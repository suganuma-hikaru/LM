' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF030C : 運行情報入力
'  作  成  者       :  [ito]
' ==========================================================================

''' <summary>
''' LMF030定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF030C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMF030IN"

    '2022.09.06 追加START
    ''' <summary>
    ''' INテーブル車輌マスタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN_CAR As String = "LMF030IN_CAR"

    ''' <summary>
    ''' OUTテーブル車輌マスタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_OUT_CAR As String = "LMF030OUT_CAR"
    '2022.09.06 追加END

    ''' <summary>
    ''' F_UNSO_Lテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_UNSO_L As String = "F_UNSO_L"

    ''' <summary>
    ''' F_UNSO_LLテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_UNSO_LL As String = "F_UNSO_LL"

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' F_SHIHARAI_TRSテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_SHIHARAI As String = "F_SHIHARAI_TRS"

    ''' <summary>
    ''' LMF030IN_KEISANテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN_KEISAN As String = "LMF030IN_KEISAN"
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

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

    'START KIM 要望番号1485 支払い関連修正
    ''' <summary>
    ''' 保存(支払先更新)アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SHIHARAISAKI_SAVE As String = "ShiharaisakiSaveAction"
    'END KIM 要望番号1485 支払い関連修正

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
    ''' 運送(大)データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT_L As String = "SelectUnsoLData"

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 保存(計算)アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_KEISAN_SAVE As String = "KeisanSaveAction"
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    '2022.09.06 追加START
    ''' <summary>
    ''' 車輌マスタ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_CAR_SELECT As String = "SelectCarData"
    '2022.09.06 追加END

    ''' <summary>
    ''' 最大桁数(整数9桁　小数3桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_9_3 As String = "999,999,999.999"

    ''' <summary>
    ''' シャープ9_3
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHARP9_3 As String = "###,###,##0.000"

    ''' <summary>
    ''' 最小桁数(整数2桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MIN_2 As String = "-99"

    ''' <summary>
    ''' 最大桁数(整数2桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_2 As String = "99"

    ''' <summary>
    ''' シャープ2
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHARP2 As String = "#0"

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
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
    ''' 最大桁数(整数3桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_3 As String = "999"

    ''' <summary>
    ''' シャープ3
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHARP3 As String = "##0"
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    ''' <summary>
    ''' 車輌KEY
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CAR_KEY As String = "車輌KEY"

    '要望番号2063 追加START 2015.05.27
    ''' <summary>
    ''' 手配作成(新規)アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_INIT_CREATE As String = "InsertTehaiAction"
    '要望番号2063 追加END 2015.05.27

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        UNSO = 0
        EIGYO
        TRIPNO
        TRIPDATE
        BINKBN
        CARNO
        CARTYPE
        CARKEY
        ONDOMM
        ONDOMX
        UNSOCOCD
        UNSOCOBRCD
        UNSOCONM
        DRIVERCD
        DRIVERNM
        JSHAKBN
        HAISO
        REMARK
        LOADWT
        UNSOONDO
        PAYAMT
        UNSONB
        TRIPWT
        REVUNCHIN
        'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        SHIHARAI
        SHIHARAIKINGAKU
        SHIHARAIWT
        SHIHARAIKENSU
        SHIHARAITARIFFKB
        SHIHARAITARIFFCD
        SHIHARAIEXTCCD
        SHIHARAIKEISAN
        'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        ROWADD
        ROWDEL
        DETAIL

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        BINKBN
        AREA
        NONYUDATE
        ORIG_NM
        ORIG_ADD
        DEST_NM
        DEST_ADD
        TASYA_WH_NM
        KOSU
        JURYO
        UNCHIN
        'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        SHIHARAIUNCHIN
        'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        CUST_NM
        CUST_REF_NO
        UNSO_NO
        KANRI_NO
        MOTO_DATA_KBN
        UNSO_REM
        UNSO_TEHAI_KBN
        ONKAN
        UNSO_CD
        UNSO_BR_CD
        REC_NO
        'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        OUTKA_PLAN_DATE
        LAST
        'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    End Enum

    ''' <summary>
    ''' アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ActionType As Integer

        INIT = 0
        EDIT
        DELETE
        MASTEROPEN
        SAVE
        CLOSE
        ENTER
        ADD
        DEL
        DOUBLECLICK
        'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        SHIHARAIKEISAN
        'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        TEHAICREATE             '要望管理 2063 追加START 2015.05.27

    End Enum

End Class
