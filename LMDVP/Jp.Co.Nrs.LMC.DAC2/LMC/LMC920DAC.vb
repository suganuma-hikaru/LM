' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC920    : ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV出力
'  作  成  者       :  [daikoku]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC920DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC920DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

#Region "標準"

    ''' <summary>
    ''' ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV作成データ検索用SQL SELECT部 
    ''' </summary>
    ''' <remarks>2019/10/17 要望管理008042 ZIPの取得内容を変更</remarks>
    Private Const SQL_SELECT_KANGAROO_CSV As String _
        = "SELECT 		  " & vbNewLine _
            & " 	  COL.NRS_BR_CD                  AS  NRS_BR_CD               --営業所CD                  " & vbNewLine _
            & " 	 ,COL.OUTKA_NO_L                 AS  OUTKA_NO_L              --出荷管理番号(大）	     " & vbNewLine _
            & " 	 ,COL.OUTKA_PLAN_DATE            AS  OUTKA_PLAN_DATE         --出荷予定日		         " & vbNewLine _
            & " 	 ,COL.DEST_TEL                   AS  AD_TEL                  --届先電話番号		         " & vbNewLine _
            & "-- 	 ,ISNULL(DEST.ZIP,'')            AS  ZIP                     --届先郵便番号		         " & vbNewLine _
            & " 	 ,CASE WHEN ISNULL(EDIL.DEST_ZIP,'') = '' THEN ISNULL(DEST.ZIP,'') ELSE EDIL.DEST_ZIP END AS  ZIP --届先郵便番号 " & vbNewLine _
            & " 	 ,SUBSTRING(LTRIM(COL.DEST_AD_1) + LTRIM(COL.DEST_AD_2) + LTRIM(COL.DEST_AD_3),1,30)  AS  AD_1    --届先住所1	 " & vbNewLine _
            & " 	 ,SUBSTRING(LTRIM(COL.DEST_AD_1) + LTRIM(COL.DEST_AD_2) + LTRIM(COL.DEST_AD_3),31,30) AS  AD_2    --届先住所2	 " & vbNewLine _
            & " 	 ,SUBSTRING(LTRIM(COL.DEST_AD_1) + LTRIM(COL.DEST_AD_2) + LTRIM(COL.DEST_AD_3),61,30) AS  AD_3    --届先住所3	 " & vbNewLine _
            & " 	 ,COL.DEST_NM                    AS  DEST_NM                 --届先名		             " & vbNewLine _
            & " 	 ,COL.OUTKA_PKG_NB               AS  OUTKA_PKG_NB            --個数		                 " & vbNewLine _
            & " 	 ,COL.REMARK                     AS  OUTKAL_REMARK           --出荷時注意事項		     " & vbNewLine _
            & " 	 ,UNSOL.REMARK                   AS  UNSOL_REMARK            --配送時注意事項		     " & vbNewLine _
            & " 	 ,COL.NHS_REMARK                 AS  NHS_REMARK              --納品書摘要		         " & vbNewLine _
            & " 	 ,COL.ARR_PLAN_DATE              AS  ARR_PLAN_DATE           --納入予定日		         " & vbNewLine _
            & " 	 ,CASE WHEN COL.NRS_BR_CD = '30' AND COL.CUST_CD_L = '00360'                             " & vbNewLine _
            & " 	      THEN KBN1.KBN_NM1                                                                 " & vbNewLine _
            & " 	      ELSE ISNULL(SOKO.TEL,'') END AS  CODE_TEL             --荷送り人コード（電話番号）" & vbNewLine _
            & " 	 ,CASE WHEN COL.NRS_BR_CD = '30' AND COL.CUST_CD_L = '00360'                             " & vbNewLine _
            & " 	      THEN KBN1.KBN_NM2                                                                 " & vbNewLine _
            & " 	      ELSE ISNULL(SOKO.TEL,'') END AS  TEL                  --荷送り人電話番号		     " & vbNewLine _
            & " 	 ,CASE WHEN COL.NRS_BR_CD = '30' AND COL.CUST_CD_L = '00360'                             " & vbNewLine _
            & " 	      THEN KBN1.KBN_NM3                                                                 " & vbNewLine _
            & " 	      ELSE  LTRIM(RTRIM(SOKO.AD_1)) + LTRIM(RTRIM(SOKO.AD_2))                           " & vbNewLine _
            & " 	      END AS AD1_AD2                                        --荷送り人住所              " & vbNewLine _
            & " 	 ,CASE WHEN COL.NRS_BR_CD = '30' AND COL.CUST_CD_L = '00360'                             " & vbNewLine _
            & " 	      THEN KBN1.KBN_NM4                                                                 " & vbNewLine _
            & " 	      ELSE  'NRS 株式会社 ' + SOKO.WH_NM END  AS SOKO_WH_NM --荷送り人名		         " & vbNewLine _
            & " 	 ,CONVERT(int,UNSOL.UNSO_WT)     AS  JYURYO                  --重量　　　　　　　	     " & vbNewLine _
            & " 	 ,ISNULL(COL.CUST_ORD_NO,'')     AS  CUST_ORD_NO             --荷主注文番号（全体）      " & vbNewLine _
            & " 	 ,ISNULL(COL.BUYER_ORD_NO,'')    AS  BUYER_ORD_NO            --買主注文番号（全体）      " & vbNewLine _
            & " 	 ,@ROW_NO                        AS  ROW_NO                  --		                     " & vbNewLine _
            & " 	 ,COL.SYS_UPD_DATE               AS  SYS_UPD_DATE            --		                     " & vbNewLine _
            & " 	 ,COL.SYS_UPD_TIME               AS  SYS_UPD_TIME            --		                     " & vbNewLine _
            & " 	 ,@FILEPATH                      AS  FILEPATH                --		                     " & vbNewLine _
            & " 	 ,@FILENAME                      AS  FILENAME                --		                     " & vbNewLine _
            & " 	 ,@SYS_DATE                      AS  SYS_DATE                --		                     " & vbNewLine _
            & " 	 ,@SYS_TIME                      AS  SYS_TIME                --		                     " & vbNewLine _
            & " 	 ,COL.CUST_CD_L                  AS  CUST_CD_L               --荷主コード                " & vbNewLine _
            & " 	 ,MGS.GOODS_NM_1                 AS  GOODS_NM                --商品（中１）              " & vbNewLine


    ''' <summary>
    ''' ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV作成データ検索用SQL FROM・WHERE部 標準用
    ''' </summary>
    ''' <remarks>2019/10/17 要望管理008042 H_OUTKAEDI_LのJOINを追加</remarks>
    Private Const SQL_SELECT_KANGAROO_CSV_FROM As String _
            = " 	FROM $LM_TRN$..C_OUTKA_L  AS COL		             " & vbNewLine _
            & " 	LEFT JOIN  $LM_MST$..M_DEST  DEST		             " & vbNewLine _
            & " 	  ON DEST.DEST_CD           =  COL.DEST_CD		     " & vbNewLine _
            & " 	 AND DEST.CUST_CD_L         =  COL.CUST_CD_L	     " & vbNewLine _
            & " 	 AND DEST.NRS_BR_CD         =  COL.NRS_BR_CD		 " & vbNewLine _
            & " 	LEFT JOIN $LM_TRN$..F_UNSO_L UNSOL		             " & vbNewLine _
            & " 	  ON UNSOL.NRS_BR_CD        =  COL.NRS_BR_CD         " & vbNewLine _
            & " 	 AND UNSOL.INOUTKA_NO_L     =  COL.OUTKA_NO_L        " & vbNewLine _
            & " 	 AND UNSOL.MOTO_DATA_KB     =  '20'		             " & vbNewLine _
            & " 	 AND UNSOL.SYS_DEL_FLG      =  '0'   		         " & vbNewLine _
            & " 	LEFT  JOIN $LM_MST$..M_SOKO    SOKO                  " & vbNewLine _
            & " 	  ON   COL.NRS_BR_CD        =  SOKO.NRS_BR_CD        " & vbNewLine _
            & " 	 AND   COL.WH_CD            =  SOKO.WH_CD            " & vbNewLine _
            & " 	 AND   COL.SYS_DEL_FLG      =  '0'                   " & vbNewLine _
            & "     LEFT JOIN $LM_MST$..Z_KBN KBN1                       " & vbNewLine _
            & "     ON  KBN1.KBN_GROUP_CD = 'C040'                       " & vbNewLine _
            & "     LEFT JOIN (                                          " & vbNewLine _
            & "       SELECT                                             " & vbNewLine _
            & "           NRS_BR_CD                                      " & vbNewLine _
            & "          ,OUTKA_CTL_NO                                   " & vbNewLine _
            & "          ,DEST_ZIP                                       " & vbNewLine _
            & "       FROM $LM_TRN$..H_OUTKAEDI_L                        " & vbNewLine _
            & "       WHERE SYS_DEL_FLG = '0'                            " & vbNewLine _
            & "       GROUP BY                                           " & vbNewLine _
            & "           NRS_BR_CD                                      " & vbNewLine _
            & "          ,OUTKA_CTL_NO                                   " & vbNewLine _
            & "          ,DEST_ZIP                                       " & vbNewLine _
            & "       ) EDIL                                             " & vbNewLine _
            & " 	  ON EDIL.NRS_BR_CD        =  COL.NRS_BR_CD          " & vbNewLine _
            & " 	 AND EDIL.OUTKA_CTL_NO     =  COL.OUTKA_NO_L         " & vbNewLine _
            & "     LEFT JOIN (                                          " & vbNewLine _
            & "       SELECT                                             " & vbNewLine _
            & "           NRS_BR_CD                                      " & vbNewLine _
            & "          ,OUTKA_NO_L                                     " & vbNewLine _
            & "          ,MIN(OUTKA_NO_M) AS OUTKA_NO_M                  " & vbNewLine _
            & "       FROM $LM_TRN$..C_OUTKA_M                           " & vbNewLine _
            & "       WHERE SYS_DEL_FLG = '0'                            " & vbNewLine _
            & "       GROUP BY                                           " & vbNewLine _
            & "           NRS_BR_CD                                      " & vbNewLine _
            & "          ,OUTKA_NO_L                                     " & vbNewLine _
            & "       ) COM_MIN                                          " & vbNewLine _
            & " 	  ON COM_MIN.NRS_BR_CD     =  COL.NRS_BR_CD          " & vbNewLine _
            & " 	 AND COM_MIN.OUTKA_NO_L    =  COL.OUTKA_NO_L         " & vbNewLine _
            & " 	LEFT JOIN $LM_TRN$..C_OUTKA_M COM		             " & vbNewLine _
            & " 	  ON COM.NRS_BR_CD        =  COM_MIN.NRS_BR_CD       " & vbNewLine _
            & " 	 AND COM.OUTKA_NO_L       =  COM_MIN.OUTKA_NO_L      " & vbNewLine _
            & " 	 AND COM.OUTKA_NO_M       =  COM_MIN.OUTKA_NO_M      " & vbNewLine _
            & " 	LEFT  JOIN $LM_MST$..M_GOODS MGS                     " & vbNewLine _
            & " 	  ON   MGS.NRS_BR_CD        =  COM.NRS_BR_CD         " & vbNewLine _
            & " 	 AND   MGS.GOODS_CD_NRS     =  COM.GOODS_CD_NRS      " & vbNewLine _
            & " 	 AND   MGS.SYS_DEL_FLG      =  '0'                   " & vbNewLine _
            & " 	WHERE COL.NRS_BR_CD    =  @NRS_BR_CD                 " & vbNewLine _
            & "       AND COL.OUTKA_NO_L   =  @OUTKA_NO_L     		     " & vbNewLine _
            & " 	  AND COL.SYS_DEL_FLG  =  '0'		                 " & vbNewLine

#End Region


#End Region

#Region "更新 SQL"

#Region "ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV作成"

    Private Const SQL_UPDATE_KANGAROO_CSV As String = "UPDATE $LM_TRN$..C_OUTKA_L SET              " & vbNewLine _
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
    ''' ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV作成対象検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>エスラインCSV作成対象データ検索件数取得SQLの構築・発行</remarks>
    Private Function SelectKangarooMagicCsv(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC920IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC920DAC.SQL_SELECT_KANGAROO_CSV)
        Me._StrSql.Append(LMC920DAC.SQL_SELECT_KANGAROO_CSV_FROM)

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

        MyBase.Logger.WriteSQLLog("LMC920DAC", "SelectKangarooMagicCsv", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("AD_TEL", "AD_TEL")
        map.Add("ZIP", "ZIP")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
        map.Add("OUTKAL_REMARK", "OUTKAL_REMARK")
        map.Add("UNSOL_REMARK", "UNSOL_REMARK")
        map.Add("NHS_REMARK", "NHS_REMARK")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("CODE_TEL", "CODE_TEL")
        map.Add("TEL", "TEL")
        map.Add("AD1_AD2", "AD1_AD2")
        map.Add("SOKO_WH_NM", "SOKO_WH_NM")
        map.Add("JYURYO", "JYURYO")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("ROW_NO", "ROW_NO")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("FILEPATH", "FILEPATH")
        map.Add("FILENAME", "FILENAME")
        map.Add("SYS_DATE", "SYS_DATE")
        map.Add("SYS_TIME", "SYS_TIME")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("GOODS_NM", "GOODS_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC920OUT")

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables("LMC920OUT").Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 出荷Lテーブル更新（ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV作成時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateKangarooCsv(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Me._Row = ds.Tables("LMC920OUT").Rows(0)

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Call Me.SetParamCommonSystemUp()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row("OUTKA_NO_L").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMC920DAC.SQL_UPDATE_KANGAROO_CSV, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC920DAC", "UpdateKangarooCsv", cmd)

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
