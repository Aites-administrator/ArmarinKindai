<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_MasterMente
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
    Me.PackingMasterButton = New System.Windows.Forms.Button()
    Me.CorporateMasterButton = New System.Windows.Forms.Button()
    Me.PrintSettingMasterButton = New System.Windows.Forms.Button()
    Me.GarbageCategoryMasterButton = New System.Windows.Forms.Button()
    Me.GarbageTypeMasterButton = New System.Windows.Forms.Button()
    Me.TitleLabel = New System.Windows.Forms.Label()
    Me.GarbageCategory2MasterButton = New System.Windows.Forms.Button()
    Me.CloseButton = New System.Windows.Forms.Button()
    Me.BtnMasterLinkage = New System.Windows.Forms.Button()
    Me.BtnTantousha = New System.Windows.Forms.Button()
    Me.LblMessage = New System.Windows.Forms.Label()
    Me.AreaMasterButton = New System.Windows.Forms.Button()
    Me.ScaleMasterButton = New System.Windows.Forms.Button()
    Me.SuspendLayout()
    '
    'PackingMasterButton
    '
    Me.PackingMasterButton.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.PackingMasterButton.Location = New System.Drawing.Point(13, 412)
    Me.PackingMasterButton.Name = "PackingMasterButton"
    Me.PackingMasterButton.Size = New System.Drawing.Size(407, 79)
    Me.PackingMasterButton.TabIndex = 2
    Me.PackingMasterButton.Text = "F5:計量機管理"
    Me.PackingMasterButton.UseVisualStyleBackColor = True
    '
    'CorporateMasterButton
    '
    Me.CorporateMasterButton.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.CorporateMasterButton.Location = New System.Drawing.Point(13, 242)
    Me.CorporateMasterButton.Name = "CorporateMasterButton"
    Me.CorporateMasterButton.Size = New System.Drawing.Size(407, 79)
    Me.CorporateMasterButton.TabIndex = 11
    Me.CorporateMasterButton.Text = "直送先"
    Me.CorporateMasterButton.UseVisualStyleBackColor = True
    Me.CorporateMasterButton.Visible = False
    '
    'PrintSettingMasterButton
    '
    Me.PrintSettingMasterButton.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.PrintSettingMasterButton.Location = New System.Drawing.Point(425, 72)
    Me.PrintSettingMasterButton.Name = "PrintSettingMasterButton"
    Me.PrintSettingMasterButton.Size = New System.Drawing.Size(407, 79)
    Me.PrintSettingMasterButton.TabIndex = 9
    Me.PrintSettingMasterButton.Text = "F2:即時発行設定"
    Me.PrintSettingMasterButton.UseVisualStyleBackColor = True
    '
    'GarbageCategoryMasterButton
    '
    Me.GarbageCategoryMasterButton.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.GarbageCategoryMasterButton.Location = New System.Drawing.Point(13, 72)
    Me.GarbageCategoryMasterButton.Name = "GarbageCategoryMasterButton"
    Me.GarbageCategoryMasterButton.Size = New System.Drawing.Size(407, 79)
    Me.GarbageCategoryMasterButton.TabIndex = 1
    Me.GarbageCategoryMasterButton.Text = "F1:得意先商品"
    Me.GarbageCategoryMasterButton.UseVisualStyleBackColor = True
    '
    'GarbageTypeMasterButton
    '
    Me.GarbageTypeMasterButton.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.GarbageTypeMasterButton.Location = New System.Drawing.Point(12, 157)
    Me.GarbageTypeMasterButton.Name = "GarbageTypeMasterButton"
    Me.GarbageTypeMasterButton.Size = New System.Drawing.Size(407, 79)
    Me.GarbageTypeMasterButton.TabIndex = 7
    Me.GarbageTypeMasterButton.Text = "商品"
    Me.GarbageTypeMasterButton.UseVisualStyleBackColor = True
    Me.GarbageTypeMasterButton.Visible = False
    '
    'TitleLabel
    '
    Me.TitleLabel.AutoSize = True
    Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.TitleLabel.Location = New System.Drawing.Point(306, 9)
    Me.TitleLabel.Name = "TitleLabel"
    Me.TitleLabel.Size = New System.Drawing.Size(242, 45)
    Me.TitleLabel.TabIndex = 14
    Me.TitleLabel.Text = "マスタメンテナンス"
    Me.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
    '
    'GarbageCategory2MasterButton
    '
    Me.GarbageCategory2MasterButton.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.GarbageCategory2MasterButton.Location = New System.Drawing.Point(425, 242)
    Me.GarbageCategory2MasterButton.Name = "GarbageCategory2MasterButton"
    Me.GarbageCategory2MasterButton.Size = New System.Drawing.Size(407, 79)
    Me.GarbageCategory2MasterButton.TabIndex = 15
    Me.GarbageCategory2MasterButton.Text = "フリー２"
    Me.GarbageCategory2MasterButton.UseVisualStyleBackColor = True
    Me.GarbageCategory2MasterButton.Visible = False
    '
    'CloseButton
    '
    Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.CloseButton.Location = New System.Drawing.Point(709, 504)
    Me.CloseButton.Name = "CloseButton"
    Me.CloseButton.Size = New System.Drawing.Size(123, 43)
    Me.CloseButton.TabIndex = 4
    Me.CloseButton.Text = "F10:終了"
    Me.CloseButton.UseVisualStyleBackColor = True
    '
    'BtnMasterLinkage
    '
    Me.BtnMasterLinkage.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.BtnMasterLinkage.Location = New System.Drawing.Point(425, 412)
    Me.BtnMasterLinkage.Name = "BtnMasterLinkage"
    Me.BtnMasterLinkage.Size = New System.Drawing.Size(407, 79)
    Me.BtnMasterLinkage.TabIndex = 3
    Me.BtnMasterLinkage.Text = "F9:マスタ取込"
    Me.BtnMasterLinkage.UseVisualStyleBackColor = True
    '
    'BtnTantousha
    '
    Me.BtnTantousha.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.BtnTantousha.Location = New System.Drawing.Point(425, 157)
    Me.BtnTantousha.Name = "BtnTantousha"
    Me.BtnTantousha.Size = New System.Drawing.Size(407, 79)
    Me.BtnTantousha.TabIndex = 18
    Me.BtnTantousha.Text = "担当者"
    Me.BtnTantousha.UseVisualStyleBackColor = True
    Me.BtnTantousha.Visible = False
    '
    'LblMessage
    '
    Me.LblMessage.AutoSize = True
    Me.LblMessage.Font = New System.Drawing.Font("Segoe UI", 20.25!)
    Me.LblMessage.Location = New System.Drawing.Point(12, 510)
    Me.LblMessage.Name = "LblMessage"
    Me.LblMessage.Size = New System.Drawing.Size(96, 37)
    Me.LblMessage.TabIndex = 19
    Me.LblMessage.Text = "Label1"
    '
    'AreaMasterButton
    '
    Me.AreaMasterButton.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.AreaMasterButton.Location = New System.Drawing.Point(13, 327)
    Me.AreaMasterButton.Name = "AreaMasterButton"
    Me.AreaMasterButton.Size = New System.Drawing.Size(407, 79)
    Me.AreaMasterButton.TabIndex = 10
    Me.AreaMasterButton.Text = "フリー３"
    Me.AreaMasterButton.UseVisualStyleBackColor = True
    Me.AreaMasterButton.Visible = False
    '
    'ScaleMasterButton
    '
    Me.ScaleMasterButton.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
    Me.ScaleMasterButton.Location = New System.Drawing.Point(425, 327)
    Me.ScaleMasterButton.Name = "ScaleMasterButton"
    Me.ScaleMasterButton.Size = New System.Drawing.Size(407, 79)
    Me.ScaleMasterButton.TabIndex = 13
    Me.ScaleMasterButton.Text = "フリー４"
    Me.ScaleMasterButton.UseVisualStyleBackColor = True
    Me.ScaleMasterButton.Visible = False
    '
    'Form_MasterMente
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(845, 559)
    Me.Controls.Add(Me.LblMessage)
    Me.Controls.Add(Me.BtnTantousha)
    Me.Controls.Add(Me.BtnMasterLinkage)
    Me.Controls.Add(Me.CloseButton)
    Me.Controls.Add(Me.GarbageCategory2MasterButton)
    Me.Controls.Add(Me.TitleLabel)
    Me.Controls.Add(Me.ScaleMasterButton)
    Me.Controls.Add(Me.PackingMasterButton)
    Me.Controls.Add(Me.CorporateMasterButton)
    Me.Controls.Add(Me.AreaMasterButton)
    Me.Controls.Add(Me.PrintSettingMasterButton)
    Me.Controls.Add(Me.GarbageCategoryMasterButton)
    Me.Controls.Add(Me.GarbageTypeMasterButton)
    Me.KeyPreview = True
    Me.Name = "Form_MasterMente"
    Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
    Me.Text = "マスタメンテナンス"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub
  Friend WithEvents PackingMasterButton As Button
    Friend WithEvents CorporateMasterButton As Button
    Friend WithEvents PrintSettingMasterButton As Button
    Friend WithEvents GarbageCategoryMasterButton As Button
    Friend WithEvents GarbageTypeMasterButton As Button
    Friend WithEvents TitleLabel As Label
    Friend WithEvents GarbageCategory2MasterButton As Button
    Friend WithEvents CloseButton As Button
    Friend WithEvents BtnMasterLinkage As Button
    Friend WithEvents BtnTantousha As Button
    Friend WithEvents LblMessage As Label
    Friend WithEvents AreaMasterButton As Button
    Friend WithEvents ScaleMasterButton As Button
End Class
