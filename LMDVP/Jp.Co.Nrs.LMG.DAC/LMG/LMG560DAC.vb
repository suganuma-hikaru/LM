' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG560DAC : デュポン請求鑑
'  作  成  者       :  [篠原]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const

Public Class LMG560DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 出力対象帳票パターン取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = "SELECT DISTINCT                                                                   " & vbNewLine _
                                            & " SEKY.NRS_BR_CD               AS NRS_BR_CD                                  " & vbNewLine _
                                            & ", '76'                      AS PTN_ID                                     " & vbNewLine _
                                            & ",CASE WHEN WCUST.PTN_CD IS NOT NULL AND WCUST.PTN_CD <> '' THEN WCUST.PTN_CD			    " & vbNewLine _
                                            & "      ELSE MR2.PTN_CD END                            AS PTN_CD	    " & vbNewLine _
                                            & ",CASE WHEN WCUST.RPT_ID IS NOT NULL AND WCUST.PTN_CD <> ''THEN WCUST.RPT_ID	            " & vbNewLine _
                                            & "	     ELSE MR2.RPT_ID END                            AS RPT_ID        " & vbNewLine

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks>
    ''' 20120123追加
    '''</remarks>
    Private Const SQL_FROM_MPrt As String = "FROM                                                                   " & vbNewLine _
                                          & "	--デュポン請求GLマスタ			                                    " & vbNewLine _
                                          & "	$LM_TRN$..G_DUPONT_SEKY_GL SEKY			                                " & vbNewLine _
                                          & "	LEFT JOIN			                                                " & vbNewLine _
                                          & "	(SELECT			                                                    " & vbNewLine _
                                          & "		 MCD.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
                                          & "		,RIGHT('00' + MCD.SET_NAIYO,2) AS DEPART		                                " & vbNewLine _
                                          & "		,CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD			    " & vbNewLine _
                                          & "		ELSE '' END                            AS PTN_CD	    " & vbNewLine _
                                          & "		,CASE WHEN MR1.RPT_ID IS NOT NULL THEN MR1.RPT_ID	            " & vbNewLine _
                                          & "		ELSE '' END                            AS RPT_ID        " & vbNewLine _
                                          & "		FROM		                                                    " & vbNewLine _
                                          & "		--荷主明細マスタ		                                        " & vbNewLine _
                                          & "		$LM_MST$..M_CUST_DETAILS MCD		                                " & vbNewLine _
                                          & "		INNER JOIN $LM_MST$..M_CUST MC		                            " & vbNewLine _
                                          & "		ON MC.NRS_BR_CD = MCD.NRS_BR_CD		                            " & vbNewLine _
                                          & "		AND MC.CUST_CD_L  = SUBSTRING(MCD.CUST_CD,1,5)		            " & vbNewLine _
                                          & "		AND MC.CUST_CD_M  = SUBSTRING(MCD.CUST_CD,6,2)		            " & vbNewLine _
                                          & "		AND MC.CUST_CD_S  = SUBSTRING(MCD.CUST_CD,8,2)		            " & vbNewLine _
                                          & "		AND MC.CUST_CD_SS = SUBSTRING(MCD.CUST_CD,10,2)		            " & vbNewLine _
                                          & "		--追加MR1 MCR1		                                            " & vbNewLine _
                                          & "		--荷主帳票パターン取得               		                    " & vbNewLine _
                                          & "		LEFT JOIN $LM_MST$..M_CUST_RPT MCR1     		                    " & vbNewLine _
                                          & "		ON  MCR1.NRS_BR_CD   = MCD.NRS_BR_CD		                    " & vbNewLine _
                                          & "		AND MCR1.CUST_CD_L   = SUBSTRING(MCD.CUST_CD,1,5)		        " & vbNewLine _
                                          & "		AND MCR1.CUST_CD_M   = SUBSTRING(MCD.CUST_CD,6,2)		        " & vbNewLine _
                                          & "  		AND MCR1.CUST_CD_S   = SUBSTRING(MCD.CUST_CD,8,2)		 	    " & vbNewLine _
                                          & "		AND MCR1.PTN_ID      = '76'                                     " & vbNewLine _
                                          & "		AND MCR1.SYS_DEL_FLG = '0'		                                " & vbNewLine _
                                          & "		                                                                " & vbNewLine _
                                          & "		--帳票パターン取得	                                            " & vbNewLine _
                                          & "		LEFT JOIN $LM_MST$..M_RPT MR1		                                " & vbNewLine _
                                          & "		ON  MR1.NRS_BR_CD   = MCR1.NRS_BR_CD		                    " & vbNewLine _
                                          & "		AND MR1.PTN_ID      = MCR1.PTN_ID		                        " & vbNewLine _
                                          & "		AND MR1.PTN_CD      = MCR1.PTN_CD		                        " & vbNewLine _
                                          & "		AND MR1.SYS_DEL_FLG = '0'         		                        " & vbNewLine _
                                          & "		--追加おわり			                                        " & vbNewLine _
                                          & "		                                                                " & vbNewLine _
                                          & "		WHERE		                                                    " & vbNewLine _
                                          & "		MCD.SUB_KB          = '01'		                                " & vbNewLine _
                                          & "		AND MCD.SYS_DEL_FLG = '0'		                                " & vbNewLine _
                                          & "		AND MC.SYS_DEL_FLG  = '0'		                                " & vbNewLine _
                                          & "		GROUP BY		                                                " & vbNewLine _
                                          & " 		MCD.NRS_BR_CD		                                            " & vbNewLine _
                                          & "		,MCD.SET_NAIYO		                                            " & vbNewLine _
                                          & "		,MR1.PTN_CD		                                                " & vbNewLine _
                                          & "		,MR1.RPT_ID		                                                " & vbNewLine _
                                          & "		) WCUST			                                                " & vbNewLine _
                                          & "		ON   SEKY.NRS_BR_CD = WCUST.NRS_BR_CD			                    " & vbNewLine _
                                          & "		AND  SEKY.DEPART    = WCUST.DEPART		                        " & vbNewLine _
                                          & "		--追加取得できない場合用		                                " & vbNewLine _
                                          & "		--存在しない場合の帳票パターン取得		                        " & vbNewLine _
                                          & "		LEFT JOIN $LM_MST$..M_RPT MR2     		                        " & vbNewLine _
                                          & "		ON  MR2.NRS_BR_CD     = @NRS_BR_CD  		                    " & vbNewLine _
                                          & "		AND MR2.PTN_ID        = '76'            		                " & vbNewLine _
                                          & "		AND MR2.STANDARD_FLAG = '01'      		                        " & vbNewLine _
                                          & "		AND MR2.SYS_DEL_FLG   = '0'         		                    " & vbNewLine _
                                          & "  WHERE                                                                        " & vbNewLine _
                                          & "      SEKY.NRS_BR_CD   = @NRS_BR_CD                                              " & vbNewLine _
                                          & "  AND SEKY.SEKY_YM     = @SEKY_YM                                              " & vbNewLine _
                                          & "  AND SEKY.DEPART      = @DEPART                                               " & vbNewLine _
                                          & "  AND SEKY.SEKY_KMK    = @SEKY_KMK                                             " & vbNewLine _
                                          & "  AND SEKY.FRB_CD      = @FRB_CD                                               " & vbNewLine _
                                          & "  AND SEKY.SRC_CD      = @SRC_CD                                               " & vbNewLine _
                                          & "  AND SEKY.COST_CENTER = @COST_CENTER                                          " & vbNewLine _
                                          & "  AND SEKY.MISK_CD     = @MISK_CD                                              " & vbNewLine


    Private Const SQL_SELECT_DATA As String = "SELECT                                       " & vbNewLine _
                                            & " CASE WHEN WCUST.RPT_ID IS NOT NULL AND WCUST.PTN_CD <> '' THEN WCUST.RPT_ID	            " & vbNewLine _
                                            & "	     ELSE MR2.RPT_ID END                    AS RPT_ID " & vbNewLine _
                                            & ",SEKY.NRS_BR_CD          AS  NRS_BR_CD       " & vbNewLine _
                                            & "--(2012.03.27) Notes№862 営業所名出力 --START --      " & vbNewLine _
                                            & ",M_NRS_BR.NRS_BR_NM      AS  NRS_BR_NM       " & vbNewLine _
                                            & "--(2012.03.27) Notes№862 営業所名出力 -- END --       " & vbNewLine _
                                            & ",SEKY.SEKY_YM			AS	SEKY_YM         " & vbNewLine _
                                            & ",SEKY.DEPART				AS	DEPART          " & vbNewLine _
                                            & ",KBN01.KBN_NM1			AS	DEPART_NM       " & vbNewLine _
                                            & ",SEKY.SEKY_KMK			AS	SEKY_KMK        " & vbNewLine _
                                            & ",KBN02.KBN_NM1			AS	SEKY_KMK_NM     " & vbNewLine _
                                            & ",KBN02.KBN_NM2			AS	ACCOUNT         " & vbNewLine _
                                            & ",SEKY.FRB_CD				AS	FRB_CD          " & vbNewLine _
                                            & ",SEKY.SRC_CD				AS	SRC_CD          " & vbNewLine _
                                            & ",KBN03.KBN_NM2			AS	SRC_NO          " & vbNewLine _
                                            & ",SEKY.COST_CENTER	    AS	COST_CENTER     " & vbNewLine _
                                            & ",SEKY.MISK_CD		    AS	MISK_CD         " & vbNewLine _
                                            & ",SEKY.DEST_CTY			AS	DEST_CTY        " & vbNewLine _
                                            & ",SEKY.AMOUNT				AS	AMOUNT          " & vbNewLine _
                                            & ",SEKY.VAT_AMOUNT		    AS	VAT_AMOUNT      " & vbNewLine _
                                            & ",SEKY.SOUND				AS	SOUND           " & vbNewLine _
                                            & ",SEKY.BOND				AS	BOND            " & vbNewLine _
                                            & ",SEKY.JIDO_FLAG			AS	JIDO_FLAG       " & vbNewLine _
                                            & ",SEKY.SHUDO_FLAG		    AS	SHUDO_FLAG      " & vbNewLine _
                                            & ",SEKY.SYS_ENT_DATE	    AS	SYS_ENT_DATE    " & vbNewLine _
                                            & ",SEKY.SYS_ENT_TIME	    AS	SYS_ENT_TIME    " & vbNewLine _
                                            & ",SEKY.SYS_ENT_PGID	    AS	SYS_ENT_PGID    " & vbNewLine _
                                            & ",SEKY.SYS_ENT_USER	    AS	SYS_ENT_USER    " & vbNewLine _
                                            & ",USER01.USER_NM	    	AS	SYS_ENT_USER_NM " & vbNewLine _
                                            & ",SEKY.SYS_UPD_DATE	    AS	SYS_UPD_DATE    " & vbNewLine _
                                            & ",SEKY.SYS_UPD_TIME	    AS	SYS_UPD_TIME    " & vbNewLine _
                                            & ",SEKY.SYS_UPD_PGID	    AS	SYS_UPD_PGID    " & vbNewLine _
                                            & ",SEKY.SYS_UPD_USER	    AS	SYS_UPD_USER    " & vbNewLine _
                                            & ",USER02.USER_NM			AS	SYS_UPD_USER_NM " & vbNewLine _
                                            & ",SEKY.SYS_DEL_FLG		AS	SYS_DEL_FLG     " & vbNewLine


    ''' <summary>
    ''' 請求書鑑出力対象データ検索用
    ''' </summary>
    ''' <remarks>
    '''</remarks>
    Private Const SQL_FROM_DATA As String = "  FROM                                                                 " & vbNewLine _
                                          & "  $LM_TRN$..G_DUPONT_SEKY_GL AS SEKY                                   " & vbNewLine _
                                          & "	LEFT JOIN			                                                " & vbNewLine _
                                          & "	(SELECT			                                                    " & vbNewLine _
                                          & "		 MCD.NRS_BR_CD    AS NRS_BR_CD                                  " & vbNewLine _
                                          & "		,RIGHT('00' + MCD.SET_NAIYO,2) AS DEPART		                " & vbNewLine _
                                          & "		,CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD			    " & vbNewLine _
                                          & "		ELSE '' END                            AS PTN_CD	            " & vbNewLine _
                                          & "		,CASE WHEN MR1.RPT_ID IS NOT NULL THEN MR1.RPT_ID	            " & vbNewLine _
                                          & "		ELSE '' END                            AS RPT_ID                " & vbNewLine _
                                          & "		FROM		                                                    " & vbNewLine _
                                          & "		--荷主明細マスタ		                                        " & vbNewLine _
                                          & "		$LM_MST$..M_CUST_DETAILS MCD		                            " & vbNewLine _
                                          & "		INNER JOIN $LM_MST$..M_CUST MC		                            " & vbNewLine _
                                          & "		ON MC.NRS_BR_CD = MCD.NRS_BR_CD		                            " & vbNewLine _
                                          & "		AND MC.CUST_CD_L  = SUBSTRING(MCD.CUST_CD,1,5)		            " & vbNewLine _
                                          & "		AND MC.CUST_CD_M  = SUBSTRING(MCD.CUST_CD,6,2)		            " & vbNewLine _
                                          & "		AND MC.CUST_CD_S  = SUBSTRING(MCD.CUST_CD,8,2)		            " & vbNewLine _
                                          & "		AND MC.CUST_CD_SS = SUBSTRING(MCD.CUST_CD,10,2)		            " & vbNewLine _
                                          & "		--追加MR1 MCR1		                                            " & vbNewLine _
                                          & "		--荷主帳票パターン取得               		                    " & vbNewLine _
                                          & "		LEFT JOIN $LM_MST$..M_CUST_RPT MCR1     		                " & vbNewLine _
                                          & "		ON  MCR1.NRS_BR_CD   = MCD.NRS_BR_CD		                    " & vbNewLine _
                                          & "		AND MCR1.CUST_CD_L   = SUBSTRING(MCD.CUST_CD,1,5)		        " & vbNewLine _
                                          & "		AND MCR1.CUST_CD_M   = SUBSTRING(MCD.CUST_CD,6,2)		        " & vbNewLine _
                                          & "  		AND MCR1.CUST_CD_S   = SUBSTRING(MCD.CUST_CD,8,2)		 	    " & vbNewLine _
                                          & "		AND MCR1.PTN_ID      = '76'                                     " & vbNewLine _
                                          & "		AND MCR1.SYS_DEL_FLG = '0'		                                " & vbNewLine _
                                          & "		                                                                " & vbNewLine _
                                          & "		--帳票パターン取得	                                            " & vbNewLine _
                                          & "		LEFT JOIN $LM_MST$..M_RPT MR1		                            " & vbNewLine _
                                          & "		ON  MR1.NRS_BR_CD   = MCR1.NRS_BR_CD		                    " & vbNewLine _
                                          & "		AND MR1.PTN_ID      = MCR1.PTN_ID		                        " & vbNewLine _
                                          & "		AND MR1.PTN_CD      = MCR1.PTN_CD		                        " & vbNewLine _
                                          & "		AND MR1.SYS_DEL_FLG = '0'         		                        " & vbNewLine _
                                          & "		--追加おわり			                                        " & vbNewLine _
                                          & "		                                                                " & vbNewLine _
                                          & "		WHERE		                                                    " & vbNewLine _
                                          & "		MCD.SUB_KB          = '01'		                                " & vbNewLine _
                                          & "		AND MCD.SYS_DEL_FLG = '0'		                                " & vbNewLine _
                                          & "		AND MC.SYS_DEL_FLG  = '0'		                                " & vbNewLine _
                                          & "		GROUP BY		                                                " & vbNewLine _
                                          & " 		MCD.NRS_BR_CD		                                            " & vbNewLine _
                                          & "		,MCD.SET_NAIYO		                                            " & vbNewLine _
                                          & "		,MR1.PTN_CD		                                                " & vbNewLine _
                                          & "		,MR1.RPT_ID		                                                " & vbNewLine _
                                          & "		) WCUST			                                                " & vbNewLine _
                                          & "		ON   SEKY.NRS_BR_CD = WCUST.NRS_BR_CD			                " & vbNewLine _
                                          & "		AND  SEKY.DEPART    = WCUST.DEPART		                        " & vbNewLine _
                                          & "  --区分01                                                             " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..Z_KBN KBN01                                      " & vbNewLine _
                                          & "  ON  KBN01.KBN_GROUP_CD           = 'Z009'                            " & vbNewLine _
                                          & "  AND KBN01.KBN_CD  = SEKY.DEPART                                      " & vbNewLine _
                                          & "  AND KBN01.SYS_DEL_FLG            = '0'                               " & vbNewLine _
                                          & "  --区分02                                                             " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..Z_KBN KBN02                                      " & vbNewLine _
                                          & "  ON  KBN02.KBN_GROUP_CD           = 'S029'                            " & vbNewLine _
                                          & "  AND KBN02.KBN_CD                 = SEKY.SEKY_KMK                     " & vbNewLine _
                                          & "  AND KBN02.SYS_DEL_FLG            = '0'                               " & vbNewLine _
                                          & "  --区分03                                                             " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..Z_KBN KBN03                                      " & vbNewLine _
                                          & "  ON  KBN03.KBN_GROUP_CD           = 'S028'                            " & vbNewLine _
                                          & "  AND KBN03.KBN_CD                 = SEKY.SRC_CD                       " & vbNewLine _
                                          & "  AND KBN03.SYS_DEL_FLG            = '0'                               " & vbNewLine _
                                          & "--(2012.03.27) Notes№862 営業所名出力対応 --START --                  " & vbNewLine _
                                          & "  --営業所マスタ                                                       " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..M_NRS_BR M_NRS_BR                                " & vbNewLine _
                                          & "         ON M_NRS_BR.NRS_BR_CD     = SEKY.NRS_BR_CD                    " & vbNewLine _
                                          & "--(2012.03.27) Notes№862 営業所名出力対応 -- END  --                  " & vbNewLine _
                                          & "  --使用者01 USER01                                                    " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..S_USER USER01                                    " & vbNewLine _
                                          & "  ON  USER01.USER_CD               = SEKY.SYS_ENT_USER                 " & vbNewLine _
                                          & "  AND USER01.SYS_DEL_FLG           = '0'                               " & vbNewLine _
                                          & "  --使用者02 USER02                                                    " & vbNewLine _
                                          & "  LEFT JOIN $LM_MST$..S_USER USER02                                    " & vbNewLine _
                                          & "  ON  USER02.USER_CD               = SEKY.SYS_UPD_USER                 " & vbNewLine _
                                          & "  AND USER02.SYS_DEL_FLG           = '0'                               " & vbNewLine _
                                          & "		--追加取得できない場合用		                                " & vbNewLine _
                                          & "		--存在しない場合の帳票パターン取得		                        " & vbNewLine _
                                          & "	 LEFT JOIN $LM_MST$..M_RPT MR2     		                            " & vbNewLine _
                                          & "	 ON  MR2.NRS_BR_CD     = @NRS_BR_CD  		                        " & vbNewLine _
                                          & "	 AND MR2.PTN_ID        = '76'            		                    " & vbNewLine _
                                          & "	 AND MR2.STANDARD_FLAG = '01'      		                            " & vbNewLine _
                                          & "	 AND MR2.SYS_DEL_FLG   = '0'         		                        " & vbNewLine _
                                          & "  WHERE                                                                " & vbNewLine _
                                          & "      SEKY.NRS_BR_CD   = @NRS_BR_CD                                    " & vbNewLine _
                                          & "  AND SEKY.SEKY_YM     = @SEKY_YM                                      " & vbNewLine _
                                          & "  AND SEKY.DEPART      = @DEPART                                       " & vbNewLine _
                                          & "  AND SEKY.SEKY_KMK    = @SEKY_KMK                                     " & vbNewLine _
                                          & "  AND SEKY.FRB_CD      = @FRB_CD                                       " & vbNewLine _
                                          & "  AND SEKY.SRC_CD      = @SRC_CD                                       " & vbNewLine _
                                          & "  AND SEKY.COST_CENTER = @COST_CENTER                                  " & vbNewLine _
                                          & "  AND SEKY.MISK_CD     = @MISK_CD                                      " & vbNewLine


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
        Dim inTbl As DataTable = ds.Tables("LMG560IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Dim Ssql As String = String.Empty
        Me._StrSql.Append(LMG560DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select)
        Me._StrSql.Append(LMG560DAC.SQL_FROM_MPrt)        'SQL構築(帳票種別用from)
        Call Me.SetConditionPrtDataSQL()
        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG560DAC", "SelectMPrt", cmd)

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
        Dim inTbl As DataTable = ds.Tables("LMG560IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append(LMG560DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMG560DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用FROM)

        Call Me.SetConditionPrtDataSQL()                  '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("MAIN_BR").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG560DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")   '(2012.03.27) 営業所名出力対応
        map.Add("SEKY_YM", "SEKY_YM")
        map.Add("DEPART", "DEPART")
        map.Add("DEPART_NM", "DEPART_NM")
        map.Add("SEKY_KMK", "SEKY_KMK")
        map.Add("SEKY_KMK_NM", "SEKY_KMK_NM")
        map.Add("ACCOUNT", "ACCOUNT")
        map.Add("FRB_CD", "FRB_CD")
        map.Add("SRC_CD", "SRC_CD")
        map.Add("SRC_NO", "SRC_NO")
        map.Add("COST_CENTER", "COST_CENTER")
        map.Add("MISK_CD", "MISK_CD")
        map.Add("DEST_CTY", "DEST_CTY")
        map.Add("AMOUNT", "AMOUNT")
        map.Add("VAT_AMOUNT", "VAT_AMOUNT")
        map.Add("SOUND", "SOUND")
        map.Add("BOND", "BOND")
        map.Add("JIDO_FLAG", "JIDO_FLAG")
        map.Add("SHUDO_FLAG", "SHUDO_FLAG")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG560OUT")

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

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionPrtDataSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_YM", Me._Row.Item("SEKY_YM").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEPART", Me._Row.Item("DEPART").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_KMK", Me._Row.Item("SEKY_KMK").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FRB_CD", Me._Row.Item("FRB_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SRC_CD", Me._Row.Item("SRC_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COST_CENTER", Me._Row.Item("COST_CENTER").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MISK_CD", Me._Row.Item("MISK_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

    End Sub

#End Region

End Class
