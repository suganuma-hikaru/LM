' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI962V : EDI入荷データ編集
'  作  成  者       :  
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI962Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMI962V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI962F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI962F)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region 'Constructor

#Region "Method"

    Friend Function TourokuCheck() As Boolean

        With Me._Frm.sprWarning.ActiveSheet

            Dim max As Integer = .Rows.Count - 1
            Dim loadNumber As String = String.Empty
            Dim shoriKb As String = String.Empty
            Dim mstFlg As String = String.Empty
            Dim goodsNm As String = String.Empty
            '2012.06.01 ディック届先マスタ対応 追加START
            Dim destNm As String = String.Empty
            '2012.06.01 ディック届先マスタ対応 追加END

            'チェック用連想配列
            Dim ht As Dictionary(Of String, String) = New Dictionary(Of String, String)

            If max = -1 Then
                'メッセージエリアの設定
                MyBase.ShowMessage("E370")
                Return False
            End If

            For i As Integer = 0 To max

                loadNumber = .Cells(i, LMI962G.sprWarning.LOAD_NUMBER.ColNo).Value().ToString()
                shoriKb = Me.NullConvertString(.Cells(i, LMI962G.sprWarning.SHORI.ColNo).Value).ToString()
                mstFlg = .Cells(i, LMI962G.sprWarning.EDI_WARNING_ID.ColNo).Value.ToString().Substring(LMI962C.WARNING_ID_FMT.MST_FLG.START_IDX, LMI962C.WARNING_ID_FMT.MST_FLG.LEN)
                goodsNm = Me.NullConvertString(.Cells(i, LMI962G.sprWarning.GOODS_NM.ColNo).Value).ToString()
                '2012.06.01 ディック届先マスタ対応 追加START
                destNm = Me.NullConvertString(.Cells(i, LMI962G.sprWarning.DEST_NM.ColNo).Value).ToString()
                '2012.06.01 ディック届先マスタ対応 追加END

                '処理が未選択の場合、エラー
                If String.IsNullOrEmpty(shoriKb) = True Then
                    'メッセージエリアの設定
                    MyBase.ShowMessage("E199", New String() {"処理"})
                    Return False
                End If

                'LoadNumberが同一で一方のみが「キャンセル」を選択している場合はエラー
                If ht.ContainsKey(loadNumber) = True Then
                    If shoriKb <> ht(loadNumber) AndAlso (shoriKb = LMI962C.SELECT_CANCEL OrElse ht(loadNumber) = LMI962C.SELECT_CANCEL) Then
                        'メッセージエリアの設定
                        MyBase.ShowMessage("E01Y", New String() {"Load Number", "Load Number：" & loadNumber})
                        Return False
                    End If
                Else
                    ht.Add(loadNumber, shoriKb)
                End If

                '商品マスタ参照「はい」かつ商品が未選択の場合エラー
                If mstFlg.Equals(LMI962C.WARNING_ID_FMT.MST_FLG.M_GOODS) = True AndAlso shoriKb.Equals(LMI962C.SELECT_YES) = True AndAlso String.IsNullOrEmpty(goodsNm) = True Then
                    'メッセージエリアの設定
                    MyBase.ShowMessage("E01Z", New String() {"商品", "商品", "Load Number：" & loadNumber})
                    Return False
                End If

                '2012.06.01 ディック届先マスタ対応 追加START
                '届先マスタ参照「はい」かつ届先が未選択の場合エラー
                If mstFlg.Equals(LMI962C.WARNING_ID_FMT.MST_FLG.M_DEST) = True AndAlso shoriKb.Equals(LMI962C.SELECT_YES) = True AndAlso String.IsNullOrEmpty(destNm) = True Then
                    'メッセージエリアの設定
                    MyBase.ShowMessage("E01Z", New String() {"届先", "届先", "Load Number：" & loadNumber})
                    Return False
                End If
                '2012.06.01 ディック届先マスタ対応 追加END

            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk() As Boolean

        Return True

    End Function

    ''' <summary>
    ''' Null変換（文字列）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertString(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = String.Empty
        ElseIf value Is Nothing Then
            value = String.Empty
        End If

        Return value

    End Function

#End Region 'Method

End Class
