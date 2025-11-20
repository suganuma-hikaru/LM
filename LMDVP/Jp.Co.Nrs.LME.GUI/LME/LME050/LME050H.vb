' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME050  : 作業個数引当
'  作  成  者       :  YANAI
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LME050ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LME050H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LME050V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LME050G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMEconV As LMEControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMEconH As LMEControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMEconG As LMEControlG

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    '''検索結果格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _SelectZaiko As DataTable

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Prm As LMFormData

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

    ''' <summary>
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

    ''' <summary>
    ''' Leave処理を行うかのフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _LeaveFlg As String = LMConst.FLG.ON

    ''' <summary>
    ''' 選択押下処理フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _SelectFlg As String = LMConst.FLG.OFF

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        Me._Prm = prm

        '画面間データを取得する
        Me._PrmDs = prm.ParamDataSet()

        'フォームの作成
        Dim frm As LME050F = New LME050F(Me)

        '画面共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMEconG = New LMEControlG(DirectCast(frm, Form))

        'Validate共通クラスの設定
        Me._LMEconV = New LMEControlV(Me, DirectCast(frm, Form))

        'Hnadler共通クラスの設定
        Me._LMEconH = New LMEControlH(DirectCast(frm, Form))

        'Gamenクラスの設定
        Me._G = New LME050G(Me, frm)

        'Validateクラスの設定
        Me._V = New LME050V(Me, frm, Me._LMEconV)

        'フォームの初期化
        MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Me._G.SetFunctionKey()

        'タブインデックスの設定
        Me._G.SetTabIndex()

        'コントロール個別設定
        Me._G.SetControl(MyBase.GetPGID())

        Me._G.SetInitValue(frm)

        'INの値を画面に表示
        Me._G.SetInitForm(frm, Me._PrmDs)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Me._G.InitSpread()

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

        '検索処理を行う
        Me.SelectData(frm, LME050C.NEW_MODE, LME050C.EventShubetsu.KENSAKU, Me._PrmDs)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        '呼び出し元画面情報を設定
        frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

        'フォームの表示
        frm.ShowDialog()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 検索以外のイベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LME050C.EventShubetsu, ByVal frm As LME050F)

        'チェックリスト格納変数
        Dim list As ArrayList = New ArrayList()

        '権限チェック
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        'ダブルクリックの場合、検索行をクリックした場合は、処理を行わない
        If LME050C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) Then
            'クリックした行が検索行の場合
            If 0.Equals(frm.sprZaiko.Sheets(0).ActiveRow.Index()) = True Then
                Exit Sub
            End If
        End If

        Select Case eventShubetsu

            Case LME050C.EventShubetsu.KENSAKU  '検索

                '処理開始アクション
                Me._LMEconH.StartAction(frm)

                'LeaveフラグをOffに設定
                Me._LeaveFlg = LMConst.FLG.OFF

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMEconH.EndAction(frm)

                    'LeaveフラグをOnに設定
                    Me._LeaveFlg = LMConst.FLG.ON
                    Exit Sub

                End If

                'ファンクションキーの設定
                Me._G.SetFunctionKey()

                '検索処理を行う
                Me.SelectData(frm, LME050C.NEW_MODE, LME050C.EventShubetsu.KENSAKU, Me._PrmDs)

                'フォーカスの設定
                Me._G.SetFoucus()

                '処理終了アクション
                Me._LMEconH.EndAction(frm)

                '画面の入力項目の制御（ここで行わないと全項目がロック解除されてしまうため）
                Me._G.SetControlsStatus()

                'LeaveフラグをOnに設定
                Me._LeaveFlg = LMConst.FLG.ON

            Case LME050C.EventShubetsu.SENTAKU  '選択

                Me._SelectFlg = LMConst.FLG.ON

                '処理開始アクション
                Me._LMEconH.StartAction(frm)

                '個数の計算をする
                Dim calsumFlg As Boolean = True
                Me._G.SetCalSum(LME050C.EventShubetsu.CAL_KONSU)

                If calsumFlg = False Then

                    Me._SelectFlg = LMConst.FLG.OFF

                    Exit Sub
                End If

                '引当個数・数量ALLゼロチェック
                If Me._V.IsAllZeroChk() = True Then
                    'ALLゼロ時、上から順に引当個数・数量を設定していく。
                    Me._G.SetAllZero()

                End If

                '引当個数、引当数量の計算をする
                Me._G.SetHikiSum(eventShubetsu)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu) = False Then

                    Me._SelectFlg = LMConst.FLG.OFF

                    '処理終了アクション
                    Me._LMEconH.EndAction(frm)
                    Exit Sub
                End If

                '選択処理を行う
                Dim rtnDs As DataSet = Me.OutDataSet(frm)

                If rtnDs Is Nothing = False Then

                    'outのパラメータをセット
                    Me._Prm.ParamDataSet = rtnDs

                    Me._SelectFlg = LMConst.FLG.OFF

                    '画面を閉じる
                    frm.Close()
                    Exit Sub

                End If

                Me._SelectFlg = LMConst.FLG.OFF

                '処理終了アクション
                Me._LMEconH.EndAction(frm)

                'START YANAI 要望番号1090 指摘修正
                'Case LME050C.EventShubetsu.CAL_KONSU, LME050C.EventShubetsu.CAL_SURYO  '梱数・端数・数量変更
            Case LME050C.EventShubetsu.CAL_KONSU, LME050C.EventShubetsu.CAL_SURYO, LME050C.EventShubetsu.CAL_IRIME  '梱数・端数・数量・入目変更
                'END YANAI 要望番号1090 指摘修正

                'フラグ判定
                If LMConst.FLG.ON.Equals(Me._LeaveFlg) = False Then
                    Exit Sub
                End If

                'メッセージのクリア
                MyBase.ClearMessageAria(DirectCast(frm, Jp.Co.Nrs.LM.GUI.Win.Interface.ILMForm))

                '個数の計算をする
                Dim calsumFlg As Boolean = Me._G.SetCalSum(eventShubetsu)
                If calsumFlg = False Then
                    Exit Sub
                End If

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMEconH.EndAction(frm)
                    Exit Sub
                End If

            Case LME050C.EventShubetsu.CHANGE_SPREAD    'スプレッドの引当個数、引当数量変更

                If (Me._SelectFlg).Equals(LMConst.FLG.ON) = True Then
                    Exit Sub
                End If

                'メッセージのクリア
                MyBase.ClearMessageAria(frm)

                'チェックオンオフ
                Me._G.SetCheckOnOff()

                '引当個数、引当数量の計算をする
                Me._G.SetHikiSum(eventShubetsu)

        End Select

        'デフォルトのメッセージを設定
        Call Me.SetDefMessage(frm)

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LME050F, ByVal reFlg As String, ByVal eventShubetsu As LME050C.EventShubetsu, ByVal prmDs As DataSet)

        '閾値の取得
        Dim dr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0)

        'DataSet設定
        Dim rtDs As DataSet = New LME050DS()
        Me.SetDataSetInData(frm, rtDs)
        
        'SPREAD(表示行)初期化
        frm.sprZaiko.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Nothing
        rtnDs = Me._LMEconH.CallWSAAction(DirectCast(frm, Form), _
                                                          "LME050BLF", "SelectListData", rtDs, Convert.ToInt32(Convert.ToDecimal(dr.Item("VALUE1"))))


        '検索成功時共通処理を行う
        If rtnDs Is Nothing = False Then
            Me._SelectZaiko = rtnDs.Tables(LME050C.TABLE_NM_OUTZAI)
            Me.SuccessSelect(frm, rtnDs, reFlg, prmDs, eventShubetsu)
        Else
            MyBase.ShowMessage(frm, "G001")
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

    End Sub

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LME050F, ByVal rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LME050C.TABLE_NM_IN).NewRow()

        '検索条件　入力部（単項目）
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("WH_CD") = frm.cmbSoko.SelectedValue
        dr("CUST_CD_L") = frm.lblCustCD_L.TextValue
        dr("CUST_CD_M") = frm.lblCustCD_M.TextValue
        dr("GOODS_CD_NRS") = frm.lblGoodsNRS.TextValue
        dr("GOODS_CD_CUST") = frm.lblGoodsCD.TextValue
        dr("SERIAL_NO") = frm.txtSerialNO.TextValue
        dr("RSV_NO") = frm.txtRsvNO.TextValue
        dr("LOT_NO") = frm.txtLotNO.TextValue
        dr("IRIME") = frm.numIrime.TextValue
        dr("GOODS_NM") = frm.lblGoodsNM.TextValue

        '検索条件　入力部（スプレッド）
        With frm.sprZaiko.ActiveSheet
            dr("TOU_NO") = Me._LMEconV.GetCellValue(.Cells(0, LME050G.sprZaiko.TOU_NO.ColNo))
            dr("SITU_NO") = Me._LMEconV.GetCellValue(.Cells(0, LME050G.sprZaiko.SHITSU_NO.ColNo))
            dr("ZONE_CD") = Me._LMEconV.GetCellValue(.Cells(0, LME050G.sprZaiko.ZONE_CD.ColNo))
            dr("LOCA") = Me._LMEconV.GetCellValue(.Cells(0, LME050G.sprZaiko.LOCA.ColNo))
            dr("GOODS_COND_KB_1") = Me._LMEconV.GetCellValue(.Cells(0, LME050G.sprZaiko.NAKAMI.ColNo))
            dr("GOODS_COND_KB_2") = Me._LMEconV.GetCellValue(.Cells(0, LME050G.sprZaiko.GAIKAN.ColNo))
            dr("GOODS_COND_KB_3") = Me._LMEconV.GetCellValue(.Cells(0, LME050G.sprZaiko.CUST_STATUS.ColNo))
            dr("REMARK") = Me._LMEconV.GetCellValue(.Cells(0, LME050G.sprZaiko.REMARK.ColNo))
            dr("OFB_KB") = Me._LMEconV.GetCellValue(.Cells(0, LME050G.sprZaiko.OFB_KBN.ColNo))
            dr("SPD_KB") = Me._LMEconV.GetCellValue(.Cells(0, LME050G.sprZaiko.SPD_KBN_S.ColNo))
            dr("REMARK_OUT") = Me._LMEconV.GetCellValue(.Cells(0, LME050G.sprZaiko.REMARK_OUT.ColNo))
            dr("TAX_KB") = Me._LMEconV.GetCellValue(.Cells(0, LME050G.sprZaiko.TAX_KB.ColNo))
            dr("HIKIATE_ALERT_YN") = Me._LMEconV.GetCellValue(.Cells(0, LME050G.sprZaiko.HIKIATE_ALERT_NM.ColNo))
            dr("DEST_NM") = Me._LMEconV.GetCellValue(.Cells(0, LME050G.sprZaiko.DEST_NM.ColNo))
            dr("ZAI_REC_NO") = Me._LMEconV.GetCellValue(.Cells(0, LME050G.sprZaiko.ZAI_REC_NO.ColNo))
        End With

        '検索条件をデータセットに設定
        rtDs.Tables(LME050C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理（画面別）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LME050F, ByVal ds As DataSet, ByVal reFlg As String, ByVal prmDs As DataSet, ByVal eventShubetsu As LME050C.EventShubetsu)

        Dim dt As DataTable = ds.Tables(LME050C.TABLE_NM_OUTZAI)

        '画面解除
        MyBase.UnLockedControls(frm)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Me._G.InitSpread()

        '取得データをSPREADに表示
        Me._G.SetSpread(frm, dt, prmDs)

        'ファンクションキーの設定
        Me._G.SetFunctionKey()

        '個数・数量の計算
        Dim calsumFlg As Boolean = Me._G.SetCalSum(LME050C.EventShubetsu.KENSAKU)

        Me._CntSelect = Convert.ToString(frm.sprZaiko.ActiveSheet.Rows.Count - 1)

        'メッセージエリアの設定
        If LME050C.NEW_MODE.Equals(reFlg) = True Then
            If ("0").Equals(Me._CntSelect) = True Then
                MyBase.ShowMessage(frm, "G001")
            Else
                MyBase.ShowMessage(frm, "G016", New String() {_CntSelect})
            End If
        End If

    End Sub

    ''' <summary>
    ''' 選択
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function OutDataSet(ByVal frm As LME050F) As DataSet

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me._V.getCheckList()
        Dim lngcnt As Integer = Me._ChkList.Count() - 1

        Dim rtDs As DataSet = New LME050DS()
        Dim dt As DataTable = rtDs.Tables(LME050C.TABLE_NM_OUT)
        Dim outDr As DataRow = dt.NewRow()

        '値設定
        For i As Integer = 0 To lngcnt
            With frm.sprZaiko.ActiveSheet

                If ("0").Equals(Me._LMEconV.GetCellValue(frm.sprZaiko.ActiveSheet.Cells(Convert.ToInt32(Me._ChkList(i)), LME050G.sprZaiko.HIKI_CNT.ColNo))) = True AndAlso _
                    (LME050C.PLUS_ZERO).Equals(Me._LMEconV.GetCellValue(frm.sprZaiko.ActiveSheet.Cells(Convert.ToInt32(Me._ChkList(i)), LME050G.sprZaiko.HIKI_AMT.ColNo))) = True  Then
                    Continue For
                End If

                outDr = dt.NewRow()

                Dim setRows() As DataRow = _SelectZaiko.Select(String.Concat("ZAI_REC_NO = '", Me._LMEconV.GetCellValue(.Cells(Convert.ToInt32(Me._ChkList(i)), LME050G.sprZaiko.ZAI_REC_NO.ColNo)), "'"))

                outDr("ZAI_REC_NO") = setRows(0)(LME050C.DsOutZaiColumnIndex.ZAI_REC_NO).ToString()
                outDr("TOU_NO") = setRows(0)(LME050C.DsOutZaiColumnIndex.TOU_NO).ToString()
                outDr("SITU_NO") = setRows(0)(LME050C.DsOutZaiColumnIndex.SITU_NO).ToString()
                outDr("ZONE_CD") = setRows(0)(LME050C.DsOutZaiColumnIndex.ZONE_CD).ToString()
                outDr("LOCA") = setRows(0)(LME050C.DsOutZaiColumnIndex.LOCA).ToString()
                outDr("LOT_NO") = setRows(0)(LME050C.DsOutZaiColumnIndex.LOT_NO).ToString().ToUpper
                outDr("INKA_NO_L") = setRows(0)(LME050C.DsOutZaiColumnIndex.INKA_NO_L).ToString()
                outDr("INKA_NO_M") = setRows(0)(LME050C.DsOutZaiColumnIndex.INKA_NO_M).ToString()
                outDr("INKA_NO_S") = setRows(0)(LME050C.DsOutZaiColumnIndex.INKA_NO_S).ToString()
                outDr("ALLOC_PRIORITY") = setRows(0)(LME050C.DsOutZaiColumnIndex.ALLOC_PRIORITY).ToString()
                outDr("RSV_NO") = setRows(0)(LME050C.DsOutZaiColumnIndex.RSV_NO).ToString()
                outDr("SERIAL_NO") = setRows(0)(LME050C.DsOutZaiColumnIndex.SERIAL_NO).ToString()
                outDr("HOKAN_YN") = setRows(0)(LME050C.DsOutZaiColumnIndex.HOKAN_YN).ToString()
                outDr("TAX_KB") = setRows(0)(LME050C.DsOutZaiColumnIndex.TAX_KB).ToString()
                outDr("GOODS_COND_KB_1") = setRows(0)(LME050C.DsOutZaiColumnIndex.GOODS_COND_KB_1).ToString()
                outDr("GOODS_COND_KB_2") = setRows(0)(LME050C.DsOutZaiColumnIndex.GOODS_COND_KB_2).ToString()
                outDr("GOODS_COND_KB_3") = setRows(0)(LME050C.DsOutZaiColumnIndex.GOODS_COND_KB_3).ToString()
                outDr("OFB_KB") = setRows(0)(LME050C.DsOutZaiColumnIndex.OFB_KB).ToString()
                outDr("SPD_KB") = setRows(0)(LME050C.DsOutZaiColumnIndex.SPD_KB).ToString()
                outDr("REMARK_OUT") = setRows(0)(LME050C.DsOutZaiColumnIndex.REMARK_OUT).ToString()
                outDr("PORA_ZAI_NB") = setRows(0)(LME050C.DsOutZaiColumnIndex.PORA_ZAI_NB).ToString()
                outDr("ALLOC_CAN_NB") = setRows(0)(LME050C.DsOutZaiColumnIndex.ALLOC_CAN_NB).ToString()
                outDr("IRIME") = setRows(0)(LME050C.DsOutZaiColumnIndex.IRIME).ToString()
                outDr("PORA_ZAI_QT") = setRows(0)(LME050C.DsOutZaiColumnIndex.PORA_ZAI_QT).ToString()
                outDr("ALLOC_CAN_QT") = setRows(0)(LME050C.DsOutZaiColumnIndex.ALLOC_CAN_QT).ToString()
                outDr("INKO_DATE") = setRows(0)(LME050C.DsOutZaiColumnIndex.INKO_DATE).ToString()
                outDr("INKO_PLAN_DATE") = setRows(0)(LME050C.DsOutZaiColumnIndex.INKO_PLAN_DATE).ToString()
                outDr("ZERO_FLAG") = setRows(0)(LME050C.DsOutZaiColumnIndex.ZERO_FLAG).ToString()
                outDr("LT_DATE") = setRows(0)(LME050C.DsOutZaiColumnIndex.LT_DATE).ToString()
                outDr("GOODS_CRT_DATE") = setRows(0)(LME050C.DsOutZaiColumnIndex.GOODS_CRT_DATE).ToString()
                outDr("DEST_CD_P") = setRows(0)(LME050C.DsOutZaiColumnIndex.DEST_CD_P).ToString()
                outDr("REMARK") = setRows(0)(LME050C.DsOutZaiColumnIndex.REMARK).ToString()
                outDr("SMPL_FLAG") = setRows(0)(LME050C.DsOutZaiColumnIndex.SMPL_FLAG).ToString()
                outDr("GOODS_COND_NM_1") = setRows(0)(LME050C.DsOutZaiColumnIndex.GOODS_COND_NM_1).ToString()
                outDr("GOODS_COND_NM_2") = setRows(0)(LME050C.DsOutZaiColumnIndex.GOODS_COND_NM_2).ToString()
                outDr("GOODS_COND_NM_3") = setRows(0)(LME050C.DsOutZaiColumnIndex.GOODS_COND_NM_3).ToString()
                outDr("ALLOC_PRIORITY_NM") = setRows(0)(LME050C.DsOutZaiColumnIndex.ALLOC_PRIORITY_NM).ToString()
                outDr("OFB_KB_NM") = setRows(0)(LME050C.DsOutZaiColumnIndex.OFB_KB_NM).ToString()
                outDr("SPD_KB_NM") = setRows(0)(LME050C.DsOutZaiColumnIndex.SPD_KB_NM).ToString()
                outDr("GOODS_CD_NRS_FROM") = setRows(0)(LME050C.DsOutZaiColumnIndex.GOODS_CD_NRS_FROM).ToString()
                outDr("KONSU") = frm.numSyukkaKosu.TextValue
                outDr("HASU") = frm.numSyukkaHasu.TextValue
                outDr("KOSU") = frm.numSyukkaSouCnt.TextValue
                outDr("SURYO") = frm.numSyukkaSouAmt.TextValue

                '今回引当てる値
                outDr("HIKI_KOSU") = Me._LMEconV.GetCellValue(.Cells(Convert.ToInt32(Me._ChkList(i)), LME050G.sprZaiko.HIKI_CNT.ColNo))
                outDr("HIKI_SURYO") = Me._LMEconV.GetCellValue(.Cells(Convert.ToInt32(Me._ChkList(i)), LME050G.sprZaiko.HIKI_AMT.ColNo))

                outDr("ALCTD_KOSU") = Me._LMEconV.GetCellValue(.Cells(Convert.ToInt32(Me._ChkList(i)), LME050G.sprZaiko.HIKI_KANO_CNT.ColNo))
                outDr("ALCTD_SURYO") = Me._LMEconV.GetCellValue(.Cells(Convert.ToInt32(Me._ChkList(i)), LME050G.sprZaiko.HIKI_KANO_AMT.ColNo))
                outDr("BUYER_ORD_NO_DTL") = setRows(0)(LME050C.DsOutZaiColumnIndex.BUYER_ORD_NO_DTL).ToString()
                outDr("SERIAL_NO_L") = frm.txtSerialNO.TextValue
                outDr("RSV_NO_L") = frm.txtRsvNO.TextValue
                outDr("LOT_NO_L") = frm.txtLotNO.TextValue
                outDr("IRIME_L") = frm.numIrime.TextValue
                outDr("GOODS_CD_CUST") = setRows(0)(LME050C.DsOutZaiColumnIndex.GOODS_CD_CUST).ToString()
                outDr("NM_1") = setRows(0)(LME050C.DsOutZaiColumnIndex.GOODS_NM_1).ToString()
                outDr("OUTKA_ATT") = setRows(0)(LME050C.DsOutZaiColumnIndex.OUTKA_ATT).ToString()
                outDr("SEARCH_KEY_1") = setRows(0)(LME050C.DsOutZaiColumnIndex.SEARCH_KEY_1).ToString()
                outDr("UNSO_ONDO_KB") = setRows(0)(LME050C.DsOutZaiColumnIndex.UNSO_ONDO_KB).ToString()
                outDr("PKG_UT") = setRows(0)(LME050C.DsOutZaiColumnIndex.PKG_UT).ToString()
                outDr("STD_IRIME_NB") = setRows(0)(LME050C.DsOutZaiColumnIndex.STD_IRIME_NB).ToString()
                outDr("STD_WT_KGS") = setRows(0)(LME050C.DsOutZaiColumnIndex.STD_WT_KGS).ToString()
                outDr("TARE_YN") = setRows(0)(LME050C.DsOutZaiColumnIndex.TARE_YN).ToString()
                outDr("HIKIATE_ALERT_YN") = setRows(0)(LME050C.DsOutZaiColumnIndex.HIKIATE_ALERT_YN).ToString()
                outDr("STD_IRIME_UT") = setRows(0)(LME050C.DsOutZaiColumnIndex.STD_IRIME_UT).ToString()
                outDr("PKG_NB") = setRows(0)(LME050C.DsOutZaiColumnIndex.PKG_NB).ToString()
                outDr("NB_UT_NM") = setRows(0)(LME050C.DsOutZaiColumnIndex.NB_UT_NM).ToString()
                outDr("IRIME_UT_NM") = setRows(0)(LME050C.DsOutZaiColumnIndex.IRIME_UT_NM).ToString()
                outDr("GOODS_CD_NRS") = setRows(0)(LME050C.DsOutZaiColumnIndex.GOODS_CD_NRS).ToString()
                outDr("CUST_CD_S") = setRows(0)(LME050C.DsOutZaiColumnIndex.CUST_CD_S).ToString()
                outDr("CUST_CD_SS") = setRows(0)(LME050C.DsOutZaiColumnIndex.CUST_CD_SS).ToString()
                outDr("IDO_DATE") = setRows(0)(LME050C.DsOutZaiColumnIndex.IDO_DATE).ToString()
                outDr("INKA_DATE") = setRows(0)(LME050C.DsOutZaiColumnIndex.INKA_DATE).ToString()
                outDr("HOKAN_STR_DATE") = setRows(0)(LME050C.DsOutZaiColumnIndex.HOKAN_STR_DATE).ToString()
                outDr("COA_YN") = setRows(0)(LME050C.DsOutZaiColumnIndex.COA_YN).ToString()
                outDr("OUTKA_KAKO_SAGYO_KB_1") = setRows(0)(LME050C.DsOutZaiColumnIndex.OUTKA_KAKO_SAGYO_KB_1).ToString()
                outDr("OUTKA_KAKO_SAGYO_KB_2") = setRows(0)(LME050C.DsOutZaiColumnIndex.OUTKA_KAKO_SAGYO_KB_2).ToString()
                outDr("OUTKA_KAKO_SAGYO_KB_3") = setRows(0)(LME050C.DsOutZaiColumnIndex.OUTKA_KAKO_SAGYO_KB_3).ToString()
                outDr("OUTKA_KAKO_SAGYO_KB_4") = setRows(0)(LME050C.DsOutZaiColumnIndex.OUTKA_KAKO_SAGYO_KB_4).ToString()
                outDr("OUTKA_KAKO_SAGYO_KB_5") = setRows(0)(LME050C.DsOutZaiColumnIndex.OUTKA_KAKO_SAGYO_KB_5).ToString()
                outDr("SIZE_KB") = setRows(0)(LME050C.DsOutZaiColumnIndex.SIZE_KB).ToString()
                outDr("NB_UT") = setRows(0)(LME050C.DsOutZaiColumnIndex.NB_UT).ToString()
                outDr("CUST_CD_L_GOODS") = setRows(0)(LME050C.DsOutZaiColumnIndex.CUST_CD_L_GOODS).ToString()
                outDr("CUST_CD_M_GOODS") = setRows(0)(LME050C.DsOutZaiColumnIndex.CUST_CD_M_GOODS).ToString()
                outDr("INKA_DATE_KANRI_KB") = setRows(0)(LME050C.DsOutZaiColumnIndex.INKA_DATE_KANRI_KB).ToString()

                outDr("SYS_UPD_DATE") = setRows(0)(LME050C.DsOutZaiColumnIndex.SYS_UPD_DATE).ToString()
                outDr("SYS_UPD_TIME") = setRows(0)(LME050C.DsOutZaiColumnIndex.SYS_UPD_TIME).ToString()

                '設定値をデータセットに設定
                rtDs.Tables(LME050C.TABLE_NM_OUT).Rows.Add(outDr)

            End With

        Next

        Return rtDs

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LME050F) As Boolean

        If Me._Prm.ParamDataSet.Tables(LME050C.TABLE_NM_OUT) Is Nothing = True _
            OrElse Me._Prm.ParamDataSet.Tables(LME050C.TABLE_NM_OUT).Rows.Count = 0 Then

            'リターンコードの設定
            Me._Prm.ReturnFlg = False
        Else

            'リターンコードの設定
            Me._Prm.ReturnFlg = True

        End If

    End Function

    ''' <summary>
    ''' デフォルトメッセージを設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDefMessage(ByVal frm As LME050F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find("lblMsgAria", True)(0).Text) = True Then

            'メッセージ設定
            MyBase.ShowMessage(frm, "G003")

        End If

    End Sub

#End Region 'イベント定義(一覧)

#Region "イベント振分"

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LME050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '「検索」処理
        Me.ActionControl(LME050C.EventShubetsu.KENSAKU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LME050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectEvent")

        '「選択」処理
        Me.ActionControl(LME050C.EventShubetsu.SENTAKU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LME050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey12Press")

        '終了処理  
        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey12Press")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LME050F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' 梱数の値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub numSyukkaKosu_Leave(ByVal frm As LME050F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CAL_KOSU")

        '「個数」処理
        Me.ActionControl(LME050C.EventShubetsu.CAL_KONSU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CAL_KOSU")

    End Sub

    ''' <summary>
    ''' 端数の値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub numSyukkaHasu_Leave(ByVal frm As LME050F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CAL_KOSU")

        '「端数」処理
        Me.ActionControl(LME050C.EventShubetsu.CAL_KONSU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CAL_KOSU")

    End Sub

    'START YANAI 要望番号1424 支払処理
    ''' <summary>
    ''' 入目の値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub numIrime_Leave(ByVal frm As LME050F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CAL_KOSU")

        '「入目」処理
        Me.ActionControl(LME050C.EventShubetsu.CAL_IRIME, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CAL_KOSU")

    End Sub
    'END YANAI 要望番号1424 支払処理

    ''' <summary>
    ''' スプレッドの値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub sprZaiko_Change(ByVal frm As LME050F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprZaiko_Change")

        '「スプレッド変更」処理
        Me.ActionControl(LME050C.EventShubetsu.CHANGE_SPREAD, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprZaiko_Change")

    End Sub

    ''' <summary>
    ''' フォームがアクティブになるときに発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub LME050F_Activated(ByVal frm As LME050F)
        Me._G.SetFoucus()
    End Sub

    ''' <summary>
    ''' スプレッドの値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub sprZaiko_KeysAdd(ByVal frm As LME050F)

        'チェックオンオフ
        Me._G.SetCheckOnOffKeysAdd(frm)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class