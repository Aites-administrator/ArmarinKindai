Imports Common
Imports Common.ClsFunction
Imports ClsAutoCommunication.ClsAutoCommunication
Imports CommonPcaDx
Public Class Form_MasterMente

  ' SQLサーバー操作オブジェクト
  Private _SqlServer As ClsSqlServer
  Private ReadOnly Property SqlServer As ClsSqlServer
    Get
      If _SqlServer Is Nothing Then
        _SqlServer = New ClsSqlServer
      End If
      Return _SqlServer
    End Get
  End Property

  Private Sub Form_MasterMente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    MaximizeBox = False
    FormBorderStyle = FormBorderStyle.FixedSingle
    LblMessage.Text = ""
  End Sub

  ''' <summary>
  ''' フォームキー押下時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>アクセスキー対応</remarks>
  Private Sub Form_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown

    Select Case e.KeyCode
      Case Keys.F1
        ' 得意先商品マスタメンテナンス
        OpenForm("M03")
      Case Keys.F2
        ' 即時発行設定マスタメンテ
        OpenForm("M02")

      Case Keys.F5
        ' 計量器管理
        OpenForm("M06")

      Case Keys.F9
        ' マスタ取込
        CallGetMaster()
      Case Keys.F10
        ' 終了
        Close()
    End Select

  End Sub

  Private Sub GarbageTypeMasterButton_Click(sender As Object, e As EventArgs) Handles GarbageTypeMasterButton.Click
    OpenForm("M01")
  End Sub

  Private Sub TenantMasterButton_Click(sender As Object, e As EventArgs) Handles PrintSettingMasterButton.Click
    OpenForm("M02")
  End Sub

  Private Sub GarbageCategoryMasterButton_Click(sender As Object, e As EventArgs) Handles GarbageCategoryMasterButton.Click
    OpenForm("M03")
  End Sub

  Private Sub BtnTantousha_Click(sender As Object, e As EventArgs) Handles BtnTantousha.Click
    OpenForm("M04")
  End Sub
  Private Sub AreaMasterButton_Click(sender As Object, e As EventArgs) Handles AreaMasterButton.Click
    OpenForm("M05")
  End Sub

  Private Sub CorporateMasterButton_Click(sender As Object, e As EventArgs) Handles CorporateMasterButton.Click
    OpenForm("M05")
  End Sub

  Private Sub PackingMasterButton_Click(sender As Object, e As EventArgs) Handles PackingMasterButton.Click
    OpenForm("M06")
  End Sub

  Private Sub ScaleMasterButton_Click(sender As Object, e As EventArgs) Handles ScaleMasterButton.Click
    OpenForm("M07")
  End Sub

  Private Sub GarbageCategory2MasterButton_Click(sender As Object, e As EventArgs) Handles GarbageCategory2MasterButton.Click
    OpenForm("M08")
  End Sub

  Private Sub CloseButton_Click(sender As Object, e As EventArgs) Handles CloseButton.Click
    Close()
  End Sub

  Private Sub BtnMasterLinkage_Click(sender As Object, e As EventArgs) Handles BtnMasterLinkage.Click
    CallGetMaster()

  End Sub

  Private Sub CallGetMaster()
    Dim dr As DialogResult
    Dim Concat_ScaleNumber As String = String.Empty
    Try
      dr = MessageBox.Show("マスタ取込を行います。" & vbCrLf & "よろしいでしょうか。", "確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Information)
      If dr = DialogResult.Cancel Then
        Exit Sub
      End If

      LblMessage.Text = "得意先マスタ取込中・・・"
      GetMaster("CustomerMasterDownload", Concat_ScaleNumber)
      LblMessage.Text = "得意先マスタ取込完了しました。 商品マスタ取込中・・・"

      GetMaster("ItemMasterDownload", Concat_ScaleNumber)
      LblMessage.Text = "商品マスタ取込完了"

      GetMaster("ShippingMasterDownload", Concat_ScaleNumber)
      LblMessage.Text = "発送先マスタ取込完了"

      ''商品登録
      'PcaInsShohin()


      ''得意先登録
      'PcaInsTokuisaki()

      MessageBox.Show("マスタ取込終了しました。" & vbCrLf & "処理結果をご確認下さい。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Information)

    Catch ex As Exception
      ComWriteErrLog(ex, False)
    End Try
  End Sub

  Private Sub PcaInsShohin()

    Dim strDate As DateTime = Now
    Dim tmpPcaInsSms As New clsPcaSms()
    Dim tmpPcaUpdSms As New clsPcaSms()
    Dim tmpPcaSmsCount As New clsPcaSms()
    Dim tmpPcaSmsList As New List(Of clsPcaSmsD)
    Dim tmpDt As New DataTable
    Dim tmpUpdFlg As Boolean = False

    Try
      SqlServer.GetResult(tmpDt, GetMstItemSql)

      For Each tmpDr As DataRow In tmpDt.Rows
        Dim tmpPcaSmsD As New clsPcaSmsD()
        Dim tmpPcaSmsSearchCondition As New clsPcaSmsD()

        tmpPcaSmsSearchCondition.商品コード = tmpDr.Item("ShohinCD").ToString
        tmpPcaSmsList = tmpPcaSmsCount.GetData(tmpPcaSmsSearchCondition)

        tmpPcaSmsD.商品コード = tmpDr.Item("ShohinCD").ToString
        tmpPcaSmsD.商品名 = tmpDr.Item("ShohinNM").ToString
        tmpPcaSmsD.システム区分 = tmpDr.Item("SysKBN").ToString
        tmpPcaSmsD.マスター区分 = tmpDr.Item("SMstKBN").ToString
        tmpPcaSmsD.在庫管理 = tmpDr.Item("ZaiKanri").ToString
        tmpPcaSmsD.実績管理 = tmpDr.Item("JituKanri").ToString
        tmpPcaSmsD.入数 = tmpDr.Item("Irisu").ToString
        tmpPcaSmsD.単位 = tmpDr.Item("Tani").ToString
        tmpPcaSmsD.色 = tmpDr.Item("Iro").ToString
        tmpPcaSmsD.サイズ = tmpDr.Item("Size").ToString
        tmpPcaSmsD.商品区分1 = tmpDr.Item("ShohinKBN1").ToString
        tmpPcaSmsD.商品区分2 = tmpDr.Item("ShohinKBN2").ToString
        tmpPcaSmsD.商品区分3 = tmpDr.Item("ShohinKBN3").ToString
        tmpPcaSmsD.税区分 = tmpDr.Item("ZeiKBN").ToString
        tmpPcaSmsD.売上税込区分 = tmpDr.Item("ZeikomiKBN").ToString
        tmpPcaSmsD.単価小数桁 = tmpDr.Item("SKetaT").ToString
        tmpPcaSmsD.数量小数桁 = tmpDr.Item("SKetaS").ToString
        tmpPcaSmsD.標準価格 = tmpDr.Item("HyojunKakaku").ToString
        tmpPcaSmsD.原価 = tmpDr.Item("Genka").ToString
        tmpPcaSmsD.売価1 = tmpDr.Item("Baika1").ToString
        tmpPcaSmsD.売価2 = tmpDr.Item("Baika2").ToString
        tmpPcaSmsD.売価3 = tmpDr.Item("Baika3").ToString
        tmpPcaSmsD.売価4 = tmpDr.Item("Baika4").ToString
        tmpPcaSmsD.売価5 = tmpDr.Item("Baika5").ToString
        tmpPcaSmsD.倉庫コード = tmpDr.Item("SokoCD").ToString
        tmpPcaSmsD.主仕入先 = tmpDr.Item("SSiireCD").ToString
        tmpPcaSmsD.在庫単価 = tmpDr.Item("ZaiTanka").ToString
        tmpPcaSmsD.仕入単価 = tmpDr.Item("SiireTanka").ToString

        If tmpPcaSmsList.Count = 0 Then
          tmpPcaInsSms.AddDetail(tmpPcaSmsD)
        Else
          tmpPcaSmsD.更新Date = tmpPcaSmsList(0).更新Date
          tmpPcaUpdSms.AddDetail(tmpPcaSmsD)
        End If

      Next


      '商品更新
      tmpPcaUpdSms.Update()
      '商品追加
      tmpPcaInsSms.Create()
    Catch ex As Exception
      ComWriteErrLog(ex)
      Throw New Exception(ex.Message)
    Finally
      tmpPcaSmsList.Clear()
      tmpPcaUpdSms.Dispose()
      tmpPcaInsSms.Dispose()

    End Try
  End Sub

  Private Sub PcaInsTokuisaki()

    Dim strDate As DateTime = Now
    Dim tmpPcaInsTms As New clsPcaTms()
    Dim tmpPcaUpdTms As New clsPcaTms()
    Dim tmpPcaTmsCount As New clsPcaTms()
    Dim tmpPcaTmsList As New List(Of clsPcaTmsD)
    Dim tmpDt As New DataTable
    Dim tmpUpdFlg As Boolean = False

    Try
      SqlServer.GetResult(tmpDt, GetMstCustomerSql)

      For Each tmpDr As DataRow In tmpDt.Rows
        Dim tmpPcaTmsD As New clsPcaTmsD()
        Dim tmpPcaTmsSearchCondition As New clsPcaTmsD()

        tmpPcaTmsSearchCondition.得意先コード = tmpDr.Item("TokuiCD").ToString
        tmpPcaTmsList = tmpPcaTmsCount.GetData(tmpPcaTmsSearchCondition)

        tmpPcaTmsD.得意先コード = tmpDr.Item("TokuiCD").ToString
        tmpPcaTmsD.得意先名1 = tmpDr.Item("TokuiNM1").ToString
        tmpPcaTmsD.得意先名2 = tmpDr.Item("TokuiNM2").ToString
        tmpPcaTmsD.先方担当者名 = tmpDr.Item("SenpoTanto").ToString
        tmpPcaTmsD.請求先コード = tmpDr.Item("SeikyuCD").ToString
        tmpPcaTmsD.実績管理 = tmpDr.Item("TJituKanri").ToString
        tmpPcaTmsD.住所1 = tmpDr.Item("Add1").ToString
        tmpPcaTmsD.住所2 = tmpDr.Item("Add1").ToString
        tmpPcaTmsD.郵便番号 = tmpDr.Item("ZipCD").ToString
        tmpPcaTmsD.相手先TEL = tmpDr.Item("TelNo").ToString
        tmpPcaTmsD.相手先FAX = tmpDr.Item("FaxNo").ToString
        tmpPcaTmsD.得意先区分1 = tmpDr.Item("TokuiKBN1").ToString
        tmpPcaTmsD.得意先区分2 = tmpDr.Item("TokuiKBN2").ToString
        tmpPcaTmsD.得意先区分3 = tmpDr.Item("TokuiKBN3").ToString
        tmpPcaTmsD.適用売価No = tmpDr.Item("TekiyoUriNo").ToString
        tmpPcaTmsD.掛率 = tmpDr.Item("TekiyoKakeritu").ToString
        tmpPcaTmsD.税換算 = tmpDr.Item("TekiyoZeikan").ToString
        'tmpPcaTmsD.主担当者 = tmpDr.Item("SyuTantoCD").ToString
        tmpPcaTmsD.請求締日 = tmpDr.Item("SeikyuSimebi").ToString
        tmpPcaTmsD.消費税端数 = tmpDr.Item("ShohizeiHasu").ToString
        tmpPcaTmsD.消費税通知 = tmpDr.Item("ShohizeiTuti").ToString
        tmpPcaTmsD.回収種別1 = tmpDr.Item("Kaisyu1").ToString
        tmpPcaTmsD.回収種別2 = tmpDr.Item("Kaisyu2").ToString
        tmpPcaTmsD.回収日 = tmpDr.Item("KaisyuYoteibi").ToString
        tmpPcaTmsD.回収方法 = tmpDr.Item("KaisyuHou").ToString
        tmpPcaTmsD.与信限度額 = tmpDr.Item("YosinGendo").ToString
        tmpPcaTmsD.繰越残高_売掛金残高 = tmpDr.Item("KurikosiZan").ToString
        'tmpPcaTmsD.納品書用紙 = tmpDr.Item("NohinYosi").ToString
        tmpPcaTmsD.納品書社名 = tmpDr.Item("NohinShamei").ToString
        'tmpPcaTmsD.請求書用紙 = tmpDr.Item("SeikyuYosi").ToString
        tmpPcaTmsD.請求書社名 = tmpDr.Item("SeikyuShamei").ToString
        tmpPcaTmsD.社店コード = tmpDr.Item("ShatenCD").ToString
        tmpPcaTmsD.伝票区分 = tmpDr.Item("TokuiKubun").ToString

        If tmpPcaTmsList.Count = 0 Then
          tmpPcaInsTms.AddDetail(tmpPcaTmsD)
        Else
          tmpPcaTmsD.更新Date = tmpPcaTmsList(0).更新Date
          tmpPcaTmsD.残高更新Date = tmpPcaTmsList(0).残高更新Date
          tmpPcaUpdTms.AddDetail(tmpPcaTmsD)
        End If

      Next


      '商品更新
      tmpPcaUpdTms.Update()
      '商品追加
      tmpPcaInsTms.Create()
    Catch ex As Exception
      ComWriteErrLog(ex)
      Throw New Exception(ex.Message)
    Finally
      tmpPcaTmsList.Clear()
      tmpPcaUpdTms.Dispose()
      tmpPcaInsTms.Dispose()

    End Try
  End Sub

  Private Function GetMstItemSql() As String
    Dim sql As String = String.Empty
    sql &= " SELECT"
    sql &= "     ShohinCD "
    sql &= "     ,ShohinNM "
    sql &= "     ,ShohinKana "
    sql &= "     ,SysKBN "
    sql &= "     ,SMstKBN "
    sql &= "     ,ZaiKanri "
    sql &= "     ,JituKanri "
    sql &= "     ,Irisu "
    sql &= "     ,Tani "
    sql &= "     ,Iro "
    sql &= "     ,Size "
    sql &= "     ,ShohinKBN1 "
    sql &= "     ,ShohinKBN2 "
    sql &= "     ,ShohinKBN3 "
    sql &= "     ,ZeiKBN "
    sql &= "     ,ZeikomiKBN "
    sql &= "     ,SKetaT "
    sql &= "     ,SKetaS "
    sql &= "     ,HyojunKakaku "
    sql &= "     ,Genka "
    sql &= "     ,Baika1 "
    sql &= "     ,Baika2 "
    sql &= "     ,Baika3 "
    sql &= "     ,Baika4 "
    sql &= "     ,Baika5 "
    sql &= "     ,SokoCD "
    sql &= "     ,SSiireCD "
    sql &= "     ,ZaiTanka "
    sql &= "     ,SiireTanka "
    sql &= "     ,JANCD "
    sql &= "     ,ShoKubun "
    sql &= "     ,TDATE "
    sql &= "     ,KDATE "
    sql &= " FROM"
    sql &= "     MST_SHOHIN"
    Call WriteExecuteLog("Module_Upload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function

  Private Function GetMstCustomerSql() As String
    Dim sql As String = String.Empty
    sql &= " SELECT"
    sql &= "     TokuiCD "
    sql &= "     ,TokuiNM1 "
    sql &= "     ,TokuiNM2 "
    sql &= "     ,TokuiKana "
    sql &= "     ,SenpoTanto "
    sql &= "     ,TMstKBN "
    sql &= "     ,SeikyuCD "
    sql &= "     ,TJituKanri "
    sql &= "     ,Add1 "
    sql &= "     ,Add2 "
    sql &= "     ,ZipCD "
    sql &= "     ,TelNo "
    sql &= "     ,FaxNo "
    sql &= "     ,TokuiKBN1 "
    sql &= "     ,TokuiKBN2 "
    sql &= "     ,TokuiKBN3 "
    sql &= "     ,TekiyoUriNo "
    sql &= "     ,CONVERT(int,TekiyoKakeritu * 10) AS TekiyoKakeritu "
    sql &= "     ,TekiyoZeikan "
    sql &= "     ,SyuTantoCD "
    sql &= "     ,SeikyuSimebi "
    sql &= "     ,ShohizeiHasu "
    sql &= "     ,ShohizeiTuti "
    sql &= "     ,Kaisyu1 "
    sql &= "     ,Kaisyu2 "
    sql &= "     ,KaisyuKyokai "
    sql &= "     ,KaisyuYoteibi "
    sql &= "     ,KaisyuHou "
    sql &= "     ,YosinGendo "
    sql &= "     ,KurikosiZan "
    sql &= "     ,NohinYosi "
    sql &= "     ,NohinShamei "
    sql &= "     ,SeikyuYosi "
    sql &= "     ,SeikyuShamei "
    sql &= "     ,KantyoKBN "
    sql &= "     ,Keisho "
    sql &= "     ,ShatenCD "
    sql &= "     ,TorihikiCD "
    sql &= "     ,TokuiKubun "
    sql &= "     ,DENP_KBN "
    sql &= " FROM"
    sql &= "     MST_TOKUISAKI"
    Call WriteExecuteLog("Module_Upload", System.Reflection.MethodBase.GetCurrentMethod().Name, sql)
    Return sql
  End Function
End Class