' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI410H : ビックケミー取込データ確認／報告
'  作  成  者       :  [Umano]
' ==========================================================================

''' <summary>
''' LMI110定数定義クラス
''' </summary>
''' <remarks></remarks>
Friend Class LMI410C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TABLE_NM_IN As String = "LMI410IN"
    Friend Const TABLE_NM_OUT As String = "LMI410OUT"
    Friend Const TABLE_NM_IN_IDO_HOKOKU As String = "LMI410IN_IDO_HOKOKU"
    Friend Const TABLE_NM_IN_IKKATU_HENKO As String = "LMI410IN_IKKATU_HENKO"
    Friend Const TABLE_NM_H_IDOEDI_DTL_BYK As String = "H_IDOEDI_DTL_BYK"
    Friend Const TABLE_NM_H_SENDINOUTEDI_BYK As String = "H_SENDINOUTEDI_BYK"
    Friend Const TABLE_NM_H_SENDOUTEDI_BYK As String = "H_SENDOUTEDI_BYK"
    Friend Const TABLE_NM_GUIERROR As String = "LMI410_GUIERROR"

    ''' <summary>
    ''' 営業所コード（千葉）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NRS_BR_CD As String = "10"

    ''' <summary>
    ''' BYK初期値　荷主コード(大)（千葉）
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DEF_BYK_CUST As String = "00266"

    '2012.05.18 umano 追加START
    ''' <summary>
    ''' ガイダンス区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"

    ''' <summary>
    ''' エラーEXCEL COLTITLE
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXCEL_COLTITLE As String = "取込ファイル名"

    ''' <summary>
    ''' コンボValue値
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Public Const CMB_00 As String = "00"
    Public Const CMB_01 As String = "01"
    Public Const CMB_02 As String = "02"
    Public Const CMB_03 As String = "03"
    Public Const CMB_04 As String = "04"
    Public Const CMB_05 As String = "05"
    Public Const CMB_06 As String = "06"
    Public Const CMB_07 As String = "07"
    Public Const CMB_08 As String = "08"
    Public Const CMB_09 As String = "09"
    Public Const CMB_10 As String = "10"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum EventShubetsu As Integer

        TORIKOMI = 0
        IDO_HOUKOKU
        IDO_TORIKESI
        GAMEN_SENI
        PRINT
        KENSAKU
        MASTEROPEN
        CLOSE
        ENTER
        EXE
        IKKATU_HENKO

    End Enum

    ''' <summary>
    ''' コンボ種別
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum comboShubetsu As Integer

        JISSEKI_CMB = 0
        SEARCH_HOUKOKU_CMB
        IKKATU_CMB

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum CtlTabIndex

        CMB_EIGYO = 0
        TXT_CUST_CD_L
        TXT_CUST_CD_M
        TXT_CUST_NM
        TXT_USER_CD
        TXT_USER_NM
        CMB_JISSEKI_KBN
        CMB_SEARCH_KBN
        IMD_SEARCH_DATE_FROM
        IMD_SEARCH_DATE_TO
        BTN_JIKKOU
        CMB_IKKATU_KBN
        TXT_IKKATU_CUST_CD_L
        TXT_IKKATU_CUST_CD_M
        IMD_IKKATU_DATE
        BTN_IKKATU
        SPR_IDO_LIST
        SAGYO_STATE_KBN
        SYORI_KBN
        FILE_NAME
        PRINT_FLG
        TXT_BIKO
        TXT_CURRENT_MATERIAL
        TXT_CURRENT_DESCRIPTION
        TXT_CURRENT_GOODS_JOTAI
        TXT_CURRENT_BATCH
        TXT_DEST_NM
        TXT_DESTINATION_MATERIAL
        TXT_DESTINATION_DESCRIPTION
        TXT_DESTINATION_GOODS_JOTAI
        TXT_DESTINATION_BATCH

    End Enum

    ''' <summary>
    ''' Spread(Goods)部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SprIdoListColumnIndex

        DEF = 0
        CUST_CD_LM
        SAGYO_STATE_NM
        SYORI_SUB
        INOUTKA_DATE
        CRT_DATE
        CURRENT_MATERIAL
        CURRENT_DESCRIPTION
        CURRENT_GOODS_JOTAI
        CURRENT_BATCH
        CURRENT_QUANTITY
        YAJIRUSI
        DEST_NM
        DESTINATION_MATERIAL
        DESTINATION_DESCRIPTION
        DESTINATION_GOODS_JOTAI
        DESTINATION_BATCH
        DESTINATION_QUANTITY
        PRINT_KBN_NM
        SAGYO_NAIYO
        TEXT_NM
        FILE_NAME
        SYS_UPD_USER
        JISSEKI_SHORI_FLG
        SYORI_KBN
        PRINT_FLG
        CURRENT_STORAGE_LOCATION
        DESTINATION_STORAGE_LOCATION
        NRS_BR_CD
        REC_NO
        SYS_UPD_DATE
        SYS_UPD_TIME
        SAGYO_STATE_KBN
        LAST

    End Enum

End Class
