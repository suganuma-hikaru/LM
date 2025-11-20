' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030    : EDI出荷検索
'  EDI荷主ID　　　　:  共通
'  作  成  者       :  umano
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH030DAC
''' </summary>
''' <remarks></remarks>
Public Class LMH030DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "DAC用 イベント種別CONST"

    ''' <summary>
    '''イベント種別
    ''' </summary>
    ''' <remarks></remarks>

    Public Enum EventShubetsu As Integer

        SAVEOUTKA = 1           '出荷登録
        CREATEJISSEKI           '実績作成
        HIMODUKE                '紐付け
        EDITORIKESI             'EDI取消
        TORIKOMI                '取込
        SAVEUNSO                '運送登録
        TORIKESIJISSEKI         '実績取消
        KENSAKU                 '検索
        IKKATUHENKO             '一括変更
        PRINT                   '印刷
        REPRINT                 '再印刷
        EXE                     '実行
        TORIKESI_MITOUROKU      'EDI取消⇒未登録
        SAKUSEIZUMI_JISSEKIMI   '実績作成済⇒実績未
        SOUSINZUMI_SOUSINMACHI  '実績送信済⇒送信待
        SOUSINZUMI_JISSEKIMI    '実績送信済⇒実績未
        SAKURA_TUIKAJIKKOU      'サクラ追加実行
        TOUROKUZUMI_MITOUROKU   '出荷登録済⇒未登録
        MASTER                  'マスタ参照
        ENTER                   'Enter
        DEF_CUST                '初期荷主変更
        CLOSE                   '閉じる
        DOUBLE_CLICK            'ダブルクリック
        UNSOTORIKESI_MITOUROKU  '運送取消⇒未登録(=24)  '2012.04.04 大阪対応 ADD

    End Enum

#End Region

#Region "EDI荷主INDEX"
    'イベント種別
    Public Enum EdiCustIndex As Integer

        Sakura00237_00 = 1               'サクラファインテック(横浜)
        Ncgo32516_00 = 57                '日本合成化学(名古屋)
        Dupont00089_00 = 18              'デュポン(テフロン)(千葉)→(横浜)移送  '2012.04.11 ADD
        Dupont00295_00 = 37              'デュポン(横浜)
        Dupont00331_00 = 87              'デュポン(DCSE)(横浜)
        Dupont00331_02 = 88              'デュポン(ABS)(横浜)
        Dupont00331_03 = 90              'デュポン()(横浜)
        Dupont00588_00 = 89              'デュポン(SFTP塗料)(横浜)
        Dupont00300_00 = 36              'デュポン(EP:大阪)
        Dupont00689_00 = 81              'デュポン(PVFM:大阪)
        Dupont00700_00 = 86              'デュポン(DCSE:大阪)
        DupontChb00187_00 = 47           'デュポン(ブタサイト:千葉)
        DupontChb00188_00 = 35           'デュポン(EP:千葉)
        DupontChb00587_00 = 76           'デュポン(農業:千葉)
        DupontChb00589_00 = 60           'デュポン(特殊化学品:千葉)
        DupontChb00688_00 = 79           'デュポン(電子材料事業:千葉)
        DupontChb00689_00 = 80           'デュポン(PVFM:千葉)


        Dsp00293_00 = 82                 '大日本住友製薬(大阪)
        Dspah08251_00 = 85               '大日本住友製薬(動物薬)(大阪)
        Toho00275_00 = 65                '東邦化学(大阪)
        '2012.05.15 追加START
        UkimaOsk00856_00 = 91            '浮間合成(大阪)
        Mitui00369_00 = 17               '三井化学(大阪)
        Mitui00375_00 = 21               '三井化学(日本特殊塗料)(大阪)
        Dow00109_00 = 43                 'ダウケミ(大阪)
        DowTaka00109_01 = 44             'ダウケミ(大阪・高石)
        DicOsk00010_00 = 28              'ディック（大阪)

        Jc31022_00 = 34                  'ジャパンコンポジット（大阪)
        Jc31022_01 = 42                  'ジャパンコンポジット（大阪)大泰化工
        Aika31023_00 = 40                'アイカ工業（大阪)
        Nik00171_00 = 92                 '日医工(千葉) '2012.05.01 ADD
        '2012.05.15 追加END

        '2012.05.28 追加START
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
        '2012.05.28 追加END

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
        TorChb00637_00 = 59             '（千葉）日通（東レダウ

        AshChb00070_00 = 61             '（千葉）旭化成ケミカルズ
        AshChb00071_00 = 33             '（千葉）旭化成イーマテリアルズ

        SnkHns10005_00 = 97             '（本社）センコー

        BP00023_00 = 77                 '（岩槻）ビーピー・カストロール

        NksOsk33224_00 = 98             '（大阪）日興産業

        ChissoChb00067_00 = 2           '（千葉）チッソ

        DicChbYuso10010_00 = 46         '（千葉）ディック千葉輸送

        DicChb00010_00 = 102            '（千葉）ディック物流千葉

        Nichigo32516_00 = 57            ' (名古屋) 日本合成化学

    End Enum

#End Region

#Region "DAC用 エラーEXCELパラメータ用CONST"

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"

    ''' <summary>
    ''' EXCEL用COLUMタイトル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXCEL_COLTITLE As String = "EDI管理番号"

#End Region

#Region "SELECT_Z_KBN"
    Private Const SQL_SELECT_Z_KBN As String = " SELECT                                        " & vbNewLine _
                                     & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..Z_KBN                        Z_KBN          " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " Z_KBN.KBN_GROUP_CD = @KBN_GROUP_CD                    " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " Z_KBN.KBN_CD   = @KBN_CD                              " & vbNewLine


    ''' <summary>
    ''' 区分マスタ取得SQL（汎用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_Z_KBN_HANYO As String =
          "SELECT                                 " & vbNewLine _
        & "       KBN_GROUP_CD                    " & vbNewLine _
        & "      ,KBN_CD                          " & vbNewLine _
        & "      ,KBN_KEYWORD                     " & vbNewLine _
        & "      ,KBN_NM1                         " & vbNewLine _
        & "      ,KBN_NM2                         " & vbNewLine _
        & "      ,KBN_NM3                         " & vbNewLine _
        & "      ,KBN_NM4                         " & vbNewLine _
        & "      ,KBN_NM5                         " & vbNewLine _
        & "      ,KBN_NM6                         " & vbNewLine _
        & "      ,KBN_NM7                         " & vbNewLine _
        & "      ,KBN_NM8                         " & vbNewLine _
        & "      ,KBN_NM9                         " & vbNewLine _
        & "      ,KBN_NM10                        " & vbNewLine _
        & "      ,KBN_NM11                        " & vbNewLine _
        & "      ,KBN_NM12                        " & vbNewLine _
        & "      ,KBN_NM13                        " & vbNewLine _
        & "      ,VALUE1                          " & vbNewLine _
        & "      ,VALUE2                          " & vbNewLine _
        & "      ,VALUE3                          " & vbNewLine _
        & "      ,SORT                            " & vbNewLine _
        & "      ,REM                             " & vbNewLine _
        & "  FROM $LM_MST$..Z_KBN                 " & vbNewLine _
        & " WHERE SYS_DEL_FLG = '0'               " & vbNewLine _
        & "   AND KBN_GROUP_CD = @KBN_GROUP_CD    " & vbNewLine

#End Region

#Region "SELECT_Z_KBN(VALUE1:荷姿)"
    Private Const SQL_SELECT_PKG_UT_Z_KBN As String = " SELECT                                 " & vbNewLine _
                                     & " COUNT(*)                               AS NIS_CNT     " & vbNewLine _
                                     & ",VALUE1                                 AS NISUGATA    " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..Z_KBN                        Z_KBN          " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " Z_KBN.KBN_GROUP_CD = @KBN_GROUP_CD                    " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " Z_KBN.KBN_CD   = @KBN_CD                              " & vbNewLine _
                                     & " GROUP BY                                              " & vbNewLine _
                                     & " Z_KBN.VALUE1                                          " & vbNewLine

#End Region

#Region "SELECT_M_SOKO"
    Private Const SQL_SELECT_WH As String = " SELECT                                           " & vbNewLine _
                                     & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_SOKO                       AS M_SOKO      " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " M_SOKO.NRS_BR_CD = @NRS_BR_CD                         " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_SOKO.WH_CD     = @WH_CD                             " & vbNewLine

#End Region

    '2012.03.23 大阪対応START
#Region "SELECT_M_HOL"
    Private Const SQL_SELECT_M_HOL As String = " SELECT                                        " & vbNewLine _
                                     & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_HOL                        AS M_HOL       " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " M_HOL.HOL = @HOL                                      " & vbNewLine
#End Region
    '2012.03.23 大阪対応END

#Region "SELECT_M_CUST"
    Private Const SQL_SELECT_M_CUST As String = " SELECT                                       " & vbNewLine _
                                     & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_CUST                       M_CUST         " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " M_CUST.NRS_BR_CD   = @NRS_BR_CD                       " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_CUST.CUST_CD_L   = @CUST_CD_L                       " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_CUST.CUST_CD_M   = @CUST_CD_M                       " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_CUST.CUST_CD_S   = '00'                             " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_CUST.CUST_CD_SS  = '00'                            " & vbNewLine


#End Region

    '2012.02.25 大阪対応 START

#Region "SELECT_M_CUST_DETAILS"

    Private Const SQL_SELECT_M_CUST_DETAILS As String = " SELECT                         " & vbNewLine _
                                     & " COUNT(*)                   AS MST_CNT            " & vbNewLine _
                                     & ",NRS_BR_CD			        AS NRS_BR_CD          " & vbNewLine _
                                     & ",CUST_CD		            AS CUST_CD            " & vbNewLine _
                                     & ",CUST_CD_EDA		        AS CUST_CD_EDA        " & vbNewLine _
                                     & ",CUST_CLASS		            AS CUST_CLASS         " & vbNewLine _
                                     & ",SUB_KB		                AS SUB_KB             " & vbNewLine _
                                     & ",SET_NAIYO		            AS SET_NAIYO          " & vbNewLine _
                                     & ",SET_NAIYO_2		        AS SET_NAIYO_2        " & vbNewLine _
                                     & ",SET_NAIYO_3		        AS SET_NAIYO_3        " & vbNewLine _
                                     & ",REMARK		                AS REMARK             " & vbNewLine _
                                     & " FROM                                             " & vbNewLine _
                                     & " $LM_MST$..M_CUST_DETAILS  M_CUST_DETAILS         " & vbNewLine _
                                     & " WHERE                                            " & vbNewLine _
                                     & " M_CUST_DETAILS.NRS_BR_CD   = @NRS_BR_CD          " & vbNewLine _
                                     & " AND                                              " & vbNewLine _
                                     & " M_CUST_DETAILS.CUST_CD   = @CUST_CD              " & vbNewLine _
                                     & " AND                                              " & vbNewLine _
                                     & " M_CUST_DETAILS.SUB_KB   = '19'                   " & vbNewLine _
                                     & " AND                                              " & vbNewLine _
                                     & " M_CUST_DETAILS.CUST_CLASS   = '01'               " & vbNewLine

#End Region

    '要望番号1282:((春日部)DICさまEDIにて、異なる届け先で同じCDでくる対策) 2012/07/19 本明 Start
#Region "SELECT_M_CUST_DETAILS2"

    Private Const SQL_SELECT_M_CUST_DETAILS2 As String = " SELECT                       " & vbNewLine _
                                     & " COUNT(*)                   AS MST_CNT          " & vbNewLine _
                                     & ",NRS_BR_CD			        AS NRS_BR_CD        " & vbNewLine _
                                     & ",CUST_CD		            AS CUST_CD          " & vbNewLine _
                                     & ",CUST_CD_EDA		        AS CUST_CD_EDA      " & vbNewLine _
                                     & ",CUST_CLASS		            AS CUST_CLASS       " & vbNewLine _
                                     & ",SUB_KB		                AS SUB_KB           " & vbNewLine _
                                     & ",SET_NAIYO		            AS SET_NAIYO        " & vbNewLine _
                                     & ",SET_NAIYO_2		        AS SET_NAIYO_2      " & vbNewLine _
                                     & ",SET_NAIYO_3		        AS SET_NAIYO_3      " & vbNewLine _
                                     & ",REMARK		                AS REMARK           " & vbNewLine _
                                     & " FROM                                           " & vbNewLine _
                                     & " $LM_MST$..M_CUST_DETAILS  M_CUST_DETAILS       " & vbNewLine _
                                     & " WHERE                                          " & vbNewLine _
                                     & " M_CUST_DETAILS.NRS_BR_CD = @NRS_BR_CD          " & vbNewLine _
                                     & " AND                                            " & vbNewLine _
                                     & " M_CUST_DETAILS.CUST_CD   = @CUST_CD            " & vbNewLine _
                                     & " AND                                            " & vbNewLine _
                                     & " M_CUST_DETAILS.SUB_KB    = '38'                " & vbNewLine _
                                     & "-- AND                                            " & vbNewLine _
                                     & "-- M_CUST_DETAILS.CUST_CLASS   = @CUST_CLASS      " & vbNewLine

#End Region
    '要望番号1282:((春日部)DICさまEDIにて、異なる届け先で同じCDでくる対策) 2012/07/19 本明 End

#Region "GROUP_BY_M_CUST_DETAILS"

    Private Const SQL_GROUP_BY_M_CUST_DETAILS_COUNT As String = " GROUP BY                 " & vbNewLine _
                                                & " NRS_BR_CD                               " & vbNewLine _
                                                & ",CUST_CD                                 " & vbNewLine _
                                                & ",CUST_CD_EDA	                            " & vbNewLine _
                                                & ",CUST_CLASS	                            " & vbNewLine _
                                                & ",SUB_KB                                  " & vbNewLine _
                                                & ",SET_NAIYO	                            " & vbNewLine _
                                                & ",SET_NAIYO_2	                            " & vbNewLine _
                                                & ",SET_NAIYO_3	                            " & vbNewLine _
                                                & ",REMARK                                  " & vbNewLine

#End Region

    '要望番号1282:((春日部)DICさまEDIにて、異なる届け先で同じCDでくる対策) 2012/07/19 本明 Start
#Region "ORDER_BY_M_CUST_DETAILS2"
    Private Const SQL_ORDER_BY_M_CUST_DETAILS2 As String = " ORDER BY                       " & vbNewLine _
                                                & " SET_NAIYO_2	                            " & vbNewLine _
                                                & ",SET_NAIYO   DESC                        " & vbNewLine
#End Region
    '要望番号1282:((春日部)DICさまEDIにて、異なる届け先で同じCDでくる対策) 2012/07/19 本明 End

    '2012.02.25 大阪対応 END

    '要望番号:1330 terakawa 2012.08.10 KANA_NM追加 Start
#Region "SELECT_M_DEST"

    Private Const SQL_SELECT_M_DEST As String = " SELECT                                       " & vbNewLine _
                                     & " COUNT(*)                      AS MST_CNT     " & vbNewLine _
                                     & ",NRS_BR_CD			AS NRS_BR_CD	" & vbNewLine _
                                     & ",CUST_CD_L			AS CUST_CD_L				     " & vbNewLine _
                                     & ",DEST_CD			AS DEST_CD				     " & vbNewLine _
                                     & ",EDI_CD				AS EDI_CD				     " & vbNewLine _
                                     & ",DEST_NM			AS DEST_NM				     " & vbNewLine _
                                     & ",KANA_NM			AS KANA_NM				     " & vbNewLine _
                                     & ",ZIP				AS ZIP				     " & vbNewLine _
                                     & ",AD_1				AS AD_1				     " & vbNewLine _
                                     & ",AD_2				AS AD_2				     " & vbNewLine _
                                     & ",AD_3				AS AD_3				     " & vbNewLine _
                                     & ",CUST_DEST_CD			AS CUST_DEST_CD			" & vbNewLine _
                                     & ",SALES_CD			AS SALES_CD			" & vbNewLine _
                                     & ",SP_NHS_KB			AS SP_NHS_KB			" & vbNewLine _
                                     & ",COA_YN				AS COA_YN				" & vbNewLine _
                                     & ",SP_UNSO_CD			AS SP_UNSO_CD			" & vbNewLine _
                                     & ",SP_UNSO_BR_CD			AS SP_UNSO_BR_CD			" & vbNewLine _
                                     & ",DELI_ATT			AS DELI_ATT			" & vbNewLine _
                                     & ",CARGO_TIME_LIMIT		AS CARGO_TIME_LIMIT	" & vbNewLine _
                                     & ",LARGE_CAR_YN			AS LARGE_CAR_YN			" & vbNewLine _
                                     & ",TEL				AS TEL				     " & vbNewLine _
                                     & ",FAX				AS FAX				     " & vbNewLine _
                                     & ",UNCHIN_SEIQTO_CD		AS UNCHIN_SEIQTO_CD				     " & vbNewLine _
                                     & ",JIS				AS JIS				     " & vbNewLine _
                                     & ",KYORI				AS KYORI				     " & vbNewLine _
                                     & ",PICK_KB			AS PICK_KB				     " & vbNewLine _
                                     & ",BIN_KB				AS BIN_KB				     " & vbNewLine _
                                     & ",MOTO_CHAKU_KB			AS MOTO_CHAKU_KB				     " & vbNewLine _
                                     & ",URIAGE_CD			AS URIAGE_CD				     " & vbNewLine _
                                     & ",SYS_ENT_DATE                   AS  SYS_ENT_DATE    " & vbNewLine _
                                     & ",SYS_ENT_TIME                   AS  SYS_ENT_TIME    " & vbNewLine _
                                     & ",SYS_ENT_PGID                   AS  SYS_ENT_PGID    " & vbNewLine _
                                     & ",SYS_ENT_USER                   AS  SYS_ENT_USER    " & vbNewLine _
                                     & ",SYS_UPD_DATE                   AS  SYS_UPD_DATE    " & vbNewLine _
                                     & ",SYS_UPD_TIME                   AS  SYS_UPD_TIME    " & vbNewLine _
                                     & ",SYS_UPD_PGID                   AS  SYS_UPD_PGID    " & vbNewLine _
                                     & ",SYS_UPD_USER                   AS  SYS_UPD_USER    " & vbNewLine _
                                     & ",SYS_DEL_FLG                    AS  SYS_DEL_FLG     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_DEST                       M_DEST         " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " M_DEST.NRS_BR_CD   = @NRS_BR_CD                       " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_DEST.CUST_CD_L   = @CUST_CD_L                       " & vbNewLine

#End Region

#Region "GROUP_BY_M_DEST"

    Private Const SQL_GROUP_BY_M_DEST As String = " GROUP BY                                      " & vbNewLine _
                                     & "NRS_BR_CD             " & vbNewLine _
& ",CUST_CD_L								 " & vbNewLine _
& ",DEST_CD								 " & vbNewLine _
& ",EDI_CD								 " & vbNewLine _
& ",DEST_NM								 " & vbNewLine _
& ",KANA_NM								 " & vbNewLine _
& ",ZIP								 " & vbNewLine _
& ",AD_1								 " & vbNewLine _
& ",AD_2								 " & vbNewLine _
& ",AD_3								 " & vbNewLine _
& ",CUST_DEST_CD								 " & vbNewLine _
& ",SALES_CD								 " & vbNewLine _
& ",SP_NHS_KB								 " & vbNewLine _
& ",COA_YN								 " & vbNewLine _
& ",SP_UNSO_CD								 " & vbNewLine _
& ",SP_UNSO_BR_CD								 " & vbNewLine _
& ",DELI_ATT								 " & vbNewLine _
& ",CARGO_TIME_LIMIT								 " & vbNewLine _
& ",LARGE_CAR_YN								 " & vbNewLine _
& ",TEL								 " & vbNewLine _
& ",FAX								 " & vbNewLine _
& ",UNCHIN_SEIQTO_CD								 " & vbNewLine _
& ",JIS								 " & vbNewLine _
& ",KYORI								 " & vbNewLine _
& ",PICK_KB								 " & vbNewLine _
& ",BIN_KB								 " & vbNewLine _
& ",MOTO_CHAKU_KB								 " & vbNewLine _
& ",URIAGE_CD								 " & vbNewLine _
& ",SYS_ENT_DATE   " & vbNewLine _
& ",SYS_ENT_TIME   " & vbNewLine _
& ",SYS_ENT_PGID   " & vbNewLine _
& ",SYS_ENT_USER   " & vbNewLine _
& ",SYS_UPD_DATE   " & vbNewLine _
& ",SYS_UPD_TIME   " & vbNewLine _
& ",SYS_UPD_PGID   " & vbNewLine _
& ",SYS_UPD_USER   " & vbNewLine _
& ",SYS_DEL_FLG   " & vbNewLine

#End Region
    '要望番号:1330 terakawa 2012.08.10 KANA_NM追加 End



    'ADD Start 2018/02/26 依頼番号:1198 東レ・ダウ届先一括変
    ''' <summary>
    ''' DEST_M、UNCHIN_TARIFF_SET_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_IKKATSU_M_DEST As String = " SELECT                                                                                        " & vbNewLine _
                                            & "	      DEST.NRS_BR_CD                                          AS NRS_BR_CD                    " & vbNewLine _
                                            & "	     ,DEST.CUST_CD_L                                          AS CUST_CD_L                    " & vbNewLine _
                                            & "	     ,DEST.DEST_CD                                            AS DEST_CD                      " & vbNewLine _
                                            & "	     ,DEST.EDI_CD                                             AS EDI_CD                       " & vbNewLine _
                                            & "	     ,DEST.DEST_NM                                            AS DEST_NM                      " & vbNewLine _
                                            & "	     ,DEST.KANA_NM                                            AS KANA_NM                      " & vbNewLine _
                                            & "	     ,DEST.ZIP                                                AS ZIP                          " & vbNewLine _
                                            & "	     ,DEST.AD_1                                               AS AD_1                         " & vbNewLine _
                                            & "	     ,DEST.AD_2                                               AS AD_2                         " & vbNewLine _
                                            & "	     ,DEST.AD_3                                               AS AD_3                         " & vbNewLine _
                                            & "	     ,DEST.CUST_DEST_CD                                       AS CUST_DEST_CD                 " & vbNewLine _
                                            & "	     ,DEST.TEL                                                AS TEL                          " & vbNewLine _
                                            & "	     ,DEST.JIS                                                AS JIS                          " & vbNewLine _
                                            & "	     ,DEST.SP_NHS_KB                                          AS SP_NHS_KB                    " & vbNewLine _
                                            & "	     ,DEST.FAX                                                AS FAX                          " & vbNewLine _
                                            & "	     ,DEST.KYORI                                              AS KYORI                        " & vbNewLine _
                                            & "	     ,DEST.COA_YN                                             AS COA_YN                       " & vbNewLine _
                                            & "	     ,DEST.SP_UNSO_CD                                         AS SP_UNSO_CD                   " & vbNewLine _
                                            & "	     ,DEST.SP_UNSO_BR_CD                                      AS SP_UNSO_BR_CD                " & vbNewLine _
                                            & "	     ,DEST.PICK_KB                                            AS PICK_KB                      " & vbNewLine _
                                            & "	     ,DEST.BIN_KB                                             AS BIN_KB                       " & vbNewLine _
                                            & "	     ,DEST.MOTO_CHAKU_KB                                      AS MOTO_CHAKU_KB                " & vbNewLine _
                                            & "	     ,DEST.CARGO_TIME_LIMIT                                   AS CARGO_TIME_LIMIT             " & vbNewLine _
                                            & "	     ,DEST.LARGE_CAR_YN                                       AS LARGE_CAR_YN                 " & vbNewLine _
                                            & "	     ,DEST.DELI_ATT                                           AS DELI_ATT                     " & vbNewLine _
                                            & "	     ,DEST.SALES_CD                                           AS SALES_CD                     " & vbNewLine _
                                            & "	     ,DEST.URIAGE_CD                                          AS URIAGE_CD                    " & vbNewLine _
                                            & "	     ,DEST.UNCHIN_SEIQTO_CD                                   AS UNCHIN_SEIQTO_CD             " & vbNewLine _
                                            & "	     ,DEST.SYS_ENT_DATE                                       AS SYS_ENT_DATE                 " & vbNewLine _
                                            & "	     ,DEST.SYS_UPD_DATE                                       AS SYS_UPD_DATE                 " & vbNewLine _
                                            & "	     ,DEST.SYS_UPD_TIME                                       AS SYS_UPD_TIME                 " & vbNewLine _
                                            & "	     ,DEST.SYS_DEL_FLG                                        AS SYS_DEL_FLG                  " & vbNewLine _
                                            & "	     ,DEST.REMARK                                             AS REMARK                       " & vbNewLine _
                                            & "	     ,DEST.SHIHARAI_AD                                        AS SHIHARAI_AD                  " & vbNewLine _
                                            & " FROM  $LM_MST$..M_DEST AS DEST                                                                " & vbNewLine


    Private Const SQL_WHERE_M_DEST As String = "WHERE                                       " & vbNewLine _
                                     & "     DEST.NRS_BR_CD = @NRS_BR_CD                    " & vbNewLine _
                                     & " AND DEST.CUST_CD_L = @CUST_CD_L                    " & vbNewLine _
                                     & " AND DEST.DEST_CD   = @DEST_CD                      " & vbNewLine

    ''' <summary>
    ''' 届先マスタ・更新対象データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>届先マスタ・運賃タリフセットマスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectIkkatsuM_DEST(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblKey As DataTable = ds.Tables("LMH030OUT_UPDATE_KEY")
        Dim inTblValue As DataTable = ds.Tables("LMH030OUT_UPDATE_VALUE")
        Dim inTblINOUT As DataTable = ds.Tables("LMH030INOUT")

        'INTableの条件rowの格納
        Me._Row = inTblKey.Rows(0)
        Dim RowValue As DataRow = inTblValue.Rows(0)
        Dim RowInOut As DataRow = inTblINOUT.Rows(0)


        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_IKKATSU_M_DEST)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMH030DAC.SQL_WHERE_M_DEST)            'SQL構築(データ抽出用Where句)

        Me._SqlPrmList = New ArrayList()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", _Row.Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", RowInOut.Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", RowValue.Item("EDIT_ITEM_VALUE1"), DBDataType.VARCHAR))


        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectIkkatsuM_DEST", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("EDI_CD", "EDI_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("KANA_NM", "KANA_NM")
        map.Add("ZIP", "ZIP")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        map.Add("CUST_DEST_CD", "CUST_DEST_CD")
        map.Add("TEL", "TEL")
        map.Add("FAX", "FAX")
        map.Add("JIS", "JIS")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_DEST")

        ''SQLの発行
        'Dim readerCnt As SqlDataReader = MyBase.GetSelectResult(cmd)
        ''処理件数の設定
        'readerCnt.Read()
        If ds.Tables("LMH030_M_DEST").Rows.Count = 0 Then
            'readerCnt.Close()
            MyBase.SetMessage("E079", New String() {"届先マスタ", "届先コード"})
            'Return ds
        End If
        'readerCnt.Close()

        Return ds

    End Function

    'ADD End   2018/02/26 依頼番号:1198 東レ・ダウ届先一括変更

#Region "SELECT_M_CUST(UNTIN_CALCULATION_KB)"
    Private Const SQL_SELECT_M_CUST_CALCULATION As String = " SELECT                                       " & vbNewLine _
                                     & " UNTIN_CALCULATION_KB                   AS UNTIN_CALCULATION_KB     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_CUST                       M_CUST         " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " M_CUST.NRS_BR_CD   = @NRS_BR_CD                       " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_CUST.CUST_CD_L   = @CUST_CD_L                       " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_CUST.CUST_CD_M   = @CUST_CD_M                       " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_CUST.CUST_CD_S   = '00'                             " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_CUST.CUST_CD_SS  = '00'                            " & vbNewLine


#End Region

#Region "SELECT_M_ZIP"

    Private Const SQL_SELECT_M_ZIP As String = " SELECT                                           " & vbNewLine _
                                     & "-- COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & " ZIP_NO                                AS ZIP_NO      " & vbNewLine _
                                     & ",JIS_CD                               AS JIS_CD     " & vbNewLine _
                                     & ",KEN_K                               AS KEN_K     " & vbNewLine _
                                     & ",CITY_K                               AS CITY_K     " & vbNewLine _
                                     & ",TOWN_K                               AS TOWN_K     " & vbNewLine _
                                     & ",KEN_N                               AS KEN_N     " & vbNewLine _
                                     & ",CITY_N                               AS CITY_N     " & vbNewLine _
                                     & ",TOWN_N                               AS TOWN_N     " & vbNewLine _
                                     & ",SYS_ENT_DATE                   AS  SYS_ENT_DATE    " & vbNewLine _
                                     & ",SYS_ENT_TIME                   AS  SYS_ENT_TIME    " & vbNewLine _
                                     & ",SYS_ENT_PGID                   AS  SYS_ENT_PGID    " & vbNewLine _
                                     & ",SYS_ENT_USER                   AS  SYS_ENT_USER    " & vbNewLine _
                                     & ",SYS_UPD_DATE                   AS  SYS_UPD_DATE    " & vbNewLine _
                                     & ",SYS_UPD_TIME                   AS  SYS_UPD_TIME    " & vbNewLine _
                                     & ",SYS_UPD_PGID                   AS  SYS_UPD_PGID    " & vbNewLine _
                                     & ",SYS_UPD_USER                   AS  SYS_UPD_USER    " & vbNewLine _
                                     & ",SYS_DEL_FLG                    AS  SYS_DEL_FLG     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_ZIP                       AS M_ZIP      " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " M_ZIP.ZIP_NO  = @DEST_ZIP                         " & vbNewLine

#End Region

#Region "GROUP_BY_M_ZIP"

    Private Const SQL_GROUP_BY_M_ZIP As String = " GROUP BY                                      " & vbNewLine _
                                     & " ZIP_NO             " & vbNewLine _
                                     & ",JIS_CD            " & vbNewLine _
                                     & ",KEN_K            " & vbNewLine _
                                     & ",CITY_K            " & vbNewLine _
                                     & ",TOWN_K            " & vbNewLine _
                                     & ",KEN_N            " & vbNewLine _
                                     & ",CITY_N            " & vbNewLine _
                                     & ",TOWN_N            " & vbNewLine _
& ",SYS_ENT_DATE   " & vbNewLine _
& ",SYS_ENT_TIME   " & vbNewLine _
& ",SYS_ENT_PGID   " & vbNewLine _
& ",SYS_ENT_USER   " & vbNewLine _
& ",SYS_UPD_DATE   " & vbNewLine _
& ",SYS_UPD_TIME   " & vbNewLine _
& ",SYS_UPD_PGID   " & vbNewLine _
& ",SYS_UPD_USER   " & vbNewLine _
& ",SYS_DEL_FLG   " & vbNewLine

#End Region

#Region "SELECT_M_UNSOCO"
    Private Const SQL_SELECT_M_UNSOCO As String = " SELECT                                     " & vbNewLine _
                                     & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_UNSOCO                       M_UNSOCO     " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " M_UNSOCO.NRS_BR_CD   = @NRS_BR_CD                     " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_UNSOCO.UNSOCO_CD   = @UNSO_CD                       " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_UNSOCO.UNSOCO_BR_CD   = @UNSO_BR_CD                 " & vbNewLine

#End Region

    'START UMANO 要望番号1302 支払運賃に伴う修正。
#Region "SELECT_M_UNSOCO(支払タリフコード取得)"
    Private Const SQL_SELECT_M_UNSOCO_SHIHARAI As String = " SELECT                            " & vbNewLine _
                                     & "  COUNT(*)                         AS MST_CNT          " & vbNewLine _
                                     & " ,SHIHARAITO_CD                    AS SHIHARAITO_CD    " & vbNewLine _
                                     & " ,UNCHIN_TARIFF_CD                 AS UNCHIN_TARIFF_CD " & vbNewLine _
                                     & " ,EXTC_TARIFF_CD                   AS EXTC_TARIFF_CD   " & vbNewLine _
                                     & " ,BETU_KYORI_CD                    AS BETU_KYORI_CD    " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_UNSOCO                       M_UNSOCO     " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " M_UNSOCO.NRS_BR_CD   = @NRS_BR_CD                     " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_UNSOCO.UNSOCO_CD   = @UNSO_CD                       " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_UNSOCO.UNSOCO_BR_CD   = @UNSO_BR_CD                 " & vbNewLine

#End Region

#Region "GROUP_BY_M_UNSOCO_SHIHARAI"

    Private Const SQL_GROUP_BY_M_UNSOCO_SHIHARAI As String = " GROUP BY                  " & vbNewLine _
                                                           & " SHIHARAITO_CD             " & vbNewLine _
                                                           & ",UNCHIN_TARIFF_CD          " & vbNewLine _
                                                           & ",EXTC_TARIFF_CD            " & vbNewLine _
                                                           & ",BETU_KYORI_CD             " & vbNewLine

#End Region

    'END UMANO 要望番号1302 支払運賃に伴う修正。

    '要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 Start
#Region "SELECT_M_UNSO_CUST_RPT"
    Private Const SQL_SELECT_M_UNSO_CUST_RPT As String = " SELECT       " & vbNewLine _
                                     & " COUNT(*)       AS MST_CNT      " & vbNewLine _
                                     & " FROM                           " & vbNewLine _
                                     & " $LM_MST$..M_UNSO_CUST_RPT      " & vbNewLine _
                                     & " WHERE                          " & vbNewLine _
                                     & " NRS_BR_CD      = @NRS_BR_CD    " & vbNewLine _
                                     & " AND                            " & vbNewLine _
                                     & " UNSOCO_CD      = @UNSO_CD      " & vbNewLine _
                                     & " AND                            " & vbNewLine _
                                     & " UNSOCO_BR_CD   = @UNSO_BR_CD   " & vbNewLine _
                                     & " AND                            " & vbNewLine _
                                     & " SYS_DEL_FLG    = '0'           " & vbNewLine

#End Region
    '要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 End

    '2012.03.21 大阪修正START
#Region "SELECT_M_UNSOCO(LIST)"
    Private Const SQL_SELECT_M_UNSOCO_LIST As String = " SELECT                                " & vbNewLine _
                                     & " COUNT(*)               AS MST_CNT                     " & vbNewLine _
                                     & ",NRS_BR_CD		        AS NRS_BR_CD		           " & vbNewLine _
                                     & ",UNSOCO_CD              AS UNSOCO_CD		           " & vbNewLine _
                                     & ",UNSOCO_BR_CD           AS UNSOCO_BR_CD                " & vbNewLine _
                                     & ",UNSOCO_NM              AS UNSOCO_NM                   " & vbNewLine _
                                     & ",UNSOCO_BR_NM           AS UNSOCO_BR_NM                " & vbNewLine _
                                     & ",UNSOCO_KB              AS UNSOCO_KB                   " & vbNewLine _
                                     & ",ZIP                    AS ZIP                         " & vbNewLine _
                                     & ",AD_1                   AS AD_1                        " & vbNewLine _
                                     & ",AD_2                   AS AD_2                        " & vbNewLine _
                                     & ",AD_3                   AS AD_3                        " & vbNewLine _
                                     & ",TEL                    AS TEL                         " & vbNewLine _
                                     & ",FAX                    AS FAX                         " & vbNewLine _
                                     & ",URL                    AS URL                         " & vbNewLine _
                                     & ",PIC                    AS PIC                         " & vbNewLine _
                                     & ",MOTOUKE_KB             AS MOTOUKE_KB                  " & vbNewLine _
                                     & ",NRS_SBETU_CD           AS NRS_SBETU_CD                " & vbNewLine _
                                     & ",NIHUDA_YN              AS NIHUDA_YN                   " & vbNewLine _
                                     & ",TARE_YN                AS TARE_YN                     " & vbNewLine _
                                     & ",UNCHIN_TARIFF_CD       AS UNCHIN_TARIFF_CD            " & vbNewLine _
                                     & ",EXTC_TARIFF_CD         AS EXTC_TARIFF_CD              " & vbNewLine _
                                     & ",BETU_KYORI_CD          AS BETU_KYORI_CD	           " & vbNewLine _
                                     & ",LAST_PU_TIME           AS LAST_PU_TIME		           " & vbNewLine _
                                     & ",EDI_USED_KBN           AS EDI_USED_KBN		           " & vbNewLine _
                                     & ",PICKLIST_GRP_KBN       AS PICKLIST_GRP_KBN		       " & vbNewLine _
                                     & ",SYS_ENT_DATE                   AS  SYS_ENT_DATE       " & vbNewLine _
                                     & ",SYS_ENT_TIME                   AS  SYS_ENT_TIME       " & vbNewLine _
                                     & ",SYS_ENT_PGID                   AS  SYS_ENT_PGID       " & vbNewLine _
                                     & ",SYS_ENT_USER                   AS  SYS_ENT_USER       " & vbNewLine _
                                     & ",SYS_UPD_DATE                   AS  SYS_UPD_DATE       " & vbNewLine _
                                     & ",SYS_UPD_TIME                   AS  SYS_UPD_TIME       " & vbNewLine _
                                     & ",SYS_UPD_PGID                   AS  SYS_UPD_PGID       " & vbNewLine _
                                     & ",SYS_UPD_USER                   AS  SYS_UPD_USER       " & vbNewLine _
                                     & ",SYS_DEL_FLG                    AS  SYS_DEL_FLG        " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_UNSOCO                       M_UNSOCO     " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " M_UNSOCO.NRS_BR_CD   = @NRS_BR_CD                     " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_UNSOCO.UNSOCO_CD   = @UNSO_CD                       " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_UNSOCO.UNSOCO_BR_CD   = @UNSO_BR_CD                 " & vbNewLine

#End Region

#Region "GROUP_BY_M_UNSOCO"

    Private Const SQL_GROUP_BY_M_UNSOCO_LIST As String = " GROUP BY                            " & vbNewLine _
                                     & "NRS_BR_CD             				                   " & vbNewLine _
                                     & ",UNSOCO_CD     					                       " & vbNewLine _
                                     & ",UNSOCO_BR_CD   				                       " & vbNewLine _
                                     & ",UNSOCO_NM   					                       " & vbNewLine _
                                     & ",UNSOCO_BR_NM   				                       " & vbNewLine _
                                     & ",UNSOCO_KB   					                       " & vbNewLine _
                                     & ",ZIP   						                           " & vbNewLine _
                                     & ",AD_1   					                           " & vbNewLine _
                                     & ",AD_2   					                           " & vbNewLine _
                                     & ",AD_3   					                           " & vbNewLine _
                                     & ",TEL   						                           " & vbNewLine _
                                     & ",FAX   						                           " & vbNewLine _
                                     & ",URL   						                           " & vbNewLine _
                                     & ",PIC   						                           " & vbNewLine _
                                     & ",MOTOUKE_KB   					                       " & vbNewLine _
                                     & ",NRS_SBETU_CD   				                       " & vbNewLine _
                                     & ",NIHUDA_YN   					                       " & vbNewLine _
                                     & ",TARE_YN   					                           " & vbNewLine _
                                     & ",UNCHIN_TARIFF_CD   				                   " & vbNewLine _
                                     & ",EXTC_TARIFF_CD   				                       " & vbNewLine _
                                     & ",BETU_KYORI_CD   				                       " & vbNewLine _
                                     & ",LAST_PU_TIME   				                       " & vbNewLine _
                                     & ",EDI_USED_KBN   				                       " & vbNewLine _
                                     & ",PICKLIST_GRP_KBN   				                       " & vbNewLine _
                                     & ",SYS_ENT_DATE   " & vbNewLine _
                                    & ",SYS_ENT_TIME   " & vbNewLine _
                                    & ",SYS_ENT_PGID   " & vbNewLine _
                                    & ",SYS_ENT_USER   " & vbNewLine _
                                    & ",SYS_UPD_DATE   " & vbNewLine _
                                    & ",SYS_UPD_TIME   " & vbNewLine _
                                    & ",SYS_UPD_PGID   " & vbNewLine _
                                    & ",SYS_UPD_USER   " & vbNewLine _
                                    & ",SYS_DEL_FLG   " & vbNewLine

#End Region
    '2012.03.21 大阪対応修正END

#Region "SELECT_M_UNCHIN_TARIFF"
    Private Const SQL_SELECT_M_UNCHIN_TARIFF As String = " SELECT                              " & vbNewLine _
                                     & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_UNCHIN_TARIFF              M_UNCHIN_TARIFF" & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " M_UNCHIN_TARIFF.NRS_BR_CD              = @NRS_BR_CD   " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_UNCHIN_TARIFF.UNCHIN_TARIFF_CD       = @UNCHIN_TARIFF_CD" & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_UNCHIN_TARIFF.STR_DATE   <= @STR_DATE               " & vbNewLine

#End Region

#Region "SELECT_M_YOKO_TARIFF"
    Private Const SQL_SELECT_M_YOKO_TARIFF As String = " SELECT                              " & vbNewLine _
                                     & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_YOKO_TARIFF_HD           TARIFF           " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " TARIFF.NRS_BR_CD          = @NRS_BR_CD                " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " TARIFF.YOKO_TARIFF_CD     = @YOKO_TARIFF_CD           " & vbNewLine

#End Region

#Region "SELECT_M_EXTC_UNCHIN"
    Private Const SQL_SELECT_M_EXTC_UNCHIN As String = " SELECT                                " & vbNewLine _
                                     & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_EXTC_UNCHIN                M_EXTC_UNCHIN  " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " M_EXTC_UNCHIN.NRS_BR_CD        = @NRS_BR_CD           " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_EXTC_UNCHIN.EXTC_TARIFF_CD   = @EXTC_TARIFF_CD      " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_EXTC_UNCHIN.JIS_CD           = '0000000'            " & vbNewLine


#End Region

#Region "SELECT_M_GOODS(OUTKA)"
    Private Const SQL_SELECT_M_GOODS As String = " SELECT                                      " & vbNewLine _
                                     & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & ",NRS_BR_CD                   AS  NRS_BR_CD    " & vbNewLine _
                                     & ",GOODS_CD_NRS                   AS  GOODS_CD_NRS    " & vbNewLine _
                                     & ",CUST_CD_L                   AS  CUST_CD_L    " & vbNewLine _
                                     & ",CUST_CD_M                   AS  CUST_CD_M    " & vbNewLine _
                                     & ",CUST_CD_S                   AS  CUST_CD_S    " & vbNewLine _
                                     & ",CUST_CD_SS                   AS  CUST_CD_SS    " & vbNewLine _
                                     & ",GOODS_CD_CUST                   AS  GOODS_CD_CUST    " & vbNewLine _
                                     & ",SEARCH_KEY_1                   AS  SEARCH_KEY_1    " & vbNewLine _
                                     & ",SEARCH_KEY_2                   AS  SEARCH_KEY_2    " & vbNewLine _
                                     & ",CUST_COST_CD1                   AS  CUST_COST_CD1    " & vbNewLine _
                                     & ",CUST_COST_CD2                   AS  CUST_COST_CD2    " & vbNewLine _
                                     & ",JAN_CD                   AS  JAN_CD    " & vbNewLine _
                                     & ",GOODS_NM_1                   AS  GOODS_NM_1    " & vbNewLine _
                                     & ",GOODS_NM_2                   AS  GOODS_NM_2    " & vbNewLine _
                                     & ",GOODS_NM_3                   AS  GOODS_NM_3    " & vbNewLine _
                                     & ",UP_GP_CD_1                   AS  UP_GP_CD_1    " & vbNewLine _
                                     & ",SHOBO_CD                   AS  SHOBO_CD    " & vbNewLine _
                                     & ",KIKEN_KB                   AS  KIKEN_KB    " & vbNewLine _
                                     & ",UN                   AS  UN    " & vbNewLine _
                                     & ",PG_KB                   AS  PG_KB    " & vbNewLine _
                                     & ",CLASS_1                   AS  CLASS_1    " & vbNewLine _
                                     & ",CLASS_2                   AS  CLASS_2    " & vbNewLine _
                                     & ",CLASS_3                   AS  CLASS_3    " & vbNewLine _
                                     & ",CHEM_MTRL_KB                   AS  CHEM_MTRL_KB    " & vbNewLine _
                                     & ",DOKU_KB                   AS  DOKU_KB    " & vbNewLine _
                                     & ",GAS_KANRI_KB                   AS  GAS_KANRI_KB    " & vbNewLine _
                                     & ",ONDO_KB                   AS  ONDO_KB    " & vbNewLine _
                                     & ",UNSO_ONDO_KB                   AS  UNSO_ONDO_KB    " & vbNewLine _
                                     & ",ONDO_MX                   AS  ONDO_MX    " & vbNewLine _
                                     & ",ONDO_MM                   AS  ONDO_MM    " & vbNewLine _
                                     & ",ONDO_STR_DATE                   AS  ONDO_STR_DATE    " & vbNewLine _
                                     & ",ONDO_END_DATE                   AS  ONDO_END_DATE    " & vbNewLine _
                                     & ",ONDO_UNSO_STR_DATE                   AS  ONDO_UNSO_STR_DATE    " & vbNewLine _
                                     & ",ONDO_UNSO_END_DATE                   AS  ONDO_UNSO_END_DATE    " & vbNewLine _
                                     & ",KYOKAI_GOODS_KB                   AS  KYOKAI_GOODS_KB    " & vbNewLine _
                                     & ",ALCTD_KB                   AS  ALCTD_KB    " & vbNewLine _
                                     & ",NB_UT                   AS  NB_UT    " & vbNewLine _
                                     & ",PKG_NB                   AS  PKG_NB    " & vbNewLine _
                                     & ",PKG_UT                   AS  PKG_UT    " & vbNewLine _
                                     & ",PLT_PER_PKG_UT                   AS  PLT_PER_PKG_UT    " & vbNewLine _
                                     & ",STD_IRIME_NB                   AS  STD_IRIME_NB    " & vbNewLine _
                                     & ",STD_IRIME_UT                   AS  STD_IRIME_UT    " & vbNewLine _
                                     & ",STD_WT_KGS                   AS  STD_WT_KGS    " & vbNewLine _
                                     & ",STD_CBM                   AS  STD_CBM    " & vbNewLine _
                                     & ",INKA_KAKO_SAGYO_KB_1                   AS  INKA_KAKO_SAGYO_KB_1    " & vbNewLine _
                                     & ",INKA_KAKO_SAGYO_KB_2                   AS  INKA_KAKO_SAGYO_KB_2    " & vbNewLine _
                                     & ",INKA_KAKO_SAGYO_KB_3                   AS  INKA_KAKO_SAGYO_KB_3    " & vbNewLine _
                                     & ",INKA_KAKO_SAGYO_KB_4                   AS  INKA_KAKO_SAGYO_KB_4    " & vbNewLine _
                                     & ",INKA_KAKO_SAGYO_KB_5                   AS  INKA_KAKO_SAGYO_KB_5    " & vbNewLine _
                                     & ",OUTKA_KAKO_SAGYO_KB_1                   AS  OUTKA_KAKO_SAGYO_KB_1    " & vbNewLine _
                                     & ",OUTKA_KAKO_SAGYO_KB_2                   AS  OUTKA_KAKO_SAGYO_KB_2    " & vbNewLine _
                                     & ",OUTKA_KAKO_SAGYO_KB_3                   AS  OUTKA_KAKO_SAGYO_KB_3    " & vbNewLine _
                                     & ",OUTKA_KAKO_SAGYO_KB_4                   AS  OUTKA_KAKO_SAGYO_KB_4    " & vbNewLine _
                                     & ",OUTKA_KAKO_SAGYO_KB_5                   AS  OUTKA_KAKO_SAGYO_KB_5    " & vbNewLine _
                                     & ",PKG_SAGYO                   AS  PKG_SAGYO    " & vbNewLine _
                                     & ",TARE_YN                   AS  TARE_YN    " & vbNewLine _
                                     & ",SP_NHS_YN                   AS  SP_NHS_YN    " & vbNewLine _
                                     & ",COA_YN                   AS  COA_YN    " & vbNewLine _
                                     & ",LOT_CTL_KB                   AS  LOT_CTL_KB    " & vbNewLine _
                                     & ",LT_DATE_CTL_KB                   AS  LT_DATE_CTL_KB    " & vbNewLine _
                                     & ",CRT_DATE_CTL_KB                   AS  CRT_DATE_CTL_KB    " & vbNewLine _
                                     & ",DEF_SPD_KB                   AS  DEF_SPD_KB    " & vbNewLine _
                                     & ",KITAKU_AM_UT_KB                   AS  KITAKU_AM_UT_KB    " & vbNewLine _
                                     & ",KITAKU_GOODS_UP                   AS  KITAKU_GOODS_UP    " & vbNewLine _
                                     & ",ORDER_KB                   AS  ORDER_KB    " & vbNewLine _
                                     & ",ORDER_NB                   AS  ORDER_NB    " & vbNewLine _
                                     & ",SHIP_CD_L                   AS  SHIP_CD_L    " & vbNewLine _
                                     & ",SKYU_MEI_YN                   AS  SKYU_MEI_YN    " & vbNewLine _
                                     & ",HIKIATE_ALERT_YN                   AS  HIKIATE_ALERT_YN    " & vbNewLine _
                                     & ",OUTKA_ATT                   AS  OUTKA_ATT    " & vbNewLine _
                                     & ",PRINT_NB                   AS  PRINT_NB    " & vbNewLine _
                                     & ",CONSUME_PERIOD_DATE                   AS  CONSUME_PERIOD_DATE    " & vbNewLine _
                                     & ",SYS_ENT_DATE                   AS  SYS_ENT_DATE    " & vbNewLine _
                                     & ",SYS_ENT_TIME                   AS  SYS_ENT_TIME    " & vbNewLine _
                                     & ",SYS_ENT_PGID                   AS  SYS_ENT_PGID    " & vbNewLine _
                                     & ",SYS_ENT_USER                   AS  SYS_ENT_USER    " & vbNewLine _
                                     & ",SYS_UPD_DATE                   AS  SYS_UPD_DATE    " & vbNewLine _
                                     & ",SYS_UPD_TIME                   AS  SYS_UPD_TIME    " & vbNewLine _
                                     & ",SYS_UPD_PGID                   AS  SYS_UPD_PGID    " & vbNewLine _
                                     & ",SYS_UPD_USER                   AS  SYS_UPD_USER    " & vbNewLine _
                                     & ",SYS_DEL_FLG                    AS  SYS_DEL_FLG     " & vbNewLine _
                                     & "--(2012.02.19) ロンザ対応START                      " & vbNewLine _
                                     & ",SIZE_KB                        AS  SIZE_KB         " & vbNewLine _
                                     & "--(2012.02.19) ロンザ対応END                        " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_GOODS                      M_GOODS        " & vbNewLine

#End Region

#Region "SELECT_M_GOODS_DETAILS(OUTKA)"
    Private Const SQL_SELECT_M_GOODS_DETAILS As String = " SELECT                                               " & vbNewLine _
                                     & " COUNT(*)                                       AS  MST_CNT                     " & vbNewLine _
                                     & ",M_GOODS.NRS_BR_CD                              AS  NRS_BR_CD                   " & vbNewLine _
                                     & ",M_GOODS.GOODS_CD_NRS                           AS  GOODS_CD_NRS                " & vbNewLine _
                                     & ",M_GOODS.CUST_CD_L                              AS  CUST_CD_L                   " & vbNewLine _
                                     & ",M_GOODS.CUST_CD_M                              AS  CUST_CD_M                   " & vbNewLine _
                                     & ",M_GOODS.CUST_CD_S                              AS  CUST_CD_S                   " & vbNewLine _
                                     & ",M_GOODS.CUST_CD_SS                             AS  CUST_CD_SS                  " & vbNewLine _
                                     & ",M_GOODS_DETAILS.SET_NAIYO                      AS  GOODS_CD_CUST               " & vbNewLine _
                                     & ",M_GOODS.SEARCH_KEY_1                           AS  SEARCH_KEY_1                " & vbNewLine _
                                     & ",M_GOODS.SEARCH_KEY_2                           AS  SEARCH_KEY_2                " & vbNewLine _
                                     & ",M_GOODS.CUST_COST_CD1                          AS  CUST_COST_CD1               " & vbNewLine _
                                     & ",M_GOODS.CUST_COST_CD2                          AS  CUST_COST_CD2               " & vbNewLine _
                                     & ",M_GOODS.JAN_CD                                 AS  JAN_CD                      " & vbNewLine _
                                     & ",M_GOODS.GOODS_NM_1                             AS  GOODS_NM_1                  " & vbNewLine _
                                     & ",M_GOODS.GOODS_NM_2                             AS  GOODS_NM_2                  " & vbNewLine _
                                     & ",M_GOODS.GOODS_NM_3                             AS  GOODS_NM_3                  " & vbNewLine _
                                     & ",M_GOODS.UP_GP_CD_1                             AS  UP_GP_CD_1                  " & vbNewLine _
                                     & ",M_GOODS.SHOBO_CD                               AS  SHOBO_CD                    " & vbNewLine _
                                     & ",M_GOODS.KIKEN_KB                               AS  KIKEN_KB                    " & vbNewLine _
                                     & ",M_GOODS.UN                                     AS  UN                          " & vbNewLine _
                                     & ",M_GOODS.PG_KB                                  AS  PG_KB                       " & vbNewLine _
                                     & ",M_GOODS.CLASS_1                                AS  CLASS_1                     " & vbNewLine _
                                     & ",M_GOODS.CLASS_2                                AS  CLASS_2                     " & vbNewLine _
                                     & ",M_GOODS.CLASS_3                                AS  CLASS_3                     " & vbNewLine _
                                     & ",M_GOODS.CHEM_MTRL_KB                           AS  CHEM_MTRL_KB                " & vbNewLine _
                                     & ",M_GOODS.DOKU_KB                                AS  DOKU_KB                     " & vbNewLine _
                                     & ",M_GOODS.GAS_KANRI_KB                           AS  GAS_KANRI_KB                " & vbNewLine _
                                     & ",M_GOODS.ONDO_KB                                AS  ONDO_KB                     " & vbNewLine _
                                     & ",M_GOODS.UNSO_ONDO_KB                           AS  UNSO_ONDO_KB                " & vbNewLine _
                                     & ",M_GOODS.ONDO_MX                                AS  ONDO_MX                     " & vbNewLine _
                                     & ",M_GOODS.ONDO_MM                                AS  ONDO_MM                     " & vbNewLine _
                                     & ",M_GOODS.ONDO_STR_DATE                          AS  ONDO_STR_DATE               " & vbNewLine _
                                     & ",M_GOODS.ONDO_END_DATE                          AS  ONDO_END_DATE               " & vbNewLine _
                                     & ",M_GOODS.ONDO_UNSO_STR_DATE                     AS  ONDO_UNSO_STR_DATE          " & vbNewLine _
                                     & ",M_GOODS.ONDO_UNSO_END_DATE                     AS  ONDO_UNSO_END_DATE          " & vbNewLine _
                                     & ",M_GOODS.KYOKAI_GOODS_KB                        AS  KYOKAI_GOODS_KB             " & vbNewLine _
                                     & ",M_GOODS.ALCTD_KB                               AS  ALCTD_KB                    " & vbNewLine _
                                     & ",M_GOODS.NB_UT                                  AS  NB_UT                       " & vbNewLine _
                                     & ",M_GOODS.PKG_NB                                 AS  PKG_NB                      " & vbNewLine _
                                     & ",M_GOODS.PKG_UT                                 AS  PKG_UT                      " & vbNewLine _
                                     & ",M_GOODS.PLT_PER_PKG_UT                         AS  PLT_PER_PKG_UT              " & vbNewLine _
                                     & ",M_GOODS.STD_IRIME_NB                           AS  STD_IRIME_NB                " & vbNewLine _
                                     & ",M_GOODS.STD_IRIME_UT                           AS  STD_IRIME_UT                " & vbNewLine _
                                     & ",M_GOODS.STD_WT_KGS                             AS  STD_WT_KGS                  " & vbNewLine _
                                     & ",M_GOODS.STD_CBM                                AS  STD_CBM                     " & vbNewLine _
                                     & ",M_GOODS.INKA_KAKO_SAGYO_KB_1                   AS  INKA_KAKO_SAGYO_KB_1        " & vbNewLine _
                                     & ",M_GOODS.INKA_KAKO_SAGYO_KB_2                   AS  INKA_KAKO_SAGYO_KB_2        " & vbNewLine _
                                     & ",M_GOODS.INKA_KAKO_SAGYO_KB_3                   AS  INKA_KAKO_SAGYO_KB_3        " & vbNewLine _
                                     & ",M_GOODS.INKA_KAKO_SAGYO_KB_4                   AS  INKA_KAKO_SAGYO_KB_4        " & vbNewLine _
                                     & ",M_GOODS.INKA_KAKO_SAGYO_KB_5                   AS  INKA_KAKO_SAGYO_KB_5        " & vbNewLine _
                                     & ",M_GOODS.OUTKA_KAKO_SAGYO_KB_1                  AS  OUTKA_KAKO_SAGYO_KB_1       " & vbNewLine _
                                     & ",M_GOODS.OUTKA_KAKO_SAGYO_KB_2                  AS  OUTKA_KAKO_SAGYO_KB_2       " & vbNewLine _
                                     & ",M_GOODS.OUTKA_KAKO_SAGYO_KB_3                  AS  OUTKA_KAKO_SAGYO_KB_3       " & vbNewLine _
                                     & ",M_GOODS.OUTKA_KAKO_SAGYO_KB_4                  AS  OUTKA_KAKO_SAGYO_KB_4       " & vbNewLine _
                                     & ",M_GOODS.OUTKA_KAKO_SAGYO_KB_5                  AS  OUTKA_KAKO_SAGYO_KB_5       " & vbNewLine _
                                     & ",M_GOODS.PKG_SAGYO                              AS  PKG_SAGYO                   " & vbNewLine _
                                     & ",M_GOODS.TARE_YN                                AS  TARE_YN                     " & vbNewLine _
                                     & ",M_GOODS.SP_NHS_YN                              AS  SP_NHS_YN                   " & vbNewLine _
                                     & ",M_GOODS.COA_YN                                 AS  COA_YN                      " & vbNewLine _
                                     & ",M_GOODS.LOT_CTL_KB                             AS  LOT_CTL_KB                  " & vbNewLine _
                                     & ",M_GOODS.LT_DATE_CTL_KB                         AS  LT_DATE_CTL_KB              " & vbNewLine _
                                     & ",M_GOODS.CRT_DATE_CTL_KB                        AS  CRT_DATE_CTL_KB             " & vbNewLine _
                                     & ",M_GOODS.DEF_SPD_KB                             AS  DEF_SPD_KB                  " & vbNewLine _
                                     & ",M_GOODS.KITAKU_AM_UT_KB                        AS  KITAKU_AM_UT_KB             " & vbNewLine _
                                     & ",M_GOODS.KITAKU_GOODS_UP                        AS  KITAKU_GOODS_UP             " & vbNewLine _
                                     & ",M_GOODS.ORDER_KB                               AS  ORDER_KB                    " & vbNewLine _
                                     & ",M_GOODS.ORDER_NB                               AS  ORDER_NB                    " & vbNewLine _
                                     & ",M_GOODS.SHIP_CD_L                              AS  SHIP_CD_L                   " & vbNewLine _
                                     & ",M_GOODS.SKYU_MEI_YN                            AS  SKYU_MEI_YN                 " & vbNewLine _
                                     & ",M_GOODS.HIKIATE_ALERT_YN                       AS  HIKIATE_ALERT_YN            " & vbNewLine _
                                     & ",M_GOODS.OUTKA_ATT                              AS  OUTKA_ATT                   " & vbNewLine _
                                     & ",M_GOODS.PRINT_NB                               AS  PRINT_NB                    " & vbNewLine _
                                     & ",M_GOODS.CONSUME_PERIOD_DATE                    AS  CONSUME_PERIOD_DATE         " & vbNewLine _
                                     & ",M_GOODS.SYS_ENT_DATE                           AS  SYS_ENT_DATE                " & vbNewLine _
                                     & ",M_GOODS.SYS_ENT_TIME                           AS  SYS_ENT_TIME                " & vbNewLine _
                                     & ",M_GOODS.SYS_ENT_PGID                           AS  SYS_ENT_PGID                " & vbNewLine _
                                     & ",M_GOODS.SYS_ENT_USER                           AS  SYS_ENT_USER                " & vbNewLine _
                                     & ",M_GOODS.SYS_UPD_DATE                           AS  SYS_UPD_DATE                " & vbNewLine _
                                     & ",M_GOODS.SYS_UPD_TIME                           AS  SYS_UPD_TIME                " & vbNewLine _
                                     & ",M_GOODS.SYS_UPD_PGID                           AS  SYS_UPD_PGID                " & vbNewLine _
                                     & ",M_GOODS.SYS_UPD_USER                           AS  SYS_UPD_USER                " & vbNewLine _
                                     & ",M_GOODS.SYS_DEL_FLG                            AS  SYS_DEL_FLG                 " & vbNewLine _
                                     & ",M_GOODS.SIZE_KB                                AS  SIZE_KB                     " & vbNewLine _
                                     & " FROM                                                                           " & vbNewLine _
                                     & " $LM_MST$..M_GOODS                      M_GOODS                                 " & vbNewLine _
                                     & " LEFT JOIN                                                                      " & vbNewLine _
                                     & " $LM_MST$..M_GOODS_DETAILS              M_GOODS_DETAILS                         " & vbNewLine _
                                     & " ON  M_GOODS_DETAILS.NRS_BR_CD    = M_GOODS.NRS_BR_CD                           " & vbNewLine _
                                     & " AND M_GOODS_DETAILS.GOODS_CD_NRS = M_GOODS.GOODS_CD_NRS                        " & vbNewLine _
                                     & " AND M_GOODS_DETAILS.SUB_KB       = '65'                                        " & vbNewLine

#End Region


#Region "GROUP_BY_M_GOODS(OUTKA)"

    Private Const SQL_GROUP_BY_M_GOODS As String = " GROUP BY                                      " & vbNewLine _
                                     & "NRS_BR_CD             " & vbNewLine _
& ",GOODS_CD_NRS          " & vbNewLine _
& ",CUST_CD_L             " & vbNewLine _
& ",CUST_CD_M             " & vbNewLine _
& ",CUST_CD_S             " & vbNewLine _
& ",CUST_CD_SS            " & vbNewLine _
& ",GOODS_CD_CUST         " & vbNewLine _
& ",SEARCH_KEY_1          " & vbNewLine _
& ",SEARCH_KEY_2          " & vbNewLine _
& ",CUST_COST_CD1         " & vbNewLine _
& ",CUST_COST_CD2         " & vbNewLine _
& ",JAN_CD                " & vbNewLine _
& ",GOODS_NM_1            " & vbNewLine _
& ",GOODS_NM_2            " & vbNewLine _
& ",GOODS_NM_3            " & vbNewLine _
& ",UP_GP_CD_1            " & vbNewLine _
& ",SHOBO_CD              " & vbNewLine _
& ",KIKEN_KB              " & vbNewLine _
& ",UN                    " & vbNewLine _
& ",PG_KB                 " & vbNewLine _
& ",CLASS_1               " & vbNewLine _
& ",CLASS_2               " & vbNewLine _
& ",CLASS_3               " & vbNewLine _
& ",CHEM_MTRL_KB          " & vbNewLine _
& ",DOKU_KB               " & vbNewLine _
& ",GAS_KANRI_KB          " & vbNewLine _
& ",ONDO_KB               " & vbNewLine _
& ",UNSO_ONDO_KB          " & vbNewLine _
& ",ONDO_MX               " & vbNewLine _
& ",ONDO_MM               " & vbNewLine _
& ",ONDO_STR_DATE         " & vbNewLine _
& ",ONDO_END_DATE         " & vbNewLine _
& ",ONDO_UNSO_STR_DATE    " & vbNewLine _
& ",ONDO_UNSO_END_DATE    " & vbNewLine _
& ",KYOKAI_GOODS_KB       " & vbNewLine _
& ",ALCTD_KB              " & vbNewLine _
& ",NB_UT                 " & vbNewLine _
& ",PKG_NB                " & vbNewLine _
& ",PKG_UT                " & vbNewLine _
& ",PLT_PER_PKG_UT        " & vbNewLine _
& ",STD_IRIME_NB          " & vbNewLine _
& ",STD_IRIME_UT          " & vbNewLine _
& ",STD_WT_KGS            " & vbNewLine _
& ",STD_CBM               " & vbNewLine _
& ",INKA_KAKO_SAGYO_KB_1  " & vbNewLine _
& ",INKA_KAKO_SAGYO_KB_2  " & vbNewLine _
& ",INKA_KAKO_SAGYO_KB_3  " & vbNewLine _
& ",INKA_KAKO_SAGYO_KB_4  " & vbNewLine _
& ",INKA_KAKO_SAGYO_KB_5  " & vbNewLine _
& ",OUTKA_KAKO_SAGYO_KB_1 " & vbNewLine _
& ",OUTKA_KAKO_SAGYO_KB_2 " & vbNewLine _
& ",OUTKA_KAKO_SAGYO_KB_3 " & vbNewLine _
& ",OUTKA_KAKO_SAGYO_KB_4 " & vbNewLine _
& ",OUTKA_KAKO_SAGYO_KB_5 " & vbNewLine _
& ",PKG_SAGYO             " & vbNewLine _
& ",TARE_YN               " & vbNewLine _
& ",SP_NHS_YN             " & vbNewLine _
& ",COA_YN                " & vbNewLine _
& ",LOT_CTL_KB            " & vbNewLine _
& ",LT_DATE_CTL_KB        " & vbNewLine _
& ",CRT_DATE_CTL_KB       " & vbNewLine _
& ",DEF_SPD_KB            " & vbNewLine _
& ",KITAKU_AM_UT_KB       " & vbNewLine _
& ",KITAKU_GOODS_UP       " & vbNewLine _
& ",ORDER_KB              " & vbNewLine _
& ",ORDER_NB              " & vbNewLine _
& ",SHIP_CD_L             " & vbNewLine _
& ",SKYU_MEI_YN           " & vbNewLine _
& ",HIKIATE_ALERT_YN      " & vbNewLine _
& ",OUTKA_ATT             " & vbNewLine _
& ",PRINT_NB              " & vbNewLine _
& ",CONSUME_PERIOD_DATE   " & vbNewLine _
& "--(2012.02.19) ロンザ対応START  " & vbNewLine _
& ",SIZE_KB   " & vbNewLine _
& "--(2012.02.19) ロンザ対応END  " & vbNewLine _
& ",SYS_ENT_DATE   " & vbNewLine _
& ",SYS_ENT_TIME   " & vbNewLine _
& ",SYS_ENT_PGID   " & vbNewLine _
& ",SYS_ENT_USER   " & vbNewLine _
& ",SYS_UPD_DATE   " & vbNewLine _
& ",SYS_UPD_TIME   " & vbNewLine _
& ",SYS_UPD_PGID   " & vbNewLine _
& ",SYS_UPD_USER   " & vbNewLine _
& ",SYS_DEL_FLG   " & vbNewLine

#End Region

#Region "GROUP_BY_M_GOODS_DETAILS(OUTKA)"

    Private Const SQL_GROUP_BY_M_GOODS_DETAILS As String = "GROUP BY  " & vbNewLine _
                                    & " M_GOODS.NRS_BR_CD             " & vbNewLine _
                                    & ",M_GOODS.GOODS_CD_NRS          " & vbNewLine _
                                    & ",M_GOODS.CUST_CD_L             " & vbNewLine _
                                    & ",M_GOODS.CUST_CD_M             " & vbNewLine _
                                    & ",M_GOODS.CUST_CD_S             " & vbNewLine _
                                    & ",M_GOODS.CUST_CD_SS            " & vbNewLine _
                                    & ",M_GOODS_DETAILS.SET_NAIYO     " & vbNewLine _
                                    & ",M_GOODS.SEARCH_KEY_1          " & vbNewLine _
                                    & ",M_GOODS.SEARCH_KEY_2          " & vbNewLine _
                                    & ",M_GOODS.CUST_COST_CD1         " & vbNewLine _
                                    & ",M_GOODS.CUST_COST_CD2         " & vbNewLine _
                                    & ",M_GOODS.JAN_CD                " & vbNewLine _
                                    & ",M_GOODS.GOODS_NM_1            " & vbNewLine _
                                    & ",M_GOODS.GOODS_NM_2            " & vbNewLine _
                                    & ",M_GOODS.GOODS_NM_3            " & vbNewLine _
                                    & ",M_GOODS.UP_GP_CD_1            " & vbNewLine _
                                    & ",M_GOODS.SHOBO_CD              " & vbNewLine _
                                    & ",M_GOODS.KIKEN_KB              " & vbNewLine _
                                    & ",M_GOODS.UN                    " & vbNewLine _
                                    & ",M_GOODS.PG_KB                 " & vbNewLine _
                                    & ",M_GOODS.CLASS_1               " & vbNewLine _
                                    & ",M_GOODS.CLASS_2               " & vbNewLine _
                                    & ",M_GOODS.CLASS_3               " & vbNewLine _
                                    & ",M_GOODS.CHEM_MTRL_KB          " & vbNewLine _
                                    & ",M_GOODS.DOKU_KB               " & vbNewLine _
                                    & ",M_GOODS.GAS_KANRI_KB          " & vbNewLine _
                                    & ",M_GOODS.ONDO_KB               " & vbNewLine _
                                    & ",M_GOODS.UNSO_ONDO_KB          " & vbNewLine _
                                    & ",M_GOODS.ONDO_MX               " & vbNewLine _
                                    & ",M_GOODS.ONDO_MM               " & vbNewLine _
                                    & ",M_GOODS.ONDO_STR_DATE         " & vbNewLine _
                                    & ",M_GOODS.ONDO_END_DATE         " & vbNewLine _
                                    & ",M_GOODS.ONDO_UNSO_STR_DATE    " & vbNewLine _
                                    & ",M_GOODS.ONDO_UNSO_END_DATE    " & vbNewLine _
                                    & ",M_GOODS.KYOKAI_GOODS_KB       " & vbNewLine _
                                    & ",M_GOODS.ALCTD_KB              " & vbNewLine _
                                    & ",M_GOODS.NB_UT                 " & vbNewLine _
                                    & ",M_GOODS.PKG_NB                " & vbNewLine _
                                    & ",M_GOODS.PKG_UT                " & vbNewLine _
                                    & ",M_GOODS.PLT_PER_PKG_UT        " & vbNewLine _
                                    & ",M_GOODS.STD_IRIME_NB          " & vbNewLine _
                                    & ",M_GOODS.STD_IRIME_UT          " & vbNewLine _
                                    & ",M_GOODS.STD_WT_KGS            " & vbNewLine _
                                    & ",M_GOODS.STD_CBM               " & vbNewLine _
                                    & ",M_GOODS.INKA_KAKO_SAGYO_KB_1  " & vbNewLine _
                                    & ",M_GOODS.INKA_KAKO_SAGYO_KB_2  " & vbNewLine _
                                    & ",M_GOODS.INKA_KAKO_SAGYO_KB_3  " & vbNewLine _
                                    & ",M_GOODS.INKA_KAKO_SAGYO_KB_4  " & vbNewLine _
                                    & ",M_GOODS.INKA_KAKO_SAGYO_KB_5  " & vbNewLine _
                                    & ",M_GOODS.OUTKA_KAKO_SAGYO_KB_1 " & vbNewLine _
                                    & ",M_GOODS.OUTKA_KAKO_SAGYO_KB_2 " & vbNewLine _
                                    & ",M_GOODS.OUTKA_KAKO_SAGYO_KB_3 " & vbNewLine _
                                    & ",M_GOODS.OUTKA_KAKO_SAGYO_KB_4 " & vbNewLine _
                                    & ",M_GOODS.OUTKA_KAKO_SAGYO_KB_5 " & vbNewLine _
                                    & ",M_GOODS.PKG_SAGYO             " & vbNewLine _
                                    & ",M_GOODS.TARE_YN               " & vbNewLine _
                                    & ",M_GOODS.SP_NHS_YN             " & vbNewLine _
                                    & ",M_GOODS.COA_YN                " & vbNewLine _
                                    & ",M_GOODS.LOT_CTL_KB            " & vbNewLine _
                                    & ",M_GOODS.LT_DATE_CTL_KB        " & vbNewLine _
                                    & ",M_GOODS.CRT_DATE_CTL_KB       " & vbNewLine _
                                    & ",M_GOODS.DEF_SPD_KB            " & vbNewLine _
                                    & ",M_GOODS.KITAKU_AM_UT_KB       " & vbNewLine _
                                    & ",M_GOODS.KITAKU_GOODS_UP       " & vbNewLine _
                                    & ",M_GOODS.ORDER_KB              " & vbNewLine _
                                    & ",M_GOODS.ORDER_NB              " & vbNewLine _
                                    & ",M_GOODS.SHIP_CD_L             " & vbNewLine _
                                    & ",M_GOODS.SKYU_MEI_YN           " & vbNewLine _
                                    & ",M_GOODS.HIKIATE_ALERT_YN      " & vbNewLine _
                                    & ",M_GOODS.OUTKA_ATT             " & vbNewLine _
                                    & ",M_GOODS.PRINT_NB              " & vbNewLine _
                                    & ",M_GOODS.CONSUME_PERIOD_DATE   " & vbNewLine _
                                    & ",M_GOODS.SIZE_KB               " & vbNewLine _
                                    & ",M_GOODS.SYS_ENT_DATE          " & vbNewLine _
                                    & ",M_GOODS.SYS_ENT_TIME          " & vbNewLine _
                                    & ",M_GOODS.SYS_ENT_PGID          " & vbNewLine _
                                    & ",M_GOODS.SYS_ENT_USER          " & vbNewLine _
                                    & ",M_GOODS.SYS_UPD_DATE          " & vbNewLine _
                                    & ",M_GOODS.SYS_UPD_TIME          " & vbNewLine _
                                    & ",M_GOODS.SYS_UPD_PGID          " & vbNewLine _
                                    & ",M_GOODS.SYS_UPD_USER          " & vbNewLine _
                                    & ",M_GOODS.SYS_DEL_FLG           " & vbNewLine

#End Region

#Region "SELECT_M_GOODS(UNSO)"
    Private Const SQL_SELECT_M_GOODS_UNSODATA As String = " SELECT                             " & vbNewLine _
                                     & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & ",NRS_BR_CD                   AS  NRS_BR_CD    " & vbNewLine _
                                     & ",GOODS_CD_NRS                   AS  GOODS_CD_NRS    " & vbNewLine _
                                     & ",CUST_CD_L                   AS  CUST_CD_L    " & vbNewLine _
                                     & ",CUST_CD_M                   AS  CUST_CD_M    " & vbNewLine _
                                     & ",CUST_CD_S                   AS  CUST_CD_S    " & vbNewLine _
                                     & ",CUST_CD_SS                   AS  CUST_CD_SS    " & vbNewLine _
                                     & ",GOODS_CD_CUST                   AS  GOODS_CD_CUST    " & vbNewLine _
                                     & ",SEARCH_KEY_1                   AS  SEARCH_KEY_1    " & vbNewLine _
                                     & ",SEARCH_KEY_2                   AS  SEARCH_KEY_2    " & vbNewLine _
                                     & ",CUST_COST_CD1                   AS  CUST_COST_CD1    " & vbNewLine _
                                     & ",CUST_COST_CD2                   AS  CUST_COST_CD2    " & vbNewLine _
                                     & ",JAN_CD                   AS  JAN_CD    " & vbNewLine _
                                     & ",GOODS_NM_1                   AS  GOODS_NM_1    " & vbNewLine _
                                     & ",GOODS_NM_2                   AS  GOODS_NM_2    " & vbNewLine _
                                     & ",GOODS_NM_3                   AS  GOODS_NM_3    " & vbNewLine _
                                     & ",UP_GP_CD_1                   AS  UP_GP_CD_1    " & vbNewLine _
                                     & ",SHOBO_CD                   AS  SHOBO_CD    " & vbNewLine _
                                     & ",KIKEN_KB                   AS  KIKEN_KB    " & vbNewLine _
                                     & ",UN                   AS  UN    " & vbNewLine _
                                     & ",PG_KB                   AS  PG_KB    " & vbNewLine _
                                     & ",CLASS_1                   AS  CLASS_1    " & vbNewLine _
                                     & ",CLASS_2                   AS  CLASS_2    " & vbNewLine _
                                     & ",CLASS_3                   AS  CLASS_3    " & vbNewLine _
                                     & ",CHEM_MTRL_KB                   AS  CHEM_MTRL_KB    " & vbNewLine _
                                     & ",DOKU_KB                   AS  DOKU_KB    " & vbNewLine _
                                     & ",GAS_KANRI_KB                   AS  GAS_KANRI_KB    " & vbNewLine _
                                     & ",ONDO_KB                   AS  ONDO_KB    " & vbNewLine _
                                     & ",UNSO_ONDO_KB                   AS  UNSO_ONDO_KB    " & vbNewLine _
                                     & ",ONDO_MX                   AS  ONDO_MX    " & vbNewLine _
                                     & ",ONDO_MM                   AS  ONDO_MM    " & vbNewLine _
                                     & ",ONDO_STR_DATE                   AS  ONDO_STR_DATE    " & vbNewLine _
                                     & ",ONDO_END_DATE                   AS  ONDO_END_DATE    " & vbNewLine _
                                     & ",ONDO_UNSO_STR_DATE                   AS  ONDO_UNSO_STR_DATE    " & vbNewLine _
                                     & ",ONDO_UNSO_END_DATE                   AS  ONDO_UNSO_END_DATE    " & vbNewLine _
                                     & ",KYOKAI_GOODS_KB                   AS  KYOKAI_GOODS_KB    " & vbNewLine _
                                     & ",ALCTD_KB                   AS  ALCTD_KB    " & vbNewLine _
                                     & ",NB_UT                   AS  NB_UT    " & vbNewLine _
                                     & ",PKG_NB                   AS  PKG_NB    " & vbNewLine _
                                     & ",PKG_UT                   AS  PKG_UT    " & vbNewLine _
                                     & ",PLT_PER_PKG_UT                   AS  PLT_PER_PKG_UT    " & vbNewLine _
                                     & ",STD_IRIME_NB                   AS  STD_IRIME_NB    " & vbNewLine _
                                     & ",STD_IRIME_UT                   AS  STD_IRIME_UT    " & vbNewLine _
                                     & ",STD_WT_KGS                   AS  STD_WT_KGS    " & vbNewLine _
                                     & ",STD_CBM                   AS  STD_CBM    " & vbNewLine _
                                     & ",INKA_KAKO_SAGYO_KB_1                   AS  INKA_KAKO_SAGYO_KB_1    " & vbNewLine _
                                     & ",INKA_KAKO_SAGYO_KB_2                   AS  INKA_KAKO_SAGYO_KB_2    " & vbNewLine _
                                     & ",INKA_KAKO_SAGYO_KB_3                   AS  INKA_KAKO_SAGYO_KB_3    " & vbNewLine _
                                     & ",INKA_KAKO_SAGYO_KB_4                   AS  INKA_KAKO_SAGYO_KB_4    " & vbNewLine _
                                     & ",INKA_KAKO_SAGYO_KB_5                   AS  INKA_KAKO_SAGYO_KB_5    " & vbNewLine _
                                     & ",OUTKA_KAKO_SAGYO_KB_1                   AS  OUTKA_KAKO_SAGYO_KB_1    " & vbNewLine _
                                     & ",OUTKA_KAKO_SAGYO_KB_2                   AS  OUTKA_KAKO_SAGYO_KB_2    " & vbNewLine _
                                     & ",OUTKA_KAKO_SAGYO_KB_3                   AS  OUTKA_KAKO_SAGYO_KB_3    " & vbNewLine _
                                     & ",OUTKA_KAKO_SAGYO_KB_4                   AS  OUTKA_KAKO_SAGYO_KB_4    " & vbNewLine _
                                     & ",OUTKA_KAKO_SAGYO_KB_5                   AS  OUTKA_KAKO_SAGYO_KB_5    " & vbNewLine _
                                     & ",PKG_SAGYO                   AS  PKG_SAGYO    " & vbNewLine _
                                     & ",TARE_YN                   AS  TARE_YN    " & vbNewLine _
                                     & ",SP_NHS_YN                   AS  SP_NHS_YN    " & vbNewLine _
                                     & ",COA_YN                   AS  COA_YN    " & vbNewLine _
                                     & ",LOT_CTL_KB                   AS  LOT_CTL_KB    " & vbNewLine _
                                     & ",LT_DATE_CTL_KB                   AS  LT_DATE_CTL_KB    " & vbNewLine _
                                     & ",CRT_DATE_CTL_KB                   AS  CRT_DATE_CTL_KB    " & vbNewLine _
                                     & ",DEF_SPD_KB                   AS  DEF_SPD_KB    " & vbNewLine _
                                     & ",KITAKU_AM_UT_KB                   AS  KITAKU_AM_UT_KB    " & vbNewLine _
                                     & ",KITAKU_GOODS_UP                   AS  KITAKU_GOODS_UP    " & vbNewLine _
                                     & ",ORDER_KB                   AS  ORDER_KB    " & vbNewLine _
                                     & ",ORDER_NB                   AS  ORDER_NB    " & vbNewLine _
                                     & ",SHIP_CD_L                   AS  SHIP_CD_L    " & vbNewLine _
                                     & ",SKYU_MEI_YN                   AS  SKYU_MEI_YN    " & vbNewLine _
                                     & ",HIKIATE_ALERT_YN                   AS  HIKIATE_ALERT_YN    " & vbNewLine _
                                     & ",OUTKA_ATT                   AS  OUTKA_ATT    " & vbNewLine _
                                     & ",PRINT_NB                   AS  PRINT_NB    " & vbNewLine _
                                     & ",CONSUME_PERIOD_DATE                   AS  CONSUME_PERIOD_DATE    " & vbNewLine _
                                     & ",SYS_ENT_DATE                   AS  SYS_ENT_DATE    " & vbNewLine _
                                     & ",SYS_ENT_TIME                   AS  SYS_ENT_TIME    " & vbNewLine _
                                     & ",SYS_ENT_PGID                   AS  SYS_ENT_PGID    " & vbNewLine _
                                     & ",SYS_ENT_USER                   AS  SYS_ENT_USER    " & vbNewLine _
                                     & ",SYS_UPD_DATE                   AS  SYS_UPD_DATE    " & vbNewLine _
                                     & ",SYS_UPD_TIME                   AS  SYS_UPD_TIME    " & vbNewLine _
                                     & ",SYS_UPD_PGID                   AS  SYS_UPD_PGID    " & vbNewLine _
                                     & ",SYS_UPD_USER                   AS  SYS_UPD_USER    " & vbNewLine _
                                     & ",SYS_DEL_FLG                    AS  SYS_DEL_FLG     " & vbNewLine _
                                     & "--(2012.02.19) ロンザ対応START                      " & vbNewLine _
                                     & ",SIZE_KB                        AS  SIZE_KB         " & vbNewLine _
                                     & "--(2012.02.19) ロンザ対応END                        " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_GOODS                      M_GOODS        " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " M_GOODS.NRS_BR_CD   = @NRS_BR_CD                       " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_GOODS.GOODS_CD_NRS  = @NRS_GOODS_CD                  " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_GOODS.SYS_DEL_FLG = '0'                             " & vbNewLine

#End Region

    '2012.03.18 修正START
#Region "SELECT_C_OUTKA_L"
    Private Const SQL_SELECT_C_OUTKA_L As String = " SELECT                                       " & vbNewLine _
                                     & " COUNT(*)                               AS OUTKA_L_CNT    " & vbNewLine _
                                     & " FROM                                                     " & vbNewLine _
                                     & " $LM_TRN$..C_OUTKA_L                     C_OUTKA_L        " & vbNewLine _
                                     & " WHERE                                                    " & vbNewLine _
                                     & " C_OUTKA_L.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                     & " AND                                                      " & vbNewLine _
                                     & " C_OUTKA_L.CUST_ORD_NO = @CUST_ORD_NO                     " & vbNewLine _
                                     & " AND                                                      " & vbNewLine _
                                     & " C_OUTKA_L.NRS_BR_CD   = @NRS_BR_CD                       " & vbNewLine _
                                     & " AND                                                      " & vbNewLine _
                                     & " C_OUTKA_L.CUST_CD_L = @CUST_CD_L                         " & vbNewLine _
                                     & " AND                                                      " & vbNewLine _
                                     & " C_OUTKA_L.CUST_CD_M   = @CUST_CD_M                       " & vbNewLine

#End Region
    '2012.03.18 修正END

#If True Then       'ADD 2019/04/24 依頼番号 : 005588   【LMS】古河日立EDI受信_重複データが重複でLMSへ取込されるバグ(古河佐藤) 

#Region "SELECT_C_OUTKA_L"
    Private Const SQL_SELECT_F_UNSO_L As String = " SELECT                                       " & vbNewLine _
                                     & " COUNT(*)                               AS UNSO_L_CNT    " & vbNewLine _
                                     & " FROM                                                    " & vbNewLine _
                                     & " $LM_TRN$..F_UNSO_L                     F_UNSO_L         " & vbNewLine _
                                     & " WHERE                                                    " & vbNewLine _
                                     & " F_UNSO_L.SYS_DEL_FLG = '0'                              " & vbNewLine _
                                     & " AND                                                      " & vbNewLine _
                                     & " F_UNSO_L.CUST_REF_NO = @CUST_ORD_NO                     " & vbNewLine _
                                     & " AND                                                      " & vbNewLine _
                                     & " F_UNSO_L.NRS_BR_CD   = @NRS_BR_CD                       " & vbNewLine _
                                     & " AND                                                      " & vbNewLine _
                                     & " F_UNSO_L.CUST_CD_L = @CUST_CD_L                         " & vbNewLine _
                                     & " AND                                                      " & vbNewLine _
                                     & " F_UNSO_L.CUST_CD_M   = @CUST_CD_M                       " & vbNewLine

#End Region

#End If
    '2014.04.09 まとめも対象にオーダー番号チェック --ST--
#Region "SQL_SELECT_C_OUTKA_L_INSUM"
    Private Const SQL_SELECT_C_OUTKA_L_INSUM As String = "SELECT                                              " & vbNewLine _
                                                       & "     COUNT(*)             AS OUTKA_L_CNT            " & vbNewLine _
                                                       & "FROM LM_TRN..H_OUTKAEDI_L AS HEL                    " & vbNewLine _
                                                       & "LEFT JOIN                                           " & vbNewLine _
                                                       & "     LM_TRN..C_OUTKA_L    AS COL                    " & vbNewLine _
                                                       & "  ON HEL.NRS_BR_CD         = COL.NRS_BR_CD          " & vbNewLine _
                                                       & " AND HEL.OUTKA_CTL_NO      = COL.OUTKA_NO_L         " & vbNewLine _
                                                       & " AND COL.SYS_DEL_FLG       = '0'                    " & vbNewLine _
                                                       & "WHERE                                               " & vbNewLine _
                                                       & "     HEL.SYS_DEL_FLG       = '0'                    " & vbNewLine _
                                                       & " AND HEL.OUT_FLAG          = '1'                    " & vbNewLine _
                                                       & " AND COL.SYS_DEL_FLG       = '0'                    " & vbNewLine _
                                                       & " AND HEL.NRS_BR_CD         = @NRS_BR_CD       --@   " & vbNewLine _
                                                       & " AND HEL.CUST_ORD_NO       = @CUST_ORD_NO     --@   " & vbNewLine _
                                                       & " AND HEL.OUTKA_PLAN_DATE   = @OUTKA_PLAN_DATE --@   " & vbNewLine _
                                                       & " AND COL.OUTKA_PLAN_DATE  <= @OUTKA_PLAN_DATE --@   " & vbNewLine _
                                                       & " AND HEL.CUST_CD_L         = @CUST_CD_L       --@   " & vbNewLine _
                                                       & " AND HEL.CUST_CD_M         = @CUST_CD_M       --@   " & vbNewLine

#End Region
    '2014.04.09 まとめも対象にオーダー番号チェック --ED--

#Region "検索処理 カウント用SQL"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(H_OUTKAEDI_L.EDI_CTL_NO)		       AS SELECT_CNT            " & vbNewLine

#End Region

#Region "検索処理 抽出用SQL"

    '2012.02.25 大阪対応 START
#Region "H_OUTKAEDI_L SELECT句"

    '要望番号:1786 terakara 2013.01.23 PRTFLG追加 Start
    ''' <summary>
    ''' H_OUTKAEDI_Lデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                      " & vbNewLine _
                                                & " Z4.KBN_NM1                                          AS JYOTAI	           " & vbNewLine _
                                                & ",Z5.KBN_NM1                                          AS HORYU	           " & vbNewLine _
                                                & ",CASE WHEN H_OUTKAEDI_L.CUST_ORD_NO IS NOT NULL                              " & vbNewLine _
                                                & " THEN H_OUTKAEDI_L.CUST_ORD_NO                                               " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_M_FST.CUST_ORD_NO_DTL                                       " & vbNewLine _
                                                & " END                                                 AS CUST_ORD_NO          " & vbNewLine _
                                                & ",Z1.KBN_NM1                                          AS OUTKA_STATE_KB_NM    " & vbNewLine _
                                                & ",CASE WHEN C_OUTKA_L.OUTKO_DATE IS NOT NULL                                  " & vbNewLine _
                                                & " THEN C_OUTKA_L.OUTKO_DATE                                                   " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_L.OUTKO_DATE                                                " & vbNewLine _
                                                & " END                                                 AS OUTKO_DATE           " & vbNewLine _
                                                & ",CASE WHEN C_OUTKA_L.OUTKA_PLAN_DATE IS NOT NULL                             " & vbNewLine _
                                                & " THEN C_OUTKA_L.OUTKA_PLAN_DATE                                              " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_L.OUTKA_PLAN_DATE                                           " & vbNewLine _
                                                & " END                                                 AS OUTKA_PLAN_DATE      " & vbNewLine _
                                                & ",CASE WHEN C_OUTKA_L.ARR_PLAN_DATE IS NOT NULL                               " & vbNewLine _
                                                & " THEN C_OUTKA_L.ARR_PLAN_DATE                                                " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_L.ARR_PLAN_DATE                                             " & vbNewLine _
                                                & " END                                                 AS ARR_PLAN_DATE        " & vbNewLine _
                                                & ",CASE WHEN H_OUTKAEDI_L.CUST_NM_L + '　' + H_OUTKAEDI_L.CUST_NM_M IS NOT NULL       " & vbNewLine _
                                                & " THEN H_OUTKAEDI_L.CUST_NM_L + '　' + H_OUTKAEDI_L.CUST_NM_M                        " & vbNewLine _
                                                & " ELSE M_CUST.CUST_NM_L + '　' + M_CUST.CUST_NM_M                                    " & vbNewLine _
                                                & " END                                                 AS CUST_NM              " & vbNewLine _
                                                & "--2012/10/12 KIM 要望番号:1512 START                                 	      " & vbNewLine _
                                                & " --,CASE WHEN H_OUTKAEDI_L.DEST_NM IS NOT NULL                                 " & vbNewLine _
                                                & " --THEN H_OUTKAEDI_L.DEST_NM                                                   " & vbNewLine _
                                                & " --ELSE M_DEST.DEST_NM                                                         " & vbNewLine _
                                                & " --END                                                 AS DEST_NM              " & vbNewLine _
                                                & ",ISNULL(H_OUTKAEDI_L.DEST_NM, '')                    AS DEST_NM              " & vbNewLine _
                                                & "--2012/10/12 KIM 要望番号:1512 END                                   	    " & vbNewLine _
                                                & ",CASE WHEN C_OUTKA_L.REMARK IS NOT NULL                                      " & vbNewLine _
                                                & " THEN C_OUTKA_L.REMARK                                                       " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_L.REMARK                                                    " & vbNewLine _
                                                & " END                                                 AS REMARK               " & vbNewLine _
                                                & ",CASE WHEN F_UNSO_L.REMARK IS NOT NULL                                       " & vbNewLine _
                                                & " THEN F_UNSO_L.REMARK                                                        " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_L.UNSO_ATT                                                  " & vbNewLine _
                                                & " END                                                 AS UNSO_ATT             " & vbNewLine _
                                                & ",H_OUTKAEDI_M_FST.GOODS_NM                           AS GOODS_NM	            " & vbNewLine _
                                                & ",H_OUTKAEDI_M_SUM.OUTKA_TTL_NB                           AS OUTKA_TTL_NB                         " & vbNewLine _
                                                & "--2012/10/12 KIM 要望番号:1512 START                                 	                        " & vbNewLine _
                                                & "--,CASE WHEN M_DEST.AD_1 + '　' + M_DEST.AD_2 + '　' + M_DEST.AD_3 IS NOT NULL                   " & vbNewLine _
                                                & "-- THEN M_DEST.AD_1 + '　' + M_DEST.AD_2 + '　' + M_DEST.AD_3                                    " & vbNewLine _
                                                & "-- ELSE H_OUTKAEDI_L.DEST_AD_1 + '　' + H_OUTKAEDI_L.DEST_AD_2 + '　' + C_OUTKA_L.DEST_AD_3      " & vbNewLine _
                                                & "-- END                                                 AS DEST_AD                                " & vbNewLine _
                                                & "--2012/10/22 HONMYO 要望番号:1529(EDI出荷検索画面、届け先住所が表示されない) START                                 	                        " & vbNewLine _
                                                & "--,H_OUTKAEDI_L.DEST_AD_1 + '　' + H_OUTKAEDI_L.DEST_AD_2 + '　' + C_OUTKA_L.DEST_AD_3  AS DEST_AD " & vbNewLine _
                                                & ",H_OUTKAEDI_L.DEST_AD_1 + '　' + H_OUTKAEDI_L.DEST_AD_2 + '　' + H_OUTKAEDI_L.DEST_AD_3  AS DEST_AD " & vbNewLine _
                                                & "--2012/10/22 HONMYO 要望番号:1529(EDI出荷検索画面、届け先住所が表示されない) END                                 	                        " & vbNewLine _
                                                & "--2012/10/12 KIM 要望番号:1512 END                                 	      " & vbNewLine _
                                                & ",M_UNSOCO.UNSOCO_NM                                  AS UNSO_NM	            " & vbNewLine _
                                                & ",Z2.KBN_NM1                                          AS BIN_KB_NM            " & vbNewLine _
                                                & ",CASE WHEN H_OUTKAEDI_L.SYS_DEL_FLG = '1'                                    " & vbNewLine _
                                                & " THEN ISNULL(MCNT.M_COUNT,0)                                	                " & vbNewLine _
                                                & " ELSE ISNULL(MCNTSYS.M_COUNT,0)                                 	            " & vbNewLine _
                                                & " END                                                 AS M_COUNT	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.EDI_CTL_NO                             AS EDI_CTL_NO	        " & vbNewLine _
                                                & "--2012.03.25 大阪対応START                                      	        " & vbNewLine _
                                                & ",CASE WHEN SUBSTRING(H_OUTKAEDI_L.FREE_C30,1,2) = '04' AND SUBSTRING(H_OUTKAEDI_L.FREE_C30,5,8) <> '00000000' " & vbNewLine _
                                                & "--2012.03.25 大阪対応END                                      	        " & vbNewLine _
                                                & " THEN SUBSTRING(H_OUTKAEDI_L.FREE_C30,4,9)                                   " & vbNewLine _
                                                & " ELSE ''                                                                     " & vbNewLine _
                                                & " END                                                 AS MATOME_NO            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.OUTKA_CTL_NO                           AS OUTKA_CTL_NO         " & vbNewLine _
                                                & ",H_OUTKAEDI_L.BUYER_ORD_NO                           AS BUYER_ORD_NO	        " & vbNewLine _
                                                & ",Z3.KBN_NM1                                          AS SYUBETU_KB_NM        " & vbNewLine _
                                                & ",CASE WHEN C_OUTKA_L.OUTKA_PKG_NB IS NOT NULL                                " & vbNewLine _
                                                & " THEN C_OUTKA_L.OUTKA_PKG_NB                                                 " & vbNewLine _
                                                & " ELSE '0'                                                                    " & vbNewLine _
                                                & " END                                                 AS OUTKA_PKG_NB         " & vbNewLine _
                                                & ",Z6.KBN_NM1                                          AS UNSO_MOTO_KB_NM      " & vbNewLine _
                                                & ",H_OUTKAEDI_L.CRT_DATE                               AS CRT_DATE	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.CRT_TIME                               AS CRT_TIME	            " & vbNewLine _
                                                & ",SENDTBL.SEND_DATE                                   AS SEND_DATE	        " & vbNewLine _
                                                & ",SENDTBL.SEND_TIME                                   AS SEND_TIME	        " & vbNewLine _
                                                & ",M_NRS_BR.NRS_BR_NM                                  AS NRS_BR_NM	        " & vbNewLine _
                                                & ",M_SOKO.WH_NM                                        AS WH_NM	            " & vbNewLine _
                                                & ",TANTO_USER.USER_NM                                  AS TANTO_USER	        " & vbNewLine _
                                                & ",ENT_USER.USER_NM                                    AS SYS_ENT_USER         " & vbNewLine _
                                                & ",UPD_USER.USER_NM                                    AS SYS_UPD_USER         " & vbNewLine _
                                                & ",H_OUTKAEDI_L.SYS_UPD_DATE                           AS SYS_UPD_DATE	        " & vbNewLine _
                                                & ",H_OUTKAEDI_L.SYS_UPD_TIME                           AS SYS_UPD_TIME	        " & vbNewLine _
                                                & ",H_OUTKAEDI_L.NRS_BR_CD                              AS NRS_BR_CD	        " & vbNewLine _
                                                & ",H_OUTKAEDI_L.WH_CD                                  AS WH_CD	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.CUST_CD_L                              AS CUST_CD_L            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.CUST_CD_M                              AS CUST_CD_M            " & vbNewLine _
                                                & ",H_OUTKAEDI_M_MIN.NRS_GOODS_CD                       AS GOODS_CD_NRS         " & vbNewLine _
                                                & ",H_OUTKAEDI_L.DEST_CD                                AS DEST_CD              " & vbNewLine _
                                                & ",C_OUTKA_L.OUTKA_STATE_KB                            AS OUTKA_STATE_KB       " & vbNewLine _
                                                & ",F_UNSO_L.UNSO_CD                                    AS UNSO_CD              " & vbNewLine _
                                                & ",F_UNSO_L.UNSO_BR_CD                                 AS UNSO_BR_CD           " & vbNewLine _
                                                & ",F_UNSO_L.UNSO_NO_L                                  AS UNSO_NO_L            " & vbNewLine _
                                                & ",F_UNSO_L.TRIP_NO                                    AS TRIP_NO              " & vbNewLine _
                                                & ",F_UNSO_L.SYS_UPD_DATE                               AS UNSO_SYS_UPD_DATE    " & vbNewLine _
                                                & ",F_UNSO_L.SYS_UPD_TIME                               AS UNSO_SYS_UPD_TIME    " & vbNewLine _
                                                & ",H_OUTKAEDI_M_SUM.MIN_NB                             AS MIN_NB               " & vbNewLine _
                                                & ",H_OUTKAEDI_L.DEL_KB                                 AS EDI_DEL_KB           " & vbNewLine _
                                                & ",C_OUTKA_L.SYS_DEL_FLG                               AS OUTKA_DEL_KB         " & vbNewLine _
                                                & ",F_UNSO_L.SYS_DEL_FLG                                AS UNSO_DEL_KB          " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C01                               AS FREE_C01	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C02                               AS FREE_C02	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C03                               AS FREE_C03	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C04                               AS FREE_C04	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C30                               AS FREE_C30	            " & vbNewLine _
                                                & ",H_OUTKAEDI_M_SUM.AKAKURO_FLG                        AS AKAKURO_FLG	        " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_01                                  AS EDI_CUST_JISSEKI     " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_07                                  AS EDI_CUST_MATOMEF     " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_08                                  AS EDI_CUST_DELDISP     " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_12                                  AS EDI_CUST_SPECIAL     " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_13                                  AS EDI_CUST_HOLDOUT     " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_14                                  AS EDI_CUST_UNSOFLG     " & vbNewLine _
                                                & ",M_EDI_CUST.EDI_CUST_INDEX                           AS EDI_CUST_INDEX       " & vbNewLine _
                                                & ",M_EDI_CUST.RCV_NM_HED                               AS RCV_NM_HED           " & vbNewLine _
                                                & ",M_EDI_CUST.RCV_NM_DTL                               AS RCV_NM_DTL           " & vbNewLine _
                                                & ",M_EDI_CUST.RCV_NM_EXT                               AS RCV_NM_EXT           " & vbNewLine _
                                                & ",M_EDI_CUST.SND_NM                                   AS SND_NM               " & vbNewLine _
                                                & ",SENDTBL.SYS_UPD_DATE                                AS SND_SYS_UPD_DATE     " & vbNewLine _
                                                & ",SENDTBL.SYS_UPD_TIME                                AS SND_SYS_UPD_TIME     " & vbNewLine _
                                                & ",RCV_HED.SYS_UPD_DATE                                AS RCV_SYS_UPD_DATE     " & vbNewLine _
                                                & ",RCV_HED.SYS_UPD_TIME                                AS RCV_SYS_UPD_TIME     " & vbNewLine _
                                                & ",C_OUTKA_L.SYS_UPD_DATE                              AS OUTKA_SYS_UPD_DATE   " & vbNewLine _
                                                & ",C_OUTKA_L.SYS_UPD_TIME                              AS OUTKA_SYS_UPD_TIME   " & vbNewLine _
                                                & ",H_OUTKAEDI_L.JISSEKI_FLAG                           AS JISSEKI_FLAG	        " & vbNewLine _
                                                & ",H_OUTKAEDI_L.OUT_FLAG                               AS OUT_FLAG	            " & vbNewLine _
                                                & ",M_EDI_CUST.AUTO_MATOME_FLG                          AS AUTO_MATOME_FLG	    " & vbNewLine _
                                                & ",H_OUTKAEDI_L.SYS_DEL_FLG                            AS SYS_DEL_FLG	        " & vbNewLine _
                                                & ",M_EDI_CUST.ORDER_CHECK_FLG                          AS ORDER_CHECK_FLG      " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_16                                  AS EDI_CUST_INOUTFLG    " & vbNewLine _
                                                & ",RCV_HED.PRTFLG                                      AS PRTFLG               " & vbNewLine _
                                                & "--(2014.01.20)要望番号2145-⑦ 追加START                                      " & vbNewLine _
                                                & ",CASE WHEN H_OUTKAEDI_L.SYS_DEL_FLG = '1'                                    " & vbNewLine _
                                                & " THEN 0                                	                                    " & vbNewLine _
                                                & " ELSE ISNULL(MCNTDEL.M_COUNT,0)                                 	            " & vbNewLine _
                                                & " END                                                 AS DELCNT               " & vbNewLine _
                                                & "--(2014.01.20)要望番号2145-⑦ 追加END                                        " & vbNewLine _
                                                & "--(2014.03.31)セミ標準対応[FLAG_17追加]] --ST--                              " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_17                                  AS FLAG_17              " & vbNewLine _
                                                & "--(2014.03.31)セミ標準対応[FLAG_17追加]] --ED--                              " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C05                               AS FREE_C05             " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C07                               AS FREE_C07             " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C08                               AS FREE_C08             " & vbNewLine _
                                                & ",ISNULL(H_OUTKAEDI_M_MIN.FREE_C13,'')                AS HAISO_SIJI_NO   --DIC古河用" & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_19                                  AS FLAG_19              " & vbNewLine _
                                                & ",Z7.KBN_NM1                                          AS PICK_KB_NM           " & vbNewLine

    'DEL 2017/06/02                                              & ",''                                                  AS UNSONCG_DEL_KB       " & vbNewLine

    '要望番号:1786 terakara 2013.01.23 PRTFLG追加 End

    '要望番号1281:(春日部：アベンド) 2012/07/18 本明 Start
    '要望番号:1786 terakara 2013.01.23 PRTFLG追加 Start
    Private Const SQL_SELECT_DATA_FOR_DICKKB As String = " SELECT                                                                      " & vbNewLine _
                                                & " Z4.KBN_NM1                                          AS JYOTAI	           " & vbNewLine _
                                                & ",Z5.KBN_NM1                                          AS HORYU	           " & vbNewLine _
                                                & ",CASE WHEN H_OUTKAEDI_L.CUST_ORD_NO IS NOT NULL                              " & vbNewLine _
                                                & " THEN H_OUTKAEDI_L.CUST_ORD_NO                                               " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_M_FST.CUST_ORD_NO_DTL                                       " & vbNewLine _
                                                & " END                                                 AS CUST_ORD_NO          " & vbNewLine _
                                                & ",Z1.KBN_NM1                                          AS OUTKA_STATE_KB_NM    " & vbNewLine _
                                                & ",CASE WHEN C_OUTKA_L.OUTKO_DATE IS NOT NULL                                  " & vbNewLine _
                                                & " THEN C_OUTKA_L.OUTKO_DATE                                                   " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_L.OUTKO_DATE                                                " & vbNewLine _
                                                & " END                                                 AS OUTKO_DATE           " & vbNewLine _
                                                & ",CASE WHEN C_OUTKA_L.OUTKA_PLAN_DATE IS NOT NULL                             " & vbNewLine _
                                                & " THEN C_OUTKA_L.OUTKA_PLAN_DATE                                              " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_L.OUTKA_PLAN_DATE                                           " & vbNewLine _
                                                & " END                                                 AS OUTKA_PLAN_DATE      " & vbNewLine _
                                                & ",CASE WHEN C_OUTKA_L.ARR_PLAN_DATE IS NOT NULL                               " & vbNewLine _
                                                & " THEN C_OUTKA_L.ARR_PLAN_DATE                                                " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_L.ARR_PLAN_DATE                                             " & vbNewLine _
                                                & " END                                                 AS ARR_PLAN_DATE        " & vbNewLine _
                                                & ",CASE WHEN H_OUTKAEDI_L.CUST_NM_L + '　' + H_OUTKAEDI_L.CUST_NM_M IS NOT NULL       " & vbNewLine _
                                                & " THEN H_OUTKAEDI_L.CUST_NM_L + '　' + H_OUTKAEDI_L.CUST_NM_M                        " & vbNewLine _
                                                & " ELSE M_CUST.CUST_NM_L + '　' + M_CUST.CUST_NM_M                                    " & vbNewLine _
                                                & " END                                                 AS CUST_NM              " & vbNewLine _
                                                & "--2012/10/12 KIM 要望番号:1512 START                                 	    " & vbNewLine _
                                                & "--,CASE WHEN H_OUTKAEDI_L.DEST_NM IS NOT NULL                                " & vbNewLine _
                                                & "-- THEN H_OUTKAEDI_L.DEST_NM                                                 " & vbNewLine _
                                                & "-- ELSE M_DEST.DEST_NM                                                       " & vbNewLine _
                                                & "-- END                                                 AS DEST_NM            " & vbNewLine _
                                                & ",ISNULL(H_OUTKAEDI_L.DEST_NM,'')                     AS DEST_NM              " & vbNewLine _
                                                & "--2012/10/12 KIM 要望番号:1512 END                                  	        " & vbNewLine _
                                                & ",CASE WHEN C_OUTKA_L.REMARK IS NOT NULL                                      " & vbNewLine _
                                                & " THEN C_OUTKA_L.REMARK                                                       " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_L.REMARK                                                    " & vbNewLine _
                                                & " END                                                 AS REMARK               " & vbNewLine _
                                                & ",CASE WHEN F_UNSO_L.REMARK IS NOT NULL                                       " & vbNewLine _
                                                & " THEN F_UNSO_L.REMARK                                                        " & vbNewLine _
                                                & " ELSE H_OUTKAEDI_L.UNSO_ATT                                                  " & vbNewLine _
                                                & " END                                                 AS UNSO_ATT             " & vbNewLine _
                                                & ",H_OUTKAEDI_M_FST.GOODS_NM                           AS GOODS_NM	            " & vbNewLine _
                                                & ",H_OUTKAEDI_M_SUM.OUTKA_TTL_NB                           AS OUTKA_TTL_NB         " & vbNewLine _
                                                & "--2012/10/12 KIM 要望番号:1512 START                                 	    " & vbNewLine _
                                                & "--,CASE WHEN M_DEST.AD_1 + '　' + M_DEST.AD_2 + '　' + M_DEST.AD_3 IS NOT NULL                   " & vbNewLine _
                                                & "-- THEN M_DEST.AD_1 + '　' + M_DEST.AD_2 + '　' + M_DEST.AD_3                                    " & vbNewLine _
                                                & "-- ELSE H_OUTKAEDI_L.DEST_AD_1 + '　' + H_OUTKAEDI_L.DEST_AD_2 + '　' + C_OUTKA_L.DEST_AD_3      " & vbNewLine _
                                                & "-- END                                                 AS DEST_AD                                " & vbNewLine _
                                                & "--2012/10/22 HONMYO 要望番号:1529(EDI出荷検索画面、届け先住所が表示されない) START                                 	                        " & vbNewLine _
                                                & "--,H_OUTKAEDI_L.DEST_AD_1 + '　' + H_OUTKAEDI_L.DEST_AD_2 + '　' + C_OUTKA_L.DEST_AD_3  AS DEST_AD " & vbNewLine _
                                                & ",H_OUTKAEDI_L.DEST_AD_1 + '　' + H_OUTKAEDI_L.DEST_AD_2 + '　' + H_OUTKAEDI_L.DEST_AD_3  AS DEST_AD " & vbNewLine _
                                                & "--2012/10/22 HONMYO 要望番号:1529(EDI出荷検索画面、届け先住所が表示されない) END                                 	                        " & vbNewLine _
                                                & "--2012/10/12 KIM 要望番号:1512 END                                  	        " & vbNewLine _
                                                & ",M_UNSOCO.UNSOCO_NM                                  AS UNSO_NM	            " & vbNewLine _
                                                & ",Z2.KBN_NM1                                          AS BIN_KB_NM            " & vbNewLine _
                                                & ",CASE WHEN H_OUTKAEDI_L.SYS_DEL_FLG = '1'                                    " & vbNewLine _
                                                & " THEN ISNULL(MCNT.M_COUNT,0)                                	                " & vbNewLine _
                                                & " ELSE ISNULL(MCNTSYS.M_COUNT,0)                                 	            " & vbNewLine _
                                                & " END                                                 AS M_COUNT	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.EDI_CTL_NO                             AS EDI_CTL_NO	        " & vbNewLine _
                                                & "--2012.03.25 大阪対応START                                      	        " & vbNewLine _
                                                & ",CASE WHEN SUBSTRING(H_OUTKAEDI_L.FREE_C30,1,2) = '04' AND SUBSTRING(H_OUTKAEDI_L.FREE_C30,5,8) <> '00000000' " & vbNewLine _
                                                & "--2012.03.25 大阪対応END                                      	        " & vbNewLine _
                                                & " THEN SUBSTRING(H_OUTKAEDI_L.FREE_C30,4,9)                                   " & vbNewLine _
                                                & " ELSE ''                                                                     " & vbNewLine _
                                                & " END                                                 AS MATOME_NO            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.OUTKA_CTL_NO                           AS OUTKA_CTL_NO         " & vbNewLine _
                                                & ",H_OUTKAEDI_L.BUYER_ORD_NO                           AS BUYER_ORD_NO	        " & vbNewLine _
                                                & ",Z3.KBN_NM1                                          AS SYUBETU_KB_NM        " & vbNewLine _
                                                & ",CASE WHEN C_OUTKA_L.OUTKA_PKG_NB IS NOT NULL                                " & vbNewLine _
                                                & " THEN C_OUTKA_L.OUTKA_PKG_NB                                                 " & vbNewLine _
                                                & " ELSE '0'                                                                    " & vbNewLine _
                                                & " END                                                 AS OUTKA_PKG_NB         " & vbNewLine _
                                                & ",Z6.KBN_NM1                                          AS UNSO_MOTO_KB_NM      " & vbNewLine _
                                                & ",H_OUTKAEDI_L.CRT_DATE                               AS CRT_DATE	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.CRT_TIME                               AS CRT_TIME	            " & vbNewLine _
                                                & ",''                              AS SEND_DATE	        " & vbNewLine _
                                                & ",''                              AS SEND_TIME	        " & vbNewLine _
                                                & ",M_NRS_BR.NRS_BR_NM                                  AS NRS_BR_NM	        " & vbNewLine _
                                                & ",M_SOKO.WH_NM                                        AS WH_NM	            " & vbNewLine _
                                                & ",TANTO_USER.USER_NM                                  AS TANTO_USER	        " & vbNewLine _
                                                & ",ENT_USER.USER_NM                                    AS SYS_ENT_USER         " & vbNewLine _
                                                & ",UPD_USER.USER_NM                                    AS SYS_UPD_USER         " & vbNewLine _
                                                & ",H_OUTKAEDI_L.SYS_UPD_DATE                           AS SYS_UPD_DATE	        " & vbNewLine _
                                                & ",H_OUTKAEDI_L.SYS_UPD_TIME                           AS SYS_UPD_TIME	        " & vbNewLine _
                                                & ",H_OUTKAEDI_L.NRS_BR_CD                              AS NRS_BR_CD	        " & vbNewLine _
                                                & ",H_OUTKAEDI_L.WH_CD                                  AS WH_CD	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.CUST_CD_L                              AS CUST_CD_L            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.CUST_CD_M                              AS CUST_CD_M            " & vbNewLine _
                                                & ",H_OUTKAEDI_M_MIN.NRS_GOODS_CD                       AS GOODS_CD_NRS         " & vbNewLine _
                                                & ",H_OUTKAEDI_L.DEST_CD                                AS DEST_CD              " & vbNewLine _
                                                & ",C_OUTKA_L.OUTKA_STATE_KB                            AS OUTKA_STATE_KB       " & vbNewLine _
                                                & ",F_UNSO_L.UNSO_CD                                    AS UNSO_CD              " & vbNewLine _
                                                & ",F_UNSO_L.UNSO_BR_CD                                 AS UNSO_BR_CD           " & vbNewLine _
                                                & ",F_UNSO_L.UNSO_NO_L                                  AS UNSO_NO_L            " & vbNewLine _
                                                & ",F_UNSO_L.TRIP_NO                                    AS TRIP_NO              " & vbNewLine _
                                                & ",F_UNSO_L.SYS_UPD_DATE                               AS UNSO_SYS_UPD_DATE    " & vbNewLine _
                                                & ",F_UNSO_L.SYS_UPD_TIME                               AS UNSO_SYS_UPD_TIME    " & vbNewLine _
                                                & ",H_OUTKAEDI_M_SUM.MIN_NB                             AS MIN_NB               " & vbNewLine _
                                                & ",H_OUTKAEDI_L.DEL_KB                                 AS EDI_DEL_KB           " & vbNewLine _
                                                & ",C_OUTKA_L.SYS_DEL_FLG                               AS OUTKA_DEL_KB         " & vbNewLine _
                                                & ",F_UNSO_L.SYS_DEL_FLG                                AS UNSO_DEL_KB          " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C01                               AS FREE_C01	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C02                               AS FREE_C02	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C03                               AS FREE_C03	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C04                               AS FREE_C04	            " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C30                               AS FREE_C30	            " & vbNewLine _
                                                & ",H_OUTKAEDI_M_SUM.AKAKURO_FLG                        AS AKAKURO_FLG	        " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_01                                  AS EDI_CUST_JISSEKI     " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_07                                  AS EDI_CUST_MATOMEF     " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_08                                  AS EDI_CUST_DELDISP     " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_12                                  AS EDI_CUST_SPECIAL     " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_13                                  AS EDI_CUST_HOLDOUT     " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_14                                  AS EDI_CUST_UNSOFLG     " & vbNewLine _
                                                & ",M_EDI_CUST.EDI_CUST_INDEX                           AS EDI_CUST_INDEX       " & vbNewLine _
                                                & ",M_EDI_CUST.RCV_NM_HED                               AS RCV_NM_HED           " & vbNewLine _
                                                & ",M_EDI_CUST.RCV_NM_DTL                               AS RCV_NM_DTL           " & vbNewLine _
                                                & ",M_EDI_CUST.RCV_NM_EXT                               AS RCV_NM_EXT           " & vbNewLine _
                                                & ",M_EDI_CUST.SND_NM                                   AS SND_NM               " & vbNewLine _
                                                & ",''                                                  AS SND_SYS_UPD_DATE     " & vbNewLine _
                                                & ",''                                                  AS SND_SYS_UPD_TIME     " & vbNewLine _
                                                & ",''                                                  AS RCV_SYS_UPD_DATE     " & vbNewLine _
                                                & ",''                                                  AS RCV_SYS_UPD_TIME     " & vbNewLine _
                                                & ",C_OUTKA_L.SYS_UPD_DATE                              AS OUTKA_SYS_UPD_DATE   " & vbNewLine _
                                                & ",C_OUTKA_L.SYS_UPD_TIME                              AS OUTKA_SYS_UPD_TIME   " & vbNewLine _
                                                & ",H_OUTKAEDI_L.JISSEKI_FLAG                           AS JISSEKI_FLAG	        " & vbNewLine _
                                                & ",H_OUTKAEDI_L.OUT_FLAG                               AS OUT_FLAG	            " & vbNewLine _
                                                & ",M_EDI_CUST.AUTO_MATOME_FLG                          AS AUTO_MATOME_FLG	    " & vbNewLine _
                                                & ",H_OUTKAEDI_L.SYS_DEL_FLG                            AS SYS_DEL_FLG	        " & vbNewLine _
                                                & ",M_EDI_CUST.ORDER_CHECK_FLG                          AS ORDER_CHECK_FLG      " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_16                                  AS EDI_CUST_INOUTFLG    " & vbNewLine _
                                                & ",''                                                  AS PRTFLG               " & vbNewLine _
                                                & "--(2014.01.20)要望番号2145-⑦ 追加START                                      " & vbNewLine _
                                                & ",CASE WHEN H_OUTKAEDI_L.SYS_DEL_FLG = '1'                                    " & vbNewLine _
                                                & " THEN 0                                	                                    " & vbNewLine _
                                                & " ELSE ISNULL(MCNTDEL.M_COUNT,0)                                 	            " & vbNewLine _
                                                & " END                                                 AS DELCNT               " & vbNewLine _
                                                & "--(2014.01.20)要望番号2145-⑦ 追加END                                        " & vbNewLine _
                                                & "--(2014.03.31)セミ標準対応[FLAG_17追加]] --ST--                              " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_17                                  AS FLAG_17              " & vbNewLine _
                                                & "--(2014.03.31)セミ標準対応[FLAG_17追加]] --ED--                              " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C05                               AS FREE_C05             " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C07                               AS FREE_C07             " & vbNewLine _
                                                & ",H_OUTKAEDI_L.FREE_C08                               AS FREE_C08             " & vbNewLine _
                                                & ",ISNULL(H_OUTKAEDI_M_MIN.FREE_C13,'')                AS HAISO_SIJI_NO        " & vbNewLine _
                                                & ",M_EDI_CUST.FLAG_19                                  AS FLAG_19              " & vbNewLine _
                                                & ",Z7.KBN_NM1                                          AS PICK_KB_NM           " & vbNewLine

    'DEL 2017/06/02                                               & ",''                                                  AS UNSONCG_DEL_KB       " & vbNewLine

    '要望番号:1786 terakara 2013.01.23 PRTFLG追加 End
    '要望番号1281:(春日部：アベンド) 2012/07/18 本明 End

    '2017/12/27 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add start
    Private Const SQL_SELECT_DATA_FOR_EXCEL_TORIKOMI_HANDAN As String = "SELECT COUNT(0) AS DATA_COUNT                          " & vbNewLine _
                                                                      & "FROM $LM_TRN$..H_OUTKAEDI_L                            " & vbNewLine _
                                                                      & "WHERE NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                                                      & "AND EDI_CTL_NO = @EDI_CTL_NO                           " & vbNewLine _
                                                                      & "AND FREE_C01 = 'EXCEL'                                 " & vbNewLine
    '2017/12/27 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add end

    '2013.30.04 / Notes1909受信ファイル名追加開始
    Private Const SQL_SELECT_FILE_NAME As String = ",RCV_HED_FILER.FILE_NAME                            AS FILE_NAME             "
    Private Const SQL_SELECT_FILE_NAME_NULL As String = ",''                                            AS FILE_NAME             "
    '2013.30.04 / Notes1909受信ファイル名追加終了
    '2013.30.04 / Notes1909受信ファイル名追加終了

    '2017/06/02 　日合対応　Start
    Private Const SQL_UNSONCG_DEL_KB As String = ",ISNULL(F_UNSO_LNCG.SYS_DEL_FLG,'')                 AS UNSONCG_DEL_KB                  "
    Private Const SQL_UNSONCG_DEL_KB_NULL As String = ",''                                            AS UNSONCG_DEL_KB             "
    '2017/06/02 　日合対応　End

    Private Const SQL_UNSOEDI_EXISTS_FLAG As String = ",CASE WHEN H_OUTKAEDI_L.CUST_CD_L = '32516' THEN                              " & vbNewLine _
                                                    & "  CASE WHEN ISNULL(UNSOEDI.EDI_CTL_NO,'') = '' THEN '1'                       " & vbNewLine _
                                                    & "  ELSE '0'                                                                    " & vbNewLine _
                                                    & "  END                                                                         " & vbNewLine _
                                                    & " ELSE '0'                                                                     " & vbNewLine _
                                                    & " END                                                 AS UNSOEDI_EXISTS_FLAG   " & vbNewLine

    Private Const SQL_UNSOEDI_EXISTS_FLAG_NULL As String = ",''                                            AS UNSOEDI_EXISTS_FLAG             "

    'Add Start 2018/10/31 要望番号002808
    Private Const SQL_NCGO_OPEOUT_ONLY_FLG As String = ",CASE WHEN H_OUTKAEDI_L.CUST_CD_L = '32516' THEN                             " & vbNewLine _
                                                     & "      CASE WHEN LEFT(DTL_NCGO_NEW.BIN_KBN, 1) = '7'    THEN '1'              " & vbNewLine _
                                                     & "           WHEN DTL_NCGO_NEW.BIN_KBN = '99'    THEN '1'                      " & vbNewLine _
                                                     & "           ELSE '0'                                                          " & vbNewLine _
                                                     & "      END                                                                    " & vbNewLine _
                                                     & "      ELSE '0'                                                               " & vbNewLine _
                                                     & " END                                                 AS NCGO_OPEOUT_ONLY_FLG " & vbNewLine
    Private Const SQL_NCGO_OPEOUT_ONLY_FLG_NULL As String = ",''                                             AS NCGO_OPEOUT_ONLY_FLG " & vbNewLine
    'Add End 2018/10/31 要望番号002808
#End Region
    '2012.02.25 大阪対応 END

#Region "H_OUTKAEDI_L COUNT FROM句"

    ''' <summary>
    ''' 検索用SQLCOUNTFROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_COUNT_FROM As String = "FROM                                                              " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_L                                         " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_TRN$..C_OUTKA_L                                            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =C_OUTKA_L.NRS_BR_CD                    " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.OUTKA_CTL_NO =C_OUTKA_L.OUTKA_NO_L                " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO                                       " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CUST_ORD_NO_DTL                                  " & vbNewLine _
                                        & ",H_OUTKAEDI_M.GOODS_NM                                         " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M     H_OUTKAEDI_M                        " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO_CHU = '001'                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & ")                   H_OUTKAEDI_M_FST                           " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =H_OUTKAEDI_M_FST.NRS_BR_CD             " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO =H_OUTKAEDI_M_FST.EDI_CTL_NO           " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_DEST                       M_DEST                  " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_DEST.NRS_BR_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_DEST.CUST_CD_L                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.DEST_CD = M_DEST.DEST_CD                          " & vbNewLine _
                                        & "--LEFT JOIN                                                      " & vbNewLine _
                                        & "--$LM_TRN$..F_UNSO_L                     F_UNSO_L                " & vbNewLine _
                                        & "--ON                                                             " & vbNewLine _
                                        & "--H_OUTKAEDI_L.NRS_BR_CD = F_UNSO_L.NRS_BR_CD                    " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--H_OUTKAEDI_L.OUTKA_CTL_NO = F_UNSO_L.INOUTKA_NO_L              " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--H_OUTKAEDI_L.OUTKA_CTL_NO <> ''                                " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--(F_UNSO_L.MOTO_DATA_KB = '20'                                  " & vbNewLine _
                                        & "--OR                                                             " & vbNewLine _
                                        & "--F_UNSO_L.MOTO_DATA_KB = '40')                                  " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_UNSOCO                 M_UNSOCO                    " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD                    " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD                " & vbNewLine _
                                        & "-- 要望番号1756 yamanaka 2013.03.01 START                      " & vbNewLine _
                                        & "--LEFT JOIN                                                    " & vbNewLine _
                                        & "--(                                                            " & vbNewLine _
                                        & "--SELECT                                                       " & vbNewLine _
                                        & "--M_TCUST.CUST_CD_L AS CUST_CD_L                               " & vbNewLine _
                                        & "--,min(USER_CD) AS USER_CD                                     " & vbNewLine _
                                        & "--FROM                                                         " & vbNewLine _
                                        & "--$LM_MST$..M_TCUST              M_TCUST                       " & vbNewLine _
                                        & "--GROUP BY                                                     " & vbNewLine _
                                        & "--M_TCUST.CUST_CD_L                                            " & vbNewLine _
                                        & "--) M_TCUST                                                    " & vbNewLine _
                                        & "--ON                                                           " & vbNewLine _
                                        & "--H_OUTKAEDI_L.CUST_CD_L = M_TCUST.CUST_CD_L                   " & vbNewLine _
                                        & "--LEFT JOIN                                                    " & vbNewLine _
                                        & "--$LM_MST$..S_USER               TANTO_USER                    " & vbNewLine _
                                        & "--ON                                                           " & vbNewLine _
                                        & "--M_TCUST.USER_CD = TANTO_USER.USER_CD                         " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_CUST               M_CUST                          " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_CUST.NRS_BR_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_CUST.CUST_CD_L                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_M = M_CUST.CUST_CD_M                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_CUST.CUST_CD_S = '00'                                        " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_CUST.CUST_CD_SS = '00'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               TANTO_USER                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "M_CUST.TANTO_CD = TANTO_USER.USER_CD                           " & vbNewLine _
                                        & "-- 要望番号1756 yamanaka 2013.03.01 END                        " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               ENT_USER                        " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_ENT_USER = ENT_USER.USER_CD                   " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               UPD_USER                        " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_UPD_USER = UPD_USER.USER_CD                   " & vbNewLine _
                                        & "INNER JOIN                                                     " & vbNewLine _
                                        & "$LM_MST$..M_EDI_CUST                       M_EDI_CUST          " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_EDI_CUST.NRS_BR_CD                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.WH_CD = M_EDI_CUST.WH_CD                          " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_EDI_CUST.CUST_CD_L                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_M = M_EDI_CUST.CUST_CD_M                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_EDI_CUST.INOUT_KB = '0'                                      " & vbNewLine


    ''' <summary>
    ''' 検索用SQLCOUNTFROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_COUNT_FROM_BP As String = "FROM                                                              " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_L                                         " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_TRN$..C_OUTKA_L                                            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =C_OUTKA_L.NRS_BR_CD                    " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.OUTKA_CTL_NO =C_OUTKA_L.OUTKA_NO_L                " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO                                       " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CUST_ORD_NO_DTL                                  " & vbNewLine _
                                        & ",H_OUTKAEDI_M.GOODS_NM                                         " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M     H_OUTKAEDI_M                        " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO_CHU = '001'                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & ")                   H_OUTKAEDI_M_FST                           " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =H_OUTKAEDI_M_FST.NRS_BR_CD             " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO =H_OUTKAEDI_M_FST.EDI_CTL_NO           " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_DEST                       M_DEST                  " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_DEST.NRS_BR_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_DEST.CUST_CD_L                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.DEST_CD = M_DEST.DEST_CD                          " & vbNewLine _
                                        & "--LEFT JOIN                                                      " & vbNewLine _
                                        & "--$LM_TRN$..F_UNSO_L                     F_UNSO_L                " & vbNewLine _
                                        & "--ON                                                             " & vbNewLine _
                                        & "--H_OUTKAEDI_L.NRS_BR_CD = F_UNSO_L.NRS_BR_CD                    " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--H_OUTKAEDI_L.OUTKA_CTL_NO = F_UNSO_L.INOUTKA_NO_L              " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--H_OUTKAEDI_L.OUTKA_CTL_NO <> ''                                " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--(F_UNSO_L.MOTO_DATA_KB = '20'                                  " & vbNewLine _
                                        & "--OR                                                             " & vbNewLine _
                                        & "--F_UNSO_L.MOTO_DATA_KB = '40')                                  " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_UNSOCO                 M_UNSOCO                    " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD                    " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD                " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ",COUNT(H_OUTKAEDI_M.EDI_CTL_NO_CHU)   AS  M_COUNT              " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M   H_OUTKAEDI_M                          " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ")                           MCNTSYS                            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = MCNTSYS.EDI_CTL_NO                   " & vbNewLine _
                                        & "-- 要望番号1756 yamanaka 2013.03.01 START                      " & vbNewLine _
                                        & "--LEFT JOIN                                                    " & vbNewLine _
                                        & "--(                                                            " & vbNewLine _
                                        & "--SELECT                                                       " & vbNewLine _
                                        & "--M_TCUST.CUST_CD_L AS CUST_CD_L                               " & vbNewLine _
                                        & "--,min(USER_CD) AS USER_CD                                     " & vbNewLine _
                                        & "--FROM                                                         " & vbNewLine _
                                        & "--$LM_MST$..M_TCUST              M_TCUST                       " & vbNewLine _
                                        & "--GROUP BY                                                     " & vbNewLine _
                                        & "--M_TCUST.CUST_CD_L                                            " & vbNewLine _
                                        & "--) M_TCUST                                                    " & vbNewLine _
                                        & "--ON                                                           " & vbNewLine _
                                        & "--H_OUTKAEDI_L.CUST_CD_L = M_TCUST.CUST_CD_L                   " & vbNewLine _
                                        & "--LEFT JOIN                                                    " & vbNewLine _
                                        & "--$LM_MST$..S_USER               TANTO_USER                    " & vbNewLine _
                                        & "--ON                                                           " & vbNewLine _
                                        & "--M_TCUST.USER_CD = TANTO_USER.USER_CD                         " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_CUST               M_CUST                          " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_CUST.NRS_BR_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_CUST.CUST_CD_L                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_M = M_CUST.CUST_CD_M                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_CUST.CUST_CD_S = '00'                                        " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_CUST.CUST_CD_SS = '00'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               TANTO_USER                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "M_CUST.TANTO_CD = TANTO_USER.USER_CD                           " & vbNewLine _
                                        & "-- 要望番号1756 yamanaka 2013.03.01 END                        " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               ENT_USER                        " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_ENT_USER = ENT_USER.USER_CD                   " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               UPD_USER                        " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_UPD_USER = UPD_USER.USER_CD                   " & vbNewLine _
                                        & "INNER JOIN                                                     " & vbNewLine _
                                        & "$LM_MST$..M_EDI_CUST                       M_EDI_CUST          " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_EDI_CUST.NRS_BR_CD                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.WH_CD = M_EDI_CUST.WH_CD                          " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_EDI_CUST.CUST_CD_L                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_M = M_EDI_CUST.CUST_CD_M                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_EDI_CUST.INOUT_KB = '0'                                      " & vbNewLine

#End Region

#Region "H_OUTKAEDI_L FROM句"

    ''' <summary>
    ''' 検索用SQLFROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "FROM                                                              " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_L                                         " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_TRN$..C_OUTKA_L                                            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =C_OUTKA_L.NRS_BR_CD                    " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.OUTKA_CTL_NO =C_OUTKA_L.OUTKA_NO_L                " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                        Z1                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "C_OUTKA_L.OUTKA_STATE_KB = Z1.KBN_CD                           " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z1.KBN_GROUP_CD = 'S010'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO                                       " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CUST_ORD_NO_DTL                                  " & vbNewLine _
                                        & ",H_OUTKAEDI_M.GOODS_NM                                         " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M     H_OUTKAEDI_M                        " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO_CHU = '001'                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & ")                   H_OUTKAEDI_M_FST                           " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =H_OUTKAEDI_M_FST.NRS_BR_CD             " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO =H_OUTKAEDI_M_FST.EDI_CTL_NO           " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO                                       " & vbNewLine _
                                        & ",MIN(H_OUTKAEDI_M.EDI_CTL_NO_CHU) AS EDI_CTL_NO_CHU            " & vbNewLine _
                                        & ",SUM(H_OUTKAEDI_M.OUTKA_TTL_NB) AS OUTKA_TTL_NB                " & vbNewLine _
                                        & ",MIN(H_OUTKAEDI_M.OUTKA_TTL_NB) AS MIN_NB                      " & vbNewLine _
                                        & ",MAX(H_OUTKAEDI_M.AKAKURO_KB) AS AKAKURO_FLG                   " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M     H_OUTKAEDI_M                        " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_L                 H_OUTKAEDI_L            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD  = H_OUTKAEDI_M.NRS_BR_CD               " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = H_OUTKAEDI_M.EDI_CTL_NO              " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & "AND    (H_OUTKAEDI_L.DEL_KB = '1'                              " & vbNewLine _
                                        & "OR     (H_OUTKAEDI_L.DEL_KB <> '1'                             " & vbNewLine _
                                        & "AND    H_OUTKAEDI_M.DEL_KB <> '1'))                            " & vbNewLine _
                                        & "AND    H_OUTKAEDI_M.OUT_KB <> '1'                              " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO                                       " & vbNewLine _
                                        & ")                   H_OUTKAEDI_M_SUM                           " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =H_OUTKAEDI_M_SUM.NRS_BR_CD             " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO =H_OUTKAEDI_M_SUM.EDI_CTL_NO           " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M             H_OUTKAEDI_M_MIN            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_M_SUM.NRS_BR_CD =H_OUTKAEDI_M_MIN.NRS_BR_CD         " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M_SUM.EDI_CTL_NO =H_OUTKAEDI_M_MIN.EDI_CTL_NO       " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M_SUM.EDI_CTL_NO_CHU =H_OUTKAEDI_M_MIN.EDI_CTL_NO_CHU " & vbNewLine _
                                        & "--2012/10/12 KIM 要望番号:1512 START                             " & vbNewLine _
                                        & "--LEFT JOIN                                                      " & vbNewLine _
                                        & "--$LM_MST$..M_DEST                       M_DEST                  " & vbNewLine _
                                        & "--ON                                                             " & vbNewLine _
                                        & "--H_OUTKAEDI_L.NRS_BR_CD = M_DEST.NRS_BR_CD                      " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--H_OUTKAEDI_L.CUST_CD_L = M_DEST.CUST_CD_L                      " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--H_OUTKAEDI_L.DEST_CD = M_DEST.DEST_CD                          " & vbNewLine _
                                        & "--2012/10/12 KIM 要望番号:1512 END                             " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_CUST                       M_CUST                  " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_CUST.NRS_BR_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_CUST.CUST_CD_L                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_M = M_CUST.CUST_CD_M                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_CUST.CUST_CD_S = '00'                                        " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_CUST.CUST_CD_SS = '00'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_GOODS              M_GOODS                         " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_M_MIN.NRS_BR_CD =M_GOODS.NRS_BR_CD                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M_MIN.NRS_GOODS_CD = M_GOODS.GOODS_CD_NRS           " & vbNewLine _
                                        & "--LEFT JOIN                                                      " & vbNewLine _
                                        & "--$LM_TRN$..F_UNSO_L                     F_UNSO_L                " & vbNewLine _
                                        & "--ON                                                             " & vbNewLine _
                                        & "--H_OUTKAEDI_L.NRS_BR_CD = F_UNSO_L.NRS_BR_CD                    " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--H_OUTKAEDI_L.OUTKA_CTL_NO = F_UNSO_L.INOUTKA_NO_L              " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--H_OUTKAEDI_L.OUTKA_CTL_NO <> ''                                " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--(F_UNSO_L.MOTO_DATA_KB = '20'                                  " & vbNewLine _
                                        & "--OR                                                             " & vbNewLine _
                                        & "--F_UNSO_L.MOTO_DATA_KB = '40')                                  " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_UNSOCO                 M_UNSOCO                    " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD                    " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD                " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ",COUNT(H_OUTKAEDI_M.EDI_CTL_NO_CHU)   AS  M_COUNT              " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M   H_OUTKAEDI_M                          " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ")                           MCNT                               " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = MCNT.EDI_CTL_NO                      " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ",COUNT(H_OUTKAEDI_M.EDI_CTL_NO_CHU)   AS  M_COUNT              " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M   H_OUTKAEDI_M                          " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ")                           MCNTSYS                            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = MCNTSYS.EDI_CTL_NO                   " & vbNewLine _
                                        & "--(2014.01.20)要望番号2145-⑦ 追加START                        " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ",COUNT(H_OUTKAEDI_M.EDI_CTL_NO_CHU)   AS  M_COUNT              " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M   H_OUTKAEDI_M                          " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.SYS_DEL_FLG = '1'                                 " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ")                           MCNTDEL                            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = MCNTDEL.EDI_CTL_NO                   " & vbNewLine _
                                        & "--(2014.01.20)要望番号2145-⑦ 追加END                          " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                Z2                              " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.BIN_KB = Z2.KBN_CD                                " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z2.KBN_GROUP_CD = 'U001'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                Z3                              " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYUBETU_KB = Z3.KBN_CD                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z3.KBN_GROUP_CD = 'S020'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                Z6                              " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_TEHAI_KB = Z6.KBN_CD                         " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z6.KBN_GROUP_CD = 'T015'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                Z7                              " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.PICK_KB = Z7.KBN_CD                               " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z7.KBN_GROUP_CD = 'P001'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_SOKO               M_SOKO                          " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "M_SOKO.NRS_BR_CD = H_OUTKAEDI_L.NRS_BR_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_SOKO.WH_CD = H_OUTKAEDI_L.WH_CD                              " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_NRS_BR                                             " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_NRS_BR.NRS_BR_CD                    " & vbNewLine _
                                        & "-- 要望番号1756 yamanaka 2013.03.01 START                      " & vbNewLine _
                                        & "--LEFT JOIN                                                    " & vbNewLine _
                                        & "--(                                                            " & vbNewLine _
                                        & "--SELECT                                                       " & vbNewLine _
                                        & "--M_TCUST.CUST_CD_L AS CUST_CD_L                               " & vbNewLine _
                                        & "--,min(USER_CD) AS USER_CD                                     " & vbNewLine _
                                        & "--FROM                                                         " & vbNewLine _
                                        & "--$LM_MST$..M_TCUST              M_TCUST                       " & vbNewLine _
                                        & "--GROUP BY                                                     " & vbNewLine _
                                        & "--M_TCUST.CUST_CD_L                                            " & vbNewLine _
                                        & "--) M_TCUST                                                    " & vbNewLine _
                                        & "--ON                                                           " & vbNewLine _
                                        & "--H_OUTKAEDI_L.CUST_CD_L = M_TCUST.CUST_CD_L                   " & vbNewLine _
                                        & "--LEFT JOIN                                                    " & vbNewLine _
                                        & "--$LM_MST$..S_USER               TANTO_USER                    " & vbNewLine _
                                        & "--ON                                                           " & vbNewLine _
                                        & "--M_TCUST.USER_CD = TANTO_USER.USER_CD                         " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               TANTO_USER                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "M_CUST.TANTO_CD = TANTO_USER.USER_CD                           " & vbNewLine _
                                        & "-- 要望番号1756 yamanaka 2013.03.01 END                        " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               ENT_USER                        " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_ENT_USER = ENT_USER.USER_CD                   " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               UPD_USER                        " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_UPD_USER = UPD_USER.USER_CD                   " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                        Z4                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_DEL_FLG = Z4.KBN_CD                           " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z4.KBN_GROUP_CD = 'S051'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                        Z5                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.DEL_KB = Z5.KBN_CD                                " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z5.KBN_GROUP_CD = 'E011'                                       " & vbNewLine _
                                        & "INNER JOIN                                                     " & vbNewLine _
                                        & "$LM_MST$..M_EDI_CUST                       M_EDI_CUST          " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_EDI_CUST.NRS_BR_CD                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.WH_CD = M_EDI_CUST.WH_CD                          " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_EDI_CUST.CUST_CD_L                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_M = M_EDI_CUST.CUST_CD_M                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_EDI_CUST.INOUT_KB = '0'                                      " & vbNewLine


    '(2013.02.27) BP新システム切替対応START
    ''' <summary>
    ''' 検索用SQLFROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_BP As String = "FROM                                                              " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_L                                         " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_TRN$..C_OUTKA_L                                            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =C_OUTKA_L.NRS_BR_CD                    " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.OUTKA_CTL_NO =C_OUTKA_L.OUTKA_NO_L                " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                        Z1                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "C_OUTKA_L.OUTKA_STATE_KB = Z1.KBN_CD                           " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z1.KBN_GROUP_CD = 'S010'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO                                       " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CUST_ORD_NO_DTL                                  " & vbNewLine _
                                        & ",H_OUTKAEDI_M.GOODS_NM                                         " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M     H_OUTKAEDI_M                        " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO_CHU = '001'                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & ")                   H_OUTKAEDI_M_FST                           " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =H_OUTKAEDI_M_FST.NRS_BR_CD             " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO =H_OUTKAEDI_M_FST.EDI_CTL_NO           " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO                                       " & vbNewLine _
                                        & ",MIN(H_OUTKAEDI_M.EDI_CTL_NO_CHU) AS EDI_CTL_NO_CHU            " & vbNewLine _
                                        & ",SUM(H_OUTKAEDI_M.OUTKA_TTL_NB) AS OUTKA_TTL_NB                " & vbNewLine _
                                        & ",MIN(H_OUTKAEDI_M.OUTKA_TTL_NB) AS MIN_NB                      " & vbNewLine _
                                        & ",MAX(H_OUTKAEDI_M.AKAKURO_KB) AS AKAKURO_FLG                   " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M     H_OUTKAEDI_M                        " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_L                 H_OUTKAEDI_L            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD  = H_OUTKAEDI_M.NRS_BR_CD               " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = H_OUTKAEDI_M.EDI_CTL_NO              " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & "AND    (H_OUTKAEDI_L.DEL_KB = '1'                              " & vbNewLine _
                                        & "OR     (H_OUTKAEDI_L.DEL_KB <> '1'                             " & vbNewLine _
                                        & "AND    H_OUTKAEDI_M.DEL_KB <> '1'))                            " & vbNewLine _
                                        & "AND    H_OUTKAEDI_M.OUT_KB <> '1'                              " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO                                       " & vbNewLine _
                                        & ")                   H_OUTKAEDI_M_SUM                           " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =H_OUTKAEDI_M_SUM.NRS_BR_CD             " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO =H_OUTKAEDI_M_SUM.EDI_CTL_NO           " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M             H_OUTKAEDI_M_MIN            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_M_SUM.NRS_BR_CD =H_OUTKAEDI_M_MIN.NRS_BR_CD         " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M_SUM.EDI_CTL_NO =H_OUTKAEDI_M_MIN.EDI_CTL_NO       " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M_SUM.EDI_CTL_NO_CHU =H_OUTKAEDI_M_MIN.EDI_CTL_NO_CHU " & vbNewLine _
                                        & "--2012/10/12 KIM 要望番号:1512 START                             " & vbNewLine _
                                        & "--LEFT JOIN                                                      " & vbNewLine _
                                        & "--$LM_MST$..M_DEST                       M_DEST                  " & vbNewLine _
                                        & "--ON                                                             " & vbNewLine _
                                        & "--H_OUTKAEDI_L.NRS_BR_CD = M_DEST.NRS_BR_CD                      " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--H_OUTKAEDI_L.CUST_CD_L = M_DEST.CUST_CD_L                      " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--H_OUTKAEDI_L.DEST_CD = M_DEST.DEST_CD                          " & vbNewLine _
                                        & "--2012/10/12 KIM 要望番号:1512 END                             " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_CUST                       M_CUST                  " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_CUST.NRS_BR_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_CUST.CUST_CD_L                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_M = M_CUST.CUST_CD_M                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_CUST.CUST_CD_S = '00'                                        " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_CUST.CUST_CD_SS = '00'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_GOODS              M_GOODS                         " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_M_MIN.NRS_BR_CD =M_GOODS.NRS_BR_CD                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M_MIN.NRS_GOODS_CD = M_GOODS.GOODS_CD_NRS           " & vbNewLine _
                                        & "--LEFT JOIN                                                      " & vbNewLine _
                                        & "--$LM_TRN$..F_UNSO_L                     F_UNSO_L                " & vbNewLine _
                                        & "--ON                                                             " & vbNewLine _
                                        & "--H_OUTKAEDI_L.NRS_BR_CD = F_UNSO_L.NRS_BR_CD                    " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--H_OUTKAEDI_L.OUTKA_CTL_NO = F_UNSO_L.INOUTKA_NO_L              " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--H_OUTKAEDI_L.OUTKA_CTL_NO <> ''                                " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--(F_UNSO_L.MOTO_DATA_KB = '20'                                  " & vbNewLine _
                                        & "--OR                                                             " & vbNewLine _
                                        & "--F_UNSO_L.MOTO_DATA_KB = '40')                                  " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_UNSOCO                 M_UNSOCO                    " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD                    " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD                " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ",COUNT(H_OUTKAEDI_M.EDI_CTL_NO_CHU)   AS  M_COUNT              " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M   H_OUTKAEDI_M                          " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ")                           MCNT                               " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = MCNT.EDI_CTL_NO                      " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ",COUNT(H_OUTKAEDI_M.EDI_CTL_NO_CHU)   AS  M_COUNT              " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M   H_OUTKAEDI_M                          " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.FREE_C06 <> 'F'                                   " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ")                           MCNTSYS                            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = MCNTSYS.EDI_CTL_NO                   " & vbNewLine _
                                        & "--(2014.01.20)要望番号2145-⑦ 追加START                        " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ",COUNT(H_OUTKAEDI_M.EDI_CTL_NO_CHU)   AS  M_COUNT              " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M   H_OUTKAEDI_M                          " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.SYS_DEL_FLG = '1'                                 " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ")                           MCNTDEL                            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = MCNTDEL.EDI_CTL_NO                   " & vbNewLine _
                                        & "--(2014.01.20)要望番号2145-⑦ 追加END                          " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                Z2                              " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.BIN_KB = Z2.KBN_CD                                " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z2.KBN_GROUP_CD = 'U001'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                Z3                              " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYUBETU_KB = Z3.KBN_CD                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z3.KBN_GROUP_CD = 'S020'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                Z6                              " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_TEHAI_KB = Z6.KBN_CD                         " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z6.KBN_GROUP_CD = 'T015'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                Z7                              " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.PICK_KB = Z7.KBN_CD                               " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z7.KBN_GROUP_CD = 'P001'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_SOKO               M_SOKO                          " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "M_SOKO.NRS_BR_CD = H_OUTKAEDI_L.NRS_BR_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_SOKO.WH_CD = H_OUTKAEDI_L.WH_CD                              " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_NRS_BR                                             " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_NRS_BR.NRS_BR_CD                    " & vbNewLine _
                                        & "-- 要望番号1756 yamanaka 2013.03.01 START                      " & vbNewLine _
                                        & "--LEFT JOIN                                                    " & vbNewLine _
                                        & "--(                                                            " & vbNewLine _
                                        & "--SELECT                                                       " & vbNewLine _
                                        & "--M_TCUST.CUST_CD_L AS CUST_CD_L                               " & vbNewLine _
                                        & "--,min(USER_CD) AS USER_CD                                     " & vbNewLine _
                                        & "--FROM                                                         " & vbNewLine _
                                        & "--$LM_MST$..M_TCUST              M_TCUST                       " & vbNewLine _
                                        & "--GROUP BY                                                     " & vbNewLine _
                                        & "--M_TCUST.CUST_CD_L                                            " & vbNewLine _
                                        & "--) M_TCUST                                                    " & vbNewLine _
                                        & "--ON                                                           " & vbNewLine _
                                        & "--H_OUTKAEDI_L.CUST_CD_L = M_TCUST.CUST_CD_L                   " & vbNewLine _
                                        & "--LEFT JOIN                                                    " & vbNewLine _
                                        & "--$LM_MST$..S_USER               TANTO_USER                    " & vbNewLine _
                                        & "--ON                                                           " & vbNewLine _
                                        & "--M_TCUST.USER_CD = TANTO_USER.USER_CD                         " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               TANTO_USER                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "M_CUST.TANTO_CD = TANTO_USER.USER_CD                           " & vbNewLine _
                                        & "-- 要望番号1756 yamanaka 2013.03.01 END                        " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               ENT_USER                        " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_ENT_USER = ENT_USER.USER_CD                   " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               UPD_USER                        " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_UPD_USER = UPD_USER.USER_CD                   " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                        Z4                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_DEL_FLG = Z4.KBN_CD                           " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z4.KBN_GROUP_CD = 'S051'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                        Z5                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.DEL_KB = Z5.KBN_CD                                " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z5.KBN_GROUP_CD = 'E011'                                       " & vbNewLine _
                                        & "INNER JOIN                                                     " & vbNewLine _
                                        & "$LM_MST$..M_EDI_CUST                       M_EDI_CUST          " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_EDI_CUST.NRS_BR_CD                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.WH_CD = M_EDI_CUST.WH_CD                          " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_EDI_CUST.CUST_CD_L                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_M = M_EDI_CUST.CUST_CD_M                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_EDI_CUST.INOUT_KB = '0'                                      " & vbNewLine

    '(2013.02.27) BP新システム切替対応END

    '(2013.02.27) BP新システム切替対応START
    ''' <summary>
    ''' 検索用SQLFROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_BP_NOT_AUTO As String = "FROM                                                              " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_L                                         " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_TRN$..C_OUTKA_L                                            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =C_OUTKA_L.NRS_BR_CD                    " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.OUTKA_CTL_NO =C_OUTKA_L.OUTKA_NO_L                " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                        Z1                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "C_OUTKA_L.OUTKA_STATE_KB = Z1.KBN_CD                           " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z1.KBN_GROUP_CD = 'S010'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO                                       " & vbNewLine _
                                        & ",H_OUTKAEDI_M.CUST_ORD_NO_DTL                                  " & vbNewLine _
                                        & ",H_OUTKAEDI_M.GOODS_NM                                         " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M     H_OUTKAEDI_M                        " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO_CHU = '001'                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & ")                   H_OUTKAEDI_M_FST                           " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =H_OUTKAEDI_M_FST.NRS_BR_CD             " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO =H_OUTKAEDI_M_FST.EDI_CTL_NO           " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO                                       " & vbNewLine _
                                        & ",MIN(H_OUTKAEDI_M.EDI_CTL_NO_CHU) AS EDI_CTL_NO_CHU            " & vbNewLine _
                                        & ",SUM(H_OUTKAEDI_M.OUTKA_TTL_NB) AS OUTKA_TTL_NB                " & vbNewLine _
                                        & ",MIN(H_OUTKAEDI_M.OUTKA_TTL_NB) AS MIN_NB                      " & vbNewLine _
                                        & ",MAX(H_OUTKAEDI_M.AKAKURO_KB) AS AKAKURO_FLG                   " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M     H_OUTKAEDI_M                        " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_L                 H_OUTKAEDI_L            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD  = H_OUTKAEDI_M.NRS_BR_CD               " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = H_OUTKAEDI_M.EDI_CTL_NO              " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & "AND    (H_OUTKAEDI_L.DEL_KB = '1'                              " & vbNewLine _
                                        & "OR     (H_OUTKAEDI_L.DEL_KB <> '1'                             " & vbNewLine _
                                        & "AND    H_OUTKAEDI_M.DEL_KB <> '1'))                            " & vbNewLine _
                                        & "AND    H_OUTKAEDI_M.OUT_KB <> '1'                              " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & " H_OUTKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                        & ",H_OUTKAEDI_M.EDI_CTL_NO                                       " & vbNewLine _
                                        & ")                   H_OUTKAEDI_M_SUM                           " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD =H_OUTKAEDI_M_SUM.NRS_BR_CD             " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO =H_OUTKAEDI_M_SUM.EDI_CTL_NO           " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M             H_OUTKAEDI_M_MIN            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_M_SUM.NRS_BR_CD =H_OUTKAEDI_M_MIN.NRS_BR_CD         " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M_SUM.EDI_CTL_NO =H_OUTKAEDI_M_MIN.EDI_CTL_NO       " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M_SUM.EDI_CTL_NO_CHU =H_OUTKAEDI_M_MIN.EDI_CTL_NO_CHU " & vbNewLine _
                                        & "--2012/10/12 KIM 要望番号:1512 START                             " & vbNewLine _
                                        & "--LEFT JOIN                                                      " & vbNewLine _
                                        & "--$LM_MST$..M_DEST                       M_DEST                  " & vbNewLine _
                                        & "--ON                                                             " & vbNewLine _
                                        & "--H_OUTKAEDI_L.NRS_BR_CD = M_DEST.NRS_BR_CD                      " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--H_OUTKAEDI_L.CUST_CD_L = M_DEST.CUST_CD_L                      " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--H_OUTKAEDI_L.DEST_CD = M_DEST.DEST_CD                          " & vbNewLine _
                                        & "--2012/10/12 KIM 要望番号:1512 END                             " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_CUST                       M_CUST                  " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_CUST.NRS_BR_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_CUST.CUST_CD_L                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_M = M_CUST.CUST_CD_M                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_CUST.CUST_CD_S = '00'                                        " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_CUST.CUST_CD_SS = '00'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_GOODS              M_GOODS                         " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_M_MIN.NRS_BR_CD =M_GOODS.NRS_BR_CD                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M_MIN.NRS_GOODS_CD = M_GOODS.GOODS_CD_NRS           " & vbNewLine _
                                        & "--LEFT JOIN                                                      " & vbNewLine _
                                        & "--$LM_TRN$..F_UNSO_L                     F_UNSO_L                " & vbNewLine _
                                        & "--ON                                                             " & vbNewLine _
                                        & "--H_OUTKAEDI_L.NRS_BR_CD = F_UNSO_L.NRS_BR_CD                    " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--H_OUTKAEDI_L.OUTKA_CTL_NO = F_UNSO_L.INOUTKA_NO_L              " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--H_OUTKAEDI_L.OUTKA_CTL_NO <> ''                                " & vbNewLine _
                                        & "--AND                                                            " & vbNewLine _
                                        & "--(F_UNSO_L.MOTO_DATA_KB = '20'                                  " & vbNewLine _
                                        & "--OR                                                             " & vbNewLine _
                                        & "--F_UNSO_L.MOTO_DATA_KB = '40')                                  " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_UNSOCO                 M_UNSOCO                    " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD                    " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD                " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ",COUNT(H_OUTKAEDI_M.EDI_CTL_NO_CHU)   AS  M_COUNT              " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M   H_OUTKAEDI_M                          " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ")                           MCNT                               " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = MCNT.EDI_CTL_NO                      " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ",COUNT(H_OUTKAEDI_M.EDI_CTL_NO_CHU)   AS  M_COUNT              " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M   H_OUTKAEDI_M                          " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.FREE_C06 = 'F'                                    " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ")                           MCNTSYS                            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = MCNTSYS.EDI_CTL_NO                   " & vbNewLine _
                                        & "--(2014.01.20)要望番号2145-⑦ 追加START                        " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "(                                                              " & vbNewLine _
                                        & "SELECT                                                         " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ",COUNT(H_OUTKAEDI_M.EDI_CTL_NO_CHU)   AS  M_COUNT              " & vbNewLine _
                                        & "FROM                                                           " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_M   H_OUTKAEDI_M                          " & vbNewLine _
                                        & "WHERE                                                          " & vbNewLine _
                                        & "H_OUTKAEDI_M.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_M.SYS_DEL_FLG = '1'                                 " & vbNewLine _
                                        & "GROUP BY                                                       " & vbNewLine _
                                        & "H_OUTKAEDI_M.EDI_CTL_NO                                        " & vbNewLine _
                                        & ")                           MCNTDEL                            " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = MCNTDEL.EDI_CTL_NO                   " & vbNewLine _
                                        & "--(2014.01.20)要望番号2145-⑦ 追加END                          " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                Z2                              " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.BIN_KB = Z2.KBN_CD                                " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z2.KBN_GROUP_CD = 'U001'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                Z3                              " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYUBETU_KB = Z3.KBN_CD                            " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z3.KBN_GROUP_CD = 'S020'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                Z6                              " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.UNSO_TEHAI_KB = Z6.KBN_CD                         " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z6.KBN_GROUP_CD = 'T015'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                Z7                              " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.PICK_KB = Z7.KBN_CD                               " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z7.KBN_GROUP_CD = 'P001'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_SOKO               M_SOKO                          " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "M_SOKO.NRS_BR_CD = H_OUTKAEDI_L.NRS_BR_CD                      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_SOKO.WH_CD = H_OUTKAEDI_L.WH_CD                              " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..M_NRS_BR                                             " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_NRS_BR.NRS_BR_CD                    " & vbNewLine _
                                        & "-- 要望番号1756 yamanaka 2013.03.01 START                      " & vbNewLine _
                                        & "--LEFT JOIN                                                    " & vbNewLine _
                                        & "--(                                                            " & vbNewLine _
                                        & "--SELECT                                                       " & vbNewLine _
                                        & "--M_TCUST.CUST_CD_L AS CUST_CD_L                               " & vbNewLine _
                                        & "--,min(USER_CD) AS USER_CD                                     " & vbNewLine _
                                        & "--FROM                                                         " & vbNewLine _
                                        & "--$LM_MST$..M_TCUST              M_TCUST                       " & vbNewLine _
                                        & "--GROUP BY                                                     " & vbNewLine _
                                        & "--M_TCUST.CUST_CD_L                                            " & vbNewLine _
                                        & "--) M_TCUST                                                    " & vbNewLine _
                                        & "--ON                                                           " & vbNewLine _
                                        & "--H_OUTKAEDI_L.CUST_CD_L = M_TCUST.CUST_CD_L                   " & vbNewLine _
                                        & "--LEFT JOIN                                                    " & vbNewLine _
                                        & "--$LM_MST$..S_USER               TANTO_USER                    " & vbNewLine _
                                        & "--ON                                                           " & vbNewLine _
                                        & "--M_TCUST.USER_CD = TANTO_USER.USER_CD                         " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               TANTO_USER                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "M_CUST.TANTO_CD = TANTO_USER.USER_CD                           " & vbNewLine _
                                        & "-- 要望番号1756 yamanaka 2013.03.01 END                        " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               ENT_USER                        " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_ENT_USER = ENT_USER.USER_CD                   " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..S_USER               UPD_USER                        " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_UPD_USER = UPD_USER.USER_CD                   " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                        Z4                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.SYS_DEL_FLG = Z4.KBN_CD                           " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z4.KBN_GROUP_CD = 'S051'                                       " & vbNewLine _
                                        & "LEFT JOIN                                                      " & vbNewLine _
                                        & "$LM_MST$..Z_KBN                        Z5                      " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.DEL_KB = Z5.KBN_CD                                " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "Z5.KBN_GROUP_CD = 'E011'                                       " & vbNewLine _
                                        & "INNER JOIN                                                     " & vbNewLine _
                                        & "$LM_MST$..M_EDI_CUST                       M_EDI_CUST          " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = M_EDI_CUST.NRS_BR_CD                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.WH_CD = M_EDI_CUST.WH_CD                          " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_L = M_EDI_CUST.CUST_CD_L                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.CUST_CD_M = M_EDI_CUST.CUST_CD_M                  " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "M_EDI_CUST.INOUT_KB = '0'                                      " & vbNewLine

    '(2013.02.27) BP新システム切替対応END


#If True Then ' 日本合成化学対応(2646) 20170116 added inoue
    Private Const SQL_FROM_ADD_NIHIGO As String _
        = " LEFT JOIN                                                   " & vbNewLine _
        & "      $LM_MST$..Z_KBN AS NICHIGO_HOKAN                       " & vbNewLine _
        & "   ON NICHIGO_HOKAN.KBN_GROUP_CD   = 'N026'                  " & vbNewLine _
        & "  AND NICHIGO_HOKAN.KBN_NM3        = @BP_PRT_FLG             " & vbNewLine _
        & "  AND NICHIGO_HOKAN.KBN_NM2        = H_OUTKAEDI_L.FREE_C08   " & vbNewLine

#End If


    '2013.03.04 / Notes1909受信ファイル名取得開始
    Private Const SQL_FROM_HED_FILER As String = "LEFT JOIN                              " & vbNewLine _
                                                & "(                                                    " & vbNewLine _
                                                & "SELECT                                               " & vbNewLine _
                                                & "$RCV_HED$.EDI_CTL_NO                                 " & vbNewLine _
                                                & ",$RCV_HED$.FILE_NAME                                 " & vbNewLine _
                                                & ",MAX($RCV_HED$.SYS_UPD_DATE) AS SYS_UPD_DATE         " & vbNewLine _
                                                & ",MAX($RCV_HED$.SYS_UPD_TIME) AS SYS_UPD_TIME         " & vbNewLine _
                                                & "FROM                                                 " & vbNewLine _
                                                & "$LM_TRN$..$RCV_HED$              $RCV_HED$           " & vbNewLine _
                                                & "WHERE                                                " & vbNewLine _
                                                & "$RCV_HED$.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine
    Private Const SQL_GROUP_BY_FILER As String = "GROUP BY                                             " & vbNewLine _
                                                & "$RCV_HED$.EDI_CTL_NO                                 " & vbNewLine _
                                                & ",$RCV_HED$.FILE_NAME                                 " & vbNewLine _
                                                & ")                                     RCV_HED_FILER  " & vbNewLine _
                                                & "ON                                                   " & vbNewLine _
                                                    & "H_OUTKAEDI_L.EDI_CTL_NO = RCV_HED_FILER.EDI_CTL_NO   " & vbNewLine
    '2013.03.04 / Notes1909受信ファイル名取得終了



    '▼▼▼二次
    Private Const SQL_FROM_SENDTABLE As String = "LEFT JOIN                                             " & vbNewLine _
                                        & "(                                                            " & vbNewLine _
                                        & "SELECT                                                       " & vbNewLine _
                                        & "H_SEND_EDI.EDI_CTL_NO                                        " & vbNewLine _
                                        & ",H_SEND_EDI.INOUT_KB       AS INOUT_KB                       " & vbNewLine _
                                        & ",MAX(H_SEND_EDI.SEND_DATE) AS SEND_DATE                      " & vbNewLine _
                                        & ",MAX(H_SEND_EDI.SEND_TIME) AS SEND_TIME                      " & vbNewLine _
                                        & ",MAX(H_SEND_EDI.SYS_UPD_DATE) AS SYS_UPD_DATE                " & vbNewLine _
                                        & ",MAX(H_SEND_EDI.SYS_UPD_TIME) AS SYS_UPD_TIME                " & vbNewLine _
                                        & "FROM                                                         " & vbNewLine _
                                        & "$LM_TRN$..$SEND_TBL$                 H_SEND_EDI              " & vbNewLine _
                                        & "WHERE                                                        " & vbNewLine _
                                        & "H_SEND_EDI.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                        & "GROUP BY                                                     " & vbNewLine _
                                        & "H_SEND_EDI.EDI_CTL_NO                                        " & vbNewLine _
                                        & ",H_SEND_EDI.INOUT_KB                                         " & vbNewLine _
                                        & ")                                     SENDTBL                " & vbNewLine _
                                        & "ON                                                           " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = SENDTBL.EDI_CTL_NO                 " & vbNewLine

    Private Const SQL_FROM_SENDTABLE_NORMAL As String = "LEFT JOIN                                  " & vbNewLine _
                                    & "(                                                            " & vbNewLine _
                                    & "SELECT                                                       " & vbNewLine _
                                    & "H_SEND_EDI.EDI_CTL_NO                                        " & vbNewLine _
                                    & ",MAX(H_SEND_EDI.SEND_DATE) AS SEND_DATE                      " & vbNewLine _
                                    & ",MAX(H_SEND_EDI.SEND_TIME) AS SEND_TIME                      " & vbNewLine _
                                    & ",MAX(H_SEND_EDI.SYS_UPD_DATE) AS SYS_UPD_DATE                " & vbNewLine _
                                    & ",MAX(H_SEND_EDI.SYS_UPD_TIME) AS SYS_UPD_TIME                " & vbNewLine _
                                    & "FROM                                                         " & vbNewLine _
                                    & "$LM_TRN$..$SEND_TBL$                 H_SEND_EDI              " & vbNewLine _
                                    & "WHERE                                                        " & vbNewLine _
                                    & "H_SEND_EDI.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                    & "GROUP BY                                                     " & vbNewLine _
                                    & "H_SEND_EDI.EDI_CTL_NO                                        " & vbNewLine _
                                    & ")                                     SENDTBL                " & vbNewLine _
                                    & "ON                                                           " & vbNewLine _
                                    & "H_OUTKAEDI_L.EDI_CTL_NO = SENDTBL.EDI_CTL_NO                 " & vbNewLine


    Private Const SQL_FROM_SENDTABLE_NULL As String = "LEFT JOIN                                        " & vbNewLine _
                                        & "(                                                            " & vbNewLine _
                                        & "SELECT                                                       " & vbNewLine _
                                        & " H_OUTKAEDI_L.EDI_CTL_NO AS EDI_CTL_NO                       " & vbNewLine _
                                        & ",'' AS SEND_DATE                                             " & vbNewLine _
                                        & ",'' AS SEND_TIME                                             " & vbNewLine _
                                        & ",'' AS SYS_UPD_DATE                                          " & vbNewLine _
                                        & ",'' AS SYS_UPD_TIME                                          " & vbNewLine _
                                        & "FROM                                                         " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_L                 H_OUTKAEDI_L          " & vbNewLine _
                                        & "WHERE                                                        " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = @NRS_BR_CD                          " & vbNewLine _
                                        & ")                                     SENDTBL                " & vbNewLine _
                                        & "ON                                                           " & vbNewLine _
                                        & "SENDTBL.EDI_CTL_NO = H_OUTKAEDI_L.EDI_CTL_NO                 " & vbNewLine


    Private Const SQL_FROM_SENDTABLE_INOUT As String = "AND                                             " & vbNewLine _
                                    & "SENDTBL.INOUT_KB = '0'                                           " & vbNewLine

    'ビーピー・カストロール対応 terakawa 2012.01.11 Start
    Private Const SQL_FROM_SENDTABLE_BP As String = "LEFT JOIN                                  " & vbNewLine _
                                & "(                                                            " & vbNewLine _
                                & "SELECT                                                       " & vbNewLine _
                                & "H_SEND_EDI.CRT_DATE                                          " & vbNewLine _
                                & ",H_SEND_EDI.FILE_NAME                                        " & vbNewLine _
                                & ",H_SEND_EDI.REC_NO                                           " & vbNewLine _
                                & ",MAX(H_SEND_EDI.SEND_DATE) AS SEND_DATE                      " & vbNewLine _
                                & ",MAX(H_SEND_EDI.SEND_TIME) AS SEND_TIME                      " & vbNewLine _
                                & ",MAX(H_SEND_EDI.SYS_UPD_DATE) AS SYS_UPD_DATE                " & vbNewLine _
                                & ",MAX(H_SEND_EDI.SYS_UPD_TIME) AS SYS_UPD_TIME                " & vbNewLine _
                                & "FROM                                                         " & vbNewLine _
                                & "$LM_TRN$..$SEND_TBL$                 H_SEND_EDI              " & vbNewLine _
                                & "WHERE                                                        " & vbNewLine _
                                & "H_SEND_EDI.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                                & "AND H_SEND_EDI.INOUT_KB = '0'                                " & vbNewLine _
                                & "GROUP BY                                                     " & vbNewLine _
                                & "H_SEND_EDI.CRT_DATE                                          " & vbNewLine _
                                & ",H_SEND_EDI.FILE_NAME                                        " & vbNewLine _
                                & ",H_SEND_EDI.REC_NO                                           " & vbNewLine _
                                & ")                                     SENDTBL                " & vbNewLine _
                                & "ON                                                           " & vbNewLine _
                                & "RCV_HED.CRT_DATE      = SENDTBL.CRT_DATE                     " & vbNewLine _
                                & "AND RCV_HED.FILE_NAME = SENDTBL.FILE_NAME                    " & vbNewLine _
                                & "AND RCV_HED.REC_NO    = SENDTBL.REC_NO                       " & vbNewLine
    'ビーピー・カストロール対応 terakawa 2012.01.11


    '2012.03.28修正START 要望番号XXX 受信HED：EDI管理№に複数受信が存在する不具合
    'Private Const SQL_FROM_RCVTABLE As String = "LEFT JOIN                                              " & vbNewLine _
    '                                    & "$LM_TRN$..$RCV_HED$              RCV_HED                     " & vbNewLine _
    '                                    & "ON                                                           " & vbNewLine _
    '                                    & "H_OUTKAEDI_L.NRS_BR_CD  = RCV_HED.NRS_BR_CD                  " & vbNewLine _
    '                                    & "AND                                                          " & vbNewLine _
    '                                    & "H_OUTKAEDI_L.EDI_CTL_NO = RCV_HED.EDI_CTL_NO                 " & vbNewLine

    '要望番号2100 2013.09.20 追加START

    Private Const SQL_FROM_HED_FILER_INNER As String = "INNER JOIN                              " & vbNewLine _
                                            & "(                                                    " & vbNewLine _
                                            & "SELECT                                               " & vbNewLine _
                                            & "$RCV_HED$.EDI_CTL_NO                                 " & vbNewLine _
                                            & ",$RCV_HED$.FILE_NAME                                 " & vbNewLine _
                                            & ",MAX($RCV_HED$.SYS_UPD_DATE) AS SYS_UPD_DATE         " & vbNewLine _
                                            & ",MAX($RCV_HED$.SYS_UPD_TIME) AS SYS_UPD_TIME         " & vbNewLine _
                                            & "FROM                                                 " & vbNewLine _
                                            & "$LM_TRN$..$RCV_HED$              $RCV_HED$           " & vbNewLine _
                                            & "WHERE                                                " & vbNewLine _
                                            & "$RCV_HED$.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine

    Private Const SQL_FROM_HED_DICNEW_FILER As String = "INNER JOIN                              " & vbNewLine _
                                            & "(                                                    " & vbNewLine _
                                            & "SELECT                                               " & vbNewLine _
                                            & "H_INOUTKAEDI_HED_DIC_NEW.EDI_CTL_NO                                 " & vbNewLine _
                                            & ",H_INOUTKAEDI_HED_DIC_NEW.FILE_NAME                                 " & vbNewLine _
                                            & ",MAX(H_INOUTKAEDI_HED_DIC_NEW.SYS_UPD_DATE) AS SYS_UPD_DATE         " & vbNewLine _
                                            & ",MAX(H_INOUTKAEDI_HED_DIC_NEW.SYS_UPD_TIME) AS SYS_UPD_TIME         " & vbNewLine _
                                            & "FROM                                                 " & vbNewLine _
                                            & "$LM_TRN$..H_INOUTKAEDI_HED_DIC_NEW  H_INOUTKAEDI_HED_DIC_NEW           " & vbNewLine _
                                            & "WHERE                                                " & vbNewLine _
                                            & "H_INOUTKAEDI_HED_DIC_NEW.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine

    Private Const SQL_GROUP_BY_DICNEW_FILER As String = "GROUP BY                                             " & vbNewLine _
                                                & "H_INOUTKAEDI_HED_DIC_NEW.EDI_CTL_NO                                 " & vbNewLine _
                                                & ",H_INOUTKAEDI_HED_DIC_NEW.FILE_NAME                                 " & vbNewLine _
                                                & ")                                     RCV_HED_FILER  " & vbNewLine _
                                                & "ON                                                   " & vbNewLine _
                                                    & "H_OUTKAEDI_L.EDI_CTL_NO = RCV_HED_FILER.EDI_CTL_NO   " & vbNewLine

    Private Const SQL_FROM_RCVDIC_NEW As String = "INNER JOIN                              " & vbNewLine _
                                & "(                                                    " & vbNewLine _
                                & "SELECT                                               " & vbNewLine _
                                & " H_INOUTKAEDI_HED_DIC_NEW.EDI_CTL_NO                                 " & vbNewLine _
                                & ",H_INOUTKAEDI_HED_DIC_NEW.PRTFLG                                    " & vbNewLine _
                                & ",MAX(H_INOUTKAEDI_HED_DIC_NEW.SYS_UPD_DATE) AS SYS_UPD_DATE         " & vbNewLine _
                                & ",MAX(H_INOUTKAEDI_HED_DIC_NEW.SYS_UPD_TIME) AS SYS_UPD_TIME         " & vbNewLine _
                                & "FROM                                                 " & vbNewLine _
                                & "$LM_TRN$..H_INOUTKAEDI_HED_DIC_NEW      H_INOUTKAEDI_HED_DIC_NEW           " & vbNewLine _
                                & "WHERE                                                " & vbNewLine _
                                & "H_INOUTKAEDI_HED_DIC_NEW.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine

    Private Const SQL_FROM_RCVDIC_NEW_GROUP_BY As String = "GROUP BY                      " & vbNewLine _
                                & "H_INOUTKAEDI_HED_DIC_NEW.EDI_CTL_NO                    " & vbNewLine _
                                & ",H_INOUTKAEDI_HED_DIC_NEW.PRTFLG                       " & vbNewLine _
                                & ")                                     RCV_HED        " & vbNewLine _
                                & "ON                                                   " & vbNewLine _
                                & "H_OUTKAEDI_L.EDI_CTL_NO = RCV_HED.EDI_CTL_NO         " & vbNewLine

    Private Const SQL_FROM_RCVTABLE_INNER As String = "INNER JOIN                              " & vbNewLine _
                            & "(                                                    " & vbNewLine _
                            & "SELECT                                               " & vbNewLine _
                            & "$RCV_HED$.EDI_CTL_NO                                 " & vbNewLine _
                            & ",$RCV_HED$.PRTFLG                                    " & vbNewLine _
                            & ",MAX($RCV_HED$.SYS_UPD_DATE) AS SYS_UPD_DATE         " & vbNewLine _
                            & ",MAX($RCV_HED$.SYS_UPD_TIME) AS SYS_UPD_TIME         " & vbNewLine _
                            & "FROM                                                 " & vbNewLine _
                            & "$LM_TRN$..$RCV_HED$              $RCV_HED$           " & vbNewLine _
                            & "WHERE                                                " & vbNewLine _
                            & "$RCV_HED$.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine

    Private Const SQL_FROM_RCVDIC_NEW_INOUT As String = "AND                              " & vbNewLine _
                            & "H_INOUTKAEDI_HED_DIC_NEW.INOUT_KB = '0'                             " & vbNewLine

    Private Const SQL_FROM_RCVDIC_NEW_WHERE_CUST_LM As String = "AND H_INOUTKAEDI_HED_DIC_NEW.CUST_CD_L = @CUST_CD_L                 " & vbNewLine _
                                                         & "AND H_INOUTKAEDI_HED_DIC_NEW.CUST_CD_M = @CUST_CD_M                 " & vbNewLine

    Private Const SQL_FROM_RCVDIC_NEW_WHERE_CUST_L As String = "AND H_INOUTKAEDI_HED_DIC_NEW.CUST_CD_L = @CUST_CD_L                 " & vbNewLine

    '要望番号2100 2013.09.20 追加END


    '要望番号:1786 terakara 2013.01.23 PRTFLG追加 Start
    Private Const SQL_FROM_RCVTABLE As String = "LEFT JOIN                              " & vbNewLine _
                                & "(                                                    " & vbNewLine _
                                & "SELECT                                               " & vbNewLine _
                                & "$RCV_HED$.EDI_CTL_NO                                 " & vbNewLine _
                                & ",$RCV_HED$.PRTFLG                                    " & vbNewLine _
                                & ",MAX($RCV_HED$.SYS_UPD_DATE) AS SYS_UPD_DATE         " & vbNewLine _
                                & ",MAX($RCV_HED$.SYS_UPD_TIME) AS SYS_UPD_TIME         " & vbNewLine _
                                & "FROM                                                 " & vbNewLine _
                                & "$LM_TRN$..$RCV_HED$              $RCV_HED$           " & vbNewLine _
                                & "WHERE                                                " & vbNewLine _
                                & "$RCV_HED$.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine
    '要望番号:1786 terakara 2013.01.23 PRTFLG追加 End

    '要望番号:1214(【春日部】出荷EDI検索で、タイムアウト) 2012/07/02 本明 Start
    Private Const SQL_FROM_RCVTABLE_WHERE_CUST_LM As String = "AND $RCV_HED$.CUST_CD_L = @CUST_CD_L                 " & vbNewLine _
                                                         & "AND $RCV_HED$.CUST_CD_M = @CUST_CD_M                 " & vbNewLine
    '要望番号:1214(【春日部】出荷EDI検索で、タイムアウト) 2012/07/02 本明 End

    '要望番号:1214(【春日部】出荷EDI検索で、タイムアウト) 2012/07/02 本明 Start
    Private Const SQL_FROM_RCVTABLE_WHERE_CUST_L As String = "AND $RCV_HED$.CUST_CD_L = @CUST_CD_L                 " & vbNewLine
    '要望番号:1214(【春日部】出荷EDI検索で、タイムアウト) 2012/07/02 本明 End



    Private Const SQL_FROM_RCVTABLE_INOUT As String = "AND                              " & vbNewLine _
                                & "$RCV_HED$.INOUT_KB = '0'                             " & vbNewLine

    '要望番号:1786 terakara 2013.01.23 PRTFLG追加 Start
    Private Const SQL_FROM_RCVTABLE_GROUP_BY As String = "GROUP BY                      " & vbNewLine _
                                & "$RCV_HED$.EDI_CTL_NO                                 " & vbNewLine _
                                & ",$RCV_HED$.PRTFLG                                    " & vbNewLine _
                                & ")                                     RCV_HED        " & vbNewLine _
                                & "ON                                                   " & vbNewLine _
                                & "H_OUTKAEDI_L.EDI_CTL_NO = RCV_HED.EDI_CTL_NO         " & vbNewLine
    '要望番号:1786 terakara 2013.01.23 PRTFLG追加 End

    '2012.03.28修正END 要望番号XXX 受信HED：EDI管理№に複数受信が存在する不具合

    '要望番号:1786 terakara 2013.01.23 PRTFLG追加 Start
    Private Const SQL_FROM_RCVTABLE_NULL As String = "LEFT JOIN                                         " & vbNewLine _
                                        & "(                                                            " & vbNewLine _
                                        & "SELECT                                                       " & vbNewLine _
                                        & " H_OUTKAEDI_L.EDI_CTL_NO AS EDI_CTL_NO                       " & vbNewLine _
                                        & ",'' AS SYS_UPD_DATE                                          " & vbNewLine _
                                        & ",'' AS SYS_UPD_TIME                                          " & vbNewLine _
                                        & ",'' AS PRTFLG                                                " & vbNewLine _
                                        & "FROM                                                         " & vbNewLine _
                                        & "$LM_TRN$..H_OUTKAEDI_L                H_OUTKAEDI_L           " & vbNewLine _
                                        & "WHERE                                                        " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = @NRS_BR_CD                          " & vbNewLine _
                                        & ")                                     RCV_HED                " & vbNewLine _
                                        & "ON                                                           " & vbNewLine _
                                        & "H_OUTKAEDI_L.EDI_CTL_NO = RCV_HED.EDI_CTL_NO                 " & vbNewLine
    '要望番号:1786 terakara 2013.01.23 PRTFLG追加 End

    '2012.03.28修正START 要望番号XXX 受信HED：EDI管理№に複数受信が存在する不具合
    'Private Const SQL_FROM_RCVTABLE_INOUT As String = "AND                                              " & vbNewLine _
    '                                    & "RCV_HED.INOUT_KB = '0'                                       " & vbNewLine
    '2012.03.28修正END 要望番号XXX 受信HED：EDI管理№に複数受信が存在する不具合

    'ビーピー・カストロール対応 terakawa 2012.01.11 Start
    '要望番号:1786 terakara 2013.01.23 PRTFLG追加 Start
    Private Const SQL_FROM_RCVTABLE_BP As String = "LEFT JOIN                       " & vbNewLine _
                            & "(                                                    " & vbNewLine _
                            & "SELECT                                               " & vbNewLine _
                            & "$RCV_HED$.EDI_CTL_NO                                 " & vbNewLine _
                            & ",$RCV_HED$.CRT_DATE                                  " & vbNewLine _
                            & ",$RCV_HED$.FILE_NAME                                 " & vbNewLine _
                            & ",$RCV_HED$.REC_NO                                    " & vbNewLine _
                            & ",$RCV_HED$.PRTFLG                                    " & vbNewLine _
                            & ",MAX($RCV_HED$.SYS_UPD_DATE) AS SYS_UPD_DATE         " & vbNewLine _
                            & ",MAX($RCV_HED$.SYS_UPD_TIME) AS SYS_UPD_TIME         " & vbNewLine _
                            & "FROM                                                 " & vbNewLine _
                            & "$LM_TRN$..$RCV_HED$              $RCV_HED$           " & vbNewLine _
                            & "WHERE                                                " & vbNewLine _
                            & "$RCV_HED$.NRS_BR_CD = @NRS_BR_CD                     " & vbNewLine _
                            & "GROUP BY                                             " & vbNewLine _
                            & "$RCV_HED$.EDI_CTL_NO                                 " & vbNewLine _
                            & ",$RCV_HED$.CRT_DATE                                  " & vbNewLine _
                            & ",$RCV_HED$.FILE_NAME                                 " & vbNewLine _
                            & ",$RCV_HED$.REC_NO                                    " & vbNewLine _
                            & ",$RCV_HED$.PRTFLG                                    " & vbNewLine _
                            & ")                                     RCV_HED        " & vbNewLine _
                            & "ON                                                   " & vbNewLine _
                            & "H_OUTKAEDI_L.EDI_CTL_NO = RCV_HED.EDI_CTL_NO         " & vbNewLine
    '要望番号:1786 terakara 2013.01.23 PRTFLG追加 Start
    'ビーピー・カストロール対応 terakawa 2012.01.11 End

    '2012.03.20 大阪対応START
    Private Const SQL_FROM_OUTKA_UNSO As String = "LEFT JOIN                                             " & vbNewLine _
                                    & "$LM_TRN$..F_UNSO_L                     F_UNSO_L                " & vbNewLine _
                                    & "ON                                                             " & vbNewLine _
                                    & "H_OUTKAEDI_L.NRS_BR_CD = F_UNSO_L.NRS_BR_CD                    " & vbNewLine _
                                    & "AND                                                            " & vbNewLine _
                                    & "H_OUTKAEDI_L.OUTKA_CTL_NO = F_UNSO_L.INOUTKA_NO_L              " & vbNewLine _
                                    & "AND                                                            " & vbNewLine _
                                    & "H_OUTKAEDI_L.OUTKA_CTL_NO <> ''                                " & vbNewLine _
                                    & "AND                                                            " & vbNewLine _
                                    & "F_UNSO_L.MOTO_DATA_KB = '20'                                   " & vbNewLine



    Private Const SQL_FROM_UNSO_ONLY As String = "LEFT JOIN                                             " & vbNewLine _
                                        & "$LM_TRN$..F_UNSO_L                     F_UNSO_L                " & vbNewLine _
                                        & "ON                                                             " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = F_UNSO_L.NRS_BR_CD                    " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "SUBSTRING(H_OUTKAEDI_L.FREE_C30,4,9) = F_UNSO_L.UNSO_NO_L      " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.OUTKA_CTL_NO = ''                                 " & vbNewLine _
                                        & "AND                                                            " & vbNewLine _
                                        & "F_UNSO_L.MOTO_DATA_KB = '40'                                   " & vbNewLine

    '2012.03.20 大阪対応END

    '▲▲▲二次

    Private Const SQL_FROM_UNSO_NCG As String = "LEFT JOIN                                            " & vbNewLine _
                                    & "$LM_TRN$..F_UNSO_L                     F_UNSO_LNCG             " & vbNewLine _
                                    & "ON                                                             " & vbNewLine _
                                    & "H_OUTKAEDI_L.NRS_BR_CD = F_UNSO_LNCG.NRS_BR_CD                 " & vbNewLine _
                                    & "AND                                                            " & vbNewLine _
                                    & "LEN(H_OUTKAEDI_L.FREE_C30) >= 12                               " & vbNewLine _
                                    & "AND                                                            " & vbNewLine _
                                    & "SUBSTRING(H_OUTKAEDI_L.FREE_C30,4,9) = F_UNSO_LNCG.UNSO_NO_L   " & vbNewLine _
                                    & "AND                                                            " & vbNewLine _
                                    & "H_OUTKAEDI_L.OUTKA_CTL_NO = ''                                 " & vbNewLine _
                                    & "AND                                                            " & vbNewLine _
                                    & "F_UNSO_LNCG.MOTO_DATA_KB = '40'                                " & vbNewLine

    '修正start 2018/11/09 要望番号002808追加
    Private Const SQL_FROM_SQL_UNSOEDI_EXISTS As String _
        = "LEFT JOIN                                         " & vbNewLine _
        & "  (SELECT NRS_BR_CD,EDI_CTL_NO FROM LM_TRN..H_UNSOEDI_DTL_NCGO " & vbNewLine _
        & "  WHERE  SYS_DEL_FLG = '0'                        " & vbNewLine _
        & "  GROUP BY NRS_BR_CD,EDI_CTL_NO                   " & vbNewLine _
        & "   )UNSOEDI                                       " & vbNewLine _
        & "  ON UNSOEDI.NRS_BR_CD = H_OUTKAEDI_L.NRS_BR_CD   " & vbNewLine _
        & " AND UNSOEDI.EDI_CTL_NO = H_OUTKAEDI_L.EDI_CTL_NO " & vbNewLine

    '    Private Const SQL_FROM_SQL_UNSOEDI_EXISTS As String _
    '        = "LEFT JOIN LM_TRN..H_UNSOEDI_DTL_NCGO UNSOEDI      " & vbNewLine _
    '        & "  ON UNSOEDI.NRS_BR_CD = H_OUTKAEDI_L.NRS_BR_CD   " & vbNewLine _
    '        & " AND UNSOEDI.EDI_CTL_NO = H_OUTKAEDI_L.EDI_CTL_NO " & vbNewLine _
    '        & " AND UNSOEDI.SYS_DEL_FLG = '0'                    " & vbNewLine
    '修正End 2018/11/09 要望番号002808追加

    'Add Start 2018/10/31 要望番号002808
    Private Const SQL_FROM_H_OUTKAEDI_DTL_NCGO_NEW As String _
        = "LEFT JOIN (SELECT EDI_CTL_NO, BIN_KBN                   " & vbNewLine _
        & "             FROM $LM_TRN$..H_OUTKAEDI_DTL_NCGO_NEW     " & vbNewLine _
        & "            WHERE NRS_BR_CD = @NRS_BR_CD                " & vbNewLine _
        & "              AND SYS_DEL_FLG  = '0'                    " & vbNewLine _
        & "            GROUP BY EDI_CTL_NO, BIN_KBN                " & vbNewLine _
        & "           )  DTL_NCGO_NEW                              " & vbNewLine _
        & "  ON DTL_NCGO_NEW.EDI_CTL_NO = H_OUTKAEDI_L.EDI_CTL_NO  " & vbNewLine
    'Add End 2018/10/31 要望番号002808

#End Region

#End Region

    '▼▼▼二次
#Region "出荷登録初期設定用SQL"
#Region "SELECT_M_CUST(出荷登録)"

    Private Const SQL_M_CUST_OUTKATOUROKU As String = " SELECT                                       " & vbNewLine _
                                 & " NRS_BR_CD                     AS  NRS_BR_CD                     " & vbNewLine _
                                 & ",CUST_CD_L                     AS  CUST_CD_L                     " & vbNewLine _
                                 & ",CUST_CD_M                     AS  CUST_CD_M                     " & vbNewLine _
                                 & ",CUST_CD_S                     AS  CUST_CD_S                     " & vbNewLine _
                                 & ",CUST_CD_SS                    AS  CUST_CD_SS                    " & vbNewLine _
                                 & ",CUST_OYA_CD                   AS  CUST_OYA_CD                   " & vbNewLine _
                                 & ",CUST_NM_L                     AS  CUST_NM_L                     " & vbNewLine _
                                 & ",CUST_NM_M                     AS  CUST_NM_M                     " & vbNewLine _
                                 & ",CUST_NM_S                     AS  CUST_NM_S                     " & vbNewLine _
                                 & ",CUST_NM_SS                    AS  CUST_NM_SS                    " & vbNewLine _
                                 & ",ZIP                           AS  ZIP                           " & vbNewLine _
                                 & ",AD_1                          AS  AD_1                          " & vbNewLine _
                                 & ",AD_2                          AS  AD_2                          " & vbNewLine _
                                 & ",AD_3                          AS  AD_3                          " & vbNewLine _
                                 & ",PIC                           AS  PIC                           " & vbNewLine _
                                 & ",FUKU_PIC                      AS  FUKU_PIC                      " & vbNewLine _
                                 & ",TEL                           AS  TEL                           " & vbNewLine _
                                 & ",FAX                           AS  FAX                           " & vbNewLine _
                                 & ",MAIL                          AS  MAIL                          " & vbNewLine _
                                 & ",SAITEI_HAN_KB                 AS  SAITEI_HAN_KB                 " & vbNewLine _
                                 & ",OYA_SEIQTO_CD                 AS  OYA_SEIQTO_CD                 " & vbNewLine _
                                 & ",HOKAN_SEIQTO_CD               AS  HOKAN_SEIQTO_CD               " & vbNewLine _
                                 & ",NIYAKU_SEIQTO_CD              AS  NIYAKU_SEIQTO_CD              " & vbNewLine _
                                 & ",UNCHIN_SEIQTO_CD              AS  UNCHIN_SEIQTO_CD              " & vbNewLine _
                                 & ",SAGYO_SEIQTO_CD               AS  SAGYO_SEIQTO_CD               " & vbNewLine _
                                 & ",INKA_RPT_YN                   AS  INKA_RPT_YN                   " & vbNewLine _
                                 & ",OUTKA_RPT_YN                  AS  OUTKA_RPT_YN                  " & vbNewLine _
                                 & ",ZAI_RPT_YN                    AS  ZAI_RPT_YN                    " & vbNewLine _
                                 & ",UNSO_TEHAI_KB                 AS  UNSO_TEHAI_KB                 " & vbNewLine _
                                 & ",SP_UNSO_CD                    AS  SP_UNSO_CD                    " & vbNewLine _
                                 & ",SP_UNSO_BR_CD                 AS  SP_UNSO_BR_CD                 " & vbNewLine _
                                 & ",BETU_KYORI_CD                 AS  BETU_KYORI_CD                 " & vbNewLine _
                                 & ",TAX_KB                        AS  TAX_KB                        " & vbNewLine _
                                 & ",HOKAN_FREE_KIKAN              AS  HOKAN_FREE_KIKAN              " & vbNewLine _
                                 & ",SMPL_SAGYO                    AS  SMPL_SAGYO                    " & vbNewLine _
                                 & ",HOKAN_NIYAKU_CALCULATION      AS  HOKAN_NIYAKU_CALCULATION      " & vbNewLine _
                                 & ",HOKAN_NIYAKU_CALCULATION_OLD  AS  HOKAN_NIYAKU_CALCULATION_OLD  " & vbNewLine _
                                 & ",NEW_JOB_NO                    AS  NEW_JOB_NO                    " & vbNewLine _
                                 & ",OLD_JOB_NO                    AS  OLD_JOB_NO                    " & vbNewLine _
                                 & ",HOKAN_NIYAKU_KEISAN_YN        AS  HOKAN_NIYAKU_KEISAN_YN        " & vbNewLine _
                                 & ",UNTIN_CALCULATION_KB          AS  UNTIN_CALCULATION_KB          " & vbNewLine _
                                 & ",DENPYO_NM                     AS  DENPYO_NM                     " & vbNewLine _
                                 & ",SOKO_CHANGE_KB                AS  SOKO_CHANGE_KB                " & vbNewLine _
                                 & ",DEFAULT_SOKO_CD               AS  DEFAULT_SOKO_CD               " & vbNewLine _
                                 & ",PICK_LIST_KB                  AS  PICK_LIST_KB                  " & vbNewLine _
                                 & ",SEKY_OFB_KB                   AS  SEKY_OFB_KB                   " & vbNewLine _
                                 & " FROM                                                            " & vbNewLine _
                                 & " $LM_MST$..M_CUST                       M_CUST                   " & vbNewLine _
                                 & " WHERE                                                           " & vbNewLine _
                                 & " M_CUST.NRS_BR_CD   = @NRS_BR_CD                                 " & vbNewLine _
                                 & " AND                                                             " & vbNewLine _
                                 & " M_CUST.CUST_CD_L   = @CUST_CD_L                                 " & vbNewLine _
                                 & " AND                                                             " & vbNewLine _
                                 & " M_CUST.CUST_CD_M   = @CUST_CD_M                                 " & vbNewLine _
                                 & " AND                                                             " & vbNewLine _
                                 & " M_CUST.CUST_CD_S   = '00'                                       " & vbNewLine _
                                 & " AND                                                             " & vbNewLine _
                                 & " M_CUST.CUST_CD_SS  = '00'                                       " & vbNewLine


#End Region

#Region "SELECT_UNCHIN_TARIFF_SET(出荷登録)"
    Private Const SQL_UNCHIN_TARIFF_SET_CUST As String = "SELECT                                            " & vbNewLine _
                                        & "*                                                                " & vbNewLine _
                                        & "FROM                                                             " & vbNewLine _
                                        & "$LM_MST$..M_UNCHIN_TARIFF_SET  M_UNCHIN_TARIFF_SET               " & vbNewLine _
                                        & "WHERE                                                            " & vbNewLine _
                                        & "M_UNCHIN_TARIFF_SET.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "M_UNCHIN_TARIFF_SET.CUST_CD_L = @CUST_CD_L                       " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "M_UNCHIN_TARIFF_SET.CUST_CD_M = @CUST_CD_M                       " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "M_UNCHIN_TARIFF_SET.SET_KB = '00'                                " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "M_UNCHIN_TARIFF_SET.DEST_CD = ''                                 " & vbNewLine


    Private Const SQL_UNCHIN_TARIFF_SET_DEST As String = "SELECT                                            " & vbNewLine _
                                        & "*                                                                " & vbNewLine _
                                        & "FROM                                                             " & vbNewLine _
                                        & "$LM_MST$..M_UNCHIN_TARIFF_SET  M_UNCHIN_TARIFF_SET               " & vbNewLine _
                                        & "WHERE                                                            " & vbNewLine _
                                        & "M_UNCHIN_TARIFF_SET.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "M_UNCHIN_TARIFF_SET.CUST_CD_L = @CUST_CD_L                       " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "M_UNCHIN_TARIFF_SET.CUST_CD_M = @CUST_CD_M                       " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "M_UNCHIN_TARIFF_SET.DEST_CD = @DEST_CD                           " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "M_UNCHIN_TARIFF_SET.SET_KB = '01'                              " & vbNewLine

#End Region

    '(2012.09.07)要望番号:1425 UMANO START
#Region "SELECT_M_UNCHIN_TARIFF_SET_UNSOCO(出荷登録)"

    Private Const SQL_UNCHIN_TARIFF_SET_UNSOCO As String = "SELECT                                      " & vbNewLine _
                                    & "*                                                                " & vbNewLine _
                                    & "FROM                                                             " & vbNewLine _
                                    & "$LM_MST$..M_UNCHIN_TARIFF_SET_UNSOCO  M_UNCHIN_TARIFF_SET        " & vbNewLine _
                                    & "WHERE                                                            " & vbNewLine _
                                    & "M_UNCHIN_TARIFF_SET.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
                                    & "AND                                                              " & vbNewLine _
                                    & "M_UNCHIN_TARIFF_SET.CUST_CD_L = @CUST_CD_L                       " & vbNewLine _
                                    & "AND                                                              " & vbNewLine _
                                    & "M_UNCHIN_TARIFF_SET.CUST_CD_M = @CUST_CD_M                       " & vbNewLine _
                                    & "AND                                                              " & vbNewLine _
                                    & "M_UNCHIN_TARIFF_SET.UNSOCO_CD = @UNSO_CD                         " & vbNewLine _
                                    & "AND                                                              " & vbNewLine _
                                    & "M_UNCHIN_TARIFF_SET.UNSOCO_BR_CD = @UNSO_BR_CD                   " & vbNewLine _
                                    & "AND                                                              " & vbNewLine _
                                    & "M_UNCHIN_TARIFF_SET.UNSO_TEHAI_KB = @UNSO_MOTO_KB                " & vbNewLine
#End Region
    '(2012.09.07)要望番号:1425 UMANO END

#Region "SELECT_SHIP_NM_FROM_DEST(出荷登録)"
    Private Const SQL_SELECT_SHIPNM_FROM_DEST As String = "SELECT                                           " & vbNewLine _
                                        & "DEST_NM                                                          " & vbNewLine _
                                        & "FROM                                                             " & vbNewLine _
                                        & "$LM_MST$..M_DEST  M_DEST                                         " & vbNewLine _
                                        & "WHERE                                                            " & vbNewLine _
                                        & "H_OUTKAEDI_L.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "M_DEST.CUST_CD_L = @CUST_CD_L                                    " & vbNewLine _
                                        & "AND                                                              " & vbNewLine _
                                        & "M_DEST.DEST_CD = @DEST_CD                                        " & vbNewLine


#End Region

#Region "SELECT_M_JIS"

    '2013.01.09 要望番号1753 修正START
    Private Const SQL_SELECT_M_JIS_FROM_ZIP As String = "SELECT DISTINCT                " & vbNewLine _
                                 & " M_JIS.JIS_CD                        AS JIS_CD      " & vbNewLine _
                                 & " FROM                                               " & vbNewLine _
                                 & " $LM_MST$..M_JIS                     M_JIS          " & vbNewLine _
                                 & "INNER JOIN                                          " & vbNewLine _
                                 & " $LM_MST$..M_ZIP                     M_ZIP          " & vbNewLine _
                                 & " ON                                                 " & vbNewLine _
                                 & " M_JIS.JIS_CD = M_ZIP.JIS_CD                        " & vbNewLine _
                                 & "WHERE                                               " & vbNewLine _
                                 & " M_ZIP.ZIP_NO  = @DEST_ZIP                          " & vbNewLine _
                                 & " AND                                                " & vbNewLine _
                                 & " M_JIS.SYS_DEL_FLG  = '0'                           " & vbNewLine _
                                 & " AND                                                " & vbNewLine _
                                 & " M_ZIP.SYS_DEL_FLG  = '0'                           " & vbNewLine

    'Private Const SQL_SELECT_M_JIS_FROM_ZIP As String = "SELECT                             " & vbNewLine _
    '                                 & " M_JIS.JIS_CD                        AS JIS_CD      " & vbNewLine _
    '                                 & " FROM                                               " & vbNewLine _
    '                                 & " $LM_MST$..M_JIS                     M_JIS          " & vbNewLine _
    '                                 & "INNER JOIN                                          " & vbNewLine _
    '                                 & " $LM_MST$..M_ZIP                     M_ZIP          " & vbNewLine _
    '                                 & " ON                                                 " & vbNewLine _
    '                                 & " M_JIS.JIS_CD = M_ZIP.JIS_CD                        " & vbNewLine _
    '                                 & "WHERE                                               " & vbNewLine _
    '                                 & " M_ZIP.ZIP_NO  = @DEST_ZIP                          " & vbNewLine _
    '                                 & " AND                                                " & vbNewLine _
    '                                 & " M_JIS.SYS_DEL_FLG  = '0'                           " & vbNewLine _
    '                                 & " AND                                                " & vbNewLine _
    '                                 & " M_ZIP.SYS_DEL_FLG  = '0'                           " & vbNewLine
    '2013.01.09 要望番号1753 修正END


    '2012.03.07 要望番号845 修正START
    Private Const SQL_SELECT_M_JIS_FROM_ADD As String = "SELECT                              " & vbNewLine _
                                     & " MAX(M_JIS.JIS_CD)                   AS JIS_CD       " & vbNewLine _
                                     & " FROM                                                " & vbNewLine _
                                     & " $LM_MST$..M_JIS                     M_JIS           " & vbNewLine _
                                     & "WHERE                                                " & vbNewLine _
                                     & " M_JIS.SYS_DEL_FLG  = '0'                            " & vbNewLine _
                                     & " AND                                                 " & vbNewLine _
                                     & " @ADDRESS LIKE REPLACE(REPLACE(M_JIS.KEN + M_JIS.SHI,'　',''),' ','')  + '%' " & vbNewLine

    '2012.03.07 要望番号845 修正END


#End Region

#End Region

#Region "一括変更処理 マスタチェック用SQL"

    ''' <summary>
    ''' 運送会社マスタﾁｪｯｸ用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UNSOCO_MST_CHECK As String = "SELECT                              " & vbNewLine _
                                                 & "   COUNT(UNSOCO_CD)  AS REC_CNT     " & vbNewLine _
                                                 & "FROM                                " & vbNewLine _
                                                 & "$LM_MST$..M_UNSOCO                  " & vbNewLine _
                                                 & "WHERE                               " & vbNewLine _
                                                 & "    NRS_BR_CD      = @NRS_BR_CD     " & vbNewLine _
                                                 & "AND UNSOCO_CD      = @UNSOCO_CD     " & vbNewLine _
                                                 & "AND UNSOCO_BR_CD   = @UNSOCO_BR_CD  " & vbNewLine

    ''' <summary>
    ''' 運送会社名取得用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UNSONM_GET As String = "SELECT                                          " & vbNewLine _
                                                 & "   COUNT(UNSOCO_CD)  AS REC_CNT           " & vbNewLine _
                                                 & ",UNSOCO_CD        AS EDIT_ITEM_VALUE1     " & vbNewLine _
                                                 & ",UNSOCO_BR_CD     AS EDIT_ITEM_VALUE2     " & vbNewLine _
                                                 & ",UNSOCO_NM        AS EDIT_ITEM_VALUE3     " & vbNewLine _
                                                 & ",UNSOCO_BR_NM     AS EDIT_ITEM_VALUE4     " & vbNewLine _
                                                 & "FROM                                      " & vbNewLine _
                                                 & "$LM_MST$..M_UNSOCO                        " & vbNewLine _
                                                 & "WHERE                                     " & vbNewLine _
                                                 & "    NRS_BR_CD      = @NRS_BR_CD           " & vbNewLine _
                                                 & "AND UNSOCO_CD      = @UNSOCO_CD           " & vbNewLine _
                                                 & "AND UNSOCO_BR_CD   = @UNSOCO_BR_CD        " & vbNewLine _
                                                 & "GROUP BY                                  " & vbNewLine _
                                                 & " UNSOCO_CD                                " & vbNewLine _
                                                 & ",UNSOCO_BR_CD                             " & vbNewLine _
                                                 & ",UNSOCO_NM                                " & vbNewLine _
                                                 & ",UNSOCO_BR_NM                             " & vbNewLine


    ''' <summary>
    ''' 排他チェック用(WHERE句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_HAITA As String = " WHERE                                " & vbNewLine _
                                            & " NRS_BR_CD         = @NRS_BR_CD       " & vbNewLine _
                                            & " AND SYS_UPD_DATE  = @SYS_UPD_DATE    " & vbNewLine _
                                            & " AND SYS_UPD_TIME  = @SYS_UPD_TIME    " & vbNewLine


#End Region

#Region "EDI取消,EDI取消⇒未登録処理 更新用SQL"

#Region "OUTKAEDI_L(EDI取消、EDI取消⇒未登録)"
    ''' <summary>
    ''' EDI出荷(大)更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_EDITORIKESI_EDI_L As String = " UPDATE                                   " & vbNewLine _
                                         & " $LM_TRN$..H_OUTKAEDI_L                                " & vbNewLine _
                                         & " SET                                                   " & vbNewLine _
                                         & " DEL_KB = @DEL_KB                                      " & vbNewLine _
                                         & ",UPD_USER = @UPD_USER                                  " & vbNewLine _
                                         & ",UPD_DATE = @UPD_DATE                                  " & vbNewLine _
                                         & ",UPD_TIME = @UPD_TIME                                  " & vbNewLine _
                                         & ",SYS_UPD_PGID = @SYS_UPD_PGID                          " & vbNewLine _
                                         & ",SYS_UPD_USER = @SYS_UPD_USER                          " & vbNewLine _
                                         & ",SYS_UPD_DATE = @SYS_UPD_DATE                          " & vbNewLine _
                                         & ",SYS_UPD_TIME = @SYS_UPD_TIME                          " & vbNewLine _
                                         & ",SYS_DEL_FLG  = @SYS_DEL_FLG                           " & vbNewLine _
                                         & " WHERE                                                 " & vbNewLine _
                                         & " NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
                                         & " AND                                                   " & vbNewLine _
                                         & " EDI_CTL_NO = @EDI_CTL_NO                              " & vbNewLine


#End Region

#Region "OUTKAEDI_M(EDI取消、EDI取消⇒未登録)"
    ''' <summary>
    ''' EDI出荷(中)更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_EDITORIKESI_EDI_M As String = " UPDATE                                   " & vbNewLine _
                                         & " $LM_TRN$..H_OUTKAEDI_M                                " & vbNewLine _
                                         & " SET                                                   " & vbNewLine _
                                         & " DEL_KB = @DEL_KB                                      " & vbNewLine _
                                         & ",UPD_USER = @UPD_USER                                  " & vbNewLine _
                                         & ",UPD_DATE = @UPD_DATE                                  " & vbNewLine _
                                         & ",UPD_TIME = @UPD_TIME                                  " & vbNewLine _
                                         & ",SYS_UPD_PGID = @SYS_UPD_PGID                          " & vbNewLine _
                                         & ",SYS_UPD_USER = @SYS_UPD_USER                          " & vbNewLine _
                                         & ",SYS_UPD_DATE = @SYS_UPD_DATE                          " & vbNewLine _
                                         & ",SYS_UPD_TIME = @SYS_UPD_TIME                          " & vbNewLine _
                                         & ",SYS_DEL_FLG  = @SYS_DEL_FLG                           " & vbNewLine _
                                         & " WHERE                                                 " & vbNewLine _
                                         & " NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
                                         & " AND                                                   " & vbNewLine _
                                         & " EDI_CTL_NO = @EDI_CTL_NO                              " & vbNewLine

#End Region

#Region "RCV_HED(EDI取消、EDI取消⇒未登録)"
    ''' <summary>
    ''' EDI受信(HED)更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_EDITORIKESI_RCV_HED As String = "UPDATE                                " & vbNewLine _
                                              & " $LM_TRN$..$RCV_HED$                               " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " DEL_KB            = @DEL_KB                       " & vbNewLine _
                                              & ",DELETE_USER       = @DELETE_USER                  " & vbNewLine _
                                              & ",DELETE_DATE       = @DELETE_DATE                  " & vbNewLine _
                                              & ",DELETE_TIME       = @DELETE_TIME                  " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG       = @SYS_DEL_FLG                  " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine

    ''' <summary>
    ''' EDI受信(HED)更新(SAP DIC用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_EDITORIKESI_RCV_HED_DICNEW As String = "UPDATE                                " & vbNewLine _
                                              & " $LM_TRN$..H_INOUTKAEDI_HED_DIC_NEW                " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " DEL_KB            = @DEL_KB                       " & vbNewLine _
                                              & ",DELETE_USER       = @DELETE_USER                  " & vbNewLine _
                                              & ",DELETE_DATE       = @DELETE_DATE                  " & vbNewLine _
                                              & ",DELETE_TIME       = @DELETE_TIME                  " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG       = @SYS_DEL_FLG                  " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine

#End Region

#Region "RCV_DTL(EDI取消、EDI取消⇒未登録)"
    ''' <summary>
    ''' EDI受信(DTL)更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_EDITORIKESI_RCV_DTL As String = "UPDATE                                " & vbNewLine _
                                              & " $LM_TRN$..$RCV_DTL$                               " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " DEL_KB            = @DEL_KB                       " & vbNewLine _
                                              & ",DELETE_USER       = @DELETE_USER                  " & vbNewLine _
                                              & ",DELETE_DATE       = @DELETE_DATE                  " & vbNewLine _
                                              & ",DELETE_TIME       = @DELETE_TIME                  " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG       = @SYS_DEL_FLG                  " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine

    ''' <summary>
    ''' EDI受信(DTL)更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_EDITORIKESI_RCV_DTL_DICNEW As String = "UPDATE                                " & vbNewLine _
                                              & " $LM_TRN$..H_INOUTKAEDI_DTL_DIC_NEW                " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " DEL_KB            = @DEL_KB                       " & vbNewLine _
                                              & ",DELETE_USER       = @DELETE_USER                  " & vbNewLine _
                                              & ",DELETE_DATE       = @DELETE_DATE                  " & vbNewLine _
                                              & ",DELETE_TIME       = @DELETE_TIME                  " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG       = @SYS_DEL_FLG                  " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine


#End Region

    '大阪対応　20120322　Start  
#Region "RCV_EXT(EDI取消、EDI取消⇒未登録)"
    ''' <summary>
    ''' EDI受信(DTL)更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_EDITORIKESI_RCV_EXT As String = "UPDATE                                " & vbNewLine _
                                              & " $LM_TRN$..$RCV_DTL$                               " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " DEL_KB            = @DEL_KB                       " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG       = @SYS_DEL_FLG                  " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine


#End Region
    '大阪対応　20120322　End  

#End Region

#Region "実績取消処理 更新用SQL"

#Region "H_OUTKAEDI_L"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKIDELEDI_L As String = "UPDATE $LM_TRN$..H_OUTKAEDI_L SET        " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                    " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                    " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                    " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                   " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                  " & vbNewLine
#End Region

#Region "H_OUTKAEDI_HED"
    ''' <summary>
    ''' EDI受信HEDのUPDATE文（H_OUTKAEDI_HED）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKICANSEL_EDI_RCV_HED As String = "UPDATE $LM_TRN$..$RCV_HED$ SET  " & vbNewLine _
                                              & " UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                    " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                   " & vbNewLine

    Private Const SQL_UPDATE_WHERE_INOUT_KB As String = "AND                                    " & vbNewLine _
                                          & "INOUT_KB     = '0'                   " & vbNewLine

#End Region

#Region "H_OUTKAEDI_DTL"
    ''' <summary>
    ''' EDI受信DTLのUPDATE文（H_OUTKAEDI_DTL）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKICANSEL_EDI_RCV_DTL As String = "UPDATE $LM_TRN$..$RCV_DTL$ SET  " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG       	    " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                    " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                   " & vbNewLine



#End Region

#End Region

#Region "実績作成済⇒実績未,実績送信済⇒実績未処理 更新用SQL"

#Region "H_OUTKAEDI_L"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_L As String = "UPDATE $LM_TRN$..H_OUTKAEDI_L SET       " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                           " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                           " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                           " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                           " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                               " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                               " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                               " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                           " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                           " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                           " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                           " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                              " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                             " & vbNewLine
#End Region

#Region "H_OUTKAEDI_M"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_M As String = "UPDATE $LM_TRN$..H_OUTKAEDI_M SET       " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                           " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                           " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                           " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                           " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                               " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                               " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                               " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                           " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                           " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                           " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                           " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                              " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                             " & vbNewLine _
                                              & "AND JISSEKI_FLAG   <> '9'                                    " & vbNewLine _
                                              & "AND OUT_KB         = '0'                                     " & vbNewLine

#End Region

#Region "H_OUTKAEDI_HED"
    ''' <summary>
    ''' EDI受信HEDのUPDATE文（H_OUTKAEDI_HED）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_RCV_HED As String = "UPDATE $LM_TRN$..$RCV_HED$ SET       " & vbNewLine _
                                              & " UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                    " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                   " & vbNewLine _
                                              & "AND SYS_DEL_FLG    <> '1'                          " & vbNewLine

#End Region

#Region "H_OUTKAEDI_DTL"
    ''' <summary>
    ''' EDI受信DTLのUPDATE文（H_OUTKAEDI_DTL）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_RCV_DTL As String = "UPDATE $LM_TRN$..$RCV_DTL$ SET                " & vbNewLine _
                                                  & " JISSEKI_SHORI_FLG      = @JISSEKI_SHORI_FLG       	              " & vbNewLine _
                                                  & ",JISSEKI_USER           = @JISSEKI_USER                              " & vbNewLine _
                                                  & ",JISSEKI_DATE           = @JISSEKI_DATE                              " & vbNewLine _
                                                  & ",JISSEKI_TIME           = @JISSEKI_TIME                              " & vbNewLine _
                                                  & ",UPD_USER               = @UPD_USER                                  " & vbNewLine _
                                                  & ",UPD_DATE               = @UPD_DATE                                  " & vbNewLine _
                                                  & ",UPD_TIME               = @UPD_TIME                                  " & vbNewLine _
                                                  & ",SYS_UPD_DATE           = @SYS_UPD_DATE                              " & vbNewLine _
                                                  & ",SYS_UPD_TIME           = @SYS_UPD_TIME                              " & vbNewLine _
                                                  & ",SYS_UPD_PGID           = @SYS_UPD_PGID                              " & vbNewLine _
                                                  & ",SYS_UPD_USER           = @SYS_UPD_USER                              " & vbNewLine _
                                                  & "WHERE   NRS_BR_CD       = @NRS_BR_CD                                 " & vbNewLine _
                                                  & "AND EDI_CTL_NO          = @EDI_CTL_NO                                " & vbNewLine _
                                                  & "AND JISSEKI_SHORI_FLG   = '2'      				                  " & vbNewLine _
                                                  & "AND SYS_DEL_FLG         <> '1'      				                  " & vbNewLine


    Private Const SQL_UPDATE_JISSEKISOUSINZUMI_JISSEKIMI_EDI_RCV_DTL As String = "UPDATE $LM_TRN$..$RCV_DTL$ SET                    " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG          = @JISSEKI_SHORI_FLG       	                        " & vbNewLine _
                                              & ",JISSEKI_USER               = @JISSEKI_USER                                        " & vbNewLine _
                                              & ",JISSEKI_DATE               = @JISSEKI_DATE                                        " & vbNewLine _
                                              & ",JISSEKI_TIME               = @JISSEKI_TIME                                        " & vbNewLine _
                                              & ",SEND_USER                  = @SEND_USER                                           " & vbNewLine _
                                              & ",SEND_DATE                  = @SEND_DATE                                           " & vbNewLine _
                                              & ",SEND_TIME                  = @SEND_TIME                                           " & vbNewLine _
                                              & ",UPD_USER                   = @UPD_USER                                            " & vbNewLine _
                                              & ",UPD_DATE                   = @UPD_DATE                                            " & vbNewLine _
                                              & ",UPD_TIME                   = @UPD_TIME                                            " & vbNewLine _
                                              & ",SYS_UPD_DATE               = @SYS_UPD_DATE                                        " & vbNewLine _
                                              & ",SYS_UPD_TIME               = @SYS_UPD_TIME                                        " & vbNewLine _
                                              & ",SYS_UPD_PGID               = @SYS_UPD_PGID                                        " & vbNewLine _
                                              & ",SYS_UPD_USER               = @SYS_UPD_USER                                        " & vbNewLine _
                                              & "WHERE   NRS_BR_CD           = @NRS_BR_CD                                           " & vbNewLine _
                                              & "AND EDI_CTL_NO              = @EDI_CTL_NO                                          " & vbNewLine _
                                              & "AND JISSEKI_SHORI_FLG       = '3'                                                  " & vbNewLine _
                                              & "AND SYS_DEL_FLG             <> '1'                                                 " & vbNewLine

    '2012/06/06 本明 住化カラー対応  Start
    Private Const SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_RCV_DTL_SMK As String = "UPDATE $LM_TRN$..$RCV_DTL$ SET                " & vbNewLine _
                                            & " JISSEKI_SHORI_FLG      = @JISSEKI_SHORI_FLG       	              " & vbNewLine _
                                            & ",JISSEKI_USER           = @JISSEKI_USER                              " & vbNewLine _
                                            & ",JISSEKI_DATE           = @JISSEKI_DATE                              " & vbNewLine _
                                            & ",JISSEKI_TIME           = @JISSEKI_TIME                              " & vbNewLine _
                                            & ",UPD_USER               = @UPD_USER                                  " & vbNewLine _
                                            & ",UPD_DATE               = @UPD_DATE                                  " & vbNewLine _
                                            & ",UPD_TIME               = @UPD_TIME                                  " & vbNewLine _
                                            & ",SYS_UPD_DATE           = @SYS_UPD_DATE                              " & vbNewLine _
                                            & ",SYS_UPD_TIME           = @SYS_UPD_TIME                              " & vbNewLine _
                                            & ",SYS_UPD_PGID           = @SYS_UPD_PGID                              " & vbNewLine _
                                            & ",SYS_UPD_USER           = @SYS_UPD_USER                              " & vbNewLine _
                                            & ",YUSO_SHUDAN            = ''                                         " & vbNewLine _
                                            & ",YUSO_GYOSHA            = ''                                         " & vbNewLine _
                                            & ",YUSO_JIS_CD            = ''                                         " & vbNewLine _
                                            & ",JISSEKI_KBN            = ''                                         " & vbNewLine _
                                            & "WHERE   NRS_BR_CD       = @NRS_BR_CD                                 " & vbNewLine _
                                            & "AND EDI_CTL_NO          = @EDI_CTL_NO                                " & vbNewLine _
                                            & "AND JISSEKI_SHORI_FLG   = '2'      				                    " & vbNewLine _
                                            & "AND SYS_DEL_FLG         <> '1'      				                    " & vbNewLine


    Private Const SQL_UPDATE_JISSEKISOUSINZUMI_JISSEKIMI_EDI_RCV_DTL_SMK As String = "UPDATE $LM_TRN$..$RCV_DTL$ SET                    " & vbNewLine _
                                            & " JISSEKI_SHORI_FLG       = @JISSEKI_SHORI_FLG       	                        " & vbNewLine _
                                            & ",JISSEKI_USER            = @JISSEKI_USER                                        " & vbNewLine _
                                            & ",JISSEKI_DATE            = @JISSEKI_DATE                                        " & vbNewLine _
                                            & ",JISSEKI_TIME            = @JISSEKI_TIME                                        " & vbNewLine _
                                            & ",SEND_USER               = @SEND_USER                                           " & vbNewLine _
                                            & ",SEND_DATE               = @SEND_DATE                                           " & vbNewLine _
                                            & ",SEND_TIME               = @SEND_TIME                                           " & vbNewLine _
                                            & ",UPD_USER                = @UPD_USER                                            " & vbNewLine _
                                            & ",UPD_DATE                = @UPD_DATE                                            " & vbNewLine _
                                            & ",UPD_TIME                = @UPD_TIME                                            " & vbNewLine _
                                            & ",SYS_UPD_DATE            = @SYS_UPD_DATE                                        " & vbNewLine _
                                            & ",SYS_UPD_TIME            = @SYS_UPD_TIME                                        " & vbNewLine _
                                            & ",SYS_UPD_PGID            = @SYS_UPD_PGID                                        " & vbNewLine _
                                            & ",SYS_UPD_USER            = @SYS_UPD_USER                                        " & vbNewLine _
                                            & ",YUSO_SHUDAN             = ''                                         " & vbNewLine _
                                            & ",YUSO_GYOSHA             = ''                                         " & vbNewLine _
                                            & ",YUSO_JIS_CD             = ''                                         " & vbNewLine _
                                            & ",JISSEKI_KBN             = ''                                         " & vbNewLine _
                                            & "WHERE   NRS_BR_CD        = @NRS_BR_CD                                           " & vbNewLine _
                                            & "AND EDI_CTL_NO           = @EDI_CTL_NO                                          " & vbNewLine _
                                            & "AND JISSEKI_SHORI_FLG    = '3'                                                  " & vbNewLine _
                                            & "AND SYS_DEL_FLG          <> '1'                                                 " & vbNewLine
    '2012/06/06 本明 住化カラー対応  End



#End Region

#Region "H_SENDOUTEDI"
    ''' <summary>
    ''' EDI送信TBLのDELETE文（H_SENDOUTEDI）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_JISSEKIZUMI_JISSEKIMI_EDI_SEND As String = "DELETE $LM_TRN$..$SEND_TBL$   " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                       " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                      " & vbNewLine _
                                              & "AND SYS_DEL_FLG     <> '1'                            " & vbNewLine

#End Region

#Region "C_OUTKA_L"
    ''' <summary>
    ''' C_OUTKA_LのUPDATE文（C_OUTKA_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_OUTKA_L As String = "UPDATE $LM_TRN$..C_OUTKA_L SET  " & vbNewLine _
                                              & " OUTKA_STATE_KB    =                                   " & vbNewLine _
                                              & " CASE WHEN OUTKA_STATE_KB > @OUTKA_STATE_KB            " & vbNewLine _
                                              & " THEN @OUTKA_STATE_KB                                  " & vbNewLine _
                                              & " ELSE OUTKA_STATE_KB END                               " & vbNewLine _
                                              & ",HOKOKU_DATE       = @HOKOKU_DATE                      " & vbNewLine _
                                              & ",HOU_USER          = @HOU_USER                         " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                     " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                     " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                     " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                        " & vbNewLine _
                                              & "AND OUTKA_NO_L     = @OUTKA_NO_L                       " & vbNewLine _
                                              & "AND SYS_DEL_FLG     <> '1'                             " & vbNewLine
#End Region

#End Region

#Region "実績送信済⇒送信待処理 更新用SQL"

#Region "H_OUTKAEDI_L"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKIZUMI_SOUSINMACHI_EDI_L As String = "UPDATE $LM_TRN$..H_OUTKAEDI_L SET       " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                           " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                               " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                               " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                               " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                           " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                           " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                           " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                           " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                              " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                             " & vbNewLine
#End Region

#Region "H_OUTKAEDI_M"
    ''' <summary>
    ''' H_OUTKAEDI_MのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_JISSEKIZUMI_SOUSINMACHI_EDI_M As String = "UPDATE $LM_TRN$..H_OUTKAEDI_M SET       " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                           " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                               " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                               " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                               " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                           " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                           " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                           " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                           " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                              " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                             " & vbNewLine


#End Region

#Region "H_OUTKAEDI_HED"
    ''' <summary>
    ''' H_OUTKAEDI_HED(実績送信済⇒送信未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_SOUSINZUMI_SOUSINMI_RCV_HED As String = "UPDATE                           " & vbNewLine _
                                              & " $LM_TRN$..$RCV_HED$                               " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine


#End Region

#Region "H_OUTKAEDI_DTL"
    ''' <summary>
    ''' H_OUTKAEDI_DTL(実績送信済⇒送信待)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_SOUSINZUMI_SOUSINMI_RCV_DTL As String = "UPDATE                           " & vbNewLine _
                                              & " $LM_TRN$..$RCV_DTL$                               " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG            " & vbNewLine _
                                              & ",SEND_USER         = @SEND_USER                    " & vbNewLine _
                                              & ",SEND_DATE         = @SEND_DATE                    " & vbNewLine _
                                              & ",SEND_TIME         = @SEND_TIME                    " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG  = '3'                          " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine

#End Region

#Region "H_SENDOUTEDI"
    ''' <summary>
    ''' H_SENDOUTEDI(実績送信済⇒送信待)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKIMODOSI_SEND As String = "UPDATE                                    " & vbNewLine _
                                              & "$LM_TRN$..$SEND_TBL$                               " & vbNewLine _
                                              & "SET                                                " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG            " & vbNewLine _
                                              & ",SEND_USER         = @SEND_USER                    " & vbNewLine _
                                              & ",SEND_DATE         = @SEND_DATE                    " & vbNewLine _
                                              & ",SEND_TIME         = @SEND_TIME                    " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine


#End Region

#End Region

#Region "出荷取消⇒未登録"

    '共通化暫定対応START
#Region "出荷取消⇒未登録処理 同一まとめ番号データ取得用SQL"

    ''' <summary>
    ''' 同一まとめ番号データ取得用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_MATOMETORIKESI As String = " SELECT                                                      " & vbNewLine _
                                            & " H_OUTKAEDI_L.NRS_BR_CD                             AS NRS_BR_CD            " & vbNewLine _
                                            & ",H_OUTKAEDI_L.EDI_CTL_NO                            AS EDI_CTL_NO           " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SYS_UPD_DATE                          AS SYS_UPD_DATE         " & vbNewLine _
                                            & ",H_OUTKAEDI_L.SYS_UPD_TIME                          AS SYS_UPD_TIME         " & vbNewLine _
                                            & ",H_OUTKAEDI_L.OUTKA_CTL_NO                          AS OUTKA_CTL_NO         " & vbNewLine _
                                            & ",''                                                 AS RCV_SYS_UPD_DATE     " & vbNewLine _
                                            & ",''                                                 AS RCV_SYS_UPD_TIME     " & vbNewLine _
                                            & ",M_EDI_CUST.RCV_NM_HED                              AS RCV_NM_HED           " & vbNewLine _
                                            & ",M_EDI_CUST.RCV_NM_DTL                              AS RCV_NM_DTL           " & vbNewLine _
                                            & ",M_EDI_CUST.RCV_NM_EXT                              AS RCV_NM_EXT           " & vbNewLine _
                                            & ",M_EDI_CUST.SND_NM                                  AS SND_NM               " & vbNewLine _
                                            & ",M_EDI_CUST.FLAG_16                                 AS EDI_CUST_INOUTFLG    " & vbNewLine _
                                            & " FROM                                                                       " & vbNewLine _
                                            & " $LM_TRN$..H_OUTKAEDI_L                    H_OUTKAEDI_L                     " & vbNewLine _
                                            & " INNER JOIN                                                                 " & vbNewLine _
                                            & " $LM_TRN$..C_OUTKA_L                       C_OUTKA_L                        " & vbNewLine _
                                            & " ON                                                                         " & vbNewLine _
                                            & " H_OUTKAEDI_L.NRS_BR_CD =C_OUTKA_L.NRS_BR_CD                                " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.OUTKA_CTL_NO =C_OUTKA_L.OUTKA_NO_L                            " & vbNewLine _
                                            & " --INNER JOIN                                                               " & vbNewLine _
                                            & " --$LM_TRN$..$RCV_HED$                       RCV_HED                        " & vbNewLine _
                                            & " --ON                                                                       " & vbNewLine _
                                            & " --H_OUTKAEDI_L.NRS_BR_CD =RCV_HED.NRS_BR_CD                                " & vbNewLine _
                                            & " --AND                                                                      " & vbNewLine _
                                            & " --H_OUTKAEDI_L.EDI_CTL_NO =RCV_HED.EDI_CTL_NO                              " & vbNewLine _
                                            & " INNER JOIN                                                                 " & vbNewLine _
                                            & " $LM_MST$..M_EDI_CUST                       M_EDI_CUST                      " & vbNewLine _
                                            & " ON                                                                         " & vbNewLine _
                                            & " H_OUTKAEDI_L.NRS_BR_CD =M_EDI_CUST.NRS_BR_CD                               " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.WH_CD =M_EDI_CUST.WH_CD                                       " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.CUST_CD_L =M_EDI_CUST.CUST_CD_L                               " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.CUST_CD_M =M_EDI_CUST.CUST_CD_M                               " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " M_EDI_CUST.INOUT_KB = '0'                                                  " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " M_EDI_CUST.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                            & " WHERE                                                                      " & vbNewLine _
                                            & " SUBSTRING(H_OUTKAEDI_L.FREE_C30,4,9) = @MATOME_NO                          " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " C_OUTKA_L.OUTKA_NO_L = @OUTKA_CTL_NO                                       " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " H_OUTKAEDI_L.SYS_DEL_FLG = '0'                                             " & vbNewLine _
                                            & " AND                                                                        " & vbNewLine _
                                            & " C_OUTKA_L.SYS_DEL_FLG = '1'                                                " & vbNewLine _
                                            & " AND C_OUTKA_L.SYS_UPD_DATE = @OUTKA_SYS_UPD_DATE                           " & vbNewLine _
                                            & " AND C_OUTKA_L.SYS_UPD_TIME = @OUTKA_SYS_UPD_TIME                           " & vbNewLine


#End Region
    '共通化暫定対応END

    '共通化暫定対応START
#Region "H_OUTKAEDI_L"

    ''' <summary>
    ''' OUTKAEDI_L(出荷取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_TOUROKUMI_EDI_L As String = "UPDATE                           " & vbNewLine _
                                          & " $LM_TRN$..H_OUTKAEDI_L                             " & vbNewLine _
                                          & " SET                                               " & vbNewLine _
                                          & " OUTKA_CTL_NO      = ''                            " & vbNewLine _
                                          & ",OUT_FLAG          = @OUT_FLAG                     " & vbNewLine _
                                          & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                          & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                          & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                          & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                          & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                          & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                          & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                          & " WHERE                                             " & vbNewLine _
                                          & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                          & " AND                                               " & vbNewLine _
                                          & " OUTKA_CTL_NO      = @OUTKA_CTL_NO                   " & vbNewLine

    ''' <summary>
    ''' OUTKAEDI_L(出荷取消⇒未登録)まとめ戻しの場合
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_TOUROKUMI_EDI_L_MATOME As String = "UPDATE                           " & vbNewLine _
                                              & " $LM_TRN$..H_OUTKAEDI_L                             " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " OUTKA_CTL_NO      = ''                            " & vbNewLine _
                                              & ",FREE_C30          = ''                            " & vbNewLine _
                                              & ",OUT_FLAG          = @OUT_FLAG                     " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & "-- OUTKA_CTL_NO      = @OUTKA_CTL_NO                 " & vbNewLine _
                                              & " EDI_CTL_NO      = @EDI_CTL_NO                 " & vbNewLine
#End Region
    '共通化暫定対応END

#Region "H_OUTKAEDI_M"
    ''' <summary>
    ''' OUTKAEDI_M(出荷取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_TOUROKUMI_EDI_M As String = "UPDATE                                " & vbNewLine _
                                          & " $LM_TRN$..H_OUTKAEDI_M                            " & vbNewLine _
                                          & " SET                                               " & vbNewLine _
                                          & " OUTKA_CTL_NO      = ''                            " & vbNewLine _
                                          & ",OUTKA_CTL_NO_CHU  = ''                            " & vbNewLine _
                                          & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                          & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                          & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                          & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                          & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                          & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                          & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                          & " WHERE                                             " & vbNewLine _
                                          & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                          & " AND                                               " & vbNewLine _
                                          & " OUTKA_CTL_NO        = @OUTKA_CTL_NO               " & vbNewLine _
                                          & " AND                                               " & vbNewLine _
                                          & " OUT_KB            = '0'                           " & vbNewLine

    ''' <summary>
    ''' OUTKAEDI_M(出荷取消⇒未登録)まとめデータ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_TOUROKUMI_EDI_M_MATOME As String = "UPDATE                           " & vbNewLine _
                                              & " $LM_TRN$..H_OUTKAEDI_M                            " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " OUTKA_CTL_NO      = ''                            " & vbNewLine _
                                              & ",OUTKA_CTL_NO_CHU  = ''                            " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & "-- OUTKA_CTL_NO      = @OUTKA_CTL_NO                 " & vbNewLine _
                                              & " EDI_CTL_NO      = @EDI_CTL_NO                     " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " OUT_KB            = '0'                           " & vbNewLine

#End Region

    '共通化暫定対応START
#Region "H_OUTKAEDI_HED"
    ''' <summary>
    ''' H_OUTKAEDI_HED(出荷取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Const SQL_UPD_TOUROKUMI_RCV_HED As String = "UPDATE                                 " & vbNewLine _
                                          & " $LM_TRN$..$RCV_HED$                               " & vbNewLine _
                                          & " SET                                               " & vbNewLine _
                                          & " OUTKA_CTL_NO      = $BR_INITIAL$ + '00000000'     " & vbNewLine _
                                          & ",OUTKA_USER        = @OUTKA_USER                   " & vbNewLine _
                                          & ",OUTKA_DATE        = @OUTKA_DATE                   " & vbNewLine _
                                          & ",OUTKA_TIME        = @OUTKA_TIME                   " & vbNewLine _
                                          & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                          & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                          & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                          & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                          & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                          & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                          & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                          & " WHERE                                             " & vbNewLine _
                                          & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine

    Private Const SQL_WHERE_TOUROKUMI_NORMAL As String = "AND                                   " & vbNewLine _
                                          & " OUTKA_CTL_NO      = @OUTKA_CTL_NO                 " & vbNewLine

    Private Const SQL_WHERE_TOUROKUMI_MATOME As String = "AND                                   " & vbNewLine _
                                          & " EDI_CTL_NO      = @EDI_CTL_NO                 " & vbNewLine

#End Region
    '共通化暫定対応END

    '2012.02.25 大阪対応 START
#Region "H_OUTKAEDI_DTL"
    ''' <summary>
    ''' H_OUTKAEDI_DTL(出荷取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Const SQL_UPD_TOUROKUMI_RCV_DTL1 As String = "UPDATE                                " & vbNewLine _
                                          & " $LM_TRN$..$RCV_DTL$                               " & vbNewLine _
                                          & " SET                                               " & vbNewLine _
                                          & " OUTKA_CTL_NO      = $BR_INITIAL$ + '00000000'     " & vbNewLine _
                                          & ",OUTKA_CTL_NO_CHU  = '000'                         " & vbNewLine

    Private Const SQL_UPD_TOUROKUMI_RCV_DTL2 As String = ",OUTKA_USER          = @OUTKA_USER      " & vbNewLine _
                                          & ",OUTKA_DATE          = @OUTKA_DATE                   " & vbNewLine _
                                          & ",OUTKA_TIME          = @OUTKA_TIME                   " & vbNewLine

    Private Const SQL_UPD_TOUROKUMI_RCV_DTL3 As String = ",UPD_USER          = @UPD_USER        " & vbNewLine _
                                          & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                          & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                          & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                          & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                          & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                          & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                          & " WHERE                                             " & vbNewLine _
                                          & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine

#End Region
    '2012.02.25 大阪対応 END

#End Region

    '2012.04.04 大阪対応追加START
#Region "運送取消⇒未登録"

#Region "H_OUTKAEDI_L"

    ''' <summary>
    ''' OUTKAEDI_L(運送取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_UNSO_TOUROKUMI_EDI_L As String = "UPDATE                           " & vbNewLine _
                                          & " $LM_TRN$..H_OUTKAEDI_L                            " & vbNewLine _
                                          & " SET                                               " & vbNewLine _
                                          & " FREE_C30          = '01-' + $BR_INITIAL$ + '00000000'     " & vbNewLine _
                                          & ",OUT_FLAG          = @OUT_FLAG                     " & vbNewLine _
                                          & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                          & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                          & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                          & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                          & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                          & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                          & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                          & " WHERE                                             " & vbNewLine _
                                          & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                          & " AND                                               " & vbNewLine _
                                          & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine

#End Region

#Region "H_OUTKAEDI_M"
    ''' <summary>
    ''' OUTKAEDI_M(運送取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_UNSO_TOUROKUMI_EDI_M As String = "UPDATE                           " & vbNewLine _
                                          & " $LM_TRN$..H_OUTKAEDI_M                            " & vbNewLine _
                                          & " SET                                               " & vbNewLine _
                                          & " FREE_C30          = '01-' + $BR_INITIAL$ + '00000000000'  " & vbNewLine _
                                          & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                          & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                          & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                          & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                          & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                          & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                          & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                          & " WHERE                                             " & vbNewLine _
                                          & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                          & " AND                                               " & vbNewLine _
                                          & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine _
                                          & " AND                                               " & vbNewLine _
                                          & " OUT_KB            = '0'                           " & vbNewLine

#End Region

#Region "H_OUTKAEDI_HED"
    ''' <summary>
    ''' H_OUTKAEDI_HED(運送取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Const SQL_UPD_UNSO_TOUROKUMI_RCV_HED As String = "UPDATE                            " & vbNewLine _
                                          & " $LM_TRN$..$RCV_HED$                               " & vbNewLine _
                                          & " SET                                               " & vbNewLine _
                                          & " UPD_USER          = @UPD_USER                     " & vbNewLine _
                                          & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                          & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                          & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                          & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                          & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                          & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                          & " WHERE                                             " & vbNewLine _
                                          & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                          & " AND                                               " & vbNewLine _
                                          & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine

#End Region

#Region "H_OUTKAEDI_DTL"
    ''' <summary>
    ''' H_OUTKAEDI_DTL(運送取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Const SQL_UPD_UNSO_TOUROKUMI_RCV_DTL As String = "UPDATE                            " & vbNewLine _
                                          & " $LM_TRN$..$RCV_DTL$                               " & vbNewLine _
                                          & " SET                                               " & vbNewLine _
                                          & " UPD_USER          = @UPD_USER                     " & vbNewLine _
                                          & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                          & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                          & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                          & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                          & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                          & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                          & " WHERE                                             " & vbNewLine _
                                          & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                          & " AND                                               " & vbNewLine _
                                          & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine

#End Region
    '2012.04.04 大阪対応追加END

#End Region

    'ディック（群馬）追加箇所 2012.07.11 terakawa Start
#Region "出荷登録処理 まとめ先取得SQL(まとめF=1)"
    Private Const SQL_SELECT_MATOME_TARGET_FLAG1 As String = "SELECT                                 " & vbNewLine _
                                    & " QUERY.OUTKA_CTL_NO                                           " & vbNewLine _
                                    & ",QUERY.OUTKA_CTL_NO_CHU                                       " & vbNewLine _
                                    & ",QUERY.EDI_CTL_NO                                             " & vbNewLine _
                                    & ",QUERY.SYS_UPD_DATE                                           " & vbNewLine _
                                    & ",QUERY.SYS_UPD_TIME                                           " & vbNewLine _
                                    & ",QUERY.UNSO_NO_L                                              " & vbNewLine _
                                    & ",QUERY.UNSO_NO_M                                              " & vbNewLine _
                                    & ",QUERY.SYS_UNSO_UPD_DATE                                      " & vbNewLine _
                                    & ",QUERY.SYS_UNSO_UPD_TIME                                      " & vbNewLine _
                                    & ",QUERY.OUTKA_PKG_NB                                           " & vbNewLine _
                                    & ",QUERY.UNSO_PKG_NB                                            " & vbNewLine _
                                    & ",QUERY.NRS_BR_CD                                              " & vbNewLine _
                                    & ",QUERY.NHS_REMARK                                             " & vbNewLine _
                                    & ",QUERY.REMARK                                                 " & vbNewLine _
                                    & ",QUERY.BUYER_ORD_NO                                           " & vbNewLine _
                                    & ",QUERY.CUST_ORD_NO                                            " & vbNewLine _
                                    & ",QUERY.OUTKA_STATE_KB_1 AS OUTKA_STATE_KB                     " & vbNewLine _
                                    & " FROM                                                         " & vbNewLine _
                                    & " (SELECT                                                      " & vbNewLine _
                                    & "    OUTKA_L.NRS_BR_CD             AS  NRS_BR_CD               " & vbNewLine _
                                    & "	   ,OUTKA_L.OUTKA_NO_L           AS  OUTKA_CTL_NO            " & vbNewLine _
                                    & "    ,MAX(OUTKA_M.OUTKA_NO_M)      AS  OUTKA_CTL_NO_CHU        " & vbNewLine _
                                    & "    ,CASE WHEN (SUBSTRING(EDI_L.FREE_C30,4,9) <> ''           " & vbNewLine _
                                    & "     AND SUBSTRING(EDI_L.FREE_C30,1,2) = '04')                " & vbNewLine _
                                    & "     THEN SUBSTRING(EDI_L.FREE_C30,4,9)                       " & vbNewLine _
                                    & "     ELSE EDI_L.EDI_CTL_NO                                    " & vbNewLine _
                                    & "     END  EDI_CTL_NO                                          " & vbNewLine _
                                    & "    ,OUTKA_L.SYS_UPD_DATE         AS  SYS_UPD_DATE            " & vbNewLine _
                                    & "    ,OUTKA_L.SYS_UPD_TIME         AS  SYS_UPD_TIME            " & vbNewLine _
                                    & "    ,UNSO_L.UNSO_NO_L             AS  UNSO_NO_L               " & vbNewLine _
                                    & "    ,MAX(UNSO_M.UNSO_NO_M)        AS  UNSO_NO_M               " & vbNewLine _
                                    & "    ,UNSO_L.SYS_UPD_DATE          AS  SYS_UNSO_UPD_DATE       " & vbNewLine _
                                    & "    ,UNSO_L.SYS_UPD_TIME          AS  SYS_UNSO_UPD_TIME       " & vbNewLine _
                                    & "    ,OUTKA_L.OUTKA_PKG_NB         AS  OUTKA_PKG_NB            " & vbNewLine _
                                    & "    ,UNSO_L.UNSO_PKG_NB           AS  UNSO_PKG_NB             " & vbNewLine _
                                    & "    ,OUTKA_L.NHS_REMARK           AS  NHS_REMARK              " & vbNewLine _
                                    & "    ,OUTKA_L.REMARK               AS  REMARK                  " & vbNewLine _
                                    & "    ,OUTKA_L.BUYER_ORD_NO         AS  BUYER_ORD_NO            " & vbNewLine _
                                    & "    ,OUTKA_L.CUST_ORD_NO          AS  CUST_ORD_NO             " & vbNewLine _
                                    & "    ,CASE MAX(ISNULL(OUTKA_L.OUTKA_STATE_KB,''))              " & vbNewLine _
                                    & "    WHEN '' THEN '99'                                         " & vbNewLine _
                                    & "    WHEN '10' THEN '10'                                       " & vbNewLine _
                                    & "    WHEN '60' THEN '60'                                       " & vbNewLine _
                                    & "    WHEN '90' THEN '90'                                       " & vbNewLine _
                                    & "    ELSE '50'                                                 " & vbNewLine _
                                    & "   END AS OUTKA_STATE_KB_1                                    " & vbNewLine _
                                    & "FROM                                                          " & vbNewLine _
                                    & " $LM_TRN$..C_OUTKA_L           OUTKA_L                        " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..C_OUTKA_M           OUTKA_M                        " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = OUTKA_M.OUTKA_NO_L                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = OUTKA_M.NRS_BR_CD                        " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_M.SYS_DEL_FLG  = '0'                                   " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..F_UNSO_L            UNSO_L                         " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = UNSO_L.INOUTKA_NO_L                     " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = UNSO_L.NRS_BR_CD                         " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.MOTO_DATA_KB = '20'                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..F_UNSO_M            UNSO_M                         " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " UNSO_L.UNSO_NO_L = UNSO_M.UNSO_NO_L                          " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = UNSO_M.NRS_BR_CD                         " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_M.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..H_OUTKAEDI_L        EDI_L                          " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = EDI_L.OUTKA_CTL_NO                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = EDI_L.NRS_BR_CD                          " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " EDI_L.SYS_DEL_FLG = '0'                                      " & vbNewLine

    Private Const SQL_WHERE_MATOME_TARGET_FLAG1 As String = "WHERE                                   " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD  = @NRS_BR_CD                              " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.OUTKA_PLAN_DATE  = @OUTKA_PLAN_DATE                  " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.OUTKO_DATE  = @OUTKO_DATE                            " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.ARR_PLAN_DATE  = @ARR_PLAN_DATE                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " --(2012.09.19)引当以上のステータスはまとめない               " & vbNewLine _
                                    & " OUTKA_L.OUTKA_STATE_KB  < '40'                               " & vbNewLine _
                                    & " --OUTKA_L.OUTKA_STATE_KB  < '60'                             " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.SYS_DEL_FLG  = '0'                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.UNSO_CD  = @UNSO_CD                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.UNSO_BR_CD  = @UNSO_BR_CD                             " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.CUST_CD_L  = @CUST_CD_L                              " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.CUST_CD_M  = @CUST_CD_M                              " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.BIN_KB  = @BIN_KB                                     " & vbNewLine

    Private Const SQL_GROUPBY_MATOME_TARGET_FLAG1 As String = "GROUP BY                                 " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD                                               " & vbNewLine _
                                    & ",OUTKA_L.OUTKA_NO_L                                              " & vbNewLine _
                                    & ",CASE WHEN (SUBSTRING(EDI_L.FREE_C30,4,9) <> ''                  " & vbNewLine _
                                    & "      AND SUBSTRING(EDI_L.FREE_C30,1,2) = '04')                  " & vbNewLine _
                                    & "      THEN SUBSTRING(EDI_L.FREE_C30,4,9)                         " & vbNewLine _
                                    & "      ELSE EDI_L.EDI_CTL_NO                                      " & vbNewLine _
                                    & "      END                                                        " & vbNewLine _
                                    & ",OUTKA_L.SYS_UPD_DATE                                            " & vbNewLine _
                                    & ",OUTKA_L.SYS_UPD_TIME                                            " & vbNewLine _
                                    & ",UNSO_L.UNSO_NO_L                                                " & vbNewLine _
                                    & ",UNSO_L.UNSO_PKG_NB                                              " & vbNewLine _
                                    & ",UNSO_L.SYS_UPD_DATE                                             " & vbNewLine _
                                    & ",UNSO_L.SYS_UPD_TIME                                             " & vbNewLine _
                                    & ",OUTKA_L.OUTKA_PKG_NB                                            " & vbNewLine _
                                    & ",OUTKA_L.NHS_REMARK                                              " & vbNewLine _
                                    & ",OUTKA_L.REMARK                                                  " & vbNewLine _
                                    & ",OUTKA_L.BUYER_ORD_NO                                            " & vbNewLine _
                                    & ",OUTKA_L.CUST_ORD_NO                                             " & vbNewLine _
                                    & " ) QUERY     "

#End Region
    'ディック（群馬）追加箇所 2012.07.11 terakawa End

    '2012.03.07 要望番号851 修正START
    'まとめ先SQLの修正(共通SQL)START(2012.02.28 修正START)
#Region "出荷登録処理 まとめ先取得SQL(まとめF=2)"
    Private Const SQL_SELECT_MATOME_TARGET_FLAG2 As String = "SELECT                                 " & vbNewLine _
                                    & " QUERY.OUTKA_CTL_NO                                           " & vbNewLine _
                                    & ",QUERY.OUTKA_CTL_NO_CHU                                       " & vbNewLine _
                                    & ",QUERY.EDI_CTL_NO                                             " & vbNewLine _
                                    & ",QUERY.SYS_UPD_DATE                                           " & vbNewLine _
                                    & ",QUERY.SYS_UPD_TIME                                           " & vbNewLine _
                                    & ",QUERY.UNSO_NO_L                                              " & vbNewLine _
                                    & ",QUERY.UNSO_NO_M                                              " & vbNewLine _
                                    & ",QUERY.SYS_UNSO_UPD_DATE                                      " & vbNewLine _
                                    & ",QUERY.SYS_UNSO_UPD_TIME                                      " & vbNewLine _
                                    & ",QUERY.OUTKA_PKG_NB                                           " & vbNewLine _
                                    & ",QUERY.UNSO_PKG_NB                                            " & vbNewLine _
                                    & ",QUERY.NRS_BR_CD                                              " & vbNewLine _
                                    & ",QUERY.NHS_REMARK                                             " & vbNewLine _
                                    & ",QUERY.REMARK                                                 " & vbNewLine _
                                    & ",QUERY.BUYER_ORD_NO                                           " & vbNewLine _
                                    & " -- 要望番号922 追加START                                     " & vbNewLine _
                                    & ",QUERY.CUST_ORD_NO                                            " & vbNewLine _
                                    & " -- 要望番号922 追加END                                       " & vbNewLine _
                                    & ",QUERY.OUTKA_STATE_KB_1 AS OUTKA_STATE_KB                     " & vbNewLine _
                                    & " FROM                                                         " & vbNewLine _
                                    & " (SELECT                                                      " & vbNewLine _
                                    & "    OUTKA_L.NRS_BR_CD             AS  NRS_BR_CD               " & vbNewLine _
                                    & "	   ,OUTKA_L.OUTKA_NO_L           AS  OUTKA_CTL_NO            " & vbNewLine _
                                    & "    ,MAX(OUTKA_M.OUTKA_NO_M)      AS  OUTKA_CTL_NO_CHU        " & vbNewLine _
                                    & "    ,CASE WHEN (SUBSTRING(EDI_L.FREE_C30,4,9) <> ''           " & vbNewLine _
                                    & "     AND SUBSTRING(EDI_L.FREE_C30,1,2) = '04')                " & vbNewLine _
                                    & "     THEN SUBSTRING(EDI_L.FREE_C30,4,9)                       " & vbNewLine _
                                    & "     ELSE EDI_L.EDI_CTL_NO                                    " & vbNewLine _
                                    & "     END  EDI_CTL_NO                                          " & vbNewLine _
                                    & "    ,OUTKA_L.SYS_UPD_DATE         AS  SYS_UPD_DATE            " & vbNewLine _
                                    & "    ,OUTKA_L.SYS_UPD_TIME         AS  SYS_UPD_TIME            " & vbNewLine _
                                    & "    ,UNSO_L.UNSO_NO_L             AS  UNSO_NO_L               " & vbNewLine _
                                    & "    ,MAX(UNSO_M.UNSO_NO_M)        AS  UNSO_NO_M               " & vbNewLine _
                                    & "    ,UNSO_L.SYS_UPD_DATE          AS  SYS_UNSO_UPD_DATE       " & vbNewLine _
                                    & "    ,UNSO_L.SYS_UPD_TIME          AS  SYS_UNSO_UPD_TIME       " & vbNewLine _
                                    & "    ,OUTKA_L.OUTKA_PKG_NB         AS  OUTKA_PKG_NB            " & vbNewLine _
                                    & "    ,UNSO_L.UNSO_PKG_NB           AS  UNSO_PKG_NB             " & vbNewLine _
                                    & "    ,OUTKA_L.NHS_REMARK           AS  NHS_REMARK              " & vbNewLine _
                                    & "    ,OUTKA_L.REMARK               AS  REMARK                  " & vbNewLine _
                                    & "    ,OUTKA_L.BUYER_ORD_NO         AS  BUYER_ORD_NO            " & vbNewLine _
                                    & " -- 要望番号922 追加START                                     " & vbNewLine _
                                    & "    ,OUTKA_L.CUST_ORD_NO          AS  CUST_ORD_NO             " & vbNewLine _
                                    & " -- 要望番号922 追加END                                       " & vbNewLine _
                                    & "    ,CASE MAX(ISNULL(OUTKA_L.OUTKA_STATE_KB,''))              " & vbNewLine _
                                    & "    WHEN '' THEN '99'                                         " & vbNewLine _
                                    & "    WHEN '10' THEN '10'                                       " & vbNewLine _
                                    & "    WHEN '60' THEN '60'                                       " & vbNewLine _
                                    & "    WHEN '90' THEN '90'                                       " & vbNewLine _
                                    & "    ELSE '50'                                                 " & vbNewLine _
                                    & "   END AS OUTKA_STATE_KB_1                                    " & vbNewLine _
                                    & "FROM                                                          " & vbNewLine _
                                    & " $LM_TRN$..C_OUTKA_L           OUTKA_L                        " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..C_OUTKA_M           OUTKA_M                        " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = OUTKA_M.OUTKA_NO_L                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = OUTKA_M.NRS_BR_CD                        " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_M.SYS_DEL_FLG  = '0'                                   " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..F_UNSO_L            UNSO_L                         " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = UNSO_L.INOUTKA_NO_L                     " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = UNSO_L.NRS_BR_CD                         " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.MOTO_DATA_KB = '20'                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..F_UNSO_M            UNSO_M                         " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " UNSO_L.UNSO_NO_L = UNSO_M.UNSO_NO_L                          " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = UNSO_M.NRS_BR_CD                         " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_M.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..H_OUTKAEDI_L        EDI_L                          " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = EDI_L.OUTKA_CTL_NO                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = EDI_L.NRS_BR_CD                          " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " EDI_L.SYS_DEL_FLG = '0'                                      " & vbNewLine

    Private Const SQL_WHERE_MATOME_TARGET_FLAG2 As String = "WHERE                                   " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD  = @NRS_BR_CD                              " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.OUTKA_PLAN_DATE  = @OUTKA_PLAN_DATE                  " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.OUTKO_DATE  = @OUTKO_DATE                            " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.ARR_PLAN_DATE  = @ARR_PLAN_DATE                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.OUTKA_STATE_KB  < '60'                               " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.SYS_DEL_FLG  = '0'                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.UNSO_CD  = @UNSO_CD                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.UNSO_BR_CD  = @UNSO_BR_CD                             " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.CUST_CD_L  = @CUST_CD_L                              " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.CUST_CD_M  = @CUST_CD_M                              " & vbNewLine

    Private Const SQL_GROUPBY_MATOME_TARGET_FLAG2 As String = "GROUP BY                                 " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD                                               " & vbNewLine _
                                    & ",OUTKA_L.OUTKA_NO_L                                              " & vbNewLine _
                                    & ",CASE WHEN (SUBSTRING(EDI_L.FREE_C30,4,9) <> ''                  " & vbNewLine _
                                    & "      AND SUBSTRING(EDI_L.FREE_C30,1,2) = '04')                  " & vbNewLine _
                                    & "      THEN SUBSTRING(EDI_L.FREE_C30,4,9)                         " & vbNewLine _
                                    & "      ELSE EDI_L.EDI_CTL_NO                                      " & vbNewLine _
                                    & "      END                                                        " & vbNewLine _
                                    & ",OUTKA_L.SYS_UPD_DATE                                            " & vbNewLine _
                                    & ",OUTKA_L.SYS_UPD_TIME                                            " & vbNewLine _
                                    & ",UNSO_L.UNSO_NO_L                                                " & vbNewLine _
                                    & ",UNSO_L.UNSO_PKG_NB                                              " & vbNewLine _
                                    & ",UNSO_L.SYS_UPD_DATE                                             " & vbNewLine _
                                    & ",UNSO_L.SYS_UPD_TIME                                             " & vbNewLine _
                                    & ",OUTKA_L.OUTKA_PKG_NB                                            " & vbNewLine _
                                    & ",OUTKA_L.NHS_REMARK                                              " & vbNewLine _
                                    & ",OUTKA_L.REMARK                                                  " & vbNewLine _
                                    & ",OUTKA_L.BUYER_ORD_NO                                            " & vbNewLine _
                                    & " -- 要望番号922 追加START                                        " & vbNewLine _
                                    & ",OUTKA_L.CUST_ORD_NO                                             " & vbNewLine _
                                    & " -- 要望番号922 追加END                                          " & vbNewLine _
                                    & " ) QUERY     "

#End Region
    'まとめ先SQLの修正(共通SQL)END(2012.02.28 修正END)
    '2012.03.07 要望番号851 修正END

    '2012.10.02 日産物流 対応START
#Region "出荷登録処理 まとめ先取得SQL(まとめF=3)"
    Private Const SQL_SELECT_MATOME_TARGET_FLAG3 As String = "SELECT                                 " & vbNewLine _
                                    & " QUERY.OUTKA_CTL_NO                                           " & vbNewLine _
                                    & ",QUERY.OUTKA_CTL_NO_CHU                                       " & vbNewLine _
                                    & ",QUERY.EDI_CTL_NO                                             " & vbNewLine _
                                    & ",QUERY.SYS_UPD_DATE                                           " & vbNewLine _
                                    & ",QUERY.SYS_UPD_TIME                                           " & vbNewLine _
                                    & ",QUERY.UNSO_NO_L                                              " & vbNewLine _
                                    & ",QUERY.UNSO_NO_M                                              " & vbNewLine _
                                    & ",QUERY.SYS_UNSO_UPD_DATE                                      " & vbNewLine _
                                    & ",QUERY.SYS_UNSO_UPD_TIME                                      " & vbNewLine _
                                    & ",QUERY.OUTKA_PKG_NB                                           " & vbNewLine _
                                    & ",QUERY.UNSO_PKG_NB                                            " & vbNewLine _
                                    & ",QUERY.NRS_BR_CD                                              " & vbNewLine _
                                    & ",QUERY.NHS_REMARK                                             " & vbNewLine _
                                    & ",QUERY.REMARK                                                 " & vbNewLine _
                                    & ",QUERY.BUYER_ORD_NO                                           " & vbNewLine _
                                    & ",QUERY.CUST_ORD_NO                                            " & vbNewLine _
                                    & ",QUERY.OUTKA_STATE_KB_1 AS OUTKA_STATE_KB                     " & vbNewLine _
                                    & " FROM                                                         " & vbNewLine _
                                    & " (SELECT                                                      " & vbNewLine _
                                    & "    OUTKA_L.NRS_BR_CD             AS  NRS_BR_CD               " & vbNewLine _
                                    & "	   ,OUTKA_L.OUTKA_NO_L           AS  OUTKA_CTL_NO            " & vbNewLine _
                                    & "    ,MAX(OUTKA_M.OUTKA_NO_M)      AS  OUTKA_CTL_NO_CHU        " & vbNewLine _
                                    & "    ,CASE WHEN (SUBSTRING(EDI_L.FREE_C30,4,9) <> ''           " & vbNewLine _
                                    & "     AND SUBSTRING(EDI_L.FREE_C30,1,2) = '04')                " & vbNewLine _
                                    & "     THEN SUBSTRING(EDI_L.FREE_C30,4,9)                       " & vbNewLine _
                                    & "     ELSE EDI_L.EDI_CTL_NO                                    " & vbNewLine _
                                    & "     END  EDI_CTL_NO                                          " & vbNewLine _
                                    & "    ,OUTKA_L.SYS_UPD_DATE         AS  SYS_UPD_DATE            " & vbNewLine _
                                    & "    ,OUTKA_L.SYS_UPD_TIME         AS  SYS_UPD_TIME            " & vbNewLine _
                                    & "    ,UNSO_L.UNSO_NO_L             AS  UNSO_NO_L               " & vbNewLine _
                                    & "    ,MAX(UNSO_M.UNSO_NO_M)        AS  UNSO_NO_M               " & vbNewLine _
                                    & "    ,UNSO_L.SYS_UPD_DATE          AS  SYS_UNSO_UPD_DATE       " & vbNewLine _
                                    & "    ,UNSO_L.SYS_UPD_TIME          AS  SYS_UNSO_UPD_TIME       " & vbNewLine _
                                    & "    ,OUTKA_L.OUTKA_PKG_NB         AS  OUTKA_PKG_NB            " & vbNewLine _
                                    & "    ,UNSO_L.UNSO_PKG_NB           AS  UNSO_PKG_NB             " & vbNewLine _
                                    & "    ,OUTKA_L.NHS_REMARK           AS  NHS_REMARK              " & vbNewLine _
                                    & "    ,OUTKA_L.REMARK               AS  REMARK                  " & vbNewLine _
                                    & "    ,OUTKA_L.BUYER_ORD_NO         AS  BUYER_ORD_NO            " & vbNewLine _
                                    & "    ,OUTKA_L.CUST_ORD_NO          AS  CUST_ORD_NO             " & vbNewLine _
                                    & "    ,CASE MAX(ISNULL(OUTKA_L.OUTKA_STATE_KB,''))              " & vbNewLine _
                                    & "    WHEN '' THEN '99'                                         " & vbNewLine _
                                    & "    WHEN '10' THEN '10'                                       " & vbNewLine _
                                    & "    WHEN '60' THEN '60'                                       " & vbNewLine _
                                    & "    WHEN '90' THEN '90'                                       " & vbNewLine _
                                    & "    ELSE '50'                                                 " & vbNewLine _
                                    & "   END AS OUTKA_STATE_KB_1                                    " & vbNewLine _
                                    & "FROM                                                          " & vbNewLine _
                                    & " $LM_TRN$..C_OUTKA_L           OUTKA_L                        " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..C_OUTKA_M           OUTKA_M                        " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = OUTKA_M.OUTKA_NO_L                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = OUTKA_M.NRS_BR_CD                        " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_M.SYS_DEL_FLG  = '0'                                   " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..F_UNSO_L            UNSO_L                         " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = UNSO_L.INOUTKA_NO_L                     " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = UNSO_L.NRS_BR_CD                         " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.MOTO_DATA_KB = '20'                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..F_UNSO_M            UNSO_M                         " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " UNSO_L.UNSO_NO_L = UNSO_M.UNSO_NO_L                          " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = UNSO_M.NRS_BR_CD                         " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_M.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..H_OUTKAEDI_L        EDI_L                          " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = EDI_L.OUTKA_CTL_NO                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = EDI_L.NRS_BR_CD                          " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " EDI_L.SYS_DEL_FLG = '0'                                      " & vbNewLine

    Private Const SQL_WHERE_MATOME_TARGET_FLAG3 As String = "WHERE                                   " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD  = @NRS_BR_CD                              " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.OUTKA_PLAN_DATE  = @OUTKA_PLAN_DATE                  " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.OUTKO_DATE  = @OUTKO_DATE                            " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.ARR_PLAN_DATE  = @ARR_PLAN_DATE                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.OUTKA_STATE_KB  = '10'                               " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.SYS_DEL_FLG  = '0'                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.UNSO_CD  = @UNSO_CD                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.UNSO_BR_CD  = @UNSO_BR_CD                             " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.CUST_CD_L  = @CUST_CD_L                              " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.CUST_CD_M  = @CUST_CD_M                              " & vbNewLine _
                                    & "-- AND                                                          " & vbNewLine _
                                    & "-- EDI_L.UNSO_ATT  = @UNSO_ATT                                  " & vbNewLine

    Private Const SQL_GROUPBY_MATOME_TARGET_FLAG3 As String = "GROUP BY                                 " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD                                               " & vbNewLine _
                                    & ",OUTKA_L.OUTKA_NO_L                                              " & vbNewLine _
                                    & ",CASE WHEN (SUBSTRING(EDI_L.FREE_C30,4,9) <> ''                  " & vbNewLine _
                                    & "      AND SUBSTRING(EDI_L.FREE_C30,1,2) = '04')                  " & vbNewLine _
                                    & "      THEN SUBSTRING(EDI_L.FREE_C30,4,9)                         " & vbNewLine _
                                    & "      ELSE EDI_L.EDI_CTL_NO                                      " & vbNewLine _
                                    & "      END                                                        " & vbNewLine _
                                    & ",OUTKA_L.SYS_UPD_DATE                                            " & vbNewLine _
                                    & ",OUTKA_L.SYS_UPD_TIME                                            " & vbNewLine _
                                    & ",UNSO_L.UNSO_NO_L                                                " & vbNewLine _
                                    & ",UNSO_L.UNSO_PKG_NB                                              " & vbNewLine _
                                    & ",UNSO_L.SYS_UPD_DATE                                             " & vbNewLine _
                                    & ",UNSO_L.SYS_UPD_TIME                                             " & vbNewLine _
                                    & ",OUTKA_L.OUTKA_PKG_NB                                            " & vbNewLine _
                                    & ",OUTKA_L.NHS_REMARK                                              " & vbNewLine _
                                    & ",OUTKA_L.REMARK                                                  " & vbNewLine _
                                    & ",OUTKA_L.BUYER_ORD_NO                                            " & vbNewLine _
                                    & ",OUTKA_L.CUST_ORD_NO                                             " & vbNewLine _
                                    & " ) QUERY     "

#End Region
    '2012.10.02 日産物流　対応END

    '要望番号:1177(出荷登録時の纏め条件に【EDI受信日】を加える)　2012/06/20 本明 Start
#Region "出荷登録処理 まとめ先取得SQL(まとめF=6)"
    Private Const SQL_SELECT_MATOME_TARGET_FLAG6 As String = "SELECT                                 " & vbNewLine _
                                    & " QUERY.OUTKA_CTL_NO                                           " & vbNewLine _
                                    & ",QUERY.OUTKA_CTL_NO_CHU                                       " & vbNewLine _
                                    & ",QUERY.EDI_CTL_NO                                             " & vbNewLine _
                                    & ",QUERY.SYS_UPD_DATE                                           " & vbNewLine _
                                    & ",QUERY.SYS_UPD_TIME                                           " & vbNewLine _
                                    & ",QUERY.UNSO_NO_L                                              " & vbNewLine _
                                    & ",QUERY.UNSO_NO_M                                              " & vbNewLine _
                                    & ",QUERY.SYS_UNSO_UPD_DATE                                      " & vbNewLine _
                                    & ",QUERY.SYS_UNSO_UPD_TIME                                      " & vbNewLine _
                                    & ",QUERY.OUTKA_PKG_NB                                           " & vbNewLine _
                                    & ",QUERY.UNSO_PKG_NB                                            " & vbNewLine _
                                    & ",QUERY.NRS_BR_CD                                              " & vbNewLine _
                                    & ",QUERY.NHS_REMARK                                             " & vbNewLine _
                                    & ",QUERY.REMARK                                                 " & vbNewLine _
                                    & ",QUERY.BUYER_ORD_NO                                           " & vbNewLine _
                                    & ",QUERY.CUST_ORD_NO                                            " & vbNewLine _
                                    & ",QUERY.OUTKA_STATE_KB_1 AS OUTKA_STATE_KB                     " & vbNewLine _
                                    & " FROM                                                         " & vbNewLine _
                                    & " (SELECT                                                      " & vbNewLine _
                                    & "    OUTKA_L.NRS_BR_CD             AS  NRS_BR_CD               " & vbNewLine _
                                    & "	   ,OUTKA_L.OUTKA_NO_L           AS  OUTKA_CTL_NO            " & vbNewLine _
                                    & "    ,MAX(OUTKA_M.OUTKA_NO_M)      AS  OUTKA_CTL_NO_CHU        " & vbNewLine _
                                    & "    ,CASE WHEN (SUBSTRING(EDI_L.FREE_C30,4,9) <> ''           " & vbNewLine _
                                    & "     AND SUBSTRING(EDI_L.FREE_C30,1,2) = '04')                " & vbNewLine _
                                    & "     THEN SUBSTRING(EDI_L.FREE_C30,4,9)                       " & vbNewLine _
                                    & "     ELSE EDI_L.EDI_CTL_NO                                    " & vbNewLine _
                                    & "     END  EDI_CTL_NO                                          " & vbNewLine _
                                    & "    ,OUTKA_L.SYS_UPD_DATE         AS  SYS_UPD_DATE            " & vbNewLine _
                                    & "    ,OUTKA_L.SYS_UPD_TIME         AS  SYS_UPD_TIME            " & vbNewLine _
                                    & "    ,UNSO_L.UNSO_NO_L             AS  UNSO_NO_L               " & vbNewLine _
                                    & "    ,MAX(UNSO_M.UNSO_NO_M)        AS  UNSO_NO_M               " & vbNewLine _
                                    & "    ,UNSO_L.SYS_UPD_DATE          AS  SYS_UNSO_UPD_DATE       " & vbNewLine _
                                    & "    ,UNSO_L.SYS_UPD_TIME          AS  SYS_UNSO_UPD_TIME       " & vbNewLine _
                                    & "    ,OUTKA_L.OUTKA_PKG_NB         AS  OUTKA_PKG_NB            " & vbNewLine _
                                    & "    ,UNSO_L.UNSO_PKG_NB           AS  UNSO_PKG_NB             " & vbNewLine _
                                    & "    ,OUTKA_L.NHS_REMARK           AS  NHS_REMARK              " & vbNewLine _
                                    & "    ,OUTKA_L.REMARK               AS  REMARK                  " & vbNewLine _
                                    & "    ,OUTKA_L.BUYER_ORD_NO         AS  BUYER_ORD_NO            " & vbNewLine _
                                    & "    ,OUTKA_L.CUST_ORD_NO          AS  CUST_ORD_NO             " & vbNewLine _
                                    & "    ,CASE MAX(ISNULL(OUTKA_L.OUTKA_STATE_KB,''))              " & vbNewLine _
                                    & "    WHEN '' THEN '99'                                         " & vbNewLine _
                                    & "    WHEN '10' THEN '10'                                       " & vbNewLine _
                                    & "    WHEN '60' THEN '60'                                       " & vbNewLine _
                                    & "    WHEN '90' THEN '90'                                       " & vbNewLine _
                                    & "    ELSE '50'                                                 " & vbNewLine _
                                    & "   END AS OUTKA_STATE_KB_1                                    " & vbNewLine _
                                    & "FROM                                                          " & vbNewLine _
                                    & " $LM_TRN$..C_OUTKA_L           OUTKA_L                        " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..C_OUTKA_M           OUTKA_M                        " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = OUTKA_M.OUTKA_NO_L                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = OUTKA_M.NRS_BR_CD                        " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_M.SYS_DEL_FLG  = '0'                                   " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..F_UNSO_L            UNSO_L                         " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = UNSO_L.INOUTKA_NO_L                     " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = UNSO_L.NRS_BR_CD                         " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.MOTO_DATA_KB = '20'                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..F_UNSO_M            UNSO_M                         " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " UNSO_L.UNSO_NO_L = UNSO_M.UNSO_NO_L                          " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = UNSO_M.NRS_BR_CD                         " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_M.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..H_OUTKAEDI_L        EDI_L                          " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = EDI_L.OUTKA_CTL_NO                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = EDI_L.NRS_BR_CD                          " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " EDI_L.SYS_DEL_FLG = '0'                                      " & vbNewLine

    Private Const SQL_WHERE_MATOME_TARGET_FLAG6 As String = "WHERE                                   " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD  = @NRS_BR_CD                              " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.OUTKA_PLAN_DATE  = @OUTKA_PLAN_DATE                  " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.OUTKO_DATE  = @OUTKO_DATE                            " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.ARR_PLAN_DATE  = @ARR_PLAN_DATE                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.OUTKA_STATE_KB  < '60'                               " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.SYS_DEL_FLG  = '0'                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.UNSO_CD  = @UNSO_CD                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.UNSO_BR_CD  = @UNSO_BR_CD                             " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.CUST_CD_L  = @CUST_CD_L                              " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.CUST_CD_M  = @CUST_CD_M                              " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " EDI_L.CRT_DATE  = @CRT_DATE                                " & vbNewLine

    Private Const SQL_GROUPBY_MATOME_TARGET_FLAG6 As String = "GROUP BY                                 " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD                                               " & vbNewLine _
                                    & ",OUTKA_L.OUTKA_NO_L                                              " & vbNewLine _
                                    & ",CASE WHEN (SUBSTRING(EDI_L.FREE_C30,4,9) <> ''                  " & vbNewLine _
                                    & "      AND SUBSTRING(EDI_L.FREE_C30,1,2) = '04')                  " & vbNewLine _
                                    & "      THEN SUBSTRING(EDI_L.FREE_C30,4,9)                         " & vbNewLine _
                                    & "      ELSE EDI_L.EDI_CTL_NO                                      " & vbNewLine _
                                    & "      END                                                        " & vbNewLine _
                                    & ",OUTKA_L.SYS_UPD_DATE                                            " & vbNewLine _
                                    & ",OUTKA_L.SYS_UPD_TIME                                            " & vbNewLine _
                                    & ",UNSO_L.UNSO_NO_L                                                " & vbNewLine _
                                    & ",UNSO_L.UNSO_PKG_NB                                              " & vbNewLine _
                                    & ",UNSO_L.SYS_UPD_DATE                                             " & vbNewLine _
                                    & ",UNSO_L.SYS_UPD_TIME                                             " & vbNewLine _
                                    & ",OUTKA_L.OUTKA_PKG_NB                                            " & vbNewLine _
                                    & ",OUTKA_L.NHS_REMARK                                              " & vbNewLine _
                                    & ",OUTKA_L.REMARK                                                  " & vbNewLine _
                                    & ",OUTKA_L.BUYER_ORD_NO                                            " & vbNewLine _
                                    & ",OUTKA_L.CUST_ORD_NO                                             " & vbNewLine _
                                    & " ) QUERY     "

#End Region
    '要望番号:1177(出荷登録時の纏め条件に【EDI受信日】を加える)　2012/06/20 本明 End

    '2012.08.03 ビックケミー対応START
#Region "出荷登録処理 まとめ先取得SQL(まとめF=7)"
    Private Const SQL_SELECT_MATOME_TARGET_FLAG7 As String = "SELECT                                 " & vbNewLine _
                                    & " QUERY.OUTKA_CTL_NO                                           " & vbNewLine _
                                    & ",QUERY.OUTKA_CTL_NO_CHU                                       " & vbNewLine _
                                    & ",QUERY.EDI_CTL_NO                                             " & vbNewLine _
                                    & ",QUERY.SYS_UPD_DATE                                           " & vbNewLine _
                                    & ",QUERY.SYS_UPD_TIME                                           " & vbNewLine _
                                    & ",QUERY.UNSO_NO_L                                              " & vbNewLine _
                                    & ",QUERY.UNSO_NO_M                                              " & vbNewLine _
                                    & ",QUERY.SYS_UNSO_UPD_DATE                                      " & vbNewLine _
                                    & ",QUERY.SYS_UNSO_UPD_TIME                                      " & vbNewLine _
                                    & ",QUERY.OUTKA_PKG_NB                                           " & vbNewLine _
                                    & ",QUERY.UNSO_PKG_NB                                            " & vbNewLine _
                                    & ",QUERY.NRS_BR_CD                                              " & vbNewLine _
                                    & ",QUERY.NHS_REMARK                                             " & vbNewLine _
                                    & ",QUERY.REMARK                                                 " & vbNewLine _
                                    & ",QUERY.BUYER_ORD_NO                                           " & vbNewLine _
                                    & ",QUERY.CUST_ORD_NO                                            " & vbNewLine _
                                    & ",QUERY.OUTKA_STATE_KB_1 AS OUTKA_STATE_KB                     " & vbNewLine _
                                    & " FROM                                                         " & vbNewLine _
                                    & " (SELECT                                                      " & vbNewLine _
                                    & "    OUTKA_L.NRS_BR_CD             AS  NRS_BR_CD               " & vbNewLine _
                                    & "	   ,OUTKA_L.OUTKA_NO_L           AS  OUTKA_CTL_NO            " & vbNewLine _
                                    & "    ,MAX(OUTKA_M.OUTKA_NO_M)      AS  OUTKA_CTL_NO_CHU        " & vbNewLine _
                                    & "    ,CASE WHEN (SUBSTRING(EDI_L.FREE_C30,4,9) <> ''           " & vbNewLine _
                                    & "     AND SUBSTRING(EDI_L.FREE_C30,1,2) = '04')                " & vbNewLine _
                                    & "     THEN SUBSTRING(EDI_L.FREE_C30,4,9)                       " & vbNewLine _
                                    & "     ELSE EDI_L.EDI_CTL_NO                                    " & vbNewLine _
                                    & "     END  EDI_CTL_NO                                          " & vbNewLine _
                                    & "    ,OUTKA_L.SYS_UPD_DATE         AS  SYS_UPD_DATE            " & vbNewLine _
                                    & "    ,OUTKA_L.SYS_UPD_TIME         AS  SYS_UPD_TIME            " & vbNewLine _
                                    & "    ,UNSO_L.UNSO_NO_L             AS  UNSO_NO_L               " & vbNewLine _
                                    & "    ,MAX(UNSO_M.UNSO_NO_M)        AS  UNSO_NO_M               " & vbNewLine _
                                    & "    ,UNSO_L.SYS_UPD_DATE          AS  SYS_UNSO_UPD_DATE       " & vbNewLine _
                                    & "    ,UNSO_L.SYS_UPD_TIME          AS  SYS_UNSO_UPD_TIME       " & vbNewLine _
                                    & "    ,OUTKA_L.OUTKA_PKG_NB         AS  OUTKA_PKG_NB            " & vbNewLine _
                                    & "    ,UNSO_L.UNSO_PKG_NB           AS  UNSO_PKG_NB             " & vbNewLine _
                                    & "    ,OUTKA_L.NHS_REMARK           AS  NHS_REMARK              " & vbNewLine _
                                    & "    ,OUTKA_L.REMARK               AS  REMARK                  " & vbNewLine _
                                    & "    ,OUTKA_L.BUYER_ORD_NO         AS  BUYER_ORD_NO            " & vbNewLine _
                                    & "    ,OUTKA_L.CUST_ORD_NO          AS  CUST_ORD_NO             " & vbNewLine _
                                    & "    ,CASE MAX(ISNULL(OUTKA_L.OUTKA_STATE_KB,''))              " & vbNewLine _
                                    & "    WHEN '' THEN '99'                                         " & vbNewLine _
                                    & "    WHEN '10' THEN '10'                                       " & vbNewLine _
                                    & "    WHEN '60' THEN '60'                                       " & vbNewLine _
                                    & "    WHEN '90' THEN '90'                                       " & vbNewLine _
                                    & "    ELSE '50'                                                 " & vbNewLine _
                                    & "   END AS OUTKA_STATE_KB_1                                    " & vbNewLine _
                                    & "FROM                                                          " & vbNewLine _
                                    & " $LM_TRN$..C_OUTKA_L           OUTKA_L                        " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..C_OUTKA_M           OUTKA_M                        " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = OUTKA_M.OUTKA_NO_L                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = OUTKA_M.NRS_BR_CD                        " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_M.SYS_DEL_FLG  = '0'                                   " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..F_UNSO_L            UNSO_L                         " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = UNSO_L.INOUTKA_NO_L                     " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = UNSO_L.NRS_BR_CD                         " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.MOTO_DATA_KB = '20'                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..F_UNSO_M            UNSO_M                         " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " UNSO_L.UNSO_NO_L = UNSO_M.UNSO_NO_L                          " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = UNSO_M.NRS_BR_CD                         " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_M.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..H_OUTKAEDI_L        EDI_L                          " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = EDI_L.OUTKA_CTL_NO                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = EDI_L.NRS_BR_CD                          " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " EDI_L.SYS_DEL_FLG = '0'                                      " & vbNewLine

    Private Const SQL_WHERE_MATOME_TARGET_FLAG7 As String = "WHERE                                   " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD  = @NRS_BR_CD                              " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.OUTKA_PLAN_DATE  = @OUTKA_PLAN_DATE                  " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.OUTKO_DATE  = @OUTKO_DATE                            " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.ARR_PLAN_DATE  = @ARR_PLAN_DATE                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & "--(2012.11.20)要望番号1618 検品済以上は纏めない START         " & vbNewLine _
                                    & "-- OUTKA_L.OUTKA_STATE_KB  < '60'                             " & vbNewLine _
                                    & " OUTKA_L.OUTKA_STATE_KB  < '50'                               " & vbNewLine _
                                    & "--(2012.11.20)要望番号1618 検品済以上は纏めない END           " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.SYS_DEL_FLG  = '0'                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.UNSO_CD  = @UNSO_CD                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.UNSO_BR_CD  = @UNSO_BR_CD                             " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.CUST_CD_L  = @CUST_CD_L                              " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.CUST_CD_M  = @CUST_CD_M                              " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " EDI_L.UNSO_ATT  = @UNSO_ATT                                  " & vbNewLine _
                                    & " --(2013.03.06)要望番号1929 追加START                         " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.SHIP_CD_L  = @SHIP_CD_L                              " & vbNewLine

    Private Const SQL_GROUPBY_MATOME_TARGET_FLAG7 As String = "GROUP BY                                 " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD                                               " & vbNewLine _
                                    & ",OUTKA_L.OUTKA_NO_L                                              " & vbNewLine _
                                    & ",CASE WHEN (SUBSTRING(EDI_L.FREE_C30,4,9) <> ''                  " & vbNewLine _
                                    & "      AND SUBSTRING(EDI_L.FREE_C30,1,2) = '04')                  " & vbNewLine _
                                    & "      THEN SUBSTRING(EDI_L.FREE_C30,4,9)                         " & vbNewLine _
                                    & "      ELSE EDI_L.EDI_CTL_NO                                      " & vbNewLine _
                                    & "      END                                                        " & vbNewLine _
                                    & ",OUTKA_L.SYS_UPD_DATE                                            " & vbNewLine _
                                    & ",OUTKA_L.SYS_UPD_TIME                                            " & vbNewLine _
                                    & ",UNSO_L.UNSO_NO_L                                                " & vbNewLine _
                                    & ",UNSO_L.UNSO_PKG_NB                                              " & vbNewLine _
                                    & ",UNSO_L.SYS_UPD_DATE                                             " & vbNewLine _
                                    & ",UNSO_L.SYS_UPD_TIME                                             " & vbNewLine _
                                    & ",OUTKA_L.OUTKA_PKG_NB                                            " & vbNewLine _
                                    & ",OUTKA_L.NHS_REMARK                                              " & vbNewLine _
                                    & ",OUTKA_L.REMARK                                                  " & vbNewLine _
                                    & ",OUTKA_L.BUYER_ORD_NO                                            " & vbNewLine _
                                    & ",OUTKA_L.CUST_ORD_NO                                             " & vbNewLine _
                                    & " ) QUERY     "

#End Region
    '2012.08.03 ビックケミー対応END

#Region "まとめ先取得"
    '    Private Const SQL_SELECT_MATOME_TARGET2 As String = "SELECT                                              " & vbNewLine _
    '                                    & " OUTKA_L.NRS_BR_CD             AS  NRS_BR_CD                          " & vbNewLine _
    '                                    & ",OUTKA_L.OUTKA_NO_L            AS  OUTKA_CTL_NO                       " & vbNewLine _
    '                                    & ",MAX(OUTKA_M.OUTKA_NO_M)       AS  OUTKA_CTL_NO_CHU                   " & vbNewLine _
    '                                    & ",EDI_L.EDI_CTL_NO              AS  EDI_CTL_NO                         " & vbNewLine _
    '                                    & ",OUTKA_L.SYS_UPD_DATE          AS  SYS_UPD_DATE                       " & vbNewLine _
    '                                    & ",OUTKA_L.SYS_UPD_TIME          AS  SYS_UPD_TIME                       " & vbNewLine _
    '                                    & ",UNSO_L.UNSO_NO_L              AS  UNSO_NO_L                          " & vbNewLine _
    '                                    & ",MAX(UNSO_M.UNSO_NO_M)         AS  UNSO_NO_M                          " & vbNewLine _
    '                                    & ",UNSO_L.SYS_UPD_DATE           AS  SYS_UNSO_UPD_DATE                  " & vbNewLine _
    '                                    & ",UNSO_L.SYS_UPD_TIME           AS  SYS_UNSO_UPD_TIME                  " & vbNewLine _
    '                                    & ",OUTKA_L.OUTKA_PKG_NB          AS  OUTKA_PKG_NB                       " & vbNewLine _
    '                                    & ",UNSO_L.UNSO_PKG_NB            AS  UNSO_PKG_NB                        " & vbNewLine _
    '                                    & "FROM                                                                  " & vbNewLine _
    '                                    & " $LM_TRN$..C_OUTKA_L           OUTKA_L                                " & vbNewLine _
    '                                    & "INNER JOIN                                                            " & vbNewLine _
    '                                    & " $LM_TRN$..C_OUTKA_M           OUTKA_M                                " & vbNewLine _
    '                                    & " ON                                                                   " & vbNewLine _
    '                                    & " OUTKA_L.OUTKA_NO_L = OUTKA_M.OUTKA_NO_L                              " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " OUTKA_L.NRS_BR_CD = OUTKA_M.NRS_BR_CD                                " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " OUTKA_M.SYS_DEL_FLG  = '0'                                " & vbNewLine _
    '                                    & "INNER JOIN                                                            " & vbNewLine _
    '                                    & " $LM_TRN$..F_UNSO_L            UNSO_L                                 " & vbNewLine _
    '                                    & " ON                                                                   " & vbNewLine _
    '                                    & " OUTKA_L.OUTKA_NO_L = UNSO_L.INOUTKA_NO_L                             " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " OUTKA_L.NRS_BR_CD = UNSO_L.NRS_BR_CD                                 " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " UNSO_L.MOTO_DATA_KB = '20'                                           " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " UNSO_L.SYS_DEL_FLG = '0'                                 " & vbNewLine _
    '                                    & "INNER JOIN                                                            " & vbNewLine _
    '                                    & " $LM_TRN$..F_UNSO_M            UNSO_M                                 " & vbNewLine _
    '                                    & " ON                                                                   " & vbNewLine _
    '                                    & " UNSO_L.UNSO_NO_L = UNSO_M.UNSO_NO_L                                  " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " UNSO_L.NRS_BR_CD = UNSO_M.NRS_BR_CD                                 " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " UNSO_M.SYS_DEL_FLG = '0'                                 " & vbNewLine _
    '                                    & "INNER JOIN                                                            " & vbNewLine _
    '                                    & " $LM_TRN$..H_OUTKAEDI_L        EDI_L                                  " & vbNewLine _
    '                                    & " ON                                                                   " & vbNewLine _
    '                                    & " OUTKA_L.OUTKA_NO_L = EDI_L.OUTKA_CTL_NO                              " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " OUTKA_L.NRS_BR_CD = EDI_L.NRS_BR_CD                                 " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " EDI_L.SYS_DEL_FLG = '0'                                 " & vbNewLine _
    '                                    & "WHERE                                                                 " & vbNewLine _
    '                                    & " OUTKA_L.NRS_BR_CD  = @NRS_BR_CD                                      " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " OUTKA_L.OUTKA_PLAN_DATE  = @OUTKA_PLAN_DATE                          " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " OUTKA_L.OUTKO_DATE  = @OUTKO_DATE                                    " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " OUTKA_L.ARR_PLAN_DATE  = @ARR_PLAN_DATE                              " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " OUTKA_L.OUTKA_STATE_KB  = '10'                                       " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " OUTKA_L.SYS_DEL_FLG  = '0'                                           " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " UNSO_L.UNSO_CD  = @UNSO_CD                                           " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " UNSO_L.UNSO_BR_CD  = @UNSO_BR_CD                                     " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " OUTKA_L.CUST_CD_L  = @CUST_CD_L                                      " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " OUTKA_L.CUST_CD_M  = @CUST_CD_M                                      " & vbNewLine _
    '                                    & " AND                                                                  " & vbNewLine _
    '                                    & " OUTKA_L.DEST_CD  = @DEST_CD                                          " & vbNewLine _
    '                                    & "GROUP BY                                                              " & vbNewLine _
    '                                    & " OUTKA_L.NRS_BR_CD                                                    " & vbNewLine _
    '                                    & ",OUTKA_L.OUTKA_NO_L                                                   " & vbNewLine _
    '                                    & ",EDI_L.EDI_CTL_NO                                                     " & vbNewLine _
    '                                    & ",OUTKA_L.SYS_UPD_DATE                                                 " & vbNewLine _
    '                                    & ",OUTKA_L.SYS_UPD_TIME                                                 " & vbNewLine _
    '                                    & ",UNSO_L.UNSO_NO_L                                                     " & vbNewLine _
    '                                    & ",UNSO_L.SYS_UPD_DATE                                                  " & vbNewLine _
    '                                    & ",UNSO_L.SYS_UPD_TIME                                                  " & vbNewLine _
    '                                    & ",OUTKA_L.OUTKA_PKG_NB                                                 " & vbNewLine _
    '                                    & ",UNSO_L.UNSO_PKG_NB                                                   " & vbNewLine

    ''' <summary>
    ''' まとめ先データ運送M取得用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MATOMEMOTO_DATA_UNSO_M As String = " SELECT                                       " & vbNewLine _
                                            & " F_UNSO_M.NRS_BR_CD                       AS NRS_BR_CD   " & vbNewLine _
                                            & ",F_UNSO_M.UNSO_NO_L                       AS UNSO_NO_L   " & vbNewLine _
                                            & ",F_UNSO_M.UNSO_NO_M                       AS UNSO_NO_M   " & vbNewLine _
                                            & ",F_UNSO_M.BETU_WT                         AS BETU_WT     " & vbNewLine _
                                            & ",F_UNSO_M.UNSO_TTL_NB                     AS UNSO_TTL_NB " & vbNewLine _
                                            & ",F_UNSO_M.HASU                            AS HASU        " & vbNewLine _
                                            & ",F_UNSO_M.PKG_NB                          AS PKG_NB      " & vbNewLine _
                                            & " FROM                                                    " & vbNewLine _
                                            & " $LM_TRN$..F_UNSO_M                       F_UNSO_M       " & vbNewLine _
                                            & " WHERE                                                   " & vbNewLine _
                                            & " F_UNSO_M.NRS_BR_CD   = @NRS_BR_CD                       " & vbNewLine _
                                            & " AND                                                     " & vbNewLine _
                                            & " F_UNSO_M.UNSO_NO_L   = @UNSO_NO_L                       " & vbNewLine
#End Region

#Region "まとめフラグ取得"
    Private Const SQL_SELECT_MATOMEFLG As String = "SELECT                                               " & vbNewLine _
                                & " FLAG_07                       AS  FLAG_07                            " & vbNewLine _
                                & "FROM $LM_MST$..M_EDI_CUST                                             " & vbNewLine _
                                & "WHERE                                                                 " & vbNewLine _
                                & " NRS_BR_CD = @NRS_BR_CD                                               " & vbNewLine _
                                & " AND                                                                  " & vbNewLine _
                                & " WH_CD = @WH_CD                                                       " & vbNewLine _
                                & " AND                                                                  " & vbNewLine _
                                & " CUST_CD_L = @CUST_CD_L                                               " & vbNewLine _
                                & " AND                                                                  " & vbNewLine _
                                & " CUST_CD_M = @CUST_CD_M                                               " & vbNewLine _
                                & " AND                                                                  " & vbNewLine _
                                & " INOUT_KB  = '0'                                                      " & vbNewLine

#End Region

    '要望番号:944（後からまとめをするとアベンド） 2012/05/25 Honmyo Start
#Region "SQL_GetMaxOUTKA_CTL_NO_CHU"
    Private Const SQL_GetMaxOUTKA_NO_CHU As String =
                                       " SELECT CASE WHEN MAX(OUTKA_NO_M) IS NULL THEN 0 ELSE MAX(OUTKA_NO_M) END " & vbNewLine _
                                     & " FROM                                                   " & vbNewLine _
                                     & "    $LM_TRN$..C_OUTKA_M                                 " & vbNewLine _
                                     & " WHERE                                                  " & vbNewLine _
                                     & "    NRS_BR_CD=@NRS_BR_CD AND OUTKA_NO_L=@OUTKA_NO_L     " & vbNewLine
#End Region
    '要望番号:944（後からまとめをするとアベンド） 2012/05/25 Honmyo End

#Region "引当可能数取得"
    Private Const SQL_GetALLOC_CAN_NB As String =
                                       " SELECT CASE WHEN SUM(ALLOC_CAN_NB) IS NULL THEN 0 ELSE SUM(ALLOC_CAN_NB) END " & vbNewLine _
                                     & " FROM                                                   " & vbNewLine _
                                     & "    $LM_TRN$..D_ZAI_TRS                                 " & vbNewLine _
                                     & " WHERE                                                  " & vbNewLine _
                                     & "    NRS_BR_CD=@NRS_BR_CD                                " & vbNewLine _
                                     & "    AND GOODS_CD_NRS=@GOODS_CD_NRS                      " & vbNewLine
    Private Const SQL_GetALLOC_CAN_NB_WhereLOT_NO As String =
                                       "    AND LOT_NO = @LOT_NO "
    '要望番号:1155 2012/06/14 本明 Start
    Private Const SQL_GetALLOC_CAN_NB_WhereIRIME As String =
                                       "    AND IRIME = @IRIME "
    '要望番号:1155 2012/06/14 本明 End

#End Region

    '■■キャッシュ更新までの暫定対応　Start■■
#Region "受信DTL取得"
    Private Const SQL_SELECT_RCV_NM_DTL As String = "SELECT                                               " & vbNewLine _
                                & " RCV_NM_DTL                       AS  RCV_NM_DTL                      " & vbNewLine _
                                & "FROM $LM_MST$..M_EDI_CUST                                             " & vbNewLine _
                                & "WHERE                                                                 " & vbNewLine _
                                & " NRS_BR_CD = @NRS_BR_CD                                               " & vbNewLine _
                                & " AND                                                                  " & vbNewLine _
                                & " WH_CD = @WH_CD                                                       " & vbNewLine _
                                & " AND                                                                  " & vbNewLine _
                                & " CUST_CD_L = @CUST_CD_L                                               " & vbNewLine _
                                & " AND                                                                  " & vbNewLine _
                                & " CUST_CD_M = @CUST_CD_M                                               " & vbNewLine _
                                & " AND                                                                  " & vbNewLine _
                                & " INOUT_KB  = '0'                                                      " & vbNewLine

#End Region
    '■■キャッシュ更新までの暫定対応　End■■

    '印刷フラグ更新対応 2012.03.18 修正START
#Region "PRINT_FLAG"
    ''' <summary>
    ''' 印刷フラグ更新のUPDATE文
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_PRTFLG_HED As String = "UPDATE $LM_TRN$..$RCV_NM$ SET  " & vbNewLine _
                                              & " PRTFLG            = @PRTFLG                       " & vbNewLine _
                                              & ",PRT_DATE          = @PRT_DATE                     " & vbNewLine _
                                              & ",PRT_TIME          = @PRT_TIME                     " & vbNewLine _
                                              & ",PRT_USER          = @PRT_USER                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                    " & vbNewLine _
                                              & "--AND NRS_WH_CD      = @WH_CD                        " & vbNewLine _
                                              & "AND CUST_CD_L      = @CUST_CD_L                    " & vbNewLine _
                                              & "AND CUST_CD_M      = @CUST_CD_M                    " & vbNewLine _
                                              & "AND PRTFLG         = '0'                           " & vbNewLine

    Private Const SQL_UPD_PRTFLG_DTL As String = "UPDATE $LM_TRN$..$RCV_NM$ SET  " & vbNewLine _
                                          & " PRTFLG            = @PRTFLG                       " & vbNewLine _
                                          & ",PRT_DATE          = @PRT_DATE                     " & vbNewLine _
                                          & ",PRT_TIME          = @PRT_TIME                     " & vbNewLine _
                                          & ",PRT_USER          = @PRT_USER                     " & vbNewLine _
                                          & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                          & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                          & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                          & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                          & "WHERE   NRS_BR_CD  = @NRS_BR_CD                    " & vbNewLine _
                                          & "--AND NRS_WH_CD      = @WH_CD                      " & vbNewLine _
                                          & "AND PRTFLG         = '0'                           " & vbNewLine

    Private Const SQL_FROM_PRTFLG_CRT_DATE_FROM As String = " AND CRT_DATE      >= @CRT_DATE_FROM   " & vbNewLine

    Private Const SQL_FROM_PRTFLG_CRT_DATE_TO As String = "   AND CRT_DATE      <= @CRT_DATE_TO     " & vbNewLine

    Private Const SQL_FROM_PRTFLG_INOUT As String = "AND INOUT_KB = '0'                             " & vbNewLine


#End Region
    '印刷フラグ更新対応 2012.03.18 修正END

    '要望番号1007 2012.05.08 修正START
#Region "H_EDI_PRINT"

    ''' <summary>
    ''' EDI印刷対象テーブルのDELETE文
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_EDI_PRINT As String = "DELETE $LM_TRN$..H_EDI_PRINT         " & vbNewLine _
                                              & " WHERE  NRS_BR_CD    = @NRS_BR_CD    " & vbNewLine _
                                              & " AND    EDI_CTL_NO   = @EDI_CTL_NO   " & vbNewLine _
                                              & " AND    INOUT_KB     = @INOUT_KB     " & vbNewLine _
                                              & " AND    PRINT_TP     = @PRINT_TP     " & vbNewLine

    ''' <summary>
    ''' EDI印刷対象テーブルのDELETE文(WHERE句：DENPYO_NO存在時)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_WHERE_DENPYO_NO As String = "AND    DENPYO_NO   = @DENPYO_NO  " & vbNewLine


    '2012.05.29 要望番号1077 修正START
    ''' <summary>
    ''' EDI印刷対象テーブルのINSERT文
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_INS_EDI_PRINT As String = "INSERT INTO $LM_TRN$..H_EDI_PRINT        " & vbNewLine _
                                       & "(                                   " & vbNewLine _
                                       & "      NRS_BR_CD                     " & vbNewLine _
                                       & "      ,EDI_CTL_NO                   " & vbNewLine _
                                       & "      ,INOUT_KB                     " & vbNewLine _
                                       & "      ,CUST_CD_L                    " & vbNewLine _
                                       & "      ,CUST_CD_M                    " & vbNewLine _
                                       & "      ,PRINT_TP                     " & vbNewLine _
                                       & "      ,DENPYO_NO                    " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                 " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                 " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                 " & vbNewLine _
                                       & "      ,SYS_ENT_USER                 " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                 " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                 " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                 " & vbNewLine _
                                       & "      ,SYS_UPD_USER                 " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                  " & vbNewLine _
                                       & "      ) VALUES (                    " & vbNewLine _
                                       & "      @NRS_BR_CD                    " & vbNewLine _
                                       & "      ,@EDI_CTL_NO                  " & vbNewLine _
                                       & "      ,@INOUT_KB                    " & vbNewLine _
                                       & "      ,@CUST_CD_L                   " & vbNewLine _
                                       & "      ,@CUST_CD_M                   " & vbNewLine _
                                       & "      ,@PRINT_TP                    " & vbNewLine _
                                       & "      ,@DENPYO_NO                   " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE                " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME                " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID                " & vbNewLine _
                                       & "      ,@SYS_ENT_USER                " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE                " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME                " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID                " & vbNewLine _
                                       & "      ,@SYS_UPD_USER                " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                 " & vbNewLine _
                                       & ")                                   " & vbNewLine

#End Region
    '要望番号1007 2012.05.08 修正END
    '2012.05.29 要望番号1077 修正END


    'START UMANO 要望番号1286 支払運賃作成
#Region "F_SHIHARAI_TRS(INSERT)"

    ''' <summary>
    ''' SHIHARAI INSERT用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SHIHARAI_INSERT As String = "INSERT INTO $LM_TRN$..F_SHIHARAI_TRS" & vbNewLine _
                                              & "(                                 " & vbNewLine _
                                              & " YUSO_BR_CD                       " & vbNewLine _
                                              & ",NRS_BR_CD                        " & vbNewLine _
                                              & ",UNSO_NO_L                        " & vbNewLine _
                                              & ",UNSO_NO_M                        " & vbNewLine _
                                              & ",CUST_CD_L                        " & vbNewLine _
                                              & ",CUST_CD_M                        " & vbNewLine _
                                              & ",CUST_CD_S                        " & vbNewLine _
                                              & ",CUST_CD_SS                       " & vbNewLine _
                                              & ",SHIHARAI_GROUP_NO                " & vbNewLine _
                                              & ",SHIHARAI_GROUP_NO_M              " & vbNewLine _
                                              & ",SHIHARAITO_CD                    " & vbNewLine _
                                              & ",UNTIN_CALCULATION_KB             " & vbNewLine _
                                              & ",SHIHARAI_SYARYO_KB               " & vbNewLine _
                                              & ",SHIHARAI_PKG_UT                  " & vbNewLine _
                                              & ",SHIHARAI_NG_NB                   " & vbNewLine _
                                              & ",SHIHARAI_DANGER_KB               " & vbNewLine _
                                              & ",SHIHARAI_TARIFF_BUNRUI_KB        " & vbNewLine _
                                              & ",SHIHARAI_TARIFF_CD               " & vbNewLine _
                                              & ",SHIHARAI_ETARIFF_CD              " & vbNewLine _
                                              & ",SHIHARAI_KYORI                   " & vbNewLine _
                                              & ",SHIHARAI_WT                      " & vbNewLine _
                                              & ",SHIHARAI_UNCHIN                  " & vbNewLine _
                                              & ",SHIHARAI_CITY_EXTC               " & vbNewLine _
                                              & ",SHIHARAI_WINT_EXTC               " & vbNewLine _
                                              & ",SHIHARAI_RELY_EXTC               " & vbNewLine _
                                              & ",SHIHARAI_TOLL                    " & vbNewLine _
                                              & ",SHIHARAI_INSU                    " & vbNewLine _
                                              & ",SHIHARAI_FIXED_FLAG              " & vbNewLine _
                                              & ",DECI_NG_NB                       " & vbNewLine _
                                              & ",DECI_KYORI                       " & vbNewLine _
                                              & ",DECI_WT                          " & vbNewLine _
                                              & ",DECI_UNCHIN                      " & vbNewLine _
                                              & ",DECI_CITY_EXTC                   " & vbNewLine _
                                              & ",DECI_WINT_EXTC                   " & vbNewLine _
                                              & ",DECI_RELY_EXTC                   " & vbNewLine _
                                              & ",DECI_TOLL                        " & vbNewLine _
                                              & ",DECI_INSU                        " & vbNewLine _
                                              & ",KANRI_UNCHIN                     " & vbNewLine _
                                              & ",KANRI_CITY_EXTC                  " & vbNewLine _
                                              & ",KANRI_WINT_EXTC                  " & vbNewLine _
                                              & ",KANRI_RELY_EXTC                  " & vbNewLine _
                                              & ",KANRI_TOLL                       " & vbNewLine _
                                              & ",KANRI_INSU                       " & vbNewLine _
                                              & ",REMARK                           " & vbNewLine _
                                              & ",SIZE_KB                          " & vbNewLine _
                                              & ",TAX_KB                           " & vbNewLine _
                                              & ",SAGYO_KANRI                      " & vbNewLine _
                                              & ",SYS_ENT_DATE                     " & vbNewLine _
                                              & ",SYS_ENT_TIME                     " & vbNewLine _
                                              & ",SYS_ENT_PGID                     " & vbNewLine _
                                              & ",SYS_ENT_USER                     " & vbNewLine _
                                              & ",SYS_UPD_DATE                     " & vbNewLine _
                                              & ",SYS_UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_PGID                     " & vbNewLine _
                                              & ",SYS_UPD_USER                     " & vbNewLine _
                                              & ",SYS_DEL_FLG                      " & vbNewLine _
                                              & " )VALUES(                         " & vbNewLine _
                                              & " @YUSO_BR_CD                      " & vbNewLine _
                                              & ",@NRS_BR_CD                       " & vbNewLine _
                                              & ",@UNSO_NO_L                       " & vbNewLine _
                                              & ",@UNSO_NO_M                       " & vbNewLine _
                                              & ",@CUST_CD_L                       " & vbNewLine _
                                              & ",@CUST_CD_M                       " & vbNewLine _
                                              & ",@CUST_CD_S                       " & vbNewLine _
                                              & ",@CUST_CD_SS                      " & vbNewLine _
                                              & ",@SHIHARAI_GROUP_NO               " & vbNewLine _
                                              & ",@SHIHARAI_GROUP_NO_M             " & vbNewLine _
                                              & ",@SHIHARAITO_CD                   " & vbNewLine _
                                              & ",@UNTIN_CALCULATION_KB            " & vbNewLine _
                                              & ",@SHIHARAI_SYARYO_KB              " & vbNewLine _
                                              & ",@SHIHARAI_PKG_UT                 " & vbNewLine _
                                              & ",@SHIHARAI_NG_NB                  " & vbNewLine _
                                              & ",@SHIHARAI_DANGER_KB              " & vbNewLine _
                                              & ",@SHIHARAI_TARIFF_BUNRUI_KB       " & vbNewLine _
                                              & ",@SHIHARAI_TARIFF_CD              " & vbNewLine _
                                              & ",@SHIHARAI_ETARIFF_CD             " & vbNewLine _
                                              & ",@SHIHARAI_KYORI                  " & vbNewLine _
                                              & ",@SHIHARAI_WT                     " & vbNewLine _
                                              & ",@SHIHARAI_UNCHIN                 " & vbNewLine _
                                              & ",@SHIHARAI_CITY_EXTC              " & vbNewLine _
                                              & ",@SHIHARAI_WINT_EXTC              " & vbNewLine _
                                              & ",@SHIHARAI_RELY_EXTC              " & vbNewLine _
                                              & ",@SHIHARAI_TOLL                   " & vbNewLine _
                                              & ",@SHIHARAI_INSU                   " & vbNewLine _
                                              & ",@SHIHARAI_FIXED_FLAG             " & vbNewLine _
                                              & ",@DECI_NG_NB                      " & vbNewLine _
                                              & ",@DECI_KYORI                      " & vbNewLine _
                                              & ",@DECI_WT                         " & vbNewLine _
                                              & ",@DECI_UNCHIN                     " & vbNewLine _
                                              & ",@DECI_CITY_EXTC                  " & vbNewLine _
                                              & ",@DECI_WINT_EXTC                  " & vbNewLine _
                                              & ",@DECI_RELY_EXTC                  " & vbNewLine _
                                              & ",@DECI_TOLL                       " & vbNewLine _
                                              & ",@DECI_INSU                       " & vbNewLine _
                                              & ",@KANRI_UNCHIN                    " & vbNewLine _
                                              & ",@KANRI_CITY_EXTC                 " & vbNewLine _
                                              & ",@KANRI_WINT_EXTC                 " & vbNewLine _
                                              & ",@KANRI_RELY_EXTC                 " & vbNewLine _
                                              & ",@KANRI_TOLL                      " & vbNewLine _
                                              & ",@KANRI_INSU                      " & vbNewLine _
                                              & ",@REMARK                          " & vbNewLine _
                                              & ",@SIZE_KB                         " & vbNewLine _
                                              & ",@TAX_KB                          " & vbNewLine _
                                              & ",@SAGYO_KANRI                     " & vbNewLine _
                                              & ",@SYS_ENT_DATE                    " & vbNewLine _
                                              & ",@SYS_ENT_TIME                    " & vbNewLine _
                                              & ",@SYS_ENT_PGID                    " & vbNewLine _
                                              & ",@SYS_ENT_USER                    " & vbNewLine _
                                              & ",@SYS_UPD_DATE                    " & vbNewLine _
                                              & ",@SYS_UPD_TIME                    " & vbNewLine _
                                              & ",@SYS_UPD_PGID                    " & vbNewLine _
                                              & ",@SYS_UPD_USER                    " & vbNewLine _
                                              & ",@SYS_DEL_FLG                     " & vbNewLine _
                                              & ")                                 " & vbNewLine

#End Region
    'END UMANO 要望番号1286 支払運賃作成

#Region "更新共通"

    Private Const SQL_UPDATE As String = ",SYS_UPD_DATE      = @SYS_UPD_DATE" & vbNewLine _
                                       & ",SYS_UPD_TIME      = @SYS_UPD_TIME" & vbNewLine _
                                       & ",SYS_UPD_PGID      = @SYS_UPD_PGID" & vbNewLine _
                                       & ",SYS_UPD_USER      = @SYS_UPD_USER" & vbNewLine _
                                       & "WHERE NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                       & "  AND EDI_CTL_NO   = @EDI_CTL_NO   " & vbNewLine _
                                       & "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                       & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#Region "制御用"

    Private Const SQL_COM_UPDATE_CONDITION As String = "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                                     & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region
    '▲▲▲二次(共通化)

#If True Then ' セミEDI(フィルメニッヒ) まとめ対象変更対応 20161017 added inoue

#Region "出荷登録処理 まとめ先取得SQL(まとめF=8):共通2の複製(フィルメ用)"


    Private Const SQL_SELECT_MATOME_TARGET_FLAG8 As String = "SELECT                                 " & vbNewLine _
                                    & " QUERY.OUTKA_CTL_NO                                           " & vbNewLine _
                                    & ",QUERY.OUTKA_CTL_NO_CHU                                       " & vbNewLine _
                                    & ",QUERY.EDI_CTL_NO                                             " & vbNewLine _
                                    & ",QUERY.SYS_UPD_DATE                                           " & vbNewLine _
                                    & ",QUERY.SYS_UPD_TIME                                           " & vbNewLine _
                                    & ",QUERY.UNSO_NO_L                                              " & vbNewLine _
                                    & ",QUERY.UNSO_NO_M                                              " & vbNewLine _
                                    & ",QUERY.SYS_UNSO_UPD_DATE                                      " & vbNewLine _
                                    & ",QUERY.SYS_UNSO_UPD_TIME                                      " & vbNewLine _
                                    & ",QUERY.OUTKA_PKG_NB                                           " & vbNewLine _
                                    & ",QUERY.UNSO_PKG_NB                                            " & vbNewLine _
                                    & ",QUERY.NRS_BR_CD                                              " & vbNewLine _
                                    & ",QUERY.NHS_REMARK                                             " & vbNewLine _
                                    & ",QUERY.REMARK                                                 " & vbNewLine _
                                    & ",QUERY.BUYER_ORD_NO                                           " & vbNewLine _
                                    & " -- 要望番号922 追加START                                     " & vbNewLine _
                                    & ",QUERY.CUST_ORD_NO                                            " & vbNewLine _
                                    & " -- 要望番号922 追加END                                       " & vbNewLine _
                                    & ",QUERY.OUTKA_STATE_KB_1 AS OUTKA_STATE_KB                     " & vbNewLine _
                                    & " FROM                                                         " & vbNewLine _
                                    & " (SELECT                                                      " & vbNewLine _
                                    & "    OUTKA_L.NRS_BR_CD             AS  NRS_BR_CD               " & vbNewLine _
                                    & "	   ,OUTKA_L.OUTKA_NO_L           AS  OUTKA_CTL_NO            " & vbNewLine _
                                    & "    ,MAX(OUTKA_M.OUTKA_NO_M)      AS  OUTKA_CTL_NO_CHU        " & vbNewLine _
                                    & "    ,CASE WHEN (SUBSTRING(EDI_L.FREE_C30,4,9) <> ''           " & vbNewLine _
                                    & "     AND SUBSTRING(EDI_L.FREE_C30,1,2) = '04')                " & vbNewLine _
                                    & "     THEN SUBSTRING(EDI_L.FREE_C30,4,9)                       " & vbNewLine _
                                    & "     ELSE EDI_L.EDI_CTL_NO                                    " & vbNewLine _
                                    & "     END  EDI_CTL_NO                                          " & vbNewLine _
                                    & "    ,OUTKA_L.SYS_UPD_DATE         AS  SYS_UPD_DATE            " & vbNewLine _
                                    & "    ,OUTKA_L.SYS_UPD_TIME         AS  SYS_UPD_TIME            " & vbNewLine _
                                    & "    ,UNSO_L.UNSO_NO_L             AS  UNSO_NO_L               " & vbNewLine _
                                    & "    ,MAX(UNSO_M.UNSO_NO_M)        AS  UNSO_NO_M               " & vbNewLine _
                                    & "    ,UNSO_L.SYS_UPD_DATE          AS  SYS_UNSO_UPD_DATE       " & vbNewLine _
                                    & "    ,UNSO_L.SYS_UPD_TIME          AS  SYS_UNSO_UPD_TIME       " & vbNewLine _
                                    & "    ,OUTKA_L.OUTKA_PKG_NB         AS  OUTKA_PKG_NB            " & vbNewLine _
                                    & "    ,UNSO_L.UNSO_PKG_NB           AS  UNSO_PKG_NB             " & vbNewLine _
                                    & "    ,OUTKA_L.NHS_REMARK           AS  NHS_REMARK              " & vbNewLine _
                                    & "    ,OUTKA_L.REMARK               AS  REMARK                  " & vbNewLine _
                                    & "    ,OUTKA_L.BUYER_ORD_NO         AS  BUYER_ORD_NO            " & vbNewLine _
                                    & " -- 要望番号922 追加START                                     " & vbNewLine _
                                    & "    ,OUTKA_L.CUST_ORD_NO          AS  CUST_ORD_NO             " & vbNewLine _
                                    & " -- 要望番号922 追加END                                       " & vbNewLine _
                                    & "    ,CASE MAX(ISNULL(OUTKA_L.OUTKA_STATE_KB,''))              " & vbNewLine _
                                    & "    WHEN '' THEN '99'                                         " & vbNewLine _
                                    & "    WHEN '10' THEN '10'                                       " & vbNewLine _
                                    & "    WHEN '60' THEN '60'                                       " & vbNewLine _
                                    & "    WHEN '90' THEN '90'                                       " & vbNewLine _
                                    & "    ELSE '50'                                                 " & vbNewLine _
                                    & "   END AS OUTKA_STATE_KB_1                                    " & vbNewLine _
                                    & "FROM                                                          " & vbNewLine _
                                    & " $LM_TRN$..C_OUTKA_L           OUTKA_L                        " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..C_OUTKA_M           OUTKA_M                        " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = OUTKA_M.OUTKA_NO_L                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = OUTKA_M.NRS_BR_CD                        " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_M.SYS_DEL_FLG  = '0'                                   " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..F_UNSO_L            UNSO_L                         " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = UNSO_L.INOUTKA_NO_L                     " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = UNSO_L.NRS_BR_CD                         " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.MOTO_DATA_KB = '20'                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..F_UNSO_M            UNSO_M                         " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " UNSO_L.UNSO_NO_L = UNSO_M.UNSO_NO_L                          " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = UNSO_M.NRS_BR_CD                         " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_M.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..H_OUTKAEDI_L        EDI_L                          " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = EDI_L.OUTKA_CTL_NO                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = EDI_L.NRS_BR_CD                          " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " EDI_L.SYS_DEL_FLG = '0'                                      " & vbNewLine

    Private Const SQL_WHERE_MATOME_TARGET_FLAG8 As String = "WHERE                                   " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD  = @NRS_BR_CD                              " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.OUTKA_PLAN_DATE  = @OUTKA_PLAN_DATE                  " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.OUTKO_DATE       = @OUTKO_DATE                       " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.ARR_PLAN_DATE    = @ARR_PLAN_DATE                    " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.OUTKA_STATE_KB  < '50'                               " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.SYS_DEL_FLG  = '0'                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.UNSO_CD  = @UNSO_CD                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.UNSO_BR_CD  = @UNSO_BR_CD                             " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.CUST_CD_L  = @CUST_CD_L                              " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.CUST_CD_M  = @CUST_CD_M                              " & vbNewLine

    Private Const SQL_GROUPBY_MATOME_TARGET_FLAG8 As String = "GROUP BY                                 " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD                                               " & vbNewLine _
                                    & ",OUTKA_L.OUTKA_NO_L                                              " & vbNewLine _
                                    & ",CASE WHEN (SUBSTRING(EDI_L.FREE_C30,4,9) <> ''                  " & vbNewLine _
                                    & "      AND SUBSTRING(EDI_L.FREE_C30,1,2) = '04')                  " & vbNewLine _
                                    & "      THEN SUBSTRING(EDI_L.FREE_C30,4,9)                         " & vbNewLine _
                                    & "      ELSE EDI_L.EDI_CTL_NO                                      " & vbNewLine _
                                    & "      END                                                        " & vbNewLine _
                                    & ",OUTKA_L.SYS_UPD_DATE                                            " & vbNewLine _
                                    & ",OUTKA_L.SYS_UPD_TIME                                            " & vbNewLine _
                                    & ",UNSO_L.UNSO_NO_L                                                " & vbNewLine _
                                    & ",UNSO_L.UNSO_PKG_NB                                              " & vbNewLine _
                                    & ",UNSO_L.SYS_UPD_DATE                                             " & vbNewLine _
                                    & ",UNSO_L.SYS_UPD_TIME                                             " & vbNewLine _
                                    & ",OUTKA_L.OUTKA_PKG_NB                                            " & vbNewLine _
                                    & ",OUTKA_L.NHS_REMARK                                              " & vbNewLine _
                                    & ",OUTKA_L.REMARK                                                  " & vbNewLine _
                                    & ",OUTKA_L.BUYER_ORD_NO                                            " & vbNewLine _
                                    & " -- 要望番号922 追加START                                        " & vbNewLine _
                                    & ",OUTKA_L.CUST_ORD_NO                                             " & vbNewLine _
                                    & " -- 要望番号922 追加END                                          " & vbNewLine _
                                    & " ) QUERY     "

#End Region

#Region "出荷登録処理 まとめ先取得SQL(まとめF=9):共通1の複製(ＤＩＣ物流群馬用)"
    Private Const SQL_SELECT_MATOME_TARGET_FLAG9 As String = "SELECT                                 " & vbNewLine _
                                    & " QUERY.OUTKA_CTL_NO                                           " & vbNewLine _
                                    & ",QUERY.OUTKA_CTL_NO_CHU                                       " & vbNewLine _
                                    & ",QUERY.EDI_CTL_NO                                             " & vbNewLine _
                                    & ",QUERY.SYS_UPD_DATE                                           " & vbNewLine _
                                    & ",QUERY.SYS_UPD_TIME                                           " & vbNewLine _
                                    & ",QUERY.UNSO_NO_L                                              " & vbNewLine _
                                    & ",QUERY.UNSO_NO_M                                              " & vbNewLine _
                                    & ",QUERY.SYS_UNSO_UPD_DATE                                      " & vbNewLine _
                                    & ",QUERY.SYS_UNSO_UPD_TIME                                      " & vbNewLine _
                                    & ",QUERY.OUTKA_PKG_NB                                           " & vbNewLine _
                                    & ",QUERY.UNSO_PKG_NB                                            " & vbNewLine _
                                    & ",QUERY.NRS_BR_CD                                              " & vbNewLine _
                                    & ",QUERY.NHS_REMARK                                             " & vbNewLine _
                                    & ",QUERY.REMARK                                                 " & vbNewLine _
                                    & ",QUERY.BUYER_ORD_NO                                           " & vbNewLine _
                                    & ",QUERY.CUST_ORD_NO                                            " & vbNewLine _
                                    & ",QUERY.OUTKA_STATE_KB_1 AS OUTKA_STATE_KB                     " & vbNewLine _
                                    & " FROM                                                         " & vbNewLine _
                                    & " (SELECT                                                      " & vbNewLine _
                                    & "    OUTKA_L.NRS_BR_CD             AS  NRS_BR_CD               " & vbNewLine _
                                    & "	   ,OUTKA_L.OUTKA_NO_L           AS  OUTKA_CTL_NO            " & vbNewLine _
                                    & "    ,MAX(OUTKA_M.OUTKA_NO_M)      AS  OUTKA_CTL_NO_CHU        " & vbNewLine _
                                    & "    ,CASE WHEN (SUBSTRING(EDI_L.FREE_C30,4,9) <> ''           " & vbNewLine _
                                    & "     AND SUBSTRING(EDI_L.FREE_C30,1,2) = '04')                " & vbNewLine _
                                    & "     THEN SUBSTRING(EDI_L.FREE_C30,4,9)                       " & vbNewLine _
                                    & "     ELSE EDI_L.EDI_CTL_NO                                    " & vbNewLine _
                                    & "     END  EDI_CTL_NO                                          " & vbNewLine _
                                    & "    ,OUTKA_L.SYS_UPD_DATE         AS  SYS_UPD_DATE            " & vbNewLine _
                                    & "    ,OUTKA_L.SYS_UPD_TIME         AS  SYS_UPD_TIME            " & vbNewLine _
                                    & "    ,UNSO_L.UNSO_NO_L             AS  UNSO_NO_L               " & vbNewLine _
                                    & "    ,MAX(UNSO_M.UNSO_NO_M)        AS  UNSO_NO_M               " & vbNewLine _
                                    & "    ,UNSO_L.SYS_UPD_DATE          AS  SYS_UNSO_UPD_DATE       " & vbNewLine _
                                    & "    ,UNSO_L.SYS_UPD_TIME          AS  SYS_UNSO_UPD_TIME       " & vbNewLine _
                                    & "    ,OUTKA_L.OUTKA_PKG_NB         AS  OUTKA_PKG_NB            " & vbNewLine _
                                    & "    ,UNSO_L.UNSO_PKG_NB           AS  UNSO_PKG_NB             " & vbNewLine _
                                    & "    ,OUTKA_L.NHS_REMARK           AS  NHS_REMARK              " & vbNewLine _
                                    & "    ,OUTKA_L.REMARK               AS  REMARK                  " & vbNewLine _
                                    & "    ,OUTKA_L.BUYER_ORD_NO         AS  BUYER_ORD_NO            " & vbNewLine _
                                    & "    ,OUTKA_L.CUST_ORD_NO          AS  CUST_ORD_NO             " & vbNewLine _
                                    & "    ,CASE MAX(ISNULL(OUTKA_L.OUTKA_STATE_KB,''))              " & vbNewLine _
                                    & "    WHEN '' THEN '99'                                         " & vbNewLine _
                                    & "    WHEN '10' THEN '10'                                       " & vbNewLine _
                                    & "    WHEN '60' THEN '60'                                       " & vbNewLine _
                                    & "    WHEN '90' THEN '90'                                       " & vbNewLine _
                                    & "    ELSE '50'                                                 " & vbNewLine _
                                    & "   END AS OUTKA_STATE_KB_1                                    " & vbNewLine _
                                    & "FROM                                                          " & vbNewLine _
                                    & " $LM_TRN$..C_OUTKA_L           OUTKA_L                        " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..C_OUTKA_M           OUTKA_M                        " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = OUTKA_M.OUTKA_NO_L                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = OUTKA_M.NRS_BR_CD                        " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_M.SYS_DEL_FLG  = '0'                                   " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..F_UNSO_L            UNSO_L                         " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = UNSO_L.INOUTKA_NO_L                     " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = UNSO_L.NRS_BR_CD                         " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.MOTO_DATA_KB = '20'                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..F_UNSO_M            UNSO_M                         " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " UNSO_L.UNSO_NO_L = UNSO_M.UNSO_NO_L                          " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = UNSO_M.NRS_BR_CD                         " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_M.SYS_DEL_FLG = '0'                                     " & vbNewLine _
                                    & "INNER JOIN                                                    " & vbNewLine _
                                    & " $LM_TRN$..H_OUTKAEDI_L        EDI_L                          " & vbNewLine _
                                    & " ON                                                           " & vbNewLine _
                                    & " OUTKA_L.OUTKA_NO_L = EDI_L.OUTKA_CTL_NO                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD = EDI_L.NRS_BR_CD                          " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " EDI_L.SYS_DEL_FLG = '0'                                      " & vbNewLine

    Private Const SQL_WHERE_MATOME_TARGET_FLAG9 As String = "WHERE                                   " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD  = @NRS_BR_CD                              " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.OUTKA_PLAN_DATE  = @OUTKA_PLAN_DATE                  " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.OUTKO_DATE  = @OUTKO_DATE                            " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.ARR_PLAN_DATE  = @ARR_PLAN_DATE                      " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " --(2012.09.19)引当以上のステータスはまとめない               " & vbNewLine _
                                    & " OUTKA_L.OUTKA_STATE_KB  < '40'                               " & vbNewLine _
                                    & " --OUTKA_L.OUTKA_STATE_KB  < '60'                             " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.SYS_DEL_FLG  = '0'                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.UNSO_CD  = @UNSO_CD                                   " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.UNSO_BR_CD  = @UNSO_BR_CD                             " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.CUST_CD_L  = @CUST_CD_L                              " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " OUTKA_L.CUST_CD_M  = @CUST_CD_M                              " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " UNSO_L.BIN_KB  = @BIN_KB                                     " & vbNewLine _
                                    & " AND                                                          " & vbNewLine _
                                    & " EDI_L.UNSO_ATT  = @UNSO_ATT                                  " & vbNewLine

    Private Const SQL_GROUPBY_MATOME_TARGET_FLAG9 As String = "GROUP BY                                 " & vbNewLine _
                                    & " OUTKA_L.NRS_BR_CD                                               " & vbNewLine _
                                    & ",OUTKA_L.OUTKA_NO_L                                              " & vbNewLine _
                                    & ",CASE WHEN (SUBSTRING(EDI_L.FREE_C30,4,9) <> ''                  " & vbNewLine _
                                    & "      AND SUBSTRING(EDI_L.FREE_C30,1,2) = '04')                  " & vbNewLine _
                                    & "      THEN SUBSTRING(EDI_L.FREE_C30,4,9)                         " & vbNewLine _
                                    & "      ELSE EDI_L.EDI_CTL_NO                                      " & vbNewLine _
                                    & "      END                                                        " & vbNewLine _
                                    & ",OUTKA_L.SYS_UPD_DATE                                            " & vbNewLine _
                                    & ",OUTKA_L.SYS_UPD_TIME                                            " & vbNewLine _
                                    & ",UNSO_L.UNSO_NO_L                                                " & vbNewLine _
                                    & ",UNSO_L.UNSO_PKG_NB                                              " & vbNewLine _
                                    & ",UNSO_L.SYS_UPD_DATE                                             " & vbNewLine _
                                    & ",UNSO_L.SYS_UPD_TIME                                             " & vbNewLine _
                                    & ",OUTKA_L.OUTKA_PKG_NB                                            " & vbNewLine _
                                    & ",OUTKA_L.NHS_REMARK                                              " & vbNewLine _
                                    & ",OUTKA_L.REMARK                                                  " & vbNewLine _
                                    & ",OUTKA_L.BUYER_ORD_NO                                            " & vbNewLine _
                                    & ",OUTKA_L.CUST_ORD_NO                                             " & vbNewLine _
                                    & " ) QUERY     "

#End Region

#Region "タブレット初期値取得"

#Region "SQL_SELECT_TABLET_YN_Z_KBN"
    Private Const SQL_SELECT_TABLET_YN_Z_KBN As String = " SELECT                              " & vbNewLine _
                                     & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..Z_KBN                        Z_KBN          " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & "     Z_KBN.KBN_GROUP_CD = @KBN_GROUP_CD                " & vbNewLine _
                                     & " AND Z_KBN.KBN_CD   = @KBN_CD                          " & vbNewLine _
                                     & " AND Z_KBN.VALUE1   = @VALUE1                          " & vbNewLine
#End Region


#Region "SQL_SELECT_LOC_MNG_YN_M_SOKO"
    Private Const SQL_SELECT_LOC_MNG_YN_M_SOKO As String = " SELECT                            " & vbNewLine _
                                     & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_SOKO                       SOKO           " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & "     SOKO.NRS_BR_CD      = @NRS_BR_CD                  " & vbNewLine _
                                     & " AND SOKO.WH_CD          = @WH_CD                      " & vbNewLine _
                                     & " AND SOKO.LOC_MANAGER_YN in ('01','02')                " & vbNewLine

#End Region

#Region "SQL_SELECT_TABLET_YN_M_CUST_DETAILS"
    Private Const SQL_SELECT_TABLET_YN_M_CUST_DETAILS As String = " SELECT                      " & vbNewLine _
                                     & " COUNT(*) AS MST_CNT                                    " & vbNewLine _
                                     & " FROM                                                   " & vbNewLine _
                                     & " LM_MST..M_CUST_DETAILS M_CUST_DETAILS                  " & vbNewLine _
                                     & " WHERE                                                  " & vbNewLine _
                                     & "     M_CUST_DETAILS.NRS_BR_CD = @NRS_BR_CD              " & vbNewLine _
                                     & " AND M_CUST_DETAILS.CUST_CD   = @CUST_CD                " & vbNewLine _
                                     & " AND M_CUST_DETAILS.SUB_KB    = '1O'                    " & vbNewLine _
                                     & " AND M_CUST_DETAILS.SET_NAIYO = '1'                     " & vbNewLine
#End Region

#End Region

#Region "Const"

    ''' <summary>
    ''' まとめフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Class MatomeFlgType

        ''' <summary>
        ''' 共通1
        ''' </summary>
        ''' <remarks></remarks>
        Public Const COMMON_1 As String = "1"

        ''' <summary>
        ''' 共通2
        ''' </summary>
        ''' <remarks></remarks>
        Public Const COMMON_2 As String = "2"

        ''' <summary>
        ''' 日立物流
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DIC As String = "3"

        ''' <summary>
        ''' 大日精化
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DAI_COLOR As String = "6"

        ''' <summary>
        ''' ビックケミー
        ''' </summary>
        ''' <remarks></remarks>
        Public Const BIC As String = "7"

        ''' <summary>
        ''' フィルメニッヒ
        ''' </summary>
        ''' <remarks></remarks>
        Public Const FIR As String = "8"

        ''' <summary>
        ''' ＤＩＣ物流群馬
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DIC_00076 As String = "9"

        ''' <summary>
        ''' (土気)テルモ サンプル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TrmSmpl00409_01 As String = "A"

        ''' <summary>
        ''' JASM
        ''' </summary>
        ''' <remarks></remarks>
        Public Const JASM As String = "B"

    End Class

#End If

#End Region 'Const

#Region "Field"

    ''' <summary>
    ''' 条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' 発行SQL作成用
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql As StringBuilder

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

    ''' <summary>
    ''' ORDER BY句作成
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSqlOrderBy As StringBuilder

#End Region 'Field

#Region "Method"

#Region "マスタ値取得処理"

#Region "区分マスタ"

    ''' <summary>
    ''' 件数取得処理(区分マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataZkbn(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_Z_KBN)

        Dim dt As DataTable = ds.Tables("LMH030_JUDGE")
        Dim dtL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetKbnParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dtL.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataZkbn", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function


    ''' <summary>
    ''' 区分マスタ取得（汎用）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function SelectZKbnHanyo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_Z_KBN_IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_Z_KBN_HANYO)
        If Me._Row.Item("KBN_CD") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_CD").ToString() <> "" Then
            Me._StrSql.Append("   AND KBN_CD = @KBN_CD                " & vbNewLine)
        End If
        If Me._Row.Item("KBN_NM1") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM1").ToString() <> "" Then
            Me._StrSql.Append("   AND KBN_NM1 = @KBN_NM1              " & vbNewLine)
        End If
#If True Then  'UPD 2020/03/04 011299   【LMS】FFEM大牟田対応_EDI入出荷登録プラントコード＋保管場所別登録（FFEM渡邉様
        If Me._Row.Item("KBN_NM2") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM2").ToString() <> "" Then
            Me._StrSql.Append("   AND KBN_NM2 = @KBN_NM2              " & vbNewLine)
        End If
        If Me._Row.Item("KBN_NM3") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM3").ToString() <> "" Then
            Me._StrSql.Append("   AND KBN_NM3 = @KBN_NM3              " & vbNewLine)
        End If
        If Me._Row.Item("KBN_NM4") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM4").ToString() <> "" Then
            Me._StrSql.Append("   AND KBN_NM4 = @KBN_NM4              " & vbNewLine)
        End If

#End If
        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_GROUP_CD", Me._Row.Item("KBN_GROUP_CD").ToString(), DBDataType.NVARCHAR))
        If Me._Row.Item("KBN_CD") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_CD").ToString() <> "" Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_CD", Me._Row.Item("KBN_CD").ToString(), DBDataType.NVARCHAR))
        End If
        If Me._Row.Item("KBN_NM1") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM1").ToString() <> "" Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_NM1", Me._Row.Item("KBN_NM1").ToString(), DBDataType.NVARCHAR))
        End If

#If True Then  'UPD 2020/03/04 011299   【LMS】FFEM大牟田対応_EDI入出荷登録プラントコード＋保管場所別登録（FFEM渡邉様
        If Me._Row.Item("KBN_NM2") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM2").ToString() <> "" Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_NM2", Me._Row.Item("KBN_NM2").ToString(), DBDataType.NVARCHAR))
        End If
        If Me._Row.Item("KBN_NM3") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM3").ToString() <> "" Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_NM3", Me._Row.Item("KBN_NM3").ToString(), DBDataType.NVARCHAR))
        End If
        If Me._Row.Item("KBN_NM4") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM4").ToString() <> "" Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_NM4", Me._Row.Item("KBN_NM4").ToString(), DBDataType.NVARCHAR))
        End If

#End If
        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectZKbnHanyo", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("KBN_GROUP_CD", "KBN_GROUP_CD")
        map.Add("KBN_CD", "KBN_CD")
        map.Add("KBN_KEYWORD", "KBN_KEYWORD")
        map.Add("KBN_NM1", "KBN_NM1")
        map.Add("KBN_NM2", "KBN_NM2")
        map.Add("KBN_NM3", "KBN_NM3")
        map.Add("KBN_NM4", "KBN_NM4")
        map.Add("KBN_NM5", "KBN_NM5")
        map.Add("KBN_NM6", "KBN_NM6")
        map.Add("KBN_NM7", "KBN_NM7")
        map.Add("KBN_NM8", "KBN_NM8")
        map.Add("KBN_NM9", "KBN_NM9")
        map.Add("KBN_NM10", "KBN_NM10")
        map.Add("KBN_NM11", "KBN_NM11")
        map.Add("KBN_NM12", "KBN_NM12")
        map.Add("KBN_NM13", "KBN_NM13")
        map.Add("VALUE1", "VALUE1")
        map.Add("VALUE2", "VALUE2")
        map.Add("VALUE3", "VALUE3")
        map.Add("SORT", "SORT")
        map.Add("REM", "REM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_Z_KBN_OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMH030_Z_KBN_OUT").Rows.Count())
        reader.Close()

        Return ds

    End Function

#End Region

#Region "区分マスタ(荷姿)重量取得"

    ''' <summary>
    ''' 件数取得処理(区分マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataPkgUtZkbn(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_PKG_UT_Z_KBN)

        Dim dt As DataTable = ds.Tables("LMH030_JUDGE")
        Dim dtM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        Dim mCnt As Integer = dtM.Rows.Count - 1

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetKbnParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dtM.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectNisugataValue", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        '荷姿件数の設定
        Dim nisugataCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NIS_CNT", "NIS_CNT")
        map.Add("NISUGATA", "NISUGATA")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_Z_KBN")

        'reader.Read()
        'dtM.Rows(0)("NIS_CNT") = reader("NIS_CNT")
        'dtM.Rows(0)("NISUGATA") = reader("NISUGATA")

        nisugataCnt = Convert.ToInt32(ds.Tables("LMH030_Z_KBN").Rows(0)("NIS_CNT"))

        reader.Close()

        Return ds

        MyBase.SetResultCount(nisugataCnt)

    End Function

#End Region

#Region "倉庫マスタ"

    ''' <summary>
    ''' M_SOKO
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataSoko(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_WH)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSokoParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataSoko", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function

#End Region

#Region "郵便番号マスタ"

    ''' <summary>
    ''' M_ZIP
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataMzip(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_ZIP)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetZipParameter(dt)

        'SQL条件設定(Group by)
        Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_M_ZIP)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataMzip", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ''郵便番号件数の設定
        Dim zipCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            Call Me.SetZipMap(reader, map)

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_ZIP")

            '処理件数の設定
            zipCnt = ds.Tables("LMH030_M_ZIP").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(zipCnt)
        Return ds
    End Function

#End Region

    '2012.03.23 大阪対応追加START
#Region "休日マスタ"

    ''' <summary>
    ''' M_HOL
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMHolList(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_HOL)

        Dim dt As DataTable = ds.Tables("LMH030_M_HOL")
        Dim dtSemi As DataTable = ds.Tables("LMH030_SEMIEDI_INFO")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetHolParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dtSemi.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectMHolList", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds

    End Function

#End Region
    '2012.03.23 大阪対応追加END

#Region "荷主マスタ件数"

    ''' <summary>
    ''' 件数取得処理(荷主マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataMcust(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_CUST)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetMcustParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataMcust", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function

#End Region

#Region "荷主マスタ(運賃計算締め基準)取得"

    ''' <summary>
    ''' 件数取得処理(荷主マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function GetUnthinCalculationKb(ByVal ds As DataSet) As String

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_CUST_CALCULATION)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetMcustParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectUnthinCalculationKb", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Dim calucKb As String = reader("UNTIN_CALCULATION_KB").ToString()
        Dim strDate As String = String.Empty
        Select Case calucKb

            '出荷日
            Case "01"
                strDate = dt.Rows(0)("OUTKA_PLAN_DATE").ToString()
                '納入日
            Case "02"
                strDate = dt.Rows(0)("ARR_PLAN_DATE").ToString()
            Case Else

        End Select
        reader.Close()

        Return strDate
    End Function

#End Region

    '★★★2012.01.12 要望番号596 START
#Region "届先マスタ(荷送人コード検索の場合)"

    ''' <summary>
    ''' 件数取得処理(届先マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataMdestShip(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_DEST)
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("AND M_DEST.DEST_CD = @SHIP_CD_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetMdestShipParameter(dt)

        'SQL条件設定(Group by)
        Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_M_DEST)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataMdestShip", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ''届先件数の設定
        Dim destCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            Call Me.SetDestMap(reader, map)

            '2012.02.25 大阪対応 START
            'ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_DEST")
            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_DEST_SHIP_CD_L")

            '処理件数の設定
            'destCnt = ds.Tables("LMH030_M_DEST").Rows.Count
            destCnt = ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows.Count
            '2012.02.25 大阪対応 END

        End If

        reader.Close()

        MyBase.SetResultCount(destCnt)
        Return ds
    End Function

#End Region
    '★★★2012.01.12 要望番号596 END

#Region "届先マスタ"

    ''' <summary>
    ''' 件数取得処理(届先マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataMdest(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_DEST)

        If dt.Rows(0)("DEST_CD").ToString() = String.Empty Then
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND M_DEST.EDI_CD = @EDI_DEST_CD")
        Else
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append("AND M_DEST.DEST_CD = @DEST_CD")
        End If

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetMdestParameter(dt)

        'SQL条件設定(Group by)
        Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_M_DEST)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataMdest", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ''届先件数の設定
        Dim destCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            Call Me.SetDestMap(reader, map)

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_DEST")

            '処理件数の設定
            destCnt = ds.Tables("LMH030_M_DEST").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(destCnt)
        Return ds
    End Function

#End Region

    '2012.02.13 大阪追加START
#Region "運送会社マスタ(データ取得)"

    ''' <summary>
    ''' データ取得処理(運送会社マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataMUnsocoList(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_UNSOCO_LIST)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUnsocoParameter(dt)

        'SQL条件設定(Group by)
        Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_M_UNSOCO_LIST)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataMUnsocoList", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ''届先件数の設定
        Dim UnsocoCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            Call Me.SetUnsocoMap(reader, map)

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_UNSOCO")

            '処理件数の設定
            UnsocoCnt = ds.Tables("LMH030_M_UNSOCO").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(UnsocoCnt)
        Return ds
    End Function

#End Region
    '2012.02.13 追加END

    'START UMANO 要望番号1302 支払運賃に伴う修正。
#Region "運送会社マスタ(支払いタリフ用)"
    ''' <summary>
    ''' データ取得処理(運送会社マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListDataShiharaiTariff(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_UNSOCO_SHIHARAI)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUnsocoParameter(dt)

        'SQL条件設定(Group by)
        Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_M_UNSOCO_SHIHARAI)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectListDataShiharaiTariff", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ''荷主明細件数の設定
        Dim unsoShiharaiCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            map.Add("MST_CNT", "MST_CNT")
            map.Add("SHIHARAITO_CD", "SHIHARAITO_CD")
            map.Add("UNCHIN_TARIFF_CD", "UNCHIN_TARIFF_CD")
            map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD")
            map.Add("BETU_KYORI_CD", "BETU_KYORI_CD")

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_SHIHARAI_TARIFF")

            '処理件数の設定
            unsoShiharaiCnt = ds.Tables("LMH030_SHIHARAI_TARIFF").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(unsoShiharaiCnt)
        Return ds

    End Function

#End Region
    'END UMANO 要望番号1302 支払運賃に伴う修正。

#Region "運送会社マスタ"

    ''' <summary>
    ''' 件数取得処理(運送会社マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataUnsoco(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_UNSOCO)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUnsocoParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataUnsoco", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function

#End Region

    '要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 Start
#Region "運送会社荷主別送り状マスタ"

    ''' <summary>
    ''' 件数取得処理(運送会社荷主別送り状マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataUnsoCustRpt(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_UNSO_CUST_RPT)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUnsoCustRptParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataUnsoCustRpt", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function

#End Region
    '要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 End

#Region "運賃タリフマスタ"

    ''' <summary>
    ''' 件数取得処理(運賃タリフマスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataMunchinTariff(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        Dim strDate As String = Me.GetUnthinCalculationKb(ds)

        'SQL格納変数の再初期化
        Me._StrSql = New StringBuilder()

        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_UNCHIN_TARIFF)

        'パラメータ設定
        Call Me.SetUnchinTariffParameter(dt, strDate)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataMunchinTariff", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()

        Return ds
    End Function

#End Region

#Region "割増運賃タリフマスタ"

    ''' <summary>
    ''' 件数取得処理(運賃タリフマスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataMextcUnchin(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_EXTC_UNCHIN)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetExtcUnchinParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataMextcUnchin", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function

#End Region

#Region "横持ちタリフヘッダー"

    ''' <summary>
    ''' 件数取得処理(横持ちタリフヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataMyokoTariffHd(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_YOKO_TARIFF)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetYokoTariffParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataMyokoTariffHd", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function

#End Region

#Region "商品マスタ(運送データの場合)"

    ''' <summary>
    ''' 件数取得処理(商品マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataMgoodsUnso(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_GOODS_UNSODATA)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        Me._SqlPrmList = New ArrayList()

        'SQL条件設定(Group by)
        Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_M_GOODS)

        'パラメータ設定
        Call Me.SetGoodsUnsoParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataMgoodsUnso", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)


        '★★★2011.11.14 要望番号503 修正START
        'reader.Read()

        ''商品件数の設定
        Dim goodsCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            'goodsCnt = Convert.ToInt32(reader("MST_CNT"))

            Call Me.SetGoodsMap(reader, map)

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_GOODS")

            '処理件数の設定
            goodsCnt = ds.Tables("LMH030_M_GOODS").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(goodsCnt)
        Return ds

        ''処理件数の設定
        'reader.Read()
        'MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        'reader.Close()
        'Return ds
        '★★★2011.11.14 要望番号503 修正END

    End Function

#End Region

#Region "商品マスタ(出荷データの場合)"

    ''' <summary>
    ''' 件数取得処理(商品マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataMgoodsOutka(ByVal ds As DataSet) As DataSet

        '2011.10.06 START EDI(メモ)№79
        Dim dtL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        '2011.10.06 END

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_GOODS)

        Me._SqlPrmList = New ArrayList()

        'SQL条件設定
        '2011.10.06 START EDI(メモ)№79
        Call Me.SetConditionGoodCd(dtM, 1)
        '2011.10.06 END

        'SQL条件設定(Group by)
        Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_M_GOODS)

        'パラメータ設定
        '2011.10.06 START EDI(メモ)№79
        Call Me.SetGoodsOutkaParameter(dtL, dtM)
        '2011.10.06 END

        'スキーマ設定
        '2011.10.06 START EDI(メモ)№79
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dtM.Rows(0).Item("NRS_BR_CD").ToString())
        '2011.10.06 END

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataMgoods", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'reader.Read()

        ''商品件数の設定
        Dim goodsCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            'goodsCnt = Convert.ToInt32(reader("MST_CNT"))

            Call Me.SetGoodsMap(reader, map)

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_GOODS")

            '処理件数の設定
            goodsCnt = ds.Tables("LMH030_M_GOODS").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(goodsCnt)
        Return ds

    End Function

#If True Then   'ADD 2018/12/14 LIKE指定　依頼番号 : 003818   【LMS】ITWセミEDI_エクセル取込時


    ''' <summary>
    ''' 件数取得処理(商品マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataMgoodsOutkaLike(ByVal ds As DataSet) As DataSet

        '2011.10.06 START EDI(メモ)№79
        Dim dtL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        '2011.10.06 END

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_GOODS)

        Me._SqlPrmList = New ArrayList()

        'SQL条件設定
        '2011.10.06 START EDI(メモ)№79
        Call Me.SetConditionGoodCdLike(dtM, 1)
        '2011.10.06 END

        'SQL条件設定(Group by)
        Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_M_GOODS)

        'パラメータ設定
        '2011.10.06 START EDI(メモ)№79
        Call Me.SetGoodsOutkaParameterLike(dtL, dtM)
        '2011.10.06 END

        'スキーマ設定
        '2011.10.06 START EDI(メモ)№79
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dtM.Rows(0).Item("NRS_BR_CD").ToString())
        '2011.10.06 END

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataMgoods", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'reader.Read()

        ''商品件数の設定
        Dim goodsCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            'goodsCnt = Convert.ToInt32(reader("MST_CNT"))

            Call Me.SetGoodsMap(reader, map)

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_GOODS")

            '処理件数の設定
            goodsCnt = ds.Tables("LMH030_M_GOODS").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(goodsCnt)
        Return ds

    End Function
#End If

#End Region

#Region "商品マスタ前方一致検索(出荷データの場合)"

    ''' <summary>
    ''' 件数取得処理(商品マスタ前方一致検索)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataMgoodsPrefixOutka(ByVal ds As DataSet) As DataSet

        Dim dtL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_GOODS)

        Me._SqlPrmList = New ArrayList()

        'SQL条件設定
        Call Me.SetConditionGoodCdPrefix(dtM, 1)

        'SQL条件設定(Group by)
        Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_M_GOODS)

        'パラメータ設定
        Call Me.SetGoodsPrefixOutkaParameter(dtL, dtM)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dtM.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataMgoodsPrefixOutka", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'reader.Read()

        ''商品件数の設定
        Dim goodsCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            'goodsCnt = Convert.ToInt32(reader("MST_CNT"))

            Call Me.SetGoodsMap(reader, map)

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_GOODS")

            '処理件数の設定
            goodsCnt = ds.Tables("LMH030_M_GOODS").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(goodsCnt)
        Return ds

    End Function

#End Region

#Region "商品マスタ(出荷データ + 商品マスタ入目検索の場合)"

    ''' <summary>
    ''' 件数取得処理(商品マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataMgoodsIrimeOutka(ByVal ds As DataSet) As DataSet

        '2011.10.06 START EDI(メモ)№79
        Dim dtL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        '2011.10.06 END

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_GOODS)

        Me._SqlPrmList = New ArrayList()

        'SQL条件設定
        '2011.10.06 START EDI(メモ)№79
        Call Me.SetConditionGoodCd(dtM, 2)
        '2011.10.06 END

        'SQL条件設定(Group by)
        Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_M_GOODS)

        'パラメータ設定
        '2011.10.06 START EDI(メモ)№79
        Call Me.SetGoodsOutkaParameter(dtL, dtM)
        '2011.10.06 END

        'スキーマ設定
        '2011.10.06 START EDI(メモ)№79
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dtM.Rows(0).Item("NRS_BR_CD").ToString())
        '2011.10.06 END

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataMgoodsIrime", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'reader.Read()

        ''商品件数の設定
        Dim goodsCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            'goodsCnt = Convert.ToInt32(reader("MST_CNT"))

            Call Me.SetGoodsMap(reader, map)

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_GOODS")

            '処理件数の設定
            goodsCnt = ds.Tables("LMH030_M_GOODS").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(goodsCnt)
        Return ds

    End Function

#End Region

#Region "商品マスタ + 商品明細(出荷データの場合)"

    ''' <summary>
    ''' 件数取得処理(商品マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataMgoodsDetailsPrefixOutka(ByVal ds As DataSet) As DataSet

        '2011.10.06 START EDI(メモ)№79
        Dim dtL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        '2011.10.06 END

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_GOODS_DETAILS)

        Me._SqlPrmList = New ArrayList()

        'SQL条件設定
        Call Me.SetConditionGoodCdDetailsPrifix(dtM)

        'SQL条件設定(Group by)
        Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_M_GOODS_DETAILS)

        'パラメータ設定
        Call Me.SetGoodsDetailsPrefixOutkaParameter(dtL, dtM)

        'スキーマ設定
        '2011.10.06 START EDI(メモ)№79
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dtM.Rows(0).Item("NRS_BR_CD").ToString())
        '2011.10.06 END

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataMgoodsDetailsPrefixOutka", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'reader.Read()

        ''商品件数の設定
        Dim goodsCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            'goodsCnt = Convert.ToInt32(reader("MST_CNT"))

            Call Me.SetGoodsMap(reader, map)

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_GOODS")

            '処理件数の設定
            goodsCnt = ds.Tables("LMH030_M_GOODS").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(goodsCnt)
        Return ds

    End Function

#End Region

#If True Then   'ADD 2019/04/14 依頼番号 : 005430 LMS】FFEM_東京工場分がEDIデータとして含まれてしまう

#Region "運送(大)データ(オーダー番号チェック用)"

    ''' <summary>
    ''' 件数取得処理(C_OUTKA_L)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>オーダー番号重複チェック</remarks>
    Private Function SelectOrderCheckUnsoData(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_F_UNSO_L)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetInkaParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectOrderCheckUnsoData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("UNSO_L_CNT")))
        reader.Close()
        Return ds
    End Function

#End Region
#End If

#Region "出荷(大)データ(オーダー番号チェック用)"

    ''' <summary>
    ''' 件数取得処理(C_OUTKA_L)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>オーダー番号重複チェック</remarks>
    Private Function SelectOrderCheckData(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_C_OUTKA_L)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetInkaParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataOrderCheck", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("OUTKA_L_CNT")))
        reader.Close()
        Return ds
    End Function

#End Region

    '2014.04.09 オーダー番号をまとめデータも対象にチェックをする --ST--
#Region "出荷(大)データ(オーダー番号チェック用[まとめデータもチェック対象])"
    ''' <summary>
    ''' 件数取得処理(H_OUTKAEDI_L/C_OUTKA_L)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>オーダー番号重複チェック[まとめデータもチェック対象]</remarks>
    Private Function SelectOrderCheckDataInSum(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_C_OUTKA_L_INSUM)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetInkaParameterInSum(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataOrderCheckInSum", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("OUTKA_L_CNT")))
        reader.Close()
        Return ds
    End Function

#End Region
    '2014.04.09 オーダー番号をまとめデータも対象にチェックをする --ED--

#Region "JISマスタ"
    ''' <summary>
    ''' JISマスタを郵便番号から取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectDataMjisFromZip(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_JIS_FROM_ZIP)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetJisParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataMjisFromZip", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ''郵便番号件数の設定
        Dim jisCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            map.Add("JIS_CD", "JIS_CD")

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_ZIP")

            '処理件数の設定
            jisCnt = ds.Tables("LMH030_M_ZIP").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(jisCnt)
        Return ds

    End Function

    ''' <summary>
    ''' JISマスタを届先マスタの郵便番号から取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectDataMjisFromDestZip(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_JIS_FROM_ZIP)

        Dim dt As DataTable = ds.Tables("LMH030_M_DEST")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetJisParameterByDestInfo(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataMjisFromDestZip", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ''郵便番号件数の設定
        Dim jisCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            map.Add("JIS_CD", "JIS_CD")

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_ZIP")

            '処理件数の設定
            jisCnt = ds.Tables("LMH030_M_ZIP").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(jisCnt)
        Return ds

    End Function


    ''' <summary>
    ''' JISマスタを住所から取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectDataMjisFromAdd(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_JIS_FROM_ADD)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetJisParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataMjisFromAdd", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ''郵便番号件数の設定
        Dim jisCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            map.Add("JIS_CD", "JIS_CD")

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_JIS")

            '処理件数の設定
            jisCnt = ds.Tables("LMH030_M_JIS").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(jisCnt)
        Return ds

    End Function
#End Region

    '2012.02.25 大阪対応 START
#Region "荷主明細マスタ"
    ''' <summary>
    ''' 件数取得処理(荷主明細マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListDataMcustDetails(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_CUST_DETAILS)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定

        Call Me.SetMcustDetailsParameter(dt)
        Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_M_CUST_DETAILS_COUNT)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectListDataMcustDetails", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ''荷主明細件数の設定
        Dim custDtlCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            map.Add("MST_CNT", "MST_CNT")
            map.Add("NRS_BR_CD", "NRS_BR_CD")
            map.Add("CUST_CD", "CUST_CD")
            map.Add("CUST_CD_EDA", "CUST_CD_EDA")
            map.Add("CUST_CLASS", "CUST_CLASS")
            map.Add("SUB_KB", "SUB_KB")
            map.Add("SET_NAIYO", "SET_NAIYO")
            map.Add("SET_NAIYO_2", "SET_NAIYO_2")
            map.Add("SET_NAIYO_3", "SET_NAIYO_3")
            map.Add("REMARK", "REMARK")

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_CUST_DETAILS")

            '処理件数の設定
            custDtlCnt = ds.Tables("LMH030_M_CUST_DETAILS").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(custDtlCnt)
        Return ds

    End Function

#End Region

    '要望番号1282:((春日部)DICさまEDIにて、異なる届け先で同じCDでくる対策) 2012/07/19 本明 Start
#Region "荷主明細マスタ２"
    ''' <summary>
    ''' 件数取得処理(荷主明細マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListDataMcustDetails2(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_M_CUST_DETAILS2)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetMcustDetailsParameter2(dt)
        Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_M_CUST_DETAILS_COUNT & SQL_ORDER_BY_M_CUST_DETAILS2)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectListDataMcustDetails2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        ''荷主明細件数の設定
        Dim custDtlCnt As Integer = 0

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        If reader.HasRows() = True Then

            map.Add("MST_CNT", "MST_CNT")
            map.Add("NRS_BR_CD", "NRS_BR_CD")
            map.Add("CUST_CD", "CUST_CD")
            map.Add("CUST_CD_EDA", "CUST_CD_EDA")
            map.Add("CUST_CLASS", "CUST_CLASS")
            map.Add("SUB_KB", "SUB_KB")
            map.Add("SET_NAIYO", "SET_NAIYO")
            map.Add("SET_NAIYO_2", "SET_NAIYO_2")
            map.Add("SET_NAIYO_3", "SET_NAIYO_3")
            map.Add("REMARK", "REMARK")

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_CUST_DETAILS")

            '処理件数の設定
            custDtlCnt = ds.Tables("LMH030_M_CUST_DETAILS").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(custDtlCnt)
        Return ds

    End Function

#End Region
    '要望番号1282:((春日部)DICさまEDIにて、異なる届け先で同じCDでくる対策) 2012/07/19 本明 End

#Region "区分マスタ(Tablet使用有無取得)"

    ''' <summary>
    ''' 区分マスタ(Tablet使用有無取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataTabletYN(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_TABLET_YN_Z_KBN)

        Dim dt As DataTable = ds.Tables("LMH030INOUT")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetKbnTabletYNParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataTabletYN", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function
    ''' <summary>
    ''' ロケーション管理有無
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataLocMngYn(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_LOC_MNG_YN_M_SOKO)

        Dim dt As DataTable = ds.Tables("LMH030INOUT")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetSokoLocMngYnParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataLocMngYn", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function

    ''' <summary>
    ''' 荷主明細マスタ(Tablet使用有無取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataCustDtlTabletYN(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_TABLET_YN_M_CUST_DETAILS)

        Dim dt As DataTable = ds.Tables("LMH030INOUT")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetCustDtlTabletYNParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectDataCustDtlTabletYN", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function

    ''' <summary>
    ''' Z_Kbn(Tablet使用有無取得)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKbnTabletYNParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_GROUP_CD", LMKbnConst.KBN_B007, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@VALUE1", 1.0, DBDataType.NUMERIC))

    End Sub

    ''' <summary>
    ''' Z_Kbn(Tablet使用有無取得)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSokoLocMngYnParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", dt.Rows(0).Item("WH_CD"), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' M_CUST_DETAILS(Tablet使用有無取得)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCustDtlTabletYNParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", String.Concat(dt.Rows(0).Item("CUST_CD_L"), dt.Rows(0).Item("CUST_CD_M")), DBDataType.CHAR))

    End Sub

#End Region

#End Region

#Region "スキーマ名称設定"
    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <param name="brCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function
#End Region

#Region "商品マスタ取得条件(出荷データの場合)"
    Private Sub SetConditionGoodCd(ByVal dtM As DataTable, ByVal cnt As Integer)

        Dim goodsCdNrs As String = dtM.Rows(0)("NRS_GOODS_CD").ToString()
        Dim goodsCdCust As String = dtM.Rows(0)("CUST_GOODS_CD").ToString()
        Dim irimeUt As String = dtM.Rows(0)("IRIME_UT").ToString
        Dim strSql As String = String.Empty

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" NRS_BR_CD = @NRS_BR_CD ")
        Me._StrSql.Append(vbNewLine)

        If String.IsNullOrEmpty(goodsCdNrs) = False Then
            Me._StrSql.Append(" AND GOODS_CD_NRS = @NRS_GOODS_CD ")
            Me._StrSql.Append(vbNewLine)
        Else
            Me._StrSql.Append(" AND GOODS_CD_CUST = @CUST_GOODS_CD ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND CUST_CD_L = @CUST_CD_L ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND CUST_CD_M = @CUST_CD_M ")
            Me._StrSql.Append(vbNewLine)
        End If

        If cnt = 2 Then
            Me._StrSql.Append(" AND STD_IRIME_NB = @IRIME ")
            Me._StrSql.Append(vbNewLine)

            If String.IsNullOrEmpty(irimeUt) = False Then
                Me._StrSql.Append(" AND STD_IRIME_UT = @IRIME_UT ")
                Me._StrSql.Append(vbNewLine)
            End If
        End If

        Me._StrSql.Append(" AND SYS_DEL_FLG = '0' ")
        Me._StrSql.Append(vbNewLine)

    End Sub

#If True Then   'ADD 2018/12/14 like指定　依頼番号 : 003818   【LMS】ITWセミEDI_エクセル取込時
    Private Sub SetConditionGoodCdLike(ByVal dtM As DataTable, ByVal cnt As Integer)

        Dim goodsCdNrs As String = dtM.Rows(0)("NRS_GOODS_CD").ToString()
        Dim goodsCdCust As String = dtM.Rows(0)("CUST_GOODS_CD").ToString()
        Dim irimeUt As String = dtM.Rows(0)("IRIME_UT").ToString
        Dim strSql As String = String.Empty

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" NRS_BR_CD = @NRS_BR_CD ")
        Me._StrSql.Append(vbNewLine)

        If String.IsNullOrEmpty(goodsCdNrs) = False Then
            Me._StrSql.Append(" AND GOODS_CD_NRS = @NRS_GOODS_CD ")
            Me._StrSql.Append(vbNewLine)
        Else

            Me._StrSql.Append(" AND GOODS_CD_CUST Like @CUST_GOODS_CD ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND CUST_CD_L = @CUST_CD_L ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND CUST_CD_M = @CUST_CD_M ")
            Me._StrSql.Append(vbNewLine)
        End If

        If cnt = 2 Then
            Me._StrSql.Append(" AND STD_IRIME_NB = @IRIME ")
            Me._StrSql.Append(vbNewLine)

            If String.IsNullOrEmpty(irimeUt) = False Then
                Me._StrSql.Append(" AND STD_IRIME_UT = @IRIME_UT ")
                Me._StrSql.Append(vbNewLine)
            End If
        End If

        Me._StrSql.Append(" AND SYS_DEL_FLG = '0' ")
        Me._StrSql.Append(vbNewLine)

    End Sub
#End If

#End Region

#Region "商品マスタ(商品明細)取得条件(出荷データの場合)"
    Private Sub SetConditionGoodCdDetailsPrifix(ByVal dtM As DataTable)

        Dim strSql As String = String.Empty

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" M_GOODS_DETAILS.NRS_BR_CD = @NRS_BR_CD ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" AND M_GOODS_DETAILS.SET_NAIYO LIKE @CUST_GOODS_CD ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" AND M_GOODS_DETAILS.SYS_DEL_FLG = '0' ")
        Me._StrSql.Append(vbNewLine)
        If Not Me.NullConvertString(dtM.Rows(0).Item("NRS_GOODS_CD")).Equals(String.Empty) Then
            Me._StrSql.Append(" AND M_GOODS.GOODS_CD_NRS LIKE @NRS_GOODS_CD ")
            Me._StrSql.Append(vbNewLine)
        End If

    End Sub
#End Region


#Region "商品マスタ前方一致検索取得条件(出荷データの場合)"
    Private Sub SetConditionGoodCdPrefix(ByVal dtM As DataTable, ByVal cnt As Integer)

        Dim goodsCdNrs As String = dtM.Rows(0)("NRS_GOODS_CD").ToString()
        Dim goodsCdCust As String = dtM.Rows(0)("CUST_GOODS_CD").ToString()
        Dim irimeUt As String = dtM.Rows(0)("IRIME_UT").ToString
        Dim strSql As String = String.Empty

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" NRS_BR_CD = @NRS_BR_CD ")
        Me._StrSql.Append(vbNewLine)

        If String.IsNullOrEmpty(goodsCdNrs) = False Then
            Me._StrSql.Append(" AND GOODS_CD_NRS = @NRS_GOODS_CD ")
            Me._StrSql.Append(vbNewLine)
        Else
            Me._StrSql.Append(" AND GOODS_CD_CUST LIKE @CUST_GOODS_CD ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND CUST_CD_L = @CUST_CD_L ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND CUST_CD_M = @CUST_CD_M ")
            Me._StrSql.Append(vbNewLine)
        End If

        If cnt = 2 Then
            Me._StrSql.Append(" AND STD_IRIME_NB = @IRIME ")
            Me._StrSql.Append(vbNewLine)

            If String.IsNullOrEmpty(irimeUt) = False Then
                Me._StrSql.Append(" AND STD_IRIME_UT = @IRIME_UT ")
                Me._StrSql.Append(vbNewLine)
            End If
        End If

        Me._StrSql.Append(" AND SYS_DEL_FLG = '0' ")
        Me._StrSql.Append(vbNewLine)

    End Sub
#End Region

#Region "区分マスタパラメータ設定"
    ''' <summary>
    ''' 区分マスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKbnParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_GROUP_CD", dt.Rows(0).Item("KBN_GROUP_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_CD", dt.Rows(0).Item("KBN_CD"), DBDataType.CHAR))
    End Sub


#End Region

#Region "荷主マスタパラメータ設定"
    ''' <summary>
    ''' 荷主マスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMcustParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", dt.Rows(0).Item("CUST_CD_M"), DBDataType.CHAR))

    End Sub

#If True Then   'ADD 2019/03/13 依頼番号 : 004085   【LMS】古河事業所日立物流_危険品と一般品の運賃請求：オーダー合算を廃止、別オーダーとして請求
    Private Sub SetMcustParameter2(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L2"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", dt.Rows(0).Item("CUST_CD_M2"), DBDataType.CHAR))

    End Sub
#End If

#End Region

    '2012.02.25 大阪対応 START
#Region "荷主明細マスタパラメータ設定"
    ''' <summary>
    ''' 荷主明細マスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMcustDetailsParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", String.Concat(dt.Rows(0).Item("CUST_CD_L").ToString().Trim(),
                                                                           dt.Rows(0).Item("CUST_CD_M").ToString().Trim()),
                                                                           DBDataType.CHAR))
    End Sub

#End Region
    '2012.02.25 大阪対応 END

    '要望番号1282:((春日部)DICさまEDIにて、異なる届け先で同じCDでくる対策) 2012/07/19 本明 Start
#Region "荷主明細マスタパラメータ設定２"
    ''' <summary>
    ''' 荷主明細マスタのパラメータ設定２
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMcustDetailsParameter2(ByVal dt As DataTable)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", dt.Rows(0).Item("CUST_CD_L"), DBDataType.NVARCHAR))

        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD", String.Concat(dt.Rows(0).Item("CUST_CD_L").ToString().Trim(), _
        '                                                                    dt.Rows(0).Item("CUST_CD_M").ToString().Trim()), _
        '                                                                    DBDataType.CHAR))
    End Sub

#End Region
    '要望番号1282:((春日部)DICさまEDIにて、異なる届け先で同じCDでくる対策) 2012/07/19 本明 End

#Region "届先マスタパラメータ設定(荷送人コード用)"
    ''' <summary>
    ''' 届先マスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMdestShipParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", dt.Rows(0).Item("SHIP_CD_L"), DBDataType.NVARCHAR))

    End Sub

#End Region

#Region "届先マスタパラメータ設定(届先コード,EDI届先コード用)"

    ''' <summary>
    ''' 届先マスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMdestParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", dt.Rows(0).Item("DEST_CD"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DEST_CD", dt.Rows(0).Item("EDI_DEST_CD"), DBDataType.NVARCHAR))

    End Sub

#End Region

#Region "倉庫マスタパラメータ設定"

    ''' <summary>
    ''' 倉庫マスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSokoParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", dt.Rows(0).Item("WH_CD"), DBDataType.CHAR))

    End Sub

#End Region

    '2012.03.23 大阪対応START
#Region "休日マスタパラメータ設定"

    ''' <summary>
    ''' 休日マスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHolParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOL", dt.Rows(0).Item("HOL"), DBDataType.CHAR))

    End Sub

#End Region
    '2012.03.23 大阪対応END

#Region "郵便番号マスタパラメータ設定"

    ''' <summary>
    ''' 郵便番号マスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetZipParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_ZIP", dt.Rows(0).Item("DEST_ZIP"), DBDataType.NVARCHAR))

    End Sub

#End Region

#Region "運送会社マスタパラメータ設定"

    ''' <summary>
    ''' 運送会社マスタパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUnsocoParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", dt.Rows(0).Item("UNSO_CD"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", dt.Rows(0).Item("UNSO_BR_CD"), DBDataType.NVARCHAR))

    End Sub

#End Region

    '要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 Start
#Region "運送会社荷主別送り状マスタパラメータ設定"

    ''' <summary>
    ''' 運送会社荷主別送り状マスタパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUnsoCustRptParameter(ByVal dt As DataTable)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", dt.Rows(0).Item("UNSO_CD"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", dt.Rows(0).Item("UNSO_BR_CD"), DBDataType.NVARCHAR))
    End Sub

#End Region
    '要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 End

#Region "運賃タリフマスタパラメータ設定"

    ''' <summary>
    ''' 運賃タリフマスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUnchinTariffParameter(ByVal dt As DataTable, ByVal strdate As String)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", dt.Rows(0).Item("UNCHIN_TARIFF_CD"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STR_DATE", strdate, DBDataType.CHAR))

    End Sub

#End Region

#Region "割増運賃タリフマスタパラメータ設定"

    ''' <summary>
    ''' 割増運賃タリフマスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetExtcUnchinParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_CD", dt.Rows(0).Item("EXTC_TARIFF_CD"), DBDataType.NVARCHAR))

    End Sub

#End Region

#Region "横持ちタリフヘッダーパラメータ設定"

    ''' <summary>
    ''' 横持ちタリフヘッダーのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetYokoTariffParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKO_TARIFF_CD", dt.Rows(0).Item("UNCHIN_TARIFF_CD"), DBDataType.NVARCHAR))

    End Sub

#End Region

#Region "商品マスタパラメータ設定(運送データ用)"

    ''' <summary>
    ''' 商品マスタパラメータ設定(運送データ用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetGoodsUnsoParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", Me.NullConvertString(dt.Rows(0).Item("NRS_GOODS_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", Me.NullConvertString(dt.Rows(0).Item("CUST_GOODS_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me.NullConvertZero(dt.Rows(0).Item("IRIME")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", Me.NullConvertString(dt.Rows(0).Item("IRIME_UT")), DBDataType.NVARCHAR))

    End Sub

#End Region

#Region "商品マスタパラメータ設定(出荷データ用)"

    ''' <summary>
    ''' 商品マスタパラメータ設定(出荷データ用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetGoodsOutkaParameter(ByVal dtL As DataTable, ByVal dtM As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dtM.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", Me.NullConvertString(dtM.Rows(0).Item("NRS_GOODS_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", Me.NullConvertString(dtM.Rows(0).Item("CUST_GOODS_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me.NullConvertZero(dtM.Rows(0).Item("IRIME")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", Me.NullConvertString(dtM.Rows(0).Item("IRIME_UT")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(dtL.Rows(0).Item("CUST_CD_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(dtL.Rows(0).Item("CUST_CD_M")), DBDataType.CHAR))

    End Sub

#If True Then   'ADD 2018/12/14 依頼番号 : 003818   【LMS】ITWセミEDI_エクセル取込時
    Private Sub SetGoodsOutkaParameterLike(ByVal dtL As DataTable, ByVal dtM As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dtM.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", Me.NullConvertString(dtM.Rows(0).Item("NRS_GOODS_CD")), DBDataType.NVARCHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", String.Concat(Me.NullConvertString(dtM.Rows(0).Item("CUST_GOODS_CD")), "%"), DBDataType.NVARCHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me.NullConvertZero(dtM.Rows(0).Item("IRIME")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", Me.NullConvertString(dtM.Rows(0).Item("IRIME_UT")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(dtL.Rows(0).Item("CUST_CD_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(dtL.Rows(0).Item("CUST_CD_M")), DBDataType.CHAR))

    End Sub
#End If

#End Region

#Region "商品マスタ前方一致検索パラメータ設定(出荷データ用)"

    ''' <summary>
    ''' 商品マスタ前方一致検索パラメータ設定(出荷データ用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetGoodsPrefixOutkaParameter(ByVal dtL As DataTable, ByVal dtM As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dtM.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", Me.NullConvertString(dtM.Rows(0).Item("NRS_GOODS_CD")), DBDataType.NVARCHAR))
        If Me.NullConvertString(dtM.Rows(0).Item("CUST_GOODS_CD")).Equals(String.Empty) Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", String.Empty, DBDataType.NVARCHAR))
        Else
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", String.Concat(dtM.Rows(0).Item("CUST_GOODS_CD").ToString, "%"), DBDataType.NVARCHAR))
        End If
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me.NullConvertZero(dtM.Rows(0).Item("IRIME")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", Me.NullConvertString(dtM.Rows(0).Item("IRIME_UT")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(dtL.Rows(0).Item("CUST_CD_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(dtL.Rows(0).Item("CUST_CD_M")), DBDataType.CHAR))

    End Sub

#End Region

#Region "商品マスタ + 商品明細パラメータ設定(出荷データ用)"

    ''' <summary>
    ''' 商品マスタ + 商品明細パラメータ設定(出荷データ用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetGoodsDetailsPrefixOutkaParameter(ByVal dtL As DataTable, ByVal dtM As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dtM.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", String.Concat(Me.NullConvertString(dtM.Rows(0).Item("CUST_GOODS_CD")), "%"), DBDataType.NVARCHAR))
        If Not Me.NullConvertString(dtM.Rows(0).Item("NRS_GOODS_CD")).Equals(String.Empty) Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", Me.NullConvertString(dtM.Rows(0).Item("NRS_GOODS_CD")), DBDataType.NVARCHAR))
        End If

    End Sub

#End Region
    '2012.03.18 修正START
#Region "出荷(大)パラメータ設定(オーダー番号チェック用)"

    ''' <summary>
    ''' C_OUTKA_L
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInkaParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", Me.NullConvertString(dt.Rows(0).Item("CUST_ORD_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(dt.Rows(0).Item("CUST_CD_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(dt.Rows(0).Item("CUST_CD_M")), DBDataType.CHAR))

    End Sub

#End Region
    '2012.03.18 修正END

    '2014.04.09 まとめデータも対象にオーダー番号チェック --ST--
#Region "出荷(大)パラメータ設定(オーダー番号チェック用)"

    ''' <summary>
    ''' C_OUTKA_L
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInkaParameterInSum(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", Me.NullConvertString(dt.Rows(0).Item("CUST_ORD_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(dt.Rows(0).Item("CUST_CD_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(dt.Rows(0).Item("CUST_CD_M")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", Me.NullConvertString(dt.Rows(0).Item("OUTKA_PLAN_DATE")), DBDataType.CHAR))

    End Sub

#End Region
    '2014.04.09 まとめデータも対象にオーダー番号チェック --ED--

#Region "JISマスタパラメータ設定"

    ''' <summary>
    ''' JISマスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetJisParameter(ByVal dt As DataTable)

        '2013.01.09 要望番号1753 修正START
        Dim zip As String = String.Empty
        If String.IsNullOrEmpty(dt.Rows(0).Item("DEST_ZIP").ToString()) = True Then

        Else
            zip = Replace(dt.Rows(0).Item("DEST_ZIP").ToString(), "-", "")
        End If
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_ZIP", Me.NullConvertString(dt.Rows(0).Item("DEST_ZIP")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_ZIP", Me.NullConvertString(zip), DBDataType.NVARCHAR))
        '2013.01.09 要望番号1753 修正END

        Dim address As String = String.Concat(dt.Rows(0).Item("DEST_AD_1"), dt.Rows(0).Item("DEST_AD_2"), dt.Rows(0).Item("DEST_AD_3"))
        address = address.Replace(" ", "")
        address = address.Replace("　", "")

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ADDRESS", address, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' JISマスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetJisParameterByDestInfo(ByVal dt As DataTable)

        Dim zip As String = String.Empty
        If String.IsNullOrEmpty(dt.Rows(0).Item("ZIP").ToString()) = True Then

        Else
            zip = Replace(dt.Rows(0).Item("ZIP").ToString(), "-", "")
        End If
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_ZIP", Me.NullConvertString(zip), DBDataType.NVARCHAR))

        Dim address As String = String.Concat(dt.Rows(0).Item("AD_1"), dt.Rows(0).Item("AD_2"), dt.Rows(0).Item("AD_3"))
        address = address.Replace(" ", "")
        address = address.Replace("　", "")

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ADDRESS", address, DBDataType.NVARCHAR))

    End Sub

#End Region

#Region "商品マスタをHashTableに設定"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="map"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function SetGoodsMap(ByVal reader As SqlDataReader, ByVal map As Hashtable) As Hashtable

        'Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("MST_CNT", "MST_CNT")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("SEARCH_KEY_1", "SEARCH_KEY_1")
        map.Add("SEARCH_KEY_2", "SEARCH_KEY_2")
        map.Add("CUST_COST_CD1", "CUST_COST_CD1")
        map.Add("CUST_COST_CD2", "CUST_COST_CD2")
        map.Add("JAN_CD", "JAN_CD")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("GOODS_NM_2", "GOODS_NM_2")
        map.Add("GOODS_NM_3", "GOODS_NM_3")
        map.Add("UP_GP_CD_1", "UP_GP_CD_1")
        map.Add("SHOBO_CD", "SHOBO_CD")
        map.Add("KIKEN_KB", "KIKEN_KB")
        map.Add("UN", "UN")
        map.Add("PG_KB", "PG_KB")
        map.Add("CLASS_1", "CLASS_1")
        map.Add("CLASS_2", "CLASS_2")
        map.Add("CLASS_3", "CLASS_3")
        map.Add("CHEM_MTRL_KB", "CHEM_MTRL_KB")
        map.Add("DOKU_KB", "DOKU_KB")
        map.Add("GAS_KANRI_KB", "GAS_KANRI_KB")
        map.Add("ONDO_KB", "ONDO_KB")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("ONDO_MX", "ONDO_MX")
        map.Add("ONDO_MM", "ONDO_MM")
        map.Add("ONDO_STR_DATE", "ONDO_STR_DATE")
        map.Add("ONDO_END_DATE", "ONDO_END_DATE")
        map.Add("ONDO_UNSO_STR_DATE", "ONDO_UNSO_STR_DATE")
        map.Add("ONDO_UNSO_END_DATE", "ONDO_UNSO_END_DATE")
        map.Add("KYOKAI_GOODS_KB", "KYOKAI_GOODS_KB")
        map.Add("ALCTD_KB", "ALCTD_KB")
        map.Add("NB_UT", "NB_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("PLT_PER_PKG_UT", "PLT_PER_PKG_UT")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("STD_WT_KGS", "STD_WT_KGS")
        map.Add("STD_CBM", "STD_CBM")
        map.Add("INKA_KAKO_SAGYO_KB_1", "INKA_KAKO_SAGYO_KB_1")
        map.Add("INKA_KAKO_SAGYO_KB_2", "INKA_KAKO_SAGYO_KB_2")
        map.Add("INKA_KAKO_SAGYO_KB_3", "INKA_KAKO_SAGYO_KB_3")
        map.Add("INKA_KAKO_SAGYO_KB_4", "INKA_KAKO_SAGYO_KB_4")
        map.Add("INKA_KAKO_SAGYO_KB_5", "INKA_KAKO_SAGYO_KB_5")
        map.Add("OUTKA_KAKO_SAGYO_KB_1", "OUTKA_KAKO_SAGYO_KB_1")
        map.Add("OUTKA_KAKO_SAGYO_KB_2", "OUTKA_KAKO_SAGYO_KB_2")
        map.Add("OUTKA_KAKO_SAGYO_KB_3", "OUTKA_KAKO_SAGYO_KB_3")
        map.Add("OUTKA_KAKO_SAGYO_KB_4", "OUTKA_KAKO_SAGYO_KB_4")
        map.Add("OUTKA_KAKO_SAGYO_KB_5", "OUTKA_KAKO_SAGYO_KB_5")
        map.Add("PKG_SAGYO", "PKG_SAGYO")
        map.Add("TARE_YN", "TARE_YN")
        map.Add("SP_NHS_YN", "SP_NHS_YN")
        map.Add("COA_YN", "COA_YN")
        map.Add("LOT_CTL_KB", "LOT_CTL_KB")
        map.Add("LT_DATE_CTL_KB", "LT_DATE_CTL_KB")
        map.Add("CRT_DATE_CTL_KB", "CRT_DATE_CTL_KB")
        map.Add("DEF_SPD_KB", "DEF_SPD_KB")
        map.Add("KITAKU_AM_UT_KB", "KITAKU_AM_UT_KB")
        map.Add("KITAKU_GOODS_UP", "KITAKU_GOODS_UP")
        map.Add("ORDER_KB", "ORDER_KB")
        map.Add("ORDER_NB", "ORDER_NB")
        map.Add("SHIP_CD_L", "SHIP_CD_L")
        map.Add("SKYU_MEI_YN", "SKYU_MEI_YN")
        map.Add("HIKIATE_ALERT_YN", "HIKIATE_ALERT_YN")
        map.Add("OUTKA_ATT", "OUTKA_ATT")
        map.Add("PRINT_NB", "PRINT_NB")
        map.Add("CONSUME_PERIOD_DATE", "CONSUME_PERIOD_DATE")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        '(2013.02.19) ロンザ対応START
        map.Add("SIZE_KB", "SIZE_KB")
        '(2013.02.19) ロンザ対応END

        Return map

    End Function

#End Region

#Region "届先マスタをHashTableに設定"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="map"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function SetDestMap(ByVal reader As SqlDataReader, ByVal map As Hashtable) As Hashtable

        'Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("MST_CNT", "MST_CNT")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("EDI_CD", "EDI_CD")
        map.Add("DEST_NM", "DEST_NM")
        '要望番号:1330 terkawa 2012.08.10 Start
        map.Add("KANA_NM", "KANA_NM")
        '要望番号:1330 terkawa 2012.08.10 End
        map.Add("ZIP", "ZIP")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        map.Add("CUST_DEST_CD", "CUST_DEST_CD")
        map.Add("SALES_CD", "SALES_CD")
        map.Add("SP_NHS_KB", "SP_NHS_KB")
        map.Add("COA_YN", "COA_YN")
        map.Add("SP_UNSO_CD", "SP_UNSO_CD")
        map.Add("SP_UNSO_BR_CD", "SP_UNSO_BR_CD")
        map.Add("DELI_ATT", "DELI_ATT")
        map.Add("CARGO_TIME_LIMIT", "CARGO_TIME_LIMIT")
        map.Add("LARGE_CAR_YN", "LARGE_CAR_YN")
        map.Add("TEL", "TEL")
        map.Add("FAX", "FAX")
        map.Add("UNCHIN_SEIQTO_CD", "UNCHIN_SEIQTO_CD")
        map.Add("JIS", "JIS")
        map.Add("KYORI", "KYORI")
        map.Add("PICK_KB", "PICK_KB")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("MOTO_CHAKU_KB", "MOTO_CHAKU_KB")
        map.Add("URIAGE_CD", "URIAGE_CD")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        Return map

    End Function

#End Region

    '2012.02.13 大阪追加START
#Region "運送会社マスタをHashTableに設定"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="map"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function SetUnsocoMap(ByVal reader As SqlDataReader, ByVal map As Hashtable) As Hashtable


        '取得データの格納先をマッピング
        map.Add("MST_CNT", "MST_CNT")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSOCO_CD", "UNSOCO_CD")
        map.Add("UNSOCO_BR_CD", "UNSOCO_BR_CD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("UNSOCO_KB", "UNSOCO_KB")
        map.Add("ZIP", "ZIP")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        map.Add("TEL", "TEL")
        map.Add("FAX", "FAX")
        map.Add("URL", "URL")
        map.Add("PIC", "PIC")
        map.Add("MOTOUKE_KB", "MOTOUKE_KB")
        map.Add("NRS_SBETU_CD", "NRS_SBETU_CD")
        map.Add("NIHUDA_YN", "NIHUDA_YN")
        map.Add("TARE_YN", "TARE_YN")
        map.Add("UNCHIN_TARIFF_CD", "UNCHIN_TARIFF_CD")
        map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD")
        map.Add("BETU_KYORI_CD", "BETU_KYORI_CD")
        map.Add("LAST_PU_TIME", "LAST_PU_TIME")
        'Add 2014.01.05 要望管理2042 Start
        map.Add("EDI_USED_KBN", "EDI_USED_KBN")
        map.Add("PICKLIST_GRP_KBN", "PICKLIST_GRP_KBN")
        'Add 2014.01.05 要望管理2042 End
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        Return map

    End Function

#End Region
    '2012.02.13 追加END

#Region "郵便番号マスタをHashTableに設定"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <param name="map"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function SetZipMap(ByVal reader As SqlDataReader, ByVal map As Hashtable) As Hashtable

        'Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        'map.Add("MST_CNT", "MST_CNT")
        map.Add("ZIP_NO", "ZIP_NO")
        map.Add("JIS_CD", "JIS_CD")
        map.Add("KEN_K", "KEN_K")
        map.Add("CITY_K", "CITY_K")
        map.Add("TOWN_K", "TOWN_K")
        map.Add("KEN_N", "KEN_N")
        map.Add("CITY_N", "CITY_N")
        map.Add("TOWN_N", "TOWN_N")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        Return map

    End Function

#End Region

#Region "Null変換"
    ''' <summary>
    ''' Null変換（文字列）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertString(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = String.Empty
        End If

        Return value

    End Function

    ''' <summary>
    ''' Null変換（数値）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertZero(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = 0
        End If

        Return value

    End Function

    '▼▼▼二次
    ''' <summary>
    ''' Null変換（数値）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvertStringZero(ByVal value As Object) As Object

        If String.IsNullOrEmpty(value.ToString()) = True Then
            value = 0
        End If

        Return value

    End Function
    '▲▲▲二次

#End Region

#Region "初期検索処理"

    ''' <summary>
    ''' 出荷データL検索対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷データLテーブル更新対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_COUNT)     'SQL構築(カウント用Select句)
        ''2011.09.21 修正START
        'Me._StrSql.Append(LMH030DAC.SQL_COUNT_FROM)             'SQL構築(データ抽出用From句)
        ''Me._StrSql.Append(LMH030DAC403.SQL_FROM)             'SQL構築(データ抽出用From句)
        ''2011.09.21 修正END

        '(2013.06.10) BP新システム切替対応START
        Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

            Case EdiCustIndex.BP00023_00

                Select Case ds.Tables("LMH030INOUT").Rows(0)("BP_PRT_FLG").ToString()

                    Case "01", "02", "03", "04"
                        Me._StrSql.Append(LMH030DAC.SQL_COUNT_FROM_BP)                                 'SQL構築(データ抽出用From句BP用)

                    Case Else

                        Me._StrSql.Append(LMH030DAC.SQL_COUNT_FROM)                                    'SQL構築(データ抽出用From句)

                End Select

            Case Else

                Me._StrSql.Append(LMH030DAC.SQL_COUNT_FROM)                                    'SQL構築(データ抽出用From句)

        End Select
        '(2013.06.10) BP新システム切替対応END

#If True Then ' 日本合成化学対応(2646) 20170116 added inoue
        Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))
            Case EdiCustIndex.Nichigo32516_00
                If (Len(ds.Tables("LMH030INOUT").Rows(0)("BP_PRT_FLG")) > 0) Then
                    Me._StrSql.Append(LMH030DAC.SQL_FROM_ADD_NIHIGO)
                End If
        End Select
#End If

        '2013.03.04 / Notes1909受信ファイル名追加開始
        If String.IsNullOrEmpty(ds.Tables("LMH030INOUT").Rows(0)("RCV_NM_HED").ToString) = False Then

            '2013.09.20 修正START
            Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

                Case EdiCustIndex.DicOsk00010_00 _
                    , EdiCustIndex.DicItk00899_00 _
                    , EdiCustIndex.DicItk10001_00 _
                    , EdiCustIndex.DicItk10002_00 _
                    , EdiCustIndex.DicGnm00072_00 _
                    , EdiCustIndex.DicGnm00076_00 _
                    , EdiCustIndex.DicItk10007_00 _
                    , EdiCustIndex.DicItk10008_00 _
                    , EdiCustIndex.DicChb00010_00

                    If ds.Tables("LMH030INOUT").Rows(0)("HITACHI_SAP_FLG").ToString().Equals("1") = True Then

                        Me._StrSql.Append(LMH030DAC.SQL_FROM_HED_DICNEW_FILER)
                        Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_DICNEW_FILER)

                    Else
                        Me._StrSql.Append(LMH030DAC.SQL_FROM_HED_FILER_INNER)
                        Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_FILER)
                    End If

                    '【注意】以下のCaseを修正する場合はSELECT句、FROM句、WHERE句をあわせて修正すること
                Case EdiCustIndex.DicKkb10001_00 _
                   , EdiCustIndex.DicKkb10001_03 _
                   , EdiCustIndex.DicKkb10012_00 _
                   , EdiCustIndex.DicKkb10013_00 _
                   , EdiCustIndex.DicChbYuso10010_00    'ADD 2018/11/06 要望番号002775(検索のタイムアウト対策)

                Case Else

                    If Not String.IsNullOrEmpty(Me._Row.Item("FILE_NAME").ToString()) Then  'ADD 2018/11/06 要望番号002775(検索のタイムアウト対策)

                        Me._StrSql.Append(LMH030DAC.SQL_FROM_HED_FILER)                      'SQL構築(受信ファイル名抽出用追加FROM句)

                        Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_FILER)                      'SQL構築(受信ファイル名抽出用追加GROUP_BY 句) & <JOIN〆>

                    End If                                                                  'ADD 2018/11/06 要望番号002775(検索のタイムアウト対策)

            End Select
            '2013.09.20 修正END

            'Me._StrSql.Append(LMH030DAC.SQL_FROM_HED_FILER)                      'SQL構築(受信ファイル名抽出用追加FROM句)

            'Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_FILER)                      'SQL構築(受信ファイル名抽出用追加GROUP_BY 句) & <JOIN〆>

        End If
        'Me._StrSql.Append(LMH030DAC.SQL_FROM_HED_FILER)                         'SQL構築(受信ファイル名抽出用追加FROM句)
        'Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_FILER)                         'SQL構築(受信ファイル名抽出用追加GROUP_BY 句) & <JOIN〆>
        '2013.03.04 / Notes1909受信ファイル名追加終了

        '2012.03.26 大阪対応START
        '追加From句
        Call Me.AddSqlFrom(ds)
        '2012.03.26 大阪対応END

        '2012.11.09 センコー対応START
        'Call Me.SetConditionMasterSQL()                   '条件設定
        Call Me.SetConditionMasterSQL(ds)                      '条件設定
        '2012.11.09 センコー対応END

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        '2012.03.26 大阪対応START
        '受信HEDテーブル名設定
        sql = Me.SetRcvTableNm(sql)

        '送信テーブル名設定
        sql = Me.SetSendTableNm(sql)
        '2012.03.26 大阪対応END

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' EDI出荷データL検索データ抽出
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSqlOrderBy = New StringBuilder()

        'SQL作成
        '要望番号1281:(春日部：アベンド) 2012/07/18 本明 Start
        'Me._StrSql.Append(LMH030DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

            '要望番号1458:(DIC群馬：速度UP) 2012/09/21 修正START(DIC群馬もJOINしない)
            Case EdiCustIndex.DicKkb10001_00 _
               , EdiCustIndex.DicKkb10001_03 _
               , EdiCustIndex.DicKkb10012_00 _
               , EdiCustIndex.DicKkb10013_00 _
               , EdiCustIndex.DicGnm00039_00 _
               , EdiCustIndex.DicGnm00072_00 _
               , EdiCustIndex.DicGnm00076_00 _
               , EdiCustIndex.DicChb00010_00
                '要望番号1458:(DIC群馬：速度UP) 2012/09/21 修正END

                Me._StrSql.Append(LMH030DAC.SQL_SELECT_DATA_FOR_DICKKB)      'SQL構築(データ抽出用Select句、春日部DIC専用)

            Case Else
                Me._StrSql.Append(LMH030DAC.SQL_SELECT_DATA)                 'SQL構築(データ抽出用Select句)
        End Select
        '要望番号1281:(春日部：アベンド) 2012/07/18 本明 End

        'ADD 2017/06/02 日合対応 Start
        Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

            Case EdiCustIndex.Nichigo32516_00
                Me._StrSql.Append(LMH030DAC.SQL_UNSONCG_DEL_KB)                         'SQL構築(データ抽出用追加Select句)
                Me._StrSql.Append(LMH030DAC.SQL_UNSOEDI_EXISTS_FLAG)
                Me._StrSql.Append(LMH030DAC.SQL_NCGO_OPEOUT_ONLY_FLG)                   'Add 2018/10/31 要望番号002808
            Case Else
                Me._StrSql.Append(LMH030DAC.SQL_UNSONCG_DEL_KB_NULL)                    'SQL構築(データ抽出用追加Select句)
                Me._StrSql.Append(LMH030DAC.SQL_UNSOEDI_EXISTS_FLAG_NULL)
                Me._StrSql.Append(LMH030DAC.SQL_NCGO_OPEOUT_ONLY_FLG_NULL)              'Add 2018/10/31 要望番号002808
        End Select
        'ADD 2017/06/02 日合対応 End

        '2013.03.04 / Notes1909 受信ファイル名追加SELECT開始
        If String.IsNullOrEmpty(ds.Tables("LMH030INOUT").Rows(0)("RCV_NM_HED").ToString) = False Then

            '要望番号2122 修正START 2013.11.07
            Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))
                '【注意】以下のCaseを修正する場合はSELECT句、FROM句、WHERE句をあわせて修正すること
                Case EdiCustIndex.DicKkb10001_00 _
                   , EdiCustIndex.DicKkb10001_03 _
                   , EdiCustIndex.DicKkb10012_00 _
                   , EdiCustIndex.DicKkb10013_00 _
                   , EdiCustIndex.DicChbYuso10010_00
                    Me._StrSql.Append(LMH030DAC.SQL_SELECT_FILE_NAME_NULL)               'SQL構築(データ抽出用追加Select句hedテーブル無し版)

                Case Else
                    Me._StrSql.Append(LMH030DAC.SQL_SELECT_FILE_NAME)                    'SQL構築(データ抽出用追加Select句)

            End Select
            'Me._StrSql.Append(LMH030DAC.SQL_SELECT_FILE_NAME)                    'SQL構築(データ抽出用追加Select句)
            '要望番号2122 修正END 2013.11.07
        Else
            Me._StrSql.Append(LMH030DAC.SQL_SELECT_FILE_NAME_NULL)               'SQL構築(データ抽出用追加Select句hedテーブル無し版)
        End If

        ' Me._StrSql.Append(LMH030DAC.SQL_SELECT_FILE_NAME)                      'SQL構築(データ抽出用追加Select句)
        '2013.03.04 / Notes1909 受信ファイル名追加SELECT終了

        '(2013.06.10) BP新システム切替対応START
        Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

            Case EdiCustIndex.BP00023_00

                Select Case ds.Tables("LMH030INOUT").Rows(0)("BP_PRT_FLG").ToString()

                    Case "01", "02", "03"
                        Me._StrSql.Append(LMH030DAC.SQL_FROM_BP)                                 'SQL構築(データ抽出用From句BP用)

                    Case "04"
                        Me._StrSql.Append(LMH030DAC.SQL_FROM_BP_NOT_AUTO)                        'SQL構築(データ抽出用From句BPオートバックス以外用)

                    Case Else

                        Me._StrSql.Append(LMH030DAC.SQL_FROM)                                    'SQL構築(データ抽出用From句)

                End Select

            Case Else

                Me._StrSql.Append(LMH030DAC.SQL_FROM)                                    'SQL構築(データ抽出用From句)

        End Select
        '(2013.06.10) BP新システム切替対応END


#If True Then ' 日本合成化学対応(2646) 20170116 added inoue
        Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))
            Case EdiCustIndex.Nichigo32516_00
                If (Len(ds.Tables("LMH030INOUT").Rows(0)("BP_PRT_FLG")) > 0) Then
                    Me._StrSql.Append(LMH030DAC.SQL_FROM_ADD_NIHIGO)
                End If
        End Select
#End If


        '2013.03.04 / Notes1909受信ファイル名追加開始
        If String.IsNullOrEmpty(ds.Tables("LMH030INOUT").Rows(0)("RCV_NM_HED").ToString) = False Then

            '2013.09.20 修正START
            Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

                Case EdiCustIndex.DicOsk00010_00 _
                    , EdiCustIndex.DicItk00899_00 _
                    , EdiCustIndex.DicItk10001_00 _
                    , EdiCustIndex.DicItk10002_00 _
                    , EdiCustIndex.DicGnm00072_00 _
                    , EdiCustIndex.DicGnm00076_00 _
                    , EdiCustIndex.DicItk10007_00 _
                    , EdiCustIndex.DicItk10008_00 _
                    , EdiCustIndex.DicChb00010_00

                    If ds.Tables("LMH030INOUT").Rows(0)("HITACHI_SAP_FLG").ToString().Equals("1") = True Then

                        Me._StrSql.Append(LMH030DAC.SQL_FROM_HED_DICNEW_FILER)
                        Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_DICNEW_FILER)

                    Else
                        Me._StrSql.Append(LMH030DAC.SQL_FROM_HED_FILER_INNER)
                        Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_FILER)
                    End If

                    '要望番号2122 修正START 2013.11.07
                    '【注意】以下のCaseを修正する場合はSELECT句、FROM句、WHERE句をあわせて修正すること
                Case EdiCustIndex.DicKkb10001_00 _
                    , EdiCustIndex.DicKkb10001_03 _
                    , EdiCustIndex.DicKkb10012_00 _
                    , EdiCustIndex.DicKkb10013_00 _
                    , EdiCustIndex.DicChbYuso10010_00
                    '要望番号2122 修正END 2013.11.07

                Case Else
                    Me._StrSql.Append(LMH030DAC.SQL_FROM_HED_FILER)                      'SQL構築(受信ファイル名抽出用追加FROM句)
                    Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_FILER)                      'SQL構築(受信ファイル名抽出用追加GROUP_BY 句) & <JOIN〆>

            End Select
            '2013.09.20 修正END

        End If
        'Me._StrSql.Append(LMH030DAC.SQL_FROM_HED_FILER)                         'SQL構築(受信ファイル名抽出用追加FROM句)
        'Me._StrSql.Append(LMH030DAC.SQL_GROUP_BY_FILER)                         'SQL構築(受信ファイル名抽出用追加GROUP_BY 句) & <JOIN〆>
        '2013.03.04 / Notes1909受信ファイル名追加終了

        '▼▼▼二次
        '追加From句
        '要望番号:1214(【春日部】出荷EDI検索で、タイムアウト) 2012/07/02 本明 Start
        'Call Me.AddSqlFrom(ds)
        Call Me.AddSqlFrom(ds, "LIST") 'LISTモードのSQL文作成 
        '要望番号:1214(【春日部】出荷EDI検索で、タイムアウト) 2012/07/02 本明 End
        '▲▲▲二次

        '2012.11.09 センコー対応STRAT
        'Call Me.SetConditionMasterSQL()                      '条件設定
        Call Me.SetConditionMasterSQL(ds)                      '条件設定
        '2012.11.09 センコー対応END

        '要望番号1870:BPの並び順は届先住所順にする
        Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

            Case EdiCustIndex.BP00023_00
                Call Me.SQLOrderByBp()                                 'SQL構築(データ抽出用Order By句 BP用)

            Case Else
                Call Me.SQLOrderBy()                                 'SQL構築(データ抽出用Order By句)

        End Select

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        '受信HEDテーブル名設定
        sql = Me.SetRcvTableNm(sql)

        '送信テーブル名設定
        sql = Me.SetSendTableNm(sql)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("JYOTAI", "JYOTAI")
        map.Add("HORYU", "HORYU")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("OUTKA_STATE_KB_NM", "OUTKA_STATE_KB_NM")
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("REMARK", "REMARK")
        map.Add("UNSO_ATT", "UNSO_ATT")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("OUTKA_TTL_NB", "OUTKA_TTL_NB")
        map.Add("DEST_AD", "DEST_AD")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("BIN_KB_NM", "BIN_KB_NM")
        map.Add("M_COUNT", "M_COUNT")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("MATOME_NO", "MATOME_NO")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("SYUBETU_KB_NM", "SYUBETU_KB_NM")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("UNSO_MOTO_KB_NM", "UNSO_MOTO_KB_NM")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("CRT_TIME", "CRT_TIME")
        map.Add("SEND_DATE", "SEND_DATE")
        map.Add("SEND_TIME", "SEND_TIME")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("WH_NM", "WH_NM")
        map.Add("TANTO_USER", "TANTO_USER")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("WH_CD", "WH_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("OUTKA_STATE_KB", "OUTKA_STATE_KB")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        '2012.11.11 センコー対応 追加START
        map.Add("TRIP_NO", "TRIP_NO")
        '2012.11.11 センコー対応 追加END
        map.Add("UNSO_SYS_UPD_DATE", "UNSO_SYS_UPD_DATE")
        map.Add("UNSO_SYS_UPD_TIME", "UNSO_SYS_UPD_TIME")
        map.Add("MIN_NB", "MIN_NB")
        map.Add("EDI_DEL_KB", "EDI_DEL_KB")
        map.Add("OUTKA_DEL_KB", "OUTKA_DEL_KB")
        map.Add("UNSO_DEL_KB", "UNSO_DEL_KB")
        map.Add("FREE_C01", "FREE_C01")
        map.Add("FREE_C02", "FREE_C02")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("FREE_C04", "FREE_C04")
        map.Add("FREE_C30", "FREE_C30")
        map.Add("AKAKURO_FLG", "AKAKURO_FLG")
        map.Add("EDI_CUST_JISSEKI", "EDI_CUST_JISSEKI")
        map.Add("EDI_CUST_MATOMEF", "EDI_CUST_MATOMEF")
        map.Add("EDI_CUST_DELDISP", "EDI_CUST_DELDISP")
        map.Add("EDI_CUST_SPECIAL", "EDI_CUST_SPECIAL")
        map.Add("EDI_CUST_HOLDOUT", "EDI_CUST_HOLDOUT")
        map.Add("EDI_CUST_UNSOFLG", "EDI_CUST_UNSOFLG")
        map.Add("EDI_CUST_INDEX", "EDI_CUST_INDEX")
        '▼▼▼二次
        map.Add("RCV_NM_HED", "RCV_NM_HED")
        map.Add("RCV_NM_DTL", "RCV_NM_DTL")
        map.Add("RCV_NM_EXT", "RCV_NM_EXT")
        map.Add("SND_NM", "SND_NM")
        '2012.02.25 大阪対応 START
        map.Add("EDI_CUST_INOUTFLG", "EDI_CUST_INOUTFLG")
        '2012.02.25 大阪対応 END
        '▲▲▲二次
        map.Add("SND_SYS_UPD_DATE", "SND_SYS_UPD_DATE")
        map.Add("SND_SYS_UPD_TIME", "SND_SYS_UPD_TIME")
        map.Add("RCV_SYS_UPD_DATE", "RCV_SYS_UPD_DATE")
        map.Add("RCV_SYS_UPD_TIME", "RCV_SYS_UPD_TIME")
        map.Add("OUTKA_SYS_UPD_DATE", "OUTKA_SYS_UPD_DATE")
        map.Add("OUTKA_SYS_UPD_TIME", "OUTKA_SYS_UPD_TIME")
        map.Add("JISSEKI_FLAG", "JISSEKI_FLAG")
        map.Add("OUT_FLAG", "OUT_FLAG")
        map.Add("AUTO_MATOME_FLG", "AUTO_MATOME_FLG")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("ORDER_CHECK_FLG", "ORDER_CHECK_FLG")

        '2013.03.04 / Notes1909受信ファイル名追加 開始
        map.Add("FILE_NAME", "FILE_NAME")
        '2013.03.04 / Notes1909受信ファイル名追加 終了

        '要望番号:1786 terakara 2013.01.23 PRTFLG追加 Start
        map.Add("PRTFLG", "PRTFLG")
        '要望番号:1786 terakara 2013.01.23 PRTFLG追加 End

        '(2014.01.21)要望番号2145 修正START
        map.Add("DELCNT", "DELCNT")
        '(2014.01.21)要望番号2145 修正END

        '(2014.03.31)セミ標準対応 --ST--
        map.Add("FLAG_17", "FLAG_17")
        '(2014.03.31)セミ標準対応 --ED--
#If True Then ' BP運送会社自動設定対応 20161117 added by inoue
        map.Add("FREE_C05", "FREE_C05")
#End If

#If True Then ' 日本合成化学対応(2646) 20170116 added inoue
        map.Add("FREE_C07", "FREE_C07")
        map.Add("FREE_C08", "FREE_C08")
#End If

        'ADD 2017/05/29 
        map.Add("UNSONCG_DEL_KB", "UNSONCG_DEL_KB")
        'ADD 2017/06/16
        map.Add("HAISO_SIJI_NO", "HAISO_SIJI_NO")
        map.Add("FLAG_19", "FLAG_19")
        map.Add("PICK_KB_NM", "PICK_KB_NM")
        map.Add("UNSOEDI_EXISTS_FLAG", "UNSOEDI_EXISTS_FLAG")
        map.Add("NCGO_OPEOUT_ONLY_FLG", "NCGO_OPEOUT_ONLY_FLG")     'Add 2018/10/31 要望番号002808

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMH030OUT").Rows.Count())
        reader.Close()

        Return ds

    End Function

    '▼▼▼二次
    ''' <summary>
    ''' FROM句追加処理
    ''' </summary>
    ''' <remarks>送信テーブル,受信HEDテーブルの有無でJOIN条件を替える</remarks>

    Private Sub AddSqlFrom(ByVal ds As DataSet, Optional ByVal sMode As String = "")
        '要望番号:1214(【春日部】出荷EDI検索で、タイムアウト) 2012/07/02 本明 Start（関数の引数追加）
        'Private Sub AddSqlFrom(ByVal ds As DataSet)
        '要望番号:1214(【春日部】出荷EDI検索で、タイムアウト) 2012/07/02 本明 End

        Dim sendTbl As String = ds.Tables("LMH030INOUT").Rows(0)("SND_NM").ToString()    '送信テーブル
        Dim rcvTbl As String = ds.Tables("LMH030INOUT").Rows(0)("RCV_NM_HED").ToString() '受信HEDテーブル
        Dim inOutFlg As String = ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INOUTFLG").ToString()  'EDI荷主テーブル入出荷FLG
        '2012.03.20 大阪対応START
        Dim unsoFlg As String = ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_UNSOFLG").ToString()  'EDI出荷運送FLG
        '2012.03.20 大阪対応END

        '要望番号:1214(【春日部】出荷EDI検索で、タイムアウト) 2012/07/05 umano start
        Dim custCdM As String = ds.Tables("LMH030INOUT").Rows(0)("CUST_CD_M").ToString() '荷主コード(中)
        '要望番号:1214(【春日部】出荷EDI検索で、タイムアウト) 2012/07/05 umano end

        '要望番号:1214(【春日部】出荷EDI検索で、タイムアウト) 2012/07/02 本明 Start
        'If String.IsNullOrEmpty(sendTbl) = True Then
        '    Me._StrSql.Append(LMH030DAC.SQL_FROM_SENDTABLE_NULL)
        'Else

        '    If inOutFlg.Equals("1") = True Then
        '        Me._StrSql.Append(LMH030DAC.SQL_FROM_SENDTABLE)
        '        Me._StrSql.Append(LMH030DAC.SQL_FROM_SENDTABLE_INOUT)
        '    Else
        '        Me._StrSql.Append(LMH030DAC.SQL_FROM_SENDTABLE_NORMAL)
        '    End If
        'End If

        '要望番号1281:(春日部：アベンド) 2012/07/18 本明 Start
        'sModeがリストの場合のみSQL文にLeftJoin追加
        'If sMode = "LIST" Then
        '    If String.IsNullOrEmpty(sendTbl) = True Then
        '        Me._StrSql.Append(LMH030DAC.SQL_FROM_SENDTABLE_NULL)
        '    Else

        '        If inOutFlg.Equals("1") = True Then
        '            Me._StrSql.Append(LMH030DAC.SQL_FROM_SENDTABLE)
        '            Me._StrSql.Append(LMH030DAC.SQL_FROM_SENDTABLE_INOUT)
        '        Else
        '            Me._StrSql.Append(LMH030DAC.SQL_FROM_SENDTABLE_NORMAL)
        '        End If
        '    End If
        'End If
        'sModeがリスト、かつDic春日部でない場合のみSQL文にLeftJoin追加
        Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

            '要望番号1458:(DIC群馬：速度UP) 2012/09/21 修正START(DIC群馬もJOINしない)
            Case EdiCustIndex.DicKkb10001_00 _
               , EdiCustIndex.DicKkb10001_03 _
               , EdiCustIndex.DicKkb10012_00 _
               , EdiCustIndex.DicKkb10013_00 _
               , EdiCustIndex.DicGnm00039_00 _
               , EdiCustIndex.DicGnm00072_00 _
               , EdiCustIndex.DicGnm00076_00 _
               , EdiCustIndex.DicChb00010_00
                'LeftJoin無しのため無処理
                '要望番号1458:(DIC群馬：速度UP) 2012/09/21 修正END
                'ビーピー・カストロール対応 terakawa 2012.01.11 Start
            Case EdiCustIndex.BP00023_00
                'BPの場合は、受信HEDのあとにSENDをJOINする
                'ビーピー・カストロール対応 terakawa 2012.01.11 End
            Case Else
                If sMode = "LIST" Then
                    If String.IsNullOrEmpty(sendTbl) = True Then
                        Me._StrSql.Append(LMH030DAC.SQL_FROM_SENDTABLE_NULL)
                    Else

                        If inOutFlg.Equals("1") = True Then
                            '2014.06.06 FFEM対応 修正START
                            If EdiCustIndex.FjfChb00195_00 = Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX")) Then
                                Me._StrSql.Append(LMH030DAC.SQL_FROM_SENDTABLE_NORMAL)
                            Else
                                Me._StrSql.Append(LMH030DAC.SQL_FROM_SENDTABLE)
                                Me._StrSql.Append(LMH030DAC.SQL_FROM_SENDTABLE_INOUT)
                            End If
                            '2014.06.06 FFEM対応 修正END
                        Else
                            Me._StrSql.Append(LMH030DAC.SQL_FROM_SENDTABLE_NORMAL)
                        End If
                    End If
                End If
        End Select
        '要望番号1281:(春日部：アベンド) 2012/07/18 本明 End



        '要望番号:1214(【春日部】出荷EDI検索で、タイムアウト) 2012/07/02 本明 End


        '要望番号:1214(【春日部】出荷EDI検索で、タイムアウト) 2012/07/02 本明 Start(以下をコメント化して別ロジックに)
        'If String.IsNullOrEmpty(rcvTbl) = True Then
        '    Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE_NULL)
        'Else
        '    Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE)
        '    If inOutFlg.Equals("1") = True Then
        '        Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE_INOUT)
        '    Else
        '    End If
        '    '2012.03.28追加START 要望番号XXX 受信HED：EDI管理№に複数受信が存在する不具合
        '    Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE_GROUP_BY)
        '    '2012.03.28追加END 要望番号XXX 受信HED：EDI管理№に複数受信が存在する不具合
        'End If
        '要望番号:1214(【春日部】出荷EDI検索で、タイムアウト) 2012/07/02 本明 End


        '要望番号:1214(【春日部】出荷EDI検索で、タイムアウト) 2012/07/02 本明 Start(上記の代替ロジック)
        '要望番号1281:(春日部：アベンド) 2012/07/18 本明 Start
        'sModeがリストの場合のみSQLにLeftJoin追加
        'If sMode = "LIST" Then
        '    If String.IsNullOrEmpty(rcvTbl) = True Then
        '        Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE_NULL)
        '    Else
        '        Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

        '            Case EdiCustIndex.DicOsk00010_00 _
        '               , EdiCustIndex.DicItk00899_00 _
        '               , EdiCustIndex.DicItk00899_01 _
        '               , EdiCustIndex.DicItk10001_00 _
        '               , EdiCustIndex.DicItk10002_00 _
        '               , EdiCustIndex.DicItk10003_00 _
        '               , EdiCustIndex.DicItk10007_00 _
        '               , EdiCustIndex.DicItk10008_00 _
        '               , EdiCustIndex.DicItk10009_00 _
        '               , EdiCustIndex.DicItk10005_00 _
        '               , EdiCustIndex.DicKkb10001_00 _
        '               , EdiCustIndex.DicKkb10001_03 _
        '               , EdiCustIndex.DicKkb10012_00 _
        '               , EdiCustIndex.DicKkb10013_00 _
        '               , EdiCustIndex.Dns20000_00 _
        '               , EdiCustIndex.Dns20000_01 _
        '               , EdiCustIndex.Dns20000_02 _
        '               , EdiCustIndex.Dupont00089_00 _
        '               , EdiCustIndex.Dupont00295_00 _
        '               , EdiCustIndex.Dupont00331_00 _
        '               , EdiCustIndex.Dupont00331_02 _
        '               , EdiCustIndex.Dupont00331_03 _
        '               , EdiCustIndex.Dupont00588_00 _
        '               , EdiCustIndex.Dupont00300_00 _
        '               , EdiCustIndex.Dupont00689_00 _
        '               , EdiCustIndex.Dupont00700_00 _
        '               , EdiCustIndex.Jc31022_00 _
        '               , EdiCustIndex.Jc31022_01 _
        '               , EdiCustIndex.Dow00109_00 _
        '               , EdiCustIndex.DowTaka00109_01 _
        '               , EdiCustIndex.Smk00952_00

        '                If String.IsNullOrEmpty(custCdM) = True Then
        '                    '受信ヘッダーテーブルは存在し、項目にCUST_L,Mがある荷主(CUST_LのみでWHERE区作成)
        '                    Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE & SQL_FROM_RCVTABLE_WHERE_CUST_L)
        '                Else
        '                    '受信ヘッダーテーブルは存在し、項目にCUST_L,Mがある荷主(CUST_L,MでWHERE区作成)
        '                    Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE & SQL_FROM_RCVTABLE_WHERE_CUST_LM)
        '                End If

        '            Case Else
        '                '受信ヘッダーテーブルは存在するが、項目にCUST_L,Mがない荷主(ロジック変更無し)
        '                Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE)
        '        End Select

        '        If inOutFlg.Equals("1") = True Then
        '            Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE_INOUT)
        '        Else
        '        End If
        '        '2012.03.28追加START 要望番号XXX 受信HED：EDI管理№に複数受信が存在する不具合
        '        Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE_GROUP_BY)
        '        '2012.03.28追加END 要望番号XXX 受信HED：EDI管理№に複数受信が存在する不具合
        '    End If
        'End If

        'sModeがリスト、かつDic春日部でない場合のみSQL文にLeftJoin追加
        Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

            '要望番号1458:(DIC群馬：速度UP) 2012/09/21 修正START(DIC群馬もJOINしない)
            Case EdiCustIndex.DicKkb10001_00 _
               , EdiCustIndex.DicKkb10001_03 _
               , EdiCustIndex.DicKkb10012_00 _
               , EdiCustIndex.DicKkb10013_00 _
               , EdiCustIndex.DicGnm00039_00 _
               , EdiCustIndex.DicGnm00072_00 _
               , EdiCustIndex.DicGnm00076_00 _
               , EdiCustIndex.DicChb00010_00

                'LeftJoin無しのため無処理
                '要望番号1458:(DIC群馬：速度UP) 2012/09/21 修正END
                'ビーピー・カストロール対応 terakawa 2012.01.11 Start
            Case EdiCustIndex.BP00023_00
                If sMode = "LIST" Then
                    'BPの場合は、受信HEDのあとにSENDをJOINする
                    Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE_BP)
                    Me._StrSql.Append(LMH030DAC.SQL_FROM_SENDTABLE_BP)
                End If
                'ビーピー・カストロール対応 terakawa 2012.01.11 End
            Case Else
                If sMode = "LIST" Then
                    If String.IsNullOrEmpty(rcvTbl) = True Then
                        Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE_NULL)
                    Else
                        Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

                            '2013.09.20 修正START
                            'Case EdiCustIndex.DicOsk00010_00 _
                            '   , EdiCustIndex.DicItk00899_00 _
                            '   , EdiCustIndex.DicItk00899_01 _
                            '   , EdiCustIndex.DicItk10001_00 _
                            '   , EdiCustIndex.DicItk10002_00 _
                            '   , EdiCustIndex.DicItk10003_00 _
                            '   , EdiCustIndex.DicItk10007_00 _
                            '   , EdiCustIndex.DicItk10008_00 _
                            '   , EdiCustIndex.DicItk10009_00 _
                            '   , EdiCustIndex.DicItk10005_00 _
                            '   , EdiCustIndex.Dns20000_00 _
                            '   , EdiCustIndex.Dns20000_01 _
                            '   , EdiCustIndex.Dns20000_02 _
                            '   , EdiCustIndex.Dupont00089_00 _
                            '   , EdiCustIndex.Dupont00295_00 _
                            '   , EdiCustIndex.Dupont00331_00 _
                            '   , EdiCustIndex.Dupont00331_02 _
                            '   , EdiCustIndex.Dupont00331_03 _
                            '   , EdiCustIndex.Dupont00588_00 _
                            '   , EdiCustIndex.Dupont00300_00 _
                            '   , EdiCustIndex.Dupont00689_00 _
                            '   , EdiCustIndex.Dupont00700_00 _
                            '   , EdiCustIndex.Jc31022_00 _
                            '   , EdiCustIndex.Jc31022_01 _
                            '   , EdiCustIndex.Dow00109_00 _
                            '   , EdiCustIndex.DowTaka00109_01 _
                            '   , EdiCustIndex.Smk00952_00

                            Case EdiCustIndex.DicItk00899_01 _
                                , EdiCustIndex.DicItk10003_00 _
                                , EdiCustIndex.DicItk10009_00 _
                                , EdiCustIndex.DicItk10005_00 _
                                , EdiCustIndex.Dns20000_00 _
                                , EdiCustIndex.Dns20000_01 _
                                , EdiCustIndex.Dns20000_02 _
                                , EdiCustIndex.Dupont00089_00 _
                                , EdiCustIndex.Dupont00295_00 _
                                , EdiCustIndex.Dupont00331_00 _
                                , EdiCustIndex.Dupont00331_02 _
                                , EdiCustIndex.Dupont00331_03 _
                                , EdiCustIndex.Dupont00588_00 _
                                , EdiCustIndex.Dupont00300_00 _
                                , EdiCustIndex.Dupont00689_00 _
                                , EdiCustIndex.Dupont00700_00 _
                                , EdiCustIndex.Jc31022_00 _
                                , EdiCustIndex.Jc31022_01 _
                                , EdiCustIndex.Dow00109_00 _
                                , EdiCustIndex.DowTaka00109_01 _
                                , EdiCustIndex.Smk00952_00
                                '2013.09.20 修正END

                                If String.IsNullOrEmpty(custCdM) = True Then
                                    '受信ヘッダーテーブルは存在し、項目にCUST_L,Mがある荷主(CUST_LのみでWHERE区作成)
                                    Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE & SQL_FROM_RCVTABLE_WHERE_CUST_L)
                                Else
                                    '受信ヘッダーテーブルは存在し、項目にCUST_L,Mがある荷主(CUST_L,MでWHERE区作成)
                                    Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE & SQL_FROM_RCVTABLE_WHERE_CUST_LM)
                                End If

                                '2013.09.20 追加START
                            Case EdiCustIndex.DicOsk00010_00 _
                                , EdiCustIndex.DicItk00899_00 _
                                , EdiCustIndex.DicItk10001_00 _
                                , EdiCustIndex.DicItk10002_00 _
                                , EdiCustIndex.DicGnm00072_00 _
                                , EdiCustIndex.DicGnm00076_00 _
                                , EdiCustIndex.DicItk10007_00 _
                                , EdiCustIndex.DicItk10008_00 _
                                , EdiCustIndex.DicChb00010_00

                                If ds.Tables("LMH030INOUT").Rows(0)("HITACHI_SAP_FLG").ToString().Equals("1") = True Then

                                    If String.IsNullOrEmpty(custCdM) = True Then
                                        '受信ヘッダーテーブルは存在し、項目にCUST_L,Mがある荷主(CUST_LのみでWHERE区作成)
                                        Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVDIC_NEW & SQL_FROM_RCVDIC_NEW_WHERE_CUST_L)
                                    Else
                                        '受信ヘッダーテーブルは存在し、項目にCUST_L,Mがある荷主(CUST_L,MでWHERE区作成)
                                        Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVDIC_NEW & SQL_FROM_RCVDIC_NEW_WHERE_CUST_LM)
                                    End If

                                    '日立FNの新テーブルレイアウトの場合は、強制的にINOUT_KBを追加する
                                    '※EDI荷主マスタFLAG16の値は"0"のままにしておく(全拠点導入時は切替)
                                    Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVDIC_NEW_INOUT)

                                Else

                                    If String.IsNullOrEmpty(custCdM) = True Then
                                        '受信ヘッダーテーブルは存在し、項目にCUST_L,Mがある荷主(CUST_LのみでWHERE区作成)
                                        Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE_INNER & SQL_FROM_RCVTABLE_WHERE_CUST_L)
                                    Else
                                        '受信ヘッダーテーブルは存在し、項目にCUST_L,Mがある荷主(CUST_L,MでWHERE区作成)
                                        Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE_INNER & SQL_FROM_RCVTABLE_WHERE_CUST_LM)
                                    End If

                                End If

                                '2013.09.20 追加END

                            Case Else
                                '受信ヘッダーテーブルは存在するが、項目にCUST_L,Mがない荷主(ロジック変更無し)
                                Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE)
                        End Select

                        If inOutFlg.Equals("1") = True Then
                            Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE_INOUT)
                        Else
                        End If

                        '2013.09.20 追加START
                        If ds.Tables("LMH030INOUT").Rows(0)("HITACHI_SAP_FLG").ToString().Equals("1") = True Then

                            Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

                                Case EdiCustIndex.DicOsk00010_00 _
                                   , EdiCustIndex.DicItk00899_00 _
                                   , EdiCustIndex.DicItk10001_00 _
                                   , EdiCustIndex.DicItk10002_00 _
                                   , EdiCustIndex.DicGnm00072_00 _
                                   , EdiCustIndex.DicGnm00076_00 _
                                   , EdiCustIndex.DicItk10007_00 _
                                   , EdiCustIndex.DicItk10008_00 _
                                   , EdiCustIndex.DicChb00010_00

                                    Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVDIC_NEW_GROUP_BY)

                                Case Else
                                    Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE_GROUP_BY)

                            End Select

                        Else

                            '2012.03.28追加START 要望番号XXX 受信HED：EDI管理№に複数受信が存在する不具合
                            Me._StrSql.Append(LMH030DAC.SQL_FROM_RCVTABLE_GROUP_BY)
                            '2012.03.28追加END 要望番号XXX 受信HED：EDI管理№に複数受信が存在する不具合
                        End If
                        '2013.09.20 追加END

                    End If
                End If
        End Select
        '要望番号1281:(春日部：アベンド) 2012/07/18 本明 End
        '要望番号:1214(【春日部】出荷EDI検索で、タイムアウト) 2012/07/02 本明 End

        '2012.03.20 大阪対応START
        If unsoFlg.Equals("1") = True Then
            Me._StrSql.Append(LMH030DAC.SQL_FROM_UNSO_ONLY)
        Else
            Me._StrSql.Append(LMH030DAC.SQL_FROM_OUTKA_UNSO)
        End If
        '2012.03.20 大阪対応END

        'ADD 2017/06/02 日合だけ処理する
        Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))
            Case EdiCustIndex.Nichigo32516_00

                'ADD 2017/05/29 日本合成で追加　いつでもUNSOチェック
                Me._StrSql.Append(LMH030DAC.SQL_FROM_UNSO_NCG)
                Me._StrSql.Append(LMH030DAC.SQL_FROM_SQL_UNSOEDI_EXISTS)
                Me._StrSql.Append(LMH030DAC.SQL_FROM_H_OUTKAEDI_DTL_NCGO_NEW)   'Add 2018/10/31 要望番号002808
        End Select

    End Sub
    '▲▲▲二次

    '2017/12/27 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add start
    ''' <summary>
    ''' 該当荷主でEXCELファイル取込であった場合、対象のEDI明細テーブル名に置き換える
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConvEdiDtlNm(ByVal ds As DataSet) As DataSet

        '画面一覧出力元データテーブルを取得する
        Dim listDt As DataTable = ds.Tables("LMH030OUT")
        Dim ediDtlNm As String = String.Empty
        Dim count As Integer = 0

        '画面一覧出力元データテーブルを一巡する
        For Each listDr As DataRow In listDt.Rows

            '該当営業所、該当荷主でなければ次のレコードを処理する
            If getExcelEdiDtlTblNm(listDr, ediDtlNm).Equals(False) Then
                Continue For
            End If

            'EXCELで取り込んだものであれば項目「RCV_NM_DTL」の内容を置き換える
            ds.Tables("DATA_COUNT").Clear()
            ds = GetExcelTorikomi(ds, listDr)
            count = Convert.ToInt32(ds.Tables("DATA_COUNT").Rows(0).Item("DATA_COUNT"))
            If count > 0 Then
                listDr("RCV_NM_DTL") = ediDtlNm
            End If

        Next
        Return ds
    End Function

    ''' <summary>
    ''' EXCEL取込対象の営業所、荷主かを確認し、対象であればEDI詳細テーブル名を取得する
    ''' </summary>
    ''' <param name="listDr">対象行</param>
    ''' <param name="ediDtlNm">EDI詳細テーブル名</param>
    ''' <returns>True=営業所、荷主がEXCEL取込対象に該当した False=該当しなかった</returns>
    ''' <remarks></remarks>
    Private Function getExcelEdiDtlTblNm(ByRef listDr As DataRow, ByRef ediDtlNm As String) As Boolean
        Dim brCd As String = listDr("NRS_BR_CD").ToString
        Dim custCdL As String = listDr("CUST_CD_L").ToString
        Dim result As Boolean = False
        Select Case brCd
            Case "40"
                Select Case custCdL
                    Case "00456"
                        ediDtlNm = "H_OUTKAEDI_DTL_GKE"
                        result = True
                End Select
        End Select
        Return result
    End Function

    ''' <summary>
    ''' EXCEL取込判断情報取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="listDr">対象行</param>
    ''' <returns></returns>
    ''' <remarks>返却するDS内のテーブル「DATA_COUNT」の項目「DATA_COUNT」が0件でなければEXCEL取込</remarks>
    Private Function GetExcelTorikomi(ByRef ds As DataSet, ByRef listDr As DataRow) As DataSet

        '営業所コード取得
        Dim brCd As String = listDr("NRS_BR_CD").ToString

        'EDI出荷管理番号L取得
        Dim ediCtlNo As String = listDr("EDI_CTL_NO").ToString

        Dim sql As String = SQL_SELECT_DATA_FOR_EXCEL_TORIKOMI_HANDAN

        'パラメータ設定
        Dim para As New ArrayList()
        para.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))
        para.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", ediCtlNo, DBDataType.CHAR))

        'スキーマ名設定
        sql = Me.SetSchemaNm(sql, brCd)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In para
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog("LMH030DAC", "GetExcelTorikomi", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'データテーブルのマッパー設定
        Dim map As Hashtable = New Hashtable()
        map.Add("DATA_COUNT", "DATA_COUNT")

        'Readerの内容をデータテーブルに設定
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "DATA_COUNT")

        reader.Close()

        Return ds

    End Function
    '2017/12/27 Annen セミEDI_千葉横浜大阪・DSP五協フード＆ケミカル株式会社対応 add end

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL(ByVal ds As DataSet)

        Dim unsoFlg As String = ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_UNSOFLG").ToString()  'EDI出荷運送FLG
        Dim rcv_Nm As String = ds.Tables("LMH030INOUT").Rows(0)("RCV_NM_HED").ToString()  'EDIHEDテーブル名

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '進捗区分
            Dim arr As ArrayList = New ArrayList()

            Dim connectFlg As Boolean = False
            Dim checkFlg As Boolean = False

            Me._StrSql.Append(" ( ")

            '未登録にチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB1").ToString()) = False Then

                Me._StrSql.Append(" ((H_OUTKAEDI_L.DEL_KB IN ('0','3','2')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND H_OUTKAEDI_L.OUT_FLAG IN ('0','2')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND H_OUTKAEDI_L.JISSEKI_FLAG IN ('0','9'))")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (H_OUTKAEDI_L.DEL_KB = '1'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND M_EDI_CUST.FLAG_08 = '1'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND H_OUTKAEDI_L.OUT_FLAG IN ('0','2')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND H_OUTKAEDI_L.JISSEKI_FLAG IN ('0','9')))")
                Me._StrSql.Append(vbNewLine)
                ''2011.10.14 廃止START デュポンEDI即実績作成データは未登録には表示しない
                ''2011.10.07 START デュポンEDIデータ即実績作成対応
                'Me._StrSql.Append(" OR (H_OUTKAEDI_L.DEL_KB IN ('0','3','2')")
                'Me._StrSql.Append(vbNewLine)
                'Me._StrSql.Append(" AND M_EDI_CUST.FLAG_01 = '3'")
                'Me._StrSql.Append(vbNewLine)
                'Me._StrSql.Append(" AND H_OUTKAEDI_L.OUT_FLAG IN ('0','2')")
                'Me._StrSql.Append(vbNewLine)
                'Me._StrSql.Append(" AND H_OUTKAEDI_L.JISSEKI_FLAG IN ('0','1','9')))")
                'Me._StrSql.Append(vbNewLine)
                ''2011.10.07 END
                ''2011.10.14 廃止END 
                connectFlg = True
                checkFlg = True
            End If

            '出荷登録済にチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB2").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                '2011.10.07 START デュポンEDIデータ即実績作成対応
                'Me._StrSql.Append(" ((M_EDI_CUST.FLAG_01 IN ('1','2','4')")
                Me._StrSql.Append(" ((M_EDI_CUST.FLAG_01 IN ('1','2','3','4')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND C_OUTKA_L.SYS_DEL_FLG = '0'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND C_OUTKA_L.OUTKA_STATE_KB < '60')")
                Me._StrSql.Append(vbNewLine)
                'Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 IN ('1','4')")
                Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 IN ('1','3','4')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND C_OUTKA_L.SYS_DEL_FLG = '1')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 IN ('0','9')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND C_OUTKA_L.OUTKA_STATE_KB IS NOT NULL)")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (LEFT(H_OUTKAEDI_L.FREE_C30,2) = '01'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND SUBSTRING(H_OUTKAEDI_L.FREE_C30,5,8) NOT IN ('','00000000')))")
                Me._StrSql.Append(vbNewLine)

                'Me._StrSql.Append(" ((C_OUTKA_L.SYS_DEL_FLG = '0'")
                'Me._StrSql.Append(vbNewLine)
                'Me._StrSql.Append(" AND C_OUTKA_L.OUTKA_STATE_KB < '60')")
                'Me._StrSql.Append(vbNewLine)
                'Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 IN ('1','4')")
                'Me._StrSql.Append(vbNewLine)
                'Me._StrSql.Append(" AND C_OUTKA_L.SYS_DEL_FLG = '1')")
                'Me._StrSql.Append(vbNewLine)
                'Me._StrSql.Append(" OR (LEFT(H_OUTKAEDI_L.FREE_C30,2) = '01'")
                'Me._StrSql.Append(vbNewLine)
                'Me._StrSql.Append(" AND SUBSTRING(H_OUTKAEDI_L.FREE_C30,3,9) NOT IN ('','Y00000000')))")
                'Me._StrSql.Append(vbNewLine)
                connectFlg = True
                checkFlg = True
            End If

            '実績未にチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB3").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                '2011.10.07 START デュポンEDIデータ即実績作成対応
                Me._StrSql.Append(" (((M_EDI_CUST.FLAG_01 IN ('1','2','3')")
                '2011.10.07 END
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND H_OUTKAEDI_L.SYS_DEL_FLG = '0'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND C_OUTKA_L.SYS_DEL_FLG = '0'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND C_OUTKA_L.OUTKA_STATE_KB >= '60')")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 = '2'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND (H_OUTKAEDI_L.SYS_DEL_FLG = '1'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR C_OUTKA_L.SYS_DEL_FLG = '1'))")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 = '4'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND H_OUTKAEDI_L.SYS_DEL_FLG = '0'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND C_OUTKA_L.SYS_DEL_FLG = '0')")
                Me._StrSql.Append(vbNewLine)
                '2011.10.07 START デュポンEDIデータ即実績作成対応
                Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 = '3'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND H_OUTKAEDI_L.SYS_DEL_FLG = '0'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND C_OUTKA_L.OUTKA_STATE_KB IS NULL)")
                Me._StrSql.Append(vbNewLine)
                '2011.10.07 END
                '2012.11.09 センコー対応START
                If unsoFlg.Equals("1") = True Then
                    Me._StrSql.Append(" OR (LEFT(H_OUTKAEDI_L.FREE_C30,2) = '01'")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append(" AND F_UNSO_L.TRIP_NO <> '' ")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append(" AND  F_UNSO_L.TRIP_NO IS NOT NULL")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append(" AND  M_EDI_CUST.FLAG_01 = '1' ))")
                    Me._StrSql.Append(vbNewLine)
                Else
                    Me._StrSql.Append(")")
                End If
                '2012.11.09 センコー対応END
                Me._StrSql.Append(" AND (H_OUTKAEDI_L.JISSEKI_FLAG = '0'))")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True
            End If

            '実績作成済にチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB4").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (H_OUTKAEDI_L.JISSEKI_FLAG = '1')")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True
            End If

            '実績送信済にチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB5").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (H_OUTKAEDI_L.JISSEKI_FLAG = '2')")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True
            End If

            '赤データにチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB6").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (H_OUTKAEDI_L.DEL_KB = '2')")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True
            End If

            '取消のみにチェックあり
            If String.IsNullOrEmpty(.Item("EDIOUTKA_STATE_KB8").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (H_OUTKAEDI_L.DEL_KB = '1')")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True
            End If

            '進捗区分チェックなしは全件検索
            If checkFlg = False Then

                Me._StrSql.Append(" ((H_OUTKAEDI_L.DEL_KB IN ('0','3','2'))")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR (M_EDI_CUST.FLAG_01 = '2'")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR M_EDI_CUST.FLAG_08 = '1'))")
                Me._StrSql.Append(vbNewLine)

            End If

            Me._StrSql.Append(" ) ")

            '====== ヘッダ項目 ======'

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '倉庫
            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.WH_CD = @WH_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'Me._StrSql.Append(" AND H_OUTKAEDI_L.CUST_CD_L LIKE @CUST_CD_L")
                Me._StrSql.Append(" AND H_OUTKAEDI_L.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'Me._StrSql.Append(" AND H_OUTKAEDI_L.CUST_CD_M LIKE @CUST_CD_M")
                Me._StrSql.Append(" AND H_OUTKAEDI_L.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '担当者コード
            whereStr = .Item("TANTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TANTO_USER.USER_CD LIKE @TANTO_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TANTO_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先コード
            whereStr = .Item("DEST_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.DEST_CD LIKE @DEST_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'EDI取込日(FROM)
            whereStr = .Item("EDI_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.CRT_DATE >= @EDI_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            'EDI取込日(TO)
            whereStr = .Item("EDI_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.CRT_DATE <= @EDI_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DATE_TO", whereStr, DBDataType.CHAR))
            End If


            '区分格納変数
            Dim kbn As String

            '可変比較対象項目名格納変数
            Dim colNM As String = String.Empty

            'EDI検索日付区分
            kbn = .Item("SEARCH_DATE_KBN").ToString()

            'EDI検索日付区分によって以下分岐
            Select Case kbn

                'Case "01"
                '    colNM = "CRT_DATE"
                Case "01"
                    colNM = "OUTKA_PLAN_DATE"
                Case "02"
                    colNM = "ARR_PLAN_DATE"
                Case Else
                    colNM = String.Empty

            End Select

            If String.IsNullOrEmpty(colNM) = False Then

                'EDI検索日(FROM)
                whereStr = .Item("SEARCH_DATE_FROM").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    '2011.09.25 修正START
                    Select Case kbn
                        'Case "01"
                        '    Me._StrSql.Append(" AND @SEARCH_DATE = H_OUTKAEDI_L.")
                        '    Me._StrSql.Append(colNM)
                        '    Me._StrSql.Append(vbNewLine)
                        '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE", whereStr, DBDataType.CHAR))
                        Case "01", "02"
                            Me._StrSql.Append(" AND @SEARCH_DATE_FROM <= ISNULL(C_OUTKA_L.")
                            Me._StrSql.Append(colNM)
                            Me._StrSql.Append(" ,H_OUTKAEDI_L.")
                            Me._StrSql.Append(colNM)
                            Me._StrSql.Append(" )")
                            Me._StrSql.Append(vbNewLine)
                            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_FROM", whereStr, DBDataType.CHAR))
                        Case Else

                    End Select
                    '2011.09.25 修正END
                End If

                'EDI検索日(TO)
                whereStr = .Item("SEARCH_DATE_TO").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then

                    Select Case kbn

                        Case "01", "02"
                            Me._StrSql.Append(" AND @SEARCH_DATE_TO >= ISNULL(C_OUTKA_L.")
                            Me._StrSql.Append(colNM)
                            Me._StrSql.Append(" ,H_OUTKAEDI_L.")
                            Me._StrSql.Append(colNM)
                            Me._StrSql.Append(" )")
                            Me._StrSql.Append(vbNewLine)
                            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_TO", whereStr, DBDataType.CHAR))
                        Case Else

                    End Select

                End If

            End If


            Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))
                Case EdiCustIndex.BP00023_00

                    '(2013.02.16)要望番号1851 BP納品書検索対応 -- START --
                    Select Case .Item("BP_PRT_FLG").ToString.Trim
                        Case "01"
                            'オートバックス
                            '(2013.02.27) BP新システム切替対応START
                            'Me._StrSql.Append(vbNewLine)
                            'Me._StrSql.Append(" AND (H_OUTKAEDI_L.FREE_C04 = 'SO' OR H_OUTKAEDI_L.FREE_C04 = 'UE') ")
                            'Me._StrSql.Append(vbNewLine)
                            'Me._StrSql.Append(" AND H_OUTKAEDI_L.FREE_C18 = '11030000' ")
                            'Me._StrSql.Append(vbNewLine)
                            'Me._StrSql.Append(" AND H_OUTKAEDI_L.DEL_KB = '0' ")
                            Me._StrSql.Append(vbNewLine)
                            Me._StrSql.Append(" AND (H_OUTKAEDI_L.FREE_C04 = 'S2' OR H_OUTKAEDI_L.FREE_C04 = 'UE') ")
                            Me._StrSql.Append(vbNewLine)
                            Me._StrSql.Append(" AND H_OUTKAEDI_L.FREE_C18 = '11500057' ")
                            Me._StrSql.Append(vbNewLine)
                            '(2013.06.10) BP新システム切替対応START
                            Me._StrSql.Append(" AND MCNTSYS.M_COUNT >= 1 ")
                            Me._StrSql.Append(vbNewLine)
                            '(2013.06.10) BP新システム切替対応END
                            Me._StrSql.Append(" AND H_OUTKAEDI_L.DEL_KB = '0' ")
                            '(2013.02.27) BP新システム切替対応END

                        Case "02"
                            'タクティ
                            '(2013.02.27) BP新システム切替対応START
                            'Me._StrSql.Append(vbNewLine)
                            'Me._StrSql.Append(" AND (H_OUTKAEDI_L.FREE_C04 = 'SO' OR H_OUTKAEDI_L.FREE_C04 = 'UE') ")
                            'Me._StrSql.Append(vbNewLine)
                            'Me._StrSql.Append(" AND H_OUTKAEDI_L.FREE_C18 = '18298100' ")
                            'Me._StrSql.Append(vbNewLine)
                            'Me._StrSql.Append(" AND H_OUTKAEDI_L.DEL_KB = '0' ")
                            Me._StrSql.Append(vbNewLine)
                            Me._StrSql.Append(" AND (H_OUTKAEDI_L.FREE_C04 = 'S2' OR H_OUTKAEDI_L.FREE_C04 = 'UE') ")
                            Me._StrSql.Append(vbNewLine)
                            Me._StrSql.Append(" AND H_OUTKAEDI_L.FREE_C18 = '11500245' ")
                            Me._StrSql.Append(vbNewLine)
                            '(2013.06.10) BP新システム切替対応START
                            Me._StrSql.Append(" AND MCNTSYS.M_COUNT >= 1 ")
                            Me._StrSql.Append(vbNewLine)
                            '(2013.06.10) BP新システム切替対応END
                            Me._StrSql.Append(" AND H_OUTKAEDI_L.DEL_KB = '0' ")
                            '(2013.02.27) BP新システム切替対応END

                        Case "03"
                            'イエローハット
                            '(2013.02.27) BP新システム切替対応START
                            'Me._StrSql.Append(vbNewLine)
                            'Me._StrSql.Append(" AND (H_OUTKAEDI_L.FREE_C04 = 'SO' OR H_OUTKAEDI_L.FREE_C04 = 'UE') ")
                            'Me._StrSql.Append(vbNewLine)
                            'Me._StrSql.Append(" AND H_OUTKAEDI_L.FREE_C18 = '17930000' ")
                            'Me._StrSql.Append(vbNewLine)
                            'Me._StrSql.Append(" AND H_OUTKAEDI_L.DEL_KB = '0' ")
                            Me._StrSql.Append(vbNewLine)
                            Me._StrSql.Append(" AND (H_OUTKAEDI_L.FREE_C04 = 'S2' OR H_OUTKAEDI_L.FREE_C04 = 'UE') ")
                            Me._StrSql.Append(vbNewLine)
                            Me._StrSql.Append(" AND H_OUTKAEDI_L.FREE_C18 = '11500153' ")
                            Me._StrSql.Append(vbNewLine)
                            '(2013.06.10) BP新システム切替対応START
                            Me._StrSql.Append(" AND MCNTSYS.M_COUNT >= 1 ")
                            Me._StrSql.Append(vbNewLine)
                            '(2013.06.10) BP新システム切替対応END
                            Me._StrSql.Append(" AND H_OUTKAEDI_L.DEL_KB = '0' ")
                            '(2013.02.27) BP新システム切替対応END

                        Case "04"
                            'オートバックス以外
                            '(2014.02.01) BP新システム切替対応START
                            'Me._StrSql.Append(vbNewLine)
                            'Me._StrSql.Append(" AND (H_OUTKAEDI_L.FREE_C04 = 'SO' OR H_OUTKAEDI_L.FREE_C04 = 'UE') ")
                            'Me._StrSql.Append(vbNewLine)
                            'Me._StrSql.Append(" AND H_OUTKAEDI_L.FREE_C18 = '11030000' ")
                            'Me._StrSql.Append(vbNewLine)
                            'Me._StrSql.Append(" AND H_OUTKAEDI_L.DEL_KB = '0' ")
                            Me._StrSql.Append(vbNewLine)
                            Me._StrSql.Append(" AND (NOT(H_OUTKAEDI_L.FREE_C04 = 'S2' OR H_OUTKAEDI_L.FREE_C04 = 'UE') ")
                            Me._StrSql.Append(vbNewLine)
                            Me._StrSql.Append(" OR NOT(H_OUTKAEDI_L.FREE_C18 = '11500057') ")
                            Me._StrSql.Append(vbNewLine)
                            '(2013.06.10) BP新システム切替対応START
                            Me._StrSql.Append(" OR NOT( MCNTSYS.M_COUNT = 0 ) ")
                            Me._StrSql.Append(vbNewLine)
                            '(2013.06.10) BP新システム切替対応END
                            Me._StrSql.Append(" OR NOT( H_OUTKAEDI_L.DEL_KB = '0') ) ")
                            '(2014.02.01) BP新システム切替対応END


                        Case Else
                            '上記以外は抽出対象外

                    End Select
                    '(2013.02.16)要望番号1851 BP納品書検索対応 --  END  --

#If True Then ' 日本合成化学対応(2646) 20170116 added inoue
                Case EdiCustIndex.Nichigo32516_00

                    If (Len(ds.Tables("LMH030INOUT").Rows(0)("BP_PRT_FLG")) > 0) Then
                        Me._StrSql.Append(" AND NICHIGO_HOKAN.KBN_CD IS NOT NULL ")

                        whereStr = .Item("BP_PRT_FLG").ToString()
                        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BP_PRT_FLG", whereStr, DBDataType.CHAR))
                    End If
#End If

            End Select

            '====== スプレッド項目 ======'

            '★★★
            '状態
            whereStr = .Item("JYOTAI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.SYS_DEL_FLG = @JYOTAI_KB ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JYOTAI_KB", whereStr, DBDataType.CHAR))
            End If

            '保留
            whereStr = .Item("HORYU_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If whereStr.Equals("3") Then
                    Me._StrSql.Append(" AND H_OUTKAEDI_L.DEL_KB = '3' ")
                Else
                    Me._StrSql.Append(" AND H_OUTKAEDI_L.DEL_KB <> '3' ")
                End If
            End If
            '★★★

            'オーダー番号
            whereStr = .Item("CUST_ORD_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.CUST_ORD_NO LIKE @CUST_ORD_NO")
                Me._StrSql.Append(vbNewLine)
#If False Then  'UPD 2019/01/23 依頼番号 : 003868   【LMS】オーダー番号の検索方法「前方一致⇒部分一致」
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
#Else
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))

#End If
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (H_OUTKAEDI_L.CUST_NM_L + H_OUTKAEDI_L.CUST_NM_M) LIKE @CUST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                '2012/10/12 KIM 要望番号:1512 START
                'Me._StrSql.Append(" AND (H_OUTKAEDI_L.DEST_NM LIKE @DEST_NM")
                'Me._StrSql.Append(" OR M_DEST.DEST_NM LIKE @DEST_NM)")
                Me._StrSql.Append(" AND H_OUTKAEDI_L.DEST_NM LIKE @DEST_NM")
                '2012/10/12 KIM 要望番号:1512 END
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '出荷時注意事項
            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (H_OUTKAEDI_L.REMARK LIKE @REMARK")
                Me._StrSql.Append(" OR C_OUTKA_L.REMARK LIKE @REMARK)")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '配送時注意事項
            whereStr = .Item("UNSO_ATT").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (H_OUTKAEDI_L.UNSO_ATT LIKE @UNSO_ATT")
                Me._StrSql.Append(" OR F_UNSO_L.REMARK LIKE @UNSO_ATT)")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ATT", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '商品名（中1）
            whereStr = .Item("GOODS_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_M_FST.GOODS_NM LIKE @GOODS_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先住所
            whereStr = .Item("DEST_AD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                '2012/10/12 KIM 要望番号:1512 START
                'Me._StrSql.Append(" AND (M_DEST.AD_1 + M_DEST.AD_2 + M_DEST.AD_3 LIKE @DEST_AD")
                'Me._StrSql.Append(" OR H_OUTKAEDI_L.DEST_AD_1 + H_OUTKAEDI_L.DEST_AD_2 + C_OUTKA_L.DEST_AD_3 LIKE @DEST_AD)")
                '2012/10/22 HONMYO 要望番号:1529(EDI出荷検索画面、届け先住所が表示されない) START
                'Me._StrSql.Append(" AND H_OUTKAEDI_L.DEST_AD_1 + H_OUTKAEDI_L.DEST_AD_2 + C_OUTKA_L.DEST_AD_3 LIKE @DEST_AD ")
                Me._StrSql.Append(" AND H_OUTKAEDI_L.DEST_AD_1 + H_OUTKAEDI_L.DEST_AD_2 + H_OUTKAEDI_L.DEST_AD_3 LIKE @DEST_AD ")
                '2012/10/22 HONMYO 要望番号:1529(EDI出荷検索画面、届け先住所が表示されない) END
                '2012/10/12 KIM 要望番号:1512 END
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '運送会社名
            whereStr = .Item("UNSO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_UNSOCO.UNSOCO_NM LIKE @UNSOCO_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '便区分
            whereStr = .Item("BIN_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.BIN_KB = @BIN_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", whereStr, DBDataType.CHAR))
            End If

            'EDI管理番号(大)
            whereStr = .Item("EDI_CTL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.EDI_CTL_NO LIKE @EDI_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '出荷管理番号(大)
            whereStr = .Item("OUTKA_CTL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.OUTKA_CTL_NO LIKE @KANRI_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            'まとめ番号(大)
            whereStr = .Item("MATOME_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SUBSTRING(H_OUTKAEDI_L.FREE_C30,4,9) LIKE @MATOME_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATOME_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '2012.03.26 大阪対応START
            '運送番号
            whereStr = .Item("UNSO_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F_UNSO_L.UNSO_NO_L LIKE @UNSO_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If
            '2012.03.26 大阪対応END

            '2012.11.11 センコー対応START
            '運行番号
            whereStr = .Item("TRIP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND F_UNSO_L.TRIP_NO LIKE @TRIP_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If
            '2012.11.11 センコー対応END

            '注文番号
            whereStr = .Item("BUYER_ORD_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.BUYER_ORD_NO LIKE @BUYER_ORD_NO")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '出荷種別
            whereStr = .Item("SYUBETU_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.SYUBETU_KB = @SYUBETU_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", whereStr, DBDataType.CHAR))
            End If

            'ピック区分
            whereStr = .Item("PICK_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.PICK_KB = @PICK_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_KB", whereStr, DBDataType.CHAR))
            End If

            'タリフ分類区分
            whereStr = .Item("UNSO_MOTO_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_OUTKAEDI_L.UNSO_TEHAI_KB = @UNSO_MOTO_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_MOTO_KB", whereStr, DBDataType.CHAR))
            End If

            '担当者
            whereStr = .Item("TANTO_USER").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TANTO_USER.USER_NM LIKE @TANTO_USER")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TANTO_USER", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '作成者
            whereStr = .Item("SYS_ENT_USER").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ENT_USER.USER_NM LIKE @SYS_ENT_USER")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '最終更新者
            whereStr = .Item("SYS_UPD_USER").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UPD_USER.USER_NM LIKE @SYS_UPD_USER")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If
            '受信ファイル名  2013.03.04 / Notes190

            If String.IsNullOrEmpty(rcv_Nm) = False Then

                'ADD START 2018/11/06 要望番号002775(検索のタイムアウト対策)
                Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))
                    '【注意】以下のCaseを修正する場合はSelectData()、SelectListData()の両方のSELECT句、FROM句、WHERE句をあわせて修正すること
                    Case EdiCustIndex.DicKkb10001_00 _
                       , EdiCustIndex.DicKkb10001_03 _
                       , EdiCustIndex.DicKkb10012_00 _
                       , EdiCustIndex.DicKkb10013_00 _
                       , EdiCustIndex.DicChbYuso10010_00

                    Case Else
                        'ADD END 2018/11/06 要望番号002775(検索のタイムアウト対策)

                        whereStr = .Item("FILE_NAME").ToString
                        If String.IsNullOrEmpty(whereStr) = False Then
                            Me._StrSql.Append(" AND RCV_HED_FILER.FILE_NAME LIKE @FILE_NAME")
                            Me._StrSql.Append(vbNewLine)
                            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
                        End If

                End Select      'ADD 2018/11/06 要望番号002775(検索のタイムアウト対策)

            End If

        End With

    End Sub

    ''' <summary>
    ''' 検索用OrderBy句作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLOrderBy()

        Me._StrSqlOrderBy.Append(" ORDER BY ")
        Me._StrSqlOrderBy.Append(vbNewLine)
        Me._StrSqlOrderBy.Append(" ISNULL(C_OUTKA_L.OUTKA_PLAN_DATE, H_OUTKAEDI_L.OUTKA_PLAN_DATE ) ")
        Me._StrSqlOrderBy.Append(vbNewLine)
        '2012/10/12 KIM 要望番号:1512 START
        'Me._StrSqlOrderBy.Append(",ISNULL(M_DEST.DEST_NM,H_OUTKAEDI_L.DEST_NM ) ")
        Me._StrSqlOrderBy.Append(",H_OUTKAEDI_L.DEST_NM ")
        '2012/10/12 KIM 要望番号:1512 END
        Me._StrSqlOrderBy.Append(vbNewLine)
        Me._StrSqlOrderBy.Append(",H_OUTKAEDI_L.FREE_C30 ")
        Me._StrSqlOrderBy.Append(vbNewLine)
        Me._StrSqlOrderBy.Append(",H_OUTKAEDI_L.EDI_CTL_NO ")
        Me._StrSqlOrderBy.Append(vbNewLine)
        Me._StrSqlOrderBy.Append(",H_OUTKAEDI_L.OUTKA_CTL_NO ")


        'SQL文にOrderBy追加
        Me._StrSql.Append(Me._StrSqlOrderBy.ToString())

    End Sub

    '要望番号1870:BPの並び順は届先住所順にするSTART
    ''' <summary>
    ''' 検索用OrderBy句作成(BP用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLOrderByBp()

        Me._StrSqlOrderBy.Append(" ORDER BY ")
        Me._StrSqlOrderBy.Append(vbNewLine)
        Me._StrSqlOrderBy.Append(" ISNULL(C_OUTKA_L.OUTKA_PLAN_DATE, H_OUTKAEDI_L.OUTKA_PLAN_DATE ) ")
        Me._StrSqlOrderBy.Append(vbNewLine)
        Me._StrSqlOrderBy.Append(",H_OUTKAEDI_L.DEST_AD_1 ")
        Me._StrSqlOrderBy.Append(vbNewLine)
        Me._StrSqlOrderBy.Append(",H_OUTKAEDI_L.DEST_AD_2 ")
        Me._StrSqlOrderBy.Append(vbNewLine)
        Me._StrSqlOrderBy.Append(",H_OUTKAEDI_L.DEST_AD_3 ")
        Me._StrSqlOrderBy.Append(vbNewLine)
        Me._StrSqlOrderBy.Append(",H_OUTKAEDI_L.DEST_NM ")
        Me._StrSqlOrderBy.Append(vbNewLine)
        Me._StrSqlOrderBy.Append(",H_OUTKAEDI_L.DEST_CD ")  '要望追加　2013/02/20　本明
        Me._StrSqlOrderBy.Append(vbNewLine)                 '要望追加　2013/02/20　本明
        Me._StrSqlOrderBy.Append(",H_OUTKAEDI_L.FREE_C30 ")
        Me._StrSqlOrderBy.Append(vbNewLine)
        Me._StrSqlOrderBy.Append(",H_OUTKAEDI_L.EDI_CTL_NO ")
        Me._StrSqlOrderBy.Append(vbNewLine)
        Me._StrSqlOrderBy.Append(",H_OUTKAEDI_L.OUTKA_CTL_NO ")


        'SQL文にOrderBy追加
        Me._StrSql.Append(Me._StrSqlOrderBy.ToString())

    End Sub
    '要望番号1870:BPの並び順は届先住所順にするEND

#End Region

    '▼▼▼二次
#Region "出荷登録初期設定"

    ''' <summary>
    ''' 荷主マスタ値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMcustOutkaToroku(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_M_CUST_OUTKATOUROKU)

        Dim dt As DataTable = ds.Tables("LMH030INOUT")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetMcustParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectMcustOutkaToroku", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable

        '取得データの格納先をマッピング
        map = Me.SetMCustMap()

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_CUST")
        reader.Close()

        Return ds

    End Function

#If True Then   'ADD 2019/03/13 依頼番号 : 004085   【LMS】古河事業所日立物流_危険品と一般品の運賃請求：オーダー合算を廃止、別オーダーとして請求
    ''' <summary>
    ''' 荷主マスタ値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMcustOutkaToroku2(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_M_CUST_OUTKATOUROKU)

        Dim dt As DataTable = ds.Tables("LMH030INOUT")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetMcustParameter2(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectMcustOutkaToroku", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable

        '取得データの格納先をマッピング
        map = Me.SetMCustMap()

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_M_CUST")
        reader.Close()

        Return ds

    End Function

#End If

    ''' <summary>
    ''' 荷主マスタをHashTableに設定
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetMCustMap() As Hashtable

        Dim map As Hashtable = New Hashtable

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CUST_OYA_CD", "CUST_OYA_CD")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("CUST_NM_SS", "CUST_NM_SS")
        map.Add("ZIP", "ZIP")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        map.Add("PIC", "PIC")
        map.Add("FUKU_PIC", "FUKU_PIC")
        map.Add("TEL", "TEL")
        map.Add("FAX", "FAX")
        map.Add("MAIL", "MAIL")
        map.Add("SAITEI_HAN_KB", "SAITEI_HAN_KB")
        map.Add("OYA_SEIQTO_CD", "OYA_SEIQTO_CD")
        map.Add("HOKAN_SEIQTO_CD", "HOKAN_SEIQTO_CD")
        map.Add("NIYAKU_SEIQTO_CD", "NIYAKU_SEIQTO_CD")
        map.Add("UNCHIN_SEIQTO_CD", "UNCHIN_SEIQTO_CD")
        map.Add("SAGYO_SEIQTO_CD", "SAGYO_SEIQTO_CD")
        map.Add("INKA_RPT_YN", "INKA_RPT_YN")
        map.Add("OUTKA_RPT_YN", "OUTKA_RPT_YN")
        map.Add("ZAI_RPT_YN", "ZAI_RPT_YN")
        map.Add("UNSO_TEHAI_KB", "UNSO_TEHAI_KB")
        map.Add("SP_UNSO_CD", "SP_UNSO_CD")
        map.Add("SP_UNSO_BR_CD", "SP_UNSO_BR_CD")
        map.Add("BETU_KYORI_CD", "BETU_KYORI_CD")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("HOKAN_FREE_KIKAN", "HOKAN_FREE_KIKAN")
        map.Add("SMPL_SAGYO", "SMPL_SAGYO")
        map.Add("HOKAN_NIYAKU_CALCULATION", "HOKAN_NIYAKU_CALCULATION")
        map.Add("HOKAN_NIYAKU_CALCULATION_OLD", "HOKAN_NIYAKU_CALCULATION_OLD")
        map.Add("NEW_JOB_NO", "NEW_JOB_NO")
        map.Add("OLD_JOB_NO", "OLD_JOB_NO")
        map.Add("HOKAN_NIYAKU_KEISAN_YN", "HOKAN_NIYAKU_KEISAN_YN")
        map.Add("UNTIN_CALCULATION_KB", "UNTIN_CALCULATION_KB")
        map.Add("DENPYO_NM", "DENPYO_NM")
        map.Add("SOKO_CHANGE_KB", "SOKO_CHANGE_KB")
        map.Add("DEFAULT_SOKO_CD", "DEFAULT_SOKO_CD")
        map.Add("PICK_LIST_KB", "PICK_LIST_KB")
        map.Add("SEKY_OFB_KB", "SEKY_OFB_KB")

        Return map

    End Function

    '(2012.09.07)要望番号:1425 UMANO START
    ''' <summary>
    ''' タリフコード設定処理(荷主・運送会社)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフコードと割増タリフコードの設定</remarks>

    Private Function SetUnsocoTariffData(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_UNCHIN_TARIFF_SET_UNSOCO)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUnchinTariffSetParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SetUnsocoTariffData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim setCnt As Integer = 0

        If reader.Read() = True Then

            '運賃タリフコード（屯キロ建）が空または荷主明細マスタより荷主取得ができた場合
            If String.IsNullOrEmpty(dt.Rows(0)("UNCHIN_TARIFF_CD").ToString().Trim()) = True _
               OrElse (ds.Tables("LMH030_M_CUST_DETAILS").Rows.Count <> 0 AndAlso
                       ((ds.Tables("LMH030_M_CUST_DETAILS").Rows(0).Item("SET_NAIYO").ToString()).Equals("01") = True OrElse
                       (ds.Tables("LMH030_M_CUST_DETAILS").Rows(0).Item("SET_NAIYO").ToString()).Equals("02") = True)) Then

                dt.Rows(0)("UNCHIN_TARIFF_CD") = reader("UNCHIN_TARIFF_CD").ToString().Trim()
                setCnt = 1
            End If

            '割増タリフコードが空または荷主明細マスタより荷主取得ができた場合
            If String.IsNullOrEmpty(dt.Rows(0)("EXTC_TARIFF_CD").ToString().Trim()) = True _
               OrElse (ds.Tables("LMH030_M_CUST_DETAILS").Rows.Count <> 0 AndAlso
                       ((ds.Tables("LMH030_M_CUST_DETAILS").Rows(0).Item("SET_NAIYO").ToString()).Equals("01") = True OrElse
                       (ds.Tables("LMH030_M_CUST_DETAILS").Rows(0).Item("SET_NAIYO").ToString()).Equals("03") = True)) Then

                dt.Rows(0)("EXTC_TARIFF_CD") = reader("EXTC_TARIFF_CD").ToString().Trim()
                setCnt = setCnt + 2
            End If

        End If

        '処理件数の設定
        MyBase.SetResultCount(setCnt)
        reader.Close()

        Return ds

    End Function
    '(2012.09.07)要望番号:1425 UMANO END

    ''' <summary>
    ''' タリフコード設定処理(運賃タリフ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフコードと割増タリフコードの設定</remarks>

    Private Function SetTariffData(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        'SQL作成
        Dim freeC29 As String = dt.Rows(0)("FREE_C29").ToString().Trim()

        'FreeC29が空もしくはFreeC29の上1桁が"1"の場合、届先コードから取得
        'FreeC29の上1桁が"0"の場合、荷主コードから取得
        If String.IsNullOrEmpty(freeC29) = True OrElse freeC29.Substring(0, 1) = "1" Then
            '2012.03.05 大阪対応START
            If String.IsNullOrEmpty(dt.Rows(0)("UNCHIN_TARIFF_FLG").ToString().Trim()) = True Then
                Me._StrSql.Append(LMH030DAC.SQL_UNCHIN_TARIFF_SET_DEST)
            ElseIf (dt.Rows(0)("UNCHIN_TARIFF_FLG").ToString().Trim()).Equals("1") = True Then
                Me._StrSql.Append(LMH030DAC.SQL_UNCHIN_TARIFF_SET_CUST)
            End If
            '2012.03.05 大阪対応END
        ElseIf freeC29.Substring(0, 1) = "0" OrElse freeC29.Substring(0, 1) = "2" Then
            Me._StrSql.Append(LMH030DAC.SQL_UNCHIN_TARIFF_SET_CUST)
        End If

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUnchinTariffSetParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SetTariffData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        If reader.Read() = True Then

            'タリフ分類区分を設定
            '2012.02.25 大阪対応 START
            'If String.IsNullOrEmpty(dt.Rows(0)("UNSO_TEHAI_KB").ToString().Trim()) = True Then
            '    If String.IsNullOrEmpty(reader("TARIFF_BUNRUI_KB").ToString().Trim()) = False Then
            '        dt.Rows(0)("UNSO_TEHAI_KB") = reader("TARIFF_BUNRUI_KB").ToString().Trim()
            '    Else
            '        dt.Rows(0)("UNSO_TEHAI_KB") = "10"
            '    End If
            'End If

            'タリフ分類区分が空または荷主明細マスタより荷主取得ができた場合
            If String.IsNullOrEmpty(dt.Rows(0)("UNSO_TEHAI_KB").ToString().Trim()) = True _
               OrElse (ds.Tables("LMH030_M_CUST_DETAILS").Rows.Count <> 0 AndAlso
                       ((ds.Tables("LMH030_M_CUST_DETAILS").Rows(0).Item("SET_NAIYO").ToString()).Equals("01") = True OrElse
                       (ds.Tables("LMH030_M_CUST_DETAILS").Rows(0).Item("SET_NAIYO").ToString()).Equals("02") = True OrElse
                       (ds.Tables("LMH030_M_CUST_DETAILS").Rows(0).Item("SET_NAIYO").ToString()).Equals("03") = True)) Then
                If String.IsNullOrEmpty(reader("TARIFF_BUNRUI_KB").ToString().Trim()) = False Then
                    dt.Rows(0)("UNSO_TEHAI_KB") = reader("TARIFF_BUNRUI_KB").ToString().Trim()
                Else
                    dt.Rows(0)("UNSO_TEHAI_KB") = "10"
                End If
            End If
            '2012.02.25 大阪対応 END

            Dim unsoTehaiKb As String = dt.Rows(0)("UNSO_TEHAI_KB").ToString().Trim()

            '運賃タリフコードを設定
            '2012.02.25 大阪対応 START
            'If String.IsNullOrEmpty(dt.Rows(0)("UNCHIN_TARIFF_CD").ToString().Trim()) = True Then
            '    If unsoTehaiKb = "10" Then
            '        dt.Rows(0)("UNCHIN_TARIFF_CD") = reader("UNCHIN_TARIFF_CD1").ToString().Trim()
            '    ElseIf unsoTehaiKb = "20" Then
            '        dt.Rows(0)("UNCHIN_TARIFF_CD") = reader("UNCHIN_TARIFF_CD2").ToString().Trim()
            '    ElseIf unsoTehaiKb = "40" Then
            '        dt.Rows(0)("UNCHIN_TARIFF_CD") = reader("YOKO_TARIFF_CD").ToString().Trim()
            '    End If
            'End If

            '運賃タリフコードが空または荷主明細マスタより荷主取得ができた場合
            If String.IsNullOrEmpty(dt.Rows(0)("UNCHIN_TARIFF_CD").ToString().Trim()) = True _
               OrElse (ds.Tables("LMH030_M_CUST_DETAILS").Rows.Count <> 0 AndAlso
                       ((ds.Tables("LMH030_M_CUST_DETAILS").Rows(0).Item("SET_NAIYO").ToString()).Equals("01") = True OrElse
                       (ds.Tables("LMH030_M_CUST_DETAILS").Rows(0).Item("SET_NAIYO").ToString()).Equals("02") = True)) Then
                If unsoTehaiKb = "10" Then
                    dt.Rows(0)("UNCHIN_TARIFF_CD") = reader("UNCHIN_TARIFF_CD1").ToString().Trim()
                ElseIf unsoTehaiKb = "20" Then
                    dt.Rows(0)("UNCHIN_TARIFF_CD") = reader("UNCHIN_TARIFF_CD2").ToString().Trim()
                ElseIf unsoTehaiKb = "40" Then
                    '(2012.09.11)要望番号1434 修正START
                    dt.Rows(0)("UNCHIN_TARIFF_CD") = reader("YOKO_TARIFF_CD").ToString().Trim()
                    '(2012.09.11)要望番号1434 修正END
                End If
            End If
            '2012.02.25 大阪対応 END

            '割増タリフコードを設定
            '2012.02.25 大阪対応 START
            'If String.IsNullOrEmpty(dt.Rows(0)("EXTC_TARIFF_CD").ToString().Trim()) = True Then

            '    If String.IsNullOrEmpty(freeC29) = True Then
            '        dt.Rows(0)("EXTC_TARIFF_CD") = reader("EXTC_TARIFF_CD").ToString().Trim()
            '    ElseIf freeC29.Substring(0, 1) = "0" Then
            '        dt.Rows(0)("EXTC_TARIFF_CD") = reader("EXTC_TARIFF_CD").ToString().Trim()
            '    ElseIf freeC29.Substring(0, 1) = "1" Then
            '        dt.Rows(0)("EXTC_TARIFF_CD") = reader("UNCHIN_TARIFF_CD1").ToString().Trim()
            '    End If

            'End If

            ''割増タリフコードが空または荷主明細マスタより荷主取得ができた場合
            'If String.IsNullOrEmpty(dt.Rows(0)("EXTC_TARIFF_CD").ToString().Trim()) = True _
            '   OrElse (ds.Tables("LMH030_M_CUST_DETAILS").Rows.Count <> 0 AndAlso _
            '           (ds.Tables("LMH030_M_CUST_DETAILS").Rows(0).Item("SET_NAIYO").ToString()).Equals("1")) = True Then

            '    If String.IsNullOrEmpty(freeC29) = True Then
            '        dt.Rows(0)("EXTC_TARIFF_CD") = reader("EXTC_TARIFF_CD").ToString().Trim()
            '    ElseIf freeC29.Substring(0, 1) = "0" Then
            '        dt.Rows(0)("EXTC_TARIFF_CD") = reader("EXTC_TARIFF_CD").ToString().Trim()
            '    ElseIf freeC29.Substring(0, 1) = "1" Then
            '        dt.Rows(0)("EXTC_TARIFF_CD") = reader("UNCHIN_TARIFF_CD1").ToString().Trim()
            '    End If

            'End If
            ''2012.02.25 大阪対応 END

            '2012.03.05 大阪対応START
        ElseIf reader.Read() = False AndAlso (String.IsNullOrEmpty(freeC29) = True OrElse freeC29.Substring(0, 1) = "1") Then

            reader.Close()
            dt.Rows(0)("UNCHIN_TARIFF_FLG") = "1"
            ds = Me.SetTariffData(ds)

            If String.IsNullOrEmpty(dt.Rows(0)("UNSO_TEHAI_KB").ToString().Trim()) = True Then
                dt.Rows(0)("UNSO_TEHAI_KB") = "10"
            End If

            Return ds
            '2012.03.05 大阪対応END

        End If

        If String.IsNullOrEmpty(dt.Rows(0)("UNSO_TEHAI_KB").ToString().Trim()) = True Then
            dt.Rows(0)("UNSO_TEHAI_KB") = "10"
        End If

        reader.Close()

        Return ds

    End Function

    '2012.03.05 大阪対応START
    ''' <summary>
    ''' タリフコード設定処理(割増タリフ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフコードと割増タリフコードの設定</remarks>

    Private Function SetExtcTariffData(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        'SQL作成
        Dim freeC29 As String = dt.Rows(0)("FREE_C29").ToString().Trim()

        'FreeC29が空もしくはFreeC29の上1桁が"1"の場合、届先コードから取得
        'FreeC29の上1桁が"0"の場合、荷主コードから取得
        If String.IsNullOrEmpty(freeC29) = True OrElse freeC29.Substring(1, 1) = "1" Then
            '2012.03.05 大阪対応START
            If String.IsNullOrEmpty(dt.Rows(0)("EXTC_TARIFF_FLG").ToString().Trim()) = True Then
                Me._StrSql.Append(LMH030DAC.SQL_UNCHIN_TARIFF_SET_DEST)
            ElseIf (dt.Rows(0)("EXTC_TARIFF_FLG").ToString().Trim()).Equals("1") = True Then
                Me._StrSql.Append(LMH030DAC.SQL_UNCHIN_TARIFF_SET_CUST)
            End If
            '2012.03.05 大阪対応END
        ElseIf freeC29.Substring(1, 1) = "0" OrElse freeC29.Substring(1, 1) = "2" Then
            Me._StrSql.Append(LMH030DAC.SQL_UNCHIN_TARIFF_SET_CUST)
        End If

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUnchinTariffSetParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SetExtcTariffData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        If reader.Read() = True Then

            '割増タリフコードを設定
            '2012.02.25 大阪対応 START
            'If String.IsNullOrEmpty(dt.Rows(0)("EXTC_TARIFF_CD").ToString().Trim()) = True Then

            '    If String.IsNullOrEmpty(freeC29) = True Then
            '        dt.Rows(0)("EXTC_TARIFF_CD") = reader("EXTC_TARIFF_CD").ToString().Trim()
            '    ElseIf freeC29.Substring(1, 1) = "0" Then
            '        dt.Rows(0)("EXTC_TARIFF_CD") = reader("EXTC_TARIFF_CD").ToString().Trim()
            '    ElseIf freeC29.Substring(1, 1) = "1" Then
            '        dt.Rows(0)("EXTC_TARIFF_CD") = reader("UNCHIN_TARIFF_CD1").ToString().Trim()
            '    End If

            'End If

            '割増タリフコードが空または荷主明細マスタより荷主取得ができた場合
            If String.IsNullOrEmpty(dt.Rows(0)("EXTC_TARIFF_CD").ToString().Trim()) = True _
               OrElse (ds.Tables("LMH030_M_CUST_DETAILS").Rows.Count <> 0 AndAlso
                       ((ds.Tables("LMH030_M_CUST_DETAILS").Rows(0).Item("SET_NAIYO").ToString()).Equals("01") = True OrElse
                       (ds.Tables("LMH030_M_CUST_DETAILS").Rows(0).Item("SET_NAIYO").ToString()).Equals("03") = True)) Then

                If String.IsNullOrEmpty(freeC29) = True Then
                    dt.Rows(0)("EXTC_TARIFF_CD") = reader("EXTC_TARIFF_CD").ToString().Trim()
                ElseIf freeC29.Substring(1, 1) = "0" OrElse freeC29.Substring(1, 1) = "2" Then
                    dt.Rows(0)("EXTC_TARIFF_CD") = reader("EXTC_TARIFF_CD").ToString().Trim()
                ElseIf freeC29.Substring(1, 1) = "1" Then
                    '2012.03.08 要望番号793 同等不具合 修正START
                    dt.Rows(0)("EXTC_TARIFF_CD") = reader("EXTC_TARIFF_CD").ToString().Trim()
                    'dt.Rows(0)("EXTC_TARIFF_CD") = reader("UNCHIN_TARIFF_CD1").ToString().Trim()
                    '2012.03.08 要望番号793 同等不具合 修正END
                End If

            End If
            '2012.02.25 大阪対応 END

            '2012.03.05 大阪対応START
        ElseIf reader.Read() = False AndAlso (String.IsNullOrEmpty(freeC29) = True OrElse freeC29.Substring(1, 1) = "1") Then

            reader.Close()
            dt.Rows(0)("EXTC_TARIFF_FLG") = "1"
            ds = Me.SetExtcTariffData(ds)

            Return ds
            '2012.03.05 大阪対応END

        End If

        reader.Close()

        Return ds

    End Function
    '2012.03.05 大阪対応END

    ''' <summary>
    ''' タリフコード設定処理パラメータ設定
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub SetUnchinTariffSetParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", dt.Rows(0).Item("CUST_CD_M"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", dt.Rows(0).Item("DEST_CD"), DBDataType.NVARCHAR))

        '(2012.09.07)要望番号:1425 UMANO START
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", dt.Rows(0).Item("UNSO_CD"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", dt.Rows(0).Item("UNSO_BR_CD"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_MOTO_KB", dt.Rows(0).Item("UNSO_MOTO_KB"), DBDataType.CHAR))
        '(2012.09.07)要望番号:1425 UMANO END

    End Sub

    ''' <summary>
    ''' 荷送人名(大)設定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフコードと割増タリフコードの設定</remarks>
    Private Function SetShip_NM_L(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMH030DAC.SQL_SELECT_SHIPNM_FROM_DEST)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetPramShip(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SetTariffData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        If reader.Read() = True Then
            dt.Rows(0)("SHIP_CD_L") = reader("DEST_NM").ToString().Trim()
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 荷送人名(大)設定処理パラメータ設定
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub SetPramShip(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))

        Dim shipCdL As String = dt.Rows(0).Item("SHIP_CD_L").ToString().Trim()
        Dim destCd As String = dt.Rows(0).Item("DEST_CD").ToString().Trim()
        Dim ediDestCd As String = dt.Rows(0).Item("EDI_DEST_CD").ToString().Trim()

        If shipCdL <> destCd Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", dt.Rows(0).Item("SHIP_CD_L"), DBDataType.NVARCHAR))
        ElseIf shipCdL = destCd AndAlso destCd = ediDestCd Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", dt.Rows(0).Item("DEST_CD"), DBDataType.NVARCHAR))
        Else
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", dt.Rows(0).Item("SHIP_CD_L"), DBDataType.NVARCHAR))
        End If

    End Sub

#End Region

#Region "一括変更処理"

#Region "運送会社存在チェック＋名称取得処理"

    ''' <summary>
    ''' 運送会社名設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送会社マスタの結果取得SQLの構築・発行</remarks>
    Private Function SelectUnsoNM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dtVal As DataTable = ds.Tables("LMH030OUT_UPDATE_VALUE")
        Dim dtKey As DataTable = ds.Tables("LMH030OUT_UPDATE_KEY")

        'INTableの条件rowの格納
        Me._Row = dtKey.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSqlOrderBy = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_UNSONM_GET)      'SQL構築(データ抽出用Select句)
        Call Me.setSQLSelectExists(dtVal, dtKey)            '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectUnsoNM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("EDIT_ITEM_VALUE3", "EDIT_ITEM_VALUE3")
        map.Add("EDIT_ITEM_VALUE4", "EDIT_ITEM_VALUE4")
        Dim ds2 As DataSet = ds.Clone

        If reader.HasRows() = True Then
            ds2 = MyBase.SetSelectResultToDataSet(map, ds2, reader, "LMH030OUT_UPDATE_VALUE")

            ds.Tables("LMH030OUT_UPDATE_VALUE").Rows(0)("EDIT_ITEM_VALUE3") = ds2.Tables("LMH030OUT_UPDATE_VALUE").Rows(0)("EDIT_ITEM_VALUE3")
            ds.Tables("LMH030OUT_UPDATE_VALUE").Rows(0)("EDIT_ITEM_VALUE4") = ds2.Tables("LMH030OUT_UPDATE_VALUE").Rows(0)("EDIT_ITEM_VALUE4")

        End If
        reader.Close()

        'SQLの発行
        Dim readerCnt As SqlDataReader = MyBase.GetSelectResult(cmd)
        '処理件数の設定
        readerCnt.Read()
        If String.IsNullOrEmpty(ds.Tables("LMH030OUT_UPDATE_VALUE").Rows(0)("EDIT_ITEM_VALUE3").ToString()) = True Then
            readerCnt.Close()
            MyBase.SetMessage("E079", New String() {"運送会社マスタ", "運送会社コード"})
            Return ds
        End If
        readerCnt.Close()

        Return ds

    End Function

#End Region

#End Region

#Region "同一まとめレコード取得処理(出荷取消⇒未登録)"

    Private Function SelectMatomeTorikesi(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_SELECT_DATA_MATOMETORIKESI)      'SQL構築(データ抽出用Select句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        '受信HEDテーブル名設定
        sql = Me.SetRcvTableNm(sql)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetMatomeTorikesiSelectParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectMatomeTorikesi", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("RCV_SYS_UPD_DATE", "RCV_SYS_UPD_DATE")
        map.Add("RCV_SYS_UPD_TIME", "RCV_SYS_UPD_TIME")
        map.Add("RCV_NM_HED", "RCV_NM_HED")
        map.Add("RCV_NM_DTL", "RCV_NM_DTL")
        map.Add("RCV_NM_EXT", "RCV_NM_EXT")
        map.Add("SND_NM", "SND_NM")
        map.Add("EDI_CUST_INOUTFLG", "EDI_CUST_INOUTFLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030OUT")

        Return ds

    End Function

#End Region

#Region "SHIHARAI_UNCHIN"

    ''' <summary>
    ''' 支払運賃テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>支払運賃新規登録SQLの構築・発行</remarks>
    Private Function InsertShiharaiUnchinData(ByVal ds As DataSet) As DataSet

        If ds.Tables("F_SHIHARAI_TRS").Rows.Count = 0 Then
            'F_UNCHIN_TRSが0件ということは本来無いが、一応念のために0件の時はINSERT処理が行われないようにする
            Return ds
        End If

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("F_SHIHARAI_TRS")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH030DAC.SQL_SHIHARAI_INSERT _
                                                                       , ds.Tables("F_SHIHARAI_TRS").Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetShiharaiComParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC", "InsertShiharaiUnchinData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "Update"

#Region "一括変更時のEDI出荷(大)"

    ''' <summary>
    ''' EDI出荷(大)テーブル更新（一括変更）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>EDI出荷(大)テーブル更新（一括変更）</remarks>
    Private Function UpdateHenko(ByVal ds As DataSet) As DataSet

        'update件数格納変数
        Dim updCnt As Integer = 0
        Dim strSql As String = String.Empty

        'DataSetのIN情報を取得
        Dim dtKey As DataTable = ds.Tables("LMH030OUT_UPDATE_KEY")
        Dim dtValue As DataTable = ds.Tables("LMH030OUT_UPDATE_VALUE")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        '更新項目
        Call Me.SetsqlEdit(dtValue)

        'ADD Start 2018/02/26 依頼番号:1198 東レ・ダウ届先一括変更
        If ds.Tables("LMH030OUT_UPDATE_VALUE").Rows(0)("EDIT_ITEM_KBN").ToString() = "06" Then

            Dim dtMDstValue As DataTable = ds.Tables("LMH030_M_DEST")
            '届先M更新項目
            Call Me.SetsqlEditMDSET(dtMDstValue)
        End If
        'ADD End   2018/02/26 依頼番号:1198 東レ・ダウ届先一括変更

        '共通項目
        Me._StrSql.Append(LMH030DAC.SQL_UPDATE)

        'SQL作成
        Me._StrSql.Append(strSql)


        'SQLパラメータ設定
        Call Me.SetUpdHenkoPrm(dtKey, dtValue)

        'ADD Start 2018/02/26 依頼番号:1198 東レ・ダウ届先一括変更
        If ds.Tables("LMH030OUT_UPDATE_VALUE").Rows(0)("EDIT_ITEM_KBN").ToString() = "06" Then

            Dim dtMDstValue As DataTable = ds.Tables("LMH030_M_DEST")
            'SQLパラメータ設定(届先M)
            Call Me.SetUpdMDestPrm(dtMDstValue)
        End If
        'ADD End   2018/02/26 依頼番号:1198 東レ・ダウ届先一括変更

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), dtKey.Rows(0).Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "UpdateHenko", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "H_OUTKAEDI_L"

    ''' <summary>
    ''' EDI出荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaEdiLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        '共通化暫定対応 START
        Dim autoMatomeF As String = dtIn.Rows(0).Item("AUTO_MATOME_FLG").ToString()
        '共通化暫定対応 END

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '実績取消SQL CONST名
            Case LMH030DAC.EventShubetsu.TORIKESIJISSEKI
                setSql = LMH030DAC.SQL_UPDATE_JISSEKIDELEDI_L

                'EDI取消、EDI取消⇒未登録SQL CONST名
            Case LMH030DAC.EventShubetsu.EDITORIKESI _
                , LMH030DAC.EventShubetsu.TORIKESI_MITOUROKU

                setSql = LMH030DAC.SQL_UPD_EDITORIKESI_EDI_L

                '実行(実績作成済⇒実績未,実績送信済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI _
                , LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI

                setSql = LMH030DAC.SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_L

                '実行(実績送信済⇒送信待)SQL CONST名
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_SOUSINMACHI

                setSql = LMH030DAC.SQL_UPDATE_JISSEKIZUMI_SOUSINMACHI_EDI_L

                '実行(出荷取消⇒未登録)SQL CONST名
            Case LMH030DAC.EventShubetsu.TOUROKUZUMI_MITOUROKU

                '共通化暫定対応 START
                If autoMatomeF.Equals("9") = True Then
                    'まとめデータでない場合
                    setSql = LMH030DAC.SQL_UPDATE_TOUROKUMI_EDI_L

                Else
                    'まとめデータの場合
                    setSql = LMH030DAC.SQL_UPDATE_TOUROKUMI_EDI_L_MATOME
                End If
                '共通化暫定対応 END

                '2012.04.04 大阪対応追加START
                '実行(運送取消⇒未登録)SQL CONST名
            Case LMH030DAC.EventShubetsu.UNSOTORIKESI_MITOUROKU
                setSql = LMH030DAC.SQL_UPDATE_UNSO_TOUROKUMI_EDI_L

                '出荷管理番号に営業所イニシャル設定
                setSql = Me.SetBrInitial(setSql, inTbl.Rows(0))

                '2012.04.04 大阪対応追加END
            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(setSql, SQL_COM_UPDATE_CONDITION) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))
        'パラメータ設定
        Call Me.SetOutkaEdiLComParameter(Me._Row, Me._SqlPrmList, dtEventShubetsu)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(inTbl.Rows(0), Me._SqlPrmList)
        Call Me.SetJissekiParameterEdiLM(inTbl.Rows(0), dtEventShubetsu)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "UpdateOutkaEdiLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "H_OUTKAEDI_M"

    ''' <summary>
    ''' EDI出荷(中)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(中)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaEdiMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim ediMTbl As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        '共通化暫定対応 START
        Dim autoMatomeF As String = dtIn.Rows(0)("AUTO_MATOME_FLG").ToString()
        '共通化暫定対応 END

        Dim loopflg As Integer = 0

        Dim rtn As Integer = 0

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            'EDI取消、EDI取消⇒未登録SQL CONST名
            Case LMH030DAC.EventShubetsu.EDITORIKESI _
                , LMH030DAC.EventShubetsu.TORIKESI_MITOUROKU

                setSql = LMH030DAC.SQL_UPD_EDITORIKESI_EDI_M

                '実行(実績作成済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI
                setSql = LMH030DAC.SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_M

                '実行(実績送信済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI
                setSql = LMH030DAC.SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_M

                '実行(実績送信済⇒送信待)SQL CONST名
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_SOUSINMACHI

                setSql = LMH030DAC.SQL_UPDATE_JISSEKIZUMI_SOUSINMACHI_EDI_M

                '実行(出荷取消⇒未登録)SQL CONST名
            Case LMH030DAC.EventShubetsu.TOUROKUZUMI_MITOUROKU

                '共通化暫定対応 START
                If autoMatomeF.Equals("9") = True Then
                    'まとめデータでない場合
                    setSql = LMH030DAC.SQL_UPDATE_TOUROKUMI_EDI_M

                Else
                    'まとめデータの場合
                    setSql = LMH030DAC.SQL_UPDATE_TOUROKUMI_EDI_M_MATOME
                End If
                '共通化暫定対応 END

                '2012.04.04 大阪対応追加START
                '実行(運送取消⇒未登録)SQL CONST名
            Case LMH030DAC.EventShubetsu.UNSOTORIKESI_MITOUROKU
                setSql = LMH030DAC.SQL_UPDATE_UNSO_TOUROKUMI_EDI_M

                '出荷管理番号に営業所イニシャル設定
                setSql = Me.SetBrInitial(setSql, ediMTbl.Rows(0))

                '2012.04.04 大阪対応追加END

            Case Else

        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql _
                                                                       , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))
        Dim max As Integer = ediMTbl.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = ediMTbl.Rows(i)

            'パラメータ設定
            Call Me.SetSysdataParameter(Me._SqlPrmList)
            Call Me.SetJissekiParameterEdiLM(ediMTbl.Rows(i), dtEventShubetsu)
            Call Me.SetOutkaEdiMComParameter(Me._Row, Me._SqlPrmList)

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))


            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC", "UpdateOutkaEdiMData", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

            '処理回数の判定
            If loopflg = 0 Then
                Exit For
            End If

        Next

        Return ds

    End Function

#End Region

#Region "H_OUTKAEDI_HED"

    ''' <summary>
    ''' EDI受信(HED)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI受信(HED)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateEdiRcvHedData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim ediRcvHedRow As DataRow = ds.Tables("LMH030_EDI_RCV_HED").Rows(0)
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim dtEdiL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Dim setSql As String = String.Empty

        'INTableの条件rowの格納
        Me._Row = ds.Tables("LMH030INOUT").Rows(0)
        '共通化暫定対応 START
        Dim autoMatomeF As String = Me._Row("AUTO_MATOME_FLG").ToString()
        Dim inOutFlg As String = Me._Row.Item("EDI_CUST_INOUTFLG").ToString()
        '共通化暫定対応 END

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通化暫定対応 START
        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '実績取消SQL CONST名
            Case LMH030DAC.EventShubetsu.TORIKESIJISSEKI
                setSql = LMH030DAC.SQL_UPDATE_JISSEKICANSEL_EDI_RCV_HED
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                'EDI取消、EDI取消⇒未登録
            Case LMH030DAC.EventShubetsu.EDITORIKESI _
                , LMH030DAC.EventShubetsu.TORIKESI_MITOUROKU

                '2013.10.10 修正START
                Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

                    Case EdiCustIndex.DicOsk00010_00 _
                       , EdiCustIndex.DicItk00899_00 _
                       , EdiCustIndex.DicItk10001_00 _
                       , EdiCustIndex.DicItk10002_00 _
                       , EdiCustIndex.DicGnm00072_00 _
                       , EdiCustIndex.DicGnm00076_00 _
                       , EdiCustIndex.DicItk10007_00 _
                       , EdiCustIndex.DicItk10008_00 _
                       , EdiCustIndex.DicChb00010_00

                        If ds.Tables("LMH030INOUT").Rows(0)("HITACHI_SAP_FLG").ToString().Equals("1") = True Then
                            setSql = LMH030DAC.SQL_UPDATE_EDITORIKESI_RCV_HED_DICNEW
                            setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)

                        Else
                            setSql = LMH030DAC.SQL_UPDATE_EDITORIKESI_RCV_HED
                            If inOutFlg.Equals("1") = True Then
                                setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                            End If
                        End If

                    Case Else

                        setSql = LMH030DAC.SQL_UPDATE_EDITORIKESI_RCV_HED
                        If inOutFlg.Equals("1") = True Then
                            setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                        End If

                End Select
                '2013.10.10 修正END

                '実行(実績作成済⇒実績未,実績送信済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI _
                , LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI
                setSql = LMH030DAC.SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_RCV_HED
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                '実行(実績送信済⇒送信待)
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_SOUSINMACHI

                setSql = LMH030DAC.SQL_UPD_SOUSINZUMI_SOUSINMI_RCV_HED
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

            Case LMH030DAC.EventShubetsu.TOUROKUZUMI_MITOUROKU

                setSql = LMH030DAC.SQL_UPD_TOUROKUMI_RCV_HED
                '共通化暫定対応 START
                If autoMatomeF.Equals("9") = True Then
                    'まとめデータでない場合
                    setSql = String.Concat(setSql, LMH030DAC.SQL_WHERE_TOUROKUMI_NORMAL)

                Else
                    'まとめデータの場合
                    setSql = String.Concat(setSql, LMH030DAC.SQL_WHERE_TOUROKUMI_MATOME)
                End If
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If
                '共通化暫定対応 END

                '2012.04.04 大阪対応追加START
                '実行(運送取消⇒未登録)SQL CONST名
            Case LMH030DAC.EventShubetsu.UNSOTORIKESI_MITOUROKU
                setSql = LMH030DAC.SQL_UPD_UNSO_TOUROKUMI_RCV_HED

                '2012.04.04 大阪対応追加END

            Case Else

        End Select
        '共通化暫定対応 END

        '受信HEDテーブル名設定
        setSql = Me.SetRcvTableNm(setSql)

        '出荷管理番号に営業所イニシャル設定
        setSql = Me.SetBrInitial(setSql, dtEdiL.Rows(0))

        'SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(setSql, SQL_COM_UPDATE_CONDITION) _
        ', ediRcvHedRow.Item("NRS_BR_CD").ToString()))

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, ediRcvHedRow.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetOutkaEdiRcvHedComParameter(ediRcvHedRow, Me._SqlPrmList, dtEventShubetsu)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(ediRcvHedRow, Me._SqlPrmList)
        Call Me.SetUpdPrmDelDateRcv(dtEventShubetsu)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "UpdateEdiRcvHedData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "H_OUTKAEDI_DTL"

    ''' <summary>
    ''' EDI受信(DTL)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI受信(DTL)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateEdiRcvDtlData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim ediRcvDtlTbl As DataTable = ds.Tables("LMH030_EDI_RCV_DTL")
        Dim ediRcvDtlRow As DataRow = Nothing
        Dim dtEdiL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        '2012.02.25 大阪対応 START
        Me._Row = ds.Tables("LMH030INOUT").Rows(0)
        Dim inOutFlg As String = Me._Row.Item("EDI_CUST_INOUTFLG").ToString()
        Dim autoMatomeF As String = Me._Row.Item("AUTO_MATOME_FLG").ToString()
        Dim rcvHedNm As String = Me._Row.Item("RCV_NM_HED").ToString().Trim()
        '2012.02.25 大阪対応 END
        Dim rcvDtlNm As String = Me._Row.Item("RCV_NM_DTL").ToString().Trim()

        Dim setSql As String = String.Empty
        Dim loopflg As Integer = 0
        Dim rtn As Integer = 0

        '共通化暫定対応 START
        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '実績取消SQL CONST名
            Case LMH030DAC.EventShubetsu.TORIKESIJISSEKI
                setSql = LMH030DAC.SQL_UPDATE_JISSEKICANSEL_EDI_RCV_DTL
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                'EDI取消、EDI取消⇒未登録、報告用EDI取消
            Case LMH030DAC.EventShubetsu.EDITORIKESI _
                , LMH030DAC.EventShubetsu.TORIKESI_MITOUROKU

                '2013.10.10 修正START
                Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))

                    Case EdiCustIndex.DicOsk00010_00 _
                        , EdiCustIndex.DicItk00899_00 _
                        , EdiCustIndex.DicItk10001_00 _
                        , EdiCustIndex.DicItk10002_00 _
                        , EdiCustIndex.DicGnm00072_00 _
                        , EdiCustIndex.DicGnm00076_00 _
                        , EdiCustIndex.DicItk10007_00 _
                        , EdiCustIndex.DicItk10008_00 _
                        , EdiCustIndex.DicChb00010_00

                        If ds.Tables("LMH030INOUT").Rows(0)("HITACHI_SAP_FLG").ToString().Equals("1") = True Then
                            setSql = LMH030DAC.SQL_UPDATE_EDITORIKESI_RCV_DTL_DICNEW
                            setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                        Else
                            setSql = LMH030DAC.SQL_UPDATE_EDITORIKESI_RCV_DTL
                            If inOutFlg.Equals("1") = True Then
                                setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                            End If
                        End If

                    Case Else

                        setSql = LMH030DAC.SQL_UPDATE_EDITORIKESI_RCV_DTL
                        If inOutFlg.Equals("1") = True Then
                            setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                        End If

                End Select
                '2013.10.10 修正END

                'setSql = LMH030DAC.SQL_UPDATE_EDITORIKESI_RCV_DTL
                'If inOutFlg.Equals("1") = True Then
                '    setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                'End If

                '実行(実績作成済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI

                '2012/06/06 本明 住化カラー対応  Start
                Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))
                    Case EdiCustIndex.Smk00952_00    '住化カラーの場合
                        setSql = LMH030DAC.SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_RCV_DTL_SMK
                    Case Else
                        setSql = LMH030DAC.SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_RCV_DTL
                End Select
                '2012/06/06 本明 住化カラー対応  End

                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                '実行(実績送信済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI

                '2012/06/06 本明 住化カラー対応  Start
                Select Case Convert.ToInt32(ds.Tables("LMH030INOUT").Rows(0)("EDI_CUST_INDEX"))
                    Case EdiCustIndex.Smk00952_00    '住化カラーの場合
                        setSql = LMH030DAC.SQL_UPDATE_JISSEKISOUSINZUMI_JISSEKIMI_EDI_RCV_DTL_SMK
                    Case Else
                        setSql = LMH030DAC.SQL_UPDATE_JISSEKISOUSINZUMI_JISSEKIMI_EDI_RCV_DTL
                End Select
                '2012/06/06 本明 住化カラー対応  End

                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                '実行(実績送信済⇒送信待)
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_SOUSINMACHI

                setSql = LMH030DAC.SQL_UPD_SOUSINZUMI_SOUSINMI_RCV_DTL
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                '実行(出荷取消⇒未登録)
            Case LMH030DAC.EventShubetsu.TOUROKUZUMI_MITOUROKU

                '2012.02.25 大阪対応 START
                If String.IsNullOrEmpty(rcvHedNm) = True AndAlso rcvDtlNm <> "H_OUTKAEDI_DTL_TSMC" Then
                    setSql = LMH030DAC.SQL_UPD_TOUROKUMI_RCV_DTL1
                    setSql = String.Concat(setSql, LMH030DAC.SQL_UPD_TOUROKUMI_RCV_DTL2)
                    setSql = String.Concat(setSql, LMH030DAC.SQL_UPD_TOUROKUMI_RCV_DTL3)
                Else
                    setSql = LMH030DAC.SQL_UPD_TOUROKUMI_RCV_DTL1
                    setSql = String.Concat(setSql, LMH030DAC.SQL_UPD_TOUROKUMI_RCV_DTL3)
                End If
                '2012.02.25 大阪対応 END

                '共通化暫定対応 START
                If autoMatomeF.Equals("9") = True Then
                    'まとめデータでない場合
                    setSql = String.Concat(setSql, LMH030DAC.SQL_WHERE_TOUROKUMI_NORMAL)

                Else
                    'まとめデータの場合
                    setSql = String.Concat(setSql, LMH030DAC.SQL_WHERE_TOUROKUMI_MATOME)
                End If
                '共通化暫定対応 END
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                '2012.04.04 大阪対応追加START
                '実行(運送取消⇒未登録)SQL CONST名
            Case LMH030DAC.EventShubetsu.UNSOTORIKESI_MITOUROKU
                setSql = LMH030DAC.SQL_UPD_UNSO_TOUROKUMI_RCV_DTL

                '2012.04.04 大阪対応追加END

            Case Else

        End Select
        '共通化暫定対応 END

        '受信DTLテーブル名設定
        setSql = Me.SetRcvDtlTableNm(setSql)

        '出荷管理番号に営業所イニシャル設定
        setSql = Me.SetBrInitial(setSql, dtEdiL.Rows(0))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql _
                                                        , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))
        Dim max As Integer = ediRcvDtlTbl.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            ediRcvDtlRow = ediRcvDtlTbl.Rows(i)

            'パラメータ設定
            Call Me.SetSysdataParameter(Me._SqlPrmList)
            Call Me.SetOutkaEdiRcvDtlComParameter(ediRcvDtlRow, Me._SqlPrmList)
            '2012.02.25 大阪対応 START
            Call Me.SetUpdPrmDelDateRcv(dtEventShubetsu)
            'Call Me.SetUpdPrmDelDateRcv(dtEventShubetsu, rcvHedNm)
            '2012.02.25 大阪対応 END
            Call Me.SetJissekiParameterRcv(ediRcvDtlRow, dtEventShubetsu)
            Call Me.SetUpdPrmSndDateRcvDtl(dtEventShubetsu)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC", "UpdateEdiRcvDtlData", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

            '処理回数の判定
            If loopflg = 0 Then
                Exit For
            End If

        Next

        Return ds

    End Function

#End Region

    '2012.03.18 大阪対応START
#Region "H_OUTKAEDI_EXT"

    ''' <summary>
    ''' EDI受信(EXT)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI受信(EXT)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateEdiRcvExtData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim ediRcvDtlTbl As DataTable = ds.Tables("LMH030_EDI_RCV_DTL")
        Dim ediRcvDtlRow As DataRow = Nothing
        Dim dtEdiL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        '2012.02.25 大阪対応 START
        Me._Row = ds.Tables("LMH030INOUT").Rows(0)
        Dim inOutFlg As String = Me._Row.Item("EDI_CUST_INOUTFLG").ToString()
        Dim autoMatomeF As String = Me._Row.Item("AUTO_MATOME_FLG").ToString()
        Dim rcvHedNm As String = Me._Row.Item("RCV_NM_HED").ToString().Trim()
        '2012.02.25 大阪対応 END

        Dim setSql As String = String.Empty
        Dim loopflg As Integer = 0
        Dim rtn As Integer = 0

        '共通化暫定対応 START
        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '実績取消SQL CONST名
            Case LMH030DAC.EventShubetsu.TORIKESIJISSEKI
                setSql = LMH030DAC.SQL_UPDATE_JISSEKICANSEL_EDI_RCV_DTL
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                'EDI取消、EDI取消⇒未登録、報告用EDI取消
            Case LMH030DAC.EventShubetsu.EDITORIKESI _
                , LMH030DAC.EventShubetsu.TORIKESI_MITOUROKU

                setSql = LMH030DAC.SQL_UPDATE_EDITORIKESI_RCV_EXT
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                '実行(実績作成済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI
                setSql = LMH030DAC.SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_EDI_RCV_DTL
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                '実行(実績作成済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI
                setSql = LMH030DAC.SQL_UPDATE_JISSEKISOUSINZUMI_JISSEKIMI_EDI_RCV_DTL
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                '実行(実績送信済⇒送信待)
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_SOUSINMACHI

                setSql = LMH030DAC.SQL_UPD_SOUSINZUMI_SOUSINMI_RCV_DTL
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                '実行(出荷取消⇒未登録)
            Case LMH030DAC.EventShubetsu.TOUROKUZUMI_MITOUROKU

                setSql = LMH030DAC.SQL_UPD_TOUROKUMI_RCV_DTL1
                setSql = String.Concat(setSql, LMH030DAC.SQL_UPD_TOUROKUMI_RCV_DTL3)

                '共通化暫定対応 START
                If autoMatomeF.Equals("9") = True Then
                    'まとめデータでない場合
                    setSql = String.Concat(setSql, LMH030DAC.SQL_WHERE_TOUROKUMI_NORMAL)

                Else
                    'まとめデータの場合
                    setSql = String.Concat(setSql, LMH030DAC.SQL_WHERE_TOUROKUMI_MATOME)
                End If
                '共通化暫定対応 END
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH030DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

            Case Else

        End Select
        '共通化暫定対応 END

        '受信DTLテーブル名設定
        setSql = Me.SetRcvExtTableNm(setSql)

        '出荷管理番号に営業所イニシャル設定
        setSql = Me.SetBrInitial(setSql, dtEdiL.Rows(0))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql _
                                                        , ds.Tables("LMH030_OUTKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString()))
        Dim max As Integer = ediRcvDtlTbl.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            ediRcvDtlRow = ediRcvDtlTbl.Rows(i)

            'パラメータ設定
            Call Me.SetSysdataParameter(Me._SqlPrmList)
            Call Me.SetOutkaEdiRcvDtlComParameter(ediRcvDtlRow, Me._SqlPrmList)
            '2012.02.25 大阪対応 START
            Call Me.SetUpdPrmDelDateRcv(dtEventShubetsu)
            'Call Me.SetUpdPrmDelDateRcv(dtEventShubetsu, rcvHedNm)
            '2012.02.25 大阪対応 END
            Call Me.SetJissekiParameterRcv(ediRcvDtlRow, dtEventShubetsu)
            Call Me.SetUpdPrmSndDateRcvDtl(dtEventShubetsu)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH030DAC", "UpdateEdiRcvDtlData", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

            '処理回数の判定
            If loopflg = 0 Then
                Exit For
            End If

        Next

        Return ds

    End Function

#End Region
    '2012.03.18 大阪対応END

#Region "C_OUTKA_L"

    ''' <summary>
    ''' 出荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateOutkaLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_C_OUTKA_L")
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '実行(実績作成済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI
                setSql = LMH030DAC.SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_OUTKA_L

                '実行(実績送信済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI
                setSql = LMH030DAC.SQL_UPDATE_JISSEKIZUMI_JISSEKIMI_OUTKA_L

            Case Else

        End Select

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(setSql, SQL_COM_UPDATE_CONDITION) _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetOutkaLComParameter(Me._Row, Me._SqlPrmList, dtEventShubetsu)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(inTbl.Rows(0), Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "UpdateOutkaLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "H_SENDOUTEDI"

    ''' <summary>
    ''' EDI送信テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI送信テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateEdiSendLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim sndRow As DataRow = ds.Tables("LMH030_EDI_SND").Rows(0)
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = dtIn.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '実行(実績送信済⇒送信待)SQL CONST名
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_SOUSINMACHI
                setSql = LMH030DAC.SQL_UPD_JISSEKIMODOSI_SEND

            Case Else

        End Select

        '送信テーブル名設定
        setSql = Me.SetSendTableNm(setSql)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(setSql, SQL_COM_UPDATE_CONDITION) _
                                                                       , sndRow.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetEdiSendComParameter(sndRow, Me._SqlPrmList, dtEventShubetsu)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetSysDateTime(sndRow, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "UpdateEdiSendLData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)


        Return ds

    End Function

#End Region

    '印刷フラグ更新対応 20120313 Start
#Region "PRINT_FLAG"

    ''' <summary>
    ''' 印刷フラグ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>印刷フラグの更新</remarks>
    Private Function UpdatePrintFlag(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dtIn As DataTable = ds.Tables("LMH030_OUTPUTIN")
        Dim setSql As String = String.Empty
        Dim RcvNm As String = String.Empty

        'INTableの条件rowの格納
        Me._Row = dtIn.Rows(0)

        '2012.03.18 大阪対応START
        '受信テーブル名設定
        If String.IsNullOrEmpty(Me._Row.Item("RCV_NM_HED").ToString()) = False Then
            RcvNm = Me._Row.Item("RCV_NM_HED").ToString()
        End If

        Dim ediCustIdx As String = Me._Row.Item("EDI_CUST_INDEX").ToString()

        '実行(印刷フラグ更新)SQL CONST名
        If String.IsNullOrEmpty(RcvNm) = False Then

            'ゴードーの場合、受信TBLにCUST_CDを持っていないので条件より外す(DTLのSQLを使用)
            If ediCustIdx.Equals("4") = True Then
                setSql = LMH030DAC.SQL_UPD_PRTFLG_DTL
                '2012/11/15日産物流追加(ゴードーと同条件)
            ElseIf ediCustIdx.Equals("32") = True Then
                setSql = LMH030DAC.SQL_UPD_PRTFLG_DTL
            Else
                setSql = LMH030DAC.SQL_UPD_PRTFLG_HED
            End If

            'setSql = LMH030DAC.SQL_UPD_PRTFLG_HED
        Else
            setSql = LMH030DAC.SQL_UPD_PRTFLG_DTL
        End If
        '2012.03.18 大阪対応END

        'CRT_DATE_FROMが設定されていた場合、条件に追加
        If String.IsNullOrEmpty(Me._Row.Item("CRT_DATE_FROM").ToString()) = False Then
            setSql = String.Concat(setSql, LMH030DAC.SQL_FROM_PRTFLG_CRT_DATE_FROM)
        End If

        'CRT_DATE_TOが設定されていた場合、条件に追加
        If String.IsNullOrEmpty(Me._Row.Item("CRT_DATE_TO").ToString()) = False Then
            setSql = String.Concat(setSql, LMH030DAC.SQL_FROM_PRTFLG_CRT_DATE_TO)
        End If

        '入出荷両用の受信テーブルの場合、INOUT_KBを条件に追加
        If Me._Row.Item("INOUT_UMU_KB").ToString() = "1" Then
            setSql = String.Concat(setSql, LMH030DAC.SQL_FROM_PRTFLG_INOUT)
        End If

        '2012.03.18 大阪対応START
        '各EDI荷主の特殊条件が存在する場合、BIKO_STR_1より条件に追加
        If String.IsNullOrEmpty(Me._Row.Item("BIKO_STR_1").ToString()) = False Then
            setSql = String.Concat(setSql, Space(2), Me._Row.Item("BIKO_STR_1").ToString())
        End If
        '2012.03.18 大阪対応END

        Me._SqlPrmList = New ArrayList()

        '受信テーブル名設定
        If String.IsNullOrEmpty(Me._Row.Item("RCV_NM_HED").ToString()) = False Then
            RcvNm = Me._Row.Item("RCV_NM_HED").ToString()
        Else
            '■■キャッシュ更新までの暫定対応　Start■■
            'RcvNm = Me._Row.Item("RCV_NM_DTL").ToString()
            RcvNm = GetRcvNmDtl(ds)
            ds.Tables("LMH030_OUTPUTIN").Rows(0).Item("RCV_NM_DTL") = RcvNm
            '■■キャッシュ更新までの暫定対応　End■■
        End If
        setSql = setSql.Replace("$RCV_NM$", RcvNm)

        'PrintFlagの更新
        dtIn.Rows(0).Item("PRTFLG") = "1"

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(setSql), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.PrintFlagParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        'Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "LMH030_OUTPUTIN", cmd)

        '要望番号1062 2012.05.15 追加・修正START
        ''SQLの発行
        'Me.UpdateResultChk(cmd)

        Dim updateCnt As Integer = 0

        'SQLの発行
        updateCnt = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(updateCnt)

        '要望番号1062 2012.05.15 追加・修正START

        Return ds

    End Function

#End Region
    '印刷フラグ更新対応 20120313 End

    '要望番号1007 2012.05.08 修正START
#Region "PRINT_FLAG(DELETE)"

    ''' <summary>
    ''' EDI印刷対象テーブル削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI印刷対象テーブル削除</remarks>
    Private Function DeleteHEdiPrint(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dtIn As DataTable = ds.Tables("H_EDI_PRINT")
        Dim setSql As String = String.Empty
        Dim max As Integer = dtIn.Rows.Count - 1

        'For i As Integer = 0 To max

        'INTableの条件rowの格納
        'Me._Row = dtIn.Rows(i)
        Me._Row = dtIn.Rows(0)

        'SQL CONST名
        setSql = LMH030DAC.SQL_DEL_EDI_PRINT
        '2012.05.29 要望番号1077 追加START
        If String.IsNullOrEmpty(Me._Row.Item("DENPYO_NO").ToString()) = True Then
            Me._Row.Item("DENPYO_NO") = "999999999999999999999999999999"
        Else
            setSql = String.Concat(setSql, LMH030DAC.SQL_WHERE_DENPYO_NO)
        End If
        '2012.05.29 要望番号1077 追加END

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(setSql), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.HEdiPrintParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "DeleteHEdiPrint", cmd)

        ''SQLの発行
        'Me.UpdateResultChk(cmd)

        Dim updateCnt As Integer = 0

        'SQLの発行
        updateCnt = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(updateCnt)

        'Next

        Return ds

    End Function

#End Region
    '要望番号1007 2012.05.08 修正END

    '要望番号1007 2012.05.08 修正START
#Region "PRINT_FLAG(INSERT)"

    ''' <summary>
    ''' EDI印刷対象テーブル新規追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI印刷対象テーブル新規追加</remarks>
    Private Function InsertHEdiPrint(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dtIn As DataTable = ds.Tables("H_EDI_PRINT")
        Dim setSql As String = String.Empty
        Dim max As Integer = dtIn.Rows.Count - 1

        'For i As Integer = 0 To max

        'INTableの条件rowの格納
        'Me._Row = dtIn.Rows(i)
        Me._Row = dtIn.Rows(0)

        'SQL CONST名
        setSql = LMH030DAC.SQL_INS_EDI_PRINT

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(setSql), Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.HEdiPrintParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetDataInsertParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "InsertHEdiPrint", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        'Next

        Return ds

    End Function

#End Region
    '要望番号1007 2012.05.08 修正END

#End Region

#Region "Delete"

#Region "H_SENDOUTEDI"

    ''' <summary>
    ''' EDI送信テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI送信テーブル削除SQLの構築・発行</remarks>
    Private Function DeleteEdiSendLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim sndRow As DataRow = ds.Tables("LMH030_EDI_SND").Rows(0)
        Dim dtIn As DataTable = ds.Tables("LMH030INOUT")
        Dim dtEventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = dtIn.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'イベント種別の判断
        Select Case DirectCast(dtEventShubetsu, LMH030DAC.EventShubetsu)

            '実行(実績作成済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI
                setSql = LMH030DAC.SQL_DELETE_JISSEKIZUMI_JISSEKIMI_EDI_SEND

                '実行(実績送信済⇒実績未)SQL CONST名
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI
                setSql = LMH030DAC.SQL_DELETE_JISSEKIZUMI_JISSEKIMI_EDI_SEND

        End Select

        '送信テーブル名設定
        setSql = Me.SetSendTableNm(setSql)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(setSql, SQL_COM_UPDATE_CONDITION) _
                                                                       , sndRow.Item("NRS_BR_CD").ToString()))
        'パラメータ設定
        Call Me.SetEdiSendComParameter(sndRow, Me._SqlPrmList, dtEventShubetsu)

        Call Me.SetSysDateTime(sndRow, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "DeleteEdiSendData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#End Region

#Region "SQL"

#Region "一括変更SQL構築"

    ''' <summary>
    ''' 一括変更SQL構築
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetsqlEdit(ByVal dtVal As DataTable)

        Dim colNm1 As String = dtVal.Rows(0).Item("EDIT_ITEM_NM1").ToString
        Dim colNm2 As String = dtVal.Rows(0).Item("EDIT_ITEM_NM2").ToString
        'SQL構築
        Me._StrSql.Append("UPDATE $LM_TRN$..H_OUTKAEDI_L SET")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(String.Concat(colNm1, " = @EDIT_ITEM_VALUE1"))
        Me._StrSql.Append(vbNewLine)
        If colNm2 = String.Empty Then

        Else
            Me._StrSql.Append(String.Concat(",", colNm2, " = @EDIT_ITEM_VALUE2"))
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(",UNSO_NM = @EDIT_ITEM_VALUE3")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(",UNSO_BR_NM = @EDIT_ITEM_VALUE4")
            Me._StrSql.Append(vbNewLine)
        End If
        Me._StrSql.Append(",EDIT_FLAG = '01'")
        Me._StrSql.Append(vbNewLine)

    End Sub

    ''' <summary>
    ''' 一括変更SQL構築 届先Mより設定 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetsqlEditMDSET(ByVal dtVal As DataTable)

        Me._StrSql.Append(",EDI_DEST_CD = ''")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(",DEST_NM = @DEST_NM")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(",DEST_ZIP = @ZIP")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(",DEST_AD_1 = @AD_1")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(",DEST_AD_2 = @AD_2")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(",DEST_AD_3 = @AD_3")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(",DEST_TEL = @TEL")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(",DEST_FAX = @FAX")
        Me._StrSql.Append(vbNewLine)
        'Me._StrSql.Append(",DEST_MAIL = @DEST_MAIL")
        'Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(",DEST_JIS_CD = @JIS")
        Me._StrSql.Append(vbNewLine)


    End Sub

#End Region

#Region "運送会社マスタ抽出パラメータ設定"

    ''' <summary>
    '''  運送会社マスタパラメータ設定
    ''' </summary>
    ''' <remarks>運送会社マスタ存在チェック用SQLの構築</remarks>
    Private Sub setSQLSelectExists(ByVal dtVal As DataTable, ByVal dtKey As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dtKey.Rows(0)("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", dtVal.Rows(0)("EDIT_ITEM_VALUE1"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", dtVal.Rows(0)("EDIT_ITEM_VALUE2"), DBDataType.NVARCHAR))

    End Sub

#End Region

#Region "届先マスタ抽出パラメータ設定"
    ''' <summary>
    ''' 届先マスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMdestParameter(ByVal dt As DataTable, ByVal prmUpdFlg As Integer)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))
        '★★★2011.10.17 届先自動追加　修正START
        If prmUpdFlg = 1 Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", dt.Rows(0).Item("DEST_CD"), DBDataType.NVARCHAR))
            'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DEST_CD", dt.Rows(0).Item("EDI_DEST_CD"), DBDataType.NVARCHAR))
        Else
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", dt.Rows(0).Item("SHIP_CD_L"), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DEST_CD", dt.Rows(0).Item("SHIP_CD_L"), DBDataType.NVARCHAR))
        End If
        '★★★2011.10.17 修正END

    End Sub


#End Region

#Region "EDI出荷(大,中),EDI受信TBL抽出パラメータ設定"

    ''' <summary>
    '''  パラメータ設定（EDI出荷(大・中),EDI受信テーブル・存在チェック）
    ''' </summary>
    ''' <remarks>出荷登録時出荷マスタ検索用SQLの構築</remarks>
    Private Sub setSQLSelectDataExists()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定(共通）
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me._Row("EDI_CTL_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me._Row("OUTKA_CTL_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me._Row("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me._Row("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#Region "共通パラメータ設定"

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal prmList As ArrayList)

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetSysdataParameter(prmList)

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList)

        'システム項目
        Call Me.SetSysdataTimeParameter(prmList)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList)

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 抽出条件(日時:EDI(大))
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysDateTime(ByVal dr As DataRow, ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", dr.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", dr.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#Region "EDI出荷(大)更新パラメータ設定"

    ''' <summary>
    ''' EDI出荷(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaEdiLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal eventShubetsu As Integer)

        With conditionRow

            '★★★2011.08.30 修正START
            'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
            '★★★2011.08.30 修正START

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me.NullConvertString(.Item("OUTKA_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_KB", Me.NullConvertString(.Item("OUTKA_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", Me.NullConvertString(.Item("SYUBETU_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NAIGAI_KB", Me.NullConvertString(.Item("NAIGAI_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_STATE_KB", Me.NullConvertString(.Item("OUTKA_STATE_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKAHOKOKU_YN", Me.NullConvertString(.Item("OUTKAHOKOKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PICK_KB", Me.NullConvertString(.Item("PICK_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_NM", Me.NullConvertString(.Item("NRS_BR_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", Me.NullConvertString(.Item("WH_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_NM", Me.NullConvertString(.Item("WH_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", Me.NullConvertString(.Item("OUTKA_PLAN_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", Me.NullConvertString(.Item("OUTKO_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", Me.NullConvertString(.Item("ARR_PLAN_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", Me.NullConvertString(.Item("ARR_PLAN_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", Me.NullConvertString(.Item("HOKOKU_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", Me.NullConvertString(.Item("TOUKI_HOKAN_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(.Item("CUST_CD_M")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", Me.NullConvertString(.Item("CUST_NM_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_NM_M", Me.NullConvertString(.Item("CUST_NM_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", Me.NullConvertString(.Item("SHIP_CD_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", Me.NullConvertString(.Item("SHIP_CD_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_NM_L", Me.NullConvertString(.Item("SHIP_NM_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_NM_M", Me.NullConvertString(.Item("SHIP_NM_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_DEST_CD", Me.NullConvertString(.Item("EDI_DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.NullConvertString(.Item("DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_ZIP", Me.NullConvertString(.Item("DEST_ZIP")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", Me.NullConvertString(.Item("DEST_AD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", Me.NullConvertString(.Item("DEST_AD_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", Me.NullConvertString(.Item("DEST_AD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_4", Me.NullConvertString(.Item("DEST_AD_4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_5", Me.NullConvertString(.Item("DEST_AD_5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_TEL", Me.NullConvertString(.Item("DEST_TEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_FAX", Me.NullConvertString(.Item("DEST_FAX")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_MAIL", Me.NullConvertString(.Item("DEST_MAIL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", Me.NullConvertString(.Item("DEST_JIS_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", Me.NullConvertString(.Item("SP_NHS_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", Me.NullConvertString(.Item("COA_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", Me.NullConvertString(.Item("CUST_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", Me.NullConvertString(.Item("BUYER_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_MOTO_KB", Me.NullConvertString(.Item("UNSO_MOTO_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", Me.NullConvertString(.Item("UNSO_TEHAI_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYARYO_KB", Me.NullConvertString(.Item("SYARYO_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", Me.NullConvertString(.Item("BIN_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", Me.NullConvertString(.Item("UNSO_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NM", Me.NullConvertString(.Item("UNSO_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", Me.NullConvertString(.Item("UNSO_BR_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_NM", Me.NullConvertString(.Item("UNSO_BR_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", Me.NullConvertString(.Item("UNCHIN_TARIFF_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_CD", Me.NullConvertString(.Item("EXTC_TARIFF_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(.Item("REMARK")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ATT", Me.NullConvertString(.Item("UNSO_ATT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_YN", Me.NullConvertString(.Item("DENP_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PC_KB", Me.NullConvertString(.Item("PC_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_YN", Me.NullConvertString(.Item("UNCHIN_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", Me.NullConvertString(.Item("NIYAKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUT_FLAG", Me.NullConvertString(.Item("OUT_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", Me.NullConvertString(.Item("AKAKURO_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", Me.NullConvertString(.Item("JISSEKI_FLAG")), DBDataType.CHAR))
            'Functionへ移動の為、コメント
            'prmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", Me.NullConvertString(.Item("JISSEKI_USER")), DBDataType.NVARCHAR))
            'prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", Me.NullConvertString(.Item("JISSEKI_DATE")), DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", Me.NullConvertString(.Item("JISSEKI_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N01", Me.NullConvertZero(.Item("FREE_N01")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N02", Me.NullConvertZero(.Item("FREE_N02")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N03", Me.NullConvertZero(.Item("FREE_N03")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N04", Me.NullConvertZero(.Item("FREE_N04")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N05", Me.NullConvertZero(.Item("FREE_N05")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N06", Me.NullConvertZero(.Item("FREE_N06")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N07", Me.NullConvertZero(.Item("FREE_N07")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N08", Me.NullConvertZero(.Item("FREE_N08")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N09", Me.NullConvertZero(.Item("FREE_N09")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N10", Me.NullConvertZero(.Item("FREE_N10")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C01", Me.NullConvertString(.Item("FREE_C01")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C02", Me.NullConvertString(.Item("FREE_C02")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C03", Me.NullConvertString(.Item("FREE_C03")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C04", Me.NullConvertString(.Item("FREE_C04")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C05", Me.NullConvertString(.Item("FREE_C05")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C06", Me.NullConvertString(.Item("FREE_C06")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C07", Me.NullConvertString(.Item("FREE_C07")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C08", Me.NullConvertString(.Item("FREE_C08")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C09", Me.NullConvertString(.Item("FREE_C09")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C10", Me.NullConvertString(.Item("FREE_C10")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C11", Me.NullConvertString(.Item("FREE_C11")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C12", Me.NullConvertString(.Item("FREE_C12")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C13", Me.NullConvertString(.Item("FREE_C13")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C14", Me.NullConvertString(.Item("FREE_C14")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C15", Me.NullConvertString(.Item("FREE_C15")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C16", Me.NullConvertString(.Item("FREE_C16")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C17", Me.NullConvertString(.Item("FREE_C17")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C18", Me.NullConvertString(.Item("FREE_C18")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C19", Me.NullConvertString(.Item("FREE_C19")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C20", Me.NullConvertString(.Item("FREE_C20")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C21", Me.NullConvertString(.Item("FREE_C21")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C22", Me.NullConvertString(.Item("FREE_C22")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C23", Me.NullConvertString(.Item("FREE_C23")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C24", Me.NullConvertString(.Item("FREE_C24")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C25", Me.NullConvertString(.Item("FREE_C25")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C26", Me.NullConvertString(.Item("FREE_C26")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C27", Me.NullConvertString(.Item("FREE_C27")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C28", Me.NullConvertString(.Item("FREE_C28")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C29", Me.NullConvertString(.Item("FREE_C29")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C30", Me.NullConvertString(.Item("FREE_C30")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_USER", Me.NullConvertString(.Item("CRT_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", Me.NullConvertString(.Item("CRT_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_TIME", Me.NullConvertString(.Item("CRT_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))
            prmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", Me.NullConvertString(.Item("SCM_CTL_NO_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDIT_FLAG", Me.NullConvertString(.Item("EDIT_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MATCHING_FLAG", Me.NullConvertString(.Item("MATCHING_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(.Item("SYS_DEL_FLG")), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "EDI出荷(大)更新パラメータ設定(実績日時用)"

    ''' <summary>
    ''' 更新時のパラメータ実績日時(EDI出荷(大,中)用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetJissekiParameterEdiLM(ByVal row As DataRow, ByVal eventShubetsu As Integer)

        '★★★2011.10.04 修正START
        'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
        '★★★2011.10.04 修正END

        Select Case DirectCast(eventShubetsu, LMH030DAC.EventShubetsu)

            Case LMH030DAC.EventShubetsu.SAVEOUTKA, LMH030DAC.EventShubetsu.HIMODUKE

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", Me.NullConvertString(row.Item("JISSEKI_USER")), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", Me.NullConvertString(row.Item("JISSEKI_DATE")), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", Me.NullConvertString(row.Item("JISSEKI_TIME")), DBDataType.CHAR))

            Case LMH030DAC.EventShubetsu.CREATEJISSEKI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", updTime))

            Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI _
                 , LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", String.Empty, DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", String.Empty, DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", String.Empty, DBDataType.CHAR))

            Case Else

        End Select

    End Sub

#End Region

#Region "EDI受信(HED,DTL)更新パラメータ設定(実績日時用)"

    ''' <summary>
    ''' 更新時のパラメータ実績日時(EDI受信(HED,DTL)用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetJissekiParameterRcv(ByVal row As DataRow, ByVal eventShubetsu As Integer)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        Select Case DirectCast(eventShubetsu, LMH030DAC.EventShubetsu)

            Case LMH030DAC.EventShubetsu.CREATEJISSEKI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", updTime))

            Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI _
                 , LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", String.Empty, DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", String.Empty, DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", String.Empty, DBDataType.CHAR))

            Case Else

        End Select

    End Sub

#End Region

#Region "EDI出荷(中)更新パラメータ設定"

    ''' <summary>
    ''' EDI出荷(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaEdiMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            '★★★2011.10.04 修正START
            'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
            '★★★2011.10.04 修正END

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("DEL_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", .Item("EDI_CTL_NO_CHU").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", .Item("OUTKA_CTL_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO_CHU", .Item("OUTKA_CTL_NO_CHU").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", .Item("CUST_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", .Item("BUYER_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", .Item("CUST_GOODS_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", .Item("NRS_GOODS_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_KB", .Item("ALCTD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.FormatNumValue(.Item("OUTKA_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU", Me.FormatNumValue(.Item("OUTKA_HASU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_QT", Me.FormatNumValue(.Item("OUTKA_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", Me.FormatNumValue(.Item("OUTKA_TTL_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", Me.FormatNumValue(.Item("OUTKA_TTL_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KB_UT", .Item("KB_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@QT_UT", .Item("QT_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PKG_NB", Me.FormatNumValue(.Item("PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PKG_UT", .Item("PKG_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ONDO_KB", .Item("ONDO_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(.Item("BETU_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUT_KB", .Item("OUT_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", .Item("AKAKURO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", .Item("JISSEKI_FLAG").ToString(), DBDataType.CHAR))
            'Functionへ移動の為、コメント
            'prmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", .Item("JISSEKI_USER").ToString(), DBDataType.NVARCHAR))
            'prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", .Item("JISSEKI_DATE").ToString(), DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", .Item("JISSEKI_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SET_KB", .Item("SET_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N01", Me.FormatNumValue(.Item("FREE_N01").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N02", Me.FormatNumValue(.Item("FREE_N02").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N03", Me.FormatNumValue(.Item("FREE_N03").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N04", Me.FormatNumValue(.Item("FREE_N04").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N05", Me.FormatNumValue(.Item("FREE_N05").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N06", Me.FormatNumValue(.Item("FREE_N06").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N07", Me.FormatNumValue(.Item("FREE_N07").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N08", Me.FormatNumValue(.Item("FREE_N08").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N09", Me.FormatNumValue(.Item("FREE_N09").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_N10", Me.FormatNumValue(.Item("FREE_N10").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C01", .Item("FREE_C01").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C02", .Item("FREE_C02").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C03", .Item("FREE_C03").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C04", .Item("FREE_C04").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C05", .Item("FREE_C05").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C06", .Item("FREE_C06").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C07", .Item("FREE_C07").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C08", .Item("FREE_C08").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C09", .Item("FREE_C09").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C10", .Item("FREE_C10").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C11", .Item("FREE_C11").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C12", .Item("FREE_C12").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C13", .Item("FREE_C13").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C14", .Item("FREE_C14").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C15", .Item("FREE_C15").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C16", .Item("FREE_C16").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C17", .Item("FREE_C17").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C18", .Item("FREE_C18").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C19", .Item("FREE_C19").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C20", .Item("FREE_C20").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C21", .Item("FREE_C21").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C22", .Item("FREE_C22").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C23", .Item("FREE_C23").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C24", .Item("FREE_C24").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C25", .Item("FREE_C25").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C26", .Item("FREE_C26").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C27", .Item("FREE_C27").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C28", .Item("FREE_C28").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C29", .Item("FREE_C29").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FREE_C30", .Item("FREE_C30").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_USER", .Item("CRT_USER").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_TIME", .Item("CRT_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))
            prmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_M", .Item("SCM_CTL_NO_M").ToString(), DBDataType.CHAR))
        End With

    End Sub

#End Region

#Region "EDI受信(HED)更新パラメータ設定"

    ''' <summary>
    ''' EDI受信(HED)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaEdiRcvHedComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal eventShubetsu As Integer)

        With conditionRow

            '★★★2011.10.04 修正START
            'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
            '★★★2011.10.04 修正END

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me.NullConvertString(.Item("OUTKA_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(.Item("SYS_DEL_FLG")), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "EDI受信(DTL)更新パラメータ設定"

    ''' <summary>
    ''' EDI受信(DTL)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaEdiRcvDtlComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me.NullConvertString(.Item("OUTKA_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(.Item("SYS_DEL_FLG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", Me.NullConvertString(.Item("EDI_CTL_NO_CHU")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO_CHU", Me.NullConvertString(.Item("OUTKA_CTL_NO_CHU")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", Me.NullConvertString(.Item("JISSEKI_SHORI_FLG")), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "更新パラメータ削除日時設定(RCV_TBL)"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="eventShubetsu"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdPrmDelDateRcv(ByVal eventShubetsu As Integer)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        'SQLパラメータ設定

        Select Case DirectCast(eventShubetsu, LMH030DAC.EventShubetsu)
            'EDI取消
            Case LMH030DAC.EventShubetsu.EDITORIKESI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", updTime, DBDataType.CHAR))

                'EDI取消⇒未登録
            Case LMH030DAC.EventShubetsu.TORIKESI_MITOUROKU

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_USER", String.Empty, DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", String.Empty, DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", String.Empty, DBDataType.CHAR))

                '出荷取消⇒未登録
            Case LMH030DAC.EventShubetsu.TOUROKUZUMI_MITOUROKU

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_USER", String.Empty, DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE", String.Empty, DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TIME", String.Empty, DBDataType.CHAR))

            Case Else

        End Select
    End Sub

#End Region

#Region "更新パラメータ送信日時設定(RCV_DTL)"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="eventShubetsu"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdPrmSndDateRcvDtl(ByVal eventShubetsu As Integer)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        'SQLパラメータ設定

        Select Case DirectCast(eventShubetsu, LMH030DAC.EventShubetsu)

            '実績送信済⇒実績未
            Case LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI _
                , LMH030DAC.EventShubetsu.SOUSINZUMI_SOUSINMACHI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_USER", String.Empty, DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DATE", String.Empty, DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_TIME", String.Empty, DBDataType.CHAR))

            Case Else

        End Select
    End Sub

#End Region

#Region "EDI送信(TBL)更新パラメータ設定"

    ''' <summary>
    ''' EDI送信(TBL)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetEdiSendComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal eventShubetsu As Integer)

        With conditionRow

            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(.Item("SYS_DEL_FLG")), DBDataType.CHAR))

            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))

            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", Me.NullConvertString(.Item("JISSEKI_SHORI_FLG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_USER", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_DATE", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_TIME", String.Empty, DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "EDI日本合成化学送信(TBL)新規登録パラメータ設定"

    ''' <summary>
    ''' EDI送信日本合成化学(TBL)の新規登録パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetEdiSendCreateParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            '★★★2011.10.04 修正START
            'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())
            'Dim updTimeNormal As String = DateTime.Now.ToString("HHmmss")
            Dim updTimeNormal As String = MyBase.GetSystemTime().Substring(0, 6)
            '★★★2011.10.04 修正END

            prmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(.Item("DEL_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(.Item("EDI_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_EDA", Me.NullConvertString(.Item("EDI_CTL_NO_EDA")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", Me.NullConvertString(.Item("OUTKA_CTL_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", Me.NullConvertString(.Item("FILE_NAME")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_NO", Me.NullConvertString(.Item("REC_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@RCV_ID", Me.NullConvertString(.Item("RCV_ID")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@RCV_UKETSUKE_NO", Me.NullConvertString(.Item("RCV_UKETSUKE_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@RCV_UKETSUKE_NO_EDA", Me.NullConvertString(.Item("RCV_UKETSUKE_NO_EDA")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@RCV_INPUT_KB", Me.NullConvertString(.Item("RCV_INPUT_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@RCV_EDA_UP_FLG", Me.NullConvertString(.Item("RCV_EDA_UP_FLG")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ID", Me.NullConvertString(.Item("ID")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYSTEM_KB", Me.NullConvertString(.Item("SYSTEM_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KEIKAKU_HOKOKU_KB", Me.NullConvertString(.Item("KEIKAKU_HOKOKU_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UKETSUKE_NO", Me.NullConvertString(.Item("UKETSUKE_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UKETSUKE_NO_EDA", Me.NullConvertString(.Item("UKETSUKE_NO_EDA")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@COMPANY_CD", Me.NullConvertString(.Item("COMPANY_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BASHO_CD", Me.NullConvertString(.Item("BASHO_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_BUMON", Me.NullConvertString(.Item("OUTKA_BUMON")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_GROUP", Me.NullConvertString(.Item("OUTKA_GROUP")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INPUT_YMD", Me.NullConvertString(.Item("INPUT_YMD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_YMD", Me.NullConvertString(.Item("OUTKA_YMD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INPUT_KB", Me.NullConvertString(.Item("INPUT_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_RYAKU", Me.NullConvertString(.Item("GOODS_RYAKU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GRADE_1", Me.NullConvertString(.Item("GRADE_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GRADE_2", Me.NullConvertString(.Item("GRADE_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@YORYO", Me.NullConvertString(.Item("YORYO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NISUGATA_CD", Me.NullConvertString(.Item("NISUGATA_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@YUSHUTSU_KB", Me.NullConvertString(.Item("YUSHUTSU_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAIKO_KB", Me.NullConvertString(.Item("ZAIKO_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_BASHO_SP", Me.NullConvertString(.Item("OUTKA_BASHO_SP")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KANNOU_KB", Me.NullConvertString(.Item("KANNOU_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TSUMIKOMI_NO", Me.NullConvertString(.Item("TSUMIKOMI_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", Me.NullConvertString(.Item("BIN_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB_NM", Me.NullConvertString(.Item("BIN_KB_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_KB", Me.NullConvertString(.Item("OUTKA_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NONYU_CD", Me.NullConvertString(.Item("NONYU_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PALETTE_KB", Me.NullConvertString(.Item("PALETTE_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@IN_BIKO_ANK", Me.NullConvertString(.Item("IN_BIKO_ANK")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@IN_BIKO_BIKO", Me.NullConvertString(.Item("IN_BIKO_BIKO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO_1", Me.NullConvertString(.Item("LOT_NO_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO2_1", Me.NullConvertString(.Item("LOT_NO2_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOSU_1", Me.ConvertStringZero(.Item("KOSU_1")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SURYO_1", Me.ConvertStringZero(.Item("SURYO_1")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO_2", Me.NullConvertString(.Item("LOT_NO_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO2_2", Me.NullConvertString(.Item("LOT_NO2_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOSU_2", Me.ConvertStringZero(.Item("KOSU_2")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SURYO_2", Me.ConvertStringZero(.Item("SURYO_2")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO_3", Me.NullConvertString(.Item("LOT_NO_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO2_3", Me.NullConvertString(.Item("LOT_NO2_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOSU_3", Me.ConvertStringZero(.Item("KOSU_3")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SURYO_3", Me.ConvertStringZero(.Item("SURYO_3")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO_4", Me.NullConvertString(.Item("LOT_NO_4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO2_4", Me.NullConvertString(.Item("LOT_NO2_4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOSU_4", Me.ConvertStringZero(.Item("KOSU_4")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SURYO_4", Me.ConvertStringZero(.Item("SURYO_4")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO_5", Me.NullConvertString(.Item("LOT_NO_5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO2_5", Me.NullConvertString(.Item("LOT_NO2_5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOSU_5", Me.ConvertStringZero(.Item("KOSU_5")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SURYO_5", Me.ConvertStringZero(.Item("SURYO_5")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@TTL_KOSU", Me.ConvertStringZero(.Item("TTL_KOSU")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@TTL_SURYO", Me.ConvertStringZero(.Item("TTL_SURYO")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@GENKA_BUMON", Me.NullConvertString(.Item("GENKA_BUMON")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHANAI_URI_KB", Me.NullConvertString(.Item("SHANAI_URI_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NONYU_YMD", Me.NullConvertString(.Item("NONYU_YMD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UKEHARAI_KB", Me.NullConvertString(.Item("UKEHARAI_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYOSHA_CD", Me.NullConvertString(.Item("GYOSHA_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHORI_NO", Me.NullConvertString(.Item("SHORI_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHORI_NO_EDA", Me.NullConvertString(.Item("SHORI_NO_EDA")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERROR_FLG", Me.NullConvertString(.Item("ERROR_FLG")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KO_UKETSUKE_NO", Me.NullConvertString(.Item("KO_UKETSUKE_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KO_UKETSUKE_NO_EDA", Me.NullConvertString(.Item("KO_UKETSUKE_NO_EDA")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HARAIDASI_BASHO", Me.NullConvertString(.Item("HARAIDASI_BASHO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KANJO_KAMOKU", Me.NullConvertString(.Item("KANJO_KAMOKU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOUMOKU_MEI", Me.NullConvertString(.Item("KOUMOKU_MEI")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KIGYO_HIMOKU", Me.NullConvertString(.Item("KIGYO_HIMOKU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHUBETSU", Me.NullConvertString(.Item("SHUBETSU")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@URIAGE_SURYO", Me.ConvertStringZero(.Item("URIAGE_SURYO")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@CROPS_KB", Me.NullConvertString(.Item("CROPS_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KESSAI_SURYO_KB", Me.NullConvertString(.Item("KESSAI_SURYO_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENPYO_NO2", Me.NullConvertString(.Item("DENPYO_NO2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SONOTA_1", Me.NullConvertString(.Item("SONOTA_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SONOTA_2", Me.NullConvertString(.Item("SONOTA_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SONOTA_3", Me.NullConvertString(.Item("SONOTA_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KANBAN_ORDER_NO", Me.NullConvertString(.Item("KANBAN_ORDER_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOBETSU_NISUGATA_CD_1", Me.NullConvertString(.Item("KOBETSU_NISUGATA_CD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOBETSU_SP_CD_1", Me.NullConvertString(.Item("KOBETSU_SP_CD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOBETSU_NISUGATA_CD_2", Me.NullConvertString(.Item("KOBETSU_NISUGATA_CD_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOBETSU_SP_CD_2", Me.NullConvertString(.Item("KOBETSU_SP_CD_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOBETSU_NISUGATA_CD_3", Me.NullConvertString(.Item("KOBETSU_NISUGATA_CD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOBETSU_SP_CD_3", Me.NullConvertString(.Item("KOBETSU_SP_CD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOBETSU_NISUGATA_CD_4", Me.NullConvertString(.Item("KOBETSU_NISUGATA_CD_4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOBETSU_SP_CD_4", Me.NullConvertString(.Item("KOBETSU_SP_CD_4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOBETSU_NISUGATA_CD_5", Me.NullConvertString(.Item("KOBETSU_NISUGATA_CD_5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOBETSU_SP_CD_5", Me.NullConvertString(.Item("KOBETSU_SP_CD_5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@YUSO_COMP_CD", Me.NullConvertString(.Item("YUSO_COMP_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_BASHO_AITE", Me.NullConvertString(.Item("OUTKA_BASHO_AITE")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHABAN", Me.NullConvertString(.Item("SHABAN")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GEKKAN_KB", Me.NullConvertString(.Item("GEKKAN_KB")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@YOBI", Me.NullConvertString(.Item("YOBI")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@YOBI2", Me.NullConvertString(.Item("YOBI2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERROR_CD_1", Me.NullConvertString(.Item("ERROR_CD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILLER_1", Me.NullConvertString(.Item("FILLER_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIFT_CD_1", Me.NullConvertString(.Item("SHIFT_CD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERROR_NM_1", Me.NullConvertString(.Item("ERROR_NM_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERROR_CD_2", Me.NullConvertString(.Item("ERROR_CD_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILLER_2", Me.NullConvertString(.Item("FILLER_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIFT_CD_2", Me.NullConvertString(.Item("SHIFT_CD_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERROR_NM_2", Me.NullConvertString(.Item("ERROR_NM_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERROR_CD_3", Me.NullConvertString(.Item("ERROR_CD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILLER_3", Me.NullConvertString(.Item("FILLER_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIFT_CD_3", Me.NullConvertString(.Item("SHIFT_CD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERROR_NM_3", Me.NullConvertString(.Item("ERROR_NM_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERROR_CD_4", Me.NullConvertString(.Item("ERROR_CD_4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILLER_4", Me.NullConvertString(.Item("FILLER_4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIFT_CD_4", Me.NullConvertString(.Item("SHIFT_CD_4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERROR_NM_4", Me.NullConvertString(.Item("ERROR_NM_4")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERROR_CD_5", Me.NullConvertString(.Item("ERROR_CD_5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILLER_5", Me.NullConvertString(.Item("FILLER_5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIFT_CD_5", Me.NullConvertString(.Item("SHIFT_CD_5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERROR_NM_5", Me.NullConvertString(.Item("ERROR_NM_5")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@RECORD_STATUS", Me.NullConvertString(.Item("RECORD_STATUS")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", Me.NullConvertString(.Item("JISSEKI_SHORI_FLG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", updTime, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_USER", Me.NullConvertString(.Item("SEND_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_DATE", Me.NullConvertString(.Item("SEND_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEND_TIME", Me.NullConvertString(.Item("SEND_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ERR_RCV_USER", String.Empty))
            prmList.Add(MyBase.GetSqlParameter("@ERR_RCV_DATE", String.Empty))
            prmList.Add(MyBase.GetSqlParameter("@ERR_RCV_TIME", String.Empty))
            prmList.Add(MyBase.GetSqlParameter("@CRT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_TIME", updTime, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", Me.NullConvertString(.Item("UPD_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", Me.NullConvertString(.Item("UPD_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", Me.NullConvertString(.Item("UPD_TIME")), DBDataType.CHAR))


        End With

    End Sub

#End Region

#Region "出荷(大)更新パラメータ設定"

    ''' <summary>
    ''' 出荷(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList, ByVal eventShubetsu As Integer)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me.NullConvertString(.Item("OUTKA_NO_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FURI_NO", Me.NullConvertString(.Item("FURI_NO")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_KB", Me.NullConvertString(.Item("OUTKA_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", Me.NullConvertString(.Item("SYUBETU_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_STATE_KB", Me.NullConvertString(.Item("OUTKA_STATE_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKAHOKOKU_YN", Me.NullConvertString(.Item("OUTKAHOKOKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PICK_KB", Me.NullConvertString(.Item("PICK_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_NO", Me.NullConvertString(.Item("DENP_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_KANRYO_INFO", Me.NullConvertString(.Item("ARR_KANRYO_INFO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", Me.NullConvertString(.Item("WH_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", Me.NullConvertString(.Item("OUTKA_PLAN_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", Me.NullConvertString(.Item("OUTKO_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", Me.NullConvertString(.Item("ARR_PLAN_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", Me.NullConvertString(.Item("ARR_PLAN_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", Me.NullConvertString(.Item("TOUKI_HOKAN_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@END_DATE", Me.NullConvertString(.Item("END_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(.Item("CUST_CD_M")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", Me.NullConvertString(.Item("SHIP_CD_L")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", Me.NullConvertString(.Item("SHIP_CD_M")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.NullConvertString(.Item("DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", Me.NullConvertString(.Item("DEST_AD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_TEL", Me.NullConvertString(.Item("DEST_TEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NHS_REMARK", Me.NullConvertString(.Item("NHS_REMARK")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", Me.NullConvertString(.Item("SP_NHS_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", Me.NullConvertString(.Item("COA_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", Me.NullConvertString(.Item("CUST_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", Me.NullConvertString(.Item("BUYER_ORD_NO")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(.Item("REMARK")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.NullConvertZero(.Item("OUTKA_PKG_NB")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DENP_YN", Me.NullConvertString(.Item("DENP_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PC_KB", Me.NullConvertString(.Item("PC_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", Me.NullConvertString(.Item("NIYAKU_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALL_PRINT_FLAG", Me.NullConvertString(.Item("ALL_PRINT_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIHUDA_FLAG", Me.NullConvertString(.Item("NIHUDA_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NHS_FLAG", Me.NullConvertString(.Item("NHS_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_FLAG", Me.NullConvertString(.Item("DENP_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_FLAG", Me.NullConvertString(.Item("COA_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKOKU_FLAG", Me.NullConvertString(.Item("HOKOKU_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MATOME_PICK_FLAG", Me.NullConvertString(.Item("MATOME_PICK_FLAG")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_DATE", Me.NullConvertString(.Item("LAST_PRINT_DATE")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_TIME", Me.NullConvertString(.Item("LAST_PRINT_TIME")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SASZ_USER", Me.NullConvertString(.Item("SASZ_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_USER", Me.NullConvertString(.Item("OUTKO_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KEN_USER", Me.NullConvertString(.Item("KEN_USER")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_USER", Me.NullConvertString(.Item("OUTKA_USER")), DBDataType.NVARCHAR))

            'イベント種別の判断
            Select Case DirectCast(eventShubetsu, LMH030DAC.EventShubetsu)

                '出荷登録処理
                '出荷登録SQL CONST名
                Case LMH030DAC.EventShubetsu.SAVEOUTKA
                    prmList.Add(MyBase.GetSqlParameter("@HOU_USER", Me.NullConvertString(.Item("HOU_USER")), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", Me.NullConvertString(.Item("HOKOKU_DATE")), DBDataType.CHAR))

                    '実績作成SQL CONST名
                Case LMH030DAC.EventShubetsu.CREATEJISSEKI
                    prmList.Add(MyBase.GetSqlParameter("@HOU_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))

                    '実績作成済⇒実績未,実績送信済⇒実績未処理
                Case LMH030DAC.EventShubetsu.SAKUSEIZUMI_JISSEKIMI, LMH030DAC.EventShubetsu.SOUSINZUMI_JISSEKIMI
                    prmList.Add(MyBase.GetSqlParameter("@HOU_USER", String.Empty, DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", String.Empty, DBDataType.CHAR))

            End Select
            prmList.Add(MyBase.GetSqlParameter("@ORDER_TYPE", Me.NullConvertString(.Item("ORDER_TYPE")), DBDataType.NVARCHAR))
            '2011.09.16 追加START
            prmList.Add(MyBase.GetSqlParameter("@DEST_KB", Me.NullConvertString(.Item("DEST_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", Me.NullConvertString(.Item("DEST_AD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", Me.NullConvertString(.Item("DEST_AD_2")), DBDataType.NVARCHAR))
            '2011.09.16 追加END

        End With

    End Sub

#End Region

#Region "出荷(中)更新パラメータ設定"

    ''' <summary>
    ''' 出荷(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_SET_NO", .Item("EDI_SET_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", .Item("CUST_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", .Item("BUYER_ORD_NO_DTL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_KB", .Item("ALCTD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", Me.FormatNumValue(.Item("OUTKA_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU", Me.FormatNumValue(.Item("OUTKA_HASU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_QT", Me.FormatNumValue(.Item("OUTKA_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", Me.FormatNumValue(.Item("OUTKA_TTL_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", Me.FormatNumValue(.Item("OUTKA_TTL_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", Me.FormatNumValue(.Item("ALCTD_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", Me.FormatNumValue(.Item("ALCTD_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BACKLOG_NB", Me.FormatNumValue(.Item("BACKLOG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BACKLOG_QT", Me.FormatNumValue(.Item("BACKLOG_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_M_PKG_NB", Me.FormatNumValue(.Item("OUTKA_M_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAIKO_KB", .Item("ZAIKO_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SOURCE_CD", .Item("SOURCE_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@YELLOW_CARD", .Item("YELLOW_CARD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", Me.FormatNumValue(.Item("PRINT_SORT").ToString()), DBDataType.NUMERIC))

        End With

    End Sub

#End Region

#Region "作業更新パラメータ設定"

    ''' <summary>
    ''' 作業の更新パラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_REC_NO", .Item("SAGYO_REC_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP", .Item("SAGYO_COMP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SKYU_CHK", .Item("SKYU_CHK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_SIJI_NO", .Item("SAGYO_SIJI_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_LM", .Item("INOUTKA_NO_LM").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IOZS_KB", .Item("IOZS_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_CD", .Item("SAGYO_CD").ToString(), DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@SAGYO_NM", .Item("SAGYO_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM_NRS", .Item("GOODS_NM_NRS").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            'prmList.Add(MyBase.GetSqlParameter("@INV_TANI", .Item("INV_TANI").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_NB", .Item("SAGYO_NB").ToString(), DBDataType.NUMERIC))
            'prmList.Add(MyBase.GetSqlParameter("@SAGYO_UP", .Item("SAGYO_UP").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_GK", .Item("SAGYO_GK").ToString(), DBDataType.NUMERIC))
            'prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))	'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            'prmList.Add(MyBase.GetSqlParameter("@REMARK_ZAI", .Item("REMARK_ZAI").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_SKYU", .Item("REMARK_SKYU").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_CD", .Item("SAGYO_COMP_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_COMP_DATE", .Item("SAGYO_COMP_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_SAGYO_FLG", .Item("DEST_SAGYO_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "運送(大)パラメータ設定"

    ''' <summary>
    ''' 運送(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoLComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Item("UNSO_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", .Item("UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@JIYU_KB", .Item("JIYU_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_NO", .Item("DENP_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_TIME", .Item("OUTKA_PLAN_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", .Item("ARR_PLAN_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_ACT_TIME", .Item("ARR_ACT_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_REF_NO", .Item("CUST_REF_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD", .Item("SHIP_CD").ToString(), DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", .Item("SHIP_CD_M").ToString(), DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@ORIG_CD", .Item("ORIG_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_PKG_NB", Me.FormatNumValue(.Item("UNSO_PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@NB_UT", .Item("NB_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_WT", Me.FormatNumValue(.Item("UNSO_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PC_KB", .Item("PC_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", .Item("TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@VCLE_KB", .Item("VCLE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Item("MOTO_DATA_KB").ToString(), DBDataType.CHAR))
            '要望番号602 2011.12.08 修正START
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            '要望番号602 2011.12.08 修正END
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_3", .Item("AD_3").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", .Item("UNSO_TEHAI_KB").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUY_CHU_NO", .Item("BUY_CHU_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AREA_CD", .Item("AREA_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TYUKEI_HAISO_FLG", .Item("TYUKEI_HAISO_FLG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUKA_TYUKEI_CD", .Item("SYUKA_TYUKEI_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HAIKA_TYUKEI_CD", .Item("HAIKA_TYUKEI_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_SYUKA", .Item("TRIP_NO_SYUKA").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_TYUKEI", .Item("TRIP_NO_TYUKEI").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_HAIKA", .Item("TRIP_NO_HAIKA").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

#Region "運送(中)パラメータ設定"

    ''' <summary>
    ''' 運送(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoMComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TTL_NB", Me.FormatNumValue(.Item("UNSO_TTL_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@NB_UT", .Item("NB_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TTL_QT", Me.FormatNumValue(.Item("UNSO_TTL_QT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@QT_UT", .Item("QT_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HASU", Me.FormatNumValue(.Item("HASU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(.Item("IRIME").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(.Item("BETU_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZBUKA_CD", .Item("ZBUKA_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ABUKA_CD", .Item("ABUKA_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PKG_NB", Me.FormatNumValue(.Item("PKG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "届先マスタ更新パラメータ設定(日本合成化学専用)"

    ''' <summary>
    ''' 届先マスタの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetMdestUpdateParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")


            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZIP", Me.NullConvertString(.Item("ZIP")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_1", Me.NullConvertString(.Item("AD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_2", Me.NullConvertString(.Item("AD_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_3", Me.NullConvertString(.Item("AD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TEL", Me.NullConvertString(.Item("TEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JIS", Me.NullConvertString(.Item("JIS")), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "届先マスタ自動追加用パラメータ設定(日本合成化学専用)"

    ''' <summary>
    ''' 届先マスタの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetMdestInsertParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(.Item("CUST_CD_L")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.NullConvertString(.Item("DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CD", Me.NullConvertString(.Item("EDI_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.NullConvertString(.Item("DEST_NM")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZIP", Me.NullConvertString(.Item("ZIP")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_1", Me.NullConvertString(.Item("AD_1")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_2", Me.NullConvertString(.Item("AD_2")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@AD_3", Me.NullConvertString(.Item("AD_3")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_DEST_CD", Me.NullConvertString(.Item("CUST_DEST_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SALES_CD", Me.NullConvertString(.Item("SALES_CD")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", Me.NullConvertString(.Item("SP_NHS_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", Me.NullConvertString(.Item("COA_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_UNSO_CD", Me.NullConvertString(.Item("SP_UNSO_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_UNSO_BR_CD", Me.NullConvertString(.Item("SP_UNSO_BR_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DELI_ATT", Me.NullConvertString(.Item("DELI_ATT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CARGO_TIME_LIMIT", Me.NullConvertString(.Item("CARGO_TIME_LIMIT")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LARGE_CAR_YN", Me.NullConvertString(.Item("LARGE_CAR_YN")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TEL", Me.NullConvertString(.Item("TEL")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FAX", Me.NullConvertString(.Item("FAX")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_SEIQTO_CD", Me.NullConvertString(.Item("UNCHIN_SEIQTO_CD")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JIS", Me.NullConvertString(.Item("JIS")), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KYORI", Me.NullConvertZero(.Item("KYORI")), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PICK_KB", Me.NullConvertString(.Item("PICK_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", Me.NullConvertString(.Item("BIN_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MOTO_CHAKU_KB", Me.NullConvertString(.Item("MOTO_CHAKU_KB")), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@URIAGE_CD", Me.NullConvertString(.Item("URIAGE_CD")), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "請求運賃パラメータ設定"

    ''' <summary>
    ''' 運賃の更新パラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnchinComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO", .Item("SEIQ_GROUP_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO_M", .Item("SEIQ_GROUP_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))   '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            prmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", .Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_SYARYO_KB", .Item("SEIQ_SYARYO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_PKG_UT", .Item("SEIQ_PKG_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_NG_NB", Me.FormatNumValue(.Item("SEIQ_NG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_DANGER_KB", .Item("SEIQ_DANGER_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_BUNRUI_KB", .Item("SEIQ_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_KYORI", Me.FormatNumValue(.Item("SEIQ_KYORI").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_WT", Me.FormatNumValue(.Item("SEIQ_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_UNCHIN", Me.FormatNumValue(.Item("SEIQ_UNCHIN").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_CITY_EXTC", Me.FormatNumValue(.Item("SEIQ_CITY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_WINT_EXTC", Me.FormatNumValue(.Item("SEIQ_WINT_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_RELY_EXTC", Me.FormatNumValue(.Item("SEIQ_RELY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TOLL", Me.FormatNumValue(.Item("SEIQ_TOLL").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_INSU", Me.FormatNumValue(.Item("SEIQ_INSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_FIXED_FLAG", .Item("SEIQ_FIXED_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DECI_NG_NB", Me.FormatNumValue(.Item("DECI_NG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_KYORI", Me.FormatNumValue(.Item("DECI_KYORI").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WT", Me.FormatNumValue(.Item("DECI_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", Me.FormatNumValue(.Item("DECI_UNCHIN").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_CITY_EXTC", Me.FormatNumValue(.Item("DECI_CITY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WINT_EXTC", Me.FormatNumValue(.Item("DECI_WINT_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_RELY_EXTC", Me.FormatNumValue(.Item("DECI_RELY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_TOLL", Me.FormatNumValue(.Item("DECI_TOLL").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_INSU", Me.FormatNumValue(.Item("DECI_INSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_UNCHIN", Me.FormatNumValue(.Item("KANRI_UNCHIN").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_CITY_EXTC", Me.FormatNumValue(.Item("KANRI_CITY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_WINT_EXTC", Me.FormatNumValue(.Item("KANRI_WINT_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_RELY_EXTC", Me.FormatNumValue(.Item("KANRI_RELY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_TOLL", Me.FormatNumValue(.Item("KANRI_TOLL").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_INSU", Me.FormatNumValue(.Item("KANRI_INSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_KANRI", .Item("SAGYO_KANRI").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region

    'START UMANO 要望番号1302 支払運賃に伴う修正。
#Region "支払運賃パラメータ設定"

    ''' <summary>
    ''' 支払運賃の更新パラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetShiharaiComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO", .Item("SHIHARAI_GROUP_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO_M", .Item("SHIHARAI_GROUP_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAITO_CD", .Item("SHIHARAITO_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", .Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_SYARYO_KB", .Item("SHIHARAI_SYARYO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_PKG_UT", .Item("SHIHARAI_PKG_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_NG_NB", Me.FormatNumValue(.Item("SHIHARAI_NG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_DANGER_KB", .Item("SHIHARAI_DANGER_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_BUNRUI_KB", .Item("SHIHARAI_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_KYORI", Me.FormatNumValue(.Item("SHIHARAI_KYORI").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_WT", Me.FormatNumValue(.Item("SHIHARAI_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_UNCHIN", Me.FormatNumValue(.Item("SHIHARAI_UNCHIN").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_CITY_EXTC", Me.FormatNumValue(.Item("SHIHARAI_CITY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_WINT_EXTC", Me.FormatNumValue(.Item("SHIHARAI_WINT_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_RELY_EXTC", Me.FormatNumValue(.Item("SHIHARAI_RELY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TOLL", Me.FormatNumValue(.Item("SHIHARAI_TOLL").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_INSU", Me.FormatNumValue(.Item("SHIHARAI_INSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_FIXED_FLAG", .Item("SHIHARAI_FIXED_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DECI_NG_NB", Me.FormatNumValue(.Item("DECI_NG_NB").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_KYORI", Me.FormatNumValue(.Item("DECI_KYORI").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WT", Me.FormatNumValue(.Item("DECI_WT").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", Me.FormatNumValue(.Item("DECI_UNCHIN").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_CITY_EXTC", Me.FormatNumValue(.Item("DECI_CITY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WINT_EXTC", Me.FormatNumValue(.Item("DECI_WINT_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_RELY_EXTC", Me.FormatNumValue(.Item("DECI_RELY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_TOLL", Me.FormatNumValue(.Item("DECI_TOLL").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_INSU", Me.FormatNumValue(.Item("DECI_INSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_UNCHIN", Me.FormatNumValue(.Item("KANRI_UNCHIN").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_CITY_EXTC", Me.FormatNumValue(.Item("KANRI_CITY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_WINT_EXTC", Me.FormatNumValue(.Item("KANRI_WINT_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_RELY_EXTC", Me.FormatNumValue(.Item("KANRI_RELY_EXTC").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_TOLL", Me.FormatNumValue(.Item("KANRI_TOLL").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_INSU", Me.FormatNumValue(.Item("KANRI_INSU").ToString()), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_KANRI", .Item("SAGYO_KANRI").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region
    'END UMANO 要望番号1302 支払運賃に伴う修正。

#Region "一括変更(EDI出荷(大))パラメータ設定"

    ''' <summary>
    ''' 一括変更(EDI出荷(大))パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUpdHenkoPrm(ByVal dtKey As DataTable, ByVal dtVal As DataTable)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Dim editType1 As Integer = Convert.ToInt32(Me.NullConvertZero(dtVal.Rows(0).Item("EDIT_ITEM_TYPE1")))
        Dim editType2 As Integer = Convert.ToInt32(Me.NullConvertZero(dtVal.Rows(0).Item("EDIT_ITEM_TYPE2")))

        If String.IsNullOrEmpty(editType1.ToString()) = False Then
            Select Case editType1
                Case 3
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE1", dtVal.Rows(0).Item("EDIT_ITEM_VALUE1"), DBDataType.CHAR))
                Case 5
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE1", dtVal.Rows(0).Item("EDIT_ITEM_VALUE1"), DBDataType.NUMERIC))
                Case 12
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE1", dtVal.Rows(0).Item("EDIT_ITEM_VALUE1"), DBDataType.NVARCHAR))
                Case Else
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE1", Me.NullConvertString(dtVal.Rows(0).Item("EDIT_ITEM_VALUE1")), DBDataType.CHAR))
            End Select
        End If

        If String.IsNullOrEmpty(editType2.ToString()) = False Then
            Select Case editType2
                Case 3
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE2", dtVal.Rows(0).Item("EDIT_ITEM_VALUE2"), DBDataType.CHAR))
                Case 5
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE2", dtVal.Rows(0).Item("EDIT_ITEM_VALUE2"), DBDataType.NUMERIC))
                Case 12
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE2", dtVal.Rows(0).Item("EDIT_ITEM_VALUE2"), DBDataType.NVARCHAR))
                Case Else
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE2", Me.NullConvertString(dtVal.Rows(0).Item("EDIT_ITEM_VALUE2")), DBDataType.CHAR))
            End Select
        End If

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE3", Me.NullConvertString(dtVal.Rows(0).Item("EDIT_ITEM_VALUE3")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_ITEM_VALUE4", Me.NullConvertString(dtVal.Rows(0).Item("EDIT_ITEM_VALUE4")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dtKey.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", dtKey.Rows(0).Item("EDI_CTL_NO"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", dtKey.Rows(0).Item("SYS_UPD_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", dtKey.Rows(0).Item("SYS_UPD_TIME"), DBDataType.CHAR))

    End Sub

#End Region

#Region "一括変更(EDI出荷(大))パラメータ設定"

    ''' <summary>
    ''' 一括変更(届先M)パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUpdMDestPrm(ByVal dtVal As DataTable)

        ''SQLパラメータ初期化
        'Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", dtVal.Rows(0).Item("DEST_NM"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP", dtVal.Rows(0).Item("ZIP"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_1", dtVal.Rows(0).Item("AD_1"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_2", dtVal.Rows(0).Item("AD_2"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AD_3", dtVal.Rows(0).Item("AD_3"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEL", dtVal.Rows(0).Item("TEL"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FAX", dtVal.Rows(0).Item("FAX"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JIS", dtVal.Rows(0).Item("JIS"), DBDataType.NVARCHAR))

    End Sub

#End Region

#Region "同一まとめデータ取得パラメータ設定(出荷取消⇒未登録)"
    ''' <summary>
    ''' 同一まとめデータ取得パラメータ設定(出荷取消⇒未登録)
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetMatomeTorikesiSelectParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATOME_NO", conditionRow.Item("MATOME_NO"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", conditionRow.Item("OUTKA_CTL_NO"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_SYS_UPD_DATE", conditionRow.Item("SYS_UPD_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_SYS_UPD_TIME", conditionRow.Item("SYS_UPD_TIME"), DBDataType.CHAR))

    End Sub

#End Region

    '印刷フラグ更新対応 20120312 Start
#Region "印刷フラグ更新パラメータ設定"
    ''' <summary>
    ''' 印刷フラグ更新パラメータ設定
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub PrintFlagParameter(ByVal dr As DataRow, ByVal prmList As ArrayList)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", dr.Item("PRTFLG"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", dr.Item("WH_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dr.Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dr.Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", dr.Item("CUST_CD_M"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE_FROM", dr.Item("CRT_DATE_FROM"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE_TO", dr.Item("CRT_DATE_TO"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_TIME", updTime))
    End Sub
#End Region
    '印刷フラグ更新対応 20120312 End

    '要望番号1007 2012.05.08 修正START
#Region "EDI印刷対象テーブルパラメータ設定"
    ''' <summary>
    ''' EDI印刷対象テーブルパラメータ設定
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub HEdiPrintParameter(ByVal dr As DataRow, ByVal prmList As ArrayList)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dr.Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", dr.Item("EDI_CTL_NO"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", dr.Item("INOUT_KB"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dr.Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", dr.Item("CUST_CD_M"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_TP", dr.Item("PRINT_TP"), DBDataType.CHAR))
        '2012.05.29 要望番号1077 追加START
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENPYO_NO", dr.Item("DENPYO_NO"), DBDataType.NVARCHAR))
        '2012.05.29 要望番号1077 追加END

    End Sub
#End Region
    '要望番号1007 2012.05.08 修正END

    '要望番号:944（後からまとめをするとアベンド） 2012/05/25 Honmyo Start
#Region "出荷管理番号（中）MAX取得"

    ''' <summary>
    ''' 出荷管理番号（中）MAX取得
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetMaxOUTKA_NO_CHU(ByVal sNRS_BR_CD As String, ByVal sOUTKA_NO_L As String) As Integer

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Me.CreateSqlCommand(Me.SetSchemaNm(SQL_GetMaxOUTKA_NO_CHU, sNRS_BR_CD))

        Dim rtn As Integer = 0

        'SQLパラメータ初期化
        _SqlPrmList = New ArrayList

        'パラメータ設定
        _SqlPrmList.Add(GetSqlParameter("@NRS_BR_CD", sNRS_BR_CD, DBDataType.CHAR))
        _SqlPrmList.Add(GetSqlParameter("@OUTKA_NO_L", sOUTKA_NO_L, DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("GetMaxOUTKA_NO_CHU", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得内容の設定
        reader.Read()
        rtn = Convert.ToInt32(reader(0))
        reader.Close()

        Return rtn

    End Function

#End Region
    '要望番号:944（後からまとめをするとアベンド） 2012/05/25 Honmyo Start

#Region "引当可能数取得"

    ''' <summary>
    ''' 引当可能数取得
    ''' </summary>
    ''' <remarks></remarks>

    Public Function GetALLOC_CAN_NB(ByVal sNRS_BR_CD As String, ByVal sGOODS_CD_NRS As String, Optional ByVal sLOT_NO As String = "", Optional ByVal dIRIME As Double = 0) As Integer
        '要望番号:1155 2012/06/14 本明 Start
        'Public Function GetALLOC_CAN_NB(ByVal sNRS_BR_CD As String, ByVal sGOODS_CD_NRS As String, Optional ByVal sLOT_NO As String = "") As Integer
        '要望番号:1155 2012/06/14 本明 End


        '要望番号:1155 2012/06/14 本明 Start

        'LOT_NO指定されているか
        'Dim sSQL As String = String.Empty
        'If String.IsNullOrEmpty(sLOT_NO.Trim) Then
        '    sSQL = SQL_GetALLOC_CAN_NB
        'Else
        '    '指定されている場合はWhere句にLOT_NOの条件を追加
        '    sSQL = String.Concat(SQL_GetALLOC_CAN_NB, SQL_GetALLOC_CAN_NB_WhereLOT_NO)

        'End If

        Dim sSQL As String = SQL_GetALLOC_CAN_NB

        'LOT_NO指定されているか
        If String.IsNullOrEmpty(sLOT_NO.Trim) = False Then
            '指定されている場合はWhere句にLOT_NOの条件を追加
            sSQL = String.Concat(sSQL, SQL_GetALLOC_CAN_NB_WhereLOT_NO)
        End If

        '入り目が指定されているか
        If dIRIME <> 0 Then
            '指定されている場合はWhere句に入り目の条件を追加
            sSQL = String.Concat(sSQL, SQL_GetALLOC_CAN_NB_WhereIRIME)
        End If
        '要望番号:1155 2012/06/14 本明 End


        'SQL文のコンパイル
        Dim cmd As SqlCommand = Me.CreateSqlCommand(Me.SetSchemaNm(sSQL, sNRS_BR_CD))

        Dim rtn As Integer = 0

        'SQLパラメータ初期化
        _SqlPrmList = New ArrayList

        'パラメータ設定
        _SqlPrmList.Add(GetSqlParameter("@NRS_BR_CD", sNRS_BR_CD, DBDataType.CHAR))
        _SqlPrmList.Add(GetSqlParameter("@GOODS_CD_NRS", sGOODS_CD_NRS, DBDataType.NVARCHAR))
        _SqlPrmList.Add(GetSqlParameter("@LOT_NO", sLOT_NO, DBDataType.NVARCHAR))
        '要望番号:1155 2012/06/14 本明 Start
        _SqlPrmList.Add(GetSqlParameter("@IRIME", dIRIME, DBDataType.NUMERIC))
        '要望番号:1155 2012/06/14 本明 End

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("GetALLOC_CAN_NB", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得内容の設定
        reader.Read()
        rtn = Convert.ToInt32(reader(0))
        reader.Close()

        Return rtn

    End Function

#End Region

    ''' <summary>
    ''' 時間コロン編集
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

    End Function

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <remarks></remarks>
    Private Sub UpdateResultChk(ByVal cmd As SqlCommand)

        Dim updateCnt As Integer = 0

        updateCnt = MyBase.GetUpdateResult(cmd)
        'SQLの発行
        If updateCnt < 1 Then
            MyBase.SetMessage("E011")
        End If

        MyBase.SetResultCount(updateCnt)

    End Sub

#Region "テーブル名設定処理"
    ''' <summary>
    ''' 受信HEDテーブル名設定
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetRcvTableNm(ByVal sql As String) As String

        Dim rcvTblNm As String = _Row("RCV_NM_HED").ToString()
        sql = sql.Replace("$RCV_HED$", rcvTblNm)

        Return sql

    End Function

    ''' <summary>
    ''' 受信DTLテーブル名設定
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetRcvDtlTableNm(ByVal sql As String) As String

        Dim rcvTblNm As String = _Row("RCV_NM_DTL").ToString()
        sql = sql.Replace("$RCV_DTL$", rcvTblNm)

        Return sql

    End Function

    '2012.03.18 大阪対応START
    ''' <summary>
    ''' 受信DTLテーブル名設定
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetRcvExtTableNm(ByVal sql As String) As String

        Dim rcvTblNm As String = _Row("RCV_NM_EXT").ToString()
        sql = sql.Replace("$RCV_DTL$", rcvTblNm)

        Return sql

    End Function

    '2012.03.18 大阪対応END

    ''' <summary>
    ''' 送信テーブル名設定
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSendTableNm(ByVal sql As String) As String

        Dim sendTblNm As String = _Row("SND_NM").ToString()
        sql = sql.Replace("$SEND_TBL$", sendTblNm)

        Return sql

    End Function

#End Region

#Region "出荷管理番号に営業所イニシャル設定"

    Private Function SetBrInitial(ByVal sql As String, ByVal dr As DataRow) As String

        Dim brInitial As String = dr("EDI_CTL_NO").ToString().Substring(0, 1)
        sql = sql.Replace("$BR_INITIAL$", String.Concat("'", brInitial, "'"))

        Return sql

    End Function

#End Region

#End Region

#Region "まとめ先データ取得処理"

    ''' <summary>
    ''' まとめ先データ取得処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectMatomeTarget(ByVal ds As DataSet) As DataSet

        '荷主マスタよりまとめフラグを取得
        Dim matomeFlg As String = Me.GetMatomeFlg(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Select Case matomeFlg
            Case "1"
                'ディック（群馬）追加箇所 2012.07.11 terakawa Start(出荷登録時の纏め条件に【便区分】を加える)
                'SQL作成(まとめF=1共通)
                Me._StrSql.Append(LMH030DAC.SQL_SELECT_MATOME_TARGET_FLAG1)
                If String.IsNullOrEmpty(Me._Row.Item("DEST_CD").ToString().Trim()) = True Then
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("EDI_L.EDI_DEST_CD = @DEST_CD")
                    Me._StrSql.Append(vbNewLine)
                End If
                Me._StrSql.Append(LMH030DAC.SQL_WHERE_MATOME_TARGET_FLAG1)
                If String.IsNullOrEmpty(Me._Row.Item("DEST_CD").ToString().Trim()) = False Then
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("OUTKA_L.DEST_CD  = @DEST_CD")
                    Me._StrSql.Append(vbNewLine)
                End If
                Me._StrSql.Append(LMH030DAC.SQL_GROUPBY_MATOME_TARGET_FLAG1)
                'ディック（群馬）追加箇所 2012.07.11 terakawa End(出荷登録時の纏め条件に【便区分】を加える)

            Case "2"

                'SQL作成(まとめF=2共通)
                Me._StrSql.Append(LMH030DAC.SQL_SELECT_MATOME_TARGET_FLAG2)
                If String.IsNullOrEmpty(Me._Row.Item("DEST_CD").ToString().Trim()) = True Then
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("EDI_L.EDI_DEST_CD = @DEST_CD")
                    Me._StrSql.Append(vbNewLine)
                End If
                Me._StrSql.Append(LMH030DAC.SQL_WHERE_MATOME_TARGET_FLAG2)
                If String.IsNullOrEmpty(Me._Row.Item("DEST_CD").ToString().Trim()) = False Then
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("OUTKA_L.DEST_CD  = @DEST_CD")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(LMH030DAC.SQL_GROUPBY_MATOME_TARGET_FLAG2)

                'Me._StrSql.Append(LMH030DAC.SQL_SELECT_MATOME_TARGET2)

            Case "3"    '日産物流（千葉）
                '2012/10/02 日産物流まとめ処理　本明追加 Start

                '一括まとめ対象出荷管理番号
                Dim chkTbl As DataTable = ds.Tables("LMH030_IKKATUMATOME_CHK")

                '一括で処理した出荷データが無ければまとめ先はなし
                If chkTbl.Rows.Count = 0 Then
                    MyBase.SetResultCount(0)
                    Return ds
                End If

                'SQL作成(まとめF=3共通)
                Me._StrSql.Append(LMH030DAC.SQL_SELECT_MATOME_TARGET_FLAG3)
                If String.IsNullOrEmpty(Me._Row.Item("DEST_CD").ToString().Trim()) = True Then
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("EDI_L.EDI_DEST_CD = @DEST_CD")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(LMH030DAC.SQL_WHERE_MATOME_TARGET_FLAG3)
                If String.IsNullOrEmpty(Me._Row.Item("DEST_CD").ToString().Trim()) = False Then
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("OUTKA_L.DEST_CD  = @DEST_CD")
                    Me._StrSql.Append(vbNewLine)
                End If

                '一括で処理した出荷管理番号をWhere条件に設定
                Call Me.SetConditionOutkaNoL(ds)

                Me._StrSql.Append(LMH030DAC.SQL_GROUPBY_MATOME_TARGET_FLAG3)
                '2012/10/02 日産物流まとめ処理　本明追加 End

            Case "4"
                'TODO

                '要望番号:1177(出荷登録時の纏め条件に【EDI受信日】を加える)　2012/06/20 本明 Start
            Case "6"    '大日精化（埼玉）
                'SQL作成(まとめF=6共通)
                Me._StrSql.Append(LMH030DAC.SQL_SELECT_MATOME_TARGET_FLAG6)
                If String.IsNullOrEmpty(Me._Row.Item("DEST_CD").ToString().Trim()) = True Then
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("EDI_L.EDI_DEST_CD = @DEST_CD")
                    Me._StrSql.Append(vbNewLine)
                End If
                Me._StrSql.Append(LMH030DAC.SQL_WHERE_MATOME_TARGET_FLAG6)
                If String.IsNullOrEmpty(Me._Row.Item("DEST_CD").ToString().Trim()) = False Then
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("OUTKA_L.DEST_CD  = @DEST_CD")
                    Me._StrSql.Append(vbNewLine)
                End If
                Me._StrSql.Append(LMH030DAC.SQL_GROUPBY_MATOME_TARGET_FLAG6)

                '要望番号:1177(出荷登録時の纏め条件に【EDI受信日】を加える)　2012/06/20 本明 End

                '2012.08.03 ビックケミー対応START(出荷登録時の纏め条件に【配送時注意事項】(UNSO_ATT)を加える)
            Case "7"    'ビックケミー（千葉）
                'SQL作成(まとめF=7共通)
                Me._StrSql.Append(LMH030DAC.SQL_SELECT_MATOME_TARGET_FLAG7)
                If String.IsNullOrEmpty(Me._Row.Item("DEST_CD").ToString().Trim()) = True Then
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("EDI_L.EDI_DEST_CD = @DEST_CD")
                    Me._StrSql.Append(vbNewLine)
                End If
                Me._StrSql.Append(LMH030DAC.SQL_WHERE_MATOME_TARGET_FLAG7)
                If String.IsNullOrEmpty(Me._Row.Item("DEST_CD").ToString().Trim()) = False Then
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("OUTKA_L.DEST_CD  = @DEST_CD")
                    Me._StrSql.Append(vbNewLine)
                End If
                Me._StrSql.Append(LMH030DAC.SQL_GROUPBY_MATOME_TARGET_FLAG7)

                '2012.08.03 ビックケミー対応END(出荷登録時の纏め条件に【配送時注意事項】(UNSO_ATT)を加える)

#If True Then ' セミEDI(フィルメニッヒ) まとめ対象変更対応 20161017 added inoue

            Case MatomeFlgType.FIR    ' フィルメニッヒ(横浜)

                Me._StrSql.Append(LMH030DAC.SQL_SELECT_MATOME_TARGET_FLAG8)
                If String.IsNullOrEmpty(Me._Row.Item("DEST_CD").ToString().Trim()) = True Then
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("EDI_L.EDI_DEST_CD = @DEST_CD")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(LMH030DAC.SQL_WHERE_MATOME_TARGET_FLAG8)
                If String.IsNullOrEmpty(Me._Row.Item("DEST_CD").ToString().Trim()) = False Then
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("OUTKA_L.DEST_CD  = @DEST_CD")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(LMH030DAC.SQL_GROUPBY_MATOME_TARGET_FLAG8)


#End If

            Case MatomeFlgType.DIC_00076    ' ＤＩＣ物流群馬
                'SQL作成(まとめF=9)
                Me._StrSql.Append(LMH030DAC.SQL_SELECT_MATOME_TARGET_FLAG9)
                If String.IsNullOrEmpty(Me._Row.Item("DEST_CD").ToString().Trim()) = True Then
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("EDI_L.EDI_DEST_CD = @DEST_CD")
                    Me._StrSql.Append(vbNewLine)
                End If
                Me._StrSql.Append(LMH030DAC.SQL_WHERE_MATOME_TARGET_FLAG9)
                If String.IsNullOrEmpty(Me._Row.Item("DEST_CD").ToString().Trim()) = False Then
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("OUTKA_L.DEST_CD  = @DEST_CD")
                    Me._StrSql.Append(vbNewLine)
                End If
                Me._StrSql.Append(LMH030DAC.SQL_GROUPBY_MATOME_TARGET_FLAG9)

            Case MatomeFlgType.TrmSmpl00409_01  ' (土気)テルモ サンプル
                'SQL作成(まとめF=A)
                Me._StrSql.Append(LMH030DAC.SQL_SELECT_MATOME_TARGET_FLAG2)
                If String.IsNullOrEmpty(Me._Row.Item("DEST_CD").ToString().Trim()) = True Then
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("EDI_L.EDI_DEST_CD = @DEST_CD")
                    Me._StrSql.Append(vbNewLine)
                End If
                Me._StrSql.Append(LMH030DAC.SQL_WHERE_MATOME_TARGET_FLAG2)
                If String.IsNullOrEmpty(Me._Row.Item("DEST_CD").ToString().Trim()) = False Then
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("OUTKA_L.DEST_CD  = @DEST_CD")
                    Me._StrSql.Append(vbNewLine)
                End If
                '「まとめF=2共通」をベースに独自の WHERE 条件を付加する
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append("AND")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append("OUTKA_L.REMARK  = @REMARK")
                Me._StrSql.Append(vbNewLine)

                Me._StrSql.Append(LMH030DAC.SQL_GROUPBY_MATOME_TARGET_FLAG2)

            Case MatomeFlgType.JASM             ' JASM
                'SQL作成(まとめF=B)
                Me._StrSql.Append(LMH030DAC.SQL_SELECT_MATOME_TARGET_FLAG2)
                If String.IsNullOrEmpty(Me._Row.Item("DEST_CD").ToString().Trim()) = True Then
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("EDI_L.EDI_DEST_CD = @DEST_CD")
                    Me._StrSql.Append(vbNewLine)
                End If
                Me._StrSql.Append(LMH030DAC.SQL_WHERE_MATOME_TARGET_FLAG2)
                If String.IsNullOrEmpty(Me._Row.Item("DEST_CD").ToString().Trim()) = False Then
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("OUTKA_L.DEST_CD  = @DEST_CD")
                    Me._StrSql.Append(vbNewLine)
                End If
                '「まとめF=2共通」をベースに独自の WHERE 条件を付加する
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append("AND")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append("OUTKA_L.WH_CD  = @WH_CD")
                Me._StrSql.Append(vbNewLine)

                Me._StrSql.Append(LMH030DAC.SQL_GROUPBY_MATOME_TARGET_FLAG2)

            Case Else
                '各荷主専用DACで処理(サクラ)
                'ここは通ってはいけない
        End Select

        Call Me.SetMatomePrm(inTbl)


        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectMatomeTarget", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("OUTKA_CTL_NO_CHU", "OUTKA_CTL_NO_CHU")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("SYS_UNSO_UPD_DATE", "SYS_UNSO_UPD_DATE")
        map.Add("SYS_UNSO_UPD_TIME", "SYS_UNSO_UPD_TIME")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        '共通修正
        map.Add("UNSO_PKG_NB", "UNSO_PKG_NB")
        map.Add("NHS_REMARK", "NHS_REMARK")
        map.Add("OUTKA_STATE_KB", "OUTKA_STATE_KB")
        '2012.02.28 大阪対応START
        map.Add("REMARK", "REMARK")
        '2012.02.28 大阪対応END
        '2012.03.07 大阪対応START
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        '2012.03.07 大阪対応END
        '要望番号922 2012.03.29 追加START
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        '要望番号922 2012.03.29 追加START

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_MATOMESAKI_EDIL")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMH030_MATOMESAKI_EDIL").Rows.Count())
        reader.Close()
        Return ds

    End Function

    '2012/10/02 日産物流まとめ処理　本明追加 Start
    ''' <summary>
    ''' まとめ先取得SQLのWHERE句設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetConditionOutkaNoL(ByVal ds As DataSet)
        '検索条件設定

        Me._StrSql.Append(" AND ")
        Me._StrSql.Append(vbNewLine)

        Me._StrSql.Append(" OUTKA_L.OUTKA_STATE_KB = '10' ")
        Me._StrSql.Append(vbNewLine)

        Dim matomeChkDt As DataTable = ds.Tables("LMH030_IKKATUMATOME_CHK")
        Dim max As Integer = matomeChkDt.Rows.Count - 1
        Dim sOutkaNoL As String = String.Empty

        If max > -1 Then
            Me._StrSql.Append(" AND ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" OUTKA_L.OUTKA_NO_L IN ( ")
            Me._StrSql.Append(vbNewLine)

            For i As Integer = 0 To max
                sOutkaNoL = matomeChkDt.Rows(i)("OUTKA_NO_L").ToString()
                If i <> max Then
                    Me._StrSql.Append("'" & sOutkaNoL & "',")
                    Me._StrSql.Append(vbNewLine)
                Else
                    Me._StrSql.Append("'" & sOutkaNoL & "')")
                    Me._StrSql.Append(vbNewLine)
                End If
            Next

        End If

    End Sub
    '2012/10/02 日産物流まとめ処理　本明追加 End


    ''' <summary>
    ''' まとめ先データ取得処理パラメータ設定
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub SetMatomePrm(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", dt.Rows(0).Item("OUTKA_PLAN_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", dt.Rows(0).Item("OUTKO_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", dt.Rows(0).Item("ARR_PLAN_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", dt.Rows(0).Item("UNSO_CD"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", dt.Rows(0).Item("UNSO_BR_CD"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", dt.Rows(0).Item("CUST_CD_M"), DBDataType.CHAR))
        'ディック（群馬）追加箇所 20120711 terakawa Start
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", dt.Rows(0).Item("BIN_KB"), DBDataType.CHAR))
        'ディック（群馬）追加箇所 20120711 terakawa End

        '届先コードが空の場合は、EDI届先コード
        If String.IsNullOrEmpty(dt.Rows(0).Item("DEST_CD").ToString()) = False Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", dt.Rows(0).Item("DEST_CD"), DBDataType.NVARCHAR))
        Else
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", dt.Rows(0).Item("EDI_DEST_CD"), DBDataType.NVARCHAR))
        End If

        '要望番号:1177(出荷登録時の纏め条件に【EDI受信日】を加える)　2012/06/20 本明 Start
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", dt.Rows(0).Item("CRT_DATE"), DBDataType.CHAR))
        '要望番号:1177(出荷登録時の纏め条件に【EDI受信日】を加える)　2012/06/20 本明 End

        '2012.08.03 ビックケミー対応START
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ATT", dt.Rows(0).Item("UNSO_ATT"), DBDataType.NVARCHAR))
        '2012.08.03 ビックケミー対応END

        '2013.03.06 要望番号1929 追加START
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", dt.Rows(0).Item("SHIP_CD_L"), DBDataType.NVARCHAR))
        '2013.03.06 要望番号1929 追加END

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", dt.Rows(0).Item("REMARK"), DBDataType.NVARCHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", dt.Rows(0).Item("WH_CD"), DBDataType.NVARCHAR))


    End Sub

    ''' <summary>
    ''' まとめフラグ取得処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>EDI荷主マスタからまとめフラグを取得する</remarks>
    Private Function GetMatomeFlg(ByVal ds As DataSet) As String

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMH030DAC.SQL_SELECT_MATOMEFLG)

        Dim dt As DataTable = ds.Tables("LMH030INOUT")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetMatomeFlgPrm(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "GetMatomeFlg", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        If reader.Read() = True Then
            Dim rtnFlg As String = reader("FLAG_07").ToString().Trim()
            reader.Close()
            Return rtnFlg
        End If

        reader.Close()

        Return ""

    End Function

    ''' <summary>
    ''' まとめフラグ取得処理パラメータ設定
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub SetMatomeFlgPrm(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", dt.Rows(0).Item("CUST_CD_M"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", dt.Rows(0).Item("WH_CD"), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' まとめデータ(運送M)取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(中)のまとめデータ(個別重量)の取得SQLの構築・発行</remarks>
    Private Function SelectUnsoMatomeTarget(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        Me._StrSqlOrderBy = New StringBuilder()
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMH030DAC.SQL_MATOMEMOTO_DATA_UNSO_M)      'SQL構築

        Call Me.SetMatomeUnsoMDataPrm()                              '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "SelectUnsoMatomeTarget", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("BETU_WT", "BETU_WT")
        map.Add("UNSO_TTL_NB", "UNSO_TTL_NB")
        map.Add("HASU", "HASU")
        map.Add("PKG_NB", "PKG_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH030_MATOME_UNSO_M")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMH030_MATOME_UNSO_M").Rows.Count())
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    '''  パラメータ設定（運送(中)）
    ''' </summary>
    ''' <remarks>まとめデータ検索用パラメータ設定</remarks>
    Private Sub SetMatomeUnsoMDataPrm()

        'パラメータ設定(サクラまとめ専用）
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(Me._Row("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me.NullConvertString(Me._Row("UNSO_NO_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", Me.NullConvertString(Me._Row.Item("UNSO_NO_M")), DBDataType.CHAR))

    End Sub



#End Region
    '▲▲▲二次(共通化)

    ''' <summary>
    ''' 受信DTL取得処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks>受信DTLを取得する</remarks>
    Private Function GetRcvNmDtl(ByVal ds As DataSet) As String

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMH030DAC.SQL_SELECT_RCV_NM_DTL)

        Dim dt As DataTable = ds.Tables("LMH030_OUTPUTIN")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetRcv_Nm_DtlPrm(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH030DAC", "GetRcvNmDtl", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        If reader.Read() = True Then
            Dim RcvNmDtl As String = reader("RCV_NM_DTL").ToString().Trim()
            reader.Close()
            Return RcvNmDtl
        End If

        reader.Close()

        Return ""

    End Function

    ''' <summary>
    ''' 受信DTL取得パラメータ設定
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub SetRcv_Nm_DtlPrm(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", dt.Rows(0).Item("CUST_CD_M"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", dt.Rows(0).Item("WH_CD"), DBDataType.CHAR))

    End Sub

#End Region 'Method

End Class

