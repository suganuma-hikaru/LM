' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB040H : 入荷検品選択
'  作  成  者       :  小林
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMB040ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMB040H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMB040V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMB040G

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
        Dim frm As LMB040F = New LMB040F(Me)

        Dim popLL As LMFormPopLL = DirectCast(frm, LMFormPopLL)

        'Gamen共通クラスの設定
        Me._LMBConG = New LMBControlG(frm)

        'Validateクラスの設定
        Me._V = New LMB040V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMB040G(Me, frm)

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

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()


        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Dim prmdRow As DataRow = _PrmDs.Tables(LMB040C.TABLE_NM_IN).Rows(0)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Me._G.SetInitForm(frm, Me._PrmDs)
        '追加開始 2015.05.15 要望番号:2292
        Dim tmpdate As Date = Date.Parse(Format(Convert.ToInt32(MyBase.GetSystemDateTime(0)), "0000/00/00"))
        frm.imdSysEntDate.TextValue = DateAdd("d", -1, tmpdate).ToString("yyyyMMdd")
        frm.imdSysEntDateTo.TextValue = MyBase.GetSystemDateTime(0)
        '追加終了 2015.05.15 要望番号:2292
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
    Private Sub SelectListEvent(ByVal frm As LMB040F)

        '処理開始アクション
        Call Me._LMBConH.StartAction(frm)

        '入力チェック
        If Me._V.IsInputChk(_G) = False Then

            '終了処理
            Call Me._LMBConH.EndAction(frm)

            Exit Sub

        End If

        '検索
        Dim rtnDs As DataSet = Me.SelectList(frm)

        If rtnDs IsNot Nothing Then

            Dim outTbl As DataTable = rtnDs.Tables(LMB040C.TABLE_NM_OUT)
            Dim count As Integer = outTbl.Rows.Count

            '取得件数による処理変更
            If 0 < count Then

                '取得データをSPREADに表示
                Call Me._G.SetSpread(outTbl)

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
    Private Sub DoubleClick(ByVal frm As LMB040F)

        Dim selectRow As Integer = frm.sprDetail.ActiveSheet.ActiveRowIndex()
        Dim prm As LMFormData
        Dim dr As DataRow

        If Me._V.IsCallGoodsPop(selectRow, _G) = False Then
            Exit Sub
        End If

#If True Then ' フィルメニッヒ入荷検品対応 20170310 added by inoue 
        Dim targetRowIndexes As New List(Of Integer) From {selectRow}

        If (_V.DoChangeCheckedGoods(selectRow, _G)) Then

            For Each checkedIndex As Integer In _V.getCheckList
                If (targetRowIndexes.Contains(checkedIndex) = False) Then

                    If (_V.IsSetSameGoodsDataWithMessage(selectRow, checkedIndex, _G) = False) Then

                        ' 商品名,ロット,入目が異なる商品が含まれる場合はエラー終了
                        Exit Sub
                    End If

                    targetRowIndexes.Add(checkedIndex)

                End If
            Next
        End If
#End If

        prm = Me.GoodsPop(frm, selectRow)

        If prm.ReturnFlg = True Then

            With frm.sprDetail.ActiveSheet

#If False Then ' フィルメニッヒ入荷検品対応 20170310 changed by inoue 
                dr = prm.ParamDataSet.Tables(LMZ020C.TABLE_NM_OUT).Rows(0)
                Dim ttlNb As Integer = Integer.Parse(.Cells(selectRow, LMB040G.sprDetailDef.KENPIN_KAKUTEI_TTL_NB.ColNo).Value.ToString())
                Dim pkgNb As Integer = Integer.Parse(dr.Item("PKG_NB").ToString())
                .Cells(selectRow, LMB040G.sprDetailDef.MST_EXISTS_MARK.ColNo).Value = ""
                '2013.07.19 追加START
                .Cells(selectRow, LMB040G.sprDetailDef.KENPIN_DATE.ColNo).Value = .Cells(selectRow, LMB040G.sprDetailDef.KENPIN_DATE.ColNo).Value.ToString()
                '2013.07.19 追加END
                .Cells(selectRow, LMB040G.sprDetailDef.GOODS_NM.ColNo).Value = dr.Item("GOODS_NM_1").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.IRIME.ColNo).Value = dr.Item("STD_IRIME_NB").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.IRIME_UT.ColNo).Value = dr.Item("STD_IRIME_UT").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.PKG_UT.ColNo).Value = dr.Item("PKG_UT").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.PKG_NB.ColNo).Value = dr.Item("PKG_NB").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.NB_UT_1.ColNo).Value = dr.Item("NB_UT").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.NB_UT_2.ColNo).Value = dr.Item("NB_UT").ToString()
                '2013.07.19 追加START
                .Cells(selectRow, LMB040G.sprDetailDef.USER_NM.ColNo).Value = .Cells(selectRow, LMB040G.sprDetailDef.USER_NM.ColNo).Value.ToString()
                '2013.07.19 追加END
                .Cells(selectRow, LMB040G.sprDetailDef.ONDO_KB.ColNo).Value = dr.Item("ONDO_KB").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.ONDO_STR_DATE.ColNo).Value = dr.Item("ONDO_STR_DATE").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.ONDO_END_DATE.ColNo).Value = dr.Item("ONDO_END_DATE").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.STD_WT_KGS.ColNo).Value = dr.Item("STD_WT_KGS").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.KONSU.ColNo).Value = RoundOff((ttlNb / pkgNb) - 0.5, 0)
                .Cells(selectRow, LMB040G.sprDetailDef.HASU.ColNo).Value = ttlNb Mod pkgNb
                .Cells(selectRow, LMB040G.sprDetailDef.BETU_WT.ColNo).Value = ttlNb * Double.Parse(dr.Item("STD_WT_KGS").ToString())
                .Cells(selectRow, LMB040G.sprDetailDef.INKA_KAKO_SAGYO_KB_1.ColNo).Value = dr.Item("INKA_KAKO_SAGYO_KB_1").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.INKA_KAKO_SAGYO_KB_2.ColNo).Value = dr.Item("INKA_KAKO_SAGYO_KB_2").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.INKA_KAKO_SAGYO_KB_3.ColNo).Value = dr.Item("INKA_KAKO_SAGYO_KB_3").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.INKA_KAKO_SAGYO_KB_4.ColNo).Value = dr.Item("INKA_KAKO_SAGYO_KB_4").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.INKA_KAKO_SAGYO_KB_5.ColNo).Value = dr.Item("INKA_KAKO_SAGYO_KB_5").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.MST_EXISTS_KBN.ColNo).Value = LMB040C.GATCH_KBN_ONE
                '2013.07.18 追加START
                .Cells(selectRow, LMB040G.sprDetailDef.CUST_CD_S.ColNo).Value = dr.Item("CUST_CD_S").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.CUST_CD_SS.ColNo).Value = dr.Item("CUST_CD_SS").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.TARE_YN.ColNo).Value = dr.Item("TARE_YN").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.LOT_CTL_KB.ColNo).Value = dr.Item("LOT_CTL_KB").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.LT_DATE_CTL_KB.ColNo).Value = dr.Item("LT_DATE_CTL_KB").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.CRT_DATE_CTL_KB.ColNo).Value = dr.Item("CRT_DATE_CTL_KB").ToString()
                .Cells(selectRow, LMB040G.sprDetailDef.GOODS_CD_NRS.ColNo).Value = dr.Item("GOODS_CD_NRS").ToString()
                '2013.07.18 追加END
#Else

                For Each rowIndex As Integer In targetRowIndexes

                    dr = prm.ParamDataSet.Tables(LMZ020C.TABLE_NM_OUT).Rows(0)
                    Dim ttlNb As Integer = Integer.Parse(.Cells(rowIndex, _G.sprDetailDef.KENPIN_KAKUTEI_TTL_NB.ColNo).Value.ToString())
                    Dim pkgNb As Integer = Integer.Parse(dr.Item("PKG_NB").ToString())
                    .Cells(rowIndex, _G.sprDetailDef.MST_EXISTS_MARK.ColNo).Value = ""
                    '2013.07.19 追加START
                    .Cells(rowIndex, _G.sprDetailDef.KENPIN_DATE.ColNo).Value = .Cells(rowIndex, _G.sprDetailDef.KENPIN_DATE.ColNo).Value.ToString()
                    '2013.07.19 追加END
                    .Cells(rowIndex, _G.sprDetailDef.GOODS_NM.ColNo).Value = dr.Item("GOODS_NM_1").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.IRIME.ColNo).Value = dr.Item("STD_IRIME_NB").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.IRIME_UT.ColNo).Value = dr.Item("STD_IRIME_UT").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.PKG_UT.ColNo).Value = dr.Item("PKG_UT").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.PKG_NB.ColNo).Value = dr.Item("PKG_NB").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.NB_UT_1.ColNo).Value = dr.Item("NB_UT").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.NB_UT_2.ColNo).Value = dr.Item("NB_UT").ToString()
                    '2013.07.19 追加START
                    .Cells(rowIndex, _G.sprDetailDef.USER_NM.ColNo).Value = .Cells(rowIndex, _G.sprDetailDef.USER_NM.ColNo).Value.ToString()
                    '2013.07.19 追加END
                    .Cells(rowIndex, _G.sprDetailDef.ONDO_KB.ColNo).Value = dr.Item("ONDO_KB").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.ONDO_STR_DATE.ColNo).Value = dr.Item("ONDO_STR_DATE").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.ONDO_END_DATE.ColNo).Value = dr.Item("ONDO_END_DATE").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.STD_WT_KGS.ColNo).Value = dr.Item("STD_WT_KGS").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.KONSU.ColNo).Value = RoundOff((ttlNb / pkgNb) - 0.5, 0)
                    .Cells(rowIndex, _G.sprDetailDef.HASU.ColNo).Value = ttlNb Mod pkgNb
                    .Cells(rowIndex, _G.sprDetailDef.BETU_WT.ColNo).Value = ttlNb * Double.Parse(dr.Item("STD_WT_KGS").ToString())
                    .Cells(rowIndex, _G.sprDetailDef.INKA_KAKO_SAGYO_KB_1.ColNo).Value = dr.Item("INKA_KAKO_SAGYO_KB_1").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.INKA_KAKO_SAGYO_KB_2.ColNo).Value = dr.Item("INKA_KAKO_SAGYO_KB_2").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.INKA_KAKO_SAGYO_KB_3.ColNo).Value = dr.Item("INKA_KAKO_SAGYO_KB_3").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.INKA_KAKO_SAGYO_KB_4.ColNo).Value = dr.Item("INKA_KAKO_SAGYO_KB_4").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.INKA_KAKO_SAGYO_KB_5.ColNo).Value = dr.Item("INKA_KAKO_SAGYO_KB_5").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.MST_EXISTS_KBN.ColNo).Value = LMB040C.GATCH_KBN_ONE
                    '2013.07.18 追加START
                    .Cells(rowIndex, _G.sprDetailDef.CUST_CD_S.ColNo).Value = dr.Item("CUST_CD_S").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.CUST_CD_SS.ColNo).Value = dr.Item("CUST_CD_SS").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.TARE_YN.ColNo).Value = dr.Item("TARE_YN").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.LOT_CTL_KB.ColNo).Value = dr.Item("LOT_CTL_KB").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.LT_DATE_CTL_KB.ColNo).Value = dr.Item("LT_DATE_CTL_KB").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.CRT_DATE_CTL_KB.ColNo).Value = dr.Item("CRT_DATE_CTL_KB").ToString()
                    .Cells(rowIndex, _G.sprDetailDef.GOODS_CD_NRS.ColNo).Value = dr.Item("GOODS_CD_NRS").ToString()
                    '2013.07.18 追加END
                    .Cells(rowIndex, _G.sprDetailDef.GOODS_CD_CUST.ColNo).Value = dr.Item("GOODS_CD_CUST").ToString()
                Next



#End If
            End With
        End If

    End Sub

    '追加開始 2015.05.13 要望番号:2292
    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub DeleteAction(ByVal frm As LMB040F)

        '処理開始アクション
        Call Me._LMBConH.StartAction(frm)

        '項目チェック
        If Me._V.IsSelectedCheck(_G) = False Then

            '処理終了アクション
            Me._LMBConH.EndAction(frm)
            Exit Sub
        End If

        Dim rtnResult As Boolean = True

        '確認メッセージ表示
        rtnResult = rtnResult AndAlso Me._LMBConH.SetMessageC001(frm, frm.btnRowDel.Text.Replace("行", String.Empty))

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション()
            Call Me._LMBConH.EndAction(frm)
            Exit Sub

        End If

        '選択行削除
        Me.DeleteList(frm)

        '再描画
        Me.SelectList(frm)

        '処理終了アクション
        Me._LMBConH.EndAction(frm)
    End Sub
    '追加開始 2015.05.13 要望番号:2292

#End Region 'イベント定義(一覧)

#Region "個別メソッド"


    ''' <summary>
    ''' 検索処理(データセット設定)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectList(ByVal frm As LMB040F) As DataSet

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
        Dim ds As DataSet = New LMB040DS()
        Call Me.SetDatasetINKAKENPINInData(frm, ds)
        Call Me.SetDatasetINPUTEDInData(frm, ds)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Dim prmdRow As DataRow = _PrmDs.Tables(LMB040C.TABLE_NM_IN).Rows(0)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")
        Dim rtnDs As DataSet
        '==========================
        'WSAクラス呼出
        '==========================
        rtnDs = Me._LMBConH.CallWSAAction(DirectCast(frm, Form) _
                                                         , "LMB040BLF", "SelectListData", ds _
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
    Private Sub SuccessSelect(ByVal frm As LMB040F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMB040C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'SPREAD(表示行)初期化
        frm.sprDetail.CrearSpread()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        '検索条件の荷主名クリア
        Call Me._G.ClearTantoNM()

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
    Private Sub RowOkSelect(ByVal frm As LMB040F)

        Me._SelectFlg = LMConst.FLG.ON

        '処理開始アクション
        Me._LMBConH.StartAction(frm)

        '項目チェック
        If Me._V.IsSelectedCheck(_G) = False Then

            Me._SelectFlg = LMConst.FLG.OFF

            '処理終了アクション
            Me._LMBConH.EndAction(frm)
            Exit Sub
        End If

        'WKテーブルの取込フラグを更新する
        If UpdateInkaToriFlg(frm) = False Then
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
    ''' 入荷取込フラグ更新
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaToriFlg(ByVal frm As LMB040F) As Boolean

        'DataSet設定
        Dim ds As DataSet = New LMB040DS()
        Call Me.SetDatasetInUpdateData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateInkaToriFlg")
        MyBase.CallWSA("LMB040BLF", "UpdateInkaToriFlg", ds)
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateInkaToriFlg")

        'エラーがある場合、終了
        If MyBase.IsErrorMessageExist() = True Then

            'メッセージ表示
            MyBase.ShowMessage(frm)

            Return False

        End If

        Return True
    End Function

    ''' <summary>
    ''' 商品マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="selectRow"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GoodsPop(ByVal frm As LMB040F, ByVal selectRow As Integer) As LMFormData

        Dim ds As DataSet = New LMZ020DS()
        Dim dt As DataTable = ds.Tables(LMZ020C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.lblCustCdL.TextValue
            .Item("CUST_CD_M") = frm.lblCustCdM.TextValue
            .Item("GOODS_CD_CUST") = Me._LMBConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(selectRow, _G.sprDetailDef.GOODS_CD_CUST.ColNo))
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
#If True Then ' フィルメニッヒ入荷検品対応 20170310 added by inoue 
            .Item("GOODS_NM_1") = Me._LMBConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(selectRow, _G.sprDetailDef.GOODS_NM.ColNo))

            Dim irime As Decimal = 0
            If (Decimal.TryParse(Me._LMBConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(selectRow _
                                                                                        , _G.sprDetailDef.IRIME.ColNo)), irime)) Then
                If (irime > 0) Then
                    .Item("IRIME") = irime
                End If
            End If
#End If
        End With
        dt.Rows.Add(dr)

        MyBase.ClearMessageData()

        'Pop起動
        Return Me._LMBConH.FormShow(ds, "LMZ020", "", True)

    End Function

    '追加開始 2015.05.13 要望番号:2292
    ''' <summary>
    ''' 削除処理(サーバアクセス)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteList(ByVal frm As LMB040F) As Boolean

        'DataSet設定
        Dim ds As DataSet = New LMB040DS()
        Call Me.SetDatasetDeleteData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteAction")
        '==========================
        'WSAクラス呼出
        '==========================
        MyBase.CallWSA("LMB040BLF", "DeleteAction", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteAction")

        'エラーがある場合、終了
        If MyBase.IsErrorMessageExist() = True Then

            'メッセージ表示
            MyBase.ShowMessage(frm)

            Return False

        End If

        Return True

    End Function
    '追加終了 2015.05.13 要望番号:2292





#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(入荷取込フラグ更新)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDatasetInUpdateData(ByVal frm As LMB040F, ByVal ds As DataSet)

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me._V.getCheckList()
        Dim lngcnt As Integer = Me._ChkList.Count() - 1

        Dim dt As DataTable = ds.Tables(LMB040C.TABLE_NM_IN_UPDATE)
        Dim outDr As DataRow = dt.NewRow()

        '値設定
        For i As Integer = 0 To lngcnt
            With frm.sprDetail.ActiveSheet

                outDr = dt.NewRow()

                outDr("NRS_BR_CD") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.NRS_BR_CD.ColNo))
                outDr("WH_CD") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.WH_CD.ColNo))
                outDr("CUST_CD_L") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.CUST_CD_L.ColNo))
                outDr("INPUT_DATE") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.INPUT_DATE.ColNo))
                outDr("SEQ") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.SEQ.ColNo))
                outDr("SYS_UPD_DATE") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.SYS_UPD_DATE.ColNo))
                outDr("SYS_UPD_TIME") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.SYS_UPD_TIME.ColNo))

                '設定値をデータセットに設定
                dt.Rows.Add(outDr)

            End With

        Next

        Return

    End Sub

    ''' <summary>
    ''' データセット設定(一覧部データ)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetINKAKENPINInData(ByVal frm As LMB040F, ByVal ds As DataSet)

        Dim drow As DataRow = ds.Tables(LMB040C.TABLE_NM_IN).NewRow

        drow("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue
        drow("CUST_CD_L") = frm.lblCustCdL.TextValue
        drow("CUST_CD_M") = frm.lblCustCdM.TextValue
        drow("SAGYO_USER_CD") = frm.txtSAGYO_USER_CD.TextValue
        drow("SYS_ENT_DATE") = frm.imdSysEntDate.TextValue
        '追加開始 2015.05.15 要望番号:2292
        drow("SYS_ENT_DATE_TO") = frm.imdSysEntDateTo.TextValue
        '追加終了 2015.05.15 要望番号:2292
        'WIT対応 
        drow("IS_ONRY_MISHORI") = frm.chkMishori.GetBinaryValue.ToString().Replace("0", "")

        With frm.sprDetail.ActiveSheet

            drow("GOODS_NM_1") = Me._LMBConV.GetCellValue(.Cells(0, _G.sprDetailDef.GOODS_NM.ColNo))
            drow("GOODS_CD_CUST") = Me._LMBConV.GetCellValue(.Cells(0, _G.sprDetailDef.GOODS_CD_CUST.ColNo))
            drow("LOT_NO") = Me._LMBConV.GetCellValue(.Cells(0, _G.sprDetailDef.LOT_NO.ColNo))

#If True Then ' JT物流入荷検品対応 20160726 added inoue
            drow("GOODS_CRT_DATE") = Me._LMBConV.GetCellValue(.Cells(0, _G.sprDetailDef.GOODS_CRT_DATE.ColNo))
#End If
#If True Then ' フィルメニッヒ入荷検品対応 20170310 added by inoue 
            drow("LT_DATE") = Me._LMBConV.GetCellValue(.Cells(0, _G.sprDetailDef.LT_DATE.ColNo))
#End If
            drow("OKIBA") = Me._LMBConV.GetCellValue(.Cells(0, _G.sprDetailDef.OKIBA.ColNo))

            drow("SERIAL_NO") = Me._LMBConV.GetCellValue(.Cells(0, _G.sprDetailDef.SERIAL_NO.ColNo))       'ADD 2018/06/05 依頼番号 : 001589   【LMS】入荷検品検索画面_千葉ハネウェルジャパン_シリアルNoでの検索ができない(千葉野村)

        End With

        ds.Tables(LMB040C.TABLE_NM_IN).Rows.Add(drow)


    End Sub

    ''' <summary>
    ''' データセット設定(すでに選択済みのデータを除く)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetINPUTEDInData(ByVal frm As LMB040F, ByVal ds As DataSet)

        For Each row As DataRow In _PrmDs.Tables(LMB040C.TABLE_NM_INPUTED).Rows

            ds.Tables(LMB040C.TABLE_NM_INPUTED).Rows.Add(row)

        Next


    End Sub

    ''' <summary>
    ''' 選択
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function OutDataSet(ByVal frm As LMB040F) As DataSet

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me._V.getCheckList()
        Dim lngcnt As Integer = Me._ChkList.Count() - 1

        Dim rtDs As DataSet = New LMB040DS()
        Dim dt As DataTable = rtDs.Tables(LMB040C.TABLE_NM_OUT)
        Dim outDr As DataRow = dt.NewRow()

        '値設定
        For i As Integer = 0 To lngcnt
            With frm.sprDetail.ActiveSheet

                outDr = dt.NewRow()

                outDr("MST_EXISTS_MARK") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.MST_EXISTS_MARK.ColNo))
                '2013.07.19 追加START
                outDr("KENPIN_DATE") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.KENPIN_DATE.ColNo))
                '2013.07.19 追加END
                outDr("GOODS_NM") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.GOODS_NM.ColNo))
                outDr("GOODS_CD_CUST") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.GOODS_CD_CUST.ColNo))
                outDr("STD_IRIME_NB") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.IRIME.ColNo))
                outDr("STD_IRIME_UT") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.IRIME_UT.ColNo))
                outDr("PKG_UT") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.PKG_UT.ColNo))
                outDr("PKG_NB") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.PKG_NB.ColNo))
                outDr("NB_UT") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.NB_UT_1.ColNo))
                '2013.07.19 追加START
                outDr("USER_NM") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.USER_NM.ColNo))
                '2013.07.19 追加END
                outDr("LOT_NO") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.LOT_NO.ColNo))
                '2014.02.17 追加START
                outDr("SERIAL_NO") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.SERIAL_NO.ColNo))
                '2014.02.17 追加END
#If True Then ' JT物流入荷検品対応 20160726 added inoue
                outDr("GOODS_CRT_DATE") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.GOODS_CRT_DATE.ColNo))
                outDr("CHK_TANI") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.CHK_TANI.ColNo))
#End If

#If True Then ' フィルメニッヒ入荷検品対応 20170310 added by inoue 
                outDr("LT_DATE") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.LT_DATE.ColNo))

#End If
                '2013.11.25 WIT対応START
                'outDr("KENPIN_NO") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.KENPIN_NO.ColNo))
                outDr("WH_CD") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.WH_CD.ColNo))
                outDr("INPUT_DATE") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.INPUT_DATE.ColNo))
                outDr("SEQ") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.SEQ.ColNo))
                outDr("TORIKOMI_FLG_NM") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.TORIKOMI_FLG_NM.ColNo))
                outDr("GOODS_KANRI_NO") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.GOODS_KANRI_NO.ColNo))
                '2013.11.25 WIT対応START
                outDr("TOU_NO") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.TOU_NO.ColNo))
                outDr("SITU_NO") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.SITU_NO.ColNo))
                outDr("ZONE_CD") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.ZONE_CD.ColNo))
                outDr("LOCA") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.LOCA.ColNo))
                outDr("NRS_BR_CD") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.NRS_BR_CD.ColNo))
                outDr("GOODS_CD_NRS") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.GOODS_CD_NRS.ColNo))
                outDr("CUST_CD_L") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.CUST_CD_L.ColNo))
                outDr("CUST_CD_M") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.CUST_CD_M.ColNo))
                outDr("ONDO_KB") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.ONDO_KB.ColNo))
                outDr("ONDO_STR_DATE") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.ONDO_STR_DATE.ColNo))
                outDr("ONDO_END_DATE") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.ONDO_END_DATE.ColNo))
                outDr("STD_WT_KGS") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.STD_WT_KGS.ColNo))
                outDr("KONSU") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.KONSU.ColNo))
                outDr("HASU") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.HASU.ColNo))
                outDr("BETU_WT") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.BETU_WT.ColNo))
                outDr("INKA_KAKO_SAGYO_KB_1") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.INKA_KAKO_SAGYO_KB_1.ColNo))
                outDr("INKA_KAKO_SAGYO_KB_2") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.INKA_KAKO_SAGYO_KB_2.ColNo))
                outDr("INKA_KAKO_SAGYO_KB_3") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.INKA_KAKO_SAGYO_KB_3.ColNo))
                outDr("INKA_KAKO_SAGYO_KB_4") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.INKA_KAKO_SAGYO_KB_4.ColNo))
                outDr("INKA_KAKO_SAGYO_KB_5") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.INKA_KAKO_SAGYO_KB_5.ColNo))
                outDr("SYS_UPD_DATE") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.SYS_UPD_DATE.ColNo))
                outDr("SYS_UPD_TIME") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.SYS_UPD_TIME.ColNo))
                outDr("MST_EXISTS_KBN") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.MST_EXISTS_KBN.ColNo))
                '2013.07.18 追加START
                outDr("CUST_CD_S") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.CUST_CD_S.ColNo))
                outDr("CUST_CD_SS") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.CUST_CD_SS.ColNo))
                outDr("TARE_YN") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.TARE_YN.ColNo))
                outDr("LOT_CTL_KB") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.LOT_CTL_KB.ColNo))
                outDr("LT_DATE_CTL_KB") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.LT_DATE_CTL_KB.ColNo))
                outDr("CRT_DATE_CTL_KB") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.CRT_DATE_CTL_KB.ColNo))
                '2013.07.18 追加END

                '設定値をデータセットに設定
                rtDs.Tables(LMB040C.TABLE_NM_OUT).Rows.Add(outDr)

            End With

        Next

        Return rtDs

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMB040F) As Boolean

        If Me._Prm.ParamDataSet.Tables(LMB040C.TABLE_NM_OUT) Is Nothing = True _
            OrElse Me._Prm.ParamDataSet.Tables(LMB040C.TABLE_NM_OUT).Rows.Count = 0 Then

            'リターンコードの設定
            Me._Prm.ReturnFlg = False
        Else

            'リターンコードの設定
            Me._Prm.ReturnFlg = True

        End If

    End Function

    '追加開始 2015.05.14 要望番号:2292
    ''' <summary>
    ''' データセット設定(削除)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDeleteData(ByVal frm As LMB040F, ByVal ds As DataSet)

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me._V.getCheckList()
        Dim lngcnt As Integer = Me._ChkList.Count() - 1

        Dim dt As DataTable = ds.Tables(LMB040C.TABLE_NM_IN_DELETE)
        Dim outDr As DataRow = dt.NewRow()

        '値設定
        For i As Integer = 0 To lngcnt
            With frm.sprDetail.ActiveSheet

                outDr = dt.NewRow()

                outDr("NRS_BR_CD") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.NRS_BR_CD.ColNo))
                outDr("WH_CD") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.WH_CD.ColNo))
                outDr("CUST_CD_L") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.CUST_CD_L.ColNo))
                outDr("INPUT_DATE") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.INPUT_DATE.ColNo))
                outDr("SEQ") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.SEQ.ColNo))
                outDr("SYS_UPD_DATE") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.SYS_UPD_DATE.ColNo))
                outDr("SYS_UPD_TIME") = Me._LMBConV.GetCellValue(.Cells(Integer.Parse(Me._ChkList(i).ToString()), _G.sprDetailDef.SYS_UPD_TIME.ColNo))

                '設定値をデータセットに設定
                dt.Rows.Add(outDr)

            End With

        Next

        Return

    End Sub
    '追加開始 2015.05.14 要望番号:2292

#End Region 'DataSet設定

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMB040F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey11Press(ByRef frm As LMB040F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey12Press(ByRef frm As LMB040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()


    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMB040F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '追加開始 2015.05.14 要望番号:2292
    ''' <summary>
    ''' 削除ボタン処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub btnRowDel_Click(ByVal frm As LMB040F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnRowDelPress")

        Call Me.DeleteAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnRowDelPress")

    End Sub
    '追加終了 2015.05.14 要望番号:2292

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMB040F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprCellDoubleClick")

        'ダブルクリックアクション処理
        Call Me.DoubleClick(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprCellDoubleClick")

    End Sub

    ''' <summary>
    ''' 作業ユーザコードテキストでのEnterイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub SAGYO_USER_Enter(ByRef frm As LMB040F)

        Logger.StartLog(Me.GetType.Name, "SAGYO_USER_Enter")

        'DBより該当データの取得処理
        Dim getUserDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER). _
            Select(String.Concat("SYS_DEL_FLG = '0' AND USER_CD = '", frm.txtSAGYO_USER_CD.TextValue.ToString(), "'"))

        If getUserDr.Length() > 0 Then
            frm.lblSAGYO_USER_NM.TextValue = getUserDr(0).Item("USER_NM").ToString()
        End If

        Logger.EndLog(Me.GetType.Name, "SAGYO_USER_Enter")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class