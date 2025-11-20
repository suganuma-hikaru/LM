' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC796    : 取扱説明書ラベル
'  作  成  者       :  inoue
' ==========================================================================
Option Strict On
Option Explicit On

Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC796DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC796DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    Private Const PTN_ID As String = "C0"

    Class TABLE_NM
        Public Const INPUT As String = "LMC796IN"
        Public Const OUTPUT As String = "LMC796OUT"
        Public Const M_RPT As String = "M_RPT"

    End Class

    Class COLUM_NM
        Public Const NRS_BR_CD As String = "NRS_BR_CD"
        Public Const OUTKA_NO_L As String = "OUTKA_NO_L"
        Public Const PTN_ID As String = "PTN_ID"
        Public Const PTN_CD As String = "PTN_CD"
        Public Const RPT_ID As String = "RPT_ID"
        Public Const PRT_PTN As String = "PRT_PTN"      'ADD 2018/06/07 依頼番号 : 000574  

        Public Const CUST_ORD_NO As String = "CUST_ORD_NO"
        Public Const GOODS_CD_NRS As String = "GOODS_CD_NRS"
        Public Const GOODS_CD_CUST As String = "GOODS_CD_CUST"
        Public Const GOODS_NM_1 As String = "GOODS_NM_1"
        Public Const LOT_NO As String = "LOT_NO"
        Public Const IRIME As String = "IRIME"
        Public Const IRIME_UT As String = "IRIME_UT"
        Public Const OUTKA_TTL_NB As String = "OUTKA_TTL_NB"
        Public Const OUTKA_TTL_QT As String = "OUTKA_TTL_QT"
        Public Const HASU As String = "HASU"
        Public Const OUTKA_PKG_NB As String = "OUTKA_PKG_NB"
        Public Const STD_IRIME_NB As String = "STD_IRIME_NB"
        Public Const STD_IRIME_UT As String = "STD_IRIME_UT"
        Public Const STD_WT_KGS As String = "STD_WT_KGS"
        Public Const PKG_NB As String = "PKG_NB"
        Public Const PKG_UT As String = "PKG_UT"
        Public Const NET As String = "NET"
        Public Const CUST_NM_L As String = "CUST_NM_L"
        Public Const CUST_NM_M As String = "CUST_NM_M"      'ADD 2020/03/10 11112 
        Public Const CUST_NM_S As String = "CUST_NM_S"      'ADD 2020/03/10 11112 
        Public Const CUST_NM_SS As String = "CUST_NM_SS"    'ADD 2020/03/10 11112 
        Public Const AD_1 As String = "AD_1"
        Public Const AD_2 As String = "AD_2"                'ADD 2020/03/10 11112
        Public Const AD_3 As String = "AD_3"                'ADD 2020/03/10 11112
        Public Const TEL As String = "TEL"
        Public Const FAX As String = "FAX"                  'ADD 2020/03/10 11112
        Public Const ATSUKAI_JIKOU As String = "ATSUKAI_JIKOU"
        Public Const SHOBO_CD As String = "SHOBO_CD"


    End Class

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String _
        = " SELECT DISTINCT                                                       " & vbNewLine _
        & "        CL.NRS_BR_CD                                     AS NRS_BR_CD  " & vbNewLine _
        & "      , @PTN_ID                                          AS PTN_ID     " & vbNewLine _
        & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD               " & vbNewLine _
        & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD               " & vbNewLine _
        & "        ELSE MR3.PTN_CD                                                " & vbNewLine _
        & "        END                                              AS PTN_CD     " & vbNewLine _
        & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID               " & vbNewLine _
        & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID               " & vbNewLine _
        & "        ELSE MR3.RPT_ID                                                " & vbNewLine _
        & "        END                                              AS RPT_ID     " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用SELECT区
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String _
        = " SELECT                                                                                            " & vbNewLine _
        & "        CASE WHEN MR2.RPT_ID IS NOT NULL                                                           " & vbNewLine _
        & "             THEN MR2.RPT_ID                                                                       " & vbNewLine _
        & "             WHEN MR1.RPT_ID IS NOT NULL                                                           " & vbNewLine _
        & "             THEN MR1.RPT_ID                                                                       " & vbNewLine _
        & "             ELSE MR3.RPT_ID                                                                       " & vbNewLine _
        & "        END                                                                AS RPT_ID               " & vbNewLine _
        & "      , CM.NRS_BR_CD                                                       AS NRS_BR_CD            " & vbNewLine _
        & "      , CM.OUTKA_NO_L                                                      AS OUTKA_NO_L           " & vbNewLine _
        & "      , CL.CUST_ORD_NO                                                     AS CUST_ORD_NO          " & vbNewLine _
        & "      , CM.GOODS_CD_NRS                                                    AS GOODS_CD_NRS         " & vbNewLine _
        & "      , MG.GOODS_CD_CUST                                                   AS GOODS_CD_CUST        " & vbNewLine _
        & "      , MG.GOODS_NM_1                                                      AS GOODS_NM_1           " & vbNewLine _
        & "      , CM.IRIME                                                           AS IRIME                " & vbNewLine _
        & "      , CM.LOT_NO                                                          AS LOT_NO               " & vbNewLine _
        & "      , ROUND((SUM(CM.CALC_TTL_QT) * MG.STD_WT_KGS) / MG.STD_IRIME_NB, 3)  AS NET                  " & vbNewLine _
        & "--UPD 2018/06/07       , CM.OUTKA_TTL_NB                                                    AS OUTKA_TTL_NB         " & vbNewLine _
        & "      , CASE @PRT_PTN                                                                              " & vbNewLine _
        & "     	    WHEN '1' THEN CM.OUTKA_TTL_NB                                                         " & vbNewLine _
        & "     	    WHEN '2' THEN '1'                                                                     " & vbNewLine _
        & "     	    WHEN '3' THEN CM.OUTKA_HASU                                                           " & vbNewLine _
        & "     	END                                                 AS OUTKA_TTL_NB                       " & vbNewLine _
        & "      , MG.STD_IRIME_NB                                                    AS STD_IRIME_NB         " & vbNewLine _
        & "      , MG.STD_IRIME_UT                                                    AS STD_IRIME_UT         " & vbNewLine _
        & "      , MG.STD_WT_KGS                                                      AS STD_WT_KGS           " & vbNewLine _
        & "      , MG.PKG_NB                                                          AS PKG_NB               " & vbNewLine _
        & "      , MG.PKG_UT                                                          AS PKG_UT               " & vbNewLine _
        & "      , MG.SHOBO_CD                                                        AS SHOBO_CD             " & vbNewLine _
        & "      , ISNULL(SB.ATSUKAI_JIKOU, '')                                       AS ATSUKAI_JIKOU        " & vbNewLine _
        & "      , MC.CUST_NM_L                                                       AS CUST_NM_L            " & vbNewLine _
        & "      , MC.CUST_NM_M                                                       AS CUST_NM_M  --ADD 2020/03/10 11112  " & vbNewLine _
        & "      , MC.CUST_NM_S                                                       AS CUST_NM_S  --ADD 2020/03/10 01112  " & vbNewLine _
        & "      , MC.CUST_NM_SS                                                      AS CUST_NM_SS --ADD 2020/03/10 01112  " & vbNewLine _
        & "      , MC.AD_1                                                            AS AD_1                 " & vbNewLine _
        & "      , MC.AD_2                                                            AS AD_2       --ADD 2020/03/10 01112  " & vbNewLine _
        & "      , MC.AD_3                                                            AS AD_3       --ADD 2020/03/10 01112  " & vbNewLine _
        & "      , MC.TEL                                                             AS TEL                  " & vbNewLine _
        & "      , MC.FAX                                                             AS FAX        --ADD 2020/03/10 01112  " & vbNewLine




    ''' <summary>
    ''' 印刷データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String _
        = "   FROM                                                           " & vbNewLine _
        & "        $LM_TRN$..C_OUTKA_L AS CL                                 " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        (SELECT                                                   " & vbNewLine _
        & "                NRS_BR_CD                                         " & vbNewLine _
        & "              , OUTKA_NO_L                                        " & vbNewLine _
        & "              , GOODS_CD_NRS                                      " & vbNewLine _
        & "              , LOT_NO                                            " & vbNewLine _
        & "              , IRIME                                             " & vbNewLine _
        & "              , IRIME * SUM(OUTKA_TTL_NB) AS CALC_TTL_QT          " & vbNewLine _
        & "              , SUM(OUTKA_TTL_NB)         AS OUTKA_TTL_NB         " & vbNewLine _
        & "              , SUM(OUTKA_TTL_QT)         AS OUTKA_TTL_QT         " & vbNewLine _
        & "              , SUM(OUTKA_HASU)           AS OUTKA_HASU           " & vbNewLine _
        & "              , SUM(OUTKA_PKG_NB)         AS OUTKA_PKG_NB         " & vbNewLine _
        & "           FROM                                                   " & vbNewLine _
        & "                $LM_TRN$..C_OUTKA_M                               " & vbNewLine _
        & "          WHERE                                                   " & vbNewLine _
        & "                NRS_BR_CD   = @NRS_BR_CD                          " & vbNewLine _
        & "            AND OUTKA_NO_L  = @OUTKA_NO_L                         " & vbNewLine _
        & "            AND SYS_DEL_FLG = '0'                                 " & vbNewLine _
        & "          GROUP BY                                                " & vbNewLine _
        & "                NRS_BR_CD                                         " & vbNewLine _
        & "              , OUTKA_NO_L                                        " & vbNewLine _
        & "              , GOODS_CD_NRS                                      " & vbNewLine _
        & "              , IRIME                                             " & vbNewLine _
        & "              , LOT_NO                                            " & vbNewLine _
        & "                                        ) AS CM                   " & vbNewLine _
        & "     ON CM.OUTKA_NO_L  = CL.OUTKA_NO_L                            " & vbNewLine _
        & "    AND CM.NRS_BR_CD   = CL.NRS_BR_CD                             " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_GOODS AS MG                                   " & vbNewLine _
        & "     ON MG.GOODS_CD_NRS = CM.GOODS_CD_NRS                         " & vbNewLine _
        & "    AND MG.NRS_BR_CD    = CM.NRS_BR_CD                            " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_SHOBO AS SB                                   " & vbNewLine _
        & "     ON SB.SHOBO_CD     = MG.SHOBO_CD                             " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_CUST  AS MC                                   " & vbNewLine _
        & "     ON MC.NRS_BR_CD    = MG.NRS_BR_CD                            " & vbNewLine _
        & "    AND MC.CUST_CD_L    = MG.CUST_CD_L                            " & vbNewLine _
        & "    AND MC.CUST_CD_M    = MG.CUST_CD_M                            " & vbNewLine _
        & "    AND MC.CUST_CD_S    = MG.CUST_CD_S                            " & vbNewLine _
        & "    AND MC.CUST_CD_SS   = MG.CUST_CD_SS                           " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_CUST_RPT AS MCR1                              " & vbNewLine _
        & "     ON MCR1.NRS_BR_CD = CL.NRS_BR_CD                             " & vbNewLine _
        & "    AND MCR1.CUST_CD_L = CL.CUST_CD_L                             " & vbNewLine _
        & "    AND MCR1.CUST_CD_M = CL.CUST_CD_M                             " & vbNewLine _
        & "    AND MCR1.CUST_CD_S = '00'                                     " & vbNewLine _
        & "    AND MCR1.PTN_ID    = @PTN_ID                                  " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_RPT AS MR1                                    " & vbNewLine _
        & "     ON MR1.NRS_BR_CD   = MCR1.NRS_BR_CD                          " & vbNewLine _
        & "    AND MR1.PTN_ID      = MCR1.PTN_ID                             " & vbNewLine _
        & "    AND MR1.PTN_CD      = MCR1.PTN_CD                             " & vbNewLine _
        & "    AND MR1.SYS_DEL_FLG = '0'                                     " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_CUST_RPT AS MCR2                              " & vbNewLine _
        & "     ON MCR2.NRS_BR_CD = MG.NRS_BR_CD                             " & vbNewLine _
        & "    AND MCR2.CUST_CD_L = MG.CUST_CD_L                             " & vbNewLine _
        & "    AND MCR2.CUST_CD_M = MG.CUST_CD_M                             " & vbNewLine _
        & "    AND MCR2.CUST_CD_S = MG.CUST_CD_S                             " & vbNewLine _
        & "    AND MCR2.PTN_ID    = @PTN_ID                                  " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_RPT AS MR2                                    " & vbNewLine _
        & "     ON MR2.NRS_BR_CD   = MCR2.NRS_BR_CD                          " & vbNewLine _
        & "    AND MR2.PTN_ID      = MCR2.PTN_ID                             " & vbNewLine _
        & "    AND MR2.PTN_CD      = MCR2.PTN_CD                             " & vbNewLine _
        & "    AND MR2.SYS_DEL_FLG = '0'                                     " & vbNewLine _
        & "   LEFT JOIN                                                      " & vbNewLine _
        & "        $LM_MST$..M_RPT AS MR3                                    " & vbNewLine _
        & "     ON MR3.NRS_BR_CD      = CL.NRS_BR_CD                         " & vbNewLine _
        & "    AND MR3.PTN_ID         = @PTN_ID                              " & vbNewLine _
        & "    AND MR3.STANDARD_FLAG  = '01'                                 " & vbNewLine _
        & "    AND MR3.SYS_DEL_FLG    = '0'                                  " & vbNewLine

#If False Then  'UPD 2018/06/07 2:端数処理時、0は印刷しないようにする
        Private Const SQL_WHERE As String _
        = "  WHERE                                                           " & vbNewLine _
        & "        CL.NRS_BR_CD   = @NRS_BR_CD                               " & vbNewLine _
        & "    AND CL.OUTKA_NO_L  = @OUTKA_NO_L                              " & vbNewLine _
        & "    AND CL.SYS_DEL_FLG = '0'                                      " & vbNewLine
#Else
    Private Const SQL_WHERE As String _
    = "  WHERE                                                           " & vbNewLine _
    & "        CL.NRS_BR_CD   = @NRS_BR_CD                               " & vbNewLine _
    & "    AND CL.OUTKA_NO_L  = @OUTKA_NO_L                              " & vbNewLine _
    & "    AND CL.SYS_DEL_FLG = '0'                                      " & vbNewLine _
    & "    AND CL.SYS_DEL_FLG = '0'                                      " & vbNewLine _
    & "    AND (( @PRT_PTN = '3'  AND CM.OUTKA_HASU  <> 0 )              " & vbNewLine _
    & "      OR ( @PRT_PTN <> '3' ))                                     " & vbNewLine

#End If


    Private Const SQL_GROUP_BY As String _
        = "  GROUP BY                                                   " & vbNewLine _
        & "        MR1.RPT_ID                                           " & vbNewLine _
        & "      , MR2.RPT_ID                                           " & vbNewLine _
        & "      , MR3.RPT_ID                                           " & vbNewLine _
        & "      , CM.NRS_BR_CD                                         " & vbNewLine _
        & "      , CM.OUTKA_NO_L                                        " & vbNewLine _
        & "      , CL.CUST_ORD_NO                                       " & vbNewLine _
        & "      , CM.GOODS_CD_NRS                                      " & vbNewLine _
        & "      , MG.GOODS_CD_CUST                                     " & vbNewLine _
        & "      , MG.GOODS_NM_1                                        " & vbNewLine _
        & "      , CM.IRIME                                             " & vbNewLine _
        & "      , CM.LOT_NO                                            " & vbNewLine _
        & "      , CM.OUTKA_TTL_NB                                      " & vbNewLine _
        & "      , CM.OUTKA_HASU  --ADD 2018/06/07                      " & vbNewLine _
        & "      , MG.STD_IRIME_NB                                      " & vbNewLine _
        & "      , MG.STD_IRIME_UT                                      " & vbNewLine _
        & "      , MG.STD_WT_KGS                                        " & vbNewLine _
        & "      , MG.PKG_NB                                            " & vbNewLine _
        & "      , MG.PKG_UT                                            " & vbNewLine _
        & "      , MG.SHOBO_CD                                          " & vbNewLine _
        & "      , SB.ATSUKAI_JIKOU                                     " & vbNewLine _
        & "      , MC.CUST_NM_L                                         " & vbNewLine _
        & "      , MC.AD_1                                              " & vbNewLine _
        & "      , MC.TEL                                               " & vbNewLine _
        & "      , MC.FAX            --ADD 2020/03/10 011112            " & vbNewLine _
        & "      , MC.CUST_NM_M      --ADD 2020/03/10 011112            " & vbNewLine _
        & "      , MC.CUST_NM_S      --ADD 2020/03/10 011112            " & vbNewLine _
        & "      , MC.CUST_NM_SS     --ADD 2020/03/10 011112            " & vbNewLine _
        & "      , MC.AD_2           --ADD 2020/03/10 011112            " & vbNewLine _
        & "      , MC.AD_3           --ADD 2020/03/10 011112            " & vbNewLine

    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String _
        = "  ORDER BY                                                   " & vbNewLine _
        & "        CM.OUTKA_NO_L                                        " & vbNewLine _
        & "      , GOODS_CD_CUST                                        " & vbNewLine _
        & "      , LOT_NO                                               " & vbNewLine _
        & "      , IRIME DESC                                           " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.INPUT)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC796DAC.SQL_SELECT_MPrt)
        Me._StrSql.Append(LMC796DAC.SQL_FROM)
        Me._StrSql.Append(LMC796DAC.SQL_WHERE)

        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item(COLUM_NM.NRS_BR_CD).ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item(COLUM_NM.OUTKA_NO_L).ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", LMC796DAC.PTN_ID, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_PTN", Me._Row.Item(COLUM_NM.PRT_PTN).ToString(), DBDataType.CHAR))      'ADD 2018/06/07 

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString() _
                                         , Me._Row.Item(COLUM_NM.NRS_BR_CD).ToString())

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(Me.GetType.Name _
                                    , System.Reflection.MethodBase.GetCurrentMethod().Name _
                                    , cmd)

            Dim selectCount As Integer = 0

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If (reader.HasRows) Then

                    '取得データの格納先をマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In New String() _
                        {
                            COLUM_NM.NRS_BR_CD,
                            COLUM_NM.PTN_ID,
                            COLUM_NM.PTN_CD,
                            COLUM_NM.RPT_ID
                        }

                        map.Add(item, item)
                    Next

                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.M_RPT)

                    selectCount = ds.Tables(TABLE_NM.M_RPT).Rows.Count

                End If

            End Using

            Me.SetResultCount(selectCount)

        End Using

        Return ds

    End Function


    ''' <summary>
    ''' 出荷指示書出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷指示書出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.INPUT)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC796DAC.SQL_SELECT_DATA)
        Me._StrSql.Append(LMC796DAC.SQL_FROM)
        Me._StrSql.Append(LMC796DAC.SQL_WHERE)
        Me._StrSql.Append(LMC796DAC.SQL_GROUP_BY)
        Me._StrSql.Append(LMC796DAC.SQL_ORDER_BY)


        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item(COLUM_NM.NRS_BR_CD).ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item(COLUM_NM.OUTKA_NO_L).ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_ID", LMC796DAC.PTN_ID, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_PTN", Me._Row.Item(COLUM_NM.PRT_PTN).ToString(), DBDataType.CHAR))          'ADD 2018/06/07 依頼番号 : 000574  

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString() _
                                         , Me._Row.Item(COLUM_NM.NRS_BR_CD).ToString())

        'SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)
            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(Me.GetType.Name _
                                    , System.Reflection.MethodBase.GetCurrentMethod().Name _
                                    , cmd)

            Dim selectCount As Integer = 0

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                For Each item As String In New String() _
                    {
                        COLUM_NM.RPT_ID,
                        COLUM_NM.NRS_BR_CD,
                        COLUM_NM.OUTKA_NO_L,
                        COLUM_NM.CUST_ORD_NO,
                        COLUM_NM.GOODS_CD_NRS,
                        COLUM_NM.GOODS_CD_CUST,
                        COLUM_NM.GOODS_NM_1,
                        COLUM_NM.IRIME,
                        COLUM_NM.LOT_NO,
                        COLUM_NM.NET,
                        COLUM_NM.OUTKA_TTL_NB,
                        COLUM_NM.STD_IRIME_NB,
                        COLUM_NM.STD_IRIME_UT,
                        COLUM_NM.STD_WT_KGS,
                        COLUM_NM.PKG_NB,
                        COLUM_NM.PKG_UT,
                        COLUM_NM.SHOBO_CD,
                        COLUM_NM.ATSUKAI_JIKOU,
                        COLUM_NM.CUST_NM_L,
                        COLUM_NM.AD_1,
                        COLUM_NM.AD_2,
                        COLUM_NM.AD_3,
                        COLUM_NM.TEL,
                        COLUM_NM.FAX
                    }

                    map.Add(item, item)
                Next

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.OUTPUT)

            End Using

            Me.SetResultCount(selectCount)

        End Using


        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
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
                Me._StrSql.Append(" OUTKA_L.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_L.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_L.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '出荷日
            whereStr = .Item("OUTKA_PLAN_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_L.OUTKA_PLAN_DATE = @OUTKA_PLAN_DATE ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", whereStr, DBDataType.CHAR))
            End If

            '"JAL"を含むこと
            Me._StrSql.Append(" AND OUTKA_L.DEST_AD_3 LIKE '%JAL%' ")
            Me._StrSql.Append(vbNewLine)

        End With

    End Sub


    Private Sub SetConditionMasterPatternSQL()

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
                Me._StrSql.Append(" OUTKA_L.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_L.CUST_CD_L = @CUST_CD_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND OUTKA_L.CUST_CD_M = @CUST_CD_M")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
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
