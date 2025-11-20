' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME040  : 作業指示書検索
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LME040定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LME040C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LME040IN"
    Public Const TABLE_NM_INOUT_SAGYO As String = "LME040INOUT_SAGYO"
    Public Const TABLE_NM_INOUT_SAGYOSIJI As String = "LME040INOUT_SAGYO_SIJI"

    'FunctionKey
    Public Const FUNCTION_HENSHU As String = "編　集"
    Public Const FUNCTION_FUKUSHA As String = "複　写"
    Public Const FUNCTION_MASTER As String = "マスタ参照"
    Public Const FUNCTION_HOZON As String = "保　存"
    Public Const FUNCTION_CLOSE As String = "閉じる"
    Public Const FUNCTION_PRINT As String = "印　刷"
    Public Const FUNCTION_ROWADD As String = "行追加"
    Public Const FUNCTION_ROWDEL As String = "行削除"
    Public Const FUNCTION_DELETE As String = "削　除"

    'EVENTNAME
    Public Const EVENTNAME_HENSHU As String = "編集"
    Public Const EVENTNAME_FUKUSHA As String = "複写"
    Public Const EVENTNAME_MASTER As String = "マスタ参照"
    Public Const EVENTNAME_HOZON As String = "保存"
    Public Const EVENTNAME_CLOSE As String = "閉じる"
    Public Const EVENTNAME_PRINT As String = "印刷"
    Public Const EVENTNAME_ROWADD As String = "行追加"
    Public Const EVENTNAME_ROWDEL As String = "行削除"
    Public Const EVENTNAME_DELETE As String = "削除"

    'モード別画面ロック用
    Public Const MODE_SINKI As String = "0"     '新規
    Public Const MODE_VIEW As String = "1"      '参照
    Public Const MODE_EDIT As String = "2"      '編集

    '作業指示ステータス
    Public Const SAGYO_SIJI_STATUS_IMCPMPLETE As String = "00"     '未完了
    Public Const SAGYO_SIJI_STATUS_COMPLETION As String = "01"     '完了


    'UPD_FLG
    '0:初期検索時に設定
    '1:追加レコードに設定
    '2:既存レコードの削除時に設定
    '-1:追加レコードだが、保存せずに削除時に設定(行追加→すぐに行削除の場合)

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        CMBPRINT = 0
        BTNPRINT
        GRPSAGYOSIJI
        EIGYO
        CUSTCDL
        CUSTNML
        CUSTCDM
        CUSTNMM
        SAGYODATE
        IOZSKB
        REMARK1
        REMARK2
        REMARK3
        GRPZAIKO
        BTNROWADD
        BTNROWDEL
        SPRDETAILS
        GRPSAGYO
        SAGYOCD1
        SAGYONM1
        SAGYORMK1
        SAGYOCD2
        SAGYONM2
        SAGYORMK2
        SAGYOCD3
        SAGYONM3
        SAGYORMK3
        SAGYOCD4
        SAGYONM4
        SAGYORMK4
        SAGYOCD5
        SAGYONM5
        SAGYORMK5

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        HENSHU = 0
        FUKUSHA
        MASTER
        HOZON
        CLOSE
        PRINT
        ROWADD
        ROWDEL
        LEAVECELL
        MASTERENTER
        JIKKOU
        DELETE

    End Enum

    ' Spread部列インデックス用列挙対
    Public Enum SprColumnIndex As Integer

        DEF = 0
        GOODSNM       '商品名
        GOODSCDCUST   '商品コード
        TOUNO         '棟
        SITUNO        '室
        ZONECD        'ZONE
        LOCA          'ロケーション
        LOTNO         'ロット№
        SAGYONB       '作業個数
        PORAZAINB     '残個数
        IRIME         '入目
        PORAZAIQT     '残数量
        LTDATE        '賞味期限
        GOODSCONDKB1  '状態 中身
        GOODSCONDKB2  '状態 外観
        GOODSCONDKB3  '状態 荷主
        OFBKB         '薄外品
        SPDKB         '保留品
        SERIALNO      'シリアル№
        GOODSCRTDATE  '製造日
        DESTCD        '届先コード
        ALLOCPRIORITY '割当優先
        INKODATE      '入荷日
        INKOPLANDATE  '入荷予定日
        GOODSCDNRS    '商品キー
        WHCD          '倉庫コード
        TAXKB         '課税区分
        ZAIRECNO      '在庫レコード番号
        INKANOLM      '入荷管理番号(大) + (中)
        PORAZAINBZAI  '実予在庫個数(現在)
        DESTNM        '届先名
        KEYNO         'キー番号
        LAST

    End Enum

End Class
