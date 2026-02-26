Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonCon.DbConnectData

Public Class clsPcaDb
  Inherits clsComDatabase

#Region "定数定義"
  ''' <summary>
  ''' PCA商品コード桁数
  ''' </summary>
  Public Shared ReadOnly ITEM_CODE_LENGTH = 4
  ''' <summary>
  ''' PCA得意先コード桁数
  ''' </summary>
  Public Shared ReadOnly CUSTOMER_CODE_LENGTH = 4
#End Region

  Public Sub New()
    Me.DataSource = PCA_DATASOURCE
    Me.DefaultDatabase = PCA_DEFAULTDATABASE
    Me.UserId = PCA_USERID
    Me.Password = PCA_PASSWORD
    Me.Provider = typProvider.sqlServer
  End Sub

  ''' <summary>
  ''' 消費税率を取得する
  ''' </summary>
  ''' <param name="prmProductCode"></param>
  ''' <returns></returns>
  Public Function GetZeiSbt(prmProductCode As String) As Dictionary(Of String, String)

    Dim ret As New Dictionary(Of String, String)
    Dim TrzDataBase As New clsSqlServer

    Dim tmpDt As New DataTable
    Dim tmpProductData As New Dictionary(Of String, String)

    Try
      tmpProductData = GetProductData(TrzDataBase.GetProductCode(prmProductCode))  ' 商品情報取得
      ret = tmpProductData
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("税種別の取得に失敗しました。")
    End Try

    Return ret
  End Function

  ''' <summary>
  ''' PCA商品情報取得
  ''' </summary>
  ''' <param name="prmProductCode">商品コード</param>
  ''' <returns></returns>
  Public Function GetProductData(prmProductCode As String) As Dictionary(Of String, String)
    Dim ret As New Dictionary(Of String, String)
    Dim tmpDt As New DataTable
    Dim tmpSearchCondition As String = "sms_scd = '" & prmProductCode.PadLeft(clsPcaDb.ITEM_CODE_LENGTH, "0"c) & "'"

    Try
      Me.GetResult(tmpDt, ComAddSqlSearchCondition(SqlSelSMS(), tmpSearchCondition))
      If tmpDt.Rows.Count > 0 Then
        For Each tmpDc As DataColumn In tmpDt.Rows(0).Table.Columns
          Call ComSetDictionaryVal(ret, tmpDc.ColumnName, tmpDt.Rows(0).Item(tmpDc.ColumnName).ToString())
        Next
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("PCA 商品情報の取得に失敗しました。")
    Finally
      tmpDt.Dispose()
    End Try

    Return ret

  End Function

  ''' <summary>
  ''' 商品情報取得SQL文の作成
  ''' </summary>
  ''' <returns></returns>
  Private Function SqlSelSMS() As String
    Dim sql As String = String.Empty

    sql &= " SELECT * "
    sql &= " FROM SMS "
    sql &= "  INNER JOIN SMSP ON SMS.sms_scd = SMSP.smsp_scd "

    Return sql
  End Function

  ''' <summary>
  ''' 消費税率を取得する
  ''' </summary>
  ''' <param name="prmCustomerCode"></param>
  ''' <param name="prmProductCode"></param>
  ''' <param name="prmDate"></param>
  ''' <returns></returns>
  Public Function GetTaxRate(prmCustomerCode As String _
                            , prmProductCode As String _
                            , Optional prmSiireKbn As Boolean = False _
                            , Optional prmDate As String = "") As Decimal

    Dim ret As Decimal

    Dim tmpDt As New DataTable
    Dim TrzDataBase As New clsSqlServer
    Dim tmpProductData As New Dictionary(Of String, String)
    Dim tmpCustomerData As New Dictionary(Of String, String)
    Dim tmpSearchCondition As String = String.Empty
    Dim tmpTargetDate As String = String.Empty
    Dim tmpTaxKind As String = String.Empty

    Try
      tmpProductData = GetProductData(TrzDataBase.GetProductCode(prmProductCode))  ' 商品情報取得
      tmpTaxKind = If(prmSiireKbn, tmpProductData("sms_kantaxkind"), tmpProductData("sms_kontaxkind"))
      If tmpProductData("smsp_tax").ToString().Trim() = "0" Then
        ' 非課税
        ret = 0
      Else
        If prmCustomerCode <> 0 Then
          tmpCustomerData = If(prmSiireKbn, GetPaymentData(prmCustomerCode), GetBillingData(prmCustomerCode)) ' 得意先情報取得
        Else
          tmpCustomerData.Add("tax", -1)
        End If

        If tmpCustomerData("tax") = 0 Then
          ' 得意先の税通知が[0:免税]の場合は非課税
          ret = 0
        Else
          If prmDate = "" Then
            tmpTargetDate = ComGetProcDate().Replace("/", "")
          Else
            tmpTargetDate = Date.Parse(prmDate).ToString("yyyyMMdd")
          End If

          tmpSearchCondition &= " tax_ymd <= " & tmpTargetDate
          tmpSearchCondition &= " AND tax_kind = " & tmpTaxKind

          Me.GetResult(tmpDt, ComAddSqlSearchCondition(SqlSelTax(), tmpSearchCondition))

          If tmpDt.Rows.Count > 0 Then
            ret = Decimal.Parse(tmpDt.Rows(0).Item("tax_rate" & tmpProductData("smsp_tax")).ToString())
          Else
            Throw New Exception("消費税設定が存在しません。")
          End If
        End If

      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("消費税率の取得に失敗しました。")
    End Try

    Return ret
  End Function

  ''' <summary>
  ''' 税テーブル取得SQL文の作成
  ''' </summary>
  ''' <returns></returns>
  Private Function SqlSelTax() As String
    Dim sql As String = String.Empty

    sql &= " SELECT * "
    sql &= " FROM TAX "
    sql &= " WHERE 1 = 1"
    sql &= " ORDER BY tax_ymd DESC "

    Return sql
  End Function

  ''' <summary>
  ''' 請求先情報取得
  ''' </summary>
  ''' <param name="prmCustomerCode">対象の得意先コード</param>
  ''' <returns>請求先情報</returns>
  Public Function GetBillingData(prmCustomerCode As String) As Dictionary(Of String, String)
    Dim tmpCustomerData As New Dictionary(Of String, String)
    Dim tmpPcaCustomerCode As String = String.Empty
    Dim TrzDataBase As New clsSqlServer

    ' 得意先情報取得
    tmpPcaCustomerCode = TrzDataBase.GetCustomerCode(prmCustomerCode)
    tmpCustomerData = GetCustomerData(tmpPcaCustomerCode)

    ' 請求先が異なる場合は請求先データを取得
    If tmpCustomerData("tms_ocd").ToString().Trim() <> tmpPcaCustomerCode.PadLeft(clsPcaDb.CUSTOMER_CODE_LENGTH, "0"c) Then
      tmpCustomerData = GetCustomerData(tmpCustomerData("tms_ocd").ToString()) ' 得意先情報取得
    End If

    Return tmpCustomerData
  End Function

  ''' <summary>
  ''' 請求先情報取得
  ''' </summary>
  ''' <param name="prmSupplierCode">対象の得意先コード</param>
  ''' <returns>請求先情報</returns>
  Public Function GetPaymentData(prmSupplierCode As String) As Dictionary(Of String, String)
    Dim tmpSupplierData As New Dictionary(Of String, String)
    Dim tmpPcaSupplierCode As String = String.Empty
    Dim TrzDataBase As New clsSqlServer

    ' 得意先情報取得
    tmpPcaSupplierCode = TrzDataBase.GetSupplierCode(prmSupplierCode)
    tmpSupplierData = GetSupplierData(tmpPcaSupplierCode)

    ' 請求先が異なる場合は請求先データを取得
    If tmpSupplierData("rms_ocd").ToString().Trim() <> tmpPcaSupplierCode.PadLeft(clsPcaDb.CUSTOMER_CODE_LENGTH, "0"c) Then
      tmpSupplierData = GetSupplierData(tmpSupplierData("rms_ocd").ToString()) ' 得意先情報取得
    End If

    Return tmpSupplierData
  End Function

  ''' <summary>
  ''' PCA得意先情報取得
  ''' </summary>
  ''' <param name="prmCustomerCode">得意先コード</param>
  ''' <returns>取得した得意先情報</returns>
  Public Function GetCustomerData(prmCustomerCode As String) As Dictionary(Of String, String)
    Dim ret As New Dictionary(Of String, String)
    Dim tmpDt As New DataTable
    Dim PcaDataBase As New clsPcaDb
    Dim tmpSearchCondition As String = "tms_tcd = '" & prmCustomerCode.PadLeft(clsPcaDb.CUSTOMER_CODE_LENGTH, "0"c) & "'"

    Try
      PcaDataBase.GetResult(tmpDt, ComAddSqlSearchCondition(SqlSelTMS(), tmpSearchCondition))
      If tmpDt.Rows.Count > 0 Then
        For Each tmpDc As DataColumn In tmpDt.Rows(0).Table.Columns
          Call ComSetDictionaryVal(ret, tmpDc.ColumnName, tmpDt.Rows(0).Item(tmpDc.ColumnName).ToString())
        Next
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("PCA 得意先情報の取得に失敗しました。")
    Finally
      tmpDt.Dispose()
    End Try

    Return ret
  End Function

  ''' <summary>
  ''' PCA得意先情報取得
  ''' </summary>
  ''' <param name="prmSupplierCode">得意先コード</param>
  ''' <returns>取得した得意先情報</returns>
  Public Function GetSupplierData(prmSupplierCode As String) As Dictionary(Of String, String)
    Dim ret As New Dictionary(Of String, String)
    Dim tmpDt As New DataTable
    Dim PcaDataBase As New clsPcaDb
    Dim tmpSearchCondition As String = "rms_tcd = '" & prmSupplierCode.PadLeft(clsPcaDb.CUSTOMER_CODE_LENGTH, "0"c) & "'"

    Try
      PcaDataBase.GetResult(tmpDt, ComAddSqlSearchCondition(SqlSelRMS(), tmpSearchCondition))
      If tmpDt.Rows.Count > 0 Then
        For Each tmpDc As DataColumn In tmpDt.Rows(0).Table.Columns
          Call ComSetDictionaryVal(ret, tmpDc.ColumnName, tmpDt.Rows(0).Item(tmpDc.ColumnName).ToString())
        Next
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("PCA 得意先情報の取得に失敗しました。")
    Finally
      tmpDt.Dispose()
    End Try

    Return ret
  End Function

  ''' <summary>
  ''' 得意先情報取得SQL文の作成
  ''' </summary>
  ''' <returns></returns>
  Private Function SqlSelTMS() As String
    Dim sql As String = String.Empty

    sql &= " SELECT tms_tax as tax,* "
    sql &= " FROM TMS INNER JOIN CMS ON TMS.tms_cmsid = CMS.cms_id "
    sql &= "           LEFT JOIN FMS ON TMS.tms_fmsid = FMS.fms_id "

    Return sql
  End Function

  ''' <summary>
  ''' 得意先情報取得SQL文の作成
  ''' </summary>
  ''' <returns></returns>
  Private Function SqlSelRMS() As String
    Dim sql As String = String.Empty

    sql &= " SELECT rms_tax as tax,* "
    sql &= " FROM RMS "

    Return sql
  End Function



End Class
