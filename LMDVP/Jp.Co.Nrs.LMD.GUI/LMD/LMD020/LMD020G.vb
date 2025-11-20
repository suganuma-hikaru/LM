' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMD     : 在庫
'  プログラムID   : LMD020G : 
'  作  成  者     : 大貫和正
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LMD020Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD020G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD020F

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDConG As LMDControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMD020F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Gamen共通クラスの設定
        Me._LMDConG = New LMDControlG(handlerClass, DirectCast(frm, Form))

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey(ByVal actionType As LMD020C.ActionType)

        Dim always As Boolean = True
        Dim allLock As Boolean = False
        Dim lock1 As Boolean = True

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True
            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = "在庫履歴"
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = "検　索"
            .F10ButtonName = "マスタ参照"
            .F11ButtonName = "保　存"
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = allLock
            .F2ButtonEnabled = allLock
            .F3ButtonEnabled = allLock
            .F4ButtonEnabled = allLock
            .F5ButtonEnabled = allLock
            .F6ButtonEnabled = lock1
            .F7ButtonEnabled = allLock
            .F8ButtonEnabled = allLock
            .F9ButtonEnabled = lock1
            .F10ButtonEnabled = lock1
            .F11ButtonEnabled = lock1
            .F12ButtonEnabled = always

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region 'FunctionKey

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm
            'Main
            .pnlKensaku.TabIndex = LMD020C.CtlTabIndex.KENSAKU
            .cmbNrsBrCd.TabIndex = LMD020C.CtlTabIndex.NRSBRCD
            .cmbSoko.TabIndex = LMD020C.CtlTabIndex.SOKO
            .txtCustCdL.TabIndex = LMD020C.CtlTabIndex.CUSTCDL
            .txtCustCdM.TabIndex = LMD020C.CtlTabIndex.CUSTCDM
            .lblCustNM.TabIndex = LMD020C.CtlTabIndex.CUSTNM
            .imdNyukaFrom.TabIndex = LMD020C.CtlTabIndex.NYUKAFROM
            .imdNyukaTo.TabIndex = LMD020C.CtlTabIndex.NYUKATO
            .optAll.TabIndex = LMD020C.CtlTabIndex.ALL
            .optDefectiveProduct.TabIndex = LMD020C.CtlTabIndex.DEFECTIVEPRODUCT
            .pnlSearchInput.TabIndex = LMD020C.CtlTabIndex.SEARCHINPUT
            .cmbJiyuran.TabIndex = LMD020C.CtlTabIndex.CJIYURAN
            .txtJiyuran.TabIndex = LMD020C.CtlTabIndex.TJIYURAN
            .imdIdoubi.TabIndex = LMD020C.CtlTabIndex.IDOUBI
            .optHeikouIdo.TabIndex = LMD020C.CtlTabIndex.HEIKOUIDO
            .optFukusuIdo.TabIndex = LMD020C.CtlTabIndex.FUKUSUIDO
            .optKyoseiShuko.TabIndex = LMD020C.CtlTabIndex.KYOSEISHUKO
            .btnSprMoveLeft.TabIndex = LMD020C.CtlTabIndex.SPRMOVELEFT
            .btnSprMoveRight.TabIndex = LMD020C.CtlTabIndex.SPRMOVERIGHT
            .btnLineAdd.TabIndex = LMD020C.CtlTabIndex.LINEADD
            .btnLineDel.TabIndex = LMD020C.CtlTabIndex.LINEDEL
            .pnlOkiba.TabIndex = LMD020C.CtlTabIndex.OKIBA
            .txtTouNo.TabIndex = LMD020C.CtlTabIndex.TOUNO
            .txtSituNo.TabIndex = LMD020C.CtlTabIndex.SITUNO
            .txtZoneCd.TabIndex = LMD020C.CtlTabIndex.ZONECD
            .txtLocation.TabIndex = LMD020C.CtlTabIndex.LOCATION
            .cmbGoodsCondKb1.TabIndex = LMD020C.CtlTabIndex.GOODSCONDKB1
            .cmbGoodsCondKb2.TabIndex = LMD020C.CtlTabIndex.GOODSCONDKB2
            .cmbGoodsCondKb3.TabIndex = LMD020C.CtlTabIndex.GOODSCONDKB3
            .cmbSpdKb.TabIndex = LMD020C.CtlTabIndex.SPDKB
            .cmbOfbKb.TabIndex = LMD020C.CtlTabIndex.OFBKB
            .imdLtDate.TabIndex = LMD020C.CtlTabIndex.LTDATE
            .imdGoodsCrtDate.TabIndex = LMD020C.CtlTabIndex.GOODSCRTDATE
            .cmdAllocPriority.TabIndex = LMD020C.CtlTabIndex.ALLOCPRIORITY
            .txtDestCd.TabIndex = LMD020C.CtlTabIndex.DESTCD
            .txtRsvNo.TabIndex = LMD020C.CtlTabIndex.RSVNO
            .txtRemarkOut.TabIndex = LMD020C.CtlTabIndex.REMARKOUT
            .txtRemark.TabIndex = LMD020C.CtlTabIndex.REMARK
            .btnAllChange.TabIndex = LMD020C.CtlTabIndex.ALLCHANGE
            .sprMoveBefor.TabIndex = LMD020C.CtlTabIndex.MOVELEFT
            .sprMoveAfter.TabIndex = LMD020C.CtlTabIndex.MOVERIGHT

        End With



    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

    End Sub

    ''' <summary>
    ''' コンボボックスの初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetcmbValue()

        'コンボボックスの初期値を設定する
        With Me._Frm
            .cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()
            .cmbJiyuran.SelectedValue = "01"

            '2014.08.04 FFEM高取対応 START
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

            If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
                .cmbNrsBrCd.ReadOnly = True
            Else
                .cmbNrsBrCd.ReadOnly = False
            End If
            '2014.08.04 FFEM高取対応 END

            '状態荷主
            .cmbGoodsCondKb3.ReadOnly = True

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal actionType As LMD020C.ActionType)

        With Me._Frm

            With Me._Frm
                Select Case actionType
                    Case LMD020C.ActionType.MAIN
                        .cmbNrsBrCd.Focus()
                    Case LMD020C.ActionType.KENSAKU
                        .sprMoveBefor.Focus()
                End Select

            End With

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = String.Empty
            .cmbSoko.SelectedValue = String.Empty
            .txtCustCdL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNM.TextValue = String.Empty
            .imdNyukaFrom.TextValue = String.Empty
            .imdNyukaTo.TextValue = String.Empty
            .optAll.Checked = True
            .optDefectiveProduct.Checked = False
            .cmbJiyuran.SelectedValue = String.Empty
            .txtJiyuran.TextValue = String.Empty
            .imdIdoubi.TextValue = String.Empty
            .optHeikouIdo.Checked = True
            .optFukusuIdo.Checked = False
            .optKyoseiShuko.Checked = False
            .txtTouNo.TextValue = String.Empty
            .txtSituNo.TextValue = String.Empty
            .txtZoneCd.TextValue = String.Empty
            .txtLocation.TextValue = String.Empty
            .cmbGoodsCondKb1.SelectedValue = String.Empty
            .cmbGoodsCondKb2.SelectedValue = String.Empty
            .cmbGoodsCondKb3.SelectedValue = String.Empty
            .cmbSpdKb.SelectedValue = String.Empty
            .cmbOfbKb.SelectedValue = String.Empty
            .imdLtDate.TextValue = String.Empty
            .imdGoodsCrtDate.TextValue = String.Empty
            .cmdAllocPriority.SelectedValue = String.Empty
            .txtDestCd.TextValue = String.Empty
            .txtRsvNo.TextValue = String.Empty
            .txtRemarkOut.TextValue = String.Empty
            .txtRemark.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' ラジオボタンの値からのコントロール個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlBtnCtrl()

        With Me._Frm

            If .optHeikouIdo.Checked = True _
               OrElse (.optFukusuIdo.Checked = True AndAlso .sprMoveAfter.Sheets(0).RowCount <= 1) Then

                '行追加、行削除ボタンを使用不可に設定
                Me.LockButton(.btnLineAdd, True)
                Me.LockButton(.btnLineDel, True)

            ElseIf .optFukusuIdo.Checked = True AndAlso 1 < .sprMoveAfter.Sheets(0).RowCount Then

                '行追加、行削除ボタンを使用可に設定
                Me.LockButton(.btnLineAdd, False)
                Me.LockButton(.btnLineDel, False)

            ElseIf .optKyoseiShuko.Checked = True Then

                '行追加、行削除ボタンを使用不可に設定
                Me.LockButton(.btnLineAdd, True)
                Me.LockButton(.btnLineDel, True)

            End If


        End With

    End Sub

    ''' <summary>
    ''' ラジオボタンのロック
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlOptCtrl(ByVal lockFlg As Boolean)

        With Me._Frm

            Me.LockOptionButton(.optFukusuIdo, lockFlg)
            Me.LockOptionButton(.optHeikouIdo, lockFlg)

            If LMD020C.NRS_BR_CD_TOKE.Equals(.cmbNrsBrCd.SelectedValue.ToString) Then
                Me.LockOptionButton(.optKyoseiShuko, lockFlg)
            Else
                '土気以外は常に使用不可
                Me.LockOptionButton(.optKyoseiShuko, True)
            End If

        End With

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' 移動元スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprMoveBefor
        'スプレッド(タイトル列)の設定

        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.DEF, " ", 20, True)
        ''START YANAI 要望番号550
        'Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.GOODS_NM, "商品名", 200, True)
        'Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.LOT_NO, "ロット№", 100, True)
        'Public Shared IRIME As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.IRIME, "入目", 65, True)
        'Public Shared STD_IRIME_UT As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.STD_IRIME_UT, "単位", 50, True)
        'Public Shared INKO_DATE As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.INKO_DATE, "入荷日", 85, True)
        'Public Shared SERIAL_NO As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.SERIAL_NO, "シリアル№", 90, True)
        'Public Shared ALLOC_CAN_NB As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.ALLOC_CAN_NB, "残数", 90, True)
        'Public Shared PKG_UT1 As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.PKG_UT, "単位", 40, True)
        'Public Shared ALCTD_NB As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.ALCTD_NB, "引当中", 90, True)
        'Public Shared PORA_ZAI_NB As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.PORA_ZAI_NB, "実数", 90, True)
        Public Shared GOODS_CD_CUST As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.GOODS_CD_CUST, "商品コード", 100, True)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.GOODS_NM, "商品名", 150, True)
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.LOT_NO, "ロット№", 80, True)
        Public Shared IRIME As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.IRIME, "入目", 60, True)
        Public Shared STD_IRIME_UT As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.STD_IRIME_UT, "単位", 40, True)
        Public Shared INKO_DATE As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.INKO_DATE, "入荷日", 75, True)
        Public Shared SERIAL_NO As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.SERIAL_NO, "シリアル№", 80, True)
        Public Shared ALLOC_CAN_NB As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.ALLOC_CAN_NB, "残数", 60, True)
        Public Shared PKG_UT1 As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.PKG_UT, "単位", 40, True)
        Public Shared ALCTD_NB As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.ALCTD_NB, "引当中", 60, True)
        Public Shared PORA_ZAI_NB As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.PORA_ZAI_NB, "実数", 60, True)
        'END YANAI 要望番号550
        Public Shared TOU_NO As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.TOU_NO, "棟", 50, True)
        Public Shared SITU_NO As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.SITU_NO, "室", 50, True)
        Public Shared ZONE_CD As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.ZONE_CD, "ZONE", 50, True)
        Public Shared LOCA As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.LOCA, "ロケーション", 100, True)
        'START YANAI 要望番号550
        'Public Shared GOODS_CD_CUST As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.GOODS_CD_CUST, "商品コード", 150, True)
        'END YANAI 要望番号550
        Public Shared GOODS_COND_NM_1 As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.GOODS_COND_NM_1, "状態 中身", 100, True)
        Public Shared GOODS_COND_NM_2 As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.GOODS_COND_NM_2, "状態 外装", 160, True)
        Public Shared GOODS_COND_NM_3 As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.GOODS_COND_NM_3, "状態 荷主", 100, True)
        Public Shared SPD_KB_NM As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.SPD_KB_NM, "保留品", 100, True)
        Public Shared OFB_KB_NM As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.OFB_KB_NM, "薄外品", 70, True)
        Public Shared KEEP_GOODS_NM As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.KEEP_GOODS_NM, "キープ品", 115, False)
        Public Shared LT_DATE As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.LT_DATE, "賞味期限", 85, True)
        Public Shared GOODS_CRT_DATE As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.GOODS_CRT_DATE, "製造日", 85, True)
        Public Shared SEARCH_KEY_2 As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.SEARCH_KEY_2, "荷主カテゴリ２", 120, True)
        Public Shared ALLOC_PRIORITY_NM As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.ALLOC_PRIORITY_NM, "割当優先", 100, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.DEST_NM, "届先名", 60, True)
        Public Shared RSV_NO As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.RSV_NO, "予約番号", 100, True)
        Public Shared REMARK_OUT As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.REMARK_OUT, "備考小(社外)", 120, True)
        Public Shared ZAI_REC_NO As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.ZAI_REC_NO, "在庫レコード" & vbCrLf & "番号", 120, True)
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.CUST_NM_L, "荷主名(大)", 100, True)
        Public Shared CUST_NM_M As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.CUST_NM_M, "荷主名(中)", 100, True)
        'START YANAI 要望番号766
        Public Shared CUST_CD_S As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.CUST_CD_S, "小ＣＤ", 50, True)
        'END YANAI 要望番号766
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.REMARK, "備考小(社内)", 120, True)
        Public Shared ALCTD_QT As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.ALCTD_QT, "引当数量", 90, True)
        Public Shared ALLOC_CAN_QT As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.ALLOC_CAN_QT, "残数量", 90, True)
        Public Shared PORA_ZAI_QT As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.PORA_ZAI_QT, "実数量", 90, True)
        Public Shared PKG_NB As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.PKG_NB, "入数", 45, True)
        Public Shared PKG_UT2 As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.PKG_UT_QT, "単位", 40, True)
        Public Shared HOKAN_NIYAKU_CALCULATION As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.HOKAN_NIYAKU_CALCULATION, "最終請求日", 85, True)

        '↓非表示項目
        Public Shared OUTKO_DATE As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.OUTKO_DATE, "最終出荷日", 85, False)
        Public Shared GOODS_CD_NRS As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.GOODS_CD_NRS, "商品KEY", 0, False)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.CUST_CD_L, "荷主コード大", 100, False)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.CUST_CD_M, "荷主コード中", 100, False)
        Public Shared INKA_NO_L As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.INKA_NO_L, "入荷管理番号大", 100, False)
        Public Shared INKA_NO_M As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.INKA_NO_M, "入荷管理番号中", 100, False)
        Public Shared INKA_NO_S As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.INKA_NO_S, "入荷管理番号小", 100, False)
        Public Shared HOKAN_YN As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.HOKAN_YN, "保管料有無", 100, False)
        Public Shared TAX_KB As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.TAX_KB, "課税区分", 100, False)
        Public Shared ZERO_FLAG As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.ZERO_FLAG, "ゼロフラグ", 100, False)
        Public Shared SMPL_FLAG As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.SMPL_FLAG, "小分け実施フラグ", 100, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.SYS_UPD_DATE, "更新日付", 100, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.SYS_UPD_TIME, "更新時刻", 100, False)
        Public Shared ROW_NO As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.ROW_NO, "行番号", 100, False)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.NRS_BR_CD, "営業所コード", 100, False)
        Public Shared WH_CD As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.WH_CD, "倉庫コード", 100, False)
        'START ADD 2013/09/10 KOBAYASHI WIT対応
        Public Shared GOODS_KANRI_NO As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveBefor.GOODS_KANRI_NO, "商品管理番号", 100, False)
        'END   ADD 2013/09/10 KOBAYASHI WIT対応

    End Class

    ''' <summary>
    ''' 移動先スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprMoveAfter

        'スプレッド(タイトル列)の設定
        Public Shared DEF_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.DEF_R, " ", 20, True)
        Public Shared TOU_NO_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.TOU_NO_R, "棟", 50, True)
        Public Shared SITU_NO_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.SITU_NO_R, "室", 50, True)
        Public Shared ZONE_CD_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.ZONE_CD_R, "ZONE", 50, True)
        Public Shared LOCA_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.LOCA_R, "ロケーション", 100, True)
        Public Shared IDO_KOSU_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.IDO_KOSU_R, "移動個数", 90, True)
        Public Shared GOODS_COND_KB_1_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.GOODS_COND_KB_1_R, "状態 中身", 100, True)
        Public Shared GOODS_COND_KB_2_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.GOODS_COND_KB_2_R, "状態 外装", 160, True)
        Public Shared GOODS_COND_KB_3_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.GOODS_COND_KB_3_R, "状態 荷主", 100, True)
        Public Shared SPD_KB_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.SPD_KB_R, "保留品", 100, True)
        Public Shared OFB_KB_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.OFB_KB_R, "薄外品", 70, True)
        Public Shared BYK_KEEP_GOODS_CD_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.BYK_KEEP_GOODS_CD_R, "キープ品", 120, False)
        Public Shared LT_DATE_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.LT_DATE_R, "賞味期限", 85, True)
        Public Shared GOODS_CRT_DATE_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.GOODS_CRT_DATE_R, "製造日", 85, True)
        Public Shared ALLOC_PRIORITY_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.ALLOC_PRIORITY_R, "割当優先", 100, True)
        Public Shared DEST_CD_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.DEST_CD_R, "届先コード", 100, True)
        Public Shared DEST_NM_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.DEST_NM_R, "届先名", 100, True)
        Public Shared RSV_NO_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.RSV_NO_R, "予約番号", 100, True)
        Public Shared REMARK_OUT_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.REMARK_OUT_R, "備考小(社外)", 120, True)
        Public Shared REMARK_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.REMARK_R, "備考小(社内)", 120, True)

        '非表示項目
        Public Shared ROW_NO As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.ROW_NO, "行番号", 100, False)
        Public Shared NRS_BR_CD_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.NRS_BR_CD_R, "営業所コード", 100, False)
        Public Shared CUST_CD_L_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.CUST_CD_L_R, "荷主コード大", 100, False)
        Public Shared WH_CD_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.WH_CD_R, "倉庫コード", 100, False)
        Public Shared CUST_CD_M_R As SpreadColProperty = New SpreadColProperty(LMD020C.SprColumnIndexMoveAfter.CUST_CD_M_R, "荷主コード中", 100, False)


    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprMoveBefor.CrearSpread()
            .sprMoveAfter.CrearSpread()

            '列数設定
            ''START ADD 2013/09/10 KOBAYASHI WIT対応
            ''START YANAI 要望番号766
            ''.sprMoveBefor.ActiveSheet.ColumnCount = 54
            ''.sprMoveBefor.ActiveSheet.ColumnCount = 55
            '.sprMoveBefor.ActiveSheet.ColumnCount = 56
            ''END YANAI 要望番号766
            ''END   ADD 2013/09/10 KOBAYASHI WIT対応
            '.sprMoveAfter.ActiveSheet.ColumnCount = 24
            ' 030509【LMS】BYK商品6桁対応
            .sprMoveBefor.ActiveSheet.ColumnCount = 57
            .sprMoveAfter.ActiveSheet.ColumnCount = 25

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprMoveBefor.SetColProperty(New LMD020G.sprMoveBefor())
            '.sprMoveAfter.SetColProperty(New LMD020G.sprMoveAfter())
            .sprMoveBefor.SetColProperty(New LMD020G.sprMoveBefor(), False)
            .sprMoveAfter.SetColProperty(New LMD020G.sprMoveAfter(), False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.ユーザー名で固定)
            'START YANAI 要望番号550
            '.sprMoveBefor.ActiveSheet.FrozenColumnCount = LMD020G.sprMoveBefor.DEF.ColNo + 1
            .sprMoveBefor.ActiveSheet.FrozenColumnCount = LMD020G.sprMoveBefor.SERIAL_NO.ColNo + 1
            'END YANAI 要望番号550

            Dim sCustM As StyleInfo = Me.StyleInfoCustCond(.sprMoveBefor)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(.sprMoveAfter, CellHorizontalAlignment.Left)

            '列設定(sprMoveBefor:移動前)
            Dim rowCount As Integer = 0
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.ALL_MIX_IME_OFF, 60, False)) '検証結果_導入時要望 №62対応(2011.09.13)
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.LOT_NO.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.ALL_MIX_IME_OFF, 40, False))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.IRIME.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.STD_IRIME_UT.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.INKO_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.SERIAL_NO.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.ALL_MIX_IME_OFF, 40, False))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.ALLOC_CAN_NB.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.PKG_UT1.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.ALCTD_NB.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.PORA_ZAI_NB.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.TOU_NO.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.HAN_NUM_ALPHA, 2, False))
            'START YANAI 要望番号705
            '.sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.SITU_NO.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.HAN_NUM_ALPHA, 1, False))
            '.sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.ZONE_CD.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.HAN_NUM_ALPHA, 1, False))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.SITU_NO.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.HAN_NUM_ALPHA, 2, False))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.ZONE_CD.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.HAN_NUM_ALPHA, 2, False))
            'END YANAI 要望番号705
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.LOCA.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.ALL_MIX_IME_OFF, 10, False))
            'START YANAI 要望番号886
            '.sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.GOODS_CD_CUST.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.HAN_NUM_ALPHA, 20, False))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.GOODS_CD_CUST.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.ALL_HANKAKU, 20, False))
            'END YANAI 要望番号886
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.GOODS_COND_NM_1.ColNo, LMSpreadUtility.GetComboCellKbn(.sprMoveBefor, LMKbnConst.KBN_S005, False))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.GOODS_COND_NM_2.ColNo, LMSpreadUtility.GetComboCellKbn(.sprMoveBefor, LMKbnConst.KBN_S006, False))

            '2017.09.08 状態荷主を検索条件に含める対応START
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.GOODS_COND_NM_3.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.ALL_MIX_IME_OFF, 20, False))
            '.sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.GOODS_COND_NM_3.ColNo, sLabel)
            '2017.09.08 状態荷主を検索条件に含める対応END

            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.SPD_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprMoveBefor, LMKbnConst.KBN_H003, False))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.OFB_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprMoveBefor, LMKbnConst.KBN_B002, False))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.KEEP_GOODS_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprMoveBefor, LMD020C.KbnConst.BYK_KEEP_GOODS_CD, False))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.LT_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.GOODS_CRT_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            'START YANAI 要望番号1065 荷主カテゴリのバイト変更
            '.sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.SEARCH_KEY_2.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.ALL_MIX, 20, False))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.SEARCH_KEY_2.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.ALL_MIX, 25, False))
            'END YANAI 要望番号1065 荷主カテゴリのバイト変更
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.ALLOC_PRIORITY_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprMoveBefor, LMKbnConst.KBN_W001, False))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.DEST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.ALL_MIX, 80, False))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.RSV_NO.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.HAN_NUM_ALPHA, 10, False))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.REMARK_OUT.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.ALL_MIX, 15, False))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.ZAI_REC_NO.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.HAN_NUM_ALPHA, 10, False))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.CUST_NM_L.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.CUST_NM_M.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            'START YANAI 要望番号766
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.CUST_CD_S.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.HAN_NUM_ALPHA, 2, False))
            'END YANAI 要望番号766
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.REMARK.ColNo, LMSpreadUtility.GetTextCell(.sprMoveBefor, InputControl.ALL_MIX, 100, False))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.ALCTD_QT.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.ALLOC_CAN_QT.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.PORA_ZAI_QT.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.PKG_NB.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.PKG_UT2.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.HOKAN_NIYAKU_CALCULATION.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.OUTKO_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.GOODS_CD_NRS.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.CUST_CD_L.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.CUST_CD_M.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.INKA_NO_L.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.INKA_NO_M.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.INKA_NO_S.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.HOKAN_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.TAX_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.ZERO_FLAG.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.SMPL_FLAG.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.SYS_UPD_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.SYS_UPD_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.NRS_BR_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.WH_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            'START ADD 2013/09/10 KOBAYASHI WIT対応
            .sprMoveBefor.SetCellStyle(rowCount, sprMoveBefor.GOODS_KANRI_NO.ColNo, LMSpreadUtility.GetLabelCell(.sprMoveBefor, CellHorizontalAlignment.Left))
            'END   ADD 2013/09/10 KOBAYASHI WIT対応

            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.DEF_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.TOU_NO_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.SITU_NO_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.ZONE_CD_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.LOCA_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.IDO_KOSU_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.GOODS_COND_KB_1_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.GOODS_COND_KB_2_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.GOODS_COND_KB_3_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.SPD_KB_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.OFB_KB_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.BYK_KEEP_GOODS_CD_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.LT_DATE_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.GOODS_CRT_DATE_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.ALLOC_PRIORITY_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.DEST_CD_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.DEST_NM_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.RSV_NO_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.REMARK_OUT_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.REMARK_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.ROW_NO.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.NRS_BR_CD_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.CUST_CD_L_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.WH_CD_R.ColNo, sLabel)
            .sprMoveAfter.SetCellStyle(rowCount, LMD020G.sprMoveAfter.CUST_CD_M_R.ColNo, sLabel)

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMD020F)

        With frm.sprMoveBefor

            Dim rowCount As Integer = 0

            .SetCellValue(rowCount, LMD020G.sprMoveBefor.GOODS_NM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.LOT_NO.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.IRIME.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.STD_IRIME_UT.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.INKO_DATE.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.SERIAL_NO.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.ALLOC_CAN_NB.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.PKG_UT1.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.ALCTD_NB.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.PORA_ZAI_NB.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.TOU_NO.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.SITU_NO.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.ZONE_CD.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.LOCA.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.GOODS_CD_CUST.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.GOODS_COND_NM_1.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.GOODS_COND_NM_2.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.GOODS_COND_NM_3.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.SPD_KB_NM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.OFB_KB_NM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.KEEP_GOODS_NM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.LT_DATE.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.GOODS_CRT_DATE.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.SEARCH_KEY_2.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.ALLOC_PRIORITY_NM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.DEST_NM.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.RSV_NO.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.REMARK_OUT.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.ZAI_REC_NO.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.CUST_NM_L.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.CUST_NM_M.ColNo, String.Empty)
            'START YANAI 要望番号766
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.CUST_CD_S.ColNo, String.Empty)
            'END YANAI 要望番号766
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.REMARK.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.ALCTD_QT.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.ALLOC_CAN_QT.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.PORA_ZAI_QT.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.PKG_NB.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.PKG_UT2.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.HOKAN_NIYAKU_CALCULATION.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.OUTKO_DATE.ColNo, String.Empty)
            .SetCellValue(rowCount, LMD020G.sprMoveBefor.GOODS_CD_NRS.ColNo, String.Empty)

        End With

    End Sub

    ''' <summary>
    ''' 移動元スプレッドセット
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Friend Sub SetSpreadBefore(ByVal dt As DataTable)
        Dim sprBefor As LMSpreadSearch = Me._Frm.sprMoveBefor

        With sprBefor

            .SuspendLayout()

            'データ挿入
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            Dim defLock As Boolean = False

            '自営業所判断
            If Me._Frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
                defLock = True
            End If

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(sprBefor, defLock)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(sprBefor, CellHorizontalAlignment.Left)
            Dim number9 As StyleInfo = LMSpreadUtility.GetNumberCell(sprBefor, 0, 9999999999, True, 0, , ",")
            Dim number9to3 As StyleInfo = LMSpreadUtility.GetNumberCell(sprBefor, 0, 9999999999.999, True, 3, , ",")
            Dim number10 As StyleInfo = LMSpreadUtility.GetNumberCell(sprBefor, 0, 99999999999, True, 0, , ",")

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            Dim isVisibleKeepGoods As Boolean = True
            For i As Integer = 1 To lngcnt
                dr = dt.Rows(i - 1)
                If dr.Item("IS_BYK_KEEP_GOODS_CD").ToString() = "0" Then
                    ' 荷主(中) が複数ヒットした場合を想定し、1件でも BYKキープ品管理 を行わない場合はキープ品の列を表示しない。
                    isVisibleKeepGoods = False
                    If Me._LMDConG.GetCellValue(.ActiveSheet.Cells(0, LMD020G.sprMoveBefor.KEEP_GOODS_NM.ColNo)).TrimEnd() <> "" Then
                        .SetCellValue(0, LMD020G.sprMoveBefor.KEEP_GOODS_NM.ColNo, "")
                    End If
                End If
            Next
            .ActiveSheet.Columns(LMD020G.sprMoveBefor.KEEP_GOODS_NM.ColNo).Visible = isVisibleKeepGoods

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイルを設定
                .SetCellStyle(i, LMD020G.sprMoveBefor.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMD020G.sprMoveBefor.GOODS_NM.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.LOT_NO.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.IRIME.ColNo, number9to3)
                .SetCellStyle(i, LMD020G.sprMoveBefor.STD_IRIME_UT.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.INKO_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.SERIAL_NO.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.ALLOC_CAN_NB.ColNo, number10)
                .SetCellStyle(i, LMD020G.sprMoveBefor.PKG_UT1.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.ALCTD_NB.ColNo, number10)
                .SetCellStyle(i, LMD020G.sprMoveBefor.PORA_ZAI_NB.ColNo, number10)
                .SetCellStyle(i, LMD020G.sprMoveBefor.TOU_NO.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.SITU_NO.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.ZONE_CD.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.LOCA.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.GOODS_CD_CUST.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.GOODS_COND_NM_1.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.GOODS_COND_NM_2.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.GOODS_COND_NM_3.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.SPD_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.OFB_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.KEEP_GOODS_NM.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.LT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.GOODS_CRT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.SEARCH_KEY_2.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.ALLOC_PRIORITY_NM.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.DEST_NM.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.RSV_NO.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.REMARK_OUT.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.ZAI_REC_NO.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.CUST_NM_L.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.CUST_NM_M.ColNo, sLabel)
                'START YANAI 要望番号766
                .SetCellStyle(i, LMD020G.sprMoveBefor.CUST_CD_S.ColNo, sLabel)
                'END YANAI 要望番号766
                .SetCellStyle(i, LMD020G.sprMoveBefor.REMARK.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.ALCTD_QT.ColNo, number9to3)
                .SetCellStyle(i, LMD020G.sprMoveBefor.ALLOC_CAN_QT.ColNo, number9to3)
                .SetCellStyle(i, LMD020G.sprMoveBefor.PORA_ZAI_QT.ColNo, number9to3)
                .SetCellStyle(i, LMD020G.sprMoveBefor.PKG_NB.ColNo, number9)
                .SetCellStyle(i, LMD020G.sprMoveBefor.PKG_UT2.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.HOKAN_NIYAKU_CALCULATION.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.OUTKO_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.GOODS_CD_NRS.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.INKA_NO_L.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.INKA_NO_M.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.INKA_NO_S.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.HOKAN_YN.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.TAX_KB.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.ZERO_FLAG.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.SMPL_FLAG.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.ROW_NO.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMD020G.sprMoveBefor.WH_CD.ColNo, sLabel)
                'START ADD 2013/09/10 KOBAYASHI WIT対応
                .SetCellStyle(i, LMD020G.sprMoveBefor.GOODS_KANRI_NO.ColNo, sLabel)
                'END   ADD 2013/09/10 KOBAYASHI WIT対応

                'セルに値を設定
                .SetCellValue(i, LMD020G.sprMoveBefor.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMD020G.sprMoveBefor.GOODS_NM.ColNo, dr.Item("GOODS_NM").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.LOT_NO.ColNo, dr.Item("LOT_NO").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.IRIME.ColNo, dr.Item("IRIME").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.STD_IRIME_UT.ColNo, dr.Item("STD_IRIME_UT").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.INKO_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("INKO_DATE").ToString()))
                .SetCellValue(i, LMD020G.sprMoveBefor.SERIAL_NO.ColNo, dr.Item("SERIAL_NO").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.ALLOC_CAN_NB.ColNo, dr.Item("ALLOC_CAN_NB").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.PKG_UT1.ColNo, dr.Item("PKG_UT").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.ALCTD_NB.ColNo, dr.Item("ALCTD_NB").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.PORA_ZAI_NB.ColNo, dr.Item("PORA_ZAI_NB").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.TOU_NO.ColNo, dr.Item("TOU_NO").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.SITU_NO.ColNo, dr.Item("SITU_NO").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.ZONE_CD.ColNo, dr.Item("ZONE_CD").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.LOCA.ColNo, dr.Item("LOCA").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.GOODS_CD_CUST.ColNo, dr.Item("GOODS_CD_CUST").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.GOODS_COND_NM_1.ColNo, dr.Item("GOODS_COND_NM_1").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.GOODS_COND_NM_2.ColNo, dr.Item("GOODS_COND_NM_2").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.GOODS_COND_NM_3.ColNo, dr.Item("GOODS_COND_NM_3").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.SPD_KB_NM.ColNo, dr.Item("SPD_KB_NM").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.OFB_KB_NM.ColNo, dr.Item("OFB_KB_NM").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.KEEP_GOODS_NM.ColNo, dr.Item("KEEP_GOODS_NM").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.LT_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("LT_DATE").ToString()))
                .SetCellValue(i, LMD020G.sprMoveBefor.GOODS_CRT_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("GOODS_CRT_DATE").ToString()))
                .SetCellValue(i, LMD020G.sprMoveBefor.SEARCH_KEY_2.ColNo, dr.Item("SEARCH_KEY_2").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.ALLOC_PRIORITY_NM.ColNo, dr.Item("ALLOC_PRIORITY_NM").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.RSV_NO.ColNo, dr.Item("RSV_NO").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.REMARK_OUT.ColNo, dr.Item("REMARK_OUT").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.ZAI_REC_NO.ColNo, dr.Item("ZAI_REC_NO").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.CUST_NM_L.ColNo, dr.Item("CUST_NM_L").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.CUST_NM_M.ColNo, dr.Item("CUST_NM_M").ToString())
                'START YANAI 要望番号766
                .SetCellValue(i, LMD020G.sprMoveBefor.CUST_CD_S.ColNo, dr.Item("CUST_CD_S").ToString())
                'END YANAI 要望番号766
                .SetCellValue(i, LMD020G.sprMoveBefor.REMARK.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.ALCTD_QT.ColNo, dr.Item("ALCTD_QT").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.ALLOC_CAN_QT.ColNo, dr.Item("ALLOC_CAN_QT").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.PORA_ZAI_QT.ColNo, dr.Item("PORA_ZAI_QT").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.PKG_NB.ColNo, dr.Item("PKG_NB").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.PKG_UT2.ColNo, dr.Item("PKG_UT").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.HOKAN_NIYAKU_CALCULATION.ColNo, DateFormatUtility.EditSlash(dr.Item("HOKAN_NIYAKU_CALCULATION").ToString()))
                .SetCellValue(i, LMD020G.sprMoveBefor.OUTKO_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("OUTKO_DATE").ToString()))
                .SetCellValue(i, LMD020G.sprMoveBefor.GOODS_CD_NRS.ColNo, dr.Item("GOODS_CD_NRS").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.INKA_NO_L.ColNo, dr.Item("INKA_NO_L").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.INKA_NO_M.ColNo, dr.Item("INKA_NO_M").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.INKA_NO_S.ColNo, dr.Item("INKA_NO_S").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.HOKAN_YN.ColNo, dr.Item("HOKAN_YN").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.TAX_KB.ColNo, dr.Item("TAX_KB").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.ZERO_FLAG.ColNo, dr.Item("ZERO_FLAG").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.SMPL_FLAG.ColNo, dr.Item("SMPL_FLAG").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.ROW_NO.ColNo, i.ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMD020G.sprMoveBefor.WH_CD.ColNo, dr.Item("WH_CD").ToString())
                'START ADD 2013/09/10 KOBAYASHI WIT対応
                .SetCellValue(i, LMD020G.sprMoveBefor.GOODS_KANRI_NO.ColNo, dr.Item("GOODS_KANRI_NO").ToString())
                'END   ADD 2013/09/10 KOBAYASHI WIT対応
            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 移動先スプレッドセット
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Friend Sub SetSpreadAfter(ByVal dt As DataTable)

        Dim sprAfter As LMSpread = Me._Frm.sprMoveAfter

        With sprAfter

            If Me._Frm.optHeikouIdo.Checked = True Then

                .SuspendLayout()

                Dim lngcnt As Integer = dt.Rows.Count
                If lngcnt = 0 Then
                    .ResumeLayout(True)
                    Exit Sub
                End If

                '行数設定
                .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

                'セルに設定するスタイルの取得
                Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(sprAfter, True)
                Dim sNumber As StyleInfo = LMSpreadUtility.GetLabelCell(sprAfter, CellHorizontalAlignment.Right)
                Dim sCustM As StyleInfo = Me.StyleInfoCustCond(sprAfter)
                Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(sprAfter, CellHorizontalAlignment.Left)

                sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

                Dim dr As DataRow = Nothing

                .ActiveSheet.Columns(LMD020G.sprMoveAfter.BYK_KEEP_GOODS_CD_R.ColNo).Visible =
                    Me._Frm.sprMoveBefor.ActiveSheet.Columns(LMD020G.sprMoveBefor.KEEP_GOODS_NM.ColNo).Visible

                '値設定
                For i As Integer = 1 To lngcnt

                    dr = dt.Rows(i - 1)

                    'セルスタイルを設定
                    .SetCellStyle(i, LMD020G.sprMoveAfter.DEF_R.ColNo, sDEF)
                    .SetCellStyle(i, LMD020G.sprMoveAfter.TOU_NO_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.HAN_NUM_ALPHA, 2, True))
                    'START YANAI 要望番号705
                    '.SetCellStyle(i, LMD020G.sprMoveAfter.SITU_NO_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.HAN_NUM_ALPHA, 1, True))
                    '.SetCellStyle(i, LMD020G.sprMoveAfter.ZONE_CD_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.HAN_NUM_ALPHA, 1, True))
                    .SetCellStyle(i, LMD020G.sprMoveAfter.SITU_NO_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.HAN_NUM_ALPHA, 2, True))
                    .SetCellStyle(i, LMD020G.sprMoveAfter.ZONE_CD_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.HAN_NUM_ALPHA, 2, True))
                    'END YANAI 要望番号705
                    .SetCellStyle(i, LMD020G.sprMoveAfter.LOCA_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.ALL_MIX_IME_OFF, 10, True))
                    .SetCellStyle(i, LMD020G.sprMoveAfter.IDO_KOSU_R.ColNo, LMSpreadUtility.GetNumberCell(sprAfter, 0, 9999999999, True, 0, , ","))
                    .SetCellStyle(i, LMD020G.sprMoveAfter.GOODS_COND_KB_1_R.ColNo, LMSpreadUtility.GetComboCellKbn(sprAfter, LMKbnConst.KBN_S005, True))
                    .SetCellStyle(i, LMD020G.sprMoveAfter.GOODS_COND_KB_2_R.ColNo, LMSpreadUtility.GetComboCellKbn(sprAfter, LMKbnConst.KBN_S006, True))
                    .SetCellStyle(i, LMD020G.sprMoveAfter.GOODS_COND_KB_3_R.ColNo, sCustM)
                    .SetCellStyle(i, LMD020G.sprMoveAfter.SPD_KB_R.ColNo, LMSpreadUtility.GetComboCellKbn(sprAfter, LMKbnConst.KBN_H003, True))
                    .SetCellStyle(i, LMD020G.sprMoveAfter.OFB_KB_R.ColNo, LMSpreadUtility.GetComboCellKbn(sprAfter, LMKbnConst.KBN_B002, True))
                    .SetCellStyle(i, LMD020G.sprMoveAfter.BYK_KEEP_GOODS_CD_R.ColNo, LMSpreadUtility.GetComboCellKbn(sprAfter, LMD020C.KbnConst.BYK_KEEP_GOODS_CD, True))
                    .SetCellStyle(i, LMD020G.sprMoveAfter.LT_DATE_R.ColNo, LMSpreadUtility.GetDateTimeCell(sprAfter, True, CellType.DateTimeFormat.ShortDate))
                    .SetCellStyle(i, LMD020G.sprMoveAfter.GOODS_CRT_DATE_R.ColNo, LMSpreadUtility.GetDateTimeCell(sprAfter, True, CellType.DateTimeFormat.ShortDate))
                    .SetCellStyle(i, LMD020G.sprMoveAfter.ALLOC_PRIORITY_R.ColNo, LMSpreadUtility.GetComboCellKbn(sprAfter, LMKbnConst.KBN_W001, True))
                    .SetCellStyle(i, LMD020G.sprMoveAfter.DEST_CD_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.ALL_MIX_IME_OFF, 15, True))
                    .SetCellStyle(i, LMD020G.sprMoveAfter.DEST_NM_R.ColNo, sLabel)
                    .SetCellStyle(i, LMD020G.sprMoveAfter.RSV_NO_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.HAN_NUM_ALPHA, 10, True))
                    .SetCellStyle(i, LMD020G.sprMoveAfter.REMARK_OUT_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.ALL_MIX_IME_OFF, 15, True))
                    .SetCellStyle(i, LMD020G.sprMoveAfter.REMARK_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.ALL_MIX, 100, True))
                    .SetCellStyle(i, LMD020G.sprMoveAfter.ROW_NO.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.ALL_MIX, 100, True))
                    .SetCellStyle(i, LMD020G.sprMoveAfter.NRS_BR_CD_R.ColNo, sLabel)
                    .SetCellStyle(i, LMD020G.sprMoveAfter.CUST_CD_L_R.ColNo, sLabel)
                    .SetCellStyle(i, LMD020G.sprMoveAfter.WH_CD_R.ColNo, sLabel)
                    .SetCellStyle(i, LMD020G.sprMoveAfter.CUST_CD_M_R.ColNo, sLabel)

                    'セルに値を設定
                    .SetCellValue(i, LMD020G.sprMoveAfter.DEF_R.ColNo, LMConst.FLG.OFF)
                    .SetCellValue(i, LMD020G.sprMoveAfter.TOU_NO_R.ColNo, dr.Item("TOU_NO").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.SITU_NO_R.ColNo, dr.Item("SITU_NO").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.ZONE_CD_R.ColNo, dr.Item("ZONE_CD").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.LOCA_R.ColNo, dr.Item("LOCA").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.IDO_KOSU_R.ColNo, dr.Item("IDO_KOSU").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.GOODS_COND_KB_1_R.ColNo, dr.Item("GOODS_COND_KB_1").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.GOODS_COND_KB_2_R.ColNo, dr.Item("GOODS_COND_KB_2").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.GOODS_COND_KB_3_R.ColNo, dr.Item("GOODS_COND_KB_3").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.SPD_KB_R.ColNo, dr.Item("SPD_KB").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.OFB_KB_R.ColNo, dr.Item("OFB_KB").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.BYK_KEEP_GOODS_CD_R.ColNo, dr.Item("BYK_KEEP_GOODS_CD").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.LT_DATE_R.ColNo, DateFormatUtility.EditSlash(dr.Item("LT_DATE").ToString()))
                    .SetCellValue(i, LMD020G.sprMoveAfter.GOODS_CRT_DATE_R.ColNo, DateFormatUtility.EditSlash(dr.Item("GOODS_CRT_DATE").ToString()))
                    .SetCellValue(i, LMD020G.sprMoveAfter.ALLOC_PRIORITY_R.ColNo, dr.Item("ALLOC_PRIORITY").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.DEST_CD_R.ColNo, dr.Item("DEST_CD").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.DEST_NM_R.ColNo, dr.Item("DEST_NM").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.RSV_NO_R.ColNo, dr.Item("RSV_NO").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.REMARK_OUT_R.ColNo, dr.Item("REMARK_OUT").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.REMARK_R.ColNo, dr.Item("REMARK").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.ROW_NO.ColNo, i.ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.NRS_BR_CD_R.ColNo, dr.Item("NRS_BR_CD").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.CUST_CD_L_R.ColNo, dr.Item("CUST_CD_L").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.WH_CD_R.ColNo, dr.Item("WH_CD").ToString())
                    .SetCellValue(i, LMD020G.sprMoveAfter.CUST_CD_M_R.ColNo, dr.Item("CUST_CD_M").ToString())
                Next

                .ResumeLayout(True)

            End If

        End With

    End Sub

    ''' <summary>
    ''' 検索時、再描画時にスプレッドデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Call Me.SetSpreadBefore(dt)
        Call Me.SetSpreadAfter(dt)

    End Sub

    ''' <summary>
    ''' 行追加、移動元選択時にスプレッドデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadSakiRowAdd(ByVal dr As DataRow)

        Dim sprAfter As LMSpread = Me._Frm.sprMoveAfter

        With sprAfter

            .SuspendLayout()
            'データ挿入
            '行数設定

            Dim rowCnt As Integer = 0

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(sprAfter, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(sprAfter, CellHorizontalAlignment.Left)
            Dim sCustM As StyleInfo = Me.StyleInfoCustCond(sprAfter)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            '値設定
            For i As Integer = 1 To 1

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                If rowCnt = 1 Then
                    .ActiveSheet.Columns(LMD020G.sprMoveAfter.BYK_KEEP_GOODS_CD_R.ColNo).Visible =
                        Me._Frm.sprMoveBefor.ActiveSheet.Columns(LMD020G.sprMoveBefor.KEEP_GOODS_NM.ColNo).Visible
                End If

                Dim chk As Boolean = Me.chkBogaiDr(dr)

                'セルスタイルを設定
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.DEF_R.ColNo, sDEF)
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.TOU_NO_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.HAN_NUM_ALPHA, 2, False))
                'START YANAI 要望番号705
                '.SetCellStyle(rowCnt, LMD020G.sprMoveAfter.SITU_NO_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.HAN_NUM_ALPHA, 1, False))
                '.SetCellStyle(rowCnt, LMD020G.sprMoveAfter.ZONE_CD_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.HAN_NUM_ALPHA, 1, False))
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.SITU_NO_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.HAN_NUM_ALPHA, 2, False))
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.ZONE_CD_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.HAN_NUM_ALPHA, 2, False))
                'END YANAI 要望番号705
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.LOCA_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.ALL_MIX_IME_OFF, 10, False))
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.IDO_KOSU_R.ColNo, LMSpreadUtility.GetNumberCell(sprAfter, 0, 9999999999, False, 0, , ","))
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.GOODS_COND_KB_1_R.ColNo, LMSpreadUtility.GetComboCellKbn(sprAfter, LMKbnConst.KBN_S005, False))
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.GOODS_COND_KB_2_R.ColNo, LMSpreadUtility.GetComboCellKbn(sprAfter, LMKbnConst.KBN_S006, False))
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.GOODS_COND_KB_3_R.ColNo, sCustM)
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.SPD_KB_R.ColNo, LMSpreadUtility.GetComboCellKbn(sprAfter, LMKbnConst.KBN_H003, False))
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.OFB_KB_R.ColNo, LMSpreadUtility.GetComboCellKbn(sprAfter, LMKbnConst.KBN_B002, chk))
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.BYK_KEEP_GOODS_CD_R.ColNo, LMSpreadUtility.GetComboCellKbn(sprAfter, LMD020C.KbnConst.BYK_KEEP_GOODS_CD, False))
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.LT_DATE_R.ColNo, LMSpreadUtility.GetDateTimeCell(sprAfter, False, CellType.DateTimeFormat.ShortDate))
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.GOODS_CRT_DATE_R.ColNo, LMSpreadUtility.GetDateTimeCell(sprAfter, False, CellType.DateTimeFormat.ShortDate))
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.ALLOC_PRIORITY_R.ColNo, LMSpreadUtility.GetComboCellKbn(sprAfter, LMKbnConst.KBN_W001, False))
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.DEST_CD_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.ALL_MIX_IME_OFF, 15, False))
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.DEST_NM_R.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.RSV_NO_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.HAN_NUM_ALPHA, 10, False))
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.REMARK_OUT_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.ALL_MIX_IME_OFF, 15, False))
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.REMARK_R.ColNo, LMSpreadUtility.GetTextCell(sprAfter, InputControl.ALL_MIX, 100, False))
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.ROW_NO.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.NRS_BR_CD_R.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.CUST_CD_L_R.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMD020G.sprMoveAfter.WH_CD_R.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.DEF_R.ColNo, LMConst.FLG.OFF)
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.TOU_NO_R.ColNo, dr.Item("TOU_NO").ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.SITU_NO_R.ColNo, dr.Item("SITU_NO").ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.ZONE_CD_R.ColNo, dr.Item("ZONE_CD").ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.LOCA_R.ColNo, dr.Item("LOCA").ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.IDO_KOSU_R.ColNo, LMConst.FLG.OFF)
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.GOODS_COND_KB_1_R.ColNo, dr.Item("GOODS_COND_KB_1").ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.GOODS_COND_KB_2_R.ColNo, dr.Item("GOODS_COND_KB_2").ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.GOODS_COND_KB_3_R.ColNo, dr.Item("GOODS_COND_KB_3").ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.SPD_KB_R.ColNo, dr.Item("SPD_KB").ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.OFB_KB_R.ColNo, dr.Item("OFB_KB").ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.BYK_KEEP_GOODS_CD_R.ColNo, dr.Item("BYK_KEEP_GOODS_CD").ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.LT_DATE_R.ColNo, DateFormatUtility.EditSlash(dr.Item("LT_DATE").ToString()))
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.GOODS_CRT_DATE_R.ColNo, DateFormatUtility.EditSlash(dr.Item("GOODS_CRT_DATE").ToString()))
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.ALLOC_PRIORITY_R.ColNo, dr.Item("ALLOC_PRIORITY").ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.DEST_CD_R.ColNo, dr.Item("DEST_CD").ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.DEST_NM_R.ColNo, dr.Item("DEST_NM").ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.RSV_NO_R.ColNo, dr.Item("RSV_NO").ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.REMARK_OUT_R.ColNo, dr.Item("REMARK_OUT").ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.REMARK_R.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.ROW_NO.ColNo, i.ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.NRS_BR_CD_R.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.CUST_CD_L_R.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(rowCnt, LMD020G.sprMoveAfter.WH_CD_R.ColNo, dr.Item("WH_CD").ToString())
            Next

            .ResumeLayout(True)

        End With


    End Sub

    ''' <summary>
    ''' セルのプロパティを設定(CUSTCOND)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Friend Function StyleInfoCustCond(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetComboCellMaster(spr _
                                                  , LMConst.CacheTBL.CUSTCOND _
                                                  , "JOTAI_CD" _
                                                  , "JOTAI_NM" _
                                                  , False _
                                                  , New String() {"NRS_BR_CD", "CUST_CD_L"} _
                                                  , New String() {Me._Frm.cmbNrsBrCd.SelectedValue.ToString(), Me._Frm.txtCustCdL.TextValue} _
                                                  , LMConst.JoinCondition.AND_WORD
                                                  )

    End Function

    ''' <summary>
    ''' 移動先の簿外品の使用可否をチェック(drから)
    ''' </summary>
    ''' <param name="dr">データロウ</param>
    ''' <returns>使用可否</returns>
    ''' <remarks></remarks>
    Friend Function chkBogaiDr(ByVal dr As DataRow) As Boolean

        Dim custCdL As String = dr.Item("CUST_CD_L").ToString()
        Dim custCdM As String = dr.Item("CUST_CD_M").ToString()

        Dim drCust As DataRow() = Nothing

        '荷主マスタからデータロウを取得する
        drCust = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("CUST_CD_L = ", " '", custCdL, "' ", " AND ", "CUST_CD_M = ", " '", custCdM, "'", " AND CUST_CD_S = '00' AND CUST_CD_SS = '00' "))

        If "01".Equals(drCust(0).Item("SOKO_CHANGE_KB").ToString()) = True Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' 移動先の簿外品の使用可否をチェック(スプレッドから)
    ''' </summary>
    ''' <returns>True:使用可、False:使用不可</returns>
    ''' <remarks></remarks>
    Friend Function chkBogaiSpr(ByVal custCdL As String, ByVal custCdM As String) As Boolean

        Dim drCust As DataRow() = Nothing

        '荷主マスタからデータロウを取得する
        drCust = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("CUST_CD_L = ", " '", custCdL, "' ", " AND ", "CUST_CD_M = ", " '", custCdM, "'", " AND CUST_CD_S = '00' AND CUST_CD_SS = '00' "))

        If "01".Equals(drCust(0).Item("SOKO_CHANGE_KB").ToString()) = True Then
            Return False
        Else
            Return True
        End If

    End Function

#End Region 'Spread

#Region "部品化検討中"

    ''' <summary>
    ''' 画面編集部ロック処理を行う
    ''' </summary>
    ''' <param name="lock">trueはロック処理</param>
    ''' <remarks></remarks>
    Friend Sub LockControl(ByVal lock As Boolean)

        With Me._Frm

            Me.SetLockControl(.cmbNrsBrCd, lock)
            Me.SetLockControl(.cmbSoko, lock)
            Me.SetLockControl(.txtCustCdL, lock)
            Me.SetLockControl(.txtCustCdM, lock)
            Me.SetLockControl(.lblCustNM, lock)
            Me.SetLockControl(.imdNyukaFrom, lock)
            Me.SetLockControl(.imdNyukaTo, lock)
            Me.SetLockControl(.optAll, lock)
            Me.SetLockControl(.optDefectiveProduct, lock)
            Me.SetLockControl(.cmbJiyuran, lock)
            Me.SetLockControl(.txtJiyuran, lock)
            Me.SetLockControl(.imdIdoubi, lock)
            Me.SetLockControl(.optHeikouIdo, lock)
            Me.SetLockControl(.optFukusuIdo, lock)
            If LMD020C.NRS_BR_CD_TOKE.Equals(.cmbNrsBrCd.SelectedValue.ToString) Then
                Me.SetLockControl(.optKyoseiShuko, lock)
            Else
                '土気以外は常に使用不可
                Me.SetLockControl(.optKyoseiShuko, True)
            End If
            Me.SetLockControl(.txtTouNo, lock)
            Me.SetLockControl(.txtSituNo, lock)
            Me.SetLockControl(.txtZoneCd, lock)
            Me.SetLockControl(.txtLocation, lock)
            Me.SetLockControl(.cmbGoodsCondKb1, lock)
            Me.SetLockControl(.cmbGoodsCondKb2, lock)
            Me.SetLockControl(.cmbGoodsCondKb3, lock)
            Me.SetLockControl(.cmbSpdKb, lock)
            Me.SetLockControl(.cmbOfbKb, lock)
            Me.SetLockControl(.imdLtDate, lock)
            Me.SetLockControl(.imdGoodsCrtDate, lock)
            Me.SetLockControl(.cmdAllocPriority, lock)
            Me.SetLockControl(.txtDestCd, lock)
            Me.SetLockControl(.txtRsvNo, lock)
            Me.SetLockControl(.txtRemarkOut, lock)
            Me.SetLockControl(.txtRemark, lock)

        End With

    End Sub

    ''' <summary>
    ''' ファンクションキーロック処理を行う
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub LockFunctionKey()

        Me.SetLockControl(Me._Frm.FunctionKey, True)

    End Sub

    ''' <summary>
    ''' ロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="ctl">制御対象項目</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Private Sub SetLockControl(ByVal ctl As Control, Optional ByVal lockFlg As Boolean = False)

        Dim arr As ArrayList = New ArrayList()
        Call Me.GetTarget(Of Nrs.Win.GUI.Win.Interface.IEditableControl)(arr, ctl)
        Dim lblArr As ArrayList = New ArrayList()

        'エディット系コントロールのロック
        For Each arrCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In arr

            'テキストボックスの場合、ラベル項目であったら処理対象外とする
            If TypeOf arrCtl Is Win.InputMan.LMImTextBox = True Then

                If DirectCast(arrCtl, Win.InputMan.LMImTextBox).Name.Substring(0, 3).Equals("lbl") = True Then
                    lblArr.Add(arrCtl)
                End If

            End If

            'ロック処理/ロック解除処理を行う
            arrCtl.ReadOnlyStatus = lockFlg
        Next

        'ラベル項目をロック
        For Each lblCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In lblArr
            lblCtl.ReadOnlyStatus = True
        Next

        'ボタンのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMButton)(arr, ctl)
        For Each arrCtl As Win.LMButton In arr
            'ロック処理/ロック解除処理を行う
            Call Me.LockButton(arrCtl, lockFlg)
        Next

        'チェックボックスのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMCheckBox)(arr, ctl)
        For Each arrCtl As Win.LMCheckBox In arr

            'ロック処理/ロック解除処理を行う
            Call Me.LockCheckBox(arrCtl, lockFlg)

        Next

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(ボタン)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockButton(ByVal ctl As Win.LMButton, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.Enabled = enabledFlg

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(チェックボックス)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockCheckBox(ByVal ctl As Win.LMCheckBox, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.EnableStatus = enabledFlg

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(ラジオボタン)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockOptionButton(ByVal ctl As Win.LMOptionButton, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.EnableStatus = enabledFlg

    End Sub

    ''' <summary>
    ''' 画面上のコントロールから指定した型のクラスかその継承クラスを取得する
    ''' </summary>
    ''' <param name="arr">取得したコントロールを格納するArrayList</param>
    ''' <param name="ownControl">コントロール取得元となるコントロール</param>
    ''' <remarks></remarks>
    Private Sub GetTarget(Of T)(ByVal arr As ArrayList, ByVal ownControl As Control)

        If TypeOf ownControl Is T _
            OrElse ownControl.GetType.IsSubclassOf(GetType(T)) Then

            '指定されたクラスかその継承クラス
            arr.Add(ownControl)

        End If

        If 0 < ownControl.Controls.Count Then
            For Each targetControl As Control In ownControl.Controls
                Call Me.GetTarget(Of T)(arr, targetControl)
            Next
        End If

    End Sub

#End Region

#End Region

End Class
