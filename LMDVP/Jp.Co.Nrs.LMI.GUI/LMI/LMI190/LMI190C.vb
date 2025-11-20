' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI190  : ハネウェル管理
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LMI190定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI190C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI190IN"
    Public Const TABLE_NM_OUT As String = "LMI190OUT"

    '桁数
    Public Const NB_MAX_4 As String = "9999"

    '一括変更項目値
    Public Const EDIT_SELECT_SHIPCD As String = "01"     '荷送人コード
    Public Const EDIT_SELECT_SHIPNM As String = "02"     '荷送人名

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        GRPMODE = 0
        OPTZAIKO
        OPTSHUKKA
        OPTHAIKI
        EIGYO
        SERIALNO
        CYLINDERTYPE
        TOFROMNM
        COOLANTGOODSKB               '2013.08.15 要望対応2095
        KEIKADATE
        KIJUNDATE
        IDODATEFROM
        IDODATETO
        BTNEXCEL
        GRPMIIRI
        OPTKARA
        OPTMIIRI
        OPTKARAMIIRI
        GRPINKAOUTKA
        OPTINKA
        OPTOUTKA
        OPTINKAOUTKA
        CHIENDATE
        SPRDETAILS

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        GETDATA = 0
        HENSHU
        'SUZUSHO
        'HENKHAKU
        'SHUKKA
        GETLOG
        HAIKI
        HAIKIKAIJO
        KENSAKU
        TEIKIKENSAKANRI
        HOZON
        CLOSE
        EXCEL
        CHANGEOPTBUTTOM
        DOUBLECLICK
        HENKYAKUSHUKKA
        MASTEROPEN
        ENTER
        IMPORT_N40CD

    End Enum

    '''' <summary>
    '''' Spread部列インデックス用列挙対(在庫検索の場合)
    '''' </summary>
    '''' <remarks></remarks>
    'Public Enum SprColumnIndexZAIKO

    '    DEF = 0
    '    EMPTYKB
    '    CYLINDERTYPE
    '    TOFROMNM
    '    SERIALNO
    '    YOUKINO
    '    INOUTDATE
    '    NEXTTESTDATE
    '    KEIKADATE1
    '    IOZSKB
    '    SHIPCDL
    '    SHIPNML
    '    BUYERORDNO
    '    TOFROMCD
    '    SEIQTO
    '    KEISANSTARTDATE
    '    KEIKADATE2
    '    CHIENDATE
    '    CHIENMONEY
    '    INOUTKANOL
    '    INOUTKANOM
    '    INOUTKANOS
    '    SYSUPDDATE
    '    SYSUPDTIME
    '    IOZSKBCD
    '    LAST

    'End Enum

    '''' <summary>
    '''' Spread部列インデックス用列挙対(履歴・廃棄済検索の場合)
    '''' </summary>
    '''' <remarks></remarks>
    'Public Enum SprColumnIndexRIREKI_HAIKI

    '    DEF = 0
    '    SERIALNO
    '    TOFROMNM
    '    INOUTDATE
    '    EMPTYKB
    '    IOZSKB
    '    HAIKIYN
    '    SHIPCDL
    '    SHIPCDL_EDIT
    '    SHIPNML
    '    INOUTKANOL
    '    INOUTKANOM
    '    INOUTKANOS
    '    SYSUPDDATE
    '    SYSUPDTIME
    '    CUSTCDL
    '    IOZSKBCD
    '    LAST

    'End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndexSPRALL

        DEF = 0
        EMPTYKB
        CYLINDERTYPE
        TOFROMNM
        SERIALNO
        'TOFROMNMRIREKI
        YOUKINO
        INOUTDATE
        'EMPTYKBRIREKI
        NEXTTESTDATE
        PROD_DATE           'ADD 2019/10/29 006786 製造日
        KEIKADATE1
        IOZSKB
        HAIKIYN
        SHIPCDL
        SHIPNML
        BUYERORDNO
        TOFROMCD
        SEIQTO
        KEISANSTARTDATE
        KEIKADATE2
        CHIENDATE
        CHIENMONEY
        INOUTKANOL
        INOUTKANOM
        INOUTKANOS
        SYSUPDDATE
        SYSUPDTIME
        CUSTCDL
        IOZSKBCD
        GOODS_CD_CUST       'ADD 2019/10/31 008262　商品コード
        GOODS_NM            'ADD 2019/10/31 008262　商品名
        SEARCH_KEY_2        'ADD 2019/12/10 009849　荷主カテゴリ2
        REMARK_IN
        LAST

    End Enum

    Public Enum EditCD

        SHIPCD = 1
        SHIPNM

    End Enum

    ''' <summary>
    ''' N40コード取込DataTable件数上限
    ''' </summary>
    ''' <remarks>
    ''' Excelのデータ件数が多すぎて一度に処理できないことがあるので一定数ごとに分割して処理する
    ''' </remarks>
    Public Const N40ImpRowsMax As Integer = 5000

End Class
