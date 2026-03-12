Imports Common.ClsFunction
Public Class ClsCommonGlobalData

  Public Shared ReadOnly DB_DATASOURCE As String = ReadConnectIniFile("DB_DATASOURCE", "VALUE")
  Public Shared ReadOnly DB_DEFAULTDATABASE As String = ReadConnectIniFile("DB_DEFAULTDATABASE", "VALUE")
  Public Shared ReadOnly DB_USERID As String = ReadConnectIniFile("DB_USERID", "VALUE")
  Public Shared ReadOnly DB_PASSWORD As String = ReadConnectIniFile("DB_PASSWORD", "VALUE")

  ' 得意先コードゼロ詰め
  Public Shared ReadOnly CUSTOMER_ZERO_PADDING As String = "000000"
  ' 商品コードゼロ詰め
  Public Shared ReadOnly ITEM_ZERO_PADDING As String = "000000"
  ' 得意先コードゼロ詰め
  Public Shared ReadOnly TANTO_ZERO_PADDING As String = "00"
  ' 直送先コードゼロ詰め
  Public Shared ReadOnly CHOKU_ZERO_PADDING As String = "00"

  ''' <summary>
  ''' 印刷帳票の保存先
  ''' </summary>
  Public Shared ReadOnly REPORT_FILENAME As String = "Report.accdb"
  ''' <summary>
  ''' 印刷プレビューフラグ
  ''' </summary>
  Public Shared ReadOnly PRINT_PREVIEW As Integer = 1     '0：プレビューしない、1：プレビューする
  ''' <summary>
  ''' 印刷プレビューフラグ
  ''' </summary>
  Public Shared ReadOnly PRINT_NON_PREVIEW As Integer = 0     '0：プレビューしない、1：プレビューする
  ''' <summary>
  ''' 印刷用Access元ファイル
  ''' </summary>
  ''' <remarks>
  '''  実行時は本ファイルを実行ファイルと同じフォルダにコピーして使用する
  ''' </remarks>
  Public Shared ReadOnly REPORT_ORG_FILEPATH As String = "C:\AUTOPRT\report\Report.accdb"

  ''' <summary>
  ''' レポートタイプ印刷なし
  ''' </summary>
  Public Shared ReadOnly REPORT_TYPE_NONE As String = "0"

  ''' <summary>
  ''' レポートタイプ納品書
  ''' </summary>
  Public Shared ReadOnly REPORT_TYPE_NOHIN As String = "1"

  ''' <summary>
  ''' レポートタイプ出荷明細書
  ''' </summary>
  Public Shared ReadOnly REPORT_TYPE_SHUKKA As String = "2"

  ''' <summary>
  ''' 実績ファイル名
  ''' </summary>
  Public Shared ReadOnly TRAN_FILE_NAME As String = "40TRAN"
End Class
