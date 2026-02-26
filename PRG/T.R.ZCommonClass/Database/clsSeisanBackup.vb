Imports T.R.ZCommonClass.clsCommonFnc

Public Class clsSeisanBackup
  Inherits clsComDatabase

  Public Sub New()

    Try

      '自分自身の存在するフォルダ  
      Dim strPath As String = System.IO.Path.GetDirectoryName(Application.ExecutablePath)

      Dim backupFileName As String = String.Empty

      ' ファイル名またはパスから拡張子を除いたファイル名を取得
      backupFileName = System.IO.Path.GetFileNameWithoutExtension(clsGlobalData.BACKUP_FILENAME)

      '更新日を取得し、DateTime値に変換する
      Dim dt As DateTime = DateTime.Parse(ComGetProcTime())
      backupFileName += dt.ToString("_yyyyMMddHHmmss.accdb")

      'コピー先のファイル名作成  
      Dim DestinationFile As String = System.IO.Path.Combine(strPath, backupFileName)

      'コピー元のファイル名作成  
      Dim SourceFile As String = System.IO.Path.Combine(strPath, clsGlobalData.BACKUP_FILENAME)

      'ファイルの存在有無確認
      If System.IO.File.Exists(DestinationFile) Then
        ' 読み取り禁止の場合、解除する
        clsCommonFnc.ReleaseReadOnly(DestinationFile)
        'ファイルを削除
        Dim fi As New System.IO.FileInfo(DestinationFile)
        fi.Delete()
      End If

      '精算ファイルのコピー
      System.IO.File.Copy(SourceFile, DestinationFile)

      ' 読み取り禁止の場合、解除する
      clsCommonFnc.ReleaseReadOnly(DestinationFile)

      Me.DataSource = DestinationFile
      Me.Provider = typProvider.Accdb

    Catch ex As Exception

      Call ComWriteErrLog(ex)

    End Try

  End Sub

End Class
