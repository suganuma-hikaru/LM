' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME       : EDI
'  プログラムID     :  LMH010BLF : EDI入荷検索
'  作  成  者       :  nishikawa
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH010BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH010BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Const"
    Private Const FLG_SHORIZUMI As String = "01"

    ''' <summary>
    ''' INテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMH010INOUT"

    ''' <summary>
    ''' INテーブル名(出力ボタン押下時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUTPUTIN As String = "LMH010_OUTPUTIN"


    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_JUDGE_IN As String = "LMH010_JUDGE"

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"


#End Region

#Region "EDI荷主INDEX"
    'イベント種別
    Public Enum EdiCustIndex As Integer

        Ncgo32516_00 = 24                   '日本合成化学(名古屋)
        Dupont00295_00 = 16                 'デュポン(横浜)
        Dupont00331_00 = 34                 'デュポン(DCSE)(横浜)
        Dupont00331_02 = 35                 'デュポン(ABS)(横浜)
        Dupont00588_00 = 36                 'デュポン(SFTP塗料)(横浜)
        Dupont00331_03 = 37                 'デュポン()(横浜)
        Dupont00089_00 = 3                  'デュポン(テフロン)(千葉)→(横浜)に移送     '2012.04.11 ADD
        Dupont00700_00 = 33                 'デュポン(DCSE)(大阪)
        Dupont00689_00 = 32                 'デュポン(PVFM)(大阪)
        Dupont00300_00 = 15                 'デュポン(EP)(大阪)
        Dupont00187_00 = 23                 'デュポン(ブタサイト)(千葉)
        Dupont00188_00 = 14                 'デュポン(EP)(千葉)
        Dupont00587_00 = 29                 'デュポン(農業)(千葉)
        Dupont00589_00 = 41                 'デュポン(特殊化学品)(千葉)
        Dupont00688_00 = 31                 'デュポン(電子材料事業)(千葉)
        Dow00109_00 = 17                    'ダウケミ(大阪)
        DowTaka00109_01 = 18                'ダウケミ(大阪・高石)
        Toho00275_00 = 26                   '東邦化学(大阪)
        Toho00347_00 = 112                  '東邦化学(千葉）
        Toho00431_00 = 163                  '東邦化学(群馬）
        UkimaOsk00856_00 = 38               '浮間合成(大阪)               'ADD 2017/02/24
        Nissan00145_00 = 13                 '日産物流(千葉)
        Nik00171_00 = 39                    '日医工(千葉)
        Fjf00195_00 = 40                    '富士フイルム(千葉)
        UkimaSai00856_00 = 1                '浮間合成(岩槻)
        Sumika00952_00 = 2                  '住化カラー(岩槻)
        Bp00023_00 = 30                     'ビーピー・カストロール(岩槻)    '2012.12.12 ADD
        TrmChb00409_00 = 25                 'テルモ(千葉)                    '2013.03.07 ADD
        '2013.08.30 要望番号2100 日立FN対応　追加START
        DicGnm00076_00 = 43                 '（群馬）ディック物流群馬
        DicItk10001_00 = 44                 '（岩槻）ディック物流春日部
        '2013.08.30 要望番号2100 日立FN対応　追加END
        DicItk10007_00 = 45                 '（岩槻）ディック物流東京営業所
        DicChb00010_00 = 103                 '（群馬）ディック物流群馬
        FjfTaka00195_00 = 46                '富士フイルム(高取)
        Rome00061_00 = 47                    '(大阪)ローム・アンド･ハース電子材料株式会社 '2015.04.13 ADD
        Byk00266_00 = 48                    '(千葉)ビックケミー '2015.08.20 ADD
        RomeYok00003_00 = 49                '(横浜)ローム・アンド･ハース(国内・輸出) '2015.09.24 ADD
        FirmeYok00004_00 = 108               '(横浜)日本フィルメニッヒ 20160411 added 
        ITC00125_00 = 115                   '(横浜)インターコンチネンタル商事株式会社 20170705  
        AWS00801_00 = 116                   '(千葉)エアウォーターゾル 20170725  
        '2018/02/07 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 add start
        Mimaki45741 = 124                   'ディストリビューション･ミマキエンジニアリング
        '2018/02/07 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 add end
        '2019.06.24 要望番号006280 add start
        AgcW00440 = 140                     '(大阪)ＡＧＣ若狭化学
        '2019.06.24 要望番号006280 add end
#If True Then   'ADD 2020/05/14 007999   【LMS】セミEDI_ジョンソンエンドジョンソンJ&J_土気自動倉庫預かり_入荷・出荷新規
        JJ15 = 151                '（土気自動倉庫）ジョンソンエンドジョンソンJ&J_
#End If
        SGM10 = 157                         '(千葉)シグマアルドリッチジャパン
        TSMC75 = 165                        '(熊本)TSMC
        Tetsutani = 166                     '(大阪)テツタニ
        Rapidus40 = 167                     '(横浜)Rapidus
        BAH15 = 169                         '(土気)物産アニマルヘルス
    End Enum

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _UnchinBlc As LMF800BLC = New LMF800BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print500 As LMH500BLC = New LMH500BLC()

    'START UMANO 要望番号1302 支払運賃に伴う修正。支払バッチ呼び出し
    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _ShiharaiBlc As LMF810BLC = New LMF810BLC()
    'END UMANO 要望番号1302 支払運賃に伴う修正。支払バッチ呼び出し

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print502 As LMH502BLC = New LMH502BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMH010BLC = New LMH010BLC()

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 初期検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Dim jobNm As String = String.Empty
        Dim ediIndex As Integer = 0
        Dim rtnBlc As Base.BLC.LMBaseBLC

        ediIndex = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(0)("EDI_CUST_INDEX"))

        rtnBlc = New LMH010BLC

        '強制実行フラグがオフ時のみ（オンになるのは閾値オーバーでも表示する時）
        If MyBase.GetForceOparation() = False Then

            jobNm = "SelectData"
            'データ件数取得
            ds = MyBase.CallBLC(rtnBlc, jobNm, ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        jobNm = "SelectListData"

        Return MyBase.CallBLC(rtnBlc, jobNm, ds)

    End Function

#End Region

#Region "入荷登録処理"

    ''' <summary>
    ''' 入荷登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InkaToroku(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        '更新対象テーブル名
        Dim tableNm As String = "LMH010INOUT"
        Dim dt As DataTable = ds.Tables(tableNm)
        Dim dtJudge As DataTable = ds.Tables("LMH010_JUDGE")
        Dim max As Integer = dt.Rows.Count - 1
        Dim ediIndex As Integer = 0

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDt As DataTable = setDs.Tables(tableNm)
        Dim setDtJudge As DataTable = setDs.Tables("LMH010_JUDGE")
        Dim setDtWarning As DataTable = setDs.Tables("WARNING_SHORI")
        '▼▼▼二次
        '日産物流専用一括まとめ用データテーブル
        Dim setDtMatomeChk As DataTable = setDs.Tables("LMH010_IKKATUMATOME_CHK")
        '▲▲▲二次
        Dim jobNm As String = "InkaToroku"
        Dim rtnBlc As Base.BLC.LMBaseBLC

        Dim shoriFlg As String = String.Empty
        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            rtnResult = False

            shoriFlg = ds.Tables(tableNm).Rows(i)("SHORI_FLG").ToString()
            '処理フラグ判定
            If shoriFlg.Equals(LMH010BLF.FLG_SHORIZUMI) Then
                '処理済の場合は次のレコードへ
                Continue For
            End If

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                ediIndex = Convert.ToInt32(ds.Tables(tableNm).Rows(i)("EDI_CUST_INDEX"))

                '▼▼▼二次
                'rtnBlc = getBLC(ediIndex)
                rtnBlc = getBlcInkaToroku(ediIndex)
                '▲▲▲二次

                '値のクリア
                setDs.Clear()

                '条件の設定
                setDt.ImportRow(dt.Rows(i))
                setDtJudge.ImportRow(dtJudge.Rows(0))
                setDtWarning.Merge(ds.Tables("WARNING_SHORI"))
                '▼▼▼二次
                setDtMatomeChk.Merge(ds.Tables("LMH010_IKKATUMATOME_CHK"))
                '▲▲▲二次

                setDs = MyBase.CallBLC(rtnBlc, jobNm, setDs)

                rowNo = Convert.ToInt32(dt.Rows(i).Item("ROW_NO"))

                'エラーがあるかを判定
                'rtnResult = Not MyBase.IsMessageExist()
                If MyBase.IsMessageStoreExist(rowNo) = False Then
                    If setDs.Tables("WARNING_DTL").Rows.Count = 0 Then
                        rtnResult = True
                    End If
                End If

                If rtnResult = True Then
                    If setDs.Tables("LMH010_INKAEDI_L").Rows(0)("UNCHIN_TP").ToString().Equals("10") = False Then
                        'トランザクション終了
                        MyBase.CommitTransaction(scope)
                        '処理フラグに処理済設定
                        ds.Tables(tableNm).Rows(i)("SHORI_FLG") = LMH010BLF.FLG_SHORIZUMI
                    Else

                        If ("10").Equals(setDs.Tables("LMH010_UNSO_L").Rows(0).Item("UNSO_TEHAI_KB").ToString) = True Then
                            '日陸手配の時のみ運賃データを作成

                            'データセット設定
                            Dim unchinDs As DataSet = Me.SetUnchinInDataSet(setDs)

                            'BLCアクセス
                            unchinDs = MyBase.CallBLC(Me._UnchinBlc, "CreateUnchinData", unchinDs)

                            'LMF800の戻り値判定
                            Dim rtnResultDt As DataTable = unchinDs.Tables("LMF800RESULT")
                            Dim rtnResultDr As DataRow = rtnResultDt.Rows(0)

                            If ("00").Equals(rtnResultDr.Item("STATUS").ToString) = True OrElse _
                               ("05").Equals(rtnResultDr.Item("STATUS").ToString) = True OrElse _
                               ("30").Equals(rtnResultDr.Item("STATUS").ToString) = True Then
                                '要望番号1223:(EDIの出荷登録・運送登録時の運賃バッチについて) 2012/08/22 本明 Start
                                MyBase.SetMessage(Nothing)  'LMF800のメッセージをクリア
                                '要望番号1223:(EDIの出荷登録・運送登録時の運賃バッチについて) 2012/08/22 本明 End

                                '正常の場合は保存処理

                                'BLCアクセス
                                unchinDs = MyBase.CallBLC(rtnBlc, "UnchinSakusei", unchinDs)

                                'エラーがあるかを判定
                                rtnResult = Not MyBase.IsMessageExist()

                                If rtnResult = True Then

                                    'START UMANO 要望番号1302 支払運賃に伴う修正。支払バッチ呼び出し
                                    If String.IsNullOrEmpty(setDs.Tables("LMH010_UNSO_L").Rows(0).Item("UNSO_CD").ToString) = True Then
                                        '運送会社が指定されていない場合
                                        'トランザクション終了
                                        MyBase.CommitTransaction(scope)
                                        '処理フラグに処理済設定
                                        ds.Tables(tableNm).Rows(i)("SHORI_FLG") = LMH010BLF.FLG_SHORIZUMI
                                    Else
                                        '運送会社が指定されている場合、支払バッチを呼び出す
                                        'データセット設定
                                        Dim shiharaiDs As DataSet = Me.SetShiharaiInDataSet(setDs)

                                        'BLCアクセス
                                        shiharaiDs = MyBase.CallBLC(Me._ShiharaiBlc, "CreateUnchinData", shiharaiDs)

                                        'LMF810の戻り値判定
                                        Dim rtnResultDt2 As DataTable = shiharaiDs.Tables("LMF810RESULT")
                                        Dim rtnResultDr2 As DataRow = rtnResultDt2.Rows(0)

                                        If ("00").Equals(rtnResultDr2.Item("STATUS").ToString) = True OrElse _
                                           ("05").Equals(rtnResultDr2.Item("STATUS").ToString) = True OrElse _
                                           ("30").Equals(rtnResultDr2.Item("STATUS").ToString) = True Then
                                            MyBase.SetMessage(Nothing)  'LMF810のメッセージをクリア

                                            '正常の場合は保存処理
                                            'BLCアクセス(支払運賃作成)
                                            '共通クラスで処理を行う
                                            rtnBlc = New LMH010BLC
                                            shiharaiDs = MyBase.CallBLC(rtnBlc, "ShiharaiSakusei", shiharaiDs)

                                            'エラーがあるかを判定
                                            rtnResult = Not MyBase.IsMessageExist()

                                            If rtnResult = True Then
                                                'トランザクション終了
                                                MyBase.CommitTransaction(scope)
                                                '処理フラグに処理済設定
                                                ds.Tables(tableNm).Rows(i)("SHORI_FLG") = LMH010BLF.FLG_SHORIZUMI
                                            End If

                                        Else
                                            'エラーの場合はエラー処理
                                            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, rtnResultDr2.Item("ERROR_CD").ToString, _
                                                                   New String() {rtnResultDr2.Item("YOBI1").ToString}, _
                                                                   setDs.Tables("LMH010INOUT").Rows(0).Item("ROW_NO").ToString(), LMH010BLC.EXCEL_COLTITLE, _
                                                                   setDs.Tables("LMH010INOUT").Rows(0).Item("EDI_CTL_NO").ToString())

                                        End If


                                    End If

                                    ''トランザクション終了
                                    'MyBase.CommitTransaction(scope)
                                    ''処理フラグに処理済設定
                                    'ds.Tables(tableNm).Rows(i)("SHORI_FLG") = LMH010BLF.FLG_SHORIZUMI

                                    'END UMANO 要望番号1302 支払運賃に伴う修正。支払バッチ呼び出し

                                End If

                            Else
                                'エラーの場合はエラー処理
                                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, rtnResultDr.Item("ERROR_CD").ToString, _
                                                       New String() {rtnResultDr.Item("YOBI1").ToString}, _
                                                       setDs.Tables("LMH010INOUT").Rows(0).Item("ROW_NO").ToString(), LMH010BLC.EXCEL_COLTITLE, _
                                                       setDs.Tables("LMH010INOUT").Rows(0).Item("EDI_CTL_NO").ToString())


                            End If

                        Else
                            '先方手配、未定の時はトランザクション終了
                            'トランザクション終了
                            MyBase.CommitTransaction(scope)
                            '処理フラグに処理済設定
                            ds.Tables(tableNm).Rows(i)("SHORI_FLG") = LMH010BLF.FLG_SHORIZUMI
                        End If
                    End If
                    'エラーがある場合、ロールバック
                End If

                'エラーの場合でも、処理フラグに処理済設定
                If MyBase.IsMessageStoreExist(rowNo) = True Then
                    ds.Tables(tableNm).Rows(i)("SHORI_FLG") = LMH010BLF.FLG_SHORIZUMI
                    'このレコードのワーニングをクリア(エラーがあるレコードはワーニングを出さない)
                    setDs.Tables("WARNING_DTL").Rows.Clear()
                End If

            End Using

            If setDs.Tables("WARNING_DTL").Rows.Count <> 0 Then
                'ワーニングが設定されている場合、データセットに設定する
                ds = Me.SetDsWarningData(ds, setDs)

            End If

            '▼▼▼二次
            ds.Tables("LMH010_IKKATUMATOME_CHK").Clear()
            ds.Tables("LMH010_IKKATUMATOME_CHK").Merge(setDs.Tables("LMH010_IKKATUMATOME_CHK"))
            '▲▲▲二次

        Next

        Return ds

    End Function

    Private Function SetDsWarningData(ByVal ds As DataSet, ByVal setDs As DataSet) As DataSet

        ds.Tables("WARNING_DTL").Merge(setDs.Tables("WARNING_DTL"))

        Return ds

    End Function


#End Region

#Region "実績作成"
    Private Function JissekiSakusei(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        '更新対象テーブル名
        Dim dtIn As DataTable = ds.Tables("LMH010INOUT")
        Dim dtEdiL As DataTable = ds.Tables("LMH010_INKAEDI_L")
        Dim dtEdiM As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim dtRcvHed As DataTable = ds.Tables("LMH010_RCV_HED")
        Dim dtRcvDtl As DataTable = ds.Tables("LMH010_RCV_DTL")
        Dim dtInkaL As DataTable = ds.Tables("LMH010_B_INKA_L")
        Dim dtJudge As DataTable = ds.Tables("LMH010_JUDGE")

        Dim max As Integer = dtEdiL.Rows.Count - 1
        Dim ediIndex As Integer = 0
        Dim rtn As String = String.Empty

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtIN As DataTable = setDs.Tables("LMH010INOUT")
        Dim setDtEdiL As DataTable = setDs.Tables("LMH010_INKAEDI_L")
        Dim setDtEdiM As DataTable = setDs.Tables("LMH010_INKAEDI_M")
        Dim setDtRcvHed As DataTable = setDs.Tables("LMH010_RCV_HED")
        Dim setDtRcvDtl As DataTable = setDs.Tables("LMH010_RCV_DTL")
        Dim setDtInkaL As DataTable = setDs.Tables("LMH010_B_INKA_L")
        Dim setDtJudge As DataTable = setDs.Tables("LMH010_JUDGE")
        Dim setDtWarning As DataTable = setDs.Tables("WARNING_SHORI")

        Dim jobNm As String = "JissekiSakusei"
        Dim rtnBlc As Base.BLC.LMBaseBLC

        Dim rowNo As Integer = 0

        Dim shoriFlg As String = String.Empty

        '要望番号:1815（千葉 日産物流 まとめた入荷を実績作成すると、排他エラーとなる) 2013/02/01 本明 Start
        Dim inkaCtlNoHt As Hashtable = New Hashtable
        Dim inkaCtlNo As String = String.Empty
        '要望番号:1815（千葉 日産物流 まとめた入荷を実績作成すると、排他エラーとなる) 2013/02/01 本明 End

        For i As Integer = 0 To max

            shoriFlg = ds.Tables("LMH010INOUT").Rows(i)("SHORI_FLG").ToString()
            '処理フラグ判定
            If shoriFlg.Equals(LMH010BLF.FLG_SHORIZUMI) Then
                '処理済の場合は次のレコードへ
                Continue For
            End If

            rtnResult = False

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                ediIndex = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(i)("EDI_CUST_INDEX"))
                '▼▼▼二次
                'rtnBlc = getBLC(ediIndex)
                rtnBlc = getBlcJisseki(ediIndex)
                '▲▲▲二次
                rowNo = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(i).Item("ROW_NO"))

                '値のクリア
                setDs.Clear()

                '条件の設定
                setDtIN.ImportRow(dtIn.Rows(i))
                setDtEdiL.ImportRow(dtEdiL.Rows(i))
                setDtEdiM.ImportRow(dtEdiM.Rows(i))
                setDtRcvHed.ImportRow(dtRcvHed.Rows(i))
                setDtRcvDtl.ImportRow(dtRcvDtl.Rows(i))
                setDtInkaL.ImportRow(dtInkaL.Rows(i))
                setDtJudge.ImportRow(dtJudge.Rows(0))
                setDtWarning.Merge(ds.Tables("WARNING_SHORI"))

                '要望番号:1815（千葉 日産物流 まとめた入荷を実績作成すると、排他エラーとなる) 2013/02/01 本明 Start
                inkaCtlNo = dtInkaL.Rows(i).Item("INKA_NO_L").ToString()
                If inkaCtlNoHt.ContainsKey(inkaCtlNo) = True Then
                    '同一入荷管理番号の場合は、既に進捗区分を更新しているので、入荷(大)は更新しない
                    setDtIN.Rows(0).Item("INKA_L_UPD_FLG") = "0"
                Else
                    setDtIN.Rows(0).Item("INKA_L_UPD_FLG") = "1"
                    inkaCtlNoHt.Add(inkaCtlNo, String.Empty)
                End If
                '要望番号:1815（千葉 日産物流 まとめた入荷を実績作成すると、排他エラーとなる) 2013/02/01 本明 End

                setDs = MyBase.CallBLC(rtnBlc, jobNm, setDs)

                'エラーがあるかを判定
                If MyBase.IsMessageStoreExist(rowNo) = False Then
                    If setDs.Tables("WARNING_DTL").Rows.Count = 0 Then
                        rtnResult = True
                    End If
                End If

                If rtnResult = True Then
                    'エラーが無ければCommit
                    MyBase.CommitTransaction(scope)
                    '処理フラグに処理済設定
                    ds.Tables("LMH010INOUT").Rows(i)("SHORI_FLG") = LMH010BLF.FLG_SHORIZUMI
                End If

                'エラーの場合でも、処理フラグに処理済設定
                If MyBase.IsMessageStoreExist(rowNo) = True Then
                    ds.Tables("LMH010INOUT").Rows(i)("SHORI_FLG") = LMH010BLF.FLG_SHORIZUMI
                    'このレコードのワーニングをクリア(エラーがあるレコードはワーニングを出さない)
                    setDs.Tables("WARNING_DTL").Rows.Clear()
                End If

            End Using

            If setDs.Tables("WARNING_DTL").Rows.Count <> 0 Then
                'ワーニングが設定されている場合、データセットに設定する
                ds = Me.SetDsWarningData(ds, setDs)
            End If

        Next

        Return ds
    End Function
#End Region

#Region "EDI取消"
    Private Function EdiTorikesi(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        '更新対象テーブル名

        Dim dtEdiL As DataTable = ds.Tables("LMH010_INKAEDI_L")
        Dim dtEdiM As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim dtRcvHed As DataTable = ds.Tables("LMH010_RCV_HED")
        Dim dtRcvDtl As DataTable = ds.Tables("LMH010_RCV_DTL")
        Dim dtJudge As DataTable = ds.Tables("LMH010_JUDGE")
        Dim dtIn As DataTable = ds.Tables("LMH010INOUT")

        Dim max As Integer = dtEdiL.Rows.Count - 1
        Dim ediIndex As Integer = 0
        Dim rtn As String = String.Empty

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtEdiL As DataTable = setDs.Tables("LMH010_INKAEDI_L")
        Dim setDtEdiM As DataTable = setDs.Tables("LMH010_INKAEDI_M")
        Dim setDtRcvHed As DataTable = setDs.Tables("LMH010_RCV_HED")
        Dim setDtRcvDtl As DataTable = setDs.Tables("LMH010_RCV_DTL")
        Dim setDtJudge As DataTable = setDs.Tables("LMH010_JUDGE")
        Dim setDtIn As DataTable = setDs.Tables("LMH010INOUT")

        Dim jobNm As String = "EdiTorikesi"
        Dim rtnBlc As Base.BLC.LMBaseBLC

        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            rtnResult = False

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                ediIndex = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(i)("EDI_CUST_INDEX"))
                '▼▼▼二次
                'rtnBlc = getBLC(ediIndex)
                rtnBlc = getBlcEditorikesi(ediIndex)
                '▲▲▲二次
                rowNo = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(i).Item("ROW_NO"))

                '値のクリア
                setDs.Clear()

                '条件の設定
                setDtEdiL.ImportRow(dtEdiL.Rows(i))
                setDtEdiM.ImportRow(dtEdiM.Rows(i))
                setDtRcvHed.ImportRow(dtRcvHed.Rows(i))
                setDtRcvDtl.ImportRow(dtRcvDtl.Rows(i))
                setDtJudge.ImportRow(dtJudge.Rows(0))
                setDtIn.ImportRow(dtIn.Rows(i))

                setDs = MyBase.CallBLC(rtnBlc, jobNm, setDs)

                'エラーがあるかを判定
                If MyBase.IsMessageStoreExist(rowNo) = False Then
                    rtnResult = True
                End If

                If rtnResult = True Then
                    'エラーが無ければCommit
                    MyBase.CommitTransaction(scope)
                End If

            End Using

        Next

        Return ds
    End Function
#End Region

#Region "取込(セミEDI)"
    Private Function SemiEdiTorikomi(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        '更新対象テーブル名

        Dim dtSemiInfo As DataTable = ds.Tables("LMH010_SEMIEDI_INFO")
        Dim dtSemiHed As DataTable = ds.Tables("LMH010_EDI_TORIKOMI_HED")
        Dim dtSemiDtl As DataTable = ds.Tables("LMH010_EDI_TORIKOMI_DTL")
        Dim dtSemiRet As DataTable = ds.Tables("LMH010_EDI_TORIKOMI_RET")   '処理件数

        '処理件数クリア
        dtSemiRet.Clear()
        dtSemiRet.Rows.Add(0)

        '総件数
        dtSemiRet.Rows(0).Item("ALL_CNT") = dtSemiDtl.Rows.Count.ToString()
        dtSemiRet.Rows(0).Item("RCV_HED_INS_CNT") = "0"
        dtSemiRet.Rows(0).Item("RCV_DTL_INS_CNT") = "0"
        dtSemiRet.Rows(0).Item("IN_HED_INS_CNT") = "0"
        dtSemiRet.Rows(0).Item("IN_DTL_INS_CNT") = "0"
        dtSemiRet.Rows(0).Item("RCV_HED_CAN_CNT") = "0"
        dtSemiRet.Rows(0).Item("RCV_DTL_CAN_CNT") = "0"
        dtSemiRet.Rows(0).Item("IN_HED_CAN_CNT") = "0"
        dtSemiRet.Rows(0).Item("IN_DTL_CAN_CNT") = "0"

        Dim iHedMax As Integer = dtSemiHed.Rows.Count - 1
        Dim iDtlMax As Integer = dtSemiDtl.Rows.Count - 1

        Dim iEdiIndex As Integer = 0
        Dim rtn As String = String.Empty

        Dim jobNmchk As String = "SemiEdiTorikomiChk"
        Dim jobNm As String = "SemiEdiTorikomi"
        Dim rtnBlc As Base.BLC.LMBaseBLC

        Dim rowNo As Integer = 0

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtInfo As DataTable = setDs.Tables("LMH010_SEMIEDI_INFO")
        Dim setDtHed As DataTable = setDs.Tables("LMH010_EDI_TORIKOMI_HED")
        Dim setSemiRet As DataTable = setDs.Tables("LMH010_EDI_TORIKOMI_RET")   '処理件数

        '更新用詳細DS（ファイル別毎の詳細DS）
        Dim setDtDtl As DataTable = setDs.Tables("LMH010_EDI_TORIKOMI_DTL")

        '2011.09.16 初期値を必ずFALSEにする
        rtnResult = False

        'EdiIndexを取得
        iEdiIndex = Convert.ToInt32(dtSemiInfo.Rows(0)("EDI_CUST_INDEX"))

        rtnBlc = getBlcSemiEdiTorikomi(iEdiIndex)

        '入力チェック処理(各個別BLC)
        ds = MyBase.CallBLC(rtnBlc, jobNmchk, ds)

        '入力チェック処理後、再取得（入力チェック処理で不要データを削除、並び順を変えている場合があるため）
        dtSemiHed = ds.Tables("LMH010_EDI_TORIKOMI_HED")
        dtSemiDtl = ds.Tables("LMH010_EDI_TORIKOMI_DTL")
        iDtlMax = dtSemiDtl.Rows.Count - 1

        Dim iDtlRowCnt As Integer = 0

        '受信ファイルの数だけループする
        For i As Integer = 0 To iHedMax

            'エラーがあるかを判定
            If dtSemiHed.Rows(i).Item("ERR_FLG").ToString().Equals("1") = True Then
                'エラーが有る場合は処理終了
                Exit For
            End If

            'エラーが無い場合

            '現在のヘッダ情報を１行取得
            setDtHed.Clear()
            setDtHed.ImportRow(dtSemiHed.Rows(i))


            '更新用の詳細DSをクリア
            setDtDtl.Clear()

            '詳細ループ
            For j As Integer = iDtlRowCnt To iDtlMax

                'ヘッドと詳細のファイル名が同じ場合
                If (dtSemiHed.Rows(i).Item("FILE_NAME_RCV").ToString()).Equals _
                   (dtSemiDtl.Rows(j).Item("FILE_NAME_RCV").ToString()) = True Then

                    '更新用の詳細DSにセットする
                    setDtDtl.ImportRow(dtSemiDtl.Rows(j))

                Else
                    '無駄なループを行わないため現在の詳細カウントを保持
                    iDtlRowCnt = j
                    '詳細ループを抜ける
                    Exit For
                End If

            Next

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                'エラーフラグを立てる（更新中のエラーはスローされてしまうので）
                dtSemiHed.Rows(i).Item("ERR_FLG") = "1"

                '処理件数のエラーフラグをセットする（0:正常、1:異常、2:混在）
                Select Case i
                    Case 0
                        '１件目の場合は異常にする（更新中のエラーはスローされてしまうので）
                        dtSemiRet.Rows(0).Item("ERR_FLG") = "1"
                    Case Else
                        '２件目以降は混在にする（以前の更新はOKなので）
                        dtSemiRet.Rows(0).Item("ERR_FLG") = "2"
                End Select


                '更新用詳細DSを元に更新処理を行う(各個別BLC)
                setDs = MyBase.CallBLC(rtnBlc, jobNm, setDs)

                '戻り値判定
                If setDtHed.Rows(0).Item("ERR_FLG").ToString().Equals("0") = True Then
                    '正常終了の場合、CommitしてERR_FLGに"0"をセット
                    rtnResult = True
                    MyBase.CommitTransaction(scope)
                    dtSemiHed.Rows(i).Item("ERR_FLG") = "0"
                    dtSemiRet.Rows(0).Item("ERR_FLG") = "0"

                    '処理件数セット
                    dtSemiRet.Rows(0).Item("FILE_CNT") = (Convert.ToInt32(dtSemiRet.Rows(0).Item("FILE_CNT").ToString()) + 1).ToString

                    dtSemiRet.Rows(0).Item("RCV_HED_INS_CNT") = (Convert.ToInt32(dtSemiRet.Rows(0).Item("RCV_HED_INS_CNT").ToString()) _
                                                              + Convert.ToInt32(setSemiRet.Rows(0).Item("RCV_HED_INS_CNT").ToString())).ToString

                    dtSemiRet.Rows(0).Item("RCV_DTL_INS_CNT") = (Convert.ToInt32(dtSemiRet.Rows(0).Item("RCV_DTL_INS_CNT").ToString()) _
                                                              + Convert.ToInt32(setSemiRet.Rows(0).Item("RCV_DTL_INS_CNT").ToString())).ToString

                    dtSemiRet.Rows(0).Item("IN_HED_INS_CNT") = (Convert.ToInt32(dtSemiRet.Rows(0).Item("IN_HED_INS_CNT").ToString()) _
                                                              + Convert.ToInt32(setSemiRet.Rows(0).Item("IN_HED_INS_CNT").ToString())).ToString

                    dtSemiRet.Rows(0).Item("IN_DTL_INS_CNT") = (Convert.ToInt32(dtSemiRet.Rows(0).Item("IN_DTL_INS_CNT").ToString()) _
                                                              + Convert.ToInt32(setSemiRet.Rows(0).Item("IN_DTL_INS_CNT").ToString())).ToString

                    dtSemiRet.Rows(0).Item("RCV_HED_CAN_CNT") = (Convert.ToInt32(dtSemiRet.Rows(0).Item("RCV_HED_CAN_CNT").ToString()) _
                                                              + Convert.ToInt32(setSemiRet.Rows(0).Item("RCV_HED_CAN_CNT").ToString())).ToString

                    dtSemiRet.Rows(0).Item("RCV_DTL_CAN_CNT") = (Convert.ToInt32(dtSemiRet.Rows(0).Item("RCV_DTL_CAN_CNT").ToString()) _
                                                              + Convert.ToInt32(setSemiRet.Rows(0).Item("RCV_DTL_CAN_CNT").ToString())).ToString

                    dtSemiRet.Rows(0).Item("IN_HED_CAN_CNT") = (Convert.ToInt32(dtSemiRet.Rows(0).Item("IN_HED_CAN_CNT").ToString()) _
                                                              + Convert.ToInt32(setSemiRet.Rows(0).Item("IN_HED_CAN_CNT").ToString())).ToString

                    dtSemiRet.Rows(0).Item("IN_DTL_CAN_CNT") = (Convert.ToInt32(dtSemiRet.Rows(0).Item("IN_DTL_CAN_CNT").ToString()) _
                                                              + Convert.ToInt32(setSemiRet.Rows(0).Item("IN_DTL_CAN_CNT").ToString())).ToString

                Else
                    '異常終了の場合、CommitせずにERR_FLGに"1"をセット
                    dtSemiHed.Rows(i).Item("ERR_FLG") = "1"

                    '処理件数のエラーフラグをセットする（0:正常、1:異常、2:混在）
                    Select Case i
                        Case 0
                            '１件目の場合は異常にする
                            dtSemiRet.Rows(0).Item("ERR_FLG") = "1"
                        Case Else
                            '２件目以降は混在にする（以前の更新はOKなので）
                            dtSemiRet.Rows(0).Item("ERR_FLG") = "2"
                    End Select
                End If
            End Using

        Next

        Return ds
    End Function
#End Region

#Region "実績取消処理"

    ''' <summary>
    ''' 実績取消処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiTorikesi(ByVal ds As DataSet) As DataSet

        '更新対象テーブル名
        Dim dtJudge As DataTable = ds.Tables("LMH010_JUDGE")
        Dim dtEdiL As DataTable = ds.Tables("LMH010_INKAEDI_L")
        Dim dtRcvHed As DataTable = ds.Tables("LMH010_RCV_HED")
        Dim dtRcvDtl As DataTable = ds.Tables("LMH010_RCV_DTL")
        Dim dtIn As DataTable = ds.Tables("LMH010INOUT")

        Dim max As Integer = dtEdiL.Rows.Count - 1
        Dim ediIndex As Integer = 0
        Dim rtn As String = String.Empty
        Dim rtnResult As Boolean = False

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtJudge As DataTable = setDs.Tables("LMH010_JUDGE")
        Dim setDtEdiL As DataTable = setDs.Tables("LMH010_INKAEDI_L")
        Dim setDtRcvHed As DataTable = setDs.Tables("LMH010_RCV_HED")
        Dim setDtRcvDtl As DataTable = setDs.Tables("LMH010_RCV_DTL")
        Dim setDtIn As DataTable = setDs.Tables("LMH010INOUT")

        Dim jobNm As String = "JissekiTorikesi"
        Dim rtnBlc As Base.BLC.LMBaseBLC

        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            rtnResult = False

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                ediIndex = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(i)("EDI_CUST_INDEX"))
                '▼▼▼二次
                'rtnBlc = getBLC(ediIndex)
                rtnBlc = getBlcJissekiTorikesi(ediIndex)
                '▲▲▲二次
                rowNo = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(i).Item("ROW_NO"))

                '値のクリア
                setDs.Clear()

                '条件の設定
                setDtJudge.ImportRow(dtJudge.Rows(0))
                setDtEdiL.ImportRow(dtEdiL.Rows(i))
                setDtRcvHed.ImportRow(dtRcvHed.Rows(i))
                setDtRcvDtl.ImportRow(dtRcvDtl.Rows(i))
                setDtIn.ImportRow(dtIn.Rows(i))

                setDs = MyBase.CallBLC(rtnBlc, jobNm, setDs)

                'エラーがあるかを判定
                If MyBase.IsMessageStoreExist(rowNo) = False Then
                    rtnResult = True
                End If

                If rtnResult = True Then
                    'トランザクション終了
                    MyBase.CommitTransaction(scope)
                End If

            End Using

        Next

        Return ds

    End Function

#End Region

#Region "EDI取消⇒未登録"
    Private Function TorikesiMitouroku(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        Dim ediIndex As Integer = 0

        Dim jobNm As String = "EdiOperation"
        Dim rtnBlc As Base.BLC.LMBaseBLC

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ediIndex = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(0)("EDI_CUST_INDEX"))
            '▼▼▼二次
            'rtnBlc = getBLC(ediIndex)
            rtnBlc = getBlcTorikesiMitouroku(ediIndex)
            '▲▲▲二次
            ds = MyBase.CallBLC(rtnBlc, jobNm, ds)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then
                'エラーが無ければCommit
                MyBase.CommitTransaction(scope)
            End If

        End Using

        Return ds
    End Function
#End Region

#Region "報告用EDI取消"
    'Private Function HoukokuEdiTorikesi(ByVal ds As DataSet) As DataSet

    '    Dim rtnResult As Boolean = False
    '    Dim ediIndex As Integer = 0

    '    Dim jobNm As String = "EdiOperation"
    '    Dim rtnBlc As Base.BLC.LMBaseBLC


    '    'トランザクション開始
    '    Using scope As TransactionScope = MyBase.BeginTransaction()

    '        ediIndex = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(0)("EDI_CUST_INDEX"))
    '        rtnBlc = getBLC(ediIndex)

    '        ds = MyBase.CallBLC(rtnBlc, jobNm, ds)

    '        'エラーがあるかを判定
    '        rtnResult = Not MyBase.IsMessageExist()

    '        If rtnResult = True Then
    '            'エラーが無ければCommit
    '            MyBase.CommitTransaction(scope)
    '        End If

    '    End Using

    '    Return ds
    'End Function
#End Region

#Region "実行処理(実績作成済⇒実績未)"

    ''' <summary>
    ''' 実行処理(実績作成済⇒実績未)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SakuseizumiJissekimi(ByVal ds As DataSet) As DataSet


        Dim rtnResult As Boolean = False
        Dim ediIndex As Integer = 0

        Dim jobNm As String = "EdiOperation2"
        Dim rtnBlc As Base.BLC.LMBaseBLC


        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ediIndex = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(0)("EDI_CUST_INDEX"))
            '▼▼▼二次
            'rtnBlc = getBLC(ediIndex)
            rtnBlc = getBlcSakuseizumiJissekimi(ediIndex)
            '▲▲▲二次
            ds = MyBase.CallBLC(rtnBlc, jobNm, ds)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then
                'エラーが無ければCommit
                MyBase.CommitTransaction(scope)
            End If

        End Using

        Return ds

    End Function

#End Region

#Region "実行処理(実績送信済⇒送信待)"

    ''' <summary>
    ''' 実行処理(実績送信済⇒送信待)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SousinSousinmi(ByVal ds As DataSet) As DataSet


        Dim rtnResult As Boolean = False
        Dim ediIndex As Integer = 0

        Dim jobNm As String = "EdiOperation3"
        Dim rtnBlc As Base.BLC.LMBaseBLC


        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ediIndex = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(0)("EDI_CUST_INDEX"))
            '▼▼▼二次
            'rtnBlc = getBLC(ediIndex)
            rtnBlc = getBlcSousinSousinmi(ediIndex)
            '▲▲▲二次
            ds = MyBase.CallBLC(rtnBlc, jobNm, ds)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then
                'エラーが無ければCommit
                MyBase.CommitTransaction(scope)
            End If

        End Using

        Return ds

    End Function

#End Region

#Region "実行処理(実績送信済⇒実績未)"

    ''' <summary>
    ''' 実行処理(実績送信済⇒実績未)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SousinzumiJissekimi(ByVal ds As DataSet) As DataSet


        Dim rtnResult As Boolean = False
        Dim ediIndex As Integer = 0

        Dim jobNm As String = "EdiOperation2"
        Dim rtnBlc As Base.BLC.LMBaseBLC


        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ediIndex = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(0)("EDI_CUST_INDEX"))

            '▼▼▼二次
            'rtnBlc = getBLC(ediIndex)
            rtnBlc = getBlcSousinzumiJissekimi(ediIndex)
            '▲▲▲二次

            ds = MyBase.CallBLC(rtnBlc, jobNm, ds)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then
                'エラーが無ければCommit
                MyBase.CommitTransaction(scope)
            End If

        End Using

        Return ds

    End Function

#End Region

#Region "実行処理(入荷取消⇒未登録)"

    ''' <summary>
    ''' 実行処理(入荷取消⇒未登録)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Mitouroku(ByVal ds As DataSet) As DataSet


        Dim rtnResult As Boolean = False
        Dim ediIndex As Integer = 0

        Dim jobNm As String = "Mitouroku"
        Dim rtnBlc As Base.BLC.LMBaseBLC

        '2012.02.25 大阪対応 START
        Dim matomeNo As String = String.Empty
        Dim autoMatomeF As String = String.Empty
        Dim rowNo As Integer = 0

        'まとめ番号を取得
        If String.IsNullOrEmpty(ds.Tables("LMH010INOUT").Rows(0)("MATOME_NO").ToString()) = False Then
            matomeNo = ds.Tables("LMH010INOUT").Rows(0)("MATOME_NO").ToString()
        End If

        If String.IsNullOrEmpty(ds.Tables("LMH010INOUT").Rows(0)("AUTO_MATOME_FLG").ToString()) = False Then
            autoMatomeF = ds.Tables("LMH010INOUT").Rows(0)("AUTO_MATOME_FLG").ToString()
        End If
        '2012.02.25 大阪対応 END

        '2012.02.25 大阪対応 START
        If String.IsNullOrEmpty(matomeNo) = False AndAlso autoMatomeF.Equals("9") = False Then
            'まとめ済みレコードの場合

            ediIndex = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(0)("EDI_CUST_INDEX"))
            '▼▼▼二次
            rtnBlc = getBlcTorikesiMitouroku(ediIndex)
            '▲▲▲二次

            '対象レコードがまとめ済みの場合、同一まとめ番号のレコードを取得する
            ds = MyBase.CallBLC(rtnBlc, "SelectMatomeTorikesi", ds)

            Dim eventShubetu As String = ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()

            rowNo = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(0).Item("ROW_NO"))

            Dim maxMatome As Integer = ds.Tables("LMH010OUT").Rows.Count - 1

            If maxMatome < 0 Then
                'エラーの場合はエラー処理
                MyBase.SetMessage("E011")

            End If

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                For j As Integer = 0 To maxMatome

                    Dim matomeDs As DataSet = ds.Copy
                    '値のクリア
                    matomeDs.Clear()

                    Call Me.SetDataTorikesiMitouroku(ds.Tables("LMH010OUT").Rows(j), matomeDs, eventShubetu, rowNo)

                    matomeDs = MyBase.CallBLC(rtnBlc, jobNm, matomeDs)

                    'エラーがあるかを判定
                    rowNo = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(0).Item("ROW_NO"))

                    If MyBase.IsMessageStoreExist(rowNo) = False AndAlso _
                       MyBase.IsMessageExist() = False Then
                        rtnResult = True
                    Else
                        '1件でもエラーがある場合は処理を抜けて終了
                        rtnResult = False
                        Exit For
                    End If

                Next

                If rtnResult = True Then

                    'トランザクション終了
                    MyBase.CommitTransaction(scope)

                End If

            End Using
        Else
            '2012.02.25 大阪対応 END


            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                ediIndex = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(0)("EDI_CUST_INDEX"))

                '▼▼▼二次
                'rtnBlc = getBLC(ediIndex)
                rtnBlc = getBlcMitouroku(ediIndex)
                '▲▲▲二次

                ds = MyBase.CallBLC(rtnBlc, jobNm, ds)

                'エラーがあるかを判定
                rtnResult = Not MyBase.IsMessageExist()

                If rtnResult = True Then
                    'エラーが無ければCommit
                    MyBase.CommitTransaction(scope)
                End If

            End Using
        End If

        Return ds

    End Function

#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function PrintData(ByVal ds As DataSet) As DataSet

        '印刷種別がチェックリストか、出荷報告書を取得
        Dim dt As DataTable = ds.Tables(LMH010BLF.TABLE_NM_IN)
        Dim dr As DataRow = dt.Rows(0)
        Dim eventflg As String = String.Empty
        Dim printflg As String = String.Empty

        eventflg = dr.Item("EVENT_SHUBETSU").ToString()     '処理種別(LMH010C.eventShubetsu)
        printflg = dr.Item("PRINT_SHUBETSU").ToString()     '印刷種別(LMH010C.Print_KBN)

        Select Case eventflg

            Case "19" '印刷ボタン押下

                Select Case printflg

                    Case "1"  'EDI入荷チェックリスト
                        ds = Me.PrintLMH500(dr)

                    Case "2"  'EDI入荷チェックリスト(アクサルタ用)
                        ds = Me.PrintLMH502(dr)

                End Select

                'ADD START 2019/9/12 依頼番号:007111
            Case "26" '現品票印刷

                'トランザクション開始
                Using scope As TransactionScope = MyBase.BeginTransaction()

                    '現品票印刷処理
                    ds = Me.PrintLMH503(ds)

                    If MyBase.IsMessageExist = False AndAlso MyBase.IsMessageStoreExist = False Then
                        'エラーが無ければCommit
                        MyBase.CommitTransaction(scope)
                    End If

                End Using
                'ADD END   2019/9/12 依頼番号:007111

        End Select

        Return ds

    End Function


    '2015.09.08 tsunehira add
#Region "一括変更"

    ''' <summary>
    ''' 一括変更
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Function SetDataEDI_L(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet = ds.Copy()
        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_L")
        Dim setDt As DataTable = setDs.Tables("LMH010_INKAEDI_L")
        Dim judge As DataTable = ds.Tables("LMH010_JUDGE")
        Dim setJudge As DataTable = setDs.Tables("LMH010_JUDGE")

        For i As Integer = 0 To dt.Rows.Count - 1

            MyBase.SetMessage(Nothing)

            setDs.Clear() 'データセットのクリア
            setDt.ImportRow(dt.Rows(i)) 'データセットに値を格納していく
            setJudge.ImportRow(judge.Rows(0)) 'イベント種別を格納する

            '商品keyの変更する値と入目を取得
            setDs = MyBase.CallBLC(Me._Blc, "SetEDI_M", setDs)

            'エラーがあるかを判定
            If MyBase.IsMessageExist() = True Then
                Continue For
            End If

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '荷主コードの変更
                setDs = MyBase.CallBLC(Me._Blc, "Chg_EDI_L", setDs)

                If setDs.Tables("LMH010_INKAEDI_M").Rows.Count > 0 Then
                    '商品keyを更新する
                    setDs = MyBase.CallBLC(Me._Blc, "Chg_EDI_M", setDs)

                End If

                'エラーがあるかを判定
                If MyBase.IsMessageExist() = False Then
                    'エラーが無ければCommit
                    MyBase.CommitTransaction(scope)
                End If

            End Using

        Next

        Return ds

    End Function
#End Region


    ''' <summary>
    ''' EDI入荷チェックリスト
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH500(ByVal dr As DataRow) As DataSet

        'EDI入荷チェックリスト
        Dim PrmDs As DataSet = New LMH500DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH500IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        PrmDr("EDIINKA_STATE_KB1") = dr.Item("EDIINKA_STATE_KB1")
        PrmDr("EDIINKA_STATE_KB2") = dr.Item("EDIINKA_STATE_KB2")
        PrmDr("EDIINKA_STATE_KB3") = dr.Item("EDIINKA_STATE_KB3")
        PrmDr("EDIINKA_STATE_KB4") = dr.Item("EDIINKA_STATE_KB4")
        PrmDr("EDIINKA_STATE_KB5") = dr.Item("EDIINKA_STATE_KB5")
        PrmDr("EDIINKA_STATE_KB6") = dr.Item("EDIINKA_STATE_KB6")
        PrmDr("EDIINKA_STATE_KB8") = dr.Item("EDIINKA_STATE_KB8")
        PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
        PrmDr("WH_CD") = dr.Item("WH_CD")
        PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
        PrmDr("TANTO_CD") = dr.Item("TANTO_CD")
        PrmDr("TORIKOMI_DATE_FROM") = dr.Item("TORIKOMI_DATE_FROM")
        PrmDr("TORIKOMI_DATE_TO") = dr.Item("TORIKOMI_DATE_TO")
        PrmDr("INKA_DATE_FROM") = dr.Item("INKA_DATE_FROM")
        PrmDr("INKA_DATE_TO") = dr.Item("INKA_DATE_TO")
        PrmDr("JYOTAI_KB") = dr.Item("JYOTAI_KB")
        PrmDr("HORYU_KB") = dr.Item("HORYU_KB")
        PrmDr("OUTKA_FROM_ORD_NO") = dr.Item("OUTKA_FROM_ORD_NO")
        PrmDr("CUST_NM") = dr.Item("CUST_NM")
        PrmDr("GOODS_NM") = dr.Item("GOODS_NM")
        PrmDr("INKA_TP") = dr.Item("INKA_TP")
        PrmDr("UNSO_KB") = dr.Item("UNSO_KB")
        PrmDr("UNSOCO_NM") = dr.Item("UNSOCO_NM")
        PrmDr("EDI_CTL_NO") = dr.Item("EDI_CTL_NO")
        PrmDr("INKA_CTL_NO_L") = dr.Item("INKA_CTL_NO_L")
        PrmDr("BUYER_ORD_NO_L") = dr.Item("BUYER_ORD_NO_L")
        PrmDr("TANTO_USER") = dr.Item("TANTO_USER")
        PrmDr("SYS_ENT_USER") = dr.Item("SYS_ENT_USER")
        PrmDr("SYS_UPD_USER") = dr.Item("SYS_UPD_USER")

        PrmDt.Rows.Add(PrmDr)

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        PrmDs.Merge(New RdPrevInfoDS)

        'LMH500の呼び出し
        PrmDs = MyBase.CallBLC(Me._Print500, "DoPrint", PrmDs)

        Return PrmDs

    End Function

    ''' <summary>
    ''' EDI入荷チェックリスト(アクサルタ用)
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH502(ByVal dr As DataRow) As DataSet

        'EDI入荷チェックリスト
        Dim PrmDs As DataSet = New LMH502DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH502IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        PrmDr("EDIINKA_STATE_KB1") = dr.Item("EDIINKA_STATE_KB1")
        PrmDr("EDIINKA_STATE_KB2") = dr.Item("EDIINKA_STATE_KB2")
        PrmDr("EDIINKA_STATE_KB3") = dr.Item("EDIINKA_STATE_KB3")
        PrmDr("EDIINKA_STATE_KB4") = dr.Item("EDIINKA_STATE_KB4")
        PrmDr("EDIINKA_STATE_KB5") = dr.Item("EDIINKA_STATE_KB5")
        PrmDr("EDIINKA_STATE_KB6") = dr.Item("EDIINKA_STATE_KB6")
        PrmDr("EDIINKA_STATE_KB8") = dr.Item("EDIINKA_STATE_KB8")
        PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
        PrmDr("WH_CD") = dr.Item("WH_CD")
        PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
        PrmDr("TANTO_CD") = dr.Item("TANTO_CD")
        PrmDr("TORIKOMI_DATE_FROM") = dr.Item("TORIKOMI_DATE_FROM")
        PrmDr("TORIKOMI_DATE_TO") = dr.Item("TORIKOMI_DATE_TO")
        PrmDr("INKA_DATE_FROM") = dr.Item("INKA_DATE_FROM")
        PrmDr("INKA_DATE_TO") = dr.Item("INKA_DATE_TO")
        PrmDr("JYOTAI_KB") = dr.Item("JYOTAI_KB")
        PrmDr("HORYU_KB") = dr.Item("HORYU_KB")
        PrmDr("OUTKA_FROM_ORD_NO") = dr.Item("OUTKA_FROM_ORD_NO")
        PrmDr("CUST_NM") = dr.Item("CUST_NM")
        PrmDr("GOODS_NM") = dr.Item("GOODS_NM")
        PrmDr("INKA_TP") = dr.Item("INKA_TP")
        PrmDr("UNSO_KB") = dr.Item("UNSO_KB")
        PrmDr("UNSOCO_NM") = dr.Item("UNSOCO_NM")
        PrmDr("EDI_CTL_NO") = dr.Item("EDI_CTL_NO")
        PrmDr("INKA_CTL_NO_L") = dr.Item("INKA_CTL_NO_L")
        PrmDr("BUYER_ORD_NO_L") = dr.Item("BUYER_ORD_NO_L")
        PrmDr("TANTO_USER") = dr.Item("TANTO_USER")
        PrmDr("SYS_ENT_USER") = dr.Item("SYS_ENT_USER")
        PrmDr("SYS_UPD_USER") = dr.Item("SYS_UPD_USER")

        PrmDt.Rows.Add(PrmDr)

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        PrmDs.Merge(New RdPrevInfoDS)

        'LMH502の呼び出し
        PrmDs = MyBase.CallBLC(Me._Print502, "DoPrint", PrmDs)

        Return PrmDs

    End Function

    'ADD START 2019/9/12 依頼番号:007111
    ''' <summary>
    ''' 現品票（FFEM大分）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH503(ByVal ds As DataSet) As DataSet

        Dim srcDt As DataTable = ds.Tables(LMH010BLF.TABLE_NM_IN)

        Dim prmDs As DataSet = New LMH503DS()
        Dim prmDt As DataTable = prmDs.Tables("PRINT_DATA_IN")

        For Each srcDr As DataRow In srcDt.Rows

            Dim prmDr As DataRow = prmDt.NewRow()

            prmDr("SHORI_KB") = "未発行"
            prmDr("NRS_BR_CD") = srcDr.Item("NRS_BR_CD")
            prmDr("WH_CD") = srcDr.Item("WH_CD")
            prmDr("CUST_CD_L") = srcDr.Item("CUST_CD_L")
            prmDr("CUST_CD_M") = srcDr.Item("CUST_CD_M")
            prmDr("SPREAD_ROW_NO") = srcDr.Item("ROW_NO")
            prmDr("EDI_CTL_NO") = srcDr.Item("EDI_CTL_NO")

            prmDt.Rows.Add(prmDr)

        Next

        prmDs.Merge(New RdPrevInfoDS)

        'LMH503の呼び出し
        prmDs = MyBase.CallBLC(New LMH503BLC(), "DoPrint", prmDs)

        Return prmDs

    End Function
    'ADD END   2019/9/12 依頼番号:007111

#End Region

    '2012.03.03 大阪対応START
#Region "出力処理(CSV作成・出力処理での印刷)"

    ''' <summary>
    ''' 出力処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function SetDsPrtData(ByVal ds As DataSet) As DataSet

        '印刷種別,処理種別(印刷or出力)の取得
        Dim dt As DataTable = ds.Tables(LMH010BLF.TABLE_NM_OUTPUTIN)

        '要望番号1061 2012.05.15 修正START
        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow
        Dim rtnDs As DataSet = Nothing

        'プレビュー用DataTable
        If ds.Tables(LMConst.RD) Is Nothing = True Then

            ds.Tables.Add(New RdPrevInfoDS.PREV_INFODataTable())

        End If
        Dim rtnDt As DataTable = ds.Tables(LMConst.RD)
        rtnDt.Clear()

        Dim cnt As Integer = 0

        For i As Integer = 0 To max
            dr = dt.Rows(i)

            'Dim dr As DataRow = dt.Rows(0)
            Dim outPutflg As String = String.Empty
            Dim setBlc As Base.BLC.LMBaseBLC
            Dim prtflgDs As DataSet = New LMH010DS()

            Dim setDs As DataSet() = Nothing
            Dim prtBlc As Com.Base.BaseBLC() = Nothing

            '2012.03.18 大阪対応START
            Dim ediCustIdx As String = dr.Item("EDI_CUST_INDEX").ToString()
            '2012.03.18 大阪対応END

            '要望番号1102 2012.06.11 修正START
            Dim loopFlg As Boolean = True
            '要望番号1102 2012.06.11 修正END

            '2012.03.18 大阪START
            '後続で値を追加したい為コメント化
            ''印刷フラグ更新用にデータセットを入替
            'prtflgDs.Merge(ds)
            '2012.03.18 大阪END

            outPutflg = dr.Item("OUTPUT_SHUBETU").ToString()     '印刷種別(LMH010Cの出力種別)

            'setBlc = getBLC(Convert.ToInt32(dr.Item("EDI_CUST_INDEX")))
            setBlc = New LMH010BLC

            '2012.03.18 大阪対応START
            Select Case outPutflg

                Case "01"  '入荷ＥＤＩ受信帳票
                    'ds = Me.PrintLMH560(dr)

                    ''ダウケミ(高石以外)の場合には、立会い伝票も併せて出力
                    ''LMH560IN,LMH561INにセット
                    'If dr.Item("EDI_CUST_INDEX").ToString().Equals("17") = True Then

                    '    prtBlc = New Com.Base.BaseBLC() {New LMH560BLC(), New LMH561BLC()}
                    '    setDs = New DataSet() {Me.PrintLMH560(dr), Me.PrintLMH561(dr)}

                    'Else
                    '    '上記でない場合は、受信伝票のみ
                    '    'LMH560INにセット
                    '    prtBlc = New Com.Base.BaseBLC() {New LMH560BLC()}
                    '    setDs = New DataSet() {Me.PrintLMH560(dr)}
                    'End If

                    Select Case ediCustIdx
                        Case "17"     'ダウケミ(大阪)

                            'ダウケミ(高石以外)の場合には、立会い伝票も併せて出力
                            'LMH560IN,LMH561INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH560BLC(), New LMH561BLC()}
                            setDs = New DataSet() {Me.PrintLMH560(dr), Me.PrintLMH561(dr)}

                        Case "18"       'ダウケミ(高石)
                            '上記でない場合は、受信伝票のみ
                            'LMH560INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH560BLC()}
                            setDs = New DataSet() {Me.PrintLMH560(dr)}

                        Case "13"       '日産物流(千葉)
                            '上記でない場合は、受信伝票のみ
                            'LMH563INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH563BLC()}
                            setDs = New DataSet() {Me.PrintLMH563(dr)}

                        Case Else

                            Return rtnDs

                    End Select

                Case "02" '入荷受信一覧表

                    Select Case ediCustIdx

                        Case "38", "1"    '浮間合成(大阪、岩槻)受信一覧表
                            dt.Rows(0).Item("BIKO_STR_1") = "AND DEL_KB <> '3'"
                            'LMH572INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH572BLC()}
                            '要望番号1102 2012.06.11 修正START
                            'setDs = New DataSet() {Me.PrintLMH572(dr)}
                            setDs = New DataSet() {Me.PrintLMH572(dt)}
                            loopFlg = False
                            '要望番号1102 2012.06.11 修正END

                        Case Else
                            Return rtnDs

                    End Select

                Case Else

            End Select

            '2012.03.18 大阪START
            '入替を行った後に、データセットを入れ替える
            '印刷フラグ更新用にデータセットを入替
            prtflgDs.Merge(ds)
            '2012.03.18 大阪START

            rtnDs = Me.PrintBlcFunc(prtBlc, setDs, ds)

            ' ''要望番号1007 2012.05.08 コメントSTART
            ''2012.03.19 修正START
            'If MyBase.IsMessageExist() = True Then
            '    Return rtnDs
            'End If
            ' ''要望番号1007 2012.05.08 コメントEND

            '要望番号1007 2012.05.08 修正START
            If dr.Item("PRTFLG").ToString().Equals("1") = False Then

                '印刷フラグの更新
                prtflgDs = MyBase.CallBLC(setBlc, "PrintFlagUpDate", prtflgDs)
                'If MyBase.GetResultCount = 0 Then
                '    MyBase.SetMessage(Nothing)
                'End If
            End If
            '2012.03.19 修正END
            '要望番号1007 2012.05.08 修正END

            'EDI印刷対象テーブルの追加(既に存在する場合は削除⇒追加)
            rtnDs = MyBase.CallBLC(setBlc, "DeleteInsertHEdiPrint", rtnDs)

            'プレビュー情報を設定
            If rtnDs.Tables(LMConst.RD) Is Nothing = False Then
                cnt = rtnDs.Tables(LMConst.RD).Rows.Count - 1
                For j As Integer = 0 To cnt
                    rtnDt.ImportRow(rtnDs.Tables(LMConst.RD).Rows(j))
                Next
            End If

            '要望番号1102 2012.06.11 修正START
            If loopFlg = False Then
                Return ds
            End If
            '要望番号1102 2012.06.11 修正END

        Next

        'Return rtnDs
        Return ds
        '要望番号1061 2012.05.15 修正END

    End Function

    ''' <summary>
    '''  入荷ＥＤＩ受信帳票(LMH560IN)
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH560(ByVal dr As DataRow) As DataSet

        ' 出荷ＥＤＩ受信帳票
        Dim PrmDs As DataSet = New LMH560DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH560IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        'LMH560INにLMH010_OUTPUTINの値を設定
        PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
        PrmDr("WH_CD") = dr.Item("WH_CD")
        PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
        PrmDr("CRT_DATE_FROM") = dr.Item("CRT_DATE_FROM")
        PrmDr("CRT_DATE_TO") = dr.Item("CRT_DATE_TO")
        PrmDr("EDI_CUST_INDEX") = dr.Item("EDI_CUST_INDEX")
        PrmDr("PRTFLG") = dr.Item("PRTFLG")
        PrmDr("RCV_NM_HED") = dr.Item("RCV_NM_HED")
        PrmDr("RCV_NM_DTL") = dr.Item("RCV_NM_DTL")
        PrmDr("INOUT_UMU_KB") = dr.Item("INOUT_UMU_KB")
        PrmDr("INOUT_KB") = dr.Item("INOUT_KB")
        '要望番号1007 2012.05.08 修正START
        PrmDr("OUTPUT_SHUBETU") = dr.Item("OUTPUT_SHUBETU")
        '要望番号1007 2012.05.08 修正END
        '要望番号1061 2012.05.15 追加START
        PrmDr("EDI_CTL_NO") = dr.Item("EDI_CTL_NO")
        PrmDr("ROW_NO") = dr.Item("ROW_NO")
        '要望番号1061 2012.05.15 追加END
        '2012.05.29 要望番号1077 追加START
        PrmDr("DENPYO_NO") = dr.Item("ORDER_NO")
        '2012.05.29 要望番号1077 追加END

        PrmDt.Rows.Add(PrmDr)

        Return PrmDs

    End Function

    ''' <summary>
    '''  立会い伝票(LMH561IN)
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH561(ByVal dr As DataRow) As DataSet

        ' 立会い伝票
        Dim PrmDs As DataSet = New LMH561DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH561IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        'LMH561INにLMH010_OUTPUTINの値を設定
        PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
        PrmDr("WH_CD") = dr.Item("WH_CD")
        PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
        PrmDr("CRT_DATE_FROM") = dr.Item("CRT_DATE_FROM")
        PrmDr("CRT_DATE_TO") = dr.Item("CRT_DATE_TO")
        PrmDr("EDI_CUST_INDEX") = dr.Item("EDI_CUST_INDEX")
        PrmDr("PRTFLG") = dr.Item("PRTFLG")
        PrmDr("RCV_NM_HED") = dr.Item("RCV_NM_HED")
        PrmDr("RCV_NM_DTL") = dr.Item("RCV_NM_DTL")
        PrmDr("INOUT_UMU_KB") = dr.Item("INOUT_UMU_KB")
        PrmDr("INOUT_KB") = dr.Item("INOUT_KB")
        '要望番号1007 2012.05.08 修正START
        PrmDr("OUTPUT_SHUBETU") = dr.Item("OUTPUT_SHUBETU")
        '要望番号1007 2012.05.08 修正END
        '要望番号1061 2012.05.15 追加START
        PrmDr("EDI_CTL_NO") = dr.Item("EDI_CTL_NO")
        PrmDr("ROW_NO") = dr.Item("ROW_NO")
        '要望番号1061 2012.05.15 追加END
        '2012.05.29 要望番号1077 追加START
        PrmDr("DENPYO_NO") = dr.Item("ORDER_NO")
        '2012.05.29 要望番号1077 追加END

        PrmDt.Rows.Add(PrmDr)

        Return PrmDs

    End Function

    ''' <summary>
    '''  入荷ＥＤＩ受信帳票(LMH563IN)
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>s
    ''' <remarks></remarks>
    Private Function PrintLMH563(ByVal dr As DataRow) As DataSet

        ' 入荷ＥＤＩ受信帳票
        Dim PrmDs As DataSet = New LMH563DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH563IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        'LMH563INにLMH010_OUTPUTINの値を設定
        PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
        PrmDr("WH_CD") = dr.Item("WH_CD")
        PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
        PrmDr("CRT_DATE_FROM") = dr.Item("CRT_DATE_FROM")
        PrmDr("CRT_DATE_TO") = dr.Item("CRT_DATE_TO")
        PrmDr("EDI_CUST_INDEX") = dr.Item("EDI_CUST_INDEX")
        PrmDr("PRTFLG") = dr.Item("PRTFLG")
        PrmDr("RCV_NM_HED") = dr.Item("RCV_NM_HED")
        PrmDr("RCV_NM_DTL") = dr.Item("RCV_NM_DTL")
        PrmDr("INOUT_UMU_KB") = dr.Item("INOUT_UMU_KB")
        PrmDr("INOUT_KB") = dr.Item("INOUT_KB")
        '要望番号1007 2012.05.08 修正START
        PrmDr("OUTPUT_SHUBETU") = dr.Item("OUTPUT_SHUBETU")
        '要望番号1007 2012.05.08 修正END
        '要望番号1061 2012.05.15 追加START
        PrmDr("EDI_CTL_NO") = dr.Item("EDI_CTL_NO")
        PrmDr("ROW_NO") = dr.Item("ROW_NO")
        '要望番号1061 2012.05.15 追加END
        '2012.05.29 要望番号1077 追加START
        PrmDr("DENPYO_NO") = dr.Item("ORDER_NO")
        '2012.05.29 要望番号1077 追加END

        PrmDt.Rows.Add(PrmDr)

        Return PrmDs

    End Function

    '2012.03.18 大阪対応START
    ''' <summary>
    '''  受信一覧表(LMH572IN)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH572(ByVal dt As DataTable) As DataSet

        '' 受信一覧表
        Dim PrmDs As DataSet = New LMH572DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH572IN")
        Dim PrmDr As DataRow

        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow

        For i As Integer = 0 To max

            dr = dt.Rows(i)
            PrmDr = PrmDt.NewRow()

            'LMH572INにLMH010_OUTPUTINの値を設定
            PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
            PrmDr("WH_CD") = dr.Item("WH_CD")
            PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
            PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
            PrmDr("CRT_DATE_FROM") = dr.Item("CRT_DATE_FROM")
            PrmDr("CRT_DATE_TO") = dr.Item("CRT_DATE_TO")
            PrmDr("EDI_CUST_INDEX") = dr.Item("EDI_CUST_INDEX")
            PrmDr("PRTFLG") = dr.Item("PRTFLG")
            PrmDr("RCV_NM_HED") = dr.Item("RCV_NM_HED")
            PrmDr("RCV_NM_DTL") = dr.Item("RCV_NM_DTL")
            PrmDr("INOUT_UMU_KB") = dr.Item("INOUT_UMU_KB")
            PrmDr("INOUT_KB") = dr.Item("INOUT_KB")
            '要望番号1007 2012.05.08 修正START
            PrmDr("OUTPUT_SHUBETU") = dr.Item("OUTPUT_SHUBETU")
            '要望番号1007 2012.05.08 修正END
            '要望番号1061 2012.05.15 追加START
            PrmDr("EDI_CTL_NO") = dr.Item("EDI_CTL_NO")
            PrmDr("ROW_NO") = dr.Item("ROW_NO")
            '要望番号1061 2012.05.15 追加END
            '2012.05.29 要望番号1077 追加START
            PrmDr("DENPYO_NO") = dr.Item("ORDER_NO")
            '2012.05.29 要望番号1077 追加END

            PrmDt.Rows.Add(PrmDr)

        Next

        Return PrmDs

    End Function

    'Private Function PrintLMH572(ByVal dr As DataRow) As DataSet

    '    '' 受信一覧表
    '    Dim PrmDs As DataSet = New LMH572DS()
    '    Dim PrmDt As DataTable = PrmDs.Tables("LMH572IN")
    '    Dim PrmDr As DataRow = PrmDt.NewRow()

    '    'LMH572INにLMH010_OUTPUTINの値を設定
    '    PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
    '    PrmDr("WH_CD") = dr.Item("WH_CD")
    '    PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
    '    PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
    '    PrmDr("CRT_DATE_FROM") = dr.Item("CRT_DATE_FROM")
    '    PrmDr("CRT_DATE_TO") = dr.Item("CRT_DATE_TO")
    '    PrmDr("EDI_CUST_INDEX") = dr.Item("EDI_CUST_INDEX")
    '    PrmDr("PRTFLG") = dr.Item("PRTFLG")
    '    PrmDr("RCV_NM_HED") = dr.Item("RCV_NM_HED")
    '    PrmDr("RCV_NM_DTL") = dr.Item("RCV_NM_DTL")
    '    PrmDr("INOUT_UMU_KB") = dr.Item("INOUT_UMU_KB")
    '    PrmDr("INOUT_KB") = dr.Item("INOUT_KB")
    '    '要望番号1007 2012.05.08 修正START
    '    PrmDr("OUTPUT_SHUBETU") = dr.Item("OUTPUT_SHUBETU")
    '    '要望番号1007 2012.05.08 修正END
    '    '要望番号1061 2012.05.15 追加START
    '    PrmDr("EDI_CTL_NO") = dr.Item("EDI_CTL_NO")
    '    PrmDr("ROW_NO") = dr.Item("ROW_NO")
    '    '要望番号1061 2012.05.15 追加END
    '    '2012.05.29 要望番号1077 追加START
    '    PrmDr("DENPYO_NO") = dr.Item("ORDER_NO")
    '    '2012.05.29 要望番号1077 追加END

    '    PrmDt.Rows.Add(PrmDr)

    '    Return PrmDs

    'End Function

    '2012.03.18 大阪対応END

    ''' <summary>
    '''  プレビューデータセット設定
    ''' </summary>
    ''' <param name="prtBlc"></param>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintBlcFunc(ByVal prtBlc As Com.Base.BaseBLC(), ByVal setDs As DataSet(), _
                                  ByVal ds As DataSet) As DataSet

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

            'rtnDs.Tables(LMConst.RD).ImportRow(rdPrevDt.Rows(i))

        Next

        '2012.05.14 Start修正
        ''2012.03.19 修正START
        'If rtnDs.Tables(LMConst.RD).Rows.Count = 0 Then
        '    Return rtnDs
        'End If
        ''2012.03.19 修正END

        ''上書きされたrtnDsのプレビュー情報を消去
        'rtnDs.Tables(LMConst.RD).Rows(0).Delete()

        ''プレビューDATATABLEをマージ
        'rtnDs.Tables(LMConst.RD).Merge(rdPrevDt)

        If rtnDs.Tables(LMConst.RD).Rows.Count = 0 _
         AndAlso rdPrevDt.Rows.Count = 0 Then
            Return rtnDs
        End If

        '上書きされたrtnDsのプレビュー情報を消去
        If rtnDs.Tables(LMConst.RD).Rows.Count <> 0 Then
            rtnDs.Tables(LMConst.RD).Rows(0).Delete()

        End If

        'プレビューDATATABLEをマージ
        rtnDs.Tables(LMConst.RD).Merge(rdPrevDt)
        '2012.05.14 End修正

        Return rtnDs

    End Function

#End Region

    '2012.03.03 大阪対応END

#Region "DataSet"

    ''' <summary>
    ''' 運賃INの値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetUnchinInDataSet(ByVal ds As DataSet) As DataSet

        Dim unchinDs As DataSet = New LMF800DS()
        Dim insRows As DataRow = unchinDs.Tables("LMF800IN").NewRow

        insRows.Item("WH_CD") = ds.Tables("LMH010_B_INKA_L").Rows(0).Item("NRS_WH_CD").ToString
        insRows.Item("NRS_BR_CD") = ds.Tables("LMH010_B_INKA_L").Rows(0).Item("NRS_BR_CD").ToString
        insRows.Item("UNSO_NO_L") = ds.Tables("LMH010_UNSO_L").Rows(0).Item("UNSO_NO_L").ToString

        'データセットに追加
        unchinDs.Tables("LMF800IN").Rows.Add(insRows)

        Return unchinDs

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。支払バッチ呼び出し
    ''' <summary>
    ''' 支払運賃INの値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetShiharaiInDataSet(ByVal ds As DataSet) As DataSet

        Dim unchinDs As DataSet = New LMF810DS()
        Dim insRows As DataRow = unchinDs.Tables("LMF810IN").NewRow

        insRows.Item("WH_CD") = ds.Tables("LMH010_B_INKA_L").Rows(0).Item("NRS_WH_CD").ToString
        insRows.Item("NRS_BR_CD") = ds.Tables("LMH010_B_INKA_L").Rows(0).Item("NRS_BR_CD").ToString
        insRows.Item("UNSO_NO_L") = ds.Tables("LMH010_UNSO_L").Rows(0).Item("UNSO_NO_L").ToString

        'データセットに追加
        unchinDs.Tables("LMF810IN").Rows.Add(insRows)

        Return unchinDs

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。支払バッチ呼び出し


#End Region

#Region "BLC設定処理"

    '▼▼▼二次
    ''' <summary>
    ''' BLC設定処理(入荷登録)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks>荷主ごとにBLCを選択する</remarks>
    Private Function getBlcInkaToroku(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC = Nothing

        Select Case DirectCast(ediIndex, LMH010BLF.EdiCustIndex)

            '2012.04.11 デュポン千葉→横浜移送(00089)追加
            '横浜：デュポン
            '大阪：デュポン
            '千葉：デュポン
            Case EdiCustIndex.Dupont00295_00 _
               , EdiCustIndex.Dupont00588_00 _
               , EdiCustIndex.Dupont00331_00 _
               , EdiCustIndex.Dupont00331_02 _
               , EdiCustIndex.Dupont00331_03 _
               , EdiCustIndex.Dupont00700_00 _
               , EdiCustIndex.Dupont00689_00 _
               , EdiCustIndex.Dupont00300_00 _
               , EdiCustIndex.Dupont00089_00 _
               , EdiCustIndex.Dupont00187_00 _
               , EdiCustIndex.Dupont00188_00 _
               , EdiCustIndex.Dupont00587_00 _
               , EdiCustIndex.Dupont00589_00 _
               , EdiCustIndex.Dupont00688_00

                Dim blc403 As LMH010BLC403 = New LMH010BLC403
                setBlc = blc403

                '名古屋：日本合成化学
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH010BLC601 = New LMH010BLC601
                setBlc = blc601

                '大阪：ダウケミ
                '大阪：ダウケミ(高石)
            Case EdiCustIndex.Dow00109_00 _
               , EdiCustIndex.DowTaka00109_01

                Dim blc202 As New LMH010BLC202
                setBlc = blc202

                '大阪：東邦化学  UPD 2017/02/24 Toho00347_00(千葉追加)
            Case EdiCustIndex.Toho00275_00 _
               , EdiCustIndex.Toho00347_00 _
               , EdiCustIndex.Toho00431_00

                Dim blc201 As New LMH010BLC201
                setBlc = blc201

                '大阪：浮間合成
                '岩槻：浮間合成
            Case EdiCustIndex.UkimaOsk00856_00 _
                , EdiCustIndex.UkimaSai00856_00

                Dim blc203 As New LMH010BLC203
                setBlc = blc203

                '千葉：日産物流
            Case EdiCustIndex.Nissan00145_00
                Dim blc104 As New LMH010BLC104
                setBlc = blc104

                '2012.04.26 追加START
                '千葉：日医工
            Case EdiCustIndex.Nik00171_00
                Dim blc101 As New LMH010BLC101
                setBlc = blc101
                '2012.04.26 追加END

                '2012.05,29 追加START
                '岩槻：住化カラー
            Case EdiCustIndex.Sumika00952_00
                Dim blc502 As New LMH010BLC502
                setBlc = blc502
                '2012.05,29 追加END

                '2012.08.01 追加START
                '千葉：富士フイルム
            Case EdiCustIndex.Fjf00195_00 _
                , EdiCustIndex.FjfTaka00195_00
                Dim blc103 As New LMH010BLC103
                setBlc = blc103
                '2012.08.01 追加END

                '2012.12.12 追加START
                '岩槻：ビーピー・カストロール
            Case EdiCustIndex.Bp00023_00
                Dim blc501 As New LMH010BLC501
                setBlc = blc501
                '2012.12.12 追加END

                '2013.03.07 追加START
                '千葉：テルモ
            Case EdiCustIndex.TrmChb00409_00
                Dim blc113 As New LMH010BLC113
                setBlc = blc113
                '2013.03.07 追加END

                '2013.08.30 要望番号2100 日立FN対応　追加START
            Case EdiCustIndex.DicItk10001_00 _
               , EdiCustIndex.DicGnm00076_00 _
               , EdiCustIndex.DicItk10007_00 _
               , EdiCustIndex.DicChb00010_00
                Dim blc205 As New LMH010BLC205
                setBlc = blc205
                '2013.08.30 要望番号2100 日立FN対応　追加END

                '大阪：ローム・アンド･ハース電子材料株式会社
            Case EdiCustIndex.Rome00061_00 _
                , EdiCustIndex.RomeYok00003_00
                Dim blc209 As New LMH010BLC209
                setBlc = blc209

                '2015.08.20 BYK セミEDI対応START
                '千葉：ビックケミー
            Case EdiCustIndex.Byk00266_00
                Dim blc102 As New LMH010BLC102
                setBlc = blc102
                '2015.08.20 BYK セミEDI対応END

#If True Then   'フィルメニッヒ(横浜) セミEDI対応 20160408 added inoue 
                '横浜：フィルメニッヒ
            Case EdiCustIndex.FirmeYok00004_00
                setBlc = New LMH010BLC404()
#End If
                'ADD 2017/07/05 インターコンチ（横浜）
            Case EdiCustIndex.ITC00125_00
                setBlc = New LMH010BLC405()

                'ADD 2017/07/25 エアウォーターゾル（千葉）
            Case EdiCustIndex.AWS00801_00
                setBlc = New LMH010BLC116()

                '2018/02/07 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 add start
            Case EdiCustIndex.Mimaki45741
                setBlc = New LMH010BLC122()
                '2018/02/07 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 add end

                '20190624 要望番号006280 add
                '大阪：ＡＧＣ若狭化学
            Case EdiCustIndex.AgcW00440
                setBlc = New LMH010BLC220()

#If True Then   'ADD 2020/05/18 007999   【LMS】セミEDI_ジョンソンエンドジョンソンJ&J_土気自動倉庫預かり_入荷・出荷新規
            Case EdiCustIndex.JJ15
                Dim blc151 As New LMH010BLC151
                setBlc = blc151
#End If
                '(千葉)シグマアルドリッチジャパン
            Case EdiCustIndex.SGM10
                Dim blc157 As New LMH010BLC157
                setBlc = blc157

                '(熊本)TSMC
            Case EdiCustIndex.TSMC75
                Dim blc164 As New LMH010BLC164
                setBlc = blc164

                '(大阪)テツタニ
            Case EdiCustIndex.Tetsutani
                Dim blc166 As New LMH010BLC166
                setBlc = blc166

                '(横浜)Rapidus
            Case EdiCustIndex.Rapidus40

                Dim blc611 As LMH010BLC611 = New LMH010BLC611
                setBlc = blc611

                '(土気)物産アニマルヘルス
            Case EdiCustIndex.BAH15

                Dim blc612 As LMH010BLC612 = New LMH010BLC612
                setBlc = blc612

            Case Else
                setBlc = New LMH010BLC

        End Select


        Return setBlc

    End Function

    ''' <summary>
    ''' BLC設定処理(実績作成)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks>荷主ごとにBLCを選択する</remarks>
    Private Function getBlcJisseki(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC = Nothing

        Select Case DirectCast(ediIndex, LMH010BLF.EdiCustIndex)

            '2012.04.11 デュポン千葉→横浜移送(00089)追加
            '横浜：デュポン
            '大阪：デュポン
            '千葉：デュポン
            Case EdiCustIndex.Dupont00295_00 _
               , EdiCustIndex.Dupont00588_00 _
               , EdiCustIndex.Dupont00331_00 _
               , EdiCustIndex.Dupont00331_02 _
               , EdiCustIndex.Dupont00331_03 _
               , EdiCustIndex.Dupont00700_00 _
               , EdiCustIndex.Dupont00689_00 _
               , EdiCustIndex.Dupont00300_00 _
               , EdiCustIndex.Dupont00089_00 _
               , EdiCustIndex.Dupont00187_00 _
               , EdiCustIndex.Dupont00188_00 _
               , EdiCustIndex.Dupont00587_00 _
               , EdiCustIndex.Dupont00588_00 _
               , EdiCustIndex.Dupont00589_00

                Dim blc403 As LMH010BLC403 = New LMH010BLC403
                setBlc = blc403

                '名古屋：日本合成化学
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH010BLC601 = New LMH010BLC601
                setBlc = blc601

                '大阪：ダウケミ
            Case EdiCustIndex.Dow00109_00

                Dim blc202 As New LMH010BLC202
                setBlc = blc202

                '千葉：日産物流
            Case EdiCustIndex.Nissan00145_00
                Dim blc104 As New LMH010BLC104
                setBlc = blc104

                '2012.04.26 追加START
                '千葉：日医工
            Case EdiCustIndex.Nik00171_00
                Dim blc101 As New LMH010BLC101
                setBlc = blc101
                '2012.04.26 追加END

                '2012.05.30 追加START
                '岩槻：住化カラー
            Case EdiCustIndex.Sumika00952_00
                Dim blc502 As New LMH010BLC502
                setBlc = blc502
                '2012.05.30 追加END

                '2012.12.12 追加START
                '岩槻：ビーピー・カストロール
            Case EdiCustIndex.Bp00023_00
                Dim blc501 As New LMH010BLC501
                setBlc = blc501
                '2012.12.12 追加END

                '2013.03.07 追加START
                '千葉：テルモ
            Case EdiCustIndex.TrmChb00409_00
                Dim blc113 As New LMH010BLC113
                setBlc = blc113
                '2013.03.07 追加END

                '横浜：Rapidus
            Case EdiCustIndex.Rapidus40
                Dim blc611 As New LMH010BLC611
                setBlc = blc611

                '土気：物産アニマルヘルス
            Case EdiCustIndex.BAH15
                Dim blc612 As New LMH010BLC612
                setBlc = blc612

            Case Else
                setBlc = New LMH010BLC

        End Select


        Return setBlc

    End Function

    ''' <summary>
    ''' BLC設定処理(EDI取消)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks>荷主ごとにBLCを選択する</remarks>
    Private Function getBlcEditorikesi(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        Select Case DirectCast(ediIndex, LMH010BLF.EdiCustIndex)

            '2012.04.11 デュポン千葉→横浜移送(00089)追加
            '横浜：デュポン
            '大阪：デュポン
            '千葉：デュポン
            Case EdiCustIndex.Dupont00295_00 _
               , EdiCustIndex.Dupont00588_00 _
               , EdiCustIndex.Dupont00331_00 _
               , EdiCustIndex.Dupont00331_02 _
               , EdiCustIndex.Dupont00331_03 _
               , EdiCustIndex.Dupont00700_00 _
               , EdiCustIndex.Dupont00689_00 _
               , EdiCustIndex.Dupont00300_00 _
               , EdiCustIndex.Dupont00089_00 _
               , EdiCustIndex.Dupont00187_00 _
               , EdiCustIndex.Dupont00188_00 _
               , EdiCustIndex.Dupont00587_00 _
               , EdiCustIndex.Dupont00589_00 _
               , EdiCustIndex.Dupont00688_00

                Dim blc403 As LMH010BLC403 = New LMH010BLC403
                setBlc = blc403

                '名古屋：日本合成化学
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH010BLC601 = New LMH010BLC601
                setBlc = blc601

            Case Else
                setBlc = New LMH010BLC

        End Select


        Return setBlc

    End Function

    ''' <summary>
    ''' BLC設定処理(取込)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks>荷主ごとにBLCを選択する</remarks>
    Private Function getBlcSemiEdiTorikomi(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC = Nothing

        Select Case DirectCast(ediIndex, LMH010BLF.EdiCustIndex)

            '大阪：ローム・アンド･ハース電子材料株式会社
            Case EdiCustIndex.Rome00061_00 _
                , EdiCustIndex.RomeYok00003_00
                Dim blc209 As New LMH010BLC209
                setBlc = blc209

                '2015.08.20 BYK セミEDI対応START
                '千葉：ビックケミー
            Case EdiCustIndex.Byk00266_00
                Dim blc102 As New LMH010BLC102
                setBlc = blc102
                '2015.08.20 BYK セミEDI対応END
#If True Then   'フィルメニッヒ(横浜) セミEDI対応 20160408 added inoue 
                '横浜：フィルメニッヒ
            Case EdiCustIndex.FirmeYok00004_00
                setBlc = New LMH010BLC404()
#End If
                'ADD 2017/07/05 インターコンチ（横浜）
            Case EdiCustIndex.ITC00125_00
                setBlc = New LMH010BLC405()

                'ADD 2017/07/25 エアウォーターゾル（千葉）
            Case EdiCustIndex.AWS00801_00
                setBlc = New LMH010BLC116()

                '2018/02/07 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 add start
            Case EdiCustIndex.Mimaki45741
                setBlc = New LMH010BLC122()
                '2018/02/07 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 add end

                '20190624 要望番号006280 add
                '大阪：ＡＧＣ若狭化学
            Case EdiCustIndex.AgcW00440
                setBlc = New LMH010BLC220()


#If True Then   'ADD 2020/05/14 007999   【LMS】セミEDI_ジョンソンエンドジョンソンJ&J_土気自動倉庫預かり_入荷・出荷新規
            Case EdiCustIndex.JJ15
                Dim blc151 As New LMH010BLC151
                setBlc = blc151
#End If
                '(千葉)シグマアルドリッチジャパン
            Case EdiCustIndex.SGM10
                Dim blc157 As New LMH010BLC157
                setBlc = blc157

                '(大阪)テツタニ
            Case EdiCustIndex.Tetsutani
                Dim blc166 As New LMH010BLC166
                setBlc = blc166

                '名古屋：日本合成化学→三菱ケミカル→三菱ケミカル物流(MCLC)
            Case EdiCustIndex.Ncgo32516_00
                Dim blc601 As LMH010BLC601 = New LMH010BLC601
                setBlc = blc601

            Case Else
                setBlc = New LMH010BLC

        End Select


        Return setBlc

    End Function

    ''' <summary>
    ''' BLC設定処理(実績取消)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks>荷主ごとにBLCを選択する</remarks>
    Private Function getBlcJissekiTorikesi(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        Select Case DirectCast(ediIndex, LMH010BLF.EdiCustIndex)

            '2012.04.11 デュポン千葉→横浜移送(00089)追加
            '横浜：デュポン
            '大阪：デュポン
            '千葉：デュポン
            Case EdiCustIndex.Dupont00295_00 _
               , EdiCustIndex.Dupont00588_00 _
               , EdiCustIndex.Dupont00331_00 _
               , EdiCustIndex.Dupont00331_02 _
               , EdiCustIndex.Dupont00331_03 _
               , EdiCustIndex.Dupont00700_00 _
               , EdiCustIndex.Dupont00689_00 _
               , EdiCustIndex.Dupont00300_00 _
               , EdiCustIndex.Dupont00089_00 _
               , EdiCustIndex.Dupont00187_00 _
               , EdiCustIndex.Dupont00188_00 _
               , EdiCustIndex.Dupont00587_00 _
               , EdiCustIndex.Dupont00589_00 _
               , EdiCustIndex.Dupont00688_00

                Dim blc403 As LMH010BLC403 = New LMH010BLC403
                setBlc = blc403

                '名古屋：日本合成化学
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH010BLC601 = New LMH010BLC601
                setBlc = blc601

            Case Else
                setBlc = New LMH010BLC

        End Select

        Return setBlc

    End Function

    ''' <summary>
    ''' BLC設定処理(EDI取消⇒未登録)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks>荷主ごとにBLCを選択する</remarks>
    Private Function getBlcTorikesiMitouroku(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        Select Case DirectCast(ediIndex, LMH010BLF.EdiCustIndex)

            '2012.04.11 デュポン千葉→横浜移送(00089)追加
            '横浜：デュポン
            '大阪：デュポン
            '千葉：デュポン
            Case EdiCustIndex.Dupont00295_00 _
               , EdiCustIndex.Dupont00588_00 _
               , EdiCustIndex.Dupont00331_00 _
               , EdiCustIndex.Dupont00331_02 _
               , EdiCustIndex.Dupont00331_03 _
               , EdiCustIndex.Dupont00700_00 _
               , EdiCustIndex.Dupont00689_00 _
               , EdiCustIndex.Dupont00300_00 _
               , EdiCustIndex.Dupont00089_00 _
               , EdiCustIndex.Dupont00187_00 _
               , EdiCustIndex.Dupont00188_00 _
               , EdiCustIndex.Dupont00587_00 _
               , EdiCustIndex.Dupont00589_00 _
               , EdiCustIndex.Dupont00688_00

                Dim blc403 As LMH010BLC403 = New LMH010BLC403
                setBlc = blc403

                '名古屋：日本合成化学
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH010BLC601 = New LMH010BLC601
                setBlc = blc601

            Case Else
                setBlc = New LMH010BLC

        End Select


        Return setBlc

    End Function

    ''' <summary>
    ''' BLC設定処理(実績送信済⇒送信待)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks>荷主ごとにBLCを選択する</remarks>
    Private Function getBlcSousinSousinmi(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        Select Case DirectCast(ediIndex, LMH010BLF.EdiCustIndex)

            '2012.04.11 デュポン千葉→横浜移送(00089)追加
            '横浜：デュポン
            '大阪：デュポン
            '千葉：デュポン
            Case EdiCustIndex.Dupont00295_00 _
               , EdiCustIndex.Dupont00588_00 _
               , EdiCustIndex.Dupont00331_00 _
               , EdiCustIndex.Dupont00331_02 _
               , EdiCustIndex.Dupont00331_03 _
               , EdiCustIndex.Dupont00700_00 _
               , EdiCustIndex.Dupont00689_00 _
               , EdiCustIndex.Dupont00300_00 _
               , EdiCustIndex.Dupont00089_00 _
               , EdiCustIndex.Dupont00187_00 _
               , EdiCustIndex.Dupont00188_00 _
               , EdiCustIndex.Dupont00587_00 _
               , EdiCustIndex.Dupont00589_00 _
               , EdiCustIndex.Dupont00688_00

                Dim blc403 As LMH010BLC403 = New LMH010BLC403
                setBlc = blc403

                '名古屋：日本合成化学
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH010BLC601 = New LMH010BLC601
                setBlc = blc601

            Case Else
                setBlc = New LMH010BLC

        End Select


        Return setBlc

    End Function

    ''' <summary>
    ''' BLC設定処理(実績作成済⇒実績未)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks>荷主ごとにBLCを選択する</remarks>
    Private Function getBlcSakuseizumiJissekimi(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        Select Case DirectCast(ediIndex, LMH010BLF.EdiCustIndex)

            '2012.04.11 デュポン千葉→横浜移送(00089)追加
            '横浜：デュポン
            '大阪：デュポン
            '千葉：デュポン
            Case EdiCustIndex.Dupont00295_00 _
               , EdiCustIndex.Dupont00588_00 _
               , EdiCustIndex.Dupont00331_00 _
               , EdiCustIndex.Dupont00331_02 _
               , EdiCustIndex.Dupont00331_03 _
               , EdiCustIndex.Dupont00700_00 _
               , EdiCustIndex.Dupont00689_00 _
               , EdiCustIndex.Dupont00300_00 _
               , EdiCustIndex.Dupont00089_00 _
               , EdiCustIndex.Dupont00187_00 _
               , EdiCustIndex.Dupont00188_00 _
               , EdiCustIndex.Dupont00587_00 _
               , EdiCustIndex.Dupont00589_00 _
               , EdiCustIndex.Dupont00688_00

                Dim blc403 As LMH010BLC403 = New LMH010BLC403
                setBlc = blc403

                '名古屋：日本合成化学
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH010BLC601 = New LMH010BLC601
                setBlc = blc601

            Case Else
                setBlc = New LMH010BLC

        End Select

        Return setBlc

    End Function

    ''' <summary>
    ''' BLC設定処理(実績送信済⇒実績未)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks>荷主ごとにBLCを選択する</remarks>
    Private Function getBlcSousinzumiJissekimi(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        Select Case DirectCast(ediIndex, LMH010BLF.EdiCustIndex)

            '2012.04.11 デュポン千葉→横浜移送(00089)追加
            '横浜：デュポン
            '大阪：デュポン
            '千葉：デュポン
            Case EdiCustIndex.Dupont00295_00 _
               , EdiCustIndex.Dupont00588_00 _
               , EdiCustIndex.Dupont00331_00 _
               , EdiCustIndex.Dupont00331_02 _
               , EdiCustIndex.Dupont00331_03 _
               , EdiCustIndex.Dupont00700_00 _
               , EdiCustIndex.Dupont00689_00 _
               , EdiCustIndex.Dupont00300_00 _
               , EdiCustIndex.Dupont00089_00 _
               , EdiCustIndex.Dupont00187_00 _
               , EdiCustIndex.Dupont00188_00 _
               , EdiCustIndex.Dupont00587_00 _
               , EdiCustIndex.Dupont00589_00 _
               , EdiCustIndex.Dupont00688_00

                Dim blc403 As LMH010BLC403 = New LMH010BLC403
                setBlc = blc403

                '名古屋：日本合成化学
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH010BLC601 = New LMH010BLC601
                setBlc = blc601

            Case Else
                setBlc = New LMH010BLC

        End Select


        Return setBlc

    End Function

    ''' <summary>
    ''' BLC設定処理(入荷取消⇒未登録)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks>荷主ごとにBLCを選択する</remarks>
    Private Function getBlcMitouroku(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        Select Case DirectCast(ediIndex, LMH010BLF.EdiCustIndex)

            '2012.04.11 デュポン千葉→横浜移送(00089)追加
            '横浜：デュポン
            '大阪：デュポン
            '千葉：デュポン
            Case EdiCustIndex.Dupont00295_00 _
               , EdiCustIndex.Dupont00588_00 _
               , EdiCustIndex.Dupont00331_00 _
               , EdiCustIndex.Dupont00331_02 _
               , EdiCustIndex.Dupont00331_03 _
               , EdiCustIndex.Dupont00700_00 _
               , EdiCustIndex.Dupont00689_00 _
               , EdiCustIndex.Dupont00300_00 _
               , EdiCustIndex.Dupont00089_00 _
               , EdiCustIndex.Dupont00187_00 _
               , EdiCustIndex.Dupont00188_00 _
               , EdiCustIndex.Dupont00587_00 _
               , EdiCustIndex.Dupont00589_00 _
               , EdiCustIndex.Dupont00688_00

                Dim blc403 As LMH010BLC403 = New LMH010BLC403
                setBlc = blc403

                '名古屋：日本合成化学
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH010BLC601 = New LMH010BLC601
                setBlc = blc601

            Case Else
                setBlc = New LMH010BLC

        End Select

        Return setBlc

    End Function
    '▲▲▲二次

    ''' <summary>
    ''' BLC設定処理(M品振替出荷)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks>荷主ごとにBLCを選択する</remarks>
    Private Function getBlcTransferItemM(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC = Nothing

        Select Case DirectCast(ediIndex, LMH010BLF.EdiCustIndex)

            '名古屋：日本合成化学
            Case EdiCustIndex.Ncgo32516_00
                setBlc = New LMH010BLC601
        End Select


        Return setBlc

    End Function

#End Region

    '2012.02.25 大阪対応 START
#Region "入荷取消⇒未登録データセット"

    ''' <summary>
    ''' 入荷取消⇒未登録データセット
    ''' </summary>
    ''' <param name="prmDr"></param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDataTorikesiMitouroku(ByVal prmDr As DataRow, ByVal ds As DataSet, ByVal eventShubetu As String, ByVal rowNo As Integer)

        Dim setDr As DataRow
        'LMH010IN
        setDr = ds.Tables("LMH010INOUT").NewRow()

        setDr("NRS_BR_CD") = prmDr("NRS_BR_CD")
        setDr("EDI_CTL_NO") = prmDr("EDI_CTL_NO")
        setDr("INKA_CTL_NO_L") = prmDr("INKA_CTL_NO_L")
        setDr("SYS_UPD_DATE") = prmDr("SYS_UPD_DATE")
        setDr("SYS_UPD_TIME") = prmDr("SYS_UPD_TIME")
        setDr("ROW_NO") = rowNo
        setDr("RCV_NM_HED") = prmDr("RCV_NM_HED")
        setDr("RCV_NM_DTL") = prmDr("RCV_NM_DTL")
        setDr("RCV_NM_EXT") = prmDr("RCV_NM_EXT")
        setDr("SND_NM") = prmDr("SND_NM")
        setDr("EDI_CUST_INOUTFLG") = prmDr("EDI_CUST_INOUTFLG")
        setDr("EDI_CUST_INDEX") = prmDr("EDI_CUST_INDEX") '追加 2012.11.22

        ds.Tables("LMH010INOUT").Rows.Add(setDr)

        'LMH010_INKAEDI_L
        setDr = ds.Tables("LMH010_INKAEDI_L").NewRow()

        setDr("NRS_BR_CD") = prmDr("NRS_BR_CD")
        setDr("EDI_CTL_NO") = prmDr("EDI_CTL_NO")
        setDr("SYS_UPD_DATE") = prmDr("SYS_UPD_DATE")
        setDr("SYS_UPD_TIME") = prmDr("SYS_UPD_TIME")
        setDr("OUT_FLAG") = "0"

        ds.Tables("LMH010_INKAEDI_L").Rows.Add(setDr)

        'LMH010_INKAEDI_M
        setDr = ds.Tables("LMH010_INKAEDI_M").NewRow()

        setDr("NRS_BR_CD") = prmDr("NRS_BR_CD")
        setDr("EDI_CTL_NO") = prmDr("EDI_CTL_NO")

        ds.Tables("LMH010_INKAEDI_M").Rows.Add(setDr)

        'LMH010_RCV_HED
        setDr = ds.Tables("LMH010_RCV_HED").NewRow()

        setDr("NRS_BR_CD") = prmDr("NRS_BR_CD")
        setDr("EDI_CTL_NO") = prmDr("EDI_CTL_NO")
        setDr("SYS_UPD_DATE") = prmDr("RCV_UPD_DATE")
        setDr("SYS_UPD_TIME") = prmDr("RCV_UPD_TIME")

        ds.Tables("LMH010_RCV_HED").Rows.Add(setDr)

        'LMH010_RCV_DTL
        setDr = ds.Tables("LMH010_RCV_DTL").NewRow()

        setDr("NRS_BR_CD") = prmDr("NRS_BR_CD")
        setDr("EDI_CTL_NO") = prmDr("EDI_CTL_NO")

        ds.Tables("LMH010_RCV_DTL").Rows.Add(setDr)

        setDr = ds.Tables("LMH010_JUDGE").NewRow()
        setDr("EVENT_SHUBETSU") = eventShubetu
        ds.Tables("LMH010_JUDGE").Rows.Add(setDr)

    End Sub
    '2012.02.25 大阪対応 END

#End Region


#Region "M品一括振替"


    ''' <summary>
    ''' 実行(M品振替出荷処理)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>
    ''' </returns>
    ''' <remarks>
    ''' </remarks>
    Private Function TransferCondM(ByVal ds As DataSet) As DataSet


        Dim isSuccess As Boolean = False
        Dim inoutTable As DataTable = ds.Tables(TABLE_NM_IN)

        Dim ediIndex As Integer = Convert.ToInt32(inoutTable.Rows(0)("EDI_CUST_INDEX"))

        Dim execBLC As Base.BLC.LMBaseBLC = Me.getBlcTransferItemM(ediIndex)
        If (execBLC Is Nothing) Then

            Dim custCdLM As String = String.Concat(inoutTable.Rows(0)("CUST_CD_L").ToString() _
                                                 , inoutTable.Rows(0)("CUST_CD_M").ToString())


            Me.SetMessage("E618", {custCdLM})
            Return ds
        End If


        Using clone As DataSet = ds.Clone()

            For Each row As DataRow In inoutTable.Rows()

                clone.Clear()
                clone.Tables(TABLE_NM_IN).ImportRow(row)
                clone.Tables(TABLE_NM_JUDGE_IN).Merge(ds.Tables(TABLE_NM_JUDGE_IN))

                'メッセージクリア
                MyBase.SetMessage(Nothing)

                ' 出荷データ作成
                clone.Merge(MyBase.CallBLC(execBLC, "SelectOutkaDataCondM", clone))
                If (MyBase.IsMessageExist) Then
                    Continue For
                End If

                'トランザクション開始
                Using scope As TransactionScope = MyBase.BeginTransaction()

                    MyBase.CallBLC(execBLC, "InsertOutkaCondM", clone)

                    If (MyBase.IsMessageExist = False) Then

                        'エラーが無ければCommit
                        MyBase.CommitTransaction(scope)

                    End If
                End Using
            Next

        End Using

        MyBase.SetMessage(Nothing)

        Return ds

    End Function
#End Region


End Class
