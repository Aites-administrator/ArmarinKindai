Imports T.R.ZCommonCtrl
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class NohinPrint
  Inherits Formbase

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(NohinPrint))
        Me.TxtTanto = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.BtnPrint = New System.Windows.Forms.Button()
        Me.TxtGoukeiKin = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TxtMeisaiSu = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.TxtUriKbnName = New System.Windows.Forms.TextBox()
        Me.TxtDenkuName = New System.Windows.Forms.TextBox()
        Me.TxtTokuNameFrom = New System.Windows.Forms.TextBox()
        Me.TxtDenNo = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtSeikyuDay = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtNohinDay = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.BtnClose = New System.Windows.Forms.Button()
        Me.TxtKakouDayFrom = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.PeriodLabel = New System.Windows.Forms.Label()
        Me.BtnEdit = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RdoAll = New System.Windows.Forms.RadioButton()
        Me.RdoPrint = New System.Windows.Forms.RadioButton()
        Me.RdoNotPrint = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.RdoTokuiDesc = New System.Windows.Forms.RadioButton()
        Me.RdoTokuiAsc = New System.Windows.Forms.RadioButton()
        Me.RdoUketsukeDesc = New System.Windows.Forms.RadioButton()
        Me.RdoUketsukeAsc = New System.Windows.Forms.RadioButton()
        Me.BtnDelete = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.CmbMstCustomer1To = New T.R.ZCommonCtrl.CmbMstCustomer()
        Me.TxtTokuNameTo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.CmbMstTanto1 = New T.R.ZCommonCtrl.CmbMstTanto()
        Me.CmbMstCustomer1From = New T.R.ZCommonCtrl.CmbMstCustomer()
        Me.TxtKakouDayTo = New System.Windows.Forms.TextBox()
        Me.CmbMstDenku1 = New T.R.ZCommonCtrl.CmbMstDenku()
        Me.CmbMstUriKbn1 = New T.R.ZCommonCtrl.CmbMstUriKbn()
        Me.TitleLabel = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'TxtTanto
        '
        Me.TxtTanto.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTanto.Location = New System.Drawing.Point(876, 33)
        Me.TxtTanto.MaxLength = 10
        Me.TxtTanto.Name = "TxtTanto"
        Me.TxtTanto.ReadOnly = True
        Me.TxtTanto.Size = New System.Drawing.Size(104, 33)
        Me.TxtTanto.TabIndex = 4
        Me.TxtTanto.TabStop = False
        Me.TxtTanto.Visible = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.Location = New System.Drawing.Point(703, 34)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(101, 30)
        Me.Label14.TabIndex = 182
        Me.Label14.Text = "担当者："
        Me.Label14.Visible = False
        '
        'BtnPrint
        '
        Me.BtnPrint.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.BtnPrint.Location = New System.Drawing.Point(1161, 360)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(123, 43)
        Me.BtnPrint.TabIndex = 16
        Me.BtnPrint.Text = "F1:発行"
        Me.BtnPrint.UseVisualStyleBackColor = True
        '
        'TxtGoukeiKin
        '
        Me.TxtGoukeiKin.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtGoukeiKin.Location = New System.Drawing.Point(1196, 325)
        Me.TxtGoukeiKin.MaxLength = 10
        Me.TxtGoukeiKin.Name = "TxtGoukeiKin"
        Me.TxtGoukeiKin.ReadOnly = True
        Me.TxtGoukeiKin.Size = New System.Drawing.Size(86, 33)
        Me.TxtGoukeiKin.TabIndex = 14
        Me.TxtGoukeiKin.TabStop = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(1087, 324)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(123, 30)
        Me.Label12.TabIndex = 177
        Me.Label12.Text = "合計金額："
        '
        'TxtMeisaiSu
        '
        Me.TxtMeisaiSu.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtMeisaiSu.Location = New System.Drawing.Point(1024, 324)
        Me.TxtMeisaiSu.MaxLength = 10
        Me.TxtMeisaiSu.Name = "TxtMeisaiSu"
        Me.TxtMeisaiSu.ReadOnly = True
        Me.TxtMeisaiSu.Size = New System.Drawing.Size(57, 33)
        Me.TxtMeisaiSu.TabIndex = 13
        Me.TxtMeisaiSu.TabStop = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(930, 324)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(101, 30)
        Me.Label11.TabIndex = 175
        Me.Label11.Text = "明細数："
        '
        'TxtUriKbnName
        '
        Me.TxtUriKbnName.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtUriKbnName.Location = New System.Drawing.Point(931, 287)
        Me.TxtUriKbnName.MaxLength = 10
        Me.TxtUriKbnName.Name = "TxtUriKbnName"
        Me.TxtUriKbnName.ReadOnly = True
        Me.TxtUriKbnName.Size = New System.Drawing.Size(104, 33)
        Me.TxtUriKbnName.TabIndex = 13
        Me.TxtUriKbnName.TabStop = False
        Me.TxtUriKbnName.Visible = False
        '
        'TxtDenkuName
        '
        Me.TxtDenkuName.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtDenkuName.Location = New System.Drawing.Point(653, 284)
        Me.TxtDenkuName.MaxLength = 10
        Me.TxtDenkuName.Name = "TxtDenkuName"
        Me.TxtDenkuName.ReadOnly = True
        Me.TxtDenkuName.Size = New System.Drawing.Size(106, 33)
        Me.TxtDenkuName.TabIndex = 11
        Me.TxtDenkuName.TabStop = False
        Me.TxtDenkuName.Visible = False
        '
        'TxtTokuNameFrom
        '
        Me.TxtTokuNameFrom.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTokuNameFrom.Location = New System.Drawing.Point(299, 71)
        Me.TxtTokuNameFrom.MaxLength = 10
        Me.TxtTokuNameFrom.Name = "TxtTokuNameFrom"
        Me.TxtTokuNameFrom.ReadOnly = True
        Me.TxtTokuNameFrom.Size = New System.Drawing.Size(314, 33)
        Me.TxtTokuNameFrom.TabIndex = 7
        Me.TxtTokuNameFrom.TabStop = False
        '
        'TxtDenNo
        '
        Me.TxtDenNo.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtDenNo.Location = New System.Drawing.Point(550, 36)
        Me.TxtDenNo.MaxLength = 10
        Me.TxtDenNo.Name = "TxtDenNo"
        Me.TxtDenNo.Size = New System.Drawing.Size(132, 33)
        Me.TxtDenNo.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(441, 36)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(123, 30)
        Me.Label9.TabIndex = 162
        Me.Label9.Text = "伝票番号："
        '
        'TxtSeikyuDay
        '
        Me.TxtSeikyuDay.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtSeikyuDay.Location = New System.Drawing.Point(392, 283)
        Me.TxtSeikyuDay.MaxLength = 10
        Me.TxtSeikyuDay.Name = "TxtSeikyuDay"
        Me.TxtSeikyuDay.Size = New System.Drawing.Size(135, 33)
        Me.TxtSeikyuDay.TabIndex = 10
        Me.TxtSeikyuDay.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(294, 283)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(101, 30)
        Me.Label8.TabIndex = 160
        Me.Label8.Text = "請求日："
        Me.Label8.Visible = False
        '
        'TxtNohinDay
        '
        Me.TxtNohinDay.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtNohinDay.Location = New System.Drawing.Point(156, 283)
        Me.TxtNohinDay.MaxLength = 10
        Me.TxtNohinDay.Name = "TxtNohinDay"
        Me.TxtNohinDay.Size = New System.Drawing.Size(132, 33)
        Me.TxtNohinDay.TabIndex = 9
        Me.TxtNohinDay.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(7, 283)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(145, 30)
        Me.Label7.TabIndex = 156
        Me.Label7.Text = "納品予定日："
        Me.Label7.Visible = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(772, 287)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(123, 30)
        Me.Label6.TabIndex = 155
        Me.Label6.Text = "売上区分："
        Me.Label6.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(533, 287)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 30)
        Me.Label2.TabIndex = 154
        Me.Label2.Text = "伝　区："
        Me.Label2.Visible = False
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(7, 360)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 21
        Me.DataGridView1.Size = New System.Drawing.Size(1148, 293)
        Me.DataGridView1.TabIndex = 15
        '
        'BtnClose
        '
        Me.BtnClose.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnClose.Location = New System.Drawing.Point(1161, 610)
        Me.BtnClose.Name = "BtnClose"
        Me.BtnClose.Size = New System.Drawing.Size(123, 43)
        Me.BtnClose.TabIndex = 19
        Me.BtnClose.Text = "F10:終了"
        Me.BtnClose.UseVisualStyleBackColor = True
        '
        'TxtKakouDayFrom
        '
        Me.TxtKakouDayFrom.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtKakouDayFrom.Location = New System.Drawing.Point(119, 35)
        Me.TxtKakouDayFrom.MaxLength = 10
        Me.TxtKakouDayFrom.Name = "TxtKakouDayFrom"
        Me.TxtKakouDayFrom.Size = New System.Drawing.Size(135, 33)
        Me.TxtKakouDayFrom.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(7, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(150, 30)
        Me.Label1.TabIndex = 148
        Me.Label1.Text = "得意先コード："
        '
        'PeriodLabel
        '
        Me.PeriodLabel.AutoSize = True
        Me.PeriodLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PeriodLabel.Location = New System.Drawing.Point(7, 36)
        Me.PeriodLabel.Name = "PeriodLabel"
        Me.PeriodLabel.Size = New System.Drawing.Size(101, 30)
        Me.PeriodLabel.TabIndex = 146
        Me.PeriodLabel.Text = "納品日："
        '
        'BtnEdit
        '
        Me.BtnEdit.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.BtnEdit.Location = New System.Drawing.Point(1161, 413)
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(123, 43)
        Me.BtnEdit.TabIndex = 17
        Me.BtnEdit.Text = "F2:修正"
        Me.BtnEdit.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RdoAll)
        Me.GroupBox1.Controls.Add(Me.RdoPrint)
        Me.GroupBox1.Controls.Add(Me.RdoNotPrint)
        Me.GroupBox1.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 112)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(294, 82)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "表示切替"
        '
        'RdoAll
        '
        Me.RdoAll.AutoSize = True
        Me.RdoAll.Location = New System.Drawing.Point(213, 38)
        Me.RdoAll.Name = "RdoAll"
        Me.RdoAll.Size = New System.Drawing.Size(70, 34)
        Me.RdoAll.TabIndex = 2
        Me.RdoAll.TabStop = True
        Me.RdoAll.Text = "全て"
        Me.RdoAll.UseVisualStyleBackColor = True
        '
        'RdoPrint
        '
        Me.RdoPrint.AutoSize = True
        Me.RdoPrint.Location = New System.Drawing.Point(110, 38)
        Me.RdoPrint.Name = "RdoPrint"
        Me.RdoPrint.Size = New System.Drawing.Size(97, 34)
        Me.RdoPrint.TabIndex = 1
        Me.RdoPrint.TabStop = True
        Me.RdoPrint.Text = "発行済"
        Me.RdoPrint.UseVisualStyleBackColor = True
        '
        'RdoNotPrint
        '
        Me.RdoNotPrint.AutoSize = True
        Me.RdoNotPrint.Location = New System.Drawing.Point(7, 38)
        Me.RdoNotPrint.Name = "RdoNotPrint"
        Me.RdoNotPrint.Size = New System.Drawing.Size(97, 34)
        Me.RdoNotPrint.TabIndex = 0
        Me.RdoNotPrint.TabStop = True
        Me.RdoNotPrint.Text = "未発行"
        Me.RdoNotPrint.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RdoTokuiDesc)
        Me.GroupBox2.Controls.Add(Me.RdoTokuiAsc)
        Me.GroupBox2.Controls.Add(Me.RdoUketsukeDesc)
        Me.GroupBox2.Controls.Add(Me.RdoUketsukeAsc)
        Me.GroupBox2.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.GroupBox2.Location = New System.Drawing.Point(330, 112)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(703, 82)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "伝票一覧並び替え順"
        '
        'RdoTokuiDesc
        '
        Me.RdoTokuiDesc.AutoSize = True
        Me.RdoTokuiDesc.Location = New System.Drawing.Point(354, 38)
        Me.RdoTokuiDesc.Name = "RdoTokuiDesc"
        Me.RdoTokuiDesc.Size = New System.Drawing.Size(153, 34)
        Me.RdoTokuiDesc.TabIndex = 3
        Me.RdoTokuiDesc.TabStop = True
        Me.RdoTokuiDesc.Text = "得意先(降順)"
        Me.RdoTokuiDesc.UseVisualStyleBackColor = True
        '
        'RdoTokuiAsc
        '
        Me.RdoTokuiAsc.AutoSize = True
        Me.RdoTokuiAsc.Location = New System.Drawing.Point(510, 38)
        Me.RdoTokuiAsc.Name = "RdoTokuiAsc"
        Me.RdoTokuiAsc.Size = New System.Drawing.Size(153, 34)
        Me.RdoTokuiAsc.TabIndex = 4
        Me.RdoTokuiAsc.TabStop = True
        Me.RdoTokuiAsc.Text = "得意先(昇順)"
        Me.RdoTokuiAsc.UseVisualStyleBackColor = True
        '
        'RdoUketsukeDesc
        '
        Me.RdoUketsukeDesc.AutoSize = True
        Me.RdoUketsukeDesc.Location = New System.Drawing.Point(6, 38)
        Me.RdoUketsukeDesc.Name = "RdoUketsukeDesc"
        Me.RdoUketsukeDesc.Size = New System.Drawing.Size(175, 34)
        Me.RdoUketsukeDesc.TabIndex = 1
        Me.RdoUketsukeDesc.TabStop = True
    Me.RdoUketsukeDesc.Text = "納品日順(降順)"
    Me.RdoUketsukeDesc.UseVisualStyleBackColor = True
        '
        'RdoUketsukeAsc
        '
        Me.RdoUketsukeAsc.AutoSize = True
        Me.RdoUketsukeAsc.Location = New System.Drawing.Point(183, 38)
        Me.RdoUketsukeAsc.Name = "RdoUketsukeAsc"
        Me.RdoUketsukeAsc.Size = New System.Drawing.Size(175, 34)
        Me.RdoUketsukeAsc.TabIndex = 2
        Me.RdoUketsukeAsc.TabStop = True
    Me.RdoUketsukeAsc.Text = "納品日順(昇順)"
    Me.RdoUketsukeAsc.UseVisualStyleBackColor = True
        '
        'BtnDelete
        '
        Me.BtnDelete.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnDelete.Location = New System.Drawing.Point(1161, 462)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(123, 43)
        Me.BtnDelete.TabIndex = 18
        Me.BtnDelete.Text = "F3:伝票削除"
        Me.BtnDelete.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.CmbMstCustomer1To)
        Me.GroupBox3.Controls.Add(Me.TxtTokuNameTo)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Controls.Add(Me.CmbMstTanto1)
        Me.GroupBox3.Controls.Add(Me.CmbMstCustomer1From)
        Me.GroupBox3.Controls.Add(Me.TxtTanto)
        Me.GroupBox3.Controls.Add(Me.GroupBox2)
        Me.GroupBox3.Controls.Add(Me.Label14)
        Me.GroupBox3.Controls.Add(Me.GroupBox1)
        Me.GroupBox3.Controls.Add(Me.TxtTokuNameFrom)
        Me.GroupBox3.Controls.Add(Me.TxtDenNo)
        Me.GroupBox3.Controls.Add(Me.Label9)
        Me.GroupBox3.Controls.Add(Me.TxtKakouDayTo)
        Me.GroupBox3.Controls.Add(Me.TxtKakouDayFrom)
        Me.GroupBox3.Controls.Add(Me.Label1)
        Me.GroupBox3.Controls.Add(Me.PeriodLabel)
        Me.GroupBox3.Font = New System.Drawing.Font("Segoe UI", 15.75!)
        Me.GroupBox3.Location = New System.Drawing.Point(2, 64)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(1126, 197)
        Me.GroupBox3.TabIndex = 198
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "検索条件"
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
        Me.CmbMstCustomer1To.Location = New System.Drawing.Point(660, 71)
        Me.CmbMstCustomer1To.Name = "CmbMstCustomer1To"
        Me.CmbMstCustomer1To.Size = New System.Drawing.Size(138, 34)
        Me.CmbMstCustomer1To.SkipChkCode = True
        Me.CmbMstCustomer1To.TabIndex = 6
        Me.CmbMstCustomer1To.ValueMember = "Code"
        '
        'TxtTokuNameTo
        '
        Me.TxtTokuNameTo.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTokuNameTo.Location = New System.Drawing.Point(804, 71)
        Me.TxtTokuNameTo.MaxLength = 10
        Me.TxtTokuNameTo.Name = "TxtTokuNameTo"
        Me.TxtTokuNameTo.ReadOnly = True
        Me.TxtTokuNameTo.Size = New System.Drawing.Size(314, 33)
        Me.TxtTokuNameTo.TabIndex = 186
        Me.TxtTokuNameTo.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(619, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 30)
        Me.Label4.TabIndex = 184
        Me.Label4.Text = "～"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(260, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 30)
        Me.Label3.TabIndex = 183
        Me.Label3.Text = "～"
        '
        'CmbMstTanto1
        '
        Me.CmbMstTanto1.AvailableBlank = False
        Me.CmbMstTanto1.BorderColor = System.Drawing.SystemColors.ControlText
        Me.CmbMstTanto1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None
        Me.CmbMstTanto1.BorderWidth = 1
        Me.CmbMstTanto1.CodeFormat = ""
        Me.CmbMstTanto1.DisplayMember = "Code"
        Me.CmbMstTanto1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbMstTanto1.DropDownWidth = 360
        Me.CmbMstTanto1.EventCancel = False
        Me.CmbMstTanto1.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.CmbMstTanto1.FormattingEnabled = True
        Me.CmbMstTanto1.Location = New System.Drawing.Point(802, 33)
        Me.CmbMstTanto1.Name = "CmbMstTanto1"
        Me.CmbMstTanto1.Size = New System.Drawing.Size(68, 34)
        Me.CmbMstTanto1.SkipChkCode = False
        Me.CmbMstTanto1.TabIndex = 4
        Me.CmbMstTanto1.ValueMember = "Code"
        Me.CmbMstTanto1.Visible = False
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
        Me.CmbMstCustomer1From.Location = New System.Drawing.Point(155, 71)
        Me.CmbMstCustomer1From.Name = "CmbMstCustomer1From"
        Me.CmbMstCustomer1From.Size = New System.Drawing.Size(138, 34)
        Me.CmbMstCustomer1From.SkipChkCode = True
        Me.CmbMstCustomer1From.TabIndex = 5
        Me.CmbMstCustomer1From.ValueMember = "Code"
        '
        'TxtKakouDayTo
        '
        Me.TxtKakouDayTo.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtKakouDayTo.Location = New System.Drawing.Point(300, 34)
        Me.TxtKakouDayTo.MaxLength = 10
        Me.TxtKakouDayTo.Name = "TxtKakouDayTo"
        Me.TxtKakouDayTo.Size = New System.Drawing.Size(135, 33)
        Me.TxtKakouDayTo.TabIndex = 2
        '
        'CmbMstDenku1
        '
        Me.CmbMstDenku1.AvailableBlank = False
        Me.CmbMstDenku1.BorderColor = System.Drawing.SystemColors.ControlText
        Me.CmbMstDenku1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None
        Me.CmbMstDenku1.BorderWidth = 1
        Me.CmbMstDenku1.CodeFormat = ""
        Me.CmbMstDenku1.DisplayMember = "Code"
        Me.CmbMstDenku1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbMstDenku1.DropDownWidth = 360
        Me.CmbMstDenku1.EventCancel = False
        Me.CmbMstDenku1.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.CmbMstDenku1.FormattingEnabled = True
        Me.CmbMstDenku1.Location = New System.Drawing.Point(612, 284)
        Me.CmbMstDenku1.Name = "CmbMstDenku1"
        Me.CmbMstDenku1.Size = New System.Drawing.Size(35, 34)
        Me.CmbMstDenku1.SkipChkCode = True
        Me.CmbMstDenku1.TabIndex = 11
        Me.CmbMstDenku1.ValueMember = "Code"
        Me.CmbMstDenku1.Visible = False
        '
        'CmbMstUriKbn1
        '
        Me.CmbMstUriKbn1.AvailableBlank = False
        Me.CmbMstUriKbn1.BorderColor = System.Drawing.SystemColors.ControlText
        Me.CmbMstUriKbn1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None
        Me.CmbMstUriKbn1.BorderWidth = 1
        Me.CmbMstUriKbn1.CodeFormat = ""
        Me.CmbMstUriKbn1.DisplayMember = "Code"
        Me.CmbMstUriKbn1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbMstUriKbn1.DropDownWidth = 360
        Me.CmbMstUriKbn1.EventCancel = False
        Me.CmbMstUriKbn1.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.CmbMstUriKbn1.FormattingEnabled = True
        Me.CmbMstUriKbn1.Location = New System.Drawing.Point(882, 287)
        Me.CmbMstUriKbn1.Name = "CmbMstUriKbn1"
        Me.CmbMstUriKbn1.Size = New System.Drawing.Size(43, 34)
        Me.CmbMstUriKbn1.SkipChkCode = True
        Me.CmbMstUriKbn1.TabIndex = 12
        Me.CmbMstUriKbn1.ValueMember = "Code"
        Me.CmbMstUriKbn1.Visible = False
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(7, 14)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(157, 37)
        Me.TitleLabel.TabIndex = 199
        Me.TitleLabel.Text = "納品書検索"
        '
        'NohinPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1289, 665)
        Me.Controls.Add(Me.TitleLabel)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.CmbMstDenku1)
        Me.Controls.Add(Me.CmbMstUriKbn1)
        Me.Controls.Add(Me.BtnPrint)
        Me.Controls.Add(Me.TxtGoukeiKin)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.TxtMeisaiSu)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TxtUriKbnName)
        Me.Controls.Add(Me.TxtDenkuName)
        Me.Controls.Add(Me.TxtSeikyuDay)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtNohinDay)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.BtnClose)
        Me.Controls.Add(Me.BtnDelete)
        Me.Controls.Add(Me.BtnEdit)
        Me.DoubleBuffered = True
        Me.KeyPreview = True
        Me.Name = "NohinPrint"
        Me.Text = "納品書検索画面"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TxtTanto As TextBox
    Friend WithEvents Label14 As Label
    Friend WithEvents BtnPrint As Button
    Friend WithEvents TxtGoukeiKin As TextBox
    Friend WithEvents Label12 As Label
    Friend WithEvents TxtMeisaiSu As TextBox
    Friend WithEvents Label11 As Label
    Friend WithEvents TxtUriKbnName As TextBox
    Friend WithEvents TxtDenkuName As TextBox
    Friend WithEvents TxtTokuNameFrom As TextBox
    Friend WithEvents TxtDenNo As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents TxtSeikyuDay As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents TxtNohinDay As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents BtnClose As Button
    Friend WithEvents TxtKakouDayFrom As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents PeriodLabel As Label
    Friend WithEvents BtnEdit As Button
    Friend WithEvents CmbMstCustomer1From As T.R.ZCommonCtrl.CmbMstCustomer
    Friend WithEvents CmbMstTanto1 As T.R.ZCommonCtrl.CmbMstTanto
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RdoAll As RadioButton
    Friend WithEvents RdoPrint As RadioButton
    Friend WithEvents RdoNotPrint As RadioButton
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents RdoTokuiDesc As RadioButton
    Friend WithEvents RdoTokuiAsc As RadioButton
    Friend WithEvents RdoUketsukeDesc As RadioButton
    Friend WithEvents RdoUketsukeAsc As RadioButton
    Friend WithEvents BtnDelete As Button
    Friend WithEvents CmbMstUriKbn1 As T.R.ZCommonCtrl.CmbMstUriKbn
    Friend WithEvents CmbMstDenku1 As T.R.ZCommonCtrl.CmbMstDenku
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtKakouDayTo As TextBox
    Friend WithEvents CmbMstCustomer1To As CmbMstCustomer
    Friend WithEvents TxtTokuNameTo As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TitleLabel As Label
End Class
