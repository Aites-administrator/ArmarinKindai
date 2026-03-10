<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_SelectPrint
  Inherits T.R.ZCommonCtrl.MFBaseDgv

  'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
  <System.Diagnostics.DebuggerNonUserCode()> _
  Protected Overrides Sub Dispose(ByVal disposing As Boolean)
    If disposing AndAlso components IsNot Nothing Then
      components.Dispose()
    End If
    MyBase.Dispose(disposing)
  End Sub

  'Windows フォーム デザイナーで必要です。
  Private components As System.ComponentModel.IContainer

  'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
  'Windows フォーム デザイナーを使用して変更できます。  
  'コード エディターを使って変更しないでください。
  <System.Diagnostics.DebuggerStepThrough()> _
  Private Sub InitializeComponent()
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_SelectPrint))
    Me.DataGridView1 = New System.Windows.Forms.DataGridView()
    Me.DataGridView2 = New System.Windows.Forms.DataGridView()
    Me.CmbMstCustomer1 = New T.R.ZCommonCtrl.CmbMstCustomer()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.CmbDateProcessing1 = New T.R.ZCommonCtrl.CmbDateProcessing()
    Me.lblItemDetailListStat = New System.Windows.Forms.Label()
    Me.lblCustomerListStat = New System.Windows.Forms.Label()
    Me.lblInformation = New System.Windows.Forms.Label()
    Me.lblUnSendCount = New System.Windows.Forms.Label()
    Me.lblPostCount = New System.Windows.Forms.Label()
    Me.btnPostPca = New T.R.ZCommonCtrl.ButtonUpdate()
    Me.btnEnd = New T.R.ZCommonCtrl.ButtonEnd()
    Me.Label3 = New System.Windows.Forms.Label()
    Me.btnShowLogForm = New T.R.ZCommonCtrl.ButtonUpdate()
    Me.TxtMstCustomer1 = New System.Windows.Forms.TextBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DataGridView1
        '
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.Control
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(20, 128)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 21
        Me.DataGridView1.Size = New System.Drawing.Size(1495, 273)
        Me.DataGridView1.TabIndex = 2
        '
        'DataGridView2
        '
        Me.DataGridView2.BackgroundColor = System.Drawing.SystemColors.Control
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(20, 452)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.RowTemplate.Height = 21
        Me.DataGridView2.Size = New System.Drawing.Size(1495, 389)
        Me.DataGridView2.TabIndex = 1
        Me.DataGridView2.TabStop = False
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
        Me.CmbMstCustomer1.FormattingEnabled = True
        Me.CmbMstCustomer1.Location = New System.Drawing.Point(492, 58)
        Me.CmbMstCustomer1.Name = "CmbMstCustomer1"
        Me.CmbMstCustomer1.Size = New System.Drawing.Size(110, 27)
        Me.CmbMstCustomer1.SkipChkCode = True
        Me.CmbMstCustomer1.TabIndex = 1
        Me.CmbMstCustomer1.ValueMember = "Code"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label1.Location = New System.Drawing.Point(401, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 19)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "得意先名"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label2.Location = New System.Drawing.Point(16, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 19)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "納品日"
        '
        'CmbDateProcessing1
        '
        Me.CmbDateProcessing1.AvailableBlank = False
        Me.CmbDateProcessing1.BorderColor = System.Drawing.SystemColors.ControlText
        Me.CmbDateProcessing1.BorderStyle = System.Windows.Forms.ButtonBorderStyle.None
        Me.CmbDateProcessing1.BorderWidth = 1
        Me.CmbDateProcessing1.CodeFormat = ""
        Me.CmbDateProcessing1.DisplayMember = "Code"
        Me.CmbDateProcessing1.EventCancel = False
        Me.CmbDateProcessing1.FormattingEnabled = True
        Me.CmbDateProcessing1.Location = New System.Drawing.Point(95, 58)
        Me.CmbDateProcessing1.Name = "CmbDateProcessing1"
        Me.CmbDateProcessing1.Size = New System.Drawing.Size(162, 27)
        Me.CmbDateProcessing1.TabIndex = 0
        Me.CmbDateProcessing1.ValueMember = "Code"
        '
        'lblItemDetailListStat
        '
        Me.lblItemDetailListStat.BackColor = System.Drawing.SystemColors.Control
        Me.lblItemDetailListStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblItemDetailListStat.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblItemDetailListStat.ForeColor = System.Drawing.Color.Black
        Me.lblItemDetailListStat.Location = New System.Drawing.Point(20, 422)
        Me.lblItemDetailListStat.Name = "lblItemDetailListStat"
        Me.lblItemDetailListStat.Size = New System.Drawing.Size(1495, 31)
        Me.lblItemDetailListStat.TabIndex = 15
        Me.lblItemDetailListStat.Text = "Label18"
        Me.lblItemDetailListStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCustomerListStat
        '
        Me.lblCustomerListStat.BackColor = System.Drawing.SystemColors.Control
        Me.lblCustomerListStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCustomerListStat.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblCustomerListStat.ForeColor = System.Drawing.Color.Black
        Me.lblCustomerListStat.Location = New System.Drawing.Point(20, 99)
        Me.lblCustomerListStat.Name = "lblCustomerListStat"
        Me.lblCustomerListStat.Size = New System.Drawing.Size(1495, 31)
        Me.lblCustomerListStat.TabIndex = 16
        Me.lblCustomerListStat.Text = "Label18"
        Me.lblCustomerListStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblInformation
        '
        Me.lblInformation.BackColor = System.Drawing.Color.White
        Me.lblInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInformation.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblInformation.ForeColor = System.Drawing.Color.Navy
        Me.lblInformation.Location = New System.Drawing.Point(0, 881)
        Me.lblInformation.Name = "lblInformation"
        Me.lblInformation.Size = New System.Drawing.Size(1535, 29)
        Me.lblInformation.TabIndex = 18
        Me.lblInformation.Text = "Label18"
        Me.lblInformation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblUnSendCount
        '
        Me.lblUnSendCount.BackColor = System.Drawing.Color.FromArgb(CType(CType(208, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.lblUnSendCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblUnSendCount.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblUnSendCount.ForeColor = System.Drawing.Color.Black
        Me.lblUnSendCount.Location = New System.Drawing.Point(1252, 840)
        Me.lblUnSendCount.Name = "lblUnSendCount"
        Me.lblUnSendCount.Size = New System.Drawing.Size(263, 31)
        Me.lblUnSendCount.TabIndex = 19
        Me.lblUnSendCount.Text = "Label18"
        Me.lblUnSendCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPostCount
        '
        Me.lblPostCount.AutoSize = True
        Me.lblPostCount.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.lblPostCount.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(160, Byte), Integer))
        Me.lblPostCount.Location = New System.Drawing.Point(23, 852)
        Me.lblPostCount.Name = "lblPostCount"
        Me.lblPostCount.Size = New System.Drawing.Size(73, 21)
        Me.lblPostCount.TabIndex = 21
        Me.lblPostCount.Text = "Label3"
        '
        'btnPostPca
        '
        Me.btnPostPca.BackColor = System.Drawing.SystemColors.Control
        Me.btnPostPca.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnPostPca.Image = CType(resources.GetObject("btnPostPca.Image"), System.Drawing.Image)
        Me.btnPostPca.Location = New System.Drawing.Point(1143, 10)
        Me.btnPostPca.Name = "btnPostPca"
        Me.btnPostPca.Size = New System.Drawing.Size(94, 84)
        Me.btnPostPca.TabIndex = 114
        Me.btnPostPca.TabStop = False
        Me.btnPostPca.Text = "F1:発行"
        Me.btnPostPca.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnPostPca.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnPostPca.UseVisualStyleBackColor = False
        '
        'btnEnd
        '
        Me.btnEnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.btnEnd.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnEnd.Image = CType(resources.GetObject("btnEnd.Image"), System.Drawing.Image)
        Me.btnEnd.Location = New System.Drawing.Point(1421, 9)
        Me.btnEnd.Name = "btnEnd"
        Me.btnEnd.Size = New System.Drawing.Size(94, 84)
        Me.btnEnd.TabIndex = 113
        Me.btnEnd.TabStop = False
        Me.btnEnd.Text = "F12:終了"
        Me.btnEnd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnEnd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnEnd.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("MS UI Gothic", 21.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Label3.Location = New System.Drawing.Point(15, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(253, 29)
        Me.Label3.TabIndex = 115
        Me.Label3.Text = "選択一括発行画面"
        '
        'btnShowLogForm
        '
        Me.btnShowLogForm.BackColor = System.Drawing.SystemColors.Control
        Me.btnShowLogForm.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.btnShowLogForm.Image = CType(resources.GetObject("btnShowLogForm.Image"), System.Drawing.Image)
        Me.btnShowLogForm.Location = New System.Drawing.Point(1285, 9)
        Me.btnShowLogForm.Name = "btnShowLogForm"
        Me.btnShowLogForm.Size = New System.Drawing.Size(94, 84)
        Me.btnShowLogForm.TabIndex = 116
        Me.btnShowLogForm.TabStop = False
        Me.btnShowLogForm.Text = "F5:集約取消"
        Me.btnShowLogForm.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnShowLogForm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        Me.btnShowLogForm.UseVisualStyleBackColor = False
        '
        'TxtMstCustomer1
        '
        Me.TxtMstCustomer1.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtMstCustomer1.Location = New System.Drawing.Point(607, 55)
        Me.TxtMstCustomer1.MaxLength = 10
        Me.TxtMstCustomer1.Name = "TxtMstCustomer1"
        Me.TxtMstCustomer1.ReadOnly = True
        Me.TxtMstCustomer1.Size = New System.Drawing.Size(401, 33)
        Me.TxtMstCustomer1.TabIndex = 117
        Me.TxtMstCustomer1.TabStop = False
        '
        'Form_SelectPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 19.0!)
        Me.BackColor = System.Drawing.SystemColors.AppWorkspace
        Me.ClientSize = New System.Drawing.Size(1534, 909)
        Me.Controls.Add(Me.TxtMstCustomer1)
        Me.Controls.Add(Me.btnShowLogForm)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnPostPca)
        Me.Controls.Add(Me.btnEnd)
        Me.Controls.Add(Me.lblPostCount)
        Me.Controls.Add(Me.lblUnSendCount)
        Me.Controls.Add(Me.lblInformation)
        Me.Controls.Add(Me.lblCustomerListStat)
        Me.Controls.Add(Me.lblItemDetailListStat)
        Me.Controls.Add(Me.CmbDateProcessing1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CmbMstCustomer1)
        Me.Controls.Add(Me.DataGridView2)
        Me.Controls.Add(Me.DataGridView1)
        Me.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "Form_SelectPrint"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents DataGridView1 As DataGridView
  Friend WithEvents DataGridView2 As DataGridView
  Friend WithEvents CmbMstCustomer1 As T.R.ZCommonCtrl.CmbMstCustomer
  Friend WithEvents Label1 As Label
  Friend WithEvents Label2 As Label
  Friend WithEvents CmbDateProcessing1 As T.R.ZCommonCtrl.CmbDateProcessing
  Friend WithEvents lblItemDetailListStat As Label
  Friend WithEvents lblCustomerListStat As Label
  Friend WithEvents lblInformation As Label
  Friend WithEvents lblUnSendCount As Label
  Friend WithEvents lblPostCount As Label
  Friend WithEvents btnPostPca As T.R.ZCommonCtrl.ButtonUpdate
  Friend WithEvents btnEnd As T.R.ZCommonCtrl.ButtonEnd
  Friend WithEvents Label3 As Label
  Friend WithEvents btnShowLogForm As T.R.ZCommonCtrl.ButtonUpdate
  Friend WithEvents TxtMstCustomer1 As TextBox
End Class
