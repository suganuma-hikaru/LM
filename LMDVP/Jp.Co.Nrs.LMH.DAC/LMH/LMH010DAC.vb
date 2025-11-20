' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH010    : EDI入荷検索
'  EDI荷主ID　　　　:  共通
'  作  成  者       :  nishikawa
' ==========================================================================
Option Strict On
Option Explicit On

Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LME010DAC
''' </summary>
''' <remarks></remarks>
''' 
Public Class LMH010DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "イベント種別"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks>
    ''' ★★★★★★★★★★★★★★★★★★★★★★★★★★★
    ''' この定義はLMH010C.vbと定義を一致させる必要があります。
    ''' ★★★★★★★★★★★★★★★★★★★★★★★★★★★
    ''' </remarks>
    Public Enum EventShubetsu As Integer

        TOROKU = 1                  '登録
        JISSEKI_SAKUSE              '実績作成
        HIMODUKE                    '紐付
        EDI_TORIKESI                'EDI取消
        TORIKOMI                    '取込
        JISSEKI_TORIKESI            '実績取消
        KENSAKU                     '検索
        MASTER                      'マスタ参照
        DEF_CUST                    '初期荷主変更
        CLOSE                       '閉じる
        DOUBLE_CLICK                'ダブルクリック
        JIKKOU_TORIKESI_MITOUROKU   '実行(EDI取消⇒未登録)
        JIKKOU_HOUKOKU_EDI_TORIKESI '実行(実績報告用EDIデータ取消)
        JIKKOU_SAKUSEI_JISSEKIMI    '実行(実績作成済⇒実績未)
        JIKKOU_SOUSIN_SOUSINMI      '実行(実績送信済⇒送信待)
        JIKKOU_SOUSIN_JISSEKIMI     '実行(実績送信済⇒実績未)
        JIKKOU_TOUROKU_MITOUROKU	
 		ENTER                       'Enterキー押下 2011/12/01 篠原 追加(要望番号513)
        PRINT                       '印刷処理
        OUTPUTPRINT                 'CSV作成・出力
        COA_TOUROKU                 '分析表取り込み
        INKA_CONF_TORIKOMI          '入荷確認ファイル取込   '2012.11.30 追加
        CONF_DEL                    '確認データ削除         '2012.11.30 追加
        BULK_CUST_CHANGE            '荷主一括変更         　'2015.09.07 tsunehira add
        JIKKOU_TRANSFER_COND_M      'M品振替
        JIKKOU_GENPIN_PRINT         '実行(現品票印刷)       'ADD 2019/9/12 依頼番号:007111
        JIKKOU_GENPIN_REPRINT       '実行(現品票再印刷)     'ADD 2019/9/12 依頼番号:007111

    End Enum

#End Region



#Region "EDI荷主INDEX"
    'イベント種別
    Public Enum EdiCustIndex As Integer

        Ncgo32516_00 = 24                   '日本合成化学(名古屋)
        Dupont00089_00 = 3                  'デュポン(テフロン)(千葉)→(横浜)移送 '2012.04.11 ADD
        Dupont00295_00 = 16                 'デュポン(横浜)
        Dupont00331_00 = 34                 'デュポン(DCSE)(横浜)
        Dupont00331_02 = 35                 'デュポン(ABS)(横浜)
        Dupont00588_00 = 36                 'デュポン(SFTP塗料)(横浜)
        Dupont00331_03 = 37                 'デュポン()(横浜)
        Dupont00700_00 = 33                 'デュポン(DCSE)(大阪)
        Dupont00689_00 = 32                 'デュポン(PVFM)(大阪)
        Dupont00300_00 = 15                 'デュポン(EP)(大阪)
        Dow00109_00 = 17                    'ダウケミ(大阪)
        DowTaka00109_01 = 18                'ダウケミ(大阪・高石)
        Toho00275_00 = 26                   '東邦化学(大阪)
        UkimaOsk00856_00 = 38               '浮間合成(大阪)
        Nissan00145_00 = 13                 '日産物流(千葉)
        Nik00171_00 = 39                    '日医工(千葉)
        UkimaSai00856_00 = 1                '浮間合成(岩槻)
        Sumika00952_00 = 2                  '住化カラー(岩槻)
        Fjf00195_00 = 40                    '富士フイルム(千葉)    '2012.08.01 ADD
        Bp00023_00 = 30                     'ビーピー・カストロール(岩槻)    '2012.12.12 ADD
        TSMC75 = 165                        '(熊本)TSMC

    End Enum

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
    Private Const SQL_SELECT_Z_KBN_HANYO As String = _
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
                                     & " VALUE1                                 AS NISUGATA    " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..Z_KBN                        Z_KBN          " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " Z_KBN.KBN_GROUP_CD = @KBN_GROUP_CD                    " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " Z_KBN.KBN_CD   = @KBN_CD                              " & vbNewLine _
                                     & " GROUP BY                                              " & vbNewLine _
                                     & " Z_KBN.VALUE1                                          " & vbNewLine

#End Region

#Region "SQL_SELECT_TABLET_YN_Z_KBN"
    Private Const SQL_SELECT_TABLET_YN_Z_KBN As String = " SELECT                              " & vbNewLine _
                                     & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..Z_KBN                        Z_KBN          " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " Z_KBN.KBN_GROUP_CD = @KBN_GROUP_CD                    " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " Z_KBN.KBN_CD   = @KBN_CD                              " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " Z_KBN.VALUE1   = @VALUE1                              " & vbNewLine
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
                                     & " M_CUST.CUST_CD_SS  = '00'                             " & vbNewLine


#End Region

#Region "SELECT_M_DEST"
    Private Const SQL_SELECT_M_DEST As String = " SELECT                                       " & vbNewLine _
                                     & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_DEST                       M_DEST         " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " M_DEST.NRS_BR_CD   = @NRS_BR_CD                       " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_DEST.CUST_CD_L   = @CUST_CD_L                       " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " M_DEST.DEST_CD     = @OUTKA_MOTO                      " & vbNewLine




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

    'START UMANO 要望番号1302 支払運賃に伴う修正。
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
    'END UMANO 要望番号1302 支払運賃に伴う修正。

#Region "SELECT_M_UNCHIN_TARIFF"
    Private Const SQL_SELECT_M_UNCHIN_TARIFF As String = " SELECT                              " & vbNewLine _
                                     & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_UNCHIN_TARIFF              TARIFF         " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " TARIFF.NRS_BR_CD          = @NRS_BR_CD                " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " TARIFF.UNCHIN_TARIFF_CD   = @UNCHIN_TARIFF_CD         " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " TARIFF.STR_DATE           <= @STR_DATE                " & vbNewLine



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

#Region "SELECT_M_UNCHIN_TARIFF_SET"
    Private Const SQL_SELECT_M_UNCHIN_TARIFF_SET As String = " SELECT                                   " & vbNewLine _
                                     & " YOKO_TARIFF_CD                    AS     YOKO_TARIFF_CD        " & vbNewLine _
                                     & " FROM                                                           " & vbNewLine _
                                     & " $LM_MST$..M_UNCHIN_TARIFF_SET           TARIFF_SET             " & vbNewLine _
                                     & " WHERE                                                          " & vbNewLine _
                                     & " TARIFF_SET.NRS_BR_CD          = @NRS_BR_CD                     " & vbNewLine _
                                     & " AND                                                            " & vbNewLine _
                                     & " TARIFF_SET.CUST_CD_L          = @CUST_CD_L                     " & vbNewLine _
                                     & " AND                                                            " & vbNewLine _
                                     & " TARIFF_SET.CUST_CD_M          = @CUST_CD_M                     " & vbNewLine _
                                     & " AND                                                            " & vbNewLine _
                                     & " TARIFF_SET.TARIFF_BUNRUI_KB   = '40'                           " & vbNewLine _
                                     & " AND                                                            " & vbNewLine _
                                     & " TARIFF_SET.SET_KB             = '02'                           " & vbNewLine




#End Region

#Region "SQL_SELECT_M_TARIFF_BUNRUI"
    Private Const SQL_SELECT_M_TARIFF_BUNRUI As String = " SELECT                                       " & vbNewLine _
                                     & " TARIFF_BUNRUI_KB                  AS   TARIFF_BUNRUI_KB        " & vbNewLine _
                                     & " FROM                                                           " & vbNewLine _
                                     & " $LM_MST$..M_UNCHIN_TARIFF_SET           TARIFF_SET             " & vbNewLine _
                                     & " WHERE                                                          " & vbNewLine _
                                     & " TARIFF_SET.NRS_BR_CD          = @NRS_BR_CD                     " & vbNewLine _
                                     & " AND                                                            " & vbNewLine _
                                     & " TARIFF_SET.CUST_CD_L          = @CUST_CD_L                     " & vbNewLine _
                                     & " AND                                                            " & vbNewLine _
                                     & " TARIFF_SET.CUST_CD_M          = @CUST_CD_M                     " & vbNewLine _
                                     & " AND                                                            " & vbNewLine _
                                     & " TARIFF_SET.SET_KB             = '02'                           " & vbNewLine




#End Region

#Region "SELECT_M_GOODS"
    Private Const SQL_SELECT_M_GOODS As String = " SELECT                                                " & vbNewLine _
                                     & " NRS_BR_CD                         AS     NRS_BR_CD              " & vbNewLine _
                                     & ",GOODS_CD_NRS                      AS     GOODS_CD_NRS           " & vbNewLine _
                                     & ",CUST_CD_L                         AS     CUST_CD_L              " & vbNewLine _
                                     & ",CUST_CD_M                         AS     CUST_CD_M              " & vbNewLine _
                                     & ",CUST_CD_S                         AS     CUST_CD_S              " & vbNewLine _
                                     & ",CUST_CD_SS                        AS     CUST_CD_SS             " & vbNewLine _
                                     & ",GOODS_CD_CUST                     AS     GOODS_CD_CUST          " & vbNewLine _
                                     & ",SEARCH_KEY_1                      AS     SEARCH_KEY_1           " & vbNewLine _
                                     & ",SEARCH_KEY_2                      AS     SEARCH_KEY_2           " & vbNewLine _
                                     & ",CUST_COST_CD1                     AS     CUST_COST_CD1          " & vbNewLine _
                                     & ",CUST_COST_CD2                     AS     CUST_COST_CD2          " & vbNewLine _
                                     & ",JAN_CD                            AS     JAN_CD                 " & vbNewLine _
                                     & ",GOODS_NM_1                        AS     GOODS_NM_1             " & vbNewLine _
                                     & ",GOODS_NM_2                        AS     GOODS_NM_2             " & vbNewLine _
                                     & ",GOODS_NM_3                        AS     GOODS_NM_3             " & vbNewLine _
                                     & ",UP_GP_CD_1                        AS     UP_GP_CD_1             " & vbNewLine _
                                     & ",SHOBO_CD                          AS     SHOBO_CD               " & vbNewLine _
                                     & ",KIKEN_KB                          AS     KIKEN_KB               " & vbNewLine _
                                     & ",UN                                AS     UN                     " & vbNewLine _
                                     & ",PG_KB                             AS     PG_KB                  " & vbNewLine _
                                     & ",CLASS_1                           AS     CLASS_1                " & vbNewLine _
                                     & ",CLASS_2                           AS     CLASS_2                " & vbNewLine _
                                     & ",CLASS_3                           AS     CLASS_3                " & vbNewLine _
                                     & ",CHEM_MTRL_KB                      AS     CHEM_MTRL_KB           " & vbNewLine _
                                     & ",DOKU_KB                           AS     DOKU_KB                " & vbNewLine _
                                     & ",GAS_KANRI_KB                      AS     GAS_KANRI_KB           " & vbNewLine _
                                     & ",ONDO_KB                           AS     ONDO_KB                " & vbNewLine _
                                     & ",UNSO_ONDO_KB                      AS     UNSO_ONDO_KB           " & vbNewLine _
                                     & ",ONDO_MX                           AS     ONDO_MX                " & vbNewLine _
                                     & ",ONDO_MM                           AS     ONDO_MM                " & vbNewLine _
                                     & ",ONDO_STR_DATE                     AS     ONDO_STR_DATE          " & vbNewLine _
                                     & ",ONDO_END_DATE                     AS     ONDO_END_DATE          " & vbNewLine _
                                     & ",ONDO_UNSO_STR_DATE                AS     ONDO_UNSO_STR_DATE     " & vbNewLine _
                                     & ",ONDO_UNSO_END_DATE                AS     ONDO_UNSO_END_DATE     " & vbNewLine _
                                     & ",KYOKAI_GOODS_KB                   AS     KYOKAI_GOODS_KB        " & vbNewLine _
                                     & ",ALCTD_KB                          AS     ALCTD_KB               " & vbNewLine _
                                     & ",NB_UT                             AS     NB_UT                  " & vbNewLine _
                                     & ",PKG_NB                            AS     PKG_NB                 " & vbNewLine _
                                     & ",PKG_UT                            AS     PKG_UT                 " & vbNewLine _
                                     & ",PLT_PER_PKG_UT                    AS     PLT_PER_PKG_UT         " & vbNewLine _
                                     & ",STD_IRIME_NB                      AS     STD_IRIME_NB           " & vbNewLine _
                                     & ",STD_IRIME_UT                      AS     STD_IRIME_UT           " & vbNewLine _
                                     & ",STD_WT_KGS                        AS     STD_WT_KGS             " & vbNewLine _
                                     & ",STD_CBM                           AS     STD_CBM                " & vbNewLine _
                                     & ",INKA_KAKO_SAGYO_KB_1              AS     INKA_KAKO_SAGYO_KB_1   " & vbNewLine _
                                     & ",INKA_KAKO_SAGYO_KB_2              AS     INKA_KAKO_SAGYO_KB_2   " & vbNewLine _
                                     & ",INKA_KAKO_SAGYO_KB_3              AS     INKA_KAKO_SAGYO_KB_3   " & vbNewLine _
                                     & ",INKA_KAKO_SAGYO_KB_4              AS     INKA_KAKO_SAGYO_KB_4   " & vbNewLine _
                                     & ",INKA_KAKO_SAGYO_KB_5              AS     INKA_KAKO_SAGYO_KB_5   " & vbNewLine _
                                     & ",OUTKA_KAKO_SAGYO_KB_1             AS     OUTKA_KAKO_SAGYO_KB_1  " & vbNewLine _
                                     & ",OUTKA_KAKO_SAGYO_KB_2             AS     OUTKA_KAKO_SAGYO_KB_2  " & vbNewLine _
                                     & ",OUTKA_KAKO_SAGYO_KB_3             AS     OUTKA_KAKO_SAGYO_KB_3  " & vbNewLine _
                                     & ",OUTKA_KAKO_SAGYO_KB_4             AS     OUTKA_KAKO_SAGYO_KB_4  " & vbNewLine _
                                     & ",OUTKA_KAKO_SAGYO_KB_5             AS     OUTKA_KAKO_SAGYO_KB_5  " & vbNewLine _
                                     & ",PKG_SAGYO                         AS     PKG_SAGYO              " & vbNewLine _
                                     & ",TARE_YN                           AS     TARE_YN                " & vbNewLine _
                                     & ",SP_NHS_YN                         AS     SP_NHS_YN              " & vbNewLine _
                                     & ",COA_YN                            AS     COA_YN                 " & vbNewLine _
                                     & ",LOT_CTL_KB                        AS     LOT_CTL_KB             " & vbNewLine _
                                     & ",LT_DATE_CTL_KB                    AS     LT_DATE_CTL_KB         " & vbNewLine _
                                     & ",CRT_DATE_CTL_KB                   AS     CRT_DATE_CTL_KB        " & vbNewLine _
                                     & ",DEF_SPD_KB                        AS     DEF_SPD_KB             " & vbNewLine _
                                     & ",KITAKU_AM_UT_KB                   AS     KITAKU_AM_UT_KB        " & vbNewLine _
                                     & ",KITAKU_GOODS_UP                   AS     KITAKU_GOODS_UP        " & vbNewLine _
                                     & ",ORDER_KB                          AS     ORDER_KB               " & vbNewLine _
                                     & ",ORDER_NB                          AS     ORDER_NB               " & vbNewLine _
                                     & ",SHIP_CD_L                         AS     SHIP_CD_L              " & vbNewLine _
                                     & ",SKYU_MEI_YN                       AS     SKYU_MEI_YN            " & vbNewLine _
                                     & ",HIKIATE_ALERT_YN                  AS     HIKIATE_ALERT_YN       " & vbNewLine _
                                     & ",OUTKA_ATT                         AS     OUTKA_ATT              " & vbNewLine _
                                     & ",PRINT_NB                          AS     PRINT_NB               " & vbNewLine _
                                     & ",CONSUME_PERIOD_DATE               AS     CONSUME_PERIOD_DATE    " & vbNewLine _
                                     & " FROM                                                            " & vbNewLine _
                                     & " $LM_MST$..M_GOODS                      M_GOODS                  " & vbNewLine

#End Region

    '要望番号1003 2012.05.08 追加START
    '商品明細マスタより置場情報を取得(サブ区分="02")セット内容)
#Region "SELECT_M_GOODS_DETAILS"

    Private Const SQL_SELECT_M_GOODS_DETAILS As String = " SELECT                                     " & vbNewLine _
                               & " M_GOODS_DETAILS.SET_NAIYO                   AS SET_NAIYO          " & vbNewLine _
                               & " FROM                                                              " & vbNewLine _
                               & " $LM_MST$..M_GOODS_DETAILS     M_GOODS_DETAILS                     " & vbNewLine _
                               & " WHERE                                                             " & vbNewLine _
                               & " M_GOODS_DETAILS.GOODS_CD_NRS = @GOODS_CD_NRS                      " & vbNewLine _
                               & " AND                                                               " & vbNewLine _
                               & " M_GOODS_DETAILS.SUB_KB = '02'                                     " & vbNewLine _
                               & " AND                                                               " & vbNewLine _
                               & " M_GOODS_DETAILS.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                               & " AND                                                               " & vbNewLine _
                               & " M_GOODS_DETAILS.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine

#End Region

    '要望番号1003 2012.05.08 追加END

    '2015.08.24 BYK セミEDI対応START

#Region "SELECT_M_CUST_DETAILS"

    Private Const SQL_SELECT_M_CUST_DETAILS As String = " SELECT                                     " & vbNewLine _
                               & " M_CUST_DETAILS.SET_NAIYO                   AS SET_NAIYO          " & vbNewLine _
                               & " FROM                                                              " & vbNewLine _
                               & " $LM_MST$..M_CUST_DETAILS     M_CUST_DETAILS                     " & vbNewLine _
                               & " WHERE                                                             " & vbNewLine _
                               & " M_CUST_DETAILS.NRS_BR_CD = @NRS_BR_CD                            " & vbNewLine _
                               & " AND                                                               " & vbNewLine _
                               & " M_CUST_DETAILS.CUST_CD = @CUST_CD_L + @CUST_CD_M                     " & vbNewLine _
                               & " AND                                                               " & vbNewLine _
                               & " M_CUST_DETAILS.SUB_KB = '0C'                                     " & vbNewLine _
                               & " AND                                                               " & vbNewLine _
                               & " M_CUST_DETAILS.SYS_DEL_FLG = '0'                                 " & vbNewLine

#End Region

#Region "SELECT_M_HOL"
    Private Const SQL_SELECT_M_HOL As String = " SELECT                                        " & vbNewLine _
                                     & " COUNT(*)                               AS MST_CNT     " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..M_HOL                        AS M_HOL       " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " M_HOL.HOL = @HOL                                      " & vbNewLine
#End Region
    '2015.08.24 BYK セミEDI対応END

#Region "SELECT_INKA_L"
    Private Const SQL_SELECT_INKA_L As String = " SELECT                                       " & vbNewLine _
                                     & " COUNT(*)                               AS INKA_L_CNT  " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_TRN$..B_INKA_L                     B_INKA_L       " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " B_INKA_L.SYS_DEL_FLG    = '0'                          " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " B_INKA_L.OUTKA_FROM_ORD_NO_L = @OUTKA_FROM_ORD_NO       " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " B_INKA_L.NRS_BR_CD         = @NRS_BR_CD               " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " B_INKA_L.CUST_CD_L         = @CUST_CD_L               " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " B_INKA_L.CUST_CD_M         = @CUST_CD_M               " & vbNewLine





#End Region


#Region "検索カウント"

    ''' <summary>
    ''' 検索カウント
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_COUNT_DATA As String = " SELECT                                               " & vbNewLine _
                                          & " COUNT(H_INKAEDI_L.EDI_CTL_NO)            AS SELECT_CNT " & vbNewLine

#End Region '検索

    '2012.02.25 大阪対応 START
    '2015.09.07 tsunehira add
#Region "検索SELECT"

    ''' <summary>
    ''' 検索SELECT
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                     " & vbNewLine _
                                            & " H_INKAEDI_L.NRS_BR_CD                               AS NRS_BR_CD           " & vbNewLine _
                                            & ",H_INKAEDI_L.NRS_WH_CD                               AS NRS_WH_CD           " & vbNewLine _
                                            & ",CASE WHEN H_INKAEDI_L.OUTKA_FROM_ORD_NO IS NULL THEN H_INKAEDI_M.OUTKA_FROM_ORD_NO " & vbNewLine _
                                            & "      ELSE H_INKAEDI_L.OUTKA_FROM_ORD_NO                                    " & vbNewLine _
                                            & " END                                                 AS OUTKA_FROM_ORD_NO   " & vbNewLine _
                                            & ",Z1.KBN_NM1                                          AS INKA_STATE_NM       " & vbNewLine _
                                            & ",CASE WHEN B_INKA_L.INKA_DATE IS NULL THEN H_INKAEDI_L.INKA_DATE            " & vbNewLine _
                                            & "      ELSE B_INKA_L.INKA_DATE                                               " & vbNewLine _
                                            & " END                                                 AS INKA_DATE           " & vbNewLine _
                                            & ",M_CUST.CUST_NM_L + '　' + M_CUST.CUST_NM_M          AS CUST_NM             " & vbNewLine _
                                            & "--,M_GOODS.GOODS_NM_1                                AS GOODS_NM	           " & vbNewLine _
                                            & ",H_INKAEDI_M.GOODS_NM                                AS GOODS_NM	           " & vbNewLine _
                                            & ",H_INKAEDI_M.NB                                      AS NB       	       " & vbNewLine _
                                            & ",CASE WHEN B_INKA_L.INKA_TTL_NB IS NULL THEN H_INKAEDI_L.INKA_TTL_NB        " & vbNewLine _
                                            & "      ELSE B_INKA_L.INKA_TTL_NB                                             " & vbNewLine _
                                            & " END                                                 AS INKA_TTL_NB         " & vbNewLine _
                                            & ",CASE WHEN H_INKAEDI_L.SYS_DEL_FLG = '1'                                    " & vbNewLine _
                                            & " THEN ISNULL(MCNT.RECCNT,0)                                        	       " & vbNewLine _
                                            & " ELSE ISNULL(MCNTSYS.RECCNT,0)                                        	   " & vbNewLine _
                                            & " END                                                 AS RECCNT	           " & vbNewLine _
                                            & ",Z2.KBN_NM1                                          AS UNSO_KB	           " & vbNewLine _
                                            & ",M_UNSOCO.UNSOCO_NM + '　' + M_UNSOCO.UNSOCO_BR_NM    AS UNSOCO_NM	       " & vbNewLine _
                                            & ",H_INKAEDI_L.OUTKA_MOTO                              AS OUTKA_MOTO          " & vbNewLine _
                                            & ",M_DEST.DEST_NM                                      AS OUTKA_MOTO_NM       " & vbNewLine _
                                            & ",H_INKAEDI_L.EDI_CTL_NO                              AS EDI_CTL_NO          " & vbNewLine _
                                            & ",CASE WHEN SUBSTRING(H_INKAEDI_L.FREE_C30,5,8) <> '00000000'                " & vbNewLine _
                                            & " THEN SUBSTRING(H_INKAEDI_L.FREE_C30,4,9)                                   " & vbNewLine _
                                            & " ELSE ''                                                                    " & vbNewLine _
                                            & " END                                                 AS MATOME_NO           " & vbNewLine _
                                            & ",H_INKAEDI_L.INKA_CTL_NO_L                           AS INKA_CTL_NO_L       " & vbNewLine _
                                            & ",H_INKAEDI_L.BUYER_ORD_NO                            AS BUYER_ORD_NO_L      " & vbNewLine _
                                            & ",Z3.KBN_NM1                                          AS INKA_TP_NM	       " & vbNewLine _
                                            & ",H_INKAEDI_L.FREE_C30                                AS FREE_C30            " & vbNewLine _
                                            & ",H_INKAEDI_L.CRT_DATE                                AS CRT_DATE  	       " & vbNewLine _
                                            & ",H_INKAEDI_L.CRT_TIME                                AS CRT_TIME  	       " & vbNewLine _
                                            & ",SENDTBL.SEND_DATE                                   AS SEND_DATE  	       " & vbNewLine _
                                            & ",SENDTBL.SEND_TIME                                   AS SEND_TIME  	       " & vbNewLine _
                                            & ",TANTO_USER.USER_NM                                  AS TANTO_USER  	       " & vbNewLine _
                                            & ",ENT_USER.USER_NM                                    AS CRT_USER            " & vbNewLine _
                                            & ",UPD_USER.USER_NM                                    AS SYS_UPD_USER        " & vbNewLine _
                                            & ",H_INKAEDI_L.SYS_UPD_DATE                            AS SYS_UPD_DATE	       " & vbNewLine _
                                            & ",H_INKAEDI_L.SYS_UPD_TIME                            AS SYS_UPD_TIME	       " & vbNewLine _
                                            & ",H_INKAEDI_L.DEL_KB                                  AS DEL_KB   	       " & vbNewLine _
                                            & ",H_INKAEDI_L.JISSEKI_FLAG                            AS JISSEKI_FLAG        " & vbNewLine _
                                            & ",H_INKAEDI_L.OUT_FLAG                                AS OUT_FLAG            " & vbNewLine _
                                            & ",H_INKAEDI_M2.MIN_NB                                 AS MIN_NB              " & vbNewLine _
                                            & ",H_INKAEDI_M2.MAX_AKAKURO_KB                         AS MAX_AKAKURO_KB      " & vbNewLine _
                                            & ",M_EDI_CUST.FLAG_01                                  AS EDI_CUST_JISSEKI    " & vbNewLine _
                                            & ",M_EDI_CUST.FLAG_07                                  AS EDI_CUST_MATOME     " & vbNewLine _
                                            & ",M_EDI_CUST.FLAG_12                                  AS EDI_CUST_SPECIAL    " & vbNewLine _
                                            & ",M_EDI_CUST.FLAG_13                                  AS EDI_CUST_HOLDOUT    " & vbNewLine _
                                            & ",M_EDI_CUST.FLAG_14                                  AS EDI_CUST_UNSO       " & vbNewLine _
                                            & ",M_EDI_CUST.EDI_CUST_INDEX                           AS EDI_CUST_INDEX      " & vbNewLine _
                                            & ",SENDTBL.SYS_UPD_DATE                                AS SND_UPD_DATE        " & vbNewLine _
                                            & ",SENDTBL.SYS_UPD_TIME                                AS SND_UPD_TIME        " & vbNewLine _
                                            & ",H_INKAEDI_HED.SYS_UPD_DATE                          AS RCV_UPD_DATE	       " & vbNewLine _
                                            & ",H_INKAEDI_HED.SYS_UPD_TIME                          AS RCV_UPD_TIME	       " & vbNewLine _
                                            & ",B_INKA_L.SYS_UPD_DATE                               AS INKA_UPD_DATE       " & vbNewLine _
                                            & ",B_INKA_L.SYS_UPD_TIME                               AS INKA_UPD_TIME       " & vbNewLine _
                                            & ",Z4.KBN_NM1                                          AS JYOTAI	           " & vbNewLine _
                                            & ",Z5.KBN_NM1                                          AS HORYU	           " & vbNewLine _
                                            & ",B_INKA_L.INKA_STATE_KB                              AS INKA_STATE_KB       " & vbNewLine _
                                            & ",H_INKAEDI_L.SYS_DEL_FLG                             AS SYS_DEL_FLG         " & vbNewLine _
                                            & ",B_INKA_L.SYS_DEL_FLG                                AS INKA_DEL_FLG        " & vbNewLine _
                                            & ",H_INKAEDI_L.CUST_CD_L                               AS CUST_CD_L           " & vbNewLine _
                                            & ",H_INKAEDI_L.CUST_CD_M                               AS CUST_CD_M           " & vbNewLine _
                                            & ",M_EDI_CUST.ORDER_CHECK_FLG                          AS ORDER_CHECK_FLG     " & vbNewLine _
                                            & ",M_EDI_CUST.AUTO_MATOME_FLG                          AS AUTO_MATOME_FLG     " & vbNewLine _
                                            & ",M_EDI_CUST.RCV_NM_HED                               AS RCV_NM_HED          " & vbNewLine _
                                            & ",M_EDI_CUST.RCV_NM_DTL                               AS RCV_NM_DTL          " & vbNewLine _
                                            & ",M_EDI_CUST.RCV_NM_EXT                               AS RCV_NM_EXT          " & vbNewLine _
                                            & ",M_EDI_CUST.SND_NM                                   AS SND_NM              " & vbNewLine _
                                            & ",M_EDI_CUST.FLAG_16                                  AS EDI_CUST_INOUTFLG   " & vbNewLine _
                                            & "--,RCV_DTL.SYS_UPD_DATE                                AS RCV_DTL_UPD_DATE    " & vbNewLine _
                                            & "--,RCV_DTL.SYS_UPD_TIME                                AS RCV_DTL_UPD_TIME    " & vbNewLine _
                                            & ",M_EDI_CUST.DATA_01                                  AS CHG_CUST_CD  --'015.09.07 tsunehira add   " & vbNewLine _
                                            & ",ISNULL(H_INKAEDI_M3.KOSU_FLG,'0')                   AS KOSU_FLG     --add 2017/05/09             " & vbNewLine _
                                            & ",ISNULL(H_INKAEDI_M4.EDI_CTL_NO,'OK')                AS GENPINHYO_CHKFLG   --ADD 2019/12/18 009991" & vbNewLine


    '受信DTL排他用コメントアウト
    '2012.02.25 大阪対応 END


    ''' <summary>
    ''' 検索項目追加(M品振替出荷管理番号あり)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_ADD_COND_M_ON As String = _
        ", ISNULL(COND_M.OUTKA_NO_L, '')                        AS OUTKA_CTL_NO_COND_M  " & vbNewLine


    ''' <summary>
    ''' 検索項目追加(M品振替出荷管理番号なし)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_ADD_COND_M_OFF As String = _
        ", ''                                                   AS OUTKA_CTL_NO_COND_M " & vbNewLine


#End Region   '検索

#Region "検索FROM句"

    ''' <summary>
    ''' 検索FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM As String = "FROM                                                  " & vbNewLine _
                                     & "$LM_TRN$..H_INKAEDI_L                                        " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "$LM_TRN$..H_INKAEDI_M                  H_INKAEDI_M           " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "H_INKAEDI_L.NRS_BR_CD  = H_INKAEDI_M.NRS_BR_CD               " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_L.EDI_CTL_NO = H_INKAEDI_M.EDI_CTL_NO              " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_M.EDI_CTL_NO_CHU = '001'                           " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "(                                                            " & vbNewLine _
                                     & "SELECT                                                       " & vbNewLine _
                                     & "H_INKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                     & ",H_INKAEDI_M.EDI_CTL_NO                                      " & vbNewLine _
                                     & ",MIN(H_INKAEDI_M.NB) AS MIN_NB                               " & vbNewLine _
                                     & ",MAX(H_INKAEDI_M.AKAKURO_KB) AS MAX_AKAKURO_KB               " & vbNewLine _
                                     & "FROM                                                         " & vbNewLine _
                                     & "$LM_TRN$..H_INKAEDI_M                 H_INKAEDI_M            " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "$LM_TRN$..H_INKAEDI_L                 H_INKAEDI_L            " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "H_INKAEDI_L.NRS_BR_CD  = H_INKAEDI_M.NRS_BR_CD               " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_L.EDI_CTL_NO = H_INKAEDI_M.EDI_CTL_NO              " & vbNewLine _
                                     & "WHERE                                                        " & vbNewLine _
                                     & "H_INKAEDI_M.NRS_BR_CD  = @NRS_BR_CD                          " & vbNewLine _
                                     & "AND    (H_INKAEDI_L.DEL_KB = '1'                             " & vbNewLine _
                                     & "OR     (H_INKAEDI_L.DEL_KB <> '1'                            " & vbNewLine _
                                     & "AND    H_INKAEDI_M.DEL_KB <> '1'))                           " & vbNewLine _
                                     & "AND    H_INKAEDI_M.OUT_KB <> '1'                             " & vbNewLine _
                                     & "GROUP BY                                                     " & vbNewLine _
                                     & "H_INKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                     & ",H_INKAEDI_M.EDI_CTL_NO                                      " & vbNewLine _
                                     & ") H_INKAEDI_M2                                               " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "H_INKAEDI_L.NRS_BR_CD  = H_INKAEDI_M2.NRS_BR_CD              " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_L.EDI_CTL_NO = H_INKAEDI_M2.EDI_CTL_NO             " & vbNewLine _
                                     & "--ADD 2017/05/09 Start                                                          " & vbNewLine _
                                     & "LEFT JOIN                                                                       " & vbNewLine _
                                     & " (                                                                              " & vbNewLine _
                                     & "  SELECT                                                                        " & vbNewLine _
                                     & "     H_INKAEDI_M.NRS_BR_CD                                                      " & vbNewLine _
                                     & "    ,H_INKAEDI_M.EDI_CTL_NO                                                     " & vbNewLine _
                                     & "    --アクサルタで入荷E未登録でFREE_C30 = 'KOSU0'                               " & vbNewLine _
                                     & "    ,MAX(CASE WHEN H_INKAEDI_M.FREE_C30 = 'KOSU0' AND H_INKAEDI_M.INKA_CTL_NO_L = ''  " & vbNewLine _
                                     & "                   AND H_INKAEDI_M.NB = 1 AND H_INKAEDI_M.SYS_DEL_FLG = '0' 　        " & vbNewLine _
                                     & "              THEN CASE WHEN H_INKAEDI_M.STD_IRIME <>  0                              " & vbNewLine _
                                     & "                        THEN                                                          " & vbNewLine _
                                     & "                          CASE WHEN FLOOR(H_INKAEDI_M.FREE_N02 / H_INKAEDI_M.STD_IRIME) = CEILING(H_INKAEDI_M.FREE_N02 / H_INKAEDI_M.STD_IRIME)     " & vbNewLine _
                                     & "                               THEN '0'                                         " & vbNewLine _
                                     & "                               ELSE '1'  END                                    " & vbNewLine _
                                     & "                        ELSE '0'   END                                          " & vbNewLine _
                                     & "              ELSE '0'  END)  AS KOSU_FLG                                       " & vbNewLine _
                                     & "  FROM                                                                          " & vbNewLine _
                                     & "    $LM_TRN$..H_INKAEDI_M                 H_INKAEDI_M                           " & vbNewLine _
                                     & "  WHERE                                                                         " & vbNewLine _
                                     & "      H_INKAEDI_M.NRS_BR_CD  = @NRS_BR_CD                                       " & vbNewLine _
                                     & "--  AND H_INKAEDI_M.DEL_KB = '0'                                                  " & vbNewLine _
                                     & "  GROUP BY                                                                      " & vbNewLine _
                                     & "      H_INKAEDI_M.NRS_BR_CD                                                     " & vbNewLine _
                                     & "     ,H_INKAEDI_M.EDI_CTL_NO                                                    " & vbNewLine _
                                     & "  ) H_INKAEDI_M3                                                                " & vbNewLine _
                                     & " ON                                                                             " & vbNewLine _
                                     & "    H_INKAEDI_L.NRS_BR_CD  = H_INKAEDI_M3.NRS_BR_CD                             " & vbNewLine _
                                     & "    AND                                                                         " & vbNewLine _
                                     & "    H_INKAEDI_L.EDI_CTL_NO = H_INKAEDI_M3.EDI_CTL_NO                            " & vbNewLine _
                                     & "--ADD 2017/05/09 End                                                            " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "$LM_TRN$..B_INKA_L                    B_INKA_L               " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "H_INKAEDI_L.NRS_BR_CD  = B_INKA_L.NRS_BR_CD                  " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_L.INKA_CTL_NO_L = B_INKA_L.INKA_NO_L               " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "$LM_MST$..M_EDI_CUST                  M_EDI_CUST             " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "H_INKAEDI_L.NRS_BR_CD = M_EDI_CUST.NRS_BR_CD                 " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_L.CUST_CD_L = M_EDI_CUST.CUST_CD_L                 " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_L.CUST_CD_M = M_EDI_CUST.CUST_CD_M                 " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_L.NRS_WH_CD = M_EDI_CUST.WH_CD                     " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "M_EDI_CUST.INOUT_KB   = '1'                                  " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_L.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "$LM_MST$..Z_KBN                        Z1                    " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "B_INKA_L.INKA_STATE_KB = Z1.KBN_CD                           " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "Z1.KBN_GROUP_CD = 'N004'                                     " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "$LM_MST$..Z_KBN                        Z2                    " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "H_INKAEDI_L.UNCHIN_KB = Z2.KBN_CD                            " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "Z2.KBN_GROUP_CD = 'T015'                                     " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "$LM_MST$..Z_KBN                        Z3                    " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "H_INKAEDI_L.INKA_TP = Z3.KBN_CD                              " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "Z3.KBN_GROUP_CD = 'N007'                                     " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "$LM_MST$..M_CUST                       M_CUST                " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "H_INKAEDI_L.NRS_BR_CD = M_CUST.NRS_BR_CD                     " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_L.CUST_CD_L = M_CUST.CUST_CD_L                     " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_L.CUST_CD_M = M_CUST.CUST_CD_M                     " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "M_CUST.CUST_CD_S = '00'                                      " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "M_CUST.CUST_CD_SS = '00'                                     " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_L.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "$LM_MST$..M_DEST                       M_DEST                " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "H_INKAEDI_L.NRS_BR_CD = M_DEST.NRS_BR_CD                     " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_L.CUST_CD_L = M_DEST.CUST_CD_L                     " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_L.OUTKA_MOTO = M_DEST.DEST_CD                      " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_L.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "$LM_MST$..M_GOODS                       M_GOODS              " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "H_INKAEDI_M.NRS_BR_CD = M_GOODS.NRS_BR_CD                    " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_M.NRS_GOODS_CD = M_GOODS.GOODS_CD_NRS              " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_M.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "$LM_MST$..M_UNSOCO                 M_UNSOCO                  " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "H_INKAEDI_L.NRS_BR_CD = M_UNSOCO.NRS_BR_CD                   " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_L.UNSO_CD = M_UNSOCO.UNSOCO_CD                     " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_L.UNSO_BR_CD = M_UNSOCO.UNSOCO_BR_CD               " & vbNewLine _
                                     & "-- 要望番号1756 yamanaka 2013.03.01 START                    " & vbNewLine _
                                     & "--LEFT JOIN                                                  " & vbNewLine _
                                     & "--(                                                          " & vbNewLine _
                                     & "--SELECT                                                     " & vbNewLine _
                                     & "--M_TCUST.CUST_CD_L AS CUST_CD_L                             " & vbNewLine _
                                     & "--,MIN(USER_CD) AS USER_CD                                   " & vbNewLine _
                                     & "--FROM                                                       " & vbNewLine _
                                     & "--$LM_MST$..M_TCUST              M_TCUST                     " & vbNewLine _
                                     & "--GROUP BY                                                   " & vbNewLine _
                                     & "--M_TCUST.CUST_CD_L                                          " & vbNewLine _
                                     & "--) M_TCUST                                                  " & vbNewLine _
                                     & "--ON                                                         " & vbNewLine _
                                     & "--H_INKAEDI_L.CUST_CD_L = M_TCUST.CUST_CD_L                  " & vbNewLine _
                                     & "--LEFT JOIN                                                  " & vbNewLine _
                                     & "--$LM_MST$..S_USER               TANTO_USER                  " & vbNewLine _
                                     & "--ON                                                         " & vbNewLine _
                                     & "--M_TCUST.USER_CD = TANTO_USER.USER_CD                       " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "$LM_MST$..S_USER               TANTO_USER                    " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "M_CUST.TANTO_CD = TANTO_USER.USER_CD                         " & vbNewLine _
                                     & "-- 要望番号1756 yamanaka 2013.03.01 END                      " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "$LM_MST$..S_USER               ENT_USER                      " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "H_INKAEDI_L.SYS_ENT_USER = ENT_USER.USER_CD                  " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "$LM_MST$..S_USER               UPD_USER                      " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "H_INKAEDI_L.SYS_UPD_USER = UPD_USER.USER_CD                  " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "(                                                            " & vbNewLine _
                                     & "SELECT                                                       " & vbNewLine _
                                     & "H_INKAEDI_M.EDI_CTL_NO                                       " & vbNewLine _
                                     & ",COUNT(H_INKAEDI_M.EDI_CTL_NO_CHU)   AS  RECCNT              " & vbNewLine _
                                     & "FROM                                                         " & vbNewLine _
                                     & "$LM_TRN$..H_INKAEDI_M   H_INKAEDI_M                          " & vbNewLine _
                                     & "WHERE                                                        " & vbNewLine _
                                     & "H_INKAEDI_M.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                     & "GROUP BY                                                     " & vbNewLine _
                                     & "H_INKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                     & ",H_INKAEDI_M.EDI_CTL_NO                                      " & vbNewLine _
                                     & ")                            MCNT                            " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "H_INKAEDI_L.EDI_CTL_NO = MCNT.EDI_CTL_NO                     " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "(                                                            " & vbNewLine _
                                     & "SELECT                                                       " & vbNewLine _
                                     & "H_INKAEDI_M.EDI_CTL_NO                                       " & vbNewLine _
                                     & ",COUNT(H_INKAEDI_M.EDI_CTL_NO_CHU)   AS  RECCNT              " & vbNewLine _
                                     & "FROM                                                         " & vbNewLine _
                                     & "$LM_TRN$..H_INKAEDI_M   H_INKAEDI_M                          " & vbNewLine _
                                     & "WHERE                                                        " & vbNewLine _
                                     & "H_INKAEDI_M.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "H_INKAEDI_M.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                     & "GROUP BY                                                     " & vbNewLine _
                                     & "H_INKAEDI_M.NRS_BR_CD                                        " & vbNewLine _
                                     & ",H_INKAEDI_M.EDI_CTL_NO                                      " & vbNewLine _
                                     & ")                            MCNTSYS                         " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "H_INKAEDI_L.EDI_CTL_NO = MCNTSYS.EDI_CTL_NO                  " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "$LM_MST$..Z_KBN                        Z4                    " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "H_INKAEDI_L.SYS_DEL_FLG = Z4.KBN_CD                          " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "Z4.KBN_GROUP_CD = 'S051'                                     " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & "$LM_MST$..Z_KBN                        Z5                    " & vbNewLine _
                                     & "ON                                                           " & vbNewLine _
                                     & "H_INKAEDI_L.DEL_KB = Z5.KBN_CD                               " & vbNewLine _
                                     & "AND                                                          " & vbNewLine _
                                     & "Z5.KBN_GROUP_CD = 'E011'                                     " & vbNewLine _
                                     & "--ADD 2019/12/18 00991 Start                                 " & vbNewLine _
                                     & "LEFT JOIN                                                    " & vbNewLine _
                                     & " (                                                           " & vbNewLine _
                                     & "  SELECT                                                     " & vbNewLine _
                                     & "     H_INKAEDI_M.NRS_BR_CD                                   " & vbNewLine _
                                     & "    ,H_INKAEDI_M.EDI_CTL_NO                                  " & vbNewLine _
                                     & "  FROM                                                       " & vbNewLine _
                                     & "    LM_TRN..H_INKAEDI_M                 H_INKAEDI_M          " & vbNewLine _
                                     & "  WHERE                                                      " & vbNewLine _
                                     & "       H_INKAEDI_M.NRS_BR_CD  = @NRS_BR_CD                   " & vbNewLine _
                                     & "   AND (NB * IRIME) <> FREE_N01                              " & vbNewLine _
                                     & "  GROUP BY                                                   " & vbNewLine _
                                     & "      H_INKAEDI_M.NRS_BR_CD                                  " & vbNewLine _
                                     & "     ,H_INKAEDI_M.EDI_CTL_NO                                 " & vbNewLine _
                                     & "  ) H_INKAEDI_M4                                             " & vbNewLine _
                                     & " ON                                                          " & vbNewLine _
                                     & "     H_INKAEDI_L.NRS_BR_CD  = H_INKAEDI_M4.NRS_BR_CD         " & vbNewLine _
                                     & " AND H_INKAEDI_L.EDI_CTL_NO = H_INKAEDI_M4.EDI_CTL_NO        " & vbNewLine _
                                     & "--ADD 2019/12/18 00991 Start                                 " & vbNewLine


    ''' <summary>
    ''' FROM追加(M品振替出荷)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_ADD_COND_M_ON As String _
        = " LEFT JOIN $LM_TRN$..C_OUTKA_L AS COND_M         " & vbNewLine _
        & "   ON COND_M.SYS_DEL_FLG = '0'                   " & vbNewLine _
        & "  AND COND_M.NRS_BR_CD   = H_INKAEDI_L.NRS_BR_CD " & vbNewLine _
        & "  AND COND_M.OUTKA_NO_L  = H_INKAEDI_L.FREE_C24  " & vbNewLine

    Private Const SQL_FROM_SENDTABLE As String = "LEFT JOIN                                      " & vbNewLine _
                                 & "(                                                            " & vbNewLine _
                                 & "SELECT                                                       " & vbNewLine _
                                 & "H_SENDINEDI.EDI_CTL_NO                                       " & vbNewLine _
                                 & ",H_SENDINEDI.INOUT_KB AS INOUT_KB                            " & vbNewLine _
                                 & ",MAX(H_SENDINEDI.SEND_DATE) AS SEND_DATE                     " & vbNewLine _
                                 & ",MAX(H_SENDINEDI.SEND_TIME) AS SEND_TIME                     " & vbNewLine _
                                 & ",MAX(H_SENDINEDI.SYS_UPD_DATE) AS SYS_UPD_DATE               " & vbNewLine _
                                 & ",MAX(H_SENDINEDI.SYS_UPD_TIME) AS SYS_UPD_TIME               " & vbNewLine _
                                 & "FROM                                                         " & vbNewLine _
                                 & "$LM_TRN$..$SEND_TBL$                 H_SENDINEDI             " & vbNewLine _
                                 & "WHERE                                                        " & vbNewLine _
                                 & "H_SENDINEDI.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                 & "GROUP BY                                                     " & vbNewLine _
                                 & "H_SENDINEDI.EDI_CTL_NO                                       " & vbNewLine _
                                 & ",H_SENDINEDI.INOUT_KB                                        " & vbNewLine _
                                 & ")                                     SENDTBL                " & vbNewLine _
                                 & "ON                                                           " & vbNewLine _
                                 & "H_INKAEDI_L.EDI_CTL_NO = SENDTBL.EDI_CTL_NO                  " & vbNewLine

    Private Const SQL_FROM_SENDTABLE_NORMAL As String = "LEFT JOIN                           " & vbNewLine _
                             & "(                                                            " & vbNewLine _
                             & "SELECT                                                       " & vbNewLine _
                             & "H_SENDINEDI.EDI_CTL_NO                                       " & vbNewLine _
                             & ",MAX(H_SENDINEDI.SEND_DATE) AS SEND_DATE                     " & vbNewLine _
                             & ",MAX(H_SENDINEDI.SEND_TIME) AS SEND_TIME                     " & vbNewLine _
                             & ",MAX(H_SENDINEDI.SYS_UPD_DATE) AS SYS_UPD_DATE               " & vbNewLine _
                             & ",MAX(H_SENDINEDI.SYS_UPD_TIME) AS SYS_UPD_TIME               " & vbNewLine _
                             & "FROM                                                         " & vbNewLine _
                             & "$LM_TRN$..$SEND_TBL$                 H_SENDINEDI             " & vbNewLine _
                             & "WHERE                                                        " & vbNewLine _
                             & "H_SENDINEDI.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                             & "GROUP BY                                                     " & vbNewLine _
                             & "H_SENDINEDI.EDI_CTL_NO                                       " & vbNewLine _
                             & ")                                     SENDTBL                " & vbNewLine _
                             & "ON                                                           " & vbNewLine _
                             & "H_INKAEDI_L.EDI_CTL_NO = SENDTBL.EDI_CTL_NO                  " & vbNewLine


    Private Const SQL_FROM_SENDTABLE_NULL As String = "LEFT JOIN                                 " & vbNewLine _
                                 & "(                                                            " & vbNewLine _
                                 & "SELECT                                                       " & vbNewLine _
                                 & " H_INKAEDI_L.EDI_CTL_NO AS EDI_CTL_NO                        " & vbNewLine _
                                 & ",'' AS SEND_DATE                                             " & vbNewLine _
                                 & ",'' AS SEND_TIME                                             " & vbNewLine _
                                 & ",'' AS SYS_UPD_DATE                                          " & vbNewLine _
                                 & ",'' AS SYS_UPD_TIME                                          " & vbNewLine _
                                 & "FROM                                                         " & vbNewLine _
                                 & "$LM_TRN$..H_INKAEDI_L                 H_INKAEDI_L            " & vbNewLine _
                                 & "WHERE                                                        " & vbNewLine _
                                 & "H_INKAEDI_L.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                 & ")                                     SENDTBL                " & vbNewLine _
                                 & "ON                                                           " & vbNewLine _
                                 & "SENDTBL.EDI_CTL_NO = H_INKAEDI_L.EDI_CTL_NO                  " & vbNewLine

    '2012.02.25 大阪対応 START
    Private Const SQL_FROM_SENDTABLE_INOUT As String = "AND                                      " & vbNewLine _
                                & "SENDTBL.INOUT_KB = '1'                                        " & vbNewLine
    '2012.02.25 大阪対応 END

    Private Const SQL_FROM_RCV_HED As String = "LEFT JOIN                                       " & vbNewLine _
                                 & "$LM_TRN$..$RCV_HED$        H_INKAEDI_HED                     " & vbNewLine _
                                 & "ON                                                           " & vbNewLine _
                                 & "H_INKAEDI_L.NRS_BR_CD  = H_INKAEDI_HED.NRS_BR_CD             " & vbNewLine _
                                 & "AND                                                          " & vbNewLine _
                                 & "H_INKAEDI_L.CRT_DATE  = H_INKAEDI_HED.CRT_DATE               " & vbNewLine _
                                 & "AND                                                          " & vbNewLine _
                                 & "H_INKAEDI_L.EDI_CTL_NO = H_INKAEDI_HED.EDI_CTL_NO            " & vbNewLine

    Private Const SQL_FROM_RCV_HED_NULL As String = "LEFT JOIN                                   " & vbNewLine _
                                 & "(                                                            " & vbNewLine _
                                 & "SELECT                                                       " & vbNewLine _
                                 & " H_INKAEDI_L.EDI_CTL_NO AS EDI_CTL_NO                        " & vbNewLine _
                                 & ",'' AS SYS_UPD_DATE                                          " & vbNewLine _
                                 & ",'' AS SYS_UPD_TIME                                          " & vbNewLine _
                                 & "FROM                                                         " & vbNewLine _
                                 & "$LM_TRN$..H_INKAEDI_L                 H_INKAEDI_L            " & vbNewLine _
                                 & "WHERE                                                        " & vbNewLine _
                                 & "H_INKAEDI_L.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                 & ")                                     H_INKAEDI_HED          " & vbNewLine _
                                 & "ON                                                           " & vbNewLine _
                                 & "H_INKAEDI_L.EDI_CTL_NO = H_INKAEDI_HED.EDI_CTL_NO            " & vbNewLine

    '2012.02.25 大阪対応 START
    Private Const SQL_FROM_RCVTABLE_INOUT As String = "AND                                       " & vbNewLine _
                                    & "H_INKAEDI_HED.INOUT_KB = '1'                              " & vbNewLine
    '2012.02.25 大阪対応 END

    Private Const SQL_FROM_RCV_DTL As String = "LEFT JOIN                                                            " & vbNewLine _
                                 & "(                                                                                " & vbNewLine _
                                 & "SELECT                                                                           " & vbNewLine _
                                 & " RCV_DTL.EDI_CTL_NO AS EDI_CTL_NO                                                " & vbNewLine _
                                 & ",LEFT(MAX(RCV_DTL.SYS_UPD_DATE + RCV_DTL.SYS_UPD_TIME),8)        AS SYS_UPD_DATE " & vbNewLine _
                                 & ",SUBSTRING(MAX(RCV_DTL.SYS_UPD_DATE + RCV_DTL.SYS_UPD_TIME),9,9) AS SYS_UPD_TIME " & vbNewLine _
                                 & "FROM                                                                             " & vbNewLine _
                                 & "$LM_TRN$..$RCV_DTL$                     RCV_DTL                                    " & vbNewLine _
                                 & "WHERE                                                                            " & vbNewLine _
                                 & "RCV_DTL.NRS_BR_CD = @NRS_BR_CD                                                   " & vbNewLine _
                                 & "GROUP BY                                                                         " & vbNewLine _
                                 & "RCV_DTL.EDI_CTL_NO                                                               " & vbNewLine _
                                 & ")                                     RCV_DTL                                    " & vbNewLine _
                                 & "ON                                                                               " & vbNewLine _
                                 & "H_INKAEDI_L.EDI_CTL_NO = RCV_DTL.EDI_CTL_NO                                      " & vbNewLine


    Private Const SQL_FROM_RCV_DTL_NULL As String = "LEFT JOIN                                   " & vbNewLine _
                                 & "(                                                            " & vbNewLine _
                                 & "SELECT                                                       " & vbNewLine _
                                 & " EDI_L.EDI_CTL_NO AS EDI_CTL_NO                              " & vbNewLine _
                                 & ",'' AS     SYS_UPD_DATE                                      " & vbNewLine _
                                 & ",'' AS     SYS_UPD_TIME                                      " & vbNewLine _
                                 & "FROM                                                         " & vbNewLine _
                                 & "$LM_TRN$..H_INKAEDI_L                 EDI_L                  " & vbNewLine _
                                 & "WHERE                                                        " & vbNewLine _
                                 & "EDI_L.NRS_BR_CD = @NRS_BR_CD                                 " & vbNewLine _
                                 & ")                                     RCV_DTL                " & vbNewLine _
                                 & "ON                                                           " & vbNewLine _
                                 & "H_INKAEDI_L.EDI_CTL_NO = RCV_DTL.EDI_CTL_NO                  " & vbNewLine



#End Region   '検索

#Region "検索ORDERBY句"
    Private Const SQL_SELECT_ORDER As String = "ORDER BY                                             " & vbNewLine _
                                           & "  H_INKAEDI_L.INKA_DATE                                " & vbNewLine _
                                           & " ,B_INKA_L.INKA_NO_L                                   " & vbNewLine _
                                           & " ,H_INKAEDI_L.EDI_CTL_NO                               " & vbNewLine
#End Region  '検索

    '▼▼▼二次（共通化）
#Region "INKAEDI_L(実績取消)"
    ''' <summary>
    ''' INKAEDI_L
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKITORIKESI_EDI_L As String = "UPDATE                               " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_L                           " & vbNewLine _
                                              & " SET                                             " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG               " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                   " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                   " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                   " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE               " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME               " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID               " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER               " & vbNewLine _
                                              & " WHERE                                           " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                  " & vbNewLine _
                                              & " AND                                             " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                 " & vbNewLine _
                                              & " AND                                             " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE         " & vbNewLine _
                                              & " AND                                             " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME         " & vbNewLine

#End Region

#Region "RCV_HED(実績取消)"
    ''' <summary>
    ''' RCV_HED(実績取消)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKITORIKESI_RCV_HED As String = "UPDATE                               " & vbNewLine _
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

#Region "RCV_DTL(実績取消)"
    ''' <summary>
    ''' RCV_DTL(実績取消)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKITORIKESI_RCV_DTL As String = "UPDATE                               " & vbNewLine _
                                              & " $LM_TRN$..$RCV_DTL$                               " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG            " & vbNewLine _
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
                                              & " JISSEKI_SHORI_FLG = '1'                           " & vbNewLine

#End Region

#Region "INKAEDI_L(EDI取消、EDI取消⇒未登録、報告用EDI取消)"
    ''' <summary>
    ''' EDI入荷(大)更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_EDITORIKESI_EDI_L As String = " UPDATE                                   " & vbNewLine _
                                         & " $LM_TRN$..H_INKAEDI_L                                 " & vbNewLine _
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
                                         & " EDI_CTL_NO = @EDI_CTL_NO                              " & vbNewLine _
                                         & " AND                                                   " & vbNewLine _
                                         & " SYS_UPD_DATE = @HAITA_SYS_UPD_DATE                    " & vbNewLine _
                                         & " AND                                                   " & vbNewLine _
                                         & " SYS_UPD_TIME = @HAITA_SYS_UPD_TIME                    " & vbNewLine


#End Region

#Region "INKAEDI_M(EDI取消、EDI取消⇒未登録、報告用EDI取消)"
    ''' <summary>
    ''' EDI入荷(大)更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_EDITORIKESI_EDI_M As String = " UPDATE                                   " & vbNewLine _
                                         & " $LM_TRN$..H_INKAEDI_M                                 " & vbNewLine _
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

#Region "RCV_HED(EDI取消、EDI取消⇒未登録、報告用EDI取消)"
    ''' <summary>
    ''' 受信ヘッダ更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_EDITORIKESI_RCV_HED As String = "UPDATE                                   " & vbNewLine _
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
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine

#End Region

#Region "RCV_DTL(EDI取消、EDI取消⇒未登録、報告用EDI取消)"
    ''' <summary>
    ''' 受信明細更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_EDITORIKESI_RCV_DTL As String = "UPDATE                                   " & vbNewLine _
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


#End Region

#Region "INKAEDI_L(実績作成済⇒実績未、実績送信済⇒実績未)"
    ''' <summary>
    ''' INKAEDI_L(実績作成済⇒実績未、実績送信済⇒実績未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKIMODOSI_EDI_L As String = "UPDATE                                   " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_L                             " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                 " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                 " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                 " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                 " & vbNewLine _
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
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine


#End Region

#Region "INKAEDI_L(実績送信済⇒送信待ち)"
    ''' <summary>
    ''' H_INKAEDI_LのUPDATE文（H_INKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_JISSEKIZUMI_SOUSINMACHI_EDI_L As String = "UPDATE $LM_TRN$..H_INKAEDI_L SET       " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                           " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                               " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                               " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                               " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                           " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                           " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                           " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                           " & vbNewLine _
                                              & " WHERE                                                       " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                              " & vbNewLine _
                                              & " AND                                                         " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                             " & vbNewLine _
                                              & " AND                                                         " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE                     " & vbNewLine _
                                              & " AND                                                         " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME                     " & vbNewLine

#End Region

   '2015.09.07 tsunehira add
#Region "INKAEDI_L(荷主一括変更)"
    ''' <summary>
    ''' H_INKAEDI_LのUPDATE文（H_INKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_BULK_CHG_EDI_L As String = "UPDATE                                                         " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_L                                          " & vbNewLine _
                                              & " SET                                                            " & vbNewLine _
                                              & "   CUST_CD_L          = @CUST_CD_L                              " & vbNewLine _
                                              & "   ,CUST_CD_M         = @CUST_CD_M                              " & vbNewLine _
                                              & "   ,UPD_USER          = @UPD_USER                               " & vbNewLine _
                                              & "   ,UPD_DATE          = @UPD_DATE                               " & vbNewLine _
                                              & "   ,UPD_TIME          = @UPD_TIME                               " & vbNewLine _
                                              & "--2018/12/11 ADD START 要望管理003739                           " & vbNewLine _
                                              & "   ,TAX_KB            =                                         " & vbNewLine _
                                              & "--2019/01/29 DEL START 要望管理004404                           " & vbNewLine _
                                              & "--  CASE WHEN @CUST_CD_L = '00003' AND @CUST_CD_M = '00' THEN '02'  " & vbNewLine _
                                              & "--       WHEN @CUST_CD_L = '00061' AND @CUST_CD_M = '00' THEN '01'  " & vbNewLine _
                                              & "--2019/01/29 DEL END   要望管理004404                           " & vbNewLine _
                                              & "--2019/01/29 ADD START 要望管理004404                           " & vbNewLine _
                                              & "    CASE WHEN @CUST_CD_L = '00003' AND @CUST_CD_M = '00'        " & vbNewLine _
                                              & "         THEN (SELECT KBN_CD                                    " & vbNewLine _
                                              & "                 FROM LM_MST..Z_KBN                             " & vbNewLine _
                                              & "                WHERE KBN_GROUP_CD = 'Z001'                     " & vbNewLine _
                                              & "                  AND KBN_NM2 = '2'                             " & vbNewLine _
                                              & "                  AND SYS_DEL_FLG = '0')                        " & vbNewLine _
                                              & "         WHEN @CUST_CD_L = '00061' AND @CUST_CD_M = '00'        " & vbNewLine _
                                              & "         THEN (SELECT KBN_CD                                    " & vbNewLine _
                                              & "                 FROM LM_MST..Z_KBN                             " & vbNewLine _
                                              & "                WHERE KBN_GROUP_CD = 'Z001'                     " & vbNewLine _
                                              & "                  AND KBN_NM2 = '1'                             " & vbNewLine _
                                              & "                  AND SYS_DEL_FLG = '0')                        " & vbNewLine _
                                              & "--2019/01/29 ADD END   要望管理004404                           " & vbNewLine _
                                              & "         ELSE TAX_KB                                            " & vbNewLine _
                                              & "    END                                                         " & vbNewLine _
                                              & "--2018/12/11 ADD END   要望管理003739                           " & vbNewLine _
                                              & "   ,SYS_UPD_DATE      = @SYS_UPD_DATE                           " & vbNewLine _
                                              & "   ,SYS_UPD_TIME      = @SYS_UPD_TIME                           " & vbNewLine _
                                              & "   ,SYS_UPD_PGID      = @SYS_UPD_PGID                           " & vbNewLine _
                                              & "   ,SYS_UPD_USER      = @SYS_UPD_USER                           " & vbNewLine _
                                              & " WHERE                                                          " & vbNewLine _
                                              & "   NRS_BR_CD          = @NRS_BR_CD                              " & vbNewLine _
                                              & "   AND                                                          " & vbNewLine _
                                              & "   EDI_CTL_NO         = @EDI_CTL_NO                             " & vbNewLine _
                                              & "   AND                                                          " & vbNewLine _
                                              & "   SYS_UPD_DATE       = @HAITA_SYS_UPD_DATE                     " & vbNewLine _
                                              & "   AND                                                          " & vbNewLine _
                                              & "   SYS_UPD_TIME       = @HAITA_SYS_UPD_TIME                     " & vbNewLine

#End Region

    '2015.09.24 tsunehira add
#Region "INKAEDI_M(荷主一括変更)"
    ''' <summary>
    ''' H_INKAEDI_MのUPDATE文（H_INKAEDI_M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_BULK_CHG_EDI_M As String = "UPDATE                                                         " & vbNewLine _
                                             & " $LM_TRN$..H_INKAEDI_M                                           " & vbNewLine _
                                             & " SET                                                             " & vbNewLine _
                                             & "    NRS_GOODS_CD       = @NRS_GOODS_CD                           " & vbNewLine _
                                             & "    ,STD_IRIME         = @STD_IRIME                              " & vbNewLine _
                                             & "    ,IRIME             = @IRIME                                  " & vbNewLine _
                                             & "    ,PKG_NB            = @PKG_NB                                 " & vbNewLine _
                                             & "    ,NB                = @NB                                     " & vbNewLine _
                                             & "    ,INKA_PKG_NB       = @INKA_PKG_NB                            " & vbNewLine _
                                             & "    ,HASU              = @HASU                                   " & vbNewLine _
                                             & "    ,UPD_USER          = @UPD_USER                               " & vbNewLine _
                                             & "    ,UPD_DATE          = @UPD_DATE                               " & vbNewLine _
                                             & "    ,UPD_TIME          = @UPD_TIME                               " & vbNewLine _
                                             & "    ,SYS_UPD_DATE      = @SYS_UPD_DATE                           " & vbNewLine _
                                             & "    ,SYS_UPD_TIME      = @SYS_UPD_TIME                           " & vbNewLine _
                                             & "    ,SYS_UPD_PGID      = @SYS_UPD_PGID                           " & vbNewLine _
                                             & "    ,SYS_UPD_USER      = @SYS_UPD_USER                           " & vbNewLine _
                                             & " WHERE                                                           " & vbNewLine _
                                             & "    NRS_BR_CD          = @NRS_BR_CD                              " & vbNewLine _
                                             & "    AND                                                          " & vbNewLine _
                                             & "    EDI_CTL_NO         = @EDI_CTL_NO                             " & vbNewLine _
                                             & "    AND                                                          " & vbNewLine _
                                             & "    EDI_CTL_NO_CHU     = @EDI_CTL_NO_CHU                         " & vbNewLine


#End Region

#Region "INKAEDI_M(実績作成済⇒実績未、実績送信済⇒実績未)"
    ''' <summary>
    ''' INKAEDI_M(実績作成済⇒実績未、実績送信済⇒実績未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKIMODOSI_EDI_M As String = "UPDATE                           " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_M                             " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " JISSEKI_FLAG      = @JISSEKI_FLAG                 " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                 " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                 " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                 " & vbNewLine _
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
                                              & " OUT_KB            = '0'                           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " JISSEKI_FLAG       <> '9'                         " & vbNewLine

#End Region

#Region "INKAEDI_M(実績送信済⇒送信待ち)"
    ''' <summary>
    ''' H_INKAEDI_MのUPDATE文（H_INKAEDI_M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_JISSEKIZUMI_SOUSINMACHI_EDI_M As String = "UPDATE $LM_TRN$..H_INKAEDI_M SET            " & vbNewLine _
                                             & " JISSEKI_FLAG      = @JISSEKI_FLAG                           " & vbNewLine _
                                             & ",UPD_USER          = @UPD_USER                               " & vbNewLine _
                                             & ",UPD_DATE          = @UPD_DATE                               " & vbNewLine _
                                             & ",UPD_TIME          = @UPD_TIME                               " & vbNewLine _
                                             & ",SYS_UPD_DATE      = @SYS_UPD_DATE                           " & vbNewLine _
                                             & ",SYS_UPD_TIME      = @SYS_UPD_TIME                           " & vbNewLine _
                                             & ",SYS_UPD_PGID      = @SYS_UPD_PGID                           " & vbNewLine _
                                             & ",SYS_UPD_USER      = @SYS_UPD_USER                           " & vbNewLine _
                                             & " WHERE                                                       " & vbNewLine _
                                             & " NRS_BR_CD         = @NRS_BR_CD                              " & vbNewLine _
                                             & " AND                                                         " & vbNewLine _
                                             & " EDI_CTL_NO        = @EDI_CTL_NO                             " & vbNewLine



#End Region

#Region "RCV_HED(実績作成、実績作成済⇒実績未、実績送信済⇒実績未)"
    ''' <summary>
    ''' RCV_HED(実績作成、実績作成済⇒実績未、実績送信済⇒実績未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKIMODOSI_RCV_HED As String = "UPDATE                                 " & vbNewLine _
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
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine

#End Region

#Region "RCV_HED(実績送信済⇒送信未)"
    ''' <summary>
    ''' RCV_HED(実績送信済⇒送信未)
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
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine

#End Region

#Region "RCV_DTL(実績作成済⇒実績未)"
    ''' <summary>
    ''' RCV_DTL(実績作成済⇒実績未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKIZUMI_JISSEKIMI_RCV_DTL As String = "UPDATE                         " & vbNewLine _
                                              & " $LM_TRN$..$RCV_DTL$                               " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG            " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                 " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                 " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                 " & vbNewLine _
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
                                              & " JISSEKI_SHORI_FLG  = '2'                          " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine


#End Region

#Region "RCV_DTL(実績作成済⇒実績未)住化カラー専用"
    ''' <summary>
    ''' RCV_DTL(実績作成済⇒実績未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKIZUMI_JISSEKIMI_RCV_DTL_SMK As String = "UPDATE                         " & vbNewLine _
                                              & " $LM_TRN$..$RCV_DTL$                               " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & "YUSO_SHUDAN        = ''                            " & vbNewLine _
                                              & ",YUSO_GYOSHA       = ''                            " & vbNewLine _
                                              & ",YUSO_JIS_CD       = ''                            " & vbNewLine _
                                              & ",JISSEKI_KBN       = ''                            " & vbNewLine _
                                              & ",JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG            " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                 " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                 " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                 " & vbNewLine _
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
                                              & " JISSEKI_SHORI_FLG  = '2'                          " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine


#End Region

#Region "RCV_DTL(実績送信済⇒実績未、実績送信済⇒送信未)"
    ''' <summary>
    ''' RCV_DTL(実績送信済⇒実績未、実績送信済⇒送信未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_SOUSINZUMI_JISSEKIMI_RCV_DTL As String = "UPDATE                          " & vbNewLine _
                                              & " $LM_TRN$..$RCV_DTL$                               " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG            " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                 " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                 " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                 " & vbNewLine _
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

#Region "RCV_DTL(実績送信済⇒実績未、実績送信済⇒送信未)住化カラー専用"
    ''' <summary>
    ''' RCV_DTL(実績送信済⇒実績未、実績送信済⇒送信未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_SOUSINZUMI_JISSEKIMI_RCV_DTL_SMK As String = "UPDATE                          " & vbNewLine _
                                              & " $LM_TRN$..$RCV_DTL$                               " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & "YUSO_SHUDAN        = ''                            " & vbNewLine _
                                              & ",YUSO_GYOSHA       = ''                            " & vbNewLine _
                                              & ",YUSO_JIS_CD       = ''                            " & vbNewLine _
                                              & ",JISSEKI_KBN       = ''                            " & vbNewLine _
                                              & ",JISSEKI_SHORI_FLG = @JISSEKI_SHORI_FLG            " & vbNewLine _
                                              & ",JISSEKI_USER      = @JISSEKI_USER                 " & vbNewLine _
                                              & ",JISSEKI_DATE      = @JISSEKI_DATE                 " & vbNewLine _
                                              & ",JISSEKI_TIME      = @JISSEKI_TIME                 " & vbNewLine _
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

#Region "RCV_DTL(実績送信済⇒送信未)"
    ''' <summary>
    ''' RCV_DTL(実績送信済⇒送信未)
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

#Region "RCV_TBL(INOUT_KB有の場合：EDI受信TBLが入荷・出荷で同TBLの場合条件追加)"
    ''' <summary>
    ''' RCV_TBL(INOUT_KBの設定。入荷の場合は"1")
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_UPDATE_WHERE_INOUT_KB As String = "AND                  " & vbNewLine _
                                      & "INOUT_KB     = '1'                   " & vbNewLine

#End Region

#Region "SEND(実績送信済⇒送信未)"
    ''' <summary>
    ''' SEND(実績送信済⇒送信未)
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
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine

#End Region

#Region "INKA_L(実績作成済⇒実績未、実績送信済⇒実績未)"
    ''' <summary>
    ''' INKA_L(実績作成済⇒実績未、実績送信済⇒実績未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKIMODOSI_INKA_L As String = "UPDATE                                  " & vbNewLine _
                                              & " $LM_TRN$..B_INKA_L                                " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " INKA_STATE_KB     = @INKA_STATE_KB                " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " INKA_NO_L         = @INKA_NO_L                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_DATE = @HAITA_SYS_UPD_DATE                " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME = @HAITA_SYS_UPD_TIME                " & vbNewLine
#End Region

    '2012.02.25 大阪対応 START
#Region "入荷取消⇒未登録処理 同一まとめ番号データ取得用SQL"

    ''' <summary>
    ''' 同一まとめ番号データ取得用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_MATOMETORIKESI As String = " SELECT                                                     " & vbNewLine _
                                            & " H_INKAEDI_L.NRS_BR_CD                              AS NRS_BR_CD           " & vbNewLine _
                                            & ",H_INKAEDI_L.EDI_CTL_NO                             AS EDI_CTL_NO          " & vbNewLine _
                                            & ",H_INKAEDI_L.SYS_UPD_DATE                           AS SYS_UPD_DATE        " & vbNewLine _
                                            & ",H_INKAEDI_L.SYS_UPD_TIME                           AS SYS_UPD_TIME        " & vbNewLine _
                                            & ",H_INKAEDI_L.INKA_CTL_NO_L                          AS INKA_CTL_NO_L       " & vbNewLine _
                                            & ",''                                                 AS RCV_UPD_DATE        " & vbNewLine _
                                            & ",''                                                 AS RCV_UPD_TIME        " & vbNewLine _
                                            & ",M_EDI_CUST.RCV_NM_HED                              AS RCV_NM_HED          " & vbNewLine _
                                            & ",M_EDI_CUST.RCV_NM_DTL                              AS RCV_NM_DTL          " & vbNewLine _
                                            & ",M_EDI_CUST.RCV_NM_EXT                              AS RCV_NM_EXT          " & vbNewLine _
                                            & ",M_EDI_CUST.SND_NM                                  AS SND_NM              " & vbNewLine _
                                            & ",M_EDI_CUST.FLAG_16                                 AS EDI_CUST_INOUTFLG   " & vbNewLine _
                                            & "--EDI_CUST_INDEX追加        2012.11.22                                     " & vbNewLine _
                                            & ",M_EDI_CUST.EDI_CUST_INDEX                          AS EDI_CUST_INDEX      " & vbNewLine _
                                            & " FROM                                                                      " & vbNewLine _
                                            & " $LM_TRN$..H_INKAEDI_L                    H_INKAEDI_L                      " & vbNewLine _
                                            & " INNER JOIN                                                                " & vbNewLine _
                                            & " $LM_TRN$..B_INKA_L                       B_INKA_L                         " & vbNewLine _
                                            & " ON                                                                        " & vbNewLine _
                                            & " H_INKAEDI_L.NRS_BR_CD =B_INKA_L.NRS_BR_CD                                 " & vbNewLine _
                                            & " AND                                                                       " & vbNewLine _
                                            & " H_INKAEDI_L.INKA_CTL_NO_L =B_INKA_L.INKA_NO_L                             " & vbNewLine _
                                            & " --INNER JOIN                                                              " & vbNewLine _
                                            & " --$LM_TRN$..$RCV_HED$                       RCV_HED                       " & vbNewLine _
                                            & " --ON                                                                      " & vbNewLine _
                                            & " --H_OUTKAEDI_L.NRS_BR_CD =RCV_HED.NRS_BR_CD                               " & vbNewLine _
                                            & " --AND                                                                     " & vbNewLine _
                                            & " --H_OUTKAEDI_L.EDI_CTL_NO =RCV_HED.EDI_CTL_NO                             " & vbNewLine _
                                            & " INNER JOIN                                                                " & vbNewLine _
                                            & " $LM_MST$..M_EDI_CUST                       M_EDI_CUST                     " & vbNewLine _
                                            & " ON                                                                        " & vbNewLine _
                                            & " H_INKAEDI_L.NRS_BR_CD =M_EDI_CUST.NRS_BR_CD                               " & vbNewLine _
                                            & " AND                                                                       " & vbNewLine _
                                            & " H_INKAEDI_L.NRS_WH_CD =M_EDI_CUST.WH_CD                                   " & vbNewLine _
                                            & " AND                                                                       " & vbNewLine _
                                            & " H_INKAEDI_L.CUST_CD_L =M_EDI_CUST.CUST_CD_L                               " & vbNewLine _
                                            & " AND                                                                       " & vbNewLine _
                                            & " H_INKAEDI_L.CUST_CD_M =M_EDI_CUST.CUST_CD_M                               " & vbNewLine _
                                            & " AND                                                                       " & vbNewLine _
                                            & " --M_EDI_CUST.INOUT_KB = '0'  20121121修正                                 " & vbNewLine _
                                            & " M_EDI_CUST.INOUT_KB = '1'                                                 " & vbNewLine _
                                            & " AND                                                                       " & vbNewLine _
                                            & " M_EDI_CUST.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                            & " WHERE                                                                     " & vbNewLine _
                                            & " SUBSTRING(H_INKAEDI_L.FREE_C30,4,9) = @MATOME_NO                          " & vbNewLine _
                                            & " AND                                                                       " & vbNewLine _
                                            & " B_INKA_L.INKA_NO_L = @INKA_NO_L                                           " & vbNewLine _
                                            & " AND                                                                       " & vbNewLine _
                                            & " H_INKAEDI_L.SYS_DEL_FLG = '0'                                             " & vbNewLine _
                                            & " AND                                                                       " & vbNewLine _
                                            & " B_INKA_L.SYS_DEL_FLG = '1'                                                " & vbNewLine _
                                            & " AND B_INKA_L.SYS_UPD_DATE = @SYS_UPD_DATE                                 " & vbNewLine _
                                            & " AND B_INKA_L.SYS_UPD_TIME = @SYS_UPD_TIME                                 " & vbNewLine


#End Region
    '2012.02.25 大阪対応 START


#Region "INKAEDI_L(入荷取消⇒未登録)"
    ''' <summary>
    ''' INKAEDI_L(入荷取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_TOUROKUMI_EDI_L As String = "UPDATE                                       " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_L                             " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " INKA_CTL_NO_L     = ''                            " & vbNewLine _
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
                                              & " INKA_CTL_NO_L     = @INKA_CTL_NO_L                " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE           " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine

    ''' <summary>
    ''' H_INKAEDI_L(入荷取消⇒未登録)まとめ戻しの場合
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_TOUROKUMI_EDI_L_MATOME As String = "UPDATE                           " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_L                             " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " INKA_CTL_NO_L     = ''                            " & vbNewLine _
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
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                 " & vbNewLine

#End Region

#Region "INKAEDI_M(入荷取消⇒未登録)"
    ''' <summary>
    ''' INKAEDI_M(入荷取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_TOUROKUMI_EDI_M As String = "UPDATE                                       " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_M                             " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " INKA_CTL_NO_L     = ''                            " & vbNewLine _
                                              & ",INKA_CTL_NO_M     = ''                            " & vbNewLine _
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
                                              & " INKA_CTL_NO_L     = @INKA_CTL_NO_L                " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " OUT_KB            = '0'                           " & vbNewLine

    ''' <summary>
    ''' INKAEDI_M(入荷取消⇒未登録)まとめデータ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_TOUROKUMI_EDI_M_MATOME As String = "UPDATE                           " & vbNewLine _
                                              & " $LM_TRN$..H_INKAEDI_M                            " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " INKA_CTL_NO_L     = ''                            " & vbNewLine _
                                              & ",INKA_CTL_NO_M     = ''                            " & vbNewLine _
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

#Region "RCV_HED(入荷取消⇒未登録)"
    ''' <summary>
    ''' RCV_HED(入荷取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_TOUROKUMI_RCV_HED As String = "UPDATE                                     " & vbNewLine _
                                              & " $LM_TRN$..$RCV_HED$                               " & vbNewLine _
                                              & " SET                                               " & vbNewLine _
                                              & " INKA_CTL_NO_L     = $BR_INITIAL$ + '00000000'     " & vbNewLine _
                                              & ",INKA_USER         = @INKA_USER                    " & vbNewLine _
                                              & ",INKA_DATE         = @INKA_DATE                    " & vbNewLine _
                                              & ",INKA_TIME         = @INKA_TIME                    " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                     " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                     " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & "-- AND                                               " & vbNewLine _
                                              & "-- INKA_CTL_NO_L     = @INKA_CTL_NO_L                " & vbNewLine _
                                              & "-- 2012.02.25 大阪対応 排他をコメントアウト          " & vbNewLine _
                                              & "-- AND                                               " & vbNewLine _
                                              & "-- SYS_UPD_DATE      = @HAITA_SYS_UPD_DATE           " & vbNewLine _
                                              & "-- AND                                               " & vbNewLine _
                                              & "-- SYS_UPD_TIME      = @HAITA_SYS_UPD_TIME           " & vbNewLine

    Private Const SQL_WHERE_TOUROKUMI_NORMAL As String = "AND                                   " & vbNewLine _
                                      & " INKA_CTL_NO_L      = @INKA_CTL_NO_L                 " & vbNewLine

    Private Const SQL_WHERE_TOUROKUMI_MATOME As String = "AND                                   " & vbNewLine _
                                          & " EDI_CTL_NO      = @EDI_CTL_NO                 " & vbNewLine


#End Region

    '2012.02.25 大阪対応 START
#Region "RCV_DTL(入荷取消⇒未登録)"
    ''' <summary>
    ''' RCV_DTL(入荷取消⇒未登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_TOUROKUMI_RCV_DTL1 As String = "UPDATE                                " & vbNewLine _
                                              & " $LM_TRN$..$RCV_DTL$                           " & vbNewLine _
                                              & " SET                                           " & vbNewLine _
                                              & " INKA_CTL_NO_L     = $BR_INITIAL$ + '00000000' " & vbNewLine _
                                              & ",INKA_CTL_NO_M     = '000'                     " & vbNewLine

    '要望番号:1088対応 terakawa 2012.06.19 Start
    Private Const SQL_UPD_TOUROKUMI_RCV_DTL2 As String = ",INKA_USER          = @INKA_USER       " & vbNewLine _
                                              & ",INKA_DATE          = @INKA_DATE                " & vbNewLine _
                                              & ",INKA_TIME          = @INKA_TIME                " & vbNewLine

    'Private Const SQL_UPD_TOUROKUMI_RCV_DTL2 As String = ",INKA_USER          = @UPD_USER       " & vbNewLine _
    '                                          & ",INKA_DATE          = @UPD_DATE                " & vbNewLine _
    '                                          & ",INKA_TIME          = @UPD_TIME                " & vbNewLine
    '要望番号:1088対応 terakawa 2012.06.19 End

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

#Region "SEND物理削除(実績作成済⇒実績未、実績送信済⇒実績未)"
    ''' <summary>
    ''' SEND(実績作成済⇒実績未、実績送信済⇒実績未)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_JISSEKIMODOSI_SEND As String = "DELETE                                    " & vbNewLine _
                                              & " $LM_TRN$..$SEND_TBL$                              " & vbNewLine _
                                              & " WHERE                                             " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                    " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " EDI_CTL_NO        = @EDI_CTL_NO                   " & vbNewLine _
                                              & " AND                                               " & vbNewLine _
                                              & " SYS_DEL_FLG     <> '1'                            " & vbNewLine

#End Region
    '▲▲▲二次（共通化）

    '共通対応 20120223 start
#Region "まとめ先取得"
    Private Const SQL_SELECT_MATOME_TARGET As String = "SELECT                                       " & vbNewLine _
                                   & " QUERY.INKA_NO_L                                               " & vbNewLine _
                                   & ",QUERY.INKA_NO_M                                               " & vbNewLine _
                                   & ",QUERY.EDI_CTL_NO                                              " & vbNewLine _
                                   & ",QUERY.SYS_UPD_DATE                                            " & vbNewLine _
                                   & ",QUERY.SYS_UPD_TIME                                            " & vbNewLine _
                                   & ",QUERY.UNSO_NO_L                                               " & vbNewLine _
                                   & ",QUERY.UNSO_NO_M                                               " & vbNewLine _
                                   & ",QUERY.UNSO_SYS_UPD_DATE                                       " & vbNewLine _
                                   & ",QUERY.UNSO_SYS_UPD_TIME                                       " & vbNewLine _
                                   & ",QUERY.INKA_TTL_NB                                             " & vbNewLine _
                                   & ",QUERY.UNSO_PKG_NB                                             " & vbNewLine _
                                   & ",QUERY.NRS_BR_CD                                               " & vbNewLine _
                                   & ",QUERY.INKA_STATE_KB_1 AS INKA_STATE_KB                        " & vbNewLine _
                                   & " FROM                                                          " & vbNewLine _
                                   & " (SELECT                                                       " & vbNewLine _
                                   & "   INKA_L.INKA_NO_L                  AS INKA_NO_L              " & vbNewLine _
                                   & "   ,INKA_L.INKA_TTL_NB               AS INKA_TTL_NB            " & vbNewLine _
                                   & "    ,CASE WHEN (SUBSTRING(EDI_L.FREE_C30,4,9) <> '' AND SUBSTRING(EDI_L.FREE_C30,1,2) = '05') " & vbNewLine _
                                   & "     THEN SUBSTRING(EDI_L.FREE_C30,4,9)                        " & vbNewLine _
                                   & "     ELSE EDI_L.EDI_CTL_NO                                     " & vbNewLine _
                                   & "     END  EDI_CTL_NO                                           " & vbNewLine _
                                   & "    ,MAX(INKA_L.SYS_ENT_DATE)     AS  SYS_ENT_DATE             " & vbNewLine _
                                   & "    ,MAX(INKA_L.SYS_ENT_TIME)     AS  SYS_ENT_TIME             " & vbNewLine _
                                   & "    ,INKA_L.SYS_UPD_DATE         AS  SYS_UPD_DATE              " & vbNewLine _
                                   & "    ,INKA_L.SYS_UPD_TIME         AS  SYS_UPD_TIME              " & vbNewLine _
                                   & "   ,MAX(ISNULL(INKA_M.INKA_NO_M,'')) AS INKA_NO_M              " & vbNewLine _
                                   & "   ,UNSO_L.UNSO_NO_L                 AS UNSO_NO_L              " & vbNewLine _
                                   & "   ,UNSO_L.UNSO_PKG_NB               AS UNSO_PKG_NB            " & vbNewLine _
                                   & "   ,UNSO_L.SYS_UPD_DATE              AS UNSO_SYS_UPD_DATE      " & vbNewLine _
                                   & "   ,UNSO_L.SYS_UPD_TIME              AS UNSO_SYS_UPD_TIME      " & vbNewLine _
                                   & "   ,MAX(ISNULL(UNSO_M.UNSO_NO_M,'')) AS UNSO_NO_M              " & vbNewLine _
                                   & "   ,CASE MAX(ISNULL(INKA_L.INKA_STATE_KB,''))                  " & vbNewLine _
                                   & "    WHEN '' THEN '99'                                          " & vbNewLine _
                                   & "    WHEN '10' THEN '10'                                        " & vbNewLine _
                                   & "    WHEN '50' THEN '50'                                        " & vbNewLine _
                                   & "    WHEN '90' THEN '90'                                        " & vbNewLine _
                                   & "    ELSE '40'                                                  " & vbNewLine _
                                   & "   END AS INKA_STATE_KB_1                                      " & vbNewLine _
                                   & "   ,INKA_L.NRS_BR_CD                 AS NRS_BR_CD              " & vbNewLine _
                                   & "   FROM $LM_TRN$..B_INKA_L  INKA_L                             " & vbNewLine _
                                   & "   INNER JOIN $LM_TRN$..B_INKA_M INKA_M                        " & vbNewLine _
                                   & "    ON                                                         " & vbNewLine _
                                   & "    INKA_M.SYS_DEL_FLG <> '1'                                  " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.NRS_BR_CD = INKA_M.NRS_BR_CD                        " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.INKA_NO_L = INKA_M.INKA_NO_L                        " & vbNewLine _
                                   & "   LEFT JOIN $LM_TRN$..F_UNSO_L UNSO_L                         " & vbNewLine _
                                   & "    ON                                                         " & vbNewLine _
                                   & "    INKA_M.SYS_DEL_FLG <> '1'                                  " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.NRS_BR_CD = UNSO_L.NRS_BR_CD                        " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.INKA_NO_L = UNSO_L.INOUTKA_NO_L                     " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.CUST_CD_L = UNSO_L.CUST_CD_L                        " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.CUST_CD_M = UNSO_L.CUST_CD_M                        " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    UNSO_L.MOTO_DATA_KB = '10'                                 " & vbNewLine _
                                   & "   LEFT JOIN $LM_TRN$..F_UNSO_M UNSO_M                         " & vbNewLine _
                                   & "    ON                                                         " & vbNewLine _
                                   & "    UNSO_M.SYS_DEL_FLG <> '1'                                  " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    UNSO_M.UNSO_NO_L = UNSO_L.UNSO_NO_L                        " & vbNewLine _
                                   & "   INNER JOIN $LM_TRN$..H_INKAEDI_L EDI_L                        " & vbNewLine _
                                   & "    ON                                                         " & vbNewLine _
                                   & "    INKA_L.NRS_BR_CD = EDI_L.NRS_BR_CD                         " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.INKA_NO_L = EDI_L.INKA_CTL_NO_L                     " & vbNewLine _
                                   & "   WHERE                                                       " & vbNewLine _
                                   & "    INKA_L.INKA_STATE_KB < '50'                                " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.SYS_DEL_FLG <> '1'                                  " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.CUST_CD_L = @CUST_CD_L                              " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.CUST_CD_M = @CUST_CD_M                              " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.INKA_DATE = @INKA_DATE                              " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    ISNULL(UNSO_L.ORIG_CD,'') = @OUTKA_MOTO                    " & vbNewLine _
                                   & "   GROUP BY                                                    " & vbNewLine _
                                   & "    INKA_L.INKA_NO_L                                           " & vbNewLine _
                                   & "   ,INKA_L.INKA_TTL_NB                                         " & vbNewLine _
                                   & "   ,CASE WHEN (SUBSTRING(EDI_L.FREE_C30,4,9) <> '' AND SUBSTRING(EDI_L.FREE_C30,1,2) = '05')   " & vbNewLine _
                                   & "     THEN SUBSTRING(EDI_L.FREE_C30,4,9)                        " & vbNewLine _
                                   & "     ELSE EDI_L.EDI_CTL_NO                                     " & vbNewLine _
                                   & "     END                                                       " & vbNewLine _
                                   & "   ,INKA_L.SYS_UPD_DATE                                        " & vbNewLine _
                                   & "   ,INKA_L.SYS_UPD_TIME                                        " & vbNewLine _
                                   & "   ,UNSO_L.UNSO_NO_L                                           " & vbNewLine _
                                   & "   ,UNSO_L.UNSO_PKG_NB                                         " & vbNewLine _
                                   & "   ,UNSO_L.SYS_UPD_DATE                                        " & vbNewLine _
                                   & "   ,UNSO_L.SYS_UPD_TIME                                        " & vbNewLine _
                                   & "   ,INKA_L.NRS_BR_CD                                           " & vbNewLine _
                                   & " ) QUERY                                                       " & vbNewLine _
                                   & "ORDER BY                                                       " & vbNewLine _
                                   & " QUERY.INKA_STATE_KB_1                                         " & vbNewLine _
                                   & ",QUERY.INKA_NO_L                                               " & vbNewLine



    '東邦化学追加箇所 20120216 end

    ''' <summary>
    ''' まとめ先データ運送M取得用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MATOMESAKI_DATA_UNSO_M As String = " SELECT                                     " & vbNewLine _
                                        & " F_UNSO_M.BETU_WT                         AS BETU_WT       " & vbNewLine _
                                        & ",F_UNSO_M.UNSO_TTL_NB                     AS UNSO_TTL_NB   " & vbNewLine _
                                        & ",F_UNSO_M.HASU                            AS HASU          " & vbNewLine _
                                        & ",F_UNSO_M.PKG_NB                          AS PKG_NB        " & vbNewLine _
                                        & " FROM                                                      " & vbNewLine _
                                        & " $LM_TRN$..F_UNSO_M                       F_UNSO_M         " & vbNewLine _
                                        & " WHERE                                                     " & vbNewLine _
                                        & " F_UNSO_M.NRS_BR_CD   = @NRS_BR_CD                         " & vbNewLine _
                                        & " AND                                                       " & vbNewLine _
                                        & " F_UNSO_M.UNSO_NO_L   = @UNSO_NO_L                         " & vbNewLine



#End Region

    '2015.05.12 ローム　入荷登録一括対応 修正START
    '20150422 まとめローム(FLAG07 = '5') 追加START
#Region "まとめ先取得(FLAG_07 = '5')(ローム)"
    Private Const SQL_SELECT_MATOME_TARGET_IKKATU As String = "SELECT                                       " & vbNewLine _
                                   & " QUERY.INKA_NO_L                                               " & vbNewLine _
                                   & ",QUERY.INKA_NO_M                                               " & vbNewLine _
                                   & ",QUERY.EDI_CTL_NO                                              " & vbNewLine _
                                   & ",QUERY.SYS_UPD_DATE                                            " & vbNewLine _
                                   & ",QUERY.SYS_UPD_TIME                                            " & vbNewLine _
                                   & ",QUERY.UNSO_NO_L                                               " & vbNewLine _
                                   & ",QUERY.UNSO_NO_M                                               " & vbNewLine _
                                   & ",QUERY.UNSO_SYS_UPD_DATE                                       " & vbNewLine _
                                   & ",QUERY.UNSO_SYS_UPD_TIME                                       " & vbNewLine _
                                   & ",QUERY.INKA_TTL_NB                                             " & vbNewLine _
                                   & ",QUERY.UNSO_PKG_NB                                             " & vbNewLine _
                                   & ",QUERY.NRS_BR_CD                                               " & vbNewLine _
                                   & ",QUERY.INKA_STATE_KB_1 AS INKA_STATE_KB                        " & vbNewLine _
                                   & " FROM                                                          " & vbNewLine _
                                   & " (SELECT                                                       " & vbNewLine _
                                   & "   INKA_L.INKA_NO_L                  AS INKA_NO_L              " & vbNewLine _
                                   & "   ,INKA_L.INKA_TTL_NB               AS INKA_TTL_NB            " & vbNewLine _
                                   & "    ,CASE WHEN (SUBSTRING(EDI_L.FREE_C30,4,9) <> '' AND SUBSTRING(EDI_L.FREE_C30,1,2) = '05') " & vbNewLine _
                                   & "     THEN SUBSTRING(EDI_L.FREE_C30,4,9)                        " & vbNewLine _
                                   & "     ELSE EDI_L.EDI_CTL_NO                                     " & vbNewLine _
                                   & "     END  EDI_CTL_NO                                           " & vbNewLine _
                                   & "    ,MAX(INKA_L.SYS_ENT_DATE)     AS  SYS_ENT_DATE             " & vbNewLine _
                                   & "    ,MAX(INKA_L.SYS_ENT_TIME)     AS  SYS_ENT_TIME             " & vbNewLine _
                                   & "    ,INKA_L.SYS_UPD_DATE         AS  SYS_UPD_DATE              " & vbNewLine _
                                   & "    ,INKA_L.SYS_UPD_TIME         AS  SYS_UPD_TIME              " & vbNewLine _
                                   & "   ,MAX(ISNULL(INKA_M.INKA_NO_M,'')) AS INKA_NO_M              " & vbNewLine _
                                   & "   ,UNSO_L.UNSO_NO_L                 AS UNSO_NO_L              " & vbNewLine _
                                   & "   ,UNSO_L.UNSO_PKG_NB               AS UNSO_PKG_NB            " & vbNewLine _
                                   & "   ,UNSO_L.SYS_UPD_DATE              AS UNSO_SYS_UPD_DATE      " & vbNewLine _
                                   & "   ,UNSO_L.SYS_UPD_TIME              AS UNSO_SYS_UPD_TIME      " & vbNewLine _
                                   & "   ,MAX(ISNULL(UNSO_M.UNSO_NO_M,'')) AS UNSO_NO_M              " & vbNewLine _
                                   & "   ,CASE MAX(ISNULL(INKA_L.INKA_STATE_KB,''))                  " & vbNewLine _
                                   & "    WHEN '' THEN '99'                                          " & vbNewLine _
                                   & "    WHEN '10' THEN '10'                                        " & vbNewLine _
                                   & "    WHEN '50' THEN '50'                                        " & vbNewLine _
                                   & "    WHEN '90' THEN '90'                                        " & vbNewLine _
                                   & "    ELSE '40'                                                  " & vbNewLine _
                                   & "   END AS INKA_STATE_KB_1                                      " & vbNewLine _
                                   & "   ,INKA_L.NRS_BR_CD                 AS NRS_BR_CD              " & vbNewLine _
                                   & "   FROM $LM_TRN$..B_INKA_L  INKA_L                             " & vbNewLine _
                                   & "   INNER JOIN $LM_TRN$..B_INKA_M INKA_M                        " & vbNewLine _
                                   & "    ON                                                         " & vbNewLine _
                                   & "    INKA_M.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.NRS_BR_CD = INKA_M.NRS_BR_CD                        " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.INKA_NO_L = INKA_M.INKA_NO_L                        " & vbNewLine _
                                   & "   LEFT JOIN $LM_TRN$..F_UNSO_L UNSO_L                         " & vbNewLine _
                                   & "    ON                                                         " & vbNewLine _
                                   & "    INKA_M.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.NRS_BR_CD = UNSO_L.NRS_BR_CD                        " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.INKA_NO_L = UNSO_L.INOUTKA_NO_L                     " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.CUST_CD_L = UNSO_L.CUST_CD_L                        " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.CUST_CD_M = UNSO_L.CUST_CD_M                        " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    UNSO_L.MOTO_DATA_KB = '10'                                 " & vbNewLine _
                                   & "   LEFT JOIN $LM_TRN$..F_UNSO_M UNSO_M                         " & vbNewLine _
                                   & "    ON                                                         " & vbNewLine _
                                   & "    UNSO_M.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    UNSO_M.NRS_BR_CD = UNSO_L.NRS_BR_CD                        " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    UNSO_M.UNSO_NO_L = UNSO_L.UNSO_NO_L                        " & vbNewLine _
                                   & "   INNER JOIN $LM_TRN$..H_INKAEDI_L EDI_L                      " & vbNewLine _
                                   & "    ON                                                         " & vbNewLine _
                                   & "    INKA_L.NRS_BR_CD = EDI_L.NRS_BR_CD                         " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.INKA_NO_L = EDI_L.INKA_CTL_NO_L                     " & vbNewLine _
                                   & "   WHERE                                                       " & vbNewLine _
                                   & "    INKA_L.INKA_STATE_KB < '50'                                " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.CUST_CD_L = @CUST_CD_L                              " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.CUST_CD_M = @CUST_CD_M                              " & vbNewLine _
                                   & "    AND                                                        " & vbNewLine _
                                   & "    INKA_L.INKA_DATE = @INKA_DATE                              " & vbNewLine _
                                   & "   GROUP BY                                                    " & vbNewLine _
                                   & "    INKA_L.INKA_NO_L                                           " & vbNewLine _
                                   & "   ,INKA_L.INKA_TTL_NB                                         " & vbNewLine _
                                   & "   ,CASE WHEN (SUBSTRING(EDI_L.FREE_C30,4,9) <> '' AND SUBSTRING(EDI_L.FREE_C30,1,2) = '05')   " & vbNewLine _
                                   & "     THEN SUBSTRING(EDI_L.FREE_C30,4,9)                        " & vbNewLine _
                                   & "     ELSE EDI_L.EDI_CTL_NO                                     " & vbNewLine _
                                   & "     END                                                       " & vbNewLine _
                                   & "   ,INKA_L.SYS_UPD_DATE                                        " & vbNewLine _
                                   & "   ,INKA_L.SYS_UPD_TIME                                        " & vbNewLine _
                                   & "   ,UNSO_L.UNSO_NO_L                                           " & vbNewLine _
                                   & "   ,UNSO_L.UNSO_PKG_NB                                         " & vbNewLine _
                                   & "   ,UNSO_L.SYS_UPD_DATE                                        " & vbNewLine _
                                   & "   ,UNSO_L.SYS_UPD_TIME                                        " & vbNewLine _
                                   & "   ,INKA_L.NRS_BR_CD                                           " & vbNewLine _
                                   & " ) QUERY                                                       " & vbNewLine

    Private Const SQL_ORDER_BY_MATOME_TARGET_IKKATU As String = "ORDER BY                            " & vbNewLine _
                                   & " QUERY.INKA_STATE_KB_1                                         " & vbNewLine _
                                   & ",QUERY.INKA_NO_L                                               " & vbNewLine



    'ローム追加箇所 20150422 追加END
    '2015.05.12 ローム　入荷登録一括対応 修正END

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
                                & " INOUT_KB  = '1'                                                      " & vbNewLine

#End Region
    '共通対応 20120223 end

    '要望番号:944（後からまとめをするとアベンド） 2012/05/25 Honmyo Start
#Region "SQL_GetMaxINKA_NO_M"
    Private Const SQL_GetMaxINKA_NO_M As String = _
                                       " SELECT CASE WHEN MAX(INKA_NO_M) IS NULL THEN 0 ELSE MAX(INKA_NO_M) END " & vbNewLine _
                                     & " FROM                                                   " & vbNewLine _
                                     & "    $LM_TRN$..B_INKA_M                                  " & vbNewLine _
                                     & " WHERE                                                  " & vbNewLine _
                                     & "    NRS_BR_CD=@NRS_BR_CD AND INKA_NO_L=@INKA_NO_L       " & vbNewLine
#End Region
    '要望番号:944（後からまとめをするとアベンド） 2012/05/25 Honmyo End



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
                                & "-- INOUT_KB  = '0'                                                    " & vbNewLine _
                                & " INOUT_KB  = '1'                                                      " & vbNewLine

#End Region
    '■■キャッシュ更新までの暫定対応　End■■

    '印刷フラグ更新対応 2012.03.18 修正START
#Region "PRINT_FLAG"
    ''' <summary>
    ''' 印刷フラグ更新のUPDATE文
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_PRTFLG_HED As String = "UPDATE $LM_TRN$..$RCV_NM$ SET                     " & vbNewLine _
                                              & " PRTFLG            = @PRTFLG                       " & vbNewLine _
                                              & ",PRT_DATE          = @PRT_DATE                     " & vbNewLine _
                                              & ",PRT_TIME          = @PRT_TIME                     " & vbNewLine _
                                              & ",PRT_USER          = @PRT_USER                     " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                    " & vbNewLine _
                                              & "--AND NRS_WH_CD    = @WH_CD                        " & vbNewLine _
                                              & "AND CUST_CD_L      = @CUST_CD_L                    " & vbNewLine _
                                              & "AND CUST_CD_M      = @CUST_CD_M                    " & vbNewLine _
                                              & "AND PRTFLG         = '0'                           " & vbNewLine


    '修正 terakawa 2012.11.27 Start
    ''千葉EDI受信帳票START　2012/11/06
    'Private Const SQL_UPD_PRTFLG_EDI_HED As String = "UPDATE $LM_TRN$..$RCV_NM$ SET  				 " & vbNewLine _
    '                                          & " PRTFLG            = @PRTFLG                       " & vbNewLine _
    '                                          & ",PRT_DATE          = @PRT_DATE                     " & vbNewLine _
    '                                          & ",PRT_TIME          = @PRT_TIME                     " & vbNewLine _
    '                                          & ",PRT_USER          = @PRT_USER                     " & vbNewLine _
    '                                          & ",SYS_UPD_DATE      = @SYS_UPD_DATE                 " & vbNewLine _
    '                                          & ",SYS_UPD_TIME      = @SYS_UPD_TIME                 " & vbNewLine _
    '                                          & ",SYS_UPD_USER      = @SYS_UPD_USER                 " & vbNewLine _
    '                                          & ",SYS_UPD_PGID      = @SYS_UPD_PGID                 " & vbNewLine _
    '                                          & "WHERE   NRS_BR_CD  = @NRS_BR_CD                    " & vbNewLine _
    '                                          & "--AND NRS_WH_CD    = @WH_CD                        " & vbNewLine _
    '                                          & "AND '00145'        = @CUST_CD_L                    " & vbNewLine _
    '                                          & "AND '00'           = @CUST_CD_M                    " & vbNewLine
    ''千葉EDI受信帳票END　2012/11/06
    '修正 terakawa 2012.11.27 End

    Private Const SQL_UPD_PRTFLG_DTL As String = "UPDATE $LM_TRN$..$RCV_NM$ SET  			" & vbNewLine _
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

    Private Const SQL_FROM_PRTFLG_INOUT As String = "AND INOUT_KB = '1'                             " & vbNewLine


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

    '2015.09.04 tsunehira add
#Region "荷主一括変更・変更先商品key取得"
    Private Const SQL_CHG_GOODSKYE As String = "SELECT                                                      　　                        " & vbNewLine _
                                    & "	EDI_M.NRS_BR_CD            AS  NRS_BR_CD            　         	                                " & vbNewLine _
                                    & "	,EDI_M.EDI_CTL_NO          AS  EDI_CTL_NO                    	                                " & vbNewLine _
                                    & "	,EDI_M.EDI_CTL_NO_CHU      AS  EDI_CTL_NO_CHU                	                                " & vbNewLine _
                                    & "	,MGOODS.CUST_CD_L          AS  CUST_CD_L                     	                                " & vbNewLine _
                                    & "	,MGOODS.CUST_CD_M          AS  CUST_CD_M                     	                                " & vbNewLine _
                                    & "	,MGOODS.GOODS_CD_NRS       AS  NRS_GOODS_CD	                                                    " & vbNewLine _
                                    & "	,MGOODS.STD_IRIME_NB       AS  STD_IRIME	                                                    " & vbNewLine _
                                    & "--(2015.10.09) ローム荷主変更対応START	                                                        " & vbNewLine _
                                    & "--	,EDI_M.IRIME               AS  IRIME	                                                    " & vbNewLine _
                                    & "	,MGOODS.STD_IRIME_NB       AS  IRIME	                                                        " & vbNewLine _
                                    & "--(2015.10.09) ローム荷主変更対応END	                                                            " & vbNewLine _
                                    & "	,MGOODS.PKG_NB             AS  PKG_NB	                                                        " & vbNewLine _
                                    & "	,CASE 	                                                                                        " & vbNewLine _
                                    & "	    WHEN EDI_M.NB = 0 THEN	                                                                    " & vbNewLine _
                                    & "	        FLOOR(EDI_M.IRIME / MGOODS.STD_IRIME_NB)	                                            " & vbNewLine _
                                    & "	    ELSE EDI_M.NB 	                                                                            " & vbNewLine _
                                    & "	  END                      AS NB	                                                            " & vbNewLine _
                                    & "	,CASE 	                                                                                        " & vbNewLine _
                                    & "	    WHEN EDI_M.NB = 0 THEN	                                                                    " & vbNewLine _
                                    & "	       FLOOR(EDI_M.IRIME / MGOODS.STD_IRIME_NB)	                                                " & vbNewLine _
                                    & "	    ELSE EDI_M.NB 	                                                                            " & vbNewLine _
                                    & "	  END                      AS INKA_PKG_NB	                                                    " & vbNewLine _
                                    & "	 ,CASE  	                                                                                    " & vbNewLine _
                                    & "	    WHEN EDI_M.NB = 0 AND EDI_M.IRIME % MGOODS.STD_IRIME_NB > 0 THEN	                        " & vbNewLine _
                                    & "	        (EDI_M.IRIME % MGOODS.STD_IRIME_NB) / (MGOODS.STD_IRIME_NB / MGOODS.PKG_NB)	            " & vbNewLine _
                                    & "	    ELSE EDI_M.HASU	                                                                            " & vbNewLine _
                                    & "	END                        AS HASU	                                                            " & vbNewLine _
                                    & "		                                                                                            " & vbNewLine _
                                    & "FROM                                                                                             " & vbNewLine _
                                    & "     $LM_TRN$..H_INKAEDI_M EDI_M                                                                 " & vbNewLine _
                                    & "     LEFT OUTER JOIN                                                                             " & vbNewLine _
                                    & "     $LM_TRN$..H_INKAEDI_L EDI_L                                                                 " & vbNewLine _
                                    & "     ON EDI_M.NRS_BR_CD = EDI_L.NRS_BR_CD                                                        " & vbNewLine _
                                    & "     AND EDI_M.EDI_CTL_NO = EDI_L.EDI_CTL_NO                                                     " & vbNewLine _
                                    & "     LEFT OUTER JOIN                                                                             " & vbNewLine _
                                    & "     (                                                                                           " & vbNewLine _
                                    & "         SELECT                                                                                  " & vbNewLine _
                                    & "             NEW_MG.NRS_BR_CD                                                                    " & vbNewLine _
                                    & "             ,NEW_MG.CUST_CD_L                                                                   " & vbNewLine _
                                    & "             ,NEW_MG.CUST_CD_M                                                                   " & vbNewLine _
                                    & "             ,NEW_MG.GOODS_CD_CUST                                                               " & vbNewLine _
                                    & "             ,CASE                                                                               " & vbNewLine _
                                    & "                 WHEN (COUNT(GOODS_CD_CUST)) >= 2 THEN NULL                                      " & vbNewLine _
                                    & "                 WHEN (COUNT(GOODS_CD_CUST))  = 0 THEN NULL                                      " & vbNewLine _
                                    & "                 ELSE NEW_MG.GOODS_CD_NRS                                                        " & vbNewLine _
                                    & "             END GOODS_CD_NRS                                                                    " & vbNewLine _
                                    & "             ,NEW_MG.STD_IRIME_NB                                                                " & vbNewLine _
                                    & "             ,NEW_MG.PKG_NB                                                                      " & vbNewLine _
                                    & "                                                                                                 " & vbNewLine _
                                    & "         FROM                                                                                    " & vbNewLine _
                                    & "             $LM_MST$..M_GOODS NEW_MG                                                            " & vbNewLine _
                                    & "                                                                                                 " & vbNewLine _
                                    & "         WHERE                                                                                   " & vbNewLine _
                                    & "             NEW_MG.NRS_BR_CD = @NRS_BR_CD                                                       " & vbNewLine _
                                    & "             AND NEW_MG.CUST_CD_L = @CUST_CD_L                                                   " & vbNewLine _
                                    & "                                                                                                 " & vbNewLine _
                                    & "         GROUP BY                                                                                " & vbNewLine _
                                    & "             NEW_MG.NRS_BR_CD                                                                    " & vbNewLine _
                                    & "             ,NEW_MG.CUST_CD_L                                                                   " & vbNewLine _
                                    & "             ,NEW_MG.CUST_CD_M                                                                   " & vbNewLine _
                                    & "             ,NEW_MG.GOODS_CD_CUST                                                               " & vbNewLine _
                                    & "             ,NEW_MG.GOODS_CD_NRS                                                                " & vbNewLine _
                                    & "             ,NEW_MG.STD_IRIME_NB                                                                " & vbNewLine _
                                    & "             ,NEW_MG.PKG_NB                                                                      " & vbNewLine _
                                    & "                                                                                                 " & vbNewLine _
                                    & "     ) MGOODS                                                                                    " & vbNewLine _
                                    & "         ON                                                                                      " & vbNewLine _
                                    & "             EDI_M.NRS_BR_CD = MGOODS.NRS_BR_CD                                                  " & vbNewLine _
                                    & "             AND EDI_M.CUST_GOODS_CD = MGOODS.GOODS_CD_CUST                                      " & vbNewLine _
                                    & "                                                                                                 " & vbNewLine _
                                    & "         WHERE                                                                                   " & vbNewLine _
                                    & "             EDI_M.NRS_BR_CD = @NRS_BR_CD                                                        " & vbNewLine _
                                    & "             AND EDI_L.EDI_CTL_NO = @EDI_CTL_NO                                                  " & vbNewLine



#End Region





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
    ''' マスタスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstSchemaNm As String

    ''' <summary>
    ''' トランザクションスキーマ名用
    ''' </summary>
    ''' <remarks></remarks>
    Private _TrnSchemaNm As String

#End Region 'Field

#Region "Method"

#Region "Z_KBN"

    ''' <summary>
    ''' Z_KBN
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataZKbn(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_Z_KBN)

        Dim dt As DataTable = ds.Tables("LMH010_JUDGE")
        Dim dtEdiL As DataTable = ds.Tables("LMH010_INKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetKbnParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dtEdiL.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectDataZKbn", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMH010_Z_KBN_IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_Z_KBN_HANYO)
        If Me._Row.Item("KBN_CD") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_CD").ToString() <> "" Then
            Me._StrSql.Append("   AND KBN_CD = @KBN_CD                " & vbNewLine)
        End If
        If Me._Row.Item("KBN_NM1") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM1").ToString() <> "" Then
            Me._StrSql.Append("   AND KBN_NM1 = @KBN_NM1              " & vbNewLine)
        End If
        If Me._Row.Item("KBN_NM2") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM2").ToString() <> "" Then
            Me._StrSql.Append("   AND KBN_NM2 = @KBN_NM2              " & vbNewLine)
        End If
        If Me._Row.Item("KBN_NM3") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM3").ToString() <> "" Then
            Me._StrSql.Append("   AND KBN_NM3 = @KBN_NM3              " & vbNewLine)
        End If
        If Me._Row.Item("KBN_NM4") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM4").ToString() <> "" Then
            Me._StrSql.Append("   AND KBN_NM4 = @KBN_NM4              " & vbNewLine)
        End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString())

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
        If Me._Row.Item("KBN_NM2") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM2").ToString() <> "" Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_NM2", Me._Row.Item("KBN_NM2").ToString(), DBDataType.NVARCHAR))
        End If
        If Me._Row.Item("KBN_NM3") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM3").ToString() <> "" Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_NM3", Me._Row.Item("KBN_NM3").ToString(), DBDataType.NVARCHAR))
        End If
        If Me._Row.Item("KBN_NM4") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM4").ToString() <> "" Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_NM4", Me._Row.Item("KBN_NM4").ToString(), DBDataType.NVARCHAR))
        End If

        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectZKbnHanyo", cmd)

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

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH010_Z_KBN_OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMH010_Z_KBN_OUT").Rows.Count())
        reader.Close()

        Return ds

    End Function

#End Region

#Region "M_SOKO"

    ''' <summary>
    ''' M_SOKO
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataSoko(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_L")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_WH)

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

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectDataSoko", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function

#End Region

#Region "M_CUST"

    ''' <summary>
    ''' 件数取得処理(荷主マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataCust(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_M_CUST)

        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetCustParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectDataCust", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function

#End Region

#Region "M_DEST"

    ''' <summary>
    ''' 件数取得処理(届先マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataDest(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_M_DEST)

        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetDestParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectDataDest", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function

#End Region

#Region "M_UNSOCO"

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
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_M_UNSOCO)

        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_L")

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

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectDataUnsoco", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function

#End Region

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

        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_L")

        'INTableの条件rowの格納
        Me._Row = dt.Rows(0)

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_M_UNSOCO_SHIHARAI)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUnsocoParameter(dt)

        'SQL条件設定(Group by)
        Me._StrSql.Append(LMH010DAC.SQL_GROUP_BY_M_UNSOCO_SHIHARAI)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectListDataShiharaiTariff", cmd)

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

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH010_SHIHARAI_TARIFF")

            '処理件数の設定
            unsoShiharaiCnt = ds.Tables("LMH010_SHIHARAI_TARIFF").Rows.Count

        End If

        reader.Close()

        MyBase.SetResultCount(unsoShiharaiCnt)
        Return ds

    End Function

#End Region
    'END UMANO 要望番号1302 支払運賃に伴う修正。


#Region "M_UNCHIN_TARIFF"

    ''' <summary>
    ''' 件数取得処理(運賃タリフ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataUnchinTariff(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_M_UNCHIN_TARIFF)

        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUnchinTariffParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dt.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectDataUnchinTarif", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function

#End Region

#Region "M_YOKO_TARIFF"

    ''' <summary>
    ''' 件数取得処理(横持ちタリフ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataYokoTariff(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_M_YOKO_TARIFF)

        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_L")

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

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectDataYokoTarif", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function

#End Region

#Region "M_GOODS"

    ''' <summary>
    ''' 件数取得処理(商品)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataGoods(ByVal ds As DataSet) As DataSet

        Dim dtM As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim dtL As DataTable = ds.Tables("LMH010_INKAEDI_L")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_M_GOODS)

        Me._SqlPrmList = New ArrayList()

        'SQL条件設定
        Call Me.SetConditionGoodCd(dtM, 1)

        'パラメータ設定
        Call Me.SetGoodsParameter(dtM, dtL)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dtM.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectDataGoods", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()
        Dim goodsCnt As Integer = 0

        If reader.HasRows() = True Then

            Call Me.SetGoodsMap(reader, map)

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH010_M_GOODS")

            '処理件数の設定
            goodsCnt = ds.Tables("LMH010_M_GOODS").Rows.Count

        End If
        reader.Close()

        MyBase.SetResultCount(goodsCnt)
        Return ds

    End Function

    ''' <summary>
    ''' 件数取得処理(商品)荷主商品コード + 入目
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataGoods2(ByVal ds As DataSet) As DataSet

        '商品テーブルの初期化
        ds.Tables("LMH010_M_GOODS").Clear()

        Dim dtM As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim dtL As DataTable = ds.Tables("LMH010_INKAEDI_L")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_M_GOODS)

        Me._SqlPrmList = New ArrayList()

        'SQL条件設定
        Call Me.SetConditionGoodCd(dtM, 2)

        'パラメータ設定
        Call Me.SetGoodsParameter(dtM, dtL)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dtM.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectDataGoods", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()
        Dim goodsCnt As Integer = 0

        If reader.HasRows() = True Then

            Call Me.SetGoodsMap(reader, map)

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH010_M_GOODS")

            '処理件数の設定
            goodsCnt = ds.Tables("LMH010_M_GOODS").Rows.Count

        End If
        reader.Close()

        MyBase.SetResultCount(goodsCnt)
        Return ds

    End Function

#End Region

    '要望番号1003 2012.05.08 追加START
#Region "M_GOODS_DETAILS"

    ''' <summary>
    ''' データ取得処理(商品明細マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataGoodsMeisaiOkiba(ByVal ds As DataSet) As DataSet

        Dim dtMgoods As DataTable = ds.Tables("LMH010_M_GOODS")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_M_GOODS_DETAILS)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetGoodsMeisaiParameter(dtMgoods)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dtMgoods.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectDataGoodsMeisaiOkiba", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()
        Dim goodsCnt As Integer = 0

        If reader.HasRows() = True Then

            map.Add("SET_NAIYO", "SET_NAIYO")

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH010_M_GOODS_DETAILS")

            '処理件数の設定
            goodsCnt = ds.Tables("LMH010_M_GOODS_DETAILS").Rows.Count

        End If
        reader.Close()

        MyBase.SetResultCount(goodsCnt)
        Return ds

    End Function

#End Region
    '要望番号1003 2012.05.08 追加END

    '2015.08.24 BYK セミEDI対応START
#Region "M_CUST_DETAILS"

    ''' <summary>
    ''' データ取得処理(荷主明細マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataCustMeisaiOkiba(ByVal ds As DataSet) As DataSet

        Dim dtInkaL As DataTable = ds.Tables("LMH010_INKAEDI_L")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_M_CUST_DETAILS)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetCustParameter(dtInkaL)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dtInkaL.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectDataCustMeisaiOkiba", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()
        Dim custCnt As Integer = 0

        If reader.HasRows() = True Then

            map.Add("SET_NAIYO", "SET_NAIYO")

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH010_M_CUST_DETAILS")

            '処理件数の設定
            custCnt = ds.Tables("LMH010_M_CUST_DETAILS").Rows.Count

        End If
        reader.Close()

        MyBase.SetResultCount(custCnt)
        Return ds

    End Function

#End Region
    '2015.08.24 BYK セミEDI対応END

#Region "INKA_L"

    ''' <summary>
    ''' 件数取得処理(INKA_L)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>オーダー番号重複チェック</remarks>
    Private Function SelectOrderCheckData(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_INKA_L)

        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_L")

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

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectDataOrderCheck", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("INKA_L_CNT")))
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
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_PKG_UT_Z_KBN)

        Dim dt As DataTable = ds.Tables("LMH010_JUDGE")
        Dim dtM As DataTable = ds.Tables("LMH010_INKAEDI_M")

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

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectNisugataValue", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        reader.Read()

        dtM.Rows(0)("NISUGATA") = reader("NISUGATA")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "横持ちタリフ取得処理(タリフセット)"

    ''' <summary>
    ''' 横持ちタリフ取得処理(タリフセット)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataUnchinTariffSet(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_M_UNCHIN_TARIFF_SET)

        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_L")

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

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectDataUnchinTariffSet", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        If reader.Read() = True Then

            If String.IsNullOrEmpty(reader("YOKO_TARIFF_CD").ToString) = False Then
                dt.Rows(0)("YOKO_TARIFF_CD") = reader("YOKO_TARIFF_CD")
            End If
        End If

        reader.Close()

        Return ds

    End Function

#End Region

#Region "タリフ分類取得処理(タリフセット)"

    ''' <summary>
    ''' タリフ分類取得処理(タリフセット)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataTariffBunrui(ByVal ds As DataSet) As DataSet

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_M_TARIFF_BUNRUI)

        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_L")

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

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectDataTariffBunrui", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        If reader.Read() = True Then

            If String.IsNullOrEmpty(reader("TARIFF_BUNRUI_KB").ToString) = False Then
                dt.Rows(0)("UNCHIN_KB") = reader("TARIFF_BUNRUI_KB")
            End If

        End If

        reader.Close()

        Return ds

    End Function

#End Region

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
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_TABLET_YN_Z_KBN)

        Dim dt As DataTable = ds.Tables("LMH010_JUDGE")
        Dim dtEdiL As DataTable = ds.Tables("LMH010_INKAEDI_L")

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetKbnTabletYNParameter(dt)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), dtEdiL.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectDataTabletYN", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds
    End Function

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

#Region "商品マスタ取得条件"
    Private Sub SetConditionGoodCd(ByVal dt As DataTable, ByVal cnt As Integer)

        Dim goodsCdNrs As String = dt.Rows(0)("NRS_GOODS_CD").ToString()
        Dim goodsCdCust As String = dt.Rows(0)("CUST_GOODS_CD").ToString()
        Dim irimeUt As String = dt.Rows(0)("IRIME_UT").ToString
        Dim strSql As String = String.Empty

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" NRS_BR_CD = @NRS_BR_CD ")
        Me._StrSql.Append(vbNewLine)

        '日陸商品コードが無ければ荷主商品コード
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

        '▼▼▼(商品取得不具合対応)
        Me._StrSql.Append(" AND SYS_DEL_FLG = '0' ")
        Me._StrSql.Append(vbNewLine)
        '▲▲▲(商品取得不具合対応)

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

        Return map

    End Function

#End Region

#Region "パラメータ設定"
    ''' <summary>
    ''' Z_Kbn
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKbnParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_GROUP_CD", dt.Rows(0).Item("KBN_GROUP_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_CD", dt.Rows(0).Item("KBN_CD"), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' M_SOKO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSokoParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", dt.Rows(0).Item("NRS_WH_CD"), DBDataType.CHAR))

    End Sub


    ''' <summary>
    ''' M_CUST
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCustParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", dt.Rows(0).Item("CUST_CD_M"), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' M_DEST
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDestParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_MOTO", dt.Rows(0).Item("OUTKA_MOTO"), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' M_UNSOCO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUnsocoParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", dt.Rows(0).Item("UNSO_CD"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", dt.Rows(0).Item("UNSO_BR_CD"), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' M_UNCHIN_TARIFF
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUnchinTariffParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", dt.Rows(0).Item("YOKO_TARIFF_CD"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STR_DATE", dt.Rows(0).Item("INKA_DATE"), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' M_YOKO_TARIFF
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetYokoTariffParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKO_TARIFF_CD", dt.Rows(0).Item("YOKO_TARIFF_CD"), DBDataType.NVARCHAR))

    End Sub


    ''' <summary>
    ''' M_Goods
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetGoodsParameter(ByVal dtM As DataTable, ByVal dtL As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dtM.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", Me.NullConvertString(dtM.Rows(0).Item("NRS_GOODS_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", Me.NullConvertString(dtM.Rows(0).Item("CUST_GOODS_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me.NullConvertZero(dtM.Rows(0).Item("IRIME")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", Me.NullConvertString(dtM.Rows(0).Item("IRIME_UT")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dtL.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", dtL.Rows(0).Item("CUST_CD_M"), DBDataType.CHAR))

    End Sub

    '要望番号1003 2012.05.08 追加START

    ''' <summary>
    ''' M_GOODS_DETAILS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetGoodsMeisaiParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", Me.NullConvertString(dt.Rows(0).Item("GOODS_CD_NRS")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(dt.Rows(0).Item("NRS_BR_CD")), DBDataType.CHAR))

    End Sub

    '要望番号1003 2012.05.08 追加END

    ''' <summary>
    ''' B_INKA_L
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInkaParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO", Me.NullConvertString(dt.Rows(0).Item("OUTKA_FROM_ORD_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", dt.Rows(0).Item("CUST_CD_M"), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' M_UNCHIN_TARIFF_SET
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetUnchinTariffSetParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dt.Rows(0).Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dt.Rows(0).Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", dt.Rows(0).Item("CUST_CD_M"), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' Z_Kbn(Tablet使用有無取得)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKbnTabletYNParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_GROUP_CD", dt.Rows(0).Item("KBN_GROUP_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_CD", dt.Rows(0).Item("KBN_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@VALUE1", dt.Rows(0).Item("VALUE1"), DBDataType.NUMERIC))

    End Sub

#End Region

#Region "件数取得処理"

    ''' <summary>
    ''' 件数取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH010INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_COUNT_DATA)
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_FROM)

        '条件設定
        Call Me.SetConditionMasterSelect()

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        '受信HEDテーブル名設定
        sql = Me.SetRcvHedTableNm(sql)

        '送信テーブル名設定
        sql = Me.SetSendTableNm(sql)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'タイムアウトの設定
        cmd.CommandTimeout = 1200

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        Return ds
    End Function

#End Region

#Region "検索データ取得処理"

    ''' <summary>
    ''' データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH010INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_DATA)

        Me.AddSqlSelect()

        Me._StrSql.Append(LMH010DAC.SQL_SELECT_FROM)

        '▼▼▼二次
        '追加From句
        Call Me.AddSqlFrom(ds)
        '▲▲▲二次

        '条件設定
        Call Me.SetConditionMasterSelect()

        Me._StrSql.Append(LMH010DAC.SQL_SELECT_ORDER)

        'スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        '受信HEDテーブル名設定
        sql = Me.SetRcvHedTableNm(sql)

        '▼▼▼二次
        '受信DTLテーブル名設定
        sql = Me.SetRcvDtlTableNm(sql)
        '▲▲▲二次

        '送信テーブル名設定
        sql = Me.SetSendTableNm(sql)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectListData", cmd)

        'Debug.Print(cmd.CommandText)
        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_WH_CD", "NRS_WH_CD")
        map.Add("OUTKA_FROM_ORD_NO", "OUTKA_FROM_ORD_NO")
        map.Add("INKA_STATE_NM", "INKA_STATE_NM")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("NB", "NB")
        map.Add("INKA_TTL_NB", "INKA_TTL_NB")
        map.Add("RECCNT", "RECCNT")
        map.Add("UNSO_KB", "UNSO_KB")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        '2013.04.03 Notes1995 START
        map.Add("OUTKA_MOTO_NM", "OUTKA_MOTO_NM")
        '2013.04.03 Notes1995 END
        '2012.02.15 大阪対応追加START
        map.Add("MATOME_NO", "MATOME_NO")
        '2012.02.15 大阪対応追加END
        map.Add("INKA_CTL_NO_L", "INKA_CTL_NO_L")
        map.Add("BUYER_ORD_NO_L", "BUYER_ORD_NO_L")
        map.Add("INKA_TP_NM", "INKA_TP_NM")
        map.Add("FREE_C30", "FREE_C30")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("CRT_TIME", "CRT_TIME")
        map.Add("SEND_DATE", "SEND_DATE")
        map.Add("SEND_TIME", "SEND_TIME")
        map.Add("TANTO_USER", "TANTO_USER")
        map.Add("CRT_USER", "CRT_USER")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("DEL_KB", "DEL_KB")
        map.Add("JISSEKI_FLAG", "JISSEKI_FLAG")
        map.Add("OUT_FLAG", "OUT_FLAG")
        map.Add("MIN_NB", "MIN_NB")
        map.Add("MAX_AKAKURO_KB", "MAX_AKAKURO_KB")
        map.Add("EDI_CUST_JISSEKI", "EDI_CUST_JISSEKI")
        map.Add("EDI_CUST_MATOME", "EDI_CUST_MATOME")
        map.Add("EDI_CUST_SPECIAL", "EDI_CUST_SPECIAL")
        map.Add("EDI_CUST_HOLDOUT", "EDI_CUST_HOLDOUT")
        map.Add("EDI_CUST_UNSO", "EDI_CUST_UNSO")
        map.Add("EDI_CUST_INDEX", "EDI_CUST_INDEX")
        map.Add("SND_UPD_DATE", "SND_UPD_DATE")
        map.Add("SND_UPD_TIME", "SND_UPD_TIME")
        map.Add("RCV_UPD_DATE", "RCV_UPD_DATE")
        map.Add("RCV_UPD_TIME", "RCV_UPD_TIME")
        map.Add("INKA_UPD_DATE", "INKA_UPD_DATE")
        map.Add("INKA_UPD_TIME", "INKA_UPD_TIME")
        map.Add("JYOTAI", "JYOTAI")
        map.Add("HORYU", "HORYU")
        map.Add("INKA_STATE_KB", "INKA_STATE_KB")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("INKA_DEL_FLG", "INKA_DEL_FLG")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("ORDER_CHECK_FLG", "ORDER_CHECK_FLG")
        map.Add("AUTO_MATOME_FLG", "AUTO_MATOME_FLG")
        '▼▼▼二次
        map.Add("RCV_NM_HED", "RCV_NM_HED")
        map.Add("RCV_NM_DTL", "RCV_NM_DTL")
        map.Add("RCV_NM_EXT", "RCV_NM_EXT")
        map.Add("SND_NM", "SND_NM")
        '2012.02.25 大阪対応 START
        map.Add("EDI_CUST_INOUTFLG", "EDI_CUST_INOUTFLG")
        '2012.02.25 大阪対応 END
        '2013.04.03 Notes1995 START
        map.Add("OUTKA_MOTO", "OUTKA_MOTO")
        '2013.04.03 Notes1995 END
        '受信DTL排他用コメントアウト
        'map.Add("RCV_DTL_UPD_DATE", "RCV_DTL_UPD_DATE")
        'map.Add("RCV_DTL_UPD_TIME", "RCV_DTL_UPD_TIME")
        '▲▲▲二次
        map.Add("CHG_CUST_CD", "CHG_CUST_CD")
        map.Add("KOSU_FLG", "KOSU_FLG")         'ADD 2017/05/09
        map.Add("OUTKA_CTL_NO_COND_M", "OUTKA_CTL_NO_COND_M")

        map.Add("GENPINHYO_CHKFLG", "GENPINHYO_CHKFLG")     'ADD 2019/12/18 009991

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH010OUT")
        reader.Close()

        Return ds

    End Function

#End Region

#Region "Select句追加処理"
    Private Sub AddSqlSelect()

        If (Me.IsShowCondM) Then
            Me._StrSql.Append(LMH010DAC.SQL_SELECT_DATA_ADD_COND_M_ON)
        Else
            Me._StrSql.Append(LMH010DAC.SQL_SELECT_DATA_ADD_COND_M_OFF)
        End If
    End Sub
#End Region

    '▼▼▼二次
#Region "FROM句追加処理"
    ''' <summary>
    ''' FROM句追加処理
    ''' </summary>
    ''' <remarks>送信テーブル,受信Hテーブルの有無でSQLのJOIN条件を替える</remarks>
    Private Sub AddSqlFrom(ByVal ds As DataSet)

        Dim sendTbl As String = ds.Tables("LMH010INOUT").Rows(0)("SND_NM").ToString()    '送信テーブル
        Dim rcvHedTbl As String = ds.Tables("LMH010INOUT").Rows(0)("RCV_NM_HED").ToString() '受信HEDテーブル
        Dim rcvDtlTbl As String = ds.Tables("LMH010INOUT").Rows(0)("RCV_NM_DTL").ToString() '受信DTLテーブル
        Dim inOutFlg As String = ds.Tables("LMH010INOUT").Rows(0)("EDI_CUST_INOUTFLG").ToString()  'EDI荷主テーブル入出荷FLG

        '送信TBL
        If String.IsNullOrEmpty(sendTbl) = True Then
            Me._StrSql.Append(LMH010DAC.SQL_FROM_SENDTABLE_NULL)
        Else
            '2012.02.25 大阪対応 START
            If inOutFlg.Equals("1") = True Then
                '2014.06.06 FFEM対応 修正START
                If EdiCustIndex.Fjf00195_00 = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(0)("EDI_CUST_INDEX")) Then
                    Me._StrSql.Append(LMH010DAC.SQL_FROM_SENDTABLE_NORMAL)
                Else
                    Me._StrSql.Append(LMH010DAC.SQL_FROM_SENDTABLE)
                    Me._StrSql.Append(LMH010DAC.SQL_FROM_SENDTABLE_INOUT)
                End If
                '2014.06.06 FFEM対応 修正END
            Else
                Me._StrSql.Append(LMH010DAC.SQL_FROM_SENDTABLE_NORMAL)
            End If
            '2012.02.25 大阪対応 END
        End If

        '受信HED
        If String.IsNullOrEmpty(rcvHedTbl) = True Then
            Me._StrSql.Append(LMH010DAC.SQL_FROM_RCV_HED_NULL)
        Else
            Me._StrSql.Append(LMH010DAC.SQL_FROM_RCV_HED)
            '2012.02.25 大阪対応 START
            If inOutFlg.Equals("1") = True Then
                Me._StrSql.Append(LMH010DAC.SQL_FROM_RCVTABLE_INOUT)
            Else
            End If
            '2012.02.25 大阪対応 END
        End If

        '受信DTL排他用コメントアウト
        ''受信DTL
        'If String.IsNullOrEmpty(rcvDtlTbl) = True Then
        '    Me._StrSql.Append(LMH010DAC.SQL_FROM_RCV_DTL_NULL)
        'Else
        '    Me._StrSql.Append(LMH010DAC.SQL_FROM_RCV_DTL)
        'End If

        If (Me.IsShowCondM()) Then
            Me._StrSql.Append(LMH010DAC.SQL_SELECT_FROM_ADD_COND_M_ON)
        End If

    End Sub


    Private Function IsShowCondM() As Boolean

        If (_Row Is Nothing) Then
            Return False
        End If

        Return LMConst.FLG.ON.Equals(If(IsDBNull(_Row.Item("IS_SHOW_COND_M")) _
                                               , "" _
                                               , _Row.Item("IS_SHOW_COND_M")))

    End Function

#End Region
    '▲▲▲二次

#Region "条件文設定処理"
    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSelect()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim whereStr2 As String = String.Empty

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Dim connectFlg As Boolean = False
        Dim checkFlg As Boolean = False

        With Me._Row

            Me._StrSql.Append(" ( ")

            '未登録にチェックあり
            If String.IsNullOrEmpty(.Item("EDIINKA_STATE_KB1").ToString()) = False Then
                Me._StrSql.Append(" ((H_INKAEDI_L.DEL_KB IN ('0','3','2') AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" H_INKAEDI_L.OUT_FLAG = '0' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" H_INKAEDI_L.JISSEKI_FLAG IN ('0','9')) ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (M_EDI_CUST.FLAG_08 = '1' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" H_INKAEDI_L.OUT_FLAG = '0' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" H_INKAEDI_L.JISSEKI_FLAG IN ('0','9'))) ")
                Me._StrSql.Append(vbNewLine)
                connectFlg = True
                checkFlg = True
            End If

            '入荷登録済にチェックあり
            If String.IsNullOrEmpty(.Item("EDIINKA_STATE_KB2").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" ((M_EDI_CUST.FLAG_01 IN ('1','2','4') AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" B_INKA_L.SYS_DEL_FLG = '0' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" B_INKA_L.INKA_STATE_KB < '50') ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (M_EDI_CUST.FLAG_01 IN ('1','4') AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" B_INKA_L.SYS_DEL_FLG = '1') ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (M_EDI_CUST.FLAG_01 IN ('0','3','9') AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" B_INKA_L.INKA_STATE_KB IS NOT NULL)) ")
                Me._StrSql.Append(vbNewLine)
                connectFlg = True
                checkFlg = True
            End If

            '実績未にチェックあり
            If String.IsNullOrEmpty(.Item("EDIINKA_STATE_KB3").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (((M_EDI_CUST.FLAG_01 IN ('1','2','3') AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" H_INKAEDI_L.SYS_DEL_FLG = '0' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" B_INKA_L.SYS_DEL_FLG = '0' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" B_INKA_L.INKA_STATE_KB >= '50') ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (M_EDI_CUST.FLAG_01 = '2' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (H_INKAEDI_L.SYS_DEL_FLG = '1' OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" B_INKA_L.SYS_DEL_FLG = '1')) ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (M_EDI_CUST.FLAG_01 = '3' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (H_INKAEDI_L.SYS_DEL_FLG = '0' OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" H_INKAEDI_L.OUT_FLAG = '2')) ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (M_EDI_CUST.FLAG_01 = '4' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (H_INKAEDI_L.SYS_DEL_FLG = '0' OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" B_INKA_L.SYS_DEL_FLG = '0'))) ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" AND ")
                Me._StrSql.Append(" (H_INKAEDI_L.JISSEKI_FLAG = '0')) ")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True
            End If

            '実績作成済にチェックあり
            If String.IsNullOrEmpty(.Item("EDIINKA_STATE_KB4").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (H_INKAEDI_L.JISSEKI_FLAG = '1') ")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True

            End If

            '実績送信済にチェックあり
            If String.IsNullOrEmpty(.Item("EDIINKA_STATE_KB5").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (H_INKAEDI_L.JISSEKI_FLAG = '2') ")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True

            End If

            '赤データにチェックあり
            If String.IsNullOrEmpty(.Item("EDIINKA_STATE_KB6").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (H_INKAEDI_L.DEL_KB = '2') ")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True

            End If

            '取消のみにチェックあり
            If String.IsNullOrEmpty(.Item("EDIINKA_STATE_KB8").ToString()) = False Then

                If connectFlg = True Then
                    Me._StrSql.Append(" OR ")
                    Me._StrSql.Append(vbNewLine)
                End If

                Me._StrSql.Append(" (H_INKAEDI_L.DEL_KB = '1') ")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True
                checkFlg = True

            End If


            '進捗区分チェックなしは全件検索
            If checkFlg = False Then

                Me._StrSql.Append(" ((H_INKAEDI_L.DEL_KB IN ('0','3','2')) ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" OR ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (H_INKAEDI_L.DEL_KB = '1' AND ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" (M_EDI_CUST.FLAG_01 = '2' OR")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(" M_EDI_CUST.FLAG_08 = '1'))) ")
                Me._StrSql.Append(vbNewLine)

                connectFlg = True

            End If


            Me._StrSql.Append(" ) ")

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_INKAEDI_L.NRS_BR_CD = @NRS_BR_CD ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '倉庫コード
            whereStr = .Item("WH_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_INKAEDI_L.NRS_WH_CD = @WH_CD ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_INKAEDI_L.CUST_CD_L = @CUST_CD_L ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_INKAEDI_L.CUST_CD_M = @CUST_CD_M ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '担当者コード
            whereStr = .Item("TANTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TANTO_USER.USER_CD LIKE @TANTO_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TANTO_CD", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            'EDI取込日(FROM)
            whereStr = .Item("TORIKOMI_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_INKAEDI_L.CRT_DATE >= @TORIKOMI_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TORIKOMI_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            'EDI取込日(TO)
            whereStr = .Item("TORIKOMI_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_INKAEDI_L.CRT_DATE <= @TORIKOMI_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TORIKOMI_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            'EDI取込日(FROM), EDI取込日(TO) 両方省略時、件数取得 SQL が本番環境のみタイムアウトする事象の回避ステートメント
            If String.IsNullOrEmpty(.Item("TORIKOMI_DATE_FROM").ToString()) AndAlso String.IsNullOrEmpty(.Item("TORIKOMI_DATE_TO").ToString()) Then
                Me._StrSql.Append(String.Concat(" AND(H_INKAEDI_L.CRT_DATE >= '00000000' OR RTRIM(H_INKAEDI_L.CRT_DATE) = '')", vbNewLine))
            End If

            '入荷日(FROM)
            whereStr = .Item("INKA_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ISNULL(B_INKA_L.INKA_DATE,H_INKAEDI_L.INKA_DATE) >= @INKA_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            '入荷日(TO)
            whereStr = .Item("INKA_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND ISNULL(B_INKA_L.INKA_DATE,H_INKAEDI_L.INKA_DATE) <= @INKA_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            '状態
            whereStr = .Item("JYOTAI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_INKAEDI_L.SYS_DEL_FLG = @JYOTAI_KB ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JYOTAI_KB", whereStr, DBDataType.CHAR))
            End If

            '保留
            whereStr = .Item("HORYU_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If whereStr.Equals("3") Then
                    Me._StrSql.Append(" AND H_INKAEDI_L.DEL_KB = '3' ")
                Else
                    Me._StrSql.Append(" AND H_INKAEDI_L.DEL_KB <> '3' ")
                End If
            End If

            '入荷種別
            whereStr = .Item("INKA_TP").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_INKAEDI_L.INKA_TP = @INKA_TP ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TP", whereStr, DBDataType.CHAR))
            End If

            '入荷区分
            whereStr = .Item("INKA_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_INKAEDI_L.INKA_KB = @INKA_KB ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KB", whereStr, DBDataType.CHAR))
            End If

            'オーダー番号
            whereStr = .Item("OUTKA_FROM_ORD_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_INKAEDI_L.OUTKA_FROM_ORD_NO LIKE @OUTKA_FROM_ORD_NO ")
                Me._StrSql.Append(vbNewLine)
#If False Then  'UPD 2019/01/23  'UPD 2019/01/23 依頼番号 : 003868   【LMS】オーダー番号の検索方法「前方一致⇒部分一致」
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
#Else
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
#End If
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND (M_CUST.CUST_NM_L + '　' + M_CUST.CUST_NM_M) LIKE @CUST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '商品名
            whereStr = .Item("GOODS_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_GOODS.GOODS_NM_1 LIKE @GOODS_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '運送元区分名
            whereStr = .Item("UNSO_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_INKAEDI_L.UNCHIN_KB = @UNSO_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_KB", whereStr, DBDataType.NVARCHAR))
            End If

            '運送会社名
            whereStr = .Item("UNSOCO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_UNSOCO.UNSOCO_NM + '　' + M_UNSOCO.UNSOCO_BR_NM LIKE @UNSOCO_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSOCO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '2013.04.03 Notes1995 START
            '運送会社名
            whereStr = .Item("OUTKA_MOTO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND M_DEST.DEST_NM LIKE @OUTKA_MOTO_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_MOTO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If
            '2013.04.03 Notes1995 END

            'EDI入荷管理番号
            whereStr = .Item("EDI_CTL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_INKAEDI_L.EDI_CTL_NO LIKE @EDI_CTL_NO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '入荷管理番号(大)
            whereStr = .Item("INKA_CTL_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_INKAEDI_L.INKA_CTL_NO_L LIKE @INKA_CTL_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '注文番号
            whereStr = .Item("BUYER_ORD_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND H_INKAEDI_L.BUYER_ORD_NO = @BUYER_ORD_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_L", whereStr, DBDataType.NVARCHAR))
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
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))  'NVARCHAR⇒VARCHAR
            End If

            '最終更新者
            whereStr = .Item("SYS_UPD_USER").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND UPD_USER.USER_NM LIKE @SYS_UPD_USER")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))  'NVARCHAR⇒VARCHAR
            End If

        End With

    End Sub
#End Region

#Region "テーブル名設定"

    ''' <summary>
    ''' 受信HEDテーブル名設定
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetRcvHedTableNm(ByVal sql As String) As String

        Dim rcvTblNm As String = Me._Row("RCV_NM_HED").ToString()
        sql = sql.Replace("$RCV_HED$", rcvTblNm)

        Return sql

    End Function

    '▼▼▼二次
    ''' <summary>
    ''' 受信DTLテーブル名設定
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetRcvDtlTableNm(ByVal sql As String) As String

        Dim rcvTblNm As String = Me._Row("RCV_NM_DTL").ToString()
        sql = sql.Replace("$RCV_DTL$", rcvTblNm)

        Return sql

    End Function
    '▲▲▲二次

    ''' <summary>
    ''' 送信テーブル名設定
    ''' </summary>
    ''' <param name="sql"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSendTableNm(ByVal sql As String) As String

        Dim sendTblNm As String = Me._Row("SND_NM").ToString()
        sql = sql.Replace("$SEND_TBL$", sendTblNm)

        Return sql

    End Function

#End Region

    '▼▼▼二次
#Region "入荷管理番号に営業所イニシャル設定"

    Private Function SetBrInitial(ByVal sql As String, ByVal dr As DataRow) As String

        Dim brInitial As String = dr("EDI_CTL_NO").ToString().Substring(0, 1)
        sql = sql.Replace("$BR_INITIAL$", String.Concat("'", brInitial, "'"))

        Return sql

    End Function

#End Region
    '▲▲▲二次

    '▼▼▼二次（共通化）
#Region "UPDATE処理"

#Region "INKAEDI_L"

    ''' <summary>
    ''' EDI(大)データ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateEdiL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_L")
        Dim nrsBrCd As String = dt.Rows(0)("NRS_BR_CD").ToString

        Dim dtIn As DataTable
        Dim autoMatomeF As String

        If (LMH010DAC.EventShubetsu.BULK_CUST_CHANGE) <> Convert.ToInt32(ds.Tables("LMH010_JUDGE").Rows(0).Item("EVENT_SHUBETSU")) Then

            '2012.02.25 大阪対応 START
            dtIn = ds.Tables("LMH010INOUT")
            autoMatomeF = dtIn.Rows(0).Item("AUTO_MATOME_FLG").ToString()
            '2012.02.25 大阪対応 END
        End If

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim eventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        Select Case DirectCast(eventShubetsu, LMH010DAC.EventShubetsu)

            '実績取消
            Case LMH010DAC.EventShubetsu.JISSEKI_TORIKESI

                setSql = LMH010DAC.SQL_UPD_JISSEKITORIKESI_EDI_L

                'EDI取消、EDI取消⇒未登録、報告用EDI取消
            Case LMH010DAC.EventShubetsu.EDI_TORIKESI _
                , LMH010DAC.EventShubetsu.JIKKOU_TORIKESI_MITOUROKU _
                , LMH010DAC.EventShubetsu.JIKKOU_HOUKOKU_EDI_TORIKESI

                setSql = LMH010DAC.SQL_UPD_EDITORIKESI_EDI_L

                '実績作成済⇒実績未、実績送信済⇒実績未
            Case LMH010DAC.EventShubetsu.JIKKOU_SAKUSEI_JISSEKIMI _
                , LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_JISSEKIMI

                setSql = LMH010DAC.SQL_UPD_JISSEKIMODOSI_EDI_L

                '入荷取消⇒未登録
            Case LMH010DAC.EventShubetsu.JIKKOU_TOUROKU_MITOUROKU

                '2012.02.25 大阪対応 START
                If autoMatomeF.Equals("9") = True Then
                    'まとめデータでない場合
                    setSql = LMH010DAC.SQL_UPD_TOUROKUMI_EDI_L

                Else
                    'まとめデータの場合
                    setSql = LMH010DAC.SQL_UPD_TOUROKUMI_EDI_L_MATOME
                End If
                'setSql = LMH010DAC.SQL_UPD_TOUROKUMI_EDI_L
                '2012.02.25 大阪対応 END

                '送信済み⇒送信待ち
            Case LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_SOUSINMI

                setSql = LMH010DAC.SQL_JISSEKIZUMI_SOUSINMACHI_EDI_L

				'2015.09.07 tsunehira add
                '荷主コード変更
            Case LMH010DAC.EventShubetsu.BULK_CUST_CHANGE

                setSql = LMH010DAC.SQL_BULK_CHG_EDI_L
                
        End Select

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(setSql, nrsBrCd))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetSysdataParameter()
        Call Me.SetUpdPrmEdiL(dt)
        Call Me.SetHaitaDateTime(dt.Rows(0))
        Call Me.SetJissekiParameterEdiLEdiM(dt.Rows(0), eventShubetsu)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "LMH010_INKAEDI_L", cmd)

        'SQLの発行
        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region

#Region "INKAEDI_M"

    ''' <summary>
    ''' EDI(中)データ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateEdiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim nrsBrCd As String = dt.Rows(0)("NRS_BR_CD").ToString

Dim dtIn As DataTable
        Dim autoMatomeF As String

        If (LMH010DAC.EventShubetsu.BULK_CUST_CHANGE) <> Convert.ToInt32(ds.Tables("LMH010_JUDGE").Rows(0).Item("EVENT_SHUBETSU")) Then


            '2012.02.25 大阪対応 START
            dtIn = ds.Tables("LMH010INOUT")
            autoMatomeF = dtIn.Rows(0)("AUTO_MATOME_FLG").ToString()
            '2012.02.25 大阪対応 END

        End If
        
        Dim eventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        Select Case DirectCast(eventShubetsu, LMH010DAC.EventShubetsu)

            'EDI取消、EDI取消⇒未登録、報告用EDI取消
            Case LMH010DAC.EventShubetsu.EDI_TORIKESI _
                , LMH010DAC.EventShubetsu.JIKKOU_TORIKESI_MITOUROKU _
                , LMH010DAC.EventShubetsu.JIKKOU_HOUKOKU_EDI_TORIKESI

                setSql = LMH010DAC.SQL_UPD_EDITORIKESI_EDI_M

                '実績作成済⇒実績未、実績送信済⇒実績未
            Case LMH010DAC.EventShubetsu.JIKKOU_SAKUSEI_JISSEKIMI _
                , LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_JISSEKIMI

                setSql = LMH010DAC.SQL_UPD_JISSEKIMODOSI_EDI_M

                '入荷取消⇒未登録
            Case LMH010DAC.EventShubetsu.JIKKOU_TOUROKU_MITOUROKU

                '2012.02.25 大阪対応 START
                If autoMatomeF.Equals("9") = True Then
                    'まとめデータでない場合
                    setSql = LMH010DAC.SQL_UPD_TOUROKUMI_EDI_M

                Else
                    'まとめデータの場合
                    setSql = LMH010DAC.SQL_UPD_TOUROKUMI_EDI_M_MATOME
                End If
                'setSql = LMH010DAC.SQL_UPD_TOUROKUMI_EDI_M
                '2012.02.25 大阪対応 END

                '送信済み⇒送信待ち
            Case LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_SOUSINMI

                setSql = LMH010DAC.SQL_JISSEKIZUMI_SOUSINMACHI_EDI_M

                '2015.09.07 tsunehira add
                '商品key(NRS_GOODS_CD)の変更
            Case LMH010DAC.EventShubetsu.BULK_CUST_CHANGE

                setSql = LMH010DAC.SQL_BULK_CHG_EDI_M
                
        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, nrsBrCd))
        Dim dtRow As DataRow
        Dim max As Integer = dt.Rows.Count() - 1

        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            dtRow = dt.Rows(i)

            'パラメータ設定
            Call Me.SetSysdataParameter()
            Call Me.SetUpdPrmEdiM(dtRow)
            Call Me.SetJissekiParameterEdiLEdiM(dtRow, eventShubetsu)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH010DAC", "UpdateEdiM", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "RCV_HED"

    ''' <summary>
    ''' 受信ヘッダ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateRcvHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMH010_RCV_HED")
        Dim dtEdiL As DataTable = ds.Tables("LMH010_INKAEDI_L")
        Me._Row = ds.Tables("LMH010INOUT").Rows(0)

        '2012.02.25 大阪対応 START
        Dim autoMatomeF As String = Me._Row("AUTO_MATOME_FLG").ToString()
        Dim inOutFlg As String = Me._Row.Item("EDI_CUST_INOUTFLG").ToString()
        '2012.02.25 大阪対応 END

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim eventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        Select Case DirectCast(eventShubetsu, LMH010DAC.EventShubetsu)

            Case LMH010DAC.EventShubetsu.JISSEKI_TORIKESI

                setSql = LMH010DAC.SQL_UPD_JISSEKITORIKESI_RCV_HED
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH010DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                'EDI取消、EDI取消⇒未登録、報告用EDI取消
            Case LMH010DAC.EventShubetsu.EDI_TORIKESI _
                , LMH010DAC.EventShubetsu.JIKKOU_TORIKESI_MITOUROKU _
                , LMH010DAC.EventShubetsu.JIKKOU_HOUKOKU_EDI_TORIKESI

                setSql = LMH010DAC.SQL_UPD_EDITORIKESI_RCV_HED
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH010DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                '実績作成済⇒実績未、実績送信済⇒実績未
            Case LMH010DAC.EventShubetsu.JIKKOU_SAKUSEI_JISSEKIMI _
                , LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_JISSEKIMI

                setSql = LMH010DAC.SQL_UPD_JISSEKIMODOSI_RCV_HED
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH010DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                '実績送信済⇒送信未
            Case LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_SOUSINMI

                setSql = LMH010DAC.SQL_UPD_SOUSINZUMI_SOUSINMI_RCV_HED
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH010DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                '入荷取消⇒未登録
            Case LMH010DAC.EventShubetsu.JIKKOU_TOUROKU_MITOUROKU

                setSql = LMH010DAC.SQL_UPD_TOUROKUMI_RCV_HED
                '2012.02.25 大阪対応 START
                If autoMatomeF.Equals("9") = True Then
                    'まとめデータでない場合
                    setSql = String.Concat(setSql, LMH010DAC.SQL_WHERE_TOUROKUMI_NORMAL)

                Else
                    'まとめデータの場合
                    setSql = String.Concat(setSql, LMH010DAC.SQL_WHERE_TOUROKUMI_MATOME)
                End If
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH010DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If
                '2012.02.25 大阪対応 END

        End Select

        '受信HEDテーブル名設定
        setSql = Me.SetRcvHedTableNm(setSql)

        '入荷管理番号に営業所イニシャル設定
        setSql = Me.SetBrInitial(setSql, dtEdiL.Rows(0))

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(setSql, dt.Rows(0).Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetSysdataParameter()
        Call Me.SetUpdPrmRcvHed(dt)
        Call Me.SetHaitaDateTime(dt.Rows(0))
        Call Me.SetUpdPrmDelDateRcv(eventShubetsu)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "UpdateRcvHed", cmd)

        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region

#Region "RCV_DTL"

    ''' <summary>
    ''' 受信明細更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateRcvDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMH010_RCV_DTL")

        Dim dtEdiL As DataTable = ds.Tables("LMH010_INKAEDI_L")
        Me._Row = ds.Tables("LMH010INOUT").Rows(0)

        Dim eventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        '2012.02.25 大阪対応 START
        Dim inOutFlg As String = Me._Row.Item("EDI_CUST_INOUTFLG").ToString()
        Dim autoMatomeF As String = Me._Row.Item("AUTO_MATOME_FLG").ToString()
        Dim rcvHedNm As String = Me._Row.Item("RCV_NM_HED").ToString()
        '2012.02.25 大阪対応 END

        '住化カラー追加箇所 terakawa 20120605 Start
        Dim custIndex As Integer = Convert.ToInt32(Me._Row.Item("EDI_CUST_INDEX"))
        '住化カラー追加箇所 terakawa 20120605 End

        Select Case DirectCast(eventShubetsu, LMH010DAC.EventShubetsu)

            '実績取消
            Case LMH010DAC.EventShubetsu.JISSEKI_TORIKESI
                setSql = LMH010DAC.SQL_UPD_JISSEKITORIKESI_RCV_DTL
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH010DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                'EDI取消、EDI取消⇒未登録、報告用EDI取消
            Case LMH010DAC.EventShubetsu.EDI_TORIKESI _
                , LMH010DAC.EventShubetsu.JIKKOU_TORIKESI_MITOUROKU _
                , LMH010DAC.EventShubetsu.JIKKOU_HOUKOKU_EDI_TORIKESI

                setSql = LMH010DAC.SQL_UPD_EDITORIKESI_RCV_DTL
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH010DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                '実績作成済⇒実績未
            Case LMH010DAC.EventShubetsu.JIKKOU_SAKUSEI_JISSEKIMI
                '住化カラーの場合は、住化カラー専用SQLを使用
                If custIndex.Equals(LMH010DAC.EdiCustIndex.Sumika00952_00) Then
                    setSql = LMH010DAC.SQL_UPD_JISSEKIZUMI_JISSEKIMI_RCV_DTL_SMK
                Else
                    setSql = LMH010DAC.SQL_UPD_JISSEKIZUMI_JISSEKIMI_RCV_DTL
                End If
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH010DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                '実績送信済⇒実績未、
            Case LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_JISSEKIMI
                '住化カラーの場合は、住化カラー専用SQLを使用
                If custIndex.Equals(LMH010DAC.EdiCustIndex.Sumika00952_00) Then
                    setSql = LMH010DAC.SQL_UPD_SOUSINZUMI_JISSEKIMI_RCV_DTL_SMK
                Else
                    setSql = LMH010DAC.SQL_UPD_SOUSINZUMI_JISSEKIMI_RCV_DTL
                End If
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH010DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                '実績送信済⇒送信未
            Case LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_SOUSINMI

                setSql = LMH010DAC.SQL_UPD_SOUSINZUMI_SOUSINMI_RCV_DTL
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH010DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If

                '入荷取消⇒未登録
            Case LMH010DAC.EventShubetsu.JIKKOU_TOUROKU_MITOUROKU

                '2012.02.25 大阪対応 START
                'setSql = LMH010DAC.SQL_UPD_TOUROKUMI_RCV_DTL
                If String.IsNullOrEmpty(rcvHedNm) = True Then
                    If custIndex.Equals(LMH010DAC.EdiCustIndex.TSMC75) Then
                        setSql = LMH010DAC.SQL_UPD_TOUROKUMI_RCV_DTL1.Replace("INKA_CTL_NO_M", "INLA_CTL_NO_M")
                        setSql = String.Concat(setSql, LMH010DAC.SQL_UPD_TOUROKUMI_RCV_DTL3)
                    Else
                        setSql = LMH010DAC.SQL_UPD_TOUROKUMI_RCV_DTL1
                        setSql = String.Concat(setSql, LMH010DAC.SQL_UPD_TOUROKUMI_RCV_DTL2)
                        setSql = String.Concat(setSql, LMH010DAC.SQL_UPD_TOUROKUMI_RCV_DTL3)
                    End If
                Else
                    setSql = LMH010DAC.SQL_UPD_TOUROKUMI_RCV_DTL1
                    setSql = String.Concat(setSql, LMH010DAC.SQL_UPD_TOUROKUMI_RCV_DTL3)
                End If
                '2012.02.25 大阪対応 END

                '2012.02.25 大阪対応 START
                If autoMatomeF.Equals("9") = True Then
                    'まとめデータでない場合
                    setSql = String.Concat(setSql, LMH010DAC.SQL_WHERE_TOUROKUMI_NORMAL)

                Else
                    'まとめデータの場合
                    setSql = String.Concat(setSql, LMH010DAC.SQL_WHERE_TOUROKUMI_MATOME)
                End If
                If inOutFlg.Equals("1") = True Then
                    setSql = String.Concat(setSql, LMH010DAC.SQL_UPDATE_WHERE_INOUT_KB)
                End If
                '2012.02.25 大阪対応 END

        End Select

        '受信DTLテーブル名設定
        setSql = Me.SetRcvDtlTableNm(setSql)

        '入荷管理番号に営業所イニシャル設定
        setSql = Me.SetBrInitial(setSql, dtEdiL.Rows(0))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, dt.Rows(0).Item("NRS_BR_CD").ToString()))
        Dim dtRow As DataRow
        Dim max As Integer = dt.Rows.Count() - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            dtRow = dt.Rows(i)

            'パラメータ設定
            Call Me.SetSysdataParameter()
            Call Me.SetUpdPrmRcvDtl(dtRow)
            Call Me.SetUpdPrmDelDateRcv(eventShubetsu)

            Dim methodNm As String = Reflection.MethodBase.GetCurrentMethod.Name


            Call Me.SetJissekiParameterRcv(dtRow, eventShubetsu)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMH010DAC", "UpdateRcvDtl", cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "INKA_L"

    ''' <summary>
    ''' 入荷(大)データ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMH010_B_INKA_L")
        Dim nrsBrCd As String = dt.Rows(0)("NRS_BR_CD").ToString()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim eventShubetsu As Integer = Convert.ToInt32(ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU"))
        Dim setSql As String = String.Empty

        setSql = SQL_UPD_JISSEKIMODOSI_INKA_L

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(setSql, nrsBrCd))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetSysdataParameter()
        Call Me.SetInkaLComParameter(dt.Rows(0))
        Call Me.SetHaitaDateTime(dt.Rows(0))
        Call Me.SetJissekiParameterEdiLEdiM(dt.Rows(0), eventShubetsu)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "UpdateInkaL", cmd)

        'SQLの発行
        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region

#Region "SEND"

    ''' <summary>
    ''' EDI送信テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI送信テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateSend(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMH010_SEND")
        Me._Row = ds.Tables("LMH010INOUT").Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim setSql As String = String.Empty

        setSql = SQL_UPD_JISSEKIMODOSI_SEND

        '送信テーブル名設定
        setSql = Me.SetSendTableNm(setSql)

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(setSql, dt.Rows(0).Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetSysdataParameter()
        Call Me.SetPrmRcvSend(dt)
        Call Me.SetHaitaDateTime(dt.Rows(0))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "UpdateSend", cmd)

        'SQLの発行
        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        If resultCnt = 0 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region

    '印刷フラグ更新対応 20120313 Start
#Region "RCV_HED(DTL) PRINT_FLAG"

    ''' <summary>
    ''' 印刷フラグ更新(受信TBL)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>印刷フラグの更新</remarks>
    Private Function UpdatePrintFlag(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dtIn As DataTable = ds.Tables("LMH010_OUTPUTIN")
        Dim setSql As String = String.Empty
        Dim RcvNm As String = String.Empty

        'INTableの条件rowの格納
        Me._Row = dtIn.Rows(0)

        '2012.03.18 大阪対応START
        '受信テーブル名設定
        If String.IsNullOrEmpty(Me._Row.Item("RCV_NM_HED").ToString()) = False Then
            RcvNm = Me._Row.Item("RCV_NM_HED").ToString()
        End If

        Dim ediCustIdx As String = Me._Row.Item("EDI_CUST_INDEX").ToString() '修正 terakawa 2012.11.27

        '実行(印刷フラグ更新)SQL CONST名
        If String.IsNullOrEmpty(RcvNm) = False Then
            '修正 terakawa 2012.11.27 Start
            ''千葉EDI 2012/11/06 追加　START
            'If String.IsNullOrEmpty(Me._Row.Item("PRTFLG").ToString) = False Then
            '    setSql = LMH010DAC.SQL_UPD_PRTFLG_EDI_HED
            'Else
            '    setSql = LMH010DAC.SQL_UPD_PRTFLG_HED
            'End If
            ''千葉EDI　2012/11/06　END

            '日産物流の場合、受信TBLにCUST_CDを持っていないので条件より外す(DTLのSQLを使用)
            If ediCustIdx.Equals("13") = True Then
                setSql = LMH010DAC.SQL_UPD_PRTFLG_DTL
            Else
                setSql = LMH010DAC.SQL_UPD_PRTFLG_HED
            End If
            '修正 terakawa 2012.11.27 End
        Else
            setSql = LMH010DAC.SQL_UPD_PRTFLG_DTL
        End If
        '2012.03.18 大阪対応END

        'CRT_DATE_FROMが設定されていた場合、条件に追加
        If String.IsNullOrEmpty(Me._Row.Item("CRT_DATE_FROM").ToString()) = False Then
            setSql = String.Concat(setSql, LMH010DAC.SQL_FROM_PRTFLG_CRT_DATE_FROM)
        End If

        'CRT_DATE_TOが設定されていた場合、条件に追加
        If String.IsNullOrEmpty(Me._Row.Item("CRT_DATE_TO").ToString()) = False Then
            setSql = String.Concat(setSql, LMH010DAC.SQL_FROM_PRTFLG_CRT_DATE_TO)
        End If

        '入出荷両用の受信テーブルの場合、INOUT_KBを条件に追加
        If Me._Row.Item("INOUT_UMU_KB").ToString() = "1" Then
            setSql = String.Concat(setSql, LMH010DAC.SQL_FROM_PRTFLG_INOUT)
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
            '要望番号1062 2012.05.15 修正START
            '■■キャッシュ更新までの暫定対応　Start■■
            RcvNm = Me._Row.Item("RCV_NM_DTL").ToString()
            'RcvNm = GetRcvNmDtl(ds)
            ds.Tables("LMH010_OUTPUTIN").Rows(0).Item("RCV_NM_DTL") = RcvNm
            '■■キャッシュ更新までの暫定対応　End■■
            '要望番号1062 2012.05.15 修正END
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
        Call Me.SetSysdataParameter()
        'Call Me.SetSysDateTime(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "LMH010_OUTPUTIN", cmd)

        Dim resultCnt As Integer = MyBase.GetUpdateResult(cmd)

        MyBase.SetResultCount(resultCnt)

        'If resultCnt = 0 Then
        '    MyBase.SetMessage("E011")
        'End If

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
        setSql = LMH010DAC.SQL_DEL_EDI_PRINT
        '2012.05.29 要望番号1077 追加START
        If String.IsNullOrEmpty(Me._Row.Item("DENPYO_NO").ToString()) = True Then
            Me._Row.Item("DENPYO_NO") = "999999999999999999999999999999"
        Else
            setSql = String.Concat(setSql, LMH010DAC.SQL_WHERE_DENPYO_NO)
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

        MyBase.Logger.WriteSQLLog("LMH010DAC", "DeleteHEdiPrint", cmd)

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

        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = dtIn.Rows(i)

            'SQL CONST名
            setSql = LMH010DAC.SQL_INS_EDI_PRINT

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

            MyBase.Logger.WriteSQLLog("LMH010DAC", "InsertHEdiPrint", cmd)

            'SQLの発行
            Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

#End Region
    '要望番号1007 2012.05.08 修正END

#End Region

#Region "DELETE処理"

#Region "SEND"

    ''' <summary>
    ''' EDI送信テーブル削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI送信テーブル削除SQLの構築・発行</remarks>
    Private Function DeleteSend(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim dt As DataTable = ds.Tables("LMH010_SEND")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim setSql As String = String.Empty

        setSql = LMH010DAC.SQL_DEL_JISSEKIMODOSI_SEND

        '送信テーブル名設定
        setSql = Me.SetSendTableNm(setSql)

        'SQL作成
        Me._StrSql.Append(Me.SetSchemaNm(setSql, dt.Rows(0).Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetPrmRcvSend(dt)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "DeleteSend", cmd)

        'SQLの発行
        If MyBase.GetDeleteResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
            Return ds
        End If

        Return ds

    End Function

#End Region

#End Region

#Region "パラメータ設定"

#Region "更新パラメータ設定(共通)"
    '要望番号1007 2012.05.08 修正START
    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal prmList As ArrayList)

        'システム項目
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetSysdataParameter()

    End Sub
    '要望番号1007 2012.05.08 修正End


    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter()

        'システム項目
        Call Me.SetSysdataTimeParameter()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter()

        '更新日時
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 更新時のパラメータ抽出条件(日時)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetHaitaDateTime(ByVal dr As DataRow)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_SYS_UPD_DATE", dr.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_SYS_UPD_TIME", dr.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 更新時のパラメータ実績日時(EDI_L,EDI_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetJissekiParameterEdiLEdiM(ByVal row As DataRow, ByVal eventShubetsu As Integer)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        Select Case DirectCast(eventShubetsu, LMH010DAC.EventShubetsu)

            Case LMH010DAC.EventShubetsu.JIKKOU_SAKUSEI_JISSEKIMI _
                 , LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_JISSEKIMI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", String.Empty, DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", String.Empty, DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", String.Empty, DBDataType.CHAR))

            Case Else

        End Select

    End Sub

    ''' <summary>
    ''' 更新時のパラメータ実績日時(Rcv)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetJissekiParameterRcv(ByVal row As DataRow, ByVal eventShubetsu As Integer)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        Select Case DirectCast(eventShubetsu, LMH010DAC.EventShubetsu)

            Case LMH010DAC.EventShubetsu.JIKKOU_SAKUSEI_JISSEKIMI _
                 , LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_JISSEKIMI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", String.Empty, DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", String.Empty, DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", String.Empty, DBDataType.CHAR))

            Case Else

        End Select

    End Sub

    ''' <summary>
    ''' 更新パラメータ削除日時設定(RCV)
    ''' </summary>
    ''' <param name="eventShubetsu"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdPrmDelDateRcv(ByVal eventShubetsu As Integer)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        'SQLパラメータ設定

        Select Case DirectCast(eventShubetsu, LMH010DAC.EventShubetsu)
            'EDI取消、報告用EDI取消
            Case LMH010DAC.EventShubetsu.EDI_TORIKESI _
                , LMH010DAC.EventShubetsu.JIKKOU_HOUKOKU_EDI_TORIKESI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", updTime, DBDataType.CHAR))

                'EDI取消⇒未登録、実績作成済⇒実績未、実績送信済⇒実績未
            Case LMH010DAC.EventShubetsu.JIKKOU_TORIKESI_MITOUROKU _
                , LMH010DAC.EventShubetsu.JIKKOU_SAKUSEI_JISSEKIMI _
                , LMH010DAC.EventShubetsu.JIKKOU_SOUSIN_JISSEKIMI

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_USER", String.Empty, DBDataType.NVARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", String.Empty, DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", String.Empty, DBDataType.CHAR))

            Case Else

        End Select
    End Sub

#End Region

#Region "更新パラメータ設定(EDI_L)"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdPrmEdiL(ByVal dt As DataTable)

        'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        'SQLパラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(dt.Rows(0).Item("DEL_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(dt.Rows(0).Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(dt.Rows(0).Item("EDI_CTL_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", Me.NullConvertString(dt.Rows(0).Item("INKA_CTL_NO_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TP", Me.NullConvertString(dt.Rows(0).Item("INKA_TP")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KB", Me.NullConvertString(dt.Rows(0).Item("INKA_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_STATE_KB", Me.NullConvertString(dt.Rows(0).Item("INKA_STATE_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", Me.NullConvertString(dt.Rows(0).Item("INKA_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TIME", Me.NullConvertString(dt.Rows(0).Item("INKA_TIME")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_WH_CD", Me.NullConvertString(dt.Rows(0).Item("NRS_WH_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(dt.Rows(0).Item("CUST_CD_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(dt.Rows(0).Item("CUST_CD_M")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", Me.NullConvertString(dt.Rows(0).Item("CUST_NM_L")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_M", Me.NullConvertString(dt.Rows(0).Item("CUST_NM_M")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT", Me.NullConvertZero(dt.Rows(0).Item("INKA_PLAN_QT")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT_UT", Me.NullConvertString(dt.Rows(0).Item("INKA_PLAN_QT_UT")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TTL_NB", Me.NullConvertZero(dt.Rows(0).Item("INKA_TTL_NB")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NAIGAI_KB", Me.NullConvertString(dt.Rows(0).Item("NAIGAI_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", Me.NullConvertString(dt.Rows(0).Item("BUYER_ORD_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO", Me.NullConvertString(dt.Rows(0).Item("OUTKA_FROM_ORD_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me.NullConvertString(dt.Rows(0).Item("SEIQTO_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", Me.NullConvertString(dt.Rows(0).Item("TOUKI_HOKAN_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_YN", Me.NullConvertString(dt.Rows(0).Item("HOKAN_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_FREE_KIKAN", Me.NullConvertZero(dt.Rows(0).Item("HOKAN_FREE_KIKAN")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_STR_DATE", Me.NullConvertString(dt.Rows(0).Item("HOKAN_STR_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", Me.NullConvertString(dt.Rows(0).Item("NIYAKU_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", Me.NullConvertString(dt.Rows(0).Item("TAX_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(dt.Rows(0).Item("REMARK")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NYUBAN_L", Me.NullConvertString(dt.Rows(0).Item("NYUBAN_L")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TP", Me.NullConvertString(dt.Rows(0).Item("UNCHIN_TP")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_KB", Me.NullConvertString(dt.Rows(0).Item("UNCHIN_KB")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_MOTO", Me.NullConvertString(dt.Rows(0).Item("OUTKA_MOTO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYARYO_KB", Me.NullConvertString(dt.Rows(0).Item("SYARYO_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", Me.NullConvertString(dt.Rows(0).Item("UNSO_ONDO_KB")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", Me.NullConvertString(dt.Rows(0).Item("UNSO_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", Me.NullConvertString(dt.Rows(0).Item("UNSO_BR_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN", Me.NullConvertString(dt.Rows(0).Item("UNCHIN")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKO_TARIFF_CD", Me.NullConvertString(dt.Rows(0).Item("YOKO_TARIFF_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_FLAG", Me.NullConvertString(dt.Rows(0).Item("OUT_FLAG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", Me.NullConvertString(dt.Rows(0).Item("JISSEKI_FLAG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", Me.NullConvertString(dt.Rows(0).Item("AKAKURO_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N01", Me.NullConvertZero(dt.Rows(0).Item("FREE_N01")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N02", Me.NullConvertZero(dt.Rows(0).Item("FREE_N02")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N03", Me.NullConvertZero(dt.Rows(0).Item("FREE_N03")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N04", Me.NullConvertZero(dt.Rows(0).Item("FREE_N04")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N05", Me.NullConvertZero(dt.Rows(0).Item("FREE_N05")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N06", Me.NullConvertZero(dt.Rows(0).Item("FREE_N06")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N07", Me.NullConvertZero(dt.Rows(0).Item("FREE_N07")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N08", Me.NullConvertZero(dt.Rows(0).Item("FREE_N08")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N09", Me.NullConvertZero(dt.Rows(0).Item("FREE_N09")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N10", Me.NullConvertZero(dt.Rows(0).Item("FREE_N10")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C01", Me.NullConvertString(dt.Rows(0).Item("FREE_C01")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C02", Me.NullConvertString(dt.Rows(0).Item("FREE_C02")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C03", Me.NullConvertString(dt.Rows(0).Item("FREE_C03")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C04", Me.NullConvertString(dt.Rows(0).Item("FREE_C04")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C05", Me.NullConvertString(dt.Rows(0).Item("FREE_C05")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C06", Me.NullConvertString(dt.Rows(0).Item("FREE_C06")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C07", Me.NullConvertString(dt.Rows(0).Item("FREE_C07")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C08", Me.NullConvertString(dt.Rows(0).Item("FREE_C08")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C09", Me.NullConvertString(dt.Rows(0).Item("FREE_C09")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C10", Me.NullConvertString(dt.Rows(0).Item("FREE_C10")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C11", Me.NullConvertString(dt.Rows(0).Item("FREE_C11")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C12", Me.NullConvertString(dt.Rows(0).Item("FREE_C12")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C13", Me.NullConvertString(dt.Rows(0).Item("FREE_C13")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C14", Me.NullConvertString(dt.Rows(0).Item("FREE_C14")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C15", Me.NullConvertString(dt.Rows(0).Item("FREE_C15")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C16", Me.NullConvertString(dt.Rows(0).Item("FREE_C16")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C17", Me.NullConvertString(dt.Rows(0).Item("FREE_C17")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C18", Me.NullConvertString(dt.Rows(0).Item("FREE_C18")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C19", Me.NullConvertString(dt.Rows(0).Item("FREE_C19")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C20", Me.NullConvertString(dt.Rows(0).Item("FREE_C20")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C21", Me.NullConvertString(dt.Rows(0).Item("FREE_C21")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C22", Me.NullConvertString(dt.Rows(0).Item("FREE_C22")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C23", Me.NullConvertString(dt.Rows(0).Item("FREE_C23")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C24", Me.NullConvertString(dt.Rows(0).Item("FREE_C24")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C25", Me.NullConvertString(dt.Rows(0).Item("FREE_C25")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C26", Me.NullConvertString(dt.Rows(0).Item("FREE_C26")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C27", Me.NullConvertString(dt.Rows(0).Item("FREE_C27")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C28", Me.NullConvertString(dt.Rows(0).Item("FREE_C28")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C29", Me.NullConvertString(dt.Rows(0).Item("FREE_C29")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C30", Me.NullConvertString(dt.Rows(0).Item("FREE_C30")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_FLAG", Me.NullConvertString(dt.Rows(0).Item("EDIT_FLAG")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATCHING_FLAG", Me.NullConvertString(dt.Rows(0).Item("MATCHING_FLAG")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(dt.Rows(0).Item("SYS_DEL_FLG")), DBDataType.CHAR))

    End Sub

#End Region

#Region "更新パラメータ設定(EDI_M)"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdPrmEdiM(ByVal row As DataRow)

        'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        'SQLパラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(row.Item("DEL_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(row.Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(row.Item("EDI_CTL_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", Me.NullConvertString(row.Item("EDI_CTL_NO_CHU")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", Me.NullConvertString(row.Item("INKA_CTL_NO_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_M", Me.NullConvertString(row.Item("INKA_CTL_NO_M")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", Me.NullConvertString(row.Item("NRS_GOODS_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", Me.NullConvertString(row.Item("CUST_GOODS_CD")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", Me.NullConvertString(row.Item("GOODS_NM")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB", Me.NullConvertZero(row.Item("NB")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NB_UT", Me.NullConvertString(row.Item("NB_UT")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_NB", Me.NullConvertZero(row.Item("PKG_NB")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_UT", Me.NullConvertString(row.Item("PKG_UT")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_PKG_NB", Me.NullConvertZero(row.Item("INKA_PKG_NB")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HASU", Me.NullConvertZero(row.Item("HASU")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_IRIME", Me.NullConvertZero(row.Item("STD_IRIME")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STD_IRIME_UT", Me.NullConvertString(row.Item("STD_IRIME_UT")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.NullConvertZero(row.Item("BETU_WT")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CBM", Me.NullConvertZero(row.Item("CBM")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_KB", Me.NullConvertString(row.Item("ONDO_KB")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO", Me.NullConvertString(row.Item("OUTKA_FROM_ORD_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", Me.NullConvertString(row.Item("BUYER_ORD_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(row.Item("REMARK")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me.NullConvertString(row.Item("LOT_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", Me.NullConvertString(row.Item("SERIAL_NO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me.NullConvertZero(row.Item("IRIME")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", Me.NullConvertString(row.Item("IRIME_UT")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_KB", Me.NullConvertString(row.Item("OUT_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", Me.NullConvertString(row.Item("AKAKURO_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", Me.NullConvertString(row.Item("JISSEKI_FLAG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N01", Me.NullConvertZero(row.Item("FREE_N01")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N02", Me.NullConvertZero(row.Item("FREE_N02")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N03", Me.NullConvertZero(row.Item("FREE_N03")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N04", Me.NullConvertZero(row.Item("FREE_N04")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N05", Me.NullConvertZero(row.Item("FREE_N05")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N06", Me.NullConvertZero(row.Item("FREE_N06")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N07", Me.NullConvertZero(row.Item("FREE_N07")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N08", Me.NullConvertZero(row.Item("FREE_N08")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N09", Me.NullConvertZero(row.Item("FREE_N09")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N10", Me.NullConvertZero(row.Item("FREE_N10")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C01", Me.NullConvertString(row.Item("FREE_C01")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C02", Me.NullConvertString(row.Item("FREE_C02")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C03", Me.NullConvertString(row.Item("FREE_C03")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C04", Me.NullConvertString(row.Item("FREE_C04")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C05", Me.NullConvertString(row.Item("FREE_C05")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C06", Me.NullConvertString(row.Item("FREE_C06")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C07", Me.NullConvertString(row.Item("FREE_C07")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C08", Me.NullConvertString(row.Item("FREE_C08")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C09", Me.NullConvertString(row.Item("FREE_C09")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C10", Me.NullConvertString(row.Item("FREE_C10")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C11", Me.NullConvertString(row.Item("FREE_C11")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C12", Me.NullConvertString(row.Item("FREE_C12")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C13", Me.NullConvertString(row.Item("FREE_C13")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C14", Me.NullConvertString(row.Item("FREE_C14")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C15", Me.NullConvertString(row.Item("FREE_C15")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C16", Me.NullConvertString(row.Item("FREE_C16")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C17", Me.NullConvertString(row.Item("FREE_C17")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C18", Me.NullConvertString(row.Item("FREE_C18")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C19", Me.NullConvertString(row.Item("FREE_C19")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C20", Me.NullConvertString(row.Item("FREE_C20")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C21", Me.NullConvertString(row.Item("FREE_C21")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C22", Me.NullConvertString(row.Item("FREE_C22")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C23", Me.NullConvertString(row.Item("FREE_C23")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C24", Me.NullConvertString(row.Item("FREE_C24")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C25", Me.NullConvertString(row.Item("FREE_C25")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C26", Me.NullConvertString(row.Item("FREE_C26")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C27", Me.NullConvertString(row.Item("FREE_C27")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C28", Me.NullConvertString(row.Item("FREE_C28")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C29", Me.NullConvertString(row.Item("FREE_C29")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C30", Me.NullConvertString(row.Item("FREE_C30")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(row.Item("SYS_DEL_FLG")), DBDataType.CHAR))


    End Sub

#End Region

#Region "更新パラメータ設定(RCV_HED)"
    ''' <summary>
    ''' 更新パラメータ設定(RCV_HED)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdPrmRcvHed(ByVal dt As DataTable)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        'SQLパラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(dt.Rows(0).Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(dt.Rows(0).Item("EDI_CTL_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", Me.NullConvertString(dt.Rows(0).Item("INKA_CTL_NO_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(dt.Rows(0).Item("DEL_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(dt.Rows(0).Item("SYS_DEL_FLG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_USER", Me.NullConvertString(dt.Rows(0).Item("INKA_USER")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", Me.NullConvertString(dt.Rows(0).Item("INKA_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TIME", Me.NullConvertString(dt.Rows(0).Item("INKA_TIME")), DBDataType.CHAR))


    End Sub

#End Region

#Region "更新パラメータ設定(RCV_DTL)"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdPrmRcvDtl(ByVal row As DataRow)

        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        'SQLパラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(row.Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(row.Item("EDI_CTL_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", Me.NullConvertString(row.Item("EDI_CTL_NO_CHU")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", Me.NullConvertString(row.Item("INKA_CTL_NO_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_M", Me.NullConvertString(row.Item("INKA_CTL_NO_M")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", Me.NullConvertString(row.Item("DEL_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(row.Item("SYS_DEL_FLG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", Me.NullConvertString(row.Item("JISSEKI_SHORI_FLG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_USER", String.Empty, DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DATE", String.Empty, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_TIME", String.Empty, DBDataType.CHAR))
        '要望番号:1088対応 terakawa 2012.06.19 Start
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_USER", String.Empty, DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", String.Empty, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TIME", String.Empty, DBDataType.CHAR))
        '要望番号:1088対応 terakawa 2012.06.19 End

    End Sub

#End Region

#Region "更新パラメータ設定(INKA_L)"
    ''' <summary>
    ''' INKA_Lの更新パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInkaLComParameter(ByVal row As DataRow)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(row.Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", Me.NullConvertString(row.Item("INKA_NO_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FURI_NO", Me.NullConvertString(row.Item("FURI_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TP", Me.NullConvertString(row.Item("INKA_TP")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_KB", Me.NullConvertString(row.Item("INKA_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_STATE_KB", Me.NullConvertString(row.Item("INKA_STATE_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", Me.NullConvertString(row.Item("INKA_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", Me.NullConvertString(row.Item("NRS_WH_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(row.Item("CUST_CD_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(row.Item("CUST_CD_M")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT", Me.NullConvertZero(row.Item("INKA_PLAN_QT")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT_UT", Me.NullConvertString(row.Item("INKA_PLAN_QT_UT")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TTL_NB", Me.NullConvertZero(row.Item("INKA_TTL_NB")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_L", Me.NullConvertString(row.Item("BUYER_ORD_NO_L")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_L", Me.NullConvertString(row.Item("OUTKA_FROM_ORD_NO_L")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me.NullConvertString(row.Item("SEIQTO_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", Me.NullConvertString(row.Item("TOUKI_HOKAN_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_YN", Me.NullConvertString(row.Item("HOKAN_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_FREE_KIKAN", Me.NullConvertZero(row.Item("HOKAN_FREE_KIKAN")), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_STR_DATE", Me.NullConvertString(row.Item("HOKAN_STR_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", Me.NullConvertString(row.Item("NIYAKU_YN")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TAX_KB", Me.NullConvertString(row.Item("TAX_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(row.Item("REMARK")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", Me.NullConvertString(row.Item("REMARK_OUT")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TP", Me.NullConvertString(row.Item("UNCHIN_TP")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_KB", Me.NullConvertString(row.Item("UNCHIN_KB")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me.NullConvertString(row.Item("SYS_DEL_FLG")), DBDataType.CHAR))

    End Sub
#End Region

#Region "更新パラメータ設定(INKA_M)"
    ''' <summary>
    ''' 更新パラメータ設定(INKA_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInkaMComParameter(ByVal row As DataRow)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", row.Item("INKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", row.Item("INKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", row.Item("GOODS_CD_NRS").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_M", row.Item("OUTKA_FROM_ORD_NO_M").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_M", row.Item("BUYER_ORD_NO_M").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", row.Item("REMARK").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#Region "更新パラメータ設定(INKA_S)"
    ''' <summary>
    ''' INKA_Sの更新パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInkaSComParameter(ByVal row As DataRow)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", row.Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", row.Item("INKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_M", row.Item("INKA_NO_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_S", row.Item("INKA_NO_S").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", row.Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", row.Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KONSU", Me.FormatNumValue(row.Item("KONSU").ToString()), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HASU", Me.FormatNumValue(row.Item("HASU").ToString()), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", Me.FormatNumValue(row.Item("IRIME").ToString()), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BETU_WT", Me.FormatNumValue(row.Item("BETU_WT").ToString()), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", row.Item("SERIAL_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SPD_KB", row.Item("SPD_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OFB_KB", row.Item("OFB_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALLOC_PRIORITY", row.Item("ALLOC_PRIORITY").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", row.Item("REMARK_OUT").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#Region "更新パラメータ設定(SEND)"
    ''' <summary>
    ''' 更新,検索パラメータ設定(SEND)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub SetPrmRcvSend(ByVal dt As DataTable)

        'Dim updTime As String = DateTime.Now.ToString("HH:mm:ss")
        Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

        'SQLパラメータ設定

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(dt.Rows(0).Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me.NullConvertString(dt.Rows(0).Item("EDI_CTL_NO")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", Me.NullConvertString(dt.Rows(0).Item("JISSEKI_SHORI_FLG")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_USER", String.Empty, DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DATE", String.Empty, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_TIME", String.Empty, DBDataType.CHAR))

    End Sub

#End Region

    '共通化対応 20120223 start
#Region "まとめ先データ取得パラメータ設定"
    Private Sub SetMatomePrm(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(dt.Rows(0).Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.NullConvertString(dt.Rows(0).Item("CUST_CD_L")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.NullConvertString(dt.Rows(0).Item("CUST_CD_M")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", Me.NullConvertString(dt.Rows(0).Item("INKA_DATE")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_MOTO", Me.NullConvertString(dt.Rows(0).Item("OUTKA_MOTO")), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO", Me.NullConvertString(dt.Rows(0).Item("OUTKA_FROM_ORD_NO")), DBDataType.NVARCHAR))

    End Sub

    Private Sub SetMatomeUnsoPrm(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(dt.Rows(0).Item("NRS_BR_CD")), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me.NullConvertString(dt.Rows(0).Item("UNSO_NO_L")), DBDataType.CHAR))

    End Sub

#End Region

#Region "まとめフラグ取得パラメータ設定"
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
#End Region
    '共通化対応 20120223 end

    '2012.02.25 大阪対応 START
#Region "同一まとめデータ取得パラメータ設定(出荷取消⇒未登録)"
    ''' <summary>
    ''' 同一まとめデータ取得パラメータ設定(出荷取消⇒未登録)
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetMatomeTorikesiSelectParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATOME_NO", conditionRow.Item("MATOME_NO"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", conditionRow.Item("INKA_CTL_NO_L"), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", conditionRow.Item("SYS_UPD_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", conditionRow.Item("SYS_UPD_TIME"), DBDataType.CHAR))

    End Sub

#End Region
    '2012.02.25 大阪対応 END

    '2015.09.07 tsunehira add
#Region "荷主一括変更"
    ''' <summary>
    ''' 荷主一括変更
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetEDI_LSelectParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", conditionRow.Item("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", conditionRow.Item("EDI_CTL_NO"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", conditionRow.Item("CUST_CD_L"), DBDataType.CHAR))
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
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", dr.Item("INOUT_KB"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", dr.Item("CUST_CD_L"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", dr.Item("CUST_CD_M"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_TP", dr.Item("PRINT_TP"), DBDataType.CHAR))
        '2012.05.29 要望番号1077 追加START
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENPYO_NO", dr.Item("DENPYO_NO"), DBDataType.NVARCHAR))
        '2012.05.29 要望番号1077 追加END

    End Sub
#End Region
    '要望番号1007 2012.05.08 修正END

    'START UMANO 要望番号1302 支払運賃に伴う修正
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
            'prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_BR_CD", .Item("SHIHARAI_BR_CD").ToString(), DBDataType.CHAR))
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
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_KANRI", .Item("SAGYO_KANRI").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region
    'END   UMANO 要望番号1302 支払運賃に伴う修正


#End Region 'パラメータ設定

#Region "変換"
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

        If String.IsNullOrEmpty(value.ToString()) = True Then
            value = 0
        End If

        Return value

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
    ''' 時間コロン編集
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

    End Function

#End Region
    '▲▲▲二次（共通化）
    '共通対応 20120223 start
#Region "まとめ先取得"
    ''' <summary>
    ''' まとめ先データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectMatomeTarget(ByVal ds As DataSet) As DataSet

        '荷主マスタよりまとめフラグを取得
        Dim matomeFlg As String = Me.GetMatomeFlg(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH010_INKAEDI_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        'SQL作成
        Select Case matomeFlg
            Case "1"
                'TODO
            Case "2"
                Me._StrSql.Append(LMH010DAC.SQL_SELECT_MATOME_TARGET)
            Case "3"
                '各荷主専用DACで処理
                'ここは通ってはいけない
            Case "4"
                'TODO

            Case "5"

                '一括まとめ対象入荷管理番号
                Dim chkTbl As DataTable = ds.Tables("LMH010_IKKATUMATOME_CHK")

                '一括で処理した入荷データが無ければまとめ先はなし
                If chkTbl.Rows.Count = 0 Then
                    MyBase.SetResultCount(0)
                    Return ds
                End If

                '2015.05.12 ローム　入荷登録一括対応 修正START
                Me._StrSql.Append(LMH010DAC.SQL_SELECT_MATOME_TARGET_IKKATU)
                '2015.05.12 ローム　入荷登録一括対応 修正END

                '2015.05.12 ローム　入荷登録一括対応 追加START
                Call Me.SetCondition(ds)                             '条件設定
                Me._StrSql.Append(LMH010DAC.SQL_ORDER_BY_MATOME_TARGET_IKKATU)
                '2015.05.12 ローム　入荷登録一括対応 追加END

            Case Else
                'TODO
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

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectMatomeTarget", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("UNSO_SYS_UPD_DATE", "UNSO_SYS_UPD_DATE")
        map.Add("UNSO_SYS_UPD_TIME", "UNSO_SYS_UPD_TIME")
        map.Add("INKA_TTL_NB", "INKA_TTL_NB")
        map.Add("UNSO_PKG_NB", "UNSO_PKG_NB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        '東邦化学追加箇所 20120216 start
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("INKA_STATE_KB", "INKA_STATE_KB")
        '東邦化学追加箇所 20120216 end

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH010_MATOMESAKI")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMH010_MATOMESAKI").Rows.Count())
        reader.Close()
        Return ds

    End Function

    '2015.05.12 ローム　入荷登録一括対応 追加START
    ''' <summary>
    ''' まとめ先取得SQLのWHERE句設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetCondition(ByVal ds As DataSet)
        '検索条件設定

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        Me._StrSql.Append(" QUERY.INKA_STATE_KB_1 < '50' ")
        Me._StrSql.Append(vbNewLine)

        Dim matomeChkDt As DataTable = ds.Tables("LMH010_IKKATUMATOME_CHK")
        Dim max As Integer = matomeChkDt.Rows.Count - 1
        Dim inkaNoL As String = String.Empty

        If max > -1 Then
            Me._StrSql.Append(" AND ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" QUERY.INKA_NO_L IN ( ")
            Me._StrSql.Append(vbNewLine)

            For i As Integer = 0 To max
                inkaNoL = matomeChkDt.Rows(i)("INKA_NO_L").ToString()
                If i <> max Then
                    Me._StrSql.Append("'" & inkaNoL & "',")
                Else
                    Me._StrSql.Append("'" & inkaNoL & "')")
                End If
            Next

        End If

    End Sub
    '2015.05.12 ローム　入荷登録一括対応 追加END

    ''' <summary>
    ''' まとめ先運送(中)データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(中)のまとめデータ(個別重量)の取得SQLの構築・発行</remarks>
    Private Function SelectUnsoMatomeTarget(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH010_MATOMESAKI")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_MATOMESAKI_DATA_UNSO_M)      'SQL構築

        Call Me.SetMatomeUnsoPrm(inTbl)                              '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectUnsoMatomeTarget", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("BETU_WT", "BETU_WT")
        map.Add("UNSO_TTL_NB", "UNSO_TTL_NB")
        map.Add("HASU", "HASU")
        map.Add("PKG_NB", "PKG_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH010_MATOME_UNSO_M")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMH010_MATOME_UNSO_M").Rows.Count())
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' まとめフラグ取得処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>EDI荷主マスタからまとめフラグを取得する</remarks>
    Private Function GetMatomeFlg(ByVal ds As DataSet) As String

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMH010DAC.SQL_SELECT_MATOMEFLG)

        Dim dt As DataTable = ds.Tables("LMH010INOUT")

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

        MyBase.Logger.WriteSQLLog("LMH010DAC", "GetMatomeFlg", cmd)

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
#End Region

    '2012.02.25 大阪対応 START
#Region "同一まとめレコード取得処理(入荷取消⇒未登録)"

    Private Function SelectMatomeTorikesi(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH010INOUT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_DATA_MATOMETORIKESI)      'SQL構築(データ抽出用Select句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        '受信HEDテーブル名設定
        sql = Me.SetRcvHedTableNm(sql)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetMatomeTorikesiSelectParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectMatomeTorikesi", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INKA_CTL_NO_L", "INKA_CTL_NO_L")
        map.Add("RCV_UPD_DATE", "RCV_UPD_DATE")
        map.Add("RCV_UPD_TIME", "RCV_UPD_TIME")
        map.Add("RCV_NM_HED", "RCV_NM_HED")
        map.Add("RCV_NM_DTL", "RCV_NM_DTL")
        map.Add("RCV_NM_EXT", "RCV_NM_EXT")
        map.Add("SND_NM", "SND_NM")
        map.Add("EDI_CUST_INOUTFLG", "EDI_CUST_INOUTFLG")
        map.Add("EDI_CUST_INDEX", "EDI_CUST_INDEX") '追加  2012.11.22

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH010OUT")

        Return ds

    End Function

#End Region

    '2015.09.08 tsunehira add
#Region "INKAEDI_LをHashTableに設定"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="reader"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function SetEDI_L(ByVal reader As SqlDataReader) As Hashtable

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTR_NO", "EDI_CTR_NO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")

        'AreaJisDS = MyBase.setd

        Return map

    End Function

#End Region

    '2015.09.07 tsunehria add
#Region "荷主一括変更"

    Private Function SelectEDI_M(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH010_INKAEDI_L")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH010DAC.SQL_CHG_GOODSKYE)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetEDI_LSelectParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectEDI_M", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("NRS_GOODS_CD", "NRS_GOODS_CD")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("NB", "NB")
        map.Add("INKA_PKG_NB", "INKA_PKG_NB")
        map.Add("HASU", "HASU")
        map.Add("STD_IRIME", "STD_IRIME")
        map.Add("IRIME", "IRIME")
        map.Add("PKG_NB", "PKG_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH010_INKAEDI_M")

        Return ds

    End Function

#End Region

    '2012.02.25 大阪対応 START

    'START UMANO 要望番号1302 支払運賃に伴う修正。支払追加処理
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
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH010DAC.SQL_SHIHARAI_INSERT _
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

            MyBase.Logger.WriteSQLLog("LMH010DAC", "InsertShiharaiUnchinData", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region
    'END   UMANO 要望番号1302 支払運賃に伴う修正。支払追加処理

    '要望番号:944（後からまとめをするとアベンド） 2012/05/25 Honmyo Start
#Region "入荷管理番号（中）MAX取得"

    ''' <summary>
    ''' 入荷管理番号（中）MAX取得
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetMaxINKA_NO_M(ByVal sNRS_BR_CD As String, ByVal sINKA_NO_L As String) As Integer

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Me.CreateSqlCommand(Me.SetSchemaNm(SQL_GetMaxINKA_NO_M, sNRS_BR_CD))

        Dim rtn As Integer = 0

        'SQLパラメータ初期化
        _SqlPrmList = New ArrayList

        'パラメータ設定
        _SqlPrmList.Add(GetSqlParameter("@NRS_BR_CD", sNRS_BR_CD, DBDataType.CHAR))
        _SqlPrmList.Add(GetSqlParameter("@INKA_NO_L", sINKA_NO_L, DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In _SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("GetMaxINKA_NO_M", "SelectData", cmd)

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



    ''' <summary>
    ''' 受信DTL取得処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks>受信DTLを取得する</remarks>
    Private Function GetRcvNmDtl(ByVal ds As DataSet) As String

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMH010DAC.SQL_SELECT_RCV_NM_DTL)

        Dim dt As DataTable = ds.Tables("LMH010_OUTPUTIN")

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

        MyBase.Logger.WriteSQLLog("LMH010DAC", "GetRcvNmDtl", cmd)

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

    '2015.08.24 BYK セミEDI対応START
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
        Me._StrSql.Append(LMH010DAC.SQL_SELECT_M_HOL)

        Dim dt As DataTable = ds.Tables("LMH010_M_HOL")
        Dim dtSemi As DataTable = ds.Tables("LMH010_SEMIEDI_INFO")

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

        MyBase.Logger.WriteSQLLog("LMH010DAC", "SelectMHolList", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("MST_CNT")))
        reader.Close()
        Return ds

    End Function
    '2015.08.24 BYK セミEDI対応END

    '2015.08.24 BYK セミEDI対応START
#Region "休日マスタパラメータ設定"

    ''' <summary>
    ''' 休日マスタのパラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHolParameter(ByVal dt As DataTable)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOL", dt.Rows(0).Item("HOL"), DBDataType.CHAR))

    End Sub

#End Region
    '2015.08.24 BYK セミEDI対応END

#End Region



#End Region 'Method

End Class

