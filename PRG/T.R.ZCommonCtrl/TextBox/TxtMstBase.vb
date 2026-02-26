Imports T.R.ZCommonClass.clsCommonFnc

Public Class TxtMstBase
  Inherits TxtNumericBase

  '----------------------------------------------
  '  マスタ検索テキストボックスベースクラス
  ' ・Enterキー入力時に以下の動作を行う
  '    コード入力有り - 該当する名称を取得
  '    コード入力無し - 共通検索フォームを表示し値の選択が可能
  '----------------------------------------------

#Region "メンバ"

#Region "プライベート"

  ''' <summary>
  ''' 名称表示用テキストボックス
  ''' </summary>
  Private _txtName As TextBox

  ''' <summary>
  ''' 一覧タイトル（コード）
  ''' </summary>
  Private _CodeName As String

  ''' <summary>
  ''' 一覧タイトル（値）
  ''' </summary>
  Private _ValueName As String

  Private _Text As String

  Private _ShowSf As Boolean = True

  ''' <summary>
  ''' 検索サブフォームタイトル
  ''' </summary>
  Private _FormTitle As String = String.Empty
#End Region

#Region "パブリック"
  Delegate Function CallBackCreateGridSrcSql() As String
  Public lcCallBackCreateGridSrcSql As CallBackCreateGridSrcSql = Nothing

  Delegate Sub CallBackMstTxtValidated(sender As Object, e As EventArgs)
  Public lcCallBackMstTxtValidated As CallBackMstTxtValidated = Nothing
#End Region

#End Region

#Region "プロパティー"
#Region "パブリック"

  ''' <summary>
  ''' テキストプロパティーオーバーライド
  ''' </summary>
  ''' <returns></returns>
  ''' <remarks>
  '''   TxtAAAA.Text = xxxx の形式でソースより値を設定された場合に
  '''   検索処理を実行しラベルに名称を設定
  ''' </remarks>
  Public Overloads Property Text As String
    Get
      Return _Text
    End Get

    Set(value As String)
      _Text = value
      MyBase.Text = value

      ' 空白が設定された場合はラベル表示をクリア
      If _Text.Trim = "" AndAlso Me._txtName IsNot Nothing Then
        Me._txtName.Text = ""
      ElseIf _ShowSf Then
        ' 空白以外が設定された場合は検索フォームより値を取得する
        GetItemNameFromSf()
      End If
    End Set

  End Property

#End Region
#End Region

#Region "コンストラクタ"

  Public Sub New()

  End Sub

  ''' <summary>
  ''' コンストラクタ
  ''' </summary>
  ''' <param name="prmCodeName">一覧タイトル（コード）</param>
  ''' <param name="prmValueName">一覧タイトル（値）</param>
  Public Sub New(prmCodeName As String _
                , prmValueName As String)

    _CodeName = prmCodeName
    _ValueName = prmValueName
    MyBase.lcCallBackValidated = AddressOf TxtMstBase_Validated
  End Sub
#End Region

#Region "メソッド"

#Region "パブリック"

  ''' <summary>
  ''' 名称表示用ラベル設定
  ''' </summary>
  ''' <param name="prmCtrl"></param>
  Public Sub SetNameCtrl(prmCtrl As TextBox)
    _txtName = prmCtrl

    ' 名称表示用テキストボックスデザイン設定
    With _txtName
      .Enabled = False
      .ReadOnly = True
      .BorderStyle = BorderStyle.None
      .BackColor = .Parent.BackColor
    End With
  End Sub

  ''' <summary>
  ''' フォームタイトル設定
  ''' </summary>
  ''' <param name="prmFormTitle"></param>
  Public Sub SetFormTitle(prmFormTitle As String)
    _FormTitle = prmFormTitle
  End Sub
#End Region

#Region "プライベート"
  Private Sub GetItemNameFromSf()
    Dim tmpSerchForm As New ComSearchForm
    Dim tmpItemData As New Dictionary(Of String, String)

    '汎用検索画面表示
    tmpItemData = tmpSerchForm.ShowSubForm(_CodeName, _ValueName, lcCallBackCreateGridSrcSql(), _FormTitle, Trim(_Text & ""))

    ' サブフォームで選択された値をテキストボックスとラベルに設定する
    If tmpItemData.Keys.Count > 0 Then

      _ShowSf = False
      Me.Text = tmpItemData("ItemCode")
      _ShowSf = True

      If Me._txtName IsNot Nothing Then
        Me._txtName.Text = tmpItemData("ItemName")
      End If
    End If

  End Sub
#End Region

#End Region

#Region "イベントプロシージャー"

  ''' <summary>
  ''' キー入力時処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  ''' <remarks>
  ''' KeyDownで実行するとカーソル移動が不細工なのでPreviewKeyDownで実行します
  ''' </remarks>
  Private Sub TxtMstBas_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles MyBase.PreviewKeyDown
    If e.KeyCode = Keys.Enter Then
      ' エンターキー入力時処理
      _Text = MyBase.Text
      ' サブフォームから値を取得する
      Call GetItemNameFromSf()
    End If

  End Sub


  Private Sub TxtMstBase_Validated(sender As Object, e As EventArgs)

    Me._Text = MyBase.Text

    If Me.LastText <> Me.Text Then
      ' 値は変更された

      If Me.Text <> "" Then
        '値は入力されている

        Call GetItemNameFromSf()  ' サブフォームから値を取得する
      Else
        ' 値は入力されていない

        If Me._txtName IsNot Nothing Then
          Me._txtName.Text = ""             ' ラベル表示クリア
        End If
      End If
    End If

    If lcCallBackMstTxtValidated IsNot Nothing Then
      Call lcCallBackMstTxtValidated(sender, e)
    End If
  End Sub

#End Region

End Class
