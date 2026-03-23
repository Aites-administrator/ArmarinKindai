Imports T.R.ZCommonCtrl
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ItemAddForm
    Inherits formbase

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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ItemAddForm))
    Me.TxtGyoNo = New System.Windows.Forms.TextBox()
    Me.Label9 = New System.Windows.Forms.Label()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.TxtItemName = New System.Windows.Forms.TextBox()
    Me.TxtItemNameKana = New System.Windows.Forms.TextBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.TxtTeikanNohinSuryo = New System.Windows.Forms.TextBox()
    Me.TxtHuteikanTani = New System.Windows.Forms.TextBox()
    Me.Label4 = New System.Windows.Forms.Label()
    Me.TxtHuteikanNohinSuryo = New System.Windows.Forms.TextBox()
    Me.Label5 = New System.Windows.Forms.Label()
    Me.TxtHuteikanTanka = New System.Windows.Forms.TextBox()
    Me.Label6 = New System.Windows.Forms.Label()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.TxtHuteikanKingaku = New System.Windows.Forms.TextBox()
    Me.Label13 = New System.Windows.Forms.Label()
    Me.Label15 = New System.Windows.Forms.Label()
    Me.TxtKotai1 = New System.Windows.Forms.TextBox()
    Me.TxtKotai2 = New System.Windows.Forms.TextBox()
    Me.Label16 = New System.Windows.Forms.Label()
    Me.TxtKotai3 = New System.Windows.Forms.TextBox()
    Me.Label17 = New System.Windows.Forms.Label()
    Me.BtnDelGyo = New System.Windows.Forms.Button()
    Me.BtnAddGyo = New System.Windows.Forms.Button()
    Me.BtnClose = New System.Windows.Forms.Button()
    Me.TxtTeikanTani = New System.Windows.Forms.TextBox()
    Me.TxtKeiryoFlg = New System.Windows.Forms.TextBox()
    Me.Label18 = New System.Windows.Forms.Label()
    Me.CmbMstItem1 = New T.R.ZCommonCtrl.CmbMstItem()
    Me.CmbTeikan = New T.R.ZCommonCtrl.CmbBase()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.TxtTeikanKingaku = New System.Windows.Forms.TextBox()
    Me.Label7 = New System.Windows.Forms.Label()
    Me.TxtTeikanTanka = New System.Windows.Forms.TextBox()
    Me.Label8 = New System.Windows.Forms.Label()
    Me.Label10 = New System.Windows.Forms.Label()
    Me.Label11 = New System.Windows.Forms.Label()
    Me.Label14 = New System.Windows.Forms.Label()
    Me.TxtCustCd = New System.Windows.Forms.TextBox()
    Me.TitleLabel = New System.Windows.Forms.Label()
    Me.Label19 = New System.Windows.Forms.Label()
    Me.TxtHuteikanKosu = New System.Windows.Forms.TextBox()
    Me.SuspendLayout()
    '
    'TxtGyoNo
    '
    Me.TxtGyoNo.Enabled = False
    Me.TxtGyoNo.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.TxtGyoNo.Location = New System.Drawing.Point(121, 80)
    Me.TxtGyoNo.MaxLength = 10
    Me.TxtGyoNo.Name = "TxtGyoNo"
    Me.TxtGyoNo.Size = New System.Drawing.Size(97, 33)
    Me.TxtGyoNo.TabIndex = 1
    '
    'Label9
    '
    Me.Label9.AutoSize = True
    Me.Label9.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label9.Location = New System.Drawing.Point(12, 80)
    Me.Label9.Name = "Label9"
    Me.Label9.Size = New System.Drawing.Size(85, 30)
    Me.Label9.TabIndex = 65
    Me.Label9.Text = "行No："
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label1.Location = New System.Drawing.Point(12, 138)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(128, 30)
    Me.Label1.TabIndex = 68
    Me.Label1.Text = "商品コード："
    '
    'TxtItemName
    '
    Me.TxtItemName.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.TxtItemName.Location = New System.Drawing.Point(273, 135)
    Me.TxtItemName.MaxLength = 100
    Me.TxtItemName.Name = "TxtItemName"
    Me.TxtItemName.Size = New System.Drawing.Size(314, 33)
    Me.TxtItemName.TabIndex = 3
    Me.TxtItemName.TabStop = False
    '
    'TxtItemNameKana
    '
    Me.TxtItemNameKana.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.TxtItemNameKana.Location = New System.Drawing.Point(273, 174)
    Me.TxtItemNameKana.MaxLength = 10
    Me.TxtItemNameKana.Name = "TxtItemNameKana"
    Me.TxtItemNameKana.Size = New System.Drawing.Size(314, 33)
    Me.TxtItemNameKana.TabIndex = 4
    Me.TxtItemNameKana.TabStop = False
    Me.TxtItemNameKana.Visible = False
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label2.Location = New System.Drawing.Point(171, 315)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(79, 30)
    Me.Label2.TabIndex = 71
    Me.Label2.Text = "個数："
    Me.Label2.Visible = False
    '
    'TxtTeikanNohinSuryo
    '
    Me.TxtTeikanNohinSuryo.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.TxtTeikanNohinSuryo.Location = New System.Drawing.Point(256, 315)
    Me.TxtTeikanNohinSuryo.MaxLength = 10
    Me.TxtTeikanNohinSuryo.Name = "TxtTeikanNohinSuryo"
    Me.TxtTeikanNohinSuryo.Size = New System.Drawing.Size(64, 33)
    Me.TxtTeikanNohinSuryo.TabIndex = 11
    Me.TxtTeikanNohinSuryo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    Me.TxtTeikanNohinSuryo.Visible = False
    '
    'TxtHuteikanTani
    '
    Me.TxtHuteikanTani.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.TxtHuteikanTani.Location = New System.Drawing.Point(422, 263)
    Me.TxtHuteikanTani.MaxLength = 10
    Me.TxtHuteikanTani.Name = "TxtHuteikanTani"
    Me.TxtHuteikanTani.Size = New System.Drawing.Size(64, 33)
    Me.TxtHuteikanTani.TabIndex = 8
    '
    'Label4
    '
    Me.Label4.AutoSize = True
    Me.Label4.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label4.Location = New System.Drawing.Point(171, 263)
    Me.Label4.Name = "Label4"
    Me.Label4.Size = New System.Drawing.Size(79, 30)
    Me.Label4.TabIndex = 75
    Me.Label4.Text = "重量："
    '
    'TxtHuteikanNohinSuryo
    '
    Me.TxtHuteikanNohinSuryo.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.TxtHuteikanNohinSuryo.Location = New System.Drawing.Point(256, 263)
    Me.TxtHuteikanNohinSuryo.MaxLength = 10
    Me.TxtHuteikanNohinSuryo.Name = "TxtHuteikanNohinSuryo"
    Me.TxtHuteikanNohinSuryo.Size = New System.Drawing.Size(64, 33)
    Me.TxtHuteikanNohinSuryo.TabIndex = 7
    Me.TxtHuteikanNohinSuryo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label5
    '
    Me.Label5.AutoSize = True
    Me.Label5.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label5.Location = New System.Drawing.Point(510, 263)
    Me.Label5.Name = "Label5"
    Me.Label5.Size = New System.Drawing.Size(79, 30)
    Me.Label5.TabIndex = 71
    Me.Label5.Text = "単価："
    '
    'TxtHuteikanTanka
    '
    Me.TxtHuteikanTanka.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.TxtHuteikanTanka.Location = New System.Drawing.Point(595, 263)
    Me.TxtHuteikanTanka.MaxLength = 10
    Me.TxtHuteikanTanka.Name = "TxtHuteikanTanka"
    Me.TxtHuteikanTanka.Size = New System.Drawing.Size(64, 33)
    Me.TxtHuteikanTanka.TabIndex = 9
    Me.TxtHuteikanTanka.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label6
    '
    Me.Label6.AutoSize = True
    Me.Label6.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label6.Location = New System.Drawing.Point(665, 263)
    Me.Label6.Name = "Label6"
    Me.Label6.Size = New System.Drawing.Size(35, 30)
    Me.Label6.TabIndex = 71
    Me.Label6.Text = "円"
    '
    'Label12
    '
    Me.Label12.AutoSize = True
    Me.Label12.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label12.Location = New System.Drawing.Point(706, 263)
    Me.Label12.Name = "Label12"
    Me.Label12.Size = New System.Drawing.Size(79, 30)
    Me.Label12.TabIndex = 82
    Me.Label12.Text = "金額："
    '
    'TxtHuteikanKingaku
    '
    Me.TxtHuteikanKingaku.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.TxtHuteikanKingaku.Location = New System.Drawing.Point(791, 263)
    Me.TxtHuteikanKingaku.MaxLength = 10
    Me.TxtHuteikanKingaku.Name = "TxtHuteikanKingaku"
    Me.TxtHuteikanKingaku.ReadOnly = True
    Me.TxtHuteikanKingaku.Size = New System.Drawing.Size(64, 33)
    Me.TxtHuteikanKingaku.TabIndex = 10
    Me.TxtHuteikanKingaku.TabStop = False
    Me.TxtHuteikanKingaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'Label13
    '
    Me.Label13.AutoSize = True
    Me.Label13.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label13.Location = New System.Drawing.Point(861, 263)
    Me.Label13.Name = "Label13"
    Me.Label13.Size = New System.Drawing.Size(35, 30)
    Me.Label13.TabIndex = 80
    Me.Label13.Text = "円"
    '
    'Label15
    '
    Me.Label15.AutoSize = True
    Me.Label15.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label15.Location = New System.Drawing.Point(12, 422)
    Me.Label15.Name = "Label15"
    Me.Label15.Size = New System.Drawing.Size(168, 30)
    Me.Label15.TabIndex = 84
    Me.Label15.Text = "生簀ロット番号："
    '
    'TxtKotai1
    '
    Me.TxtKotai1.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.TxtKotai1.Location = New System.Drawing.Point(207, 423)
    Me.TxtKotai1.MaxLength = 20
    Me.TxtKotai1.Name = "TxtKotai1"
    Me.TxtKotai1.Size = New System.Drawing.Size(314, 33)
    Me.TxtKotai1.TabIndex = 15
    '
    'TxtKotai2
    '
    Me.TxtKotai2.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.TxtKotai2.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtKotai2.Location = New System.Drawing.Point(207, 462)
    Me.TxtKotai2.MaxLength = 20
    Me.TxtKotai2.Name = "TxtKotai2"
    Me.TxtKotai2.Size = New System.Drawing.Size(314, 33)
    Me.TxtKotai2.TabIndex = 16
    '
    'Label16
    '
    Me.Label16.AutoSize = True
    Me.Label16.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label16.Location = New System.Drawing.Point(79, 461)
    Me.Label16.Name = "Label16"
    Me.Label16.Size = New System.Drawing.Size(101, 30)
    Me.Label16.TabIndex = 86
    Me.Label16.Text = "原産地："
    '
    'TxtKotai3
    '
    Me.TxtKotai3.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.TxtKotai3.Location = New System.Drawing.Point(207, 501)
    Me.TxtKotai3.MaxLength = 20
    Me.TxtKotai3.Name = "TxtKotai3"
    Me.TxtKotai3.Size = New System.Drawing.Size(314, 33)
    Me.TxtKotai3.TabIndex = 17
    Me.TxtKotai3.Visible = False
    '
    'Label17
    '
    Me.Label17.AutoSize = True
    Me.Label17.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label17.Location = New System.Drawing.Point(112, 500)
    Me.Label17.Name = "Label17"
    Me.Label17.Size = New System.Drawing.Size(67, 30)
    Me.Label17.TabIndex = 88
    Me.Label17.Text = "メモ："
    Me.Label17.Visible = False
    '
    'BtnDelGyo
    '
    Me.BtnDelGyo.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.BtnDelGyo.Location = New System.Drawing.Point(639, 558)
    Me.BtnDelGyo.Name = "BtnDelGyo"
    Me.BtnDelGyo.Size = New System.Drawing.Size(123, 43)
    Me.BtnDelGyo.TabIndex = 20
    Me.BtnDelGyo.Text = "F3:明細削除"
    Me.BtnDelGyo.UseVisualStyleBackColor = True
    '
    'BtnAddGyo
    '
    Me.BtnAddGyo.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.BtnAddGyo.Location = New System.Drawing.Point(510, 558)
    Me.BtnAddGyo.Name = "BtnAddGyo"
    Me.BtnAddGyo.Size = New System.Drawing.Size(123, 43)
    Me.BtnAddGyo.TabIndex = 19
    Me.BtnAddGyo.Text = "F1:明細登録"
    Me.BtnAddGyo.UseVisualStyleBackColor = True
    '
    'BtnClose
    '
    Me.BtnClose.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.BtnClose.Location = New System.Drawing.Point(768, 558)
    Me.BtnClose.Name = "BtnClose"
    Me.BtnClose.Size = New System.Drawing.Size(123, 43)
    Me.BtnClose.TabIndex = 21
    Me.BtnClose.Text = "F10:終了"
    Me.BtnClose.UseVisualStyleBackColor = True
    '
    'TxtTeikanTani
    '
    Me.TxtTeikanTani.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.TxtTeikanTani.Location = New System.Drawing.Point(422, 315)
    Me.TxtTeikanTani.MaxLength = 10
    Me.TxtTeikanTani.Name = "TxtTeikanTani"
    Me.TxtTeikanTani.Size = New System.Drawing.Size(64, 33)
    Me.TxtTeikanTani.TabIndex = 12
    Me.TxtTeikanTani.Visible = False
    '
    'TxtKeiryoFlg
    '
    Me.TxtKeiryoFlg.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.TxtKeiryoFlg.Location = New System.Drawing.Point(801, 62)
    Me.TxtKeiryoFlg.MaxLength = 10
    Me.TxtKeiryoFlg.Name = "TxtKeiryoFlg"
    Me.TxtKeiryoFlg.Size = New System.Drawing.Size(64, 33)
    Me.TxtKeiryoFlg.TabIndex = 90
    Me.TxtKeiryoFlg.Text = "0"
    Me.TxtKeiryoFlg.Visible = False
    '
    'Label18
    '
    Me.Label18.AutoSize = True
    Me.Label18.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label18.Location = New System.Drawing.Point(12, 214)
    Me.Label18.Name = "Label18"
    Me.Label18.Size = New System.Drawing.Size(153, 30)
    Me.Label18.TabIndex = 91
    Me.Label18.Text = "定貫/不定貫："
    Me.Label18.Visible = False
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
    Me.CmbMstItem1.Location = New System.Drawing.Point(146, 135)
    Me.CmbMstItem1.Name = "CmbMstItem1"
    Me.CmbMstItem1.Size = New System.Drawing.Size(121, 34)
    Me.CmbMstItem1.SkipChkCode = False
    Me.CmbMstItem1.TabIndex = 2
    Me.CmbMstItem1.ValueMember = "Code"
    '
    'CmbTeikan
    '
    Me.CmbTeikan.AvailableBlank = False
    Me.CmbTeikan.BorderColor = System.Drawing.SystemColors.ControlText
    Me.CmbTeikan.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None
    Me.CmbTeikan.BorderWidth = 1
    Me.CmbTeikan.CodeFormat = ""
    Me.CmbTeikan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
    Me.CmbTeikan.EventCancel = False
    Me.CmbTeikan.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.CmbTeikan.FormattingEnabled = True
    Me.CmbTeikan.Items.AddRange(New Object() {"不定貫", "定貫"})
    Me.CmbTeikan.Location = New System.Drawing.Point(157, 215)
    Me.CmbTeikan.Name = "CmbTeikan"
    Me.CmbTeikan.Size = New System.Drawing.Size(121, 33)
    Me.CmbTeikan.TabIndex = 5
    Me.CmbTeikan.Visible = False
    '
    'Label3
    '
    Me.Label3.AutoSize = True
    Me.Label3.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label3.Location = New System.Drawing.Point(706, 315)
    Me.Label3.Name = "Label3"
    Me.Label3.Size = New System.Drawing.Size(79, 30)
    Me.Label3.TabIndex = 97
    Me.Label3.Text = "金額："
    Me.Label3.Visible = False
    '
    'TxtTeikanKingaku
    '
    Me.TxtTeikanKingaku.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.TxtTeikanKingaku.Location = New System.Drawing.Point(791, 315)
    Me.TxtTeikanKingaku.MaxLength = 10
    Me.TxtTeikanKingaku.Name = "TxtTeikanKingaku"
    Me.TxtTeikanKingaku.ReadOnly = True
    Me.TxtTeikanKingaku.Size = New System.Drawing.Size(64, 33)
    Me.TxtTeikanKingaku.TabIndex = 14
    Me.TxtTeikanKingaku.TabStop = False
    Me.TxtTeikanKingaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    Me.TxtTeikanKingaku.Visible = False
    '
    'Label7
    '
    Me.Label7.AutoSize = True
    Me.Label7.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label7.Location = New System.Drawing.Point(861, 315)
    Me.Label7.Name = "Label7"
    Me.Label7.Size = New System.Drawing.Size(35, 30)
    Me.Label7.TabIndex = 96
    Me.Label7.Text = "円"
    Me.Label7.Visible = False
    '
    'TxtTeikanTanka
    '
    Me.TxtTeikanTanka.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.TxtTeikanTanka.Location = New System.Drawing.Point(595, 315)
    Me.TxtTeikanTanka.MaxLength = 10
    Me.TxtTeikanTanka.Name = "TxtTeikanTanka"
    Me.TxtTeikanTanka.Size = New System.Drawing.Size(64, 33)
    Me.TxtTeikanTanka.TabIndex = 13
    Me.TxtTeikanTanka.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    Me.TxtTeikanTanka.Visible = False
    '
    'Label8
    '
    Me.Label8.AutoSize = True
    Me.Label8.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label8.Location = New System.Drawing.Point(665, 315)
    Me.Label8.Name = "Label8"
    Me.Label8.Size = New System.Drawing.Size(35, 30)
    Me.Label8.TabIndex = 94
    Me.Label8.Text = "円"
    Me.Label8.Visible = False
    '
    'Label10
    '
    Me.Label10.AutoSize = True
    Me.Label10.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label10.Location = New System.Drawing.Point(510, 315)
    Me.Label10.Name = "Label10"
    Me.Label10.Size = New System.Drawing.Size(79, 30)
    Me.Label10.TabIndex = 95
    Me.Label10.Text = "単価："
    Me.Label10.Visible = False
    '
    'Label11
    '
    Me.Label11.AutoSize = True
    Me.Label11.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label11.Location = New System.Drawing.Point(326, 315)
    Me.Label11.Name = "Label11"
    Me.Label11.Size = New System.Drawing.Size(79, 30)
    Me.Label11.TabIndex = 99
    Me.Label11.Text = "単位："
    Me.Label11.Visible = False
    '
    'Label14
    '
    Me.Label14.AutoSize = True
    Me.Label14.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label14.Location = New System.Drawing.Point(326, 263)
    Me.Label14.Name = "Label14"
    Me.Label14.Size = New System.Drawing.Size(79, 30)
    Me.Label14.TabIndex = 98
    Me.Label14.Text = "単位："
    '
    'TxtCustCd
    '
    Me.TxtCustCd.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.TxtCustCd.Location = New System.Drawing.Point(801, 101)
    Me.TxtCustCd.MaxLength = 10
    Me.TxtCustCd.Name = "TxtCustCd"
    Me.TxtCustCd.Size = New System.Drawing.Size(64, 33)
    Me.TxtCustCd.TabIndex = 100
    Me.TxtCustCd.Text = "0"
    Me.TxtCustCd.Visible = False
    '
    'TitleLabel
    '
    Me.TitleLabel.Location = New System.Drawing.Point(0, 0)
    Me.TitleLabel.Name = "TitleLabel"
    Me.TitleLabel.Size = New System.Drawing.Size(100, 23)
    Me.TitleLabel.TabIndex = 0
    '
    'Label19
    '
    Me.Label19.AutoSize = True
    Me.Label19.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label19.Location = New System.Drawing.Point(12, 263)
    Me.Label19.Name = "Label19"
    Me.Label19.Size = New System.Drawing.Size(79, 30)
    Me.Label19.TabIndex = 102
    Me.Label19.Text = "尾数："
    '
    'TxtHuteikanKosu
    '
    Me.TxtHuteikanKosu.Font = New System.Drawing.Font("Segoe UI", 14.25!)
    Me.TxtHuteikanKosu.Location = New System.Drawing.Point(97, 263)
    Me.TxtHuteikanKosu.MaxLength = 10
    Me.TxtHuteikanKosu.Name = "TxtHuteikanKosu"
    Me.TxtHuteikanKosu.Size = New System.Drawing.Size(64, 33)
    Me.TxtHuteikanKosu.TabIndex = 6
    Me.TxtHuteikanKosu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'ItemAddForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(903, 613)
    Me.Controls.Add(Me.Label19)
    Me.Controls.Add(Me.TxtHuteikanKosu)
    Me.Controls.Add(Me.TitleLabel)
    Me.Controls.Add(Me.TxtCustCd)
    Me.Controls.Add(Me.Label11)
    Me.Controls.Add(Me.Label14)
    Me.Controls.Add(Me.Label3)
    Me.Controls.Add(Me.TxtTeikanKingaku)
    Me.Controls.Add(Me.Label7)
    Me.Controls.Add(Me.TxtTeikanTanka)
    Me.Controls.Add(Me.Label8)
    Me.Controls.Add(Me.Label10)
    Me.Controls.Add(Me.CmbTeikan)
    Me.Controls.Add(Me.Label18)
    Me.Controls.Add(Me.TxtKeiryoFlg)
    Me.Controls.Add(Me.TxtTeikanTani)
    Me.Controls.Add(Me.BtnClose)
    Me.Controls.Add(Me.BtnAddGyo)
    Me.Controls.Add(Me.BtnDelGyo)
    Me.Controls.Add(Me.TxtKotai3)
    Me.Controls.Add(Me.Label17)
    Me.Controls.Add(Me.TxtKotai2)
    Me.Controls.Add(Me.Label16)
    Me.Controls.Add(Me.TxtKotai1)
    Me.Controls.Add(Me.Label15)
    Me.Controls.Add(Me.Label12)
    Me.Controls.Add(Me.TxtHuteikanKingaku)
    Me.Controls.Add(Me.Label13)
    Me.Controls.Add(Me.Label4)
    Me.Controls.Add(Me.TxtHuteikanNohinSuryo)
    Me.Controls.Add(Me.TxtHuteikanTani)
    Me.Controls.Add(Me.TxtHuteikanTanka)
    Me.Controls.Add(Me.TxtTeikanNohinSuryo)
    Me.Controls.Add(Me.Label6)
    Me.Controls.Add(Me.Label5)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.TxtItemNameKana)
    Me.Controls.Add(Me.TxtItemName)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.CmbMstItem1)
    Me.Controls.Add(Me.TxtGyoNo)
    Me.Controls.Add(Me.Label9)
    Me.DoubleBuffered = True
    Me.KeyPreview = True
    Me.Name = "ItemAddForm"
    Me.Text = "明細追加画面"
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents TxtGyoNo As TextBox
  Friend WithEvents Label9 As Label
  Friend WithEvents CmbMstItem1 As T.R.ZCommonCtrl.CmbMstItem
  Friend WithEvents Label1 As Label
  Friend WithEvents TxtItemName As TextBox
  Friend WithEvents TxtItemNameKana As TextBox
  Friend WithEvents Label2 As Label
  Friend WithEvents TxtTeikanNohinSuryo As TextBox
  Friend WithEvents TxtHuteikanTani As TextBox
  Friend WithEvents Label4 As Label
  Friend WithEvents TxtHuteikanNohinSuryo As TextBox
  Friend WithEvents Label5 As Label
  Friend WithEvents TxtHuteikanTanka As TextBox
  Friend WithEvents Label6 As Label
  Friend WithEvents Label12 As Label
  Friend WithEvents TxtHuteikanKingaku As TextBox
  Friend WithEvents Label13 As Label
  Friend WithEvents Label15 As Label
  Friend WithEvents TxtKotai1 As TextBox
  Friend WithEvents TxtKotai2 As TextBox
  Friend WithEvents Label16 As Label
  Friend WithEvents TxtKotai3 As TextBox
  Friend WithEvents Label17 As Label
    Friend WithEvents BtnDelGyo As Button
    Friend WithEvents BtnAddGyo As Button
    Friend WithEvents BtnClose As Button
    Friend WithEvents TxtTeikanTani As TextBox
    Friend WithEvents TxtKeiryoFlg As TextBox
    Friend WithEvents Label18 As Label
    Friend WithEvents CmbTeikan As T.R.ZCommonCtrl.CmbBase
    Friend WithEvents Label3 As Label
    Friend WithEvents TxtTeikanKingaku As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TxtTeikanTanka As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label14 As Label
    Friend WithEvents TxtCustCd As TextBox
    Friend WithEvents TitleLabel As Label
    Friend WithEvents Label19 As Label
    Friend WithEvents TxtHuteikanKosu As TextBox
End Class
