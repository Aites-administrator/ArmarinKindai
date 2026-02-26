Imports System.Net
Imports T.R.ZCommonClass.clsCommonFnc

Public Class clsComDatabase
  Implements IDisposable

  '-------------------------------------
  '       パブリックEnum
  '-------------------------------------
  Public Enum typProvider
    ''' <summary>
    ''' SQL-Server
    ''' </summary>
    sqlServer = 0
    ''' <summary>
    ''' Access(MDB)
    ''' </summary>
    Mdb
    ''' <summary>
    ''' Access(Accdb)
    ''' </summary>
    Accdb
  End Enum

#Region "メンバ"

#Region "プライベート"
  Private _ObjCon As OleDb.OleDbConnection  ' ADOコネクション
  Private _ObjCmd As OleDb.OleDbCommand
  Private _ObjDa As OleDb.OleDbDataAdapter
  Private _ObjTran As OleDb.OleDbTransaction

  Private _Provider As typProvider
  Private _DataSource As String
  Private _DefaultDatabase As String
  Private _UserId As String
  Private _Password As String
  Private _TrnRunnig As Boolean              ' トランザクション状態フラグ（True：トランザクション開始中）
  Private _QueryTimeOut As Integer
  Private _SqlHistory As String
#End Region

#End Region

#Region "プロパティー"

#Region "プライベート"
  Private ReadOnly Property DbConnection() As OleDb.OleDbConnection
    Get
      If IsConnected() = False Then DbConnect()
      Return _ObjCon
    End Get
  End Property
#End Region

#Region "パブリック"
  ' プロバイダー
  Public Property Provider As typProvider
    Get
      Return _Provider
    End Get
    Set(value As typProvider)
      _Provider = value
    End Set
  End Property

  ' データベースサーバー
  Public WriteOnly Property DataSource As String
    Set(value As String)
      _DataSource = value
    End Set
  End Property

  ' データベース
  Public WriteOnly Property DefaultDatabase As String
    Set(value As String)
      _DefaultDatabase = value
    End Set
  End Property

  ' 接続ID
  Public WriteOnly Property UserId As String
    Set(value As String)
      _UserId = value
    End Set
  End Property

  ' パスワード
  Public WriteOnly Property Password As String
    Set(value As String)
      _Password = value
    End Set
  End Property

  ''' <summary>
  ''' クエリータイムアウト時間
  ''' </summary>
  ''' <returns></returns>
  Public Property QueryTimeOut() As Integer
    Get
      Return _QueryTimeOut
    End Get
    Set(value As Integer)
      _QueryTimeOut = value
    End Set
  End Property


#End Region

#End Region

#Region "コンストラクタ"
  Public Sub New()
    ' トランザクションフラグ解除
    _TrnRunnig = False

    ' クエリータイムアウトデフォルト
    _QueryTimeOut = 30

    ' SQL実行履歴初期化
    _SqlHistory = String.Empty

  End Sub

#End Region

#Region "メソッド"

#Region "プライベート"
  ' データベース接続確認
  Private Function IsConnected() As Boolean
    Dim bConnected As Boolean

    ' 存在確認
    bConnected = Not (_ObjCon Is Nothing)
    ' 接続確認
    If bConnected Then bConnected = (_ObjCon.State = ConnectionState.Open)
    Return bConnected
  End Function

  ' プロバイダ文字列取得
  Private Function GetProviderName() As String
    Dim ProviderName As String = String.Empty

    Select Case _Provider
      Case typProvider.sqlServer
        ProviderName = "SQLOLEDB"
      Case typProvider.Mdb
        ProviderName = "Microsoft.ACE.OLEDB.12.0"
      Case typProvider.Accdb
        ProviderName = "Microsoft.ACE.OLEDB.12.0"
      Case Else
        Call Err.Raise(9999, "GetProviderName", "適切なプロバイダーが設定されていません")
    End Select

    Return ProviderName
  End Function

  Private Sub ReleaseTrnRb()
    If _ObjTran IsNot Nothing Then
      _ObjTran.Rollback()
      _ObjTran.Dispose()
      _ObjTran = Nothing
      _SqlHistory = String.Empty
    End If
  End Sub

  Private Sub ReleaseTrnCmt()
    If _ObjTran IsNot Nothing Then
      _ObjTran.Commit()
      _ObjTran.Dispose()
      _ObjTran = Nothing
      ' SQLSERVERのみログを残す
      If (_Provider = typProvider.sqlServer) Then
        Call WriteExecuteLog(_SqlHistory)
      End If
      _SqlHistory = String.Empty
    End If
  End Sub
#End Region

#Region "パブリック"

  ' DB接続
  Public Sub DbConnect()
    Dim ConnectionString As String

    ' 接続中なら切断
    If IsConnected() Then
      Call DbDisconnect()
      _ObjCon = Nothing
    End If

    ' 接続オブジェクト初期化
    _ObjCon = New OleDb.OleDbConnection

    ' 接続文字列作成
    ConnectionString = ""
    ConnectionString = ConnectionString & "Provider=" & GetProviderName() & ";"
    ConnectionString = ConnectionString & "Data Source=" & _DataSource & ";"

    ' 以下、SQL-Server接続時のみ設定
    If _Provider = typProvider.sqlServer Then
      ConnectionString = ConnectionString & "Initial Catalog=" & _DefaultDatabase & ";"
      ConnectionString = ConnectionString & "User ID=" & _UserId & ";"
      ConnectionString = ConnectionString & "Password=" & _Password & ";"
    End If

    
    _ObjCon.ConnectionString = ConnectionString
    ' 接続
    _ObjCon.Open()

  End Sub

  ' DB切断
  Public Sub DbDisconnect()
    Try
      If IsConnected() Then
        _ObjCon.Close()
        _SqlHistory = String.Empty
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
    End Try
  End Sub

  ''' <summary>
  ''' 参照系SQL実行
  ''' </summary>
  ''' <param name="dt">実行結果を保持するデータテーブル</param>
  ''' <param name="sql">実行するSQL文</param>
  Public Sub GetResult(ByRef dt As DataTable _
                                , sql As String, Optional prmLog As Boolean = False)

    '指定したSQL文が空白の場合、クエリーを実行しない
    If String.IsNullOrWhiteSpace(sql) Then
      Return
    End If

    Try
      _ObjCmd = DbConnection.CreateCommand
      With _ObjCmd
        .CommandTimeout = _QueryTimeOut
        .CommandText = sql
        If _TrnRunnig Then
          .Transaction = _ObjTran
        End If
      End With

      If (prmLog = False) Then
        If (_Provider = typProvider.sqlServer) Then
          Call WriteExecuteLog(sql)
        End If
      End If

      '---
      _ObjDa = New OleDb.OleDbDataAdapter
      _ObjDa.SelectCommand = _ObjCmd
      If dt Is Nothing Then
        dt = New DataTable
      Else
        dt.Clear()
      End If

      _ObjDa.Fill(dt)
      '---
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("SQL実行時エラー:" & sql)
    Finally
      If _ObjDa IsNot Nothing Then
        _ObjDa.Dispose()
        _ObjDa = Nothing
      End If
    End Try
  End Sub

  ''' <summary>
  ''' 更新系クエリー実行
  ''' </summary>
  ''' <param name="sql">実行するSQL文</param>
  ''' <returns></returns>
  Public Function Execute(sql As String) As Integer

    Dim AffectedCount As Integer

    Try
      'トランザクション開始
      If _ObjTran Is Nothing Then _ObjTran = DbConnection.BeginTransaction()

      _ObjCmd = DbConnection.CreateCommand()

      With _ObjCmd
        .CommandTimeout = _QueryTimeOut
        .Transaction = _ObjTran
        .CommandText = sql
        _SqlHistory &= sql & ";;;"
        AffectedCount = .ExecuteNonQuery()
      End With

      If _TrnRunnig = False Then
        ReleaseTrnCmt()
      End If

    Catch ex As Exception

      Call ComWriteErrLog(ex)   ' Error出力

      If _TrnRunnig = False Then
        ReleaseTrnRb()
      End If

      Throw New Exception("SQL実行時エラー:" & sql)
    End Try

    Return AffectedCount
  End Function

  ' トランザクション開始
  Public Sub TrnStart()
    If _TrnRunnig Then
      Throw New Exception("トランザクションは既に開始されています。")
    Else
      _ObjTran = DbConnection.BeginTransaction()
      _TrnRunnig = True
    End If
  End Sub

  ' トランザクションコミット
  Public Sub TrnCommit()
    If _TrnRunnig Then
      ReleaseTrnCmt()
      _TrnRunnig = False
    End If
  End Sub

  ' トランザクションロールバック
  Public Sub TrnRollBack()
    If _TrnRunnig Then
      ReleaseTrnRb()
      _TrnRunnig = False
    End If
  End Sub


  ''' <summary>
  ''' SQL実行ログ保存
  ''' </summary>
  ''' <param name="prmSql">SQL文</param>
  ''' <remarks>テストコードの為、エラーが発生しても無視</remarks>
  Private Sub WriteExecuteLog(prmSql As String)

    Try
      Dim tmpProcTime As String = ComGetProcTime()
      Dim tmpExeFileName As String = System.IO.Path.GetFileName(System.Windows.Forms.Application.ExecutablePath)
      Dim logFileName As String = "SqlLog_" & Date.Parse(tmpProcTime).ToString("yyyyMM") & ".log"
      Dim logText As String = tmpProcTime & ":" & Dns.GetHostName() & ":" & tmpExeFileName & ":" & prmSql
      ' ComWriteLog(logText, "\\nikserver21\d$\TRZdotDX\sqllog\" & logFileName)
      Dim myPath As String = System.IO.Path.Combine(My.Application.Info.DirectoryPath, logFileName)
      ComWriteLog(logText, myPath)
    Catch ex As Exception

    End Try
  End Sub

#End Region

#End Region

#Region "IDisposable Support"
  Private disposedValue As Boolean ' 重複する呼び出しを検出するには

  ' IDisposable
  Protected Overridable Sub Dispose(disposing As Boolean)
    If Not disposedValue Then
      If disposing Then
        ' TODO: マネージド状態を破棄します (マネージド オブジェクト)。
        DbDisconnect()

        If _ObjCmd IsNot Nothing Then
          _ObjCmd.Dispose()
          _ObjCmd = Nothing
        End If

        If _ObjDa IsNot Nothing Then
          _ObjDa.Dispose()
          _ObjDa = Nothing
        End If

        If _ObjTran IsNot Nothing Then
          _ObjTran.Dispose()
          _ObjTran = Nothing
        End If

        If _ObjCon IsNot Nothing Then
          _ObjCon.Dispose()
          _ObjCon = Nothing
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

    ' GetResult処理を複数回実行すると、laccdbファイルが残る不具合対応
    DbConnection.Close()

  End Sub
#End Region

End Class
