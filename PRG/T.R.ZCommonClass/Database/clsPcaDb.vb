Imports T.R.ZCommonClass

Public Class clsPcaDb
  Inherits clsComDatabase

  Public Sub New()
    Me.DataSource = "localhost\PCADB"
    'Me.DataSource = "nikserver21\PCADB"
    Me.DefaultDatabase = "P20V01C001KON0002"
    Me.UserId = "sa"
    Me.Password = "Aites495344!"
    Me.Provider = typProvider.sqlServer
  End Sub

End Class
