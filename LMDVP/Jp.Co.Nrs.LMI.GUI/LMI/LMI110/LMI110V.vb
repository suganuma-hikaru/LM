' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : 特殊荷主機能
'  プログラムID     :  LMI110V : 日医工製品マスタ登録
'  作  成  者       :  [寺川徹]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMI110Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMI110V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI110F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMIControlV

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlC As LMIControlC

    ''' <summary>
    ''' チェックリストを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI110F, ByVal v As LMIControlV)

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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI110C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI110C.EventShubetsu.KENSAKU         '検索
                '10:閲覧者、50:外部の場合エラー
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

            Case LMI110C.EventShubetsu.MASTEROPEN    'マスタ参照
                '10:閲覧者、50:外部の場合エラー
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

            Case LMI110C.EventShubetsu.SAVEGOODSM    '商品M反映
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

            Case LMI110C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMI110C.EventShubetsu.ENTER          'Enter
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
    ''' 検索押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuInputChk() As Boolean


        '単項目チェック
        If Me.IsKensakuSingleChk() = False Then
            Return False
        End If

        '関連チェック
        If Me.IsKensakuKanrenChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 商品マスタ参照時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsMasterShowInputChk() As Boolean


        '単項目チェック
        If Me.IsMasterShowSingleChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As String) As Boolean

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

                Case .txtSerchGoodsCd.Name, .txtSerchGoodsNm.Name

                    'Dim goodsNm As String = .lblTitleGoods.Text
                    'txtCtl = New Win.InputMan.LMImTextBox() {.txtSerchGoodsCd, .txtSerchGoodsNm}
                    'lblCtl = New Control() {.lblTitleGoods, .lblTitleGoods}
                    'msg = New String() {goodsNm, goodsNm}

                    Return True

            End Select

            Return Me._ControlV.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)
            'Return False

        End With

    End Function

    ''' <summary>
    ''' 商品M反映時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsSaveGoodsMChk() As Boolean

        With Me._Frm

            ''スペース除去
            ''Call Me._ControlV.TrimSpaceTextvalue(.tabGoodsMst)
            ''Call Me._ControlV.TrimSpaceSprTextvalue(.sprGoodsDetail _
            ''                                        , .sprGoodsDetail.ActiveSheet.Rows.Count - 1 _
            ''                                        , LMI110G.sprGoodsDtlDef.BIKO.ColNo)

            '単項目チェック
            If Me.IsSaveGoodsMSingleChk() = False Then
                Return False
            End If

            '自営業所チェック
            Dim rtnMsg As String = String.Empty

            rtnMsg = LMIControlC.FUNCTION_GOODSM

            If Me.IsNrsChk(rtnMsg) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 基本メッセージ設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetBaseMsg()

        MyBase.ShowMessage("G013")

    End Sub


#Region "内部メソッド"

    ''' <summary>
    ''' 検索押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleChk() As Boolean

        With Me._Frm
            'EDI取込日(FROM)
            If .imdEdiDateFrom.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"EDI取込日From", "8"})
                Return False
            End If

            'EDI取込日(TO)
            If .imdEdiDateTo.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"EDI取込日To", "8"})
                Return False
            End If

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprNichikoGoods)

            '【製品コード】
            vCell.SetValidateCell(0, LMI110G.sprNichikoGoods.GOODS_CD.ColNo)
            vCell.ItemName = LMI110G.sprNichikoGoods.GOODS_CD.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 9
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If


            '【製品名（漢字）】
            vCell.SetValidateCell(0, LMI110G.sprNichikoGoods.GOODS_NM.ColNo)
            vCell.ItemName = LMI110G.sprNichikoGoods.GOODS_NM.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 50
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【製品名（カナ）】
            vCell.SetValidateCell(0, LMI110G.sprNichikoGoods.GOODS_NM_KANA.ColNo)
            vCell.ItemName = LMI110G.sprNichikoGoods.GOODS_NM_KANA.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 50
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【製品名（規格漢字）】
            vCell.SetValidateCell(0, LMI110G.sprNichikoGoods.GOODS_KIKAKU.ColNo)
            vCell.ItemName = LMI110G.sprNichikoGoods.GOODS_KIKAKU.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 30
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【製品名（規格カナ）】
            vCell.SetValidateCell(0, LMI110G.sprNichikoGoods.GOODS_KIKAKU_KANA.ColNo)
            vCell.ItemName = LMI110G.sprNichikoGoods.GOODS_KIKAKU_KANA.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 30
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【JANコード（規格カナ）】
            vCell.SetValidateCell(0, LMI110G.sprNichikoGoods.JAN_CD.ColNo)
            vCell.ItemName = LMI110G.sprNichikoGoods.JAN_CD.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 13
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 検索押下時関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuKanrenChk() As Boolean

        With Me._Frm

            '【EDI取込日(FROM)、(TO)】　大小チェック
            If String.IsNullOrEmpty(.imdEdiDateFrom.TextValue) = False AndAlso _
               String.IsNullOrEmpty(.imdEdiDateTo.TextValue) = False AndAlso _
              Convert.ToInt32(.imdEdiDateFrom.TextValue) <= Convert.ToInt32(.imdEdiDateTo.TextValue) = False Then
                MyBase.ShowMessage("E039", New String() {"EDI取込日(To)", "EDI取込日(From)"})
                Return False
            End If

        End With

        Return True

    End Function


    ''' <summary>
    ''' 商品マスタポップ単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMasterShowSingleChk() As Boolean

        With Me._Frm
            '【商品コード】
            .txtSerchGoodsCd.ItemName = "商品コード"
            .txtSerchGoodsCd.IsForbiddenWordsCheck = True
            .txtSerchGoodsCd.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtSerchGoodsCd) = False Then
                Call Me._ControlV.SetErrorControl(.txtSerchGoodsCd)
                Return False
            End If

            '【商品名】
            .txtSerchGoodsNm.ItemName = "商品名"
            .txtSerchGoodsNm.IsForbiddenWordsCheck = True
            .txtSerchGoodsNm.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtSerchGoodsNm) = False Then
                Call Me._ControlV.SetErrorControl(.txtSerchGoodsNm)
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 商品M反映押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveGoodsMSingleChk() As Boolean

        With Me._Frm
            '1件もチェックがされていない時、エラー
            Dim chkList As ArrayList = Me.getCheckList()
            If chkList.Count() = 0 Then
                MyBase.ShowMessage("E009")
                Return False
            End If
        End With

        Return True

    End Function

    ''' <summary>
    ''' 保存押下時マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveExistMst(ByVal dateStr As String) As Boolean

    End Function

#Region "関連チェック"

    ''' <summary>
    ''' 商品Ｍ反映時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSaveGoodsMRelationChk(ByRef errDs As DataSet) As Hashtable

        '続行確認
        'Dim rtn As MsgBoxResult

        Dim errHt As Hashtable = New Hashtable

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0

        Dim mGoodsNrs As String = Me._Frm.txtGoodsKey.TextValue.ToString()

        Dim status As String = String.Empty
        Dim seihinDelF As String = String.Empty
        Dim goodsCdNik As String = String.Empty
        Dim nikOndoKb As String = String.Empty
        Dim goodsOndoKb As String = String.Empty
        Dim seihinIrisuNb As Decimal = 0
        Dim seihinIrimeNb As Decimal = 0
        Dim goodsIrimeNb As Decimal = 0
        Dim seihinPkgNb As Integer = 0
        Dim goodsPkgNb As Integer = 0
        Dim errId As String = String.Empty
        Dim torikomiKb As String = String.Empty

        '******************** Spread項目の入力チェック ********************
        Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprNichikoGoods)

        For i As Integer = 0 To max

            With Me._Frm.sprNichikoGoods.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                status = .Cells(selectRow, LMI110G.sprNichikoGoods.STATUS.ColNo).Value().ToString().Trim()                                      'ステータス
                seihinDelF = .Cells(selectRow, LMI110G.sprNichikoGoods.M_SEIHIN_SYS_DEL_FLAG.ColNo).Value().ToString()                          '削除F(日医工製品M)
                goodsCdNik = .Cells(selectRow, LMI110G.sprNichikoGoods.GOODS_CD.ColNo).Value().ToString()                                       '製品CD(日医工製品M)
                nikOndoKb = .Cells(selectRow, LMI110G.sprNichikoGoods.ONDO_KB.ColNo).Value().ToString()                                         '保管温度区分(日医工製品M)
                goodsOndoKb = .Cells(selectRow, LMI110G.sprNichikoGoods.M_GOODS_ONDO_KB.ColNo).Value().ToString()                               '温度区分(商品M)
                seihinIrisuNb = Convert.ToDecimal(.Cells(selectRow, LMI110G.sprNichikoGoods.PKG_NB.ColNo).Value())                        '入目(画面入力)
                seihinIrimeNb = Convert.ToDecimal(.Cells(selectRow, LMI110G.sprNichikoGoods.STD_IRIME_NB.ColNo).Value())                        '入目(画面入力)
                goodsIrimeNb = Convert.ToDecimal(.Cells(selectRow, LMI110G.sprNichikoGoods.M_GOODS_STD_IRIME_NB.ColNo).Value())                 '標準入目(商品M)
                seihinPkgNb = Convert.ToInt32(.Cells(selectRow, LMI110G.sprNichikoGoods.PKG_NB.ColNo).Value())                                  '入数(日医工製品M)
                goodsPkgNb = Convert.ToInt32(.Cells(selectRow, LMI110G.sprNichikoGoods.M_GOODS_PKG_NB.ColNo).Value())                           '包装個数(商品M)
                torikomiKb = .Cells(selectRow, LMI110G.sprNichikoGoods.TORIKOMI_KBN.ColNo).Value().ToString()                                   '取込区分(日医工製品M)商品マスタ反映フラグ


                '①削除データチェック
                If seihinDelF.Equals("1") = True Then

                    errId = "E336"
                    errDs = Me.getErrMsgSetDataSet(errId, New String() {"削除データ", _
                                                                        LMIControlC.FUNCTION_GOODSM, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty}, _
                                                                        goodsCdNik, selectRow.ToString(), errDs)
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                '【入数】(必須チェック)
                vCell.SetValidateCell(selectRow, LMI110G.sprNichikoGoods.PKG_NB.ColNo)
                vCell.ItemName = LMI110G.sprNichikoGoods.PKG_NB.ColName
                vCell.IsHissuCheck() = True
                If MyBase.IsValidateCheck(vCell) = False Then

                    errId = "E001"
                    errDs = Me.getErrMsgSetDataSet(errId, New String() {vCell.ItemName, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty}, _
                                                                        goodsCdNik, selectRow.ToString(), errDs)
                    errHt.Add(i, String.Empty)
                    Continue For

                End If

                '【入数】(数値チェック)
                vCell.SetValidateCell(selectRow, LMI110G.sprNichikoGoods.PKG_NB.ColNo)
                vCell.ItemName = LMI110G.sprNichikoGoods.PKG_NB.ColName
                vCell.IsForbiddenWordsCheck = True
                If MyBase.IsValidateCheck(vCell) = False Then

                    errId = "E005"
                    errDs = Me.getErrMsgSetDataSet(errId, New String() {vCell.ItemName, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty}, _
                                                                        goodsCdNik, selectRow.ToString(), errDs)
                    errHt.Add(i, String.Empty)
                    Continue For

                End If

                '【入目】(バイト数チェック)
                vCell.SetValidateCell(selectRow, LMI110G.sprNichikoGoods.PKG_NB.ColNo)
                vCell.ItemName = LMI110G.sprNichikoGoods.PKG_NB.ColName
                vCell.IsByteCheck = 8
                If MyBase.IsValidateCheck(vCell) = False Then

                    errId = "E007"
                    errDs = Me.getErrMsgSetDataSet(errId, New String() {vCell.ItemName, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty}, _
                                                                        goodsCdNik, selectRow.ToString(), errDs)
                    errHt.Add(i, String.Empty)
                    Continue For

                End If

                '⑦入力入目チェック

                '一覧で選択されている日医工製品マスタデータの入目が０の場合はエラー
                If seihinIrisuNb = 0 Then

                    errId = "E320"
                    errDs = Me.getErrMsgSetDataSet(errId, New String() {"入力した入数が０", _
                                                                        "商品マスタ反映", _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty}, _
                                                                        goodsCdNik, selectRow.ToString(), errDs)
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                '【個数単位】
                vCell.SetValidateCell(selectRow, LMI110G.sprNichikoGoods.NB_UT.ColNo)
                vCell.ItemName = LMI110G.sprNichikoGoods.NB_UT.ColName
                vCell.IsHissuCheck() = True
                If MyBase.IsValidateCheck(vCell) = False Then

                    errId = "E001"
                    errDs = Me.getErrMsgSetDataSet(errId, New String() {vCell.ItemName, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty}, _
                                                                        goodsCdNik, selectRow.ToString(), errDs)
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                '【入目】(必須チェック)
                vCell.SetValidateCell(selectRow, LMI110G.sprNichikoGoods.STD_IRIME_NB.ColNo)
                vCell.ItemName = LMI110G.sprNichikoGoods.STD_IRIME_NB.ColName
                vCell.IsHissuCheck() = True
                If MyBase.IsValidateCheck(vCell) = False Then

                    errId = "E001"
                    errDs = Me.getErrMsgSetDataSet(errId, New String() {vCell.ItemName, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty}, _
                                                                        goodsCdNik, selectRow.ToString(), errDs)
                    errHt.Add(i, String.Empty)
                    Continue For

                End If

                '【入目】(数値チェック)
                vCell.SetValidateCell(selectRow, LMI110G.sprNichikoGoods.STD_IRIME_NB.ColNo)
                vCell.ItemName = LMI110G.sprNichikoGoods.STD_IRIME_NB.ColName
                vCell.IsForbiddenWordsCheck = True
                If MyBase.IsValidateCheck(vCell) = False Then

                    errId = "E005"
                    errDs = Me.getErrMsgSetDataSet(errId, New String() {vCell.ItemName, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty}, _
                                                                        goodsCdNik, selectRow.ToString(), errDs)
                    errHt.Add(i, String.Empty)
                    Continue For

                End If

                '【入目】(バイト数チェック)
                vCell.SetValidateCell(selectRow, LMI110G.sprNichikoGoods.STD_IRIME_NB.ColNo)
                vCell.ItemName = LMI110G.sprNichikoGoods.STD_IRIME_NB.ColName
                vCell.IsByteCheck = 9
                If MyBase.IsValidateCheck(vCell) = False Then

                    errId = "E007"
                    errDs = Me.getErrMsgSetDataSet(errId, New String() {vCell.ItemName, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty}, _
                                                                        goodsCdNik, selectRow.ToString(), errDs)
                    errHt.Add(i, String.Empty)
                    Continue For

                End If

                '⑤入力入目チェック

                '一覧で選択されている日医工製品マスタデータの入目が０の場合はエラー
                If seihinIrimeNb = 0 Then

                    errId = "E320"
                    errDs = Me.getErrMsgSetDataSet(errId, New String() {"入力した入目が０", _
                                                                        "商品マスタ反映", _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty}, _
                                                                        goodsCdNik, selectRow.ToString(), errDs)
                    errHt.Add(i, String.Empty)
                    Continue For
                End If


                '【標準入目単位】(必須チェック)
                vCell.SetValidateCell(selectRow, LMI110G.sprNichikoGoods.STD_IRIME_UT.ColNo)
                vCell.ItemName = LMI110G.sprNichikoGoods.STD_IRIME_UT.ColName
                vCell.IsHissuCheck() = True
                If MyBase.IsValidateCheck(vCell) = False Then

                    errId = "E001"
                    errDs = Me.getErrMsgSetDataSet(errId, New String() {vCell.ItemName, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty}, _
                                                                        goodsCdNik, selectRow.ToString(), errDs)
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                '②ステータスチェック
                '③ステータス＋日医工製品マスタ商品マスタ整合性チェック
                Select Case status

                    ' 一覧で選択されている日医工製品マスタデータのステータスが"新規"の場合、
                    Case LMI110C.MGOODS_NEW

                        '商品マスタPOP(LMZ020)より商品を選択していなければエラー
                        If String.IsNullOrEmpty(mGoodsNrs) = True Then

                            errId = "E199"
                            errDs = Me.getErrMsgSetDataSet(errId, New String() {"反映を行う商品情報", _
                                                                                String.Empty, _
                                                                                String.Empty, _
                                                                                String.Empty, _
                                                                                String.Empty}, _
                                                                                goodsCdNik, selectRow.ToString(), errDs)
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        ' 一覧で選択されている日医工製品マスタデータのステータスが"変更"の場合、
                    Case LMI110C.MGOODS_EDIT

                        ''①日医工製品マスタ：保管温度区分(ONDO_KB) <> 商品マスタ：温度管理区分(ONDO_KB)　の場合はワーニング
                        'If nikOndoKb.Equals(goodsOndoKb) = False Then

                        '    errId = "E332"
                        '    errDs = Me.getErrMsgSetDataSet(errId, New String() {"入目", _
                        '                                                        "商品マスタ", _
                        '                                                        "標準入目", _
                        '                                                        String.Empty, _
                        '                                                        String.Empty}, _
                        '                                                        goodsCdNik, selectRow.ToString(), errDs)
                        '    errHt.Add(i, String.Empty)
                        '    Continue For
                        'End If

                        ''②日医工製品マスタ：梱包入目数(PKG_NB) <> 画面入力：包装個数(PKG_NB) 　の場合はエラー
                        'If seihinPkgNb <> goodsPkgNb Then

                        '    errId = "E332"
                        '    errDs = Me.getErrMsgSetDataSet(errId, New String() {"入数(梱包入目数)", _
                        '                                                        "商品マスタ", _
                        '                                                        "入数(包装個数)", _
                        '                                                        String.Empty, _
                        '                                                        String.Empty}, _
                        '                                                        goodsCdNik, selectRow.ToString(), errDs)
                        '    errHt.Add(i, String.Empty)
                        '    Continue For
                        'End If

                        ''③日医工製品マスタ：個装重量(NB_WT_GS) <> 画面入力：標準入目(STD_IRIME_NB)　の場合はエラー
                        'If seihinIrimeNb <> goodsIrimeNb Then

                        '    errId = "E332"
                        '    errDs = Me.getErrMsgSetDataSet(errId, New String() {"入目", _
                        '                                                        "商品マスタ", _
                        '                                                        "標準入目", _
                        '                                                        String.Empty, _
                        '                                                        String.Empty}, _
                        '                                                        goodsCdNik, selectRow.ToString(), errDs)
                        '    errHt.Add(i, String.Empty)
                        '    Continue For
                        'End If

                        ' 一覧で選択されている日医工製品マスタデータのｽﾃｰﾀｽが"商品M重複"の場合はエラー
                    Case LMI110C.MGOODS_DOUBLE

                        errId = "E022"
                        errDs = Me.getErrMsgSetDataSet(errId, New String() {"商品コード", _
                                                                            String.Empty, _
                                                                            String.Empty, _
                                                                            String.Empty, _
                                                                            String.Empty}, _
                                                                            goodsCdNik, selectRow.ToString(), errDs)
                        errHt.Add(i, String.Empty)
                        Continue For
                End Select

                '④取込区分チェック

                ' 一覧で選択されている日医工製品マスタデータの取込区分が"反映済"の場合はエラー
                If torikomiKb.Equals(LMI110C.HANEI_SUMI) = True Then

                    errId = "E320"
                    errDs = Me.getErrMsgSetDataSet(errId, New String() {LMI110C.HANEI_SUMI, _
                                                                        "商品マスタ反映", _
                                                                        String.Empty, _
                                                                        String.Empty, _
                                                                        String.Empty}, _
                                                                        goodsCdNik, selectRow.ToString(), errDs)
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

            End With

        Next

        Return errHt

    End Function

#End Region

#Region "選択行取得"
    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMI110C.SprGoodsColumnIndex.DEF

        Return Me._ControlV.SprSelectList(defNo, Me._Frm.sprNichikoGoods)

    End Function

#End Region

#Region "自営業所チェック"
    ''' <summary>
    ''' 自営業所チェック
    ''' </summary>
    ''' <returns>True:エラーなし, False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsNrsChk(ByVal rtnMsg As String) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        'If LMUserInfoManager.GetNrsBrCd().Equals(Me._Frm.cmbEigyo.SelectedValue.ToString()) = False Then
        '    Return Me._ControlV.SetErrMessage("E178", New String() {rtnMsg})
        'End If

        Return True

    End Function
#End Region

#Region "エラーメッセージIDデータセット格納"
    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getErrMsgSetDataSet(ByVal msgId As String, ByVal msgStr As String(), _
                                        ByVal goodsCdNik As String, ByVal selectRow As String, _
                                        ByRef errDs As DataSet) As DataSet

        Dim dr As DataRow

        'エラーがある場合、DataTableに設定
        dr = errDs.Tables(LMI110C.TABLE_NM_GUIERROR).NewRow()
        dr("GUIDANCE_ID") = LMI110C.GUIDANCE_KBN
        dr("MESSAGE_ID") = msgId
        dr("PARA1") = msgStr(0)
        dr("PARA2") = msgStr(1)
        dr("PARA3") = msgStr(2)
        dr("PARA4") = msgStr(3)
        dr("PARA5") = msgStr(4)
        dr("KEY_NM") = LMI110C.EXCEL_COLTITLE
        dr("KEY_VALUE") = goodsCdNik
        dr("ROW_NO") = selectRow.ToString()
        errDs.Tables(LMI110C.TABLE_NM_GUIERROR).Rows.Add(dr)

        Return errDs

    End Function

#End Region

#End Region

#End Region

End Class
