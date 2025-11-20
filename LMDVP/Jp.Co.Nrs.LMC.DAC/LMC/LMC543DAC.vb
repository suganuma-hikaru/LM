'  ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC543DAC : 送品案内書(FFEM)
'  作  成  者       :  
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC543DAC
''' </summary>
''' <remarks></remarks>
Public Class LMC543DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "SQL"

#Region "帳票パターン取得 SQL"

    ''' <summary>
    ''' 帳票パターン取得 SELECT 句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPRT_SELECT As String = "" _
            & "SELECT                                                   " & vbNewLine _
            & "      @NRS_BR_CD                            AS NRS_BR_CD " & vbNewLine _
            & "    , CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_ID   " & vbNewLine _
            & "        ELSE MR3.PTN_ID                                  " & vbNewLine _
            & "      END                                   AS PTN_ID    " & vbNewLine _
            & "    , CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD   " & vbNewLine _
            & "        ELSE MR3.PTN_CD                                  " & vbNewLine _
            & "      END                                   AS PTN_CD    " & vbNewLine _
            & "    , CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID   " & vbNewLine _
            & "        ELSE MR3.RPT_ID                                  " & vbNewLine _
            & "      END                                   AS RPT_ID    " & vbNewLine _
            & ""

#End Region

#Region "印刷データ取得"

    ''' <summary>
    ''' 印刷データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_PRINT_SELECT As String = "" _
            & "SELECT                                                 " & vbNewLine _
            & "      CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID " & vbNewLine _
            & "        ELSE MR3.RPT_ID                                " & vbNewLine _
            & "      END        AS RPT_ID                             " & vbNewLine _
            & "    , HIDF.REF_DEN_NO AS OFFLINE_NO                    " & vbNewLine _
            & "    , COL.OUTKA_PLAN_DATE AS OUTKA_DATE                " & vbNewLine _
            & "    , COL.ARR_PLAN_DATE AS ARR_DATE                    " & vbNewLine _
            & "    , DEST.ZIP                                         " & vbNewLine _
            & "    , DEST.AD_1 AS DEST_AD_1                           " & vbNewLine _
            & "    , DEST.AD_2 AS DEST_AD_2                           " & vbNewLine _
            & "    , DEST.AD_3 AS DEST_AD_3                           " & vbNewLine _
            & "    , DEST.DEST_NM AS COMP_NM                          " & vbNewLine _
            & "    , '' AS BUSYO_NM                                   " & vbNewLine _
            & "    , '' AS TANTO_NM                                   " & vbNewLine _
            & "    , DEST.TEL                                         " & vbNewLine _
            & "    , HODF.GOODS_NM                                    " & vbNewLine _
            & "    , COS.LOT_NO                                       " & vbNewLine _
            & "    , COS.OUTKA_TTL_NB AS INOUTKA_NB                   " & vbNewLine _
            & "    , KONDO.KBN_NM1 AS ONDO                            " & vbNewLine _
            & "    , CASE WHEN GOODS.DOKU_KB ='01' THEN ''            " & vbNewLine _
            & "        ELSE KDOKUGEKI.KBN_NM1                         " & vbNewLine _
            & "      END AS DOKUGEKI                                  " & vbNewLine _
            & "    , COL.REMARK                                       " & vbNewLine _
            & "    , UNSOCO.UNSOCO_NM AS HAISO                        " & vbNewLine _
            & "    , COM.REMARK AS REMARK_DTL                         " & vbNewLine _
            & "    , CASE WHEN HIN_FJF.DOKUGEKI_FLG ='X' THEN ' *' ELSE '' END AS DOKU_MARK " & vbNewLine _
            & "    , CASE WHEN HIN_FJF.EXPORT_REG_FLG ='X' THEN ' $' ELSE '' END AS EXP_MARK " & vbNewLine _
            & ""

#End Region ' "印刷データ取得 SQL"

#Region "帳票パターン取得/印刷データ取得 共通 SQL"

    Private Const SQL_SELECT_PRINT_FROM As String = "" _
            & "FROM                                                 " & vbNewLine _
            & "    $LM_TRN$..H_OUTKAEDI_L HOL                       " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_TRN$..H_OUTKAEDI_M HOM                       " & vbNewLine _
            & "        ON  HOM.NRS_BR_CD = HOL.NRS_BR_CD            " & vbNewLine _
            & "        AND HOM.EDI_CTL_NO = HOL.EDI_CTL_NO          " & vbNewLine _
            & "        AND HOM.SYS_DEL_FLG = '0'                    " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_TRN$..H_INOUTKAEDI_DTL_FJF HIDF              " & vbNewLine _
            & "        ON  HIDF.NRS_BR_CD = HOM.NRS_BR_CD           " & vbNewLine _
            & "        AND HIDF.EDI_CTL_NO = HOM.EDI_CTL_NO         " & vbNewLine _
            & "        AND HIDF.EDI_CTL_NO_CHU = HOM.EDI_CTL_NO_CHU " & vbNewLine _
            & "        AND HIDF.SYS_DEL_FLG = '0'                   " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_TRN$..H_OUTKAEDI_DTL_FJF HODF                " & vbNewLine _
            & "        ON  HODF.NRS_BR_CD = HOM.NRS_BR_CD           " & vbNewLine _
            & "        AND HODF.EDI_CTL_NO = HOM.EDI_CTL_NO         " & vbNewLine _
            & "        AND HODF.EDI_CTL_NO_CHU = HOM.EDI_CTL_NO_CHU " & vbNewLine _
            & "        AND HODF.SYS_DEL_FLG = '0'                   " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_TRN$..C_OUTKA_L COL                          " & vbNewLine _
            & "        ON  COL.NRS_BR_CD = HOL.NRS_BR_CD            " & vbNewLine _
            & "        AND COL.OUTKA_NO_L = HOL.OUTKA_CTL_NO        " & vbNewLine _
            & "        AND COL.SYS_DEL_FLG = '0'                    " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_TRN$..C_OUTKA_M COM                          " & vbNewLine _
            & "        ON  COM.NRS_BR_CD = HOM.NRS_BR_CD            " & vbNewLine _
            & "        AND COM.OUTKA_NO_L = HOM.OUTKA_CTL_NO        " & vbNewLine _
            & "        AND COM.OUTKA_NO_M = HOM.OUTKA_CTL_NO_CHU    " & vbNewLine _
            & "        AND COM.SYS_DEL_FLG = '0'                    " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_TRN$..C_OUTKA_S COS                          " & vbNewLine _
            & "        ON  COS.NRS_BR_CD = COM.NRS_BR_CD            " & vbNewLine _
            & "        AND COS.OUTKA_NO_L = COM.OUTKA_NO_L          " & vbNewLine _
            & "        AND COS.OUTKA_NO_M = COM.OUTKA_NO_M          " & vbNewLine _
            & "        AND COS.SYS_DEL_FLG = '0'                    " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_TRN$..F_UNSO_L FUL                           " & vbNewLine _
            & "        ON  FUL.NRS_BR_CD = COL.NRS_BR_CD            " & vbNewLine _
            & "        AND FUL.INOUTKA_NO_L = COL.OUTKA_NO_L        " & vbNewLine _
            & "        AND FUL.SYS_DEL_FLG = '0'                    " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_MST$..M_DEST DEST                            " & vbNewLine _
            & "        ON  DEST.NRS_BR_CD = COL.NRS_BR_CD           " & vbNewLine _
            & "        AND DEST.CUST_CD_L = COL.CUST_CD_L           " & vbNewLine _
            & "        AND DEST.DEST_CD = COL.DEST_CD               " & vbNewLine _
            & "        AND DEST.SYS_DEL_FLG = '0'                   " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_MST$..M_GOODS GOODS                          " & vbNewLine _
            & "        ON  GOODS.NRS_BR_CD = COM.NRS_BR_CD          " & vbNewLine _
            & "        AND GOODS.GOODS_CD_NRS = COM.GOODS_CD_NRS    " & vbNewLine _
            & "        AND GOODS.SYS_DEL_FLG = '0'                  " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_TRN$..M_HINMOKU_FJF HIN_FJF                  " & vbNewLine _
            & "        ON  HIN_FJF.NRS_BR_CD = COM.NRS_BR_CD        " & vbNewLine _
            & "        AND HIN_FJF.HINMOKU_CD = GOODS.GOODS_CD_CUST " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_MST$..M_UNSOCO UNSOCO                        " & vbNewLine _
            & "        ON  UNSOCO.NRS_BR_CD = FUL.NRS_BR_CD         " & vbNewLine _
            & "        AND UNSOCO.UNSOCO_CD = FUL.UNSO_CD           " & vbNewLine _
            & "        AND UNSOCO.UNSOCO_BR_CD = FUL.UNSO_BR_CD     " & vbNewLine _
            & "        AND UNSOCO.SYS_DEL_FLG = '0'                 " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_MST$..M_CUST CUST                            " & vbNewLine _
            & "        ON  CUST.NRS_BR_CD = HOL.NRS_BR_CD           " & vbNewLine _
            & "        AND CUST.CUST_CD_L = HOL.CUST_CD_L           " & vbNewLine _
            & "        AND CUST.CUST_CD_M = HOL.CUST_CD_M           " & vbNewLine _
            & "        AND CUST.CUST_CD_S = '00'                    " & vbNewLine _
            & "        AND CUST.CUST_CD_SS = '00'                   " & vbNewLine _
            & "        AND CUST.SYS_DEL_FLG = '0'                   " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_MST$..Z_KBN KONDO                            " & vbNewLine _
            & "        ON  KONDO.KBN_GROUP_CD = 'O002'              " & vbNewLine _
            & "        AND KONDO.KBN_CD = GOODS.ONDO_KB             " & vbNewLine _
            & "        AND KONDO.SYS_DEL_FLG = '0'                  " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_MST$..Z_KBN KDOKUGEKI                        " & vbNewLine _
            & "        ON  KDOKUGEKI.KBN_GROUP_CD = 'G001'          " & vbNewLine _
            & "        AND KDOKUGEKI.KBN_CD = GOODS.DOKU_KB         " & vbNewLine _
            & "        AND KDOKUGEKI.SYS_DEL_FLG = '0'              " & vbNewLine _
            & "-- 荷主帳票パターン                                  " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_MST$..M_CUST_RPT MCR1                        " & vbNewLine _
            & "        ON  MCR1.NRS_BR_CD = HOL.NRS_BR_CD           " & vbNewLine _
            & "        AND MCR1.CUST_CD_L = HOL.CUST_CD_L           " & vbNewLine _
            & "        AND MCR1.CUST_CD_M = HOL.CUST_CD_M           " & vbNewLine _
            & "        AND MCR1.CUST_CD_S = '00'                    " & vbNewLine _
            & "        AND MCR1.PTN_ID = 'F1'                       " & vbNewLine _
            & "-- 荷主帳票パターンに紐付く帳票パターン              " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_MST$..M_RPT MR1                              " & vbNewLine _
            & "        ON  MR1.NRS_BR_CD = MCR1.NRS_BR_CD           " & vbNewLine _
            & "        AND MR1.PTN_ID = MCR1.PTN_ID                 " & vbNewLine _
            & "        AND MR1.PTN_CD = MCR1.PTN_CD                 " & vbNewLine _
            & "        AND MR1.SYS_DEL_FLG = '0'                    " & vbNewLine _
            & "-- 存在しない場合の帳票パターン                      " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_MST$..M_RPT MR3                              " & vbNewLine _
            & "        ON  MR3.NRS_BR_CD = HOL.NRS_BR_CD            " & vbNewLine _
            & "        AND MR3.PTN_ID = 'F1'                        " & vbNewLine _
            & "        AND MR3.STANDARD_FLAG = '01'                 " & vbNewLine _
            & "        AND MR3.SYS_DEL_FLG = '0'                    " & vbNewLine _
            & ""

    Private Const SQL_SELECT_PRINT_WHERE As String = "" _
            & "WHERE                            " & vbNewLine _
            & "    HOL.NRS_BR_CD = @NRS_BR_CD   " & vbNewLine _
            & "AND COL.OUTKA_NO_L = @OUTKA_NO_L " & vbNewLine _
            & "AND HOL.SYS_DEL_FLG = '0'        " & vbNewLine _
            & "AND HIDF.REF_DEN_NO <> ''        " & vbNewLine _
            & "AND HIDF.SHIKAKARI_HIN_FLG <> '' " & vbNewLine _
            & "AND (   MR1.PTN_CD IS NOT NULL   " & vbNewLine _
            & "     OR MR3.PTN_ID IS NOT NULL ) " & vbNewLine _
            & ""

    Private Const SQL_SELECT_PRINT_ORDER_BY As String = "" _
            & "ORDER BY            " & vbNewLine _
            & "    HIDF.REF_DEN_NO " & vbNewLine _
            & ""

#End Region ' "帳票パターン取得/印刷データ取得 共通 SQL"

#End Region ' "SQL"

#Region "Field"

    ''' <summary>
    ''' DataTableの行抜き出し
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql As StringBuilder

    ''' <summary>
    ''' SQLパラメータ
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region ' "Field"

#Region "Method"

#Region "帳票パターン取得"

    ''' <summary>
    ''' 帳票パターン取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC543IN")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMC543DAC.SQL_SELECT_MPRT_SELECT)
        Me._StrSql.Append(LMC543DAC.SQL_SELECT_PRINT_FROM)
        Me._StrSql.Append(LMC543DAC.SQL_SELECT_PRINT_WHERE)

        ' SQLのコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            ' パラメータの設定
            Me._SqlPrmList = New ArrayList()
            Call SetSqlParamSelect()

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            ' ログ出力
            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' 取得データの格納先をマッピング
                Dim map As Hashtable = New Hashtable()

                map.Add("NRS_BR_CD", "NRS_BR_CD")
                map.Add("PTN_ID", "PTN_ID")
                map.Add("PTN_CD", "PTN_CD")
                map.Add("RPT_ID", "RPT_ID")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")

            End Using

            ' パラメータの初期化
            cmd.Parameters.Clear()

        End Using

        Return ds

    End Function

#End Region ' "帳票パターン取得"

#Region "印刷データ取得"

    ''' <summary>
    ''' 印刷データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC543IN")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMC543DAC.SQL_SELECT_PRINT_SELECT)
        Me._StrSql.Append(LMC543DAC.SQL_SELECT_PRINT_FROM)
        Me._StrSql.Append(LMC543DAC.SQL_SELECT_PRINT_WHERE)
        Me._StrSql.Append(LMC543DAC.SQL_SELECT_PRINT_ORDER_BY)

        ' SQLのコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            ' パラメータの設定
            Me._SqlPrmList = New ArrayList()
            Call SetSqlParamSelect()

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            ' ログ出力
            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' 取得データ格納先のマッピング
                Dim map As Hashtable = New Hashtable()
                Call SetFieldNamePrintData(map)
                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC543OUT")

            End Using

            ' パラメータの初期化
            cmd.Parameters.Clear()

        End Using

        Return ds

    End Function

#Region "取得データ格納先のマッピング"

    ''' <summary>
    ''' 印刷データ取得
    ''' 取得データ格納先のマッピング
    ''' </summary>
    ''' <param name="map"></param>
    Private Sub SetFieldNamePrintData(ByVal map As Hashtable)

        map.Add("RPT_ID", "RPT_ID")
        map.Add("OFFLINE_NO", "OFFLINE_NO")
        map.Add("OUTKA_DATE", "OUTKA_DATE")
        map.Add("ARR_DATE", "ARR_DATE")
        map.Add("ZIP", "ZIP")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("COMP_NM_1", "COMP_NM")
        map.Add("BUSYO_NM", "BUSYO_NM")
        map.Add("TANTO_NM", "TANTO_NM")
        map.Add("TEL", "TEL")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("INOUTKA_NB", "INOUTKA_NB")
        map.Add("ONDO", "ONDO")
        map.Add("DOKUGEKI", "DOKUGEKI")
        map.Add("REMARK", "REMARK")
        map.Add("HAISO", "HAISO")
        map.Add("REMARK_DTL", "REMARK_DTL")
        map.Add("DOKU_MARK", "DOKU_MARK")
        map.Add("EXP_MARK", "EXP_MARK")

    End Sub

#End Region ' "取得データ格納先のマッピング"

#End Region ' "印刷データ取得"

#Region "帳票パターン取得/印刷データ取得 共通 パラメータ設定"

    ''' <summary>
    ''' 帳票パターン取得/印刷データ取得 共通 パラメータ設定
    ''' </summary>
    Private Sub SetSqlParamSelect()

        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.VARCHAR))
        End With

    End Sub

#End Region ' "帳票パターン取得/印刷データ取得 共通 パラメータ設定"

#Region "共通処理"

    ''' <summary>
    ''' 共通処理：スキーマ名称設定
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>SQL</returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#Region "編集・変換"

#Region "Null変換"

    ''' <summary>
    ''' Null変換（数値）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertZero(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = 0
        End If

        Return value

    End Function

#End Region ' "Null変換"

#End Region ' "編集・変換"

#End Region ' "共通処理"

#End Region ' "Method"

End Class
