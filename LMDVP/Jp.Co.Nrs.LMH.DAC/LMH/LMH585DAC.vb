' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH585    : EDI納品書(日興産業用) ｲｴﾛｰﾊｯﾄ
'  作  成  者       :  大貫和正
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH585DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH585DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_MPrt_SELECT As String = " SELECT DISTINCT                                                      " & vbNewLine _
                                            & "	       NKS.NRS_BR_CD                                    AS NRS_BR_CD " & vbNewLine _
                                            & "      , 'B8'                                             AS PTN_ID    " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD              " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD              " & vbNewLine _
                                            & "        ELSE MR3.PTN_CD END                              AS PTN_CD    " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID              " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID              " & vbNewLine _
                                            & "        ELSE MR3.RPT_ID END                              AS RPT_ID    " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = " SELECT                                                            " & vbNewLine _
                                       & "        CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID           " & vbNewLine _
                                       & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID           " & vbNewLine _
                                       & "        ELSE MR3.RPT_ID                                            " & vbNewLine _
                                       & "        END                              AS RPT_ID                 " & vbNewLine _
                                       & "      , NKS.DEL_KB                       AS DEL_KB                 " & vbNewLine _
                                       & "      , NKS.CRT_DATE                     AS CRT_DATE               " & vbNewLine _
                                       & "      , NKS.FILE_NAME                    AS FILE_NAME              " & vbNewLine _
                                       & "      , NKS.REC_NO                       AS REC_NO                 " & vbNewLine _
                                       & "      , NKS.GYO                          AS GYO                    " & vbNewLine _
                                       & "      , NKS.NRS_BR_CD                    AS NRS_BR_CD              " & vbNewLine _
                                       & "      , NKS.EDI_CTL_NO                   AS EDI_CTL_NO             " & vbNewLine _
                                       & "      , NKS.EDI_CTL_NO_CHU               AS EDI_CTL_NO_CHU         " & vbNewLine _
                                       & "      , NKS.OUTKA_CTL_NO                 AS OUTKA_CTL_NO           " & vbNewLine _
                                       & "      , NKS.OUTKA_CTL_NO_CHU             AS OUTKA_CTL_NO_CHU       " & vbNewLine _
                                       & "      , NKS.CUST_CD_L                    AS CUST_CD_L              " & vbNewLine _
                                       & "      , NKS.CUST_CD_M                    AS CUST_CD_M              " & vbNewLine _
                                       & "      , NKS.DENPYO_NO                    AS DENPYO_NO              " & vbNewLine _
                                       & "      , NKS.TOKUISAKI_CD                 AS TOKUISAKI_CD           " & vbNewLine _
                                       & "      , NKS.TOKUISAKI_NM                 AS TOKUISAKI_NM           " & vbNewLine _
                                       & "--      , NKS.JYUCYU_NO                    AS JYUCYU_NO              " & vbNewLine _
                                       & "--      , CASE ISNUMERIC(NKS.JYUCYU_NO) WHEN 1 THEN CONVERT(VARCHAR,(CONVERT(NUMERIC,NKS.JYUCYU_NO))) ELSE JYUCYU_NO END                    AS JYUCYU_NO              " & vbNewLine _
                                       & "      , CASE ISNUMERIC(RIGHT(NKS.JYUCYU_NO,6)) WHEN 1 THEN CONVERT(VARCHAR,(CONVERT(NUMERIC,RIGHT(NKS.JYUCYU_NO,6)))) ELSE JYUCYU_NO END  AS JYUCYU_NO " & vbNewLine _
                                       & "      , NKS.NKS_GYO_NO                   AS NKS_GYO_NO             " & vbNewLine _
                                       & "      , NKS.AITESAKI_CYUMON_NO           AS AITESAKI_CYUMON_NO     " & vbNewLine _
                                       & "      , NKS.NONYUSAKI_CD                 AS NONYUSAKI_CD           " & vbNewLine _
                                       & "      , NKS.NONYUSAKI_NM                 AS NONYUSAKI_NM           " & vbNewLine _
                                       & "      , NKS.HINMOKU_CD                   AS HINMOKU_CD             " & vbNewLine _
                                       & "      , NKS.HINMOKU_NM                   AS HINMOKU_NM             " & vbNewLine _
                                       & "      , NKS.AITE_HINMOKU_CD              AS AITE_HINMOKU_CD        " & vbNewLine _
                                       & "      , NKS.AITE_HINMOKU_NM              AS AITE_HINMOKU_NM        " & vbNewLine _
                                       & "      --(2013.01.09)要望番号1754 -- START --                       " & vbNewLine _
                                       & "      --, NKS.L_KANSANCHI * NKS.SURYO    AS KANSAN_SURYO           " & vbNewLine _
                                       & "      , NKS.KANSAN_SURYO                 AS KANSAN_SURYO           " & vbNewLine _
                                       & "      --(2013.01.09)要望番号1754 --  END  --                       " & vbNewLine _
                                       & "      , NKS.OUTKA_PLAN_DATE              AS OUTKA_PLAN_DATE        " & vbNewLine _
                                       & "      , NKS.DENPYO_OUTKA_BASHO           AS DENPYO_OUTKA_BASHO     " & vbNewLine _
                                       & "      , NKS.DENPYO_OUTKA_AD1             AS DENPYO_OUTKA_AD1       " & vbNewLine _
                                       & "      , NKS.DENPYO_OUTKA_AD2             AS DENPYO_OUTKA_AD2       " & vbNewLine _
                                       & "      , NKS.DENPYO_OUTKA_AD3             AS DENPYO_OUTKA_AD3       " & vbNewLine _
                                       & "      , NKS.DENPYO_OUTKA_TEL             AS DENPYO_OUTKA_TEL       " & vbNewLine _
                                       & "      , NKS.DENPYO_OUTKA_FAX             AS DENPYO_OUTKA_FAX       " & vbNewLine _
                                       & "      , NKS.DENPYO_OUTKA_ZIP             AS DENPYO_OUTKA_ZIP       " & vbNewLine _
                                       & "      , NKS.TENPO_CD                     AS TENPO_CD               " & vbNewLine _
                                       & "      , NKS.CODE_FREE1                   AS CODE_FREE1             " & vbNewLine _
                                       & "      , NKS.CODE_FREE2                   AS CODE_FREE2             " & vbNewLine _
                                       & "      , NKS.URI_TANKA                    AS URI_TANKA              " & vbNewLine _
                                       & "      , NKS.URI_TANKA2                   AS URI_TANKA2             " & vbNewLine _
                                       & "      , NKS.URI_KINGAKU2                 AS URI_KINGAKU2           " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用 FROM句
    ''' </summary>
    ''' <remarks>
    ''' 大阪 日興産業 出荷EDIデータ
    ''' </remarks>
    Private Const SQL_FROM As String = "  FROM $LM_TRN$..H_OUTKAEDI_DTL_NKS NKS                               " & vbNewLine _
                                     & "       -- 商品マスタ                                                  " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_GOODS M_GOODS                      " & vbNewLine _
                                     & "                    ON M_GOODS.NRS_BR_CD      = NKS.NRS_BR_CD         " & vbNewLine _
                                     & "                   AND M_GOODS.GOODS_CD_NRS   = NKS.HINMOKU_CD        " & vbNewLine _
                                     & "       -- EDI印刷種別テーブル                                         " & vbNewLine _
                                     & "       LEFT JOIN $LM_TRN$..H_EDI_PRINT EDI_PRT                        " & vbNewLine _
                                     & "              ON EDI_PRT.NRS_BR_CD = NKS.NRS_BR_CD                    " & vbNewLine _
                                     & "             AND EDI_PRT.EDI_CTL_NO = NKS.EDI_CTL_NO                  " & vbNewLine _
                                     & "             AND EDI_PRT.INOUT_KB = '0'                               " & vbNewLine _
                                     & "       LEFT JOIN (                                                    " & vbNewLine _
                                     & "                   SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT            " & vbNewLine _
                                     & "                        , H_EDI_PRINT.NRS_BR_CD                       " & vbNewLine _
                                     & "                        , H_EDI_PRINT.EDI_CTL_NO                      " & vbNewLine _
                                     & "                        , H_EDI_PRINT.DENPYO_NO                       " & vbNewLine _
                                     & "                     FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT           " & vbNewLine _
                                     & "                    WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD        " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L        " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M        " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.PRINT_TP    = '11'              " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB         " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.SYS_DEL_FLG = '0'               " & vbNewLine _
                                     & "                    GROUP BY                                          " & vbNewLine _
                                     & "                          H_EDI_PRINT.NRS_BR_CD                       " & vbNewLine _
                                     & "                        , H_EDI_PRINT.EDI_CTL_NO                      " & vbNewLine _
                                     & "                        , H_EDI_PRINT.DENPYO_NO                       " & vbNewLine _
                                     & "                 ) HEDIPRINT                                          " & vbNewLine _
                                     & "              ON HEDIPRINT.NRS_BR_CD  = NKS.NRS_BR_CD                 " & vbNewLine _
                                     & "             AND HEDIPRINT.EDI_CTL_NO = NKS.EDI_CTL_NO                " & vbNewLine _
                                     & "             AND HEDIPRINT.DENPYO_NO  = NKS.DENPYO_NO                 " & vbNewLine _
                                     & "       -- 帳票パターンマスタ①(OUTKAEDI_HED_DICの荷主より取得)        " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1                " & vbNewLine _
                                     & "                    ON M_CUSTRPT1.NRS_BR_CD   = NKS.NRS_BR_CD         " & vbNewLine _
                                     & "                   AND M_CUSTRPT1.CUST_CD_L   = NKS.CUST_CD_L         " & vbNewLine _
                                     & "                   AND M_CUSTRPT1.CUST_CD_M   = NKS.CUST_CD_M         " & vbNewLine _
                                     & "                   AND M_CUSTRPT1.CUST_CD_S   = '00'                  " & vbNewLine _
                                     & "                   AND M_CUSTRPT1.PTN_ID      = 'B8'                  " & vbNewLine _
                                     & "                   AND M_CUSTRPT1.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                           " & vbNewLine _
                                     & "                    ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD  " & vbNewLine _
                                     & "                   AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID     " & vbNewLine _
                                     & "                   AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD     " & vbNewLine _
                                     & "                   AND MR1.SYS_DEL_FLG        = '0'                   " & vbNewLine _
                                     & "       -- 帳票パターンマスタ②(商品マスタより)                        " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2                " & vbNewLine _
                                     & "                    ON M_CUSTRPT2.NRS_BR_CD   = M_GOODS.NRS_BR_CD     " & vbNewLine _
                                     & "                   AND M_CUSTRPT2.CUST_CD_L   = M_GOODS.CUST_CD_L     " & vbNewLine _
                                     & "                   AND M_CUSTRPT2.CUST_CD_M   = M_GOODS.CUST_CD_M     " & vbNewLine _
                                     & "                   AND M_CUSTRPT2.CUST_CD_S   = '00'                  " & vbNewLine _
                                     & "                   AND M_CUSTRPT2.PTN_ID      = 'B8'                  " & vbNewLine _
                                     & "                   AND M_CUSTRPT2.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                           " & vbNewLine _
                                     & "                    ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD  " & vbNewLine _
                                     & "                   AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID     " & vbNewLine _
                                     & "                   AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD     " & vbNewLine _
                                     & "                   AND MR2.SYS_DEL_FLG        = '0'                   " & vbNewLine _
                                     & "       -- 帳票パターンマスタ③ <存在しない場合の帳票パターン取得 >    " & vbNewLine _
                                     & "       LEFT OUTER JOIN $LM_MST$..M_RPT MR3                            " & vbNewLine _
                                     & "                    ON MR3.NRS_BR_CD          =  NKS.NRS_BR_CD        " & vbNewLine _
                                     & "                   AND MR3.PTN_ID             = 'B8'                  " & vbNewLine _
                                     & "                   AND MR3.STANDARD_FLAG      = '01'                  " & vbNewLine _
                                     & "                   AND MR3.SYS_DEL_FLG        = '0'                   " & vbNewLine

    ''' <summary>                             
    ''' 印刷データ抽出用 ORDER BY句           
    ''' </summary>                            
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = " ORDER BY                    " & vbNewLine _
                                         & " --      NKS.OUTKA_CTL_NO      " & vbNewLine _
                                         & " --    , NKS.OUTKA_CTL_NO_CHU  " & vbNewLine _
                                         & " --    , NKS.TOKUISAKI_NM      " & vbNewLine _
                                         & "       NKS.JYUCYU_NO         " & vbNewLine _
                                         & "     , NKS.DENPYO_NO         " & vbNewLine _
                                         & "     , NKS.NKS_GYO_NO        " & vbNewLine _
                                         & "     , NKS.EDI_CTL_NO        " & vbNewLine _
                                         & "     , NKS.EDI_CTL_NO_CHU    " & vbNewLine



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

    ''' <summary>
    ''' ゼロフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ZERO_FLG As String = "0"


#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    '''帳票パターンマスタ データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>帳票パターンマスタデータ取得 SQLの構築・発行</remarks>
    Private Function SelectMPrintPattern(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH585IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH585DAC.SQL_MPrt_SELECT)    'SQL構築(帳票種別用SELECT句)
        Me._StrSql.Append(LMH585DAC.SQL_FROM)           'SQL構築(帳票種別用FROM句)
        If Me._Row.Item("PRTFLG").ToString = "1" Then   'SQL構築(印刷データ抽出用条件設定)
            Call Me.SetConditionMasterSQL_OUT()         '出力済
        Else
            Call Me.SetConditionMasterSQL()             '未出力・両方(出力済、未出力併せて)
        End If                                          
        'パラメータ設定
        Call Me.SetConditionPrintPatternMSQL(Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH585DAC", "SelectMPrt", cmd)

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
    ''' 日興産業EDI対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>日興産業EDI出荷 対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH585IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH585DAC.SQL_SELECT)      	'SQL構築(印刷データ抽出用 SELECT句)
        Me._StrSql.Append(LMH585DAC.SQL_FROM)        	'SQL構築(印刷データ抽出用 FROM句)
        If Me._Row.Item("PRTFLG").ToString = "1" Then	'SQL構築(印刷データ抽出条件)
            Call Me.SetConditionMasterSQL_OUT()         '出力済の場合
        Else
            Call Me.SetConditionMasterSQL()             '未出力・両方(出力済、未出力併せて)
        End If 

        'SQL構築(印刷データ抽出用条件設定)
        Me._StrSql.Append(LMH585DAC.SQL_ORDER_BY)    	'SQL構築(印刷データ抽出用 ORDER BY句)

        'パラメータ設定
        Call Me.SetConditionPrintPatternMSQL(Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH585DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("DENPYO_NO", "DENPYO_NO")
        map.Add("NKS_GYO_NO", "NKS_GYO_NO")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("JYUCYU_NO", "JYUCYU_NO")
        map.Add("TOKUISAKI_NM", "TOKUISAKI_NM")
        map.Add("AITESAKI_CYUMON_NO", "AITESAKI_CYUMON_NO")
        map.Add("NONYUSAKI_NM", "NONYUSAKI_NM")
        map.Add("HINMOKU_NM", "HINMOKU_NM")
        map.Add("AITE_HINMOKU_CD", "AITE_HINMOKU_CD")
        map.Add("KANSAN_SURYO", "KANSAN_SURYO")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("DENPYO_OUTKA_BASHO", "DENPYO_OUTKA_BASHO")
        map.Add("DENPYO_OUTKA_AD1", "DENPYO_OUTKA_AD1")
        map.Add("DENPYO_OUTKA_TEL", "DENPYO_OUTKA_TEL")
        map.Add("DENPYO_OUTKA_FAX", "DENPYO_OUTKA_FAX")
        map.Add("TENPO_CD", "TENPO_CD")
        map.Add("CODE_FREE1", "CODE_FREE1")
        map.Add("CODE_FREE2", "CODE_FREE2")
        map.Add("URI_TANKA", "URI_TANKA")
        map.Add("URI_TANKA2", "URI_TANKA2")
        map.Add("URI_KINGAKU2", "URI_KINGAKU2")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH585OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 帳票パターンＭ取得 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionPrintPatternMSQL(ByVal prmList As ArrayList)

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", Me._Row.Item("INOUT_KB").ToString(), DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール(出力済)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL_OUT()

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" NKS.NRS_BR_CD = @NRS_BR_CD")
                'Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '伝票№(オーダー№)
            whereStr = .Item("DENPYO_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                'Me._StrSql.Append(" AND NKS.DENPYO_NO = @DENPYO_NO ") 馬野さん依頼により修正 2013/01/10本明
                Me._StrSql.Append(" AND NKS.JYUCYU_NO = @DENPYO_NO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENPYO_NO", whereStr, DBDataType.NVARCHAR))
            End If

            'プリントフラグ
            whereStr = .Item("PRTFLG").ToString()
            Select Case whereStr
                Case "0"
                    '未出力
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT  = 0 OR HEDIPRINT.PRT_COUNT IS NULL) ")
                Case "1"
                    '出力済
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT >= 1 ) ")
            End Select
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", whereStr, DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 帳票出力 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        Me._StrSql.Append(" WHERE ")
        Me._StrSql.Append(vbNewLine)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定 ---------------------------------
        Dim whereStr As String = String.Empty

        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" NKS.NRS_BR_CD = @NRS_BR_CD")
                'Me._StrSql.Append(vbNewLine)
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            'EDI出荷予定日(FROM)
            whereStr = .Item("OUTKA_PLAN_DATE_FROM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND NKS.OUTKA_PLAN_DATE >= @OUTKA_PLAN_DATE_FROM ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE_FROM", whereStr, DBDataType.CHAR))
            End If

            'EDI出荷予定日(TO)
            whereStr = .Item("OUTKA_PLAN_DATE_TO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND NKS.OUTKA_PLAN_DATE <= @OUTKA_PLAN_DATE_TO ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE_TO", whereStr, DBDataType.CHAR))
            End If

            'プリントフラグ
            whereStr = .Item("PRTFLG").ToString()
            Select Case whereStr
                Case "0"
                    '未出力
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT  = 0 OR HEDIPRINT.PRT_COUNT IS NULL) ")
                Case "1"
                    '出力済
                    Me._StrSql.Append(" AND (HEDIPRINT.PRT_COUNT >= 1 ) ")
            End Select
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", whereStr, DBDataType.CHAR))

            'SYS_DEL_FLG
            Me._StrSql.Append(" AND NKS.SYS_DEL_FLG = '0' ")
            Me._StrSql.Append(vbNewLine)

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
