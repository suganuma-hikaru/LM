' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : 出荷管理
'  プログラムID     :  LMH588    : テルモ仕切書(千葉)
'  作  成  者       :  篠田
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH588DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH588DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPrt As String = " SELECT DISTINCT                                                       " & vbNewLine _
                                            & "        TRM.NRS_BR_CD                                AS NRS_BR_CD  " & vbNewLine _
                                            & "      , 'BP'                                             AS PTN_ID     " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.PTN_CD               " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.PTN_CD               " & vbNewLine _
                                            & "        ELSE MR3.PTN_CD                                                " & vbNewLine _
                                            & "        END                                              AS PTN_CD     " & vbNewLine _
                                            & "      , CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID               " & vbNewLine _
                                            & "             WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID               " & vbNewLine _
                                            & "        ELSE MR3.RPT_ID                                                " & vbNewLine _
                                            & "        END                                              AS RPT_ID     " & vbNewLine


    ''' <summary>
    ''' 印刷データ抽出用SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                         " & vbNewLine _
                                            & "       CASE WHEN MR2.PTN_CD IS NOT NULL THEN MR2.RPT_ID         " & vbNewLine _
                                            & "            WHEN MR1.PTN_CD IS NOT NULL THEN MR1.RPT_ID         " & vbNewLine _
                                            & "       ELSE MR3.RPT_ID                                          " & vbNewLine _
                                            & "       END                       AS RPT_ID                      " & vbNewLine _
                                            & "     , TRM.CRT_DATE          AS CRT_DATE                    " & vbNewLine _
                                            & "     , TRM.NRS_BR_CD         AS NRS_BR_CD                   " & vbNewLine _
                                            & "     , TRM.EDI_CTL_NO        AS EDI_CTL_NO                  " & vbNewLine _
                                            & "     , TRM.EDI_CTL_NO_CHU    AS EDI_CTL_NO_CHU              " & vbNewLine _
                                            & "     , TRM.PRTFLG            AS PRTFLG                      " & vbNewLine _
                                            & "     , TRM.CANCEL_FLG        AS CANCEL_FLG                  " & vbNewLine _
                                            & "     , TRM.RCV_DATA          AS RCV_DATA                    " & vbNewLine _
                                            & "     , TRM.PRINT_ID          AS PRINT_ID                    " & vbNewLine _
                                            & "     , TRM.SIKIRI_NO         AS SIKIRI_NO                   " & vbNewLine _
                                            & "     , TRM.SOUKO_CD_1        AS SOUKO_CD_1                  " & vbNewLine _
                                            & "     , TRM.CHOUGOUSAKI_CD    AS CHOUGOUSAKI_CD              " & vbNewLine _
                                            & "     , TRM.AKAKURO_ID_1      AS AKAKURO_ID_1                " & vbNewLine _
                                            & "     , TRM.KARIDEN_OUTKA_ID  AS KARIDEN_OUTKA_ID            " & vbNewLine _
                                            & "     , TRM.AKAKURO_ID_2      AS AKAKURO_ID_2                " & vbNewLine _
                                            & "     , TRM.SOUKO_CD_2        AS SOUKO_CD_2                  " & vbNewLine _
                                            & "     , TRM.OUTKA_PLAN_DATE   AS OUTKA_PLAN_DATE                  " & vbNewLine

                                            


    ''' <summary>
    ''' 印刷データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = " --EDI出荷(大)                                                                         " & vbNewLine _
                                     & " FROM $LM_TRN$..H_OUTKAEDI_DTL_TRM TRM             --2013.02.05修正             " & vbNewLine _
                                     & "       --荷主M                                                                         " & vbNewLine _
                                     & "       LEFT JOIN (                                                                     " & vbNewLine _
                                     & "                   SELECT ISNULL(COUNT(*),0)  AS PRT_COUNT                             " & vbNewLine _
                                     & "                        , H_EDI_PRINT.NRS_BR_CD                                        " & vbNewLine _
                                     & "                        , H_EDI_PRINT.EDI_CTL_NO                                       " & vbNewLine _
                                     & "                      --, H_EDI_PRINT.DENPYO_NO            --★2013.0207条件より除外   " & vbNewLine _
                                     & "                     FROM $LM_TRN$..H_EDI_PRINT H_EDI_PRINT                            " & vbNewLine _
                                     & "                    WHERE H_EDI_PRINT.NRS_BR_CD   = @NRS_BR_CD                         " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.CUST_CD_L   = @CUST_CD_L                         " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.CUST_CD_M   = @CUST_CD_M                         " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.PRINT_TP    = '12'                               " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.INOUT_KB    = @INOUT_KB                          " & vbNewLine _
                                     & "                      AND H_EDI_PRINT.SYS_DEL_FLG = '0'                                " & vbNewLine _
                                     & "                    GROUP BY                                                           " & vbNewLine _
                                     & "                          H_EDI_PRINT.NRS_BR_CD                                        " & vbNewLine _
                                     & "                        , H_EDI_PRINT.EDI_CTL_NO                                       " & vbNewLine _
                                     & "                     -- , H_EDI_PRINT.DENPYO_NO             --★2013.0207条件より除外  " & vbNewLine _
                                     & "                 ) HEDIPRINT                                                           " & vbNewLine _
                                     & "              ON HEDIPRINT.NRS_BR_CD  = TRM.NRS_BR_CD                              " & vbNewLine _
                                     & "             AND HEDIPRINT.EDI_CTL_NO = TRM.EDI_CTL_NO                             " & vbNewLine _
                                     & "           --AND HEDIPRINT.DENPYO_NO  = TRM.CUST_ORD_NO --★2013.0207条件より除外  " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_TRN$..H_OUTKAEDI_L EL                                " & vbNewLine _
                                     & "                     ON EL.NRS_BR_CD = TRM.NRS_BR_CD                      " & vbNewLine _
                                     & "                    AND EL.EDI_CTL_NO = TRM.EDI_CTL_NO              " & vbNewLine _
                                     & "        -- 帳票パターンマスタ①(H_INOUTKAEDI_HED_DOWの荷主より取得)                    " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT1                                " & vbNewLine _
                                     & "                     ON M_CUSTRPT1.NRS_BR_CD   = TRM.NRS_BR_CD                     " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.CUST_CD_L   = @CUST_CD_L  --修正             " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.CUST_CD_M   = @CUST_CD_M  --修正             " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.CUST_CD_S   = '00'                                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.PTN_ID      = 'BP'                                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT1.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_RPT  MR1                                           " & vbNewLine _
                                     & "                     ON MR1.NRS_BR_CD          = M_CUSTRPT1.NRS_BR_CD                  " & vbNewLine _
                                     & "                    AND MR1.PTN_ID             = M_CUSTRPT1.PTN_ID                     " & vbNewLine _
                                     & "                    AND MR1.PTN_CD             = M_CUSTRPT1.PTN_CD                     " & vbNewLine _
                                     & "                    AND MR1.SYS_DEL_FLG        = '0'                                   " & vbNewLine _
                                     & "        -- 帳票パターンマスタ②(商品マスタより)                                        " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_CUST_RPT M_CUSTRPT2                                " & vbNewLine _
                                     & "                     ON M_CUSTRPT2.NRS_BR_CD   = TRM.NRS_BR_CD                     " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.CUST_CD_L   = @CUST_CD_L                     " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.CUST_CD_M   = @CUST_CD_M                     " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.CUST_CD_S   = '00'                                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.PTN_ID      = 'BP'                                  " & vbNewLine _
                                     & "                    AND M_CUSTRPT2.SYS_DEL_FLG = '0'                                   " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_RPT  MR2                                           " & vbNewLine _
                                     & "                     ON MR2.NRS_BR_CD          = M_CUSTRPT2.NRS_BR_CD                  " & vbNewLine _
                                     & "                    AND MR2.PTN_ID             = M_CUSTRPT2.PTN_ID                     " & vbNewLine _
                                     & "                    AND MR2.PTN_CD             = M_CUSTRPT2.PTN_CD                     " & vbNewLine _
                                     & "                    AND MR2.SYS_DEL_FLG        = '0'                                   " & vbNewLine _
                                     & "        -- 帳票パターンマスタ③<存在しない場合の帳票パターン取得>                      " & vbNewLine _
                                     & "        LEFT OUTER JOIN $LM_MST$..M_RPT MR3                                            " & vbNewLine _
                                     & "                     ON MR3.NRS_BR_CD          = TRM.NRS_BR_CD                     " & vbNewLine _
                                     & "                    AND MR3.PTN_ID             = 'BP'                                  " & vbNewLine _
                                     & "                    AND MR3.STANDARD_FLAG      = '01'                                  " & vbNewLine _
                                     & "                    AND MR3.SYS_DEL_FLG        = '0'                                   " & vbNewLine



    ''' <summary>
    ''' ORDER BY
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                          " & vbNewLine _
                                         & "      TRM.SIKIRI_NO               " & vbNewLine _
                                         & "    , TRM.EDI_CTL_NO              " & vbNewLine _
                                         & "    , TRM.REC_NO                  " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables("LMH588IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH588DAC.SQL_SELECT_MPrt)      'SQL構築(帳票種別用Select句)

        Select Case Me._Row.Item("PRTFLG").ToString
            Case "0"
                '未出力の場合
                Me._StrSql.Append(LMH588DAC.SQL_FROM)    'SQL構築(データ抽出用From句)
                Call Me.SetConditionMasterSQL()
            Case "1"
                '出力済の場合
                Me._StrSql.Append(LMH588DAC.SQL_FROM)     'SQL構築(データ抽出用From句)
                Call Me.SetConditionMasterSQL()         'EDI管理番号が付与だけどテルモは同じ
        End Select


        Call Me.SetConditionPrintPatternMSQL()          '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH588DAC", "SelectMPrt", cmd)

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
    ''' 仕切書対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>納品送状出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH588IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH588DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)

        Select Case Me._Row.Item("PRTFLG").ToString
            Case "0"
                '未出力の場合
                Me._StrSql.Append(LMH588DAC.SQL_FROM)    'SQL構築(データ抽出用From句)
                Call Me.SetConditionMasterSQL()
            Case "1"
                '出力済の場合
                Me._StrSql.Append(LMH588DAC.SQL_FROM)     'SQL構築(データ抽出用From句)
                Call Me.SetConditionMasterSQL()         'EDI管理番号が付与だけどテルモは同じ
        End Select

        Me._StrSql.Append(LMH588DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)
        Call Me.SetConditionPrintPatternMSQL()            '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH588DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("PRTFLG", "PRTFLG")
        map.Add("CANCEL_FLG", "CANCEL_FLG")
        map.Add("RCV_DATA", "RCV_DATA")
        map.Add("PRINT_ID", "PRINT_ID")
        map.Add("SIKIRI_NO", "SIKIRI_NO")
        map.Add("SOUKO_CD_1", "SOUKO_CD_1")
        map.Add("CHOUGOUSAKI_CD", "CHOUGOUSAKI_CD")
        map.Add("AKAKURO_ID_1", "AKAKURO_ID_1")
        map.Add("KARIDEN_OUTKA_ID", "KARIDEN_OUTKA_ID")
        map.Add("AKAKURO_ID_2", "AKAKURO_ID_2")
        map.Add("SOUKO_CD_2", "SOUKO_CD_2")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH588OUT_TEMP")

        Return ds

    End Function
    ''' <summary>
    ''' 帳票パターンＭ取得 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionPrintPatternMSQL()

        ''SQLパラメータ初期化(WHERE句で実施しているので、ここではコメント)
        'Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        'パラメータ設定
        With Me._Row

            '入出荷区分
            whereStr = .Item("INOUT_KB").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", whereStr, DBDataType.CHAR))

            '営業所
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            '荷主コード(大)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))

            '荷主コード(中)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))

            'EDI管理番号
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me._Row.Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))

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
                Me._StrSql.Append(" TRM.NRS_BR_CD = @NRS_BR_CD")
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EL.CUST_CD_L = @CUST_CD_L")
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND EL.CUST_CD_M = @CUST_CD_M")
            End If

            'EDI管理番号
            whereStr = .Item("EDI_CTL_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND TRM.EDI_CTL_NO = @EDI_CTL_NO")
            End If

            Me._StrSql.Append(" AND TRM.SYS_DEL_FLG = '0'")

            '赤黒区分
            whereStr = .Item("AKAKURO_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
#If False Then  'UPD 2019/02/22 依頼番号 : 004291   【LMS】千葉テルモ_EDIテスト+EDI出荷改修
                Me._StrSql.Append(" AND TRM.AKAKURO_ID_1 = @AKAKURO_ID_1 ")
                Me._StrSql.Append(vbNewLine)
                If whereStr = "02" Then
                    '黒
                    whereStr = "2"
                Else
                    '赤
                    whereStr = "1"
                End If
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AKAKURO_ID_1", whereStr, DBDataType.NVARCHAR))

#Else
                If whereStr = "03" Then
                    Me._StrSql.Append(" AND EL.FREE_C13 = '1400074' ")      '簿外
                    Me._StrSql.Append(vbNewLine)
                Else
                    Me._StrSql.Append(" AND TRM.AKAKURO_ID_1 = @AKAKURO_ID_1 ")
                    Me._StrSql.Append(vbNewLine)

                    If whereStr = "02" Then
                        '黒
                        whereStr = "2"
                    Else
                        '赤
                        whereStr = "1"
                    End If
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AKAKURO_ID_1", whereStr, DBDataType.NVARCHAR))

                    Me._StrSql.Append(" AND EL.FREE_C13 <> '1400074' ")     ''簿外以外
                    Me._StrSql.Append(vbNewLine)

                End If

#End If
            End If

                'EDI出荷予定日(FROM)
                whereStr = .Item("OUTKA_PLAN_DATE_FROM").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append(" AND EL.OUTKA_PLAN_DATE >= @OUTKA_PLAN_DATE_FROM ")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE_FROM", whereStr, DBDataType.CHAR))
                End If

                'EDI出荷予定日(TO)
                whereStr = .Item("OUTKA_PLAN_DATE_TO").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append(" AND EL.OUTKA_PLAN_DATE <= @OUTKA_PLAN_DATE_TO ")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE_TO", whereStr, DBDataType.CHAR))
                End If
                'プリントフラグ (未出力/出力済の判断をHEDIPRINTのレコード有無で行う)
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
