Imports T.R.ZCommonClass.clsCommonFnc
Imports Common

Public Class clsSqlServer
  Inherits clsComDatabase

    Public Sub New()

        Me.DataSource = ClsCommonGlobalData.DB_DATASOURCE
        Me.DefaultDatabase = ClsCommonGlobalData.DB_DEFAULTDATABASE
        Me.UserId = ClsCommonGlobalData.DB_USERID
        Me.Password = ClsCommonGlobalData.DB_PASSWORD
        Me.Provider = typProvider.sqlServer
    End Sub

    ''' <summary>
    ''' PCA商品コード取得
    ''' </summary>
    ''' <param name="prmProductCode"></param>
    ''' <returns></returns>
    Public Function GetProductCode(prmProductCode As String) As String
    Dim ret As String = String.Empty
    Dim tmpDt As New DataTable
    Dim tmpSearchCondition As String = " SCODE = " & prmProductCode

    Try
      Me.GetResult(tmpDt, ComAddSqlSearchCondition(SqlSelShenkan(), tmpSearchCondition))
      If tmpDt.Rows.Count > 0 Then
        ret = tmpDt.Rows(0).Item("HENKAN").ToString()
      Else
        '        Throw New Exception("PCA商品コードの変換に失敗しました。")
        ret = prmProductCode
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("PCA商品コードの取得に失敗しました。")
    End Try

    Return ret
  End Function

  Private Function SqlSelShenkan() As String
    Dim sql As String = String.Empty

    sql &= " SELECT HENKAN "
    sql &= " FROM SHENKAN "

    Return sql
  End Function

  ''' <summary>
  ''' PCA得意先コード取得
  ''' </summary>
  ''' <param name="prmCustomerCode"></param>
  ''' <returns></returns>
  Public Function GetCustomerCode(prmCustomerCode As String) As String
    Dim ret As String = String.Empty
    Dim TrzDataBase As New clsSqlServer
    Dim tmpDt As New DataTable
    Dim tmpSearchCondition As String = " CAST(TOKUISAKI.TKCODE as numeric) = " & prmCustomerCode

    Try
      TrzDataBase.GetResult(tmpDt, ComAddSqlSearchCondition(SqlSelThenkan(), tmpSearchCondition))
      If tmpDt.Rows.Count > 0 Then
        ret = tmpDt.Rows(0).Item("TKCODE").ToString()
      Else
        Throw New Exception("PCA得意先コードの変換に失敗しました。")
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("PCA得意先コードの取得に失敗しました。")
    End Try

    Return ret
  End Function

  ''' <summary>
  ''' PCA得意先コード取得
  ''' </summary>
  ''' <param name="prmSupplierCode"></param>
  ''' <returns></returns>
  Public Function GetSupplierCode(prmSupplierCode As String) As String
    Dim ret As String = String.Empty
    Dim TrzDataBase As New clsSqlServer
    Dim tmpDt As New DataTable
    Dim tmpSearchCondition As String = " CAST(CUTSR.SRCODE As numeric) = " & prmSupplierCode

    Try
      TrzDataBase.GetResult(tmpDt, ComAddSqlSearchCondition(SqlSelSiHenkan(), tmpSearchCondition))
      If tmpDt.Rows.Count > 0 Then
        ret = tmpDt.Rows(0).Item("SRCODE").ToString()
      Else
        Throw New Exception("PCA得意先コードの変換に失敗しました。")
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("PCA得意先コードの取得に失敗しました。")
    End Try

    Return ret
  End Function

  Private Function SqlSelThenkan() As String
    Dim sql As String = String.Empty

    sql &= "  SELECT THENKAN.HENKAN AS TKCODE  "
    sql &= "       , TOKUISAKI.TNAME  "
    sql &= "       , TOKUISAKI.KUBUN "
    sql &= "  FROM TOKUISAKI "
    sql &= "       INNER JOIN THENKAN ON CAST(TOKUISAKI.TKCODE as numeric) = THENKAN.TKCODE "

    Return sql

  End Function

  Private Function SqlSelSiHenkan() As String
    Dim sql As String = String.Empty

    sql &= "  SELECT SIHENKA.HENKAN AS SRCODE  "
    sql &= "       , CUTSR.LSRNAME  "
    sql &= "       , CUTSR.KUBUN "
    sql &= "  FROM CUTSR "
    sql &= "       INNER JOIN SIHENKA ON CAST(CUTSR.SRCODE as numeric) = SIHENKA.SRCODE "

    Return sql

  End Function

End Class
