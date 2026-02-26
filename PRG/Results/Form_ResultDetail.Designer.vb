Imports T.R.ZCommonCtrl
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_ResultDetail
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtDenNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.CmbNohinDay = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.CreateButton = New System.Windows.Forms.Button()
        Me.TxtTokuName = New System.Windows.Forms.TextBox()
        Me.TxtShohinCd = New System.Windows.Forms.TextBox()
        Me.TxtGoukeiKin = New System.Windows.Forms.TextBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 30)
        Me.Label1.TabIndex = 96
        Me.Label1.Text = "伝票番号"
        '
        'TxtDenNo
        '
        Me.TxtDenNo.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtDenNo.Location = New System.Drawing.Point(119, 48)
        Me.TxtDenNo.MaxLength = 8
        Me.TxtDenNo.Name = "TxtDenNo"
        Me.TxtDenNo.Size = New System.Drawing.Size(147, 33)
        Me.TxtDenNo.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(272, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(106, 30)
        Me.Label2.TabIndex = 98
        Me.Label2.Text = "ShohinCD"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(643, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 30)
        Me.Label3.TabIndex = 100
        Me.Label3.Text = "合計Kingaku"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(857, 51)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 30)
        Me.Label5.TabIndex = 100
        Me.Label5.Text = "円"
        '
        'CmbNohinDay
        '
        Me.CmbNohinDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CmbNohinDay.Font = New System.Drawing.Font("Segoe UI", 14.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(128, Byte))
        Me.CmbNohinDay.FormattingEnabled = True
        Me.CmbNohinDay.ItemHeight = 25
        Me.CmbNohinDay.Location = New System.Drawing.Point(728, 3)
        Me.CmbNohinDay.Name = "CmbNohinDay"
        Me.CmbNohinDay.Size = New System.Drawing.Size(186, 33)
        Me.CmbNohinDay.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(643, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(79, 30)
        Me.Label6.TabIndex = 100
        Me.Label6.Text = "納品日"
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(12, 87)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowTemplate.Height = 21
        Me.DataGridView1.Size = New System.Drawing.Size(1066, 617)
        Me.DataGridView1.TabIndex = 4
        '
        'CreateButton
        '
        Me.CreateButton.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.CreateButton.Location = New System.Drawing.Point(955, 710)
        Me.CreateButton.Name = "CreateButton"
        Me.CreateButton.Size = New System.Drawing.Size(123, 43)
        Me.CreateButton.TabIndex = 5
        Me.CreateButton.Text = "閉じる"
        Me.CreateButton.UseVisualStyleBackColor = True
        '
        'TxtTokuName
        '
        Me.TxtTokuName.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTokuName.Location = New System.Drawing.Point(17, 9)
        Me.TxtTokuName.MaxLength = 8
        Me.TxtTokuName.Name = "TxtTokuName"
        Me.TxtTokuName.ReadOnly = True
        Me.TxtTokuName.Size = New System.Drawing.Size(620, 33)
        Me.TxtTokuName.TabIndex = 97
        Me.TxtTokuName.TabStop = False
        '
        'TxtShohinCd
        '
        Me.TxtShohinCd.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtShohinCd.Location = New System.Drawing.Point(384, 48)
        Me.TxtShohinCd.MaxLength = 8
        Me.TxtShohinCd.Name = "TxtShohinCd"
        Me.TxtShohinCd.Size = New System.Drawing.Size(164, 33)
        Me.TxtShohinCd.TabIndex = 2
        '
        'TxtGoukeiKin
        '
        Me.TxtGoukeiKin.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.TxtGoukeiKin.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtGoukeiKin.Location = New System.Drawing.Point(750, 51)
        Me.TxtGoukeiKin.MaxLength = 8
        Me.TxtGoukeiKin.Name = "TxtGoukeiKin"
        Me.TxtGoukeiKin.ReadOnly = True
        Me.TxtGoukeiKin.Size = New System.Drawing.Size(101, 33)
        Me.TxtGoukeiKin.TabIndex = 97
        Me.TxtGoukeiKin.TabStop = False
        Me.TxtGoukeiKin.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Form_ResultDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1090, 764)
        Me.Controls.Add(Me.CreateButton)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtTokuName)
        Me.Controls.Add(Me.TxtGoukeiKin)
        Me.Controls.Add(Me.TxtShohinCd)
        Me.Controls.Add(Me.TxtDenNo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.CmbNohinDay)
        Me.Name = "Form_ResultDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form2"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtDenNo As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents CmbNohinDay As ComboBox
    Friend WithEvents Label6 As Label
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents CreateButton As Button
    Friend WithEvents TxtTokuName As TextBox
    Friend WithEvents TxtShohinCd As TextBox
    Friend WithEvents TxtGoukeiKin As TextBox
End Class
