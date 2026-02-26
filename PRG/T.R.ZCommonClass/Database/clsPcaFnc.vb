Imports T.R.ZCommonClass.clsCommonFnc


Public Class clsPcaFnc


  ''' <summary>
  ''' PCA商品コード取得
  ''' </summary>
  ''' <param name="prmProductCode"></param>
  ''' <returns></returns>
  Public Shared Function GetProductCode(prmProductCode As String) As String
    Dim ret As String = String.Empty
    Dim TrzDataBase As New clsSqlServer
    Dim tmpDt As New DataTable
    Dim tmpSearchCondition As String = " SCODE = " & prmProductCode

    Try
      TrzDataBase.GetResult(tmpDt, ComAddSqlSearchCondition(SqlSelShenkan(), tmpSearchCondition))
      If tmpDt.Rows.Count > 0 Then
        ret = tmpDt.Rows(0).Item("HENKAN").ToString()
      Else
        ' 変換コードが存在しない場合は元コードを採用する
        ret = prmProductCode
      End If
    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("PCA商品コードの取得に失敗しました。")
    End Try

    Return ret
  End Function


  Private Shared Function SqlSelShenkan() As String
    Dim sql As String = String.Empty

    sql &= " SELECT HENKAN "
    sql &= " FROM SHENKAN "

    Return sql
  End Function


End Class
