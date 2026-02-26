Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsPcaFnc


''' <summary>
''' PCAデータベース操作クラス
''' </summary>
Public Class clsPcaDatabase
  Implements IDisposable

#Region "定数定義"

#Region "パブリック"
  Public Enum typTaxType
    ''' <summary>
    ''' 非課税
    ''' </summary>
    TAX_NONE = 0

    ''' <summary>
    ''' 内税
    ''' </summary>
    TAX_INCLUSIVE

    ''' <summary>
    ''' 外税
    ''' </summary>
    TAX_EXCLUSIVE   ' 外税
  End Enum

  Public Enum typZeikomi
    売上 = 0
    仕入
  End Enum
#End Region

#End Region

#Region "メンバ"
#Region "プライベート"
  Private _PcaDataBase As New clsComDatabase
  Private _TrzDataBase As New clsSqlServer

  Private _taxRate(10) As Decimal

#End Region
#End Region

#Region "コンストラクタ"
  Public Sub New()
    _PcaDataBase = New clsPcaDb
  End Sub
#End Region

#Region "メソッド"

#Region "プライベート"

  ''' <summary>
  ''' 得意先情報取得SQL文の作成
  ''' </summary>
  ''' <returns></returns>
  Private Function SqlSelTMS() As String
    Dim sql As String = String.Empty

    sql &= " SELECT * "
    sql &= " FROM TMS INNER JOIN CMS ON TMS.tms_cmsid = CMS.cms_id "
    sql &= "           LEFT JOIN FMS ON TMS.tms_fmsid = FMS.fms_id "

    Return sql
  End Function

  ''' <summary>
  ''' 仕入先情報取得SQL文の作成
  ''' </summary>
  ''' <returns></returns>
  Private Function SqlSelRMS() As String
    Dim sql As String = String.Empty

    sql &= " SELECT * "
    sql &= " FROM RMS "

    Return sql
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
  ''' PCA売上伝票より外税額を取得するSQL文の作成
  ''' </summary>
  ''' <param name="prmSlipNumber">伝票番号</param>
  ''' <param name="prmSalesDate">売上日</param>
  ''' <param name="prmCumstomerCode">得意先コード</param>
  ''' <returns>作成したSQL文</returns>
  Private Function SqlSelSYKTax(prmSlipNumber As String _
                              , prmSalesDate As String _
                              , prmCumstomerCode As String) As String

    Dim sql As String = String.Empty

    sql &= " SELECT SUM(sykt_zei) as 伝票外税 "
    sql &= " FROM SYKT INNER Join SYKH On SYKT.sykt_hid = SYKH.sykh_id "
    sql &= " WHERE SYKH.sykh_denno =  " & prmSlipNumber
    sql &= " AND SYKH.sykh_uribi =  " & prmSalesDate
    sql &= " AND SYKH.sykh_tcd = '" & prmCumstomerCode & "'"

    Return sql
  End Function

  ''' <summary>
  ''' THENKAN（得意先変換入力）から得意先コードを取得するSQL文の作成
  ''' </summary>
  ''' <returns></returns>
  Private Function SqlSelThenkan(prmCumstomerCode As String) As String
    Dim sql As String = String.Empty

    sql &= " SELECT HENKAN AS TKCODE "
    sql &= " FROM THENKAN "
    sql &= " WHERE TKCODE =  " & prmCumstomerCode

    Return sql

  End Function

  ''' <summary>
  ''' SIHENKA（仕入先変換入力）から仕入先コードを取得するSQL文の作成
  ''' </summary>
  ''' <returns></returns>
  Private Function SqlSelSihenka(prmSupplieCode As String) As String
    Dim sql As String = String.Empty

    sql &= " SELECT HENKAN AS SCODE "
    sql &= " FROM SIHENKA "
    sql &= " WHERE SRCODE =  " & prmSupplieCode

    Return sql

  End Function

  ''' <summary>
  ''' PCA商品コード取得するSQL文の作成
  ''' </summary>
  ''' <returns></returns>
  Private Function SqlSelShenkan() As String
    Dim sql As String = String.Empty

    sql &= " SELECT HENKAN "
    sql &= " FROM SHENKAN "

    Return sql
  End Function

#End Region

#Region "パブリック"

  ''' <summary>
  ''' 
  ''' </summary>
  ''' <returns></returns>
  Public Function GetSqlCon() As clsComDatabase

    Dim sqlCon As New clsComDatabase

    sqlCon = _PcaDataBase

    Return sqlCon

  End Function

  ''' <summary>
  ''' 伝票外税額取得
  ''' </summary>
  ''' <param name="prmSlipNumber">伝票番号</param>
  ''' <param name="prmSalesDate">売上日</param>
  ''' <param name="prmCumstomerCode">得意先コード</param>
  ''' <returns>外税額</returns>
  Public Function GetOutTax(prmSlipNumber As String _
                          , prmSalesDate As String _
                          , prmCumstomerCode As String) As Long
    Dim ret As Long = 0
    Dim tmpDt As New DataTable

    Try
      _PcaDataBase.GetResult(tmpDt, SqlSelSYKTax(prmSlipNumber, prmSalesDate, prmCumstomerCode))
      If tmpDt.Rows.Count <> 1 Then
        Throw New Exception("PCA 売上伝票取得に失敗しました。")
      Else
        If String.IsNullOrWhiteSpace(tmpDt.Rows(0).Item("伝票外税").ToString()) Then
          ret = 0
        Else
          ret = Decimal.ToInt32(Decimal.Parse(tmpDt.Rows(0).Item("伝票外税").ToString()))
        End If
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("PCA 売上伝票外税額の取得に失敗しました。")
    Finally
      tmpDt.Dispose()
      tmpDt = Nothing
    End Try

    Return ret

  End Function

  ''' <summary>
  ''' PCA仕入先情報取得
  ''' </summary>
  ''' <param name="prmSupplierrCode">仕入先コード</param>
  ''' <returns>取得した仕入先情報</returns>
  Public Function GetSupplierData(prmSupplierrCode As String) As Dictionary(Of String, String)
    Dim ret As New Dictionary(Of String, String)
    Dim tmpDt As New DataTable
    Dim tmpSearchCondition As String = "rms_tcd = '" & prmSupplierrCode.PadLeft(6, "0"c) & "'"

    Try
      _PcaDataBase.GetResult(tmpDt, ComAddSqlSearchCondition(SqlSelRMS(), tmpSearchCondition))
      If tmpDt.Rows.Count > 0 Then
        For Each tmpDc As DataColumn In tmpDt.Rows(0).Table.Columns
          Call ComSetDictionaryVal(ret, tmpDc.ColumnName, tmpDt.Rows(0).Item(tmpDc.ColumnName).ToString())
        Next
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("PCA 仕入先情報の取得に失敗しました。")
    Finally
      tmpDt.Dispose()
    End Try

    Return ret
  End Function

  ''' <summary>
  ''' PCA得意先情報取得
  ''' </summary>
  ''' <param name="prmCustomerCode">得意先コード</param>
  ''' <returns>取得した得意先情報</returns>
  Public Function GetCustomerData(prmCustomerCode As String) As Dictionary(Of String, String)
    Dim ret As New Dictionary(Of String, String)
    Dim tmpDt As New DataTable
    Dim tmpSearchCondition As String = "tms_tcd = '" & prmCustomerCode.PadLeft(6, "0"c) & "'"

    Try
      _PcaDataBase.GetResult(tmpDt, ComAddSqlSearchCondition(SqlSelTMS(), tmpSearchCondition))
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
  ''' PCA商品情報取得
  ''' </summary>
  ''' <param name="prmProductCode">商品コード</param>
  ''' <returns></returns>
  Public Function GetProductData(prmProductCode As String) As Dictionary(Of String, String)
    Dim ret As New Dictionary(Of String, String)
    Dim tmpDt As New DataTable
    Dim tmpSearchCondition As String = "sms_scd = '" & prmProductCode.PadLeft(6, "0"c) & "'"

    Try
      _PcaDataBase.GetResult(tmpDt, ComAddSqlSearchCondition(SqlSelSMS(), tmpSearchCondition))
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
  ''' 消費税率を取得する
  ''' </summary>
  Public Sub InitTaxRate()

    Dim tmpDt As New DataTable
    Dim tmpProductData As New Dictionary(Of String, String)
    Dim tmpSearchCondition As String = String.Empty
    Dim tmpTargetDate As String = String.Empty

    Try

      tmpTargetDate = ComGetProcDate().Replace("/", "")

      tmpSearchCondition &= " tax_ymd <= " & tmpTargetDate

      _PcaDataBase.GetResult(tmpDt, ComAddSqlSearchCondition(SqlSelTax(), tmpSearchCondition))

      If tmpDt.Rows.Count > 0 Then
        For i = 1 To 9
          _taxRate(i) = Decimal.Parse(tmpDt.Rows(0).Item("tax_rate" & i.ToString).ToString())
        Next
      Else
        Throw New Exception("消費税設定が存在しません。")
      End If

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("消費税率の取得に失敗しました。")
    End Try

  End Sub

  ''' <summary>
  ''' 仕入消費税率を取得する
  ''' </summary>
  ''' <returns></returns>
  Public Function GetPurchaseTaxRate(prmSupplieCode As String _
                            , prmProductCode As String _
                            , Optional prmDate As String = "") As Decimal
    Dim ret As Decimal
    Dim tmpDt As New DataTable
    Dim tmpCustomerData As New Dictionary(Of String, String)

    Try
      tmpCustomerData = GetSupplieBillingData(prmSupplieCode)  ' 仕入先情報取得
      If (tmpCustomerData.Count <> 0) Then
        If CInt(tmpCustomerData("rms_tax")) = 0 Then
          ' 仕入先の税通知が[0:免税]の場合は非課税
          ret = 0
        Else
          ' 仕入先の税通知が免税以外は売上日と商品の税種別から税率を取得する
          ret = GetItemTaxRate(prmProductCode, "sms_kantaxkind", prmDate)
        End If
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("消費税率の取得に失敗しました。")
    End Try

    Return ret
  End Function


  ''' <summary>
  ''' 売上消費税率を取得する
  ''' </summary>
  ''' <param name="prmCustomerCode"></param>
  ''' <param name="prmProductCode"></param>
  ''' <param name="prmDate"></param>
  ''' <returns></returns>
  Public Function GetSalesTaxRate(prmCustomerCode As String _
                            , prmProductCode As String _
                            , Optional prmDate As String = "") As Decimal

    Dim ret As Decimal
    Dim tmpDt As New DataTable
    Dim tmpCustomerData As New Dictionary(Of String, String)

    Try
      tmpCustomerData = GetBillingData(prmCustomerCode) ' 得意先情報取得
      If (tmpCustomerData.Count <> 0) Then
        If CInt(tmpCustomerData("tms_tax")) = 0 Then
          ' 得意先の税通知が[0:免税]の場合は非課税
          ret = 0
        Else
          ' 得意先の税通知が免税以外は売上日と商品の税種別から税率を取得する
          ret = GetItemTaxRate(prmProductCode, "sms_kontaxkind", prmDate)
        End If
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("消費税率の取得に失敗しました。")
    End Try

    Return ret
  End Function

  ''' <summary>
  ''' 商品の税率を取得する
  ''' </summary>
  ''' <param name="prmProductCode">商品コード</param>
  ''' <param name="prmTaxkind">税種別（標準:0 or 軽減:1 ）</param>
  ''' <param name="prmDate">対象日付（省略時は本日日付）</param>
  ''' <returns>税率</returns>
  Public Function GetItemTaxRate(prmProductCode As String _
                               , prmTaxkind As String _
                               , Optional prmDate As String = "") As Decimal
    Dim ret As Decimal
    Dim tmpDt As New DataTable
    Dim tmpTargetDate As String
    Dim tmpSearchCondition As String = String.Empty
    Dim tmpProductData As New Dictionary(Of String, String)

    Try
      tmpProductData = GetProductData(prmProductCode)  ' 商品情報取得
      If (tmpProductData.Count = 0) Then
        ' 設定不正
        Throw New Exception("商品コード[" & prmProductCode & "]がPCA商品マスタ(SMS)に存在しません。")
      Else
        If prmDate = "" Then
          tmpTargetDate = ComGetProcDate().Replace("/", "")
        Else
          tmpTargetDate = Date.Parse(prmDate).ToString("yyyyMMdd")
        End If

        tmpSearchCondition &= " tax_ymd <= " & tmpTargetDate
        tmpSearchCondition &= " AND tax_kind = " & tmpProductData(prmTaxkind)

        _PcaDataBase.GetResult(tmpDt, ComAddSqlSearchCondition(SqlSelTax(), tmpSearchCondition))

        If tmpDt.Rows.Count > 0 Then
          If tmpProductData("smsp_tax").ToString = "0" Then
            ret = Decimal.Parse("0")
          Else
            ret = Decimal.Parse(tmpDt.Rows(0).Item("tax_rate" & tmpProductData("smsp_tax")).ToString())
          End If
        Else
          Throw New Exception("消費税設定が存在しません。")
        End If
      End If
    Catch ex As Exception
      ComWriteErrLog(ex)
      Throw New Exception("商品の消費税率取得に失敗しました。")
    End Try

    Return ret
  End Function

  ''' <summary>
  ''' PCA得意先コード取得
  ''' </summary>
  ''' <param name="prmCustomerCode">得意先コード</param>
  ''' <param name="prmNoErr">エラーメッセージ表示有無設定</param>
  ''' <returns></returns>
  Public Function GetCustomerCode(prmCustomerCode As String,
                                  Optional prmNoErr As Boolean = False) As String

    Dim ret As String = String.Empty
    Dim tmpDt As New DataTable

    Try
      _TrzDataBase.GetResult(tmpDt, SqlSelThenkan(prmCustomerCode))
      If tmpDt.Rows.Count > 0 Then
        ret = tmpDt.Rows(0).Item("TKCODE").ToString()
      Else
        If (prmNoErr = False) Then
          Throw New Exception("PCA得意先コードの[" & prmCustomerCode & "]の変換に失敗しました。")
        End If
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("PCA得意先コードの取得に失敗しました。")
    End Try

    Return ret
  End Function

  ''' <summary>
  ''' PCA仕入先コード取得
  ''' </summary>
  ''' <param name="prmSupplieCode">得意先コード</param>
  ''' <param name="prmNoErr">エラーメッセージ表示有無設定</param>
  ''' <returns></returns>
  Public Function GetSupplieCode(prmSupplieCode As String,
                                  Optional prmNoErr As Boolean = False) As String

    Dim ret As String = String.Empty
    Dim tmpDt As New DataTable

    Try
      _TrzDataBase.GetResult(tmpDt, SqlSelSihenka(prmSupplieCode))
      If tmpDt.Rows.Count > 0 Then
        ret = tmpDt.Rows(0).Item("SCODE").ToString()
      Else
        If (prmNoErr = False) Then
          Throw New Exception("PCA仕入先コードの[" & prmSupplieCode & "]の変換に失敗しました。")
        End If
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("PCA仕入先コードの取得に失敗しました。")
    End Try

    Return ret
  End Function

  ''' <summary>
  ''' 税込種別の取得（内税、外税 or 非課税）
  ''' </summary>
  ''' <param name="prmSupplieCode">仕入先コード</param>
  ''' <param name="prmProductCode">商品コード</param>
  ''' <returns></returns>
  ''' <remarks>
  ''' 税換算について
  '''   仕入先の消費税通知(tms_tax)を取得
  '''    0:免税の場合は 非課税
  '''   仕入先の税換算（tms_kan）を取得
  '''	   0:計算不要
  '''	   1:税込計算（内税）
  '''	   2:税抜計算（外税）
  '''  仕入先税換算が0の場合
  '''  商品単価マスタ（SMSP）の税区分（smsp_tax）を取得
  '''	   0：非課税
  '''	   0以外：課税
  '''  税区分が0以外の場合
  '''	 商品単価マスタ（SMSP）の税込区分(smsp_komi)を取得
  '''	   0:税抜（外税）
  '''	   1:税込（内税）
  ''' </remarks>
  Public Function GetSupplieTaxType(prmSupplieCode As String _
                                  , prmProductCode As String) As typTaxType
    Dim ret As typTaxType = typTaxType.TAX_EXCLUSIVE
    Dim tmpCustomerData As New Dictionary(Of String, String)
    Dim tmpProductData As New Dictionary(Of String, String)

    Try
      tmpCustomerData = GetSupplieBillingData(prmSupplieCode) ' 請求先情報取得
      If (tmpCustomerData.Count <> 0) Then
        ' 仕入先税通知が免税の場合は非課税
        If CInt(tmpCustomerData("rms_tax")) = 0 Then
          ' 非課税
          ret = typTaxType.TAX_NONE
        ElseIf tmpCustomerData("rms_kan") = "1" Then
          ' 内税
          ret = typTaxType.TAX_INCLUSIVE
        ElseIf tmpCustomerData("rms_kan") = "2" Then
          ' 外税
          ret = typTaxType.TAX_EXCLUSIVE
        ElseIf tmpCustomerData("rms_kan") = "0" Then
          ' 計算不要 → 商品の税込区分取得
          tmpProductData = GetProductData(GetProductCode(prmProductCode))  ' 商品情報取得
          If (tmpProductData.Count = 0) Then
            ' 設定不正
            Throw New Exception("商品税種別不正 商品コード[" & prmProductCode & "]が存在しません。")
          Else
            If tmpProductData("smsp_tax") = "0" Then
              ' 非課税
              ret = typTaxType.TAX_NONE
            ElseIf tmpProductData("smsp_komi") = "0" Then
              ' 外税
              ret = typTaxType.TAX_EXCLUSIVE
            ElseIf tmpProductData("smsp_komi") = "1" Then
              ' 内税
              ret = typTaxType.TAX_INCLUSIVE
            Else
              ' 設定不正
              Throw New Exception("商品税種別不正 商品コード[" & prmProductCode & "]の税種別設定が不正です。")
            End If
          End If
        Else
          ' 設定不正
          Throw New Exception("仕入先税種別不正 得意先コード[" & prmSupplieCode & "]の税種別設定が不正です。")
        End If
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("税込種別の取得に失敗しました。" + vbCrLf + ex.Message)
    End Try

    Return ret
  End Function

  ''' <summary>
  ''' 売上税込種別の取得（内税、外税 or 非課税）
  ''' </summary>
  ''' <param name="prmCustomerCode">得意先コード</param>
  ''' <param name="prmProductCode">商品コード</param>
  ''' <returns></returns>
  ''' <remarks>
  ''' 税換算について
  '''   得意先の消費税通知(tms_tax)を取得
  '''    0:免税の場合は 非課税
  '''   得意先の税換算（tms_kan）を取得
  '''	   0:計算不要
  '''	   1:税込計算（内税）
  '''	   2:税抜計算（外税）
  '''  得意先税換算が0の場合
  '''  商品単価マスタ（SMSP）の税区分（smsp_tax）を取得
  '''	   0：非課税
  '''	   0以外：課税
  '''  税区分が0以外の場合
  '''	 商品単価マスタ（SMSP）の税込区分(smsp_komi)を取得
  '''	   0:税抜（外税）
  '''	   1:税込（内税）
  '''	 得意先税換算が0以外の場合
  '''	 商品単価マスタの税区分（smsp_tax）を取得
  '''	   0    ：非課税
  '''	   0以外：得意先税換算に準じる
  ''' </remarks>
  Public Function GetSalesTaxType(prmCustomerCode As String _
                            , prmProductCode As String) As typTaxType
    Dim ret As typTaxType = typTaxType.TAX_EXCLUSIVE
    Dim tmpCustomerData As New Dictionary(Of String, String)
    Dim tmpProductData As New Dictionary(Of String, String)
    Dim tmpItemTaxType As typTaxType

    Try
      ' 商品の税込区分を取得
      tmpItemTaxType = GetItemTaxType(prmProductCode, typZeikomi.売上)
      If typTaxType.TAX_NONE = tmpItemTaxType Then
        ret = typTaxType.TAX_NONE
      Else
        tmpCustomerData = GetBillingData(prmCustomerCode, False) ' 得意先情報取得
        If (tmpCustomerData.Count <> 0) Then
          ' 得意先税通知が免税の場合は非課税
          If CInt(tmpCustomerData("tms_tax")) = 0 Then
            ' 非課税
            ret = typTaxType.TAX_NONE
          ElseIf tmpCustomerData("tms_kan") = "1" Then
            ' 内税
            ret = typTaxType.TAX_INCLUSIVE
          ElseIf tmpCustomerData("tms_kan") = "2" Then
            ' 外税
            ret = typTaxType.TAX_EXCLUSIVE
          ElseIf tmpCustomerData("tms_kan") = "0" Then
            ret = tmpItemTaxType
          Else
            ' 設定不正
            Throw New Exception("得意先税種別不正 得意先コード[" & prmCustomerCode & "]の税種別設定が不正です。")
          End If
        End If
      End If
    Catch ex As Exception
      ComWriteErrLog(ex)
      Throw New Exception("税込種別の取得の取得に失敗しました。")
    End Try

    Return ret
  End Function

  ''' <summary>
  ''' 仕入税込種別の取得（内税、外税 or 非課税）
  ''' </summary>
  ''' <param name="prmSupplieCode">仕入先コード</param>
  ''' <param name="prmProductCode">商品コード</param>
  ''' <returns></returns>
  Public Function GetPurchaseTaxType(prmSupplieCode As String _
                                    , prmProductCode As String) As typTaxType
    Dim ret As typTaxType = typTaxType.TAX_EXCLUSIVE
    Dim tmpSupplieData As New Dictionary(Of String, String)
    Dim tmpProductData As New Dictionary(Of String, String)
    Dim tmpItemTaxType As typTaxType

    Try
      ' 商品の税込区分を取得
      tmpItemTaxType = GetItemTaxType(prmProductCode, typZeikomi.仕入)
      If typTaxType.TAX_NONE = tmpItemTaxType Then
        ret = typTaxType.TAX_NONE
      Else
        tmpSupplieData = GetSupplieBillingData(prmSupplieCode) ' 仕入先情報取得
        If (tmpSupplieData.Count <> 0) Then
          ' 得意先税通知が免税の場合は非課税
          If CInt(tmpSupplieData("rms_tax")) = 0 Then
            ' 非課税
            ret = typTaxType.TAX_NONE
          ElseIf tmpSupplieData("rms_kan") = "1" Then
            ' 内税
            ret = typTaxType.TAX_INCLUSIVE
          ElseIf tmpSupplieData("rms_kan") = "2" Then
            ' 外税
            ret = typTaxType.TAX_EXCLUSIVE
          ElseIf tmpSupplieData("rms_kan") = "0" Then
            ret = tmpItemTaxType
          Else
            ' 設定不正
            Throw New Exception("仕入先税種別不正 仕入先コード[" & prmSupplieCode & "]の税種別設定が不正です。")
          End If
        End If
      End If
    Catch ex As Exception
      ComWriteErrLog(ex)
      Throw New Exception("税込種別の取得の取得に失敗しました。")
    End Try

    Return ret

  End Function


  ''' <summary>
  ''' 商品の税込区分を取得する
  ''' </summary>
  ''' <param name="prmProductCode">商品コード</param>
  ''' <param name="prmTaxTypeKind">税込区分種別()</param>
  ''' <returns></returns>
  Public Function GetItemTaxType(prmProductCode As String _
                               , prmTaxTypeKind As typZeikomi) As typTaxType
    Dim ret As typTaxType
    Dim tmpProductData As New Dictionary(Of String, String)
    Dim tmpTaxTypeKind As String = String.Empty

    Select Case prmTaxTypeKind
      Case typZeikomi.仕入
        tmpTaxTypeKind = "smsp_kankomi"
      Case typZeikomi.売上
        tmpTaxTypeKind = "smsp_komi"
    End Select

    Try
      ' 計算不要 → 商品の税込区分取得
      tmpProductData = GetProductData(prmProductCode)  ' 商品情報取得
      If (tmpProductData.Count = 0) Then
        ' 設定不正
        Throw New Exception("商品税種別不正 商品コード[" & prmProductCode & "]が存在しません。")
      Else
        If tmpProductData("smsp_tax") = "0" Then
          ' 非課税
          ret = typTaxType.TAX_NONE
        ElseIf tmpProductData(tmpTaxTypeKind) = "0" Then
          ' 外税
          ret = typTaxType.TAX_EXCLUSIVE
        ElseIf tmpProductData(tmpTaxTypeKind) = "1" Then
          ' 内税
          ret = typTaxType.TAX_INCLUSIVE
        Else
          ' 設定不正
          Throw New Exception("商品税種別不正 商品コード[" & prmProductCode & "]の税種別設定が不正です。")
        End If
      End If

    Catch ex As Exception
      ComWriteErrLog(ex)
      Throw New Exception("商品の税込区分取得に失敗しました。")
    End Try

    Return ret
  End Function

  ''' <summary>
  ''' 請求先情報取得
  ''' </summary>
  ''' <param name="prmCustomerCode">対象の得意先コード</param>
  ''' <returns>請求先情報</returns>
  Public Function GetBillingData(prmCustomerCode As String _
                                 , Optional prmDoTranslate As Boolean = True) As Dictionary(Of String, String)
    Dim tmpCustomerData As New Dictionary(Of String, String)
    Dim tmpPcaCustomerCode As String = String.Empty

    Try
      ' 得意先情報取得
      If prmDoTranslate = False Then
        tmpPcaCustomerCode = prmCustomerCode.PadLeft(6, "0")
      Else
        ' 変換テーブルより変換コード（PCA得意先コード）を取得
        ' 変換コードが存在しない場合にErrorを発生させない
        tmpPcaCustomerCode = GetCustomerCode(prmCustomerCode, True)
        ' 変換コードが存在しない場合は変換前コードを使用する
        If tmpCustomerData.Equals(String.Empty) Then
          tmpPcaCustomerCode = prmCustomerCode.PadLeft(6, "0")
        End If
      End If

      tmpCustomerData = GetCustomerData(tmpPcaCustomerCode)

      If (tmpCustomerData.Count = 0) Then
        Throw New Exception("得意先コード[" & prmCustomerCode & "]がPCA得意先マスタ(TMS)に存在しません。")
      Else
        ' 請求先が異なる場合は請求先データを取得
        If tmpCustomerData("tms_ocd").ToString().Trim() <> tmpPcaCustomerCode.PadLeft(6, "0"c) Then
          tmpCustomerData = GetCustomerData(tmpCustomerData("tms_ocd").ToString()) ' 得意先情報取得
        End If
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("請求先情報の取得に失敗しました。")
    End Try

    Return tmpCustomerData
  End Function

  ''' <summary>
  ''' 支払先情報取得
  ''' </summary>
  ''' <param name="prmSupplieCode">仕入先コード</param>
  ''' <returns>支払先情報</returns>
  Public Function GetSupplieBillingData(prmSupplieCode As String) As Dictionary(Of String, String)
    Dim tmpSupplieData As New Dictionary(Of String, String)
    Dim tmpPcaSupplieCode As String = String.Empty

    Try
      ' 仕入先情報取得(変換コードが存在しない場合にエラーを発生させない)
      tmpPcaSupplieCode = GetSupplieCode(prmSupplieCode, True)
      ' 変換コードが存在しない場合は変換元コードを使用
      If tmpPcaSupplieCode.Equals(String.Empty) Then
        tmpPcaSupplieCode = prmSupplieCode
      End If

      tmpSupplieData = GetSupplierData(tmpPcaSupplieCode)

      If (tmpSupplieData.Count = 0) Then
        Throw New Exception("仕入先コード[" & prmSupplieCode & "]がPCA仕入先マスタ(RMS)に存在しません。")
      Else
        ' 支払先が異なる場合は仕入先データを取得
        If tmpSupplieData("rms_ocd").ToString().Trim() <> tmpPcaSupplieCode.PadLeft(6, "0"c) Then
          tmpSupplieData = GetSupplierData(tmpSupplieData("rms_ocd").ToString()) ' 仕入先情報取得
        End If
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("支払先情報の取得に失敗しました。")
    End Try

    Return tmpSupplieData
  End Function

  ''' <summary>
  ''' 税種別（売上・仕入）を取得する
  ''' </summary>
  ''' <param name="prmProductCode">トレ蔵商品コード</param>
  ''' <returns>
  '''   連想配列で返す
  '''    売上税種別: ret("UriageZeiSyubetsu") 
  '''    仕入税種別: ret("GenkaZeiSyubetsu") 
  ''' </returns>
  Public Function GetZeiSbt(prmProductCode As String) As Dictionary(Of String, String)

    Dim ret As New Dictionary(Of String, String)

    Dim tmpDt As New DataTable
    Dim tmpProductData As New Dictionary(Of String, String)

    Try
      tmpProductData = GetProductData(prmProductCode)  ' 商品情報取得
      ret.Add("UriageZeiSyubetsu", tmpProductData("sms_kontaxkind"))
      ret.Add("GenkaZeiSyubetsu", tmpProductData("sms_kantaxkind"))
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("税種別の取得に失敗しました。")
    End Try

    Return ret
  End Function

#End Region
#End Region

#Region "IDisposable Support"
  Private disposedValue As Boolean ' 重複する呼び出しを検出するには

  ' IDisposable
  Protected Overridable Sub Dispose(disposing As Boolean)
    If Not disposedValue Then
      If disposing Then
        ' TODO: マネージド状態を破棄します (マネージド オブジェクト)。
        _PcaDataBase.Dispose()
      End If

      ' TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、下の Finalize() をオーバーライドします。
      ' TODO: 大きなフィールドを null に設定します。
    End If
    disposedValue = True
  End Sub

  ' TODO: 上の Dispose(disposing As Boolean) にアンマネージド リソースを解放するコードが含まれる場合にのみ Finalize() をオーバーライドします。
  'Protected Overrides Sub Finalize()
  '    ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
  '    Dispose(False)
  '    MyBase.Finalize()
  'End Sub

  ' このコードは、破棄可能なパターンを正しく実装できるように Visual Basic によって追加されました。
  Public Sub Dispose() Implements IDisposable.Dispose
    ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
    Dispose(True)
    ' TODO: 上の Finalize() がオーバーライドされている場合は、次の行のコメントを解除してください。
    ' GC.SuppressFinalize(Me)
  End Sub
#End Region

End Class
