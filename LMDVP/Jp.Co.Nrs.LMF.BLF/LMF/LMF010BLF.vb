' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブ
'  プログラムID     :  LMF010BLF : 運送検索
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMF010BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF010BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMF010BLC = New LMF010BLC()

    Private _Blc690 As LMF690BLC = New LMF690BLC()
    Private _Blc700 As LMF700BLC = New LMF700BLC()
    Private _Blc710 As LMF710BLC = New LMF710BLC()

#End Region

#Region "Const"

    ''' <summary>
    ''' 運送(大)検索対象データ件数検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_COUNT As String = "SelectData"

    ''' <summary>
    ''' 運送(特大)の運送(特大)件数検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_COUNT_LL As String = "SelectLLCountData"

    ''' <summary>
    ''' データセットテーブル名(ITEMテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_ITEM As String = "ITEM"

    ''' <summary>
    ''' データセットテーブル名(UNSO_Lテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNSO_L As String = "UNSO_L"

    ''' <summary>
    ''' データセットテーブル名(UNSOCOマスタ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNSOCO As String = "UNSOCO"

    ''' <summary>
    ''' データセットテーブル名(ERRテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_ERR As String = "ERR"

    ''' <summary>
    ''' 修正項目(運行番号)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_TRIP As String = "01"

    'START UMANO 要望番号1369 支払運賃に伴う修正。
    ''' <summary>
    ''' 修正項目(運送番号)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_UNSOCO As String = "03"
    'END UMANO 要望番号1369 支払運賃に伴う修正。

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' LMF810INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_810IN As String = "LMF810IN"
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' LMF800RESULTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_RTN2_TBL As String = "LMF810RESULT"
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 計算プログラムの戻り値(正常終了)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const RTN_KBN_NORMAL As String = "00"
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 計算プログラムの戻り値(ワーニング)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const RTN_KBN_WARNING As String = "05"
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 計算プログラムの戻り値(運賃Zero円)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const RTN_KBN_ZEROYEN As String = "30"
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 運賃テーブル更新アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_CALC_DATA As String = "CreateUnchinData"
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START UMANO 要望番号1369 運行紐付け時切替対応
    ''' <summary>
    ''' 一括変更(登録)アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SAVE As String = "SaveAction"
    'END UMANO 要望番号1369 運行紐付け時切替対応

    'START UMANO 要望番号1369 運行紐付け時切替対応
    ''' <summary>
    ''' 一括変更(解除)アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_REMOVED As String = "RemovedAction"
    'END UMANO 要望番号1369 運行紐付け時切替対応

    'START UMANO 要望番号1369 運行紐付け時切替対応
    ''' <summary>
    ''' 運行更新アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPD_UNSOLL As String = "UpdateUnsoLLData"
    'END UMANO 要望番号1369 運行紐付け時切替対応

#If True Then ' 西濃自動送り状番号出力対応 20160701 added inoue

    ''' <summary>
    ''' 自動採番送り状区分(O010)
    ''' </summary>
    ''' <remarks></remarks>
    Class AUTO_DENP_KBN

        ''' <summary>
        ''' 名鉄運輸
        ''' </summary>
        ''' <remarks></remarks>
        Public Const MEITETSU_TRANSPORT As String = "01"

        ''' <summary>
        ''' 西濃運輸千葉(標準)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SEINO_TRANSPORTATION_CHIBA As String = "02"

        ''' <summary>
        ''' トールエクスプレス(大阪)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TTOLL_EXPRESS_OSAKA As String = "03"

        ''' <summary>
        ''' トールエクスプレス(群馬)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TTOLL_EXPRESS_GUNMA As String = "04"

        ''' <summary>
        ''' 西濃運輸土気(標準)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SEINO_TRANSPORTATION_TOKE As String = "05"

        ''' <summary>
        ''' JPロジスティクス[元トールエクスプレス](千葉)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TTOLL_EXPRESS_CHIBA As String = "06"

        ''' <summary>
        ''' JPロジスティクス[元トールエクスプレス](土気)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TTOLL_EXPRESS_TOKE As String = "07"

        ''' <summary>
        ''' 西濃運輸(袖ヶ浦)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SEINO_TRANSPORTATION_SODEGAURA As String = "08"

        ''' <summary>
        ''' 西濃運輸(大阪)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SEINO_TRANSPORTATION_OSAKA As String = "09"

    End Class
#End If
    ''' <summary>
    ''' 印刷区分(S039)
    ''' </summary>
    ''' <remarks></remarks>
    Class PRINT_KB
        ''' <summary>
        ''' 梱包明細
        ''' </summary>
        ''' <remarks></remarks>
        Public Const PACKAGE_DETAILS As String = "13"

#If True Then       'ADD 2018/11/20 依頼番号 : 002743   【LMS】運行運送情報画面_一括印刷機能(千葉角田)◎玉野・大極Team◎T6_28 

        ''' <summary>
        ''' 一括印刷
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ALL_PRINT As String = "14"
#End If

    End Class


#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 運送(大)検索対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = Me.BlcAccess(ds, LMF010BLF.ACTION_ID_COUNT)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '検索結果取得
        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 排他チェック検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkHaitaData(ByVal ds As DataSet) As DataSet
        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    ''' <summary>
    ''' 運行検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectLLCountData(ByVal ds As DataSet) As DataSet
        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    ''' <summary>
    ''' コンボ用のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCombData(ByVal ds As DataSet) As DataSet
        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    ''' <summary>
    ''' 対象運行データキャンセルチェックアクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkCancelData(ByVal ds As DataSet) As DataSet
        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

#End Region

#Region "車載受注受渡し"

    '＃(2012.08.13) 要望番号：1341 車載受注渡し対応 --- STRAT ---
    ''' <summary>
    ''' 車載受注渡しデータ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSyasaiWatashiメソッドに飛ぶ</remarks>
    Private Function SyasaiWatashi(ByVal ds As DataSet) As DataSet

        'LMF820のデータセットIN情報を設定
        Dim rtnDs As DataSet = SetDataSyasaiInData(ds, New LMF820DS(), "LMF820IN")

        '検索結果取得
        Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMF820BLC(), "SyasaiWatashi", rtnDs))

        Return rtnDs

    End Function

    ''' <summary>
    ''' 車載受注渡し処理時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSyasaiInData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim max As Integer = ds.Tables("LMF010IN").Rows.Count - 1

        'LMF010IN → LMF820INへ
        For i As Integer = 0 To max
            dr.Item("NRS_BR_CD") = ds.Tables("LMF010IN").Rows(i).Item("NRS_BR_CD").ToString
            dr.Item("UNSO_NO_L") = ds.Tables("LMF010IN").Rows(i).Item("UNSO_NO_L").ToString

            dt.Rows.Add(dr)
            dr = dt.NewRow()
        Next

        Return inDs

    End Function
    '＃(2012.08.13) 要望番号：1341 車載受注渡し対応 ---  END  ---

#End Region

    '2012.06.22 要望番号1189 追加START
#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet() = Nothing
        Dim prtBlc As Com.Base.BaseBLC() = Nothing
        Dim printType As String = ds.Tables("LMF010IN").Rows(0)("PRINT_KB").ToString()

        Select Case printType

            Case "01" '仕訳表

                prtBlc = New Com.Base.BaseBLC() {New LMF560BLC()}
                setDs = New DataSet() {Me.SetDataSetLMF560InData(ds, printType)}

            Case "02" '配車表

                prtBlc = New Com.Base.BaseBLC() {New LMF580BLC()}
                setDs = New DataSet() {Me.SetDataSetLMF580InData(ds, printType)}

            Case "03" '配送一覧表

                prtBlc = New Com.Base.BaseBLC() {New LMF570BLC()}
                setDs = New DataSet() {Me.SetDataSetLMF570InData(ds, printType)}

                '2012.08.03 群馬対応 追加START
            Case "04" '配車表(群馬)

                prtBlc = New Com.Base.BaseBLC() {New LMF581BLC()}
                setDs = New DataSet() {Me.SetDataSetLMF581InData(ds, printType)}
                '2012.08.03 群馬対応 追加END

                '＃(2012.08.23)群馬対応 --- START ---
            Case "05" '運行指示書

                prtBlc = New Com.Base.BaseBLC() {New LMF620BLC()}
                setDs = New DataSet() {Me.SetDataSetLMF620InData(ds, printType)}
                '＃(2012.08.23)群馬対応 ---  END  ---

                'START-2012/11/20運賃ﾁｪｯｸﾘｽﾄ(運行基準)開始▼▼▼＊＊＊2012/11/27導入未定のため一時コメントアウト＊＊＊
            Case "06" '運賃ﾁｪｯｸﾘｽﾄ(運行基準)

                prtBlc = New Com.Base.BaseBLC() {New LMF534BLC()}
                setDs = New DataSet() {Me.SetDataSetLMF534InData(ds, printType)}
                '▲▲▲2012/11/20運賃ﾁｪｯｸﾘｽﾄ(運行基準)終了-TOEND

                '追加開始 2015.05.12 塩浜ケミカル対応
            Case "09" '送状
                prtBlc = New Com.Base.BaseBLC() {New LMC560BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType)}

            Case "10" '荷札
                prtBlc = New Com.Base.BaseBLC() {New LMC550BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC550InData(ds, printType)}
                '追加終了 2015.05.12 塩浜ケミカル対応

                '2015.09.09　納品書対応START
            Case "11" '納品書
                prtBlc = New Com.Base.BaseBLC() {New LMC500BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printType)}
                '2015.09.09　納品書対応END

                '2016.02.02　シンガポール対応START
            Case "12" 'Delivery Notes
                prtBlc = New Com.Base.BaseBLC() {New LMF670BLC()}
                setDs = New DataSet() {Me.SetDataSetLMF670InData(ds, printType)}
                '2016.02.02　シンガポール対応END

            Case PRINT_KB.PACKAGE_DETAILS
                prtBlc = New Com.Base.BaseBLC() {New LMF680BLC()}
                setDs = New DataSet() {Me.SetDataSetLMF680InData(ds, printType)}

#If True Then   'ADD 2018/11/20 依頼番号 : 002743   【LMS】運行運送情報画面_一括印刷機能(千葉角田)◎玉野・大極Team◎T6_28 
            Case PRINT_KB.ALL_PRINT
                '一括印刷
                'ADD Start　2019/06/10 005795【LMS】運送メニュー日陸便の場合、一括印刷で荷札印刷しない
                Dim nifudaFLG As String = ds.Tables("LMF010IN").Rows(0)("NIHUDA_FLAG").ToString()

                If ("01").Equals(nifudaFLG) = True Then
                    '荷札あり
                    prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC550BLC(), New LMC500BLC(), New LMF690BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType), Me.SetDataSetLMC550InData(ds, printType), Me.SetDataSetLMC500InData(ds, printType), Me.SetDataSetLMF690InData(ds, printType)}

                Else
                    prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMF690BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType), Me.SetDataSetLMC500InData(ds, printType), Me.SetDataSetLMF690InData(ds, printType)}
                End If
                'ADDEnd   　2019/06/10 005795【LMS】運送メニュー日陸便の場合、一括印刷で荷札印刷しない


#End If
#If True Then   'ADD 2021/01/07 026832   【LMS】運送保険料システム化_実装_運送保険申込書対応_運行・運送情
            Case "15" '運送保険申込書
                prtBlc = New Com.Base.BaseBLC() {New LMF690BLC()}
                setDs = New DataSet() {Me.SetDataSetLMF690InData(ds, printType)}

#End If

            Case "16" '運送チェックリスト
                prtBlc = New Com.Base.BaseBLC() {New LMF700BLC()}
                setDs = New DataSet() {Me.SetDataSetLMF700InData(ds, printType)}

            Case "17" '立合書（運送）
                prtBlc = New Com.Base.BaseBLC() {New LMF710BLC()}
                setDs = New DataSet() {Me.SetDataSetLMF710InData(ds, printType)}

            Case "99" '一括印刷


        End Select

        If prtBlc Is Nothing = True Then
            Return ds
        End If
        Dim max As Integer = prtBlc.Count - 1
        Dim rtnDs As DataSet = Nothing

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        For i As Integer = 0 To max

            If setDs Is Nothing = True Then
                Continue For
            End If

            setDs(i).Merge(New RdPrevInfoDS)

            rtnDs = MyBase.CallBLC(prtBlc(i), "DoPrint", setDs(i))

            rdPrevDt.Merge(setDs(i).Tables(LMConst.RD))

        Next

        rtnDs.Tables(LMConst.RD).Clear()
        rtnDs.Tables(LMConst.RD).Merge(rdPrevDt)

#If False Then  '運送保険料テーブル追加だったがなしになった　ADD 2021/01/07 026832   【LMS】運送保険料システム化_実装_運送保険申込書対応_運行・運送情
f
        If printType.Equals("15") Then
            '運送保険申込書のときテーブル書き込みする

            If rtnDs.Tables("LMF690OUT").Rows.Count > 0 Then

                Dim rtnResult As Boolean = False

                'トランザクション開始
                Using scope As TransactionScope = MyBase.BeginTransaction()

                    '削除追加処理
                    rtnDs = Me.SetItemData(rtnDs, "UpdatUnsoHoken")

                    'If rtnResult = True Then
                    If rtnDs.Tables("LMF690IN").Rows.Count <> 0 Then

                        'トランザクション終了
                        MyBase.CommitTransaction(scope)

                    End If

                End Using


            End If
        End If
        
#End If

        Return rtnDs

    End Function


    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetItemData(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        '更新処理
        ds = Me.Blc690Access(ds, actionId)

        Dim rtnResult As Boolean = Not MyBase.IsMessageExist()

        If rtnResult = False Then
            '更新エラーの時INをクリアで対応
            ds.Tables("LMF690IN").Clear()

        End If

        Return ds
    End Function

    'START-2012/11/20運賃ﾁｪｯｸﾘｽﾄ(運行基準)追加開始▼▼▼
    ''' <summary>
    ''' LMF534DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks>2012/11/27時点で導入未定。削除はしないでおいてください</remarks>
    Private Function SetDataSetLMF534InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMFInDataUnko(ds, New LMF534DS(), "LMF534IN", printType)

    End Function
    '▲▲▲2012/11/20運賃ﾁｪｯｸﾘｽﾄ(運行基準)追加終了-TOEND

    ''' <summary>
    ''' LMF560DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMF560InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMFInData(ds, New LMF560DS(), "LMF560IN", printType)

    End Function

    ''' <summary>620im
    ''' LMF570DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMF570InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMFInData(ds, New LMF570DS(), "LMF570IN", printType)

    End Function

    ''' <summary>
    ''' LMF580DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMF580InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMFInData(ds, New LMF580DS(), "LMF580IN", printType)

    End Function

    '2012.08.03 群馬対応 追加START
    ''' <summary>
    ''' LMF581DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMF581InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMFInData_LMF581(ds, New LMF581DS(), "LMF581IN", printType)

    End Function

    '＃(2012.08.23)群馬対応 --- START ---
    ''' <summary>
    ''' LMF581DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMF620InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMFInDataUnko(ds, New LMF620DS(), "LMF620IN", printType)

    End Function
    '＃(2012.08.23)群馬対応 --- END  ---

    '追加開始 2015.05.12

    ''' <summary>
    ''' LMC560DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    Private Function SetDataSetLMC560InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMFInDataLMC(ds, New LMC560DS(), "LMC560IN", False, printType)

    End Function

    ''' <summary>
    ''' LMC550DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    Private Function SetDataSetLMC550InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMFInDataLMC(ds, New LMC550DS(), "LMC550IN", True, printType)

    End Function

    '追加開始 2015.05.12

    '2015.09.09 納品書対応START
    ''' <summary>
    ''' LMC560DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    Private Function SetDataSetLMC500InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMFInDataLMC(ds, New LMC500DS(), "LMC500IN", False, printType)

    End Function
    '2015.09.09 納品書対応END

    '2016.02.02 シンガポール対応START
    ''' <summary>
    ''' LMF670DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    Private Function SetDataSetLMF670InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMFInData(ds, New LMF670DS(), "LMF670IN", printType)

    End Function
    '2016.02.02 シンガポール対応END

    ''' <summary>
    ''' 梱包明細用入力データ設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMF680InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMFInData(ds, New LMF680DS(), "LMF680IN", printType)

    End Function

#If True Then   'ADD 2021/01/07 026832   【LMS】運送保険料システム化_実装_運送保険申込書対応_運行・運送情
    ''' <summary>
    ''' 梱包明細用入力データ設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMF690InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMFInDataUNSOHOKEN(ds, New LMF690DS(), "LMF690IN", printType)

    End Function

#End If

    ''' <summary>
    ''' 運送チェックリスト用入力データ設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMF700InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMFInData(ds, New LMF700DS(), "LMF700IN", printType)

    End Function

    ''' <summary>
    ''' 立合書（運送）用入力データ設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMF710InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMFInData(ds, New LMF710DS(), "LMF710IN", printType)

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>

    Private Function SetDataSetLMFInData_LMF581(ByVal ds As DataSet, _
                                                ByVal inDs As DataSet, _
                                                ByVal tblNm As String, _
                                                Optional ByVal printType As String = "") As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow
        Dim setDr As DataRow
        Dim max As Integer = ds.Tables("LMF010IN").Rows.Count - 1

        For i As Integer = 0 To max

            dr = dt.NewRow()
            setDr = ds.Tables("LMF010IN").Rows(i)
            dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
            dr.Item("YUSO_BR_CD") = setDr.Item("YUSO_BR_CD").ToString()
            dr.Item("UNSO_CD") = setDr.Item("UNSO_CD").ToString()
            dr.Item("UNSO_BR_CD") = setDr.Item("UNSO_BR_CD").ToString()
            dr.Item("UNSO_NM") = setDr.Item("UNSO_NM").ToString()
            dr.Item("CUST_CD_L") = setDr.Item("CUST_CD_L").ToString()
            dr.Item("CUST_CD_M") = setDr.Item("CUST_CD_M").ToString()
            dr.Item("CUST_NM") = setDr.Item("CUST_NM").ToString()
            dr.Item("DATE_KBN") = setDr.Item("DATE_KBN").ToString()
            dr.Item("DATE_FROM") = setDr.Item("DATE_FROM").ToString()
            dr.Item("DATE_TO") = setDr.Item("DATE_TO").ToString()
            dr.Item("SYS_ENT_USER") = setDr.Item("SYS_ENT_USER").ToString()
            dr.Item("SYS_ENT_USER_NM") = setDr.Item("SYS_ENT_USER_NM").ToString()
            dr.Item("UNCO_ARI_NASHI") = setDr.Item("UNCO_ARI_NASHI").ToString()
            dr.Item("TYUKEI_HAISO_FLG") = setDr.Item("TYUKEI_HAISO_FLG").ToString()
            dr.Item("UNSO_NO_L") = setDr.Item("UNSO_NO_L").ToString()
            dr.Item("BIN_KB") = setDr.Item("BIN_KB").ToString()
            dr.Item("TARIFF_BUNRUI_KB") = setDr.Item("TARIFF_BUNRUI_KB").ToString()
            dr.Item("CUST_REF_NO") = setDr.Item("CUST_REF_NO").ToString()
            dr.Item("ORIG_NM") = setDr.Item("ORIG_NM").ToString()
            dr.Item("DEST_NM") = setDr.Item("DEST_NM").ToString()
            dr.Item("DEST_ADD") = setDr.Item("DEST_ADD").ToString()
            dr.Item("AREA_NM") = setDr.Item("AREA_NM").ToString()
            dr.Item("KANRI_NO_L") = setDr.Item("KANRI_NO_L").ToString()
            dr.Item("REMARK") = setDr.Item("REMARK").ToString()
            dr.Item("SEIQ_GROUP_NO") = setDr.Item("SEIQ_GROUP_NO").ToString()
            dr.Item("UNSO_ONDO_KB") = setDr.Item("UNSO_ONDO_KB").ToString()
            dr.Item("MOTO_DATA_KB") = setDr.Item("MOTO_DATA_KB").ToString()
            dr.Item("SYUKA_TYUKEI_NM") = setDr.Item("SYUKA_TYUKEI_NM").ToString()
            dr.Item("HAIKA_TYUKEI_NM") = setDr.Item("HAIKA_TYUKEI_NM").ToString()
            dr.Item("TRIP_NO_SYUKA") = setDr.Item("TRIP_NO_SYUKA").ToString()
            dr.Item("TRIP_NO_TYUKEI") = setDr.Item("TRIP_NO_TYUKEI").ToString()
            dr.Item("TRIP_NO_HAIKA") = setDr.Item("TRIP_NO_HAIKA").ToString()
            dr.Item("UNSOCO_SYUKA") = setDr.Item("UNSOCO_SYUKA").ToString()
            dr.Item("UNSOCO_TYUKEI") = setDr.Item("UNSOCO_TYUKEI").ToString()
            dr.Item("UNSOCO_HAIKA") = setDr.Item("UNSOCO_HAIKA").ToString()
            dr.Item("UNSOCO_CD") = setDr.Item("UNSOCO_CD").ToString()
            dr.Item("UNSOCO_BR_CD") = setDr.Item("UNSOCO_BR_CD").ToString()
            dr.Item("UNSOCO_NM") = setDr.Item("UNSOCO_NM").ToString()
            dr.Item("TRIP_NO") = setDr.Item("TRIP_NO").ToString()
            dr.Item("DRIVER_NM") = setDr.Item("DRIVER_NM").ToString()
            dr.Item("CAR_TP_KB") = setDr.Item("CAR_TP_KB").ToString()
            dr.Item("CAR_NO") = setDr.Item("CAR_NO").ToString()
            dr.Item("JSHA_KB") = setDr.Item("JSHA_KB").ToString()
            dr.Item("PRINT_KB") = setDr.Item("PRINT_KB").ToString()

            dt.Rows.Add(dr)

        Next

        Return inDs

    End Function
    '2012.08.03 群馬対応 追加END

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>

    Private Function SetDataSetLMFInData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String, _
                                         Optional ByVal printType As String = "") As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow
        Dim setDr As DataRow
        Dim max As Integer = ds.Tables("LMF010IN").Rows.Count - 1
        For i As Integer = 0 To max

            dr = dt.NewRow()
            setDr = ds.Tables("LMF010IN").Rows(i)
            dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
            dr.Item("UNSO_NO_L") = setDr.Item("UNSO_NO_L").ToString()

            dt.Rows.Add(dr)

        Next

        Return inDs

    End Function

    '＃(2012.08.23)群馬対応 --- START ---
    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMFInDataUnko(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String, _
                                             Optional ByVal printType As String = "") As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dtIn As DataTable = ds.Tables("LMF010IN")
        Dim dr As DataRow
        Dim setDr As DataRow
        Dim New_TRIP_NO As String = ""
        Dim Old_TRIP_NO As String = ""
        Dim max As Integer = ds.Tables("LMF010IN").Rows.Count - 1

        '#(1.0)DateSet内並び替え処理 -------------------------------------------------

        'ソート実行(運行番号順)
        Dim inDr As DataRow() = dtIn.Select(Nothing, "TRIP_NO ASC")

        'ソート実行後データ格納データセット作成
        dtIn = dtIn.Clone

        'ソート済みデータ格納
        For Each rowOut As DataRow In inDr
            dtIn.ImportRow(rowOut)
        Next

        '#(2.0)帳票DataSseの格納処理 ------------------------------------------------
        For i As Integer = 0 To max

            dr = dt.NewRow()

            setDr = dtIn.Rows(i)
            New_TRIP_NO = setDr.Item("TRIP_NO").ToString()

            If Old_TRIP_NO <> New_TRIP_NO Then
                '運行番号が異なれば、帳票側のDataSetに格納
                dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
                dr.Item("TRIP_NO") = setDr.Item("TRIP_NO").ToString()
                dt.Rows.Add(dr)
            End If

            Old_TRIP_NO = New_TRIP_NO

        Next

        Return inDs

    End Function
    '＃(2012.08.23)群馬対応 --- END ---


    '追加開始 2015.05.12 塩浜ケミカル対応
    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMFInDataLMC(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String,
                                         ByVal nbFlg As Boolean,
                                         Optional ByVal printType As String = "") As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow
        Dim setDr As DataRow
        Dim max As Integer = ds.Tables("LMF010IN").Rows.Count - 1
        For i As Integer = 0 To max

            dr = dt.NewRow()
            setDr = ds.Tables("LMF010IN").Rows(i)
            dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
            If tblNm.Equals("LMC500IN") = True Then
                dr.Item("KANRI_NO_L") = setDr.Item("UNSO_NO_L").ToString()
            Else
                dr.Item("OUTKA_NO_L") = setDr.Item("UNSO_NO_L").ToString()
            End If
            dr.Item("PTN_FLAG") = LMConst.FLG.ON

            Select Case nbFlg
                Case True '枚数指定
                    dr.Item("PRT_NB") = setDr.Item("UNSO_PKG_NB").ToString()
                    dr.Item("PRT_NB_FROM") = "1"
                    dr.Item("PRT_NB_TO") = setDr.Item("UNSO_PKG_NB").ToString()

                Case False '枚数指定なし

            End Select

            dt.Rows.Add(dr)

        Next

        Return inDs

    End Function

#If True Then   'ADD 2021/01/11 026832   【LMS】運送保険料システム化_実装_運送保険申込書対応_運行・運送情報
    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMFInDataUNSOHOKEN(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String,
                                                                               Optional ByVal printType As String = "") As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow
        Dim setDr As DataRow
        Dim max As Integer = ds.Tables("LMF010IN").Rows.Count - 1
        For i As Integer = 0 To max

            dr = dt.NewRow()
            setDr = ds.Tables("LMF010IN").Rows(i)
            dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
            dr.Item("UNSO_NO_L") = setDr.Item("UNSO_NO_L").ToString()
            dr.Item("KANRI_NO_L") = setDr.Item("KANRI_NO_L").ToString()
            dr.Item("MOTO_DATA_KB") = setDr.Item("MOTO_DATA_KB").ToString()

            dt.Rows.Add(dr)

        Next

        Return inDs

    End Function
#End If

    '追加終了 2015.05.12 塩浜ケミカル対応

    '2014.07.09 追加START
    ''' <summary>
    ''' データ検索処理(名鉄CSV作成時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectMeitetuCsvメソッドに飛ぶ</remarks>
    Private Function SelectMeitetuCsv(ByVal ds As DataSet) As DataSet

        'LMF860のデータセットIN情報を設定
        Dim rtnDs As DataSet = SetDataSetCsvInData(ds, New LMF860DS(), "LMF860IN")

        '検索結果取得
        Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMF860BLC(), "MeitetuCsv", rtnDs))

        Return rtnDs

    End Function
    '2014.07.09 追加END

    '2014.07.01 追加START
    ''' <summary>
    ''' データ検索処理(岡山貨物CSV作成時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectOkakenCsvメソッドに飛ぶ</remarks>
    Private Function SelectOkakenCsv(ByVal ds As DataSet) As DataSet

        'LMF850のデータセットIN情報を設定
        Dim rtnDs As DataSet = SetDataSetCsvInData(ds, New LMF850DS(), "LMF850IN")

        '検索結果取得
        Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMF850BLC(), "OkakenCsv", rtnDs))

        Return rtnDs

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetCsvInData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim max As Integer = ds.Tables("LMF010IN_CSV").Rows.Count - 1

        For i As Integer = 0 To max
            dr.Item("NRS_BR_CD") = ds.Tables("LMF010IN_CSV").Rows(i).Item("NRS_BR_CD").ToString
            dr.Item("UNSO_NO_L") = ds.Tables("LMF010IN_CSV").Rows(i).Item("UNSO_NO_L").ToString
            dr.Item("ROW_NO") = ds.Tables("LMF010IN_CSV").Rows(i).Item("ROW_NO").ToString
            dr.Item("FILEPATH") = ds.Tables("LMF010IN_CSV").Rows(i).Item("FILEPATH").ToString
            dr.Item("FILENAME") = ds.Tables("LMF010IN_CSV").Rows(i).Item("FILENAME").ToString
            dr.Item("SYS_DATE") = ds.Tables("LMF010IN_CSV").Rows(i).Item("SYS_DATE").ToString
            dr.Item("SYS_TIME") = ds.Tables("LMF010IN_CSV").Rows(i).Item("SYS_TIME").ToString

            dt.Rows.Add(dr)
            dr = dt.NewRow()
        Next

        Return inDs

    End Function

    '2014.07.01 追加END


#Region "名鉄固有処理 ADD 2017/02/28"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintMeitetuReport(ByVal ds As DataSet) As DataSet
        Return Me.PrintMeitetuReport(ds, False)
    End Function

    ''' <summary>
    ''' 名鉄帳票印刷(荷札+送状:UpdatePrintData亜種)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintMeitetuReport(ByVal ds As DataSet _
                                      , ByVal isGrouping As Boolean) As DataSet

        Dim updateData As DataSet = ds.Copy()

        ' 送り状用パラメータ設定
        Dim inputDS As DataSet = SetDataSetPrintInData(ds, New LMC794DS(), "LMC794IN")

        ' 検索結果取得
        Dim selectData As DataSet = Nothing

        ' まとめなし
        selectData = MyBase.CallBLC(New LMC794BLC(), "MeitetuLabelUnso", inputDS)

        If (MyBase.IsMessageStoreExist() = True OrElse _
            MyBase.IsMessageExist() = True) Then
            Return ds
        End If

        Dim invoicePrintInData As DataSet = selectData.Clone()
        Dim tagPrintInData As DataSet = Nothing

        '更新用データ取得
        'updateData.Merge(selectData.Tables("LMC794IN_UPDATE_UNSO_L"))

        ' プレビューデータ格納準備
        ds.Merge(New RdPrevInfoDS)
        Dim prevTempDs As New DataSet()
        prevTempDs.Merge(New RdPrevInfoDS)


        Dim keyNumber As String = String.Empty
        For Each selectRow As DataRow In selectData.Tables("LMC794OUT").Rows
            '運送からは更新なし
            ''If (isGrouping) Then
            ''    keyNumber = selectRow("AUTO_DENP_NO").ToString()
            ''Else
            ''    keyNumber = selectRow("OUTKA_NO_L").ToString()
            ''End If

            ''Me.UpdatePrintDataMeitetu(updateData, keyNumber, isGrouping)
            ''If (MyBase.IsMessageStoreExist() = True OrElse _
            ''    MyBase.IsMessageExist() = True) Then
            ''    Return ds
            ''End If

            ' 送状用入力データ
            invoicePrintInData.Clear()
            invoicePrintInData.Tables("LMC794OUT").ImportRow(selectRow)

            ' 荷札用入力データ
            tagPrintInData = Me.SetDataSetLMC789InData(invoicePrintInData, "LMC794OUT")


            ' 送状印刷
            invoicePrintInData = MyBase.CallBLC(New LMC794BLC(), "DoPrintUnso", invoicePrintInData)
            prevTempDs.Tables(LMConst.RD).Merge(invoicePrintInData.Tables(LMConst.RD))

            ' 荷札印刷
            tagPrintInData = MyBase.CallBLC(New LMC789BLC(), "DoPrintUnso", tagPrintInData)
            prevTempDs.Tables(LMConst.RD).Merge(tagPrintInData.Tables(LMConst.RD))

        Next

        ' プレビューデータ格納
        ds.Tables(LMConst.RD).Merge(prevTempDs.Tables(LMConst.RD))

        Return ds

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetPrintInData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()

        Dim TABLE_NM_IN_UPDATE As String = "LMF010IN_UPDATE"

        For i As Integer = 0 To ds.Tables(TABLE_NM_IN_UPDATE).Rows.Count - 1

            dr.Item("NRS_BR_CD") = ds.Tables(TABLE_NM_IN_UPDATE).Rows(i).Item("NRS_BR_CD").ToString
            dr.Item("OUTKA_NO_L") = ds.Tables(TABLE_NM_IN_UPDATE).Rows(i).Item("OUTKA_NO_L").ToString
            dr.Item("ROW_NO") = ds.Tables(TABLE_NM_IN_UPDATE).Rows(i).Item("RECORD_NO").ToString
            dr.Item("SYS_DATE") = ds.Tables(TABLE_NM_IN_UPDATE).Rows(i).Item("SYS_UPD_DATE").ToString
            dr.Item("SYS_TIME") = ds.Tables(TABLE_NM_IN_UPDATE).Rows(i).Item("SYS_UPD_TIME").ToString
            dr.Item("UNSO_SYS_UPD_DATE") = ds.Tables(TABLE_NM_IN_UPDATE).Rows(i).Item("UNSO_SYS_UPD_DATE").ToString
            dr.Item("UNSO_SYS_UPD_TIME") = ds.Tables(TABLE_NM_IN_UPDATE).Rows(i).Item("UNSO_SYS_UPD_TIME").ToString

            dt.Rows.Add(dr)
            dr = dt.NewRow()
        Next

        Return inDs

    End Function


    ''' <summary>
    ''' 荷札印刷用入力データ設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC789InData(ByVal ds As DataSet, ByVal tblNm As String) As DataSet

        Dim tagPrintInData As New LMC789DS()
        Dim tagRow As LMC789DS.LMC789OUTRow = Nothing

        Dim inTable As DataTable = ds.Tables(tblNm)
        For i As Int32 = 0 To inTable.Rows.Count - 1

            tagRow = tagPrintInData.LMC789OUT.NewLMC789OUTRow()
            tagRow.ARR_PLAN_DATE = inTable.Rows(i).Item("ARR_PLAN_DATE").ToString()
            tagRow.ARR_PLAN_TIME = inTable.Rows(i).Item("ARR_PLAN_TIME").ToString()
            tagRow.AUTO_DENP_NO = inTable.Rows(i).Item("AUTO_DENP_NO").ToString()
            tagRow.CHAKU_CD = ""
            tagRow.CUST_CD_L = inTable.Rows(i).Item("CUST_CD_L").ToString()
            tagRow.CUST_NM_L = inTable.Rows(i).Item("CUST_NM_L").ToString()
            tagRow.CYAKU_NM = ""
            tagRow.DENPYO_NM = inTable.Rows(i).Item("DENPYO_NM").ToString()
            tagRow.DENPYO_NO = inTable.Rows(i).Item("DENPYO_NO").ToString()
            tagRow.HAITATSU_KBN = inTable.Rows(i).Item("HAITATSU_KBN").ToString()
            tagRow.HAITATSU_TIME_KBN = inTable.Rows(i).Item("HAITATSU_TIME_KBN").ToString()
            tagRow.HINSYU_KEIYAKU = ""
            tagRow.HOKENRYOU = "0"
            tagRow.JIS = inTable.Rows(i).Item("JIS").ToString()
            tagRow.JYURYO = inTable.Rows(i).Item("JYURYO").ToString()
            tagRow.KIJI_1 = inTable.Rows(i).Item("KIJI_1").ToString()
            tagRow.KIJI_2 = inTable.Rows(i).Item("KIJI_2").ToString()
            tagRow.KIJI_3 = inTable.Rows(i).Item("KIJI_3").ToString()
            tagRow.KIJI_4 = inTable.Rows(i).Item("KIJI_4").ToString()
            tagRow.KIJI_5 = inTable.Rows(i).Item("KIJI_5").ToString()
            tagRow.KIJI_6 = inTable.Rows(i).Item("KIJI_6").ToString()
            tagRow.KIJI_7 = inTable.Rows(i).Item("KIJI_7").ToString()
            tagRow.KIJI_8 = inTable.Rows(i).Item("KIJI_8").ToString()
            tagRow.KOSU = inTable.Rows(i).Item("KOSU").ToString()
            tagRow.NIOKURININ_ADD1 = inTable.Rows(i).Item("NIOKURININ_ADD1").ToString()
            tagRow.NIOKURININ_ADD2 = inTable.Rows(i).Item("NIOKURININ_ADD2").ToString()
            tagRow.NIOKURININ_ADD3 = inTable.Rows(i).Item("NIOKURININ_ADD3").ToString()
            tagRow.NIOKURININ_CD = inTable.Rows(i).Item("NIOKURININ_CD").ToString()
            tagRow.NIOKURININ_MEI1 = inTable.Rows(i).Item("NIOKURININ_MEI1").ToString()
            tagRow.NIOKURININ_MEI2 = inTable.Rows(i).Item("NIOKURININ_MEI2").ToString()
            tagRow.NIOKURININ_TEL = inTable.Rows(i).Item("NIOKURININ_TEL").ToString()
            tagRow.NIOKURININ_ZIP = inTable.Rows(i).Item("NIOKURININ_ZIP").ToString()
            tagRow.NIUKENIN_ADD1 = inTable.Rows(i).Item("NIUKENIN_ADD1").ToString()
            tagRow.NIUKENIN_ADD2 = inTable.Rows(i).Item("NIUKENIN_ADD2").ToString()
            tagRow.NIUKENIN_ADD3 = inTable.Rows(i).Item("NIUKENIN_ADD3").ToString()
            tagRow.NIUKENIN_CD = inTable.Rows(i).Item("NIUKENIN_CD").ToString()
            tagRow.NIUKENIN_MEI1 = inTable.Rows(i).Item("NIUKENIN_MEI1").ToString()
            tagRow.NIUKENIN_MEI2 = inTable.Rows(i).Item("NIUKENIN_MEI2").ToString()
            tagRow.NIUKENIN_TEL = inTable.Rows(i).Item("NIUKENIN_TEL").ToString()
            tagRow.NIUKENIN_ZIP = inTable.Rows(i).Item("NIUKENIN_ZIP").ToString()
            tagRow.NRS_BR_CD = inTable.Rows(i).Item("NRS_BR_CD").ToString()
            tagRow.OUTKA_NO_L = inTable.Rows(i).Item("OUTKA_NO_L").ToString()
            tagRow.OUTKA_NO_M = ""
            tagRow.OUTKA_PLAN_DATE = inTable.Rows(i).Item("OUTKA_PLAN_DATE").ToString()
            tagRow.PAGES = ""
            tagRow.PAGES_2 = ""
            tagRow.PARETTOSU = "0"
            tagRow.ROW_NO = inTable.Rows(i).Item("ROW_NO").ToString()
            tagRow.RPT_ID = ""
            tagRow.SHIHARAININ_CD = inTable.Rows(i).Item("SHIHARAININ_CD").ToString()
            tagRow.SHIP_NM_L = inTable.Rows(i).Item("SHIP_NM_L").ToString()
            tagRow.SIWAKE_NO = ""
            tagRow.SYS_DATE = ""
            tagRow.SYS_TIME = ""
            tagRow.SYS_UPD_DATE = inTable.Rows(i).Item("SYS_UPD_DATE").ToString()
            tagRow.SYS_UPD_TIME = inTable.Rows(i).Item("SYS_UPD_TIME").ToString()
            tagRow.TEL = inTable.Rows(i).Item("TEL").ToString()
            tagRow.UNCHIN_SEIQTO_CD = inTable.Rows(i).Item("UNCHIN_SEIQTO_CD").ToString()
            tagRow.UNSOCO_BR_NM = inTable.Rows(i).Item("UNSOCO_BR_NM").ToString()
            tagRow.YOSEKI = inTable.Rows(i).Item("YOSEKI").ToString()
            tagRow.ZIP_PATTERN = inTable.Rows(i).Item("ZIP_PATTERN").ToString()

            tagPrintInData.LMC789OUT.AddLMC789OUTRow(tagRow)
        Next

        Return tagPrintInData

    End Function
#End Region




#End Region
    '2012.06.22 要望番号1189 追加END

#Region "設定処理"

    ''' <summary>
    ''' 登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveAction(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, LMF010BLF.ACTION_ID_COUNT_LL)

        'START UMANO 要望番号1369 運行に紐づいていないデータは支払バッチ(LMF810)を起動
        rtnResult = rtnResult AndAlso Me.UpdateAction(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)
        'END UMANO 要望番号1369 運行に紐づいていないデータは支払バッチ(LMF810)を起動

        rtnResult = rtnResult AndAlso Me.UpdateUnsoLLData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 解除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function RemovedAction(ByVal ds As DataSet) As DataSet

        Call Me.UpdateAction(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Return ds

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionStr">メソッド名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ScopeStartEnd(ByVal ds As DataSet, ByVal actionStr As String) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'BLCアクセス
            ds = Me.BlcAccess(ds, actionStr)

            'タブレット側運送テーブル更新
            'イベント種別が「登録」で修正項目が運送会社の場合
            If (ds.Tables(LMF010BLF.TABLE_NM_ITEM).Rows(0).Item("ITEM_DATA").Equals(LMF010BLF.SHUSEI_UNSOCO) = True AndAlso _
                actionStr.Equals(LMF010BLF.ACTION_ID_SAVE) = True) Then
                ds = Me.UpdateTabletUnsoData(ds)
            End If

            'エラーがない場合、コミット
            If MyBase.IsMessageStoreExist(Convert.ToInt32(ds.Tables(LMF010BLF.TABLE_NM_UNSO_L).Rows(0).Item("ROW_NO").ToString())) = False Then

                'START UMANO 要望番号1302 支払運賃に伴う修正。
                '運行更新の場合２回通るので処理を行わない
                If actionStr.Equals(LMF010BLF.ACTION_ID_UPD_UNSOLL) = False Then

                    'If Me.SetShiharaiData(ds, updateUnchin) = True Then
                    If Me.SetShiharaiData(ds, True) = True Then

                        'トランザクション終了
                        MyBase.CommitTransaction(scope)
                    End If

                End If
                'END UMANO 要望番号1302 支払運賃に伴う修正。

                'START KIM 要望番号1485 支払い関連修正。

                'イベント種別が「登録」で修正項目が「運行番号」の場合
                If (ds.Tables(LMF010BLF.TABLE_NM_ITEM).Rows(0).Item("ITEM_DATA").Equals(LMF010BLF.SHUSEI_TRIP) = True AndAlso _
                    actionStr.Equals(LMF010BLF.ACTION_ID_SAVE) = True) Then
                    '支払運賃テーブル（F_SHIHARAI_TRS）更新
                    ds = Me.BlcAccess(ds, "UpdateShiharaiData")
                End If

                'END KIM 要望番号1485 支払い関連修正。


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
    ''' BLCクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Blc690Access(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallBLC(Me._Blc690, actionId, ds)

    End Function
    ''' <summary>
    ''' BLCでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudgeStore(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.BlcAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageStoreExist(Convert.ToInt32(ds.Tables(LMF010BLF.TABLE_NM_UNSO_L).Rows(0).Item("ROW_NO").ToString()))

    End Function

    ''' <summary>
    ''' BLCでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.BlcAccess(ds, actionId)

        '(2012.09.06)START UMANO 要望番号1410 
        ''エラーがあるかを判定
        'Return Not MyBase.IsMessageExist()

        'エラーEXCELがあるかを判定
        Return Not MyBase.IsMessageStoreExist()
        ''(2012.09.06)END UMANO 要望番号1410 

    End Function

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function UpdateAction(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        Dim upDs As DataSet = ds.Copy()
        Dim upDt As DataTable = upDs.Tables(LMF010BLF.TABLE_NM_UNSO_L)
        Dim setDt As DataTable = upDs.Tables(LMF010BLF.TABLE_NM_ERR)
        Dim dt As DataTable = ds.Tables(LMF010BLF.TABLE_NM_UNSO_L)
        Dim max As Integer = dt.Rows.Count - 1

        'START UMANO 要望番号1369 運行紐付け対応
        '①イベント種別が「登録」で修正項目が「運送会社」の場合
        If (ds.Tables(LMF010BLF.TABLE_NM_ITEM).Rows(0).Item("ITEM_DATA").Equals(LMF010BLF.SHUSEI_UNSOCO) = True AndAlso _
            actionId.Equals(LMF010BLF.ACTION_ID_SAVE) = True) Then
            '画面で入力された運送会社コードよりタリフを取得
            upDs = Me.UnsoLShiharaiEdit(upDs)

            '②イベント種別が「登録」で修正項目が「運行番号」の場合
        ElseIf (ds.Tables(LMF010BLF.TABLE_NM_ITEM).Rows(0).Item("ITEM_DATA").Equals(LMF010BLF.SHUSEI_TRIP) = True AndAlso _
            actionId.Equals(LMF010BLF.ACTION_ID_SAVE) = True) Then

            '運行データの支払タリフコードを取得する
            upDs = Me.SelectUncodataTariff(upDs)
        End If
        'END UMANO 要望番号1369 運行紐付け対応

        For i As Integer = 0 To max

            '値を初期化
            upDt.Clear()
            setDt.Clear()

            '更新する行を設定
            upDt.ImportRow(dt.Rows(i))

            '最終請求日のチェック
            If Me.ChkSeiqDate(ds, upDs, setDt) = False Then
                Continue For
            End If

            'START UMANO 要望番号1369 運行紐付け対応
            '②イベント種別が「解除」で修正項目が「運行番号」の場合
            If (ds.Tables(LMF010BLF.TABLE_NM_ITEM).Rows(0).Item("ITEM_DATA").Equals(LMF010BLF.SHUSEI_TRIP) = True AndAlso _
                actionId.Equals(LMF010BLF.ACTION_ID_REMOVED) = True) Then
                '一覧の選択行の運送会社コード(1次)よりタリフを取得
                upDs = Me.UnsoLShiharaiEdit(upDs)
            End If
            'END UMANO 要望番号1369 運行紐付け対応

            'START UMANO 要望番号1369 支払運賃に伴う修正。
            If upDs.Tables(LMF010BLF.TABLE_NM_UNSOCO).Rows.Count > 0 Then
                If String.IsNullOrEmpty(upDs.Tables(LMF010BLF.TABLE_NM_UNSOCO).Rows(0).Item("UNCHIN_TARIFF_CD").ToString()) = False OrElse _
                   String.IsNullOrEmpty(upDs.Tables(LMF010BLF.TABLE_NM_UNSOCO).Rows(0).Item("EXTC_TARIFF_CD").ToString()) = False Then
                    upDt.Rows(0).Item("SHIHARAI_TARIFF_CD") = upDs.Tables(LMF010BLF.TABLE_NM_UNSOCO).Rows(0).Item("UNCHIN_TARIFF_CD").ToString()
                    upDt.Rows(0).Item("SHIHARAI_ETARIFF_CD") = upDs.Tables(LMF010BLF.TABLE_NM_UNSOCO).Rows(0).Item("EXTC_TARIFF_CD").ToString()
                End If

            End If
            'END UMANO 要望番号1369 支払運賃に伴う修正。

#If True Then ' 西濃自動送り状番号対応 20160705 added inoue
            If (ds.Tables(LMF010BLF.TABLE_NM_ITEM).Rows(0).Item("ITEM_DATA").Equals(LMF010BLF.SHUSEI_UNSOCO) = True AndAlso _
                actionId.Equals(LMF010BLF.ACTION_ID_SAVE) = True) Then

                ' 運送会社一括変更の場合に実施する

                Dim unsoLRow As DataRow = upDt.Rows(0)

                ' AUTO_DENP_KBNが変更された場合、呼び出し元でautoDenpNoはクリアされる。
                ' AUTO_DENP_KBN変更時、変更前のautoDenpNoが設定されていることは考慮しない。
                If (String.IsNullOrEmpty(unsoLRow.Item("AUTO_DENP_NO").ToString())) Then

                    Dim autoDenpKbn As String = TryCast(unsoLRow.Item("AUTO_DENP_KBN"), String)

                    unsoLRow.Item("AUTO_DENP_NO") = Me.GetAutoDenpNo(autoDenpKbn, ds)
                End If
            End If
#End If
            '更新処理
            upDs = Me.ScopeStartEnd(upDs, actionId)

            'エラー行のマージ
            ds = Me.ErrDataMerge(ds, setDt)

        Next

        Return True

    End Function

    ''' <summary>
    ''' 最終請求日のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="setDs">DataSet</param>
    ''' <param name="setDt">DataTable</param>
    ''' <returns>最終請求日</returns>
    ''' <remarks></remarks>
    Private Function ChkSeiqDate(ByVal ds As DataSet, ByVal setDs As DataSet, ByVal setDt As DataTable) As Boolean

        If Me.ServerChkJudgeStore(setDs, System.Reflection.MethodBase.GetCurrentMethod.Name) = False Then

            'エラー行のマージ
            ds = Me.ErrDataMerge(ds, setDt)

            Return False

        End If

        Return True

    End Function

    'START UMANO 要望番号1369 運行紐付け対応
    ''' <summary>
    ''' 運送会社修正に伴う支払運賃タリフの変更
    ''' </summary>
    ''' <param name="setDs">DataSet</param>
    ''' <returns>DATASET</returns>
    ''' <remarks></remarks>
    Private Function UnsoLShiharaiEdit(ByVal setDs As DataSet) As DataSet

        setDs = Me.BlcAccess(setDs, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Return setDs

    End Function
    'END UMANO 要望番号1369 運行紐付け対応

    'START UMANO 要望番号1369 運行紐付け対応
    ''' <summary>
    ''' 画面で入力された運行番号を元に支払運賃タリフコードを取得
    ''' </summary>
    ''' <param name="setDs">DataSet</param>
    ''' <returns>DATASET</returns>
    ''' <remarks></remarks>
    Private Function SelectUncodataTariff(ByVal setDs As DataSet) As DataSet

        setDs = Me.BlcAccess(setDs, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Return setDs

    End Function
    'END UMANO 要望番号1369 運行紐付け対応

    ''' <summary>
    ''' エラー行のマージ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="setDt">DataTable</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ErrDataMerge(ByVal ds As DataSet, ByVal setDt As DataTable) As DataSet

        Dim errDt As DataTable = ds.Tables(LMF010BLF.TABLE_NM_ERR)
        Dim max As Integer = setDt.Rows.Count - 1
        For i As Integer = 0 To max
            errDt.ImportRow(setDt.Rows(i))
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 運行レコード更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateUnsoLLData(ByVal ds As DataSet) As Boolean

        '更新項目が運行番号以外、スルー
        If LMF010BLF.SHUSEI_TRIP.Equals(ds.Tables(LMF010BLF.TABLE_NM_ITEM).Rows(0).Item("ITEM_DATA").ToString()) = False Then
            Return True
        End If

        'エラー行と更新する行数が同じ場合、スルー(1件も成功していない)
        If ds.Tables(LMF010BLF.TABLE_NM_ERR).Rows.Count = ds.Tables(LMF010BLF.TABLE_NM_UNSO_L).Rows.Count Then
            Return True
        End If

        Call Me.ScopeStartEnd(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)
        Return True

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' (支払)運賃情報の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="updateShiharai">運賃更新フラグ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetShiharaiData(ByVal ds As DataSet, ByVal updateShiharai As Boolean) As Boolean

        '運賃情報を更新しない場合、スルー
        If updateShiharai = False Then
            Return True
        End If

        Dim blc As LMF810BLC = New LMF810BLC()
        Dim inTbl As DataTable = ds.Tables(LMF010BLF.TABLE_NM_810IN)
        'inTbl.ImportRow(ds.Tables(LMF010BLF.TABLE_NM_UNSO_L).Rows(0))

        '運送会社が指定されている場合、支払バッチを呼び出す
        'データセット設定
        ds = Me.SetShiharaiInDataSet(ds)

        '計算処理
        ds = MyBase.CallBLC(blc, LMF010BLF.ACTION_ID_CALC_DATA, ds)

        '戻り値判定
        Dim rtnDr As DataRow = ds.Tables(LMF010BLF.TABLE_NM_RTN2_TBL).Rows(0)
        Select Case rtnDr.Item("STATUS").ToString()

            Case LMF010BLF.RTN_KBN_NORMAL, LMF010BLF.RTN_KBN_WARNING, LMF010BLF.RTN_KBN_ZEROYEN
            Case Else
                MyBase.SetMessage(rtnDr.Item("ERROR_CD").ToString())
                Return False

        End Select

        '更新処理
        ds = Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Return Not MyBase.IsMessageExist()

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START UMANO 要望番号1302 支払運賃に伴う修正。支払バッチ呼び出し
    ''' <summary>
    ''' 支払運賃INの値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetShiharaiInDataSet(ByVal ds As DataSet) As DataSet

        Dim unchinDs As DataSet = New LMF810DS()
        Dim insRows As DataRow = unchinDs.Tables("LMF810IN").NewRow

        insRows.Item("NRS_BR_CD") = ds.Tables(LMF010BLF.TABLE_NM_UNSO_L).Rows(0).Item("NRS_BR_CD").ToString
        insRows.Item("UNSO_NO_L") = ds.Tables(LMF010BLF.TABLE_NM_UNSO_L).Rows(0).Item("UNSO_NO_L").ToString
        insRows.Item("WH_CD") = ds.Tables(LMF010BLF.TABLE_NM_UNSO_L).Rows(0).Item("WH_CD").ToString         'ADD 2019/08/05 005193

        'データセットに追加
        unchinDs.Tables("LMF810IN").Rows.Add(insRows)

        Return unchinDs

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。支払バッチ呼び出し


#If True Then ' 西濃自動送り状番号出力対応 20160701 added inoue


    ''' <summary>
    ''' MEITETSU_DENP_NOを取得
    ''' </summary>
    ''' <returns>MEITETSU_DENP_NO</returns>
    ''' <remarks></remarks>
    Private Function GetMeiTetsuDenpNoL() As String

        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
            GetAutoCode(NumberMasterUtility.NumberKbn.MEITETSU_DENP_NO, Me)


        If (String.IsNullOrEmpty(newCode) = False) Then

            'チェックデジットの組込み
            Dim checkDigit As Integer = Convert.ToInt32(newCode) Mod 7
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo

    End Function

    ''' <summary>
    ''' TOLL_DENP_NOを取得(大阪トール)
    ''' </summary>
    ''' <returns>TOLL_DENP_NO</returns>
    ''' <remarks></remarks>
    Private Function GetTollDenpNoL(ByVal ds As DataSet) As String


        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
        GetAutoCode(NumberMasterUtility.NumberKbn.TOLL_DENP_NO, Me)

        Dim dt As DataTable = ds.Tables("LMF010_OKURIJYO_WK")
        Dim okurijyoHed As String = String.Empty

        If dt.Rows.Count > 0 Then
            okurijyoHed = dt.Rows(0).Item("OKURIJYO_HEAD").ToString
        End If

        If (String.IsNullOrEmpty(newCode) = False) Then

            Dim checkDigit As Integer

            'チェックデジットの組込み

            If String.Empty.Equals(okurijyoHed) = False Then
                '区分マスタより取得できた時
                newCode = String.Concat(okurijyoHed, newCode)
            End If

            checkDigit = CInt(Convert.ToInt64(newCode) Mod 7)
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo


    End Function

    ''' <summary>
    ''' TOLL_DENP_NOを取得(群馬トール)
    ''' </summary>
    ''' <returns>TOLL_DENP_NO</returns>
    ''' <remarks></remarks>
    Private Function GetGunmaTollDenpNoL(ByVal ds As DataSet) As String


        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
        GetAutoCode(NumberMasterUtility.NumberKbn.TOLL_DENP_NO_CYD, Me)

        Dim dt As DataTable = ds.Tables("LMF010_OKURIJYO_WK")
        Dim okurijyoHed As String = String.Empty

        If dt.Rows.Count > 0 Then
            okurijyoHed = dt.Rows(0).Item("OKURIJYO_HEAD").ToString
        End If

        If (String.IsNullOrEmpty(newCode) = False) Then

            Dim checkDigit As Integer

            'チェックデジットの組込み

            If String.Empty.Equals(okurijyoHed) = False Then
                '区分マスタより取得できた時
                newCode = String.Concat(okurijyoHed, newCode)
            End If

            checkDigit = CInt(Convert.ToInt64(newCode) Mod 7)
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo


    End Function

    ''' <summary>
    ''' 送り状番号生成(千葉JPロジスティクス[元トールエクスプレス])
    ''' </summary>
    ''' <returns>送り状番号</returns>
    ''' <remarks></remarks>
    Private Function GetChibaTollDenpNoL(ByVal ds As DataSet) As String


        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
        GetAutoCode(NumberMasterUtility.NumberKbn.JPLOGI_DENP_NO_CHI, Me)

        Dim dt As DataTable = ds.Tables("LMF010_OKURIJYO_WK")
        Dim okurijyoHed As String = String.Empty

        If dt.Rows.Count > 0 Then
            okurijyoHed = dt.Rows(0).Item("OKURIJYO_HEAD").ToString
        End If

        If (String.IsNullOrEmpty(newCode) = False) Then

            Dim checkDigit As Integer

            'チェックデジットの組込み

            If String.Empty.Equals(okurijyoHed) = False Then
                '区分マスタより取得できた時
                newCode = String.Concat(okurijyoHed, newCode)
            End If

            checkDigit = CInt(Convert.ToInt64(newCode) Mod 7)
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo


    End Function

    ''' <summary>
    ''' 送り状番号生成(土気JPロジスティクス[元トールエクスプレス])
    ''' </summary>
    ''' <returns>送り状番号</returns>
    ''' <remarks></remarks>
    Private Function GetTokeTollDenpNoL(ByVal ds As DataSet) As String


        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
        GetAutoCode(NumberMasterUtility.NumberKbn.JPLOGI_DENP_NO_TOK, Me)

        Dim dt As DataTable = ds.Tables("LMF010_OKURIJYO_WK")
        Dim okurijyoHed As String = String.Empty

        If dt.Rows.Count > 0 Then
            okurijyoHed = dt.Rows(0).Item("OKURIJYO_HEAD").ToString
        End If

        If (String.IsNullOrEmpty(newCode) = False) Then

            Dim checkDigit As Integer

            'チェックデジットの組込み

            If String.Empty.Equals(okurijyoHed) = False Then
                '区分マスタより取得できた時
                newCode = String.Concat(okurijyoHed, newCode)
            End If

            checkDigit = CInt(Convert.ToInt64(newCode) Mod 7)
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo


    End Function

    ''' <summary>
    ''' 自動採番送り状番号取得
    ''' </summary>
    ''' <param name="autoDenpKbn">自動送り状番号区分</param>
    ''' <returns>自動送り状番号</returns>
    ''' <remarks>
    ''' LMC010BLC,LMC020BLC,LMF020BLC,LMF010BLFに同メソッドあり
    ''' ToDo:共通ライブラリ化を検討
    ''' </remarks>
    Private Function GetAutoDenpNo(ByVal autoDenpKbn As String, ByVal ds As DataSet) As String


        Dim autoDenpNo As String = String.Empty

        If (String.IsNullOrEmpty(autoDenpKbn)) Then
            ' 自動送り状番号払い出し対象外
            Return autoDenpNo
        End If

        Dim nubmerKbn As NumberMasterUtility.NumberKbn = Nothing

        Select Case autoDenpKbn
            Case AUTO_DENP_KBN.MEITETSU_TRANSPORT
                ' 名鉄運輸送り状番号生成
                autoDenpNo = Me.GetMeiTetsuDenpNoL()

            Case AUTO_DENP_KBN.SEINO_TRANSPORTATION_CHIBA
                ' 西濃運輸(千葉)送り状番号生成
                nubmerKbn = NumberMasterUtility.NumberKbn.AUTO_DENP_NO_SEINO_CHIBA
                autoDenpNo = New NumberMasterUtility().GetAutoCode(nubmerKbn, Me)

            Case AUTO_DENP_KBN.TTOLL_EXPRESS_OSAKA
                ' トールエクスプレス送り状番号生成                
                autoDenpNo = Me.GetTollDenpNoL(ds)

            Case AUTO_DENP_KBN.TTOLL_EXPRESS_GUNMA
                ' トールエクスプレス送り状番号生成                
                autoDenpNo = Me.GetGunmaTollDenpNoL(ds)

            Case AUTO_DENP_KBN.SEINO_TRANSPORTATION_TOKE
                ' 西濃運輸(土気)送り状番号生成
                nubmerKbn = NumberMasterUtility.NumberKbn.AUTO_DENP_NO_SEINO_TOKE
                autoDenpNo = New NumberMasterUtility().GetAutoCode(nubmerKbn, Me)

            Case AUTO_DENP_KBN.TTOLL_EXPRESS_CHIBA
                'JPロジスティクス[元トールエクスプレス](千葉) 送り状番号生成
                autoDenpNo = Me.GetChibaTollDenpNoL(ds)

            Case AUTO_DENP_KBN.TTOLL_EXPRESS_TOKE
                'JPロジスティクス[元トールエクスプレス](土気) 送り状番号生成
                autoDenpNo = Me.GetTokeTollDenpNoL(ds)

            Case AUTO_DENP_KBN.SEINO_TRANSPORTATION_SODEGAURA
                ' 西濃運輸(袖ヶ浦)送り状番号生成
                nubmerKbn = NumberMasterUtility.NumberKbn.AUTO_DENP_NO_SEINO_SODEGAURA
                autoDenpNo = New NumberMasterUtility().GetAutoCode(nubmerKbn, Me)

            Case AUTO_DENP_KBN.SEINO_TRANSPORTATION_OSAKA
                ' 西濃運輸(大阪)送り状番号生成
                nubmerKbn = NumberMasterUtility.NumberKbn.AUTO_DENP_NO_SEINO_OSAKA
                autoDenpNo = New NumberMasterUtility().GetAutoCode(nubmerKbn, Me)

            Case Else
                Throw New InvalidOperationException(String.Format("定義されていないAUTO_DENP_KBN=[{0}]が指定されました。", autoDenpKbn))

        End Select


        Return autoDenpNo


    End Function

    'ADD 2017/02/27 Start

    Private Function UpdatePrintDataMeitetu(ByVal ds As DataSet _
                                      , ByVal keyNumber As String _
                                      , ByVal isGrouping As Boolean) As DataSet


        '更新用入力データテーブル名
        'Dim tableNm As String = "LMC010IN_UPDATE"
        Dim outKaInTableName As String = "LMC010IN_OUTKA_L"
        Dim unsoInTableName As String = "LMC794IN_UPDATE_UNSO_L"

        Dim inputUnsoTable As DataTable = ds.Tables(unsoInTableName)
        Dim inputOutkaTable As DataTable = ds.Tables(outKaInTableName)

        Dim updateData As DataSet = ds.Clone()
        Dim updateUnsoInTable As DataTable = updateData.Tables(unsoInTableName)
        Dim updateOutkaInTable As DataTable = updateData.Tables(outKaInTableName)

        ' 更新対象の出荷管理番号
        Dim outKaNoLList As New List(Of Object)()

        'メッセージクリア
        MyBase.SetMessage(Nothing)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            outKaNoLList.Clear()

            If (isGrouping) Then

                Dim updateUnsoRows As IEnumerable(Of DataRow) = _
                    From row In inputUnsoTable.AsEnumerable() _
                    Where row.Item("AUTO_DENP_NO").Equals(keyNumber)
                    Select row

                For Each updateRow As DataRow In updateUnsoRows

                    ' 更新対象の出荷管理番号格納
                    outKaNoLList.Add(updateRow("OUTKA_NO_L").ToString())

                    '初期化
                    updateData.Clear()

                    '条件の設定
                    updateUnsoInTable.ImportRow(updateRow)
                    ds = MyBase.CallBLC(New LMC794BLC(), "UpdateUnsoL", updateData)

                    If (MyBase.IsMessageStoreExist() OrElse _
                        MyBase.IsMessageExist()) Then

                        Return ds
                    End If

                Next
            Else
                outKaNoLList.Add(keyNumber)
            End If

            ' 出荷(大)テーブル更新
            For Each outKaNoL As String In outKaNoLList

                Dim updateOutKaRows As IEnumerable(Of DataRow) = _
                    From row In inputOutkaTable.AsEnumerable() _
                    Where row.Item("OUTKA_NO_L").Equals(outKaNoL)
                    Select row

                For Each updateRow As DataRow In updateOutKaRows

                    '自営業以外、スルー
                    If updateRow("USER_BR_CD").ToString().Equals(updateRow("NRS_BR_CD").ToString()) = False Then
                        Continue For
                    End If

                    '初期化
                    updateData.Clear()

                    '条件の設定
                    updateOutkaInTable.ImportRow(updateRow)

                    ds = MyBase.CallBLC(Me._Blc, "UpdateOutkaLPrintMeitetu", updateData)

                    If (MyBase.IsMessageStoreExist() OrElse _
                        MyBase.IsMessageExist()) Then

                        Return ds
                    End If


                Next
            Next


            'エラーがあるかを判定
            If (MyBase.IsMessageStoreExist() = False AndAlso _
                MyBase.IsMessageExist() = False) Then
                MyBase.CommitTransaction(scope)
            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' サーバ日時を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetSysDateTime(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("SYS_DATETIME")
        Dim dr As DataRow = dt.NewRow()
        dr.Item("SYS_DATE") = MyBase.GetSystemDate()
        dr.Item("SYS_TIME") = MyBase.GetSystemTime()
        dt.Rows.Add(dr)
        Return ds

    End Function

    'ADD 2017/02/27 End
#End If

#Region "タブレット対応"

    ''' <summary>
    ''' タブレットデータのキャンセル処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Function UpdateTabletUnsoData(ByVal ds As DataSet) As DataSet

        Dim inDs As New LMC930DS

        Dim nrsBrCd As String = ds.Tables("UNSO_L").Rows(0).Item("NRS_BR_CD").ToString
        Dim unsoNoL As String = ds.Tables("UNSO_L").Rows(0).Item("UNSO_NO_L").ToString
        Dim whCd As String = ds.Tables("UNSO_L").Rows(0).Item("WH_CD").ToString
        Dim unsoCd As String = ds.Tables("ITEM").Rows(0).Item("UNSO_CD").ToString
        Dim unsoBrCd As String = ds.Tables("ITEM").Rows(0).Item("UNSO_BR_CD").ToString

        Dim inDt As DataTable = inDs.Tables("LMC930IN")
        Dim inDr As DataRow = inDt.NewRow
        inDr.Item("NRS_BR_CD") = nrsBrCd
        inDr.Item("OUTKA_NO_L") = String.Empty
        inDr.Item("UNSO_NO_L") = unsoNoL
        inDr.Item("WH_CD") = whCd
        inDr.Item("UNSO_CD") = unsoCd
        inDr.Item("UNSO_BR_CD") = unsoBrCd
        inDr.Item("PROC_TYPE") = "03"           '処理区分：運送変更
        inDt.Rows.Add(inDr)

        MyBase.CallBLC(New LMC930BLC(), LMC930BLC.FUNCTION_NM.WH_UNSO_UPDATE, inDs)

        Return ds

    End Function

#End Region

#End Region

#End Region

End Class
