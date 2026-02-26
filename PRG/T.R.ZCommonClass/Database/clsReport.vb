Imports T.R.ZCommonClass.clsCommonFnc

Public Class clsReport
  Inherits clsComDatabase

  Public Sub New()

  End Sub

  Public Sub New(rptFileName As String)

    Try

      '自分自身の存在するフォルダ  
      Dim strPath As String = System.IO.Path.GetDirectoryName(Application.ExecutablePath)

      'コピー先のファイル名作成  
      Dim DestinationFile As String = System.IO.Path.Combine(strPath, rptFileName)


      'ファイルの存在有無確認
      If System.IO.File.Exists(DestinationFile) Then
        ' 読み取り禁止の場合、解除する
        clsCommonFnc.ReleaseReadOnly(DestinationFile)
        'ファイルを削除
        Dim fi As New System.IO.FileInfo(DestinationFile)
        fi.Delete()
      End If

      'レポートファイルのコピー
      System.IO.File.Copy(clsGlobalData.REPORT_ORG_FILEPATH, DestinationFile)

      ' 読み取り禁止の場合、解除する
      clsCommonFnc.ReleaseReadOnly(DestinationFile)

      Me.DataSource = DestinationFile
      Me.Provider = typProvider.Accdb

    Catch ex As Exception
      Call ComWriteErrLog(ex)
      Throw New Exception("レポート出力用Accessファイルの初期化に失敗しました。")
    End Try

  End Sub

End Class
