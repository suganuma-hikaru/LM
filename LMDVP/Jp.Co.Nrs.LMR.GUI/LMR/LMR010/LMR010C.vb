' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMR       : 完了
'  プログラムID     :  LMR010    : 完了取込
'  作  成  者       :  [矢内正之]
' ==========================================================================

''' <summary>
''' LMR010定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMR010C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    'Public Const TABLE_NM_IN As String = "LMR010IN"
    Public Const TABLE_NM_INOUT As String = "LMR010INOUT"
    Public Const TABLE_NM_IN_CHECK As String = "LMR010_IN_CHECK"
    Public Const TABLE_NM_OUT_CHECK As String = "LMR010_OUT_CHECK"
    Public Const TABLE_NM_STATECHK As String = "LMR010_STATECHK"
    Public Const TABLE_NM_CUST_IN As String = "LMR010_CUST_IN"
    Public Const TABLE_NM_CUST As String = "LMR010_CUST"
    Public Const TABLE_NM_ZAI As String = "LMR010_ZAI"
    Public Const TABLE_NM_JIKAI_BUNNOU_IN As String = "LMR010_JIKAI_BUNNOU_IN"

    'モード別画面ロック用
    Public Const MODE_DEFAULT As String = "0"
    Public Const MODE_KANRYO As String = "1"

    'フラグオン・オフ
    Public Const FLG_OFF As String = "00"
    Public Const FLG_ON As String = "01"

    '完了種別コンボ用
    Public Const KANRYO_01 As String = "01"
    Public Const KANRYO_02 As String = "02"
    Public Const KANRYO_03 As String = "03"
    Public Const KANRYO_04 As String = "04"
    Public Const KANRYO_05 As String = "05"
    Public Const KANRYO_06 As String = "06"
    Public Const KANRYO_07 As String = "07"

    '入荷作業進捗区分
    Public Const INKA_SINTYOKU_10 As String = "10"
    Public Const INKA_SINTYOKU_20 As String = "20"
    Public Const INKA_SINTYOKU_30 As String = "30"
    Public Const INKA_SINTYOKU_40 As String = "40"
    Public Const INKA_SINTYOKU_50 As String = "50"
    Public Const INKA_SINTYOKU_90 As String = "90"

    '出荷作業進捗区分
    Public Const OUTKA_SINTYOKU_10 As String = "10"
    Public Const OUTKA_SINTYOKU_30 As String = "30"
    Public Const OUTKA_SINTYOKU_40 As String = "40"
    Public Const OUTKA_SINTYOKU_50 As String = "50"
    Public Const OUTKA_SINTYOKU_60 As String = "60"
    Public Const OUTKA_SINTYOKU_90 As String = "90"

    '作業指示進捗区分
    Public Const SAGYO_SINTYOKU_00 As String = "00"
    Public Const SAGYO_SINTYOKU_01 As String = "01"

    'スプレッドチェックボックス
    Public Const CHECK_TRUE As String = "True"

    'イベント種別
    Public Enum EventShubetsu As Integer

        TORIKOMI = 0    '取込
        KENSAKU         '検索
        MASTER          'マスタ参照
        KANRYO          '完了
        CLOSE           '閉じる
        ENTER           'Enterキー押下

    End Enum

    'プログラムID
    Public Const PGID_LMA020 As String = "LMA020"
    Public Const PGID_LMB010 As String = "LMB010"
    Public Const PGID_LMC010 As String = "LMC010"
    Public Const PGID_LME030 As String = "LME030"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        EIGYO = 0
        TANTOCD
        CUSTCD
        KANRYO
        NYUKADATE_FROM
        NYUKADATE_TO
        SPDKANRYO

    End Enum

    'START YANAI 要望番号932
    ''スプレッド列数
    'Public Const sprKanryoColCount As Integer = 10
    'スプレッド列数
    'Public Const sprKanryoColCount As Integer = 11
    'END YANAI 要望番号932
    Public Const sprKanryoColCount As Integer = 13

    ' Spread部列インデックス用列挙対
    Public Enum sprKanryoColumnIndex

        DEF = 0
        KANRI_NO
        PLAN_DATE
        ORDER_NO
        TANTO_USER
        CUST_NM
        DEST_NM
        KONPO_KOSU
        CHK_INKA_STATE_KB
        CUST_CD_L
        'START YANAI 要望番号932
        SCNT
        'END YANAI 要望番号932
        TSMC_QTY_SUMI
        TSMC_QTY

    End Enum



    ''' <summary>
    ''' 庫内作業ステータス(入荷)
    ''' </summary>
    ''' <remarks></remarks>
    Class WH_KENPIN_WK_STATUS_INKA

        ''' <summary>
        ''' 未設定
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NONE As String = ""

        ''' <summary>
        ''' 検品中
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INSPECTIONG As String = "01"

        ''' <summary>
        ''' 検品済(取込待)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const WAITING_FOR_CAPTURE As String = "02"

        ''' <summary>
        ''' 検品済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INSPECTED As String = "03"


    End Class

End Class
