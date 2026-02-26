Public Class clsDataGridEditTextBox

  ''' <summary>
  ''' 項目の型
  ''' </summary>
  Enum typValueType
    ''' <summary>
    ''' 文字列
    ''' </summary>
    VT_TEXT = 0

    ''' <summary>
    ''' 数値
    ''' </summary>
    VT_NUMBER

    ''' <summary>
    ''' 数値（符号付き）
    ''' </summary>
    VT_SIGNED_NUMBER

    ''' <summary>
    ''' 日付(yyyy/MM/dd)
    ''' </summary>
    VT_DATE
  End Enum

#Region "メンバ"

#Region "プライベート"
  Private _TitleName As String
  Private _ValueType As typValueType
  Private _MaxChar As Long
  Private _IsReload As Boolean
#End Region

#Region "パブリック"
  Delegate Function CallBackUpDate() As Boolean
  Public lcCallBackUpDate As CallBackUpDate
#End Region

#End Region

#Region "プロパティ"

#Region "パブリック"
  Public Property TitleName() As String
    Get
      Return _TitleName
    End Get
    Set(value As String)
      _TitleName = value
    End Set
  End Property

  Public Property ValueType() As typValueType
    Get
      Return _ValueType
    End Get
    Set(value As typValueType)
      _ValueType = value
    End Set
  End Property

  Public Property MaxChar() As Long
    Get
      Return _MaxChar
    End Get
    Set(value As Long)
      _MaxChar = value
    End Set
  End Property

  Public Property IsReload As Boolean
    Get
      Return _IsReload
    End Get
    Set(value As Boolean)
      _IsReload = value
    End Set
  End Property

#End Region

#End Region

#Region "コンストラクタ"

  ''' <summary>
  ''' 編集可能セル設定
  ''' </summary>
  ''' <param name="prmTitleName">編集可能とするセルのタイトル</param>
  ''' <param name="prmValueType">値の型</param>
  ''' <param name="prmMaxChar">入力可能最大文字数</param>
  ''' <param name="prmUpdateFnc">変更後に実行する関数</param>
  ''' <param name="prmIsReload">更新後に一覧再読み込みを行う/行わないフラグ
  '''                          （True - 更新後画面再読み込み /False - 更新後処理無し）
  ''' </param>
  Public Sub New(prmTitleName As String _
                 , Optional prmValueType As typValueType = typValueType.VT_TEXT _
                 , Optional prmMaxChar As Long = 0 _
                 , Optional prmUpdateFnc As CallBackUpDate = Nothing _
                 , Optional prmIsReload As Boolean = True)
    _TitleName = prmTitleName
    lcCallBackUpDate = prmUpdateFnc
    _ValueType = prmValueType
    _MaxChar = prmMaxChar
    _IsReload = prmIsReload
  End Sub
#End Region

End Class
