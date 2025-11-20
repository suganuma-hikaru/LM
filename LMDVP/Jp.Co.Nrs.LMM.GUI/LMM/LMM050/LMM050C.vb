' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM050C : 請求先マスタメンテ
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMM050定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM050C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM050IN"
    Public Const TABLE_NM_OUT As String = "LMM050OUT"
    Public Const TABLE_NM_BUSYO_CD As String = "LMM050_BUSYO_CD"
    Public Const TABLE_NM_VAR_STRAGE As String = "LMM050_VAR_STRAGE"

    'コンボボックス初期値
    Public Const KOUZA As String = "01"
    Public Const HONSHA As String = "00"
    Public Const BUTSURYU_CENTER As String = "01"  'K011:鑑名義区分 01:物流センター  ADD 2018/11/20 要望番号002425
    Public Const MATUJIME As String = "00"
    Public Const TEKIYOUNO As String = "0"
    Public Const VAR_RATE_3 As String = "1.3"
    Public Const VAR_RATE_6 As String = "1.6"
    '帳票パターン
    Public Const PTNID53 As String = "53"
    'START YANAI 要望番号661
    Public Const PTNID77 As String = "77"
    'END YANAI 要望番号661

    'メッセージ置換文字
    Public Const SEIQTOMSG As String = "請求先コード"

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

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        DETAIL = 0
        SEIQTOCD
        SEIQTONM
        '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start
        EIGYOTANTO
        '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add end
        SEIQTOBUSYONM
        CLOSEKBN

        KOUZAKBN
        MEIGIKBN
        NRSKEIRICD1
        NRSKEIRICD2
        SEIQSNDPERIOD
        '(2013.03.14)要望番号1950 レイアウト変更に伴い、順番変更 -- START --
        OYAPIC
        TEL
        FAX
        ZIP
        AD1
        AD2
        AD3
        REMARK
        CUSTKAGAMITYPE1
        CUSTKAGAMITYPE2
        CUSTKAGAMITYPE3
        '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 -- START --
        CUSTKAGAMITYPE4
        CUSTKAGAMITYPE5
        CUSTKAGAMITYPE6
        CUSTKAGAMITYPE7
        CUSTKAGAMITYPE8
        CUSTKAGAMITYPE9
        '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 --  END  --
        '(2013.03.14)要望番号1950 レイアウト変更に伴い、順番変更 --  END  --
        OPT_VAR_STRAGE
        OPT_VAR_STRAGE_FLG_N
        OPT_VAR_STRAGE_FLG_Y
        CMB_VAR_RATE_3
        CMB_VAR_RATE_6
        SEIQ
        STORAGENR
        STORAGENG
        STORAGEMIN
        STORAGEOTHERMIN
        STORAGEZEROFLG
        STORAGETOTALFLG
        HANDLINGNR
        HANDLINGNG
        HANDLINGMIN
        HANDLINGOTHERMIN
        HANDLINGZEROFLG
        HANDLINGTOTALFLG
        UNCHINNR
        UNCHINNG
        UNCHINMIN
        UNCHINTOTALFLG
        SAGYONR
        SAGYONG
        SAGYOMIN
        SAGYOTOTALFLG
        CLEARANCENR
        CLEARANCENG
        YOKOMOCHINR
        YOKOMOCHING
        TOTALNR
        TOTALNG
        TOTALMIN
        DOCPTN
        SEI
        FUKU
        HIKAE
        KEIRI
        'START YANAI 要望番号661
        DOCPTNNOMAL
        'END YANAI 要望番号661
        SITUATION
        CRTDATE
        CRTUSER
        UPDUSER
        UPDDATE
        UPDTIME
        SYSDELFLG
        TOTAL_MIN_SEIQ_AMT
        TOTAL_MIN_SEIQ_CURR_CD
        STORAGE_TOTAL_FLG
        HANDLING_TOTAL_FLG
        UNCHIN_TOTAL_FLG
        SAGYO_TOTAL_FLG
    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SYS_DEL_NM
        NRS_BR_CD
        NRS_BR_NM
        SEIQTO_CD
        SEIQTO_NM
        SEIQTO_BUSYO_NM
        KOUZA_KB
        KOUZA_KB_NM
        MEIGI_KB
        OYA_PIC
        ZIP
        AD_1
        AD_2
        AD_3
        REMARK          'ADD 2019/07/10 002520

        TEL
        FAX
        CLOSE_KB
        CLOSE_KB_NM
        DOC_PTN
        'START YANAI 要望番号661
        DOC_PTN_NOMAL
        'END YANAI 要望番号661
        DOC_SEI_YN
        DOC_HUKU_YN
        DOC_HIKAE_YN
        DOC_KEIRI_YN
        DOC_DEST_YN　　　　'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
        NRS_KEIRI_CD1
        NRS_KEIRI_CD2
        SEIQ_SND_PERIOD
        TOTAL_NR
        STORAGE_NR
        HANDLING_NR
        UNCHIN_NR
        SAGYO_NR
        CLEARANCE_NR
        YOKOMOCHI_NR
        TOTAL_NG
        STORAGE_NG
        HANDLING_NG
        UNCHIN_NG
        SAGYO_NG
        CLEARANCE_NG
        YOKOMOCHI_NG
        STORAGE_MIN
        CUST_KAGAMI_TYPE1
        CUST_KAGAMI_TYPE2
        CUST_KAGAMI_TYPE3
        '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 -- START --
        CUST_KAGAMI_TYPE4
        CUST_KAGAMI_TYPE5
        CUST_KAGAMI_TYPE6
        CUST_KAGAMI_TYPE7
        CUST_KAGAMI_TYPE8
        CUST_KAGAMI_TYPE9
        '(2013.03.14)要望番号1950 荷主鑑分類種別 追加 --  END  --
        '(2014.09.17)要望番号2229 請求通貨追加 追加 -- START --
        SEIQ_CURR_CD
        TOTAL_NG_CURR_CD
        STORAGE_NG_CURR_CD
        HANDLING_NG_CURR_CD
        UNCHIN_NG_CURR_CD
        SAGYO_NG_CURR_CD
        CLEARANCE_NG_CURR_CD
        YOKOMOCHI_NG_CURR_CD
        STORAGE_MIN_CURR_CD
        '(2014.09.17)要望番号2229 請求通貨追加 追加 --  END  --
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG
        TOTAL_MIN_SEIQ_AMT
        TOTAL_MIN_SEIQ_CURR_CD
        STORAGE_TOTAL_FLG
        HANDLING_TOTAL_FLG
        UNCHIN_TOTAL_FLG
        SAGYO_TOTAL_FLG
        STORAGE_MIN_AMT
        STORAGE_MIN_AMT_CURR_CD
        STORAGE_OTHER_MIN_AMT
        STORAGE_OTHER_MIN_AMT_CURR_CD
        HANDLING_MIN_AMT
        HANDLING_MIN_AMT_CURR_CD
        HANDLING_OTHER_MIN_AMT
        HANDLING_OTHER_MIN_AMT_CURR_CD
        UNCHIN_MIN_AMT
        UNCHIN_MIN_AMT_CURR_CD
        SAGYO_MIN_AMT
        SAGYO_MIN_AMT_CURR_CD
        STORAGE_ZERO_FLG
        HANDLING_ZERO_FLG
        '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add start
        EIGYO_TANTO
        '2018/04/16 001485 【LMS】荷主マスタ_営業担当者ID-項目追加(PS高橋) Annen Add end
        VAR_STRAGE_FLG
        VAR_RATE_3
        VAR_RATE_6
        LAST
    End Enum

End Class
