Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.DgvForm01
Imports T.R.ZCommonClass.clsDataGridSearchControl
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsDGVColumnSetting
Imports T.R.ZCommonClass.clsGlobalData
Imports T.R.ZCommonClass.clsComDatabase

Public Class Form_OutConvLog
  Implements IDgvForm01

#Region "定数定義"
#Region "プライベート"
  Private Const PRG_ID As String = "OutConvLog"
  Private Const PRG_TITLE As String = "売上伝票作成ログ"
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
    Call InitGrid(DG1V1, CreateGrid1Src1(), CreateGrid1Layout1())

    ' グリッド動作設定
    With DG1V1

      With Controlz(.Name)

        ' 検索コントロール設定
        .AddSearchControl(Me.CmbMstCustomer1, "MTOKUISAKIC", typExtraction.EX_EQ, typColumnKind.CK_Number)

        ' 編集可能列設定
        .EditColumnList = CreateGrid1EditCol1()
      End With
    End With
  End Sub

  ''' <summary>
  ''' PCA送信ログ抽出SQL文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function CreateGrid1Src1() As String Implements IDgvForm01.CreateGridSrc
    Dim sql As String = String.Empty

    sql &= " SELECT *  "
    sql &= " FROM URIAGE "
    sql &= " WHERE [UPDATE] >= '" & Date.Parse(ComGetProcDate()).AddDays(-14).ToString("yyyy/MM/dd") & "'"
    sql &= "    OR FLG = 0  "
    sql &= " ORDER BY DENPYOUNO DESC "
    sql &= "        , GYOBAN "
    sql &= "        , MTOKUISAKIC "
    sql &= "        , MSYOHINC "

    Return sql
  End Function


  ''' <summary>
  ''' 一覧表示レイアウト設定作成
  ''' </summary>
  ''' <returns>作成したレイアウト</returns>
  Public Function CreateGrid1Layout1() As List(Of clsDGVColumnSetting) Implements IDgvForm01.CreateGridlayout
    Dim ret As New List(Of clsDGVColumnSetting)

    With ret
      .Add(New clsDGVColumnSetting("作成日", "UPDATE", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=160))
      .Add(New clsDGVColumnSetting("伝票No", "DENPYOUNO", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=80))
      .Add(New clsDGVColumnSetting("伝票日付", "URIAGEBI", argTextAlignment:=typAlignment.MiddleCenter, argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("得意先C", "MTOKUISAKIC", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=100))
      .Add(New clsDGVColumnSetting("得意先名", "TOKUISAKIMEI", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=210))
      .Add(New clsDGVColumnSetting("枝", "SKOUMOKU2", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=60))
      .Add(New clsDGVColumnSetting("商品コード", "SYOHINC", argTextAlignment:=typAlignment.MiddleRight, argColumnWidth:=120))
      .Add(New clsDGVColumnSetting("商品名", "HINMEI", argTextAlignment:=typAlignment.MiddleLeft, argColumnWidth:=210))
      .Add(New clsDGVColumnSetting("重量", "SURYO", argTextAlignment:=typAlignment.MiddleRight, argFormat:="##0.0", argColumnWidth:=70))
      .Add(New clsDGVColumnSetting("単価", "TANKA", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,##0", argColumnWidth:=70))
      .Add(New clsDGVColumnSetting("売上金額", "KINGAKU", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,##0", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("原価額", "GENKAGAKU", argTextAlignment:=typAlignment.MiddleRight, argFormat:="#,##0", argColumnWidth:=90))
      .Add(New clsDGVColumnSetting("個体識別番号", "BIKOU", argTextAlignment:=typAlignment.MiddleCenter, argFormat:="0000000000"))
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

#Region "メソッド"

#Region "プライベート"

  ''' <summary>
  ''' 本日送信件数を画面に表示する
  ''' </summary>
  Private Sub DspPlanedCoutn()
    Dim tmpDb As New clsSqlServer
    Dim tmpDt As New DataTable

    Try
      Dim tmpPlanedCount As String = String.Empty
      tmpDb.GetResult(tmpDt, SqlSelPlanedCount())
      If tmpDt.Rows.Count <= 0 Then
        tmpPlanedCount = "0"
      Else
        tmpPlanedCount = tmpDt.Rows.Count.ToString()
      End If
      Me.lblPostCount.Text = "本日作成される伝票件数は" & tmpPlanedCount & "件です。（" & ComGetProcTime() & " 現在)"
    Catch ex As Exception
      ComWriteErrLog(ex)
      Throw New Exception("送信予定件数の取得に失敗しました。")
    Finally
      tmpDb.Dispose()
      tmpDt.Dispose()
    End Try

  End Sub

  ''' <summary>
  ''' URIAGEテーブルより売上日が本日日付の伝票件数を取得するSQL文の作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelPlanedCount() As String
    Dim sql As String = String.Empty

    sql &= " SELECT DISTINCT DENPYOUNO "
    sql &= " FROM URIAGE "
    sql &= " WHERE FORMAT([UPDATE],'yyyy/MM/dd') = '" & ComGetProcDate() & "'"

    Return sql
  End Function

#End Region

#End Region

#Region "イベントプロシージャー"

#Region "フォーム関連"

  ''' <summary>
  ''' フォームロード時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub From_OutConvLog_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    Me.Text = PRG_TITLE

    Call InitForm01()

    With Controlz(DG1V1.Name)
      .AutoSearch = True
    End With

    MyBase.SetMsgLbl(Me.lblInformation)
    Me.ActiveControl = Me.CmbMstCustomer1

    Call DspPlanedCoutn()
  End Sub

  ''' <summary>
  ''' フォームキー押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>アクセスキー対応</remarks>
  Private Sub Form_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    Select Case e.KeyCode
      Case Keys.F12
        ' 戻る
        With btnEnd
          .Focus()
          .PerformClick()
        End With
    End Select
  End Sub

#End Region

#Region "ボタン関連"

  ''' <summary>
  ''' 戻るボタン押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub btnEnd_Click(sender As Object, e As EventArgs) Handles btnEnd.Click
    Me.Close()
  End Sub

#End Region

#End Region

End Class
