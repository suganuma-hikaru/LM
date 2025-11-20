' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LML       : 協力会社
'  プログラムID     :  LML010    : 協力会社
'  作  成  者       :  [daikoku]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LML010DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LML010DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#End Region

#Region "作業チェック"
    ''' <summary>
    ''' 作業チェック1（SELECT句）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SAGYO_CHK1 As String = " 	SELECT * FROM (	                           " & vbNewLine _
                                              & "	SELECT SAGYO_CD FROM LM_MST..M_SAGYO	   " & vbNewLine _
                                              & "	WHERE NRS_BR_CD = @PC_BR_CD	               " & vbNewLine _
                                              & "	AND CUST_CD_L = @PC_CUST_CD	               " & vbNewLine _
                                              & "	AND SAGYO_UP <> '0'	                       " & vbNewLine _
                                              & "	) A LEFT JOIN	                           " & vbNewLine _
                                              & "	(	                                       " & vbNewLine _
                                              & "	SELECT SAGYO_CD_SUB FROM LM_MST..M_SAGYO   " & vbNewLine _
                                              & "	WHERE NRS_BR_CD = @NRS_BR_CD	           " & vbNewLine _
                                              & "	AND CUST_CD_L = @NRS_CUST_CD	           " & vbNewLine _
                                              & "	) B	                                       " & vbNewLine _
                                              & "	ON A.SAGYO_CD = B.SAGYO_CD_SUB	           " & vbNewLine _
                                              & "	WHERE B.SAGYO_CD_SUB IS NULL	           " & vbNewLine
    Private Const SQL_SAGYO_CHK2 As String = "	SELECT SAGYO_CD,COUNT(A.SAGYO_CD) DABURI FROM (	 " & vbNewLine _
                                              & "	SELECT SAGYO_CD FROm LM_MST..M_SAGYO	     " & vbNewLine _
                                              & "	WHERE NRS_BR_CD = @PC_BR_CD                  " & vbNewLine _
                                              & "	AND CUST_CD_L = @PC_CUST_CD                  " & vbNewLine _
                                              & "	AND SAGYO_UP <> '0'	                         " & vbNewLine _
                                              & "	) A LEFT JOIN	                             " & vbNewLine _
                                              & "	(	                                         " & vbNewLine _
                                              & "	SELECT SAGYO_CD_SUB FROm LM_MST..M_SAGYO     " & vbNewLine _
                                              & "	WHERE NRS_BR_CD = @NRS_BR_CD	             " & vbNewLine _
                                              & "	AND CUST_CD_L = @NRS_CUST_CD                 " & vbNewLine _
                                              & "	) B	                                         " & vbNewLine _
                                              & "	ON A.SAGYO_CD = B.SAGYO_CD_SUB	             " & vbNewLine _
                                              & "	GROUP BY A.SAGYO_CD	                         " & vbNewLine _
                                              & "	HAVING COUNT(A.SAGYO_CD) >= 2	             " & vbNewLine

#End Region

#Region "商品マスタ関連"

    ''' <summary>
    ''' 商品マスタ（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_MGOODS As String = "	DELETE LM_MST..M_GOODS	        " & vbNewLine _
                                              & "	WHERE NRS_BR_CD = @NRS_BR_CD	" & vbNewLine _
                                              & "	AND CUST_CD_L = @NRS_CUST_CD    " & vbNewLine _
                                              & "	AND GOODS_CD_NRS IN 	        " & vbNewLine _
                                              & "	(	                            " & vbNewLine _
                                              & "	SELECT GOODS_CD_NRS	            " & vbNewLine _
                                              & "	FROM LM_MST..M_GOODS	        " & vbNewLine _
                                              & "	WHERE NRS_BR_CD = @PC_BR_CD     " & vbNewLine _
                                              & "	AND CUST_CD_L = @PC_CUST_CD	    " & vbNewLine _
                                              & "	)	                            " & vbNewLine


    ''' <summary>
    ''' 商品マスタ詳細（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_MGOODS_DETAILS As String = "DELETE LM_MST..M_GOODS_DETAILS	 " & vbNewLine _
                                              & "	WHERE NRS_BR_CD = @NRS_BR_CD	         " & vbNewLine _
                                              & "	AND GOODS_CD_NRS IN 	                 " & vbNewLine _
                                              & "	(	                                     " & vbNewLine _
                                              & "	SELECT GOODS_CD_NRS	                     " & vbNewLine _
                                              & "	FROM LM_MST..M_GOODS                     " & vbNewLine _
                                              & "	WHERE NRS_BR_CD = @PC_BR_CD	             " & vbNewLine _
                                              & "	AND CUST_CD_L = @PC_CUST_CD              " & vbNewLine _
                                              & "	)	                            " & vbNewLine

    ''' <summary>
    ''' WK_RT商品マスタ詳細（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_WK_RTGOODS_DETAILS As String = "DELETE LM_MST..WK_RT_GOODS_DETAILS	 "

    ''' <summary>
    ''' WK_RT商品マスタ（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_WK_RTGOODS As String = "DELETE LM_MST..WK_RT_GOODS	 "

    ''' <summary>
    ''' 商品マスタ（追加）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
#If False Then  'UPD 2022/03/30 028346
    Private Const SQL_INSERT_MGOODS As String = "	INSERT INTO LM_MST..M_GOODS	 " & vbNewLine _
                   & "	SELECT                     	           " & vbNewLine _
                   & "	   @NRS_BR_CD	                 " & vbNewLine _
                   & "	 , GOODS_CD_NRS	                 " & vbNewLine _
                   & "	 , @NRS_CUST_CD	                 " & vbNewLine _
                   & "	 , CUST_CD_M	                 " & vbNewLine _
                   & "	 , CUST_CD_S	                 " & vbNewLine _
                   & "	 , CUST_CD_SS	                 " & vbNewLine _
                   & "	 , GOODS_CD_CUST	                             " & vbNewLine _
                   & "	 , SEARCH_KEY_1, SEARCH_KEY_2, CUST_COST_CD1, CUST_COST_CD2, JAN_CD, GOODS_NM_1, GOODS_NM_2	     " & vbNewLine _
                   & "	 , GOODS_NM_3, UP_GP_CD_1, SHOBO_CD, KIKEN_KB, UN, PG_KB, CLASS_1, CLASS_2, CLASS_3, CHEM_MTRL_KB	 " & vbNewLine _
                   & "	 , DOKU_KB, GAS_KANRI_KB, ONDO_KB, UNSO_ONDO_KB, ONDO_MX, ONDO_MM, ONDO_STR_DATE	         " & vbNewLine _
                   & "	 , ONDO_END_DATE, ONDO_UNSO_STR_DATE, ONDO_UNSO_END_DATE, KYOKAI_GOODS_KB, ALCTD_KB, NB_UT	     " & vbNewLine _
                   & "	 , PKG_NB, PKG_UT, PLT_PER_PKG_UT, INNER_PKG_NB, STD_IRIME_NB, STD_IRIME_UT, STD_WT_KGS, STD_CBM	 " & vbNewLine _
                   & "	 , INKA_KAKO_SAGYO_KB_1, INKA_KAKO_SAGYO_KB_2, INKA_KAKO_SAGYO_KB_3, INKA_KAKO_SAGYO_KB_4	     " & vbNewLine _
                   & "	 , INKA_KAKO_SAGYO_KB_5, OUTKA_KAKO_SAGYO_KB_1, OUTKA_KAKO_SAGYO_KB_2, OUTKA_KAKO_SAGYO_KB_3	   " & vbNewLine _
                   & "	 , OUTKA_KAKO_SAGYO_KB_4, OUTKA_KAKO_SAGYO_KB_5, PKG_SAGYO, TARE_YN, SP_NHS_YN, COA_YN, LOT_CTL_KB	 " & vbNewLine _
                   & "	 , LT_DATE_CTL_KB, CRT_DATE_CTL_KB, DEF_SPD_KB, KITAKU_AM_UT_KB, KITAKU_GOODS_UP, ORDER_KB, ORDER_NB " & vbNewLine _
                   & "	 , SHIP_CD_L, SKYU_MEI_YN, HIKIATE_ALERT_YN, UNSO_HOKEN_YN, OUTKA_ATT, PRINT_NB, CONSUME_PERIOD_DATE " & vbNewLine _
                   & "	 , OCR_GOODS_CD_CUST, OCR_GOODS_CD_NM1, OCR_GOODS_CD_NM2, OCR_GOODS_CD_STD_IRIME, OUTER_PKG	     " & vbNewLine _
                   & "	 , KIKEN_DATE, KIKEN_USER_ID, WIDTH, HEIGHT, DEPTH, VOLUME, OCCUPY_VOLUME, OUTKA_HASU_SAGYO_KB_1	 " & vbNewLine _
                   & "	 , OUTKA_HASU_SAGYO_KB_2, OUTKA_HASU_SAGYO_KB_3, AVAL_YN, HIZYU, KOUATHUGAS_KB, YAKUZIHO_KB	         " & vbNewLine _
                   & "	 , SHOBOKIKEN_KB, KAIYOUOSEN_KB	                                                                    " & vbNewLine _
                   & "	 , SYS_ENT_DATE	                                                     " & vbNewLine _
                   & "	 , SYS_ENT_TIME	                                                     " & vbNewLine _
                   & "	 , SYS_ENT_PGID                                                      " & vbNewLine _
                   & "	 , SYS_ENT_USER	                                                     " & vbNewLine _
                   & "	 , @SYS_UPD_DATE                                                     " & vbNewLine _
                   & "	 , @SYS_UPD_TIME	                                                 " & vbNewLine _
                   & "	 , @SYS_UPD_PGID	                                                 " & vbNewLine _
                   & "	 , @SYS_UPD_USER	                                                 " & vbNewLine _
                   & "	 , SYS_DEL_FLG	                                                     " & vbNewLine _
                   & "	 , SIZE_KB	                                                         " & vbNewLine _
                   & "	FROM LM_MST..M_GOODS                                               	 " & vbNewLine _
                   & "	WHERE  NRS_BR_CD = @PC_BR_CD                                       	 " & vbNewLine _
                   & "	  AND  CUST_CD_L = @PC_CUST_CD                                       " & vbNewLine _
                   & "--★追加★                                                             " & vbNewLine _
                   & " AND GOODS_CD_NRS NOT IN                                               " & vbNewLine _
                   & " (                                                                     " & vbNewLine _
                   & "  SELECT GOODS_CD_NRS FROM LM_MST..M_GOODS                             " & vbNewLine _
                   & "  WHERE NRS_BR_CD = @NRS_BR_CD                                         " & vbNewLine _
                   & "  AND CUST_CD_L   = @NRS_CUST_CD                                       " & vbNewLine _
                   & " )                                                                     " & vbNewLine


#Else
    Private Const SQL_INSERT_WK_RT_GOODS As String = "INSERT INTO LM_MST..WK_RT_GOODS	 " & vbNewLine _
                                                   & "	SELECT  M_GOODS.*                " & vbNewLine _
                                                   & "	FROM LM_MST..M_GOODS                                               	 " & vbNewLine _
                                                   & "	WHERE  NRS_BR_CD = @PC_BR_CD                                       	 " & vbNewLine _
                                                   & "	  AND  CUST_CD_L = @PC_CUST_CD                                       " & vbNewLine _
                                                   & "--★追加★                                                             " & vbNewLine _
                                                   & " AND GOODS_CD_NRS NOT IN                                               " & vbNewLine _
                                                   & " (                                                                     " & vbNewLine _
                                                   & "  SELECT GOODS_CD_NRS FROM LM_MST..M_GOODS                             " & vbNewLine _
                                                   & "  WHERE NRS_BR_CD = @NRS_BR_CD                                         " & vbNewLine _
                                                   & "  AND CUST_CD_L   = @NRS_CUST_CD                                       " & vbNewLine _
                                                   & " )                                                                     "


#End If
    ''' <summary>
    ''' WK_RT_GOODS（更新）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Const SQL_UPDATT_WK_RT_GOODS As String = "UPDATE LM_MST..WK_RT_GOODS	                                         " & vbNewLine _
                                                   & "	  SET NRS_BR_CD    = @NRS_BR_CD	                                     " & vbNewLine _
                                                   & "	     ,CUST_CD_L　  = @NRS_CUST_CD	                                 " & vbNewLine _
                                                   & "	     ,SYS_UPD_DATE = @SYS_UPD_DATE                                   " & vbNewLine _
                                                   & "	     ,SYS_UPD_TIME = @SYS_UPD_TIME	                                 " & vbNewLine _
                                                   & "	     ,SYS_UPD_PGID = @SYS_UPD_PGID	                                 " & vbNewLine _
                                                   & "	     ,SYS_UPD_USER = @SYS_UPD_USER	                                 " & vbNewLine _
                                                   & "	WHERE  NRS_BR_CD = @PC_BR_CD                                       	 "

    ''' <summary>
    ''' 商品マスタ（追加）
    ''' </summary>
    ''' <remarks></remarks>
    '''
    Private Const SQL_INSERT_M_GOODS As String = "INSERT LM_MST..M_GOODS　　　                         " & vbNewLine _
                                                      & "	SELECT WK_RT_GOODS.*               　　　　" & vbNewLine _
                                                      & "	  FROM LM_MST..WK_RT_GOODS  　　　　　　　 " & vbNewLine _
                                                      & "	  WHERE  SYS_UPD_DATE  = @SYS_UPD_DATE	   " & vbNewLine _
                                                      & "	    AND  SYS_UPD_TIME  = @SYS_UPD_TIME	   " & vbNewLine _
                                                      & "	    AND  SYS_UPD_PGID  = @SYS_UPD_PGID	   " & vbNewLine _
                                                      & "	    AND  SYS_UPD_USER  = @SYS_UPD_USER	   "

    ''' <summary>
    ''' 商品詳細マスタ（追加）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
#If False Then  'UPD 2022/03/30 028346
    Private Const SQL_INSERT_MGOODS_DETAILS As String = "INSERT LM_MST..M_GOODS_DETAILS	 " & vbNewLine _
                  & "	SELECT @NRS_BR_CD	     " & vbNewLine _
                  & "	      ,GOODS_CD_NRS	     " & vbNewLine _
                  & "	      ,GOODS_CD_NRS_EDA	 " & vbNewLine _
                  & "	      ,SUB_KB	         " & vbNewLine _
                  & "	      ,SET_NAIYO	     " & vbNewLine _
                  & "	      ,REMARK	         " & vbNewLine _
                  & "	      ,SYS_ENT_DATE	     " & vbNewLine _
                  & "	      ,SYS_ENT_TIME	     " & vbNewLine _
                  & "	      ,SYS_ENT_PGID	     " & vbNewLine _
                  & "	      ,SYS_ENT_USER	     " & vbNewLine _
                  & "	      ,@SYS_UPD_DATE	 " & vbNewLine _
                  & "	      ,@SYS_UPD_TIME	 " & vbNewLine _
                  & "	      ,@SYS_UPD_PGID	 " & vbNewLine _
                  & "	      ,@SYS_UPD_USER	 " & vbNewLine _
                  & "	      ,SYS_DEL_FLG	     " & vbNewLine _
                  & "	  FROM LM_MST..M_GOODS_DETAILS	       " & vbNewLine _
                  & "	  WHERE NRS_BR_CD = @PC_BR_CD          " & vbNewLine _
                  & "	　　AND GOODS_CD_NRS IN                " & vbNewLine _
                  & "	　　　(	                               " & vbNewLine _
                  & "	　　　　SELECT GOODS_CD_NRS	           " & vbNewLine _
                  & "	　　　　　FROM LM_MST..M_GOODS	       " & vbNewLine _
                  & "	　　　　WHERE NRS_BR_CD = @PC_BR_CD	   " & vbNewLine _
                  & "	　　　　  AND CUST_CD_L = @PC_CUST_CD  " & vbNewLine _
                  & "	　　　 )	                           " & vbNewLine _
                  & "--★追加★                                                             " & vbNewLine _
                  & " AND GOODS_CD_NRS NOT IN                                               " & vbNewLine _
                  & " (                                                                     " & vbNewLine _
                  & "  SELECT GOODS_CD_NRS FROM LM_MST..M_GOODS                             " & vbNewLine _
                  & "  WHERE NRS_BR_CD = @NRS_BR_CD                                         " & vbNewLine _
                  & "  AND CUST_CD_L   = @NRS_CUST_CD                                       " & vbNewLine _
                  & " )                                                                     " & vbNewLine

#Else
    Private Const SQL_INSERT_WK_RT_GOODS_DETAILS As String = "INSERT LM_MST..WK_RT_GOODS_DETAILS	 " & vbNewLine _
                                                          & "	SELECT M_GOODS_DETAILS.*               " & vbNewLine _
                                                          & "	  FROM LM_MST..M_GOODS_DETAILS	       " & vbNewLine _
                                                          & "	  WHERE NRS_BR_CD = @PC_BR_CD          " & vbNewLine _
                                                          & "	　　AND GOODS_CD_NRS IN                " & vbNewLine _
                                                          & "	　　　(	                               " & vbNewLine _
                                                          & "	　　　　SELECT GOODS_CD_NRS	           " & vbNewLine _
                                                          & "	　　　　　FROM LM_MST..M_GOODS	       " & vbNewLine _
                                                          & "	　　　　WHERE NRS_BR_CD = @PC_BR_CD	   " & vbNewLine _
                                                          & "	　　　　  AND CUST_CD_L = @PC_CUST_CD  " & vbNewLine _
                                                          & "	　　　 )	                           " & vbNewLine _
                                                          & "--★追加★                                                             " & vbNewLine _
                                                          & " AND GOODS_CD_NRS NOT IN                                               " & vbNewLine _
                                                          & " (                                                                     " & vbNewLine _
                                                          & "  SELECT GOODS_CD_NRS FROM LM_MST..M_GOODS                             " & vbNewLine _
                                                          & "  WHERE NRS_BR_CD = @NRS_BR_CD                                         " & vbNewLine _
                                                          & "  AND CUST_CD_L   = @NRS_CUST_CD                                       " & vbNewLine _
                                                          & " )                                                                     " & vbNewLine

#End If

    ''' <summary>
    ''' WK_RT_商品詳細マスタ（更新）
    ''' </summary>
    ''' <remarks></remarks>
    '''
    Private Const SQL_UPDATE_WK_RT_GOODS_DETAILS As String = "UPDATE LM_MST..WK_RT_GOODS_DETAILS	 " & vbNewLine _
                                                      & "	   SET NRS_BR_CD     = @NRS_BR_CD	     " & vbNewLine _
                                                      & "	      ,SYS_UPD_DATE  = @SYS_UPD_DATE	 " & vbNewLine _
                                                      & "	      ,SYS_UPD_TIME  = @SYS_UPD_TIME	 " & vbNewLine _
                                                      & "	      ,SYS_UPD_PGID  = @SYS_UPD_PGID	 " & vbNewLine _
                                                      & "	      ,SYS_UPD_USER  = @SYS_UPD_USER	 " & vbNewLine _
                                                      & "	  WHERE NRS_BR_CD = @PC_BR_CD            "
    ''' <summary>
    ''' 商品詳細マスタ（追加）
    ''' </summary>
    ''' <remarks></remarks>
    '''
    Private Const SQL_INSERT_M_GOODS_DETAILS As String = "INSERT LM_MST..M_GOODS_DETAILS	 " & vbNewLine _
                                                      & "	SELECT WK_RT_GOODS_DETAILS.*               " & vbNewLine _
                                                      & "	  FROM LM_MST..WK_RT_GOODS_DETAILS	       " & vbNewLine _
                                                      & "	  WHERE  SYS_UPD_DATE  = @SYS_UPD_DATE	   " & vbNewLine _
                                                      & "	    AND  SYS_UPD_TIME  = @SYS_UPD_TIME	   " & vbNewLine _
                                                      & "	    AND  SYS_UPD_PGID  = @SYS_UPD_PGID	   " & vbNewLine _
                                                      & "	    AND  SYS_UPD_USER  = @SYS_UPD_USER	   "

#End Region

#Region "届先マスタ関連"

    ''' <summary>
    ''' 届先マスタ（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_DEST As String = "	DELETE LM_MST..M_DEST	 " & vbNewLine _
     & "	WHERE NRS_BR_CD = @NRS_BR_CD	 " & vbNewLine _
     & "	AND CUST_CD_L = @NRS_CUST_CD	 " & vbNewLine _
     & "	AND DEST_CD IN 	                 " & vbNewLine _
     & "	(	                             " & vbNewLine _
     & "	SELECT DEST_CD	                 " & vbNewLine _
     & "	FROM LM_MST..M_DEST	             " & vbNewLine _
     & "	WHERE NRS_BR_CD = @PC_BR_CD	     " & vbNewLine _
     & "	AND CUST_CD_L = @PC_CUST_CD	     " & vbNewLine _
     & "	)	                            " & vbNewLine

    ''' <summary>
    ''' 届先マスタ詳細（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_MDEST_DETAILS As String = "	DELETE LM_MST..M_DEST_DETAILS	 " & vbNewLine _
     & "	WHERE NRS_BR_CD = @NRS_BR_CD     " & vbNewLine _
     & "	AND CUST_CD_L = @NRS_CUST_CD	 " & vbNewLine _
     & "	AND DEST_CD IN 	                 " & vbNewLine _
     & "	(	                             " & vbNewLine _
     & "	SELECT DEST_CD	                 " & vbNewLine _
     & "	FROM LM_MST..M_DEST	             " & vbNewLine _
     & "	WHERE NRS_BR_CD = @PC_BR_CD	     " & vbNewLine _
     & "	  AND CUST_CD_L = @PC_CUST_CD    " & vbNewLine _
     & "	)	                             " & vbNewLine

    ''' <summary>
    ''' WK_RT_DEST_DETAILS（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_WK_RTDEST_DETAILS As String = "DELETE LM_MST..WK_RT_DEST_DETAILS	 "

    ''' <summary>
    ''' WK_RT_DEST（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_WK_RTDEST As String = "DELETE LM_MST..WK_RT_DEST	 "


    ''' <summary>
    ''' 届先マスタ（追加）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
#If False Then
       Private Const SQL_INSERT_MDEST As String = "	INSERT INTO LM_MST..M_DEST	 " & vbNewLine _
     & "	SELECT @NRS_BR_CD	 " & vbNewLine _
     & "	      ,@NRS_CUST_CD	 " & vbNewLine _
     & "	      ,DEST_CD	 " & vbNewLine _
     & "	      ,EDI_CD	 " & vbNewLine _
     & "	      ,DEST_NM	 " & vbNewLine _
     & "	      ,KANA_NM	 " & vbNewLine _
     & "	      ,ZIP	 " & vbNewLine _
     & "	      ,AD_1	 " & vbNewLine _
     & "	      ,AD_2	 " & vbNewLine _
     & "	      ,AD_3	 " & vbNewLine _
     & "	      ,CUST_DEST_CD	 " & vbNewLine _
     & "	      ,SALES_CD	 " & vbNewLine _
     & "	      ,SP_NHS_KB	 " & vbNewLine _
     & "	      ,COA_YN	 " & vbNewLine _
     & "	      ,SP_UNSO_CD	 " & vbNewLine _
     & "	      ,SP_UNSO_BR_CD	 " & vbNewLine _
     & "	      ,DELI_ATT	 " & vbNewLine _
     & "	      ,CARGO_TIME_LIMIT	 " & vbNewLine _
     & "	      ,LARGE_CAR_YN	 " & vbNewLine _
     & "	      ,TEL	 " & vbNewLine _
     & "	      ,FAX	 " & vbNewLine _
     & "	      ,UNCHIN_SEIQTO_CD	 " & vbNewLine _
     & "	      ,JIS	 " & vbNewLine _
     & "	      ,0 	 " & vbNewLine _
     & "	      ,PICK_KB	 " & vbNewLine _
     & "	      ,BIN_KB	 " & vbNewLine _
     & "	      ,MOTO_CHAKU_KB	 " & vbNewLine _
     & "	      ,URIAGE_CD	 " & vbNewLine _
     & "	      ,REMARK	 " & vbNewLine _
     & "	      ,SHIHARAI_AD	 " & vbNewLine _
     & "	      ,INTEG_DISP	 " & vbNewLine _
     & "	      ,SYS_ENT_DATE	 " & vbNewLine _
     & "	      ,SYS_ENT_TIME	 " & vbNewLine _
     & "	      ,SYS_ENT_PGID	 " & vbNewLine _
     & "	      ,SYS_ENT_USER	 " & vbNewLine _
     & "	      ,@SYS_UPD_DATE	 " & vbNewLine _
     & "	      ,@SYS_UPD_TIME	 " & vbNewLine _
     & "	      ,@SYS_UPD_PGID	 " & vbNewLine _
     & "	      ,@SYS_UPD_USER	 " & vbNewLine _
     & "	      ,SYS_DEL_FLG	     " & vbNewLine _
     & "	  FROM LM_MST..M_DEST	 " & vbNewLine _
     & "	WHERE NRS_BR_CD = @PC_BR_CD		 " & vbNewLine _
     & "	  AND CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
     & "--★追加★                                 " & vbNewLine _
     & " AND DEST_CD NOT IN                        " & vbNewLine _
     & " (                                         " & vbNewLine _
     & "  SELECT DEST_CD FROM LM_MST..M_DEST       " & vbNewLine _
     & "  WHERE NRS_BR_CD = @NRS_BR_CD             " & vbNewLine _
     & "  AND CUST_CD_L   = @NRS_CUST_CD           " & vbNewLine _
     & "  )                                        " & vbNewLine.
#Else

    Private Const SQL_INSERT_WK_RTDEST As String = "INSERT LM_MST..WK_RT_DEST   　　　　　　　" & vbNewLine _
                                                 & "	SELECT M_DEST.*	                       " & vbNewLine _
                                                 & "	  FROM LM_MST..M_DEST	               " & vbNewLine _
                                                 & "	WHERE NRS_BR_CD = @PC_BR_CD		       " & vbNewLine _
                                                 & "	  AND CUST_CD_L = @PC_CUST_CD	       " & vbNewLine _
                                                 & "--★追加★                                 " & vbNewLine _
                                                 & "      AND DEST_CD NOT IN                   " & vbNewLine _
                                                 & "       (                                   " & vbNewLine _
                                                 & "        SELECT DEST_CD FROM LM_MST..M_DEST " & vbNewLine _
                                                 & "         WHERE NRS_BR_CD = @NRS_BR_CD      " & vbNewLine _
                                                 & "           AND CUST_CD_L   = @NRS_CUST_CD  " & vbNewLine _
                                                 & "       )                                   "

#End If


    ''' <summary>
    ''' WK_RT届先マスタ（更新）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Const SQL_UPDATE_WK_RTDEST As String = "UPDATE LM_MST..WK_RT_DEST	     " & vbNewLine _
                                                     & "	  SET  NRS_BR_CD    = @NRS_BR_CD	 " & vbNewLine _
                                                     & "	      ,CUST_CD_L    = @NRS_CUST_CD	 " & vbNewLine _
                                                     & "	      ,SYS_UPD_DATE = @SYS_UPD_DATE	 " & vbNewLine _
                                                     & "	      ,SYS_UPD_TIME = @SYS_UPD_TIME	 " & vbNewLine _
                                                     & "	      ,SYS_UPD_PGID = @SYS_UPD_PGID	 " & vbNewLine _
                                                     & "	      ,SYS_UPD_USER = @SYS_UPD_USER	 " & vbNewLine _
                                                     & "	  FROM LM_MST..WK_RT_DEST            " & vbNewLine _
                                                     & "	  WHERE NRS_BR_CD = @PC_BR_CD	     " & vbNewLine _
                                                     & "	    AND CUST_CD_L = @PC_CUST_CD	     "

    ''' <summary>
    ''' 届先マスタ（追加）（WK_RT_DESTより追加）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Const SQL_INSERT_M_DEST As String = "INSERT LM_MST..M_DEST   　　　　　　　　　　　　　　  " & vbNewLine _
                                            & "	SELECT WK_RT_DEST.*                        " & vbNewLine _
                                            & "	  FROM LM_MST..WK_RT_DEST  　　　　　　　　" & vbNewLine _
                                            & "	  WHERE  SYS_UPD_DATE  = @SYS_UPD_DATE	   " & vbNewLine _
                                            & "	    AND  SYS_UPD_TIME  = @SYS_UPD_TIME	   " & vbNewLine _
                                            & "	    AND  SYS_UPD_PGID  = @SYS_UPD_PGID	   " & vbNewLine _
                                            & "	    AND  SYS_UPD_USER  = @SYS_UPD_USER	   "


    ''' <summary>
    ''' 届先詳細マスタ（追加）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
#If False Then  'UPD 2022/03/31 028346
    Private Const SQL_INSERT_MDEST_DETAILS As String = "	INSERT INTO LM_MST..M_DEST_DETAILS	 " & vbNewLine _
     & "	SELECT @NRS_BR_CD	 " & vbNewLine _
     & "	      ,@NRS_CUST_CD	 " & vbNewLine _
     & "	      ,DEST_CD	     " & vbNewLine _
     & "	      ,DEST_CD_EDA	 " & vbNewLine _
     & "	      ,SUB_KB	     " & vbNewLine _
     & "	      ,SET_NAIYO	 " & vbNewLine _
     & "	      ,REMARK	     " & vbNewLine _
     & "	      ,SYS_ENT_DATE	 " & vbNewLine _
     & "	      ,SYS_ENT_TIME	 " & vbNewLine _
     & "	      ,SYS_ENT_PGID	 " & vbNewLine _
     & "	      ,SYS_ENT_USER	 " & vbNewLine _
     & "	      ,@SYS_UPD_DATE	 " & vbNewLine _
     & "	      ,@SYS_UPD_TIME	 " & vbNewLine _
     & "	      ,@SYS_UPD_PGID	 " & vbNewLine _
     & "	      ,@SYS_UPD_USER	 " & vbNewLine _
     & "	      ,SYS_DEL_FLG	 " & vbNewLine _
     & "	  FROM LM_MST..M_DEST_DETAILS	 " & vbNewLine _
     & "	  WHERE NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
     & "	    AND CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
     & "--★追加★                                 " & vbNewLine _
     & " AND DEST_CD NOT IN                        " & vbNewLine _
     & " (                                         " & vbNewLine _
     & "  SELECT DEST_CD FROM LM_MST..M_DEST       " & vbNewLine _
     & "  WHERE NRS_BR_CD = @NRS_BR_CD             " & vbNewLine _
     & "  AND CUST_CD_L   = @NRS_CUST_CD           " & vbNewLine _
     & "  )                                        " & vbNewLine

#Else
    Private Const SQL_INSERT_WK_RTDEST_DETAILS As String = "INSERT INTO LM_MST..WK_RT_DEST_DETAILS	  " & vbNewLine _
                                                         & "	SELECT M_DEST_DETAILS.*	              " & vbNewLine _
                                                         & "	  FROM LM_MST..M_DEST_DETAILS	      " & vbNewLine _
                                                         & "	  WHERE NRS_BR_CD = @PC_BR_CD	      " & vbNewLine _
                                                         & "	    AND CUST_CD_L = @PC_CUST_CD	      " & vbNewLine _
                                                         & "--★追加★                                        " & vbNewLine _
                                                         & "        AND DEST_CD NOT IN                        " & vbNewLine _
                                                         & "         (                                        " & vbNewLine _
                                                         & "          SELECT DEST_CD FROM LM_MST..M_DEST      " & vbNewLine _
                                                         & "            WHERE NRS_BR_CD = @NRS_BR_CD          " & vbNewLine _
                                                         & "              And CUST_CD_L   = @NRS_CUST_CD      " & vbNewLine _
                                                         & "         )                                        " & vbNewLine

#End If

    ''' <summary>
    ''' WK_RT届先詳細マスタ（更新）
    ''' </summary>
    ''' <remarks></remarks>SQL_UPDATE_WK_RTDEST_DETAILS
    ''' 
    Private Const SQL_UPDATE_WK_RTDEST_DETAILS As String = "UPDATE LM_MST..WK_RT_DEST_DETAILS	     " & vbNewLine _
                                                     & "	  SET  NRS_BR_CD    = @NRS_BR_CD	 " & vbNewLine _
                                                     & "	      ,CUST_CD_L    = @NRS_CUST_CD	 " & vbNewLine _
                                                     & "	      ,SYS_UPD_DATE = @SYS_UPD_DATE	 " & vbNewLine _
                                                     & "	      ,SYS_UPD_TIME = @SYS_UPD_TIME	 " & vbNewLine _
                                                     & "	      ,SYS_UPD_PGID = @SYS_UPD_PGID	 " & vbNewLine _
                                                     & "	      ,SYS_UPD_USER = @SYS_UPD_USER	 " & vbNewLine _
                                                     & "	  WHERE NRS_BR_CD = @PC_BR_CD	     " & vbNewLine _
                                                     & "	    AND CUST_CD_L = @PC_CUST_CD	     "

    ''' <summary>
    ''' 届先詳細マスタ（追加））
    ''' </summary>
    ''' <remarks></remarks>
    '''
    Private Const SQL_INSERT_M_DEST_DETAILS As String = "INSERT LM_MST..M_DEST_DETAILS	 " & vbNewLine _
                                                      & "	SELECT WK_RT_DEST_DETAILS.*               " & vbNewLine _
                                                      & "	  FROM LM_MST..WK_RT_DEST_DETAILS	       " & vbNewLine _
                                                      & "	  WHERE  SYS_UPD_DATE  = @SYS_UPD_DATE	   " & vbNewLine _
                                                      & "	    AND  SYS_UPD_TIME  = @SYS_UPD_TIME	   " & vbNewLine _
                                                      & "	    AND  SYS_UPD_PGID  = @SYS_UPD_PGID	   " & vbNewLine _
                                                      & "	    AND  SYS_UPD_USER  = @SYS_UPD_USER	   "

#End Region

#Region "入荷関連"

    ''' <summary>
    ''' 入荷L（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_INKA_L As String = "DELETE $NRS_TRN_NM$..B_INKA_L" & vbNewLine _
     & "	WHERE                        " & vbNewLine _
     & "	INKA_NO_L In	             " & vbNewLine _
     & "	(	                         " & vbNewLine _
     & "	Select INKA_NO_L FROM	     " & vbNewLine _
     & "	$PC_TRN_NM$..B_INKA_L	     " & vbNewLine _
     & "	WHERE	                     " & vbNewLine _
     & "	    NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
     & "	And CUST_CD_L = @PC_CUST_CD  " & vbNewLine _
     & "    )                            "


    ''' <summary>
    ''' 入荷M（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_INKA_M As String = "DELETE $NRS_TRN_NM$..B_INKA_M	 " & vbNewLine _
     & "	WHERE	                     " & vbNewLine _
     & "	INKA_NO_L In	             " & vbNewLine _
     & "	(	                         " & vbNewLine _
     & "	Select INKA_NO_L FROM	     " & vbNewLine _
     & "	$PC_TRN_NM$..B_INKA_L	     " & vbNewLine _
     & "	WHERE	                     " & vbNewLine _
     & "	    NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
     & "	And CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
     & "    )                            "



    ''' <summary>
    ''' 入荷S（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_INKA_S As String = "DELETE $NRS_TRN_NM$..B_INKA_S	 " & vbNewLine _
     & "	WHERE	                     " & vbNewLine _
     & "	INKA_NO_L In	             " & vbNewLine _
     & "	(	                         " & vbNewLine _
     & "	Select INKA_NO_L FROM	     " & vbNewLine _
     & "	$PC_TRN_NM$..B_INKA_L	     " & vbNewLine _
     & "	WHERE	                     " & vbNewLine _
     & "	    NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
     & "	And CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
     & "    )                            "


    ''' <summary>
    ''' 入荷L（作成）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
#If False Then  'UPD 2022/03/18 028346
        Private Const SQL_INSERT_INKA_L As String = "INSERT INTO $NRS_TRN_NM$..B_INKA_L	 " & vbNewLine _
     & "	Select @NRS_BR_CD	         " & vbNewLine _
     & "	      ,INKA_NO_L	         " & vbNewLine _
     & "	      ,FURI_NO	             " & vbNewLine _
     & "	      ,INKA_TP	             " & vbNewLine _
     & "	      ,INKA_KB	             " & vbNewLine _
     & "	      ,INKA_STATE_KB	     " & vbNewLine _
     & "	      ,INKA_DATE	         " & vbNewLine _
     & "	      ,STORAGE_DUE_DATE	     " & vbNewLine _
     & "	      ,@NRS_WH_CD	         " & vbNewLine _
     & "	      ,@NRS_CUST_CD          " & vbNewLine _
     & "	      ,CUST_CD_M	         " & vbNewLine _
     & "	      ,INKA_PLAN_QT	         " & vbNewLine _
     & "	      ,INKA_PLAN_QT_UT	     " & vbNewLine _
     & "	      ,INKA_TTL_NB	         " & vbNewLine _
     & "	      ,BUYER_ORD_NO_L	     " & vbNewLine _
     & "	      ,OUTKA_FROM_ORD_NO_L	 " & vbNewLine _
     & "	      ,SEIQTO_CD	         " & vbNewLine _
     & "	      ,TOUKI_HOKAN_YN	     " & vbNewLine _
     & "	      ,HOKAN_YN	             " & vbNewLine _
     & "	      ,HOKAN_FREE_KIKAN	     " & vbNewLine _
     & "	      ,HOKAN_STR_DATE	     " & vbNewLine _
     & "	      ,NIYAKU_YN	         " & vbNewLine _
     & "	      ,TAX_KB	             " & vbNewLine _
     & "	      ,REMARK	             " & vbNewLine _
     & "	      ,REMARK_OUT	         " & vbNewLine _
     & "	      ,CHECKLIST_PRT_DATE	 " & vbNewLine _
     & "	      ,CHECKLIST_PRT_USER	 " & vbNewLine _
     & "	      ,UKETSUKELIST_PRT_DATE  " & vbNewLine _
     & "	      ,UKETSUKELIST_PRT_USER  " & vbNewLine _
     & "	      ,UKETSUKE_DATE	   " & vbNewLine _
     & "	      ,UKETSUKE_USER	   " & vbNewLine _
     & "	      ,KEN_DATE	           " & vbNewLine _
     & "	      ,KEN_USER	           " & vbNewLine _
     & "	      ,INKO_DATE           " & vbNewLine _
     & "	      ,INKO_USER	       " & vbNewLine _
     & "	      ,HOUKOKUSYO_PR_DATE  " & vbNewLine _
     & "	      ,HOUKOKUSYO_PR_USER  " & vbNewLine _
     & "	      ,UNCHIN_TP	       " & vbNewLine _
     & "	      ,UNCHIN_KB	       " & vbNewLine _
     & "	      ,WH_KENPIN_WK_STATUS " & vbNewLine _
     & "	      ,WH_TAB_STATUS	   " & vbNewLine _
     & "	      ,WH_TAB_YN	       " & vbNewLine _
     & "	      ,WH_TAB_IMP_YN	   " & vbNewLine _
     & "	      ,STOP_ALLOC	       " & vbNewLine _
     & "	      ,WH_TAB_NO_SIJI_FLG  " & vbNewLine _
     & "	      ,SYS_ENT_DATE	       " & vbNewLine _
     & "	      ,SYS_ENT_TIME	       " & vbNewLine _
     & "	      ,SYS_ENT_PGID	       " & vbNewLine _
     & "	      ,SYS_ENT_USER        " & vbNewLine _
     & "	      ,@SYS_UPD_DATE	   " & vbNewLine _
     & "	      ,@SYS_UPD_TIME	   " & vbNewLine _
     & "	      ,@SYS_UPD_PGID	   " & vbNewLine _
     & "	      ,@SYS_UPD_USER	   " & vbNewLine _
     & "	      ,SYS_DEL_FLG	         " & vbNewLine _
     & "	FROM  $PC_TRN_NM$..B_INKA_L	 " & vbNewLine _
     & "	WHERE	                     " & vbNewLine _
     & "	    NRS_BR_CD = @PC_BR_CD	     " & vbNewLine _
     & "	And CUST_CD_L = @PC_CUST_CD	     " & vbNewLine

#Else
    Private Const SQL_INSERT_INKA_L As String = "INSERT INTO $NRS_TRN_NM$..B_INKA_L	 " & vbNewLine _
                                                 & "	Select *                     " & vbNewLine _
                                                 & "	FROM  $PC_TRN_NM$..B_INKA_L	 " & vbNewLine _
                                                 & "	WHERE	                     " & vbNewLine _
                                                 & "	    NRS_BR_CD = @PC_BR_CD	     " & vbNewLine _
                                                 & "	And CUST_CD_L = @PC_CUST_CD	     " & vbNewLine

#End If

    ''' <summary>
    ''' 入荷L（更新）
    ''' </summary>
    ''' <remarks></remarks>
    '''
    Private Const SQL_UPDATE_INKA_L As String = "UPDATE $NRS_TRN_NM$..B_INKA_L	      " & vbNewLine _
                                             & "   Set NRS_BR_CD     = @NRS_BR_CD     " & vbNewLine _
                                             & "　　　,WH_CD         = @NRS_WH_CD	  " & vbNewLine _
                                             & "　　　,CUST_CD_L     = @NRS_CUST_CD   " & vbNewLine _
                                             & "      ,SYS_UPD_DATE  = @SYS_UPD_DATE  " & vbNewLine _
                                             & "      ,SYS_UPD_TIME  = @SYS_UPD_TIME  " & vbNewLine _
                                             & "      ,SYS_UPD_PGID  = @SYS_UPD_PGID  " & vbNewLine _
                                             & "      ,SYS_UPD_USER  = @SYS_UPD_USER  " & vbNewLine _
                                             & "  WHERE NRS_BR_CD = @PC_BR_CD         " & vbNewLine
    ''' <summary>
    ''' 入荷M（作成）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
#If False Then  'UPD 2022/03/18 028346
    Private Const SQL_INSERT_INKA_M As String = "INSERT INTO $NRS_TRN_NM$..B_INKA_M	 " & vbNewLine _
     & "	Select @NRS_BR_CD	 " & vbNewLine _
     & "	      ,INKA_NO_L	 " & vbNewLine _
     & "	      ,INKA_NO_M	 " & vbNewLine _
     & "	      ,GOODS_CD_NRS	 " & vbNewLine _
     & "	      ,OUTKA_FROM_ORD_NO_M	 " & vbNewLine _
     & "	      ,BUYER_ORD_NO_M	 " & vbNewLine _
     & "	      ,REMARK	     " & vbNewLine _
     & "	      ,PRINT_SORT	 " & vbNewLine _
     & "	      ,SYS_ENT_DATE	 " & vbNewLine _
     & "	      ,SYS_ENT_TIME	 " & vbNewLine _
     & "	      ,SYS_ENT_PGID	 " & vbNewLine _
     & "	      ,SYS_ENT_USER	 " & vbNewLine _
     & "	      ,@SYS_UPD_DATE	 " & vbNewLine _
     & "	      ,@SYS_UPD_TIME	 " & vbNewLine _
     & "	      ,@SYS_UPD_PGID	 " & vbNewLine _
     & "	      ,@SYS_UPD_USER	 " & vbNewLine _
     & "	      ,SYS_DEL_FLG	     " & vbNewLine _
     & "	FROM	                 " & vbNewLine _
     & "	$PC_TRN_NM$..B_INKA_M	 " & vbNewLine _
     & "	WHERE          	         " & vbNewLine _
     & "	INKA_NO_L In	         " & vbNewLine _
     & "	(	                     " & vbNewLine _
     & "	Select INKA_NO_L FROM	 " & vbNewLine _
     & "	$PC_TRN_NM$..B_INKA_L	 " & vbNewLine _
     & "	WHERE	                 " & vbNewLine _
     & "	     NRS_BR_CD = @PC_BR_CD	     " & vbNewLine _
     & "	 And CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
     & "	)	                             " & vbNewLine

#Else
    Private Const SQL_INSERT_INKA_M As String = "INSERT INTO $NRS_TRN_NM$..B_INKA_M	 " & vbNewLine _
     & "	Select *                 " & vbNewLine _
     & "	FROM	                 " & vbNewLine _
     & "	$PC_TRN_NM$..B_INKA_M	 " & vbNewLine _
     & "	WHERE          	         " & vbNewLine _
     & "	INKA_NO_L In	         " & vbNewLine _
     & "	(	                     " & vbNewLine _
     & "	Select INKA_NO_L FROM	 " & vbNewLine _
     & "	$PC_TRN_NM$..B_INKA_L	 " & vbNewLine _
     & "	WHERE	                 " & vbNewLine _
     & "	     NRS_BR_CD = @PC_BR_CD	     " & vbNewLine _
     & "	 And CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
     & "	)	                             " & vbNewLine

#End If

    ''' <summary>
    ''' 入荷M（更新）
    ''' </summary>
    ''' <remarks></remarks>
    '''
    Private Const SQL_UPDATE_INKA_M As String = "UPDATE $NRS_TRN_NM$..B_INKA_M	      " & vbNewLine _
                                             & "   Set NRS_BR_CD     = @NRS_BR_CD     " & vbNewLine _
                                             & "      ,SYS_UPD_DATE  = @SYS_UPD_DATE  " & vbNewLine _
                                             & "      ,SYS_UPD_TIME  = @SYS_UPD_TIME  " & vbNewLine _
                                             & "      ,SYS_UPD_PGID  = @SYS_UPD_PGID  " & vbNewLine _
                                             & "      ,SYS_UPD_USER  = @SYS_UPD_USER  " & vbNewLine _
                                             & "  WHERE NRS_BR_CD = @PC_BR_CD         " & vbNewLine _
                                             & "	AND INKA_NO_L In	                 " & vbNewLine _
                                             & "	    (	                             " & vbNewLine _
                                             & "	     Select INKA_NO_L                " & vbNewLine _
                                             & "	       FROM $PC_TRN_NM$..B_INKA_L	 " & vbNewLine _
                                             & "	       WHERE	                     " & vbNewLine _
                                             & "	             NRS_BR_CD = @PC_BR_CD    " & vbNewLine _
                                             & "	         And CUST_CD_L = @PC_CUST_CD " & vbNewLine _
                                             & "	    )	                             " & vbNewLine



    ''' <summary>
    ''' 入荷S（作成）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
#If False Then  'UPD 2022/03/18 028346
    Private Const SQL_INSERT_INKA_S As String = "INSERT INTO $NRS_TRN_NM$..B_INKA_S	 " & vbNewLine _
     & "	Select @NRS_BR_CD	 " & vbNewLine _
     & "	      ,INKA_NO_L	 " & vbNewLine _
     & "	      ,INKA_NO_M	 " & vbNewLine _
     & "	      ,INKA_NO_S	 " & vbNewLine _
     & "	      ,ZAI_REC_NO	 " & vbNewLine _
     & "	      ,LOT_NO	 " & vbNewLine _
     & "	      ,LOCA	     " & vbNewLine _
     & "	      ,@NRS_TOU_NO	 " & vbNewLine _
     & "	      ,@NRS_SITU_NO	 " & vbNewLine _
     & "	      ,@NRS_ZONE_CD	 " & vbNewLine _
     & "	      ,KONSU	 " & vbNewLine _
     & "	      ,HASU	     " & vbNewLine _
     & "	      ,IRIME	 " & vbNewLine _
     & "	      ,BETU_WT	 " & vbNewLine _
     & "	      ,SERIAL_NO	 " & vbNewLine _
     & "	      ,GOODS_COND_KB_1	 " & vbNewLine _
     & "	      ,GOODS_COND_KB_2	 " & vbNewLine _
     & "	      ,GOODS_COND_KB_3	 " & vbNewLine _
     & "	      ,GOODS_CRT_DATE	 " & vbNewLine _
     & "	      ,LT_DATE	 " & vbNewLine _
     & "	      ,SPD_KB	 " & vbNewLine _
     & "	      ,OFB_KB	 " & vbNewLine _
     & "	      ,DEST_CD	 " & vbNewLine _
     & "	      ,REMARK	 " & vbNewLine _
     & "	      ,ALLOC_PRIORITY	 " & vbNewLine _
     & "	      ,REMARK_OUT	 " & vbNewLine _
     & "	      ,BUG_YN	     " & vbNewLine _
     & "	      ,SYS_ENT_DATE	 " & vbNewLine _
     & "	      ,SYS_ENT_TIME	 " & vbNewLine _
     & "	      ,SYS_ENT_PGID	 " & vbNewLine _
     & "	      ,SYS_ENT_USER	 " & vbNewLine _
     & "	      ,@SYS_UPD_DATE	 " & vbNewLine _
     & "	      ,@SYS_UPD_TIME	 " & vbNewLine _
     & "	      ,@SYS_UPD_PGID	 " & vbNewLine _
     & "	      ,@SYS_UPD_USER	 " & vbNewLine _
     & "	      ,SYS_DEL_FLG	             " & vbNewLine _
     & "	FROM	                         " & vbNewLine _
     & "	$PC_TRN_NM$..B_INKA_S	         " & vbNewLine _
     & "	WHERE	                         " & vbNewLine _
     & "	INKA_NO_L In	                 " & vbNewLine _
     & "	(	                             " & vbNewLine _
     & "	Select INKA_NO_L FROM	         " & vbNewLine _
     & "	$PC_TRN_NM$..B_INKA_L	             " & vbNewLine _
     & "	WHERE	                         " & vbNewLine _
     & "	     NRS_BR_CD = @PC_BR_CD	     " & vbNewLine _
     & "	 And CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
     & "	)	                             " & vbNewLine

#Else
    Private Const SQL_INSERT_INKA_S As String = "INSERT INTO $NRS_TRN_NM$..B_INKA_S	 " & vbNewLine _
                                            & "	Select *	                     " & vbNewLine _
                                            & "	FROM	                         " & vbNewLine _
                                            & "	$PC_TRN_NM$..B_INKA_S	         " & vbNewLine _
                                            & "	WHERE	                         " & vbNewLine _
                                            & "	INKA_NO_L In	                 " & vbNewLine _
                                            & "	(	                             " & vbNewLine _
                                            & "	Select INKA_NO_L FROM	         " & vbNewLine _
                                            & "	$PC_TRN_NM$..B_INKA_L	             " & vbNewLine _
                                            & "	WHERE	                         " & vbNewLine _
                                            & "	     NRS_BR_CD = @PC_BR_CD	     " & vbNewLine _
                                            & "	 And CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
                                            & "	)	                             " & vbNewLine

#End If

    ''' <summary>
    ''' 入荷S（更新）
    ''' </summary>
    ''' <remarks></remarks>
    '''
    Private Const SQL_UPDATE_INKA_S As String = "UPDATE $NRS_TRN_NM$..B_INKA_S	      " & vbNewLine _
                                             & "   Set NRS_BR_CD     = @NRS_BR_CD     " & vbNewLine _
                                             & "      ,TOU_NO        = @NRS_TOU_NO	  " & vbNewLine _
                                             & "      ,SITU_NO       = @NRS_SITU_NO	  " & vbNewLine _
                                             & "      ,ZONE_CD       = @NRS_ZONE_CD	  " & vbNewLine _
                                             & "      ,SYS_UPD_DATE  = @SYS_UPD_DATE  " & vbNewLine _
                                             & "      ,SYS_UPD_TIME  = @SYS_UPD_TIME  " & vbNewLine _
                                             & "      ,SYS_UPD_PGID  = @SYS_UPD_PGID  " & vbNewLine _
                                             & "      ,SYS_UPD_USER  = @SYS_UPD_USER  " & vbNewLine _
                                             & "  WHERE NRS_BR_CD = @PC_BR_CD         " & vbNewLine _
                                             & "	AND INKA_NO_L In	                 " & vbNewLine _
                                             & "	    (	                             " & vbNewLine _
                                             & "	     Select INKA_NO_L                " & vbNewLine _
                                             & "	       FROM $PC_TRN_NM$..B_INKA_L	 " & vbNewLine _
                                             & "	       WHERE	                     " & vbNewLine _
                                             & "	             NRS_BR_CD = @PC_BR_CD   " & vbNewLine _
                                             & "	         And CUST_CD_L = @PC_CUST_CD " & vbNewLine _
                                             & "	    )	                             " & vbNewLine


#End Region

#Region "出荷関連"

    ''' <summary>
    ''' 出荷L（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_OUTKA_L As String = "DELETE $NRS_TRN_NM$..C_OUTKA_L" & vbNewLine _
     & "	WHERE                        " & vbNewLine _
     & "	OUTKA_NO_L In	             " & vbNewLine _
     & "	(	                         " & vbNewLine _
     & "	Select OUTKA_NO_L FROM	     " & vbNewLine _
     & "	$PC_TRN_NM$..C_OUTKA_L	 " & vbNewLine _
     & "	WHERE	                     " & vbNewLine _
     & "	    NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
     & "	And CUST_CD_L = @PC_CUST_CD  " & vbNewLine _
     & "    )                            "


    ''' <summary>
    ''' 出荷M(削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_OUTKA_M As String = "DELETE $NRS_TRN_NM$..C_OUTKA_M	 " & vbNewLine _
     & "	WHERE	                     " & vbNewLine _
     & "	OUTKA_NO_L In	             " & vbNewLine _
     & "	(	                         " & vbNewLine _
     & "	Select OUTKA_NO_L FROM	     " & vbNewLine _
     & "	$PC_TRN_NM$..C_OUTKA_L	     " & vbNewLine _
     & "	WHERE	                     " & vbNewLine _
     & "	    NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
     & "	And CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
     & "    )                            "


    ''' <summary>
    ''' 出荷S（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_OUTKA_S As String = "DELETE $NRS_TRN_NM$..C_OUTKA_S	 " & vbNewLine _
     & "	WHERE	                     " & vbNewLine _
     & "	OUTKA_NO_L In	             " & vbNewLine _
     & "	(	                         " & vbNewLine _
     & "	Select OUTKA_NO_L FROM	     " & vbNewLine _
     & "	$PC_TRN_NM$..C_OUTKA_L	     " & vbNewLine _
     & "	WHERE	                     " & vbNewLine _
     & "	    NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
     & "	And CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
     & "    )                            "

    ''' <summary>
    ''' 出荷L（作成）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
#If False Then  'UPD 2022/03/18 028346
    Private Const SQL_INSERT_OUTKA_L As String = "INSERT INTO $NRS_TRN_NM$..C_OUTKA_L	 " & vbNewLine _
     & "	Select @NRS_BR_CD	 " & vbNewLine _
     & "	      , OUTKA_NO_L	 " & vbNewLine _
     & "	      ,FURI_NO	 " & vbNewLine _
     & "	      ,OUTKA_KB	 " & vbNewLine _
     & "	      ,SYUBETU_KB	 " & vbNewLine _
     & "	      ,OUTKA_STATE_KB	 " & vbNewLine _
     & "	      ,OUTKAHOKOKU_YN	 " & vbNewLine _
     & "	      ,PICK_KB	 " & vbNewLine _
     & "	      ,DENP_NO	 " & vbNewLine _
     & "	      ,ARR_KANRYO_INFO	 " & vbNewLine _
     & "	      ,@NRS_WH_CD	 " & vbNewLine _
     & "	      ,OUTKA_PLAN_DATE	 " & vbNewLine _
     & "	      ,OUTKO_DATE	 " & vbNewLine _
     & "	      ,ARR_PLAN_DATE	 " & vbNewLine _
     & "	      ,ARR_PLAN_TIME	 " & vbNewLine _
     & "	      ,HOKOKU_DATE	 " & vbNewLine _
     & "	      ,TOUKI_HOKAN_YN	 " & vbNewLine _
     & "	      ,END_DATE	 " & vbNewLine _
     & "	      ,@NRS_CUST_CD	 " & vbNewLine _
     & "	      ,CUST_CD_M	 " & vbNewLine _
     & "	      ,SHIP_CD_L	 " & vbNewLine _
     & "	      ,SHIP_CD_M	 " & vbNewLine _
     & "	      ,DEST_CD	 " & vbNewLine _
     & "	      ,DEST_AD_3	 " & vbNewLine _
     & "	      ,DEST_TEL	 " & vbNewLine _
     & "	      ,NHS_REMARK	 " & vbNewLine _
     & "	      ,SP_NHS_KB	 " & vbNewLine _
     & "	      ,COA_YN	 " & vbNewLine _
     & "	      ,CUST_ORD_NO	 " & vbNewLine _
     & "	      ,BUYER_ORD_NO	 " & vbNewLine _
     & "	      ,REMARK	 " & vbNewLine _
     & "	      ,OUTKA_PKG_NB	 " & vbNewLine _
     & "	      ,DENP_YN	 " & vbNewLine _
     & "	      ,PC_KB	 " & vbNewLine _
     & "	      ,NIYAKU_YN	 " & vbNewLine _
     & "	      ,DEST_KB	 " & vbNewLine _
     & "	      ,DEST_NM	 " & vbNewLine _
     & "	      ,DEST_AD_1	 " & vbNewLine _
     & "	      ,DEST_AD_2	 " & vbNewLine _
     & "	      ,ALL_PRINT_FLAG	 " & vbNewLine _
     & "	      ,NIHUDA_FLAG	 " & vbNewLine _
     & "	      ,NHS_FLAG	 " & vbNewLine _
     & "	      ,DENP_FLAG	 " & vbNewLine _
     & "	      ,COA_FLAG	 " & vbNewLine _
     & "	      ,HOKOKU_FLAG	 " & vbNewLine _
     & "	      ,MATOME_PICK_FLAG	 " & vbNewLine _
     & "	      ,MATOME_PRINT_DATE	 " & vbNewLine _
     & "	      ,MATOME_PRINT_TIME	 " & vbNewLine _
     & "	      ,LAST_PRINT_DATE	 " & vbNewLine _
     & "	      ,LAST_PRINT_TIME	 " & vbNewLine _
     & "	      ,SASZ_USER	 " & vbNewLine _
     & "	      ,OUTKO_USER	 " & vbNewLine _
     & "	      ,KEN_USER	 " & vbNewLine _
     & "	      ,OUTKA_USER	 " & vbNewLine _
     & "	      ,HOU_USER	 " & vbNewLine _
     & "	      ,ORDER_TYPE	 " & vbNewLine _
     & "	      ,WH_KENPIN_WK_STATUS	 " & vbNewLine _
     & "	      ,WH_TAB_STATUS	 " & vbNewLine _
     & "	      ,WH_TAB_YN	 " & vbNewLine _
     & "	      ,URGENT_YN	 " & vbNewLine _
     & "	      ,WH_SIJI_REMARK	 " & vbNewLine _
     & "	      ,WH_TAB_NO_SIJI_FLG	 " & vbNewLine _
     & "	      ,WH_TAB_HOKOKU_YN	 " & vbNewLine _
     & "	      ,WH_TAB_HOKOKU	 " & vbNewLine _
     & "	      ,SYS_ENT_DATE	 " & vbNewLine _
     & "	      ,SYS_ENT_TIME	 " & vbNewLine _
     & "	      ,SYS_ENT_PGID	 " & vbNewLine _
     & "	      ,SYS_ENT_USER	 " & vbNewLine _
     & "	      ,@SYS_UPD_DATE	 " & vbNewLine _
     & "	      ,@SYS_UPD_TIME	 " & vbNewLine _
     & "	      ,@SYS_UPD_PGID	 " & vbNewLine _
     & "	      ,@SYS_UPD_USER	 " & vbNewLine _
     & "	      ,SYS_DEL_FLG	 " & vbNewLine _
     & "	FROM  $PC_TRN_NM$..C_OUTKA_L	 " & vbNewLine _
     & "	WHERE	                         " & vbNewLine _
     & "	    NRS_BR_CD = @PC_BR_CD	     " & vbNewLine _
     & "	And CUST_CD_L = @PC_CUST_CD	     "

#Else
    Private Const SQL_INSERT_OUTKA_L As String = "INSERT INTO $NRS_TRN_NM$..C_OUTKA_L	 " & vbNewLine _
     & "	Select *	                     " & vbNewLine _
     & "	FROM  $PC_TRN_NM$..C_OUTKA_L	 " & vbNewLine _
     & "	WHERE	                         " & vbNewLine _
     & "	    NRS_BR_CD = @PC_BR_CD	     " & vbNewLine _
     & "	And CUST_CD_L = @PC_CUST_CD	     "

#End If

    ''' <summary>
    ''' 出荷L（更新）
    ''' </summary>
    ''' <remarks></remarks>
    '''
    Private Const SQL_UPDATE_OUTKA_L As String = "UPDATE $NRS_TRN_NM$..C_OUTKA_L	      " & vbNewLine _
                                             & "   Set NRS_BR_CD     = @NRS_BR_CD     " & vbNewLine _
                                             & "      ,WH_CD         = @NRS_WH_CD	  " & vbNewLine _
                                             & "      ,CUST_CD_L     = @NRS_CUST_CD   " & vbNewLine _
                                             & "      ,SYS_UPD_DATE  = @SYS_UPD_DATE  " & vbNewLine _
                                             & "      ,SYS_UPD_TIME  = @SYS_UPD_TIME  " & vbNewLine _
                                             & "      ,SYS_UPD_PGID  = @SYS_UPD_PGID  " & vbNewLine _
                                             & "      ,SYS_UPD_USER  = @SYS_UPD_USER  " & vbNewLine _
                                             & "  WHERE NRS_BR_CD = @PC_BR_CD         " & vbNewLine _
                                             & "	AND CUST_CD_L = @PC_CUST_CD	     "

    ''' <summary>
    ''' 出荷M（作成）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
#If False Then  'UPD 2022/03/18 028346
    Private Const SQL_INSERT_OUTKA_M As String = "	INSERT INTO $NRS_TRN_NM$..C_OUTKA_M	 " & vbNewLine _
     & "	Select @NRS_BR_CD	 " & vbNewLine _
     & "	      ,OUTKA_NO_L	 " & vbNewLine _
     & "	      ,OUTKA_NO_M	 " & vbNewLine _
     & "	      ,EDI_SET_NO	 " & vbNewLine _
     & "	      ,COA_YN	 " & vbNewLine _
     & "	      ,CUST_ORD_NO_DTL	 " & vbNewLine _
     & "	      ,BUYER_ORD_NO_DTL	 " & vbNewLine _
     & "	      ,GOODS_CD_NRS	 " & vbNewLine _
     & "	      ,RSV_NO	 " & vbNewLine _
     & "	      ,LOT_NO	 " & vbNewLine _
     & "	      ,SERIAL_NO	 " & vbNewLine _
     & "	      ,ALCTD_KB	 " & vbNewLine _
     & "	      ,OUTKA_PKG_NB	 " & vbNewLine _
     & "	      ,OUTKA_HASU	 " & vbNewLine _
     & "	      ,OUTKA_QT	 " & vbNewLine _
     & "	      ,OUTKA_TTL_NB	 " & vbNewLine _
     & "	      ,OUTKA_TTL_QT	 " & vbNewLine _
     & "	      ,ALCTD_NB	 " & vbNewLine _
     & "	      ,ALCTD_QT	 " & vbNewLine _
     & "	      ,BACKLOG_NB	 " & vbNewLine _
     & "	      ,BACKLOG_QT	 " & vbNewLine _
     & "	      ,UNSO_ONDO_KB	 " & vbNewLine _
     & "	      ,IRIME	 " & vbNewLine _
     & "	      ,IRIME_UT	 " & vbNewLine _
     & "	      ,OUTKA_M_PKG_NB	 " & vbNewLine _
     & "	      ,REMARK	 " & vbNewLine _
     & "	      ,SIZE_KB	 " & vbNewLine _
     & "	      ,ZAIKO_KB	 " & vbNewLine _
     & "	      ,SOURCE_CD	 " & vbNewLine _
     & "	      ,YELLOW_CARD	 " & vbNewLine _
     & "	      ,GOODS_CD_NRS_FROM	 " & vbNewLine _
     & "	      ,PRINT_SORT	 " & vbNewLine _
     & "	      ,SYS_ENT_DATE	 " & vbNewLine _
     & "	      ,SYS_ENT_TIME	 " & vbNewLine _
     & "	      ,SYS_ENT_PGID	 " & vbNewLine _
     & "	      ,SYS_ENT_USER	 " & vbNewLine _
     & "	      ,@SYS_UPD_DATE	 " & vbNewLine _
     & "	      ,@SYS_UPD_TIME	 " & vbNewLine _
     & "	      ,@SYS_UPD_PGID	 " & vbNewLine _
     & "	      ,@SYS_UPD_USER	 " & vbNewLine _
     & "	      ,SYS_DEL_FLG	 " & vbNewLine _
     & "	FROM  $PC_TRN_NM$..C_OUTKA_M	 " & vbNewLine _
     & "	WHERE	                     " & vbNewLine _
     & "	OUTKA_NO_L In	             " & vbNewLine _
     & "	(	                         " & vbNewLine _
     & "	Select OUTKA_NO_L FROM	     " & vbNewLine _
     & "	$PC_TRN_NM$..C_OUTKA_L	     " & vbNewLine _
     & "	WHERE	                     " & vbNewLine _
     & "	    NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
     & "	And CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
     & "    )                            "

#Else
    Private Const SQL_INSERT_OUTKA_M As String = "	INSERT INTO $NRS_TRN_NM$..C_OUTKA_M	 " & vbNewLine _
                                                 & "	Select *	 　　　　　       " & vbNewLine _
                                                 & "	FROM  $PC_TRN_NM$..C_OUTKA_M  " & vbNewLine _
                                                 & "	WHERE	                      " & vbNewLine _
                                                 & "	OUTKA_NO_L In	              " & vbNewLine _
                                                 & "	(	                          " & vbNewLine _
                                                 & "	Select OUTKA_NO_L FROM	      " & vbNewLine _
                                                 & "	$PC_TRN_NM$..C_OUTKA_L	      " & vbNewLine _
                                                 & "	WHERE	                      " & vbNewLine _
                                                 & "	    NRS_BR_CD = @PC_BR_CD	  " & vbNewLine _
                                                 & "	And CUST_CD_L = @PC_CUST_CD	  " & vbNewLine _
                                                 & "    )                             "

#End If

    ''' <summary>
    ''' 出荷M（更新）
    ''' </summary>
    ''' <remarks></remarks>
    '''
    Private Const SQL_UPDATE_OUTKA_M As String = "UPDATE $NRS_TRN_NM$..C_OUTKA_M	      " & vbNewLine _
                                             & "   Set NRS_BR_CD     = @NRS_BR_CD     " & vbNewLine _
                                             & "      ,SYS_UPD_DATE  = @SYS_UPD_DATE  " & vbNewLine _
                                             & "      ,SYS_UPD_TIME  = @SYS_UPD_TIME  " & vbNewLine _
                                             & "      ,SYS_UPD_PGID  = @SYS_UPD_PGID  " & vbNewLine _
                                             & "	WHERE	                      " & vbNewLine _
                                             & "	OUTKA_NO_L In	              " & vbNewLine _
                                             & "	(	                          " & vbNewLine _
                                             & "	Select OUTKA_NO_L FROM	      " & vbNewLine _
                                             & "	$PC_TRN_NM$..C_OUTKA_L	      " & vbNewLine _
                                             & "	WHERE	                      " & vbNewLine _
                                             & "	    NRS_BR_CD = @PC_BR_CD	  " & vbNewLine _
                                             & "	And CUST_CD_L = @PC_CUST_CD	  " & vbNewLine _
                                             & "    )                             "


    ''' <summary>
    ''' 出荷S（作成）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
#If False Then  'UPD 2022/03/24 028346
    Private Const SQL_INSERT_OUTKA_S As String = "	INSERT INTO $NRS_TRN_NM$..C_OUTKA_S	 " & vbNewLine _
     & "	Select @NRS_BR_CD	 " & vbNewLine _
     & "	      ,OUTKA_NO_L	 " & vbNewLine _
     & "	      ,OUTKA_NO_M	 " & vbNewLine _
     & "	      ,OUTKA_NO_S	 " & vbNewLine _
     & "	      ,@NRS_TOU_NO	 " & vbNewLine _
     & "	      ,@NRS_SITU_NO	 " & vbNewLine _
     & "	      ,@NRS_ZONE_CD	 " & vbNewLine _
     & "	      ,LOCA	         " & vbNewLine _
     & "	      ,LOT_NO	     " & vbNewLine _
     & "	      ,SERIAL_NO	 " & vbNewLine _
     & "	      ,OUTKA_TTL_NB	 " & vbNewLine _
     & "	      ,OUTKA_TTL_QT	 " & vbNewLine _
     & "	      ,ZAI_REC_NO	 " & vbNewLine _
     & "	      ,INKA_NO_L	 " & vbNewLine _
     & "	      ,INKA_NO_M	 " & vbNewLine _
     & "	      ,INKA_NO_S	 " & vbNewLine _
     & "	      ,ZAI_UPD_FLAG	 " & vbNewLine _
     & "	      ,ALCTD_CAN_NB	 " & vbNewLine _
     & "	      ,ALCTD_NB	 " & vbNewLine _
     & "	      ,ALCTD_CAN_QT	 " & vbNewLine _
     & "	      ,ALCTD_QT	 " & vbNewLine _
     & "	      ,IRIME	 " & vbNewLine _
     & "	      ,BETU_WT	 " & vbNewLine _
     & "	      ,COA_FLAG	 " & vbNewLine _
     & "	      ,REMARK	 " & vbNewLine _
     & "	      ,SMPL_FLAG	 " & vbNewLine _
     & "	      ,REC_NO	 " & vbNewLine _
     & "	      ,SYS_ENT_DATE	 " & vbNewLine _
     & "	      ,SYS_ENT_TIME	 " & vbNewLine _
     & "	      ,SYS_ENT_PGID	 " & vbNewLine _
     & "	      ,SYS_ENT_USER	 " & vbNewLine _
     & "	      ,@SYS_UPD_DATE	 " & vbNewLine _
     & "	      ,@SYS_UPD_TIME	 " & vbNewLine _
     & "	      ,@SYS_UPD_PGID	 " & vbNewLine _
     & "	      ,@SYS_UPD_USER	 " & vbNewLine _
     & "	      ,SYS_DEL_FLG	 " & vbNewLine _
     & "	FROM  $PC_TRN_NM$..C_OUTKA_S " & vbNewLine _
     & "	WHERE	                     " & vbNewLine _
     & "	OUTKA_NO_L In	             " & vbNewLine _
     & "	(	                         " & vbNewLine _
     & "	Select OUTKA_NO_L FROM	     " & vbNewLine _
     & "	$PC_TRN_NM$..C_OUTKA_L	     " & vbNewLine _
     & "	WHERE	                     " & vbNewLine _
     & "	    NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
     & "	And CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
     & "    )                            "
#Else
    Private Const SQL_INSERT_OUTKA_S As String = "	INSERT INTO $NRS_TRN_NM$..C_OUTKA_S	 " & vbNewLine _
     & "	Select *             " & vbNewLine _
     & "	FROM  $PC_TRN_NM$..C_OUTKA_S " & vbNewLine _
     & "	WHERE	                     " & vbNewLine _
     & "	OUTKA_NO_L In	             " & vbNewLine _
     & "	(	                         " & vbNewLine _
     & "	Select OUTKA_NO_L FROM	     " & vbNewLine _
     & "	$PC_TRN_NM$..C_OUTKA_L	     " & vbNewLine _
     & "	WHERE	                     " & vbNewLine _
     & "	    NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
     & "	And CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
     & "    )                            "
#End If


    ''' <summary>
    ''' 出荷M（更新）
    ''' </summary>
    ''' <remarks></remarks>
    '''
    Private Const SQL_UPDATE_OUTKA_S As String = "UPDATE $NRS_TRN_NM$..C_OUTKA_S	      " & vbNewLine _
                                             & "   Set NRS_BR_CD     = @NRS_BR_CD     " & vbNewLine _
                                             & "	   ,TOU_NO       = @NRS_TOU_NO	  " & vbNewLine _
                                             & "     　,SITU_NO      = @NRS_SITU_NO	  " & vbNewLine _
                                             & "	   ,ZONE_CD      = @NRS_ZONE_CD	  " & vbNewLine _
                                             & "      ,SYS_UPD_DATE  = @SYS_UPD_DATE  " & vbNewLine _
                                             & "      ,SYS_UPD_TIME  = @SYS_UPD_TIME  " & vbNewLine _
                                             & "      ,SYS_UPD_PGID  = @SYS_UPD_PGID  " & vbNewLine _
                                             & "	WHERE	                      " & vbNewLine _
                                             & "	OUTKA_NO_L In	              " & vbNewLine _
                                             & "	(	                          " & vbNewLine _
                                             & "	Select OUTKA_NO_L FROM	      " & vbNewLine _
                                             & "	$PC_TRN_NM$..C_OUTKA_L	      " & vbNewLine _
                                             & "	WHERE	                      " & vbNewLine _
                                             & "	    NRS_BR_CD = @PC_BR_CD	  " & vbNewLine _
                                             & "	And CUST_CD_L = @PC_CUST_CD	  " & vbNewLine _
                                             & "    )                             "

#End Region

#Region "D_ZAI_TRS関連"

    ''' <summary>
    ''' D_ZAI_TRS（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_ZAI_TRS As String = "DELETE $NRS_TRN_NM$..D_ZAI_TRS	 " & vbNewLine _
     & "--	WHERE NRS_BR_CD = @NRS_BR_CD   " & vbNewLine _
     & "--	  And CUST_CD_L = @NRS_CUST_CD " & vbNewLine _
     & "	WHERE ZAI_REC_NO In            " & vbNewLine _
     & "	(	                           " & vbNewLine _
      & "	Select ZAI_REC_NO              " & vbNewLine _
     & "	FROM  $PC_TRN_NM$..D_ZAI_TRS   " & vbNewLine _
     & "	WHERE NRS_BR_CD = @PC_BR_CD	   " & vbNewLine _
     & "	  And CUST_CD_L = @PC_CUST_CD  " & vbNewLine _
     & "	)	                           "

    ''' <summary>
    ''' D_ZAI_TRS（作成）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
#If False Then  'UPD 2022/03/24 028346
    Private Const SQL_INSERT_ZAI_TRS As String = "INSERT INTO $NRS_TRN_NM$..D_ZAI_TRS	 " & vbNewLine _
     & "	Select @NRS_BR_CD	 " & vbNewLine _
     & "	      ,ZAI_REC_NO	 " & vbNewLine _
     & "	      ,@NRS_WH_CD	 " & vbNewLine _
     & "	      ,@NRS_TOU_NO	 " & vbNewLine _
     & "	      ,@NRS_SITU_NO	 " & vbNewLine _
     & "	      ,@NRS_ZONE_CD	 " & vbNewLine _
     & "	      ,LOCA	         " & vbNewLine _
     & "	      ,LOT_NO	     " & vbNewLine _
     & "	      ,@NRS_CUST_CD	 " & vbNewLine _
     & "	      ,CUST_CD_M	 " & vbNewLine _
     & "	      ,GOODS_CD_NRS	 " & vbNewLine _
     & "	      ,GOODS_KANRI_NO	 " & vbNewLine _
     & "	      ,INKA_NO_L	 " & vbNewLine _
     & "	      ,INKA_NO_M	 " & vbNewLine _
     & "	      ,INKA_NO_S	 " & vbNewLine _
     & "	      ,ALLOC_PRIORITY	 " & vbNewLine _
     & "	      ,RSV_NO	 " & vbNewLine _
     & "	      ,SERIAL_NO	 " & vbNewLine _
     & "	      ,HOKAN_YN	 " & vbNewLine _
     & "	      ,TAX_KB	 " & vbNewLine _
     & "	      ,GOODS_COND_KB_1	 " & vbNewLine _
     & "	      ,GOODS_COND_KB_2	 " & vbNewLine _
     & "	      ,GOODS_COND_KB_3	 " & vbNewLine _
     & "	      ,OFB_KB	 " & vbNewLine _
     & "	      ,SPD_KB	 " & vbNewLine _
     & "	      ,REMARK_OUT	 " & vbNewLine _
     & "	      ,PORA_ZAI_NB	 " & vbNewLine _
     & "	      ,ALCTD_NB	 " & vbNewLine _
     & "	      ,ALLOC_CAN_NB	 " & vbNewLine _
     & "	      ,IRIME	 " & vbNewLine _
     & "	      ,PORA_ZAI_QT	 " & vbNewLine _
     & "	      ,ALCTD_QT	 " & vbNewLine _
     & "	      ,ALLOC_CAN_QT	 " & vbNewLine _
     & "	      ,INKO_DATE	 " & vbNewLine _
     & "	      ,INKO_PLAN_DATE	 " & vbNewLine _
     & "	      ,ZERO_FLAG	 " & vbNewLine _
     & "	      ,LT_DATE	 " & vbNewLine _
     & "	      ,GOODS_CRT_DATE	 " & vbNewLine _
     & "	      ,DEST_CD_P	 " & vbNewLine _
     & "	      ,REMARK	 " & vbNewLine _
     & "	      ,SMPL_FLAG	 " & vbNewLine _
     & "	      ,SYS_ENT_DATE	 " & vbNewLine _
     & "	      ,SYS_ENT_TIME	 " & vbNewLine _
     & "	      ,SYS_ENT_PGID	 " & vbNewLine _
     & "	      ,SYS_ENT_USER	 " & vbNewLine _
     & "	      ,@SYS_UPD_DATE	 " & vbNewLine _
     & "	      ,@SYS_UPD_TIME	 " & vbNewLine _
     & "	      ,@SYS_UPD_PGID	 " & vbNewLine _
     & "	      ,@SYS_UPD_USER	 " & vbNewLine _
     & "	      ,SYS_DEL_FLG	 " & vbNewLine _
     & "	FROM $PC_TRN_NM$..D_ZAI_TRS   " & vbNewLine _
     & "	WHERE NRS_BR_CD = @PC_BR_CD	   " & vbNewLine _
     & "	  And CUST_CD_L = @PC_CUST_CD  " & vbNewLine _

#Else
    Private Const SQL_INSERT_ZAI_TRS As String = "INSERT INTO $NRS_TRN_NM$..D_ZAI_TRS	 " & vbNewLine _
                                             & "	Select *             " & vbNewLine _
                                             & "	FROM $PC_TRN_NM$..D_ZAI_TRS   " & vbNewLine _
                                             & "	WHERE NRS_BR_CD = @PC_BR_CD	   " & vbNewLine _
                                             & "	  And CUST_CD_L = @PC_CUST_CD  " & vbNewLine _

#End If

    ''' <summary>
    ''' D_ZAI_TRS（更新）
    ''' </summary>
    ''' <remarks></remarks>
    '''
    Private Const SQL_UPDATE_ZAI_TRS As String = "UPDATE $NRS_TRN_NM$..D_ZAI_TRS	      " & vbNewLine _
                                                 & "	Set NRS_BR_CD     = @NRS_BR_CD	  " & vbNewLine _
                                                 & "	   ,WH_CD         = @NRS_WH_CD	  " & vbNewLine _
                                                 & "	   ,TOU_NO        = @NRS_TOU_NO	  " & vbNewLine _
                                                 & "	   ,SITU_NO       = @NRS_SITU_NO  " & vbNewLine _
                                                 & "	   ,ZONE_CD       = @NRS_ZONE_CD  " & vbNewLine _
                                                 & "	   ,CUST_CD_L     = @NRS_CUST_CD  " & vbNewLine _
                                                 & "	   ,SYS_UPD_DATE  = @SYS_UPD_DATE " & vbNewLine _
                                                 & "	   ,SYS_UPD_TIME  = @SYS_UPD_TIME " & vbNewLine _
                                                 & "	   ,SYS_UPD_PGID  = @SYS_UPD_PGID " & vbNewLine _
                                                 & "	   ,SYS_UPD_USER  = @SYS_UPD_USER " & vbNewLine _
                                                 & "	WHERE NRS_BR_CD = @PC_BR_CD	      " & vbNewLine _
                                                 & "	  And CUST_CD_L = @PC_CUST_CD     " & vbNewLine _

#End Region

#Region "運送関連"

    ''' <summary>
    ''' F_UNSO_LL（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_UNSO_LL As String = "DELETE $NRS_TRN_NM$..F_UNSO_LL	 " & vbNewLine _
     & "	WHERE NRS_BR_CD = @NRS_BR_CD  " & vbNewLine _
     & "	And TRIP_NO IN	              " & vbNewLine _
     & "	(	                          " & vbNewLine _
     & "	SELECT TRIP_NO	              " & vbNewLine _
     & "	FROM $PC_TRN_NM$..F_UNSO_L	  " & vbNewLine _
     & "	WHERE NRS_BR_CD = @PC_BR_CD   " & vbNewLine _
     & "	  And CUST_CD_L = @PC_CUST_CD " & vbNewLine _
     & "--	  And TRIP_NO <> ''           " & vbNewLine _
     & "	)	                           "


    ''' <summary>
    ''' F_UNSO_L（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_UNSO_L As String = "	DELETE $NRS_TRN_NM$..F_UNSO_L	 " & vbNewLine _
     & "	WHERE NRS_BR_CD = @NRS_BR_CD	 " & vbNewLine _
     & "	  AND CUST_CD_L = @NRS_CUST_CD	 " & vbNewLine _
     & "	  AND UNSO_NO_L IN	             " & vbNewLine _
     & "	(	                             " & vbNewLine _
     & "	SELECT UNSO_NO_L	             " & vbNewLine _
     & "	FROM $PC_TRN_NM$..F_UNSO_L       " & vbNewLine _
     & "	WHERE NRS_BR_CD = @PC_BR_CD	    " & vbNewLine _
     & "	  AND CUST_CD_L = @PC_CUST_CD   " & vbNewLine _
     & "	)	 "


    ''' <summary>
    ''' F_UNSO_M（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_UNSO_M As String = "DELETE $NRS_TRN_NM$..F_UNSO_M	 " & vbNewLine _
     & "	WHERE NRS_BR_CD = @NRS_BR_CD	 " & vbNewLine _
     & "	AND UNSO_NO_L IN	    " & vbNewLine _
     & "	(	                    " & vbNewLine _
     & "	SELECT UNSO_NO_L	    " & vbNewLine _
     & "	FROM $PC_TRN_NM$..F_UNSO_L " & vbNewLine _
     & "	WHERE NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
     & "      AND CUST_CD_L = @PC_CUST_CD " & vbNewLine _
     & "	)	 "


    ''' <summary>
    ''' UNSO_LL（作成）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
#If False Then  'UPD 2022/03/24 028346
    Private Const SQL_INSERT_UNSO_LL As String = "	INSERT INTO $NRS_TRN_NM$..F_UNSO_LL	 " & vbNewLine _
     & "	SELECT @NRS_BR_CD	 " & vbNewLine _
     & "	      ,F_UNSO_LL.TRIP_NO	 " & vbNewLine _
     & "	      ,F_UNSO_LL.UNSOCO_CD	 " & vbNewLine _
     & "	      ,F_UNSO_LL.UNSOCO_BR_CD	 " & vbNewLine _
     & "	      ,F_UNSO_LL.JSHA_KB	 " & vbNewLine _
     & "	      ,F_UNSO_LL.BIN_KB	 " & vbNewLine _
     & "	      ,F_UNSO_LL.CAR_KEY	 " & vbNewLine _
     & "	      ,F_UNSO_LL.UNSO_ONDO	 " & vbNewLine _
     & "	      ,F_UNSO_LL.DRIVER_CD	 " & vbNewLine _
     & "	      ,F_UNSO_LL.TRIP_DATE	 " & vbNewLine _
     & "	      ,F_UNSO_LL.PAY_UNCHIN	 " & vbNewLine _
     & "	      ,F_UNSO_LL.PAY_TARIFF_CD " & vbNewLine _
     & "	      ,F_UNSO_LL.HAISO_KB	 " & vbNewLine _
     & "	      ,F_UNSO_LL.REMARK	 " & vbNewLine _
     & "	      ,F_UNSO_LL.SHIHARAI_TARIFF_CD	 " & vbNewLine _
     & "	      ,F_UNSO_LL.SHIHARAI_ETARIFF_CD	 " & vbNewLine _
     & "	      ,F_UNSO_LL.SHIHARAI_UNSO_WT	 " & vbNewLine _
     & "	      ,F_UNSO_LL.SHIHARAI_COUNT	 " & vbNewLine _
     & "	      ,F_UNSO_LL.SHIHARAI_UNCHIN	 " & vbNewLine _
     & "	      ,F_UNSO_LL.SHIHARAI_TARIFF_BUNRUI_KB	 " & vbNewLine _
     & "	      ,F_UNSO_LL.SYS_ENT_DATE	 " & vbNewLine _
     & "	      ,F_UNSO_LL.SYS_ENT_TIME	 " & vbNewLine _
     & "	      ,F_UNSO_LL.SYS_ENT_PGID	 " & vbNewLine _
     & "	      ,F_UNSO_LL.SYS_ENT_USER	 " & vbNewLine _
     & "	      ,@SYS_UPD_DATE	 " & vbNewLine _
     & "	      ,@SYS_UPD_TIME	 " & vbNewLine _
     & "	      ,@SYS_UPD_PGID	 " & vbNewLine _
     & "	      ,@SYS_UPD_USER	 " & vbNewLine _
     & "	      ,F_UNSO_LL.SYS_DEL_FLG	 " & vbNewLine _
     & "	FROM $PC_TRN_NM$..F_UNSO_LL	 " & vbNewLine _
     & "	LEFT JOIN $PC_TRN_NM$..F_UNSO_L	 " & vbNewLine _
     & "	  ON F_UNSO_L.NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
     & "	 AND F_UNSO_L.CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
     & "--	 AND F_UNSO_L.TRIP_NO <> ''	 " & vbNewLine _
     & "	WHERE F_UNSO_LL.TRIP_NO = F_UNSO_L.TRIP_NO	 " & vbNewLine

#Else
    Private Const SQL_INSERT_UNSO_LL As String = "	INSERT INTO $NRS_TRN_NM$..F_UNSO_LL	     " & vbNewLine _
                                            & "	SELECT F_UNSO_LL.*                           " & vbNewLine _
                                            & "	　FROM $PC_TRN_NM$..F_UNSO_LL	             " & vbNewLine _
                                            & "	　LEFT JOIN $PC_TRN_NM$..F_UNSO_L	         " & vbNewLine _
                                            & "	  　ON F_UNSO_L.NRS_BR_CD = @PC_BR_CD	     " & vbNewLine _
                                            & "    AND F_UNSO_L.CUST_CD_L = @PC_CUST_CD	     " & vbNewLine _
                                            & "--	 AND F_UNSO_L.TRIP_NO <> ''	             " & vbNewLine _
                                            & "   WHERE F_UNSO_LL.TRIP_NO = F_UNSO_L.TRIP_NO " & vbNewLine

#End If

    ''' <summary>
    ''' UNSO_LL（更新）
    ''' </summary>
    ''' <remarks></remarks>
    '''
    Private Const SQL_UPDATE_UNSO_LL As String = "UPDATE $NRS_TRN_NM$..F_UNSO_LL	         " & vbNewLine _
                                             & "   SET NRS_BR_CD            = @NRS_BR_CD     " & vbNewLine _
                                             & "      ,SYS_UPD_DATE         = @SYS_UPD_DATE  " & vbNewLine _
                                             & "      ,SYS_UPD_TIME         = @SYS_UPD_TIME  " & vbNewLine _
                                             & "      ,SYS_UPD_PGID         = @SYS_UPD_PGID  " & vbNewLine _
                                             & "      ,SYS_UPD_USER         = @SYS_UPD_USER  " & vbNewLine _
                                             & "	WHERE NRS_BR_CD         = @PC_BR_CD	     "


    ''' <summary>
    ''' UNSO_L（作成）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
#If False Then  'UPD 2022/03/24 028346
    Private Const SQL_INSERT_UNSO_L As String = "INSERT INTO $NRS_TRN_NM$..F_UNSO_L	 " & vbNewLine _
     & "	SELECT @NRS_BR_CD	 " & vbNewLine _
     & "	      ,UNSO_NO_L	 " & vbNewLine _
     & "	      ,YUSO_BR_CD	 " & vbNewLine _
     & "	      ,INOUTKA_NO_L	 " & vbNewLine _
     & "	      ,TRIP_NO	 " & vbNewLine _
     & "	      ,UNSO_CD	 " & vbNewLine _
     & "	      ,UNSO_BR_CD	 " & vbNewLine _
     & "	      ,BIN_KB	 " & vbNewLine _
     & "	      ,JIYU_KB	 " & vbNewLine _
     & "	      ,DENP_NO	 " & vbNewLine _
     & "	      ,AUTO_DENP_KBN	 " & vbNewLine _
     & "	      ,AUTO_DENP_NO	 " & vbNewLine _
     & "	      ,OUTKA_PLAN_DATE	 " & vbNewLine _
     & "	      ,OUTKA_PLAN_TIME	 " & vbNewLine _
     & "	      ,ARR_PLAN_DATE	 " & vbNewLine _
     & "	      ,ARR_PLAN_TIME	 " & vbNewLine _
     & "	      ,ARR_ACT_TIME	 " & vbNewLine _
     & "	      ,@NRS_CUST_CD	 " & vbNewLine _
     & "	      ,CUST_CD_M	 " & vbNewLine _
     & "	      ,CUST_REF_NO	 " & vbNewLine _
     & "	      ,SHIP_CD	 " & vbNewLine _
     & "	      ,ORIG_CD	 " & vbNewLine _
     & "	      ,DEST_CD	 " & vbNewLine _
     & "	      ,UNSO_PKG_NB	 " & vbNewLine _
     & "	      ,NB_UT	 " & vbNewLine _
     & "	      ,UNSO_WT	 " & vbNewLine _
     & "	      ,UNSO_ONDO_KB	 " & vbNewLine _
     & "	      ,PC_KB	 " & vbNewLine _
     & "	      ,TARIFF_BUNRUI_KB	 " & vbNewLine _
     & "	      ,VCLE_KB	 " & vbNewLine _
     & "	      ,MOTO_DATA_KB	 " & vbNewLine _
     & "	      ,TAX_KB	 " & vbNewLine _
     & "	      ,REMARK	 " & vbNewLine _
     & "	      ,''  --SEIQ_TARIFF_CD	 " & vbNewLine _
     & "	      ,''  --SEIQ_ETARIFF_CD	 " & vbNewLine _
     & "	      ,AD_3	 " & vbNewLine _
     & "	      ,UNSO_TEHAI_KB	 " & vbNewLine _
     & "	      ,BUY_CHU_NO	 " & vbNewLine _
     & "	      ,AREA_CD	 " & vbNewLine _
     & "	      ,TYUKEI_HAISO_FLG	 " & vbNewLine _
     & "	      ,SYUKA_TYUKEI_CD	 " & vbNewLine _
     & "	      ,HAIKA_TYUKEI_CD	 " & vbNewLine _
     & "	      ,TRIP_NO_SYUKA	 " & vbNewLine _
     & "	      ,TRIP_NO_TYUKEI	 " & vbNewLine _
     & "	      ,TRIP_NO_HAIKA	 " & vbNewLine _
     & "	      ,''  --SHIHARAI_TARIFF_CD	 " & vbNewLine _
     & "	      ,''  --SHIHARAI_ETARIFF_CD	 " & vbNewLine _
     & "	      ,MAIN_DELI_KB	 " & vbNewLine _
     & "	      ,NHS_REMARK	 " & vbNewLine _
     & "	      ,SYS_ENT_DATE	 " & vbNewLine _
     & "	      ,SYS_ENT_TIME	 " & vbNewLine _
     & "	      ,SYS_ENT_PGID	 " & vbNewLine _
     & "	      ,SYS_ENT_USER	 " & vbNewLine _
     & "	      ,@SYS_UPD_DATE	 " & vbNewLine _
     & "	      ,@SYS_UPD_TIME	 " & vbNewLine _
     & "	      ,@SYS_UPD_PGID	 " & vbNewLine _
     & "	      ,@SYS_UPD_USER	 " & vbNewLine _
     & "	      ,SYS_DEL_FLG	 " & vbNewLine _
     & "	FROM $PC_TRN_NM$..F_UNSO_L	 " & vbNewLine _
     & "	WHERE NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
     & "	AND CUST_CD_L = @PC_CUST_CD	 " & vbNewLine

#Else
    Private Const SQL_INSERT_UNSO_L As String = "INSERT INTO $NRS_TRN_NM$..F_UNSO_L	 " & vbNewLine _
     & "	SELECT *             " & vbNewLine _
     & "	FROM $PC_TRN_NM$..F_UNSO_L	 " & vbNewLine _
     & "	WHERE NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
     & "	AND CUST_CD_L = @PC_CUST_CD	 " & vbNewLine

#End If



    ''' <summary>
    ''' UNSO_L（更新）
    ''' </summary>
    ''' <remarks></remarks>
    '''
    Private Const SQL_UPDATE_UNSO_L As String = "UPDATE $NRS_TRN_NM$..F_UNSO_L	      " & vbNewLine _
                                             & "   SET NRS_BR_CD     = @NRS_BR_CD     " & vbNewLine _
                                             & "      ,CUST_CD_L            = @NRS_CUST_CD " & vbNewLine _
                                             & "      ,SEIQ_TARIFF_CD       = ''      " & vbNewLine _
                                             & "      ,SEIQ_ETARIFF_CD      = ''      " & vbNewLine _
                                             & "      ,SHIHARAI_TARIFF_CD   = ''      " & vbNewLine _
                                             & "      ,SHIHARAI_ETARIFF_CD  = ''      " & vbNewLine _
                                             & "      ,SYS_UPD_DATE  = @SYS_UPD_DATE  " & vbNewLine _
                                             & "      ,SYS_UPD_TIME  = @SYS_UPD_TIME  " & vbNewLine _
                                             & "      ,SYS_UPD_PGID  = @SYS_UPD_PGID  " & vbNewLine _
                                             & "      ,SYS_UPD_USER  = @SYS_UPD_USER  " & vbNewLine _
                                             & "	WHERE NRS_BR_CD  = @PC_BR_CD	  " & vbNewLine _
                                             & "	   AND CUST_CD_L = @PC_CUST_CD	  " & vbNewLine


    ''' <summary>
    ''' UNSO_M（作成）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
#If False Then  'UPD 2022/03/24 028346
    ' UPD 2022/03/17 028346 【LMS】昭栄物流_協力会社管理機能で異常終了(本対策)
    ' & "	SELECT @NRS_BR_CD	 " & vbNewLine _
    ' & "	      ,UNSO_NO_L	 " & vbNewLine _
    ' & "	      ,UNSO_NO_M	 " & vbNewLine _
    ' & "	      ,GOODS_CD_NRS	 " & vbNewLine _
    ' & "	      ,GOODS_NM	 " & vbNewLine _
    '& "	      ,UNSO_TTL_NB	 " & vbNewLine _
    ' & "	      ,NB_UT	 " & vbNewLine _
    ' & "	      ,UNSO_TTL_QT	 " & vbNewLine _
    ' & "	      ,QT_UT	 " & vbNewLine _
    ' & "	      ,HASU	 " & vbNewLine _
    ' & "	      ,ZAI_REC_NO	 " & vbNewLine _
    ' & "	      ,UNSO_ONDO_KB	 " & vbNewLine _
    ' & "	      ,IRIME	 " & vbNewLine _
    ' & "	      ,IRIME_UT	 " & vbNewLine _
    ' & "	      ,BETU_WT	 " & vbNewLine _
    ' & "	      ,SIZE_KB	 " & vbNewLine _
    ' & "	      ,ZBUKA_CD	 " & vbNewLine _
    ' & "	      ,ABUKA_CD	 " & vbNewLine _
    ' & "	      ,PKG_NB	 " & vbNewLine _
    ' & "	      ,LOT_NO	 " & vbNewLine _
    ' & "	      ,REMARK	 " & vbNewLine _
    ' & "	      ,PRINT_SORT	 " & vbNewLine _
    ' & "	      ,UNSO_HOKEN_UM	--ADD 2022/03/14 028346    " & vbNewLine _
    ' & "	      ,KITAKU_GOODS_UP  --ADD 2022/03/14 028346	 " & vbNewLine _
    ' & "	      ,SYS_ENT_DATE	 " & vbNewLine _
    ' & "	      ,SYS_ENT_TIME	 " & vbNewLine _
    ' & "	      ,SYS_ENT_PGID	 " & vbNewLine _
    ' & "	      ,SYS_ENT_USER	 " & vbNewLine _
    ' & "	      ,@SYS_UPD_DATE	 " & vbNewLine _
    ' & "	      ,@SYS_UPD_TIME	 " & vbNewLine _
    ' & "	      ,@SYS_UPD_PGID	 " & vbNewLine _
    ' & "	      ,@SYS_UPD_USER	 " & vbNewLine _
    ' & "	      ,SYS_DEL_FLG	 " & vbNewLine _
    ' & "	FROM  $PC_TRN_NM$..F_UNSO_M	 " & vbNewLine _
    ' & "	WHERE NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
    ' & "	AND UNSO_NO_L IN	     " & vbNewLine _
    ' & "	(	                     " & vbNewLine _
    ' & "	SELECT UNSO_NO_L	     " & vbNewLine _
    ' & "	FROM $PC_TRN_NM$..F_UNSO_L " & vbNewLine _
    ' & "	WHERE NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
    ' & "	AND CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
    ' & "	)	                     " & vbNewLine _

#Else
    Private Const SQL_INSERT_UNSO_M As String = "INSERT INTO $NRS_TRN_NM$..F_UNSO_Ｍ	 " & vbNewLine _
                                             & "	SELECT *                     " & vbNewLine _
                                             & "	FROM  $PC_TRN_NM$..F_UNSO_M	 " & vbNewLine _
                                             & "	WHERE NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
                                             & "	AND UNSO_NO_L IN	         " & vbNewLine _
                                             & "	(	                         " & vbNewLine _
                                             & "	SELECT UNSO_NO_L	         " & vbNewLine _
                                             & "	FROM $PC_TRN_NM$..F_UNSO_L   " & vbNewLine _
                                             & "	WHERE NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
                                             & "	AND CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
                                             & "	)	                         " & vbNewLine _

#End If

    ''' <summary>
    ''' UNSO_M（更新）
    ''' </summary>
    ''' <remarks></remarks>
    '''
    Private Const SQL_UPDATE_UNSO_M As String = "UPDATE $NRS_TRN_NM$..F_UNSO_M	      " & vbNewLine _
                                             & "   SET NRS_BR_CD     = @NRS_BR_CD     " & vbNewLine _
                                             & "      ,SYS_UPD_DATE  = @SYS_UPD_DATE  " & vbNewLine _
                                             & "      ,SYS_UPD_TIME  = @SYS_UPD_TIME  " & vbNewLine _
                                             & "      ,SYS_UPD_PGID  = @SYS_UPD_PGID  " & vbNewLine _
                                             & "      ,SYS_UPD_USER  = @SYS_UPD_USER  " & vbNewLine _
                                             & "	WHERE NRS_BR_CD = @PC_BR_CD	            " & vbNewLine _
                                             & "      AND UNSO_NO_L IN	                    " & vbNewLine _
                                             & "         (	                                " & vbNewLine _
                                             & "          SELECT UNSO_NO_L	                " & vbNewLine _
                                             & "            FROM $PC_TRN_NM$..F_UNSO_L      " & vbNewLine _
                                             & "            WHERE NRS_BR_CD = @PC_BR_CD	    " & vbNewLine _
                                             & "              AND CUST_CD_L = @PC_CUST_CD	" & vbNewLine _
                                             & "         )	                                " & vbNewLine _

#End Region

#Region "F_UNCHIN_TRS関連"
    ''' <summary>
    ''' F_UNCHIN_TRS（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_UNCHIN_TRS As String = "DELETE $NRS_TRN_NM$..F_UNCHIN_TRS	 " & vbNewLine _
     & "	WHERE	                             " & vbNewLine _
     & "	UNSO_NO_L IN	                     " & vbNewLine _
     & "	(	                                 " & vbNewLine _
     & "	SELECT UNSO_NO_L FROM	             " & vbNewLine _
     & "	$PC_TRN_NM$..F_UNCHIN_TRS	         " & vbNewLine _
     & "	WHERE	                             " & vbNewLine _
     & "	    NRS_BR_CD = @PC_BR_CD	         " & vbNewLine _
     & "	AND CUST_CD_L = @PC_CUST_CD	         " & vbNewLine _
     & "	)	                                 " & vbNewLine _
     & "	AND SEIQ_FIXED_FLAG = '00' -- 未確定は削除してOk	" & vbNewLine
    ''' <summary>
    ''' F_UNCHIN_TRS（作成）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
#If False Then  'UPD2022/03/24 028346
    Private Const SQL_INSERT_UNCHIN_TRS As String = "INSERT INTO $NRS_TRN_NM$..F_UNCHIN_TRS	 " & vbNewLine _
         & "	SELECT @NRS_BR_CD  --YUSO_BR_CD	 " & vbNewLine _
         & "	      ,@NRS_BR_CD  --NRS_BR_CD	 " & vbNewLine _
         & "	      ,UNSO_NO_L	 " & vbNewLine _
         & "	      ,UNSO_NO_M	 " & vbNewLine _
         & "	      ,@NRS_CUST_CD  --CUST_CD_L	 " & vbNewLine _
         & "	      ,CUST_CD_M	 " & vbNewLine _
         & "	      ,CUST_CD_S	 " & vbNewLine _
         & "	      ,CUST_CD_SS	 " & vbNewLine _
         & "	      ,SEIQ_GROUP_NO	 " & vbNewLine _
         & "	      ,SEIQ_GROUP_NO_M	 " & vbNewLine _
         & "	      ,SEIQTO_CD	 " & vbNewLine _
         & "	      ,UNTIN_CALCULATION_KB	 " & vbNewLine _
         & "	      ,SEIQ_SYARYO_KB	 " & vbNewLine _
         & "	      ,SEIQ_PKG_UT	 " & vbNewLine _
         & "	      ,SEIQ_NG_NB	 " & vbNewLine _
         & "	      ,SEIQ_DANGER_KB	 " & vbNewLine _
         & "	      ,SEIQ_TARIFF_BUNRUI_KB	 " & vbNewLine _
         & "	      ,''  --SEIQ_TARIFF_CD	 " & vbNewLine _
         & "	      ,''  --SEIQ_ETARIFF_CD	 " & vbNewLine _
         & "	      ,SEIQ_KYORI	 " & vbNewLine _
         & "	      ,SEIQ_WT	 " & vbNewLine _
         & "	      ,0  --SEIQ_UNCHIN	 " & vbNewLine _
         & "	      ,SEIQ_CITY_EXTC	 " & vbNewLine _
         & "	      ,SEIQ_WINT_EXTC	 " & vbNewLine _
         & "	      ,SEIQ_RELY_EXTC	 " & vbNewLine _
         & "	      ,SEIQ_TOLL	 " & vbNewLine _
         & "	      ,SEIQ_INSU	 " & vbNewLine _
         & "	      ,'00'--SEIQ_FIXED_FLAG	 " & vbNewLine _
         & "	      ,DECI_NG_NB	 " & vbNewLine _
         & "	      ,DECI_KYORI	 " & vbNewLine _
         & "	      ,DECI_WT	 " & vbNewLine _
         & "	      ,0  --DECI_UNCHIN	 " & vbNewLine _
         & "	      ,0  --DECI_CITY_EXTC	 " & vbNewLine _
         & "	      ,0  --DECI_WINT_EXTC	 " & vbNewLine _
         & "	      ,0  --DECI_RELY_EXTC	 " & vbNewLine _
         & "	      ,0  --DECI_TOLL	 " & vbNewLine _
         & "	      ,0  --DECI_INSU	 " & vbNewLine _
         & "	      ,0  --KANRI_UNCHIN	 " & vbNewLine _
         & "	      ,0  --KANRI_CITY_EXTC	 " & vbNewLine _
         & "	      ,0  --KANRI_WINT_EXTC	 " & vbNewLine _
         & "	      ,0  --KANRI_RELY_EXTC	 " & vbNewLine _
         & "	      ,0  --KANRI_TOLL	 " & vbNewLine _
         & "	      ,0  --KANRI_INSU	 " & vbNewLine _
         & "	      ,REMARK	 " & vbNewLine _
         & "	      ,SIZE_KB	 " & vbNewLine _
         & "	      ,TAX_KB	 " & vbNewLine _
         & "	      ,SAGYO_KANRI	 " & vbNewLine _
         & "	      ,SYS_ENT_DATE	 " & vbNewLine _
         & "	      ,SYS_ENT_TIME	 " & vbNewLine _
         & "	      ,SYS_ENT_PGID	 " & vbNewLine _
         & "	      ,SYS_ENT_USER	 " & vbNewLine _
         & "	      ,@SYS_UPD_DATE	 " & vbNewLine _
         & "	      ,@SYS_UPD_TIME	 " & vbNewLine _
         & "	      ,@SYS_UPD_PGID	 " & vbNewLine _
         & "	      ,@SYS_UPD_USER	 " & vbNewLine _
         & "	      ,SYS_DEL_FLG	 " & vbNewLine _
         & "	FROM  $PC_TRN_NM$..F_UNCHIN_TRS	 " & vbNewLine _
         & "	WHERE NRS_BR_CD = @PC_BR_CD	  " & vbNewLine _
         & "	  AND CUST_CD_L = @PC_CUST_CD	  " & vbNewLine _
         & "	  AND UNSO_NO_L  NOT IN	  " & vbNewLine _
         & "	(	                     " & vbNewLine _
         & "	SELECT UNSO_NO_L	" & vbNewLine _
         & "	FROM $NRS_TRN_NM$..F_UNCHIN_TRS	" & vbNewLine _
         & "	WHERE NRS_BR_CD = @NRS_BR_CD	" & vbNewLine _
         & "	  AND CUST_CD_L = @NRS_CUST_CD	" & vbNewLine _
         & "	)	" & vbNewLine

#Else
    Private Const SQL_INSERT_UNCHIN_TRS As String = "INSERT INTO $NRS_TRN_NM$..F_UNCHIN_TRS	 " & vbNewLine _
                                                 & "	SELECT *             	         " & vbNewLine _
                                                 & "	FROM  $PC_TRN_NM$..F_UNCHIN_TRS	 " & vbNewLine _
                                                 & "	WHERE NRS_BR_CD = @PC_BR_CD	     " & vbNewLine _
                                                 & "	  AND CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
                                                 & "	  AND UNSO_NO_L  NOT IN	         " & vbNewLine _
                                                 & "	(	                             " & vbNewLine _
                                                 & "	SELECT UNSO_NO_L	             " & vbNewLine _
                                                 & "	FROM $NRS_TRN_NM$..F_UNCHIN_TRS	 " & vbNewLine _
                                                 & "	WHERE NRS_BR_CD = @NRS_BR_CD	 " & vbNewLine _
                                                 & "	  AND CUST_CD_L = @NRS_CUST_CD	 " & vbNewLine _
                                                 & "	)	                             " & vbNewLine

#End If

    ''' <summary>
    ''' F_UNCHIN_TRS（更新）
    ''' </summary>
    ''' <remarks></remarks>
    '''
    Private Const SQL_UPDATE_UNCHIN_TRS As String = "UPDATE $NRS_TRN_NM$..F_UNCHIN_TRS	      " & vbNewLine _
                                                 & "	  SET  YUSO_BR_CD  = @NRS_BR_CD  --YUSO_BR_CD	 " & vbNewLine _
                                                 & "	      ,NRS_BR_CD   = @NRS_BR_CD  --NRS_BR_CD	 " & vbNewLine _
                                                 & "	      ,CUST_CD_L   = @NRS_CUST_CD  --CUST_CD_L	 " & vbNewLine _
                                                 & "	      ,SEIQ_TARIFF_CD  = ''  --SEIQ_TARIFF_CD	 " & vbNewLine _
                                                 & "	      ,SEIQ_ETARIFF_CD =''  --SEIQ_ETARIFF_CD	 " & vbNewLine _
                                                 & "	      ,SEIQ_UNCHIN     = 0  --SEIQ_UNCHIN	 " & vbNewLine _
                                                 & "	      ,SEIQ_FIXED_FLAG = '00'--SEIQ_FIXED_FLAG	 " & vbNewLine _
                                                 & "	      ,DECI_UNCHIN     = 0  --DECI_UNCHIN	 " & vbNewLine _
                                                 & "	      ,DECI_CITY_EXTC  = 0  --DECI_CITY_EXTC	 " & vbNewLine _
                                                 & "	      ,DECI_WINT_EXTC  = 0  --DECI_WINT_EXTC	 " & vbNewLine _
                                                 & "	      ,DECI_RELY_EXTC  = 0  --DECI_RELY_EXTC	 " & vbNewLine _
                                                 & "	      ,DECI_TOLL       = 0  --DECI_TOLL	 " & vbNewLine _
                                                 & "	      ,DECI_INSU       = 0  --DECI_INSU	 " & vbNewLine _
                                                 & "	      ,KANRI_UNCHIN    = 0  --KANRI_UNCHIN	 " & vbNewLine _
                                                 & "	      ,KANRI_CITY_EXTC = 0  --KANRI_CITY_EXTC	 " & vbNewLine _
                                                 & "	      ,KANRI_WINT_EXTC = 0  --KANRI_WINT_EXTC	 " & vbNewLine _
                                                 & "	      ,KANRI_RELY_EXTC = 0  --KANRI_RELY_EXTC	 " & vbNewLine _
                                                 & "	      ,KANRI_TOLL      = 0  --KANRI_TOLL	   " & vbNewLine _
                                                 & "	      ,KANRI_INSU      = 0  --KANRI_INSU	   " & vbNewLine _
                                                 & "	      ,SYS_UPD_DATE    = @SYS_UPD_DATE	       " & vbNewLine _
                                                 & "	      ,SYS_UPD_TIME    = @SYS_UPD_TIME	       " & vbNewLine _
                                                 & "	      ,SYS_UPD_PGID    = @SYS_UPD_PGID	       " & vbNewLine _
                                                 & "	      ,SYS_UPD_USER    = @SYS_UPD_USER	       " & vbNewLine _
                                                 & "	WHERE NRS_BR_CD = @PC_BR_CD	     " & vbNewLine _
                                                 & "	  AND CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
                                                 & "	  AND UNSO_NO_L  IN	                     " & vbNewLine _
                                                 & "         (	                                 " & vbNewLine _
                                                 & "          SELECT UNSO_NO_L	                 " & vbNewLine _
                                                 & "            FROM $PC_TRN_NM$..F_UNCHIN_TRS	 " & vbNewLine _
                                                 & "            WHERE NRS_BR_CD = @PC_BR_CD	     " & vbNewLine _
                                                 & "              AND CUST_CD_L = @PC_CUST_CD	 " & vbNewLine _
                                                 & "         )	                                 " & vbNewLine

#End Region

#Region "_SAGYO関連"
    ''' <summary>
    ''' E_SAGYO（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_SAGYO As String = "DELETE $NRS_TRN_NM$..E_SAGYO	 " & vbNewLine _
                                        & "	WHERE NRS_BR_CD = @NRS_BR_CD	 " & vbNewLine _
                                        & "	  AND CUST_CD_L = @NRS_CUST_CD	 " & vbNewLine _
                                        & "   AND SKYU_CHK = '00' -- 未確定は削除してOk	 " & vbNewLine _
                                        & "	  AND SAGYO_REC_NO IN	     " & vbNewLine _
                                        & "	(	                     " & vbNewLine _
                                        & "	SELECT SAGYO_REC_NO	     " & vbNewLine _
                                        & "	FROM $PC_TRN_NM$..E_SAGYO	 " & vbNewLine _
                                        & "	WHERE NRS_BR_CD = @PC_BR_CD	 " & vbNewLine _
                                        & "	AND CUST_CD_L = @PC_CUST_CD  " & vbNewLine _
                                        & "	)	                     " & vbNewLine

    ''' <summary>
    ''' E_SAGYO（作成）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
#If False Then  'UPD2022/03/29 028346
    Private Const SQL_INSERT_SAGYO As String = "INSERT INTO $NRS_TRN_NM$..E_SAGYO	 " & vbNewLine _
                                    & "	SELECT 	 " & vbNewLine _
                                    & "	@NRS_BR_CD,	 " & vbNewLine _
                                    & "	ESAGYO.SAGYO_REC_NO,	 " & vbNewLine _
                                    & "	ESAGYO.SAGYO_COMP,	 " & vbNewLine _
                                    & "	'00',	--SKYU_CHK " & vbNewLine _
                                    & "	ESAGYO.SAGYO_SIJI_NO,	 " & vbNewLine _
                                    & "	ESAGYO.INOUTKA_NO_LM,	 " & vbNewLine _
                                    & "	@NRS_WH_CD,  --WH_CD,	 " & vbNewLine _
                                    & "	ESAGYO.IOZS_KB,	 " & vbNewLine _
                                    & "	MSAGYO.SAGYO_CD,	 " & vbNewLine _
                                    & "	MSAGYO.SAGYO_NM,	 " & vbNewLine _
                                    & "	@NRS_CUST_CD,  --CUST_CD_L,	 " & vbNewLine _
                                    & "	ESAGYO.CUST_CD_M,	 " & vbNewLine _
                                    & "	ESAGYO.DEST_CD,	 " & vbNewLine _
                                    & "	ESAGYO.DEST_NM,	 " & vbNewLine _
                                    & "	ESAGYO.GOODS_CD_NRS,	 " & vbNewLine _
                                    & "	ESAGYO.GOODS_NM_NRS,	 " & vbNewLine _
                                    & "	ESAGYO.LOT_NO,	 " & vbNewLine _
                                    & "	ESAGYO.INV_TANI,	 " & vbNewLine _
                                    & "	ESAGYO.SAGYO_NB,	 " & vbNewLine _
                                    & "	MSAGYO.SAGYO_UP,	 " & vbNewLine _
                                    & "	MSAGYO.SAGYO_UP * ESAGYO.SAGYO_NB,	 " & vbNewLine _
                                    & "	ESAGYO.TAX_KB,	 " & vbNewLine _
                                    & "	ESAGYO.SEIQTO_CD,	 " & vbNewLine _
                                    & "	ESAGYO.REMARK_ZAI,	 " & vbNewLine _
                                    & "	ESAGYO.REMARK_SKYU,	 " & vbNewLine _
                                    & "	ESAGYO.REMARK_SIJI,	 " & vbNewLine _
                                    & "	ESAGYO.SAGYO_COMP_CD,	 " & vbNewLine _
                                    & "	ESAGYO.SAGYO_COMP_DATE,	 " & vbNewLine _
                                    & "	ESAGYO.DEST_SAGYO_FLG,	 " & vbNewLine _
                                    & "	ESAGYO.ZAI_REC_NO,	 " & vbNewLine _
                                    & "	ESAGYO.PORA_ZAI_NB,	 " & vbNewLine _
                                    & "	ESAGYO.PORA_ZAI_QT,	 " & vbNewLine _
                                    & "	ESAGYO.SYS_ENT_DATE,	 " & vbNewLine _
                                    & "	ESAGYO.SYS_ENT_TIME,	 " & vbNewLine _
                                    & "	ESAGYO.SYS_ENT_PGID,	 " & vbNewLine _
                                    & "	ESAGYO.SYS_ENT_USER,	 " & vbNewLine _
                                    & "	@SYS_UPD_DATE,	 " & vbNewLine _
                                    & "	@SYS_UPD_TIME,	 " & vbNewLine _
                                    & "	@SYS_UPD_PGID,	 " & vbNewLine _
                                    & "	@SYS_UPD_USER,	 " & vbNewLine _
                                    & "	ESAGYO.SYS_DEL_FLG	 " & vbNewLine _
                                    & "	FROM $PC_TRN_NM$..E_SAGYO ESAGYO	 " & vbNewLine _
                                    & "	INNER JOIN LM_MST..M_SAGYO MSAGYO	 " & vbNewLine _
                                    & "	  ON ESAGYO.SAGYO_CD  = MSAGYO.SAGYO_CD_SUB	 " & vbNewLine _
                                    & "	 AND MSAGYO.NRS_BR_CD = @NRS_BR_CD	 " & vbNewLine _
                                    & "	 AND MSAGYO.CUST_CD_L = @NRS_CUST_CD	 " & vbNewLine _
                                    & "	And ESAGYO.SAGYO_REC_NO Not In	 " & vbNewLine _
                                    & "	(	                            " & vbNewLine _
                                    & "	Select SAGYO_REC_NO             " & vbNewLine _
                                    & "	FROM $NRS_TRN_NM$..E_SAGYO	 " & vbNewLine _
                                    & "	WHERE NRS_BR_CD = @NRS_BR_CD		 " & vbNewLine _
                                    & "	  And CUST_CD_L = @NRS_CUST_CD       " & vbNewLine _
                                    & "	  And SKYU_CHK = '01' -- 確定はINSERTしない	 " & vbNewLine _
                                    & "	)	                     " & vbNewLine

#Else
    Private Const SQL_INSERT_SAGYO As String = "INSERT INTO $NRS_TRN_NM$..E_SAGYO	 " & vbNewLine _
                                    & "	SELECT 	ESAGYO.* " & vbNewLine _
                                    & "	FROM $PC_TRN_NM$..E_SAGYO ESAGYO	 " & vbNewLine _
                                    & "	INNER JOIN LM_MST..M_SAGYO MSAGYO	 " & vbNewLine _
                                    & "	  ON ESAGYO.SAGYO_CD  = MSAGYO.SAGYO_CD_SUB	 " & vbNewLine _
                                    & "	 AND MSAGYO.NRS_BR_CD = @NRS_BR_CD	 " & vbNewLine _
                                    & "	 AND MSAGYO.CUST_CD_L = @NRS_CUST_CD	 " & vbNewLine _
                                    & "	And ESAGYO.SAGYO_REC_NO Not In	 " & vbNewLine _
                                    & "	(	                            " & vbNewLine _
                                    & "	Select SAGYO_REC_NO             " & vbNewLine _
                                    & "	FROM $NRS_TRN_NM$..E_SAGYO	 " & vbNewLine _
                                    & "	WHERE NRS_BR_CD = @NRS_BR_CD		 " & vbNewLine _
                                    & "	  And CUST_CD_L = @NRS_CUST_CD       " & vbNewLine _
                                    & "	  And SKYU_CHK = '01' -- 確定はINSERTしない	 " & vbNewLine _
                                    & "	)	                     " & vbNewLine

#End If

    ''' <summary>
    ''' E_SAGYO（更新）
    ''' </summary>
    ''' <remarks></remarks>
    '''
    Private Const SQL_UPDATE_SAGYO As String = "UPDATE $NRS_TRN_NM$..E_SAGYO	                                 " & vbNewLine _
                                                        & "	  SET  NRS_BR_CD       = @NRS_BR_CD    --NRS_BR_CD	 " & vbNewLine _
                                                        & "	      ,SKYU_CHK        = '00' 	       --SKYU_CHK    " & vbNewLine _
                                                        & "	      ,WH_CD           = @NRS_WH_CD    --WH_CD,	     " & vbNewLine _
                                                        & "	      ,CUST_CD_L       = @NRS_CUST_CD  --CUST_CD_L,	 " & vbNewLine _
                                                        & "	      ,SYS_UPD_DATE    = @SYS_UPD_DATE	       " & vbNewLine _
                                                        & "	      ,SYS_UPD_TIME    = @SYS_UPD_TIME	       " & vbNewLine _
                                                        & "	      ,SYS_UPD_PGID    = @SYS_UPD_PGID	       " & vbNewLine _
                                                        & "	      ,SYS_UPD_USER    = @SYS_UPD_USER	       " & vbNewLine _
                                                        & "	WHERE NRS_BR_CD = @PC_BR_CD		 " & vbNewLine _

#End Region

#Region "D_IDO_TRS関連"
    ''' <summary>
    ''' D_IDO_TRS（削除）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_D_IDO_TRS As String = "DELETE $NRS_TRN_NM$..D_IDO_TRS                       " & vbNewLine _
        & " WHERE O_ZAI_REC_NO  IN (                       " & vbNewLine _
        & "                         SELECT ZAI_REC_NO FROM $PC_TRN_NM$..D_ZAI_TRS  " & vbNewLine _
        & "                          WHERE NRS_BR_CD = @PC_BR_CD                    " & vbNewLine _
        & "                            AND CUST_CD_L = @PC_CUST_CD                  " & vbNewLine _
        & "                        )                                                " & vbNewLine

    ''' <summary>
    ''' D_IDO_TRN（作成）
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
#If False Then  'UPD2022/03/29 028346
    Private Const SQL_INSERT_D_IDO_TRS As String = "INSERT INTO $NRS_TRN_NM$..D_IDO_TRS	 " & vbNewLine _
         & "	SELECT @NRS_BR_CD	   " & vbNewLine _
         & "	     ,REC_NO           " & vbNewLine _
         & "	     ,IDO_DATE         " & vbNewLine _
         & "	     ,O_ZAI_REC_NO     " & vbNewLine _
         & "	     ,O_PORA_ZAI_NB    " & vbNewLine _
         & "	     ,O_ALCTD_NB       " & vbNewLine _
         & "	     ,O_ALLOC_CAN_NB   " & vbNewLine _
         & "	     ,O_IRIME          " & vbNewLine _
         & "	     ,N_ZAI_REC_NO     " & vbNewLine _
         & "	     ,N_PORA_ZAI_NB    " & vbNewLine _
         & "	     ,N_ALCTD_NB       " & vbNewLine _
         & "	     ,N_ALLOC_CAN_NB   " & vbNewLine _
         & "	     ,REMARK_KBN       " & vbNewLine _
         & "	     ,REMARK           " & vbNewLine _
         & "	     ,HOKOKU_DATE      " & vbNewLine _
         & "	     ,ZAIK_ZAN_FLG     " & vbNewLine _
         & "	     ,ZAIK_IRIME       " & vbNewLine _
         & "	     ,OUTKO_NO         " & vbNewLine _
         & "	     ,INKO_NO          " & vbNewLine _
         & "	     ,SYS_ENT_DATE     " & vbNewLine _
         & "	     ,SYS_ENT_TIME     " & vbNewLine _
         & "	     ,SYS_ENT_PGID     " & vbNewLine _
         & "	     ,SYS_ENT_USER     " & vbNewLine _
         & "	     ,@SYS_UPD_DATE    " & vbNewLine _
         & "	     ,@SYS_UPD_TIME	   " & vbNewLine _
         & "	     ,@SYS_UPD_PGID	   " & vbNewLine _
         & "	     ,@SYS_UPD_USER	   " & vbNewLine _
         & "	     ,SYS_DEL_FLG	   " & vbNewLine _
        & "FROM $PC_TRN_NM$..D_IDO_TRS " & vbNewLine _
        & "WHERE O_ZAI_REC_NO IN (     " & vbNewLine _
        & "SELECT ZAI_REC_NO FROM $PC_TRN_NM$..D_ZAI_TRS " & vbNewLine _
        & "WHERE NRS_BR_CD = @PC_BR_CD    " & vbNewLine _
        & "  AND CUST_CD_L = @PC_CUST_CD ) "

#Else
    Private Const SQL_INSERT_D_IDO_TRS As String = "INSERT INTO $NRS_TRN_NM$..D_IDO_TRS	 " & vbNewLine _
                                                & "	SELECT *	   　　　　　　　　" & vbNewLine _
                                                & "　　FROM $PC_TRN_NM$..D_IDO_TRS " & vbNewLine _
                                                & "　　WHERE O_ZAI_REC_NO IN (     " & vbNewLine _
                                                & "　　　SELECT ZAI_REC_NO FROM $PC_TRN_NM$..D_ZAI_TRS " & vbNewLine _
                                                & "　　　　WHERE NRS_BR_CD = @PC_BR_CD    " & vbNewLine _
                                                & "  　　　　AND CUST_CD_L = @PC_CUST_CD ) "

#End If

    ''' <summary>
    ''' D_IDO_TRS（更新）
    ''' </summary>
    ''' <remarks></remarks>
    '''
    Private Const SQL_UPDATE_D_IDO_TRS As String = "UPDATE $NRS_TRN_NM$..D_IDO_TRS	                                 " & vbNewLine _
                                                        & "	  SET  NRS_BR_CD       = @NRS_BR_CD    --NRS_BR_CD	 " & vbNewLine _
                                                        & "	      ,SYS_UPD_DATE    = @SYS_UPD_DATE	       " & vbNewLine _
                                                        & "	      ,SYS_UPD_TIME    = @SYS_UPD_TIME	       " & vbNewLine _
                                                        & "	      ,SYS_UPD_PGID    = @SYS_UPD_PGID	       " & vbNewLine _
                                                        & "	      ,SYS_UPD_USER    = @SYS_UPD_USER	       " & vbNewLine _
                                                        & "	WHERE NRS_BR_CD = @PC_BR_CD		 " & vbNewLine _

#End Region

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row2 As Data.DataRow

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

#Region "作業チェック"
    ''' <summary>
    ''' 作業チェックのデータを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索結果取得SQLの構築・発行</remarks>
    Private Function Sagyo_CHK1(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_SAGYO_CHK1)      'SQL構築(作業チェック１


        'Call Me.SetParamSelect()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "Sagyo_CHK1", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SAGYO_CD", "SAGYO_CD")
        map.Add("SAGYO_CD_SUB", "SAGYO_CD_SUB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "SAGYO_CHK")

        ''処理件数の設定
        'MyBase.SetResultCount(ds.Tables("SAGYO_CHK").Rows.Count())

        ''メッセージコードの設定
        'Dim count As Integer = MyBase.GetResultCount()
        'If count < 1 Then
        '    '0件の場合
        '    MyBase.SetMessage("G001")
        'End If
        reader.Close()
        Return ds

    End Function

    Private Function Sagyo_CHK2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_SAGYO_CHK2)      'SQL構築(作業チェック１


        'Call Me.SetParamSelect()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "Sagyo_CHK2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SAGYO_CD", "SAGYO_CD")
        map.Add("DABURI", "DABURI")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "SAGYO_CHK")
        reader.Close()
        Return ds

    End Function
#End Region

#Region "商品マスタ関連更新"

    Private Function DeleteGOODS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_MGOODS)      'SQL構築(商品マスタ削除


        'Call Me.SetParamCommonSystemIns()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteGOODS", cmd)

        'SQLの発行
        'MyBase.GetUpdateResult(cmd)
        Dim rtn As Integer = MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

    Private Function DeleteWK_RTGOODS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_WK_RTGOODS)      'SQL構築(WK_RT商品詳細マスタ削除


        'Call Me.SetParamCommonSystemIns()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteWK_RTGOODS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetDeleteResult(cmd)

        Return ds

    End Function


    Private Function DeleteGOODS_DETAILS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_MGOODS_DETAILS)      'SQL構築(商品マスタ削除


        'Call Me.SetParamCommonSystemIns()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteGOODS_DETAILS", cmd)

        'SQLの発行
        'MyBase.GetUpdateResult(cmd)
        Dim rtn As Integer = MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

    Private Function DeleteWK_RTGOODS_DETAILS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_WK_RTGOODS_DETAILS)      'SQL構築(WK_RT商品詳細マスタ削除


        'Call Me.SetParamCommonSystemIns()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteWK_RTGOODS_DETAILS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

    Private Function InsertGOODS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_M_GOODS)      'SQL構築(商品マスタ作成

        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertGOODS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    Private Function InsertWK_RTGOODS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_WK_RT_GOODS)      'SQL構築(WK_RT商品マスタ作成

        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertWK_RTGOODS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    Private Function UpdateWK_RTGOODS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_UPDATT_WK_RT_GOODS)      'SQL構築(SAGYO更新

        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "UpdateWK_RTGOODS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds
    End Function

    Private Function UpdateWK_RTGOODS_DETAILS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_UPDATE_WK_RT_GOODS_DETAILS)      'SQL構築(SAGYO更新

        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "UpdateWK_RTGOODS_DETAILS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds
    End Function

    Private Function InsertGOODS_DETAILS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_M_GOODS_DETAILS)      'SQL構築(商品詳細マスタ作成

        Call Me.SetParamCommonSystemUp()

        'SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertGOODS_DETAILS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    Private Function InsertWK_RTGOODS_DETAILS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_WK_RT_GOODS_DETAILS)      'SQL構築(商品詳細マスタ作成

        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertWK_RTGOODS_DETAILS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function
#End Region

#Region "届先マスタ関連更新"

    Private Function DeleteDEST(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_DEST)      'SQL構築(商品マスタ削除


        'Call Me.SetParamCommonSystemIns()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteDEST", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'reader.Close()
        Return ds

    End Function

    Private Function DeleteDEST_DETAILS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_MDEST_DETAILS)      'SQL構築(商品マスタ削除


        'Call Me.SetParamCommonSystemIns()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteDEST_DETAILS", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        'reader.Close()
        Return ds

    End Function

    Private Function DeleteWK_RTDEST_DETAILS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_WK_RTDEST_DETAILS)      'SQL構築(WK_RT商品詳細マスタ削除

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteWK_RTDEST_DETAILS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

    Private Function UpdateWK_RTDEST_DETAILS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_UPDATE_WK_RTDEST_DETAILS)      'SQL構築(WK_RT_DEST_DETAILS更新

        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "UpdateWK_RTDEST_DETAILS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds
    End Function


    Private Function DeleteWK_RTDEST(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_WK_RTDEST)      'SQL構築(WK_RT_DEST削除

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteWK_RTDEST_DETAILS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

    Private Function InsertDEST(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_M_DEST)      'SQL構築(商品マスタ作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertDEST", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    Private Function InsertDEST_DETAILS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_M_DEST_DETAILS)      'SQL構築(商品詳細マスタ作成


        Call Me.SetParamCommonSystemUp()

        'SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertDEST_DETAILS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    Private Function InsertWK_RTDEST_DETAILS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_WK_RTDEST_DETAILS)      'SQL構築(商品詳細マスタ作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertWK_RTDEST_DETAILS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    Private Function InsertWK_RTDEST(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_WK_RTDEST)      'SQL構築(商品詳細マスタ作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertWK_RTDEST", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    Private Function UpdateWK_RTDEST(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_UPDATE_WK_RTDEST)      'SQL構築(WK_RT_DEST_DETAILS更新

        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "UpdateWK_RTDEST", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds
    End Function

#End Region

#Region "入荷関連更新"

    Private Function DeleteINKA_L(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_INKA_L)      'SQL構築(INKA_L削除

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_TRN_NM", Me._Row.Item("NRS_TRN_NM").ToString(), DBDataType.NUMERIC))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_TRN_NM", Me._Row.Item("PC_TRN_NM").ToString(), DBDataType.NUMERIC))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteINKA_L", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    Private Function DeleteINKA_M(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_INKA_M)      'SQL構築(商品マスタ削除


        'Call Me.SetParamCommonSystemIns()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteINKA_M", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    Private Function DeleteINKA_S(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_INKA_S)      'SQL構築(商品マスタ削除


        'Call Me.SetParamCommonSystemIns()


        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteINKA_S", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    Private Function InsertINKA_L(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_INKA_L)      'SQL構築(INKA_L作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_WH_CD", Me._Row.Item("NRS_WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertINKA_L", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    Private Function UpdateINKA_L(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_UPDATE_INKA_L)      'SQL構築(UNSO_M作成

        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_WH_CD", Me._Row.Item("NRS_WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "UpdateINKA_L", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    Private Function InsertINKA_M(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_INKA_M)      'SQL構築(INKA_M作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertINKA_M", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)
        'MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    Private Function UpdateINKA_M(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_UPDATE_INKA_M)      'SQL構築(UNSO_M作成

        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "UpdateINKA_M", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    Private Function InsertINKA_S(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_INKA_S)      'SQL構築(INKA_M作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_TOU_NO", Me._Row.Item("NRS_TOU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_SITU_NO", Me._Row.Item("NRS_SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_ZONE_CD", Me._Row.Item("NRS_ZONE_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertINKA_S", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)
        'MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    Private Function UpdateINKA_S(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_UPDATE_INKA_S)      'SQL構築(INKA_S作成

        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_TOU_NO", Me._Row.Item("NRS_TOU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_SITU_NO", Me._Row.Item("NRS_SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_ZONE_CD", Me._Row.Item("NRS_ZONE_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))


        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "UpdateINKA_S", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "出荷関連更新"

    Private Function DeleteOUTKA_L(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_OUTKA_L)      'SQL構築(OUTKA_L削除

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_TRN_NM", Me._Row.Item("NRS_TRN_NM").ToString(), DBDataType.NUMERIC))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_TRN_NM", Me._Row.Item("PC_TRN_NM").ToString(), DBDataType.NUMERIC))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteOUTKA_L", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    Private Function DeleteOUTKA_M(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_OUTKA_M)      'SQL構築(OUTKA_M削除

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_TRN_NM", Me._Row.Item("NRS_TRN_NM").ToString(), DBDataType.NUMERIC))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_TRN_NM", Me._Row.Item("PC_TRN_NM").ToString(), DBDataType.NUMERIC))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteOUTKA_M", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function


    Private Function DeleteOUTKA_S(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_OUTKA_S)      'SQL構築(OUTKA_L削除

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_TRN_NM", Me._Row.Item("NRS_TRN_NM").ToString(), DBDataType.NUMERIC))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))
        'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_TRN_NM", Me._Row.Item("PC_TRN_NM").ToString(), DBDataType.NUMERIC))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteOUTKA_S", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function


    Private Function InsertOUTKA_L(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_OUTKA_L)      'SQL構築(OUTKA_L作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_WH_CD", Me._Row.Item("NRS_WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertOUTKA_L", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)
        'MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    Private Function UpdateOUTKA_L(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_UPDATE_OUTKA_L)      'SQL構築(UNSO_M作成

        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_WH_CD", Me._Row.Item("NRS_WH_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "UpdateOUTKA_L", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function


    Private Function InsertOUTKA_M(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_OUTKA_M)      'SQL構築(OUTKA_M作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertOUTKA_M", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)
        'MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    Private Function UpdateOUTKA_M(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_UPDATE_OUTKA_M)      'SQL構築(UNSO_M作成

        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "UpdateOUTKA_M", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function



    Private Function InsertOUTKA_S(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_OUTKA_S)      'SQL構築(OUTKA_M作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_TOU_NO", Me._Row.Item("NRS_TOU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_SITU_NO", Me._Row.Item("NRS_SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_ZONE_CD", Me._Row.Item("NRS_ZONE_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertOUTKA_S", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)
        'MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function


    Private Function UpdateOUTKA_S(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_UPDATE_OUTKA_S)      'SQL構築(UNSO_M作成

        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_TOU_NO", Me._Row.Item("NRS_TOU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_SITU_NO", Me._Row.Item("NRS_SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_ZONE_CD", Me._Row.Item("NRS_ZONE_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "UpdateOUTKA_S", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "D_ZAI_TRS関連更新"

    Private Function DeleteZAI_TRS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_ZAI_TRS)      'SQL構築(商品マスタ削除


        'Call Me.SetParamCommonSystemIns()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteZAI_TRS", cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

    Private Function InsertZAI_TRS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_ZAI_TRS)      'SQL構築(ZAI_TRS作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_TOU_NO", Me._Row.Item("NRS_TOU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_SITU_NO", Me._Row.Item("NRS_SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_ZONE_CD", Me._Row.Item("NRS_ZONE_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_WH_CD", Me._Row.Item("NRS_WH_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertZAI_TRS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)
        'MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    Private Function UpdateZAI_TRS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_UPDATE_ZAI_TRS)      'SQL構築(ZAI_TRS更新

        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_TOU_NO", Me._Row.Item("NRS_TOU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_SITU_NO", Me._Row.Item("NRS_SITU_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_ZONE_CD", Me._Row.Item("NRS_ZONE_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_WH_CD", Me._Row.Item("NRS_WH_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "UpdateZAI_TRS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds
    End Function
#End Region

#Region "UNSO関連更新"

    Private Function DeleteUNSO_LL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_UNSO_LL)      'SQL構築(商品マスタ削除


        'Call Me.SetParamCommonSystemIns()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteUNSO_LL", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function


    Private Function DeleteUNSO_L(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_UNSO_L)      'SQL構築(商品マスタ削除


        'Call Me.SetParamCommonSystemIns()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteUNSO_L", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    Private Function DeleteUNSO_M(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_UNSO_M)      'SQL構築(商品マスタ削除


        'Call Me.SetParamCommonSystemIns()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteUNSO_M", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function


    Private Function InsertUNSO_LL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_UNSO_LL)      'SQL構築(ZAI_TRS作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertUNSO_LL", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)
        'MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    Private Function UpdateUNSO_LL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_UPDATE_UNSO_LL)      'SQL構築(UNSO_M作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "UpdateUNSO_LL", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function


    Private Function InsertUNSO_L(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_UNSO_L)      'SQL構築(ZAI_TRS作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertUNSO_L", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)
        'MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    Private Function UpdateUNSO_L(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_UPDATE_UNSO_L)      'SQL構築(UNSO_M作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "UpdateUNSO_L", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    Private Function InsertUNSO_M(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_UNSO_M)      'SQL構築(UNSO_M作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertUNSO_M", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)
        'MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    Private Function UpdateUNSO_M(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_UPDATE_UNSO_M)      'SQL構築(UNSO_M作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "UpdateUNSO_M", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function
#End Region

#Region "F_UNCHIN_TRS関連更新"

    Private Function DeleteUNCHIN_TRS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_UNCHIN_TRS)      'SQL構築(UNCHIN_TRS削除


        'Call Me.SetParamCommonSystemIns()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteUNCHIN_TRS", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    Private Function InsertUNCHIN_TRS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_UNCHIN_TRS)      'SQL構築(商品マスタ作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertUNCHIN_TRS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)
        'MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    Private Function UpdateUNCHIN_TRS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_UPDATE_UNCHIN_TRS)      'SQL構築(UNCHIN_TRS更新

        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "UpdateUNCHIN_TRS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds
    End Function
#End Region

#Region "E_SAGYO関連更新"

    Private Function DeleteSAGYO(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_SAGYO)      'SQL構築(UNCHIN_TRS削除


        'Call Me.SetParamCommonSystemIns()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteSAGYO", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    Private Function InsertSAGYO(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_SAGYO)      'SQL構築(作業作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_WH_CD", Me._Row.Item("NRS_WH_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertSAGYO", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)
        'MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    Private Function UpdateSAGYO(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_UPDATE_SAGYO)      'SQL構築(SAGYO更新

        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_WH_CD", Me._Row.Item("NRS_WH_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "UpdateSAGYO", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds
    End Function
#End Region


#Region "D_IDO_TRS関連更新"

    Private Function DeleteD_IDO_TRS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_DELETE_D_IDO_TRS)      'SQL構築(UNCHIN_TRS削除


        'Call Me.SetParamCommonSystemIns()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "DeleteD_IDO_TRS", cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    Private Function InsertD_IDO_TRS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_INSERT_D_IDO_TRS)      'SQL構築(作業作成


        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_WH_CD", Me._Row.Item("NRS_WH_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "InsertD_IDO_TRS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    Private Function UpdateD_IDO_TRS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LML010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList

        'SQL作成
        Me._StrSql.Append(LML010DAC.SQL_UPDATE_D_IDO_TRS)      'SQL構築(D_IDO_TRS更新

        Call Me.SetParamCommonSystemUp()

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_CUST_CD", Me._Row.Item("NRS_CUST_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_WH_CD", Me._Row.Item("NRS_WH_CD").ToString(), DBDataType.CHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_BR_CD", Me._Row.Item("PC_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_CUST_CD", Me._Row.Item("PC_CUST_CD").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim sql As String = SetSchemaNmNrs(Me._StrSql.ToString(), Me._Row.Item("NRS_TRN_NM").ToString())
        sql = Me.SetSchemaNmPc(sql.ToString, Me._Row.Item("PC_TRN_NM").ToString())

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql.ToString)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LML010DAC", "UpdateD_IDO_TRS", cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetUpdateResult(cmd)

        Return ds
    End Function
#End Region
#Region "パラメータ"

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

    Private Function SetSchemaNmNrs(ByVal sql As String, ByVal trnNm As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$NRS_TRN_NM$", trnNm)

        Return sql

    End Function

    Private Function SetSchemaNmPc(ByVal sql As String, ByVal trnNm As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$PC_TRN_NM$", trnNm)

        Return sql

    End Function
    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUp()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
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

    ''' <summary>
    ''' パラメータ設定モジュール(SEIQ_HED)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComSelectChkDateParamSAGYO()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SAGYO_SEIQTO_CD").ToString(), DBDataType.NVARCHAR))   '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

    End Sub

#End Region

End Class
