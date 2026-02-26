Imports T.R.ZCommonClass.clsCommonFnc

Public Class clsDataGridSearchControl

  ' 抽出条件
  Public Enum typExtraction
    ''' <summary>等しい</summary>
    EX_EQ = 0
    ''' <summary>等しくない</summary>
    EX_NEQ
    ''' <summary>より小さい</summary>
    EX_GT
    ''' <summary>より小さいか等しい</summary>
    EX_GTE
    ''' <summary>より大きい</summary>
    EX_LT
    ''' <summary>より大きいか等しい</summary>
    EX_LTE
    ''' <summary>部分一致検索</summary>
    EX_LIK
    ''' <summary>前方一致検索</summary>
    EX_LIKF
    ''' <summary>後方一致検索</summary>
    EX_LIKB
  End Enum

  ' データタイプ
  Public Enum typColumnKind
    CK_Text = 0
    CK_Number
    CK_Date
  End Enum

#Region "メンバ"

#Region "プライベート"
  Private _SearcyType As typExtraction
  Private _DataType As typColumnKind
  Private _SearchItemName As String
#End Region

#Region "パブリック"
  Public WithEvents mTargetControl As Control
  Public mCallBack As Action

  Delegate Sub CallBackSetTargetControl()
  Public lcCallBackSetTargetControl As CallBackSetTargetControl

  Protected _LastText As String

#End Region

#End Region

#Region "プロパティー"

#Region "パブリック"

  Public Property SearchType As typExtraction
    Get
      Return _SearcyType
    End Get
    Set(value As typExtraction)
      _SearcyType = value
    End Set
  End Property

  Public Property DataType As typColumnKind
    Get
      Return _DataType
    End Get
    Set(value As typColumnKind)
      _DataType = value
    End Set
  End Property

  Public Property SearchItemName As String
    Get
      Return _SearchItemName
    End Get
    Set(value As String)
      _SearchItemName = value
    End Set
  End Property

  Public Property TargetControl() As Control
    Get
      Return mTargetControl
    End Get
    Set(value As Control)
      mTargetControl = value
      lcCallBackSetTargetControl()
    End Set
  End Property
#End Region

#End Region

#Region "コンストラクタ"

  Public Sub New()

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"
  Public Function Value() As String
    Dim ret As String = String.Empty

    If IsComboBox(mTargetControl) Then
      ret = DirectCast(mTargetControl, ComboBox).SelectedValue
    ElseIf IsTextBox(mTargetControl) Then
      ret = DirectCast(mTargetControl, TextBox).Text
    End If

    If ret Is Nothing Then ret = String.Empty
    Return ret
  End Function

  Public Sub ClearLastText()
    Me._LastText = Nothing
  End Sub


#End Region

#End Region

End Class
