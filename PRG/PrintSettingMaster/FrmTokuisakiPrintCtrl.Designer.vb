<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmTokuisakiPrintCtrl
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
    Me.txtTokuisakiCd = New T.R.ZCommonCtrl.TxtBase()
    Me.txtTokuisakiNm = New T.R.ZCommonCtrl.TxtBase()
    Me.btnSearch = New T.R.ZCommonCtrl.BtnBase()
    Me.btnClear = New T.R.ZCommonCtrl.BtnBase()
    Me.btnSave = New T.R.ZCommonCtrl.BtnBase()
    Me.dgvList = New System.Windows.Forms.DataGridView()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.Label2 = New System.Windows.Forms.Label()
    CType(Me.dgvList, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'txtTokuisakiCd
    '
    Me.txtTokuisakiCd.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.txtTokuisakiCd.Location = New System.Drawing.Point(163, 40)
    Me.txtTokuisakiCd.Name = "txtTokuisakiCd"
    Me.txtTokuisakiCd.Size = New System.Drawing.Size(155, 28)
    Me.txtTokuisakiCd.TabIndex = 0
    '
    'txtTokuisakiNm
    '
    Me.txtTokuisakiNm.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.txtTokuisakiNm.Location = New System.Drawing.Point(467, 40)
    Me.txtTokuisakiNm.Name = "txtTokuisakiNm"
    Me.txtTokuisakiNm.Size = New System.Drawing.Size(296, 28)
    Me.txtTokuisakiNm.TabIndex = 1
    '
    'btnSearch
    '
    Me.btnSearch.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.btnSearch.Location = New System.Drawing.Point(467, 87)
    Me.btnSearch.Name = "btnSearch"
    Me.btnSearch.Size = New System.Drawing.Size(78, 36)
    Me.btnSearch.TabIndex = 2
    Me.btnSearch.Text = "検索"
    Me.btnSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.btnSearch.UseVisualStyleBackColor = True
    '
    'btnClear
    '
    Me.btnClear.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.btnClear.Location = New System.Drawing.Point(574, 87)
    Me.btnClear.Name = "btnClear"
    Me.btnClear.Size = New System.Drawing.Size(78, 36)
    Me.btnClear.TabIndex = 3
    Me.btnClear.Text = "クリア"
    Me.btnClear.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.btnClear.UseVisualStyleBackColor = True
    '
    'btnSave
    '
    Me.btnSave.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.btnSave.Location = New System.Drawing.Point(685, 87)
    Me.btnSave.Name = "btnSave"
    Me.btnSave.Size = New System.Drawing.Size(78, 36)
    Me.btnSave.TabIndex = 4
    Me.btnSave.Text = "保存"
    Me.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
    Me.btnSave.UseVisualStyleBackColor = True
    '
    'dgvList
    '
    Me.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.dgvList.Location = New System.Drawing.Point(41, 143)
    Me.dgvList.Name = "dgvList"
    Me.dgvList.RowTemplate.Height = 21
    Me.dgvList.Size = New System.Drawing.Size(722, 283)
    Me.dgvList.TabIndex = 5
    '
    'Label1
    '
    Me.Label1.AutoSize = True
    Me.Label1.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label1.Location = New System.Drawing.Point(37, 45)
    Me.Label1.Name = "Label1"
    Me.Label1.Size = New System.Drawing.Size(120, 21)
    Me.Label1.TabIndex = 6
    Me.Label1.Text = "得意先コード"
    '
    'Label2
    '
    Me.Label2.AutoSize = True
    Me.Label2.Font = New System.Drawing.Font("MS UI Gothic", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.Label2.Location = New System.Drawing.Point(357, 45)
    Me.Label2.Name = "Label2"
    Me.Label2.Size = New System.Drawing.Size(94, 21)
    Me.Label2.TabIndex = 7
    Me.Label2.Text = "得意先名"
    '
    'FrmTokuisakiPrintCtrl
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
    Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
    Me.ClientSize = New System.Drawing.Size(800, 450)
    Me.Controls.Add(Me.Label2)
    Me.Controls.Add(Me.Label1)
    Me.Controls.Add(Me.dgvList)
    Me.Controls.Add(Me.btnSave)
    Me.Controls.Add(Me.btnClear)
    Me.Controls.Add(Me.btnSearch)
    Me.Controls.Add(Me.txtTokuisakiNm)
    Me.Controls.Add(Me.txtTokuisakiCd)
    Me.Name = "FrmTokuisakiPrintCtrl"
    Me.Text = "Form1"
    CType(Me.dgvList, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents txtTokuisakiCd As T.R.ZCommonCtrl.TxtBase
  Friend WithEvents txtTokuisakiNm As T.R.ZCommonCtrl.TxtBase
  Friend WithEvents btnSearch As T.R.ZCommonCtrl.BtnBase
  Friend WithEvents btnClear As T.R.ZCommonCtrl.BtnBase
  Friend WithEvents btnSave As T.R.ZCommonCtrl.BtnBase
  Friend WithEvents dgvList As DataGridView
  Friend WithEvents Label1 As Label
  Friend WithEvents Label2 As Label
End Class
