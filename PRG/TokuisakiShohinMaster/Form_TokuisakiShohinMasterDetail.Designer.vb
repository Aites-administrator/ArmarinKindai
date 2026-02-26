Imports T.R.ZCommonCtrl
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_TokuisakiShohinMasterDetail
  Inherits FormBase

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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_TokuisakiShohinMasterDetail))
    Me.BtnCreate = New System.Windows.Forms.Button()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.TxtTokuisakiTanka = New System.Windows.Forms.TextBox()
    Me.TxtNouhinTanka = New System.Windows.Forms.TextBox()
    Me.TxtHyojunTanka = New System.Windows.Forms.TextBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.TxtItemName = New System.Windows.Forms.TextBox()
    Me.TxtTokuName = New System.Windows.Forms.TextBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.TxtTani = New System.Windows.Forms.TextBox()
        Me.CmbMstItem1 = New T.R.ZCommonCtrl.CmbMstItem()
        Me.CmbMstCustomer1 = New T.R.ZCommonCtrl.CmbMstCustomer()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtIrisu = New System.Windows.Forms.TextBox()
        Me.TxtItemKana = New System.Windows.Forms.TextBox()
        Me.NameLabel = New System.Windows.Forms.Label()
        Me.CodeLabel = New System.Windows.Forms.Label()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnCreate
        '
        Me.BtnCreate.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCreate.Location = New System.Drawing.Point(440, 427)
        Me.BtnCreate.Name = "BtnCreate"
        Me.BtnCreate.Size = New System.Drawing.Size(123, 43)
        Me.BtnCreate.TabIndex = 11
        Me.BtnCreate.Text = "F1:登録"
        Me.BtnCreate.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 286)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 30)
        Me.Label6.TabIndex = 86
        Me.Label6.Text = "単価"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 247)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 30)
        Me.Label5.TabIndex = 85
        Me.Label5.Text = "卸単価"
        '
        'TxtTokuisakiTanka
        '
        Me.TxtTokuisakiTanka.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTokuisakiTanka.Location = New System.Drawing.Point(159, 288)
        Me.TxtTokuisakiTanka.MaxLength = 7
        Me.TxtTokuisakiTanka.Name = "TxtTokuisakiTanka"
        Me.TxtTokuisakiTanka.Size = New System.Drawing.Size(147, 33)
        Me.TxtTokuisakiTanka.TabIndex = 10
        '
        'TxtNouhinTanka
        '
        Me.TxtNouhinTanka.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtNouhinTanka.Location = New System.Drawing.Point(159, 249)
        Me.TxtNouhinTanka.MaxLength = 7
        Me.TxtNouhinTanka.Name = "TxtNouhinTanka"
        Me.TxtNouhinTanka.Size = New System.Drawing.Size(147, 33)
        Me.TxtNouhinTanka.TabIndex = 9
        '
        'TxtHyojunTanka
        '
        Me.TxtHyojunTanka.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtHyojunTanka.Location = New System.Drawing.Point(159, 210)
        Me.TxtHyojunTanka.MaxLength = 7
        Me.TxtHyojunTanka.Name = "TxtHyojunTanka"
        Me.TxtHyojunTanka.Size = New System.Drawing.Size(147, 33)
        Me.TxtHyojunTanka.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 209)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 30)
        Me.Label4.TabIndex = 83
        Me.Label4.Text = "定金額"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 170)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 30)
        Me.Label3.TabIndex = 82
        Me.Label3.Text = "入数"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TxtItemName)
        Me.GroupBox1.Controls.Add(Me.TxtTokuName)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TxtTani)
        Me.GroupBox1.Controls.Add(Me.CmbMstItem1)
        Me.GroupBox1.Controls.Add(Me.CmbMstCustomer1)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.TxtTokuisakiTanka)
        Me.GroupBox1.Controls.Add(Me.TxtNouhinTanka)
        Me.GroupBox1.Controls.Add(Me.TxtHyojunTanka)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TxtIrisu)
        Me.GroupBox1.Controls.Add(Me.TxtItemKana)
        Me.GroupBox1.Controls.Add(Me.NameLabel)
        Me.GroupBox1.Controls.Add(Me.CodeLabel)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 42)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(672, 379)
        Me.GroupBox1.TabIndex = 89
        Me.GroupBox1.TabStop = False
        '
        'TxtItemName
        '
        Me.TxtItemName.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtItemName.Location = New System.Drawing.Point(278, 58)
        Me.TxtItemName.MaxLength = 10
        Me.TxtItemName.Name = "TxtItemName"
        Me.TxtItemName.Size = New System.Drawing.Size(388, 33)
        Me.TxtItemName.TabIndex = 4
        '
        'TxtTokuName
        '
        Me.TxtTokuName.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTokuName.Location = New System.Drawing.Point(278, 15)
        Me.TxtTokuName.MaxLength = 10
        Me.TxtTokuName.Name = "TxtTokuName"
        Me.TxtTokuName.ReadOnly = True
        Me.TxtTokuName.Size = New System.Drawing.Size(388, 33)
        Me.TxtTokuName.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 135)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 30)
        Me.Label1.TabIndex = 90
        Me.Label1.Text = "単位"
        '
        'TxtTani
        '
        Me.TxtTani.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTani.Location = New System.Drawing.Point(159, 136)
        Me.TxtTani.MaxLength = 4
        Me.TxtTani.Name = "TxtTani"
        Me.TxtTani.Size = New System.Drawing.Size(147, 33)
        Me.TxtTani.TabIndex = 6
        '
        'CmbMstItem1
        '
        Me.CmbMstItem1.AvailableBlank = False
        Me.CmbMstItem1.BorderColor = System.Drawing.SystemColors.ControlText
        Me.CmbMstItem1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None
        Me.CmbMstItem1.BorderWidth = 1
        Me.CmbMstItem1.CodeFormat = ""
        Me.CmbMstItem1.DisplayMember = "Code"
        Me.CmbMstItem1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbMstItem1.DropDownWidth = 360
        Me.CmbMstItem1.EventCancel = False
        Me.CmbMstItem1.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.CmbMstItem1.FormattingEnabled = True
        Me.CmbMstItem1.Location = New System.Drawing.Point(159, 57)
        Me.CmbMstItem1.Name = "CmbMstItem1"
        Me.CmbMstItem1.Size = New System.Drawing.Size(113, 34)
        Me.CmbMstItem1.SkipChkCode = False
        Me.CmbMstItem1.TabIndex = 3
        Me.CmbMstItem1.ValueMember = "Code"
        '
        'CmbMstCustomer1
        '
        Me.CmbMstCustomer1.AvailableBlank = False
        Me.CmbMstCustomer1.BorderColor = System.Drawing.SystemColors.ControlText
        Me.CmbMstCustomer1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None
        Me.CmbMstCustomer1.BorderWidth = 1
        Me.CmbMstCustomer1.CodeFormat = ""
        Me.CmbMstCustomer1.DisplayMember = "Code"
        Me.CmbMstCustomer1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbMstCustomer1.DropDownWidth = 360
        Me.CmbMstCustomer1.EventCancel = False
        Me.CmbMstCustomer1.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.CmbMstCustomer1.FormattingEnabled = True
        Me.CmbMstCustomer1.Location = New System.Drawing.Point(159, 16)
        Me.CmbMstCustomer1.Name = "CmbMstCustomer1"
        Me.CmbMstCustomer1.Size = New System.Drawing.Size(113, 34)
        Me.CmbMstCustomer1.SkipChkCode = True
        Me.CmbMstCustomer1.TabIndex = 1
        Me.CmbMstCustomer1.ValueMember = "Code"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 30)
        Me.Label2.TabIndex = 81
        Me.Label2.Text = "カナ"
        '
        'TxtIrisu
        '
        Me.TxtIrisu.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtIrisu.Location = New System.Drawing.Point(159, 171)
        Me.TxtIrisu.MaxLength = 4
        Me.TxtIrisu.Name = "TxtIrisu"
        Me.TxtIrisu.Size = New System.Drawing.Size(147, 33)
        Me.TxtIrisu.TabIndex = 7
        '
        'TxtItemKana
        '
        Me.TxtItemKana.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtItemKana.ImeMode = System.Windows.Forms.ImeMode.KatakanaHalf
        Me.TxtItemKana.Location = New System.Drawing.Point(159, 97)
        Me.TxtItemKana.MaxLength = 36
        Me.TxtItemKana.Name = "TxtItemKana"
        Me.TxtItemKana.Size = New System.Drawing.Size(507, 33)
        Me.TxtItemKana.TabIndex = 5
        '
        'NameLabel
        '
        Me.NameLabel.AutoSize = True
        Me.NameLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NameLabel.Location = New System.Drawing.Point(6, 57)
        Me.NameLabel.Name = "NameLabel"
        Me.NameLabel.Size = New System.Drawing.Size(106, 30)
        Me.NameLabel.TabIndex = 79
        Me.NameLabel.Text = "商品コード"
        '
        'CodeLabel
        '
        Me.CodeLabel.AutoSize = True
        Me.CodeLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CodeLabel.Location = New System.Drawing.Point(6, 15)
        Me.CodeLabel.Name = "CodeLabel"
        Me.CodeLabel.Size = New System.Drawing.Size(128, 30)
        Me.CodeLabel.TabIndex = 77
        Me.CodeLabel.Text = "得意先コード"
        '
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CloseButton.Location = New System.Drawing.Point(569, 427)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 43)
        Me.CloseButton.TabIndex = 12
        Me.CloseButton.Text = "F10:終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(12, 9)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(216, 30)
        Me.TitleLabel.TabIndex = 86
        Me.TitleLabel.Text = "得意先商品マスタ詳細"
        '
        'Form_TokuisakiShohinMasterDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(703, 488)
        Me.Controls.Add(Me.BtnCreate)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.TitleLabel)
        Me.DoubleBuffered = True
        Me.KeyPreview = True
        Me.Name = "Form_TokuisakiShohinMasterDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "36"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnCreate As Button
  Friend WithEvents Label6 As Label
  Friend WithEvents Label5 As Label
  Friend WithEvents TxtTokuisakiTanka As TextBox
  Friend WithEvents TxtNouhinTanka As TextBox
  Friend WithEvents TxtHyojunTanka As TextBox
  Friend WithEvents Label4 As Label
  Friend WithEvents Label3 As Label
  Friend WithEvents GroupBox1 As GroupBox
  Friend WithEvents Label2 As Label
  Friend WithEvents TxtIrisu As TextBox
  Friend WithEvents TxtItemKana As TextBox
  Friend WithEvents NameLabel As Label
  Friend WithEvents CodeLabel As Label
  Friend WithEvents CloseButton As Button
  Friend WithEvents TitleLabel As Label
  Friend WithEvents CmbMstItem1 As T.R.ZCommonCtrl.CmbMstItem
  Friend WithEvents CmbMstCustomer1 As T.R.ZCommonCtrl.CmbMstCustomer
  Friend WithEvents Label1 As Label
  Friend WithEvents TxtTani As TextBox
  Friend WithEvents TxtItemName As TextBox
  Friend WithEvents TxtTokuName As TextBox
End Class
