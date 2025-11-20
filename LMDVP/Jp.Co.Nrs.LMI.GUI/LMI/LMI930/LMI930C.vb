' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI930  : 住化カラー実績報告データ作成
'  作  成  者       :  [umano]
' ==========================================================================

''' <summary>
''' LMI930定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI930C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI930IN"
    Public Const TABLE_NM_OUT_TXT As String = "LMI930_SMKJISSEKI_TXT"

    '送信ファイルバイト数
    Public Const COMPANY_CD As Integer = 2
    Public Const RB_KB As Integer = 1
    Public Const DENP_NO As Integer = 7
    Public Const DENP_NO_EDA As Integer = 1
    Public Const DENP_NO_REN As Integer = 2
    Public Const DENP_NO_GYO As Integer = 1
    Public Const SHORI_DATE As Integer = 8
    Public Const OUTKA_PLAN_DATE As Integer = 8
    Public Const UKE_CHUKEI As Integer = 3
    Public Const UKE_AITE As Integer = 3
    Public Const UKE_SHUBETSU As Integer = 3
    Public Const GOODS_CLASS As Integer = 4
    Public Const GOODS_CD As Integer = 6
    Public Const GOODS_NM As Integer = 26
    Public Const NISUGATA As Integer = 2
    Public Const IRIME As Integer = 8
    Public Const TANA_KB As Integer = 2
    Public Const ZAIKO_KB As Integer = 1
    Public Const LOT_NO As Integer = 15
    Public Const SEISAN_KOJO As Integer = 3
    Public Const KOSU As Integer = 5
    Public Const SURYO As Integer = 13
    Public Const ARR_PLAN_DATE As Integer = 8
    Public Const YOUKI_JOKEN As Integer = 2
    Public Const NIWATASHI_JOKEN As Integer = 2
    Public Const SHIKEN_HYO As Integer = 1
    Public Const SHITEI_DENPYO As Integer = 1
    Public Const DEST_CD As Integer = 6
    Public Const DEST_ZIP As Integer = 8
    Public Const DEST_TEL As Integer = 18
    Public Const DEST_MSG As Integer = 20
    Public Const BUYER_ORD_NO_DTL As Integer = 20
    Public Const BUYER_CD As Integer = 6
    Public Const BUYER_TEL As Integer = 18
    Public Const KOJO_MSG As Integer = 20
    Public Const HANBAI_KA As Integer = 5
    Public Const HANBAI_TANTO As Integer = 4
    Public Const ORDER_KB As Integer = 1
    Public Const DEST_NM As Integer = 40
    Public Const DEST_AD_1 As Integer = 15
    Public Const DEST_AD_2 As Integer = 15
    Public Const BUYER_NM As Integer = 40
    Public Const DEST_NM_KANA As Integer = 25
    Public Const DEST_AD_KANA1 As Integer = 20
    Public Const DEST_AD_KANA2 As Integer = 20
    Public Const SAKUSEI_DATE As Integer = 8
    Public Const SAKUSEI_TIME As Integer = 8
    Public Const RACK_NO As Integer = 3
    Public Const CUST_ORD_NO_DTL As Integer = 8
    Public Const RIYUU As Integer = 2
    Public Const DATA_KB As Integer = 5
    Public Const YUSO_SHUDAN As Integer = 10
    Public Const YUSO_GYOSHA As Integer = 10
    Public Const YUSO_JIS_CD As Integer = 5
    Public Const JISSEKI_KBN As Integer = 1
    Public Const REC_SPACE_CNT As Integer = 37

End Class
