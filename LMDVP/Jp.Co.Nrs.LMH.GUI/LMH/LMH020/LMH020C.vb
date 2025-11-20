' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH020C : EDI入荷データ編集
'  作  成  者       :  
' ==========================================================================

''' <summary>
''' LMH020定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMH020C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' 初期検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT As String = "SelectData"

    ''' <summary>
    ''' 保存アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SAVE As String = "SaveItemData"

    ''' <summary>
    ''' 編集アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_EDIT As String = "SelectEditData"

    ''' <summary>
    ''' データセットテーブル名(IN)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMH020IN"

    ''' <summary>
    ''' データセットテーブル名(INKAEDI_L)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_L As String = "INKAEDI_L"

    ''' <summary>
    ''' データセットテーブル名(INKAEDI_M)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_M As String = "INKAEDI_M"

    ''' <summary>
    ''' データセットテーブル名(M_FREE_STATE)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_FREE As String = "M_FREE_STATE"

    ''' <summary>
    ''' 入力制御(半角)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INPUT_ALL_HANKAKU As String = "01"

    ''' <summary>
    ''' 入力制御(全半角)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INPUT_ALL_MIX As String = "02"

    ''' <summary>
    ''' 入力制御(全半角_IMEOFF)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INPUT_ALL_MIX_IME_OFF As String = "03"

    ''' <summary>
    ''' 入力制御(全角)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INPUT_ALL_ZENKAKU As String = "04"

    ''' <summary>
    ''' 入力制御(アルファベットのみ)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INPUT_HAN_ALPHA As String = "05"

    ''' <summary>
    ''' 入力制御(アルファベット(小文字)のみ)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INPUT_HAN_ALPHA_L As String = "06"

    ''' <summary>
    ''' 入力制御(アルファベット(大文字)のみ)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INPUT_HAN_ALPHA_U As String = "07"

    ''' <summary>
    ''' 入力制御(半角カナのみ)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INPUT_HAN_KANA As String = "08"

    ''' <summary>
    ''' 入力制御(数字＋アルファベットのみ)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INPUT_HAN_NUM_ALPHA As String = "09"

    ''' <summary>
    ''' 入力制御(数字＋アルファベット(小文字)のみ)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INPUT_HAN_NUM_ALPHA_L As String = "10"

    ''' <summary>
    ''' 入力制御(数字＋アルファベット(大文字)のみ)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INPUT_HAN_NUM_ALPHA_U As String = "11"

    ''' <summary>
    ''' 入力制御(数字型)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const INPUT_NUMBER As String = "12"

    ''' <summary>
    ''' 削除区分(通常)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DEL_KBN_NOMAL As String = "0"

    ''' <summary>
    ''' 削除区分(キャンセル)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DEL_KBN_CANCELL As String = "2"

    ''' <summary>
    ''' 削除区分(保留)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DEL_KBN_RESERVE As String = "3"

    ''' <summary>
    ''' データ区分(L)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DATA_KB_L As String = "10"

    ''' <summary>
    ''' データ区分(M)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DATA_KB_M As String = "20"

    ''' <summary>
    ''' 前ゼロ(000)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAEZERO As String = "000"

    ''' <summary>
    ''' 手配区分(日陸手配)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TEHAI_NRS As String = "10"

    ''' <summary>
    ''' テーブルタイプ(車種・距離)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TBL_TYPE_KURUMA As String = "01"

    ''' <summary>
    ''' 用途区分(入荷)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const YOTO_INKA As String = "01"

    ''' <summary>
    ''' 入荷大ステータス(正常)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const STATUS_SEIJO As String = "00"

    ''' <summary>
    ''' 入荷大ステータス(保留)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const STATUS_HORYU As String = "05"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        INKA = 0
        NYUKA
        EDIKANRINO
        STATUS
        KANRINOL
        EIGYO
        WH
        NYUKATYPE
        NYUKAKBN
        SHINSHOKUKBN
        NYUKADATE
        BUYERORDNO
        ORDERNO
        FREE
        HOKANDATE
        CUSTCDL
        CUSTCDM
        CUSTNM
        TAX
        TOUKIHO
        ZENKIHO
        NIYAKUUMU
        PLANQT
        PLANQTUT
        NYUKACNT
        NYUBANL
        NYUKACOMMENT
        MIDDLE
        GOODS
        REVIVAL
        DEL
        GOODSDEF
        KANRINOM
        GOODSCD
        GOODSNM
        GOODSKEY
        KOSU
        HASU
        IRISU
        STDIRIME
        ONDO
        SUMANT
        SUMCNT
        TARE
        IRIME
        ORDERNOM
        BUYERORDNOM
        LOT
        GOODSCOMMENT
        SERIAL
        UNSO
        PNL_UNSO
        UNCHINTEHAI
        TARIFFKBN
        SHARYOKBN
        ONKAN
        UNCHIN
        UNSOCD
        UNSOBRCD
        UNSONM
        TARIFFCD
        TARIFFNM
        SHUKKAMOTOCD
        SHUKKAMOTONM
        FREEL
        FREEINPUTSL
        TAB_FREE
        FREEM
        FREEINPUTSM

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対(INKA_M)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprInkaMColumnIndex

        DEF = 0
        JYOTAI_NM
        EDI_CTL_NO_CHU
        CUST_GOODS_CD
        GOODS_NM
        NB
        SURYO
        REMARK
        OUTKA_FROM_ORD_NO
        RECNO

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対(FREE)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprFREEColumnIndex

        DEF = 0
        TITLE
        FREE
        COLNM

    End Enum

    ''' <summary>
    ''' アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ActionType As Integer

        EDIT = 0
        MASTEROPEN
        SAVE
        CLOSE
        ENTER
        REVIVAL
        DEL
        DOUBLECLICK

    End Enum

End Class
