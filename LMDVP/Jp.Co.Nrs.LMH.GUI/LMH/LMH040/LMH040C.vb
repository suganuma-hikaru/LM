' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH040C : EDI出荷データ編集
'  作  成  者       :  kim
' ==========================================================================

''' <summary>
''' LMH040定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMH040C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMH040IN"
    Public Const TABLE_NM_IN_FIX As String = "LMH040IN_FIX"
    Public Const TABLE_NM_OUT_L As String = "LMH040_OUTKAEDI_L"
    Public Const TABLE_NM_OUT_M As String = "LMH040_OUTKAEDI_M"
    Public Const TABLE_NM_FREE_L As String = "LMH040_M_FREE_STATE_L"
    Public Const TABLE_NM_FREE_M As String = "LMH040_M_FREE_STATE_M"
    Public Const TABLE_NM_HAITA As String = "LMH040_RCV"
    Public Const TABLE_NM_TORIKOMI_DATE As String = "LMH040_TORIKOMI_DATE"
    Public Const TABLE_NM_INOUTKAEDI_HED_FJF As String = "LMH040_INOUTKAEDI_HED_FJF"

    'モード別画面ロック用
    Public Const MODE_DEFAULT As String = "0"
    Public Const MODE_REF As String = "1"
    Public Const MODE_EDIT As String = "2"
    Public Const MODE_LOCK As String = "9"

    '運送手配（旧運送元区分）
    Public Const UNSO_MOTO_KB_NRS As String = "10"        '日陸手配
    Public Const UNSO_MOTO_KB_CUST As String = "20"       '先方手配
    Public Const UNSO_MOTO_KB_UNFIXED As String = "90"    '未定

    '運送時Mタブ固定対応 terakawa 2012.06.15 Start
    '運送データ識別区分
    Public Const UNSO_DATA As String = "01" '運送データ

    '運送時Mタブ編集対応 2017/05/30 Start
    '対象荷主CD
    Public Const UNSO_CUST_CD_LM As String = "3251600" '日本合成（名古屋）

    '編集フラグ
    Public Const EDIT_FLG_DEFAULT As String = "00" '通常時
    '運送時Mタブ固定対応 terakawa 2012.06.15 End

    '分析表添付有無区分
    Public Const COA_N As String = "0"
    Public Const COA_Y As String = "1"

    '出荷単位
    Public Const ALCTD_KB_KOSU As String = "01"
    Public Const ALCTD_KB_SURYO As String = "02"
    Public Const ALCTD_KB_SAMPLE As String = "04"

    'ステータス
    Public Const STATUS_NOMAL As String = "00"
    Public Const STATUS_TORIKESI As String = "01"
    Public Const STATUS_CANCEL As String = "02"
    Public Const STATUS_TOUROKU As String = "03"
    Public Const STATUS_MATOME As String = "04"

    '状態
    Public Const DEL_KB_OK As String = "0"         '正常
    Public Const DEL_KB_OK_NM As String = "正常"
    Public Const DEL_KB_NG As String = "1"         '削除
    Public Const DEL_KB_NG_NM As String = "削除"

    'DB項目名
    Public Const COLMUN_NM_FREE_C30 As String = "FREE_C30"

    ' アクションID
    Public Const ACTION_ID_SEARCH As String = "SelectListData"
    Public Const ACTION_ID_SAVE As String = "SaveAction"
    Public Const ACTION_ID_TORIKOMI_CHECK As String = "TorikomiCheckAction"
    Public Const ACTION_ID_EDIT_HAITA As String = "HaitaCheckForEdit"

    'アクションタイプ
    Public Enum ActionType As Integer

        EDIT = 0       '編集
        MASTEROPEN     'マスタ参照
        ENTER          'enter
        SAVE           '保存
        CLOSE          '閉じる
        ROWDEL         '行削除
        ROWRE          '行復活
        SINKI          '届先新規登録
        ROWSELECT      '中レコード選択
        KENSAKU        'データ検索（初期表示、再描画）

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        IMDOUTKODATE = 0
        IMDOUTKAPLANDATE
        IMDARRPLANDATE
        CMBARRPLANTIME
        IMDHOKOKUDATE
        CMBOUTKAKB
        CMBSYUBETUKB
        CMBOUTKASTATEKB
        TXTCUSTORDNO
        TXTBUYERORDNO
        CMBPICKKB
        CMBOUTKAHOKOKUYN
        CMBTOUKIHOKANYN
        CMBNIYAKUYN
        TXTSHIPCDL
        CMBDENPYN
        BTNDESTSINKI
        TXTDESTCD
        CMBSPNHSKB
        CMBCOAYN
        TXTEDIDESTCD
        TXTDESTZIP
        TXTDESTJISCD
        TXTDESTTEL
        TXTDESTFAX
        TXTDESTEMAIL
        TXTDESTAD3
        TXTREMARK
        TXTUNSOATT
        BTNROWREM
        BTNROWDELM
        SPRGOODSDEF
        TXTCUSTGOODSCD
        TXTCUSTORDNODTL
        TXTRSVNO
        TXTSERIALNO
        TXTBUYERORDNODTL
        OPTTEMPY
        OPTTEMPN
        OPTCNT
        OPTAMT
        OPTSAMPLE
        NUMOUTKATTLNB
        NUMOUTKAPKGNB
        NUMOUTKAHASU
        NUMOUTKATTLQT
        TXTGOODSREMARK
        CMBUNSOMOTOKB
        CMBUNSOTEHAIKB
        CMBSHARYOKB
        CMBBINKB
        CMBPCKB
        TXTUNSOCD
        TXTUNSOBRCD
        TXTUNTINTARIFFCD
        TXTEXTCTARIFF
        SPRFREEINPUTSL
        SPRFREEINPUTSM

    End Enum

    'Spread部列インデックス用列挙対（商品別情報）
    Public Enum SprMainColumnIndex

        DEF = 0
        DEL                '状態
        EDI_CTL_NO_CHU     'EDI出荷管理番号（中）
        CUST_GOODS_CD      '商品コード
        M_GOODS_NM         '商品名
        ALCTD_KB_NM        '出荷単位
        IRIME              '入目
        '▼▼▼(梱数→個数対応)
        'OUTKA_PKG_QT       '梱数
        NB                 '個数
        '▲▲▲(梱数→個数対応)
        OUTKA_TTL_QT       '数量
        REMARK             '商品別注意事項
        CUST_ORD_NO_DTL    'オーダー番号
        SYS_UPD_DATE
        SYS_UPD_TIME
        LAST

    End Enum

    'Spread部列インデックス用列挙対（自由項目）
    Public Enum SprFreeColumnIndex

        'DEF = 0
        TITLE = 0   'タイトル
        INPUT     '入力
        COLNM     'DB列名
        EDIT_ABLE_FLAG
        ROW_VISIBLE_FLAG
        ROW_NUM
        LAST

    End Enum

End Class
