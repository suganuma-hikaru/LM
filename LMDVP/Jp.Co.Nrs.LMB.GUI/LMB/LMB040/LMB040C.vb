' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB040C : 入荷検品選択
'  作  成  者       :  小林
' ==========================================================================

''' <summary>
''' LMB040定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB040C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMB040IN"
    Public Const TABLE_NM_INPUTED As String = "LMB040INPUTED"
    Public Const TABLE_NM_OUT As String = "LMB040OUT"
    Public Const TABLE_NM_IN_UPDATE As String = "LMB040IN_UPDATE"
    Public Const TABLE_NM_IN_DELETE As String = "LMB040IN_DELETE"

    '商品マスタ合致区分
    Public Const GATCH_KBN_ONE As String = "00"
    Public Const GATCH_KBN_NON As String = "01"
    Public Const GATCH_KBN_ANY As String = "02"
    Public Const GATCH_KBN_DEL As String = "03"
    Public Const GATCH_KBN_DIFF_CUST_M As String = "04"


#If True Then ' フィルメニッヒ入荷検品対応 20170310 added by inoue 


    ''' <summary>
    ''' 商品マスタ参照区分
    ''' </summary>
    ''' <remarks></remarks>
    Class M_GOODS_REF_KBN

        ''' <summary>
        ''' 商品マスタ合致区分がANYの場合のみ商品M参照を実行する
        ''' </summary>
        ''' <remarks>
        ''' デフォルト
        ''' </remarks>
        Public Const ANY_ONLY As String = "0"

        ''' <summary>
        ''' 商品マスタ合致区分を問わず商品M参照を実行する
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ALL_STATUS As String = "1"

        Class DEFINITION

            ''' <summary>
            ''' 商品マスタ詳細サブ区分(商品M参照)
            ''' </summary>
            ''' <remarks></remarks>
            Public Const SUB_KBN As String = "59"

            ''' <summary>
            ''' 設定カラム名
            ''' </summary>
            ''' <remarks></remarks>
            Public Const VALUE_COLUMN_NM As String = "SET_NAIYO_3"
        End Class


    End Class

#End If

    'タブインデックス用列挙体(Main)
    Public Enum CtlTabIndex_MAIN

        SAGYO_USER_CD = 0
        SYS_ENT_DATE
        SYS_ENT_DATE_TO
        CHK_MISHORI
        SPRDETAIL

    End Enum

    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        MST_EXISTS_MARK
        KENPIN_DATE       '2013.07.19 追加START
        GOODS_NM
        GOODS_CD_CUST
        IRIME
        IRIME_UT
        PKG_UT
        PKG_NB
        NB_UT_1
        LOT_NO
        '2013.11.25 WIT対応START
        'KENPIN_NO
        SERIAL_NO       '2014.02.17 追加START
        GOODS_CRT_DATE  ' JT物流入荷検品対応20160726 added 
        LT_DATE         ' フィルメニッヒ入荷検品対応 20170310 added 
        WH_CD
        INPUT_DATE
        SEQ
        TORIKOMI_FLG_NM
        GOODS_KANRI_NO
        '2013.11.25 WIT対応END
        OKIBA
        KENPIN_KAKUTEI_TTL_NB
        NB_UT_2
        USER_NM         '2013.07.19 追加START
        NRS_BR_CD
        CUST_CD_L
        CUST_CD_M
        GOODS_CD_NRS
        TOU_NO
        SITU_NO
        ZONE_CD
        LOCA
        ONDO_KB
        ONDO_STR_DATE
        ONDO_END_DATE
        STD_WT_KGS
        KONSU
        HASU
        BETU_WT
        INKA_KAKO_SAGYO_KB_1
        INKA_KAKO_SAGYO_KB_2
        INKA_KAKO_SAGYO_KB_3
        INKA_KAKO_SAGYO_KB_4
        INKA_KAKO_SAGYO_KB_5
        SYS_UPD_DATE
        SYS_UPD_TIME
        MST_EXISTS_KBN
        CUST_CD_S           '2013.07.18 追加START
        CUST_CD_SS          '2013.07.18 追加START
        TARE_YN             '2013.07.18 追加START
        LOT_CTL_KB          '2013.07.18 追加START
        LT_DATE_CTL_KB      '2013.07.18 追加START
        CRT_DATE_CTL_KB     '2013.07.18 追加START
        CHK_TANI            ' JT物流入荷検品対応20160726 added 
        NEXT_TEST_DATE      'ADD 2018/11/06 依頼番号 : 002669   【LMS】ハネウェル管理_次回定検日項目追加
        INDEX_COUNT
    End Enum

End Class
