
Imports T.R.ZCommonClass.clsDataGridSearchControl
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsDataGridEditTextBox.typValueType
Imports System.Reflection
Imports T.R.ZCommonClass.clsDataGridEditTextBox

''' <summary>
''' DataGridView操作クラス
''' </summary>
Public Class clsDataGrid

#Region "メンバ"

#Region "プライベート"

#Region "データグリッド関連"
  Private WithEvents _DataGridView As DataGridView

  ''' <summary>
  ''' データテーブル
  ''' </summary>
  Private _GridSrc As DataTable = New DataTable

  ''' <summary>
  ''' バインディングソース
  ''' </summary>
  Private _BdSrc As BindingSource = New BindingSource

  ''' <summary>
  ''' ソート設定
  ''' </summary>
  Private _SortSetting As Dictionary(Of String, String)

  ''' <summary>
  ''' 並び順設定（昇順）
  ''' </summary>
  Private _OrderUpSetting As Dictionary(Of String, String)

  ''' <summary>
  ''' 並び順設定（降順）
  ''' </summary>
  Private _OrderDownSetting As Dictionary(Of String, String)

  ''' <summary>
  ''' 並び順向き選択
  ''' </summary>
  Private _OrderDirection As Dictionary(Of String, Integer)

  ''' <summary>
  ''' ソート有効・無効フラグ
  ''' </summary>
  Private _SortActive As Boolean = True

  ''' <summary>
  ''' コンテキストメニュー
  ''' </summary>
  Private _ContextMenu As ContextMenuStrip
#End Region

#Region "データベース関連"
  ''' <summary>
  ''' データベース接続オブジェクト
  ''' </summary>
  Private _SqlCon As clsComDatabase

  ''' <summary>
  ''' 一覧表示用SQL文
  ''' </summary>
  Private _SrcSql As String

#End Region

#Region "動作設定関連"
  ''' <summary>
  ''' 一覧抽出コントロールリスト
  ''' </summary>
  Private _SearchConditionz As New List(Of clsDataGridSearchControl)

  ''' <summary>
  ''' 編集コントロール保持
  ''' </summary>
  Private WithEvents _DgvTextBoxEditingControl As DataGridViewTextBoxEditingControl

  ''' <summary>
  ''' 編集可能コントロールリスト
  ''' </summary>
  Private _EditColumnList As New List(Of clsDataGridEditTextBox)

  ''' <summary>
  ''' 一覧レイアウト
  ''' </summary>
  Private _ColumnList As New List(Of clsDGVColumnSetting)

  ''' <summary>
  ''' 自動検索実行フラグ
  ''' </summary>
  ''' <remarks>
  '''  Trueにすると、検索条件コントロールが更新される度に自動で一覧表示を更新する
  ''' </remarks>
  Private _AutoSearch As Boolean = False

  ''' <summary>
  ''' 固定列数
  ''' </summary>
  Private _FixedRow As Integer = -1

  ''' <summary>
  ''' メッセージ出力ラベル
  ''' </summary>
  Private _msgLabel As Label = Nothing

  ''' <summary>
  ''' 出力メッセージ
  ''' </summary>
  Private _msgLabelText As String = String.Empty

#End Region

#Region "更新処理関連"

  ''' <summary>
  ''' 最終更新列名称
  ''' </summary>
  Private _LastEditColSrc As String = String.Empty

  ''' <summary>
  ''' データ更新処理実行フラグ
  ''' </summary>
  ''' <remarks>
  ''' データ更新後の再表示判断に使用
  '''  True  - データ更新処理が行われた
  '''  False - データ更新処理は行われていない
  ''' </remarks>
  Private _DataUpDate As Boolean = False

  ''' <summary>
  ''' 最終データ取得日時
  ''' </summary>
  Private _lastUpdate As Date = Now

#End Region

#Region "初期化フラグ"

  ''' <summary>
  ''' レイアウト初期化フラグ
  ''' </summary>
  Private _bLayOuted As Boolean = False

  ''' <summary>
  ''' 編集コントロール初期化フラグ
  ''' </summary>
  Private _bSetEditColum As Boolean = False

#End Region

#End Region

#Region "パブリック"
  'Delegate Function CallBackCreateGridSrcSql() As String
  'Public lcCallBackCreateGridSrcSql As CallBackCreateGridSrcSql

  'Delegate Function CallBackCreateGridLayout() As List(Of clsDGVColumnSetting)
  'Public lcCallBackCreateGridLayout As CallBackCreateGridLayout

  ' ダブルクリックイベント
  Delegate Sub CallBackCellDoubleClick(sender As Object, e As DataGridViewCellEventArgs)
  Public lcCallBackCellDoubleClick As CallBackCellDoubleClick

  ' データ表示イベント
  Delegate Sub CallBackReLoadData(sender As DataGridView, LastUpdate As String, DataCount As Long, DataJuryo As Decimal, DataKingaku As Decimal)
  Public lcCallBackReLoadData As CallBackReLoadData

  ' ユーザー操作によるデータグリッド編集エラー発生時イベント
  Delegate Sub CallBackValidatingFailed(sender As Object, e As Exception)
  Public lcCallBackValidatingFailed As CallBackValidatingFailed
#End Region

#Region "セレクター関連"
  ''' <summary>
  ''' 選択されたリスト
  ''' </summary>
  Private _SelectedList As New List(Of String)

  ''' <summary>
  ''' セレクターオブジェクト
  ''' </summary>
  Private _GridSelecter As clsDataGridSelecter

#End Region

#End Region

#Region "プロパティー"

#Region "プライベート"
  Private ReadOnly Property CurrentColumnHeaderText() As String
    Get
      With _DataGridView
        Return .Columns(.CurrentCell.ColumnIndex).HeaderText
      End With
    End Get
  End Property

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <returns></returns>
  Private ReadOnly Property IsInitialized() As Boolean
    Get
      Return Not _SrcSql.Equals(String.Empty) AndAlso _SqlCon IsNot Nothing
    End Get
  End Property

  Private Property ColumnList() As List(Of clsDGVColumnSetting)
    Get
      Return _ColumnList
    End Get
    Set(value As List(Of clsDGVColumnSetting))
      _ColumnList = value
    End Set
  End Property

#End Region

#Region "パブリック"

  ''' <summary>
  ''' 一覧表示用SQL
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks>一覧表示用のSQL文を直接編集</remarks>
  Public Property SrcSql As String
    Get
      Return _SrcSql
    End Get
    Set(value As String)
      _SrcSql = value
      If IsInitialized AndAlso _AutoSearch Then
        ShowList()
      End If
    End Set
  End Property

  ''' <summary>
  ''' 検索処理自動実行フラグ
  ''' </summary>
  ''' <returns>フラグ</returns>
  ''' <remarks>
  '''   True  : SQL文が設定変更されたら即時Gridに反映する
  '''   False : SQL文が設定変更されてもGridに反映しない
  ''' </remarks>
  Public Property AutoSearch As Boolean
    Get
      Return _AutoSearch
    End Get
    Set(value As Boolean)
      _AutoSearch = value
      ' フラグ設定時に初期化済かつ設定された値がTrueなら検索処理実行
      If IsInitialized AndAlso _AutoSearch Then
        ShowList()
      End If
    End Set
  End Property

  Public ReadOnly Property SelectedRow() As Dictionary(Of String, String)
    Get
      Dim ret As New Dictionary(Of String, String)

      ' データグリッド上でCTRLキーを押しながらクリックすると落ちる不具合対応
      Dim RowIdx As Integer = 0
      If _DataGridView.SelectedCells.Count > 0 Then
        RowIdx = _DataGridView.SelectedCells(0).RowIndex
      Else
        RowIdx = 0
      End If

      For Each tmpCel As DataGridViewCell In _DataGridView.Rows(RowIdx).Cells
        ret.Add(_DataGridView.Columns(tmpCel.ColumnIndex).Name, tmpCel.Value.ToString())
      Next
      Return ret
    End Get
  End Property

  Public WriteOnly Property SqlCon As clsComDatabase
    Set(value As clsComDatabase)
      _SqlCon = value
      If IsInitialized AndAlso _AutoSearch Then
        ShowList()
      End If
    End Set
  End Property

  Public ReadOnly Property EditData() As Dictionary(Of String, String)
    Get
      Dim tmpEt As clsDataGridEditTextBox = GetEditColBySrcName(_LastEditColSrc)
      Dim tmpEditText As String = _DgvTextBoxEditingControl.Text

      ' 数値型のカラムはカンマを削除
      If tmpEt.ValueType = typValueType.VT_NUMBER _
         OrElse tmpEt.ValueType = typValueType.VT_SIGNED_NUMBER Then
        tmpEditText = tmpEditText.Replace(",", "")
        tmpEditText = Long.Parse(tmpEditText).ToString()
      End If

      Return New Dictionary(Of String, String) From {{_LastEditColSrc, tmpEditText}}
    End Get
  End Property

  ' 編集可能列設定
  Public Property EditColumnList() As List(Of clsDataGridEditTextBox)
    Get
      Return _EditColumnList
    End Get
    Set(value As List(Of clsDataGridEditTextBox))
      _EditColumnList = value
      SetEditColum()
    End Set
  End Property

  ' 固定列設定
  Public Property FixedRow() As Integer
    Get
      Return _FixedRow
    End Get
    Set(value As Integer)
      _FixedRow = value
      If _DataGridView.ColumnCount > 0 Then
        ' 列を固定する
        _DataGridView.Columns(ColumnList(value).DataSrc).Frozen = True
        ' 境界線の太さを変更する
        _DataGridView.Columns(ColumnList(value).DataSrc).DividerWidth = 3
      End If
    End Set
  End Property

  ''' <summary>
  ''' 最終更新日時取得
  ''' </summary>
  ''' <returns>最終更新日時</returns>
  Public ReadOnly Property LastUpdate() As Date
    Get
      Return Now
    End Get
  End Property

  ''' <summary>
  ''' データ行数取得
  ''' </summary>
  ''' <returns>データ行数</returns>
  Public ReadOnly Property DataCount() As Long
    Get
      Return _DataGridView.Rows.Count
    End Get
  End Property

  ''' <summary>
  ''' 重量合計取得
  ''' </summary>
  ''' <returns>データ行数</returns>
  Public ReadOnly Property DataJuryo() As Decimal
    Get
      Dim JuryoSum As Decimal = 0
      If _DataGridView.Columns.Contains("JYURYOK") Then
        For Each DataRow As DataGridViewRow In _DataGridView.Rows
          JuryoSum += CDec(DataRow.Cells(_DataGridView.Columns("JYURYOK").Index).Value)
        Next

      End If
      Return Math.Round(JuryoSum, 1)
    End Get
  End Property

  ''' <summary>
  ''' 重量合計取得
  ''' </summary>
  ''' <returns>データ行数</returns>
  Public ReadOnly Property DataKingaku() As Decimal
    Get
      Dim KingakuSum As Decimal = 0
      If _DataGridView.Columns.Contains("KINGAKUW") Then
        For Each DataRow As DataGridViewRow In _DataGridView.Rows
          KingakuSum += CDec(DataRow.Cells(_DataGridView.Columns("KINGAKUW").Index).Value)
        Next
      End If
      Return Math.Round(KingakuSum)
    End Get
  End Property

  ''' <summary>
  ''' ソート設定
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks>
  '''  Keyに設定された列をクリックすると、Valueに設定された値でソートする
  '''  Key = 対象列
  '''  Value = ソートする値
  ''' </remarks>
  Public ReadOnly Property SortSetting As Dictionary(Of String, String)
    Get
      Return _SortSetting
    End Get
  End Property

  ''' <summary>
  ''' ソート有効・無効設定　!!!注意!!! 1行もデータが無い場合ソート有効無効が設定されません
  ''' </summary>
  ''' <returns>ソート有効・無効状態</returns>
  Public Property SortActive() As Boolean
    Get
      Return _SortActive
    End Get
    Set(value As Boolean)
      _SortActive = value
      If _SortActive = False Then
        ' 並び替えができないようにする
        For Each c As DataGridViewColumn In _DataGridView.Columns
          c.SortMode = DataGridViewColumnSortMode.NotSortable
        Next c
      Else
        ' 並び替えができるようにする
        For Each c As DataGridViewColumn In _DataGridView.Columns
          c.SortMode = DataGridViewColumnSortMode.Automatic
        Next c
      End If
    End Set
  End Property

  ''' <summary>
  ''' 選択行数を取得する
  ''' </summary>
  ''' <returns>選択行数</returns>
  ''' <remarks>ダブルクリックでチェックを付けた行数を返す</remarks>
  Public ReadOnly Property SelectCount As Long
    Get
      If _SelectedList Is Nothing Then
        Return 0
      Else
        Dim ret As Long = 0

        For idx As Integer = 0 To _DataGridView.Rows.Count - 1
          If GetDataRow_ByDataGridViewRowIdx(idx)(_GridSelecter.DataSourcName).ToString() = _GridSelecter.SelectChar Then
            ret += 1
          End If
        Next

        Return ret
      End If
    End Get
  End Property
#End Region

#End Region

#Region "コンストラクタ"
  Public Sub New(prmDataGridView As DataGridView _
                 , prmGridSrcSql As String _
                 , prmGridLayout As List(Of clsDGVColumnSetting) _
                 , Optional prmGridSelecter As clsDataGridSelecter = Nothing)

    ' ソート設定初期化
    _SortSetting = New Dictionary(Of String, String)

    ' 並び順（昇順）初期化
    _OrderUpSetting = New Dictionary(Of String, String)

    ' 並び順（降順）初期化
    _OrderDownSetting = New Dictionary(Of String, String)

    ' 並び順向き初期化
    _OrderDirection = New Dictionary(Of String, Integer)

    ' DataGridViewを保持
    _DataGridView = prmDataGridView

    ' 表示用SQL文を保持
    _SrcSql = prmGridSrcSql

    ' 一覧表示レイアウトを保持
    _ColumnList = prmGridLayout

    ' 選択列追加
    If prmGridSelecter IsNot Nothing Then
      _GridSelecter = prmGridSelecter
      With prmGridSelecter
        _ColumnList.Insert(.ColIndex _
                           , New clsDGVColumnSetting(" " _
                                                     , .DataSourcName _
                                                     , argColumnWidth:= .ColumnWidth _
                                                     , argTextAlignment:=clsDGVColumnSetting.typAlignment.MiddleCenter))
      End With
    End If

    ' DataGridView初期化
    Call InitGrid()


    _ContextMenu = New ContextMenuStrip
    With _ContextMenu
      .Items.Add("コピー(&C)", Nothing, AddressOf ContextCopy)
    End With
    prmDataGridView.ContextMenuStrip = _ContextMenu
  End Sub
#End Region

#Region "メソッド"

#Region "プライベート"

  ' Grid初期化
  Private Sub InitGrid()
    _BdSrc.DataSource = _GridSrc
    _DataGridView.DataSource = _BdSrc
    'グリッドに初期を設定する
    With _DataGridView
      'デフォルトスタイルを無効にする
      .EnableHeadersVisualStyles = False
      '列ヘッダ
      .ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray
      'セル、行、列が複数選択されないようにする
      .MultiSelect = False
      '行の高さをユーザーが変更できないようにする
      .AllowUserToResizeRows = False
      '行ヘッダーを非表示にする
      .RowHeadersVisible = False
      'ユーザーが新しい行を追加できないようにする
      .AllowUserToAddRows = False
      ' ツールチップを非表示にする
      .ShowCellToolTips = False
      '--------------------------
      '   以下、高速化処理
      '--------------------------

      ' ダブルバッファリング有効
      .GetType().InvokeMember(
        "DoubleBuffered",
        BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.SetProperty,
        Nothing,
        _DataGridView,
        New Object() {True})

      ' バーチャルモード有効
      .VirtualMode = True

    End With
  End Sub

  ' 検索条件文字列の作成
  Private Function CreateConditionText() As String
    Dim sqlWhere As String = String.Empty

    For Each tmpSc As clsDataGridSearchControl In _SearchConditionz
      With tmpSc
        If Not .Value.Equals(String.Empty) Then
          sqlWhere &= .SearchItemName
          sqlWhere &= ComSearchType2Text(.SearchType)
          sqlWhere &= ComGetLiteralChar(.DataType, _SqlCon.Provider)
          Select Case .SearchType
            Case typExtraction.EX_LIK
              sqlWhere &= "%" & .Value & "%"
            Case typExtraction.EX_LIKB
              sqlWhere &= "%" & .Value
            Case typExtraction.EX_LIKF
              sqlWhere &= .Value & "%"
            Case Else
              sqlWhere &= .Value
          End Select
          sqlWhere &= ComGetLiteralChar(.DataType, _SqlCon.Provider)
          sqlWhere &= " AND "
        End If
      End With
    Next

    ' 最終の "AND" を削除
    If Not sqlWhere.Equals(String.Empty) Then
      sqlWhere = Mid(sqlWhere, 1, Len(sqlWhere) - Len("AND "))
    End If

    Return sqlWhere

  End Function

  ' 編集可能列の設定
  Private Sub SetEditColum()

    ' 編集可能列に設定されている項目がソースデータに存在しない場合はエラーを上げる
    For Each tmpEditTxtBox As clsDataGridEditTextBox In _EditColumnList
      Dim bFined As Boolean = False
      Dim tmpTitle As String = tmpEditTxtBox.TitleName
      For Each tmpColSetting As clsDGVColumnSetting In _ColumnList

        bFined = (bFined Or tmpColSetting.TitleCaption.Equals(tmpTitle))
      Next
      If bFined = False Then
        Throw New Exception("編集列:" & tmpTitle & "は一覧に存在しません。")
      End If
    Next

    ' データが表示されているなら編集可能列を設定する
    If _DataGridView.Rows.Count > 0 Then

      For Each tmpEc As clsDataGridEditTextBox In _EditColumnList
        For Each tmpDgvCc As DataGridViewColumn In _DataGridView.Columns
          If tmpDgvCc.HeaderText.Equals(tmpEc.TitleName) Then
            tmpDgvCc.ReadOnly = False
            Exit For
          End If
        Next
      Next

      _bSetEditColum = True

    End If


  End Sub

  Private Function TryGetEditColumn(ByRef prmEditColumn As clsDataGridEditTextBox) As Boolean
    Dim bRet As Boolean = False

    For Each tmpDgvEc As clsDataGridEditTextBox In _EditColumnList
      With tmpDgvEc
        If .TitleName.Equals(CurrentColumnHeaderText) Then
          prmEditColumn = tmpDgvEc
          bRet = True
          Exit For
        End If
      End With
    Next

    Return bRet
  End Function


  ' カラムスタイル設定
  Private Sub SetDefaultCellStyle(pDgvColumn As DataGridViewColumn _
                                  , pColSetting As clsDGVColumnSetting)
    With pDgvColumn
      .HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter           ' タイトルは中央固定
      .HeaderCell.Style.Font = New System.Drawing.Font(pColSetting.GetFontName, pColSetting.TitleFontSize)  ' フォント
      .Width = pColSetting.ColumnWidth  ' セル幅
      With .DefaultCellStyle
        .Font = New System.Drawing.Font(pColSetting.GetFontName, pColSetting.FontSize)  ' フォント
        .Alignment = pColSetting.GetTextAlignment                                       ' 文字配置
        If Not pColSetting.GetFormat.Equals(String.Empty) Then
          .Format = pColSetting.GetFormat                                               ' 書式  
        End If
      End With
    End With

  End Sub


  ''' <summary>  
  ''' DataGridViewのRowIndexからDataTableのDataRowを取得する  
  ''' </summary>  
  ''' <param name="RowIdx"></param>
  ''' <returns></returns>  
  ''' <remarks>
  ''' ソート時など、DataGridViewのRowIndexが必ずしもDataTableのRowIndexとマッチしないため
  ''' ※ ここからコピペ
  '''      → http://dd0125.blogspot.com/2011/02/vbnetdatagridviewdatarow.html
  ''' </remarks>  
  Private Function GetDataRow_ByDataGridViewRowIdx(ByVal RowIdx As Integer) As DataRow
    Try
      If _DataGridView.Rows(RowIdx).DataBoundItem Is Nothing Then
        Return Nothing
      End If
    Catch ex As IndexOutOfRangeException
      Return Nothing
    End Try

    Dim Dr As DataRow
    Dim Drv As DataRowView = CType(_DataGridView.Rows(RowIdx).DataBoundItem, System.Data.DataRowView)
    Dr = CType(Drv.Row, System.Data.DataRow)

    Return Dr
  End Function

  ''' <summary>
  ''' データソース名から表示位置のインデックスを取得する
  ''' </summary>
  ''' <returns>Colインデックス</returns>
  ''' <remarks>
  '''  DisplayIndexで並び替えを行っている為、表示位置とインデックスは一致しない場合があります
  ''' </remarks>
  Private Function GetColPosByDataSource(tmpDsName As String) As Integer
    Dim ret As Integer = -1

    For idx As Integer = 0 To _GridSrc.Columns.Count - 1
      If _DataGridView.Columns(idx).Name.Equals(tmpDsName) Then
        ret = idx
        Exit For
      End If
    Next

    Return ret
  End Function

  ''' <summary>
  ''' データソース名より編集カラムオブジェクトを取得する
  ''' </summary>
  ''' <param name="prmSrcName">データソース名</param>
  ''' <returns>編集カラムオブジェクト</returns>
  Private Function GetEditColBySrcName(prmSrcName As String) As clsDataGridEditTextBox
    Dim ret As clsDataGridEditTextBox = Nothing

    For Each tmpDgvColSetting As clsDGVColumnSetting In _ColumnList
      If tmpDgvColSetting.DataSrc = prmSrcName Then
        For Each tmpDgeTxtBox As clsDataGridEditTextBox In _EditColumnList
          If tmpDgeTxtBox.TitleName = tmpDgvColSetting.TitleCaption Then
            ret = tmpDgeTxtBox
          End If
        Next
      End If
    Next

    Return ret
  End Function

  ''' <summary>
  ''' 任意の条件で一覧の選択列に記号を表示する
  ''' </summary>
  ''' <param name="prmEqList">一致条件</param>
  ''' <param name="prmNEqList">不一致条件</param>
  ''' <param name="prmSelectChar">表示する記号</param>
  Private Function ChangeRowSelectMark(prmEqList As Dictionary(Of String, String) _
                            , prmNEqList As Dictionary(Of String, String) _
                            , prmSelectChar As String) As Long

    Dim tmpCnt As Long = 0

    ' 表示中のデータを最終までループ
    For idx As Integer = 0 To _DataGridView.Rows.Count - 1
      Dim tmpCurrentKey As String = String.Empty

      Dim tmpMuch As Boolean = True

      ' イコールリストと一致するか？
      For Each tmpKey As String In prmEqList.Keys
        With GetDataRow_ByDataGridViewRowIdx(idx)(tmpKey)
          If .ToString() <> prmEqList(tmpKey) Then

            tmpMuch = False
            Exit For
          End If
        End With
      Next

      ' ノットイコールリストと一致しないか？
      If tmpMuch AndAlso prmNEqList IsNot Nothing Then
        For Each tmpKey As String In prmEqList.Keys
          With GetDataRow_ByDataGridViewRowIdx(idx)(tmpKey)
            If .ToString() = prmEqList(tmpKey) Then
              tmpMuch = False
              Exit For
            End If
          End With
        Next
      End If

      If tmpMuch Then
        Dim tmpDr As DataRow = GetDataRow_ByDataGridViewRowIdx(idx)
        Dim tmpSelectedKey As String = String.Empty

        ' 選択マーク更新
        tmpDr(_GridSelecter.DataSourcName) = prmSelectChar
        For Each tmpKey As String In _GridSelecter.SelectKeyList
          tmpSelectedKey &= tmpDr(tmpKey).ToString & "<>"
        Next

        If prmSelectChar = _GridSelecter.SelectChar Then
          ' チェックON時
          If _SelectedList.Contains(tmpSelectedKey) = False Then
            _SelectedList.Add(tmpSelectedKey)       ' リストに追加
          End If
        Else
          ' チェックOFF時
          If _SelectedList.Contains(tmpSelectedKey) Then
            _SelectedList.Remove(tmpSelectedKey)    ' リストから削除
          End If
        End If

        tmpCnt += 1

      End If
    Next

    Return tmpCnt

  End Function
#End Region

#Region "パブリック"

  ''' <summary>
  ''' 選択リストクリア
  ''' </summary>
  Public Sub ClearSelectedList()

    '' 画面上の〇を消す
    If _GridSelecter IsNot Nothing Then

      ' リストクリア
      _SelectedList.Clear()

      With _GridSelecter

        ' 表示中のデータを最終までループ
        For idx As Integer = 0 To _DataGridView.Rows.Count - 1
          GetDataRow_ByDataGridViewRowIdx(idx)(.DataSourcName) = ""
        Next

      End With
    End If

  End Sub

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
  ''' 検索処理実行
  ''' </summary>
  ''' <remarks>
  ''' 自動検索フラグに影響をウケます
  ''' 抽出コントロール変更時コールバックに使用
  ''' </remarks>
  Public Sub ExecSearch()
    If AutoSearch Then
      ShowList()
    End If
  End Sub

  ''' <summary>
  ''' 一覧表示
  ''' </summary>
  ''' <param name="prmKeepCurrentPos"></param>
  Public Sub ShowList(Optional prmKeepCurrentPos As Boolean = False)

    ' 現在カーソル位置取得
    Dim CurrentPos As Point = Me._DataGridView.CurrentCellAddress

    ' 現在最上位セル取得
    Dim tmpTopRow As Integer = _DataGridView.FirstDisplayedScrollingRowIndex

    Me._DataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing


    ' 一覧表示更新
    _SqlCon.GetResult(_GridSrc, ComAddSqlSearchCondition(_SrcSql, CreateConditionText()))

    ' 選択列追加
    If _GridSelecter IsNot Nothing Then
      With _GridSelecter
        If _GridSrc.Columns.Contains(.DataSourcName) = False Then
          _GridSrc.Columns.Add(.DataSourcName)
        End If

        ' 表示中のデータを最終までループ
        For idx As Integer = 0 To _DataGridView.Rows.Count - 1
          Dim tmpCurrentKey As String = String.Empty


          ' 選択キー作成
          For Each tmpKey As String In .SelectKeyList
            tmpCurrentKey &= GetDataRow_ByDataGridViewRowIdx(idx)(tmpKey).ToString() & "<>"
          Next

          ' 選択キーが選択edリストに存在するか？
          If _SelectedList.Contains(tmpCurrentKey) = False Then
            ' 存在しない
            GetDataRow_ByDataGridViewRowIdx(idx)(.DataSourcName) = String.Empty
          Else
            ' 存在する
            Dim tmpEnableSelect As Boolean = True

            ' 選択可能条件が設定されているなら満たしているかチェック
            If .SelectingCondition IsNot Nothing Then
              For Each tmpKey As String In .SelectingCondition.Keys
                If GetDataRow_ByDataGridViewRowIdx(idx)(tmpKey).ToString() <> .SelectingCondition(tmpKey) Then
                  tmpEnableSelect = False
                  Exit For
                End If
              Next
            End If

            If tmpEnableSelect Then
              GetDataRow_ByDataGridViewRowIdx(idx)(.DataSourcName) = .SelectChar
            End If
          End If
        Next
      End With
    End If

    ' 最終更新日時更新
    _lastUpdate = CDate(ComGetProcTime())

    ' レイアウト変更
    If _bLayOuted = False Then SetLayout()

    ' 固定列設定
    If _FixedRow > 0 _
      AndAlso _DataGridView.Columns(ColumnList(_FixedRow).DataSrc).Frozen = False Then
      Me.FixedRow = _FixedRow
    End If

    ' 編集列設定
    If _bSetEditColum = False Then SetEditColum()

    ' データグリッドが０件の場合、処理を行わない
    ' カーソル位置復帰
    If (prmKeepCurrentPos) And (Me._DataGridView.Rows.Count >= 1) Then
      If CurrentPos.X >= 0 AndAlso CurrentPos.Y >= 0 Then
        Me._DataGridView.CurrentCell = Me._DataGridView(CurrentPos.X, CurrentPos.Y)
        _DataGridView.FirstDisplayedScrollingRowIndex = tmpTopRow
      End If
    End If

    If lcCallBackReLoadData IsNot Nothing Then
      Call lcCallBackReLoadData(_DataGridView, LastUpdate, DataCount, DataJuryo, DataKingaku)
    End If

  End Sub

  ''' <summary>
  ''' 一覧表示(並び替えが設定されている場合)
  ''' </summary>
  ''' <param name="orderSql"></param>

  Public Sub ShowList(orderSql As String)

    ' 現在カーソル位置取得
    Dim CurrentPos As Point = Me._DataGridView.CurrentCellAddress

    ' 現在最上位セル取得
    Dim tmpTopRow As Integer = _DataGridView.FirstDisplayedScrollingRowIndex

    Me._DataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing

    ' 一覧表示更新
    _SqlCon.GetResult(_GridSrc, ComAddSqlSearchCondition(_SrcSql, CreateConditionText(), orderSql))

    ' 選択列追加
    If _GridSelecter IsNot Nothing Then
      With _GridSelecter
        If _GridSrc.Columns.Contains(.DataSourcName) = False Then
          _GridSrc.Columns.Add(.DataSourcName)
        End If

        ' 表示中のデータを最終までループ
        For idx As Integer = 0 To _DataGridView.Rows.Count - 1
          Dim tmpCurrentKey As String = String.Empty


          ' 選択キー作成
          For Each tmpKey As String In .SelectKeyList
            tmpCurrentKey &= GetDataRow_ByDataGridViewRowIdx(idx)(tmpKey).ToString() & "<>"
          Next

          ' 選択キーが選択edリストに存在するか？
          If _SelectedList.Contains(tmpCurrentKey) = False Then
            ' 存在しない
            GetDataRow_ByDataGridViewRowIdx(idx)(.DataSourcName) = String.Empty
          Else
            ' 存在する
            Dim tmpEnableSelect As Boolean = True

            ' 選択可能条件が設定されているなら満たしているかチェック
            If .SelectingCondition IsNot Nothing Then
              For Each tmpKey As String In .SelectingCondition.Keys
                If GetDataRow_ByDataGridViewRowIdx(idx)(tmpKey).ToString() <> .SelectingCondition(tmpKey) Then
                  tmpEnableSelect = False
                  Exit For
                End If
              Next
            End If

            If tmpEnableSelect Then
              GetDataRow_ByDataGridViewRowIdx(idx)(.DataSourcName) = .SelectChar
            End If
          End If
        Next
      End With
    End If

    ' 最終更新日時更新
    _lastUpdate = CDate(ComGetProcTime())

    ' レイアウト変更
    If _bLayOuted = False Then SetLayout()

    ' 固定列設定
    If _FixedRow > 0 _
      AndAlso _DataGridView.Columns(ColumnList(_FixedRow).DataSrc).Frozen = False Then
      Me.FixedRow = _FixedRow
    End If

    ' 編集列設定
    If _bSetEditColum = False Then SetEditColum()

    ' データグリッドが０件の場合、処理を行わない
    ' カーソル位置復帰
    If (Me._DataGridView.Rows.Count >= 1) Then
      If CurrentPos.X >= 0 AndAlso CurrentPos.Y >= 0 Then
        Me._DataGridView.CurrentCell = Me._DataGridView(CurrentPos.X, CurrentPos.Y)
        _DataGridView.FirstDisplayedScrollingRowIndex = tmpTopRow
      End If
    End If

    If lcCallBackReLoadData IsNot Nothing Then
      Call lcCallBackReLoadData(_DataGridView, LastUpdate, DataCount, DataJuryo, DataKingaku)
    End If

  End Sub

  ' レイアウト設定
  Public Sub SetLayout()
    ' 表示対象を配置
    'ColumnList = lcCallBackCreateGridLayout()

    _bLayOuted = True

    ' 表示対象を表示する
    For idx = 0 To _ColumnList.Count - 1
      For Each tmpCol As DataGridViewColumn In _DataGridView.Columns
        If tmpCol.Name.Equals(_ColumnList(idx).DataSrc) Then
          tmpCol.HeaderText = _ColumnList(idx).TitleCaption
          tmpCol.DisplayIndex = idx
          Exit For
        End If
      Next
    Next

    ' 表示対象外を非表示にする
    For Each tmpCol As DataGridViewColumn In _DataGridView.Columns
      tmpCol.Visible = False
      tmpCol.ReadOnly = True
      For idx = 0 To _ColumnList.Count - 1
        If tmpCol.HeaderText.Equals(_ColumnList(idx).TitleCaption) Then
          tmpCol.Visible = True                                               ' 表示
          SetDefaultCellStyle(tmpCol, _ColumnList(idx))                       ' スタイル設定
          Exit For
        End If
      Next
    Next


  End Sub

  ' 検索条件コントロール追加
  Public Sub AddSearchControl(prmCtrl As Control _
                              , prmSearchItemName As String _
                              , prmSearchType As typExtraction _
                              , prmDataType As typColumnKind)

    Dim tmpSc As clsDataGridSearchControl

    If IsComboBox(prmCtrl) Then
      tmpSc = New clsDataGridSearchCmb
    ElseIf IsTextBox(prmCtrl) Then
      tmpSc = New clsDataGridSearchTextBox
    Else
      Throw New Exception("")
    End If

    With tmpSc
      .DataType = prmDataType
      .SearchItemName = prmSearchItemName
      .SearchType = prmSearchType
      .TargetControl = prmCtrl
      .mCallBack = Sub() ExecSearch()
    End With

    Call _SearchConditionz.Add(tmpSc)

  End Sub

  Public Sub ClearSearchCondition(Optional prmExclusionControls As List(Of Control) = Nothing)
    For Each tmpSc In _SearchConditionz
      If prmExclusionControls Is Nothing _
          OrElse prmExclusionControls.Contains(tmpSc.TargetControl) = False Then
        tmpSc.ClearLastText()
      End If
    Next
  End Sub

  ''' <summary>
  ''' 表示中データ全件取得
  ''' </summary>
  ''' <returns>表示中のデータ</returns>
  ''' <remarks>
  '''   List[]([DB項目名],[値])形式で返す
  ''' </remarks>
  Public Function GetAllData() As List(Of Dictionary(Of String, String))
    Dim ret As New List(Of Dictionary(Of String, String))

    For idx As Integer = 0 To _DataGridView.Rows.Count - 1
      Dim tmpRowData As New Dictionary(Of String, String)
      For Each tmpCel As DataGridViewCell In _DataGridView.Rows(idx).Cells
        tmpRowData.Add(_DataGridView.Columns(tmpCel.ColumnIndex).Name, tmpCel.Value.ToString())
      Next
      ret.Add(tmpRowData)
    Next

    Return ret
  End Function

  ''' <summary>
  ''' ソートを解除する
  ''' </summary>
  Public Sub InitSort()
    _BdSrc.Sort = ""
  End Sub

  ''' <summary>
  ''' カーソル（スクロール）位置を左上に設定
  ''' </summary>
  Public Sub ResetPosition()
    With _DataGridView
      If .Rows.Count > 0 Then
        Dim tmpVisible As Boolean = _DataGridView.Visible
        .Visible = True
        .FirstDisplayedScrollingRowIndex = 0

        ' 非固定の先頭列をスクロールに先頭に設定
        .FirstDisplayedScrollingColumnIndex = GetColPosByDataSource(_ColumnList(_FixedRow + 1).DataSrc)

        .Visible = tmpVisible
      End If
    End With
  End Sub

  ''' <summary>
  ''' 任意のセルに任意の値をセットする
  ''' </summary>
  ''' <param name="prmSrcName">値をセットするセルのデータソース名</param>
  ''' <param name="prmRowPos">値をセットするセルの行Index</param>
  ''' <param name="prmSetVal">セットする値</param>
  ''' <remarks>
  '''  データグリッドのデータソースにセットするだけでDB上の値は更新されません
  ''' </remarks>
  Public Sub SetCellVal(prmSrcName As String, prmRowPos As Integer, prmSetVal As String)
    GetDataRow_ByDataGridViewRowIdx(prmRowPos)(prmSrcName) = prmSetVal
  End Sub

  ''' <summary>
  ''' ソート設定追加
  ''' </summary>
  ''' <param name="prmTitleName">列タイトル</param>
  ''' <param name="prmSortKey">ソートキー</param>
  Public Sub AddSortKey(prmTitleName As String, prmSortKey As String)
    If _SortSetting.ContainsKey(prmTitleName) Then
      _SortSetting(prmTitleName) = prmSortKey
    Else
      _SortSetting.Add(prmTitleName, prmSortKey)
    End If
  End Sub

  ''' <summary>
  ''' 並び順設定追加
  ''' </summary>
  ''' <param name="prmTitleName">列タイトル</param>
  ''' <param name="prmOrderUpKey">並び順（昇順）キー</param>
  ''' <param name="prmOrderDownKey">並び順（降順）キー</param>
  Public Sub AddOrderKey(prmTitleName As String, prmOrderUpKey As String, Optional prmOrderDownKey As String = "")

    ' 並び順（昇順）設定
    If _OrderUpSetting.ContainsKey(prmTitleName) Then
      _OrderUpSetting(prmTitleName) = prmOrderUpKey
    Else
      _OrderUpSetting.Add(prmTitleName, prmOrderUpKey)
    End If

    ' 並び順（降順）設定
    If (Not String.IsNullOrEmpty(prmOrderDownKey)) Then
      If _OrderDownSetting.ContainsKey(prmTitleName) Then
        _OrderDownSetting(prmTitleName) = prmOrderDownKey
      Else
        _OrderDownSetting.Add(prmTitleName, prmOrderDownKey)
      End If
      '並び順の向き初期化
      If _OrderDirection.ContainsKey(prmTitleName) Then
        _OrderDirection(prmTitleName) = 1
      Else
        _OrderDirection.Add(prmTitleName, 1)

      End If
    Else
      '並び順の向き初期化
      If _OrderDirection.ContainsKey(prmTitleName) Then
        _OrderDirection(prmTitleName) = 0
      Else
        _OrderDirection.Add(prmTitleName, 0)
      End If
    End If

  End Sub

  ''' <summary>
  ''' 任意の条件で一覧の選択状態を解除する
  ''' </summary>
  ''' <param name="prmEqList">一致条件</param>
  ''' <param name="prmNEqList">不一致条件</param>
  Public Function UnSetRowSelectMark(prmEqList As Dictionary(Of String, String) _
                            , Optional prmNEqList As Dictionary(Of String, String) = Nothing) As Long
    Return ChangeRowSelectMark(prmEqList, prmNEqList, "")
  End Function

  ''' <summary>
  ''' 任意の条件で一覧を選択状態にする
  ''' </summary>
  ''' <param name="prmEqList">一致条件</param>
  ''' <param name="prmNEqList">不一致条件</param>
  Public Function SetRowSelectMark(prmEqList As Dictionary(Of String, String) _
                            , Optional prmNEqList As Dictionary(Of String, String) = Nothing) As Long
    Return ChangeRowSelectMark(prmEqList, prmNEqList, _GridSelecter.SelectChar)
  End Function



#End Region

#End Region

#Region "イベントプロシージャー"

  ''' <summary>
  ''' データグリッドダブルクリック時イベント
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles _DataGridView.CellDoubleClick
    Dim tmpDgv As DataGridView = DirectCast(sender, DataGridView)


    ' 選択キー追加・削除
    If _GridSelecter IsNot Nothing Then
      'If e.RowIndex >= 0 _
      '  AndAlso e.ColumnIndex = (_GridSrc.Columns.Count - 1) Then
      If e.RowIndex >= 0 Then

        ' 選択キー作成
        Dim tmpSelectedKey As String = String.Empty
        Dim tmpGridSrc As DataRow = GetDataRow_ByDataGridViewRowIdx(e.RowIndex)

        Dim tmpEnableSelect As Boolean = True

        ' 選択可能条件が設定されている場合は、条件に一致するかチェック
        With _GridSelecter
          If .SelectingCondition IsNot Nothing Then
            For Each tmpKey As String In .SelectingCondition.Keys
              If tmpGridSrc(tmpKey).ToString() <> .SelectingCondition(tmpKey) Then
                tmpEnableSelect = False
                Exit For
              End If
            Next
          End If
        End With

        If tmpEnableSelect Then
          For Each tmpKey As String In _GridSelecter.SelectKeyList
            tmpSelectedKey &= tmpGridSrc(tmpKey).ToString & "<>"
          Next

          ' 選択edリストに存在するか？
          With _SelectedList
            If .Contains(tmpSelectedKey) Then
              ' 存在する
              .Remove(tmpSelectedKey)   ' リストから削除
            Else
              ' 存在しない
              .Add(tmpSelectedKey)      ' リストに追加
            End If
          End With

          Me.ShowList(True)
        End If
      End If

    End If

      ' コールバックが設定されているかつタイトル以外をダブルクリック
      If Me.lcCallBackCellDoubleClick IsNot Nothing _
      AndAlso e.RowIndex >= 0 Then
      Call lcCallBackCellDoubleClick(sender, e)
    End If
  End Sub

  ''' <summary>
  ''' エンターキー押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DgvKeyDown(sender As Object, e As KeyEventArgs) Handles _DataGridView.KeyDown

    ' セルダブルクリックと同一処理
    If e.KeyCode = Keys.Enter Then
      If Me.lcCallBackCellDoubleClick IsNot Nothing Then
        Call lcCallBackCellDoubleClick(sender, Nothing)
        e.Handled = True
      End If
    End If
  End Sub

  ''' <summary>
  ''' データグリッドフォーカス取得時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DGVGotFocus(sender As Object, e As EventArgs) Handles _DataGridView.GotFocus

    'メッセージラベルの定義が未設定の場合
    If _msgLabel Is Nothing Then
      Exit Sub
    Else
      'メッセージラベルへのメッセージの表示
      _msgLabel.Text = _msgLabelText
    End If

  End Sub

  ''' <summary>
  ''' データグリッドビュータイトルクリック時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DgvColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles _DataGridView.ColumnHeaderMouseClick
    Static SortDirection As New Dictionary(Of String, SortOrder)
    Dim tmpDg As DataGridView = DirectCast(sender, DataGridView)
    Dim tmpColumn As DataGridViewColumn = Nothing
    Dim tmpHeaderTxt As String

    ' ソート処理無効の場合は処理を抜ける
    If _SortActive = False Then
      Exit Sub
    End If

    ' 右クリックは処理を抜ける
    If e.Button = MouseButtons.Right Then
      Exit Sub
    End If

    ' クリックされた行タイトル取得
    tmpHeaderTxt = tmpDg.Columns(e.ColumnIndex).HeaderCell.Value

    If _OrderUpSetting.ContainsKey(tmpHeaderTxt) Then

      InitSort()

      ' 並び替えが設定されている
      ' 設定されている値で並び替え
      Dim tmpOrder As String = String.Empty
      Select Case _OrderDirection(tmpHeaderTxt)
        Case 0
          ' 並び順（昇順）の取得
          tmpOrder = _OrderUpSetting(tmpHeaderTxt)
        Case 1
          ' 並び順の向き変更
          _OrderDirection(tmpHeaderTxt) = 2
          ' 並び順（昇順）の取得
          tmpOrder = _OrderUpSetting(tmpHeaderTxt)
        Case 2
          ' 並び順の向き変更
          _OrderDirection(tmpHeaderTxt) = 1
          ' 並び順（降順）の取得
          tmpOrder = _OrderDownSetting(tmpHeaderTxt)
      End Select

      ShowList(tmpOrder)

    Else

        If _SortSetting.ContainsKey(tmpHeaderTxt) Then
        ' 並び替えが設定されている
        ' 設定されている値で並び替え
        tmpColumn = tmpDg.Columns(GetColPosByDataSource(_SortSetting(tmpHeaderTxt)))
      Else
        ' 並び替えが設定されていない
        ' タイトル行のデータソースで並び替え
        tmpColumn = tmpDg.Columns(e.ColumnIndex)
      End If

      ' 並び替え方向取得
      If SortDirection.ContainsKey(tmpHeaderTxt) = False Then
        ' 初回並び替えなら昇順
        SortDirection.Add(tmpHeaderTxt, System.ComponentModel.ListSortDirection.Ascending)
      Else
        ' 二回目以降はトグル
        If SortDirection(tmpHeaderTxt) = System.ComponentModel.ListSortDirection.Ascending Then
          SortDirection(tmpHeaderTxt) = System.ComponentModel.ListSortDirection.Descending
        Else
          SortDirection(tmpHeaderTxt) = System.ComponentModel.ListSortDirection.Ascending
        End If

      End If

      ' 並び替え処理実行
      tmpDg.Sort(tmpColumn, SortDirection(tmpHeaderTxt))

    End If

  End Sub

  ''' <summary>
  ''' コンテキストメニュー（コピー）選択時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub ContextCopy(sender As Object, e As System.EventArgs)
    Clipboard.SetText(_DataGridView.CurrentCell.FormattedValue().ToString())
  End Sub

  ''' <summary>
  ''' コンテキストメニュー要求時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub CellContextMenuStripNeeded(ByVal sender As Object _
                                                      , ByVal e As DataGridViewCellContextMenuStripNeededEventArgs) Handles _DataGridView.CellContextMenuStripNeeded
    Dim dgv As DataGridView = CType(sender, DataGridView)
    If e.RowIndex < 0 Then
      ' 行ヘッダークリック時はメニュー非表示
      _DataGridView.ContextMenuStrip = Nothing
    Else
      _DataGridView.ContextMenuStrip = _ContextMenu
    End If
  End Sub

  ''' <summary>
  ''' グリッドセルクリック時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub CellMouseDown(ByVal sender As Object _
                          , ByVal e As DataGridViewCellMouseEventArgs) Handles _DataGridView.CellMouseDown

    '右クリック時のみ処理を実行します。
    If e.Button = Windows.Forms.MouseButtons.Right Then
      ' クリックされたセルと選択中のセルが異なる場合はクリックされたセルを選択状態に
      If _DataGridView.CurrentCell Is Nothing OrElse
          Not _DataGridView.CurrentCell.Equals(_DataGridView(e.ColumnIndex, e.RowIndex)) Then

        Me._DataGridView.ClearSelection()
        _DataGridView.CurrentCell = _DataGridView(e.ColumnIndex, e.RowIndex)

      End If
    End If

  End Sub

#End Region

#Region "テスト中"
  Private Sub EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles _DataGridView.EditingControlShowing
    '表示されているコントロールがDataGridViewTextBoxEditingControlか調べる
    If TypeOf e.Control Is DataGridViewTextBoxEditingControl Then

      _DgvTextBoxEditingControl = DirectCast(e.Control, DataGridViewTextBoxEditingControl)

      _LastEditColSrc _
            = _DataGridView.Columns(DirectCast(sender, DataGridView).SelectedCells(0).ColumnIndex).Name

      Dim tmpDgvEc As clsDataGridEditTextBox = Nothing
      If TryGetEditColumn(tmpDgvEc) Then

        ' 値の種別に合わせて入力時イベントを設定
        ' イベント解除は[CellValidating]で実行
        Select Case tmpDgvEc.ValueType
          Case VT_NUMBER
            ' 数値入力
            AddHandler _DgvTextBoxEditingControl.KeyPress, AddressOf DGVNumberEditingControlKeyPress
          Case VT_SIGNED_NUMBER
            ' 数値入力（符号有り）
            AddHandler _DgvTextBoxEditingControl.KeyPress, AddressOf DGVSignedNumberEditingControlKeyPress
          Case VT_DATE
            ' 日付入力
            AddHandler _DgvTextBoxEditingControl.KeyPress, AddressOf DGVDateEditingControlKeyPress
          Case Else
        End Select

      End If
    End If

  End Sub

  '''' <summary>
  '''' 編集可能セル更新時処理
  '''' </summary>
  '''' <param name="sender"></param>
  '''' <param name="e"></param>
  '''' <remarks>
  '''' _DataGridView.CellValidatingと本関数の両方にカンマ削除処理を追加しないとエラーで落ちます
  '''' </remarks>
  'Private Sub EditingControlValidating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles _DgvTextBoxEditingControl.Validating
  '  Dim tmpDgvEc As clsDataGridEditTextBox = Nothing

  '  If TryGetEditColumn(tmpDgvEc) Then
  '    ' 編集列が数値の場合、カンマを削除する
  '    If tmpDgvEc.ValueType = typValueType.VT_NUMBER _
  '          OrElse tmpDgvEc.ValueType = typValueType.VT_SIGNED_NUMBER Then
  '      _DgvTextBoxEditingControl.Text = _DgvTextBoxEditingControl.Text.Replace(",", "")
  '    End If
  '  End If

  'End Sub


  Private Sub CellValidating(sender As Object, e As DataGridViewCellValidatingEventArgs) Handles _DataGridView.CellValidating
    Dim dgv As DataGridView = DirectCast(sender, DataGridView)
    If e.RowIndex = dgv.NewRowIndex OrElse Not dgv.IsCurrentCellDirty Then
      Exit Sub
    End If

    Dim tmpDgvEc As clsDataGridEditTextBox = Nothing

    If TryGetEditColumn(tmpDgvEc) Then
      With tmpDgvEc

        Try
          Select Case .ValueType
            Case VT_NUMBER
              ' 編集列が数値の場合、カンマを削除する
              _DgvTextBoxEditingControl.Text = _DgvTextBoxEditingControl.Text.Replace(",", "")
              ' 入力制限解除
              RemoveHandler _DgvTextBoxEditingControl.KeyPress, AddressOf DGVNumberEditingControlKeyPress
              If String.IsNullOrWhiteSpace(_DgvTextBoxEditingControl.Text) Then
                _DgvTextBoxEditingControl.Text = "0"
              End If
            Case VT_SIGNED_NUMBER
              ' 編集列が数値の場合、カンマを削除する
              _DgvTextBoxEditingControl.Text = _DgvTextBoxEditingControl.Text.Replace(",", "")
              If String.IsNullOrWhiteSpace(_DgvTextBoxEditingControl.Text) Then
                _DgvTextBoxEditingControl.Text = "0"
              End If
              ' 入力制限解除
              RemoveHandler _DgvTextBoxEditingControl.KeyPress, AddressOf DGVSignedNumberEditingControlKeyPress
            Case VT_DATE
              ' 編集列が日付の場合、日付型にフォーマット
              _DgvTextBoxEditingControl.Text = ComCreateDateText(_DgvTextBoxEditingControl.Text)
              ' 入力制限解除
              RemoveHandler _DgvTextBoxEditingControl.KeyPress, AddressOf DGVDateEditingControlKeyPress
          End Select
        Catch ex As Exception
          '失敗
          e.Cancel = True
          _DataUpDate = False
          If lcCallBackValidatingFailed IsNot Nothing Then
            Call lcCallBackValidatingFailed(sender, ex)
          End If
          Exit Sub
        End Try

        If .lcCallBackUpDate IsNot Nothing Then
          ' 更新処理実行
          _DataUpDate = True

          Try
            If .lcCallBackUpDate() = False Then
              '失敗
              e.Cancel = True
              _DataUpDate = False
            End If
          Catch ex As Exception
            e.Cancel = True
            _DataUpDate = False
          End Try

        End If
      End With
    End If

  End Sub

  ''' <summary>
  ''' バリデート完了時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub CellValidated(sender As Object, e As DataGridViewCellEventArgs) Handles _DataGridView.CellValidated
    '再表示
    Dim tmpDgvEc As clsDataGridEditTextBox = Nothing

    If TryGetEditColumn(tmpDgvEc) Then
      ' CellValidatingでデータ更新に成功かつ更新後再表示が有効の場合、再表示を行う
      If _DataUpDate AndAlso tmpDgvEc.IsReload Then
        _DataUpDate = False
        Me.ShowList(True)
      End If
    End If

  End Sub

  ''' <summary>
  ''' 数値入力コントロールキー入力時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>
  ''' 数値とバックスペースのみ入力可
  ''' </remarks>
  Private Sub DGVNumberEditingControlKeyPress(sender As Object, e As KeyPressEventArgs)
    Dim tmpDgvEc As clsDataGridEditTextBox = Nothing

    If TryGetEditColumn(tmpDgvEc) Then

      ' 入力可能最大文字数が設定されている場合は最大文字数までの入力を可能とする
      If sender.Text.Length >= tmpDgvEc.MaxChar _
          AndAlso tmpDgvEc.MaxChar > 0 _
          AndAlso sender.SelectedText.Length < tmpDgvEc.MaxChar Then
        If e.KeyChar <> ControlChars.Back Then
          e.Handled = True
        End If
      Else
        If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) _
        AndAlso e.KeyChar <> ControlChars.Back Then
          e.Handled = True
        End If
      End If

    End If

  End Sub

  ''' <summary>
  ''' 数値入力コントロールキー入力時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>
  ''' 数値・バックスペース・マイナス符号（先頭位置のみ）のみ入力可
  ''' </remarks>
  Private Sub DGVSignedNumberEditingControlKeyPress(sender As Object, e As KeyPressEventArgs)
    Dim tmpDgvEc As clsDataGridEditTextBox = Nothing

    If TryGetEditColumn(tmpDgvEc) Then
      ' 入力可能最大文字数が設定されている場合は最大文字数までの入力を可能とする
      If sender.Text.Replace("-", "").Length >= tmpDgvEc.MaxChar _
          AndAlso tmpDgvEc.MaxChar > 0 _
          AndAlso sender.SelectedText.Length < tmpDgvEc.MaxChar Then
        If e.KeyChar <> ControlChars.Back Then
          e.Handled = True
        End If
      Else
        If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) _
          AndAlso e.KeyChar <> ControlChars.Back _
              AndAlso (sender.SelectionStart <> 0 OrElse e.KeyChar <> "-"c) Then
          e.Handled = True
        End If
      End If
    End If

  End Sub

  ''' <summary>
  ''' 日付入力コントロール入力時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DGVDateEditingControlKeyPress(sender As Object, e As KeyPressEventArgs)
    If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) _
      AndAlso e.KeyChar <> "/"c _
      AndAlso e.KeyChar <> ControlChars.Back Then
      e.Handled = True
    End If
  End Sub
#End Region

#Region "インナークラス"
  Private Class CustomDGV
    Inherits DataGridView

  End Class
#End Region
End Class
