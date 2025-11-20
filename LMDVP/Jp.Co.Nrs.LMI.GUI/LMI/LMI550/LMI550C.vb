' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI550C : TSMC在庫照会
'  作  成  者       :  
' ==========================================================================

''' <summary>
''' LMI550定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI550C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' イベント種別用列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer
        SEARCH = 1              ' 検索
    End Enum

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TABLE_NM
        Public Const IN_SEARCH As String = "LMI550IN"
        Public Const OUT As String = "LMI550OUT"
    End Class

    ''' <summary>
    ''' タブインデックス用列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex_MAIN
        CMBEIGYO = 0
        CMBWARE
        CMBSEARCHDATE
        IMDSEARCHDATEFROM
        IMDSEARCHDATETO
        CHKMISEIKYU
        TXTCUSTCDL
        TXTCUSTCDM
        CHKZAI
        CHKOUTKA
        CHKREZAI
        CHKREOUTKA
        CHKHENPINOUTKA
        NUMKEIKAFROM
        NUMKEIKATO
        SPRDETAIL
    End Enum

    ''' <summary>
    ''' スプレッド列インデックス用列挙体
    ''' </summary>
    ''' <remarks>S_SPREADへの状態保存の都合上、列数99を超える定義はNG</remarks>
    Public Enum SprColumnIndex
        CUST_GOODS_CD = 0
        GOODS_NM
        LVL1_UT
        LOT_NO
        STATUS_NM
        INKA_DATE
        OUTKA_PLAN_DATE
        RTN_INKA_DATE
        RTN_OUTKA_PLAN_DATE
        LAST_INV_DATE
        UP_FLG_NM
        RETURN_FLAG_NM
        SUPPLY_CD
        ASN_NO
        TSMC_REC_NO
        GRLVL1_PPNID
        PLT_NO
        DEPLT_NO
        LV2_SERIAL_NO
        CYLINDER_NO
        STOCK_TYPE_NM
        KEIKA_DAYS
        LT_DATE
        LVL1_CHECK
        LVL2_CHECK
        LAST_CLC_DATE
        WH_NM
        JISSEKI_SHORI_FLG_IN_NM
        JISSEKI_SHORI_FLG_OUT_NM
        TOU_NO
        SITU_NO
        TOU_SITU_NM
        ZONE_CD
        ZONE_NM
        LOCA
        NRS_WH_CD
        STATUS
        STOCK_TYPE
        UP_FLG
        RETURN_FLAG
        JISSEKI_SHORI_FLG_IN
        JISSEKI_SHORI_FLG_OUT
        SYS_UPD_DATE
        SYS_UPD_TIME
        LAST
    End Enum

    ''' <summary>
    ''' 処理モード用列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum Mode

        ''' <summary>
        ''' 初期モード
        ''' </summary>
        ''' <remarks></remarks>
        INT = 0
        ''' <summary>
        ''' 参照モード
        ''' </summary>
        ''' <remarks></remarks>
        REF
        ''' <summary>
        ''' 編集モード
        ''' </summary>
        ''' <remarks></remarks>
        EDT

    End Enum

End Class
