' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI330V : 納品データ選択&編集
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread

''' <summary>
''' LMI330Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMI330V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI330F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMIControlV

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMIControlG

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI330F, ByVal v As LMIControlV, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._ControlV = v

        'Gamen共通クラスの設定
        Me._ControlG = g

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI330C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI330C.EventShubetsu.SETHIN       'セット品編集
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

            Case LMI330C.EventShubetsu.HENSHU          '編集
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

            Case LMI330C.EventShubetsu.KENSAKU         '検索
                '50:外部の場合エラー
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
                        kengenFlg = False
                End Select


            Case LMI330C.EventShubetsu.MASTEROPEN      'マスタ参照
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMI330C.EventShubetsu.HOZON           '保存
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMI330C.EventShubetsu.TOJIRU          '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMI330C.EventShubetsu.DOUBLE_CLICK    'ダブルクリック
                '50:外部の場合エラー
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
                        kengenFlg = False
                End Select

            Case LMI330C.EventShubetsu.ENTER           'Enter
                '50:外部の場合エラー
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
                        kengenFlg = False
                End Select

            Case LMI330C.EventShubetsu.PRINT           '印刷
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

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        If kengenFlg = True Then
            Return True
        Else
            MyBase.ShowMessage("E016")
        End If
        Return False


    End Function

    ''' <summary>
    ''' 単項目/関連チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsInputChk(ByVal eventShubetsu As LMI330C.EventShubetsu, Optional ByVal arr As ArrayList = Nothing) As Boolean

        '単項目/関連チェック
        If Me.IsSingleChk(eventShubetsu, arr) = False OrElse Me.IsSaveCheck(eventShubetsu) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 基本メッセージ設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetBaseMsg()

        Select Case Me._Frm.lblSituation.DispMode
            Case DispMode.INIT

                MyBase.ShowMessage("G007")

            Case DispMode.VIEW

                MyBase.ShowMessage("G013")

            Case DispMode.EDIT

                MyBase.ShowMessage("G003")

        End Select

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetCheckList(ByVal eventShubetsu As LMI330C.EventShubetsu) As ArrayList

        'チェックボックスセルのカラム№取得
        Dim arr As Integer = Nothing
        Dim defNo As Integer = Nothing
        Dim spr As SheetView = Nothing

        Select Case eventShubetsu

            Case LMI330C.EventShubetsu.PRINT
                defNo = LMI330C.SprSearchColumnIndex.DEF
                spr = Me._Frm.sprSearch.ActiveSheet

            Case LMI330C.EventShubetsu.ROW_DEL
                defNo = LMI330C.SprDetailColumnIndex.DEF
                spr = Me._Frm.sprDetail.ActiveSheet

        End Select

        Return Me.GetCheckList(spr, defNo)

    End Function

    ''' <summary>
    ''' スプレッド明細行のチェックリスト(RowIndex)取得
    ''' </summary>
    ''' <param name="activeSheet">スプレッドシート</param>
    ''' <param name="defNo">レコードチェックボックスのカラム№</param>
    ''' <returns>チェックリスト</returns>
    ''' <remarks></remarks>
    Friend Function GetCheckList(ByVal activeSheet As SheetView, ByVal defNo As Integer) As ArrayList

        'チェック件数取得
        With activeSheet

            Dim arr As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If Me._ControlV.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    arr.Add(i)
                End If
            Next

            Return arr

        End With

    End Function

    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSingleChk(ByVal eventShubetsu As LMI330C.EventShubetsu, ByVal arr As ArrayList) As Boolean

        With Me._Frm

            Dim sCell As LMValidatableCells = New LMValidatableCells(.sprSearch)
            Dim dCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            Select Case eventShubetsu

                Case LMI330C.EventShubetsu.KENSAKU '検索

                    '【営業所】
                    .cmbNrsBr.ItemName = "営業所"
                    .cmbNrsBr.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbNrsBr) = False Then
                        Me._ControlV.SetErrorControl(.cmbNrsBr)
                        Return False
                    End If

                    '【倉庫】
                    .cmbSoko.ItemName = "倉庫"
                    .cmbSoko.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbSoko) = False Then
                        Me._ControlV.SetErrorControl(.cmbSoko)
                        Return False
                    End If

                    '【荷主コード(大)】
                    .txtCustCdL.ItemName = "荷主(大)"
                    .txtCustCdL.IsHissuCheck = True
                    .txtCustCdL.IsForbiddenWordsCheck = True
                    .txtCustCdL.IsFullByteCheck = 5
                    .txtCustCdL.IsMiddleSpace = True
                    If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                        Me._ControlV.SetErrorControl(.txtCustCdL)
                        Return False
                    End If

                    '【出荷予定日From】
                    .imdOutkaPlanDate_From.ItemName = "出荷予定日From"
                    If Me.IsInputDateFullByteChk(.imdOutkaPlanDate_From, .imdOutkaPlanDate_From.ItemName) = False Then
                        Return False
                    End If

                    '【出荷予定日To】
                    .imdOutkaPlanDate_To.ItemName = "出荷予定日To"
                    If Me.IsInputDateFullByteChk(.imdOutkaPlanDate_To, .imdOutkaPlanDate_To.ItemName) = False Then
                        Return False
                    End If

                    '【デリバリー№】
                    sCell.SetValidateCell(0, LMI330G.sprSearchDef.DELIVERY_NO.ColNo)
                    sCell.ItemName = "デリバリー№"
                    sCell.IsForbiddenWordsCheck = True
                    sCell.IsByteCheck = 30
                    If MyBase.IsValidateCheck(sCell) = False Then
                        Return False
                    End If

                    '【届先コード】
                    sCell.SetValidateCell(0, LMI330G.sprSearchDef.DEST_CD.ColNo)
                    sCell.ItemName = "届先コード"
                    sCell.IsForbiddenWordsCheck = True
                    sCell.IsByteCheck = 15
                    If MyBase.IsValidateCheck(sCell) = False Then
                        Return False
                    End If

                    '【届先名】
                    sCell.SetValidateCell(0, LMI330G.sprSearchDef.DEST_NM.ColNo)
                    sCell.ItemName = "届先名"
                    sCell.IsForbiddenWordsCheck = True
                    sCell.IsByteCheck = 60
                    If MyBase.IsValidateCheck(sCell) = False Then
                        Return False
                    End If

                Case LMI330C.EventShubetsu.HOZON '保存処理

                    '【デリバリー№】
                    .txtDeliveryNo.ItemName = "デリバリー№"
                    .txtDeliveryNo.IsForbiddenWordsCheck = True
                    .txtDeliveryNo.IsByteCheck = 15
                    If MyBase.IsValidateCheck(.txtDestCd) = False Then
                        Me._ControlV.SetErrorControl(.txtDestCd)
                        Return False
                    End If

                    '【届先コード】
                    .txtDestCd.ItemName = "届先コード"
                    .txtDestCd.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.txtDestCd) = False Then
                        Me._ControlV.SetErrorControl(.txtDestCd)
                        Return False
                    End If

                    '【納入日】
                    .imdArrPlanDate.ItemName = "納入日"
                    .imdArrPlanDate.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.imdArrPlanDate) = False Then
                        Me._ControlV.SetErrorControl(.imdArrPlanDate)
                        Return False
                    End If

                    'フルバイトチェック
                    If Me.IsInputDateFullByteChk(.imdArrPlanDate, .imdArrPlanDate.ItemName) = False Then
                        Return False
                    End If


                    For i As Integer = 0 To .sprDetail.ActiveSheet.Rows.Count - 1


                        '【品番】
                        dCell.SetValidateCell(i, LMI330G.sprDetailDef.GOODS_CD_CUST.ColNo)
                        dCell.ItemName = "品番"
                        dCell.IsHissuCheck = True
                        dCell.IsForbiddenWordsCheck = True
                        dCell.IsByteCheck = 20
                        If MyBase.IsValidateCheck(dCell) = False Then
                            Return False
                        End If

                        '【ロット番号】
                        dCell.SetValidateCell(i, LMI330G.sprDetailDef.LOT_NO.ColNo)
                        dCell.ItemName = "ロット番号"
                        dCell.IsForbiddenWordsCheck = True
                        dCell.IsByteCheck = 40
                        If MyBase.IsValidateCheck(dCell) = False Then
                            Return False
                        End If

                        '【品名】
                        dCell.SetValidateCell(i, LMI330G.sprDetailDef.GOODS_NM.ColNo)
                        dCell.ItemName = "品名"
                        dCell.IsHissuCheck = True
                        dCell.IsForbiddenWordsCheck = True
                        dCell.IsByteCheck = 60
                        If MyBase.IsValidateCheck(dCell) = False Then
                            Return False
                        End If

                        '【オーダー番号】
                        dCell.SetValidateCell(i, LMI330G.sprDetailDef.BUYER_ORD_NO.ColNo)
                        dCell.ItemName = "オーダー番号"
                        dCell.IsForbiddenWordsCheck = True
                        dCell.IsByteCheck = 60
                        If MyBase.IsValidateCheck(dCell) = False Then
                            Return False
                        End If

                        '【個数】
                        dCell.SetValidateCell(i, LMI330G.sprDetailDef.OUTKA_TTL_NB.ColNo)
                        dCell.ItemName = "個数"

                        Dim kosu As Integer = Convert.ToInt32(Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMI330G.sprDetailDef.OUTKA_TTL_NB.ColNo)))
                        If kosu < 1 Then
                            MyBase.ShowMessage("E182", New String() {"個数", "1"})
                            Return False
                        End If

                    Next

                Case LMI330C.EventShubetsu.PRINT '印刷処理

                    '【印刷種別】
                    .cmbPrintShubetu.ItemName = "印刷種別"
                    .cmbPrintShubetu.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbPrintShubetu) = False Then
                        Me._ControlV.SetErrorControl(.cmbPrintShubetu)
                        Return False
                    End If

                    '選択チェック
                    If Me._ControlV.IsSelectChk(arr.Count) = False Then
                        MyBase.ShowMessage("E009")
                        Return False
                    End If

                Case LMI330C.EventShubetsu.EXCEL

                    '【営業所】
                    .cmbNrsBr.ItemName = "営業所"
                    .cmbNrsBr.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbNrsBr) = False Then
                        Me._ControlV.SetErrorControl(.cmbNrsBr)
                        Return False
                    End If

                    '【荷主コード(大)】
                    .txtCustCdL.ItemName = "荷主(大)"
                    .txtCustCdL.IsHissuCheck = True
                    .txtCustCdL.IsForbiddenWordsCheck = True
                    .txtCustCdL.IsFullByteCheck = 5
                    .txtCustCdL.IsMiddleSpace = True
                    If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                        Me._ControlV.SetErrorControl(.txtCustCdL)
                        Return False
                    End If

                    '【商品コード位置】
                    .txtGoodsCdPosition.ItemName = "商品コード位置"
                    .txtGoodsCdPosition.IsHissuCheck = True
                    .txtGoodsCdPosition.IsByteCheck = 2
                    If MyBase.IsValidateCheck(.txtGoodsCdPosition) = False Then
                        Me._ControlV.SetErrorControl(.txtGoodsCdPosition)
                        Return False
                    End If

                    '【エクセル作成種別】
                    .cmbExcel.ItemName = "エクセル作成種別"
                    .cmbExcel.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbExcel) = False Then
                        Me._ControlV.SetErrorControl(.cmbExcel)
                        Return False
                    End If

            End Select

            Return True

        End With

    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveCheck(ByVal eventShubetsu As LMI330C.EventShubetsu) As Boolean

        With Me._Frm

            Dim dateFrom As String = .imdOutkaPlanDate_From.TextValue
            Dim dateTo As String = .imdOutkaPlanDate_To.TextValue

            Select Case eventShubetsu

                Case LMI330C.EventShubetsu.KENSAKU

                    If String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = False Then

                        '日付の大小チェック
                        If Me.IsLargeSmallChk(Convert.ToDecimal(dateTo), Convert.ToDecimal(dateFrom), False) = False Then
                            .imdOutkaPlanDate_From.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                            Me._ControlV.SetErrorControl(.imdOutkaPlanDate_To)
                            MyBase.ShowMessage("E039", New String() {"出荷予定日To", "出荷予定日From"})
                            Return False
                        End If

                    ElseIf String.IsNullOrEmpty(dateFrom) = True AndAlso String.IsNullOrEmpty(dateTo) = True Then

                        Me._ControlV.SetErrorControl(.imdOutkaPlanDate_From)
                        .imdOutkaPlanDate_To.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        MyBase.ShowMessage("E270", New String() {"出荷予定日From", "出荷予定日To"})
                        Return False

                    End If

                Case LMI330C.EventShubetsu.HOZON

                    '存在チェック（届先マスタ）
                    Dim destMstDs As MDestDS = New MDestDS
                    Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
                    destMstDr.Item("CUST_CD_L") = .txtCustCdL.TextValue
                    destMstDr.Item("DEST_CD") = .txtDestCd.TextValue
                    destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
                    Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
                    Dim destDr() As DataRow = rtnDs.Tables(LMConst.CacheTBL.DEST).Select

                    If destDr.Length = 0 Then
                        Me._ControlV.SetErrorControl(.txtDestCd)
                        MyBase.ShowMessage("E079", New String() {"届先マスタ", .txtDestCd.TextValue})
                        Return False
                    End If

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMI330C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If objNm Is Nothing = True Then
            Return False
        End If

        '判定するコントロール設定先変数
        Dim txtCtl As Win.InputMan.LMImTextBox() = Nothing
        Dim lblCtl As Control() = Nothing
        Dim msg As String() = Nothing

        With Me._Frm

            Select Case objNm

                Case .txtCustCdL.Name

                    Dim custNm As String = .lblTitleCust.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtCustCdL}
                    lblCtl = New Control() {.lblCustNmL}
                    msg = New String() {String.Concat(custNm, "コード(大)")}

                Case .txtDestCd.Name

                    Dim DestNm As String = .lblTitleDest.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtDestCd}
                    lblCtl = New Control() {.lblDestNm}
                    msg = New String() {DestNm}

            End Select

            Return Me._ControlV.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)

        End With

    End Function

    ''' <summary>
    '''クリア処理を行う
    ''' </summary>
    ''' <param name="ctl">クリア対象項目</param>
    ''' <remarks></remarks>
    Private Sub ClearControl(ByVal ctl As Win.InputMan.LMImTextBox(), _
                             ByVal clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl())

        'クリアコントロール未設定の場合、処理終了
        If clearCtl Is Nothing Then
            Exit Sub
        End If

        '対象コントロールに値が入っている場合、処理終了
        'If IsFoucsValueChk(ctl, ctl.Length - 1) = True Then
        '    Exit Sub
        'End If

        Dim clearMax As Integer = clearCtl.Length - 1

        'エディット系コントロールのクリア処理を行う
        For index As Integer = 0 To clearMax

            'コントロール別にクリア処理を行う
            If TypeOf clearCtl(index) Is Win.InputMan.LMImCombo = True Then

                DirectCast(clearCtl(index), Win.InputMan.LMImCombo).SelectedValue = String.Empty

            ElseIf TypeOf clearCtl(index) Is Win.InputMan.LMComboKubun = True Then

                DirectCast(clearCtl(index), Win.InputMan.LMComboKubun).SelectedValue = String.Empty

            ElseIf TypeOf clearCtl(index) Is Win.InputMan.LMImNumber = True Then

                DirectCast(clearCtl(index), Win.InputMan.LMImNumber).Value = 0

            Else

                clearCtl(index).TextValue = String.Empty

            End If

        Next

    End Sub

#End Region

#Region "日付関連"

    ''' <summary>
    ''' 日付のフルバイトチェック
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="str">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputDateFullByteChk(ByVal ctl As Win.InputMan.LMImDate, ByVal str As String) As Boolean

        If ctl.IsDateFullByteCheck = False Then
            MyBase.ShowMessage("E038", New String() {str, "8"})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 大小チェック
    ''' </summary>
    ''' <param name="large">大きい方の値</param>
    ''' <param name="small">小さい方の値</param>
    ''' <param name="equalFlg">イコールがエラーの場合：True　イコールがエラーではないの場合：False</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Overloads Function IsLargeSmallChk(ByVal large As Decimal, ByVal small As Decimal, ByVal equalFlg As Boolean) As Boolean

        '大小比較
        If equalFlg = True Then
            If large <= small Then
                Return False
            End If
        Else
            If large < small Then
                Return False
            End If
        End If

        Return True

    End Function

#End Region

#End Region

End Class
