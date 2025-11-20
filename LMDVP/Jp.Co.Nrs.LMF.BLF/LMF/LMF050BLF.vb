' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF050BLF : 運賃編集
'  作  成  者       :  菱刈
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMF050BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF050BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMF050BLC = New LMF050BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 運賃マスタ更新対象データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'データ件数取得
        ds = MyBase.CallBLC(Me._Blc, "SelectData", ds)
        'メッセージの判定
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)
        
        Return ds

    End Function

    ''' <summary>
    ''' 運賃マスタ排他処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function HaitaData(ByVal ds As DataSet) As DataSet

        '請求鏡ヘッダ
        ds = MyBase.CallBLC(Me._Blc, "HedChcik", ds)

        '請求鏡ヘッダー時にエラーがあった場合
        If MyBase.IsMessageExist() = True Then

            'メッセージの再セット
            'START YANAI 要望番号607
            'MyBase.SetMessage("E232", New String() {"経理取込", "編集"})
            '2016.01.06 UMANO 英語化対応START
            'MyBase.SetMessage("E307", New String() {"鑑取込"})
            '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen del start
            'MyBase.SetMessage("E885")
            '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen del end
            '2016.01.06 UMANO 英語化対応END
            'END YANAI 要望番号607

        End If

        If MyBase.IsMessageExist() = False Then
            '排他チェック
            ds = MyBase.CallBLC(Me._Blc, "CheckHaitaUnchinM", ds)

        End If

        Return ds

    End Function

    'START YANAI 要望番号561
    '''' <summary>
    '''' 再検索時に使用
    '''' </summary>
    '''' <param name="ds">DataSet(更新用)</param>
    '''' <remarks></remarks>
    'Private Function Saiken(ByVal ds As DataSet) As DataSet

    '    'データテーブルの宣言
    '    Dim inTbl As DataTable = ds.Tables("LMF050IN")

    '    'INのデータテーブルをクリア
    '    inTbl.Clear()

    '    'OUTのテーブルにINと同じ物があればデータセット
    '    inTbl.ImportRow(ds.Tables("LMF050OUT").Rows(0))

    '    '再検索
    '    ds = Me.SelectListData(ds)

    '    Return ds

    'End Function
    'END YANAI 要望番号561

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 運賃マスタ更新処理(保存)
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        '請求鏡ヘッダ
        ds = MyBase.CallBLC(Me._Blc, "HedChcik", ds)

        '請求鏡ヘッダー時にエラーがあった場合
        If MyBase.IsMessageExist() = True Then

            'メッセージの再セット
            'START YANAI 要望番号607
            'MyBase.SetMessage("E232", New String() {"経理取込", "保存"})
            '2016.01.06 UMANO 英語化対応START
            'MyBase.SetMessage("E307", New String() {"鑑取込"})
            '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen del start
            'MyBase.SetMessage("E885")
            '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen del end
            '2016.01.06 UMANO 英語化対応END
            'END YANAI 要望番号607
            Return ds

        End If

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateUnchinM", ds)
            End If

            'メッセージがなかったらtrue
            rtnResult = Not MyBase.IsMessageExist()

            'リターンフラグでメッセージ判定trueのときはトランザクション終了
            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        'START YANAI 要望番号561
        ''更新成功の場合、再検索
        'If rtnResult = True Then

        '    Return Me.Saiken(ds)

        'End If
        'END YANAI 要望番号561

        Return ds

    End Function

    ''' <summary>
    ''' 運賃マスタ更新処理(確定)
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function UpdateDataKakutei(ByVal ds As DataSet) As DataSet

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = False Then
            '排他チェック
            ds = MyBase.CallBLC(Me._Blc, "CheckHaitaUnchinM", ds)
        End If

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateUnchinKakuteiM", ds)
            End If

            'エラーメッセージがなければtrue
            rtnResult = Not MyBase.IsMessageExist()

            'フラグでエラーメッセージがtrueの場合トランザクション終了
            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        'START YANAI 要望番号561
        ''確定成功の場合、再検索
        'If rtnResult = True Then

        '    '再検索時のINパラメータ取得
        '    Return Me.Saiken(ds)

        'End If
        'END YANAI 要望番号561

        Return ds

    End Function

    ''' <summary>
    ''' 運賃マスタ更新処理(確定解除)
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function UpdateDataKakuteikaijo(ByVal ds As DataSet) As DataSet

        '請求鏡ヘッダ
        ds = MyBase.CallBLC(Me._Blc, "HedChcik", ds)

        '請求鏡ヘッダー時にエラーがあった場合
        If MyBase.IsMessageExist() = True Then

            'メッセージの再セット
            'START YANAI 要望番号607
            'MyBase.SetMessage("E232", New String() {"経理取込", "確定解除"})
            '2016.01.06 UMANO 英語化対応START
            'MyBase.SetMessage("E307", New String() {"鑑取込"})
            '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen del start
            'MyBase.SetMessage("E885")
            '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen del end
            '2016.01.06 UMANO 英語化対応END
            'END YANAI 要望番号607
            Return ds

        End If

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = False Then
            '排他チェック
            ds = MyBase.CallBLC(Me._Blc, "CheckHaitaUnchinM", ds)
        End If

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateUnchinKakuteiKijoM", ds)
            End If

            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using


        'START YANAI 要望番号561
        ''確定解除成功の場合、再検索
        'If rtnResult = True Then

        '    '再検索時のINパラメータ取得
        '    Return Me.Saiken(ds)

        'End If
        'END YANAI 要望番号561

        Return ds

    End Function

#End Region

#End Region

End Class
