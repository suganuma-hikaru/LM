' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC903    : 佐川e飛伝Ⅲ CSV出力
'  作  成  者       :  
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC903DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC903DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 佐川CSV作成データ検索用SQL SELECT部 標準用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAGAWAEHIDEN_CSV As String _
           = " SELECT TOP(1)                                                                " & vbNewLine _
           & "    OUTKAL.NRS_BR_CD	            AS NRS_BR_CD                                " & vbNewLine _
           & "   ,OUTKAL.OUTKA_NO_L             AS DENPYO_NO                                " & vbNewLine _
           & "   ,'1'                           AS TODOKESAKI_CD_GET_KB                     " & vbNewLine _
           & "   ,''                            AS TODOKESAKI_CD                            " & vbNewLine _
           & "   ,OUTKAL.DEST_TEL               AS DEST_TEL                                 " & vbNewLine _
           & "   , CASE WHEN OUTKAL.DEST_KB = '02'                                          " & vbNewLine _
           & "          THEN EDIL.DEST_ZIP                                                  " & vbNewLine _
           & "          ELSE DEST.ZIP                                                       " & vbNewLine _
           & "     END                                                      AS DEST_ZIP     " & vbNewLine _
           & "   , CASE OUTKAL.DEST_KB WHEN '01'                                            " & vbNewLine _
           & "                         THEN UPPER(OUTKAL.DEST_AD_1)                         " & vbNewLine _
           & "                            + UPPER(OUTKAL.DEST_AD_2)                         " & vbNewLine _
           & "                         WHEN '02'                                            " & vbNewLine _
           & "                         THEN UPPER(EDIL.DEST_AD_1)                           " & vbNewLine _
           & "                            + UPPER(EDIL.DEST_AD_2)                           " & vbNewLine _
           & "                         ELSE UPPER(DEST.AD_1)                                " & vbNewLine _
           & "                            + UPPER(DEST.AD_2)                                " & vbNewLine _
           & "                         END                                  AS DEST_AD_1    " & vbNewLine _
           & "   ,''                                                        AS DEST_AD_2    " & vbNewLine _
           & "   , CASE OUTKAL.DEST_KB WHEN '00' THEN OUTKAL.DEST_AD_3                      " & vbNewLine _
           & "                         WHEN '01' THEN OUTKAL.DEST_AD_3                      " & vbNewLine _
           & "                         WHEN '02' THEN EDIL.DEST_AD_3                        " & vbNewLine _
           & "                         ELSE  DEST.AD_3 END                  AS DEST_AD_3    " & vbNewLine _
           & "   , CASE OUTKAL.DEST_KB WHEN '01' THEN OUTKAL.DEST_NM                        " & vbNewLine _
           & "                         WHEN '02' THEN EDIL.DEST_NM                          " & vbNewLine _
           & "                         ELSE  DEST.DEST_NM END               AS DEST_NM_1    " & vbNewLine _
           & "   ,''                                                        AS DEST_NM_2    " & vbNewLine _
           & "   ,OUTKAL.OUTKA_NO_L                                   AS OKYAKU_KANRI_NO    " & vbNewLine _
           & "   ,CASE WHEN OKURIJOCSV.NRS_BR_CD IS NOT NULL                                " & vbNewLine _
           & "         THEN OKURIJOCSV.FREE_C02                                             " & vbNewLine _
           & "         WHEN OKURIJOCSVX.NRS_BR_CD IS NOT NULL                               " & vbNewLine _
           & "         THEN OKURIJOCSVX.FREE_C02                                            " & vbNewLine _
           & "         ELSE ''                                                              " & vbNewLine _
           & "    END                                                 AS OKYAKU_CD          " & vbNewLine _
           & "   ,'1'                                                 AS BUSYO_TANTOUSYA_CD_GET_KB " & vbNewLine _
           & "   ,''                                                  AS BUSYO_TANTOUSYA_CD " & vbNewLine _
           & "   ,''                                                  AS BUSYO_TANTOUSYA    " & vbNewLine _
           & "   ,''                                                  AS NIOKURININ_TEL     " & vbNewLine _
           & "   ,'1'                                                 AS GOIRAINUSHI_CD_GET_KB " & vbNewLine _
           & "   ,''                                                  AS GOIRAINUSHI_CD     " & vbNewLine _
           & "   , CASE WHEN SOKO.WH_KB = '01'  -- 自社倉庫                                 " & vbNewLine _
           & "          THEN SOKO.TEL                                                       " & vbNewLine _
           & "          ELSE NRSBR.TEL                                                      " & vbNewLine _
           & "     END                                                AS GOIRAINUSHI_TEL    " & vbNewLine _
           & "   , CASE WHEN SOKO.WH_KB = '01'                                              " & vbNewLine _
           & "            THEN                                                              " & vbNewLine _
           & "                 CASE WHEN LEN(SOKO.ZIP) = 7                                  " & vbNewLine _
           & "                      THEN SUBSTRING(SOKO.ZIP, 1, 3)                          " & vbNewLine _
           & "                         + '-'                                                " & vbNewLine _
           & "                         + SUBSTRING(SOKO.ZIP, 4, 4)                          " & vbNewLine _
           & "                      ELSE SOKO.ZIP                                           " & vbNewLine _
           & "                 END                                                          " & vbNewLine _
           & "            ELSE                                                              " & vbNewLine _
           & "                 CASE WHEN LEN(NRSBR.ZIP) = 7                                 " & vbNewLine _
           & "                      THEN SUBSTRING(NRSBR.ZIP, 1, 3)                         " & vbNewLine _
           & "                         + '-'                                                " & vbNewLine _
           & "                         + SUBSTRING(NRSBR.ZIP, 4, 4)                         " & vbNewLine _
           & "                      ELSE NRSBR.ZIP                                          " & vbNewLine _
           & "                 END                                                          " & vbNewLine _
           & "            END                                         AS GOIRAINUSHI_ZIP    " & vbNewLine _
           & "    , CASE WHEN SOKO.WH_KB = '01'                                             " & vbNewLine _
           & "           THEN LTRIM(RTRIM(SOKO.AD_1))                                       " & vbNewLine _
           & "           ELSE LTRIM(RTRIM(NRSBR.AD_1))                                      " & vbNewLine _
           & "      END                                               AS GOIRAINUSHI_AD1    " & vbNewLine _
           & "    , CASE WHEN SOKO.WH_KB = '01'                                             " & vbNewLine _
           & "           THEN LTRIM(RTRIM(SOKO.AD_2))                                       " & vbNewLine _
           & "           ELSE LTRIM(RTRIM(NRSBR.AD_2))                                      " & vbNewLine _
           & "      END                                               AS GOIRAINUSHI_AD2    " & vbNewLine _
           & "    , NRSBR.NRS_BR_NM                                   AS GOIRAINUSHI_NM1    " & vbNewLine _
           & "   ,''                           AS GOIRAINUSHI_NM2                           " & vbNewLine _
           & "   ,'008'                        AS NISUGATA_CD                               " & vbNewLine _
           & "   ,OUTKAL.CUST_ORD_NO           AS HINMEI1                                   " & vbNewLine _
           & "   ,''                           AS HINMEI2                                   " & vbNewLine _
           & "   ,''                           AS HINMEI3                                   " & vbNewLine _
           & "   ,''                           AS HINMEI4                                   " & vbNewLine _
           & "   ,''                           AS HINMEI5                                   " & vbNewLine _
           & "   ,''                           AS NIFUDA_NISUGATA                           " & vbNewLine _
           & "   ,''                           AS NIFUDA_HINMEI1                            " & vbNewLine _
           & "   ,''                           AS NIFUDA_HINMEI2                            " & vbNewLine _
           & "   ,''                           AS NIFUDA_HINMEI3                            " & vbNewLine _
           & "   ,''                           AS NIFUDA_HINMEI4                            " & vbNewLine _
           & "   ,''                           AS NIFUDA_HINMEI5                            " & vbNewLine _
           & "   ,''                           AS NIFUDA_HINMEI6                            " & vbNewLine _
           & "   ,''                           AS NIFUDA_HINMEI7                            " & vbNewLine _
           & "   ,''                           AS NIFUDA_HINMEI8                            " & vbNewLine _
           & "   ,''                           AS NIFUDA_HINMEI9                            " & vbNewLine _
           & "   ,''                           AS NIFUDA_HINMEI10                           " & vbNewLine _
           & "   ,''                           AS NIFUDA_HINMEI11                           " & vbNewLine _
           & "   ,OUTKAL.OUTKA_PKG_NB          AS SYUKKA_KOSU                               " & vbNewLine _
           & "   ,''                           AS BINSYU_SPEED                              " & vbNewLine _
           & " 	 ,ISNULL(OKURIJOCSV.FREE_C19,'001')     AS BINSYU_HINMEI       -- 指定なし           " & vbNewLine _
           & "   ,OUTKAL.ARR_PLAN_DATE         AS HAITATSU_BI                               " & vbNewLine _
           & "   ,''                           AS HAITATSU_JIKANTAI                         " & vbNewLine _
           & "   ,''                           AS HAITATSU_JIKAN                            " & vbNewLine _
           & "   ,''                           AS DAIBIKI_KINGAKU                           " & vbNewLine _
           & "   ,''                           AS TAX                                       " & vbNewLine _
           & "   ,''                           AS KESSAI_SYUBETSU                           " & vbNewLine _
           & "   ,''                           AS HOKEN_KINGAKU                             " & vbNewLine _
           & "   ,ISNULL(OKURIJOCSV.FREE_C20,'')                           AS SEAL1                                     " & vbNewLine _
           & "  ,''                                                       AS SEAL2                                     " & vbNewLine _
           & "  ,''                                                       AS SEAL3                                     " & vbNewLine _
           & "  ,''                            AS EIGYOTENDOME                              " & vbNewLine _
           & "  ,''                            AS SRC_KBN                                   " & vbNewLine _
           & "  ,''                            AS EIGYOTEN_CD                               " & vbNewLine _
           & "  ,'1'                           AS MOTOCHAKU_KBN                             " & vbNewLine _
           & "  ,''                            AS EMAIL_ADDRESSS                            " & vbNewLine _
           & "  ,''                            AS FUZAIJI_TEL                               " & vbNewLine _
           & "  ,''                            AS SYUKKA_BI                                 " & vbNewLine _
           & "  ,''                            AS TOIAWASE_DENPYO_NO                        " & vbNewLine _
           & "  ,''                            AS SYUKKABA_PRINT_KB                         " & vbNewLine _
           & "  ,''                            AS SYUYAKU_KAIJO                             " & vbNewLine _
           & "  ,''                            AS EDIT01                                    " & vbNewLine _
           & "  ,''                            AS EDIT02                                    " & vbNewLine _
           & "  ,''                            AS EDIT03                                    " & vbNewLine _
           & "  ,''                            AS EDIT04                                    " & vbNewLine _
           & "  ,''                            AS EDIT05                                    " & vbNewLine _
           & "  ,''                            AS EDIT06                                    " & vbNewLine _
           & "  ,''                            AS EDIT07                                    " & vbNewLine _
           & "  ,''                            AS EDIT08                                    " & vbNewLine _
           & "  ,''                            AS EDIT09                                    " & vbNewLine _
           & "  ,''                            AS EDIT10                                    " & vbNewLine _
           & "  ,@ROW_NO AS ROW_NO                                                          " & vbNewLine _
           & "  ,OUTKAL.SYS_UPD_DATE           AS SYS_UPD_DATE                              " & vbNewLine _
           & "  ,OUTKAL.SYS_UPD_TIME           AS SYS_UPD_TIME                              " & vbNewLine _
           & "  ,@FILEPATH                     AS FILEPATH                                  " & vbNewLine _
           & "  ,@FILENAME                     AS FILENAME                                  " & vbNewLine _
           & "  ,@SYS_DATE                     AS SYS_DATE                                  " & vbNewLine _
           & "  ,@SYS_TIME                     AS SYS_TIME                                  " & vbNewLine

    ''' <summary>
    ''' 佐川CSV作成データ検索用SQL FROM・WHERE部 標準用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SAGAWAEHIDEN_CSV_FROM As String _
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
        & " LEFT JOIN $LM_TRN$..C_OUTKA_M OUTKAM ON                                                 " & vbNewLine _
        & "           OUTKAM.NRS_BR_CD   = OUTKAL.NRS_BR_CD                                         " & vbNewLine _
        & "       AND OUTKAM.OUTKA_NO_L  = OUTKAL.OUTKA_NO_L                                        " & vbNewLine _
        & "       AND OUTKAM.SYS_DEL_FLG  = '0'                                                     " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_GOODS GOODS                                                       " & vbNewLine _
        & "        ON GOODS.NRS_BR_CD    = OUTKAM.NRS_BR_CD                                         " & vbNewLine _
        & "       AND GOODS.GOODS_CD_NRS = OUTKAM.GOODS_CD_NRS                                      " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_SOKO SOKO                                                         " & vbNewLine _
        & "   ON SOKO.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                  " & vbNewLine _
        & "  AND SOKO.WH_CD     = OUTKAL.WH_CD                                                      " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_NRS_BR AS NRSBR                                                   " & vbNewLine _
        & "   ON NRSBR.NRS_BR_CD = OUTKAL.NRS_BR_CD                                                 " & vbNewLine _
        & " LEFT JOIN (                                                                             " & vbNewLine _
        & "               SELECT                                                                    " & vbNewLine _
        & "                     NRS_BR_CD                                                           " & vbNewLine _
        & "                   , EDI_CTL_NO                                                          " & vbNewLine _
        & "                   , OUTKA_CTL_NO                                                        " & vbNewLine _
        & "               FROM (                                                                    " & vbNewLine _
        & "                       SELECT                                                            " & vbNewLine _
        & "                             EDIOUTL.NRS_BR_CD                                           " & vbNewLine _
        & "                           , EDIOUTL.EDI_CTL_NO                                          " & vbNewLine _
        & "                           , EDIOUTL.OUTKA_CTL_NO                                        " & vbNewLine _
        & "                           , CASE WHEN EDIOUTL.OUTKA_CTL_NO = '' THEN 1                  " & vbNewLine _
        & "                             ELSE ROW_NUMBER() OVER (PARTITION BY EDIOUTL.NRS_BR_CD      " & vbNewLine _
        & "                                                                , EDIOUTL.OUTKA_CTL_NO   " & vbNewLine _
        & "                                                         ORDER BY EDIOUTL.NRS_BR_CD      " & vbNewLine _
        & "                                                                , EDIOUTL.EDI_CTL_NO     " & vbNewLine _
        & "                                                     )                                   " & vbNewLine _
        & "                             END AS IDX                                                  " & vbNewLine _
        & "                       FROM $LM_TRN$..H_OUTKAEDI_L EDIOUTL                               " & vbNewLine _
        & "                       WHERE EDIOUTL.SYS_DEL_FLG  = '0'                                  " & vbNewLine _
        & "                         AND EDIOUTL.NRS_BR_CD    = @NRS_BR_CD                           " & vbNewLine _
        & "                         AND EDIOUTL.OUTKA_CTL_NO = @OUTKA_NO_L                          " & vbNewLine _
        & "                     ) EBASE                                                             " & vbNewLine _
        & "               WHERE EBASE.IDX = 1                                                       " & vbNewLine _
        & "               ) TOPEDI                                                                  " & vbNewLine _
        & "           ON TOPEDI.NRS_BR_CD               = OUTKAL.NRS_BR_CD                          " & vbNewLine _
        & "          AND TOPEDI.OUTKA_CTL_NO            = OUTKAL.OUTKA_NO_L                         " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..H_OUTKAEDI_L EDIL                                                   " & vbNewLine _
        & "        ON EDIL.NRS_BR_CD                    = TOPEDI.NRS_BR_CD                          " & vbNewLine _
        & "       AND EDIL.EDI_CTL_NO                   = TOPEDI.EDI_CTL_NO                         " & vbNewLine _
        & " LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL                                                      " & vbNewLine _
        & "   ON UNSOL.NRS_BR_CD                      = OUTKAL.NRS_BR_CD                            " & vbNewLine _
        & "  AND UNSOL.INOUTKA_NO_L                   = OUTKAL.OUTKA_NO_L                           " & vbNewLine _
        & "  AND UNSOL.MOTO_DATA_KB                   = '20'                                        " & vbNewLine _
        & "  AND UNSOL.SYS_DEL_FLG                    = '0'                                         " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSV     --既存マスタJOIN                       " & vbNewLine _
        & "    ON OKURIJOCSV.NRS_BR_CD                = UNSOL.NRS_BR_CD                             " & vbNewLine _
        & "   AND OKURIJOCSV.UNSOCO_CD                = UNSOL.UNSO_CD                               " & vbNewLine _
        & "   AND OKURIJOCSV.CUST_CD_L                = UNSOL.CUST_CD_L                             " & vbNewLine _
        & "   --20170324  FREEC18と支店コードJOIN条件追加                                           " & vbNewLine _
        & "   AND OKURIJOCSV.FREE_C18                 = UNSOL.UNSO_BR_CD                           " & vbNewLine _
        & "   AND OKURIJOCSV.OKURIJO_TP               = '05' --佐川                                 " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_OKURIJO_CSV OKURIJOCSVX    --追加マスタJOIN                       " & vbNewLine _
        & "    ON OKURIJOCSVX.NRS_BR_CD               = UNSOL.NRS_BR_CD                             " & vbNewLine _
        & "   AND OKURIJOCSVX.UNSOCO_CD               = UNSOL.UNSO_CD                               " & vbNewLine _
        & "   AND OKURIJOCSVX.CUST_CD_L               = 'XXXXX'                                     " & vbNewLine _
        & "   AND OKURIJOCSVX.OKURIJO_TP              = '05' --佐川                                 " & vbNewLine _
        & " WHERE OUTKAL.NRS_BR_CD   = @NRS_BR_CD                                                   " & vbNewLine _
        & "   AND OUTKAL.OUTKA_NO_L  = @OUTKA_NO_L                                                  " & vbNewLine _
        & "   AND OUTKAL.SYS_DEL_FLG = '0'                                                          " & vbNewLine _
        & "   AND NOT EXISTS (SELECT * FROM LM_MST..M_CUST_DETAILS                                  " & vbNewLine _
        & "                            WHERE M_CUST_DETAILS.NRS_BR_CD   = OUTKAL.NRS_BR_CD          " & vbNewLine _
        & "                              AND M_CUST_DETAILS.CUST_CD     = OUTKAL.CUST_CD_L          " & vbNewLine _
        & "                              AND M_CUST_DETAILS.SUB_KB      = '0M'                      " & vbNewLine _
        & "                              AND M_CUST_DETAILS.SYS_DEL_FLG = '0'                       " & vbNewLine _
        & "                              AND M_CUST_DETAILS.SET_NAIYO   = '1')                      " & vbNewLine

#End Region ' "検索処理 SQL"

#Region "更新 SQL"

#Region "佐川e飛伝ⅢCSV作成"

    Private Const SQL_UPDATE_SAGAWAEHIDEN_CSV As String = "UPDATE $LM_TRN$..C_OUTKA_L SET              " & vbNewLine _
                                             & " DENP_FLAG         = '01'                         " & vbNewLine _
                                             & ",SYS_UPD_DATE      = @SYS_UPD_DATE                " & vbNewLine _
                                             & ",SYS_UPD_TIME      = @SYS_UPD_TIME                " & vbNewLine _
                                             & ",SYS_UPD_PGID      = @SYS_UPD_PGID                " & vbNewLine _
                                             & ",SYS_UPD_USER      = @SYS_UPD_USER                " & vbNewLine _
                                             & "WHERE NRS_BR_CD    = @NRS_BR_CD                   " & vbNewLine _
                                             & "  AND OUTKA_NO_L   = @DENPYO_NO                   " & vbNewLine

#End Region ' "佐川e飛伝ⅢCSV作成"

#End Region ' "更新 SQL"

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

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 佐川e飛伝ⅢCSV作成対象検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>佐川e飛伝ⅢCSV作成対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectSagawaEHiden3Csv(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC903IN")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMC903DAC.SQL_SELECT_SAGAWAEHIDEN_CSV)
        Me._StrSql.Append(LMC903DAC.SQL_SELECT_SAGAWAEHIDEN_CSV_FROM)

        ' 条件設定
        Call Me.setSQLSelect()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", Me._Row("ROW_NO"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILEPATH", Me._Row("FILEPATH"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILENAME", Me._Row("FILENAME"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DATE", Me._Row("SYS_DATE"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_TIME", Me._Row("SYS_TIME"), DBDataType.CHAR))

        ' スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMC903DAC", "SelectSagawaEHiden3Csv", cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                ' 取得データの格納先をマッピング
                map.Add("NRS_BR_CD", "NRS_BR_CD")
                map.Add("DENPYO_NO", "DENPYO_NO")
                map.Add("TODOKESAKI_CD_GET_KB", "TODOKESAKI_CD_GET_KB")
                map.Add("TODOKESAKI_CD", "TODOKESAKI_CD")
                map.Add("DEST_TEL", "DEST_TEL")
                map.Add("DEST_ZIP", "DEST_ZIP")
                map.Add("DEST_AD_1", "DEST_AD_1")
                map.Add("DEST_AD_2", "DEST_AD_2")
                map.Add("DEST_AD_3", "DEST_AD_3")
                map.Add("DEST_NM_1", "DEST_NM_1")
                map.Add("DEST_NM_2", "DEST_NM_2")
                map.Add("OKYAKU_KANRI_NO", "OKYAKU_KANRI_NO")
                map.Add("OKYAKU_CD", "OKYAKU_CD")
                map.Add("BUSYO_TANTOUSYA_CD_GET_KB", "BUSYO_TANTOUSYA_CD_GET_KB")
                map.Add("BUSYO_TANTOUSYA_CD", "BUSYO_TANTOUSYA_CD")
                map.Add("BUSYO_TANTOUSYA", "BUSYO_TANTOUSYA")
                map.Add("NIOKURININ_TEL", "NIOKURININ_TEL")
                map.Add("GOIRAINUSHI_CD_GET_KB", "GOIRAINUSHI_CD_GET_KB")
                map.Add("GOIRAINUSHI_CD", "GOIRAINUSHI_CD")
                map.Add("GOIRAINUSHI_TEL", "GOIRAINUSHI_TEL")
                map.Add("GOIRAINUSHI_ZIP", "GOIRAINUSHI_ZIP")
                map.Add("GOIRAINUSHI_AD1", "GOIRAINUSHI_AD1")
                map.Add("GOIRAINUSHI_AD2", "GOIRAINUSHI_AD2")
                map.Add("GOIRAINUSHI_NM1", "GOIRAINUSHI_NM1")
                map.Add("GOIRAINUSHI_NM2", "GOIRAINUSHI_NM2")
                map.Add("NISUGATA_CD", "NISUGATA_CD")
                map.Add("HINMEI1", "HINMEI1")
                map.Add("HINMEI2", "HINMEI2")
                map.Add("HINMEI3", "HINMEI3")
                map.Add("HINMEI4", "HINMEI4")
                map.Add("HINMEI5", "HINMEI5")
                map.Add("NIFUDA_NISUGATA", "NIFUDA_NISUGATA")
                map.Add("NIFUDA_HINMEI1", "NIFUDA_HINMEI1")
                map.Add("NIFUDA_HINMEI2", "NIFUDA_HINMEI2")
                map.Add("NIFUDA_HINMEI3", "NIFUDA_HINMEI3")
                map.Add("NIFUDA_HINMEI4", "NIFUDA_HINMEI4")
                map.Add("NIFUDA_HINMEI5", "NIFUDA_HINMEI5")
                map.Add("NIFUDA_HINMEI6", "NIFUDA_HINMEI6")
                map.Add("NIFUDA_HINMEI7", "NIFUDA_HINMEI7")
                map.Add("NIFUDA_HINMEI8", "NIFUDA_HINMEI8")
                map.Add("NIFUDA_HINMEI9", "NIFUDA_HINMEI9")
                map.Add("NIFUDA_HINMEI10", "NIFUDA_HINMEI10")
                map.Add("NIFUDA_HINMEI11", "NIFUDA_HINMEI11")
                map.Add("SYUKKA_KOSU", "SYUKKA_KOSU")
                map.Add("BINSYU_SPEED", "BINSYU_SPEED")
                map.Add("BINSYU_HINMEI", "BINSYU_HINMEI")
                map.Add("HAITATSU_BI", "HAITATSU_BI")
                map.Add("HAITATSU_JIKANTAI", "HAITATSU_JIKANTAI")
                map.Add("HAITATSU_JIKAN", "HAITATSU_JIKAN")
                map.Add("DAIBIKI_KINGAKU", "DAIBIKI_KINGAKU")
                map.Add("TAX", "TAX")
                map.Add("KESSAI_SYUBETSU", "KESSAI_SYUBETSU")
                map.Add("HOKEN_KINGAKU", "HOKEN_KINGAKU")
                map.Add("SEAL1", "SEAL1")
                map.Add("SEAL2", "SEAL2")
                map.Add("SEAL3", "SEAL3")
                map.Add("EIGYOTENDOME", "EIGYOTENDOME")
                map.Add("SRC_KBN", "SRC_KBN")
                map.Add("EIGYOTEN_CD", "EIGYOTEN_CD")
                map.Add("MOTOCHAKU_KBN", "MOTOCHAKU_KBN")
                map.Add("EMAIL_ADDRESSS", "EMAIL_ADDRESSS")
                map.Add("FUZAIJI_TEL", "FUZAIJI_TEL")
                map.Add("SYUKKA_BI", "SYUKKA_BI")
                map.Add("TOIAWASE_DENPYO_NO", "TOIAWASE_DENPYO_NO")
                map.Add("SYUKKABA_PRINT_KB", "SYUKKABA_PRINT_KB")
                map.Add("SYUYAKU_KAIJO", "SYUYAKU_KAIJO")
                map.Add("EDIT01", "EDIT01")
                map.Add("EDIT02", "EDIT02")
                map.Add("EDIT03", "EDIT03")
                map.Add("EDIT04", "EDIT04")
                map.Add("EDIT05", "EDIT05")
                map.Add("EDIT06", "EDIT06")
                map.Add("EDIT07", "EDIT07")
                map.Add("EDIT08", "EDIT08")
                map.Add("EDIT09", "EDIT09")
                map.Add("EDIT10", "EDIT10")
                map.Add("ROW_NO", "ROW_NO")
                map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
                map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
                map.Add("FILEPATH", "FILEPATH")
                map.Add("FILENAME", "FILENAME")
                map.Add("SYS_DATE", "SYS_DATE")
                map.Add("SYS_TIME", "SYS_TIME")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC903OUT")

                ' 処理件数の設定
                MyBase.SetResultCount(ds.Tables("LMC903OUT").Rows.Count())
                reader.Close()

            End Using

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 出荷Lテーブル更新（佐川CSV作成時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateSagawaEHiden3Csv(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMC903OUT").Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamCommonSystemUp()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENPYO_NO", Me._Row("DENPYO_NO").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC903DAC.SQL_UPDATE_SAGAWAEHIDEN_CSV, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC903DAC", "UpdateSagawaEHiden3Csv", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, True)

        Return ds

    End Function

#End Region ' "検索処理"

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

#End Region ' "SQL"

#End Region ' "設定処理"


#End Region

End Class
