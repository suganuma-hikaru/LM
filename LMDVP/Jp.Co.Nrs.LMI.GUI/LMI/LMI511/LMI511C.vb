' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI511C : JNC EDI
'  作  成  者       :  
' ==========================================================================

''' <summary>
''' LMI511定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI511C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' 顧客名称
    ''' </summary>
    ''' <remarks></remarks>
    Public Const COMPANY_NAME As String = "ＪＮＣ"

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TABLE_NM
        Public Const IN_BO_MST As String = "LMI511IN_BO_MST"
        Public Const OUT_BO_MST As String = "LMI511OUT_BO_MST"
        Public Const INOUT_OLD_DATA_CHK As String = "LMI511INOUT_OLD_DATA_CHK"
        Public Const INOUT_GET_AUTO_CD As String = "LMI511INOUT_GET_AUTO_CD"
        Public Const IN_SEARCH As String = "LMI511IN_SEARCH"
        Public Const OUT As String = "LMI511OUT"
        Public Const IN_HED As String = "LMI511IN_HED"
        Public Const OUT_HED As String = "LMI511OUT_HED"
        Public Const IN_DTL As String = "LMI511IN_DTL"
        Public Const OUT_DTL As String = "LMI511OUT_DTL"
        Public Const OUT_HED_EDI As String = "LMI511OUT_HED_EDI"
        Public Const OUT_DTL_EDI As String = "LMI511OUT_DTL_EDI"
        Public Const IN_EDI_L As String = "LMI511IN_H_OUTKAEDI_L"
        Public Const OUT_EDI_L As String = "LMI511OUT_H_OUTKAEDI_L"
        Public Const IN_EDI_M As String = "LMI511IN_H_OUTKAEDI_M"
        Public Const IN_PRT1 As String = "LMI511IN_PRT1"
        Public Const IN_PRT2 As String = "LMI511IN_PRT2"
        Public Const IN_PRT3 As String = "LMI511IN_PRT3"
        Public Const IN_PRT4 As String = "LMI511IN_PRT4"
        Public Const IN_PRT5 As String = "LMI511IN_PRT5"
        Public Const IN_IDX As String = "LMI511IN_IDX"
    End Class

    ''' <summary>
    ''' 入出庫区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Class INOUT_KB
        ''' <summary>
        ''' 出庫
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTKA As String = "0"
        ''' <summary>
        ''' 入庫
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INKA As String = "1"
    End Class

    ''' <summary>
    ''' データ種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DATA_KIND
        ''' <summary>
        ''' 出荷
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTKA As String = "4001"
        ''' <summary>
        ''' 入荷
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INKA As String = "4101"
        ''' <summary>
        ''' 運送
        ''' </summary>
        ''' <remarks></remarks>
        Public Const UNSO As String = "3001"
    End Class

    ''' <summary>
    ''' 報告区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Class RTN_FLG
        ''' <summary>
        ''' 未送
        ''' </summary>
        ''' <remarks></remarks>
        Public Const MISO As String = "0"
        ''' <summary>
        ''' 送待
        ''' </summary>
        ''' <remarks></remarks>
        Public Const WAIT As String = "1"
        ''' <summary>
        ''' 完了
        ''' </summary>
        ''' <remarks></remarks>
        Public Const COMP As String = "2"
    End Class

    ''' <summary>
    ''' 送信訂正区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SND_CANCEL_FLG
        ''' <summary>
        ''' なし
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NORMAL As String = "0"
        ''' <summary>
        ''' 修赤
        ''' </summary>
        ''' <remarks></remarks>
        Public Const RED As String = "1"
        ''' <summary>
        ''' 修黒
        ''' </summary>
        ''' <remarks></remarks>
        Public Const BLACK As String = "2"
    End Class

    ''' <summary>
    ''' 実施区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Class JISSI_KBN
        ''' <summary>
        ''' 未
        ''' </summary>
        ''' <remarks></remarks>
        Public Const MI As String = "0"
        ''' <summary>
        ''' 済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SUMI As String = "1"
    End Class

    ''' <summary>
    ''' 検索日区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SELECT_DATE
        ''' <summary>
        ''' 出荷予定日
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTKA As String = "01"
        ''' <summary>
        ''' 出荷予定日(名称)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTKA_NM As String = "出荷予定日"
        ''' <summary>
        ''' 入荷予定日
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INKA As String = "02"
        ''' <summary>
        ''' 入荷予定日(名称)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INKA_NM As String = "入荷予定日"
    End Class

    ''' <summary>
    ''' 印刷種類
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PRINT_TYPE
        ''' <summary>
        ''' 出荷予定表
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTKA_YOTEI As String = "01"

        ''' <summary>
        ''' 酢酸出荷依頼書(川本倉庫)
        ''' </summary>
        Public Const SAKUSAN_KAWAMOTO As String = "02"

        ''' <summary>
        ''' ファクシミリ連絡票(三菱ケミカル)
        ''' </summary>
        Public Const FAX_MITSUBISHI_CHEMICAL As String = "03"

        ''' <summary>
        ''' 酢酸注文書(KHネオケム)
        ''' </summary>
        Public Const SAKUSAN_KH_NEOKEMU As String = "04"

        ''' <summary>
        ''' イソブタノール依頼書(ｼﾝｺｰｹﾐ神戸)
        ''' </summary>
        Public Const IBA_SHINKO_CHEMICAL_KOBE As String = "05"

    End Class

    ''' <summary>
    ''' 出荷場所部門コード
    ''' </summary>
    ''' <remarks>判断に使用するもののみ</remarks>
    Public Class OUTKA_POSI_BU_CD
        ''' <summary>
        ''' 川本倉庫
        ''' </summary>
        Public Const KAWAMOTO As String = "AW14"

        ''' <summary>
        ''' 三菱ケミカル(三菱化学物流)
        ''' </summary>
        Public Const MITSUBISHI_CHEMICAL As String = "AX34"

        ''' <summary>
        ''' KHネオケム
        ''' </summary>
        Public Const KH_NEOKEM As String = "AX18"

        ''' <summary>
        ''' ｼﾝｺｰｹﾐ神戸
        ''' </summary>
        Public Const SHINKO_CHEMICAL_KOBE As String = "AW46"

    End Class

    ''' <summary>
    ''' イベント種別用列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer
        OUTKASAVE = 1           '出荷登録
        EDIT                    '編集
        MTMSAVE                 'まとめ指示
        MTMCANCEL               'まとめ解除
        SNDREQ                  '送信要求
        REVISION                '報告訂正
        SNDCANCEL               '送信取消
        MTMSEARCH               'まとめ候補検索
        SEARCH                  '検索
        MASTER                  'マスタ参照
        SAVEEDT                 '保存(編集)
        SAVEREV                 '保存(訂正)
        CLOSE                   '閉じる
        ENTER                   'Enter
        PRINT1                  '印刷(出荷予定表)
        PRINT2                  '印刷(酢酸出荷依頼書(川本倉庫))
        PRINT3                  '印刷(ファクシミリ連絡票(三菱ケミカル))
        PRINT4                  '印刷(酢酸注文書(KHネオケム))
        PRINT5                  '印刷(イソブタノール依頼書(ｼﾝｺｰｹﾐ神戸))
        DOUBLE_CLICK            'ダブルクリック
    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex_MAIN
        CMBEIGYO = 0
        TXTCUSTCD_L
        TXTCUSTCD_M
        GRPSEARCHKBN
        CMBOUTKAPOSI
        IMDEDIDATEFROM
        IMDEDIDATETO
        CMBSELECTDATE
        IMDSEARCHDATEFROM
        IMDSEARCHDATETO
        GRPHIDE
        CHKHIDE1
        CHKHIDE2
        GRPEXCLUSION
        TXTEXCLUSIONA
        TXTEXCLUSIONB
        TXTEXCLUSIONC
        CMBPRINT
        BTNPRINT
        BTNEXCEL
        BTNALLCHECK
        BTNALLCLEAR
        SPREDILIST
    End Enum

    ''' <summary>
    ''' スプレッド列インデックス用列挙体
    ''' </summary>
    ''' <remarks>S_SPREADへの状態保存の都合上、列数99を超える定義はNG</remarks>
    Public Enum SprColumnIndex
        DEF = 0
        RB_KBN
        RB_KBN_NM
        MOD_KBN
        MOD_KBN_NM
        RTN_FLG
        RTN_FLG_NM
        SND_CANCEL_FLG
        SND_CANCEL_FLG_NM
        PRTFLG
        PRTFLG_NM
        PRTFLG_SUB
        PRTFLG_SUB_NM
        NRS_SYS_FLG
        NRS_SYS_FLG_NM
        COMBI
        UNSO_REQ_YN
        UNSO_REQ_YN_NM
#If True Then   'ADD 2020/08/27 013087   【LMS】JNC EDI　改修
        UNSO_EDI_CTL_NO
        UNSO_RTN_FLG
        UNSO_RTN_FLG_NM
#End If
        COMBI_NO
        SR_DEN_NO
        OUTKA_DATE
        ARRIVAL_DATE
        INKO_DATE
        DEST_CD
        DEST_NM
        DEST_AD_NM
        OUTKA_POSI_BU_CD
        OUTKA_POSI_BU_NM
        ACT_UNSO_CD
        ACT_UNSO_NM
        MEMO_UNSO_CD
        MEMO_UNSO_NM
        UNSO_ROUTE_CD
        UNSO_ROUTE_NM
        GOODS_CD
        GOODS_NM
        SURY_REQ
        SURY_TANI_CD
        SURY_RPT
        SURY_RPT_UNSO
        CAR_NO
        TUMI_SU
        PRINT_NO
        JYUCHU_NO
        ORDER_NO
        DELIVERY_NM
        INV_REM_NM
        GOODS_CD2
        GOODS_NM2
        SURY_REQ2
        SURY_TANI_CD2
        SURY_RPT2
        HIS_NO
        OLD_DATA_FLG
        NRS_BR_CD
        INOUT_KB
        DATA_KIND
        EDI_CTL_NO
        EDI_CTL_NO_CHU
        CUST_CD_L
        CUST_CD_M
        SYS_UPD_DATE_HED
        SYS_UPD_TIME_HED
        SYS_UPD_DATE_DTL
        SYS_UPD_TIME_DTL
        EDI_CTL_NO_UHD
        SYS_UPD_DATE_UHD
        SYS_UPD_TIME_UHD
        EDI_CTL_NO_CHU_UDL
        SYS_UPD_DATE_UDL
        SYS_UPD_TIME_UDL
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
        ''' <summary>
        ''' 訂正モード
        ''' </summary>
        ''' <remarks></remarks>
        REV
        ''' <summary>
        ''' まとめモード
        ''' </summary>
        ''' <remarks></remarks>
        MTM
    End Enum

End Class
