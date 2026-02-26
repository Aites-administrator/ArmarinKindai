Imports System.ComponentModel
Imports Common
Imports Common.ClsFunction
Imports T.R.ZCommonCtrl

Public Class ItemAddForm
  Inherits FormBase
#Region "定数定義"
  ''' <summary>
  ''' プログラムタイトル
  ''' </summary>
  Private Const PRG_TITLE As String = "明細追加画面"
  ''' <summary>
  ''' 定貫
  ''' </summary>
  Private Const TEI_KAN As String = "1"
  ''' <summary>
  ''' 不定貫
  ''' </summary>
  Private Const HU_TEI_KAN As String = "0"
#End Region

#Region "変数定義"
  ''' <summary>
  ''' 実績一覧フォーム
  ''' </summary>
  Private Shadows parentForm As Form_ResultList

  ''' <summary>
  ''' 実績一覧のデータグリッド
  ''' </summary>
  Private DataGridView2 As New DataGridView

  ''' <summary>
  ''' 得意先コンボボックス
  ''' </summary>
  Private lastCmbMst As String = String.Empty

  ''' <summary>
  ''' SQLサーバー操作オブジェクト
  ''' </summary>
  Private _SqlServer As ClsSqlServer

  ''' <summary>
  ''' フォーム内のコントロール
  ''' </summary>
  Private originalValues As New Dictionary(Of String, String)
#End Region

#Region "コンストラクタ"
  ''' <summary>
  ''' 親フォームを連携するためのコンストラクタ
  ''' </summary>
  ''' <param name="parent">親フォーム</param>
  ''' <returns></returns>
  Public Sub New(parent As Form_ResultList)

    ' この呼び出しはデザイナーで必要です。
    InitializeComponent()
    '親フォームを取得
    parentForm = parent

    ' InitializeComponent() 呼び出しの後で初期化を追加します。

  End Sub

  ''' <summary>
  ''' SQLサーバー操作オブジェクト
  ''' </summary>
  Private ReadOnly Property SqlServer As ClsSqlServer
    Get
      If _SqlServer Is Nothing Then
        _SqlServer = New ClsSqlServer
      End If
      Return _SqlServer
    End Get
  End Property

#End Region

#Region "イベントプロシージャ"

#Region "フォーム関連"
  ''' <summary>
  ''' フォームロード時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub Form_ItemAdd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    ' フォームの最大化ボタンを無効にする
    MaximizeBox = False

    '商品コード設定値
    If (lastCmbMst = String.Empty) Then
      lastCmbMst = CmbMstItem1.Text
    Else
      CmbMstItem1.Text = lastCmbMst
      If String.IsNullOrWhiteSpace(TxtItemName.Text) Then
      CmbMstItemValidating(CmbMstItem1, TxtItemName, TxtItemNameKana)
    End If
    End If

    '定貫フラグを設定値
    CmbTeikan.SelectedIndex = CInt(TxtKeiryoFlg.Text)

    ' アセンブリの最終更新日時を取得し、フォームのタイトルに表示するテキストを設定
    Dim updateTime As DateTime = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location)
    Text = PRG_TITLE & " ( " & updateTime & " ) "

    If String.IsNullOrWhiteSpace(Me.TxtGyoNo.Text) Then
      Me.BtnDelGyo.Enabled = False
    End If

    ' 各コントロールの初期値を保存
    For Each ctrl In Me.Controls
      If TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is ComboBox Then
        originalValues(ctrl.Name) = ctrl.Text
      End If
    Next

  End Sub

  Private Sub TxtCustomerCd_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
    Me.ActiveControl = Me.CmbMstItem1
  End Sub



  '仮チェック
  Private Function IsModified() As Boolean
    For Each ctrl In Me.Controls
      If TypeOf ctrl Is TextBox OrElse TypeOf ctrl Is ComboBox Then
        Dim currentText = ctrl.Text
        If originalValues.ContainsKey(ctrl.Name) Then
          If originalValues(ctrl.Name) <> currentText Then
            Return True ' 変更あり
          End If
        End If
      End If
    Next
    Return False ' すべて元のまま
  End Function

  ''' <summary>
  ''' フォームキー押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>アクセスキー対応</remarks>
  Private Sub Form_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    Select Case e.KeyCode
      Case Keys.F1
        ' 明細追加
        ClickAddGyoButton()

      Case Keys.F3
        ' 明細削除
        ClickDelGyoButton()

      Case Keys.F10
        ' 終了
        MeClose()
    End Select

  End Sub

#End Region

#Region "テキストボックス関連"
  ''' <summary>
  ''' 納品数テキストボックス検証イベント
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtNohinSuryo_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles TxtHuteikanNohinSuryo.Validating, TxtHuteikanTanka.Validating, TxtTeikanNohinSuryo.Validating, TxtTeikanTanka.Validating
    '金額計算処理
    KingakuCalc()
  End Sub

  ''' <summary>
  ''' テキストボックス検証イベント
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtBiko_Validating(sender As Object, e As CancelEventArgs) Handles TxtKotai1.Validating, TxtKotai2.Validating, TxtKotai3.Validating
    Dim tb As TextBox = CType(sender, TextBox)
    If String.IsNullOrWhiteSpace(tb.Text) Then
      Exit Sub
    End If

    If Not ComChkKotaiNo(tb.Text) Then
      ComMessageBox("正しい個体識別番号を入力してください。", PRG_TITLE, typMsgBox.MSG_WARNING)
      e.Cancel = True  ' フォーカスを移動させない
    End If
  End Sub

  ''' <summary>
  ''' テキストボックス検証イベント
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TxtBox_Validating(sender As Object, e As CancelEventArgs) Handles TxtItemName.Validating, TxtItemNameKana.Validating, TxtKotai2.Validating, TxtKotai3.Validating
    Dim tb As TextBox = CType(sender, TextBox)
    If tb.Text.Contains("'") Then
      tb.Text = tb.Text.Replace("'", "")
    End If

    If tb.Name = "TxtItemName" AndAlso
      String.IsNullOrWhiteSpace(tb.Text) Then
      CmbMstItemValidating(CmbMstItem1, TxtItemName, TxtItemNameKana)

    End If

  End Sub


#End Region

#Region "コンボボックス関連"
  ''' <summary>
  ''' 商品マスタコンボボックス検証イベント
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub CmbMstItem1_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmbMstItem1.Validating
    '商品名表示更新処理
    If CmbMstItemValidating(CmbMstItem1, TxtItemName, TxtItemNameKana) Then
      e.Cancel = True
    End If

  End Sub

  ''' <summary>
  ''' 定貫コンボボックス検証イベント
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub CmbTeikan_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbTeikan.SelectedIndexChanged

    '不定貫のとき
    If Me.CmbTeikan.SelectedIndex = HU_TEI_KAN Then
      TxtHuteikanKosu.Enabled = True
      TxtHuteikanNohinSuryo.Enabled = True
      TxtHuteikanTani.Enabled = True
      TxtHuteikanTanka.Enabled = True
      TxtHuteikanKingaku.Enabled = True
      TxtTeikanNohinSuryo.Enabled = False
      TxtTeikanTani.Enabled = False
      TxtTeikanTanka.Enabled = False
      TxtTeikanKingaku.Enabled = False
    Else
      TxtHuteikanKosu.Enabled = False
      TxtHuteikanNohinSuryo.Enabled = False
      TxtHuteikanTani.Enabled = False
      TxtHuteikanTanka.Enabled = False
      TxtHuteikanKingaku.Enabled = False
      TxtTeikanNohinSuryo.Enabled = True
      TxtTeikanTani.Enabled = True
      TxtTeikanTanka.Enabled = True
      TxtTeikanKingaku.Enabled = True

    End If
    '金額計算処理
    KingakuCalc()
    '選択
    TxtKeiryoFlg.Text = CmbTeikan.SelectedIndex

  End Sub

  ''' <summary>
  ''' 商品が異なる場合、更新
  ''' </summary>
  ''' <param name="prmCmbMst"></param>
  ''' <param name="prmTxtLabel"></param>
  Private Function CmbMstItemValidating(prmCmbMst As ComboBox, prmTxtLabel As TextBox, prmTxtKanaLabel As TextBox)
    Dim rtn As Boolean = False
    If (lastCmbMst.Equals(prmCmbMst.Text) = False) Then
      lastCmbMst = prmCmbMst.Text
    End If

    If String.IsNullOrWhiteSpace(prmCmbMst.Text) Then
      ' 商品コード
      prmCmbMst.Text = String.Empty
      prmTxtLabel.Text = String.Empty
    Else
      Dim tmpDt As New DataTable
      Dim tmpTSDt As New DataTable
      Dim tmpTanka As String = String.Empty
      Dim tmpTani As String = String.Empty
      If (GetShohinCD(prmCmbMst.Text, tmpDt)) Then

        tmpTanka = tmpDt.Rows(0)("Baika").ToString
        tmpTani = tmpDt.Rows(0)("Tani").ToString
        If GetTokuisakiShohinCD(TxtCustCd.Text, prmCmbMst.Text, tmpTSDt) Then
          tmpTanka = If(CmbTeikan.SelectedIndex = HU_TEI_KAN, tmpTSDt.Rows(0)("Baika").ToString, tmpTSDt.Rows(0)("Tanka").ToString)
          tmpTani = tmpTSDt.Rows(0)("Tani").ToString
        End If

        ' 商品コード
        prmCmbMst.Text = tmpDt.Rows(0)("Code").ToString
        prmTxtLabel.Text = tmpDt.Rows(0)("Name").ToString
        prmTxtKanaLabel.Text = tmpDt.Rows(0)("KanaName").ToString
        If CmbTeikan.SelectedIndex = HU_TEI_KAN Then
          Me.TxtHuteikanTanka.Text = If(String.IsNullOrWhiteSpace(Me.TxtHuteikanTanka.Text) Or Me.TxtHuteikanTanka.Text = "0", tmpTanka, Me.TxtHuteikanTanka.Text)
          Me.TxtHuteikanTani.Text = If(String.IsNullOrWhiteSpace(Me.TxtHuteikanTani.Text), tmpTani, Me.TxtHuteikanTani.Text)
        Else
          Me.TxtTeikanTanka.Text = If(String.IsNullOrWhiteSpace(Me.TxtTeikanTanka.Text) Or Me.TxtTeikanTanka.Text = "0", tmpTanka, Me.TxtTeikanTanka.Text)
          Me.TxtTeikanTani.Text = If(String.IsNullOrWhiteSpace(Me.TxtTeikanTani.Text), tmpTani, Me.TxtTeikanTani.Text)
        End If

        Me.TxtTeikanNohinSuryo.Text = If(Me.TxtTeikanNohinSuryo.Text = "0", tmpDt.Rows(0)("Irisu").ToString, Me.TxtTeikanNohinSuryo.Text)
        Me.TxtHuteikanTani.Text = If(Me.TxtHuteikanTani.Text = "", tmpDt.Rows(0)("Tani").ToString, Me.TxtHuteikanTani.Text)
      Else
        ComMessageBox("商品が存在しません。" _
                        , PRG_TITLE _
                        , typMsgBox.MSG_WARNING _
                        , typMsgBoxButton.BUTTON_OK)
        rtn = True
      End If
    End If

    Return rtn
  End Function

  ''' <summary>
  ''' 商品マスタ検索処理
  ''' </summary>
  ''' <param name="prmCode"></param>
  ''' <param name="prmDt"></param>
  ''' <returns></returns>
  Private Function GetShohinCD(prmCode As String,
                                   ByRef prmDt As DataTable) As Boolean


    Dim ret As Boolean = True
    Dim tmpDb As New ClsSqlServer

    ' 実行
    With tmpDb
      Try

        ' SQL実行結果が指定した件数か？
        Call tmpDb.GetResult(prmDt, SqlGetShohinCD(prmCode))
        If (prmDt.Rows.Count = 0) Then
          ret = False
        End If

      Catch ex As Exception
        ' Error
        Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）
        ret = False
      End Try

    End With

    Return ret


  End Function

  ''' <summary>
  ''' 得意先商品マスタ検索処理
  ''' </summary>
  ''' <param name="prmCode"></param>
  ''' <param name="prmDt"></param>
  ''' <returns></returns>
  Private Function GetTokuisakiShohinCD(prmTCode As String, prmSCode As String,
                                   ByRef prmDt As DataTable) As Boolean


    Dim ret As Boolean = True
    Dim tmpDb As New ClsSqlServer

    ' 実行
    With tmpDb
      Try

        ' SQL実行結果が指定した件数か？
        Call tmpDb.GetResult(prmDt, SqlGetTokuisakiShohinCD(prmTCode, prmSCode))
        If (prmDt.Rows.Count = 0) Then
          ret = False
        End If

      Catch ex As Exception
        ' Error
        Call ComWriteErrLog(ex, False)   ' Error出力（＋画面表示）
        ret = False
      End Try

    End With

    Return ret


  End Function


#End Region

#Region "ボタン関連"
  ''' <summary>
  ''' 明細登録ボタン押下イベント
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub BtnAddGyo_Click(sender As Object, e As EventArgs) Handles BtnAddGyo.Click
    ClickAddGyoButton()
  End Sub

  ''' <summary>
  ''' 明細行削除ボタン押下イベント
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub BtnDelGyo_Click(sender As Object, e As EventArgs) Handles BtnDelGyo.Click
    ClickDelGyoButton()
  End Sub

  ''' <summary>
  ''' 終了ボタン押下イベント
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
    '終了処理
    MeClose()
  End Sub

  ''' <summary>
  ''' 終了処理
  ''' </summary>
  ''' <returns></returns>
  Private Sub MeClose()
    'データクリア処理
    DataClear()
    parentForm.Enabled = True
    '終了処理
    Me.Close()
  End Sub


#End Region

#Region "画面項目操作"
  ''' <summary>
  ''' 明細追加ボタン押下処理
  ''' </summary>
  ''' <returns></returns>
  Private Sub ClickAddGyoButton()
    '実績一覧フォームを設定
    Dim DataGridView1 As DataGridView = parentForm.DataGridView1
    '新規明細追加フラグ
    Dim tmpAddGyoFlg As Boolean = True

    '入力チェック
    If Not CheckInput() Then
      Exit Sub
    End If

    '行追加判定処理 (行番号が空のとき行追加とする)
    If Me.TxtGyoNo.Text <> "" Then
      For Each tmpDataGridRow As DataGridViewRow In DataGridView1.Rows
        If (tmpDataGridRow.Cells("行No").Value = Me.TxtGyoNo.Text) Then
          tmpDataGridRow.Cells("行No").Value = Me.TxtGyoNo.Text
          tmpDataGridRow.Cells("商品コード").Value = Me.CmbMstItem1.Text
          tmpDataGridRow.Cells("商品名").Value = Me.TxtItemName.Text
          tmpDataGridRow.Cells("個数").Value = If(Me.TxtKeiryoFlg.Text = HU_TEI_KAN, Me.TxtHuteikanKosu.Text, Me.TxtTeikanNohinSuryo.Text)
          tmpDataGridRow.Cells("定貫タイプ").Value = Me.TxtKeiryoFlg.Text
          tmpDataGridRow.Cells("単位").Value = If(Me.TxtKeiryoFlg.Text = HU_TEI_KAN, Me.TxtHuteikanTani.Text, Me.TxtTeikanTani.Text)
          tmpDataGridRow.Cells("数量").Value = If(Me.TxtKeiryoFlg.Text = HU_TEI_KAN, Me.TxtHuteikanNohinSuryo.Text, "")
          tmpDataGridRow.Cells("単価").Value = If(Me.TxtKeiryoFlg.Text = HU_TEI_KAN, Me.TxtHuteikanTanka.Text, Me.TxtTeikanTanka.Text)
          tmpDataGridRow.Cells("金額").Value = If(Me.TxtKeiryoFlg.Text = HU_TEI_KAN, Me.TxtHuteikanKingaku.Text, Me.TxtTeikanKingaku.Text)
          tmpDataGridRow.Cells("個体識別番号１").Value = Me.TxtKotai1.Text
          tmpDataGridRow.Cells("個体識別番号２").Value = Me.TxtKotai2.Text
          tmpDataGridRow.Cells("個体識別番号３").Value = Me.TxtKotai3.Text
          tmpAddGyoFlg = False
        End If
      Next
    End If

    '新規明細追加時
    If tmpAddGyoFlg Then

      If DataGridView1.Columns.Count = 0 Then
        'カラム追加処理
        SetColumnAdd(DataGridView1)
      End If
      'データグリッドに行追加
      DataGridView1.Rows.Add()

      '新規追加行に値を設定
      With DataGridView1.Rows(DataGridView1.Rows.Count - 1)
        .Cells("行No").Value = CInt(DataGridView1.Rows(DataGridView1.Rows.Count - 2).Cells("行No").Value) + 1
        .Cells("商品コード").Value = Me.CmbMstItem1.Text
        .Cells("商品名").Value = Me.TxtItemName.Text
        .Cells("個数").Value = If(CmbTeikan.SelectedIndex = TEI_KAN, If(String.IsNullOrWhiteSpace(Me.TxtTeikanNohinSuryo.Text), "0", Me.TxtTeikanNohinSuryo.Text), If(String.IsNullOrWhiteSpace(Me.TxtHuteikanKosu.Text), "0", Me.TxtHuteikanKosu.Text))
        .Cells("定貫タイプ").Value = Me.TxtKeiryoFlg.Text
        .Cells("単位").Value = Me.TxtHuteikanTani.Text
        .Cells("数量").Value = If(CmbTeikan.SelectedIndex = HU_TEI_KAN, If(String.IsNullOrWhiteSpace(Me.TxtHuteikanNohinSuryo.Text), "0", Me.TxtHuteikanNohinSuryo.Text), "")
        .Cells("単価").Value = If(String.IsNullOrWhiteSpace(Me.TxtHuteikanTanka.Text), "0", Me.TxtHuteikanTanka.Text)
        .Cells("金額").Value = Me.TxtHuteikanKingaku.Text
        .Cells("個体識別番号１").Value = Me.TxtKotai1.Text
        .Cells("個体識別番号２").Value = Me.TxtKotai2.Text
        .Cells("個体識別番号３").Value = Me.TxtKotai3.Text
      End With

    End If

    '編集チェック
    parentForm.FlgUpdate = IsModified()

    '終了処理
    MeClose()
  End Sub

  Private Sub ClickDelGyoButton()
    '実績一覧フォームを設定
    Dim DataGridView1 As DataGridView = If(DataGridView2 Is Nothing, parentForm.DataGridView1, DataGridView2)

    '明細行から除外する
    DataGridView1.Rows.RemoveAt(Integer.Parse(Me.TxtGyoNo.Text) - 1)

    '行Noの振り直し
    For Each tmpRow As DataGridViewRow In DataGridView1.Rows
      If Integer.Parse(tmpRow.Cells("行No").Value) >= Integer.Parse(Me.TxtGyoNo.Text) Then
        tmpRow.Cells("行No").Value = Integer.Parse(tmpRow.Cells("行No").Value) - 1
      End If
    Next

    '編集チェック
    parentForm.FlgUpdate = True

    MeClose()
  End Sub

  ''' <summary>
  ''' 金額計算処理
  ''' </summary>
  ''' <returns></returns>
  Private Sub KingakuCalc()
    Dim tmpTanka As String = If(CmbTeikan.SelectedIndex = HU_TEI_KAN, Me.TxtHuteikanTanka.Text, Me.TxtTeikanTanka.Text)
    Dim tmpSuryo As String = If(CmbTeikan.SelectedIndex = HU_TEI_KAN, Me.TxtHuteikanNohinSuryo.Text, Me.TxtTeikanNohinSuryo.Text)



    Dim strKingaku As String = String.Empty
    Dim tmpCalcTanka As String = If(String.IsNullOrWhiteSpace(tmpTanka), "0", tmpTanka)
    Dim tmpNohinsu As String = If(String.IsNullOrWhiteSpace(tmpSuryo), "0", tmpSuryo)

    If CmbTeikan.SelectedIndex = HU_TEI_KAN Then
      strKingaku = Fix(Decimal.Parse(tmpCalcTanka) * Decimal.Parse(tmpNohinsu) / 100).ToString()
      Me.TxtHuteikanKingaku.Text = strKingaku
    Else
      strKingaku = Fix(Decimal.Parse(tmpCalcTanka) * Decimal.Parse(tmpNohinsu)).ToString()
      Me.TxtTeikanKingaku.Text = strKingaku
    End If

  End Sub

  ''' <summary>
  ''' 入力チェック
  ''' </summary>
  ''' <returns></returns>
  Private Function CheckInput() As Boolean
    Dim rtn As Boolean = True
    If String.IsNullOrWhiteSpace(CmbMstItem1.Text) Then
      ComMessageBox("商品を選択してください。", PRG_TITLE, typMsgBox.MSG_WARNING)
      CmbMstItem1.Focus()
      rtn = False
      Return rtn
      Exit Function
    End If

    If CmbTeikan.SelectedIndex = TEI_KAN AndAlso
      String.IsNullOrWhiteSpace(TxtTeikanNohinSuryo.Text) Then
      ComMessageBox("個数を入力してください。", PRG_TITLE, typMsgBox.MSG_WARNING)
      TxtTeikanNohinSuryo.Focus()
      rtn = False
      Return rtn
      Exit Function
    End If

    If CmbTeikan.SelectedIndex = HU_TEI_KAN AndAlso
      String.IsNullOrWhiteSpace(TxtHuteikanNohinSuryo.Text) Then
      ComMessageBox("数量を入力してください。", PRG_TITLE, typMsgBox.MSG_WARNING)
      TxtHuteikanNohinSuryo.Focus()
      rtn = False
      Return rtn
      Exit Function
    End If

    Return rtn
  End Function

  ''' <summary>
  ''' データクリア処理
  ''' </summary>
  ''' <returns></returns>
  Public Sub DataClear()

    '表示列値設定
    Me.TxtGyoNo.Text = ""
    Me.CmbMstItem1.Text = ""
    CmbMstItemValidating(CmbMstItem1, TxtItemName, TxtItemNameKana)
    Me.TxtItemNameKana.Text = ""
    Me.TxtTeikanNohinSuryo.Text = ""
    Me.TxtHuteikanNohinSuryo.Text = ""
    Me.TxtHuteikanTani.Text = ""
    Me.TxtHuteikanTanka.Text = ""
    Me.TxtHuteikanKingaku.Text = ""
    Me.TxtKotai1.Text = ""
    Me.TxtKotai2.Text = ""
    Me.TxtKotai3.Text = ""

  End Sub

  ''' <summary>
  ''' 商品表示処理
  ''' </summary>
  ''' <param name="prmDenNo">検索項目　伝票番号</param>
  ''' <param name="prmGyoNo">検索項目　　行番号</param>
  ''' <param name="prmDataGridView">実績一覧フォーム　データグリッド</param>
  ''' <returns></returns>
  Public Sub DispItem(prmDenNo As String, prmGyoNo As String, prmIndex As String, prmDataGridView As DataGridView)
    ' 
    Dim tmpDt As New DataTable

    '行番号を設定
    Me.TxtGyoNo.Text = prmGyoNo

    '明細追加画面用のデータグリッドに実績一覧のデータグリッドを設定
    DataGridView2 = prmDataGridView

    '伝票明細行を検索
    SqlServer.GetResult(tmpDt, SqlGetDenpyoGyo(prmDenNo, prmGyoNo))

    'If tmpDt.Rows.Count <> 0 Then
    '  '表示列値設定
    '  Me.CmbMstItem1.Text = tmpDt.Rows(0).Item("ShohinCD")
    '  CmbMstItemValidating(CmbMstItem1, TxtItemName, TxtItemNameKana)
    '  Me.TxtItemNameKana.Text = tmpDt.Rows(0).Item("ShohinNMカナ")
    '  Me.TxtKeiryoFlg.Text = tmpDt.Rows(0).Item("Hakosu")

    '  '定貫タイプによって使用する項目を設定
    '  If Me.TxtKeiryoFlg.Text = HU_TEI_KAN Then
    '  Me.TxtHuteikanNohinSuryo.Text = tmpDt.Rows(0).Item("Suryo")
    '  Me.TxtHuteikanTani.Text = tmpDt.Rows(0).Item("Tani")
    '  Me.TxtHuteikanTanka.Text = tmpDt.Rows(0).Item("Tanka")
    '  Me.TxtHuteikanKingaku.Text = tmpDt.Rows(0).Item("Kingaku")
    '  Me.TxtTeikanNohinSuryo.Text = ""
    '  Me.TxtTeikanTani.Text = ""
    '  Me.TxtTeikanTanka.Text = ""
    '  Me.TxtTeikanKingaku.Text = ""
    '  Else
    '  Me.TxtHuteikanNohinSuryo.Text = ""
    '  Me.TxtHuteikanTani.Text = ""
    '  Me.TxtHuteikanTanka.Text = ""
    '  Me.TxtHuteikanKingaku.Text = ""
    '  Me.TxtTeikanNohinSuryo.Text = tmpDt.Rows(0).Item("入数")
    '  Me.TxtTeikanTani.Text = tmpDt.Rows(0).Item("Tani")
    '  Me.TxtTeikanTanka.Text = tmpDt.Rows(0).Item("Tanka")
    '  Me.TxtTeikanKingaku.Text = tmpDt.Rows(0).Item("Kingaku")
    'End If
    'Me.TxtBiko.Text = tmpDt.Rows(0).Item("Memo1")
    'Me.TxtMemo1.Text = tmpDt.Rows(0).Item("Memo2")
    'Me.TxtMemo2.Text = tmpDt.Rows(0).Item("Biko")
    'Else
    '表示列値設定
    Me.CmbMstItem1.Text = DataGridView2.Rows(prmIndex).Cells("商品コード").Value
    lastCmbMst = Me.CmbMstItem1.Text '名称、カナ表示 空ならマスタから
    TxtItemName.Text = DataGridView2.Rows(prmIndex).Cells("商品名").Value
    TxtItemNameKana.Text = If(tmpDt.Rows.Count = 0, "", tmpDt.Rows(0).Item("ShohinNMカナ"))
    TxtKeiryoFlg.Text = DataGridView2.Rows(prmIndex).Cells("定貫タイプ").Value

    If TxtKeiryoFlg.Text = TEI_KAN Then
      Me.TxtTeikanNohinSuryo.Text = DataGridView2.Rows(prmIndex).Cells("個数").Value
      Me.TxtTeikanTani.Text = DataGridView2.Rows(prmIndex).Cells("単位").Value
      Me.TxtTeikanTanka.Text = DataGridView2.Rows(prmIndex).Cells("単価").Value
      Me.TxtTeikanKingaku.Text = DataGridView2.Rows(prmIndex).Cells("金額").Value

    Else
      Me.TxtHuteikanKosu.Text = DataGridView2.Rows(prmIndex).Cells("個数").Value
      Me.TxtHuteikanNohinSuryo.Text = DataGridView2.Rows(prmIndex).Cells("数量").Value
      Me.TxtHuteikanTani.Text = DataGridView2.Rows(prmIndex).Cells("単位").Value
      Me.TxtHuteikanTanka.Text = DataGridView2.Rows(prmIndex).Cells("単価").Value
      Me.TxtHuteikanKingaku.Text = DataGridView2.Rows(prmIndex).Cells("金額").Value

    End If

    Me.TxtKotai1.Text = DataGridView2.Rows(prmIndex).Cells("個体識別番号１").Value
    Me.TxtKotai2.Text = DataGridView2.Rows(prmGyoNo - 1).Cells("個体識別番号２").Value
    Me.TxtKotai3.Text = DataGridView2.Rows(prmGyoNo - 1).Cells("個体識別番号３").Value
    'End If

  End Sub

#End Region

#Region "実績一覧データグリッド操作関連"
  ''' <summary>
  ''' カラム追加処理
  ''' </summary>
  ''' <param name="prmDataGridView1">カラム追加したいデータグリッド</param>
  ''' <returns>追加カラム</returns>
  Private Sub SetColumnAdd(prmDataGridView1 As DataGridView)
    With prmDataGridView1
      .Rows.Clear()
      .Columns.Add(SetColumn("行No"))
      .Columns.Add(SetColumn("商品コード"))
      .Columns.Add(SetColumn("商品名"))
      .Columns.Add(SetColumn("個数"))
      .Columns.Add(SetColumn("単位"))
      .Columns.Add(SetColumn("数量"))
      .Columns.Add(SetColumn("単価"))
      .Columns.Add(SetColumn("金額"))
      .Columns.Add(SetColumn("個体識別番号１"))
      .Columns.Add(SetColumn("個体識別番号２"))
      .Columns.Add(SetColumn("個体識別番号３"))

    End With
  End Sub


  ''' <summary>
  ''' カラムセット処理
  ''' </summary>
  ''' <param name="prmColumnName">カラム名</param>
  ''' <returns>追加カラム</returns>
  Private Function SetColumn(prmColumnName As String) As DataGridViewTextBoxColumn
    Dim textColumn As New DataGridViewTextBoxColumn()
    textColumn.DataPropertyName = prmColumnName
    '名前とヘッダーを設定する
    textColumn.Name = prmColumnName
    textColumn.HeaderText = prmColumnName

    Return textColumn
  End Function

#End Region

#Region "SQL関連"
  ''' <summary>
  ''' 伝票明細行を検索するＳＱＬ文作成
  ''' </summary>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlGetDenpyoGyo(prmDenNo As String, prmGyoNo As String) As String

    Dim sql As String = String.Empty
    sql &= " SELECT	GyoNo GyoNo "
    sql &= " 	,	ShohinCD ShohinCD "
    sql &= " 	,	ShohinNM ShohinNM "
    sql &= " 	,	ShohinKN ShohinNMカナ "
    sql &= " 	,	SShohinCD 先方ShohinCD "
    sql &= " 	,	Irisu 入数 "
    sql &= " 	,	Hakosu Hakosu "
    sql &= " 	,	Tani Tani "
    sql &= " 	,	JutyuSu 受注数 "
    sql &= " 	,	Suryo Suryo "
    sql &= " 	,	Tanka Tanka "
    sql &= " 	,	UriTanka 売Tanka "
    sql &= " 	,	HyojunKKKu 定金額 "
    sql &= " 	,	UriageKin Kingaku "
    sql &= " 	,	SokoCD 倉庫コード "
    sql &= " 	,	Biko Biko "
    sql &= "	,	Memo1 Memo1 "
    sql &= " 	,	Memo2 Memo2 "
    sql &= " FROM TRN_JISSEKI "
    sql &= " WHERE 1 = 1 "
    sql &= " AND DenNo = '" & prmDenNo & "'"
    sql &= " AND GyoNo = " & prmGyoNo

    Return sql

  End Function

  ' コンボボックスソース抽出用
  Public Function SqlGetShohinCD(prmCode As String) As String
    Dim sql As String = String.Empty

    sql &= " SELECT ShohinCD AS Code "
    sql &= "      , ShohinNM as Name "
    sql &= "      , ShohinKana as KanaName "
    sql &= "      , Irisu as Irisu "
    sql &= "      , Tani as Tani "
    sql &= "      , Baika1 as Baika "
    sql &= "      , HyojunKakaku as HyojunKakaku "
    sql &= "      , SokoCd as SokoCd "
    sql &= " FROM MST_SHOHIN "
    If prmCode <> "" Then
      sql &= "  WHERE ShohinCD = " & prmCode
    End If
    sql &= " ORDER BY ShohinCD "

    Return sql
  End Function

  ' コンボボックスソース抽出用
  Public Function SqlGetTokuisakiShohinCD(prmTCode As String, prmSCode As String) As String
    Dim sql As String = String.Empty

    sql &= " SELECT ShohinCD AS SCode "
    sql &= "      , TokuiCd as TCode "
    sql &= "      , Irisu as Irisu "
    sql &= "      , Tani as Tani "
    sql &= "      , HyojunKakaku as HyojunKakaku "
    sql &= "      , Tanka as Tanka "
    sql &= "      , Baika as Baika "
    sql &= " FROM MST_TOKUISAKI_SHOHIN "
    If prmTCode <> "" Then
      sql &= "  WHERE TokuiCD = " & prmTCode
    End If
    If prmSCode <> "" Then
      sql &= "  AND ShohinCD = " & prmSCode
    End If
    sql &= " ORDER BY ShohinCD "

    Return sql
  End Function

  Private Sub TxtMemo2_TextChanged(sender As Object, e As EventArgs) Handles TxtKotai3.TextChanged
    LimitByByteLength(TxtKotai3, 100)
  End Sub

  Private Sub TxtTeikanTanka_TextChanged(sender As Object, e As EventArgs) Handles TxtHuteikanNohinSuryo.TextChanged, TxtHuteikanTanka.TextChanged, TxtTeikanNohinSuryo.TextChanged, TxtTeikanTanka.TextChanged
    Dim tb As TextBox = CType(sender, TextBox)
    ' 数字以外の文字をすべて除去
    Dim cleanText As String = System.Text.RegularExpressions.Regex.Replace(tb.Text, "[^0-9]", "")
    tb.Text = cleanText

  End Sub

  Private Sub TxtItemName_TextChanged(sender As Object, e As EventArgs) Handles TxtItemName.TextChanged
    LimitByByteLength(TxtItemName, 36)

  End Sub


#End Region

#End Region

End Class