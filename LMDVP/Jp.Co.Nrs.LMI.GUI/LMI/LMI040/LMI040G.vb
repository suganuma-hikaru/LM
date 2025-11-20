' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMMG     : 請求
'  プログラムID     :  LMI040G  : 請求データ編集[デュポン用]
'  作  成  者       :  [HISHI]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports GrapeCity.Win.Editors.Fields

''' <summary>
''' LMI040Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI040G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI040F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI040F, ByVal g As LMIControlG)

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
    Friend Sub SetFunctionKey(ByVal eventShubetsu As LMI040C.EventShubetsu)

        Dim always As Boolean = True
        Dim edit As Boolean = False
        Dim view As Boolean = False
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = "新　規"
            .F2ButtonName = "編　集"
            .F3ButtonName = "複　写"
            .F4ButtonName = "削　除"
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = "検　索"
            .F10ButtonName = String.Empty
            .F11ButtonName = "保　存"
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            If (eventShubetsu).Equals(LMI040C.EventShubetsu.SHOKI) = True Then  '初期表示
                .F1ButtonEnabled = always
                .F2ButtonEnabled = lock
                .F3ButtonEnabled = lock
                .F4ButtonEnabled = lock
                .F5ButtonEnabled = lock
                .F6ButtonEnabled = lock
                .F7ButtonEnabled = lock
                .F8ButtonEnabled = lock
                .F9ButtonEnabled = always
                .F10ButtonEnabled = lock
                .F11ButtonEnabled = lock
                .F12ButtonEnabled = always
                Me._Frm.btnPrint.Enabled = always
                Me._Frm.btnFile.Enabled = always
            ElseIf (eventShubetsu).Equals(LMI040C.EventShubetsu.SINKI) = True Then  '新規
                .F1ButtonEnabled = lock
                .F2ButtonEnabled = lock
                .F3ButtonEnabled = lock
                .F4ButtonEnabled = lock
                .F5ButtonEnabled = lock
                .F6ButtonEnabled = lock
                .F7ButtonEnabled = lock
                .F8ButtonEnabled = lock
                .F9ButtonEnabled = always
                .F10ButtonEnabled = lock
                .F11ButtonEnabled = always
                .F12ButtonEnabled = always
                Me._Frm.btnPrint.Enabled = lock
                Me._Frm.btnFile.Enabled = lock
            ElseIf (eventShubetsu).Equals(LMI040C.EventShubetsu.HENSHU) = True Then  '編集
                .F1ButtonEnabled = lock
                .F2ButtonEnabled = lock
                .F3ButtonEnabled = lock
                .F4ButtonEnabled = lock
                .F5ButtonEnabled = lock
                .F6ButtonEnabled = lock
                .F7ButtonEnabled = lock
                .F8ButtonEnabled = lock
                .F9ButtonEnabled = always
                .F10ButtonEnabled = lock
                .F11ButtonEnabled = always
                .F12ButtonEnabled = always
                Me._Frm.btnPrint.Enabled = lock
                Me._Frm.btnFile.Enabled = lock
            ElseIf (eventShubetsu).Equals(LMI040C.EventShubetsu.FUKUSHA) = True Then  '複写
                .F1ButtonEnabled = lock
                .F2ButtonEnabled = lock
                .F3ButtonEnabled = lock
                .F4ButtonEnabled = lock
                .F5ButtonEnabled = lock
                .F6ButtonEnabled = lock
                .F7ButtonEnabled = lock
                .F8ButtonEnabled = lock
                .F9ButtonEnabled = always
                .F10ButtonEnabled = lock
                .F11ButtonEnabled = always
                .F12ButtonEnabled = always
                Me._Frm.btnPrint.Enabled = lock
                Me._Frm.btnFile.Enabled = lock
            ElseIf (eventShubetsu).Equals(LMI040C.EventShubetsu.DEL) = True Then  '削除
                .F1ButtonEnabled = always
                .F2ButtonEnabled = lock
                .F3ButtonEnabled = lock
                .F4ButtonEnabled = lock
                .F5ButtonEnabled = lock
                .F6ButtonEnabled = lock
                .F7ButtonEnabled = lock
                .F8ButtonEnabled = lock
                .F9ButtonEnabled = always
                .F10ButtonEnabled = lock
                .F11ButtonEnabled = lock
                .F12ButtonEnabled = always
                Me._Frm.btnPrint.Enabled = always
                Me._Frm.btnFile.Enabled = always
            ElseIf (eventShubetsu).Equals(LMI040C.EventShubetsu.KENSAKU) = True Then  '検索
                .F1ButtonEnabled = always
                .F2ButtonEnabled = lock
                .F3ButtonEnabled = lock
                .F4ButtonEnabled = lock
                .F5ButtonEnabled = lock
                .F6ButtonEnabled = lock
                .F7ButtonEnabled = lock
                .F8ButtonEnabled = lock
                .F9ButtonEnabled = always
                .F10ButtonEnabled = lock
                .F11ButtonEnabled = lock
                .F12ButtonEnabled = always
                Me._Frm.btnPrint.Enabled = always
                Me._Frm.btnFile.Enabled = always
            ElseIf (eventShubetsu).Equals(LMI040C.EventShubetsu.HOZON) = True Then  '保存
                .F1ButtonEnabled = always
                .F2ButtonEnabled = lock
                .F3ButtonEnabled = lock
                .F4ButtonEnabled = lock
                .F5ButtonEnabled = lock
                .F6ButtonEnabled = lock
                .F7ButtonEnabled = lock
                .F8ButtonEnabled = lock
                .F9ButtonEnabled = always
                .F10ButtonEnabled = lock
                .F11ButtonEnabled = always
                .F12ButtonEnabled = always
                Me._Frm.btnPrint.Enabled = always
                Me._Frm.btnFile.Enabled = always
            ElseIf (eventShubetsu).Equals(LMI040C.EventShubetsu.DOUBLECLICK) = True Then  '選択
                .F1ButtonEnabled = always
                .F2ButtonEnabled = always
                .F3ButtonEnabled = always
                .F4ButtonEnabled = always
                .F5ButtonEnabled = lock
                .F6ButtonEnabled = lock
                .F7ButtonEnabled = lock
                .F8ButtonEnabled = lock
                .F9ButtonEnabled = always
                .F10ButtonEnabled = lock
                .F11ButtonEnabled = lock
                .F12ButtonEnabled = always
                Me._Frm.btnPrint.Enabled = always
                Me._Frm.btnFile.Enabled = always
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

            .cmbEigyo.TabIndex = LMI040C.CtlTabIndex.EIGYO
            .imdSeikyu.TabIndex = LMI040C.CtlTabIndex.SEIKYUDATEKENSAKU
            .cmbMisuku.TabIndex = LMI040C.CtlTabIndex.MISUKUKENSAKU
            .cmbJigyoubu.TabIndex = LMI040C.CtlTabIndex.ZIGYOKENSAKU
            .cmbSeikyuKoumoku.TabIndex = LMI040C.CtlTabIndex.SEIKYUKENSAKU
            .cmbPrint.TabIndex = LMI040C.CtlTabIndex.PRINT
            'START YANAI 要望番号830
            .cmbPrintType10.TabIndex = LMI040C.CtlTabIndex.PRINTTYPE10
            .cmbPrintType11.TabIndex = LMI040C.CtlTabIndex.PRINTTYPE11
            .cmbPrintType12.TabIndex = LMI040C.CtlTabIndex.PRINTTYPE12
            .cmbPrintType13.TabIndex = LMI040C.CtlTabIndex.PRINTTYPE13
            .cmbPrintType20.TabIndex = LMI040C.CtlTabIndex.PRINTTYPE20
            .cmbPrintType21.TabIndex = LMI040C.CtlTabIndex.PRINTTYPE21
            'END YANAI 要望番号830
            .btnPrint.TabIndex = LMI040C.CtlTabIndex.BTNPRINT
            .cmbFile.TabIndex = LMI040C.CtlTabIndex.FILESYUBETSU
            .btnFile.TabIndex = LMI040C.CtlTabIndex.BTNFILE
            .sprDetail.TabIndex = LMI040C.CtlTabIndex.SPRDETAILS
            .imdTuki.TabIndex = LMI040C.CtlTabIndex.SEIKYUDATE
            .cmbJigyou.TabIndex = LMI040C.CtlTabIndex.ZIGYO
            .cmbSeikyukoumoku2.TabIndex = LMI040C.CtlTabIndex.SEIKYU
            .txtFrbcd.TabIndex = LMI040C.CtlTabIndex.FRBCD
            .txtSrc.TabIndex = LMI040C.CtlTabIndex.SRCCD
            .txtCust.TabIndex = LMI040C.CtlTabIndex.COSTCENTER
            .cmbMisk.TabIndex = LMI040C.CtlTabIndex.MISKCD
            .txtDestCity.TabIndex = LMI040C.CtlTabIndex.DESTCITY
            .numKazei.TabIndex = LMI040C.CtlTabIndex.KAZEI
            .numHiKazei.TabIndex = LMI040C.CtlTabIndex.HIKAZEI
            .numZeigaku.TabIndex = LMI040C.CtlTabIndex.ZEIGAKU

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

        With Me._Frm
            '前月の末日を求める
            Dim lastDate As String = Convert.ToString(DateSerial(Convert.ToInt32(Mid(sysDate, 1, 4)), Convert.ToInt32(Mid(sysDate, 5, 2)), 1).AddDays(-1))
            lastDate = String.Concat( _
                                    Mid(lastDate, 1, 4), _
                                    Mid(lastDate, 6, 2), _
                                    Mid(lastDate, 9, 2) _
                                   )
            .imdSeikyu.TextValue = lastDate

        End With

    End Sub

    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetNumberControl()

        With Me._frm
            Dim d10 As Decimal = Convert.ToDecimal(LMI040C.KINGAKU_MAX)
            Dim sharp10 As String = "#,###,###,##0"
            'START YANAI 要望番号830
            Dim d12 As Decimal = Convert.ToDecimal(LMI040C.KINGAKU_SUM_MAX)
            Dim sharp12 As String = "###,###,###,##0"
            'END YANAI 要望番号830

            .numSeikyuKingaku.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, Convert.ToDecimal(-999999999))
            .numKazei.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, Convert.ToDecimal(-999999999))
            .numHiKazei.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, Convert.ToDecimal(-999999999))
            .numZeigaku.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, Convert.ToDecimal(-999999999))
            'START YANAI 要望番号830
            .numSeikyuKingakuSum.SetInputFields(sharp12, , 12, 1, , 0, 0, , d12, Convert.ToDecimal(-99999999999))
            .numkazeigakuSum.SetInputFields(sharp12, , 12, 1, , 0, 0, , d12, Convert.ToDecimal(-99999999999))
            .numhikazeigakuSum.SetInputFields(sharp12, , 12, 1, , 0, 0, , d12, Convert.ToDecimal(-99999999999))
            .numzeigakuSum.SetInputFields(sharp12, , 12, 1, , 0, 0, , d12, Convert.ToDecimal(-99999999999))
            'END YANAI 要望番号830

        End With

    End Sub

    ''' <summary>
    ''' 日付を表示するコントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetDateControl()

        With Me._Frm

            Call Me.SetDateFormat(.imdSeikyu)
            Call Me.SetDateFormat(.imdTuki)
            Call Me.SetDateFormat(.imdTukiHide)

        End With

    End Sub

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <param name="ctl">書式設定を行うコントロール</param>
    ''' <remarks></remarks>
    Private Sub SetDateFormat(ByVal ctl As LMImDate)

        ctl.Format = DateFieldsBuilder.BuildFields("yyyyMM")
        ctl.DisplayFormat = DateDisplayFieldsBuilder.BuildFields("yyyy/MM")

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
                .cmbEigyo.ReadOnly = False
                .imdSeikyu.ReadOnly = False
                .cmbMisuku.ReadOnly = False
                .cmbJigyoubu.ReadOnly = False
                .cmbSeikyuKoumoku.ReadOnly = False
                .cmbPrint.ReadOnly = False
                'START YANAI 要望番号830
                .cmbPrintType11.ReadOnly = False
                .cmbPrintType12.ReadOnly = False
                .cmbPrintType13.ReadOnly = False
                .cmbPrintType21.ReadOnly = False
                'END YANAI 要望番号830
                .cmbFile.ReadOnly = False

                .cmbEigyo2.ReadOnly = False
                .imdTuki.ReadOnly = False
                .cmbJigyou.ReadOnly = False
                .cmbSeikyukoumoku2.ReadOnly = False
                .txtFrbcd.ReadOnly = False
                .txtSrc.ReadOnly = False
                .txtCust.ReadOnly = False
                .cmbMisk.ReadOnly = False
                .txtDestCity.ReadOnly = False
                .numSeikyuKingaku.ReadOnly = True
                .numKazei.ReadOnly = False
                .numHiKazei.ReadOnly = False
                .numZeigaku.ReadOnly = False
            ElseIf (RecordStatus.NOMAL_REC).Equals(recStatus) Then
                '編集モード
                .cmbEigyo.ReadOnly = False
                .imdSeikyu.ReadOnly = False
                .cmbMisuku.ReadOnly = False
                .cmbJigyoubu.ReadOnly = False
                .cmbSeikyuKoumoku.ReadOnly = False
                .cmbPrint.ReadOnly = True
                'START YANAI 要望番号830
                .cmbPrintType11.ReadOnly = True
                .cmbPrintType12.ReadOnly = True
                .cmbPrintType13.ReadOnly = True
                .cmbPrintType21.ReadOnly = True
                'END YANAI 要望番号830
                .cmbFile.ReadOnly = True

                .cmbEigyo2.ReadOnly = True
                .imdTuki.ReadOnly = True
                .cmbJigyou.ReadOnly = True
                .cmbSeikyukoumoku2.ReadOnly = True
                .txtFrbcd.ReadOnly = True
                .txtSrc.ReadOnly = True
                .txtCust.ReadOnly = True
                .cmbMisk.ReadOnly = True
                .txtDestCity.ReadOnly = False
                .numSeikyuKingaku.ReadOnly = True
                .numKazei.ReadOnly = False
                .numHiKazei.ReadOnly = False
                .numZeigaku.ReadOnly = False
            ElseIf (RecordStatus.COPY_REC).Equals(recStatus) Then
                'コピーモード
                .cmbEigyo.ReadOnly = False
                .imdSeikyu.ReadOnly = False
                .cmbMisuku.ReadOnly = False
                .cmbJigyoubu.ReadOnly = False
                .cmbSeikyuKoumoku.ReadOnly = False
                .cmbPrint.ReadOnly = False
                'START YANAI 要望番号830
                .cmbPrintType11.ReadOnly = False
                .cmbPrintType12.ReadOnly = False
                .cmbPrintType13.ReadOnly = False
                .cmbPrintType21.ReadOnly = False
                'END YANAI 要望番号830
                .cmbFile.ReadOnly = False

                .cmbEigyo2.ReadOnly = False
                .imdTuki.ReadOnly = False
                .cmbJigyou.ReadOnly = False
                .cmbSeikyukoumoku2.ReadOnly = False
                .txtFrbcd.ReadOnly = False
                .txtSrc.ReadOnly = False
                .txtCust.ReadOnly = False
                .cmbMisk.ReadOnly = False
                .txtDestCity.ReadOnly = False
                .numSeikyuKingaku.ReadOnly = True
                .numKazei.ReadOnly = False
                .numHiKazei.ReadOnly = False
                .numZeigaku.ReadOnly = False

                .lblAuto.TextValue = String.Empty
                .lblShuDou.TextValue = String.Empty
                .lblAutoHide.TextValue = "00"
                .lblShuDouHide.TextValue = "01"
            End If

            If (DispMode.VIEW).Equals(disMode) OrElse _
                (DispMode.INIT).Equals(disMode) Then
                '参照モード時
                .cmbEigyo.ReadOnly = False
                .imdSeikyu.ReadOnly = False
                .cmbMisuku.ReadOnly = False
                .cmbJigyoubu.ReadOnly = False
                .cmbSeikyuKoumoku.ReadOnly = False
                .cmbPrint.ReadOnly = False
                'START YANAI 要望番号830
                .cmbPrintType11.ReadOnly = False
                .cmbPrintType12.ReadOnly = False
                .cmbPrintType13.ReadOnly = False
                .cmbPrintType21.ReadOnly = False
                'END YANAI 要望番号830
                .cmbFile.ReadOnly = False

                .cmbEigyo2.ReadOnly = True
                .imdTuki.ReadOnly = True
                .cmbJigyou.ReadOnly = True
                .cmbSeikyukoumoku2.ReadOnly = True
                .txtFrbcd.ReadOnly = True
                .txtSrc.ReadOnly = True
                .txtCust.ReadOnly = True
                .cmbMisk.ReadOnly = True
                .txtDestCity.ReadOnly = True
                .numSeikyuKingaku.ReadOnly = True
                .numKazei.ReadOnly = True
                .numHiKazei.ReadOnly = True
                .numZeigaku.ReadOnly = True
            End If

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .cmbEigyo.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbEigyo2.SelectedValue = Nothing
            .imdTuki.TextValue = String.Empty
            .cmbJigyou.SelectedValue = Nothing
            .cmbSeikyukoumoku2.SelectedValue = Nothing
            .txtFrbcd.TextValue = String.Empty
            .txtSrc.TextValue = String.Empty
            .txtCust.TextValue = String.Empty
            .cmbMisk.SelectedValue = Nothing
            .txtDestCity.TextValue = String.Empty
            .numSeikyuKingaku.Value = 0
            .numKazei.Value = 0
            .numHiKazei.Value = 0
            .numZeigaku.Value = 0
            .lblAuto.TextValue = String.Empty
            .lblShuDou.TextValue = String.Empty
            .lblCreateUser.TextValue = String.Empty
            .lblCreateDate.TextValue = String.Empty
            .lblUpdateUser.TextValue = String.Empty
            .lblUpdateDate.TextValue = String.Empty
            .lblUpdateTime.TextValue = String.Empty
            .numSeikyuKingakuSum.Value = 0
            .numhikazeigakuSum.Value = 0
            .numkazeigakuSum.Value = 0
            .numzeigakuSum.Value = 0
            '隠し項目
            .imdTukiHide.TextValue = String.Empty
            .cmbJigyouHide.SelectedValue = Nothing
            .cmbSeikyukoumoku2Hide.SelectedValue = Nothing
            .txtFrbcdHide.TextValue = String.Empty
            .txtSrcHide.TextValue = String.Empty
            .txtCustHide.TextValue = String.Empty
            .cmbMiskHide.SelectedValue = Nothing
            .txtDestCityHide.TextValue = String.Empty
            .lblAutoHide.TextValue = "00"
            .lblShuDouHide.TextValue = "01"

        End With

    End Sub

    'START YANAI 要望番号830
    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControlSinki()

        With Me._Frm

            .cmbEigyo2.SelectedValue = .cmbEigyo.SelectedValue
            .imdTuki.TextValue = Mid(.imdSeikyu.TextValue, 1, 6)
            .cmbJigyou.SelectedValue = .cmbJigyoubu.SelectedValue
            .cmbSeikyukoumoku2.SelectedValue = .cmbSeikyuKoumoku.SelectedValue
            .txtFrbcd.TextValue = String.Empty
            .txtSrc.TextValue = String.Empty
            .txtCust.TextValue = String.Empty
            .cmbMisk.SelectedValue = Nothing
            .txtDestCity.TextValue = String.Empty
            .numSeikyuKingaku.Value = 0
            .numKazei.Value = 0
            .numHiKazei.Value = 0
            .numZeigaku.Value = 0
            .lblAuto.TextValue = String.Empty
            .lblShuDou.TextValue = String.Empty
            .lblCreateUser.TextValue = String.Empty
            .lblCreateDate.TextValue = String.Empty
            .lblUpdateUser.TextValue = String.Empty
            .lblUpdateDate.TextValue = String.Empty
            .lblUpdateTime.TextValue = String.Empty
            .numSeikyuKingakuSum.Value = 0
            .numhikazeigakuSum.Value = 0
            .numkazeigakuSum.Value = 0
            .numzeigakuSum.Value = 0
            '隠し項目
            .imdTukiHide.TextValue = String.Empty
            .cmbJigyouHide.SelectedValue = Nothing
            .cmbSeikyukoumoku2Hide.SelectedValue = Nothing
            .txtFrbcdHide.TextValue = String.Empty
            .txtSrcHide.TextValue = String.Empty
            .txtCustHide.TextValue = String.Empty
            .cmbMiskHide.SelectedValue = Nothing
            .txtDestCityHide.TextValue = String.Empty
            .lblAutoHide.TextValue = "00"
            .lblShuDouHide.TextValue = "01"

        End With

    End Sub
    'END YANAI 要望番号830

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetEigyo()

        With Me._Frm

            '自営業所の設定
            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            Me._Frm.cmbEigyo2.SelectedValue = brCd

        End With

    End Sub

    ''' <summary>
    ''' コントロール(スプレッド)値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearSpreadControl()

        With Me._Frm

            .sprDetail.CrearSpread()

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()
        Call Me.SetInitSpread()
    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal frm As LMI040F, ByVal row As Integer)

        With frm

            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet
            Dim kbnDr() As DataRow = Nothing
            '******************* 商品タブ *****************************
            .cmbEigyo2.SelectedValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.NRSBRCD.ColNo))
            .imdTuki.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.TUKI.ColNo)).Replace("/", "")
            .cmbJigyou.SelectedValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.JIGYOUCD.ColNo))
            .cmbSeikyukoumoku2.SelectedValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.SEIKYUCD.ColNo))
            .txtFrbcd.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.FRBCD.ColNo))
            .txtSrc.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.SRCCD.ColNo))
            .txtCust.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.COST.ColNo))
            .cmbMisk.SelectedValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.MISKCD.ColNo))
            .txtDestCity.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.DESTCITY.ColNo))
            .numSeikyuKingaku.Value = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.AMOUNT.ColNo))
            .numKazei.Value = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.SOUND.ColNo))
            .numHiKazei.Value = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.BOND.ColNo))
            .numZeigaku.Value = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.VATAMOUNT.ColNo))

            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'U009' AND ", _
                                                                                           "KBN_CD = '", Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.JIDOU.ColNo)), "'"))
            .lblAuto.TextValue = String.Empty
            If 0 < kbnDr.Length Then
                .lblAuto.TextValue = kbnDr(0).Item("KBN_NM2").ToString
            End If
            .lblAutoHide.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.JIDOU.ColNo))

            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'U009' AND ", _
                                                                                           "KBN_CD = '", Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.SHUDOU.ColNo)), "'"))
            .lblShuDou.TextValue = String.Empty
            If 0 < kbnDr.Length Then
                .lblShuDou.TextValue = kbnDr(0).Item("KBN_NM2").ToString
            End If
            .lblShuDouHide.TextValue = "01"

            .lblCreateUser.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.SYSENTUSER.ColNo))
            'START YANAI 要望番号830
            '.lblCreateDate.TextValue = String.Concat(Mid(Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.SYSENTDATE.ColNo)), 1, 4), _
            '                                         "/", _
            '                                         Mid(Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.SYSENTDATE.ColNo)), 5, 2), _
            '                                         "/", _
            '                                         Mid(Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.SYSENTDATE.ColNo)), 7, 2))
            .lblCreateDate.TextValue =Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.SYSENTDATE.ColNo))
            'END YANAI 要望番号830
            .lblUpdateUser.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.SYSUPDUSER.ColNo))
            'START YANAI 要望番号830
            '.lblUpdateDate.TextValue = String.Concat(Mid(Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.SYSUPDDATE.ColNo)), 1, 4), _
            '                                         "/", _
            '                                         Mid(Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.SYSUPDDATE.ColNo)), 5, 2), _
            '                                         "/", _
            '                                         Mid(Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.SYSUPDDATE.ColNo)), 7, 2))
            .lblUpdateDate.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.SYSUPDDATE.ColNo))
            'END YANAI 要望番号830
            .lblUpdateTime.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.SYSUPDTIME.ColNo))
            '隠し項目
            .imdTukiHide.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.TUKI.ColNo)).Replace("/", "")
            .cmbJigyouHide.SelectedValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.JIGYOUCD.ColNo))
            .cmbSeikyukoumoku2Hide.SelectedValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.SEIKYUCD.ColNo))
            .txtFrbcdHide.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.FRBCD.ColNo))
            .txtSrcHide.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.SRCCD.ColNo))
            .txtCustHide.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.COST.ColNo))
            .cmbMiskHide.SelectedValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.MISKCD.ColNo))
            .txtDestCityHide.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI040G.sprDetailDef.DESTCITY.ColNo))

        End With

    End Sub

    ''' <summary>
    ''' 請求金額を計算
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SEIKYUKEISAN()

        With Me._Frm
            If .numKazei.Enabled = True Then
                'ロックの場合は計算しない
                .numSeikyuKingaku.Value = Convert.ToDecimal(.numKazei.Value) + Convert.ToDecimal(.numHiKazei.Value)
            End If
        End With

    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        '******* 表示列 *******
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.DEF, "  ", 20, True)
        'START YANAI 要望番号830
        'Public Shared TUKI As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.TUKI, "請求月", 80, True)
        'Public Shared JIGYOU As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.JIGYOU, "事業部", 60, True)
        'Public Shared SEIKYU As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SEIKYU, "請求項目", 80, True)
        'Public Shared FRBCD As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.FRBCD, "FRBコード", 120, True)
        'Public Shared SRCCD As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SRCCD, "SRCコード", 120, True)
        'Public Shared COST As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.COST, "コストセンター", 140, True)
        Public Shared TUKI As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.TUKI, "請求月", 55, True)
        Public Shared JIGYOU As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.JIGYOU, "事業部", 100, True)
        Public Shared SEIKYU As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SEIKYU, "請求項目", 80, True)
        Public Shared FRBCD As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.FRBCD, "FRBコード", 65, True)
        Public Shared SRCCD As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SRCCD, "SRCコード", 50, True)
        Public Shared COST As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.COST, "コストセンター", 60, True)
        'END YANAI 要望番号830
        'START YANAI 要望番号830
        'Public Shared MISKNM As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.MISKNM, "ミスク", 120, True)
        Public Shared MISKNM As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.MISKNM, "ミスク", 60, True)
        'END YANAI 要望番号830
        'START YANAI 要望番号830
        'Public Shared DESTCITY As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.DESTCITY, "届先都市", 1, False)
        Public Shared DESTCITY As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.DESTCITY, "届先都市", 40, True)
        'END YANAI 要望番号830
        'START YANAI 要望番号830
        'Public Shared AMOUNT As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.AMOUNT, "請求金額", 100, True)
        'Public Shared SOUND As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SOUND, "課税金額", 100, True)
        'Public Shared BOND As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.BOND, "非課税金額", 100, True)
        'Public Shared VATAMOUNT As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.VATAMOUNT, "税額", 100, True)
        Public Shared AMOUNT As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.AMOUNT, "請求金額", 85, True)
        Public Shared SOUND As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SOUND, "課税金額", 85, True)
        Public Shared BOND As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.BOND, "非課税金額", 85, True)
        Public Shared VATAMOUNT As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.VATAMOUNT, "税額", 80, True)
        'END YANAI 要望番号830
        'START YANAI 要望番号830
        'Public Shared SYSENTDATE As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SYSENTDATE, "作成日", 1, False)
        'Public Shared SYSUPDDATE As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SYSUPDDATE, "更新日", 1, False)
        'Public Shared SYSUPDTIME As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SYSUPDTIME, "更新時間", 1, False)
        'START YANAI 要望番号830
        'Public Shared NRSBRNM As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.NRSBRNM, "営業所", 200, True)
        Public Shared JIDOUFLG As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.JIDOUFLG, "自動計算", 35, True)
        Public Shared SHUDOUFLG As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SHUDOUFLG, "手動入力", 35, True)
        Public Shared NRSBRNM As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.NRSBRNM, "営業所", 140, True)
        'END YANAI 要望番号830
        Public Shared SYSENTDATE As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SYSENTDATE, "作成日", 80, True)
        Public Shared SYSENTUSER As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SYSENTUSER, "作成者", 80, True)
        Public Shared SYSUPDDATE As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SYSUPDDATE, "更新日", 80, True)
        Public Shared SYSUPDUSER As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SYSUPDUSER, "更新者", 80, True)
        Public Shared SYSUPDTIME As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SYSUPDTIME, "更新時間", 70, True)
        'END YANAI 要望番号830
        Public Shared NRSBRCD As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.NRSBRCD, "営業所", 1, False)
        'START YANAI 要望番号830
        'Public Shared SYSENTUSER As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SYSENTUSER, "作成者", 1, False)
        'Public Shared SYSUPDUSER As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SYSUPDUSER, "更新者", 1, False)
        'END YANAI 要望番号830
        Public Shared JIDOU As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.JIDOU, "自動計算フラグ", 1, False)
        Public Shared SHUDOU As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SHUDOU, "手動入力フラグ", 1, False)
        Public Shared SYSDELFLG As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SYSDELFLG, "削除フラグ", 1, False)
        Public Shared JIGYOUCD As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.JIGYOUCD, "事業部コード", 1, False)
        Public Shared SEIKYUCD As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.SEIKYUCD, "請求項目コード", 1, False)
        Public Shared MISKCD As SpreadColProperty = New SpreadColProperty(LMI040C.SprColumnIndex.MISKCD, "ミスクコード", 1, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            .sprDetail.ActiveSheet.ColumnCount = LMI040C.SprColumnIndex.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New LMI040G.sprDetailDef())

            '列固定位置を設定します。(ex.納入予定で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMI040G.sprDetailDef.DEF.ColNo + 1

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータ初期値を設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitSpread()

        'レイアウト用
        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim sLabelRight As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
        Dim sLabelLeft As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
        Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
        Dim sTxt As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, False)
        Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -999999999, 9999999999, True, 0, , ",")
        Dim sCmb As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, Nothing, False)
        Dim sTxtlo As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, True)
        'START YANAI 要望番号830
        Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
        'END YANAI 要望番号830

        With spr

            '行クリア
            .CrearSpread()

            .SuspendLayout()

            'START YANAI 要望番号830
            '.ActiveSheet.Rows.Count = 0
            .ActiveSheet.Rows.Count = 1
            'END YANAI 要望番号830

            sTxt.BackColor = Color.White
            sCmb.BackColor = Color.White


            'スプレッドの設定
            'START YANAI 要望番号830
            '.SetCellStyle(0, LMI040C.SprColumnIndex.DEF, sCheck)
            .SetCellStyle(0, LMI040C.SprColumnIndex.DEF, sDEF)
            'END YANAI 要望番号830
            .SetCellStyle(0, LMI040C.SprColumnIndex.TUKI, sLabelLeft)
            .SetCellStyle(0, LMI040C.SprColumnIndex.JIGYOU, sLabelLeft)
            .SetCellStyle(0, LMI040C.SprColumnIndex.SEIKYU, sLabelLeft)
            'START YANAI 要望番号830
            '.SetCellStyle(0, LMI040C.SprColumnIndex.FRBCD, sLabelLeft)
            '.SetCellStyle(0, LMI040C.SprColumnIndex.SRCCD, sLabelLeft)
            '.SetCellStyle(0, LMI040C.SprColumnIndex.COST, sLabelLeft)
            .SetCellStyle(0, LMI040C.SprColumnIndex.FRBCD, LMSpreadUtility.GetTextCell(Me._Frm.sprDetail, InputControl.HAN_NUM_ALPHA, 7, False))
            .SetCellStyle(0, LMI040C.SprColumnIndex.SRCCD, LMSpreadUtility.GetTextCell(Me._Frm.sprDetail, InputControl.HAN_NUM_ALPHA, 7, False))
            .SetCellStyle(0, LMI040C.SprColumnIndex.COST, LMSpreadUtility.GetTextCell(Me._Frm.sprDetail, InputControl.HAN_NUM_ALPHA, 10, False))
            'END YANAI 要望番号830
            .SetCellStyle(0, LMI040C.SprColumnIndex.MISKNM, sLabelLeft)
            .SetCellStyle(0, LMI040C.SprColumnIndex.AMOUNT, sNum)
            .SetCellStyle(0, LMI040C.SprColumnIndex.SOUND, sNum)
            .SetCellStyle(0, LMI040C.SprColumnIndex.BOND, sNum)
            .SetCellStyle(0, LMI040C.SprColumnIndex.VATAMOUNT, sNum)
            'START YANAI 要望番号830
            .SetCellStyle(0, LMI040C.SprColumnIndex.JIDOUFLG, sLabelLeft)
            .SetCellStyle(0, LMI040C.SprColumnIndex.SHUDOUFLG, sLabelLeft)
            'END YANAI 要望番号830
            .SetCellStyle(0, LMI040C.SprColumnIndex.JIDOU, sLabelLeft)
            .SetCellStyle(0, LMI040C.SprColumnIndex.SHUDOU, sLabelLeft)
            'START YANAI 要望番号830
            '.SetCellStyle(0, LMI040C.SprColumnIndex.SYSENTDATE, sLabelLeft)
            .SetCellStyle(0, LMI040C.SprColumnIndex.SYSENTDATE, LMSpreadUtility.GetTextCell(Me._Frm.sprDetail, InputControl.HAN_NUMBER, 6, False))
            'END YANAI 要望番号830
            .SetCellStyle(0, LMI040C.SprColumnIndex.SYSENTUSER, sLabelLeft)
            .SetCellStyle(0, LMI040C.SprColumnIndex.SYSUPDDATE, sLabelLeft)
            .SetCellStyle(0, LMI040C.SprColumnIndex.SYSUPDTIME, sLabelLeft)
            .SetCellStyle(0, LMI040C.SprColumnIndex.SYSUPDUSER, sLabelLeft)
            .SetCellStyle(0, LMI040C.SprColumnIndex.SYSDELFLG, sLabelLeft)
            .SetCellStyle(0, LMI040C.SprColumnIndex.NRSBRCD, sLabelLeft)
            .SetCellStyle(0, LMI040C.SprColumnIndex.JIGYOUCD, sLabelLeft)
            .SetCellStyle(0, LMI040C.SprColumnIndex.SEIKYUCD, sLabelLeft)
            .SetCellStyle(0, LMI040C.SprColumnIndex.MISKCD, sLabelLeft)
            'START YANAI 要望番号830
            '.SetCellStyle(0, LMI040C.SprColumnIndex.DESTCITY, sLabelLeft)
            .SetCellStyle(0, LMI040C.SprColumnIndex.DESTCITY, LMSpreadUtility.GetTextCell(Me._Frm.sprDetail, InputControl.HAN_ALPHA, 3, False))
            'END YANAI 要望番号830
            'START YANAI 要望番号830
            .SetCellStyle(0, LMI040C.SprColumnIndex.NRSBRNM, sLabelLeft)
            'END YANAI 要望番号830

            'START YANAI 要望番号830
            .Sheets(0).Cells(0, sprDetailDef.DEF.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.TUKI.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.JIGYOU.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SEIKYU.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.FRBCD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SRCCD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.COST.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.MISKNM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.AMOUNT.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SOUND.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.BOND.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.VATAMOUNT.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.JIDOUFLG.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SHUDOUFLG.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.JIDOU.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SHUDOU.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SYSENTDATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SYSENTUSER.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SYSUPDDATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SYSUPDTIME.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SYSUPDUSER.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SYSDELFLG.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.NRSBRCD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.JIGYOUCD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SEIKYUCD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.MISKCD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.DESTCITY.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.NRSBRNM.ColNo).Value = String.Empty
            'END YANAI 要望番号830

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet)

        'レイアウト用
        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim sLabelRight As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
        Dim sLabelLeft As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
        Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
        Dim sTxt As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, False)
        Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -999999999, 9999999999, True, 0, , ",")
        Dim sCmb As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, Nothing, False)
        Dim sTxtlo As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, True)

        'START YANAI 要望番号830
        'Dim max As Integer = ds.Tables(LMI040C.TABLE_NM_OUT).Rows.Count - 1
        Dim max As Integer = ds.Tables(LMI040C.TABLE_NM_OUT).Rows.Count
        Dim kbnDr() As DataRow = Nothing
        'END YANAI 要望番号830

        With spr

            '行クリア
            .CrearSpread()

            .SuspendLayout()

            'START YANAI 要望番号830
            '.ActiveSheet.Rows.Count = 0
            'END YANAI 要望番号830

            sTxt.BackColor = Color.White
            sCmb.BackColor = Color.White

            'START YANAI 要望番号830
            'For i As Integer = 0 To max
            If 0 < max Then
                .Sheets(0).AddRows(.Sheets(0).Rows.Count, max)
            End If
            For i As Integer = 1 To max
                'END YANAI 要望番号830
                'START YANAI 要望番号830
                ''行の追加
                '.Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)
                'END YANAI 要望番号830

                'スプレッドの設定
                .SetCellStyle(i, LMI040C.SprColumnIndex.DEF, sCheck)
                .SetCellStyle(i, LMI040C.SprColumnIndex.TUKI, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.JIGYOU, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.SEIKYU, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.FRBCD, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.SRCCD, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.COST, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.MISKNM, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.AMOUNT, sNum)
                .SetCellStyle(i, LMI040C.SprColumnIndex.SOUND, sNum)
                .SetCellStyle(i, LMI040C.SprColumnIndex.BOND, sNum)
                .SetCellStyle(i, LMI040C.SprColumnIndex.VATAMOUNT, sNum)
                'START YANAI 要望番号830
                .SetCellStyle(i, LMI040C.SprColumnIndex.JIDOUFLG, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.SHUDOUFLG, sLabelLeft)
                'END YANAI 要望番号830
                .SetCellStyle(i, LMI040C.SprColumnIndex.JIDOU, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.SHUDOU, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.SYSENTDATE, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.SYSENTUSER, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.SYSUPDDATE, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.SYSUPDTIME, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.SYSUPDUSER, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.SYSDELFLG, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.NRSBRCD, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.JIGYOUCD, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.SEIKYUCD, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.MISKCD, sLabelLeft)
                .SetCellStyle(i, LMI040C.SprColumnIndex.DESTCITY, sLabelLeft)
                'START YANAI 要望番号830
                .SetCellStyle(i, LMI040C.SprColumnIndex.NRSBRNM, sLabelLeft)
                'END YANAI 要望番号830

                .SetCellValue(i, LMI040C.SprColumnIndex.DEF, LMConst.FLG.OFF)
                .SetCellValue(i, LMI040C.SprColumnIndex.TUKI, String.Concat(Mid(ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SEKY_YM").ToString, 1, 4), "/", Mid(ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SEKY_YM").ToString, 5, 2)))
                .SetCellValue(i, LMI040C.SprColumnIndex.JIGYOU, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("DEPART_NM").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.SEIKYU, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SEKY_KMK_NM").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.FRBCD, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("FRB_CD").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.SRCCD, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SRC_CD").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.COST, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("COST_CENTER").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.MISKNM, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("MISK_NM").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.AMOUNT, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("AMOUNT").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.SOUND, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SOUND").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.BOND, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("BOND").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.VATAMOUNT, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("VAT_AMOUNT").ToString)
                'START YANAI 要望番号830
                .SetCellValue(i, LMI040C.SprColumnIndex.JIDOUFLG, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("JIDO_FLAG_NM").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.SHUDOUFLG, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SHUDO_FLAG_NM").ToString)
                'END YANAI 要望番号830
                .SetCellValue(i, LMI040C.SprColumnIndex.JIDOU, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("JIDO_FLAG").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.SHUDOU, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SHUDO_FLAG").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.SYSENTDATE, String.Concat(Mid(ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SYS_ENT_DATE").ToString, 1, 4), "/", Mid(ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SYS_ENT_DATE").ToString, 5, 2), "/", Mid(ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SYS_ENT_DATE").ToString, 7, 2)))
                .SetCellValue(i, LMI040C.SprColumnIndex.SYSENTUSER, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SYS_ENT_USER_NM").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.SYSUPDDATE, String.Concat(Mid(ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SYS_UPD_DATE").ToString, 1, 4), "/", Mid(ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SYS_UPD_DATE").ToString, 5, 2), "/", Mid(ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SYS_UPD_DATE").ToString, 7, 2)))
                .SetCellValue(i, LMI040C.SprColumnIndex.SYSUPDTIME, String.Concat(Mid(ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SYS_UPD_TIME").ToString, 1, 2), ":", Mid(ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SYS_UPD_TIME").ToString, 3, 2), ":", Mid(ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SYS_UPD_TIME").ToString, 5, 2)))
                .SetCellValue(i, LMI040C.SprColumnIndex.SYSUPDUSER, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SYS_UPD_USER_NM").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.SYSDELFLG, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SYS_DEL_FLG").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.NRSBRCD, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("NRS_BR_CD").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.JIGYOUCD, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("DEPART").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.SEIKYUCD, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("SEKY_KMK").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.MISKCD, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("MISK_CD").ToString)
                .SetCellValue(i, LMI040C.SprColumnIndex.DESTCITY, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("DEST_CTY").ToString)
                'START YANAI 要望番号830
                .SetCellValue(i, LMI040C.SprColumnIndex.NRSBRNM, ds.Tables(LMI040C.TABLE_NM_OUT).Rows(i - 1).Item("NRS_BR_NM").ToString)
                'END YANAI 要望番号830

            Next

            .ResumeLayout(True)

        End With

    End Sub

    'START YANAI 要望番号830
    ''' <summary>
    ''' スプレッドの各金額の合計を求める
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SumKingaku()

        'レイアウト用
        Dim spr As LMSpread = Me._Frm.sprDetail

        Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
        Dim amountSum As Decimal = 0
        Dim soundSum As Decimal = 0
        Dim bondSum As Decimal = 0
        Dim vamountSum As Decimal = 0

        With spr

            For i As Integer = 1 To max
                If Me._LMIConG.GetCellValue(spr.ActiveSheet.Cells(i, LMI040G.sprDetailDef.SYSDELFLG.ColNo)).Equals("1") = True Then
                    Continue For
                End If
                amountSum = amountSum + Convert.ToDecimal(Me._LMIConG.GetCellValue(spr.ActiveSheet.Cells(i, LMI040G.sprDetailDef.AMOUNT.ColNo)))
                soundSum = soundSum + Convert.ToDecimal(Me._LMIConG.GetCellValue(spr.ActiveSheet.Cells(i, LMI040G.sprDetailDef.SOUND.ColNo)))
                bondSum = bondSum + Convert.ToDecimal(Me._LMIConG.GetCellValue(spr.ActiveSheet.Cells(i, LMI040G.sprDetailDef.BOND.ColNo)))
                vamountSum = vamountSum + Convert.ToDecimal(Me._LMIConG.GetCellValue(spr.ActiveSheet.Cells(i, LMI040G.sprDetailDef.VATAMOUNT.ColNo)))
            Next

            Me._Frm.numSeikyuKingakuSum.Value = amountSum
            Me._Frm.numkazeigakuSum.Value = soundSum
            Me._Frm.numhikazeigakuSum.Value = bondSum
            Me._Frm.numzeigakuSum.Value = vamountSum

        End With

    End Sub
    'END YANAI 要望番号830

#End Region

#Region "ユーティリティ"

#Region "プロパティ"

    ''' <summary>
    ''' セルのプロパティを設定(CheckBox)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoChk(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetCheckBoxCell(spr, False)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Label)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoLabel(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

    End Function

#End Region

#End Region

#End Region

End Class
