' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI260V : 引取運賃明細入力
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMI260Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMI260V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI260F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMIControlV

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI260F, ByVal v As LMIControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._ControlV = v

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI260C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI260C.EventShubetsu.SHINKI           '新規
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

            Case LMI260C.EventShubetsu.HENSHU          '編集
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

            Case LMI260C.EventShubetsu.FUKUSHA          '複写
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

            Case LMI260C.EventShubetsu.SAKUJO_HUKKATU   '削除・復活
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

            Case LMI260C.EventShubetsu.KENSAKU         '検索
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


            Case LMI260C.EventShubetsu.MASTEROPEN      'マスタ参照
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

            Case LMI260C.EventShubetsu.HOZON           '保存
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

            Case LMI260C.EventShubetsu.TOJIRU          '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMI260C.EventShubetsu.DOUBLE_CLICK    'ダブルクリック
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

            Case LMI260C.EventShubetsu.ENTER           'Enter
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

            Case LMI260C.EventShubetsu.PRINT           '印刷
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
    Friend Function IsInputChk(ByVal eventShubetsu As LMI260C.EventShubetsu, Optional ByVal arr As ArrayList = Nothing) As Boolean

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
    Friend Function GetCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMI260C.SprDetailColumnIndex.DEF

        Return Me._ControlV.SprSelectList(defNo, Me._Frm.sprDetail)

    End Function

    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSingleChk(ByVal eventShubetsu As LMI260C.EventShubetsu, ByVal arr As ArrayList) As Boolean

        With Me._Frm

            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            Select Case eventShubetsu

                Case LMI260C.EventShubetsu.SAKUJO_HUKKATU '削除・復活処理

                    '選択チェック
                    If Me._ControlV.IsSelectChk(arr.Count) = False Then
                        MyBase.ShowMessage("E009")
                        Return False
                    End If

                Case LMI260C.EventShubetsu.KENSAKU '検索

                    '【引取日From】
                    .imdHikiDate_From.ItemName = "引取日From"
                    If Me.IsInputDateFullByteChk(.imdHikiDate_From, .imdHikiDate_From.ItemName) = False Then
                        Return False
                    End If

                    '【引取日To】
                    .imdHikiDate_To.ItemName = "引取日To"
                    If Me.IsInputDateFullByteChk(.imdHikiDate_To, .imdHikiDate_To.ItemName) = False Then
                        Return False
                    End If

                    '【引取先コード】
                    vCell.SetValidateCell(0, LMI260G.sprDetailDef.HIKITORI_CD.ColNo)
                    vCell.ItemName = LMI260G.sprDetailDef.HIKITORI_CD.ColName
                    vCell.IsByteCheck = 15
                    If MyBase.IsValidateCheck(vCell) = False Then
                        Return False
                    End If

                    '【引取先名称】
                    vCell.SetValidateCell(0, LMI260G.sprDetailDef.HIKITORI_NM.ColNo)
                    vCell.ItemName = LMI260G.sprDetailDef.HIKITORI_NM.ColName
                    vCell.IsForbiddenWordsCheck = True
                    vCell.IsByteCheck = 60
                    If MyBase.IsValidateCheck(vCell) = False Then
                        Return False
                    End If

                Case LMI260C.EventShubetsu.HOZON '保存処理

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

                    '【荷主コード(中)】
                    .txtCustCdM.ItemName = "荷主(中)"
                    .txtCustCdM.IsHissuCheck = True
                    .txtCustCdM.IsForbiddenWordsCheck = True
                    .txtCustCdM.IsFullByteCheck = 2
                    .txtCustCdM.IsMiddleSpace = True
                    If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                        Me._ControlV.SetErrorControl(.txtCustCdM)
                        Return False
                    End If

                    '【引取日】
                    .imdHikiDate.ItemName = "引取日"
                    .imdHikiDate.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.imdHikiDate) = False Then
                        Me._ControlV.SetErrorControl(.imdHikiDate)
                        Return False
                    End If

                    'フルバイトチェック
                    If Me.IsInputDateFullByteChk(.imdHikiDate, .imdHikiDate.ItemName) = False Then
                        Return False
                    End If

                    '【明細NO】
                    .numMeisaiNo.ItemName = "明細NO"
                    .numMeisaiNo.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.numMeisaiNo) = False Then
                        Me._ControlV.SetErrorControl(.numMeisaiNo)
                        Return False
                    End If

                    If Convert.ToInt32(.numMeisaiNo.Value) = 0 Then
                        Me._ControlV.SetErrorControl(.numMeisaiNo)
                        MyBase.ShowMessage("E233", New String() {.numMeisaiNo.ItemName})
                        Return False
                    End If

                    '【品名】
                    .cmbHinNm.ItemName = "品名"
                    .cmbHinNm.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbHinNm) = False Then
                        Me._ControlV.SetErrorControl(.cmbHinNm)
                        Return False
                    End If

                    '【引取先コード】
                    .txtHikitoriCd.ItemName = "引取先コード"
                    .txtHikitoriCd.IsHissuCheck = True
                    .txtHikitoriCd.IsForbiddenWordsCheck = True
                    .txtHikitoriCd.IsByteCheck = 15
                    If MyBase.IsValidateCheck(.txtHikitoriCd) = False Then
                        Me._ControlV.SetErrorControl(.txtHikitoriCd)
                        Return False
                    End If

                Case LMI260C.EventShubetsu.PRINT '印刷処理

                    '【引取日From】
                    .imdHikiDate_From.ItemName = "引取日From"
                    .imdHikiDate_From.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.imdHikiDate_From) = False Then
                        Me._ControlV.SetErrorControl(.imdHikiDate_From)
                        Return False
                    End If

                    If Me.IsInputDateFullByteChk(.imdHikiDate_From, .imdHikiDate_From.ItemName) = False Then
                        Return False
                    End If

                    '【引取日To】
                    .imdHikiDate_To.ItemName = "引取日To"
                    .imdHikiDate_To.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.imdHikiDate_To) = False Then
                        Me._ControlV.SetErrorControl(.imdHikiDate_To)
                        Return False
                    End If

                    If Me.IsInputDateFullByteChk(.imdHikiDate_To, .imdHikiDate_To.ItemName) = False Then
                        Return False
                    End If

                    '【印刷種別】
                    .cmbPrintShubetu.ItemName = "印刷種別"
                    .cmbPrintShubetu.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbPrintShubetu) = False Then
                        Me._ControlV.SetErrorControl(.cmbPrintShubetu)
                        Return False
                    End If

                    '【印刷用商品種別】
                    .cmbPrintHinNm.ItemName = "印刷用商品種別"
                    .cmbPrintHinNm.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbPrintHinNm) = False Then
                        Me._ControlV.SetErrorControl(.cmbPrintHinNm)
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
    Private Function IsSaveCheck(ByVal eventShubetsu As LMI260C.EventShubetsu) As Boolean

        With Me._Frm

            Dim dateFrom As String = .imdHikiDate_From.TextValue
            Dim dateTo As String = .imdHikiDate_To.TextValue

            Select Case eventShubetsu

                Case LMI260C.EventShubetsu.KENSAKU, LMI260C.EventShubetsu.PRINT

                    If String.IsNullOrEmpty(dateFrom) = False AndAlso String.IsNullOrEmpty(dateTo) = False Then

                        '日付の大小チェック
                        If Me.IsLargeSmallChk(Convert.ToDecimal(dateTo), Convert.ToDecimal(dateFrom), False) = False Then
                            .imdHikiDate_From.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                            Me._ControlV.SetErrorControl(.imdHikiDate_To)
                            MyBase.ShowMessage("E039", New String() {"引取日To", "引取日From"})
                            Return False
                        End If

                    ElseIf String.IsNullOrEmpty(dateFrom) = True AndAlso String.IsNullOrEmpty(dateTo) = True Then

                        Me._ControlV.SetErrorControl(.imdHikiDate_From)
                        .imdHikiDate_To.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        MyBase.ShowMessage("E270", New String() {"引取日From", "引取日To"})
                        Return False

                    End If

                Case LMI260C.EventShubetsu.HOZON

                    '存在チェック（荷主マスタ）
                    Dim custDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("NRS_BR_CD = '", .cmbNrsBr.SelectedValue _
                                                                                                                    , "' AND CUST_CD_L = '", .txtCustCdL.TextValue _
                                                                                                                    , "' AND CUST_CD_M = '", .txtCustCdM.TextValue _
                                                                                                                    , "' AND CUST_CD_S = '", "00" _
                                                                                                                    , "' AND CUST_CD_SS = '", "00", "'"))

                    If custDr.Length = 0 Then
                        Me._ControlV.SetErrorControl(.txtCustCdL)
                        .txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        MyBase.ShowMessage("E079", New String() {"荷主マスタ", String.Concat(.txtCustCdL.TextValue, "-", .txtCustCdM.TextValue)})
                        Return False
                    End If

                    '存在チェック（届先マスタ）
                    '---↓
                    'Dim destDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(String.Concat("NRS_BR_CD = '", .cmbNrsBr.SelectedValue _
                    '                                                                                                , "' AND DEST_CD = '", .txtHikitoriCd.TextValue, "'"))

                    Dim destMstDs As MDestDS = New MDestDS
                    Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
                    destMstDr.Item("CUST_CD_L") = .txtCustCdL.TextValue
                    destMstDr.Item("DEST_CD") = .txtHikitoriCd.TextValue
                    destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
                    Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
                    Dim destDr() As DataRow = rtnDs.Tables(LMConst.CacheTBL.DEST).Select
                    '---↑

                    If destDr.Length = 0 Then
                        Me._ControlV.SetErrorControl(.txtHikitoriCd)
                        MyBase.ShowMessage("E079", New String() {"届先マスタ", .txtHikitoriCd.TextValue})
                        Return False
                    End If

                    '荷主明細チェック
                    Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbNrsBr.SelectedValue _
                                                                                                                                   , "' AND CUST_CD = '", String.Concat(.txtCustCdL.TextValue, .txtCustCdM.TextValue) _
                                                                                                                                   , "' AND SUB_KB = '49'"))
                    If custDetailsDr.Length = 0 Then
                        MyBase.ShowMessage("E336", New String() {"対象荷主", "印刷"})
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
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMI260C.EventShubetsu) As Boolean

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

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    Dim custNm As String = .lblTitleCust.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtCustCdL, .txtCustCdM}
                    lblCtl = New Control() {.lblCustNmL, .lblCustNmM}
                    msg = New String() {String.Concat(custNm, "コード(大)"), String.Concat(custNm, "コード(中)")}

                Case .txtHikitoriCd.Name

                    Dim HikitoriNm As String = .lblTitleHikitori.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtHikitoriCd}
                    lblCtl = New Control() {.lblHikitoriNm}
                    msg = New String() {HikitoriNm}

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

    ''' <summary>
    ''' 合計の範囲チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function TotalSumCheck(ByVal fcTotal As Decimal, ByVal dmTotal As Decimal, ByVal allTotal As Decimal) As Boolean

        'フレコン合計
        If fcTotal < 0 OrElse 999999999 < fcTotal Then
            MyBase.ShowMessage("E222", New String() {"フレコン合計", "0", "999999999"})
            Me._Frm.numFcNb.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Me._Frm.numFcTanka.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Return False
        End If

        'ドラム合計
        If dmTotal < 0 OrElse 999999999 < dmTotal Then
            MyBase.ShowMessage("E222", New String() {"ドラム合計", "0", "999999999"})
            Me._Frm.numDmNb.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Me._Frm.numDmTanka.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Return False
        End If

        '総合計
        If allTotal < 0 OrElse 999999999 < allTotal Then
            MyBase.ShowMessage("E222", New String() {"総合計", "0", "999999999"})
            Me._Frm.numFcNb.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Me._Frm.numFcTanka.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Me._Frm.numDmNb.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Me._Frm.numDmTanka.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Me._Frm.numSeihin.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Me._Frm.numSukurap.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Me._Frm.numWarimasi.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Me._Frm.numSeikei.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Me._Frm.numRosen.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Me._Frm.numKousoku.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Me._Frm.numSonota.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Return False
        End If

        Return True

    End Function

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
