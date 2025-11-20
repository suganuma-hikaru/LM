' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM120C : 単価マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================

''' <summary>
''' LMM120定数定義クラス
''' </summary>
''' <remarks></remarks>
Friend Class LMM120C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TABLE_NM_IN As String = "LMM120IN"
    Friend Const TABLE_NM_OUT As String = "LMM120OUT"

    ''' <summary>
    ''' 単価区分【区分マスタ：T005】
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TANKA_KBN_KOSU_DATE As String = "01"     '個数建
    Friend Const TANKA_KBN_SURYO_DATE As String = "02"    '数量建
    Friend Const TANKA_KBN_RIPPO_DATE As String = "03"    '立方建
    Friend Const TANKA_KBN_PALETTO_DATE As String = "04"  'パレット
    Friend Const TANKA_KBN_JYURYO_DATE As String = "05"   '重量建

    ''' <summary>
    ''' 保管料チェック対象種別
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum ChkSeiqtoShubetu As Integer

        HOKAN = 0
        NIYAKU

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum EventShubetsu As Integer

        SHINKI = 0
        HENSHU
        FUKUSHA
        SAKUJO_HUKKATU
        KENSAKU
        MASTEROPEN
        HOZON
        TOJIRU
        DOUBLE_CLICK
        ADD_ROW
        DEL_ROW
        ENTER
        REQUEST             '申請
        APPROVAL            '承認
        REMAND              '差し戻し

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum CtlTabIndex

        CHK_ZENDATA_HYOJI = 0
        SPR_DTL
        CMB_BR
        LBL_REC_NO
        TXT_CUST_CD_L
        LBL_CUST_NM_L
        TXT_CUST_CD_M
        LBL_CUST_NM_M
        CMB_KIWARI_KBN
        TXT_TEKIYO
        TXT_TANKA_MST_CD
        IMD_TEKIYO_START
        CMB_HOKANRYO_KBN_NASHI
        CMB_HOKANRYO_KBN_ARI
        NUM_HOKANRYO_NASHI
        NUM_HOKANRYO_ARI
        CMB_NIYAKURYO_KBN_NYUKO
        CMB_NIYAKURYO_KBN_SHUKKO
        NUM_NIYAKURYO_NYUKO
        NUM_NIYAKURYO_SHUKKO
        NUM_MIN_HOSHO_NIYAKU_NYUKO
        NUM_MIN_HOSHO_NIYAKU_SHUKKO
        CMB_PRODUCT_SEG_CD

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SprDtlColumnIndex

        DEF = 0
        STATUS
        AVAL_YN             'ADD 2019/04/18 依頼番号 : 004862
        AVAL_YN_NM          'ADD 2019/04/18 依頼番号 : 004862
        APPROVAL_CD
        APPROVAL_NM
        APPROVAL_USER
        APPROVAL_DATE
        APPROVAL_TIME
        BR_CD
        BR_NM
        REC_NO
        CUST_CD_L
        CUST_NM_L
        CUST_CD_M
        CUST_NM_M
        TANKA_MST_CD
        TEKIYO_START_DATE
        TEKIYO
        HOKANRYO_KBN_NASHI
        HOKANRYO_KBN_ARI
        HOKANRYO_NASHI
        HOKANRYO_ARI
        NIYAKURYO_KBN_NYUKO
        NIYAKURYO_KBN_SHUKKO
        NIYAKURYO_NYUKO
        NIYAKURYO_SHUKKO
        MIN_HOSHO_NIYAKURYO_NYUKO
        MIN_HOSHO_NIYAKURYO_SHUKKO
        KIWARI_KBN
        HOKANRYO_NASHI_CURR_CD
        HOKANRYO_ARI_CURR_CD
        NIYAKURYO_NYUKO_CURR_CD
        NIYAKURYO_SHUKKO_CURR_CD
        MIN_HOSHO_NIYAKURYO_NYUKO_CURR_CD
        MIN_HOSHO_NIYAKURYO_SHUKKO_CURR_CD
        CREATE_DATE
        CREATE_USER
        UPDATE_DATE
        UPDATE_USER
        UPDATE_TIME
        SYS_DEL_FLG
        PRODUCT_SEG_CD
        PRODUCT_SEG_NM_L
        PRODUCT_SEG_NM_M

    End Enum

End Class
