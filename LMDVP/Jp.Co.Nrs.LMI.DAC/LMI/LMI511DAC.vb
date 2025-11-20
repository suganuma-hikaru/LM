'  ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI511DAC : JNC EDI
'  作  成  者       :  
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI511DAC
''' </summary>
''' <remarks></remarks>
Public Class LMI511DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Class TABLE_NM
        Public Const IN_BO_MST As String = "LMI511IN_BO_MST"
        Public Const OUT_BO_MST As String = "LMI511OUT_BO_MST"
        Public Const INOUT_OLD_DATA_CHK As String = "LMI511INOUT_OLD_DATA_CHK"
        Public Const INOUT_GET_AUTO_CD As String = "LMI511INOUT_GET_AUTO_CD"
        Public Const IN_SEARCH As String = "LMI511IN_SEARCH"
        Public Const OUT As String = "LMI511OUT"
        Public Const IN_HED As String = "LMI511IN_HED"
        Public Const OUT_HED As String = "LMI511OUT_HED"
        Public Const IN_DTL As String = "LMI511IN_DTL"
        Public Const OUT_DTL As String = "LMI511OUT_DTL"
        Public Const OUT_HED_EDI As String = "LMI511OUT_HED_EDI"
        Public Const OUT_DTL_EDI As String = "LMI511OUT_DTL_EDI"
        Public Const IN_EDI_L As String = "LMI511IN_H_OUTKAEDI_L"
        Public Const OUT_EDI_L As String = "LMI511OUT_H_OUTKAEDI_L"
        Public Const IN_EDI_M As String = "LMI511IN_H_OUTKAEDI_M"
        Public Const IN_IDX As String = "LMI511IN_IDX"
    End Class

    ''' <summary>
    ''' 入出庫区分
    ''' </summary>
    ''' <remarks></remarks>
    Private Class INOUT_KB
        ''' <summary>
        ''' 出庫
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTKA As String = "0"
        ''' <summary>
        ''' 入庫
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INKA As String = "1"
    End Class

    ''' <summary>
    ''' データ種別
    ''' </summary>
    ''' <remarks></remarks>
    Private Class DATA_KIND
        ''' <summary>
        ''' 出荷
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTKA As String = "4001"
        ''' <summary>
        ''' 入荷
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INKA As String = "4101"
        ''' <summary>
        ''' 運送
        ''' </summary>
        ''' <remarks></remarks>
        Public Const UNSO As String = "3001"

    End Class

    ''' <summary>
    ''' 送信訂正区分
    ''' </summary>
    ''' <remarks></remarks>
    Private Class SND_CANCEL_FLG
        ''' <summary>
        ''' なし
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NORMAL As String = "0"
        ''' <summary>
        ''' 修赤
        ''' </summary>
        ''' <remarks></remarks>
        Public Const RED As String = "1"
        ''' <summary>
        ''' 修黒
        ''' </summary>
        ''' <remarks></remarks>
        Public Const BLACK As String = "2"
    End Class

    ''' <summary>
    ''' 有無フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private Class UMU_FLG
        ''' <summary>
        ''' 有
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ARI As String = "1"
        ''' <summary>
        ''' 無
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NASI As String = "2"
    End Class

#End Region 'Const

#Region "SQL"

#Region "マスタデータ"

    ''' <summary>
    ''' ＪＮＣ営業所マスタ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_BO_MST As String = "" _
            & " SELECT" & vbNewLine _
            & "   OUTKA_POSI_BU_CD," & vbNewLine _
            & "   OUTKA_POSI_BU_NM," & vbNewLine _
            & "   OUTKA_POSI_BU_NM_RYAK," & vbNewLine _
            & "   OUTKA_POSI_AD_CD," & vbNewLine _
            & "   OUTKA_POSI_AD_NM," & vbNewLine _
            & "   OUTKA_POSI_TEL_NO," & vbNewLine _
            & "   OUTKA_POSI_FAX_NO," & vbNewLine _
            & "   CHISSO_OUTKA_POSI_CD," & vbNewLine _
            & "   CHISSO_OUTKA_POSI_NM," & vbNewLine _
            & "   ST_KBN," & vbNewLine _
            & "   NRS_SV," & vbNewLine _
            & "   NRS_DB," & vbNewLine _
            & "   CUST_CD_L," & vbNewLine _
            & "   CUST_CD_M," & vbNewLine _
            & "   NRS_BO," & vbNewLine _
            & "   NRS_SOKO_CD" & vbNewLine _
            & " FROM" & vbNewLine _
            & "   $LM_MST$..M_BO_MST_JNC" & vbNewLine

#End Region

#Region "チェック"

    ''' <summary>
    ''' 編集中に旧データになっていないかチェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OLD_DATA_CHK As String = "" _
        & " SELECT" & vbNewLine _
        & "   COUNT(*) AS SELECT_CNT" & vbNewLine _
        & " FROM" & vbNewLine _
        & "   $LM_TRN$..H_INOUTKAEDI_HED_JNC" & vbNewLine _
        & " WHERE" & vbNewLine _
        & "   SYS_DEL_FLG = '0'" & vbNewLine _
        & "   AND OLD_DATA_FLG = ''" & vbNewLine _
        & "   AND EDI_CTL_NO = @EDI_CTL_NO" & vbNewLine

#End Region

#Region "出荷登録処理"

    ''' <summary>
    ''' 出荷登録処理：取得：ＥＤＩ出荷データＬ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_OUTKAEDI_L_OUTKA_SAVE As String = "" _
            & " SELECT" & vbNewLine _
            & "   DEL_KB," & vbNewLine _
            & "   NRS_BR_CD," & vbNewLine _
            & "   EDI_CTL_NO," & vbNewLine _
            & "   OUTKA_CTL_NO," & vbNewLine _
            & "   OUTKA_KB," & vbNewLine _
            & "   SYUBETU_KB," & vbNewLine _
            & "   NAIGAI_KB," & vbNewLine _
            & "   OUTKA_STATE_KB," & vbNewLine _
            & "   OUTKAHOKOKU_YN," & vbNewLine _
            & "   PICK_KB," & vbNewLine _
            & "   NRS_BR_NM," & vbNewLine _
            & "   WH_CD," & vbNewLine _
            & "   WH_NM," & vbNewLine _
            & "   OUTKA_PLAN_DATE," & vbNewLine _
            & "   OUTKO_DATE," & vbNewLine _
            & "   ARR_PLAN_DATE," & vbNewLine _
            & "   ARR_PLAN_TIME," & vbNewLine _
            & "   HOKOKU_DATE," & vbNewLine _
            & "   TOUKI_HOKAN_YN," & vbNewLine _
            & "   CUST_CD_L," & vbNewLine _
            & "   CUST_CD_M," & vbNewLine _
            & "   CUST_NM_L," & vbNewLine _
            & "   CUST_NM_M," & vbNewLine _
            & "   SHIP_CD_L," & vbNewLine _
            & "   SHIP_CD_M," & vbNewLine _
            & "   SHIP_NM_L," & vbNewLine _
            & "   SHIP_NM_M," & vbNewLine _
            & "   EDI_DEST_CD," & vbNewLine _
            & "   DEST_CD," & vbNewLine _
            & "   DEST_NM," & vbNewLine _
            & "   DEST_ZIP," & vbNewLine _
            & "   DEST_AD_1," & vbNewLine _
            & "   DEST_AD_2," & vbNewLine _
            & "   DEST_AD_3," & vbNewLine _
            & "   DEST_AD_4," & vbNewLine _
            & "   DEST_AD_5," & vbNewLine _
            & "   DEST_TEL," & vbNewLine _
            & "   DEST_FAX," & vbNewLine _
            & "   DEST_MAIL," & vbNewLine _
            & "   DEST_JIS_CD," & vbNewLine _
            & "   SP_NHS_KB," & vbNewLine _
            & "   COA_YN," & vbNewLine _
            & "   CUST_ORD_NO," & vbNewLine _
            & "   BUYER_ORD_NO," & vbNewLine _
            & "   UNSO_MOTO_KB," & vbNewLine _
            & "   UNSO_TEHAI_KB," & vbNewLine _
            & "   SYARYO_KB," & vbNewLine _
            & "   BIN_KB," & vbNewLine _
            & "   UNSO_CD," & vbNewLine _
            & "   UNSO_NM," & vbNewLine _
            & "   UNSO_BR_CD," & vbNewLine _
            & "   UNSO_BR_NM," & vbNewLine _
            & "   UNCHIN_TARIFF_CD," & vbNewLine _
            & "   EXTC_TARIFF_CD," & vbNewLine _
            & "   REMARK," & vbNewLine _
            & "   UNSO_ATT," & vbNewLine _
            & "   DENP_YN," & vbNewLine _
            & "   PC_KB," & vbNewLine _
            & "   UNCHIN_YN," & vbNewLine _
            & "   NIYAKU_YN," & vbNewLine _
            & "   OUT_FLAG," & vbNewLine _
            & "   AKAKURO_KB," & vbNewLine _
            & "   JISSEKI_FLAG," & vbNewLine _
            & "   JISSEKI_USER," & vbNewLine _
            & "   JISSEKI_DATE," & vbNewLine _
            & "   JISSEKI_TIME," & vbNewLine _
            & "   FREE_N01," & vbNewLine _
            & "   FREE_N02," & vbNewLine _
            & "   FREE_N03," & vbNewLine _
            & "   FREE_N04," & vbNewLine _
            & "   FREE_N05," & vbNewLine _
            & "   FREE_N06," & vbNewLine _
            & "   FREE_N07," & vbNewLine _
            & "   FREE_N08," & vbNewLine _
            & "   FREE_N09," & vbNewLine _
            & "   FREE_N10," & vbNewLine _
            & "   FREE_C01," & vbNewLine _
            & "   FREE_C02," & vbNewLine _
            & "   FREE_C03," & vbNewLine _
            & "   FREE_C04," & vbNewLine _
            & "   FREE_C05," & vbNewLine _
            & "   FREE_C06," & vbNewLine _
            & "   FREE_C07," & vbNewLine _
            & "   FREE_C08," & vbNewLine _
            & "   FREE_C09," & vbNewLine _
            & "   FREE_C10," & vbNewLine _
            & "   FREE_C11," & vbNewLine _
            & "   FREE_C12," & vbNewLine _
            & "   FREE_C13," & vbNewLine _
            & "   FREE_C14," & vbNewLine _
            & "   FREE_C15," & vbNewLine _
            & "   FREE_C16," & vbNewLine _
            & "   FREE_C17," & vbNewLine _
            & "   FREE_C18," & vbNewLine _
            & "   FREE_C19," & vbNewLine _
            & "   FREE_C20," & vbNewLine _
            & "   FREE_C21," & vbNewLine _
            & "   FREE_C22," & vbNewLine _
            & "   FREE_C23," & vbNewLine _
            & "   FREE_C24," & vbNewLine _
            & "   FREE_C25," & vbNewLine _
            & "   FREE_C26," & vbNewLine _
            & "   FREE_C27," & vbNewLine _
            & "   FREE_C28," & vbNewLine _
            & "   FREE_C29," & vbNewLine _
            & "   FREE_C30," & vbNewLine _
            & "   CRT_USER," & vbNewLine _
            & "   CRT_DATE," & vbNewLine _
            & "   CRT_TIME," & vbNewLine _
            & "   UPD_USER," & vbNewLine _
            & "   UPD_DATE," & vbNewLine _
            & "   UPD_TIME," & vbNewLine _
            & "   SCM_CTL_NO_L," & vbNewLine _
            & "   EDIT_FLAG," & vbNewLine _
            & "   MATCHING_FLAG," & vbNewLine _
            & "   SYS_ENT_DATE," & vbNewLine _
            & "   SYS_ENT_TIME," & vbNewLine _
            & "   SYS_ENT_PGID," & vbNewLine _
            & "   SYS_ENT_USER," & vbNewLine _
            & "   SYS_UPD_DATE," & vbNewLine _
            & "   SYS_UPD_TIME," & vbNewLine _
            & "   SYS_UPD_PGID," & vbNewLine _
            & "   SYS_UPD_USER," & vbNewLine _
            & "   SYS_DEL_FLG" & vbNewLine _
            & " FROM" & vbNewLine _
            & "   $LM_TRN$..H_OUTKAEDI_L" & vbNewLine

    ''' <summary>
    ''' 出荷登録処理：取得：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_HED_OUTKA_SAVE As String = "" _
            & " SELECT" & vbNewLine _
            & "   HED1.DEL_KB," & vbNewLine _
            & "   HED1.CRT_DATE," & vbNewLine _
            & "   HED1.FILE_NAME," & vbNewLine _
            & "   HED1.REC_NO," & vbNewLine _
            & "   HED1.NRS_BR_CD," & vbNewLine _
            & "   HED1.INOUT_KB," & vbNewLine _
            & "   HED1.EDI_CTL_NO," & vbNewLine _
            & "   HED1.INKA_CTL_NO_L," & vbNewLine _
            & "   HED1.OUTKA_CTL_NO," & vbNewLine _
            & "   HED1.CUST_CD_L," & vbNewLine _
            & "   HED1.CUST_CD_M," & vbNewLine _
            & "   HED1.PRTFLG," & vbNewLine _
            & "   HED1.PRTFLG_SUB," & vbNewLine _
            & "   HED1.CANCEL_FLG," & vbNewLine _
            & "   HED1.NRS_TANTO," & vbNewLine _
            & "   HED1.DATA_KIND," & vbNewLine _
            & "   HED1.SEND_CODE," & vbNewLine _
            & "   HED1.SR_DEN_NO," & vbNewLine _
            & "   HED1.HIS_NO," & vbNewLine _
            & "   HED1.PROC_YMD," & vbNewLine _
            & "   HED1.PROC_TIME," & vbNewLine _
            & "   HED1.PROC_NO," & vbNewLine _
            & "   HED1.SEND_DEN_YMD," & vbNewLine _
            & "   HED1.SEND_DEN_TIME," & vbNewLine _
            & "   HED1.BPID_KKN," & vbNewLine _
            & "   HED1.BPID_SUB_KKN," & vbNewLine _
            & "   HED1.BPID_HAN," & vbNewLine _
            & "   HED1.RCV_COMP_CD," & vbNewLine _
            & "   HED1.SND_COMP_CD," & vbNewLine _
            & "   HED1.RB_KBN," & vbNewLine _
            & "   HED1.MOD_KBN," & vbNewLine _
            & "   HED1.DATA_KBN," & vbNewLine _
            & "   HED1.FAX_KBN," & vbNewLine _
            & "   HED1.OUTKA_REQ_KBN," & vbNewLine _
            & "   HED1.INKA_P_KBN," & vbNewLine _
            & "   HED1.OUTKA_SEPT_KBN," & vbNewLine _
            & "   HED1.EM_OUTKA_KBN," & vbNewLine _
            & "   HED1.UNSO_ROUTE_P," & vbNewLine _
            & "   HED1.UNSO_ROUTE_A," & vbNewLine _
            & "   HED1.CAR_KIND_P," & vbNewLine _
            & "   HED1.CAR_KIND_A," & vbNewLine _
            & "   HED1.CAR_NO_P," & vbNewLine _
            & "   HED1.CAR_NO_A," & vbNewLine _
            & "   HED1.COMBI_NO_P," & vbNewLine _
            & "   HED1.COMBI_NO_A," & vbNewLine _
            & "   HED1.UNSO_REQ_YN," & vbNewLine _
            & "   HED1.DEST_CHK_KBN," & vbNewLine _
            & "   HED1.INKO_DATE_P," & vbNewLine _
            & "   HED1.INKO_DATE_A," & vbNewLine _
            & "   HED1.INKO_TIME," & vbNewLine _
            & "   HED1.OUTKA_DATE_P," & vbNewLine _
            & "   HED1.OUTKA_DATE_A," & vbNewLine _
            & "   HED1.OUTKA_TIME_E," & vbNewLine _
            & "   HED1.CARGO_BKG_DATE_P," & vbNewLine _
            & "   HED1.CARGO_BKG_DATE_A," & vbNewLine _
            & "   HED1.ARRIVAL_DATE_P," & vbNewLine _
            & "   HED1.ARRIVAL_DATE_A," & vbNewLine _
            & "   HED1.ARRIVAL_TIME," & vbNewLine _
            & "   HED1.UNSO_DATE," & vbNewLine _
            & "   HED1.UNSO_TIME," & vbNewLine _
            & "   HED1.ZAI_RPT_DATE," & vbNewLine _
            & "   HED1.BAILER_CD," & vbNewLine _
            & "   HED1.BAILER_NM," & vbNewLine _
            & "   HED1.BAILER_BU_CD," & vbNewLine _
            & "   HED1.BAILER_BU_NM," & vbNewLine _
            & "   HED1.SHIPPER_CD," & vbNewLine _
            & "   HED1.SHIPPER_NM," & vbNewLine _
            & "   HED1.SHIPPER_BU_CD," & vbNewLine _
            & "   HED1.SHIPPER_BU_NM," & vbNewLine _
            & "   HED1.CONSIGNEE_CD," & vbNewLine _
            & "   HED1.CONSIGNEE_NM," & vbNewLine _
            & "   HED1.CONSIGNEE_BU_CD," & vbNewLine _
            & "   HED1.CONSIGNEE_BU_NM," & vbNewLine _
            & "   HED1.SOKO_PROV_CD," & vbNewLine _
            & "   HED1.SOKO_PROV_NM," & vbNewLine _
            & "   HED1.UNSO_PROV_CD," & vbNewLine _
            & "   HED1.UNSO_PROV_NM," & vbNewLine _
            & "   HED1.ACT_UNSO_CD," & vbNewLine _
            & "   HED1.UNSO_TF_KBN," & vbNewLine _
            & "   HED1.UNSO_F_KBN," & vbNewLine _
            & "   HED1.DEST_CD," & vbNewLine _
            & "   HED1.DEST_NM," & vbNewLine _
            & "   HED1.DEST_BU_CD," & vbNewLine _
            & "   HED1.DEST_BU_NM," & vbNewLine _
            & "   HED1.DEST_AD_CD," & vbNewLine _
            & "   HED1.DEST_AD_NM," & vbNewLine _
            & "   HED1.DEST_YB_NO," & vbNewLine _
            & "   HED1.DEST_TEL_NO," & vbNewLine _
            & "   HED1.DEST_FAX_NO," & vbNewLine _
            & "   HED1.DELIVERY_NM," & vbNewLine _
            & "   HED1.DELIVERY_SAGYO," & vbNewLine _
            & "   HED1.ORDER_NO," & vbNewLine _
            & "   HED1.JYUCHU_NO," & vbNewLine _
            & "   HED1.PRI_SHOP_CD," & vbNewLine _
            & "   HED1.PRI_SHOP_NM," & vbNewLine _
            & "   HED1.INV_REM_NM," & vbNewLine _
            & "   HED1.INV_REM_KANA," & vbNewLine _
            & "   HED1.DEN_NO," & vbNewLine _
            & "   HED1.MEI_DEN_NO," & vbNewLine _
            & "   HED1.OUTKA_POSI_CD," & vbNewLine _
            & "   HED1.OUTKA_POSI_NM," & vbNewLine _
            & "   HED1.OUTKA_POSI_BU_CD_PA," & vbNewLine _
            & "   HED1.OUTKA_POSI_BU_CD_NAIBU," & vbNewLine _
            & "   HED1.OUTKA_POSI_BU_NM_PA," & vbNewLine _
            & "   HED1.OUTKA_POSI_BU_NM_NAIBU," & vbNewLine _
            & "   HED1.OUTKA_POSI_AD_CD_PA," & vbNewLine _
            & "   HED1.OUTKA_POSI_AD_NM_PA," & vbNewLine _
            & "   HED1.OUTKA_POSI_TEL_NO_PA," & vbNewLine _
            & "   HED1.OUTKA_POSI_FAX_NO_PA," & vbNewLine _
            & "   HED1.UNSO_JURYO," & vbNewLine _
            & "   HED1.UNSO_JURYO_FLG," & vbNewLine _
            & "   HED1.UNIT_LOAD_CD," & vbNewLine _
            & "   HED1.UNIT_LOAD_SU," & vbNewLine _
            & "   HED1.REMARK," & vbNewLine _
            & "   HED1.REMARK_KANA," & vbNewLine _
            & "   HED1.HARAI_KBN," & vbNewLine _
            & "   HED1.DATA_TYPE," & vbNewLine _
            & "   HED1.RTN_FLG," & vbNewLine _
            & "   HED1.SND_CANCEL_FLG," & vbNewLine _
            & "   HED1.OLD_DATA_FLG," & vbNewLine _
            & "   HED1.PRINT_NO," & vbNewLine _
            & "   HED1.NRS_SYS_FLG," & vbNewLine _
            & "   HED1.OLD_SYS_FLG," & vbNewLine _
            & "   HED1.RTN_FILE_DATE," & vbNewLine _
            & "   HED1.RTN_FILE_TIME," & vbNewLine _
            & "   HED1.RECORD_STATUS," & vbNewLine _
            & "   HED1.DELETE_USER," & vbNewLine _
            & "   HED1.DELETE_DATE," & vbNewLine _
            & "   HED1.DELETE_TIME," & vbNewLine _
            & "   HED1.DELETE_EDI_NO," & vbNewLine _
            & "   HED1.PRT_USER," & vbNewLine _
            & "   HED1.PRT_DATE," & vbNewLine _
            & "   HED1.PRT_TIME," & vbNewLine _
            & "   HED1.EDI_USER," & vbNewLine _
            & "   HED1.EDI_DATE," & vbNewLine _
            & "   HED1.EDI_TIME," & vbNewLine _
            & "   HED1.OUTKA_USER," & vbNewLine _
            & "   HED1.OUTKA_DATE," & vbNewLine _
            & "   HED1.OUTKA_TIME," & vbNewLine _
            & "   HED1.INKA_USER," & vbNewLine _
            & "   HED1.INKA_DATE," & vbNewLine _
            & "   HED1.INKA_TIME," & vbNewLine _
            & "   HED1.UPD_USER," & vbNewLine _
            & "   HED1.UPD_DATE," & vbNewLine _
            & "   HED1.UPD_TIME," & vbNewLine _
            & "   HED1.SYS_ENT_DATE," & vbNewLine _
            & "   HED1.SYS_ENT_TIME," & vbNewLine _
            & "   HED1.SYS_ENT_PGID," & vbNewLine _
            & "   HED1.SYS_ENT_USER," & vbNewLine _
            & "   HED1.SYS_UPD_DATE," & vbNewLine _
            & "   HED1.SYS_UPD_TIME," & vbNewLine _
            & "   HED1.SYS_UPD_PGID," & vbNewLine _
            & "   HED1.SYS_UPD_USER," & vbNewLine _
            & "   HED1.SYS_DEL_FLG," & vbNewLine _
            & "   HED1.PRT_FLG," & vbNewLine _
            & "   HED1.MATOME_FLG," & vbNewLine _
            & "   HED2.EDI_CTL_NO AS UNSO_EDI_CTL_NO," & vbNewLine _
            & "   BO.NRS_SOKO_CD," & vbNewLine _
            & "   CUST.SP_UNSO_CD," & vbNewLine _
            & "   CUST.SP_UNSO_BR_CD," & vbNewLine _
            & "   UNSOCO.UNSOCO_NM," & vbNewLine _
            & "   UNSOCO.UNSOCO_BR_NM " & vbNewLine _
            & " FROM" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_HED_JNC HED1" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_HED_JNC HED2" & vbNewLine _
            & "   ON   HED2.DATA_KIND = '3001'" & vbNewLine _
            & "   AND  HED2.NRS_BR_CD = HED1.NRS_BR_CD" & vbNewLine _
            & "   AND  HED2.SR_DEN_NO = HED1.SR_DEN_NO" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..M_BO_MST_JNC BO" & vbNewLine _
            & "   ON   BO.OUTKA_POSI_BU_CD = HED1.OUTKA_POSI_BU_CD_PA" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..M_CUST CUST" & vbNewLine _
            & "   ON   CUST.NRS_BR_CD  = HED1.NRS_BR_CD" & vbNewLine _
            & "   AND  CUST.CUST_CD_L  = HED1.CUST_CD_L" & vbNewLine _
            & "   AND  CUST.CUST_CD_M  = HED1.CUST_CD_M" & vbNewLine _
            & "   AND  CUST.CUST_CD_S  = '00' " & vbNewLine _
            & "   AND  CUST.CUST_CD_SS = '00' " & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..M_UNSOCO UNSOCO" & vbNewLine _
            & "   ON   UNSOCO.NRS_BR_CD    = CUST.NRS_BR_CD" & vbNewLine _
            & "   AND  UNSOCO.UNSOCO_CD    = CUST.SP_UNSO_CD" & vbNewLine _
            & "   AND  UNSOCO.UNSOCO_BR_CD = CUST.SP_UNSO_BR_CD" & vbNewLine _
            & " WHERE" & vbNewLine _
            & "   HED1.NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
            & "   AND HED1.EDI_CTL_NO = @EDI_CTL_NO" & vbNewLine _
            & "   AND HED1.DATA_KIND = '4001'" & vbNewLine

    ''' <summary>
    ''' 出荷登録処理：取得：ＪＮＣＥＤＩ受信データ(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DTL_OUTKA_SAVE As String = "" _
            & " SELECT" & vbNewLine _
            & "   DTL.DEL_KB," & vbNewLine _
            & "   DTL.CRT_DATE," & vbNewLine _
            & "   DTL.FILE_NAME," & vbNewLine _
            & "   DTL.REC_NO," & vbNewLine _
            & "   DTL.GYO," & vbNewLine _
            & "   DTL.NRS_BR_CD," & vbNewLine _
            & "   DTL.INOUT_KB," & vbNewLine _
            & "   DTL.EDI_CTL_NO," & vbNewLine _
            & "   DTL.EDI_CTL_NO_CHU," & vbNewLine _
            & "   DTL.INKA_CTL_NO_L," & vbNewLine _
            & "   DTL.INKA_CTL_NO_M," & vbNewLine _
            & "   DTL.OUTKA_CTL_NO," & vbNewLine _
            & "   DTL.OUTKA_CTL_NO_CHU," & vbNewLine _
            & "   DTL.CUST_CD_L," & vbNewLine _
            & "   DTL.CUST_CD_M," & vbNewLine _
            & "   DTL.SR_DEN_NO," & vbNewLine _
            & "   DTL.HIS_NO," & vbNewLine _
            & "   DTL.MEI_NO_P," & vbNewLine _
            & "   DTL.MEI_NO_A," & vbNewLine _
            & "   DTL.JYUCHU_GOODS_CD," & vbNewLine _
            & "   DTL.GOODS_NM," & vbNewLine _
            & "   DTL.GOODS_KANA1," & vbNewLine _
            & "   DTL.GOODS_KANA2," & vbNewLine _
            & "   DTL.NISUGATA_CD," & vbNewLine _
            & "   DTL.IRISUU," & vbNewLine _
            & "   DTL.LOT_NO_P," & vbNewLine _
            & "   DTL.LOT_NO_A," & vbNewLine _
            & "   DTL.SURY_TANI_CD," & vbNewLine _
            & "   DTL.SURY_REQ," & vbNewLine _
            & "   DTL.SURY_RPT," & vbNewLine _
            & "   DTL.MEI_REM1," & vbNewLine _
            & "   DTL.MEI_REM2," & vbNewLine _
            & "   DTL.RECORD_STATUS," & vbNewLine _
            & "   DTL.JISSEKI_SHORI_FLG," & vbNewLine _
            & "   DTL.JISSEKI_USER," & vbNewLine _
            & "   DTL.JISSEKI_DATE," & vbNewLine _
            & "   DTL.JISSEKI_TIME," & vbNewLine _
            & "   DTL.SEND_USER," & vbNewLine _
            & "   DTL.SEND_DATE," & vbNewLine _
            & "   DTL.SEND_TIME," & vbNewLine _
            & "   DTL.DELETE_USER," & vbNewLine _
            & "   DTL.DELETE_DATE," & vbNewLine _
            & "   DTL.DELETE_TIME," & vbNewLine _
            & "   DTL.DELETE_EDI_NO," & vbNewLine _
            & "   DTL.DELETE_EDI_NO_CHU," & vbNewLine _
            & "   DTL.UPD_USER," & vbNewLine _
            & "   DTL.UPD_DATE," & vbNewLine _
            & "   DTL.UPD_TIME," & vbNewLine _
            & "   DTL.SYS_ENT_DATE," & vbNewLine _
            & "   DTL.SYS_ENT_TIME," & vbNewLine _
            & "   DTL.SYS_ENT_PGID," & vbNewLine _
            & "   DTL.SYS_ENT_USER," & vbNewLine _
            & "   DTL.SYS_UPD_DATE," & vbNewLine _
            & "   DTL.SYS_UPD_TIME," & vbNewLine _
            & "   DTL.SYS_UPD_PGID," & vbNewLine _
            & "   DTL.SYS_UPD_USER," & vbNewLine _
            & "   DTL.SYS_DEL_FLG," & vbNewLine _
            & "   HED.ORDER_NO," & vbNewLine _
            & "   HED.JYUCHU_NO" & vbNewLine _
            & " FROM" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_HED_JNC HED" & vbNewLine _
            & " INNER JOIN" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_DTL_JNC DTL" & vbNewLine _
            & "   ON   DTL.NRS_BR_CD = HED.NRS_BR_CD" & vbNewLine _
            & "   AND  DTL.EDI_CTL_NO = HED.EDI_CTL_NO" & vbNewLine _
            & "   AND  DTL.INOUT_KB = HED.INOUT_KB" & vbNewLine

    ''' <summary>
    ''' 出荷登録処理：登録：ＥＤＩ出荷データＬ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_OUTKAEDI_L_OUTKA_SAVE As String = "" _
            & " INSERT INTO $LM_TRN$..H_OUTKAEDI_L (" & vbNewLine _
            & "   DEL_KB," & vbNewLine _
            & "   NRS_BR_CD," & vbNewLine _
            & "   EDI_CTL_NO," & vbNewLine _
            & "   OUTKA_CTL_NO," & vbNewLine _
            & "   OUTKA_KB," & vbNewLine _
            & "   SYUBETU_KB," & vbNewLine _
            & "   NAIGAI_KB," & vbNewLine _
            & "   OUTKA_STATE_KB," & vbNewLine _
            & "   OUTKAHOKOKU_YN," & vbNewLine _
            & "   PICK_KB," & vbNewLine _
            & "   NRS_BR_NM," & vbNewLine _
            & "   WH_CD," & vbNewLine _
            & "   WH_NM," & vbNewLine _
            & "   OUTKA_PLAN_DATE," & vbNewLine _
            & "   OUTKO_DATE," & vbNewLine _
            & "   ARR_PLAN_DATE," & vbNewLine _
            & "   ARR_PLAN_TIME," & vbNewLine _
            & "   HOKOKU_DATE," & vbNewLine _
            & "   TOUKI_HOKAN_YN," & vbNewLine _
            & "   CUST_CD_L," & vbNewLine _
            & "   CUST_CD_M," & vbNewLine _
            & "   CUST_NM_L," & vbNewLine _
            & "   CUST_NM_M," & vbNewLine _
            & "   SHIP_CD_L," & vbNewLine _
            & "   SHIP_CD_M," & vbNewLine _
            & "   SHIP_NM_L," & vbNewLine _
            & "   SHIP_NM_M," & vbNewLine _
            & "   EDI_DEST_CD," & vbNewLine _
            & "   DEST_CD," & vbNewLine _
            & "   DEST_NM," & vbNewLine _
            & "   DEST_ZIP," & vbNewLine _
            & "   DEST_AD_1," & vbNewLine _
            & "   DEST_AD_2," & vbNewLine _
            & "   DEST_AD_3," & vbNewLine _
            & "   DEST_AD_4," & vbNewLine _
            & "   DEST_AD_5," & vbNewLine _
            & "   DEST_TEL," & vbNewLine _
            & "   DEST_FAX," & vbNewLine _
            & "   DEST_MAIL," & vbNewLine _
            & "   DEST_JIS_CD," & vbNewLine _
            & "   SP_NHS_KB," & vbNewLine _
            & "   COA_YN," & vbNewLine _
            & "   CUST_ORD_NO," & vbNewLine _
            & "   BUYER_ORD_NO," & vbNewLine _
            & "   UNSO_MOTO_KB," & vbNewLine _
            & "   UNSO_TEHAI_KB," & vbNewLine _
            & "   SYARYO_KB," & vbNewLine _
            & "   BIN_KB," & vbNewLine _
            & "   UNSO_CD," & vbNewLine _
            & "   UNSO_NM," & vbNewLine _
            & "   UNSO_BR_CD," & vbNewLine _
            & "   UNSO_BR_NM," & vbNewLine _
            & "   UNCHIN_TARIFF_CD," & vbNewLine _
            & "   EXTC_TARIFF_CD," & vbNewLine _
            & "   REMARK," & vbNewLine _
            & "   UNSO_ATT," & vbNewLine _
            & "   DENP_YN," & vbNewLine _
            & "   PC_KB," & vbNewLine _
            & "   UNCHIN_YN," & vbNewLine _
            & "   NIYAKU_YN," & vbNewLine _
            & "   OUT_FLAG," & vbNewLine _
            & "   AKAKURO_KB," & vbNewLine _
            & "   JISSEKI_FLAG," & vbNewLine _
            & "   JISSEKI_USER," & vbNewLine _
            & "   JISSEKI_DATE," & vbNewLine _
            & "   JISSEKI_TIME," & vbNewLine _
            & "   FREE_N01," & vbNewLine _
            & "   FREE_N02," & vbNewLine _
            & "   FREE_N03," & vbNewLine _
            & "   FREE_N04," & vbNewLine _
            & "   FREE_N05," & vbNewLine _
            & "   FREE_N06," & vbNewLine _
            & "   FREE_N07," & vbNewLine _
            & "   FREE_N08," & vbNewLine _
            & "   FREE_N09," & vbNewLine _
            & "   FREE_N10," & vbNewLine _
            & "   FREE_C01," & vbNewLine _
            & "   FREE_C02," & vbNewLine _
            & "   FREE_C03," & vbNewLine _
            & "   FREE_C04," & vbNewLine _
            & "   FREE_C05," & vbNewLine _
            & "   FREE_C06," & vbNewLine _
            & "   FREE_C07," & vbNewLine _
            & "   FREE_C08," & vbNewLine _
            & "   FREE_C09," & vbNewLine _
            & "   FREE_C10," & vbNewLine _
            & "   FREE_C11," & vbNewLine _
            & "   FREE_C12," & vbNewLine _
            & "   FREE_C13," & vbNewLine _
            & "   FREE_C14," & vbNewLine _
            & "   FREE_C15," & vbNewLine _
            & "   FREE_C16," & vbNewLine _
            & "   FREE_C17," & vbNewLine _
            & "   FREE_C18," & vbNewLine _
            & "   FREE_C19," & vbNewLine _
            & "   FREE_C20," & vbNewLine _
            & "   FREE_C21," & vbNewLine _
            & "   FREE_C22," & vbNewLine _
            & "   FREE_C23," & vbNewLine _
            & "   FREE_C24," & vbNewLine _
            & "   FREE_C25," & vbNewLine _
            & "   FREE_C26," & vbNewLine _
            & "   FREE_C27," & vbNewLine _
            & "   FREE_C28," & vbNewLine _
            & "   FREE_C29," & vbNewLine _
            & "   FREE_C30," & vbNewLine _
            & "   CRT_USER," & vbNewLine _
            & "   CRT_DATE," & vbNewLine _
            & "   CRT_TIME," & vbNewLine _
            & "   UPD_USER," & vbNewLine _
            & "   UPD_DATE," & vbNewLine _
            & "   UPD_TIME," & vbNewLine _
            & "   SCM_CTL_NO_L," & vbNewLine _
            & "   EDIT_FLAG," & vbNewLine _
            & "   MATCHING_FLAG," & vbNewLine _
            & "   SYS_ENT_DATE," & vbNewLine _
            & "   SYS_ENT_TIME," & vbNewLine _
            & "   SYS_ENT_PGID," & vbNewLine _
            & "   SYS_ENT_USER," & vbNewLine _
            & "   SYS_UPD_DATE," & vbNewLine _
            & "   SYS_UPD_TIME," & vbNewLine _
            & "   SYS_UPD_PGID," & vbNewLine _
            & "   SYS_UPD_USER," & vbNewLine _
            & "   SYS_DEL_FLG" & vbNewLine _
            & " ) VALUES (" & vbNewLine _
            & "   @DEL_KB," & vbNewLine _
            & "   @NRS_BR_CD," & vbNewLine _
            & "   @EDI_CTL_NO," & vbNewLine _
            & "   @OUTKA_CTL_NO," & vbNewLine _
            & "   @OUTKA_KB," & vbNewLine _
            & "   @SYUBETU_KB," & vbNewLine _
            & "   @NAIGAI_KB," & vbNewLine _
            & "   @OUTKA_STATE_KB," & vbNewLine _
            & "   @OUTKAHOKOKU_YN," & vbNewLine _
            & "   @PICK_KB," & vbNewLine _
            & "   @NRS_BR_NM," & vbNewLine _
            & "   @WH_CD," & vbNewLine _
            & "   @WH_NM," & vbNewLine _
            & "   @OUTKA_PLAN_DATE," & vbNewLine _
            & "   @OUTKO_DATE," & vbNewLine _
            & "   @ARR_PLAN_DATE," & vbNewLine _
            & "   @ARR_PLAN_TIME," & vbNewLine _
            & "   @HOKOKU_DATE," & vbNewLine _
            & "   @TOUKI_HOKAN_YN," & vbNewLine _
            & "   @CUST_CD_L," & vbNewLine _
            & "   @CUST_CD_M," & vbNewLine _
            & "   @CUST_NM_L," & vbNewLine _
            & "   @CUST_NM_M," & vbNewLine _
            & "   @SHIP_CD_L," & vbNewLine _
            & "   @SHIP_CD_M," & vbNewLine _
            & "   @SHIP_NM_L," & vbNewLine _
            & "   @SHIP_NM_M," & vbNewLine _
            & "   @EDI_DEST_CD," & vbNewLine _
            & "   @DEST_CD," & vbNewLine _
            & "   @DEST_NM," & vbNewLine _
            & "   @DEST_ZIP," & vbNewLine _
            & "   @DEST_AD_1," & vbNewLine _
            & "   @DEST_AD_2," & vbNewLine _
            & "   @DEST_AD_3," & vbNewLine _
            & "   @DEST_AD_4," & vbNewLine _
            & "   @DEST_AD_5," & vbNewLine _
            & "   @DEST_TEL," & vbNewLine _
            & "   @DEST_FAX," & vbNewLine _
            & "   @DEST_MAIL," & vbNewLine _
            & "   @DEST_JIS_CD," & vbNewLine _
            & "   @SP_NHS_KB," & vbNewLine _
            & "   @COA_YN," & vbNewLine _
            & "   @CUST_ORD_NO," & vbNewLine _
            & "   @BUYER_ORD_NO," & vbNewLine _
            & "   @UNSO_MOTO_KB," & vbNewLine _
            & "   @UNSO_TEHAI_KB," & vbNewLine _
            & "   @SYARYO_KB," & vbNewLine _
            & "   @BIN_KB," & vbNewLine _
            & "   @UNSO_CD," & vbNewLine _
            & "   @UNSO_NM," & vbNewLine _
            & "   @UNSO_BR_CD," & vbNewLine _
            & "   @UNSO_BR_NM," & vbNewLine _
            & "   @UNCHIN_TARIFF_CD," & vbNewLine _
            & "   @EXTC_TARIFF_CD," & vbNewLine _
            & "   @REMARK," & vbNewLine _
            & "   @UNSO_ATT," & vbNewLine _
            & "   @DENP_YN," & vbNewLine _
            & "   @PC_KB," & vbNewLine _
            & "   @UNCHIN_YN," & vbNewLine _
            & "   @NIYAKU_YN," & vbNewLine _
            & "   @OUT_FLAG," & vbNewLine _
            & "   @AKAKURO_KB," & vbNewLine _
            & "   @JISSEKI_FLAG," & vbNewLine _
            & "   @JISSEKI_USER," & vbNewLine _
            & "   @JISSEKI_DATE," & vbNewLine _
            & "   @JISSEKI_TIME," & vbNewLine _
            & "   @FREE_N01," & vbNewLine _
            & "   @FREE_N02," & vbNewLine _
            & "   @FREE_N03," & vbNewLine _
            & "   @FREE_N04," & vbNewLine _
            & "   @FREE_N05," & vbNewLine _
            & "   @FREE_N06," & vbNewLine _
            & "   @FREE_N07," & vbNewLine _
            & "   @FREE_N08," & vbNewLine _
            & "   @FREE_N09," & vbNewLine _
            & "   @FREE_N10," & vbNewLine _
            & "   @FREE_C01," & vbNewLine _
            & "   @FREE_C02," & vbNewLine _
            & "   @FREE_C03," & vbNewLine _
            & "   @FREE_C04," & vbNewLine _
            & "   @FREE_C05," & vbNewLine _
            & "   @FREE_C06," & vbNewLine _
            & "   @FREE_C07," & vbNewLine _
            & "   @FREE_C08," & vbNewLine _
            & "   @FREE_C09," & vbNewLine _
            & "   @FREE_C10," & vbNewLine _
            & "   @FREE_C11," & vbNewLine _
            & "   @FREE_C12," & vbNewLine _
            & "   @FREE_C13," & vbNewLine _
            & "   @FREE_C14," & vbNewLine _
            & "   @FREE_C15," & vbNewLine _
            & "   @FREE_C16," & vbNewLine _
            & "   @FREE_C17," & vbNewLine _
            & "   @FREE_C18," & vbNewLine _
            & "   @FREE_C19," & vbNewLine _
            & "   @FREE_C20," & vbNewLine _
            & "   @FREE_C21," & vbNewLine _
            & "   @FREE_C22," & vbNewLine _
            & "   @FREE_C23," & vbNewLine _
            & "   @FREE_C24," & vbNewLine _
            & "   @FREE_C25," & vbNewLine _
            & "   @FREE_C26," & vbNewLine _
            & "   @FREE_C27," & vbNewLine _
            & "   @FREE_C28," & vbNewLine _
            & "   @FREE_C29," & vbNewLine _
            & "   @FREE_C30," & vbNewLine _
            & "   @CRT_USER," & vbNewLine _
            & "   @CRT_DATE," & vbNewLine _
            & "   @CRT_TIME," & vbNewLine _
            & "   @UPD_USER," & vbNewLine _
            & "   @UPD_DATE," & vbNewLine _
            & "   @UPD_TIME," & vbNewLine _
            & "   @SCM_CTL_NO_L," & vbNewLine _
            & "   @EDIT_FLAG," & vbNewLine _
            & "   @MATCHING_FLAG," & vbNewLine _
            & "   @SYS_ENT_DATE," & vbNewLine _
            & "   @SYS_ENT_TIME," & vbNewLine _
            & "   @SYS_ENT_PGID," & vbNewLine _
            & "   @SYS_ENT_USER," & vbNewLine _
            & "   @SYS_UPD_DATE," & vbNewLine _
            & "   @SYS_UPD_TIME," & vbNewLine _
            & "   @SYS_UPD_PGID," & vbNewLine _
            & "   @SYS_UPD_USER," & vbNewLine _
            & "   @SYS_DEL_FLG" & vbNewLine _
            & " )" & vbNewLine

    ''' <summary>
    ''' 出荷登録処理：登録：ＥＤＩ出荷データＭ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_OUTKAEDI_M_OUTKA_SAVE As String = "" _
            & " INSERT INTO $LM_TRN$..H_OUTKAEDI_M (" & vbNewLine _
            & "   DEL_KB," & vbNewLine _
            & "   NRS_BR_CD," & vbNewLine _
            & "   EDI_CTL_NO," & vbNewLine _
            & "   EDI_CTL_NO_CHU," & vbNewLine _
            & "   OUTKA_CTL_NO," & vbNewLine _
            & "   OUTKA_CTL_NO_CHU," & vbNewLine _
            & "   COA_YN," & vbNewLine _
            & "   CUST_ORD_NO_DTL," & vbNewLine _
            & "   BUYER_ORD_NO_DTL," & vbNewLine _
            & "   CUST_GOODS_CD," & vbNewLine _
            & "   NRS_GOODS_CD," & vbNewLine _
            & "   GOODS_NM," & vbNewLine _
            & "   RSV_NO," & vbNewLine _
            & "   LOT_NO," & vbNewLine _
            & "   SERIAL_NO," & vbNewLine _
            & "   ALCTD_KB," & vbNewLine _
            & "   OUTKA_PKG_NB," & vbNewLine _
            & "   OUTKA_HASU," & vbNewLine _
            & "   OUTKA_QT," & vbNewLine _
            & "   OUTKA_TTL_NB," & vbNewLine _
            & "   OUTKA_TTL_QT," & vbNewLine _
            & "   KB_UT," & vbNewLine _
            & "   QT_UT," & vbNewLine _
            & "   PKG_NB," & vbNewLine _
            & "   PKG_UT," & vbNewLine _
            & "   ONDO_KB," & vbNewLine _
            & "   UNSO_ONDO_KB," & vbNewLine _
            & "   IRIME," & vbNewLine _
            & "   IRIME_UT," & vbNewLine _
            & "   BETU_WT," & vbNewLine _
            & "   REMARK," & vbNewLine _
            & "   OUT_KB," & vbNewLine _
            & "   AKAKURO_KB," & vbNewLine _
            & "   JISSEKI_FLAG," & vbNewLine _
            & "   JISSEKI_USER," & vbNewLine _
            & "   JISSEKI_DATE," & vbNewLine _
            & "   JISSEKI_TIME," & vbNewLine _
            & "   SET_KB," & vbNewLine _
            & "   FREE_N01," & vbNewLine _
            & "   FREE_N02," & vbNewLine _
            & "   FREE_N03," & vbNewLine _
            & "   FREE_N04," & vbNewLine _
            & "   FREE_N05," & vbNewLine _
            & "   FREE_N06," & vbNewLine _
            & "   FREE_N07," & vbNewLine _
            & "   FREE_N08," & vbNewLine _
            & "   FREE_N09," & vbNewLine _
            & "   FREE_N10," & vbNewLine _
            & "   FREE_C01," & vbNewLine _
            & "   FREE_C02," & vbNewLine _
            & "   FREE_C03," & vbNewLine _
            & "   FREE_C04," & vbNewLine _
            & "   FREE_C05," & vbNewLine _
            & "   FREE_C06," & vbNewLine _
            & "   FREE_C07," & vbNewLine _
            & "   FREE_C08," & vbNewLine _
            & "   FREE_C09," & vbNewLine _
            & "   FREE_C10," & vbNewLine _
            & "   FREE_C11," & vbNewLine _
            & "   FREE_C12," & vbNewLine _
            & "   FREE_C13," & vbNewLine _
            & "   FREE_C14," & vbNewLine _
            & "   FREE_C15," & vbNewLine _
            & "   FREE_C16," & vbNewLine _
            & "   FREE_C17," & vbNewLine _
            & "   FREE_C18," & vbNewLine _
            & "   FREE_C19," & vbNewLine _
            & "   FREE_C20," & vbNewLine _
            & "   FREE_C21," & vbNewLine _
            & "   FREE_C22," & vbNewLine _
            & "   FREE_C23," & vbNewLine _
            & "   FREE_C24," & vbNewLine _
            & "   FREE_C25," & vbNewLine _
            & "   FREE_C26," & vbNewLine _
            & "   FREE_C27," & vbNewLine _
            & "   FREE_C28," & vbNewLine _
            & "   FREE_C29," & vbNewLine _
            & "   FREE_C30," & vbNewLine _
            & "   CRT_USER," & vbNewLine _
            & "   CRT_DATE," & vbNewLine _
            & "   CRT_TIME," & vbNewLine _
            & "   UPD_USER," & vbNewLine _
            & "   UPD_DATE," & vbNewLine _
            & "   UPD_TIME," & vbNewLine _
            & "   SCM_CTL_NO_L," & vbNewLine _
            & "   SCM_CTL_NO_M," & vbNewLine _
            & "   SYS_ENT_DATE," & vbNewLine _
            & "   SYS_ENT_TIME," & vbNewLine _
            & "   SYS_ENT_PGID," & vbNewLine _
            & "   SYS_ENT_USER," & vbNewLine _
            & "   SYS_UPD_DATE," & vbNewLine _
            & "   SYS_UPD_TIME," & vbNewLine _
            & "   SYS_UPD_PGID," & vbNewLine _
            & "   SYS_UPD_USER," & vbNewLine _
            & "   SYS_DEL_FLG" & vbNewLine _
            & " ) VALUES (" & vbNewLine _
            & "   @DEL_KB," & vbNewLine _
            & "   @NRS_BR_CD," & vbNewLine _
            & "   @EDI_CTL_NO," & vbNewLine _
            & "   @EDI_CTL_NO_CHU," & vbNewLine _
            & "   @OUTKA_CTL_NO," & vbNewLine _
            & "   @OUTKA_CTL_NO_CHU," & vbNewLine _
            & "   @COA_YN," & vbNewLine _
            & "   @CUST_ORD_NO_DTL," & vbNewLine _
            & "   @BUYER_ORD_NO_DTL," & vbNewLine _
            & "   @CUST_GOODS_CD," & vbNewLine _
            & "   @NRS_GOODS_CD," & vbNewLine _
            & "   @GOODS_NM," & vbNewLine _
            & "   @RSV_NO," & vbNewLine _
            & "   @LOT_NO," & vbNewLine _
            & "   @SERIAL_NO," & vbNewLine _
            & "   @ALCTD_KB," & vbNewLine _
            & "   @OUTKA_PKG_NB," & vbNewLine _
            & "   @OUTKA_HASU," & vbNewLine _
            & "   @OUTKA_QT," & vbNewLine _
            & "   @OUTKA_TTL_NB," & vbNewLine _
            & "   @OUTKA_TTL_QT," & vbNewLine _
            & "   @KB_UT," & vbNewLine _
            & "   @QT_UT," & vbNewLine _
            & "   @PKG_NB," & vbNewLine _
            & "   @PKG_UT," & vbNewLine _
            & "   @ONDO_KB," & vbNewLine _
            & "   @UNSO_ONDO_KB," & vbNewLine _
            & "   @IRIME," & vbNewLine _
            & "   @IRIME_UT," & vbNewLine _
            & "   @BETU_WT," & vbNewLine _
            & "   @REMARK," & vbNewLine _
            & "   @OUT_KB," & vbNewLine _
            & "   @AKAKURO_KB," & vbNewLine _
            & "   @JISSEKI_FLAG," & vbNewLine _
            & "   @JISSEKI_USER," & vbNewLine _
            & "   @JISSEKI_DATE," & vbNewLine _
            & "   @JISSEKI_TIME," & vbNewLine _
            & "   @SET_KB," & vbNewLine _
            & "   @FREE_N01," & vbNewLine _
            & "   @FREE_N02," & vbNewLine _
            & "   @FREE_N03," & vbNewLine _
            & "   @FREE_N04," & vbNewLine _
            & "   @FREE_N05," & vbNewLine _
            & "   @FREE_N06," & vbNewLine _
            & "   @FREE_N07," & vbNewLine _
            & "   @FREE_N08," & vbNewLine _
            & "   @FREE_N09," & vbNewLine _
            & "   @FREE_N10," & vbNewLine _
            & "   @FREE_C01," & vbNewLine _
            & "   @FREE_C02," & vbNewLine _
            & "   @FREE_C03," & vbNewLine _
            & "   @FREE_C04," & vbNewLine _
            & "   @FREE_C05," & vbNewLine _
            & "   @FREE_C06," & vbNewLine _
            & "   @FREE_C07," & vbNewLine _
            & "   @FREE_C08," & vbNewLine _
            & "   @FREE_C09," & vbNewLine _
            & "   @FREE_C10," & vbNewLine _
            & "   @FREE_C11," & vbNewLine _
            & "   @FREE_C12," & vbNewLine _
            & "   @FREE_C13," & vbNewLine _
            & "   @FREE_C14," & vbNewLine _
            & "   @FREE_C15," & vbNewLine _
            & "   @FREE_C16," & vbNewLine _
            & "   @FREE_C17," & vbNewLine _
            & "   @FREE_C18," & vbNewLine _
            & "   @FREE_C19," & vbNewLine _
            & "   @FREE_C20," & vbNewLine _
            & "   @FREE_C21," & vbNewLine _
            & "   @FREE_C22," & vbNewLine _
            & "   @FREE_C23," & vbNewLine _
            & "   @FREE_C24," & vbNewLine _
            & "   @FREE_C25," & vbNewLine _
            & "   @FREE_C26," & vbNewLine _
            & "   @FREE_C27," & vbNewLine _
            & "   @FREE_C28," & vbNewLine _
            & "   @FREE_C29," & vbNewLine _
            & "   @FREE_C30," & vbNewLine _
            & "   @CRT_USER," & vbNewLine _
            & "   @CRT_DATE," & vbNewLine _
            & "   @CRT_TIME," & vbNewLine _
            & "   @UPD_USER," & vbNewLine _
            & "   @UPD_DATE," & vbNewLine _
            & "   @UPD_TIME," & vbNewLine _
            & "   @SCM_CTL_NO_L," & vbNewLine _
            & "   @SCM_CTL_NO_M," & vbNewLine _
            & "   @SYS_ENT_DATE," & vbNewLine _
            & "   @SYS_ENT_TIME," & vbNewLine _
            & "   @SYS_ENT_PGID," & vbNewLine _
            & "   @SYS_ENT_USER," & vbNewLine _
            & "   @SYS_UPD_DATE," & vbNewLine _
            & "   @SYS_UPD_TIME," & vbNewLine _
            & "   @SYS_UPD_PGID," & vbNewLine _
            & "   @SYS_UPD_USER," & vbNewLine _
            & "   @SYS_DEL_FLG" & vbNewLine _
            & " )" & vbNewLine

    ''' <summary>
    ''' 出荷登録処理：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_HED_OUTKA_SAVE As String = "" _
            & " UPDATE $LM_TRN$..H_INOUTKAEDI_HED_JNC" & vbNewLine _
            & " SET" & vbNewLine _
            & "   NRS_SYS_FLG = @NRS_SYS_FLG," & vbNewLine _
            & "   SYS_UPD_DATE = @SYS_UPD_DATE," & vbNewLine _
            & "   SYS_UPD_TIME = @SYS_UPD_TIME," & vbNewLine _
            & "   SYS_UPD_USER = @SYS_UPD_USER," & vbNewLine _
            & "   SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine

#End Region

#Region "まとめ指示処理"

    ''' <summary>
    ''' まとめ指示処理：排他：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_HED_MTM_SAVE As String = "" _
            & " SELECT" & vbNewLine _
            & "   COUNT(*) AS SELECT_CNT" & vbNewLine _
            & " FROM" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_HED_JNC" & vbNewLine _
            & " WHERE" & vbNewLine _
            & "   NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
            & "   AND EDI_CTL_NO = @EDI_CTL_NO" & vbNewLine _
            & "   AND DATA_KIND = @DATA_KIND" & vbNewLine _
            & "   AND SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
            & "   AND SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine

    ''' <summary>
    ''' まとめ指示処理：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_HED_MTM_SAVE As String = "" _
            & " UPDATE $LM_TRN$..H_INOUTKAEDI_HED_JNC" & vbNewLine _
            & " SET" & vbNewLine _
            & "   COMBI_NO_A = @COMBI_NO_A," & vbNewLine _
            & "   SYS_UPD_DATE = @SYS_UPD_DATE," & vbNewLine _
            & "   SYS_UPD_TIME = @SYS_UPD_TIME," & vbNewLine _
            & "   SYS_UPD_USER = @SYS_UPD_USER," & vbNewLine _
            & "   SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
            & " WHERE" & vbNewLine _
            & "   NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
            & "   AND EDI_CTL_NO = @EDI_CTL_NO" & vbNewLine _
            & "   AND DATA_KIND = @DATA_KIND" & vbNewLine

#End Region

#Region "送信要求処理"

    ''' <summary>
    ''' 送信要求処理：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_HED_SND_REQ As String = "" _
            & " UPDATE $LM_TRN$..H_INOUTKAEDI_HED_JNC" & vbNewLine _
            & " SET" & vbNewLine _
            & "   RTN_FLG = @RTN_FLG," & vbNewLine _
            & "   SYS_UPD_DATE = @SYS_UPD_DATE," & vbNewLine _
            & "   SYS_UPD_TIME = @SYS_UPD_TIME," & vbNewLine _
            & "   SYS_UPD_USER = @SYS_UPD_USER," & vbNewLine _
            & "   SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
            & " WHERE" & vbNewLine _
            & "   NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
            & "   AND SR_DEN_NO = @SR_DEN_NO" & vbNewLine _
            & "   AND DATA_KIND = @DATA_KIND" & vbNewLine _
            & "   AND (MOD_KBN <> 'E' AND MOD_KBN <> 'L' AND MOD_KBN <> '3')" & vbNewLine _
            & "   AND SND_CANCEL_FLG <> '2'" & vbNewLine _
            & "   AND (OLD_DATA_FLG = '' OR OLD_DATA_FLG IS NULL)" & vbNewLine

#If True Then   'ADD 2020/08/31 013087   【LMS】JNC EDI　改修
    ''' <summary>
    ''' 送信要求処理：更新：ＪＮＣＥＤＩ受信データ(ヘッダー運送)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_HED_SND_REQ_UNSO As String = "" _
            & " UPDATE $LM_TRN$..H_INOUTKAEDI_HED_JNC" & vbNewLine _
            & " SET" & vbNewLine _
            & "   RTN_FLG = @RTN_FLG," & vbNewLine _
            & "   SYS_UPD_DATE = @SYS_UPD_DATE," & vbNewLine _
            & "   SYS_UPD_TIME = @SYS_UPD_TIME," & vbNewLine _
            & "   SYS_UPD_USER = @SYS_UPD_USER," & vbNewLine _
            & "   SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
            & " WHERE" & vbNewLine _
            & "   NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
            & "   AND EDI_CTL_NO = @UNSO_EDI_CTL_NO" & vbNewLine _
            & "   AND DATA_KIND = '3001'" & vbNewLine
#End If
#End Region

#Region "まとめ候補検索処理"

    ''' <summary>
    ''' まとめ候補検索処理：取得：SELECT句（件数取得用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_MTMSEARCH As String = "" _
            & " SELECT" & vbNewLine _
            & "   COUNT(*) AS SELECT_CNT" & vbNewLine

    ''' <summary>
    ''' まとめ候補検索処理：取得：SELECT句（データ取得用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_MTMSEARCH As String = "" _
            & " SELECT" & vbNewLine _
            & "   HED1.CRT_DATE," & vbNewLine _
            & "   HED1.FILE_NAME," & vbNewLine _
            & "   HED1.REC_NO," & vbNewLine _
            & "   MEI.GYO," & vbNewLine _
            & "   HED1.NRS_BR_CD," & vbNewLine _
            & "   HED1.INOUT_KB," & vbNewLine _
            & "   HED1.DATA_KIND," & vbNewLine _
            & "   HED1.EDI_CTL_NO," & vbNewLine _
            & "   MEI.EDI_CTL_NO_CHU," & vbNewLine _
            & "   HED1.INKA_CTL_NO_L," & vbNewLine _
            & "   HED1.OUTKA_CTL_NO," & vbNewLine _
            & "   HED1.CUST_CD_L," & vbNewLine _
            & "   HED1.CUST_CD_M," & vbNewLine _
            & "   HED1.RB_KBN," & vbNewLine _
            & "   HED1.MOD_KBN," & vbNewLine _
            & "   HED1.RTN_FLG," & vbNewLine _
            & "   HED1.SND_CANCEL_FLG," & vbNewLine _
            & "   HED1.PRTFLG," & vbNewLine _
            & "   HED1.PRTFLG_SUB," & vbNewLine _
            & "   HED1.NRS_SYS_FLG," & vbNewLine _
            & "   HED1.COMBI_NO_A," & vbNewLine _
            & "   HED1.UNSO_REQ_YN," & vbNewLine _
            & "   HED1.DEST_CD," & vbNewLine _
            & "   HED1.DEST_NM," & vbNewLine _
            & "   HED1.DEST_AD_NM," & vbNewLine _
            & "   HED1.OUTKA_POSI_BU_CD_PA," & vbNewLine _
            & "   HED1.DEST_BU_CD," & vbNewLine _
            & "   HED1.CAR_NO_A," & vbNewLine _
            & "   CASE CONVERT(decimal, MEI.IRISUU) WHEN 0 THEN 0 ELSE MEI.SURY_RPT / MEI.IRISUU END AS TUMI_SU," & vbNewLine _
            & "   HED1.OUTKA_DATE_A," & vbNewLine _
            & "   HED1.ARRIVAL_DATE_A," & vbNewLine _
            & "   HED1.ACT_UNSO_CD," & vbNewLine _
            & "   HED1.UNSO_CD_MEMO," & vbNewLine _
            & "   HED1.UNSO_ROUTE_A," & vbNewLine _
            & "   HED1.INKO_DATE_A," & vbNewLine _
            & "   HED1.PRINT_NO," & vbNewLine _
            & "   HED1.SR_DEN_NO," & vbNewLine _
            & "   HED1.JYUCHU_NO," & vbNewLine _
            & "   HED1.ORDER_NO," & vbNewLine _
            & "   HED1.HIS_NO," & vbNewLine _
            & "   HED1.OLD_DATA_FLG," & vbNewLine _
            & "   HED1.SYS_UPD_DATE AS SYS_UPD_DATE_HED," & vbNewLine _
            & "   HED1.SYS_UPD_TIME AS SYS_UPD_TIME_HED," & vbNewLine _
            & "   MEI.JYUCHU_GOODS_CD," & vbNewLine _
            & "   MEI.GOODS_KANA2," & vbNewLine _
            & "   MEI.SURY_REQ," & vbNewLine _
            & "   MEI.SURY_TANI_CD," & vbNewLine _
            & "   MEI.SURY_RPT," & vbNewLine _
            & "   MEI.SYS_UPD_DATE AS SYS_UPD_DATE_DTL," & vbNewLine _
            & "   MEI.SYS_UPD_TIME AS SYS_UPD_TIME_DTL," & vbNewLine _
            & "   UHD.EDI_CTL_NO AS EDI_CTL_NO_UHD," & vbNewLine _
            & "   UHD.SYS_UPD_DATE AS SYS_UPD_DATE_UHD," & vbNewLine _
            & "   UHD.SYS_UPD_TIME AS SYS_UPD_TIME_UHD," & vbNewLine _
            & "   UDL.EDI_CTL_NO_CHU AS EDI_CTL_NO_CHU_UDL," & vbNewLine _
            & "   UDL.SURY_RPT AS SURY_RPT_UDL," & vbNewLine _
            & "   UDL.SYS_UPD_DATE AS SYS_UPD_DATE_UDL," & vbNewLine _
            & "   UDL.SYS_UPD_TIME AS SYS_UPD_TIME_UDL," & vbNewLine _
            & "   BO1.OUTKA_POSI_BU_NM_RYAK AS OUTKA_POSI_BU_NM," & vbNewLine _
            & "   BO2.OUTKA_POSI_BU_NM_RYAK AS DEST_BU_NM," & vbNewLine _
            & "   KB1.KBN_NM1 AS RB_KBN_NM," & vbNewLine _
            & "   KB2.KBN_NM1 AS MOD_KBN_NM," & vbNewLine _
            & "   KB3.KBN_NM1 AS RTN_FLG_NM," & vbNewLine _
            & "   KB4.KBN_NM1 AS SND_CANCEL_FLG_NM," & vbNewLine _
            & "   KB5.KBN_NM1 AS PRTFLG_NM," & vbNewLine _
            & "   KBB.KBN_NM1 AS PRTFLG_SUB_NM," & vbNewLine _
            & "   KB6.KBN_NM1 AS NRS_SYS_FLG_NM," & vbNewLine _
            & "   KB7.KBN_NM1 AS UNSO_REQ_YN_NM," & vbNewLine _
            & "   KB8.KBN_NM1 AS ACT_UNSO_NM," & vbNewLine _
            & "   KB9.KBN_NM1 AS UNSO_ROUTE_NM," & vbNewLine _
            & "   KBA.KBN_NM1 AS COMBI," & vbNewLine _
            & "   CASE HED1.OUTKA_DATE_A WHEN '' THEN '99999999' ELSE HED1.OUTKA_DATE_A END AS SORT_OUTKA_DATE," & vbNewLine _
            & "   CASE HED1.INKO_DATE_A WHEN '' THEN '99999999' ELSE HED1.INKO_DATE_A END AS SORT_INKO_DATE," & vbNewLine _
            & "   CASE HED1.DEST_BU_CD WHEN '' THEN '9999' ELSE HED1.DEST_BU_CD END AS SORT_DEST_BU_CD" & vbNewLine

    ''' <summary>
    ''' まとめ候補検索処理：取得：FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_MTMSEARCH As String = "" _
            & " FROM (" & vbNewLine _
            & "   SELECT *" & vbNewLine _
            & "   FROM" & vbNewLine _
            & "     $LM_TRN$..H_INOUTKAEDI_HED_JNC" & vbNewLine _
            & "   WHERE" & vbNewLine _
            & "     SYS_DEL_FLG = '0'" & vbNewLine _
            & "     AND DATA_KIND = '4001'" & vbNewLine _
            & "     AND MOD_KBN NOT IN ('3', 'E', 'L')" & vbNewLine _
            & "     AND OLD_DATA_FLG = ''" & vbNewLine _
            & "     AND OUTKA_DATE_A >= @SEARCH_DATE_FROM" & vbNewLine _
            & "     AND OUTKA_DATE_A <= @SEARCH_DATE_TO" & vbNewLine _
            & "     AND PRTFLG = '1'" & vbNewLine _
            & "   ) HED1" & vbNewLine _
            & " INNER JOIN" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_DTL_JNC MEI" & vbNewLine _
            & "   ON   MEI.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  MEI.NRS_BR_CD = HED1.NRS_BR_CD" & vbNewLine _
            & "   AND  MEI.EDI_CTL_NO = HED1.EDI_CTL_NO" & vbNewLine _
            & "   AND  MEI.INOUT_KB = HED1.INOUT_KB" & vbNewLine _
            & " INNER JOIN (" & vbNewLine _
            & "   SELECT" & vbNewLine _
            & "     NRS_BR_CD," & vbNewLine _
            & "     DEST_CD," & vbNewLine _
            & "     COUNT(*) AS CNT" & vbNewLine _
            & "   FROM" & vbNewLine _
            & "     $LM_TRN$..H_INOUTKAEDI_HED_JNC" & vbNewLine _
            & "   WHERE" & vbNewLine _
            & "     SYS_DEL_FLG = '0'" & vbNewLine _
            & "     AND DATA_KIND = '4001'" & vbNewLine _
            & "     AND OUTKA_POSI_BU_CD_PA = @OUTKA_POSI_BU_CD" & vbNewLine _
            & "     AND RTN_FLG <> '2'" & vbNewLine _
            & "     AND PRTFLG = '1'" & vbNewLine _
            & "     AND MOD_KBN NOT IN ('3', 'E', 'L')" & vbNewLine _
            & "     AND OLD_DATA_FLG = ''" & vbNewLine _
            & "     AND OUTKA_DATE_A >= @SEARCH_DATE_FROM" & vbNewLine _
            & "     AND OUTKA_DATE_A <= @SEARCH_DATE_TO" & vbNewLine _
            & "   GROUP BY" & vbNewLine _
            & "     NRS_BR_CD," & vbNewLine _
            & "     DEST_CD" & vbNewLine _
            & "   ) HED2" & vbNewLine _
            & "   ON   HED2.NRS_BR_CD = HED1.NRS_BR_CD" & vbNewLine _
            & "   AND  HED2.DEST_CD = HED1.DEST_CD" & vbNewLine _
            & "   AND  HED2.CNT >= 2" & vbNewLine _
            & " LEFT JOIN (" & vbNewLine _
            & "   SELECT *" & vbNewLine _
            & "   FROM" & vbNewLine _
            & "     $LM_TRN$..H_INOUTKAEDI_HED_JNC" & vbNewLine _
            & "   WHERE" & vbNewLine _
            & "     SYS_DEL_FLG = '0'" & vbNewLine _
            & "     AND DATA_KIND = '3001'" & vbNewLine _
            & "     AND MOD_KBN NOT IN ('3', 'E', 'L')" & vbNewLine _
            & "     AND OLD_DATA_FLG = ''" & vbNewLine _
            & "     AND DEST_CHK_KBN = '1'" & vbNewLine _
            & "   ) UHD" & vbNewLine _
            & "   ON  UHD.NRS_BR_CD = HED1.NRS_BR_CD" & vbNewLine _
            & "   AND UHD.SR_DEN_NO = HED1.SR_DEN_NO" & vbNewLine _
            & "   AND UHD.RB_KBN = HED1.RB_KBN" & vbNewLine _
            & " LEFT JOIN (" & vbNewLine _
            & "   SELECT *" & vbNewLine _
            & "   FROM" & vbNewLine _
            & "     $LM_TRN$..H_INOUTKAEDI_DTL_JNC" & vbNewLine _
            & "   WHERE" & vbNewLine _
            & "     SYS_DEL_FLG = '0'" & vbNewLine _
            & "     AND EDI_CTL_NO_CHU = '001'" & vbNewLine _
            & "   ) UDL" & vbNewLine _
            & "   ON  UDL.NRS_BR_CD = UHD.NRS_BR_CD" & vbNewLine _
            & "   AND UDL.EDI_CTL_NO = UHD.EDI_CTL_NO" & vbNewLine _
            & "   AND UDL.INOUT_KB = UHD.INOUT_KB" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..M_BO_MST_JNC BO1" & vbNewLine _
            & "   ON   BO1.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  BO1.OUTKA_POSI_BU_CD = HED1.OUTKA_POSI_BU_CD_PA" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..M_BO_MST_JNC BO2" & vbNewLine _
            & "   ON   BO2.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  BO2.OUTKA_POSI_BU_CD = HED1.DEST_BU_CD" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB1" & vbNewLine _
            & "   ON   KB1.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB1.KBN_GROUP_CD = 'J022'" & vbNewLine _
            & "   AND  KB1.KBN_CD = HED1.RB_KBN" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB2" & vbNewLine _
            & "   ON   KB2.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB2.KBN_GROUP_CD = 'J018'" & vbNewLine _
            & "   AND  KB2.KBN_CD = HED1.MOD_KBN" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB3" & vbNewLine _
            & "   ON   KB3.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB3.KBN_GROUP_CD = 'J017'" & vbNewLine _
            & "   AND  KB3.KBN_CD = HED1.RTN_FLG" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB4" & vbNewLine _
            & "   ON   KB4.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB4.KBN_GROUP_CD = 'J021'" & vbNewLine _
            & "   AND  KB4.KBN_CD = HED1.SND_CANCEL_FLG" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB5" & vbNewLine _
            & "   ON   KB5.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB5.KBN_GROUP_CD = 'J019'" & vbNewLine _
            & "   AND  KB5.KBN_CD = HED1.PRTFLG" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB6" & vbNewLine _
            & "   ON   KB6.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB6.KBN_GROUP_CD = 'J019'" & vbNewLine _
            & "   AND  KB6.KBN_CD = HED1.NRS_SYS_FLG" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB7" & vbNewLine _
            & "   ON   KB7.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB7.KBN_GROUP_CD = 'J020'" & vbNewLine _
            & "   AND  KB7.KBN_CD = HED1.UNSO_REQ_YN" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB8" & vbNewLine _
            & "   ON   KB8.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB8.KBN_GROUP_CD = 'J016'" & vbNewLine _
            & "   AND  KB8.KBN_NM2 = HED1.ACT_UNSO_CD" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB9" & vbNewLine _
            & "   ON   KB9.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB9.KBN_GROUP_CD = 'J015'" & vbNewLine _
            & "   AND  KB9.KBN_NM2 = HED1.UNSO_ROUTE_A" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KBA" & vbNewLine _
            & "   ON   KBA.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KBA.KBN_GROUP_CD = 'J020'" & vbNewLine _
            & "   AND  KBA.KBN_CD = CASE HED1.COMBI_NO_A WHEN '' THEN '2' ELSE '1' END" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KBB" & vbNewLine _
            & "   ON   KBB.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KBB.KBN_GROUP_CD = 'J019'" & vbNewLine _
            & "   AND  KBB.KBN_CD = HED1.PRTFLG_SUB" & vbNewLine

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 検索処理：取得：SELECT句（件数取得用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_SEARCH As String = "" _
            & " SELECT" & vbNewLine _
            & "   COUNT(*) AS SELECT_CNT" & vbNewLine

    ''' <summary>
    ''' 検索処理：取得：SELECT句（データ取得用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_SEARCH As String = "" _
            & " SELECT" & vbNewLine _
            & "   HED.CRT_DATE," & vbNewLine _
            & "   HED.FILE_NAME," & vbNewLine _
            & "   HED.REC_NO," & vbNewLine _
            & "   MEI.GYO," & vbNewLine _
            & "   HED.NRS_BR_CD," & vbNewLine _
            & "   HED.INOUT_KB," & vbNewLine _
            & "   HED.DATA_KIND," & vbNewLine _
            & "   HED.EDI_CTL_NO," & vbNewLine _
            & "   MEI.EDI_CTL_NO_CHU," & vbNewLine _
            & "   HED.INKA_CTL_NO_L," & vbNewLine _
            & "   HED.OUTKA_CTL_NO," & vbNewLine _
            & "   HED.CUST_CD_L," & vbNewLine _
            & "   HED.CUST_CD_M," & vbNewLine _
            & "   HED.RB_KBN," & vbNewLine _
            & "   HED.MOD_KBN," & vbNewLine _
            & "   HED.RTN_FLG," & vbNewLine _
            & "   HED.SND_CANCEL_FLG," & vbNewLine _
            & "   HED.PRTFLG," & vbNewLine _
            & "   HED.PRTFLG_SUB," & vbNewLine _
            & "   HED.NRS_SYS_FLG," & vbNewLine _
            & "   HED.COMBI_NO_A," & vbNewLine _
            & "   HED.UNSO_REQ_YN," & vbNewLine _
            & "   HED.DEST_CD," & vbNewLine _
            & "   HED.DEST_NM," & vbNewLine _
            & "   HED.DEST_AD_NM," & vbNewLine _
            & "   HED.OUTKA_POSI_BU_CD_PA," & vbNewLine _
            & "   HED.DEST_BU_CD," & vbNewLine _
            & "   HED.CAR_NO_A," & vbNewLine _
            & "   CASE CONVERT(decimal, MEI.IRISUU) WHEN 0 THEN 0 ELSE MEI.SURY_RPT / MEI.IRISUU END AS TUMI_SU," & vbNewLine _
            & "   HED.OUTKA_DATE_A," & vbNewLine _
            & "   HED.ARRIVAL_DATE_A," & vbNewLine _
            & "   HED.ACT_UNSO_CD," & vbNewLine _
            & "   HED.UNSO_CD_MEMO," & vbNewLine _
            & "   HED.UNSO_ROUTE_A," & vbNewLine _
            & "   HED.INKO_DATE_A," & vbNewLine _
            & "   HED.PRINT_NO," & vbNewLine _
            & "   HED.SR_DEN_NO," & vbNewLine _
            & "   HED.JYUCHU_NO," & vbNewLine _
            & "   HED.ORDER_NO," & vbNewLine _
            & "   HED.DELIVERY_NM," & vbNewLine _
            & "   HED.INV_REM_NM," & vbNewLine _
            & "   HED.HIS_NO," & vbNewLine _
            & "   HED.OLD_DATA_FLG," & vbNewLine _
            & "   HED.SYS_UPD_DATE AS SYS_UPD_DATE_HED," & vbNewLine _
            & "   HED.SYS_UPD_TIME AS SYS_UPD_TIME_HED," & vbNewLine _
            & "   MEI.JYUCHU_GOODS_CD," & vbNewLine _
            & "   MEI.GOODS_KANA2," & vbNewLine _
            & "   MEI.SURY_REQ," & vbNewLine _
            & "   MEI.SURY_TANI_CD," & vbNewLine _
            & "   MEI.SURY_RPT," & vbNewLine _
            & "   MEI.SYS_UPD_DATE AS SYS_UPD_DATE_DTL," & vbNewLine _
            & "   MEI.SYS_UPD_TIME AS SYS_UPD_TIME_DTL," & vbNewLine _
            & "   UHD.EDI_CTL_NO AS EDI_CTL_NO_UHD," & vbNewLine _
            & "   UHD.SYS_UPD_DATE AS SYS_UPD_DATE_UHD," & vbNewLine _
            & "   UHD.SYS_UPD_TIME AS SYS_UPD_TIME_UHD," & vbNewLine _
            & "   UDL.EDI_CTL_NO_CHU AS EDI_CTL_NO_CHU_UDL," & vbNewLine _
            & "   UDL.SURY_RPT AS SURY_RPT_UDL," & vbNewLine _
            & "   UDL.SYS_UPD_DATE AS SYS_UPD_DATE_UDL," & vbNewLine _
            & "   UDL.SYS_UPD_TIME AS SYS_UPD_TIME_UDL," & vbNewLine _
            & "   BO1.OUTKA_POSI_BU_NM_RYAK AS OUTKA_POSI_BU_NM," & vbNewLine _
            & "   BO2.OUTKA_POSI_BU_NM_RYAK AS DEST_BU_NM," & vbNewLine _
            & "   KB1.KBN_NM1 AS RB_KBN_NM," & vbNewLine _
            & "   KB2.KBN_NM1 AS MOD_KBN_NM," & vbNewLine _
            & "   KB3.KBN_NM1 AS RTN_FLG_NM," & vbNewLine _
            & "   KB4.KBN_NM1 AS SND_CANCEL_FLG_NM," & vbNewLine _
            & "   KB5.KBN_NM1 AS PRTFLG_NM," & vbNewLine _
            & "   KBC.KBN_NM1 AS PRTFLG_SUB_NM," & vbNewLine _
            & "   KB6.KBN_NM1 AS NRS_SYS_FLG_NM," & vbNewLine _
            & "   KB7.KBN_NM1 AS UNSO_REQ_YN_NM," & vbNewLine _
            & "   KB8.KBN_NM1 AS ACT_UNSO_NM," & vbNewLine _
            & "   KB9.KBN_NM1 AS UNSO_ROUTE_NM," & vbNewLine _
            & "   KBA.KBN_NM1 AS COMBI," & vbNewLine _
            & "   CASE HED.OUTKA_DATE_A WHEN '' THEN '99999999' ELSE HED.OUTKA_DATE_A END AS SORT_OUTKA_DATE," & vbNewLine _
            & "   CASE HED.INKO_DATE_A WHEN '' THEN '99999999' ELSE HED.INKO_DATE_A END AS SORT_INKO_DATE," & vbNewLine _
            & "   CASE HED.DEST_BU_CD WHEN '' THEN '9999' ELSE HED.DEST_BU_CD END AS SORT_DEST_BU_CD" & vbNewLine _
            & "--ADD 2020/08/27 013087   【LMS】JNC EDI　" & vbNewLine _
            & "  ,ISNULL(HEDUNSO.EDI_CTL_NO,'')   AS UNSO_EDI_CTL_NO" & vbNewLine _
            & "  ,ISNULL(HEDUNSO.RTN_FLG,'')      AS UNSO_RTN_FLG" & vbNewLine _
            & "  ,ISNULL(KBB.KBN_NM1,'-')         AS UNSO_RTN_FLG_NM" & vbNewLine



    ''' <summary>
    ''' 検索処理：取得：FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_SEARCH As String = "" _
            & " FROM" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_HED_JNC HED" & vbNewLine _
            & " INNER JOIN" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_DTL_JNC MEI" & vbNewLine _
            & "   ON   MEI.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  MEI.NRS_BR_CD = HED.NRS_BR_CD" & vbNewLine _
            & "   AND  MEI.EDI_CTL_NO = HED.EDI_CTL_NO" & vbNewLine _
            & "   AND  MEI.INOUT_KB = HED.INOUT_KB" & vbNewLine _
            & " LEFT JOIN (" & vbNewLine _
            & "   SELECT *" & vbNewLine _
            & "   FROM" & vbNewLine _
            & "     $LM_TRN$..H_INOUTKAEDI_HED_JNC" & vbNewLine _
            & "   WHERE" & vbNewLine _
            & "     SYS_DEL_FLG = '0'" & vbNewLine _
            & "     AND DATA_KIND = '3001'" & vbNewLine _
            & "     AND MOD_KBN NOT IN ('3', 'E', 'L')" & vbNewLine _
            & "     AND OLD_DATA_FLG = ''" & vbNewLine _
            & "     AND DEST_CHK_KBN = '1'" & vbNewLine _
            & "   ) UHD" & vbNewLine _
            & "   ON  UHD.NRS_BR_CD = HED.NRS_BR_CD" & vbNewLine _
            & "   AND UHD.SR_DEN_NO = HED.SR_DEN_NO" & vbNewLine _
            & "   AND UHD.RB_KBN = HED.RB_KBN" & vbNewLine _
            & " LEFT JOIN (" & vbNewLine _
            & "   SELECT *" & vbNewLine _
            & "   FROM" & vbNewLine _
            & "     $LM_TRN$..H_INOUTKAEDI_DTL_JNC" & vbNewLine _
            & "   WHERE" & vbNewLine _
            & "     SYS_DEL_FLG = '0'" & vbNewLine _
            & "     AND EDI_CTL_NO_CHU = '001'" & vbNewLine _
            & "   ) UDL" & vbNewLine _
            & "   ON  UDL.NRS_BR_CD = UHD.NRS_BR_CD" & vbNewLine _
            & "   AND UDL.EDI_CTL_NO = UHD.EDI_CTL_NO" & vbNewLine _
            & "   AND UDL.INOUT_KB = UHD.INOUT_KB" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..M_BO_MST_JNC BO1" & vbNewLine _
            & "   ON   BO1.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  BO1.OUTKA_POSI_BU_CD = HED.OUTKA_POSI_BU_CD_PA" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..M_BO_MST_JNC BO2" & vbNewLine _
            & "   ON   BO2.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  BO2.OUTKA_POSI_BU_CD = HED.DEST_BU_CD" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB1" & vbNewLine _
            & "   ON   KB1.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB1.KBN_GROUP_CD = 'J022'" & vbNewLine _
            & "   AND  KB1.KBN_CD = HED.RB_KBN" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB2" & vbNewLine _
            & "   ON   KB2.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB2.KBN_GROUP_CD = 'J018'" & vbNewLine _
            & "   AND  KB2.KBN_CD = HED.MOD_KBN" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB3" & vbNewLine _
            & "   ON   KB3.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB3.KBN_GROUP_CD = 'J017'" & vbNewLine _
            & "   AND  KB3.KBN_CD = HED.RTN_FLG" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB4" & vbNewLine _
            & "   ON   KB4.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB4.KBN_GROUP_CD = 'J021'" & vbNewLine _
            & "   AND  KB4.KBN_CD = HED.SND_CANCEL_FLG" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB5" & vbNewLine _
            & "   ON   KB5.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB5.KBN_GROUP_CD = 'J019'" & vbNewLine _
            & "   AND  KB5.KBN_CD = HED.PRTFLG" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB6" & vbNewLine _
            & "   ON   KB6.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB6.KBN_GROUP_CD = 'J019'" & vbNewLine _
            & "   AND  KB6.KBN_CD = HED.NRS_SYS_FLG" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB7" & vbNewLine _
            & "   ON   KB7.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB7.KBN_GROUP_CD = 'J020'" & vbNewLine _
            & "   AND  KB7.KBN_CD = HED.UNSO_REQ_YN" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB8" & vbNewLine _
            & "   ON   KB8.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB8.KBN_GROUP_CD = 'J016'" & vbNewLine _
            & "   AND  KB8.KBN_NM2 = HED.ACT_UNSO_CD" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KB9" & vbNewLine _
            & "   ON   KB9.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KB9.KBN_GROUP_CD = 'J015'" & vbNewLine _
            & "   AND  KB9.KBN_NM2 = HED.UNSO_ROUTE_A" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KBA" & vbNewLine _
            & "   ON   KBA.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KBA.KBN_GROUP_CD = 'J020'" & vbNewLine _
            & "   AND  KBA.KBN_CD = CASE HED.COMBI_NO_A WHEN '' THEN '2' ELSE '1' END" & vbNewLine _
            & " --運送データ内容　ADD 2020/08/27 013087 " & vbNewLine _
            & " --LEFT JOIN " & vbNewLine _
            & " --  (SELECT HED2.SR_DEN_NO " & vbNewLine _
            & " --         ,HED2.NRS_BR_CD " & vbNewLine _
            & " -- ,MAX(HED2.EDI_CTL_NO) as EDI_CTL_NO " & vbNewLine _
            & " --       FROM " & vbNewLine _
            & " --         $LM_TRN$..H_INOUTKAEDI_HED_JNC HED2 " & vbNewLine _
            & " --       WHERE  HED2.SYS_DEL_FLG = '0' " & vbNewLine _
            & " --         AND  HED2.DATA_KIND = '3001' " & vbNewLine _
            & " --       GROUP BY HED2.SR_DEN_NO,HED2.NRS_BR_CD) HEDUNSOGET " & vbNewLine _
            & " --   ON HEDUNSOGET.SR_DEN_NO = HED.SR_DEN_NO " & vbNewLine _
            & " --  AND HEDUNSOGET.NRS_BR_CD = HED.NRS_BR_CD  " & vbNewLine _
            & " LEFT JOIN " & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_HED_JNC HEDUNSO " & vbNewLine _
            & "   ON   HEDUNSO.SYS_DEL_FLG = '0' " & vbNewLine _
            & "--以前の運送データに修正   AND  HEDUNSO.EDI_CTL_NO = HEDUNSOGET.EDI_CTL_NO " & vbNewLine _
            & "   AND  HEDUNSO.EDI_CTL_NO = UHD.EDI_CTL_NO " & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KBB" & vbNewLine _
            & "   ON   KBB.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KBB.KBN_GROUP_CD = 'J017'" & vbNewLine _
            & " --  AND  KBB.KBN_CD = HEDUNSOGET.RTN_FLG" & vbNewLine _
            & "   AND  KBB.KBN_CD = UHD.RTN_FLG" & vbNewLine _
            & " LEFT JOIN" & vbNewLine _
            & "   $LM_MST$..Z_KBN KBC" & vbNewLine _
            & "   ON   KBC.SYS_DEL_FLG = '0'" & vbNewLine _
            & "   AND  KBC.KBN_GROUP_CD = 'J019'" & vbNewLine _
            & "   AND  KBC.KBN_CD = HED.PRTFLG_SUB" & vbNewLine


#End Region

#Region "保存処理(編集)"

    ''' <summary>
    ''' 保存処理(編集)：排他：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_HED_SAVE_EDIT As String = "" _
            & " SELECT" & vbNewLine _
            & "   COUNT(*) AS SELECT_CNT" & vbNewLine _
            & " FROM" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_HED_JNC" & vbNewLine _
            & " WHERE" & vbNewLine _
            & "   NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
            & "   AND EDI_CTL_NO = @EDI_CTL_NO" & vbNewLine _
            & "   AND DATA_KIND = @DATA_KIND" & vbNewLine _
            & "   AND SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
            & "   AND SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine

    ''' <summary>
    ''' 保存処理(編集)：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_HED_SAVE_EDIT As String = "" _
            & " UPDATE $LM_TRN$..H_INOUTKAEDI_HED_JNC" & vbNewLine _
            & " SET" & vbNewLine _
            & "   OUTKA_DATE_A = @OUTKA_DATE_A," & vbNewLine _
            & "   UNSO_ROUTE_A = @UNSO_ROUTE_A," & vbNewLine _
            & "   CAR_NO_A = @CAR_NO_A," & vbNewLine _
            & "   ACT_UNSO_CD = @ACT_UNSO_CD," & vbNewLine _
            & "   UNSO_CD_MEMO = @UNSO_CD_MEMO," & vbNewLine _
            & "   SYS_UPD_DATE = @SYS_UPD_DATE," & vbNewLine _
            & "   SYS_UPD_TIME = @SYS_UPD_TIME," & vbNewLine _
            & "   SYS_UPD_USER = @SYS_UPD_USER," & vbNewLine _
            & "   SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
            & " WHERE" & vbNewLine _
            & "   NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
            & "   AND EDI_CTL_NO = @EDI_CTL_NO" & vbNewLine _
            & "   AND DATA_KIND = @DATA_KIND" & vbNewLine

    ''' <summary>
    ''' 保存処理(編集)：排他：ＪＮＣＥＤＩ受信データ(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_DTL_SAVE_EDIT As String = "" _
            & " SELECT" & vbNewLine _
            & "   COUNT(*) AS SELECT_CNT" & vbNewLine _
            & " FROM" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_DTL_JNC" & vbNewLine _
            & " WHERE" & vbNewLine _
            & "   NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
            & "   AND INOUT_KB = @INOUT_KB" & vbNewLine _
            & "   AND EDI_CTL_NO = @EDI_CTL_NO" & vbNewLine _
            & "   AND EDI_CTL_NO_CHU = @EDI_CTL_NO_CHU" & vbNewLine _
            & "   AND SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
            & "   AND SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine

    ''' <summary>
    ''' 保存処理(編集)：更新：ＪＮＣＥＤＩ受信データ(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_DTL_SAVE_EDIT As String = "" _
            & " UPDATE $LM_TRN$..H_INOUTKAEDI_DTL_JNC" & vbNewLine _
            & " SET" & vbNewLine _
            & "   SURY_RPT = @SURY_RPT," & vbNewLine _
            & "   SYS_UPD_DATE = @SYS_UPD_DATE," & vbNewLine _
            & "   SYS_UPD_TIME = @SYS_UPD_TIME," & vbNewLine _
            & "   SYS_UPD_USER = @SYS_UPD_USER," & vbNewLine _
            & "   SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
            & " WHERE" & vbNewLine _
            & "   NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
            & "   AND INOUT_KB = @INOUT_KB" & vbNewLine _
            & "   AND EDI_CTL_NO = @EDI_CTL_NO" & vbNewLine _
            & "   AND EDI_CTL_NO_CHU = @EDI_CTL_NO_CHU" & vbNewLine

#End Region

#Region "保存処理(訂正)"

    ''' <summary>
    ''' 保存処理(訂正)：排他：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_HED_SAVE_REVISION As String = "" _
            & " SELECT" & vbNewLine _
            & "   COUNT(*) AS SELECT_CNT" & vbNewLine _
            & " FROM" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_HED_JNC" & vbNewLine _
            & " WHERE" & vbNewLine _
            & "   NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
            & "   AND EDI_CTL_NO = @EDI_CTL_NO" & vbNewLine _
            & "   AND DATA_KIND = @DATA_KIND" & vbNewLine _
            & "   AND SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
            & "   AND SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine

    ''' <summary>
    ''' 保存処理(訂正)：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_HED_SAVE_REVISION As String = "" _
            & " UPDATE $LM_TRN$..H_INOUTKAEDI_HED_JNC" & vbNewLine _
            & " SET" & vbNewLine _
            & "   SND_CANCEL_FLG = @SND_CANCEL_FLG," & vbNewLine _
            & "   SYS_UPD_DATE = @SYS_UPD_DATE," & vbNewLine _
            & "   SYS_UPD_TIME = @SYS_UPD_TIME," & vbNewLine _
            & "   SYS_UPD_USER = @SYS_UPD_USER," & vbNewLine _
            & "   SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
            & " WHERE" & vbNewLine _
            & "   NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
            & "   AND EDI_CTL_NO = @EDI_CTL_NO" & vbNewLine _
            & "   AND DATA_KIND = @DATA_KIND" & vbNewLine

    ''' <summary>
    ''' 保存処理(訂正)：取得：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_HED_SAVE_REVISION As String = "" _
            & " SELECT" & vbNewLine _
            & "   DEL_KB," & vbNewLine _
            & "   CRT_DATE," & vbNewLine _
            & "   FILE_NAME," & vbNewLine _
            & "   REC_NO," & vbNewLine _
            & "   NRS_BR_CD," & vbNewLine _
            & "   INOUT_KB," & vbNewLine _
            & "   EDI_CTL_NO," & vbNewLine _
            & "   INKA_CTL_NO_L," & vbNewLine _
            & "   OUTKA_CTL_NO," & vbNewLine _
            & "   CUST_CD_L," & vbNewLine _
            & "   CUST_CD_M," & vbNewLine _
            & "   PRTFLG," & vbNewLine _
            & "   PRTFLG_SUB," & vbNewLine _
            & "   CANCEL_FLG," & vbNewLine _
            & "   NRS_TANTO," & vbNewLine _
            & "   DATA_KIND," & vbNewLine _
            & "   SEND_CODE," & vbNewLine _
            & "   SR_DEN_NO," & vbNewLine _
            & "   HIS_NO," & vbNewLine _
            & "   PROC_YMD," & vbNewLine _
            & "   PROC_TIME," & vbNewLine _
            & "   PROC_NO," & vbNewLine _
            & "   SEND_DEN_YMD," & vbNewLine _
            & "   SEND_DEN_TIME," & vbNewLine _
            & "   BPID_KKN," & vbNewLine _
            & "   BPID_SUB_KKN," & vbNewLine _
            & "   BPID_HAN," & vbNewLine _
            & "   RCV_COMP_CD," & vbNewLine _
            & "   SND_COMP_CD," & vbNewLine _
            & "   RB_KBN," & vbNewLine _
            & "   MOD_KBN," & vbNewLine _
            & "   DATA_KBN," & vbNewLine _
            & "   FAX_KBN," & vbNewLine _
            & "   OUTKA_REQ_KBN," & vbNewLine _
            & "   INKA_P_KBN," & vbNewLine _
            & "   OUTKA_SEPT_KBN," & vbNewLine _
            & "   EM_OUTKA_KBN," & vbNewLine _
            & "   UNSO_ROUTE_P," & vbNewLine _
            & "   UNSO_ROUTE_A," & vbNewLine _
            & "   CAR_KIND_P," & vbNewLine _
            & "   CAR_KIND_A," & vbNewLine _
            & "   CAR_NO_P," & vbNewLine _
            & "   CAR_NO_A," & vbNewLine _
            & "   COMBI_NO_P," & vbNewLine _
            & "   COMBI_NO_A," & vbNewLine _
            & "   UNSO_REQ_YN," & vbNewLine _
            & "   DEST_CHK_KBN," & vbNewLine _
            & "   INKO_DATE_P," & vbNewLine _
            & "   INKO_DATE_A," & vbNewLine _
            & "   INKO_TIME," & vbNewLine _
            & "   OUTKA_DATE_P," & vbNewLine _
            & "   OUTKA_DATE_A," & vbNewLine _
            & "   OUTKA_TIME_E," & vbNewLine _
            & "   CARGO_BKG_DATE_P," & vbNewLine _
            & "   CARGO_BKG_DATE_A," & vbNewLine _
            & "   ARRIVAL_DATE_P," & vbNewLine _
            & "   ARRIVAL_DATE_A," & vbNewLine _
            & "   ARRIVAL_TIME," & vbNewLine _
            & "   UNSO_DATE," & vbNewLine _
            & "   UNSO_TIME," & vbNewLine _
            & "   ZAI_RPT_DATE," & vbNewLine _
            & "   BAILER_CD," & vbNewLine _
            & "   BAILER_NM," & vbNewLine _
            & "   BAILER_BU_CD," & vbNewLine _
            & "   BAILER_BU_NM," & vbNewLine _
            & "   SHIPPER_CD," & vbNewLine _
            & "   SHIPPER_NM," & vbNewLine _
            & "   SHIPPER_BU_CD," & vbNewLine _
            & "   SHIPPER_BU_NM," & vbNewLine _
            & "   CONSIGNEE_CD," & vbNewLine _
            & "   CONSIGNEE_NM," & vbNewLine _
            & "   CONSIGNEE_BU_CD," & vbNewLine _
            & "   CONSIGNEE_BU_NM," & vbNewLine _
            & "   SOKO_PROV_CD," & vbNewLine _
            & "   SOKO_PROV_NM," & vbNewLine _
            & "   UNSO_PROV_CD," & vbNewLine _
            & "   UNSO_PROV_NM," & vbNewLine _
            & "   ACT_UNSO_CD," & vbNewLine _
            & "   UNSO_TF_KBN," & vbNewLine _
            & "   UNSO_F_KBN," & vbNewLine _
            & "   DEST_CD," & vbNewLine _
            & "   DEST_NM," & vbNewLine _
            & "   DEST_BU_CD," & vbNewLine _
            & "   DEST_BU_NM," & vbNewLine _
            & "   DEST_AD_CD," & vbNewLine _
            & "   DEST_AD_NM," & vbNewLine _
            & "   DEST_YB_NO," & vbNewLine _
            & "   DEST_TEL_NO," & vbNewLine _
            & "   DEST_FAX_NO," & vbNewLine _
            & "   DELIVERY_NM," & vbNewLine _
            & "   DELIVERY_SAGYO," & vbNewLine _
            & "   ORDER_NO," & vbNewLine _
            & "   JYUCHU_NO," & vbNewLine _
            & "   PRI_SHOP_CD," & vbNewLine _
            & "   PRI_SHOP_NM," & vbNewLine _
            & "   INV_REM_NM," & vbNewLine _
            & "   INV_REM_KANA," & vbNewLine _
            & "   DEN_NO," & vbNewLine _
            & "   MEI_DEN_NO," & vbNewLine _
            & "   OUTKA_POSI_CD," & vbNewLine _
            & "   OUTKA_POSI_NM," & vbNewLine _
            & "   OUTKA_POSI_BU_CD_PA," & vbNewLine _
            & "   OUTKA_POSI_BU_CD_NAIBU," & vbNewLine _
            & "   OUTKA_POSI_BU_NM_PA," & vbNewLine _
            & "   OUTKA_POSI_BU_NM_NAIBU," & vbNewLine _
            & "   OUTKA_POSI_AD_CD_PA," & vbNewLine _
            & "   OUTKA_POSI_AD_NM_PA," & vbNewLine _
            & "   OUTKA_POSI_TEL_NO_PA," & vbNewLine _
            & "   OUTKA_POSI_FAX_NO_PA," & vbNewLine _
            & "   UNSO_JURYO," & vbNewLine _
            & "   UNSO_JURYO_FLG," & vbNewLine _
            & "   UNIT_LOAD_CD," & vbNewLine _
            & "   UNIT_LOAD_SU," & vbNewLine _
            & "   REMARK," & vbNewLine _
            & "   REMARK_KANA," & vbNewLine _
            & "   HARAI_KBN," & vbNewLine _
            & "   DATA_TYPE," & vbNewLine _
            & "   RTN_FLG," & vbNewLine _
            & "   SND_CANCEL_FLG," & vbNewLine _
            & "   OLD_DATA_FLG," & vbNewLine _
            & "   PRINT_NO," & vbNewLine _
            & "   NRS_SYS_FLG," & vbNewLine _
            & "   OLD_SYS_FLG," & vbNewLine _
            & "   RTN_FILE_DATE," & vbNewLine _
            & "   RTN_FILE_TIME," & vbNewLine _
            & "   RECORD_STATUS," & vbNewLine _
            & "   DELETE_USER," & vbNewLine _
            & "   DELETE_DATE," & vbNewLine _
            & "   DELETE_TIME," & vbNewLine _
            & "   DELETE_EDI_NO," & vbNewLine _
            & "   PRT_USER," & vbNewLine _
            & "   PRT_DATE," & vbNewLine _
            & "   PRT_TIME," & vbNewLine _
            & "   EDI_USER," & vbNewLine _
            & "   EDI_DATE," & vbNewLine _
            & "   EDI_TIME," & vbNewLine _
            & "   OUTKA_USER," & vbNewLine _
            & "   OUTKA_DATE," & vbNewLine _
            & "   OUTKA_TIME," & vbNewLine _
            & "   INKA_USER," & vbNewLine _
            & "   INKA_DATE," & vbNewLine _
            & "   INKA_TIME," & vbNewLine _
            & "   UPD_USER," & vbNewLine _
            & "   UPD_DATE," & vbNewLine _
            & "   UPD_TIME," & vbNewLine _
            & "   SYS_ENT_DATE," & vbNewLine _
            & "   SYS_ENT_TIME," & vbNewLine _
            & "   SYS_ENT_PGID," & vbNewLine _
            & "   SYS_ENT_USER," & vbNewLine _
            & "   SYS_UPD_DATE," & vbNewLine _
            & "   SYS_UPD_TIME," & vbNewLine _
            & "   SYS_UPD_PGID," & vbNewLine _
            & "   SYS_UPD_USER," & vbNewLine _
            & "   SYS_DEL_FLG," & vbNewLine _
            & "   PRT_FLG," & vbNewLine _
            & "   MATOME_FLG" & vbNewLine _
            & " FROM" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_HED_JNC" & vbNewLine _
            & " WHERE" & vbNewLine _
            & "   NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
            & "   AND EDI_CTL_NO = @EDI_CTL_NO" & vbNewLine _
            & "   AND DATA_KIND = @DATA_KIND" & vbNewLine

    ''' <summary>
    ''' 保存処理(訂正)：登録：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_HED_SAVE_REVISION As String = "" _
            & " INSERT INTO $LM_TRN$..H_INOUTKAEDI_HED_JNC (" & vbNewLine _
            & "   DEL_KB," & vbNewLine _
            & "   CRT_DATE," & vbNewLine _
            & "   FILE_NAME," & vbNewLine _
            & "   REC_NO," & vbNewLine _
            & "   NRS_BR_CD," & vbNewLine _
            & "   INOUT_KB," & vbNewLine _
            & "   EDI_CTL_NO," & vbNewLine _
            & "   INKA_CTL_NO_L," & vbNewLine _
            & "   OUTKA_CTL_NO," & vbNewLine _
            & "   CUST_CD_L," & vbNewLine _
            & "   CUST_CD_M," & vbNewLine _
            & "   PRTFLG," & vbNewLine _
            & "   PRTFLG_SUB," & vbNewLine _
            & "   CANCEL_FLG," & vbNewLine _
            & "   NRS_TANTO," & vbNewLine _
            & "   DATA_KIND," & vbNewLine _
            & "   SEND_CODE," & vbNewLine _
            & "   SR_DEN_NO," & vbNewLine _
            & "   HIS_NO," & vbNewLine _
            & "   PROC_YMD," & vbNewLine _
            & "   PROC_TIME," & vbNewLine _
            & "   PROC_NO," & vbNewLine _
            & "   SEND_DEN_YMD," & vbNewLine _
            & "   SEND_DEN_TIME," & vbNewLine _
            & "   BPID_KKN," & vbNewLine _
            & "   BPID_SUB_KKN," & vbNewLine _
            & "   BPID_HAN," & vbNewLine _
            & "   RCV_COMP_CD," & vbNewLine _
            & "   SND_COMP_CD," & vbNewLine _
            & "   RB_KBN," & vbNewLine _
            & "   MOD_KBN," & vbNewLine _
            & "   DATA_KBN," & vbNewLine _
            & "   FAX_KBN," & vbNewLine _
            & "   OUTKA_REQ_KBN," & vbNewLine _
            & "   INKA_P_KBN," & vbNewLine _
            & "   OUTKA_SEPT_KBN," & vbNewLine _
            & "   EM_OUTKA_KBN," & vbNewLine _
            & "   UNSO_ROUTE_P," & vbNewLine _
            & "   UNSO_ROUTE_A," & vbNewLine _
            & "   CAR_KIND_P," & vbNewLine _
            & "   CAR_KIND_A," & vbNewLine _
            & "   CAR_NO_P," & vbNewLine _
            & "   CAR_NO_A," & vbNewLine _
            & "   COMBI_NO_P," & vbNewLine _
            & "   COMBI_NO_A," & vbNewLine _
            & "   UNSO_REQ_YN," & vbNewLine _
            & "   DEST_CHK_KBN," & vbNewLine _
            & "   INKO_DATE_P," & vbNewLine _
            & "   INKO_DATE_A," & vbNewLine _
            & "   INKO_TIME," & vbNewLine _
            & "   OUTKA_DATE_P," & vbNewLine _
            & "   OUTKA_DATE_A," & vbNewLine _
            & "   OUTKA_TIME_E," & vbNewLine _
            & "   CARGO_BKG_DATE_P," & vbNewLine _
            & "   CARGO_BKG_DATE_A," & vbNewLine _
            & "   ARRIVAL_DATE_P," & vbNewLine _
            & "   ARRIVAL_DATE_A," & vbNewLine _
            & "   ARRIVAL_TIME," & vbNewLine _
            & "   UNSO_DATE," & vbNewLine _
            & "   UNSO_TIME," & vbNewLine _
            & "   ZAI_RPT_DATE," & vbNewLine _
            & "   BAILER_CD," & vbNewLine _
            & "   BAILER_NM," & vbNewLine _
            & "   BAILER_BU_CD," & vbNewLine _
            & "   BAILER_BU_NM," & vbNewLine _
            & "   SHIPPER_CD," & vbNewLine _
            & "   SHIPPER_NM," & vbNewLine _
            & "   SHIPPER_BU_CD," & vbNewLine _
            & "   SHIPPER_BU_NM," & vbNewLine _
            & "   CONSIGNEE_CD," & vbNewLine _
            & "   CONSIGNEE_NM," & vbNewLine _
            & "   CONSIGNEE_BU_CD," & vbNewLine _
            & "   CONSIGNEE_BU_NM," & vbNewLine _
            & "   SOKO_PROV_CD," & vbNewLine _
            & "   SOKO_PROV_NM," & vbNewLine _
            & "   UNSO_PROV_CD," & vbNewLine _
            & "   UNSO_PROV_NM," & vbNewLine _
            & "   ACT_UNSO_CD," & vbNewLine _
            & "   UNSO_TF_KBN," & vbNewLine _
            & "   UNSO_F_KBN," & vbNewLine _
            & "   DEST_CD," & vbNewLine _
            & "   DEST_NM," & vbNewLine _
            & "   DEST_BU_CD," & vbNewLine _
            & "   DEST_BU_NM," & vbNewLine _
            & "   DEST_AD_CD," & vbNewLine _
            & "   DEST_AD_NM," & vbNewLine _
            & "   DEST_YB_NO," & vbNewLine _
            & "   DEST_TEL_NO," & vbNewLine _
            & "   DEST_FAX_NO," & vbNewLine _
            & "   DELIVERY_NM," & vbNewLine _
            & "   DELIVERY_SAGYO," & vbNewLine _
            & "   ORDER_NO," & vbNewLine _
            & "   JYUCHU_NO," & vbNewLine _
            & "   PRI_SHOP_CD," & vbNewLine _
            & "   PRI_SHOP_NM," & vbNewLine _
            & "   INV_REM_NM," & vbNewLine _
            & "   INV_REM_KANA," & vbNewLine _
            & "   DEN_NO," & vbNewLine _
            & "   MEI_DEN_NO," & vbNewLine _
            & "   OUTKA_POSI_CD," & vbNewLine _
            & "   OUTKA_POSI_NM," & vbNewLine _
            & "   OUTKA_POSI_BU_CD_PA," & vbNewLine _
            & "   OUTKA_POSI_BU_CD_NAIBU," & vbNewLine _
            & "   OUTKA_POSI_BU_NM_PA," & vbNewLine _
            & "   OUTKA_POSI_BU_NM_NAIBU," & vbNewLine _
            & "   OUTKA_POSI_AD_CD_PA," & vbNewLine _
            & "   OUTKA_POSI_AD_NM_PA," & vbNewLine _
            & "   OUTKA_POSI_TEL_NO_PA," & vbNewLine _
            & "   OUTKA_POSI_FAX_NO_PA," & vbNewLine _
            & "   UNSO_JURYO," & vbNewLine _
            & "   UNSO_JURYO_FLG," & vbNewLine _
            & "   UNIT_LOAD_CD," & vbNewLine _
            & "   UNIT_LOAD_SU," & vbNewLine _
            & "   REMARK," & vbNewLine _
            & "   REMARK_KANA," & vbNewLine _
            & "   HARAI_KBN," & vbNewLine _
            & "   DATA_TYPE," & vbNewLine _
            & "   RTN_FLG," & vbNewLine _
            & "   SND_CANCEL_FLG," & vbNewLine _
            & "   OLD_DATA_FLG," & vbNewLine _
            & "   PRINT_NO," & vbNewLine _
            & "   NRS_SYS_FLG," & vbNewLine _
            & "   OLD_SYS_FLG," & vbNewLine _
            & "   RTN_FILE_DATE," & vbNewLine _
            & "   RTN_FILE_TIME," & vbNewLine _
            & "   RECORD_STATUS," & vbNewLine _
            & "   DELETE_USER," & vbNewLine _
            & "   DELETE_DATE," & vbNewLine _
            & "   DELETE_TIME," & vbNewLine _
            & "   DELETE_EDI_NO," & vbNewLine _
            & "   PRT_USER," & vbNewLine _
            & "   PRT_DATE," & vbNewLine _
            & "   PRT_TIME," & vbNewLine _
            & "   EDI_USER," & vbNewLine _
            & "   EDI_DATE," & vbNewLine _
            & "   EDI_TIME," & vbNewLine _
            & "   OUTKA_USER," & vbNewLine _
            & "   OUTKA_DATE," & vbNewLine _
            & "   OUTKA_TIME," & vbNewLine _
            & "   INKA_USER," & vbNewLine _
            & "   INKA_DATE," & vbNewLine _
            & "   INKA_TIME," & vbNewLine _
            & "   UPD_USER," & vbNewLine _
            & "   UPD_DATE," & vbNewLine _
            & "   UPD_TIME," & vbNewLine _
            & "   SYS_ENT_DATE," & vbNewLine _
            & "   SYS_ENT_TIME," & vbNewLine _
            & "   SYS_ENT_PGID," & vbNewLine _
            & "   SYS_ENT_USER," & vbNewLine _
            & "   SYS_UPD_DATE," & vbNewLine _
            & "   SYS_UPD_TIME," & vbNewLine _
            & "   SYS_UPD_PGID," & vbNewLine _
            & "   SYS_UPD_USER," & vbNewLine _
            & "   SYS_DEL_FLG," & vbNewLine _
            & "   PRT_FLG," & vbNewLine _
            & "   MATOME_FLG" & vbNewLine _
            & " ) VALUES (" & vbNewLine _
            & "   @DEL_KB," & vbNewLine _
            & "   @CRT_DATE," & vbNewLine _
            & "   @FILE_NAME," & vbNewLine _
            & "   @REC_NO," & vbNewLine _
            & "   @NRS_BR_CD," & vbNewLine _
            & "   @INOUT_KB," & vbNewLine _
            & "   @EDI_CTL_NO," & vbNewLine _
            & "   @INKA_CTL_NO_L," & vbNewLine _
            & "   @OUTKA_CTL_NO," & vbNewLine _
            & "   @CUST_CD_L," & vbNewLine _
            & "   @CUST_CD_M," & vbNewLine _
            & "   @PRTFLG," & vbNewLine _
            & "   @PRTFLG_SUB," & vbNewLine _
            & "   @CANCEL_FLG," & vbNewLine _
            & "   @NRS_TANTO," & vbNewLine _
            & "   @DATA_KIND," & vbNewLine _
            & "   @SEND_CODE," & vbNewLine _
            & "   @SR_DEN_NO," & vbNewLine _
            & "   @HIS_NO," & vbNewLine _
            & "   @PROC_YMD," & vbNewLine _
            & "   @PROC_TIME," & vbNewLine _
            & "   @PROC_NO," & vbNewLine _
            & "   @SEND_DEN_YMD," & vbNewLine _
            & "   @SEND_DEN_TIME," & vbNewLine _
            & "   @BPID_KKN," & vbNewLine _
            & "   @BPID_SUB_KKN," & vbNewLine _
            & "   @BPID_HAN," & vbNewLine _
            & "   @RCV_COMP_CD," & vbNewLine _
            & "   @SND_COMP_CD," & vbNewLine _
            & "   @RB_KBN," & vbNewLine _
            & "   @MOD_KBN," & vbNewLine _
            & "   @DATA_KBN," & vbNewLine _
            & "   @FAX_KBN," & vbNewLine _
            & "   @OUTKA_REQ_KBN," & vbNewLine _
            & "   @INKA_P_KBN," & vbNewLine _
            & "   @OUTKA_SEPT_KBN," & vbNewLine _
            & "   @EM_OUTKA_KBN," & vbNewLine _
            & "   @UNSO_ROUTE_P," & vbNewLine _
            & "   @UNSO_ROUTE_A," & vbNewLine _
            & "   @CAR_KIND_P," & vbNewLine _
            & "   @CAR_KIND_A," & vbNewLine _
            & "   @CAR_NO_P," & vbNewLine _
            & "   @CAR_NO_A," & vbNewLine _
            & "   @COMBI_NO_P," & vbNewLine _
            & "   @COMBI_NO_A," & vbNewLine _
            & "   @UNSO_REQ_YN," & vbNewLine _
            & "   @DEST_CHK_KBN," & vbNewLine _
            & "   @INKO_DATE_P," & vbNewLine _
            & "   @INKO_DATE_A," & vbNewLine _
            & "   @INKO_TIME," & vbNewLine _
            & "   @OUTKA_DATE_P," & vbNewLine _
            & "   @OUTKA_DATE_A," & vbNewLine _
            & "   @OUTKA_TIME_E," & vbNewLine _
            & "   @CARGO_BKG_DATE_P," & vbNewLine _
            & "   @CARGO_BKG_DATE_A," & vbNewLine _
            & "   @ARRIVAL_DATE_P," & vbNewLine _
            & "   @ARRIVAL_DATE_A," & vbNewLine _
            & "   @ARRIVAL_TIME," & vbNewLine _
            & "   @UNSO_DATE," & vbNewLine _
            & "   @UNSO_TIME," & vbNewLine _
            & "   @ZAI_RPT_DATE," & vbNewLine _
            & "   @BAILER_CD," & vbNewLine _
            & "   @BAILER_NM," & vbNewLine _
            & "   @BAILER_BU_CD," & vbNewLine _
            & "   @BAILER_BU_NM," & vbNewLine _
            & "   @SHIPPER_CD," & vbNewLine _
            & "   @SHIPPER_NM," & vbNewLine _
            & "   @SHIPPER_BU_CD," & vbNewLine _
            & "   @SHIPPER_BU_NM," & vbNewLine _
            & "   @CONSIGNEE_CD," & vbNewLine _
            & "   @CONSIGNEE_NM," & vbNewLine _
            & "   @CONSIGNEE_BU_CD," & vbNewLine _
            & "   @CONSIGNEE_BU_NM," & vbNewLine _
            & "   @SOKO_PROV_CD," & vbNewLine _
            & "   @SOKO_PROV_NM," & vbNewLine _
            & "   @UNSO_PROV_CD," & vbNewLine _
            & "   @UNSO_PROV_NM," & vbNewLine _
            & "   @ACT_UNSO_CD," & vbNewLine _
            & "   @UNSO_TF_KBN," & vbNewLine _
            & "   @UNSO_F_KBN," & vbNewLine _
            & "   @DEST_CD," & vbNewLine _
            & "   @DEST_NM," & vbNewLine _
            & "   @DEST_BU_CD," & vbNewLine _
            & "   @DEST_BU_NM," & vbNewLine _
            & "   @DEST_AD_CD," & vbNewLine _
            & "   @DEST_AD_NM," & vbNewLine _
            & "   @DEST_YB_NO," & vbNewLine _
            & "   @DEST_TEL_NO," & vbNewLine _
            & "   @DEST_FAX_NO," & vbNewLine _
            & "   @DELIVERY_NM," & vbNewLine _
            & "   @DELIVERY_SAGYO," & vbNewLine _
            & "   @ORDER_NO," & vbNewLine _
            & "   @JYUCHU_NO," & vbNewLine _
            & "   @PRI_SHOP_CD," & vbNewLine _
            & "   @PRI_SHOP_NM," & vbNewLine _
            & "   @INV_REM_NM," & vbNewLine _
            & "   @INV_REM_KANA," & vbNewLine _
            & "   @DEN_NO," & vbNewLine _
            & "   @MEI_DEN_NO," & vbNewLine _
            & "   @OUTKA_POSI_CD," & vbNewLine _
            & "   @OUTKA_POSI_NM," & vbNewLine _
            & "   @OUTKA_POSI_BU_CD_PA," & vbNewLine _
            & "   @OUTKA_POSI_BU_CD_NAIBU," & vbNewLine _
            & "   @OUTKA_POSI_BU_NM_PA," & vbNewLine _
            & "   @OUTKA_POSI_BU_NM_NAIBU," & vbNewLine _
            & "   @OUTKA_POSI_AD_CD_PA," & vbNewLine _
            & "   @OUTKA_POSI_AD_NM_PA," & vbNewLine _
            & "   @OUTKA_POSI_TEL_NO_PA," & vbNewLine _
            & "   @OUTKA_POSI_FAX_NO_PA," & vbNewLine _
            & "   @UNSO_JURYO," & vbNewLine _
            & "   @UNSO_JURYO_FLG," & vbNewLine _
            & "   @UNIT_LOAD_CD," & vbNewLine _
            & "   @UNIT_LOAD_SU," & vbNewLine _
            & "   @REMARK," & vbNewLine _
            & "   @REMARK_KANA," & vbNewLine _
            & "   @HARAI_KBN," & vbNewLine _
            & "   @DATA_TYPE," & vbNewLine _
            & "   @RTN_FLG," & vbNewLine _
            & "   @SND_CANCEL_FLG," & vbNewLine _
            & "   @OLD_DATA_FLG," & vbNewLine _
            & "   @PRINT_NO," & vbNewLine _
            & "   @NRS_SYS_FLG," & vbNewLine _
            & "   @OLD_SYS_FLG," & vbNewLine _
            & "   @RTN_FILE_DATE," & vbNewLine _
            & "   @RTN_FILE_TIME," & vbNewLine _
            & "   @RECORD_STATUS," & vbNewLine _
            & "   @DELETE_USER," & vbNewLine _
            & "   @DELETE_DATE," & vbNewLine _
            & "   @DELETE_TIME," & vbNewLine _
            & "   @DELETE_EDI_NO," & vbNewLine _
            & "   @PRT_USER," & vbNewLine _
            & "   @PRT_DATE," & vbNewLine _
            & "   @PRT_TIME," & vbNewLine _
            & "   @EDI_USER," & vbNewLine _
            & "   @EDI_DATE," & vbNewLine _
            & "   @EDI_TIME," & vbNewLine _
            & "   @OUTKA_USER," & vbNewLine _
            & "   @OUTKA_DATE," & vbNewLine _
            & "   @OUTKA_TIME," & vbNewLine _
            & "   @INKA_USER," & vbNewLine _
            & "   @INKA_DATE," & vbNewLine _
            & "   @INKA_TIME," & vbNewLine _
            & "   @UPD_USER," & vbNewLine _
            & "   @UPD_DATE," & vbNewLine _
            & "   @UPD_TIME," & vbNewLine _
            & "   @SYS_ENT_DATE," & vbNewLine _
            & "   @SYS_ENT_TIME," & vbNewLine _
            & "   @SYS_ENT_PGID," & vbNewLine _
            & "   @SYS_ENT_USER," & vbNewLine _
            & "   @SYS_UPD_DATE," & vbNewLine _
            & "   @SYS_UPD_TIME," & vbNewLine _
            & "   @SYS_UPD_PGID," & vbNewLine _
            & "   @SYS_UPD_USER," & vbNewLine _
            & "   @SYS_DEL_FLG," & vbNewLine _
            & "   @PRT_FLG," & vbNewLine _
            & "   @MATOME_FLG" & vbNewLine _
            & " )" & vbNewLine

    ''' <summary>
    ''' 保存処理(訂正)：取得：ＪＮＣＥＤＩ受信データ(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DTL_SAVE_REVISION As String = "" _
            & " SELECT" & vbNewLine _
            & "   DEL_KB," & vbNewLine _
            & "   CRT_DATE," & vbNewLine _
            & "   FILE_NAME," & vbNewLine _
            & "   REC_NO," & vbNewLine _
            & "   GYO," & vbNewLine _
            & "   NRS_BR_CD," & vbNewLine _
            & "   INOUT_KB," & vbNewLine _
            & "   EDI_CTL_NO," & vbNewLine _
            & "   EDI_CTL_NO_CHU," & vbNewLine _
            & "   INKA_CTL_NO_L," & vbNewLine _
            & "   INKA_CTL_NO_M," & vbNewLine _
            & "   OUTKA_CTL_NO," & vbNewLine _
            & "   OUTKA_CTL_NO_CHU," & vbNewLine _
            & "   CUST_CD_L," & vbNewLine _
            & "   CUST_CD_M," & vbNewLine _
            & "   SR_DEN_NO," & vbNewLine _
            & "   HIS_NO," & vbNewLine _
            & "   MEI_NO_P," & vbNewLine _
            & "   MEI_NO_A," & vbNewLine _
            & "   JYUCHU_GOODS_CD," & vbNewLine _
            & "   GOODS_NM," & vbNewLine _
            & "   GOODS_KANA1," & vbNewLine _
            & "   GOODS_KANA2," & vbNewLine _
            & "   NISUGATA_CD," & vbNewLine _
            & "   IRISUU," & vbNewLine _
            & "   LOT_NO_P," & vbNewLine _
            & "   LOT_NO_A," & vbNewLine _
            & "   SURY_TANI_CD," & vbNewLine _
            & "   SURY_REQ," & vbNewLine _
            & "   SURY_RPT," & vbNewLine _
            & "   MEI_REM1," & vbNewLine _
            & "   MEI_REM2," & vbNewLine _
            & "   RECORD_STATUS," & vbNewLine _
            & "   JISSEKI_SHORI_FLG," & vbNewLine _
            & "   JISSEKI_USER," & vbNewLine _
            & "   JISSEKI_DATE," & vbNewLine _
            & "   JISSEKI_TIME," & vbNewLine _
            & "   SEND_USER," & vbNewLine _
            & "   SEND_DATE," & vbNewLine _
            & "   SEND_TIME," & vbNewLine _
            & "   DELETE_USER," & vbNewLine _
            & "   DELETE_DATE," & vbNewLine _
            & "   DELETE_TIME," & vbNewLine _
            & "   DELETE_EDI_NO," & vbNewLine _
            & "   DELETE_EDI_NO_CHU," & vbNewLine _
            & "   UPD_USER," & vbNewLine _
            & "   UPD_DATE," & vbNewLine _
            & "   UPD_TIME," & vbNewLine _
            & "   SYS_ENT_DATE," & vbNewLine _
            & "   SYS_ENT_TIME," & vbNewLine _
            & "   SYS_ENT_PGID," & vbNewLine _
            & "   SYS_ENT_USER," & vbNewLine _
            & "   SYS_UPD_DATE," & vbNewLine _
            & "   SYS_UPD_TIME," & vbNewLine _
            & "   SYS_UPD_PGID," & vbNewLine _
            & "   SYS_UPD_USER," & vbNewLine _
            & "   SYS_DEL_FLG" & vbNewLine _
            & " FROM" & vbNewLine _
            & "   $LM_TRN$..H_INOUTKAEDI_DTL_JNC" & vbNewLine _
            & " WHERE" & vbNewLine _
            & "   NRS_BR_CD = @NRS_BR_CD" & vbNewLine _
            & "   AND INOUT_KB = @INOUT_KB" & vbNewLine _
            & "   AND EDI_CTL_NO = @EDI_CTL_NO" & vbNewLine _
            & " ORDER BY" & vbNewLine _
            & "   EDI_CTL_NO_CHU" & vbNewLine

    ''' <summary>
    ''' 保存処理(訂正)：登録：ＪＮＣＥＤＩ受信データ(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_DTL_SAVE_REVISION As String = "" _
            & " INSERT INTO $LM_TRN$..H_INOUTKAEDI_DTL_JNC (" & vbNewLine _
            & "   DEL_KB," & vbNewLine _
            & "   CRT_DATE," & vbNewLine _
            & "   FILE_NAME," & vbNewLine _
            & "   REC_NO," & vbNewLine _
            & "   GYO," & vbNewLine _
            & "   NRS_BR_CD," & vbNewLine _
            & "   INOUT_KB," & vbNewLine _
            & "   EDI_CTL_NO," & vbNewLine _
            & "   EDI_CTL_NO_CHU," & vbNewLine _
            & "   INKA_CTL_NO_L," & vbNewLine _
            & "   INKA_CTL_NO_M," & vbNewLine _
            & "   OUTKA_CTL_NO," & vbNewLine _
            & "   OUTKA_CTL_NO_CHU," & vbNewLine _
            & "   CUST_CD_L," & vbNewLine _
            & "   CUST_CD_M," & vbNewLine _
            & "   SR_DEN_NO," & vbNewLine _
            & "   HIS_NO," & vbNewLine _
            & "   MEI_NO_P," & vbNewLine _
            & "   MEI_NO_A," & vbNewLine _
            & "   JYUCHU_GOODS_CD," & vbNewLine _
            & "   GOODS_NM," & vbNewLine _
            & "   GOODS_KANA1," & vbNewLine _
            & "   GOODS_KANA2," & vbNewLine _
            & "   NISUGATA_CD," & vbNewLine _
            & "   IRISUU," & vbNewLine _
            & "   LOT_NO_P," & vbNewLine _
            & "   LOT_NO_A," & vbNewLine _
            & "   SURY_TANI_CD," & vbNewLine _
            & "   SURY_REQ," & vbNewLine _
            & "   SURY_RPT," & vbNewLine _
            & "   MEI_REM1," & vbNewLine _
            & "   MEI_REM2," & vbNewLine _
            & "   RECORD_STATUS," & vbNewLine _
            & "   JISSEKI_SHORI_FLG," & vbNewLine _
            & "   JISSEKI_USER," & vbNewLine _
            & "   JISSEKI_DATE," & vbNewLine _
            & "   JISSEKI_TIME," & vbNewLine _
            & "   SEND_USER," & vbNewLine _
            & "   SEND_DATE," & vbNewLine _
            & "   SEND_TIME," & vbNewLine _
            & "   DELETE_USER," & vbNewLine _
            & "   DELETE_DATE," & vbNewLine _
            & "   DELETE_TIME," & vbNewLine _
            & "   DELETE_EDI_NO," & vbNewLine _
            & "   DELETE_EDI_NO_CHU," & vbNewLine _
            & "   UPD_USER," & vbNewLine _
            & "   UPD_DATE," & vbNewLine _
            & "   UPD_TIME," & vbNewLine _
            & "   SYS_ENT_DATE," & vbNewLine _
            & "   SYS_ENT_TIME," & vbNewLine _
            & "   SYS_ENT_PGID," & vbNewLine _
            & "   SYS_ENT_USER," & vbNewLine _
            & "   SYS_UPD_DATE," & vbNewLine _
            & "   SYS_UPD_TIME," & vbNewLine _
            & "   SYS_UPD_PGID," & vbNewLine _
            & "   SYS_UPD_USER," & vbNewLine _
            & "   SYS_DEL_FLG" & vbNewLine _
            & " ) VALUES (" & vbNewLine _
            & "   @DEL_KB," & vbNewLine _
            & "   @CRT_DATE," & vbNewLine _
            & "   @FILE_NAME," & vbNewLine _
            & "   @REC_NO," & vbNewLine _
            & "   @GYO," & vbNewLine _
            & "   @NRS_BR_CD," & vbNewLine _
            & "   @INOUT_KB," & vbNewLine _
            & "   @EDI_CTL_NO," & vbNewLine _
            & "   @EDI_CTL_NO_CHU," & vbNewLine _
            & "   @INKA_CTL_NO_L," & vbNewLine _
            & "   @INKA_CTL_NO_M," & vbNewLine _
            & "   @OUTKA_CTL_NO," & vbNewLine _
            & "   @OUTKA_CTL_NO_CHU," & vbNewLine _
            & "   @CUST_CD_L," & vbNewLine _
            & "   @CUST_CD_M," & vbNewLine _
            & "   @SR_DEN_NO," & vbNewLine _
            & "   @HIS_NO," & vbNewLine _
            & "   @MEI_NO_P," & vbNewLine _
            & "   @MEI_NO_A," & vbNewLine _
            & "   @JYUCHU_GOODS_CD," & vbNewLine _
            & "   @GOODS_NM," & vbNewLine _
            & "   @GOODS_KANA1," & vbNewLine _
            & "   @GOODS_KANA2," & vbNewLine _
            & "   @NISUGATA_CD," & vbNewLine _
            & "   @IRISUU," & vbNewLine _
            & "   @LOT_NO_P," & vbNewLine _
            & "   @LOT_NO_A," & vbNewLine _
            & "   @SURY_TANI_CD," & vbNewLine _
            & "   @SURY_REQ," & vbNewLine _
            & "   @SURY_RPT," & vbNewLine _
            & "   @MEI_REM1," & vbNewLine _
            & "   @MEI_REM2," & vbNewLine _
            & "   @RECORD_STATUS," & vbNewLine _
            & "   @JISSEKI_SHORI_FLG," & vbNewLine _
            & "   @JISSEKI_USER," & vbNewLine _
            & "   @JISSEKI_DATE," & vbNewLine _
            & "   @JISSEKI_TIME," & vbNewLine _
            & "   @SEND_USER," & vbNewLine _
            & "   @SEND_DATE," & vbNewLine _
            & "   @SEND_TIME," & vbNewLine _
            & "   @DELETE_USER," & vbNewLine _
            & "   @DELETE_DATE," & vbNewLine _
            & "   @DELETE_TIME," & vbNewLine _
            & "   @DELETE_EDI_NO," & vbNewLine _
            & "   @DELETE_EDI_NO_CHU," & vbNewLine _
            & "   @UPD_USER," & vbNewLine _
            & "   @UPD_DATE," & vbNewLine _
            & "   @UPD_TIME," & vbNewLine _
            & "   @SYS_ENT_DATE," & vbNewLine _
            & "   @SYS_ENT_TIME," & vbNewLine _
            & "   @SYS_ENT_PGID," & vbNewLine _
            & "   @SYS_ENT_USER," & vbNewLine _
            & "   @SYS_UPD_DATE," & vbNewLine _
            & "   @SYS_UPD_TIME," & vbNewLine _
            & "   @SYS_UPD_PGID," & vbNewLine _
            & "   @SYS_UPD_USER," & vbNewLine _
            & "   @SYS_DEL_FLG" & vbNewLine _
            & " )" & vbNewLine

#End Region

#End Region 'SQL

#Region "Field"

    ''' <summary>
    ''' DataTableの行抜き出し
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' 除外用DataTableの行抜き出し
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row2 As Data.DataRow

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

#End Region 'Field

#Region "Method"

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

    ''' <summary>
    ''' 共通処理：データテーブルのカレントインデックスを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>カレントインデックス</returns>
    ''' <remarks></remarks>
    Private Function GetCurrentIndex(ByVal ds As DataSet) As Integer

        Return Convert.ToInt32(ds.Tables(TABLE_NM.IN_IDX).Rows(0).Item("IDX"))

    End Function

#End Region

#Region "マスタデータ"

    ''' <summary>
    ''' マスタデータ：取得：ＪＮＣ営業所マスタ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function BoMstSelect(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_BO_MST)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_SELECT_BO_MST)

        '条件およびパラメータの設定
        Call Me.BoMstSelectSetCondition()

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()
        If reader.HasRows() Then
            map.Add("OUTKA_POSI_BU_CD", "OUTKA_POSI_BU_CD")
            map.Add("OUTKA_POSI_BU_NM", "OUTKA_POSI_BU_NM")
            map.Add("OUTKA_POSI_BU_NM_RYAK", "OUTKA_POSI_BU_NM_RYAK")
            map.Add("OUTKA_POSI_AD_CD", "OUTKA_POSI_AD_CD")
            map.Add("OUTKA_POSI_AD_NM", "OUTKA_POSI_AD_NM")
            map.Add("OUTKA_POSI_TEL_NO", "OUTKA_POSI_TEL_NO")
            map.Add("OUTKA_POSI_FAX_NO", "OUTKA_POSI_FAX_NO")
            map.Add("CHISSO_OUTKA_POSI_CD", "CHISSO_OUTKA_POSI_CD")
            map.Add("CHISSO_OUTKA_POSI_NM", "CHISSO_OUTKA_POSI_NM")
            map.Add("ST_KBN", "ST_KBN")
            map.Add("NRS_SV", "NRS_SV")
            map.Add("NRS_DB", "NRS_DB")
            map.Add("CUST_CD_L", "CUST_CD_L")
            map.Add("CUST_CD_M", "CUST_CD_M")
            map.Add("NRS_BO", "NRS_BO")
            map.Add("NRS_SOKO_CD", "NRS_SOKO_CD")
            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.OUT_BO_MST)
        End If

        '処理件数の設定
        MyBase.SetResultCount(ds.Tables(TABLE_NM.OUT_BO_MST).Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' マスタデータ：取得：ＪＮＣ営業所マスタ：条件
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BoMstSelectSetCondition()

        Dim whereStr As String = String.Empty

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件
        Me._StrSql.Append(" WHERE" & vbNewLine)
        With Me._Row
            '固定条件
            Me._StrSql.Append(" SYS_DEL_FLG = '0'" & vbNewLine)

            '出荷場所部門コード
            whereStr = .Item("OUTKA_POSI_BU_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND OUTKA_POSI_BU_CD = @OUTKA_POSI_BU_CD " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_POSI_BU_CD", whereStr, DBDataType.CHAR))
            End If

            '並び
            Me._StrSql.Append(" ORDER BY OUTKA_POSI_BU_CD" & vbNewLine)
        End With

    End Sub

#End Region

#Region "チェック"

    ''' <summary>
    ''' チェック：取得：編集中に旧データになっていないかチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OldDataChkSelect(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.INOUT_OLD_DATA_CHK)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_SELECT_OLD_DATA_CHK)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()
        If reader.HasRows() Then
            map.Add("SELECT_CNT", "SELECT_CNT")
            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.INOUT_OLD_DATA_CHK)
        End If

        '取得件数の設定
        MyBase.SetResultCount(ds.Tables(TABLE_NM.INOUT_OLD_DATA_CHK).Rows.Count())
        reader.Close()

        Return ds

    End Function

#End Region

#Region "出荷登録処理"

    ''' <summary>
    ''' 出荷登録処理：取得：ＥＤＩ出荷データＬ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OutkaSaveSelectEdiL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_EDI_L)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_SELECT_OUTKAEDI_L_OUTKA_SAVE)

        '条件およびパラメータの設定
        Call Me.OutkaSaveSelectEdiLSetCondition()

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()
        map.Add("DEL_KB", "DEL_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("OUTKA_KB", "OUTKA_KB")
        map.Add("SYUBETU_KB", "SYUBETU_KB")
        map.Add("NAIGAI_KB", "NAIGAI_KB")
        map.Add("OUTKA_STATE_KB", "OUTKA_STATE_KB")
        map.Add("OUTKAHOKOKU_YN", "OUTKAHOKOKU_YN")
        map.Add("PICK_KB", "PICK_KB")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("WH_CD", "WH_CD")
        map.Add("WH_NM", "WH_NM")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("OUTKO_DATE", "OUTKO_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("HOKOKU_DATE", "HOKOKU_DATE")
        map.Add("TOUKI_HOKAN_YN", "TOUKI_HOKAN_YN")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("SHIP_CD_L", "SHIP_CD_L")
        map.Add("SHIP_CD_M", "SHIP_CD_M")
        map.Add("SHIP_NM_L", "SHIP_NM_L")
        map.Add("SHIP_NM_M", "SHIP_NM_M")
        map.Add("EDI_DEST_CD", "EDI_DEST_CD")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_ZIP", "DEST_ZIP")
        map.Add("DEST_AD_1", "DEST_AD_1")
        map.Add("DEST_AD_2", "DEST_AD_2")
        map.Add("DEST_AD_3", "DEST_AD_3")
        map.Add("DEST_AD_4", "DEST_AD_4")
        map.Add("DEST_AD_5", "DEST_AD_5")
        map.Add("DEST_TEL", "DEST_TEL")
        map.Add("DEST_FAX", "DEST_FAX")
        map.Add("DEST_MAIL", "DEST_MAIL")
        map.Add("DEST_JIS_CD", "DEST_JIS_CD")
        map.Add("SP_NHS_KB", "SP_NHS_KB")
        map.Add("COA_YN", "COA_YN")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
        map.Add("UNSO_MOTO_KB", "UNSO_MOTO_KB")
        map.Add("UNSO_TEHAI_KB", "UNSO_TEHAI_KB")
        map.Add("SYARYO_KB", "SYARYO_KB")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("UNSO_BR_NM", "UNSO_BR_NM")
        map.Add("UNCHIN_TARIFF_CD", "UNCHIN_TARIFF_CD")
        map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD")
        map.Add("REMARK", "REMARK")
        map.Add("UNSO_ATT", "UNSO_ATT")
        map.Add("DENP_YN", "DENP_YN")
        map.Add("PC_KB", "PC_KB")
        map.Add("UNCHIN_YN", "UNCHIN_YN")
        map.Add("NIYAKU_YN", "NIYAKU_YN")
        map.Add("OUT_FLAG", "OUT_FLAG")
        map.Add("AKAKURO_KB", "AKAKURO_KB")
        map.Add("JISSEKI_FLAG", "JISSEKI_FLAG")
        map.Add("JISSEKI_USER", "JISSEKI_USER")
        map.Add("JISSEKI_DATE", "JISSEKI_DATE")
        map.Add("JISSEKI_TIME", "JISSEKI_TIME")
        map.Add("FREE_N01", "FREE_N01")
        map.Add("FREE_N02", "FREE_N02")
        map.Add("FREE_N03", "FREE_N03")
        map.Add("FREE_N04", "FREE_N04")
        map.Add("FREE_N05", "FREE_N05")
        map.Add("FREE_N06", "FREE_N06")
        map.Add("FREE_N07", "FREE_N07")
        map.Add("FREE_N08", "FREE_N08")
        map.Add("FREE_N09", "FREE_N09")
        map.Add("FREE_N10", "FREE_N10")
        map.Add("FREE_C01", "FREE_C01")
        map.Add("FREE_C02", "FREE_C02")
        map.Add("FREE_C03", "FREE_C03")
        map.Add("FREE_C04", "FREE_C04")
        map.Add("FREE_C05", "FREE_C05")
        map.Add("FREE_C06", "FREE_C06")
        map.Add("FREE_C07", "FREE_C07")
        map.Add("FREE_C08", "FREE_C08")
        map.Add("FREE_C09", "FREE_C09")
        map.Add("FREE_C10", "FREE_C10")
        map.Add("FREE_C11", "FREE_C11")
        map.Add("FREE_C12", "FREE_C12")
        map.Add("FREE_C13", "FREE_C13")
        map.Add("FREE_C14", "FREE_C14")
        map.Add("FREE_C15", "FREE_C15")
        map.Add("FREE_C16", "FREE_C16")
        map.Add("FREE_C17", "FREE_C17")
        map.Add("FREE_C18", "FREE_C18")
        map.Add("FREE_C19", "FREE_C19")
        map.Add("FREE_C20", "FREE_C20")
        map.Add("FREE_C21", "FREE_C21")
        map.Add("FREE_C22", "FREE_C22")
        map.Add("FREE_C23", "FREE_C23")
        map.Add("FREE_C24", "FREE_C24")
        map.Add("FREE_C25", "FREE_C25")
        map.Add("FREE_C26", "FREE_C26")
        map.Add("FREE_C27", "FREE_C27")
        map.Add("FREE_C28", "FREE_C28")
        map.Add("FREE_C29", "FREE_C29")
        map.Add("FREE_C30", "FREE_C30")
        map.Add("CRT_USER", "CRT_USER")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("CRT_TIME", "CRT_TIME")
        map.Add("UPD_USER", "UPD_USER")
        map.Add("UPD_DATE", "UPD_DATE")
        map.Add("UPD_TIME", "UPD_TIME")
        map.Add("SCM_CTL_NO_L", "SCM_CTL_NO_L")
        map.Add("EDIT_FLAG", "EDIT_FLAG")
        map.Add("MATCHING_FLAG", "MATCHING_FLAG")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.OUT_EDI_L)

        '取得件数の設定
        MyBase.SetResultCount(ds.Tables(TABLE_NM.OUT_EDI_L).Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 出荷登録処理：取得：ＥＤＩ出荷データＬ：条件
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OutkaSaveSelectEdiLSetCondition()

        Dim whereStr As String = String.Empty

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件
        Me._StrSql.Append(" WHERE" & vbNewLine)
        With Me._Row
            '固定条件
            Me._StrSql.Append(" SYS_DEL_FLG = '0'" & vbNewLine)

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND NRS_BR_CD = @NRS_BR_CD" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            'EDI管理番号L
            whereStr = .Item("EDI_CTL_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND EDI_CTL_NO = @EDI_CTL_NO" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND CUST_CD_L = @CUST_CD_L" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND CUST_CD_M = @CUST_CD_M" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '荷主注文番号（全体）
            whereStr = .Item("CUST_ORD_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND CUST_ORD_NO = @CUST_ORD_NO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", whereStr, DBDataType.NVARCHAR))
            End If
        End With

    End Sub

    ''' <summary>
    ''' 出荷登録処理：取得：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OutkaSaveSelectHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_HED)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_SELECT_HED_OUTKA_SAVE)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()
        map.Add("DEL_KB", "DEL_KB")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INOUT_KB", "INOUT_KB")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("INKA_CTL_NO_L", "INKA_CTL_NO_L")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("PRTFLG", "PRTFLG")
        map.Add("PRTFLG_SUB", "PRTFLG_SUB")
        map.Add("CANCEL_FLG", "CANCEL_FLG")
        map.Add("NRS_TANTO", "NRS_TANTO")
        map.Add("DATA_KIND", "DATA_KIND")
        map.Add("SEND_CODE", "SEND_CODE")
        map.Add("SR_DEN_NO", "SR_DEN_NO")
        map.Add("HIS_NO", "HIS_NO")
        map.Add("PROC_YMD", "PROC_YMD")
        map.Add("PROC_TIME", "PROC_TIME")
        map.Add("PROC_NO", "PROC_NO")
        map.Add("SEND_DEN_YMD", "SEND_DEN_YMD")
        map.Add("SEND_DEN_TIME", "SEND_DEN_TIME")
        map.Add("BPID_KKN", "BPID_KKN")
        map.Add("BPID_SUB_KKN", "BPID_SUB_KKN")
        map.Add("BPID_HAN", "BPID_HAN")
        map.Add("RCV_COMP_CD", "RCV_COMP_CD")
        map.Add("SND_COMP_CD", "SND_COMP_CD")
        map.Add("RB_KBN", "RB_KBN")
        map.Add("MOD_KBN", "MOD_KBN")
        map.Add("DATA_KBN", "DATA_KBN")
        map.Add("FAX_KBN", "FAX_KBN")
        map.Add("OUTKA_REQ_KBN", "OUTKA_REQ_KBN")
        map.Add("INKA_P_KBN", "INKA_P_KBN")
        map.Add("OUTKA_SEPT_KBN", "OUTKA_SEPT_KBN")
        map.Add("EM_OUTKA_KBN", "EM_OUTKA_KBN")
        map.Add("UNSO_ROUTE_P", "UNSO_ROUTE_P")
        map.Add("UNSO_ROUTE_A", "UNSO_ROUTE_A")
        map.Add("CAR_KIND_P", "CAR_KIND_P")
        map.Add("CAR_KIND_A", "CAR_KIND_A")
        map.Add("CAR_NO_P", "CAR_NO_P")
        map.Add("CAR_NO_A", "CAR_NO_A")
        map.Add("COMBI_NO_P", "COMBI_NO_P")
        map.Add("COMBI_NO_A", "COMBI_NO_A")
        map.Add("UNSO_REQ_YN", "UNSO_REQ_YN")
        map.Add("DEST_CHK_KBN", "DEST_CHK_KBN")
        map.Add("INKO_DATE_P", "INKO_DATE_P")
        map.Add("INKO_DATE_A", "INKO_DATE_A")
        map.Add("INKO_TIME", "INKO_TIME")
        map.Add("OUTKA_DATE_P", "OUTKA_DATE_P")
        map.Add("OUTKA_DATE_A", "OUTKA_DATE_A")
        map.Add("OUTKA_TIME_E", "OUTKA_TIME_E")
        map.Add("CARGO_BKG_DATE_P", "CARGO_BKG_DATE_P")
        map.Add("CARGO_BKG_DATE_A", "CARGO_BKG_DATE_A")
        map.Add("ARRIVAL_DATE_P", "ARRIVAL_DATE_P")
        map.Add("ARRIVAL_DATE_A", "ARRIVAL_DATE_A")
        map.Add("ARRIVAL_TIME", "ARRIVAL_TIME")
        map.Add("UNSO_DATE", "UNSO_DATE")
        map.Add("UNSO_TIME", "UNSO_TIME")
        map.Add("ZAI_RPT_DATE", "ZAI_RPT_DATE")
        map.Add("BAILER_CD", "BAILER_CD")
        map.Add("BAILER_NM", "BAILER_NM")
        map.Add("BAILER_BU_CD", "BAILER_BU_CD")
        map.Add("BAILER_BU_NM", "BAILER_BU_NM")
        map.Add("SHIPPER_CD", "SHIPPER_CD")
        map.Add("SHIPPER_NM", "SHIPPER_NM")
        map.Add("SHIPPER_BU_CD", "SHIPPER_BU_CD")
        map.Add("SHIPPER_BU_NM", "SHIPPER_BU_NM")
        map.Add("CONSIGNEE_CD", "CONSIGNEE_CD")
        map.Add("CONSIGNEE_NM", "CONSIGNEE_NM")
        map.Add("CONSIGNEE_BU_CD", "CONSIGNEE_BU_CD")
        map.Add("CONSIGNEE_BU_NM", "CONSIGNEE_BU_NM")
        map.Add("SOKO_PROV_CD", "SOKO_PROV_CD")
        map.Add("SOKO_PROV_NM", "SOKO_PROV_NM")
        map.Add("UNSO_PROV_CD", "UNSO_PROV_CD")
        map.Add("UNSO_PROV_NM", "UNSO_PROV_NM")
        map.Add("ACT_UNSO_CD", "ACT_UNSO_CD")
        map.Add("UNSO_TF_KBN", "UNSO_TF_KBN")
        map.Add("UNSO_F_KBN", "UNSO_F_KBN")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_BU_CD", "DEST_BU_CD")
        map.Add("DEST_BU_NM", "DEST_BU_NM")
        map.Add("DEST_AD_CD", "DEST_AD_CD")
        map.Add("DEST_AD_NM", "DEST_AD_NM")
        map.Add("DEST_YB_NO", "DEST_YB_NO")
        map.Add("DEST_TEL_NO", "DEST_TEL_NO")
        map.Add("DEST_FAX_NO", "DEST_FAX_NO")
        map.Add("DELIVERY_NM", "DELIVERY_NM")
        map.Add("DELIVERY_SAGYO", "DELIVERY_SAGYO")
        map.Add("ORDER_NO", "ORDER_NO")
        map.Add("JYUCHU_NO", "JYUCHU_NO")
        map.Add("PRI_SHOP_CD", "PRI_SHOP_CD")
        map.Add("PRI_SHOP_NM", "PRI_SHOP_NM")
        map.Add("INV_REM_NM", "INV_REM_NM")
        map.Add("INV_REM_KANA", "INV_REM_KANA")
        map.Add("DEN_NO", "DEN_NO")
        map.Add("MEI_DEN_NO", "MEI_DEN_NO")
        map.Add("OUTKA_POSI_CD", "OUTKA_POSI_CD")
        map.Add("OUTKA_POSI_NM", "OUTKA_POSI_NM")
        map.Add("OUTKA_POSI_BU_CD_PA", "OUTKA_POSI_BU_CD_PA")
        map.Add("OUTKA_POSI_BU_CD_NAIBU", "OUTKA_POSI_BU_CD_NAIBU")
        map.Add("OUTKA_POSI_BU_NM_PA", "OUTKA_POSI_BU_NM_PA")
        map.Add("OUTKA_POSI_BU_NM_NAIBU", "OUTKA_POSI_BU_NM_NAIBU")
        map.Add("OUTKA_POSI_AD_CD_PA", "OUTKA_POSI_AD_CD_PA")
        map.Add("OUTKA_POSI_AD_NM_PA", "OUTKA_POSI_AD_NM_PA")
        map.Add("OUTKA_POSI_TEL_NO_PA", "OUTKA_POSI_TEL_NO_PA")
        map.Add("OUTKA_POSI_FAX_NO_PA", "OUTKA_POSI_FAX_NO_PA")
        map.Add("UNSO_JURYO", "UNSO_JURYO")
        map.Add("UNSO_JURYO_FLG", "UNSO_JURYO_FLG")
        map.Add("UNIT_LOAD_CD", "UNIT_LOAD_CD")
        map.Add("UNIT_LOAD_SU", "UNIT_LOAD_SU")
        map.Add("REMARK", "REMARK")
        map.Add("REMARK_KANA", "REMARK_KANA")
        map.Add("HARAI_KBN", "HARAI_KBN")
        map.Add("DATA_TYPE", "DATA_TYPE")
        map.Add("RTN_FLG", "RTN_FLG")
        map.Add("SND_CANCEL_FLG", "SND_CANCEL_FLG")
        map.Add("OLD_DATA_FLG", "OLD_DATA_FLG")
        map.Add("PRINT_NO", "PRINT_NO")
        map.Add("NRS_SYS_FLG", "NRS_SYS_FLG")
        map.Add("OLD_SYS_FLG", "OLD_SYS_FLG")
        map.Add("RTN_FILE_DATE", "RTN_FILE_DATE")
        map.Add("RTN_FILE_TIME", "RTN_FILE_TIME")
        map.Add("RECORD_STATUS", "RECORD_STATUS")
        map.Add("DELETE_USER", "DELETE_USER")
        map.Add("DELETE_DATE", "DELETE_DATE")
        map.Add("DELETE_TIME", "DELETE_TIME")
        map.Add("DELETE_EDI_NO", "DELETE_EDI_NO")
        map.Add("PRT_USER", "PRT_USER")
        map.Add("PRT_DATE", "PRT_DATE")
        map.Add("PRT_TIME", "PRT_TIME")
        map.Add("EDI_USER", "EDI_USER")
        map.Add("EDI_DATE", "EDI_DATE")
        map.Add("EDI_TIME", "EDI_TIME")
        map.Add("OUTKA_DATE", "OUTKA_DATE")
        map.Add("OUTKA_USER", "OUTKA_USER")
        map.Add("OUTKA_TIME", "OUTKA_TIME")
        map.Add("INKA_USER", "INKA_USER")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("INKA_TIME", "INKA_TIME")
        map.Add("UPD_USER", "UPD_USER")
        map.Add("UPD_DATE", "UPD_DATE")
        map.Add("UPD_TIME", "UPD_TIME")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("PRT_FLG", "PRT_FLG")
        map.Add("MATOME_FLG", "MATOME_FLG")
        map.Add("UNSO_EDI_CTL_NO", "UNSO_EDI_CTL_NO")
        map.Add("NRS_SOKO_CD", "NRS_SOKO_CD")
        map.Add("SP_UNSO_CD", "SP_UNSO_CD")
        map.Add("SP_UNSO_BR_CD", "SP_UNSO_BR_CD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.OUT_HED_EDI)

        '取得件数の設定
        MyBase.SetResultCount(ds.Tables(TABLE_NM.OUT_HED).Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 出荷登録処理：取得：ＪＮＣＥＤＩ受信データ(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OutkaSaveSelectDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得(ヘッダーの条件を利用するので間違いではない)
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_HED)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_SELECT_DTL_OUTKA_SAVE)

        '条件およびパラメータの設定
        Call Me.OutkaSaveSelectDtlSetCondition()

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()
        map.Add("DEL_KB", "DEL_KB")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO", "GYO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INOUT_KB", "INOUT_KB")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("INKA_CTL_NO_L", "INKA_CTL_NO_L")
        map.Add("INKA_CTL_NO_M", "INKA_CTL_NO_M")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("OUTKA_CTL_NO_CHU", "OUTKA_CTL_NO_CHU")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("SR_DEN_NO", "SR_DEN_NO")
        map.Add("HIS_NO", "HIS_NO")
        map.Add("MEI_NO_P", "MEI_NO_P")
        map.Add("MEI_NO_A", "MEI_NO_A")
        map.Add("JYUCHU_GOODS_CD", "JYUCHU_GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("GOODS_KANA1", "GOODS_KANA1")
        map.Add("GOODS_KANA2", "GOODS_KANA2")
        map.Add("NISUGATA_CD", "NISUGATA_CD")
        map.Add("IRISUU", "IRISUU")
        map.Add("LOT_NO_P", "LOT_NO_P")
        map.Add("LOT_NO_A", "LOT_NO_A")
        map.Add("SURY_TANI_CD", "SURY_TANI_CD")
        map.Add("SURY_REQ", "SURY_REQ")
        map.Add("SURY_RPT", "SURY_RPT")
        map.Add("MEI_REM1", "MEI_REM1")
        map.Add("MEI_REM2", "MEI_REM2")
        map.Add("RECORD_STATUS", "RECORD_STATUS")
        map.Add("JISSEKI_SHORI_FLG", "JISSEKI_SHORI_FLG")
        map.Add("JISSEKI_USER", "JISSEKI_USER")
        map.Add("JISSEKI_DATE", "JISSEKI_DATE")
        map.Add("JISSEKI_TIME", "JISSEKI_TIME")
        map.Add("SEND_USER", "SEND_USER")
        map.Add("SEND_DATE", "SEND_DATE")
        map.Add("SEND_TIME", "SEND_TIME")
        map.Add("DELETE_USER", "DELETE_USER")
        map.Add("DELETE_DATE", "DELETE_DATE")
        map.Add("DELETE_TIME", "DELETE_TIME")
        map.Add("DELETE_EDI_NO", "DELETE_EDI_NO")
        map.Add("DELETE_EDI_NO_CHU", "DELETE_EDI_NO_CHU")
        map.Add("UPD_USER", "UPD_USER")
        map.Add("UPD_DATE", "UPD_DATE")
        map.Add("UPD_TIME", "UPD_TIME")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("ORDER_NO", "ORDER_NO")
        map.Add("JYUCHU_NO", "JYUCHU_NO")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.OUT_DTL_EDI)

        '取得件数の設定
        MyBase.SetResultCount(ds.Tables(TABLE_NM.OUT_DTL_EDI).Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 出荷登録処理：取得：ＪＮＣＥＤＩ受信データ(明細)：条件
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OutkaSaveSelectDtlSetCondition()

        Dim whereStr As String = String.Empty

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件
        Me._StrSql.Append(" WHERE" & vbNewLine)
        With Me._Row
            '固定条件
            Me._StrSql.Append(" HED.DATA_KIND = '4001'" & vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.NRS_BR_CD = @NRS_BR_CD" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            'EDI管理番号L
            whereStr = .Item("EDI_CTL_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.EDI_CTL_NO = @EDI_CTL_NO" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", whereStr, DBDataType.CHAR))
            End If

            '積み合せ番号【報告】
            whereStr = .Item("COMBI_NO_A").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.COMBI_NO_A = @COMBI_NO_A" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COMBI_NO_A", whereStr, DBDataType.CHAR))
            End If

            '並び
            Me._StrSql.Append(" ORDER BY" & vbNewLine)
            Me._StrSql.Append(" DTL.EDI_CTL_NO," & vbNewLine)
            Me._StrSql.Append(" DTL.EDI_CTL_NO_CHU" & vbNewLine)
        End With

    End Sub

    ''' <summary>
    ''' 出荷登録処理：登録：ＥＤＩ出荷データＬ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OutkaSaveInsertEdiL(ByVal ds As DataSet) As DataSet

        'カレントインデックスを取得
        Dim dtIdx As Integer = GetCurrentIndex(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_EDI_L)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(dtIdx)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_INSERT_OUTKAEDI_L_OUTKA_SAVE)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("DEL_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", .Item("OUTKA_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_KB", .Item("OUTKA_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", .Item("SYUBETU_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NAIGAI_KB", .Item("NAIGAI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_STATE_KB", .Item("OUTKA_STATE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKAHOKOKU_YN", .Item("OUTKAHOKOKU_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PICK_KB", .Item("PICK_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_NM", .Item("NRS_BR_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_NM", .Item("WH_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", .Item("OUTKO_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", .Item("ARR_PLAN_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", .Item("HOKOKU_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", .Item("TOUKI_HOKAN_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_L", .Item("CUST_NM_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM_M", .Item("CUST_NM_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", .Item("SHIP_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", .Item("SHIP_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_NM_L", .Item("SHIP_NM_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIP_NM_M", .Item("SHIP_NM_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DEST_CD", .Item("EDI_DEST_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_ZIP", .Item("DEST_ZIP").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", .Item("DEST_AD_1").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", .Item("DEST_AD_2").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", .Item("DEST_AD_3").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_4", .Item("DEST_AD_4").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_5", .Item("DEST_AD_5").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_TEL", .Item("DEST_TEL").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_FAX", .Item("DEST_FAX").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_MAIL", .Item("DEST_MAIL").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", .Item("DEST_JIS_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", .Item("SP_NHS_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", .Item("CUST_ORD_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", .Item("BUYER_ORD_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_MOTO_KB", .Item("UNSO_MOTO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", .Item("UNSO_TEHAI_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYARYO_KB", .Item("SYARYO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Item("UNSO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NM", .Item("UNSO_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", .Item("UNSO_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_BR_NM", .Item("UNSO_BR_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", .Item("UNCHIN_TARIFF_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXTC_TARIFF_CD", .Item("EXTC_TARIFF_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ATT", .Item("UNSO_ATT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DENP_YN", .Item("DENP_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PC_KB", .Item("PC_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_YN", .Item("UNCHIN_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", .Item("NIYAKU_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_FLAG", .Item("OUT_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", .Item("AKAKURO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", .Item("JISSEKI_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", .Item("JISSEKI_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", .Item("JISSEKI_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", .Item("JISSEKI_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N01", .Item("FREE_N01").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N02", .Item("FREE_N02").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N03", .Item("FREE_N03").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N04", .Item("FREE_N04").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N05", .Item("FREE_N05").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N06", .Item("FREE_N06").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N07", .Item("FREE_N07").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N08", .Item("FREE_N08").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N09", .Item("FREE_N09").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N10", .Item("FREE_N10").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C01", .Item("FREE_C01").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C02", .Item("FREE_C02").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C03", .Item("FREE_C03").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C04", .Item("FREE_C04").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C05", .Item("FREE_C05").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C06", .Item("FREE_C06").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C07", .Item("FREE_C07").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C08", .Item("FREE_C08").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C09", .Item("FREE_C09").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C10", .Item("FREE_C10").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C11", .Item("FREE_C11").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C12", .Item("FREE_C12").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C13", .Item("FREE_C13").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C14", .Item("FREE_C14").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C15", .Item("FREE_C15").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C16", .Item("FREE_C16").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C17", .Item("FREE_C17").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C18", .Item("FREE_C18").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C19", .Item("FREE_C19").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C20", .Item("FREE_C20").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C21", .Item("FREE_C21").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C22", .Item("FREE_C22").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C23", .Item("FREE_C23").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C24", .Item("FREE_C24").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C25", .Item("FREE_C25").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C26", .Item("FREE_C26").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C27", .Item("FREE_C27").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C28", .Item("FREE_C28").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C29", .Item("FREE_C29").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C30", .Item("FREE_C30").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_USER", .Item("CRT_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_TIME", .Item("CRT_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", .Item("UPD_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", .Item("UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", .Item("UPD_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDIT_FLAG", .Item("EDIT_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATCHING_FLAG", .Item("MATCHING_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 出荷登録処理：登録：ＥＤＩ出荷データＭ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OutkaSaveInsertEdiM(ByVal ds As DataSet) As DataSet

        'カレントインデックスを取得
        Dim dtIdx As Integer = GetCurrentIndex(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_EDI_M)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(dtIdx)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_INSERT_OUTKAEDI_M_OUTKA_SAVE)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("DEL_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", .Item("EDI_CTL_NO_CHU").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", .Item("OUTKA_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO_CHU", .Item("OUTKA_CTL_NO_CHU").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", .Item("CUST_ORD_NO_DTL").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", .Item("BUYER_ORD_NO_DTL").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_GOODS_CD", .Item("CUST_GOODS_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_GOODS_CD", .Item("NRS_GOODS_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RSV_NO", .Item("RSV_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", .Item("SERIAL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ALCTD_KB", .Item("ALCTD_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", .Item("OUTKA_PKG_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU", .Item("OUTKA_HASU").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_QT", .Item("OUTKA_QT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", .Item("OUTKA_TTL_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", .Item("OUTKA_TTL_QT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KB_UT", .Item("KB_UT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@QT_UT", .Item("QT_UT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_NB", .Item("PKG_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PKG_UT", .Item("PKG_UT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO_KB", .Item("ONDO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME", .Item("IRIME").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BETU_WT", .Item("BETU_WT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUT_KB", .Item("OUT_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@AKAKURO_KB", .Item("AKAKURO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_FLAG", .Item("JISSEKI_FLAG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", .Item("JISSEKI_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", .Item("JISSEKI_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", .Item("JISSEKI_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SET_KB", .Item("SET_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N01", .Item("FREE_N01").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N02", .Item("FREE_N02").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N03", .Item("FREE_N03").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N04", .Item("FREE_N04").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N05", .Item("FREE_N05").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N06", .Item("FREE_N06").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N07", .Item("FREE_N07").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N08", .Item("FREE_N08").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N09", .Item("FREE_N09").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_N10", .Item("FREE_N10").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C01", .Item("FREE_C01").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C02", .Item("FREE_C02").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C03", .Item("FREE_C03").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C04", .Item("FREE_C04").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C05", .Item("FREE_C05").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C06", .Item("FREE_C06").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C07", .Item("FREE_C07").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C08", .Item("FREE_C08").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C09", .Item("FREE_C09").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C10", .Item("FREE_C10").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C11", .Item("FREE_C11").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C12", .Item("FREE_C12").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C13", .Item("FREE_C13").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C14", .Item("FREE_C14").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C15", .Item("FREE_C15").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C16", .Item("FREE_C16").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C17", .Item("FREE_C17").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C18", .Item("FREE_C18").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C19", .Item("FREE_C19").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C20", .Item("FREE_C20").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C21", .Item("FREE_C21").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C22", .Item("FREE_C22").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C23", .Item("FREE_C23").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C24", .Item("FREE_C24").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C25", .Item("FREE_C25").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C26", .Item("FREE_C26").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C27", .Item("FREE_C27").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C28", .Item("FREE_C28").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C29", .Item("FREE_C29").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FREE_C30", .Item("FREE_C30").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_USER", .Item("CRT_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_TIME", .Item("CRT_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", .Item("UPD_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", .Item("UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", .Item("UPD_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_L", .Item("SCM_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SCM_CTL_NO_M", .Item("SCM_CTL_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 出荷登録処理：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OutkaSaveUpdateHed(ByVal ds As DataSet) As DataSet

        'カレントインデックスを取得
        Dim dtIdx As Integer = GetCurrentIndex(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_HED)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(dtIdx)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_UPDATE_HED_OUTKA_SAVE)

        '条件およびパラメータの設定
        Call Me.OutkaSaveUpdateHedSetCondition()

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 出荷登録処理：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)：条件
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OutkaSaveUpdateHedSetCondition()

        Dim whereStr As String = String.Empty

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '更新
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_SYS_FLG", .Item("NRS_SYS_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        End With

        '条件
        Me._StrSql.Append(" WHERE" & vbNewLine)
        With Me._Row
            '固定条件
            Me._StrSql.Append(" DATA_KIND = '4001'" & vbNewLine)

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND NRS_BR_CD = @NRS_BR_CD" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            'EDI管理番号L
            whereStr = .Item("EDI_CTL_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND EDI_CTL_NO = @EDI_CTL_NO" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", whereStr, DBDataType.CHAR))
            End If

            '積み合せ番号【報告】
            whereStr = .Item("COMBI_NO_A").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND COMBI_NO_A = @COMBI_NO_A" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COMBI_NO_A", whereStr, DBDataType.CHAR))
            End If
        End With

    End Sub

#End Region

#Region "まとめ指示処理"

    ''' <summary>
    ''' まとめ指示処理：排他：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function MtmSaveExitHed(ByVal ds As DataSet) As DataSet

        'カレントインデックスを取得
        Dim dtIdx As Integer = GetCurrentIndex(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_HED)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(dtIdx)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_EXIT_HED_MTM_SAVE)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_KIND", .Item("DATA_KIND").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得件数による排他チェック
        reader.Read()
        If Convert.ToInt32(reader("SELECT_CNT")) < 1 Then
            MyBase.SetMessage("E011")
        End If
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' まとめ指示処理：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function MtmSaveUpdateHed(ByVal ds As DataSet) As DataSet

        'カレントインデックスを取得
        Dim dtIdx As Integer = GetCurrentIndex(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_HED)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(dtIdx)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_UPDATE_HED_MTM_SAVE)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COMBI_NO_A", .Item("COMBI_NO_A").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_KIND", .Item("DATA_KIND").ToString(), DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

#End Region

#Region "まとめ解除処理"

    ''' <summary>
    ''' まとめ解除処理：排他：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function MtmCancelExitHed(ByVal ds As DataSet) As DataSet

        'まとめ指示処理と同様につき利用
        Return MtmSaveExitHed(ds)

    End Function

    ''' <summary>
    ''' まとめ指示処理：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function MtmCancelUpdateHed(ByVal ds As DataSet) As DataSet

        'まとめ指示処理と同様につき利用
        Return MtmSaveUpdateHed(ds)

    End Function

#End Region

#Region "送信要求処理"

    ''' <summary>
    ''' 送信要求処理：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SndReqUpdateHed(ByVal ds As DataSet) As DataSet

        'カレントインデックスを取得
        Dim dtIdx As Integer = GetCurrentIndex(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_HED)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(dtIdx)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_UPDATE_HED_SND_REQ)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RTN_FLG", .Item("RTN_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SR_DEN_NO", .Item("SR_DEN_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_KIND", .Item("DATA_KIND").ToString(), DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

#If True Then   'ADD 2020/08/31 013087   【LMS】JNC EDI　改修
    ''' <summary>
    ''' 送信要求処理：更新：ＪＮＣＥＤＩ受信データ(ヘッダ運送ー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SndReqUpdateHedUnso(ByVal ds As DataSet) As DataSet

        'カレントインデックスを取得
        Dim dtIdx As Integer = GetCurrentIndex(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_HED)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(dtIdx)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_UPDATE_HED_SND_REQ_UNSO)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RTN_FLG", .Item("RTN_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_EDI_CTL_NO", .Item("UNSO_EDI_CTL_NO").ToString(), DBDataType.CHAR))
            '   Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_KIND", .Item("DATA_KIND").ToString(), DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function
#End If
#End Region

#Region "送信取消処理"

    ''' <summary>
    ''' 送信取消処理：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SndCancelUpdateHed(ByVal ds As DataSet) As DataSet

        '送信要求処理と同様につき利用
        Return SndReqUpdateHed(ds)

    End Function

#End Region

#Region "まとめ候補検索処理"

    ''' <summary>
    ''' まとめ候補検索処理：件数
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function MtmSearchCount(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_SEARCH)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_SELECT_COUNT_MTMSEARCH & LMI511DAC.SQL_SELECT_FROM_MTMSEARCH)

        '条件およびパラメータの設定
        Call Me.MtmSearchSelectSetCondition()

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' まとめ候補検索処理：取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function MtmSearchSelect(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_SEARCH)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_SELECT_DATA_MTMSEARCH & LMI511DAC.SQL_SELECT_FROM_MTMSEARCH)

        '条件およびパラメータの設定
        Call Me.MtmSearchSelectSetCondition()

        '並びの設定
        Call Me.MtmSearchSelectSetOrder()

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO", "GYO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INOUT_KB", "INOUT_KB")
        map.Add("DATA_KIND", "DATA_KIND")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("INKA_CTL_NO_L", "INKA_CTL_NO_L")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("RB_KBN", "RB_KBN")
        map.Add("MOD_KBN", "MOD_KBN")
        map.Add("RTN_FLG", "RTN_FLG")
        map.Add("SND_CANCEL_FLG", "SND_CANCEL_FLG")
        map.Add("PRTFLG", "PRTFLG")
        map.Add("PRTFLG_SUB", "PRTFLG_SUB")
        map.Add("NRS_SYS_FLG", "NRS_SYS_FLG")
        map.Add("COMBI_NO_A", "COMBI_NO_A")
        map.Add("UNSO_REQ_YN", "UNSO_REQ_YN")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_NM", "DEST_AD_NM")
        map.Add("OUTKA_POSI_BU_CD_PA", "OUTKA_POSI_BU_CD_PA")
        map.Add("DEST_BU_CD", "DEST_BU_CD")
        map.Add("CAR_NO_A", "CAR_NO_A")
        map.Add("TUMI_SU", "TUMI_SU")
        map.Add("OUTKA_DATE_A", "OUTKA_DATE_A")
        map.Add("ARRIVAL_DATE_A", "ARRIVAL_DATE_A")
        map.Add("ACT_UNSO_CD", "ACT_UNSO_CD")
        map.Add("UNSO_CD_MEMO", "UNSO_CD_MEMO")
        map.Add("UNSO_ROUTE_A", "UNSO_ROUTE_A")
        map.Add("INKO_DATE_A", "INKO_DATE_A")
        map.Add("PRINT_NO", "PRINT_NO")
        map.Add("SR_DEN_NO", "SR_DEN_NO")
        map.Add("JYUCHU_NO", "JYUCHU_NO")
        map.Add("ORDER_NO", "ORDER_NO")
        map.Add("HIS_NO", "HIS_NO")
        map.Add("OLD_DATA_FLG", "OLD_DATA_FLG")
        map.Add("SYS_UPD_DATE_HED", "SYS_UPD_DATE_HED")
        map.Add("SYS_UPD_TIME_HED", "SYS_UPD_TIME_HED")
        map.Add("JYUCHU_GOODS_CD", "JYUCHU_GOODS_CD")
        map.Add("GOODS_KANA2", "GOODS_KANA2")
        map.Add("SURY_REQ", "SURY_REQ")
        map.Add("SURY_TANI_CD", "SURY_TANI_CD")
        map.Add("SURY_RPT", "SURY_RPT")
        map.Add("SYS_UPD_DATE_DTL", "SYS_UPD_DATE_DTL")
        map.Add("SYS_UPD_TIME_DTL", "SYS_UPD_TIME_DTL")
        map.Add("EDI_CTL_NO_UHD", "EDI_CTL_NO_UHD")
        map.Add("SYS_UPD_DATE_UHD", "SYS_UPD_DATE_UHD")
        map.Add("SYS_UPD_TIME_UHD", "SYS_UPD_TIME_UHD")
        map.Add("EDI_CTL_NO_CHU_UDL", "EDI_CTL_NO_CHU_UDL")
        map.Add("SURY_RPT_UDL", "SURY_RPT_UDL")
        map.Add("SYS_UPD_DATE_UDL", "SYS_UPD_DATE_UDL")
        map.Add("SYS_UPD_TIME_UDL", "SYS_UPD_TIME_UDL")
        map.Add("OUTKA_POSI_BU_NM", "OUTKA_POSI_BU_NM")
        map.Add("DEST_BU_NM", "DEST_BU_NM")
        map.Add("RB_KBN_NM", "RB_KBN_NM")
        map.Add("MOD_KBN_NM", "MOD_KBN_NM")
        map.Add("RTN_FLG_NM", "RTN_FLG_NM")
        map.Add("SND_CANCEL_FLG_NM", "SND_CANCEL_FLG_NM")
        map.Add("PRTFLG_NM", "PRTFLG_NM")
        map.Add("PRTFLG_SUB_NM", "PRTFLG_SUB_NM")
        map.Add("ACT_UNSO_NM", "ACT_UNSO_NM")
        map.Add("UNSO_ROUTE_NM", "UNSO_ROUTE_NM")
        map.Add("NRS_SYS_FLG_NM", "NRS_SYS_FLG_NM")
        map.Add("UNSO_REQ_YN_NM", "UNSO_REQ_YN_NM")
        map.Add("COMBI", "COMBI")
        map.Add("SORT_OUTKA_DATE", "SORT_OUTKA_DATE")
        map.Add("SORT_INKO_DATE", "SORT_INKO_DATE")
        map.Add("SORT_DEST_BU_CD", "SORT_DEST_BU_CD")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.OUT)

        '取得件数の設定
        MyBase.SetResultCount(ds.Tables(TABLE_NM.OUT).Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' まとめ候補検索処理：条件
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MtmSearchSelectSetCondition()

        Dim whereStr As String = String.Empty

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件
        Me._StrSql.Append(" WHERE" & vbNewLine)
        With Me._Row
            '固定条件
            Me._StrSql.Append(" HED1.SYS_DEL_FLG = '0'" & vbNewLine)
            Me._StrSql.Append(" AND HED1.OLD_DATA_FLG = ''" & vbNewLine)
            Me._StrSql.Append(" AND HED1.DATA_KIND = '4001'" & vbNewLine)

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.NRS_BR_CD = @NRS_BR_CD" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.CUST_CD_L = @CUST_CD_L" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.CUST_CD_M = @CUST_CD_M" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '出荷先
            whereStr = .Item("OUTKA_POSI_BU_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.OUTKA_POSI_BU_CD_PA = @OUTKA_POSI_BU_CD " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_POSI_BU_CD", whereStr, DBDataType.NVARCHAR))
            End If

            'EDI取込日FROM
            whereStr = .Item("EDI_DATE_FROM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.CRT_DATE >= @EDI_DATE_FROM " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DATE_FROM", whereStr, DBDataType.NVARCHAR))
            End If

            'EDI取込日TO
            whereStr = .Item("EDI_DATE_TO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.CRT_DATE <= @EDI_DATE_TO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DATE_TO", whereStr, DBDataType.NVARCHAR))
            End If

            '検索日FROM
            whereStr = .Item("SEARCH_DATE_FROM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.OUTKA_DATE_A >= @SEARCH_DATE_FROM " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_FROM", whereStr, DBDataType.NVARCHAR))
            End If

            '検索日TO
            whereStr = .Item("SEARCH_DATE_TO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.OUTKA_DATE_A <= @SEARCH_DATE_TO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_TO", whereStr, DBDataType.NVARCHAR))
            End If

            '赤黒
            whereStr = .Item("RB_KBN").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.RB_KBN = @RB_KBN" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RB_KBN", whereStr, DBDataType.NVARCHAR))
            End If

            '変更
            whereStr = .Item("MOD_KBN").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.MOD_KBN = @MOD_KBN" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOD_KBN", whereStr, DBDataType.NVARCHAR))
            End If

            '報告
            whereStr = .Item("RTN_FLG").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.RTN_FLG = @RTN_FLG" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RTN_FLG", whereStr, DBDataType.NVARCHAR))
            End If

            '送信訂正
            whereStr = .Item("SND_CANCEL_FLG").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.SND_CANCEL_FLG = @SND_CANCEL_FLG" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SND_CANCEL_FLG", whereStr, DBDataType.NVARCHAR))
            End If

            '印刷
            whereStr = .Item("PRTFLG").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.PRTFLG = @PRTFLG" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", whereStr, DBDataType.NVARCHAR))
            End If

            '専門印刷
            whereStr = .Item("PRTFLG_SUB").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.PRTFLG_SUB = @PRTFLG_SUB" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG_SUB", whereStr, DBDataType.NVARCHAR))
            End If

            '取込
            whereStr = .Item("NRS_SYS_FLG").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.NRS_SYS_FLG = @NRS_SYS_FLG" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_SYS_FLG", whereStr, DBDataType.NVARCHAR))
            End If

            '積合
            whereStr = .Item("COMBI").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.COMBI_NO_A " & IIf(whereStr = UMU_FLG.ARI, "<> ''", "= ''").ToString() & vbNewLine)
            End If

            '運送
            whereStr = .Item("UNSO_REQ_YN").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.UNSO_REQ_YN = @UNSO_REQ_YN" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_REQ_YN", whereStr, DBDataType.NVARCHAR))
            End If

            '伝票番号
            whereStr = .Item("SR_DEN_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.SR_DEN_NO LIKE @SR_DEN_NO" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SR_DEN_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.DEST_NM LIKE @DEST_NM" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先住所
            whereStr = .Item("DEST_AD_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.DEST_AD_NM LIKE @DEST_AD_NM" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '運送会社名
            whereStr = .Item("ACT_UNSO_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.ACT_UNSO_CD = @ACT_UNSO_CD" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ACT_UNSO_CD", whereStr, DBDataType.NVARCHAR))
            End If

            '運送会社名(控)
            whereStr = .Item("UNSO_CD_MEMO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.UNSO_CD_MEMO = @UNSO_CD_MEMO" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD_MEMO", whereStr, DBDataType.NVARCHAR))
            End If

            '商品名
            whereStr = .Item("GOODS_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND (MEI.EDI_CTL_NO_CHU = '001'" & vbNewLine)
                Me._StrSql.Append("      AND MEI.GOODS_KANA2 LIKE @GOODS_NM)" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '車両番号
            whereStr = .Item("CAR_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.CAR_NO_A LIKE @CAR_NO" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '受注番号
            whereStr = .Item("JYUCHU_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED1.JYUCHU_NO LIKE @JYUCHU_NO" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JYUCHU_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If
#If True Then   'ADD 2020/08/31 013087   【LMS】JNC EDI　改修
            '運送報告
            whereStr = .Item("UNSO_RTN_FLG").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND UHD.RTN_FLG = @UNSO_RTN_FLG" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RTN_FLG", whereStr, DBDataType.NVARCHAR))
            End If

            '運送手段
            whereStr = .Item("UNSO_ROUTE_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.UNSO_ROUTE_A = @UNSO_ROUTE_CD" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ROUTE_CD", whereStr, DBDataType.NVARCHAR))
            End If
#End If
        End With

    End Sub

    ''' <summary>
    ''' まとめ候補検索処理：並び
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MtmSearchSelectSetOrder()

        '並び
        Me._StrSql.Append(" ORDER BY" & vbNewLine)
        Me._StrSql.Append(" HED1.DEST_CD," & vbNewLine)
        Me._StrSql.Append(" HED1.EDI_CTL_NO," & vbNewLine)
        Me._StrSql.Append(" MEI.EDI_CTL_NO_CHU" & vbNewLine)

    End Sub

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 検索処理：件数
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SearchCount(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_SEARCH)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_SELECT_COUNT_SEARCH & LMI511DAC.SQL_SELECT_FROM_SEARCH)

        '条件およびパラメータの設定
        Call Me.SearchSelectSetCondition()

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理：取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SearchSelect(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_SEARCH)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_SELECT_DATA_SEARCH & LMI511DAC.SQL_SELECT_FROM_SEARCH)

        '条件およびパラメータの設定
        Call Me.SearchSelectSetCondition()

        '並びの設定
        Call Me.SearchSelectSetOrder()

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO", "GYO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INOUT_KB", "INOUT_KB")
        map.Add("DATA_KIND", "DATA_KIND")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("INKA_CTL_NO_L", "INKA_CTL_NO_L")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("RB_KBN", "RB_KBN")
        map.Add("MOD_KBN", "MOD_KBN")
        map.Add("RTN_FLG", "RTN_FLG")
        map.Add("SND_CANCEL_FLG", "SND_CANCEL_FLG")
        map.Add("PRTFLG", "PRTFLG")
        map.Add("PRTFLG_SUB", "PRTFLG_SUB")
        map.Add("NRS_SYS_FLG", "NRS_SYS_FLG")
        map.Add("COMBI_NO_A", "COMBI_NO_A")
        map.Add("UNSO_REQ_YN", "UNSO_REQ_YN")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_AD_NM", "DEST_AD_NM")
        map.Add("OUTKA_POSI_BU_CD_PA", "OUTKA_POSI_BU_CD_PA")
        map.Add("DEST_BU_CD", "DEST_BU_CD")
        map.Add("CAR_NO_A", "CAR_NO_A")
        map.Add("TUMI_SU", "TUMI_SU")
        map.Add("OUTKA_DATE_A", "OUTKA_DATE_A")
        map.Add("ARRIVAL_DATE_A", "ARRIVAL_DATE_A")
        map.Add("ACT_UNSO_CD", "ACT_UNSO_CD")
        map.Add("UNSO_CD_MEMO", "UNSO_CD_MEMO")
        map.Add("UNSO_ROUTE_A", "UNSO_ROUTE_A")
        map.Add("INKO_DATE_A", "INKO_DATE_A")
        map.Add("PRINT_NO", "PRINT_NO")
        map.Add("SR_DEN_NO", "SR_DEN_NO")
        map.Add("JYUCHU_NO", "JYUCHU_NO")
        map.Add("ORDER_NO", "ORDER_NO")
        map.Add("DELIVERY_NM", "DELIVERY_NM")
        map.Add("INV_REM_NM", "INV_REM_NM")
        map.Add("HIS_NO", "HIS_NO")
        map.Add("OLD_DATA_FLG", "OLD_DATA_FLG")
        map.Add("SYS_UPD_DATE_HED", "SYS_UPD_DATE_HED")
        map.Add("SYS_UPD_TIME_HED", "SYS_UPD_TIME_HED")
        map.Add("JYUCHU_GOODS_CD", "JYUCHU_GOODS_CD")
        map.Add("GOODS_KANA2", "GOODS_KANA2")
        map.Add("SURY_REQ", "SURY_REQ")
        map.Add("SURY_TANI_CD", "SURY_TANI_CD")
        map.Add("SURY_RPT", "SURY_RPT")
        map.Add("SYS_UPD_DATE_DTL", "SYS_UPD_DATE_DTL")
        map.Add("SYS_UPD_TIME_DTL", "SYS_UPD_TIME_DTL")
        map.Add("EDI_CTL_NO_UHD", "EDI_CTL_NO_UHD")
        map.Add("SYS_UPD_DATE_UHD", "SYS_UPD_DATE_UHD")
        map.Add("SYS_UPD_TIME_UHD", "SYS_UPD_TIME_UHD")
        map.Add("EDI_CTL_NO_CHU_UDL", "EDI_CTL_NO_CHU_UDL")
        map.Add("SURY_RPT_UDL", "SURY_RPT_UDL")
        map.Add("SYS_UPD_DATE_UDL", "SYS_UPD_DATE_UDL")
        map.Add("SYS_UPD_TIME_UDL", "SYS_UPD_TIME_UDL")
        map.Add("OUTKA_POSI_BU_NM", "OUTKA_POSI_BU_NM")
        map.Add("DEST_BU_NM", "DEST_BU_NM")
        map.Add("RB_KBN_NM", "RB_KBN_NM")
        map.Add("MOD_KBN_NM", "MOD_KBN_NM")
        map.Add("RTN_FLG_NM", "RTN_FLG_NM")
        map.Add("SND_CANCEL_FLG_NM", "SND_CANCEL_FLG_NM")
        map.Add("PRTFLG_NM", "PRTFLG_NM")
        map.Add("PRTFLG_SUB_NM", "PRTFLG_SUB_NM")
        map.Add("ACT_UNSO_NM", "ACT_UNSO_NM")
        map.Add("UNSO_ROUTE_NM", "UNSO_ROUTE_NM")
        map.Add("NRS_SYS_FLG_NM", "NRS_SYS_FLG_NM")
        map.Add("UNSO_REQ_YN_NM", "UNSO_REQ_YN_NM")
        map.Add("COMBI", "COMBI")
        map.Add("SORT_OUTKA_DATE", "SORT_OUTKA_DATE")
        map.Add("SORT_INKO_DATE", "SORT_INKO_DATE")
        map.Add("SORT_DEST_BU_CD", "SORT_DEST_BU_CD")
#If True Then   'ADD 2020/08/27 013087   【LMS】JNC EDI　改修
        map.Add("UNSO_EDI_CTL_NO", "UNSO_EDI_CTL_NO")
        map.Add("UNSO_RTN_FLG", "UNSO_RTN_FLG")
        map.Add("UNSO_RTN_FLG_NM", "UNSO_RTN_FLG_NM")
#End If

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.OUT)

        '取得件数の設定
        MyBase.SetResultCount(ds.Tables(TABLE_NM.OUT).Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理：条件
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SearchSelectSetCondition()

        Dim whereStr As String = String.Empty

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件
        Me._StrSql.Append(" WHERE" & vbNewLine)
        With Me._Row
            '固定条件
            Me._StrSql.Append(" HED.SYS_DEL_FLG = '0'" & vbNewLine)
            Me._StrSql.Append(" AND HED.OLD_DATA_FLG = ''" & vbNewLine)

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.NRS_BR_CD = @NRS_BR_CD" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(大)
            whereStr = .Item("CUST_CD_L").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.CUST_CD_L = @CUST_CD_L" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
            End If

            '荷主コード(中)
            whereStr = .Item("CUST_CD_M").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.CUST_CD_M = @CUST_CD_M" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            End If

            '出荷先
            whereStr = .Item("OUTKA_POSI_BU_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.OUTKA_POSI_BU_CD_PA = @OUTKA_POSI_BU_CD " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_POSI_BU_CD", whereStr, DBDataType.NVARCHAR))
            End If

            'EDI取込日FROM
            whereStr = .Item("EDI_DATE_FROM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.CRT_DATE >= @EDI_DATE_FROM " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DATE_FROM", whereStr, DBDataType.NVARCHAR))
            End If

            'EDI取込日TO
            whereStr = .Item("EDI_DATE_TO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.CRT_DATE <= @EDI_DATE_TO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DATE_TO", whereStr, DBDataType.NVARCHAR))
            End If

            '検索日区分（判断に使用）
            Dim searchDateKbn As String = .Item("SEARCH_DATE_KBN").ToString()

            '検索日FROM
            whereStr = .Item("SEARCH_DATE_FROM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED." & IIf(searchDateKbn = "01", "OUTKA_DATE_A", "INKO_DATE_A").ToString() & " >= @SEARCH_DATE_FROM " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_FROM", whereStr, DBDataType.NVARCHAR))
            End If

            '検索日TO
            whereStr = .Item("SEARCH_DATE_TO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED." & IIf(searchDateKbn = "01", "OUTKA_DATE_A", "INKO_DATE_A").ToString() & " <= @SEARCH_DATE_TO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_TO", whereStr, DBDataType.NVARCHAR))
            End If

            '入出庫区分
            Dim inoutKb As String = .Item("INOUT_KB").ToString()
            whereStr = IIf(inoutKb = INOUT_KB.OUTKA, DATA_KIND.OUTKA, DATA_KIND.INKA).ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.DATA_KIND = @DATA_KIND " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_KIND", whereStr, DBDataType.NVARCHAR))
            End If

            '赤黒
            whereStr = .Item("RB_KBN").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.RB_KBN = @RB_KBN" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RB_KBN", whereStr, DBDataType.NVARCHAR))
            End If

            '変更
            whereStr = .Item("MOD_KBN").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.MOD_KBN = @MOD_KBN" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOD_KBN", whereStr, DBDataType.NVARCHAR))
            End If

            '報告
            whereStr = .Item("RTN_FLG").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.RTN_FLG = @RTN_FLG" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RTN_FLG", whereStr, DBDataType.NVARCHAR))
            End If

            '送信訂正
            whereStr = .Item("SND_CANCEL_FLG").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.SND_CANCEL_FLG = @SND_CANCEL_FLG" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SND_CANCEL_FLG", whereStr, DBDataType.NVARCHAR))
            End If

            '印刷
            whereStr = .Item("PRTFLG").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.PRTFLG = @PRTFLG" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", whereStr, DBDataType.NVARCHAR))
            End If

            '専門印刷
            whereStr = .Item("PRTFLG_SUB").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.PRTFLG_SUB = @PRTFLG_SUB" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG_SUB", whereStr, DBDataType.NVARCHAR))
            End If

            '取込
            whereStr = .Item("NRS_SYS_FLG").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.NRS_SYS_FLG = @NRS_SYS_FLG" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_SYS_FLG", whereStr, DBDataType.NVARCHAR))
            End If

            '積合
            whereStr = .Item("COMBI").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.COMBI_NO_A " & IIf(whereStr = UMU_FLG.ARI, "<> ''", "= ''").ToString() & vbNewLine)
            End If

            '運送
            whereStr = .Item("UNSO_REQ_YN").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.UNSO_REQ_YN = @UNSO_REQ_YN" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_REQ_YN", whereStr, DBDataType.NVARCHAR))
            End If

            '伝票番号
            whereStr = .Item("SR_DEN_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.SR_DEN_NO LIKE @SR_DEN_NO" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SR_DEN_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.DEST_NM LIKE @DEST_NM" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '届先住所
            whereStr = .Item("DEST_AD_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.DEST_AD_NM LIKE @DEST_AD_NM" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '運送会社名
            whereStr = .Item("ACT_UNSO_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.ACT_UNSO_CD = @ACT_UNSO_CD" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ACT_UNSO_CD", whereStr, DBDataType.NVARCHAR))
            End If

            '運送会社名(控)
            whereStr = .Item("UNSO_CD_MEMO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.UNSO_CD_MEMO = @UNSO_CD_MEMO" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD_MEMO", whereStr, DBDataType.NVARCHAR))
            End If

            '商品名
            whereStr = .Item("GOODS_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND (MEI.EDI_CTL_NO_CHU = '001'" & vbNewLine)
                Me._StrSql.Append("      AND MEI.GOODS_KANA2 LIKE @GOODS_NM)" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '車両番号
            whereStr = .Item("CAR_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.CAR_NO_A LIKE @CAR_NO" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '受注番号
            whereStr = .Item("JYUCHU_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.JYUCHU_NO LIKE @JYUCHU_NO" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JYUCHU_NO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

#If True Then   'ADD 2020/08/31 013087   【LMS】JNC EDI　改修

            '運送報告
            whereStr = .Item("UNSO_RTN_FLG").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND UHD.RTN_FLG = @UNSO_RTN_FLG" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_RTN_FLG", whereStr, DBDataType.NVARCHAR))
            End If
            '
            '運送手段
            whereStr = .Item("UNSO_ROUTE_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND HED.UNSO_ROUTE_A = @UNSO_ROUTE_CD" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ROUTE_CD", whereStr, DBDataType.NVARCHAR))
            End If
#End If

#Region "表示条件"
            '消 × を非表示
            If Not String.IsNullOrEmpty(.Item("HYOJI_KBN_1").ToString()) Then
                Me._StrSql.Append(" AND NOT HED.MOD_KBN = '3'" & vbNewLine)
                Me._StrSql.Append(" AND NOT HED.MOD_KBN = 'E'" & vbNewLine)
            End If

            '赤データ非表示
            If Not String.IsNullOrEmpty(.Item("HYOJI_KBN_2").ToString()) Then
                Me._StrSql.Append(" AND NOT HED.RB_KBN = '1'" & vbNewLine)
            End If
#End Region

#Region "除外条件"
            '商品名A
            whereStr = .Item("GOODS_NM_A").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND NOT MEI.GOODS_KANA2 LIKE @GOODS_NM_A" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_A", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '商品名B
            whereStr = .Item("GOODS_NM_B").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND NOT MEI.GOODS_KANA2 LIKE @GOODS_NM_B" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_B", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '商品名C
            whereStr = .Item("GOODS_NM_C").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append(" AND NOT MEI.GOODS_KANA2 LIKE @GOODS_NM_C" & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM_C", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If
#End Region
        End With
    End Sub


    ''' <summary>
    ''' 検索処理：並び
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SearchSelectSetOrder()

        '入出庫区分（判断に使用）
        Dim inoutKb As String = Me._Row.Item("INOUT_KB").ToString()

        '並び
        Me._StrSql.Append(" ORDER BY" & vbNewLine)
        If inoutKb = "0" Then
            Me._StrSql.Append(" SORT_OUTKA_DATE DESC," & vbNewLine)
            Me._StrSql.Append(" HED.SR_DEN_NO," & vbNewLine)
            Me._StrSql.Append(" HED.DEST_NM," & vbNewLine)
            Me._StrSql.Append(" HED.CRT_DATE," & vbNewLine)
            Me._StrSql.Append(" HED.EDI_CTL_NO," & vbNewLine)
            Me._StrSql.Append(" MEI.EDI_CTL_NO_CHU" & vbNewLine)
        Else
            Me._StrSql.Append(" SORT_INKO_DATE DESC," & vbNewLine)
            Me._StrSql.Append(" SORT_DEST_BU_CD," & vbNewLine)
            Me._StrSql.Append(" HED.SR_DEN_NO," & vbNewLine)
            Me._StrSql.Append(" HED.CRT_DATE," & vbNewLine)
            Me._StrSql.Append(" HED.EDI_CTL_NO," & vbNewLine)
            Me._StrSql.Append(" MEI.EDI_CTL_NO_CHU" & vbNewLine)
        End If

    End Sub

#End Region

#Region "保存処理(編集)"

    ''' <summary>
    ''' 保存処理(編集)：排他：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveEditExitHed(ByVal ds As DataSet) As DataSet

        'カレントインデックスを取得
        Dim dtIdx As Integer = GetCurrentIndex(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_HED)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(dtIdx)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_EXIT_HED_SAVE_EDIT)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_KIND", .Item("DATA_KIND").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得件数による排他チェック
        reader.Read()
        If Convert.ToInt32(reader("SELECT_CNT")) < 1 Then
            MyBase.SetMessage("E011")
        End If
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理(編集)：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveEditUpdateHed(ByVal ds As DataSet) As DataSet

        'カレントインデックスを取得
        Dim dtIdx As Integer = GetCurrentIndex(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_HED)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(dtIdx)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_UPDATE_HED_SAVE_EDIT)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE_A", .Item("OUTKA_DATE_A").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ROUTE_A", .Item("UNSO_ROUTE_A").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_NO_A", .Item("CAR_NO_A").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ACT_UNSO_CD", .Item("ACT_UNSO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_CD_MEMO", .Item("UNSO_CD_MEMO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_KIND", .Item("DATA_KIND").ToString(), DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理(編集)：排他：ＪＮＣＥＤＩ受信データ(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveEditExitDtl(ByVal ds As DataSet) As DataSet

        'カレントインデックスを取得
        Dim dtIdx As Integer = GetCurrentIndex(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_DTL)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(dtIdx)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_EXIT_DTL_SAVE_EDIT)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", .Item("INOUT_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", .Item("EDI_CTL_NO_CHU").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得件数による排他チェック
        reader.Read()
        If Convert.ToInt32(reader("SELECT_CNT")) < 1 Then
            MyBase.SetMessage("E011")
        End If
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理(編集)：更新：ＪＮＣＥＤＩ受信データ(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveEditUpdateDtl(ByVal ds As DataSet) As DataSet

        'カレントインデックスを取得
        Dim dtIdx As Integer = GetCurrentIndex(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_DTL)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(dtIdx)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_UPDATE_DTL_SAVE_EDIT)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SURY_RPT", .Item("SURY_RPT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", .Item("INOUT_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", .Item("EDI_CTL_NO_CHU").ToString(), DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

#End Region

#Region "保存処理(訂正)"

    ''' <summary>
    ''' 保存処理(訂正)：排他：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveRevisionExitHed(ByVal ds As DataSet) As DataSet

        'カレントインデックスを取得
        Dim dtIdx As Integer = GetCurrentIndex(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_HED)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(dtIdx)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_EXIT_HED_SAVE_REVISION)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_KIND", .Item("DATA_KIND").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得件数による排他チェック
        reader.Read()
        If Convert.ToInt32(reader("SELECT_CNT")) < 1 Then
            MyBase.SetMessage("E011")
        End If
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理(訂正)：更新：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveRevisionUpdateHed(ByVal ds As DataSet) As DataSet

        'カレントインデックスを取得
        Dim dtIdx As Integer = GetCurrentIndex(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_HED)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(dtIdx)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_UPDATE_HED_SAVE_REVISION)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SND_CANCEL_FLG", SND_CANCEL_FLG.BLACK, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_KIND", .Item("DATA_KIND").ToString(), DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理(訂正)：取得：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveRevisionSelectHed(ByVal ds As DataSet) As DataSet

        'カレントインデックスを取得
        Dim dtIdx As Integer = GetCurrentIndex(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_HED)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(dtIdx)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_SELECT_HED_SAVE_REVISION)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_KIND", .Item("DATA_KIND").ToString(), DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()
        map.Add("DEL_KB", "DEL_KB")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INOUT_KB", "INOUT_KB")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("INKA_CTL_NO_L", "INKA_CTL_NO_L")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("PRTFLG", "PRTFLG")
        map.Add("PRTFLG_SUB", "PRTFLG_SUB")
        map.Add("CANCEL_FLG", "CANCEL_FLG")
        map.Add("NRS_TANTO", "NRS_TANTO")
        map.Add("DATA_KIND", "DATA_KIND")
        map.Add("SEND_CODE", "SEND_CODE")
        map.Add("SR_DEN_NO", "SR_DEN_NO")
        map.Add("HIS_NO", "HIS_NO")
        map.Add("PROC_YMD", "PROC_YMD")
        map.Add("PROC_TIME", "PROC_TIME")
        map.Add("PROC_NO", "PROC_NO")
        map.Add("SEND_DEN_YMD", "SEND_DEN_YMD")
        map.Add("SEND_DEN_TIME", "SEND_DEN_TIME")
        map.Add("BPID_KKN", "BPID_KKN")
        map.Add("BPID_SUB_KKN", "BPID_SUB_KKN")
        map.Add("BPID_HAN", "BPID_HAN")
        map.Add("RCV_COMP_CD", "RCV_COMP_CD")
        map.Add("SND_COMP_CD", "SND_COMP_CD")
        map.Add("RB_KBN", "RB_KBN")
        map.Add("MOD_KBN", "MOD_KBN")
        map.Add("DATA_KBN", "DATA_KBN")
        map.Add("FAX_KBN", "FAX_KBN")
        map.Add("OUTKA_REQ_KBN", "OUTKA_REQ_KBN")
        map.Add("INKA_P_KBN", "INKA_P_KBN")
        map.Add("OUTKA_SEPT_KBN", "OUTKA_SEPT_KBN")
        map.Add("EM_OUTKA_KBN", "EM_OUTKA_KBN")
        map.Add("UNSO_ROUTE_P", "UNSO_ROUTE_P")
        map.Add("UNSO_ROUTE_A", "UNSO_ROUTE_A")
        map.Add("CAR_KIND_P", "CAR_KIND_P")
        map.Add("CAR_KIND_A", "CAR_KIND_A")
        map.Add("CAR_NO_P", "CAR_NO_P")
        map.Add("CAR_NO_A", "CAR_NO_A")
        map.Add("COMBI_NO_P", "COMBI_NO_P")
        map.Add("COMBI_NO_A", "COMBI_NO_A")
        map.Add("UNSO_REQ_YN", "UNSO_REQ_YN")
        map.Add("DEST_CHK_KBN", "DEST_CHK_KBN")
        map.Add("INKO_DATE_P", "INKO_DATE_P")
        map.Add("INKO_DATE_A", "INKO_DATE_A")
        map.Add("INKO_TIME", "INKO_TIME")
        map.Add("OUTKA_DATE_P", "OUTKA_DATE_P")
        map.Add("OUTKA_DATE_A", "OUTKA_DATE_A")
        map.Add("OUTKA_TIME_E", "OUTKA_TIME_E")
        map.Add("CARGO_BKG_DATE_P", "CARGO_BKG_DATE_P")
        map.Add("CARGO_BKG_DATE_A", "CARGO_BKG_DATE_A")
        map.Add("ARRIVAL_DATE_P", "ARRIVAL_DATE_P")
        map.Add("ARRIVAL_DATE_A", "ARRIVAL_DATE_A")
        map.Add("ARRIVAL_TIME", "ARRIVAL_TIME")
        map.Add("UNSO_DATE", "UNSO_DATE")
        map.Add("UNSO_TIME", "UNSO_TIME")
        map.Add("ZAI_RPT_DATE", "ZAI_RPT_DATE")
        map.Add("BAILER_CD", "BAILER_CD")
        map.Add("BAILER_NM", "BAILER_NM")
        map.Add("BAILER_BU_CD", "BAILER_BU_CD")
        map.Add("BAILER_BU_NM", "BAILER_BU_NM")
        map.Add("SHIPPER_CD", "SHIPPER_CD")
        map.Add("SHIPPER_NM", "SHIPPER_NM")
        map.Add("SHIPPER_BU_CD", "SHIPPER_BU_CD")
        map.Add("SHIPPER_BU_NM", "SHIPPER_BU_NM")
        map.Add("CONSIGNEE_CD", "CONSIGNEE_CD")
        map.Add("CONSIGNEE_NM", "CONSIGNEE_NM")
        map.Add("CONSIGNEE_BU_CD", "CONSIGNEE_BU_CD")
        map.Add("CONSIGNEE_BU_NM", "CONSIGNEE_BU_NM")
        map.Add("SOKO_PROV_CD", "SOKO_PROV_CD")
        map.Add("SOKO_PROV_NM", "SOKO_PROV_NM")
        map.Add("UNSO_PROV_CD", "UNSO_PROV_CD")
        map.Add("UNSO_PROV_NM", "UNSO_PROV_NM")
        map.Add("ACT_UNSO_CD", "ACT_UNSO_CD")
        map.Add("UNSO_TF_KBN", "UNSO_TF_KBN")
        map.Add("UNSO_F_KBN", "UNSO_F_KBN")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_BU_CD", "DEST_BU_CD")
        map.Add("DEST_BU_NM", "DEST_BU_NM")
        map.Add("DEST_AD_CD", "DEST_AD_CD")
        map.Add("DEST_AD_NM", "DEST_AD_NM")
        map.Add("DEST_YB_NO", "DEST_YB_NO")
        map.Add("DEST_TEL_NO", "DEST_TEL_NO")
        map.Add("DEST_FAX_NO", "DEST_FAX_NO")
        map.Add("DELIVERY_NM", "DELIVERY_NM")
        map.Add("DELIVERY_SAGYO", "DELIVERY_SAGYO")
        map.Add("ORDER_NO", "ORDER_NO")
        map.Add("JYUCHU_NO", "JYUCHU_NO")
        map.Add("PRI_SHOP_CD", "PRI_SHOP_CD")
        map.Add("PRI_SHOP_NM", "PRI_SHOP_NM")
        map.Add("INV_REM_NM", "INV_REM_NM")
        map.Add("INV_REM_KANA", "INV_REM_KANA")
        map.Add("DEN_NO", "DEN_NO")
        map.Add("MEI_DEN_NO", "MEI_DEN_NO")
        map.Add("OUTKA_POSI_CD", "OUTKA_POSI_CD")
        map.Add("OUTKA_POSI_NM", "OUTKA_POSI_NM")
        map.Add("OUTKA_POSI_BU_CD_PA", "OUTKA_POSI_BU_CD_PA")
        map.Add("OUTKA_POSI_BU_CD_NAIBU", "OUTKA_POSI_BU_CD_NAIBU")
        map.Add("OUTKA_POSI_BU_NM_PA", "OUTKA_POSI_BU_NM_PA")
        map.Add("OUTKA_POSI_BU_NM_NAIBU", "OUTKA_POSI_BU_NM_NAIBU")
        map.Add("OUTKA_POSI_AD_CD_PA", "OUTKA_POSI_AD_CD_PA")
        map.Add("OUTKA_POSI_AD_NM_PA", "OUTKA_POSI_AD_NM_PA")
        map.Add("OUTKA_POSI_TEL_NO_PA", "OUTKA_POSI_TEL_NO_PA")
        map.Add("OUTKA_POSI_FAX_NO_PA", "OUTKA_POSI_FAX_NO_PA")
        map.Add("UNSO_JURYO", "UNSO_JURYO")
        map.Add("UNSO_JURYO_FLG", "UNSO_JURYO_FLG")
        map.Add("UNIT_LOAD_CD", "UNIT_LOAD_CD")
        map.Add("UNIT_LOAD_SU", "UNIT_LOAD_SU")
        map.Add("REMARK", "REMARK")
        map.Add("REMARK_KANA", "REMARK_KANA")
        map.Add("HARAI_KBN", "HARAI_KBN")
        map.Add("DATA_TYPE", "DATA_TYPE")
        map.Add("RTN_FLG", "RTN_FLG")
        map.Add("SND_CANCEL_FLG", "SND_CANCEL_FLG")
        map.Add("OLD_DATA_FLG", "OLD_DATA_FLG")
        map.Add("PRINT_NO", "PRINT_NO")
        map.Add("NRS_SYS_FLG", "NRS_SYS_FLG")
        map.Add("OLD_SYS_FLG", "OLD_SYS_FLG")
        map.Add("RTN_FILE_DATE", "RTN_FILE_DATE")
        map.Add("RTN_FILE_TIME", "RTN_FILE_TIME")
        map.Add("RECORD_STATUS", "RECORD_STATUS")
        map.Add("DELETE_USER", "DELETE_USER")
        map.Add("DELETE_DATE", "DELETE_DATE")
        map.Add("DELETE_TIME", "DELETE_TIME")
        map.Add("DELETE_EDI_NO", "DELETE_EDI_NO")
        map.Add("PRT_USER", "PRT_USER")
        map.Add("PRT_DATE", "PRT_DATE")
        map.Add("PRT_TIME", "PRT_TIME")
        map.Add("EDI_USER", "EDI_USER")
        map.Add("EDI_DATE", "EDI_DATE")
        map.Add("EDI_TIME", "EDI_TIME")
        map.Add("OUTKA_DATE", "OUTKA_DATE")
        map.Add("OUTKA_USER", "OUTKA_USER")
        map.Add("OUTKA_TIME", "OUTKA_TIME")
        map.Add("INKA_USER", "INKA_USER")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("INKA_TIME", "INKA_TIME")
        map.Add("UPD_USER", "UPD_USER")
        map.Add("UPD_DATE", "UPD_DATE")
        map.Add("UPD_TIME", "UPD_TIME")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("PRT_FLG", "PRT_FLG")
        map.Add("MATOME_FLG", "MATOME_FLG")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.OUT_HED)

        '取得件数の設定
        MyBase.SetResultCount(ds.Tables(TABLE_NM.OUT_HED).Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理(訂正)：登録：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveRevisionInsertHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_HED)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_INSERT_HED_SAVE_REVISION)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("DEL_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", .Item("INOUT_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", .Item("INKA_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", .Item("OUTKA_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG", .Item("PRTFLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRTFLG_SUB", .Item("PRTFLG_SUB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CANCEL_FLG", .Item("CANCEL_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_TANTO", .Item("NRS_TANTO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_KIND", .Item("DATA_KIND").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_CODE", .Item("SEND_CODE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SR_DEN_NO", .Item("SR_DEN_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIS_NO", .Item("HIS_NO").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROC_YMD", .Item("PROC_YMD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROC_TIME", .Item("PROC_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PROC_NO", .Item("PROC_NO").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DEN_YMD", .Item("SEND_DEN_YMD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DEN_TIME", .Item("SEND_DEN_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BPID_KKN", .Item("BPID_KKN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BPID_SUB_KKN", .Item("BPID_SUB_KKN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BPID_HAN", .Item("BPID_HAN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RCV_COMP_CD", .Item("RCV_COMP_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SND_COMP_CD", .Item("SND_COMP_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RB_KBN", .Item("RB_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOD_KBN", .Item("MOD_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_KBN", .Item("DATA_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FAX_KBN", .Item("FAX_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_REQ_KBN", .Item("OUTKA_REQ_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_P_KBN", .Item("INKA_P_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_SEPT_KBN", .Item("OUTKA_SEPT_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EM_OUTKA_KBN", .Item("EM_OUTKA_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ROUTE_P", .Item("UNSO_ROUTE_P").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_ROUTE_A", .Item("UNSO_ROUTE_A").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_KIND_P", .Item("CAR_KIND_P").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_KIND_A", .Item("CAR_KIND_A").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_NO_P", .Item("CAR_NO_P").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_NO_A", .Item("CAR_NO_A").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COMBI_NO_P", .Item("COMBI_NO_P").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COMBI_NO_A", .Item("COMBI_NO_A").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_REQ_YN", .Item("UNSO_REQ_YN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CHK_KBN", .Item("DEST_CHK_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_DATE_P", .Item("INKO_DATE_P").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_DATE_A", .Item("INKO_DATE_A").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKO_TIME", .Item("INKO_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE_P", .Item("OUTKA_DATE_P").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE_A", .Item("OUTKA_DATE_A").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TIME_E", .Item("OUTKA_TIME_E").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CARGO_BKG_DATE_P", .Item("CARGO_BKG_DATE_P").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CARGO_BKG_DATE_A", .Item("CARGO_BKG_DATE_A").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARRIVAL_DATE_P", .Item("ARRIVAL_DATE_P").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARRIVAL_DATE_A", .Item("ARRIVAL_DATE_A").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARRIVAL_TIME", .Item("ARRIVAL_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_DATE", .Item("UNSO_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_TIME", .Item("UNSO_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZAI_RPT_DATE", .Item("ZAI_RPT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BAILER_CD", .Item("BAILER_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BAILER_NM", .Item("BAILER_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BAILER_BU_CD", .Item("BAILER_BU_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BAILER_BU_NM", .Item("BAILER_BU_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIPPER_CD", .Item("SHIPPER_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIPPER_NM", .Item("SHIPPER_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIPPER_BU_CD", .Item("SHIPPER_BU_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIPPER_BU_NM", .Item("SHIPPER_BU_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CONSIGNEE_CD", .Item("CONSIGNEE_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CONSIGNEE_NM", .Item("CONSIGNEE_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CONSIGNEE_BU_CD", .Item("CONSIGNEE_BU_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CONSIGNEE_BU_NM", .Item("CONSIGNEE_BU_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_PROV_CD", .Item("SOKO_PROV_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SOKO_PROV_NM", .Item("SOKO_PROV_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_PROV_CD", .Item("UNSO_PROV_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_PROV_NM", .Item("UNSO_PROV_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ACT_UNSO_CD", .Item("ACT_UNSO_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_TF_KBN", .Item("UNSO_TF_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_F_KBN", .Item("UNSO_F_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_NM", .Item("DEST_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_BU_CD", .Item("DEST_BU_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_BU_NM", .Item("DEST_BU_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_CD", .Item("DEST_AD_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD_NM", .Item("DEST_AD_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_YB_NO", .Item("DEST_YB_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_TEL_NO", .Item("DEST_TEL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_FAX_NO", .Item("DEST_FAX_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELIVERY_NM", .Item("DELIVERY_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELIVERY_SAGYO", .Item("DELIVERY_SAGYO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_NO", .Item("ORDER_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JYUCHU_NO", .Item("JYUCHU_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRI_SHOP_CD", .Item("PRI_SHOP_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRI_SHOP_NM", .Item("PRI_SHOP_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_REM_NM", .Item("INV_REM_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_REM_KANA", .Item("INV_REM_KANA").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEN_NO", .Item("DEN_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MEI_DEN_NO", .Item("MEI_DEN_NO").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_POSI_CD", .Item("OUTKA_POSI_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_POSI_NM", .Item("OUTKA_POSI_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_POSI_BU_CD_PA", .Item("OUTKA_POSI_BU_CD_PA").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_POSI_BU_CD_NAIBU", .Item("OUTKA_POSI_BU_CD_NAIBU").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_POSI_BU_NM_PA", .Item("OUTKA_POSI_BU_NM_PA").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_POSI_BU_NM_NAIBU", .Item("OUTKA_POSI_BU_NM_NAIBU").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_POSI_AD_CD_PA", .Item("OUTKA_POSI_AD_CD_PA").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_POSI_AD_NM_PA", .Item("OUTKA_POSI_AD_NM_PA").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_POSI_TEL_NO_PA", .Item("OUTKA_POSI_TEL_NO_PA").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_POSI_FAX_NO_PA", .Item("OUTKA_POSI_FAX_NO_PA").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_JURYO", .Item("UNSO_JURYO").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_JURYO_FLG", .Item("UNSO_JURYO_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNIT_LOAD_CD", .Item("UNIT_LOAD_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNIT_LOAD_SU", .Item("UNIT_LOAD_SU").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK_KANA", .Item("REMARK_KANA").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HARAI_KBN", .Item("HARAI_KBN").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DATA_TYPE", .Item("DATA_TYPE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RTN_FLG", .Item("RTN_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SND_CANCEL_FLG", .Item("SND_CANCEL_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OLD_DATA_FLG", .Item("OLD_DATA_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRINT_NO", .Item("PRINT_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_SYS_FLG", .Item("NRS_SYS_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OLD_SYS_FLG", .Item("OLD_SYS_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RTN_FILE_DATE", .Item("RTN_FILE_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RTN_FILE_TIME", .Item("RTN_FILE_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RECORD_STATUS", .Item("RECORD_STATUS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_USER", .Item("DELETE_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", .Item("DELETE_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", .Item("DELETE_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO", .Item("DELETE_EDI_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_USER", .Item("PRT_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_DATE", .Item("PRT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_TIME", .Item("PRT_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_USER", .Item("EDI_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_DATE", .Item("EDI_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_TIME", .Item("EDI_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_USER", .Item("OUTKA_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE", .Item("OUTKA_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_TIME", .Item("OUTKA_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_USER", .Item("INKA_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", .Item("INKA_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_TIME", .Item("INKA_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", .Item("UPD_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", .Item("UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", .Item("UPD_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PRT_FLG", .Item("PRT_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MATOME_FLG", .Item("MATOME_FLG").ToString(), DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理(訂正)：取得：ＪＮＣＥＤＩ受信データ(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveRevisionSelectDtl(ByVal ds As DataSet) As DataSet

        'カレントインデックスを取得
        Dim dtIdx As Integer = GetCurrentIndex(ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_DTL)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(dtIdx)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_SELECT_DTL_SAVE_REVISION)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", .Item("INOUT_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '取得データの格納先をマッピング
        Dim map As Hashtable = New Hashtable()
        map.Add("DEL_KB", "DEL_KB")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("GYO", "GYO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INOUT_KB", "INOUT_KB")
        map.Add("EDI_CTL_NO", "EDI_CTL_NO")
        map.Add("EDI_CTL_NO_CHU", "EDI_CTL_NO_CHU")
        map.Add("INKA_CTL_NO_L", "INKA_CTL_NO_L")
        map.Add("INKA_CTL_NO_M", "INKA_CTL_NO_M")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("OUTKA_CTL_NO_CHU", "OUTKA_CTL_NO_CHU")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("SR_DEN_NO", "SR_DEN_NO")
        map.Add("HIS_NO", "HIS_NO")
        map.Add("MEI_NO_P", "MEI_NO_P")
        map.Add("MEI_NO_A", "MEI_NO_A")
        map.Add("JYUCHU_GOODS_CD", "JYUCHU_GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("GOODS_KANA1", "GOODS_KANA1")
        map.Add("GOODS_KANA2", "GOODS_KANA2")
        map.Add("NISUGATA_CD", "NISUGATA_CD")
        map.Add("IRISUU", "IRISUU")
        map.Add("LOT_NO_P", "LOT_NO_P")
        map.Add("LOT_NO_A", "LOT_NO_A")
        map.Add("SURY_TANI_CD", "SURY_TANI_CD")
        map.Add("SURY_REQ", "SURY_REQ")
        map.Add("SURY_RPT", "SURY_RPT")
        map.Add("MEI_REM1", "MEI_REM1")
        map.Add("MEI_REM2", "MEI_REM2")
        map.Add("RECORD_STATUS", "RECORD_STATUS")
        map.Add("JISSEKI_SHORI_FLG", "JISSEKI_SHORI_FLG")
        map.Add("JISSEKI_USER", "JISSEKI_USER")
        map.Add("JISSEKI_DATE", "JISSEKI_DATE")
        map.Add("JISSEKI_TIME", "JISSEKI_TIME")
        map.Add("SEND_USER", "SEND_USER")
        map.Add("SEND_DATE", "SEND_DATE")
        map.Add("SEND_TIME", "SEND_TIME")
        map.Add("DELETE_USER", "DELETE_USER")
        map.Add("DELETE_DATE", "DELETE_DATE")
        map.Add("DELETE_TIME", "DELETE_TIME")
        map.Add("DELETE_EDI_NO", "DELETE_EDI_NO")
        map.Add("DELETE_EDI_NO_CHU", "DELETE_EDI_NO_CHU")
        map.Add("UPD_USER", "UPD_USER")
        map.Add("UPD_DATE", "UPD_DATE")
        map.Add("UPD_TIME", "UPD_TIME")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
        map.Add("SYS_ENT_PGID", "SYS_ENT_PGID")
        map.Add("SYS_ENT_USER", "SYS_ENT_USER")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
        map.Add("SYS_UPD_USER", "SYS_UPD_USER")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM.OUT_DTL)

        '取得件数の設定
        MyBase.SetResultCount(ds.Tables(TABLE_NM.OUT_DTL).Rows.Count())
        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理(訂正)：登録：ＪＮＣＥＤＩ受信データ(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveRevisionInsertDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(TABLE_NM.IN_DTL)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI511DAC.SQL_INSERT_DTL_SAVE_REVISION)

        'SQLのコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("DEL_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GYO", .Item("GYO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUT_KB", .Item("INOUT_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO", .Item("EDI_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EDI_CTL_NO_CHU", .Item("EDI_CTL_NO_CHU").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_L", .Item("INKA_CTL_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_CTL_NO_M", .Item("INKA_CTL_NO_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", .Item("OUTKA_CTL_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO_CHU", .Item("OUTKA_CTL_NO_CHU").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SR_DEN_NO", .Item("SR_DEN_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HIS_NO", .Item("HIS_NO").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MEI_NO_P", .Item("MEI_NO_P").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MEI_NO_A", .Item("MEI_NO_A").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JYUCHU_GOODS_CD", .Item("JYUCHU_GOODS_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_KANA1", .Item("GOODS_KANA1").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_KANA2", .Item("GOODS_KANA2").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NISUGATA_CD", .Item("NISUGATA_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRISUU", .Item("IRISUU").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO_P", .Item("LOT_NO_P").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO_A", .Item("LOT_NO_A").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SURY_TANI_CD", .Item("SURY_TANI_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SURY_REQ", .Item("SURY_REQ").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SURY_RPT", .Item("SURY_RPT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MEI_REM1", .Item("MEI_REM1").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MEI_REM2", .Item("MEI_REM2").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RECORD_STATUS", .Item("RECORD_STATUS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", .Item("JISSEKI_SHORI_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_USER", .Item("JISSEKI_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_DATE", .Item("JISSEKI_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JISSEKI_TIME", .Item("JISSEKI_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_USER", .Item("SEND_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_DATE", .Item("SEND_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEND_TIME", .Item("SEND_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_USER", .Item("DELETE_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_DATE", .Item("DELETE_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_TIME", .Item("DELETE_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO", .Item("DELETE_EDI_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DELETE_EDI_NO_CHU", .Item("DELETE_EDI_NO_CHU").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_USER", .Item("UPD_USER").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE", .Item("UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME", .Item("UPD_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
        End With

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログ出力
        MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

#End Region

#End Region 'Method

End Class
