' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD110C : 在庫振替検索
'  作  成  者       :  daikoku
' ==========================================================================

''' <summary>
''' LMD110定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD110C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMD110IN"
    Public Const TABLE_NM_INOUT As String = "LMD110OUT"

    'モード別画面ロック用
    Public Const MODE_DEFAULT As String = "0"
    Public Const MODE_REF As String = "1"
    Public Const MODE_EDIT As String = "2"
    Public Const MODE_LOCK As String = "9"

    '作業数の桁数
    Public Const SAGYO_NB As String = "9999999999"
    Public Const SAGYO_NB_MAX As String = "9,999,999,999"
    Public Const SAGYO_NB_MIN As String = "0"

    '請求単価の桁数
    Public Const SAGYO_UP As String = "999999999.999"
    Public Const SAGYO_UP_MAX As String = "999,999,999.999"
    Public Const SAGYO_UP_MIN As String = "0.000"

    '請求金額の桁数
    Public Const SAGYO_GK As String = "999999999"
    Public Const SAGYO_GK_MAX As String = "999,999,999"
    Public Const SAGYO_GK_MIN As String = "0"

    '一括変更項目値
    Public Const EDIT_SELECT_GOODS As String = "01"     '商品
    Public Const EDIT_SELECT_LOT As String = "02"       'ロット№
    Public Const EDIT_SELECT_SAGYONM As String = "03"   '作業名
    Public Const EDIT_SELECT_SAGYOSU As String = "04"   '作業数
    Public Const EDIT_SELECT_SEIQUP As String = "05"    '請求単価
    Public Const EDIT_SELECT_SEIQUT As String = "06"    '請求単位
    Public Const EDIT_SELECT_SEIQGK As String = "07"    '請求金額
    Public Const EDIT_SELECT_SEIQTO As String = "08"      '請求先コード
    Public Const EDIT_SELECT_SAGYODATE As String = "09" '作業完了日
    Public Const EDIT_SELECT_DEST As String = "10"      '届先名
    Public Const EDIT_SELECT_REMARK As String = "11"    '備考
    Public Const EDIT_SELECT_TAX As String = "12"       '課税区分

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"

    ''' <summary>
    ''' EXCEL用COLUMタイトル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXCEL_COLTITLE As String = "作業レコード番号"

    'イベント種別
    Public Enum EventShubetsu As Integer

        KAKUTEI = 0
        SINKI           '新規
        KENSAKU         '検索
        MASTER          'マスタ参照
        HOZON           '保存
        CLOSE           '閉じる
        PRINT           '印刷
        ENTER           'ENTER

    End Enum

    ' Spread部列インデックス用列挙対
    Public Enum SprColumnIndex

        DEF = 0                     '選択列
        ORDER_NO                    'オーダー番号
        FURIKAE_DATE                '振替日
        FURI_NO                     '振替管理番号
        FURI_NM                     '振替名
        YOUKI_NM                    '容器変更
        MOTO_CUST_NM                '振替元荷主名称
        SAKI_CUST_NM                '振替先荷主名称
        SAKI_GOODS_NM               '振替先商品名ﾞ
        OUTKA_NO_L                  '出荷管理番号
        INKA_NO_L                   '入荷管理番号

        UPT_NM                      '更新者
        UPD_DATE                    '更新日
        MU_NM                       '振替元担当者名
        SU_NM                       '振替先担当者名
        '隠し項目
        NRS_BR_CD                   '営業所コード
        WH_CD                       '倉庫コード
        HAITA_UPD_TM                '排他用時間
        FURI_SYS_ENT_DATE           '振替作成日
        OUT_UP_DT                   '出荷更新日時
        IN_UP_DT                    '入荷更新日時
        FURI_KBN                    '振替区分
        YOUKI_HENKO_KBN             '容器変更区分
        OUT_TAX_KB                  '税区分
    End Enum

    'タブインデックス用列挙体(Main)
    Public Enum CtlTabIndex_MAIN


        GRP_SEARCH = 0

        FURIKAE_DATE_S
        FURIKAE_DATE_E
        MOTO_CUST_CD_L
        MOTO_CUST_CD_M
        SAKI_CUST_CD_L
        SAKI_CUST_CD_M

        FURIKAE_KBN
        YOUKI_HENKO
        MY_CREATE

        NRS_BR_CD
        WH_CD
        PRINT
        BTN_PRINT

    End Enum



    Public Enum EditCD

        GOODS_NM_NRS = 1
        LOT_NO
        SAGYO_NM
        SAGYO_NB
        SAGYO_UP
        INV_TANI
        SAGYO_GK
        SEQTO_CD
        SAGYO_COMP_DATE
        DEST_NM
        REMARK_SKYU
        TAX_KB

    End Enum

    '印刷種別
    Public Enum PrintShubetsu As Integer

        '01	振替印刷帳票名（振替データ検索）	振替

        FURIKAE = 1     '振替
    End Enum
End Class
