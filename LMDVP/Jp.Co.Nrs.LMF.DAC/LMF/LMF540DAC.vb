' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF540DAC : 運賃試算重量別
'  作  成  者       :  菱刈
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF540DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF540DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "印刷種別"
    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                        " & vbNewLine _
                                            & "	TARIFF.NRS_BR_CD                                            AS NRS_BR_CD " & vbNewLine _
                                            & ",'51'                                                     AS PTN_ID    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                      " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                     " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD END                                AS PTN_CD    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                      " & vbNewLine _
                                            & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                 " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                                AS RPT_ID    " & vbNewLine


#End Region

#Region "SELECT句"

    ''' <summary>
    ''' データタイプ０のデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_0 As String = " SELECT*                 " & vbNewLine

    ''' <summary>
    ''' データタイプ０のデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_01 As String = "SELECT TARIFF.*         " & vbNewLine

    ''' <summary>
    ''' データタイプ０のデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_WARIMASI As String = " SELECT                                                        " & vbNewLine _
                                                     & "        KBN1.KBN_NM1         AS RELY_EXTC_NM                   " & vbNewLine _
                                                     & "      , KBN2.KBN_NM1         AS FRRY_EXTC_NM                   " & vbNewLine _
                                                     & "      , KBN3.KBN_NM1         AS CITY_EXTC_NM                   " & vbNewLine _
                                                     & "      , CASE WHEN KBN4.KBN_NM1 IS NOT NULL THEN KBN4.KBN_NM1   " & vbNewLine _
                                                     & "--(2012.10.25)要望番号884 冬季期間追加 -- START --             " & vbNewLine _
                                                     & "             WHEN KBN5.KBN_NM1 IS NOT NULL THEN KBN5.KBN_NM1   " & vbNewLine _
                                                     & "--(2012.10.25)要望番号884 冬季期間追加 --  END  --             " & vbNewLine _
                                                     & "        ELSE 'なし' END    AS WINT_EXTC_NM                     " & vbNewLine _
                                                     & "      , CASE WHEN SYUHAI.NRS_BR_CD IS NOT NULL THEN 'あり'     " & vbNewLine _
                                                     & "        ELSE 'なし'  END     AS POST_EXTC_NM                   " & vbNewLine _
                                                     & "      , EXTC.EXTC_TARIFF_REM AS EXTC_TARIFF_REM                " & vbNewLine


    ' & "     , EXTC.EXTC_TARIFF_REM AS EXTC_TARIFF_REM                 " & vbNewLine

    'Private Const SQL_SELECT_NM As String = " SELECT                                                                   " & vbNewLine _
    '                                      & "  CUST.CUST_NM_L       AS CUST_NM_L                                       " & vbNewLine _
    '                                      & " ,  ORIGJIS.ORIG       AS ORIG_JIS_NM                                     " & vbNewLine _
    '                                      & " ,  DESTJIS.DEST       AS DEST_JIS_NM                                     " & vbNewLine

    Private Const SQL_SELECT_NM As String = " SELECT                                                                   " & vbNewLine _
                                          & "        CUST.CUST_NM_L              AS CUST_NM_L                          " & vbNewLine _
                                          & "      , ORIGJIS.KEN + ORIGJIS.SHI   AS ORIG_JIS_NM                        " & vbNewLine _
                                          & "      , DESTJIS.KEN + DESTJIS.SHI   AS DEST_JIS_NM                        " & vbNewLine







#End Region

#Region "FROM句"

    Private Const SQL_FROM_0 As String = "FROM  $LM_MST$..M_UNCHIN_TARIFF  AS TARIFF                                                                        " & vbNewLine _
                                       & "     INNER JOIN  (SELECT MAX(STR_DATE) AS STR_DATE1,UNCHIN_TARIFF_CD AS UNCHIN_TARIFF_CD1 FROM $LM_MST$..M_UNCHIN_TARIFF AS TARIFF1    " & vbNewLine _
                                       & "     WHERE TARIFF1.UNCHIN_TARIFF_CD=@UNCHIN_TARIFF_CD                                                           " & vbNewLine _
                                       & "     AND TARIFF1.DATA_TP='00'                                                                                   " & vbNewLine _
                                       & "     GROUP BY UNCHIN_TARIFF_CD) AS A                                                                            " & vbNewLine _
                                       & "     ON A.UNCHIN_TARIFF_CD1=TARIFF.UNCHIN_TARIFF_CD                                                             " & vbNewLine _
                                       & "     AND A.STR_DATE1=TARIFF.STR_DATE                                                                            " & vbNewLine _
                                       & "     AND  TARIFF.DATA_TP='00'                                                                                   " & vbNewLine _
                                       & "     WHERE                                                                                                      " & vbNewLine _
                                       & "     TARIFF.UNCHIN_TARIFF_CD =@UNCHIN_TARIFF_CD                                                                 " & vbNewLine _
                                       & "      AND A.STR_DATE1=TARIFF.STR_DATE                                                                           " & vbNewLine



    Private Const SQL_FROM_1 As String = "FROM  $LM_MST$..M_UNCHIN_TARIFF AS TARIFF                                                 " & vbNewLine _
                                         & "     INNER JOIN                                                                       " & vbNewLine _
                                         & "     (SELECT  UNCHIN_TARIFF_CD AS UNCHIN_TARIFF_CD1                                   " & vbNewLine _
                                         & "     , MIN(DATA_TP) AS DATA_TP1 ,max(STR_DATE) AS STR_DATE1 FROM $LM_MST$..M_UNCHIN_TARIFF  " & vbNewLine _
                                         & "      WHERE DATA_TP <> '00' GROUP BY UNCHIN_TARIFF_CD) AS TARIFF1                     " & vbNewLine _
                                         & "     ON  TARIFF.DATA_TP = TARIFF1.DATA_TP1                                            " & vbNewLine _
                                         & "     AND TARIFF.UNCHIN_TARIFF_CD = TARIFF1.UNCHIN_TARIFF_CD1                          " & vbNewLine _
                                         & "     WHERE                                                                            " & vbNewLine _
                                         & "     TARIFF.UNCHIN_TARIFF_CD = @UNCHIN_TARIFF_CD                                      " & vbNewLine _
                                         & "     AND TARIFF1.STR_DATE1=TARIFF.STR_DATE                                            " & vbNewLine


    ''' <summary>
    ''' 割増運賃情報データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_WARIMASI As String = "  FROM $LM_MST$..M_EXTC_UNCHIN AS EXTC                       " & vbNewLine _
                                              & "       --区分マスタ(中継)                                    " & vbNewLine _
                                              & "       LEFT JOIN $LM_MST$..Z_KBN AS KBN1                     " & vbNewLine _
                                              & "              ON EXTC.RELY_EXTC_YN=KBN1.KBN_CD               " & vbNewLine _
                                              & "             AND    KBN1.KBN_GROUP_CD='W004'                 " & vbNewLine _
                                              & "       --区分マスタ(航送)                                    " & vbNewLine _
                                              & "       LEFT JOIN $LM_MST$..Z_KBN AS KBN2                     " & vbNewLine _
                                              & "              ON EXTC.FRRY_EXTC_YN=KBN2.KBN_CD               " & vbNewLine _
                                              & "             AND KBN2.KBN_GROUP_CD='W005'                    " & vbNewLine _
                                              & "       --区分マスタ(都市)                                    " & vbNewLine _
                                              & "       LEFT JOIN $LM_MST$..Z_KBN AS KBN3                     " & vbNewLine _
                                              & "              ON EXTC.CITY_EXTC_YN=KBN3.KBN_CD               " & vbNewLine _
                                              & "             AND KBN3.KBN_GROUP_CD='W003'                    " & vbNewLine _
                                              & " --(2012.10.25)要望番号884 冬季期間修正 -- START --          " & vbNewLine _
                                              & " --      --区分マスタ(冬期)                                  " & vbNewLine _
                                              & " --      LEFT JOIN $LM_MST$..Z_KBN AS KBN4                   " & vbNewLine _
                                              & " --             ON EXTC.WINT_EXTC_YN=KBN4.KBN_CD             " & vbNewLine _
                                              & " --            AND KBN4.KBN_GROUP_CD='W002'                  " & vbNewLine _
                                              & " --            AND EXTC.WINT_KIKAN_FROM<=@DATA               " & vbNewLine _
                                              & " --            AND EXTC.WINT_KIKAN_TO>=@DATA                 " & vbNewLine _
                                              & "       --区分マスタ(冬期)                                                           " & vbNewLine _
                                              & "       LEFT JOIN LM_MST..Z_KBN AS KBN4                                              " & vbNewLine _
                                              & "              ON EXTC.WINT_EXTC_YN=KBN4.KBN_CD                                      " & vbNewLine _
                                              & "             AND KBN4.KBN_GROUP_CD='W002'                                           " & vbNewLine _
                                              & "             AND ( CONVERT(varchar,year(getdate())) + @DATA                         " & vbNewLine _
                                              & "                   BETWEEN CONVERT(varchar,year(getdate())) + EXTC.WINT_KIKAN_FROM  " & vbNewLine _
                                              & "                   AND     CONVERT(varchar,year(getdate())+1) + EXTC.WINT_KIKAN_TO  " & vbNewLine _
                                              & "                  )                                                                 " & vbNewLine _
                                              & "       --区分マスタ(冬期)                                                           " & vbNewLine _
                                              & "       LEFT JOIN LM_MST..Z_KBN AS KBN5                                              " & vbNewLine _
                                              & "              ON EXTC.WINT_EXTC_YN=KBN5.KBN_CD                                      " & vbNewLine _
                                              & "             AND KBN5.KBN_GROUP_CD='W002'                                           " & vbNewLine _
                                              & "             AND ( CONVERT(varchar,year(getdate())+1) + @DATA                       " & vbNewLine _
                                              & "                   BETWEEN CONVERT(varchar,year(getdate())) + EXTC.WINT_KIKAN_FROM  " & vbNewLine _
                                              & "                   AND     CONVERT(varchar,year(getdate())+1) + EXTC.WINT_KIKAN_TO  " & vbNewLine _
                                              & "                  )                                                                 " & vbNewLine _
                                              & " --(2012.10.25)要望番号884 冬季期間修正 --  END  --                                 " & vbNewLine _
                                              & "       --集配設定ファイル                                                           " & vbNewLine _
                                              & "       LEFT JOIN $LM_MST$..M_SYUHAI_SET AS SYUHAI                                   " & vbNewLine _
                                              & "              ON SYUHAI.CUST_CD_L = @CUST_CD_L                                      " & vbNewLine _
                                              & "             AND SYUHAI.UNCHIN_TARIFF_CD = @UNCHIN_TARIFF_CD                        " & vbNewLine _
                                              & "             AND SYUHAI.JIS_CD = @ORIG_JIS_CD                                       " & vbNewLine _
                                              & " WHERE                                                                              " & vbNewLine _
                                              & " --(2013.02.07)要望番号1826 冬季期間修正(884コメントアウト、以前に戻す) -- START -- " & vbNewLine _
                                              & " ----(2012.10.25)要望番号884 冬季期間修正 -- START --                               " & vbNewLine _
                                              & " ----    EXTC.JIS_CD = @DEST_JIS_CD                                                 " & vbNewLine _
                                              & " --      EXTC.JIS_CD = CASE WHEN @ORIG_JIS_CD < @DEST_JIS_CD  THEN @ORIG_JIS_CD     " & vbNewLine _
                                              & " --                    ELSE @DEST_JIS_CD                                            " & vbNewLine _
                                              & " --                    END                                                          " & vbNewLine _
                                              & " ----(2012.10.25)要望番号884 冬季期間修正 --  END  --                               " & vbNewLine _
                                              & "       EXTC.JIS_CD         = @DEST_JIS_CD                                           " & vbNewLine _
                                              & " --(2013.02.07)要望番号1826 冬季期間修正 -- END --                                  " & vbNewLine _
                                              & "   AND EXTC.EXTC_TARIFF_CD = @EXTC_TARIFF_CD                                        " & vbNewLine _
                                              & "   AND EXTC.NRS_BR_CD = @NRS_BR_CD                                                  " & vbNewLine


    ''' <summary>
    ''' 割増運賃情報データ抽出用(JISコードを切り替えた場合)'Notes1826 2013/02/07 
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_WARIMASI_CHANGED As String = "  FROM $LM_MST$..M_EXTC_UNCHIN AS EXTC               " & vbNewLine _
                                              & "       --区分マスタ(中継)                                    " & vbNewLine _
                                              & "       LEFT JOIN $LM_MST$..Z_KBN AS KBN1                     " & vbNewLine _
                                              & "              ON EXTC.RELY_EXTC_YN=KBN1.KBN_CD               " & vbNewLine _
                                              & "             AND    KBN1.KBN_GROUP_CD='W004'                 " & vbNewLine _
                                              & "       --区分マスタ(航送)                                    " & vbNewLine _
                                              & "       LEFT JOIN $LM_MST$..Z_KBN AS KBN2                     " & vbNewLine _
                                              & "              ON EXTC.FRRY_EXTC_YN=KBN2.KBN_CD               " & vbNewLine _
                                              & "             AND KBN2.KBN_GROUP_CD='W005'                    " & vbNewLine _
                                              & "       --区分マスタ(都市)                                    " & vbNewLine _
                                              & "       LEFT JOIN $LM_MST$..Z_KBN AS KBN3                     " & vbNewLine _
                                              & "              ON EXTC.CITY_EXTC_YN=KBN3.KBN_CD               " & vbNewLine _
                                              & "             AND KBN3.KBN_GROUP_CD='W003'                    " & vbNewLine _
                                              & " --(2012.10.25)要望番号884 冬季期間修正 -- START --          " & vbNewLine _
                                              & " --      --区分マスタ(冬期)                                  " & vbNewLine _
                                              & " --      LEFT JOIN $LM_MST$..Z_KBN AS KBN4                   " & vbNewLine _
                                              & " --             ON EXTC.WINT_EXTC_YN=KBN4.KBN_CD             " & vbNewLine _
                                              & " --            AND KBN4.KBN_GROUP_CD='W002'                  " & vbNewLine _
                                              & " --            AND EXTC.WINT_KIKAN_FROM<=@DATA               " & vbNewLine _
                                              & " --            AND EXTC.WINT_KIKAN_TO>=@DATA                 " & vbNewLine _
                                              & "       --区分マスタ(冬期)                                                           " & vbNewLine _
                                              & "       LEFT JOIN LM_MST..Z_KBN AS KBN4                                              " & vbNewLine _
                                              & "              ON EXTC.WINT_EXTC_YN=KBN4.KBN_CD                                      " & vbNewLine _
                                              & "             AND KBN4.KBN_GROUP_CD='W002'                                           " & vbNewLine _
                                              & "             AND ( CONVERT(varchar,year(getdate())) + @DATA                         " & vbNewLine _
                                              & "                   BETWEEN CONVERT(varchar,year(getdate())) + EXTC.WINT_KIKAN_FROM  " & vbNewLine _
                                              & "                   AND     CONVERT(varchar,year(getdate())+1) + EXTC.WINT_KIKAN_TO  " & vbNewLine _
                                              & "                  )                                                                 " & vbNewLine _
                                              & "       --区分マスタ(冬期)                                                           " & vbNewLine _
                                              & "       LEFT JOIN LM_MST..Z_KBN AS KBN5                                              " & vbNewLine _
                                              & "              ON EXTC.WINT_EXTC_YN=KBN5.KBN_CD                                      " & vbNewLine _
                                              & "             AND KBN5.KBN_GROUP_CD='W002'                                           " & vbNewLine _
                                              & "             AND ( CONVERT(varchar,year(getdate())+1) + @DATA                       " & vbNewLine _
                                              & "                   BETWEEN CONVERT(varchar,year(getdate())) + EXTC.WINT_KIKAN_FROM  " & vbNewLine _
                                              & "                   AND     CONVERT(varchar,year(getdate())+1) + EXTC.WINT_KIKAN_TO  " & vbNewLine _
                                              & "                  )                                                                 " & vbNewLine _
                                              & " --(2012.10.25)要望番号884 冬季期間修正 --  END  --                                 " & vbNewLine _
                                              & "       --集配設定ファイル                                                           " & vbNewLine _
                                              & "       LEFT JOIN $LM_MST$..M_SYUHAI_SET AS SYUHAI                                   " & vbNewLine _
                                              & "              ON SYUHAI.CUST_CD_L = @CUST_CD_L                                      " & vbNewLine _
                                              & "             AND SYUHAI.UNCHIN_TARIFF_CD = @UNCHIN_TARIFF_CD                        " & vbNewLine _
                                              & "             AND SYUHAI.JIS_CD = @ORIG_JIS_CD                                       " & vbNewLine _
                                              & " WHERE                                                                              " & vbNewLine _
                                              & " --(2012.10.25)要望番号884 冬季期間修正 -- START --                                 " & vbNewLine _
                                              & " --    EXTC.JIS_CD = @DEST_JIS_CD                                                   " & vbNewLine _
                                              & "       EXTC.JIS_CD = CASE WHEN @ORIG_JIS_CD < @DEST_JIS_CD  THEN @ORIG_JIS_CD       " & vbNewLine _
                                              & "                     ELSE @DEST_JIS_CD                                              " & vbNewLine _
                                              & "                     END                                                            " & vbNewLine _
                                              & " --(2012.10.25)要望番号884 冬季期間修正 --  END  --                                 " & vbNewLine _
                                              & "   AND EXTC.EXTC_TARIFF_CD = @EXTC_TARIFF_CD                                        " & vbNewLine _
                                              & "   AND EXTC.NRS_BR_CD = @NRS_BR_CD                                                  " & vbNewLine


    Private Const SQL_FROM As String = "FROM                                                         " & vbNewLine _
                                           & "	$LM_MST$..M_UNCHIN_TARIFF AS TARIFF                    " & vbNewLine _
                                           & "	 --運賃                                              " & vbNewLine _
                                           & "	 LEFT JOIN  $LM_TRN$..F_UNCHIN_TRS AS UNCHIN           " & vbNewLine _
                                           & "	ON UNCHIN.SEIQ_TARIFF_CD=TARIFF.UNCHIN_TARIFF_CD     " & vbNewLine _
                                           & "	AND UNCHIN.NRS_BR_CD=TARIFF.NRS_BR_CD                " & vbNewLine _
                                           & "	 --運送M                                     		 " & vbNewLine _
                                           & "	 LEFT JOIN $LM_TRN$..F_UNSO_M AS UNSOM         		 " & vbNewLine _
                                           & "	 ON UNCHIN.UNSO_NO_L =UNSOM.UNSO_NO_L     			 " & vbNewLine _
                                           & "	 AND UNCHIN.UNSO_NO_M =UNSOM.UNSO_NO_M     			 " & vbNewLine _
                                           & "	 AND UNSOM.SYS_DEL_FLG='0'                 			 " & vbNewLine _
                                           & "	  --商品マスタ                                       " & vbNewLine _
                                           & "	 LEFT JOIN $LM_MST$..M_GOODS AS GOODS                  " & vbNewLine _
                                           & "	 ON UNCHIN.NRS_BR_CD=GOODS.NRS_BR_CD                 " & vbNewLine _
                                           & "	 AND UNSOM.GOODS_CD_NRS=GOODS.GOODS_CD_NRS           " & vbNewLine _
                                           & "	--運賃での荷主帳票パターン取得                       " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                    " & vbNewLine _
                                           & "	ON  UNCHIN.NRS_BR_CD = MCR1.NRS_BR_CD                " & vbNewLine _
                                           & "	AND UNCHIN.CUST_CD_L = MCR1.CUST_CD_L                " & vbNewLine _
                                           & "	AND UNCHIN.CUST_CD_M = MCR1.CUST_CD_M                " & vbNewLine _
                                           & "	AND '00' = MCR1.CUST_CD_S                            " & vbNewLine _
                                           & "	AND MCR1.PTN_ID = '51'                               " & vbNewLine _
                                           & "	--帳票パターン取得                                   " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_RPT MR1                          " & vbNewLine _
                                           & "	ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                   " & vbNewLine _
                                           & "	AND MR1.PTN_ID = MCR1.PTN_ID                         " & vbNewLine _
                                           & "	AND MR1.PTN_CD = MCR1.PTN_CD                         " & vbNewLine _
                                           & "  AND MR1.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                           & "	--商品Mの荷主での荷主帳票パターン取得                " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                    " & vbNewLine _
                                           & "	ON  GOODS.NRS_BR_CD = MCR2.NRS_BR_CD                 " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_L = MCR2.CUST_CD_L                 " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_M = MCR2.CUST_CD_M                 " & vbNewLine _
                                           & "	AND GOODS.CUST_CD_S = MCR2.CUST_CD_S                 " & vbNewLine _
                                           & "	AND MCR2.PTN_ID = '51'                               " & vbNewLine _
                                           & "	--帳票パターン取得                                   " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_RPT MR2                          " & vbNewLine _
                                           & "	ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                   " & vbNewLine _
                                           & "	AND MR2.PTN_ID = MCR2.PTN_ID                         " & vbNewLine _
                                           & "	AND MR2.PTN_CD = MCR2.PTN_CD                         " & vbNewLine _
                                           & "  AND MR2.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                           & "	--存在しない場合の帳票パターン取得                   " & vbNewLine _
                                           & "	LEFT JOIN $LM_MST$..M_RPT MR3                          " & vbNewLine _
                                           & "	ON  MR3.NRS_BR_CD = TARIFF.NRS_BR_CD                 " & vbNewLine _
                                           & "	AND MR3.PTN_ID = '51'                                " & vbNewLine _
                                           & "	AND MR3.STANDARD_FLAG = '01'                     	 " & vbNewLine _
                                           & "  AND MR3.SYS_DEL_FLG = '0'                     " & vbNewLine _
                                           & "	WHERE  TARIFF.NRS_BR_CD = @NRS_BR_CD                 " & vbNewLine _
                                           & "	AND  TARIFF.UNCHIN_TARIFF_CD = @UNCHIN_TARIFF_CD     " & vbNewLine _
                                           & "	AND TARIFF.UNCHIN_TARIFF_CD_EDA='000'                " & vbNewLine

    'Private Const SQL_FROM_NM As String = "FROM                                                       " & vbNewLine _
    '                                      & "					                                      " & vbNewLine _
    '                                      & "		(			                                      " & vbNewLine _
    '                                      & "		--荷主マスタ			                          " & vbNewLine _
    '                                      & "		SELECT CUST_NM_L 			                      " & vbNewLine _
    '                                      & "		FROM   $LM_MST$..M_CUST			                  " & vbNewLine _
    '                                      & "		WHERE 			                                  " & vbNewLine _
    '                                      & "		    NRS_BR_CD=@NRS_BR_CD			              " & vbNewLine _
    '                                      & "		AND CUST_CD_L=@CUST_CD_L			              " & vbNewLine _
    '                                      & "		AND CUST_CD_M=@CUST_CD_M			              " & vbNewLine _
    '                                      & "		AND CUST_CD_S='00'			                      " & vbNewLine _
    '                                      & "		AND CUST_CD_SS='00'			                      " & vbNewLine _
    '                                      & "		) AS CUST			                              " & vbNewLine _
    '                                      & "		,(			                                      " & vbNewLine _
    '                                      & "		--JISマスタ			                              " & vbNewLine _
    '                                      & "		--発地名取得時のJISマスタ			              " & vbNewLine _
    '                                      & "		SELECT KEN + SHI AS ORIG 			              " & vbNewLine _
    '                                      & "		FROM   $LM_MST$..M_JIS			                  " & vbNewLine _
    '                                      & "		WHERE JIS_CD=@ORIG_JIS_CD			              " & vbNewLine _
    '                                      & "		) AS ORIGJIS			                          " & vbNewLine _
    '                                      & "		,(			                                      " & vbNewLine _
    '                                      & "		--届け先取得時のJIS			                      " & vbNewLine _
    '                                      & "		SELECT KEN + SHI AS DEST 			              " & vbNewLine _
    '                                      & "		FROM   $LM_MST$..M_JIS			                  " & vbNewLine _
    '                                      & "		WHERE JIS_CD=@DEST_JIS_CD			              " & vbNewLine _
    '                                      & "		) AS DESTJIS			                          " & vbNewLine


    Private Const SQL_FROM_NM As String = " FROM $LM_MST$..M_CUST  AS CUST                                                       " & vbNewLine _
                                         & " LEFT JOIN  $LM_MST$..M_JIS AS ORIGJIS					                                      " & vbNewLine _
                                         & " ON ORIGJIS.JIS_CD=@ORIG_JIS_CD                 				                                      " & vbNewLine _
                                         & " LEFT JOIN  $LM_MST$..M_JIS AS DESTJIS			                          " & vbNewLine _
                                         & " ON DESTJIS.JIS_CD=@DEST_JIS_CD 			                      " & vbNewLine _
                                         & " WHERE			                  " & vbNewLine _
                                         & " CUST.NRS_BR_CD=@NRS_BR_CD			              " & vbNewLine _
                                         & " AND CUST_CD_L=@CUST_CD_L			              " & vbNewLine _
                                         & " AND CUST_CD_M=@CUST_CD_M			              " & vbNewLine _
                                         & " AND CUST_CD_S='00'			                      " & vbNewLine _
                                         & " AND CUST_CD_SS='00'			                      " & vbNewLine

#End Region

#Region "ORDER BY"
    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                         " & vbNewLine _
                                         & "    TARIFF.WT_LV                                 " & vbNewLine


#End Region

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


#Region "検索処理"

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF540DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMF540DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF540DAC", "SelectMPrt", cmd)

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
    ''' 運賃タリフ (距離の取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>適用運賃レコードを特定する</remarks>
    Private Function GetTariffKyori(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF540DAC.SQL_SELECT_DATA_0)
        Me._StrSql.Append(LMF540DAC.SQL_FROM_0)

        'パラメータ設定
        Call Me.SetConditionMasterSQL()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF540DAC", "GetTariffKyori", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        ds = MyBase.SetSelectResultToDataSet(SetTariffResult(), ds, reader, "M_UNCHIN_TARIFF")

        Return ds

    End Function
    ''' <summary>
    ''' 運賃タリフ (金額、重量の取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>適用運賃レコードを特定する</remarks>
    Private Function GetTariffJuryou(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF540DAC.SQL_SELECT_DATA_01)
        Me._StrSql.Append(LMF540DAC.SQL_FROM_1)
        Me._StrSql.Append(LMF540DAC.SQL_ORDER_BY)

        'パラメータ設定
        Call Me.SetConditionMasterSQL()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF540DAC", "GetTariffJuryou", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        ds = MyBase.SetSelectResultToDataSet(SetTariffResult(), ds, reader, "M_UNCHIN_TARIFF")

        Return ds

    End Function
    ''' <summary>
    '''割増区分の取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function GetWarimasi(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF540DAC.SQL_SELECT_DATA_WARIMASI)        'SQL構築(帳票種別用Select句)
        If inTbl.Rows(0).Item("CHANGE_FLG").Equals("1") = True Then  'Notes1826 2013/02/07 (発地と届先を逆転させた = 1)
            Me._StrSql.Append(LMF540DAC.SQL_FROM_WARIMASI_CHANGED)   'Notes1826 2013/02/07 SQL構築(データ抽出用From句)'(発地と届先を逆転させた)
        Else
            Me._StrSql.Append(LMF540DAC.SQL_FROM_WARIMASI)           'SQL構築(データ抽出用From句)
        End If
        Call Me.SetConditionMasterSQL()                              '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF540DAC", "GetWarimasi", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RELY_EXTC_NM", "RELY_EXTC_NM")
        map.Add("FRRY_EXTC_NM", "FRRY_EXTC_NM")
        map.Add("CITY_EXTC_NM", "CITY_EXTC_NM")
        map.Add("WINT_EXTC_NM", "WINT_EXTC_NM")
        map.Add("POST_EXTC_NM", "POST_EXTC_NM")
        map.Add("EXTC_TARIFF_REM", "EXTC_TARIFF_REM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_EXTC_UNCHIN_OUT")

        Return ds


    End Function
    ''' <summary>
    '''名称の取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function GetNm(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMF540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMF540DAC.SQL_SELECT_NM)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMF540DAC.SQL_FROM_NM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF540DAC", "GetWarimasi", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("ORIG_JIS_NM", "ORIG_JIS_NM")
        map.Add("DEST_JIS_NM", "DEST_JIS_NM")
        'map.Add("EXTC_TARIFF_REM", "EXTC_TARIFF_REM")



        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMF540IN")

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
        With Me._Row

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORIG_JIS_CD", Me._Row.Item("ORIG_JIS_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", Me._Row.Item("DEST_JIS_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI", Me._Row.Item("KYORI").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KYORI_CD", Me._Row.Item("KYORI_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", Me._Row.Item("UNCHIN_TARIFF_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_CD", Me._Row.Item("EXTC_TARIFF_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@STR_DATE", Me._Row.Item("STR_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA", Me._Row.Item("STR_DATE_4").ToString(), DBDataType.CHAR))




        End With

    End Sub


#End Region

#Region "設定処理"

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

#End Region

#Region "結果格納"

    ''' <summary>
    ''' タリフ情報の結果を格納
    ''' </summary>
    ''' <returns>HashTable</returns>
    ''' <remarks></remarks>
    Private Function SetTariffResult() As Hashtable

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNCHIN_TARIFF_CD", "UNCHIN_TARIFF_CD")
        map.Add("UNCHIN_TARIFF_CD_EDA", "UNCHIN_TARIFF_CD_EDA")
        map.Add("STR_DATE", "STR_DATE")
        map.Add("UNCHIN_TARIFF_REM", "UNCHIN_TARIFF_REM")
        map.Add("DATA_TP", "DATA_TP")
        map.Add("TABLE_TP", "TABLE_TP")
        map.Add("CAR_TP", "CAR_TP")
        map.Add("WT_LV", "WT_LV")
        map.Add("KYORI_1", "KYORI_1")
        map.Add("KYORI_2", "KYORI_2")
        map.Add("KYORI_3", "KYORI_3")
        map.Add("KYORI_4", "KYORI_4")
        map.Add("KYORI_5", "KYORI_5")
        map.Add("KYORI_6", "KYORI_6")
        map.Add("KYORI_7", "KYORI_7")
        map.Add("KYORI_8", "KYORI_8")
        map.Add("KYORI_9", "KYORI_9")
        map.Add("KYORI_10", "KYORI_10")
        map.Add("KYORI_11", "KYORI_11")
        map.Add("KYORI_12", "KYORI_12")
        map.Add("KYORI_13", "KYORI_13")
        map.Add("KYORI_14", "KYORI_14")
        map.Add("KYORI_15", "KYORI_15")
        map.Add("KYORI_16", "KYORI_16")
        map.Add("KYORI_17", "KYORI_17")
        map.Add("KYORI_18", "KYORI_18")
        map.Add("KYORI_19", "KYORI_19")
        map.Add("KYORI_20", "KYORI_20")
        map.Add("KYORI_21", "KYORI_21")
        map.Add("KYORI_22", "KYORI_22")
        map.Add("KYORI_23", "KYORI_23")
        map.Add("KYORI_24", "KYORI_24")
        map.Add("KYORI_25", "KYORI_25")
        map.Add("KYORI_26", "KYORI_26")
        map.Add("KYORI_27", "KYORI_27")
        map.Add("KYORI_28", "KYORI_28")
        map.Add("KYORI_29", "KYORI_29")
        map.Add("KYORI_30", "KYORI_30")
        map.Add("KYORI_31", "KYORI_31")
        map.Add("KYORI_32", "KYORI_32")
        map.Add("KYORI_33", "KYORI_33")
        map.Add("KYORI_34", "KYORI_34")
        map.Add("KYORI_35", "KYORI_35")
        map.Add("KYORI_36", "KYORI_36")
        map.Add("KYORI_37", "KYORI_37")
        map.Add("KYORI_38", "KYORI_38")
        map.Add("KYORI_39", "KYORI_39")
        map.Add("KYORI_40", "KYORI_40")
        map.Add("KYORI_41", "KYORI_41")
        map.Add("KYORI_42", "KYORI_42")
        map.Add("KYORI_43", "KYORI_43")
        map.Add("KYORI_44", "KYORI_44")
        map.Add("KYORI_45", "KYORI_45")
        map.Add("KYORI_46", "KYORI_46")
        map.Add("KYORI_47", "KYORI_47")
        map.Add("KYORI_48", "KYORI_48")
        map.Add("KYORI_49", "KYORI_49")
        map.Add("KYORI_50", "KYORI_50")
        map.Add("KYORI_51", "KYORI_51")
        map.Add("KYORI_52", "KYORI_52")
        map.Add("KYORI_53", "KYORI_53")
        map.Add("KYORI_54", "KYORI_54")
        map.Add("KYORI_55", "KYORI_55")
        map.Add("KYORI_56", "KYORI_56")
        map.Add("KYORI_57", "KYORI_57")
        map.Add("KYORI_58", "KYORI_58")
        map.Add("KYORI_59", "KYORI_59")
        map.Add("KYORI_60", "KYORI_60")
        map.Add("KYORI_61", "KYORI_61")
        map.Add("KYORI_62", "KYORI_62")
        map.Add("KYORI_63", "KYORI_63")
        map.Add("KYORI_64", "KYORI_64")
        map.Add("KYORI_65", "KYORI_65")
        map.Add("KYORI_66", "KYORI_66")
        map.Add("KYORI_67", "KYORI_67")
        map.Add("KYORI_68", "KYORI_68")
        map.Add("KYORI_69", "KYORI_69")
        map.Add("KYORI_70", "KYORI_70")
        map.Add("CITY_EXTC_A", "CITY_EXTC_A")
        map.Add("CITY_EXTC_B", "CITY_EXTC_B")
        map.Add("WINT_EXTC_A", "WINT_EXTC_A")
        map.Add("WINT_EXTC_B", "WINT_EXTC_B")
        map.Add("RELY_EXTC", "RELY_EXTC")
        map.Add("INSU", "INSU")
        map.Add("FRRY_EXTC_10KG", "FRRY_EXTC_10KG")
        map.Add("FRRY_EXTC_PART", "FRRY_EXTC_PART")
        map.Add("UNCHIN_TARIFF_CD2", "UNCHIN_TARIFF_CD2")

        Return map

    End Function

#End Region

#End Region

End Class
