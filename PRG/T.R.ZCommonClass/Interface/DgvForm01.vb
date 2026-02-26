
Public Class DgvForm01
  Public Interface IDgvForm01
    Sub InitForm()
    Function CreateGridSrc() As String
    Function CreateGridlayout() As List(Of T.R.ZCommonClass.clsDGVColumnSetting)
    Function CreateGridEditCol() As List(Of T.R.ZCommonClass.clsDataGridEditTextBox)
  End Interface
End Class
