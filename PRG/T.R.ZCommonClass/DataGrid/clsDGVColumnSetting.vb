Public Class clsDGVColumnSetting

  ''' <summary>
  ''' DataGridViewカラムクラス
  ''' </summary>

#Region "プライベートメンバー"
  Private _DataSrc As String
  Private _TitleCaption As String
  Private _FontName As typFontName
  Private _FontSize As Single
  Private _TextAlignment As typAlignment
  Private _ColumnWidth As Integer
  Private _Format As String
  Private _TitleFontSize As Single
#End Region

#Region "列挙体"

  ''' <summary>
  ''' 文字フォント
  ''' </summary>
  Public Enum typFontName
    MSゴシック = 0
    MS明朝
    MeiryoUI
    メイリオ
    UDデジタル教科書体NKR
  End Enum


  ''' <summary>
  ''' セル内文字配置
  ''' </summary>
  Public Enum typAlignment
    ''' <summary>
    ''' 指定無し
    ''' </summary>
    NotSet = 0
    ''' <summary>
    ''' 上段左寄せ
    ''' </summary>
    TopLeft
    ''' <summary>
    ''' 上段中央寄せ
    ''' </summary>
    TopCenter
    ''' <summary>
    ''' 上段右寄せ
    ''' </summary>
    TopRight
    ''' <summary>
    ''' 中段左寄せ
    ''' </summary>
    MiddleLeft
    ''' <summary>
    ''' 中段中央寄せ
    ''' </summary>
    MiddleCenter
    ''' <summary>
    ''' 中段右寄せ
    ''' </summary>
    MiddleRight
    ''' <summary>
    ''' 下段左寄せ
    ''' </summary>
    BottomLeft
    ''' <summary>
    ''' 下段中央寄せ
    ''' </summary>
    BottomCenter
    ''' <summary>
    ''' 下段右寄せ
    ''' </summary>
    BottomRight
  End Enum

#End Region

#Region "パブリックプロパティー"
  Public Property ColumnWidth As Integer
    Get
      Return _ColumnWidth
    End Get
    Set(value As Integer)
      _ColumnWidth = value
    End Set
  End Property

  Public Property ColumnFormat As Integer
    Get
      Return _Format
    End Get
    Set(value As Integer)
      _Format = value
    End Set
  End Property

  Public WriteOnly Property TextAlignment As typAlignment
    Set(value As typAlignment)
      _TextAlignment = value
    End Set
  End Property


  Public WriteOnly Property FontName As typFontName
    Set(value As typFontName)
      _FontName = value
    End Set
  End Property

  Public Property FontSize As Single
    Get
      Return _FontSize
    End Get
    Set(value As Single)
      _FontSize = value
    End Set
  End Property

  Public Property DataSrc() As String
    Get
      Return _DataSrc
    End Get
    Set(value As String)
      _DataSrc = value
    End Set
  End Property

  ''' <summary>
  ''' 一覧表示タイトル
  ''' </summary>
  ''' <returns></returns>
  Public Property TitleCaption As String
    Get
      Return _TitleCaption
    End Get
    Set(value As String)
      _TitleCaption = value
    End Set
  End Property

  Public Property TitleFontSize As Single
    Get
      Return _TitleFontSize
    End Get
    Set(value As Single)
      _TitleFontSize = value
    End Set
  End Property
#End Region

#Region "コンストラクタ"
  Public Sub New()
    Me.New(String.Empty, String.Empty)
  End Sub

  ''' <summary>
  ''' グリッドカラム作成
  ''' </summary>
  ''' <param name="argTitelCaption">タイトル</param>
  ''' <param name="argDataSrc">データソース</param>
  ''' <param name="argFontName">フォント名</param>
  ''' <param name="argFontSize">フォントサイズ</param>
  ''' <param name="argTextAlignment">表示位置</param>
  ''' <param name="artTitleFontSize">タイトルフォントサイズ</param>
  ''' <param name="argColumnWidth">幅</param>
  ''' <param name="argFormat">書式</param>
  Public Sub New(argTitelCaption As String _
                 , argDataSrc As String _
                 , Optional argFontName As typFontName = typFontName.MSゴシック _
                 , Optional argFontSize As Single = 12 _
                 , Optional argTextAlignment As typAlignment = typAlignment.MiddleLeft _
                 , Optional artTitleFontSize As Single = 10 _
                 , Optional argColumnWidth As Integer = 120 _
                 , Optional argFormat As String = "")
    _DataSrc = argDataSrc
    _TitleCaption = argTitelCaption
    _FontName = argFontName
    _FontSize = argFontSize
    _TextAlignment = argTextAlignment
    _ColumnWidth = argColumnWidth
    _Format = argFormat
    _TitleFontSize = artTitleFontSize
  End Sub
#End Region

#Region "パブリックメソッド"

  ' フォント名取得
  Public Function GetFontName() As String
    Dim FontName As String = ""

    Select Case _FontName
      Case typFontName.MSゴシック
        FontName = "ＭＳ ゴシック"
      Case typFontName.MS明朝
        FontName = "ＭＳ 明朝"
      Case typFontName.UDデジタル教科書体NKR
        FontName = "UD デジタル 教科書体 NK-R"
      Case typFontName.メイリオ
        FontName = "メイリオ"
      Case typFontName.MeiryoUI
        FontName = "Meiryo UI"
    End Select

    Return FontName
  End Function

  ' 文字配置定数取得
  Public Function GetTextAlignment() As DataGridViewContentAlignment
    Dim TextAlignment As DataGridViewContentAlignment = DataGridViewContentAlignment.MiddleCenter

    Select Case _TextAlignment
      Case typAlignment.BottomCenter
        TextAlignment = DataGridViewContentAlignment.BottomCenter
      Case typAlignment.BottomLeft
        TextAlignment = DataGridViewContentAlignment.BottomLeft
      Case typAlignment.BottomRight
        TextAlignment = DataGridViewContentAlignment.BottomRight
      Case typAlignment.MiddleCenter
        TextAlignment = DataGridViewContentAlignment.MiddleCenter
      Case typAlignment.MiddleLeft
        TextAlignment = DataGridViewContentAlignment.MiddleLeft
      Case typAlignment.MiddleRight
        TextAlignment = DataGridViewContentAlignment.MiddleRight
      Case typAlignment.NotSet
        TextAlignment = DataGridViewContentAlignment.NotSet
      Case typAlignment.TopCenter
        TextAlignment = DataGridViewContentAlignment.TopCenter
      Case typAlignment.TopLeft
        TextAlignment = DataGridViewContentAlignment.TopLeft
      Case typAlignment.TopRight
        TextAlignment = DataGridViewContentAlignment.TopLeft
    End Select

    Return TextAlignment
  End Function

  ' フォーマット取得
  Public Function GetFormat() As String

    Return _Format
  End Function

#End Region

End Class
