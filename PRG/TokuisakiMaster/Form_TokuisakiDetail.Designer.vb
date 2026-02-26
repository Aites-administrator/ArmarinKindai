<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form_TokuisakiDetail
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
        Me.TitleLabel = New System.Windows.Forms.Label()
        Me.CloseButton = New System.Windows.Forms.Button()
        Me.OkButton = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtGroupCode = New System.Windows.Forms.TextBox()
        Me.TxtShohiZeiTuchi = New System.Windows.Forms.TextBox()
        Me.TxtSeikyuSmb = New System.Windows.Forms.TextBox()
        Me.TxtTelNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtYubinNo = New System.Windows.Forms.TextBox()
        Me.TxtJusho2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtJusho1 = New System.Windows.Forms.TextBox()
        Me.TxtTokuisakiName = New System.Windows.Forms.TextBox()
        Me.NameLabel = New System.Windows.Forms.Label()
        Me.TxtTokuisakiCode = New System.Windows.Forms.TextBox()
        Me.CodeLabel = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TitleLabel
        '
        Me.TitleLabel.AutoSize = True
        Me.TitleLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TitleLabel.Location = New System.Drawing.Point(12, 9)
        Me.TitleLabel.Name = "TitleLabel"
        Me.TitleLabel.Size = New System.Drawing.Size(172, 30)
        Me.TitleLabel.TabIndex = 45
        Me.TitleLabel.Text = "得意先マスタ詳細"
        '
        'CloseButton
        '
        Me.CloseButton.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CloseButton.Location = New System.Drawing.Point(569, 427)
        Me.CloseButton.Name = "CloseButton"
        Me.CloseButton.Size = New System.Drawing.Size(123, 43)
        Me.CloseButton.TabIndex = 80
        Me.CloseButton.Text = "終了"
        Me.CloseButton.UseVisualStyleBackColor = True
        '
        'OkButton
        '
        Me.OkButton.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OkButton.Location = New System.Drawing.Point(440, 427)
        Me.OkButton.Name = "OkButton"
        Me.OkButton.Size = New System.Drawing.Size(123, 43)
        Me.OkButton.TabIndex = 79
        Me.OkButton.Text = "登録"
        Me.OkButton.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.TxtGroupCode)
        Me.GroupBox1.Controls.Add(Me.TxtShohiZeiTuchi)
        Me.GroupBox1.Controls.Add(Me.TxtSeikyuSmb)
        Me.GroupBox1.Controls.Add(Me.TxtTelNo)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.TxtYubinNo)
        Me.GroupBox1.Controls.Add(Me.TxtJusho2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TxtJusho1)
        Me.GroupBox1.Controls.Add(Me.TxtTokuisakiName)
        Me.GroupBox1.Controls.Add(Me.NameLabel)
        Me.GroupBox1.Controls.Add(Me.TxtTokuisakiCode)
        Me.GroupBox1.Controls.Add(Me.CodeLabel)
        Me.GroupBox1.Location = New System.Drawing.Point(17, 42)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(672, 379)
        Me.GroupBox1.TabIndex = 81
        Me.GroupBox1.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(6, 330)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(132, 30)
        Me.Label7.TabIndex = 87
        Me.Label7.Text = "グループコード"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(6, 291)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(123, 30)
        Me.Label6.TabIndex = 86
        Me.Label6.Text = "消費税通知"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 249)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(101, 30)
        Me.Label5.TabIndex = 85
        Me.Label5.Text = "請求締日"
        '
        'TxtGroupCode
        '
        Me.TxtGroupCode.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtGroupCode.Location = New System.Drawing.Point(159, 327)
        Me.TxtGroupCode.MaxLength = 48
        Me.TxtGroupCode.Name = "TxtGroupCode"
        Me.TxtGroupCode.Size = New System.Drawing.Size(147, 33)
        Me.TxtGroupCode.TabIndex = 9
        '
        'TxtShohiZeiTuchi
        '
        Me.TxtShohiZeiTuchi.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtShohiZeiTuchi.Location = New System.Drawing.Point(159, 288)
        Me.TxtShohiZeiTuchi.MaxLength = 48
        Me.TxtShohiZeiTuchi.Name = "TxtShohiZeiTuchi"
        Me.TxtShohiZeiTuchi.Size = New System.Drawing.Size(147, 33)
        Me.TxtShohiZeiTuchi.TabIndex = 8
        '
        'TxtSeikyuSmb
        '
        Me.TxtSeikyuSmb.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtSeikyuSmb.Location = New System.Drawing.Point(159, 249)
        Me.TxtSeikyuSmb.MaxLength = 48
        Me.TxtSeikyuSmb.Name = "TxtSeikyuSmb"
        Me.TxtSeikyuSmb.Size = New System.Drawing.Size(147, 33)
        Me.TxtSeikyuSmb.TabIndex = 7
        '
        'TxtTelNo
        '
        Me.TxtTelNo.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTelNo.Location = New System.Drawing.Point(159, 210)
        Me.TxtTelNo.MaxLength = 48
        Me.TxtTelNo.Name = "TxtTelNo"
        Me.TxtTelNo.Size = New System.Drawing.Size(147, 33)
        Me.TxtTelNo.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 210)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 30)
        Me.Label4.TabIndex = 83
        Me.Label4.Text = "電話番号"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(6, 171)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(101, 30)
        Me.Label3.TabIndex = 82
        Me.Label3.Text = "郵便番号"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(6, 132)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 30)
        Me.Label2.TabIndex = 81
        Me.Label2.Text = "住所2"
        '
        'TxtYubinNo
        '
        Me.TxtYubinNo.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtYubinNo.Location = New System.Drawing.Point(159, 171)
        Me.TxtYubinNo.MaxLength = 48
        Me.TxtYubinNo.Name = "TxtYubinNo"
        Me.TxtYubinNo.Size = New System.Drawing.Size(147, 33)
        Me.TxtYubinNo.TabIndex = 5
        '
        'TxtJusho2
        '
        Me.TxtJusho2.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtJusho2.Location = New System.Drawing.Point(159, 132)
        Me.TxtJusho2.MaxLength = 48
        Me.TxtJusho2.Name = "TxtJusho2"
        Me.TxtJusho2.Size = New System.Drawing.Size(507, 33)
        Me.TxtJusho2.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(6, 96)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 30)
        Me.Label1.TabIndex = 81
        Me.Label1.Text = "住所1"
        '
        'TxtJusho1
        '
        Me.TxtJusho1.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtJusho1.Location = New System.Drawing.Point(159, 93)
        Me.TxtJusho1.MaxLength = 48
        Me.TxtJusho1.Name = "TxtJusho1"
        Me.TxtJusho1.Size = New System.Drawing.Size(507, 33)
        Me.TxtJusho1.TabIndex = 3
        '
        'TxtTokuisakiName
        '
        Me.TxtTokuisakiName.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTokuisakiName.Location = New System.Drawing.Point(159, 54)
        Me.TxtTokuisakiName.MaxLength = 48
        Me.TxtTokuisakiName.Name = "TxtTokuisakiName"
        Me.TxtTokuisakiName.Size = New System.Drawing.Size(147, 33)
        Me.TxtTokuisakiName.TabIndex = 2
        '
        'NameLabel
        '
        Me.NameLabel.AutoSize = True
        Me.NameLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NameLabel.Location = New System.Drawing.Point(6, 57)
        Me.NameLabel.Name = "NameLabel"
        Me.NameLabel.Size = New System.Drawing.Size(101, 30)
        Me.NameLabel.TabIndex = 79
        Me.NameLabel.Text = "得意先名"
        '
        'TxtTokuisakiCode
        '
        Me.TxtTokuisakiCode.Font = New System.Drawing.Font("Segoe UI", 14.25!)
        Me.TxtTokuisakiCode.Location = New System.Drawing.Point(159, 15)
        Me.TxtTokuisakiCode.MaxLength = 6
        Me.TxtTokuisakiCode.Name = "TxtTokuisakiCode"
        Me.TxtTokuisakiCode.Size = New System.Drawing.Size(147, 33)
        Me.TxtTokuisakiCode.TabIndex = 1
        '
        'CodeLabel
        '
        Me.CodeLabel.AutoSize = True
        Me.CodeLabel.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CodeLabel.Location = New System.Drawing.Point(6, 15)
        Me.CodeLabel.Name = "CodeLabel"
        Me.CodeLabel.Size = New System.Drawing.Size(128, 30)
        Me.CodeLabel.TabIndex = 77
        Me.CodeLabel.Text = "得意先コード"
        '
        'Form_TokuisakiDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(701, 482)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CloseButton)
        Me.Controls.Add(Me.OkButton)
        Me.Controls.Add(Me.TitleLabel)
        Me.Name = "Form_TokuisakiDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Form_ManufacturerDetail"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TitleLabel As Label
    Friend WithEvents CloseButton As Button
    Friend WithEvents OkButton As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents TxtTokuisakiName As TextBox
    Friend WithEvents NameLabel As Label
    Friend WithEvents TxtTokuisakiCode As TextBox
    Friend WithEvents CodeLabel As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents TxtGroupCode As TextBox
    Friend WithEvents TxtShohiZeiTuchi As TextBox
    Friend WithEvents TxtSeikyuSmb As TextBox
    Friend WithEvents TxtTelNo As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents TxtYubinNo As TextBox
    Friend WithEvents TxtJusho2 As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TxtJusho1 As TextBox
End Class
