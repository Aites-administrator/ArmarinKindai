Imports Common.ClsFunction
Imports Common.ClsCommonGlobalData
Imports T.R.ZCommonClass.clsCodeLengthSetting

Public Class CmbMstTanto
    Inherits CmbMstBase


#Region "コンストラクタ"

    Public Sub New()

    MyBase.New(TANTO_CODE_LENGTH)
    lcCallBackCreateSql = AddressOf SqlSelListSrc
        InitCmb()
        ' フォーカス時、表示メッセージ設定
        MyBase.SetMsgLabelText("担当者を選択してください。")

    End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

    ' コンボボックスソース抽出用
    Public Function SqlSelListSrc(prmCode As String) As String
        Dim sql As String = String.Empty

        If (ComChkNumeric(prmCode)) Then

            sql &= " SELECT CODE AS Code "
            sql &= "      , NAME as Name "
            sql &= " FROM MST_TANTO "
            If prmCode <> "" Then
                sql &= "  WHERE CODE = " & prmCode
            End If
            sql &= " ORDER BY CODE "

        End If

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

      .Text = StringToInt(.Text).ToString(TANTO_CODE_LENGTH)

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
