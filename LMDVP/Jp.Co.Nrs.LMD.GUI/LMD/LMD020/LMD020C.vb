' ==========================================================================
'  システム名       :  LMS
'  サブシステム名   :  LMD     : 在庫サブシステム
'  プログラムID     :  LMD020C : 在庫履歴照会
'  作  成  者       :  
' ==========================================================================

''' <summary>
''' LMD020定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD020C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMD020IN"
    Public Const TABLE_NM_OUT As String = "LMD020OUT"
    Public Const TABLE_NM_SEIQ_HED_IN As String = "LMD020_SEIQ_HED_IN"
    Public Const TABLE_NM_SEIQ_HED_OUT As String = "LMD020_SEIQ_HED_OUT"
    Public Const TABLE_NM_HOKAN_NIYAKU As String = "LMD020_HOKAN_NIYAKU"
    '要望番号:1350 terakawa 2012.08.27 Start
    Public Const TABLE_NM_WORNING As String = "LMD020_WORNING"
    '要望番号:1350 terakawa 2012.08.27 End

    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    Public Const TABLE_NM_TOU_SITU_EXP As String = "LMD020_TOU_SITU_EXP"
    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

    '更新、登録用のデータセット
    Public Const TABLE_NM_ZAI_OLD As String = "LMD020_ZAI_OLD"
    Public Const TABLE_NM_ZAI_NEW As String = "LMD020_ZAI_NEW"
    Public Const TABLE_NM_IDO As String = "LMD020_IDO"

    '2015.10.29 tusnehira add Start
    '英語化対応
    Public Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Public Const MESSEGE_LANGUAGE_ENGLISH As String = "1"
    '2015.10.29 tusnehira add End


    ''' <summary>
    ''' アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ActionType As Integer

        MAIN = 0
        ZAIKORIREKI
        KENSAKU
        MASTER
        HOZON
        TOJIRU
        LEFTSCRL
        RIGHTSCRL
        COLDEL
        COLADD
        ALLCHANGE
        ENTER
        CHK

    End Enum


    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        KENSAKU = 0
        NRSBRCD
        SOKO
        CUSTCDL
        CUSTCDM
        CUSTNM
        NYUKAFROM
        NYUKATO
        ALL
        DEFECTIVEPRODUCT
        SEARCHINPUT
        CJIYURAN
        TJIYURAN
        IDOUBI
        HEIKOUIDO
        FUKUSUIDO
        KYOSEISHUKO
        SPRMOVELEFT
        SPRMOVERIGHT
        LINEADD
        LINEDEL
        OKIBA
        TOUNO
        SITUNO
        ZONECD
        LOCATION
        GOODSCONDKB1
        GOODSCONDKB2
        GOODSCONDKB3
        SPDKB
        OFBKB
        LTDATE
        GOODSCRTDATE
        ALLOCPRIORITY
        DESTCD
        RSVNO
        REMARKOUT
        REMARK
        ALLCHANGE
        MOVELEFT
        MOVERIGHT

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対(移動前)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndexMoveBefor

        DEF = 0
        'START YANAI 要望番号550
        GOODS_CD_CUST
        'END YANAI 要望番号550
        GOODS_NM
        LOT_NO
        IRIME
        STD_IRIME_UT
        INKO_DATE
        SERIAL_NO
        ALLOC_CAN_NB
        PKG_UT
        ALCTD_NB
        PORA_ZAI_NB
        TOU_NO
        SITU_NO
        ZONE_CD
        LOCA
        'START YANAI 要望番号550
        'GOODS_CD_CUST
        'END YANAI 要望番号550
        GOODS_COND_NM_1
        GOODS_COND_NM_2
        GOODS_COND_NM_3
        SPD_KB_NM
        OFB_KB_NM
        KEEP_GOODS_NM
        LT_DATE
        GOODS_CRT_DATE
        SEARCH_KEY_2
        ALLOC_PRIORITY_NM
        DEST_NM
        RSV_NO
        REMARK_OUT
        ZAI_REC_NO
        CUST_NM_L
        CUST_NM_M
        'START YANAI 要望番号766
        CUST_CD_S
        'END YANAI 要望番号766
        REMARK
        ALCTD_QT
        ALLOC_CAN_QT
        PORA_ZAI_QT
        PKG_NB
        PKG_UT_QT
        HOKAN_NIYAKU_CALCULATION
        OUTKO_DATE
        GOODS_CD_NRS
        CUST_CD_L
        CUST_CD_M
        INKA_NO_L
        INKA_NO_M
        INKA_NO_S
        HOKAN_YN
        TAX_KB
        ZERO_FLAG
        SMPL_FLAG
        SYS_UPD_DATE
        SYS_UPD_TIME
        ROW_NO
        NRS_BR_CD
        WH_CD
        GOODS_KANRI_NO
    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対(移動後)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndexMoveAfter

        DEF_R = 0
        TOU_NO_R
        SITU_NO_R
        ZONE_CD_R
        LOCA_R
        IDO_KOSU_R
        GOODS_COND_KB_1_R
        GOODS_COND_KB_2_R
        GOODS_COND_KB_3_R
        SPD_KB_R
        OFB_KB_R
        BYK_KEEP_GOODS_CD_R
        LT_DATE_R
        GOODS_CRT_DATE_R
        ALLOC_PRIORITY_R
        DEST_CD_R
        DEST_NM_R
        RSV_NO_R
        REMARK_OUT_R
        REMARK_R
        ROW_NO
        NRS_BR_CD_R
        CUST_CD_L_R
        WH_CD_R
        CUST_CD_M_R
    End Enum

    '2017/11/15 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    ''' <summary>
    ''' 危険物チェック エラー、ワーニング切替用
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DangerousGoodsCheckErrorOrWarning
        Warning = 0
        Err = 1
    End Enum
    '2017/11/15 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    'データセットINデータ（在庫履歴照会）　固定値　

    Public Const DEL_VIEW_FLG As String = "00" '出荷取消表示フラグ
    Public Const VIEW_FLG As String = "00" '表示フラグ

    ''' <summary>
    ''' 事由欄：自動倉庫移動
    ''' </summary>
    ''' <remarks>要望管理009859</remarks>
    Public Const JIYURAN_AUTO_IDO As String = "07"

    ''' <summary>
    ''' 営業所コード：土気
    ''' </summary>
    Public Const NRS_BR_CD_TOKE As String = "15"

    ''' <summary>
    ''' 区分マスタ 区分分類コード
    ''' </summary>
    Public Class KbnConst
        ''' <summary>
        ''' BYKキープ品 
        ''' </summary>
        Public Const BYK_KEEP_GOODS_CD As String = "B039"
    End Class

End Class
