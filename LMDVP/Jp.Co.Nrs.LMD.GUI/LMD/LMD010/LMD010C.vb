' ==========================================================================
'  システム名       :  LMS
'  サブシステム名   :  LMD     : 在庫サブシステム
'  プログラムID     :  LMD010C : 在庫振替入力
'  作  成  者       :  
' ==========================================================================

''' <summary>
''' LMD010定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD010C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMD010IN"
    Public Const TABLE_NM_OUT As String = "LMD010OUT"
    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    Public Const TABLE_NM_TOU_SITU_EXP As String = "LMD010_TOU_SITU_EXP"
    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

    '請求日取得用データセット
    Public Const TABLE_NM_KAGAMI_IN As String = "LMD010_KAGAMI_IN"
    Public Const TABLE_NM_STORAGE_SKYU_DATE As String = "LMD010_STORAGE_SKYU_DATE"
    Public Const TABLE_NM_HANDLING_SKYU_DATE As String = "LMD010_HANDLING_SKYU_DATE"
    Public Const TABLE_NM_SAGYO_SKYU_DATE As String = "LMD010_SAGYO_SKYU_DATE"

    '移動日取得用データセット
    Public Const TABLE_NM_IDO_TRS_IN As String = "LMD010_IDO_TRS_IN"
    Public Const TABLE_NM_IDO_TRS_OUT As String = "LMD010_IDO_TRS_OUT"

    '振替元在庫データセット
    Public Const TABLE_NM_ZAI_OLD_IN As String = "LMD010_ZAI_OLDIN"
    Public Const TABLE_NM_ZAI_OLD_OUT As String = "LMD010_ZAI_OLDOUT"

    '更新、登録用のデータセット
    Public Const TABLE_NM_OUTKA_L As String = "LMD010_OUTKA_L"
    Public Const TABLE_NM_INKA_L As String = "LMD010_INKA_L"
    Public Const TABLE_NM_OUTKA_M As String = "LMD010_OUTKA_M"
    Public Const TABLE_NM_INKA_M As String = "LMD010_INKA_M"
    Public Const TABLE_NM_ZAI_OLD As String = "LMD010_ZAI_OLD"
    Public Const TABLE_NM_OUTKA_S As String = "LMD010_OUTKA_S"
    Public Const TABLE_NM_INKA_S As String = "LMD010_INKA_S"
    Public Const TABLE_NM_ZAI_NEW As String = "LMD010_ZAI_NEW"
    Public Const TABLE_NM_SAGYO_OUTKA As String = "LMD010_SAGYO_OUTKA"
    Public Const TABLE_NM_SAGYO_INKA As String = "LMD010_SAGYO_INKA"
    Public Const TABLE_NM_FURIKAE As String = "LMD010_FURIKAE"          'ADD 2019/01/08 依頼番号 : 004100   【LMS】在庫振替の新規画面追加・編集・削除・再印刷機能の追加機能

    '荷主
    Public Const CUST_L As String = "L"
    Public Const CUST_M As String = "M"

    ''' <summary>
    ''' 区分マスタ【H004:振替区分】
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const FURIKAE_KBN_CUST As String = "01"
    Friend Const FURIKAE_KBN_GOODS As String = "02"
    Friend Const FURIKAE_KBN_LOT As String = "03"
    Friend Const FURIKAE_KBN_HAKUGAIHIN As String = "04"

    '2015.10.30 tusnehira add Start
    '英語化対応
    Public Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Public Const MESSEGE_LANGUAGE_ENGLISH As String = "1"
    '2015.10.30 tusnehira add End

    '英語化対応
    '20151118 M_takahashi
    Public Const indexCHGSHPR As Integer = 1
    Public Const indexCHGIC As Integer = 2
    Public Const indexCHGLOTNo As Integer = 3
    Public Const indexUnaccount As Integer = 4

    ''' <summary>
    ''' アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ActionType As Integer

        MAIN = 0
        HENSHU
        FUKUSHA
        HIKIATE
        ZENRYO
        MASTER
        FURIKAEMOTOKAKUTEI
        FURIKAEKAKUTEI
        TOJIRU
        COLDEL
        COLADDNEW
        COLDELNEW
        ENTER
        PRINT_INIT      'ADD 2018/12/21 依頼番号 : 003904   【LMS】在庫振替入力時の印刷プレビューを再度閲覧したい対応
        PRINT           'ADD 2018/12/21 依頼番号 : 003904   【LMS】在庫振替入力時の印刷プレビューを再度閲覧したい対応

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        FURIKAENO = 0
        NRSBRCD
        SOKO
        FURIKAEKBN
        FURIKAEDATE
        YOUKICHANGE
        TOUKIHOKANKBN
        FURIKAE
        CUSTCDL
        CUSTNML
        CUSTCDM
        CUSTNMM
        ORDERNO
        GOODSCDCUST
        GOODSNMCUST
        LOTNO
        SERIALNO
        IRIME
        IRIMETANNI
        NIYAKU
        TAXKBN
        KOSU
        KONSU
        KONSUTANNI
        CNT
        CNTTANI
        IRISUCNT
        KOSUCNT
        HIKISUMICNT
        HIKIZANCNT
        HUTAISAGYO
        SAGYOCD1
        SAGYONM1
        SAGYOCD2
        SAGYONM2
        SAGYOCD3
        SAGYONM3
        PSYUKKAREMARK
        SYUKKAREMARK
        DEL
        DETAIL
        FURIKAENEW
        CUSTCDLNEW
        CUSTNMLNEW
        CUSTCDMNEW
        CUSTNMMNEW
        DENPNO
        GOODSCDCUSTNEW
        GOODSNMCUSTNEW
        GOODSCDNRSNEW
        NIYAKUNEW
        TAXKBNNEW
        INKODATEUMU
        HUTAISAGYONEW
        SAGYOCD1NEW
        SAGYONM1NEW
        SAGYOCD2NEW
        SAGYONM2NEW
        SAGYOCD3NEW
        SAGYONM3NEW
        PNYUKAREMARK
        NYUKAREMARK
        ADDNEW
        DELNEW
        DETAILNEW


    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対(振替元)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndexSprDtl

        DEF = 0
        MOTO_INKA_NO_S
        MOTO_TOU_NO
        MOTO_SITU_NO
        MOTO_ZONE_CD
        MOTO_LOCA
        MOTO_LOT_NO
        MOTO_HURIKAE_KOSU
        MOTO_ZAN_KOSU
        MOTO_IRIME
        MOTO_HURIKAE_SURYO
        MOTO_ZAN_SURYO
        MOTO_ZAI_REC
        MOTO_LT_DATE
        MOTO_GOODS_COND_NM_1
        MOTO_GOODS_COND_NM_2
        MOTO_GOODS_COND_NM_3
        MOTO_OFB_NM
        MOTO_SPD_NM
        KEEP_GOODS_NM
        MOTO_SERIAL_NO
        MOTO_GOODS_CRT_DATE
        MOTO_DEST_CD
        MOTO_ALLOC_PRIORITY_NM
        MOTO_BUYER_ORD_NO_L
        MOTO_INKA_DATE
        MOTO_INKA_YOTEI_DATE
        MOTO_UPDATE_DATE
        MOTO_UPDATE_TIME
        MOTO_PORA_ZAI_QT
        MOTO_HOKAN_YN
        MOTO_GOODS_COND_KB_1
        MOTO_GOODS_COND_KB_2
        MOTO_GOODS_COND_KB_3
        MOTO_OFB_KB
        MOTO_SPD_KB
        MOTO_ALLOC_PRIORITY
        MOTO_REMARK
        MOTO_REMARK_OUT
        MOTO_BYK_KEEP_GOODS_CD


    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対(振替先)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndexSprDtlNew

        DEF = 0
        SAKI_INKA_NO_S
        SAKI_TOU_NO
        SAKI_SITU_NO
        SAKI_ZONE_CD
        SAKI_LOCA
        SAKI_LOT_NO
        SAKI_HURIKAE_KOSU
        SAKI_HURIKAE_TANNI
        SAKI_IRIME
        SAKI_SURYO_TANNI
        SAKI_HURIKAE_SURYO
        SAKI_REMARK
        SAKI_LT_DATE
        SAKI_REMARK_OUT
        SAKI_GOODS_COND_KB_1
        SAKI_GOODS_COND_KB_2
        SAKI_GOODS_COND_KB_3
        SAKI_OFB_KB
        SAKI_SPD_KB
        SAKI_BYK_KEEP_GOODS_CD
        SAKI_SERIAL_NO
        SAKI_GOODS_CRT_DATE
        SAKI_DEST_CD
        SAKI_ALLOC_PRIORITY
        SAKI_INKA_DATE
        SAKI_INKA_YOTEI_DATE
        SAKI_ZAI_REC_NO


    End Enum

    '入荷(小)数量の桁数
    Public Const FURI_SURYO_MAX As String = "999999999.999"
    Public Const FURI_SURYO_MIN As String = "0"

    'START YANAI メモNo.88
    Public Const FURI_KOSU_MAX As String = "9999999999"
    Public Const FURI_KOSU_MIN As String = "0"
    'END YANAI メモNo.88

    '作業の項目名
    Public Const SAGYO_PK As String = "lblRecNo"
    Public Const SAGYO_CD As String = "txtSagyoCd"
    Public Const SAGYO_NM As String = "lblSagyoNm"
    Public Const SAGYO_IN_NM As String = "lblSagyoInNm"
    Public Const SAGYO_UP As String = "lblAddFlg"

    '作業レコードの最大レコード数
    Public Const SAGYO_MAX_REC As Integer = 3

    ''' <summary>
    ''' 作業データ設定 切替用
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SagyoData As Integer
        O
        N
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

    '入荷(小)数量の桁数
    Public Const SURYO_MAX As String = "9999999999"
    Public Const SURYO_MIN As String = "0"

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
