<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_MsgBox
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
    Me.picIcon = New System.Windows.Forms.PictureBox()
    Me.btnYes = New System.Windows.Forms.Button()
    Me.btnNo = New System.Windows.Forms.Button()
    Me.btnCancel = New System.Windows.Forms.Button()
    Me.lblMsg = New System.Windows.Forms.Label()
    CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'picIcon
    '
    Me.picIcon.Location = New System.Drawing.Point(50, 50)
    Me.picIcon.Name = "picIcon"
    Me.picIcon.Size = New System.Drawing.Size(50, 50)
    Me.picIcon.TabIndex = 0
    Me.picIcon.TabStop = False
    '
    'btnYes
    '
    Me.btnYes.Location = New System.Drawing.Point(50, 200)
    Me.btnYes.Name = "btnYes"
    Me.btnYes.Size = New System.Drawing.Size(120, 30)
    Me.btnYes.TabIndex = 1
    Me.btnYes.Text = "はい"
    Me.btnYes.UseVisualStyleBackColor = True
    '
    'btnNo
    '
    Me.btnNo.Location = New System.Drawing.Point(225, 200)
    Me.btnNo.Name = "btnNo"
    Me.btnNo.Size = New System.Drawing.Size(120, 30)
    Me.btnNo.TabIndex = 2
    Me.btnNo.Text = "いいえ"
    Me.btnNo.UseVisualStyleBackColor = True
    '
    'btnCancel
    '
    Me.btnCancel.Location = New System.Drawing.Point(400, 200)
    Me.btnCancel.Name = "btnCancel"
    Me.btnCancel.Size = New System.Drawing.Size(120, 30)
    Me.btnCancel.TabIndex = 3
    Me.btnCancel.Text = "キャンセル"
    Me.btnCancel.UseVisualStyleBackColor = True
    '
    'lblMsg
    '
    Me.lblMsg.Location = New System.Drawing.Point(110, 52)
    Me.lblMsg.Name = "lblMsg"
    Me.lblMsg.Size = New System.Drawing.Size(409, 128)
    Me.lblMsg.TabIndex = 4
    Me.lblMsg.Text = "Label1"
    '
    'Form_MsgBox
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(584, 259)
    Me.Controls.Add(Me.lblMsg)
    Me.Controls.Add(Me.btnCancel)
    Me.Controls.Add(Me.btnNo)
    Me.Controls.Add(Me.btnYes)
    Me.Controls.Add(Me.picIcon)
    Me.MaximizeBox = False
    Me.MinimizeBox = False
    Me.Name = "Form_MsgBox"
    Me.ShowIcon = False
    Me.Text = "Form1"
    CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)

  End Sub

  Friend WithEvents picIcon As PictureBox
  Friend WithEvents btnYes As Button
  Friend WithEvents btnNo As Button
  Friend WithEvents btnCancel As Button
  Friend WithEvents lblMsg As Label
End Class
