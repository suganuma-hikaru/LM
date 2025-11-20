' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM080C : 運送会社マスタメンテ
'  作  成  者       :  hirayama
' ==========================================================================

''' <summary>
''' LMM080定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM080C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM080IN"
    Public Const TABLE_NM_OUT As String = "LMM080OUT"
    Public Const TABLE_NM_CUSTRPT As String = "LMM080_UNSO_CUST_RPT"
    Public Const TABLE_NM_DATE As String = "LMM080DATE"

    'メッセージ置換文字
    Public Const UNSOCD As String = "運送会社コード"
    Public Const UNSOBRCD As String = "運送会社支店コード"
    Public Const MOTOCHAKUKBN As String = "元着払区分"

    Public Const MOTOKBN As String = "元払いの"
    Public Const CHAKUKBN As String = "着払いの"

    '有無フラグ
    Public Const KBN_00 As String = "00"
    Public Const KBN_01 As String = "01"

    '元着払区分
    Public Const MOTO As String = "01"
    Public Const CHAKU As String = "02"

#If False Then ' 名鉄対応(2499) 2016.1.27 changed inoue
    '帳票ID
    Public Const PTNID As String = "11"
#Else
    '帳票ID(送り状)
    Public Const INVOICE_PTNID As String = "11"

    '帳票ID(荷札)
    Public Const TAG_PTNID As String = "10"

#End If

    '前ゼロ
    Public Const MAEZERO As String = "000"

    '運送会社荷主別送り状マスタ入力チェック
    Public Const NRSBRCD As String = "NRS_BR_CD"
    Public Const UNSOCOCD As String = "UNSOCO_CD"
    Public Const UNSOCOBRCD As String = "UNSOCO_BR_CD"
    Public Const MOTOTYAKUKB As String = "MOTO_TYAKU_KB"
    Public Const CUSTCDL As String = "CUST_CD_L"
    Public Const CUSTCDM As String = "CUST_CD_M"
#If True Then ' 名鉄対応(2499) 2016.1.27 added inoue
    Public Const PTN_ID As String = "PTN_ID"

#End If

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
        INIT
        KENSAKU
        SANSHO
        SHINKI
        HENSHU
        HUKUSHA
        SAKUJO
        HOZON
        MASTEROPEN
        ENTER
        TOJIRU
        DCLICK
        INS_T           '行追加
        DEL_T           '行削除

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        DETAIL = 0
        NRSBRCD
        UNSOCOCD
        UNSOCONM
        UNSOCOBRCD
        UNSOCOBRNM
        UNSOCOKB
        ZIP
        AD1
        AD2
        AD3
        MOTOUKEKB
        PNLCOST
        '(2012.08.17)支払サブ機能対応 --- START ---
        SHIHARAITOCD
        SHIHARAITONM
        '(2012.08.17)支払サブ機能対応 ---  END  ---
        UNCHINTARIFFCD
        UNCHINTARIFFREM
        EXTCTARIFFCD
        EXTCTARIFFREM
        BETUKYORICD
        BETUKYORIREM
        DETAIL2
        LASTPUTIME
        NIHUDAYN
        TAREYN
        TEL
#If True Then ' FFEM荷札検品対応 20160610 added inoue
        TAGBCDKBN
#End If
        FAX
        URL
        PIC
        NRSSBETUCD
        '要望番号:1275 yamanaka 2012.07.13 Start
        CUST_UNSO_RYAKU_NM
        '要望番号:1275 yamanaka 2012.07.13 End
        '要望番号:2140 kobayashi 2013.12.24 Start
        PICKLIST_GRP_KBN
        '要望番号:2140 kobayashi 2013.12.24 End
        EDI_USED_KBN
        WH_NIFUDA_SCAN_YN
        SITUATION
        CRTDATE
        CRTUSER
        UPDUSER
        UPDDATE
        UPDTIME
        SYSDELFLG

    End Enum

    ''' <summary>
    ''' Spread部(上部)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SYS_DEL_NM
        NRS_BR_CD
        NRS_BR_NM
        UNSOCO_CD
        UNSOCO_BR_CD
        UNSOCO_NM
        UNSOCO_BR_NM
        MOTOUKE_KB
        MOTOUKE_KB_NM
        UNSOCO_KB
        ZIP
        AD_1
        AD_2
        AD_3
        TEL
        FAX
        URL
        PIC
        NRS_SBETU_CD
        NIHUDA_YN
        TARE_YN
        '(2012.08.17)支払サブ機能対応 --- START ---
        SHIHARAITO_CD
        SHIHARAITO_NM
        '(2012.08.17)支払サブ機能対応 ---  END  ---
        UNCHIN_TARIFF_CD
        UNCHIN_TARIFF_REM
        EXTC_TARIFF_CD
        EXTC_TARIFF_REM
        BETU_KYORI_CD
        BETU_KYORI_REM
        LAST_PU_TIME
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG
        '要望番号:1275 yamanaka 2012.07.13 Start
        CUST_UNSO_RYAKU_NM
        '要望番号:1275 yamanaka 2012.07.13 End
        '要望番号:2140 kobayashi 2013.12.24 Start
        PICKLIST_GRP_KBN
        '要望番号:2140 kobayashi 2013.12.24 End
        EDI_USED_KBN
        '(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正開始 --- 
        NIFUDA_SCAN_YN
        '(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正終了 --- 
        WH_NIFUDA_SCAN_YN
        AUTO_DENP_NO_FLG '要望番号:2408 2015.09.17 ADD
        AUTO_DENP_NO_KBN '要望番号:2408 2015.09.17 ADD
#If True Then   'FFEM荷札検品対応 20160610 added inoue
        TAG_BARCD_KBN
#End If

        CLMCNT

    End Enum

    ''' <summary>
    ''' Spread部(下部)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex2

        DEF = 0
        CUST_CD_L
        CUST_CD_M
#If False Then ' 名鉄対応(2499) 2016.01.27 changed inoue
        MOTO_TYAKU_KB
        PTN_CD
        PTN_ID
#Else
        PTN_ID
        MOTO_TYAKU_KB
        PTN_CD
#End If
        ROW_INDEX
#If True Then ' 名鉄対応(2499) 2016.01.27 added inoue
        COLUMN_COUNT
#End If


    End Enum

End Class
