' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM280C : 横持ちマスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================

''' <summary>
''' LMM280定数定義クラス
''' </summary>
''' <remarks></remarks>
Friend Class LMM280C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TABLE_NM_IN As String = "LMM280IN"
    Friend Const TABLE_NM_YOKO_HED As String = "LMM280OUT"
    Friend Const TABLE_NM_YOKO_DTL As String = "LMM280_YOKO_TARIFF_DTL"

    ''' <summary>
    ''' 計算コード区分【区分マスタ：K012】
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const KEISAN_CD_NISUGATA As String = "01"    '荷姿建て	
    Friend Const KEISAN_CD_SHADATE As String = "02"     '車建て		
    Friend Const KEISAN_CD_TEIZO_UNCHIN As String = "03"     '逓増運賃	
    Friend Const KEISAN_CD_JURYO As String = "04"     '重量建て	

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum EventShubetsu As Integer

        SHINKI = 0
        HENSHU
        FUKUSHA
        SAKUJO_HUKKATU
        KENSAKU
        HOZON
        TOJIRU
        DOUBLE_CLICK
        ENTER
        ADD_ROW
        DEL_ROW

    End Enum

    ''' <summary>
    ''' 列スタイル設定用
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SetDtlCol As Integer

        NISUGATA = 0
        SHASHU
        KUGIRI_JURYO
        KIKENHIN
        TANKA_PER_KGS
        TANKA

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum CtlTabIndex

        SPR_YOKO_HED = 0
        CMB_BR
        TXT_YOKOMOCHI_TARIFF_CD
        TXT_BIKO
        CMB_KEISANHOHO
        CMB_MEISAI_BUNKATU
        NUM_MIN_HOSHO
        BTN_ADD
        BTN_DEL
        SPR_YOKO_DTL

    End Enum

    ''' <summary>
    ''' Spread(YokomochiHed)部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SprYokoTariffColumnIndex

        DEF = 0
        STATUS
        BR_CD
        BR_NM
        YOKOMOCHI_TARIFF_CD
        BIKO
        KEISAN_CD_KBN
        KEISANHOHO
        MEISAI_BUNKATU_FLG
        MEISAI_BUNKATU
        MIN_HOSHO
        CREATE_DATE
        CREATE_USER
        UPDATE_DATE
        UPDATE_USER
        UPDATE_TIME
        SYS_DEL_FLG

    End Enum

    ''' <summary>
    ''' Spread(YokomochiDtl)部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SprYokoTariffDtlColumnIndex

        DEF = 0        
        NISUGATA_KBN
        SHASHU
        KUGIRI_JURYO
        KIKENHIN
        TANKA_PER_KGS
        TANKA
        EDA_NO

    End Enum

End Class
