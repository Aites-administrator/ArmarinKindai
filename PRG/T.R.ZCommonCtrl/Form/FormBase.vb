Imports T.R.ZCommonClass.clsCommonFnc
Imports IpcService
Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels
Imports System.Runtime.Remoting.Channels.Ipc

Public Class FormBase
  Inherits Form

#Region "メンバ"
#Region "パブリック"

  'IPC用クラスの生成
  Public WithEvents IpcServiceClass As New IpcService.clsIpcService

  ' 非表示→表示時コールバック（共通）
  Delegate Sub CallBackShowForm(ByVal prmMsg As String)
  Private lcCallBackCallBackShowForm As New CallBackShowForm(AddressOf ShowForm)

  ' 非表示→表示時コールバック（個別）
  Delegate Sub CallBackShowFormLc()
  Public lcCallBackShowFormLc As CallBackShowFormLc

#End Region
#End Region

#Region "メソッド"

#Region "IPC関連"

  ''' <summary>
  ''' IPCサービス初期化
  ''' </summary>
  ''' <param name="prmPrgId">プログラムID</param>
  ''' <remarks>
  ''' 起動後はプログラムIDで再起動メッセージの待ち受けを行い
  ''' 二重起動時は後で起動したプロセスがプログラムIDで再表示メッセージを送信する
  ''' </remarks>
  Public Sub InitIPC(prmPrgId As String)
    'IPCチャネルを用意
    Dim IpcChannel As New IpcServerChannel(prmPrgId)
    ChannelServices.RegisterChannel(IpcChannel, False)
    Dim strSClassName As String = GetType(clsIpcService).Name
    RemotingConfiguration.RegisterWellKnownServiceType(GetType(clsIpcService), strSClassName, WellKnownObjectMode.SingleCall)
    '「ServiceClass」を参照できるように設定
    Dim ref As ObjRef = RemotingServices.Marshal(IpcServiceClass, strSClassName)

    'IPC受信準備
    IpcChannel.StartListening(Nothing)
  End Sub

  ''' <summary>
  ''' IPCメッセージ受信時処理
  ''' </summary>
  ''' <param name="message">プログラムID（使用しません）</param>
  ''' <remarks>
  ''' 二重起動時に後で起動したプロセスから送信されたメッセージ受信時処理
  ''' </remarks>
  Private Sub IpcServiceClass_RaiseClientEvent(ByVal message As String) Handles IpcServiceClass.RaiseClientEvent
    'このイベント処理は、別プロセスのServiceClassからRaiseされるので、
    'このフォームのコントロールにアクセスするにはデリゲート処理を行う
    '(クライアントから受信したメッセージを処理)

    Me.Invoke(lcCallBackCallBackShowForm, New Object() {message})
  End Sub

  ''' <summary>
  ''' 画面再表示
  ''' </summary>
  ''' <param name="prmMsg"></param>
  ''' <remarks>
  ''' 二重起動したプロセスよりIPC経由でコールバックで実行される
  ''' </remarks>
  Private Sub ShowForm(ByVal prmMsg As String)

    Me.ShowInTaskbar = True
    Me.Show()

    ' 再起動時個別処理
    If lcCallBackShowFormLc IsNot Nothing Then
      lcCallBackShowFormLc()
    End If
  End Sub

#End Region

#Region "コントロール制御関連"

  ''' <summary>
  ''' 画面上の全てのコントロールを初期化する
  ''' </summary>
  ''' <param name="prmExclusionControls">除外するコントロールリスト</param>
  ''' <remarks>
  '''  コンボボックスとテキストボックスが対象
  ''' </remarks>
  Public Sub AllClear(Optional prmExclusionControls As List(Of Control) = Nothing)
    Call ComInitCmb(Me, prmExclusionControls)
    Call InitTxt(prmExclusionControls)
  End Sub

  ''' <summary>
  ''' 画面上の全てのコントロールにメッセージラベルを設定
  ''' </summary>
  ''' <param name="prmMsglbl">メッセージを表示するラベル</param>
  ''' <remarks>
  '''  CmbBase,TxtBase,BtnBaseを継承しているコントロールと
  '''  clsDataGridが対象
  ''' </remarks>
  Public Sub SetMsgLbl(prmMsglbl As Label)
    Dim tmpControls As Control() = ComGetAllControls(Me)

    ' CmbBase,TxtBase,BtnBaseを継承しているコントロールにメッセージ表示オブジェクトを設定
    For Each tmpCtrl As Control In tmpControls
      If IsTargetControl(New CmbBase, tmpCtrl) Then
        DirectCast(tmpCtrl, CmbBase).SetMsgLabel(prmMsglbl)
      ElseIf IsTargetControl(New TxtBase, tmpCtrl) Then
        DirectCast(tmpCtrl, TxtBase).SetMsgLabel(prmMsglbl)
      ElseIf IsTargetControl(New BtnBase, tmpCtrl) Then
        DirectCast(tmpCtrl, BtnBase).SetMsgLabel(prmMsglbl)
      End If
    Next

  End Sub

  ''' <summary>
  ''' 画面上の全てのコントロールを非表示にする
  ''' </summary>
  Public Sub AllHide()

    Dim tmpControls As Control() = ComGetAllControls(Me)
    For Each tmpCtrl As Control In tmpControls
      tmpCtrl.Hide()
    Next
  End Sub

  ''' <summary>
  ''' 画面上の全テキストボックス初期化
  ''' </summary>
  ''' <param name="prmExclusionControls">除外対象コントロール</param>
  ''' <remarks>
  ''' TxtBaseを継承したコントロールのみ対象
  ''' </remarks>
  Private Sub InitTxt(Optional prmExclusionControls As List(Of Control) = Nothing)
    Dim tmpControls As Control() = ComGetAllControls(Me)
    For Each tmpCtrl As Control In tmpControls
      If IsTargetControl(New TxtMstBase, tmpCtrl) Then
        ' 除外対象のコントロールで無いなら初期化
        If prmExclusionControls Is Nothing _
          OrElse prmExclusionControls.Contains(tmpCtrl) = False Then
          DirectCast(tmpCtrl, TxtMstBase).Text = ""
        End If
      Else
        If IsTargetControl(New TxtBase, tmpCtrl) Then
          ' 除外対象のコントロールで無いなら初期化
          If prmExclusionControls Is Nothing _
          OrElse prmExclusionControls.Contains(tmpCtrl) = False Then
            DirectCast(tmpCtrl, TxtBase).Text = ""
          End If
        End If
      End If
    Next

  End Sub

#End Region



#End Region

#Region "イベントプロシージャー"

  Private Sub BaseForm_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    ' DataGridView以外でエンターキーが押されたら次のコントロールにフォーカスを移動する
    If e.KeyCode = Keys.Enter _
      AndAlso Me.ActiveControl IsNot Nothing _
      AndAlso Me.ActiveControl.GetType().Name.ToUpper <> "DataGridView".ToUpper Then
      Call SetFocusNextCtrl(Me.ActiveControl)
    End If
  End Sub

  Private Sub BaseForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    'フォームを画面の真ん中に表示する
    Me.SetBounds((Screen.PrimaryScreen.Bounds.Width - Width) / 2, (Screen.PrimaryScreen.Bounds.Height - Height) / 2 - 25, Width, Height)

    Me.KeyPreview = True

    ' コンボボックス初期化
    ComInitCmb(Me)

    ' ダブルバッファリング有効
    Me.DoubleBuffered = True

  End Sub

  ''' <summary>
  ''' フォームアクティブ時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_Activated(sender As Object, e As EventArgs) Handles MyBase.Activated
    For Each tmpCtrl As Control In ComGetAllControls(Me)
      If IsDataGridView(tmpCtrl) Then
        ' フォーム上の全てのDataGridViewのツールチップを有効にする
        DirectCast(tmpCtrl, DataGridView).ShowCellToolTips = True
      End If
    Next
  End Sub

  ''' <summary>
  ''' フォームアクティブ解除時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_Deactivate(sender As Object, e As EventArgs) Handles MyBase.Deactivate
    For Each tmpCtrl As Control In ComGetAllControls(Me)
      If IsDataGridView(tmpCtrl) Then
        ' フォーム上の全てのDataGridViewのツールチップを無効にする
        ' ※ツールチップ表示前にフォーカスを外れると異常終了するバグの対策
        DirectCast(tmpCtrl, DataGridView).ShowCellToolTips = False
      End If
    Next
  End Sub

  Private Sub InitializeComponent()
    Me.SuspendLayout()
    '
    'FormBase
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
    Me.ClientSize = New System.Drawing.Size(284, 261)
    Me.Name = "FormBase"
    Me.ResumeLayout(False)

  End Sub

#End Region

End Class
