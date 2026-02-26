Imports ClsAutoCommunication.ClsAutoCommunication
Imports Common
Imports Common.ClsFunction
Imports System.ComponentModel
Imports System.Text
Imports System.Threading
Imports System.IO
Imports System.IO.Pipes
Imports IisFtpManager.IisFtpManager

Public Class Form_Top
  '  Private lblMessageText As Label = LblMessage ' これがエラーの原因
  Private serverThread As Thread
  Private PrintableChack As Boolean = True
  Private StopFtpText As String = "_停止中"
  Dim myTimer As New System.Windows.Forms.Timer()
  ' SQLサーバー操作オブジェクト
  Private _SqlServer As ClsSqlServer
  Private ReadOnly Property SqlServer As ClsSqlServer
    Get
      If _SqlServer Is Nothing Then
        _SqlServer = New ClsSqlServer
      End If
      Return _SqlServer
    End Get
  End Property


  Private Sub Form_Top_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Try
      'If Not System.IO.Directory.Exists(ClsFunction.ReadSettingIniFile("FTP_DOWNLOAD_PATH", "VALUE")) Then
      '  System.IO.Directory.Move(ClsFunction.ReadSettingIniFile("FTP_DOWNLOAD_PATH", "VALUE") & StopFtpText, ClsFunction.ReadSettingIniFile("FTP_DOWNLOAD_PATH", "VALUE"))
      'End If
      StartFtp("FTPSVC")

    '待機中受信中メッセージ更新
    Task.Run(Sub() StartPipeServer())
    'serverThread.IsBackground = True
    'serverThread.Start()

    FormBorderStyle = FormBorderStyle.FixedSingle

    LblMessage.Text = "待機中"

    'Dim timer As Timer = New Timer()
    myTimer.Interval = 1000
    myTimer.Enabled = True

    AddHandler myTimer.Tick, New EventHandler(AddressOf timer_Tick)

    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, "メインメニュー", typMsgBox.MSG_ERROR)
    End Try

  End Sub

  Private Sub StartPipeServer()
    While True
      Using pipeServer As New NamedPipeServerStream("MyPipe", PipeDirection.InOut)
        Try
          Console.WriteLine("📡 サーバー: WaitForConnection 開始")
          pipeServer.WaitForConnection()
          Console.WriteLine("🔗 サーバー: 接続完了")

          ' StreamReader を先に用意（サーバーは読む側）
          Using reader As New StreamReader(pipeServer, Encoding.UTF8)
            Console.WriteLine("👂 サーバー: ReadLine 入る直前")

            Dim message As String = reader.ReadLine() ' ← ここでクライアントの送信を待つ
            Console.WriteLine("✅ サーバー: ReadLine 受信 → [" & If(message, "<Nothing>") & "]")

            Using writer As New StreamWriter(pipeServer, Encoding.UTF8) With {.AutoFlush = True}
              If message = "GET_TEXT" Then
                Console.WriteLine("🟡 指令: GET_TEXT")

                Dim currentText As String = ""
                Me.Invoke(Sub() currentText = LblMessage.Text)
                writer.WriteLine(currentText)
                Console.WriteLine("📤 サーバー: テキスト送信 → [" & currentText & "]")
              ElseIf message = "<END>" Then
                Exit Sub
              Else
                Console.WriteLine("🟢 指令: SET_TEXT → [" & message & "]")
                Me.Invoke(Sub() LblMessage.Text = message)
                writer.WriteLine("OK")
              End If
            End Using
          End Using
        Catch ex As Exception
          Throw New Exception(ex.Message)
        Finally
          If pipeServer.IsConnected Then
            pipeServer.Disconnect()
            Console.WriteLine("とじました: ")
          End If
        End Try
      End Using
    End While

  End Sub

  Private Sub PictureBox_MasterMente_Click(sender As Object, e As EventArgs) Handles PictureBox_MasterMente.Click
    Form_MasterMente.ShowDialog()
    Me.ActiveControl = Me.CloseButton

  End Sub
  Private Sub PictureBox_AchievementMente_Click(sender As Object, e As EventArgs) Handles PictureBox_AchievementMente.Click
    Form_Results.ShowDialog()
    Me.ActiveControl = Me.CloseButton
  End Sub
  Private Sub PictureBox_OutPut_Click(sender As Object, e As EventArgs) Handles PictureBox_OutPut.Click
    Form_OutPut.ShowDialog()
    Me.ActiveControl = Me.CloseButton
  End Sub

  Private Sub PictureBox_Log_Click(sender As Object, e As EventArgs) Handles PictureBox_Log.Click
    Form_Log.ShowDialog()
    Me.ActiveControl = Me.CloseButton
  End Sub

  Private Sub PictureBox_MasterMente_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox_MasterMente.MouseMove
    PictureBox_MasterMente.BorderStyle = BorderStyle.FixedSingle
  End Sub
  Private Sub PictureBox_MasterMente_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox_MasterMente.MouseLeave
    PictureBox_MasterMente.BorderStyle = BorderStyle.None
  End Sub
  Private Sub PictureBox_MasterMente_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox_MasterMente.MouseEnter
    PictureBox_MasterMente.BorderStyle = BorderStyle.FixedSingle
  End Sub

  Private Sub PictureBox_MasterMente_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox_MasterMente.MouseUp
    PictureBox_MasterMente.BorderStyle = BorderStyle.None
  End Sub
  Private Sub PictureBox_OutPut_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox_OutPut.MouseMove
    PictureBox_OutPut.BorderStyle = BorderStyle.FixedSingle
  End Sub
  Private Sub PictureBox_OutPut_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox_OutPut.MouseLeave
    PictureBox_OutPut.BorderStyle = BorderStyle.None
  End Sub
  Private Sub PictureBox_OutPut_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox_OutPut.MouseEnter
    PictureBox_OutPut.BorderStyle = BorderStyle.FixedSingle
  End Sub
  Private Sub PictureBox_OutPut_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox_OutPut.MouseUp
    PictureBox_OutPut.BorderStyle = BorderStyle.None
  End Sub

  Private Sub PictureBox_Log_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox_Log.MouseMove
    PictureBox_Log.BorderStyle = BorderStyle.FixedSingle
  End Sub

  Private Sub PictureBox_Log_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox_Log.MouseLeave
    PictureBox_Log.BorderStyle = BorderStyle.None
  End Sub

  Private Sub PictureBox_Log_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox_Log.MouseEnter
    PictureBox_Log.BorderStyle = BorderStyle.FixedSingle
  End Sub

  Private Sub PictureBox_Log_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox_Log.MouseUp
    PictureBox_Log.BorderStyle = BorderStyle.None
  End Sub

  Private Sub Form_Top_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
    Select Case e.KeyCode
      Case Keys.F1
        Form_MasterMente.ShowDialog()
        Me.ActiveControl = Me.CloseButton
      Case Keys.F2
        'LblMessage.Text = "待機停止中"
        'PrintableChack = False
        'Form_Results.ShowDialog()
        'PrintableChack = True
        'LblMessage.Text = "待機中"
        'Case Keys.F3
        OpenFormOutPut()

      Case Keys.F4
        Form_Log.ShowDialog()
        Me.ActiveControl = Me.CloseButton
      Case Keys.F10
        Close()
      Case Keys.S
        Form_Information.ShowDialog()
        Me.ActiveControl = Me.CloseButton
    End Select
  End Sub

  Private Sub PictureBox_AchievementMente_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox_AchievementMente.MouseEnter
    PictureBox_AchievementMente.BorderStyle = BorderStyle.FixedSingle
  End Sub
  Private Sub PictureBox_AchievementMente_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox_AchievementMente.MouseLeave
    PictureBox_AchievementMente.BorderStyle = BorderStyle.None
  End Sub
  Private Sub PictureBox_AchievementMente_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox_AchievementMente.MouseMove
    PictureBox_AchievementMente.BorderStyle = BorderStyle.FixedSingle
  End Sub
  Private Sub PictureBox_AchievementMente_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox_AchievementMente.MouseUp
    PictureBox_AchievementMente.BorderStyle = BorderStyle.None
  End Sub
  Private Sub PictureBox_Close_Click(sender As Object, e As EventArgs) Handles PictureBox_Close.Click
    Close()
  End Sub
  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub
  Private Sub PictureBox_Close_MouseEnter(sender As Object, e As EventArgs) Handles PictureBox_Close.MouseEnter
    PictureBox_Close.BorderStyle = BorderStyle.FixedSingle
  End Sub
  Private Sub PictureBox_Close_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox_Close.MouseLeave
    PictureBox_Close.BorderStyle = BorderStyle.None
  End Sub
  Private Sub PictureBox_Close_MouseMove(sender As Object, e As MouseEventArgs) Handles PictureBox_Close.MouseMove
    PictureBox_Close.BorderStyle = BorderStyle.FixedSingle
  End Sub
  Private Sub PictureBox_Close_MouseUp(sender As Object, e As MouseEventArgs) Handles PictureBox_Close.MouseUp
    PictureBox_Close.BorderStyle = BorderStyle.None
  End Sub
  Private Sub MasterMenteButton_Click(sender As Object, e As EventArgs) Handles MasterMenteButton.Click
    Form_MasterMente.ShowDialog()
    Me.ActiveControl = Me.CloseButton
  End Sub
  Private Sub ResultsButton_Click(sender As Object, e As EventArgs) Handles ResultsButton.Click
    LblMessage.Text = "待機停止中"
    PrintableChack = False
    Dim origMenu = Me.MainMenuStrip
    Me.MainMenuStrip = Nothing
    Form_Results.ShowDialog()
    Me.BeginInvoke(Sub()
                     Me.MainMenuStrip = origMenu
                     Me.ActiveControl = Me.CloseButton
                   End Sub)
    Me.ActiveControl = Me.CloseButton
    PrintableChack = True
    LblMessage.Text = "待機中"
  End Sub
  Private Sub OutPutButton_Click(sender As Object, e As EventArgs) Handles OutPutButton.Click
    OpenFormOutPut()
  End Sub
  Private Sub LogButton_Click(sender As Object, e As EventArgs) Handles LogButton.Click
    Form_Log.ShowDialog()
    Me.ActiveControl = Me.CloseButton
  End Sub
  Private Sub Support_MenuItem_Click(sender As Object, e As EventArgs) Handles Support_MenuItem.Click
    Form_Information.ShowDialog()
    Me.ActiveControl = Me.CloseButton
  End Sub

  Private Sub timer_Tick(sender As Object, e As EventArgs)
    AutoCommunication(PrintableChack)
  End Sub

  Private Sub SetLblMessage(prmMessage As String)
    'lblMessageText.Text = prmMessage
  End Sub

  Private Sub DisplayClose()
    Try
      StopFtp("FTPSVC")
      'System.IO.Directory.Move(ClsFunction.ReadSettingIniFile("FTP_DOWNLOAD_PATH", "VALUE"), ClsFunction.ReadSettingIniFile("FTP_DOWNLOAD_PATH", "VALUE") & StopFtpText)

    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub

  Private Sub OpenFormOutPut()

    LblMessage.Text = "待機停止中"
    PrintableChack = False
    Form_OutPut.ShowDialog()
    Me.ActiveControl = Me.CloseButton
    PrintableChack = True
    LblMessage.Text = "待機中"


    'TODO  計量器のフォルダを取得して、SetConcat_ScaleNumberの部分にはめる。
    'ファイル取込処理 
    '計量器管理を取得
    Dim tmpDt As DataTable = SelectFtpResult()

    '計量器単位でループ
    For Each tmpDr As DataRow In tmpDt.Rows
      'ファイルがあれば、移動してFor分抜ける。
      Dim BkFolder As String = ClsFunction.ReadSettingIniFile("FTP_DOWNLOAD_PATH", "VALUE") & "\" & tmpDr.Item("IP_ADDRESS") & "\bk"    'bkフォルダ取得
      If Process.GetProcessesByName("Nohin").Count = 0 _
                   AndAlso Process.GetProcessesByName("Result").Count = 0 Then
        If Dir(BkFolder & "\01TRAN*.csv") <> "" Then
          System.IO.File.Move(BkFolder & "\" & Dir(BkFolder & "\01TRAN*.csv"), ClsFunction.ReadSettingIniFile("FTP_DOWNLOAD_PATH", "VALUE") & "\" & tmpDr.Item("IP_ADDRESS") & "\01TRAN.csv")
          Exit For
        End If
      End If
    Next

  End Sub

  Private Sub Form_Top_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
    Try
    DisplayClose()
    Catch ex As Exception
      ComWriteErrLog(ex)
      ComMessageBox(ex.Message, "メインメニュー", typMsgBox.MSG_ERROR)
      e.Cancel = True
    End Try
  End Sub

  Public Function SelectFtpResult() As DataTable
    Dim sql As String = String.Empty
    Dim tmpDt As New DataTable
    sql = GetScaleSql()
    Try
      SqlServer.GetResult(tmpDt, sql)
      If tmpDt.Rows.Count = 0 Then
        MessageBox.Show("計量器マスタにデータが登録されていません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error)
      End If
      Return tmpDt

    Catch ex As Exception
      Throw New Exception(ex.Message)
    Finally
      tmpDt.Dispose()
    End Try

  End Function

  Private Function GetScaleSql() As String
    Dim sql As String = String.Empty

    sql &= "SELECT  UNIT_NUMBER "
    sql &= "  ,     IP_ADDRESS "
    sql &= "FROM    MST_Scale "

    Return sql
  End Function

  Public Class SetMessage

    Public Sub New(prmMessage As String)
      Form_Top.SetLblMessage(prmMessage)
    End Sub

  End Class

End Class