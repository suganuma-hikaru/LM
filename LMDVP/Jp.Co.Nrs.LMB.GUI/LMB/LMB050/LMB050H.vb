' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB050H : 入荷検品取込
'  作  成  者       :  菊池
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMB050ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMB050H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMB050V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMB050G

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBConG As LMBControlG

    ''' <summary>
    ''' ハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBConH As LMBControlH

    ''' <summary>
    ''' Validateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBConV As LMBControlV

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Prm As LMFormData

    ''' <summary>
    ''' パラメータデータセット
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

    ''' <summary>
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

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
        Dim frm As LMB050F = New LMB050F(Me)

        Dim popLL As LMFormPopLL = DirectCast(frm, LMFormPopLL)

        'Gamen共通クラスの設定

        'Validateクラスの設定
        Me._V = New LMB050V(Me, frm)

        Me._LMBConG = New LMBControlG(frm)
        'Gamenクラスの設定
        Me._G = New LMB050G(Me, frm)

        Me._LMBConV = New LMBControlV(Me, DirectCast(frm, Form))

        'Hnadler共通クラスの設定
        Me._LMBConH = New LMBControlH(DirectCast(frm, Form), Me.GetPGID, Me)

        'Gamen共通クラスの設定
        Me._LMBConG = New LMBControlG(DirectCast(frm, Form))

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        '営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(frm.cmbNrsBrCd, frm.cmbSoko)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()


        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Dim prmdRow As DataRow = _PrmDs.Tables(LMB050C.TABLE_NM_IN).Rows(0)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Me._G.SetInitForm(frm, Me._PrmDs)
        Call Me._G.InitSpread(prmdRow)

        '↓ データ取得の必要があればここにコーディングする。

        '検索処理(キャッシュ)
        Call Me.SelectListEvent(frm)

        '↑ データ取得の必要があればここにコーディングする。

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
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMB050F)

        '処理開始アクション
        Call Me._LMBConH.StartAction(frm)

        '入力チェック
        If Me._V.IsInputChk() = False Then

            '終了処理
            Call Me._LMBConH.EndAction(frm)

            Exit Sub

        End If

        '検索
        Dim rtnDs As DataSet = Me.SelectList(frm)

        If rtnDs IsNot Nothing Then

            Dim outTbl As DataTable = rtnDs.Tables(LMB050C.TABLE_NM_OUT)
            Dim count As Integer = outTbl.Rows.Count

            '取得件数による処理変更
            If 0 < count Then

                '取得データをSPREADに表示
                Call Me._G.SetSpread(outTbl)
                Call Me._G.SetSpreadColor(outTbl)

                'メッセージエリアの設定
                MyBase.ShowMessage(frm, "G008", New String() {Convert.ToString(count)})

            End If

        End If

        '終了処理
        Call Me._LMBConH.EndAction(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        frm.sprDetail.Focus()


    End Sub


    ''' <summary>
    ''' ダブルクリック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks>マスタ参照項目の場合、POP呼出</remarks>
    Private Sub DoubleClick(ByVal frm As LMB050F)

        Dim selectRow As Integer = frm.sprDetail.ActiveSheet.ActiveRowIndex()
        Dim prm As LMFormData
        Dim dr As DataRow

        If Me._V.IsCallGoodsPop(selectRow) = False Then
            Exit Sub
        End If

        prm = Me.GoodsPop(frm, selectRow)

        If prm.ReturnFlg = True Then
            With frm.sprDetail.ActiveSheet
                dr = prm.ParamDataSet.Tables(LMZ020C.TABLE_NM_OUT).Rows(0)

                '商品M項目
                .Cells(selectRow, LMB050G.sprDetailDef.GOODS_CD_CUST.ColNo).Value = dr.Item("GOODS_CD_CUST").ToString()
                .Cells(selectRow, LMB050G.sprDetailDef.STD_IRIME_NB.ColNo).Value = dr.Item("STD_IRIME_NB").ToString()
                .Cells(selectRow, LMB050G.sprDetailDef.STD_IRIME_UT.ColNo).Value = dr.Item("STD_IRIME_UT").ToString()
                .Cells(selectRow, LMB050G.sprDetailDef.NB_UT.ColNo).Value = dr.Item("NB_UT").ToString()
                .Cells(selectRow, LMB050G.sprDetailDef.GOODS_CD_NRS.ColNo).Value = dr.Item("GOODS_CD_NRS").ToString()
                .Cells(selectRow, LMB050G.sprDetailDef.ONDO_KB.ColNo).Value = dr.Item("ONDO_KB").ToString()
                .Cells(selectRow, LMB050G.sprDetailDef.ONDO_STR_DATE.ColNo).Value = dr.Item("ONDO_STR_DATE").ToString()
                .Cells(selectRow, LMB050G.sprDetailDef.ONDO_END_DATE.ColNo).Value = dr.Item("ONDO_END_DATE").ToString()
                .Cells(selectRow, LMB050G.sprDetailDef.STD_WT_KGS.ColNo).Value = dr.Item("STD_WT_KGS").ToString()

                'マスタ登録によるカウンタ正常化
                .Cells(selectRow, LMB050G.sprDetailDef.M_GOODS_UT_NB_COUNT.ColNo).Value = "1"

            End With
        End If

    End Sub

#End Region 'イベント定義(一覧)

#Region "個別メソッド"


    ''' <summary>
    ''' 検索処理(データセット設定)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectList(ByVal frm As LMB050F) As DataSet

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        '閾値の設定
        Dim lc As Integer = Convert.ToInt32(Convert.ToDouble( _
                              MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                              .Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1")))
        MyBase.SetLimitCount(lc)

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                              MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                              .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1")))
        MyBase.SetMaxResultCount(mc)

        'DataSet設定
        Dim ds As DataSet = New LMB050DS()
        Call Me.SetDatasetINKAKENPINInData(frm, ds)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Dim prmdRow As DataRow = _PrmDs.Tables(LMB050C.TABLE_NM_IN).Rows(0)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")
        Dim rtnDs As DataSet
        '==========================
        'WSAクラス呼出
        '==========================
        rtnDs = Me._LMBConH.CallWSAAction(DirectCast(frm, Form) _
                                                         , "LMB050BLF", "SelectListData", ds _
                                                         , lc, mc)

        '検索成功時共通処理を行う
        If rtnDs IsNot Nothing Then

            Call Me.SuccessSelect(frm, rtnDs)

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()


        Return rtnDs

    End Function


    ''' <summary>
    ''' 検索成功時共通処理（画面別）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMB050F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMB050C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'SPREAD(表示行)初期化
        frm.sprDetail.CrearSpread()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        Call Me._G.SetSpreadColor(dt)

        Dim max As Integer = dt.Rows.Count

        If 0 < max Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G016", New String() {max.ToString()})

        End If

    End Sub


    ''' <summary>
    ''' 選択処理（OKボタン押下時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RowOkSelect(ByVal frm As LMB050F)

        Me._SelectFlg = LMConst.FLG.ON

        '処理開始アクション
        Me._LMBConH.StartAction(frm)

        '項目チェック
        If Me._V.IsSelectedCheck() = False Then

            Me._SelectFlg = LMConst.FLG.OFF

            '処理終了アクション
            Me._LMBConH.EndAction(frm)
            Exit Sub
        End If

        '更新データチェックアクション
        Dim inDs As DataSet = Me.InDataSet(frm)

        '==========================
        'WSAクラス呼出
        '==========================
        inDs = MyBase.CallWSA("LMB050BLF", "SelectHenkouChk", inDs)

        If Me._V.IsStateHenko(inDs) = False Then

            Me._SelectFlg = LMConst.FLG.OFF

            '処理終了アクション
            Me._LMBConH.EndAction(frm)
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
        Me._LMBConH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 商品マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="selectRow"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GoodsPop(ByVal frm As LMB050F, ByVal selectRow As Integer) As LMFormData

        Dim ds As DataSet = New LMZ020DS()
        Dim dt As DataTable = ds.Tables(LMZ020C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.lblCustCdL.TextValue
            .Item("CUST_CD_M") = frm.lblCustCdM.TextValue
            .Item("GOODS_CD_CUST") = Me._LMBConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(selectRow, LMB050G.sprDetailDef.GOODS_CD_CUST.ColNo))
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        MyBase.ClearMessageData()

        'Pop起動
        Return Me._LMBConH.FormShow(ds, "LMZ020", "", True)

    End Function


#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(一覧部データ)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetINKAKENPINInData(ByVal frm As LMB050F, ByVal ds As DataSet)

        Dim drow As DataRow = ds.Tables(LMB050C.TABLE_NM_IN).NewRow

        drow("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue
        drow("CUST_CD_L") = frm.lblCustCdL.TextValue
        drow("CUST_CD_M") = frm.lblCustCdM.TextValue
        drow("WH_CD") = frm.cmbSoko.SelectedValue
        drow("INKA_DATE") = frm.imdSysEntDate.TextValue

        With frm.sprDetail.ActiveSheet

            drow("BUYER_ORD_NO_L") = Me._LMBConV.GetCellValue(.Cells(0, LMB050G.sprDetailDef.BUYER_ORD_NO_L.ColNo))
            drow("GOODS_NM") = Me._LMBConV.GetCellValue(.Cells(0, LMB050G.sprDetailDef.GOODS_NM.ColNo))
            drow("GOODS_CD_CUST") = Me._LMBConV.GetCellValue(.Cells(0, LMB050G.sprDetailDef.GOODS_CD_CUST.ColNo))
            drow("LOT_NO") = Me._LMBConV.GetCellValue(.Cells(0, LMB050G.sprDetailDef.LOT_NO.ColNo))
            drow("REMARK_L") = Me._LMBConV.GetCellValue(.Cells(0, LMB050G.sprDetailDef.REMARK_L.ColNo))
            drow("REMARK_M") = Me._LMBConV.GetCellValue(.Cells(0, LMB050G.sprDetailDef.REMARK_M.ColNo))
            drow("TOU_NO") = Me._LMBConV.GetCellValue(.Cells(0, LMB050G.sprDetailDef.TOU_NO.ColNo))
            drow("SITU_NO") = Me._LMBConV.GetCellValue(.Cells(0, LMB050G.sprDetailDef.SITU_NO.ColNo))
            drow("ZONE_CD") = Me._LMBConV.GetCellValue(.Cells(0, LMB050G.sprDetailDef.ZONE_CD.ColNo))
            drow("LOCA") = Me._LMBConV.GetCellValue(.Cells(0, LMB050G.sprDetailDef.LOCA.ColNo))
            drow("OFB_KB") = Me._LMBConV.GetCellValue(.Cells(0, LMB050G.sprDetailDef.OFB_KB.ColNo))
            drow("OUTKA_FROM_ORD_NO_L") = Me._LMBConV.GetCellValue(.Cells(0, LMB050G.sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo))

        End With

        ds.Tables(LMB050C.TABLE_NM_IN).Rows.Add(drow)

    End Sub


    ''' <summary>
    ''' 選択行をOUTのDSに変換
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function OutDataSet(ByVal frm As LMB050F) As DataSet

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me._V.getCheckList()
        Dim lngcnt As Integer = Me._ChkList.Count() - 1

        Dim rtDs As DataSet = New LMB050DS()
        Dim dt As DataTable = rtDs.Tables(LMB050C.TABLE_NM_OUT)
        Dim outDr As DataRow = dt.NewRow()

        '値設定
        For i As Integer = 0 To lngcnt
            With frm.sprDetail.ActiveSheet

                outDr = dt.NewRow()

                outDr("STATE") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.STATE.ColNo))
                outDr("BUYER_ORD_NO_L") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.BUYER_ORD_NO_L.ColNo))
                outDr("GOODS_NM") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.GOODS_NM.ColNo))
                outDr("GOODS_CD_CUST") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.GOODS_CD_CUST.ColNo))
                outDr("STD_IRIME_NB") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.STD_IRIME_NB.ColNo))
                outDr("STD_IRIME_UT") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.STD_IRIME_UT.ColNo))
                outDr("JISSEKI_INKA_NB") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.JISSEKI_INKA_NB.ColNo))
                outDr("NB_UT") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.NB_UT.ColNo))
                outDr("JISSEKI_INKA_QT") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.JISSEKI_INKA_QT.ColNo))
                outDr("IRIME") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.IRIME.ColNo))
                outDr("JISSEKI_PKG_UT") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.JISSEKI_PKG_UT.ColNo))
                outDr("PKG_NB") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.PKG_NB.ColNo))
                outDr("PKG_UT") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.PKG_UT.ColNo))
                outDr("LOT_NO") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.LOT_NO.ColNo))
                outDr("REMARK_L") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.REMARK_L.ColNo))
                outDr("REMARK_M") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.REMARK_M.ColNo))
                outDr("TOU_NO") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.TOU_NO.ColNo))
                outDr("SITU_NO") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.SITU_NO.ColNo))
                outDr("ZONE_CD") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.ZONE_CD.ColNo))
                outDr("LOCA") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.LOCA.ColNo))
                outDr("OFB_KB") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.OFB_KB.ColNo))
                outDr("OUTKA_FROM_ORD_NO_L") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo))
                outDr("CRT_DATE") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.CRT_DATE.ColNo))
                outDr("GOODS_CD_NRS") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.GOODS_CD_NRS.ColNo))
                outDr("ONDO_KB") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.ONDO_KB.ColNo))
                outDr("ONDO_STR_DATE") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.ONDO_STR_DATE.ColNo))
                outDr("ONDO_END_DATE") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.ONDO_END_DATE.ColNo))
                outDr("SYS_UPD_DATE") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.SYS_UPD_DATE.ColNo))
                outDr("SYS_UPD_TIME") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.SYS_UPD_TIME.ColNo))
                outDr("STD_WT_KGS") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.STD_WT_KGS.ColNo))

                '設定値をデータセットに設定
                rtDs.Tables(LMB050C.TABLE_NM_OUT).Rows.Add(outDr)

            End With

        Next

        Return rtDs

    End Function

    ''' <summary>
    ''' 選択行をOUTのDSに変換
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function InDataSet(ByVal frm As LMB050F) As DataSet

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me._V.getCheckList()
        Dim lngcnt As Integer = Me._ChkList.Count() - 1

        Dim rtDs As DataSet = New LMB050DS()
        Dim dt As DataTable = rtDs.Tables(LMB050C.TABLE_NM_IN)
        Dim inDr As DataRow = dt.NewRow()

        '値設定
        For i As Integer = 0 To lngcnt
            With frm.sprDetail.ActiveSheet

                inDr = dt.NewRow()

                inDr("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
                inDr("BUYER_ORD_NO_L") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.BUYER_ORD_NO_L.ColNo))
                inDr("STATE") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), LMB050G.sprDetailDef.STATE.ColNo))

                '設定値をデータセットに設定
                rtDs.Tables(LMB050C.TABLE_NM_IN).Rows.Add(inDr)

            End With

        Next

        Return rtDs

    End Function


    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMB050F) As Boolean

        If Me._Prm.ParamDataSet.Tables(LMB050C.TABLE_NM_OUT) Is Nothing = True _
            OrElse Me._Prm.ParamDataSet.Tables(LMB050C.TABLE_NM_OUT).Rows.Count = 0 Then

            'リターンコードの設定
            Me._Prm.ReturnFlg = False
        Else

            'リターンコードの設定
            Me._Prm.ReturnFlg = True

        End If

    End Function

#End Region 'DataSet設定

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMB050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "selectdata")

        '検索処理
        Call Me.SelectListEvent(frm)

        Logger.EndLog(Me.GetType.Name, "selectdata")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(OK処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMB050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectEvent")

        '選択処理
        Call Me.RowOkSelect(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectEvent")
        Logger.StartLog(Me.GetType.Name, "OKbutton")



    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMB050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()


    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMB050F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMB050F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprCellDoubleClick")

        'ダブルクリックアクション処理
        Call Me.DoubleClick(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprCellDoubleClick")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class