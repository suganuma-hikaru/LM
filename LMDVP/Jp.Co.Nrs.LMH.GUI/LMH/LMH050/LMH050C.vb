' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH050C : EDIワーニング・エラーチェック画面
'  作  成  者       :  
' ==========================================================================

''' <summary>
''' LMH050定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMH050C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_HED As String = "WARNING_HED"
    Public Const TABLE_NM_DTL As String = "WARNING_DTL"
    Public Const TABLE_NM_SHORI As String = "WARNING_SHORI"


    'モード別画面ロック用
    Public Const MODE_DEFAULT As String = "0"
    Public Const MODE_REF As String = "1"
    Public Const MODE_EDIT As String = "2"
    Public Const MODE_LOCK As String = "9"

    ' Spread部列インデックス用列挙対
    Public Enum SprWarning As Integer

        DEF = 0
        SYORI           '処理区分
        ORDER_NO_L      'オーダー番号（大）
        ORDER_NO_M      'オーダー番号（中）
        MESSAGE_ID      'メッセージID
        WARNING_MSG     'メッセージ
        GOODS_NM        '商品名
        DEST_NM         '届先名             '2012.06.01 ディック対応 追加ADD
        KOMOKU_NM       '項目名
        KOMOKU_VAL      '項目値
        ADDITIONAL_FIELD_VALUE_1 ' 付加項目値1
        MASTER_VAL      'マスタ値
        EDI_CTL_NO_L    'EDI管理番号L
        EDI_CTL_NO_M    'EDI管理番号M
        EDI_WARNING_ID  'ワーニングID
        'GOODS_CD_NRS    'NRS商品コード
        LAST_INDEX
        
    End Enum

    Public Const SHOKI_SEARCH_ROW As String = "0"

    '選択値
    Public Const SELECT_YES As String = "01"
    Public Const SELECT_NO As String = "02"
    Public Const SELECT_CANCEL As String = "03"

    '処理区分
    Public Const SHORI_INKA_TOROKU As String = "10"
    Public Const SHORI_OUTKA_TOROKU As String = "20"
    Public Const SHORI_INKA_HIMODUKE As String = "11"
    Public Const SHORI_OUTKA_HIMODUKE As String = "21"
    Public Const SHORI_INKA_JISSEKI As String = "12"
    Public Const SHORI_OUTKA_JISSEKI As String = "22"
    Public Const SHORI_UNSO_TOROKU As String = "40"             '2012.03.25 大阪対応START


    ''' <summary>
    ''' ワーニングIDフォーマット
    ''' </summary>
    ''' <remarks></remarks>
    Class WARNING_ID_FORMAT


        Class MST_FLG
            ''' <summary>
            ''' 指定なし
            ''' </summary>
            ''' <remarks></remarks>
            Public Const NONE As String = "0"

            ''' <summary>
            ''' 商品マスタ
            ''' </summary>
            ''' <remarks></remarks>
            Public Const M_GOODS As String = "1"


            ''' <summary>
            ''' 届先マスタ
            ''' </summary>
            ''' <remarks></remarks>
            Public Const M_DEST As String = "2"

        End Class

        ''' <summary>
        ''' マスタフラグ開始位置
        ''' </summary>
        ''' <remarks></remarks>
        Public Const MST_FLG_START_INDEX As Integer = 7

        ''' <summary>
        ''' マスタフラグ文字長
        ''' </summary>
        ''' <remarks></remarks>
        Public Const MST_FLG_LENGTH As Integer = 1


    End Class

#If True Then ' 届先自動変換対応(日立物流) 20170407 added by inoue 
    ''' <summary>
    ''' ワーニングID
    ''' </summary>
    ''' <remarks></remarks>
    Class EDI_WARNING_ID

        ''' <summary>
        ''' 振分対象となる届先が不明の場合に出力するワーニング(日立物流)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DIC_WID_L017 As String = "205_OT_2_L017"     'ディック(群馬)用:Lレベル
    End Class
#End If

End Class
