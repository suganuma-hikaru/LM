' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDIサブシステム
'  プログラムID     :  LMH090C : 現品票印刷
'  作  成  者       :  
' ==========================================================================

''' <summary>
''' LMH090定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMH090C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ' Spread部列インデックス用列挙対
    Public Enum SprColumnIndex

        '表示
        DEF = 0             '選択列
        OUTKA_FROM_ORD_NO   'オーダー番号
        INKA_DATE           '入荷日
        CUST_GOODS_CD       '商品コード
        GOODS_NM            '商品名
        LOT_NO              'ロット№
        STD_IRIME           '標準入目
        NET                 '内容量
        NB                  '個数

        '非表示
        CRT_DATE            'データ受信日
        FILE_NAME           '受信ファイル名
        REC_NO              'レコード番号
        HED_UPD_DATE        'HED更新日
        HED_UPD_TIME        'HED更新時刻
        GYO                 '行
        DTL_UPD_DATE        'DTL更新日
        DTL_UPD_TIME        'DTL更新時刻
        GENPINHYO_CHKFLG    '現品票印刷チェックフラグ
        LAST

    End Enum

    'タブインデックス用列挙体(Main)
    Public Enum CtlTabIndex_MAIN

        SPR_MAIN = 0

    End Enum

End Class
