Imports T.R.ZCommonClass.clsCommonFnc

''' <summary>
''' 商品検索条件構造体クラス
''' </summary>
Public Class clsPcaInputInfoSmsCondition
  Inherits clsDetailBase

#Region "コンストラクタ"
  Public Sub New()
    Me.New("200")
  End Sub

  Public Sub New(prmVersion As String)
    AddRowData("BEVersion", prmVersion)
  End Sub
#End Region

#Region "プロパティー"
#Region "パブリック"

  ''' <summary>
  ''' 設定データをXML形式で出力
  ''' </summary>
  ''' <returns></returns>
  Public Overloads ReadOnly Property XML As String
    Get
      Dim tmpXml = "<BEInputInfoSmsCondition>"
      tmpXml &= MyBase.XML
      tmpXml &= "</BEInputInfoSmsCondition>"
      Return tmpXml
    End Get
  End Property

  ''' <summary>
  ''' 商品コード
  ''' </summary>
  Public WriteOnly Property 商品コード As String
    Set(value As String)
      Call AddRowData("SyohinCode", value)
    End Set
  End Property

  ''' <summary>
  ''' システム区分
  ''' </summary>
  Public WriteOnly Property システム区分 As String
    Set(value As String)
      Call AddRowData("SystemKubun", value)
    End Set
  End Property

  ''' <summary>
  ''' 得意先コード
  ''' </summary>
  ''' <remarks>仕入先コードと同一</remarks>
  Public WriteOnly Property 得意先コード As String
    Set(value As String)
      Call AddRowData("TorihikisakiCode", value)
    End Set
  End Property

  ''' <summary>
  ''' 仕入先コード
  ''' </summary>
  ''' <remarks>得意先コードと同一</remarks>
  Public WriteOnly Property 仕入先コード As String
    Set(value As String)
      Call AddRowData("TorihikisakiCode", value)
    End Set
  End Property

  ''' <summary>
  ''' 伝票日付
  ''' </summary>
  Public WriteOnly Property 伝票日付 As String
    Set(value As String)
      Call AddRowData("DenpyoHizuke", value)
    End Set
  End Property

  ''' <summary>
  ''' 数量
  ''' </summary>
  Public WriteOnly Property 数量 As String
    Set(value As String)
      Call AddRowData("Suryo", value)
    End Set
  End Property


  ''' <summary>
  ''' 商品名の適用方法
  ''' </summary>
  Public WriteOnly Property 商品名の適用方法 As String
    Set(value As String)
      Call AddRowData("SyohinMeiType", value)
    End Set
  End Property

  ''' <summary>
  ''' 単価の適用方法
  ''' </summary>
  Public WriteOnly Property 単価の適用方法 As String
    Set(value As String)
      Call AddRowData("TankaType", value)
    End Set
  End Property

  ''' <summary>
  ''' 税換算
  ''' </summary>
  Public WriteOnly Property 税換算 As String
    Set(value As String)
      Call AddRowData("ZeiKansan", value)
    End Set
  End Property

  ''' <summary>
  ''' 消費税端数
  ''' </summary>
  Public WriteOnly Property 消費税端数 As String
    Set(value As String)
      Call AddRowData("SyohizeiHasu", value)
    End Set
  End Property

  ''' <summary>
  ''' 原価の適用方法
  ''' </summary>
  Public WriteOnly Property 原価の適用方法 As String
    Set(value As String)
      Call AddRowData("GenkaType", value)
    End Set
  End Property

  ''' <summary>
  ''' 仕入単価の適用方法
  ''' </summary>
  Public WriteOnly Property 仕入単価の適用方法 As String
    Set(value As String)
      Call AddRowData("ShireTankaType", value)
    End Set
  End Property

  ''' <summary>
  ''' 原価端数の適用方法
  ''' </summary>
  Public WriteOnly Property 原価端数の適用方法 As String
    Set(value As String)
      Call AddRowData("GenkaHasuType", value)
    End Set
  End Property

  ''' <summary>
  ''' 金額端数
  ''' </summary>
  Public WriteOnly Property 金額端数 As String
    Set(value As String)
      Call AddRowData("KingakuHasu", value)
    End Set
  End Property


  ''' <summary>
  ''' 部門倉庫
  ''' </summary>
  Public WriteOnly Property 部門倉庫 As String
    Set(value As String)
      Call AddRowData("BumonSoko", value)
    End Set
  End Property

  ''' <summary>
  ''' 売単価の適用方法
  ''' </summary>
  Public WriteOnly Property 売単価の適用方法 As String
    Set(value As String)
      Call AddRowData("BaiTankaType", value)
    End Set
  End Property

  ''' <summary>
  ''' 適用売価No
  ''' </summary>
  Public WriteOnly Property 適用売価No As String
    Set(value As String)
      Call AddRowData("TekiyoBaikaNo", value)
    End Set
  End Property

  ''' <summary>
  ''' 掛率
  ''' </summary>
  Public WriteOnly Property 掛率 As String
    Set(value As String)
      Call AddRowData("Kakeritsu", value)
    End Set
  End Property

  ''' <summary>
  ''' 売単価換算
  ''' </summary>
  Public WriteOnly Property 売単価換算 As String
    Set(value As String)
      Call AddRowData("BaiTankaKansan", value)
    End Set
  End Property

  ''' <summary>
  ''' 単位区分
  ''' </summary>
  Public WriteOnly Property 単位区分 As String
    Set(value As String)
      Call AddRowData("TaniKubun", value)
    End Set
  End Property

  ''' <summary>
  ''' 最終更新日時（From）
  ''' </summary>
  Public WriteOnly Property 登録変更期間_開始 As String
    Set(value As String)
      Call AddRowData("KosinbiFrom", value)
    End Set
  End Property

  ''' <summary>
  ''' 最終更新日時（To）
  ''' </summary>
  Public WriteOnly Property 登録変更期間_終了 As String
    Set(value As String)
      Call AddRowData("KosinbiTo", value)
    End Set
  End Property

#End Region

#End Region

End Class
