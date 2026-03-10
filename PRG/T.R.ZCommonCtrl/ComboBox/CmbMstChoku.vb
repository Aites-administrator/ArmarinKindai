Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc
Imports T.R.ZCommonClass.clsGlobalData
Imports T.R.ZCommonClass.clsCodeLengthSetting

Public Class CmbMstChoku
  Inherits CmbMstBase

  '----------------------------------------------
  '          直送先選択コンボボックス
  '
  '
  '----------------------------------------------
#Region "メンバ"

#Region "プライベート"

#Region "SQL関連"
  Private _StopFlg As Boolean
#End Region
#End Region
#End Region

#Region "コンストラクタ"
  Public Sub New()

    MyBase.New()
    lcCallBackCreateSql = AddressOf SqlSelListSrc
    InitCmb()
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("直送先を選択入力して下さい。")
    MyBase.DropDownWidth = 360
    MyBase.SkipChkCode = True

  End Sub


  Public Sub New(Optional prmStopFlg As Boolean = False)

    MyBase.New("")
    lcCallBackCreateSql = AddressOf SqlSelListSrc
    InitCmb()
    ' フォーカス時、表示メッセージ設定
    MyBase.SetMsgLabelText("直送先を選択入力して下さい。")
    MyBase.DropDownWidth = 360
    MyBase.SkipChkCode = True

    _StopFlg = prmStopFlg

  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  ' コンボボックスソース抽出用

  Public Function SqlSelListSrc(prmCode As String) As String
    Dim sql As String = String.Empty

    sql &= " SELECT CODE As Code"
    sql &= "    ,   NAME As Name"
    sql &= " FROM MST_CHOKUSO "
    sql &= " ORDER BY CODE "

    Return sql
  End Function

#End Region

#Region "イベントプロシージャー"
  Private Sub TxtDateBase_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Me.Validating
    Dim tmpDateText As String = String.Empty
    With Me

      ' 得意先コードが空白の場合
      If String.IsNullOrWhiteSpace(.Text) Then
        Return
      End If

      .Text = StringToInt(.Text).ToString.PadLeft(TYOKUSO_CODE_LENGTH, "0"c)

    End With

  End Sub

  ''' <summary>
  ''' 数値とバックスペースのみ入力可
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub TTxtDateBase_KeyPress(sender As Object, e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress

    ' 数値とバックスペースのみ入力可
    If (e.KeyChar < "0"c OrElse "9"c < e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
      '押されたキーが 0～9でない場合は、イベントをキャンセルする
      e.Handled = True
    End If

  End Sub

#End Region

#End Region

End Class
