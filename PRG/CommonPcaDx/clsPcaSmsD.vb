Imports T.R.ZCommonClass.clsCommonFnc

''' <summary>
''' 商品マスタ明細クラス
''' </summary>
Public Class clsPcaSmsD
  Implements IDisposable

#Region "メンバ"
#Region "プライベート"
  Private _PostData As New Dictionary(Of String, String)
#End Region
#End Region

#Region "プロパティー"
#Region "パブリック"
  Public Sub AddRowData(prmKey As String _
                      , prmVal As String)
    Call ComSetDictionaryVal(_PostData, prmKey, prmVal)

  End Sub

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
    ''' エンティティバージョン
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
    ''' 商品コード
    ''' </summary>
    Public Property 商品コード As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinCode") Then
        Return _PostData("SyohinCode")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinCode", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品名
  ''' </summary>
  Public Property 商品名 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinMei") Then
        Return _PostData("SyohinMei")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinMei", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品名２
  ''' </summary>
  Public Property 商品名２ As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinMei2") Then
        Return _PostData("SyohinMei2")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinMei2", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品コード2
  ''' </summary>
  Public Property 商品コード2 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinCode2") Then
        Return _PostData("SyohinCode2")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinCode2", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品コード3
  ''' </summary>
  Public Property 商品コード3 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinCode3") Then
        Return _PostData("SyohinCode3")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinCode3", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品区分1
  ''' </summary>
  Public Property 商品区分1 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinKubun1") Then
        Return _PostData("SyohinKubun1")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinKubun1", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品区分2
  ''' </summary>
  Public Property 商品区分2 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinKubun2") Then
        Return _PostData("SyohinKubun2")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinKubun2", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品区分3
  ''' </summary>
  Public Property 商品区分3 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinKubun3") Then
        Return _PostData("SyohinKubun3")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinKubun3", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品区分4
  ''' </summary>
  Public Property 商品区分4 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinKubun4") Then
        Return _PostData("SyohinKubun4")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinKubun4", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品区分5
  ''' </summary>
  Public Property 商品区分5 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinKubun5") Then
        Return _PostData("SyohinKubun5")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinKubun5", value)
    End Set
  End Property

  ''' <summary>
  ''' 単位
  ''' </summary>
  Public Property 単位 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Tani") Then
        Return _PostData("Tani")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Tani", value)
    End Set
  End Property

  ''' <summary>
  ''' 入数
  ''' </summary>
  Public Property 入数 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Irisu") Then
        Return _PostData("Irisu")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Irisu", value)
    End Set
  End Property

  ''' <summary>
  ''' 規格_型番
  ''' </summary>
  Public Property 規格_型番 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("KikakuKataban") Then
        Return _PostData("KikakuKataban")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "KikakuKataban", value)
    End Set
  End Property

  ''' <summary>
  ''' 色
  ''' </summary>
  Public Property 色 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Color") Then
        Return _PostData("Color")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Color", value)
    End Set
  End Property

  ''' <summary>
  ''' サイズ
  ''' </summary>
  Public Property サイズ As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Size") Then
        Return _PostData("Size")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Size", value)
    End Set
  End Property

  ''' <summary>
  ''' 倉庫コード
  ''' </summary>
  Public Property 倉庫コード As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SokoCode") Then
        Return _PostData("SokoCode")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SokoCode", value)
    End Set
  End Property

  ''' <summary>
  ''' 主仕入先
  ''' </summary>
  Public Property 主仕入先 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyuShiresaki") Then
        Return _PostData("SyuShiresaki")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyuShiresaki", value)
    End Set
  End Property

  ''' <summary>
  ''' 標準価格
  ''' </summary>
  Public Property 標準価格 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("HyojunKakaku") Then
        Return _PostData("HyojunKakaku")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "HyojunKakaku", value)
    End Set
  End Property

  ''' <summary>
  ''' 原価
  ''' </summary>
  Public Property 原価 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Genka") Then
        Return _PostData("Genka")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Genka", value)
    End Set
  End Property

  ''' <summary>
  ''' 売価1
  ''' </summary>
  Public Property 売価1 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Baika1") Then
        Return _PostData("Baika1")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Baika1", value)
    End Set
  End Property

  ''' <summary>
  ''' 売価2
  ''' </summary>
  Public Property 売価2 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Baika2") Then
        Return _PostData("Baika2")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Baika2", value)
    End Set
  End Property

  ''' <summary>
  ''' 売価3
  ''' </summary>
  Public Property 売価3 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Baika3") Then
        Return _PostData("Baika3")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Baika3", value)
    End Set
  End Property

  ''' <summary>
  ''' 売価4
  ''' </summary>
  Public Property 売価4 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Baika4") Then
        Return _PostData("Baika4")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Baika4", value)
    End Set
  End Property

  ''' <summary>
  ''' 売価5
  ''' </summary>
  Public Property 売価5 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("Baika5") Then
        Return _PostData("Baika5")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "Baika5", value)
    End Set
  End Property

  ''' <summary>
  ''' 仕入単価
  ''' </summary>
  Public Property 仕入単価 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("ShireTanka") Then
        Return _PostData("ShireTanka")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ShireTanka", value)
    End Set
  End Property

  ''' <summary>
  ''' 在庫単価
  ''' </summary>
  Public Property 在庫単価 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("ZaikoTanka") Then
        Return _PostData("ZaikoTanka")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ZaikoTanka", value)
    End Set
  End Property

  ''' <summary>
  ''' システム区分
  ''' </summary>
  Public Property システム区分 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SystemKubun") Then
        Return _PostData("SystemKubun")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SystemKubun", value)
    End Set
  End Property

  ''' <summary>
  ''' マスター区分
  ''' </summary>
  Public Property マスター区分 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("MasterKubun") Then
        Return _PostData("MasterKubun")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "MasterKubun", value)
    End Set
  End Property

  ''' <summary>
  ''' 在庫管理
  ''' </summary>
  Public Property 在庫管理 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("ZaikoKanri") Then
        Return _PostData("ZaikoKanri")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ZaikoKanri", value)
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
  ''' 税区分
  ''' </summary>
  Public Property 税区分 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("ZeiKubun") Then
        Return _PostData("ZeiKubun")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ZeiKubun", value)
    End Set
  End Property

  ''' <summary>
  ''' 売上税込区分
  ''' </summary>
  Public Property 売上税込区分 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("ZeikomiKubun") Then
        Return _PostData("ZeikomiKubun")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ZeikomiKubun", value)
    End Set
  End Property

  ''' <summary>
  ''' 仕入税込区分
  ''' </summary>
  Public Property 仕入税込区分 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("ShireZeikomiKubun") Then
        Return _PostData("ShireZeikomiKubun")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ShireZeikomiKubun", value)
    End Set
  End Property

  ''' <summary>
  ''' 売上税種別
  ''' </summary>
  Public Property 売上税種別 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("UriageZeiSyubetsu") Then
        Return _PostData("UriageZeiSyubetsu")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "UriageZeiSyubetsu", value)
    End Set
  End Property

  ''' <summary>
  ''' 仕入税種別
  ''' </summary>
  Public Property 仕入税種別 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("ShireZeiSyubetsu") Then
        Return _PostData("ShireZeiSyubetsu")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ShireZeiSyubetsu", value)
    End Set
  End Property

  ''' <summary>
  ''' 税種別切替
  ''' </summary>
  Public Property 税種別切替 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("ZeiSyubetsuKirikae") Then
        Return _PostData("ZeiSyubetsuKirikae")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ZeiSyubetsuKirikae", value)
    End Set
  End Property

  ''' <summary>
  ''' 単価小数桁
  ''' </summary>
  Public Property 単価小数桁 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TankaKeta") Then
        Return _PostData("TankaKeta")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "TankaKeta", value)
    End Set
  End Property

  ''' <summary>
  ''' 数量小数桁
  ''' </summary>
  Public Property 数量小数桁 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SuryoKeta") Then
        Return _PostData("SuryoKeta")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SuryoKeta", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品項目1
  ''' </summary>
  Public Property 商品項目1 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinKomoku1") Then
        Return _PostData("SyohinKomoku1")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinKomoku1", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品項目2
  ''' </summary>
  Public Property 商品項目2 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinKomoku2") Then
        Return _PostData("SyohinKomoku2")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinKomoku2", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品項目3
  ''' </summary>
  Public Property 商品項目3 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinKomoku3") Then
        Return _PostData("SyohinKomoku3")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinKomoku3", value)
    End Set
  End Property

  ''' <summary>
  ''' 売上計算式コード
  ''' </summary>
  Public Property 売上計算式コード As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("UriageKeisan") Then
        Return _PostData("UriageKeisan")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "UriageKeisan", value)
    End Set
  End Property

  ''' <summary>
  ''' 仕入計算式コード
  ''' </summary>
  Public Property 仕入計算式コード As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("ShireKeisan") Then
        Return _PostData("ShireKeisan")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ShireKeisan", value)
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
    ''' 商品予備1
    ''' </summary>
    Public Property 商品予備1 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinYobiShort1") Then
        Return _PostData("SyohinYobiShort1")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinYobiShort1", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品予備2
  ''' </summary>
  Public Property 商品予備2 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinYobiShort2") Then
        Return _PostData("SyohinYobiShort2")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinYobiShort2", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品予備3
  ''' </summary>
  Public Property 商品予備3 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinYobiShort3") Then
        Return _PostData("SyohinYobiShort3")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinYobiShort3", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品予備数値1
  ''' </summary>
  Public Property 商品予備数値1 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinYobiInt1") Then
        Return _PostData("SyohinYobiInt1")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinYobiInt1", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品予備数値2
  ''' </summary>
  Public Property 商品予備数値2 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinYobiInt2") Then
        Return _PostData("SyohinYobiInt2")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinYobiInt2", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品予備数値3
  ''' </summary>
  Public Property 商品予備数値3 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinYobiInt3") Then
        Return _PostData("SyohinYobiInt3")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinYobiInt3", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品予備金額1
  ''' </summary>
  Public Property 商品予備金額1 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinYobiDecimal1") Then
        Return _PostData("SyohinYobiDecimal1")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinYobiDecimal1", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品予備金額2
  ''' </summary>
  Public Property 商品予備金額2 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinYobiDecimal2") Then
        Return _PostData("SyohinYobiDecimal2")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinYobiDecimal2", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品予備金額3
  ''' </summary>
  Public Property 商品予備金額3 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinYobiDecimal3") Then
        Return _PostData("SyohinYobiDecimal3")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinYobiDecimal3", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品予備文字列1
  ''' </summary>
  Public Property 商品予備文字列1 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinYobiString1") Then
        Return _PostData("SyohinYobiString1")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinYobiString1", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品予備文字列2
  ''' </summary>
  Public Property 商品予備文字列2 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinYobiString2") Then
        Return _PostData("SyohinYobiString2")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinYobiString2", value)
    End Set
  End Property

  ''' <summary>
  ''' 商品予備文字列3
  ''' </summary>
  Public Property 商品予備文字列3 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SyohinYobiString3") Then
        Return _PostData("SyohinYobiString3")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SyohinYobiString3", value)
    End Set
  End Property

    ''' <summary>
    ''' 単価登録ユーザー
    ''' </summary>
    Public Property 単価登録ユーザー As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TankaTorokuUser") Then
                Return _PostData("TankaTorokuUser")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TankaTorokuUser", value)
        End Set
    End Property

    ''' <summary>
    ''' 単価登録PG
    ''' </summary>
    Public Property 単価登録PG As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TankaTorokuProgram") Then
                Return _PostData("TankaTorokuProgram")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TankaTorokuProgram", value)
        End Set
    End Property
    ''' <summary>
    ''' 単価登録Date
    ''' </summary>
    Public Property 単価登録Date As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("	TankaTorokubi") Then
                Return _PostData("TankaTorokubi")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TankaTorokubi", value)
        End Set
    End Property

    ''' <summary>
    ''' 単価更新ユーザー
    ''' </summary>
    Public Property 単価更新ユーザー As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TankaKosinUser") Then
                Return _PostData("TankaKosinUser")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TankaKosinUser", value)
        End Set
    End Property

    ''' <summary>
    ''' 単価更新PG
    ''' </summary>
    Public Property 単価更新PG As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TankaKosinProgram") Then
                Return _PostData("TankaKosinProgram")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TankaKosinProgram", value)
        End Set
    End Property

    ''' <summary>
    ''' 単価更新Date
    ''' </summary>
    Public Property 単価更新Date As String
        Get
            If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("	TankaKosinbi") Then
                Return _PostData("TankaKosinbi")
            Else
                Return String.Empty
            End If
        End Get
        Set(value As String)
            Call ComSetDictionaryVal(_PostData, "TankaKosinbi", value)
        End Set
    End Property

    ''' <summary>
    ''' 単価予備1
    ''' </summary>
    Public Property 単価予備1 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TankaYobiShort1") Then
        Return _PostData("TankaYobiShort1")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "TankaYobiShort1", value)
    End Set
  End Property

  ''' <summary>
  ''' 単価予備数値1
  ''' </summary>
  Public Property 単価予備数値1 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TankaYobiInt1") Then
        Return _PostData("TankaYobiInt1")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "TankaYobiInt1", value)
    End Set
  End Property

  ''' <summary>
  ''' 単価予備金額1
  ''' </summary>
  Public Property 単価予備金額1 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("TankaYobiDecimal1") Then
        Return _PostData("TankaYobiDecimal1")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "TankaYobiDecimal1", value)
    End Set
  End Property

  ''' <summary>
  ''' 入数小数桁
  ''' </summary>
  Public Property 入数小数桁 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("IrisuKeta") Then
        Return _PostData("IrisuKeta")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "IrisuKeta", value)
    End Set
  End Property

  ''' <summary>
  ''' 箱数小数桁
  ''' </summary>
  Public Property 箱数小数桁 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("HakosuKeta") Then
        Return _PostData("HakosuKeta")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "HakosuKeta", value)
    End Set
  End Property

  ''' <summary>
  ''' 数量端数
  ''' </summary>
  Public Property 数量端数 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("SuryoHasu") Then
        Return _PostData("SuryoHasu")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "SuryoHasu", value)
    End Set
  End Property

  ''' <summary>
  ''' 有効期間開始
  ''' </summary>
  Public Property 有効期間開始 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("YukoKikanKaishi") Then
        Return _PostData("YukoKikanKaishi")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "YukoKikanKaishi", value)
    End Set
  End Property

  ''' <summary>
  ''' 有効期間終了
  ''' </summary>
  Public Property 有効期間終了 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("YukoKikanSyuryo") Then
        Return _PostData("YukoKikanSyuryo")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "YukoKikanSyuryo", value)
    End Set
  End Property

  ''' <summary>
  ''' 売上単位区分
  ''' </summary>
  Public Property 売上単位区分 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("UriageTaniKubun") Then
        Return _PostData("UriageTaniKubun")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "UriageTaniKubun", value)
    End Set
  End Property

  ''' <summary>
  ''' 仕入単位区分
  ''' </summary>
  Public Property 仕入単位区分 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("ShireTaniKubun") Then
        Return _PostData("ShireTaniKubun")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "ShireTaniKubun", value)
    End Set
  End Property

  ''' <summary>
  ''' ロット管理
  ''' </summary>
  Public Property ロット管理 As String
    Get
      If _PostData IsNot Nothing _
        AndAlso _PostData.ContainsKey("LotKanri") Then
        Return _PostData("LotKanri")
      Else
        Return String.Empty
      End If
    End Get
    Set(value As String)
      Call ComSetDictionaryVal(_PostData, "LotKanri", value)
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

    ''''''

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

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 重複する呼び出しを検出するには

  ' IDisposable
  Protected Overridable Sub Dispose(disposing As Boolean)
    If Not disposedValue Then
      If disposing Then
        ' TODO: マネージド状態を破棄します (マネージド オブジェクト)。
      End If

      ' TODO: アンマネージド リソース (アンマネージド オブジェクト) を解放し、下の Finalize() をオーバーライドします。
      ' TODO: 大きなフィールドを null に設定します。
      _PostData.Clear()
    End If
    disposedValue = True
  End Sub

  ' TODO: 上の Dispose(disposing As Boolean) にアンマネージド リソースを解放するコードが含まれる場合にのみ Finalize() をオーバーライドします。
  Protected Overrides Sub Finalize()
    ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
    Dispose(False)
    MyBase.Finalize()
  End Sub

  ' このコードは、破棄可能なパターンを正しく実装できるように Visual Basic によって追加されました。
  Public Sub Dispose() Implements IDisposable.Dispose
    ' このコードを変更しないでください。クリーンアップ コードを上の Dispose(disposing As Boolean) に記述します。
    Dispose(True)
    ' TODO: 上の Finalize() がオーバーライドされている場合は、次の行のコメントを解除してください。
    GC.SuppressFinalize(Me)
  End Sub
#End Region

End Class
