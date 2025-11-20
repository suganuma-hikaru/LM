' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI511V : JNC EDI
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.DSL
Imports Microsoft.Office.Interop

''' <summary>
''' LMI511Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI511V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI511F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMIControlV

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMIControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI511F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm
        Me._Frm = frm
        Me._Gcon = New LMIControlG(frm)

        'Validate共通クラスの設定
        Me._Vcon = New LMIControlV(handlerClass, DirectCast(frm, Form), Me._Gcon)

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "権限チェック"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI511C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu
            Case LMI511C.EventShubetsu.OUTKASAVE
                '出荷登録
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW : kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP : kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER : kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER : kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT : kengenFlg = False
                End Select

            Case LMI511C.EventShubetsu.EDIT
                '編集
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW : kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP : kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER : kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER : kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT : kengenFlg = False
                End Select

            Case LMI511C.EventShubetsu.MTMSAVE
                'まとめ指示
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW : kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP : kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER : kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER : kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT : kengenFlg = False
                End Select

            Case LMI511C.EventShubetsu.MTMCANCEL
                'まとめ解除
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW : kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP : kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER : kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER : kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT : kengenFlg = False
                End Select

            Case LMI511C.EventShubetsu.SNDREQ
                '送信要求
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW : kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP : kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER : kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER : kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT : kengenFlg = False
                End Select

            Case LMI511C.EventShubetsu.REVISION
                '報告訂正
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW : kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP : kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER : kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER : kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT : kengenFlg = False
                End Select

            Case LMI511C.EventShubetsu.SNDCANCEL
                '送信取消
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW : kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP : kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER : kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER : kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT : kengenFlg = False
                End Select

            Case LMI511C.EventShubetsu.MTMSEARCH
                'まとめ候補検索
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW : kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP : kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER : kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER : kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT : kengenFlg = False
                End Select

            Case LMI511C.EventShubetsu.SEARCH
                '検索
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP : kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER : kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER : kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT : kengenFlg = False
                End Select

            Case LMI511C.EventShubetsu.MASTER
                'マスタ参照
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP : kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER : kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER : kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT : kengenFlg = False
                End Select

            Case LMI511C.EventShubetsu.SAVEEDT, LMI511C.EventShubetsu.SAVEREV
                '保存
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW : kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP : kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER : kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER : kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT : kengenFlg = False
                End Select

            Case LMI511C.EventShubetsu.CLOSE
                '閉じる
                kengenFlg = True

            Case LMI511C.EventShubetsu.PRINT1
                '印刷
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW : kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT : kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP : kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER : kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER : kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT : kengenFlg = False
                End Select

            Case LMI511C.EventShubetsu.DOUBLE_CLICK
                'ダブルクリック
                kengenFlg = True
        End Select

        If kengenFlg Then
            Return True
        Else
            MyBase.ShowMessage("E016")
        End If

    End Function

#End Region

#Region "共通処理"

    ''' <summary>
    ''' ヘッダーの値をTrim
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceHeaderTextValue()

        With Me._Frm
            .txtExclusionA.TextValue = Me._Frm.txtExclusionA.TextValue.Trim()
            .txtExclusionB.TextValue = Me._Frm.txtExclusionB.TextValue.Trim()
            .txtExclusionC.TextValue = Me._Frm.txtExclusionC.TextValue.Trim()
        End With

    End Sub

    ''' <summary>
    ''' チェックされた行番号のリストを取得
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        'チェックボックスセルの列番号取得
        Dim defNo As Integer = LMI511C.SprColumnIndex.DEF

        Return Me._Vcon.SprSelectList(defNo, Me._Frm.sprEdiList)

    End Function

    ''' <summary>
    ''' 未選択チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks>チェックリストの取得が前提</remarks>
    Friend Function IsSelectDataChk(ByVal chkList As ArrayList) As Boolean

        If Not Me._Vcon.IsSelectChk(chkList.Count()) Then
            Me.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 複数選択チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks>チェックリストの取得が前提</remarks>
    Friend Function IsSelectDataOneChk(ByVal chkList As ArrayList) As Boolean

        If Not Me._Vcon.IsSelectOneChk(chkList.Count()) Then
            Me.ShowMessage("E008")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 編集中に旧データになっていないかチェック
    ''' </summary>
    ''' <param name="ediCtlNo">EDI管理番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsOldDataChk(ByVal ediCtlNo As String) As Boolean

        'Dim rtDs As DataSet = DirectCast(Me.MyHandler, LMI511H).OldDataChk(Me._Frm, LM.Base.LMUserInfoManager.GetNrsBrCd().ToString(), ediCtlNo)
        Dim rtDs As DataSet = DirectCast(Me.MyHandler, LMI511H).OldDataChk(Me._Frm, Convert.ToString(Me._Frm.cmbEigyo.SelectedValue()), ediCtlNo)
        Dim dtOldDataChk As DataTable = rtDs.Tables(LMI511C.TABLE_NM.INOUT_OLD_DATA_CHK)
        Dim cnt As Integer = Convert.ToInt32(dtOldDataChk.Rows(0).Item("SELECT_CNT"))

        'データがない＝旧データになっている
        If cnt = 0 Then
            MyBase.ShowMessage("E00S")
            Return False
        End If

        Return True

    End Function

#End Region

#Region "出荷登録処理"

    ''' <summary>
    ''' 出荷登録処理：入力チェック（単項目チェック）
    ''' </summary>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsOutkaSaveSingleCheck(ByVal chkList As ArrayList) As Boolean

        '未選択チェック
        If Not IsSelectDataChk(chkList) Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 出荷登録処理：入力チェック（関連チェック）
    ''' </summary>
    ''' <param name="g"></param>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsOutkaSaveKanrenCheck(ByVal g As LMI511G, ByVal chkList As ArrayList) As Boolean

        Dim sprEdiListDef As LMI511G.sprEdiListDefault = DirectCast(g.objSprDef, Jp.Co.Nrs.LM.GUI.LMI511G.sprEdiListDefault)

        With Me._Frm.sprEdiList.ActiveSheet
            '出荷先単一チェック
            Dim saveoutkaPosiBuCd As String = String.Empty
            For liIdx As Integer = 0 To chkList.Count - 1
                Dim spIdx As Integer = Convert.ToInt32(chkList(liIdx))
                Dim outkaPosiBuCd As String = .Cells(spIdx, sprEdiListDef.OUTKA_POSI_BU_CD.ColNo).Value().ToString()

                If String.IsNullOrEmpty(saveoutkaPosiBuCd) Then
                    saveoutkaPosiBuCd = outkaPosiBuCd
                Else
                    If outkaPosiBuCd <> saveoutkaPosiBuCd Then
                        MyBase.ShowMessage("E00U")
                        Return False
                    End If
                End If
            Next
        End With

        Return True

    End Function

#End Region

#Region "編集処理"

    ''' <summary>
    ''' 編集処理：入力チェック（単項目チェック）
    ''' </summary>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsEditSingleCheck(ByVal chkList As ArrayList) As Boolean

        '未選択チェック
        If Not IsSelectDataChk(chkList) Then
            Return False
        End If

        '複数選択チェック
        If Not IsSelectDataOneChk(chkList) Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 編集処理：入力チェック（関連チェック）
    ''' </summary>
    ''' <param name="g"></param>
    ''' <param name="spIdx">該当行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsEditKanrenCheck(ByVal g As LMI511G, ByVal spIdx As Integer) As Boolean

        Dim sprEdiListDef As LMI511G.sprEdiListDefault = DirectCast(g.objSprDef, Jp.Co.Nrs.LM.GUI.LMI511G.sprEdiListDefault)

        With Me._Frm.sprEdiList.ActiveSheet
            'チェック用項目を取得
            Dim rtnFlg As String = .Cells(spIdx, sprEdiListDef.RTN_FLG.ColNo).Value().ToString()
            Dim sndCancelFlg As String = .Cells(spIdx, sprEdiListDef.SND_CANCEL_FLG.ColNo).Value().ToString()
#If True Then   'ADD 2020/08/28 013087   【LMS】JNC EDI　改修
            Dim unsoRtnFlg As String = .Cells(spIdx, sprEdiListDef.UNSO_RTN_FLG.ColNo).Value().ToString()
#End If
            '完了チェック①
            'UPD 2020/08/28 013087   【LMS】JNC EDI　改修
            'If rtnFlg = LMI511C.RTN_FLG.COMP And sndCancelFlg = LMI511C.SND_CANCEL_FLG.NORMAL Then
            If rtnFlg = LMI511C.RTN_FLG.COMP And sndCancelFlg = LMI511C.SND_CANCEL_FLG.NORMAL _
                And (unsoRtnFlg = LMI511C.RTN_FLG.COMP Or unsoRtnFlg = "") Then

                MyBase.ShowMessage("E00R")
                Return False
            End If

            '完了チェック②
            'UPD 2020/08/28 013087   【LMS】JNC EDI　改修
            'If rtnFlg = LMI511C.RTN_FLG.COMP And sndCancelFlg <> LMI511C.SND_CANCEL_FLG.NORMAL Then
            If rtnFlg = LMI511C.RTN_FLG.COMP And sndCancelFlg <> LMI511C.SND_CANCEL_FLG.NORMAL _
               And (unsoRtnFlg = LMI511C.RTN_FLG.COMP Or unsoRtnFlg = "") Then

                MyBase.ShowMessage("E00S")
                Return False
            End If
        End With

        Return True

    End Function

#End Region

#Region "まとめ解除処理"

    ''' <summary>
    ''' まとめ解除処理：入力チェック（単項目チェック）
    ''' </summary>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsMtmCancelSingleCheck(ByVal chkList As ArrayList) As Boolean

        '未選択チェック
        If Not IsSelectDataChk(chkList) Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "送信要求処理"

    ''' <summary>
    ''' 送信要求処理：入力チェック（単項目チェック）
    ''' </summary>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSndReqSingleCheck(ByVal chkList As ArrayList) As Boolean

        '未選択チェック
        If Not IsSelectDataChk(chkList) Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "報告訂正処理"

    ''' <summary>
    ''' 報告訂正処理：入力チェック（単項目チェック）
    ''' </summary>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRevisionSingleCheck(ByVal chkList As ArrayList) As Boolean

        '未選択チェック
        If Not IsSelectDataChk(chkList) Then
            Return False
        End If

        '複数選択チェック
        If Not IsSelectDataOneChk(chkList) Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 報告訂正処理：入力チェック（関連チェック）
    ''' </summary>
    ''' <param name="g"></param>
    ''' <param name="spIdx">該当行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRevisionKanrenCheck(ByVal g As LMI511G, ByVal spIdx As Integer) As Boolean

        Dim sprEdiListDef As LMI511G.sprEdiListDefault = DirectCast(g.objSprDef, Jp.Co.Nrs.LM.GUI.LMI511G.sprEdiListDefault)

        With Me._Frm.sprEdiList.ActiveSheet
            'チェック用項目を取得
            Dim rtnFlg As String = .Cells(spIdx, sprEdiListDef.RTN_FLG.ColNo).Value().ToString()
            Dim sndCancelFlg As String = .Cells(spIdx, sprEdiListDef.SND_CANCEL_FLG.ColNo).Value().ToString()

            '完了チェック③
            If rtnFlg <> LMI511C.RTN_FLG.COMP Or sndCancelFlg <> LMI511C.SND_CANCEL_FLG.NORMAL Then
                MyBase.ShowMessage("E00T")
                Return False
            End If
        End With

        Return True

    End Function

#End Region

#Region "送信取消処理"

    ''' <summary>
    ''' 送信取消処理：入力チェック（単項目チェック）
    ''' </summary>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSndCancelSingleCheck(ByVal chkList As ArrayList) As Boolean

        '未選択チェック
        If Not IsSelectDataChk(chkList) Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "まとめ候補検索処理"

    ''' <summary>
    ''' まとめ候補検索処理：入力チェック（単項目チェック）
    ''' </summary>
    ''' <param name="g"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsMtmSearchSingleCheck(ByVal g As LMI511G) As Boolean

        Dim sprEdiListDef As LMI511G.sprEdiListDefault = DirectCast(g.objSprDef, Jp.Co.Nrs.LM.GUI.LMI511G.sprEdiListDefault)

        With Me._Frm
            'ヘッダーの値をTrim
            Call Me.TrimSpaceHeaderTextValue()

            '出庫でのみ可能な処理なので強制的に設定(エラーとしない)
            .optSyuko.Checked = True
            .cmbSelectDate.SelectedValue = LMI511C.SELECT_DATE.OUTKA

            '営業所
            .cmbEigyo.ItemName() = "営業所"
            .cmbEigyo.IsHissuCheck() = True
            If Not MyBase.IsValidateCheck(.cmbEigyo) Then
                Return False
            End If

            ''荷主コード(大)
            '.txtCustCD_L.ItemName() = "荷主コード(大)"
            '.txtCustCD_L.IsHissuCheck() = True
            '.txtCustCD_L.IsForbiddenWordsCheck() = True
            '.txtCustCD_L.IsFullByteCheck = 5
            'If Not MyBase.IsValidateCheck(.txtCustCD_L) Then
            '    Return False
            'End If

            ''荷主コード(中)
            '.txtCustCD_M.ItemName() = "荷主コード(中)"
            '.txtCustCD_M.IsForbiddenWordsCheck() = True
            '.txtCustCD_M.IsFullByteCheck = 2
            'If Not MyBase.IsValidateCheck(.txtCustCD_M) Then
            '    Return False
            'End If

            '出荷先
            .cmbOutkaPosi.ItemName() = "出荷先"
            .cmbOutkaPosi.IsHissuCheck() = True
            If Not MyBase.IsValidateCheck(.cmbOutkaPosi) Then
                Return False
            End If

            'EDI取込日FROM
            If Not .imdEdiDateFrom.IsDateFullByteCheck(8) Then
                MyBase.ShowMessage("E038", New String() {"EDI取込日From", "8"})
                Return False
            End If

            'EDI取込日TO
            If Not .imdEdiDateTo.IsDateFullByteCheck(8) Then
                MyBase.ShowMessage("E038", New String() {"EDI取込日To", "8"})
                Return False
            End If

            '検索日区分(判定用)
            Dim dateMsg As String = String.Empty
            If .cmbSelectDate.SelectedValue.ToString = LMI511C.SELECT_DATE.OUTKA Then
                dateMsg = LMI511C.SELECT_DATE.OUTKA_NM
            ElseIf .cmbSelectDate.SelectedValue.ToString = LMI511C.SELECT_DATE.INKA Then
                dateMsg = LMI511C.SELECT_DATE.INKA_NM
            End If

            '検索日FROM
            .imdSearchDateFrom.ItemName() = String.Concat(dateMsg, "From")
            .imdSearchDateFrom.IsHissuCheck() = True
            If Not MyBase.IsValidateCheck(.imdSearchDateFrom) Then
                Return False
            End If
            If Not .imdSearchDateFrom.IsDateFullByteCheck(8) Then
                MyBase.ShowMessage("E038", New String() {String.Concat(dateMsg, "From"), "8"})
                Return False
            End If

            '検索日TO
            .imdSearchDateTo.ItemName() = String.Concat(dateMsg, "To")
            .imdSearchDateTo.IsHissuCheck() = True
            If Not MyBase.IsValidateCheck(.imdSearchDateTo) Then
                Return False
            End If
            If Not .imdSearchDateTo.IsDateFullByteCheck(8) Then
                MyBase.ShowMessage("E038", New String() {String.Concat(dateMsg, "To"), "8"})
                Return False
            End If

            'スプレッドの値をTrim
            Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprEdiList, 0)

            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprEdiList)

            '伝票番号
            vCell.SetValidateCell(0, sprEdiListDef.SR_DEN_NO.ColNo)
            vCell.ItemName() = "伝票番号"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            '届先名
            vCell.SetValidateCell(0, sprEdiListDef.DEST_NM.ColNo)
            vCell.ItemName() = "届先名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 100
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            '届先住所
            vCell.SetValidateCell(0, sprEdiListDef.DEST_AD_NM.ColNo)
            vCell.ItemName() = "届先住所"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 180
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            '商品名
            vCell.SetValidateCell(0, sprEdiListDef.GOODS_NM.ColNo)
            vCell.ItemName() = "商品名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 40
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            '受注番号
            vCell.SetValidateCell(0, sprEdiListDef.JYUCHU_NO.ColNo)
            vCell.ItemName() = "受注番号"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 23
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If
        End With

        Return True

    End Function

    ''' <summary>
    ''' まとめ候補検索処理：入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsMtmSearchKanrenCheck() As Boolean

        With Me._Frm
            '検索日FROM／検索日TO
            If Not String.IsNullOrEmpty(.cmbSelectDate.SelectedValue.ToString) Then
                '同一チェック
                If .imdSearchDateFrom.TextValue <> .imdSearchDateTo.TextValue Then
                    Me.ShowMessage("G014", New String() {"同一日付"})
                    .imdSearchDateFrom.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdSearchDateTo.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdSearchDateFrom.Focus()
                    Return False
                End If
            End If
        End With

        Return True

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 検索処理：入力チェック（単項目チェック）
    ''' </summary>
    ''' <param name="g"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSearchSingleCheck(ByVal g As LMI511G) As Boolean

        Dim sprEdiListDef As LMI511G.sprEdiListDefault = DirectCast(g.objSprDef, Jp.Co.Nrs.LM.GUI.LMI511G.sprEdiListDefault)

        With Me._Frm
            'ヘッダーの値をTrim
            Call Me.TrimSpaceHeaderTextValue()

            '営業所
            .cmbEigyo.ItemName() = "営業所"
            .cmbEigyo.IsHissuCheck() = True
            If Not MyBase.IsValidateCheck(.cmbEigyo) Then
                Return False
            End If

            ''荷主コード(大)
            '.txtCustCD_L.ItemName() = "荷主コード(大)"
            '.txtCustCD_L.IsHissuCheck() = True
            '.txtCustCD_L.IsForbiddenWordsCheck() = True
            '.txtCustCD_L.IsFullByteCheck = 5
            'If Not MyBase.IsValidateCheck(.txtCustCD_L) Then
            '    Return False
            'End If

            ''荷主コード(中)
            '.txtCustCD_M.ItemName() = "荷主コード(中)"
            '.txtCustCD_M.IsForbiddenWordsCheck() = True
            '.txtCustCD_M.IsFullByteCheck = 2
            'If Not MyBase.IsValidateCheck(.txtCustCD_M) Then
            '    Return False
            'End If

            'EDI取込日FROM
            If Not .imdEdiDateFrom.IsDateFullByteCheck(8) Then
                MyBase.ShowMessage("E038", New String() {"EDI取込日From", "8"})
                Return False
            End If

            'EDI取込日TO
            If Not .imdEdiDateTo.IsDateFullByteCheck(8) Then
                MyBase.ShowMessage("E038", New String() {"EDI取込日To", "8"})
                Return False
            End If

            '検索日区分／検索日FROM／検索日TO
            If Not String.IsNullOrEmpty(.cmbSelectDate.SelectedValue.ToString) Then
                Dim dateMsg As String = String.Empty
                If .cmbSelectDate.SelectedValue.ToString = LMI511C.SELECT_DATE.OUTKA Then
                    dateMsg = LMI511C.SELECT_DATE.OUTKA_NM
                ElseIf .cmbSelectDate.SelectedValue.ToString = LMI511C.SELECT_DATE.INKA Then
                    dateMsg = LMI511C.SELECT_DATE.INKA_NM
                End If
                If Not String.IsNullOrEmpty(dateMsg) Then
                    If Not .imdSearchDateFrom.IsDateFullByteCheck(8) Then
                        MyBase.ShowMessage("E038", New String() {String.Concat(dateMsg, "From"), "8"})
                        Return False
                    End If

                    If Not .imdSearchDateTo.IsDateFullByteCheck(8) Then
                        MyBase.ShowMessage("E038", New String() {String.Concat(dateMsg, "To"), "8"})
                        Return False
                    End If
                End If
            End If

            'スプレッドの値をTrim
            Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprEdiList, 0)

            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprEdiList)

            '伝票番号
            vCell.SetValidateCell(0, sprEdiListDef.SR_DEN_NO.ColNo)
            vCell.ItemName() = "伝票番号"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            '届先名
            vCell.SetValidateCell(0, sprEdiListDef.DEST_NM.ColNo)
            vCell.ItemName() = "届先名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 100
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            '届先住所
            vCell.SetValidateCell(0, sprEdiListDef.DEST_AD_NM.ColNo)
            vCell.ItemName() = "届先住所"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 180
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            '商品名
            vCell.SetValidateCell(0, sprEdiListDef.GOODS_NM.ColNo)
            vCell.ItemName() = "商品名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 40
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If

            '受注番号
            vCell.SetValidateCell(0, sprEdiListDef.JYUCHU_NO.ColNo)
            vCell.ItemName() = "受注番号"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 23
            If Not MyBase.IsValidateCheck(vCell) Then
                Return False
            End If
        End With

        Return True

    End Function

    ''' <summary>
    ''' 検索処理：入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSearchKanrenCheck() As Boolean

        With Me._Frm
            'EDI取込日FROM／EDI取込日TO
            If (Not String.IsNullOrEmpty(.imdEdiDateFrom.TextValue)) AndAlso (Not String.IsNullOrEmpty(.imdEdiDateTo.TextValue)) Then
                If Convert.ToInt32(.imdEdiDateTo.TextValue) < Convert.ToInt32(.imdEdiDateFrom.TextValue) Then
                    '大小チェック
                    Me.ShowMessage("E039", New String() {"EDI取込日To", "EDI取込日From"})
                    .imdEdiDateFrom.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdEdiDateTo.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdEdiDateFrom.Focus()
                    Return False
                End If
            End If

            '検索日FROM／検索日TO
            If Not String.IsNullOrEmpty(.cmbSelectDate.SelectedValue.ToString) Then
                Dim dateMsg As String = String.Empty
                If .cmbSelectDate.SelectedValue.ToString = "01" Then
                    dateMsg = "出荷予定日"
                Else
                    dateMsg = "納入予定日"
                End If

                If (Not String.IsNullOrEmpty(.imdSearchDateFrom.TextValue)) AndAlso (Not String.IsNullOrEmpty(.imdSearchDateTo.TextValue)) Then
                    If Convert.ToInt32(.imdSearchDateTo.TextValue) < Convert.ToInt32(.imdSearchDateFrom.TextValue) Then
                        '大小チェック
                        Me.ShowMessage("E039", New String() {String.Concat(dateMsg, "To"), String.Concat(dateMsg, "From")})
                        .imdSearchDateFrom.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                        .imdSearchDateTo.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                        .imdSearchDateFrom.Focus()
                        Return False
                    End If
                End If
            End If
        End With

        Return True

    End Function

#End Region

#Region "保存処理(編集)"

    ''' <summary>
    ''' 保存処理(編集)：入力チェック（単項目チェック）
    ''' </summary>
    ''' <param name="g"></param>
    ''' <param name="spIdx">該当行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSaveEditSingleCheck(ByVal g As LMI511G, ByVal spIdx As Integer) As Boolean

        Dim sprEdiListDef As LMI511G.sprEdiListDefault = DirectCast(g.objSprDef, Jp.Co.Nrs.LM.GUI.LMI511G.sprEdiListDefault)

        With Me._Frm.sprEdiList.ActiveSheet
            'チェック用項目を取得
            Dim outkaDate As String = .Cells(spIdx, sprEdiListDef.OUTKA_DATE.ColNo).Text().ToString()
            Dim suryReq As Double = Val(.Cells(spIdx, sprEdiListDef.SURY_REQ.ColNo).Value())
            Dim suryPrt As Double = Val(.Cells(spIdx, sprEdiListDef.SURY_RPT.ColNo).Value())
            Dim ediCtlNoUhd As String = .Cells(spIdx, sprEdiListDef.EDI_CTL_NO_UHD.ColNo).Value().ToString()
            Dim suryPrtUnso As Double = Val(.Cells(spIdx, sprEdiListDef.SURY_RPT_UNSO.ColNo).Value())
            Dim unsoRouteCd As String = .Cells(spIdx, sprEdiListDef.UNSO_ROUTE_NM.ColNo).Value().ToString()

            If Not String.IsNullOrEmpty(unsoRouteCd) Then
                unsoRouteCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'J015' AND KBN_CD = '", unsoRouteCd, "' "))(0).Item("KBN_NM2").ToString()
            End If

            '入庫時編集不可INKA_FLG
            Dim INKA_FLG As String = .Cells(spIdx, sprEdiListDef.INOUT_KB.ColNo).Value.ToString()

            '入庫時は編集不可
            If INKA_FLG <> LMI511C.INOUT_KB.INKA Then
                '必須項目チェック
                If String.IsNullOrEmpty(outkaDate) Then
                    Me.ShowMessage("E001", New String() {"出荷予定日"})
                    Return False
                End If
            End If

            '変更許容チェック(実出荷数量(積み))
            If (suryPrt > (suryReq * 1.05)) Or (suryPrt < (suryReq * 0.69)) Then
                Me.ShowMessage("E00Q", New String() {LMI511C.COMPANY_NAME})
                Return False
            End If

            '変更許容チェック(実出荷数量(卸し))
            If Me._Frm.optSyuko.Checked Then
                If Not String.IsNullOrEmpty(ediCtlNoUhd) Then
                    If (suryPrtUnso > (suryReq * 1.05)) Or (suryPrtUnso < (suryReq * 0.69)) Then
                        Me.ShowMessage("E00Q", New String() {LMI511C.COMPANY_NAME})
                        Return False
                    End If
                End If
            End If

            'トラックチェック(確認)
            If Me._Frm.optSyuko.Checked Then
                If (unsoRouteCd.Length > 0) AndAlso (Left(unsoRouteCd, 1) = "K") Then
                    If Me.ShowMessage("W140", New String() {"運送手段", "トラック"}) = MsgBoxResult.Cancel Then
                        Return False
                    End If
                End If
            End If

        End With

        Return True

    End Function

#End Region

#Region "マスタ参照"

    ''' <summary>
    ''' マスタ参照：入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRefMstInputCheck() As Boolean

        With Me._Frm
            'ヘッダーの値をTrim
            Call Me.TrimSpaceHeaderTextValue()

            '営業所
            .cmbEigyo.ItemName() = "営業所"
            .cmbEigyo.IsHissuCheck() = True
            If Not MyBase.IsValidateCheck(.cmbEigyo) Then
                Return False
            End If

            ''荷主コード(大)
            '.txtCustCD_L.ItemName() = "荷主コード(大)"
            '.txtCustCD_L.IsForbiddenWordsCheck() = True
            'If Not MyBase.IsValidateCheck(.txtCustCD_L) Then
            '    Return False
            'End If

            ''荷主コード(中)
            '.txtCustCD_M.ItemName() = "荷主コード(中)"
            '.txtCustCD_M.IsForbiddenWordsCheck() = True
            'If Not MyBase.IsValidateCheck(.txtCustCD_M) Then
            '    Return False
            'End If
        End With

        Return True

    End Function

#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理：入力チェック（単項目チェック）
    ''' </summary>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsPrintSingleCheck(ByVal chkList As ArrayList) As Boolean

        '未選択チェック
        If Not IsSelectDataChk(chkList) Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 印刷処理：入力チェック（複数選択チェック）
    ''' </summary>
    ''' <param name="chkList">チェックされた行番号のリスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsPrintNotSingleCheck(ByVal chkList As ArrayList) As Boolean

        '複数選択チェック
        If Not IsSelectDataOneChk(chkList) Then
            Return False
        End If

        Return True

    End Function
#End Region

#End Region 'Method

End Class
