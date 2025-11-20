'  ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC901DAC : 毒劇物譲受書(FFEM)
'  作  成  者       :  
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC901DAC
''' </summary>
''' <remarks></remarks>
Public Class LMC901DAC
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
            & "SELECT                                                   " & vbNewLine _
            & "      CASE WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID   " & vbNewLine _
            & "        ELSE MR3.RPT_ID                                  " & vbNewLine _
            & "      END        AS RPT_ID                               " & vbNewLine _
            & "    , MTF.PSTLZ             AS ZIP                       " & vbNewLine _
            & "    , MTF.ZFVYADDR1         AS ADD1                      " & vbNewLine _
            & "    , MTF.ZFVYADDR2         AS ADD2                      " & vbNewLine _
            & "    , MTF.ZFVYADDR3         AS ADD3                      " & vbNewLine _
            & "    , MTF.ZFVYFULLNM        AS SEIQTO_NM                 " & vbNewLine _
            & "    , HODF.SEIQTO_BUSYO_NM  AS BUSHO_NM                  " & vbNewLine _
            & "    , HODF.SEIQTO_TANTO     AS TANTO_NM                  " & vbNewLine _
            & "    , HODF.SEIQTO_TEL       AS TEL                       " & vbNewLine _
            & "    , ''                    AS TEXT_1                    " & vbNewLine _
            & "    , ''                    AS BARCODE_1                 " & vbNewLine _
            & "    , HODF.TARGET_COMPONENT AS MEISHO                    " & vbNewLine _
            & "    , HODF.GOODS_NM                                      " & vbNewLine _
            & "    , COS.OUTKA_TTL_NB      AS SURYO                     " & vbNewLine _
            & "    , COL.ARR_PLAN_DATE                                  " & vbNewLine _
            & "    , '会社員'              AS SHOKUGYO                  " & vbNewLine _
            & "    , ''                    AS TEXT_2                    " & vbNewLine _
            & "    , ''                    AS BARCODE_2                 " & vbNewLine _
            & "    , HODF.JUCHU_DENPYO_NO                               " & vbNewLine _
            & "    , HODF.MEISAI                                        " & vbNewLine _
            & "    , HODF.CUST_GOODS_CD                                 " & vbNewLine _
            & "    , COS.LOT_NO                                         " & vbNewLine _
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
            & "    $LM_TRN$..M_TOKUI_FJF MTF                        " & vbNewLine _
            & "        ON  MTF.NRS_BR_CD = HODF.NRS_BR_CD           " & vbNewLine _
            & "        AND MTF.KUNNR = HODF.SEIQTO_CD               " & vbNewLine _
            & "        AND MTF.SYS_DEL_FLG = '0'                    " & vbNewLine _
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
            & "    $LM_MST$..M_GOODS GOODS                          " & vbNewLine _
            & "        ON  GOODS.NRS_BR_CD = COM.NRS_BR_CD          " & vbNewLine _
            & "        AND GOODS.GOODS_CD_NRS = COM.GOODS_CD_NRS    " & vbNewLine _
            & "        AND GOODS.SYS_DEL_FLG = '0'                  " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_TRN$..M_HINMOKU_FJF HIN_FJF                  " & vbNewLine _
            & "        ON  HIN_FJF.NRS_BR_CD = COM.NRS_BR_CD        " & vbNewLine _
            & "        AND HIN_FJF.HINMOKU_CD = GOODS.GOODS_CD_CUST " & vbNewLine _
            & "-- 荷主帳票パターン                                  " & vbNewLine _
            & "LEFT JOIN                                            " & vbNewLine _
            & "    $LM_MST$..M_CUST_RPT MCR1                        " & vbNewLine _
            & "        ON  MCR1.NRS_BR_CD = HOL.NRS_BR_CD           " & vbNewLine _
            & "        AND MCR1.CUST_CD_L = HOL.CUST_CD_L           " & vbNewLine _
            & "        AND MCR1.CUST_CD_M = HOL.CUST_CD_M           " & vbNewLine _
            & "        AND MCR1.CUST_CD_S = '00'                    " & vbNewLine _
            & "        AND MCR1.PTN_ID = 'DJ'                       " & vbNewLine _
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
            & "        AND MR3.PTN_ID = 'DJ'                        " & vbNewLine _
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
            & "AND HIN_FJF.DOKUGEKI_FLG = 'X'   " & vbNewLine _
            & "AND (   MR1.PTN_CD IS NOT NULL   " & vbNewLine _
            & "     OR MR3.PTN_ID IS NOT NULL ) " & vbNewLine _
            & ""

    Private Const SQL_SELECT_PRINT_ORDER_BY As String = "" _
            & "ORDER BY                     " & vbNewLine _
            & "      HODF.JUCHU_DENPYO_NO   " & vbNewLine _
            & "    , HODF.MEISAI            " & vbNewLine _
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
        Dim inTbl As DataTable = ds.Tables("LMC901IN")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMC901DAC.SQL_SELECT_MPRT_SELECT)
        Me._StrSql.Append(LMC901DAC.SQL_SELECT_PRINT_FROM)
        Me._StrSql.Append(LMC901DAC.SQL_SELECT_PRINT_WHERE)

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
        Dim inTbl As DataTable = ds.Tables("LMC901IN")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMC901DAC.SQL_SELECT_PRINT_SELECT)
        Me._StrSql.Append(LMC901DAC.SQL_SELECT_PRINT_FROM)
        Me._StrSql.Append(LMC901DAC.SQL_SELECT_PRINT_WHERE)
        Me._StrSql.Append(LMC901DAC.SQL_SELECT_PRINT_ORDER_BY)

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
                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC901OUT")

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
        map.Add("ZIP", "ZIP")
        map.Add("ADD1", "ADD1")
        map.Add("ADD2", "ADD2")
        map.Add("ADD3", "ADD3")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("BUSHO_NM", "BUSHO_NM")
        map.Add("TANTO_NM", "TANTO_NM")
        map.Add("TEL", "TEL")
        map.Add("TEXT_1", "TEXT_1")
        map.Add("BARCODE_1", "BARCODE_1")
        map.Add("MEISHO", "MEISHO")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("SURYO", "SURYO")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("SHOKUGYO", "SHOKUGYO")
        map.Add("TEXT_2", "TEXT_2")
        map.Add("BARCODE_2", "BARCODE_2")
        map.Add("JUCHU_DENPYO_NO", "JUCHU_DENPYO_NO")
        map.Add("MEISAI", "MEISAI")
        map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")
        map.Add("LOT_NO", "LOT_NO")

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

#End Region ' "共通処理"

#End Region ' "Method"

End Class
