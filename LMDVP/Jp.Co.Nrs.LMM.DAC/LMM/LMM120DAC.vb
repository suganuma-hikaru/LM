' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタ
'  プログラムID     :  LMM120DAC : 単価マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM120DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM120DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "編集処理 SQL"

    ''' <summary>
    ''' 単価マスタ排他チェック処理(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_HAITA_CHK As String = "SELECT                                       " & vbNewLine _
                                          & "       COUNT(NRS_BR_CD)     AS    SELECT_CNT " & vbNewLine _
                                          & "FROM                                         " & vbNewLine _
                                          & "     $LM_MST$..M_TANKA                       " & vbNewLine _
                                          & "WHERE                                        " & vbNewLine _
                                          & "       NRS_BR_CD          =    @NRS_BR_CD    " & vbNewLine _
                                          & "AND    CUST_CD_L          =    @CUST_CD_L    " & vbNewLine _
                                          & "AND    CUST_CD_M          =    @CUST_CD_M    " & vbNewLine _
                                          & "AND    UP_GP_CD_1         =    @UP_GP_CD_1   " & vbNewLine _
                                          & "AND    STR_DATE           =    @STR_DATE     " & vbNewLine _
                                          & "AND    SYS_UPD_DATE       =    @HAITA_DATE   " & vbNewLine _
                                          & "AND    SYS_UPD_TIME       =    @HAITA_TIME   " & vbNewLine

#End Region

#Region "検索処理 SQL"

    ''' <summary>
    ''' 単価マスタ検索処理(件数取得(FROM句))用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_SELECT As String = " SELECT COUNT(MAIN.SELECT_CNT)	   AS SELECT_CNT   " & vbNewLine _
                                                    & " FROM (                                             " & vbNewLine

    ''' <summary>
    ''' 単価マスタ検索処理(件数取得(FROM句))用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_FROM As String = " SELECT TNK.NRS_BR_CD	   AS SELECT_CNT   " & vbNewLine


    ''' <summary>
    ''' 単価マスタ検索処理(データ取得(SELECT句))用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_SELECT As String = " SELECT                                              " & vbNewLine _
                                                   & "     TNK.NRS_BR_CD           AS    NRS_BR_CD         " & vbNewLine _
                                                   & "    ,BRM.NRS_BR_NM           AS    NRS_BR_NM         " & vbNewLine _
                                                   & "    ,TNK.CUST_CD_L           AS    CUST_CD_L         " & vbNewLine _
                                                   & "    ,CST.CUST_NM_L           AS    CUST_NM_L         " & vbNewLine _
                                                   & "    ,TNK.CUST_CD_M           AS    CUST_CD_M         " & vbNewLine _
                                                   & "    ,CST.CUST_NM_M           AS    CUST_NM_M         " & vbNewLine _
                                                   & "    ,TNK.UP_GP_CD_1          AS    UP_GP_CD_1        " & vbNewLine _
                                                   & "    ,TNK.STR_DATE            AS    STR_DATE          " & vbNewLine _
                                                   & "    ,TNK.REC_NO              AS    REC_NO            " & vbNewLine _
                                                   & "    ,TNK.REMARK              AS    REMARK            " & vbNewLine _
                                                   & "    ,TNK.STORAGE_KB1         AS    STORAGE_KB1       " & vbNewLine _
                                                   & "    ,TNK.STORAGE_KB2         AS    STORAGE_KB2       " & vbNewLine _
                                                   & "    ,TNK.HANDLING_IN_KB      AS    HANDLING_IN_KB    " & vbNewLine _
                                                   & "    ,TNK.HANDLING_OUT_KB     AS    HANDLING_OUT_KB   " & vbNewLine _
                                                   & "    ,TNK.STORAGE_1           AS    STORAGE_1         " & vbNewLine _
                                                   & "    ,TNK.STORAGE_2           AS    STORAGE_2         " & vbNewLine _
                                                   & "    ,TNK.HANDLING_IN         AS    HANDLING_IN       " & vbNewLine _
                                                   & "    ,TNK.MINI_TEKI_IN_AMO    AS    MINI_TEKI_IN_AMO  " & vbNewLine _
                                                   & "    ,TNK.HANDLING_OUT        AS    HANDLING_OUT      " & vbNewLine _
                                                   & "    ,TNK.MINI_TEKI_OUT_AMO   AS    MINI_TEKI_OUT_AMO " & vbNewLine _
                                                   & "    ,TNK.KIWARI_KB           AS    KIWARI_KB         " & vbNewLine _
                                                   & "    ,CST.ITEM_CURR_CD        AS    ITEM_CURR_CD      " & vbNewLine _
                                                   & "    ,TNK.SYS_ENT_DATE        AS    SYS_ENT_DATE      " & vbNewLine _
                                                   & "    ,USE1.USER_NM            AS    SYS_ENT_USER_NM   " & vbNewLine _
                                                   & "    ,TNK.SYS_UPD_DATE        AS    SYS_UPD_DATE      " & vbNewLine _
                                                   & "    ,TNK.SYS_UPD_TIME        AS    SYS_UPD_TIME      " & vbNewLine _
                                                   & "    ,USE2.USER_NM            AS    SYS_UPD_USER_NM   " & vbNewLine _
                                                   & "    ,TNK.SYS_DEL_FLG         AS    SYS_DEL_FLG       " & vbNewLine _
                                                   & "    ,KBN1.KBN_NM1            AS    SYS_DEL_NM	       " & vbNewLine _
                                                   & "    ,ISNULL(TNK.AVAL_YN,'')  AS    AVAL_YN           " & vbNewLine _
                                                   & "    ,ISNULL(KBN2.KBN_NM1,'') AS    AVAL_YN_NM        " & vbNewLine _
                                                   & "    ,TNK.PRODUCT_SEG_CD      AS    PRODUCT_SEG_CD    " & vbNewLine _
                                                   & "    ,SEG.SGMT_L_NM           AS    PRODUCT_SEG_NM_L  " & vbNewLine _
                                                   & "    ,SEG.SGMT_M_NM           AS    PRODUCT_SEG_NM_M  " & vbNewLine _
                                                   & "    ,TNK.APPROVAL_CD         AS    APPROVAL_CD       " & vbNewLine _
                                                   & "    ,ISNULL(KBN3.KBN_NM1,'') AS    APPROVAL_NM       " & vbNewLine _
                                                   & "    ,USE3.USER_NM            AS    APPROVAL_USER     " & vbNewLine _
                                                   & "    ,TNK.APPROVAL_DATE       AS    APPROVAL_DATE     " & vbNewLine _
                                                   & "    ,TNK.APPROVAL_TIME       AS    APPROVAL_TIME     " & vbNewLine



    ''' <summary>
    ''' 単価マスタ検索処理(データ取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_FROM As String = " FROM                                               " & vbNewLine _
                                                 & "    $LM_MST$..M_TANKA    TNK                        " & vbNewLine _
                                                 & "LEFT JOIN $LM_MST$..M_NRS_BR    BRM                 " & vbNewLine _
                                                 & "ON  BRM.NRS_BR_CD         =    TNK.NRS_BR_CD        " & vbNewLine _
                                                 & "AND BRM.SYS_DEL_FLG       =    '0'                  " & vbNewLine _
                                                 & "LEFT JOIN $LM_MST$..M_CUST    CST                   " & vbNewLine _
                                                 & "ON  CST.NRS_BR_CD         =    TNK.NRS_BR_CD        " & vbNewLine _
                                                 & "AND CST.CUST_CD_L         =    TNK.CUST_CD_L        " & vbNewLine _
                                                 & "AND CST.CUST_CD_M         =    TNK.CUST_CD_M        " & vbNewLine _
                                                 & "AND CST.CUST_CD_S         =    '00'                 " & vbNewLine _
                                                 & "AND CST.CUST_CD_SS        =    '00'                 " & vbNewLine _
                                                 & "AND CST.SYS_DEL_FLG       =    '0'                  " & vbNewLine _
                                                 & "LEFT JOIN $LM_MST$..S_USER      USE1                " & vbNewLine _
                                                 & "ON  USE1.USER_CD          =    TNK.SYS_ENT_USER     " & vbNewLine _
                                                 & "AND USE1.SYS_DEL_FLG      =    '0'                  " & vbNewLine _
                                                 & "LEFT JOIN $LM_MST$..S_USER      USE2                " & vbNewLine _
                                                 & "ON  USE2.USER_CD          =    TNK.SYS_UPD_USER     " & vbNewLine _
                                                 & "AND USE2.SYS_DEL_FLG      =    '0'                  " & vbNewLine _
                                                 & "LEFT JOIN $LM_MST$..S_USER      USE3                " & vbNewLine _
                                                 & "ON  USE3.USER_CD          =    TNK.APPROVAL_USER    " & vbNewLine _
                                                 & "AND USE3.SYS_DEL_FLG      =    '0'                  " & vbNewLine _
                                                 & "LEFT JOIN $LM_MST$..Z_KBN      KBN1                 " & vbNewLine _
                                                 & "ON  KBN1.KBN_GROUP_CD     =    'S051'               " & vbNewLine _
                                                 & "AND KBN1.KBN_CD           =    TNK.SYS_DEL_FLG      " & vbNewLine _
                                                 & "AND KBN1.SYS_DEL_FLG      =    '0'                  " & vbNewLine _
                                                 & "LEFT JOIN $LM_MST$..Z_KBN      KBN2                 " & vbNewLine _
                                                 & "ON  KBN2.KBN_GROUP_CD     =    'K017'               " & vbNewLine _
                                                 & "AND KBN2.KBN_CD           =    TNK.AVAL_YN          " & vbNewLine _
                                                 & "AND KBN2.SYS_DEL_FLG      =    '0'                  " & vbNewLine _
                                                 & "LEFT JOIN $LM_MST$..Z_KBN      KBN3                 " & vbNewLine _
                                                 & "ON  KBN3.KBN_GROUP_CD     =    'A009'               " & vbNewLine _
                                                 & "AND KBN3.KBN_CD           =    TNK.APPROVAL_CD      " & vbNewLine _
                                                 & "AND KBN3.SYS_DEL_FLG      =    '0'                  " & vbNewLine _
                                                 & "LEFT JOIN ABM_DB..M_SEGMENT    SEG                  " & vbNewLine _
                                                 & "ON  SEG.DATA_TYPE_CD      =    '00002'              " & vbNewLine _
                                                 & "AND SEG.CNCT_SEG_CD       =    TNK.PRODUCT_SEG_CD   " & vbNewLine _
                                                 & "AND SEG.KBN_LANG          =    @KBN_LANG            " & vbNewLine


    ''' <summary>
    ''' 単価マスタ検索処理(全データ表示なし時JOIN条件)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_NO_ALL_DATA_JOIN1 As String = "LEFT JOIN                                   " & vbNewLine _
                                                   & "    (SELECT                                       " & vbNewLine _
                                                   & "         TNK.NRS_BR_CD           AS    NRS_BR_CD  " & vbNewLine _
                                                   & "        ,TNK.CUST_CD_L           AS    CUST_CD_L  " & vbNewLine _
                                                   & "        ,TNK.CUST_CD_M           AS    CUST_CD_M  " & vbNewLine _
                                                   & "        ,TNK.UP_GP_CD_1          AS    UP_GP_CD_1 " & vbNewLine _
                                                   & "        ,MAX(TNK.STR_DATE)       AS    STR_DATE   " & vbNewLine _
                                                   & "     FROM                                         " & vbNewLine _
                                                   & "        $LM_MST$..M_TANKA TNK                     " & vbNewLine _
                                                   & "     LEFT JOIN $LM_MST$..M_CUST    CST            " & vbNewLine _
                                                   & "     ON  CST.NRS_BR_CD         =    TNK.NRS_BR_CD " & vbNewLine _
                                                   & "     AND CST.CUST_CD_L         =    TNK.CUST_CD_L " & vbNewLine _
                                                   & "     AND CST.CUST_CD_M         =    TNK.CUST_CD_M " & vbNewLine _
                                                   & "     AND CST.CUST_CD_S         =    '00'          " & vbNewLine _
                                                   & "     AND CST.CUST_CD_SS        =    '00'          " & vbNewLine _
                                                   & "     AND CST.SYS_DEL_FLG       =    '0'           " & vbNewLine


    ''' <summary>
    ''' 単価マスタ検索処理(全データ表示なし時JOIN条件)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_NO_ALL_DATA_JOIN2 As String = "                                            " & vbNewLine _
                                                   & "    GROUP BY                                      " & vbNewLine _
                                                   & "         TNK.NRS_BR_CD                            " & vbNewLine _
                                                   & "        ,TNK.CUST_CD_L                            " & vbNewLine _
                                                   & "        ,TNK.CUST_CD_M                            " & vbNewLine _
                                                   & "        ,TNK.UP_GP_CD_1                           " & vbNewLine _
                                                   & "    ) TNK2                                        " & vbNewLine _
                                                   & "ON  TNK.NRS_BR_CD   =   TNK2.NRS_BR_CD            " & vbNewLine _
                                                   & "AND TNK.CUST_CD_L   =   TNK2.CUST_CD_L            " & vbNewLine _
                                                   & "AND TNK.CUST_CD_M   =   TNK2.CUST_CD_M            " & vbNewLine _
                                                   & "AND TNK.UP_GP_CD_1  =   TNK2.UP_GP_CD_1           " & vbNewLine _
                                                   & "AND TNK.STR_DATE    =   TNK2.STR_DATE             " & vbNewLine




    ''' <summary>
    ''' 並び順
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                   " & vbNewLine _
                                         & "    TNK.NRS_BR_CD          " & vbNewLine _
                                         & "   ,TNK.CUST_CD_L          " & vbNewLine _
                                         & "   ,TNK.CUST_CD_M          " & vbNewLine _
                                         & "   ,TNK.UP_GP_CD_1         " & vbNewLine _
                                         & "   ,TNK.STR_DATE           " & vbNewLine

#End Region

#Region "削除/復活処理 SQL"

    ''' <summary>
    ''' 単価マスタ更新SQL(論理削除)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_DEL_TANKA_M As String = "UPDATE                                            " & vbNewLine _
                                                & "    $LM_MST$..M_TANKA                             " & vbNewLine _
                                                & "SET                                               " & vbNewLine _
                                                & "      SYS_UPD_DATE        =    @SYS_UPD_DATE      " & vbNewLine _
                                                & "     ,SYS_UPD_TIME        =    @SYS_UPD_TIME      " & vbNewLine _
                                                & "     ,SYS_UPD_PGID        =    @SYS_UPD_PGID      " & vbNewLine _
                                                & "     ,SYS_UPD_USER        =    @SYS_UPD_USER      " & vbNewLine _
                                                & "     ,SYS_DEL_FLG         =    @SYS_DEL_FLG       " & vbNewLine _
                                                & "WHERE                                             " & vbNewLine _
                                                & "      NRS_BR_CD           =    @NRS_BR_CD         " & vbNewLine _
                                                & "AND   CUST_CD_L           =    @CUST_CD_L         " & vbNewLine _
                                                & "AND   CUST_CD_M           =    @CUST_CD_M         " & vbNewLine _
                                                & "AND   UP_GP_CD_1          =    @UP_GP_CD_1        " & vbNewLine _
                                                & "AND   STR_DATE            =    @STR_DATE          " & vbNewLine _
                                                & "AND   SYS_UPD_DATE        =    @HAITA_DATE        " & vbNewLine _
                                                & "AND   SYS_UPD_TIME        =    @HAITA_TIME        " & vbNewLine


#End Region

#Region "保存処理 SQL"

    ''' <summary>
    ''' 単価マスタ重複チェック処理(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_REPEAT_TANKA As String = "SELECT                                       " & vbNewLine _
                                             & "       COUNT(NRS_BR_CD)  AS   SELECT_CNT     " & vbNewLine _
                                             & "FROM                                         " & vbNewLine _
                                             & "     $LM_MST$..M_TANKA                       " & vbNewLine _
                                             & "WHERE                                        " & vbNewLine _
                                             & "       NRS_BR_CD          =    @NRS_BR_CD    " & vbNewLine _
                                             & "AND    CUST_CD_L          =    @CUST_CD_L    " & vbNewLine _
                                             & "AND    CUST_CD_M          =    @CUST_CD_M    " & vbNewLine _
                                             & "AND    UP_GP_CD_1         =    @UP_GP_CD_1   " & vbNewLine _
                                             & "AND    STR_DATE           =    @STR_DATE     " & vbNewLine

#If True Then   'ADD 2020/12/23 017521　【LMS】単価マスタエラー通知仕様追加
    ''' <summary>
    ''' 単価マスタ単価マスタコード重複チェック処理(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CheckTankaM_UP_GP_CD As String = "SELECT                                       " & vbNewLine _
                                             & "       COUNT(NRS_BR_CD)  AS   SELECT_CNT     " & vbNewLine _
                                             & "FROM                                         " & vbNewLine _
                                             & "     $LM_MST$..M_TANKA                       " & vbNewLine _
                                             & "WHERE                                        " & vbNewLine _
                                             & "       NRS_BR_CD          =    @NRS_BR_CD    " & vbNewLine _
                                             & "AND    CUST_CD_L          =    @CUST_CD_L    " & vbNewLine _
                                             & "AND    CUST_CD_M          =    @CUST_CD_M    " & vbNewLine _
                                             & "AND    UP_GP_CD_1         =    @UP_GP_CD_1   " & vbNewLine _
                                             & "AND    SYS_DEL_FLG        =    '0'           " & vbNewLine

#End If

    ''' <summary>
    ''' 適用開始日チェック処理(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_CHK_START_DATE As String = "SELECT                                       " & vbNewLine _
                                               & "       STR_DATE          AS   STR_DATE       " & vbNewLine _
                                               & "FROM                                         " & vbNewLine _
                                               & "     $LM_MST$..M_TANKA                       " & vbNewLine _
                                               & "WHERE                                        " & vbNewLine _
                                               & "       NRS_BR_CD          =    @NRS_BR_CD    " & vbNewLine _
                                               & "AND    CUST_CD_L          =    @CUST_CD_L    " & vbNewLine _
                                               & "AND    CUST_CD_M          =    @CUST_CD_M    " & vbNewLine _
                                               & "AND    UP_GP_CD_1         =    @UP_GP_CD_1   " & vbNewLine _
                                               & "AND    SYS_DEL_FLG        =    '0'           " & vbNewLine _
                                               & "ORDER BY  STR_DATE DESC                      " & vbNewLine


    ''' <summary>
    ''' 単価マスタ新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_TANKA_M As String = "INSERT INTO                          " & vbNewLine _
                                               & "    $LM_MST$..M_TANKA                " & vbNewLine _
                                               & "    (                                " & vbNewLine _
                                               & "      NRS_BR_CD                      " & vbNewLine _
                                               & "     ,CUST_CD_L                      " & vbNewLine _
                                               & "     ,CUST_CD_M                      " & vbNewLine _
                                               & "     ,UP_GP_CD_1                     " & vbNewLine _
                                               & "     ,STR_DATE                       " & vbNewLine _
                                               & "     ,REC_NO                         " & vbNewLine _
                                               & "     ,REMARK                         " & vbNewLine _
                                               & "     ,STORAGE_KB1                    " & vbNewLine _
                                               & "     ,STORAGE_KB2                    " & vbNewLine _
                                               & "     ,HANDLING_IN_KB                 " & vbNewLine _
                                               & "     ,HANDLING_OUT_KB                " & vbNewLine _
                                               & "     ,STORAGE_1                      " & vbNewLine _
                                               & "     ,STORAGE_2                      " & vbNewLine _
                                               & "     ,HANDLING_IN                    " & vbNewLine _
                                               & "     ,MINI_TEKI_IN_AMO               " & vbNewLine _
                                               & "     ,HANDLING_OUT                   " & vbNewLine _
                                               & "     ,MINI_TEKI_OUT_AMO              " & vbNewLine _
                                               & "     ,KIWARI_KB                      " & vbNewLine _
                                               & "     ,AVAL_YN                        " & vbNewLine _
                                               & "     ,PRODUCT_SEG_CD                 " & vbNewLine _
                                               & "     ,APPROVAL_CD                    " & vbNewLine _
                                               & "     ,APPROVAL_USER                  " & vbNewLine _
                                               & "     ,APPROVAL_DATE                  " & vbNewLine _
                                               & "     ,APPROVAL_TIME                  " & vbNewLine _
                                               & "     ,SYS_ENT_DATE                   " & vbNewLine _
                                               & "     ,SYS_ENT_TIME                   " & vbNewLine _
                                               & "     ,SYS_ENT_PGID                   " & vbNewLine _
                                               & "     ,SYS_ENT_USER                   " & vbNewLine _
                                               & "     ,SYS_UPD_DATE                   " & vbNewLine _
                                               & "     ,SYS_UPD_TIME                   " & vbNewLine _
                                               & "     ,SYS_UPD_PGID                   " & vbNewLine _
                                               & "     ,SYS_UPD_USER                   " & vbNewLine _
                                               & "     ,SYS_DEL_FLG                    " & vbNewLine _
                                               & "    )                                " & vbNewLine _
                                               & "VALUES                               " & vbNewLine _
                                               & "    (                                " & vbNewLine _
                                               & "      @NRS_BR_CD                     " & vbNewLine _
                                               & "     ,@CUST_CD_L                     " & vbNewLine _
                                               & "     ,@CUST_CD_M                     " & vbNewLine _
                                               & "     ,@UP_GP_CD_1                    " & vbNewLine _
                                               & "     ,@STR_DATE                      " & vbNewLine _
                                               & "     ,@REC_NO                        " & vbNewLine _
                                               & "     ,@REMARK                        " & vbNewLine _
                                               & "     ,@STORAGE_KB1                   " & vbNewLine _
                                               & "     ,@STORAGE_KB2                   " & vbNewLine _
                                               & "     ,@HANDLING_IN_KB                " & vbNewLine _
                                               & "     ,@HANDLING_OUT_KB               " & vbNewLine _
                                               & "     ,@STORAGE_1                     " & vbNewLine _
                                               & "     ,@STORAGE_2                     " & vbNewLine _
                                               & "     ,@HANDLING_IN                   " & vbNewLine _
                                               & "     ,@MINI_TEKI_IN_AMO              " & vbNewLine _
                                               & "     ,@HANDLING_OUT                  " & vbNewLine _
                                               & "     ,@MINI_TEKI_OUT_AMO             " & vbNewLine _
                                               & "     ,@KIWARI_KB                     " & vbNewLine _
                                               & "     ,@AVAL_YN                       " & vbNewLine _
                                               & "     ,@PRODUCT_SEG_CD                " & vbNewLine _
                                               & "     ,@APPROVAL_CD                   " & vbNewLine _
                                               & "     ,@APPROVAL_USER                 " & vbNewLine _
                                               & "     ,@APPROVAL_DATE                 " & vbNewLine _
                                               & "     ,@APPROVAL_TIME                 " & vbNewLine _
                                               & "     ,@SYS_ENT_DATE                  " & vbNewLine _
                                               & "     ,@SYS_ENT_TIME                  " & vbNewLine _
                                               & "     ,@SYS_ENT_PGID                  " & vbNewLine _
                                               & "     ,@SYS_ENT_USER                  " & vbNewLine _
                                               & "     ,@SYS_UPD_DATE                  " & vbNewLine _
                                               & "     ,@SYS_UPD_TIME                  " & vbNewLine _
                                               & "     ,@SYS_UPD_PGID                  " & vbNewLine _
                                               & "     ,@SYS_UPD_USER                  " & vbNewLine _
                                               & "     ,@SYS_DEL_FLG                   " & vbNewLine _
                                               & "    )                                " & vbNewLine


    ''' <summary>
    ''' 単価マスタ更新登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_TANKA_M As String = "UPDATE                                             " & vbNewLine _
                                               & "    $LM_MST$..M_TANKA                              " & vbNewLine _
                                               & "SET                                                " & vbNewLine _
                                               & "      REMARK            =    @REMARK               " & vbNewLine _
                                               & "     ,STORAGE_KB1       =    @STORAGE_KB1          " & vbNewLine _
                                               & "     ,STORAGE_KB2       =    @STORAGE_KB2          " & vbNewLine _
                                               & "     ,HANDLING_IN_KB    =    @HANDLING_IN_KB       " & vbNewLine _
                                               & "     ,HANDLING_OUT_KB   =    @HANDLING_OUT_KB      " & vbNewLine _
                                               & "     ,STORAGE_1         =    @STORAGE_1            " & vbNewLine _
                                               & "     ,STORAGE_2         =    @STORAGE_2            " & vbNewLine _
                                               & "     ,HANDLING_IN       =    @HANDLING_IN          " & vbNewLine _
                                               & "     ,MINI_TEKI_IN_AMO  =    @MINI_TEKI_IN_AMO     " & vbNewLine _
                                               & "     ,HANDLING_OUT      =    @HANDLING_OUT         " & vbNewLine _
                                               & "     ,MINI_TEKI_OUT_AMO =    @MINI_TEKI_OUT_AMO    " & vbNewLine _
                                               & "     ,KIWARI_KB         =    @KIWARI_KB            " & vbNewLine _
                                               & "     ,AVAL_YN           =    @AVAL_YN              " & vbNewLine _
                                               & "     ,PRODUCT_SEG_CD    =    @PRODUCT_SEG_CD       " & vbNewLine _
                                               & "     ,APPROVAL_CD       =    @APPROVAL_CD          " & vbNewLine _
                                               & "     ,APPROVAL_USER     =    @APPROVAL_USER        " & vbNewLine _
                                               & "     ,APPROVAL_DATE     =    @APPROVAL_DATE        " & vbNewLine _
                                               & "     ,APPROVAL_TIME     =    @APPROVAL_TIME        " & vbNewLine _
                                               & "     ,SYS_UPD_DATE      =    @SYS_UPD_DATE         " & vbNewLine _
                                               & "     ,SYS_UPD_TIME      =    @SYS_UPD_TIME         " & vbNewLine _
                                               & "     ,SYS_UPD_PGID      =    @SYS_UPD_PGID         " & vbNewLine _
                                               & "     ,SYS_UPD_USER      =    @SYS_UPD_USER         " & vbNewLine _
                                               & "WHERE                                              " & vbNewLine _
                                               & "      NRS_BR_CD         =    @NRS_BR_CD            " & vbNewLine _
                                               & "AND   CUST_CD_L         =    @CUST_CD_L            " & vbNewLine _
                                               & "AND   CUST_CD_M         =    @CUST_CD_M            " & vbNewLine _
                                               & "AND   UP_GP_CD_1        =    @UP_GP_CD_1           " & vbNewLine _
                                               & "AND   STR_DATE          =    @STR_DATE             " & vbNewLine _
                                               & "AND   SYS_UPD_DATE      =    @HAITA_DATE           " & vbNewLine _
                                               & "AND   SYS_UPD_TIME      =    @HAITA_TIME           " & vbNewLine

#End Region

#Region "承認処理 SQL"

    ''' <summary>
    ''' 単価マスタ更新SQL(承認)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_APPROVAL_TANKA_M As String _
            = "UPDATE                                            " & vbNewLine _
            & "    $LM_MST$..M_TANKA                             " & vbNewLine _
            & "SET                                               " & vbNewLine _
            & "      APPROVAL_CD         =    @APPROVAL_CD       " & vbNewLine _
            & "     ,APPROVAL_USER       =    @APPROVAL_USER     " & vbNewLine _
            & "     ,APPROVAL_DATE       =    @APPROVAL_DATE     " & vbNewLine _
            & "     ,APPROVAL_TIME       =    @APPROVAL_TIME     " & vbNewLine _
            & "     ,SYS_UPD_DATE        =    @SYS_UPD_DATE      " & vbNewLine _
            & "     ,SYS_UPD_TIME        =    @SYS_UPD_TIME      " & vbNewLine _
            & "     ,SYS_UPD_PGID        =    @SYS_UPD_PGID      " & vbNewLine _
            & "     ,SYS_UPD_USER        =    @SYS_UPD_USER      " & vbNewLine _
            & "WHERE                                             " & vbNewLine _
            & "      NRS_BR_CD           =    @NRS_BR_CD         " & vbNewLine _
            & "AND   CUST_CD_L           =    @CUST_CD_L         " & vbNewLine _
            & "AND   CUST_CD_M           =    @CUST_CD_M         " & vbNewLine _
            & "AND   UP_GP_CD_1          =    @UP_GP_CD_1        " & vbNewLine _
            & "AND   STR_DATE            =    @STR_DATE          " & vbNewLine _
            & "AND   SYS_UPD_DATE        =    @HAITA_DATE        " & vbNewLine _
            & "AND   SYS_UPD_TIME        =    @HAITA_TIME        " & vbNewLine

#End Region

#Region "ComboBox SQL"

    ''' <summary>
    ''' 製品セグメント取得用
    ''' </summary>
    ''' <remarks>コンボボックスの選択肢として利用</remarks>
    Private Const SQL_SELECT_COMBO_SEIHIN As String _
            = " SELECT                                          " & vbNewLine _
            & "    CNCT_SEG_CD AS SEG_CD                        " & vbNewLine _
            & "   ,CONCAT(SGMT_L_NM,'：',SGMT_M_NM) AS SEG_NM   " & vbNewLine _
            & " FROM                                            " & vbNewLine _
            & "   ABM_DB..M_SEGMENT                             " & vbNewLine _
            & " WHERE                                           " & vbNewLine _
            & "   DATA_TYPE_CD = '00002'                        " & vbNewLine _
            & "   AND KBN_LANG = @KBN_LANG                      " & vbNewLine _
            & "   AND SYS_DEL_FLG = '0'                         " & vbNewLine _
            & " ORDER BY                                        " & vbNewLine _
            & "     CNCT_SEG_CD                                 " & vbNewLine

#End Region

#End Region

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

#End Region

#Region "Method"

#Region "編集処理"

    ''' <summary>
    ''' 単価マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>単価マスタ検索結果取得SQLの構築・発行</remarks>
    Private Function HaitaChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM120IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM120DAC.SQL_HAITA_CHK)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaitaChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM120DAC", "HaitaChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        'エラーメッセージの設定
        If MyBase.GetResultCount() < 1 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(単価マスタ排他チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaitaChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '単価マスタ主キー
        Call Me.SetParamPrimaryKeyTankaM()

        '排他項目
        Call Me.SetParamHaita()

    End Sub

#End Region

#Region "削除/復活処理"

    ''' <summary>
    ''' 単価マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>単価マスタ更新SQLの構築・発行</remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM120IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM120DAC.SQL_UPD_DEL_TANKA_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                     , Me._Row.Item("USER_BR_CD").ToString())
                                      )

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdDelData()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM120DAC", "DeleteData", cmd)

        '処理件数の設定
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        'エラーメッセージの設定
        If MyBase.GetResultCount() < 1 Then
            MyBase.SetMessage("E011")
        End If

        cmd = New SqlCommand()

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(削除復活処理共通)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdDelData()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '削除フラグ
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))
        End With

        '単価マスタ主キー
        Call Me.SetParamPrimaryKeyTankaM()

        '更新時共通項目
        Call Me.SetParamCommonSystemUpd()

        '排他項目
        Call Me.SetParamHaita()

    End Sub

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 単価マスタ検索処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>単価マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM120IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM120DAC.SQL_SELECT_COUNT_FROM)       'SQL構築(カウント用FROM句)
        Me._StrSql.Append(LMM120DAC.SQL_SELECT_DATA_FROM)        'SQL構築(カウント用FROM句)
        Call Me.SetConditionMasterSQL()                   '条件設定
        '作成したSQL(FROM句)を一時退避
        Dim intSql As Integer = Me._StrSql.Length - 1
        Dim sql1 As StringBuilder = New StringBuilder()
        For i As Integer = 0 To intSql
            sql1.Append(Me._StrSql(i))
        Next

        'SQL再設定
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMM120DAC.SQL_SELECT_COUNT_SELECT)    'SQL構築(カウント用SELECT句)
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(sql1)                                 'SQL構築(カウント用FROM句)
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" ) MAIN")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM120DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 単価マスタ検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>単価マスタ検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectDataTankaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM120IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM120DAC.SQL_SELECT_DATA_SELECT)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM120DAC.SQL_SELECT_DATA_FROM)        'SQL構築(データ抽出用FROM句)
        Call Me.SetConditionMasterSQL()                          '条件設定
        Me._StrSql.Append(LMM120DAC.SQL_ORDER_BY)                'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM120DAC", "SelectDataTankaM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("UP_GP_CD_1", "UP_GP_CD_1")
        map.Add("STR_DATE", "STR_DATE")
        map.Add("REC_NO", "REC_NO")
        map.Add("REMARK", "REMARK")
        map.Add("STORAGE_KB1", "STORAGE_KB1")
        map.Add("STORAGE_KB2", "STORAGE_KB2")
        map.Add("HANDLING_IN_KB", "HANDLING_IN_KB")
        map.Add("HANDLING_OUT_KB", "HANDLING_OUT_KB")
        map.Add("STORAGE_1", "STORAGE_1")
        map.Add("STORAGE_2", "STORAGE_2")
        map.Add("HANDLING_IN", "HANDLING_IN")
        map.Add("MINI_TEKI_IN_AMO", "MINI_TEKI_IN_AMO")
        map.Add("HANDLING_OUT", "HANDLING_OUT")
        map.Add("MINI_TEKI_OUT_AMO", "MINI_TEKI_OUT_AMO")
        map.Add("KIWARI_KB", "KIWARI_KB")
        map.Add("ITEM_CURR_CD", "ITEM_CURR_CD")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")
        map.Add("AVAL_YN", "AVAL_YN")           'ADD 2019/04/18 依頼番号 : 004862
        map.Add("AVAL_YN_NM", "AVAL_YN_NM")     'ADD 2019/04/18 依頼番号 : 004862
        map.Add("PRODUCT_SEG_CD", "PRODUCT_SEG_CD")
        map.Add("PRODUCT_SEG_NM_L", "PRODUCT_SEG_NM_L")
        map.Add("PRODUCT_SEG_NM_M", "PRODUCT_SEG_NM_M")
        map.Add("APPROVAL_CD", "APPROVAL_CD")
        map.Add("APPROVAL_NM", "APPROVAL_NM")
        map.Add("APPROVAL_USER", "APPROVAL_USER")
        map.Add("APPROVAL_DATE", "APPROVAL_DATE")
        map.Add("APPROVAL_TIME", "APPROVAL_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM120OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()

        With Me._Row

            '【営業所コード：=】
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  TNK.NRS_BR_CD = @NRS_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '【荷主コード(大)：LIKE 値%】
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  TNK.CUST_CD_L LIKE @CUST_CD_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '【荷主名(大)：LIKE %値%】
            whereStr = .Item("CUST_NM_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  CST.CUST_NM_L LIKE @CUST_NM_L")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【荷主コード(中)：LIKE 値%】
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  TNK.CUST_CD_M LIKE @CUST_CD_M")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '【荷主名(中)：LIKE %値%】
            whereStr = .Item("CUST_NM_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  CST.CUST_NM_M LIKE @CUST_NM_M")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_M", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【単価マスタコード：LIKE 値%】
            whereStr = .Item("UP_GP_CD_1").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  TNK.UP_GP_CD_1 LIKE @UP_GP_CD_1")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UP_GP_CD_1", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If
#If True Then       'ADD 2019/04/18 'ADD 2019/04/18 依頼番号 : 004862
            '【使用可能】
            whereStr = .Item("AVAL_YN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                If whereStr = "01" Then
                    '可の場合
                    andstr.Append("  TNK.AVAL_YN  in ('01','')")
                    andstr.Append(vbNewLine)

                Else
                    andstr.Append("  TNK.AVAL_YN = @AVAL_YN")
                    andstr.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AVAL_YN", whereStr, DBDataType.CHAR))

                End If
            End If

#End If
            '【承認状況】
            whereStr = .Item("APPROVAL_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  TNK.APPROVAL_CD = @APPROVAL_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APPROVAL_CD", whereStr, DBDataType.CHAR))
            End If
            '【期割区分】
            whereStr = .Item("KIWARI_KB_COND").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append(String.Concat("  TNK.KIWARI_KB", whereStr))
                andstr.Append(vbNewLine)
            End If

            '【削除フラグ：=】
            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  TNK.SYS_DEL_FLG = @SYS_DEL_FLG")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            '【ログイン言語】
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_LANG", .Item("KBN_LANG").ToString(), DBDataType.CHAR))

            'Where句付加前のSQL保持
            Dim intSql As Integer = Me._StrSql.Length - 1
            Dim sql1 As StringBuilder = New StringBuilder()
            For i As Integer = 0 To intSql
                sql1.Append(Me._StrSql(i))
            Next

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If

            '【全データ表示チェックボックスによる切り分け】
            whereStr = .Item("ALL_DATA_FLG").ToString()
            If whereStr.Equals(LMConst.FLG.OFF) Then
                If andstr.Length <> 0 Then
                    Me._StrSql.Append("AND")
                Else
                    Me._StrSql.Append("WHERE")
                End If
                Me._StrSql.Append("  TNK.STR_DATE > @SYS_DATE")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append("  UNION ALL ")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(sql1)
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(LMM120DAC.SQL_SELECT_NO_ALL_DATA_JOIN1)
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append("WHERE")
                If andstr.Length <> 0 Then
                    Me._StrSql.Append(andstr)
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                End If

                Me._StrSql.Append("  TNK.STR_DATE <= @SYS_DATE")
                Me._StrSql.Append(vbNewLine)
                Me._StrSql.Append(LMM120DAC.SQL_SELECT_NO_ALL_DATA_JOIN2)
                Me._StrSql.Append("WHERE")
                If andstr.Length <> 0 Then
                    Me._StrSql.Append(andstr)
                    Me._StrSql.Append(vbNewLine)
                    Me._StrSql.Append("AND")
                End If
                Me._StrSql.Append("  TNK2.NRS_BR_CD IS NOT NULL")
                Me._StrSql.Append(vbNewLine)

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", Me.GetSystemDate(), DBDataType.CHAR))

            End If

        End With

    End Sub

#End Region

#Region "保存処理"

    ''' <summary>
    ''' 単価マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>単価マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function ExistTankaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM120IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM120DAC.SQL_REPEAT_TANKA)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )
        'SQLパラメータ初期化/設定
        Call Me.SetParamTankaMChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM120DAC", "ExistTankaM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
        If Convert.ToInt32(reader("SELECT_CNT")) > 0 Then
            MyBase.SetMessage("E010")
        End If

        reader.Close()

        Return ds

    End Function

#If True Then   'ADD 2020/12/23 017521　【LMS】単価マスタエラー通知仕様追加
    ''' <summary>
    ''' 単価マスタ単価マスタコード存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>単価マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function CheckTankaM_UP_GP_CD_1(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM120IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM120DAC.CheckTankaM_UP_GP_CD)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )
        'SQLパラメータ初期化/設定
        Call Me.SetParamTankaMChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM120DAC", "CheckTankaM_UP_GP_CD_1", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
        If Convert.ToInt32(reader("SELECT_CNT")) > 0 Then
            'MyBase.SetMessage("W134", New String() {"単価マスタコード"})
            MyBase.SetMessage("E010")
            Return ds
        End If

        reader.Close()

        Return ds

    End Function
#End If

    ''' <summary>
    ''' 適用開始日チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>単価マスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function ChkStartDate(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM120IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM120DAC.SQL_CHK_START_DATE)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )
        'SQLパラメータ初期化/設定
        Call Me.SetParamStartDateChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM120DAC", "ChkStartDate", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("STR_DATE", "STR_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM120OUT")

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(単価マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamTankaMChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '単価マスタ主キー設定
        Call Me.SetParamPrimaryKeyTankaM()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(適用開始日チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamStartDateChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UP_GP_CD_1", .Item("UP_GP_CD_1").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#Region "新規登録/更新"

    ''' <summary>
    ''' 単価マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>単価マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM120IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM120DAC.SQL_INSERT_TANKA_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsertTankaM()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM120DAC", "InsertData", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 単価マスタ更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>単価マスタ更新登録SQLの構築・発行</remarks>
    Private Function UpdateSaveData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM120IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM120DAC.SQL_UPDATE_TANKA_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString())
                                                                        )

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdateTankaM()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM120DAC", "UpdateSaveData", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))
        If MyBase.GetResultCount < 1 Then
            MyBase.SetMessage("E011")
        End If

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(単価マスタ新規登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertTankaM()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '単価マスタ全項目
        Call Me.SetParamTankaM()

        '新規登録共通項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(単価マスタ更新用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdateTankaM()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '単価マスタ全項目
        Call Me.SetParamTankaM()

        '排他項目
        Call Me.SetParamHaita()

        '更新時共通項目
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(単価マスタ全項目)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamTankaM()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UP_GP_CD_1", .Item("UP_GP_CD_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STR_DATE", .Item("STR_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE_KB1", .Item("STORAGE_KB1").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE_KB2", .Item("STORAGE_KB2").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_IN_KB", .Item("HANDLING_IN_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_OUT_KB", .Item("HANDLING_OUT_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE_1", .Item("STORAGE_1").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STORAGE_2", .Item("STORAGE_2").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_IN", .Item("HANDLING_IN").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MINI_TEKI_IN_AMO", .Item("MINI_TEKI_IN_AMO").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HANDLING_OUT", .Item("HANDLING_OUT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MINI_TEKI_OUT_AMO", .Item("MINI_TEKI_OUT_AMO").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KIWARI_KB", .Item("KIWARI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AVAL_YN", .Item("AVAL_YN").ToString(), DBDataType.CHAR))        'ADD 2019/04/18 依頼番号 : 004862
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRODUCT_SEG_CD", .Item("PRODUCT_SEG_CD").ToString(), DBDataType.CHAR))
            '承認項目は未に戻す
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APPROVAL_CD", "00", DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APPROVAL_USER", String.Empty, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APPROVAL_DATE", String.Empty, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APPROVAL_TIME", String.Empty, DBDataType.CHAR))


        End With

    End Sub

#End Region

#End Region

#Region "承認処理"

    ''' <summary>
    ''' 単価マスタ更新（承認）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>単価マスタ更新SQLの構築・発行</remarks>
    Private Function ApprovalData(ByVal ds As DataSet) As DataSet

        Dim allCnt As Integer = 0

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM120IN")

        'データ数分繰り返し
        For i As Integer = 0 To inTbl.Rows.Count - 1

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQL構築
            Me._StrSql.Append(LMM120DAC.SQL_UPD_APPROVAL_TANKA_M)

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

            'SQLパラメータの設定
            Me._SqlPrmList = New ArrayList()
            With Me._Row
                .Item("APPROVAL_DATE") = If(String.IsNullOrEmpty(.Item("APPROVAL_DATE").ToString()), String.Empty, Me.GetSystemDate())
                .Item("APPROVAL_TIME") = If(String.IsNullOrEmpty(.Item("APPROVAL_TIME").ToString()), String.Empty, Left(Me.GetSystemTime(), 6))

                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APPROVAL_CD", .Item("APPROVAL_CD").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APPROVAL_USER", .Item("APPROVAL_USER").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APPROVAL_DATE", .Item("APPROVAL_DATE").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@APPROVAL_TIME", .Item("APPROVAL_TIME").ToString(), DBDataType.CHAR))
            End With
            Call Me.SetParamPrimaryKeyTankaM()
            Call Me.SetParamCommonSystemUpd()
            Call Me.SetParamHaita()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM120DAC", "ApprovalData", cmd)

            'SQL実行
            Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

            'エラーメッセージの設定
            If cnt < 1 Then
                MyBase.SetMessage("E011")
                Exit For
            End If

            '全処理件数のカウントアップ
            allCnt += cnt

            cmd = New SqlCommand()

        Next

        '処理件数の設定
        MyBase.SetResultCount(allCnt)

        Return ds

    End Function

#End Region

#Region "ComboBox"

    ''' <summary>
    ''' 製品セグメント取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>コンボボックスの選択肢として利用</remarks>
    Private Function SelectComboSeihin(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM120COMBO_SEIHINA")

        'INの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(LMM120DAC.SQL_SELECT_COMBO_SEIHIN)

        ' SQLパラメータ初期化/設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_LANG", Me._Row.Item("KBN_LANG").ToString(), DBDataType.CHAR))

        ' パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM120DAC", "SelectComboSeihin", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        'レコードをクリア
        ds.Tables("LMM120COMBO_SEIHINA").Rows.Clear()

        '取得データの格納先をマッピング
        map.Add("SEG_CD", "SEG_CD")
        map.Add("SEG_NM", "SEG_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM120COMBO_SEIHINA")

        Return ds

    End Function

#End Region

#Region "共通項目"

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(新規時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", Me.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(単価マスタ主キー)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamPrimaryKeyTankaM()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UP_GP_CD_1", .Item("UP_GP_CD_1").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STR_DATE", .Item("STR_DATE").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaita()

        With Me._Row
            '排他共通項目
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="brCd">営業所</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String _
                                 , ByVal brCd As String _
                                 ) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region

#End Region

End Class
