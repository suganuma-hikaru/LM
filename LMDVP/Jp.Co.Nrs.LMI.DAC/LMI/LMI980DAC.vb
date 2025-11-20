' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI980DAC : 運送依頼書(日産物流)
'  作  成  者       :  minagawa
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI980DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI980DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "SQL"

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
        & "   AND MR1.PTN_ID = 'C5'                                " & vbNewLine _
        & "   AND MR1.STANDARD_FLAG = '01'                         " & vbNewLine _
        & "   AND MR1.SYS_DEL_FLG = '0'                            " & vbNewLine

    ''' <summary>
    ''' 印刷データ抽出用 SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = _
          "SELECT                                                                                     " & vbNewLine _
        & "     @RPT_ID AS RPT_ID                                                                     " & vbNewLine _
        & "    ,@PRINT_DATE AS PRINT_DATE                                                             " & vbNewLine _
        & "    ,@PRINT_TIME AS PRINT_TIME                                                             " & vbNewLine _
        & "    ,YUSOIRAIEDI.CUST_NM                                                                   " & vbNewLine _
        & "    ,YUSOIRAIEDI.CUST_SHOZOKU_NM                                                           " & vbNewLine _
        & "    ,YUSOIRAIEDI.KOJO_KANRI_NO                                                             " & vbNewLine _
        & "    ,YUSOIRAIEDI.TSUMIKOMI_DATE                                                            " & vbNewLine _
        & "    ,YUSOIRAIEDI.SHUKKA_DATE                                                               " & vbNewLine _
        & "    ,YUSOIRAIEDI.NONYU_DATE                                                                " & vbNewLine _
        & "    ,ISNULL(Z_N028.KBN_NM2, YUSOIRAIEDI.SHITEI_HICCHAKU_KB) AS SHITEI_HICCHAKU_KB          " & vbNewLine _
        & "    ,YUSOIRAIEDI.NONYU_TIME                                                                " & vbNewLine _
        & "    ,YUSOIRAIEDI.NOUHINSHO_HINMEI                                                          " & vbNewLine _
        & "    ,YUSOIRAIEDI.NISUGATA_NM                                                               " & vbNewLine _
        & "    ,YUSOIRAIEDI.YORYO                                                                     " & vbNewLine _
        & "    ,ISNULL(Z_N029.KBN_NM2, YUSOIRAIEDI.TAN_I) AS TAN_I                                    " & vbNewLine _
        & "    ,YUSOIRAIEDI.KOSU                                                                      " & vbNewLine _
        & "    ,YUSOIRAIEDI.SURYO                                                                     " & vbNewLine _
        & "    ,YUSOIRAIEDI.TORIHIKISAKI_HACCHU_NO                                                    " & vbNewLine _
        & "    ,YUSOIRAIEDI.TORIHIKISAKI_KAISHA_NM                                                    " & vbNewLine _
        & "    ,YUSOIRAIEDI.DEST_KAISHA_NM                                                            " & vbNewLine _
        & "    ,YUSOIRAIEDI.DEST_SHOZOKU_NM                                                           " & vbNewLine _
        & "    ,YUSOIRAIEDI.JUSHO_1                                                                   " & vbNewLine _
        & "    ,YUSOIRAIEDI.JUSHO_2                                                                   " & vbNewLine _
        & "    ,YUSOIRAIEDI.YUBIN_BANGO                                                               " & vbNewLine _
        & "    ,YUSOIRAIEDI.DENWA_BANGO                                                               " & vbNewLine _
        & "    ,YUSOIRAIEDI.SHUKKA_BASHO_NM                                                           " & vbNewLine _
        & "    ,ISNULL(Z_N030.KBN_NM2, YUSOIRAIEDI.UKEWATASHI_JOKEN) AS UKEWATASHI_JOKEN              " & vbNewLine _
        & "    ,ISNULL(Z_N031.KBN_NM2, YUSOIRAIEDI.UNSO_SHUDAN) AS UNSO_SHUDAN                        " & vbNewLine _
        & "    ,ISNULL(Z_N032.KBN_NM2, YUSOIRAIEDI.NOUHINSHO_HITSUYO_KB) AS NOUHINSHO_HITSUYO_KB      " & vbNewLine _
        & "    ,ISNULL(Z_N033.KBN_NM2, YUSOIRAIEDI.SENPO_SURYO_HITSUYO_KB) AS SENPO_SURYO_HITSUYO_KB  " & vbNewLine _
        & "    ,ISNULL(Z_N034.KBN_NM2, YUSOIRAIEDI.SANPURU_KB) AS SANPURU_KB                          " & vbNewLine _
        & "    ,ISNULL(Z_N035.KBN_NM2, YUSOIRAIEDI.BUNSEKIHYO_KB) AS BUNSEKIHYO_KB                    " & vbNewLine _
        & "    ,YUSOIRAIEDI.KOMENTO                                                                   " & vbNewLine _
        & "    ,YUSOIRAIEDI.DENPYO_SHUTURYOKU_KOMENTO                                                 " & vbNewLine _
        & "    ,YUSOIRAIEDI.HAISHA_KOMENTO                                                            " & vbNewLine _
        & "    ,YUSOIRAIEDI.KURUMA_KONTENA_NO                                                         " & vbNewLine _
        & "    ,YUSOIRAIEDI.UKETSUKE_NO                                                               " & vbNewLine _
        & "    ,YUSOIRAIEDI.TUMIAWASE_NO                                                              " & vbNewLine _
        & "    ,YUSOIRAIEDI.YUSO_JOKEN                                                                " & vbNewLine _
        & "  FROM $LM_TRN$..H_YUSOIRAIEDI_NSN  YUSOIRAIEDI                                            " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..Z_KBN  Z_N028                                                        " & vbNewLine _
        & "    ON Z_N028.KBN_GROUP_CD = 'N028'                                                        " & vbNewLine _
        & "   AND Z_N028.KBN_NM1 = YUSOIRAIEDI.SHITEI_HICCHAKU_KB                                     " & vbNewLine _
        & "   AND Z_N028.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..Z_KBN  Z_N029                                                        " & vbNewLine _
        & "    ON Z_N029.KBN_GROUP_CD = 'N029'                                                        " & vbNewLine _
        & "   AND Z_N029.KBN_NM1 = YUSOIRAIEDI.TAN_I                                                  " & vbNewLine _
        & "   AND Z_N029.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..Z_KBN  Z_N030                                                        " & vbNewLine _
        & "    ON Z_N030.KBN_GROUP_CD = 'N030'                                                        " & vbNewLine _
        & "   AND Z_N030.KBN_NM1 = YUSOIRAIEDI.UKEWATASHI_JOKEN                                       " & vbNewLine _
        & "   AND Z_N030.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..Z_KBN  Z_N031                                                        " & vbNewLine _
        & "    ON Z_N031.KBN_GROUP_CD = 'N031'                                                        " & vbNewLine _
        & "   AND Z_N031.KBN_NM1 = YUSOIRAIEDI.UNSO_SHUDAN                                            " & vbNewLine _
        & "   AND Z_N031.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..Z_KBN  Z_N032                                                        " & vbNewLine _
        & "    ON Z_N032.KBN_GROUP_CD = 'N032'                                                        " & vbNewLine _
        & "   AND Z_N032.KBN_NM1 = YUSOIRAIEDI.NOUHINSHO_HITSUYO_KB                                   " & vbNewLine _
        & "   AND Z_N032.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..Z_KBN  Z_N033                                                        " & vbNewLine _
        & "    ON Z_N033.KBN_GROUP_CD = 'N033'                                                        " & vbNewLine _
        & "   AND Z_N033.KBN_NM1 = YUSOIRAIEDI.SENPO_SURYO_HITSUYO_KB                                 " & vbNewLine _
        & "   AND Z_N033.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..Z_KBN  Z_N034                                                        " & vbNewLine _
        & "    ON Z_N034.KBN_GROUP_CD = 'N034'                                                        " & vbNewLine _
        & "   AND Z_N034.KBN_NM1 = YUSOIRAIEDI.SANPURU_KB                                             " & vbNewLine _
        & "   AND Z_N034.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..Z_KBN  Z_N035                                                        " & vbNewLine _
        & "    ON Z_N035.KBN_GROUP_CD = 'N035'                                                        " & vbNewLine _
        & "   AND Z_N035.KBN_NM1 = YUSOIRAIEDI.BUNSEKIHYO_KB                                          " & vbNewLine _
        & "   AND Z_N035.SYS_DEL_FLG = '0'                                                            " & vbNewLine _
        & " WHERE YUSOIRAIEDI.CRT_DATE = @CRT_DATE                                                    " & vbNewLine _
        & "   AND YUSOIRAIEDI.FILE_NAME = @FILE_NAME                                                  " & vbNewLine _
        & "   AND YUSOIRAIEDI.REC_NO = @REC_NO                                                        " & vbNewLine _
        & "   AND YUSOIRAIEDI.SYS_UPD_DATE = @SYS_UPD_DATE                                            " & vbNewLine _
        & "   AND YUSOIRAIEDI.SYS_UPD_TIME = @SYS_UPD_TIME                                            " & vbNewLine _
        & "   AND YUSOIRAIEDI.SYS_DEL_FLG = '0'                                                       " & vbNewLine

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

#Region "印刷処理"

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI980IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI980DAC.SQL_SELECT_MPRT)      'SQL構築(帳票種別用Select句)
        Call Me.SetParamSelectMPrt()                      'パラメータ設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI980DAC", "SelectMPRT", cmd)

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

        '処理件数の設定
        If ds.Tables("M_RPT").Rows.Count < 1 Then
            MyBase.SetMessage("G021")
        End If

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
        Dim inTbl As DataTable = ds.Tables("LMI980IN")
        Dim mrptTbl As DataTable = ds.Tables("M_RPT")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI980DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Call Me.SetParamSelectPrintData(mrptTbl.Rows(0))                 'パラメータ設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI980DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("RPT_ID", "RPT_ID")
        map.Add("PRINT_DATE", "PRINT_DATE")
        map.Add("PRINT_TIME", "PRINT_TIME")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("CUST_SHOZOKU_NM", "CUST_SHOZOKU_NM")
        map.Add("KOJO_KANRI_NO", "KOJO_KANRI_NO")
        map.Add("TSUMIKOMI_DATE", "TSUMIKOMI_DATE")
        map.Add("SHUKKA_DATE", "SHUKKA_DATE")
        map.Add("NONYU_DATE", "NONYU_DATE")
        map.Add("SHITEI_HICCHAKU_KB", "SHITEI_HICCHAKU_KB")
        map.Add("NONYU_TIME", "NONYU_TIME")
        map.Add("NOUHINSHO_HINMEI", "NOUHINSHO_HINMEI")
        map.Add("NISUGATA_NM", "NISUGATA_NM")
        map.Add("YORYO", "YORYO")
        map.Add("TAN_I", "TAN_I")
        map.Add("KOSU", "KOSU")
        map.Add("SURYO", "SURYO")
        map.Add("TORIHIKISAKI_HACCHU_NO", "TORIHIKISAKI_HACCHU_NO")
        map.Add("TORIHIKISAKI_KAISHA_NM", "TORIHIKISAKI_KAISHA_NM")
        map.Add("DEST_KAISHA_NM", "DEST_KAISHA_NM")
        map.Add("DEST_SHOZOKU_NM", "DEST_SHOZOKU_NM")
        map.Add("JUSHO_1", "JUSHO_1")
        map.Add("JUSHO_2", "JUSHO_2")
        map.Add("YUBIN_BANGO", "YUBIN_BANGO")
        map.Add("DENWA_BANGO", "DENWA_BANGO")
        map.Add("SHUKKA_BASHO_NM", "SHUKKA_BASHO_NM")
        map.Add("UKEWATASHI_JOKEN", "UKEWATASHI_JOKEN")
        map.Add("UNSO_SHUDAN", "UNSO_SHUDAN")
        map.Add("NOUHINSHO_HITSUYO_KB", "NOUHINSHO_HITSUYO_KB")
        map.Add("SENPO_SURYO_HITSUYO_KB", "SENPO_SURYO_HITSUYO_KB")
        map.Add("SANPURU_KB", "SANPURU_KB")
        map.Add("BUNSEKIHYO_KB", "BUNSEKIHYO_KB")
        map.Add("KOMENTO", "KOMENTO")
        map.Add("DENPYO_SHUTURYOKU_KOMENTO", "DENPYO_SHUTURYOKU_KOMENTO")
        map.Add("HAISHA_KOMENTO", "HAISHA_KOMENTO")
        map.Add("KURUMA_KONTENA_NO", "KURUMA_KONTENA_NO")
        map.Add("UKETSUKE_NO", "UKETSUKE_NO")
        map.Add("TUMIAWASE_NO", "TUMIAWASE_NO")
        map.Add("YUSO_JOKEN", "YUSO_JOKEN")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI980OUT")

        Return ds

    End Function

#End Region

#Region "設定処理"

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
    Private Sub SetParamSelectPrintData(ByVal mrptRow As DataRow)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RPT_ID", mrptRow.Item("RPT_ID").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_DATE", MyBase.GetSystemDate.Insert(6, "/").Insert(4, "/"), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_TIME", Left(MyBase.GetSystemTime, 4).Insert(2, ":"), DBDataType.VARCHAR))

        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", Me._Row.Item("CRT_DATE").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", Me._Row.Item("FILE_NAME").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", Me._Row.Item("REC_NO").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me._Row.Item("SYS_UPD_DATE").ToString(), DBDataType.VARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me._Row.Item("SYS_UPD_TIME").ToString(), DBDataType.VARCHAR))

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
