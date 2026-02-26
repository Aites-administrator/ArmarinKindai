Public Class ClsSqlServer

  Inherits ClsComDatabase
  Public Sub New()
        DataSource = ClsCommonGlobalData.DB_DATASOURCE
        DefaultDatabase = ClsCommonGlobalData.DB_DEFAULTDATABASE
        UserId = ClsCommonGlobalData.DB_USERID
        Password = ClsCommonGlobalData.DB_PASSWORD
    End Sub

End Class
