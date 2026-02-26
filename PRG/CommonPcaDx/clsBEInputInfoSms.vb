''' <summary>
''' 伝票入力用商品情報構造体子クラス
''' </summary>
Public Class clsBEInputInfoSms
  Inherits clsDetailBase

#Region "プロパティー"
#Region "パブリック"
  ''' <summary>
  ''' エンティティバージョン
  ''' </summary>
  Public ReadOnly Property エンティティバージョン As String
    Get
      Return GetRowData("BEVersion")
    End Get
  End Property

  ''' <summary>
  ''' 取得状態
  ''' </summary>
  Public ReadOnly Property 取得状態 As String
    Get
      Return GetRowData("Status")
    End Get
  End Property

  ''' <summary>
  ''' 商品コード
  ''' </summary>
  Public ReadOnly Property 商品コード As String
    Get
      Return GetRowData("SyohinCode")
    End Get
  End Property

  ''' <summary>
  ''' システム区分
  ''' </summary>
  Public ReadOnly Property システム区分 As String
    Get
      Return GetRowData("SystemKubun")
    End Get
  End Property

  ''' <summary>
  ''' マスター区分
  ''' </summary>
  Public ReadOnly Property マスター区分 As String
    Get
      Return GetRowData("MasterKubun")
    End Get
  End Property

  ''' <summary>
  ''' 在庫管理
  ''' </summary>
  Public ReadOnly Property 在庫管理 As String
    Get
      Return GetRowData("ZaikoKanri")
    End Get
  End Property

  ''' <summary>
  ''' 税区分
  ''' </summary>
  Public ReadOnly Property 税区分 As String
    Get
      Return GetRowData("ZeiKubun")
    End Get
  End Property

  ''' <summary>
  ''' 税込区分
  ''' </summary>
  Public ReadOnly Property 税込区分 As String
    Get
      Return GetRowData("ZeikomiKubun")
    End Get
  End Property

  ''' <summary>
  ''' 単価小数桁
  ''' </summary>
  Public ReadOnly Property 単価小数桁 As String
    Get
      Return GetRowData("TankaKeta")
    End Get
  End Property

  ''' <summary>
  ''' 数量小数桁
  ''' </summary>
  Public ReadOnly Property 数量小数桁 As String
    Get
      Return GetRowData("SuryoKeta")
    End Get
  End Property

  ''' <summary>
  ''' 入数小数桁
  ''' </summary>
  Public ReadOnly Property 入数小数桁 As String
    Get
      Return GetRowData("IrisuKeta")
    End Get
  End Property

  ''' <summary>
  ''' 箱数小数桁
  ''' </summary>
  Public ReadOnly Property 箱数小数桁 As String
    Get
      Return GetRowData("HakosuKeta")
    End Get
  End Property

  ''' <summary>
  ''' 商品名
  ''' </summary>
  Public ReadOnly Property 商品名 As String
    Get
      Return GetRowData("SyohinMei")
    End Get
  End Property

  ''' <summary>
  ''' 商品名２
  ''' </summary>
  Public ReadOnly Property SyohinMei2 As String
    Get
      Return GetRowData("SyohinMei2")
    End Get
  End Property

  ''' <summary>
  ''' 規格・型番
  ''' </summary>
  Public ReadOnly Property 規格型番 As String
    Get
      Return GetRowData("KikakuKataban")
    End Get
  End Property

  ''' <summary>
  ''' 色
  ''' </summary>
  Public ReadOnly Property 色 As String
    Get
      Return GetRowData("Color")
    End Get
  End Property

  ''' <summary>
  ''' サイズ
  ''' </summary>
  Public ReadOnly Property サイズ As String
    Get
      Return GetRowData("Size")
    End Get
  End Property

  ''' <summary>
  ''' 倉庫コード
  ''' </summary>
  Public ReadOnly Property 倉庫コード As String
    Get
      Return GetRowData("SokoCode")
    End Get
  End Property

  ''' <summary>
  ''' 入数
  ''' </summary>
  Public ReadOnly Property 入数 As String
    Get
      Return GetRowData("Irisu")
    End Get
  End Property


  ''' <summary>
  ''' 数量
  ''' </summary>
  Public ReadOnly Property 数量 As String
    Get
      Return GetRowData("Suryo")
    End Get
  End Property

  ''' <summary>
  ''' 単位
  ''' </summary>
  Public ReadOnly Property 単位 As String
    Get
      Return GetRowData("Tani")
    End Get
  End Property

  ''' <summary>
  ''' 単価
  ''' </summary>
  Public ReadOnly Property 単価 As String
    Get
      Return GetRowData("Tanka")
    End Get
  End Property

  ''' <summary>
  ''' 原単価
  ''' </summary>
  Public ReadOnly Property 原単価 As String
    Get
      Return GetRowData("GenTanka")
    End Get
  End Property

  ''' <summary>
  ''' 売単価
  ''' </summary>
  Public ReadOnly Property 売単価 As String
    Get
      Return GetRowData("BaiTanka")
    End Get
  End Property

  ''' <summary>
  ''' 標準価格（標準仕入単価）
  ''' </summary>
  Public ReadOnly Property 標準価格 As String
    Get
      Return GetRowData("HyojunKakaku")
    End Get
  End Property

  ''' <summary>
  ''' 計算式コード
  ''' </summary>
  Public ReadOnly Property 計算式コード As String
    Get
      Return GetRowData("Keisanshiki")
    End Get
  End Property

  ''' <summary>
  ''' 商品項目1
  ''' </summary>
  Public ReadOnly Property 商品項目1 As String
    Get
      Return GetRowData("SyohinKomoku1")
    End Get
  End Property

  ''' <summary>
  ''' 商品項目2
  ''' </summary>
  Public ReadOnly Property 商品項目2 As String
    Get
      Return GetRowData("SyohinKomoku2")
    End Get
  End Property

  ''' <summary>
  ''' 商品項目3
  ''' </summary>
  Public ReadOnly Property 商品項目3 As String
    Get
      Return GetRowData("SyohinKomoku3")
    End Get
  End Property

  ''' <summary>
  ''' 使用区分
  ''' </summary>
  Public ReadOnly Property 使用区分 As String
    Get
      Return GetRowData("ShiyoKubun")
    End Get
  End Property

  ''' <summary>
  ''' コメント
  ''' </summary>
  Public ReadOnly Property コメント As String
    Get
      Return GetRowData("Comment")
    End Get
  End Property

  ''' <summary>
  ''' 税率
  ''' </summary>
  Public ReadOnly Property 税率 As String
    Get
      Return GetRowData("ZeiRitsu")
    End Get
  End Property

  ''' <summary>
  ''' 数量端数
  ''' </summary>
  Public ReadOnly Property 数量端数 As String
    Get
      Return GetRowData("SuryoHasu")
    End Get
  End Property


  ''' <summary>
  ''' 原価端数
  ''' </summary>    
  Public ReadOnly Property 原価端数 As String
    Get
      Return GetRowData("GenkaHasu")
    End Get
  End Property

  ''' <summary>
  ''' 原価消費税端数
  ''' </summary>    
  Public ReadOnly Property 原価消費税端数 As String
    Get
      Return GetRowData("GenkaSyohizeiHasu")
    End Get
  End Property

  ''' <summary>
  ''' 有効期間開始
  ''' </summary>    
  Public ReadOnly Property 有効期間開始 As String
    Get
      Return GetRowData("YukoKikanKaishi")
    End Get
  End Property

  ''' <summary>
  ''' 有効期間終了
  ''' </summary>    
  Public ReadOnly Property 有効期間終了 As String
    Get
      Return GetRowData("YukoKikanSyuryo")
    End Get
  End Property

  ''' <summary>
  ''' 単位区分
  ''' </summary>    
  Public ReadOnly Property 単位区分 As String
    Get
      Return GetRowData("TaniKubun")
    End Get
  End Property

  ''' <summary>
  ''' ロット管理
  ''' </summary>    
  Public ReadOnly Property ロット管理 As String
    Get
      Return GetRowData("LotKanri")
    End Get
  End Property

  ''' <summary>
  ''' 税種別
  ''' </summary>    
  Public ReadOnly Property 税種別 As String
    Get
      Return GetRowData("ZeiSyubetsu")
    End Get
  End Property

  ''' <summary>
  ''' 原価税込区分
  ''' </summary>    
  Public ReadOnly Property 原価税込区分 As String
    Get
      Return GetRowData("GenkaZeikomiKubun")
    End Get
  End Property

  ''' <summary>
  ''' 原価税率
  ''' </summary>    
  Public ReadOnly Property 原価税率 As String
    Get
      Return GetRowData("GenkaZeiRitsu")
    End Get
  End Property

  ''' <summary>
  ''' 原価税種別
  ''' </summary>    
  Public ReadOnly Property 原価税種別 As String
    Get
      Return GetRowData("GenkaZeiSyubetsu")
    End Get
  End Property

#End Region
#End Region

End Class
