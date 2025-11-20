' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI400C : セット品マスタメンテ
'  作  成  者       :  yamanaka
' ==========================================================================

''' <summary>
''' LMI400定数定義クラス
''' </summary>
''' <remarks></remarks>
Friend Class LMI400C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TABLE_NM_IN As String = "LMI400IN"
    Friend Const TABLE_NM_OUT As String = "LMI400OUT"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum EventShubetsu As Integer

        SHINKI = 0
        HENSHU
        SAKUJO_HUKKATU
        KENSAKU
        MASTEROPEN
        HOZON
        TOJIRU
        DOUBLE_CLICK
        ENTER
        ROW_ADD
        ROW_DEL

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum CtlTabIndex

        SPR_SEARCH = 0
        CMB_NRS_BR
        TXT_OYA_CD
        TXT_OYA_NM
        BTN_ADD
        BTN_DEL
        SPR_DETAIL

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SprSearchColumnIndex

        DEF = 0
        STATUS
        NRS_BR_NM
        OYA_CD
        OYA_NM

        NRS_BR_CD
        CREATE_USER
        CREATE_DATE
        UPDATE_USER
        UPDATE_DATE
        UPDATE_TIME
        SYS_DEL_FLG
        CLM_NM


    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SprDetailColumnIndex

        DEF = 0
        KO_CD
        KO_NM
        KOSU
        CLM_NM


    End Enum

End Class
