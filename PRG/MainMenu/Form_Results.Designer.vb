Imports T.R.ZCommonCtrl

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_Results
  Inherits Formbase

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.AchievementCreateButton = New System.Windows.Forms.Button()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.BtnOutExcel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(355, 9)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(152, 45)
        Me.TitleLabel.TabIndex = 22
        Me.TitleLabel.Text = "伝票入力"
        Me.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'AchievementCreateButton
        '
        Me.AchievementCreateButton.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AchievementCreateButton.Location = New System.Drawing.Point(20, 69)
        Me.AchievementCreateButton.Name = "AchievementCreateButton"
        Me.AchievementCreateButton.Size = New System.Drawing.Size(407, 79)
        Me.AchievementCreateButton.TabIndex = 15
        Me.AchievementCreateButton.Text = "伝票入力"
        Me.AchievementCreateButton.UseVisualStyleBackColor = True
        '
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CloseButton.Location = New System.Drawing.Point(717, 373)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 43)
        Me.CloseButton.TabIndex = 23
        Me.CloseButton.Text = "終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'BtnOutExcel
        '
        Me.BtnOutExcel.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOutExcel.Location = New System.Drawing.Point(433, 69)
        Me.BtnOutExcel.Name = "BtnOutExcel"
        Me.BtnOutExcel.Size = New System.Drawing.Size(407, 79)
        Me.BtnOutExcel.TabIndex = 24
    Me.BtnOutExcel.Text = "CSV出力"
        Me.BtnOutExcel.UseVisualStyleBackColor = True
        '
        'Form_Results
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(850, 428)
        Me.Controls.Add(Me.BtnOutExcel)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.TitleLabel)
        Me.Controls.Add(Me.AchievementCreateButton)
        Me.Name = "Form_Results"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "伝票入力"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TitleLabel As Label
    Friend WithEvents AchievementCreateButton As Button
    Friend WithEvents CloseButton As Button
    Friend WithEvents BtnOutExcel As Button
End Class
