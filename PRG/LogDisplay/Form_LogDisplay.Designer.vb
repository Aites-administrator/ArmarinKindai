<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_LogDisplay
    Inherits System.Windows.Forms.Form

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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.DateLabel = New System.Windows.Forms.Label()
        Me.ScaleLabel = New System.Windows.Forms.Label()
        Me.LogDetail = New System.Windows.Forms.DataGridView()
        Me.Scale_ComboBox = New System.Windows.Forms.ComboBox()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.DateTime = New System.Windows.Forms.TextBox()
        CType(Me.LogDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(12, 9)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(156, 30)
        Me.TitleLabel.TabIndex = 0
        Me.TitleLabel.Text = "送受信ログ一覧"
        '
        'DateLabel
        '
        Me.DateLabel.AutoSize = True
        Me.DateLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DateLabel.Location = New System.Drawing.Point(12, 66)
        Me.DateLabel.Name = "DateLabel"
        Me.DateLabel.Size = New System.Drawing.Size(72, 30)
        Me.DateLabel.TabIndex = 1
        Me.DateLabel.Text = "日　付"
        '
        'ScaleLabel
        '
        Me.ScaleLabel.AutoSize = True
        Me.ScaleLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ScaleLabel.Location = New System.Drawing.Point(261, 66)
        Me.ScaleLabel.Name = "ScaleLabel"
        Me.ScaleLabel.Size = New System.Drawing.Size(109, 30)
        Me.ScaleLabel.TabIndex = 3
        Me.ScaleLabel.Text = "計　量　器"
        '
        'LogDetail
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.LogDetail.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.LogDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.LogDetail.DefaultCellStyle = DataGridViewCellStyle2
        Me.LogDetail.Location = New System.Drawing.Point(12, 109)
        Me.LogDetail.Name = "LogDetail"
        Me.LogDetail.ReadOnly = True
        Me.LogDetail.RowTemplate.Height = 21
        Me.LogDetail.Size = New System.Drawing.Size(1060, 401)
        Me.LogDetail.TabIndex = 5
        Me.LogDetail.TabStop = False
        '
        'Scale_ComboBox
        '
        Me.Scale_ComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Scale_ComboBox.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Scale_ComboBox.FormattingEnabled = True
        Me.Scale_ComboBox.Location = New System.Drawing.Point(376, 66)
        Me.Scale_ComboBox.Name = "Scale_ComboBox"
        Me.Scale_ComboBox.Size = New System.Drawing.Size(45, 33)
        Me.Scale_ComboBox.TabIndex = 8
        '
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CloseButton.Location = New System.Drawing.Point(949, 522)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 43)
        Me.CloseButton.TabIndex = 9
        Me.CloseButton.Text = "F10:終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'DateTime
        '
        Me.DateTime.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.DateTime.Location = New System.Drawing.Point(90, 66)
        Me.DateTime.MaxLength = 10
        Me.DateTime.Name = "DateTime"
        Me.DateTime.Size = New System.Drawing.Size(153, 33)
        Me.DateTime.TabIndex = 10
        '
        'Form_LogDisplay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1084, 577)
        Me.Controls.Add(Me.DateTime)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.Scale_ComboBox)
        Me.Controls.Add(Me.LogDetail)
        Me.Controls.Add(Me.ScaleLabel)
        Me.Controls.Add(Me.DateLabel)
        Me.Controls.Add(Me.TitleLabel)
        Me.KeyPreview = True
        Me.Name = "Form_LogDisplay"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.LogDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TitleLabel As Label
    Friend WithEvents DateLabel As Label
    Friend WithEvents ScaleLabel As Label
    Friend WithEvents LogDetail As DataGridView
    Friend WithEvents Scale_ComboBox As ComboBox
    Friend WithEvents CloseButton As Button
    Friend WithEvents DateTime As TextBox
End Class
