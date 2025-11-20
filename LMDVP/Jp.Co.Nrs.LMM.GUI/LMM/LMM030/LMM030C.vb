' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM030C : 作業項目マスタメンテ
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMM030定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM030C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM030IN"
    Public Const TABLE_NM_OUT As String = "LMM030OUT"

    'コンボボックス初期値
    Public Const YN As String = "00"
    Public Const ZEI As String = "01"

    'コンボボックス有無
    Public Const ARI As String = "01"

    '荷主マスタ存在チェックデフォルトコード
    Public Const NINUSHI As String = "00"

    '処理結果メッセージ
    Public Const CUSTCDLMSG As String = "荷主コード(大)"
    Public Const SAGYOCDMSG As String = "作業コード"

    'エラーメッセージ
    Public Const INVTANI As String = "請求単位"
    Public Const TANKA As String = "単価"
    Public Const INVMSG As String = "請求有無が有"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
        INIT
        KENSAKU
        SANSHO
        SHINKI
        HENSHU
        HUKUSHA
        SAKUJO
        HOZON
        MASTEROPEN
        ENTER
        TOJIRU
        DCLICK

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        DETAIL = 0
        NRSBRCD
        CUSTCDL
        CUSTNML
        SAGYOCD
        SAGYONM
        SAGYORYAK
        INVYN
        INVTANI
        KOSUBAI
        SAGYOUP
        ZEIKBN
        SPLRPT
        FLWPYN
        SAGYOREMARK
        TABYN
        WHSAGYONM
        WHSAGYOREMARK
        SAGYOSUBCD
        SAGYOSUBNM
        CUSTSUBCDL
        CUSTSUBNML
        SITUATION
        CRTDATE
        CRTUSER
        UPDUSER
        UPDDATE
        UPDTIME
        SYSDELFLG

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SYS_DEL_NM
        NRS_BR_CD
        NRS_BR_NM
        SAGYO_CD
        SAGYO_NM
        SAGYO_RYAK
        CUST_CD_L
        CUST_NM_L
        INV_YN_NM
        INV_YN
        FLWP_YN_NM
        FLWP_YN
        WH_SAGYO_YN_NM
        WH_SAGYO_YN
        SPL_RPT
        INV_TANI
        KOSU_BAI
        SAGYO_UP
        SAGYO_UP_CURR_CD
        ZEI_KBN
        SAGYO_REMARK
        WH_SAGYO_NM
        WH_SIJI_REMARK
        SAGYO_SUB_CD
        SAGYO_SUB_NM
        CUST_SUB_CD_L
        CUST_SUB_NM_L
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG

    End Enum

End Class
