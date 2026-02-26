<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_TokuisakiList
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
        Me.CreateButton = New System.Windows.Forms.Button()
        Me.UpdateButton = New System.Windows.Forms.Button()
        Me.DeleteButton = New System.Windows.Forms.Button()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.ManufacturerDetail = New System.Windows.Forms.DataGridView()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.DeletedDisplayCheckBox = New System.Windows.Forms.CheckBox()
        Me.RowCount = New System.Windows.Forms.Label()
        CType(Me.ManufacturerDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CreateButton
        '
        Me.CreateButton.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.CreateButton.Location = New System.Drawing.Point(838, 57)
        Me.CreateButton.Name = "CreateButton"
        Me.CreateButton.Size = New System.Drawing.Size(123, 43)
        Me.CreateButton.TabIndex = 0
        Me.CreateButton.Text = "新規"
        Me.CreateButton.UseVisualStyleBackColor = True
        '
        'UpdateButton
        '
        Me.UpdateButton.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.UpdateButton.Location = New System.Drawing.Point(838, 106)
        Me.UpdateButton.Name = "UpdateButton"
        Me.UpdateButton.Size = New System.Drawing.Size(123, 43)
        Me.UpdateButton.TabIndex = 4
        Me.UpdateButton.Text = "更新"
        Me.UpdateButton.UseVisualStyleBackColor = True
        '
        'DeleteButton
        '
        Me.DeleteButton.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.DeleteButton.Location = New System.Drawing.Point(838, 155)
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.Size = New System.Drawing.Size(123, 43)
        Me.DeleteButton.TabIndex = 5
        Me.DeleteButton.Text = "削除"
        Me.DeleteButton.UseVisualStyleBackColor = True
        '
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.CloseButton.Location = New System.Drawing.Point(838, 486)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 43)
        Me.CloseButton.TabIndex = 6
        Me.CloseButton.Text = "終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'ManufacturerDetail
        '
        Me.ManufacturerDetail.AllowUserToAddRows = False
        Me.ManufacturerDetail.AllowUserToDeleteRows = False
        Me.ManufacturerDetail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ManufacturerDetail.Location = New System.Drawing.Point(30, 57)
        Me.ManufacturerDetail.Name = "ManufacturerDetail"
        Me.ManufacturerDetail.ReadOnly = True
        Me.ManufacturerDetail.RowTemplate.Height = 21
        Me.ManufacturerDetail.Size = New System.Drawing.Size(802, 472)
        Me.ManufacturerDetail.TabIndex = 7
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(25, 9)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(172, 30)
        Me.TitleLabel.TabIndex = 8
        Me.TitleLabel.Text = "得意先マスタ一覧"
        '
        'DeletedDisplayCheckBox
        '
        Me.DeletedDisplayCheckBox.AutoSize = True
        Me.DeletedDisplayCheckBox.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.DeletedDisplayCheckBox.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DeletedDisplayCheckBox.Location = New System.Drawing.Point(627, 535)
        Me.DeletedDisplayCheckBox.Name = "DeletedDisplayCheckBox"
        Me.DeletedDisplayCheckBox.Size = New System.Drawing.Size(111, 29)
        Me.DeletedDisplayCheckBox.TabIndex = 9
        Me.DeletedDisplayCheckBox.TabStop = False
        Me.DeletedDisplayCheckBox.Text = "全件表示"
        Me.DeletedDisplayCheckBox.UseVisualStyleBackColor = True
        '
        'RowCount
        '
        Me.RowCount.AutoSize = True
        Me.RowCount.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RowCount.Location = New System.Drawing.Point(240, 539)
        Me.RowCount.Name = "RowCount"
        Me.RowCount.Size = New System.Drawing.Size(0, 25)
        Me.RowCount.TabIndex = 10
        '
        'Form_TokuisakiList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(973, 576)
        Me.Controls.Add(Me.ManufacturerDetail)
        Me.Controls.Add(Me.DeletedDisplayCheckBox)
        Me.Controls.Add(Me.RowCount)
        Me.Controls.Add(Me.TitleLabel)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.DeleteButton)
        Me.Controls.Add(Me.UpdateButton)
        Me.Controls.Add(Me.CreateButton)
        Me.Name = "Form_TokuisakiList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        CType(Me.ManufacturerDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CreateButton As Button
    Friend WithEvents UpdateButton As Button
    Friend WithEvents DeleteButton As Button
    Friend WithEvents CloseButton As Button
    Friend WithEvents ManufacturerDetail As DataGridView
    Friend WithEvents TitleLabel As Label
    Friend WithEvents DeletedDisplayCheckBox As CheckBox
    Friend WithEvents RowCount As Label
End Class
