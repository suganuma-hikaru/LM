' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI540C : オフライン出荷検索(FFEM)
'  作  成  者       :  
' ==========================================================================

''' <summary>
''' LMI540定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI540C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' cmbPrint 表示元区分マスタ(KBN_GROUP_CD = 'S034') 中の本機能での表示対象区分コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Class cmbPrintUseKbnCd
        ''' <summary>
        ''' 依頼書
        ''' </summary>
        Public Const IRAI_SHO As String = "99"

    End Class

    ''' <summary>
    ''' イベント種別用列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer
        TORIKOMI = 1            ' 取込
        SEARCH                  ' 検索
        PRINT                   ' 印刷
    End Enum

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TABLE_NM
        Public Const IN_SEARCH As String = "LMI540IN_SEARCH"
        Public Const IN_IMPORT As String = "LMI540IN_IMPORT"
        Public Const IN_PRINT As String = "LMI540IN_PRINT"
        Public Const OUT As String = "LMI540OUT"
        Public Const TORIKOMI_HED As String = "LMI540_TORIKOMI_HED"
        Public Const TORIKOMI_DTL As String = "LMI540_TORIKOMI_DTL"
    End Class

    ''' <summary>
    ''' タブインデックス用列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex_MAIN
        CMBEIGYO = 0
        CMBWARE
        TXTCUSTCD_L
        TXTCUSTCD_M
        IMEOUTKADATEFROM
        IMEOUTKADATETO
        CMBPRINT
        BTNPRINT
        SPRDETAIL
    End Enum

    ''' <summary>
    ''' スプレッド列インデックス用列挙体
    ''' </summary>
    ''' <remarks>S_SPREADへの状態保存の都合上、列数99を超える定義はNG</remarks>
    Public Enum SprColumnIndex
        DEF = 0
        KEY_NO
        OFFLINE_NO
        SHIZI_KB_NM
        NOHIN_KB_NM
        IRAI_DATE
        IRAI_SYA
        MOTO
        SHUBETSU
        OUTKA_DATE
        ARR_DATE
        ZIP
        DEST_AD
        COMP_NM
        BUSYO_NM
        TANTO_NM
        TEL
        GOODS_NM
        LOT_NO
        INOUTKA_NB
        ONDO
        DOKUGEKI
        HAISO
        SAP_NO
        REMARK
        SHIZI_KB
        NOHIN_KB
        PLANT_CD
        NRS_BR_CD
        WH_CD
        CUST_CD_L
        CUST_CD_M
        SYS_UPD_DATE
        SYS_UPD_TIME
        LAST
    End Enum

    ''' <summary>
    ''' 処理モード用列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum Mode

        ''' <summary>
        ''' 初期モード
        ''' </summary>
        ''' <remarks></remarks>
        INT = 0
        ''' <summary>
        ''' 参照モード
        ''' </summary>
        ''' <remarks></remarks>
        REF

    End Enum

End Class
