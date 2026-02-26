<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_RealtimeConfirmation
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
        Me.ResultDetail = New System.Windows.Forms.DataGridView()
        Me.SendButton = New System.Windows.Forms.Button()
        Me.ReceiveButton = New System.Windows.Forms.Button()
        Me.CloseButton = New System.Windows.Forms.Button()
        CType(Me.ResultDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(12, 9)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(123, 30)
        Me.TitleLabel.TabIndex = 0
        Me.TitleLabel.Text = "計量器通信"
        '
        'ResultDetail
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ResultDetail.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.ResultDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ResultDetail.DefaultCellStyle = DataGridViewCellStyle2
        Me.ResultDetail.Location = New System.Drawing.Point(18, 51)
        Me.ResultDetail.Name = "ResultDetail"
        Me.ResultDetail.ReadOnly = True
        Me.ResultDetail.RowTemplate.Height = 21
        Me.ResultDetail.Size = New System.Drawing.Size(668, 276)
        Me.ResultDetail.TabIndex = 1
        Me.ResultDetail.TabStop = False
        '
        'SendButton
        '
        Me.SendButton.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.SendButton.Location = New System.Drawing.Point(695, 100)
        Me.SendButton.Name = "SendButton"
        Me.SendButton.Size = New System.Drawing.Size(123, 43)
        Me.SendButton.TabIndex = 3
        Me.SendButton.Text = "マスタ送信"
        Me.SendButton.UseVisualStyleBackColor = True
        Me.SendButton.Visible = False
        '
        'ReceiveButton
        '
        Me.ReceiveButton.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.ReceiveButton.Location = New System.Drawing.Point(695, 51)
        Me.ReceiveButton.Name = "ReceiveButton"
        Me.ReceiveButton.Size = New System.Drawing.Size(123, 43)
        Me.ReceiveButton.TabIndex = 2
        Me.ReceiveButton.Text = "F1:実績受信"
        Me.ReceiveButton.UseVisualStyleBackColor = True
        '
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CloseButton.Location = New System.Drawing.Point(695, 284)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 43)
        Me.CloseButton.TabIndex = 10
        Me.CloseButton.Text = "F10:終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'Form_RealtimeConfirmation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(830, 339)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.ReceiveButton)
        Me.Controls.Add(Me.ResultDetail)
        Me.Controls.Add(Me.SendButton)
        Me.Controls.Add(Me.TitleLabel)
        Me.KeyPreview = True
        Me.Name = "Form_RealtimeConfirmation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "計量器通信"
        CType(Me.ResultDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TitleLabel As Label
    Friend WithEvents ResultDetail As DataGridView
    Friend WithEvents SendButton As Button
    Friend WithEvents ReceiveButton As Button
    Friend WithEvents CloseButton As Button
End Class
