' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI030C : 請求データ作成 [デュポン用]
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LMI030定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI030C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI030IN"
    Public Const TABLE_NM_OUT As String = "LMI030OUT"
    Public Const TABLE_NM_OUT_EXCEL As String = "LMI030OUT_EXCEL"
    Public Const TABLE_NM_OUT_SELECT As String = "LMI030OUT_SELECT"
    Public Const TABLE_NM_OUT_SELECT_GL As String = "LMI030OUT_SELECT_GL"

    'EVENTNAME
    Public Const EVENTNAME_EXCEL As String = "Excel出力"
    Public Const EVENTNAME_KENSAKU As String = "検索"
    Public Const EVENTNAME_MASTER As String = "マスタ参照"
    Public Const EVENTNAME_CLOSE As String = "閉じる"
    Public Const EVENTNAME_MAKE As String = "作成"
    Public Const EVENTNAME_PRINT As String = "印刷"

    '荷主コード
    Public Const CUSTCD00088 As String = "00088"
    Public Const CUSTCD00089 As String = "00089"
    Public Const CUSTCD00187 As String = "00187"
    Public Const CUSTCD00188 As String = "00188"
    Public Const CUSTCD00514 As String = "00514"
    Public Const CUSTCD00554 As String = "00554"
    Public Const CUSTCD00587 As String = "00587"
    Public Const CUSTCD00588 As String = "00588"
    Public Const CUSTCD00589 As String = "00589"
    Public Const CUSTCD00688 As String = "00688"
    Public Const CUSTCD00689 As String = "00689"

    Public Const CUSTCDS03 As String = "03"
    Public Const CUSTCDSS00 As String = "00"
    Public Const CUSTCDSS02 As String = "02"

    Public Const EXCEL_DKK_FLG As String = "01"
    Public Const EXCEL_TEF_FLG As String = "02"
    Public Const EXCEL_ELE_FLG As String = "03"
    Public Const EXCEL_AXA_FLG As String = "04"

    'EXCELタイトル列名(DKK)
    Public Const DKK_COL As Integer = 17
    Public Const DKK_COL_01 As String = "運送番号"
    Public Const DKK_COL_02 As String = "出荷日"
    Public Const DKK_COL_03 As String = "SALES_ORD"
    Public Const DKK_COL_04 As String = "オーダー番号"
    Public Const DKK_COL_05 As String = "届け先"
    Public Const DKK_COL_06 As String = "市"
    Public Const DKK_COL_07 As String = "請求距離"
    Public Const DKK_COL_08 As String = "個数"
    Public Const DKK_COL_09 As String = "請求重量"
    Public Const DKK_COL_10 As String = "請求金額"
    Public Const DKK_COL_11 As String = "商品名"
    Public Const DKK_COL_12 As String = "着日"
    Public Const DKK_COL_13 As String = "FRBコード"
    Public Const DKK_COL_14 As String = "事業部コード"
    Public Const DKK_COL_15 As String = "商品コード"
    Public Const DKK_COL_16 As String = "ソースコード"
    Public Const DKK_COL_17 As String = "届け先コード"

    'EXCELタイトル列名(TEF)
    Public Const TEF_COL As Integer = 14
    Public Const TEF_COL_01 As String = "出荷日"
    Public Const TEF_COL_02 As String = "荷主注文番号"
    Public Const TEF_COL_03 As String = "届先名"
    Public Const TEF_COL_04 As String = "住所（市）"
    Public Const TEF_COL_05 As String = "請求重量"
    Public Const TEF_COL_06 As String = "請求距離"
    Public Const TEF_COL_07 As String = "請求個数"
    Public Const TEF_COL_08 As String = "請求運賃"
    Public Const TEF_COL_09 As String = "品名"
    Public Const TEF_COL_10 As String = "FRB-CD"
    Public Const TEF_COL_11 As String = "SRC-CD"
    Public Const TEF_COL_12 As String = "SRC-NO"
    Public Const TEF_COL_13 As String = "荷主商品コード"
    Public Const TEF_COL_14 As String = "届先コード"

    'EXCELタイトル列名(ELE)
    Public Const ELE_COL As Integer = 14
    Public Const ELE_COL_01 As String = "出荷日"
    Public Const ELE_COL_02 As String = "荷主注文番号"
    Public Const ELE_COL_03 As String = "届先名"
    Public Const ELE_COL_04 As String = "住所（市）"
    Public Const ELE_COL_05 As String = "請求重量"
    Public Const ELE_COL_06 As String = "請求距離"
    Public Const ELE_COL_07 As String = "請求個数"
    Public Const ELE_COL_08 As String = "請求運賃"
    Public Const ELE_COL_09 As String = "品名"
    Public Const ELE_COL_10 As String = "FRB-CD"
    Public Const ELE_COL_11 As String = "SRC-CD"
    Public Const ELE_COL_12 As String = "SRC-NO"
    Public Const ELE_COL_13 As String = "荷主商品コード"
    Public Const ELE_COL_14 As String = "届先コード"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        EIGYO = 0
        CUSTCDL
        CUSTCDM
        CUSTCDS
        CUSTCDSS
        SEIKYUFROM
        SEIKYUTO
        JIGYO
        CMBSEIKYU
        BTNMAKE
        CMBPRINT
        BTNPRINT
        SPRDETAILS
       
    End Enum


    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        EXCEL = 0
        KENSAKU
        MASTER
        CLOSE
        MAKE
        PRINT
      
    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        CUST_NM
        DEPART
        NRS_BR_NM
        SEKYUTSUKI
        SHINCHOKU
        'START YANAI 要望番号830
        TANTO_NM
        SHINCHOKU_KB
        'END YANAI 要望番号830

    End Enum
End Class
