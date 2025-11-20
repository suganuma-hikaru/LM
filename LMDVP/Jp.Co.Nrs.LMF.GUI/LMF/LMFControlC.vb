' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF         : 運送サブ
'  プログラムID     :  LMFControlC : 運送サブ 共通コンスト
'  作  成  者       :  [ito]
' ==========================================================================

''' <summary>
''' LMFControl定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMFControlC
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"

    ''' <summary>
    ''' 荷主M存在チェック(置換文字)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CustMsgType As Integer

        CUST_L
        CUST_M
        CUST_S
        CUST_SS

    End Enum

    ''' <summary>
    ''' 800INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMF800IN"

    ''' <summary>
    ''' 800OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_OUT As String = "LMF800OUT"

    ''' <summary>
    ''' M_YOKO_TARIFF_HDテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_YOKO As String = "M_YOKO_TARIFF_HD"

    ''' <summary>
    ''' UNCHIN_TARIFF_LATEST_RECテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_REC As String = "UNCHIN_TARIFF_LATEST_REC"

    ''' <summary>
    ''' 元データ区分(入荷)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MOTO_DATA_NYUKA As String = "10"

    ''' <summary>
    ''' 元データ区分(出荷)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MOTO_DATA_SHUKKA As String = "20"

    ''' <summary>
    ''' 元データ区分(運送)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MOTO_DATA_UNSO As String = "40"

    ''' <summary>
    ''' 運賃計算締め基準(出荷日)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CALC_SHUKKA As String = "01"

    ''' <summary>
    ''' 運賃計算締め基準(納入日)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CALC_NYUKA As String = "02"

    ''' <summary>
    ''' 計算コード区分(荷姿建て)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CALC_KBN_NISUGATA As String = "01"

    ''' <summary>
    ''' 計算コード区分(車建て)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CALC_KBN_CAR As String = "02"

    ''' <summary>
    ''' フラグ(無)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FLG_ON As String = "01"

    ''' <summary>
    ''' フラグ(有)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FLG_OFF As String = "00"

    ''' <summary>
    ''' 配送区分(集荷)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HAISO_SHUKA As String = "01"

    ''' <summary>
    ''' 配送区分(中継)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HAISO_THUKEI As String = "02"

    ''' <summary>
    ''' 配送区分(配荷)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const HAISO_HAIKA As String = "03"

    ''' <summary>
    ''' 運送手配(日陸手配)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TEHAI_NRS As String = "10"

    ''' <summary>
    ''' 閾値(検索)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const LIMIT_SELECT As String = "02"

    ''' <summary>
    ''' 最大桁数[整数18]
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_KETA As String = "999999999999999999"

    ''' <summary>
    ''' 最大桁数[整数14]
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_KETA_SPR As Double = 99999999999999

    ''' <summary>
    ''' 最大桁数(整数18桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_18 As String = "###,###,###,###,###,##0"

    ''' <summary>
    ''' 最大桁数[整数15　小数3]
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_KETA_DEC As Double = 999999999999.999

    ''' <summary>
    ''' 最大桁数(整数15桁　小数3桁)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_15_3 As String = "###,###,###,###,##0.000"

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 最大桁数[整数10　少数2桁]
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_KETA_KINGAKU As Double = 9999999999.99
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    ''' <summary>
    ''' 最小値(0)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MIN_0 As String = "0"

    ''' <summary>
    ''' yyyyMMdd
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DATE_YYYYMMDD As String = "yyyyMMdd"

    ''' <summary>
    ''' yyyy/MM/dd
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DATE_SLASH_YYYYMMDD As String = "yyyy/MM/dd"

    ''' <summary>
    ''' yyyyMM
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DATE_YYYYMM As String = "yyyyMM"

    ''' <summary>
    ''' エラーコード(E)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MESSAGE_E As String = "E"

    ''' <summary>
    ''' マスタ参照ボタンイベント
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MASTEROPEN As String = "MASTEROPEN"

    ''' <summary>
    ''' Enterイベント
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ENTER As String = "ENTER"

    ' ''' <summary>
    ' ''' BLF
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Const BLF As String = "BLF"

    '2016.01.06 UMANO 英語化対応START
    ' ''' <summary>
    ' ''' 運賃
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Const UNCHIN As String = "運賃(Freight)"

    ' ''' <summary>
    ' ''' 車建て
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Const SHADATE As String = "車建て(per Truck)"

    ''' <summary>
    ''' 以外
    ''' </summary>
    ''' <remarks></remarks>
    Public Const IGAI As String = "以外"

    ' ''' <summary>
    ' ''' 日陸手配
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Const NRS_TEHAI As String = "日陸手配(NRS Arrangement)"

    ''' <summary>
    ''' まとめ済
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MATOME_ZUMI As String = "まとめ済"

    ''' <summary>
    ''' 確定済
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KAKUTEI_ZUMI As String = "確定済(Committed)"

    ''' <summary>
    ''' 未確定
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MI_KAKUTEI As String = "未確定"

    ''' <summary>
    ''' コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Const CD As String = "コード(CODE)"

    ''' <summary>
    ''' 支店コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BR_CD As String = "支店コード(BRANCH CODE)"

    ''' <summary>
    ''' (大)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const L_NM As String = "(大:L)"

    ''' <summary>
    ''' (中)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const M_NM As String = "(中:M)"

    ''' <summary>
    ''' (小)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const S_NM As String = "(小:S)"

    ''' <summary>
    ''' (極小)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SS_NM As String = "(極小:SS)"
    '2016.01.06 UMANO 英語化対応END

    ''' <summary>
    ''' (From)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FROM_NM As String = "(From)"

    ''' <summary>
    ''' (To)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TO_NM As String = "(To)"

    ''' <summary>
    ''' 括弧[
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KAKKO_1 As String = "["

    ''' <summary>
    ''' 括弧]
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KAKKO_2 As String = "]"

    ''' <summary>
    ''' " = "
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EQUAL As String = " = "

    ''' <summary>
    ''' ,
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KANMA As String = ","

    ''' <summary>
    ''' 日付
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DATE_NM As String = "日付"

    ''' <summary>
    ''' 納入予定
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NONYU As String = "納入予定"

    ''' <summary>
    ''' 運送会社
    ''' </summary>
    ''' <remarks></remarks>
    Public Const UNSOCO As String = "運送会社"

    ''' <summary>
    ''' 印刷種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_KBN As String = "印刷種別(Print Classification)"

    ''' <summary>
    ''' lblMsgAria
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MES_AREA As String = "lblMsgAria"

    ''' <summary>
    ''' "　"
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ZENKAKU_SPACE As String = "　"

    ''' <summary>
    ''' 運賃確定
    ''' </summary>
    ''' <remarks></remarks>
    Public Const KAKUTEI As String = "運賃確定(Freight Confirm)"

    ''' <summary>
    ''' まとめ指示
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GROUP As String = "まとめ指示"

    '(2013.01.17)要望番号1617 -- START --
    ''' <summary>
    ''' 出荷編集
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_OUTKA As String = "出荷編集"
    '(2013.01.17)要望番号1617 --  END  --

    '2022.08.22 追加START
    ''' <summary>
    ''' データ送信
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_DATASEND As String = "データ送信"
    '2022.08.22 追加END

    ''' <summary>
    ''' 運行新規
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_UNCONEW As String = "運行新規"

    ''' <summary>
    ''' 運行編集
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_UNCOEDIT As String = "運行編集"

    ''' <summary>
    ''' 運送新規
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_UNSONEW As String = "運送新規"

    'START YANAI 要望番号1241 運送検索：運送複写機能を追加する
    ''' <summary>
    ''' 運送複写
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_UNSOCOPY As String = "運送複写"
    'END YANAI 要望番号1241 運送検索：運送複写機能を追加する

    ''' <summary>
    ''' 届先登録
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_DESTSAVE As String = "届先登録"

    ''' <summary>
    ''' 検　索
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_KENSAKU As String = "検　索"

    ''' <summary>
    ''' 編　集
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_HENSHU As String = "編　集"

    ''' <summary>
    ''' 複　写
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_FUKUSHA As String = "複　写"

    ''' <summary>
    ''' 削　除
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_SAKUJO As String = "削　除"

    ''' <summary>
    ''' マスタ参照
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_POP As String = "マスタ参照"

    ''' <summary>
    ''' 保　存
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_HOZON As String = "保　存"

    ''' <summary>
    ''' 閉じる
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_CLOSE As String = "閉じる"

    ''' <summary>
    ''' 確　定
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_KAKUTEI As String = "確　定"

    ''' <summary>
    ''' 確定解除
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_KAIJO As String = "確定解除"

    ''' <summary>
    ''' まとめ指示
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_MATOMESHIJI As String = "まとめ指示"

    ''' <summary>
    ''' まとめ解除
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_MATOMEKAIJO As String = "まとめ解除"

    'START YANAI 要望番号561
    ''' <summary>
    ''' 連続入力
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_RENNYU As String = "連続入力"

    ''' <summary>
    ''' スキップ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_SKIP As String = "スキップ"
    'END YANAI 要望番号561

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    ''' <summary>
    ''' 再計算
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_SAIKEI As String = "再計算"
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

    '＃(2012.08.13) 要望番号：1341 車載受注渡し対応 --- START ---
    ''' <summary>
    ''' 車載受注受渡し
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_SYASAIU As String = "車載受注渡し"
    '＃(2012.08.13) 要望番号：1341 車載受注渡し対応 ---  END  ---

    ''' <summary>
    ''' 納入日
    ''' </summary>
    ''' <remarks></remarks>
    Public Const NONYUBI As String = "納入日"

    ''' <summary>
    ''' 出荷日
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUKKABI As String = "出荷日"

    'START YANAI 要望番号446
    ''' <summary>
    ''' 請求先コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SEIQTO_CD As String = "請求先コード"
    'END YANAI 要望番号446

    ''' <summary>
    ''' タリフ区分(混載)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TARIFF_KONSAI As String = "10"

    ''' <summary>
    ''' タリフ区分(車扱い)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TARIFF_KURUMA As String = "20"

    ''' <summary>
    ''' タリフ区分(特便)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TARIFF_TOKUBIN As String = "30"

    ''' <summary>
    ''' タリフ区分(横持ち)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TARIFF_YOKO As String = "40"

    ''' <summary>
    ''' タリフ区分(入荷着払い)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TARIFF_INKA As String = "50"

    'START YANAI 要望番号477
    ''' <summary>
    ''' テーブルタイプ(重量・距離)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABTP_JYUKYO As String = "00"

    ''' <summary>
    ''' テーブルタイプ(車種・距離)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABTP_SHAKYO As String = "01"

    ''' <summary>
    ''' テーブルタイプ(個数・距離)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABTP_KOKYO As String = "02"

    ''' <summary>
    ''' テーブルタイプ(重量・距離（重量建）)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABTP_JYUKYOT As String = "03"

    ''' <summary>
    ''' テーブルタイプ(数量・距離)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABTP_SUKYO As String = "04"

    ''' <summary>
    ''' テーブルタイプ(個数・県コード)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABTP_KOKEN As String = "05"

    ''' <summary>
    ''' テーブルタイプ(宅急便サイズ・県コード)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABTP_TAKEN As String = "06"

    ''' <summary>
    ''' テーブルタイプ(重量・県（重量建）計算結果小数点以下切捨て)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABTP_JYUKEN As String = "07"
    'END YANAI 要望番号477

    'START YANAI 要望番号582
    ''' <summary>
    ''' 印刷
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_PRINT As String = "印　刷"
    'END YANAI 要望番号582

    ''' <summary>
    ''' 運賃バックアップ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const FUNCTION_BACKUP As String = "ﾊﾞｯｸｱｯﾌﾟ"

    'START YANAI 要望番号1191 運送：貨物引取書の入力チェック誤り
    ''' <summary>
    ''' 運送事由区分(配送)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const JIYUKB_HAISO As String = "01"

    ''' <summary>
    ''' 印刷種別(物品引取書)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINTKB_BUPPIN As String = "04"
    'END YANAI 要望番号1191 運送：貨物引取書の入力チェック誤り

    ''' <summary>
    ''' クリア処理対象コントロール
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CLERA_DATA As Integer
        IMTEXT
        IMNUMBER
        IMCOMB
        IMKBN_COMB
        IMNRS_COMB
        IMSOK_COMB
        IMDATE
        ISNULL
    End Enum

End Class