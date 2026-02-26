Imports T.R.ZCommonClass.clsCommonFnc

''' <summary>
''' 売上伝票ヘッダー操作クラス
''' </summary>
Public Class clsPcaSYKH
  Inherits clsDetailBase

#Region "プロパティー"
#Region "パブリック"

  Public Property 連動区分 As String
    Get
      Return GetRowData("MitsumoriJuchuKubun")
    End Get
    Set(value As String)
      Call AddRowData("MitsumoriJuchuKubun", value)
    End Set
  End Property

  Public Property 見積No_or_受注No As String
    Get
      Return GetRowData("MitsumoriJuchuNo")
    End Get
    Set(value As String)
      Call AddRowData("MitsumoriJuchuNo", value)
    End Set
  End Property

  Public ReadOnly Property ID As String
    Get
      Return GetRowData("Id")
    End Get
  End Property

  Public Property 伝区 As String
    Get
      Return GetRowData("Denku")
    End Get
    Set(value As String)
      Call AddRowData("Denku", value)
    End Set
  End Property

  Public Property 売上日 As String
    Get
      Return GetRowData("Uriagebi")
    End Get
    Set(value As String)
      Call AddRowData("Uriagebi", value)
    End Set
  End Property

  Public Property 請求日 As String
    Get
      Return GetRowData("Seikyubi")
    End Get
    Set(value As String)
      Call AddRowData("Seikyubi", value)
    End Set
  End Property


  Public Property 伝票No As String
    Get
      Return GetRowData("DenpyoNo")
    End Get
    Set(value As String)
      Call AddRowData("DenpyoNo", value)
    End Set
  End Property

  Public Property 得意先コード As String
    Get
      Return GetRowData("TokuisakiCode")
    End Get
    Set(value As String)
      Call AddRowData("TokuisakiCode", value)
    End Set
  End Property

  Public Property 得意先名1 As String
    Get
      Return GetRowData("TokuisakiMei1")
    End Get
    Set(value As String)
      Call AddRowData("TokuisakiMei1", value)
    End Set
  End Property

  Public Property 得意先名2 As String
    Get
      Return GetRowData("TokuisakiMei2")
    End Get
    Set(value As String)
      Call AddRowData("TokuisakiMei2", value)
    End Set
  End Property
  Public Property 得意先_住所1 As String
    Get
      Return GetRowData("Jyusyo1")
    End Get
    Set(value As String)
      Call AddRowData("Jyusyo1", value)
    End Set
  End Property
  Public Property 得意先_住所2 As String
    Get
      Return GetRowData("Jyusyo2")
    End Get
    Set(value As String)
      Call AddRowData("Jyusyo2", value)
    End Set
  End Property
  Public Property 得意先_郵便番号 As String
    Get
      Return GetRowData("YubinBango")
    End Get
    Set(value As String)
      Call AddRowData("YubinBango", value)
    End Set
  End Property
  Public Property 得意先_TEL As String
    Get
      Return GetRowData("TelNo")
    End Get
    Set(value As String)
      Call AddRowData("TelNo", value)
    End Set
  End Property
  Public Property 得意先_FAX As String
    Get
      Return GetRowData("FAXNo")
    End Get
    Set(value As String)
      Call AddRowData("FAXNo", value)
    End Set
  End Property
  Public Property 得意先_敬称(相手先) As String
    Get
      Return GetRowData("Keisyo")
    End Get
    Set(value As String)
      Call AddRowData("Keisyo", value)
    End Set
  End Property
  Public Property 得意先_メールアドレス(相手先) As String
    Get
      Return GetRowData("MailAddress")
    End Get
    Set(value As String)
      Call AddRowData("MailAddress", value)
    End Set
  End Property
  Public Property 得意先_法人番号 As String
    Get
      Return GetRowData("TokuisakiHojinBango")
    End Get
    Set(value As String)
      Call AddRowData("TokuisakiHojinBango", value)
    End Set
  End Property
  Public Property 相手先ID(スポット用) As String
    Get
      Return GetRowData("SpotAitesakiId")
    End Get
    Set(value As String)
      Call AddRowData("SpotAitesakiId", value)
    End Set
  End Property
  Public Property 先方担当者ID As String
    Get
      Return GetRowData("SenpoTantosyaId")
    End Get
    Set(value As String)
      Call AddRowData("SenpoTantosyaId", value)
    End Set
  End Property
  Public Property 先方担当者名 As String
    Get
      Return GetRowData("SenpoTantosyaMei")
    End Get
    Set(value As String)
      Call AddRowData("SenpoTantosyaMei", value)
    End Set
  End Property
  Public Property 部門コード As String
    Get
      Return GetRowData("BumonCode")
    End Get
    Set(value As String)
      Call AddRowData("BumonCode", value)
    End Set
  End Property
  Public Property 担当者コード As String
    Get
      Return GetRowData("TantosyaCode")
    End Get
    Set(value As String)
      Call AddRowData("TantosyaCode", value)
    End Set
  End Property
  Public Property 摘要コード As String
    Get
      Return GetRowData("TekiyoCode")
    End Get
    Set(value As String)
      Call AddRowData("TekiyoCode", value)
    End Set
  End Property
  Public Property 摘要名 As String
    Get
      Return GetRowData("Tekiyo")
    End Get
    Set(value As String)
      Call AddRowData("Tekiyo", value)
    End Set
  End Property
  Public Property プロジェクトコード As String
    Get
      Return GetRowData("ProCode")
    End Get
    Set(value As String)
      Call AddRowData("ProCode", value)
    End Set
  End Property

  Public Property 分類コード As String
    Get
      Return GetRowData("BunruiCode")
    End Get
    Set(value As String)
      Call AddRowData("BunruiCode", value)
    End Set
  End Property
  Public Property 伝票区分 As String
    Get
      Return GetRowData("DenpyoKubun")
    End Get
    Set(value As String)
      Call AddRowData("DenpyoKubun", value)
    End Set
  End Property
  Public Property 直送先コード As String
    Get
      Return GetRowData("ChokusosakiCode")
    End Get
    Set(value As String)
      Call AddRowData("ChokusosakiCode", value)
    End Set
  End Property
  Public Property 直送先_名1 As String
    Get
      Return GetRowData("ChokusosakiMei1")
    End Get
    Set(value As String)
      Call AddRowData("ChokusosakiMei1", value)
    End Set
  End Property
  Public Property 直送先_名2 As String
    Get
      Return GetRowData("ChokusosakiMei2")
    End Get
    Set(value As String)
      Call AddRowData("ChokusosakiMei2", value)
    End Set
  End Property
  Public Property 直送先_住所1 As String
    Get
      Return GetRowData("ChokusosakiJyusyo1")
    End Get
    Set(value As String)
      Call AddRowData("ChokusosakiJyusyo1", value)
    End Set
  End Property
  Public Property 直送先_住所2 As String
    Get
      Return GetRowData("ChokusosakiJyusyo2")
    End Get
    Set(value As String)
      Call AddRowData("ChokusosakiJyusyo2", value)
    End Set
  End Property
  Public Property 直送先_郵便番号 As String
    Get
      Return GetRowData("ChokusosakiYubinBango")
    End Get
    Set(value As String)
      Call AddRowData("ChokusosakiYubinBango", value)
    End Set
  End Property
  Public Property 直送先_TEL As String
    Get
      Return GetRowData("ChokusosakiTelNo")
    End Get
    Set(value As String)
      Call AddRowData("ChokusosakiTelNo", value)
    End Set
  End Property
  Public Property 直送先_FAX As String
    Get
      Return GetRowData("ChokusosakiFAXNo")
    End Get
    Set(value As String)
      Call AddRowData("ChokusosakiFAXNo", value)
    End Set
  End Property
  Public Property 直送先_敬称 As String
    Get
      Return GetRowData("ChokusosakiKeisyo")
    End Get
    Set(value As String)
      Call AddRowData("ChokusosakiKeisyo", value)
    End Set
  End Property
  Public Property 直送先_メールアドレス As String
    Get
      Return GetRowData("ChokusosakiMailAddress")
    End Get
    Set(value As String)
      Call AddRowData("ChokusosakiMailAddress", value)
    End Set
  End Property
  Public Property 直送先_法人番号 As String
    Get
      Return GetRowData("ChokusosakiHojinBango")
    End Get
    Set(value As String)
      Call AddRowData("ChokusosakiHojinBango", value)
    End Set
  End Property
  Public Property 直送先_先方担当者名 As String
    Get
      Return GetRowData("ChokusosakiTantosyaMei")
    End Get
    Set(value As String)
      Call AddRowData("ChokusosakiTantosyaMei", value)
    End Set
  End Property
  Public Property 直送先_相手先ID(スポット用) As String
    Get
      Return GetRowData("SpotChokusoAitesakiId")
    End Get
    Set(value As String)
      Call AddRowData("SpotChokusoAitesakiId", value)
    End Set
  End Property
  Public Property 伝票No2 As String
    Get
      Return GetRowData("DenpyoNo2")
    End Get
    Set(value As String)
      Call AddRowData("DenpyoNo2", value)
    End Set
  End Property
  Public Property フセンコメント As String
    Get
      Return GetRowData("FusenComment")
    End Get
    Set(value As String)
      Call AddRowData("FusenComment", value)
    End Set
  End Property

  Public ReadOnly Property 更新Date As String
    Get
      Return GetRowData("Kosinbi")
    End Get
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


#End Region
#End Region


End Class
