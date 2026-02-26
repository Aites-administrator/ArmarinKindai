Imports System.ComponentModel
Imports Common

Public Class CmbBase
    Inherits ComboBox
    Private Const WM_PAINT As Integer = &HF

#Region "メンバ"
#Region "プライベート"
    ' フォーカス取得フラグ
    ' マウスでのフォーカス移動時の全選択に使用
    Private _OnFocus As Boolean

    ''' <summary>
    ''' メッセージ出力ラベル
    ''' </summary>
    Private _msgLabel As Label

    ''' <summary>
    ''' メッセージ出力ラベルテキスト
    ''' </summary>
    Private _msgLabelText As String

    ''' <summary>
    ''' 空文字列有効無効フラグ 
    ''' </summary>
    Private _AvailableBlank As Boolean

    ''' <summary>
    ''' コード入力フォーマット
    ''' </summary>
    Private _CodeFormat As String = String.Empty

    ''' <summary>
    ''' コード入力有無
    ''' </summary>
    Private _CodeInput As Boolean = True

    ''' <summary>
    ''' コンボボックスの枠線色
    ''' </summary>
    Private _BorderColor As Color = System.Drawing.SystemColors.ControlText

    ''' <summary>
    ''' コンボボックスの枠線スタイル
    ''' </summary>
    Private _BorderStyle As ButtonBorderStyle = ButtonBorderStyle.None

    ''' <summary>
    ''' コンボボックスの枠線幅
    ''' </summary>
    Private _BorderWidth As Integer = 1

    ''' <summary>
    ''' イベントキャンセル有無
    ''' </summary>
    Private _EventCancel As Boolean = False

    ''' <summary>
    ''' デフォルト背景色
    ''' </summary>
    Private _BackColor As Color

    ''' <summary>
    ''' 初期分岐タイプ
    ''' </summary>
    Private _InitType As Integer = 0

#End Region

#Region "パブリック"

    ''' <summary>
    ''' 選択項目抽出SQL文作成関数コールバック(型)
    ''' </summary>
    ''' <param name="prmCode">絞込項目</param>
    ''' <returns>作成したSQL文</returns>
    Delegate Function CallBackCreateSql(ByVal prmCode As String) As String

    ''' <summary>
    ''' 選択項目抽出SQL文作成関数コールバック（本体）
    ''' </summary>
    Public lcCallBackCreateSql As CallBackCreateSql
#End Region

#End Region

#Region "プロパティー"
#Region "パブリック"

    ''' <summary>
    ''' 処理分岐
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>デフォルト 0</remarks>
    <DefaultValueAttribute(0)>
    Public Property InitType As Integer
        Get
            Return _InitType
        End Get
        Set(value As Integer)
            _InitType = value
        End Set
    End Property

    ''' <summary>
    ''' 空文字列の許可
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>デフォルト False</remarks>
    Public Property AvailableBlank As Boolean
        Get
            Return _AvailableBlank
        End Get
        Set(value As Boolean)
            _AvailableBlank = value
        End Set
    End Property

    ''' <summary>
    ''' コード入力フォーマット
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>コード入力フォーマット</remarks>
    <DefaultValueAttribute("00")>
    Public Property CodeFormat As String
        Get
            Return _CodeFormat
        End Get
        Set(value As String)
            _CodeFormat = value
        End Set
    End Property

    ''' <summary>
    ''' コード入力有無
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>コード入力有無</remarks>
    <DefaultValueAttribute(True)>
    Public Property CodeInput As Boolean
        Get
            Return _CodeInput
        End Get
        Set(value As Boolean)
            _CodeInput = value
        End Set
    End Property

    ''' <summary>
    ''' コンボボックスの枠線色選択
    ''' </summary>
    ''' <returns></returns>
    Public Property BorderColor() As Color
        Get
            Return Me._BorderColor
        End Get
        Set(ByVal value As Color)
            Me._BorderColor = value
        End Set
    End Property

    ''' <summary>
    ''' コンボボックスの枠線スタイル選択
    ''' </summary>
    ''' <returns></returns>
    Public Property BorderStyle() As ButtonBorderStyle
        Get
            Return Me._BorderStyle
        End Get
        Set(ByVal value As ButtonBorderStyle)
            Me._BorderStyle = value
        End Set
    End Property

    ''' <summary>
    ''' コンボボックスの枠線幅選択
    ''' </summary>
    ''' <returns></returns>
    Public Property BorderWidth() As Integer
        Get
            Return Me._BorderWidth
        End Get
        Set(ByVal value As Integer)
            If value < 0 Then value = 1
            If value > 3 Then value = 3
            Me._BorderWidth = value
        End Set
    End Property

    ''' <summary>
    ''' イベントキャンセル有無
    ''' </summary>
    ''' <returns></returns>
    Public Property EventCancel() As Boolean
        Get
            Return Me._EventCancel
        End Get
        Set(ByVal value As Boolean)
            Me._EventCancel = value
        End Set
    End Property

#End Region
#End Region

#Region "コンストラクタ"
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    Public Sub New()
        _OnFocus = False

        _AvailableBlank = False

    End Sub

#End Region

#Region "メソッド"

#Region "プライベート"

    ''' <summary>
    ''' 選択項目抽出SQL文作成関数
    ''' </summary>
    ''' <param name="prmCode">絞込項目</param>
    ''' <returns>作成したSQL文</returns>
    ''' <remarks>継承先関数で指定されるlcCallBackCreateSqlを実行する</remarks>
    Private Function CreateSql(Optional prmCode As String = "") As String
        Dim tmpSql As String = String.Empty

        If lcCallBackCreateSql IsNot Nothing Then
            tmpSql = lcCallBackCreateSql(prmCode)
        End If

        Return tmpSql
    End Function

    ''' <summary>
    ''' コンボボックスの枠線描画
    ''' </summary>
    Private Sub DrawRectangle()
        Try
            'ボタンスタイルコントロールの輪郭の描画(線の幅指定有り)  
            ControlPaint.DrawBorder(Me.CreateGraphics(), Me.ClientRectangle,
                              Me.BorderColor, Me.BorderWidth, Me.BorderStyle,
                              Me.BorderColor, Me.BorderWidth, Me.BorderStyle,
                              Me.BorderColor, Me.BorderWidth, Me.BorderStyle,
                              Me.BorderColor, Me.BorderWidth, Me.BorderStyle)
        Catch
        End Try
    End Sub

#End Region

#Region "パブリック"

    ''' <summary>
    ''' メッセージラベルの定義
    ''' </summary>
    ''' <param name="msgLabel">メッセージを表示するラベル情報</param>
    Public Sub SetMsgLabel(msgLabel As Label)

        _msgLabel = msgLabel

    End Sub

    ''' <summary>
    ''' メッセージラベルへのメッセージ表示
    ''' </summary>
    ''' <param name="msg">メッセージ</param>
    Public Sub SetMsgLabelText(msg As String)

        _msgLabelText = msg

    End Sub

    ''' <summary>
    ''' コンボボックス初期化
    ''' </summary>
    Public Function InitCmb() As DataTable

        Dim tmpDb As New ClsSqlServer
        Dim tmpDt As New DataTable

        tmpDb.GetResult(tmpDt, CreateSql)

        With Me
            .DataSource = tmpDt
            If tmpDt.Columns.Contains("Name") Then
                .DisplayMember = tmpDt.Columns("Name").ColumnName
            Else
                .DisplayMember = tmpDt.Columns("Code").ColumnName
            End If
            .ValueMember = tmpDt.Columns("Code").ColumnName
            .DropDownStyle = ComboBoxStyle.DropDown

        End With

        Return tmpDt

    End Function

    ''' <summary>
    ''' コンボボックス再設定
    ''' </summary>
    Public Function ReSetCmb(prmSql As String) As DataTable

        Dim tmpDb As New ClsSqlServer
        Dim tmpDt As New DataTable

        tmpDb.GetResult(tmpDt, prmSql)

        With Me
            .DataSource = Nothing
            .DataSource = tmpDt
            If tmpDt.Columns.Contains("Name") Then
                .DisplayMember = tmpDt.Columns("Name").ColumnName
            Else
                .DisplayMember = tmpDt.Columns("Code").ColumnName
            End If
            .ValueMember = tmpDt.Columns("Code").ColumnName
            .DropDownStyle = ComboBoxStyle.DropDown

        End With

        Return tmpDt

    End Function

    ''' <summary>
    ''' コンボボックス初期化（ＳＱＬ未使用）
    ''' </summary>
    Public Sub InitCmbNonSql()

        With Me

            .DropDownStyle = ComboBoxStyle.DropDown

            ' コンボボックスイベントハンドラの削除
            RemoveHandler .Validating, AddressOf CmbBoxValidating

            ' コンボボックスイベントハンドラの追加
            AddHandler .Validating, AddressOf CmbBoxValidating

        End With

    End Sub

    ''' <summary>
    ''' コンボボックス初期化（ＳＱＬ未使用）
    ''' </summary>
    Public Sub InitCmbNonSql(prmList As Dictionary(Of String, String),
                           Optional prmCodeDisp As Boolean = False)

        With Me

            Dim tmpDt As New DataTable
            tmpDt.Columns.Add("Code")
            tmpDt.Columns.Add("Name")

            Dim row As DataRow
            For Each kvp As KeyValuePair(Of String, String) In prmList
                row = tmpDt.NewRow
                row("Code") = kvp.Key
                row("Name") = kvp.Value
                tmpDt.Rows.Add(row)
            Next

            If (prmCodeDisp) Then
                .DisplayMember = "Code"
            Else
                .DisplayMember = "Name"
            End If

            .ValueMember = "Code"
            .DataSource = tmpDt
            .DropDownStyle = ComboBoxStyle.DropDown

            ' コンボボックスイベントハンドラの削除
            RemoveHandler .Validating, AddressOf CmbBoxValidating

            ' コンボボックスイベントハンドラの追加
            AddHandler .Validating, AddressOf CmbBoxValidating

        End With

    End Sub

    ''' <summary>
    ''' コンボボックスデータソースクリア
    ''' </summary>
    Public Sub ClearDataSorce()

        With Me

            .DataSource = Nothing

        End With

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <returns></returns>
    Public Function CmbTextChanged(sender As Object) As String

        Dim wkCmb As ComboBox = CType(sender, ComboBox)
        Dim strTemp As String = String.Empty

        If wkCmb.SelectedIndex <> -1 Then

            Dim cmbRow As DataRowView
            cmbRow = DirectCast(wkCmb.Items(wkCmb.SelectedIndex), DataRowView)
            If IsDBNull(cmbRow(1)) Then
                strTemp = ""
            Else
                strTemp = cmbRow(1).ToString
            End If

        End If

        Return strTemp

    End Function

#End Region

#End Region

#Region "イベントプロシージャー"
    ''' <summary>
    ''' マウスホイールが動くと発生するイベント
    ''' </summary>
    Private Sub CmbMstBase_MouseWheel(ByVal sender As Object, ByVal e As MouseEventArgs) Handles Me.MouseWheel
        ' イベントを処理済みにし、選択値が変わらないようにする
        Dim eventArgs As HandledMouseEventArgs = DirectCast(e, HandledMouseEventArgs)
        eventArgs.Handled = True
    End Sub

    ''' <summary>
    ''' コンボボックスにコードが入力されたときに、設定された値を表示する
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CmbBoxValidating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating

        Try
            If System.Text.RegularExpressions.Regex.IsMatch(Me.Text, "^[0-9]+$") Then

                If (_CodeInput) Then
                    If String.IsNullOrWhiteSpace(CodeFormat) Then
                        Me.SelectedValue = Me.Text
                    Else
                        Me.SelectedValue = Val(Me.Text).ToString(CodeFormat)
                    End If
                Else
                    Me.Text = Me.Text
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            e.Cancel = True
        End Try

    End Sub

    Private Sub TCmbMstBase_MousUp(sender As Object, e As EventArgs) Handles Me.MouseUp
        If _OnFocus Then
            _OnFocus = False
            sender.SelectAll()
        End If
    End Sub

    ''' <summary>
    ''' メッセージラベルへのメッセージの表示
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CmbMstBase_OnEnter(sender As Object, e As EventArgs) Handles Me.Enter

        sender.SelectAll()
        _OnFocus = True

        _BackColor = Me.BackColor
        Me.BackColor = Color.Aqua

        ' メッセージラベルの定義が未設定の場合
        If _msgLabel Is Nothing Then
            Exit Sub
        Else
            ' メッセージラベルへのメッセージの表示
            _msgLabel.Text = _msgLabelText
        End If

    End Sub

    Private Sub CmbMstBase_Leave(sender As Object, e As EventArgs) Handles Me.Leave
        Me.BackColor = _BackColor
    End Sub

    ''' <summary>
    ''' コンボボックスの枠線描画呼び出し
    ''' </summary>
    ''' <param name="m"></param>
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)
        MyBase.WndProc(m)
        Select Case m.Msg
            Case WM_PAINT
                Call DrawRectangle()
        End Select
    End Sub



    Private Sub CmbBase_DrawItem(sender As Object, e As DrawItemEventArgs) Handles Me.DrawItem
        ''▼データの内容を取得
        Try

            Dim ItemString1 As String = String.Empty
            Dim ItemString2 As String = String.Empty

            If e.Index < 0 Then
                ItemString1 = ""
                ItemString2 = ""
            Else

                ' データソース別項目取得
                Select Case TypeName(sender.Items(e.Index))
                    Case "KeyValuePair(Of String,String)"

                        Dim kvp As KeyValuePair(Of String, String)
                        kvp = DirectCast(sender.Items(e.Index), KeyValuePair(Of String, String))
                        ItemString1 = kvp.Key
                        ItemString2 = kvp.Value

                    Case "DataRowView"
                        Dim Row As DataRowView

                        Row = DirectCast(sender.Items(e.Index), DataRowView)

                        If IsDBNull(Row(0)) Then
                            ItemString1 = ""
                        Else
                            ItemString1 = Row(0)
                        End If
                        If IsDBNull(Row(1)) Then
                            ItemString2 = ""
                        Else
                            ItemString2 = Row(1)
                        End If

                    Case Else
                        Dim strTmp As String = DirectCast(sender.Items(e.Index), String)

                        If (strTmp.Length > 10) Then
                            ItemString1 = strTmp.Substring(0, 8)
                            ItemString2 = strTmp
                        Else
                            ItemString1 = ""
                            ItemString2 = ""
                        End If

                End Select
            End If

            Dim cb As ComboBox = DirectCast(sender, ComboBox)
            Dim bLineX As Single
            Dim p As Pen = New Pen(Color.Gray)
            Dim b As Brush = New SolidBrush(e.ForeColor)

            e.DrawBackground()
            If CBool(e.State And DrawItemState.Selected) Then
                ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds)
            Else
                e.Graphics.FillRectangle(Brushes.White, New RectangleF(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height))
            End If

            Dim g As Graphics = cb.CreateGraphics()
            Dim sf As SizeF = g.MeasureString(New String("0"c, ItemString1.Length), cb.Font)
            g.Dispose()

            e.Graphics.DrawString(Convert.ToString(ItemString1), e.Font, b, e.Bounds.X + 10, e.Bounds.Y)

            bLineX = sf.Width + 20
            e.Graphics.DrawLine(p, bLineX, e.Bounds.Top, bLineX, e.Bounds.Bottom)

            e.Graphics.DrawString(Convert.ToString(ItemString2), e.Font, b, bLineX, e.Bounds.Y)

            Return

        Catch ex As Exception

        End Try


    End Sub


#End Region

End Class
