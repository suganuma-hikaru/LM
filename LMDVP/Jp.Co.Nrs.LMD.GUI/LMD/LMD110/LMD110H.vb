' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD110H : 在庫振替検索
'  作  成  者       :  daikoku
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMD110ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMD110H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMD110V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMD110G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconV As LMDControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconH As LMDControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconG As LMDControlG

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    ''' 検索値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _DsSel As DataSet

    ''' <summary>
    '''検索条件格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FindDs As DataSet

    ''' <summary>
    ''' チェックパス格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _SelectArr As ArrayList

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' 印刷種類（Enum)格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrintSybetuEnum As LMD110C.PrintShubetsu

    ''' <summary>
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

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

        'フォームの作成
        Dim frm As LMD110F = New LMD110F(Me)

        'Validateクラスの設定
        Me._V = New LMD110V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMD110G(Me, frm)

        'Hnadler共通クラスの設定
        'Me._LMDconH = New LMDControlH(DirectCast(frm, Form))
        Me._LMDconH = New LMDControlH(DirectCast(frm, Form), MyBase.GetPGID())


        Me._LMDconV = New LMDControlV(Me, DirectCast(frm, Form))

        'Gamen共通クラスの設定
        'Me._LMDconG = New LMDControlG(DirectCast(frm, Form))
        Me._LMDConG = New LMDControlG(Me, DirectCast(frm, Form))

        'START YANAI 20120319　作業画面改造
        'Me._FlgSave = False
        'END YANAI 20120319　作業画面改造

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        '営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(frm.cmbEigyo, frm.cmbWare)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMD110C.MODE_DEFAULT)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetInitControl(Me.GetPGID(), frm)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        Me._G.SetInitValue(frm)

        '↓ データ取得の必要があればここにコーディングする。


        '↑ データ取得の必要があればここにコーディングする。

        'メッセージの表示
        Me.ShowMessage(frm, "G007")

        '画面の入力項目の制御
        Call _G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

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
    Friend Function ActionControl(ByVal eventShubetsu As LMD110C.EventShubetsu, ByVal frm As LMD110F) As Boolean

        '処理開始アクション
        Call Me._LMDconH.StartAction(frm)

        frm.lblMotoCustNM_L.TextValue = String.Empty
        frm.lblMotoCustNM_M.TextValue = String.Empty
        'キャッシュから名称取得
        Call Me.SetCachedName(frm)

        '権限チェック（共通）
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Call Me._LMDconH.EndAction(frm, Me.GetGMessage())
            Exit Function
        End If

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData

        'パラメータ設定
        prm.ReturnFlg = False
        Dim prmDs As DataSet = Nothing
        Dim row As DataRow = Nothing
        Dim errDs As DataSet = Nothing
        Dim errHashTable As Hashtable = New Hashtable

        Dim chkList As ArrayList = Me._V.getCheckList()

        'イベント種別による分岐
        Select Case eventShubetsu
            'Case LMD110C.EventShubetsu.SINKI
            '    '新規
            '    LMFormNavigate.NextFormNavigate(Me, "LMD010", New LMFormData())


            Case LMD110C.EventShubetsu.MASTER

                Call Me.MasterRefer(frm, prm, eventShubetsu)

                '*****検索処理******
            Case LMD110C.EventShubetsu.KENSAKU

                '項目チェック
                If Me._V.IsKensakuSingleCheck() = False Then
                    Call Me._LMDconH.EndAction(frm, Me.GetGMessage()) '終了処理
                    Exit Function
                End If

                '関連チェック
                If Me._V.IsKensakuKanrenCheck() = False Then
                    Call Me._LMDconH.EndAction(frm, Me.GetGMessage()) '終了処理
                    Exit Function
                End If

                '検索処理を行う
                Call Me.SelectData(frm)

                'フォーカスの設定
                Call Me._G.SetFoucus()

            Case LMD110C.EventShubetsu.PRINT

                '******************「印刷」******************'

                '初期化
                Me._SelectArr = New ArrayList()
                Me._PrintSybetuEnum = 0
                Dim temp As String = frm.cmbPrint.SelectedValue.ToString()

                Select Case temp

                    Case "01"
                        Me._PrintSybetuEnum = LMD110C.PrintShubetsu.FURIKAE

                End Select

                '入力チェック
                Me._ChkList = Me._V.getCheckList()
                If Me._V.IsPrintInputCheck(Me._PrintSybetuEnum, Me._ChkList) = False Then
                    Call Me._LMDconH.EndAction(frm, Me.GetGMessage()) '終了処理
                    Exit Function
                End If

                'Call Me.IsPrintChk(frm)

                ''印刷処理

                Select Case temp

                    Case "01"

                        Call SaiPrintRTN(frm)

                End Select


        End Select

        '処理終了アクション
        Call Me._LMDconH.EndAction(frm, Me.GetGMessage())

    End Function

#End Region '外部メソッド

#Region "印刷"
    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SaiPrintRTN(ByVal frm As LMD110F) As Boolean

        '印刷処理）
        Dim rtDs As DataSet = Me.SetLMD600InDataSet(frm)

        rtDs.Merge(New RdPrevInfoDS)
        'UPD 2021/11/15 025392【LMS】性能改善_DBリードオンリー設定_在庫系
        'Dim lmd600Ds As DataSet = MyBase.CallWSA("LMD110BLF", "PrintAction", rtDs)
        Dim lmd600Ds As DataSet = MyBase.CallWSA("LMD110BLF", "PrintAction", rtDs, True)

        If IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._LMDconH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Function
        End If

        'プレビュー判定 
        Dim prevDt As DataTable = lmd600Ds.Tables(LMConst.RD)
        If prevDt.Rows.Count > 0 Then

            'プレビューの生成 
            Dim prevFrm As New RDViewer()

            'データ設定 
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始 
            prevFrm.Run()

            'プレビューフォームの表示 
            prevFrm.Show()

        End If

        ''画面の入力項目の制御
        'Call Me._G.SetControlsStatus(LMD110C.ActionType.HENSHU)

        ''フォーカスの設定
        'Call Me._G.SetFoucus(LMD110C.ActionType.MAIN)

        Return True

    End Function

#End Region



#Region "内部メソッド"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByRef frm As LMD110F)

        'DataSet設定
        Dim rtDs As DataSet = New LMD110DS()
        Call Me.SetDataSetInData(frm, rtDs)

        '検索条件格納
        Me._FindDs = rtDs.Copy()
        'Me._SelectTp = String.Empty

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMDconH.CallWSAAction(DirectCast(frm, Form), _
                                                "LMD110BLF", "SelectListData", _FindDs _
                                        , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                        (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))) _
                                        , Convert.ToInt32(Convert.ToDouble( _
                                        MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                            .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))

        '検索成功時共通処理を行う
        If rtnDs IsNot Nothing Then

            Call Me.SuccessSelect(frm, rtnDs)

        End If

        'キャッシュから名称取得
        Call Me.SetCachedName(frm)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' キャッシュから名称取得（全項目）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetCachedName(ByVal frm As LMD110F)

        'With frm
        '    Dim NrsBrCd As String = .cmbEigyo.SelectedValue.ToString()
        '    Dim custCdL As String = frm.txtMotoCustCD_L.TextValue
        '    Dim custCdM As String = frm.txtMotoCustCD_M.TextValue

        '    '元荷主名称
        '    .lblMotoCustNM_L.TextValue = String.Empty
        '    .lblMotoCustNM_M.TextValue = String.Empty
        '    If String.IsNullOrEmpty(custCdL) = False Then
        '        If String.IsNullOrEmpty(custCdM) = True Then
        '            custCdM = "00"
        '        End If

        '        Dim custArray() As String = Me.GetCachedCust(NrsBrCd, custCdL, custCdM, "00", "00")

        '        .lblMotoCustNM_L.TextValue = custArray(0)
        '        .lblMotoCustNM_M.TextValue = custArray(1)
        '    End If

        'End With

    End Sub

    ''' <summary>
    ''' 荷主キャッシュから名称取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCachedCust(ByVal NrsBrCd As String, _
                                   ByVal custCdL As String, _
                                   ByVal custCdM As String, _
                                   ByVal custCdS As String, _
                                   ByVal custCdSS As String) As String()

        Dim dr As DataRow() = Nothing
        Dim custArray(1) As String

        '荷主名称
        '20160621 tsunehira  要番2491 add start 
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                                           "NRS_BR_CD = '", NrsBrCd, "' AND " _
                                                                         , "CUST_CD_L = '", custCdL, "' AND " _
                                                                         , "CUST_CD_M = '", custCdM, "' AND " _
                                                                         , "CUST_CD_S = '", custCdS, "' AND " _
                                                                         , "CUST_CD_SS = '", custCdSS, "' AND " _
                                                                         , "SYS_DEL_FLG = '0'"))
        '20160621 tsunehira  要番2491 add end
        If 0 < dr.Length Then
            custArray(0) = dr(0).Item("CUST_NM_L").ToString()
            custArray(1) = dr(0).Item("CUST_NM_M").ToString()
            Return custArray
        End If

        custArray(0) = String.Empty
        custArray(1) = String.Empty

        Return custArray

    End Function

#End Region '検索処理

#Region "マスタ参照"

    ''' <summary>
    ''' マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="prm"></param>
    ''' <remarks></remarks>
    Private Sub MasterRefer(ByVal frm As LMD110F, ByVal prm As LMFormData, ByVal Eventshubetsu As LMD110C.EventShubetsu)

        '現在フォーカスのあるコントロール名の取得
        Dim objNm As String = frm.FocusedControlName()

        'Pop起動処理
        Call Me.ShowPopup(frm, objNm, Eventshubetsu)

    End Sub

#End Region 'マスタ参照


#Region "検索成功時"
    ''' <summary>
    ''' 検索成功時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMD110F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMD110C.TABLE_NM_INOUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'SPREAD(表示行)初期化
        frm.sprFurrikae.CrearSpread()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        Me._CntSelect = dt.Rows.Count.ToString()

        '検索結果データセットを格納
        _DsSel = ds.Copy()

        If Me._CntSelect.Equals("0") = False Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G016", New String() {Me._CntSelect, String.Empty})

        End If

    End Sub
#End Region

#Region "検索条件データセット"
    ''' <summary>
    ''' 検索条件データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMD110F, ByVal rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LMD110C.TABLE_NM_IN).NewRow()

        '検索条件　単項目部
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("WH_CD") = frm.cmbWare.SelectedValue
        dr("FIRIKAE_DATE_FROM") = frm.imdFurikaeDate_S.TextValue.Trim()
        dr("FURIKAE_DATE_TO") = frm.imdFurikaeDate_E.TextValue.Trim()
        dr("MOTO_CUST_CD_L") = frm.txtMotoCustCD_L.TextValue.Trim()
        dr("MOTO_CUST_CD_M") = frm.txtMotoCustCD_M.TextValue.Trim()

        dr("SAKI_CUST_CD_L") = frm.txtSakiCustCD_L.TextValue.Trim()
        dr("SAKI_CUST_CD_M") = frm.txtSakiCustCD_M.TextValue.Trim()

        dr("FURI_KBN") = frm.cmFurikaeKBN.SelectedValue
        Dim youkiChange As String = String.Empty
        youkiChange = CStr(frm.cmYoukiKBN.SelectedValue)
        If youkiChange = "01" Then
            youkiChange = "20"      '変更有
        Else
            youkiChange = "30"      '変更無
        End If
        dr.Item("YOUKI_KBN") = youkiChange.ToString

        If frm.chkSelectByNrsB.Checked = True Then
            dr("MY_SELECT") = LMConst.FLG.ON
        Else
            dr("MY_SELECT") = LMConst.FLG.OFF
        End If
        dr("USER_ID") = LMUserInfoManager.GetUserID

        '検索条件　入力部（スプレッド）
        With frm.sprFurrikae.ActiveSheet

            dr("ORDER_NO") = Me._LMDconV.GetCellValue(.Cells(0, LMD110G.sprDetailDef.ORDER_NO.ColNo))
            dr("FUERI_NO") = Me._LMDconV.GetCellValue(.Cells(0, LMD110G.sprDetailDef.FURI_NO.ColNo))
            dr("MOTO_CUST_NM") = Me._LMDconV.GetCellValue(.Cells(0, LMD110G.sprDetailDef.MOTO_CUST_NM.ColNo))
            dr("SAKI_CUST_NM") = Me._LMDconV.GetCellValue(.Cells(0, LMD110G.sprDetailDef.SAKI_CUST_NM.ColNo))
            dr("SAKI_GOODS_NM_NRS") = Me._LMDconV.GetCellValue(.Cells(0, LMD110G.sprDetailDef.SAKI_GOODS_NM.ColNo))
            'dr("UP_DT") = Me._LMDconV.GetCellValue(.Cells(0, LMD110G.sprDetailDef.UPD_DT.ColNo))

        End With

        '検索条件をデータセットに設定
        rtDs.Tables(LMD110C.TABLE_NM_IN).Rows.Add(dr)

    End Sub
#End Region



#Region "保存成功時（複写、削除）データセット"
    Private Sub SetDataHozonSuccess(ByVal frm As LMD110F)

        Dim max As Integer = 0
        Dim setDt As DataTable = _DsSel.Tables(LMD110C.TABLE_NM_INOUT)
        Dim dtRow As Integer = 0

        With frm.sprFurrikae.ActiveSheet

            max = setDt.Rows.Count - 1

            For i As Integer = 0 To max

                If setDt.Rows(i)("SYS_DEL_FLG").ToString = "1" AndAlso setDt.Rows(i)("SAVE_FLG").ToString = "0" Then
                    setDt.Rows(i)("SAVE_FLG") = "1"
                End If

                If setDt.Rows(i)("COPY_FLG").ToString = "1" AndAlso setDt.Rows(i)("SAVE_FLG").ToString = "0" Then
                    setDt.Rows(i)("SAVE_FLG") = "1"
                End If

            Next

        End With

    End Sub
#End Region

#Region "コントロール初期化"
    Private Sub ClearControl(ByVal frm As LMD110F)

        With frm
            '.txtEditNum.TextValue = String.Empty
            '.txtEditTxt.TextValue = String.Empty
            '.cmbEditKbSkyu.TextValue = String.Empty
            '.cmbEditDate.TextValue = String.Empty
            '.cmbEditKbTax.TextValue = String.Empty
            '.lblEditNM.TextValue = String.Empty
        End With

    End Sub
#End Region

#Region "PopUp"


    ''' <summary>
    ''' マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function ShowPopup(ByVal frm As LMD110F, ByVal objNM As String, ByVal eventshubetsu As LMD110C.EventShubetsu) As Boolean

        If Me._V.IsPopSingleCheck(objNM) = False Then
            Exit Function
        End If

        With frm

            Select Case objNM

                Case .txtMotoCustCD_L.Name, .txtMotoCustCD_M.Name               '荷主マスタ参照

                    If String.IsNullOrEmpty(.txtMotoCustCD_L.TextValue) = True Then
                        .lblMotoCustNM_L.TextValue = String.Empty
                        .lblMotoCustNM_M.TextValue = String.Empty
                    End If

                    '荷主マスタ参照POP起動
                    Call Me.SetMstResult(frm, eventshubetsu, objNM)

                    'メッセージの表示
                    Me.ShowMessage(frm, "G007")

                Case .txtSakiCustCD_L.Name, .txtSakiCustCD_M.Name               '荷主マスタ参照

                    If String.IsNullOrEmpty(.txtSakiCustCD_L.TextValue) = True Then
                        .lblSakiCustNM_L.TextValue = String.Empty
                        .lblSakiCustNM_M.TextValue = String.Empty
                    End If

                    '荷主マスタ参照POP起動
                    Call Me.SetMstResult(frm, eventshubetsu, objNM)

                    'メッセージの表示
                    Me.ShowMessage(frm, "G007")

                Case Else
                    'メッセージの表示
                    Me.ShowMessage(frm, "G005")


            End Select
        End With

        Return True

    End Function

#Region "荷主マスタ"

    ''' <summary>
    ''' 荷主マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetMstResult(ByVal frm As LMD110F, ByVal EventShubetsu As LMD110C.EventShubetsu, ByVal objNM As String) As Boolean

        Dim prm As LMFormData = Me.SetMstResultByVal(frm, EventShubetsu, objNM)
        If prm.ReturnFlg = True Then

            'PopUpから取得したデータをコントロールにセット()
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

            With frm

                Select Case objNM
                    Case .txtMotoCustCD_L.Name, .txtMotoCustCD_M.Name
                        frm.txtMotoCustCD_L.TextValue = dr.Item("CUST_CD_L").ToString    '荷主コード（大）
                        frm.lblMotoCustNM_L.TextValue = dr.Item("CUST_NM_L").ToString    '荷主名（大）
                        frm.txtMotoCustCD_M.TextValue = dr.Item("CUST_CD_M").ToString    '荷主コード（中）
                        frm.lblMotoCustNM_M.TextValue = dr.Item("CUST_NM_M").ToString    '荷主名（大）

                    Case .txtSakiCustCD_L.Name, .txtSakiCustCD_M.Name
                        frm.txtSakiCustCD_L.TextValue = dr.Item("CUST_CD_L").ToString    '荷主コード（大）
                        frm.lblSakiCustNM_L.TextValue = dr.Item("CUST_NM_L").ToString    '荷主名（大）
                        frm.txtSakiCustCD_M.TextValue = dr.Item("CUST_CD_M").ToString    '荷主コード（中）
                        frm.lblSakiCustNM_M.TextValue = dr.Item("CUST_NM_M").ToString    '荷主名（大）


                End Select
            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">コントロール</param>
    ''' <remarks></remarks>
    Private Function SetMstResultByVal(ByVal frm As LMD110F, ByVal EventShubetsu As LMD110C.EventShubetsu, ByVal objNM As String) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        Dim sCustL As String = String.Empty
        Dim sCustM As String = String.Empty

        With frm

            Select Case objNM
                Case .txtMotoCustCD_L.Name, .txtMotoCustCD_M.Name
                    sCustL = frm.txtMotoCustCD_L.TextValue.Trim()
                    sCustM = frm.txtMotoCustCD_M.TextValue.Trim()

                Case .txtSakiCustCD_L.Name, .txtSakiCustCD_M.Name
                    sCustL = frm.txtSakiCustCD_L.TextValue.Trim()
                    sCustM = frm.txtSakiCustCD_M.TextValue.Trim()

            End Select
        End With

        With dr

            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue().ToString()
            'If EventShubetsu = LMD110C.EventShubetsu.ENTER Then

            .Item("CUST_CD_L") = sCustL.ToString
            .Item("CUST_CD_M") = sCustM.ToString

            ''End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S   '検証結果(メモ)№77対応(2011.09.12)

        End With

        dt.Rows.Add(dr)

        'Pop起動
        'Return Me._LMDconH.FormShow(ds, Me._PopupSkipFlg, "LMZ260")
        Return Me._LMDconH.FormShow(ds, "LMZ260")

    End Function

#End Region

#Region "初期荷主変更"

    ''' <summary>
    ''' 初期荷主変更参照POP起動
    ''' </summary>
    ''' <param name="frm">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowDefPopup(ByVal frm As LMD110F) As LMFormData

        Dim ds As DataSet = New LMZ010DS()
        Dim dt As DataTable = ds.Tables(LMZ010C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr

            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With
        dt.Rows.Add(dr)

        'Pop起動
        'Dim prm As LMFormData = Me._LMDconH.FormShow(ds, Me._PopupSkipFlg, "LMZ010")
        Dim prm As LMFormData = Me._LMDconH.FormShow(ds, "LMZ010")

        '戻り処理
        If prm.ReturnFlg = True Then
            With prm.ParamDataSet.Tables(LMZ010C.TABLE_NM_OUT).Rows(0)
                frm.txtMotoCustCD_L.TextValue = .Item("CUST_CD_L").ToString    '荷主コード（大）
                frm.txtMotoCustCD_M.TextValue = .Item("CUST_CD_M").ToString    '荷主コード（中）
                frm.lblMotoCustNM_L.TextValue = .Item("CUST_NM_L").ToString    '荷主名（大）
                frm.lblMotoCustNM_M.TextValue = .Item("CUST_NM_M").ToString    '荷主名（中）
            End With
        End If

        Return prm

    End Function

#End Region


    ''' <summary>
    ''' 行複写時、作業項目マスタ参照POP起動
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ShowSagyoPopup(ByVal frm As LMD110F, ByVal nrsBrCd As String, ByVal custCd As String) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ200DS()
        Dim dt As DataTable = ds.Tables(LMZ200C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = nrsBrCd
            .Item("CUST_CD_L") = custCd
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SAGYO_CNT") = "1"
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        'Pop起動
        'Return Me.PopFormShow(prm, "LMZ200")
        LMFormNavigate.NextFormNavigate(Me, "LMZ200", prm)

        Return prm

    End Function

#Region "ROW_NO取得"
    ''' <summary>
    ''' ROW_NO取得
    ''' </summary>
    ''' <param name="rowNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDrFromRowNo(ByVal rowNo As Integer) As Integer
        Dim dt As DataTable = _DsSel.Tables(LMD110C.TABLE_NM_INOUT)
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max
            If Convert.ToInt32(dt.Rows(i)("ROW_NO")) = rowNo Then
                Return i
            End If
        Next
    End Function
#End Region

#Region "スプレッド行削除"
    ''' <summary>
    ''' スプレッドの行削除
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub DelSpread(ByVal frm As LMD110F)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim selectRow As Integer = 0
        Dim max As Integer = chkList.Count - 1

        With frm.sprFurrikae.ActiveSheet

            For i As Integer = max To 0 Step -1
                selectRow = Convert.ToInt32(chkList(i))
                .RemoveRows(selectRow, 1)
            Next

        End With

    End Sub
#End Region

#Region "データセットの中から削除したデータを削除"
    'START YANAI 20120319　作業画面改造
    ''' <summary>
    ''' データセットの中から削除したデータを削除
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub DelDataSet(ByVal frm As LMD110F)

        Dim outDr As DataRow() = Me._DsSel.Tables(LMD110C.TABLE_NM_INOUT).Select("SYS_DEL_FLG = '1'")
        Dim max As Integer = outDr.Length - 1

        For i As Integer = 0 To max
            Me._DsSel.Tables(LMD110C.TABLE_NM_INOUT).Rows.Remove(outDr(i))
        Next

    End Sub
    'END YANAI 20120319　作業画面改造
#End Region

#Region "エラーEXCEL出力のデータセット設定"

    ''' <summary>
    ''' エラーEXCEL出力データセット設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function ExcelErrorSet(ByRef ds As DataSet) As DataSet

        Dim max As Integer = ds.Tables("LMD110_GUIERROR").Rows.Count() - 1
        Dim dr As DataRow
        Dim prm1 As String = String.Empty
        Dim prm2 As String = String.Empty
        Dim prm3 As String = String.Empty
        Dim prm4 As String = String.Empty
        Dim prm5 As String = String.Empty

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        For i As Integer = 0 To max

            dr = ds.Tables("LMD110_GUIERROR").Rows(i)

            If String.IsNullOrEmpty(dr("PARA1").ToString()) = False Then
                prm1 = dr("PARA1").ToString()
            End If
            If String.IsNullOrEmpty(dr("PARA2").ToString()) = False Then
                prm2 = dr("PARA2").ToString()
            End If
            If String.IsNullOrEmpty(dr("PARA3").ToString()) = False Then
                prm3 = dr("PARA3").ToString()
            End If
            If String.IsNullOrEmpty(dr("PARA4").ToString()) = False Then
                prm4 = dr("PARA4").ToString()
            End If
            If String.IsNullOrEmpty(dr("PARA5").ToString()) = False Then
                prm5 = dr("PARA5").ToString()
            End If
            MyBase.SetMessageStore(dr("GUIDANCE_ID").ToString() _
                     , dr("MESSAGE_ID").ToString() _
                     , New String() {prm1, prm2, prm3, prm4, prm5} _
                     , dr("ROW_NO").ToString() _
                     , dr("KEY_NM").ToString() _
                     , dr("KEY_VALUE").ToString())

        Next

        Return ds

    End Function

#End Region

#Region "EXCEL出力処理"
    Private Sub OutputExcel(ByVal frm As LMD110F)

        MyBase.ShowMessage(frm, "E235")
        'EXCEL起動()
        MyBase.MessageStoreDownload()

    End Sub

#End Region

#Region "削除処理"
    'START YANAI 20120319　作業画面改造
    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMD110BLF", "DeleteData", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteData")

        Return rtnDs

    End Function
    'END YANAI 20120319　作業画面改造
#End Region

#Region "新規ボタン押下時"
    ''' <summary>
    ''' 新規押下時、作業ポップを呼び出し⇒作業明細編集画面に遷移
    ''' </summary>
    ''' <param name="frm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function ShowLMD110sinki(ByVal frm As LMD110F, ByVal objNm As String, ByVal eventshubetsu As LMD110C.EventShubetsu) As Boolean

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        Dim prmDs As DataSet = New LMD010DS

        prm.ParamDataSet = prmDs
        prm.RecStatus = RecordStatus.NEW_REC

        '画面遷移
        LMFormNavigate.NextFormNavigate(Me, "LMD010", prm)

        Return True


    End Function
    'END YANAI 20120319　作業画面改造
#End Region

#End Region

#Region "スプレッドダブルクリック時"
    'START YANAI 20120319　作業画面改造
    ''' <summary>
    ''' ダブルクリック時、作業明細編集画面に遷移
    ''' </summary>
    ''' <param name="frm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function ShowLMD110henshu(ByVal frm As LMD110F) As Boolean

        Dim rowNo As Integer = frm.sprFurrikae.Sheets(0).ActiveRow.Index '選択行番号

        'inputDataSet作成
        Dim ds As DataSet = New LMD110DS()
        Dim indr As DataRow = ds.Tables(LMD110C.TABLE_NM_IN).NewRow()

        With frm.sprFurrikae.ActiveSheet
            indr.Item("NRS_BR_CD") = Me._LMDconV.GetCellValue(.Cells(rowNo, LMD110G.sprDetailDef.NRS_BR_CD.ColNo)) '営業所
            'indr.Item("SAGYO_REC_NO") = Me._LMDconV.GetCellValue(.Cells(rowNo, LMD110G.sprDetailDef.SAGYO_REC_NO.ColNo)) '作業レコード番号
            indr.Item("WH_CD") = String.Empty '倉庫コード
            indr.Item("SAGYO_CD") = String.Empty '作業コード
            indr.Item("SAGYO_NM") = String.Empty '作業名
            indr.Item("CUST_CD_L") = String.Empty '荷主コード(大)
            indr.Item("RENZOKU_FLG") = "00" '連続入力フラグ
            ds.Tables(LMD110C.TABLE_NM_IN).Rows.Add(indr)
        End With

        Dim prmDs As DataSet = ds
        Dim prm As LMFormData = New LMFormData()
        prm.ParamDataSet = prmDs
        prm.RecStatus = RecordStatus.NOMAL_REC

        'モーダレスなので画面ロック必要なし
        Call Me._LMDconH.EndAction(frm, Me.GetGMessage()) '終了処理

        '画面遷移
        LMFormNavigate.NextFormNavigate(Me, "LMD110", prm)

        MyBase.ShowMessage(frm, "G007")

        Return True

    End Function
    'END YANAI 20120319　作業画面改造
#End Region

#Region "連続ボタン押下時"
    'START YANAI 20120319　作業画面改造
    ''' <summary>
    ''' 連続ボタン押下時、作業明細編集画面に遷移
    ''' </summary>
    ''' <param name="frm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function ShowLMD110renzoku(ByVal frm As LMD110F) As Boolean

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count - 1

        'inputDataSet作成
        Dim ds As DataSet = New LMD110DS()
        Dim indr As DataRow = ds.Tables(LMD110C.TABLE_NM_IN).NewRow()

        With frm.sprFurrikae.ActiveSheet

            For i As Integer = 0 To max
                indr = ds.Tables(LMD110C.TABLE_NM_IN).NewRow()

                indr.Item("NRS_BR_CD") = Me._LMDconV.GetCellValue(.Cells(Convert.ToInt32(chkList(i)), LMD110G.sprDetailDef.NRS_BR_CD.ColNo)) '営業所
                'indr.Item("SAGYO_REC_NO") = Me._LMDconV.GetCellValue(.Cells(Convert.ToInt32(chkList(i)), LMD110G.sprDetailDef.SAGYO_REC_NO.ColNo)) '作業レコード番号
                indr.Item("WH_CD") = String.Empty '倉庫コード
                indr.Item("SAGYO_CD") = String.Empty '作業コード
                indr.Item("SAGYO_NM") = String.Empty '作業名
                indr.Item("CUST_CD_L") = String.Empty '荷主コード(大)
                indr.Item("RENZOKU_FLG") = "01" '連続入力フラグ

                ds.Tables(LMD110C.TABLE_NM_IN).Rows.Add(indr)

            Next

        End With

        Dim prmDs As DataSet = ds
        Dim prm As LMFormData = New LMFormData()
        prm.ParamDataSet = prmDs
        prm.RecStatus = RecordStatus.NOMAL_REC

        'モーダレスなので画面ロック必要なし
        Call Me._LMDconH.EndAction(frm, Me.GetGMessage()) '終了処理

        '画面遷移
        LMFormNavigate.NextFormNavigate(Me, "LMD110", prm)

        MyBase.ShowMessage(frm, "G007")

        Return True

    End Function

#End Region

#Region "四捨五入"
    ''' <summary>
    ''' 四捨五入
    ''' </summary>
    ''' <param name="decValue"></param>
    ''' <param name="iDigits"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ToRound(ByVal decValue As Decimal, ByVal iDigits As Integer) As Decimal

        Dim dCoef As Double = System.Math.Pow(10, iDigits)

        If decValue > 0 Then
            Return Convert.ToDecimal(Math.Floor((decValue * dCoef) + 0.5) / dCoef)
        Else
            Return Convert.ToDecimal(Math.Ceiling((decValue * dCoef) - 0.5) / dCoef)
        End If
    End Function
#End Region

#End Region

#Region "印刷"



#If False Then


    ''' <summary>
    ''' 印刷用データセット作成
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetLME500InDataSet(ByVal frm As LMD110F) As DataSet

        Dim inDs As DSL.LME500DS = New DSL.LME500DS
        Dim dt As DataTable = inDs.Tables("LME500IN")
        Dim dr As DataRow = dt.NewRow()

        With frm

            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue()
            dr.Item("WH_CD") = .cmbWare.SelectedValue()
            dr.Item("CUST_CD_L") = .txtCustCD_L.TextValue.Trim()
            dr.Item("CUST_CD_M") = .txtCustCD_M.TextValue.Trim()
            dr.Item("SEIQTO_CD") = .txtSeikyuCD.TextValue.Trim()
            dr.Item("SAGYO_CD") = .txtSagyoCD.TextValue.Trim()
            dr.Item("SAGYO_SIJI_NO") = .txtSagyoSijiNO.TextValue.Trim()
            dr.Item("F_DATE") = Format(.imdSagyoDate_S.Value, "yyyyMMdd")
            dr.Item("T_DATE") = Format(.imdSagyoDate_E.Value, "yyyyMMdd")
            '2次対応 作業料明細書・チェックリストの切替 2012.01.18 START
            dr.Item("PRT_SHUBETU") = .cmbPrint.SelectedValue()
            '2次対応 作業料明細書・チェックリストの切替 2012.01.18 END

        End With

        dt.Rows.Add(dr)

        inDs.Merge(New RdPrevInfoDS)

        Return inDs

    End Function
#End If

    Private Function GetChkDs(ByVal frm As LMD110F) As DataSet

        Dim chkDs As DataSet = New LMD110DS
        Dim inDr As DataRow = chkDs.Tables("LMD110IN").NewRow()

        With frm
            inDr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue()
            inDr.Item("CUST_CD_L") = .txtMotoCustCD_L.TextValue.Trim()
            inDr.Item("CUST_CD_M") = .txtMotoCustCD_M.TextValue.Trim()
            'inDr.Item("SEIQTO_CD") = .txtSeikyuCD.TextValue.Trim()
        End With

        chkDs.Tables("LMD110IN").Rows.Add(inDr)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectPrintCheck")
        '==== WSAクラス呼出 ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMD110BLF", "SelectPrintCheck", chkDs)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectPrintCheck")

        Return rtnDs

    End Function

    ''' <summary>
    ''' 振替伝票用データセット作成
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetLMD600InDataSet(ByVal frm As LMD110F) As DataSet

        Dim inDs As DSL.LMD600DS = New DSL.LMD600DS
        Dim dt As DataTable = inDs.Tables("LMD600IN")

        Dim dr As DataRow = Nothing
        Dim flg As Boolean = True
        Dim spr As Win.Spread.LMSpread = frm.sprFurrikae
        Dim max As Integer = Me._ChkList.Count() - 1
        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            With spr.ActiveSheet

                '変換ミスはサーバに渡さない
                flg = Integer.TryParse(Me._ChkList(i).ToString(), rowNo)
                If flg = False Then
                    Continue For
                End If

                dr = dt.NewRow()
                dr.Item("NRS_BR_CD") = .Cells(rowNo, LMD110G.sprDetailDef.NRS_BR_CD.ColNo).Value()
                dr.Item("FURI_NO") = .Cells(rowNo, LMD110G.sprDetailDef.FURI_NO.ColNo).Value()
                dr.Item("FURIKAEBI") = .Cells(rowNo, LMD110G.sprDetailDef.FURI_SYS_ENT_DATE.ColNo).Value().ToString.Replace("/", "")

                dr.Item("YOUKI_HENKO") = .Cells(rowNo, LMD110G.sprDetailDef.YOUKI_HENKO_KBN.ColNo).Value()
                dr.Item("OUT_TAX_KB") = .Cells(rowNo, LMD110G.sprDetailDef.OUT_TAX_KB.ColNo).Value()

                dr.Item("FURIKAE_KBN") = .Cells(rowNo, LMD110G.sprDetailDef.FURI_KBN.ColNo).Value()

                dt.Rows.Add(dr)

            End With

        Next


        Return inDs

    End Function
#End Region '印刷

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseForm(ByVal frm As LMD110F, ByVal e As FormClosingEventArgs)

        ''未保存データがない場合、処理終了
        'If Me._FlgSave = False Then
        '    Exit Sub
        'End If

        'START YANAI 20120319　作業画面改造
        ''メッセージの表示
        'Select Case MyBase.ShowMessage(frm, "W002")

        '    Case MsgBoxResult.Yes '「はい」押下時

        '        '保存処理
        '        If Me.ActionControl(LMD110C.EventShubetsu.HOZON, frm) = False Then
        '            e.Cancel = True
        '        Else
        '            e.Cancel = False
        '        End If


        '    Case MsgBoxResult.Cancel '「キャンセル」押下時

        '        e.Cancel = True

        'End Select
        'END YANAI 20120319　作業画面改造

    End Sub

    ''' <summary>
    ''' ガイダンスメッセージを取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGMessage() As String
        Return "G007"
    End Function
    ''' <summary>
    ''' F1押下時処理呼び出し(新規)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMD110F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "Shinki")

        '「新規」処理
        Call Me.ActionControl(LMD110C.EventShubetsu.SINKI, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "Shinki")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMD110F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Search")

        '検索処理
        Me.ActionControl(LMD110C.EventShubetsu.KENSAKU, frm)

        Logger.EndLog(Me.GetType.Name, "Search")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMD110F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Master")

        'F10押下時イベント：１件時表示あり
        Me._PopupSkipFlg = True
        Me.ActionControl(LMD110C.EventShubetsu.MASTER, frm)


        Logger.EndLog(Me.GetType.Name, "Master")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMD110F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LMD110F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    Friend Sub cmbEditList_SelectedValueChanged(ByVal frm As LMD110F)

        '値のクリア
        Call Me.ClearControl(frm)

        'コントロールの設定
        'Call Me._G.SetControl(frm)

    End Sub

#End Region 'イベント振分け


#End Region 'Method

End Class