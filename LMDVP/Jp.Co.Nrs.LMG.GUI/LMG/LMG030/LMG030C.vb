' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG030C : 保管料荷役料明細編集
'  作  成  者       :  [熊本史子]
' ==========================================================================

''' <summary>
''' LMG030定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMG030C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMG030IN"
    Public Const TABLE_NM_OUT As String = "LMG030OUT"
    Public Const TABLE_NM_IN_UPDATE As String = "LMG030IN_UPDATE"
    Public Const TABLE_NM_BATCH As String = "LMG030IN_ACQUISITHION"
    Public Const TABLE_NM_BATCH_HAITA As String = "LMG030IN_BATCH_HAITA"
    'START YANAI 20111014 一括変更追加
    Public Const TABLE_NM_IN_IKKATU As String = "LMG030IN_IKKATU_UPDATE"
    'END YANAI 20111014 一括変更追加
    Public Const TABLE_NM_OUT_VAR_STRAGE As String = "LMG030OUT_VAR_STRAGE"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
        HENSHU
        TORIKOMI
        KENSAKU
        SAVE
        CLOSE
        PRINT
        MEISAI
        CHANGE
        DOUBLECLICK
        IKKATU

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        GRP_SHUTURYOKU = 0
        LBL_JOBNO
        LBL_CUST_L
        LBL_CUST_M
        LBL_CUST_S
        LBL_CUST_SS
        LBL_CUST_NM
        CMB_PRINT
        BTN_PRINT
        CMB_IKKATU
        TXT_IKKATU
        BTN_IKKATU
        SPR_MEISAI
        LBL_GOODS_CD_CUST
        LBL_GOODS_NM1
        LBL_GOODS_CD_NRS
        LBL_LOTNO
        LBL_SERIALNO
        CMB_NB_UT
        LBL_IRIME_NB
        CMB_IRIME_UT
        CMB_TAX_KB
        TXT_SEKI_NB1
        TXT_SEKI_NB2
        TXT_SEKI_NB3
        TXT_HOKAN_TNK1
        TXT_HOKAN_TNK2
        TXT_HOKAN_TNK3
        TXT_HOKAN_AMT
        TXT_VAR_HOKAN_AMT
        TXT_IN_NB
        TXT_NIYAKU_IN_TNK1
        TXT_NIYAKU_IN_TNK2
        TXT_NIYAKU_IN_TNK3
        TXT_OUT_NB
        TXT_NIYAKU_OUT_TNK1
        TXT_NIYAKU_OUT_TNK2
        TXT_NIYAKU_OUT_TNK3
        TXT_NIYAKU_AMT

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        GOODS_CD
        GOODS_NM
        LOT_NO
        SERIAL_NO
        TANI
        IRIME_SURYO
        IRIME_TANI
        ZEIKUBUN
        INKA_DATE
        INKA_NO_L
        SEKI_NB_1
        SEKI_NB_2
        SEKI_NB_3
        NYUUKODAKA
        SYUKKODAKA
        HOKAN_1
        HOKAN_2
        HOKAN_3
        HOKANRYO
        VAR_RATE
        VAR_HOKANRYO
        NIYAKU_IN_1
        NIYAKU_IN_2
        NIYAKU_IN_3
        NIYAKU_OUT_1
        NIYAKU_OUT_2
        NIYAKU_OUT_3
        NIYAKURYO
        NRSGOODS_CD
        CUST_NM_S_SS
        CUST_CD_L
        CUST_CD_M
        CUST_CD_S
        CUST_CD_SS
        CTL_NO
        IRIME_UT
        TAX_KB
        NB_UT
        SYS_UPD_DATE
        SYS_UPD_TIME

    End Enum

End Class
