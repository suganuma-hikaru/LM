' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM550C : 下払いタリフマスタメンテ
'  作  成  者       :  matsumoto
' ==========================================================================

''' <summary>
''' LMM550定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM550C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM550IN"
    Public Const TABLE_NM_OUT As String = "LMM550OUT"
    Public Const TABLE_NM_KYORI As String = "LMM550_KYORI"
    Public Const TABLE_NM_SHIHARAI_TARRIF_MAXEDA As String = "LMM550_SHIHARAI_TARRIF_MAXEDA"

    '前ゼロ
    Public Const MAEZERO As String = "000"

    '運賃の桁数
    Public Const UNCHIN As String = "0.000"

    '重量・個数・数量の桁数
    Public Const WT As String = "0"

    '適用開始日の初期値
    Public Const STR_DATE As String = "00000000"

    'ダイアログ設定
    Public Const DEF_FILENM As String = "Book1.xls"
    Public Const ALL_FILE As Integer = 2
    Public Const DEF_DRIVE As String = "C:\"
    Public Const FILETYPE As String = "Excelファイル|*.xls;*.xlsx"
    Public Const DLGTITLE As String = "取込ファイルを選択してください"

    '運賃Spread(距離刻み/運賃)のタイトル名
    Public Const WT_LV As String = "重量"
    Public Const CAR_TP As String = "車種"
    Public Const NB As String = "個数"
    Public Const QT As String = "数量"
    Public Const T_SIZE As String = "宅急便ｻｲｽﾞ"
    Public Const KYORI As String = "距離"
    Public Const CITY_EXTC_A As String = "都市割増A"
    Public Const CITY_EXTC_B As String = "都市割増B"
    Public Const WINT_EXTC_A As String = "冬期割増A"
    Public Const WINT_EXTC_B As String = "冬期割増B"
    Public Const RELY_EXTC As String = "中継料"
    Public Const INSU As String = "保険料"
    '運賃タリフマスタと同じく"10㎏あたりの航送料"を除外する
    'Public Const FRRY_EXTC_10KG As String = "10㎏あたりの航送料"
    Public Const FRRY_EXTC_PART As String = "1件あたりの航送料"

    ''' <summary>
    ''' データタイプ【区分マスタ：U010】
    ''' </summary>
    Friend Const DATA_TP_KBN_00 As String = "00"  '距離刻み
    Friend Const DATA_TP_KBN_01 As String = "01"  '運賃
    Friend Const DATA_TP_KBN_02 As String = "02"  '保冷温運賃
    Friend Const DATA_TP_KBN_08 As String = "08"  '割増

    ''' <summary>
    ''' テーブルタイプ【区分マスタ：U040】
    ''' </summary>
    Friend Const TABLE_TP_KBN_00 As String = "00"  '重量・距離
    Friend Const TABLE_TP_KBN_01 As String = "01"  '車種・距離
    Friend Const TABLE_TP_KBN_02 As String = "02"  '個数・距離
    Friend Const TABLE_TP_KBN_03 As String = "03"  '重量・距離（重量建）
    Friend Const TABLE_TP_KBN_04 As String = "04"  '数量・距離
    Friend Const TABLE_TP_KBN_05 As String = "05"  '個数・県コード
    Friend Const TABLE_TP_KBN_06 As String = "06"  '宅急便サイズ・県コード
    Friend Const TABLE_TP_KBN_07 As String = "07"  '重量・県（重量建）計算結果小数点以下切捨て
    Friend Const TABLE_TP_KBN_08 As String = "08"  '重量・JISコード
    Friend Const TABLE_TP_KBN_09 As String = "09"  '個数・JISコード

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
        INIT
        KENSAKU
        SANSHO
        SHINKI
        HENSHU
        HUKUSHA
        SAKUJO
        HOZON
        MASTEROPEN
        ENTER
        TOJIRU
        DCLICK
        INS_T           '行追加
        DEL_T           '行削除
        COLADD          '列挿入
        COLDEL          '列削除
        EXCELINPUT      'Excel取込
        EXCELOUTPUT     'Excel出力

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        SHIHARAI_TARIFF_HD = 0
        NRS_BR_CD
        SHIHARAI_TARIFF_CD
        STR_DATE
        DATA_TP
        TABLE_TP
        SHIHARAI_TARIFF_REM
        SHIHARAI_TARIFF_CD2
        BTN_ADD
        BTN_DEL
        BTN_IN_XLS
        BTN_OUT_XLS
        BTN_COL_ADD
        BTN_COL_DEL
        BTN_LOCK
        BTN_UNLOCK
        SHIHARAI_TARIFF_DTL
        SHIHARAI_TARIFF_DTL_B

    End Enum

    ''' <summary>
    ''' Spread部(上部)列インデックス用列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SYS_DEL_NM
        NRS_BR_CD
        NRS_BR_NM
        SHIHARAI_TARIFF_CD
        DATA_TP_NM
        TABLE_TP_NM
        STR_DATE
        SHIHARAI_TARIFF_CD2
        SHIHARAI_TARIFF_REM
        DATA_TP
        TABLE_TP
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG

    End Enum

    ''' <summary>
    ''' Spread部(下部/TypeA)列インデックス用列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex2

        DEF = 0
        WT_LV
        CAR_TP
        NB
        QT
        T_SIZE
        KYORI_1
        KYORI_2
        KYORI_3
        KYORI_4
        KYORI_5
        KYORI_6
        KYORI_7
        KYORI_8
        KYORI_9
        KYORI_10
        KYORI_11
        KYORI_12
        KYORI_13
        KYORI_14
        KYORI_15
        KYORI_16
        KYORI_17
        KYORI_18
        KYORI_19
        KYORI_20
        KYORI_21
        KYORI_22
        KYORI_23
        KYORI_24
        KYORI_25
        KYORI_26
        KYORI_27
        KYORI_28
        KYORI_29
        KYORI_30
        KYORI_31
        KYORI_32
        KYORI_33
        KYORI_34
        KYORI_35
        KYORI_36
        KYORI_37
        KYORI_38
        KYORI_39
        KYORI_40
        KYORI_41
        KYORI_42
        KYORI_43
        KYORI_44
        KYORI_45
        KYORI_46
        KYORI_47
        KYORI_48
        KYORI_49
        KYORI_50
        KYORI_51
        KYORI_52
        KYORI_53
        KYORI_54
        KYORI_55
        KYORI_56
        KYORI_57
        KYORI_58
        KYORI_59
        KYORI_60
        KYORI_61
        KYORI_62
        KYORI_63
        KYORI_64
        KYORI_65
        KYORI_66
        KYORI_67
        KYORI_68
        KYORI_69
        KYORI_70
        CITY_EXTC_A
        CITY_EXTC_B
        WINT_EXTC_A
        WINT_EXTC_B
        RELY_EXTC
        INSU
        '運賃タリフマスタと同じく"FRRY_EXTC_10KG"を除外する
        'FRRY_EXTC_10KG
        FRRY_EXTC_PART
        SHIHARAI_TARIFF_CD_EDA
        UPD_FLG
        SYS_DEL_FLG_T
        SYS_UPD_DATE
        SYS_UPD_TIME
        RECNO

    End Enum

    ''' <summary>
    ''' Spread部(下部/TypeB)列インデックス用列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex3

        DEF = 0
        ORIG_KEN_N
        ORIG_CITY_N
        ORIG_JIS_CD
        DEST_KEN_N
        DEST_CITY_N
        DEST_JIS_CD
        KYORI_1
        KYORI_2
        KYORI_3
        KYORI_4
        KYORI_5
        KYORI_6
        KYORI_7
        KYORI_8
        KYORI_9
        KYORI_10
        KYORI_11
        KYORI_12
        KYORI_13
        KYORI_14
        KYORI_15
        KYORI_16
        KYORI_17
        KYORI_18
        KYORI_19
        KYORI_20
        KYORI_21
        KYORI_22
        KYORI_23
        KYORI_24
        KYORI_25
        KYORI_26
        KYORI_27
        KYORI_28
        KYORI_29
        KYORI_30
        KYORI_31
        KYORI_32
        KYORI_33
        KYORI_34
        KYORI_35
        KYORI_36
        KYORI_37
        KYORI_38
        KYORI_39
        KYORI_40
        KYORI_41
        KYORI_42
        KYORI_43
        KYORI_44
        KYORI_45
        KYORI_46
        KYORI_47
        KYORI_48
        KYORI_49
        KYORI_50
        KYORI_51
        KYORI_52
        KYORI_53
        KYORI_54
        KYORI_55
        KYORI_56
        KYORI_57
        KYORI_58
        KYORI_59
        KYORI_60
        KYORI_61
        KYORI_62
        KYORI_63
        KYORI_64
        KYORI_65
        KYORI_66
        KYORI_67
        KYORI_68
        KYORI_69
        KYORI_70
        '運賃タリフマスタと同じく"FRRY_EXTC_10KG"を除外する
        'FRRY_EXTC_10KG
        FRRY_EXTC_PART
        SHIHARAI_TARIFF_CD_EDA
        UPD_FLG
        SYS_DEL_FLG_T
        SYS_UPD_DATE
        SYS_UPD_TIME
        RECNO

    End Enum

    ''' <summary>
    ''' Spread部(下部)利用状況用列挙体
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SpreadType

        A = 0   'TypeA
        B       'TypeB

    End Enum

End Class
