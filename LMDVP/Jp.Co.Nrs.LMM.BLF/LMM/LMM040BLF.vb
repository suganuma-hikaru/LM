' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM040H : 届先マスタメンテナンス
'  作  成  者       :  [金へスル]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMM040BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM040BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM040BLC = New LMM040BLC()

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>    
    Private _Ds As DataSet

    ''' <summary>
    ''' 画面のZIP
    ''' </summary>
    ''' <remarks></remarks>    
    Private zipCdI As String

    ''' <summary>
    ''' 画面のJISコード
    ''' </summary>
    ''' <remarks></remarks>    
    Private jisCdI As String

    ''' <summary>
    ''' 画面の住所1
    ''' </summary>
    ''' <remarks></remarks>    
    Private Ad1I As String

    ''' <summary>
    ''' 画面の住所2
    ''' </summary>
    ''' <remarks></remarks>    
    Private Ad2I As String

    ''' <summary>
    ''' 画面の住所3
    ''' </summary>
    ''' <remarks></remarks>    
    Private Ad3I As String

    ''' <summary>
    ''' 郵便番号ＭのJISコード
    ''' </summary>
    ''' <remarks></remarks>    
    Private jisCdO As String

    ''' <summary>
    ''' 郵便番号Ｍの住所1
    ''' </summary>
    ''' <remarks></remarks>    
    Private Ad1O As String

    ''' <summary>
    ''' 郵便番号Ｍの住所2
    ''' </summary>
    ''' <remarks></remarks>    
    Private Ad2O As String

    ''' <summary>
    ''' 郵便番号Ｍの住所3
    ''' </summary>
    ''' <remarks></remarks>    
    Private Ad3O As String

#End Region

#Region "Const"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMM040IN"

    ''' <summary>
    ''' OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMM040OUT"

    ''' <summary>
    ''' INOUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_INOUT As String = "LMM040INOUT"

    ''' <summary>
    ''' 郵便番号(メッセージ用の名称)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ZIP As String = "郵便番号"

    ''' <summary>
    ''' JISコード(メッセージ用の名称)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const JIS As String = "JISコード"

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 届先マスタ更新対象データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = MyBase.CallBLC(Me._Blc, "SelectData", ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

    End Function

    ''' <summary>
    ''' 郵便番号マスタ情報取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectZipJisData(ByVal ds As DataSet) As DataSet

        '郵便番号マスタ情報取得
        Return MyBase.CallBLC(Me._Blc, "SelectZipJisData", ds)

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        If ("01").Equals(ds.Tables(LMM040BLF.TABLE_NM_IN).Rows(0).Item("MODE_FLG")) = True Then
            Return Me.ScopeStartEnd(ds, "InsertSaveAction", False)
        Else
            Return Me.ScopeStartEnd(ds, "InsertSaveAction")
        End If


    End Function

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        If ("01").Equals(ds.Tables(LMM040BLF.TABLE_NM_IN).Rows(0).Item("MODE_FLG")) = True Then
            Return Me.ScopeStartEnd(ds, "UpdateSaveAction", False)
        Else
            Return Me.ScopeStartEnd(ds, "UpdateSaveAction")
        End If

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, "DeleteAction")

    End Function

    ''' <summary>
    ''' 届先マスタ排他処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function HaitaData(ByVal ds As DataSet) As DataSet

        '排他チェック
        Return MyBase.CallBLC(Me._Blc, "CheckHaitaUserM", ds)

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function PrintData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(New LMM520BLC(), "DoPrint", ds)

    End Function

    ''' <summary>
    ''' 一括登録処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function Import(ByVal ds As DataSet) As DataSet

        Const MSG_DEF As String = "{0}行目：届先コード{1}：警告 {2}が{3}に存在しません。確認願います。({4})"

        Dim dt As DataTable = ds.Tables("LMM040_IMPORT")

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)
                Dim jis As String = String.Empty

                '郵便番号のマスタ登録状況および対応するJISコードの取得
                ds.Tables("LMM040_IMPORT_CHK").Rows.Clear()
                Dim newRow As DataRow = ds.Tables("LMM040_IMPORT_CHK").NewRow()
                newRow("NRS_BR_CD") = dr("NRS_BR_CD").ToString()
                newRow("ZIP") = dr("ZIP").ToString.Replace("-", "")
                ds.Tables("LMM040_IMPORT_CHK").Rows.Add(newRow)

                ds = MyBase.CallBLC(Me._Blc, "ImportChk", ds)

                '項目のチェック
                If ds.Tables("LMM040_IMPORT_CHK").Rows.Count = 0 Then
                    '郵便番号が未登録（警告するが処理続行）
                    Dim recNo As String = dr("ROW_NO").ToString()
                    Dim destCd As String = dr("DEST_CD").ToString()
                    Dim zip As String = dr("ZIP").ToString()
                    MyBase.SetMessageStore("00", "E01U", {String.Format(MSG_DEF, recNo, destCd, "郵便番号", "郵便マスタ", zip)})
                Else
                    jis = ds.Tables("LMM040_IMPORT_CHK").Rows(0).Item("JIS").ToString()
                    If String.IsNullOrEmpty(jis) Then
                        'JISコードが未登録（警告するが処理続行）
                        Dim recNo As String = dr("ROW_NO").ToString()
                        Dim destCd As String = dr("DEST_CD").ToString()
                        Dim zipJis As String = ds.Tables("LMM040_IMPORT_CHK").Rows(0).Item("ZIP_JIS").ToString()
                        MyBase.SetMessageStore("00", "E01U", {String.Format(MSG_DEF, recNo, destCd, "JISコード", "JISマスタ", zipJis)})
                    End If
                End If

                '届先マスタの存在チェック
                ds.Tables("LMM040IN").Rows.Clear()
                newRow = ds.Tables("LMM040IN").NewRow()
                newRow("NRS_BR_CD") = dr("NRS_BR_CD").ToString()
                newRow("CUST_CD_L") = dr("CUST_CD_L").ToString()
                newRow("DEST_CD") = dr("DEST_CD").ToString()
                ds.Tables("LMM040IN").Rows.Add(newRow)

                ds = MyBase.CallBLC(Me._Blc, "ImportExistChk", ds)

                'マスタ更新用データ編集（取込ファイルより）
                ds.Tables("LMM040IN").Rows.Clear()
                newRow = ds.Tables("LMM040IN").NewRow()
                newRow("NRS_BR_CD") = dr("NRS_BR_CD").ToString()
                newRow("CUST_CD_L") = dr("CUST_CD_L").ToString()
                newRow("DEST_CD") = dr("DEST_CD").ToString()
                newRow("DEST_NM") = dr("DEST_NM").ToString()
                newRow("ZIP") = dr("ZIP").ToString()

                Dim ad As String = dr("AD_1").ToString()
                Dim ad1 As String = Me.LeftB(ad, 40)
                ad = Mid(ad, ad1.Length + 1)
                Dim ad2 As String = String.Empty
                If Not String.IsNullOrEmpty(ad) Then
                    ad2 = Me.LeftB(ad, 40)
                End If
                newRow("AD_1") = ad1
                newRow("AD_2") = ad2

                newRow("AD_3") = dr("AD_3").ToString()
                newRow("TEL") = dr("TEL").ToString()
                newRow("JIS") = jis
                newRow("SHIHARAI_AD") = dr("SHIHARAI_AD").ToString()

                'マスタ更新用データ編集（その他固定値等）
                newRow("PICK_KB") = "01"

                ds.Tables("LMM040IN").Rows.Add(newRow)

                '更新
                If MyBase.GetResultCount() = 0 Then
                    '届先マスタに未登録ならば新規登録
                    ds = MyBase.CallBLC(Me._Blc, "ImportInsertData", ds)

                Else
                    '届先マスタに登録済みならば内容更新
                    ds = MyBase.CallBLC(Me._Blc, "ImportUpdateData", ds)

                End If

                'エラーがあるかを判定
                If MyBase.IsMessageExist() Then
                    Return ds
                End If
            Next

            'エラーが無ければCommit
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionStr">メソッド名</param>
    ''' <param name="accessFlg">アクセスフラグ</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ScopeStartEnd(ByVal ds As DataSet, ByVal actionStr As String, Optional ByVal accessFlg As Boolean = True) As DataSet

        Dim rtnResult As Boolean = False

        If actionStr.Equals("InsertSaveAction") = True AndAlso accessFlg = True Then
            ''2011.09.08 検証結果_導入時要望№1対応 START
            '郵便番号存在チェック
            'ds = MyBase.CallBLC(Me._Blc, "CheckZipM", ds)
            'If MyBase.IsErrorMessageExist() = False Then
            '郵便番号/JISコード存在チェック
            ds = MyBase.CallBLC(Me._Blc, "CheckZipJisM", ds)
            'End If
            ''2011.09.08 検証結果_導入時要望№1対応 END

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                '存在チェック
                ds = MyBase.CallBLC(Me._Blc, "CheckExistUserM", ds)
            End If


        ElseIf actionStr.Equals("UpdateSaveAction") = True AndAlso accessFlg = True Then
            ''2011.09.08 検証結果_導入時要望№1対応 START
            '郵便番号存在チェック
            'ds = MyBase.CallBLC(Me._Blc, "CheckZipM", ds)
            'If MyBase.IsErrorMessageExist() = False Then
            '郵便番号/JISコード存在チェック
            ds = MyBase.CallBLC(Me._Blc, "CheckZipJisM", ds)
            'End If
            ''2011.09.08 検証結果_導入時要望№1対応 END

        ElseIf actionStr.Equals("DeleteAction") = True Then
            '排他チェック
            ds = MyBase.CallBLC(Me._Blc, "CheckHaitaUserM", ds)

        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then

                If actionStr.Equals("InsertSaveAction") = True AndAlso accessFlg = True OrElse _
                   actionStr.Equals("UpdateSaveAction") = True AndAlso accessFlg = True Then

                    '画面の住所1～2・JISコードの設定
                    Call Me.SetAdJisIn(ds)

                    '①画面のJISコードが郵便番号マスタに存在しない場合 → 郵便番号マスタの値を設定
                    If MyBase.GetMessageID().Equals("W129") = True Then

                        '1)郵便番号マスタを検索した結果が1件の場合
                        If MyBase.GetResultCount() = 1 Then
                            '検索結果が1件の場合、郵便番号マスタの住所1～2・JISコードの設定
                            Call Me.SetAdJisOut(ds)
                            'START YANAI 要望番号825
                            ''住所1～2は、画面の値が空の場合のみ設定
                            'If String.IsNullOrEmpty(Ad1I) = True Then
                            '    ds.Tables(LMM040BLF.TABLE_NM_IN).Rows(0).Item("AD_1") = Ad1O
                            'End If
                            'If String.IsNullOrEmpty(Ad2I) = True Then
                            '    ds.Tables(LMM040BLF.TABLE_NM_IN).Rows(0).Item("AD_2") = Ad2O
                            'End If
                            'END YANAI 要望番号825
                            ''2011.08.25 検証結果一覧№4対応 START
                            '2)郵便番号マスタを検索した結果がN件の場合
                            'ElseIf MyBase.GetResultCount() > 1 Then
                            '    MyBase.SetMessage("E206", New String() {LMM040BLF.ZIP, LMM040BLF.JIS})
                            ''2011.08.25 検証結果一覧№4対応 END
                        End If
                        '②画面の郵便番号がマスタに存在しない場合 → 処理なし
                    ElseIf MyBase.GetMessageID().Equals("W176") = True Then
                        '処理なし
                    Else
                        '③画面のJISコードが郵便番号マスタに存在する場合 → 処理なし
                        jisCdO = ds.Tables(LMM040BLF.TABLE_NM_IN).Rows(0)("JIS").ToString()
                        If String.IsNullOrEmpty(zipCdI) = False AndAlso _
                          (String.IsNullOrEmpty(Ad1I) = True OrElse _
                           String.IsNullOrEmpty(Ad2I) = True OrElse _
                           String.IsNullOrEmpty(jisCdI) = True) Then
                            'BLCアクセス
                            ds = Me.BlcAccess(ds, "SelectZipJisData")
                            '1)郵便番号マスタを検索した結果が1件の場合
                            If ds.Tables(LMM040BLF.TABLE_NM_OUT).Rows.Count = 1 Then
                                Call Me.SetAdJisOut(ds)
                                'START YANAI 要望番号825
                                ''住所1～2は、画面の値が空の場合のみ設定
                                'If String.IsNullOrEmpty(Ad1I) = True Then
                                '    ds.Tables(LMM040BLF.TABLE_NM_IN).Rows(0).Item("AD_1") = Ad1O
                                'End If
                                'If String.IsNullOrEmpty(Ad2I) = True Then
                                '    ds.Tables(LMM040BLF.TABLE_NM_IN).Rows(0).Item("AD_2") = Ad2O
                                'End If
                                'END YANAI 要望番号825
                                ''2011.08.25 検証結果一覧№4対応 START
                                '2)郵便番号マスタを検索した結果がN件の場合
                                'ElseIf ds.Tables(LMM040BLF.TABLE_NM_OUT).Rows.Count > 1 Then
                                '    MyBase.SetMessage("E206", New String() {LMM040BLF.ZIP, LMM040BLF.JIS})
                                ''2011.08.25 検証結果一覧№4対応 END
                            End If
                        End If
                    End If

                    If (jisCdI).Equals(jisCdO) = True Then
                        '画面のJISコード = 郵便番号マスタから取得したJISコードの場合、そのまま
                        'BLCアクセス
                        ds = Me.BlcAccess(ds, actionStr)
                    Else
                        'トランザクション終了(ワーニングなのでBLCアクセスは行わない。)
                        rtnResult = True
                    End If

                Else
                    'BLCアクセス
                    ds = Me.BlcAccess(ds, actionStr)
                    If MyBase.GetMessageID().Equals("W155") = True Then
                        'トランザクション終了(ワーニングなので更新処理は行わない。)
                        rtnResult = True
                    End If

                End If

            End If

            'エラーがあるかを判定
            If rtnResult = False Then
                rtnResult = Not MyBase.IsMessageExist()
            End If

            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

                'INdatasetをINOUTDataset(遷移元画面に返却用)に置き換え
                ds.Tables(LMM040BLF.TABLE_NM_INOUT).Merge(ds.Tables(LMM040BLF.TABLE_NM_IN))

            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' BLCクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function BlcAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallBLC(Me._Blc, actionId, ds)

    End Function

    ''' <summary>
    ''' 画面の郵便番号・住所1～2・JISコードの設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetAdJisIn(ByVal ds As DataSet)

        zipCdI = ds.Tables(LMM040BLF.TABLE_NM_IN).Rows(0)("ZIP").ToString()
        jisCdI = ds.Tables(LMM040BLF.TABLE_NM_IN).Rows(0)("JIS").ToString()
        Ad1I = ds.Tables(LMM040BLF.TABLE_NM_IN).Rows(0)("AD_1").ToString()
        Ad2I = ds.Tables(LMM040BLF.TABLE_NM_IN).Rows(0)("AD_2").ToString()

    End Sub

    ''' <summary>
    ''' 郵便番号マスタから取得した住所1～2の設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetAdJisOut(ByVal ds As DataSet)

        If MyBase.GetMessageID().Equals("W129") = True Then
            jisCdO = ds.Tables(LMM040BLF.TABLE_NM_OUT).Rows(0)("JIS").ToString()
        End If
        Ad1O = ds.Tables(LMM040BLF.TABLE_NM_OUT).Rows(0)("AD_1").ToString()
        Ad2O = ds.Tables(LMM040BLF.TABLE_NM_OUT).Rows(0)("AD_2").ToString()

    End Sub

    ''' <summary>Left関数のバイト版。文字数をバイト数で指定して文字列を切捨て。</summary>
    ''' <param name="str">対象の文字列</param>
    ''' <param name="Length">切り抜く文字列のバイト数</param>
    ''' <returns>切捨てられた文字列</returns>
    ''' <remarks>最後の１バイトが全角文字の半分になる場合、その１バイトは無視される。</remarks>
    Public Function LeftB(ByVal str As String, Optional ByVal Length As Integer = 0) As String

        If str = "" Then
            Return ""
        End If

        'Lengthが0か、バイト数をオーバーする場合は全バイトが指定されたものとみなす。
        Dim RestLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str)

        If Length = 0 OrElse Length > RestLength Then
            Length = RestLength
        End If

        '切捨て
        Dim SJIS As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift-JIS")
        Dim B() As Byte = CType(Array.CreateInstance(GetType(Byte), Length), Byte())

        Array.Copy(SJIS.GetBytes(str), 0, B, 0, Length)

        Dim st1 As String = SJIS.GetString(B)

        '切捨てた結果、最後の１バイトが全角文字の半分だった場合、その半分は切り捨てる。
        Dim ResultLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(st1)

        If Length = ResultLength - 1 Then
            Return st1.Substring(0, st1.Length - 1)
        Else
            Return st1
        End If

    End Function

#End Region

#End Region

End Class
