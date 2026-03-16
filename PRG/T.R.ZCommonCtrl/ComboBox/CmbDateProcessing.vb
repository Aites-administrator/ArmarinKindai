Imports T.R.ZCommonClass.clsCommonFnc

Public Class CmbDateProcessing
  Inherits CmbDateBase

  Private Const CODE_FORMAT As String = "yyyy/MM/dd"

#Region "コンストラクタ"

  ''' <summary>
  ''' コンストラクタ
  ''' </summary>
  Public Sub New()
    ' 選択項目抽出SQL文設定
    lcCallBackCreateSql = AddressOf SqlSelListSrc
    ' 初期化
    InitCmb()
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("納品日を選択してください。")

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"
  ' コンボボックスソース抽出用
  Public Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty


    sql &= " SELECT Format(NohinDay,'" & CODE_FORMAT & "')  AS Code  "
    sql &= " FROM ("
    sql &= " SELECT CAST(NohinDay AS datetime) AS NohinDay "
    'sql &= " SELECT LEFT(NohinDay,4) + '/'  + SUBSTRING(NohinDay,5,2) + '/' + RIGHT(NohinDay,2) AS NohinDay "
    sql &= " FROM TRN_JISSEKI "
    sql &= " WHERE NohinDay >= dateadd(month, - 7, getdate())  "
    sql &= " GROUP BY NohinDay "
    sql &= " ) SRC "
    sql &= " ORDER BY NohinDay DESC"


    Return sql
  End Function
#End Region

#End Region

End Class
