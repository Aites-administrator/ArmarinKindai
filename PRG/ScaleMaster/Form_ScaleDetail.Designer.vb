Imports T.R.ZCommonCtrl

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_ScaleDetail
  Inherits FormBase

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
    Me.TxtFolderName = New System.Windows.Forms.TextBox()
    Me.UnitNumberText = New System.Windows.Forms.TextBox()
    Me.NameLabel = New System.Windows.Forms.Label()
    Me.CodeLabel = New System.Windows.Forms.Label()
    Me.OkButton = New System.Windows.Forms.Button()
    Me.TitleLabel = New System.Windows.Forms.Label()
    Me.MemoText = New System.Windows.Forms.TextBox()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.CloseButton = New System.Windows.Forms.Button()
        Me.LblMessage = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TxtFolderName
        '
        Me.TxtFolderName.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFolderName.Location = New System.Drawing.Point(180, 113)
        Me.TxtFolderName.MaxLength = 10
        Me.TxtFolderName.Name = "TxtFolderName"
        Me.TxtFolderName.Size = New System.Drawing.Size(181, 33)
        Me.TxtFolderName.TabIndex = 2
        '
        'UnitNumberText
        '
        Me.UnitNumberText.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UnitNumberText.Location = New System.Drawing.Point(180, 74)
        Me.UnitNumberText.MaxLength = 2
        Me.UnitNumberText.Name = "UnitNumberText"
        Me.UnitNumberText.Size = New System.Drawing.Size(181, 33)
        Me.UnitNumberText.TabIndex = 1
        '
        'NameLabel
        '
        Me.NameLabel.AutoSize = True
        Me.NameLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NameLabel.Location = New System.Drawing.Point(54, 113)
        Me.NameLabel.Name = "NameLabel"
        Me.NameLabel.Size = New System.Drawing.Size(121, 30)
        Me.NameLabel.TabIndex = 3
        Me.NameLabel.Text = "フォルダ名："
        '
        'CodeLabel
        '
        Me.CodeLabel.AutoSize = True
        Me.CodeLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CodeLabel.Location = New System.Drawing.Point(54, 77)
        Me.CodeLabel.Name = "CodeLabel"
        Me.CodeLabel.Size = New System.Drawing.Size(123, 30)
        Me.CodeLabel.TabIndex = 1
        Me.CodeLabel.Text = "号機番号："
        '
        'OkButton
        '
        Me.OkButton.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OkButton.Location = New System.Drawing.Point(361, 238)
        Me.OkButton.Name = "OkButton"
        Me.OkButton.Size = New System.Drawing.Size(123, 43)
        Me.OkButton.TabIndex = 4
        Me.OkButton.Text = "F1:登録"
        Me.OkButton.UseVisualStyleBackColor = True
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(12, 9)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(172, 30)
        Me.TitleLabel.TabIndex = 0
        Me.TitleLabel.Text = "計量器マスタ詳細"
        '
        'MemoText
        '
        Me.MemoText.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MemoText.Location = New System.Drawing.Point(180, 152)
        Me.MemoText.Multiline = True
        Me.MemoText.Name = "MemoText"
        Me.MemoText.Size = New System.Drawing.Size(409, 71)
        Me.MemoText.TabIndex = 3
        Me.MemoText.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(75, 151)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(100, 30)
        Me.Label12.TabIndex = 40
        Me.Label12.Text = "用　途 ："
        Me.Label12.Visible = False
        '
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CloseButton.Location = New System.Drawing.Point(490, 238)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 43)
        Me.CloseButton.TabIndex = 5
        Me.CloseButton.Text = "F10:終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'LblMessage
        '
        Me.LblMessage.AutoSize = True
        Me.LblMessage.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMessage.Location = New System.Drawing.Point(181, 158)
        Me.LblMessage.Name = "LblMessage"
        Me.LblMessage.Size = New System.Drawing.Size(0, 21)
        Me.LblMessage.TabIndex = 41
        '
        'Form_ScaleDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(625, 293)
        Me.Controls.Add(Me.LblMessage)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.MemoText)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.TxtFolderName)
        Me.Controls.Add(Me.UnitNumberText)
        Me.Controls.Add(Me.NameLabel)
        Me.Controls.Add(Me.CodeLabel)
        Me.Controls.Add(Me.OkButton)
        Me.Controls.Add(Me.TitleLabel)
        Me.DoubleBuffered = True
        Me.KeyPreview = True
        Me.Name = "Form_ScaleDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form_ScaleDetail"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TxtFolderName As TextBox
    Friend WithEvents UnitNumberText As TextBox
    Friend WithEvents NameLabel As Label
    Friend WithEvents CodeLabel As Label
    Friend WithEvents OkButton As Button
    Friend WithEvents TitleLabel As Label
    Friend WithEvents MemoText As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents CloseButton As Button
    Friend WithEvents LblMessage As Label
End Class
