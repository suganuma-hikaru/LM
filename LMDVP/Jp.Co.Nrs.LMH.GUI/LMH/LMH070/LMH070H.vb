' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH070H : 手入力入荷データ分報告用ＥＤＩデータ作成
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMH070ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMH070H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMH070V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMH070G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconV As LMHControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconH As LMHControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconG As LMHControlG

    '値の保持（パラメータ）
    Private _Ds As DataSet

    '値の保持（検索結果）
    Private _SelDs As DataSet

    Private _InOutFlg As String

    Private _InOutMngNo As String

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

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        'パラメータ情報を退避
        Me._Ds = prmDs.Copy

        'フォームの作成
        Dim frm As LMH070F = New LMH070F(Me)

        'Hnadler共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMHconH = New LMHControlH(sForm, MyBase.GetPGID())

        'Gamen共通クラスの設定
        Me._LMHconG = New LMHControlG(sForm)

        'Validate共通クラスの設定
        Me._LMHconV = New LMHControlV(Me, sForm, Me._LMHconG)

        'Gamenクラスの設定
        Me._G = New LMH070G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMH070V(Me, frm)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'コントロール個別設定
        Call Me._G.SetControl()

        '値のクリア
        Call Me._G.ClearControl()

        '初期設定
        If Me.SetForm(frm, prmDs) = False Then
            Exit Sub
        End If

        'メッセージの表示
        Me.ShowMessage(frm, "G007")

        ''画面の入力項目の制御
        'Call Me._G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        '呼び出し元画面情報を設定
        frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

        'フォームの表示
        frm.ShowDialog()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "外部Method"
    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Function ActionControl(ByVal eventShubetsu As LMH070C.EventShubetsu, ByVal frm As LMH070F) As Boolean

        '処理開始アクション
        Call Me._LMHconH.StartAction(frm)

        '権限チェック（共通）
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Call Me._LMHconH.EndAction(frm)
            Exit Function
        End If

        'イベント種別による分岐
        Select Case eventShubetsu
            '*****検索処理******
            Case LMH070C.EventShubetsu.KENSAKU

                Me._InOutMngNo = String.Empty

                '項目チェック
                If Me._V.IsKensakuCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                '検索処理を行う
                Call Me.SelectListInOutka(frm)

                'フォーカスの設定
                Call Me._G.SetFoucus()


            Case LMH070C.EventShubetsu.HOZON

                If frm.sprGoodsInfoInOutka.ActiveSheet.RowCount = 0 Then
                    MyBase.ShowMessage(frm, "E315")
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                '検索結果チェック
                If Me._V.IsKensakuResultCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                '入力チェック
                If Me._V.IsHozonCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Function
                End If

                '処理終了アクション
                Call Me._LMHconH.EndAction(frm)

                Return Me.Hozon(frm)

        End Select

        '処理終了アクション
        Call Me._LMHconH.EndAction(frm)

    End Function
#End Region

#Region "内部Method"

    ''' <summary>
    ''' ロード処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>
    ''' True ：検索成功
    ''' false：検索失敗
    ''' </returns>
    ''' <remarks></remarks>
    Private Function SetForm(ByVal frm As LMH070F, ByVal ds As DataSet) As Boolean

        Dim inDr As DataRow = ds.Tables(LMH070C.TABLE_NM_IN).Rows(0)
        Dim ediCustDrs As DataRow()
        Dim ediIndex As String = String.Empty
        Dim inOutKb As String = inDr("INOUT_KB").ToString()
        Dim nrsBrCd As String = inDr("NRS_BR_CD").ToString()
        Dim nrsWhCd As String = inDr("WH_CD").ToString()
        Dim custCdL As String = inDr("CUST_CD_L").ToString()
        Dim custCdM As String = inDr("CUST_CD_M").ToString()

        'EDI対象荷主マスタの荷主のINDEXの取得(キャッシュ)
        ediCustDrs = Me._LMHconV.SelectEdiCustListDataRow(inOutKb, nrsBrCd, nrsWhCd, custCdL, custCdM)
        If 0 < ediCustDrs.Length Then
            inDr("RCV_NM_HED") = ediCustDrs(0).Item("RCV_NM_HED").ToString()
            '2012.02.29 大阪対応START
            inDr("TBL_INOUT") = ediCustDrs(0).Item("FLAG_16").ToString()
            '2012.02.29 大阪対応END
        Else
            Return False
        End If

        Me._InOutFlg = inDr("INOUT_KB").ToString()

        '初期検索処理
        Dim rtnDs As DataSet = MyBase.CallWSA("LMH070BLF", "SelectEdi", ds)

        '検索成功
        If rtnDs Is Nothing = False _
            AndAlso 0 < rtnDs.Tables(LMH070C.TABLE_NM_OUT_L).Rows.Count _
            AndAlso 0 < rtnDs.Tables(LMH070C.TABLE_NM_OUT_M).Rows.Count Then

            '検索結果をプライベート変数に退避
            Me._SelDs = rtnDs.Copy

            '初期表示時の値設定
            Call Me._G.SetHeaderEdi(rtnDs, ds)
            Call Me._G.SetSpreadEdi(rtnDs)

            Return True

        End If

        Return False

    End Function

#End Region

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMH070F, ByVal e As FormClosingEventArgs) As Boolean

        ''メッセージの表示
        'Select Case MyBase.ShowMessage(frm, "W002")

        '    Case MsgBoxResult.Yes '「はい」押下時

        '        '保存処理
        '        If Me.ActionControl(LMH070C.EventShubetsu.HOZON, frm) = False Then
        '            e.Cancel = True
        '        Else
        '            e.Cancel = False
        '        End If


        '    Case MsgBoxResult.Cancel '「キャンセル」押下時

        '        e.Cancel = True

        'End Select


    End Function

    ''' <summary>
    ''' 入出荷検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListInOutka(ByVal frm As LMH070F)

        'DataSet設定
        Dim rtDs As DataSet = New LMH070DS()
        rtDs = Me.SetDataSelectInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectInOutka")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMH070BLF", "SelectInOutka", rtDs)

        'SPREAD(表示行)初期化
        frm.sprGoodsInfoInOutka.CrearSpread()

        Dim maxedi As Integer = frm.sprGoodsInfoEdi.ActiveSheet.RowCount - 1
        For i As Integer = 0 To maxEdi
            Me._G.ClearLinkNo(i)
        Next

        '検索成功時共通処理を行う
        If rtnDs.Tables(LMH070C.TABLE_NM_INOUTKA).Rows.Count > 0 Then

            '取得データをヘッダー部に表示
            Call Me._G.SetHeaderInOutka(rtnDs)
            '取得データをSPREADに表示
            Call Me._G.SetSpreadInOutka(rtnDs)

            '検索結果のチェック
            If Me._V.IsKensakuResultCheck() = True Then

                '仮紐付処理
                Call Me.TempLink(frm)

                '管理番号をセット
                Me._InOutMngNo = frm.txtInOutkaMngNo.TextValue

                Me.ShowMessage(frm, "G003")
            End If

        Else
            '0件の場合
            MyBase.ShowMessage(frm, "G001")

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectInOutka")


        ''ファンクションキーの設定
        'Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' 紐付け番号自動入力
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub TempLink(ByVal frm As LMH070F)

        Dim maxEdi As Integer = frm.sprGoodsInfoEdi.ActiveSheet.RowCount - 1
        Dim maxInOut As Integer = frm.sprGoodsInfoInOutka.ActiveSheet.RowCount - 1
        Dim ediLotNo As String = String.Empty
        Dim inoutLotNo As String = String.Empty
        Dim ediGoodsCd As String = String.Empty
        Dim inoutGoodsCd As String = String.Empty
        Dim ediNo As String = String.Empty

        With frm.sprGoodsInfoInOutka.ActiveSheet

            For i As Integer = 0 To maxInOut

                inoutLotNo = Me._LMHconV.GetCellValue(.Cells(i, LMH070G.sprGoodsInfoInOutkaDef.LOT_NO.ColNo))
                inoutGoodsCd = Me._LMHconV.GetCellValue(.Cells(i, LMH070G.sprGoodsInfoInOutkaDef.GOODS_CD_CUST.ColNo))
                ediNo = Me._LMHconV.GetCellValue(.Cells(i, LMH070G.sprGoodsInfoInOutkaDef.INOUTKA_CTL_NO_M.ColNo))

                With frm.sprGoodsInfoEdi.ActiveSheet

                    For j As Integer = 0 To maxEdi

                        ediLotNo = Me._LMHconV.GetCellValue(.Cells(i, LMH070G.sprGoodsInfoEdiDef.LOT_NO.ColNo))
                        ediGoodsCd = Me._LMHconV.GetCellValue(.Cells(i, LMH070G.sprGoodsInfoEdiDef.GOODS_CD_CUST.ColNo))

                        If ediLotNo.Equals(inoutLotNo) = True AndAlso ediGoodsCd.Equals(inoutGoodsCd) = True Then
                            If String.IsNullOrEmpty(Me._LMHconV.GetCellValue(.Cells(j, LMH070G.sprGoodsInfoEdiDef.LINK_NO.ColNo))) = True Then
                                Me._G.SetLinkNo(j, ediNo)
                                Exit For
                            End If
                        End If
                    Next

                End With

            Next

        End With

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function Hozon(ByVal frm As LMH070F) As Boolean

        Dim rtDs As DataSet
        Dim rtn As MsgBoxResult

        '続行確認
        rtn = Me.ShowMessage(frm, "C001", New String() {"紐付け処理"})

        If rtn = MsgBoxResult.Ok Then
        ElseIf rtn = MsgBoxResult.Cancel Then
            Call Me._LMHconH.EndAction(frm)
            Exit Function
        End If

        rtDs = Me.SetDataHozonInData(frm)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMH070BLF", "Hozon", rtDs)

        ''メッセージコードの判定
        'If MyBase.IsErrorMessageExist() = True Then
        '    '保存処理失敗時、返却メッセージを設定
        '    MyBase.ShowMessage(frm)
        '    Call Me._LMHconH.EndAction(frm)
        '    Exit Function
        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            MyBase.ShowMessage(frm, "E235")
            'EXCEL起動()
            MyBase.MessageStoreDownload()

            If rtnDs.Tables("WARNING_DTL").Rows.Count > 0 Then
                'ワーニングが設定されている場合
                Call Me.CallWarning(rtnDs, frm)
            End If

        ElseIf rtnDs.Tables("WARNING_DTL").Rows.Count > 0 Then

            'ワーニングが設定されている場合
            Call Me.CallWarning(rtnDs, frm)

        Else

            MyBase.ShowMessage(frm, "G002", New String() {"紐付け処理", String.Empty})
            Return True
        End If

    End Function


#Region "ワーニング画面呼出処理"
    Private Sub CallWarning(ByVal ds As DataSet, ByVal frm As LMH070F)

        Dim dtW As DataTable
        Dim dtIN As DataTable
        Dim drW As DataRow
        Dim drIN As DataRow
        Dim prm As LMFormData = New LMFormData

        If Me._InOutFlg.Equals("0") Then
            dtW = ds.Tables(LMH030C.TABLE_NM_WARNING_HED)
            dtIN = ds.Tables(LMH030C.TABLE_NM_IN)
        Else
            dtW = ds.Tables(LMH010C.TABLE_NM_WARNING_HED)
            dtIN = ds.Tables(LMH010C.TABLE_NM_IN)
        End If

        drW = dtW.NewRow()
        drIN = dtIN.Rows(0)

        If Me._InOutFlg.Equals("0") Then
            drW.Item("SYORI_KB") = LMH050C.SHORI_OUTKA_HIMODUKE '入荷紐付け
        Else
            drW.Item("SYORI_KB") = LMH050C.SHORI_INKA_HIMODUKE '出荷紐付け
        End If

        drW.Item("NRS_BR_CD") = frm.cmbNrsBr.SelectedValue
        drW.Item("WH_CD") = frm.cmbNrsWh.SelectedValue
        drW.Item("CUST_CD_L") = frm.txtCustCD_L.TextValue
        drW.Item("CUST_CD_M") = frm.txtCustCD_M.TextValue

        dtW.Rows.Add(drW)

        prm.ParamDataSet = ds
        LMFormNavigate.NextFormNavigate(Me, "LMH050", prm)

    End Sub

#End Region

#Region "検索条件データセット"
    ''' <summary>
    ''' 検索条件データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Function SetDataSelectInData(ByVal frm As LMH070F, ByVal rtDs As DataSet) As DataSet

        Dim inDr As DataRow = rtDs.Tables(LMH070C.TABLE_NM_IN).NewRow()


        '検索条件　単項目部

        inDr("INOUTKA_NO") = frm.txtInOutkaMngNo.TextValue
        inDr("NRS_BR_CD") = frm.cmbNrsBr.SelectedValue
        inDr("INOUT_KB") = Me._InOutFlg

        '検索条件をデータセットに設定
        rtDs.Tables(LMH070C.TABLE_NM_IN).Rows.Add(inDr)

        Return rtDs

    End Function

#End Region

#Region "保存データセット"
    ''' <summary>
    ''' 保存データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function SetDataHozonInData(ByVal frm As LMH070F) As DataSet

        Dim rtDs As DataSet
        Dim inTableNm As String = String.Empty
        Dim judgeTableNm As String = String.Empty
        Dim ctlNoNm As String = String.Empty
        Dim himodukeTableNm As String = String.Empty
        Dim rcvUpdDateNm As String = String.Empty
        Dim rcvUpdTimeNm As String = String.Empty

        If Me._InOutFlg.Equals("0") Then
            '出荷紐付け
            rtDs = New LMH030DS
            inTableNm = LMH030C.TABLE_NM_IN
            judgeTableNm = LMH030C.TABLE_NM_JUDGE
            ctlNoNm = "OUTKA_CTL_NO"
            himodukeTableNm = LMH030C.TABLE_NM_HIMODUKE
            rcvUpdDateNm = "RCV_SYS_UPD_DATE"
            rcvUpdTimeNm = "RCV_SYS_UPD_TIME"
        Else
            '入荷紐付け
            rtDs = New LMH010DS
            inTableNm = LMH010C.TABLE_NM_IN
            judgeTableNm = LMH010C.TABLE_NM_JUDGE
            ctlNoNm = "INKA_CTL_NO_L"
            himodukeTableNm = LMH010C.TABLE_NM_HIMODUKE
            rcvUpdDateNm = "RCV_UPD_DATE"
            rcvUpdTimeNm = "RCV_UPD_TIME"
        End If


        Dim saveDr As DataRow = rtDs.Tables(inTableNm).NewRow()
        Dim judgeDr As DataRow = rtDs.Tables(judgeTableNm).NewRow()
        Dim inDr As DataRow = _Ds.Tables(LMH070C.TABLE_NM_IN).Rows(0)
        Dim ediDr As DataRow = _SelDs.Tables(LMH070C.TABLE_NM_OUT_L).Rows(0)

        saveDr("EDI_CTL_NO") = frm.txtEdiMngNo.TextValue
        saveDr("NRS_BR_CD") = frm.cmbNrsBr.SelectedValue
        saveDr("WH_CD") = frm.cmbNrsWh.SelectedValue
        '2012.02.21 修正START
        saveDr("CUST_CD_L") = frm.txtCustCD_L.TextValue
        saveDr("CUST_CD_M") = frm.txtCustCD_M.TextValue
        '1レコード処理なので固定で１とする
        saveDr("ROW_NO") = "1"
        '2012.02.21 修正END
        saveDr("SYS_UPD_DATE") = ediDr("SYS_UPD_DATE")
        saveDr("SYS_UPD_TIME") = ediDr("SYS_UPD_TIME")
        saveDr(rcvUpdDateNm) = ediDr("RCV_UPD_DATE")
        saveDr(rcvUpdTimeNm) = ediDr("RCV_UPD_TIME")
        saveDr("EDI_CUST_INDEX") = inDr("EDI_CUST_INDEX")
        'saveDr(ctlNoNm) = frm.txtInOutkaMngNo.TextValue
        saveDr(ctlNoNm) = Me._InOutMngNo
        judgeDr("EVENT_SHUBETSU") = inDr("EVENT_SHUBETSU")

        rtDs.Tables(inTableNm).Rows.Add(saveDr)
        rtDs.Tables(judgeTableNm).Rows.Add(judgeDr)

        Dim max As Integer = frm.sprGoodsInfoEdi.ActiveSheet.RowCount - 1
        Dim himodukeDr As DataRow
        Dim himodukeDt As DataTable = rtDs.Tables(himodukeTableNm)

        For i As Integer = 0 To max

            With frm.sprGoodsInfoEdi.ActiveSheet

                himodukeDr = himodukeDt.NewRow()
                himodukeDr("EDI_CTL_NO_M") = Me._LMHconV.GetCellValue(.Cells(i, LMH070G.sprGoodsInfoEdiDef.EDI_NO.ColNo))
                himodukeDr("HIMODUKE_NO") = Me._LMHconV.GetCellValue(.Cells(i, LMH070G.sprGoodsInfoEdiDef.LINK_NO.ColNo))

                himodukeDt.Rows.Add(himodukeDr)

            End With
        Next

        Return rtDs

    End Function

#End Region

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMH070F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectEvent")

        '検索処理
        Call Me.ActionControl(LMH070C.EventShubetsu.KENSAKU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMH070F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "HimozukeEvent")

        Call Me.ActionControl(LMH070C.EventShubetsu.HOZON, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "HimozukeEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMH070F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class