Imports T.R.ZCommonCtrl
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class OutExcel
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OutExcel))
    Me.TxtTokuNameFrom = New System.Windows.Forms.TextBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.TxtItemNameFrom = New System.Windows.Forms.TextBox()
    Me.RdoCross = New System.Windows.Forms.RadioButton()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.RdoA4 = New System.Windows.Forms.RadioButton()
    Me.DataGridView1 = New System.Windows.Forms.DataGridView()
    Me.BtnOutExcel = New System.Windows.Forms.Button()
    Me.TxtUKakouDayFrom = New System.Windows.Forms.TextBox()
    Me.PeriodLabel = New System.Windows.Forms.Label()
    Me.BtnClose = New System.Windows.Forms.Button()
    Me.CmbMstItem1From = New T.R.ZCommonCtrl.CmbMstItem()
    Me.CmbMstCustomer1From = New T.R.ZCommonCtrl.CmbMstCustomer()
        Me.TxtUKakouDayTo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CmbMstCustomer1To = New T.R.ZCommonCtrl.CmbMstCustomer()
        Me.TxtTokuNameTo = New System.Windows.Forms.TextBox()
        Me.CmbMstItem1To = New T.R.ZCommonCtrl.CmbMstItem()
        Me.TxtItemNameTo = New System.Windows.Forms.TextBox()
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtTokuNameFrom
        '
        Me.TxtTokuNameFrom.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTokuNameFrom.Location = New System.Drawing.Point(299, 97)
        Me.TxtTokuNameFrom.MaxLength = 10
        Me.TxtTokuNameFrom.Name = "TxtTokuNameFrom"
        Me.TxtTokuNameFrom.ReadOnly = True
        Me.TxtTokuNameFrom.Size = New System.Drawing.Size(314, 33)
        Me.TxtTokuNameFrom.TabIndex = 70
        Me.TxtTokuNameFrom.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 98)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(150, 30)
        Me.Label1.TabIndex = 69
        Me.Label1.Text = "得意先コード："
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(12, 148)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(128, 30)
        Me.Label2.TabIndex = 72
        Me.Label2.Text = "商品コード："
        '
        'TxtItemNameFrom
        '
        Me.TxtItemNameFrom.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtItemNameFrom.Location = New System.Drawing.Point(299, 148)
        Me.TxtItemNameFrom.MaxLength = 10
        Me.TxtItemNameFrom.Name = "TxtItemNameFrom"
        Me.TxtItemNameFrom.ReadOnly = True
        Me.TxtItemNameFrom.Size = New System.Drawing.Size(314, 33)
        Me.TxtItemNameFrom.TabIndex = 73
        Me.TxtItemNameFrom.TabStop = False
        '
        'RdoCross
        '
        Me.RdoCross.AutoSize = True
        Me.RdoCross.Checked = True
        Me.RdoCross.Location = New System.Drawing.Point(7, 39)
        Me.RdoCross.Name = "RdoCross"
        Me.RdoCross.Size = New System.Drawing.Size(112, 34)
        Me.RdoCross.TabIndex = 0
        Me.RdoCross.TabStop = True
        Me.RdoCross.Text = "CSV出力"
        Me.RdoCross.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RdoA4)
        Me.GroupBox1.Controls.Add(Me.RdoCross)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.GroupBox1.Location = New System.Drawing.Point(14, 182)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(596, 82)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "出力形式"
        '
        'RdoA4
        '
        Me.RdoA4.AutoSize = True
        Me.RdoA4.Checked = True
        Me.RdoA4.Location = New System.Drawing.Point(174, 42)
        Me.RdoA4.Name = "RdoA4"
        Me.RdoA4.Size = New System.Drawing.Size(144, 34)
        Me.RdoA4.TabIndex = 1
        Me.RdoA4.TabStop = True
        Me.RdoA4.Text = "A4帳票印刷"
        Me.RdoA4.UseVisualStyleBackColor = True
        Me.RdoA4.Visible = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(14, 269)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 21
        Me.DataGridView1.Size = New System.Drawing.Size(1321, 335)
        Me.DataGridView1.TabIndex = 9
        '
        'BtnOutExcel
        '
        Me.BtnOutExcel.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.BtnOutExcel.Location = New System.Drawing.Point(1212, 218)
        Me.BtnOutExcel.Name = "BtnOutExcel"
        Me.BtnOutExcel.Size = New System.Drawing.Size(123, 43)
        Me.BtnOutExcel.TabIndex = 8
        Me.BtnOutExcel.Text = "F1:出力"
        Me.BtnOutExcel.UseVisualStyleBackColor = True
        '
        'TxtUKakouDayFrom
        '
        Me.TxtUKakouDayFrom.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtUKakouDayFrom.Location = New System.Drawing.Point(155, 57)
        Me.TxtUKakouDayFrom.MaxLength = 10
        Me.TxtUKakouDayFrom.Name = "TxtUKakouDayFrom"
        Me.TxtUKakouDayFrom.Size = New System.Drawing.Size(132, 33)
        Me.TxtUKakouDayFrom.TabIndex = 1
        '
        'PeriodLabel
        '
        Me.PeriodLabel.AutoSize = True
        Me.PeriodLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PeriodLabel.Location = New System.Drawing.Point(16, 57)
        Me.PeriodLabel.Name = "PeriodLabel"
        Me.PeriodLabel.Size = New System.Drawing.Size(116, 30)
        Me.PeriodLabel.TabIndex = 78
    Me.PeriodLabel.Text = "　納品日："
    '
    'BtnClose
    '
    Me.BtnClose.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.BtnClose.Location = New System.Drawing.Point(1212, 611)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(123, 43)
        Me.BtnClose.TabIndex = 10
        Me.BtnClose.Text = "F10:終了"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'CmbMstItem1From
        '
        Me.CmbMstItem1From.AvailableBlank = False
        Me.CmbMstItem1From.BorderColor = System.Drawing.SystemColors.ControlText
        Me.CmbMstItem1From.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None
        Me.CmbMstItem1From.BorderWidth = 1
        Me.CmbMstItem1From.CodeFormat = ""
        Me.CmbMstItem1From.DisplayMember = "Code"
        Me.CmbMstItem1From.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbMstItem1From.DropDownWidth = 360
        Me.CmbMstItem1From.EventCancel = False
        Me.CmbMstItem1From.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.CmbMstItem1From.FormattingEnabled = True
        Me.CmbMstItem1From.Location = New System.Drawing.Point(155, 148)
        Me.CmbMstItem1From.Name = "CmbMstItem1From"
        Me.CmbMstItem1From.Size = New System.Drawing.Size(138, 34)
        Me.CmbMstItem1From.SkipChkCode = False
        Me.CmbMstItem1From.TabIndex = 5
        Me.CmbMstItem1From.ValueMember = "Code"
        '
        'CmbMstCustomer1From
        '
        Me.CmbMstCustomer1From.AvailableBlank = False
        Me.CmbMstCustomer1From.BorderColor = System.Drawing.SystemColors.ControlText
        Me.CmbMstCustomer1From.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None
        Me.CmbMstCustomer1From.BorderWidth = 1
        Me.CmbMstCustomer1From.CodeFormat = ""
        Me.CmbMstCustomer1From.DisplayMember = "Code"
        Me.CmbMstCustomer1From.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbMstCustomer1From.DropDownWidth = 360
        Me.CmbMstCustomer1From.EventCancel = False
        Me.CmbMstCustomer1From.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.CmbMstCustomer1From.FormattingEnabled = True
        Me.CmbMstCustomer1From.Location = New System.Drawing.Point(155, 97)
        Me.CmbMstCustomer1From.Name = "CmbMstCustomer1From"
        Me.CmbMstCustomer1From.Size = New System.Drawing.Size(138, 34)
        Me.CmbMstCustomer1From.SkipChkCode = False
        Me.CmbMstCustomer1From.TabIndex = 3
        Me.CmbMstCustomer1From.ValueMember = "Code"
        '
        'TxtUKakouDayTo
        '
        Me.TxtUKakouDayTo.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtUKakouDayTo.Location = New System.Drawing.Point(335, 58)
        Me.TxtUKakouDayTo.MaxLength = 10
        Me.TxtUKakouDayTo.Name = "TxtUKakouDayTo"
        Me.TxtUKakouDayTo.Size = New System.Drawing.Size(132, 33)
        Me.TxtUKakouDayTo.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(294, 57)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 30)
        Me.Label3.TabIndex = 80
        Me.Label3.Text = "～"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(619, 98)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 30)
        Me.Label4.TabIndex = 81
        Me.Label4.Text = "～"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(619, 151)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 30)
        Me.Label5.TabIndex = 81
        Me.Label5.Text = "～"
        '
        'CmbMstCustomer1To
        '
        Me.CmbMstCustomer1To.AvailableBlank = False
        Me.CmbMstCustomer1To.BorderColor = System.Drawing.SystemColors.ControlText
        Me.CmbMstCustomer1To.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None
        Me.CmbMstCustomer1To.BorderWidth = 1
        Me.CmbMstCustomer1To.CodeFormat = ""
        Me.CmbMstCustomer1To.DisplayMember = "Code"
        Me.CmbMstCustomer1To.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbMstCustomer1To.DropDownWidth = 360
        Me.CmbMstCustomer1To.EventCancel = False
        Me.CmbMstCustomer1To.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.CmbMstCustomer1To.FormattingEnabled = True
        Me.CmbMstCustomer1To.Location = New System.Drawing.Point(660, 98)
        Me.CmbMstCustomer1To.Name = "CmbMstCustomer1To"
        Me.CmbMstCustomer1To.Size = New System.Drawing.Size(138, 34)
        Me.CmbMstCustomer1To.SkipChkCode = False
        Me.CmbMstCustomer1To.TabIndex = 4
        Me.CmbMstCustomer1To.ValueMember = "Code"
        '
        'TxtTokuNameTo
        '
        Me.TxtTokuNameTo.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTokuNameTo.Location = New System.Drawing.Point(804, 98)
        Me.TxtTokuNameTo.MaxLength = 10
        Me.TxtTokuNameTo.Name = "TxtTokuNameTo"
        Me.TxtTokuNameTo.ReadOnly = True
        Me.TxtTokuNameTo.Size = New System.Drawing.Size(314, 33)
        Me.TxtTokuNameTo.TabIndex = 83
        Me.TxtTokuNameTo.TabStop = False
        '
        'CmbMstItem1To
        '
        Me.CmbMstItem1To.AvailableBlank = False
        Me.CmbMstItem1To.BorderColor = System.Drawing.SystemColors.ControlText
        Me.CmbMstItem1To.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None
        Me.CmbMstItem1To.BorderWidth = 1
        Me.CmbMstItem1To.CodeFormat = ""
        Me.CmbMstItem1To.DisplayMember = "Code"
        Me.CmbMstItem1To.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbMstItem1To.DropDownWidth = 360
        Me.CmbMstItem1To.EventCancel = False
        Me.CmbMstItem1To.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.CmbMstItem1To.FormattingEnabled = True
        Me.CmbMstItem1To.Location = New System.Drawing.Point(660, 149)
        Me.CmbMstItem1To.Name = "CmbMstItem1To"
        Me.CmbMstItem1To.Size = New System.Drawing.Size(138, 34)
        Me.CmbMstItem1To.SkipChkCode = False
        Me.CmbMstItem1To.TabIndex = 6
        Me.CmbMstItem1To.ValueMember = "Code"
        '
        'TxtItemNameTo
        '
        Me.TxtItemNameTo.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtItemNameTo.Location = New System.Drawing.Point(804, 149)
        Me.TxtItemNameTo.MaxLength = 10
        Me.TxtItemNameTo.Name = "TxtItemNameTo"
        Me.TxtItemNameTo.ReadOnly = True
        Me.TxtItemNameTo.Size = New System.Drawing.Size(314, 33)
        Me.TxtItemNameTo.TabIndex = 73
        Me.TxtItemNameTo.TabStop = False
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(10, 14)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(177, 37)
        Me.TitleLabel.TabIndex = 201
        Me.TitleLabel.Text = "CSV出力画面"
        '
        'OutExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1347, 666)
        Me.Controls.Add(Me.TitleLabel)
        Me.Controls.Add(Me.CmbMstCustomer1To)
        Me.Controls.Add(Me.TxtTokuNameTo)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtUKakouDayTo)
        Me.Controls.Add(Me.TxtUKakouDayFrom)
        Me.Controls.Add(Me.PeriodLabel)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.BtnOutExcel)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TxtItemNameTo)
        Me.Controls.Add(Me.TxtItemNameFrom)
        Me.Controls.Add(Me.CmbMstItem1To)
        Me.Controls.Add(Me.CmbMstItem1From)
        Me.Controls.Add(Me.CmbMstCustomer1From)
        Me.Controls.Add(Me.TxtTokuNameFrom)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.DoubleBuffered = True
        Me.KeyPreview = True
        Me.Name = "OutExcel"
        Me.Text = "CSV出力画面"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CmbMstCustomer1From As T.R.ZCommonCtrl.CmbMstCustomer
    Friend WithEvents TxtTokuNameFrom As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents CmbMstItem1From As T.R.ZCommonCtrl.CmbMstItem
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtItemNameFrom As TextBox
    Friend WithEvents RdoCross As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents BtnOutExcel As Button
    Friend WithEvents TxtUKakouDayFrom As TextBox
    Friend WithEvents PeriodLabel As Label
    Friend WithEvents BtnClose As Button
    Friend WithEvents RdoA4 As RadioButton
    Friend WithEvents TxtUKakouDayTo As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents CmbMstCustomer1To As CmbMstCustomer
    Friend WithEvents TxtTokuNameTo As TextBox
    Friend WithEvents CmbMstItem1To As CmbMstItem
    Friend WithEvents TxtItemNameTo As TextBox
    Friend WithEvents TitleLabel As Label
End Class
