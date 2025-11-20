' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN810DAC : 欠品チェック
'  作  成  者       :  [佐川央]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMN810DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN810DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _row As Data.DataRow

    ''' <summary>
    ''' 発行SQL作成用
    ''' </summary>
    ''' <remarks></remarks>
    Private _strSql As StringBuilder

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _sqlPrmList As ArrayList

    ''' <summary>
    ''' マスタ用スキーマ名称
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstSchemaNm As String

    ''' <summary>
    ''' トランザクション用スキーマ名称
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMNTrnSchemaNm As String

    ''' <summary>
    ''' EDI用スキーマ名称
    ''' </summary>
    ''' <remarks></remarks>
    Private _TrnEDINm As String

    ''' <summary>
    ''' EDIマスタ用スキーマ名称
    ''' </summary>
    ''' <remarks></remarks>
    Private _MstEDINm As String

    ''' <summary>
    ''' 接続先名称
    ''' </summary>
    ''' <remarks></remarks>
    Private _ConnectNm As String

    ''' <summary>
    ''' LMSのコネクション
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMS1 As SqlConnection = New SqlConnection

#End Region

#Region "Const"

    'マスタスキーマ名
    Private Const MST_SCHEMA As String = "LM_MST"

    'トランザクションスキーマ名
    Private Const TRN_SCHEMA As String = "LM_TRN"

    'TODO(後で削除)
    '***************** テスト用現在日付 *******************'
    Private Const NOW_DATE As String = "20110201"

#End Region

#Region "SQL"

    ''' <summary>
    ''' (LMS)予定総入荷数取得(B_INKA_M)SQL作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLGetB_INKA_M()

        With Me._row

            Me._strSql.Append("SELECT       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("SUM(S.KONSU * G.PKG_NB + S.HASU)    AS PLAN_INKA_NB   ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("FROM       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._TrnEDINm)
            Me._strSql.Append("B_INKA_M M       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("LEFT JOIN       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._TrnEDINm)
            Me._strSql.Append("B_INKA_S S       ")
            Me._strSql.Append("ON       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("M.INKA_NO_L = S.INKA_NO_L       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("M.INKA_NO_M = S.INKA_NO_M       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("LEFT JOIN       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._MstEDINm)
            Me._strSql.Append("M_GOODS G       ")
            Me._strSql.Append("ON       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("M.GOODS_CD_NRS = G.GOODS_CD_NRS        ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("RIGHT JOIN       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._TrnEDINm)
            Me._strSql.Append("B_INKA_L L       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("ON       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.INKA_NO_L = M.INKA_NO_L       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.NRS_BR_CD = @BR_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.WH_CD = @SOKO_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.CUST_CD_L = @LMS_CUST_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("(L.INKA_DATE >= @NOW_DATE AND L.INKA_DATE <= @OUTKA_DATE)       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.INKA_STATE_KB < '50'       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.SYS_DEL_FLG = '0'       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("WHERE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("G.GOODS_CD_CUST = @GOODS_CD_CUST       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("M.SYS_DEL_FLG = '0'       ")

        End With

    End Sub

    ''' <summary>
    ''' (LMS)予定総入荷数取得(H_INKAEDI_M)SQL作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLGetH_INKAEDI_M()

        With Me._row

            Me._strSql.Append("SELECT       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("SUM(M.NB)    AS PLAN_INKA_NB   ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("FROM       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._TrnEDINm)
            Me._strSql.Append("H_INKAEDI_M M       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("RIGHT JOIN       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._TrnEDINm)
            Me._strSql.Append("H_INKAEDI_L L       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("ON       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.EDI_CTL_NO = M.EDI_CTL_NO       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.NRS_BR_CD = @BR_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.NRS_WH_CD = @SOKO_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.CUST_CD_L = @LMS_CUST_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("(L.INKA_DATE >= @NOW_DATE AND L.INKA_DATE <= @OUTKA_DATE)       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.INKA_CTL_NO_L = ''       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.DEL_KB = '0'       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("WHERE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("M.CUST_GOODS_CD = @GOODS_CD_CUST       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("M.DEL_KB = '0'       ")

        End With

    End Sub

    ''' <summary>
    ''' (LMS)予定総出荷数取得(C_OUTKA_M)SQL作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLGetC_OUTKA_M()

        With Me._row

            Me._strSql.Append("SELECT       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("SUM(M.OUTKA_TTL_NB)      AS PLAN_OUTKA_NB    ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("FROM       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._TrnEDINm)
            Me._strSql.Append("C_OUTKA_M M       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("RIGHT JOIN       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._TrnEDINm)
            Me._strSql.Append("C_OUTKA_L L       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("ON       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.OUTKA_NO_L = M.OUTKA_NO_L       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.NRS_BR_CD = @BR_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.WH_CD = @SOKO_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.CUST_CD_L = @LMS_CUST_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("( L.OUTKA_PLAN_DATE <= @OUTKA_DATE AND L.OUTKA_PLAN_DATE >=@NOW_DATE ) ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.OUTKA_STATE_KB < '60'       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.SYS_DEL_FLG = '0'       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("WHERE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("M.GOODS_CD_NRS = (SELECT       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                       GOODS_CD_NRS       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                  FROM       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._MstEDINm)
            Me._strSql.Append("M_GOODS       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                  WHERE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                       NRS_BR_CD = @BR_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                  AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                       CUST_CD_L = @LMS_CUST_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                  AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                       GOODS_CD_CUST = @GOODS_CD_CUST       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                  AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                       SYS_DEL_FLG = '0')       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("M.SYS_DEL_FLG = '0'       ")

        End With

    End Sub

    ''' <summary>
    ''' (LMS)予定総出荷数取得(H_OUTKAEDI_M)SQL作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLGetH_OUTKAEDI_M()

        With Me._row

            Me._strSql.Append("SELECT       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("SUM(M.OUTKA_TTL_NB)      AS PLAN_OUTKA_NB    ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("FROM       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._TrnEDINm)
            Me._strSql.Append("H_OUTKAEDI_M M       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("RIGHT JOIN       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._TrnEDINm)
            Me._strSql.Append("H_OUTKAEDI_L L       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("ON       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.EDI_CTL_NO = M.EDI_CTL_NO       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.NRS_BR_CD = @BR_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.WH_CD = @SOKO_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.CUST_CD_L = @LMS_CUST_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(" ( L.OUTKA_PLAN_DATE <= @OUTKA_DATE AND L.OUTKA_PLAN_DATE >= @NOW_DATE ) ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.OUTKA_CTL_NO = ''      ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.DEL_KB = '0'       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("WHERE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("M.CUST_GOODS_CD = @GOODS_CD_CUST       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("M.DEL_KB = '0'       ")

        End With

    End Sub

    ''' <summary>
    ''' (LMS)現在在庫数取得(D_ZAI_TRS)SQL作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLGetD_ZAI_TRS()

        With Me._row

            Me._strSql.Append("SELECT       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("SUM(ZAI.PORA_ZAI_NB)    AS ZAIKO_NB    ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("FROM       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._TrnEDINm)
            Me._strSql.Append("D_ZAI_TRS ZAI      ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("LEFT JOIN       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._TrnEDINm)
            Me._strSql.Append("B_INKA_L INKA       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("ON       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("INKA.INKA_NO_L = ZAI.INKA_NO_L       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("INKA.SYS_DEL_FLG = '0'       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("WHERE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("ZAI.NRS_BR_CD = @BR_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("ZAI.WH_CD = @SOKO_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("ZAI.GOODS_CD_NRS = (SELECT       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                     GOODS_CD_NRS       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                FROM       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._MstEDINm)
            Me._strSql.Append("M_GOODS       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                WHERE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                     NRS_BR_CD = @BR_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                     CUST_CD_L = @LMS_CUST_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                     GOODS_CD_CUST = @GOODS_CD_CUST       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("                     SYS_DEL_FLG = '0')       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("INKA.INKA_STATE_KB >= '50'       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("ZAI.SYS_DEL_FLG = '0'       ")

        End With

    End Sub

    ''' <summary>
    ''' 現在在庫数取得(N_OUTKASCM_M)SQL作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLGetN_OUTKASCM_M()

        Call Me.SetSchemaNm()

        With Me._row

            Me._strSql.Append("SELECT       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("SUM(M.OUTKA_TTL_NB)     AS SCM_OUTKA_NB  ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("FROM       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._LMNTrnSchemaNm)
            Me._strSql.Append("N_OUTKASCM_M M       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("RIGHT JOIN       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append(Me._LMNTrnSchemaNm)
            Me._strSql.Append("N_OUTKASCM_L L       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("ON       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.SCM_CTL_NO_L = M.SCM_CTL_NO_L       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.STATUS_KBN = '01'       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.BR_CD = @BR_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.SOKO_CD = @SOKO_CD       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("(L.OUTKA_DATE >= @NOW_DATE AND L.OUTKA_DATE <= @OUTKA_DATE)       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("L.SYS_DEL_FLG = '0'       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("WHERE       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("M.CUST_GOODS_CD = @GOODS_CD_CUST      ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("AND       ")
            Me._strSql.Append(vbNewLine)
            Me._strSql.Append("M.SYS_DEL_FLG = '0'       ")

        End With

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' (LMS)予定総入荷数取得(B_INKA_M)(LMSVer1)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetB_INKA_MLMSVer1(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '営業所コード取得
        Dim brCd As String = ds.Tables("BR_CD_LIST").Rows(0).Item("BR_CD").ToString()

        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN810IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        '******************************** LMSコネクション開始 *****************************

        Using Me._LMS1

            Try
                'LMSVer1のOpen処理
                Call Me.OpenConnectionLMS1(brCd)

                'SQL作成
                Call Me.SQLGetB_INKA_M()

                'SQL文のコンパイル
                Dim cmd As SqlCommand = New SqlClient.SqlCommand(Me._strSql.ToString(), Me._LMS1)

                'INTableの条件rowの格納
                Me._row = inTbl.Rows(0)

                'SQLパラメータ設定
                Call Me.SetParamGetB_INKA_M()

                'パラメータの反映
                For Each obj As Object In Me._sqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMN810DAC", "GetB_INKA_MLMSVer1", cmd)

                'SQLの発行
                Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                map.Add("PLAN_INKA_NB", "PLAN_INKA_NB")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN810OUT")

                Return ds

            Catch

                Throw

            Finally

                'LMSVer1のClose処理
                Call Me.CloseConnectionLMS1()

            End Try

        End Using

    End Function

    ''' <summary>
    ''' (LMS)予定総入荷数取得(B_INKA_M)(LMSVer2)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetB_INKA_MLMSVer2(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN810IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL作成
        Call Me.SQLGetB_INKA_M()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQLパラメータ設定
        Call Me.SetParamGetB_INKA_M()

        'パラメータの反映
        For Each obj As Object In Me._sqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN810DAC", "GetB_INKA_MLMSVer2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("PLAN_INKA_NB", "PLAN_INKA_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN810OUT")

        Return ds

    End Function

    ''' <summary>
    ''' (LMS)予定総入荷数取得(H_INKAEDI_M)(LMSVer1)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetH_INKAEDI_MLMSVer1(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '営業所コード取得
        Dim brCd As String = ds.Tables("BR_CD_LIST").Rows(0).Item("BR_CD").ToString()
        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN810IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        '******************************** LMSコネクション開始 *****************************

        Using Me._LMS1

            Try
                'LMSVer1のOpen処理
                Call Me.OpenConnectionLMS1(brCd)

                'SQL作成
                Call Me.SQLGetH_INKAEDI_M()

                'SQL文のコンパイル
                Dim cmd As SqlCommand = New SqlClient.SqlCommand(Me._strSql.ToString(), Me._LMS1)

                'INTableの条件rowの格納
                Me._row = inTbl.Rows(0)

                'SQLパラメータ設定
                Call Me.SetParamGetH_INKAEDI_M()

                'パラメータの反映
                For Each obj As Object In Me._sqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMN810DAC", "GetH_INKAEDI_MLMSVer1", cmd)

                'SQLの発行
                Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                map.Add("PLAN_INKA_NB", "PLAN_INKA_NB")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN810OUT")

                Return ds

            Catch

                Throw

            Finally

                'LMSVer1のClose処理
                Call Me.CloseConnectionLMS1()

            End Try

        End Using

    End Function

    ''' <summary>
    ''' (LMS)予定総入荷数取得(H_INKAEDI_M)(LMSVer2)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetH_INKAEDI_MLMSVer2(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN810IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL作成
        Call Me.SQLGetH_INKAEDI_M()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQLパラメータ設定
        Call Me.SetParamGetH_INKAEDI_M()

        'パラメータの反映
        For Each obj As Object In Me._sqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN810DAC", "GetH_INKAEDI_MLMSVer2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("PLAN_INKA_NB", "PLAN_INKA_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN810OUT")

        Return ds

    End Function

    ''' <summary>
    ''' (LMS)予定総出荷数取得(C_OUTKA_M)(LMSVer1)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetC_OUTKA_MLMSVer1(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '営業所コード取得
        Dim brCd As String = ds.Tables("BR_CD_LIST").Rows(0).Item("BR_CD").ToString()
        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN810IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        '******************************** LMSコネクション開始 *****************************

        Using Me._LMS1

            Try
                'LMSVer1のOpen処理
                Call Me.OpenConnectionLMS1(brCd)

                'SQL作成
                Call Me.SQLGetC_OUTKA_M()

                'SQL文のコンパイル
                Dim cmd As SqlCommand = New SqlClient.SqlCommand(Me._strSql.ToString(), Me._LMS1)

                'INTableの条件rowの格納
                Me._row = inTbl.Rows(0)

                'SQLパラメータ設定
                Call Me.SetParamGetC_OUTKA_M()

                'パラメータの反映
                For Each obj As Object In Me._sqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMN810DAC", "GetC_OUTKA_MLMSVer1", cmd)

                'SQLの発行
                Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                map.Add("PLAN_OUTKA_NB", "PLAN_OUTKA_NB")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN810OUT")

                Return ds

            Catch

                Throw

            Finally

                'LMSVer1のClose処理
                Call Me.CloseConnectionLMS1()

            End Try

        End Using

    End Function

    ''' <summary>
    ''' (LMS)予定総出荷数取得(C_OUTKA_M)(LMSVer2)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetC_OUTKA_MLMSVer2(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN810IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL作成
        Call Me.SQLGetC_OUTKA_M()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQLパラメータ設定
        Call Me.SetParamGetC_OUTKA_M()

        'パラメータの反映
        For Each obj As Object In Me._sqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN810DAC", "GetC_OUTKA_MLMSVer2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("PLAN_OUTKA_NB", "PLAN_OUTKA_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN810OUT")

        Return ds

    End Function

    ''' <summary>
    ''' (LMS)予定総出荷数取得(H_OUTKAEDI_M)(LMSVer1)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetH_OUTKAEDI_MLMSVer1(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '営業所コード取得
        Dim brCd As String = ds.Tables("BR_CD_LIST").Rows(0).Item("BR_CD").ToString()
        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN810IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        '******************************** LMSコネクション開始 *****************************

        Using Me._LMS1

            Try
                'LMSVer1のOpen処理
                Call Me.OpenConnectionLMS1(brCd)

                'SQL作成
                Call Me.SQLGetH_OUTKAEDI_M()

                'SQL文のコンパイル
                Dim cmd As SqlCommand = New SqlClient.SqlCommand(Me._strSql.ToString(), Me._LMS1)

                'INTableの条件rowの格納
                Me._row = inTbl.Rows(0)

                'SQLパラメータ設定
                Call Me.SetParamGetH_OUTKAEDI_M()

                'パラメータの反映
                For Each obj As Object In Me._sqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMN810DAC", "GetH_OUTKAEDI_MLMSVer1", cmd)

                'SQLの発行
                Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                map.Add("PLAN_OUTKA_NB", "PLAN_OUTKA_NB")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN810OUT")

                Return ds

            Catch

                Throw

            Finally

                'LMSVer1のClose処理
                Call Me.CloseConnectionLMS1()

            End Try

        End Using

    End Function

    ''' <summary>
    ''' (LMS)予定総出荷数取得(H_OUTKAEDI_M)(LMSVer2)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetH_OUTKAEDI_MLMSVer2(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN810IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL作成
        Call Me.SQLGetH_OUTKAEDI_M()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQLパラメータ設定
        Call Me.SetParamGetH_OUTKAEDI_M()

        'パラメータの反映
        For Each obj As Object In Me._sqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN810DAC", "GetH_OUTKAEDI_MLMSVer2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("PLAN_OUTKA_NB", "PLAN_OUTKA_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN810OUT")

        Return ds

    End Function

    ''' <summary>
    ''' (LMS)現在在庫数取得(D_ZAI_TRS)(LMSVer1)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetD_ZAI_TRSLMSVer1(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '営業所コード取得
        Dim brCd As String = ds.Tables("BR_CD_LIST").Rows(0).Item("BR_CD").ToString()

        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN810IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        '******************************** LMSコネクション開始 *****************************

        Using Me._LMS1

            Try
                'LMSVer1のOpen処理
                Call Me.OpenConnectionLMS1(brCd)

                'SQL作成
                Call Me.SQLGetD_ZAI_TRS()

                'SQL文のコンパイル
                Dim cmd As SqlCommand = New SqlClient.SqlCommand(Me._strSql.ToString(), Me._LMS1)

                'INTableの条件rowの格納
                Me._row = inTbl.Rows(0)

                'SQLパラメータ設定
                Call Me.SetParamGetD_ZAI_TRS()

                'パラメータの反映
                For Each obj As Object In Me._sqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                MyBase.Logger.WriteSQLLog("LMN810DAC", "GetD_ZAI_TRSLMSVer1", cmd)

                'SQLの発行
                Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                'DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                map.Add("ZAIKO_NB", "ZAIKO_NB")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN810OUT")

                Return ds

            Catch

                Throw

            Finally

                'LMSVer1のClose処理
                Call Me.CloseConnectionLMS1()

            End Try

        End Using

    End Function

    ''' <summary>
    ''' (LMS)現在在庫数取得(D_ZAI_TRS)(LMSVer2)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetD_ZAI_TRSLMSVer2(ByVal ds As DataSet) As DataSet

        'DACの初期で必ず指定
        Call LMNControl()

        '接続スキーマ名称取得
        Call Me.GetLMSConnectName(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN810IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL作成
        Call Me.SQLGetD_ZAI_TRS()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQLパラメータ設定
        Call Me.SetParamGetD_ZAI_TRS()

        'パラメータの反映
        For Each obj As Object In Me._sqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN810DAC", "GetD_ZAI_TRSLMSVer2", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("ZAIKO_NB", "ZAIKO_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN810OUT")

        Return ds

    End Function

    ''' <summary>
    ''' SCM合計総出荷個数取得(N_OUTKASCM_M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetN_OUTKASCM_M(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMN810IN")

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL作成
        Call Me.SQLGetN_OUTKASCM_M()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        'INTableの条件rowの格納
        Me._row = inTbl.Rows(0)

        'SQLパラメータ初期化/設定
        Call Me.SetParamGetN_OUTKASCM_M()

        'パラメータの反映
        For Each obj As Object In Me._sqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMN810DAC", "GetN_OUTKASCM_M", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SCM_OUTKA_NB", "SCM_OUTKA_NB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMN810OUT")

        Return ds

    End Function

#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(B_INKA_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamGetB_INKA_M()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@BR_CD", .Item("BR_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("SOKO_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@LMS_CUST_CD", .Item("LMS_CUST_CD").ToString(), DBDataType.CHAR))

            'TODO(後でコメントはずす)
            'Me._sqlPrmList.Add(MyBase.GetSqlParameter("@NOW_DATE", Me.GetSystemDate(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@NOW_DATE", NOW_DATE, DBDataType.CHAR))

            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE", .Item("OUTKA_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(H_INKAEDI_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamGetH_INKAEDI_M()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@BR_CD", .Item("BR_CD").ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("SOKO_CD").ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@LMS_CUST_CD", .Item("LMS_CUST_CD").ToString(), DBDataType.NVARCHAR))

            'TODO(後でコメントはずす)
            'Me._sqlPrmList.Add(MyBase.GetSqlParameter("@NOW_DATE", Me.GetSystemDate(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@NOW_DATE", NOW_DATE, DBDataType.CHAR))

            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE", .Item("OUTKA_DATE").ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(C_OUTKA_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamGetC_OUTKA_M()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@BR_CD", .Item("BR_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("SOKO_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@LMS_CUST_CD", .Item("LMS_CUST_CD").ToString(), DBDataType.CHAR))

            'TODO(後でコメントはずす)
            'Me._sqlPrmList.Add(MyBase.GetSqlParameter("@NOW_DATE", Me.GetSystemDate(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@NOW_DATE", NOW_DATE, DBDataType.CHAR))

            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE", .Item("OUTKA_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(H_OUTKAEDI_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamGetH_OUTKAEDI_M()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@BR_CD", .Item("BR_CD").ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("SOKO_CD").ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@LMS_CUST_CD", .Item("LMS_CUST_CD").ToString(), DBDataType.NVARCHAR))

            'TODO(後でコメントはずす)
            'Me._sqlPrmList.Add(MyBase.GetSqlParameter("@NOW_DATE", Me.GetSystemDate(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@NOW_DATE", NOW_DATE, DBDataType.CHAR))

            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE", .Item("OUTKA_DATE").ToString(), DBDataType.NVARCHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(D_ZAI_TRS)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamGetD_ZAI_TRS()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@BR_CD", .Item("BR_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("SOKO_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@LMS_CUST_CD", .Item("LMS_CUST_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(N_OUTKASCM_M)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamGetN_OUTKASCM_M()

        'SQLパラメータ初期化
        Me._sqlPrmList = New ArrayList()

        With Me._row
            'パラメータ設定
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@BR_CD", .Item("BR_CD").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_CD", .Item("SOKO_CD").ToString(), DBDataType.CHAR))

            'TODO(後でコメントはずす)
            'Me._sqlPrmList.Add(MyBase.GetSqlParameter("@NOW_DATE", Me.GetSystemDate(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@NOW_DATE", NOW_DATE, DBDataType.CHAR))

            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE", .Item("OUTKA_DATE").ToString(), DBDataType.CHAR))
            Me._sqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", .Item("GOODS_CD_CUST").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

#End Region

#Region "スキーマ設定"

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSchemaNm()

        Me._MstSchemaNm = String.Concat(MST_SCHEMA, "..")

        Me._LMNTrnSchemaNm = String.Concat(TRN_SCHEMA, "..")

    End Sub

#End Region

#Region "接続名取得"

    Private Sub GetLMSConnectName(ByVal ds As DataSet)

        'DataSetの接続DB名称情報を取得
        Dim dbTbl As DataTable = ds.Tables("BR_CD_LIST")

        '営業所コード取得
        Dim brCd As String = dbTbl.Rows(0).Item("BR_CD").ToString()

        Me._TrnEDINm = String.Concat(Me.GetSchemaEDI(brCd), "..")
        Me._MstEDINm = String.Concat(Me.GetSchemaEDIMst(brCd), "..")

        Me._MstSchemaNm = String.Concat(MST_SCHEMA, "..")
        Me._LMNTrnSchemaNm = String.Concat(TRN_SCHEMA, "..")

        ''DataSetの接続DB名称情報を取得
        'Dim dbTbl As DataTable = ds.Tables("BR_CD_LIST")

        ''営業所コード取得
        'Dim brCd As String = dbTbl.Rows(0).Item("BR_CD").ToString()

        ''移行前
        'If dbTbl.Rows(0).Item("IKO_FLG").ToString() = "00" Then
        '    Me._MstSchemaNm = String.Concat(MyBase.GetDatabaseName(brCd, DBKbn.ARC), "..")
        '    Me._TrnSchemaNm = String.Concat(MyBase.GetDatabaseName(brCd, DBKbn.ARC), "..")
        '    Me._ConnectNm = dbTbl.Rows(0).Item("SV1_CONNECT_NM").ToString()
        'End If
        ''移行後
        'If dbTbl.Rows(0).Item("IKO_FLG").ToString() = "01" Then
        '    Me._MstSchemaNm = String.Concat(MyBase.GetDatabaseName(brCd, DBKbn.MST), "..")
        '    Me._TrnSchemaNm = String.Concat(MyBase.GetDatabaseName(brCd, DBKbn.TRN), "..")
        '    Me._ConnectNm = dbTbl.Rows(0).Item("SV2_CONNECT_NM").ToString()
        'End If

    End Sub

#End Region

#Region "LMS DB OPen/Close"

    ''' <summary>
    ''' LMSVer1のOPEN
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenConnectionLMS1(ByVal brCd As String)

        Me._LMS1.ConnectionString = Me.GetConnectionLMS1(brCd)
        Me._LMS1.Open()


    End Sub

    ''' <summary>
    '''  LMSVer1のCLOSE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CloseConnectionLMS1()

        Me._LMS1.Close()
        Me._LMS1.Dispose()

    End Sub

#End Region

#Region "LMNControl"

    Private Sub LMNControl()

        If _kbnDs Is Nothing = True Then

            Me.SetSchemaNm()

            Me.CreateKbnDataSet()

            Me.SetConnectDataSet(_kbnDs)

        End If

    End Sub


    ''' <summary>
    ''' 区分マスタ保持用
    ''' </summary>
    ''' <remarks></remarks>
    Private _kbnDs As DataSet

#Region "Const"

    Private Const COL_BR_CD As String = "COL_BR_CD"

    Private Const COL_IKO_FLG As String = "COL_IKO_FLG"

    Private Const COL_LMS_SV_NM As String = "COL_LMS_SV_NM"

    Private Const COL_LMS_SCHEMA_NM As String = "COL_LMS_SCHEMA_NM"

    Private Const COL_LMS2_SV_NM As String = "COL_LMS2_SV_NM"

    Private Const COL_LMS2_SCHEMA_NM As String = "COL_LMS2_SCHEMA_NM"

#End Region

    Private Sub CreateKbnDataSet()

        '区分マスタ取得
        _kbnDs = New DataSet
        Dim dt As DataTable = New DataTable
        _kbnDs.Tables.Add(dt)
        _kbnDs.Tables(0).TableName = "Z_KBN"

        For i As Integer = 0 To 17
            _kbnDs.Tables("Z_KBN").Columns.Add(SetCol(i))
        Next

    End Sub

    Private Function SetCol(ByVal colno As Integer) As DataColumn
        Dim col As DataColumn = New DataColumn
        Dim colname As String = String.Empty
        col = New DataColumn
        Select Case colno
            Case 0
                colname = "KBN_GROUP_CD"
            Case 1
                colname = "KBN_CD"
            Case 2
                colname = "KBN_KEYWORD"
            Case 3 'KBN_NM1
                colname = "KBN_NM1"
            Case 4 'KBN_NM2
                colname = "KBN_NM2"
            Case 5 'KBN_NM3
                colname = "KBN_NM3"
            Case 6 'KBN_NM4
                colname = "KBN_NM4"
            Case 7 'KBN_NM5
                colname = "KBN_NM5"
            Case 8 'KBN_NM6
                colname = "KBN_NM6"
            Case 9 'KBN_NM7
                colname = "KBN_NM7"
            Case 10 'KBN_NM8
                colname = "KBN_NM8"
            Case 11 'KBN_NM9
                colname = "KBN_NM9"
            Case 12 'KBN_NM10
                colname = "KBN_NM10"
            Case 13
                colname = "VALUE1"
            Case 14
                colname = "VALUE2"
            Case 15
                colname = "VALUE3"
            Case 16
                colname = "SORT"
            Case 17
                colname = "REM"
        End Select

        col.ColumnName = colname
        col.Caption = colname

        Return col
    End Function


    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <remarks></remarks>
    Friend Function GetSchemaEDI(ByVal brCd As String) As String

        Dim rtnSchema As String = String.Empty
        Dim dataRows() As DataRow = _kbnDs.Tables("Z_KBN").Select("KBN_NM3 = '" & brCd & "'")
        Dim serverAcFlg As String = dataRows(0).Item("KBN_NM4").ToString

        Select Case serverAcFlg
            Case "00"
                rtnSchema = dataRows(0).Item("KBN_NM8").ToString
            Case "01"
                rtnSchema = dataRows(0).Item("KBN_NM6").ToString

        End Select

        Return rtnSchema

    End Function

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <remarks></remarks>
    Friend Function GetSchemaEDIMst(ByVal brCd As String) As String

        Dim rtnSchema As String = String.Empty
        Dim dataRows() As DataRow = _kbnDs.Tables("Z_KBN").Select("KBN_NM3 = '" & brCd & "'")
        Dim serverAcFlg As String = dataRows(0).Item("KBN_NM4").ToString

        Select Case serverAcFlg
            Case "00"
                rtnSchema = dataRows(0).Item("KBN_NM8").ToString
            Case "01"
                rtnSchema = Me._MstSchemaNm.Replace("..", "")

        End Select

        Return rtnSchema

    End Function

    ''' <summary>
    ''' LMSVer1の接続文字列取得
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetConnectionLMS1(ByVal brCd As String) As String

        Dim rtnSchema As String = String.Empty
        Dim dataRows() As DataRow = _kbnDs.Tables("Z_KBN").Select("KBN_NM3 = '" & brCd & "'")

        Dim DBName As String = String.Empty
        Dim loginSchemaNM As String = String.Empty
        Dim userId As String = "sa"
        Dim pass As String = "as"

        DBName = dataRows(0).Item("KBN_NM7").ToString
        loginSchemaNM = dataRows(0).Item("KBN_NM8").ToString

        Return String.Concat("Data Source=", DBName, ";Initial Catalog=", loginSchemaNM, ";Persist Security Info=True;User ID=", userId, ";Password=", pass)

    End Function


    ''' <summary>
    ''' 区分マスタの接続情報を取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetConnectDataSet(ByVal ds As DataSet)

        'SQL格納変数の初期化
        Me._strSql = New StringBuilder()

        'SQL作成
        Call Me.SQLGetConnection()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._strSql.ToString())

        MyBase.Logger.WriteSQLLog("LMNControlDAC", "SQLGetConnection", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("KBN_GROUP_CD", "KBN_GROUP_CD")
        map.Add("KBN_CD", "KBN_CD")
        map.Add("KBN_KEYWORD", "KBN_KEYWORD")
        map.Add("KBN_NM1", "KBN_NM1")
        map.Add("KBN_NM2", "KBN_NM2")
        map.Add("KBN_NM3", "KBN_NM3")
        map.Add("KBN_NM4", "KBN_NM4")
        map.Add("KBN_NM5", "KBN_NM5")
        map.Add("KBN_NM6", "KBN_NM6")
        map.Add("KBN_NM7", "KBN_NM7")
        map.Add("KBN_NM8", "KBN_NM8")
        map.Add("KBN_NM9", "KBN_NM9")
        map.Add("KBN_NM10", "KBN_NM10")
        map.Add("VALUE1", "VALUE1")
        map.Add("VALUE2", "VALUE2")
        map.Add("VALUE3", "VALUE3")
        map.Add("SORT", "SORT")
        map.Add("REM", "REM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "Z_KBN")


    End Sub

    ''' <summary>
    '''区分マスタ情報取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SQLGetConnection()

        Me._strSql.Append("SELECT       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("  KBN_GROUP_CD	AS	KBN_GROUP_CD")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_CD		    AS	KBN_CD")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_KEYWORD	AS	KBN_KEYWORD")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM1		AS	KBN_NM1")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM2		AS	KBN_NM2")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM3		AS	KBN_NM3")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM4		AS	KBN_NM4")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM5		AS	KBN_NM5")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM6		AS	KBN_NM6")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM7		AS	KBN_NM7")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM8		AS	KBN_NM8")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM9		AS	KBN_NM9")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,KBN_NM10		AS	KBN_NM10")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,VALUE1		    AS	VALUE1")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,VALUE2	       	AS	VALUE2")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,VALUE3	    	AS	VALUE3")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,SORT	    	AS	SORT")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" ,REM	    	AS	REM")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("FROM       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(Me._MstSchemaNm)
        Me._strSql.Append("Z_KBN KBN       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append("WHERE       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" KBN.SYS_DEL_FLG = '0'       ")
        Me._strSql.Append(vbNewLine)
        Me._strSql.Append(" AND KBN.KBN_GROUP_CD ='L001'       ")


    End Sub


#End Region

End Class
