Public Class clsIpcService
  Inherits MarshalByRefObject

  'クライアントから呼び出しを受け、イベントを発生させる
  Public Sub RaiseServerEvent(ByVal message As String)
    'イベントを発生させる
    RaiseEvent RaiseClientEvent(message)
  End Sub

  Public Event RaiseClientEvent(ByVal messsage As String)

  ''' <summary>
  ''' 初期化イベント
  ''' </summary>
  ''' <returns></returns>
  Public Overrides Function InitializeLifetimeService() As Object
    ' 期限切れを発生させないため、本イベントをオーバーライドしてます
    Return Nothing
  End Function
End Class
