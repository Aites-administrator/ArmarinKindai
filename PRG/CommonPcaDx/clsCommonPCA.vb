Imports T.R.ZCommonClass.clsCommonFnc
Imports PCA.ApplicationIntegration
Imports PCA.Kon.Integration

''' <summary>
''' PCA API操作クラス
''' </summary>
Public Class clsCommonPCA
  Implements IDisposable


#Region "定数定義"
  Private Const APP_ID As String = "Kon20"
  Private Const STATUS_FAILURE As IntegratedStatus = IntegratedStatus.Failure
  Private Const STATUS_SUCCESS As IntegratedStatus = IntegratedStatus.Success
#End Region

#Region "メンバ"
#Region "プライベート"

  Private _PcaAPIFactory As KonIntegrationFactory = Nothing

  Private _PcaAPIApp As IntegratedApplication

#End Region
#End Region

#Region "プロパティー"

#Region "プライベート"
  Private ReadOnly Property PcaAPIFactory As KonIntegrationFactory
    Get
      If _PcaAPIFactory Is Nothing Then
        _PcaAPIFactory = New KonIntegrationFactory()

        If _PcaAPIFactory.FindIntegratedApplications("").Status = STATUS_FAILURE Then
          Throw New Exception("連携対象のアプリケーションが見つかりません。")
        End If
        _PcaAPIFactory.AppId = APP_ID
      End If

      Return _PcaAPIFactory
    End Get
  End Property

  Private ReadOnly Property PcaAPIApp As IntegratedApplication
    Get

      If _PcaAPIApp Is Nothing Then
        _PcaAPIApp = PcaAPIFactory.CreateApplication
      End If

      ' 未接続時は再接続
      If IsConnected() = False Then
        Call ConnectionApi()
      End If

      Return _PcaAPIApp
    End Get
  End Property
#End Region

#Region "パブリック"

  ''' <summary>
  ''' ユーザーID
  ''' </summary>
  ''' <returns></returns>
  Public Property UserID As String

  ''' <summary>
  ''' パスワード
  ''' </summary>
  ''' <returns></returns>
  Public Property PassWord As String

  ''' <summary>
  ''' プログラムID
  ''' </summary>
  ''' <returns></returns>
  Public Property ProgramId As String

  ''' <summary>
  ''' プログラム名
  ''' </summary>
  ''' <returns></returns>
  Public Property ProgramName As String

  ''' <summary>
  ''' データベース名
  ''' </summary>
  ''' <returns></returns>
  Public Property DataAreaName As String

  ''' <summary>
  ''' バージョン
  ''' </summary>
  ''' <returns></returns>
  Public Property Version As String

#End Region

#End Region

#Region "コンストラクタ"
  Public Sub New()

  End Sub

  Public Sub New(prmUserId As String _
                 , prmPassWord As String _
                 , prmProgramId As String _
                 , prmProgramName As String _
                 , prmDataAreaName As String)

    Me.UserID = prmUserId
    Me.PassWord = prmPassWord
    Me.ProgramId = prmProgramId
    Me.ProgramName = prmProgramName
    Me.DataAreaName = prmDataAreaName

  End Sub
#End Region

#Region "メソッド"

#Region "プライベート"

  ''' <summary>
  ''' API 接続
  ''' </summary>
  Private Sub ConnectionApi()
    Dim tmpAPIResult As IIntegratedResult

    With _PcaAPIApp
      .UserId = UserID
      .Password = PassWord
      .ProgramId = ProgramId
      .ProgramName = ProgramName
    End With

    Try
      tmpAPIResult = _PcaAPIApp.LogOnSystem
      If tmpAPIResult.Status = STATUS_FAILURE Then
        Throw New Exception(tmpAPIResult.ErrorMessage)
      End If

      '領域設定
      tmpAPIResult = _PcaAPIApp.SelectDataArea(DataAreaName)
      ' 接続実行
      If tmpAPIResult.Status <> STATUS_SUCCESS Then
        Throw New Exception(tmpAPIResult.ErrorMessage)
      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("PCA API接続に失敗しました")
    End Try

  End Sub

  ''' <summary>
  ''' API接続確認
  ''' </summary>
  ''' <returns>
  '''  True  -  接続されている
  '''  False -  接続されていない
  ''' </returns>
  Private Function IsConnected() As Boolean
    Dim ret As Boolean = True

    Try
      ' このメソッドでエラーが発生したら接続されていないと判断
      If _PcaAPIApp.GetLogOnDataArea().BusinessValue = "" Then

      End If
    Catch ex As Exception
      ret = False
    End Try

    Return ret
  End Function

#End Region

#Region "パブリック"

  ''' <summary>
  ''' API切断
  ''' </summary>
  Public Sub Disconnect()
    Try
      PcaAPIApp.LogOffSystem()
    Catch ex As Exception
      ' 切断に失敗しても処理なし
    End Try

  End Sub

  Public Sub Create(prmURI As String, prmXmlText As String)
    Dim tmpAPIResult As IIntegratedResult = Nothing

    Try
      tmpAPIResult = PcaAPIApp.Create(prmURI, prmXmlText)
      If tmpAPIResult.Status <> STATUS_SUCCESS Then
        Throw New Exception(tmpAPIResult.ErrorMessage & vbCrLf & "POST XML:" & prmXmlText & vbCrLf & tmpAPIResult.BusinessValue.ToString())
      End If
    Catch ex As Exception
      Dim msg As String = ex.Message
      Call ComWriteErrLog(ex)
      Throw New Exception("PCA API '" & prmURI & "'に失敗しました" & vbCrLf & msg)
    Finally
      Call Disconnect()
    End Try

  End Sub

  Public Sub Modify(prmURI As String, prmXmlText As String)
    Dim tmpAPIResult As IIntegratedResult = Nothing

    Try
      tmpAPIResult = PcaAPIApp.Modify(prmURI, prmXmlText)
      If tmpAPIResult.Status <> STATUS_SUCCESS Then
        Throw New Exception(tmpAPIResult.ErrorMessage & vbCrLf & "POST XML:" & prmXmlText & vbCrLf & tmpAPIResult.BusinessValue.ToString())
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("PCA API '" & prmURI & "'に失敗しました")
    Finally
      Call Disconnect()
    End Try

  End Sub

  Public Function GetData(prmURI As String, prmXmlText As String) As String
    Dim tmpAPIResult As IIntegratedResult = Nothing

    Try
      tmpAPIResult = PcaAPIApp.FindByCondition(prmURI, prmXmlText)
      If tmpAPIResult.Status <> STATUS_SUCCESS Then
        Throw New Exception(tmpAPIResult.ErrorMessage & vbCrLf & "POST XML:" & prmXmlText & vbCrLf & tmpAPIResult.BusinessValue.ToString())
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("PCA API '" & prmURI & "'に失敗しました")
    Finally
      Call Disconnect()
    End Try

    Return tmpAPIResult.BusinessValue
  End Function

  Public Function GetData(prmURI As String) As String
    Dim tmpAPIResult As IIntegratedResult = Nothing

    Try
      tmpAPIResult = PcaAPIApp.Find(prmURI)
      If tmpAPIResult.Status <> STATUS_SUCCESS Then
        Throw New Exception(tmpAPIResult.ErrorMessage & vbCrLf & "POST XML:" & prmURI & vbCrLf & tmpAPIResult.BusinessValue.ToString())
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("PCA API '" & prmURI & "'に失敗しました")
    Finally
      Call Disconnect()
    End Try

    Return tmpAPIResult.BusinessValue
  End Function

#End Region
#End Region

#Region "IDisposable Support"
  Private disposedValue As Boolean ' 重複する呼び出しを検出するには

  ' IDisposable
  Protected Overridable Sub Dispose(disposing As Boolean)
    If Not disposedValue Then
      If disposing Then
        ' TODO: マネージド状態を破棄します (マネージド オブジェクト)。

        ' API接続解除
        Call Disconnect()

        ' オブジェクト解放
        If _PcaAPIFactory IsNot Nothing Then
          _PcaAPIFactory.Dispose()
          _PcaAPIFactory = Nothing
        End If

        If _PcaAPIApp IsNot Nothing Then
          _PcaAPIApp.Dispose()
          _PcaAPIApp = Nothing
        End If
      End If

      ' TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、下の Finalize() をオーバーライドします。
      ' TODO: 大きなフィールドを null に設定します。
    End If
    disposedValue = True
  End Sub

  ' TODO: 上の Dispose(disposing As Boolean) にアンマネージド リソースを解放するコードが含まれる場合にのみ Finalize() をオーバーライドします。
  'Protected Overrides Sub Finalize()
  '    ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
  '    Dispose(False)
  '    MyBase.Finalize()
  'End Sub

  ' このコードは、破棄可能なパターンを正しく実装できるように Visual Basic によって追加されました。
  Public Sub Dispose() Implements IDisposable.Dispose
    ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
    Dispose(True)
    ' TODO: 上の Finalize() がオーバーライドされている場合は、次の行のコメントを解除してください。
    ' GC.SuppressFinalize(Me)
  End Sub
#End Region

End Class
