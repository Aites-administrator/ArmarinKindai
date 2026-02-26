Imports T.R.ZCommonClass.clsCommonFnc

''' <summary>
''' 売上伝票明細操作クラス
''' </summary>
Public Class clsPcaSYKD
  Inherits clsDetailBase

#Region "プロパティー"
#Region "パブリック"
  Public Property 受注ヘッダーID As String
    Get
      Return GetRowData("JuchuHeaderId")
    End Get
    Set(value As String)
      Call AddRowData("JuchuHeaderId", value)
    End Set
  End Property

  Public Property 受注明細SEQ As String
    Get
      Return GetRowData("JuchuSequence")
    End Get
    Set(value As String)
      Call AddRowData("JuchuSequence", value)
    End Set
  End Property

  Public Property 明細SEQ As String
    Get
      Return GetRowData("Sequence")
    End Get
    Set(value As String)
      Call AddRowData("Sequence", value)
    End Set
  End Property
  Public Property 商品コード As String
    Get
      Return GetRowData("SyohinCode")
    End Get
    Set(value As String)
      Call AddRowData("SyohinCode", value.PadLeft(6, "0"))
    End Set
  End Property
  Public Property マスター区分 As String
    Get
      Return GetRowData("MasterKubun")
    End Get
    Set(value As String)
      Call AddRowData("MasterKubun", value)
    End Set
  End Property
  Public Property 単位区分 As String
    Get
      Return GetRowData("TaniKubun")
    End Get
    Set(value As String)
      Call AddRowData("TaniKubun", value)
    End Set
  End Property
  Public Property 税区分 As String
    Get
      Return GetRowData("ZeiKubun")
    End Get
    Set(value As String)
      Call AddRowData("ZeiKubun", value)
    End Set
  End Property
  Public Property 税込区分 As String
    Get
      Return GetRowData("ZeikomiKubun")
    End Get
    Set(value As String)
      Call AddRowData("ZeikomiKubun", value)
    End Set
  End Property
  Public Property 単価小数桁 As String
    Get
      Return GetRowData("TankaKeta")
    End Get
    Set(value As String)
      Call AddRowData("TankaKeta", value)
    End Set
  End Property
  Public Property 数量小数桁 As String
    Get
      Return GetRowData("SuryoKeta")
    End Get
    Set(value As String)
      Call AddRowData("SuryoKeta", value)
    End Set
  End Property
  Public Property 商品名 As String
    Get
      Return GetRowData("SyohinMei")
    End Get
    Set(value As String)
      Call AddRowData("SyohinMei", value)
    End Set
  End Property
  Public Property 商品名２ As String
    Get
      Return GetRowData("SyohinMei2")
    End Get
    Set(value As String)
      Call AddRowData("SyohinMei2", value)
    End Set
  End Property
  Public Property 規格_型番 As String
    Get
      Return GetRowData("KikakuKataban")
    End Get
    Set(value As String)
      Call AddRowData("KikakuKataban", value)
    End Set
  End Property
  Public Property 色 As String
    Get
      Return GetRowData("Color")
    End Get
    Set(value As String)
      Call AddRowData("Color", value)
    End Set
  End Property
  Public Property サイズ As String
    Get
      Return GetRowData("Size")
    End Get
    Set(value As String)
      Call AddRowData("Size", value)
    End Set
  End Property
  Public Property 倉庫 As String
    Get
      Return GetRowData("SokoCode")
    End Get
    Set(value As String)
      Call AddRowData("SokoCode", value)
    End Set
  End Property
  Public Property 区 As String
    Get
      Return GetRowData("Ku")
    End Get
    Set(value As String)
      Call AddRowData("Ku", value)
    End Set
  End Property
  Public Property 入数 As String
    Get
      Return GetRowData("Irisu")
    End Get
    Set(value As String)
      Call AddRowData("Irisu", value)
    End Set
  End Property
  Public Property 箱数 As String
    Get
      Return GetRowData("Hakosu")
    End Get
    Set(value As String)
      Call AddRowData("Hakosu", value)
    End Set
  End Property

  Public Property 数量 As String
    Get
      Return GetRowData("Suryo")
    End Get
    Set(value As String)
      Call AddRowData("Suryo", value)
    End Set
  End Property
  Public Property 単位 As String
    Get
      Return GetRowData("Tani")
    End Get
    Set(value As String)
      Call AddRowData("Tani", value)
    End Set
  End Property
  Public Property 単価 As String
    Get
      Return GetRowData("Tanka")
    End Get
    Set(value As String)
      Call AddRowData("Tanka", value)
    End Set
  End Property
  Public Property 原単価 As String
    Get
      Return GetRowData("GenTanka")
    End Get
    Set(value As String)
      Call AddRowData("GenTanka", value)
    End Set
  End Property
  Public Property 売単価 As String
    Get
      Return GetRowData("BaiTanka")
    End Get
    Set(value As String)
      Call AddRowData("BaiTanka", value)
    End Set
  End Property
  Public Property 金額 As String
    Get
      Return GetRowData("Kingaku")
    End Get
    Set(value As String)
      Call AddRowData("Kingaku", value)
    End Set
  End Property
  Public Property 原価金額 As String
    Get
      Return GetRowData("Genka")
    End Get
    Set(value As String)
      Call AddRowData("Genka", value)
    End Set
  End Property
  Public Property 売価金額 As String
    Get
      Return GetRowData("BaikaKingaku")
    End Get
    Set(value As String)
      Call AddRowData("BaikaKingaku", value)
    End Set
  End Property
  Public Property 粗利益 As String
    Get
      Return GetRowData("Ararieki")
    End Get
    Set(value As String)
      Call AddRowData("Ararieki", value)
    End Set
  End Property
  Public Property 備考 As String
    Get
      Return GetRowData("Biko")
    End Get
    Set(value As String)
      Call AddRowData("Biko", value)
    End Set
  End Property
  Public Property 入荷マーク As String
    Get
      Return GetRowData("NyukaMark")
    End Get
    Set(value As String)
      Call AddRowData("NyukaMark", value)
    End Set
  End Property
  Public Property 標準価格 As String
    Get
      Return GetRowData("HyojunKakaku")
    End Get
    Set(value As String)
      Call AddRowData("HyojunKakaku", value)
    End Set
  End Property
  Public Property 標準価格(入力) As String
    Get
      Return GetRowData("HyojunKakaku2")
    End Get
    Set(value As String)
      Call AddRowData("HyojunKakaku2", value)
    End Set
  End Property
  Public Property 税率 As String
    Get
      Return GetRowData("ZeiRitsu")
    End Get
    Set(value As String)
      Call AddRowData("ZeiRitsu", value)
    End Set
  End Property
  Public Property 外税額 As String
    Get
      Return GetRowData("SotoZeigaku")
    End Get
    Set(value As String)
      Call AddRowData("SotoZeigaku", value)
    End Set
  End Property
  Public Property 内税額 As String
    Get
      Return GetRowData("UchiZeigaku")
    End Get
    Set(value As String)
      Call AddRowData("UchiZeigaku", value)
    End Set
  End Property
  Public Property 計算式コード As String
    Get
      Return GetRowData("SyokonKeisan")
    End Get
    Set(value As String)
      Call AddRowData("SyokonKeisan", value)
    End Set
  End Property
  Public Property 商品項目1 As String
    Get
      Return GetRowData("SyohinKomoku1")
    End Get
    Set(value As String)
      Call AddRowData("SyohinKomoku1", value)
    End Set
  End Property
  Public Property 商品項目2 As String
    Get
      Return GetRowData("SyohinKomoku2")
    End Get
    Set(value As String)
      Call AddRowData("SyohinKomoku2", value)
    End Set
  End Property
  Public Property 商品項目3 As String
    Get
      Return GetRowData("SyohinKomoku3")
    End Get
    Set(value As String)
      Call AddRowData("SyohinKomoku3", value)
    End Set
  End Property
  Public Property 売上項目1 As String
    Get
      Return GetRowData("UriageKomoku1")
    End Get
    Set(value As String)
      Call AddRowData("UriageKomoku1", value)
    End Set
  End Property
  Public Property 売上項目2 As String
    Get
      Return GetRowData("UriageKomoku2")
    End Get
    Set(value As String)
      Call AddRowData("UriageKomoku2", value)
    End Set
  End Property
  Public Property 売上項目3 As String
    Get
      Return GetRowData("UriageKomoku3")
    End Get
    Set(value As String)
      Call AddRowData("UriageKomoku3", value)
    End Set
  End Property
  Public Property 出荷指示 As String
    Get
      Return GetRowData("SyukaShiji")
    End Get
    Set(value As String)
      Call AddRowData("SyukaShiji", value)
    End Set
  End Property
  Public Property 予備1 As String
    Get
      Return GetRowData("YobiShort1")
    End Get
    Set(value As String)
      Call AddRowData("YobiShort1", value)
    End Set
  End Property
  Public Property 予備2 As String
    Get
      Return GetRowData("YobiShort2")
    End Get
    Set(value As String)
      Call AddRowData("YobiShort2", value)
    End Set
  End Property
  Public Property 予備3 As String
    Get
      Return GetRowData("YobiShort3")
    End Get
    Set(value As String)
      Call AddRowData("YobiShort3", value)
    End Set
  End Property
  Public Property 予備数値1 As String
    Get
      Return GetRowData("YobiInt1")
    End Get
    Set(value As String)
      Call AddRowData("YobiInt1", value)
    End Set
  End Property
  Public Property 予備数値2 As String
    Get
      Return GetRowData("YobiInt2")
    End Get
    Set(value As String)
      Call AddRowData("YobiInt2", value)
    End Set
  End Property
  Public Property 予備数値3 As String
    Get
      Return GetRowData("YobiInt3")
    End Get
    Set(value As String)
      Call AddRowData("YobiInt3", value)
    End Set
  End Property
  Public Property 予備金額1 As String
    Get
      Return GetRowData("YobiDecimal1")
    End Get
    Set(value As String)
      Call AddRowData("YobiDecimal1", value)
    End Set
  End Property
  Public Property 予備金額2 As String
    Get
      Return GetRowData("YobiDecimal2")
    End Get
    Set(value As String)
      Call AddRowData("YobiDecimal2", value)
    End Set
  End Property
  Public Property 予備金額3 As String
    Get
      Return GetRowData("YobiDecimal3")
    End Get
    Set(value As String)
      Call AddRowData("YobiDecimal3", value)
    End Set
  End Property
  Public Property 予備文字列1 As String
    Get
      Return GetRowData("YobiString1")
    End Get
    Set(value As String)
      Call AddRowData("YobiString1", value)
    End Set
  End Property
  Public Property 予備文字列2 As String
    Get
      Return GetRowData("YobiString2")
    End Get
    Set(value As String)
      Call AddRowData("YobiString2", value)
    End Set
  End Property
  Public Property 予備文字列3 As String
    Get
      Return GetRowData("YobiString3")
    End Get
    Set(value As String)
      Call AddRowData("YobiString3", value)
    End Set
  End Property
  Public Property 入数小数桁 As String
    Get
      Return GetRowData("IrisuKeta")
    End Get
    Set(value As String)
      Call AddRowData("IrisuKeta", value)
    End Set
  End Property
  Public Property 箱数小数桁 As String
    Get
      Return GetRowData("HakosuKeta")
    End Get
    Set(value As String)
      Call AddRowData("HakosuKeta", value)
    End Set
  End Property
  Public Property ロットID As String
    Get
      Return GetRowData("LotId")
    End Get
    Set(value As String)
      Call AddRowData("LotId", value)
    End Set
  End Property
  Public Property 売上税種別 As String
    Get
      Return GetRowData("UriageZeiSyubetsu")
    End Get
    Set(value As String)
      Call AddRowData("UriageZeiSyubetsu", value)
    End Set
  End Property
  Public Property 原価税込区分 As String
    Get
      Return GetRowData("GenkaZeikomiKubun")
    End Get
    Set(value As String)
      Call AddRowData("GenkaZeikomiKubun", value)
    End Set
  End Property
  Public Property 原価税率 As String
    Get
      Return GetRowData("GenkaZeiRitsu")
    End Get
    Set(value As String)
      Call AddRowData("GenkaZeiRitsu", value)
    End Set
  End Property
  Public Property 原価税種別 As String
    Get
      Return GetRowData("GenkaZeiSyubetsu")
    End Get
    Set(value As String)
      Call AddRowData("GenkaZeiSyubetsu", value)
    End Set
  End Property

#End Region

#End Region

End Class
