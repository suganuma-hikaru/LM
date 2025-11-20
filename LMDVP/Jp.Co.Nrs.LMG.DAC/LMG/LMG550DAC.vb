' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG550DAC : デュポン請求鑑
'  作  成  者       :  [篠原]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const

Public Class LMG550DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 出力対象帳票パターン取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                           " & vbNewLine _
                                            & "  @NRS_BR_CD      AS NRS_BR_CD                                                " & vbNewLine _
                                            & ", @PRT_TYPE      AS PTN_ID                                                " & vbNewLine _
                                            & ", WCUST.PTN_CD   AS PTN_CD                                                " & vbNewLine _
                                            & ", WCUST.RPT_ID   AS RPT_ID                                                " & vbNewLine

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks>
    ''' 20120123追加
    '''</remarks>
    Private Const SQL_FROM_MPrt As String = "FROM                                                                    " & vbNewLine _
                                          & "	--デュポン請求GLマスタ			                                     " & vbNewLine _
                                          & "	$LM_TRN$..G_DUPONT_SEKY_GL SEKY                                      " & vbNewLine _
                                          & "	LEFT JOIN			                                                 " & vbNewLine _
                                          & "	(SELECT			                                                     " & vbNewLine _
                                          & "	    MCDB.NRS_BR_CD    AS NRS_BR_CD	                                 " & vbNewLine _
                                          & "	   ,MCDB.DEPART AS DEPART		                                     " & vbNewLine _
                                          & "	   ,CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD			     " & vbNewLine _
                                          & "		ELSE MR2.PTN_CD END                            AS PTN_CD	     " & vbNewLine _
                                          & "	   ,CASE WHEN MR1.RPT_ID IS NOT NULL THEN MR1.RPT_ID	             " & vbNewLine _
                                          & "	    ELSE MR2.RPT_ID END                            AS RPT_ID		 " & vbNewLine _
                                          & "		FROM		                                                     " & vbNewLine _
                                          & "		--荷主明細マスタ		                                         " & vbNewLine _
                                            & "  (                                                                                              " & vbNewLine _
                                            & "      SELECT                                                                                     " & vbNewLine _
                                            & "      NRS_BR_CD                                                                                  " & vbNewLine _
                                            & "      ,CUST_CD_L                                                                                 " & vbNewLine _
                                            & "      ,CUST_CD_M                                                                                 " & vbNewLine _
                                            & "      ,CUST_CD_S                                                                                 " & vbNewLine _
                                            & "      ,CUST_CD_SS                                                                                " & vbNewLine _
                                            & "      ,DEPART                                                                                    " & vbNewLine _
                                            & "      FROM (                                                                                     " & vbNewLine _
                                            & "          SELECT                                                                                 " & vbNewLine _
                                            & "              MCD.NRS_BR_CD as NRS_BR_CD                                                         " & vbNewLine _
                                            & "              ,SUBSTRING(MCD.CUST_CD,1,5) as CUST_CD_L                                           " & vbNewLine _
                                            & "              ,SUBSTRING(MCD.CUST_CD,6,2) as CUST_CD_M                                           " & vbNewLine _
                                            & "              ,SUBSTRING(MCD.CUST_CD,8,2) as CUST_CD_S                                           " & vbNewLine _
                                            & "              ,SUBSTRING(MCD.CUST_CD,10,2) as CUST_CD_SS                                         " & vbNewLine _
                                            & "              ,RIGHT('00' + MCD.SET_NAIYO,2) as DEPART                                           " & vbNewLine _
                                            & "              ,rank() over(partition by MCD.NRS_BR_CD,MCD.SET_NAIYO order by MCD.CUST_CD) rnk    " & vbNewLine _
                                            & "          FROM $LM_MST$..M_CUST_DETAILS MCD                                                        " & vbNewLine _
                                            & "          WHERE                                                                                  " & vbNewLine _
                                            & "              MCD.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
                                            & "              AND MCD.SUB_KB = '01'                                                              " & vbNewLine _
                                            & "      ) MCDB2 where MCDB2.rnk = 1                                                                " & vbNewLine _
                                            & "  ) MCDB                                                                                         " & vbNewLine _
                                          & "		--追加 MR1 MCR1		                                             " & vbNewLine _
                                          & "		--荷主帳票パターン取得                		                     " & vbNewLine _
                                          & "		LEFT JOIN $LM_MST$..M_CUST_RPT MCR1     		                     " & vbNewLine _
                                          & "		ON  MCR1.NRS_BR_CD   = MCDB.NRS_BR_CD		                     " & vbNewLine _
                                          & "		AND MCR1.CUST_CD_L   = MCDB.CUST_CD_L		         " & vbNewLine _
                                          & "		AND MCR1.CUST_CD_M   = MCDB.CUST_CD_M		         " & vbNewLine _
                                          & "	    AND MCR1.CUST_CD_S   = MCDB.CUST_CD_S			     " & vbNewLine _
                                          & "		AND MCR1.PTN_ID      = @PRT_TYPE		                                 " & vbNewLine _
                                          & "		AND MCR1.SYS_DEL_FLG = '0'		                                 " & vbNewLine _
                                          & "				                                                         " & vbNewLine _
                                          & "		--帳票パターン取得	                                             " & vbNewLine _
                                          & "		LEFT JOIN $LM_MST$..M_RPT MR1		                                 " & vbNewLine _
                                          & "		ON  MR1.NRS_BR_CD   = MCR1.NRS_BR_CD		                     " & vbNewLine _
                                          & "		AND MR1.PTN_ID      = MCR1.PTN_ID		                         " & vbNewLine _
                                          & "		AND MR1.PTN_CD      = MCR1.PTN_CD		                         " & vbNewLine _
                                          & "		AND MR1.SYS_DEL_FLG = '0'         		                         " & vbNewLine _
                                          & "	  --追加おわり			                                             " & vbNewLine _
                                          & "				                                                         " & vbNewLine _
                                          & "		--追加 取得できない場合用		                                 " & vbNewLine _
                                          & "		--存在しない場合の帳票パターン取得		                         " & vbNewLine _
                                          & "		LEFT JOIN $LM_MST$..M_RPT MR2     		                         " & vbNewLine _
                                          & "		ON  MR2.NRS_BR_CD     = @NRS_BR_CD  		                     " & vbNewLine _
                                          & "		AND MR2.PTN_ID        = @PRT_TYPE             		             " & vbNewLine _
                                          & "		AND MR2.STANDARD_FLAG = '01'      		                         " & vbNewLine _
                                          & "		AND MR2.SYS_DEL_FLG   = '0'         		                     " & vbNewLine _
                                          & "				                                                         " & vbNewLine _
                                          & "	GROUP BY		                                                     " & vbNewLine _
                                          & "		 MCDB.NRS_BR_CD		                                             " & vbNewLine _
                                          & "		,MCDB.DEPART		                                             " & vbNewLine _
                                          & "		,MR1.PTN_CD		                                                 " & vbNewLine _
                                          & "		,MR1.RPT_ID		                                                 " & vbNewLine _
                                          & "		,MR2.PTN_CD		                                                 " & vbNewLine _
                                          & "		,MR2.RPT_ID		                                                 " & vbNewLine _
                                          & "		,MR2.RPT_ID		                                                 " & vbNewLine _
                                          & "	) WCUST			                                                     " & vbNewLine _
                                          & "	ON   SEKY.NRS_BR_CD = WCUST.NRS_BR_CD			                     " & vbNewLine _
                                          & "	AND  SEKY.DEPART    = RIGHT('00' + WCUST.DEPART,2)	                 " & vbNewLine _
                                          & "     --Add2012/09/25要望管理1430         		                         " & vbNewLine _
                                          & "  --区分01                                                                       " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..Z_KBN KBN01                                                  " & vbNewLine _
                                          & "  ON  KBN01.KBN_GROUP_CD  = 'Z009'                                               " & vbNewLine _
                                          & "  AND KBN01.KBN_CD        =  SEKY.DEPART                                         " & vbNewLine _
                                          & "	LEFT JOIN $LM_MST$..Z_KBN KBN03          		                     " & vbNewLine _
                                          & "	ON  KBN03.KBN_GROUP_CD  = 'D016'         		                 " & vbNewLine _
                                          & "	AND KBN03.KBN_CD        =  KBN01.KBN_NM3         		         " & vbNewLine

    '00用
    Private Const SQL_SELECT_DATA_00 As String = "SELECT                                                                      " & vbNewLine _
                                          & "  BASE.RPT_ID               AS RPT_ID,                                           " & vbNewLine _
                                          & "  BASE.NRS_BR_CD            AS NRS_BR_CD,                                        " & vbNewLine _
                                          & "  BASE.SEKY_YM              AS SEKY_YM,                                          " & vbNewLine _
                                          & "  BASE.SEKY_M_ENG          AS SEKY_M_ENG,                                       " & vbNewLine _
                                          & "  BASE.DEPART               AS DEPART,                                           " & vbNewLine _
                                          & "  BASE.DEPART_NM              AS DEPART_NM,                                        " & vbNewLine _
                                          & "  BASE.DEPART_NM_JP         AS DEPART_NM_JP,                                     " & vbNewLine _
                                          & "  BASE.CUST_NM_S            AS CUST_NM_S,                                        " & vbNewLine _
                                          & "  BASE.CUST_NM_SS           AS CUST_NM_SS,                                       " & vbNewLine _
                                          & "  BASE.MISK_CD              AS MISK_CD,                                          " & vbNewLine _
                                          & "  BASE.SRC_CD               AS SRC_CD,                                           " & vbNewLine _
                                          & "  BASE.SRC_NO               AS SRC_NO,                                           " & vbNewLine _
                                          & "  BASE.PLANT_CD             AS PLANT_CD,                                         " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.STORAGE),'0')              AS STORAGE,                                          " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.HANDLING),'0')             AS HANDLING,                                         " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.TRUCKAGE),'0')             AS TRUCKAGE,                                         " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.CLEARANCE),'0')            AS CLEARANCE,                                        " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.EXPORT),'0')               AS EXPORT,                                           " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.FPDE_OTHER),'0')           AS FPDE_OTHER,                                       " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.EXPENSE),'0')              AS EXPENSE,                                          " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.TAX),'0')                  AS TAX,                                              " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.TOTAL),'0')                AS TOTAL,                                            " & vbNewLine _
                                          & "  '0'                       AS SOUND_STORAGE,                                    " & vbNewLine _
                                          & "  '0'                       AS SOUND_HANDLING,                                   " & vbNewLine _
                                          & "  '0'                       AS SOUND_TRUCKAGE,                                   " & vbNewLine _
                                          & "  '0'                       AS SOUND_CLEARANCE,                                  " & vbNewLine _
                                          & "  '0'                       AS SOUND_EXPORT,                                     " & vbNewLine _
                                          & "  '0'                       AS SOUND_FPDE_OTHER,                                 " & vbNewLine _
                                          & "  '0'                       AS SOUND_EXPENSE,                                    " & vbNewLine _
                                          & "  '0'                       AS SOUND_TAX,                                        " & vbNewLine _
                                          & "  '0'                       AS SOUND_TOTAL,                                      " & vbNewLine _
                                          & "  '0'                       AS BOND_STORAGE,                                     " & vbNewLine _
                                          & "  '0'                       AS BOND_HANDLING,                                    " & vbNewLine _
                                          & "  '0'                       AS BOND_TRUCKAGE,                                    " & vbNewLine _
                                          & "  '0'                       AS BOND_CLEARANCE,                                   " & vbNewLine _
                                          & "  '0'                       AS BOND_EXPORT,                                      " & vbNewLine _
                                          & "  '0'                       AS BOND_FPDE_OTHER,                                  " & vbNewLine _
                                          & "  '0'                       AS BOND_EXPENSE,                                     " & vbNewLine _
                                          & "  '0'                       AS BOND_TAX,                                         " & vbNewLine _
                                          & "  '0'                       AS BOND_TOTAL,                                       " & vbNewLine _
                                          & "  BASE.GL_BR_CD             AS GL_BR_CD,                                         " & vbNewLine _
                                          & "  BASE.GL_BR_NM             AS GL_BR_NM,                                         " & vbNewLine _
                                          & "  BASE.DUPONT_SEIQTO_NM     AS DUPONT_SEIQTO_NM                                  " & vbNewLine

    '01用
    Private Const SQL_SELECT_DATA_01 As String = "SELECT                                                                         " & vbNewLine _
                                          & "  BASE.RPT_ID               AS RPT_ID,                                           " & vbNewLine _
                                          & "  BASE.NRS_BR_CD            AS NRS_BR_CD,                                        " & vbNewLine _
                                          & "  BASE.SEKY_YM              AS SEKY_YM,                                          " & vbNewLine _
                                          & "  BASE.SEKY_M_ENG           AS SEKY_M_ENG,                                       " & vbNewLine _
                                          & "  BASE.DEPART               AS DEPART,                                           " & vbNewLine _
                                          & "  BASE.DEPART_NM            AS DEPART_NM,                                        " & vbNewLine _
                                          & "  BASE.DEPART_NM_JP         AS DEPART_NM_JP,                                     " & vbNewLine _
                                          & "  'ＤＵＰＯＮＴ'            AS CUST_NM_S,                                        " & vbNewLine _
                                          & "  ''                        AS CUST_NM_SS,                                       " & vbNewLine _
                                          & "  ''                        AS MISK_CD,                                          " & vbNewLine _
                                          & "  ''                        AS SRC_CD,                                           " & vbNewLine _
                                          & "  ''                        AS SRC_NO,                                           " & vbNewLine _
                                          & "  ''                        AS PLANT_CD,                                         " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.STORAGE),'0')              AS STORAGE,                                          " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.HANDLING),'0')             AS HANDLING,                                         " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.TRUCKAGE),'0')             AS TRUCKAGE,                                         " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.CLEARANCE),'0')            AS CLEARANCE,                                        " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.EXPORT),'0')               AS EXPORT,                                           " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.FPDE_OTHER),'0')           AS FPDE_OTHER,                                       " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.EXPENSE),'0')              AS EXPENSE,                                          " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.TAX),'0')                  AS TAX,                                              " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.TOTAL),'0')                AS TOTAL,                                            " & vbNewLine _
                                          & "  '0'                       AS SOUND_STORAGE,                                    " & vbNewLine _
                                          & "  '0'                       AS SOUND_HANDLING,                                   " & vbNewLine _
                                          & "  '0'                       AS SOUND_TRUCKAGE,                                   " & vbNewLine _
                                          & "  '0'                       AS SOUND_CLEARANCE,                                  " & vbNewLine _
                                          & "  '0'                       AS SOUND_EXPORT,                                     " & vbNewLine _
                                          & "  '0'                       AS SOUND_FPDE_OTHER,                                 " & vbNewLine _
                                          & "  '0'                       AS SOUND_EXPENSE,                                    " & vbNewLine _
                                          & "  '0'                       AS SOUND_TAX,                                        " & vbNewLine _
                                          & "  '0'                       AS SOUND_TOTAL,                                      " & vbNewLine _
                                          & "  '0'                       AS BOND_STORAGE,                                     " & vbNewLine _
                                          & "  '0'                       AS BOND_HANDLING,                                    " & vbNewLine _
                                          & "  '0'                       AS BOND_TRUCKAGE,                                    " & vbNewLine _
                                          & "  '0'                       AS BOND_CLEARANCE,                                   " & vbNewLine _
                                          & "  '0'                       AS BOND_EXPORT,                                      " & vbNewLine _
                                          & "  '0'                       AS BOND_FPDE_OTHER,                                  " & vbNewLine _
                                          & "  '0'                       AS BOND_EXPENSE,                                     " & vbNewLine _
                                          & "  '0'                       AS BOND_TAX,                                         " & vbNewLine _
                                          & "  '0'                       AS BOND_TOTAL,                                       " & vbNewLine _
                                          & "  BASE.GL_BR_CD             AS GL_BR_CD,                                         " & vbNewLine _
                                          & "  BASE.GL_BR_NM             AS GL_BR_NM,                                         " & vbNewLine _
                                          & "  BASE.DUPONT_SEIQTO_NM     AS DUPONT_SEIQTO_NM                                  " & vbNewLine

    '02用
    Private Const SQL_SELECT_DATA_02 As String = "SELECT                                                                      " & vbNewLine _
                                          & "  BASE.RPT_ID               AS RPT_ID,                                           " & vbNewLine _
                                          & "  BASE.NRS_BR_CD            AS NRS_BR_CD,                                        " & vbNewLine _
                                          & "  BASE.SEKY_YM              AS SEKY_YM,                                          " & vbNewLine _
                                          & "  BASE.SEKY_M_ENG           AS SEKY_M_ENG,                                       " & vbNewLine _
                                          & "  BASE.DEPART               AS DEPART,                                           " & vbNewLine _
                                          & "  BASE.DEPART_NM            AS DEPART_NM,                                        " & vbNewLine _
                                          & "  BASE.DEPART_NM_JP         AS DEPART_NM_JP,                                     " & vbNewLine _
                                          & "  'ＤＵＰＯＮＴ'            AS CUST_NM_S,                                        " & vbNewLine _
                                          & "  ''                        AS CUST_NM_SS,                                       " & vbNewLine _
                                          & "  ''                        AS MISK_CD,                                          " & vbNewLine _
                                          & "  ''                        AS SRC_CD,                                           " & vbNewLine _
                                          & "  ''                        AS SRC_NO,                                           " & vbNewLine _
                                          & "  ''                        AS PLANT_CD,                                         " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.STORAGE),'0')              AS STORAGE,                                          " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.HANDLING),'0')             AS HANDLING,                                         " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.TRUCKAGE),'0')             AS TRUCKAGE,                                         " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.CLEARANCE),'0')            AS CLEARANCE,                                        " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.EXPORT),'0')               AS EXPORT,                                           " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.FPDE_OTHER),'0')           AS FPDE_OTHER,                                       " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.EXPENSE),'0')              AS EXPENSE,                                          " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.TAX),'0')                  AS TAX,                                              " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.TOTAL),'0')                AS TOTAL,                                            " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.SOUND_STORAGE),'0')        AS SOUND_STORAGE,                                    " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.SOUND_HANDLING),'0')       AS SOUND_HANDLING,                                   " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.SOUND_TRUCKAGE),'0')       AS SOUND_TRUCKAGE,                                   " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.SOUND_CLEARANCE),'0')      AS SOUND_CLEARANCE,                                  " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.SOUND_EXPORT),'0')         AS SOUND_EXPORT,                                     " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.SOUND_FPDE_OTHER),'0')     AS SOUND_FPDE_OTHER,                                 " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.SOUND_EXPENSE),'0')        AS SOUND_EXPENSE,                                    " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.SOUND_TAX),'0')            AS SOUND_TAX,                                        " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.SOUND_TOTAL),'0')          AS SOUND_TOTAL,                                      " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.BOND_STORAGE),'0')         AS BOND_STORAGE,                                     " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.BOND_HANDLING),'0')        AS BOND_HANDLING,                                    " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.BOND_TRUCKAGE),'0')        AS BOND_TRUCKAGE,                                    " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.BOND_CLEARANCE),'0')       AS BOND_CLEARANCE,                                   " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.BOND_EXPORT),'0')          AS BOND_EXPORT,                                      " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.BOND_FPDE_OTHER),'0')      AS BOND_FPDE_OTHER,                                  " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.BOND_EXPENSE),'0')         AS BOND_EXPENSE,                                     " & vbNewLine _
                                          & "  '0'                                        AS BOND_TAX,                                         " & vbNewLine _
                                          & "  ISNULL(SUM(BASE.BOND_TOTAL),'0')           AS BOND_TOTAL,                                       " & vbNewLine _
                                          & "  BASE.GL_BR_CD                              AS GL_BR_CD,                                         " & vbNewLine _
                                          & "  BASE.GL_BR_NM                              AS GL_BR_NM,                                         " & vbNewLine _
                                          & "  BASE.DUPONT_SEIQTO_NM                      AS DUPONT_SEIQTO_NM                                  " & vbNewLine



    ''' <summary>
    ''' 請求書鑑出力対象データ検索用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " FROM                                                                    " & vbNewLine _
                                          & "(SELECT                                                                          " & vbNewLine _
                                          & "CASE WHEN MR1.RPT_ID IS NOT NULL THEN MR1.RPT_ID	                              " & vbNewLine _
                                          & "     WHEN MR2.RPT_ID IS NOT NULL THEN MR2.RPT_ID                                 " & vbNewLine _
                                          & "     ELSE '' END                      AS RPT_ID,                                 " & vbNewLine _
                                          & "  @NRS_BR_CD            AS NRS_BR_CD,                                        " & vbNewLine _
                                          & "  SEKY.SEKY_YM              AS SEKY_YM,                                          " & vbNewLine _
                                          & "  CASE WHEN SUBSTRING (SEKY.SEKY_YM,5,2) ='01' THEN                              " & vbNewLine _
                                          & "  'Ｊ Ａ Ｎ Ｕ Ａ Ｒ Ｙ'                                                         " & vbNewLine _
                                          & "  WHEN SUBSTRING (SEKY.SEKY_YM,5,2) ='02' THEN                                   " & vbNewLine _
                                          & "  'Ｆ Ｅ Ｂ Ｒ Ｕ Ａ Ｒ Ｙ'                                                      " & vbNewLine _
                                          & "  WHEN SUBSTRING (SEKY.SEKY_YM,5,2) ='03' THEN                                   " & vbNewLine _
                                          & "  'Ｍ Ａ Ｒ Ｃ Ｈ'                                                               " & vbNewLine _
                                          & "  WHEN SUBSTRING (SEKY.SEKY_YM,5,2) ='04' THEN                                   " & vbNewLine _
                                          & "  'Ａ Ｐ Ｒ Ｉ Ｌ'                                                               " & vbNewLine _
                                          & "  WHEN SUBSTRING (SEKY.SEKY_YM,5,2) ='05' THEN                                   " & vbNewLine _
                                          & "  'Ｍ Ａ Ｙ'                                                                     " & vbNewLine _
                                          & "  WHEN SUBSTRING (SEKY.SEKY_YM,5,2) ='06' THEN                                   " & vbNewLine _
                                          & "  'Ｊ Ｕ Ｎ Ｅ'                                                                  " & vbNewLine _
                                          & "  WHEN SUBSTRING (SEKY.SEKY_YM,5,2) ='07' THEN                                   " & vbNewLine _
                                          & "  'Ｊ Ｕ Ｌ Ｙ'                                                                  " & vbNewLine _
                                          & "  WHEN SUBSTRING (SEKY.SEKY_YM,5,2) ='08' THEN                                   " & vbNewLine _
                                          & "  'Ａ Ｕ Ｇ Ｕ Ｓ Ｔ'                                                            " & vbNewLine _
                                          & "  WHEN SUBSTRING (SEKY.SEKY_YM,5,2) ='09' THEN                                   " & vbNewLine _
                                          & "  'Ｓ Ｅ Ｐ Ｔ Ｅ Ｍ Ｂ Ｅ Ｒ'                                                   " & vbNewLine _
                                          & "  WHEN SUBSTRING (SEKY.SEKY_YM,5,2) ='10' THEN                                   " & vbNewLine _
                                          & "  'Ｏ Ｃ Ｔ Ｏ Ｂ Ｅ Ｒ'                                                         " & vbNewLine _
                                          & "  WHEN SUBSTRING (SEKY.SEKY_YM,5,2) ='11' THEN                                   " & vbNewLine _
                                          & "  'Ｎ Ｏ Ｖ Ｅ Ｍ Ｂ Ｅ Ｒ'                                                      " & vbNewLine _
                                          & "  WHEN SUBSTRING (SEKY.SEKY_YM,5,2) ='12' THEN                                   " & vbNewLine _
                                          & "  'Ｄ Ｅ Ｃ Ｅ Ｍ Ｂ Ｅ Ｒ'                                                      " & vbNewLine _
                                          & "  ELSE '' END               AS SEKY_M_ENG,                                       " & vbNewLine _
                                          & "  SEKY.DEPART               AS DEPART,                                           " & vbNewLine _
                                          & "  KBN01.KBN_NM1             AS DEPART_NM,                                        " & vbNewLine _
                                          & "  KBN01.KBN_NM2             AS DEPART_NM_JP,                                        " & vbNewLine _
                                          & "  MC.CUST_NM_S              AS CUST_NM_S,                                        " & vbNewLine _
                                          & "  MC.CUST_NM_SS             AS CUST_NM_SS,                                       " & vbNewLine _
                                          & "  SEKY.MISK_CD              AS MISK_CD,                                          " & vbNewLine _
                                          & "  SEKY.SRC_CD               AS SRC_CD,                                           " & vbNewLine _
                                          & "  KBN02.KBN_NM2             AS SRC_NO,                                           " & vbNewLine _
                                          & "  CASE WHEN SEKY.MISK_CD <> '' THEN                                              " & vbNewLine _
                                          & "  SEKY.COST_CENTER                                                               " & vbNewLine _
                                          & "  WHEN SEKY.MISK_CD       = '' THEN                                              " & vbNewLine _
                                          & "  SEKY.FRB_CD                                                                    " & vbNewLine _
                                          & "  ELSE '' END               AS PLANT_CD,                                         " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK = '01' THEN                                            " & vbNewLine _
                                          & "  SUM(AMOUNT) END  AS STORAGE,                                                   " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK = '02' THEN                                            " & vbNewLine _
                                          & "  SUM(AMOUNT) END  AS HANDLING,                                                  " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK = '03' THEN                                            " & vbNewLine _
                                          & "  SUM(AMOUNT) END  AS TRUCKAGE,                                                  " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK = '00' THEN                                            " & vbNewLine _
                                          & "  SUM(AMOUNT) END AS CLEARANCE,                                                  " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK = '04' THEN                                            " & vbNewLine _
                                          & "  SUM(AMOUNT) END AS EXPORT,                                                     " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK = '05' THEN                                            " & vbNewLine _
                                          & "  SUM(AMOUNT) END AS FPDE_OTHER,                                                 " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK >= '06' THEN                                           " & vbNewLine _
                                          & "  SUM(AMOUNT) END  AS EXPENSE,                                                   " & vbNewLine _
                                          & "  SUM(VAT_AMOUNT)  AS TAX,                                                       " & vbNewLine _
                                          & "  SUM(AMOUNT + VAT_AMOUNT)  AS TOTAL,                                            " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK = '01' THEN                                            " & vbNewLine _
                                          & "  SUM(SOUND) END  AS SOUND_STORAGE,                                              " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK = '02' THEN                                            " & vbNewLine _
                                          & "  SUM(SOUND) END  AS SOUND_HANDLING,                                             " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK = '03' THEN                                            " & vbNewLine _
                                          & "  SUM(SOUND) END  AS SOUND_TRUCKAGE,                                             " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK = '00' THEN                                            " & vbNewLine _
                                          & "  SUM(SOUND) END AS SOUND_CLEARANCE,                                             " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK = '04' THEN                                            " & vbNewLine _
                                          & "  SUM(SOUND) END AS SOUND_EXPORT,                                                " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK = '05' THEN                                            " & vbNewLine _
                                          & "  SUM(SOUND) END AS SOUND_FPDE_OTHER,                                            " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK >= '06' THEN                                           " & vbNewLine _
                                          & "  SUM(SOUND) END  AS SOUND_EXPENSE,                                              " & vbNewLine _
                                          & "  SUM(VAT_AMOUNT) AS SOUND_TAX,                                                  " & vbNewLine _
                                          & "  SUM(SOUND + VAT_AMOUNT)  AS SOUND_TOTAL,                                       " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK = '01' THEN                                            " & vbNewLine _
                                          & "  SUM(BOND) END  AS BOND_STORAGE,                                                " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK = '02' THEN                                            " & vbNewLine _
                                          & "  SUM(BOND) END  AS BOND_HANDLING,                                               " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK = '03' THEN                                            " & vbNewLine _
                                          & "  SUM(BOND) END  AS BOND_TRUCKAGE,                                               " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK = '00' THEN                                            " & vbNewLine _
                                          & "  SUM(BOND) END AS BOND_CLEARANCE,                                               " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK = '04' THEN                                            " & vbNewLine _
                                          & "  SUM(BOND) END AS BOND_EXPORT,                                                  " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK = '05' THEN                                            " & vbNewLine _
                                          & "  SUM(BOND) END AS BOND_FPDE_OTHER,                                              " & vbNewLine _
                                          & "  CASE WHEN SEKY.SEKY_KMK >= '06' THEN                                           " & vbNewLine _
                                          & "  SUM(BOND) END  AS BOND_EXPENSE,                                                " & vbNewLine _
                                          & "  SUM(BOND)  AS BOND_TOTAL                                                       " & vbNewLine

    Private Const SQL_SELECT_DATA_GLBRCD_1 As String = "  ,SEKY.NRS_BR_CD AS GL_BR_CD                                         " & vbNewLine _
                                                     & "  ,MB.NRS_BR_NM   AS GL_BR_NM                                         " & vbNewLine

    Private Const SQL_SELECT_DATA_GLBRCD_2 As String = "  ,''             AS GL_BR_CD                                         " & vbNewLine _
                                                     & "  ,''             AS GL_BR_NM                                         " & vbNewLine

    Private Const SQL_SELECT_DATA_SEIQ_NM_1 As String = "  ,KBN03.KBN_NM2  AS DUPONT_SEIQTO_NM                                " & vbNewLine


    ''' <summary>
    ''' 請求書鑑出力対象データ検索用
    ''' </summary>
    ''' <remarks>
    ''' 20120123追加
    '''</remarks>
    Private Const SQL_FROM_DATA As String = "  FROM                                                                         " & vbNewLine _
                                            & "  $LM_TRN$..G_DUPONT_SEKY_GL AS SEKY                                               " & vbNewLine _
                                            & "  --荷主明細                                                                     " & vbNewLine _
                                            & "  LEFT JOIN                                                                                      " & vbNewLine _
                                            & "  (                                                                                              " & vbNewLine _
                                            & "      SELECT                                                                                     " & vbNewLine _
                                            & "      NRS_BR_CD                                                                                  " & vbNewLine _
                                            & "      ,CUST_CD_L                                                                                 " & vbNewLine _
                                            & "      ,CUST_CD_M                                                                                 " & vbNewLine _
                                            & "      ,CUST_CD_S                                                                                 " & vbNewLine _
                                            & "      ,CUST_CD_SS                                                                                " & vbNewLine _
                                            & "      ,DEPART                                                                                    " & vbNewLine _
                                            & "      FROM (                                                                                     " & vbNewLine _
                                            & "          SELECT                                                                                 " & vbNewLine _
                                            & "              MCD.NRS_BR_CD as NRS_BR_CD                                                         " & vbNewLine _
                                            & "              ,SUBSTRING(MCD.CUST_CD,1,5) as CUST_CD_L                                           " & vbNewLine _
                                            & "              ,SUBSTRING(MCD.CUST_CD,6,2) as CUST_CD_M                                           " & vbNewLine _
                                            & "              ,SUBSTRING(MCD.CUST_CD,8,2) as CUST_CD_S                                           " & vbNewLine _
                                            & "              ,SUBSTRING(MCD.CUST_CD,10,2) as CUST_CD_SS                                         " & vbNewLine _
                                            & "              ,RIGHT('00' + MCD.SET_NAIYO,2) as DEPART                                           " & vbNewLine _
                                            & "              ,rank() over(partition by MCD.NRS_BR_CD,MCD.SET_NAIYO order by MCD.CUST_CD) rnk    " & vbNewLine _
                                            & "          FROM $LM_MST$..M_CUST_DETAILS MCD                                                        " & vbNewLine _
                                            & "          WHERE                                                                                  " & vbNewLine _
                                            & "              MCD.SYS_DEL_FLG = '0'                                                              " & vbNewLine _
                                            & "              AND MCD.SUB_KB = '01'                                                              " & vbNewLine _
                                            & "      ) MCDB2 where MCDB2.rnk = 1                                                                " & vbNewLine _
                                            & "  ) MCDB                                                                                         " & vbNewLine _
                                            & "  ON  SEKY.NRS_BR_CD = MCDB.NRS_BR_CD                                                            " & vbNewLine _
                                            & "  AND SEKY.DEPART  = MCDB. DEPART                                                                " & vbNewLine _
                                            & "  --営業所                                                                       " & vbNewLine _
                                            & "  LEFT JOIN $LM_MST$..M_NRS_BR MB                                                  " & vbNewLine _
                                            & "  ON  SEKY.NRS_BR_CD = MB.NRS_BR_CD                                               " & vbNewLine _
                                            & "  --荷主                                                                         " & vbNewLine _
                                            & "  LEFT JOIN $LM_MST$..M_CUST MC                                                    " & vbNewLine _
                                            & "  ON  MC.NRS_BR_CD = MCDB.NRS_BR_CD                                               " & vbNewLine _
                                            & "  AND MC.CUST_CD_L  = MCDB.CUST_CD_L                                 " & vbNewLine _
                                            & "  AND MC.CUST_CD_M  = MCDB.CUST_CD_M                                 " & vbNewLine _
                                            & "  AND MC.CUST_CD_S  = MCDB.CUST_CD_S                                 " & vbNewLine _
                                            & "  AND MC.CUST_CD_SS = MCDB.CUST_CD_SS                                " & vbNewLine _
                                            & "  --区分01                                                                       " & vbNewLine _
                                            & "  LEFT JOIN $LM_MST$..Z_KBN KBN01                                                  " & vbNewLine _
                                            & "  ON  KBN01.KBN_GROUP_CD  = 'Z009'                                               " & vbNewLine _
                                            & "  AND KBN01.KBN_CD        =  SEKY.DEPART                                         " & vbNewLine _
                                            & "  --区分02                                                                       " & vbNewLine _
                                            & "  LEFT JOIN $LM_MST$..Z_KBN KBN02                                                  " & vbNewLine _
                                            & "  ON  KBN02.KBN_GROUP_CD  = 'S028'                                               " & vbNewLine _
                                            & "  AND KBN02.KBN_CD        =  SEKY.SRC_CD                                         " & vbNewLine _
                                            & "--追加 MR1 MCR1		                                                            " & vbNewLine _
                                            & "--荷主帳票パターン取得                		                                    " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1     		                                    " & vbNewLine _
                                            & "ON  MCR1.NRS_BR_CD   = MCDB.NRS_BR_CD		                                        " & vbNewLine _
                                            & "AND MCR1.CUST_CD_L   = MCDB.CUST_CD_L		                        " & vbNewLine _
                                            & "AND MCR1.CUST_CD_M   = MCDB.CUST_CD_M		                        " & vbNewLine _
                                            & "  AND MCR1.CUST_CD_S = MCDB.CUST_CD_S	                            " & vbNewLine _
                                            & "AND MCR1.PTN_ID      = @PRT_TYPE		                                            " & vbNewLine _
                                            & "AND MCR1.SYS_DEL_FLG = '0'		                                                " & vbNewLine _
                                            & "		                                                                            " & vbNewLine _
                                            & "--帳票パターン取得	                                                            " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR1		                                                " & vbNewLine _
                                            & "ON  MR1.NRS_BR_CD   = @NRS_BR_CD		                                        " & vbNewLine _
                                            & "AND MR1.PTN_ID      = MCR1.PTN_ID		                                        " & vbNewLine _
                                            & "AND MR1.PTN_CD      = MCR1.PTN_CD		                                        " & vbNewLine _
                                            & "AND MR1.SYS_DEL_FLG = '0'         		                                        " & vbNewLine _
                                            & "--追加おわり			                                                            " & vbNewLine _
                                            & "--追加 取得できない場合用		                                                " & vbNewLine _
                                            & "--存在しない場合の帳票パターン取得		                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_RPT MR2     		                                            " & vbNewLine _
                                            & "ON  MR2.NRS_BR_CD     = SEKY.NRS_BR_CD  		                                    " & vbNewLine _
                                            & "AND MR2.PTN_ID        = @PRT_TYPE             		                            " & vbNewLine _
                                            & "AND MR2.STANDARD_FLAG = '01'      		                                        " & vbNewLine _
                                            & "AND MR2.SYS_DEL_FLG   = '0'         		                                        " & vbNewLine _
                                            & "--Add2012/09/25要望管理1430         		                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..Z_KBN KBN03          		                                " & vbNewLine _
                                            & "ON  KBN03.KBN_GROUP_CD  = 'D016'         		                                " & vbNewLine _
                                            & "AND KBN03.KBN_CD        =  KBN01.KBN_NM3         		                        " & vbNewLine

    Private Const SQL_WHERE_DATA As String = "  WHERE                                                                          " & vbNewLine _
                                            & "  SEKY.SYS_DEL_FLG = '0'                                                      " & vbNewLine

    Private Const SQL_GROUP_DATA As String = "  GROUP BY                                                                       " & vbNewLine _
                                            & "   MR1.RPT_ID                                                                    " & vbNewLine _
                                            & "  ,MR2.RPT_ID                                                                    " & vbNewLine _
                                            & "  ,SEKY.NRS_BR_CD                                                                 " & vbNewLine _
                                            & "  ,SEKY.SEKY_YM                                                                  " & vbNewLine _
                                            & "  ,SEKY.DEPART                                                                   " & vbNewLine _
                                            & "  ,KBN01.KBN_NM1                                                                 " & vbNewLine _
                                            & "  ,KBN01.KBN_NM2                                                                 " & vbNewLine _
                                            & "  ,MC.CUST_NM_S                                                                  " & vbNewLine _
                                            & "  ,MC.CUST_NM_SS                                                                 " & vbNewLine _
                                            & "  ,SEKY.MISK_CD                                                                  " & vbNewLine _
                                            & "  ,SEKY.SRC_CD                                                                   " & vbNewLine _
                                            & "  ,KBN02.KBN_NM2                                                                  " & vbNewLine _
                                            & "  ,SEKY.COST_CENTER                                                              " & vbNewLine _
                                            & "  ,SEKY.FRB_CD                                                                   " & vbNewLine _
                                            & "  ,SEKY.SEKY_KMK                                                                 " & vbNewLine _
                                            & "  ,MC.TAX_KB                                                                     " & vbNewLine _
                                            & "  ,MB.NRS_BR_NM                                                                  " & vbNewLine _
                                            & "  ,KBN03.KBN_CD                                                                  " & vbNewLine _
                                            & "  ,KBN03.KBN_NM2                                                                 " & vbNewLine _
                                            & "  ) AS BASE                                                                      " & vbNewLine

    Private Const SQL_GROUP_DATA_00 As String = "  GROUP BY                                                                       " & vbNewLine _
                                          & "  BASE.RPT_ID,                                           " & vbNewLine _
                                          & "  BASE.NRS_BR_CD,                                        " & vbNewLine _
                                          & "  BASE.SEKY_YM,                                          " & vbNewLine _
                                          & "  BASE.SEKY_M_ENG,                                       " & vbNewLine _
                                          & "  BASE.DEPART,                                           " & vbNewLine _
                                          & "  BASE.DEPART_NM,                                        " & vbNewLine _
                                          & "  BASE.DEPART_NM_JP,                                     " & vbNewLine _
                                          & "  BASE.CUST_NM_S,                                        " & vbNewLine _
                                          & "  BASE.CUST_NM_SS,                                       " & vbNewLine _
                                          & "  BASE.MISK_CD,                                          " & vbNewLine _
                                          & "  BASE.SRC_CD,                                           " & vbNewLine _
                                          & "  BASE.SRC_NO,                                           " & vbNewLine _
                                          & "  BASE.PLANT_CD,                                         " & vbNewLine _
                                          & "  BASE.GL_BR_CD,                                         " & vbNewLine _
                                          & "  BASE.GL_BR_NM,                                         " & vbNewLine _
                                          & "  BASE.DUPONT_SEIQTO_NM                                  " & vbNewLine

    Private Const SQL_GROUP_DATA_01_02 As String = "  GROUP BY                                                                       " & vbNewLine _
                                          & "  BASE.RPT_ID,                                           " & vbNewLine _
                                          & "  BASE.NRS_BR_CD,                                        " & vbNewLine _
                                          & "  BASE.SEKY_YM,                                          " & vbNewLine _
                                          & "  BASE.SEKY_M_ENG,                                       " & vbNewLine _
                                          & "  BASE.DEPART,                                           " & vbNewLine _
                                          & "  BASE.DEPART_NM,                                        " & vbNewLine _
                                          & "  BASE.DEPART_NM_JP,                                     " & vbNewLine _
                                          & "  BASE.GL_BR_CD,                                         " & vbNewLine _
                                          & "  BASE.GL_BR_NM,                                         " & vbNewLine _
                                          & "  BASE.DUPONT_SEIQTO_NM                                  " & vbNewLine

    ''' <summary>
    ''' 請求書鑑出力対象データソート用
    ''' </summary>
    ''' <remarks>
    ''' 20120126追加
    '''</remarks>
    Private Const SQL_SORT_DATA00_01 As String = " ORDER BY                          " & vbNewLine _
                                            & " BASE.SEKY_YM                      " & vbNewLine _
                                            & ",BASE.DEPART                       " & vbNewLine _
                                            & ",BASE.MISK_CD                      " & vbNewLine _
                                            & ",BASE.SRC_CD                       " & vbNewLine

    ''' <summary>
    ''' 請求書鑑出力対象データソート用(営業所別)
    ''' </summary>
    ''' <remarks>
    '''</remarks>
    Private Const SQL_SORT_DATA00_02 As String = " ORDER BY                          " & vbNewLine _
                                            & " BASE.SEKY_YM                      " & vbNewLine _
                                            & ",BASE.GL_BR_CD                       " & vbNewLine _
                                            & ",BASE.DEPART                       " & vbNewLine _
                                            & ",BASE.MISK_CD                      " & vbNewLine _
                                            & ",BASE.SRC_CD                       " & vbNewLine

    ''' <summary>
    ''' 請求書鑑出力対象データソート用(事業部営業所別)
    ''' </summary>
    ''' <remarks>
    '''</remarks>
    Private Const SQL_SORT_DATA00_03 As String = " ORDER BY                          " & vbNewLine _
                                            & " BASE.SEKY_YM                      " & vbNewLine _
                                            & ",BASE.DEPART                       " & vbNewLine _
                                            & ",BASE.GL_BR_CD                       " & vbNewLine _
                                            & ",BASE.MISK_CD                      " & vbNewLine _
                                            & ",BASE.SRC_CD                       " & vbNewLine

    ''' <summary>
    ''' 集計表・経理用出力対象データソート用
    ''' </summary>
    ''' <remarks>
    ''' 20120126追加
    '''</remarks>
    Private Const SQL_SORT_DATA01_01 As String = " ORDER BY                       " & vbNewLine _
                                                & " BASE.SEKY_YM                  " & vbNewLine _
                                                & ",BASE.DEPART                   " & vbNewLine

    ''' <summary>
    ''' 集計表・経理用出力対象データソート用（営業所別）
    ''' </summary>
    ''' <remarks>
    '''</remarks>
    Private Const SQL_SORT_DATA01_02 As String = " ORDER BY                       " & vbNewLine _
                                                & " BASE.SEKY_YM                  " & vbNewLine _
                                                & ",BASE.GL_BR_CD                       " & vbNewLine _
                                                & ",BASE.DEPART                   " & vbNewLine

    ''' <summary>
    ''' 集計表・経理用出力対象データソート用（事業部営業所別）
    ''' </summary>
    ''' <remarks>
    '''</remarks>
    Private Const SQL_SORT_DATA01_03 As String = " ORDER BY                       " & vbNewLine _
                                                & " BASE.SEKY_YM                  " & vbNewLine _
                                                & ",BASE.DEPART                   " & vbNewLine _
                                                & ",BASE.GL_BR_CD                       " & vbNewLine

#End Region

#Region "MISK_CD"

    Private Const MISK_CD_MISK As String = "**"  'ミスク
    Private Const MISK_CD_MEIHEN As String = "##"  '名変

#End Region

#Region "印刷種別"

    Private Const PRINT_KAGAMI As String = "01"  '請求鑑
    Private Const PRINT_SHUKEI As String = "02"  '集計表
    Private Const PRINT_SHUKEIKEIRI As String = "03"  '集計表経理用

#End Region

#Region ""
    Private Const PRINT_TYPE_NOMAL As String = "01"  '通常
    Private Const PRINT_TYPE_BR As String = "02"     '営業所別
    Private Const PRINT_TYPE_DPTBR As String = "03"  '事業部営業所別

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

#Region "検索処理"

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG550IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Dim Ssql As String = String.Empty
        Me._StrSql.Append(LMG550DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select)
        Me._StrSql.Append(LMG550DAC.SQL_FROM_MPrt)        'SQL構築(帳票種別用from)
        Me._StrSql.Append(LMG550DAC.SQL_WHERE_DATA)       'SQL構築(データ抽出用WHERE)
        Call Me.SetSQLWhereSelectData(inTbl.Rows(0))
        Me._StrSql.Append("AND WCUST.RPT_ID IS NOT NULL") 'SQL構築(データ抽出用WHERE)

        Call Me.SetConditionPrtDataSQL()
        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG550DAC", "SelectMPrt", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")

        Return ds

    End Function

    ''' <summary>
    ''' 請求書鑑出力対象データ検索★
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>請求書鑑出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG550IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim PrintType As String = Me._Row.Item("PRT_TYPE").ToString()
        Dim PrintType2 As String = Me._Row.Item("PRT_TYPE2").ToString()

        'SQL作成

        Select Case PrintType

            Case PRINT_KAGAMI
                Me._StrSql.Append(LMG550DAC.SQL_SELECT_DATA_00)      'SQL構築(データ抽出用Select句00用)
            Case PRINT_SHUKEI
                Me._StrSql.Append(LMG550DAC.SQL_SELECT_DATA_01)      'SQL構築(データ抽出用Select句03用)
            Case PRINT_SHUKEIKEIRI
                Me._StrSql.Append(LMG550DAC.SQL_SELECT_DATA_02)      'SQL構築(データ抽出用Select句02用)
        End Select

        Me._StrSql.Append(LMG550DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        If PrintType2 = PRINT_TYPE_BR Or PrintType2 = PRINT_TYPE_DPTBR Then
            Me._StrSql.Append(LMG550DAC.SQL_SELECT_DATA_GLBRCD_1)
        Else
            Me._StrSql.Append(LMG550DAC.SQL_SELECT_DATA_GLBRCD_2)
        End If
        Me._StrSql.Append(LMG550DAC.SQL_SELECT_DATA_SEIQ_NM_1) '請求先名称取得
        Me._StrSql.Append(LMG550DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用FROM)
        Me._StrSql.Append(LMG550DAC.SQL_WHERE_DATA)        'SQL構築(データ抽出用WHERE)
        Call Me.SetSQLWhereSelectData(inTbl.Rows(0))
        Me._StrSql.Append(LMG550DAC.SQL_GROUP_DATA)        'SQL構築(データ抽出用GROUP)
        If PrintType = PRINT_KAGAMI Then
            Me._StrSql.Append(LMG550DAC.SQL_GROUP_DATA_00)        'SQL構築(データ抽出用GROUP)
            If PrintType2 = PRINT_TYPE_BR Then
                Me._StrSql.Append(LMG550DAC.SQL_SORT_DATA00_02)
            ElseIf PrintType2 = PRINT_TYPE_DPTBR Then
                Me._StrSql.Append(LMG550DAC.SQL_SORT_DATA00_03)
            Else
                Me._StrSql.Append(LMG550DAC.SQL_SORT_DATA00_01)        'SQL構築(データ抽出用FROM)
            End If
        Else
            Me._StrSql.Append(LMG550DAC.SQL_GROUP_DATA_01_02)        'SQL構築(データ抽出用GROUP)
            If PrintType2 = PRINT_TYPE_BR Then
                Me._StrSql.Append(LMG550DAC.SQL_SORT_DATA01_02)
            ElseIf PrintType2 = PRINT_TYPE_DPTBR Then
                Me._StrSql.Append(LMG550DAC.SQL_SORT_DATA01_03)
            Else
                Me._StrSql.Append(LMG550DAC.SQL_SORT_DATA01_01)        'SQL構築(データ抽出用FROM)
            End If
        End If

        Call Me.SetConditionPrtDataSQL()                  '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG550DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング(LMG550向けに変更済み)
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("SEKY_YM", "SEKY_YM")
        map.Add("SEKY_M_ENG", "SEKY_M_ENG")
        map.Add("DEPART", "DEPART")
        map.Add("DEPART_NM", "DEPART_NM")
        map.Add("DEPART_NM_JP", "DEPART_NM_JP")
        map.Add("CUST_NM_S", "CUST_NM_S")
        map.Add("CUST_NM_SS", "CUST_NM_SS")
        map.Add("MISK_CD", "MISK_CD")
        map.Add("SRC_CD", "SRC_CD")
        map.Add("SRC_NO", "SRC_NO")
        map.Add("PLANT_CD", "PLANT_CD")
        map.Add("STORAGE", "STORAGE")
        map.Add("HANDLING", "HANDLING")
        map.Add("TRUCKAGE", "TRUCKAGE")
        map.Add("CLEARANCE", "CLEARANCE")
        map.Add("EXPORT", "EXPORT")
        map.Add("FPDE_OTHER", "FPDE_OTHER")
        map.Add("EXPENSE", "EXPENSE")
        map.Add("TAX", "TAX")
        map.Add("TOTAL", "TOTAL")
        map.Add("SOUND_STORAGE", "SOUND_STORAGE")
        map.Add("SOUND_HANDLING", "SOUND_HANDLING")
        map.Add("SOUND_TRUCKAGE", "SOUND_TRUCKAGE")
        map.Add("SOUND_CLEARANCE", "SOUND_CLEARANCE")
        map.Add("SOUND_EXPORT", "SOUND_EXPORT")
        map.Add("SOUND_FPDE_OTHER", "SOUND_FPDE_OTHER")
        map.Add("SOUND_EXPENSE", "SOUND_EXPENSE")
        map.Add("SOUND_TAX", "SOUND_TAX")
        map.Add("SOUND_TOTAL", "SOUND_TOTAL")
        map.Add("BOND_STORAGE", "BOND_STORAGE")
        map.Add("BOND_HANDLING", "BOND_HANDLING")
        map.Add("BOND_TRUCKAGE", "BOND_TRUCKAGE")
        map.Add("BOND_CLEARANCE", "BOND_CLEARANCE")
        map.Add("BOND_EXPORT", "BOND_EXPORT")
        map.Add("BOND_FPDE_OTHER", "BOND_FPDE_OTHER")
        map.Add("BOND_EXPENSE", "BOND_EXPENSE")
        map.Add("BOND_TAX", "BOND_TAX")
        map.Add("BOND_TOTAL", "BOND_TOTAL")
        map.Add("GL_BR_CD", "GL_BR_CD")
        map.Add("GL_BR_NM", "GL_BR_NM")
        map.Add("DUPONT_SEIQTO_NM", "DUPONT_SEIQTO_NM")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG550OUT")

        Return ds

    End Function

#End Region

#Region "SQL"

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

#End Region

#Region "パラメタ設定"

    '''' <summary>
    '''' 条件文・パラメータ設定モジュール（出力対象帳票パターン取得処理用）
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub SetConditionPrtMasterSQL()

    '    'SQLパラメータ初期化
    '    Me._SqlPrmList = New ArrayList()

    '    'パラメータ設定
    '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_NO", Me._Row.Item("SKYU_NO").ToString(), DBDataType.CHAR))

    'End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール（請求書鑑出力対象データ検索用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionPrtDataSQL()

        Dim PrintType As String = Me._Row.Item("PRT_TYPE").ToString()
        Dim PrintType2 As String = Me._Row.Item("PRT_TYPE2").ToString()

        Select Case PrintType

            Case PRINT_KAGAMI
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_TYPE", "73", DBDataType.CHAR))

            Case PRINT_SHUKEI
                If PrintType2 = PRINT_TYPE_DPTBR Then
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_TYPE", "87", DBDataType.CHAR))
                Else
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_TYPE", "74", DBDataType.CHAR))
                End If
            Case PRINT_SHUKEIKEIRI
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_TYPE", "75", DBDataType.CHAR))

        End Select
    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhereSelectData(ByVal inTblRow As DataRow)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Dim PrintType As String = Me._Row.Item("PRT_TYPE").ToString()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '請求先
            whereStr = .Item("SEIQTO_KBN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND KBN03.KBN_CD = @SEIQTO_KBN")
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_KBN", whereStr, DBDataType.CHAR))
            End If

            '請求年月
            whereStr = .Item("INV_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEKY.SEKY_YM = @INV_DATE")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_DATE", whereStr, DBDataType.CHAR))
            End If

            ''請求項目
            'whereStr = .Item("SEKY_KMK").ToString()
            'If String.IsNullOrEmpty(whereStr) = False Then
            '    Me._StrSql.Append(" AND SEKY.SEKY_KMK = @SEKY_KMK")
            '    Me._StrSql.Append(vbNewLine)
            '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_KMK", whereStr, DBDataType.CHAR))
            'End If

            '処理選択
            Select Case PrintType
                Case PRINT_KAGAMI
                    whereStr = .Item("KAGAMI_PRC").ToString()
                    Select Case whereStr
                        Case "00" '通常
                            Me._StrSql.Append(String.Concat(" AND (SEKY.SEKY_KMK <> '00' AND SEKY.SEKY_KMK <> '04' AND SEKY.MISK_CD <> '", MISK_CD_MISK, "' AND SEKY.MISK_CD <> '", MISK_CD_MEIHEN, "')"))
                            Me._StrSql.Append(vbNewLine)
                        Case "01" 'ミスク
                            Me._StrSql.Append(String.Concat("  AND SEKY.MISK_CD = '", MISK_CD_MISK, "'"))
                            Me._StrSql.Append(vbNewLine)
                        Case "02" '名変
                            Me._StrSql.Append(String.Concat("  AND SEKY.MISK_CD = '", MISK_CD_MEIHEN, "'"))
                            Me._StrSql.Append(vbNewLine)
                        Case "03" '通関
                            Me._StrSql.Append(String.Concat(" AND ((SEKY.SEKY_KMK = '00' AND SEKY.MISK_CD <> '", MISK_CD_MISK, "' AND SEKY.MISK_CD <> '", MISK_CD_MEIHEN, "') OR (SEKY.SEKY_KMK = '04' AND SEKY.MISK_CD <> '", MISK_CD_MISK, "' AND SEKY.MISK_CD <> '", MISK_CD_MEIHEN, "'))"))
                            Me._StrSql.Append(vbNewLine)

                    End Select

                    whereStr = .Item("DEPERT").ToString()
                    If String.IsNullOrEmpty(whereStr) = False Then
                        Me._StrSql.Append(" AND SEKY.DEPART = @DEPERT")
                        Me._StrSql.Append(vbNewLine)
                        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEPERT", whereStr, DBDataType.CHAR))
                    End If
                    'ADD 2021/05/21 020903   【LMS】ケマーズ・アクサルタの特別荷主機能の編集
                    'FRB_CD  'BD00942' は除く
                    Me._StrSql.Append(" AND SEKY.FRB_CD <> 'BD00942'")
                    Me._StrSql.Append(vbNewLine)

                Case PRINT_SHUKEI '集計表
                    whereStr = .Item("MISK_PRC").ToString()
                    Select Case whereStr
                        Case "00" '通常
                            Me._StrSql.Append(String.Concat(" AND (SEKY.MISK_CD <> '", MISK_CD_MISK, "' AND SEKY.MISK_CD <> '", MISK_CD_MEIHEN, "')"))
                            Me._StrSql.Append(vbNewLine)
                        Case "01" 'ミスク
                            Me._StrSql.Append(String.Concat(" AND (SEKY.MISK_CD = '", MISK_CD_MISK, "'   OR SEKY.MISK_CD = '", MISK_CD_MEIHEN, "')"))
                            Me._StrSql.Append(vbNewLine)
                        Case "02" '全部

                    End Select

                Case Else
                    'なにも設定しない
            End Select

        End With

    End Sub
#End Region

End Class
