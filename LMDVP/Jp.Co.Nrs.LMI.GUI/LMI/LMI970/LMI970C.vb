' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI970C : 運賃データ入力・確認（千葉日産物流）
'  作  成  者       :  Minagawa
' ==========================================================================

''' <summary>
''' LMI970C定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI970C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' 検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SELECT As String = "SelectListData"

    ''' <summary>
    ''' 保存アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_UPDATE_YUSOIRAI As String = "UpdateYusoIrai"

    ''' <summary>
    ''' 実績作成アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_INSERT_SENDUNCHIN As String = "InsertSendUnchin"

    ''' <summary>
    ''' 印刷アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_PRINTDATA As String = "PrintData"

    ''' <summary>
    ''' 単価更新アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_UPDATE_TANKA As String = "UpdateYusoIraiTanka"    'ADD 2019/05/30 要望管理006030

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMI970IN"

    ''' <summary>
    ''' OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_OUT As String = "LMI970OUT"

    ''' <summary>
    ''' 実績作成対象テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_SENDUNCHIN_TARGET As String = "LMI970SENDUNCHIN_TARGET"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        JISSEKI_SAKUSEI = 0     '実績作成
        HENSHU                  '編集
        KENSAKU                 '検索
        HOZON                   '保存
        CLOSE                   '閉じる
        PRINT                   '印刷
        UPDATE_TANKA            '単価更新   'ADD 2019/05/30 要望管理006030

    End Enum


    ''' <summary>
    ''' アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ActionType As Integer

        KENSAKU = 0
        ENTER
        MASTEROPEN
        DOUBLECLICK
        SAVE
        CLOSE
        LOOPEDIT
        PRINT
        SAIKEISAN
        BACKUP

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        CONDITION = 0
        NRS_BR_CD
        CUST_CD_L
        CUST_NM
        TORIKOMI_DATE_FROM
        TORIKOMI_DATE_TO
        SEARCH_DATE_KB
        SEARCH_DATE_FROM
        SEARCH_DATE_TO
        CMB_PRINT
        BTN_PRINT
        'ADD START 2019/05/30 要望管理006030
        PNL_UPDATE_TANKA
        TARGET_YM
        TANKA
        BTN_UPDATE_TANKA
        'ADD END   2019/05/30 要望管理006030
        DETAIL

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SHORI_KB
        HORYU_KB
        PRINT_KB    'ADD 2019/05/30 要望管理006030
        SEND_KB
        KOJO_KANRI_NO
        SHUKKA_DATE
        NONYU_DATE
        DEST_KAISHA_NM
        DEST_KAISHA_JUSHO
        HINMEI
        JURYO       'ADD 2019/05/30 要望管理006030
        TANKA       'ADD 2019/05/30 要望管理006030
        UNCHIN
        CRT_DATE
        FILE_NAME
        REC_NO
        SYS_UPD_DATE
        SYS_UPD_TIME
        LAST

    End Enum

    ''' <summary>
    ''' 検索日付区分(cmbSearchDateKb選択肢)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CmbSearchDateKbItems
        Public Const Blank As String = ""
        Public Const ShukkaDate As String = "出荷日"
        Public Const NonyuDate As String = "納入日"
    End Class

    ''' <summary>
    ''' 印刷区分(cmbPrint選択肢)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CmbPrintItems
        Public Const Blank As String = ""
        Public Const UnsoIraisho As String = "運送依頼書"
    End Class

    ''' <summary>
    ''' 送信区分名称
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SendKbName
        Public Const Misoushin As String = "未"
        Public Const SoushinZumi As String = "済"
    End Class

    'ADD START 2019/05/30 要望管理006030
    ''' <summary>
    ''' 印刷区分名称
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PrintKbName
        Public Const Mi As String = "未"
        Public Const Sumi As String = "済"
    End Class
    'ADD END   2019/05/30 要望管理006030

End Class
