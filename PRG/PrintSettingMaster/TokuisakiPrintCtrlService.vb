Imports System.Data
Imports System.Text
Imports T.R.ZCommonClass
Imports T.R.ZCommonClass.clsCommonFnc

Public Class TokuisakiPrintCtrlService

  ''' <summary>
  ''' 一覧取得
  ''' </summary>
  Public Function GetList(tokuisakiCd As String, tokuisakiNm As String, prmSqlServer As clsSqlServer) As DataTable
    Dim dt As New DataTable()
    Dim sql As String = BuildSelectSql(tokuisakiCd, tokuisakiNm)

    Try
      prmSqlServer.GetResult(dt, sql)
    Catch ex As Exception
      ComWriteErrLog(ex)
      Throw New Exception("一覧の取得に失敗しました。")
    End Try

    If Not dt.Columns.Contains("ORIGINAL_FLG") Then
      dt.Columns.Add("ORIGINAL_FLG", GetType(Boolean))
    End If

    For Each row As DataRow In dt.Rows
      row("ORIGINAL_FLG") = ToBooleanSafe(row("INSTANT_PRINT_FLG"))
    Next

    Return dt
  End Function

  ''' <summary>
  ''' 保存
  ''' OFFはDELETE方式
  ''' </summary>
  Public Function Save(dt As DataTable, prmSqlServer As clsSqlServer) As Integer
    Dim updateCount As Integer = 0

    For Each row As DataRow In dt.Rows
      Dim currentFlg As Boolean = ToBooleanSafe(row("INSTANT_PRINT_FLG"))
      Dim originalFlg As Boolean = ToBooleanSafe(row("ORIGINAL_FLG"))
      Dim tokuisakiCd As String = row("TokuiCD").ToString().Trim()

      If currentFlg = originalFlg Then
        Continue For
      End If

      Dim sql As String

      If currentFlg Then
        sql = BuildInsertSql(tokuisakiCd)
      Else
        sql = BuildDeleteSql(tokuisakiCd)
      End If

      Try
        prmSqlServer.Execute(sql)
      Catch ex As Exception
        ComWriteErrLog(ex)
        Throw New Exception("データ保存に失敗しました")
      End Try

      row("ORIGINAL_FLG") = currentFlg
      updateCount += 1
    Next

    Return updateCount
  End Function

  ''' <summary>
  ''' 一覧取得SQL
  ''' レコード存在あり = ON
  ''' レコードなし = OFF
  ''' </summary>
  Private Function BuildSelectSql(tokuisakiCd As String, tokuisakiNm As String) As String
    Dim sb As New StringBuilder()

    sb.AppendLine("SELECT")
    sb.AppendLine("    T.TokuiCD,")
    sb.AppendLine("    T.TokuiNM1,")
    sb.AppendLine("    CAST(CASE WHEN P.TOKUISAKI_CD IS NULL THEN 0 ELSE 1 END AS BIT) AS INSTANT_PRINT_FLG")
    sb.AppendLine("FROM")
    sb.AppendLine("    MST_TOKUISAKI T")
    sb.AppendLine("    LEFT JOIN M_TOKUISAKI_PRINT_CTRL P")
    sb.AppendLine("        ON T.TokuiCD = P.TOKUISAKI_CD")
    sb.AppendLine("WHERE 1 = 1")

    If tokuisakiCd <> "" Then
      sb.AppendLine("  AND T.TokuiCD = '" & EscapeSql(tokuisakiCd) & "'")
    End If

    If tokuisakiNm <> "" Then
      sb.AppendLine("  AND T.TokuiNM1 LIKE '%" & EscapeSql(tokuisakiNm) & "%'")
    End If

    sb.AppendLine("ORDER BY")
    sb.AppendLine("    T.TokuiCD")

    Return sb.ToString()
  End Function

  ''' <summary>
  ''' ON時のINSERT SQL
  ''' 既に存在する場合はUPDATE_DTとFLGを更新
  ''' </summary>
  Private Function BuildInsertSql(tokuisakiCd As String) As String
    Dim sb As New StringBuilder()
    Dim cd As String = EscapeSql(tokuisakiCd)

    sb.AppendLine("IF NOT EXISTS (SELECT 1 FROM M_TOKUISAKI_PRINT_CTRL WHERE TOKUISAKI_CD = '" & cd & "')")
    sb.AppendLine("BEGIN")
    sb.AppendLine("    INSERT INTO M_TOKUISAKI_PRINT_CTRL")
    sb.AppendLine("        (TOKUISAKI_CD)")
    sb.AppendLine("    VALUES")
    sb.AppendLine("        ('" & cd & "')")
    sb.AppendLine("END")
    sb.AppendLine("ELSE")
    sb.AppendLine("BEGIN")
    sb.AppendLine("    UPDATE M_TOKUISAKI_PRINT_CTRL")
    sb.AppendLine("       SET UPDATE_DT = GETDATE(),")
    sb.AppendLine("           INSTANT_PRINT_FLG = 1")
    sb.AppendLine("     WHERE TOKUISAKI_CD = '" & cd & "'")
    sb.AppendLine("END")

    Return sb.ToString()
  End Function

  ''' <summary>
  ''' OFF時のDELETE SQL
  ''' </summary>
  Private Function BuildDeleteSql(tokuisakiCd As String) As String
    Dim sb As New StringBuilder()
    Dim cd As String = EscapeSql(tokuisakiCd)

    sb.AppendLine("DELETE FROM M_TOKUISAKI_PRINT_CTRL")
    sb.AppendLine("WHERE TOKUISAKI_CD = '" & cd & "'")

    Return sb.ToString()
  End Function

  Private Function EscapeSql(value As String) As String
    If value Is Nothing Then
      Return ""
    End If

    Return value.Replace("'", "''")
  End Function

  Private Function ToBooleanSafe(value As Object) As Boolean
    If value Is Nothing OrElse IsDBNull(value) Then
      Return False
    End If

    If TypeOf value Is Boolean Then
      Return DirectCast(value, Boolean)
    End If

    Dim str As String = value.ToString().Trim()

    If str = "1" Then
      Return True
    End If

    If str = "0" OrElse str = "" Then
      Return False
    End If

    Return Convert.ToBoolean(value)
  End Function


End Class