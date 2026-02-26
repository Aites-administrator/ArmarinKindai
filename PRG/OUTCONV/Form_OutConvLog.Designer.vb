<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_OutConvLog
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
    Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form_OutConvLog))
    Me.DataGridView1 = New System.Windows.Forms.DataGridView()
    Me.CmbMstCustomer1 = New T.R.ZCommonCtrl.CmbMstCustomer()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.lblInformation = New System.Windows.Forms.Label()
    Me.lblPostCount = New System.Windows.Forms.Label()
    Me.btnEnd = New T.R.ZCommonCtrl.ButtonEnd()
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'DataGridView1
    '
    Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView1.Location = New System.Drawing.Point(12, 12)
    Me.DataGridView1.Name = "DataGridView1"
    Me.DataGridView1.RowTemplate.Height = 21
    Me.DataGridView1.Size = New System.Drawing.Size(1510, 728)
    Me.DataGridView1.TabIndex = 0
    Me.DataGridView1.TabStop = False
    '
    'CmbMstCustomer1
    '
    Me.CmbMstCustomer1.AvailableBlank = False
    Me.CmbMstCustomer1.DisplayMember = "ItemName"
    Me.CmbMstCustomer1.FormattingEnabled = True
    Me.CmbMstCustomer1.Location = New System.Drawing.Point(970, 798)
    Me.CmbMstCustomer1.Name = "CmbMstCustomer1"
    Me.CmbMstCustomer1.Size = New System.Drawing.Size(422, 27)
    Me.CmbMstCustomer1.TabIndex = 3
    Me.CmbMstCustomer1.ValueMember = "ItemCode"
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.ForeColor = System.Drawing.Color.White
    Me.Label1.Location = New System.Drawing.Point(860, 801)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(104, 19)
    Me.Label1.TabIndex = 4
    Me.Label1.Text = "得意先指定"
    '
    'lblInformation
    '
    Me.lblInformation.BackColor = System.Drawing.Color.White
    Me.lblInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
    Me.lblInformation.Font = New System.Drawing.Font("MS UI Gothic", 14.26415!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblInformation.ForeColor = System.Drawing.Color.Navy
    Me.lblInformation.Location = New System.Drawing.Point(0, 883)
    Me.lblInformation.Name = "lblInformation"
    Me.lblInformation.Size = New System.Drawing.Size(1535, 29)
    Me.lblInformation.TabIndex = 19
    Me.lblInformation.Text = "Label18"
    Me.lblInformation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
    '
    'lblPostCount
    '
    Me.lblPostCount.AutoSize = True
    Me.lblPostCount.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.lblPostCount.ForeColor = System.Drawing.Color.Navy
    Me.lblPostCount.Location = New System.Drawing.Point(23, 801)
    Me.lblPostCount.Name = "lblPostCount"
    Me.lblPostCount.Size = New System.Drawing.Size(281, 21)
    Me.lblPostCount.TabIndex = 20
    Me.lblPostCount.Text = "ここに送信件数が表示されます"
    '
    'btnEnd
    '
    Me.btnEnd.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
    Me.btnEnd.Font = New System.Drawing.Font("MS UI Gothic", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.btnEnd.Image = CType(resources.GetObject("btnEnd.Image"), System.Drawing.Image)
    Me.btnEnd.Location = New System.Drawing.Point(1428, 768)
    Me.btnEnd.Name = "btnEnd"
    Me.btnEnd.Size = New System.Drawing.Size(94, 84)
    Me.btnEnd.TabIndex = 114
    Me.btnEnd.TabStop = False
    Me.btnEnd.Text = "F12:戻る"
    Me.btnEnd.TextAlign = System.Drawing.ContentAlignment.BottomCenter
    Me.btnEnd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.btnEnd.UseVisualStyleBackColor = False
    '
    'Form_OutConvLog
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(10.0!, 19.0!)
    Me.BackColor = System.Drawing.Color.Teal
    Me.ClientSize = New System.Drawing.Size(1534, 909)
    Me.Controls.Add(Me.btnEnd)
    Me.Controls.Add(Me.lblPostCount)
    Me.Controls.Add(Me.lblInformation)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.CmbMstCustomer1)
    Me.Controls.Add(Me.DataGridView1)
    Me.Font = New System.Drawing.Font("MS UI Gothic", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.KeyPreview = True
    Me.Name = "Form_OutConvLog"
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents DataGridView1 As DataGridView
  Friend WithEvents CmbMstCustomer1 As T.R.ZCommonCtrl.CmbMstCustomer
  Friend WithEvents Label1 As Label
  Friend WithEvents lblInformation As Label
  Friend WithEvents lblPostCount As Label
  Friend WithEvents btnEnd As T.R.ZCommonCtrl.ButtonEnd
End Class
