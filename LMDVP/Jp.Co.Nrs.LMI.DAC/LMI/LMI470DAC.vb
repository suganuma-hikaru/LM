' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI470  : 日本合成　物流費送信
'  作  成  者       :  [daikoku]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI470DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI470DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "物流費チェック"

#Region "物流費チェック"

    ''' <summary>
    ''' 物流費チェック運賃
    ''' </summary>
    ''' <remarks></remarks>
    ''' 

    Private Const SQL_BUTSURYUHI_CHK1 As String = " 	SELECT 				  " & vbNewLine _
                                        & " 	　  '1'    as SQ_NO				  " & vbNewLine _
                                        & " 	　  ,isnull(sum(  CASE WHEN RN = 1 THEN  CONVERT(INT,NET_AMT) + CONVERT(INT,SAI) 				  " & vbNewLine _
                                        & " 	　                      ELSE  CONVERT(INT,NET_AMT)        END),0)   as NET_AMT  				  " & vbNewLine _
                                        & " 	　　FROM (				  " & vbNewLine

    Private Const SQL_BUTSURYUHI_DATA1 As String = " SELECT 				  " & vbNewLine _
                                        & " 	    -- 運賃--				  " & vbNewLine _
                                        & " 	    CONVERT(char(4),RECORD_ID)               --レコードID				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(3),DATA_ID)                 --データID         データIDの3桁目：V     (日陸様を表すコードです)				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(3),COMP_CD)                 --会社コード 040   (日本合成化学工業を表すコードです)				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(6),KEIJYO_YM)               --計上年月				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(12),UKETUKE_NO)             --受付				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(12),BUTURYU_SERI_NO + 'KU' +  FORMAT(seq,'000000'))      --物流整理№鑑より				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(3),TEISEI_NO)          --訂正No.				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),TEISEI_KBN)         --訂正区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),KINOU)              --機能				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),KINOU_SAIMOKU)      --機能細目				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),SAGYO_N_KBN)           --作業内容区分  	     			  " & vbNewLine _
                                        & " 	 +  CONVERT(char(8),SYORI_YMD)          --伝送処理年月日				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(9),SYORI_JIKAN)        --伝送処理時間				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),NYURYOKU_BASYO)     --入力場所				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),NYURYOKU_BUMON)     --入力部門				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),NYURYOKU_GROUP)     --入力グループ				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(8),SAGYOU_DATE)        --作業年月日				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(10),HINMEI_RYAKUGO)    --品目略号				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(5),UG_CD)              --UGコード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),NISUGATA)           --荷姿				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(8),YOURYO)             --容量				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),KEIYAKU_BASYO)      --契約場所 'AA'固定				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),BIN_KBN)            --便区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(7),BUTSURYU_COMP_CD)   --物流会社コード H441B (日陸様を表すコードです)				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(10),TSUMEAWASE_NO)     --詰合わせNo.				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(7),JITSU_USER)         --実ユーザー				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(7),KEIYAKUSAKI_CD)     --契約先コード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),KOTEIHI_HEND_KBN)   --固定費・変動費区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),HANBAI_BUNRUI)      --販売分類				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),HACCHI_KBN)         --発地情報　発地区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(7),HACCHI_CD)          --　　　　　　　発地コード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(6),HACCHI_CIKU_CD)     --　　　　　　　市区町村コード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),CHAKUCHI_KBN)           --着地情報　着地区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(7),CHAKUCHI_CD)            --　　　　　　　着地コード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(6),CHAKUCHI_CHIKU_CD)      --　　　　　　　市区町村コード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(20),CHAKUCHI_NOUNYU_BASYO) --　　　　　　　納入場所名				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),HASSEI_BASYO_KBN)       --発生場所区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(7),HASEI_SP_CD)            --発生SPコード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),SIHARAI_BASYO)          --支払場所				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),BUTURYU_TANI)           --物流単位				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),BUTURYU_FUGO)           --物流量符号				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(16),BUTURYU_RYO)           --物流量				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(10),LOT_NO)                --ロット№				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),TAX_BN)                 --税区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),GN_KBN)                 --GN区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),NET_AMT_FUGO)           --NET金額符号				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(15),CASE WHEN RN = 1 THEN  FORMAT(CONVERT(INT,NET_AMT) + CONVERT(INT,SAI) ,'000000000000000')				  " & vbNewLine _
                                        & " 	                    ELSE  FORMAT(CONVERT(INT,NET_AMT),'000000000000000')        END)      --NET金額				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),GROSS_AMT_FUGO)        --GROSS金額符号				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(15),CASE WHEN RN = 1 THEN  FORMAT((CONVERT(INT,NET_AMT) + CONVERT(INT,SAI))* (1 + ZEIRITSU),'000000000000000')				  " & vbNewLine _
                                        & " 	                     ELSE  FORMAT(CONVERT(INT,NET_AMT) * (1 + ZEIRITSU),'000000000000000')        END)      --GROSS金額				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),TAX_FUGO)               --税額符号				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(15),CASE WHEN RN = 1 THEN  FORMAT((CONVERT(INT,NET_AMT) + CONVERT(INT,SAI)) *  ZEIRITSU,'000000000000000')				  " & vbNewLine _
                                        & " 	                     ELSE  FORMAT(CONVERT(INT,NET_AMT) * ZEIRITSU,'000000000000000')        END)      --税額				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),ZEIRITU_FUGO)          --税率符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(6),FORMAT(ZEIRITSU * 100,'000.00'))              --税率				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),YUSO_KYORI_FUGO)       --輸送距離符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),YUSOKYORI)             --輸送距離				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),SAIMOKU1_CD)               --項目細分１　項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SAIMOKU1_BETSU_AMT_FUGO)   --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SAIMOKU1_BETU_AMT)          --　　　　　　　　項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),SAIMOKU2_CD)                --項目細分２　項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SAIMOKU2_BETSU_AMT_FUGO)    --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SAIMOKU2_BETU_AMT)          --　　　　　　　　項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),SAIMOKU3_CD)                --項目細分３　項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SAIMOKU3_BETSU_AMT_FUGO)    --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SAIMOKU3_BETU_AMT)          --　　　　　　　　項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),SAIMOKU4_CD)                --項目細分４　項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SAIMOKU4_BETSU_AMT_FUGO)    --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SAIMOKU4_BETU_AMT)          --　　　　　　　　項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),SAIMOKU5_CD)                --項目細分５　項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SAIMOKU5_BETSU_AMT_FUGO)    --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SAIMOKU5_BETU_AMT)          --　　　　　　　　項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),SAIMOKU6_CD)                --項目細分６　項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SAIMOKU6__BETSU_AMT_FUGO)   --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SAIMOKU6_BETU_AMT)          --　　　　　　　　項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(4),ERR_CD)                     --エラーコード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(8),NOUNYU_DATE)                --納入日				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SYABAN)                     --車番				  " & vbNewLine _
                                        & " 	 + CONVERT(char(6),YOBI)                       --FILLER				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN1_TAISYO_KISU)           --保管対象期数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN1_KIMATU_ZAIKOSU_FUGO)   --保管対象１　期末在庫数符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN1_KIMATU_ZAIKOSU)        --         　　　 期末在庫数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN1_NYUKO_SURYO_FUGO)      --　　　　　　　　入庫数量符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN1_NYUKO_SURYO)           --　　　　　　　　入庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN1_SYUKO_SURYO_FUGO)      --　　　　　　　　出庫数量符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN1_SYUKO_SURYO)          --　　　　　　　　出庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN2_KIMATU_ZAIKOSU_FUGO)   --保管対象２　期末在庫数符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN2_KIMATU_ZAIKOSU)       --           　　　 期末在庫数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN2_NYUKO_SURYO_FUGO)      --　　　　　　　　入庫数量符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN2_NYUKO_SURYO)          --　　　　　　　　入庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN2_SYUKO_SURYO_FUGO)      --　　　　　　　　出庫数量符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN2_SYUKO_SURYO)          --　　　　　　　　出庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN3_KIMATU_ZAIKOSU_FUGO)   --保管対象３　期末在庫数符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN3_KIMATU_ZAIKOSU)       --         　　　 期末在庫数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN3_NYUKO_SURYO_FUGO)      --　　　　　　　　入庫数量符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN3_NYUKO_SURYO)          --　　　　　　　　入庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN3_SYUKO_SURYO_FUGO)      --　　　　　　　　出庫数量符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN3_SYUKO_SURYO)          --　　　　　　　　出庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),YOBI1)                      --FILLER				  " & vbNewLine _
                                        & " 	 + CONVERT(char(10),ORDER_NO)                  --オーダー№				  " & vbNewLine _
                                        & " 	 + CONVERT(char(8),SYORI_DATE)                 --処理日付				  " & vbNewLine _
                                        & " 	 + CONVERT(char(9),SYORI_TIME)                 --処理時間				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),YOBI2)                      --FILLER				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SITABARAI_KBN)              --下払区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),SITABARAI_CD)               --下払先コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(10),GRADE1)                    --グレード1				  " & vbNewLine _
                                        & " 	 + CONVERT(char(3),YOBI3)                      --FILLER				  " & vbNewLine _
                                        & "        AS  NET_AMT				  " & vbNewLine _
                                        & " 	　　FROM (				  " & vbNewLine



    Private Const SQL_BUTSURYUHI_SQL1 As String = "	　SELECT 				  " & vbNewLine _
                                       & " 	　          ROW_NUMBER() OVER(ORDER BY SKYU_YM) AS seq				  " & vbNewLine _
                                       & " 	　          ,1                                   AS RN 				  " & vbNewLine _
                                       & " 	　　　  ,0                                   AS SAI				  " & vbNewLine _
                                       & " 	　　　  ,0                                   AS SUM_NET_AMT				  " & vbNewLine _
                                       & " 	　　　  ,0                                   AS DECI_UNCHIN				  " & vbNewLine _
                                       & " 	　　　  ,''                                  AS UNSO_NO_L                                   				  " & vbNewLine _
                                       & " 	　　　  ,'VK74'                              AS RECORD_ID               --レコードID				  " & vbNewLine _
                                       & " 	　　　  ,'41V'                               AS DATA_ID                 --データID         データIDの3桁目：V     (日陸様を表すコードです)				  " & vbNewLine _
                                       & " 	　　　  ,'040 '                              AS COMP_CD                 --会社コード 040   (日本合成化学工業を表すコードです)				  " & vbNewLine _
                                       & " 	　　　  ,LEFT(@S_DATE,6)                     AS KEIJYO_YM               --計上年月				  " & vbNewLine _
                                       & " 	　　　  ,''                                  AS UKETUKE_NO              --受付№				  " & vbNewLine _
                                       & " 	　　　  ,SUBSTRING(@S_DATE,3,4)              AS BUTURYU_SERI_NO         --物流整理№				  " & vbNewLine _
                                       & " 	　　　  ,'000'                               AS TEISEI_NO               --訂正No.				  " & vbNewLine _
                                       & " 	　　　  ,'1 '                                AS TEISEI_KBN              --訂正区分				  " & vbNewLine _
                                       & " 	　　　  ,SPACE(2)                            AS KINOU                   --機能				  " & vbNewLine _
                                       & " 	　　　  ,SPACE(2)                            AS KINOU_SAIMOKU           --機能細目				  " & vbNewLine _
                                       & " 	　　　  ,'11'                                AS SAGYO_N_KBN　    --作業内容区分  				  " & vbNewLine _
                                       & " 	　　　  ,CONVERT(VARCHAR,GETDATE(),112)      AS SYORI_YMD         --伝送処理年月日				  " & vbNewLine _
                                       & " 	　　　  ,FORMAT(GETDATE(),'HHmmss')          AS SYORI_JIKAN       --伝送処理時間				  " & vbNewLine _
                                       & " 	　　　  ,SPACE(2)                            AS NYURYOKU_BASYO     --入力場所				  " & vbNewLine _
                                       & " 	　　　  ,SPACE(2)                            AS NYURYOKU_BUMON     --入力部門				  " & vbNewLine _
                                       & " 	　　　  ,SPACE(2)                            AS NYURYOKU_GROUP     --入力グループ				  " & vbNewLine _
                                       & " 	　　　  ,light.SKYU_YMD                      AS SAGYOU_DATE        --作業年月日				  " & vbNewLine _
                                       & " 	　　　  ,''                                  AS HINMEI_RYAKUGO     --品目略号				  " & vbNewLine _
                                       & " 	　　　  ,'722'                               AS UG_CD              --UGコード				  " & vbNewLine _
                                       & " 	　　　  ,''                                  AS NISUGATA           --荷姿				  " & vbNewLine _
                                       & " 	　　　  ,'00000.00'                          AS YOURYO             --容量				  " & vbNewLine _
                                       & " 	　　　  ,'AA'                                AS KEIYAKU_BASYO      --契約場所 'AA'固定				  " & vbNewLine _
                                       & " 	　　　  ,'71'                                AS BIN_KBN            --便区分				  " & vbNewLine _
                                       & " 	　　　  ,'H441B'                             AS BUTSURYU_COMP_CD   --物流会社コード H441B (日陸様を表すコードです)				  " & vbNewLine _
                                       & " 	　　　  ,''                                  AS TSUMEAWASE_NO      --詰合わせNo.				  " & vbNewLine _
                                       & " 	　　　  ,SPACE(7)                            AS JITSU_USER          --実ユーザー				  " & vbNewLine _
                                       & " 	　　　  ,SPACE(7)                            AS KEIYAKUSAKI_CD      --契約先コード				  " & vbNewLine _
                                       & " 	　　　  ,'2'                                 AS KOTEIHI_HEND_KBN    --固定費・変動費区分				  " & vbNewLine _
                                       & " 	　　　  ,'0'                                 AS HANBAI_BUNRUI       --販売分類				  " & vbNewLine _
                                       & " 	　　　  ,'2'                                 AS HACCHI_KBN          --発地情報　発地区分				  " & vbNewLine _
                                       & " 	　　　  ,'A0I4'                              AS HACCHI_CD           --　　　　　　　発地コード				  " & vbNewLine _
                                       & " 	　　　  ,''                                  AS HACCHI_CIKU_CD      --　　　　　　　市区町村コード				  " & vbNewLine _
                                       & " 	　　　  ,'3'　                               AS CHAKUCHI_KBN          --着地情報　着地区分				  " & vbNewLine _
                                       & " 	　　　  ,'Y1111'                             AS CHAKUCHI_CD           --　　　　　　　着地コード  ??????				  " & vbNewLine _
                                       & " 	　　　  ,''                                  AS CHAKUCHI_CHIKU_CD     --　　　　　　　市区町村コード				  " & vbNewLine _
                                       & " 	　　　  ,''                                  AS CHAKUCHI_NOUNYU_BASYO --　　　　　　　納入場所名				  " & vbNewLine _
                                       & " 	　　　  ,'2'                                 AS HASSEI_BASYO_KBN      --発生場所区分				  " & vbNewLine _
                                       & " 	　　　  ,'A0I4'                              AS HASEI_SP_CD           --発生SPコード				  " & vbNewLine _
                                       & " 	　　　  ,'A0'                                AS SIHARAI_BASYO         --支払場所				  " & vbNewLine _
                                       & " 	　　　  ,'2'                                 AS BUTURYU_TANI          --物流単位				  " & vbNewLine _
                                       & " 	　　　  ,'+'                                 AS BUTURYU_FUGO          --物流量符号				  " & vbNewLine _
                                       & " 	　　　  ,'00000000001.0000'                  AS BUTURYU_RYO           --物流量				  " & vbNewLine _
                                       & " 	　　　  ,''                                  AS LOT_NO                --ロット№				  " & vbNewLine _
                                       & " 	　　　  ,CASE WHEN light.TAX_RATE <> 0 THEN '1' 				  " & vbNewLine _
                                       & " 	　　　　　ELSE '2'               END  AS TAX_BN                --税区分				  " & vbNewLine _
                                       & " 	　　　  ,'N'                               AS GN_KBN                --GN区分				  " & vbNewLine _
                                       & " 	　　　  ,'+'            AS NET_AMT_FUGO          --NET金額符号				  " & vbNewLine _
                                       & " 	　　　  ,FORMAT(light.KEISAN_TLGK,'000000000000000')   AS NET_AMT     --C				  " & vbNewLine _
                                       & " 	　				  " & vbNewLine _
                                       & " 	　　　  ,'+'              AS GROSS_AMT_FUGO        --GROSS金額符号				  " & vbNewLine _
                                       & " 	　　　  ,FORMAT(light.KEISAN_TLGK,'000000000000000')  AS GROSS_AMT    --GROSS金額  (ここではNET金額と同じ)				  " & vbNewLine _
                                       & " 	　　　  ,'+'                               AS TAX_FUGO              --税額符号				  " & vbNewLine _
                                       & " 	　　　  ----,FORMAT(ROUND(UNCHIN.DECI_UNCHIN * TAX_RATE,0),'000000000000000')                                AS TAX                   --税額				  " & vbNewLine _
                                       & " 	　　　  ,'+'                               AS ZEIRITU_FUGO          --税率符号				  " & vbNewLine _
                                       & " 	　　　  ,CONVERT(money,light.TAX_RATE)     AS ZEIRITSU              --税率				  " & vbNewLine _
                                       & " 	　　　  ,'+'                               AS YUSO_KYORI_FUGO       --輸送距離符号				  " & vbNewLine _
                                       & " 	　　　  ,FORMAT(0,'00000')                 AS YUSOKYORI             --輸送距離				  " & vbNewLine _
                                       & " 	　　　  ,'  '                 AS SAIMOKU1_CD               --項目細分１　項目コード				  " & vbNewLine _
                                       & " 	　　　  ,'+'                  AS SAIMOKU1_BETSU_AMT_FUGO   --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                       & " 	　　　  ,FORMAT(0,'00000')    AS SAIMOKU1_BETU_AMT          --　　　　　　　　項目別金額				  " & vbNewLine _
                                       & " 	　　　  ,'  '                 AS SAIMOKU2_CD                --項目細分２　項目コード				  " & vbNewLine _
                                       & " 	　　　  ,'+'                  AS SAIMOKU2_BETSU_AMT_FUGO    --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                       & " 	　　　  ,FORMAT(0,'00000')    AS SAIMOKU2_BETU_AMT          --　　　　　　　　項目別金額				  " & vbNewLine _
                                       & " 	　　　  ,'  '                 AS SAIMOKU3_CD                --項目細分３　項目コード				  " & vbNewLine _
                                       & " 	　　　  ,'+'                  AS SAIMOKU3_BETSU_AMT_FUGO    --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                       & " 	　　　  ,FORMAT(0,'00000')    AS SAIMOKU3_BETU_AMT          --　　　　　　　　項目別金額				  " & vbNewLine _
                                       & " 	　　　  ,'  '                 AS SAIMOKU4_CD                --項目細分４　項目コード				  " & vbNewLine _
                                       & " 	　　　  ,'+'                  AS SAIMOKU4_BETSU_AMT_FUGO    --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                       & " 	　　　  ,FORMAT(0,'00000')    AS SAIMOKU4_BETU_AMT          --　　　　　　　　項目別金額				  " & vbNewLine _
                                       & " 	　　　  ,'  '                 AS SAIMOKU5_CD                --項目細分５　項目コード				  " & vbNewLine _
                                       & " 	　　　  ,'+'                  AS SAIMOKU5_BETSU_AMT_FUGO    --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                       & " 	　　　  ,FORMAT(0,'00000')    AS SAIMOKU5_BETU_AMT          --　　　　　　　　項目別金額				  " & vbNewLine _
                                       & " 	　　　  ,'  '                 AS SAIMOKU6_CD                --項目細分６　項目コード				  " & vbNewLine _
                                       & " 	　　　  ,'+'                  AS SAIMOKU6__BETSU_AMT_FUGO   --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                       & " 	　　　  ,FORMAT(0,'00000')    AS SAIMOKU6_BETU_AMT          --　　　　　　　　項目別金額				  " & vbNewLine _
                                       & " 	　　　  ,''                   AS ERR_CD                     --エラーコード				  " & vbNewLine _
                                       & " 	　　　  ,@E_DATE              AS NOUNYU_DATE                --納入日				  " & vbNewLine _
                                       & " 	　　　  ,SPACE(5)             AS SYABAN                     --車番				  " & vbNewLine _
                                       & " 	　　　  ,SPACE(6)             AS YOBI                       --FILLER				  " & vbNewLine _
                                       & " 	　　　  ,' '                  AS HKN1_TAISYO_KISU           --保管対象期数				  " & vbNewLine _
                                       & " 	　　　  ,'+'                  AS HKN1_KIMATU_ZAIKOSU_FUGO   --保管対象１　期末在庫数符号				  " & vbNewLine _
                                       & " 	　　　  ,FORMAT(0,'00000000000.0000')              AS HKN1_KIMATU_ZAIKOSU        --         　　　 期末在庫数				  " & vbNewLine _
                                       & " 	　　　  ,'+'                                       AS HKN1_NYUKO_SURYO_FUGO      --　　　　　　　　入庫数量符号				  " & vbNewLine _
                                       & " 	　　　  ,FORMAT(0,'00000000000.0000')              AS HKN1_NYUKO_SURYO           --　　　　　　　　入庫数量				  " & vbNewLine _
                                       & " 	　　　  ,'+'                                       AS HKN1_SYUKO_SURYO_FUGO      --　　　　　　　　出庫数量符号				  " & vbNewLine _
                                       & " 	　　　  ,FORMAT(0,'00000000000.0000')              AS HKN1_SYUKO_SURYO           --　　　　　　　　出庫数量				  " & vbNewLine _
                                       & " 	　　　  ,'+ '                                      AS HKN2_KIMATU_ZAIKOSU_FUGO   --保管対象２　期末在庫数符号				  " & vbNewLine _
                                       & " 	　　　  ,FORMAT(0,'00000000000.0000')              AS HKN2_KIMATU_ZAIKOSU        --           　　　 期末在庫数				  " & vbNewLine _
                                       & " 	　　　  ,'+'                                       AS HKN2_NYUKO_SURYO_FUGO      --　　　　　　　　入庫数量符号				  " & vbNewLine _
                                       & " 	　　　  ,FORMAT(0,'00000000000.0000')              AS HKN2_NYUKO_SURYO           --　　　　　　　　入庫数量				  " & vbNewLine _
                                       & " 	　　　  ,'+'                                       AS HKN2_SYUKO_SURYO_FUGO      --　　　　　　　　出庫数量符号				  " & vbNewLine _
                                       & " 	　　　  ,FORMAT(0,'00000000000.0000')              AS HKN2_SYUKO_SURYO           --　　　　　　　　出庫数量				  " & vbNewLine _
                                       & " 	　　　  ,'+'                                       AS HKN3_KIMATU_ZAIKOSU_FUGO   --保管対象３　期末在庫数符号				  " & vbNewLine _
                                       & " 	　　　  ,FORMAT(0,'00000000000.0000')              AS HKN3_KIMATU_ZAIKOSU        --         　　　 期末在庫数				  " & vbNewLine _
                                       & " 	　　  ,'+'                                       AS HKN3_NYUKO_SURYO_FUGO      --　　　　　　　　入庫数量符号				  " & vbNewLine _
                                       & " 	　　  ,FORMAT(0,'00000000000.0000')              AS HKN3_NYUKO_SURYO           --　　　　　　　　入庫数量				  " & vbNewLine _
                                       & " 	　　  ,'+'                                       AS HKN3_SYUKO_SURYO_FUGO      --　　　　　　　　出庫数量符号				  " & vbNewLine _
                                       & " 	　　  ,FORMAT(0,'00000000000.0000')              AS HKN3_SYUKO_SURYO           --　　　　　　　　出庫数量				  " & vbNewLine _
                                       & " 	　　  ,SPACE(7)                                  AS YOBI1                      --FILLER				  " & vbNewLine _
                                       & " 	　　  ,''                                        AS ORDER_NO                   --オーダー№				  " & vbNewLine _
                                       & " 	　　  ,SPACE(8)                                  AS SYORI_DATE                 --処理日付				  " & vbNewLine _
                                       & " 	　　  ,SPACE(9)                                  AS SYORI_TIME                 --処理時間				  " & vbNewLine _
                                       & " 	　　  ,SPACE(2)                                  AS YOBI2                      --FILLER				  " & vbNewLine _
                                       & " 	　　  ,SPACE(2)                                  AS SITABARAI_KBN              --下払区分				  " & vbNewLine _
                                       & " 	　　  ,SPACE(7)                                  AS SITABARAI_CD               --下払先コード				  " & vbNewLine _
                                       & " 	　　  ,''                                        AS GRADE1                     --グレード1				  " & vbNewLine _
                                       & " 	　　  ,SPACE(3)                                  AS YOBI3                       --FILLER				  " & vbNewLine _
                                       & " 	  FROM  (				  " & vbNewLine _
                                       & " 	         SELECT				  " & vbNewLine _
                                       & " 	             SUM(ISNULL(KDTL.KEISAN_TLGK,0))   AS KEISAN_TLGK				  " & vbNewLine _
                                       & " 	　　　,KDTL.TAX_CD_JDE          AS TAX_CD_JDE				  " & vbNewLine _
                                       & " 	　        ,ISNULL(TAX.TAX_RATE,0)   AS TAX_RATE 				  " & vbNewLine _
                                       & " 	　　　				  " & vbNewLine _
                                       & " 	　　　,LEFT(KHED.SKYU_DATE,6)   AS  SKYU_YM				  " & vbNewLine _
                                       & " 	　　　,KHED.SKYU_DATE           AS  SKYU_YMD				  " & vbNewLine _
                                       & " 	         FROM LM_TRN..G_KAGAMI_HED KHED				  " & vbNewLine _
                                       & " 	　　　　  LEFT JOIN LM_TRN..G_KAGAMI_DTL KDTL ON				  " & vbNewLine _
                                       & " 	　　　　　　KDTL.SKYU_NO = KHED.SKYU_NO				  " & vbNewLine _
                                       & " 	　　　　　AND KDTL.SYS_DEL_FLG = '0'				  " & vbNewLine _
                                       & " 	　　　　　AND KDTL.GROUP_KB    IN ('03','05','10','12')    --'03':運賃 ,'05':横持料 ,'10':再保管用 , '12':再保管横持料				  " & vbNewLine _
                                       & " 	　　　　　　　　　AND KDTL.MAKE_SYU_KB = '01'    --追加				  " & vbNewLine _
                                       & " 	                   --税率マスタ				  " & vbNewLine _
                                       & " 	　　　　　LEFT JOIN LM_MST..M_TAX TAX				  " & vbNewLine _
                                       & " 	　　　　　　 ON TAX.TAX_CD_JDE = KDTL.TAX_CD_JDE				  " & vbNewLine _
                                       & " 	　　　　　　 AND TAX.SYS_DEL_FLG = '0'				  " & vbNewLine _
                                       & " 					  " & vbNewLine _
                                       & " 	　　　　  WHERE KHED.NRS_BR_CD = '60'				  " & vbNewLine _
                                       & " 	　　　　　AND SEIQTO_CD = '3251699'   --日合請求先CD				  " & vbNewLine _
                                       & " 	　　　　　AND KHED.SKYU_DATE >= @S_DATE				  " & vbNewLine _
                                       & " 	　　　　　AND KHED.SKYU_DATE <= @E_DATE				  " & vbNewLine _
                                       & " 	　　　　　AND KHED.SYS_DEL_FLG = '0'				  " & vbNewLine _
                                       & " 	　　　　　AND KDTL.SKYU_NO = KHED.SKYU_NO				  " & vbNewLine _
                                       & " 	　　　　  GROUP BY KDTL.TAX_CD_JDE				  " & vbNewLine _
                                       & " 	　　　　　　  ,TAX.TAX_RATE				  " & vbNewLine _
                                       & " 	　　　　　　  ,LEFT(KHED.SKYU_DATE,6)				  " & vbNewLine _
                                       & " 	　　　　　　  ,KHED.SKYU_DATE				  " & vbNewLine _
                                       & " 	        UNION ALL				  " & vbNewLine _
                                       & " 	　    SELECT 				  " & vbNewLine _
                                       & " 	             ISNULL(UNCHIN.DECI_UNCHIN,0)   AS KEISAN_TLGK				  " & vbNewLine _
                                       & " 	　　　,''                           AS TAX_CD_JDE				  " & vbNewLine _
                                       & " 	　        ,CONVERT(money,TAX.TAX_RATE)  AS TAX_RATE 				  " & vbNewLine _
                                       & " 	　　　				  " & vbNewLine _
                                       & " 	　　　,LEFT(OUTKAL.OUTKA_PLAN_DATE ,6)   AS  SKYU_YM				  " & vbNewLine _
                                       & " 	　　　,UNSOL.ARR_PLAN_DATE           AS  SKYU_YMD				  " & vbNewLine _
                                       & " 					  " & vbNewLine _
                                       & " 	　　FROM LM_TRN..F_UNSO_L UNSOL				  " & vbNewLine _
                                       & " 	　　LEFT JOIN LM_TRN..F_UNCHIN_TRS UNCHIN ON 				  " & vbNewLine _
                                       & " 	　　　  UNCHIN.SYS_DEL_FLG = '0'				  " & vbNewLine _
                                       & " 	　　  AND UNCHIN.NRS_BR_CD   =  UNSOL.NRS_BR_CD				  " & vbNewLine _
                                       & " 	　　  AND UNCHIN.UNSO_NO_L   =  UNSOL.UNSO_NO_L				  " & vbNewLine _
                                       & " 					  " & vbNewLine _
                                       & " 	　　  --出荷L				  " & vbNewLine _
                                       & " 	　　  LEFT JOIN LM_TRN..C_OUTKA_L OUTKAL ON 				  " & vbNewLine _
                                       & " 	　　　  OUTKAL.SYS_DEL_FLG = '0'				  " & vbNewLine _
                                       & " 	　　  AND OUTKAL.NRS_BR_CD   =  UNSOL.NRS_BR_CD				  " & vbNewLine _
                                       & " 	　　  AND OUTKAL.OUTKA_NO_L   =  UNSOL.INOUTKA_NO_L				  " & vbNewLine _
                                       & " 					  " & vbNewLine _
                                       & " 	　    --EDI出荷				  " & vbNewLine _
                                       & " 	　　  LEFT JOIN LM_TRN..H_OUTKAEDI_DTL_NCGO_NEW EDIWK ON				  " & vbNewLine _
                                       & " 	　　　   EDIWK.SYS_DEL_FLG  = '0'				  " & vbNewLine _
                                       & " 	　　  AND EDIWK.DEL_KB       = '0'				  " & vbNewLine _
                                       & " 	　　  AND EDIWK.NRS_BR_CD    = UNSOL.NRS_BR_CD				  " & vbNewLine _
                                       & " 	　　  AND EDIWK.OUTKA_CTL_NO     = UNSOL.INOUTKA_NO_L  				  " & vbNewLine _
                                       & " 	　　 ----AND EDIWK.OUTKA_CTL_NO_CHU = EDIM.OUTKA_CTL_NO_CHU				  " & vbNewLine _
                                       & " 					  " & vbNewLine _
                                       & " 	　　--EDI輸送				  " & vbNewLine _
                                       & " 	　　LEFT JOIN LM_TRN..H_UNSOEDI_DTL_NCGO EDIUNSO ON				  " & vbNewLine _
                                       & " 	　　　 EDIUNSO.SYS_DEL_FLG  = '0'				  " & vbNewLine _
                                       & " 	　　 AND EDIUNSO.DEL_KB       = '0'				  " & vbNewLine _
                                       & " 	　　 AND EDIUNSO.NRS_BR_CD         = EDIWK.NRS_BR_CD				  " & vbNewLine _
                                       & " 	　　 AND EDIUNSO.OUTKA_DENP_NO     = EDIWK.OUTKA_DENP_NO  				  " & vbNewLine _
                                       & " 	　　 AND EDIUNSO.OUTKA_DENP_DTL_NO = EDIWK.OUTKA_DENP_DTL_NO				  " & vbNewLine _
                                       & " 	     AND EDIUNSO.OUTKA_CTL_NO = UNSOL.INOUTKA_NO_L				  " & vbNewLine _
                                       & " 	　　--区分マスタ 税区分				  " & vbNewLine _
                                       & " 	　　LEFT JOIN LM_MST..Z_KBN KBN1 ON				  " & vbNewLine _
                                       & " 	　　　 KBN1.SYS_DEL_FLG = '0'				  " & vbNewLine _
                                       & " 	　　 AND KBN1.KBN_GROUP_CD = 'Z001'				  " & vbNewLine _
                                       & " 	　　 AND KBN1.KBN_CD       = UNCHIN.TAX_KB				  " & vbNewLine _
                                       & " 					  " & vbNewLine _
                                       & " 	　　--税率マスタ				  " & vbNewLine _
                                       & " 	　　LEFT JOIN (				  " & vbNewLine _
                                       & " 	　　　　   SELECT                                     				  " & vbNewLine _
                                       & " 	　　　　　　TAX.TAX_RATE   AS TAX_RATE             				  " & vbNewLine _
                                       & " 	　　　　　  , TAX.START_DATE AS START_DATE           				  " & vbNewLine _
                                       & " 	　　　　　FROM                                       				  " & vbNewLine _
                                       & " 	　　　　　   LM_MST..M_TAX TAX                     				  " & vbNewLine _
                                       & " 	　　　　　INNER JOIN (                               				  " & vbNewLine _
                                       & " 	　　　　　　SELECT                                 				  " & vbNewLine _
                                       & " 	　　　　　　　KBN1.KBN_GROUP_CD                  				  " & vbNewLine _
                                       & " 	　　　　　　  , KBN1.KBN_CD                        				  " & vbNewLine _
                                       & " 	　　　　　　  , KBN1.KBN_NM3                       				  " & vbNewLine _
                                       & " 	　　　　　　  , TAX2.START_DATE                    				  " & vbNewLine _
                                       & " 	　　　　　　  , TAX2.TAX_CD                        				  " & vbNewLine _
                                       & " 	　　　　　　FROM (                                 				  " & vbNewLine _
                                       & " 	　　　　　　　SELECT                             				  " & vbNewLine _
                                       & " 	　　　　　　　　MAX(START_DATE) AS START_DATE  				  " & vbNewLine _
                                       & " 	　　　　　　　  , TAX3.TAX_CD     AS TAX_CD      				  " & vbNewLine _
                                       & " 	　　　　　　　FROM                               				  " & vbNewLine _
                                       & " 	　　　　　　　   LM_MST..M_TAX TAX3            				  " & vbNewLine _
                                       & " 	　　　　　　　WHERE                              				  " & vbNewLine _
                                       & " 	　　　　　　　　TAX3.START_DATE <= @E_DATE  --@OUTKA_PLAN_DATE  				  " & vbNewLine _
                                       & " 	　　　　　　　GROUP BY                           				  " & vbNewLine _
                                       & " 	　　　　　　　　TAX3.TAX_CD) TAX2              				  " & vbNewLine _
                                       & " 	　　　　　　INNER JOIN                             				  " & vbNewLine _
                                       & " 	　　　　　　   LM_MST..Z_KBN KBN1                				  " & vbNewLine _
                                       & " 	　　　　　　 ON KBN1.KBN_GROUP_CD = 'Z001'         				  " & vbNewLine _
                                       & " 	　　　　　　AND KBN1.KBN_CD = '01'   --NCHIN.TAX_KB              				  " & vbNewLine _
                                       & " 	　　　　　　AND KBN1.KBN_NM3 = TAX2.TAX_CD) TAX1   				  " & vbNewLine _
                                       & " 	　　　　　 ON TAX1.START_DATE = TAX.START_DATE       				  " & vbNewLine _
                                       & " 	　　　　　AND TAX1.KBN_NM3 = TAX.TAX_CD) TAX          				  " & vbNewLine _
                                       & " 	　　　　  on TAX.START_DATE <= UNSOL.OUTKA_PLAN_DATE    				  " & vbNewLine _
                                       & " 	　　WHERE UNSOL.SYS_DEL_FLG = '0'				  " & vbNewLine _
                                       & " 	　　  AND UNSOL. CUST_CD_L  = '32516'				  " & vbNewLine _
                                       & " 	　　  AND UNSOL.MOTO_DATA_KB  ='20'  --元データ区分 出荷				  " & vbNewLine _
                                       & " 	　　  AND UNSOL.OUTKA_PLAN_DATE >= @S_DATE				  " & vbNewLine _
                                       & " 	　　  AND UNSOL.OUTKA_PLAN_DATE <= @E_DATE				  " & vbNewLine _
                                       & " 	　　  AND UNCHIN.SEIQ_FIXED_FLAG = '01'   --確定済 				  " & vbNewLine _
                                       & " 	　　  AND EDIWK.OUTKA_CTL_NO IS  NULL　　　　　　  　  				  " & vbNewLine _
                                       & " 	      ) light				  " & vbNewLine _
                                       & " 	    )  BUTSU 				  " & vbNewLine




    Private Const SQL_BUTSURYUHI_CHK2 As String = " 	SELECT 				  " & vbNewLine _
                                        & " 	   '2'    as SQ_NO				  " & vbNewLine _
                                        & " 	  ,sum(  CASE WHEN RN = 1 THEN  CONVERT(INT,NET_AMT) + CONVERT(INT,SAI) 				  " & vbNewLine _
                                        & " 	                      ELSE  CONVERT(INT,NET_AMT)        END) as NET_AMT     --NET金額				  " & vbNewLine _
                                        & " 	  FROM (				  " & vbNewLine _
                                        & " 					  " & vbNewLine

    Private Const SQL_BUTSURYUHI_DATA2 As String = " 	SELECT 				  " & vbNewLine _
                                        & " 	    -- 運賃--				  " & vbNewLine _
                                        & " 	    CONVERT(char(4),RECORD_ID)               --レコードID				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(3),DATA_ID)                 --データID         データIDの3桁目：V     (日陸様を表すコードです)				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(3),COMP_CD)                 --会社コード 040   (日本合成化学工業を表すコードです)				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(6),KEIJYO_YM)               --計上年月				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(12),UKETUKE_NO)             --受付				  " & vbNewLine _
                                        & " 	 +  CASE WHEN UNSO_NO_L <> '' THEN				  " & vbNewLine _
                                        & " 	             CONVERT(char(12),BUTURYU_SERI_NO + 'U' +  FORMAT(seq,'0000000'))        --物流整理№				  " & vbNewLine _
                                        & " 	     ELSE				  " & vbNewLine _
                                        & " 	           CONVERT(char(12),BUTURYU_SERI_NO + 'UK' +  FORMAT(seq,'000000'))  END    --物流整理№鑑より				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(3),TEISEI_NO)          --訂正No.				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),TEISEI_KBN)         --訂正区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),KINOU)              --機能				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),KINOU_SAIMOKU)      --機能細目				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),SAGYO_N_KBN)               --作業内容区分  				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(8),SYORI_YMD)          --伝送処理年月日				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(9),SYORI_JIKAN)        --伝送処理時間				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),NYURYOKU_BASYO)     --入力場所				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),NYURYOKU_BUMON)     --入力部門				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),NYURYOKU_GROUP)     --入力グループ				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(8),SAGYOU_DATE)        --作業年月日				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(10),HINMEI_RYAKUGO)    --品目略号				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(5),UG_CD)              --UGコード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),NISUGATA)           --荷姿				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(8),YOURYO)             --容量				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),KEIYAKU_BASYO)      --契約場所 'AA'固定				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),BIN_KBN)            --便区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(7),BUTSURYU_COMP_CD)   --物流会社コード H441B (日陸様を表すコードです)				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(10),TSUMEAWASE_NO)     --詰合わせNo.				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(7),JITSU_USER)         --実ユーザー				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(7),KEIYAKUSAKI_CD)     --契約先コード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),KOTEIHI_HEND_KBN)   --固定費・変動費区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),HANBAI_BUNRUI)      --販売分類				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),HACCHI_KBN)         --発地情報 発地区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(7),HACCHI_CD)          --       発地コード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(6),HACCHI_CIKU_CD)     --       市区町村コード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),CHAKUCHI_KBN)           --着地情報 着地区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(7),CHAKUCHI_CD)            --       着地コード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(6),CHAKUCHI_CHIKU_CD)      --       市区町村コード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(20),CHAKUCHI_NOUNYU_BASYO) --       納入場所名				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),HASSEI_BASYO_KBN)       --発生場所区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(7),HASEI_SP_CD)            --発生SPコード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),SIHARAI_BASYO)          --支払場所				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),BUTURYU_TANI)           --物流単位				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),BUTURYU_FUGO)           --物流量符号				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(16),BUTURYU_RYO)           --物流量				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(10),LOT_NO)                --ロット№				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),TAX_BN)                 --税区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),GN_KBN)                 --GN区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),NET_AMT_FUGO)           --NET金額符号				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	 +  CONVERT(char(15),CASE WHEN RN = 1 THEN  FORMAT(CONVERT(INT,NET_AMT) + CONVERT(INT,SAI) ,'000000000000000')				  " & vbNewLine _
                                        & " 	                    ELSE  FORMAT(CONVERT(INT,NET_AMT),'000000000000000')        END)      --NET金額				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),GROSS_AMT_FUGO)        --GROSS金額符号				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(15),CASE WHEN RN = 1 THEN  FORMAT((CONVERT(INT,NET_AMT) + CONVERT(INT,SAI))* (1 + ZEIRITSU),'000000000000000')				  " & vbNewLine _
                                        & " 	                     ELSE  FORMAT(CONVERT(INT,NET_AMT) * (1 + ZEIRITSU),'000000000000000')        END)      --GROSS金額				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),TAX_FUGO)               --税額符号				  " & vbNewLine _
                                        & " 	 --+ CONVERT(char(15),TAX)                  --税額				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(15),CASE WHEN RN = 1 THEN  FORMAT((CONVERT(INT,NET_AMT) + CONVERT(INT,SAI)) *  ZEIRITSU,'000000000000000')				  " & vbNewLine _
                                        & " 	                     ELSE  FORMAT(CONVERT(INT,NET_AMT) * ZEIRITSU,'000000000000000')        END)      --税額				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),ZEIRITU_FUGO)          --税率符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(6),FORMAT(ZEIRITSU * 100,'000.00'))              --税率				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),YUSO_KYORI_FUGO)       --輸送距離符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),YUSOKYORI)             --輸送距離				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),SAIMOKU1_CD)               --項目細分１ 項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SAIMOKU1_BETSU_AMT_FUGO)   --        項目別金額符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SAIMOKU1_BETU_AMT)          --        項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),SAIMOKU2_CD)                --項目細分２ 項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SAIMOKU2_BETSU_AMT_FUGO)    --        項目別金額符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SAIMOKU2_BETU_AMT)          --        項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),SAIMOKU3_CD)                --項目細分３ 項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SAIMOKU3_BETSU_AMT_FUGO)    --        項目別金額符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SAIMOKU3_BETU_AMT)          --        項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),SAIMOKU4_CD)                --項目細分４ 項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SAIMOKU4_BETSU_AMT_FUGO)    --        項目別金額符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SAIMOKU4_BETU_AMT)          --        項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),SAIMOKU5_CD)                --項目細分５ 項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SAIMOKU5_BETSU_AMT_FUGO)    --        項目別金額符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SAIMOKU5_BETU_AMT)          --        項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),SAIMOKU6_CD)                --項目細分６ 項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SAIMOKU6__BETSU_AMT_FUGO)   --        項目別金額符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SAIMOKU6_BETU_AMT)          --        項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(4),ERR_CD)                     --エラーコード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(8),NOUNYU_DATE)                --納入日				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SYABAN)                     --車番				  " & vbNewLine _
                                        & " 	 + CONVERT(char(6),YOBI)                       --FILLER				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN1_TAISYO_KISU)           --保管対象期数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN1_KIMATU_ZAIKOSU_FUGO)   --保管対象１ 期末在庫数符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN1_KIMATU_ZAIKOSU)        --             期末在庫数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN1_NYUKO_SURYO_FUGO)      --        入庫数量符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN1_NYUKO_SURYO)           --        入庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN1_SYUKO_SURYO_FUGO)      --        出庫数量符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN1_SYUKO_SURYO)          --        出庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN2_KIMATU_ZAIKOSU_FUGO)   --保管対象２ 期末在庫数符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN2_KIMATU_ZAIKOSU)       --               期末在庫数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN2_NYUKO_SURYO_FUGO)      --        入庫数量符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN2_NYUKO_SURYO)          --        入庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN2_SYUKO_SURYO_FUGO)      --        出庫数量符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN2_SYUKO_SURYO)          --        出庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN3_KIMATU_ZAIKOSU_FUGO)   --保管対象３ 期末在庫数符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN3_KIMATU_ZAIKOSU)       --             期末在庫数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN3_NYUKO_SURYO_FUGO)      --        入庫数量符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN3_NYUKO_SURYO)          --        入庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN3_SYUKO_SURYO_FUGO)      --        出庫数量符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN3_SYUKO_SURYO)          --        出庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),YOBI1)                      --FILLER				  " & vbNewLine _
                                        & " 	 + CONVERT(char(10),ORDER_NO)                  --オーダー№				  " & vbNewLine _
                                        & " 	 + CONVERT(char(8),SYORI_DATE)                 --処理日付				  " & vbNewLine _
                                        & " 	 + CONVERT(char(9),SYORI_TIME)                 --処理時間				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),YOBI2)                      --FILLER				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SITABARAI_KBN)              --下払区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),SITABARAI_CD)               --下払先コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(10),GRADE1)                    --グレード1				  " & vbNewLine _
                                        & " 	 + CONVERT(char(3),YOBI3)                      --FILLER				  " & vbNewLine _
                                        & "        AS  NET_AMT				  " & vbNewLine _
                                        & " 	  FROM (				  " & vbNewLine




    Private Const SQL_BUTSURYUHI_SQL2 As String = " 	  SELECT 				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	  -- 確認テスト用---				  " & vbNewLine _
                                        & " 	  --EDIWK.DATA_ID_DETAIL,				  " & vbNewLine _
                                        & " 	  --EDIUNSO.OUTKA_DENP_NO,				  " & vbNewLine _
                                        & " 	  EDIWK.OUTKA_DENP_NO ,				  " & vbNewLine _
                                        & " 	  -- 確認テスト用---				  " & vbNewLine _
                                        & " 	   -- 運賃--				  " & vbNewLine _
                                        & " 	          ROW_NUMBER() OVER(ORDER BY LEFT(UNSOL.OUTKA_PLAN_DATE,6)) AS seq, 				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	     row_number() over(PARTITION BY EDIWK.OUTKA_DENP_NO order by EDIWK.OUTKA_DENP_NO )  AS RN 				  " & vbNewLine _
                                        & " 	    ,UNCHIN.DECI_UNCHIN - GSUM.SUM_NET_AMT  AS SAI				  " & vbNewLine _
                                        & " 	    --,GSUM.GROSS_AMT				  " & vbNewLine _
                                        & " 	    ,GSUM.SUM_NET_AMT				  " & vbNewLine _
                                        & " 	    ,UNCHIN.DECI_UNCHIN 				  " & vbNewLine _
                                        & " 	          ,UNSOL.UNSO_NO_L,				  " & vbNewLine _
                                        & " 	    --EDIWK.DATA_ID_DETAIL, 				  " & vbNewLine _
                                        & " 	    --EDIUNSO.OUTKA_DENP_NO,				  " & vbNewLine _
                                        & " 	    --EDIWK.OUTKA_DENP_NO,				  " & vbNewLine _
                                        & " 	    --EDIWK.HOKAN_BASYO,				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	     'VK74'                             AS RECORD_ID               --レコードID				  " & vbNewLine _
                                        & " 	    ,'11V'                              AS DATA_ID                 --データID         データIDの3桁目：V     (日陸様を表すコードです)				  " & vbNewLine _
                                        & " 	    ,'040 '                             AS COMP_CD                 --会社コード 040   (日本合成化学工業を表すコードです)				  " & vbNewLine _
                                        & " 	    ,LEFT(UNSOL.OUTKA_PLAN_DATE,6)      AS KEIJYO_YM               --計上年月				  " & vbNewLine _
                                        & " 	    ,EDIWK.OUTKA_DENP_NO                AS UKETUKE_NO              --受付№				  " & vbNewLine _
                                        & " 	    --,row_number() over(PARTITION BY EDIWK.OUTKA_DENP_NO order by EDIWK.OUTKA_DENP_NO )  AS BUTURYU_SERI_NO  --物流整理№				  " & vbNewLine _
                                        & " 	    ,SUBSTRING(UNSOL.OUTKA_PLAN_DATE,3,4)      AS BUTURYU_SERI_NO         --物流整理№				  " & vbNewLine _
                                        & " 	    ,'000'         AS TEISEI_NO          --訂正No.				  " & vbNewLine _
                                        & " 	    ,'1 '          AS TEISEI_KBN         --訂正区分				  " & vbNewLine _
                                        & " 	    ,SPACE(2)       AS KINOU              --機能				  " & vbNewLine _
                                        & " 	    ,SPACE(2)       AS KINOU_SAIMOKU      --機能細目				  " & vbNewLine _
                                        & " 	    ,'11'                                       AS SAGYO_N_KBN     --作業内容区分  				  " & vbNewLine _
                                        & " 	    ,CONVERT(VARCHAR,GETDATE(),112)             AS SYORI_YMD         --伝送処理年月日				  " & vbNewLine _
                                        & " 	    ,FORMAT(GETDATE(),'HHmmss')                 AS SYORI_JIKAN       --伝送処理時間				  " & vbNewLine _
                                        & " 	    ,SPACE(2)                            AS NYURYOKU_BASYO     --入力場所				  " & vbNewLine _
                                        & " 	    ,SPACE(2)                            AS NYURYOKU_BUMON     --入力部門				  " & vbNewLine _
                                        & " 	    ,SPACE(2)                            AS NYURYOKU_GROUP     --入力グループ				  " & vbNewLine _
                                        & " 	    ,UNSOL.ARR_PLAN_DATE                 AS SAGYOU_DATE        --作業年月日				  " & vbNewLine _
                                        & " 	    ,EDIWK.ITEM_RYAKUGO                  AS HINMEI_RYAKUGO     --品目略号				  " & vbNewLine _
                                        & " 	    ,EDIWK.UG                            AS UG_CD              --UGコード				  " & vbNewLine _
                                        & " 	    ,EDIWK.KOBETSU_NISUGATA_CD           AS NISUGATA           --荷姿				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	    ,CASE WHEN EDIWK.YOURYOU = '' THEN '00000.00'				  " & vbNewLine _
                                        & " 	          ELSE FORMAT(convert(float,EDIWK.YOURYOU),'00000.00') END   AS YOURYO             --容量				  " & vbNewLine _
                                        & " 	    ,'AA'                                AS KEIYAKU_BASYO      --契約場所 'AA'固定				  " & vbNewLine _
                                        & " 	    ,EDIWK.BIN_KBN                       AS BIN_KBN            --便区分				  " & vbNewLine _
                                        & " 	    ,'H441B'                             AS BUTSURYU_COMP_CD   --物流会社コード H441B (日陸様を表すコードです)				  " & vbNewLine _
                                        & " 	    ,''                                  AS TSUMEAWASE_NO      --詰合わせNo.				  " & vbNewLine _
                                        & " 	    ,CASE WHEN EDIWK.DATA_ID_DETAIL = 'F' --転送				  " & vbNewLine _
                                        & " 	     THEN SPACE(7)				  " & vbNewLine _
                                        & " 	    WHEN EDIWK.DATA_ID_DETAIL = 'G' --倉替				  " & vbNewLine _
                                        & " 	      THEN SPACE(7)				  " & vbNewLine _
                                        & " 	    ELSE   ISNULL(EDIUNSO.JYUCHUSAKI_CD,'')   				  " & vbNewLine _
                                        & " 	     END                               AS JITSU_USER          --実ユーザー				  " & vbNewLine _
                                        & " 	    ,CASE WHEN EDIWK.DATA_ID_DETAIL = 'F' --転送				  " & vbNewLine _
                                        & " 	       THEN SPACE(7)				  " & vbNewLine _
                                        & " 	    WHEN EDIWK.DATA_ID_DETAIL = 'G' --倉替				  " & vbNewLine _
                                        & " 	      THEN SPACE(7)				  " & vbNewLine _
                                        & " 	    ELSE   ISNULL(EDIUNSO.SHIHARAININ_CD,'')   				  " & vbNewLine _
                                        & " 	     END                               AS KEIYAKUSAKI_CD      --契約先コード				  " & vbNewLine _
                                        & " 	    ,'2'                               AS KOTEIHI_HEND_KBN    --固定費・変動費区分				  " & vbNewLine _
                                        & " 	    ,EDIWK.YUSYUTSU_KBN                AS HANBAI_BUNRUI       --販売分類				  " & vbNewLine _
                                        & " 	    ,'2'                               AS HACCHI_KBN          --発地情報 発地区分				  " & vbNewLine _
                                        & " 	    ,EDIWK.HOKAN_BASYO               AS HACCHI_CD           --       発地コード				  " & vbNewLine _
                                        & " 	    ,EDIWK.HACCHI_BASYO_CD             AS HACCHI_CIKU_CD      --       市区町村コード				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	    ,CASE WHEN EDIWK.DATA_ID_DETAIL = 'F' --転送				  " & vbNewLine _
                                        & " 	          THEN '2'     --保管場所				  " & vbNewLine _
                                        & " 	    ELSE '3'				  " & vbNewLine _
                                        & " 	           END                           AS CHAKUCHI_KBN          --着地情報 着地区分				  " & vbNewLine _
                                        & " 	    ,CASE WHEN EDIWK.DATA_ID_DETAIL = 'F' --転送				  " & vbNewLine _
                                        & " 	          THEN  EDIWK.HOKAN_BASYO				  " & vbNewLine _
                                        & " 	    ELSE  EDIWK.SYUKKASAKI_CD				  " & vbNewLine _
                                        & " 	     END                               AS CHAKUCHI_CD           --       着地コード  ??????				  " & vbNewLine _
                                        & " 	    ,EDIWK.SYUKKASAKI_BASYO_CD         AS CHAKUCHI_CHIKU_CD     --       市区町村コード				  " & vbNewLine _
                                        & " 	    ,EDIWK.SYUKKASAKI_NM1              AS CHAKUCHI_NOUNYU_BASYO --       納入場所名				  " & vbNewLine _
                                        & " 	    ,'2'                               AS HASSEI_BASYO_KBN      --発生場所区分				  " & vbNewLine _
                                        & " 	    ,EDIWK.HOKAN_BASYO                 AS HASEI_SP_CD           --発生SPコード				  " & vbNewLine _
                                        & " 	    ,SUBSTRING(EDIWK.HOKAN_BASYO,1,2)  AS SIHARAI_BASYO         --支払場所				  " & vbNewLine _
                                        & " 	    ,'2'                               AS BUTURYU_TANI          --物流単位				  " & vbNewLine _
                                        & " 	    ,'+'                               AS BUTURYU_FUGO          --物流量符号				  " & vbNewLine _
                                        & " 	    ----,FORMAT(UNSOM.UNSO_TTL_QT,'00000000000.0000')          AS BUTURYU_RYO           --物流量				  " & vbNewLine _
                                        & " 	    ,FORMAT(EDIWK.SUURYO,'00000000000.0000')          AS BUTURYU_RYO           --物流量				  " & vbNewLine _
                                        & " 	    ,EDIWK.SEIZO_LOT                    LOT_NO                  --ロット№				  " & vbNewLine _
                                        & " 	    ,CASE WHEN TAX.TAX_RATE <> 0 THEN '1' 				  " & vbNewLine _
                                        & " 	    ELSE '2'               END   AS TAX_BN                --税区分				  " & vbNewLine _
                                        & " 	    ,'N'                               AS GN_KBN                --GN区分				  " & vbNewLine _
                                        & " 	    ,'+'            AS NET_AMT_FUGO          --NET金額符号				  " & vbNewLine _
                                        & " 	    ,FORMAT(ROUND(CASE WHEN UNSOL.UNSO_WT = 0 THEN 0 ELSE (UNCHIN.DECI_UNCHIN /  UNSOL.UNSO_PKG_NB) *  EDIWK.KOSU END,0),'000000000000000')   AS NET_AMT     --C				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	    ,'+'              AS GROSS_AMT_FUGO        --GROSS金額符号				  " & vbNewLine _
                                        & " 	    --,FORMAT(UNCHIN.DECI_UNCHIN,'000000000000000')              AS GROSS_AMT             --GROSS金額				  " & vbNewLine _
                                        & " 	    ,FORMAT(ROUND(CASE WHEN UNSOL.UNSO_WT = 0 THEN 0  ELSE (UNCHIN.DECI_UNCHIN /  UNSOL.UNSO_PKG_NB) *  EDIWK.KOSU END,0),'000000000000000')  AS GROSS_AMT    --GROSS金額  (ここではNET金額と同じ)				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	    ,'+'                               AS TAX_FUGO              --税額符号				  " & vbNewLine _
                                        & " 	    ----,FORMAT(ROUND(UNCHIN.DECI_UNCHIN * TAX_RATE,0),'000000000000000')                                AS TAX                   --税額				  " & vbNewLine _
                                        & " 	    ,'+'                               AS ZEIRITU_FUGO          --税率符号				  " & vbNewLine _
                                        & " 	    ,CONVERT(money,TAX.TAX_RATE)     AS ZEIRITSU              --税率				  " & vbNewLine _
                                        & " 	    ,'+'                               AS YUSO_KYORI_FUGO       --輸送距離符号				  " & vbNewLine _
                                        & " 	    ,FORMAT(UNCHIN.DECI_KYORI,'00000')                 AS YUSOKYORI             --輸送距離				  " & vbNewLine _
                                        & " 	    ,'  '                 AS SAIMOKU1_CD               --項目細分１ 項目コード				  " & vbNewLine _
                                        & " 	    ,'+'                  AS SAIMOKU1_BETSU_AMT_FUGO   --        項目別金額符号				  " & vbNewLine _
                                        & " 	    ,FORMAT(0,'00000')    AS SAIMOKU1_BETU_AMT          --        項目別金額				  " & vbNewLine _
                                        & " 	    ,'  '                 AS SAIMOKU2_CD                --項目細分２ 項目コード				  " & vbNewLine _
                                        & " 	    ,'+'                  AS SAIMOKU2_BETSU_AMT_FUGO    --        項目別金額符号				  " & vbNewLine _
                                        & " 	    ,FORMAT(0,'00000')    AS SAIMOKU2_BETU_AMT          --        項目別金額				  " & vbNewLine _
                                        & " 	    ,'  '                 AS SAIMOKU3_CD                --項目細分３ 項目コード				  " & vbNewLine _
                                        & " 	    ,'+'                  AS SAIMOKU3_BETSU_AMT_FUGO    --        項目別金額符号				  " & vbNewLine _
                                        & " 	    ,FORMAT(0,'00000')    AS SAIMOKU3_BETU_AMT          --        項目別金額				  " & vbNewLine _
                                        & " 	    ,'  '                 AS SAIMOKU4_CD                --項目細分４ 項目コード				  " & vbNewLine _
                                        & " 	    ,'+'                  AS SAIMOKU4_BETSU_AMT_FUGO    --        項目別金額符号				  " & vbNewLine _
                                        & " 	    ,FORMAT(0,'00000')    AS SAIMOKU4_BETU_AMT          --        項目別金額				  " & vbNewLine _
                                        & " 	    ,'  '                 AS SAIMOKU5_CD                --項目細分５ 項目コード				  " & vbNewLine _
                                        & " 	    ,'+'                  AS SAIMOKU5_BETSU_AMT_FUGO    --        項目別金額符号				  " & vbNewLine _
                                        & " 	    ,FORMAT(0,'00000')    AS SAIMOKU5_BETU_AMT          --        項目別金額				  " & vbNewLine _
                                        & " 	    ,'  '                 AS SAIMOKU6_CD                --項目細分６ 項目コード				  " & vbNewLine _
                                        & " 	    ,'+'                  AS SAIMOKU6__BETSU_AMT_FUGO   --        項目別金額符号				  " & vbNewLine _
                                        & " 	    ,FORMAT(0,'00000')    AS SAIMOKU6_BETU_AMT          --        項目別金額				  " & vbNewLine _
                                        & " 	    ,''                   AS ERR_CD                     --エラーコード				  " & vbNewLine _
                                        & " 	    ,EDIWK.NOUKI_DATE     AS NOUNYU_DATE                --納入日				  " & vbNewLine _
                                        & " 	    ,SPACE(5)             AS SYABAN                     --車番				  " & vbNewLine _
                                        & " 	    ,SPACE(6)             AS YOBI                       --FILLER				  " & vbNewLine _
                                        & " 	    ,' '                  AS HKN1_TAISYO_KISU           --保管対象期数				  " & vbNewLine _
                                        & " 	    ,'+'                  AS HKN1_KIMATU_ZAIKOSU_FUGO   --保管対象１ 期末在庫数符号				  " & vbNewLine _
                                        & " 	    ,FORMAT(0,'00000000000.0000')              AS HKN1_KIMATU_ZAIKOSU        --             期末在庫数				  " & vbNewLine _
                                        & " 	    ,'+'                                       AS HKN1_NYUKO_SURYO_FUGO      --        入庫数量符号				  " & vbNewLine _
                                        & " 	    ,FORMAT(0,'00000000000.0000')              AS HKN1_NYUKO_SURYO           --        入庫数量				  " & vbNewLine _
                                        & " 	    ,'+'                                       AS HKN1_SYUKO_SURYO_FUGO      --        出庫数量符号				  " & vbNewLine _
                                        & " 	    ,FORMAT(0,'00000000000.0000')              AS HKN1_SYUKO_SURYO           --        出庫数量				  " & vbNewLine _
                                        & " 	    ,'+ '                                      AS HKN2_KIMATU_ZAIKOSU_FUGO   --保管対象２ 期末在庫数符号				  " & vbNewLine _
                                        & " 	    ,FORMAT(0,'00000000000.0000')              AS HKN2_KIMATU_ZAIKOSU        --               期末在庫数				  " & vbNewLine _
                                        & " 	    ,'+'                                       AS HKN2_NYUKO_SURYO_FUGO      --        入庫数量符号				  " & vbNewLine _
                                        & " 	    ,FORMAT(0,'00000000000.0000')              AS HKN2_NYUKO_SURYO           --        入庫数量				  " & vbNewLine _
                                        & " 	    ,'+'                                       AS HKN2_SYUKO_SURYO_FUGO      --        出庫数量符号				  " & vbNewLine _
                                        & " 	    ,FORMAT(0,'00000000000.0000')              AS HKN2_SYUKO_SURYO           --        出庫数量				  " & vbNewLine _
                                        & " 	    ,'+'                                       AS HKN3_KIMATU_ZAIKOSU_FUGO   --保管対象３ 期末在庫数符号				  " & vbNewLine _
                                        & " 	    ,FORMAT(0,'00000000000.0000')              AS HKN3_KIMATU_ZAIKOSU        --             期末在庫数				  " & vbNewLine _
                                        & " 	    ,'+'                                       AS HKN3_NYUKO_SURYO_FUGO      --        入庫数量符号				  " & vbNewLine _
                                        & " 	    ,FORMAT(0,'00000000000.0000')              AS HKN3_NYUKO_SURYO           --        入庫数量				  " & vbNewLine _
                                        & " 	    ,'+'                                       AS HKN3_SYUKO_SURYO_FUGO      --        出庫数量符号				  " & vbNewLine _
                                        & " 	    ,FORMAT(0,'00000000000.0000')              AS HKN3_SYUKO_SURYO           --        出庫数量				  " & vbNewLine _
                                        & " 	    ,SPACE(7)                                  AS YOBI1                      --FILLER				  " & vbNewLine _
                                        & " 	    ,CASE WHEN EDIWK.DATA_ID_DETAIL = 'A' THEN  EDIWK.JYUCHU_DENP_NO				  " & vbNewLine _
                                        & " 	          ELSE EDIWK.HACCHU_DENP_NO     END    AS ORDER_NO                   --オーダー№				  " & vbNewLine _
                                        & " 	    ,SPACE(8)                                  AS SYORI_DATE                 --処理日付				  " & vbNewLine _
                                        & " 	    ,SPACE(9)                                  AS SYORI_TIME                 --処理時間				  " & vbNewLine _
                                        & " 	    ,SPACE(2)                                  AS YOBI2                      --FILLER				  " & vbNewLine _
                                        & " 	    ,SPACE(2)                                  AS SITABARAI_KBN              --下払区分				  " & vbNewLine _
                                        & " 	    ,SPACE(7)                                  AS SITABARAI_CD               --下払先コード				  " & vbNewLine _
                                        & " 	    ,EDIWK.GRADE1                              AS GRADE1                     --グレード1				  " & vbNewLine _
                                        & " 	    ,SPACE(3)                                  AS YOBI3                       --FILLER				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	  FROM LM_TRN..F_UNSO_L UNSOL				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	  LEFT JOIN LM_TRN..F_UNCHIN_TRS UNCHIN ON 				  " & vbNewLine _
                                        & " 	     UNCHIN.SYS_DEL_FLG = '0'				  " & vbNewLine _
                                        & " 	    AND UNCHIN.NRS_BR_CD   =  UNSOL.NRS_BR_CD				  " & vbNewLine _
                                        & " 	    AND UNCHIN.UNSO_NO_L   =  UNSOL.UNSO_NO_L				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	    --出荷L				  " & vbNewLine _
                                        & " 	    LEFT JOIN LM_TRN..C_OUTKA_L OUTKAL ON 				  " & vbNewLine _
                                        & " 	     OUTKAL.SYS_DEL_FLG = '0'				  " & vbNewLine _
                                        & " 	    AND OUTKAL.NRS_BR_CD   =  UNSOL.NRS_BR_CD				  " & vbNewLine _
                                        & " 	    AND OUTKAL.OUTKA_NO_L   =  UNSOL.INOUTKA_NO_L				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	     --EDI出荷				  " & vbNewLine _
                                        & " 	    LEFT JOIN LM_TRN..H_OUTKAEDI_DTL_NCGO_NEW EDIWK ON				  " & vbNewLine _
                                        & " 	      EDIWK.SYS_DEL_FLG  = '0'				  " & vbNewLine _
                                        & " 	    AND EDIWK.DEL_KB       = '0'				  " & vbNewLine _
                                        & " 	    AND EDIWK.NRS_BR_CD    = UNSOL.NRS_BR_CD				  " & vbNewLine _
                                        & " 	    AND EDIWK.OUTKA_CTL_NO     = UNSOL.INOUTKA_NO_L  				  " & vbNewLine _
                                        & " 	   ----AND EDIWK.OUTKA_CTL_NO_CHU = EDIM.OUTKA_CTL_NO_CHU				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	  --EDI輸送				  " & vbNewLine _
                                        & " 	  LEFT JOIN LM_TRN..H_UNSOEDI_DTL_NCGO EDIUNSO ON				  " & vbNewLine _
                                        & " 	    EDIUNSO.SYS_DEL_FLG  = '0'				  " & vbNewLine _
                                        & " 	   AND EDIUNSO.DEL_KB       = '0'				  " & vbNewLine _
                                        & " 	   AND EDIUNSO.NRS_BR_CD         = EDIWK.NRS_BR_CD				  " & vbNewLine _
                                        & " 	   AND EDIUNSO.OUTKA_DENP_NO     = EDIWK.OUTKA_DENP_NO  				  " & vbNewLine _
                                        & " 	   AND EDIUNSO.OUTKA_DENP_DTL_NO = EDIWK.OUTKA_DENP_DTL_NO				  " & vbNewLine _
                                        & " 	--				  " & vbNewLine _
                                        & " 	         AND EDIUNSO.OUTKA_CTL_NO = UNSOL.INOUTKA_NO_L				  " & vbNewLine _
                                        & " 	--				  " & vbNewLine _
                                        & " 	  --区分マスタ 税区分				  " & vbNewLine _
                                        & " 	  LEFT JOIN LM_MST..Z_KBN KBN1 ON				  " & vbNewLine _
                                        & " 	    KBN1.SYS_DEL_FLG = '0'				  " & vbNewLine _
                                        & " 	   AND KBN1.KBN_GROUP_CD = 'Z001'				  " & vbNewLine _
                                        & " 	   AND KBN1.KBN_CD       = UNCHIN.TAX_KB				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	  --税率マスタ				  " & vbNewLine _
                                        & " 	  LEFT JOIN (				  " & vbNewLine _
                                        & " 	       SELECT                                     				  " & vbNewLine _
                                        & " 	      TAX.TAX_RATE   AS TAX_RATE             				  " & vbNewLine _
                                        & " 	       , TAX.START_DATE AS START_DATE           				  " & vbNewLine _
                                        & " 	     FROM                                       				  " & vbNewLine _
                                        & " 	        LM_MST..M_TAX TAX                     				  " & vbNewLine _
                                        & " 	     INNER JOIN (                               				  " & vbNewLine _
                                        & " 	      SELECT                                 				  " & vbNewLine _
                                        & " 	       KBN1.KBN_GROUP_CD                  				  " & vbNewLine _
                                        & " 	        , KBN1.KBN_CD                        				  " & vbNewLine _
                                        & " 	        , KBN1.KBN_NM3                       				  " & vbNewLine _
                                        & " 	        , TAX2.START_DATE                    				  " & vbNewLine _
                                        & " 	        , TAX2.TAX_CD                        				  " & vbNewLine _
                                        & " 	      FROM (                                 				  " & vbNewLine _
                                        & " 	       SELECT                             				  " & vbNewLine _
                                        & " 	        MAX(START_DATE) AS START_DATE  				  " & vbNewLine _
                                        & " 	         , TAX3.TAX_CD     AS TAX_CD      				  " & vbNewLine _
                                        & " 	       FROM                               				  " & vbNewLine _
                                        & " 	          LM_MST..M_TAX TAX3            				  " & vbNewLine _
                                        & " 	       WHERE                              				  " & vbNewLine _
                                        & " 	        TAX3.START_DATE <= @E_DATE  --@OUTKA_PLAN_DATE  				  " & vbNewLine _
                                        & " 	       GROUP BY                           				  " & vbNewLine _
                                        & " 	        TAX3.TAX_CD) TAX2              				  " & vbNewLine _
                                        & " 	      INNER JOIN                             				  " & vbNewLine _
                                        & " 	         LM_MST..Z_KBN KBN1                				  " & vbNewLine _
                                        & " 	       ON KBN1.KBN_GROUP_CD = 'Z001'         				  " & vbNewLine _
                                        & " 	      AND KBN1.KBN_CD = '01'   --NCHIN.TAX_KB              				  " & vbNewLine _
                                        & " 	      AND KBN1.KBN_NM3 = TAX2.TAX_CD) TAX1   				  " & vbNewLine _
                                        & " 	      ON TAX1.START_DATE = TAX.START_DATE       				  " & vbNewLine _
                                        & " 	     AND TAX1.KBN_NM3 = TAX.TAX_CD) TAX          				  " & vbNewLine _
                                        & " 	      on TAX.START_DATE <= UNSOL.OUTKA_PLAN_DATE    				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	  -----------------------------				  " & vbNewLine _
                                        & " 	  LEFT JOIN (				  " & vbNewLine _
                                        & " 	     SELECT 				  " & vbNewLine _
                                        & " 	       EDIWK.OUTKA_DENP_NO                AS UKETUKE_NO              --受付№				  " & vbNewLine _
                                        & " 	       --***				  " & vbNewLine _
                                        & " 	      ,SUM(ROUND((UNCHIN.DECI_UNCHIN /  UNSOL.UNSO_PKG_NB) *  EDIWK.KOSU,0))   AS SUM_NET_AMT     --NET金額				  " & vbNewLine _
                                        & " 	      ,MAX(UNCHIN.DECI_UNCHIN)                                               AS GROSS_AMT             --GROSS金額				  " & vbNewLine _
                                        & " 	      FROM LM_TRN..F_UNSO_L UNSOL				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	     LEFT JOIN LM_TRN..F_UNCHIN_TRS UNCHIN ON 				  " & vbNewLine _
                                        & " 	       UNCHIN.SYS_DEL_FLG = '0'				  " & vbNewLine _
                                        & " 	      AND UNCHIN.NRS_BR_CD   =  UNSOL.NRS_BR_CD				  " & vbNewLine _
                                        & " 	      AND UNCHIN.UNSO_NO_L   =  UNSOL.UNSO_NO_L				  " & vbNewLine _
                                        & " 	 				  " & vbNewLine _
                                        & " 	       --出荷L				  " & vbNewLine _
                                        & " 	      LEFT JOIN LM_TRN..C_OUTKA_L OUTKAL ON 				  " & vbNewLine _
                                        & " 	        OUTKAL.SYS_DEL_FLG = '0'				  " & vbNewLine _
                                        & " 	       AND OUTKAL.NRS_BR_CD   =  UNSOL.NRS_BR_CD				  " & vbNewLine _
                                        & " 	       AND OUTKAL.OUTKA_NO_L   =  UNSOL.INOUTKA_NO_L				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	         LEFT JOIN LM_TRN..H_OUTKAEDI_DTL_NCGO_NEW EDIWK ON				  " & vbNewLine _
                                        & " 	       EDIWK.SYS_DEL_FLG  = '0'				  " & vbNewLine _
                                        & " 	      AND EDIWK.DEL_KB       = '0'				  " & vbNewLine _
                                        & " 	      AND EDIWK.NRS_BR_CD    = UNSOL.NRS_BR_CD				  " & vbNewLine _
                                        & " 	      AND EDIWK.OUTKA_CTL_NO     = UNSOL.INOUTKA_NO_L 				  " & vbNewLine _
                                        & " 	      ----AND EDIWK.OUTKA_CTL_NO_CHU = EDIM.OUTKA_CTL_NO_CHU				  " & vbNewLine _
                                        & " 	     WHERE UNSOL.SYS_DEL_FLG = '0'				  " & vbNewLine _
                                        & " 	        --AND UNSOL. CUST_CD_L  = '32516'				  " & vbNewLine _
                                        & " 	        --AND UNSOL.MOTO_DATA_KB  ='20'  --元データ区分 出荷				  " & vbNewLine _
                                        & " 	        AND UNSOL.OUTKA_PLAN_DATE >= @S_DATE				  " & vbNewLine _
                                        & " 	        AND UNSOL.OUTKA_PLAN_DATE <= @E_DATE				  " & vbNewLine _
                                        & " 	         AND UNCHIN.SEIQ_FIXED_FLAG = '01'   --確定済 				  " & vbNewLine _
                                        & " 	      --AND UNSOL.UNSO_NO_L IN('N23917755', 'N23917756')				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	     GROUP BY 				  " & vbNewLine _
                                        & " 	      EDIWK.OUTKA_DENP_NO				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	     ) GSUM   ON GSUM.UKETUKE_NO = EDIWK.OUTKA_DENP_NO				  " & vbNewLine _
                                        & " 	  WHERE UNSOL.SYS_DEL_FLG = '0'				  " & vbNewLine _
                                        & " 	    --AND UNSOL. CUST_CD_L  = '32516'				  " & vbNewLine _
                                        & " 	    --AND UNSOL.MOTO_DATA_KB  ='20'  --元データ区分 出荷				  " & vbNewLine _
                                        & " 	    AND UNSOL.OUTKA_PLAN_DATE >= @S_DATE				  " & vbNewLine _
                                        & " 	    AND UNSOL.OUTKA_PLAN_DATE <= @E_DATE				  " & vbNewLine _
                                        & " 	    AND UNCHIN.SEIQ_FIXED_FLAG = '01'   --確定済 				  " & vbNewLine _
                                        & " 	    AND EDIWK.OUTKA_CTL_NO IS NOT NULL				  " & vbNewLine _
                                        & " 	)  BUTSU  				  " & vbNewLine


    Private Const SQL_BUTSURYUHI_CHK3 As String = " 	SELECT 				  " & vbNewLine _
                                        & " 	   '3'    as SQ_NO				  " & vbNewLine _
                                        & " 	 ,isnull(sum(  CASE WHEN RN = 1 THEN  CONVERT(INT,NET_AMT) + CONVERT(INT,SAI) 				  " & vbNewLine _
                                        & " 	                    ELSE  CONVERT(INT,NET_AMT)       END),0)  as  NET_AMT      --NET金額				  " & vbNewLine _
                                        & " 					  " & vbNewLine _
                                        & " 	  FROM (				  " & vbNewLine

    Private Const SQL_BUTSURYUHI_DATA3 As String = " 	SELECT 				  " & vbNewLine _
                                        & " 	    -- 運賃--				  " & vbNewLine _
                                        & " 	    CONVERT(char(4),RECORD_ID)               --レコードID				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(3),DATA_ID)                 --データID         データIDの3桁目：V     (日陸様を表すコードです)				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(3),COMP_CD)                 --会社コード 040   (日本合成化学工業を表すコードです)				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(6),KEIJYO_YM)               --計上年月				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(12),UKETUKE_NO)             --受付				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(12),BUTURYU_SERI_NO + 'KU' +  FORMAT(seq,'000000'))      --物流整理№鑑より				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(3),TEISEI_NO)          --訂正No.				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),TEISEI_KBN)         --訂正区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),KINOU)              --機能				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),KINOU_SAIMOKU)      --機能細目				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),SAGYO_N_KBN)         --作業内容区分  	       			  " & vbNewLine _
                                        & " 	 +  CONVERT(char(8),SYORI_YMD)          --伝送処理年月日				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(9),SYORI_JIKAN)        --伝送処理時間				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),NYURYOKU_BASYO)     --入力場所				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),NYURYOKU_BUMON)     --入力部門				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),NYURYOKU_GROUP)     --入力グループ				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(8),SAGYOU_DATE)        --作業年月日				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(10),HINMEI_RYAKUGO)    --品目略号				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(5),UG_CD)              --UGコード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),NISUGATA)           --荷姿				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(8),YOURYO)             --容量				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),KEIYAKU_BASYO)      --契約場所 'AA'固定				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),BIN_KBN)            --便区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(7),BUTSURYU_COMP_CD)   --物流会社コード H441B (日陸様を表すコードです)				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(10),TSUMEAWASE_NO)     --詰合わせNo.				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(7),JITSU_USER)         --実ユーザー				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(7),KEIYAKUSAKI_CD)     --契約先コード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),KOTEIHI_HEND_KBN)   --固定費・変動費区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),HANBAI_BUNRUI)      --販売分類				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),HACCHI_KBN)         --発地情報　発地区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(7),HACCHI_CD)          --　　　　　　　発地コード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(6),HACCHI_CIKU_CD)     --　　　　　　　市区町村コード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),CHAKUCHI_KBN)           --着地情報　着地区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(7),CHAKUCHI_CD)            --　　　　　　　着地コード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(6),CHAKUCHI_CHIKU_CD)      --　　　　　　　市区町村コード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(20),CHAKUCHI_NOUNYU_BASYO) --　　　　　　　納入場所名				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),HASSEI_BASYO_KBN)       --発生場所区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(7),HASEI_SP_CD)            --発生SPコード				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(2),SIHARAI_BASYO)          --支払場所				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),BUTURYU_TANI)           --物流単位				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),BUTURYU_FUGO)           --物流量符号				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(16),BUTURYU_RYO)           --物流量				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(10),LOT_NO)                --ロット№				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),TAX_BN)                 --税区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),GN_KBN)                 --GN区分				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),NET_AMT_FUGO)           --NET金額符号				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(15),CASE WHEN RN = 1 THEN  FORMAT(CONVERT(INT,NET_AMT) + CONVERT(INT,SAI) ,'000000000000000')				  " & vbNewLine _
                                        & " 	                    ELSE  FORMAT(CONVERT(INT,NET_AMT),'000000000000000')        END)      --NET金額				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),GROSS_AMT_FUGO)        --GROSS金額符号				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(15),CASE WHEN RN = 1 THEN  FORMAT((CONVERT(INT,NET_AMT) + CONVERT(INT,SAI))* (1 + ZEIRITSU),'000000000000000')				  " & vbNewLine _
                                        & " 	                     ELSE  FORMAT(CONVERT(INT,NET_AMT) * (1 + ZEIRITSU),'000000000000000')        END)      --GROSS金額				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),TAX_FUGO)               --税額符号				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(15),CASE WHEN RN = 1 THEN  FORMAT((CONVERT(INT,NET_AMT) + CONVERT(INT,SAI)) *  ZEIRITSU,'000000000000000')				  " & vbNewLine _
                                        & " 	                     ELSE  FORMAT(CONVERT(INT,NET_AMT) * ZEIRITSU,'000000000000000')        END)      --税額				  " & vbNewLine _
                                        & " 	 +  CONVERT(char(1),ZEIRITU_FUGO)          --税率符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(6),FORMAT(ZEIRITSU * 100,'000.00'))              --税率				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),YUSO_KYORI_FUGO)       --輸送距離符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),YUSOKYORI)             --輸送距離				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),SAIMOKU1_CD)               --項目細分１　項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SAIMOKU1_BETSU_AMT_FUGO)   --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SAIMOKU1_BETU_AMT)          --　　　　　　　　項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),SAIMOKU2_CD)                --項目細分２　項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SAIMOKU2_BETSU_AMT_FUGO)    --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SAIMOKU2_BETU_AMT)          --　　　　　　　　項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),SAIMOKU3_CD)                --項目細分３　項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SAIMOKU3_BETSU_AMT_FUGO)    --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SAIMOKU3_BETU_AMT)          --　　　　　　　　項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),SAIMOKU4_CD)                --項目細分４　項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SAIMOKU4_BETSU_AMT_FUGO)    --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SAIMOKU4_BETU_AMT)          --　　　　　　　　項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),SAIMOKU5_CD)                --項目細分５　項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SAIMOKU5_BETSU_AMT_FUGO)    --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SAIMOKU5_BETU_AMT)          --　　　　　　　　項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),SAIMOKU6_CD)                --項目細分６　項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SAIMOKU6__BETSU_AMT_FUGO)   --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SAIMOKU6_BETU_AMT)          --　　　　　　　　項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(4),ERR_CD)                     --エラーコード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(8),NOUNYU_DATE)                --納入日				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),SYABAN)                     --車番				  " & vbNewLine _
                                        & " 	 + CONVERT(char(6),YOBI)                       --FILLER				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN1_TAISYO_KISU)           --保管対象期数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN1_KIMATU_ZAIKOSU_FUGO)   --保管対象１　期末在庫数符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN1_KIMATU_ZAIKOSU)        --         　　　 期末在庫数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN1_NYUKO_SURYO_FUGO)      --　　　　　　　　入庫数量符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN1_NYUKO_SURYO)           --　　　　　　　　入庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN1_SYUKO_SURYO_FUGO)      --　　　　　　　　出庫数量符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN1_SYUKO_SURYO)          --　　　　　　　　出庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN2_KIMATU_ZAIKOSU_FUGO)   --保管対象２　期末在庫数符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN2_KIMATU_ZAIKOSU)       --           　　　 期末在庫数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN2_NYUKO_SURYO_FUGO)      --　　　　　　　　入庫数量符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN2_NYUKO_SURYO)          --　　　　　　　　入庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN2_SYUKO_SURYO_FUGO)      --　　　　　　　　出庫数量符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN2_SYUKO_SURYO)          --　　　　　　　　出庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN3_KIMATU_ZAIKOSU_FUGO)   --保管対象３　期末在庫数符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN3_KIMATU_ZAIKOSU)       --         　　　 期末在庫数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN3_NYUKO_SURYO_FUGO)      --　　　　　　　　入庫数量符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN3_NYUKO_SURYO)          --　　　　　　　　入庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),HKN3_SYUKO_SURYO_FUGO)      --　　　　　　　　出庫数量符号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(16),HKN3_SYUKO_SURYO)          --　　　　　　　　出庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),YOBI1)                      --FILLER				  " & vbNewLine _
                                        & " 	 + CONVERT(char(10),ORDER_NO)                  --オーダー№				  " & vbNewLine _
                                        & " 	 + CONVERT(char(8),SYORI_DATE)                 --処理日付				  " & vbNewLine _
                                        & " 	 + CONVERT(char(9),SYORI_TIME)                 --処理時間				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),YOBI2)                      --FILLER				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),SITABARAI_KBN)              --下払区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),SITABARAI_CD)               --下払先コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(10),GRADE1)                    --グレード1				  " & vbNewLine _
                                        & " 	 + CONVERT(char(3),YOBI3)                      --FILLER				  " & vbNewLine _
                                        & "        AS  NET_AMT				  " & vbNewLine _
                                        & " 	  FROM (				  " & vbNewLine


    Private Const SQL_BUTSURYUHI_SQL3 As String = " 	SELECT 				  " & vbNewLine _
                                         & " 	           ROW_NUMBER() OVER(ORDER BY SKYU_YM) AS seq				  " & vbNewLine _
                                         & " 	          ,1                                   AS RN 				  " & vbNewLine _
                                         & " 	      ,0                                   AS SAI				  " & vbNewLine _
                                         & " 	      ,0                                   AS SUM_NET_AMT				  " & vbNewLine _
                                         & " 	      ,0                                   AS DECI_UNCHIN				  " & vbNewLine _
                                         & " 	      ,''                                  AS UNSO_NO_L				  " & vbNewLine _
                                         & " 	                                       				  " & vbNewLine _
                                         & " 	      ,'VK74'                              AS RECORD_ID               --レコードID				  " & vbNewLine _
                                         & " 	      ,'41V'                               AS DATA_ID                 --データID         データIDの3桁目：V     (日陸様を表すコードです)				  " & vbNewLine _
                                         & " 	      ,'040 '                              AS COMP_CD                 --会社コード 040   (日本合成化学工業を表すコードです)				  " & vbNewLine _
                                         & " 	      ,LEFT(@S_DATE,6)                     AS KEIJYO_YM               --計上年月				  " & vbNewLine _
                                         & " 	      ,''                                  AS UKETUKE_NO              --受付№				  " & vbNewLine _
                                         & " 	      ,SUBSTRING(@S_DATE,3,4)              AS BUTURYU_SERI_NO         --物流整理№				  " & vbNewLine _
                                         & " 	      ,'000'                               AS TEISEI_NO               --訂正No.				  " & vbNewLine _
                                         & " 	      ,'1 '                                AS TEISEI_KBN              --訂正区分				  " & vbNewLine _
                                         & " 	      ,SPACE(2)                            AS KINOU                   --機能				  " & vbNewLine _
                                         & " 	      ,SPACE(2)                            AS KINOU_SAIMOKU           --機能細目				  " & vbNewLine _
                                         & " 	      ,'11'                                AS SAGYO_N_KBN      --作業内容区分  				  " & vbNewLine _
                                         & " 	      ,CONVERT(VARCHAR,GETDATE(),112)      AS SYORI_YMD         --伝送処理年月日				  " & vbNewLine _
                                         & " 	      ,FORMAT(GETDATE(),'HHmmss')          AS SYORI_JIKAN       --伝送処理時間				  " & vbNewLine _
                                         & " 	      ,SPACE(2)                            AS NYURYOKU_BASYO     --入力場所				  " & vbNewLine _
                                         & " 	      ,SPACE(2)                            AS NYURYOKU_BUMON     --入力部門				  " & vbNewLine _
                                         & " 	      ,SPACE(2)                            AS NYURYOKU_GROUP     --入力グループ				  " & vbNewLine _
                                         & " 	      ,light.SKYU_YMD                      AS SAGYOU_DATE        --作業年月日				  " & vbNewLine _
                                         & " 	      ,''                                  AS HINMEI_RYAKUGO     --品目略号				  " & vbNewLine _
                                         & " 	      ,'722'                               AS UG_CD              --UGコード				  " & vbNewLine _
                                         & " 	      ,''                                  AS NISUGATA           --荷姿				  " & vbNewLine _
                                         & " 	      ,'00000.00'                          AS YOURYO             --容量				  " & vbNewLine _
                                         & " 	      ,'AA'                                AS KEIYAKU_BASYO      --契約場所 'AA'固定				  " & vbNewLine _
                                         & " 	      ,'71'                                AS BIN_KBN            --便区分				  " & vbNewLine _
                                         & " 	      ,'H441B'                             AS BUTSURYU_COMP_CD   --物流会社コード H441B (日陸様を表すコードです)				  " & vbNewLine _
                                         & " 	      ,''                                  AS TSUMEAWASE_NO      --詰合わせNo.				  " & vbNewLine _
                                         & " 	      ,SPACE(7)                            AS JITSU_USER          --実ユーザー				  " & vbNewLine _
                                         & " 	      ,SPACE(7)                            AS KEIYAKUSAKI_CD      --契約先コード				  " & vbNewLine _
                                         & " 	      ,'2'                                 AS KOTEIHI_HEND_KBN    --固定費・変動費区分				  " & vbNewLine _
                                         & " 	      ,'0'                                 AS HANBAI_BUNRUI       --販売分類				  " & vbNewLine _
                                         & " 	      ,'2'                                 AS HACCHI_KBN          --発地情報　発地区分				  " & vbNewLine _
                                         & " 	      ,'A0I4'                              AS HACCHI_CD           --　　　　　　　発地コード				  " & vbNewLine _
                                         & " 	      ,''                                  AS HACCHI_CIKU_CD      --　　　　　　　市区町村コード				  " & vbNewLine _
                                         & " 	      ,'3'                                 AS CHAKUCHI_KBN          --着地情報　着地区分				  " & vbNewLine _
                                         & " 	      ,'Y1111'                             AS CHAKUCHI_CD           --　　　　　　　着地コード  ??????				  " & vbNewLine _
                                         & " 	      ,''                                  AS CHAKUCHI_CHIKU_CD     --　　　　　　　市区町村コード				  " & vbNewLine _
                                         & " 	      ,''                                  AS CHAKUCHI_NOUNYU_BASYO --　　　　　　　納入場所名				  " & vbNewLine _
                                         & " 	      ,'2'                                 AS HASSEI_BASYO_KBN      --発生場所区分				  " & vbNewLine _
                                         & " 	      ,'A0I4'                              AS HASEI_SP_CD           --発生SPコード				  " & vbNewLine _
                                         & " 	      ,'A0'                                AS SIHARAI_BASYO         --支払場所				  " & vbNewLine _
                                         & " 	      ,'2'                                 AS BUTURYU_TANI          --物流単位				  " & vbNewLine _
                                         & " 	      ,'+'                                 AS BUTURYU_FUGO          --物流量符号				  " & vbNewLine _
                                         & " 	      ,'00000000001.0000'                  AS BUTURYU_RYO           --物流量				  " & vbNewLine _
                                         & " 	      ,''                                  AS LOT_NO                --ロット№				  " & vbNewLine _
                                         & " 	      ,CASE WHEN light.TAX_RATE <> 0 THEN '1' 				  " & vbNewLine _
                                         & " 	        ELSE '2'               END  AS TAX_BN                --税区分				  " & vbNewLine _
                                         & " 	      ,'N'                               AS GN_KBN                --GN区分				  " & vbNewLine _
                                         & " 	      ,'+'            AS NET_AMT_FUGO          --NET金額符号				  " & vbNewLine _
                                         & " 	      ,FORMAT(light.KEISAN_TLGK,'000000000000000')   AS NET_AMT     --C				  " & vbNewLine _
                                         & " 					  " & vbNewLine _
                                         & " 	      ,'+'              AS GROSS_AMT_FUGO        --GROSS金額符号				  " & vbNewLine _
                                         & " 	      ,FORMAT(light.KEISAN_TLGK,'000000000000000')  AS GROSS_AMT    --GROSS金額  (ここではNET金額と同じ)				  " & vbNewLine _
                                         & " 					  " & vbNewLine _
                                         & " 	      ,'+'                               AS TAX_FUGO              --税額符号				  " & vbNewLine _
                                         & " 	      ----,FORMAT(ROUND(UNCHIN.DECI_UNCHIN * TAX_RATE,0),'000000000000000')                                AS TAX                   --税額				  " & vbNewLine _
                                         & " 	      ,'+'                               AS ZEIRITU_FUGO          --税率符号				  " & vbNewLine _
                                         & " 	      ,CONVERT(money,light.TAX_RATE)     AS ZEIRITSU              --税率				  " & vbNewLine _
                                         & " 	      ,'+'                               AS YUSO_KYORI_FUGO       --輸送距離符号				  " & vbNewLine _
                                         & " 	      ,FORMAT(0,'00000')                 AS YUSOKYORI             --輸送距離				  " & vbNewLine _
                                         & " 	      ,'  '                 AS SAIMOKU1_CD               --項目細分１　項目コード				  " & vbNewLine _
                                         & " 	      ,'+'                  AS SAIMOKU1_BETSU_AMT_FUGO   --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                         & " 	      ,FORMAT(0,'00000')    AS SAIMOKU1_BETU_AMT          --　　　　　　　　項目別金額				  " & vbNewLine _
                                         & " 	      ,'  '                 AS SAIMOKU2_CD                --項目細分２　項目コード				  " & vbNewLine _
                                         & " 	      ,'+'                  AS SAIMOKU2_BETSU_AMT_FUGO    --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                         & " 	      ,FORMAT(0,'00000')    AS SAIMOKU2_BETU_AMT          --　　　　　　　　項目別金額				  " & vbNewLine _
                                         & " 	      ,'  '                 AS SAIMOKU3_CD                --項目細分３　項目コード				  " & vbNewLine _
                                         & " 	      ,'+'                  AS SAIMOKU3_BETSU_AMT_FUGO    --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                         & " 	      ,FORMAT(0,'00000')    AS SAIMOKU3_BETU_AMT          --　　　　　　　　項目別金額				  " & vbNewLine _
                                         & " 	      ,'  '                 AS SAIMOKU4_CD                --項目細分４　項目コード				  " & vbNewLine _
                                         & " 	      ,'+'                  AS SAIMOKU4_BETSU_AMT_FUGO    --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                         & " 	      ,FORMAT(0,'00000')    AS SAIMOKU4_BETU_AMT          --　　　　　　　　項目別金額				  " & vbNewLine _
                                         & " 	      ,'  '                 AS SAIMOKU5_CD                --項目細分５　項目コード				  " & vbNewLine _
                                         & " 	      ,'+'                  AS SAIMOKU5_BETSU_AMT_FUGO    --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                         & " 	      ,FORMAT(0,'00000')    AS SAIMOKU5_BETU_AMT          --　　　　　　　　項目別金額				  " & vbNewLine _
                                         & " 	      ,'  '                 AS SAIMOKU6_CD                --項目細分６　項目コード				  " & vbNewLine _
                                         & " 	      ,'+'                  AS SAIMOKU6__BETSU_AMT_FUGO   --　　　　　　　　項目別金額符号				  " & vbNewLine _
                                         & " 	      ,FORMAT(0,'00000')    AS SAIMOKU6_BETU_AMT          --　　　　　　　　項目別金額				  " & vbNewLine _
                                         & " 	      ,''                   AS ERR_CD                     --エラーコード				  " & vbNewLine _
                                         & " 	      ,@E_DATE              AS NOUNYU_DATE                --納入日				  " & vbNewLine _
                                         & " 	      ,SPACE(5)             AS SYABAN                     --車番				  " & vbNewLine _
                                         & " 	      ,SPACE(6)             AS YOBI                       --FILLER				  " & vbNewLine _
                                         & " 	      ,' '                  AS HKN1_TAISYO_KISU           --保管対象期数				  " & vbNewLine _
                                         & " 	      ,'+'                  AS HKN1_KIMATU_ZAIKOSU_FUGO   --保管対象１　期末在庫数符号				  " & vbNewLine _
                                         & " 	      ,FORMAT(0,'00000000000.0000')              AS HKN1_KIMATU_ZAIKOSU        --         　　　 期末在庫数				  " & vbNewLine _
                                         & " 	      ,'+'                                       AS HKN1_NYUKO_SURYO_FUGO      --　　　　　　　　入庫数量符号				  " & vbNewLine _
                                         & " 	      ,FORMAT(0,'00000000000.0000')              AS HKN1_NYUKO_SURYO           --　　　　　　　　入庫数量				  " & vbNewLine _
                                         & " 	      ,'+'                                       AS HKN1_SYUKO_SURYO_FUGO      --　　　　　　　　出庫数量符号				  " & vbNewLine _
                                         & " 	      ,FORMAT(0,'00000000000.0000')              AS HKN1_SYUKO_SURYO           --　　　　　　　　出庫数量				  " & vbNewLine _
                                         & " 	      ,'+ '                                      AS HKN2_KIMATU_ZAIKOSU_FUGO   --保管対象２　期末在庫数符号				  " & vbNewLine _
                                         & " 	      ,FORMAT(0,'00000000000.0000')              AS HKN2_KIMATU_ZAIKOSU        --           　　　 期末在庫数				  " & vbNewLine _
                                         & " 	      ,'+'                                       AS HKN2_NYUKO_SURYO_FUGO      --　　　　　　　　入庫数量符号				  " & vbNewLine _
                                         & " 	      ,FORMAT(0,'00000000000.0000')              AS HKN2_NYUKO_SURYO           --　　　　　　　　入庫数量				  " & vbNewLine _
                                         & " 	      ,'+'                                       AS HKN2_SYUKO_SURYO_FUGO      --　　　　　　　　出庫数量符号				  " & vbNewLine _
                                         & " 	      ,FORMAT(0,'00000000000.0000')              AS HKN2_SYUKO_SURYO           --　　　　　　　　出庫数量				  " & vbNewLine _
                                         & " 	      ,'+'                                       AS HKN3_KIMATU_ZAIKOSU_FUGO   --保管対象３　期末在庫数符号				  " & vbNewLine _
                                         & " 	      ,FORMAT(0,'00000000000.0000')              AS HKN3_KIMATU_ZAIKOSU        --         　　　 期末在庫数				  " & vbNewLine _
                                         & " 	      ,'+'                                       AS HKN3_NYUKO_SURYO_FUGO      --　　　　　　　　入庫数量符号				  " & vbNewLine _
                                         & " 	      ,FORMAT(0,'00000000000.0000')              AS HKN3_NYUKO_SURYO           --　　　　　　　　入庫数量				  " & vbNewLine _
                                         & " 	      ,'+'                                       AS HKN3_SYUKO_SURYO_FUGO      --　　　　　　　　出庫数量符号				  " & vbNewLine _
                                         & " 	      ,FORMAT(0,'00000000000.0000')              AS HKN3_SYUKO_SURYO           --　　　　　　　　出庫数量				  " & vbNewLine _
                                         & " 	      ,SPACE(7)                                  AS YOBI1                      --FILLER				  " & vbNewLine _
                                         & " 	      ,''                                        AS ORDER_NO                   --オーダー№				  " & vbNewLine _
                                         & " 	      ,SPACE(8)                                  AS SYORI_DATE                 --処理日付				  " & vbNewLine _
                                         & " 	      ,SPACE(9)                                  AS SYORI_TIME                 --処理時間				  " & vbNewLine _
                                         & " 	      ,SPACE(2)                                  AS YOBI2                      --FILLER				  " & vbNewLine _
                                         & " 	      ,SPACE(2)                                  AS SITABARAI_KBN              --下払区分				  " & vbNewLine _
                                         & " 	      ,SPACE(7)                                  AS SITABARAI_CD               --下払先コード				  " & vbNewLine _
                                         & " 	      ,''                                        AS GRADE1                     --グレード1				  " & vbNewLine _
                                         & " 	      ,SPACE(3)                                  AS YOBI3                       --FILLER				  " & vbNewLine _
                                         & " 					  " & vbNewLine _
                                         & " 	  FROM  (				  " & vbNewLine _
                                         & " 	         SELECT				  " & vbNewLine _
                                         & " 	             SUM(ISNULL(KDTL.KEISAN_TLGK,0))   AS KEISAN_TLGK				  " & vbNewLine _
                                         & " 	      ,KDTL.TAX_CD_JDE          AS TAX_CD_JDE				  " & vbNewLine _
                                         & " 	          ,ISNULL(TAX.TAX_RATE,0)   AS TAX_RATE 				  " & vbNewLine _
                                         & " 	      				  " & vbNewLine _
                                         & " 	      ,LEFT(KHED.SKYU_DATE,6)   AS  SKYU_YM				  " & vbNewLine _
                                         & " 	      ,KHED.SKYU_DATE           AS  SKYU_YMD				  " & vbNewLine _
                                         & " 	         FROM LM_TRN..G_KAGAMI_HED KHED				  " & vbNewLine _
                                         & " 	          LEFT JOIN LM_TRN..G_KAGAMI_DTL KDTL ON				  " & vbNewLine _
                                         & " 	            KDTL.SKYU_NO = KHED.SKYU_NO				  " & vbNewLine _
                                         & " 	          AND KDTL.SYS_DEL_FLG = '0'				  " & vbNewLine _
                                         & " 	          AND KDTL.GROUP_KB    IN ('03','05','10','12')    --'03':運賃 ,'05':横持料 ,'10':再保管用 , '12':再保管横持料				  " & vbNewLine _
                                         & " 	                  AND KDTL.MAKE_SYU_KB = '01'    --追加				  " & vbNewLine _
                                         & " 	                   --税率マスタ				  " & vbNewLine _
                                         & " 	          LEFT JOIN LM_MST..M_TAX TAX				  " & vbNewLine _
                                         & " 	             ON TAX.TAX_CD_JDE = KDTL.TAX_CD_JDE				  " & vbNewLine _
                                         & " 	             AND TAX.SYS_DEL_FLG = '0'				  " & vbNewLine _
                                         & " 					  " & vbNewLine _
                                         & " 	          WHERE KHED.NRS_BR_CD = '60'				  " & vbNewLine _
                                         & " 	          AND SEIQTO_CD = '3251699'   --日合請求先CD				  " & vbNewLine _
                                         & " 	          AND KHED.SKYU_DATE >= @S_DATE				  " & vbNewLine _
                                         & " 	          AND KHED.SKYU_DATE <= @E_DATE				  " & vbNewLine _
                                         & " 	          AND KHED.SYS_DEL_FLG = '0'				  " & vbNewLine _
                                         & " 	          AND KDTL.SKYU_NO = KHED.SKYU_NO				  " & vbNewLine _
                                         & " 	          GROUP BY KDTL.TAX_CD_JDE				  " & vbNewLine _
                                         & " 	              ,TAX.TAX_RATE				  " & vbNewLine _
                                         & " 	              ,LEFT(KHED.SKYU_DATE,6)				  " & vbNewLine _
                                         & " 	              ,KHED.SKYU_DATE) light				  " & vbNewLine _
                                         & " 					  " & vbNewLine _
                                         & " 	)  BUTSU  				  " & vbNewLine



    Private Const SQL_BUTSURYUHI_CHK4 As String = " 	SELECT				 " & vbNewLine _
                                            & " 	 '4'    as SQ_NO				 " & vbNewLine _
                                            & " 	 ,isnull(sum( CASE WHEN RN = 1 				 " & vbNewLine _
                                            & " 	                         THEN  isnull(NET_AMT,0) + isnull(DIFF,0)				 " & vbNewLine _
                                            & " 	                         ELSE  isnull(NET_AMT,0)        				 " & vbNewLine _
                                            & " 	     END ),0)   as NET_AMT                   --NET金額				 " & vbNewLine _
                                            & " 					 " & vbNewLine _
                                            & " 	FROM (				 " & vbNewLine

    Private Const SQL_BUTSURYUHI_DATA4 As String = " 	SELECT				  " & vbNewLine _
                                            & " 	   CONVERT(char(4),RECORD_ID)               --レコードID				  " & vbNewLine _
                                            & " 	 + CONVERT(char(3),DATA_ID)                 --データID         データIDの3桁目：V     (日陸様を表すコードです)				  " & vbNewLine _
                                            & " 	 + CONVERT(char(3),COMP_CD)                 --会社コード 040   (日本合成化学工業を表すコードです)				  " & vbNewLine _
                                            & " 	 + CONVERT(char(6),KEIJYO_YM)               --計上年月				  " & vbNewLine _
                                            & " 	 + CONVERT(char(12),UKETUKE_NO)             --受付№				  " & vbNewLine _
                                            & " 	 + CONVERT(char(12),BUTURYU_SERI_NO)        --物流整理№				  " & vbNewLine _
                                            & " 	 + CONVERT(char(3),TEISEI_NO)          --訂正No.				  " & vbNewLine _
                                            & " 	 + CONVERT(char(2),TEISEI_KBN)         --訂正区分				  " & vbNewLine _
                                            & " 	 + CONVERT(char(2),KINOU)              --機能				  " & vbNewLine _
                                            & " 	 + CONVERT(char(2),KINOU_SAIMOKU)      --機能細目				  " & vbNewLine _
                                            & " 	 + CONVERT(char(2),SAGYO_N_KBN)         --作業内容区分  	     			  " & vbNewLine _
                                            & " 	 + CONVERT(char(8),SYORI_YMD)          --伝送処理年月日				  " & vbNewLine _
                                            & " 	 + CONVERT(char(9),SYORI_JIKAN)        --伝送処理時間				  " & vbNewLine _
                                            & " 	 + CONVERT(char(2),NYURYOKU_BASYO)     --入力場所				  " & vbNewLine _
                                            & " 	 + CONVERT(char(2),NYURYOKU_BUMON)     --入力部門				  " & vbNewLine _
                                            & " 	 + CONVERT(char(2),NYURYOKU_GROUP)     --入力グループ				  " & vbNewLine _
                                            & " 	 + CONVERT(char(8),SAGYOU_DATE)        --作業年月日				  " & vbNewLine _
                                            & " 	 + CONVERT(char(10),HINMEI_RYAKUGO)    --品目略号				  " & vbNewLine _
                                            & " 	 + CONVERT(char(5),UG_CD)              --UGコード				  " & vbNewLine _
                                            & " 	 + CONVERT(char(2),NISUGATA)           --荷姿				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(YOURYO, 0), '00000.00') --容量				  " & vbNewLine _
                                            & " 	 + CONVERT(char(2),KEIYAKU_BASYO)      --契約場所 'AA'固定				  " & vbNewLine _
                                            & " 	 + CONVERT(char(2),BIN_KBN)            --便区分				  " & vbNewLine _
                                            & " 	 + CONVERT(char(7),BUTSURYU_COMP_CD)   --物流会社コード H441B (日陸様を表すコードです)				  " & vbNewLine _
                                            & " 	 + CONVERT(char(10),TSUMEAWASE_NO)     --詰合わせNo.				  " & vbNewLine _
                                            & " 	 + CONVERT(char(7),JITSU_USER)         --実ユーザー				  " & vbNewLine _
                                            & " 	 + CONVERT(char(7),KEIYAKUSAKI_CD)     --契約先コード				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),KOTEIHI_HEND_KBN)   --固定費・変動費区分				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),HANBAI_BUNRUI)      --販売分類				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),HACCHI_KBN)         --発地情報 発地区分				  " & vbNewLine _
                                            & " 	 + CONVERT(char(7),HACCHI_CD)          --       発地コード				  " & vbNewLine _
                                            & " 	 + CONVERT(char(6),HACCHI_CIKU_CD)     --       市区町村コード				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),CHAKUCHI_KBN)           --着地情報 着地区分				  " & vbNewLine _
                                            & " 	 + CONVERT(char(7),CHAKUCHI_CD)            --       着地コード				  " & vbNewLine _
                                            & " 	 + CONVERT(char(6),CHAKUCHI_CHIKU_CD)      --       市区町村コード				  " & vbNewLine _
                                            & " 	 + CONVERT(char(20),CHAKUCHI_NOUNYU_BASYO) --       納入場所名				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),HASSEI_BASYO_KBN)       --発生場所区分				  " & vbNewLine _
                                            & " 	 + CONVERT(char(7),HASEI_SP_CD)            --発生SPコード				  " & vbNewLine _
                                            & " 	 + CONVERT(char(2),SIHARAI_BASYO)          --支払場所				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),BUTURYU_TANI)           --物流単位				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),BUTURYU_FUGO)           --物流量符号				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(BUTURYU_RYO,0), '00000000000.0000')           --物流量				  " & vbNewLine _
                                            & " 	 + CONVERT(char(10),LOT_NO)                --ロット№				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),TAX_BN)                 --税区分				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),GN_KBN)                 --GN区分				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),NET_AMT_FUGO)           --NET金額符号				  " & vbNewLine _
                                            & " 	 + CONVERT(char(15),CASE WHEN RN = 1 				  " & vbNewLine _
                                            & " 	                         THEN  FORMAT(NET_AMT + DIFF,'000000000000000')				  " & vbNewLine _
                                            & " 	                         ELSE  FORMAT(NET_AMT ,'000000000000000')        				  " & vbNewLine _
                                            & " 	                    END)                   --NET金額				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),GROSS_AMT_FUGO)         --GROSS金額符号				  " & vbNewLine _
                                            & " 	 + CONVERT(char(15),CASE WHEN RN = 1 				  " & vbNewLine _
                                            & " 	                         THEN  FORMAT(NET_AMT + TAX + DIFF + DIFF_TAX,'000000000000000')				  " & vbNewLine _
                                            & " 	                         ELSE  FORMAT(NET_AMT + TAX ,'000000000000000')        				  " & vbNewLine _
                                            & " 	                    END)                  --GROSS金額				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),TAX_FUGO)              --税額符号				  " & vbNewLine _
                                            & " 	 + FORMAT(CASE WHEN RN = 1 				  " & vbNewLine _
                                            & " 	               THEN TAX + DIFF_TAX				  " & vbNewLine _
                                            & " 	               ELSE TAX				  " & vbNewLine _
                                            & " 	          END, '000000000000000')             --税額				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(ZEIRITU_FUGO,'+'))          --税率符号				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(ZEIRITSU * 100,0), '000.00')              --税率				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(YUSO_KYORI_FUGO,'+'))       --輸送距離符号				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(YUSOKYORI,''), '00000')             --輸送距離				  " & vbNewLine _
                                            & " 	 + CONVERT(char(2),ISNULL(SAIMOKU1_CD,''))               --項目細分１ 項目コード				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(SAIMOKU1_BETSU_AMT_FUGO,'+'))   --        項目別金額符号				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(SAIMOKU1_BETU_AMT,''), '00000')          --        項目別金額				  " & vbNewLine _
                                            & " 	 + CONVERT(char(2),ISNULL(SAIMOKU2_CD,''))                --項目細分２ 項目コード				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(SAIMOKU2_BETSU_AMT_FUGO,'+'))    --        項目別金額符号				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(SAIMOKU2_BETU_AMT,''), '00000')          --        項目別金額				  " & vbNewLine _
                                            & " 	 + CONVERT(char(2),ISNULL(SAIMOKU3_CD,''))                --項目細分３ 項目コード				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(SAIMOKU3_BETSU_AMT_FUGO,'+'))    --        項目別金額符号				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(SAIMOKU3_BETU_AMT,''), '00000')          --        項目別金額				  " & vbNewLine _
                                            & " 	 + CONVERT(char(2),ISNULL(SAIMOKU4_CD,''))                --項目細分４ 項目コード				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(SAIMOKU4_BETSU_AMT_FUGO,'+'))    --        項目別金額符号				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(SAIMOKU4_BETU_AMT,''), '00000')          --        項目別金額				  " & vbNewLine _
                                            & " 	 + CONVERT(char(2),ISNULL(SAIMOKU5_CD,''))                --項目細分５ 項目コード				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(SAIMOKU5_BETSU_AMT_FUGO,'+'))    --        項目別金額符号				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(SAIMOKU5_BETU_AMT,''), '00000')          --        項目別金額				  " & vbNewLine _
                                            & " 	 + CONVERT(char(2),ISNULL(SAIMOKU6_CD,''))                --項目細分６ 項目コード				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(SAIMOKU6__BETSU_AMT_FUGO,'+'))   --        項目別金額符号				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(SAIMOKU6_BETU_AMT,''), '00000')          --        項目別金額				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(ERR_CD,''), '0000')                     --エラーコード				  " & vbNewLine _
                                            & " 	 + CONVERT(char(8),ISNULL(NOUNYU_DATE,''))                --納入日				  " & vbNewLine _
                                            & " 	 + CONVERT(char(5),ISNULL(SYABAN,''))                     --車番				  " & vbNewLine _
                                            & " 	 + CONVERT(char(6),ISNULL(YOBI,''))                       --FILLER				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(HKN1_TAISYO_KISU,''))           --保管対象期数				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(HKN1_KIMATU_ZAIKOSU_FUGO,'+'))   --保管対象１ 期末在庫数符号				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(HKN1_KIMATU_ZAIKOSU,0), '00000000000.0000')        --             期末在庫数				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(HKN1_NYUKO_SURYO_FUGO,'+'))      --        入庫数量符号				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(HKN1_NYUKO_SURYO,0), '00000000000.0000')           --        入庫数量				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(HKN1_SYUKO_SURYO_FUGO,'+'))      --        出庫数量符号				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(HKN1_SYUKO_SURYO,0), '00000000000.0000')          --        出庫数量				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(HKN2_KIMATU_ZAIKOSU_FUGO,'+'))   --保管対象２ 期末在庫数符号				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(HKN2_KIMATU_ZAIKOSU,0), '00000000000.0000')       --               期末在庫数				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(HKN2_NYUKO_SURYO_FUGO,'+'))      --        入庫数量符号				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(HKN2_NYUKO_SURYO,0), '00000000000.0000')          --        入庫数量				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(HKN2_SYUKO_SURYO_FUGO,'+'))      --        出庫数量符号				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(HKN2_SYUKO_SURYO,0), '00000000000.0000')          --        出庫数量				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(HKN3_KIMATU_ZAIKOSU_FUGO,'+'))   --保管対象３ 期末在庫数符号				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(HKN3_KIMATU_ZAIKOSU,0), '00000000000.0000')       --             期末在庫数				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(HKN3_NYUKO_SURYO_FUGO,'+'))      --        入庫数量符号				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(HKN3_NYUKO_SURYO,0), '00000000000.0000')          --        入庫数量				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(HKN3_SYUKO_SURYO_FUGO,'+'))      --        出庫数量符号				  " & vbNewLine _
                                            & " 	 + FORMAT(ISNULL(HKN3_SYUKO_SURYO,0), '00000000000.0000')          --        出庫数量				  " & vbNewLine _
                                            & " 	 + CONVERT(char(7),ISNULL(YOBI1,''))                      --FILLER				  " & vbNewLine _
                                            & " 	 + CONVERT(char(10),ISNULL(ORDER_NO,''))                  --オーダー№				  " & vbNewLine _
                                            & " 	 + CONVERT(char(8),ISNULL(SYORI_DATE,''))                 --処理日付				  " & vbNewLine _
                                            & " 	 + CONVERT(char(9),ISNULL(SYORI_TIME,''))                 --処理時間				  " & vbNewLine _
                                            & " 	 + CONVERT(char(2),ISNULL(YOBI2,''))                      --FILLER				  " & vbNewLine _
                                            & " 	 + CONVERT(char(1),ISNULL(SITABARAI_KBN,''))              --下払区分				  " & vbNewLine _
                                            & " 	 + CONVERT(char(7),ISNULL(SITABARAI_CD,''))               --下払先コード				  " & vbNewLine _
                                            & " 	 + CONVERT(char(10),ISNULL(GRADE1,''))                    --グレード1				  " & vbNewLine _
                                            & " 	 + CONVERT(char(3),ISNULL(YOBI3,''))                      --FILLER				  " & vbNewLine _
                                            & "        AS  NET_AMT				  " & vbNewLine _
                                            & " 	FROM (				  " & vbNewLine


    Private Const SQL_BUTSURYUHI_SQL4 As String = " 	SELECT 				 " & vbNewLine _
                                        & " 	      'VK74'                                            AS RECORD_ID          --レコードID				 " & vbNewLine _
                                        & " 	     , NG.DATA_ID                                       AS DATA_ID            --データID         データIDの3桁目：V     (日陸様を表すコードです)				 " & vbNewLine _
                                        & " 	     , '040'                                            AS COMP_CD            --会社コード 040   (日本合成化学工業を表すコードです)				 " & vbNewLine _
                                        & " 	     , @KEIJYO                                          AS KEIJYO_YM          --計上年月				 " & vbNewLine _
                                        & " 	     , NG.UKETUKE_NO                                    AS UKETUKE_NO         --受付№				 " & vbNewLine _
                                        & " 	     , NG.BUTURYU_SERI_NO                               AS BUTURYU_SERI_NO    --物流整理№				 " & vbNewLine _
                                        & " 	     , '000'                                            AS TEISEI_NO             --訂正No.(別紙3:000, 001)				 " & vbNewLine _
                                        & " 	     , '1'                                              AS TEISEI_KBN            --訂正区分(別紙3:1,21,2,31)				 " & vbNewLine _
                                        & " 	     , ''                                               AS KINOU                 --機能(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS KINOU_SAIMOKU         --機能細目(設定不要)				 " & vbNewLine _
                                        & " 	     , NG.SAGYO_N_KBN                                   AS SAGYO_N_KBN           --作業内容区分(別紙4:)				 " & vbNewLine _
                                        & " 	     , CONVERT(VARCHAR, GETDATE(),112)                  AS SYORI_YMD             --伝送処理年月日				 " & vbNewLine _
                                        & " 	     , FORMAT(GETDATE(), 'HHmmss')                      AS SYORI_JIKAN           --伝送処理時間				 " & vbNewLine _
                                        & " 	     , ''                                               AS NYURYOKU_BASYO        --入力場所(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS NYURYOKU_BUMON        --入力部門(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS NYURYOKU_GROUP        --入力グループ(設定不要)				 " & vbNewLine _
                                        & " 	     , NG.SAGYOU_DATE                                   AS SAGYOU_DATE           --作業年月日(荷役作業日)				 " & vbNewLine _
                                        & " 	     , NG.HINMEI_RYAKUGO                                AS HINMEI_RYAKUGO        --品名略号				 " & vbNewLine _
                                        & " 	     , NG.UG_CD                                         AS UG_CD                 --UGコード				 " & vbNewLine _
                                        & " 	     , NG.NISUGATA                                      AS NISUGATA              --荷姿				 " & vbNewLine _
                                        & " 	     , NG.YOURYO                                        AS YOURYO                --容量				 " & vbNewLine _
                                        & " 	     , 'AA'                                             AS KEIYAKU_BASYO         --契約場所 'AA'固定				 " & vbNewLine _
                                        & " 	     , ''                                               AS BIN_KBN               --便区分(設定不要)				 " & vbNewLine _
                                        & " 	     , 'H441B'                                          AS BUTSURYU_COMP_CD      --物流会社コード H441B (日陸様を表すコードです)				 " & vbNewLine _
                                        & " 	     , ''                                               AS TSUMEAWASE_NO         --積合わせNo.(設定不要)				 " & vbNewLine _
                                        & " 	     , NG.JITSU_USER                                    AS JITSU_USER            --実ユーザー(受注先コード)				 " & vbNewLine _
                                        & " 	     , NG.KEIYAKUSAKI_CD                                AS KEIYAKUSAKI_CD        --契約先コード(支払人コード)				 " & vbNewLine _
                                        & " 	     , '2'                                              AS KOTEIHI_HEND_KBN      --固定費・変動費区分				 " & vbNewLine _
                                        & " 	     , CASE WHEN LEN(NG.HANBAI_BUNRUI) > 0 				 " & vbNewLine _
                                        & " 	            THEN NG.HANBAI_BUNRUI				 " & vbNewLine _
                                        & " 	            ELSE '0'				 " & vbNewLine _
                                        & " 	        END                                             AS HANBAI_BUNRUI         --販売分類				 " & vbNewLine _
                                        & " 	     , ''                                               AS HACCHI_KBN            --発地情報 発地区分(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS HACCHI_CD             --         発地コード(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS HACCHI_CIKU_CD        --         市区町村コード(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS CHAKUCHI_KBN          --着地情報 着地区分(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS CHAKUCHI_CD           --         着地コード(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS CHAKUCHI_CHIKU_CD     --         市区町村コード(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS CHAKUCHI_NOUNYU_BASYO --         納入場所名(設定不要)				 " & vbNewLine _
                                        & " 	     , '2'                                              AS HASSEI_BASYO_KBN      --発生場所区分				 " & vbNewLine _
                                        & " 	     , NG.HASEI_SP_CD                                   AS HASEI_SP_CD           --発生SPコード				 " & vbNewLine _
                                        & " 	     , NG.SIHARAI_BASYO                                 AS SIHARAI_BASYO         --支払場所				 " & vbNewLine _
                                        & " 	     , '2'                                              AS BUTURYU_TANI          --物流単位				 " & vbNewLine _
                                        & " 	     , CASE WHEN NG.BUTURYU_RYO < 0				 " & vbNewLine _
                                        & " 	            THEN '-'				 " & vbNewLine _
                                        & " 	            ELSE '+'				 " & vbNewLine _
                                        & " 	        END                                             AS BUTURYU_FUGO          --物流量符号				 " & vbNewLine _
                                        & " 	     , NG.BUTURYU_RYO                                   AS BUTURYU_RYO           --物流量				 " & vbNewLine _
                                        & " 	     , ''                                               AS LOT_NO                --ロット№(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN SKM.TAX_RATE <> 0 THEN '1' 				 " & vbNewLine _
                                        & " 	            ELSE '2'               				 " & vbNewLine _
                                        & " 	       END                                              AS TAX_BN                --税区分				 " & vbNewLine _
                                        & " 	     , 'N'                                              AS GN_KBN                --GN区分(G or N)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS NET_AMT_FUGO          --NET金額符号				 " & vbNewLine _
                                        & " 	     , CASE WHEN NG.IS_TYPE_M = ''				 " & vbNewLine _
                                        & " 	            THEN CASE WHEN NG.RN = 1				 " & vbNewLine _
                                        & " 	                      THEN FLOOR(SKM.HANDLING_AMO_TTL * NG.WIGHT_KG / SKM.TOTAL_WT) 				 " & vbNewLine _
                                        & " 	                      ELSE FLOOR(SKM.HANDLING_AMO_TTL * NG.WIGHT_KG / SKM.TOTAL_WT)				 " & vbNewLine _
                                        & " 	                 END				 " & vbNewLine _
                                        & " 	            ELSE CASE WHEN NG.IS_TYPE_M = '01' 				 " & vbNewLine _
                                        & " 	                      THEN CEILING(SKM.HANDLING_AMO_TTL / 2)				 " & vbNewLine _
                                        & " 	                      ELSE FLOOR(SKM.HANDLING_AMO_TTL / 2)				 " & vbNewLine _
                                        & " 	                 END				 " & vbNewLine _
                                        & " 	        END                                             AS NET_AMT               --NET金額				 " & vbNewLine _
                                        & " 	     , '+'                                              AS GROSS_AMT_FUGO        --GROSS金額符号				 " & vbNewLine _
                                        & " 	     , ''                                               AS GROSS_AMT             --GROSS金額				 " & vbNewLine _
                                        & " 	     , '+'                                              AS TAX_FUGO              --税額符号				 " & vbNewLine _
                                        & " 	     , ROUND(CASE WHEN NG.IS_TYPE_M = ''				 " & vbNewLine _
                                        & " 	            THEN CASE WHEN NG.RN = 1				 " & vbNewLine _
                                        & " 	                      THEN FLOOR(SKM.HANDLING_AMO_TTL * NG.WIGHT_KG / SKM.TOTAL_WT)				 " & vbNewLine _
                                        & " 	                      ELSE FLOOR(SKM.HANDLING_AMO_TTL * NG.WIGHT_KG / SKM.TOTAL_WT)				 " & vbNewLine _
                                        & " 	                 END				 " & vbNewLine _
                                        & " 	            ELSE CASE WHEN NG.IS_TYPE_M = '01' 				 " & vbNewLine _
                                        & " 	                      THEN CEILING(SKM.HANDLING_AMO_TTL / 2)				 " & vbNewLine _
                                        & " 	                      ELSE FLOOR(SKM.HANDLING_AMO_TTL / 2)				 " & vbNewLine _
                                        & " 	                 END				 " & vbNewLine _
                                        & " 	        END * SKM.TAX_RATE, 0)                          AS TAX                        --税額				 " & vbNewLine _
                                        & " 	     , '+'                                              AS ZEIRITU_FUGO               --税率符号				 " & vbNewLine _
                                        & " 	     ,  SKM.TAX_RATE                                    AS ZEIRITSU                   --税率				 " & vbNewLine _
                                        & " 	     , '+'                                              AS YUSO_KYORI_FUGO            --輸送距離符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS YUSOKYORI                  --輸送距離(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SAIMOKU1_CD                --項目細分１ 項目コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS SAIMOKU1_BETSU_AMT_FUGO    --      項目別金額符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS SAIMOKU1_BETU_AMT          --       項目別金額(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SAIMOKU2_CD                --項目細分２ 項目コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS SAIMOKU2_BETSU_AMT_FUGO    --      項目別金額符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS SAIMOKU2_BETU_AMT          --      項目別金額(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SAIMOKU3_CD                --項目細分３ 項目コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS SAIMOKU3_BETSU_AMT_FUGO    --      項目別金額符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS SAIMOKU3_BETU_AMT          --        項目別金額(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SAIMOKU4_CD                --項目細分４ 項目コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS SAIMOKU4_BETSU_AMT_FUGO    --        項目別金額符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS SAIMOKU4_BETU_AMT          --        項目別金額(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SAIMOKU5_CD                --項目細分５ 項目コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS SAIMOKU5_BETSU_AMT_FUGO    --        項目別金額符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS SAIMOKU5_BETU_AMT          --        項目別金額(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SAIMOKU6_CD                --項目細分６ 項目コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS SAIMOKU6__BETSU_AMT_FUGO   --        項目別金額符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS SAIMOKU6_BETU_AMT          --        項目別金額(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS ERR_CD                     --エラーコード(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS NOUNYU_DATE                --納入日(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SYABAN                     --車番(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS YOBI                       --FILLER(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS HKN1_TAISYO_KISU           --保管対象期数(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS HKN1_KIMATU_ZAIKOSU_FUGO   --保管対象１ 期末在庫数符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS HKN1_KIMATU_ZAIKOSU        --             期末在庫数(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS HKN1_NYUKO_SURYO_FUGO      --        入庫数量符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS HKN1_NYUKO_SURYO           --        入庫数量(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS HKN1_SYUKO_SURYO_FUGO      --        出庫数量符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS HKN1_SYUKO_SURYO           --        出庫数量(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS HKN2_KIMATU_ZAIKOSU_FUGO   --保管対象２ 期末在庫数符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS HKN2_KIMATU_ZAIKOSU        --               期末在庫数(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS HKN2_NYUKO_SURYO_FUGO      --        入庫数量符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS HKN2_NYUKO_SURYO           --        入庫数量(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS HKN2_SYUKO_SURYO_FUGO      --        出庫数量符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS HKN2_SYUKO_SURYO           --        出庫数量(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS HKN3_KIMATU_ZAIKOSU_FUGO   --保管対象３ 期末在庫数符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS HKN3_KIMATU_ZAIKOSU        --             期末在庫数(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS HKN3_NYUKO_SURYO_FUGO      --        入庫数量符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS HKN3_NYUKO_SURYO           --        入庫数量(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS HKN3_SYUKO_SURYO_FUGO      --        出庫数量符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS HKN3_SYUKO_SURYO           --        出庫数量(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS YOBI1                      -- FILLER(設定不要)				 " & vbNewLine _
                                        & " 	     , NG.ORDER_NO                                      AS ORDER_NO                   -- オーダー№				 " & vbNewLine _
                                        & " 	     , ''                                               AS SYORI_DATE                 -- 処理日付(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SYORI_TIME                 -- 処理時間(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS YOBI2                      -- FILLER(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SITABARAI_KBN              --下払区分(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SITABARAI_CD               --下払先コード(設定不要)				 " & vbNewLine _
                                        & " 	     , NG.GRADE1                                        AS GRADE1                      --グレード1				 " & vbNewLine _
                                        & " 	     , ''                                               AS YOBI3                      -- FILLER(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN SKM.IS_TYPE_M = ''  				 " & vbNewLine _
                                        & " 	            THEN NG.RN				 " & vbNewLine _
                                        & " 	            ELSE 0				 " & vbNewLine _
                                        & " 	       END                                              AS RN				 " & vbNewLine _
                                        & " 	     , ISNULL(SKM.DIFF, 0)                              AS DIFF				 " & vbNewLine _
                                        & " 	     , ISNULL(TAX_DIFF, 0)                              AS DIFF_TAX				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	  FROM				 " & vbNewLine _
                                        & " 	(				 " & vbNewLine _
                                        & " 	select BL.NRS_BR_CD				 " & vbNewLine _
                                        & " 	     , BL.INKA_NO_L                                         AS INOUTKA_NO_L				 " & vbNewLine _
                                        & " 	     , 'IN'                                                 AS IN_OUT_TYPE 				 " & vbNewLine _
                                        & " 	     , CASE WHEN NG.NRS_BR_CD IS NULL				 " & vbNewLine _
                                        & " 	            THEN TM.KBN_CD				 " & vbNewLine _
                                        & " 	            ELSE ''				 " & vbNewLine _
                                        & " 	       END                                                  AS IS_TYPE_M				 " & vbNewLine _
                                        & " 	     , CASE WHEN NG.NRS_BR_CD IS NOT NULL  				 " & vbNewLine _
                                        & " 	            THEN '21V'				 " & vbNewLine _
                                        & " 	            ELSE '41V'				 " & vbNewLine _
                                        & " 	        END                                                 AS DATA_ID				 " & vbNewLine _
                                        & " 	     , ISNULL(CASE WHEN NG.DATA_ID_DETAIL = 'K' 				 " & vbNewLine _
                                        & " 	                   THEN NG.HACCHU_DENP_NO				 " & vbNewLine _
                                        & " 	                   ELSE NG.OUTKA_DENP_NO				 " & vbNewLine _
                                        & " 	               END, '')                                     AS UKETUKE_NO            --受付№				 " & vbNewLine _
                                        & " 	     , SUBSTRING(BL.INKA_DATE, 3, 4) 				 " & vbNewLine _
                                        & " 	     + 'N'				 " & vbNewLine _
                                        & " 	     + FORMAT(row_number() over(ORDER BY NG.INKA_CTL_NO_L				 " & vbNewLine _
                                        & " 	                                       , NG.INKA_CTL_NO_M  ), '0000000')				 " & vbNewLine _
                                        & " 	                                                            AS BUTURYU_SERI_NO       --物流整理№				 " & vbNewLine _
                                        & " 	     , '51'                                                 AS SAGYO_N_KBN           --作業内容区分(別紙4:)				 " & vbNewLine _
                                        & " 	     , BL.INKA_DATE                                         AS SAGYOU_DATE           --作業年月日(荷役作業日)				 " & vbNewLine _
                                        & " 	     , ISNULL(NG.ITEM_RYAKUGO, '')                          AS HINMEI_RYAKUGO        --品名略号				 " & vbNewLine _
                                        & " 	     , ISNULL(NG.UG, TM.KBN_NM1)                            AS UG_CD                 --UGコード				 " & vbNewLine _
                                        & " 	     , ISNULL(NG.NISUGATA_CD, '')                           AS NISUGATA              --荷姿				 " & vbNewLine _
                                        & " 	     , TRY_PARSE(ISNULL(NG.YOURYOU, 0) AS numeric(5,2))     AS YOURYO                --容量				 " & vbNewLine _
                                        & " 	     , ISNULL(NG.YUSYUTSU_KBN, '0')                         AS HANBAI_BUNRUI         --販売分類				 " & vbNewLine _
                                        & " 	     , ISNULL(NG.INKA_HOKAN_BASYO, 'A0I4')                  AS HASEI_SP_CD           --発生SPコード				 " & vbNewLine _
                                        & " 	     , ISNULL(LEFT(NG.INKA_HOKAN_BASYO, 2), 'A0')           AS SIHARAI_BASYO         --支払場所				 " & vbNewLine _
                                        & " 	     , ISNULL(NG.SUURYO, 1)                                 AS BUTURYU_RYO           --物流量				 " & vbNewLine _
                                        & " 	     , ISNULL(NG.JURYO_KANSAN_KEISU * NG.SUURYO, 0)         AS WIGHT_KG				 " & vbNewLine _
                                        & " 	     , ''                                                   AS JITSU_USER            --実ユーザー(受注先コード)				 " & vbNewLine _
                                        & " 	     , ''                                                   AS KEIYAKUSAKI_CD        --契約先コード(支払人コード)				 " & vbNewLine _
                                        & " 	     , ISNULL(CASE WHEN NG.DATA_ID_DETAIL = 'K' 				 " & vbNewLine _
                                        & " 	            THEN NG.HACCHU_DENP_DTL_NO + FORMAT(NG.RENBAN, '0')				 " & vbNewLine _
                                        & " 	            ELSE NG.OUTKA_DENP_DTL_NO				 " & vbNewLine _
                                        & " 	       END, '')                                             AS ORDER_NO              -- オーダー№				 " & vbNewLine _
                                        & " 	     , ISNULL(NG.GRADE1, '')                                AS GRADE1                --グレード1				 " & vbNewLine _
                                        & " 	     , ROW_NUMBER() OVER(PARTITION BY NG.INKA_CTL_NO_L 				 " & vbNewLine _
                                        & " 	                             ORDER BY NG.INKA_CTL_NO_L, NG.INKA_CTL_NO_M) AS RN				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	     				 " & vbNewLine _
                                        & " 	  from LM_TRN..B_INKA_L AS BL				 " & vbNewLine _
                                        & " 	                 LEFT JOIN				 " & vbNewLine _
                                        & " 	                      LM_TRN..H_INKAEDI_DTL_NCGO_NEW AS NG				 " & vbNewLine _
                                        & " 	                   ON NG.NRS_BR_CD     = BL.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                  AND NG.INKA_CTL_NO_L = BL.INKA_NO_L				 " & vbNewLine _
                                        & " 	                  AND NG.SYS_DEL_FLG   = '0'				 " & vbNewLine _
                                        & " 	                  AND NG.DEL_KB        = '0'				 " & vbNewLine _
                                        & " 	                LEFT JOIN				 " & vbNewLine _
                                        & " 	                     LM_MST..Z_KBN AS TM				 " & vbNewLine _
                                        & " 	                  ON TM.KBN_GROUP_CD = 'M026'				 " & vbNewLine _
                                        & " 	                 AND TM.SYS_DEL_FLG  = '0'				 " & vbNewLine _
                                        & " 	                 AND NG.NRS_BR_CD IS NULL 				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                				 " & vbNewLine _
                                        & " 	                          				 " & vbNewLine _
                                        & " 	 where                        				 " & vbNewLine _
                                        & " 	                       BL.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	                       AND LEFT(BL.INKA_DATE, 6) = @KEIJYO                     				 " & vbNewLine _
                                        & " 	                       AND BL.CUST_CD_L = '32516'				 " & vbNewLine _
                                        & " --DEL 2022/08/03 031530             AND BL.CUST_CD_M = '00'   				 " & vbNewLine _
                                        & " 	                       AND BL.INKA_STATE_KB >= '50'				 " & vbNewLine _
                                        & " 	                       AND BL.INKA_KB = '10'				 " & vbNewLine _
                                        & " 	                       AND BL.NIYAKU_YN = '01'				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	----------------------------				 " & vbNewLine _
                                        & " 	union 				 " & vbNewLine _
                                        & " 	----------------------------				 " & vbNewLine _
                                        & " 	select CL.NRS_BR_CD				 " & vbNewLine _
                                        & " 	     , CL.OUTKA_NO_L                                         AS INOUTKA_CTL_NO_L				 " & vbNewLine _
                                        & " 	     , 'OUT'                                                 AS IN_OUT_TYPE 				 " & vbNewLine _
                                        & " 	     , CASE WHEN NG.NRS_BR_CD IS NULL				 " & vbNewLine _
                                        & " 	            THEN TM.KBN_CD              				 " & vbNewLine _
                                        & " 	            ELSE ''				 " & vbNewLine _
                                        & " 	       END                                                   AS IS_TYPE_M				 " & vbNewLine _
                                        & " 	     , CASE WHEN NG.NRS_BR_CD IS NOT NULL  				 " & vbNewLine _
                                        & " 	            THEN '21V'				 " & vbNewLine _
                                        & " 	            ELSE '41V'				 " & vbNewLine _
                                        & " 	        END                                                  AS DATA_ID  				 " & vbNewLine _
                                        & " 	     , ISNULL(NG.OUTKA_DENP_NO, '')                          AS UKETUKE_NO            --受付№				 " & vbNewLine _
                                        & " 	     , SUBSTRING(CL.OUTKO_DATE, 3, 4) 				 " & vbNewLine _
                                        & " 	     + 'S'				 " & vbNewLine _
                                        & " 	     + FORMAT(row_number() over(ORDER BY NG.OUTKA_CTL_NO				 " & vbNewLine _
                                        & " 	                                       , NG.OUTKA_CTL_NO_CHU  ), '0000000')				 " & vbNewLine _
                                        & " 	                                                             AS BUTURYU_SERI_NO       --物流整理№				 " & vbNewLine _
                                        & " 	     , '41'                                                  AS SAGYO_N_KBN           --作業内容区分(別紙4:)				 " & vbNewLine _
                                        & " 	     , CL.OUTKO_DATE                                         AS SAGYOU_DATE           --作業年月日(荷役作業日)				 " & vbNewLine _
                                        & " 	     , ISNULL(NG.ITEM_RYAKUGO, '')                           AS HINMEI_RYAKUGO        --品名略号				 " & vbNewLine _
                                        & " 	     , ISNULL(NG.UG, TM.KBN_NM1)                             AS UG_CD                 --UGコード				 " & vbNewLine _
                                        & " 	     , ISNULL(NG.KOBETSU_NISUGATA_CD, '')                    AS NISUGATA              --荷姿				 " & vbNewLine _
                                        & " 	     , TRY_PARSE(NG.YOURYOU AS numeric(5,2))                 AS YOURYO                --容量				 " & vbNewLine _
                                        & " 	     , ISNULL(NG.YUSYUTSU_KBN 				 " & vbNewLine _
                                        & " 	            , CASE WHEN CL.DEST_KB = '01' -- 輸出(T018)				 " & vbNewLine _
                                        & " 	                   THEN '9'				 " & vbNewLine _
                                        & " 	                   ELSE '0'				 " & vbNewLine _
                                        & " 	              END)                                           AS HANBAI_BUNRUI         --販売分類				 " & vbNewLine _
                                        & " 	     , ISNULL(NG.HOKAN_BASYO, 'A0I4')                        AS HASEI_SP_CD           --発生SPコード				 " & vbNewLine _
                                        & " 	     , ISNULL(LEFT(NG.HOKAN_BASYO, 2), 'A0')                 AS SIHARAI_BASYO         --支払場所				 " & vbNewLine _
                                        & " 	     , ISNULL(NG.SUURYO, 0)                                  AS BUTURYU_RYO           --物流量				 " & vbNewLine _
                                        & " 	     , ISNULL(NG.JURYO_KANSAN_KEISU * NG.SUURYO, 0)          AS WIGHT_KG				 " & vbNewLine _
                                        & " 	     , CASE WHEN NG.DATA_ID_DETAIL = 'F' -- 転送				 " & vbNewLine _
                                        & " 	              OR NG.DATA_ID_DETAIL = 'G' -- 倉替				 " & vbNewLine _
                                        & " 	            THEN ''				 " & vbNewLine _
                                        & " 	            ELSE ISNULL(NU.JYUCHUSAKI_CD, '')				 " & vbNewLine _
                                        & " 	       END                                                   AS JITSU_USER            --実ユーザー(受注先コード)				 " & vbNewLine _
                                        & " 	     , CASE WHEN NG.DATA_ID_DETAIL = 'F' -- 転送				 " & vbNewLine _
                                        & " 	              OR NG.DATA_ID_DETAIL = 'G' -- 倉替				 " & vbNewLine _
                                        & " 	            THEN ''				 " & vbNewLine _
                                        & " 	            ELSE ISNULL(NU.SHIHARAININ_CD, '')				 " & vbNewLine _
                                        & " 	       END                                                   AS KEIYAKUSAKI_CD        --契約先コード(支払人コード)				 " & vbNewLine _
                                        & " 	     				 " & vbNewLine _
                                        & " 	     , ISNULL(NG.OUTKA_DENP_DTL_NO, '')                      AS ORDER_NO              -- オーダー№				 " & vbNewLine _
                                        & " 	     , ISNULL(NG.GRADE1, '')                                 AS GRADE1                --グレード1				 " & vbNewLine _
                                        & " 	     , ROW_NUMBER() OVER(PARTITION BY NG.OUTKA_CTL_NO 				 " & vbNewLine _
                                        & " 	                             ORDER BY NG.OUTKA_CTL_NO				 " & vbNewLine _
                                        & " 	                                    , NG.OUTKA_CTL_NO_CHU) AS RN				 " & vbNewLine _
                                        & " 	  from LM_TRN..C_OUTKA_L AS CL				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                      LEFT JOIN				 " & vbNewLine _
                                        & " 	                           LM_TRN..H_OUTKAEDI_DTL_NCGO_NEW AS NG				 " & vbNewLine _
                                        & " 	                        ON NG.NRS_BR_CD     = CL.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                       AND NG.OUTKA_CTL_NO  = CL.OUTKA_NO_L				 " & vbNewLine _
                                        & " 	                       AND LEFT(CL.OUTKO_DATE, 6) = @KEIJYO  				 " & vbNewLine _
                                        & " 	                       AND NG.SYS_DEL_FLG   = '0'				 " & vbNewLine _
                                        & " 	                       AND NG.DEL_KB        = '0'				 " & vbNewLine _
                                        & " 	                LEFT JOIN				 " & vbNewLine _
                                        & " 	                     LM_MST..Z_KBN AS TM				 " & vbNewLine _
                                        & " 	                  ON TM.KBN_GROUP_CD = 'M026'				 " & vbNewLine _
                                        & " 	                 AND TM.SYS_DEL_FLG  = '0'				 " & vbNewLine _
                                        & " 	                 AND NG.NRS_BR_CD IS NULL				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	  LEFT JOIN 				 " & vbNewLine _
                                        & " 	       LM_TRN..H_UNSOEDI_DTL_NCGO AS NU				 " & vbNewLine _
                                        & " 	    ON NU.OUTKA_CTL_NO = NG.OUTKA_CTL_NO				 " & vbNewLine _
                                        & " 	   AND NU.OUTKA_CTL_NO_CHU = NG.OUTKA_CTL_NO_CHU 				 " & vbNewLine _
                                        & " 	   AND NU.DEL_KB = '0'				 " & vbNewLine _
                                        & " 	   AND NU.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	 where                        				 " & vbNewLine _
                                        & " 	                           CL.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	                       AND LEFT(CL.OUTKO_DATE, 6) = @KEIJYO                      				 " & vbNewLine _
                                        & " 	                       AND CL.CUST_CD_L = '32516'				 " & vbNewLine _
                                        & " --DEL 2022/08/03 031530          AND CL.CUST_CD_M = '00'   				 " & vbNewLine _
                                        & " 	                       AND CL.OUTKA_KB  = '10'        -- 通常出荷    				 " & vbNewLine _
                                        & " 	                       AND CL.OUTKA_STATE_KB >= '60'  -- 出荷済				 " & vbNewLine _
                                        & " 	                       AND CL.NIYAKU_YN = '01'        -- 荷役有				 " & vbNewLine _
                                        & " 	           				 " & vbNewLine _
                                        & " 	) AS NG				 " & vbNewLine _
                                        & " 	LEFT JOIN				 " & vbNewLine _
                                        & " 	     (				 " & vbNewLine _
                                        & " 	                            SELECT NG.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                                       , TTL.INOUTKA_NO_L				 " & vbNewLine _
                                        & " 	                                       , TTL.IN_OUT_TYPE				 " & vbNewLine _
                                        & " 	                                       , TTL2.IS_TYPE_M				 " & vbNewLine _
                                        & " 	                                       , TTL.INV_DATE_YM				 " & vbNewLine _
                                        & " 	                                       , TTL.TAX_RATE				 " & vbNewLine _
                                        & " 	                                       , TTL2.TOTAL_WT				 " & vbNewLine _
                                        & " 	                                       , TTL.HANDLING_AMO_TTL				 " & vbNewLine _
                                        & " 	                                       , TTL.HANDLING_AMO_TTL - SUM(FLOOR(NG.WIGHT_KG / TTL2.TOTAL_WT * TTL.HANDLING_AMO_TTL)) AS DIFF				 " & vbNewLine _
                                        & " 	                                       , TTL.HANDLING_AMO_TTL_TAX - (SUM(FLOOR(NG.WIGHT_KG / TTL2.TOTAL_WT * TTL.HANDLING_AMO_TTL) * TTL.TAX_RATE)) AS TAX_DIFF             				 " & vbNewLine _
                                        & " 	             				 " & vbNewLine _
                                        & " 	                                    FROM (				 " & vbNewLine _
                                        & " 	                          select 				 " & vbNewLine _
                                        & " 	                                 BL.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                               , ISNULL(NG.INKA_CTL_NO_L, BL.INKA_NO_L)               AS INOUTKA_NO_L				 " & vbNewLine _
                                        & " 	                               , ISNULL(NG.INKA_CTL_NO_M, '')                         AS INOUTKA_NO_M				 " & vbNewLine _
                                        & " 	                               , 'IN'                                                 AS IN_OUT_TYPE 				 " & vbNewLine _
                                        & " 	                               , CASE WHEN NG.NRS_BR_CD IS NULL				 " & vbNewLine _
                                        & " 	                                      THEN NULL				 " & vbNewLine _
                                        & " 	                                      ELSE ''				 " & vbNewLine _
                                        & " 	                                 END                                                  AS IS_TYPE_M				 " & vbNewLine _
                                        & " 	                               , NG.JURYO_KANSAN_KEISU * NG.SUURYO                    AS WIGHT_KG				 " & vbNewLine _
                                        & " 	    				 " & vbNewLine _
                                        & " 	                            from LM_TRN..B_INKA_L AS BL				 " & vbNewLine _
                                        & " 	                                           LEFT JOIN				 " & vbNewLine _
                                        & " 	                                                LM_TRN..H_INKAEDI_DTL_NCGO_NEW AS NG				 " & vbNewLine _
                                        & " 	                                             ON NG.NRS_BR_CD     = BL.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                                            AND NG.INKA_CTL_NO_L = BL.INKA_NO_L				 " & vbNewLine _
                                        & " 	                                            AND NG.SYS_DEL_FLG   = '0'				 " & vbNewLine _
                                        & " 	                                            AND NG.DEL_KB        = '0'				 " & vbNewLine _
                                        & " 	               				 " & vbNewLine _
                                        & " 	                          				 " & vbNewLine _
                                        & " 	                           where                        				 " & vbNewLine _
                                        & " 	                                                 BL.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	                                                 AND LEFT(BL.INKA_DATE, 6) = @KEIJYO                      				 " & vbNewLine _
                                        & " 	                                                 AND BL.CUST_CD_L = '32516'				 " & vbNewLine _
                                        & " --DEL 2022/08/03 031530                                 AND BL.CUST_CD_M = '00'   				 " & vbNewLine _
                                        & " 	                                                 AND BL.INKA_STATE_KB = '50'				 " & vbNewLine _
                                        & " 	                          union 				 " & vbNewLine _
                                        & " 	                          select 				 " & vbNewLine _
                                        & " 	                                 CL.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                               , ISNULL(NG.OUTKA_CTL_NO, CL.OUTKA_NO_L)                AS INOUTKA_NO_L				 " & vbNewLine _
                                        & " 	                               , ISNULL(NG.OUTKA_CTL_NO_CHU, '')                       AS INOUTKA_NO_M				 " & vbNewLine _
                                        & " 	                               , 'OUT'                                                 AS IN_OUT_TYPE 				 " & vbNewLine _
                                        & " 	                               , CASE WHEN NG.NRS_BR_CD IS NULL				 " & vbNewLine _
                                        & " 	                                      THEN NULL				 " & vbNewLine _
                                        & " 	                                      ELSE ''				 " & vbNewLine _
                                        & " 	                                 END                                                   AS IS_TYPE_M				 " & vbNewLine _
                                        & " 	                               , NG.JURYO_KANSAN_KEISU * NG.SUURYO                    AS WIGHT_KG				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                            from LM_TRN..C_OUTKA_L AS CL				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                                                LEFT JOIN				 " & vbNewLine _
                                        & " 	                                                     LM_TRN..H_OUTKAEDI_DTL_NCGO_NEW AS NG				 " & vbNewLine _
                                        & " 	                                                  ON NG.NRS_BR_CD     = CL.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                                                 AND NG.OUTKA_CTL_NO  = CL.OUTKA_NO_L				 " & vbNewLine _
                                        & " 	                                                 AND LEFT(CL.OUTKO_DATE, 6) = @KEIJYO  				 " & vbNewLine _
                                        & " 	                                                 AND NG.SYS_DEL_FLG   = '0'				 " & vbNewLine _
                                        & " 	                                                 AND NG.DEL_KB        = '0'				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                            LEFT JOIN 				 " & vbNewLine _
                                        & " 	                                 LM_TRN..H_UNSOEDI_DTL_NCGO AS NU				 " & vbNewLine _
                                        & " 	                              ON NU.OUTKA_DENP_NO = NG.OUTKA_DENP_NO				 " & vbNewLine _
                                        & " 	                             AND NU.OUTKA_DENP_DTL_NO = NG.OUTKA_DENP_DTL_NO 				 " & vbNewLine _
                                        & " 	                             AND NU.DEL_KB = '0'				 " & vbNewLine _
                                        & " 	                             AND NU.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	                           where                        				 " & vbNewLine _
                                        & " 	                                                 CL.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	                                                 AND LEFT(CL.OUTKO_DATE, 6) = @KEIJYO                      				 " & vbNewLine _
                                        & " 	                                                 AND CL.CUST_CD_L = '32516'				 " & vbNewLine _
                                        & " 	  --DEL 2022/08/03 031530                             AND CL.CUST_CD_M = '00'   				 " & vbNewLine _
                                        & " 	                                                 AND CL.OUTKA_KB  = '10'            				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                ) AS NG				 " & vbNewLine _
                                        & " 	          LEFT JOIN(				 " & vbNewLine _
                                        & " 	                     -- ここまでOK				 " & vbNewLine _
                                        & " 	                     SELECT 				 " & vbNewLine _
                                        & " 	                            SKM.NRS_BR_CD                                                       AS NRS_BR_CD				 " & vbNewLine _
                                        & " 	                          , SKM.INOUTKA_NO_L                                                    AS INOUTKA_NO_L				 " & vbNewLine _
                                        & " 	                          , CASE WHEN BL.NRS_BR_CD IS NOT NULL				 " & vbNewLine _
                                        & " 	                                 THEN 'IN'				 " & vbNewLine _
                                        & " 	                                 WHEN CL.NRS_BR_CD IS NOT NULL				 " & vbNewLine _
                                        & " 	                                 THEN 'OUT'				 " & vbNewLine _
                                        & " 	                            END                                                                 AS IN_OUT_TYPE				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                          , LEFT(SKM.INV_DATE_FROM, 6)                                          AS INV_DATE_YM				 " & vbNewLine _
                                        & " 	                          , CASE WHEN BL.NRS_BR_CD IS NOT NULL				 " & vbNewLine _
                                        & " 	                                 THEN SUM(SKM.HANDLING_IN_AMO_TTL)				 " & vbNewLine _
                                        & " 	                                 WHEN CL.NRS_BR_CD IS NOT NULL				 " & vbNewLine _
                                        & " 	                                 THEN SUM(SKM.HANDLING_OUT_AMO_TTL)				 " & vbNewLine _
                                        & " 	                            END                                                                 AS HANDLING_AMO_TTL				 " & vbNewLine _
                                        & " 	                          , CASE WHEN BL.NRS_BR_CD IS NOT NULL				 " & vbNewLine _
                                        & " 	                                 THEN SUM(SKM.HANDLING_IN_AMO_TTL)  * TAX_RATE 				 " & vbNewLine _
                                        & " 	                                 WHEN CL.NRS_BR_CD IS NOT NULL				 " & vbNewLine _
                                        & " 	                                 THEN SUM(SKM.HANDLING_OUT_AMO_TTL) * TAX_RATE 				 " & vbNewLine _
                                        & " 	                            END                                                                 AS HANDLING_AMO_TTL_TAX                 				 " & vbNewLine _
                                        & " 	                        				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                          , TAX_RATE                                                            AS TAX_RATE				 " & vbNewLine _
                                        & " 	                       FROM LM_TRN..G_SEKY_MEISAI AS SKM				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                      LEFT JOIN LM_TRN..B_INKA_L AS BL				 " & vbNewLine _
                                        & " 	                        ON BL.INKA_NO_L = SKM.INOUTKA_NO_L				 " & vbNewLine _
                                        & " 	                       AND BL.NRS_BR_CD  = SKM.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                       AND BL.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	                       --AND BL.INKA_DATE >= SKM.INV_DATE_FROM				 " & vbNewLine _
                                        & " 	                       --AND BL.INKA_DATE <= SKM.INV_DATE_TO				 " & vbNewLine _
                                        & " 	                       AND SKM.HANDLING_IN_AMO_TTL > 0				 " & vbNewLine _
                                        & " 	                       AND BL.CUST_CD_L = '32516'   				 " & vbNewLine _
                                        & " --DEL 2022/08/03 031530           AND BL.CUST_CD_M = '00'				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                      LEFT JOIN LM_TRN..C_OUTKA_L AS CL				 " & vbNewLine _
                                        & " 	                        ON CL.OUTKA_NO_L = SKM.INOUTKA_NO_L				 " & vbNewLine _
                                        & " 	                       AND CL.NRS_BR_CD  = SKM.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                       AND CL.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	                       --AND CL.OUTKO_DATE >= SKM.INV_DATE_FROM				 " & vbNewLine _
                                        & " 	                       --AND CL.OUTKO_DATE <= SKM.INV_DATE_TO				 " & vbNewLine _
                                        & " 	                       AND SKM.HANDLING_OUT_AMO_TTL > 0				 " & vbNewLine _
                                        & " 	                       AND CL.CUST_CD_L = '32516'				 " & vbNewLine _
                                        & " 	--DEL 2022/08/03 031530       AND CL.CUST_CD_M = '00'   				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                      LEFT JOIN LM_MST..Z_KBN AS Z_TAX				 " & vbNewLine _
                                        & " 	                        ON Z_TAX.SYS_DEL_FLG  = '0'				 " & vbNewLine _
                                        & " 	                       AND Z_TAX.KBN_GROUP_CD = 'Z001'				 " & vbNewLine _
                                        & " 	                       AND Z_TAX.KBN_CD       = SKM.TAX_KB				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                      LEFT JOIN 				 " & vbNewLine _
                                        & " 	                                (SELECT M_TAX.[START_DATE]				 " & vbNewLine _
                                        & " 	                                      , M_TAX.TAX_CD				 " & vbNewLine _
                                        & " 	                                      , M_TAX.TAX_RATE				 " & vbNewLine _
                                        & " 	                                   FROM LM_MST..M_TAX 				 " & vbNewLine _
                                        & " 	                                   JOIN (SELECT  				 " & vbNewLine _
                                        & " 	                                                MAX([START_DATE]) as [START_DATE]				 " & vbNewLine _
                                        & " 	                                              , TAX_CD 				 " & vbNewLine _
                                        & " 	                                           FROM LM_MST..M_TAX 				 " & vbNewLine _
                                        & " 	                                          WHERE SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	                                            AND [START_DATE] <= @S_DATE				 " & vbNewLine _
                                        & " 	                                            AND M_TAX.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	                                          GROUP BY TAX_CD				 " & vbNewLine _
                                        & " 	                                         ) AS NOW_TAX				 " & vbNewLine _
                                        & " 	                                     ON M_TAX.TAX_CD = NOW_TAX.TAX_CD				 " & vbNewLine _
                                        & " 	                                    AND M_TAX.[START_DATE] = NOW_TAX.[START_DATE]				 " & vbNewLine _
                                        & " 	                                    AND M_TAX.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	                                 ) AS TAX				 " & vbNewLine _
                                        & " 	                        ON TAX.TAX_CD = Z_TAX.KBN_NM3  -- KBN_NM3:売上(JRS), KBN_NM4:仕入(JPS)				 " & vbNewLine _
                                        & " 	                       AND ( TAX.START_DATE <= CL.OUTKO_DATE OR TAX.START_DATE <= BL.INKA_DATE) 				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                      WHERE SKM.INV_DATE_FROM = @S_DATE				 " & vbNewLine _
                                        & " 	                        AND SKM.SEKY_FLG = '00'				 " & vbNewLine _
                                        & " 	                        AND SKM.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	                    --   AND SKM.SYS_ENT_PGID <> 'IKOU '    				 " & vbNewLine _
                                        & " 	                    --   AND MG.NRS_BR_CD = '60'  -- これが必要				 " & vbNewLine _
                                        & " 	                        AND SKM.NRS_BR_CD = '60'				 " & vbNewLine _
                                        & " 	                        AND SKM.HANDLING_IN_AMO_TTL + SKM.HANDLING_OUT_AMO_TTL > 0				 " & vbNewLine _
                                        & " 	      				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                      GROUP BY				 " & vbNewLine _
                                        & " 	                            SKM.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                          , SKM.INOUTKA_NO_L				 " & vbNewLine _
                                        & " 	                          , SKM.INV_DATE_FROM				 " & vbNewLine _
                                        & " 	                          , TAX_RATE     				 " & vbNewLine _
                                        & " 	                          , BL.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                          , CL.NRS_BR_CD				 " & vbNewLine _
                                        & " 	  				 " & vbNewLine _
                                        & " 	         ) AS TTL				 " & vbNewLine _
                                        & " 	    ON TTL.INOUTKA_NO_L = NG.INOUTKA_NO_L				 " & vbNewLine _
                                        & " 	   AND TTL.NRS_BR_CD = NG.NRS_BR_CD				 " & vbNewLine _
                                        & " 	   AND TTL.IN_OUT_TYPE = NG.IN_OUT_TYPE				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	          LEFT JOIN(				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                     SELECT 				 " & vbNewLine _
                                        & " 	                            SKM.NRS_BR_CD                                                       AS NRS_BR_CD				 " & vbNewLine _
                                        & " 	                          , SKM.INOUTKA_NO_L                                                    AS INOUTKA_NO_L				 " & vbNewLine _
                                        & " 	                          , CASE WHEN BL.NRS_BR_CD IS NOT NULL				 " & vbNewLine _
                                        & " 	                                 THEN 'IN'				 " & vbNewLine _
                                        & " 	                                 WHEN CL.NRS_BR_CD IS NOT NULL				 " & vbNewLine _
                                        & " 	                                 THEN 'OUT'				 " & vbNewLine _
                                        & " 	                            END                                                                 AS IN_OUT_TYPE				 " & vbNewLine _
                                        & " 	                          , CASE WHEN EI.NRS_BR_CD  IS NULL AND EO.NRS_BR_CD IS NULL				 " & vbNewLine _
                                        & " 	                                 THEN NULL				 " & vbNewLine _
                                        & " 	                                 ELSE ''				 " & vbNewLine _
                                        & " 	                             END                                                                AS IS_TYPE_M				 " & vbNewLine _
                                        & " 	                          , LEFT(SKM.INV_DATE_FROM, 6)                                          AS INV_DATE_YM				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	               				 " & vbNewLine _
                                        & " 	                        				 " & vbNewLine _
                                        & " 	                          , CASE WHEN EI.NRS_BR_CD IS NOT NULL				 " & vbNewLine _
                                        & " 	                                 THEN SUM(EI.SUURYO * EI.JURYO_KANSAN_KEISU)				 " & vbNewLine _
                                        & " 	                                 WHEN EO.NRS_BR_CD IS NOT NULL				 " & vbNewLine _
                                        & " 	                                 THEN SUM(EO.SUURYO * EO.JURYO_KANSAN_KEISU)				 " & vbNewLine _
                                        & " 	                                 ELSE 0				 " & vbNewLine _
                                        & " 	                            END                                                                 AS TOTAL_WT				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                       FROM LM_TRN..G_SEKY_MEISAI AS SKM				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                      LEFT JOIN LM_TRN..B_INKA_L AS BL				 " & vbNewLine _
                                        & " 	                        ON BL.INKA_NO_L = SKM.INOUTKA_NO_L				 " & vbNewLine _
                                        & " 	                       AND BL.NRS_BR_CD  = SKM.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                       AND BL.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	                       --AND BL.INKA_DATE >= SKM.INV_DATE_FROM				 " & vbNewLine _
                                        & " 	                       --AND BL.INKA_DATE <= SKM.INV_DATE_TO				 " & vbNewLine _
                                        & " 	                       AND SKM.HANDLING_IN_AMO_TTL > 0				 " & vbNewLine _
                                        & " 	                       AND BL.CUST_CD_L = '32516'   				 " & vbNewLine _
                                        & " 	--DEL 2022/08/03 031530        AND BL.CUST_CD_M = '00'				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                      LEFT JOIN LM_TRN..H_INKAEDI_DTL_NCGO_NEW AS EI				 " & vbNewLine _
                                        & " 	                        ON EI.NRS_BR_CD = BL.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                       AND EI.INKA_CTL_NO_L = BL.INKA_NO_L				 " & vbNewLine _
                                        & " 	                       AND EI.SYS_DEL_FLG  = '0'				 " & vbNewLine _
                                        & " 	                       AND EI.DEL_KB       = 0				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                      LEFT JOIN LM_TRN..C_OUTKA_L AS CL				 " & vbNewLine _
                                        & " 	                        ON CL.OUTKA_NO_L = SKM.INOUTKA_NO_L				 " & vbNewLine _
                                        & " 	                       AND CL.NRS_BR_CD  = SKM.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                       AND CL.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	                       --AND CL.OUTKO_DATE >= SKM.INV_DATE_FROM				 " & vbNewLine _
                                        & " 	                       --AND CL.OUTKO_DATE <= SKM.INV_DATE_TO				 " & vbNewLine _
                                        & " 	                       AND SKM.HANDLING_OUT_AMO_TTL > 0				 " & vbNewLine _
                                        & " 	                       AND CL.CUST_CD_L = '32516'				 " & vbNewLine _
                                        & " 	--DEL 2022/08/03 031530        AND CL.CUST_CD_M = '00'   				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                      LEFT JOIN LM_TRN..H_OUTKAEDI_DTL_NCGO_NEW AS EO				 " & vbNewLine _
                                        & " 	                        ON EO.NRS_BR_CD = CL.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                       AND EO.OUTKA_CTL_NO = CL.OUTKA_NO_L				 " & vbNewLine _
                                        & " 	                       AND EO.SYS_DEL_FLG  = '0'				 " & vbNewLine _
                                        & " 	                       AND EO.DEL_KB       = 0				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                      WHERE SKM.SEKY_FLG = '00'				 " & vbNewLine _
                                        & " 	                        AND SKM.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	                    --   AND SKM.SYS_ENT_PGID <> 'IKOU '    				 " & vbNewLine _
                                        & " 	                    --   AND MG.NRS_BR_CD = '60'  -- これが必要				 " & vbNewLine _
                                        & " 	                        AND SKM.NRS_BR_CD = '60'				 " & vbNewLine _
                                        & " 	                        AND(    SKM.HANDLING_IN_AMO_TTL > 0 				 " & vbNewLine _
                                        & " 	                             OR SKM.HANDLING_OUT_AMO_TTL > 0				 " & vbNewLine _
                                        & " 	                           )				 " & vbNewLine _
                                        & " 	                        AND SKM.INV_DATE_FROM = @S_DATE				 " & vbNewLine _
                                        & " 	                        AND SKM.INV_DATE_TO   = @E_DATE				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	                      GROUP BY				 " & vbNewLine _
                                        & " 	                            SKM.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                          , SKM.INOUTKA_NO_L				 " & vbNewLine _
                                        & " 	                          , SKM.INV_DATE_FROM				 " & vbNewLine _
                                        & " 	                          , BL.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                          , CL.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                          , EI.NRS_BR_CD				 " & vbNewLine _
                                        & " 	                          , EO.NRS_BR_CD				 " & vbNewLine _
                                        & " 	         ) AS TTL2				 " & vbNewLine _
                                        & " 	    ON TTL2.INOUTKA_NO_L = NG.INOUTKA_NO_L				 " & vbNewLine _
                                        & " 	   AND TTL2.NRS_BR_CD = NG.NRS_BR_CD				 " & vbNewLine _
                                        & " 	   AND TTL2.IN_OUT_TYPE = NG.IN_OUT_TYPE				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	  WHERE TTL.NRS_BR_CD IS NOT NULL				 " & vbNewLine _
                                        & " 	      GROUP BY NG.NRS_BR_CD				 " & vbNewLine _
                                        & " 	           , TTL.INOUTKA_NO_L				 " & vbNewLine _
                                        & " 	           , TTL.IN_OUT_TYPE				 " & vbNewLine _
                                        & " 	           , TTL2.IS_TYPE_M				 " & vbNewLine _
                                        & " 	           , TTL.INV_DATE_YM				 " & vbNewLine _
                                        & " 	           , TTL.HANDLING_AMO_TTL				 " & vbNewLine _
                                        & " 	           , TTL.HANDLING_AMO_TTL_TAX				 " & vbNewLine _
                                        & " 	           , TTL2.TOTAL_WT				 " & vbNewLine _
                                        & " 	           , TTL.TAX_RATE				 " & vbNewLine _
                                        & " 	    				 " & vbNewLine _
                                        & " 	     ) AS SKM				 " & vbNewLine _
                                        & " 	 ON SKM.INOUTKA_NO_L = NG.INOUTKA_NO_L				 " & vbNewLine _
                                        & " 	AND SKM.NRS_BR_CD    = NG.NRS_BR_CD				 " & vbNewLine _
                                        & " 	AND SKM.IN_OUT_TYPE  = NG.IN_OUT_TYPE       				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	) AS SDFD				 " & vbNewLine _
                                        & " 	WHERE SDFD.NET_AMT IS NOT NULL				 " & vbNewLine



    Private Const SQL_BUTSURYUHI_CHK5 As String = " 	SELECT				 " & vbNewLine _
                                        & " 	 '5'    as SQ_NO				 " & vbNewLine _
                                        & " 	 , isnull(SUM(NET_AMT),0)   as NET_AMT                              --NET金額				 " & vbNewLine _
                                        & " 	FROM (				 " & vbNewLine

    Private Const SQL_BUTSURYUHI_DATA5 As String = " 	SELECT				  " & vbNewLine _
                                        & " 	   CONVERT(char(4),ISNULL(RECORD_ID, ''))                          --レコードID				  " & vbNewLine _
                                        & " 	 + CONVERT(char(3),ISNULL(DATA_ID, ''))                            --データID         データIDの3桁目：V     (日陸様を表すコードです)				  " & vbNewLine _
                                        & " 	 + CONVERT(char(3),ISNULL(COMP_CD,''))                             --会社コード 040   (日本合成化学工業を表すコードです)				  " & vbNewLine _
                                        & " 	 + CONVERT(char(6),ISNULL(KEIJYO_YM,''))                           --計上年月				  " & vbNewLine _
                                        & " 	 + CONVERT(char(12),ISNULL(UKETUKE_NO,''))                         --受付№				  " & vbNewLine _
                                        & " 	 + CONVERT(char(12),ISNULL(BUTURYU_SERI_NO,''))                    --物流整理№				  " & vbNewLine _
                                        & " 	 + CONVERT(char(3),ISNULL(TEISEI_NO,''))                           --訂正No.				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(TEISEI_KBN,''))                          --訂正区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(KINOU,''))                               --機能				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(KINOU_SAIMOKU,''))                       --機能細目				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(SAGYO_N_KBN,''))                     --作業内容区分  	                       			  " & vbNewLine _
                                        & " 	 + CONVERT(char(8),ISNULL(SYORI_YMD,''))                           --伝送処理年月日				  " & vbNewLine _
                                        & " 	 + CONVERT(char(9),ISNULL(SYORI_JIKAN,''))                         --伝送処理時間				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(NYURYOKU_BASYO,''))                      --入力場所				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(NYURYOKU_BUMON,''))                      --入力部門				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(NYURYOKU_GROUP,''))                      --入力グループ				  " & vbNewLine _
                                        & " 	 + CONVERT(char(8),ISNULL(SAGYOU_DATE,''))                         --作業年月日				  " & vbNewLine _
                                        & " 	 + CONVERT(char(10),ISNULL(HINMEI_RYAKUGO,''))                     --品目略号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),ISNULL(UG_CD,''))                               --UGコード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(NISUGATA,''))                            --荷姿				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(YOURYO, 0), '00000.00')                           --容量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(KEIYAKU_BASYO,''))                       --契約場所 'AA'固定				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(BIN_KBN,''))                             --便区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),ISNULL(BUTSURYU_COMP_CD,''))                    --物流会社コード H441B (日陸様を表すコードです)				  " & vbNewLine _
                                        & " 	 + CONVERT(char(10),ISNULL(TSUMEAWASE_NO,''))                      --詰合わせNo.				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),ISNULL(JITSU_USER,''))                          --実ユーザー				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),ISNULL(KEIYAKUSAKI_CD,''))                      --契約先コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(KOTEIHI_HEND_KBN,''))                    --固定費・変動費区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HANBAI_BUNRUI,''))                       --販売分類				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HACCHI_KBN,''))                          --発地情報 発地区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),ISNULL(HACCHI_CD,''))                           --     発地コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(6),ISNULL(HACCHI_CIKU_CD,''))                      --     市区町村コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(CHAKUCHI_KBN,''))                        --着地情報 着地区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),ISNULL(CHAKUCHI_CD,''))                         --     着地コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(6),ISNULL(CHAKUCHI_CHIKU_CD,''))                   --     市区町村コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(20),ISNULL(CHAKUCHI_NOUNYU_BASYO,''))              --     納入場所名				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HASSEI_BASYO_KBN,''))                    --発生場所区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),ISNULL(HASEI_SP_CD,''))                         --発生SPコード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(SIHARAI_BASYO,''))                       --支払場所				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(BUTURYU_TANI,''))                        --物流単位				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(BUTURYU_FUGO,'+'))                       --物流量符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(BUTURYU_RYO,0), '00000000000.0000')               --物流量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(10),ISNULL(LOT_NO,''))                             --ロット№				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(TAX_BN,''))                              --税区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(GN_KBN,''))                              --GN区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(NET_AMT_FUGO,'+'))                       --NET金額符号				  " & vbNewLine _
                                        & " 	 + FORMAT(NET_AMT ,'000000000000000')                              --NET金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(GROSS_AMT_FUGO,'+'))                     --GROSS金額符号				  " & vbNewLine _
                                        & " 	 + FORMAT(NET_AMT + TAX ,'000000000000000')                        --GROSS金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),TAX_FUGO)                                       --税額符号				  " & vbNewLine _
                                        & " 	 + FORMAT(TAX, '000000000000000')                                  --税額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(ZEIRITU_FUGO,'+'))                       --税率符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(ZEIRITSU * 100, 0), '000.00')                     --税率				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(YUSO_KYORI_FUGO,''))                     --輸送距離符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(YUSOKYORI,''), '00000')                           --輸送距離				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(SAIMOKU1_CD,''))                         --項目細分１ 項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(SAIMOKU1_BETSU_AMT_FUGO,'+'))            --      項目別金額符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(SAIMOKU1_BETU_AMT,''), '00000')                   --      項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(SAIMOKU2_CD,''))                         --項目細分２ 項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(SAIMOKU2_BETSU_AMT_FUGO,'+'))            --      項目別金額符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(SAIMOKU2_BETU_AMT,''), '00000')                   --      項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(SAIMOKU3_CD,''))                         --項目細分３ 項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(SAIMOKU3_BETSU_AMT_FUGO,'+'))            --       項目別金額符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(SAIMOKU3_BETU_AMT,''), '00000')                   --      項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(SAIMOKU4_CD,''))                         --項目細分４ 項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(SAIMOKU4_BETSU_AMT_FUGO,'+'))            --      項目別金額符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(SAIMOKU4_BETU_AMT,''), '00000')                   --      項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(SAIMOKU5_CD,''))                         --項目細分５ 項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(SAIMOKU5_BETSU_AMT_FUGO,'+'))            --      項目別金額符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(SAIMOKU5_BETU_AMT,''), '00000')                   --      項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(SAIMOKU6_CD,''))                         --項目細分６ 項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(SAIMOKU6__BETSU_AMT_FUGO,'+'))           --      項目別金額符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(SAIMOKU6_BETU_AMT,''), '00000')                   --      項目別金額				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(ERR_CD,''), '0000')                               --エラーコード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(8),ISNULL(NOUNYU_DATE,''))                         --納入日				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),ISNULL(SYABAN,''))                              --車番				  " & vbNewLine _
                                        & " 	 + CONVERT(char(6),ISNULL(YOBI,''))                                --FILLER				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN1_TAISYO_KISU,''))                    --保管対象期数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN1_KIMATU_ZAIKOSU_FUGO,''))            --保管対象１ 期末在庫数符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(HKN1_KIMATU_ZAIKOSU,0), '00000000000.0000')       --           期末在庫数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN1_NYUKO_SURYO_FUGO,''))               --      入庫数量符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(HKN1_NYUKO_SURYO,0), '00000000000.0000')          --      入庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN1_SYUKO_SURYO_FUGO,''))               --      出庫数量符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(HKN1_SYUKO_SURYO,0), '00000000000.0000')          --      出庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN2_KIMATU_ZAIKOSU_FUGO,''))            --保管対象２ 期末在庫数符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(HKN2_KIMATU_ZAIKOSU,0), '00000000000.0000')       --           期末在庫数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN2_NYUKO_SURYO_FUGO,''))               --      入庫数量符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(HKN2_NYUKO_SURYO,0), '00000000000.0000')          --      入庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN2_SYUKO_SURYO_FUGO,''))               --      出庫数量符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(HKN2_SYUKO_SURYO,0), '00000000000.0000')          --      出庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN3_KIMATU_ZAIKOSU_FUGO,''))            --保管対象３ 期末在庫数符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(HKN3_KIMATU_ZAIKOSU,0), '00000000000.0000')       --           期末在庫数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN3_NYUKO_SURYO_FUGO,''))               --      入庫数量符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(HKN3_NYUKO_SURYO,0), '00000000000.0000')          --      入庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN3_SYUKO_SURYO_FUGO,''))               --      出庫数量符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(HKN3_SYUKO_SURYO,0), '00000000000.0000')          --      出庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),ISNULL(YOBI1,''))                               --FILLER				  " & vbNewLine _
                                        & " 	 + CONVERT(char(10),ISNULL(ORDER_NO,''))                           --オーダー№				  " & vbNewLine _
                                        & " 	 + CONVERT(char(8),ISNULL(SYORI_DATE,''))                          --処理日付				  " & vbNewLine _
                                        & " 	 + CONVERT(char(9),ISNULL(SYORI_TIME,''))                          --処理時間				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(YOBI2,''))                               --FILLER				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(SITABARAI_KBN,''))                       --下払区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),ISNULL(SITABARAI_CD,''))                        --下払先コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(10),ISNULL(GRADE1,''))                             --グレード1				  " & vbNewLine _
                                        & " 	 + CONVERT(char(3),ISNULL(YOBI3,''))                               --FILLER				  " & vbNewLine _
                                        & "        AS  NET_AMT				  " & vbNewLine _
                                        & " 	FROM (				  " & vbNewLine


    Private Const SQL_BUTSURYUHI_SQL5 As String = " 	SELECT 				 " & vbNewLine _
                                        & " 	      'VK74'                                            AS RECORD_ID          --レコードID				 " & vbNewLine _
                                        & " 	     , CASE WHEN DTL.UG IS NOT NULL  				 " & vbNewLine _
                                        & " 	            THEN '31V'				 " & vbNewLine _
                                        & " 	            ELSE '41V'				 " & vbNewLine _
                                        & " 	        END                                             AS DATA_ID            --データID         データIDの3桁目：V     (日陸様を表すコードです)				 " & vbNewLine _
                                        & " 	     , '040'                                            AS COMP_CD            --会社コード 040   (日本合成化学工業を表すコードです)				 " & vbNewLine _
                                        & " 	     , LEFT(SK.INV_DATE_FROM, 6)                        AS KEIJYO_YM          --計上年月				 " & vbNewLine _
                                        & " 	     , ''                                               AS UKETUKE_NO         --受付№(設定不要)				 " & vbNewLine _
                                        & " 	     , SUBSTRING(SK.INV_DATE_FROM, 3, 4) 				 " & vbNewLine _
                                        & " 	     + 'H'				 " & vbNewLine _
                                        & " 	     + FORMAT(row_number() over(ORDER BY MG.GOODS_CD_CUST, LOT_NO ), '0000000')    				 " & vbNewLine _
                                        & " 	                                                        AS BUTURYU_SERI_NO       --物流整理№				 " & vbNewLine _
                                        & " 	     , '000'                                            AS TEISEI_NO             --訂正No.(別紙3:000, 001)				 " & vbNewLine _
                                        & " 	     , '1'                                              AS TEISEI_KBN            --訂正区分(別紙3:1,21,2,31)				 " & vbNewLine _
                                        & " 	     , ''                                               AS KINOU                 --機能(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS KINOU_SAIMOKU         --機能細目(設定不要)				 " & vbNewLine _
                                        & " 	     , '61'                                             AS SAGYO_N_KBN           --作業内容区分(別紙4:)				 " & vbNewLine _
                                        & " 	     , CONVERT(VARCHAR,GETDATE(),112)                   AS SYORI_YMD             --伝送処理年月日				 " & vbNewLine _
                                        & " 	     , FORMAT(GETDATE(),'HHmmss')                       AS SYORI_JIKAN           --伝送処理時間				 " & vbNewLine _
                                        & " 	     , ''                                               AS NYURYOKU_BASYO        --入力場所(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS NYURYOKU_BUMON        --入力部門(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS NYURYOKU_GROUP        --入力グループ(設定不要)				 " & vbNewLine _
                                        & " 	     , SK.INV_DATE_TO                                   AS SAGYOU_DATE           --作業年月日(月末日)				 " & vbNewLine _
                                        & " 	     , MG.GOODS_CD_CUST                                 AS HINMEI_RYAKUGO        --品名略号				 " & vbNewLine _
                                        & " 	     , ISNULL(DTL.UG, MUG.KBN_NM1)                      AS UG_CD                 --UG(運賃グループ)コード				 " & vbNewLine _
                                        & " 	     , ISNULL(DTL.NISUGATA_CD, 'ZZ')                    AS NISUGATA              --荷姿				 " & vbNewLine _
                                        & " 	     , SK.IRIME                                         AS YOURYO                --容量				 " & vbNewLine _
                                        & " 	     , 'AA'                                             AS KEIYAKU_BASYO         --契約場所 'AA'固定				 " & vbNewLine _
                                        & " 	     , ''                                               AS BIN_KBN               --便区分(設定不要)				 " & vbNewLine _
                                        & " 	     , 'H441B'                                          AS BUTSURYU_COMP_CD      --物流会社コード H441B (日陸様を表すコードです)				 " & vbNewLine _
                                        & " 	     , ''                                               AS TSUMEAWASE_NO         --積合わせNo.(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS JITSU_USER            --実ユーザー(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS KEIYAKUSAKI_CD        --契約先コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '2'                                              AS KOTEIHI_HEND_KBN      --固定費・変動費区分				 " & vbNewLine _
                                        & " 	     , ''                                               AS HANBAI_BUNRUI         --販売分類(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS HACCHI_KBN            --発地情報 発地区分(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS HACCHI_CD             --         発地コード(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS HACCHI_CIKU_CD        --         市区町村コード(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS CHAKUCHI_KBN          --着地情報 着地区分(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS CHAKUCHI_CD           --         着地コード(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS CHAKUCHI_CHIKU_CD     --         市区町村コード(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS CHAKUCHI_NOUNYU_BASYO --         納入場所名(設定不要)				 " & vbNewLine _
                                        & " 	     , '2'                                              AS HASSEI_BASYO_KBN      --発生場所区分				 " & vbNewLine _
                                        & " 	     , ISNULL(DTL.HOKAN_BASYO, 'A0I4')                  AS HASEI_SP_CD           --発生SPコード				 " & vbNewLine _
                                        & " 	     , ISNULL(LEFT(DTL.HOKAN_BASYO, 2), 'A0')           AS SIHARAI_BASYO         --支払場所				 " & vbNewLine _
                                        & " 	     , '2'                                              AS BUTURYU_TANI          --物流単位				 " & vbNewLine _
                                        & " 	     , '+'                                              AS BUTURYU_FUGO          --物流量符号				 " & vbNewLine _
                                        & " 	     , CASE WHEN MUG.KBN_CD = '01'				 " & vbNewLine _
                                        & " 	            THEN CEILING((SUM(SK.INKO_NB1)				 " & vbNewLine _
                                        & " 	                        + SUM(SK.INKO_NB2)				 " & vbNewLine _
                                        & " 	                        + SUM(SK.INKO_NB3)				 " & vbNewLine _
                                        & " 	                        + SUM(SK.OUTKO_NB1)				 " & vbNewLine _
                                        & " 	                        + SUM(SK.OUTKO_NB2)				 " & vbNewLine _
                                        & " 	                        + SUM(SK.OUTKO_NB3)) / 2)				 " & vbNewLine _
                                        & " 	            WHEN MUG.KBN_CD = '02'				 " & vbNewLine _
                                        & " 	            THEN FLOOR((SUM(SK.INKO_NB1)				 " & vbNewLine _
                                        & " 	                      + SUM(SK.INKO_NB2)				 " & vbNewLine _
                                        & " 	                      + SUM(SK.INKO_NB3)				 " & vbNewLine _
                                        & " 	                      + SUM(SK.OUTKO_NB1)				 " & vbNewLine _
                                        & " 	                      + SUM(SK.OUTKO_NB2)				 " & vbNewLine _
                                        & " 	                      + SUM(SK.OUTKO_NB3)) / 2)				 " & vbNewLine _
                                        & " 	            ELSE (SUM(SK.INKO_NB1)				 " & vbNewLine _
                                        & " 	                + SUM(SK.INKO_NB2)				 " & vbNewLine _
                                        & " 	                + SUM(SK.INKO_NB3)				 " & vbNewLine _
                                        & " 	                + SUM(SK.OUTKO_NB1)				 " & vbNewLine _
                                        & " 	                + SUM(SK.OUTKO_NB2)				 " & vbNewLine _
                                        & " 	                + SUM(SK.OUTKO_NB3))				 " & vbNewLine _
                                        & " 	      END * SK.IRIME                                    AS BUTURYU_RYO            --物流量				 " & vbNewLine _
                                        & " 	     , ''                                               AS LOT_NO                 --ロット№(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN TAX.TAX_RATE <> 0 THEN '1' 				 " & vbNewLine _
                                        & " 	            ELSE '2'               				 " & vbNewLine _
                                        & " 	       END                                              AS TAX_BN                 --税区分				 " & vbNewLine _
                                        & " 	     , 'N'                                              AS GN_KBN                 --GN区分(G or N)				 " & vbNewLine _
                                        & " 	     ,  CASE WHEN SUM(SK.SEKI_ARI_NB1 * STORAGE1) 				 " & vbNewLine _
                                        & " 	                + SUM(SK.SEKI_ARI_NB2 * STORAGE2) 				 " & vbNewLine _
                                        & " 	                + SUM(SK.SEKI_ARI_NB3 * STORAGE3) >= 0				 " & vbNewLine _
                                        & " 	             THEN '+'				 " & vbNewLine _
                                        & " 	             ELSE '-'				 " & vbNewLine _
                                        & " 	       END                                              AS NET_AMT_FUGO           --NET金額符号				 " & vbNewLine _
                                        & " 	     , CASE WHEN MUG.KBN_CD = '01'				 " & vbNewLine _
                                        & " 	            THEN CEILING((SUM(SK.SEKI_ARI_NB1 * STORAGE1) 				 " & vbNewLine _
                                        & " 	                        + SUM(SK.SEKI_ARI_NB2 * STORAGE2) 				 " & vbNewLine _
                                        & " 	                        + SUM(SK.SEKI_ARI_NB3 * STORAGE3)) / 2)				 " & vbNewLine _
                                        & " 	            WHEN MUG.KBN_CD = '02'				 " & vbNewLine _
                                        & " 	            THEN FLOOR((SUM(SK.SEKI_ARI_NB1 * STORAGE1) 				 " & vbNewLine _
                                        & " 	                      + SUM(SK.SEKI_ARI_NB2 * STORAGE2) 				 " & vbNewLine _
                                        & " 	                      + SUM(SK.SEKI_ARI_NB3 * STORAGE3)) / 2)				 " & vbNewLine _
                                        & " 	            ELSE SUM(SK.SEKI_ARI_NB1 * STORAGE1) 				 " & vbNewLine _
                                        & " 	               + SUM(SK.SEKI_ARI_NB2 * STORAGE2) 				 " & vbNewLine _
                                        & " 	               + SUM(SK.SEKI_ARI_NB3 * STORAGE3)				 " & vbNewLine _
                                        & " 	       END                                               AS NET_AMT               --NET金額				 " & vbNewLine _
                                        & " 	     , CASE WHEN SUM(SK.SEKI_ARI_NB1 * STORAGE1) 				 " & vbNewLine _
                                        & " 	               + SUM(SK.SEKI_ARI_NB2 * STORAGE2) 				 " & vbNewLine _
                                        & " 	               + SUM(SK.SEKI_ARI_NB3 * STORAGE3) >= 0				 " & vbNewLine _
                                        & " 	             THEN '+'				 " & vbNewLine _
                                        & " 	             ELSE '-'				 " & vbNewLine _
                                        & " 	       END                                               AS GROSS_AMT_FUGO        --GROSS金額符号				 " & vbNewLine _
                                        & " 	     , 0                                                 AS GROSS_AMT             --GROSS金額				 " & vbNewLine _
                                        & " 	     , CASE WHEN TAX.TAX_RATE >= 0				 " & vbNewLine _
                                        & " 	             THEN '+'				 " & vbNewLine _
                                        & " 	             ELSE '-'				 " & vbNewLine _
                                        & " 	       END                                               AS TAX_FUGO              --税額符号				 " & vbNewLine _
                                        & " 	     , ROUND(CASE WHEN MUG.KBN_CD = '01'				 " & vbNewLine _
                                        & " 	             THEN CEILING((SUM(SK.SEKI_ARI_NB1 * STORAGE1) 				 " & vbNewLine _
                                        & " 	                         + SUM(SK.SEKI_ARI_NB2 * STORAGE2) 				 " & vbNewLine _
                                        & " 	                         + SUM(SK.SEKI_ARI_NB3 * STORAGE3)) / 2)				 " & vbNewLine _
                                        & " 	             WHEN MUG.KBN_CD = '02'				 " & vbNewLine _
                                        & " 	             THEN FLOOR((SUM(SK.SEKI_ARI_NB1 * STORAGE1) 				 " & vbNewLine _
                                        & " 	                       + SUM(SK.SEKI_ARI_NB2 * STORAGE2) 				 " & vbNewLine _
                                        & " 	                       + SUM(SK.SEKI_ARI_NB3 * STORAGE3)) / 2)				 " & vbNewLine _
                                        & " 	             ELSE SUM(SK.SEKI_ARI_NB1 * STORAGE1) 				 " & vbNewLine _
                                        & " 	                + SUM(SK.SEKI_ARI_NB2 * STORAGE2) 				 " & vbNewLine _
                                        & " 	                + SUM(SK.SEKI_ARI_NB3 * STORAGE3)				 " & vbNewLine _
                                        & " 	       END * TAX.TAX_RATE, 0)				 " & vbNewLine _
                                        & " 	                                                        AS TAX                        --税額				 " & vbNewLine _
                                        & " 	     , '+'                                              AS ZEIRITU_FUGO               --税率符号				 " & vbNewLine _
                                        & " 	     , TAX.TAX_RATE                                     AS ZEIRITSU                   --税率				 " & vbNewLine _
                                        & " 	     , '+'                                              AS YUSO_KYORI_FUGO            --輸送距離符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS YUSOKYORI                  --輸送距離(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SAIMOKU1_CD                --項目細分１ 項目コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS SAIMOKU1_BETSU_AMT_FUGO    --      項目別金額符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS SAIMOKU1_BETU_AMT          --       項目別金額(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SAIMOKU2_CD                --項目細分２ 項目コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS SAIMOKU2_BETSU_AMT_FUGO    --      項目別金額符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS SAIMOKU2_BETU_AMT          --      項目別金額(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SAIMOKU3_CD                --項目細分３ 項目コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS SAIMOKU3_BETSU_AMT_FUGO    --      項目別金額符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS SAIMOKU3_BETU_AMT          --        項目別金額(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SAIMOKU4_CD                --項目細分４ 項目コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS SAIMOKU4_BETSU_AMT_FUGO    --        項目別金額符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS SAIMOKU4_BETU_AMT          --        項目別金額(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SAIMOKU5_CD                --項目細分５ 項目コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS SAIMOKU5_BETSU_AMT_FUGO    --        項目別金額符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS SAIMOKU5_BETU_AMT          --        項目別金額(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SAIMOKU6_CD                --項目細分６ 項目コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS SAIMOKU6__BETSU_AMT_FUGO   --        項目別金額符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS SAIMOKU6_BETU_AMT          --        項目別金額(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS ERR_CD                     --エラーコード(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS NOUNYU_DATE                --納入日(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SYABAN                     --車番(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS YOBI                       --FILLER(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS HKN1_TAISYO_KISU           --保管対象期数(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN SUM(SK.KISYZAN_NB1 				 " & vbNewLine _
                                        & " 	                   + SK.INKO_NB1 				 " & vbNewLine _
                                        & " 	                   - SK.OUTKO_NB1) >= 0				 " & vbNewLine _
                                        & " 	            THEN '+'				 " & vbNewLine _
                                        & " 	            ELSE '-'				 " & vbNewLine _
                                        & " 	       END                                              AS HKN1_KIMATU_ZAIKOSU_FUGO   --保管対象１ 期末在庫数符号(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN MUG.KBN_CD = '01'				 " & vbNewLine _
                                        & " 	            THEN CEILING(SUM(SK.KISYZAN_NB1 + SK.INKO_NB1 - SK.OUTKO_NB1) / 2)				 " & vbNewLine _
                                        & " 	            WHEN MUG.KBN_CD = '02'				 " & vbNewLine _
                                        & " 	            THEN FLOOR(SUM(SK.KISYZAN_NB1 + SK.INKO_NB1 - SK.OUTKO_NB1) / 2)				 " & vbNewLine _
                                        & " 	            ELSE SUM(SK.KISYZAN_NB1 + SK.INKO_NB1 - SK.OUTKO_NB1)				 " & vbNewLine _
                                        & " 	        END                                             AS HKN1_KIMATU_ZAIKOSU        --           期末在庫数(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN SUM(SK.INKO_NB1) >= 0				 " & vbNewLine _
                                        & " 	            THEN '+'				 " & vbNewLine _
                                        & " 	            ELSE '-'				 " & vbNewLine _
                                        & " 	       END                                              AS HKN1_NYUKO_SURYO_FUGO      --           入庫数量符号(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN MUG.KBN_CD = '01'				 " & vbNewLine _
                                        & " 	            THEN CEILING(SUM(SK.INKO_NB1) / 2)				 " & vbNewLine _
                                        & " 	            WHEN MUG.KBN_CD = '02'				 " & vbNewLine _
                                        & " 	            THEN FLOOR(SUM(SK.INKO_NB1) / 2)				 " & vbNewLine _
                                        & " 	            ELSE SUM(SK.INKO_NB1)				 " & vbNewLine _
                                        & " 	        END * SK.IRIME                                  AS HKN1_NYUKO_SURYO           --           入庫数量(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN SUM(SK.OUTKO_NB1) >= 0				 " & vbNewLine _
                                        & " 	            THEN '+'				 " & vbNewLine _
                                        & " 	            ELSE '-'				 " & vbNewLine _
                                        & " 	       END                                              AS HKN1_SYUKO_SURYO_FUGO      --           出庫数量符号(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN MUG.KBN_CD = '01'				 " & vbNewLine _
                                        & " 	            THEN CEILING(SUM(SK.OUTKO_NB1) / 2)				 " & vbNewLine _
                                        & " 	            WHEN MUG.KBN_CD = '02'				 " & vbNewLine _
                                        & " 	            THEN FLOOR(SUM(SK.OUTKO_NB1) / 2)				 " & vbNewLine _
                                        & " 	            ELSE SUM(SK.OUTKO_NB1)				 " & vbNewLine _
                                        & " 	       END * SK.IRIME                                   AS HKN1_SYUKO_SURYO           --           出庫数量(設定不要)              				 " & vbNewLine _
                                        & " 	     , CASE WHEN SUM(SK.KISYZAN_NB2 				 " & vbNewLine _
                                        & " 	                   + SK.INKO_NB2 				 " & vbNewLine _
                                        & " 	                   - SK.OUTKO_NB2) >= 0				 " & vbNewLine _
                                        & " 	            THEN '+'				 " & vbNewLine _
                                        & " 	            ELSE '-'				 " & vbNewLine _
                                        & " 	       END                                              AS HKN2_KIMATU_ZAIKOSU_FUGO   --保管対象２ 期末在庫数符号(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN MUG.KBN_CD = '01' 				 " & vbNewLine _
                                        & " 	            THEN CEILING(SUM(SK.KISYZAN_NB2 + SK.INKO_NB2 - SK.OUTKO_NB2) / 2)				 " & vbNewLine _
                                        & " 	            WHEN MUG.KBN_CD = '02' 				 " & vbNewLine _
                                        & " 	            THEN FLOOR(SUM(SK.KISYZAN_NB2 + SK.INKO_NB2 - SK.OUTKO_NB2) / 2)				 " & vbNewLine _
                                        & " 	            ELSE SUM(SK.KISYZAN_NB2 + SK.INKO_NB2 - SK.OUTKO_NB2)				 " & vbNewLine _
                                        & " 	        END                                             AS HKN2_KIMATU_ZAIKOSU        --           期末在庫数(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN SUM(SK.INKO_NB2) >= 0				 " & vbNewLine _
                                        & " 	            THEN '+'				 " & vbNewLine _
                                        & " 	            ELSE '-'				 " & vbNewLine _
                                        & " 	       END                                              AS HKN2_NYUKO_SURYO_FUGO      --           入庫数量符号(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN MUG.KBN_CD = '01' 				 " & vbNewLine _
                                        & " 	            THEN CEILING(SUM(SK.INKO_NB2) / 2)				 " & vbNewLine _
                                        & " 	            WHEN MUG.KBN_CD = '02' 				 " & vbNewLine _
                                        & " 	            THEN FLOOR(SUM(SK.INKO_NB2) / 2)				 " & vbNewLine _
                                        & " 	            ELSE SUM(SK.INKO_NB2)				 " & vbNewLine _
                                        & " 	        END * SK.IRIME                                  AS HKN2_NYUKO_SURYO           --           入庫数量(設定不要)				 " & vbNewLine _
                                        & " 	     				 " & vbNewLine _
                                        & " 	     , CASE WHEN SUM(SK.OUTKO_NB2) >= 0				 " & vbNewLine _
                                        & " 	            THEN '+'				 " & vbNewLine _
                                        & " 	            ELSE '-'				 " & vbNewLine _
                                        & " 	       END                                              AS HKN2_SYUKO_SURYO_FUGO      --           出庫数量符号(設定不要)				 " & vbNewLine _
                                        & " 	     				 " & vbNewLine _
                                        & " 	     , CASE WHEN MUG.KBN_CD = '01' 				 " & vbNewLine _
                                        & " 	            THEN CEILING(SUM(SK.OUTKO_NB2) / 2)				 " & vbNewLine _
                                        & " 	            WHEN MUG.KBN_CD = '02' 				 " & vbNewLine _
                                        & " 	            THEN FLOOR(SUM(SK.OUTKO_NB2) / 2)				 " & vbNewLine _
                                        & " 	            ELSE SUM(SK.OUTKO_NB2)				 " & vbNewLine _
                                        & " 	        END * SK.IRIME                                  AS HKN2_SYUKO_SURYO           --           出庫数量(設定不要)				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	     , CASE WHEN SUM(SK.KISYZAN_NB3 				 " & vbNewLine _
                                        & " 	                   + SK.INKO_NB3 				 " & vbNewLine _
                                        & " 	                   - SK.OUTKO_NB3) >= 0				 " & vbNewLine _
                                        & " 	            THEN '+'				 " & vbNewLine _
                                        & " 	            ELSE '-'				 " & vbNewLine _
                                        & " 	       END                                              AS HKN3_KIMATU_ZAIKOSU_FUGO   --保管対象３ 期末在庫数符号(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN MUG.KBN_CD = '01' 				 " & vbNewLine _
                                        & " 	            THEN CEILING(SUM(SK.KISYZAN_NB3 + SK.INKO_NB3 - SK.OUTKO_NB3) / 2)				 " & vbNewLine _
                                        & " 	            WHEN MUG.KBN_CD = '02' 				 " & vbNewLine _
                                        & " 	            THEN FLOOR(SUM(SK.KISYZAN_NB3 + SK.INKO_NB3 - SK.OUTKO_NB3) / 2)				 " & vbNewLine _
                                        & " 	            ELSE SUM(SK.KISYZAN_NB3 + SK.INKO_NB3 - SK.OUTKO_NB3)				 " & vbNewLine _
                                        & " 	        END                                             AS HKN3_KIMATU_ZAIKOSU        --           期末在庫数(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN SUM(SK.INKO_NB3) >= 0				 " & vbNewLine _
                                        & " 	            THEN '+'				 " & vbNewLine _
                                        & " 	            ELSE '-'				 " & vbNewLine _
                                        & " 	       END                                              AS HKN3_NYUKO_SURYO_FUGO      --           入庫数量符号(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN MUG.KBN_CD = '01' 				 " & vbNewLine _
                                        & " 	            THEN CEILING(SUM(SK.INKO_NB3) / 2)				 " & vbNewLine _
                                        & " 	            WHEN MUG.KBN_CD = '02' 				 " & vbNewLine _
                                        & " 	            THEN FLOOR(SUM(SK.INKO_NB3) / 2)				 " & vbNewLine _
                                        & " 	            ELSE SUM(SK.INKO_NB3)				 " & vbNewLine _
                                        & " 	        END * SK.IRIME                      AS HKN3_NYUKO_SURYO                       --           入庫数量(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN SUM(SK.OUTKO_NB3) >= 0				 " & vbNewLine _
                                        & " 	            THEN '+'				 " & vbNewLine _
                                        & " 	            ELSE '-'				 " & vbNewLine _
                                        & " 	       END                                              AS HKN3_SYUKO_SURYO_FUGO      --           出庫数量符号(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN MUG.KBN_CD = '01' 				 " & vbNewLine _
                                        & " 	            THEN CEILING(SUM(SK.OUTKO_NB3)) / 2				 " & vbNewLine _
                                        & " 	            WHEN MUG.KBN_CD = '02' 				 " & vbNewLine _
                                        & " 	            THEN FLOOR(SUM(SK.OUTKO_NB3)) / 2				 " & vbNewLine _
                                        & " 	            ELSE SUM(SK.OUTKO_NB3) / 2				 " & vbNewLine _
                                        & " 	       END * SK.IRIME                                   AS HKN3_SYUKO_SURYO           --           出庫数量(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS YOBI1                      -- FILLER(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS ORDER_NO                   -- オーダー№(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SYORI_DATE                 -- 処理日付(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SYORI_TIME                 -- 処理時間(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS YOBI2                      -- FILLER(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SITABARAI_KBN              -- 下払区分(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SITABARAI_CD               -- 下払先コード(設定不要)				 " & vbNewLine _
                                        & " 	     , ISNULL(DTL.GRADE_1, '')                          AS GRADE1                     -- グレード1				 " & vbNewLine _
                                        & " 	     , ''                                               AS YOBI3                      -- FILLER(設定不要)          				 " & vbNewLine _
                                        & " 	     				 " & vbNewLine _
                                        & " 	  FROM LM_TRN..G_SEKY_TBL AS SK				 " & vbNewLine _
                                        & " 	  LEFT JOIN (				 " & vbNewLine _
                                        & " 	        SELECT NG.NRS_BR_CD				 " & vbNewLine _
                                        & " 	             , NG.INKA_CTL_NO_L				 " & vbNewLine _
                                        & " 	             , MAX(INKA_HOKAN_BASYO)  AS HOKAN_BASYO				 " & vbNewLine _
                                        & " 	             , MAX(UG)          AS UG				 " & vbNewLine _
                                        & " 	             , MAX(NISUGATA_CD) AS NISUGATA_CD				 " & vbNewLine _
                                        & " 	             , MAX(YOURYOU)       AS YORYO				 " & vbNewLine _
                                        & " 	             , MAX(GRADE1)     AS GRADE_1				 " & vbNewLine _
                                        & " 	          FROM LM_TRN..H_INKAEDI_DTL_NCGO_NEW AS NG				 " & vbNewLine _
                                        & " 	          LEFT JOIN				 " & vbNewLine _
                                        & " 	              LM_TRN..D_ZAI_TRS AS ZAI				 " & vbNewLine _
                                        & " 	           ON ZAI.INKA_NO_L = NG.INKA_CTL_NO_L				 " & vbNewLine _
                                        & " 	          AND NOT (ZAI.ALLOC_CAN_NB = 0 AND ZAI.SYS_UPD_DATE < @S_DATE)				 " & vbNewLine _
                                        & " 	          AND ZAI.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	          AND ZAI.HOKAN_YN = '01'				 " & vbNewLine _
                                        & " 	         WHERE NG.NRS_BR_CD = '60'				 " & vbNewLine _
                                        & " 	           AND DEL_KB = '0'				 " & vbNewLine _
                                        & " 	           AND NG.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	           AND ZAI.NRS_BR_CD IS NOT NULL				 " & vbNewLine _
                                        & " 	         GROUP BY 				 " & vbNewLine _
                                        & " 	               NG.NRS_BR_CD				 " & vbNewLine _
                                        & " 	             , NG.INKA_CTL_NO_L				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	         UNION 				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	        SELECT NG.NRS_BR_CD				 " & vbNewLine _
                                        & " 	             , NG.INKA_CTL_NO_L				 " & vbNewLine _
                                        & " 	             , MAX(CASE NG.INKA_BASHO_SP				 " & vbNewLine _
                                        & " 	                        WHEN 'AANR1' THEN 'A0EH'				 " & vbNewLine _
                                        & " 	                        WHEN 'AANR2' THEN 'A0EI'				 " & vbNewLine _
                                        & " 	                        WHEN 'AANR3' THEN 'A0EJ'				 " & vbNewLine _
                                        & " 	                        WHEN 'AANR4' THEN 'A0EK'				 " & vbNewLine _
                                        & " 	                        WHEN 'AANR9' THEN 'A2YG'				 " & vbNewLine _
                                        & " 	                        ELSE ''				 " & vbNewLine _
                                        & " 	                    END)           AS HOKAN_BASYO				 " & vbNewLine _
                                        & " 	             , MAX(NG.UG)          AS UG				 " & vbNewLine _
                                        & " 	             , MAX(NG.NISUGATA_CD) AS NISUGATA_CD				 " & vbNewLine _
                                        & " 	             , MAX(NG.YORYO)       AS YORYO				 " & vbNewLine _
                                        & " 	             , MAX(NG.GRADE_1)     AS GRADE_1				 " & vbNewLine _
                                        & " 	          FROM LM_TRN..H_INKAEDI_HED_NCGO AS NG				 " & vbNewLine _
                                        & " 	          LEFT JOIN				 " & vbNewLine _
                                        & " 	               LM_TRN..D_ZAI_TRS AS ZAI				 " & vbNewLine _
                                        & " 	            ON ZAI.INKA_NO_L = NG.INKA_CTL_NO_L				 " & vbNewLine _
                                        & " 	           AND NOT (ZAI.ALLOC_CAN_NB = 0 AND ZAI.SYS_UPD_DATE < @S_DATE)				 " & vbNewLine _
                                        & " 	           AND ZAI.ALLOC_CAN_NB > 0				 " & vbNewLine _
                                        & " 	           AND ZAI.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	           AND ZAI.HOKAN_YN = '01'				 " & vbNewLine _
                                        & " 	         WHERE NG.NRS_BR_CD = '60'				 " & vbNewLine _
                                        & " 	           AND NG.DEL_KB = '0'				 " & vbNewLine _
                                        & " 	           AND NG.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	           AND ZAI.NRS_BR_CD IS NOT NULL				 " & vbNewLine _
                                        & " 	         GROUP BY 				 " & vbNewLine _
                                        & " 	               NG.NRS_BR_CD				 " & vbNewLine _
                                        & " 	             , NG.INKA_CTL_NO_L				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	       ) AS DTL				 " & vbNewLine _
                                        & " 	    ON DTL.INKA_CTL_NO_L = SK.INKA_NO_L				 " & vbNewLine _
                                        & " 	   AND DTL.NRS_BR_CD = SK.NRS_BR_CD    				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	  LEFT JOIN LM_TRN..B_INKA_L AS BL				 " & vbNewLine _
                                        & " 	    ON BL.CUST_CD_L   = '32516'				 " & vbNewLine _
                                        & "--DEL 2022/08/03 031530   AND BL.CUST_CD_M   = '00'				 " & vbNewLine _
                                        & " 	   AND BL.NRS_BR_CD = SK.NRS_BR_CD				 " & vbNewLine _
                                        & " 	   AND BL.INKA_NO_L = SK.INKA_NO_L				 " & vbNewLine _
                                        & " 	   AND BL.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	   				 " & vbNewLine _
                                        & " 	  LEFT JOIN LM_MST..M_GOODS AS MG				 " & vbNewLine _
                                        & " 	    ON MG.CUST_CD_L    = '32516'				 " & vbNewLine _
                                        & " --DEL 2022/08/03 031530 AND MG.CUST_CD_M    = '00'				 " & vbNewLine _
                                        & " 	   AND MG.GOODS_CD_NRS = SK.GOODS_CD_NRS				 " & vbNewLine _
                                        & " 	   AND MG.NRS_BR_CD    = SK.NRS_BR_CD				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	  LEFT JOIN (				 " & vbNewLine _
                                        & " 	     SELECT NRS_BR_CD				 " & vbNewLine _
                                        & " 	           , WH_CD				 " & vbNewLine _
                                        & " 	          , TOU_NO				 " & vbNewLine _
                                        & " 	          , JISYATASYA_KB				 " & vbNewLine _
                                        & " 	     FROM 				 " & vbNewLine _
                                        & " 	          LM_MST..M_TOU_SITU AS TS				 " & vbNewLine _
                                        & " 	     GROUP BY NRS_BR_CD				 " & vbNewLine _
                                        & " 	            , WH_CD				 " & vbNewLine _
                                        & " 	            , TOU_NO				 " & vbNewLine _
                                        & " 	            , JISYATASYA_KB				 " & vbNewLine _
                                        & " 	     ) TS  				 " & vbNewLine _
                                        & " 	    ON TS.TOU_NO = SK.TOU_NO				 " & vbNewLine _
                                        & " 	   AND TS.WH_CD = SK.WH_CD				 " & vbNewLine _
                                        & " 	   AND TS.NRS_BR_CD = SK.NRS_BR_CD				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	  LEFT JOIN LM_MST..Z_KBN AS Z_TAX				 " & vbNewLine _
                                        & " 	    ON Z_TAX.KBN_GROUP_CD = 'Z001'   				 " & vbNewLine _
                                        & " 	   AND Z_TAX.KBN_CD       = SK.TAX_KB				 " & vbNewLine _
                                        & " 	   AND Z_TAX.SYS_DEL_FLG  = '0'				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	  LEFT JOIN 				 " & vbNewLine _
                                        & " 	       (SELECT M_TAX.[START_DATE]				 " & vbNewLine _
                                        & " 	             , M_TAX.TAX_CD				 " & vbNewLine _
                                        & " 	             , M_TAX.TAX_RATE				 " & vbNewLine _
                                        & " 	          FROM LM_MST..M_TAX 				 " & vbNewLine _
                                        & " 	          JOIN (SELECT  				 " & vbNewLine _
                                        & " 	                       MAX([START_DATE]) as [START_DATE]				 " & vbNewLine _
                                        & " 	                     , TAX_CD 				 " & vbNewLine _
                                        & " 	                  FROM LM_MST..M_TAX 				 " & vbNewLine _
                                        & " 	                 WHERE SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	                   AND [START_DATE] <= @S_DATE				 " & vbNewLine _
                                        & " 	                   AND M_TAX.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	                 GROUP BY TAX_CD				 " & vbNewLine _
                                        & " 	                ) AS NOW_TAX				 " & vbNewLine _
                                        & " 	            ON M_TAX.TAX_CD = NOW_TAX.TAX_CD				 " & vbNewLine _
                                        & " 	           AND M_TAX.[START_DATE] = NOW_TAX.[START_DATE]				 " & vbNewLine _
                                        & " 	           AND M_TAX.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	        ) AS TAX				 " & vbNewLine _
                                        & " 	    ON TAX.TAX_CD = Z_TAX.KBN_NM3           -- KBN_NM3:売上(JRS), KBN_NM4:仕入(JPS)				 " & vbNewLine _
                                        & " 	   AND TAX.START_DATE <= SK.INV_DATE_FROM				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	   LEFT JOIN				 " & vbNewLine _
                                        & " 	        LM_MST..Z_KBN AS MUG				 " & vbNewLine _
                                        & " 	     ON MUG.KBN_GROUP_CD = 'M026'				 " & vbNewLine _
                                        & " 	    AND DTL.NRS_BR_CD IS NULL  				 " & vbNewLine _
                                        & " 	 WHERE SK.INV_DATE_FROM = @S_DATE				 " & vbNewLine _
                                        & " 	   AND BL.NRS_BR_CD = '60'				 " & vbNewLine _
                                        & " 	   AND BL.CUST_CD_L = '32516'				 " & vbNewLine _
                                        & " --DEL 2022/08/03 031530 AND BL.CUST_CD_M = '00'				 " & vbNewLine _
                                        & " 	   AND SK.SEKY_FLG = '00'				 " & vbNewLine _
                                        & " 	   AND SK.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	   -- AND Z_TOU.KBN_NM2 IS NOT NULL				 " & vbNewLine _
                                        & " 	   -- AND DTL.NRS_BR_CD IS NOT NULL				 " & vbNewLine _
                                        & " 	   AND MG.NRS_BR_CD = '60'				 " & vbNewLine _
                                        & " 	 GROUP BY 				 " & vbNewLine _
                                        & " 	       SK.NRS_BR_CD				 " & vbNewLine _
                                        & " 	     , MG.GOODS_CD_CUST				 " & vbNewLine _
                                        & " 	     , SK.LOT_NO				 " & vbNewLine _
                                        & " 	     , SK.IRIME				 " & vbNewLine _
                                        & " 	     , SK.TOU_NO				 " & vbNewLine _
                                        & " 	     , SK.TAX_KB				 " & vbNewLine _
                                        & " 	     , MG.GOODS_NM_1				 " & vbNewLine _
                                        & " 	     , LEFT(SK.INV_DATE_FROM, 6)				 " & vbNewLine _
                                        & " 	     , SUBSTRING(SK.INV_DATE_FROM, 3, 4)				 " & vbNewLine _
                                        & " 	     , SK.INV_DATE_TO				 " & vbNewLine _
                                        & " 	     , DTL.UG				 " & vbNewLine _
                                        & " 	     , DTL.NISUGATA_CD				 " & vbNewLine _
                                        & " 	     , TAX.TAX_RATE				 " & vbNewLine _
                                        & " 	     , DTL.GRADE_1 				 " & vbNewLine _
                                        & " 	     , TAX.TAX_CD				 " & vbNewLine _
                                        & " 	     , Z_TAX.KBN_NM3    				 " & vbNewLine _
                                        & " 	     , MUG.KBN_NM1				 " & vbNewLine _
                                        & " 	     , MUG.KBN_CD				 " & vbNewLine _
                                        & " 	     , DTL.HOKAN_BASYO				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	  HAVING NOT (SUM(SK.SEKI_ARI_NB1 * STORAGE1) 				 " & vbNewLine _
                                        & " 	            + SUM(SK.SEKI_ARI_NB2 * STORAGE2) 				 " & vbNewLine _
                                        & " 	            + SUM(SK.SEKI_ARI_NB3 * STORAGE3) = 0  				 " & vbNewLine _
                                        & " 	         AND  SUM(SK.INKO_NB1)				 " & vbNewLine _
                                        & " 	            + SUM(SK.INKO_NB2)				 " & vbNewLine _
                                        & " 	            + SUM(SK.INKO_NB3)				 " & vbNewLine _
                                        & " 	            + SUM(SK.OUTKO_NB1)				 " & vbNewLine _
                                        & " 	            + SUM(SK.OUTKO_NB2)				 " & vbNewLine _
                                        & " 	            + SUM(SK.OUTKO_NB3) = 0)           				 " & vbNewLine _
                                        & " 	  ) AS SDFD				 " & vbNewLine _
                                        & " 	  WHERE RECORD_ID IS NOT NULL				 " & vbNewLine




    Private Const SQL_BUTSURYUHI_CHK6 As String = " 	SELECT				 " & vbNewLine _
                                        & " 	   -- 鑑(運賃以外)				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	   '6'    as SQ_NO				 " & vbNewLine _
                                        & " 	 , isnull(sum(NET_AMT) ,0)   as  NET_AMT                               --NET金額				 " & vbNewLine _
                                        & " 	FROM (				 " & vbNewLine _
                                        & " 					 " & vbNewLine


    Private Const SQL_BUTSURYUHI_DATA6 As String = " 	SELECT				  " & vbNewLine _
                                        & " 	   -- 鑑(運賃以外)				  " & vbNewLine _
                                        & " 	   CONVERT(char(4),ISNULL(RECORD_ID, ''))                           --レコードID				  " & vbNewLine _
                                        & " 	 + CONVERT(char(3),ISNULL(DATA_ID, ''))                             --データID         データIDの3桁目：V     (日陸様を表すコードです)				  " & vbNewLine _
                                        & " 	 + CONVERT(char(3),ISNULL(COMP_CD,''))                              --会社コード 040   (日本合成化学工業を表すコードです)				  " & vbNewLine _
                                        & " 	 + CONVERT(char(6),ISNULL(KEIJYO_YM,''))                            --計上年月				  " & vbNewLine _
                                        & " 	 + CONVERT(char(12),ISNULL(UKETUKE_NO,''))                          --受付№				  " & vbNewLine _
                                        & " 	 + CONVERT(char(12),ISNULL(BUTURYU_SERI_NO,''))                     --物流整理№				  " & vbNewLine _
                                        & " 	 + CONVERT(char(3),ISNULL(TEISEI_NO,''))                            --訂正No.				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(TEISEI_KBN,''))                           --訂正区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(KINOU,''))                                --機能				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(KINOU_SAIMOKU,''))                        --機能細目				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(SAGYO_N_KBN,''))                             --作業内容区分  	                       			  " & vbNewLine _
                                        & " 	 + CONVERT(char(8),ISNULL(SYORI_YMD,''))                            --伝送処理年月日				  " & vbNewLine _
                                        & " 	 + CONVERT(char(9),ISNULL(SYORI_JIKAN,''))                          --伝送処理時間				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(NYURYOKU_BASYO,''))                       --入力場所				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(NYURYOKU_BUMON,''))                       --入力部門				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(NYURYOKU_GROUP,''))                       --入力グループ				  " & vbNewLine _
                                        & " 	 + CONVERT(char(8),ISNULL(SAGYOU_DATE,''))                          --作業年月日				  " & vbNewLine _
                                        & " 	 + CONVERT(char(10),ISNULL(HINMEI_RYAKUGO,''))                      --品目略号				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),ISNULL(UG_CD,''))                                --UGコード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(NISUGATA,''))                             --荷姿				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(YOURYO, 0), '00000.00')                            --容量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(KEIYAKU_BASYO,''))                        --契約場所 'AA'固定				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(BIN_KBN,''))                              --便区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),ISNULL(BUTSURYU_COMP_CD,''))                     --物流会社コード H441B (日陸様を表すコードです)				  " & vbNewLine _
                                        & " 	 + CONVERT(char(10),ISNULL(TSUMEAWASE_NO,''))                       --詰合わせNo.				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),ISNULL(JITSU_USER,''))                           --実ユーザー				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),ISNULL(KEIYAKUSAKI_CD,''))                       --契約先コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(KOTEIHI_HEND_KBN,''))                     --固定費・変動費区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HANBAI_BUNRUI,''))                        --販売分類				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HACCHI_KBN,''))                           --発地情報　発地区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),ISNULL(HACCHI_CD,''))                            --　　　　　発地コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(6),ISNULL(HACCHI_CIKU_CD,''))                       --　　　　　市区町村コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(CHAKUCHI_KBN,''))                         --着地情報　着地区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),ISNULL(CHAKUCHI_CD,''))                          --　　　　　着地コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(6),ISNULL(CHAKUCHI_CHIKU_CD,''))                    --　　　　　市区町村コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(20),ISNULL(CHAKUCHI_NOUNYU_BASYO,''))               --　　　　　納入場所名				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HASSEI_BASYO_KBN,''))                     --発生場所区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),ISNULL(HASEI_SP_CD,''))                          --発生SPコード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(SIHARAI_BASYO,''))                        --支払場所				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(BUTURYU_TANI,''))                         --物流単位				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(BUTURYU_FUGO,'+'))                        --物流量符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(BUTURYU_RYO,0), '00000000000.0000')                --物流量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(10),ISNULL(LOT_NO,''))                              --ロット№				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(TAX_BN,''))                               --税区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(GN_KBN,''))                               --GN区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(NET_AMT_FUGO,'+'))                        --NET金額符号				  " & vbNewLine _
                                        & " 	 + FORMAT(NET_AMT ,'000000000000000')                               --NET金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(GROSS_AMT_FUGO,'+'))                      --GROSS金額符号				  " & vbNewLine _
                                        & " 	 + FORMAT(NET_AMT + TAX ,'000000000000000')                         --GROSS金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),TAX_FUGO)                                        --税額符号				  " & vbNewLine _
                                        & " 	 + FORMAT(TAX, '000000000000000')                                   --税額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(ZEIRITU_FUGO,'+'))                        --税率符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(ZEIRITSU * 100, 0), '000.00')                      --税率				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(YUSO_KYORI_FUGO,''))                      --輸送距離符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(YUSOKYORI,''), '00000')                            --輸送距離				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(SAIMOKU1_CD,''))                          --項目細分１　項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(SAIMOKU1_BETSU_AMT_FUGO,'+'))             --　　　　　　項目別金額符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(SAIMOKU1_BETU_AMT,''), '00000')                    --　　　　　　項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(SAIMOKU2_CD,''))                          --項目細分２　項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(SAIMOKU2_BETSU_AMT_FUGO,'+'))             --　　　　　　項目別金額符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(SAIMOKU2_BETU_AMT,''), '00000')                    --　　　　　　項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(SAIMOKU3_CD,''))                          --項目細分３　項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(SAIMOKU3_BETSU_AMT_FUGO,'+'))             --　　　　　　項目別金額符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(SAIMOKU3_BETU_AMT,''), '00000')                    --　　　　　　項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(SAIMOKU4_CD,''))                          --項目細分４　項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(SAIMOKU4_BETSU_AMT_FUGO,'+'))             --　　　　　　項目別金額符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(SAIMOKU4_BETU_AMT,''), '00000')                    --　　　　　　項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(SAIMOKU5_CD,''))                          --項目細分５　項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(SAIMOKU5_BETSU_AMT_FUGO,'+'))             --　　　　　　項目別金額符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(SAIMOKU5_BETU_AMT,''), '00000')                    --　　　　　　項目別金額				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(SAIMOKU6_CD,''))                          --項目細分６　項目コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(SAIMOKU6__BETSU_AMT_FUGO,'+'))            --　　　　　　項目別金額符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(SAIMOKU6_BETU_AMT,''), '00000')                    --　　　　　　項目別金額				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(ERR_CD,''), '0000')                                --エラーコード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(8),ISNULL(NOUNYU_DATE,''))                          --納入日				  " & vbNewLine _
                                        & " 	 + CONVERT(char(5),ISNULL(SYABAN,''))                               --車番				  " & vbNewLine _
                                        & " 	 + CONVERT(char(6),ISNULL(YOBI,''))                                 --FILLER				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN1_TAISYO_KISU,''))                     --保管対象期数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN1_KIMATU_ZAIKOSU_FUGO,''))             --保管対象１　期末在庫数符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(HKN1_KIMATU_ZAIKOSU,0), '00000000000.0000')        --         　 期末在庫数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN1_NYUKO_SURYO_FUGO,''))                --　　　　　　入庫数量符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(HKN1_NYUKO_SURYO,0), '00000000000.0000')           --　　　　　　入庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN1_SYUKO_SURYO_FUGO,''))                --　　　　　　出庫数量符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(HKN1_SYUKO_SURYO,0), '00000000000.0000')           --　　　　　　出庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN2_KIMATU_ZAIKOSU_FUGO,''))             --保管対象２　期末在庫数符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(HKN2_KIMATU_ZAIKOSU,0), '00000000000.0000')        --            期末在庫数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN2_NYUKO_SURYO_FUGO,''))                --　　　　　　入庫数量符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(HKN2_NYUKO_SURYO,0), '00000000000.0000')           --　　　　　　入庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN2_SYUKO_SURYO_FUGO,''))                --　　　　　　出庫数量符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(HKN2_SYUKO_SURYO,0), '00000000000.0000')           --　　　　　　出庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN3_KIMATU_ZAIKOSU_FUGO,''))             --保管対象３　期末在庫数符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(HKN3_KIMATU_ZAIKOSU,0), '00000000000.0000')        --         　 期末在庫数				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN3_NYUKO_SURYO_FUGO,''))                --　　　　　　入庫数量符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(HKN3_NYUKO_SURYO,0), '00000000000.0000')           --　　　　　　入庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(HKN3_SYUKO_SURYO_FUGO,''))                --　　　　　　出庫数量符号				  " & vbNewLine _
                                        & " 	 + FORMAT(ISNULL(HKN3_SYUKO_SURYO,0), '00000000000.0000')           --　　　　　　出庫数量				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),ISNULL(YOBI1,''))                                --FILLER				  " & vbNewLine _
                                        & " 	 + CONVERT(char(10),ISNULL(ORDER_NO,''))                            --オーダー№				  " & vbNewLine _
                                        & " 	 + CONVERT(char(8),ISNULL(SYORI_DATE,''))                           --処理日付				  " & vbNewLine _
                                        & " 	 + CONVERT(char(9),ISNULL(SYORI_TIME,''))                           --処理時間				  " & vbNewLine _
                                        & " 	 + CONVERT(char(2),ISNULL(YOBI2,''))                                --FILLER				  " & vbNewLine _
                                        & " 	 + CONVERT(char(1),ISNULL(SITABARAI_KBN,''))                        --下払区分				  " & vbNewLine _
                                        & " 	 + CONVERT(char(7),ISNULL(SITABARAI_CD,''))                         --下払先コード				  " & vbNewLine _
                                        & " 	 + CONVERT(char(10),ISNULL(GRADE1,''))                              --グレード1				  " & vbNewLine _
                                        & " 	 + CONVERT(char(3),ISNULL(YOBI3,''))                                --FILLER				  " & vbNewLine _
                                        & "        AS  NET_AMT				  " & vbNewLine _
                                        & " 	FROM (				  " & vbNewLine



    Private Const SQL_BUTSURYUHI_SQL6 As String = " 	SELECT   				 " & vbNewLine _
                                        & " 	       'VK74'                                           AS RECORD_ID             --レコードID				 " & vbNewLine _
                                        & " 	     , '41V'                                            AS DATA_ID               --データID         データIDの3桁目：V     (日陸様を表すコードです)				 " & vbNewLine _
                                        & " 	     , '040'                                            AS COMP_CD               --会社コード 040   (日本合成化学工業を表すコードです)				 " & vbNewLine _
                                        & " 	     , LEFT(HD.SKYU_DATE, 6)                            AS KEIJYO_YM             --計上年月				 " & vbNewLine _
                                        & " 	     , ''                                               AS UKETUKE_NO            --受付№(設定不要)				 " & vbNewLine _
                                        & " 	     , SUBSTRING(HD.SKYU_DATE, 3, 4) 				 " & vbNewLine _
                                        & " 	     + 'K'				 " & vbNewLine _
                                        & " 	     + FORMAT(row_number() over(ORDER BY HD.SKYU_DATE), '0000000')    				 " & vbNewLine _
                                        & " 	                                                        AS BUTURYU_SERI_NO       --物流整理№				 " & vbNewLine _
                                        & " 	     , '000'                                            AS TEISEI_NO             --訂正No.(別紙3:000, 001)				 " & vbNewLine _
                                        & " 	     , '1'                                              AS TEISEI_KBN            --訂正区分(別紙3:1,21,2,31)				 " & vbNewLine _
                                        & " 	     , ''                                               AS KINOU                 --機能(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS KINOU_SAIMOKU         --機能細目(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN DL.GROUP_KB IN ('04', '11')              -- 作業料				 " & vbNewLine _
                                        & " 	            THEN '44'           -- 港湾荷役				 " & vbNewLine _
                                        & " 	            WHEN DL.GROUP_KB IN ('02', '09')              -- 荷役料				 " & vbNewLine _
                                        & " 	            THEN '44'           -- 港湾荷役				 " & vbNewLine _
                                        & " 	            WHEN DL.GROUP_KB IN ('01', '08')              -- 保管料 				 " & vbNewLine _
                                        & " 	            THEN '61'           -- 倉庫保管				 " & vbNewLine _
                                        & " 	            ELSE '96'           -- 業務委託				 " & vbNewLine _
                                        & " 	        END                                             AS SAGYO_N_KBN           --作業内容区分(別紙4:)				 " & vbNewLine _
                                        & " 	     , CONVERT(VARCHAR, GETDATE(),112)                  AS SYORI_YMD             --伝送処理年月日				 " & vbNewLine _
                                        & " 	     , FORMAT(GETDATE(), 'HHmmss')                      AS SYORI_JIKAN           --伝送処理時間				 " & vbNewLine _
                                        & " 	     , ''                                               AS NYURYOKU_BASYO        --入力場所(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS NYURYOKU_BUMON        --入力部門(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS NYURYOKU_GROUP        --入力グループ(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN DL.GROUP_KB IN ('02', '04', '09', '11')  				 " & vbNewLine _
                                        & " 	            THEN HD.SKYU_DATE   -- 荷役                                  				 " & vbNewLine _
                                        & " 	            ELSE ''             -- その他				 " & vbNewLine _
                                        & " 	        END                                             AS SAGYOU_DATE           --作業年月日(荷役作業日)				 " & vbNewLine _
                                        & " 	     , ''                                               AS HINMEI_RYAKUGO        --品名略号				 " & vbNewLine _
                                        & " 	     , ISNULL(MUG.KBN_NM1, '')                          AS UG_CD                 --UGコード				 " & vbNewLine _
                                        & " 	     , ''                                               AS NISUGATA              --荷姿				 " & vbNewLine _
                                        & " 	     , 0                                                AS YOURYO                --容量				 " & vbNewLine _
                                        & " 	     , 'AA'                                             AS KEIYAKU_BASYO         --契約場所 'AA'固定				 " & vbNewLine _
                                        & " 	     , ''                                               AS BIN_KBN               --便区分(設定不要)				 " & vbNewLine _
                                        & " 	     , 'H441B'                                          AS BUTSURYU_COMP_CD      --物流会社コード H441B (日陸様を表すコードです)				 " & vbNewLine _
                                        & " 	     , ''                                               AS TSUMEAWASE_NO         --積合わせNo.(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS JITSU_USER            --実ユーザー(受注先コード)				 " & vbNewLine _
                                        & " 	     , ''                                               AS KEIYAKUSAKI_CD        --契約先コード(支払人コード)				 " & vbNewLine _
                                        & " 	     , '2'                                              AS KOTEIHI_HEND_KBN      --固定費・変動費区分				 " & vbNewLine _
                                        & " 	     , CASE WHEN DL.GROUP_KB IN ('02', '09') 				 " & vbNewLine _
                                        & " 	            THEN '0'   -- 荷役				 " & vbNewLine _
                                        & " 	            WHEN DL.GROUP_KB IN ('04', '11')				 " & vbNewLine _
                                        & " 	            THEN CASE WHEN DL.SEIQKMK_CD IN ('60', '62')				 " & vbNewLine _
                                        & " 	                      THEN '9'				 " & vbNewLine _
                                        & " 	                      ELSE '0'				 " & vbNewLine _
                                        & " 	                  END                                   				 " & vbNewLine _
                                        & " 	            ELSE ''    -- その他				 " & vbNewLine _
                                        & " 	       END                                              AS HANBAI_BUNRUI         --販売分類				 " & vbNewLine _
                                        & " 	     , ''                                               AS HACCHI_KBN            --発地情報 発地区分(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS HACCHI_CD             --         発地コード(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS HACCHI_CIKU_CD        --         市区町村コード(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS CHAKUCHI_KBN          --着地情報 着地区分(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS CHAKUCHI_CD           --         着地コード(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS CHAKUCHI_CHIKU_CD     --         市区町村コード(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS CHAKUCHI_NOUNYU_BASYO --         納入場所名(設定不要)				 " & vbNewLine _
                                        & " 	     , '2'                                              AS HASSEI_BASYO_KBN      --発生場所区分				 " & vbNewLine _
                                        & " 	     , 'A0I4'                                           AS HASEI_SP_CD           --発生SPコード				 " & vbNewLine _
                                        & " 	     , 'A0'                                             AS SIHARAI_BASYO         --支払場所				 " & vbNewLine _
                                        & " 	     , '2'                                              AS BUTURYU_TANI          --物流単位				 " & vbNewLine _
                                        & " 	     , '+'                                              AS BUTURYU_FUGO          --物流量符号				 " & vbNewLine _
                                        & " 	     , 1                                                AS BUTURYU_RYO           --物流量				 " & vbNewLine _
                                        & " 	     , ''                                               AS LOT_NO                --ロット№(設定不要)				 " & vbNewLine _
                                        & " 	     , CASE WHEN TAX.TAX_RATE <> 0 THEN '1' 				 " & vbNewLine _
                                        & " 	            ELSE '2'               				 " & vbNewLine _
                                        & " 	       END                                              AS TAX_BN                --税区分				 " & vbNewLine _
                                        & " 	     , 'N'                                              AS GN_KBN                --GN区分(G or N)				 " & vbNewLine _
                                        & " 	     ,  CASE WHEN SUM(DL.KEISAN_TLGK) >= 0				 " & vbNewLine _
                                        & " 	             THEN '+'				 " & vbNewLine _
                                        & " 	             ELSE '-'				 " & vbNewLine _
                                        & " 	       END                                              AS NET_AMT_FUGO          --NET金額符号				 " & vbNewLine _
                                        & " 	     , CASE WHEN MUG.KBN_CD = '01'				 " & vbNewLine _
                                        & " 	            THEN CEILING(SUM(DL.KEISAN_TLGK) / 2)				 " & vbNewLine _
                                        & " 	            WHEN MUG.KBN_CD = '02'				 " & vbNewLine _
                                        & " 	            THEN FLOOR(SUM(DL.KEISAN_TLGK) / 2)				 " & vbNewLine _
                                        & " 	            ELSE SUM(DL.KEISAN_TLGK)				 " & vbNewLine _
                                        & " 	       END                                              AS NET_AMT               --NET金額				 " & vbNewLine _
                                        & " 	     , CASE WHEN SUM(DL.KEISAN_TLGK) >= 0				 " & vbNewLine _
                                        & " 	             THEN '+'				 " & vbNewLine _
                                        & " 	             ELSE '-'				 " & vbNewLine _
                                        & " 	       END                                              AS GROSS_AMT_FUGO        --GROSS金額符号				 " & vbNewLine _
                                        & " 	     , 0                                                AS GROSS_AMT             --GROSS金額				 " & vbNewLine _
                                        & " 	     , CASE WHEN TAX.TAX_RATE >= 0				 " & vbNewLine _
                                        & " 	             THEN '+'				 " & vbNewLine _
                                        & " 	             ELSE '-'				 " & vbNewLine _
                                        & " 	       END                                              AS TAX_FUGO              --税額符号				 " & vbNewLine _
                                        & " 	     , ROUND(CASE WHEN MUG.KBN_CD = '01'				 " & vbNewLine _
                                        & " 	             THEN CEILING(SUM(DL.KEISAN_TLGK) / 2)				 " & vbNewLine _
                                        & " 	             WHEN MUG.KBN_CD = '02'				 " & vbNewLine _
                                        & " 	             THEN FLOOR(SUM(DL.KEISAN_TLGK) / 2)				 " & vbNewLine _
                                        & " 	             ELSE SUM(DL.KEISAN_TLGK)				 " & vbNewLine _
                                        & " 	       END * TAX.TAX_RATE, 0)				 " & vbNewLine _
                                        & " 	                                                        AS TAX                        --税額				 " & vbNewLine _
                                        & " 	     , '+'                                              AS ZEIRITU_FUGO               --税率符号				 " & vbNewLine _
                                        & " 	     ,  TAX.TAX_RATE                                    AS ZEIRITSU                   --税率				 " & vbNewLine _
                                        & " 	     , '+'                                              AS YUSO_KYORI_FUGO            --輸送距離符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS YUSOKYORI                  --輸送距離(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SAIMOKU1_CD                --項目細分１ 項目コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS SAIMOKU1_BETSU_AMT_FUGO    --           項目別金額符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS SAIMOKU1_BETU_AMT          --           項目別金額(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SAIMOKU2_CD                --項目細分２ 項目コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS SAIMOKU2_BETSU_AMT_FUGO    --           項目別金額符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS SAIMOKU2_BETU_AMT          --           項目別金額(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SAIMOKU3_CD                --項目細分３ 項目コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS SAIMOKU3_BETSU_AMT_FUGO    --           項目別金額符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS SAIMOKU3_BETU_AMT          --           項目別金額(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SAIMOKU4_CD                --項目細分４ 項目コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS SAIMOKU4_BETSU_AMT_FUGO    --           項目別金額符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS SAIMOKU4_BETU_AMT          --           項目別金額(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SAIMOKU5_CD                --項目細分５ 項目コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS SAIMOKU5_BETSU_AMT_FUGO    --           項目別金額符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS SAIMOKU5_BETU_AMT          --           項目別金額(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SAIMOKU6_CD                --項目細分６ 項目コード(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS SAIMOKU6__BETSU_AMT_FUGO   --           項目別金額符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS SAIMOKU6_BETU_AMT          --           項目別金額(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS ERR_CD                     --エラーコード(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS NOUNYU_DATE                --納入日(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SYABAN                     --車番(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS YOBI                       --FILLER(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS HKN1_TAISYO_KISU           --保管対象期数(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS HKN1_KIMATU_ZAIKOSU_FUGO   --保管対象１ 期末在庫数符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS HKN1_KIMATU_ZAIKOSU        --           期末在庫数(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS HKN1_NYUKO_SURYO_FUGO      --           入庫数量符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS HKN1_NYUKO_SURYO           --           入庫数量(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS HKN1_SYUKO_SURYO_FUGO      --           出庫数量符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS HKN1_SYUKO_SURYO           --           出庫数量(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS HKN2_KIMATU_ZAIKOSU_FUGO   --保管対象２ 期末在庫数符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS HKN2_KIMATU_ZAIKOSU        --           期末在庫数(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS HKN2_NYUKO_SURYO_FUGO      --           入庫数量符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS HKN2_NYUKO_SURYO           --           入庫数量(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS HKN2_SYUKO_SURYO_FUGO      --           出庫数量符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS HKN2_SYUKO_SURYO           --           出庫数量(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS HKN3_KIMATU_ZAIKOSU_FUGO   --保管対象３ 期末在庫数符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS HKN3_KIMATU_ZAIKOSU        --           期末在庫数(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS HKN3_NYUKO_SURYO_FUGO      --           入庫数量符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS HKN3_NYUKO_SURYO           --           入庫数量(設定不要)				 " & vbNewLine _
                                        & " 	     , '+'                                              AS HKN3_SYUKO_SURYO_FUGO      --           出庫数量符号(設定不要)				 " & vbNewLine _
                                        & " 	     , 0                                                AS HKN3_SYUKO_SURYO           --           出庫数量(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS YOBI1                      -- FILLER(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS ORDER_NO                   -- オーダー№				 " & vbNewLine _
                                        & " 	     , ''                                               AS SYORI_DATE                 -- 処理日付(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SYORI_TIME                 -- 処理時間(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS YOBI2                      -- FILLER(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SITABARAI_KBN              --下払区分(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS SITABARAI_CD               --下払先コード(設定不要)				 " & vbNewLine _
                                        & " 	     , ''                                               AS GRADE1                     --グレード1				 " & vbNewLine _
                                        & " 	     , ''                                               AS YOBI3                      -- FILLER(設定不要				 " & vbNewLine _
                                        & " 	  FROM LM_TRN..G_KAGAMI_HED AS HD				 " & vbNewLine _
                                        & " 	  LEFT JOIN 				 " & vbNewLine _
                                        & " 	       LM_TRN..G_KAGAMI_DTL AS DL				 " & vbNewLine _
                                        & " 	    ON DL.SKYU_NO = HD.SKYU_NO				 " & vbNewLine _
                                        & " 	   AND DL.SYS_DEL_FLG = '0'   				 " & vbNewLine _
                                        & " 	   AND DL.GROUP_KB NOT IN ('03', '05', '10', '12') -- 運賃以外             				 " & vbNewLine _
                                        & " 	   LEFT JOIN 				 " & vbNewLine _
                                        & " 	        LM_MST..M_SEIQKMK AS KM				 " & vbNewLine _
                                        & " 	     ON KM.GROUP_KB   = DL.GROUP_KB				 " & vbNewLine _
                                        & " 	    AND KM.SEIQKMK_CD = DL.SEIQKMK_CD				 " & vbNewLine _
                                        & " 	    				 " & vbNewLine _
                                        & " 	   LEFT JOIN				 " & vbNewLine _
                                        & " 	        LM_MST..Z_KBN AS MUG				 " & vbNewLine _
                                        & " 	     ON MUG.KBN_GROUP_CD = 'M026'				 " & vbNewLine _
                                        & " 	    AND DL.GROUP_KB NOT IN ('03', '05', '10', '12') -- 運賃以外				 " & vbNewLine _
                                        & " 	   				 " & vbNewLine _
                                        & " 	  LEFT JOIN LM_MST..Z_KBN AS Z_TAX				 " & vbNewLine _
                                        & " 	    ON Z_TAX.KBN_GROUP_CD = 'Z001'   				 " & vbNewLine _
                                        & " 	   AND Z_TAX.KBN_CD       = KM.TAX_KB				 " & vbNewLine _
                                        & " 	   AND Z_TAX.SYS_DEL_FLG  = '0'				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	  LEFT JOIN 				 " & vbNewLine _
                                        & " 	       (SELECT M_TAX.[START_DATE]				 " & vbNewLine _
                                        & " 	             , M_TAX.TAX_CD				 " & vbNewLine _
                                        & " 	             , M_TAX.TAX_RATE				 " & vbNewLine _
                                        & " 	          FROM LM_MST..M_TAX 				 " & vbNewLine _
                                        & " 	          JOIN (SELECT  				 " & vbNewLine _
                                        & " 	                       MAX([START_DATE]) as [START_DATE]				 " & vbNewLine _
                                        & " 	                     , TAX_CD 				 " & vbNewLine _
                                        & " 	                  FROM LM_MST..M_TAX 				 " & vbNewLine _
                                        & " 	                 WHERE SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	                   AND [START_DATE] <= @S_DATE				 " & vbNewLine _
                                        & " 	                   AND M_TAX.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	                 GROUP BY TAX_CD				 " & vbNewLine _
                                        & " 	                ) AS NOW_TAX				 " & vbNewLine _
                                        & " 	            ON M_TAX.TAX_CD = NOW_TAX.TAX_CD				 " & vbNewLine _
                                        & " 	           AND M_TAX.[START_DATE] = NOW_TAX.[START_DATE]				 " & vbNewLine _
                                        & " 	           AND M_TAX.SYS_DEL_FLG = '0'				 " & vbNewLine _
                                        & " 	        ) AS TAX				 " & vbNewLine _
                                        & " 	    ON TAX.TAX_CD = Z_TAX.KBN_NM3           -- KBN_NM3:売上(JRS), KBN_NM4:仕入(JPS)				 " & vbNewLine _
                                        & " 	   AND TAX.START_DATE <= HD.SKYU_DATE				 " & vbNewLine _
                                        & " 	 WHERE 				 " & vbNewLine _
                                        & " 	       HD.SEIQTO_CD = '3251699'				 " & vbNewLine _
                                        & " 	   AND LEFT(HD.SKYU_DATE, 6) = @KEIJYO				 " & vbNewLine _
                                        & " 	   AND HD.STATE_KB           >= '01' -- 確定				 " & vbNewLine _
                                        & " 	   AND (   HD.CRT_KB         = '01'                      -- 手書き請求書				 " & vbNewLine _
                                        & " 	        OR (HD.CRT_KB = '00' AND DL.MAKE_SYU_KB = '01')  -- 追加[K021]				 " & vbNewLine _
                                        & " 	        OR DL.TEMPLATE_IMP_FLG = '01'                    -- テンプレート				 " & vbNewLine _
                                        & " 	        )				 " & vbNewLine _
                                        & " 	   AND DL.SEIQKMK_CD IS NOT NULL				 " & vbNewLine _
                                        & " 	   AND DL.SEIQKMK_CD IS NOT NULL				 " & vbNewLine _
                                        & " 					 " & vbNewLine _
                                        & " 	 GROUP BY 				 " & vbNewLine _
                                        & " 	       HD.SKYU_DATE				 " & vbNewLine _
                                        & " 	     , MUG.KBN_NM1				 " & vbNewLine _
                                        & " 	     , MUG.KBN_CD				 " & vbNewLine _
                                        & " 	     , TAX_RATE				 " & vbNewLine _
                                        & " 	      , CASE WHEN DL.GROUP_KB IN ('04', '11')             -- 作業料				 " & vbNewLine _
                                        & " 	            THEN '44'           -- 港湾荷役				 " & vbNewLine _
                                        & " 	            WHEN DL.GROUP_KB IN ('02', '09')              -- 荷役料				 " & vbNewLine _
                                        & " 	            THEN '44'           -- 港湾荷役				 " & vbNewLine _
                                        & " 	            WHEN DL.GROUP_KB IN ('01', '08')              -- 保管料 				 " & vbNewLine _
                                        & " 	            THEN '61'           -- 倉庫保管				 " & vbNewLine _
                                        & " 	            ELSE '96'           -- 業務委託				 " & vbNewLine _
                                        & " 	        END        				 " & vbNewLine _
                                        & " 	      , CASE WHEN DL.GROUP_KB IN ('02', '04', '09', '11')  				 " & vbNewLine _
                                        & " 	             THEN HD.SKYU_DATE   -- 荷役                                  				 " & vbNewLine _
                                        & " 	             ELSE ''             -- その他				 " & vbNewLine _
                                        & " 	         END  				 " & vbNewLine _
                                        & " 	     , CASE WHEN DL.GROUP_KB IN ('02', '09') 				 " & vbNewLine _
                                        & " 	            THEN '0'   -- 荷役				 " & vbNewLine _
                                        & " 	            WHEN DL.GROUP_KB IN ('04', '11')				 " & vbNewLine _
                                        & " 	            THEN CASE WHEN DL.SEIQKMK_CD IN ('60', '62')				 " & vbNewLine _
                                        & " 	                      THEN '9'				 " & vbNewLine _
                                        & " 	                      ELSE '0'				 " & vbNewLine _
                                        & " 	                  END                                   				 " & vbNewLine _
                                        & " 	            ELSE ''    -- その他				 " & vbNewLine _
                                        & " 	       END                                              				 " & vbNewLine _
                                        & " 	  ) AS SDFD				 " & vbNewLine _
                                        & " 	  WHERE RECORD_ID IS NOT NULL				 " & vbNewLine



#End Region

#End Region

#Region "Field"

    '''' <summary>
    '''' 検索条件設定用
    '''' </summary>
    '''' <remarks></remarks>
    'Private _Row As Data.DataRow

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

    Private setWhereBr As Boolean
    Private setWhereCustL As Boolean
    Private setWhereCustM As Boolean
    Private setWhereCustS As Boolean
    Private setWhereCustSS As Boolean
    Private setWhereDateFrom As Boolean
    Private setWhereDateTo As Boolean
    Private setWhereDepart As Boolean


#End Region

#Region "SQLメイン処理"

#Region "物流費チェック"

    ''' <summary>
    ''' 物流費チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Butsuryuhi_Chk(ByVal ds As DataSet, ByVal sqlNo As String) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI470IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        'Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_CHK_ALL)         'SQL構築

        Select Case sqlNo
            Case "1"
                Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_CHK1)         'SQL構築
            Case "2"
                Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_CHK2)         'SQL構築
            Case "3"
                Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_CHK3)         'SQL構築
            Case "4"
                Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_CHK4)         'SQL構築
            Case "5"
                Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_CHK5)         'SQL構築
            Case "6"
                Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_CHK6)         'SQL構築

        End Select

        'パラメータ設定
        Call Me.SetUntinParameter(inTbl.Rows(0), Me._SqlPrmList)
        'Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI470IN").Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI470DAC", "Butsuryuhi_Chk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NET_AMT", "NET_AMT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI470OUTWK")

        Return ds

    End Function


    Private Function Butsuryuhi_Rtn1(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI470IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Select Case inTbl.Rows(0).Item("SYORI_PTN").ToString
            Case "1"
                'チェック
                Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_CHK1)         'SQL
            Case "2"
                '物流費データ作成
                Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_DATA1)         'SQL
        End Select

        Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_SQL1)         'SQL構築

        'パラメータ設定
        Call Me.SetUntinParameter(inTbl.Rows(0), Me._SqlPrmList)
        'Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI470IN").Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI470DAC", "Butsuryuhi_Rtn1", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NET_AMT", "NET_AMT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI470OUTWK")

        Return ds

    End Function

    Private Function Butsuryuhi_Rtn2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI470IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Select Case inTbl.Rows(0).Item("SYORI_PTN").ToString
            Case "1"
                'チェック
                Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_CHK2)         'SQL
            Case "2"
                '物流費データ作成
                Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_DATA2)         'SQL
        End Select

        Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_SQL2)         'SQL構築


        'パラメータ設定
        Call Me.SetUntinParameter(inTbl.Rows(0), Me._SqlPrmList)
        'Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI470IN").Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI470DAC", "Butsuryuhi_Rtn2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NET_AMT", "NET_AMT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI470OUTWK")

        Return ds

    End Function

    Private Function Butsuryuhi_Rtn3(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI470IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Select Case inTbl.Rows(0).Item("SYORI_PTN").ToString
            Case "1"
                'チェック
                Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_CHK3)         'SQL
            Case "2"
                '物流費データ作成
                Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_DATA3)         'SQL
        End Select

        Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_SQL3)         'SQL構築


        'パラメータ設定
        Call Me.SetUntinParameter(inTbl.Rows(0), Me._SqlPrmList)
        'Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI470IN").Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI470DAC", "Butsuryuhi_Rtn3", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NET_AMT", "NET_AMT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI470OUTWK")

        Return ds

    End Function

    Private Function Butsuryuhi_Rtn4(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI470IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Select Case inTbl.Rows(0).Item("SYORI_PTN").ToString
            Case "1"
                'チェック
                Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_CHK4)         'SQL
            Case "2"
                '物流費データ作成
                Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_DATA4)         'SQL
        End Select

        Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_SQL4)         'SQL構築

        'パラメータ設定
        Call Me.SetUntinParameter(inTbl.Rows(0), Me._SqlPrmList)
        'Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI470IN").Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        cmd.CommandTimeout = 6000       'ADD 2018/07/09 TIMEOUT対策

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI470DAC", "Butsuryuhi_Rtn4", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NET_AMT", "NET_AMT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI470OUTWK")

        Return ds

    End Function

    Private Function Butsuryuhi_Rtn5(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI470IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Select Case inTbl.Rows(0).Item("SYORI_PTN").ToString
            Case "1"
                'チェック
                Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_CHK5)         'SQL
            Case "2"
                '物流費データ作成
                Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_DATA5)         'SQL
        End Select

        Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_SQL5)         'SQL構築


        'パラメータ設定
        Call Me.SetUntinParameter(inTbl.Rows(0), Me._SqlPrmList)
        'Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI470IN").Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI470DAC", "Butsuryuhi_Rtn5", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NET_AMT", "NET_AMT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI470OUTWK")

        Return ds

    End Function

    Private Function Butsuryuhi_Rtn6(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI470IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        Select Case inTbl.Rows(0).Item("SYORI_PTN").ToString
            Case "1"
                'チェック
                Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_CHK6)         'SQL
            Case "2"
                '物流費データ作成
                Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_DATA6)         'SQL
        End Select

        Me._StrSql.Append(LMI470DAC.SQL_BUTSURYUHI_SQL6)         'SQL構築

        'パラメータ設定
        Call Me.SetUntinParameter(inTbl.Rows(0), Me._SqlPrmList)
        'Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI470IN").Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI470DAC", "Butsuryuhi_Rtn6", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NET_AMT", "NET_AMT")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI470OUTWK")

        Return ds

    End Function

#End Region

#Region "SQL条件設定 共通"

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter(prmList, dataSetNm)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))

    End Sub

    ''''' <summary>
    ''''' パラメータ設定モジュール(システム共通項目(登録時))
    ''''' </summary>
    ''''' <remarks></remarks>
    ''Private Sub SetParamCommonSystemIns()

    ''    'パラメータ設定
    ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
    ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
    ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
    ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
    ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
    ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
    ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
    ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
    ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

    ''End Sub

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String, ByVal mainBrCd As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        'デュポン業務主営業所トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN_DPN$", MyBase.GetDatabaseName(mainBrCd, DBKbn.TRN))

        Return sql

    End Function

    ''' <summary>
    ''' 運賃データの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUntinParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@S_DATE", .Item("DATE_FROM").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@E_DATE", .Item("DATE_TO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KEIJYO ", Left(.Item("DATE_TO").ToString(), 6), DBDataType.CHAR))

        End With

    End Sub


#End Region

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E483", New String() {"更新"}) '2013.03.07エラーメッセージ変更
            Return False
        End If

        Return True

    End Function

#End Region

End Class
