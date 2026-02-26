<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ComSearchForm
  Inherits R.ZCommonCtrl.MFBaseDgv

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
    Me.DataGridView1 = New System.Windows.Forms.DataGridView()
    Me.btnSelect = New System.Windows.Forms.Button()
    Me.btnClose = New System.Windows.Forms.Button()
    Me.txtName = New System.Windows.Forms.TextBox()
    Me.TxtNumericBase1 = New T.R.ZCommonCtrl.TxtNumericBase()
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
    Me.SuspendLayout()
    '
    'DataGridView1
    '
    Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
    Me.DataGridView1.Location = New System.Drawing.Point(12, 53)
    Me.DataGridView1.Name = "DataGridView1"
    Me.DataGridView1.Size = New System.Drawing.Size(425, 292)
    Me.DataGridView1.TabIndex = 0
    '
    'btnSelect
    '
    Me.btnSelect.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 12.22642!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.btnSelect.Location = New System.Drawing.Point(233, 364)
    Me.btnSelect.Name = "btnSelect"
    Me.btnSelect.Size = New System.Drawing.Size(90, 27)
    Me.btnSelect.TabIndex = 1
    Me.btnSelect.Text = "選択"
    Me.btnSelect.UseVisualStyleBackColor = True
    '
    'btnClose
    '
    Me.btnClose.Font = New System.Drawing.Font("ＭＳ Ｐゴシック", 12.22642!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.btnClose.Location = New System.Drawing.Point(347, 364)
    Me.btnClose.Name = "btnClose"
    Me.btnClose.Size = New System.Drawing.Size(90, 27)
    Me.btnClose.TabIndex = 2
    Me.btnClose.Text = "閉じる"
    Me.btnClose.UseVisualStyleBackColor = True
    '
    'txtName
    '
    Me.txtName.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.22642!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
    Me.txtName.Location = New System.Drawing.Point(129, 19)
    Me.txtName.Name = "txtName"
    Me.txtName.Size = New System.Drawing.Size(308, 26)
    Me.txtName.TabIndex = 4
    '
    'TxtNumericBase1
    '
    Me.TxtNumericBase1.Font = New System.Drawing.Font("ＭＳ ゴシック", 12.22642!)
    Me.TxtNumericBase1.ImeMode = System.Windows.Forms.ImeMode.Alpha
    Me.TxtNumericBase1.Location = New System.Drawing.Point(12, 19)
    Me.TxtNumericBase1.Name = "TxtNumericBase1"
    Me.TxtNumericBase1.Size = New System.Drawing.Size(69, 26)
    Me.TxtNumericBase1.TabIndex = 5
    Me.TxtNumericBase1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
    '
    'ComSearchForm
    '
    Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
    Me.ClientSize = New System.Drawing.Size(449, 408)
    Me.Controls.Add(Me.TxtNumericBase1)
    Me.Controls.Add(Me.txtName)
    Me.Controls.Add(Me.btnClose)
    Me.Controls.Add(Me.btnSelect)
    Me.Controls.Add(Me.DataGridView1)
    Me.KeyPreview = True
    Me.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
    Me.Name = "ComSearchForm"
    CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
    Me.ResumeLayout(False)
    Me.PerformLayout()

  End Sub

  Friend WithEvents DataGridView1 As DataGridView
  Friend WithEvents btnSelect As Button
  Friend WithEvents btnClose As Button
  Friend WithEvents txtName As TextBox
  Friend WithEvents TxtNumericBase1 As TxtNumericBase
End Class
