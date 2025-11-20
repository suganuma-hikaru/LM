' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME010C : 作業料明細書作成
'  作  成  者       :  Nishikawa
' ==========================================================================

''' <summary>
''' LME010定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LME010C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LME010IN"
    Public Const TABLE_NM_INOUT As String = "LME010INOUT"
    Public Const TABLE_NM_SAGYO As String = "LME010_SAGYO"
    Public Const TABLE_NM_UPDATE_KEY As String = "LME010OUT_UPDATE_KEY"
    Public Const TABLE_NM_UPDATE_VALUE As String = "LME010OUT_UPDATE_VALUE"
    Public Const TABLE_NM_GUIERROR As String = "LME010_GUIERROR"
    Public Const TABLE_NM_PRINTCHECK As String = "LME010_PRINT_CHECK"

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
        KAKUTEIKAIJO
        DEF_CUST        '初期荷主変更
        KENSAKU         '検索
        MASTER          'マスタ参照
        HOZON           '保存
        CLOSE           '閉じる
        HENKO           '一括変更
        ROW_COPY        '行複写
        ROW_DEL         '行削除
        PRINT           '印刷
        ENTER           'ENTER
        'START YANAI 20120319　作業画面改造
        SINKI           '新規
        RENZOKU         '連続
        DOUBLECLICK     'ダブルクリック
        KANRYO          '完了
        'END YANAI 20120319　作業画面改造

    End Enum

    ' Spread部列インデックス用列挙対
    Public Enum SprColumnIndex

        DEF = 0                     '選択列
        GOODS_NM                    '商品名
        LOT_NO                      'ＬＯＴ番号
        SAGYO_NM                    '作業名
        SAGYOSU                     '作業数
        SAGYO_TANKA                 '請求単価
        INV_TANI_NM                 '請求単位
        AMT                         '請求金額
        ITEM_CURR_CD                '契約通貨コード
        SQTO_CD                     '請求先コード
        SQTO_NM                     '請求先名称
        SAGYO_COMP_DATE             '作業完了日
        DEST_NM                     '届先名
        SKYU_REMARK                 '備考
        TAX_KB_NM                   '課税区分名
        CUST_NM                     '荷主名称
        IOKA_CTL_NO                 '管理番号
        SAGYO_SIJI_NO               '作業指示書番号
        SAGYO_REC_NO                '作業レコード番号
        SAGYO_COMP_NM               '確認作業者名
        SAGYO_CD                    '作業コード
        CUST_CD_L                   '荷主大
        CUST_CD_M                   '荷主中
        IOZS_NM                     '入出在その他名
        UPD_DT                      '更新日
        UPD_TM                      '更新時間
        UPT_NM                      '更新者
        '隠し項目
        NRS_BR_CD                   '営業所コード
        WH_CD                       '倉庫コード
        INV_TANI                    '請求単位区分
        DEST_CD                     '届先コード
        TAX_KB                      '課税区分
        IOZS_KB                     '入出在その他区分
        SAGYO_COMP_KB               '作業完了区分
        SKYU_CHK                    '請求確認フラグ
        REMARK_ZAI                  '在庫用備考
        SAGYO_COMP_CD               '確認作業者コード
        DEST_SAGYO_FLG              '届先作業有無フラグ
        SYS_DEL_FLG                 '削除フラグ
        COPY_FLG                    '複写フラグ
        SAVE_FLG                    '登録済フラグ
        ROW_NO                      '行番号
        HAITA_UPD_TM                '排他用時間
    End Enum

    'タブインデックス用列挙体(Main)
    Public Enum CtlTabIndex_MAIN


        GRP_SEARCH = 0
        CUST_CD_L
        CUST_CD_M
        SEIQ_CD
        SAGYO_CD
        NRS_BR_CD
        WH_CD
        CMB_PRINT
        BTN_PRINT
        SAGYO_SIJI_NO
        SAGYO_DATE_FROM
        SAGYO_DATE_TO
        GRP_CHK
        NOT_KAKUTEI
        KAKUTEI
        'START YANAI 20120319　作業画面改造
        GRP_KANRYO
        NOT_KANRYO
        KANRYO
        'END YANAI 20120319　作業画面改造
        GRP_CHANGE
        SYUSEI_KOUMOKU
        TXT_EDIT
        NUM_EDIT
        CMB_DATE
        CMB_TAX
        CMB_TANI
        BTN_CHANGE
        BTN_COPY
        BTN_DEL
        SPR

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

End Class
