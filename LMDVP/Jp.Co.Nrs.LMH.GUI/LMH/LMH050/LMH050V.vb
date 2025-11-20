' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH050V : EDI入荷データ編集
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMH050Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMH050V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMH050F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMH050F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

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
            Dim ediCtlNo As String = String.Empty
            Dim shoriKb As String = String.Empty
            Dim mstFlg As String = String.Empty
            Dim goodsNm As String = String.Empty
            '2012.06.01 ディック届先マスタ対応 追加START
            Dim destNm As String = String.Empty
            '2012.06.01 ディック届先マスタ対応 追加END

            'チェック用連想配列
            Dim ht As Hashtable = New Hashtable()

            If max = -1 Then
                'メッセージエリアの設定
                MyBase.ShowMessage("E370")
                Return False
            End If

            For i As Integer = 0 To max

                ediCtlNo = .Cells(i, LMH050G.sprWarning.KANRI_NO_L.ColNo).Value().ToString()
                shoriKb = Me.NullConvertString(.Cells(i, LMH050G.sprWarning.SYORI.ColNo).Value).ToString()
                mstFlg = .Cells(i, LMH050G.sprWarning.EDI_WARNING_ID.ColNo).Value.ToString().Substring(7, 1)
                goodsNm = Me.NullConvertString(.Cells(i, LMH050G.sprWarning.GOODS_NM.ColNo).Value).ToString()
                '2012.06.01 ディック届先マスタ対応 追加START
                destNm = Me.NullConvertString(.Cells(i, LMH050G.sprWarning.DEST_NM.ColNo).Value).ToString()
                '2012.06.01 ディック届先マスタ対応 追加END

                '処理が未選択の場合、エラー
                If String.IsNullOrEmpty(shoriKb) = True Then
                    'メッセージエリアの設定
                    MyBase.ShowMessage("E199", New String() {"処理"})
                    Return False
                End If


                'EDI管理番号(大)が同一で一方のみが「キャンセル」を選択している場合はエラー
                If ht.ContainsKey(ediCtlNo) = True Then
                    If shoriKb <> ht(ediCtlNo).ToString() AndAlso (shoriKb = LMH050C.SELECT_CANCEL OrElse ht(ediCtlNo).ToString() = LMH050C.SELECT_CANCEL) Then

                        'メッセージエリアの設定
                        MyBase.ShowMessage("E354", New String() {ediCtlNo.ToString()})
                        Return False
                    End If

                Else

                    ht.Add(ediCtlNo, shoriKb)

                End If

                '商品マスタ参照「はい」かつ商品が未選択の場合エラー
                If mstFlg.Equals("1") = True AndAlso shoriKb.Equals(LMH050C.SELECT_YES) = True AndAlso String.IsNullOrEmpty(goodsNm) = True Then
                    'メッセージエリアの設定
                    'MyBase.ShowMessage("E358", New String() {ediCtlNo.ToString()})
                    MyBase.ShowMessage("E358", New String() {"商品", ediCtlNo.ToString()})
                    Return False

                End If

                '2012.06.01 ディック届先マスタ対応 追加START
                '届先マスタ参照「はい」かつ届先が未選択の場合エラー
                If mstFlg.Equals("2") = True AndAlso shoriKb.Equals(LMH050C.SELECT_YES) = True AndAlso String.IsNullOrEmpty(destNm) = True Then

#If False Then ' 届先自動変換対応(日立物流) 20170407 changed by inoue 
                    'メッセージエリアの設定
                    'MyBase.ShowMessage("E358", New String() {ediCtlNo.ToString()})
                    MyBase.ShowMessage("E358", New String() {"届先", ediCtlNo.ToString()})
                    Return False
#Else

                    If (Me.IsSkipDestNotSelectedCheck(i) = False) Then
                        'メッセージエリアの設定
                        MyBase.ShowMessage("E358", New String() {"届先", ediCtlNo.ToString()})
                        Return False
                    End If
#End If
                End If
                '2012.06.01 ディック届先マスタ対応 追加END

            Next

        End With

        Return True

    End Function


#If True Then ' 届先自動変換対応(日立物流) 20170407 added by inoue 
    ''' <summary>
    ''' 届先の未選択チェックの実施要否を判定する
    ''' </summary>
    ''' <param name="rowIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSkipDestNotSelectedCheck(ByVal rowIndex As Integer) As Boolean

        With Me._Frm.sprWarning.ActiveSheet

            Dim warningId As String _
                = Me.NullConvertString(.Cells(rowIndex _
                                            , LMH050G.sprWarning.EDI_WARNING_ID.ColNo).Value).ToString()

            ' ToDo: スキップ対象が頻繁に追加されるようであれば、ワーニングID内のマスターフラグの分割を検討する
            If (LMH050C.EDI_WARNING_ID.DIC_WID_L017.Equals(warningId)) Then
                ' 日立物流の届先変換の届先選択ワーニング)の場合、未選択チェックを実施しない。
                Return True
            End If
        End With

        Return False

    End Function
#End If



    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk() As Boolean
        ''Dim strDept As String = LMUserInfoManager.GetDeptKbn
        ''Dim strAuth As String = LMUserInfoManager.GetAuthFlg

        ''If strDept <> LMH050C.DEPT_KEIRI And strDept <> LMH050C.DEPT_ZAIMU Then
        ''    Me.ShowMessage("E016")
        ''    Return False
        ''End If

        ''If strAuth = LMH050C.AUTH_ROM Then
        ''    Me.ShowMessage("E016")
        ''    Return False
        ''End If

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
