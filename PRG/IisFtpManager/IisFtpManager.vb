Imports System.ServiceProcess

Public Class IisFtpManager
  Public Shared Sub StartFtp(siteName As String)
    Try
      Dim ftpService As New ServiceController(siteName)
      If ftpService.Status = ServiceControllerStatus.Stopped Then
        ftpService.Start()
        ftpService.WaitForStatus(ServiceControllerStatus.Running)
      End If
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub

  Public Shared Sub StopFtp(siteName As String)
    Try
      Dim ftpService As New ServiceController(siteName)
      If ftpService.Status = ServiceControllerStatus.Running Then
        ftpService.Stop()
        ftpService.WaitForStatus(ServiceControllerStatus.Stopped)
      End If
    Catch ex As Exception
      Throw New Exception(ex.Message)
    End Try
  End Sub
End Class