' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDIサブシステム
'  プログラムID     :  LMH503DAC : 現品票印刷
'  作  成  者       :  
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH503DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH503DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

#Region "印刷ステータス管理"

    ''' <summary>
    ''' 現品票印刷ステータステーブル取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GENPIN_PRT As String = _
          "SELECT EDI_CTL_NO                    " & vbNewLine _
        & "      ,OUTKA_FROM_ORD_NO             " & vbNewLine _
        & "  FROM $LM_TRN$..H_GENPIN_PRT_FJF    " & vbNewLine _
        & " WHERE SYS_DEL_FLG = '0'             " & vbNewLine _
        & "   AND EDI_CTL_NO IN(@EDI_CTL_NO)    " & vbNewLine

    ''' <summary>
    ''' 現品票印刷ステータステーブル登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_GENPIN_PRT As String = _
          "INSERT INTO $LM_TRN$..H_GENPIN_PRT_FJF (    " & vbNewLine _
        & "     EDI_CTL_NO                             " & vbNewLine _
        & "    ,OUTKA_FROM_ORD_NO                      " & vbNewLine _
        & "    ,LAST_PRT_DATE                          " & vbNewLine _
        & "    ,LAST_PRT_TIME                          " & vbNewLine _
        & "    ,SYS_ENT_DATE                           " & vbNewLine _
        & "    ,SYS_ENT_TIME                           " & vbNewLine _
        & "    ,SYS_ENT_PGID                           " & vbNewLine _
        & "    ,SYS_ENT_USER                           " & vbNewLine _
        & "    ,SYS_UPD_DATE                           " & vbNewLine _
        & "    ,SYS_UPD_TIME                           " & vbNewLine _
        & "    ,SYS_UPD_PGID                           " & vbNewLine _
        & "    ,SYS_UPD_USER                           " & vbNewLine _
        & "    ,SYS_DEL_FLG                            " & vbNewLine _
        & ") VALUES (                                  " & vbNewLine _
        & "     @EDI_CTL_NO                            " & vbNewLine _
        & "    ,@OUTKA_FROM_ORD_NO                     " & vbNewLine _
        & "    ,@LAST_PRT_DATE                         " & vbNewLine _
        & "    ,@LAST_PRT_TIME                         " & vbNewLine _
        & "    ,@SYS_ENT_DATE                          " & vbNewLine _
        & "    ,@SYS_ENT_TIME                          " & vbNewLine _
        & "    ,@SYS_ENT_PGID                          " & vbNewLine _
        & "    ,@SYS_ENT_USER                          " & vbNewLine _
        & "    ,@SYS_UPD_DATE                          " & vbNewLine _
        & "    ,@SYS_UPD_TIME                          " & vbNewLine _
        & "    ,@SYS_UPD_PGID                          " & vbNewLine _
        & "    ,@SYS_UPD_USER                          " & vbNewLine _
        & "    ,'0'                                    " & vbNewLine _
        & ")                                           " & vbNewLine

    ''' <summary>
    ''' 現品票印刷ステータステーブル更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_GENPIN_PRT As String = _
          "UPDATE $LM_TRN$..H_GENPIN_PRT_FJF                " & vbNewLine _
        & "   SET OUTKA_FROM_ORD_NO = @OUTKA_FROM_ORD_NO    " & vbNewLine _
        & "      ,LAST_PRT_DATE = @LAST_PRT_DATE            " & vbNewLine _
        & "      ,LAST_PRT_TIME = @LAST_PRT_TIME            " & vbNewLine _
        & "      ,SYS_UPD_DATE=@SYS_UPD_DATE                " & vbNewLine _
        & "      ,SYS_UPD_TIME=@SYS_UPD_TIME                " & vbNewLine _
        & "      ,SYS_UPD_PGID=@SYS_UPD_PGID                " & vbNewLine _
        & "      ,SYS_UPD_USER=@SYS_UPD_USER                " & vbNewLine _
        & "      ,SYS_DEL_FLG = '0'                         " & vbNewLine _
        & " WHERE EDI_CTL_NO = @EDI_CTL_NO                  " & vbNewLine

#End Region '現品票印刷ステータステーブル

#Region "印刷処理"

    ''' <summary>
    ''' 帳票種別取得用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_MPRT As String = _
          "SELECT                                                  " & vbNewLine _
        & "     ISNULL(MR2.NRS_BR_CD, MR1.NRS_BR_CD)  AS NRS_BR_CD " & vbNewLine _
        & "    ,ISNULL(MR2.PTN_ID, MR1.PTN_ID)  AS PTN_ID          " & vbNewLine _
        & "    ,ISNULL(MR2.PTN_CD, MR1.PTN_CD)  AS PTN_CD          " & vbNewLine _
        & "    ,ISNULL(MR2.RPT_ID, MR1.RPT_ID)  AS RPT_ID          " & vbNewLine _
        & "  FROM $LM_MST$..M_RPT  MR1                             " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..M_CUST_RPT  MCR1                  " & vbNewLine _
        & "    ON MCR1.NRS_BR_CD = MR1.NRS_BR_CD                   " & vbNewLine _
        & "   AND MCR1.CUST_CD_L = @CUST_CD_L                      " & vbNewLine _
        & "   AND MCR1.CUST_CD_M = '00'                            " & vbNewLine _
        & "   AND MCR1.CUST_CD_S = '00'                            " & vbNewLine _
        & "   AND MCR1.PTN_ID = MR1.PTN_ID                         " & vbNewLine _
        & "   AND MCR1.SYS_DEL_FLG = '0'                           " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..M_RPT  MR2                        " & vbNewLine _
        & "    ON MR2.NRS_BR_CD = MCR1.NRS_BR_CD                   " & vbNewLine _
        & "   AND MR2.PTN_ID = MCR1.PTN_ID                         " & vbNewLine _
        & "   AND MR2.PTN_CD = MCR1.PTN_CD                         " & vbNewLine _
        & "   AND MR2.SYS_DEL_FLG = '0'                            " & vbNewLine _
        & " WHERE MR1.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
        & "   AND MR1.PTN_ID = 'D5'                                " & vbNewLine _
        & "   AND MR1.STANDARD_FLAG = '01'                         " & vbNewLine _
        & "   AND MR1.SYS_DEL_FLG = '0'                            " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = _
          "SELECT                                                          " & vbNewLine _
        & "     HED.NRS_BR_CD                                              " & vbNewLine _
        & "    ,HED.ZFVYDENYMD  AS NONYU_DATE                              " & vbNewLine _
        & "    ,DTL.LGORT       AS NONYU_BASHO1                            " & vbNewLine _
        & "    ,DTL.ZFVYMTEKIYO AS NONYU_BASHO2                            " & vbNewLine _
        & "    ,DTL.MATNR       AS CUST_GOODS_CD                           " & vbNewLine _
        & "    ,DTL.ZFVYMAKTX3  AS GOODS_NM                                " & vbNewLine _
        & "    ,DTL.KDMAT       AS MLOT                                    " & vbNewLine _
        & "    ,DTL.CHARG       AS LOT_NO                                  " & vbNewLine _
        & "    ,DTL.ZFVYSURYO                                              " & vbNewLine _
        & "    ,HED.ZFVYDENNO   AS OUTKA_FROM_ORD_NO                       " & vbNewLine _
        & "    ,HED.EDI_CTL_NO                                             " & vbNewLine _
        & "    ,DTL.ZFVYBRGEW                                              " & vbNewLine _
        & "    ,MG.STD_IRIME_NB AS STD_IRIME                               " & vbNewLine _
        & "  FROM $LM_TRN$..H_INOUTKAEDI_HED_FJF  HED                      " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_INOUTKAEDI_DTL_FJF  DTL                 " & vbNewLine _
        & "    ON DTL.CRT_DATE = HED.CRT_DATE                              " & vbNewLine _
        & "   AND DTL.FILE_NAME = HED.FILE_NAME                            " & vbNewLine _
        & "   AND DTL.REC_NO = HED.REC_NO                                  " & vbNewLine _
        & "  -- 商品マスタ                                                 " & vbNewLine _
        & "  LEFT JOIN                                                     " & vbNewLine _
        & "       (SELECT *                                                " & vbNewLine _
        & "          FROM $LM_MST$..M_GOODS  MG1                           " & vbNewLine _
        & "         WHERE MG1.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
        & "           AND MG1.CUST_CD_L = @CUST_CD_L                       " & vbNewLine _
        & "           AND MG1.CUST_CD_M = @CUST_CD_M                       " & vbNewLine _
        & "           AND MG1.SYS_DEL_FLG = '0'                            " & vbNewLine _
        & "           AND NOT EXISTS                                       " & vbNewLine _
        & "               (SELECT COUNT(*)                                 " & vbNewLine _
        & "                  FROM $LM_MST$..M_GOODS  MG2                   " & vbNewLine _
        & "                 WHERE MG2.NRS_BR_CD = @NRS_BR_CD               " & vbNewLine _
        & "                   AND MG2.CUST_CD_L = @CUST_CD_L               " & vbNewLine _
        & "                   AND MG2.CUST_CD_M = @CUST_CD_M               " & vbNewLine _
        & "                   AND MG2.GOODS_CD_CUST = MG1.GOODS_CD_CUST    " & vbNewLine _
        & "                   AND MG2.SYS_DEL_FLG = '0'                    " & vbNewLine _
        & "                HAVING COUNT(*) > 1                             " & vbNewLine _
        & "               )                                                " & vbNewLine _
        & "       )  MG                                                    " & vbNewLine _
        & "    ON MG.GOODS_CD_CUST = DTL.MATNR                             " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用 WHERE句(未発行分)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_WHERE_MIHAKKOU As String = _
          " WHERE HED.SYS_DEL_FLG = '0'                     " & vbNewLine _
        & "   AND HED.DEL_KB = '0'                          " & vbNewLine _
        & "   AND HED.NRS_BR_CD = @NRS_BR_CD                " & vbNewLine _
        & "   AND HED.INOUT_KB = '1'                        " & vbNewLine _
        & "   AND HED.EDI_CTL_NO = @EDI_CTL_NO              " & vbNewLine _
        & "   AND DTL.SYS_DEL_FLG = '0'                     " & vbNewLine _
        & "   AND DTL.DEL_KB = '0'                          " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用 WHERE句(再印刷分)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_WHERE_SAIHAKKOU As String = _
          " WHERE HED.SYS_DEL_FLG = '0'                     " & vbNewLine _
        & "   AND HED.DEL_KB = '0'                          " & vbNewLine _
        & "   AND HED.NRS_BR_CD = @NRS_BR_CD                " & vbNewLine _
        & "   AND HED.CRT_DATE = @CRT_DATE                  " & vbNewLine _
        & "   AND HED.FILE_NAME = @FILE_NAME                " & vbNewLine _
        & "   AND HED.REC_NO = @REC_NO                      " & vbNewLine _
        & "   AND HED.INOUT_KB = '1'                        " & vbNewLine _
        & "   AND HED.SYS_UPD_DATE = @HED_UPD_DATE          " & vbNewLine _
        & "   AND HED.SYS_UPD_TIME = @HED_UPD_TIME          " & vbNewLine _
        & "   AND DTL.SYS_DEL_FLG = '0'                     " & vbNewLine _
        & "   AND DTL.DEL_KB = '0'                          " & vbNewLine _
        & "   AND DTL.GYO = @GYO                            " & vbNewLine _
        & "   AND DTL.SYS_UPD_DATE = @DTL_UPD_DATE          " & vbNewLine _
        & "   AND DTL.SYS_UPD_TIME = @DTL_UPD_TIME          " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用 ORDER句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_ORDER As String = _
          " ORDER BY DTL.CRT_DATE                           " & vbNewLine _
        & "         ,DTL.FILE_NAME                          " & vbNewLine _
        & "         ,DTL.REC_NO                             " & vbNewLine _
        & "         ,DTL.GYO                                " & vbNewLine

#End Region '印刷処理

#Region "シーケンス処理"

    ''' <summary>
    ''' シーケンス番号取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SEQ_NO As String = _
          "SELECT                                 " & vbNewLine _
        & "    NEXT_SEQ_NO                        " & vbNewLine _
        & "FROM                                   " & vbNewLine _
        & "    $LM_TRN$..M_GENSEQ_FJF             " & vbNewLine _
        & "WITH(TABLOCKX)                         " & vbNewLine _
        & "WHERE                                  " & vbNewLine _
        & "        NRS_BR_CD     = @NRS_BR_CD     " & vbNewLine _
        & "    AND CUST_CD_L     = @CUST_CD_L     " & vbNewLine _
        & "    AND GOODS_CD_CUST = @GOODS_CD_CUST " & vbNewLine _
        & "    AND LOT_NO        = @LOT_NO        " & vbNewLine

    ''' <summary>
    ''' シーケンス番号更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_SEQ_NO As String = _
          "UPDATE                                 " & vbNewLine _
        & "    $LM_TRN$..M_GENSEQ_FJF             " & vbNewLine _
        & "SET                                    " & vbNewLine _
        & "    NEXT_SEQ_NO  = NEXT_SEQ_NO + 1,    " & vbNewLine _
        & "    SYS_UPD_DATE = @SYS_UPD_DATE,      " & vbNewLine _
        & "    SYS_UPD_TIME = @SYS_UPD_TIME,      " & vbNewLine _
        & "    SYS_UPD_PGID = @SYS_UPD_PGID,      " & vbNewLine _
        & "    SYS_UPD_USER = @SYS_UPD_USER       " & vbNewLine _
        & "WHERE                                  " & vbNewLine _
        & "        NRS_BR_CD     = @NRS_BR_CD     " & vbNewLine _
        & "    AND CUST_CD_L     = @CUST_CD_L     " & vbNewLine _
        & "    AND GOODS_CD_CUST = @GOODS_CD_CUST " & vbNewLine _
        & "    AND LOT_NO        = @LOT_NO        " & vbNewLine

    ''' <summary>
    ''' シーケンス番号登録
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SEQ_NO As String = _
          "INSERT INTO $LM_TRN$..M_GENSEQ_FJF( " & vbNewLine _
        & "    NRS_BR_CD,                      " & vbNewLine _
        & "    CUST_CD_L,                      " & vbNewLine _
        & "    GOODS_CD_CUST,                  " & vbNewLine _
        & "    LOT_NO,                         " & vbNewLine _
        & "    NEXT_SEQ_NO,                    " & vbNewLine _
        & "    SYS_ENT_DATE,                   " & vbNewLine _
        & "    SYS_ENT_TIME,                   " & vbNewLine _
        & "    SYS_ENT_PGID,                   " & vbNewLine _
        & "    SYS_ENT_USER,                   " & vbNewLine _
        & "    SYS_UPD_DATE,                   " & vbNewLine _
        & "    SYS_UPD_TIME,                   " & vbNewLine _
        & "    SYS_UPD_PGID,                   " & vbNewLine _
        & "    SYS_UPD_USER,                   " & vbNewLine _
        & "    SYS_DEL_FLG                     " & vbNewLine _
        & ")VALUES(                            " & vbNewLine _
        & "    @NRS_BR_CD,                     " & vbNewLine _
        & "    @CUST_CD_L,                     " & vbNewLine _
        & "    @GOODS_CD_CUST,                 " & vbNewLine _
        & "    @LOT_NO,                        " & vbNewLine _
        & "    @NEXT_SEQ_NO,                   " & vbNewLine _
        & "    @SYS_ENT_DATE,                  " & vbNewLine _
        & "    @SYS_ENT_TIME,                  " & vbNewLine _
        & "    @SYS_ENT_PGID,                  " & vbNewLine _
        & "    @SYS_ENT_USER,                  " & vbNewLine _
        & "    @SYS_UPD_DATE,                  " & vbNewLine _
        & "    @SYS_UPD_TIME,                  " & vbNewLine _
        & "    @SYS_UPD_PGID,                  " & vbNewLine _
        & "    @SYS_UPD_USER,                  " & vbNewLine _
        & "    @SYS_DEL_FLG                    " & vbNewLine _
        & ")                                   " & vbNewLine

#End Region 'シーケンス処理

#End Region 'SQL

#End Region 'Const

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

#Region "印刷ステータス管理"

    ''' <summary>
    '''現品票印刷ステータス取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>現品票印刷ステータス取得SQLの構築・発行</remarks>
    Private Function SelectGenpinPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("PRINT_DATA_IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMH503DAC.SQL_SELECT_GENPIN_PRT, Me._Row.Item("NRS_BR_CD").ToString)

        'SelectCommand作成
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sql)

        'SQLパラメータ設定
        cmd.Parameters.Add(GetSqlParameter("@EDI_CTL_NO", Me._Row.Item("EDI_CTL_NO").ToString(), DBDataType.VARCHAR))

        MyBase.Logger.WriteSQLLog("LMH503DAC", "SelectGenpinPrt", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("OUTKA_FROM_ORD_NO", "OUTKA_FROM_ORD_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "H_GENPIN_PRT_FJF")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 現品票印刷ステータス更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateGenpinPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("PRINT_DATA_IN")
        Dim prnTbl As DataTable = ds.Tables("H_GENPIN_PRT_FJF")

        '条件rowの格納
        Me._Row = prnTbl.Rows(0)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(SQL_UPDATE_GENPIN_PRT, inTbl.Rows(0).Item("NRS_BR_CD").ToString)

        'SelectCommand作成
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sql)

        'パラメータ設定
        Call Me.SetParamUpdateGenpinPrt()

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog("LMH503DAC", "UpdateGenpinPrt", cmd)

        'SQLの発行
        Dim resultCount As Integer = MyBase.GetUpdateResult(cmd)

        '処理件数の設定
        MyBase.SetResultCount(resultCount)

        Return ds

    End Function

    ''' <summary>
    '''現品票印刷ステータス登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertGenpinPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("PRINT_DATA_IN")
        Dim prnTbl As DataTable = ds.Tables("H_GENPIN_PRT_FJF")

        '条件rowの格納
        Me._Row = prnTbl.Rows(0)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(SQL_INSERT_GENPIN_PRT, inTbl.Rows(0).Item("NRS_BR_CD").ToString)

        'SelectCommand作成
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sql)

        'パラメータ設定
        Call Me.SetParamInsertGenpinPrt()

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog("LMH503DAC", "InsertGenpinPrt", cmd)

        'SQLの発行
        Dim resultCount As Integer = MyBase.GetInsertResult(cmd)

        '処理件数の設定
        MyBase.SetResultCount(resultCount)

        Return ds

    End Function

#End Region

#Region "印刷処理"

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("PRINT_DATA_IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH503DAC.SQL_SELECT_MPRT)      'SQL構築(帳票種別用Select句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Call Me.SetParamSelectMPrt()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH503DAC", "SelectMPRT", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 印刷対象データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("PRINT_DATA_IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        Dim shoriKb As String = Me._Row.Item("SHORI_KB").ToString()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH503DAC.SQL_SELECT_DATA)

        Select Case shoriKb
            Case "未発行"
                Me._StrSql.Append(LMH503DAC.SQL_SELECT_DATA_WHERE_MIHAKKOU)

            Case "再発行"
                Me._StrSql.Append(LMH503DAC.SQL_SELECT_DATA_WHERE_SAIHAKKOU)
        End Select

        Me._StrSql.Append(LMH503DAC.SQL_SELECT_DATA_ORDER)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Call Me.SetParamSelectPrintData(shoriKb)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH503DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NONYU_DATE", "NONYU_DATE")
        map.Add("NONYU_BASHO1", "NONYU_BASHO1")
        map.Add("NONYU_BASHO2", "NONYU_BASHO2")
        map.Add("CUST_GOODS_CD", "CUST_GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("MLOT", "MLOT")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("ZFVYSURYO", "ZFVYSURYO")
        map.Add("OUTKA_FROM_ORD_NO", "OUTKA_FROM_ORD_NO")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("ZFVYBRGEW", "ZFVYBRGEW")
        map.Add("STD_IRIME", "STD_IRIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "SELECT_PRINT_DATA_OUT")

        Return ds

    End Function

#End Region

#Region "シーケンス処理"

    ''' <summary>
    ''' シーケンス番号抽出メソッド
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SelectSeqNo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("GET_SEQ_NO_IN")
        Dim inRow As DataRow = inTbl.Rows(0)

        'SelectCommand作成
        Dim cmd As SqlCommand = Me.CreateSqlCommand(Me.SetSchemaNm(SQL_SELECT_SEQ_NO, inRow("NRS_BR_CD").ToString))

        'SQLパラメータ設定
        cmd.Parameters.Add(GetSqlParameter("@NRS_BR_CD", inRow.Item("NRS_BR_CD"), DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@CUST_CD_L", inRow.Item("CUST_CD_L"), DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@GOODS_CD_CUST", inRow.Item("GOODS_CD_CUST"), DBDataType.NVARCHAR))
        cmd.Parameters.Add(GetSqlParameter("@LOT_NO", inRow.Item("LOT_NO"), DBDataType.NVARCHAR))

        MyBase.Logger.WriteSQLLog("LMH503DAC", "SelectSeqNo", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NEXT_SEQ_NO", "NEXT_SEQ_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "GET_SEQ_NO_OUT")

        Return ds

    End Function

    ''' <summary>
    ''' シーケンス番号更新メソッド
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UpdateSeqNo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("GET_SEQ_NO_IN")
        Dim inRow As DataRow = inTbl.Rows(0)

        'SelectCommand作成
        Dim cmd As SqlCommand = Me.CreateSqlCommand(Me.SetSchemaNm(SQL_UPDATE_SEQ_NO, inRow("NRS_BR_CD").ToString))

        'SQLパラメータ設定
        cmd.Parameters.Add(GetSqlParameter("@NRS_BR_CD", inRow.Item("NRS_BR_CD"), DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@CUST_CD_L", inRow.Item("CUST_CD_L"), DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@GOODS_CD_CUST", inRow.Item("GOODS_CD_CUST"), DBDataType.NVARCHAR))
        cmd.Parameters.Add(GetSqlParameter("@LOT_NO", inRow.Item("LOT_NO"), DBDataType.NVARCHAR))
        cmd.Parameters.Add(GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.VARCHAR))

        MyBase.Logger.WriteSQLLog("LMH503DAC", "UpdateSeqNo", cmd)

        'SQLの発行
        Dim resultCount As Integer = MyBase.GetUpdateResult(cmd)

        '処理件数の設定
        MyBase.SetResultCount(resultCount)

        Return ds

    End Function

    ''' <summary>
    ''' シーケンス番号登録メソッド
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function InsertSeqNo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("GET_SEQ_NO_IN")
        Dim inRow As DataRow = inTbl.Rows(0)

        'SelectCommand作成
        Dim cmd As SqlCommand = Me.CreateSqlCommand(Me.SetSchemaNm(SQL_INSERT_SEQ_NO, inRow("NRS_BR_CD").ToString))

        'SQLパラメータ設定
        cmd.Parameters.Add(GetSqlParameter("@NRS_BR_CD", inRow.Item("NRS_BR_CD"), DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@CUST_CD_L", inRow.Item("CUST_CD_L"), DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@GOODS_CD_CUST", inRow.Item("GOODS_CD_CUST"), DBDataType.NVARCHAR))
        cmd.Parameters.Add(GetSqlParameter("@LOT_NO", inRow.Item("LOT_NO"), DBDataType.NVARCHAR))
        cmd.Parameters.Add(GetSqlParameter("@NEXT_SEQ_NO", 2, DBDataType.NUMERIC))
        cmd.Parameters.Add(GetSqlParameter("@SYS_ENT_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@SYS_ENT_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@SYS_ENT_PGID", Me.GetPGID(), DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@SYS_ENT_USER", Me.GetUserID(), DBDataType.VARCHAR))
        cmd.Parameters.Add(GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
        cmd.Parameters.Add(GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.VARCHAR))
        cmd.Parameters.Add(GetSqlParameter("@SYS_DEL_FLG", "0", DBDataType.VARCHAR))

        MyBase.Logger.WriteSQLLog("LMH503DAC", "InsertSeqNo", cmd)

        'SQLの発行
        Dim resultCount As Integer = MyBase.GetInsertResult(cmd)

        '処理件数の設定
        MyBase.SetResultCount(resultCount)

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 条件文設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdateGenpinPrt()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me._Row.Item("EDI_CTL_NO").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO", Me._Row.Item("OUTKA_FROM_ORD_NO").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_PRT_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_PRT_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.VARCHAR))

    End Sub

    ''' <summary>
    ''' 条件文設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertGenpinPrt()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me._Row.Item("EDI_CTL_NO").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO", Me._Row.Item("OUTKA_FROM_ORD_NO").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_PRT_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LAST_PRT_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", Me.GetUserID(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.VARCHAR))

    End Sub

    ''' <summary>
    ''' 条件文設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSelectMPrt()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.VARCHAR))

    End Sub

    ''' <summary>
    ''' 条件文設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamSelectPrintData(ByVal shoriKb As String)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.VARCHAR))

        Select Case shoriKb
            Case "未発行"
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", Me._Row.Item("EDI_CTL_NO").ToString(), DBDataType.VARCHAR))

            Case "再発行"
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", Me._Row.Item("CRT_DATE").ToString(), DBDataType.VARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", Me._Row.Item("FILE_NAME").ToString(), DBDataType.VARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", Me._Row.Item("REC_NO").ToString(), DBDataType.VARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HED_UPD_DATE", Me._Row.Item("HED_UPD_DATE").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HED_UPD_TIME", Me._Row.Item("HED_UPD_TIME").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GYO", Me._Row.Item("GYO").ToString(), DBDataType.VARCHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DTL_UPD_DATE", Me._Row.Item("DTL_UPD_DATE").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DTL_UPD_TIME", Me._Row.Item("DTL_UPD_TIME").ToString(), DBDataType.CHAR))
        End Select

    End Sub

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

End Class
