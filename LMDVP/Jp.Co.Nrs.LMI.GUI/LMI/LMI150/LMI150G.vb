' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主
'  プログラムID     :  LMI150G : 物産アニマルヘルス倉庫内処理編集
'  作  成  者       :  [HORI]
' ==========================================================================

Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports GrapeCity.Win.Editors.Fields

''' <summary>
''' LMI150Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI150G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI150F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMIControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI150F, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMIConG = g

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey(ByVal eventShubetsu As LMI150C.EventShubetsu)

        Dim always As Boolean = True
        Dim edit As Boolean = False
        Dim view As Boolean = False
        Dim lock As Boolean = False

        Dim disMode As String = Me._Frm.lblSituation.DispMode
        Dim recStatus As String = Me._Frm.lblSituation.RecordStatus

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = LMI150C.EVENTNAME_HENSHU
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = String.Empty
            .F11ButtonName = LMI150C.EVENTNAME_HOZON
            .F12ButtonName = LMI150C.EVENTNAME_CLOSE

            'ファンクションキーの制御
            If (eventShubetsu).Equals(LMI150C.EventShubetsu.SHOKI) = True Then
                '初期表示
                .F1ButtonEnabled = lock
                .F2ButtonEnabled = lock
                .F3ButtonEnabled = lock
                .F4ButtonEnabled = lock
                .F5ButtonEnabled = lock
                .F6ButtonEnabled = lock
                .F7ButtonEnabled = lock
                .F8ButtonEnabled = lock
                .F9ButtonEnabled = lock
                .F10ButtonEnabled = lock
                .F11ButtonEnabled = always
                .F12ButtonEnabled = always
                Me._Frm.btnZaikoSel.Enabled = lock

            ElseIf (eventShubetsu).Equals(LMI150C.EventShubetsu.HENSHU) = True Then
                '編集
                .F1ButtonEnabled = lock
                .F2ButtonEnabled = lock
                .F3ButtonEnabled = lock
                .F4ButtonEnabled = lock
                .F5ButtonEnabled = lock
                .F6ButtonEnabled = lock
                .F7ButtonEnabled = lock
                .F8ButtonEnabled = lock
                .F9ButtonEnabled = lock
                .F10ButtonEnabled = lock
                .F11ButtonEnabled = always
                .F12ButtonEnabled = always
                Me._Frm.btnZaikoSel.Enabled = always

            ElseIf (eventShubetsu).Equals(LMI150C.EventShubetsu.VIEW_ONLY) = True Then
                '参照のみ
                .F1ButtonEnabled = lock
                .F2ButtonEnabled = lock
                .F3ButtonEnabled = lock
                .F4ButtonEnabled = lock
                .F5ButtonEnabled = lock
                .F6ButtonEnabled = lock
                .F7ButtonEnabled = lock
                .F8ButtonEnabled = lock
                .F9ButtonEnabled = lock
                .F10ButtonEnabled = lock
                .F11ButtonEnabled = lock
                .F12ButtonEnabled = always
                Me._Frm.btnZaikoSel.Enabled = lock

            ElseIf (eventShubetsu).Equals(LMI150C.EventShubetsu.VIEW) = True Then
                '参照
                .F1ButtonEnabled = lock
                .F2ButtonEnabled = always
                .F3ButtonEnabled = lock
                .F4ButtonEnabled = lock
                .F5ButtonEnabled = lock
                .F6ButtonEnabled = lock
                .F7ButtonEnabled = lock
                .F8ButtonEnabled = lock
                .F9ButtonEnabled = lock
                .F10ButtonEnabled = lock
                .F11ButtonEnabled = lock
                .F12ButtonEnabled = always
                Me._Frm.btnZaikoSel.Enabled = lock

            ElseIf (eventShubetsu).Equals(LMI150C.EventShubetsu.HOZON) = True Then
                '保存
                .F1ButtonEnabled = lock
                .F2ButtonEnabled = lock
                .F3ButtonEnabled = lock
                .F4ButtonEnabled = lock
                .F5ButtonEnabled = lock
                .F6ButtonEnabled = lock
                .F7ButtonEnabled = lock
                .F8ButtonEnabled = lock
                .F9ButtonEnabled = lock
                .F10ButtonEnabled = lock
                .F11ButtonEnabled = always
                .F12ButtonEnabled = always
                Me._Frm.btnZaikoSel.Enabled = always

            ElseIf (eventShubetsu).Equals(LMI150C.EventShubetsu.DOUBLECLICK) = True Then  '選択
                .F1ButtonEnabled = lock
                .F2ButtonEnabled = always
                .F3ButtonEnabled = lock
                .F4ButtonEnabled = lock
                .F5ButtonEnabled = lock
                .F6ButtonEnabled = lock
                .F7ButtonEnabled = lock
                .F8ButtonEnabled = lock
                .F9ButtonEnabled = lock
                .F10ButtonEnabled = lock
                .F11ButtonEnabled = lock
                .F12ButtonEnabled = always
                Me._Frm.btnZaikoSel.Enabled = lock
            End If

        End With

    End Sub

#End Region 'FunctionKey

#Region "設定・制御"

    ''' <summary>
    ''' シチュエーションラベルの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSituation(ByVal dispMode As String, ByVal recordStatus As String)

        '編集部の項目をクリア
        With Me._frm
            .lblSituation.DispMode = dispMode
            .lblSituation.RecordStatus = recordStatus
        End With

    End Sub

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .txtNrsProcNo.TabIndex = LMI150C.CtlTabIndex.NRS_PROC_NO
            .cmbEigyo.TabIndex = LMI150C.CtlTabIndex.NRS_BR_CD
            .cmbJissekiFuyo.TabIndex = LMI150C.CtlTabIndex.JISSEKI_FUYO
            .cmbJissekiShoriFlg.TabIndex = LMI150C.CtlTabIndex.JISSEKI_SHORI_FLG
            .cmbProcType.TabIndex = LMI150C.CtlTabIndex.PROC_TYPE
            .cmbProcKbn.TabIndex = LMI150C.CtlTabIndex.PROC_KBN
            .imdProcDate.TabIndex = LMI150C.CtlTabIndex.PROC_DATE
            .grpWh.TabIndex = LMI150C.CtlTabIndex.GRP_WH
            .cmbOutkaWhType.TabIndex = LMI150C.CtlTabIndex.OUTKA_WH_TYPE
            .txtOutkaCustNo.TabIndex = LMI150C.CtlTabIndex.OUTKA_CUST_CD
            .txtOutkaCustNm.TabIndex = LMI150C.CtlTabIndex.OUTKA_CUST_NM
            .cmbInkaWhType.TabIndex = LMI150C.CtlTabIndex.INKA_WH_TYPE
            .txtInkaCustNo.TabIndex = LMI150C.CtlTabIndex.INKA_CUST_CD
            .txtInkaCustNm.TabIndex = LMI150C.CtlTabIndex.INKA_CUST_NM
            .grpGoodsRank.TabIndex = LMI150C.CtlTabIndex.GRP_GOODS_RANK
            .cmbBeforeGoodsRank.TabIndex = LMI150C.CtlTabIndex.BEFORE_GOODS_RANK
            .cmbAfterGoodsRank.TabIndex = LMI150C.CtlTabIndex.AFTER_GOODS_RANK
            .btnZaikoSel.TabIndex = LMI150C.CtlTabIndex.BTN_ZAIKO_SEL
            .txtGoodsCd.TabIndex = LMI150C.CtlTabIndex.GOODS_CD
            .txtGoodsCdNrs.TabIndex = LMI150C.CtlTabIndex.GOODS_CD_NRS
            .txtGoodsNm.TabIndex = LMI150C.CtlTabIndex.GOODS_NM
            .txtLotNo.TabIndex = LMI150C.CtlTabIndex.LOT_NO
            .numNb.TabIndex = LMI150C.CtlTabIndex.NB
            .imdLtDate.TabIndex = LMI150C.CtlTabIndex.LT_DATE
            .txtRemark.TabIndex = LMI150C.CtlTabIndex.REMARK
            .grpObic.TabIndex = LMI150C.CtlTabIndex.GRP_OBIC
            .txtObicShubetu.TabIndex = LMI150C.CtlTabIndex.OBIC_SHUBETU
            .txtObicTorihikiKbn.TabIndex = LMI150C.CtlTabIndex.OBIC_TORIHIKI_KBN
            .txtObicDenpNo.TabIndex = LMI150C.CtlTabIndex.OBIC_DENP_NO
            .txtObicGyoNo.TabIndex = LMI150C.CtlTabIndex.OBIC_GYO_NO
            .txtObicDetailNo.TabIndex = LMI150C.CtlTabIndex.OBIC_DETAIL_NO

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal sysDate As String)

        '自営業所の設定
        Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
        Me._Frm.cmbEigyo.SelectedValue = brCd

    End Sub

    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetNumberControl()

        With Me._Frm
            Dim d9 As Decimal = Convert.ToDecimal("999999999")
            Dim sharp9 As String = "###,###,##0"

            .numNb.SetInputFields(sharp9, , 9, 1, , 0, 0, , d9, Convert.ToDecimal(-99999999))

        End With

    End Sub

    ''' <summary>
    ''' コンボボックスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CreateComboBox()

        With Me._Frm

            Dim nrsBrCd = LMUserInfoManager.GetNrsBrCd()

            Me._LMIConG.CreateComboBox(.cmbOutkaWhType, LMConst.CacheTBL.KBN, New String() {"KBN_NM1"}, New String() {"REM"}, " KBN_GROUP_CD = 'B047'", "KBN_CD")
            Me._LMIConG.CreateComboBox(.cmbInkaWhType, LMConst.CacheTBL.KBN, New String() {"KBN_NM1"}, New String() {"REM"}, " KBN_GROUP_CD = 'B047'", "KBN_CD")
            Me._LMIConG.CreateComboBox(.cmbBeforeGoodsRank, LMConst.CacheTBL.CUSTCOND, New String() {"JOTAI_CD"}, New String() {"JOTAI_NM"}, String.Concat(" NRS_BR_CD = '", nrsBrCd, "' AND CUST_CD_L = '00294'"), "JOTAI_CD")
            Me._LMIConG.CreateComboBox(.cmbAfterGoodsRank, LMConst.CacheTBL.CUSTCOND, New String() {"JOTAI_CD"}, New String() {"JOTAI_NM"}, String.Concat(" NRS_BR_CD = '", nrsBrCd, "' AND CUST_CD_L = '00294'"), "JOTAI_CD")

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        Dim disMode As String = Me._Frm.lblSituation.DispMode
        Dim recStatus As String = Me._Frm.lblSituation.RecordStatus

        With Me._Frm

            If (RecordStatus.NEW_REC).Equals(recStatus) Then
                '新規モード
                .txtNrsProcNo.ReadOnly = True
                .cmbEigyo.ReadOnly = True
                .cmbJissekiFuyo.ReadOnly = True
                .cmbJissekiShoriFlg.ReadOnly = True
                .cmbProcType.ReadOnly = True
                .cmbProcKbn.ReadOnly = True
                .imdProcDate.ReadOnly = False
                .cmbOutkaWhType.ReadOnly = False
                .txtOutkaCustNo.ReadOnly = True
                .txtOutkaCustNm.ReadOnly = True
                .cmbInkaWhType.ReadOnly = True
                .txtInkaCustNo.ReadOnly = True
                .txtInkaCustNm.ReadOnly = True
                .cmbBeforeGoodsRank.ReadOnly = True
                .cmbAfterGoodsRank.ReadOnly = False
                .btnZaikoSel.Enabled = True
                .txtGoodsCd.ReadOnly = True
                .txtGoodsCdNrs.ReadOnly = True
                .txtGoodsNm.ReadOnly = True
                .txtLotNo.ReadOnly = True
                .numNb.ReadOnly = False
                .imdLtDate.ReadOnly = True
                .txtRemark.ReadOnly = True
                .txtObicShubetu.ReadOnly = True
                .txtObicTorihikiKbn.ReadOnly = True
                .txtObicDenpNo.ReadOnly = True
                .txtObicGyoNo.ReadOnly = True
                .txtObicDetailNo.ReadOnly = True

            ElseIf (RecordStatus.NOMAL_REC).Equals(recStatus) Then
                '編集モード
                .txtNrsProcNo.ReadOnly = True
                .cmbEigyo.ReadOnly = True
                .cmbJissekiFuyo.ReadOnly = True
                .cmbJissekiShoriFlg.ReadOnly = True
                .cmbProcType.ReadOnly = True
                .cmbProcKbn.ReadOnly = True
                .imdProcDate.ReadOnly = False
                .cmbOutkaWhType.ReadOnly = False
                .txtOutkaCustNo.ReadOnly = True
                .txtOutkaCustNm.ReadOnly = True
                .cmbInkaWhType.ReadOnly = True
                .txtInkaCustNo.ReadOnly = True
                .txtInkaCustNm.ReadOnly = True
                .cmbBeforeGoodsRank.ReadOnly = True
                .cmbAfterGoodsRank.ReadOnly = False
                .btnZaikoSel.Enabled = True
                .txtGoodsCd.ReadOnly = True
                .txtGoodsCdNrs.ReadOnly = True
                .txtGoodsNm.ReadOnly = True
                .txtLotNo.ReadOnly = True
                .numNb.ReadOnly = False
                .imdLtDate.ReadOnly = True
                .txtRemark.ReadOnly = True
                .txtObicShubetu.ReadOnly = True
                .txtObicTorihikiKbn.ReadOnly = True
                .txtObicDenpNo.ReadOnly = True
                .txtObicGyoNo.ReadOnly = True
                .txtObicDetailNo.ReadOnly = True
            End If

            If (DispMode.VIEW).Equals(disMode) OrElse (DispMode.INIT).Equals(disMode) Then
                '参照モード
                .txtNrsProcNo.ReadOnly = True
                .cmbEigyo.ReadOnly = True
                .cmbJissekiFuyo.ReadOnly = True
                .cmbJissekiShoriFlg.ReadOnly = True
                .cmbProcType.ReadOnly = True
                .cmbProcKbn.ReadOnly = True
                .imdProcDate.ReadOnly = True
                .cmbOutkaWhType.ReadOnly = True
                .txtOutkaCustNo.ReadOnly = True
                .txtOutkaCustNm.ReadOnly = True
                .cmbInkaWhType.ReadOnly = True
                .txtInkaCustNo.ReadOnly = True
                .txtInkaCustNm.ReadOnly = True
                .cmbBeforeGoodsRank.ReadOnly = True
                .cmbAfterGoodsRank.ReadOnly = True
                .btnZaikoSel.Enabled = False
                .txtGoodsCd.ReadOnly = True
                .txtGoodsCdNrs.ReadOnly = True
                .txtGoodsNm.ReadOnly = True
                .txtLotNo.ReadOnly = True
                .numNb.ReadOnly = True
                .imdLtDate.ReadOnly = True
                .txtRemark.ReadOnly = True
                .txtObicShubetu.ReadOnly = True
                .txtObicTorihikiKbn.ReadOnly = True
                .txtObicDenpNo.ReadOnly = True
                .txtObicGyoNo.ReadOnly = True
                .txtObicDetailNo.ReadOnly = True
            End If

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        Dim disMode As String = Me._Frm.lblSituation.DispMode
        Dim recStatus As String = Me._Frm.lblSituation.RecordStatus

        With Me._Frm

            If (RecordStatus.NEW_REC).Equals(recStatus) Then
                '新規モード
                .imdProcDate.Focus()
            Else
                '編集モード
                .cmbEigyo.Focus()
            End If

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .txtNrsProcNo.TextValue = String.Empty
            .cmbEigyo.SelectedValue = Nothing
            .cmbJissekiFuyo.SelectedValue = Nothing
            .cmbJissekiShoriFlg.SelectedValue = Nothing
            .cmbProcType.SelectedValue = Nothing
            .cmbProcKbn.SelectedValue = Nothing
            .imdProcDate.TextValue = String.Empty
            .cmbOutkaWhType.SelectedValue = Nothing
            .txtOutkaCustNo.TextValue = String.Empty
            .txtOutkaCustNm.TextValue = String.Empty
            .cmbInkaWhType.SelectedValue = Nothing
            .txtInkaCustNo.TextValue = String.Empty
            .txtInkaCustNm.TextValue = String.Empty
            .cmbBeforeGoodsRank.SelectedValue = Nothing
            .cmbAfterGoodsRank.SelectedValue = Nothing
            .txtGoodsCd.TextValue = String.Empty
            .txtGoodsCdNrs.TextValue = String.Empty
            .txtGoodsNm.TextValue = String.Empty
            .txtLotNo.TextValue = String.Empty
            .numNb.Value = 0
            .imdLtDate.TextValue = String.Empty
            .txtRemark.TextValue = String.Empty
            .txtObicShubetu.TextValue = String.Empty
            .txtObicTorihikiKbn.TextValue = String.Empty
            .txtObicDenpNo.TextValue = String.Empty
            .txtObicGyoNo.TextValue = String.Empty
            .txtObicDetailNo.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControlSinki()

        With Me._Frm

            .txtNrsProcNo.TextValue = String.Empty
            .cmbEigyo.SelectedValue = .cmbEigyo.SelectedValue
            .cmbJissekiFuyo.SelectedValue = Nothing
            .cmbJissekiShoriFlg.SelectedValue = Nothing
            .cmbProcType.SelectedValue = Nothing
            .cmbProcKbn.SelectedValue = Nothing
            .imdProcDate.TextValue = String.Empty
            .cmbOutkaWhType.SelectedValue = Nothing
            .txtOutkaCustNo.TextValue = String.Empty
            .txtOutkaCustNm.TextValue = String.Empty
            .cmbInkaWhType.SelectedValue = Nothing
            .txtInkaCustNo.TextValue = String.Empty
            .txtInkaCustNm.TextValue = String.Empty
            .cmbBeforeGoodsRank.SelectedValue = Nothing
            .cmbAfterGoodsRank.SelectedValue = Nothing
            .txtGoodsCd.TextValue = String.Empty
            .txtGoodsCdNrs.TextValue = String.Empty
            .txtGoodsNm.TextValue = String.Empty
            .txtLotNo.TextValue = String.Empty
            .numNb.Value = 0
            .imdLtDate.TextValue = String.Empty
            .txtRemark.TextValue = String.Empty
            .txtObicShubetu.TextValue = String.Empty
            .txtObicTorihikiKbn.TextValue = String.Empty
            .txtObicDenpNo.TextValue = String.Empty
            .txtObicGyoNo.TextValue = String.Empty
            .txtObicDetailNo.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetEigyo()

        With Me._Frm

            '自営業所の設定
            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            Me._Frm.cmbEigyo.SelectedValue = brCd

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal ds As DataSet)

        Dim disMode As String = Me._Frm.lblSituation.DispMode
        Dim recStatus As String = Me._Frm.lblSituation.RecordStatus

        With Me._Frm

            If (RecordStatus.NEW_REC).Equals(recStatus) Then
                '新規モード
                .cmbEigyo.SelectedValue = LMUserInfoManager.GetNrsBrCd()
                .cmbJissekiFuyo.SelectedValue = "01"        '要
                .cmbJissekiShoriFlg.SelectedValue = "0"     '実績未送信
                .cmbProcType.SelectedValue = "1"            'NRS ← 顧客
                .cmbProcKbn.SelectedValue = "1"             'ステータス変更

            ElseIf (RecordStatus.NOMAL_REC).Equals(recStatus) Then
                '編集モード
                Dim dr As DataRow = ds.Tables(LMI150C.TABLE_NM_OUT).Rows(0)

                .txtNrsProcNo.TextValue = dr("NRS_PROC_NO").ToString()
                .cmbEigyo.SelectedValue = dr("NRS_BR_CD").ToString()
                .cmbJissekiFuyo.SelectedValue = dr("JISSEKI_FUYO").ToString()
                .cmbJissekiShoriFlg.SelectedValue = dr("JISSEKI_SHORI_FLG").ToString()
                .cmbProcType.SelectedValue = dr("PROC_TYPE").ToString()
                .cmbProcKbn.SelectedValue = dr("PROC_KBN").ToString()
                .imdProcDate.TextValue = dr("PROC_DATE").ToString()
                .cmbOutkaWhType.SelectedValue = dr("OUTKA_WH_TYPE").ToString()
                .txtOutkaCustNo.TextValue = String.Concat(dr("OUTKA_CUST_CD_L").ToString(), "-", dr("OUTKA_CUST_CD_M").ToString())
                .txtOutkaCustNm.TextValue = String.Concat(dr("OUTKA_CUST_NM_L").ToString(), "　", dr("OUTKA_CUST_NM_M").ToString())
                .cmbInkaWhType.SelectedValue = dr("INKA_WH_TYPE").ToString()
                .txtInkaCustNo.TextValue = String.Concat(dr("INKA_CUST_CD_L").ToString(), "-", dr("INKA_CUST_CD_M").ToString())
                .txtInkaCustNm.TextValue = String.Concat(dr("INKA_CUST_NM_L").ToString(), "　", dr("INKA_CUST_NM_M").ToString())
                .cmbBeforeGoodsRank.SelectedValue = dr("BEFORE_GOODS_RANK").ToString()
                .cmbAfterGoodsRank.SelectedValue = dr("AFTER_GOODS_RANK").ToString()
                .txtGoodsCd.TextValue = dr("GOODS_CD").ToString()
                .txtGoodsCdNrs.TextValue = dr("GOODS_CD_NRS").ToString()
                .txtGoodsNm.TextValue = dr("GOODS_NM").ToString()
                .txtLotNo.TextValue = dr("LOT_NO").ToString()
                .numNb.Value = dr("NB").ToString()
                .imdLtDate.TextValue = dr("LT_DATE").ToString()
                .txtRemark.TextValue = dr("REMARK").ToString()
                .txtObicShubetu.TextValue = dr("OBIC_SHUBETU").ToString()
                .txtObicTorihikiKbn.TextValue = dr("OBIC_TORIHIKI_KBN").ToString()
                .txtObicDenpNo.TextValue = dr("OBIC_DENP_NO").ToString()
                .txtObicGyoNo.TextValue = dr("OBIC_GYO_NO").ToString()
                .txtObicDetailNo.TextValue = dr("OBIC_DETAIL_NO").ToString()
                .txtSysUpdDate.TextValue = dr("SYS_UPD_DATE").ToString()
                .txtSysUpdTime.TextValue = dr("SYS_UPD_TIME").ToString()
            End If

        End With

    End Sub

#End Region

#End Region

End Class
