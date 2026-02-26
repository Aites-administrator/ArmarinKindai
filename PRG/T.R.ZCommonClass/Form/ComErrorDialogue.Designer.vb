<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ComErrorDialogue
  Inherits System.Windows.Forms.Form

  'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    Try
      If disposing AndAlso components IsNot Nothing Then
        components.Dispose()
      End If
    Finally
      MyBase.Dispose(disposing)
    End Try
  End Sub

  'Windows フォーム デザイナーで必要です。
  Private components As System.ComponentModel.IContainer

  'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
  'Windows フォーム デザイナーを使用して変更できます。  
  'コード エディターを使って変更しないでください。
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ComErrorDialogue))
    Me.Label1 = New System.Windows.Forms.Label()
    Me.btnClose = New System.Windows.Forms.Button()
    Me.txtErrorMsg = New System.Windows.Forms.TextBox()
    Me.SuspendLayout()
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.BackColor = System.Drawing.Color.Transparent
    Me.Label1.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label1.ForeColor = System.Drawing.Color.White
    Me.Label1.Location = New System.Drawing.Point(16, 13)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(87, 33)
    Me.Label1.TabIndex = 5
    Me.Label1.Text = "Error"
    Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'btnClose
    '
    Me.btnClose.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.btnClose.Location = New System.Drawing.Point(486, 367)
    Me.btnClose.Name = "btnClose"
    Me.btnClose.Size = New System.Drawing.Size(82, 31)
    Me.btnClose.TabIndex = 2
    Me.btnClose.TabStop = False
    Me.btnClose.Text = "OK"
    Me.btnClose.UseVisualStyleBackColor = True
    '
    'txtErrorMsg
    '
    Me.txtErrorMsg.BackColor = System.Drawing.Color.Black
    Me.txtErrorMsg.BorderStyle = System.Windows.Forms.BorderStyle.None
    Me.txtErrorMsg.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 26.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.txtErrorMsg.ForeColor = System.Drawing.Color.White
    Me.txtErrorMsg.Location = New System.Drawing.Point(22, 59)
    Me.txtErrorMsg.Multiline = True
    Me.txtErrorMsg.Name = "txtErrorMsg"
    Me.txtErrorMsg.ReadOnly = True
    Me.txtErrorMsg.Size = New System.Drawing.Size(546, 293)
    Me.txtErrorMsg.TabIndex = 1
    Me.txtErrorMsg.Text = "ここにエラーメッセージが表示されます"
    '
    'ComErrorDialogue
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
    Me.ClientSize = New System.Drawing.Size(584, 411)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.btnClose)
    Me.Controls.Add(Me.txtErrorMsg)
    Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
    Me.Name = "ComErrorDialogue"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "ComErrorDialogue"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents Label1 As Label
  Friend WithEvents btnClose As Button
  Friend WithEvents txtErrorMsg As TextBox
End Class
