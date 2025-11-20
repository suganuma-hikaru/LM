' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC910    : エスライン CSV出力
'  作  成  者       :  [daikoku]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC910DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC910DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "標準"

    ''' <summary>
    ''' エスラインCSV作成データ検索用SQL SELECT部 
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SLINE_CSV2 As String _
                        = "SELECT AAA.NRS_BR_CD                                " & vbNewLine _
                        & "     ,AAA.OUTKA_NO_L                                " & vbNewLine _
                        & "     ,AAA.OUTKA_PLAN_DATE                            " & vbNewLine _
                        & "     ,AAA.着地コード                                 " & vbNewLine _
                        & "     ,AAA.ADD1                                       " & vbNewLine _
                        & "     ,CASE WHEN AAA.ADD2 IS NULL                     " & vbNewLine _
                        & "         THEN ''                                     " & vbNewLine _
                        & "         ELSE AAA.ADD2 END AS ADDR2                  " & vbNewLine _
                        & "     ,CASE WHEN AAA.ADD3 IS NULL                     " & vbNewLine _
                        & "         THEN ''                                     " & vbNewLine _
                        & "         ELSE AAA.ADD3 END AS ADDR3                  " & vbNewLine _
                        & "     ,AAA.届け先名１                                 " & vbNewLine _
                        & "     ,AAA.届け先名２                                  " & vbNewLine _
                        & "     ,AAA.届け先電話番号                             " & vbNewLine _
                        & "     ,AAA.荷送人住所１                               " & vbNewLine _
                        & "     ,AAA.荷送人住所２                               " & vbNewLine _
                        & "     ,AAA.荷送人住所３                               " & vbNewLine _
                        & "     ,AAA.荷送人名１                                 " & vbNewLine _
                        & "     ,AAA.荷送人名２                                 " & vbNewLine _
                        & "     ,AAA.荷送人電話                                 " & vbNewLine _
                        & "     ,AAA.荷送人コード                               " & vbNewLine _
                        & "     ,AAA.支払人コード                               " & vbNewLine _
                        & "     ,AAA.記事欄１                                   " & vbNewLine _
                        & "     ,AAA.記事欄２                                   " & vbNewLine _
                        & "     ,AAA.記事欄３                                   " & vbNewLine _
                        & "     ,AAA.記事欄４                                   " & vbNewLine _
                        & "     ,AAA.記事欄５                                   " & vbNewLine _
                        & "     ,SUM(AAA.個数) AS NM                            " & vbNewLine _
                        & "     ,AAA.容積                                       " & vbNewLine _
                        & "     ,CEILING(SUM(AAA.重量)) AS SIZE            --UPD 20170417 CEILING追加             " & vbNewLine _
                        & "     ,AAA.オ数                                       " & vbNewLine _
                        & "     ,AAA.ROW_NO                                     " & vbNewLine _
                        & "     ,AAA.FILEPATH                                   " & vbNewLine _
                        & "     ,AAA.FILENAME                                   " & vbNewLine _
                        & "     ,AAA.SYS_DATE                                   " & vbNewLine _
                        & "     ,AAA.SYS_TIME                                   " & vbNewLine _
                        & "  FROM (                                             " & vbNewLine _
                        & "        SELECT  OUTKAL.OUTKA_NO_L                    " & vbNewLine _
                        & "               ,OUTKAL.CUST_CD_L                     " & vbNewLine _
                        & "               ,OUTKAL.NRS_BR_CD                     " & vbNewLine _
                        & "               ,OUTKAL.OUTKA_PLAN_DATE               " & vbNewLine _
                        & "               ,'' AS '着地コード'                   " & vbNewLine _
                        & "               ,CONVERT(VARCHAR(90),OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3) AS JYUSYO                                                                " & vbNewLine _
                        & "               ,CONVERT(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 1, 30)) AS 'ADD1'                              " & vbNewLine _
                        & "               ,DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 1, 30))) AS LENGTHADD1              " & vbNewLine _
                        & "               ,CASE WHEN (DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 1, 30)))=30              " & vbNewLine _
                        & "                           AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 31, 30)))>0)         " & vbNewLine _
                        & "                        THEN convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 31, 30))                          " & vbNewLine _
                        & "                     WHEN (DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 1, 30)))=30              " & vbNewLine _
                        & "                          AND (DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 31, 30)))=0          " & vbNewLine _
                        & "                            OR DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 31, 30))) IS NULL))  " & vbNewLine _
                        & "                         THEN ''                                                                                                                                         " & vbNewLine _
                        & "                     WHEN DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 1, 30)))=29               " & vbNewLine _
                        & "                          AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 30, 30)))>0           " & vbNewLine _
                        & "                        THEN  convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 30, 30))                         " & vbNewLine _
                        & "                    WHEN DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 1, 30)))=29                " & vbNewLine _
                        & "                         AND (DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 30, 30)))=0           " & vbNewLine _
                        & "                           OR DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 30, 30))) IS NULL)    " & vbNewLine _
                        & "                       THEN ''                                                                                                                                           " & vbNewLine _
                        & "                    WHEN DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 1, 30)))<29                " & vbNewLine _
                        & "                         AND (DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_2), 31, 30)))=0           " & vbNewLine _
                        & "                           OR DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_2), 31, 30))) IS NULL)    " & vbNewLine _
                        & "                       THEN '' END      AS 'ADD2'                                                                                                                        " & vbNewLine _
                        & "               ,CASE WHEN (DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 1, 30)))=30              " & vbNewLine _
                        & "                           AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 31, 30)))=30         " & vbNewLine _
                        & "                          AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 61, 30)))>=0)         " & vbNewLine _
                        & "                         THEN convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 61, 30))                         " & vbNewLine _
                        & "                     WHEN (DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 1, 30)))=30              " & vbNewLine _
                        & "                           AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 31, 30)))=30         " & vbNewLine _
                        & "                           AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 61, 30)))IS NULL)    " & vbNewLine _
                        & "                         THEN ''                                                                                                                                         " & vbNewLine _
                        & "                     WHEN (DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 1, 30)))=30              " & vbNewLine _
                        & "                          AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 31, 30)))=29          " & vbNewLine _
                        & "                          AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 60, 30)))>0)          " & vbNewLine _
                        & "                         THEN convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 60, 30))                         " & vbNewLine _
                        & "                     WHEN (DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 1, 30)))=30              " & vbNewLine _
                        & "                           AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 31, 30)))=29         " & vbNewLine _
                        & "                           AND (DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 60, 30)))=0         " & vbNewLine _
                        & "                               OR DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 60, 30))) IS NULL)) " & vbNewLine _
                        & "                          THEN ''                                                                                                                                         " & vbNewLine _
                        & "                     WHEN (DATALENGTH(convert(varchar(30), substring(convert(text,OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 1, 30)))=30                " & vbNewLine _
                        & "                           AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 31, 30)))<29          " & vbNewLine _
                        & "                           AND (DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 60, 30)))=0          " & vbNewLine _
                        & "                            OR DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 60, 30))) IS NULL ))  " & vbNewLine _
                        & "                         THEN ''                                                                                                                                          " & vbNewLine _
                        & "                     WHEN (DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_2), 1, 30)))=29               " & vbNewLine _
                        & "                          AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 30, 30)))=30           " & vbNewLine _
                        & "                          AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_3+' '+OUTKAL.DEST_AD_3), 60, 30)))>=0)          " & vbNewLine _
                        & "                         THEN convert(varchar(30), substring(convert(text,OUTKAL.DEST_AD_1 +' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 60, 30))                          " & vbNewLine _
                        & "                    WHEN (DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_2), 1, 30)))=29                " & vbNewLine _
                        & "                          AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 30, 30)))=30           " & vbNewLine _
                        & "                          AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_3+' '+OUTKAL.DEST_AD_3), 60, 30))) IS NULL)     " & vbNewLine _
                        & "                        THEN ''                                                                                                                                           " & vbNewLine _
                        & "                    WHEN (DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 1, 30)))=29                " & vbNewLine _
                        & "                          AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 30, 30)))=29           " & vbNewLine _
                        & "                          AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 59, 30)))>=0)          " & vbNewLine _
                        & "                        THEN convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 59, 30))                           " & vbNewLine _
                        & "                    WHEN (DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 1, 30)))=29                " & vbNewLine _
                        & "                          AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 30, 30)))=29           " & vbNewLine _
                        & "                          AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 59, 30))) IS NULL)     " & vbNewLine _
                        & "                        THEN ''                                                                                                                                           " & vbNewLine _
                        & "                    WHEN (DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 1, 30)))=29                " & vbNewLine _
                        & "                         AND DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 30, 30)))<29            " & vbNewLine _
                        & "                         AND (DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 59, 30)))=0            " & vbNewLine _
                        & "                           OR DATALENGTH(convert(varchar(30), substring(convert(text, OUTKAL.DEST_AD_1+' '+OUTKAL.DEST_AD_2+' '+OUTKAL.DEST_AD_3), 59, 30))) IS NULL))    " & vbNewLine _
                        & "                        THEN '' END      AS 'ADD3'                                                                                                                        " & vbNewLine _
                        & "               ,CONVERT(VARCHAR(60),OUTKAL.DEST_NM) AS '届け先名'                                                                                                         " & vbNewLine _
                        & "               ,CONVERT(varchar(30), SUBSTRING(CONVERT(TEXT,OUTKAL.DEST_NM),1,30)) AS '届け先名1'                                                                         " & vbNewLine _
                        & "               ,DATALENGTH(CONVERT(varchar(30), SUBSTRING(CONVERT(TEXT,OUTKAL.DEST_NM),1,30))) AS 'LENGTHNM1'                                                             " & vbNewLine _
                        & "               ,CASE WHEN (DATALENGTH(CONVERT(varchar(30), SUBSTRING(CONVERT(TEXT,OUTKAL.DEST_NM),1,30)))=30                                                              " & vbNewLine _
                        & "                          AND DATALENGTH(CONVERT(varchar(30), SUBSTRING(CONVERT(TEXT,OUTKAL.DEST_NM),31,30)))>0)                                                          " & vbNewLine _
                        & "                         THEN CONVERT(varchar(30), SUBSTRING(CONVERT(TEXT,OUTKAL.DEST_NM),31,30))                                                                         " & vbNewLine _
                        & "                     WHEN (DATALENGTH(CONVERT(varchar(30), SUBSTRING(CONVERT(TEXT,OUTKAL.DEST_NM),1,30)))=30                                                              " & vbNewLine _
                        & "                          AND (DATALENGTH(CONVERT(varchar(30), SUBSTRING(CONVERT(TEXT,OUTKAL.DEST_NM),31,30)))=0                                                          " & vbNewLine _
                        & "                           OR DATALENGTH(DATALENGTH(CONVERT(varchar(30), SUBSTRING(CONVERT(TEXT,OUTKAL.DEST_NM),31,30)))) IS NULL))                                       " & vbNewLine _
                        & "                         THEN ''                                                                                                                                          " & vbNewLine _
                        & "                    WHEN  (DATALENGTH(CONVERT(varchar(30), SUBSTRING(CONVERT(TEXT,OUTKAL.DEST_NM),1,30)))=29                                                              " & vbNewLine _
                        & "                           AND DATALENGTH(CONVERT(varchar(30), SUBSTRING(CONVERT(TEXT,OUTKAL.DEST_NM),30,30)))>0)                                                         " & vbNewLine _
                        & "                        THEN (CONVERT(varchar(30), SUBSTRING(CONVERT(TEXT,OUTKAL.DEST_NM),30,30)))                                                                        " & vbNewLine _
                        & "                    WHEN (DATALENGTH(CONVERT(varchar(30), SUBSTRING(CONVERT(TEXT,OUTKAL.DEST_NM),1,30)))=29                                                               " & vbNewLine _
                        & "                          AND (DATALENGTH(CONVERT(varchar(30), SUBSTRING(CONVERT(TEXT,OUTKAL.DEST_NM),30,30)))=0                                                          " & vbNewLine _
                        & "                           OR DATALENGTH(CONVERT(varchar(30), SUBSTRING(CONVERT(TEXT,OUTKAL.DEST_NM),30,30))) IS NULL))                                                   " & vbNewLine _
                        & "                        THEN ''                                                                                                                                           " & vbNewLine _
                        & "                    WHEN DATALENGTH(CONVERT(varchar(30), SUBSTRING(CONVERT(TEXT,OUTKAL.DEST_NM),1,30)))<29                                                                " & vbNewLine _
                        & "                         AND (DATALENGTH(CONVERT(varchar(30), SUBSTRING(CONVERT(TEXT,OUTKAL.DEST_NM),30,30)))=0                                                           " & vbNewLine _
                        & "                          OR DATALENGTH(CONVERT(varchar(30), SUBSTRING(CONVERT(TEXT,OUTKAL.DEST_NM),30,30))) IS NULL)                                                     " & vbNewLine _
                        & "                        THEN ''                                                                                                                                           " & vbNewLine _
                        & "               END       AS '届け先名２'                                                                                                                                   " & vbNewLine _
                        & "              ,OUTKAL.DEST_TEL           AS '届け先電話番号'                                                  " & vbNewLine _
                        & "              ,NRS.AD_1                  AS '荷送人住所１'                                                    " & vbNewLine _
                        & "              ,NRS.AD_2                  AS '荷送人住所２'                                                    " & vbNewLine _
                        & "              ,NRS.AD_3                  AS '荷送人住所３'                                                    " & vbNewLine _
                        & "--              ,LEFT(NRS.WH_NM,7)         AS '荷送人名１'                                                    " & vbNewLine _
                        & "--              ,RIGHT(NRS.WH_NM,12)       AS '荷送人名２'                                                    " & vbNewLine _
                        & "              ,LEFT(NRS.WH_NM,CONVERT(int,CHARINDEX('　', NRS.WH_NM,1)))             AS '荷送人名１'          " & vbNewLine _
                        & "              ,SUBSTRING(NRS.WH_NM,CONVERT(int,CHARINDEX('　', NRS.WH_NM,1)) + 1,30) AS '荷送人名２'          " & vbNewLine _
                        & "              ,NRS.TEL                   AS '荷送人電話'                                                      " & vbNewLine _
                        & "--              ,'045508155100'            AS '荷送人コード'                                                  " & vbNewLine _
                        & "--              ,'045508155100'            AS '支払人コード'                                                  " & vbNewLine _
                        & "              ,REPLACE(NRS.TEL,'-','') + '00'            AS '荷送人コード'                                    " & vbNewLine _
                        & "              ,REPLACE(NRS.TEL,'-','') + '00'            AS '支払人コード'                                    " & vbNewLine _
                        & "              ,CASE WHEN (OUTKAL.ARR_PLAN_TIME IS NULL OR OUTKAL.ARR_PLAN_TIME='')                            " & vbNewLine _
                        & "                        THEN LEFT(OUTKAL.ARR_PLAN_DATE,4)+'年'                                                " & vbNewLine _
                        & "                             +SUBSTRING(OUTKAL.ARR_PLAN_DATE,5,2)+'月'                                        " & vbNewLine _
                        & "                             +SUBSTRING(OUTKAL.ARR_PLAN_DATE,7,2)+'日'  + '必着'                              " & vbNewLine _
                        & "                    WHEN OUTKAL.ARR_PLAN_TIME IS NOT NULL                                                     " & vbNewLine _
                        & "                        THEN LEFT(OUTKAL.ARR_PLAN_DATE,4)+'年'                                                " & vbNewLine _
                        & "                            +SUBSTRING(OUTKAL.ARR_PLAN_DATE,5,2)+'月'                                         " & vbNewLine _
                        & "                            +SUBSTRING(OUTKAL.ARR_PLAN_DATE,7,2)+'日' + PULLDOWN.KBN_NM1 END AS '記事欄１'    " & vbNewLine _
                        & "              ,CUST.CUST_NM_L + '様扱い' AS '記事欄２'                                                        " & vbNewLine _
                        & "              ,OUTKAL.CUST_ORD_NO AS '記事欄３'                                                               " & vbNewLine _
                        & "              ,OUTKAL.BUYER_ORD_NO AS '記事欄４'                                                              " & vbNewLine _
                        & "              ,OUTKAL.REMARK AS MARK                                                                          " & vbNewLine _
                        & "              ,CONVERT(VARCHAR(40),OUTKAL.REMARK) AS '記事欄５'                                               " & vbNewLine _
                        & "              ,'' AS '容積'                                                                                   " & vbNewLine _
                        & "              ,'' AS 'オ数'                                                                                   " & vbNewLine _
                        & "--UPD 2018/05/31              ,UNSO.UNSO_PKG_NB AS '個数'                                                     " & vbNewLine _
                        & "              ,OUTKAL.OUTKA_PKG_NB  AS '個数'                                                             " & vbNewLine _
                        & "              ,UNSO.UNSO_WT AS '重量'                                                                         " & vbNewLine _
                        & "              ,@ROW_NO                 AS  ROW_NO                                                             " & vbNewLine _
                        & "              ,@FILEPATH               AS  FILEPATH                                                           " & vbNewLine _
                        & "              ,@FILENAME               AS  FILENAME                                                           " & vbNewLine _
                        & "              ,@SYS_DATE               AS  SYS_DATE                                                           " & vbNewLine _
                        & "              ,@SYS_TIME               AS  SYS_TIME                                                           " & vbNewLine _
                        & "       FROM $LM_TRN$..C_OUTKA_L     AS OUTKAL                                                                 " & vbNewLine _
                        & "       LEFT JOIN $LM_TRN$..F_UNSO_L AS UNSO                                                                   " & vbNewLine _
                        & "         ON OUTKAL.OUTKA_NO_L =UNSO.INOUTKA_NO_L                                                              " & vbNewLine _
                        & "        AND UNSO.SYS_DEL_FLG='0'                                                                              " & vbNewLine _
                        & "        AND UNSO.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                                 " & vbNewLine _
                        & "       LEFT JOIN $LM_MST$..M_SOKO   AS NRS                                                                    " & vbNewLine _
                        & "         ON OUTKAL.NRS_BR_CD = NRS.NRS_BR_CD                                                                  " & vbNewLine _
                        & "        AND OUTKAL.WH_CD = NRS.WH_CD                                                                          " & vbNewLine _
                        & "        AND NRS.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                                  " & vbNewLine _
                        & "        AND NRS.SYS_DEL_FLG='0'                                                                               " & vbNewLine _
                        & "       LEFT JOIN $LM_MST$..M_CUST   AS CUST                                                                   " & vbNewLine _
                        & "         ON CUST.CUST_CD_L = OUTKAL.CUST_CD_L                                                                 " & vbNewLine _
                        & "        AND CUST.CUST_CD_M = OUTKAL.CUST_CD_M                                                                 " & vbNewLine _
                        & "        AND CUST.CUST_CD_S = '00'                                                                             " & vbNewLine _
                        & "        AND CUST.CUST_CD_SS ='00'                                                                             " & vbNewLine _
                        & "        AND CUST.NRS_BR_CD   = OUTKAL.NRS_BR_CD                                                               " & vbNewLine _
                        & "        AND CUST.SYS_DEL_FLG = '0'                                                                            " & vbNewLine _
                        & "       LEFT JOIN $LM_MST$..Z_KBN    AS PULLDOWN                                                               " & vbNewLine _
                        & "         ON OUTKAL.ARR_PLAN_TIME= PULLDOWN.KBN_CD                                                             " & vbNewLine _
                        & "        AND PULLDOWN.KBN_GROUP_CD='N010'                                                                      " & vbNewLine _
                        & "        AND PULLDOWN.SYS_DEL_FLG='0'                                                                          " & vbNewLine _
                        & "       WHERE OUTKAL.NRS_BR_CD  = @NRS_BR_CD                                                                   " & vbNewLine _
                        & "         AND OUTKAL.OUTKA_NO_L = @OUTKA_NO_L                                                                  " & vbNewLine _
                        & "       ) AS AAA                                                                                               " & vbNewLine _
                        & "GROUP BY AAA.OUTKA_NO_L                                                                                       " & vbNewLine _
                        & "         ,AAA.NRS_BR_CD                                                                                       " & vbNewLine _
                        & "         ,AAA.OUTKA_PLAN_DATE                                                                                 " & vbNewLine _
                        & "         ,AAA.着地コード                                                                                      " & vbNewLine _
                        & "         ,AAA.ADD1                                                                                            " & vbNewLine _
                        & "         ,CASE WHEN AAA.ADD2 IS NULL THEN ''                                                                  " & vbNewLine _
                        & "             ELSE AAA.ADD2 END                                                                                " & vbNewLine _
                        & "         ,CASE WHEN AAA.ADD3 IS NULL THEN ''                                                                  " & vbNewLine _
                        & "             ELSE AAA.ADD3 END                                                                                " & vbNewLine _
                        & "         ,AAA.届け先名１                                                                                      " & vbNewLine _
                        & "         ,AAA.届け先名２                                                                                       " & vbNewLine _
                        & "         ,AAA.届け先電話番号                                                                                  " & vbNewLine _
                        & "         ,AAA.荷送人住所１                                                                                    " & vbNewLine _
                        & "         ,AAA.荷送人住所２                                                                                    " & vbNewLine _
                        & "         ,AAA.荷送人住所３                                                                                    " & vbNewLine _
                        & "         ,AAA.荷送人名１                                                                                      " & vbNewLine _
                        & "         ,AAA.荷送人名２                                                                                      " & vbNewLine _
                        & "         ,AAA.荷送人電話                                                                                      " & vbNewLine _
                        & "         ,AAA.荷送人コード                                                                                    " & vbNewLine _
                        & "         ,AAA.支払人コード                                                                                    " & vbNewLine _
                        & "         ,AAA.記事欄１                                                                                        " & vbNewLine _
                        & "         ,AAA.記事欄２                                                                                        " & vbNewLine _
                        & "         ,AAA.記事欄３                                                                                        " & vbNewLine _
                        & "         ,AAA.記事欄４                                                                                        " & vbNewLine _
                        & "         ,AAA.記事欄５                                                                                        " & vbNewLine _
                        & "         ,AAA.容積                                                                                            " & vbNewLine _
                        & "         ,AAA.オ数                                                                                            " & vbNewLine _
                        & "         ,AAA.ROW_NO                                                                                          " & vbNewLine _
                        & "         ,AAA.FILEPATH                                                                                        " & vbNewLine _
                        & "         ,AAA.FILENAME                                                                                        " & vbNewLine _
                        & "         ,AAA.SYS_DATE                                                                                        " & vbNewLine _
                        & "         ,AAA.SYS_TIME                                                                                        " & vbNewLine






    ''' <summary>
    ''' エスラインCSV作成データ検索用SQL FROM・WHERE部 標準用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SLINE_CSV_FROM2 As String _
        = " FROM $LM_TRN$..C_OUTKA_L OUTKAL                                                         " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_DEST DEST ON                                                      " & vbNewLine _
        & "      DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                              " & vbNewLine _
        & "      DEST.CUST_CD_L = OUTKAL.CUST_CD_L AND                                              " & vbNewLine _
        & "      DEST.DEST_CD = OUTKAL.DEST_CD                                                      " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_CUST CUST ON                                                      " & vbNewLine _
        & "      CUST.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                              " & vbNewLine _
        & "      CUST.CUST_CD_L = OUTKAL.CUST_CD_L AND                                              " & vbNewLine _
        & "      CUST.CUST_CD_M = OUTKAL.CUST_CD_M AND                                              " & vbNewLine _
        & "      CUST.CUST_CD_S = '00' AND                                                          " & vbNewLine _
        & "      CUST.CUST_CD_SS = '00'                                                             " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_SOKO SOKO  ON                                                     " & vbNewLine _
        & "      SOKO.NRS_BR_CD = OUTKAL.NRS_BR_CD AND                                              " & vbNewLine _
        & "      SOKO.WH_CD     = OUTKAL.WH_CD                                                      " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKAM ON                                                 " & vbNewLine _
        & "           OUTKAM.NRS_BR_CD   = OUTKAL.NRS_BR_CD                                         " & vbNewLine _
        & "       AND OUTKAM.OUTKA_NO_L  = OUTKAL.OUTKA_NO_L                                        " & vbNewLine _
        & "       AND OUTKAM.SYS_DEL_FLG  = '0'                                                     " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_GOODS GOODS ON                                                    " & vbNewLine _
        & "           GOODS.NRS_BR_CD    = OUTKAM.NRS_BR_CD                                         " & vbNewLine _
        & "       AND GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                                      " & vbNewLine _
        & " LEFT JOIN LM_MST..M_NRS_BR AS NRSBR                                                     " & vbNewLine _
        & "   ON NRSBR.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                 " & vbNewLine _
        & " LEFT JOIN (                                                                             " & vbNewLine _
        & "            SELECT                                                                       " & vbNewLine _
        & "                  NRS_BR_CD                                                              " & vbNewLine _
        & "                , EDI_CTL_NO                                                             " & vbNewLine _
        & "                , OUTKA_CTL_NO                                                           " & vbNewLine _
        & "            FROM (                                                                       " & vbNewLine _
        & "                    SELECT                                                               " & vbNewLine _
        & "                          EDIOUTL.NRS_BR_CD                                              " & vbNewLine _
        & "                        , EDIOUTL.EDI_CTL_NO                                             " & vbNewLine _
        & "                        , EDIOUTL.OUTKA_CTL_NO                                           " & vbNewLine _
        & "                        , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                     " & vbNewLine _
        & "                          ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD         " & vbNewLine _
        & "                                                             , EDIOUTL.OUTKA_CTL_NO      " & vbNewLine _
        & "                                                      ORDER BY EDIOUTL.NRS_BR_CD         " & vbNewLine _
        & "                                                             , EDIOUTL.EDI_CTL_NO        " & vbNewLine _
        & "                                                  )                                      " & vbNewLine _
        & "                          END AS IDX                                                     " & vbNewLine _
        & "                    FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                                 " & vbNewLine _
        & "                    WHERE EDIOUTL.SYS_DEL_FLG  = '0'                                     " & vbNewLine _
        & "                      AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD                              " & vbNewLine _
        & "                      AND EDIOUTL.OUTKA_CTL_NO = @OUTKA_NO_L                             " & vbNewLine _
        & "                  ) EBASE                                                                " & vbNewLine _
        & "            WHERE EBASE.IDX = 1                                                          " & vbNewLine _
        & "            ) TOPEDI                                                                     " & vbNewLine _
        & "        ON TOPEDI.NRS_BR_CD               = OUTKAL.NRS_BR_CD                             " & vbNewLine _
        & "       AND TOPEDI.OUTKA_CTL_NO            = OUTKAL.OUTKA_NO_L                            " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                   " & vbNewLine _
        & "        ON EDIL.NRS_BR_CD                 = TOPEDI.NRS_BR_CD                             " & vbNewLine _
        & "       AND EDIL.EDI_CTL_NO                = TOPEDI.EDI_CTL_NO                            " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                                                      " & vbNewLine _
        & "   ON UNSOL.NRS_BR_CD                     = OUTKAL.NRS_BR_CD                             " & vbNewLine _
        & "  AND UNSOL.INOUTKA_NO_L                  = OUTKAL.OUTKA_NO_L                            " & vbNewLine _
        & "  AND UNSOL.MOTO_DATA_KB                  = '20'                                         " & vbNewLine _
        & "  AND UNSOL.SYS_DEL_FLG                   = '0'                                          " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSV     --既存マスタJOIN                       " & vbNewLine _
        & "   ON OKURIJOCSV.NRS_BR_CD                = UNSOL.NRS_BR_CD                              " & vbNewLine _
        & "  AND OKURIJOCSV.UNSOCO_CD                = UNSOL.UNSO_CD                                " & vbNewLine _
        & "  AND OKURIJOCSV.CUST_CD_L                = UNSOL.CUST_CD_L                              " & vbNewLine _
        & "  AND OKURIJOCSV.OKURIJO_TP               = '04' --エスライン                                " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSVX    --追加マスタJOIN                       " & vbNewLine _
        & "   ON OKURIJOCSVX.NRS_BR_CD               = UNSOL.NRS_BR_CD                              " & vbNewLine _
        & "  AND OKURIJOCSVX.UNSOCO_CD               = UNSOL.UNSO_CD                                " & vbNewLine _
        & "  AND OKURIJOCSVX.CUST_CD_L               = 'XXXXX'                                      " & vbNewLine _
        & "  AND OKURIJOCSVX.OKURIJO_TP              = '04' --エスライン                                " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJO_KBN    --追加マスタJOIN(区分用)  20161122    " & vbNewLine _
        & "   ON OKURIJO_KBN.NRS_BR_CD               = UNSOL.NRS_BR_CD                              " & vbNewLine _
        & "  AND OKURIJO_KBN.UNSOCO_CD               = UNSOL.UNSO_CD                                " & vbNewLine _
        & "  AND OKURIJO_KBN.CUST_CD_L               = 'XXXXX'                                      " & vbNewLine _
        & "  AND OKURIJO_KBN.OKURIJO_TP              = '04' --エスライン                                " & vbNewLine _
        & "  AND OKURIJO_KBN.FREE_C20 = UNSOl.UNSO_BR_CD                                            " & vbNewLine _
        & "  AND OKURIJO_KBN.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..Z_KBN KBNZ024 --                                                   " & vbNewLine _
        & "  ON KBNZ024.KBN_GROUP_CD = 'Z024'                                                       " & vbNewLine _
        & "  AND KBNZ024.KBN_CD = OKURIJO_KBN.FREE_C19                                              " & vbNewLine _
        & "  AND KBNZ024.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_DEST SHIP_CD_DEST                                                 " & vbNewLine _
        & "   ON SHIP_CD_DEST.NRS_BR_CD = OUTKAL.NRS_BR_CD                                          " & vbNewLine _
        & "  AND SHIP_CD_DEST.CUST_CD_L = OUTKAL.CUST_CD_L                                          " & vbNewLine _
        & "  AND SHIP_CD_DEST.DEST_CD   = OUTKAL.SHIP_CD_L                                          " & vbNewLine _
        & "--荷主明細M  丸和(横浜)対応                                                              " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_CUST_DETAILS MCD                                                  " & vbNewLine _
        & "   ON MCD.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                   " & vbNewLine _
        & "  AND MCD.CUST_CD   = OUTKAL.CUST_CD_L + OUTKAL.CUST_CD_M                                " & vbNewLine _
        & "  AND MCD.SUB_KB    = '0S'                                                               " & vbNewLine _
        & "LEFT JOIN (                                     " & vbNewLine _
        & "    SELECT                                      " & vbNewLine _
        & "        TAX.TAX_RATE   AS TAX_RATE              " & vbNewLine _
        & "      , TAX.START_DATE AS START_DATE            " & vbNewLine _
        & "    FROM                                        " & vbNewLine _
        & "       $LM_MST$..M_TAX TAX                      " & vbNewLine _
        & "    INNER JOIN (                                " & vbNewLine _
        & "        SELECT                                  " & vbNewLine _
        & "            KBN1.KBN_GROUP_CD                   " & vbNewLine _
        & "          , KBN1.KBN_CD                         " & vbNewLine _
        & "          , KBN1.KBN_NM3                        " & vbNewLine _
        & "          , TAX2.START_DATE                     " & vbNewLine _
        & "          , TAX2.TAX_CD                         " & vbNewLine _
        & "        FROM (                                  " & vbNewLine _
        & "            SELECT                              " & vbNewLine _
        & "                MAX(START_DATE) AS START_DATE   " & vbNewLine _
        & "              , TAX3.TAX_CD     AS TAX_CD       " & vbNewLine _
        & "            FROM                                " & vbNewLine _
        & "               $LM_MST$..M_TAX TAX3             " & vbNewLine _
        & "            WHERE                               " & vbNewLine _
        & "                TAX3.START_DATE <= @OUTKA_PLAN_DATE   " & vbNewLine _
        & "            GROUP BY                            " & vbNewLine _
        & "                TAX3.TAX_CD) TAX2               " & vbNewLine _
        & "        INNER JOIN                              " & vbNewLine _
        & "           $LM_MST$..Z_KBN KBN1                 " & vbNewLine _
        & "         ON KBN1.KBN_GROUP_CD = 'Z001'          " & vbNewLine _
        & "        AND KBN1.KBN_CD = '01'                  " & vbNewLine _
        & "        AND KBN1.KBN_NM3 = TAX2.TAX_CD) TAX1    " & vbNewLine _
        & "     ON TAX1.START_DATE = TAX.START_DATE        " & vbNewLine _
        & "    AND TAX1.KBN_NM3 = TAX.TAX_CD)TAX           " & vbNewLine _
        & "  ON TAX.START_DATE <= @OUTKA_PLAN_DATE         " & vbNewLine _
        & " WHERE OUTKAL.NRS_BR_CD      = @NRS_BR_CD                                                " & vbNewLine _
        & "   AND OUTKAL.OUTKA_NO_L     = @OUTKA_NO_L                                               " & vbNewLine _
        & "   AND OUTKAL.SYS_DEL_FLG    = '0'                                                       " & vbNewLine _
        & "   AND NOT EXISTS (SELECT * FROM LM_MST..M_CUST_DETAILS                                  " & vbNewLine _
        & "                            WHERE M_CUST_DETAILS.NRS_BR_CD   = OUTKAL.NRS_BR_CD          " & vbNewLine _
        & "                              AND M_CUST_DETAILS.CUST_CD     = OUTKAL.CUST_CD_L          " & vbNewLine _
        & "                              AND M_CUST_DETAILS.SUB_KB      = '0M'                      " & vbNewLine _
        & "                              AND M_CUST_DETAILS.SYS_DEL_FLG = '0'                       " & vbNewLine _
        & "                              AND M_CUST_DETAILS.SET_NAIYO   = '1')                      " & vbNewLine



#End Region


#End Region

#Region "更新 SQL"

#Region "エスラインCSV作成"

    Private Const SQL_UPDATE_SLINE_CSV As String = "UPDATE $LM_TRN$..C_OUTKA_L SET              " & vbNewLine _
                                             & " DENP_FLAG         = '01'                         " & vbNewLine _
                                             & ",SYS_UPD_DATE      = @SYS_UPD_DATE                " & vbNewLine _
                                             & ",SYS_UPD_TIME      = @SYS_UPD_TIME                " & vbNewLine _
                                             & ",SYS_UPD_PGID      = @SYS_UPD_PGID                " & vbNewLine _
                                             & ",SYS_UPD_USER      = @SYS_UPD_USER                " & vbNewLine _
                                             & "WHERE NRS_BR_CD    = @NRS_BR_CD                   " & vbNewLine _
                                             & "  AND OUTKA_NO_L   = @OUTKA_NO_L                  " & vbNewLine

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
    ''' エスラインCSV作成対象検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>エスラインCSV作成対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectSLineCsv(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC910IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC910DAC.SQL_SELECT_SLINE_CSV2)
        ' ''Me._StrSql.Append(LMC910DAC.SQL_SELECT_SLINE_CSV_FROM2)

        Call setSQLSelect()                   '条件設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", Me._Row("ROW_NO"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILEPATH", Me._Row("FILEPATH"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILENAME", Me._Row("FILENAME"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", Me._Row("SYS_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_TIME", Me._Row("SYS_TIME"), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC910DAC", "SelectSLINECsv", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("着地コード", "着地コード")
        map.Add("ADD1", "ADD1")
        map.Add("ADDR2", "ADDR2")
        map.Add("ADDR3", "ADDR3")
        map.Add("届け先名１", "届け先名１")
        map.Add("届け先名２", "届け先名２")
        map.Add("届け先電話番号", "届け先電話番号")
        map.Add("荷送人住所１", "荷送人住所１")
        map.Add("荷送人住所２", "荷送人住所２")
        map.Add("荷送人住所３", "荷送人住所３")
        map.Add("荷送人名１", "荷送人名１")
        map.Add("荷送人名２", "荷送人名２")
        map.Add("荷送人電話", "荷送人電話")
        map.Add("荷送人コード", "荷送人コード")
        map.Add("支払人コード", "支払人コード")
        map.Add("記事欄１", "記事欄１")
        map.Add("記事欄２", "記事欄２")
        map.Add("記事欄３", "記事欄３")
        map.Add("記事欄４", "記事欄４")
        map.Add("記事欄５", "記事欄５")
        map.Add("NM", "NM")
        map.Add("容積", "容積")
        map.Add("SIZE", "SIZE")
        map.Add("オ数", "オ数")
        map.Add("ROW_NO", "ROW_NO")
        'map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        'map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("FILEPATH", "FILEPATH")
        map.Add("FILENAME", "FILENAME")
        map.Add("SYS_DATE", "SYS_DATE")
        map.Add("SYS_TIME", "SYS_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC910OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMC910OUT").Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 出荷Lテーブル更新（エスラインCSV作成時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateSLineCsv(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMC910OUT").Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamCommonSystemUp()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC910DAC.SQL_UPDATE_SLINE_CSV, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC910DAC", "UpdateSLINECsv", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, True)

        Return ds

    End Function

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

    ''' <summary>
    '''  パラメータ設定モジュール（出荷検索）
    ''' </summary>
    ''' <remarks>出荷マスタ検索用SQLの構築</remarks>
    Private Sub setSQLSelect()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L"), DBDataType.CHAR))

    End Sub

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
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <param name="setFlg">セットフラグ False:通常のメッセージセット True:一括更新のメッセージセット</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand, Optional ByVal setFlg As Boolean = False) As Boolean

        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd), setFlg)

    End Function

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="setFlg">セットフラグ False:通常のメッセージセット True:一括更新のメッセージセット</param>
    ''' <param name="cnt">カウント</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cnt As Integer, Optional ByVal setFlg As Boolean = False) As Boolean

        '判定
        If cnt < 1 Then
            If setFlg = False Then
                MyBase.SetMessage("E011")
            Else
                MyBase.SetMessageStore("00", "E011", , Me._Row.Item("ROW_NO").ToString())
            End If
            Return False
        End If

        Return True

    End Function

#End Region

#End Region


#End Region

End Class
