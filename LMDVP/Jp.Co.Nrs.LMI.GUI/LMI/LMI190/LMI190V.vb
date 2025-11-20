' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI190  : ハネウェル管理
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports FarPoint.Win.Spread
Imports System.IO

''' <summary>
''' LMI190Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI190V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI190F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMIControlV

    Private _G As LMI190G

    ''' <summary>
    ''' チェックリストを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI190F, ByVal v As LMIControlV, ByVal g As LMI190G)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

        Me._G = g

    End Sub

#End Region 'Constructor

#Region "Method"


    Private Function GetModeFlg() As String

        Dim modeFlg As String = String.Empty '01:在庫、02:履歴、03:廃棄

        With Me._Frm

            If .optZaiko.Checked = True Then
                modeFlg = LMI190G.MODE_ZAI
            ElseIf .optRireki.Checked = True Then
                modeFlg = LMI190G.MODE_RIREKI
            ElseIf .optHaiki.Checked = True Then
                modeFlg = LMI190G.MODE_HAIKI
            End If

        End With

        Return modeFlg

    End Function

    ''' <summary>
    ''' 単項目入力チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMI190C.EventShubetsu) As Boolean

        '【単項目チェック】

        Select Case eventShubetsu

            Case LMI190C.EventShubetsu.GETDATA 'データ取得
                Return Me.IsGetDataSingleCheck()

            Case LMI190C.EventShubetsu.KENSAKU '検索
                Return Me.IsSearchSingleCheck()

            Case LMI190C.EventShubetsu.HOZON '保存
                Return Me.IsHozonSingleCheck()

        End Select

        Return True

    End Function

    ''' <summary>
    ''' データ取得処理時の単項目チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsGetDataSingleCheck() As Boolean

        Dim modeFlg As String = Me.GetModeFlg()

        With Me._Frm

            '2013.08.15 要望番号2095 START
            '冷媒商品
            .cmbCoolantGoodsKb.ItemName() = .lblTitleCoolantGoods.TextValue
            .cmbCoolantGoodsKb.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbCoolantGoodsKb) = False Then
                Return False
            End If
            '2013.08.15 要望番号2095 END

        End With

        Return True

    End Function


    ''' <summary>
    ''' 検索処理時の単項目チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSearchSingleCheck() As Boolean

        Dim modeFlg As String = Me.GetModeFlg()

        With Me._Frm

            'シリンダ番号
            .txtSerialNo.ItemName() = .lblTitleSerialNo.TextValue
            .txtSerialNo.IsForbiddenWordsCheck() = True
            .txtSerialNo.IsByteCheck() = 40
            If MyBase.IsValidateCheck(.txtSerialNo) = False Then
                Return False
            End If

            '2013.08.15 要望番号2095 START
            '冷媒商品
            .cmbCoolantGoodsKb.ItemName() = .lblTitleCoolantGoods.TextValue
            .cmbCoolantGoodsKb.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbCoolantGoodsKb) = False Then
                Return False
            End If
            '2013.08.15 要望番号2095 END

            Select Case modeFlg

                Case LMI190G.MODE_ZAI

                    '基準日
                    .imdKijunDate.ItemName() = .lblTitleKijunDate.TextValue
                    .imdKijunDate.IsHissuCheck() = True
                    If MyBase.IsValidateCheck(.imdKijunDate) = False Then
                        Return False
                    End If

                    '遅延金制度開始日
                    .imdChienDate.ItemName() = .lblTitleChienDate.TextValue
                    .imdChienDate.IsHissuCheck() = True
                    If MyBase.IsValidateCheck(.imdChienDate) = False Then
                        Return False
                    End If

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 保存処理時の単項目チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsHozonSingleCheck() As Boolean

        Dim preCd As String = String.Empty
        Dim editCd As String = String.Empty
        Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprDetails)

        'With Me._Frm.sprDetails.ActiveSheet

        '    For i As Integer = 0 To .Rows.Count - 1

        '        preCd = Me._Vcon.GetCellValue(.Cells(i, LMI190G.sprDetailsRireki.SHIPCDL.ColNo))
        '        editCd = Me._Vcon.GetCellValue(.Cells(i, LMI190G.sprDetailsRireki.SHIPCDL_EDIT.ColNo))

        '        If preCd.Equals(editCd) = True Then
        '            Continue For
        '        End If

        '        '荷送人コード
        '        vCell.SetValidateCell(i, LMI190G.sprDetailsRireki.SHIPCDL_EDIT.ColNo)
        '        vCell.ItemName = "荷送人コード"
        '        vCell.IsForbiddenWordsCheck() = True
        '        vCell.IsByteCheck = 15
        '        If MyBase.IsValidateCheck(vCell) = False Then
        '            Return False
        '        End If

        '    Next

        'End With

        With Me._Frm

            '【単項目チェック】

            'チェックリスト初期化
            Me._ChkList = New ArrayList()

            'チェック行リスト取得
            Me._ChkList = Me.GetCheckList()

            Dim max As Integer = Me._ChkList.Count - 1
            Dim chkVal As String = String.Empty
            Dim chkSagyoVal As Decimal = 0
            Dim sagyoGk As Decimal = 0

            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            '修正項目選択チェック
            If .cmbEditList.SelectedValue Is String.Empty Then
                MyBase.ShowMessage("E199", New String() {"修正項目"})
                Me._Vcon.SetErrorControl(.cmbEditList)
                Return False
            End If

            .txtShipCd.TextValue = Trim(.txtShipCd.TextValue)
            .txtShipCd.ItemName() = "荷送人コード"
            .txtShipCd.IsHissuCheck() = False
            .txtShipCd.IsForbiddenWordsCheck() = True
            .txtShipCd.IsByteCheck() = 15
            If MyBase.IsValidateCheck(.txtShipCd) = False Then
                Return False
            End If

            .txtShipNm.TextValue = Trim(.txtShipNm.TextValue)
            .txtShipNm.ItemName() = "荷送人名"
            .txtShipNm.IsHissuCheck() = False
            .txtShipNm.IsForbiddenWordsCheck() = True
            .txtShipNm.IsByteCheck() = 80
            If MyBase.IsValidateCheck(.txtShipNm) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連項目入力チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMI190C.EventShubetsu) As Boolean

        '【関連項目チェック】
        Select Case eventShubetsu

            Case LMI190C.EventShubetsu.KENSAKU '検索
                Return Me.IsSearchKanrenCheck()

            Case LMI190C.EventShubetsu.EXCEL   'EXCEL出力
                Return Me.IsExcelKanrenCheck()

        End Select

        Return True

    End Function

    ''' <summary>
    ''' 検索処理時の関連チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSearchKanrenCheck() As Boolean

        Dim modeFlg As String = Me.GetModeFlg()

        With Me._Frm

            Select Case modeFlg

                Case LMI190G.MODE_RIREKI, LMI190G.MODE_HAIKI

                    If String.IsNullOrEmpty(.imdIdoDateFrom.TextValue) = False AndAlso _
                       String.IsNullOrEmpty(.imdIdoDateTo.TextValue) = False Then

                        '大小チェック（移動日）
                        If .imdIdoDateFrom.TextValue > .imdIdoDateTo.TextValue Then
                            MyBase.ShowMessage("E039", New String() {"移動日To", "移動日From"})
                            .imdIdoDateFrom.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                            .imdIdoDateTo.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                            .imdIdoDateFrom.Focus()
                            .imdIdoDateFrom.Select()
                            Return False
                        End If

                    End If

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' EXCEL出力処理時の関連チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsExcelKanrenCheck() As Boolean

        If Me._Frm.sprDetails.ActiveSheet.Rows.Count = 0 Then
            MyBase.ShowMessage("E501", New String() {""})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI190C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu
            Case LMI190C.EventShubetsu.GETDATA,
            LMI190C.EventShubetsu.HENSHU,
            LMI190C.EventShubetsu.GETLOG,
            LMI190C.EventShubetsu.HAIKI,
            LMI190C.EventShubetsu.HAIKIKAIJO,
            LMI190C.EventShubetsu.KENSAKU,
            LMI190C.EventShubetsu.TEIKIKENSAKANRI,
            LMI190C.EventShubetsu.HOZON,
            LMI190C.EventShubetsu.EXCEL,
            LMI190C.EventShubetsu.HENKYAKUSHUKKA,
            LMI190C.EventShubetsu.MASTEROPEN,
            LMI190C.EventShubetsu.IMPORT_N40CD

                '10:閲覧者、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMI190C.EventShubetsu.CLOSE        '閉じる
                'すべての権限許可
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = True
                End Select

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        Return kengenFlg

    End Function

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetCheckList() As ArrayList

        Dim list As ArrayList = New ArrayList()

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMI190C.SprColumnIndexSPRALL.DEF

        With Me._Frm.sprDetails.ActiveSheet

            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If Me._Vcon.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    list.Add(i)
                End If
            Next

        End With

        Return list

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMI190C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            'ポップ対象外の場合
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMI190C.EventShubetsu.MASTEROPEN) = True Then
                Call Me._Vcon.SetFocusErrMessage(False)
            End If
            Return False
        End If

        '判定するコントロール設定先変数
        Dim txtCtl As Win.InputMan.LMImTextBox() = Nothing
        Dim lblCtl As Control() = Nothing
        Dim msg As String() = Nothing

        With Me._Frm

            Select Case objNm

                Case .sprDetails.Name

                    Return True

                Case .txtShipCd.Name

                    Dim shipNm As String = .lblTitleEdit.Text
                    lblCtl = New Control() {.txtShipNm}
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtShipCd}
                    msg = New String() {String.Concat(shipNm, LMFControlC.L_NM, LMFControlC.CD)}

            End Select

            'フォーカス位置チェック
            Return Me._Vcon.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)

        End With

    End Function

#End Region 'Method

#Region "選択チェック"
    ''' <summary>
    ''' 選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSelectDataChk() As Boolean

        'チェックリスト初期化
        Me._ChkList = New ArrayList()

        'チェック行リスト取得
        Me._ChkList = Me.getCheckList()

        '選択チェック
        If Me._Vcon.IsSelectChk(Me._ChkList.Count()) = False Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function
#End Region

End Class
