Imports T.R.ZCommonCtrl
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_ResultList
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_ResultList))
    Me.CreateButton = New System.Windows.Forms.Button()
    Me.TxtNohinDay = New System.Windows.Forms.TextBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.ScaleLabel = New System.Windows.Forms.Label()
    Me.PeriodLabel = New System.Windows.Forms.Label()
    Me.CloseButton = New System.Windows.Forms.Button()
    Me.DataGridView1 = New System.Windows.Forms.DataGridView()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.TxtNohinDay2 = New System.Windows.Forms.TextBox()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.TxtSeikyuDay = New System.Windows.Forms.TextBox()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.TxtDenNo = New System.Windows.Forms.TextBox()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.TxtTokuName = New System.Windows.Forms.TextBox()
    Me.TxtTokuiTel = New System.Windows.Forms.TextBox()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.TxtTyokuTel = New System.Windows.Forms.TextBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.TxtChokuName = New System.Windows.Forms.TextBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.TxtMeisaiSu = New System.Windows.Forms.TextBox()
    Me.Label11 = New System.Windows.Forms.Label()
    Me.TxtGoukeiKin = New System.Windows.Forms.TextBox()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.TxtBaikaKei = New System.Windows.Forms.TextBox()
    Me.Label13 = New System.Windows.Forms.Label()
    Me.BtnGyoEdit = New System.Windows.Forms.Button()
    Me.TxtBunruiCd = New System.Windows.Forms.TextBox()
    Me.TxtDenpyoKbn = New System.Windows.Forms.TextBox()
    Me.TxtDenkuName = New System.Windows.Forms.TextBox()
    Me.TxtUriKbnName = New System.Windows.Forms.TextBox()
    Me.BtnAddGyo = New System.Windows.Forms.Button()
    Me.TxtTanto = New System.Windows.Forms.TextBox()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.TxtBumonCd = New System.Windows.Forms.TextBox()
    Me.Label15 = New System.Windows.Forms.Label()
    Me.TxtTekiyo = New System.Windows.Forms.TextBox()
    Me.Label16 = New System.Windows.Forms.Label()
    Me.TxtHoka1 = New System.Windows.Forms.TextBox()
    Me.Label17 = New System.Windows.Forms.Label()
    Me.TxtHoka2 = New System.Windows.Forms.TextBox()
    Me.Label18 = New System.Windows.Forms.Label()
    Me.CmbMstChoku1 = New T.R.ZCommonCtrl.CmbMstChoku()
    Me.CmbMstTanto1 = New T.R.ZCommonCtrl.CmbMstTanto()
    Me.CmbMstUriKbn1 = New T.R.ZCommonCtrl.CmbMstUriKbn()
    Me.CmbMstDenku1 = New T.R.ZCommonCtrl.CmbMstDenku()
    Me.CmbMstCustomer1 = New T.R.ZCommonCtrl.CmbMstCustomer()
    Me.TitleLabel = New System.Windows.Forms.Label()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CreateButton
        '
        Me.CreateButton.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.CreateButton.Location = New System.Drawing.Point(1154, 439)
        Me.CreateButton.Name = "CreateButton"
        Me.CreateButton.Size = New System.Drawing.Size(123, 43)
        Me.CreateButton.TabIndex = 28
        Me.CreateButton.Text = "F3:登録"
        Me.CreateButton.UseVisualStyleBackColor = True
        '
        'TxtNohinDay
        '
        Me.TxtNohinDay.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtNohinDay.Location = New System.Drawing.Point(159, 91)
        Me.TxtNohinDay.MaxLength = 10
        Me.TxtNohinDay.Name = "TxtNohinDay"
        Me.TxtNohinDay.Size = New System.Drawing.Size(121, 33)
        Me.TxtNohinDay.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 171)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(150, 30)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "得意先コード："
        '
        'ScaleLabel
        '
        Me.ScaleLabel.AutoSize = True
        Me.ScaleLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.ScaleLabel.Location = New System.Drawing.Point(17, 130)
        Me.ScaleLabel.Name = "ScaleLabel"
        Me.ScaleLabel.Size = New System.Drawing.Size(143, 30)
        Me.ScaleLabel.TabIndex = 38
        Me.ScaleLabel.Text = "　分類コード："
        Me.ScaleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ScaleLabel.Visible = False
        '
        'PeriodLabel
        '
        Me.PeriodLabel.AutoSize = True
        Me.PeriodLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.PeriodLabel.Location = New System.Drawing.Point(14, 91)
        Me.PeriodLabel.Name = "PeriodLabel"
        Me.PeriodLabel.Size = New System.Drawing.Size(146, 30)
        Me.PeriodLabel.TabIndex = 37
        Me.PeriodLabel.Text = "　　　納品日："
        Me.PeriodLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CloseButton.Location = New System.Drawing.Point(1154, 587)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 43)
        Me.CloseButton.TabIndex = 31
        Me.CloseButton.Text = "F10:終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(21, 337)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowTemplate.Height = 21
        Me.DataGridView1.Size = New System.Drawing.Size(1127, 293)
        Me.DataGridView1.TabIndex = 26
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(287, 91)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(94, 30)
        Me.Label2.TabIndex = 55
        Me.Label2.Text = "伝　区："
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label6.Location = New System.Drawing.Point(526, 91)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(123, 30)
        Me.Label6.TabIndex = 57
        Me.Label6.Text = "売上区分："
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TxtNohinDay2
        '
        Me.TxtNohinDay2.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtNohinDay2.Location = New System.Drawing.Point(1148, 132)
        Me.TxtNohinDay2.MaxLength = 10
        Me.TxtNohinDay2.Name = "TxtNohinDay2"
        Me.TxtNohinDay2.Size = New System.Drawing.Size(132, 33)
        Me.TxtNohinDay2.TabIndex = 7
        Me.TxtNohinDay2.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label7.Location = New System.Drawing.Point(1004, 132)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(145, 30)
        Me.Label7.TabIndex = 59
        Me.Label7.Text = "納品予定日："
        Me.Label7.Visible = False
        '
        'TxtSeikyuDay
        '
        Me.TxtSeikyuDay.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtSeikyuDay.Location = New System.Drawing.Point(1145, 93)
        Me.TxtSeikyuDay.MaxLength = 10
        Me.TxtSeikyuDay.Name = "TxtSeikyuDay"
        Me.TxtSeikyuDay.Size = New System.Drawing.Size(135, 33)
        Me.TxtSeikyuDay.TabIndex = 8
        Me.TxtSeikyuDay.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label8.Location = New System.Drawing.Point(1047, 93)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(101, 30)
        Me.Label8.TabIndex = 61
        Me.Label8.Text = "請求日："
        Me.Label8.Visible = False
        '
        'TxtDenNo
        '
        Me.TxtDenNo.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtDenNo.Location = New System.Drawing.Point(159, 51)
        Me.TxtDenNo.MaxLength = 10
        Me.TxtDenNo.Name = "TxtDenNo"
        Me.TxtDenNo.ReadOnly = True
        Me.TxtDenNo.Size = New System.Drawing.Size(121, 33)
        Me.TxtDenNo.TabIndex = 1
        Me.TxtDenNo.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label9.Location = New System.Drawing.Point(16, 51)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(144, 30)
        Me.Label9.TabIndex = 63
        Me.Label9.Text = " 　伝票番号："
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label10.Location = New System.Drawing.Point(286, 129)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(123, 30)
        Me.Label10.TabIndex = 65
        Me.Label10.Text = "伝票区分："
        Me.Label10.Visible = False
        '
        'TxtTokuName
        '
        Me.TxtTokuName.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTokuName.Location = New System.Drawing.Point(303, 171)
        Me.TxtTokuName.MaxLength = 10
        Me.TxtTokuName.Name = "TxtTokuName"
        Me.TxtTokuName.Size = New System.Drawing.Size(314, 33)
        Me.TxtTokuName.TabIndex = 15
        Me.TxtTokuName.TabStop = False
        '
        'TxtTokuiTel
        '
        Me.TxtTokuiTel.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTokuiTel.Location = New System.Drawing.Point(748, 171)
        Me.TxtTokuiTel.MaxLength = 13
        Me.TxtTokuiTel.Name = "TxtTokuiTel"
        Me.TxtTokuiTel.Size = New System.Drawing.Size(135, 33)
        Me.TxtTokuiTel.TabIndex = 16
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(623, 171)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(133, 30)
        Me.Label3.TabIndex = 68
        Me.Label3.Text = "得意先TEL："
        '
        'TxtTyokuTel
        '
        Me.TxtTyokuTel.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTyokuTel.Location = New System.Drawing.Point(748, 216)
        Me.TxtTyokuTel.MaxLength = 10
        Me.TxtTyokuTel.Name = "TxtTyokuTel"
        Me.TxtTyokuTel.Size = New System.Drawing.Size(135, 33)
        Me.TxtTyokuTel.TabIndex = 19
        Me.TxtTyokuTel.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label4.Location = New System.Drawing.Point(623, 216)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(133, 30)
        Me.Label4.TabIndex = 73
        Me.Label4.Text = "請求書TEL："
        Me.Label4.Visible = False
        '
        'TxtChokuName
        '
        Me.TxtChokuName.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtChokuName.Location = New System.Drawing.Point(303, 215)
        Me.TxtChokuName.MaxLength = 10
        Me.TxtChokuName.Name = "TxtChokuName"
        Me.TxtChokuName.Size = New System.Drawing.Size(314, 33)
        Me.TxtChokuName.TabIndex = 18
        Me.TxtChokuName.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label5.Location = New System.Drawing.Point(10, 216)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(150, 30)
        Me.Label5.TabIndex = 70
        Me.Label5.Text = "発送先コード："
        '
        'TxtMeisaiSu
        '
        Me.TxtMeisaiSu.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtMeisaiSu.Location = New System.Drawing.Point(1226, 183)
        Me.TxtMeisaiSu.MaxLength = 10
        Me.TxtMeisaiSu.Name = "TxtMeisaiSu"
        Me.TxtMeisaiSu.ReadOnly = True
        Me.TxtMeisaiSu.Size = New System.Drawing.Size(57, 33)
        Me.TxtMeisaiSu.TabIndex = 23
        Me.TxtMeisaiSu.TabStop = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label11.Location = New System.Drawing.Point(1132, 183)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(101, 30)
        Me.Label11.TabIndex = 75
        Me.Label11.Text = "明細数："
        '
        'TxtGoukeiKin
        '
        Me.TxtGoukeiKin.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtGoukeiKin.Location = New System.Drawing.Point(1226, 220)
        Me.TxtGoukeiKin.MaxLength = 10
        Me.TxtGoukeiKin.Name = "TxtGoukeiKin"
        Me.TxtGoukeiKin.ReadOnly = True
        Me.TxtGoukeiKin.Size = New System.Drawing.Size(57, 33)
        Me.TxtGoukeiKin.TabIndex = 25
        Me.TxtGoukeiKin.TabStop = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label12.Location = New System.Drawing.Point(1115, 219)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(123, 30)
        Me.Label12.TabIndex = 77
        Me.Label12.Text = "合計金額："
        '
        'TxtBaikaKei
        '
        Me.TxtBaikaKei.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtBaikaKei.Location = New System.Drawing.Point(1058, 220)
        Me.TxtBaikaKei.MaxLength = 10
        Me.TxtBaikaKei.Name = "TxtBaikaKei"
        Me.TxtBaikaKei.ReadOnly = True
        Me.TxtBaikaKei.Size = New System.Drawing.Size(57, 33)
        Me.TxtBaikaKei.TabIndex = 24
        Me.TxtBaikaKei.TabStop = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label13.Location = New System.Drawing.Point(947, 219)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(123, 30)
        Me.Label13.TabIndex = 79
        Me.Label13.Text = "売価合計："
        '
        'BtnGyoEdit
        '
        Me.BtnGyoEdit.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGyoEdit.Location = New System.Drawing.Point(1154, 390)
        Me.BtnGyoEdit.Name = "BtnGyoEdit"
        Me.BtnGyoEdit.Size = New System.Drawing.Size(123, 43)
        Me.BtnGyoEdit.TabIndex = 30
        Me.BtnGyoEdit.Text = "F2:明細修正"
        Me.BtnGyoEdit.UseVisualStyleBackColor = True
        '
        'TxtBunruiCd
        '
        Me.TxtBunruiCd.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtBunruiCd.Location = New System.Drawing.Point(159, 130)
        Me.TxtBunruiCd.MaxLength = 10
        Me.TxtBunruiCd.Name = "TxtBunruiCd"
        Me.TxtBunruiCd.Size = New System.Drawing.Size(121, 33)
        Me.TxtBunruiCd.TabIndex = 9
        Me.TxtBunruiCd.Visible = False
        '
        'TxtDenpyoKbn
        '
        Me.TxtDenpyoKbn.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtDenpyoKbn.Location = New System.Drawing.Point(418, 130)
        Me.TxtDenpyoKbn.MaxLength = 10
        Me.TxtDenpyoKbn.Name = "TxtDenpyoKbn"
        Me.TxtDenpyoKbn.Size = New System.Drawing.Size(105, 33)
        Me.TxtDenpyoKbn.TabIndex = 10
        Me.TxtDenpyoKbn.Visible = False
        '
        'TxtDenkuName
        '
        Me.TxtDenkuName.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtDenkuName.Location = New System.Drawing.Point(418, 91)
        Me.TxtDenkuName.MaxLength = 10
        Me.TxtDenkuName.Name = "TxtDenkuName"
        Me.TxtDenkuName.ReadOnly = True
        Me.TxtDenkuName.Size = New System.Drawing.Size(105, 33)
        Me.TxtDenkuName.TabIndex = 4
        Me.TxtDenkuName.TabStop = False
        '
        'TxtUriKbnName
        '
        Me.TxtUriKbnName.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtUriKbnName.Location = New System.Drawing.Point(686, 91)
        Me.TxtUriKbnName.MaxLength = 10
        Me.TxtUriKbnName.Name = "TxtUriKbnName"
        Me.TxtUriKbnName.ReadOnly = True
        Me.TxtUriKbnName.Size = New System.Drawing.Size(104, 33)
        Me.TxtUriKbnName.TabIndex = 6
        Me.TxtUriKbnName.TabStop = False
        '
        'BtnAddGyo
        '
        Me.BtnAddGyo.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.BtnAddGyo.Location = New System.Drawing.Point(1154, 337)
        Me.BtnAddGyo.Name = "BtnAddGyo"
        Me.BtnAddGyo.Size = New System.Drawing.Size(123, 43)
        Me.BtnAddGyo.TabIndex = 27
        Me.BtnAddGyo.Text = "F1:明細追加"
        Me.BtnAddGyo.UseVisualStyleBackColor = True
        '
        'TxtTanto
        '
        Me.TxtTanto.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTanto.Location = New System.Drawing.Point(686, 130)
        Me.TxtTanto.MaxLength = 10
        Me.TxtTanto.Name = "TxtTanto"
        Me.TxtTanto.Size = New System.Drawing.Size(104, 33)
        Me.TxtTanto.TabIndex = 12
        Me.TxtTanto.TabStop = False
        Me.TxtTanto.Visible = False
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label14.Location = New System.Drawing.Point(532, 129)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(116, 30)
        Me.Label14.TabIndex = 85
        Me.Label14.Text = "　担当者："
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label14.Visible = False
        '
        'TxtBumonCd
        '
        Me.TxtBumonCd.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtBumonCd.Location = New System.Drawing.Point(922, 130)
        Me.TxtBumonCd.MaxLength = 10
        Me.TxtBumonCd.Name = "TxtBumonCd"
        Me.TxtBumonCd.Size = New System.Drawing.Size(104, 33)
        Me.TxtBumonCd.TabIndex = 13
        Me.TxtBumonCd.Visible = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label15.Location = New System.Drawing.Point(800, 132)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(128, 30)
        Me.Label15.TabIndex = 88
        Me.Label15.Text = "部門コード："
        Me.Label15.Visible = False
        '
        'TxtTekiyo
        '
        Me.TxtTekiyo.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTekiyo.Location = New System.Drawing.Point(159, 256)
        Me.TxtTekiyo.MaxLength = 10
        Me.TxtTekiyo.Name = "TxtTekiyo"
        Me.TxtTekiyo.Size = New System.Drawing.Size(502, 33)
        Me.TxtTekiyo.TabIndex = 20
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label16.Location = New System.Drawing.Point(6, 256)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(154, 30)
        Me.Label16.TabIndex = 90
        Me.Label16.Text = "　　　　摘　要："
        '
        'TxtHoka1
        '
        Me.TxtHoka1.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtHoka1.Location = New System.Drawing.Point(160, 295)
        Me.TxtHoka1.MaxLength = 10
        Me.TxtHoka1.Name = "TxtHoka1"
        Me.TxtHoka1.Size = New System.Drawing.Size(502, 33)
        Me.TxtHoka1.TabIndex = 21
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label17.Location = New System.Drawing.Point(16, 295)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(143, 30)
        Me.Label17.TabIndex = 92
        Me.Label17.Text = "　　その他１："
        '
        'TxtHoka2
        '
        Me.TxtHoka2.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtHoka2.Location = New System.Drawing.Point(774, 295)
        Me.TxtHoka2.MaxLength = 10
        Me.TxtHoka2.Name = "TxtHoka2"
        Me.TxtHoka2.Size = New System.Drawing.Size(502, 33)
        Me.TxtHoka2.TabIndex = 22
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label18.Location = New System.Drawing.Point(668, 295)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(113, 30)
        Me.Label18.TabIndex = 94
        Me.Label18.Text = "その他２："
        '
        'CmbMstChoku1
        '
        Me.CmbMstChoku1.AvailableBlank = False
        Me.CmbMstChoku1.BorderColor = System.Drawing.SystemColors.ControlText
        Me.CmbMstChoku1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None
        Me.CmbMstChoku1.BorderWidth = 1
        Me.CmbMstChoku1.CodeFormat = ""
        Me.CmbMstChoku1.DisplayMember = "Code"
        Me.CmbMstChoku1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.CmbMstChoku1.DropDownWidth = 360
        Me.CmbMstChoku1.EventCancel = False
        Me.CmbMstChoku1.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.CmbMstChoku1.FormattingEnabled = True
        Me.CmbMstChoku1.Location = New System.Drawing.Point(159, 215)
        Me.CmbMstChoku1.Name = "CmbMstChoku1"
        Me.CmbMstChoku1.Size = New System.Drawing.Size(138, 34)
        Me.CmbMstChoku1.SkipChkCode = True
        Me.CmbMstChoku1.TabIndex = 17
        Me.CmbMstChoku1.ValueMember = "Code"
        '
        'CmbMstTanto1
        '
        Me.CmbMstTanto1.AvailableBlank = False
        Me.CmbMstTanto1.BorderColor = System.Drawing.SystemColors.ControlText
        Me.CmbMstTanto1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None
        Me.CmbMstTanto1.BorderWidth = 1
        Me.CmbMstTanto1.CodeFormat = ""
        Me.CmbMstTanto1.DisplayMember = "Code"
        Me.CmbMstTanto1.EventCancel = False
        Me.CmbMstTanto1.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.CmbMstTanto1.FormattingEnabled = True
        Me.CmbMstTanto1.Location = New System.Drawing.Point(639, 130)
        Me.CmbMstTanto1.Name = "CmbMstTanto1"
        Me.CmbMstTanto1.Size = New System.Drawing.Size(44, 33)
        Me.CmbMstTanto1.SkipChkCode = False
        Me.CmbMstTanto1.TabIndex = 11
        Me.CmbMstTanto1.ValueMember = "Code"
        Me.CmbMstTanto1.Visible = False
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
        Me.CmbMstUriKbn1.Location = New System.Drawing.Point(639, 91)
        Me.CmbMstUriKbn1.Name = "CmbMstUriKbn1"
        Me.CmbMstUriKbn1.Size = New System.Drawing.Size(40, 34)
        Me.CmbMstUriKbn1.SkipChkCode = True
        Me.CmbMstUriKbn1.TabIndex = 5
        Me.CmbMstUriKbn1.ValueMember = "Code"
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
        Me.CmbMstDenku1.Location = New System.Drawing.Point(371, 91)
        Me.CmbMstDenku1.Name = "CmbMstDenku1"
        Me.CmbMstDenku1.Size = New System.Drawing.Size(41, 34)
        Me.CmbMstDenku1.SkipChkCode = True
        Me.CmbMstDenku1.TabIndex = 3
        Me.CmbMstDenku1.ValueMember = "Code"
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
        Me.CmbMstCustomer1.Location = New System.Drawing.Point(159, 171)
        Me.CmbMstCustomer1.Name = "CmbMstCustomer1"
        Me.CmbMstCustomer1.Size = New System.Drawing.Size(138, 34)
        Me.CmbMstCustomer1.SkipChkCode = False
        Me.CmbMstCustomer1.TabIndex = 14
        Me.CmbMstCustomer1.ValueMember = "Code"
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(10, 14)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(157, 37)
        Me.TitleLabel.TabIndex = 200
        Me.TitleLabel.Text = "納品書一覧"
        '
        'Form_ResultList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1286, 642)
        Me.Controls.Add(Me.TitleLabel)
        Me.Controls.Add(Me.CmbMstChoku1)
        Me.Controls.Add(Me.TxtHoka2)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.TxtHoka1)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.TxtTekiyo)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.TxtBumonCd)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.CmbMstTanto1)
        Me.Controls.Add(Me.TxtTanto)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.BtnAddGyo)
        Me.Controls.Add(Me.CmbMstUriKbn1)
        Me.Controls.Add(Me.CmbMstDenku1)
        Me.Controls.Add(Me.CmbMstCustomer1)
        Me.Controls.Add(Me.TxtBaikaKei)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.TxtGoukeiKin)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.TxtMeisaiSu)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.TxtTyokuTel)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtChokuName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtTokuiTel)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtUriKbnName)
        Me.Controls.Add(Me.TxtDenkuName)
        Me.Controls.Add(Me.TxtTokuName)
        Me.Controls.Add(Me.TxtDenNo)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TxtSeikyuDay)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtDenpyoKbn)
        Me.Controls.Add(Me.TxtBunruiCd)
        Me.Controls.Add(Me.TxtNohinDay2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.BtnGyoEdit)
        Me.Controls.Add(Me.TxtNohinDay)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ScaleLabel)
        Me.Controls.Add(Me.PeriodLabel)
        Me.Controls.Add(Me.CreateButton)
        Me.Controls.Add(Me.Label10)
        Me.DoubleBuffered = True
        Me.KeyPreview = True
        Me.Name = "Form_ResultList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form1"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CreateButton As Button
  Friend WithEvents TxtNohinDay As TextBox
  Friend WithEvents Label1 As Label
  Friend WithEvents ScaleLabel As Label
  Friend WithEvents PeriodLabel As Label
  Friend WithEvents CloseButton As Button
  Friend WithEvents DataGridView1 As DataGridView
  Friend WithEvents Label2 As Label
  Friend WithEvents Label6 As Label
  Friend WithEvents TxtNohinDay2 As TextBox
  Friend WithEvents Label7 As Label
  Friend WithEvents TxtSeikyuDay As TextBox
  Friend WithEvents Label8 As Label
  Friend WithEvents TxtDenNo As TextBox
  Friend WithEvents Label9 As Label
  Friend WithEvents Label10 As Label
  Friend WithEvents TxtTokuName As TextBox
  Friend WithEvents TxtTokuiTel As TextBox
  Friend WithEvents Label3 As Label
  Friend WithEvents TxtTyokuTel As TextBox
  Friend WithEvents Label4 As Label
  Friend WithEvents TxtChokuName As TextBox
  Friend WithEvents Label5 As Label
  Friend WithEvents TxtMeisaiSu As TextBox
  Friend WithEvents Label11 As Label
  Friend WithEvents TxtGoukeiKin As TextBox
  Friend WithEvents Label12 As Label
  Friend WithEvents TxtBaikaKei As TextBox
  Friend WithEvents Label13 As Label
  Friend WithEvents BtnGyoEdit As Button
  Friend WithEvents CmbMstCustomer1 As T.R.ZCommonCtrl.CmbMstCustomer
  Friend WithEvents TxtBunruiCd As TextBox
  Friend WithEvents TxtDenpyoKbn As TextBox
  Friend WithEvents CmbMstDenku1 As T.R.ZCommonCtrl.CmbMstDenku
  Friend WithEvents CmbMstUriKbn1 As T.R.ZCommonCtrl.CmbMstUriKbn
  Friend WithEvents TxtDenkuName As TextBox
  Friend WithEvents TxtUriKbnName As TextBox
  Friend WithEvents BtnAddGyo As Button
  Friend WithEvents TxtTanto As TextBox
  Friend WithEvents Label14 As Label
  Friend WithEvents CmbMstTanto1 As T.R.ZCommonCtrl.CmbMstTanto
  Friend WithEvents TxtBumonCd As TextBox
  Friend WithEvents Label15 As Label
  Friend WithEvents TxtTekiyo As TextBox
  Friend WithEvents Label16 As Label
  Friend WithEvents TxtHoka1 As TextBox
  Friend WithEvents Label17 As Label
  Friend WithEvents TxtHoka2 As TextBox
  Friend WithEvents Label18 As Label
  Friend WithEvents CmbMstChoku1 As T.R.ZCommonCtrl.CmbMstChoku
    Friend WithEvents TitleLabel As Label
End Class
