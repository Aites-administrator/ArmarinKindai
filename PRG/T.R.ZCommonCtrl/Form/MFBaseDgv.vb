Imports T.R.ZCommonClass

Public Class MFBaseDgv

#Region "メンバ"

#Region "パブリック"

  ' データグリッド保持用
  Public Controlz As Dictionary(Of String, clsDataGrid)

#End Region

#End Region

#Region "メソッド"

#Region "パブリック"

  ''' <summary>
  ''' Grid初期化
  ''' </summary>
  ''' <param name="prmDgv">初期化対象のDatagridvidw</param>
  ''' <param name="prmGridSrcSql">一覧表示内容（SQL文）</param>
  ''' <param name="prmGridLayout">一覧表示レイアウト</param>
  ''' <param name="prmGridSelecter">行セレクター設定オブジェクト</param>
  Public Sub InitGrid(prmDgv As DataGridView _
                   , prmGridSrcSql As String _
                   , prmGridLayout As List(Of clsDGVColumnSetting) _
                   , Optional prmGridSelecter As clsDataGridSelecter = Nothing)

    Dim tmpDataGrid As clsDataGrid = Nothing

    If Controlz.ContainsKey(prmDgv.Name) Then
      ' 二回目の初期化に対応してません

    Else
      tmpDataGrid = New clsDataGrid(prmDgv, prmGridSrcSql, prmGridLayout, prmGridSelecter)
      Call Controlz.Add(prmDgv.Name, tmpDataGrid)
      With tmpDataGrid
        .SqlCon = New clsSqlServer
      End With
    End If

  End Sub


  ''' <summary>
  ''' 画面上の全てのコントロールにメッセージラベルを設定
  ''' </summary>
  ''' <param name="prmMsglbl">メッセージを表示するラベル</param>
  ''' <remarks>
  '''  clsDataGridが対象
  ''' </remarks>
  Public Overloads Sub SetMsgLbl(prmMsglbl As Label)

    MyBase.SetMsgLbl(prmMsglbl)

    ' clsDataGridにメッセージ表示オブジェクトを設定
    For Each tmpKey As String In Controlz.Keys
      Controlz(tmpKey).SetMsgLabel(prmMsglbl)
    Next

  End Sub

  Public Overloads Sub AllClear(Optional prmExclusionControls As List(Of Control) = Nothing)
    For Each tmpDgvName In Controlz.Keys
      Controlz(tmpDgvName).ClearSearchCondition(prmExclusionControls)
    Next
    MyBase.AllClear(prmExclusionControls)
  End Sub

#End Region

#End Region

#Region "イベントプロシージャー"

  ''' <summary>
  ''' フォームロード時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub BaseForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ' DataGridView保持用連想配列初期化
    Controlz = New Dictionary(Of String, clsDataGrid)
  End Sub

  ''' <summary>
  ''' キー押下時
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub BaseForm_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles MyBase.KeyDown
    'Control+Rの時再表示を行う
    If (e.Modifiers And Keys.Control) = Keys.Control And e.KeyCode = Keys.R Then
      For Each tmpDataGrid As clsDataGrid In Controlz.Values
        tmpDataGrid.ShowList()
      Next
    End If
  End Sub


#End Region

End Class