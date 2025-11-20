' ===================_9 as strin     = ""=======================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF020C : 運送入力
'  作  成  者       :  [ito]
' ==========================================================================

''' <summary>
''' LMF020定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF020C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMF020IN"

    ''' <summary>
    ''' F_UNSO_Lテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_UNSO_L As String = "F_UNSO_L"

    ''' <summary>
    ''' F_UNSO_Mテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_UNSO_M As String = "F_UNSO_M"

    ''' <summary>
    ''' UNCHIN_INFOテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_INFO As String = "UNCHIN_INFO"

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' SHIHARAI_INFOテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_SHIHARAI As String = "SHIHARAI_INFO"
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 初期検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_INIT_SELECT As String = "SelectInitData"

    ''' <summary>
    ''' 新規検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_INIT_NEW As String = "SelectNewData"

    ''' <summary>
    ''' 保存(新規)アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_INIT_SAVE As String = "InsertSaveAction"

    ''' <summary>
    ''' 保存(更新)アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_EDIT_SAVE As String = "UpdateSaveAction"

    ''' <summary>
    ''' 排他チェックアクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_HAITA_CHK As String = "ChkHaitaData"

    ''' <summary>
    ''' 対象運行データキャンセルチェックアクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_CANCEL_DATA As String = "ChkCancelData"

    ''' <summary>
    ''' 削除処理アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_DELETE As String = "DeleteAction"

    ''' <summary>
    ''' 印刷処理アクション(更新無)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_PRINT_ONLY As String = "DoPrint"

    ''' <summary>
    ''' 納品書
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_NOUHIN As String = "01"

    ''' <summary>
    ''' 送状
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_OKURI As String = "02"

    ''' <summary>
    ''' 荷札
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_NIFUDA As String = "03"

    ''' <summary>
    ''' 物品引取書
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_HIKITORI As String = "04"

    ''' <summary>
    ''' 梱包明細
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_KONPOU As String = "05"

#If True Then   'ADD 2018/11/21 依頼番号 : 002743   【LMS】運行運送情報画面_一括印刷機能

    ''' <summary>
    ''' 一括印刷
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_ALL As String = "06"

    'ADD 2022/01/26 026832 【LMS】運送保険料システム化_実装_運送保険申込書対応_運行・運送情報
    ''' <summary>
    ''' 荷札
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_UNSO_HOKEN As String = "07"

#End If

    ''' <summary>
    ''' 進捗(出荷済)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const STAGE_SHUKKAZUMI As String = "60"

    ''' <summary>
    ''' 進捗(報告済)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const STAGE_HOUKOKU As String = "90"

    ''' <summary>
    ''' 帳票パターン(送状)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PTN_ID_OKURIJO As String = "09"

    ''' <summary>
    ''' セット区分(届先)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SET_KBN_DEST As String = "01"

    ''' <summary>
    ''' 休日
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KYUJITU As String = "休日(Holiday)"

    ''' <summary>
    ''' 祝日
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUKUJITU As String = "祝日(Public Holiday)"

    ''' <summary>
    ''' 部数
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BUSU As String = "部数(Number of copies)"

    ''' <summary>
    ''' 印刷範囲(From)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_RANGE_FROM As String = "印刷範囲 From(Print Range From)"

    ''' <summary>
    ''' 印刷範囲(To)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_RANGE_TO As String = "印刷範囲 To(Print Range To)"

    ''' <summary>
    ''' 最大桁数(整数10桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_10 As String = "9,999,999,999"

    ''' <summary>
    ''' シャープ10
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHARP10 As String = "#,###,###,##0"

    ''' <summary>
    ''' 最大桁数(整数9桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_9 As String = "999,999,999"

    ''' <summary>
    ''' 最大桁数(整数9桁　小数3桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_9_3 As String = "999,999,999.999"

    'START 要望番号1243 赤データの表示・・EDI検索
    ''' <summary>
    ''' 最小桁数(整数9桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MIN_9 As String = "-999,999,999"
    'END 要望番号1243 赤データの表示・・EDI検索

    ''' <summary>
    ''' シャープ9
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHARP9 As String = "###,###,##0"

    ''' <summary>
    ''' 最大桁数(整数5桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_5 As String = "99,999"

    ''' <summary>
    ''' シャープ5
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHARP5 As String = "##,##0"

    ''' <summary>
    ''' 最大桁数(整数2桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_2 As String = "99"

    '要望対応:1816 yamanaka 2013.02.22 Start
    ''' <summary>
    ''' 最大桁数(整数3桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_3 As String = "999"
    '要望対応:1816 yamanaka 2013.02.22 End

    ''' <summary>
    ''' シャープ2
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHARP2 As String = "#0"

    ''' <summary>
    ''' 運送番号(中)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const UNSO_L_NM As String = "運送番号(中)"

    ''' <summary>
    ''' 括弧(
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KAKKO_1 As String = "("

    ''' <summary>
    ''' 括弧)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KAKKO_2 As String = ")"

    ''' <summary>
    ''' テーブルタイプ(宅急便)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_TYPE_TAK As String = "06"

    ''' <summary>
    ''' タリフタイプが宅急便
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TYPE_TAKKYUBIN As String = "タリフタイプが宅急便"

    '2017/12/14 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add start
    Public Const TABLE_NM_OKURIJYO_WK As String = "LMF020_OKURIJYO_WK"
    '2017/12/14 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add end

    ''' <summary>
    ''' アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ActionType As Integer

        INIT = 0
        UNSO_NEW            'ADD 2018/06/25
        EDIT
        DELETE
        MASTEROPEN
        SAVE
        CLOSE
        ENTER
        ADD
        DEL
        CALC
        LEAVE
        COPY
        DEST_SAVE
        PRINT
        'START YANAI 要望番号1241 運送検索：運送複写機能を追加する
        COPY_INIT '初期処理での複写時
        'END YANAI 要望番号1241 運送検索：運送複写機能を追加する

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        PRINT = 0
        PRTCNT
        PRTCNT_FROM
        PRTCNT_TO
        BTNPRINT
        UNSO
        EIGYO
        YUSOEIGYO
        UNSONO
        MOTODATAKBN
        UNSOJIYUKBN
        PCKBN
        TAXKB
        UNKONO
        TEHAIKBN
        BINKBN
        TARIFFKBN
        SHARYOKBN
        KANRINO
        UNSOCOCD
        UNSOCOBRCD
        UNSOCONM
        OKURINO
        CUSTCDL
        CUSTCDM
        CUSTNM
        ORDNO
        SHIPCD
        SHIPNM
        BUYERORDNO
        TARIFFCD
        TARIFFREM
        EXTCTARIFFCD
        EXTCTARIFFREM
        PAYTARIFFCD             'ADD UMANO 要望番号1302 支払運賃に伴う修正。
        PAYTARIFFREM            'ADD UMANO 要望番号1302 支払運賃に伴う修正。
        PAYEXTCTARIFFCD         'ADD UMANO 要望番号1302 支払運賃に伴う修正。
        PAYEXTCTARIFFREM        'ADD UMANO 要望番号1302 支払運賃に伴う修正。
        ORIGDEST
        ORIGDATE
        ORIGTIME
        ORIGCD
        ORIGNM
        ORIGJISCD
        DESTDATE
        DESTTIME
        DESTJITIME
        AUTO_DENP_KBN                '要望番号:2408 2015.09.17 ADD
        AUTO_DENP_NO                 '要望番号:2408 2015.09.17 ADD
        DESTCD
        DESTNM
        DESTJISCD
        ZIPNO
        DESTADD1
        DESTADD2
        DESTADD3
        '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add start
        TEL_NO
        '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add end
        AREACD
        AREANM
        UNSOPKGCNT
        UNSOWT_L
        UNSOCNT_UT
        THERMOKBN
        KEISAN
        UNSOCOMMENT
        REMARK
        CARGO
        ADD
        DEL
        DETAIL
        UNSOWT
        SEIQTARIFFDES
        SEIQUNCHIN
        PAYUNCHIN
        CITYEXTC
        WINTEXTC
        RELYEXTC
        PASSEXTC
        INSUREXTC
        PAYUNSOWT               'ADD UMANO 要望番号1302 支払運賃に伴う修正。
        PAYSEIQTARIFFDES        'ADD UMANO 要望番号1302 支払運賃に伴う修正。
        PAYPAYUNCHIN            'ADD UMANO 要望番号1302 支払運賃に伴う修正。
        PAYCITYEXTC             'ADD UMANO 要望番号1302 支払運賃に伴う修正。
        PAYWINTEXTC             'ADD UMANO 要望番号1302 支払運賃に伴う修正。
        PAYRELYEXTC             'ADD UMANO 要望番号1302 支払運賃に伴う修正。
        PAYPASSEXTC             'ADD UMANO 要望番号1302 支払運賃に伴う修正。
        PAYINSUREXTC            'ADD UMANO 要望番号1302 支払運賃に伴う修正。

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        PRT_ORDER       'ADD 2018/11/28 依頼番号 : 003400   【LMS】運送情報画面_印刷順変更機能追加
        GOODS_CD
        '要望対応:1816 yamanaka 2013.02.22 Start
        GOODS_CD_CUST
        '要望対応:1816 yamanaka 2013.02.22 End
        GOODS_NM
        LOT_NO
        JURYO
        UNSO_KOSU
        KOSU_TANI
        UNSO_SURYO
        SURYO_TANI
        HASU
        ZAI_REC_NO
        ONDO_KANRI
        IRIME
        IRIME_TANI
        REMARK
        SIZE
        KONPO_KOSU
        ZAI_BUKA
        HOKA_BUKA
        STD_IRIME_NB
        STD_WT_KGS
        CALC_FLG
        TARE_YN
        REC_NO
        UNSO_HOKEN_UM       'ADD 2022/01/24 026832
        KITAKU_GOODS_UP     'ADD 2022/01/12 026832
        CLM_NUM

    End Enum

    ''' <summary>
    ''' ロック制御リスト
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum Lock As Integer

        EDIT = 0
        UNSO
        REPT
        LOCK
        TRIP
        '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 Start
        CUST
        '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 End

    End Enum

End Class
