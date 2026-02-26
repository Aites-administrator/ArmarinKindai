Public Class clsDataGridSearchTextBox
  Inherits clsDataGridSearchControl

#Region "メンバ"

#Region "プライベート"
  Private WithEvents mTxt As TextBox

#End Region

#End Region

#Region "コンストラクタ"

  Public Sub New()
    lcCallBackSetTargetControl = Sub() SetTargetCtrl()
  End Sub

#End Region

#Region "メソッド"

#Region "パブリック"

  Public Sub SetTargetCtrl()
    mTxt = DirectCast(mTargetControl, TextBox)
  End Sub

#End Region

#Region "プライベート"
  Private Sub Validated(sender As Object, e As EventArgs) Handles mTxt.Validated
    Dim tmpValue As String = DirectCast(sender, TextBox).Text

    If mCallBack IsNot Nothing Then
      If _LastText <> tmpValue Then
        ' 最終入力テキスト更新
        _LastText = tmpValue
        mCallBack()
      End If
    End If
  End Sub
#End Region

#End Region

End Class
