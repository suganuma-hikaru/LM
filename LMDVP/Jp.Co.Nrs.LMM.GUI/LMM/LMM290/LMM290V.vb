' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM290V : 振替対象商品マスタメンテ
'  作  成  者       :  [kishi]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win

''' <summary>
''' LMM290Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM290V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM290F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMMControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMMControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM290F, ByVal v As LMMControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = v

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSaveInputChk() As Boolean

        'trim
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        Dim rtnResult As Boolean = Me.IsSaveSingleCheck()

        '商品KEYの入力(存在)チェック
        rtnResult = rtnResult AndAlso Me.IsInputChk()

        'マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsMstExistChk()

        '関連チェック
        rtnResult = rtnResult AndAlso Me.IsInputConnectionChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 商品KEY入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsInputChk() As Boolean

        With Me._Frm

            If String.IsNullOrEmpty(.lblCdNrs.TextValue) = True Then
                MyBase.ShowMessage("E340", New String() {"振替元の商品KEY"})
                Call Me._Vcon.SetErrorControl(.lblCdNrs)
                Call Me._Vcon.SetErrorControl(.txtCdCust)
                Return False
            End If

            If String.IsNullOrEmpty(.lblCdNrsTo.TextValue) = True Then
                MyBase.ShowMessage("E340", New String() {"振替先の商品KEY"})
                Call Me._Vcon.SetErrorControl(.lblCdNrsTo)
                Call Me._Vcon.SetErrorControl(.txtCdCustTo)
                Return False
            End If

        End With

        Return True

    End Function
    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputConnectionChk() As Boolean

        With Me._Frm

            '同一チェック(荷主コード)
            Dim rtnResult As Boolean = Me.IsSameCustCdLChk()

            '同一チェック(入目)
            rtnResult = rtnResult AndAlso Me.IsDifferentChk(.lblIrime, .lblIrimeTo)

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 荷主コード(大)同一チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSameCustCdLChk() As Boolean

        With Me._Frm

            '荷主コード(大)
            Dim rtnResult As Boolean = Me.HikakuChk(.txtCustCdL, .txtCustCdLTo)

            '商品マスタ照会で選択された荷主コード(大)
            '両方空だったらチェックしない
            If String.IsNullOrEmpty(.lblGoodsCustL.TextValue) = False _
               AndAlso String.IsNullOrEmpty(.lblGoodsCustL.TextValue) = False Then
                rtnResult = rtnResult AndAlso Me.HikakuChk(.lblGoodsCustL, .lblGoodsCustLTo)
            End If

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 同一チェック(同じ場合エラー)
    ''' </summary>
    ''' <param name="ctl">振替元</param>
    ''' <param name="ctlTo">振替先</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function HikakuChk(ByVal ctl As Win.InputMan.LMImTextBox, ByVal ctlTo As Win.InputMan.LMImTextBox) As Boolean

        With Me._Frm

            If ctl.TextValue.Equals(ctlTo.TextValue) = True Then
                Me._Vcon.SetErrorControl(.txtCustCdL)
                Me._Vcon.SetErrorControl(.txtCustCdLTo)
                MyBase.ShowMessage("E291", New String() {LMM290C.MSGL, LMM290C.MSGLTO, "荷主コード(大)"})
                Return False
            End If

        End With
        

        Return True

    End Function

    ''' <summary>
    ''' 同一チェック(異なる場合エラー)
    ''' </summary>
    ''' <param name="ctl">振替元</param>
    ''' <param name="ctlTo">振替先</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsDifferentChk(ByVal ctl As Win.InputMan.LMImTextBox, ByVal ctlTo As Win.InputMan.LMImTextBox) As Boolean

        If ctl.TextValue.Equals(ctlTo.TextValue) = False Then
            Call Me._Vcon.SetErrorControl(ctl)
            Call Me._Vcon.SetErrorControl(ctlTo)
            MyBase.ShowMessage("E217", New String() {LMM290C.MSGIRIME, LMM290C.MSGIRIMETO})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMstExistChk() As Boolean

        With Me._Frm

            '荷主マスタ存在チェック
            Dim rtnresult As Boolean = Me.IsCustExistChk(.txtCustCdL, .txtCustCdM, .lblCustCdS, .lblCustCdSS _
                                                         , .lblCustNmL, .lblCustNmM, .lblCustNmS, .lblCustNmSS)

            rtnresult = rtnresult AndAlso Me.IsCustExistChk(.txtCustCdLTo, .txtCustCdMTo, .lblCustCdSTo, .lblCustCdSSTo _
                                                            , .lblCustNmLTo, .lblCustNmMTo, .lblCustNmSTo, .lblCustNmSSTo)

            '商品マスタ存在チェック
            rtnresult = rtnresult AndAlso Me.IsGoodsExistCheck(.txtCdCust, .lblCdNrs, .txtCustCdL, .txtCustCdM, .lblNm1 _
                                                               , .lblPkg, .lblIrime)

            rtnresult = rtnresult AndAlso Me.IsGoodsExistCheck(.txtCdCustTo, .lblCdNrsTo, .txtCustCdLTo, .txtCustCdMTo, .lblNm1To _
                                                            , .lblPkgTo, .lblIrimeTo)


            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 荷主マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsCustExistChk(ByVal custCdL As Win.InputMan.LMImTextBox _
                                    , ByVal custCdM As Win.InputMan.LMImTextBox _
                                    , ByVal custCdS As Win.InputMan.LMImTextBox _
                                    , ByVal custCdSS As Win.InputMan.LMImTextBox _
                                    , ByVal custNmL As Win.InputMan.LMImTextBox _
                                    , ByVal custNmM As Win.InputMan.LMImTextBox _
                                    , ByVal custNmS As Win.InputMan.LMImTextBox _
                                    , ByVal custNmSS As Win.InputMan.LMImTextBox _
                                    ) As Boolean

        With Me._Frm

            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectCustListDataRow(drs, custCdL.TextValue, custCdM.TextValue, custCdS.TextValue, custCdSS.TextValue, LMMControlC.CustMsgType.CUST_SS) = False Then
                custNmL.TextValue = String.Empty
                custNmM.TextValue = String.Empty
                custNmS.TextValue = String.Empty
                custNmSS.TextValue = String.Empty
                Call Me._Vcon.SetErrorControl(custCdL)
                Call Me._Vcon.SetErrorControl(custCdM)
                Return False
            End If

            '名称を設定

            custNmL.TextValue = drs(0).Item("CUST_NM_L").ToString()
            custNmM.TextValue = drs(0).Item("CUST_NM_M").ToString()
            custNmS.TextValue = drs(0).Item("CUST_NM_S").ToString()
            custNmSS.TextValue = drs(0).Item("CUST_NM_SS").ToString()

            Return True

        End With

    End Function

    ''' <summary>
    ''' 商品マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsGoodsExistCheck(ByVal goodsCd As Win.InputMan.LMImTextBox _
                                       , ByVal goodsKey As Win.InputMan.LMImTextBox _
                                       , ByVal custCdL As Win.InputMan.LMImTextBox _
                                       , ByVal custCdM As Win.InputMan.LMImTextBox _
                                       , ByVal goodsNm As Win.InputMan.LMImTextBox _
                                       , ByVal pkg As Win.InputMan.LMImTextBox _
                                       , ByVal irime As Win.InputMan.LMImTextBox _
                                       ) As Boolean

        With Me._Frm

            Dim drs As DataRow() = Nothing

            drs = Me._Vcon.SelectgoodsListDataGoodCdRow(.cmbNrsBrCd.SelectedValue.ToString() _
                                                        , goodsCd.TextValue _
                                                        , goodsKey.TextValue _
                                                        , custCdL.TextValue _
                                                        , custCdM.TextValue)

            If drs.Length < 1 Then
                MyBase.ShowMessage("E079", New String() {"商品マスタ", goodsCd.TextValue})
                Call Me.goodsCtlErrSet(goodsCd, goodsKey, goodsNm, pkg, irime)
                Return False
            End If
            ''2件以上
            'If drs.Length > 1 Then
            '    MyBase.ShowMessage("E206", New String() {goodsCd.TextValue, "商品KEY"})
            '    Call Me.goodsCtlErrSet(goodsCd, goodsKey, goodsNm, pkg, irime)
            '    Return False
            'End If

            goodsNm.TextValue = drs(0).Item("GOODS_NM_1").ToString()
            goodsKey.TextValue = drs(0).Item("GOODS_CD_NRS").ToString()
            pkg.TextValue = drs(0).Item("PKG_UT_NM").ToString()
            irime.TextValue = String.Concat((drs(0).Item("STD_IRIME_NB").ToString), " ", (drs(0).Item("STD_IRIME_UT_NM").ToString))

        End With

        Return True


    End Function

    ''' <summary>
    ''' 商品マスタ存在チェックエラー時コントロール設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub goodsCtlErrSet(ByVal goodsCd As Win.InputMan.LMImTextBox _
               , ByVal goodsKey As Win.InputMan.LMImTextBox _
               , ByVal goodsNm As Win.InputMan.LMImTextBox _
               , ByVal pkg As Win.InputMan.LMImTextBox _
               , ByVal irime As Win.InputMan.LMImTextBox)

        With Me._Frm

            goodsNm.TextValue = String.Empty
            goodsKey.TextValue = String.Empty
            pkg.TextValue = String.Empty
            irime.TextValue = String.Empty
            Call Me._Vcon.SetErrorControl(goodsKey)
            Call Me._Vcon.SetErrorControl(goodsCd)

        End With

    End Sub

    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRecordStatusChk(ByVal frm As LMM290F) As Boolean

        If frm.lblSituation.RecordStatus = RecordStatus.DELETE_REC Then
            MyBase.ShowMessage("E035")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 他営業所チェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMM290F, ByVal eventShubetsu As LMM290C.EventShubetsu) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        'ユーザーのログイン営業所と異なる場合エラー
        'If frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
        '    Dim msg As String = String.Empty

        '    Select Case eventShubetsu

        '        Case LMM290C.EventShubetsu.HUKUSHA
        '            msg = "複写"

        '        Case LMM290C.EventShubetsu.SAKUJO
        '            msg = "削除・復活"

        '    End Select

        '    MyBase.ShowMessage("E178", New String() {msg})
        '    Return False

        'End If

        Return True

    End Function

    ''' <summary>
    ''' 保存時のtrim
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm

            .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
            .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()
            .txtCdCust.TextValue = .txtCdCust.TextValue.Trim()
            .lblCdNrs.TextValue = .lblCdNrs.TextValue.Trim()

            .txtCustCdLTo.TextValue = .txtCustCdLTo.TextValue.Trim()
            .txtCustCdMTo.TextValue = .txtCustCdMTo.TextValue.Trim()
            .txtCdCustTo.TextValue = .txtCdCustTo.TextValue.Trim()
            .lblCdNrsTo.TextValue = .lblCdNrsTo.TextValue.Trim()

        End With

    End Sub

    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSingleCheck() As Boolean

        With Me._Frm
            '**********編集部のチェック

            '営業所
            .cmbNrsBrCd.Name = "営業所"
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If

            '荷主コード(大)(振替元)
            .txtCustCdL.ItemName = "荷主コード(大)(振替元)"
            .txtCustCdL.IsHissuCheck = True
            .txtCustCdL.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                Return False
            End If

            '荷主コード(中)(振替元)
            .txtCustCdM.ItemName = "荷主コード(中)(振替元)"
            .txtCustCdM.IsHissuCheck = True
            .txtCustCdM.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                Return False
            End If

            '商品コード(振替元)
            .txtCdCust.ItemName = "商品コード(振替元)"
            .txtCdCust.IsForbiddenWordsCheck = True
            .txtCdCust.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtCdCust) = False Then
                Return False
            End If


            '荷主コード(大)(振替先)
            .txtCustCdLTo.ItemName = "荷主コード(大)(振替先)"
            .txtCustCdLTo.IsHissuCheck = True
            .txtCustCdLTo.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(.txtCustCdLTo) = False Then
                Return False
            End If

            '荷主コード(中)(振替先)
            .txtCustCdMTo.ItemName = "荷主コード(中)(振替先)"
            .txtCustCdMTo.IsHissuCheck = True
            .txtCustCdMTo.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(.txtCustCdMTo) = False Then
                Return False
            End If

            '商品コード(振替先)
            .txtCdCustTo.ItemName = "商品コード(振替先)"
            .txtCdCustTo.IsForbiddenWordsCheck = True
            .txtCdCustTo.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtCdCustTo) = False Then
                Return False
            End If

        End With

        Return True

    End Function


    ''' <summary>
    ''' 検索押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuInputChk() As Boolean

        'Trimチェック
        '検索
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0, Me._Frm.sprDetail.ActiveSheet.Columns.Count - 1)

        '単項目チェック
        If Me.IsKensakuSingleCheck() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 検索押下時スプレッド単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleCheck() As Boolean

        With Me._Frm

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            '荷主コード（大）
            vCell.SetValidateCell(0, LMM290G.sprDetailDef.CUST_CD_L.ColNo)
            vCell.ItemName = "荷主コード(大)"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 5
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '荷主名（大）
            vCell.SetValidateCell(0, LMM290G.sprDetailDef.CUST_NM_L.ColNo)
            vCell.ItemName = "荷主名(大)"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '商品コード
            vCell.SetValidateCell(0, LMM290G.sprDetailDef.GOODS_CD_CUST.ColNo)
            vCell.ItemName = "商品コード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            'START YANAI 要望番号886
            'vCell.IsHankakuCheck = True
            'END YANAI 要望番号886
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '商品名
            vCell.SetValidateCell(0, LMM290G.sprDetailDef.GOODS_NM_1.ColNo)
            vCell.ItemName = "商品名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '振替先荷主コード（大）
            vCell.SetValidateCell(0, LMM290G.sprDetailDef.CUST_CD_L_TO.ColNo)
            vCell.ItemName = "振替先荷主コード(大)"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 5
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '振替先荷主名（大）
            vCell.SetValidateCell(0, LMM290G.sprDetailDef.CUST_NM_L_TO.ColNo)
            vCell.ItemName = "振替先荷主名(大)"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '振替先商品コード
            vCell.SetValidateCell(0, LMM290G.sprDetailDef.GOODS_CD_CUST_TO.ColNo)
            vCell.ItemName = "振替先商品コード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            'START YANAI 要望番号886
            'vCell.IsHankakuCheck = True
            'END YANAI 要望番号886
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '振替先商品名
            vCell.SetValidateCell(0, LMM290G.sprDetailDef.GOODS_NM_1_TO.ColNo)
            vCell.ItemName = "振替先商品名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM290C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM290C.EventShubetsu.SHINKI           '新規
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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

            Case LMM290C.EventShubetsu.HENSHU          '編集
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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

            Case LMM290C.EventShubetsu.HUKUSHA          '複写
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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


            Case LMM290C.EventShubetsu.SAKUJO          '削除・復活
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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

            Case LMM290C.EventShubetsu.KENSAKU         '検索
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

            Case LMM290C.EventShubetsu.MASTEROPEN          'マスタ参照
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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

            Case LMM290C.EventShubetsu.HOZON           '保存
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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

            Case LMM290C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM290C.EventShubetsu.DCLICK         'ダブルクリック
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

            Case LMM290C.EventShubetsu.ENTER          'Enter
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
    ''' エラー項目の背景色とフォーカスを設定する
    ''' </summary>
    ''' <param name="ctl">エラーコントロール</param>
    ''' <remarks></remarks>
    Friend Sub SetErrorControl(ByVal ctl As Control)

        Dim errorColor As System.Drawing.Color = Utility.LMGUIUtility.GetAttentionBackColor()

        If TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMComboKubun = True Then

            DirectCast(ctl, Win.InputMan.LMComboKubun).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMImNumber = True Then

            DirectCast(ctl, Win.InputMan.LMImNumber).BackColorDef = errorColor

        End If

        ctl.Focus()
        ctl.Select()

    End Sub

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMM290C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If objNm Is Nothing = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM290C.EventShubetsu.MASTEROPEN) = True Then
                Call Me._Vcon.SetFocusErrMessage(False)
            End If
            Return False
        End If

        '判定するコントロール設定先変数
        Dim ctl As Win.InputMan.LMImTextBox() = Nothing
        Dim msg As String() = Nothing
        Dim clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl() = Nothing
        Dim focusCtl As Control = Me._Frm.ActiveControl

        With Me._Frm

            Select Case objNm



                Case .txtCustCdL.Name, .txtCustCdM.Name

                    Dim custNm As String = .lblTitleCust.Text
                    ctl = New Win.InputMan.LMImTextBox() {.txtCustCdL, .txtCustCdM}
                    msg = New String() {String.Concat("振替元", custNm, LMMControlC.CD, LMMControlC.L_NM) _
                                        , String.Concat("振替元", custNm, LMMControlC.CD, LMMControlC.M_NM)}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblCustCdS, .lblCustCdSS _
                                                                                 , .lblCustNmL, .lblCustNmM _
                                                                                 , .lblCustNmS, .lblCustNmSS}


                Case .txtCustCdLTo.Name, .txtCustCdMTo.Name

                    Dim custNm As String = .lblTitleCustTo.Text
                    ctl = New Win.InputMan.LMImTextBox() {.txtCustCdLTo, .txtCustCdMTo}
                    msg = New String() {String.Concat("振替先", custNm, LMMControlC.CD, LMMControlC.L_NM) _
                                        , String.Concat("振替先", custNm, LMMControlC.CD, LMMControlC.M_NM)}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblCustCdSTo, .lblCustCdSSTo _
                                                                                 , .lblCustNmLTo, .lblCustNmMTo _
                                                                                 , .lblCustNmSTo, .lblCustNmSSTo}

                Case .txtCdCust.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtCdCust}
                    msg = New String() {String.Concat(.lblTitleGoodsCdCust.Text)}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblCdNrs, .lblNm1, .lblGoodsCustL _
                                                                                 , .lblPkg, .lblIrime}

                Case .txtCdCustTo.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtCdCustTo}
                    msg = New String() {String.Concat(.lblTitleGoodsCdCustTo.Text)}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblCdNrsTo, .lblNm1To, .lblGoodsCustLTo _
                                                                                 , .lblPkgTo, .lblIrimeTo}

            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), ctl, msg, focusCtl, clearCtl)

        End With

    End Function

#End Region 'Method

End Class
