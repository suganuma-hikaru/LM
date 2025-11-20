' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷管理
'  プログラムID     :  LMB540    : 入荷確定入力モニター表
'  作  成  者       :  [Ri]
'  第二作成者       :  [!NoUser!]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB540DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB540DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                        " & vbNewLine _
                                            & "	INL.NRS_BR_CD                                            AS NRS_BR_CD " & vbNewLine _
                                            & ",'AY'                                                     AS PTN_ID    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD                      " & vbNewLine _
                                            & "		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD                     " & vbNewLine _
                                            & "	 	  ELSE MR3.PTN_CD END                                AS PTN_CD    " & vbNewLine _
                                            & ",CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                      " & vbNewLine _
                                            & "  		  WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                 " & vbNewLine _
                                            & "		  ELSE MR3.RPT_ID END                                AS RPT_ID    " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                              							                    " & vbNewLine _
                                         & "    CASE    WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID                                 " & vbNewLine _
                                         & "            WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID                      	        " & vbNewLine _
                                         & "            ELSE MR3.RPT_ID END                    AS RPT_ID         	                " & vbNewLine _
                                         & "    ,INL.NRS_BR_CD                                 AS NRS_BR_CD                         " & vbNewLine _
                                         & "    ,INL.INKA_DATE                                 AS INKA_DATE                         " & vbNewLine _
                                         & "    ,INL.INKA_NO_L                                 AS INKA_NO_L                         " & vbNewLine _
                                         & "    ,INL.OUTKA_FROM_ORD_NO_L                       AS OUTKA_FROM_ORD_NO_L               " & vbNewLine _
                                         & "    ,INL.CUST_CD_L                                 AS CUST_CD_L                         " & vbNewLine _
                                         & "    ,INL.CUST_CD_M                                 AS CUST_CD_M                         " & vbNewLine _
                                         & "    ,CUST.CUST_NM_L                                AS CUST_NM_L                         " & vbNewLine _
                                         & "    ,MNB.NRS_BR_NM                                 AS NRS_BR_NM                         " & vbNewLine _
                                         & "    ,MG.GOODS_NM_1                                 AS GOODS_NM_1                        " & vbNewLine _
                                         & "    ,INS.LOT_NO                                    AS LOT_NO                            " & vbNewLine _
                                         & "      --     ,ROUND(SUM(ISNULL(INS.KONSU , 0)                                           " & vbNewLine _
                                         & "      --      * ISNULL(MG.PKG_NB , 0)                                                   " & vbNewLine _
                                         & "      --      + ISNULL(INS.HASU  , 0))                                                  " & vbNewLine _
                                         & "      --      / ISNULL(MG.PKG_NB, 1), 0, 1)             AS KONSU                        " & vbNewLine _
                                         & "      --  ,SUM(ISNULL(INS.KONSU , 0)                                                    " & vbNewLine _
                                         & "      --   * ISNULL(MG.PKG_NB , 0)                                                      " & vbNewLine _
                                         & "      --   + ISNULL(INS.HASU  , 0))                                                     " & vbNewLine _
                                         & "      --   % ISNULL(MG.PKG_NB, 1)                       AS HASU                         " & vbNewLine _
                                         & "      --  ,MG.PKG_NB                                    AS PKG_NB                       " & vbNewLine _
                                         & "    ,SUM(ISNULL(INS.KONSU , 0)                                                          " & vbNewLine _
                                         & "    * ISNULL(MG.PKG_NB , 0)                                 	                        " & vbNewLine _
                                         & "    + ISNULL(INS.HASU  , 0)) 	                    AS NB                               " & vbNewLine _
                                         & "   	,CASE   WHEN INS.GOODS_COND_KB_1=''                                                 " & vbNewLine _
                                         & "            AND INS.GOODS_COND_KB_2='' THEN '0'     		    	                    " & vbNewLine _
                                         & "            ELSE '1'                                    			                    " & vbNewLine _
                                         & "	 END                                             AS GOODS_COND 	                    " & vbNewLine _
                                         & "      --   ,@INKA_DATE_FROM                             AS INKA_DATE_FROM               " & vbNewLine _
                                         & "      --   ,@INKA_DATE_TO                               AS INKA_DATE_TO                 " & vbNewLine _
                                         & "      --【修】数量をSUMする -- START --                           	                    " & vbNewLine _
                                         & "      --	  ,(ISNULL(INS.KONSU, 0)                                                    " & vbNewLine _
                                         & "      --   ,SUM((ISNULL(INS.KONSU, 0)                                                   " & vbNewLine _
                                         & "      --    * ISNULL(MG.PKG_NB, 0)                                                      " & vbNewLine _
                                         & "      --    + ISNULL(INS.HASU, 0))                                                      " & vbNewLine _
                                         & "      --    * ISNULL(INS.IRIME, 0))                     AS SURYO           	            " & vbNewLine _
                                         & "      --【修】数量をSUMする --  END  --                            	                    " & vbNewLine _
                                         & "      --(出荷元のスイッチ格納) -- START --                                              " & vbNewLine _
                                         & "    ,CASE   WHEN EDIL.INKA_CTL_NO_L IS NOT NULL THEN ISNULL(DESTEDI.DEST_NM,'')  	    " & vbNewLine _
                                         & "            ELSE ISNULL(DESTL.DEST_NM,'')                                       		" & vbNewLine _
                                         & "     END                                          AS OUTKA_MOTO                         " & vbNewLine _
                                         & " --(出荷元のスイッチ格納) --  END  --                          			                " & vbNewLine _
                                         & " -- ,MSO.WH_NM                                     AS WH_NM               			   	" & vbNewLine _
                                         & "    ,MG.GOODS_CD_CUST                              AS GOODS_CD_CUST       				" & vbNewLine _
                                         & " --追記(ベースにない項目類)															    " & vbNewLine _
                                         & " 	,''									            AS GYO --行番号格納用フィールド		" & vbNewLine _
                                         & " 	,CUST.PIC                                       AS PIC								" & vbNewLine _
                                         & " 	,DESTL.DEST_CD                                  AS DEST_CD --暫定フィールド			" & vbNewLine _
                                         & " 	,INM.INKA_NO_M                                  AS INKA_NO_M						" & vbNewLine _
                                         & " 	,EDIM.FREE_N09                                  AS FREE_N09						    " & vbNewLine


    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = "FROM                                        " & vbNewLine _
                                      & "--入荷L                                                                " & vbNewLine _
                                      & "$LM_TRN$..B_INKA_L INL                                                 " & vbNewLine _
                                      & "--入荷M                                                                " & vbNewLine _
                                      & "LEFT join $LM_TRN$..B_INKA_M INM                                       " & vbNewLine _
                                      & "ON  INL.NRS_BR_CD = INM.NRS_BR_CD                                      " & vbNewLine _
                                      & "AND INL.INKA_NO_L = INM.INKA_NO_L                                      " & vbNewLine _
                                      & "AND INM.SYS_DEL_FLG  = '0'                                             " & vbNewLine _
                                      & "--入荷S                                                                " & vbNewLine _
                                      & "LEFT join $LM_TRN$..B_INKA_S INS                                       " & vbNewLine _
                                      & "ON  INM.NRS_BR_CD = INS.NRS_BR_CD                                      " & vbNewLine _
                                      & "AND INM.INKA_NO_L = INS.INKA_NO_L                                      " & vbNewLine _
                                      & "AND INM.INKA_NO_M = INS.INKA_NO_M                                      " & vbNewLine _
                                      & "AND INS.SYS_DEL_FLG  = '0'                                             " & vbNewLine _
                                      & "   --(2012.06.14) LMB524対応 出荷元出力 -- START --                    " & vbNewLine _
                                      & "   --入荷EDIL                                                          " & vbNewLine _
                                      & "   LEFT JOIN (                                                         " & vbNewLine _
                                      & "               SELECT NRS_BR_CD                                        " & vbNewLine _
                                      & "                    , INKA_CTL_NO_L                                    " & vbNewLine _
                                      & "                    , CUST_CD_L                                        " & vbNewLine _
                                      & "                    , OUTKA_MOTO                                       " & vbNewLine _
                                      & "                    , SYS_DEL_FLG                                      " & vbNewLine _
                                      & "                 FROM $LM_TRN$..H_INKAEDI_L                            " & vbNewLine _
                                      & "                GROUP BY                                               " & vbNewLine _
                                      & "                      NRS_BR_CD                                        " & vbNewLine _
                                      & "                    , INKA_CTL_NO_L                                    " & vbNewLine _
                                      & "                    , CUST_CD_L                                        " & vbNewLine _
                                      & "                    , OUTKA_MOTO                                       " & vbNewLine _
                                      & "                    , SYS_DEL_FLG                                      " & vbNewLine _
                                      & "              ) EDIL                                                   " & vbNewLine _
                                      & "          ON EDIL.NRS_BR_CD     = INL.NRS_BR_CD                        " & vbNewLine _
                                      & "         AND EDIL.INKA_CTL_NO_L = INL.INKA_NO_L                        " & vbNewLine _
                                      & "         AND EDIL.SYS_DEL_FLG   = '0'                                  " & vbNewLine _
                                      & "   --入荷EDIM                                                          " & vbNewLine _
                                      & "   LEFT JOIN (                                                         " & vbNewLine _
                                      & "               SELECT NRS_BR_CD                                        " & vbNewLine _
                                      & "                    , INKA_CTL_NO_L                                    " & vbNewLine _
                                      & "                    , INKA_CTL_NO_M                                    " & vbNewLine _
                                      & "                    , FREE_N09                                         " & vbNewLine _
                                      & "                    , SYS_DEL_FLG                                      " & vbNewLine _
                                      & "                 FROM $LM_TRN$..H_INKAEDI_M                            " & vbNewLine _
                                      & "                GROUP BY                                               " & vbNewLine _
                                      & "                      NRS_BR_CD                                        " & vbNewLine _
                                      & "                    , INKA_CTL_NO_L                                    " & vbNewLine _
                                      & "                    , INKA_CTL_NO_M                                    " & vbNewLine _
                                      & "                    , FREE_N09                                         " & vbNewLine _
                                      & "                    , SYS_DEL_FLG                                      " & vbNewLine _
                                      & "              ) EDIM                                                   " & vbNewLine _
                                      & "          ON EDIM.NRS_BR_CD     = INL.NRS_BR_CD                        " & vbNewLine _
                                      & "         AND EDIM.INKA_CTL_NO_L = INL.INKA_NO_L                        " & vbNewLine _
                                      & "         AND EDIM.INKA_CTL_NO_M = INM.INKA_NO_M                        " & vbNewLine _
                                      & "         AND EDIM.SYS_DEL_FLG   = '0'                                  " & vbNewLine _
                                      & "   --運送L                                                             " & vbNewLine _
                                      & "   LEFT JOIN $LM_TRN$..F_UNSO_L UL                                     " & vbNewLine _
                                      & "          ON INL.NRS_BR_CD   = UL.NRS_BR_CD                            " & vbNewLine _
                                      & "         AND INL.INKA_NO_L   = UL.INOUTKA_NO_L                         " & vbNewLine _
                                      & "         AND UL.MOTO_DATA_KB = '10'                                    " & vbNewLine _
                                      & "         AND UL.SYS_DEL_FLG  = '0'                                     " & vbNewLine _
                                      & "   --届先M（出荷元取得入荷L参照）                                      " & vbNewLine _
                                      & "   LEFT JOIN $LM_MST$..M_DEST DESTL                                    " & vbNewLine _
                                      & "          ON DESTL.NRS_BR_CD = INL.NRS_BR_CD                           " & vbNewLine _
                                      & "         AND DESTL.CUST_CD_L = INL.CUST_CD_L                           " & vbNewLine _
                                      & "         AND DESTL.DEST_CD   = UL.ORIG_CD                              " & vbNewLine _
                                      & "   --届先M（出荷元取得EDIL参照）                                       " & vbNewLine _
                                      & "   LEFT JOIN $LM_MST$..M_DEST DESTEDI                                  " & vbNewLine _
                                      & "          ON DESTEDI.NRS_BR_CD = EDIL.NRS_BR_CD                        " & vbNewLine _
                                      & "         AND DESTEDI.CUST_CD_L = EDIL.CUST_CD_L                        " & vbNewLine _
                                      & "         AND DESTEDI.DEST_CD   = EDIL.OUTKA_MOTO                       " & vbNewLine _
                                      & "   --(2012.06.14) LMB524対応 出荷元出力 --  END  --                    " & vbNewLine _
                                      & "--日陸営業所マスタ                                                     " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_NRS_BR MNB                                       " & vbNewLine _
                                      & "ON  MNB.NRS_BR_CD    = INL.NRS_BR_CD                                   " & vbNewLine _
                                      & "--倉庫マスタ                                                           " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_SOKO MSO                                         " & vbNewLine _
                                      & "ON  MSO.WH_CD = INL.WH_CD                                              " & vbNewLine _
                                      & "--商品マスタ                                                           " & vbNewLine _
                                      & "LEFT join $LM_MST$..M_GOODS MG                                         " & vbNewLine _
                                      & "ON  INM.NRS_BR_CD    = MG.NRS_BR_CD                                    " & vbNewLine _
                                      & "AND INM.GOODS_CD_NRS = MG.GOODS_CD_NRS                                 " & vbNewLine _
                                      & " --荷主マスタ                                                          " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_CUST CUST                                        " & vbNewLine _
                                      & "ON  CUST.NRS_BR_CD    = MG.NRS_BR_CD                                   " & vbNewLine _
                                      & "AND CUST.CUST_CD_L    = MG.CUST_CD_L                                   " & vbNewLine _
                                      & "AND CUST.CUST_CD_M    = MG.CUST_CD_M                                   " & vbNewLine _
                                      & "AND CUST.CUST_CD_S    = MG.CUST_CD_S                                   " & vbNewLine _
                                      & "AND CUST.CUST_CD_SS   = MG.CUST_CD_SS                                  " & vbNewLine _
                                      & "--入荷Lでの荷主帳票パターン取得                                        " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                                    " & vbNewLine _
                                      & "ON  INL.NRS_BR_CD = MCR1.NRS_BR_CD                                     " & vbNewLine _
                                      & "AND INL.CUST_CD_L = MCR1.CUST_CD_L                                     " & vbNewLine _
                                      & "AND INL.CUST_CD_M = MCR1.CUST_CD_M                                     " & vbNewLine _
                                      & "AND '00' = MCR1.CUST_CD_S                                              " & vbNewLine _
                                      & "--AND MCR1.PTN_ID = '01'                                               " & vbNewLine _
                                      & "AND MCR1.PTN_ID = 'AY'                                                 " & vbNewLine _
                                      & "--帳票パターン取得                                                     " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_RPT MR1                                          " & vbNewLine _
                                      & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                                     " & vbNewLine _
                                      & "AND MR1.PTN_ID = MCR1.PTN_ID                                           " & vbNewLine _
                                      & "AND MR1.PTN_CD = MCR1.PTN_CD                                           " & vbNewLine _
                                      & "AND MR1.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                      & "--商品Mの荷主での荷主帳票パターン取得                                  " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                                    " & vbNewLine _
                                      & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                                      " & vbNewLine _
                                      & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                                      " & vbNewLine _
                                      & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                                      " & vbNewLine _
                                      & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                                      " & vbNewLine _
                                      & "AND MCR2.PTN_ID = 'AY'                                                 " & vbNewLine _
                                      & "--帳票パターン取得                                                     " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_RPT MR2                                          " & vbNewLine _
                                      & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                                     " & vbNewLine _
                                      & "AND MR2.PTN_ID = MCR2.PTN_ID                                           " & vbNewLine _
                                      & "AND MR2.PTN_CD = MCR2.PTN_CD                                           " & vbNewLine _
                                      & "AND MR2.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                      & "--存在しない場合の帳票パターン取得                                     " & vbNewLine _
                                      & "LEFT JOIN $LM_MST$..M_RPT MR3                                          " & vbNewLine _
                                      & "ON  MR3.NRS_BR_CD = INL.NRS_BR_CD                                      " & vbNewLine _
                                      & "AND MR3.PTN_ID = 'AY'                                                  " & vbNewLine _
                                      & "AND MR3.STANDARD_FLAG = '01'                                           " & vbNewLine _
                                      & "AND MR3.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                      & "   --商品状態中身(区分1)                                               " & vbNewLine _
                                      & "   LEFT JOIN  $LM_MST$..Z_KBN KBN_1                                    " & vbNewLine _
                                      & "   ON                                                                  " & vbNewLine _
                                      & "    KBN_1.KBN_GROUP_CD = 'S005'                                        " & vbNewLine _
                                      & "   AND                                                                 " & vbNewLine _
                                      & "    KBN_1.KBN_CD      = INS.GOODS_COND_KB_1                            " & vbNewLine _
                                      & "   --商品状態外観(区分2)                                               " & vbNewLine _
                                      & "   LEFT JOIN  $LM_MST$..Z_KBN KBN_2                                    " & vbNewLine _
                                      & "   ON                                                                  " & vbNewLine _
                                      & "    KBN_2.KBN_GROUP_CD = 'S006'                                        " & vbNewLine _
                                      & "   AND                                                                 " & vbNewLine _
                                      & "    KBN_2.KBN_CD      = INS.GOODS_COND_KB_2                            " & vbNewLine _
                                      & "          --★T                                                        " & vbNewLine _
                                      & "          --F01 F_UNSO_L(運送L)                                        " & vbNewLine _
                                      & "          LEFT JOIN $LM_TRN$..F_UNSO_L UNSO                            " & vbNewLine _
                                      & "          ON  INL.NRS_BR_CD  =  UNSO.NRS_BR_CD                         " & vbNewLine _
                                      & "          AND INL.INKA_NO_L  =  UNSO.INOUTKA_NO_L                      " & vbNewLine _
                                      & "          --元データ区分を条件に追加 -- START --                       " & vbNewLine _
                                      & "          AND UNSO.MOTO_DATA_KB ='10'                                  " & vbNewLine _
                                      & "          --元データ区分を条件に追加 --  END  --                       " & vbNewLine _
                                      & "          AND UNSO.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                      & "          --届先マスタは使用しない為、コメントとする -- START --       " & vbNewLine _
                                      & "          --M10 M_DEST(届先マスタ)                                     " & vbNewLine _
                                      & "          --LEFT JOIN $LM_MST$..M_DEST DEST                            " & vbNewLine _
                                      & "          --ON  DEST.DEST_CD   =  UNSO.ORIG_CD                         " & vbNewLine _
                                      & "          --AND DEST.SYS_DEL_FLG = '0'                                 " & vbNewLine _
                                      & "          --届先マスタは使用しない為、コメントとする --  END  --       " & vbNewLine _
                                      & "          --★                                                         " & vbNewLine _
                                      & "   WHERE INL.NRS_BR_CD  = @NRS_BR_CD                                   " & vbNewLine _
                                      & "    AND INL.SYS_DEL_FLG  = '0'                                         " & vbNewLine _
                                      & "                                                                       " & vbNewLine


    ''' <summary>
    ''' LMB540用データ抽出用GROUP BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_GROUP_BY As String = " GROUP BY                                                     " & vbNewLine _
                                         & "   MR1.PTN_CD       						  				  " & vbNewLine _
                                         & " , MR2.PTN_CD                                                 " & vbNewLine _
                                         & " , MR1.RPT_ID                                                 " & vbNewLine _
                                         & " , MR2.RPT_ID                                                 " & vbNewLine _
                                         & " , MR3.RPT_ID                                                 " & vbNewLine _
                                         & " , INL.INKA_DATE                                              " & vbNewLine _
                                         & " , INL.NRS_BR_CD                                              " & vbNewLine _
                                         & " , INL.INKA_NO_L                                              " & vbNewLine _
                                         & " , INL.OUTKA_FROM_ORD_NO_L                                    " & vbNewLine _
                                         & " , INL.CUST_CD_L                                              " & vbNewLine _
                                         & " , INL.CUST_CD_M                                              " & vbNewLine _
                                         & " , CUST.CUST_NM_L                                             " & vbNewLine _
                                         & " , CUST.CUST_NM_M											  " & vbNewLine _
                                         & " , MNB.NRS_BR_NM                                              " & vbNewLine _
                                         & " , MNB.AD_1                                                   " & vbNewLine _
                                         & " , MNB.AD_2                                                   " & vbNewLine _
                                         & " , MSO.WH_KB                                                  " & vbNewLine _
                                         & " , MSO.AD_1                                                   " & vbNewLine _
                                         & " , MSO.AD_2                                                   " & vbNewLine _
                                         & " , MG.GOODS_CD_NRS                                            " & vbNewLine _
                                         & " , MG.GOODS_NM_1                                              " & vbNewLine _
                                         & " , INS.LOT_NO                                                 " & vbNewLine _
                                         & " , INS.SERIAL_NO                                              " & vbNewLine _
                                         & " , INS.IRIME                                                  " & vbNewLine _
                                         & " , MG.STD_IRIME_UT                                            " & vbNewLine _
                                         & " , MG.PKG_NB                                                  " & vbNewLine _
                                         & " , MG.PKG_UT                                                  " & vbNewLine _
                                         & " , MG.NB_UT                                                   " & vbNewLine _
                                         & " , KBN_1.KBN_NM1                                              " & vbNewLine _
                                         & " , KBN_2.KBN_NM1                                              " & vbNewLine _
                                         & " , INS.REMARK_OUT                                             " & vbNewLine _
                                         & " , INS.LT_DATE                                                " & vbNewLine _
                                         & " , INM.PRINT_SORT                                             " & vbNewLine _
                                         & " , INM.INKA_NO_M                                              " & vbNewLine _
                                         & " , UNSO.ORIG_CD                                               " & vbNewLine _
                                         & " , INS.IRIME                                                  " & vbNewLine _
                                         & " , MG.PKG_NB                                                  " & vbNewLine _
                                         & " , MG.GOODS_NM_2                                              " & vbNewLine _
                                         & " , MG.GOODS_NM_3                                              " & vbNewLine _
                                         & " , EDIL.INKA_CTL_NO_L                                         " & vbNewLine _
                                         & " , DESTEDI.DEST_NM                                            " & vbNewLine _
                                         & " , DESTL.DEST_NM                                              " & vbNewLine _
                                         & " , DESTL.DEST_CD                                              " & vbNewLine _
                                         & " , MSO.WH_NM                                                  " & vbNewLine _
                                         & " , MG.GOODS_CD_NRS                                            " & vbNewLine _
                                         & " , MG.GOODS_CD_CUST  										  " & vbNewLine _
                                         & " , CUST.PIC    												  " & vbNewLine _
                                         & " , UNSO.DEST_CD 											  " & vbNewLine _
                                         & " , GOODS_COND_KB_1											  " & vbNewLine _
                                         & " , GOODS_COND_KB_2                                   		  " & vbNewLine _
                                         & " , FREE_N09                                   		          " & vbNewLine

    ''' <summary>
    ''' 帳票ID抽出FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM_Mprt As String = "FROM                                                     " & vbNewLine _
                                          & "--入荷L                                                  " & vbNewLine _
                                          & "$LM_TRN$..B_INKA_L INL                                   " & vbNewLine _
                                          & "LEFT join $LM_TRN$..B_INKA_M INM                         " & vbNewLine _
                                          & "ON  INL.NRS_BR_CD = INM.NRS_BR_CD                        " & vbNewLine _
                                          & "AND  INL.INKA_NO_L = INM.INKA_NO_L                       " & vbNewLine _
                                          & "--入荷M                                                  " & vbNewLine _
                                          & "LEFT join $LM_TRN$..B_INKA_S INS                         " & vbNewLine _
                                          & "ON  INM.NRS_BR_CD = INS.NRS_BR_CD                        " & vbNewLine _
                                          & "AND  INM.INKA_NO_L = INS.INKA_NO_L                       " & vbNewLine _
                                          & "AND INM.INKA_NO_M = INS.INKA_NO_M                        " & vbNewLine _
                                          & "AND INM.SYS_DEL_FLG  = '0'                               " & vbNewLine _
                                          & "--入荷S                                                  " & vbNewLine _
                                          & "LEFT join $LM_MST$..M_GOODS MG                           " & vbNewLine _
                                          & "ON  INM.NRS_BR_CD = MG.NRS_BR_CD                         " & vbNewLine _
                                          & "AND  INM.GOODS_CD_NRS = MG.GOODS_CD_NRS                  " & vbNewLine _
                                          & "AND INS.SYS_DEL_FLG  = '0'                               " & vbNewLine _
                                          & "--運送L                                                  " & vbNewLine _
                                          & "LEFT join $LM_TRN$..F_UNSO_L UL                          " & vbNewLine _
                                          & "ON  INL.NRS_BR_CD = UL.NRS_BR_CD                         " & vbNewLine _
                                          & "AND  INL.INKA_NO_L = UL.INOUTKA_NO_L                     " & vbNewLine _
                                          & "AND  UL.MOTO_DATA_KB = '10'                              " & vbNewLine _
                                          & "AND UL.SYS_DEL_FLG  = '0'                                " & vbNewLine _
                                          & "   --(2012.06.14) LMB524対応 出荷元出力 -- START --      " & vbNewLine _
                                          & "   --入荷EDIL                                            " & vbNewLine _
                                          & "   LEFT JOIN (                                           " & vbNewLine _
                                          & "               SELECT NRS_BR_CD                          " & vbNewLine _
                                          & "                    , INKA_CTL_NO_L                      " & vbNewLine _
                                          & "                    , CUST_CD_L                          " & vbNewLine _
                                          & "                    , OUTKA_MOTO                         " & vbNewLine _
                                          & "                    , SYS_DEL_FLG                        " & vbNewLine _
                                          & "                 FROM $LM_TRN$..H_INKAEDI_L              " & vbNewLine _
                                          & "                GROUP BY                                 " & vbNewLine _
                                          & "                      NRS_BR_CD                          " & vbNewLine _
                                          & "                    , INKA_CTL_NO_L                      " & vbNewLine _
                                          & "                    , CUST_CD_L                          " & vbNewLine _
                                          & "                    , OUTKA_MOTO                         " & vbNewLine _
                                          & "                    , SYS_DEL_FLG                        " & vbNewLine _
                                          & "              ) EDIL                                     " & vbNewLine _
                                          & "          ON EDIL.NRS_BR_CD     = INL.NRS_BR_CD          " & vbNewLine _
                                          & "         AND EDIL.INKA_CTL_NO_L = INL.INKA_NO_L          " & vbNewLine _
                                          & "         AND EDIL.SYS_DEL_FLG   = '0'                    " & vbNewLine _
                                          & "   --届先M（出荷元取得入荷L参照）                        " & vbNewLine _
                                          & "   LEFT JOIN $LM_MST$..M_DEST DESTL                      " & vbNewLine _
                                          & "          ON DESTL.NRS_BR_CD = INL.NRS_BR_CD             " & vbNewLine _
                                          & "         AND DESTL.CUST_CD_L = INL.CUST_CD_L             " & vbNewLine _
                                          & "         AND DESTL.DEST_CD   = UL.ORIG_CD                " & vbNewLine _
                                          & "   --届先M（出荷元取得EDIL参照）                         " & vbNewLine _
                                          & "   LEFT JOIN $LM_MST$..M_DEST DESTEDI                    " & vbNewLine _
                                          & "          ON DESTEDI.NRS_BR_CD = EDIL.NRS_BR_CD          " & vbNewLine _
                                          & "         AND DESTEDI.CUST_CD_L = EDIL.CUST_CD_L          " & vbNewLine _
                                          & "         AND DESTEDI.DEST_CD   = EDIL.OUTKA_MOTO         " & vbNewLine _
                                          & "   --(2012.06.14) LMB524対応 出荷元出力 --  END  --      " & vbNewLine _
                                          & "--入荷Lでの荷主帳票パターン取得                          " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR1                      " & vbNewLine _
                                          & "ON  INL.NRS_BR_CD = MCR1.NRS_BR_CD                       " & vbNewLine _
                                          & "AND INL.CUST_CD_L = MCR1.CUST_CD_L                       " & vbNewLine _
                                          & "AND INL.CUST_CD_M = MCR1.CUST_CD_M                       " & vbNewLine _
                                          & "AND '00' = MCR1.CUST_CD_S                                " & vbNewLine _
                                          & "AND MCR1.PTN_ID = 'AY'                                   " & vbNewLine _
                                          & "--帳票パターン取得                                       " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_RPT MR1                            " & vbNewLine _
                                          & "ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD                       " & vbNewLine _
                                          & "AND MR1.PTN_ID = MCR1.PTN_ID                             " & vbNewLine _
                                          & "AND MR1.PTN_CD = MCR1.PTN_CD                             " & vbNewLine _
                                          & "AND MR1.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                          & "--商品Mの荷主での荷主帳票パターン取得                    " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_CUST_RPT MCR2                      " & vbNewLine _
                                          & "ON  MG.NRS_BR_CD = MCR2.NRS_BR_CD                        " & vbNewLine _
                                          & "AND MG.CUST_CD_L = MCR2.CUST_CD_L                        " & vbNewLine _
                                          & "AND MG.CUST_CD_M = MCR2.CUST_CD_M                        " & vbNewLine _
                                          & "AND MG.CUST_CD_S = MCR2.CUST_CD_S                        " & vbNewLine _
                                          & "AND MCR2.PTN_ID = 'AY'                                   " & vbNewLine _
                                          & "--帳票パターン取得                                       " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_RPT MR2                            " & vbNewLine _
                                          & "ON  MR2.NRS_BR_CD = MCR2.NRS_BR_CD                       " & vbNewLine _
                                          & "AND MR2.PTN_ID = MCR2.PTN_ID                             " & vbNewLine _
                                          & "AND MR2.PTN_CD = MCR2.PTN_CD                             " & vbNewLine _
                                          & "AND MR2.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                          & "--存在しない場合の帳票パターン取得                       " & vbNewLine _
                                          & "LEFT JOIN $LM_MST$..M_RPT MR3                            " & vbNewLine _
                                          & "ON  MR3.NRS_BR_CD = INL.NRS_BR_CD                        " & vbNewLine _
                                          & "AND MR3.PTN_ID = 'AY'                                    " & vbNewLine _
                                          & "AND MR3.STANDARD_FLAG = '01'                             " & vbNewLine _
                                          & "AND MR3.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                          & "WHERE INL.NRS_BR_CD  = @NRS_BR_CD                  	  " & vbNewLine _
                                          & "AND INL.SYS_DEL_FLG = '0'                         	      " & vbNewLine


    ''' <summary>
    ''' LMB540用ORDER BY（①入荷日、②管理番号）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY                   " & vbNewLine _
                                        & " INL.OUTKA_FROM_ORD_NO_L     " & vbNewLine _
                                        & " , INL.INKA_DATE             " & vbNewLine _
                                        & " , INL.INKA_NO_L             " & vbNewLine _
                                        & " , INM.INKA_NO_M             " & vbNewLine _
                                        & " , INS.LOT_NO                " & vbNewLine



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
        Dim inTbl As DataTable = ds.Tables("LMB540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB540DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)
        Me._StrSql.Append(LMB540DAC.SQL_FROM_Mprt)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL_01()                   '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB540DAC", "SelectMPrt", cmd)

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
    ''' 入荷データLテーブル対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB540IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB540DAC.SQL_SELECT_DATA)        'SQL構築(データ抽出用Select句)

        Me._StrSql.Append(LMB540DAC.SQL_FROM)               'SQL構築(データ抽出用From句)

        Call Me.SetConditionMasterSQL()                     'SQL構築(条件設定)

        Me._StrSql.Append(LMB540DAC.SQL_GROUP_BY)           'SQL構築(データ抽出用GROUP BY句)

        Me._StrSql.Append(LMB540DAC.SQL_ORDER_BY)           'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB540DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("INKA_NO_L", "INKA_NO_L")
        map.Add("OUTKA_FROM_ORD_NO_L", "OUTKA_FROM_ORD_NO_L")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("GOODS_COND", "GOODS_COND")
        'map.Add("GOODS_COND_KB_2", "GOODS_COND_KB_2")
        map.Add("OUTKA_MOTO", "OUTKA_MOTO")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GYO", "GYO")
        map.Add("PIC", "PIC")
        'map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("INKA_NO_M", "INKA_NO_M")
        map.Add("NB", "NB")
        'map.Add("IRIME", "IRIME")
        map.Add("FREE_N09", "FREE_N09") '--2012/12/25.追加完了--'

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB540OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL_01()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '出荷管理番号L
            whereStr = .Item("INKA_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INL.INKA_NO_L   = @INKA_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INL.CUST_CD_L   LIKE '%' + @CUST_CD_L + '%'")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INL.CUST_CD_M   LIKE '%' + @CUST_CD_M + '%'")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

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

            '2012/12/10現在LMB030での使用はないのでコメントアウト

            'If (.Item("PGID").ToString()) = "LMB030" Then
            '    '振替含むにチェックあり。
            '    If String.IsNullOrEmpty(.Item("INKA_KB").ToString()) = False Then
            '        '追加条件なし
            '    Else
            '        'INKA_KB <> '50'(振替) の条件追加
            '        '(2012.02.28) "振替"は入荷データ区分(INKA_KB)ではなく、入荷データ種別(INKA_TP)で指定されている。
            '        'Me._StrSql.Append(" AND INL.INKA_KB <> '50' ")
            '        Me._StrSql.Append(" AND INL.INKA_TP <> '50' ")
            '        Me._StrSql.Append(vbNewLine)
            '    End If
            'End If

            '入荷日(FROM/TO)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_FROM", .Item("INKA_DATE_FROM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_TO", .Item("INKA_DATE_TO").ToString(), DBDataType.CHAR))

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '入荷管理番号L
            whereStr = .Item("INKA_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INL.INKA_NO_L   = @INKA_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INL.CUST_CD_L   LIKE '%' + @CUST_CD_L + '%'")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INL.CUST_CD_M   LIKE '%' + @CUST_CD_M + '%'")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '入荷日From
            whereStr = .Item("INKA_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INL.INKA_DATE   >= @INKA_DATE_FROM_2")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_FROM_2", whereStr, DBDataType.CHAR))
            End If

            '入荷日To
            whereStr = .Item("INKA_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INL.INKA_DATE   <= @INKA_DATE_TO_2")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_TO_2", whereStr, DBDataType.CHAR))
            End If

            'データ登録日
            whereStr = .Item("SYS_ENT_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND INL.SYS_ENT_DATE = @SYS_ENT_DATE")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", whereStr, DBDataType.CHAR))
            End If

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


#End Region

End Class


