Public Class clsGlobalData

  ''' <summary>
  '''  返品買戻し番号
  ''' </summary>
  Public Shared ReadOnly HENPIN_KAIMODOSHI_ID As Integer = 2

  ''' <summary>
  '''  加工賃の部位コードを登録
  ''' </summary>
  Public Shared ReadOnly listWageCode As String() = {"9999"}

  ''' <summary>
  ''' ＦＴＰサーバーデータディレクトリ
  ''' </summary>
  Public Shared ReadOnly FTPDir As String = "\\nikserver21\FTPDATA"

  ''' <summary>
  ''' バックアップ用Accdbの保存先
  ''' </summary>
  Public Shared ReadOnly BACKUP_FILENAME As String = "TrzBackup.accdb"

  ''' <summary>
  ''' 印刷帳票の保存先
  ''' </summary>
  Public Shared ReadOnly REPORT_FILENAME As String = "TrzReports.accdb"
  Public Shared ReadOnly REPORT_FILENAME2 As String = "TrzReports2.accdb"

  ''' <summary>
  ''' 印刷用Access元ファイル
  ''' </summary>
  ''' <remarks>
  '''  実行時は本ファイルを実行ファイルと同じフォルダにコピーして使用する
  ''' </remarks>
  Public Shared ReadOnly REPORT_ORG_FILEPATH As String = "D:\TRZdotDX\report\TrzReports_org.accdb"

  ''' <summary>
  ''' 印刷プレビューフラグ
  ''' </summary>
  Public Shared ReadOnly PRINT_PREVIEW As Integer = 1     '0：プレビューしない、1：プレビューする

  ''' <summary>
  ''' 集計のSP区分（セット、パーツを区別して実績表を出力　０：区別なし、１：区別有り）
  ''' </summary>
  Public Shared ReadOnly SHUKEI_SP_KUBUN As Integer = 0

  ''' <summary>
  ''' セット処理表（０：横、１：縦、２：縦・種別別）
  ''' </summary>
  Public Shared ReadOnly SHUKEI_TATEYOKO As Integer = 2

  ''' <summary>
  ''' 集計の並び
  ''' </summary>
  Public Shared ReadOnly SHUKEI_NARABI As Integer = 0

  ''' <summary>
  ''' 牛捌き単価
  ''' </summary>
  Public Shared ReadOnly NIPPO_GSABAKI As Integer = 22

  ''' <summary>
  ''' 豚捌き単価
  ''' </summary>
  Public Shared ReadOnly NIPPO_BSABAKI As Integer = 1000

  ''' <summary>
  ''' 動作モード
  ''' </summary>
  Public Shared ReadOnly SEISAN_TYPE As Integer = 1

  ''' <summary>
  ''' 枝肉部位コード
  ''' </summary>
  Public Shared ReadOnly EDANIKU_CODE As Integer = 0

  ''' <summary>
  ''' 左右区分（左）
  ''' </summary>
  Public Shared ReadOnly PARTS_SIDE_LEFT As Integer = 1

  ''' <summary>
  ''' 左右区分（右）
  ''' </summary>
  Public Shared ReadOnly PARTS_SIDE_RIGHT As Integer = 2

  ''' <summary>
  ''' 左右区分（1頭）
  ''' </summary>
  Public Shared ReadOnly PARTS_SIDE_BOTH As Integer = 0

  ''' <summary>
  ''' 枝別精算パスワード
  ''' </summary>
  Public Shared ReadOnly EDASEISAN_PASSWORD As String = "0714"

  ''' <summary>
  ''' 枝番最大値
  ''' </summary>
  Public Shared ReadOnly EDABAN_MAX As Integer = 99999

  ''' <summary>
  ''' 枝番最小値
  ''' </summary>
  Public Shared ReadOnly EDABAN_MIN As Integer = 10001

  ''' <summary>
  ''' 自社電話番号・ＦＡＸ
  ''' </summary>
  Public Shared ReadOnly JISSYA_SYAMEI As String = "有限会社 三弘食品 TEL 06-6747-3123"

  ' 得意先コードゼロ詰め
  Public Shared ReadOnly CUSTOMER_ZERO_PADDING As String = "000000"
  ' 直送先コードゼロ詰め
  Public Shared ReadOnly CHOKU_ZERO_PADDING As String = "000000"

End Class
