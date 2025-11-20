' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDIサブシステム
'  プログラムID     :  LMH080H : EDI出荷データ検索
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Utility
Imports System.Text
Imports System.IO

''' <summary>
''' LMH080ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMH080H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMH080V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMH080G

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

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    '''検索条件格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FindDs As DataSet

    ''' <summary>
    ''' 印刷種類格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrintSybetu As String

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet = New LMH080DS

    ''' <summary>
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

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
        Dim frm As LMH080F = New LMH080F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'Validateクラスの設定
        Me._V = New LMH080V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMH080G(Me, frm)

        Me._LMHconG = New LMHControlG(frm)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMH080C.MODE_DEFAULT)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Dim sysdate As String() = MyBase.GetSystemDateTime()
        Call Me._G.SetControl(MyBase.GetPGID(), frm, sysdate(0), prmDs)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()
        Call Me._G.SetInitValue(frm)

        '↓ データ取得の必要があればここにコーディングする。

        '↑ データ取得の必要があればここにコーディングする。

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G007")

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()


        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'Validate共通クラスの設定
        Me._LMHconV = New LMHControlV(Me, DirectCast(frm, Form), Me._LMHconG)

        'Hnadler共通クラスの設定
        Me._LMHconH = New LMHControlH(DirectCast(frm, Form), MyBase.GetPGID())

        'Gamen共通クラスの設定
        Me._LMHconG = New LMHControlG(DirectCast(frm, Form))

    End Sub

#End Region '初期処理

#Region "外部Method"
    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMH080C.EventShubetsu, ByVal frm As LMH080F)

        '処理開始アクション
        Call Me._LMHconH.StartAction(frm)

        '権限チェック（共通）
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Call Me._LMHconH.EndAction(frm)
            Exit Sub
        End If

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData

        'パラメータ設定
        prm.ReturnFlg = False
        Dim prmDs As DataSet = Nothing
        Dim row As DataRow = Nothing

        Dim chkList As ArrayList = Me._V.getCheckList()

        'イベント種別による分岐
        Select Case eventShubetsu

            '*****検索処理******
            Case LMH080C.EventShubetsu.KENSAKU

                '項目チェック
                If Me._V.IsKensakuSingleCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '関連チェック
                If Me._V.IsKensakuKanrenCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If


                '検索処理を行う
                Call Me.SelectData(frm, "NEW")

                'フォーカスの設定
                Call Me._G.SetFoucus()

                '*****削除処理******
            Case LMH080C.EventShubetsu.Delete

                '項目チェック
                If Me._V.IsDeleteSingleCheck() = False Then
                    Call Me._LMHconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                Call Me.DeleteUTI(frm)

        End Select

        '処理終了アクション
        Call Me._LMHconH.EndAction(frm)

    End Sub

#End Region '外部メソッド

#Region "内部メソッド"

#Region "EXCEL出力処理"
    Private Sub OutputExcel(ByVal frm As LMH080F)

        MyBase.ShowMessage(frm, "E235")
        'EXCEL起動()
        MyBase.MessageStoreDownload()

    End Sub


#End Region

#Region "検索処理"
    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMH080F, ByVal reFlg As String)

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        'DataSet設定
        Dim rtDs As DataSet = New LMH080DS()

        If reFlg.Equals("NEW") Then
            '新規検索の場合
            Call Me.SetDataSetInData(frm, rtDs)

        ElseIf reFlg.Equals("RE") Then
            '再検索の場合
            rtDs = Me._FindDs
        End If

        'SPREAD(表示行)初期化
        frm.sprEdiList.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMHconH.CallWSAAction(DirectCast(frm, Form), _
                                        "LMH080BLF", "SelectListData", rtDs _
                                         , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                         (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '03'")(0).Item("VALUE1"))) _
                                         , Convert.ToInt32(Convert.ToDouble( _
                                         MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                        .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))

        '検索成功時共通処理を行う
        If rtnDs IsNot Nothing Then

            Call Me.SuccessSelect(frm, rtnDs, reFlg)

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()

    End Sub


#End Region

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="checkbool"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OptReturnString(ByVal checkbool As Boolean) As String

        If checkbool = True Then
            Return "1"
        End If

        Return ""

    End Function

#Region "削除処理"
    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub DeleteUTI(ByVal frm As LMH080F)

        'DataSet設定
        Dim rtDs As DataSet = New LMH080DS()
        Call Me.SetDataSetInDel(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateDelUTI")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMH080BLF", "UpdateDelUTI", rtDs)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            Call Me.OutputExcel(frm)

        Else
            Me.DeleteUTI(frm, rtDs)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateDelUTI")

        Call Me._LMHconH.EndAction(frm)

    End Sub

#End Region '一括変更

#Region "DataSet設定"

#Region "検索時"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMH080F, ByRef rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LMH080C.TABLE_NM_IN).NewRow()

        '検索条件　単項目部
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("CUST_CD_L") = frm.txtCustCD_L.TextValue
        dr("CUST_CD_M") = frm.txtCustCD_M.TextValue
        dr("CRT_DATE_FROM") = frm.imdEdiDateFrom.TextValue
        dr("CRT_DATE_TO") = frm.imdEdiDateTo.TextValue
        dr("VIEW_KB1") = Me.OptReturnString(frm.optMikakunin.Checked)
        dr("VIEW_KB2") = Me.OptReturnString(frm.optKakuninZumi.Checked)
        dr("VIEW_KB3") = Me.OptReturnString(frm.optSoshinZumi.Checked)
        dr("VIEW_KB4") = Me.OptReturnString(frm.optSakujoZumi.Checked)
        dr("VIEW_KB5") = Me.OptReturnString(frm.optAll.Checked)

        '検索条件　入力部（スプレッド）
        With frm.sprEdiList.ActiveSheet

            dr("DELIVERY_NO") = Me._LMHconV.GetCellValue(.Cells(0, LMH080G.sprEdiListDef.DELIV_NO.ColNo))
            dr("GOODS_CD") = Me._LMHconV.GetCellValue(.Cells(0, LMH080G.sprEdiListDef.GOODS_CD.ColNo))
            dr("GOODS_NM") = Me._LMHconV.GetCellValue(.Cells(0, LMH080G.sprEdiListDef.GOODS_NM.ColNo))
            dr("QT_UT") = Me._LMHconV.GetCellValue(.Cells(0, LMH080G.sprEdiListDef.QT_UT.ColNo))
            dr("LOT_NO") = Me._LMHconV.GetCellValue(.Cells(0, LMH080G.sprEdiListDef.LOT_NO.ColNo))
            dr("DEST_NM") = Me._LMHconV.GetCellValue(.Cells(0, LMH080G.sprEdiListDef.DEST_NM.ColNo))
            dr("PKG_UT") = Me._LMHconV.GetCellValue(.Cells(0, LMH080G.sprEdiListDef.PKG_UT.ColNo))
            dr("INKA_CTL_NO_L") = Me._LMHconV.GetCellValue(.Cells(0, LMH080G.sprEdiListDef.INKA_CTL_NO_L.ColNo))
            dr("FILE_NAME") = Me._LMHconV.GetCellValue(.Cells(0, LMH080G.sprEdiListDef.FILE_NAME.ColNo))
            dr("REC_NO") = Me._LMHconV.GetCellValue(.Cells(0, LMH080G.sprEdiListDef.DATA_SEQ.ColNo))
        End With

        '検索条件をデータセットに設定
        rtDs.Tables(LMH080C.TABLE_NM_IN).Rows.Add(dr)

        '再検索用データセットに保存
        Me._FindDs = rtDs

    End Sub

#End Region

#Region "削除選択行データセット"
    ''' <summary>
    ''' 削除選択行データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInDel(ByVal frm As LMH080F, ByVal rtDs As DataSet)

        Dim chkList As ArrayList = Me._V.GetCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0

        With frm.sprEdiList.ActiveSheet

            For i As Integer = 0 To chkList.Count - 1

                selectRow = Convert.ToInt32(chkList(i))
                dr = rtDs.Tables(LMH080C.TABLE_NM_INDEL).NewRow()

                dr("NRS_BR_CD") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH080G.sprEdiListDef.NRS_BR_CD.ColNo))
                dr("CUST_CD_L") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH080G.sprEdiListDef.CUST_CD_L.ColNo))
                dr("CUST_CD_M") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH080G.sprEdiListDef.CUST_CD_M.ColNo))
                dr("DELIVERY_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH080G.sprEdiListDef.DELIV_NO.ColNo))
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH080G.sprEdiListDef.SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH080G.sprEdiListDef.SYS_UPD_TIME.ColNo))
                dr("ROW_NO") = selectRow
                dr("CRT_DATE") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH080G.sprEdiListDef.CRT_DATE.ColNo)).Replace("/", "")
                dr("FILE_NAME") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH080G.sprEdiListDef.FILE_NAME.ColNo))
                dr("REC_NO") = Me._LMHconV.GetCellValue(.Cells(selectRow, LMH080G.sprEdiListDef.DATA_SEQ.ColNo))

                'データセットに設定
                rtDs.Tables(LMH080C.TABLE_NM_INDEL).Rows.Add(dr)

            Next

        End With

    End Sub
#End Region

#Region "エラーEXCEL出力のデータセット設定"

    ''' <summary>
    ''' エラーEXCEL出力データセット設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function ExcelErrorSet(ByRef ds As DataSet) As DataSet

        Dim max As Integer = ds.Tables("LMH080_GUIERROR").Rows.Count() - 1
        Dim dr As DataRow
        Dim prm1 As String = String.Empty
        Dim prm2 As String = String.Empty
        Dim prm3 As String = String.Empty
        Dim prm4 As String = String.Empty
        Dim prm5 As String = String.Empty

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        For i As Integer = 0 To max

            dr = ds.Tables("LMH080_GUIERROR").Rows(i)

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

#End Region 'DataSet設定

#Region "検索成功時"

    ''' <summary>
    ''' 検索成功時共通処理（画面別）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMH080F, ByVal ds As DataSet, ByVal reFlg As String)

        Dim dt As DataTable = ds.Tables(LMH080C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        ''SPREAD(表示行)初期化
        frm.sprEdiList.CrearSpread()

        Me._CntSelect = dt.Rows.Count.ToString()

        'データテーブルのカウントを設定
        Dim cnt As Integer = dt.Rows.Count()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        'カウントが0件以上の時メッセージの上書き
        If cnt > 0 Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G016", New String() {Me._CntSelect})

        End If

    End Sub

#End Region

#Region "削除成功時"
    ''' <summary>
    ''' 削除時処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub DeleteUTI(ByVal frm As LMH080F, ByVal ds As DataSet)
        MyBase.ShowMessage(frm, "G035", New String() {"削除", String.Empty})
    End Sub

#End Region

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMH080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")
    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMH080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")
    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMH080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")
    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMH080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "DeleteEvent")

        '検索処理
        Call ActionControl(LMH080C.EventShubetsu.Delete, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "DeleteEvent")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMH080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMH080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMH080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し(実績取消)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMH080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMH080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "SelectDataEvent")

        '検索処理
        Call ActionControl(LMH080C.EventShubetsu.KENSAKU, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "SelectDataEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMH080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(初期荷主変更)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMH080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMH080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMH080F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMH080F) As Boolean

        Return True

    End Function

#End Region 'イベント振分け

#End Region 'Method

End Class