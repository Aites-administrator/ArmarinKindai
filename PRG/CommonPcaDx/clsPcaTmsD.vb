Option Strict Off
Imports T.R.ZCommonClass.clsCommonFnc

''' <summary>
''' 得意先マスタデータ定義クラス
''' </summary>
Public Class clsPcaTmsD
  Implements IDisposable


#Region "メンバ"
#Region "プライベート"
  Private _PostData As New Dictionary(Of String, String)
#End Region
#End Region

#Region "プロパティ"

#Region "パブリック"

  ''' <summary>
  ''' 設定データをXML形式で出力
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property XML As String
    Get
      Return ComDic2XmlText(_PostData)
    End Get
  End Property

    ''' <summary>
    ''' 得意先コード
    ''' </summary>
    Public Property エンティティバージョン As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("BEVersion") Then
                Return _PostData("BEVersion")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "BEVersion", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先コード
    ''' </summary>
    Public Property 得意先コード As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiCode") Then
                Return _PostData("TokuisakiCode")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiCode", value)
        End Set
    End Property

    ''' <summary>
    ''' 相手先ID
    ''' </summary>
    Public Property 相手先ID As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("AitesakiId") Then
                Return _PostData("AitesakiId")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "AitesakiId", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先名1
    ''' </summary>
    Public Property 得意先名1 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiMei1") Then
                Return _PostData("TokuisakiMei1")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiMei1", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先名2
    ''' </summary>
    Public Property 得意先名2 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiMei2") Then
                Return _PostData("TokuisakiMei2")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiMei2", value)
        End Set
    End Property

    ''' <summary>
    ''' 相手先敬称
    ''' </summary>
    Public Property 相手先敬称 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("AitesakiKeisyo") Then
                Return _PostData("AitesakiKeisyo")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "AitesakiKeisyo", value)
        End Set
    End Property

    ''' <summary>
    ''' ｶﾅ索引
    ''' </summary>
    Public Property ｶﾅ索引 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("KanaSakuin") Then
                Return _PostData("KanaSakuin")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "KanaSakuin", value)
        End Set
    End Property

    ''' <summary>
    ''' 郵便番号
    ''' </summary>
    Public Property 郵便番号 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("YubinBango") Then
                Return _PostData("YubinBango")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "YubinBango", value)
        End Set
    End Property

    ''' <summary>
    ''' 住所1
    ''' </summary>
    Public Property 住所1 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Jyusyo1") Then
                Return _PostData("Jyusyo1")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "Jyusyo1", value)
        End Set
    End Property

    ''' <summary>
    ''' 住所2
    ''' </summary>
    Public Property 住所2 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Jyusyo2") Then
                Return _PostData("Jyusyo2")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "Jyusyo2", value)
        End Set
    End Property

    ''' <summary>
    ''' 相手先TEL
    ''' </summary>
    Public Property 相手先TEL As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("AitesakiTelNo") Then
                Return _PostData("AitesakiTelNo")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "AitesakiTelNo", value)
        End Set
    End Property

    ''' <summary>
    ''' 相手先FAX
    ''' </summary>
    Public Property 相手先FAX As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("AitesakiFaxNo") Then
                Return _PostData("AitesakiFaxNo")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "AitesakiFaxNo", value)
        End Set
    End Property

    ''' <summary>
    ''' 相手先メールアドレス
    ''' </summary>
    Public Property 相手先メールアドレス As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("AitesakiMailAddress") Then
                Return _PostData("AitesakiMailAddress")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "AitesakiMailAddress", value)
        End Set
    End Property

    ''' <summary>
    ''' 企業コード1
    ''' </summary>
    Public Property 企業コード1 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("KigyoCode1") Then
                Return _PostData("KigyoCode1")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "KigyoCode1", value)
        End Set
    End Property

    ''' <summary>
    ''' 企業コード2
    ''' </summary>
    Public Property 企業コード2 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("KigyoCode2") Then
                Return _PostData("KigyoCode2")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "KigyoCode2", value)
        End Set
    End Property

    ''' <summary>
    ''' 法人番号
    ''' </summary>
    Public Property 法人番号 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("HojinBango") Then
                Return _PostData("HojinBango")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "HojinBango", value)
        End Set
    End Property

    ''' <summary>
    ''' 先方担当者ID
    ''' </summary>
    Public Property 先方担当者ID As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SenpoTantosyaId") Then
                Return _PostData("SenpoTantosyaId")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SenpoTantosyaId", value)
        End Set
    End Property

    ''' <summary>
    ''' 先方担当者名
    ''' </summary>
    Public Property 先方担当者名 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SenpoTantosyaMei") Then
                Return _PostData("SenpoTantosyaMei")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SenpoTantosyaMei", value)
        End Set
    End Property

    ''' <summary>
    ''' 所属部署
    ''' </summary>
    Public Property 所属部署 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TantoBusyo") Then
                Return _PostData("TantoBusyo")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TantoBusyo", value)
        End Set
    End Property

    ''' <summary>
    ''' 役職
    ''' </summary>
    Public Property 役職 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Yakusyoku") Then
                Return _PostData("Yakusyoku")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "Yakusyoku", value)
        End Set
    End Property

    ''' <summary>
    ''' 先方担当者敬称
    ''' </summary>
    Public Property 先方担当者敬称 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SenpoTantosyaKeisyo") Then
                Return _PostData("SenpoTantosyaKeisyo")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SenpoTantosyaKeisyo", value)
        End Set
    End Property

    ''' <summary>
    ''' 先方担当者TEL
    ''' </summary>
    Public Property 先方担当者TEL As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SenpoTantosyaTelNo") Then
                Return _PostData("SenpoTantosyaTelNo")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SenpoTantosyaTelNo", value)
        End Set
    End Property

    ''' <summary>
    ''' 先方担当者FAX
    ''' </summary>
    Public Property 先方担当者FAX As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SenpoTantosyaFaxNo") Then
                Return _PostData("SenpoTantosyaFaxNo")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SenpoTantosyaFaxNo", value)
        End Set
    End Property

    ''' <summary>
    ''' 携帯番号
    ''' </summary>
    Public Property 携帯番号 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("KeitaiTelNo") Then
                Return _PostData("KeitaiTelNo")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "KeitaiTelNo", value)
        End Set
    End Property

    ''' <summary>
    ''' 先方担当者メールアドレス
    ''' </summary>
    Public Property 先方担当者メールアドレス As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SenpoTantosyaMailAddress") Then
                Return _PostData("SenpoTantosyaMailAddress")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SenpoTantosyaMailAddress", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先区分1
    ''' </summary>
    Public Property 得意先区分1 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiKubun1") Then
                Return _PostData("TokuisakiKubun1")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiKubun1", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先区分2
    ''' </summary>
    Public Property 得意先区分2 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiKubun2") Then
                Return _PostData("TokuisakiKubun2")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiKubun2", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先区分3
    ''' </summary>
    Public Property 得意先区分3 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiKubun3") Then
                Return _PostData("TokuisakiKubun3")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiKubun3", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先区分4
    ''' </summary>
    Public Property 得意先区分4 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiKubun4") Then
                Return _PostData("TokuisakiKubun4")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiKubun4", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先区分5
    ''' </summary>
    Public Property 得意先区分5 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiKubun5") Then
                Return _PostData("TokuisakiKubun5")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiKubun5", value)
        End Set
    End Property

    ''' <summary>
    ''' 主担当者
    ''' </summary>
    Public Property 主担当者 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Syutantosya") Then
                Return _PostData("Syutantosya")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "Syutantosya", value)
        End Set
    End Property

    ''' <summary>
    ''' 主部門
    ''' </summary>
    Public Property 主部門 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyuBumon") Then
                Return _PostData("SyuBumon")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SyuBumon", value)
        End Set
    End Property

    ''' <summary>
    ''' 適用売価No
    ''' </summary>
    Public Property 適用売価No As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TekiyoBaikaNo") Then
                Return _PostData("TekiyoBaikaNo")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TekiyoBaikaNo", value)
        End Set
    End Property

    ''' <summary>
    ''' 掛率
    ''' </summary>
    Public Property 掛率 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Kakeritsu") Then
                Return _PostData("Kakeritsu")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "Kakeritsu", value)
        End Set
    End Property

    ''' <summary>
    ''' 実績管理
    ''' </summary>
    Public Property 実績管理 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("JissekiKanri") Then
                Return _PostData("JissekiKanri")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "JissekiKanri", value)
        End Set
    End Property

    ''' <summary>
    ''' 売上日印字
    ''' </summary>
    Public Property 売上日印字 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("UriagebiInji") Then
                Return _PostData("UriagebiInji")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "UriagebiInji", value)
        End Set
    End Property

    ''' <summary>
    ''' 税換算
    ''' </summary>
    Public Property 税換算 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("ZeiKansan") Then
                Return _PostData("ZeiKansan")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "ZeiKansan", value)
        End Set
    End Property

    ''' <summary>
    ''' 売単価換算
    ''' </summary>
    Public Property 売単価換算 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("UritankaKansan") Then
                Return _PostData("UritankaKansan")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "UritankaKansan", value)
        End Set
    End Property

    ''' <summary>
    ''' 納品書用紙
    ''' </summary>
    Public Property 納品書用紙 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("NohinsyoYoshi") Then
                Return _PostData("NohinsyoYoshi")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "NohinsyoYoshi", value)
        End Set
    End Property

    ''' <summary>
    ''' 納品書社名
    ''' </summary>
    Public Property 納品書社名 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("NohinsyoSyamei") Then
                Return _PostData("NohinsyoSyamei")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "NohinsyoSyamei", value)
        End Set
    End Property

    ''' <summary>
    ''' 社店コード
    ''' </summary>
    Public Property 社店コード As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("	SyatenCode") Then
                Return _PostData("SyatenCode")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SyatenCode", value)
        End Set
    End Property

    ''' <summary>
    ''' 分類コード
    ''' </summary>
    Public Property 分類コード As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("BunruiCode") Then
                Return _PostData("BunruiCode")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "BunruiCode", value)
        End Set
    End Property

    ''' <summary>
    ''' 伝票区分
    ''' </summary>
    Public Property 伝票区分 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("DenpyoKubun") Then
                Return _PostData("DenpyoKubun")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "DenpyoKubun", value)
        End Set
    End Property

    ''' <summary>
    ''' チェーンストア取引先コード
    ''' </summary>
    Public Property チェーンストア取引先コード As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("StoreTorihikisakiCode") Then
                Return _PostData("StoreTorihikisakiCode")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "StoreTorihikisakiCode", value)
        End Set
    End Property

    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    Public Property 請求先コード As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SeikyusakiCode") Then
                Return _PostData("SeikyusakiCode")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SeikyusakiCode", value)
        End Set
    End Property

    ''' <summary>
    ''' 請求締日
    ''' </summary>
    Public Property 請求締日 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SeikyuShimebi") Then
                Return _PostData("SeikyuShimebi")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SeikyuShimebi", value)
        End Set
    End Property

    ''' <summary>
    ''' 与信限度額
    ''' </summary>
    Public Property 与信限度額 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("YoshinGendogaku") Then
                Return _PostData("YoshinGendogaku")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "YoshinGendogaku", value)
        End Set
    End Property

    ''' <summary>
    ''' 繰越残高(売掛金残高)
    ''' </summary>
    Public Property 繰越残高_売掛金残高 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("UrikakekinZandaka") Then
                Return _PostData("UrikakekinZandaka")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "UrikakekinZandaka", value)
        End Set
    End Property

    ''' <summary>
    ''' 金額端数
    ''' </summary>
    Public Property 金額端数 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("KingakuHasu") Then
                Return _PostData("KingakuHasu")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "KingakuHasu", value)
        End Set
    End Property

    ''' <summary>
    ''' 消費税端数
    ''' </summary>
    Public Property 消費税端数 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohizeiHasu") Then
                Return _PostData("SyohizeiHasu")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SyohizeiHasu", value)
        End Set
    End Property

    ''' <summary>
    ''' 消費税通知
    ''' </summary>
    Public Property 消費税通知 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohizeiTsuchi") Then
                Return _PostData("SyohizeiTsuchi")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SyohizeiTsuchi", value)
        End Set
    End Property

    ''' <summary>
    ''' 請求書用紙
    ''' </summary>
    Public Property 請求書用紙 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SeikyusyoYoshi") Then
                Return _PostData("SeikyusyoYoshi")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SeikyusyoYoshi", value)
        End Set
    End Property

    ''' <summary>
    ''' 請求書社名
    ''' </summary>
    Public Property 請求書社名 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SeikyusyoSyamei") Then
                Return _PostData("SeikyusyoSyamei")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SeikyusyoSyamei", value)
        End Set
    End Property

    ''' <summary>
    ''' 回収種別1
    ''' </summary>
    Public Property 回収種別1 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("KaisyuSyubetsu1") Then
                Return _PostData("KaisyuSyubetsu1")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "KaisyuSyubetsu1", value)
        End Set
    End Property

    ''' <summary>
    ''' 回収種別2
    ''' </summary>
    Public Property 回収種別2 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("KaisyuSyubetsu2") Then
                Return _PostData("KaisyuSyubetsu2")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "KaisyuSyubetsu2", value)
        End Set
    End Property

    ''' <summary>
    ''' 種別境界額
    ''' </summary>
    Public Property 種別境界額 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyubetsuKyokaigaku") Then
                Return _PostData("SyubetsuKyokaigaku")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SyubetsuKyokaigaku", value)
        End Set
    End Property

    ''' <summary>
    ''' 回収日
    ''' </summary>
    Public Property 回収日 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Kaisyubi") Then
                Return _PostData("Kaisyubi")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "Kaisyubi", value)
        End Set
    End Property

    ''' <summary>
    ''' 回収方法
    ''' </summary>
    Public Property 回収方法 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("KaisyuHoho") Then
                Return _PostData("KaisyuHoho")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "KaisyuHoho", value)
        End Set
    End Property

    ''' <summary>
    ''' 会社口座ID
    ''' </summary>
    Public Property 会社口座ID As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("KozaId") Then
                Return _PostData("KozaId")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "KozaId", value)
        End Set
    End Property

    ''' <summary>
    ''' コメント
    ''' </summary>
    Public Property コメント As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Comment") Then
                Return _PostData("Comment")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "Comment", value)
        End Set
    End Property

    ''' <summary>
    ''' 使用区分
    ''' </summary>
    Public Property 使用区分 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("ShiyoKubun") Then
                Return _PostData("ShiyoKubun")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "ShiyoKubun", value)
        End Set
    End Property

    ''' <summary>
    ''' 登録ユーザー
    ''' </summary>
    Public Property 登録ユーザー As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TorokuUser") Then
                Return _PostData("TorokuUser")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TorokuUser", value)
        End Set
    End Property

    ''' <summary>
    ''' 登録PG
    ''' </summary>
    Public Property 登録PG As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TorokuProgram") Then
                Return _PostData("TorokuProgram")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TorokuProgram", value)
        End Set
    End Property

    ''' <summary>
    ''' 登録Date
    ''' </summary>
    Public Property 登録Date As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Torokubi") Then
                Return _PostData("Torokubi")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "Torokubi", value)
        End Set
    End Property

    ''' <summary>
    ''' 更新ユーザー
    ''' </summary>
    Public Property 更新ユーザー As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("KosinUser") Then
                Return _PostData("KosinUser")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "KosinUser", value)
        End Set
    End Property

    ''' <summary>
    ''' 更新PG
    ''' </summary>
    Public Property 更新PG As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("KosinProgram") Then
                Return _PostData("KosinProgram")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "KosinProgram", value)
        End Set
    End Property

    ''' <summary>
    ''' 更新Date
    ''' </summary>
    Public Property 更新Date As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Kosinbi") Then
                Return _PostData("Kosinbi")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "Kosinbi", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先予備1
    ''' </summary>
    Public Property 得意先予備1 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiYobiShort1") Then
                Return _PostData("TokuisakiYobiShort1")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiYobiShort1", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先予備2
    ''' </summary>
    Public Property 得意先予備2 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiYobiShort2") Then
                Return _PostData("TokuisakiYobiShort2")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiYobiShort2", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先予備3
    ''' </summary>
    Public Property 得意先予備3 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiYobiShort3") Then
                Return _PostData("TokuisakiYobiShort3")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiYobiShort3", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先予備数値1
    ''' </summary>
    Public Property 得意先予備数値1 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiYobiInt1") Then
                Return _PostData("TokuisakiYobiInt1")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiYobiInt1", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先予備数値2
    ''' </summary>
    Public Property 得意先予備数値2 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiYobiInt2") Then
                Return _PostData("TokuisakiYobiInt2")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiYobiInt2", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先予備数値3
    ''' </summary>
    Public Property 得意先予備数値3 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiYobiInt3") Then
                Return _PostData("TokuisakiYobiInt3")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiYobiInt3", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先予備金額1
    ''' </summary>
    Public Property 得意先予備金額1 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiYobiDecimal1") Then
                Return _PostData("TokuisakiYobiDecimal1")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiYobiDecimal1", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先予備金額2
    ''' </summary>
    Public Property 得意先予備金額2 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiYobiDecimal2") Then
                Return _PostData("TokuisakiYobiDecimal2")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiYobiDecimal2", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先予備金額3
    ''' </summary>
    Public Property 得意先予備金額3 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiYobiDecimal3") Then
                Return _PostData("TokuisakiYobiDecimal3")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiYobiDecimal3", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先予備文字列1
    ''' </summary>
    Public Property 得意先予備文字列1 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiYobiString1") Then
                Return _PostData("TokuisakiYobiString1")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiYobiString1", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先予備文字列2
    ''' </summary>
    Public Property 得意先予備文字列2 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiYobiString2") Then
                Return _PostData("TokuisakiYobiString2")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiYobiString2", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先予備文字列3
    ''' </summary>
    Public Property 得意先予備文字列3 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TokuisakiYobiString3") Then
                Return _PostData("TokuisakiYobiString3")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TokuisakiYobiString3", value)
        End Set
    End Property

    ''' <summary>
    ''' 得意先フラグ
    ''' </summary>
    Public Property 得意先フラグ As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("IsTmsflg") Then
                Return _PostData("IsTmsflg")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "IsTmsflg", value)
        End Set
    End Property

    ''' <summary>
    ''' 仕入先フラグ
    ''' </summary>
    Public Property 仕入先フラグ As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("IsRmsflg") Then
                Return _PostData("IsRmsflg")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "IsRmsflg", value)
        End Set
    End Property

    ''' <summary>
    ''' 出荷先フラグ
    ''' </summary>
    Public Property 出荷先フラグ As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("IsXmsflg") Then
                Return _PostData("IsXmsflg")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "IsXmsflg", value)
        End Set
    End Property

    ''' <summary>
    ''' 直送先フラグ
    ''' </summary>
    Public Property 直送先フラグ As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("IsYmsflg") Then
                Return _PostData("IsYmsflg")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "IsYmsflg", value)
        End Set
    End Property

    ''' <summary>
    ''' 決済会社フラグ
    ''' </summary>
    Public Property 決済会社フラグ As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("IsDmsflg") Then
                Return _PostData("IsDmsflg")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "IsDmsflg", value)
        End Set
    End Property


    ''' <summary>
    ''' コード0フラグ
    ''' </summary>
    Public Property コード0フラグ As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("IsZeroflg") Then
                Return _PostData("IsZeroflg")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "IsZeroflg", value)
        End Set
    End Property


    ''' <summary>
    ''' 相手先登録ユーザー
    ''' </summary>
    Public Property 相手先登録ユーザー As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("AitesakiTorokuUser") Then
                Return _PostData("AitesakiTorokuUser")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "AitesakiTorokuUser", value)
        End Set
    End Property


    ''' <summary>
    ''' 相手先登録PG
    ''' </summary>
    Public Property 相手先登録PG As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("AitesakiTorokuProgram") Then
                Return _PostData("AitesakiTorokuProgram")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "AitesakiTorokuProgram", value)
        End Set
    End Property


    ''' <summary>
    ''' 相手先登録Date
    ''' </summary>
    Public Property 相手先登録Date As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("AitesakiTorokubi") Then
                Return _PostData("AitesakiTorokubi")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "AitesakiTorokubi", value)
        End Set
    End Property


    ''' <summary>
    ''' 相手先更新ユーザー
    ''' </summary>
    Public Property 相手先更新ユーザー As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("AitesakiKosinUser") Then
                Return _PostData("AitesakiKosinUser")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "AitesakiKosinUser", value)
        End Set
    End Property


    ''' <summary>
    ''' 相手先更新PG
    ''' </summary>
    Public Property 相手先更新PG As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("AitesakiKosinProgram") Then
                Return _PostData("AitesakiKosinProgram")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "AitesakiKosinProgram", value)
        End Set
    End Property


    ''' <summary>
    ''' 相手先更新Date
    ''' </summary>
    Public Property 相手先更新Date As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("AitesakiKosinbi") Then
                Return _PostData("AitesakiKosinbi")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "AitesakiKosinbi", value)
        End Set
    End Property


    ''' <summary>
    ''' 相手先予備1
    ''' </summary>
    Public Property 相手先予備1 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("AitesakiYobiShort1") Then
                Return _PostData("AitesakiYobiShort1")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "AitesakiYobiShort1", value)
        End Set
    End Property


    ''' <summary>
    ''' 相手先予備数値1
    ''' </summary>
    Public Property 相手先予備数値1 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("AitesakiYobiInt1") Then
                Return _PostData("AitesakiYobiInt1")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "AitesakiYobiInt1", value)
        End Set
    End Property


    ''' <summary>
    ''' 相手先予備金額1
    ''' </summary>
    Public Property 相手先予備金額1 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("AitesakiYobiDecimal1") Then
                Return _PostData("AitesakiYobiDecimal1")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "AitesakiYobiDecimal1", value)
        End Set
    End Property


    ''' <summary>
    ''' 相手先予備文字列1
    ''' </summary>
    Public Property 相手先予備文字列1 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("AitesakiYobiString1") Then
                Return _PostData("AitesakiYobiString1")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "AitesakiYobiString1", value)
        End Set
    End Property


    ''' <summary>
    ''' 相手先予備文字列2
    ''' </summary>
    Public Property 相手先予備文字列2 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("AitesakiYobiString2") Then
                Return _PostData("AitesakiYobiString2")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "AitesakiYobiString2", value)
        End Set
    End Property

    ''' <summary>
    ''' 相手先予備文字列2
    ''' </summary>
    Public Property 相手先予備文字列3 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("AitesakiYobiString3") Then
                Return _PostData("AitesakiYobiString3")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "AitesakiYobiString3", value)
        End Set
    End Property


    ''' <summary>
    ''' 先方担当者登録ユーザー
    ''' </summary>
    Public Property 先方担当者登録ユーザー As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SenpoTantosyaTorokuUser") Then
                Return _PostData("SenpoTantosyaTorokuUser")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SenpoTantosyaTorokuUser", value)
        End Set
    End Property


    ''' <summary>
    ''' 先方担当者登録PG
    ''' </summary>
    Public Property 先方担当者登録PG As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SenpoTantosyaTorokuProgram") Then
                Return _PostData("SenpoTantosyaTorokuProgram")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SenpoTantosyaTorokuProgram", value)
        End Set
    End Property


    ''' <summary>
    ''' 先方担当者登録Date
    ''' </summary>
    Public Property 先方担当者登録Date As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SenpoTantosyaTorokubi") Then
                Return _PostData("SenpoTantosyaTorokubi")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SenpoTantosyaTorokubi", value)
        End Set
    End Property


    ''' <summary>
    ''' 先方担当者更新ユーザー
    ''' </summary>
    Public Property 先方担当者更新ユーザー As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SenpoTantosyaKosinUser") Then
                Return _PostData("SenpoTantosyaKosinUser")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SenpoTantosyaKosinUser", value)
        End Set
    End Property


    ''' <summary>
    ''' 先方担当者更新PG
    ''' </summary>
    Public Property 先方担当者更新PG As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SenpoTantosyaKosinProgram") Then
                Return _PostData("SenpoTantosyaKosinProgram")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SenpoTantosyaKosinProgram", value)
        End Set
    End Property


    ''' <summary>
    ''' 先方担当者更新Date
    ''' </summary>
    Public Property 先方担当者更新Date As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SenpoTantosyaKosinbi") Then
                Return _PostData("SenpoTantosyaKosinbi")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SenpoTantosyaKosinbi", value)
        End Set
    End Property

    ''' <summary>
    ''' 先方担当者予備1
    ''' </summary>
    Public Property 先方担当者予備1 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SenpoTantosyaYobiShort1") Then
                Return _PostData("SenpoTantosyaYobiShort1")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SenpoTantosyaYobiShort1", value)
        End Set
    End Property

    ''' <summary>
    ''' 先方担当者予備数値1
    ''' </summary>
    Public Property 先方担当者予備数値1 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SenpoTantosyaYobiInt1") Then
                Return _PostData("SenpoTantosyaYobiInt1")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SenpoTantosyaYobiInt1", value)
        End Set
    End Property

    ''' <summary>
    ''' 先方担当者予備金額1
    ''' </summary>
    Public Property 先方担当者予備金額1 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SenpoTantosyaYobiDecimal1") Then
                Return _PostData("SenpoTantosyaYobiDecimal1")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SenpoTantosyaYobiDecimal1", value)
        End Set
    End Property

    ''' <summary>
    ''' 先方担当者予備文字列1
    ''' </summary>
    Public Property 先方担当者予備文字列1 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SenpoTantosyaYobiString1") Then
                Return _PostData("SenpoTantosyaYobiString1")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SenpoTantosyaYobiString1", value)
        End Set
    End Property

    ''' <summary>
    ''' 先方担当者予備文字列2
    ''' </summary>
    Public Property 先方担当者予備文字列2 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SenpoTantosyaYobiString2") Then
                Return _PostData("SenpoTantosyaYobiString2")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SenpoTantosyaYobiString2", value)
        End Set
    End Property

    ''' <summary>
    ''' 先方担当者予備文字列3
    ''' </summary>
    Public Property 先方担当者予備文字列3 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SenpoTantosyaYobiString3") Then
                Return _PostData("SenpoTantosyaYobiString3")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SenpoTantosyaYobiString3", value)
        End Set
    End Property

    ''' <summary>
    ''' 	最終請求期間TO
    ''' </summary>
    Public Property 最終請求期間TO As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SeikyuKikanTo") Then
                Return _PostData("SeikyuKikanTo")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SeikyuKikanTo", value)
        End Set
    End Property

    ''' <summary>
    ''' 請求残高
    ''' </summary>
    Public Property 請求残高 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SeikyuZandaka") Then
                Return _PostData("SeikyuZandaka")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SeikyuZandaka", value)
        End Set
    End Property

    ''' <summary>
    ''' 請求後入金
    ''' </summary>
    Public Property 請求後入金 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SeikyugoNyukin") Then
                Return _PostData("SeikyugoNyukin")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SeikyugoNyukin", value)
        End Set
    End Property

    ''' <summary>
    ''' 未請求売上
    ''' </summary>
    Public Property 未請求売上 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("MiseikyuUriage") Then
                Return _PostData("MiseikyuUriage")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "MiseikyuUriage", value)
        End Set
    End Property

    ''' <summary>
    ''' 残高更新Date
    ''' </summary>
    Public Property 残高更新Date As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("ZandakaKosinbi") Then
                Return _PostData("ZandakaKosinbi")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "ZandakaKosinbi", value)
        End Set
    End Property

    ''' <summary>
    ''' 送り状用紙
    ''' </summary>
    Public Property 送り状用紙 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("	OkurijyoYoshi") Then
                Return _PostData("	OkurijyoYoshi")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "OkurijyoYoshi", value)
        End Set
    End Property

    ''' <summary>
    ''' 休日対応
    ''' </summary>
    Public Property 休日対応 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("KyujitsuTaio") Then
                Return _PostData("KyujitsuTaio")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "KyujitsuTaio", value)
        End Set
    End Property

    ''' <summary>
    ''' 休日カレンダーID
    ''' </summary>
    Public Property 休日カレンダーID As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("KyujitsuCalendarId") Then
                Return _PostData("KyujitsuCalendarId")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "KyujitsuCalendarId", value)
        End Set
    End Property

    ''' <summary>
    ''' 部門ロック
    ''' </summary>
    Public Property 部門ロック As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("BumonLockType") Then
                Return _PostData("BumonLockType")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "BumonLockType", value)
        End Set
    End Property

    ''' <summary>
    ''' 請求後入金(前受金)
    ''' </summary>
    Public Property 請求後入金_前受金 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SeikyugoNyukinMaeuke") Then
                Return _PostData("SeikyugoNyukinMaeuke")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SeikyugoNyukinMaeuke", value)
        End Set
    End Property

    ''' <summary>
    ''' 請求後振替(前受金⇒売掛金)
    ''' </summary>
    Public Property 請求後振替_前受金売掛金 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SeikyugoNyukinFurikae") Then
                Return _PostData("SeikyugoNyukinFurikae")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "SeikyugoNyukinFurikae", value)
        End Set
    End Property

    ''' <summary>
    ''' 前受金残高
    ''' </summary>
    Public Property 前受金残高 As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("MaeukeZandaka") Then
                Return _PostData("MaeukeZandaka")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "MaeukeZandaka", value)
        End Set
    End Property

    ''' <summary>
    ''' 最終更新日時（From）
    ''' </summary>
    ''' <remarks>詳細検索専用</remarks>
    Public WriteOnly Property 登録変更期間_開始 As String
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "KosinbiFrom", value)
        End Set
    End Property

    ''' <summary>
    ''' 最終更新日時（To）
    ''' </summary>
    ''' <remarks>詳細検索専用</remarks>
    Public WriteOnly Property 登録変更期間_終了 As String
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "KosinbiTo", value)
        End Set
    End Property

#End Region

#End Region

#Region "メソッド"
#Region "パブリック"
    Public Sub AddRowData(prmKey As String _
                      , prmVal As String)
    Call ComSetDictionaryVal(_PostData, prmKey, prmVal)
  End Sub
#End Region
#End Region

#Region "IDisposable Support"
  Private disposedValue As Boolean ' 重複する呼び出しを検出するには

  ' IDisposable
  Protected Overridable Sub Dispose(disposing As Boolean)
    If Not disposedValue Then
      If disposing Then
        ' TODO: マネージド状態を破棄します (マネージド オブジェクト)。
        If _PostData IsNot Nothing Then
          _PostData.Clear()
          _PostData = Nothing
        End If
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
