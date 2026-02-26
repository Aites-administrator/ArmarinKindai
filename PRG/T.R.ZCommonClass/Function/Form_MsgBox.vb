Public Class Form_MsgBox

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="owner"></param>
  ''' <param name="msg">メッセージ</param>
  ''' <param name="title">フォームのタイトル</param>
  ''' <param name="typMsgBtn"></param>
  ''' <param name="icon"></param>
  ''' <param name="defaultButton"></param>
  Public Overloads Sub ShowDialog(ByVal owner As System.Windows.Forms.IWin32Window,
                                       ByVal msg As String,
                                       ByVal title As String,
                                       ByVal typMsgBtn As MessageBoxButtons,
                                       ByVal icon As MessageBoxIcon,
                                       ByVal defaultButton As MessageBoxDefaultButton)

    ' フォームのタイトル
    Me.Text = title

    ' メッセージ
    lblMsg.Text = msg
    Dim setIcon As Icon

    ' アイコン
    Select Case icon
      Case MessageBoxIcon.Information
        setIcon = SystemIcons.Information
      Case MessageBoxIcon.Warning
        setIcon = SystemIcons.Warning
      Case MessageBoxIcon.Error
        setIcon = SystemIcons.Error
    End Select

    ' アイコンの描画
    Dim canvas As New Bitmap(picIcon.Width, picIcon.Height)
    Dim g As Graphics = Graphics.FromImage(canvas)
    g.DrawIcon(SystemIcons.Question, 0, 0)
    g.Dispose()
    Me.picIcon.Image = canvas

    Select Case (typMsgBtn)
      Case MessageBoxButtons.OK
        ' 使用ボタン表示・非表示設定 
        Me.btnYes.Visible = True
        Me.btnNo.Visible = False
        Me.btnCancel.Visible = False
        AddHandler btnYes.Click, AddressOf btnOK_Click
        Me.btnYes.Location = New Point(225, 200)

      'Case MessageBoxButtons.AbortRetryIgnore
      '  ' 使用ボタン表示・非表示設定 
      '  Me.btnYes.Visible = True
      '  Me.btnNo.Visible = True
      '  Me.btnCancel.Visible = True

      Case MessageBoxButtons.OKCancel
        ' 使用ボタン表示・非表示設定 
        Me.btnYes.Visible = True
        Me.btnNo.Visible = True
        Me.btnCancel.Visible = False
        AddHandler btnYes.Click, AddressOf btnOK_Click
        AddHandler btnNo.Click, AddressOf btnCancel_Click

      Case MessageBoxButtons.YesNoCancel
        ' 使用ボタン表示・非表示設定 
        Me.btnYes.Visible = True
        Me.btnNo.Visible = True
        Me.btnCancel.Visible = True
        AddHandler btnYes.Click, AddressOf btnYes_Click
        AddHandler btnNo.Click, AddressOf btnNo_Click
        AddHandler btnCancel.Click, AddressOf btnCancel_Click
        Me.btnYes.Location = New Point(50, 200)
        Me.btnYes.Location = New Point(225, 200)
        Me.btnYes.Location = New Point(400, 200)

      Case MessageBoxButtons.YesNo
        ' 使用ボタン表示・非表示設定 
        Me.btnYes.Visible = True
        Me.btnNo.Visible = True
        Me.btnCancel.Visible = False
        AddHandler btnYes.Click, AddressOf btnYes_Click
        AddHandler btnNo.Click, AddressOf btnNo_Click
        Me.btnYes.Location = New Point(100, 200)
        Me.btnNo.Location = New Point(350, 200)

        'Case MessageBoxButtons.RetryCancel
        '  ' 使用ボタン表示・非表示設定 
        '  Me.btnYes.Visible = True
        '  Me.btnNo.Visible = True
        '  Me.btnCancel.Visible = False

    End Select

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnOK_Click(sender As Object, e As EventArgs)

    ' OKボタンが押された時はDialogResult.OKを設定する
    Me.DialogResult = DialogResult.OK

    ' ShowDialog()で表示されているので閉じる
    Me.Close()

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnYes_Click(sender As Object, e As EventArgs)

    ' YESボタンが押された時はDialogResult.Yesを設定する
    Me.DialogResult = DialogResult.Yes

    ' ShowDialog()で表示されているので閉じる
    Me.Close()

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnNo_Click(sender As Object, e As EventArgs)

    ' Noボタンが押された時はDialogResult.Noを設定する
    Me.DialogResult = DialogResult.No

    ' ShowDialog()で表示されているので閉じる
    Me.Close()

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnCancel_Click(sender As Object, e As EventArgs)

    ' Cancelボタンが押された時はDialogResult.Cancelを設定する
    Me.DialogResult = DialogResult.Cancel

    ' ShowDialog()で表示されているので閉じる
    Me.Close()

  End Sub

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_MsgBox_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

    RemoveHandler btnYes.Click, AddressOf btnOK_Click
    RemoveHandler btnYes.Click, AddressOf btnYes_Click
    RemoveHandler btnNo.Click, AddressOf btnCancel_Click
    RemoveHandler btnNo.Click, AddressOf btnNo_Click
    RemoveHandler btnCancel.Click, AddressOf btnCancel_Click

  End Sub

End Class