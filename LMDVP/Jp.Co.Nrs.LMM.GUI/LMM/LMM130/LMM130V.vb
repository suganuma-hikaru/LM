' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM130V : 棟室マスタメンテナンス
'  作  成  者       :  [kishi]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMM130Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM130V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM130F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMMControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMMControlG

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM130G

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM130F, ByVal v As LMMControlV)

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
        Me._ControlG = New LMMControlG(handlerClass, DirectCast(frm, Form))

        'Gamenクラスの設定
        Me._G = New LMM130G(handlerClass, Me._Frm, Me._ControlG)

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM130C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM130C.EventShubetsu.SHINKI           '新規
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM130C.EventShubetsu.HENSHU          '編集
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM130C.EventShubetsu.HUKUSHA          '複写
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select


            Case LMM130C.EventShubetsu.SAKUJO          '削除・復活
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM130C.EventShubetsu.KENSAKU         '検索
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

            Case LMM130C.EventShubetsu.MASTEROPEN          'マスタ参照
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM130C.EventShubetsu.HOZON           '保存
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM130C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM130C.EventShubetsu.DCLICK         'ダブルクリック
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

            Case LMM130C.EventShubetsu.ENTER          'Enter
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

                '2017/10/27 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
            Case LMM130C.EventShubetsu.INS_EXP_T, LMM130C.EventShubetsu.DEL_EXP_T, LMM130C.EventShubetsu.IKKATU_TOUROKU
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select
                '2017/10/27 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        If kengenFlg = True Then
            Return True
        Else
            MyBase.ShowMessage("E016")
        End If

    End Function

    ''' <summary>
    ''' 編集押下時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsHenshuChk() As Boolean

        '2016.01.06 UMANO 英語化対応START
        'Dim msg As String = "編集"
        Dim msg As String = Me._Frm.FunctionKey.F2ButtonName
        '2016.01.06 UMANO 英語化対応END

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.txtTouNo.TextValue) = False Then
            Return False
        End If

        '他営業所チェック
        If Me._ControlV.IsUserNrsBrCdChk(Me._Frm.cmbNrsBrCd.SelectedValue.ToString(), msg) = False Then
            Return False
        End If

        'データ存在チェックを行う
        If Me._ControlV.IsRecordStatusChk(Me._Frm.lblSituation.RecordStatus) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 複写押下時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsFukushaChk() As Boolean

        '2016.01.06 UMANO 英語化対応START
        'Dim msg As String = "複写"
        Dim msg As String = Me._Frm.FunctionKey.F3ButtonName
        '2016.01.06 UMANO 英語化対応END

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.txtTouNo.TextValue) = False Then
            Return False
        End If

        '他営業所チェック
        If Me._ControlV.IsUserNrsBrCdChk(Me._Frm.cmbNrsBrCd.SelectedValue.ToString(), msg) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 削除/復活押下時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsSakujoHukkatuChk() As Boolean

        '2016.01.06 UMANO 英語化対応START
        'Dim msg As String = "削除・復活"
        Dim msg As String = Me._Frm.FunctionKey.F4ButtonName
        '2016.01.06 UMANO 英語化対応END

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.txtTouNo.TextValue) = False Then
            Return False
        End If

        '他営業所チェック
        If Me._ControlV.IsUserNrsBrCdChk(Me._Frm.cmbNrsBrCd.SelectedValue.ToString(), msg) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSaveInputChk() As Boolean

        With Me._Frm

            'スペース除去
            Call Me._ControlV.TrimSpaceTextvalue(Me._Frm)
            Call Me._ControlV.TrimSpaceSprTextvalue(.sprDetail2 _
                                                    , .sprDetail2.ActiveSheet.Rows.Count - 1 _
                                                    , LMM130G.sprDetailDef2.WH_KYOKA_DATE.ColNo)

            '単項目チェック(編集部)
            Dim rtnResult As Boolean = Me.IsSaveSingleCheck()

            '単項目チェック(棟室消防Spread)
            rtnResult = rtnResult AndAlso Me.IsTouSituChk()

            '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
            '単項目チェック(申請外の商品保管許可ルールSpread)
            rtnResult = rtnResult AndAlso Me.IsTouSituExpChk()
            '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

            '関連チェック
            rtnResult = rtnResult AndAlso Me.IsSaveRelationChk()

            '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
            '関連チェック(申請外の商品保管許可ルールSpread)
            rtnResult = rtnResult AndAlso Me.IsSaveRelationExpChk()
            '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

            '単項目チェック(棟室ゾーンチェックマスタSpread)
            rtnResult = rtnResult AndAlso Me.IsExistTouSituZonChkSpr(.sprDetail4, LMM130G.sprDetailDef4.DOKU_KB.ColNo)
            rtnResult = rtnResult AndAlso Me.IsExistTouSituZonChkSpr(.sprDetail5, LMM130G.sprDetailDef5.KOUATHUGAS_KB.ColNo)
            rtnResult = rtnResult AndAlso Me.IsExistTouSituZonChkSpr(.sprDetail6, LMM130G.sprDetailDef6.YAKUZIHO_KB.ColNo)

            '非表示項目（毒劇区分、ガス管理区分）設定
            Call Me.SetCmbValue()

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSingleCheck() As Boolean

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '**********編集部のチェック
            '営業所
            .cmbNrsBrCd.ItemName = .lblEigyosyo.Text
            .cmbNrsBrCd.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = errorFlg Then
                Call Me._ControlV.SetErrorControl(.cmbNrsBrCd, .tabTouSitu, .tpgTouSitu)
                Return errorFlg
            End If

            '倉庫
            .cmbWare.ItemName = .lblWare.Text
            .cmbWare.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbWare) = errorFlg Then
                Call Me._ControlV.SetErrorControl(.cmbWare, .tabTouSitu, .tpgTouSitu)
                Return errorFlg
            End If

            '棟番号
            '2016.01.06 UMANO 英語化対応START
            '.txtTouNo.ItemName = LMM130C.TOU
            .txtTouNo.ItemName = .sprDetail.ActiveSheet.GetColumnLabel(0, LMM130C.SprColumnIndex.TOU_NO)
            '2016.01.06 UMANO 英語化対応END
            .txtTouNo.IsHissuCheck = chkFlg
            .txtTouNo.IsForbiddenWordsCheck = chkFlg
            .txtTouNo.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtTouNo) = errorFlg Then
                Call Me._ControlV.SetErrorControl(.txtTouNo, .tabTouSitu, .tpgTouSitu)
                Return errorFlg
            End If

            '室番号
            '2016.01.06 UMANO 英語化対応START
            '.txtSituNo.ItemName = LMM130C.SHITU
            .txtSituNo.ItemName = .sprDetail.ActiveSheet.GetColumnLabel(0, LMM130C.SprColumnIndex.SITU_NO)
            '2016.01.06 UMANO 英語化対応END
            .txtSituNo.IsHissuCheck = chkFlg
            .txtSituNo.IsForbiddenWordsCheck = chkFlg
            'START YANAI 要望番号705
            '.txtSituNo.IsByteCheck = 1
            'START S_KOBA 要望番号705
            '.txtSituNo.IsFullByteCheck = 2
            .txtSituNo.IsByteCheck = 2
            'END S_KOBA 要望番号705
            'END YANAI 要望番号705
            If MyBase.IsValidateCheck(.txtSituNo) = errorFlg Then
                Call Me._ControlV.SetErrorControl(.txtSituNo, .tabTouSitu, .tpgTouSitu)
                Return errorFlg
            End If

            '棟室名
            '2016.01.06 UMANO 英語化対応START
            '.txtTouSituNm.ItemName = .lblTouSitu.Text & "名"
            .txtTouSituNm.ItemName = .sprDetail.ActiveSheet.GetColumnLabel(0, LMM130C.SprColumnIndex.TOU_SITU_NM)
            '2016.01.06 UMANO 英語化対応END
            .txtTouSituNm.IsHissuCheck = chkFlg
            .txtTouSituNm.IsForbiddenWordsCheck = chkFlg
            .txtTouSituNm.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtTouSituNm) = errorFlg Then
                Call Me._ControlV.SetErrorControl(.txtTouSituNm, .tabTouSitu, .tpgTouSitu)
                Return errorFlg
            End If

            '倉庫区分
            .cmbSokoKbn.ItemName = .lblSokoKbn.Text
            .cmbSokoKbn.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbSokoKbn) = errorFlg Then
                Call Me._ControlV.SetErrorControl(.cmbSokoKbn, .tabTouSitu, .tpgTouSitu)
                Return errorFlg
            End If

            '保税区分
            .cmbHozeiKbn.ItemName = .lblHozeiKbn.Text
            .cmbHozeiKbn.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbHozeiKbn) = errorFlg Then
                Call Me._ControlV.SetErrorControl(.cmbHozeiKbn, .tabTouSitu, .tpgTouSitu)
                Return errorFlg
            End If

            '温度管理区分
            .cmbOndoCtlKbn.ItemName = .lblOndoCtlKbn.Text
            .cmbOndoCtlKbn.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbOndoCtlKbn) = errorFlg Then
                Call Me._ControlV.SetErrorControl(.cmbOndoCtlKbn, .tabTouSitu, .tpgTouSitu)
                Return errorFlg
            End If

            '温度管理中フラグ
            .cmbOndoCtlFlg.ItemName = .lblOndoCtlFlg.Text
            .cmbOndoCtlFlg.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbOndoCtlFlg) = errorFlg Then
                Call Me._ControlV.SetErrorControl(.cmbOndoCtlFlg, .tabTouSitu, .tpgTouSitu)
                Return errorFlg
            End If

            '担当作業班
            .txtHan.ItemName = .lblHan.Text
            .txtHan.IsForbiddenWordsCheck = chkFlg
            .txtHan.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtHan) = errorFlg Then
                Call Me._ControlV.SetErrorControl(.txtHan, .tabTouSitu, .tpgTouSitu)
                Return errorFlg
            End If

            'ラック設備
            .cmbRackYn.ItemName = .lblRackYn.Text
            .cmbRackYn.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbRackYn) = errorFlg Then
                Call Me._ControlV.SetErrorControl(.cmbRackYn, .tabTouSitu, .tpgTouSitu)
                Return errorFlg
            End If

            '保安監督者名
            .txtFctMgr.ItemName = .lblFctMgr.Text
            .txtFctMgr.IsHissuCheck = True
            .txtFctMgr.IsForbiddenWordsCheck = True
            .txtFctMgr.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtFctMgr) = errorFlg Then
                Call Me._ControlV.SetErrorControl(.txtFctMgr, .tabTouSitu, .tpgTouSitu)
                Return errorFlg
            End If
            Dim userDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", .txtFctMgr.TextValue, "'"))
            If 0 = userDr.Length Then
                MyBase.ShowMessage("E871")
                Call Me._ControlV.SetErrorControl(.txtFctMgr, .tabTouSitu, .tpgTouSitu)
                Return errorFlg
            End If

            '主担当作業者
            .txtUserCd.ItemName = .lblWHSagyoTanto.Text
            .txtUserCd.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(.txtUserCd) = errorFlg Then
                Call Me._ControlV.SetErrorControl(.txtUserCd, .tabTouSitu, .tpgTouSitu)
                Return errorFlg
            End If
            If Not String.IsNullOrEmpty(.txtUserCd.TextValue) Then
                userDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", .txtUserCd.TextValue, "'"))
                If 0 = userDr.Length Then
                    MyBase.ShowMessage("E871")
                    Call Me._ControlV.SetErrorControl(.txtUserCd, .tabTouSitu, .tpgTouSitu)
                    Return errorFlg
                End If
            End If

            '副担当作業者
            .txtUserCdSub.ItemName = .lblWHSagyoTantoSub.Text
            .txtUserCdSub.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(.txtUserCdSub) = errorFlg Then
                Call Me._ControlV.SetErrorControl(.txtUserCdSub, .tabTouSitu, .tpgTouSitu)
                Return errorFlg
            End If
            If Not String.IsNullOrEmpty(.txtUserCdSub.TextValue) Then
                userDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", .txtUserCdSub.TextValue, "'"))
                If 0 = userDr.Length Then
                    MyBase.ShowMessage("E871")
                    Call Me._ControlV.SetErrorControl(.txtUserCdSub, .tabTouSitu, .tpgTouSitu)
                    Return errorFlg
                End If
            End If

            '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
            '自社他社区分
            .cmbJisyatasyaKbn.ItemName = .lblJisyatasyaKbn.Text
            .cmbJisyatasyaKbn.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbJisyatasyaKbn) = errorFlg Then
                Call Me._ControlV.SetErrorControl(.cmbJisyatasyaKbn, .tabTouSitu, .tpgTouSitu)
                Return errorFlg
            End If
            '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

            '他社倉庫情報（入力可能状態ならばチェック）
            If Me._G.IsTasyaInfo() Then
                '他社倉庫名称
                .txtTasyaWhNm.ItemName = .lblTasyaWhNm.Text
                .txtTasyaWhNm.IsHissuCheck = chkFlg
                .txtTasyaWhNm.IsForbiddenWordsCheck = chkFlg
                .txtTasyaWhNm.IsByteCheck = 60
                If MyBase.IsValidateCheck(.txtTasyaWhNm) = errorFlg Then
                    Call Me._ControlV.SetErrorControl(.txtTasyaWhNm, .tabTouSitu, .tpgTouSitu)
                    Return errorFlg
                End If

                '他社倉庫郵便番号
                .txtTasyaZip.ItemName = .lblTasyaZip.Text
                .txtTasyaZip.IsHissuCheck = chkFlg
                .txtTasyaZip.IsForbiddenWordsCheck = chkFlg
                .txtTasyaZip.IsByteCheck = 10
                If MyBase.IsValidateCheck(.txtTasyaZip) = errorFlg Then
                    Call Me._ControlV.SetErrorControl(.txtTasyaZip, .tabTouSitu, .tpgTouSitu)
                    Return errorFlg
                End If

                '他社倉庫住所1
                .txtTasyaAd1.ItemName = .lblTasyaAd1.Text
                .txtTasyaAd1.IsHissuCheck = chkFlg
                .txtTasyaAd1.IsForbiddenWordsCheck = chkFlg
                .txtTasyaAd1.IsByteCheck = 40
                If MyBase.IsValidateCheck(.txtTasyaAd1) = errorFlg Then
                    Call Me._ControlV.SetErrorControl(.txtTasyaAd1, .tabTouSitu, .tpgTouSitu)
                    Return errorFlg
                End If

                '他社倉庫住所2
                .txtTasyaAd2.ItemName = .lblTasyaAd2.Text
                .txtTasyaAd2.IsForbiddenWordsCheck = chkFlg
                .txtTasyaAd2.IsByteCheck = 40
                If MyBase.IsValidateCheck(.txtTasyaAd2) = errorFlg Then
                    Call Me._ControlV.SetErrorControl(.txtTasyaAd2, .tabTouSitu, .tpgTouSitu)
                    Return errorFlg
                End If

                '他社倉庫住所3
                .txtTasyaAd3.ItemName = .lblTasyaAd3.Text
                .txtTasyaAd3.IsForbiddenWordsCheck = chkFlg
                .txtTasyaAd3.IsByteCheck = 40
                If MyBase.IsValidateCheck(.txtTasyaAd3) = errorFlg Then
                    Call Me._ControlV.SetErrorControl(.txtTasyaAd3, .tabTouSitu, .tpgTouSitu)
                    Return errorFlg
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 棟室消防Spreadの単項目チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsTouSituChk() As Boolean

        '**********棟室消防スプレッドのチェック
        Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprDetail2)

        With vCell

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False
            Dim spr As LMSpread = Me._Frm.sprDetail2
            Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To max

                '許可日
                .SetValidateCell(i, LMM130G.sprDetailDef2.WH_KYOKA_DATE.ColNo)
                .ItemName = LMM130G.sprDetailDef2.WH_KYOKA_DATE.ColName
                .IsFullByteCheck = 10
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Me._Frm.tabTouSitu.SelectedTab = Me._Frm.tpgTouSituDetail
                    Return errorFlg
                End If

            Next

        End With

        Return True

    End Function

    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    ''' <summary>
    ''' 申請外の商品保管許可ルールSpreadの単項目チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsTouSituExpChk() As Boolean

        '**********申請外の商品保管許可ルールスプレッドのチェック
        Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprDetail3)

        With vCell

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False
            Dim spr As LMSpread = Me._Frm.sprDetail3
            Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To max

                '適用日From 必須チェック、10バイトチェック
                .SetValidateCell(i, LMM130G.sprDetailDef3.APPLICATION_DATE_FROM.ColNo)
                .ItemName = LMM130G.sprDetailDef3.APPLICATION_DATE_FROM.ColName
                .IsHissuCheck = chkFlg
                .IsFullByteCheck = 10
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Me._Frm.tabTouSitu.SelectedTab = Me._Frm.tpgTouSituDetail
                    Return errorFlg
                End If

                '適用日To 必須チェック、10バイトチェック
                .SetValidateCell(i, LMM130G.sprDetailDef3.APPLICATION_DATE_TO.ColNo)
                .ItemName = LMM130G.sprDetailDef3.APPLICATION_DATE_TO.ColName
                .IsHissuCheck = chkFlg
                .IsFullByteCheck = 10
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Me._Frm.tabTouSitu.SelectedTab = Me._Frm.tpgTouSituDetail
                    Return errorFlg
                End If

            Next

        End With

        Return True

    End Function

    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

    ''' <summary>
    ''' 保存押下時関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveRelationChk() As Boolean

        Dim ctl As Control() = Nothing
        Dim focus As Control = Nothing

        With Me._Frm

            '******************** エラーチェック ********************

            '【最高設定温度上限・下限(大小チェック】
            If Convert.ToInt32(.numMaxOndoUp.Value) < Convert.ToInt32(.numMiniOndoDown.Value) Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E182", New String() {"最高設定温度上限", "最低設定温度下限"})
                MyBase.ShowMessage("E182", New String() {.lblMaxOndoUp.Text(), .lblMiniOndoDown.Text()})
                '2016.01.06 UMANO 英語化対応END
                ctl = New Control() {.numMaxOndoUp, .numMiniOndoDown}
                focus = .numMaxOndoUp
                Call Me._ControlV.SetErrorControl(ctl, focus, .tabTouSitu, .tpgTouSitu)
                Return False
            End If

            '【最高設定温度上限・下限・現在設定温度(整合性チェック】
            focus = .numOndo
            If Convert.ToInt32(.numOndo.Value) < Convert.ToInt32(.numMiniOndoDown.Value) _
            OrElse Convert.ToInt32(.numOndo.Value) > Convert.ToInt32(.numMaxOndoUp.Value) Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E014", New String() {"現在設定温度", "最低設定温度下限", "最高設定温度上限"})
                MyBase.ShowMessage("E014", New String() {.lblOndo.Text(), .lblMiniOndoDown.Text(), .lblMaxOndoUp.Text()})
                '2016.01.06 UMANO 英語化対応END
                ctl = New Control() {.numOndo, .numMaxOndoUp, .numMiniOndoDown}
                Call Me._ControlV.SetErrorControl(ctl, focus, .tabTouSitu, .tpgTouSitu)
                Return False
            End If

            '【温度管理区分・温度管理中(整合性チェック１】
            If .cmbOndoCtlKbn.SelectedValue.Equals(LMMControlC.FLG_ON) _
            AndAlso .cmbOndoCtlFlg.SelectedValue.Equals(LMMControlC.FLG_ON) Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E187", New String() {"温度管理区分が「常温」", "温度管理は「×」"})
                MyBase.ShowMessage("E876")
                '2016.01.06 UMANO 英語化対応END
                ctl = New Control() {.cmbOndoCtlKbn, .cmbOndoCtlFlg}
                focus = .cmbOndoCtlFlg
                Call Me._ControlV.SetErrorControl(ctl, focus, .tabTouSitu, .tpgTouSitu)
                Return False
            End If

            '【温度管理区分・温度管理中(整合性チェック２】
            If .cmbOndoCtlKbn.SelectedValue.Equals(LMM130C.ONDO_T) _
            AndAlso .cmbOndoCtlFlg.SelectedValue.Equals(LMMControlC.FLG_OFF) Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E187", New String() {"温度管理区分が「定温」", "温度管理は「○」"})
                MyBase.ShowMessage("E877")
                '2016.01.06 UMANO 英語化対応END
                ctl = New Control() {.cmbOndoCtlKbn, .cmbOndoCtlFlg}
                focus = .cmbOndoCtlFlg
                Call Me._ControlV.SetErrorControl(ctl, focus, .tabTouSitu, .tpgTouSitu)
                Return False
            End If

        End With

        Return True

    End Function

    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' 関連チェック(申請外の商品保管許可ルールSpread)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSaveRelationExpChk() As Boolean
        Dim chkFlg As Boolean = True
        Dim errorFlg As Boolean = False
        Dim spr As LMSpread = Me._Frm.sprDetail3
        Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
        Dim ctl As Control() = Nothing
        Dim focus As Control = Nothing

        '******************** エラーチェック ********************

        '【他社倉庫チェック】
        With Me._Frm
            If .cmbJisyatasyaKbn.SelectedValue.Equals(LMM130C.TASYA) Then
                If max >= 0 Then
                    '自社他社区分が他社の場合、申請外の商品保管許可ルールの設定はできません。
                    MyBase.ShowMessage("E961")
                    ctl = New Control() {.cmbJisyatasyaKbn}
                    focus = .cmbJisyatasyaKbn
                    Call Me._ControlV.SetErrorControl(ctl, focus, .tabTouSitu, .tpgTouSitu)
                    Return False
                End If
            End If
        End With

        With spr.ActiveSheet
            For i As Integer = 0 To max
                '【適用日FROMと適用日TOの逆転チェック】
                If Convert.ToDateTime(Nrs.Win.Utility.DateFormatUtility.EditSlash(_ControlG.GetCellValue(.Cells(i, LMM130C.SprColumnIndex3.APPLICATION_DATE_FROM)))) > _
                   Convert.ToDateTime(Nrs.Win.Utility.DateFormatUtility.EditSlash(_ControlG.GetCellValue(.Cells(i, LMM130C.SprColumnIndex3.APPLICATION_DATE_TO)))) Then
                    '適用日FROMは適用日TO以前になるように入力してください。
                    'MyBase.ShowMessage("E271", New String() {"適用日FROM", "適用日TO以前に"})
                    MyBase.ShowMessage("E962")
                    '適用日FROMに着色とフォーカスを設定
                    Call Me._ControlV.SetErrorControl(spr, i, LMM130C.SprColumnIndex3.APPLICATION_DATE_FROM, Me._Frm.tabTouSitu, Me._Frm.tpgTouSituDetail)
                    Return False
                End If

                '【適用日FROMと適用日TOの範囲チェック】
                '適用期間の取得
                Dim period As Integer = Convert.ToInt32(Convert.ToDecimal( _
                                        MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                        .Select("KBN_GROUP_CD = 'B032' AND KBN_CD = '1'")(0).Item("VALUE1")))

                If Convert.ToDateTime(Nrs.Win.Utility.DateFormatUtility.EditSlash(_ControlG.GetCellValue(.Cells(i, LMM130C.SprColumnIndex3.APPLICATION_DATE_FROM)))) <= _
                   Convert.ToDateTime(Nrs.Win.Utility.DateFormatUtility.EditSlash(_ControlG.GetCellValue(.Cells(i, LMM130C.SprColumnIndex3.APPLICATION_DATE_TO)))).AddDays(-1 * period - 1) Then
                    '適用日TOは適用日FROMの30日後より前になるように入力してください。
                    'MyBase.ShowMessage("E271", New String() {"適用日TO", "適用日FROMの30日後より前に"})
                    MyBase.ShowMessage("E963", New String() {period.ToString})
                    '適用日FROMに着色とフォーカスを設定
                    Call Me._ControlV.SetErrorControl(spr, i, LMM130C.SprColumnIndex3.APPLICATION_DATE_TO, Me._Frm.tabTouSitu, Me._Frm.tpgTouSituDetail)
                    Return False
                End If

                If Not i.Equals(max) Then
                    For j As Integer = i + 1 To max
                        '【同荷主で適用期間重複チェック】
                        If _ControlG.GetCellValue(.Cells(i, LMM130C.SprColumnIndex3.CUST_CD)).Equals( _
                            _ControlG.GetCellValue(.Cells(j, LMM130C.SprColumnIndex3.CUST_CD))) Then

                            If Convert.ToDateTime(Nrs.Win.Utility.DateFormatUtility.EditSlash(_ControlG.GetCellValue(.Cells(i, LMM130C.SprColumnIndex3.APPLICATION_DATE_FROM)))).Equals( _
                               Convert.ToDateTime(Nrs.Win.Utility.DateFormatUtility.EditSlash(_ControlG.GetCellValue(.Cells(j, LMM130C.SprColumnIndex3.APPLICATION_DATE_FROM))))) Then

                                '～行目と～行目の荷主と適用日FROMは重複しています。確認してください。
                                'MyBase.ShowMessage("E022", New String() {(i + 1).ToString & "行目と" & (j + 1).ToString & "行目の荷主と適用日FROM"})
                                MyBase.ShowMessage("E964", New String() {(i + 1).ToString, (j + 1).ToString})
                                '適用日FROM、TOに着色とフォーカスを設定
                                Call Me._ControlV.SetErrorControl(spr, i, LMM130C.SprColumnIndex3.APPLICATION_DATE_FROM, Me._Frm.tabTouSitu, Me._Frm.tpgTouSituDetail)
                                Call Me._ControlV.SetErrorControl(spr, j, LMM130C.SprColumnIndex3.APPLICATION_DATE_FROM, Me._Frm.tabTouSitu, Me._Frm.tpgTouSituDetail)
                                Return False
                            End If

                        End If
                    Next
                End If
            Next
        End With
        Return True
    End Function
    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' 棟室ゾーンチェックマスタSpreadの単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsExistTouSituZonChkSpr(ByVal spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread, ByVal kbnColmNo As Integer) As Boolean
        Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
        Dim id As String = String.Empty
        Dim chckId As String = String.Empty

        Dim sprName As String = String.Empty

        Select Case spr.Name
            Case "sprDetail4"
                sprName = "毒劇区分"
            Case "sprDetail5"
                sprName = "高圧ガス区分"
            Case "sprDetail6"
                sprName = "薬機法区分"
        End Select

        Dim vCell As LMValidatableCells = New LMValidatableCells(spr)

        '必須チェック
        For i As Integer = 0 To max

            vCell.SetValidateCell(i, kbnColmNo)
            vCell.ItemName = sprName
            vCell.IsHissuCheck = True

            If MyBase.IsValidateCheck(vCell) = False Then
                Call Me._ControlV.SetErrorControl(spr _
                                                      , i _
                                                      , kbnColmNo, Me._Frm.tabTouSitu, Me._Frm.tpgTouSituDetail)
                Return False
            End If
        Next

        '重複チェック(同一の区分が使用されている場合エラー)
        For i As Integer = 0 To max

            With spr.ActiveSheet
                id = Me._ControlV.GetCellValue(.Cells(i, kbnColmNo))
                If i <> max Then
                    For j As Integer = i + 1 To max
                        chckId = Me._ControlV.GetCellValue(.Cells(j, kbnColmNo))
                        If id.Equals(chckId) = True Then

                            MyBase.ShowMessage("E496", {sprName, "当該行：" & (i + 1).ToString() & "行目，" & (j + 1).ToString() & "行目"})
                            Call Me._ControlV.SetErrorControl(spr, {i, j}, {kbnColmNo, kbnColmNo}, Me._Frm.tabTouSitu, Me._Frm.tpgTouSituDetail)
                            Return False
                        End If
                    Next
                End If
            End With
        Next

        Return True

    End Function

    ''' <summary>
    ''' 非表示項目（毒劇区分、ガス管理区分）設定
    ''' </summary>
    Private Sub SetCmbValue()

        Dim spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
        Dim max As Integer
        Dim id As String = String.Empty
        Dim tempId As String = String.Empty

        With Me._Frm
            '毒劇区分
            spr = .sprDetail4
            max = spr.ActiveSheet.Rows.Count - 1

            '毒劇区分を無に設定
            .cmbDokuKbn.SelectedValue = String.Empty

            For i As Integer = 0 To max

                id = Me._ControlV.GetCellValue(spr.ActiveSheet.Cells(i, LMM130G.sprDetailDef4.DOKU_KB.ColNo))

                '毒劇区分を 特定毒物 > 毒物 > 劇物 > なしで設定
                If String.IsNullOrEmpty(id) = False Then

                    If LMM130C.M_Z_KBN_DOKUGEKI_TOKU.Equals(id) Then

                        tempId = LMM130C.M_Z_KBN_DOKUGEKI_TOKU
                        Exit For '最上位なので、以降確認不要

                    ElseIf LMM130C.M_Z_KBN_DOKUGEKI_DOKU.Equals(id) _
                        And tempId.Equals(LMM130C.M_Z_KBN_DOKUGEKI_TOKU) = False Then

                        tempId = LMM130C.M_Z_KBN_DOKUGEKI_DOKU

                    ElseIf LMM130C.M_Z_KBN_DOKUGEKI_GEKI.Equals(id) _
                        And tempId.Equals(LMM130C.M_Z_KBN_DOKUGEKI_TOKU) = False _
                        And tempId.Equals(LMM130C.M_Z_KBN_DOKUGEKI_DOKU) = False Then

                        tempId = LMM130C.M_Z_KBN_DOKUGEKI_GEKI

                    ElseIf LMM130C.M_Z_KBN_DOKUGEKI_NASI.Equals(id) _
                        And String.IsNullOrEmpty(tempId) Then

                        tempId = LMM130C.M_Z_KBN_DOKUGEKI_NASI

                    End If

                End If
            Next

            Me._Frm.cmbDokuKbn.SelectedValue = tempId

            'ガス管理区分
            .cmbGasKanriKbn.SelectedValue = String.Empty '無に設定

        End With

    End Sub

    ''' <summary>
    ''' 検索押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuInputChk() As Boolean

        'スペース除去
        Call Me.TrinmFindRow()

        '単項目チェック
        If Me.IsKensakuSingleChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 行追加/行削除 入力チェック。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsRowCheck(ByVal eventShubetsu As LMM130C.EventShubetsu, ByVal ds As DataSet, ByVal frm As LMM130F) As Boolean
        Dim arr As ArrayList = Nothing

        Select Case eventShubetsu
            Case LMM130C.EventShubetsu.INS_T    '行追加

                Dim outSDt As DataTable = ds.Tables(LMZ280C.TABLE_NM_OUT)
                Dim outSRow As DataRow = Nothing

                For j As Integer = 0 To outSDt.Rows.Count - 1

                    outSRow = outSDt.Rows(j)
                    Dim ShoboCd As String = String.Empty
                    ShoboCd = outSRow.Item("SHOBO_CD").ToString

                    With Me._Frm

                        Dim sprMax As Integer = .sprDetail2.ActiveSheet.Rows.Count - 1
                        For i As Integer = 0 To sprMax

                            If (ShoboCd).Equals(_ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM130C.SprColumnIndex2.SHOBO_CD))) Then
                                '2016.01.06 UMANO 英語化対応START
                                'MyBase.ShowMessage("E177", New String() {"消防情報"})
                                MyBase.ShowMessage("E177", New String() {frm.grpShoboJoho.Text()})
                                '2016.01.06 UMANO 英語化対応END
                                Return False
                            End If
                        Next

                    End With
                Next

                Return True

            Case LMM130C.EventShubetsu.DEL_T    '行削除

                With Me._Frm
                    '選択ﾁｪｯｸ
                    arr = Nothing
                    arr = Me.getCheckList(.sprDetail2)
                    If 0 = arr.Count Then
                        .sprDetail2.Focus()
                        MyBase.ShowMessage("E009")
                        Return False
                    End If

                End With

                Return True

        End Select

    End Function

    '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' 行追加/行削除 入力チェック。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsExpRowCheck(ByVal eventShubetsu As LMM130C.EventShubetsu, ByVal frm As LMM130F) As Boolean
        Dim arr As ArrayList = Nothing

        Select Case eventShubetsu
            Case LMM130C.EventShubetsu.INS_EXP_T    '行追加

                Return True

            Case LMM130C.EventShubetsu.DEL_EXP_T    '行削除

                With frm
                    '選択ﾁｪｯｸ
                    arr = Nothing
                    arr = Me.getCheckExpList(.sprDetail3)
                    If 0 = arr.Count Then
                        .sprDetail3.Focus()
                        MyBase.ShowMessage("E009")
                        Return False
                    End If

                End With

                Return True

        End Select

    End Function

    ''' <summary>
    ''' 行追加/行削除 入力チェック 棟室ゾーンチェックマスタスプレッド(3種)
    ''' </summary>
    ''' <param name="eventShubetsu"></param>
    ''' <param name="frm"></param>
    ''' <param name="spr"></param>
    ''' <param name="defColNo"></param>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsTouSituZoneChkRowCheck(ByVal eventShubetsu As LMM130C.EventShubetsu, ByVal frm As LMM130F, ByVal spr As LMSpread, ByVal defColNo As Integer) As Boolean

        Dim arr As ArrayList = Nothing

        Select Case eventShubetsu
            Case LMM130C.EventShubetsu.INS_DOKU,
                 LMM130C.EventShubetsu.INS_KOUATHUGAS,
                 LMM130C.EventShubetsu.INS_YAKUZIHO     '行追加

                '空行チェック
                If Me.IsKuranChk(spr, defColNo) = False Then
                    Return False
                End If

                Return True

            Case LMM130C.EventShubetsu.DEL_DOKU,
                 LMM130C.EventShubetsu.DEL_KOUATHUGAS,
                 LMM130C.EventShubetsu.DEL_YAKUZIHO     '行削除

                '選択ﾁｪｯｸ
                arr = _ControlV.SprSelectList(defColNo, spr)
                If 0 = arr.Count Then
                    spr.Focus()
                    MyBase.ShowMessage("E009")
                    Return False
                End If

                Return True

        End Select

    End Function

    ''' <summary>
    ''' 一括登録　押下時チェック
    ''' </summary>
    ''' <param name="isExistMCustCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IkkatuTourokuCheck(ByVal isExistMCustCd As Boolean) As Boolean

        Dim lgm As New Utility.lmLangMGR(Nrs.Win.Base.MessageManager.MessageLanguage)

        '【適用日FROM未入力チェック】
        If Me._Frm.imdSearchDate_From.TextValue.Equals(String.Empty) Then
            '適用日FROMは必須入力です。
            MyBase.ShowMessage("E001", New String() {lgm.Selector({"適用日FROM", "Apply date From", "적용일FROM", "中国語"})})
            Call Me._ControlV.SetErrorControl(Me._Frm.imdSearchDate_From)
            Return False
        End If

        '【適用日FROM入力8バイトチェック】
        If Not Me._Frm.imdSearchDate_From.IsDateFullByteCheck(8) Then
            '適用日FROMは8バイトで入力してください。
            'MyBase.ShowMessage("E038", New String() {"適用日FROM", "8"})
            MyBase.ShowMessage("E038", New String() {lgm.Selector({"適用日FROM", "Apply date From", "적용일FROM", "中国語"}), "8"})
            Call Me._ControlV.SetErrorControl(Me._Frm.imdSearchDate_From)
            Return False
        End If

        '【適用日TO未入力チェック】
        If Me._Frm.imdSearchDate_To.TextValue.Equals(String.Empty) Then
            '適用日TOは必須入力です。
            'MyBase.ShowMessage("E001", New String() {"適用日TO"})
            MyBase.ShowMessage("E001", New String() {lgm.Selector({"適用日TO", "Apply date To", "적용일TO", "中国語"})})
            Call Me._ControlV.SetErrorControl(Me._Frm.imdSearchDate_To)
            Return False
        End If

        '【適用日TO入力8バイトチェック】
        If Not Me._Frm.imdSearchDate_To.IsDateFullByteCheck(8) Then
            '適用日FROMは8バイトで入力してください。
            'MyBase.ShowMessage("E038", New String() {"適用日TO", "8"})
            MyBase.ShowMessage("E038", New String() {lgm.Selector({"適用日TO", "Apply date To", "적용일TO", "中国語"}), "8"})
            Call Me._ControlV.SetErrorControl(Me._Frm.imdSearchDate_To)
            Return False
        End If

        '【適用日FROMと適用日TOの逆転チェック】
        If Convert.ToDateTime(Nrs.Win.Utility.DateFormatUtility.EditSlash(Me._Frm.imdSearchDate_From.TextValue)) > _
           Convert.ToDateTime(Nrs.Win.Utility.DateFormatUtility.EditSlash(Me._Frm.imdSearchDate_To.TextValue)) Then
            '適用日FROMは適用日TO以前になるように入力してください。
            'MyBase.ShowMessage("E271", New String() {"適用日FROM", "適用日TO以前に"})
            MyBase.ShowMessage("E962")
            '適用日FROMに着色とフォーカスを設定
            Call Me._ControlV.SetErrorControl(Me._Frm.imdSearchDate_From)
            Return False
        End If

        '【適用日FROMと適用日TOの範囲チェック】
        '適用期間の取得
        Dim period As Integer = Convert.ToInt32(Convert.ToDecimal( _
                                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                .Select("KBN_GROUP_CD = 'B032' AND KBN_CD = '1'")(0).Item("VALUE1")))

        If Convert.ToDateTime(Nrs.Win.Utility.DateFormatUtility.EditSlash(Me._Frm.imdSearchDate_From.TextValue)) <= _
           Convert.ToDateTime(Nrs.Win.Utility.DateFormatUtility.EditSlash(Me._Frm.imdSearchDate_To.TextValue)).AddDays(-1 * period - 1) Then
            '適用日TOは適用日FROMの30日後より前になるように入力してください。
            'MyBase.ShowMessage("E271", New String() {"適用日TO", "適用日FROMの30日後より前に"})
            MyBase.ShowMessage("E963", New String() {period.ToString})
            '適用日FROMに着色とフォーカスを設定
            Call Me._ControlV.SetErrorControl(Me._Frm.imdSearchDate_To)
            Return False
        End If

        '【荷主コードチェック】
        If Me._Frm.txtCustCD.TextValue.Equals(String.Empty) Then
            '荷主は必須入力です。
            'MyBase.ShowMessage("E001", New String() {"荷主"})
            MyBase.ShowMessage("E001", New String() {lgm.Selector({"荷主", "Shipper", "하주", "中国語"})})
            Call Me._ControlV.SetErrorControl(Me._Frm.txtCustCD)
            Return False
        End If

        Me._Frm.txtCustCD.IsForbiddenWordsCheck() = True
        Me._Frm.txtCustCD.IsByteCheck() = 5
        If Not MyBase.IsValidateCheck(Me._Frm.txtCustCD) OrElse Not isExistMCustCd Then
            '荷主の入力コードが正しくありません。
            'MyBase.ShowMessage("E030", New String() {"荷主"})
            MyBase.ShowMessage("E030", New String() {lgm.Selector({"荷主", "Shipper", "하주", "中国語"})})
            Call Me._ControlV.SetErrorControl(Me._Frm.txtCustCD)
            Return False
        End If

        Return True

    End Function

    '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

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
    ''' 検索行のTrim処理を行う
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrinmFindRow()

        With Me._Frm.sprDetail
            Call Me._ControlV.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0)
        End With

    End Sub

    ''' <summary>
    ''' 検索押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleChk() As Boolean

        Dim chkFlg As Boolean = True
        Dim errorFlg As Boolean = False


        With Me._Frm

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            '棟番号
            vCell.SetValidateCell(0, LMM130G.sprDetailDef.TOU_NO.ColNo)
            vCell.ItemName = LMM130G.sprDetailDef.TOU_NO.ColName
            vCell.IsByteCheck = 2
            vCell.IsForbiddenWordsCheck = chkFlg
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

            '室番号
            vCell.SetValidateCell(0, LMM130G.sprDetailDef.SITU_NO.ColNo)
            vCell.ItemName = LMM130G.sprDetailDef.SITU_NO.ColName
            'START YANAI 要望番号705
            'vCell.IsByteCheck = 1
            vCell.IsByteCheck = 2
            'END YANAI 要望番号705
            vCell.IsForbiddenWordsCheck = chkFlg
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

            '棟番名
            vCell.SetValidateCell(0, LMM130G.sprDetailDef.TOU_SITU_NM.ColNo)
            vCell.ItemName = LMM130G.sprDetailDef.TOU_SITU_NM.ColName
            vCell.IsByteCheck = 60
            vCell.IsForbiddenWordsCheck = chkFlg
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If


        End With

        Return True

    End Function

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList(ByVal sprDetail As Spread.LMSpread) As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = 0
        If ("sprDetail2").Equals(sprDetail.Name) = True Then
            defNo = LMM130C.SprColumnIndex2.DEF
        End If

        '選択された行の行番号を取得
        Return _ControlV.SprSelectList(defNo, sprDetail)

    End Function

    '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckExpList(ByVal sprDetail As Spread.LMSpread) As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = 0
        If ("sprDetail3").Equals(sprDetail.Name) = True Then
            defNo = LMM130C.SprColumnIndex3.DEF
        End If

        '選択された行の行番号を取得
        Return _ControlV.SprSelectList(defNo, sprDetail)

    End Function
    '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 

    ''' <summary>
    '''行追加時、Spreadに何も入力されていない場合、エラー
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKuranChk(ByVal spr As LMSpread, ByVal defColNo As Integer) As Boolean

        With spr.ActiveSheet

            Dim rowMax As Integer = .Rows.Count - 1
            Dim chkFlg As Boolean = False

            For i As Integer = 0 To rowMax
                If String.IsNullOrEmpty(Me._ControlV.GetCellValue(.Cells(i, defColNo + 1))) = False Then
                    chkFlg = True
                End If

                If chkFlg = False Then
                    MyBase.ShowMessage("E219")
                    Return False
                End If

                '初期値設定
                chkFlg = False
            Next
        End With

        Return True

    End Function
#End Region

#End Region 'Method

End Class
