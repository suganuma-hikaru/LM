' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG040G : 請求処理 請求鑑検索
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports GrapeCity.Win.Editors

''' <summary>
''' LMG040Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMG040G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMG040F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMGControlG

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMG040F, ByVal g As LMGControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ControlG = g

    End Sub

#End Region

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim always As Boolean = True
        Dim lock As Boolean = False

        '外部倉庫用ABP対策
        Dim drABP As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'G203' AND KBN_NM1 = '", LM.Base.LMUserInfoManager.GetNrsBrCd, "'"))

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True
            'ファンクションキー個別設定
            .F1ButtonName = LMGControlC.FUNCTION_SHINKI_TORIKOMI
            .F2ButtonName = LMGControlC.FUNCTION_SHINKI_TEGAKI
            .F3ButtonName = String.Empty
            'START YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
            '.F4ButtonName = String.Empty
            .F4ButtonName = LMGControlC.FUNCTION_SAKUJO
            'END YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
            .F5ButtonName = LMGControlC.FUNCTION_KAKUTEI
            .F6ButtonName = String.Empty
            'START YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
            '.F7ButtonName = String.Empty
            '(2013.02.05)要望番号1829 初期化ボタンをなくす -- START --
            '.F7ButtonName = LMGControlC.FUNCTION_SHOKIKA
            ' UPD 2021/04/08 依頼番号 : 019742
            '.F7ButtonName = String.Empty
            If drABP.Length > 0 Then
                'SAP出力を非表示
                .F7ButtonName = String.Empty
            Else
                .F7ButtonName = LMGControlC.FUNCTION_SAP_OUT
            End If
            '(2013.02.05)要望番号1829 初期化ボタンをなくす --  END  --
            'END YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
            ' UPD 2021/04/08 依頼番号 : 019742
            '.F8ButtonName = String.Empty
            '.F8ButtonName = LMGControlC.FUNCTION_SAP_CANCEL
            .F8ButtonName = String.Empty
            .F9ButtonName = LMGControlC.FUNCTION_KENSAKU
            .F10ButtonName = LMGControlC.FUNCTION_MST_SANSHO
            .F11ButtonName = LMGControlC.FUNCTION_SKYU_CSV
            .F12ButtonName = LMGControlC.FUNCTION_TOJIRU

            'ファンクションキーの制御
            .F1ButtonEnabled = always
            .F2ButtonEnabled = always
            .F3ButtonEnabled = lock
            'START YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
            '.F4ButtonEnabled = lock
            .F4ButtonEnabled = always
            'END YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
            .F5ButtonEnabled = always
            .F6ButtonEnabled = lock
            'START YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
            '.F7ButtonEnabled = lock
            '(2013.02.05)要望番号1829 初期化ボタンをなくす -- START --
            '.F7ButtonEnabled = always
            ' UPD 2021/04/08 依頼番号 : 019742
            '.F7ButtonEnabled = lock
            If drABP.Length > 0 Then
                'SAP出力を非活性
                .F7ButtonEnabled = lock
            Else
                .F7ButtonEnabled = always
            End If
            '(2013.02.05)要望番号1829 初期化ボタンをなくす --  END  --
            'END YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
            ' UPD 2021/04/08 依頼番号 : 019742
            '.F8ButtonEnabled = lock
            '.F8ButtonEnabled = always
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = always
            .F10ButtonEnabled = always
            .F11ButtonEnabled = always
            .F12ButtonEnabled = always

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .grpSelect.TabIndex = LMG040C.CtlTabIndex.GRP_SELECT
            .cmbBr.TabIndex = LMG040C.CtlTabIndex.CMB_BR
            .imdSeikyuYm.TabIndex = LMG040C.CtlTabIndex.IMD_INV_DATE
            .txtSeikyuCd.TabIndex = LMG040C.CtlTabIndex.TXT_SEIQT_CD
            .lblSeikyuNm.TabIndex = LMG040C.CtlTabIndex.LBL_SEIQT_NM
            .txtSeikyuNo.TabIndex = LMG040C.CtlTabIndex.TXT_SEIQT_NO
            .chkMikakutei.TabIndex = LMG040C.CtlTabIndex.CHK_MIKAKUTEI
            .chkKakutei.TabIndex = LMG040C.CtlTabIndex.CHK_KAKUTEI
            .chkInsatuZumi.TabIndex = LMG040C.CtlTabIndex.CHK_INSATU_ZUMI
            .chkKeiriTorikomi.TabIndex = LMG040C.CtlTabIndex.CHK_KEIRI_TORIKOMI
            .chkKeiriTorikomiTaishoGai.TabIndex = LMG040C.CtlTabIndex.CHK_KEIRI_TORIKOMI_TAISHOGAI
            .chkKeiriTorikeshi.TabIndex = LMG040C.CtlTabIndex.CHK_KEIRI_TORIKESHI
            .chkSkyuCsvFlg.TabIndex = LMG040C.CtlTabIndex.CHK_SKYU_CSV_FLG
            .lblBrCd.TabIndex = LMG040C.CtlTabIndex.LBL_BR_CD
            .sprMeisai.TabIndex = LMG040C.CtlTabIndex.SPR_MEISAI

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

        'コントロールの日付書式設定
        Call Me.SetDateControl()

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .grpSelect.Focus()
            .cmbBr.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbBr.SelectedValue = LMUserInfoManager.GetNrsBrCd

            '2014.08.04 FFEM高取対応 START
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

            If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
                .cmbBr.ReadOnly = True
            Else
                .cmbBr.ReadOnly = False
            End If
            '2014.08.04 FFEM高取対応 END

            .imdSeikyuYm.TextValue = String.Empty
            .txtSeikyuCd.TextValue = String.Empty
            .lblSeikyuNm.TextValue = String.Empty
            .txtSeikyuNo.TextValue = String.Empty
            '2011/08/03 菱刈 進捗区分のチェックボックスのフラグONに変更 スタート
            .chkMikakutei.SetBinaryValue(LMConst.FLG.ON)
            .chkKakutei.SetBinaryValue(LMConst.FLG.ON)
            .chkInsatuZumi.SetBinaryValue(LMConst.FLG.ON)
            .chkKeiriTorikomi.SetBinaryValue(LMConst.FLG.OFF)
            .chkKeiriTorikomiTaishoGai.SetBinaryValue(LMConst.FLG.OFF)
            '2011/08/03 菱刈 進捗区分のチェックボックスのフラグONに変更 エンド
            .lblBrCd.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey()

    End Sub

    ''' <summary>
    ''' 日付を表示するコントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateControl()

        With Me._Frm

            .imdSeikyuYm.Format = Fields.DateFieldsBuilder.BuildFields("yyyyMM")
            .imdSeikyuYm.DisplayFormat = Fields.DateDisplayFieldsBuilder.BuildFields("yyyy/MM")

        End With

    End Sub

    ''' <summary>
    ''' 隠し項目に検索結果格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetHead(ByVal dr As DataRow)

        Me._Frm.lblBrCd.TextValue = dr.Item("NRS_BR_CD").ToString()

    End Sub

    ''' <summary>
    ''' 請求先名称の取得
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSeqName()

        With Me._Frm
            'Dim seqNm As ArrayList = Me._ControlG.GetSeqNm(.txtSeikyuCd.TextValue)
            '20160927 要番2622 tsunehira add Start
            Dim seqNm As ArrayList = Me._ControlG.GetSeqNm(.cmbBr.SelectedValue.ToString, .txtSeikyuCd.TextValue)
            '20160927 要番2622 tsunehira add End

            If seqNm.Count >= 1 Then
                '.lblSeikyuNm.TextValue = Me._ControlG.GetSeqNm(.txtSeikyuCd.TextValue)(0).ToString()
                '20160927 要番2622 tsunehira add Start
                .lblSeikyuNm.TextValue = Me._ControlG.GetSeqNm(.cmbBr.SelectedValue.ToString, .txtSeikyuCd.TextValue)(0).ToString()
                '20160927 要番2622 tsunehira add End


            End If
        End With

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        '*****表示列*****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SEIKYU_NO As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.SEIQT_NO, "請求書" & vbNewLine & "番号", 65, True)
        Public Shared SAP_NO As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.SAP_NO, "SAP伝票番号", 130, True)
        Public Shared SEIKYU_CD As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.SEIQT_CD, "請求先" & vbNewLine & "コード", 65, True)
        Public Shared SEIKYU_NM As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.SEIQT_NM, "請求先名", 300, True)
        Public Shared SEIKYU_CAL As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.SEIQT_AMT, "請求総額", 130, True)
        Public Shared SEIKYUSYO_DATE As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.SEIQT_DATE, "請求書日付", 85, True)
        Public Shared KAKUTEI_ZUMI_CHK As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.KAKUTEIZUMI, "確定済", 70, True)
        Public Shared PRINT_ZUMI_CHK As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.INSATUZUMI, "印刷済", 70, True)
        Public Shared KEIRI_TORIKOMI_CHK As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.KEIRI_TORIKOMI, "経理取込", 70, True)
        Public Shared KEIRI_TORIKOMI_TAISHOGAI_CHK As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.KEIRI_TORIKOMI_TAISHOGAI, "経理取込" & vbNewLine & "対象外", 70, True)
        Public Shared SKYU_CSV_CHK As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.SKYU_CSV, "請求データ" & vbNewLine & "出力済", 70, True)
        Public Shared CREATE_SYUBETU_NM As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.SHUBETU_NM, "作成種別", 120, True)
        Public Shared AKAKURO_NM As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.AKAKURO_NM, "赤黒区分", 70, True)
        Public Shared SEIKYU_NO_RELATED As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.SEIQT_NO_RELATED, "元請求書" & vbNewLine & "番号", 80, True)
        Public Shared SAP_OUT_USER_NM As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.SAP_OUT_USER_NM, "SAP連携ユーザ名", 140, True)
        '*****隠し列*****
        Public Shared CREATE_SYUBETU_KB As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.SHUBETU_KB, "作成種別", 90, False)
        Public Shared AKAKURO_KB As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.AKAKURO_KB, "赤黒区分", 90, False)
        Public Shared STATE_KB As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.STATE_KB, "進捗区分", 90, False)
        Public Shared UPD_DATE As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.UPDATE_DATE, "更新日", 90, False)
        Public Shared UPD_TIME As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.UPDATE_TIME, "更新時間", 90, False)
        Public Shared UNCHIN_INV_FROM As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.UNCHIN_INV_FROM, "運賃取込開始日", 90, False)
        Public Shared SAGYO_INV_FROM As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.SAGYO_INV_FROM, "作業料取込開始日", 90, False)
        Public Shared YOKOMOCHI_INV_FROM As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.YOKOMOCHI_INV_FROM, "横持料取込開始日", 90, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.SYS_DEL_FLG, "削除", 90, False)             'ADD 2018/08/10 依頼番号 : 002136  
        Public Shared SAP_OUT_USER As SpreadColProperty = New SpreadColProperty(LMG040C.SprColumnIndex.SAP_OUT_USER, "SAP連携ユーザID", 90, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        Dim spr As LMSpreadSearch = Me._Frm.sprMeisai

        With spr

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = 26

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprSagyo.SetColProperty(New sprDetailDef)
            .SetColProperty(New LMG040G.sprDetailDef(), False)
            '2015.10.15 英語化対応END

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New sprDetailDef)

            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)

            '列設定
            '*****表示列*****
            .SetCellStyle(0, sprDetailDef.SEIKYU_NO.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SAP_NO.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SEIKYU_CD.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SEIKYU_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 60, False))
            .SetCellStyle(0, sprDetailDef.SEIKYU_CAL.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SEIKYUSYO_DATE.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.KAKUTEI_ZUMI_CHK.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.PRINT_ZUMI_CHK.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.KEIRI_TORIKOMI_CHK.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.KEIRI_TORIKOMI_TAISHOGAI_CHK.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SKYU_CSV_CHK.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.CREATE_SYUBETU_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_K019, False))
            .SetCellStyle(0, sprDetailDef.AKAKURO_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_A001, False))
            .SetCellStyle(0, sprDetailDef.SEIKYU_NO_RELATED.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 7, False))
            .SetCellStyle(0, sprDetailDef.SAP_OUT_USER_NM.ColNo, lbl)
            '*****隠し列*****
            .SetCellStyle(0, sprDetailDef.CREATE_SYUBETU_KB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.AKAKURO_KB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.STATE_KB.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.UPD_DATE.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.UPD_TIME.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.UNCHIN_INV_FROM.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SAGYO_INV_FROM.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.YOKOMOCHI_INV_FROM.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_DEL_FLG.ColNo, lbl)          'ADD 2018/08/10  依頼番号 : 002136  
            .SetCellStyle(0, sprDetailDef.SAP_OUT_USER.ColNo, lbl)

            '明細検索行の初期化
            Call Me.SetInitValue()

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprMeisai

        With spr

            .SuspendLayout()

            'データ挿入
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            '列設定用変数
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim lblL As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            Dim lblC As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim num As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -9999999999999, 9999999999999, True, 0, , ",")

            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                '*****表示列*****
                .SetCellStyle(i, LMG040G.sprDetailDef.DEF.ColNo, def)
                .SetCellStyle(i, LMG040G.sprDetailDef.SEIKYU_NO.ColNo, lblL)
                .SetCellStyle(i, LMG040G.sprDetailDef.SAP_NO.ColNo, lblL)
                .SetCellStyle(i, LMG040G.sprDetailDef.SEIKYU_CD.ColNo, lblL)
                .SetCellStyle(i, LMG040G.sprDetailDef.SEIKYU_NM.ColNo, lblL)
                .SetCellStyle(i, LMG040G.sprDetailDef.SEIKYU_CAL.ColNo, num)
                .SetCellStyle(i, LMG040G.sprDetailDef.SEIKYUSYO_DATE.ColNo, lblL)
                .SetCellStyle(i, LMG040G.sprDetailDef.KAKUTEI_ZUMI_CHK.ColNo, lblC)
                .SetCellStyle(i, LMG040G.sprDetailDef.PRINT_ZUMI_CHK.ColNo, lblC)
                .SetCellStyle(i, LMG040G.sprDetailDef.KEIRI_TORIKOMI_CHK.ColNo, lblC)
                .SetCellStyle(i, LMG040G.sprDetailDef.KEIRI_TORIKOMI_TAISHOGAI_CHK.ColNo, lblC)
                .SetCellStyle(i, LMG040G.sprDetailDef.SKYU_CSV_CHK.ColNo, lblC)
                .SetCellStyle(i, LMG040G.sprDetailDef.CREATE_SYUBETU_NM.ColNo, lblL)
                .SetCellStyle(i, LMG040G.sprDetailDef.AKAKURO_NM.ColNo, lblL)
                .SetCellStyle(i, LMG040G.sprDetailDef.SEIKYU_NO_RELATED.ColNo, lblL)
                .SetCellStyle(i, LMG040G.sprDetailDef.SAP_OUT_USER_NM.ColNo, lblL)
                '*****隠し列*****
                .SetCellStyle(i, LMG040G.sprDetailDef.CREATE_SYUBETU_KB.ColNo, lblL)
                .SetCellStyle(i, LMG040G.sprDetailDef.AKAKURO_KB.ColNo, lblL)
                .SetCellStyle(i, LMG040G.sprDetailDef.STATE_KB.ColNo, lblL)
                .SetCellStyle(i, LMG040G.sprDetailDef.UPD_DATE.ColNo, lblL)
                .SetCellStyle(i, LMG040G.sprDetailDef.UPD_TIME.ColNo, lblL)
                .SetCellStyle(i, LMG040G.sprDetailDef.UNCHIN_INV_FROM.ColNo, lblL)
                .SetCellStyle(i, LMG040G.sprDetailDef.SAGYO_INV_FROM.ColNo, lblL)
                .SetCellStyle(i, LMG040G.sprDetailDef.YOKOMOCHI_INV_FROM.ColNo, lblL)
                .SetCellStyle(i, LMG040G.sprDetailDef.SYS_DEL_FLG.ColNo, lblL)          'ADD 2018/08/10 依頼番号 : 002136  
                .SetCellStyle(i, LMG040G.sprDetailDef.SAP_OUT_USER.ColNo, lblL)

                'セル値設定
                '*****表示列*****
                .SetCellValue(i, LMG040G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMG040G.sprDetailDef.SEIKYU_NO.ColNo, dr.Item("SKYU_NO").ToString())
                .SetCellValue(i, LMG040G.sprDetailDef.SAP_NO.ColNo, dr.Item("SAP_NO").ToString())
                .SetCellValue(i, LMG040G.sprDetailDef.SEIKYU_CD.ColNo, dr.Item("SEIQTO_CD").ToString())
                .SetCellValue(i, LMG040G.sprDetailDef.SEIKYU_NM.ColNo, dr.Item("SEIQTO_NM").ToString())
                'If Replace(dr.Item("SEIQTO_SOGAKU").ToString(), "-", "").Length > 10 Then
                '.SetCellValue(i, LMG040G.sprDetailDef.SEIKYU_CAL.ColNo, "9999999999")
                'Else
                .SetCellValue(i, LMG040G.sprDetailDef.SEIKYU_CAL.ColNo, dr.Item("SEIQTO_SOGAKU").ToString())
                'End If

                .SetCellValue(i, LMG040G.sprDetailDef.SEIKYUSYO_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("SKYU_DATE").ToString()))
                If dr.Item("KAKUTEI_FLG").ToString().Equals(LMConst.FLG.ON) Then
                    .SetCellValue(i, LMG040G.sprDetailDef.KAKUTEI_ZUMI_CHK.ColNo, "○")
                Else
                    .SetCellValue(i, LMG040G.sprDetailDef.KAKUTEI_ZUMI_CHK.ColNo, "")
                End If
                If dr.Item("PRINT_FLG").ToString().Equals(LMConst.FLG.ON) Then
                    .SetCellValue(i, LMG040G.sprDetailDef.PRINT_ZUMI_CHK.ColNo, "○")
                Else
                    .SetCellValue(i, LMG040G.sprDetailDef.PRINT_ZUMI_CHK.ColNo, "")
                End If
                If dr.Item("KEIRITORIKOMI_FLG").ToString().Equals(LMConst.FLG.ON) Then
                    .SetCellValue(i, LMG040G.sprDetailDef.KEIRI_TORIKOMI_CHK.ColNo, "○")
                Else
                    .SetCellValue(i, LMG040G.sprDetailDef.KEIRI_TORIKOMI_CHK.ColNo, "")
                End If
                If dr.Item("TAISHOGAI_FLG").ToString().Equals(LMConst.FLG.ON) Then
                    .SetCellValue(i, LMG040G.sprDetailDef.KEIRI_TORIKOMI_TAISHOGAI_CHK.ColNo, "○")
                Else
                    .SetCellValue(i, LMG040G.sprDetailDef.KEIRI_TORIKOMI_TAISHOGAI_CHK.ColNo, "")
                End If
                If dr.Item("SKYU_CSV_FLG").ToString().Equals(LMConst.FLG.ON) Then
                    .SetCellValue(i, LMG040G.sprDetailDef.SKYU_CSV_CHK.ColNo, "○")
                Else
                    .SetCellValue(i, LMG040G.sprDetailDef.SKYU_CSV_CHK.ColNo, "")
                End If
                .SetCellValue(i, LMG040G.sprDetailDef.SAP_OUT_USER_NM.ColNo, dr.Item("SAP_OUT_USER_NM").ToString())

                .SetCellValue(i, LMG040G.sprDetailDef.CREATE_SYUBETU_NM.ColNo, dr.Item("CRT_KB_NM").ToString())
                .SetCellValue(i, LMG040G.sprDetailDef.AKAKURO_NM.ColNo, dr.Item("RB_FLG_NM").ToString())
                .SetCellValue(i, LMG040G.sprDetailDef.SEIKYU_NO_RELATED.ColNo, dr.Item("SKYU_NO_RELATED").ToString())
                '*****隠し列*****
                .SetCellValue(i, LMG040G.sprDetailDef.CREATE_SYUBETU_KB.ColNo, dr.Item("CRT_KB").ToString())
                .SetCellValue(i, LMG040G.sprDetailDef.AKAKURO_KB.ColNo, dr.Item("RB_FLG").ToString())
                .SetCellValue(i, LMG040G.sprDetailDef.STATE_KB.ColNo, dr.Item("STATE_KB").ToString())
                .SetCellValue(i, LMG040G.sprDetailDef.UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMG040G.sprDetailDef.UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMG040G.sprDetailDef.UNCHIN_INV_FROM.ColNo, dr.Item("UNCHIN_IMP_FROM_DATE").ToString())
                .SetCellValue(i, LMG040G.sprDetailDef.SAGYO_INV_FROM.ColNo, dr.Item("SAGYO_IMP_FROM_DATE").ToString())
                .SetCellValue(i, LMG040G.sprDetailDef.YOKOMOCHI_INV_FROM.ColNo, dr.Item("YOKOMOCHI_IMP_FROM_DATE").ToString())
                .SetCellValue(i, LMG040G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())         'ADD 2018/18/10 依頼番号 : 002136  
                .SetCellValue(i, LMG040G.sprDetailDef.SAP_OUT_USER.ColNo, dr.Item("SAP_OUT_USER").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInitValue()

        With Me._Frm.sprMeisai.ActiveSheet

            '*****表示列*****
            .Cells(0, LMG040G.sprDetailDef.DEF.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.SEIKYU_NO.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.SAP_NO.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.SEIKYU_CD.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.SEIKYU_NM.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.SEIKYU_CAL.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.SEIKYUSYO_DATE.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.KAKUTEI_ZUMI_CHK.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.PRINT_ZUMI_CHK.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.KEIRI_TORIKOMI_CHK.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.KEIRI_TORIKOMI_TAISHOGAI_CHK.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.SKYU_CSV_CHK.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.CREATE_SYUBETU_NM.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.AKAKURO_NM.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.SEIKYU_NO_RELATED.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.SAP_OUT_USER_NM.ColNo).Value = String.Empty
            '*****隠し列*****
            .Cells(0, LMG040G.sprDetailDef.CREATE_SYUBETU_KB.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.AKAKURO_KB.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.STATE_KB.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.UPD_DATE.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.UPD_TIME.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.UNCHIN_INV_FROM.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.SAGYO_INV_FROM.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.YOKOMOCHI_INV_FROM.ColNo).Value = String.Empty
            .Cells(0, LMG040G.sprDetailDef.SYS_DEL_FLG.ColNo).Value = String.Empty          'ADD 2018/08/10 依頼番号 : 002136  
            .Cells(0, LMG040G.sprDetailDef.SAP_OUT_USER.ColNo).Value = String.Empty

        End With

    End Sub

#End Region

#End Region

End Class
