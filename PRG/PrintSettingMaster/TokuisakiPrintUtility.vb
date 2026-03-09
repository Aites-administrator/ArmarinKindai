Imports System.Data
Imports System.Text
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc

Public Class TokuisakiPrintUtility

  ''' <summary>
  ''' 得意先の即時発行可否を返す
  ''' レコードが存在する場合は即時発行する
  ''' レコードが存在しない場合は即時発行しない
  ''' </summary>
  Public Shared Function IsInstantPrintEnabled(tokuisakiCd As String, prmSqlServer As clsSqlServer) As Boolean
    If String.IsNullOrWhiteSpace(tokuisakiCd) Then
      Return False
    End If

    Dim dt As New DataTable()
    Dim sql As String = BuildSelectSql(tokuisakiCd.Trim())

    Try
      prmSqlServer.GetResult(dt, sql)
    Catch ex As Exception
      ComWriteErrLog(ex)
      Throw New Exception("即時発行フラグの取得に失敗しました")
    End Try

    Return dt IsNot Nothing AndAlso dt.Rows.Count > 0
  End Function

  Private Shared Function BuildSelectSql(tokuisakiCd As String) As String
    Dim sb As New StringBuilder()

    sb.AppendLine("SELECT")
    sb.AppendLine("    TOKUISAKI_CD")
    sb.AppendLine("FROM")
    sb.AppendLine("    M_TOKUISAKI_PRINT_CTRL")
    sb.AppendLine("WHERE")
    sb.AppendLine("    TOKUISAKI_CD = '" & EscapeSql(tokuisakiCd) & "'")

    Return sb.ToString()
  End Function

  Private Shared Function EscapeSql(value As String) As String
    If value Is Nothing Then
      Return ""
    End If

    Return value.Replace("'", "''")
  End Function

End Class