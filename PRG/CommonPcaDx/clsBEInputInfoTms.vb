Public Class clsBEInputInfoTms
  Inherits clsDetailBase

#Region "プロパティー"
#Region "パブリック"
  ''' <summary>
  ''' エンティティバージョン
  ''' </summary>
  Public ReadOnly Property エンティティバージョン As String
    Get
      Return GetRowData("BEVersion")
    End Get
  End Property

  ''' <summary>
  ''' 取得状態
  ''' </summary>
  Public ReadOnly Property 取得状態 As String
    Get
      Return GetRowData("Status")
    End Get
  End Property

  ''' <summary>
  ''' 金額端数
  ''' </summary>
  ''' <returns></returns>
  Public ReadOnly Property 金額端数 As String
    Get
      Return GetRowData("KingakuHasu")
    End Get
  End Property
#End Region
#End Region

End Class
