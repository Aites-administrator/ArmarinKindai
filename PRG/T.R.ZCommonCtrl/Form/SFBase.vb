Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsComDatabase

Public Class SFBase

  ''' <summary>
  ''' サブフォーム戻り値列挙体
  ''' </summary>
  Public Enum typSfResult
    ' 足りない場合は足して下さい
    SF_OK = 0
    SF_CANCEL
    SF_CLOSE
  End Enum

#Region "メンバ"

#Region "パブリック"

  ''' <summary>
  ''' 戻り値保持用
  ''' </summary>
  Public SfResult As typSfResult

  ''' <summary>
  ''' 画面初期化関数デリゲート
  ''' </summary>
  ''' <param name="prmTargetData">親画面より渡されるパラメータ</param>
  Delegate Sub CallBackInitForm(ByVal prmTargetData As Dictionary(Of String, String))

  ''' <summary>
  ''' 画面初期化関数本体
  ''' </summary>
  Public lcCallBackInitForm As CallBackInitForm
#End Region

#End Region

#Region "メソッド"

#Region "パブリック"

  ''' <summary>
  ''' サブフォーム起動処理
  ''' </summary>
  ''' <param name="prmTargetData">親画面より渡されるパラメータ</param>
  ''' <returns>押下されたボタン</returns>
  ''' <remarks>
  ''' 戻り値は本クラスを継承した先のクラスで、フォームを閉じる前に設定すること
  ''' </remarks>
  Public Function ShowSubForm(prmTargetData As Dictionary(Of String, String)) As typSfResult

    SfResult = typSfResult.SF_OK

    ' 画面初期化処理
    If lcCallBackInitForm IsNot Nothing Then
      lcCallBackInitForm(prmTargetData)
    End If

    ' 画面表示
    Me.ShowDialog()

    ' 押下されたボタンを返す
    Return SfResult
  End Function

  ''' <summary>
  ''' サブフォーム起動処理
  ''' </summary>
  ''' <param name="prmTargetData">親画面より渡されるパラメータ</param>
  ''' <param name="prmForm">呼び出し元の親フォーム</param>
  ''' <returns>押下されたボタン</returns>
  ''' <remarks>
  ''' 戻り値は本クラスを継承した先のクラスで、フォームを閉じる前に設定すること
  ''' </remarks>
  ''' 
  Public Function ShowSubForm(prmTargetData As Dictionary(Of String, String), prmForm As Form) As typSfResult

    SfResult = typSfResult.SF_OK

    ' 画面初期化処理
    If lcCallBackInitForm IsNot Nothing Then
      lcCallBackInitForm(prmTargetData)
    End If

    ' 画面表示
    Me.ShowDialog(prmForm)

    ' 押下されたボタンを返す
    Return SfResult
  End Function

  ''' <summary>
  ''' サブフォーム起動処理
  ''' </summary>
  ''' <param name="prmForm">呼び出し元の親フォーム</param>
  ''' <returns>押下されたボタン</returns>
  ''' <remarks>
  ''' 戻り値は本クラスを継承した先のクラスで、フォームを閉じる前に設定すること
  ''' </remarks>
  ''' 
  Public Function ShowSubForm(prmForm As Form) As typSfResult

    SfResult = typSfResult.SF_OK

    ' 画面表示
    Me.ShowDialog(prmForm)

    ' 押下されたボタンを返す
    Return SfResult
  End Function



#End Region

#End Region

#Region "イベントプロシージャー"

  ''' <summary>
  ''' フォームロード時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub SFBase_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Me.KeyPreview = True

    ' コンボボックス初期化
    comInitCmb(Me)

    ' ダブルバッファリング有効
    Me.DoubleBuffered = True

    ' 呼び出し元の親フォームが存在するかどうか判定
    If Me.Owner IsNot Nothing Then
      ' 親フォームの真ん中に表示する
      Me.Location = New Point(
        Me.Owner.Location.X + (Me.Owner.Width - Me.Width) \ 2,
        Me.Owner.Location.Y + (Me.Owner.Height - Me.Height) \ 2)
    End If

  End Sub

  ''' <summary>
  ''' キー入力時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub SFBase_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    ' DataGridView以外でエンターキーが押されたら次のコントロールにフォーカスを移動する
    If e.KeyCode = Keys.Enter _
      AndAlso Me.ActiveControl IsNot Nothing _
      AndAlso Me.ActiveControl.GetType().Name.ToUpper <> "DataGridView".ToUpper Then
      Call SetFocusNextCtrl(Me.ActiveControl)
    End If
  End Sub

  ''' <summary>
  ''' 閉じられた場合
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub SFBase_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

    ' プロセスの終了
    ProcessKill()

  End Sub

#End Region

End Class