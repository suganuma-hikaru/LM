' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG060G : 請求印刷指示
'  作  成  者       :  [菱刈]
' ==========================================================================
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Base
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMG060Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMG060G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMG060F

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMGConG As LMGControlG


#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMG060F)

        '親クラスのコンストラクタを呼ぶ。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付る。
        MyBase.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付ける。
        MyBase.MyForm = frm

        Me._Frm = frm

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

        Dim empty As String = String.Empty

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = empty
            .F2ButtonName = empty
            .F3ButtonName = empty
            .F4ButtonName = empty
            .F5ButtonName = empty
            .F6ButtonName = empty
            .F7ButtonName = LMGControlC.FUNCTION_INSATU
            .F8ButtonName = empty
            '(2013.02.14)要望番号1832 検索ボタン追加 -- START --
            '.F9ButtonName = empty
            .F9ButtonName = LMGControlC.FUNCTION_KENSAKU
            '(2013.02.14)要望番号1832 検索ボタン追加 --  END  --
            .F10ButtonName = LMGControlC.FUNCTION_MST_SANSHO
            .F11ButtonName = empty
            .F12ButtonName = LMGControlC.FUNCTION_TOJIRU

            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = always
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = lock
            .F10ButtonEnabled = always
            .F11ButtonEnabled = lock
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

            .cmbPrint.TabIndex = LMG060C.CtlTabIndex.Print
            .cmbBr.TabIndex = LMG060C.CtlTabIndex.Br
            .txtCustCdL.TabIndex = LMG060C.CtlTabIndex.CustCdL
            .lblCustNmL.TabIndex = LMG060C.CtlTabIndex.CustNmL
            .txtCustCdM.TabIndex = LMG060C.CtlTabIndex.CustCdM
            .lblCustNmM.TabIndex = LMG060C.CtlTabIndex.CustNmM
            .txtSeiqCd.TabIndex = LMG060C.CtlTabIndex.SeiqCd
            .lblSeiqNm.TabIndex = LMG060C.CtlTabIndex.SeiqNm
            .imdOutkaDateFrom.TabIndex = LMG060C.CtlTabIndex.OutkaDateFrom
            .imdOutkaDateTo.TabIndex = LMG060C.CtlTabIndex.OutkaDateTo
            .cmbCloseKbNm.TabIndex = LMG060C.CtlTabIndex.CloseKb

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal data As String)

        '編集部の項目をクリア
        Call Me.ClearControl()

        '自営業所の設定、日付の当月日付1日目、当月日付最終日の設定
        Call Me.SetInput(data)


    End Sub

#Region "コントロールの初期設定"
    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControl()

        With Me._Frm

            .cmbPrint.SelectedValue = Nothing
            .cmbBr.SelectedValue = Nothing
            .txtCustCdL.TextValue = String.Empty
            .lblCustNmL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNmM.TextValue = String.Empty
            .imdOutkaDateFrom.TextValue = String.Empty
            .imdOutkaDateTo.TextValue = String.Empty
            .cmbCloseKbNm.SelectedValue = Nothing


        End With

    End Sub

    ''' <summary>
    '''営業所、日付の当月の1日目、当月の最終日の設定 
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SetInput(ByVal data As String)
        With Me._Frm
            .cmbBr.SelectedValue = LMUserInfoManager.GetNrsBrCd()

            '2014.08.04 FFEM高取対応 START
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

            If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
                .cmbBr.ReadOnly = True
            Else
                .cmbBr.ReadOnly = False
            End If
            '2014.08.04 FFEM高取対応 END

            '当月日付1日目の取得
            Dim nowDate As String = String.Concat(Convert.ToDateTime(DateFormatUtility.EditSlash(data)).ToString("yyyyMM"), "01")

            Dim nextDate As DateTime = Convert.ToDateTime(DateFormatUtility.EditSlash(nowDate))

            .imdOutkaDateFrom.TextValue = nowDate

            '当月の最終日取得
            .imdOutkaDateTo.TextValue = nextDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd")
        End With

    End Sub

    'START YANAI 要望番号582
    ''' <summary>
    '''荷主・日付の設定 
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SetPrmData(ByVal prmDs As DataSet)

        With Me._Frm
            If 0 < prmDs.Tables(LMControlC.LMG060C_TABLE_NM_IN).Rows.Count Then
                .txtCustCdL.TextValue = prmDs.Tables(LMControlC.LMG060C_TABLE_NM_IN).Rows(0).Item("CUST_CD_L").ToString()
                .txtCustCdM.TextValue = prmDs.Tables(LMControlC.LMG060C_TABLE_NM_IN).Rows(0).Item("CUST_CD_M").ToString()
                If String.IsNullOrEmpty(prmDs.Tables(LMControlC.LMG060C_TABLE_NM_IN).Rows(0).Item("F_DATE").ToString()) = False Then
                    .imdOutkaDateFrom.TextValue = prmDs.Tables(LMControlC.LMG060C_TABLE_NM_IN).Rows(0).Item("F_DATE").ToString()
                End If
                If String.IsNullOrEmpty(prmDs.Tables(LMControlC.LMG060C_TABLE_NM_IN).Rows(0).Item("T_DATE").ToString()) = False Then
                    .imdOutkaDateTo.TextValue = prmDs.Tables(LMControlC.LMG060C_TABLE_NM_IN).Rows(0).Item("T_DATE").ToString()
                End If

                '名称を取得し、ラベルに表示を行う
                Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("NRS_BR_CD = '", .cmbBr.SelectedValue.ToString(), "' AND " _
                                                                                                            , "CUST_CD_L = '", .txtCustCdL.TextValue, "' AND " _
                                                                                                            , "CUST_CD_M = '", .txtCustCdM.TextValue, "'" _
                                                                                               ))

                'Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                '                                                                              "CUST_CD_L = '", .txtCustCdL.TextValue, "' AND " _
                '                                                                            , "CUST_CD_M = '", .txtCustCdM.TextValue, "'" _
                '                                                               ))

                If 0 < dr.Length Then
                    .lblCustNmL.TextValue = dr(0).Item("CUST_NM_L").ToString()
                    .lblCustNmM.TextValue = dr(0).Item("CUST_NM_M").ToString()
                End If

            End If
        End With

    End Sub
    'END YANAI 要望番号582

#End Region


    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey()

    End Sub

    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm
            .cmbPrint.Focus()
        End With
    End Sub

    'START YANAI 要望番号582
    ''' <summary>
    ''' 運賃検索から遷移時のみ初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlPrm(ByVal prmDs As DataSet, ByVal rootPGID As String)

        '運賃検索から遷移時のみ初期値設定
        If (LMG060C.PGID_LMF040).Equals(rootPGID) = True Then
            Call Me.SetPrmData(prmDs)
        End If

    End Sub
    'END YANAI 要望番号582

#End Region

#Region "印刷区分変更時"

    ''' <summary>
    ''' 印刷区分値変更のロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub Locktairff(ByVal frm As LMG060F)

        With Me._Frm

            Dim lockflgCust As Boolean = False
            Dim lockflgSiqto As Boolean = False
            Dim lockflgdata As Boolean = False
            '(2013.02.14)要望番号1834 連続印刷対応 -- START --
            Dim lockflgCombo As Boolean = False
            Dim lockflgF9 As Boolean = False
            Dim lockSpr As Boolean = True
            '(2013.02.14)要望番号1834 連続印刷対応 --  END  --
            '2013.02.27 / Notes1774 開始
            Dim lockflgCloKb As Boolean = True
            '2013.02.27 / Notes1774 終了


            '印刷区分
            Dim PrintKb As String = .cmbPrint.SelectedValue.ToString

            '印刷種別が運賃チェックリストの場合
            If LMG060C.PRINT_UNCHIN_CHECK.Equals(PrintKb) = True Then

                '請求先コードをロック
                lockflgSiqto = True

                'クリアするもの
                .txtSeiqCd.TextValue = String.Empty
                .lblSeiqNm.TextValue = String.Empty
            End If

            '(2013.02.14)要望番号1834 連続印刷対応 -- START --
            '印刷種別が運賃請求明細書(連続)の場合
            If LMG060C.PRINT_UNCHIN_RENZOKU.Equals(PrintKb) = True Then

                '荷主コード・請求先コードをロック
                lockflgCust = True
                lockflgSiqto = True
                lockSpr = False
                'F9検索ボタン活性化
                lockflgF9 = True

                '値のクリア
                .txtCustCdL.TextValue = String.Empty    '荷主コード(大)
                .lblCustNmL.TextValue = String.Empty    '荷主名(大)
                .txtCustCdM.TextValue = String.Empty    '荷主コード(中)
                .lblCustNmM.TextValue = String.Empty    '荷主名(中)
                .txtSeiqCd.TextValue = String.Empty     '請求先コード
                .lblSeiqNm.TextValue = String.Empty     '請求先名
            End If
            '(2013.02.14)要望番号1834 連続印刷対応 --  END  --

            '2013.02.27 / Notes1774 開始
            '印刷種別が運賃請求明細書の場合
            If LMG060C.PRINT_UNCHIN_SEIKYU.Equals(PrintKb) = True Then
                '締日区分コンボ活性化
                lockflgCloKb = False
            End If

            '請求明細書でない場合は締日コンボを常にクリア
            If LMG060C.PRINT_UNCHIN_SEIKYU.Equals(PrintKb) = False Then
                .cmbCloseKbNm.SelectedValue = Nothing
            End If
            '2013.02.27 / Notes1774 終了

            '(2013.02.14)要望番号1834 連続印刷対応 -- START --
            .FunctionKey.F9ButtonEnabled = lockflgF9
            'Me.SetLockControl(.cmbPrint, lockflgCust)
            Me.SetLockControl(.cmbPrint, lockflgCombo)
            '(2013.02.14)要望番号1834 連続印刷対応 --  END  --
            Me.SetLockControl(.txtCustCdL, lockflgCust)
            Me.SetLockControl(.txtCustCdM, lockflgCust)
            Me.SetLockControl(.txtSeiqCd, lockflgSiqto)
            Me.SetLockControl(.imdOutkaDateFrom, lockflgdata)
            Me.SetLockControl(.imdOutkaDateTo, lockflgdata)
            '2013.02.27 / Notes1774 開始
            Me.SetLockControl(.cmbCloseKbNm, lockflgCloKb)
            '2013.02.27 / Notes1774 終了
            Me.InitSpread(lockSpr)

        End With

    End Sub

#End Region

#End Region

#Region "Spread"

    '(2013.02.14)要望番号1832 荷主SPREAD追加 -- START --
    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMG060C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMG060C.SprColumnIndex.CUST_NM_L, "荷主名(大)", 324, True)
        Public Shared CUST_NM_M As SpreadColProperty = New SpreadColProperty(LMG060C.SprColumnIndex.CUST_NM_M, "荷主名(中)", 190, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMG060C.SprColumnIndex.CUST_CD_L, "荷主コード" & vbCrLf & "(大)", 90, True)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMG060C.SprColumnIndex.CUST_CD_M, "荷主コード" & vbCrLf & "(中)", 90, True)
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMG060C.SprColumnIndex.ROW_INDEX, "行番号", 10, False)
        Public Shared CLOSE_KB As SpreadColProperty = New SpreadColProperty(LMG060C.SprColumnIndex.CLOSE_KB, "締日区分", 80, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread(Optional ByVal lock As Boolean = True)

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            .sprDetail.ActiveSheet.ColumnCount = 7

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprSagyo.SetColProperty(New sprDetailDef)
            .sprDetail.SetColProperty(New LMG060G.sprDetailDef(), False)
            '2015.10.15 英語化対応END

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New sprDetailDef)

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMG060G.sprDetailDef.DEF.ColNo + 1

            '列設定
            '(2013.02.26)要望番号1835 --  START  --
            .sprDetail.SetCellStyle(0, LMG060G.sprDetailDef.CUST_NM_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, lock))
            .sprDetail.SetCellStyle(0, LMG060G.sprDetailDef.CUST_NM_M.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, lock))
            .sprDetail.SetCellStyle(0, LMG060G.sprDetailDef.CUST_CD_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 5, lock))
            .sprDetail.SetCellStyle(0, LMG060G.sprDetailDef.CUST_CD_M.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 2, lock))
            '(2013.02.26)要望番号1835 --  END  --
            .sprDetail.SetCellStyle(0, LMG060G.sprDetailDef.CLOSE_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMG060G.sprDetailDef.ROW_INDEX.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))

        End With

    End Sub

    ''' <summary>
    ''' SPREAD初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()

        With _Frm.sprDetail.ActiveSheet

            .Cells(0, LMG060G.sprDetailDef.DEF.ColNo).Value = String.Empty
            .Cells(0, LMG060G.sprDetailDef.CUST_NM_L.ColNo).Value = String.Empty
            .Cells(0, LMG060G.sprDetailDef.CUST_NM_M.ColNo).Value = String.Empty
            .Cells(0, LMG060G.sprDetailDef.CUST_CD_L.ColNo).Value = String.Empty
            .Cells(0, LMG060G.sprDetailDef.CUST_CD_M.ColNo).Value = String.Empty
            .Cells(0, LMG060G.sprDetailDef.CLOSE_KB.ColNo).Value = String.Empty
            .Cells(0, LMG060G.sprDetailDef.ROW_INDEX.ColNo).Value = "0"

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As DataSet = New DataSet()

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()
            .SuspendLayout()

            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            Dim dRow As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dRow = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprDetailDef.CUST_NM_L.ColNo, sLabel)  '荷主名(大)
                .SetCellStyle(i, sprDetailDef.CUST_NM_M.ColNo, sLabel)  '荷主名(中)
                .SetCellStyle(i, sprDetailDef.CUST_CD_L.ColNo, sLabel)  '荷主コード(大)
                .SetCellStyle(i, sprDetailDef.CUST_CD_M.ColNo, sLabel)  '荷主コード(中)
                .SetCellStyle(i, sprDetailDef.CLOSE_KB.ColNo, sLabel)   '締日区分
                .SetCellStyle(i, sprDetailDef.ROW_INDEX.ColNo, sLabel)  '行番号

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.CUST_NM_L.ColNo, dRow.Item("CUST_NM_L").ToString)
                .SetCellValue(i, sprDetailDef.CUST_NM_M.ColNo, dRow.Item("CUST_NM_M").ToString)
                .SetCellValue(i, sprDetailDef.CUST_CD_L.ColNo, dRow.Item("CUST_CD_L").ToString)
                .SetCellValue(i, sprDetailDef.CUST_CD_M.ColNo, dRow.Item("CUST_CD_M").ToString)
                .SetCellValue(i, sprDetailDef.CLOSE_KB.ColNo, dRow.Item("CLOSE_KB_NM").ToString)
                .SetCellValue(i, sprDetailDef.ROW_INDEX.ColNo, Convert.ToString(i - 1))
            Next

            .ResumeLayout(True)

        End With

    End Sub
    '(2013.02.14)要望番号1832 荷主SPREAD追加 --  END  --

#End Region

#Region "部品化検討中"

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <param name="ctl">書式設定を行うコントロール</param>
    ''' <remarks></remarks>
    Friend Sub SetDateFormat(ByVal ctl As LMImDate)

        '**********TODO:削除予定　日付コントロールの型設定がある場合のみ使用*********************
        'EX)
        'ctl.Format = DateFieldsBuilder.BuildFields("ddMMMyyyy")
        'ctl.DisplayFormat = DateDisplayFieldsBuilder.BuildFields("dd/MMM/yyyy")

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
