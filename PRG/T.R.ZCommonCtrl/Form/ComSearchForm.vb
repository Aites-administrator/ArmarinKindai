Imports T.R.ZCommonClass.DgvForm01
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsDGVColumnSetting
Imports T.R.ZCommonClass.clsDataGridSearchControl
Imports T.R.ZCommonClass.clsCommonFnc

Public Class ComSearchForm
  Implements IDgvForm01

#Region "メンバ"

#Region "プライベート"
  ''' <summary>
  ''' 一覧表示用SQL文
  ''' </summary>
  Private _sql As String = String.Empty

  ''' <summary>
  ''' 戻り値
  ''' </summary>
  ''' <remarks>
  ''' 以下の形式で選択されたデータを呼び出し元に返す
  '''   _retval("code","一覧で選択されたコード"
  '''         , "name","一覧で選択された名称")
  ''' </remarks>
  Private _retval As New Dictionary(Of String, String)

  ''' <summary>
  ''' 一覧タイトル名
  ''' </summary>
  Private _CodeName As String

  ''' <summary>
  ''' 一覧項目名
  ''' </summary>
  Private _ValueName As String

  ''' <summary>
  ''' フォームタイトル
  ''' </summary>
  Private _FormTitle As String

#End Region

#End Region



#Region "メソッド"

#Region "パブリック"

  ''' <summary>
  ''' サブフォーム起動処理
  ''' </summary>
  ''' <param name="prmCodeName">一覧タイトル（コード）</param>
  ''' <param name="prmValueName">一覧タイトル（データ）</param>
  ''' <param name="prmSql">一覧抽出用SQL文</param>
  ''' <param name="prmFormTitle">フォームタイトル</param>
  ''' <param name="prmItemCode">アイテムコード</param>
  ''' <returns>選択された値</returns>
  Public Function ShowSubForm(prmCodeName As String _
                              , prmValueName As String _
                              , prmSql As String _
                              , prmFormTitle As String _
                              , Optional prmItemCode As String = "") As Dictionary(Of String, String)

    _CodeName = prmCodeName
    _sql = prmSql
    _ValueName = prmValueName
    Me._FormTitle = prmFormTitle

    If prmItemCode = "" Then
      ' アイテムコード未指定時は一覧表示
      Me.ShowDialog()
    Else
      ' アイテムコード指定時は入力されたコードの名称を取得
      Call GetItemNameByCode(prmItemCode)
    End If

    ' 選択された値を返す
    Return _retval

  End Function

#End Region

#Region "プライベート"

  ''' <summary>
  ''' コードより名称を取得する
  ''' </summary>
  ''' <param name="prmItemCode">検索対象のコード</param>
  Private Sub GetItemNameByCode(prmItemCode As String)
    Dim tmpSql As New clsSqlServer
    Dim tmpDt As New DataTable

    Try
      Call tmpSql.GetResult(tmpDt, ComAddSqlSearchCondition(CreateGrid1Src1, "ListSrc.ItemCode = " & prmItemCode))

      If tmpDt.Rows.Count > 0 Then
        _retval.Add("ItemCode", prmItemCode)
        _retval.Add("ItemName", tmpDt.Rows(0)("ItemName").ToString())
      Else
        _retval.Add("ItemCode", prmItemCode)
        _retval.Add("ItemName", "対象のデータはありません")
      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("名称の取得に失敗しました")
    End Try

  End Sub
#End Region

#End Region

#Region "データグリッドビュー操作関連共通"

  '１つ目のDataGridViewオブジェクト格納先
  Private DG1V1 As DataGridView

  ''' <summary>
  ''' 初期化処理
  ''' </summary>
  ''' <remarks>
  ''' コントロールの初期化（Form_Loadで実行して下さい）
  ''' </remarks>
  Private Sub InitForm01() Implements IDgvForm01.InitForm

    '１つ目のDataGridViewオブジェクトの設定
    DG1V1 = Me.DataGridView1

    ' グリッド初期化
    Call InitGrid(DG1V1, CreateGrid1Src1(), CreateGridLayout())

    ' グリッド動作設定
    With DG1V1
      With Controlz(.Name)

        ' 検索コントロール設定
        .AddSearchControl(Me.TxtNumericBase1, "ItemCode", typExtraction.EX_EQ, typColumnKind.CK_Number)
        .AddSearchControl(Me.txtName, "ItemName", typExtraction.EX_LIK, typColumnKind.CK_Text)

        ' 編集可能列設定
        .EditColumnList = CreateGrid1EditCol1()
      End With
    End With
  End Sub

  ''' <summary>
  ''' 一覧表示データ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  ''' <remarks>
  '''   画面毎の抽出内容をここに記載する
  ''' </remarks>
  Private Function CreateGrid1Src1() As String Implements IDgvForm01.CreateGridSrc
    Dim sql As String = String.Empty

    sql &= " SELECT * "
    sql &= " FROM  (" & _sql & ") as ListSrc"
    sql &= " ORDER BY ListSrc.ItemCode "

    Return sql
  End Function


  ''' <summary>
  ''' 一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGridLayout() As List(Of clsDGVColumnSetting) Implements IDgvForm01.CreateGridlayout
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      .Add(New clsDGVColumnSetting(_CodeName, "ItemCode", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=120))
      .Add(New clsDGVColumnSetting(_ValueName, "ItemName", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=240))
    End With

    Return ret
  End Function

  ''' <summary>
  ''' 一覧表示編集可能カラム設定
  ''' </summary>
  ''' <returns>作成した編集可能カラム配列</returns>
  Public Function CreateGrid1EditCol1() As List(Of clsDataGridEditTextBox) Implements IDgvForm01.CreateGridEditCol
    Dim ret As New List(Of clsDataGridEditTextBox)

    With ret
      '.Add(New clsDataGridEditTextBox("タイトル", prmUpdateFnc:=AddressOf [更新時実行関数], prmValueType:=[データ型]))
    End With

    Return ret
  End Function

#End Region

#Region "イベントプロシージャー"

  ''' <summary>
  ''' 選択ボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
    _retval = Controlz(DG1V1.Name).SelectedRow
    Me.Close()
  End Sub

  ''' <summary>
  ''' 閉じるボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
    Me.Close()
  End Sub

  ''' <summary>
  ''' グリッドダブルクリック時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DgvDblClick(sender As Object, e As DataGridViewCellEventArgs)
    ' 選択行を戻り値に設定し本画面を閉じる
    _retval = Controlz(DG1V1.Name).SelectedRow
    Me.Close()
  End Sub

  ''' <summary>
  ''' フォームロード時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub ComSearchForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Me.MinimizeBox = False  ' 最小化ボタン消去
    Me.MaximizeBox = False  ' 最大化ボタン消去
    Me.ControlBox = False   ' コントロールボックス消去(= 閉じるボタン消去)  
    Me.Text = _FormTitle    ' フォームタイトル

    ' 画面初期化
    Call InitForm01()
    With Controlz(DG1V1.Name)
      .AutoSearch = True
      .lcCallBackCellDoubleClick = AddressOf DgvDblClick
    End With

  End Sub


  ''' <summary>
  ''' エンターキー押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub DgvPreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles DataGridView1.PreviewKeyDown

    If e.KeyCode = Keys.Enter Then
      ' 選択行を戻り値に設定し本画面を閉じる
      _retval = Controlz(DG1V1.Name).SelectedRow
      Me.Close()
    End If
  End Sub

#End Region

End Class
