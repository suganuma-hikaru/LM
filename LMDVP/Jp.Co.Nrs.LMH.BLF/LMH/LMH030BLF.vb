' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030BLF : EDI出荷検索
'  作  成  者       :  umano
' ==========================================================================
Option Strict On
Option Explicit On

Imports System.Transactions
Imports System.Linq
Imports System.Collections
Imports System.Collections.Generic
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH030BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Const"

    Private Const FLG_SHORIZUMI As String = "01"

    ''' <summary>
    ''' INテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMH030INOUT"

    ''' <summary>
    ''' INテーブル名(出力ボタン押下時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUTPUTIN As String = "LMH030_OUTPUTIN"



    ''' <summary>
    ''' 出力種別
    ''' </summary>
    ''' <remarks></remarks>
    Class OUTPUT_SHUBETU

        ''' <summary>
        ''' EDI送り状_BPカストロール(日通)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const EDI_INVOICE_BP_NIPPON_EXPRESS As String = "17"

        ''' <summary>
        ''' EDI納品書_日本合成
        ''' </summary>
        ''' <remarks></remarks>
        Public Const EDI_DELIVERY_NOTE_NICHIGO As String = "18"
    End Class


#End Region

#Region "EDI荷主INDEX"
    'イベント種別
    Public Enum EdiCustIndex As Integer

        StanderdEdition = 0             '標準EDI荷主
        Sakura00237_00 = 1              'サクラファインテック(横浜)
        Sakura00660_00 = 114            'サクラファインテック(千葉)  ADD 2017/04/07
        Ncgo32516_00 = 57               '日本合成化学(名古屋)
        Dupont00089_00 = 18             'デュポン(テフロン)(千葉)→(横浜)移送  '2012.04.11 ADD
        Dupont00295_00 = 37             'デュポン(横浜)
        Dupont00331_00 = 87             'デュポン(DCSE)(横浜)
        Dupont00331_02 = 88             'デュポン(ABS)(横浜)
        Dupont00331_03 = 90             'デュポン()(横浜)
        Dupont00588_00 = 89             'デュポン(SFTP塗料)(横浜)
        Dupont00300_00 = 36             'デュポン(EP:大阪)
        Dupont00689_00 = 81             'デュポン(PVFM:大阪)
        Dupont00700_00 = 86             'デュポン(DCSE:大阪)
        DupontChb00187_00 = 47          'デュポン(ブタサイト:千葉)
        DupontChb00188_00 = 35          'デュポン(EP:千葉)
        DupontChb00587_00 = 76          'デュポン(農業:千葉)
        DupontChb00589_00 = 60          'デュポン(特殊化学品:千葉)
        DupontChb00688_00 = 79          'デュポン(電子材料事業:千葉)
        DupontChb00689_00 = 80          'デュポン(PVFM:千葉)

        Dsp00293_00 = 82                '大日本住友製薬
        Dspah08251_00 = 85              '大日本住友製薬(動物薬)
        Toho00275_00 = 65               '東邦化学(大阪)
        Toho00347_00 = 111              '東邦化学(千葉)　'長浦倉庫/袖ヶ浦倉庫/市原倉庫  ADD 2017/02/23
        Toho00431_00 = 162              '東邦化学(群馬)
        UkimaOsk00856_00 = 91           '浮間合成(大阪)
        Mitui00369_00 = 17              '三井化学(大阪)
        Mitui00375_00 = 21              '三井化学(日本特殊塗料)(大阪)
        Dow00109_00 = 43                'ダウケミ(大阪)
        DowTaka00109_01 = 44            'ダウケミ(大阪・高石)
        DicOsk00010_00 = 28             'ディック（大阪)

        Jc31022_00 = 34                 'ジャパンコンポジット（大阪)
        Jc31022_01 = 42                 'ジャパンコンポジット（大阪)大泰化工
        Aika31023_00 = 40               'アイカ工業（大阪)
        Nik00171_00 = 92                '日医工(千葉) '2012.05.01 ADD

        UkimaItk00856_00 = 3            '（岩槻）浮間合成
        Godo00950_00 = 4                '（岩槻）ゴードー溶剤
        Smk00952_00 = 6                 '（岩槻）住化カラー

        Dns20000_00 = 9                 '（岩槻）大日精化（東京製造）
        Dns20000_01 = 10                '（岩槻）大日精化（化成品）
        Dns20000_02 = 24                '（岩槻）大日精化（洗顔２課）

        DicItk00899_00 = 11             '（岩槻）ディック物流埼玉
        DicItk00899_01 = 41             '（岩槻）ディック物流埼玉
        DicItk10001_00 = 5              '（岩槻）ディック物流春日部
        DicItk10002_00 = 8              '（岩槻）ディック物流千葉－岩槻分
        DicItk10003_00 = 13             '（岩槻）ディック物流埼玉
        DicItk10007_00 = 39             '（岩槻）ディック物流東京営業所
        DicItk10008_00 = 12             '（岩槻）ディック物流館林
        DicItk10009_00 = 68             '（岩槻）ディック物流埼玉リナブルー
        'DicItkXXXXX_XX = ?              '（岩槻）ディック（荷主特定不能分）
        DicItk10005_00 = 7              '（岩槻）ディック共同配送（505倉庫）

        DicKkb10001_00 = 62             '（春日部）DIC春日部
        DicKkb10001_03 = 63             '（春日部）DIC春日部顔料
        DicKkb10012_00 = 69             '（春日部）DIC春日部関東工場
        DicKkb10013_00 = 64             '（春日部）DIC春日部他社物流

        KtkKkb10009_00 = 67             '（春日部）関塗工（大宝）

        SnzGnm00021_00 = 58             '（群馬）篠崎運輸

        MituiGnm00001_00 = 16           '（群馬）三井化学

        GodoGnm00026_00 = 58            '（群馬）ゴードー溶剤

        DicGnm00039_00 = 31             '（群馬）ディック物流群馬日パケ
        DicGnm00072_00 = 26             '（群馬）ディック物流群馬トートタンク
        DicGnm00076_00 = 25             '（群馬）ディック物流群馬

        DicChb00010_00 = 102            '（千葉）ディック物流千葉

        BykChb00266_00 = 93             '（千葉）ビックケミー
        BykChb00266_01 = 94             '（千葉）ビックケミー(テツタニ)
        BykChb00266_02 = 95             '（千葉）ビックケミー(長瀬)
        '2013.07.30 追加START
        BykChb00729_00 = 100            '（千葉）ビックケミー(エカルト)
        '2013.07.30 追加END

        FjfChb00195_00 = 96             '（千葉）富士フイルム
        JtChb00444_00 = 70              '（千葉）ジェイティ物流
        MhmChb00117_00 = 78             '（千葉）美浜株式会社
        LnzChb00182_00 = 84             '（千葉）ロンザジャパン
        SmkChb00002_00 = 72             '（千葉）住化カラー(市原)
        SmkChb00404_00 = 73             '（千葉）住化カラー(市原)
        MituiChb00456_00 = 45           '（千葉）三井化学
        NsnChb00145_00 = 32             '（千葉）日産物流
        UtiChb00625_00 = 22             '（千葉）ユーティーアイ
        TorChb00041_00 = 23             '（千葉）東レダウ
        TorYok00266_00 = 131             '（横浜）東レダウ ADD 2018/01/05  UPD 2018/09/03 121 → 131 依頼番号 : 002357   【LMS】千葉ITWセミEDI_不具合
        TorChb00637_00 = 59             '（千葉）日通（東レダウ）

        AshChb00070_00 = 61             '（千葉）旭化成ケミカルズ
        AshChb00071_00 = 33             '（千葉）旭化成イーマテリアルズ

        SnkHns10005_00 = 97             '（本社）センコー

        BP00023_00 = 77                 '（岩槻）ビーピー・カストロール

        NksOsk33224_00 = 98             '（大阪）日興産業

        ChissoChb00067_00 = 2           '（千葉）チッソ

        DicChbYuso10010_00 = 46         '（千葉）ディック千葉輸送

        HonChb00630_00 = 48                '（千葉）ハネウェル（市原：市原）
        HonChb00632_00 = 56                '（千葉）ハネウェル（市原：市原Ｂ＆Ｊ）
        HonChb10630_00 = 49                '（千葉）ハネウェル（大阪：兵機）
        HonChb20630_00 = 50                '（千葉）ハネウェル（名古屋：由良）
        HonChb30630_00 = 51                '（千葉）ハネウェル（北海道：三和）
        HonChb40630_00 = 52                '（千葉）ハネウェル（九州：博多）
        HonChb50630_00 = 53                '（千葉）ハネウェル（横浜：舟津） 

        TrmChb00409_00 = 99                 'テルモ(千葉)                    '2013.03.18 ADD

        AtsChb00750_00 = 101             'アクタス(千葉)

        MrcChb00097_00 = 103             'Ｍ・Ｒ・Ｃデュポン(千葉)           '2015.06.09 ADD

        SmkByr39548_00 = 104             '住化バイエル(プロ推)               '2015.06.11 住化バイエル ADD

        KrtOsk39941_00 = 105             '協立化学(大阪)                     '2015.06.17 協立化学 ADD

        MrwKyu46065_00 = 106             '丸和バイオ(九州)                   '2015.07.21 丸和バイオ ADD

        KrtEdiInJapan = 107              ' 協立化学(国内向け:大阪,横浜,千葉) '2015.11.17 協立化学 ADD

        MaruwaYoko00330_00 = 109        '丸和(横浜)   ADD 2016/09/14 

        est00784_00 = 110               'エストコミュ(千葉)   ADD 2017/01/23

        angfa10 = 113                   'アンファ(千葉)   ADD 2017/03/21　　CUST_CD　10-00900

        AWS00801_00 = 119               'エアウォーターゾル(千葉)   ADD 2017/07/28

        ITC00125_00 = 120               'インターコンチ(横浜)   ADD 2017/08/25

        '2017/11/28 Annen セミEDI_千葉ITW_新規登録対応 add start
        ITW00750_00 = 121               '（大阪ITW）
        '2017/11/28 Annen セミEDI_千葉ITW_新規登録対応 add end
        '2018/01/26 Annen 001007 【LMS】_セミEDI開発_片山ナルコ add start
        NRK00486_00 = 122
        NRK00486_01 = 123
        '2018/01/26 Annen 001007 【LMS】_セミEDI開発_片山ナルコ add start
        '2017/12/18 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add start
        GKC00456_00 = 130               'DSP五協フード＆ケミカル株式会社対応（横浜）（CSV出力）
        GKE00456_00 = 131               'DSP五協フード＆ケミカル株式会社対応（横浜）（EXCEL出力）
        '2017/12/18 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add start
        '2018/02/13 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 add start
        Mimaki45741 = 124
        '2018/02/13 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 add end
        TORAY00951_00 = 125             '千葉東レ
        Rome00061_00 = 135              'ローム(横浜)    ADD 2018/11/07 要望番号002046
        DDP00050_00 = 135              'DDP(横浜)    ADD 2020/07/27 014220   【LMS】DDPスペシャリティ_セミEDI取込機能追加
        Rome00061_01 = 136              'ローム(大阪)    ADD 2022/10/28 要望番号033290
#If True Then   ' ADD 2019/01/21 依頼番号 : 004188   【LMS】東レDOW・デュポン分社化EDI対応
        DowChb00817_00 = 137             '（千葉）ダウ・東レ株式会社    　
        DowYok00155_00 = 138             '（横浜）ダウ・東レ株式会社    　
#End If
        '2019.06.27 要望番号006280 add start
        AgcW00440 = 140                     '(大阪)ＡＧＣ若狭化学
        '2019.06.27 要望番号006280 add end
        '2019.09.17 要望番号006984 add start
        CJC00787 = 141                      '(千葉)コーヴァンス・ジャパン株式会社
        '2019.09.17 要望番号006984 end start
#If True Then   'ADD 2020/04/15 007999   【LMS】セミEDI_ジョンソンエンドジョンソンJ&J_土気自動倉庫預かり_入荷・出荷新規
        JJ15 = 151                '（土気自動倉庫）ジョンソンエンドジョンソンJ&J_

#End If
        MercChb00025_00 = 152               '(千葉)メルクエレクトロニクス    'ADD 2023/02/27 035852 メルクエレクトロニクス セミEDI新規構築
        MercOsk00102_00 = 153               '(大阪)メルクエレクトロニクス    'ADD 2023/02/27 035852 メルクエレクトロニクス セミEDI新規構築
        SBS = 155                           ' SBS東芝ロジスティクス
        AGRM = 156                          ' アグリマート
        SGM10 = 157                         '(千葉)シグマアルドリッチジャパン
        TrmSmpl00409_01 = 158               '(土気)テルモ サンプル
        MaruwaYokoCsv00330_00 = 159         '丸和(横浜)(CSV)
        DhlYok00052_00 = 160                '(横浜)DHLサプライチェーン
        AftOsk = 161                        '(大阪)アフトンケミカル
        EneosOsk = 163                      '(大阪)ENEOS  'ADD 2023/01/11 033215 ENEOS　EDI作成
        EneosShm = 164                      '(塩浜)ENEOS  'ADD 2023/01/11 033215 ENEOS　EDI作成
        Tetsutani = 167                     '(大阪)テツタニ
        Rapidus40 = 168                     '(横浜)Rapidus
        BAH15 = 170                         '(土気)物産アニマルヘルス
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
    Private _Print510 As LMH510BLC = New LMH510BLC()

    '2012.09.11 要望番号1429 修正START
    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print540 As LMH540BLC = New LMH540BLC()
    '2012.09.11 要望番号1429 修正END

    'START UMANO 要望番号1302 支払運賃に伴う修正。支払バッチ呼び出し
    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _ShiharaiBlc As LMF810BLC = New LMF810BLC()
    'END UMANO 要望番号1302 支払運賃に伴う修正。支払バッチ呼び出し


#If True Then ' BP運送会社自動設定対応 20161115 added by inoue
    Private _BpCharterManagementTable As DataTable = Nothing
#End If
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

        'INTABLEよりEDI荷主対象INDEXを取得
        ediIndex = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

        rtnBlc = New LMH030BLC

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

#Region "出荷登録,運送登録処理"

    ''' <summary>
    ''' 出荷登録,運送登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OutkaToroku(ByVal ds As DataSet) As DataSet

        '更新対象テーブル名
        Dim tableNm As String = "LMH030INOUT"
        Dim dt As DataTable = ds.Tables(tableNm)
        Dim dtJudge As DataTable = ds.Tables("LMH030_JUDGE")
        Dim max As Integer = dt.Rows.Count - 1
        Dim ediIndex As Integer = 0
        Dim rtn As String = String.Empty
        Dim rtnResult As Boolean = False

        '要望番号1262:(EDI：運送登録時メッセージに件数を表示) 2012/07/13 本明 Start
        Dim iCommitCnt As Integer = 0   '更新件数
        '便宜上LMH030_EDI_TORIKOMI_RETを使用する
        '※ワーニング画面からも呼ばれる場合があるので初期化はしない事！
        Dim dtCntRet As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_RET")   '処理件数
        '要望番号1262:(EDI：運送登録時メッセージに件数を表示) 2012/07/13 本明 End

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDt As DataTable = setDs.Tables(tableNm)
        Dim setDtJudge As DataTable = setDs.Tables("LMH030_JUDGE")
        Dim setDtWarning As DataTable = setDs.Tables("WARNING_SHORI")

        '要望番号:1716(日産物流出荷EDIで出荷登録時、まとめが出来ない) 2012/12/20 本明 Start
        Dim sIkkatumatomeChk() As String     '日産物流まとめチェックの保持用配列
        '要望番号:1716(日産物流出荷EDIで出荷登録時、まとめが出来ない) 2012/12/20 本明 End

        Dim jobNm As String = "OutkaToroku"
        Dim rtnBlc As Base.BLC.LMBaseBLC

        Dim shoriFlg As String = String.Empty
        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            '2011.09.16 初期値を必ずFALSEにする
            rtnResult = False

            shoriFlg = ds.Tables(tableNm).Rows(i)("SHORI_FLG").ToString()
            '処理フラグ判定
            If shoriFlg.Equals(LMH030BLF.FLG_SHORIZUMI) Then
                '処理済の場合は次のレコードへ
                Continue For
            End If

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                ediIndex = Convert.ToInt32(ds.Tables(tableNm).Rows(i)("EDI_CUST_INDEX"))

                '▼▼▼二次
                'rtnBlc = getBLC(ediIndex)
                rtnBlc = getBlcOutkaToroku(ediIndex)
                '▲▲▲二次

                '値のクリア
                setDs.Clear()

                '条件の設定
                setDt.ImportRow(dt.Rows(i))
                setDtJudge.ImportRow(dtJudge.Rows(0))
                setDtWarning.Merge(ds.Tables("WARNING_SHORI"))

#If True Then ' BP運送会社自動設定対応 20161115 added by inoue
                setDs = Me.MergeTakeOverTables(ediIndex, ds, setDs)
#End If
                '要望番号:1716(日産物流出荷EDIで出荷登録時、まとめが出来ない) 2012/12/20 本明 Start
                'クリアされるので再設定
                If sIkkatumatomeChk IsNot Nothing Then
                    For index As Integer = 0 To UBound(sIkkatumatomeChk)
                        Dim dr As DataRow = setDs.Tables("LMH030_IKKATUMATOME_CHK").NewRow()
                        dr("OUTKA_NO_L") = sIkkatumatomeChk(index)
                        setDs.Tables("LMH030_IKKATUMATOME_CHK").Rows.Add(dr)
                    Next
                End If
                '要望番号:1716(日産物流出荷EDIで出荷登録時、まとめが出来ない) 2012/12/20 本明 End

                If ediIndex = EdiCustIndex.MaruwaYoko00330_00 OrElse ediIndex = EdiCustIndex.MaruwaYokoCsv00330_00 Then
                    ' 対象 EDI_CUST_INDEX が、丸和(横浜) または 丸和(横浜)(CSV) の場合
                    If (Not (ds.Tables("LMH030_SEMIEDI_INFO") Is Nothing)) AndAlso
                        ds.Tables("LMH030_SEMIEDI_INFO").Rows.Count > 0 Then
                        ' M_SEMIEDI_INFO_STATE の DataTable 内容を取り込む。
                        setDs.Tables("LMH030_SEMIEDI_INFO").ImportRow(ds.Tables("LMH030_SEMIEDI_INFO").Rows(0))
                    End If
                End If

                setDs = MyBase.CallBLC(rtnBlc, jobNm, setDs)


                '要望番号:1716(日産物流出荷EDIで出荷登録時、まとめが出来ない) 2012/12/20 本明 Start
                '設定された内容を保持
                If setDs.Tables("LMH030_IKKATUMATOME_CHK").Rows.Count > 0 Then
                    ReDim sIkkatumatomeChk(setDs.Tables("LMH030_IKKATUMATOME_CHK").Rows.Count - 1)
                    For index As Integer = 0 To setDs.Tables("LMH030_IKKATUMATOME_CHK").Rows.Count - 1
                        sIkkatumatomeChk(index) = setDs.Tables("LMH030_IKKATUMATOME_CHK").Rows(index).Item("OUTKA_NO_L").ToString
                    Next
                End If
                '要望番号:1716(日産物流出荷EDIで出荷登録時、まとめが出来ない) 2012/12/20 本明 End
#If True Then ' BP運送会社自動設定対応 20161115 added by inoue
                Me.MergeTakeOverResult(ediIndex, ds, setDs)
#End If

                'エラーがあるかを判定
                'rtnResult = Not MyBase.IsMessageExist()
                'rtnResult = Not MyBase.IsMessageStoreExist()
                rowNo = Convert.ToInt32(dt.Rows(i).Item("ROW_NO"))

                If MyBase.IsMessageStoreExist(rowNo) = False Then
                    If setDs.Tables("WARNING_DTL").Rows.Count = 0 Then
                        rtnResult = True
                    End If
                End If

                If rtnResult = True Then

                    '↓FFEM特殊処理↓
                    '2014.06.09 追加START
                    'FFEMでキャンセル出荷登録(実績作成)の場合はここで処理終了
                    If ediIndex = 96 AndAlso ("2").Equals(setDs.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("DEL_KB").ToString) = True Then
                        'トランザクション終了
                        MyBase.CommitTransaction(scope)
                        '処理フラグに処理済設定
                        ds.Tables(tableNm).Rows(i)("SHORI_FLG") = LMH030BLF.FLG_SHORIZUMI
                        iCommitCnt = iCommitCnt + 1     '更新件数
                        '↑FFEM特殊処理↑
                        '2014.06.09 追加START

                    ElseIf ediIndex = 105 Then
                        'トランザクション終了
                        MyBase.CommitTransaction(scope)

                        rtnBlc = getBlcOutkaToroku(ediIndex)

                        If setDs.Tables("LMH030_HASUSHORI").Rows(0).Item("RTN_FLG").Equals("1") = True Then

                            'トランザクション開始
                            Using scopeHasu As TransactionScope = MyBase.BeginTransaction()
                                setDs = MyBase.CallBLC(rtnBlc, "OutkaTorokuHasu", setDs)

                                If rtnResult = True Then
                                    'トランザクション終了
                                    MyBase.CommitTransaction(scopeHasu)
                                    '処理フラグに処理済設定
                                    ds.Tables(tableNm).Rows(i)("SHORI_FLG") = LMH030BLF.FLG_SHORIZUMI
                                    iCommitCnt = iCommitCnt + 1     '更新件数
                                End If
                            End Using

                        End If

                        '(2013.03.18)修正START BPCの場合、運賃データは作成しない(※出荷登録速度改善の為)
                        'If ("10").Equals(setDs.Tables("LMH030_UNSO_L").Rows(0).Item("UNSO_TEHAI_KB").ToString) = True Then
                    ElseIf ("10").Equals(setDs.Tables("LMH030_UNSO_L").Rows(0).Item("UNSO_TEHAI_KB").ToString) = True _
                       AndAlso ediIndex <> 77 Then
                        '(2013.03.18)修正END BPCの場合、運賃データは作成しない(※出荷登録速度改善の為)
                        '日陸手配の時のみ運賃データを作成

#If False Then      'UPD 2019/02/20 依頼番号 : 004085   【LMS】古河事業所日立物流_危険品と一般品の運賃請求：オーダー合算を廃止、別オーダーとして請求
                        'データセット設定
                        Dim unchinDs As DataSet = Me.SetUnchinInDataSet(setDs)

                        'BLCアクセス
                        unchinDs = MyBase.CallBLC(Me._UnchinBlc, "CreateUnchinData", unchinDs)

                        '修正開始 --- 2014.02.13 要望番号:2270
                        unchinDs = SetUnchinInDataSetMHM(unchinDs, setDs, ediIndex)
                        '修正終了 --- 2014.02.13 要望番号:2270

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
                                If String.IsNullOrEmpty(setDs.Tables("LMH030_UNSO_L").Rows(0).Item("UNSO_CD").ToString) = True Then

                                    '運送会社が指定されていない場合
                                    'トランザクション終了
                                    MyBase.CommitTransaction(scope)
                                    '処理フラグに処理済設定
                                    ds.Tables(tableNm).Rows(i)("SHORI_FLG") = LMH030BLF.FLG_SHORIZUMI
                                    iCommitCnt = iCommitCnt + 1     '更新件数

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
                                        rtnBlc = New LMH030BLC
                                        shiharaiDs = MyBase.CallBLC(rtnBlc, "ShiharaiSakusei", shiharaiDs)

                                        'エラーがあるかを判定
                                        rtnResult = Not MyBase.IsMessageExist()

                                        If rtnResult = True Then

                                            'トランザクション終了
                                            MyBase.CommitTransaction(scope)
                                            '処理フラグに処理済設定
                                            ds.Tables(tableNm).Rows(i)("SHORI_FLG") = LMH030BLF.FLG_SHORIZUMI
                                            iCommitCnt = iCommitCnt + 1     '更新件数
                                        End If

                                    Else
                                        'エラーの場合はエラー処理
                                        MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, rtnResultDr2.Item("ERROR_CD").ToString, _
                                                               New String() {rtnResultDr2.Item("YOBI1").ToString}, _
                                                               setDs.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString(), LMH030BLC.EXCEL_COLTITLE, _
                                                               setDs.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString())
                                    End If
                                End If
                                'END UMANO 要望番号1302 支払運賃に伴う修正。支払バッチ呼び出し

                            End If

                        Else
                            'エラーの場合はエラー処理
                            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, rtnResultDr.Item("ERROR_CD").ToString, _
                                                   New String() {rtnResultDr.Item("YOBI1").ToString}, _
                                                   setDs.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString(), LMH030BLC.EXCEL_COLTITLE, _
                                                   setDs.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString())
                            'MyBase.SetMessage(rtnResultDr.Item("ERROR_CD").ToString, New String() {rtnResultDr.Item("YOBI1").ToString})

                        End If

#Else

                        Dim ediCusCdL As String = setDs.Tables("LMH030INOUT").Rows(0).Item("CUST_CD_L").ToString()
                        Dim ediCusCdM As String = setDs.Tables("LMH030INOUT").Rows(0).Item("CUST_CD_M").ToString()
                        Dim ediNRS_BR_CD As String = setDs.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
                        Dim free_C28 As String = String.Empty

                        If setDs.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("FREE_C28").ToString.Length = 12 Then
                            free_C28 = setDs.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("FREE_C28").ToString().Substring(0, 3)
                        End If

                        Dim unsoMax As Integer = 0

                        'If ("30001").Equals(ediCusCdL) = True _
                        '    And ("00").Equals(ediCusCdM) = True _
                        '    And ("55").Equals(ediNRS_BR_CD) = True _
                        '    And ("01-").Equals(free_C28) = True Then
                        If ("30001").Equals(ediCusCdL) = True _
                            And ("00").Equals(ediCusCdM) = True _
                            And ("30").Equals(ediNRS_BR_CD) = True _
                            And ("01-").Equals(free_C28) = True Then
                            '古河で危険物有・無しで分割された時
                            unsoMax = 1
                        End If
                        For x As Integer = 0 To unsoMax

                            'データセット設定
                            Dim unchinDs As DataSet = Me.SetUnchinInDataSet(setDs, x)

                            'BLCアクセス
                            unchinDs = MyBase.CallBLC(Me._UnchinBlc, "CreateUnchinData", unchinDs)

                            '修正開始 --- 2014.02.13 要望番号:2270
                            unchinDs = SetUnchinInDataSetMHM(unchinDs, setDs, ediIndex)
                            '修正終了 --- 2014.02.13 要望番号:2270

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
                                rtnBlc = getBlcOutkaToroku(ediIndex)

                                unchinDs = MyBase.CallBLC(rtnBlc, "UnchinSakusei", unchinDs)

                                'エラーがあるかを判定
                                rtnResult = Not MyBase.IsMessageExist()

                                If rtnResult = True Then

                                    'START UMANO 要望番号1302 支払運賃に伴う修正。支払バッチ呼び出し
                                    If String.IsNullOrEmpty(setDs.Tables("LMH030_UNSO_L").Rows(0).Item("UNSO_CD").ToString) = True Then

                                        '運送会社が指定されていない場合
                                        'トランザクション終了
                                        MyBase.CommitTransaction(scope)
                                        '処理フラグに処理済設定
                                        ds.Tables(tableNm).Rows(i)("SHORI_FLG") = LMH030BLF.FLG_SHORIZUMI
                                        iCommitCnt = iCommitCnt + 1     '更新件数

                                    Else
                                        '運送会社が指定されている場合、支払バッチを呼び出す
                                        'データセット設定
                                        Dim shiharaiDs As DataSet = Me.SetShiharaiInDataSet(setDs, x)

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
                                            rtnBlc = New LMH030BLC
                                            shiharaiDs = MyBase.CallBLC(rtnBlc, "ShiharaiSakusei", shiharaiDs)

                                            'エラーがあるかを判定
                                            rtnResult = Not MyBase.IsMessageExist()

                                            If rtnResult = True Then

                                                If x = unsoMax Then
                                                    'トランザクション終了
                                                    MyBase.CommitTransaction(scope)
                                                    '処理フラグに処理済設定
                                                    ds.Tables(tableNm).Rows(i)("SHORI_FLG") = LMH030BLF.FLG_SHORIZUMI

                                                End If


                                                iCommitCnt = iCommitCnt + 1     '更新件数
                                            End If

                                        Else
                                            'エラーの場合はエラー処理
                                            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, rtnResultDr2.Item("ERROR_CD").ToString, _
                                                                   New String() {rtnResultDr2.Item("YOBI1").ToString}, _
                                                                   setDs.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString(), LMH030BLC.EXCEL_COLTITLE, _
                                                                   setDs.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString())
                                        End If
                                    End If
                                    'END UMANO 要望番号1302 支払運賃に伴う修正。支払バッチ呼び出し

                                End If

                            Else
                                'エラーの場合はエラー処理
                                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, rtnResultDr.Item("ERROR_CD").ToString, _
                                                       New String() {rtnResultDr.Item("YOBI1").ToString}, _
                                                       setDs.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString(), LMH030BLC.EXCEL_COLTITLE, _
                                                       setDs.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString())
                                'MyBase.SetMessage(rtnResultDr.Item("ERROR_CD").ToString, New String() {rtnResultDr.Item("YOBI1").ToString})

                            End If
                        Next


#End If

                    Else
                        '先方手配、未定の時はトランザクション終了

                        'トランザクション終了
                        MyBase.CommitTransaction(scope)
                        '処理フラグに処理済設定
                        ds.Tables(tableNm).Rows(i)("SHORI_FLG") = LMH030BLF.FLG_SHORIZUMI
                        '要望番号1262:(EDI：運送登録時メッセージに件数を表示) 2012/07/13 本明 Start
                        iCommitCnt = iCommitCnt + 1     '更新件数
                        '要望番号1262:(EDI：運送登録時メッセージに件数を表示) 2012/07/13 本明 End
                    End If

                End If

                'エラーの場合、処理フラグに処理済設定
                'If MyBase.IsErrorMessageExist = True Then
                If MyBase.IsMessageStoreExist(rowNo) = True Then
                    ds.Tables(tableNm).Rows(i)("SHORI_FLG") = LMH030BLF.FLG_SHORIZUMI
                    'このレコードのワーニングをクリア(エラーがあるレコードはワーニングを出さない)
                    setDs.Tables("WARNING_DTL").Rows.Clear()
                End If

            End Using

            If setDs.Tables("WARNING_DTL").Rows.Count <> 0 Then
                'ワーニングが設定されている場合、データセットに設定する
                ds = Me.SetDsWarningData(ds, setDs)

            End If

        Next


        '要望番号1262:(EDI：運送登録時メッセージに件数を表示) 2012/07/13 本明 Start
        '便宜上"RCV_HED_INS_CNT"に値をセット
        '※ワーニング画面からも呼ばれる場合があるのでRCV_HED_INS_CNTの値に加算する
        dtCntRet.Rows(0).Item("RCV_HED_INS_CNT") = Convert.ToInt32(dtCntRet.Rows(0).Item("RCV_HED_INS_CNT").ToString) + iCommitCnt
        '要望番号1262:(EDI：運送登録時メッセージに件数を表示) 2012/07/13 本明 End

        Return ds

    End Function

    ''' <summary>
    ''' 出荷登録,運送登録処理(ワーニング設定)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDsWarningData(ByVal ds As DataSet, ByVal setDs As DataSet) As DataSet

        ds.Tables("WARNING_DTL").Merge(setDs.Tables("WARNING_DTL"))

        Return ds

    End Function

#If True Then ' BP運送会社自動設定対応 20161115 added by inoue

    ''' <summary>
    ''' 運送重量,届先別の運送会社一覧を取得する
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectUnsoByWgtAndDestList(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallBLC(New LMH030BLC501() _
                            , LMH030BLC501.Functions.SelectUnsoByWgtAndDestList _
                            , ds)


        ds = MyBase.CallBLC(New LMH030BLC501() _
                            , LMH030BLC501.Functions.SelectCharterManagementList _
                            , ds)

        Return ds

    End Function

    ''' <summary>
    ''' 全出荷データの処理内で引継ぎするテーブルをマージする
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <param name="ds"></param>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MergeTakeOverTables(ByVal ediIndex As Integer _
                                     , ByVal ds As DataSet _
                                     , ByVal setDs As DataSet) As DataSet

        Select Case DirectCast(ediIndex, LMH030BLF.EdiCustIndex)
            Case EdiCustIndex.BP00023_00
                Return Me.MergeTakeOverTablesBp(ds, setDs)
        End Select

        Return setDs

    End Function


    ''' <summary>
    ''' 全出荷データの処理内で引継ぎする処理結果をマージする
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <param name="ds"></param>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MergeTakeOverResult(ByVal ediIndex As Integer _
                                       , ByVal ds As DataSet _
                                       , ByVal setDs As DataSet) As DataSet

        Select Case DirectCast(ediIndex, LMH030BLF.EdiCustIndex)
            Case EdiCustIndex.BP00023_00
                Return Me.MergeTakeOverResultBp(setDs)
        End Select

        Return setDs

    End Function


    ''' <summary>
    ''' 全出荷データの処理内で引継ぎする処理をマージする(BPカストロール用)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MergeTakeOverTablesBp(ByVal ds As DataSet _
                                         , ByVal setDs As DataSet) As DataSet

        setDs.Tables(LMH030BLC501.TableNames.OUT_UNSO_BY_WGT_AND_DEST) _
            .Merge(ds.Tables(LMH030BLC501.TableNames.OUT_UNSO_BY_WGT_AND_DEST))


        If (_BpCharterManagementTable Is Nothing) Then
            _BpCharterManagementTable = ds.Tables(LMH030BLC501.TableNames.OUT_CHARTER_MANAGEMENT).Copy
        End If

        setDs.Tables(LMH030BLC501.TableNames.OUT_CHARTER_MANAGEMENT) _
            .Merge(_BpCharterManagementTable)

        Return setDs

    End Function


    ''' <summary>
    ''' 全出荷データの処理内で引継ぎする処理結果をマージする(BPカストロール用)
    ''' </summary>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MergeTakeOverResultBp(ByVal setDs As DataSet) As DataSet

        _BpCharterManagementTable.Clear()
        _BpCharterManagementTable.Merge(setDs.Tables(LMH030BLC501.TableNames.OUT_CHARTER_MANAGEMENT))

        Return setDs

    End Function


#End If


#End Region

#Region "実績作成処理"

    ''' <summary>
    ''' 実績作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiSakusei(ByVal ds As DataSet) As DataSet

        '更新対象テーブル名
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtJudge As DataTable = ds.Tables("LMH030_JUDGE")
        Dim dtEdiL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtEdiM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim dtRcvHed As DataTable = ds.Tables("LMH030_EDI_RCV_HED")
        Dim dtRcvDtl As DataTable = ds.Tables("LMH030_EDI_RCV_DTL")
        Dim dtOutkaL As DataTable = ds.Tables("LMH030_C_OUTKA_L")
        Dim dtSend As DataTable = ds.Tables("LMH030_EDI_SND")

        Dim max As Integer = dtIn.Rows.Count - 1
        Dim ediIndex As Integer = 0
        Dim rtn As String = String.Empty
        Dim rtnResult As Boolean = False

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtIn As DataTable = setDs.Tables("LMH030INOUT")
        Dim setDtJudge As DataTable = setDs.Tables("LMH030_JUDGE")
        Dim setDtEdiL As DataTable = setDs.Tables("LMH030_OUTKAEDI_L")
        Dim setDtEdiM As DataTable = setDs.Tables("LMH030_OUTKAEDI_M")
        Dim setDtRcvHed As DataTable = setDs.Tables("LMH030_EDI_RCV_HED")
        Dim setDtRcvDtl As DataTable = setDs.Tables("LMH030_EDI_RCV_DTL")
        Dim setDtOutkaL As DataTable = setDs.Tables("LMH030_C_OUTKA_L")
        Dim setDtSend As DataTable = setDs.Tables("LMH030_EDI_SND")

        Dim setDtWarning As DataTable = setDs.Tables("WARNING_SHORI")

        Dim jobNm As String = "JissekiSakusei"
        Dim rtnBlc As Base.BLC.LMBaseBLC

        Dim outkaCtlNoHt As Hashtable = New Hashtable
        Dim outkaCtlNo As String = String.Empty
        Dim rowNo As Integer = 0
        Dim shoriFlg As String = String.Empty
        Dim matomeNo As String = String.Empty
        Dim autoMatomeF As String = String.Empty

        Dim matomeHt As Hashtable = New Hashtable

        For i As Integer = 0 To max

            '2011.09.16 初期値を必ずFALSEにする
            rtnResult = False

            shoriFlg = ds.Tables("LMH030INOUT").Rows(i)("SHORI_FLG").ToString()
            '処理フラグ判定
            If shoriFlg.Equals(LMH030BLF.FLG_SHORIZUMI) Then
                '処理済の場合は次のレコードへ
                Continue For
            End If

            'まとめ番号取得前の初期化
            matomeNo = String.Empty
            'まとめ番号を取得
            If String.IsNullOrEmpty(ds.Tables("LMH030INOUT").Rows(i)("MATOME_NO").ToString()) = False Then
                matomeNo = ds.Tables("LMH030INOUT").Rows(i)("MATOME_NO").ToString()
            End If

            If String.IsNullOrEmpty(matomeNo) = False Then

                If matomeHt.ContainsKey(matomeNo) = True Then
                    '同一まとめ番号の場合は、既に更新しているので処理をしない
                    Continue For
                Else
                    matomeHt.Add(matomeNo, String.Empty)
                End If
            End If

            If String.IsNullOrEmpty(ds.Tables("LMH030INOUT").Rows(i)("AUTO_MATOME_FLG").ToString()) = False Then
                autoMatomeF = ds.Tables("LMH030INOUT").Rows(i)("AUTO_MATOME_FLG").ToString()
            End If

            If String.IsNullOrEmpty(matomeNo) = False AndAlso autoMatomeF.Equals("9") = False Then
                'まとめ済みレコードの場合

                ediIndex = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(i)("EDI_CUST_INDEX"))
                '▼▼▼二次
                'rtnBlc = getBLC(ediIndex)
                rtnBlc = getBlcJisseki(ediIndex)
                '▲▲▲二次

                Dim matomeDs As DataSet = ds.Copy
                matomeDs.Clear()

                matomeDs.Tables("LMH030INOUT").ImportRow(dtIn.Rows(i))
                '対象レコードがまとめ済みの場合、同一まとめ番号のレコードを取得する
                matomeDs = MyBase.CallBLC(rtnBlc, "SelectMatome", matomeDs)

                Dim eventShubetu As String = ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()

                rowNo = Convert.ToInt32(dtIn.Rows(i).Item("ROW_NO"))

                Dim maxMatome As Integer = matomeDs.Tables("LMH030OUT").Rows.Count - 1

                If maxMatome < 0 Then
                    'エラーの場合はエラー処理
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , _
                                           rowNo.ToString(), LMH030BLC.EXCEL_COLTITLE, _
                                           dtIn.Rows(i).Item("EDI_CTL_NO").ToString())

                    Continue For

                End If

                'トランザクション開始
                Using scope As TransactionScope = MyBase.BeginTransaction()

                    '初期値を必ずfalseにする
                    rtnResult = False

                    For j As Integer = 0 To maxMatome
                        '値のクリア
                        setDs.Clear()
                        Call Me.SetDataJissekiSakusei(matomeDs.Tables("LMH030OUT").Rows(j), setDs, eventShubetu, rowNo)

                        outkaCtlNo = dtIn.Rows(i).Item("OUTKA_CTL_NO").ToString()

                        If outkaCtlNoHt.ContainsKey(outkaCtlNo) = True Then
                            '同一出荷管理番号の場合は、既に進捗区分を更新しているので、出荷(大)は更新しない
                            setDtIn.Rows(0).Item("OUTKA_L_UPD_FLG") = "0"
                        Else
                            setDtIn.Rows(0).Item("OUTKA_L_UPD_FLG") = "1"
                            outkaCtlNoHt.Add(outkaCtlNo, String.Empty)
                        End If

                        setDs = MyBase.CallBLC(rtnBlc, jobNm, setDs)

                        'エラーがあるかを判定
                        rowNo = Convert.ToInt32(dtIn.Rows(i).Item("ROW_NO"))

                        If MyBase.IsMessageStoreExist(rowNo) = False Then
                            If setDs.Tables("WARNING_DTL").Rows.Count = 0 Then
                                rtnResult = True
                            Else
                                '1件でもエラーがある場合は処理を抜けて終了
                                rtnResult = False
                                Exit For
                            End If
                        End If

                    Next

                    If rtnResult = True Then

                        'トランザクション終了
                        MyBase.CommitTransaction(scope)

                        '▼▼▼要望番号:479
                        '処理フラグに処理済設定
                        ds.Tables("LMH030INOUT").Rows(i)("SHORI_FLG") = LMH030BLF.FLG_SHORIZUMI
                        '▲▲▲要望番号:479
                    End If

                    'エラーの場合、処理フラグに処理済設定
                    If MyBase.IsMessageStoreExist(rowNo) = True Then
                        ds.Tables("LMH030INOUT").Rows(i)("SHORI_FLG") = LMH030BLF.FLG_SHORIZUMI
                        'このレコードのワーニングをクリア(エラーがあるレコードはワーニングを出さない)
                        setDs.Tables("WARNING_DTL").Rows.Clear()
                    End If

                End Using
            Else
                '通常レコード
                'トランザクション開始
                Using scope As TransactionScope = MyBase.BeginTransaction()

                    ediIndex = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(i)("EDI_CUST_INDEX"))

                    '▼▼▼二次
                    'rtnBlc = getBLC(ediIndex)
                    rtnBlc = getBlcJisseki(ediIndex)
                    '▲▲▲二次

                    '値のクリア
                    setDs.Clear()

                    '条件の設定
                    'setDt.ImportRow(dt.Rows(i))
                    setDtIn.ImportRow(dtIn.Rows(i))
                    setDtJudge.ImportRow(dtJudge.Rows(0))
                    setDtEdiL.ImportRow(dtEdiL.Rows(i))
                    setDtEdiM.ImportRow(dtEdiM.Rows(i))
                    setDtRcvHed.ImportRow(dtRcvHed.Rows(i))
                    setDtRcvDtl.ImportRow(dtRcvDtl.Rows(i))
                    setDtOutkaL.ImportRow(dtOutkaL.Rows(i))
                    setDtSend.ImportRow(dtSend.Rows(i))
                    setDtWarning.Merge(ds.Tables("WARNING_SHORI"))
                    outkaCtlNo = dtIn.Rows(i).Item("OUTKA_CTL_NO").ToString()

                    If outkaCtlNoHt.ContainsKey(outkaCtlNo) = True Then
                        '同一出荷管理番号の場合は、既に進捗区分を更新しているので、出荷(大)は更新しない
                        setDtIn.Rows(0).Item("OUTKA_L_UPD_FLG") = "0"
                    Else
                        setDtIn.Rows(0).Item("OUTKA_L_UPD_FLG") = "1"
                        outkaCtlNoHt.Add(outkaCtlNo, String.Empty)
                    End If

                    setDs = MyBase.CallBLC(rtnBlc, jobNm, setDs)

                    'エラーがあるかを判定
                    rowNo = Convert.ToInt32(dtIn.Rows(i).Item("ROW_NO"))

                    If MyBase.IsMessageStoreExist(rowNo) = False Then
                        If setDs.Tables("WARNING_DTL").Rows.Count = 0 Then
                            rtnResult = True
                        End If
                    End If

                    If rtnResult = True Then

                        'トランザクション終了
                        MyBase.CommitTransaction(scope)
                        '▼▼▼要望番号:479
                        '処理フラグに処理済設定
                        ds.Tables("LMH030INOUT").Rows(i)("SHORI_FLG") = LMH030BLF.FLG_SHORIZUMI
                        '▲▲▲要望番号:479
                    End If

                    'エラーの場合、処理フラグに処理済設定
                    If MyBase.IsMessageStoreExist(rowNo) = True Then
                        ds.Tables("LMH030INOUT").Rows(i)("SHORI_FLG") = LMH030BLF.FLG_SHORIZUMI
                        'このレコードのワーニングをクリア(エラーがあるレコードはワーニングを出さない)
                        setDs.Tables("WARNING_DTL").Rows.Clear()
                    End If

                End Using

                If setDs.Tables("WARNING_DTL").Rows.Count <> 0 Then
                    'ワーニングが設定されている場合、データセットに設定する
                    ds = Me.SetDsWarningData(ds, setDs)

                End If

            End If

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
        'Dim tableNm As String = "LMH030INOUT"
        'Dim dt As DataTable = ds.Tables(tableNm)
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtJudge As DataTable = ds.Tables("LMH030_JUDGE")
        Dim dtEdiL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtRcvHed As DataTable = ds.Tables("LMH030_EDI_RCV_HED")
        Dim dtRcvDtl As DataTable = ds.Tables("LMH030_EDI_RCV_DTL")

        'Dim max As Integer = dt.Rows.Count - 1
        Dim max As Integer = dtIn.Rows.Count - 1
        Dim ediIndex As Integer = 0
        Dim rtn As String = String.Empty
        Dim rtnResult As Boolean = False

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        'Dim setDt As DataTable = setDs.Tables(tableNm)
        Dim setDtIn As DataTable = setDs.Tables("LMH030INOUT")
        Dim setDtJudge As DataTable = setDs.Tables("LMH030_JUDGE")
        Dim setDtEdiL As DataTable = setDs.Tables("LMH030_OUTKAEDI_L")
        Dim setDtRcvHed As DataTable = setDs.Tables("LMH030_EDI_RCV_HED")
        Dim setDtRcvDtl As DataTable = setDs.Tables("LMH030_EDI_RCV_DTL")


        Dim jobNm As String = "JissekiTorikesi"
        Dim rtnBlc As Base.BLC.LMBaseBLC
        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            '2011.09.16 初期値を必ずFALSEにする
            rtnResult = False

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                ediIndex = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(i)("EDI_CUST_INDEX"))

                '▼▼▼二次
                'rtnBlc = getBLC(ediIndex)
                rtnBlc = getBlcJissekiTorikesi(ediIndex)
                '▲▲▲二次

                '値のクリア
                setDs.Clear()

                '条件の設定
                'setDt.ImportRow(dt.Rows(i))
                setDtIn.ImportRow(dtIn.Rows(i))
                setDtJudge.ImportRow(dtJudge.Rows(0))
                setDtEdiL.ImportRow(dtEdiL.Rows(i))
                setDtRcvHed.ImportRow(dtRcvHed.Rows(i))
                setDtRcvDtl.ImportRow(dtRcvDtl.Rows(i))


                setDs = MyBase.CallBLC(rtnBlc, jobNm, setDs)

                'エラーがあるかを判定
                rowNo = Convert.ToInt32(dtIn.Rows(i).Item("ROW_NO"))

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

#Region "実績作成済⇒実績未(実行処理)"

    ''' <summary>
    ''' 実行処理(実績作成済⇒実績未)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiSakuseiJissekimi(ByVal ds As DataSet) As DataSet

        Dim ediIndex As Integer = 0
        Dim rtn As String = String.Empty
        Dim rtnResult As Boolean = False

        Dim jobNm As String = "JikkouSyori"
        Dim rtnBlc As Base.BLC.LMBaseBLC

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ediIndex = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

            '▼▼▼二次
            'rtnBlc = getBLC(ediIndex)
            rtnBlc = getBlcJissekiSakuseiJissekimi(ediIndex)
            '▲▲▲二次

            ds = MyBase.CallBLC(rtnBlc, jobNm, ds)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function

#End Region

#Region "実績送信済⇒送信待(実行処理)"

    ''' <summary>
    ''' 実行処理(実績送信済⇒送信待)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SousinSousinmi(ByVal ds As DataSet) As DataSet


        Dim rtnResult As Boolean = False
        Dim ediIndex As Integer = 0

        Dim jobNm As String = "SousinSousinmi"
        Dim rtnBlc As Base.BLC.LMBaseBLC


        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ediIndex = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))
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

#Region "実績送信済⇒実績未(実行処理)"

    ''' <summary>
    ''' 実行処理(実績作成済⇒実績未)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function JissekiSousinJissekimi(ByVal ds As DataSet) As DataSet

        Dim ediIndex As Integer = 0
        Dim rtn As String = String.Empty
        Dim rtnResult As Boolean = False

        Dim jobNm As String = "JikkouSyori"
        Dim rtnBlc As Base.BLC.LMBaseBLC

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ediIndex = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

            '▼▼▼二次
            'rtnBlc = getBLC(ediIndex)
            rtnBlc = getBlcSousinJissekimi(ediIndex)
            '▲▲▲二次

            ds = MyBase.CallBLC(rtnBlc, jobNm, ds)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function

#End Region

#Region "出荷取消⇒未登録(実行処理)"

    ''' <summary>
    ''' 実行処理(出荷取消⇒未登録)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Mitouroku(ByVal ds As DataSet) As DataSet

        Dim ediIndex As Integer = 0
        Dim rtn As String = String.Empty
        Dim rtnResult As Boolean = False

        Dim jobNm As String = "Mitouroku"
        Dim rtnBlc As Base.BLC.LMBaseBLC

        Dim matomeNo As String = String.Empty
        Dim autoMatomeF As String = String.Empty
        Dim rowNo As Integer = 0

        'まとめ番号を取得
        If String.IsNullOrEmpty(ds.Tables("LMH030INOUT").Rows(0)("MATOME_NO").ToString()) = False Then
            matomeNo = ds.Tables("LMH030INOUT").Rows(0)("MATOME_NO").ToString()
        End If

        If String.IsNullOrEmpty(ds.Tables("LMH030INOUT").Rows(0)("AUTO_MATOME_FLG").ToString()) = False Then
            autoMatomeF = ds.Tables("LMH030INOUT").Rows(0)("AUTO_MATOME_FLG").ToString()
        End If

        If String.IsNullOrEmpty(matomeNo) = False AndAlso autoMatomeF.Equals("9") = False Then
            'まとめ済みレコードの場合

            ediIndex = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))
            '▼▼▼二次
            'rtnBlc = getBLC(ediIndex)
            rtnBlc = getBlcMitouroku(ediIndex)
            '▲▲▲二次

            '対象レコードがまとめ済みの場合、同一まとめ番号のレコードを取得する
            ds = MyBase.CallBLC(rtnBlc, "SelectMatomeTorikesi", ds)

            Dim eventShubetu As String = ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()

            rowNo = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO"))

            Dim maxMatome As Integer = ds.Tables("LMH030OUT").Rows.Count - 1

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

                    Call Me.SetDataTorikesiMitouroku(ds.Tables("LMH030OUT").Rows(j), matomeDs, eventShubetu, rowNo)

                    matomeDs = MyBase.CallBLC(rtnBlc, jobNm, matomeDs)

                    'エラーがあるかを判定
                    rowNo = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO"))

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

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                ediIndex = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

                '▼▼▼二次
                'rtnBlc = getBLC(ediIndex)
                rtnBlc = getBlcMitouroku(ediIndex)
                '▲▲▲二次

                ds = MyBase.CallBLC(rtnBlc, jobNm, ds)

                'エラーがあるかを判定
                rtnResult = Not MyBase.IsMessageExist()

                If rtnResult = True Then

                    'トランザクション終了
                    MyBase.CommitTransaction(scope)

                End If

            End Using

        End If

        Return ds

    End Function

#End Region

    '2012.04.04 大阪対応追加START
#Region "運送取消⇒未登録(実行処理)"

    ''' <summary>
    ''' 実行処理(運送取消⇒未登録)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UnsoMitouroku(ByVal ds As DataSet) As DataSet

        Dim ediIndex As Integer = 0
        Dim rtn As String = String.Empty
        Dim rtnResult As Boolean = False

        Dim jobNm As String = "UnsoMitouroku"
        Dim rtnBlc As Base.BLC.LMBaseBLC

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ediIndex = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

            rtnBlc = getBlcUnsoMitouroku(ediIndex)

            ds = MyBase.CallBLC(rtnBlc, jobNm, ds)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function

#End Region
    '2012.04.04 大阪対応追加END

#Region "EDI取消"
    Private Function EdiTorikesi(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        '更新対象テーブル名

        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEdiL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtEdiM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim dtRcvHed As DataTable = ds.Tables("LMH030_EDI_RCV_HED")
        Dim dtRcvDtl As DataTable = ds.Tables("LMH030_EDI_RCV_DTL")
        Dim dtJudge As DataTable = ds.Tables("LMH030_JUDGE")

        Dim max As Integer = dtEdiL.Rows.Count - 1
        Dim ediIndex As Integer = 0
        Dim rtn As String = String.Empty

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtIn As DataTable = setDs.Tables("LMH030INOUT")
        Dim setDtEdiL As DataTable = setDs.Tables("LMH030_OUTKAEDI_L")
        Dim setDtEdiM As DataTable = setDs.Tables("LMH030_OUTKAEDI_M")
        Dim setDtRcvHed As DataTable = setDs.Tables("LMH030_EDI_RCV_HED")
        Dim setDtRcvDtl As DataTable = setDs.Tables("LMH030_EDI_RCV_DTL")
        Dim setDtJudge As DataTable = setDs.Tables("LMH030_JUDGE")

        Dim jobNm As String = "EdiTorikesi"
        Dim rtnBlc As Base.BLC.LMBaseBLC

        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            '2011.09.16 初期値を必ずFALSEにする
            rtnResult = False

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                ediIndex = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(i)("EDI_CUST_INDEX"))
                '▼▼▼二次
                'rtnBlc = getBLC(ediIndex)
                rtnBlc = getBlcEdiTorikesi(ediIndex)
                '▲▲▲二次

                '値のクリア
                setDs.Clear()

                '条件の設定
                setDtIn.ImportRow(dtIn.Rows(i))
                setDtEdiL.ImportRow(dtEdiL.Rows(i))
                setDtEdiM.ImportRow(dtEdiM.Rows(i))
                setDtRcvHed.ImportRow(dtRcvHed.Rows(i))
                setDtRcvDtl.ImportRow(dtRcvDtl.Rows(i))
                setDtJudge.ImportRow(dtJudge.Rows(0))

                setDs = MyBase.CallBLC(rtnBlc, jobNm, setDs)

                'エラーがあるかを判定
                rowNo = Convert.ToInt32(dtIn.Rows(i).Item("ROW_NO"))

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

#Region "EDI取消⇒未登録(実行処理)"
    Private Function TorikesiMitouroku(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False

        Dim ediIndex As Integer = 0

        Dim jobNm As String = "EdiMitouroku"
        Dim rtnBlc As Base.BLC.LMBaseBLC


        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ediIndex = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))
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

#Region "一括変更処理"

    ''' <summary>
    ''' 一括変更処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateHenko(ByVal ds As DataSet) As DataSet

        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtKey As DataTable = ds.Tables("LMH030OUT_UPDATE_KEY")
        Dim max As Integer = dtKey.Rows.Count - 1
        Dim ediIndex As Integer = 0
        Dim rtn As String = String.Empty
        Dim rtnResult As Boolean = False

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtIn As DataTable = setDs.Tables("LMH030INOUT")
        Dim setDtKey As DataTable = setDs.Tables("LMH030OUT_UPDATE_KEY")

        Dim jobNm As String = "UpdateHenko"
        Dim rtnBlc As Base.BLC.LMBaseBLC

        For i As Integer = 0 To max

            '2011.09.16 初期値を必ずFALSEにする
            rtnResult = False

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                ediIndex = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(i)("EDI_CUST_INDEX"))

                rtnBlc = getBLC(ediIndex)

                '値のクリア
                setDtKey.Clear()

                '条件の設定
                setDtIn.ImportRow(dtIn.Rows(i))
                setDtKey.ImportRow(dtKey.Rows(i))

                'DACクラス呼出
                setDs = MyBase.CallBLC(rtnBlc, "UpdateHenko", setDs)

                'エラーがあるかを判定
                'rtnResult = Not MyBase.IsMessageExist()
                rtnResult = Not MyBase.IsMessageStoreExist()

                If rtnResult = True Then
                    'エラーが無ければCommit
                    MyBase.CommitTransaction(scope)
                End If

            End Using
        Next

        Return setDs

    End Function

#End Region

#Region "印刷処理(画面検索条件での印刷)"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function PrintData(ByVal ds As DataSet) As DataSet

        '印刷種別,処理種別(印刷or出力)の取得
        Dim dt As DataTable = ds.Tables(LMH030BLF.TABLE_NM_IN)
        Dim dr As DataRow = dt.Rows(0)
        Dim eventflg As String = String.Empty
        Dim printflg As String = String.Empty

        eventflg = dr.Item("EVENT_SHUBETSU").ToString()     '処理種別(LMH030C.eventShubetsu)
        printflg = dr.Item("PRINT_SHUBETSU").ToString()     '印刷種別(LMH030C.Print_KBN)

        Select Case eventflg

            Case "11" '印刷ボタン押下

                Select Case printflg

                    Case "1"  'EDI出荷チェックリスト
                        ds = Me.PrintLMH510(dr)

                        '    '2012.09.11 要望番号1429 修正START
                        'Case "2"  'EDI出荷取消チェックリスト
                        '    ds = Me.PrintLMH540(dr)
                        '    '2012.09.11 要望番号1429 修正END

                End Select

        End Select

        Return ds

    End Function

    ''' <summary>
    ''' EDI出荷チェックリスト
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH510(ByVal dr As DataRow) As DataSet

        'EDI出荷チェックリスト
        Dim PrmDs As DataSet = New LMH510DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH510IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        PrmDr("EDIOUTKA_STATE_KB1") = dr.Item("EDIOUTKA_STATE_KB1")
        PrmDr("EDIOUTKA_STATE_KB2") = dr.Item("EDIOUTKA_STATE_KB2")
        PrmDr("EDIOUTKA_STATE_KB3") = dr.Item("EDIOUTKA_STATE_KB3")
        PrmDr("EDIOUTKA_STATE_KB4") = dr.Item("EDIOUTKA_STATE_KB4")
        PrmDr("EDIOUTKA_STATE_KB5") = dr.Item("EDIOUTKA_STATE_KB5")
        PrmDr("EDIOUTKA_STATE_KB6") = dr.Item("EDIOUTKA_STATE_KB6")
        PrmDr("EDIOUTKA_STATE_KB7") = dr.Item("EDIOUTKA_STATE_KB7")
        PrmDr("EDIOUTKA_STATE_KB8") = dr.Item("EDIOUTKA_STATE_KB8")
        PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
        PrmDr("WH_CD") = dr.Item("WH_CD")
        PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
        PrmDr("TANTO_CD") = dr.Item("TANTO_CD")
        PrmDr("DEST_CD") = dr.Item("DEST_CD")
        PrmDr("EDI_DATE_FROM") = dr.Item("EDI_DATE_FROM")
        PrmDr("EDI_DATE_TO") = dr.Item("EDI_DATE_TO")
        PrmDr("SEARCH_DATE_KBN") = dr.Item("SEARCH_DATE_KBN")
        PrmDr("SEARCH_DATE_FROM") = dr.Item("SEARCH_DATE_FROM")
        PrmDr("SEARCH_DATE_TO") = dr.Item("SEARCH_DATE_TO")
        PrmDr("CUST_ORD_NO") = dr.Item("CUST_ORD_NO")
        PrmDr("CUST_NM") = dr.Item("CUST_NM")
        PrmDr("DEST_NM") = dr.Item("DEST_NM")
        PrmDr("REMARK") = dr.Item("REMARK")
        PrmDr("UNSO_ATT") = dr.Item("UNSO_ATT")
        PrmDr("GOODS_NM") = dr.Item("GOODS_NM")
        PrmDr("DEST_AD") = dr.Item("DEST_AD")
        PrmDr("UNSO_NM") = dr.Item("UNSO_NM")
        PrmDr("BIN_KB") = dr.Item("BIN_KB")
        PrmDr("EDI_CTL_NO") = dr.Item("EDI_CTL_NO")
        PrmDr("MATOME_NO") = dr.Item("MATOME_NO")
        PrmDr("OUTKA_CTL_NO") = dr.Item("OUTKA_CTL_NO")
        PrmDr("BUYER_ORD_NO") = dr.Item("BUYER_ORD_NO")
        PrmDr("SYUBETU_KB") = dr.Item("SYUBETU_KB")
        PrmDr("UNSO_MOTO_KB") = dr.Item("UNSO_MOTO_KB")
        PrmDr("TANTO_USER") = dr.Item("TANTO_USER")
        PrmDr("SYS_ENT_USER") = dr.Item("SYS_ENT_USER")
        PrmDr("SYS_UPD_USER") = dr.Item("SYS_UPD_USER")
        PrmDr("SYS_UPD_DATE") = dr.Item("SYS_UPD_DATE")
        PrmDr("SYS_UPD_TIME") = dr.Item("SYS_UPD_TIME")
        PrmDr("RCV_SYS_UPD_DATE") = dr.Item("RCV_SYS_UPD_DATE")
        PrmDr("RCV_SYS_UPD_TIME") = dr.Item("RCV_SYS_UPD_TIME")
        PrmDr("EDI_CUST_INDEX") = dr.Item("EDI_CUST_INDEX")
        PrmDr("AUTO_MATOME_FLG") = dr.Item("AUTO_MATOME_FLG")
        PrmDr("SHORI_FLG") = dr.Item("SHORI_FLG")
        PrmDr("ORDER_CHECK_FLG") = dr.Item("ORDER_CHECK_FLG")
        PrmDr("OUTKA_L_UPD_FLG") = dr.Item("OUTKA_L_UPD_FLG")
        PrmDr("ROW_NO") = dr.Item("ROW_NO")
        PrmDr("JYOTAI_KB") = dr.Item("JYOTAI_KB")
        PrmDr("HORYU_KB") = dr.Item("HORYU_KB")
        PrmDr("RCV_NM_HED") = dr.Item("RCV_NM_HED")
        PrmDr("RCV_NM_DTL") = dr.Item("RCV_NM_DTL")
        PrmDr("RCV_NM_EXT") = dr.Item("RCV_NM_EXT")
        PrmDr("SND_NM") = dr.Item("SND_NM")

        PrmDt.Rows.Add(PrmDr)

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        PrmDs.Merge(New RdPrevInfoDS)

        'LMH510の呼び出し
        PrmDs = MyBase.CallBLC(Me._Print510, "DoPrint", PrmDs)

        Return PrmDs

    End Function
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
        Dim dt As DataTable = ds.Tables(LMH030BLF.TABLE_NM_OUTPUTIN)
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
            Dim prtflgDs As DataSet = New LMH030DS()

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

            outPutflg = dr.Item("OUTPUT_SHUBETU").ToString()     '印刷種別(LMH030Cの出力種別)

            'setBlc = getBLC(Convert.ToInt32(dr.Item("EDI_CUST_INDEX")))
            setBlc = New LMH030BLC

            '2012.03.18 大阪対応START
            Select Case outPutflg

                Case "03"  '出荷ＥＤＩ受信帳票
                    'ds = Me.PrintLMH560(dr)

                    ''ダウケミ(高石以外)の場合には、立会い伝票も併せて出力
                    ''LMH560IN,LMH561INにセット
                    'If dr.Item("EDI_CUST_INDEX").ToString().Equals("43") = True Then

                    '    prtBlc = New Com.Base.BaseBLC() {New LMH560BLC(), New LMH561BLC()}
                    '    setDs = New DataSet() {Me.PrintLMH560(dr), Me.PrintLMH561(dr)}

                    'Else
                    '    '上記でない場合は、受信伝票のみ
                    '    'LMH560INにセット
                    '    prtBlc = New Com.Base.BaseBLC() {New LMH560BLC()}
                    '    setDs = New DataSet() {Me.PrintLMH560(dr)}
                    'End If

                    Select Case ediCustIdx
                        Case "43"     'ダウケミ(大阪)

                            'ダウケミ(高石以外)の場合には、立会い伝票も併せて出力
                            'LMH560IN,LMH561INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH560BLC(), New LMH561BLC()}
                            setDs = New DataSet() {Me.PrintLMH560(dr), Me.PrintLMH561(dr)}

                        Case "44"       'ダウケミ(高石)
                            '上記でない場合は、受信伝票のみ
                            'LMH560INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH560BLC()}
                            setDs = New DataSet() {Me.PrintLMH560(dr)}

                            '2012/11/02日産物流開始
                        Case "32"       '日産物流
                            '上記でない場合は、受信伝票のみ
                            'LMH562INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH562BLC()}
                            setDs = New DataSet() {Me.PrintLMH562(dr)}
                            '2012/11/02日産物流終了

                            '2012/11/15旭化成(ケミカルズ)開始
                        Case "61"       '旭化成ケミカルズ
                            '上記でない場合は、受信伝票のみ
                            'LMH564INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH564BLC()}
                            setDs = New DataSet() {Me.PrintLMH564(dr)}
                            '2012/11/15旭化成(ケミカルズ)終了

                            '2012/11/15旭化成(イーマテリアルズ)開始
                        Case "33"       '旭化成イーマテリアルズ
                            '上記でない場合は、受信伝票のみ
                            'LMH564INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH564BLC()}
                            setDs = New DataSet() {Me.PrintLMH564(dr)}
                            '2012/11/15旭化成(イーマテリアルズ)終了

                        Case Else

                            'Return rtnDs
                            Continue For

                    End Select

                Case "04" '出荷受信一覧表

                    Select Case ediCustIdx

                        Case "9", "10", "24"       '大日精化(埼玉)受信一覧表
                            dt.Rows(0).Item("BIKO_STR_1") = "AND DEL_KB <> '1'"
                            ''LMH573INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH573BLC()}
                            '要望番号1102 2012.06.11 修正START
                            'setDs = New DataSet() {Me.PrintLMH573(dr)}
                            setDs = New DataSet() {Me.PrintLMH573(dt)}
                            loopFlg = False
                            '要望番号1102 2012.06.11 修正END

                        Case "28", "26"       'ディック大阪受信一覧表(キャンセル分含む)

                            'プリントフラグ更新時、DIC共同配送より作成されたバッチID(STB0180)は除く
                            dt.Rows(0).Item("BIKO_STR_1") = "AND SYS_ENT_PGID <> 'STB0180'"
                            ''LMH570INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH570BLC()}
                            '要望番号1102 2012.06.11 修正START
                            'setDs = New DataSet() {Me.PrintLMH570(dr)}
                            setDs = New DataSet() {Me.PrintLMH570(dt)}
                            loopFlg = False
                            '要望番号1102 2012.06.11 修正END

                        Case "3", "91"       '浮間合成(大阪・埼玉)受信一覧表
                            dt.Rows(0).Item("BIKO_STR_1") = "AND DEL_KB <> '3'"
                            ''LMH571INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH571BLC()}
                            '要望番号1102 2012.06.11 修正START
                            'setDs = New DataSet() {Me.PrintLMH571(dr)}
                            setDs = New DataSet() {Me.PrintLMH571(dt)}
                            loopFlg = False
                            '要望番号1102 2012.06.11 修正END

                        Case Else
                            'Return rtnDs
                            Continue For

                    End Select

                Case "05" '出荷伝票

                    Select Case ediCustIdx

                        Case "9", "10", "24"       '大日精化(埼玉)出荷伝票
                            dt.Rows(0).Item("BIKO_STR_1") = "AND DEL_KB <> '1'"
                            'LMH551INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH551BLC()}
                            setDs = New DataSet() {Me.PrintLMH551(dr)}

                        Case "3", "91"       '浮間合成(大阪・埼玉)出荷伝票

                            'LMH550INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH550BLC()}
                            setDs = New DataSet() {Me.PrintLMH550(dr)}

                            '2012.06.18 追加START
                        Case "4"       'ゴードー溶剤(埼玉)出荷伝票

                            'LMH552INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH552BLC()}
                            setDs = New DataSet() {Me.PrintLMH552(dr)}
                            '2012.06.18 追加END

                        Case Else
                            'Return rtnDs
                            Continue For

                    End Select
                    '2012.09.12 要望番号1429 追加START
                Case "07" 'EDI出荷取消チェックリスト

                    Select Case ediCustIdx

                        Case "25", "26"      'DIC物流群馬

                            'LMH540INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH540BLC()}
                            'setDs = New DataSet() {Me.PrintLMH540(dr)}
                            setDs = New DataSet() {Me.PrintLMH540(dt)}
                            loopFlg = False

                        Case Else
                            'Return rtnDs
                            Continue For

                    End Select
                    '2012.09.12 要望番号1429 追加END

                    '(2012.12.07) 埼玉BP納品書追加　START
                Case "08" 'EDI納品送状

                    Select Case ediCustIdx

                        Case "77"       'ビーピー・カストロール株式会社

                            'LMH580INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH580BLC()}
                            'setDs = New DataSet() {Me.PrintLMH580(dr)}
                            setDs = New DataSet() {Me.PrintLMH580(dt)}
                            loopFlg = False

                        Case Else
                            'Return rtnDs
                            Continue For

                    End Select

                Case "09" 'EDI納品書(オートバックス用)

                    Select Case ediCustIdx

                        Case "77"       'ビーピー・カストロール株式会社

                            'LMH581INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH581BLC()}
                            'setDs = New DataSet() {Me.PrintLMH581(dr)}
                            setDs = New DataSet() {Me.PrintLMH581(dt)}
                            loopFlg = False

                        Case Else
                            'Return rtnDs
                            Continue For

                    End Select

                Case "10" 'EDI納品書(タクティー用)

                    Select Case ediCustIdx

                        Case "77"       'ビーピー・カストロール株式会社

                            'LMH582INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH582BLC()}
                            'setDs = New DataSet() {Me.PrintLMH582(dr)}
                            setDs = New DataSet() {Me.PrintLMH582(dt)}
                            loopFlg = False

                        Case Else
                            'Return rtnDs
                            Continue For

                    End Select

                Case "11" 'EDI納品書(イエローハット用)

                    Select Case ediCustIdx

                        Case "77"       'ビーピー・カストロール株式会社

                            'LMH583INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH583BLC()}
                            'setDs = New DataSet() {Me.PrintLMH583(dr)}
                            setDs = New DataSet() {Me.PrintLMH583(dt)}
                            loopFlg = False

                        Case Else
                            'Return rtnDs
                            Continue For

                    End Select
                    '(2012.12.07) 埼玉BP納品書追加　END

                    '(2012.12.25) 大阪_日興産業 EDI納品書 追加 -- START --
                Case "12" 'EDI納品書(ｲｴﾛｰﾊｯﾄ用)

                    Select Case ediCustIdx
                        Case "98"       '日興産業株式会社
                            'LMH585INのDataSetにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH585BLC()}
                            setDs = New DataSet() {Me.PrintLMH585(dt)}
                            loopFlg = False

                        Case Else
                            Continue For

                    End Select
                    '(2012.12.25) 大阪_日興産業 EDI納品書 追加 --  END  --

                    '(2012.12.25) ロンザ納品書送り状(千葉) START
                Case "13" 'ロンザ納品書送り状(千葉)

                    Select Case ediCustIdx

                        Case "84"       'ロンザジャパン

                            'LMH584INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH584BLC()}
                            'setDs = New DataSet() {Me.PrintLMH584(dr)}
                            setDs = New DataSet() {Me.PrintLMH584(dt)}
                            loopFlg = False

                        Case Else
                            'Return rtnDs
                            Continue For

                    End Select
                    '(2012.12.25) ロンザ納品書送り状(千葉)  END 


                    'オートバックス専用納品送状　2014.02.01 追加START
                Case "14" 'EDI納品送状(オートバックス用)

                    Select Case ediCustIdx

                        Case "77"       'ビーピー・カストロール株式会社

                            'LMH587INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH587BLC()}
                            'setDs = New DataSet() {Me.PrintLMH580(dr)}
                            setDs = New DataSet() {Me.PrintLMH587(dt)}
                            loopFlg = False

                        Case Else
                            'Return rtnDs
                            Continue For

                    End Select

                    '(2015.01.21) テルモ仕切書(千葉) START
                Case "15" 'テルモ仕切書(千葉)

                    'LMH588INにセット
                    prtBlc = New Com.Base.BaseBLC() {New LMH588BLC()}
                    setDs = New DataSet() {Me.PrintLMH588(dt)}
                    loopFlg = False

                    '(2015.01.21) テルモ仕切書(千葉)  END 

                    '(2015.02.26) 東レ・ダウEDI荷札(千葉) START
                Case "16" '東レ・ダウEDI荷札(千葉)

                    'LMH590INにセット
                    prtBlc = New Com.Base.BaseBLC() {New LMH590BLC()}
                    setDs = New DataSet() {Me.PrintLMH590(dt)}
                    loopFlg = False

                    '(2015.02.26) 東レ・ダウEDI荷札(千葉)  END 

#If True Then ' BP運送会社自動設定対応 20161118 added by inoue
                Case OUTPUT_SHUBETU.EDI_INVOICE_BP_NIPPON_EXPRESS
                    prtBlc = New Com.Base.BaseBLC() {New LMH600BLC()}
                    setDs = New DataSet() {Me.PrintLMH600(dt)}
                    loopFlg = False
#End If

#If True Then ' 日本合成対応(2646) 20170113 added by inoue
                Case OUTPUT_SHUBETU.EDI_DELIVERY_NOTE_NICHIGO
                    prtBlc = New Com.Base.BaseBLC() {New LMH589BLC()}
                    setDs = New DataSet() {Me.PrintLMH589(dt)}
                    loopFlg = False
#End If

                Case "99"   '一括印刷

                    Select Case ediCustIdx

                        Case "9", "10", "24"     '大日精化(埼玉)
                            dt.Rows(0).Item("BIKO_STR_1") = "AND DEL_KB <> '1'"
                            'LMH560IN,LMH561INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH551BLC(), New LMH573BLC()}
                            '要望番号1102 2012.06.11 修正START
                            'setDs = New DataSet() {Me.PrintLMH551(dr), Me.PrintLMH573(dr)}
                            setDs = New DataSet() {Me.PrintLMH551(dr), Me.PrintLMH573(dt)}
                            '要望番号1102 2012.06.11 修正END

                        Case "28"       'ディック大阪受信一覧表(キャンセル分含む)

                            'プリントフラグ更新時、DIC共同配送より作成されたバッチID(STB0180)は除く
                            dt.Rows(0).Item("BIKO_STR_1") = "AND FILE_DIST <> 'STB0180'"
                            ''LMH570INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH570BLC()}
                            '要望番号1102 2012.06.11 修正START
                            'setDs = New DataSet() {Me.PrintLMH570(dr)}
                            setDs = New DataSet() {Me.PrintLMH570(dt)}
                            '要望番号1102 2012.06.11 修正END

                        Case "43"     'ダウケミ(大阪)

                            'ダウケミ(高石以外)の場合には、立会い伝票も併せて出力
                            'LMH560IN,LMH561INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH560BLC(), New LMH561BLC()}
                            setDs = New DataSet() {Me.PrintLMH560(dr), Me.PrintLMH561(dr)}

                        Case "44"       'ダウケミ(高石)
                            '上記でない場合は、受信伝票のみ
                            'LMH560INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH560BLC()}
                            setDs = New DataSet() {Me.PrintLMH560(dr)}

                        Case "3", "91"       '浮間合成(大阪・埼玉)受信一覧表,浮間合成(大阪・埼玉)出荷伝票
                            dt.Rows(0).Item("BIKO_STR_1") = "AND DEL_KB <> '3'"
                            ''LMH571INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH550BLC(), New LMH571BLC()}
                            '要望番号1102 2012.06.11 修正START
                            'setDs = New DataSet() {Me.PrintLMH550(dr), Me.PrintLMH571(dr)}
                            setDs = New DataSet() {Me.PrintLMH550(dr), Me.PrintLMH571(dt)}
                            '要望番号1102 2012.06.11 修正START

                            '2012.06.18 追加START
                        Case "4"       'ゴードー溶剤(埼玉)出荷伝票

                            'LMH552INにセット
                            prtBlc = New Com.Base.BaseBLC() {New LMH552BLC()}
                            setDs = New DataSet() {Me.PrintLMH552(dr)}
                            '2012.06.18 追加END

                        Case Else

                            'Return rtnDs
                            Continue For

                    End Select
                    '要望番号1007 2012.05.08 追加END

                Case Else

            End Select

            '2012.03.18 大阪対応END

            '2012.03.18 大阪START
            '入替を行った後に、データセットを入れ替える
            '印刷フラグ更新用にデータセットを入替
            prtflgDs.Merge(ds)
            '2012.03.18 大阪START

            '要望番号1151 2012.06.14　修正START(一括印刷不具合)
            'rtnDs = Me.PrintBlcFunc(prtBlc, setDs, ds)
            rtnDs = Me.PrintBlcFunc(prtBlc, setDs, ds, setBlc)
            '要望番号1151 2012.06.14　修正END

            ''要望番号1007 2012.05.08 コメントSTART
            ''2012.03.19 修正START
            'If MyBase.IsMessageExist() = True Then
            '    Return rtnDs
            'End If
            ''要望番号1007 2012.05.08 コメントEND

            '要望番号:1444 terakawa 2012.09.18 Start


#If False Then ' 日本合成化学対応(2646) 20170116 changed by inoue
            'EDI出荷取消チェックリストの場合は受信テーブルを更新しない
            If outPutflg.Equals("07") = False Then
#Else
            'EDI出荷取消チェックリストの場合は受信テーブルを更新しない
            If (outPutflg.Equals("07") = False AndAlso _
                OUTPUT_SHUBETU.EDI_DELIVERY_NOTE_NICHIGO.Equals(outPutflg) = False
                ) Then
#End If
                '要望番号1007 2012.05.08 修正START⇒要望番号1062 2012.05.16 コメント外す
                If dr.Item("PRTFLG").ToString().Equals("1") = False Then

                    '印刷フラグの更新
                    prtflgDs = MyBase.CallBLC(setBlc, "PrintFlagUpDate", prtflgDs)
                    'If MyBase.GetResultCount = 0 Then
                    '    MyBase.SetMessage(Nothing)
                    'End If

                End If
                '2012.03.19 修正END
                '要望番号1007 2012.05.08 修正END⇒要望番号1062 2012.05.16 コメント外す
            End If
            '要望番号:1444 terakawa 2012.09.18 End

            ''要望番号1151 2012.06.14　コメント化START(一括印刷不具合におけるロジック移動)
            ''EDI印刷対象テーブルの追加(既に存在する場合は削除⇒追加)
            'rtnDs = MyBase.CallBLC(setBlc, "DeleteInsertHEdiPrint", rtnDs)
            ''要望番号1151 2012.06.14　コメント化END

            '(2012.02.08)要望番号1822 印刷件数の取得 -- START --
            If ediCustIdx.ToString.Equals("77") = True Then
                'BP･カストロール株式会社のみ対象

                Select Case outPutflg

                    Case "08"
                        'LMH580:納品送り状
                        ds = Me.PrintCountaSet(rtnDs, ds, "LMH580OUT")

                    Case "09"
                        'LMH581:EDI納品書(ｵｰﾄﾊﾞｯｸｽ)
                        ds = Me.PrintCountaSet(rtnDs, ds, "LMH581OUT")

                    Case "10"
                        'LMH582:EDI納品書(ﾀｸﾃｨ)
                        ds = Me.PrintCountaSet(rtnDs, ds, "LMH582OUT")

                    Case "11"
                        'LMH583:EDI品書(ｲｴﾛｰ)
                        ds = Me.PrintCountaSet(rtnDs, ds, "LMH583OUT")
                    Case "14"
                        'LMH587:納品送り状【オートバックス専用】
                        ds = Me.PrintCountaSet(rtnDs, ds, "LMH587OUT")

                    Case OUTPUT_SHUBETU.EDI_INVOICE_BP_NIPPON_EXPRESS
                        'LMH600:日通送り状
                        ds = Me.PrintCountaSet(rtnDs, ds, LMH600BLC.OUT_TABLE_NAME)

                End Select

            End If
            '(2012.02.08)要望番号1822 印刷件数の取得 --  END --

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

    '(2012.02.08)要望番号1822 印刷件数の取得 -- START --
    ''' <summary>
    ''' データセットの編集を行う。
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintCountaSet(ByVal rtnDs As DataSet, ByVal ds As DataSet, ByVal DataSetNM As String) As DataSet

        Dim outDt As DataTable = rtnDs.Tables(DataSetNM)
        Dim max As Integer = outDt.Rows.Count - 1

        Dim cntDt As DataTable = ds.Tables("LMH030_PRT_CNT")
        Dim cntDr As DataRow = cntDt.NewRow()

        Dim prtCnt As Integer = 0
        Dim NewCtlNo As String = String.Empty
        Dim OldCtlNo As String = String.Empty
        Dim NewOrdNo As String = String.Empty
        Dim OldOrdNo As String = String.Empty
        Dim RPT_ID As String = String.Empty         'ADD 2017/02/08
        Dim RPT_ID_LMH583 As String = "LMH583"      'ADD 2017/02/08 BPｲｴﾛｰﾊｯﾄ納品書兼請求明細書

        '2013.06.17 追加START
        Dim outPutCnt As Integer = 0
        '2013.06.17 追加END

        '印刷件数の取得(EDI出荷管理番号単位)
        For i As Integer = 0 To max

#If True Then ' BP運送会社自動設定対応 20170110 added by inoue
            ' 日通送り状は、ループの外で設定する
            If (LMH600BLC.OUT_TABLE_NAME.Equals(DataSetNM)) Then
                Exit For
            End If
#End If

            'EDI出荷管理番号/オーダー番号をセット
            NewCtlNo = outDt.Rows(i).Item("EDI_CTL_NO").ToString
            NewOrdNo = outDt.Rows(i).Item("DTL_CUST_ORD_NO").ToString

            RPT_ID = outDt.Rows(i).Item("RPT_ID").ToString      'ADD 2017/02/08

            'EDI出荷管理番号が異なればCountUp
            If NewCtlNo <> OldCtlNo Then
                '但し、オーダー№が同一ならば、割引伝票とみなしCountUp対象外とする
                If NewOrdNo <> OldOrdNo Then
                    prtCnt = prtCnt + 1
                Else
                    'UPD 2017/02/08　Start
                    'BPｲｴﾛｰﾊｯﾄ納品書兼請求明細書は、オーダー№が同一でもCountUpする(但し、割引伝票はCountUp対象外とする)
                    If RPT_ID_LMH583.Equals(RPT_ID) = True _
                       And (outDt.Rows(i).Item("ROW_TYPE").ToString.Equals("U") = False _
                            And outDt.Rows(i).Item("ROW_TYPE").ToString.Equals("V") = False _
                            And outDt.Rows(i).Item("ROW_TYPE").ToString.Equals("W") = False) Then

                        prtCnt = prtCnt + 1

                    End If
                End If
            End If
            'UPD 2017/02/08　End

            ''2013.06.17 追加START
            Select Case DataSetNM
                Case "LMH580OUT", "LMH582OUT", "LMH587OUT"
                    outPutCnt = outPutCnt + Convert.ToInt32(outDt.Rows(i).Item("OUTPUT_CNT").ToString)
                Case Else

            End Select
            ''2013.06.17 追加END

            OldCtlNo = NewCtlNo
            OldOrdNo = NewOrdNo

        Next

        ''2013.06.17 追加START
        Select Case DataSetNM

            Case "LMH580OUT", "LMH582OUT", "LMH587OUT"
                Exit Select

            Case "LMH581OUT", "LMH583OUT"

                Dim countDt As DataTable = rtnDs.Tables("CNT_TBL")
                Dim maxC As Integer = countDt.Rows.Count - 1

                For j As Integer = 0 To maxC
                    outPutCnt = outPutCnt + Convert.ToInt32(countDt.Rows(j).Item("OUTPUT_CNT").ToString)
                Next

#If True Then ' BP運送会社自動設定対応 20170110 added by inoue
            Case LMH600BLC.OUT_TABLE_NAME
                ' 日通送り状の印刷枚数と伝票数設定を設定

                ' EDI管理番号毎に内訳数を取得
                Dim groupByEdiCtrlNoData As IEnumerable(Of Integer) = _
                    rtnDs.Tables(LMH600BLC.OUT_TABLE_NAME).AsEnumerable() _
                        .GroupBy(Function(s) s.Item("EDI_CTL_NO")) _
                        .Select(Function(s) s.Count())

                ' 伝票数
                prtCnt = groupByEdiCtrlNoData.Count

                ' 印刷枚数(最小値は伝票数と同じ)
                outPutCnt = prtCnt


                ' 二ページ以上となる伝票を抽出して、印刷枚数に加算
                For Each detailCount As Integer In groupByEdiCtrlNoData _
                            .Where(Function(r) r > LMH600BLC.MAX_DETAIL_COUNT_BY_PAGE)

                    ' 余り
                    Dim remainder As Integer = 0

                    ' 商
                    Dim quotient As Integer = Math.DivRem(detailCount _
                                                        , LMH600BLC.MAX_DETAIL_COUNT_BY_PAGE _
                                                        , remainder)

                    If (quotient > 1) Then
                        ' 一ページ目は、加算済みなので、残りのページを加算
                        outPutCnt += (quotient - 1)
                    End If

                    ' 余りがある場合は、一ページ加算
                    If (remainder > 0) Then
                        outPutCnt += 1
                    End If

                Next
#End If
            Case Else

        End Select
        ''2013.06.17 追加END

        cntDt.Rows.Add(cntDr)
        cntDt.Rows(0).Item("PRT_CNT") = prtCnt.ToString

        '2013.06.17 追加START
        cntDt.Rows(0).Item("OUTPUT_CNT") = outPutCnt.ToString
        '2013.06.17 追加END

        Return ds

    End Function
    '(2012.02.08)要望番号1822 印刷件数の取得 --  END --

    '2012.09.12 要望番号1429 追加START
    ''' <summary>
    '''  EDI出荷取消チェックリスト(LMH540IN)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH540(ByVal dt As DataTable) As DataSet

        ' EDI出荷取消チェックリスト
        Dim PrmDs As DataSet = New LMH540DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH540IN")
        Dim PrmDr As DataRow

        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow

        For i As Integer = 0 To max

            dr = dt.Rows(i)
            PrmDr = PrmDt.NewRow()

            'LMH540INにLMH030_OUTPUTINの値を設定
            PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
            PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
            PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
            PrmDr("EDI_CTL_NO") = dr.Item("EDI_CTL_NO")
            PrmDr("SEARCH_DATE_FROM") = dr.Item("CRT_DATE_FROM")
            PrmDr("SEARCH_DATE_TO") = dr.Item("CRT_DATE_TO")
            PrmDr("INOUT_KB") = dr.Item("INOUT_KB")
            PrmDr("PRTFLG") = dr.Item("PRTFLG")
            PrmDr("ROW_NO") = dr.Item("ROW_NO")
            PrmDr("OUTPUT_SHUBETU") = dr.Item("OUTPUT_SHUBETU")
            '要望番号:1446 terakawa 2012.09.19 Start
            PrmDr("DEL_KB") = dr.Item("DEL_KB")
            '要望番号:1446 terakawa 2012.09.19 End

            PrmDt.Rows.Add(PrmDr)

        Next

        Return PrmDs

    End Function

    '2012.09.12 要望番号1429 追加END
    '2012.03.18 大阪対応START
    ''' <summary>
    '''  出荷伝票(LMH550IN)
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH550(ByVal dr As DataRow) As DataSet

        ' 出荷伝票
        Dim PrmDs As DataSet = New LMH550DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH550IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        'LMH550INにLMH030_OUTPUTINの値を設定
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
    '2012.03.18 大阪対応END

    '2012.06.06 埼玉対応START
    ''' <summary>
    '''  出荷伝票(LMH551IN)
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH551(ByVal dr As DataRow) As DataSet

        ' 出荷伝票
        Dim PrmDs As DataSet = New LMH551DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH551IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        'LMH551INにLMH030_OUTPUTINの値を設定
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
    '2012.06.06 埼玉対応END

    '2012.06.18 埼玉対応START
    ''' <summary>
    '''  出荷伝票(LMH552IN)
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH552(ByVal dr As DataRow) As DataSet

        ' 出荷伝票
        Dim PrmDs As DataSet = New LMH552DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH552IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        'LMH552INにLMH030_OUTPUTINの値を設定
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
        PrmDr("OUTPUT_SHUBETU") = dr.Item("OUTPUT_SHUBETU")
        PrmDr("EDI_CTL_NO") = dr.Item("EDI_CTL_NO")
        PrmDr("ROW_NO") = dr.Item("ROW_NO")
        PrmDr("DENPYO_NO") = dr.Item("ORDER_NO")

        PrmDt.Rows.Add(PrmDr)

        Return PrmDs

    End Function
    '2012.06.18 埼玉対応END

    ''' <summary>
    '''  出荷ＥＤＩ受信帳票(LMH560IN)
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH560(ByVal dr As DataRow) As DataSet

        ' 出荷ＥＤＩ受信帳票
        Dim PrmDs As DataSet = New LMH560DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH560IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        'LMH560INにLMH030_OUTPUTINの値を設定
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

        'LMH561INにLMH030_OUTPUTINの値を設定
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
    '''  出荷ＥＤＩ受信帳票(LMH562IN)
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH562(ByVal dr As DataRow) As DataSet

        ' 出荷ＥＤＩ受信帳票
        Dim PrmDs As DataSet = New LMH562DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH562IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        'LMH562INにLMH030_OUTPUTINの値を設定
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
    '''  出荷ＥＤＩ受信帳票(LMH564IN)
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH564(ByVal dr As DataRow) As DataSet

        ' 出荷ＥＤＩ受信帳票
        Dim PrmDs As DataSet = New LMH564DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH564IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        'LMH562INにLMH030_OUTPUTINの値を設定
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
    '''  受信一覧表(LMH570IN)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH570(ByVal dt As DataTable) As DataSet

        ' 受信一覧表
        Dim PrmDs As DataSet = New LMH570DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH570IN")
        Dim PrmDr As DataRow

        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow

        For i As Integer = 0 To max

            dr = dt.Rows(i)
            PrmDr = PrmDt.NewRow()

            'LMH570INにLMH030_OUTPUTINの値を設定
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

            PrmDr("YAKUJO_NO") = dr.Item("YAKUJO_NO")

            PrmDt.Rows.Add(PrmDr)

        Next

        Return PrmDs

    End Function

    'Private Function PrintLMH570(ByVal dr As DataRow) As DataSet

    '    ' 受信一覧表
    '    Dim PrmDs As DataSet = New LMH570DS()
    '    Dim PrmDt As DataTable = PrmDs.Tables("LMH570IN")
    '    Dim PrmDr As DataRow = PrmDt.NewRow()

    '    'LMH570INにLMH030_OUTPUTINの値を設定
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

    '2012.03.18 大阪対応START
    ''' <summary>
    '''  受信一覧表(LMH571IN)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH571(ByVal dt As DataTable) As DataSet

        ' 受信一覧表
        Dim PrmDs As DataSet = New LMH571DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH571IN")
        Dim PrmDr As DataRow

        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow

        For i As Integer = 0 To max

            dr = dt.Rows(i)
            PrmDr = PrmDt.NewRow()

            'LMH571INにLMH030_OUTPUTINの値を設定
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

    'Private Function PrintLMH571(ByVal dr As DataRow) As DataSet

    '    ' 受信一覧表
    '    Dim PrmDs As DataSet = New LMH571DS()
    '    Dim PrmDt As DataTable = PrmDs.Tables("LMH571IN")
    '    Dim PrmDr As DataRow = PrmDt.NewRow()

    '    'LMH571INにLMH030_OUTPUTINの値を設定
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

    '2012.06.06 埼玉対応START
    ''' <summary>
    '''  受信一覧表(LMH573IN)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH573(ByVal dt As DataTable) As DataSet

        ' 受信一覧表
        Dim PrmDs As DataSet = New LMH573DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH573IN")
        Dim PrmDr As DataRow

        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow

        For i As Integer = 0 To max

            dr = dt.Rows(i)
            PrmDr = PrmDt.NewRow()

            'LMH573INにLMH030_OUTPUTINの値を設定
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

    'Private Function PrintLMH573(ByVal dr As DataRow) As DataSet

    '    ' 受信一覧表
    '    Dim PrmDs As DataSet = New LMH573DS()
    '    Dim PrmDt As DataTable = PrmDs.Tables("LMH573IN")
    '    Dim PrmDr As DataRow = PrmDt.NewRow()

    '    'LMH573INにLMH030_OUTPUTINの値を設定
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

    '2012.06.06 埼玉対応END

    '(2012.12.07) 埼玉 EDI納品送状(BP)  ━━━━━START━━━━━
    ''' <summary>
    ''' EDI納品送状(LMH580IN)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH580(ByVal dt As DataTable) As DataSet

        'EDI納品送状
        Dim PrmDs As DataSet = New LMH580DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH580IN")
        Dim PrmDr As DataRow

        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow

        For i As Integer = 0 To max

            dr = dt.Rows(i)
            PrmDr = PrmDt.NewRow()

            'LMH580INにLMH030_OUTPUTINの値を設定
            PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
            PrmDr("WH_CD") = dr.Item("WH_CD")
            PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
            PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
            '2012.12.17 出荷予定日 START
            PrmDr("OUTKA_PLAN_DATE_FROM") = dr.Item("CRT_DATE_FROM")
            PrmDr("OUTKA_PLAN_DATE_TO") = dr.Item("CRT_DATE_TO")
            '2012.12.17 出荷予定日  END
            'PrmDr("CRT_DATE_FROM") = dr.Item("CRT_DATE_FROM")
            'PrmDr("CRT_DATE_TO") = dr.Item("CRT_DATE_TO")
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
    '(2012.12.07) 埼玉 EDI納品送状(BP)  ━━━━━ END ━━━━━    

    '(2012.12.07) 埼玉 EDI納品書(BP_オートバックス)  ━━━━━START━━━━━
    ''' <summary>
    ''' EDI納品書(LMH581IN)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH581(ByVal dt As DataTable) As DataSet

        'EDI納品書
        Dim PrmDs As DataSet = New LMH581DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH581IN")
        Dim PrmDr As DataRow

        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow

        For i As Integer = 0 To max

            dr = dt.Rows(i)
            PrmDr = PrmDt.NewRow()

            'LMH581INにLMH030_OUTPUTINの値を設定
            PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
            PrmDr("WH_CD") = dr.Item("WH_CD")
            PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
            PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
            '2012.12.17 出荷予定日 START
            PrmDr("OUTKA_PLAN_DATE_FROM") = dr.Item("CRT_DATE_FROM")
            PrmDr("OUTKA_PLAN_DATE_TO") = dr.Item("CRT_DATE_TO")
            '2012.12.17 出荷予定日  END
            'PrmDr("CRT_DATE_FROM") = dr.Item("CRT_DATE_FROM")
            'PrmDr("CRT_DATE_TO") = dr.Item("CRT_DATE_TO")
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
    '(2012.12.07) 埼玉 EDI納品書(BP_オートバックス)  ━━━━━ END ━━━━━

    '(2012.12.07) 埼玉 EDI納品書(BP_タクティ) ━━━━━START━━━━━
    ''' <summary>
    ''' EDI納品書((LMH582IN)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH582(ByVal dt As DataTable) As DataSet

        'EDI納品書
        Dim PrmDs As DataSet = New LMH582DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH582IN")
        Dim PrmDr As DataRow

        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow

        For i As Integer = 0 To max

            dr = dt.Rows(i)
            PrmDr = PrmDt.NewRow()

            'LMH582INにLMH030_OUTPUTINの値を設定
            PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
            PrmDr("WH_CD") = dr.Item("WH_CD")
            PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
            PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
            '2012.12.17 出荷予定日 START
            PrmDr("OUTKA_PLAN_DATE_FROM") = dr.Item("CRT_DATE_FROM")
            PrmDr("OUTKA_PLAN_DATE_TO") = dr.Item("CRT_DATE_TO")
            '2012.12.17 出荷予定日  END
            'PrmDr("CRT_DATE_FROM") = dr.Item("CRT_DATE_FROM")
            'PrmDr("CRT_DATE_TO") = dr.Item("CRT_DATE_TO")
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
    '(2012.12.07) 埼玉 EDI納品書(BP_タクティ)    ━━━━━ END ━━━━━

    '(2012.12.07) 埼玉 EDI納品書(BP_イエローハット)    ━━━━━START━━━━━
    ''' <summary>
    '''  EDI納品書(LMH583IN)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH583(ByVal dt As DataTable) As DataSet

        'EDI納品書
        Dim PrmDs As DataSet = New LMH583DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH583IN")
        Dim PrmDr As DataRow

        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow

        For i As Integer = 0 To max

            dr = dt.Rows(i)
            PrmDr = PrmDt.NewRow()

            'LMH583INにLMH030_OUTPUTINの値を設定
            PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
            PrmDr("WH_CD") = dr.Item("WH_CD")
            PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
            PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
            '2012.12.17 出荷予定日 START
            PrmDr("OUTKA_PLAN_DATE_FROM") = dr.Item("CRT_DATE_FROM")
            PrmDr("OUTKA_PLAN_DATE_TO") = dr.Item("CRT_DATE_TO")
            '2012.12.17 出荷予定日  END
            'PrmDr("CRT_DATE_FROM") = dr.Item("CRT_DATE_FROM")
            'PrmDr("CRT_DATE_TO") = dr.Item("CRT_DATE_TO")
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
    '(2012.12.07) 埼玉 EDI納品書(BP_イエローハット)    ━━━━━ END ━━━━━

    '(2012.12.20) ロンザ納品書送り状(千葉)        ━━━━━START━━━━━  
    ''' <summary>
    '''  受信一覧表(LMH584IN)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH584(ByVal dt As DataTable) As DataSet

        ' 受信一覧表
        Dim PrmDs As DataSet = New LMH584DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH584IN")
        Dim PrmDr As DataRow

        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow

        For i As Integer = 0 To max

            dr = dt.Rows(i)
            PrmDr = PrmDt.NewRow()

            'LMH584INにLMH030_OUTPUTINの値を設定
            PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
            PrmDr("WH_CD") = dr.Item("WH_CD")
            PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
            PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
            PrmDr("OUTKA_PLAN_DATE_FROM") = dr.Item("CRT_DATE_FROM")
            PrmDr("OUTKA_PLAN_DATE_TO") = dr.Item("CRT_DATE_TO")
            'PrmDr("CRT_DATE_FROM") = dr.Item("CRT_DATE_FROM")
            'PrmDr("CRT_DATE_TO") = dr.Item("CRT_DATE_TO")
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
    '(2012.12.25) ロンザ納品書送り状(千葉)        ━━━━━ END━━━━━

    '(2012.12.25) 大阪 EDI納品書(日興産業_ｲｴﾛｰﾊｯﾄ用) -- START --
    ''' <summary>
    ''' EDI納品書(LMH585IN)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH585(ByVal dt As DataTable) As DataSet

        'EDI納品書
        Dim PrmDs As DataSet = New LMH585DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH585IN")
        Dim PrmDr As DataRow

        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow

        For i As Integer = 0 To max

            dr = dt.Rows(i)
            PrmDr = PrmDt.NewRow()

            'LMH585INにLMH030_OUTPUTINの値を設定
            PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
            PrmDr("WH_CD") = dr.Item("WH_CD")
            PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
            PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
            PrmDr("OUTKA_PLAN_DATE_FROM") = dr.Item("CRT_DATE_FROM")
            PrmDr("OUTKA_PLAN_DATE_TO") = dr.Item("CRT_DATE_TO")
            PrmDr("EDI_CUST_INDEX") = dr.Item("EDI_CUST_INDEX")
            PrmDr("PRTFLG") = dr.Item("PRTFLG")
            PrmDr("RCV_NM_HED") = dr.Item("RCV_NM_HED")
            PrmDr("RCV_NM_DTL") = dr.Item("RCV_NM_DTL")
            PrmDr("INOUT_UMU_KB") = dr.Item("INOUT_UMU_KB")
            PrmDr("INOUT_KB") = dr.Item("INOUT_KB")
            PrmDr("OUTPUT_SHUBETU") = dr.Item("OUTPUT_SHUBETU")
            PrmDr("EDI_CTL_NO") = dr.Item("EDI_CTL_NO")
            PrmDr("ROW_NO") = dr.Item("ROW_NO")
            PrmDr("DENPYO_NO") = dr.Item("ORDER_NO")

            PrmDt.Rows.Add(PrmDr)

        Next

        Return PrmDs

    End Function
    '(2012.12.25) 大阪 EDI納品書(日興産業_ｲｴﾛｰﾊｯﾄ用) --  END --

    '(2014.02.01) 群馬 EDI納品送状【オートバックス専用】(BP)  ━━━━━START━━━━━
    ''' <summary>
    ''' EDI納品送状(LMH587IN)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH587(ByVal dt As DataTable) As DataSet

        'EDI納品送状
        Dim PrmDs As DataSet = New LMH587DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH587IN")
        Dim PrmDr As DataRow

        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow

        For i As Integer = 0 To max

            dr = dt.Rows(i)
            PrmDr = PrmDt.NewRow()

            'LMH587INにLMH030_OUTPUTINの値を設定
            PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
            PrmDr("WH_CD") = dr.Item("WH_CD")
            PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
            PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
            '2012.12.17 出荷予定日 START
            PrmDr("OUTKA_PLAN_DATE_FROM") = dr.Item("CRT_DATE_FROM")
            PrmDr("OUTKA_PLAN_DATE_TO") = dr.Item("CRT_DATE_TO")
            '2012.12.17 出荷予定日  END
            'PrmDr("CRT_DATE_FROM") = dr.Item("CRT_DATE_FROM")
            'PrmDr("CRT_DATE_TO") = dr.Item("CRT_DATE_TO")
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
    '(2014.02.01) 群馬 EDI納品送状【オートバックス専用】(BP)  ━━━━━ END ━━━━━    

    '(2015.01.21) テルモ仕切書(千葉)        ━━━━━START━━━━━  
    ''' <summary>
    '''  受信一覧表(LMH588IN)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH588(ByVal dt As DataTable) As DataSet

        ' 受信一覧表
        Dim PrmDs As DataSet = New LMH588DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH588IN")
        Dim PrmDr As DataRow

        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow

        For i As Integer = 0 To max

            dr = dt.Rows(i)
            PrmDr = PrmDt.NewRow()

            'LMH588INにLMH030_OUTPUTINの値を設定
            PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
            PrmDr("WH_CD") = dr.Item("WH_CD")
            PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
            PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
            PrmDr("OUTKA_PLAN_DATE_FROM") = dr.Item("CRT_DATE_FROM")
            PrmDr("OUTKA_PLAN_DATE_TO") = dr.Item("CRT_DATE_TO")
            'PrmDr("CRT_DATE_FROM") = dr.Item("CRT_DATE_FROM")
            'PrmDr("CRT_DATE_TO") = dr.Item("CRT_DATE_TO")
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
            PrmDr("AKAKURO_KB") = dr.Item("AKAKURO_KB")

            PrmDt.Rows.Add(PrmDr)

        Next

        Return PrmDs

    End Function

    '(2015.02.26) 東レ・ダウ(千葉) 追加START
    ''' <summary>
    '''  EDI荷札_東レ・ダウ(LMH590IN)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH590(ByVal dt As DataTable) As DataSet

        ' 受信一覧表
        Dim PrmDs As DataSet = New LMH590DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMH590IN")
        Dim PrmDr As DataRow

        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow

        For i As Integer = 0 To max

            dr = dt.Rows(i)
            PrmDr = PrmDt.NewRow()

            'LMH590INにLMH030_OUTPUTINの値を設定
            PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
            PrmDr("WH_CD") = dr.Item("WH_CD")
            PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
            PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
            PrmDr("OUTKA_PLAN_DATE_FROM") = dr.Item("CRT_DATE_FROM")
            PrmDr("OUTKA_PLAN_DATE_TO") = dr.Item("CRT_DATE_TO")
            PrmDr("EDI_CUST_INDEX") = dr.Item("EDI_CUST_INDEX")
            PrmDr("PRTFLG") = dr.Item("PRTFLG")
            PrmDr("RCV_NM_HED") = dr.Item("RCV_NM_HED")
            PrmDr("RCV_NM_DTL") = dr.Item("RCV_NM_DTL")
            PrmDr("INOUT_UMU_KB") = dr.Item("INOUT_UMU_KB")
            PrmDr("INOUT_KB") = dr.Item("INOUT_KB")
            PrmDr("OUTPUT_SHUBETU") = dr.Item("OUTPUT_SHUBETU")
            PrmDr("EDI_CTL_NO") = dr.Item("EDI_CTL_NO")
            PrmDr("ROW_NO") = dr.Item("ROW_NO")
            PrmDr("DENPYO_NO") = dr.Item("ORDER_NO")

            PrmDt.Rows.Add(PrmDr)

        Next

        Return PrmDs

    End Function


#If True Then ' BP運送会社自動設定対応 20161121 added by inoue

    ''' <summary>
    '''  EDI送り状印刷(日通BP)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH600(ByVal dt As DataTable) As DataSet

        Dim inDs As LMH600DS = New LMH600DS()
        Dim newRow As LMH600DS.LMH600INRow = Nothing

        Dim dr As DataRow = Nothing

        With inDs.LMH600IN

            For i As Integer = 0 To dt.Rows.Count - 1

                dr = dt.Rows(i)
                newRow = inDs.LMH600IN.NewLMH600INRow

                newRow(.NRS_BR_CDColumn.ColumnName) = dr.Item("NRS_BR_CD")
                newRow(.WH_CDColumn.ColumnName) = dr.Item("WH_CD")
                newRow(.CUST_CD_LColumn.ColumnName) = dr.Item("CUST_CD_L")
                newRow(.CUST_CD_MColumn.ColumnName) = dr.Item("CUST_CD_M")
                newRow(.INOUT_KBColumn.ColumnName) = dr.Item("INOUT_KB")
                newRow(.OUTKA_PLAN_DATE_FROMColumn.ColumnName) = dr.Item("CRT_DATE_FROM")
                newRow(.OUTKA_PLAN_DATE_TOColumn.ColumnName) = dr.Item("CRT_DATE_TO")
                newRow(.PRTFLGColumn.ColumnName) = dr.Item("PRTFLG")
                newRow(.EDI_CUST_INDEXColumn.ColumnName) = dr.Item("EDI_CUST_INDEX")
                newRow(.RCV_NM_HEDColumn.ColumnName) = dr.Item("RCV_NM_HED")
                newRow(.RCV_NM_DTLColumn.ColumnName) = dr.Item("RCV_NM_DTL")
                newRow(.INOUT_UMU_KBColumn.ColumnName) = dr.Item("INOUT_UMU_KB")
                newRow(.EDI_CTL_NOColumn.ColumnName) = dr.Item("EDI_CTL_NO")
                newRow(.ROW_NOColumn.ColumnName) = dr.Item("ROW_NO")
                newRow(.OUTPUT_SHUBETUColumn.ColumnName) = dr.Item("OUTPUT_SHUBETU")
                newRow(.DENPYO_NOColumn.ColumnName) = dr.Item("ORDER_NO")

                inDs.LMH600IN.Rows.Add(newRow)

            Next

        End With

        Return inDs

    End Function

#End If

#If True Then ' 日本合成化学対応(2646) 20170116 added inoue

    ''' <summary>
    '''  EDI納品書印刷(日本合成)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMH589(ByVal dt As DataTable) As DataSet

        Dim inDs As LMH589DS = New LMH589DS()
        Dim newRow As LMH589DS.LMH589INRow = Nothing

        Dim dr As DataRow = Nothing

        With inDs.LMH589IN

            For i As Integer = 0 To dt.Rows.Count - 1

                dr = dt.Rows(i)
                newRow = inDs.LMH589IN.NewLMH589INRow

                newRow(.NRS_BR_CDColumn.ColumnName) = dr.Item("NRS_BR_CD")
                newRow(.WH_CDColumn.ColumnName) = dr.Item("WH_CD")
                newRow(.CUST_CD_LColumn.ColumnName) = dr.Item("CUST_CD_L")
                newRow(.CUST_CD_MColumn.ColumnName) = dr.Item("CUST_CD_M")
                newRow(.INOUT_KBColumn.ColumnName) = dr.Item("INOUT_KB")
                newRow(.OUTKA_PLAN_DATE_FROMColumn.ColumnName) = dr.Item("CRT_DATE_FROM")
                newRow(.OUTKA_PLAN_DATE_TOColumn.ColumnName) = dr.Item("CRT_DATE_TO")
                newRow(.PRTFLGColumn.ColumnName) = dr.Item("PRTFLG")
                newRow(.EDI_CUST_INDEXColumn.ColumnName) = dr.Item("EDI_CUST_INDEX")
                newRow(.RCV_NM_HEDColumn.ColumnName) = dr.Item("RCV_NM_HED")
                newRow(.RCV_NM_DTLColumn.ColumnName) = dr.Item("RCV_NM_DTL")
                newRow(.INOUT_UMU_KBColumn.ColumnName) = dr.Item("INOUT_UMU_KB")
                newRow(.EDI_CTL_NOColumn.ColumnName) = dr.Item("EDI_CTL_NO")
                newRow(.ROW_NOColumn.ColumnName) = dr.Item("ROW_NO")
                newRow(.OUTPUT_SHUBETUColumn.ColumnName) = dr.Item("OUTPUT_SHUBETU")
                newRow(.DENPYO_NOColumn.ColumnName) = dr.Item("ORDER_NO")
                newRow(.UNSO_TEHAI_KBColumn.ColumnName) = dr.Item("UNSO_TEHAI_KB")


                inDs.LMH589IN.Rows.Add(newRow)

            Next

        End With

        Return inDs

    End Function

#End If


    '要望番号1151 2012.06.14　修正START(setBlcパラメータ追加)
    ''' <summary>
    '''  プレビューデータセット設定
    ''' </summary>
    ''' <param name="prtBlc"></param>
    ''' <param name="setDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function PrintBlcFunc(ByVal prtBlc As Com.Base.BaseBLC(), ByVal setDs As DataSet(), _
                                  ByVal ds As DataSet, ByVal setBlc As Base.BLC.LMBaseBLC) As DataSet
        '要望番号1151 2012.06.14　修正END

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

            '2012.09.12 要望番号1429 追加START

            '要望番号1151 2012.06.14　追加START(一括印刷不具合)
            'EDI印刷対象テーブルの追加(既に存在する場合は削除⇒追加)
            rtnDs = MyBase.CallBLC(setBlc, "DeleteInsertHEdiPrint", rtnDs)
            '要望番号1151 2012.06.14　追加END
        Next

        '2012.05.14 Start修正小林
        ' '' ''2012.03.19 修正START
        ' '' ''If rdPrevDt.Rows.Count = 0 Then
        '' ''If rtnDs.Tables(LMConst.RD).Rows.Count = 0 Then
        '' ''    Return rtnDs
        '' ''End If
        ' '' ''2012.03.19 修正END

        ' '' ''上書きされたrtnDsのプレビュー情報を消去
        '' ''rtnDs.Tables(LMConst.RD).Rows(0).Delete()

        ' '' ''プレビューDATATABLEをマージ
        '' ''rtnDs.Tables(LMConst.RD).Merge(rdPrevDt)

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
        '2012.05.14 End修正小林

        Return rtnDs

    End Function

#End Region

    '2012.03.03 大阪対応END

    '2012.03.05 大阪対応START
#Region "取込(セミEDI)"
    Private Function SemiEdiTorikomi(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        '更新対象テーブル名

        Dim dtSemiInfo As DataTable = ds.Tables("LMH030_SEMIEDI_INFO")
        Dim dtSemiHed As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_HED")
        Dim dtSemiDtl As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_DTL")
        Dim dtSemiRet As DataTable = ds.Tables("LMH030_EDI_TORIKOMI_RET")   '処理件数

        '処理件数クリア
        dtSemiRet.Clear()
        dtSemiRet.Rows.Add(0)

        '総件数
        dtSemiRet.Rows(0).Item("ALL_CNT") = dtSemiDtl.Rows.Count.ToString()
        dtSemiRet.Rows(0).Item("RCV_HED_INS_CNT") = "0"
        dtSemiRet.Rows(0).Item("RCV_DTL_INS_CNT") = "0"
        dtSemiRet.Rows(0).Item("OUT_HED_INS_CNT") = "0"
        dtSemiRet.Rows(0).Item("OUT_DTL_INS_CNT") = "0"
        dtSemiRet.Rows(0).Item("RCV_HED_CAN_CNT") = "0"
        dtSemiRet.Rows(0).Item("RCV_DTL_CAN_CNT") = "0"
        dtSemiRet.Rows(0).Item("OUT_HED_CAN_CNT") = "0"
        dtSemiRet.Rows(0).Item("OUT_DTL_CAN_CNT") = "0"

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
        Dim setDtInfo As DataTable = setDs.Tables("LMH030_SEMIEDI_INFO")
        Dim setDtHed As DataTable = setDs.Tables("LMH030_EDI_TORIKOMI_HED")
        Dim setSemiRet As DataTable = setDs.Tables("LMH030_EDI_TORIKOMI_RET")   '処理件数

        '更新用詳細DS（ファイル別毎の詳細DS）
        Dim setDtDtl As DataTable = setDs.Tables("LMH030_EDI_TORIKOMI_DTL")

        '2011.09.16 初期値を必ずFALSEにする
        rtnResult = False

        'EdiIndexを取得
        iEdiIndex = Convert.ToInt32(dtSemiInfo.Rows(0)("EDI_CUST_INDEX"))

        rtnBlc = getBlcSemiEdiTorikomi(iEdiIndex)

        '2015.06.09 千葉・MRCデュポン対応 START
        '2015.03.23 横浜・古川エージェンシー対応 START
        'セミEDI標準化の時のみ
        If DirectCast(iEdiIndex, LMH030BLF.EdiCustIndex) = EdiCustIndex.StanderdEdition OrElse
           DirectCast(iEdiIndex, LMH030BLF.EdiCustIndex) = EdiCustIndex.MrcChb00097_00 OrElse
           DirectCast(iEdiIndex, LMH030BLF.EdiCustIndex) = EdiCustIndex.Rome00061_00 OrElse 'ローム(横浜)を追加 2018/11/07 要望番号002046
           DirectCast(iEdiIndex, LMH030BLF.EdiCustIndex) = EdiCustIndex.Rome00061_01 Then   'ローム(大阪)を追加 2022/10/28 要望番号033290
            '---------------------------------------------------------------------------
            ' 該当する荷主明細を全取得
            '---------------------------------------------------------------------------
            ds = MyBase.CallBLC(rtnBlc, "SelectMstCustAll", ds)

            Dim dtMcustD As DataTable = ds.Tables("LMH030_M_CUST_DETAILS")
            Dim setDtMcustD As DataTable = setDs.Tables("LMH030_M_CUST_DETAILS")

            setDtMcustD.Merge(dtMcustD)

        End If
        '2015.03.23 横浜・古川エージェンシー対応 END
        '2015.06.09 千葉・MRCデュポン対応 END

        '入力チェック処理(各個別BLC)
        ds = MyBase.CallBLC(rtnBlc, jobNmchk, ds)

        '入力チェック処理後、再取得（入力チェック処理で不要データを削除、並び順を変えている場合があるため）
        dtSemiHed = ds.Tables("LMH030_EDI_TORIKOMI_HED")
        dtSemiDtl = ds.Tables("LMH030_EDI_TORIKOMI_DTL")
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

                    dtSemiRet.Rows(0).Item("OUT_HED_INS_CNT") = (Convert.ToInt32(dtSemiRet.Rows(0).Item("OUT_HED_INS_CNT").ToString()) _
                                                              + Convert.ToInt32(setSemiRet.Rows(0).Item("OUT_HED_INS_CNT").ToString())).ToString

                    dtSemiRet.Rows(0).Item("OUT_DTL_INS_CNT") = (Convert.ToInt32(dtSemiRet.Rows(0).Item("OUT_DTL_INS_CNT").ToString()) _
                                                              + Convert.ToInt32(setSemiRet.Rows(0).Item("OUT_DTL_INS_CNT").ToString())).ToString

                    dtSemiRet.Rows(0).Item("RCV_HED_CAN_CNT") = (Convert.ToInt32(dtSemiRet.Rows(0).Item("RCV_HED_CAN_CNT").ToString()) _
                                                              + Convert.ToInt32(setSemiRet.Rows(0).Item("RCV_HED_CAN_CNT").ToString())).ToString

                    dtSemiRet.Rows(0).Item("RCV_DTL_CAN_CNT") = (Convert.ToInt32(dtSemiRet.Rows(0).Item("RCV_DTL_CAN_CNT").ToString()) _
                                                              + Convert.ToInt32(setSemiRet.Rows(0).Item("RCV_DTL_CAN_CNT").ToString())).ToString

                    dtSemiRet.Rows(0).Item("OUT_HED_CAN_CNT") = (Convert.ToInt32(dtSemiRet.Rows(0).Item("OUT_HED_CAN_CNT").ToString()) _
                                                              + Convert.ToInt32(setSemiRet.Rows(0).Item("OUT_HED_CAN_CNT").ToString())).ToString

                    dtSemiRet.Rows(0).Item("OUT_DTL_CAN_CNT") = (Convert.ToInt32(dtSemiRet.Rows(0).Item("OUT_DTL_CAN_CNT").ToString()) _
                                                              + Convert.ToInt32(setSemiRet.Rows(0).Item("OUT_DTL_CAN_CNT").ToString())).ToString


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
    '2012.03.05 大阪対応END

#Region "DataSet"

    ''' <summary>
    ''' 運賃INの値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="x"></param>
    ''' <remarks></remarks>
    Friend Function SetUnchinInDataSet(ByVal ds As DataSet,ByVal x As Integer) As DataSet

        Dim unchinDs As DataSet = New LMF800DS()
        Dim insRows As DataRow = unchinDs.Tables("LMF800IN").NewRow
        Dim free_C28 As String = ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("FREE_C28").ToString()

        insRows.Item("WH_CD") = ds.Tables("LMH030_C_OUTKA_L").Rows(0).Item("WH_CD").ToString
        insRows.Item("NRS_BR_CD") = ds.Tables("LMH030_C_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString

        If x = 1 Then
            '危険物有で分割されたとき
            insRows.Item("UNSO_NO_L") = free_C28.ToString.Substring(3, 9)
        Else
            insRows.Item("UNSO_NO_L") = ds.Tables("LMH030_UNSO_L").Rows(0).Item("UNSO_NO_L").ToString

        End If

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
    Friend Function SetShiharaiInDataSet(ByVal ds As DataSet, ByVal x As Integer) As DataSet

        Dim unchinDs As DataSet = New LMF810DS()
        Dim insRows As DataRow = unchinDs.Tables("LMF810IN").NewRow

        Dim free_C28 As String = ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("FREE_C28").ToString() 'ADD 2019/02/20 

        insRows.Item("WH_CD") = ds.Tables("LMH030_C_OUTKA_L").Rows(0).Item("WH_CD").ToString
        insRows.Item("NRS_BR_CD") = ds.Tables("LMH030_C_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString
        If x = 1 Then
            '危険物有で分割されたとき
            insRows.Item("UNSO_NO_L") = free_C28.ToString.Substring(3, 9)
        Else
            insRows.Item("UNSO_NO_L") = ds.Tables("LMH030_UNSO_L").Rows(0).Item("UNSO_NO_L").ToString
        End If

        'データセットに追加
        unchinDs.Tables("LMF810IN").Rows.Add(insRows)

        Return unchinDs

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。支払バッチ呼び出し

    '修正開始 --- 2014.02.13 要望番号:2270
    ''' <summary>
    ''' 運賃INの値を設定
    ''' </summary>
    ''' <param name="unchinDs">DataSet</param>
    ''' <param name="setDs">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetUnchinInDataSetMHM(ByVal unchinDs As DataSet, ByVal setDs As DataSet, ByVal ediIndex As Integer) As DataSet

        '荷主が美浜ならば実行
        If EdiCustIndex.MhmChb00117_00 = ediIndex Then
            '荷主
            Dim newSeiqtoCD As String = setDs.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("FREE_C06").ToString
            Dim max As Integer = unchinDs.Tables("F_UNCHIN_TRS").Rows.Count - 1

            For i As Integer = 0 To max
                unchinDs.Tables("F_UNCHIN_TRS").Rows(i).Item("SEIQTO_CD") = newSeiqtoCD
            Next
        End If

        Return unchinDs

    End Function
    '修正終了 --- 2014.02.13 要望番号:2270

#End Region

#Region "BLC設定処理"
    '▼▼▼二次
    ''' <summary>
    ''' BLC設定処理(一括変更,CSV作成,他)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getBLC(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        Select Case DirectCast(ediIndex, LMH030BLF.EdiCustIndex)

            'EDI荷主INDEX番号
            'サクラファインテック(横浜)
            Case EdiCustIndex.Sakura00237_00

                Dim blc401 As LMH030BLC401 = New LMH030BLC401
                setBlc = blc401

                'サクラファインテック(千葉) ADD 2017/04/07
            Case EdiCustIndex.Sakura00660_00

                Dim blc118 As LMH030BLC118 = New LMH030BLC118
                setBlc = blc118

                'デュポン(横浜)
            Case EdiCustIndex.Dupont00295_00, EdiCustIndex.Dupont00588_00, _
                 EdiCustIndex.Dupont00331_00, EdiCustIndex.Dupont00331_02, _
                 EdiCustIndex.Dupont00331_03

                Dim blc403 As LMH030BLC403 = New LMH030BLC403
                setBlc = blc403

                '日本合成化学(名古屋)
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH030BLC601 = New LMH030BLC601
                setBlc = blc601

            Case Else
                setBlc = New LMH030BLC

        End Select

        Return setBlc

    End Function

    ''' <summary>
    ''' BLC設定処理(出荷登録,運送登録)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getBlcOutkaToroku(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC = Nothing

        Select Case DirectCast(ediIndex, LMH030BLF.EdiCustIndex)

            Case EdiCustIndex.StanderdEdition
                '標準仕様
                Dim blc030_000 As LMH030BLC000 = New LMH030BLC000
                setBlc = blc030_000

                'EDI荷主INDEX番号
                'サクラファインテック(横浜)
            Case EdiCustIndex.Sakura00237_00

                Dim blc401 As LMH030BLC401 = New LMH030BLC401
                setBlc = blc401

                'サクラファインテック(千葉) ADD 2017/04/07
            Case EdiCustIndex.Sakura00660_00

                Dim blc118 As LMH030BLC118 = New LMH030BLC118
                setBlc = blc118

                '2012.04.11 デュポン千葉→横浜移送(00089)追加
                'デュポン(横浜)
                'デュポン(DCSE)(横浜)
                'デュポン(ABS)(横浜)
                'デュポン()(横浜)
                'デュポン(SFTP塗料)(横浜)
                'デュポン(EP:大阪)
                'デュポン(PVFM:大阪)
                'デュポン(DCSE:大阪)
            Case EdiCustIndex.Dupont00295_00 _
                , EdiCustIndex.Dupont00588_00 _
                , EdiCustIndex.Dupont00331_00 _
                , EdiCustIndex.Dupont00331_02 _
                , EdiCustIndex.Dupont00331_03 _
                , EdiCustIndex.Dupont00300_00 _
                , EdiCustIndex.Dupont00689_00 _
                , EdiCustIndex.Dupont00700_00 _
                , EdiCustIndex.Dupont00089_00 _
                , EdiCustIndex.DupontChb00187_00 _
                , EdiCustIndex.DupontChb00188_00 _
                , EdiCustIndex.DupontChb00587_00 _
                , EdiCustIndex.DupontChb00589_00 _
                , EdiCustIndex.DupontChb00688_00 _
                , EdiCustIndex.DupontChb00689_00


                Dim blc403 As LMH030BLC403 = New LMH030BLC403
                setBlc = blc403

                '日本合成化学(名古屋)
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH030BLC601 = New LMH030BLC601
                setBlc = blc601

                '大日本住友製薬
                '大日本住友製薬(動物薬)
            Case EdiCustIndex.Dsp00293_00 _
                , EdiCustIndex.Dspah08251_00
                Dim blc206 As LMH030BLC206 = New LMH030BLC206
                setBlc = blc206

                '東邦化学(大阪)  ADD 2017/02/23 千葉追加
            Case EdiCustIndex.Toho00275_00 _
                , EdiCustIndex.Toho00347_00 _
                , EdiCustIndex.Toho00431_00
                Dim blc201 As LMH030BLC201 = New LMH030BLC201
                setBlc = blc201

                '浮間合成(大阪)
                '（岩槻）浮間合成
            Case EdiCustIndex.UkimaOsk00856_00 _
               , EdiCustIndex.UkimaItk00856_00
                Dim blc203 As LMH030BLC203 = New LMH030BLC203
                setBlc = blc203

                '三井化学(大阪)
                '三井化学(日本特殊塗料)(大阪)
                '（千葉）三井化学
            Case EdiCustIndex.Mitui00369_00 _
                , EdiCustIndex.Mitui00375_00 _
                , EdiCustIndex.MituiGnm00001_00 _
                , EdiCustIndex.MituiChb00456_00
                Dim blc204 As LMH030BLC204 = New LMH030BLC204
                setBlc = blc204

                '大阪：ダウケミ
                '大阪：ダウケミ(高石)
            Case EdiCustIndex.Dow00109_00 _
               , EdiCustIndex.DowTaka00109_01
                Dim blc202 As LMH030BLC202 = New LMH030BLC202
                setBlc = blc202

                'ディック物流大坂(大阪)
                '（岩槻）ディック物流埼玉 東ケミ
                '（岩槻）ディック物流埼玉 東ケミ０１
                '（岩槻）ディック物流春日部
                '（岩槻）ディック物流千葉－岩槻分
                '（岩槻）ディック物流埼玉
                '（岩槻）ディック物流東京営業所
                '（岩槻）ディック物流館林
                '（岩槻）ディック物流埼玉リナブリー
                '（群馬）ディック物流群馬
                '（群馬）ディック物流群馬トートタンク
                '（群馬）ディック物流群馬日パケ
            Case EdiCustIndex.DicOsk00010_00 _
               , EdiCustIndex.DicItk00899_00 _
               , EdiCustIndex.DicItk00899_01 _
               , EdiCustIndex.DicItk10001_00 _
               , EdiCustIndex.DicItk10002_00 _
               , EdiCustIndex.DicItk10003_00 _
               , EdiCustIndex.DicItk10007_00 _
               , EdiCustIndex.DicItk10008_00 _
               , EdiCustIndex.DicItk10009_00 _
               , EdiCustIndex.DicGnm00076_00 _
               , EdiCustIndex.DicGnm00072_00 _
               , EdiCustIndex.DicGnm00039_00 _
               , EdiCustIndex.DicChb00010_00

                Dim blc205 As LMH030BLC205 = New LMH030BLC205
                setBlc = blc205

                'JC(大阪)
                'JC大秦化工(大阪)
                'アイカ工業(大阪)
            Case EdiCustIndex.Jc31022_00 _
               , EdiCustIndex.Jc31022_01 _
               , EdiCustIndex.Aika31023_00
                Dim blc207 As LMH030BLC207 = New LMH030BLC207
                setBlc = blc207

                '2012.05.01 追加START
                '千葉：日医工
            Case EdiCustIndex.Nik00171_00
                Dim blc101 As New LMH030BLC101
                setBlc = blc101
                '2012.05.01 追加END

                '（岩槻）ゴードー溶剤
            Case EdiCustIndex.Godo00950_00
                ', EdiCustIndex.GodoGnm00026_00
                Dim lmh030blc As New LMH030BLC506
                setBlc = lmh030blc

                '（岩槻）住化カラー
                '（市原）住化カラー
                '（市原）住化カラー
            Case EdiCustIndex.Smk00952_00 _
               , EdiCustIndex.SmkChb00002_00 _
               , EdiCustIndex.SmkChb00404_00
                Dim lmh030blc As New LMH030BLC502
                setBlc = lmh030blc

                '（岩槻）大日精化（東京製造）
                '（岩槻）大日精化（化成品）
                '（岩槻）大日精化（洗顔２課）
            Case EdiCustIndex.Dns20000_00 _
               , EdiCustIndex.Dns20000_01 _
               , EdiCustIndex.Dns20000_02
                Dim lmh030blc As New LMH030BLC504
                setBlc = lmh030blc

                '（岩槻）ディック共同配送（505倉庫）
                '（春日部）DIC春日部
                '（春日部）DIC春日部顔料
                '（春日部）DIC春日部関東工場
                '（春日部）DIC春日部他社物流
                '（千葉）DIC千葉輸送
            Case EdiCustIndex.DicItk10005_00 _
               , EdiCustIndex.DicKkb10001_00 _
               , EdiCustIndex.DicKkb10001_03 _
               , EdiCustIndex.DicKkb10012_00 _
               , EdiCustIndex.DicKkb10013_00 _
               , EdiCustIndex.DicChbYuso10010_00

                Dim lmh030blc As New LMH030BLC505
                setBlc = lmh030blc

                '（春日部）関塗工
            Case EdiCustIndex.KtkKkb10009_00
                Dim lmh030blc As New LMH030BLC551
                setBlc = lmh030blc


                '（群馬）篠崎運送
            Case EdiCustIndex.SnzGnm00021_00
                Dim lmh030blc As New LMH030BLC301
                setBlc = lmh030blc

                '（千葉）ビックケミー
                '（千葉）ビックケミー(テツタニ)
                '（千葉）ビックケミー(長瀬)
                'Case EdiCustIndex.BykChb00266_00 _
                '   , EdiCustIndex.BykChb00266_01 _
                '   , EdiCustIndex.BykChb00266_02

                '2013.07.30　修正START
                '（千葉）ビックケミー(直販)
                '（千葉）ビックケミー(テツタニ)
                '（千葉）ビックケミー(エカルト)
            Case EdiCustIndex.BykChb00266_00 _
               , EdiCustIndex.BykChb00266_01 _
               , EdiCustIndex.BykChb00729_00
                '2013.07.30　修正END
                Dim blc102 As LMH030BLC102 = New LMH030BLC102
                setBlc = blc102


                '（千葉）富士フイルム
            Case EdiCustIndex.FjfChb00195_00
                Dim blc103 As LMH030BLC103 = New LMH030BLC103
                setBlc = blc103

                '2012.10.09 追加 START
                '（千葉）ジェイティ物流
            Case EdiCustIndex.JtChb00444_00
                Dim blc111 As LMH030BLC111 = New LMH030BLC111
                setBlc = blc111

                '（千葉）日産物流
            Case EdiCustIndex.NsnChb00145_00
                Dim blc104 As LMH030BLC104 = New LMH030BLC104
                setBlc = blc104

                '（千葉）旭化成ケミカルズ,旭化成イーマテリアルズ
            Case EdiCustIndex.AshChb00070_00, _
                 EdiCustIndex.AshChb00071_00
                Dim blc108 As LMH030BLC108 = New LMH030BLC108
                setBlc = blc108

                '（千葉）美浜
            Case EdiCustIndex.MhmChb00117_00
                Dim blc109 As LMH030BLC109 = New LMH030BLC109
                setBlc = blc109

                '（千葉）ユーティーアイ
            Case EdiCustIndex.UtiChb00625_00
                'Dim blc112 As LMH030BLC112 = New LMH030BLC112
                'setBlc = blc112
#If False Then  'UPD 2019/01/21 依頼番号 : 004188   【LMS】東レDOW・デュポン分社化EDI対応　同じテーブル使用
                                '（千葉）東レダウ・日通（東レダウ）ADD 2018/01/05 （横浜）東レダウ
            Case EdiCustIndex.TorChb00041_00 _
                , EdiCustIndex.TorYok00266_00

                ', EdiCustIndex.TorChb00637_00
                Dim blc106 As LMH030BLC106 = New LMH030BLC106
                setBlc = blc106
                '2012.10.09 追加 END

#Else
            Case EdiCustIndex.TorChb00041_00 _
                , EdiCustIndex.TorYok00266_00 _
                , EdiCustIndex.DowChb00817_00 _
                , EdiCustIndex.DowYok00155_00

                ', EdiCustIndex.TorChb00637_00
                Dim blc106 As LMH030BLC106 = New LMH030BLC106
                setBlc = blc106

#End If

                '（本社）センコー
            Case EdiCustIndex.SnkHns10005_00
                Dim blc001 As LMH030BLC001 = New LMH030BLC001
                setBlc = blc001

                '2012.12.19 追加
                '（岩槻）ビーピー・カストロール
            Case EdiCustIndex.BP00023_00
                Dim blc501 As LMH030BLC501 = New LMH030BLC501
                setBlc = blc501
                '2012.12.19 追加

                '（大阪）日興産業
            Case EdiCustIndex.NksOsk33224_00
                Dim blc208 As LMH030BLC208 = New LMH030BLC208
                setBlc = blc208

                '（千葉）ロンザ
            Case EdiCustIndex.LnzChb00182_00
                Dim blc110 As LMH030BLC110 = New LMH030BLC110
                setBlc = blc110

                '（千葉）ハネウェル（市原：市原）
                '（千葉）ハネウェル（市原：市原Ｂ＆Ｊ）
                '（千葉）ハネウェル（大阪：兵機）
                '（千葉）ハネウェル（名古屋：由良）
                '（千葉）ハネウェル（北海道：三和）
                '（千葉）ハネウェル（九州：博多）
                '（千葉）ハネウェル（横浜：舟津） 
            Case EdiCustIndex.HonChb00630_00 _
                , EdiCustIndex.HonChb00632_00 _
                , EdiCustIndex.HonChb10630_00 _
                , EdiCustIndex.HonChb20630_00 _
                , EdiCustIndex.HonChb30630_00 _
                , EdiCustIndex.HonChb40630_00 _
                , EdiCustIndex.HonChb50630_00

                Dim blc105 As LMH030BLC105 = New LMH030BLC105
                setBlc = blc105

                '（千葉）チッソ
            Case EdiCustIndex.ChissoChb00067_00
                Dim blc107 As LMH030BLC107 = New LMH030BLC107
                setBlc = blc107

                '2013.03.18 追加START
                '千葉：テルモ
            Case EdiCustIndex.TrmChb00409_00
                Dim blc113 As New LMH030BLC113
                setBlc = blc113
                '2013.03.18 追加END

                '千葉：アクタス
            Case EdiCustIndex.AtsChb00750_00
                Dim blc114 As New LMH030BLC114
                setBlc = blc114

                '2015.06.09 Ｍ・Ｒ・Ｃデュポン クラス分割 追加START
            Case EdiCustIndex.MrcChb00097_00
                Dim blc115 As New LMH030BLC115
                setBlc = blc115
                '2015.06.09 Ｍ・Ｒ・Ｃデュポン クラス分割 追加END

                '2015.06.11 住化バイエル 追加START
            Case EdiCustIndex.SmkByr39548_00
                Dim blc911 As New LMH030BLC911
                setBlc = blc911
                '2015.06.11 住化バイエル 追加END

                '2015.06.17 協立化学 追加START
            Case EdiCustIndex.KrtOsk39941_00
                Dim blc210 As New LMH030BLC210
                setBlc = blc210
                '2015.06.17 協立化学 追加END
#If True Then '2015.11.17 協立化学(国内向け:大阪,横浜,千葉) 追加
            Case EdiCustIndex.KrtEdiInJapan
                setBlc = New LMH030BLC211()
#End If
                '2015.07.21 丸和バイオ 追加START
            Case EdiCustIndex.MrwKyu46065_00
                Dim blc701 As New LMH030BLC701
                setBlc = blc701
                '2015.07.21 丸和バイオ 追加END

                'ADD 2016/09/14 丸和(横浜) Start
            Case EdiCustIndex.MaruwaYoko00330_00
                Dim blc404 As New LMH030BLC404
                setBlc = blc404
                'ADD 2016/09/14 丸和(横浜) End

                'ADD 2017/01/23 エストコミュ(千葉) Start
            Case EdiCustIndex.est00784_00
                Dim blc116 As New LMH030BLC116
                setBlc = blc116
                'ADD 2017/01/23 エストコミュ(千葉) End

                'ADD 2017/03/21 アンファ(千葉) Start
            Case EdiCustIndex.angfa10
                Dim blc117 As New LMH030BLC117
                setBlc = blc117
                '    'ADD 2017/03/21 アンファ(千葉) End

                'ADDエアウォーターゾル(千葉)
            Case EdiCustIndex.AWS00801_00

                Dim blc119 As LMH030BLC119 = New LMH030BLC119
                setBlc = blc119

                '2017/11/28 Annen セミEDI_千葉ITW_新規登録対応 add start
                '(千葉ITW)
            Case EdiCustIndex.ITW00750_00
                Dim blc120 As LMH030BLC120 = New LMH030BLC120
                setBlc = blc120
                '2017/11/28 Annen セミEDI_千葉ITW_新規登録対応 add end

                '2018/01/26 Annen 001007 【LMS】_セミEDI開発_片山ナルコ add start
            Case EdiCustIndex.NRK00486_00 _
                 , EdiCustIndex.NRK00486_01
                Dim blc121 As LMH030BLC121 = New LMH030BLC121
                setBlc = blc121
                '2018/01/26 Annen 001007 【LMS】_セミEDI開発_片山ナルコ add end

                'ADD 2017/08/25 インターコンチ(横浜)
            Case EdiCustIndex.ITC00125_00

                Dim blc120 As LMH030BLC405 = New LMH030BLC405
                setBlc = blC120

                '2017/12/21 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add start
                'DSP五協フード＆ケミカル株式会社(横浜)（CSV）
            Case EdiCustIndex.GKC00456_00
                Dim blc130 As New LMH030BLC130
                setBlc = blc130
                'DSP五協フード＆ケミカル株式会社(横浜)（EXCEL）
            Case EdiCustIndex.GKE00456_00
                Dim blc131 As New LMH030BLC131
                setBlc = blc131
                '2017/12/21 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add end
                '2018/02/13 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 add start
            Case EdiCustIndex.Mimaki45741
                Dim blc122 As New LMH030BLC122
                setBlc = blc122
                '2018/02/13 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 add end
            Case EdiCustIndex.TORAY00951_00
                Dim blc132 As New LMH030BLC132
                setBlc = blc132

                'ADD START 2018/11/07 要望番号002046
            Case EdiCustIndex.Rome00061_00, EdiCustIndex.DDP00050_00
                Dim blc406 As New LMH030BLC406
                setBlc = blc406
                'ADD END 2018/11/07 要望番号002046

                'ADD START 2022/10/28 033290 大阪ロームEDI改修
            Case EdiCustIndex.Rome00061_01
                Dim blc407 As New LMH030BLC407
                setBlc = blc407
                'ADD END 2022/10/28 033290 大阪ロームEDI改修

                ''2019.06.27 要望番号006280 add
            Case EdiCustIndex.AgcW00440
                Dim blc220 As New LMH030BLC220
                setBlc = blc220

                ''2019.09.17 要望番号006984 add
            Case EdiCustIndex.CJC00787
                Dim blc133 As New LMH030BLC133
                setBlc = blc133

#If True Then   'ADD 2020/04/15 007999   【LMS】セミEDI_ジョンソンエンドジョンソンJ&J_土気自動倉庫預かり_入荷・出荷新規
            Case EdiCustIndex.JJ15
                Dim blc151 As New LMH030BLC151
                setBlc = blc151
#End If

                'ADD START 2023/02/27 035852 メルクエレクトロニクス セミEDI新規構築
                ' メルクエレクトロニクス(千葉,大阪)
            Case EdiCustIndex.MercChb00025_00, EdiCustIndex.MercOsk00102_00
                Dim blc152 As New LMH030BLC152
                setBlc = blc152
                'ADD END 2023/02/27 035852 メルクエレクトロニクス セミEDI新規構築

                ' SBS東芝ロジスティクス(群馬)
            Case EdiCustIndex.SBS
                Dim blc155 As New LMH030BLC155
                setBlc = blc155

                ' アグリマート(土気)
            Case EdiCustIndex.AGRM
                Dim blc156 As New LMH030BLC156
                setBlc = blc156

                ' シグマアルドリッチジャパン(千葉)
            Case EdiCustIndex.SGM10
                Dim blc157 As New LMH030BLC157
                setBlc = blc157

                ' テルモ サンプル(土気)
            Case EdiCustIndex.TrmSmpl00409_01
                Dim blc158 As New LMH030BLC158
                setBlc = blc158

                ' 丸和(横浜)(CSV)
            Case EdiCustIndex.MaruwaYokoCsv00330_00
                Dim blc159 As New LMH030BLC159
                setBlc = blc159

                ' DHLサプライチェーン(横浜)
            Case EdiCustIndex.DhlYok00052_00
                Dim blc160 As New LMH030BLC160
                setBlc = blc160

                ' アフトンケミカル(大阪)
            Case EdiCustIndex.AftOsk
                Dim blc161 As New LMH030BLC161
                setBlc = blc161

                'ADD START 2023/01/11 033215 ENEOS　EDI作成
                ' ENEOS(大阪,塩浜)
            Case EdiCustIndex.EneosOsk, EdiCustIndex.EneosShm
                Dim blc163 As New LMH030BLC163
                setBlc = blc163
                'ADD END 2023/01/11 033215 ENEOS　EDI作成

                ' テツタニ(大阪)
            Case EdiCustIndex.Tetsutani
                Dim blc167 As New LMH030BLC167
                setBlc = blc167

                ' Rapidus(横浜)
            Case EdiCustIndex.Rapidus40
                Dim blc611 As New LMH030BLC611
                setBlc = blc611

                ' 物産アニマルヘルス(土気)
            Case EdiCustIndex.BAH15
                Dim blc612 As New LMH030BLC612
                setBlc = blc612

            Case Else

        End Select

        Return setBlc

    End Function

    ''' <summary>
    ''' BLC設定処理(実績作成)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getBlcJisseki(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        Select Case DirectCast(ediIndex, LMH030BLF.EdiCustIndex)

            'EDI荷主INDEX番号
            'サクラファインテック(横浜)
            Case EdiCustIndex.Sakura00237_00

                Dim blc401 As LMH030BLC401 = New LMH030BLC401
                setBlc = blc401

                'サクラファインテック(千葉) ADD 2017/04/07
            Case EdiCustIndex.Sakura00660_00

                Dim blc118 As LMH030BLC118 = New LMH030BLC118
                setBlc = blc118

                '2012.04.11 デュポン千葉→横浜移送(00089)追加
                'デュポン(横浜)
                'デュポン(DCSE)(横浜)
                'デュポン(ABS)(横浜)
                'デュポン()(横浜)
                'デュポン(SFTP塗料)(横浜)
                'デュポン(EP:大阪)
                'デュポン(PVFM:大阪)
                'デュポン(DCSE:大阪)
            Case EdiCustIndex.Dupont00295_00 _
                , EdiCustIndex.Dupont00588_00 _
                , EdiCustIndex.Dupont00331_00 _
                , EdiCustIndex.Dupont00331_02 _
                , EdiCustIndex.Dupont00331_03 _
                , EdiCustIndex.Dupont00300_00 _
                , EdiCustIndex.Dupont00689_00 _
                , EdiCustIndex.Dupont00700_00 _
                , EdiCustIndex.Dupont00089_00 _
                , EdiCustIndex.DupontChb00187_00 _
                , EdiCustIndex.DupontChb00188_00 _
                , EdiCustIndex.DupontChb00587_00 _
                , EdiCustIndex.DupontChb00589_00 _
                , EdiCustIndex.DupontChb00688_00 _
                , EdiCustIndex.DupontChb00689_00

                Dim blc403 As LMH030BLC403 = New LMH030BLC403
                setBlc = blc403

                '日本合成化学(名古屋)
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH030BLC601 = New LMH030BLC601
                setBlc = blc601

                '大阪：ダウケミ
                '大阪：ダウケミ(高石)
            Case EdiCustIndex.Dow00109_00 _
               , EdiCustIndex.DowTaka00109_01

                Dim blc202 As LMH030BLC202 = New LMH030BLC202
                setBlc = blc202

                '2012.04.26 追加START
                '千葉：日医工
            Case EdiCustIndex.Nik00171_00
                Dim blc101 As New LMH030BLC101
                setBlc = blc101
                '2012.04.26 追加END

                '（岩槻））住化カラー
            Case EdiCustIndex.Smk00952_00 _
                , EdiCustIndex.SmkChb00002_00 _
                , EdiCustIndex.SmkChb00404_00
                Dim blc502 As New LMH030BLC502
                setBlc = blc502

                '2012.10.15 追加START
                '千葉：日産物流
            Case EdiCustIndex.NsnChb00145_00
                Dim blc104 As New LMH030BLC104
                setBlc = blc104
                '2012.10.15 追加END

                '（千葉）旭化成ケミカルズ,旭化成イーマテリアルズ
            Case EdiCustIndex.AshChb00070_00, _
                 EdiCustIndex.AshChb00071_00
                Dim blc108 As LMH030BLC108 = New LMH030BLC108
                setBlc = blc108

                '（本社）センコー
            Case EdiCustIndex.SnkHns10005_00
                Dim blc001 As LMH030BLC001 = New LMH030BLC001
                setBlc = blc001

                '2012.12.19 追加START
                '（埼玉）ビーピー・カストロール
            Case EdiCustIndex.BP00023_00
                Dim blc501 As LMH030BLC501 = New LMH030BLC501
                setBlc = blc501
                '2012.12.19 追加END

                '2013.01.28 追加START
                '（千葉）ハネウェル（市原：市原）
                '（千葉）ハネウェル（市原：市原Ｂ＆Ｊ）
                '（千葉）ハネウェル（大阪：兵機）
                '（千葉）ハネウェル（名古屋：由良）
                '（千葉）ハネウェル（北海道：三和）
                '（千葉）ハネウェル（九州：博多）
                '（千葉）ハネウェル（横浜：舟津） 
            Case EdiCustIndex.HonChb00630_00 _
                , EdiCustIndex.HonChb00632_00 _
                , EdiCustIndex.HonChb10630_00 _
                , EdiCustIndex.HonChb20630_00 _
                , EdiCustIndex.HonChb30630_00 _
                , EdiCustIndex.HonChb40630_00 _
                , EdiCustIndex.HonChb50630_00
                Dim blc105 As LMH030BLC105 = New LMH030BLC105
                setBlc = blc105
                '2013.01.28 追加END

                '（千葉）チッソ
            Case EdiCustIndex.ChissoChb00067_00
                Dim blc107 As LMH030BLC107 = New LMH030BLC107
                setBlc = blc107

                '2013.03.18 追加START
                '千葉：テルモ
            Case EdiCustIndex.TrmChb00409_00
                Dim blc113 As New LMH030BLC113
                setBlc = blc113
                '2013.03.18 追加END

                '2013.07.30 追加START
                '（千葉）BYK(直販)
                '（千葉）BYK(テツタニ)
                '（千葉）BYK(エカルト)
            Case EdiCustIndex.BykChb00266_00 _
                , EdiCustIndex.BykChb00266_01 _
                , EdiCustIndex.BykChb00729_00

                Dim blc102 As LMH030BLC102 = New LMH030BLC102
                setBlc = blc102
                '2013.07.30 追加END

                'ADD 2017/03/21 アンファ(千葉) Start
            Case EdiCustIndex.angfa10
                Dim blc117 As New LMH030BLC117
                setBlc = blc117
                'ADD 2017/03/21 アンファ(千葉) End

                ' Rapidus(横浜)
            Case EdiCustIndex.Rapidus40
                Dim blc611 As LMH030BLC611 = New LMH030BLC611
                setBlc = blc611

                ' 物産アニマルヘルス(土気)
            Case EdiCustIndex.BAH15
                Dim blc612 As LMH030BLC612 = New LMH030BLC612
                setBlc = blc612

                'その他
            Case Else
                setBlc = New LMH030BLC

        End Select

        Return setBlc

    End Function

    ''' <summary>
    ''' BLC設定処理(実績取消)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getBlcJissekiTorikesi(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        Select Case DirectCast(ediIndex, LMH030BLF.EdiCustIndex)

            'EDI荷主INDEX番号
            'サクラファインテック(横浜)
            Case EdiCustIndex.Sakura00237_00

                Dim blc401 As LMH030BLC401 = New LMH030BLC401
                setBlc = blc401

                'サクラファインテック(千葉) ADD 2017/04/07
            Case EdiCustIndex.Sakura00660_00

                Dim blc118 As LMH030BLC118 = New LMH030BLC118
                setBlc = blc118

                '2012.04.11 デュポン千葉→横浜移送(00089)追加
                'デュポン(横浜)
                'デュポン(DCSE)(横浜)
                'デュポン(ABS)(横浜)
                'デュポン()(横浜)
                'デュポン(SFTP塗料)(横浜)
                'デュポン(EP:大阪)
                'デュポン(PVFM:大阪)
                'デュポン(DCSE:大阪)
            Case EdiCustIndex.Dupont00295_00 _
                , EdiCustIndex.Dupont00588_00 _
                , EdiCustIndex.Dupont00331_00 _
                , EdiCustIndex.Dupont00331_02 _
                , EdiCustIndex.Dupont00331_03 _
                , EdiCustIndex.Dupont00300_00 _
                , EdiCustIndex.Dupont00689_00 _
                , EdiCustIndex.Dupont00700_00 _
                , EdiCustIndex.Dupont00089_00 _
                , EdiCustIndex.DupontChb00187_00 _
                , EdiCustIndex.DupontChb00188_00 _
                , EdiCustIndex.DupontChb00587_00 _
                , EdiCustIndex.DupontChb00589_00 _
                , EdiCustIndex.DupontChb00688_00 _
                , EdiCustIndex.DupontChb00689_00

                Dim blc403 As LMH030BLC403 = New LMH030BLC403
                setBlc = blc403

                '日本合成化学(名古屋)
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH030BLC601 = New LMH030BLC601
                setBlc = blc601

                'その他
            Case Else
                setBlc = New LMH030BLC

        End Select

        Return setBlc

    End Function

    ''' <summary>
    ''' BLC設定処理(実績作成済⇒実績未)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getBlcJissekiSakuseiJissekimi(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        Select Case DirectCast(ediIndex, LMH030BLF.EdiCustIndex)

            'EDI荷主INDEX番号
            'サクラファインテック(横浜)
            Case EdiCustIndex.Sakura00237_00

                Dim blc401 As LMH030BLC401 = New LMH030BLC401
                setBlc = blc401

                'サクラファインテック(千葉) ADD 2017/04/07
            Case EdiCustIndex.Sakura00660_00

                Dim blc118 As LMH030BLC118 = New LMH030BLC118
                setBlc = blc118

                '2012.04.11 デュポン千葉→横浜移送(00089)追加
                'デュポン(横浜)
                'デュポン(DCSE)(横浜)
                'デュポン(ABS)(横浜)
                'デュポン()(横浜)
                'デュポン(SFTP塗料)(横浜)
                'デュポン(EP:大阪)
                'デュポン(PVFM:大阪)
                'デュポン(DCSE:大阪)
            Case EdiCustIndex.Dupont00295_00 _
                , EdiCustIndex.Dupont00588_00 _
                , EdiCustIndex.Dupont00331_00 _
                , EdiCustIndex.Dupont00331_02 _
                , EdiCustIndex.Dupont00331_03 _
                , EdiCustIndex.Dupont00300_00 _
                , EdiCustIndex.Dupont00689_00 _
                , EdiCustIndex.Dupont00700_00 _
                , EdiCustIndex.Dupont00089_00 _
                , EdiCustIndex.DupontChb00187_00 _
                , EdiCustIndex.DupontChb00188_00 _
                , EdiCustIndex.DupontChb00587_00 _
                , EdiCustIndex.DupontChb00589_00 _
                , EdiCustIndex.DupontChb00688_00 _
                , EdiCustIndex.DupontChb00689_00

                Dim blc403 As LMH030BLC403 = New LMH030BLC403
                setBlc = blc403

                '日本合成化学(名古屋)
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH030BLC601 = New LMH030BLC601
                setBlc = blc601

                'その他
            Case Else
                setBlc = New LMH030BLC

        End Select

        Return setBlc

    End Function

    ''' <summary>
    ''' BLC設定処理(実績送信済⇒送信待)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getBlcSousinSousinmi(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        Select Case DirectCast(ediIndex, LMH030BLF.EdiCustIndex)

            'EDI荷主INDEX番号
            'サクラファインテック(横浜)
            Case EdiCustIndex.Sakura00237_00

                Dim blc401 As LMH030BLC401 = New LMH030BLC401
                setBlc = blc401

                'サクラファインテック(千葉) ADD 2017/04/07
            Case EdiCustIndex.Sakura00660_00

                Dim blc118 As LMH030BLC118 = New LMH030BLC118
                setBlc = blc118

                '2012.04.11 デュポン千葉→横浜移送(00089)追加
                'デュポン(横浜)
                'デュポン(DCSE)(横浜)
                'デュポン(ABS)(横浜)
                'デュポン()(横浜)
                'デュポン(SFTP塗料)(横浜)
                'デュポン(EP:大阪)
                'デュポン(PVFM:大阪)
                'デュポン(DCSE:大阪)
            Case EdiCustIndex.Dupont00295_00 _
                , EdiCustIndex.Dupont00588_00 _
                , EdiCustIndex.Dupont00331_00 _
                , EdiCustIndex.Dupont00331_02 _
                , EdiCustIndex.Dupont00331_03 _
                , EdiCustIndex.Dupont00300_00 _
                , EdiCustIndex.Dupont00689_00 _
                , EdiCustIndex.Dupont00700_00 _
                , EdiCustIndex.Dupont00089_00 _
                , EdiCustIndex.DupontChb00187_00 _
                , EdiCustIndex.DupontChb00188_00 _
                , EdiCustIndex.DupontChb00587_00 _
                , EdiCustIndex.DupontChb00589_00 _
                , EdiCustIndex.DupontChb00688_00 _
                , EdiCustIndex.DupontChb00689_00

                Dim blc403 As LMH030BLC403 = New LMH030BLC403
                setBlc = blc403

                '日本合成化学(名古屋)
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH030BLC601 = New LMH030BLC601
                setBlc = blc601

                'その他
            Case Else
                setBlc = New LMH030BLC

        End Select

        Return setBlc

    End Function

    ''' <summary>
    ''' BLC設定処理(実績送信済⇒実績未)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getBlcSousinJissekimi(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        Select Case DirectCast(ediIndex, LMH030BLF.EdiCustIndex)

            'EDI荷主INDEX番号
            'サクラファインテック(横浜)
            Case EdiCustIndex.Sakura00237_00

                Dim blc401 As LMH030BLC401 = New LMH030BLC401
                setBlc = blc401

                'サクラファインテック(千葉) ADD 2017/04/07
            Case EdiCustIndex.Sakura00660_00

                Dim blc118 As LMH030BLC118 = New LMH030BLC118
                setBlc = blc118

                '2012.04.11 デュポン千葉→横浜移送(00089)追加
                'デュポン(横浜)
                'デュポン(DCSE)(横浜)
                'デュポン(ABS)(横浜)
                'デュポン()(横浜)
                'デュポン(SFTP塗料)(横浜)
                'デュポン(EP:大阪)
                'デュポン(PVFM:大阪)
                'デュポン(DCSE:大阪)
            Case EdiCustIndex.Dupont00295_00 _
                , EdiCustIndex.Dupont00588_00 _
                , EdiCustIndex.Dupont00331_00 _
                , EdiCustIndex.Dupont00331_02 _
                , EdiCustIndex.Dupont00331_03 _
                , EdiCustIndex.Dupont00300_00 _
                , EdiCustIndex.Dupont00689_00 _
                , EdiCustIndex.Dupont00700_00 _
                , EdiCustIndex.Dupont00089_00 _
                , EdiCustIndex.DupontChb00187_00 _
                , EdiCustIndex.DupontChb00188_00 _
                , EdiCustIndex.DupontChb00587_00 _
                , EdiCustIndex.DupontChb00589_00 _
                , EdiCustIndex.DupontChb00688_00 _
                , EdiCustIndex.DupontChb00689_00

                Dim blc403 As LMH030BLC403 = New LMH030BLC403
                setBlc = blc403

                '日本合成化学(名古屋)
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH030BLC601 = New LMH030BLC601
                setBlc = blc601

                'その他
            Case Else
                setBlc = New LMH030BLC

        End Select

        Return setBlc

    End Function

    ''' <summary>
    ''' BLC設定処理(出荷取消⇒未登録)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getBlcMitouroku(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        Select Case DirectCast(ediIndex, LMH030BLF.EdiCustIndex)

            'EDI荷主INDEX番号
            'サクラファインテック(横浜)
            Case EdiCustIndex.Sakura00237_00

                Dim blc401 As LMH030BLC401 = New LMH030BLC401
                setBlc = blc401

                'サクラファインテック(千葉) ADD 2017/04/07
            Case EdiCustIndex.Sakura00660_00

                Dim blc118 As LMH030BLC118 = New LMH030BLC118
                setBlc = blc118

                '2012.04.11 デュポン千葉→横浜移送(00089)追加
                'デュポン(横浜)
                'デュポン(DCSE)(横浜)
                'デュポン(ABS)(横浜)
                'デュポン()(横浜)
                'デュポン(SFTP塗料)(横浜)
                'デュポン(EP:大阪)
                'デュポン(PVFM:大阪)
                'デュポン(DCSE:大阪)
            Case EdiCustIndex.Dupont00295_00 _
                , EdiCustIndex.Dupont00588_00 _
                , EdiCustIndex.Dupont00331_00 _
                , EdiCustIndex.Dupont00331_02 _
                , EdiCustIndex.Dupont00331_03 _
                , EdiCustIndex.Dupont00300_00 _
                , EdiCustIndex.Dupont00689_00 _
                , EdiCustIndex.Dupont00700_00 _
                , EdiCustIndex.Dupont00089_00 _
                , EdiCustIndex.DupontChb00187_00 _
                , EdiCustIndex.DupontChb00188_00 _
                , EdiCustIndex.DupontChb00587_00 _
                , EdiCustIndex.DupontChb00589_00 _
                , EdiCustIndex.DupontChb00688_00 _
                , EdiCustIndex.DupontChb00689_00

                Dim blc403 As LMH030BLC403 = New LMH030BLC403
                setBlc = blc403

                '日本合成化学(名古屋)
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH030BLC601 = New LMH030BLC601
                setBlc = blc601

                'その他
            Case Else
                setBlc = New LMH030BLC

        End Select

        Return setBlc

    End Function

    '2012.04.04 追加START
    ''' <summary>
    ''' BLC設定処理(運送取消⇒未登録)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getBlcUnsoMitouroku(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        Select Case DirectCast(ediIndex, LMH030BLF.EdiCustIndex)

            'EDI荷主INDEX番号

            '現段階ではレアケースがないので共通BLCで処理
            Case Else
                setBlc = New LMH030BLC

        End Select

        Return setBlc

    End Function
    '2012.04.04 追加END

    ''' <summary>
    ''' BLC設定処理(EDI取消)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getBlcEdiTorikesi(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        Select Case DirectCast(ediIndex, LMH030BLF.EdiCustIndex)

            'EDI荷主INDEX番号
            'サクラファインテック(横浜)
            Case EdiCustIndex.Sakura00237_00

                Dim blc401 As LMH030BLC401 = New LMH030BLC401
                setBlc = blc401

                'サクラファインテック(千葉) ADD 2017/04/07
            Case EdiCustIndex.Sakura00660_00

                Dim blc118 As LMH030BLC118 = New LMH030BLC118
                setBlc = blc118

                '2012.04.11 デュポン千葉→横浜移送(00089)追加
                'デュポン(横浜)
                'デュポン(DCSE)(横浜)
                'デュポン(ABS)(横浜)
                'デュポン()(横浜)
                'デュポン(SFTP塗料)(横浜)
                'デュポン(EP:大阪)
                'デュポン(PVFM:大阪)
                'デュポン(DCSE:大阪)
            Case EdiCustIndex.Dupont00295_00 _
                , EdiCustIndex.Dupont00588_00 _
                , EdiCustIndex.Dupont00331_00 _
                , EdiCustIndex.Dupont00331_02 _
                , EdiCustIndex.Dupont00331_03 _
                , EdiCustIndex.Dupont00300_00 _
                , EdiCustIndex.Dupont00689_00 _
                , EdiCustIndex.Dupont00700_00 _
                , EdiCustIndex.Dupont00089_00 _
                , EdiCustIndex.DupontChb00187_00 _
                , EdiCustIndex.DupontChb00188_00 _
                , EdiCustIndex.DupontChb00587_00 _
                , EdiCustIndex.DupontChb00589_00 _
                , EdiCustIndex.DupontChb00688_00 _
                , EdiCustIndex.DupontChb00689_00

                Dim blc403 As LMH030BLC403 = New LMH030BLC403
                setBlc = blc403

                '日本合成化学(名古屋)
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH030BLC601 = New LMH030BLC601
                setBlc = blc601

                'その他
            Case Else
                setBlc = New LMH030BLC

        End Select

        Return setBlc

    End Function

    ''' <summary>
    ''' BLC設定処理(EDI取消⇒未登録)
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getBlcTorikesiMitouroku(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC

        Select Case DirectCast(ediIndex, LMH030BLF.EdiCustIndex)

            'EDI荷主INDEX番号
            'サクラファインテック(横浜)
            Case EdiCustIndex.Sakura00237_00

                Dim blc401 As LMH030BLC401 = New LMH030BLC401
                setBlc = blc401

                'サクラファインテック(千葉) ADD 2017/04/07
            Case EdiCustIndex.Sakura00660_00

                Dim blc118 As LMH030BLC118 = New LMH030BLC118
                setBlc = blc118

                '2012.04.11 デュポン千葉→横浜移送(00089)追加
                'デュポン(横浜)
                'デュポン(DCSE)(横浜)
                'デュポン(ABS)(横浜)
                'デュポン()(横浜)
                'デュポン(SFTP塗料)(横浜)
                'デュポン(EP:大阪)
                'デュポン(PVFM:大阪)
                'デュポン(DCSE:大阪)
            Case EdiCustIndex.Dupont00295_00 _
                , EdiCustIndex.Dupont00588_00 _
                , EdiCustIndex.Dupont00331_00 _
                , EdiCustIndex.Dupont00331_02 _
                , EdiCustIndex.Dupont00331_03 _
                , EdiCustIndex.Dupont00300_00 _
                , EdiCustIndex.Dupont00689_00 _
                , EdiCustIndex.Dupont00700_00 _
                , EdiCustIndex.Dupont00089_00 _
                , EdiCustIndex.DupontChb00187_00 _
                , EdiCustIndex.DupontChb00188_00 _
                , EdiCustIndex.DupontChb00587_00 _
                , EdiCustIndex.DupontChb00589_00 _
                , EdiCustIndex.DupontChb00688_00 _
                , EdiCustIndex.DupontChb00689_00

                Dim blc403 As LMH030BLC403 = New LMH030BLC403
                setBlc = blc403

                '日本合成化学(名古屋)
            Case EdiCustIndex.Ncgo32516_00

                Dim blc601 As LMH030BLC601 = New LMH030BLC601
                setBlc = blc601

                'その他
            Case Else
                setBlc = New LMH030BLC

        End Select

        Return setBlc

    End Function

    '2012.03.06 大阪対応START
    ''' <summary>
    ''' BLC設定処理(セミEDI(画面取込))
    ''' </summary>
    ''' <param name="ediIndex"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getBlcSemiEdiTorikomi(ByVal ediIndex As Integer) As Base.BLC.LMBaseBLC

        Dim setBlc As Base.BLC.LMBaseBLC = Nothing

        Select Case DirectCast(ediIndex, LMH030BLF.EdiCustIndex)

            'EDI荷主INDEX番号を元にBLCクラスを取得する
            '標準セミEDI荷主
            'アクタス(千葉)　※セミEDIのみ標準ロジックを通る
            Case EdiCustIndex.StanderdEdition
                ', EdiCustIndex.AtsChb00750_00  'UPD 2020/01/20 千葉アクタス　標準→BLC114へ

                Dim blc030_000 As LMH030BLC000 = New LMH030BLC000
                setBlc = blc030_000


                '大日本住友製薬(医薬品)
                '大日本住友製薬(動物薬)
            Case EdiCustIndex.Dsp00293_00 _
               , EdiCustIndex.Dspah08251_00

                Dim blc206 As LMH030BLC206 = New LMH030BLC206
                setBlc = blc206

                'ジャパンコンポジット
                'ジャパンコンポジット大秦化工
                'アイカ工業
            Case EdiCustIndex.Jc31022_00 _
               , EdiCustIndex.Jc31022_01 _
               , EdiCustIndex.Aika31023_00

                Dim blc207 As LMH030BLC207 = New LMH030BLC207
                setBlc = blc207

                '（春日部）関塗工（大宝）
            Case EdiCustIndex.KtkKkb10009_00

                Dim blc551 As LMH030BLC551 = New LMH030BLC551
                setBlc = blc551

                '    '（群馬）篠崎運輸
                'Case EdiCustIndex.SnzGnm00021_00

                '    Dim blc301 As LMH030BLC301 = New LMH030BLC301
                '    setBlc = blc301


                '（千葉）ビックケミー
                '（千葉）ビックケミー(テツタニ)
                '（千葉）ビックケミー(長瀬)
            Case EdiCustIndex.BykChb00266_00 _
               , EdiCustIndex.BykChb00266_01 _
               , EdiCustIndex.BykChb00266_02

                Dim blc102 As LMH030BLC102 = New LMH030BLC102
                setBlc = blc102

                '（千葉）富士フイルム
            Case EdiCustIndex.FjfChb00195_00
                Dim blc103 As LMH030BLC103 = New LMH030BLC103
                setBlc = blc103

                '（千葉）美浜
            Case EdiCustIndex.MhmChb00117_00
                Dim blc109 As LMH030BLC109 = New LMH030BLC109
                setBlc = blc109

                '（千葉）ロンザ
            Case EdiCustIndex.LnzChb00182_00
                Dim blc110 As LMH030BLC110 = New LMH030BLC110
                setBlc = blc110

                '（千葉）ジェイティ物流
            Case EdiCustIndex.JtChb00444_00
                Dim blc111 As LMH030BLC111 = New LMH030BLC111
                setBlc = blc111

                '2017/11/28 Annen セミEDI_千葉ITW_新規登録対応 add start
                '(千葉ITW)
            Case EdiCustIndex.ITW00750_00
                Dim blc120 As LMH030BLC120 = New LMH030BLC120
                setBlc = blc120
                '2017/11/28 Annen セミEDI_千葉ITW_新規登録対応 add end

                '2018/01/29 Annen 001007 【LMS】_セミEDI開発_片山ナルコ add start
            Case EdiCustIndex.NRK00486_00 _
                 , EdiCustIndex.NRK00486_01
                Dim blc121 As LMH030BLC121 = New LMH030BLC121
                setBlc = blc121
                '2018/01/29 Annen 001007 【LMS】_セミEDI開発_片山ナルコ add end

                '（大阪）日興産業
            Case EdiCustIndex.NksOsk33224_00
                Dim blc208 As LMH030BLC208 = New LMH030BLC208
                setBlc = blc208

                '2013.01.22 EDIバッチ⇒セミEDIに切り替えの為追加
                '（群馬）住化カラー
                '（市原）住化カラー
                '（市原：南袖）住化カラー
            Case EdiCustIndex.Smk00952_00 _
               , EdiCustIndex.SmkChb00002_00 _
               , EdiCustIndex.SmkChb00404_00
                Dim blc502 As New LMH030BLC502
                setBlc = blc502

                '2015.06.09 Ｍ・Ｒ・Ｃデュポン クラス分割 追加START(標準⇒荷主専用に変更)
            Case EdiCustIndex.MrcChb00097_00
                Dim blc115 As New LMH030BLC115
                setBlc = blc115
                '2015.06.09 Ｍ・Ｒ・Ｃデュポン クラス分割 追加END

                '2015.06.11 住化バイエル 追加START
            Case EdiCustIndex.SmkByr39548_00
                Dim blc911 As New LMH030BLC911
                setBlc = blc911
                '2015.06.11 住化バイエル 追加END

                '2015.06.17 協立化学 追加START
            Case EdiCustIndex.KrtOsk39941_00
                Dim blc210 As New LMH030BLC210
                setBlc = blc210
                '2015.06.17 協立化学 追加END
#If True Then '2015.12.02 協立化学(国内向け:大阪,横浜,千葉) 追加
            Case EdiCustIndex.KrtEdiInJapan
                setBlc = New LMH030BLC211()
#End If



                '2015.07.21 丸和バイオ 追加START
            Case EdiCustIndex.MrwKyu46065_00
                Dim blc701 As New LMH030BLC701
                setBlc = blc701
                '2015.07.21 丸和バイオ 追加END

                'ADD 2016/09/14 丸和(横浜) Start
            Case EdiCustIndex.MaruwaYoko00330_00
                Dim blc404 As New LMH030BLC404
                setBlc = blc404
                'ADD 2016/09/14 丸和(横浜) End


                'ADD 2017/01/23 エストコミュ(千葉) Start
            Case EdiCustIndex.est00784_00
                Dim blc116 As New LMH030BLC116
                setBlc = blc116
                'ADD 2017/01/23 エストコミュ(千葉) End

                'ADD 2017/07/28 エアウォーターゾル(千葉) Start
            Case EdiCustIndex.AWS00801_00
                Dim blc119 As New LMH030BLC119
                setBlc = blc119

                'ADD 2017/08/25 インターコンチ(横浜) Start
            Case EdiCustIndex.ITC00125_00
                Dim blc405 As New LMH030BLC405
                setBlc = blc405

                '2017/12/18 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add start
                'DSP五協フード＆ケミカル株式会社(横浜)（CSV）
            Case EdiCustIndex.GKC00456_00
                Dim blc130 As New LMH030BLC130
                setBlc = blc130
                'DSP五協フード＆ケミカル株式会社(横浜)（EXCEL）
            Case EdiCustIndex.GKE00456_00
                Dim blc131 As New LMH030BLC131
                setBlc = blc131
                '2017/12/18 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add end
                '2018/02/13 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 add start
            Case EdiCustIndex.Mimaki45741
                Dim blc122 As New LMH030BLC122
                setBlc = blc122
                '2018/02/13 Annen 000731【LMS】セミEDI_新規開発ディストリビューション･ミマキエンジニアリング-入庫･出庫 対応 add end
            Case EdiCustIndex.TORAY00951_00
                Dim blc132 As New LMH030BLC132
                setBlc = blc132

                'ADD START 2018/11/07 要望番号002046
            Case EdiCustIndex.Rome00061_00, EdiCustIndex.DDP00050_00
                Dim blc406 As New LMH030BLC406
                setBlc = blc406
                'ADD END 2018/11/07 要望番号002046

                'ADD START 2022/10/28 033290 大阪ロームEDI改修
            Case EdiCustIndex.Rome00061_01
                Dim blc407 As New LMH030BLC407
                setBlc = blc407
                'ADD END 2022/10/28 033290 大阪ロームEDI改修

                '20190624 要望番号006280 add
                '大阪：ＡＧＣ若狭化学
            Case EdiCustIndex.AgcW00440
                Dim blc220 As LMH030BLC220 = New LMH030BLC220
                setBlc = blc220

                '2019.09.17 要望番号006984 add
                '千葉：コーヴァンス・ジャパン
            Case EdiCustIndex.CJC00787
                Dim blc133 As LMH030BLC133 = New LMH030BLC133
                setBlc = blc133

#If True Then   'ADD 2020/01/20 (標準000　→ BLC114へ)
                '千葉：アクタス
            Case EdiCustIndex.AtsChb00750_00
                Dim blc114 As New LMH030BLC114
                setBlc = blc114
#End If
#If True Then   'ADD 2020/04/15 007999   【LMS】セミEDI_ジョンソンエンドジョンソンJ&J_土気自動倉庫預かり_入荷・出荷新規
            Case EdiCustIndex.JJ15
                Dim blc151 As New LMH030BLC151
                setBlc = blc151
#End If

                'ADD START 2023/02/27 035852 メルクエレクトロニクス セミEDI新規構築
                ' メルクエレクトロニクス(千葉,大阪)
            Case EdiCustIndex.MercChb00025_00, EdiCustIndex.MercOsk00102_00
                Dim blc152 As New LMH030BLC152
                setBlc = blc152
                'ADD END 2023/02/27 035852 メルクエレクトロニクス セミEDI新規構築

                ' SBS東芝ロジスティクス(群馬)
            Case EdiCustIndex.SBS
                Dim blc155 As New LMH030BLC155
                setBlc = blc155

                ' アグリマート(土気)
            Case EdiCustIndex.AGRM
                Dim blc156 As New LMH030BLC156
                setBlc = blc156

                ' シグマアルドリッチジャパン(千葉)
            Case EdiCustIndex.SGM10
                Dim blc157 As New LMH030BLC157
                setBlc = blc157

                ' テルモ サンプル(土気)
            Case EdiCustIndex.TrmSmpl00409_01
                Dim blc158 As New LMH030BLC158
                setBlc = blc158

                ' 丸和(横浜)(CSV)
            Case EdiCustIndex.MaruwaYokoCsv00330_00
                Dim blc159 As New LMH030BLC159
                setBlc = blc159

                ' DHLサプライチェーン(横浜)
            Case EdiCustIndex.DhlYok00052_00
                Dim blc160 As New LMH030BLC160
                setBlc = blc160

                ' アフトンケミカル(大阪)
            Case EdiCustIndex.AftOsk
                Dim blc161 As New LMH030BLC161
                setBlc = blc161

                'ADD START 2023/01/11 033215 ENEOS　EDI作成
                ' ENEOS(大阪,塩浜)
            Case EdiCustIndex.EneosOsk, EdiCustIndex.EneosShm
                Dim blc163 As New LMH030BLC163
                setBlc = blc163
                'ADD END 2023/01/11 033215 ENEOS　EDI作成

                ' テツタニ(大阪)
            Case EdiCustIndex.Tetsutani
                Dim blc167 As New LMH030BLC167
                setBlc = blc167

                '日本合成化学(名古屋)→三菱ケミカル→三菱ケミカル物流(MCLC)
            Case EdiCustIndex.Ncgo32516_00
                Dim blc601 As LMH030BLC601 = New LMH030BLC601
                setBlc = blc601

                'その他
            Case Else
                setBlc = New LMH030BLC

        End Select

        Return setBlc

    End Function
    '2012.03.06 大阪対応END

    '▲▲▲二次

#End Region

#Region "実績作成データセット"

    ''' <summary>
    ''' 実績作成データセット
    ''' </summary>
    ''' <param name="prmDr"></param>
    ''' <param name="setDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataJissekiSakusei(ByVal prmDr As DataRow, ByVal setDs As DataSet, ByVal eventShubetu As String, ByVal rowNo As Integer)

        Dim setDr As DataRow
        'LMH030IN
        setDr = setDs.Tables("LMH030INOUT").NewRow()

        setDr("NRS_BR_CD") = prmDr("NRS_BR_CD")
        setDr("EDI_CTL_NO") = prmDr("EDI_CTL_NO")
        setDr("OUTKA_CTL_NO") = prmDr("OUTKA_CTL_NO")
        setDr("SYS_UPD_DATE") = prmDr("SYS_UPD_DATE")
        setDr("SYS_UPD_TIME") = prmDr("SYS_UPD_TIME")
        setDr("ROW_NO") = rowNo

        setDs.Tables("LMH030INOUT").Rows.Add(setDr)

        'LMH030_OUTKAEDI_L
        setDr = setDs.Tables("LMH030_OUTKAEDI_L").NewRow()

        setDr("NRS_BR_CD") = prmDr("NRS_BR_CD")
        setDr("EDI_CTL_NO") = prmDr("EDI_CTL_NO")
        setDr("JISSEKI_FLAG") = "1"
        setDr("SYS_UPD_DATE") = prmDr("SYS_UPD_DATE")
        setDr("SYS_UPD_TIME") = prmDr("SYS_UPD_TIME")

        setDs.Tables("LMH030_OUTKAEDI_L").Rows.Add(setDr)

        'LMH030_OUTKAEDI_M
        setDr = setDs.Tables("LMH030_OUTKAEDI_M").NewRow()

        setDr("NRS_BR_CD") = prmDr("NRS_BR_CD")
        setDr("EDI_CTL_NO") = prmDr("EDI_CTL_NO")
        setDr("JISSEKI_FLAG") = "1"

        setDs.Tables("LMH030_OUTKAEDI_M").Rows.Add(setDr)

        'LMH030_RCV_HED
        setDr = setDs.Tables("LMH030_EDI_RCV_HED").NewRow()

        setDr("NRS_BR_CD") = prmDr("NRS_BR_CD")
        setDr("EDI_CTL_NO") = prmDr("EDI_CTL_NO")
        setDr("SYS_UPD_DATE") = prmDr("RCV_SYS_UPD_DATE")
        setDr("SYS_UPD_TIME") = prmDr("RCV_SYS_UPD_TIME")

        setDs.Tables("LMH030_EDI_RCV_HED").Rows.Add(setDr)

        'LMH030_RCV_DTL
        setDr = setDs.Tables("LMH030_EDI_RCV_DTL").NewRow()

        setDr("NRS_BR_CD") = prmDr("NRS_BR_CD")
        setDr("EDI_CTL_NO") = prmDr("EDI_CTL_NO")
        setDr("JISSEKI_SHORI_FLG") = "2"

        setDs.Tables("LMH030_EDI_RCV_DTL").Rows.Add(setDr)

        'LMH030_EDI_SND
        setDr = setDs.Tables("LMH030_EDI_SND").NewRow()

        setDr("NRS_BR_CD") = prmDr("NRS_BR_CD")
        setDr("EDI_CTL_NO") = prmDr("EDI_CTL_NO")
        setDr("JISSEKI_SHORI_FLG") = "2"

        setDs.Tables("LMH030_EDI_SND").Rows.Add(setDr)

        'LMH030_C_OUTKA_L
        setDr = setDs.Tables("LMH030_C_OUTKA_L").NewRow()

        setDr("NRS_BR_CD") = prmDr("NRS_BR_CD")
        setDr("OUTKA_NO_L") = prmDr("OUTKA_CTL_NO")
        setDr("OUTKA_STATE_KB") = "90"
        setDr("SYS_UPD_DATE") = prmDr("OUTKA_SYS_UPD_DATE")
        setDr("SYS_UPD_TIME") = prmDr("OUTKA_SYS_UPD_TIME")
        setDr("SYS_DEL_FLG") = prmDr("OUTKA_DEL_KB")

        setDs.Tables("LMH030_C_OUTKA_L").Rows.Add(setDr)

        setDr = setDs.Tables("LMH030_JUDGE").NewRow()
        setDr("EVENT_SHUBETSU") = eventShubetu
        setDs.Tables("LMH030_JUDGE").Rows.Add(setDr)

    End Sub

#End Region

#Region "出荷取消⇒未登録データセット"

    ''' <summary>
    ''' 出荷取消⇒未登録データセット
    ''' </summary>
    ''' <param name="prmDr"></param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDataTorikesiMitouroku(ByVal prmDr As DataRow, ByVal ds As DataSet, ByVal eventShubetu As String, ByVal rowNo As Integer)

        Dim setDr As DataRow
        'LMH030IN
        setDr = ds.Tables("LMH030INOUT").NewRow()

        setDr("NRS_BR_CD") = prmDr("NRS_BR_CD")
        setDr("EDI_CTL_NO") = prmDr("EDI_CTL_NO")
        setDr("OUTKA_CTL_NO") = prmDr("OUTKA_CTL_NO")
        setDr("SYS_UPD_DATE") = prmDr("SYS_UPD_DATE")
        setDr("SYS_UPD_TIME") = prmDr("SYS_UPD_TIME")
        setDr("ROW_NO") = rowNo
        setDr("RCV_NM_HED") = prmDr("RCV_NM_HED")
        setDr("RCV_NM_DTL") = prmDr("RCV_NM_DTL")
        setDr("RCV_NM_EXT") = prmDr("RCV_NM_EXT")
        setDr("SND_NM") = prmDr("SND_NM")
        setDr("EDI_CUST_INOUTFLG") = prmDr("EDI_CUST_INOUTFLG")

        ds.Tables("LMH030INOUT").Rows.Add(setDr)

        'LMH030_OUTKAEDI_L
        setDr = ds.Tables("LMH030_OUTKAEDI_L").NewRow()

        setDr("NRS_BR_CD") = prmDr("NRS_BR_CD")
        setDr("EDI_CTL_NO") = prmDr("EDI_CTL_NO")
        setDr("SYS_UPD_DATE") = prmDr("SYS_UPD_DATE")
        setDr("SYS_UPD_TIME") = prmDr("SYS_UPD_TIME")
        setDr("OUT_FLAG") = "0"

        ds.Tables("LMH030_OUTKAEDI_L").Rows.Add(setDr)

        'LMH030_OUTKAEDI_M
        setDr = ds.Tables("LMH030_OUTKAEDI_M").NewRow()

        setDr("NRS_BR_CD") = prmDr("NRS_BR_CD")
        setDr("EDI_CTL_NO") = prmDr("EDI_CTL_NO")

        ds.Tables("LMH030_OUTKAEDI_M").Rows.Add(setDr)

        'LMH030_RCV_HED
        setDr = ds.Tables("LMH030_EDI_RCV_HED").NewRow()

        setDr("NRS_BR_CD") = prmDr("NRS_BR_CD")
        setDr("EDI_CTL_NO") = prmDr("EDI_CTL_NO")
        setDr("SYS_UPD_DATE") = prmDr("RCV_SYS_UPD_DATE")
        setDr("SYS_UPD_TIME") = prmDr("RCV_SYS_UPD_TIME")

        ds.Tables("LMH030_EDI_RCV_HED").Rows.Add(setDr)

        'LMH030_RCV_DTL
        setDr = ds.Tables("LMH030_EDI_RCV_DTL").NewRow()

        setDr("NRS_BR_CD") = prmDr("NRS_BR_CD")
        setDr("EDI_CTL_NO") = prmDr("EDI_CTL_NO")

        ds.Tables("LMH030_EDI_RCV_DTL").Rows.Add(setDr)

        setDr = ds.Tables("LMH030_JUDGE").NewRow()
        setDr("EVENT_SHUBETSU") = eventShubetu
        ds.Tables("LMH030_JUDGE").Rows.Add(setDr)

    End Sub

#End Region

    '▼▼▼要望番号:467
    ''' <summary>
    ''' 出荷依頼送信データ作成処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDsCsvData(ByVal ds As DataSet) As DataSet

        Dim ediIndex As Integer = Convert.ToInt32(ds.Tables("LMH030_CSVIN").Rows(0)("EDI_CUST_INDEX"))
        Dim rtnBlc As Base.BLC.LMBaseBLC

        rtnBlc = getBLC(ediIndex)

        ds = MyBase.CallBLC(rtnBlc, "SelectCsv", ds)

        Return ds

    End Function
    '▲▲▲要望番号:467

End Class
