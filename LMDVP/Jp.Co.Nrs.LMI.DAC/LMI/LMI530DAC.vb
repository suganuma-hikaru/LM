' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI530  : セミEDI環境切り替え(丸和物産)
'  作  成  者       :  
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI530DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI530DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "参照値"

    ''' <summary>
    ''' EDI_CUST_INDEX
    ''' </summary>
    ''' <remarks></remarks>
    Private Class ediCustIndex
        Public Const CSV As Integer = 159
        Public Const Excel As Integer = 109
    End Class

#End Region ' "参照値"

#Region "検索処理 SQL"

#Region "初期表示用データ取得 SQL"

    ''' <summary>
    ''' 初期表示用データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_INIT_DATA As String = "" _
        & "SELECT TOP 1                           " & vbNewLine _
        & "    EDI_CUST_INDEX                     " & vbNewLine _
        & "FROM                                   " & vbNewLine _
        & "   (                                   " & vbNewLine _
        & "    SELECT                             " & vbNewLine _
        & "          1 AS SEQ                     " & vbNewLine _
        & "        , EDI_CUST_INDEX               " & vbNewLine _
        & "    FROM                               " & vbNewLine _
        & "        $LM_MST$..M_EDI_CUST           " & vbNewLine _
        & "    WHERE                              " & vbNewLine _
        & "        INOUT_KB = '0'                 " & vbNewLine _
        & "    AND NRS_BR_CD = @NRS_BR_CD         " & vbNewLine _
        & "    AND WH_CD = '400'                  " & vbNewLine _
        & "    AND CUST_CD_L = '00330'            " & vbNewLine _
        & "    AND CUST_CD_M = '00'               " & vbNewLine _
        & "    AND SYS_DEL_FLG = '0'              " & vbNewLine _
        & "    UNION ALL                          " & vbNewLine _
        & "    SELECT                             " & vbNewLine _
        & "          2 AS SEQ                     " & vbNewLine _
        & "        , EDI_CUST_INDEX               " & vbNewLine _
        & "    FROM                               " & vbNewLine _
        & "        $LM_MST$..M_SEMIEDI_INFO_STATE " & vbNewLine _
        & "    WHERE                              " & vbNewLine _
        & "        NRS_BR_CD = @NRS_BR_CD         " & vbNewLine _
        & "    AND WH_CD = '400'                  " & vbNewLine _
        & "    AND CUST_CD_L = '00330'            " & vbNewLine _
        & "    AND CUST_CD_M = '00'               " & vbNewLine _
        & "    AND INOUT_KB = '20'                " & vbNewLine _
        & "    AND SYS_DEL_FLG = '0'              " & vbNewLine _
        & "    ) EDI_CUST                         " & vbNewLine _
        & "ORDER BY                               " & vbNewLine _
        & "    SEQ                                " & vbNewLine _
        & ""

#End Region ' "初期表示用データ取得 SQL"

#End Region ' "検索処理 SQL"

#Region "物理削除 SQL"

#Region "EDI対象荷主マスタ 物理削除 SQL"

    ''' <summary>
    ''' EDI対象荷主マスタ 物理削除 SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_EDI_CUST As String = "" _
        & "DELETE                     " & vbNewLine _
        & "FROM                       " & vbNewLine _
        & "    $LM_MST$..M_EDI_CUST   " & vbNewLine _
        & "WHERE                      " & vbNewLine _
        & "    INOUT_KB = '0'         " & vbNewLine _
        & "AND NRS_BR_CD = @NRS_BR_CD " & vbNewLine _
        & "AND WH_CD = '400'          " & vbNewLine _
        & "AND CUST_CD_L = '00330'    " & vbNewLine _
        & "AND CUST_CD_M = '00'       " & vbNewLine _
        & "--AND SYS_DEL_FLG = '0'    " & vbNewLine _
        & ""

#End Region ' "EDI対象荷主マスタ 物理削除 SQL"

#Region "セミEDI情報設定マスタ 物理削除 SQL"

    ''' <summary>
    ''' セミEDI情報設定マスタ 物理削除 SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_SEMIEDI_INFO_STATE As String = "" _
        & "DELETE                             " & vbNewLine _
        & "FROM                               " & vbNewLine _
        & "    $LM_MST$..M_SEMIEDI_INFO_STATE " & vbNewLine _
        & "WHERE                              " & vbNewLine _
        & "    NRS_BR_CD = @NRS_BR_CD         " & vbNewLine _
        & "AND WH_CD = '400'                  " & vbNewLine _
        & "AND CUST_CD_L = '00330'            " & vbNewLine _
        & "AND CUST_CD_M = '00'               " & vbNewLine _
        & "AND INOUT_KB = '20'                " & vbNewLine _
        & "--AND SYS_DEL_FLG = '0'            " & vbNewLine _
        & ""

#End Region ' "セミEDI情報設定マスタ 物理削除 SQL"

#End Region ' "物理削除 SQL"

#Region "登録 SQL"

#Region "EDI対象荷主マスタ 登録 SQL"

#Region "EDI対象荷主マスタ 登録 共通部 SQL"

    ''' <summary>
    ''' EDI対象荷主マスタ 登録 共通部 SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_EDI_CUST_BASE As String = "" _
        & "INSERT INTO $LM_MST$..M_EDI_CUST ( " & vbNewLine _
        & "      DEL_KB                       " & vbNewLine _
        & "    , INOUT_KB                     " & vbNewLine _
        & "    , NRS_BR_CD                    " & vbNewLine _
        & "    , WH_CD                        " & vbNewLine _
        & "    , CUST_CD_L                    " & vbNewLine _
        & "    , CUST_CD_M                    " & vbNewLine _
        & "    , CUST_NM                      " & vbNewLine _
        & "    , FLAG_01                      " & vbNewLine _
        & "    , FLAG_02                      " & vbNewLine _
        & "    , FLAG_03                      " & vbNewLine _
        & "    , FLAG_04                      " & vbNewLine _
        & "    , FLAG_05                      " & vbNewLine _
        & "    , FLAG_06                      " & vbNewLine _
        & "    , FLAG_07                      " & vbNewLine _
        & "    , FLAG_08                      " & vbNewLine _
        & "    , FLAG_09                      " & vbNewLine _
        & "    , FLAG_10                      " & vbNewLine _
        & "    , FLAG_11                      " & vbNewLine _
        & "    , FLAG_12                      " & vbNewLine _
        & "    , FLAG_13                      " & vbNewLine _
        & "    , FLAG_14                      " & vbNewLine _
        & "    , FLAG_15                      " & vbNewLine _
        & "    , FLAG_16                      " & vbNewLine _
        & "    , FLAG_17                      " & vbNewLine _
        & "    , FLAG_18                      " & vbNewLine _
        & "    , FLAG_19                      " & vbNewLine _
        & "    , FLAG_20                      " & vbNewLine _
        & "    , DATA_01                      " & vbNewLine _
        & "    , DATA_02                      " & vbNewLine _
        & "    , DATA_03                      " & vbNewLine _
        & "    , DATA_04                      " & vbNewLine _
        & "    , DATA_05                      " & vbNewLine _
        & "    , DATA_06                      " & vbNewLine _
        & "    , DATA_07                      " & vbNewLine _
        & "    , DATA_08                      " & vbNewLine _
        & "    , DATA_09                      " & vbNewLine _
        & "    , DATA_10                      " & vbNewLine _
        & "    , DATA_11                      " & vbNewLine _
        & "    , DATA_12                      " & vbNewLine _
        & "    , DATA_13                      " & vbNewLine _
        & "    , DATA_14                      " & vbNewLine _
        & "    , DATA_15                      " & vbNewLine _
        & "    , DATA_16                      " & vbNewLine _
        & "    , DATA_17                      " & vbNewLine _
        & "    , DATA_18                      " & vbNewLine _
        & "    , DATA_19                      " & vbNewLine _
        & "    , DATA_20                      " & vbNewLine _
        & "    , CRT_USER                     " & vbNewLine _
        & "    , CRT_DATE                     " & vbNewLine _
        & "    , CRT_TIME                     " & vbNewLine _
        & "    , UPD_USER                     " & vbNewLine _
        & "    , UPD_DATE                     " & vbNewLine _
        & "    , UPD_TIME                     " & vbNewLine _
        & "    , EDI_CUST_INDEX               " & vbNewLine _
        & "    , RCV_NM_HED                   " & vbNewLine _
        & "    , RCV_NM_DTL                   " & vbNewLine _
        & "    , RCV_NM_EXT                   " & vbNewLine _
        & "    , SND_NM                       " & vbNewLine _
        & "    , ORDER_CHECK_FLG              " & vbNewLine _
        & "    , AUTO_MATOME_FLG              " & vbNewLine _
        & "    , NINUSHI_SET_FLG              " & vbNewLine _
        & "    , ADDEXE_INPUT_DIR             " & vbNewLine _
        & "    , ADDEXE_FILE_NM               " & vbNewLine _
        & "    , ADDEXE_FILE_EXTENTION        " & vbNewLine _
        & "    , SYS_ENT_DATE                 " & vbNewLine _
        & "    , SYS_ENT_TIME                 " & vbNewLine _
        & "    , SYS_ENT_PGID                 " & vbNewLine _
        & "    , SYS_ENT_USER                 " & vbNewLine _
        & "    , SYS_UPD_DATE                 " & vbNewLine _
        & "    , SYS_UPD_TIME                 " & vbNewLine _
        & "    , SYS_UPD_PGID                 " & vbNewLine _
        & "    , SYS_UPD_USER                 " & vbNewLine _
        & "    , SYS_DEL_FLG                  " & vbNewLine _
        & ""

#End Region ' "EDI対象荷主マスタ 登録 共通部 SQL"

#Region "EDI対象荷主マスタ 登録 xlsm取込用 SQL"

    ''' <summary>
    ''' EDI対象荷主マスタ 登録 xlsm取込用 SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_EDI_CUST_EXCEL As String = "" _
        & ") VALUES (               " & vbNewLine _
        & "      '0'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , @NRS_BR_CD         " & vbNewLine _
        & "    , '400'              " & vbNewLine _
        & "    , '00330'            " & vbNewLine _
        & "    , '00'               " & vbNewLine _
        & "    , '丸和物産株式会社' " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , @CRT_USER          " & vbNewLine _
        & "    , @CRT_DATE          " & vbNewLine _
        & "    , @CRT_TIME          " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , 109                " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , '9'                " & vbNewLine _
        & "    , '0'                " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , ''                 " & vbNewLine _
        & "    , @SYS_ENT_DATE      " & vbNewLine _
        & "    , @SYS_ENT_TIME      " & vbNewLine _
        & "    , @SYS_ENT_PGID      " & vbNewLine _
        & "    , @SYS_ENT_USER      " & vbNewLine _
        & "    , @SYS_UPD_DATE      " & vbNewLine _
        & "    , @SYS_UPD_TIME      " & vbNewLine _
        & "    , @SYS_UPD_PGID      " & vbNewLine _
        & "    , @SYS_UPD_USER      " & vbNewLine _
        & "    , @SYS_DEL_FLG       " & vbNewLine _
        & ")                        " & vbNewLine _
        & ""

#End Region ' "EDI対象荷主マスタ 登録 xlsm取込用 SQL"

#Region "EDI対象荷主マスタ 登録 CSV取込用 SQL"

    ''' <summary>
    ''' EDI対象荷主マスタ 登録 CSV取込用 SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_EDI_CUST_CSV As String = "" _
        & ") VALUES (                    " & vbNewLine _
        & "      '0'                     " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , @NRS_BR_CD              " & vbNewLine _
        & "    , '400'                   " & vbNewLine _
        & "    , '00330'                 " & vbNewLine _
        & "    , '00'                    " & vbNewLine _
        & "    , '丸和物産株式会社'      " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , '1'                     " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , @CRT_USER               " & vbNewLine _
        & "    , @CRT_DATE               " & vbNewLine _
        & "    , @CRT_TIME               " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , 159                     " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , 'H_OUTKAEDI_DTL_MRCCSV' " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , '9'                     " & vbNewLine _
        & "    , '0'                     " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , ''                      " & vbNewLine _
        & "    , @SYS_ENT_DATE           " & vbNewLine _
        & "    , @SYS_ENT_TIME           " & vbNewLine _
        & "    , @SYS_ENT_PGID           " & vbNewLine _
        & "    , @SYS_ENT_USER           " & vbNewLine _
        & "    , @SYS_UPD_DATE           " & vbNewLine _
        & "    , @SYS_UPD_TIME           " & vbNewLine _
        & "    , @SYS_UPD_PGID           " & vbNewLine _
        & "    , @SYS_UPD_USER           " & vbNewLine _
        & "    , @SYS_DEL_FLG            " & vbNewLine _
        & ")                             " & vbNewLine _
        & ""

#End Region ' "EDI対象荷主マスタ 登録 CSV取込用 SQL"

#End Region ' "EDI対象荷主マスタ 登録 SQL"

#Region "セミEDI情報設定マスタ 登録 SQL"

#Region "セミEDI情報設定マスタ 共通部 登録 SQL"

    ''' <summary>
    ''' セミEDI情報設定マスタ 共通部 登録 SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SEMIEDI_INFO_STATE_BASE As String = "" _
        & "INSERT INTO $LM_MST$..M_SEMIEDI_INFO_STATE ( " & vbNewLine _
        & "      NRS_BR_CD                              " & vbNewLine _
        & "    , WH_CD                                  " & vbNewLine _
        & "    , CUST_CD_L                              " & vbNewLine _
        & "    , CUST_CD_M                              " & vbNewLine _
        & "    , INOUT_KB                               " & vbNewLine _
        & "    , EDI_CUST_INDEX                         " & vbNewLine _
        & "    , SEMI_EDI_FLAG                          " & vbNewLine _
        & "    , SEMI_EDI_PRINT_FLAG                    " & vbNewLine _
        & "    , RCV_INPUT_DIR                          " & vbNewLine _
        & "    , WORK_INPUT_DIR                         " & vbNewLine _
        & "    , BACKUP_INPUT_DIR                       " & vbNewLine _
        & "    , RCV_FILE_EXTENTION                     " & vbNewLine _
        & "    , DELIMITER_KB                           " & vbNewLine _
        & "    , RCV_FILE_NM                            " & vbNewLine _
        & "    , RCV_FILE_COL_CNT                       " & vbNewLine _
        & "    , RCV_FILE_WORKRENAME                    " & vbNewLine _
        & "    , RCV_FILE_BACKRENAME                    " & vbNewLine _
        & "    , TOP_ROW_CNT                            " & vbNewLine _
        & "    , PLURAL_FILE_FLAG                       " & vbNewLine _
        & "    , PRINT_CLASS_NM                         " & vbNewLine _
        & "    , SHEET_NM                               " & vbNewLine _
        & "    , KEY_COL_CNT                            " & vbNewLine _
        & "    , RCV_TBL_INS_FLG                        " & vbNewLine _
        & "    , FILE_CHICE_KBN                         " & vbNewLine _
        & "    , BUCKING_NO_1                           " & vbNewLine _
        & "    , BUCKING_NO_2                           " & vbNewLine _
        & "    , BUCKING_NO_3                           " & vbNewLine _
        & "    , BUCKING_TERM_1                         " & vbNewLine _
        & "    , BUCKING_TERM_2                         " & vbNewLine _
        & "    , BUCKING_TERM_3                         " & vbNewLine _
        & "    , DEVIDE_NO_1                            " & vbNewLine _
        & "    , DEVIDE_NO_2                            " & vbNewLine _
        & "    , DEVIDE_NO_3                            " & vbNewLine _
        & "    , L_DEL_KB_NO                            " & vbNewLine _
        & "    , L_OUTKA_PLAN_DATE_NO                   " & vbNewLine _
        & "    , L_ARR_PLAN_DATE_NO                     " & vbNewLine _
        & "    , L_DEST_CD_NO                           " & vbNewLine _
        & "    , L_DEST_NM_NO                           " & vbNewLine _
        & "    , L_DEST_ZIP_NO                          " & vbNewLine _
        & "    , L_DEST_AD_1_NO                         " & vbNewLine _
        & "    , L_DEST_AD_2_NO                         " & vbNewLine _
        & "    , L_DEST_AD_3_NO                         " & vbNewLine _
        & "    , L_DEST_TEL_NO                          " & vbNewLine _
        & "    , L_DEST_JIS_CD_NO                       " & vbNewLine _
        & "    , L_SHIP_CD_NO                           " & vbNewLine _
        & "    , L_DEST_CUST_ORD_NO                     " & vbNewLine _
        & "    , L_BUYER_ORD_NO                         " & vbNewLine _
        & "    , L_REMARK_NO                            " & vbNewLine _
        & "    , L_FREE_C01_NO                          " & vbNewLine _
        & "    , L_FREE_C02_NO                          " & vbNewLine _
        & "    , L_FREE_C03_NO                          " & vbNewLine _
        & "    , L_FREE_C24_NO                          " & vbNewLine _
        & "    , L_FREE_C25_NO                          " & vbNewLine _
        & "    , L_FREE_C26_NO                          " & vbNewLine _
        & "    , L_FREE_N01_NO                          " & vbNewLine _
        & "    , L_FREE_N02_NO                          " & vbNewLine _
        & "    , L_FREE_N03_NO                          " & vbNewLine _
        & "    , M_DEL_KB_NO                            " & vbNewLine _
        & "    , M_CUST_ORD_NO_DTL_NO                   " & vbNewLine _
        & "    , M_CUST_GOODS_CD_NO                     " & vbNewLine _
        & "    , M_GOODS_NM_NO                          " & vbNewLine _
        & "    , M_LOT_NO_NO                            " & vbNewLine _
        & "    , M_SERIAL_NO_NO                         " & vbNewLine _
        & "    , M_DEF_ALCTD_KB                         " & vbNewLine _
        & "    , M_OUTKA_TTL_NB_NO                      " & vbNewLine _
        & "    , M_OUTKA_TTL_QT_NO                      " & vbNewLine _
        & "    , M_IRIME_NO                             " & vbNewLine _
        & "    , M_IRIME_UT_NO                          " & vbNewLine _
        & "    , M_REMARK_NO                            " & vbNewLine _
        & "    , M_FREE_C01_NO                          " & vbNewLine _
        & "    , M_FREE_C02_NO                          " & vbNewLine _
        & "    , M_FREE_C03_NO                          " & vbNewLine _
        & "    , M_FREE_C24_NO                          " & vbNewLine _
        & "    , M_FREE_C25_NO                          " & vbNewLine _
        & "    , M_FREE_C26_NO                          " & vbNewLine _
        & "    , M_FREE_N01_NO                          " & vbNewLine _
        & "    , M_FREE_N02_NO                          " & vbNewLine _
        & "    , M_FREE_N03_NO                          " & vbNewLine _
        & "    , EDI_TORIKESI_FLG                       " & vbNewLine _
        & "    , EDI_TORITERM_FLG                       " & vbNewLine _
        & "    , DTL_DATACHECK_FLG                      " & vbNewLine _
        & "    , DTL_OUTKAPKGNB_CALC_FLG                " & vbNewLine _
        & "    , SYS_ENT_DATE                           " & vbNewLine _
        & "    , SYS_ENT_TIME                           " & vbNewLine _
        & "    , SYS_ENT_PGID                           " & vbNewLine _
        & "    , SYS_ENT_USER                           " & vbNewLine _
        & "    , SYS_UPD_DATE                           " & vbNewLine _
        & "    , SYS_UPD_TIME                           " & vbNewLine _
        & "    , SYS_UPD_PGID                           " & vbNewLine _
        & "    , SYS_UPD_USER                           " & vbNewLine _
        & "    , SYS_DEL_FLG                            " & vbNewLine _
        & ""

#End Region ' "セミEDI情報設定マスタ 共通部 登録 SQL"

#Region "セミEDI情報設定マスタ xlsm取込用 登録 SQL"

    ''' <summary>
    ''' セミEDI情報設定マスタ xlsm取込用 登録 SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SEMIEDI_INFO_STATE_EXCEL As String = "" _
        & ") VALUES (          " & vbNewLine _
        & "      @NRS_BR_CD    " & vbNewLine _
        & "    , '400'         " & vbNewLine _
        & "    , '00330'       " & vbNewLine _
        & "    , '00'          " & vbNewLine _
        & "    , '20'          " & vbNewLine _
        & "    , 109           " & vbNewLine _
        & "    , '1'           " & vbNewLine _
        & "    , '0'           " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , 'xlsm'        " & vbNewLine _
        & "    , '04'          " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , 22            " & vbNewLine _
        & "    , 'OPE_'        " & vbNewLine _
        & "    , 'RCV_'        " & vbNewLine _
        & "    , 4             " & vbNewLine _
        & "    , '0'           " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , 0             " & vbNewLine _
        & "    , '0'           " & vbNewLine _
        & "    , '01'          " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , '1'           " & vbNewLine _
        & "    , '2'           " & vbNewLine _
        & "    , '3'           " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , '18'          " & vbNewLine _
        & "    , '17'          " & vbNewLine _
        & "    , '4'           " & vbNewLine _
        & "    , '5'           " & vbNewLine _
        & "    , '9'           " & vbNewLine _
        & "    , '10'          " & vbNewLine _
        & "    , '11'          " & vbNewLine _
        & "    , '12'          " & vbNewLine _
        & "    , '13'          " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , '3'           " & vbNewLine _
        & "    , '7'           " & vbNewLine _
        & "    , '8'           " & vbNewLine _
        & "    , '5'           " & vbNewLine _
        & "    , '6'           " & vbNewLine _
        & "    , '10'          " & vbNewLine _
        & "    , '11'          " & vbNewLine _
        & "    , '12'          " & vbNewLine _
        & "    , '19'          " & vbNewLine _
        & "    , '20'          " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , '14'          " & vbNewLine _
        & "    , '15'          " & vbNewLine _
        & "    , '22'          " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , '01'          " & vbNewLine _
        & "    , '16'          " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , '21'          " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , '0'           " & vbNewLine _
        & "    , ' '           " & vbNewLine _
        & "    , ' '           " & vbNewLine _
        & "    , ' '           " & vbNewLine _
        & "    , @SYS_ENT_DATE " & vbNewLine _
        & "    , @SYS_ENT_TIME " & vbNewLine _
        & "    , @SYS_ENT_PGID " & vbNewLine _
        & "    , @SYS_ENT_USER " & vbNewLine _
        & "    , @SYS_UPD_DATE " & vbNewLine _
        & "    , @SYS_UPD_TIME " & vbNewLine _
        & "    , @SYS_UPD_PGID " & vbNewLine _
        & "    , @SYS_UPD_USER " & vbNewLine _
        & "    , @SYS_DEL_FLG  " & vbNewLine _
        & ")                   " & vbNewLine _
        & ""

#End Region ' "セミEDI情報設定マスタ xlsm取込用 登録 SQL"

#Region "セミEDI情報設定マスタ CSV取込用 登録 SQL"

    ''' <summary>
    ''' セミEDI情報設定マスタ CSV取込用 登録 SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SEMIEDI_INFO_STATE_CSV As String = "" _
        & ") VALUES (          " & vbNewLine _
        & "      @NRS_BR_CD    " & vbNewLine _
        & "    , '400'         " & vbNewLine _
        & "    , '00330'       " & vbNewLine _
        & "    , '00'          " & vbNewLine _
        & "    , '20'          " & vbNewLine _
        & "    , 159           " & vbNewLine _
        & "    , '1'           " & vbNewLine _
        & "    , '0'           " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , 'csv'         " & vbNewLine _
        & "    , '01'          " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , 113           " & vbNewLine _
        & "    , 'OPE_'        " & vbNewLine _
        & "    , 'RCV_'        " & vbNewLine _
        & "    , 1             " & vbNewLine _
        & "    , '0'           " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , 0             " & vbNewLine _
        & "    , '0'           " & vbNewLine _
        & "    , '01'          " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , '1'           " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , ''            " & vbNewLine _
        & "    , '0'           " & vbNewLine _
        & "    , ' '           " & vbNewLine _
        & "    , ' '           " & vbNewLine _
        & "    , ' '           " & vbNewLine _
        & "    , @SYS_ENT_DATE " & vbNewLine _
        & "    , @SYS_ENT_TIME " & vbNewLine _
        & "    , @SYS_ENT_PGID " & vbNewLine _
        & "    , @SYS_ENT_USER " & vbNewLine _
        & "    , @SYS_UPD_DATE " & vbNewLine _
        & "    , @SYS_UPD_TIME " & vbNewLine _
        & "    , @SYS_UPD_PGID " & vbNewLine _
        & "    , @SYS_UPD_USER " & vbNewLine _
        & "    , @SYS_DEL_FLG  " & vbNewLine _
        & ")                   " & vbNewLine _
        & ""

#End Region ' "セミEDI情報設定マスタ CSV取込用 登録 SQL"

#End Region ' "セミEDI情報設定マスタ 登録 SQL"

#End Region ' "登録 SQL"

#End Region ' "Const"

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
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

#End Region ' "Field"

#Region "Method"

#Region "SQLメイン処理"

#Region "初期表示用データ取得"

    ''' <summary>
    ''' 初期表示用データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInitData(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI530IN")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' SQL作成
        Me._StrSql.Append(LMI530DAC.SQL_SELECT_INIT_DATA)

        ' パラメータ設定
        Call Me.SetSelectInitDataParameter()

        ' スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI530DAC", "SelectInitData", cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                ' 取得データの格納先のマッピング
                map.Add("EDI_CUST_INDEX", "EDI_CUST_INDEX")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI530OUT")

            End Using

        End Using

        Return ds

    End Function

#End Region ' "初期表示用データ取得"

#Region "EDI対象荷主マスタ 物理削除"

    ''' <summary>
    ''' EDI対象荷主マスタ 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteEdiCust(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI530IN")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' SQL作成
        Me._StrSql.Append(LMI530DAC.SQL_DELETE_EDI_CUST)

        ' パラメータ設定
        Call Me.SetDeleteEdiCustParameter()

        ' スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI530DAC", "DeleteEdiCust", cmd)

            ' SQLの発行
            MyBase.GetUpdateResult(cmd)

        End Using

        Return ds

    End Function

#End Region ' "EDI対象荷主マスタ 物理削除"

#Region "セミEDI情報設定マスタ 物理削除"

    ''' <summary>
    ''' セミEDI情報設定マスタ 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteSemiediInfoState(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI530IN")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' SQL作成
        Me._StrSql.Append(LMI530DAC.SQL_DELETE_SEMIEDI_INFO_STATE)

        ' パラメータ設定
        Call Me.SetDeleteSemiediInfoStateParameter()

        ' スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI530DAC", "DeleteSemiediInfoState", cmd)

            ' SQLの発行
            MyBase.GetUpdateResult(cmd)

        End Using

        Return ds

    End Function

#End Region ' "セミEDI情報設定マスタ 物理削除"

#Region "EDI対象荷主マスタ 登録"

    ''' <summary>
    ''' EDI対象荷主マスタ 登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertEdiCust(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI530IN")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' SQL作成
        Me._StrSql.Append(LMI530DAC.SQL_INSERT_EDI_CUST_BASE)
        If Me._Row.Item("EDI_CUST_INDEX").ToString() = LMI530DAC.ediCustIndex.Excel.ToString() Then
            Me._StrSql.Append(LMI530DAC.SQL_INSERT_EDI_CUST_EXCEL)
        Else
            Me._StrSql.Append(LMI530DAC.SQL_INSERT_EDI_CUST_CSV)
        End If

        ' パラメータ設定
        Call Me.SetInsertEdiCustParameter()

        ' スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI530DAC", "InsertEdiCust", cmd)

            ' SQLの発行
            MyBase.GetUpdateResult(cmd)

        End Using

        Return ds

    End Function

#End Region ' "EDI対象荷主マスタ 登録"

#Region "セミEDI情報設定マスタ 登録"

    ''' <summary>
    ''' セミEDI情報設定マスタ 登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSemiediInfoState(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI530IN")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' SQL作成
        Me._StrSql.Append(LMI530DAC.SQL_INSERT_SEMIEDI_INFO_STATE_BASE)
        If Me._Row.Item("EDI_CUST_INDEX").ToString() = LMI530DAC.ediCustIndex.Excel.ToString() Then
            Me._StrSql.Append(LMI530DAC.SQL_INSERT_SEMIEDI_INFO_STATE_EXCEL)
        Else
            Me._StrSql.Append(LMI530DAC.SQL_INSERT_SEMIEDI_INFO_STATE_CSV)
        End If

        ' パラメータ設定
        Call Me.SetInsertSemiediInfoStateParameter()

        ' スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMI530DAC", "InsertSemiediInfoState", cmd)

            ' SQLの発行
            MyBase.GetUpdateResult(cmd)

        End Using

        Return ds

    End Function

#End Region ' "セミEDI情報設定マスタ 登録"

#End Region ' "SQLメイン処理"

#Region "パラメータ設定"

#Region "初期表示用データ取得 パラメータ設定"

    ''' <summary>
    ''' 初期表示用データ取得 パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSelectInitDataParameter()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region ' "初期表示用データ取得 パラメータ設定"

#Region "EDI対象荷主マスタ 物理削除 パラメータ設定"

    ''' <summary>
    ''' EDI対象荷主マスタ 物理削除 パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDeleteEdiCustParameter()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region ' "EDI対象荷主マスタ 物理削除 パラメータ設定"

#Region "セミEDI情報設定マスタ 物理削除 パラメータ設定"


    ''' <summary>
    ''' セミEDI情報設定マスタ 物理削除 パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDeleteSemiediInfoStateParameter()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

#End Region ' "セミEDI情報設定マスタ 物理削除 パラメータ設定"

#Region "EDI対象荷主マスタ 登録 パラメータ設定"

    ''' <summary>
    ''' EDI対象荷主マスタ 登録 パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInsertEdiCustParameter()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        End With

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_TIME", GetColonEditTime(MyBase.GetSystemTime()), DBDataType.CHAR))

        ' システム共通項目(登録時) パラメータ設定
        Call SetParamCommonSystemIns()

    End Sub

#End Region ' "EDI対象荷主マスタ 登録 パラメータ設定"

#Region "セミEDI情報設定マスタ 登録 パラメータ設定"

    ''' <summary>
    ''' セミEDI情報設定マスタ 登録 パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInsertSemiediInfoStateParameter()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        End With

        ' システム共通項目(登録時) パラメータ設定
        Call SetParamCommonSystemIns()

    End Sub

#End Region ' "セミEDI情報設定マスタ 登録 パラメータ設定"

#Region "システム共通項目(登録時) パラメータ設定"

    ''' <summary>
    ''' システム共通項目(登録時) パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        ' パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

    End Sub

#End Region

#Region "スキーマ名称設定"

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region ' "スキーマ名称設定"

#End Region ' "パラメータ設定"

#Region "編集・変換"

#Region "時間コロン編集"

    ''' <summary>
    ''' 時間コロン編集
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

    End Function

#End Region ' "時間コロン編集"

#End Region ' "編集・変換"

#End Region ' "Method"

End Class
