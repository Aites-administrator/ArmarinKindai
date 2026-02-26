<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form_ItemDetail
  Inherits System.Windows.Forms.Form

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
    Me.TitleLabel = New System.Windows.Forms.Label()
    Me.CloseButton = New System.Windows.Forms.Button()
    Me.OkButton = New System.Windows.Forms.Button()
    Me.GroupBox1 = New System.Windows.Forms.GroupBox()
    Me.TxtZeiKbn = New System.Windows.Forms.TextBox()
    Me.Label18 = New System.Windows.Forms.Label()
    Me.TxtIrisu = New System.Windows.Forms.TextBox()
    Me.Label19 = New System.Windows.Forms.Label()
    Me.TxtTani = New System.Windows.Forms.TextBox()
    Me.Label20 = New System.Windows.Forms.Label()
    Me.TxtItemName = New System.Windows.Forms.TextBox()
    Me.Label21 = New System.Windows.Forms.Label()
    Me.TxtItemNameKana = New System.Windows.Forms.TextBox()
    Me.Label22 = New System.Windows.Forms.Label()
    Me.TxtItemCode = New System.Windows.Forms.TextBox()
    Me.Label23 = New System.Windows.Forms.Label()
    Me.GroupBox2 = New System.Windows.Forms.GroupBox()
    Me.TxtGenka = New System.Windows.Forms.TextBox()
    Me.Label1 = New System.Windows.Forms.Label()
    Me.TxtHyojunTanka = New System.Windows.Forms.TextBox()
    Me.Label2 = New System.Windows.Forms.Label()
    Me.TxtNouhinTanka = New System.Windows.Forms.TextBox()
    Me.Label12 = New System.Windows.Forms.Label()
    Me.TxtTyuBunrui = New System.Windows.Forms.TextBox()
    Me.Label13 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(12, 9)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(150, 30)
        Me.TitleLabel.TabIndex = 44
        Me.TitleLabel.Text = "商品マスタ詳細"
        '
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CloseButton.Location = New System.Drawing.Point(622, 342)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 43)
        Me.CloseButton.TabIndex = 78
        Me.CloseButton.Text = "終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'OkButton
        '
        Me.OkButton.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OkButton.Location = New System.Drawing.Point(492, 342)
        Me.OkButton.Name = "OkButton"
        Me.OkButton.Size = New System.Drawing.Size(123, 43)
        Me.OkButton.TabIndex = 77
        Me.OkButton.Text = "登録"
        Me.OkButton.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.TxtZeiKbn)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.TxtIrisu)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.TxtTani)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.TxtItemName)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.TxtItemNameKana)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.TxtItemCode)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Location = New System.Drawing.Point(42, 42)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(344, 281)
        Me.GroupBox1.TabIndex = 80
        Me.GroupBox1.TabStop = False
        '
        'TxtZeiKbn
        '
        Me.TxtZeiKbn.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtZeiKbn.Location = New System.Drawing.Point(183, 217)
        Me.TxtZeiKbn.MaxLength = 6
        Me.TxtZeiKbn.Name = "TxtZeiKbn"
        Me.TxtZeiKbn.Size = New System.Drawing.Size(147, 33)
        Me.TxtZeiKbn.TabIndex = 6
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(6, 220)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(79, 30)
        Me.Label18.TabIndex = 83
        Me.Label18.Text = "税区分"
        '
        'TxtIrisu
        '
        Me.TxtIrisu.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtIrisu.Location = New System.Drawing.Point(183, 137)
        Me.TxtIrisu.MaxLength = 6
        Me.TxtIrisu.Name = "TxtIrisu"
        Me.TxtIrisu.Size = New System.Drawing.Size(147, 33)
        Me.TxtIrisu.TabIndex = 4
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(6, 140)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(57, 30)
        Me.Label19.TabIndex = 81
        Me.Label19.Text = "入数"
        '
        'TxtTani
        '
        Me.TxtTani.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTani.Location = New System.Drawing.Point(183, 177)
        Me.TxtTani.MaxLength = 6
        Me.TxtTani.Name = "TxtTani"
        Me.TxtTani.Size = New System.Drawing.Size(147, 33)
        Me.TxtTani.TabIndex = 5
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(6, 180)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(57, 30)
        Me.Label20.TabIndex = 79
        Me.Label20.Text = "単位"
        '
        'TxtItemName
        '
        Me.TxtItemName.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtItemName.Location = New System.Drawing.Point(183, 57)
        Me.TxtItemName.MaxLength = 20
        Me.TxtItemName.Name = "TxtItemName"
        Me.TxtItemName.Size = New System.Drawing.Size(147, 33)
        Me.TxtItemName.TabIndex = 2
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(6, 60)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(79, 30)
        Me.Label21.TabIndex = 77
        Me.Label21.Text = "商品名"
        '
        'TxtItemNameKana
        '
        Me.TxtItemNameKana.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtItemNameKana.Location = New System.Drawing.Point(183, 97)
        Me.TxtItemNameKana.MaxLength = 99
        Me.TxtItemNameKana.Name = "TxtItemNameKana"
        Me.TxtItemNameKana.Size = New System.Drawing.Size(147, 33)
        Me.TxtItemNameKana.TabIndex = 3
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(6, 100)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(128, 30)
        Me.Label22.TabIndex = 75
        Me.Label22.Text = "商品名(かな)"
        '
        'TxtItemCode
        '
        Me.TxtItemCode.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtItemCode.Location = New System.Drawing.Point(183, 17)
        Me.TxtItemCode.MaxLength = 8
        Me.TxtItemCode.Name = "TxtItemCode"
        Me.TxtItemCode.Size = New System.Drawing.Size(147, 33)
        Me.TxtItemCode.TabIndex = 1
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(6, 20)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(85, 30)
        Me.Label23.TabIndex = 73
        Me.Label23.Text = "商品CD"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.TxtGenka)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.TxtHyojunTanka)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.TxtNouhinTanka)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.TxtTyuBunrui)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Location = New System.Drawing.Point(392, 42)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(344, 193)
        Me.GroupBox2.TabIndex = 81
        Me.GroupBox2.TabStop = False
        '
        'TxtGenka
        '
        Me.TxtGenka.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtGenka.Location = New System.Drawing.Point(183, 57)
        Me.TxtGenka.MaxLength = 6
        Me.TxtGenka.Name = "TxtGenka"
        Me.TxtGenka.Size = New System.Drawing.Size(147, 33)
        Me.TxtGenka.TabIndex = 8
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 30)
        Me.Label1.TabIndex = 77
        Me.Label1.Text = "原価"
        '
        'TxtHyojunTanka
        '
        Me.TxtHyojunTanka.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtHyojunTanka.Location = New System.Drawing.Point(183, 17)
        Me.TxtHyojunTanka.MaxLength = 6
        Me.TxtHyojunTanka.Name = "TxtHyojunTanka"
        Me.TxtHyojunTanka.Size = New System.Drawing.Size(147, 33)
        Me.TxtHyojunTanka.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 30)
        Me.Label2.TabIndex = 75
        Me.Label2.Text = "標準単価"
        '
        'TxtNouhinTanka
        '
        Me.TxtNouhinTanka.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtNouhinTanka.Location = New System.Drawing.Point(183, 97)
        Me.TxtNouhinTanka.MaxLength = 6
        Me.TxtNouhinTanka.Name = "TxtNouhinTanka"
        Me.TxtNouhinTanka.Size = New System.Drawing.Size(147, 33)
        Me.TxtNouhinTanka.TabIndex = 9
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(6, 100)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(101, 30)
        Me.Label12.TabIndex = 73
        Me.Label12.Text = "納品単価"
        '
        'TxtTyuBunrui
        '
        Me.TxtTyuBunrui.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTyuBunrui.Location = New System.Drawing.Point(183, 137)
        Me.TxtTyuBunrui.MaxLength = 6
        Me.TxtTyuBunrui.Name = "TxtTyuBunrui"
        Me.TxtTyuBunrui.Size = New System.Drawing.Size(147, 33)
        Me.TxtTyuBunrui.TabIndex = 10
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(6, 140)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(79, 30)
        Me.Label13.TabIndex = 71
        Me.Label13.Text = "中分類"
        '
        'Form_ItemDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(754, 397)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.OkButton)
        Me.Controls.Add(Me.TitleLabel)
        Me.Name = "Form_ItemDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ItemDetail"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TitleLabel As Label
  Friend WithEvents CloseButton As Button
  Friend WithEvents OkButton As Button
  Friend WithEvents GroupBox1 As GroupBox
  Friend WithEvents TxtZeiKbn As TextBox
  Friend WithEvents Label18 As Label
  Friend WithEvents TxtIrisu As TextBox
  Friend WithEvents Label19 As Label
  Friend WithEvents TxtTani As TextBox
  Friend WithEvents Label20 As Label
  Friend WithEvents TxtItemName As TextBox
  Friend WithEvents Label21 As Label
  Friend WithEvents TxtItemNameKana As TextBox
  Friend WithEvents Label22 As Label
  Friend WithEvents TxtItemCode As TextBox
  Friend WithEvents Label23 As Label
  Friend WithEvents GroupBox2 As GroupBox
  Friend WithEvents TxtGenka As TextBox
  Friend WithEvents Label1 As Label
  Friend WithEvents TxtHyojunTanka As TextBox
  Friend WithEvents Label2 As Label
  Friend WithEvents TxtNouhinTanka As TextBox
  Friend WithEvents Label12 As Label
  Friend WithEvents TxtTyuBunrui As TextBox
  Friend WithEvents Label13 As Label
End Class
