' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB020C : 入荷データ編集
'  作  成  者       :  [iwamoto]
' ==========================================================================

''' <summary>
''' LMB020定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB020C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    'Public Const TABLE_NM_IN As String = "LMB020IN"
    Public Const TABLE_NM_INKA_L As String = "LMB020_INKA_L"
    Public Const TABLE_NM_INKA_M As String = "LMB020_INKA_M"
    Public Const TABLE_NM_INKA_S As String = "LMB020_INKA_S"
    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    Public Const TABLE_NM_TOU_SITU_EXP As String = "LMB020_TOU_SITU_EXP"
    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end
    'START ADD 2013/09/10 KURIHARA WIT対応
    Public Const TABLE_NM_INKA_WK As String = "LMB020_INKA_WK"
    'END   ADD 2013/09/10 KURIHARA WIT対応
    Public Const TABLE_NM_UNSO_L As String = "LMB020_UNSO_L"
    Public Const TABLE_NM_UNSO_M As String = "LMB020_UNSO_M"
    Public Const TABLE_NM_UNCHIN As String = "F_UNCHIN_TRS"
    Public Const TABLE_NM_SAGYO As String = "LMB020_SAGYO"
    Public Const TABLE_NM_ZAI As String = "LMB020_ZAI"
    Public Const TABLE_NM_MAX_NO As String = "LMB020_MAX_NO"
    Public Const TABLE_NM_ERR As String = "ERR"
    Public Const TABLE_NM_PRINT_TYPE As String = "LMB020_PRINT_TYPE"
    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
    Public Const TABLE_NM_SHIHARAI As String = "F_SHIHARAI_TRS"
    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End
    '要望番号:1350 terakawa 2012.08.27 Start
    Public Const TABLE_NM_WORNING As String = "LMB020_WORNING"
    '要望番号:1350 terakawa 2012.08.27 End
    Public Const TABLE_NM_GOODS As String = "LMB020_GOODS"  '2013/02/14　ハネウェルのRC(CSV)対応　篠原
    Public Const TABLE_NM_CSV_DATA As String = "LMB020_CSV_DATA"  '2013/02/14　ハネウェルのRC(CSV)対応　篠原
    '2013.07.16 追加START
    Public Const TABLE_NM_KENPIN_DATA As String = "LMB020_KENPIN_DATA"  '2013/07/16　入荷検品(ローム)対応
    '2013.07.16 追加END

    '2014.07.28 追加 -ST-
    Public Const TABLE_NM_KENPIN_WK_DATA As String = "LMB020_KENPIN_WK_DATA"  '2014.07.28　入荷検品(ｱｸﾞﾘ)対応
    '2014.07.28 追加 -ED-
    Public Const TABLE_NM_KENPIN_WK_TORI_RESET As String = "LMB020_KENPIN_WK_TORI_RESET"  'ADD 2019/12/02 006350
    Public Const TABLE_NM_INKA_SEQ_QR As String = "LMB020_INKA_SEQ_QR"
    Public Const TABLE_NM_TAB_DTL As String = "LMB020_TAB_DTL"
    Public Const TABLE_NM_GOODS_DETAILS_GET As String = "LMB020_GOODS_DETAILS_GET"    'ADD 2020/08/06 014005   【LMS】商品マスタ_入荷仮置場機能の追加
    Public Const TABLE_NM_INKA_PHOTO As String = "LMB020_INKA_PHOTO"    'ADD 2022/11/07 倉庫写真アプリ対応
    Public Const TABLE_NM_HOKAN_NIYAKU_CALCULATION As String = "LMB020_HOKAN_NIYAKU_CALCULATION"

    '前ゼロ
    Public Const MAEZERO As String = "000"

    '運送会社
    Public Const UNSOCO_CD As String = "001"

    '入荷区分(通常)
    Public Const INKAKBN_NOMAL As String = "10"

    '手配区分
    Public Const TEHAI_NRS As String = "10"
    Public Const TEHAI_MITEI As String = "90"

    'テーブルタイプ(宅急便)
    Public Const TABLE_TYPE_TAK As String = "06"

    '有無フラグ(有)
    Public Const UMU_ARI As String = "01"

    '温度管理区分(定温)
    Public Const ONKAN_TEION As String = "02"

    '割当優先区分(フリー)
    Public Const WARIATE_FREE As String = "10"

    '元着払区分(元払い)
    Public Const PC_KB_MOTO As String = "01"

    'フラグ(ON)
    Public Const FLG_ON As String = "01"

    'フラグ(OFF)
    Public Const FLG_OFF As String = "00"

    'タリフ区分
    Public Const TARIFF_KONSAI As String = "10"
    Public Const TARIFF_KURUMA As String = "20"
    Public Const TARIFF_TOKUBIN As String = "30"
    Public Const TARIFF_YOKO As String = "40"
    Public Const TARIFF_ROSEN As String = "50"

    '計算コード区分
    Public Const CALC_KB_NISUGATA As String = "01"
    Public Const CALC_KB_KURUMA As String = "02"

    '入荷種別(通常)
    Public Const SHUBETU_NOMAL As String = "10"

    '要望番号:1097 yamanaka 2012.07.18 Start
    '入荷種別(振替)
    Public Const SHUBETU_HURIKAE As String = "50"
    '要望番号:1097 yamanaka 2012.07.18 End

    'ステージ
    Public Const STATE_YOTEI As String = "10"
    Public Const STATE_UKETUKEHYOINSATU As String = "20"
    Public Const STATE_UKETUKEZUMI As String = "30"
    Public Const STATE_NYUKAKEPPINZUMI As String = "40"
    Public Const STATE_NYUKOZUMI As String = "50"
    Public Const STATE_NYUKOHOUKOKUZUMI As String = "90"

    '印刷種別(入庫報告)
    Public Const PRINT_NYUKOHOKOKU As String = "01"
    '要望番号:1523 terakawa 2012.10.22 Start
    Public Const PRINT_CHECKLIST As String = "02"
    '要望番号:1523 terakawa 2012.10.22 End

    '作業の元データ区分
    Public Const MOTODATA_KBN_INKA As String = "10"
    Public Const MOTODATA_KBN_INKA_L As String = "10"
    Public Const MOTODATA_KBN_INKA_M As String = "11"

    '内外貨区分
    Public Const CURR_KBN_NAI As String = "01"

    '事由区分
    Public Const JIYU_KBN_TORIHIKI As String = "02"

    '便区分
    Public Const BIN_KBN_NOMAL As String = "01"

    'ソートの桁数
    Public Const SORT_MAX As String = "99"

    'フリー期間の桁数
    Public Const FREE_MAX As String = "999"

    '入荷(小)梱数の桁数
    Public Const NB_MAX As String = "9999999999"
    Public Const NB_MIN As String = "0"

    '運賃の桁数
    Public Const UNCHIN_MAX As String = "9999999999.99"
    Public Const UNCHIN_MIN As String = "0"

    '入荷(小)個数の桁数
    Public Const KOSU_MAX As String = "9999999999"
    Public Const KOSU_MIN As String = "0"

    '距離の桁数
    Public Const KYORI_MAX As String = "99999"
    Public Const KYORI_MIN As String = "0"

    '入荷(小)数量の桁数
    Public Const SURYO_MAX As String = "999999999.999"
    Public Const SURYO_MIN As String = "0"

    '入荷(小)重量の桁数
    Public Const JURYO_MAX As String = "999999999.999"
    Public Const JURYO_MIN As String = "0"
    Public Const JURYO_ROUND_POS As Integer = 3

    '入数の桁数
    Public Const IRISU_MAX As String = "99999999"
    Public Const IRISU_MIN As String = "0"

    '入目の桁数
    Public Const IRIME_MAX As String = "999999.999"
    Public Const IRIME_MIN As String = "0.001"

    '入目の桁数
    Public Const UNSO_JURYO_MAX As String = "999999999.999"
    Public Const UNSO_JURYO_MIN As String = "0"

    '予想数量の桁数
    Public Const PLANE_QT_MAX As String = "999999999.999"
    Public Const PLANE_QT_MIN As String = "0"

    'START YANAI 要望番号557
    '複写行数の桁数
    Public Const COPY_S_MAX As String = "999"
    Public Const COPY_S_MIN As String = "1"
    'END YANAI 要望番号557

    '引当状況
    Public Const HIKIATE_ARI As String = "済"
    Public Const HIKIATE_NASI As String = "未"

    '作業有無
    Public Const SAGYO_ARI As String = "有"
    Public Const SAGYO_NASI As String = "無"

    '作業の項目名
    Public Const SAGYO_PK As String = "lblRecNo"
    Public Const SAGYO_CD As String = "txtSagyoCd"
    Public Const SAGYO_NM As String = "lblSagyoNm"
    Public Const SAGYO_FL As String = "lblSagyoFlg"
    Public Const SAGYO_UP As String = "lblAddFlg"
    Public Const SAGYO_RMK_SIJI As String = "txtSagyoRemark"

    '作業レコードの最大レコード数
    Public Const SAGYO_MAX_REC As Integer = 5

    '作業ステージ
    Public Const SAGYO_STAGE_END As String = "01"

    'スプレッド日付のフルバイト
    Public Const SPR_DATE_FULL As Integer = 10

    '請求金額計算区分
    Public Const SEIQ_CALC_ZERO As String = "01"
    
    '更新区分
    Public Const INSERT As String = "0"
    Public Const UPDATE As String = "1"
    Public Const DELETE As String = "2"

    '用途区分
    Public Const YOTO_INKA As String = "01"

    '作業完了(01)
    Public Const SAGYO_KANRYO_ON As String = "01"

    '作業完了(00)
    Public Const SAGYO_KANRYO_OFF As String = "00"

    '日付初期値
    Public Const INKA_DATE_INIT_01 As String = "01"
    Public Const INKA_DATE_INIT_02 As String = "02"
    Public Const INKA_DATE_INIT_03 As String = "03"
    Public Const INKA_DATE_INIT_04 As String = "04"
    Public Const INKA_DATE_INIT_05 As String = "05"

    '課税区分
    Public Const TAX_KB01 As String = "01"  '課税
    Public Const TAX_KB02 As String = "02"  '免税
    Public Const TAX_KB03 As String = "03"  '非課税
    Public Const TAX_KB04 As String = "04"  '内税

    'START ADD 2013/09/10 KURIHARA WIT対応
    '検品確定フラグ
    Public Const KENPIN_KAKUTEI As String = "01"
    'END   ADD 2013/09/10 KURIHARA WIT対応

    'プログラムID
    Public Const PGID_LMD040 As String = "LMD040"

    '2015.10.20 tusnehira add
    '英語化対応
    Public Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Public Const MESSEGE_LANGUAGE_ENGLISH As String = "1"

    'ADD 2022/11/07 倉庫写真アプリ対応 START
    '登録済み画像
    Public Const PHOTO_YN_NM As String = "登録済"
    'ADD 2022/11/07 倉庫写真アプリ対応 END

#If True Then ' JT物流入荷検品対応 20160726 added inoue


    ''' <summary>
    ''' 検品チェック単位
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CHK_TANI

        ''' <summary>
        ''' 商品コード単位
        ''' </summary>
        ''' <remarks></remarks>
        Public Const GOODS As String = "01"

        ''' <summary>
        ''' 商品コード・ロット単位
        ''' </summary>
        ''' <remarks></remarks>
        Public Const GOODS_AND_LOT As String = "02"

        ''' <summary>
        ''' 商品管理番号単位
        ''' </summary>
        ''' <remarks></remarks>
        Public Const GOODS_KANRI_NO As String = "03"


        ''' <summary>
        ''' 商品コード・ロット・入り目単位
        ''' </summary>
        ''' <remarks></remarks>
        Public Const GOODS_AND_LOT_AND_IREME As String = "04"

        ''' <summary>
        ''' 商品コード・ロット・入り目・製造日単位
        ''' </summary>
        ''' <remarks></remarks>
        Public Const GOODS_AND_LOT_AND_IREME_AND_MFG_DATE As String = "05"

    End Class

#End If

    ''' <summary>
    ''' 入荷Mデータ取込処理フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Public Class M_DATA_FLG

        ''' <summary>
        ''' 入荷M明細
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INKA_M_DTL As String = "0"

        ''' <summary>
        ''' CSV取り込み
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CSV As String = "1"

        ''' <summary>
        ''' 検品WK
        ''' </summary>
        ''' <remarks></remarks>
        Public Const KENPIN_WK As String = "2"


        ''' <summary>
        ''' 日陸連番QR番号
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NRS_SEQ_QR_NO As String = "3"

    End Class

    ''' <summary>
    ''' 庫内作業ステータス(入荷)[W008]
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

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex
        BTN_TAB_TORIKOMI = 0
        JIKKOU
        BTN_JIKKOU
        PRINT
        LABEL_TYPE          'ADD 2017/08/04
        BTN_PRINT
        NYUKAL
        KANRINOL
        EIGYO
        SOKO
        NYUKATYPE
        WH_WK_STATUS
        WH_TAB_SIJI_STATUS
        WH_TAB_SAGYO_STATUS
        NYUKAKBN
        SHINSHOKUKBN
        NYUKADATE
        STORAGE_DUE_DATE    'ADD 2017/07/10
        HURIKANRINO
        BUYERORDNO
        ORDERNO
        FREEKIKAN
        HOKANSTRDATE
        WH_TAB_NO_SIJI
        CUSTCDL
        CUSTCDM
        CUSTNM
        KAZEIKBN
        TOUKIHOKANUMU
        ZENKIHOKANUMU
        NIYAKUUMU
        CHKSTOPALLOC    'ADD 2019/08/01 要望管理005237
        PLANQT
        PLANQTUT
        NYUKACNT
        NYUBANL
        NYUKACOMMENT
        MIDDLE
        GOODS
        ROWADDM
        ROWDELM
        LIST
        SERCHGOODSCD
        SERCHGOODSNM
        PNL_HENKO
        HENKO_INJUN
        BTN_HENKO
        TOUNO
        SITUNO
        ZONECD
        LOCATION
        BTN_ALLCHANGE
        GOODSDEF
        KANRINOM
        GOODSCD
        GOODSNM
        SORT
        HIKIATE
        ONDO
        SUMCNT
        SURYO
        IRISU
        STDIRIME
        TARE
        EDIKOSU
        EDISURYO
        ORDERNOM
        BUYERORDNOM
        GOODSCOMMENT
        SAGYOM
        SAGYOCDM1
        SAGYONMM1
        SAGYOFLGM1
        SAGYORMKM1
        SAGYOCDM2
        SAGYONMM2
        SAGYOFLGM2
        SAGYORMKM2
        SAGYOCDM3
        SAGYONMM3
        SAGYOFLGM3
        SAGYORMKM3
        SAGYOCDM4
        SAGYONMM4
        SAGYOFLGM4
        SAGYORMKM4
        SAGYOCDM5
        SAGYONMM5
        SAGYOFLGM5
        SAGYORMKM5
        TAB_UNSO
        PNL_UNSO
        UNSONO
        UNCHINUMU
        UNCHINKBN
        SHARYOKBN
        TRNTHERMOKBN
        UNSOCD
        TRNBRCD
        TRNNM
        UNCHIN
        UNSOTARIFFCD
        UNSOTARIFFNM
        '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
        SHIHARAITARIFFCD
        SHIHARAITARIFFNM
        '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End
        UNSOJURYO
        SHUKKAMOTOCD
        SHUKKAMOTONM
        KYORI
        UNCHINCOMMENT
        UNSOKAZEIKBN
        YUSONRS
        SAGYOL
        SAGYOCD_L1
        SAGYONM_L1
        SAGYOFLG_L1
        SAGYORMK_L1
        SAGYOCD_L2
        SAGYONM_L2
        SAGYOFLG_L2
        SAGYORMK_L2
        SAGYOCD_L3
        SAGYONM_L3
        SAGYOFLG_L3
        SAGYORMK_L3
        SAGYOCD_L4
        SAGYONM_L4
        SAGYOFLG_L4
        SAGYORMK_L4
        SAGYOCD_L5
        SAGYONM_L5
        SAGYOFLG_L5
        SAGYORMK_L5
        ROWADDS
        ROWDELS
        ROWCOPYS
        'START YANAI 要望番号557
        ROWCOPYSCNT
        'END YANAI 要望番号557
        ENTRYCNT
        BTN_PHOTOSEL    'ADD 2022/11/07 倉庫写真アプリ対応
        BTN_IMGADD
        BTN_COAADD
        BTN_YCARDADD
        DETAIL
    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対(INKA_M)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprInkaMColumnIndex

        DEF = 0
        SORT_NO
        KANRI_NO
        GOODS_CD
        GOODS_NM
        ALL_KOSU
        ALL_SURYO
        HIKIATE_STATE
        GOODS_COMMENT
        ORDER_NO
        SAGYO_UMU
        RECNO

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対(INKA_S)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprInkaSColumnIndex

        DEF = 0
        KANRI_NO_S
        IMG_YN
        LOT_NO
        TOU_NO
        SHITSU_NO
        ZONE_CD
        LOCA
        NB
        HASU
        SUM
        IRIME
        TANI
        SURYO
        JURYO
        LT_DATE
        ENT_PHOTO   'ADD 2022/11/07 倉庫写真アプリ対応
        REMARK
        REMARK_OUT
        BUG_YN
        GOODS_COND_KB_1
        GOODS_COND_KB_2
        GOODS_COND_KB_3
        OFB_KBN
        SPD_KBN_S
        SERIAL_NO
        GOODS_CRT_DATE
        DEST_CD
        DEST_NM
        ALLOC_PRIORITY
        LOT_CTL_KB
        LT_DATE_CTL_KB
        CRT_DATE_CTL_KB
        RECNO
        STD_WT
        'GOODS_CD_NRS        '2013.07.16 追加START
        'GOODS_CD_CUST        '2013.07.16 追加END

    End Enum

    ''' <summary>
    ''' アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ActionType As Integer

        INIT
        NEWMODE
        EDIT
        COPY
        DELETE
        UNSOEDIT
        DATEEDIT
        MASTEROPEN
        SAVE
        CLOSE
        ENTER
        DOUBLECLICK
        TEHAI_CHANGED
        TARIFF_CHANGED
        PRINT
        INIT_M
        DEL_M
        INIT_S
        DEL_S
        HENKO
        CSVINPUT      'CSV取込(2013/02/14追加)
        COA
        YCARD
        PHOTOSEL    'ADD 2022/11/07 倉庫写真アプリ対応

    End Enum

    ''' <summary>
    ''' 作業データ設定 切替用
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SagyoData As Integer
        L
        M
    End Enum

    '2017/11/15 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    ''' <summary>
    ''' 危険物チェック エラー、ワーニング切替用
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DangerousGoodsCheckErrorOrWarning
        Warning = 0
        Err = 1
    End Enum
    '2017/11/15 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"

    'タブレット対応
    '作業指示ステータス
    Public Const WH_TAB_SIJI_00 As String = "00"
    Public Const WH_TAB_SIJI_01 As String = "01"
    Public Const WH_TAB_SIJI_02 As String = "02"
    '作業ステータス
    Public Const WH_TAB_SAGYO_00 As String = "00"
    Public Const WH_TAB_SAGYO_01 As String = "01"
    Public Const WH_TAB_SAGYO_02 As String = "02"
    Public Const WH_TAB_SAGYO_03 As String = "03"
    Public Const WH_TAB_SAGYO_04 As String = "04"
    Public Const WH_TAB_SAGYO_05 As String = "05"
    Public Const WH_TAB_SAGYO_99 As String = "99"
    '現場作業有無
    Public Const WH_TAB_YN_00 As String = "00"
    Public Const WH_TAB_YN_01 As String = "01"
    '現場作業取込有無
    Public Const WH_TAB_IMP_YN_00 As String = "00"
    Public Const WH_TAB_IMP_YN_01 As String = "01"
    '再指示不要フラグ
    Public Const WH_TAB_NO_SIJI_YN_00 As String = "00"
    Public Const WH_TAB_NO_SIJI_YN_01 As String = "01"

    'ADD Start 2019/08/01 要望管理005237
    Public Class StopAllocYN
        Public Const Yes As String = "1"
        Public Const No As String = ""
    End Class
    'ADD End   2019/08/01 要望管理005237
    'Add Start 2019/10/09 要望管理007373
    ''' <summary>保留品区分</summary>
    Public Class SPD_KB
        Public Const ShipOK As String = "01"        '出荷可能
        Public Const Unpermitted As String = "16"   '未許可
    End Class
    'Add End   2019/10/09 要望管理007373

End Class