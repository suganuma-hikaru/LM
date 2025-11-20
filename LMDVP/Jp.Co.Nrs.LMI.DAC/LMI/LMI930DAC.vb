' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI930  : 住化カラー実績報告データ作成
'  作  成  者       :  [umano]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI930DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI930DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "TXT出力データの検索"

#Region "TXT出力データの検索 SQL SELECT句"

    ''' <summary>
    ''' 住化カラー受信(HED/DTL)検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT As String = "SELECT                                              " & vbNewLine _
                    & "      -- 住化カラー実績報告データ                                  " & vbNewLine _
                    & "       SMK_HED.COMPANY_CD                 AS COMPANY_CD            " & vbNewLine _
                    & "      ,SMK_HED.RB_KB                      AS RB_KB                 " & vbNewLine _
                    & "      ,SMK_HED.DENP_NO                    AS DENP_NO               " & vbNewLine _
                    & "      ,SMK_HED.DENP_NO_EDA                AS DENP_NO_EDA           " & vbNewLine _
                    & "      ,SMK_DTL.DENP_NO_REN                AS DENP_NO_REN           " & vbNewLine _
                    & "      ,SMK_DTL.DENP_NO_GYO                AS DENP_NO_GYO           " & vbNewLine _
                    & "      ,SMK_HED.SHORI_DATE                 AS SHORI_DATE            " & vbNewLine _
                    & "      ,SMK_HED.OUTKA_PLAN_DATE            AS OUTKA_PLAN_DATE       " & vbNewLine _
                    & "      ,SMK_HED.UKE_CHUKEI                 AS UKE_CHUKEI            " & vbNewLine _
                    & "      ,SMK_HED.UKE_AITE                   AS UKE_AITE              " & vbNewLine _
                    & "      ,SMK_HED.UKE_SHUBETSU               AS UKE_SHUBETSU          " & vbNewLine _
                    & "      ,SMK_DTL.GOODS_CLASS                AS GOODS_CLASS           " & vbNewLine _
                    & "      ,SMK_DTL.GOODS_CD                   AS GOODS_CD              " & vbNewLine _
                    & "      ,SMK_DTL.GOODS_NM                   AS GOODS_NM              " & vbNewLine _
                    & "      ,SMK_DTL.NISUGATA                   AS NISUGATA              " & vbNewLine _
                    & "--      ,SMK_DTL.IRIME * 1000               AS IRIME                 " & vbNewLine _
                    & "      ,SMK_DTL.IRIME                      AS IRIME                 " & vbNewLine _
                    & "      ,SMK_DTL.TANA_KB                    AS TANA_KB               " & vbNewLine _
                    & "      ,SMK_DTL.ZAIKO_KB                   AS ZAIKO_KB              " & vbNewLine _
                    & "      ,SMK_DTL.LOT_NO                     AS LOT_NO                " & vbNewLine _
                    & "      ,SMK_DTL.SEISAN_KOJO                AS SEISAN_KOJO           " & vbNewLine _
                    & "      ,SMK_DTL.KOSU                       AS KOSU                  " & vbNewLine _
                    & "--      ,SMK_DTL.SURYO * 1000               AS SURYO                 " & vbNewLine _
                    & "      ,SMK_DTL.SURYO                      AS SURYO                 " & vbNewLine _
                    & "      ,SMK_HED.ARR_PLAN_DATE              AS ARR_PLAN_DATE         " & vbNewLine _
                    & "      ,SMK_DTL.YOUKI_JOKEN                AS YOUKI_JOKEN           " & vbNewLine _
                    & "      ,SMK_DTL.NIWATASHI_JOKEN            AS NIWATASHI_JOKEN       " & vbNewLine _
                    & "      ,SMK_DTL.SHIKEN_HYO                 AS SHIKEN_HYO            " & vbNewLine _
                    & "      ,SMK_DTL.SHITEI_DENPYO              AS SHITEI_DENPYO         " & vbNewLine _
                    & "      ,SMK_HED.DEST_CD                    AS DEST_CD               " & vbNewLine _
                    & "      ,SMK_HED.DEST_ZIP                   AS DEST_ZIP              " & vbNewLine _
                    & "      ,SMK_HED.DEST_TEL                   AS DEST_TEL              " & vbNewLine _
                    & "      ,SMK_HED.DEST_MSG                   AS DEST_MSG              " & vbNewLine _
                    & "      ,SMK_DTL.BUYER_ORD_NO_DTL           AS BUYER_ORD_NO_DTL      " & vbNewLine _
                    & "      ,SMK_DTL.BUYER_CD                   AS BUYER_CD              " & vbNewLine _
                    & "      ,SMK_DTL.BUYER_TEL                  AS BUYER_TEL             " & vbNewLine _
                    & "      ,SMK_DTL.KOJO_MSG                   AS KOJO_MSG              " & vbNewLine _
                    & "      ,SMK_DTL.HANBAI_KA                  AS HANBAI_KA             " & vbNewLine _
                    & "      ,SMK_DTL.HANBAI_TANTO               AS HANBAI_TANTO          " & vbNewLine _
                    & "      ,SMK_DTL.ORDER_KB                   AS ORDER_KB              " & vbNewLine _
                    & "      ,SMK_HED.DEST_NM                    AS DEST_NM               " & vbNewLine _
                    & "      ,SMK_HED.DEST_AD_1                  AS DEST_AD_1             " & vbNewLine _
                    & "      ,SMK_HED.DEST_AD_2                  AS DEST_AD_2             " & vbNewLine _
                    & "      ,SMK_DTL.BUYER_NM                   AS BUYER_NM              " & vbNewLine _
                    & "      ,SMK_HED.DEST_NM_KANA               AS DEST_NM_KANA          " & vbNewLine _
                    & "      ,SMK_HED.DEST_AD_KANA1              AS DEST_AD_KANA1         " & vbNewLine _
                    & "      ,SMK_HED.DEST_AD_KANA2              AS DEST_AD_KANA2         " & vbNewLine _
                    & "      ,SMK_DTL.SAKUSEI_DATE               AS SAKUSEI_DATE          " & vbNewLine _
                    & "      ,SMK_DTL.SAKUSEI_TIME               AS SAKUSEI_TIME          " & vbNewLine _
                    & "      ,SMK_DTL.RACK_NO                    AS RACK_NO               " & vbNewLine _
                    & "      ,SMK_DTL.CUST_ORD_NO_DTL            AS CUST_ORD_NO_DTL       " & vbNewLine _
                    & "      ,SMK_DTL.RIYUU                      AS RIYUU                 " & vbNewLine _
                    & "      ,SMK_DTL.DATA_KB                    AS DATA_KB               " & vbNewLine _
                    & "      ,SMK_DTL.YUSO_SHUDAN                AS YUSO_SHUDAN           " & vbNewLine _
                    & "      ,SMK_DTL.YUSO_GYOSHA                AS YUSO_GYOSHA           " & vbNewLine _
                    & "      ,SMK_DTL.YUSO_JIS_CD                AS YUSO_JIS_CD           " & vbNewLine _
                    & "      ,SMK_DTL.JISSEKI_KBN                AS JISSEKI_KBN           " & vbNewLine _
                    & "      ,SMK_DTL.CRT_DATE                   AS CRT_DATE              " & vbNewLine _
                    & "      ,SMK_DTL.FILE_NAME                  AS FILE_NAME             " & vbNewLine _
                    & "      ,SMK_DTL.REC_NO                     AS REC_NO                " & vbNewLine _
                    & "      ,SMK_DTL.GYO                        AS GYO                   " & vbNewLine _
                    & "      ,SMK_DTL.EDI_CTL_NO                 AS EDI_CTL_NO            " & vbNewLine _
                    & "      ,SMK_DTL.EDI_CTL_NO_CHU             AS EDI_CTL_NO_CHU        " & vbNewLine _
                    & "      ,SMK_DTL.NRS_BR_CD                  AS NRS_BR_CD             " & vbNewLine _
                    & "      ,SMK_DTL.INOUT_KB                   AS INOUT_KB              " & vbNewLine

#End Region

#Region "TXT出力データの検索 SQL FROM句"

    ''' <summary>
    ''' TXT出力データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM As String = "  FROM $LM_TRN$..H_INOUTKAEDI_DTL_SMK SMK_DTL                                                        " & vbNewLine _
                    & "     LEFT JOIN $LM_TRN$..H_INOUTKAEDI_HED_SMK SMK_HED                                                                    " & vbNewLine _
                    & "             ON SMK_DTL.CRT_DATE      = SMK_HED.CRT_DATE                                                           " & vbNewLine _
                    & "            AND SMK_DTL.FILE_NAME     = SMK_HED.FILE_NAME                                                          " & vbNewLine _
                    & "            AND SMK_DTL.REC_NO        = SMK_HED.REC_NO                                                          " & vbNewLine

#End Region

#Region "TXT出力データの検索 SQL ORDER句"

    ''' <summary>
    ''' TXT出力データの検索 SQL ORDER句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER As String = "ORDER BY                                                                                    " & vbNewLine _
                    & "     SMK_DTL.CRT_DATE                                                                                                " & vbNewLine _
                    & "    ,SMK_DTL.FILE_NAME                                                                                               " & vbNewLine _
                    & "    ,SMK_DTL.REC_NO                                                                                                  " & vbNewLine _
                    & "    ,SMK_DTL.GYO                                                                                                     " & vbNewLine

#End Region

#Region "RCV_HED(実績送信済)"

    ''' <summary>
    ''' RCV_HED(実績送信済)
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Const SQL_UPD_JISSEKI_RCV_HED As String = "UPDATE $LM_TRN$..H_INOUTKAEDI_HED_SMK SET        " & vbNewLine _
                                              & " UPD_USER               = @UPD_USER                    " & vbNewLine _
                                              & ",UPD_DATE               = @UPD_DATE                    " & vbNewLine _
                                              & ",UPD_TIME               = @UPD_TIME                    " & vbNewLine _
                                              & ",SYS_UPD_DATE           = @SYS_UPD_DATE                " & vbNewLine _
                                              & ",SYS_UPD_TIME           = @SYS_UPD_TIME                " & vbNewLine _
                                              & ",SYS_UPD_PGID           = @SYS_UPD_PGID                " & vbNewLine _
                                              & ",SYS_UPD_USER           = @SYS_UPD_USER                " & vbNewLine _
                                              & "WHERE   CRT_DATE        = @CRT_DATE                    " & vbNewLine _
                                              & "AND FILE_NAME           = @FILE_NAME                   " & vbNewLine _
                                              & "AND REC_NO              = @REC_NO                      " & vbNewLine _
                                              & "--AND INOUT_KB            = '0'     		                " & vbNewLine

#End Region

#Region "RCV_DTL(実績送信済)"

    ''' <summary>
    ''' RCV_DTL(実績送信済)
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Const SQL_UPD_JISSEKI_RCV_DTL As String = "UPDATE $LM_TRN$..H_INOUTKAEDI_DTL_SMK SET        " & vbNewLine _
                                              & " JISSEKI_SHORI_FLG      = '3'                          " & vbNewLine _
                                              & ",SEND_USER              = @UPD_USER                    " & vbNewLine _
                                              & ",SEND_DATE              = @UPD_DATE                    " & vbNewLine _
                                              & ",SEND_TIME              = @UPD_TIME                    " & vbNewLine _
                                              & ",UPD_USER               = @UPD_USER                    " & vbNewLine _
                                              & ",UPD_DATE               = @UPD_DATE                    " & vbNewLine _
                                              & ",UPD_TIME               = @UPD_TIME                    " & vbNewLine _
                                              & ",SYS_UPD_DATE           = @SYS_UPD_DATE                " & vbNewLine _
                                              & ",SYS_UPD_TIME           = @SYS_UPD_TIME                " & vbNewLine _
                                              & ",SYS_UPD_PGID           = @SYS_UPD_PGID                " & vbNewLine _
                                              & ",SYS_UPD_USER           = @SYS_UPD_USER                " & vbNewLine _
                                              & "WHERE   CRT_DATE        = @CRT_DATE                    " & vbNewLine _
                                              & "AND FILE_NAME           = @FILE_NAME                   " & vbNewLine _
                                              & "AND REC_NO              = @REC_NO                      " & vbNewLine _
                                              & "AND GYO                 = @GYO                         " & vbNewLine _
                                              & "AND JISSEKI_SHORI_FLG   = '2'                          " & vbNewLine _
                                              & "--AND INOUT_KB            = '0'     		                " & vbNewLine


#End Region

#Region "H_OUTKAEDI_L"
    ''' <summary>
    ''' H_OUTKAEDI_LのUPDATE文（H_OUTKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKI_OUTKAEDIL As String = "UPDATE $LM_TRN$..H_OUTKAEDI_L SET       " & vbNewLine _
                                              & " JISSEKI_FLAG      = '2'                        " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                  " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                  " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                  " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE              " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME              " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID              " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER              " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                 " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                " & vbNewLine _
                                              & "AND JISSEKI_FLAG   = '1'                        " & vbNewLine

#End Region

#Region "H_OUTKAEDI_M"
    ''' <summary>
    ''' H_OUTKAEDI_MのUPDATE文（H_OUTKAEDI_M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKI_OUTKAEDIM As String = "UPDATE $LM_TRN$..H_OUTKAEDI_M SET       " & vbNewLine _
                                              & " JISSEKI_FLAG      = '2'                        " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                  " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                  " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                  " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE              " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME              " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID              " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER              " & vbNewLine _
                                              & "WHERE   NRS_BR_CD  = @NRS_BR_CD                 " & vbNewLine _
                                              & "AND EDI_CTL_NO     = @EDI_CTL_NO                " & vbNewLine _
                                              & "AND EDI_CTL_NO_CHU = @EDI_CTL_NO_CHU            " & vbNewLine _
                                              & "AND JISSEKI_FLAG   = '1'                        " & vbNewLine


#End Region

#Region "H_INKAEDI_L"
    ''' <summary>
    ''' H_INKAEDI_LのUPDATE文（H_INKAEDI_L）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKI_INKAEDIL As String = "UPDATE $LM_TRN$..H_INKAEDI_L SET         " & vbNewLine _
                                              & " JISSEKI_FLAG      = '2'                        " & vbNewLine _
                                              & ",UPD_USER          = @UPD_USER                  " & vbNewLine _
                                              & ",UPD_DATE          = @UPD_DATE                  " & vbNewLine _
                                              & ",UPD_TIME          = @UPD_TIME                  " & vbNewLine _
                                              & ",SYS_UPD_DATE      = @SYS_UPD_DATE              " & vbNewLine _
                                              & ",SYS_UPD_TIME      = @SYS_UPD_TIME              " & vbNewLine _
                                              & ",SYS_UPD_PGID      = @SYS_UPD_PGID              " & vbNewLine _
                                              & ",SYS_UPD_USER      = @SYS_UPD_USER              " & vbNewLine _
                                              & " WHERE                                          " & vbNewLine _
                                              & " NRS_BR_CD         = @NRS_BR_CD                 " & vbNewLine _
                                              & " AND EDI_CTL_NO    = @EDI_CTL_NO                " & vbNewLine _
                                              & " AND JISSEKI_FLAG  = '1'                        " & vbNewLine


#End Region

#Region "H_INKAEDI_M"
    ''' <summary>
    ''' H_INKAEDI_MのUPDATE文（H_INKAEDI_M）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_JISSEKI_INKAEDIM As String = "UPDATE $LM_TRN$..H_INKAEDI_M SET         " & vbNewLine _
                                             & " JISSEKI_FLAG      = '2'                         " & vbNewLine _
                                             & ",UPD_USER          = @UPD_USER                   " & vbNewLine _
                                             & ",UPD_DATE          = @UPD_DATE                   " & vbNewLine _
                                             & ",UPD_TIME          = @UPD_TIME                   " & vbNewLine _
                                             & ",SYS_UPD_DATE      = @SYS_UPD_DATE               " & vbNewLine _
                                             & ",SYS_UPD_TIME      = @SYS_UPD_TIME               " & vbNewLine _
                                             & ",SYS_UPD_PGID      = @SYS_UPD_PGID               " & vbNewLine _
                                             & ",SYS_UPD_USER      = @SYS_UPD_USER               " & vbNewLine _
                                             & " WHERE                                           " & vbNewLine _
                                             & " NRS_BR_CD         = @NRS_BR_CD                  " & vbNewLine _
                                             & " AND EDI_CTL_NO     = @EDI_CTL_NO                " & vbNewLine _
                                             & " AND EDI_CTL_NO_CHU = @EDI_CTL_NO_CHU            " & vbNewLine _
                                             & " AND JISSEKI_FLAG   = '1'                        " & vbNewLine

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

#Region "SQLメイン処理"

#Region "TXT出力データの検索"

    ''' <summary>
    ''' TXT出力データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectTXT(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI930IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI930DAC.SQL_SELECT)       'SQL構築(Select句 出荷)
        Me._StrSql.Append(LMI930DAC.SQL_SELECT_FROM)  'SQL構築(From句 出荷)
        Call SetSQLWhere(inTbl.Rows(0))                '条件設定
        Me._StrSql.Append(LMI930DAC.SQL_SELECT_ORDER)  'SQL構築(Order句 出荷)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI930DAC", "SelectTXT", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("COMPANY_CD", "COMPANY_CD")
        map.Add("RB_KB", "RB_KB")
        map.Add("DENP_NO", "DENP_NO")
        map.Add("DENP_NO_EDA", "DENP_NO_EDA")
        map.Add("DENP_NO_REN", "DENP_NO_REN")
        map.Add("DENP_NO_GYO", "DENP_NO_GYO")
        map.Add("SHORI_DATE", "SHORI_DATE")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("UKE_CHUKEI", "UKE_CHUKEI")
        map.Add("UKE_AITE", "UKE_AITE")
        map.Add("UKE_SHUBETSU", "UKE_SHUBETSU")
        map.Add("GOODS_CLASS", "GOODS_CLASS")
        map.Add("GOODS_CD", "GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("NISUGATA", "NISUGATA")
        map.Add("IRIME", "IRIME")
        map.Add("TANA_KB", "TANA_KB")
        map.Add("ZAIKO_KB", "ZAIKO_KB")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("SEISAN_KOJO", "SEISAN_KOJO")
        map.Add("KOSU", "KOSU")
        map.Add("SURYO", "SURYO")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("YOUKI_JOKEN", "YOUKI_JOKEN")
        map.Add("NIWATASHI_JOKEN", "NIWATASHI_JOKEN")
        map.Add("SHIKEN_HYO", "SHIKEN_HYO")
        map.Add("SHITEI_DENPYO", "SHITEI_DENPYO")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("DEST_MSG", "DEST_MSG")
        map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
        map.Add("BUYER_CD", "BUYER_CD")
        map.Add("BUYER_TEL", "BUYER_TEL")
        map.Add("KOJO_MSG", "KOJO_MSG")
        map.Add("HANBAI_KA", "HANBAI_KA")
        map.Add("HANBAI_TANTO", "HANBAI_TANTO")
        map.Add("ORDER_KB", "ORDER_KB")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("BUYER_NM", "BUYER_NM")
        map.Add("DEST_NM_KANA", "DEST_NM_KANA")
        map.Add("DEST_AD_KANA1", "DEST_AD_KANA1")
        map.Add("DEST_AD_KANA2", "DEST_AD_KANA2")
        map.Add("SAKUSEI_DATE", "SAKUSEI_DATE")
        map.Add("SAKUSEI_TIME", "SAKUSEI_TIME")
        map.Add("RACK_NO", "RACK_NO")
        map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
        map.Add("RIYUU", "RIYUU")
        map.Add("DATA_KB", "DATA_KB")
        map.Add("YUSO_SHUDAN", "YUSO_SHUDAN")
        map.Add("YUSO_GYOSHA", "YUSO_GYOSHA")
        map.Add("YUSO_JIS_CD", "YUSO_JIS_CD")
        map.Add("JISSEKI_KBN", "JISSEKI_KBN")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO", "GYO")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INOUT_KB", "INOUT_KB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI930_SMKJISSEKI_TXT")

        reader.Close()

        Return ds

    End Function

#End Region

#Region "H_INOUTKAEDI_HED"

    ''' <summary>
    ''' EDI受信(HED)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI受信(HED)テーブル更新SQLの構築・発行</remarks>
    Private Function updateInOutkaSmkHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim ediRcvHedTbl As DataTable = ds.Tables("LMI930_SMKJISSEKI_TXT")
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = ediRcvHedTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '実績作成
        setSql = LMI930DAC.SQL_UPD_JISSEKI_RCV_HED

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetOutkaEdiRcvComParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI930DAC", "updateInOutkaSmkHed", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "H_INOUTKAEDI_DTL"

    ''' <summary>
    ''' EDI受信(DTL)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI受信(DTL)テーブル更新SQLの構築・発行</remarks>
    Private Function updateInOutkaSmkDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim ediRcvDtlTbl As DataTable = ds.Tables("LMI930_SMKJISSEKI_TXT")
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = ediRcvDtlTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '実績作成
        setSql = LMI930DAC.SQL_UPD_JISSEKI_RCV_DTL

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetOutkaEdiRcvComParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI930DAC", "updateInOutkaSmkDtl", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "H_OUTKAEDI_L"

    ''' <summary>
    ''' EDI出荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function updateOutkaEdiL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim ediLTbl As DataTable = ds.Tables("LMI930_SMKJISSEKI_TXT")
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = ediLTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '実績作成
        setSql = LMI930DAC.SQL_UPD_JISSEKI_OUTKAEDIL

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetOutkaEdiRcvComParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI930DAC", "updateOutkaEdiL", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "H_OUTKAEDI_M"

    ''' <summary>
    ''' EDI出荷(中)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI出荷(中)テーブル更新SQLの構築・発行</remarks>
    Private Function updateOutkaEdiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim ediMTbl As DataTable = ds.Tables("LMI930_SMKJISSEKI_TXT")
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = ediMTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '実績作成
        setSql = LMI930DAC.SQL_UPD_JISSEKI_OUTKAEDIM

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetOutkaEdiRcvComParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI930DAC", "updateOutkaEdiM", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "H_INKAEDI_L"

    ''' <summary>
    ''' EDI入荷(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI入荷(大)テーブル更新SQLの構築・発行</remarks>
    Private Function updateInkaEdiL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim ediLTbl As DataTable = ds.Tables("LMI930_SMKJISSEKI_TXT")
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = ediLTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '実績作成
        setSql = LMI930DAC.SQL_UPD_JISSEKI_INKAEDIL

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetOutkaEdiRcvComParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI930DAC", "updateInkaEdiL", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "H_INKAEDI_M"

    ''' <summary>
    ''' EDI入荷(中)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDI入荷(中)テーブル更新SQLの構築・発行</remarks>
    Private Function updateInkaEdiM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim ediMTbl As DataTable = ds.Tables("LMI930_SMKJISSEKI_TXT")
        Dim setSql As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = ediMTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '実績作成
        setSql = LMI930DAC.SQL_UPD_JISSEKI_INKAEDIM

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(setSql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetOutkaEdiRcvComParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI930DAC", "updateInkaEdiM", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#End Region

#Region "SQL条件設定"

#Region "SQL条件設定 TXT出力データの検索"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSQLWhere(ByVal inTblRow As DataRow)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim strSqlAppend As String = String.Empty
        With inTblRow

            Me._StrSql.Append("WHERE                                                          ")
            Me._StrSql.Append(vbNewLine)

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" SMK_DTL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '各荷主指定
            If .Item("OUTPUT_TANI_KBN").ToString().Equals("03") = True Then

                '荷主コード(大)
                whereStr = .Item("CUST_CD_L").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append(" AND SMK_DTL.CUST_CD_L = @CUST_CD_L")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
                End If

                '荷主コード(中)
                whereStr = .Item("CUST_CD_M").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append(" AND SMK_DTL.CUST_CD_M = @CUST_CD_M")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
                End If

            End If

            '実績作成済(出荷予定日)
            If .Item("OUTPUT_TAISYO_KBN").ToString().Equals("02") = True Then

                '出荷予定日(FROM)
                whereStr = .Item("OUTKA_PLAN_DATE_FROM").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append(" AND SMK_HED.OUTKA_PLAN_DATE >= @OUTKA_PLAN_DATE_FROM")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE_FROM", whereStr, DBDataType.CHAR))
                End If

                '出荷予定日(TO)
                whereStr = .Item("OUTKA_PLAN_DATE_TO").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    Me._StrSql.Append(" AND SMK_HED.OUTKA_PLAN_DATE <= @OUTKA_PLAN_DATE_TO")
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE_TO", whereStr, DBDataType.CHAR))
                End If

            End If

            '共通条件
            Me._StrSql.Append(" AND SMK_DTL.DEL_KB <> '1'")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND SMK_HED.DEL_KB <> '1'")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND SMK_DTL.JISSEKI_SHORI_FLG = '2'")
            Me._StrSql.Append(vbNewLine)

        End With

    End Sub

#End Region

#Region "SQL条件設定 共通"

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList)

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter(prmList)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList)

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

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

#Region "EDI受信(HED/DTL)・EDI出荷(大・中)更新パラメータ設定"

    ''' <summary>
    ''' EDI受信(HED/DTL)・EDI出荷(大・中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaEdiRcvComParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            Dim updTime As String = Me.GetColonEditTime(MyBase.GetSystemTime())

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD"), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE"), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME"), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO"), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO", .Item("GYO"), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO"), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", .Item("EDI_CTL_NO_CHU"), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UPD_TIME", updTime))

        End With

    End Sub

#End Region

#Region "時間コロン編集"
    ''' <summary>
    ''' 時間コロン編集
    ''' </summary>
    ''' <param name="value">サーバ時間</param>
    ''' <returns>時間</returns>
    ''' <remarks></remarks>
    Private Function GetColonEditTime(ByVal value As String) As String

        Return String.Concat(value.Substring(0, 2), ":", value.Substring(2, 2), ":", value.Substring(4, 2))

    End Function
#End Region

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            'MyBase.SetMessage("E011")
            'Return False
        End If

        Return True

    End Function

#End Region

#End Region

End Class
