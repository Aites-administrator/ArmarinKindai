
Public Class DgvForm02
  Public Interface IDgvForm02
    Sub InitForm02()
    Function CreateGrid2Src1() As String
    Function CreateGrid2layout1() As List(Of T.R.ZCommonClass.clsDGVColumnSetting)
    Function CreateGrid2EditCol1() As List(Of T.R.ZCommonClass.clsDataGridEditTextBox)
    Function CreateGrid2Src2() As String
    Function CreateGrid2layout2() As List(Of T.R.ZCommonClass.clsDGVColumnSetting)
    Function CreateGrid2EditCol2() As List(Of T.R.ZCommonClass.clsDataGridEditTextBox)
  End Interface
End Class
