' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM160V : 届先商品マスタメンテナンス
'  作  成  者       :  [金へスル]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win

''' <summary>
''' LMM160Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM160V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM160F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMMControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM160F, ByVal v As LMMControlV)

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
        If Me.IsSaveSingleCheck() = False Then
            Return False
        End If

        '存在チェック
        If Me.IsSaveExistCheck() = False Then
            Return False
        End If

        '納品書表示名入力チェック(空ならば商品名を入力)
        Call Me.DestGoodsNmChk()

        Return True

    End Function

    ''' <summary>
    ''' スペース除去(編集部)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm

            .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
            .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()
            .txtDestCd.TextValue = .txtDestCd.TextValue.Trim()
            .txtGoodsCd.TextValue = .txtGoodsCd.TextValue.Trim()
            .txtWorkSeiqCd.TextValue = .txtWorkSeiqCd.TextValue.Trim()
            .txtWork1Kb.TextValue = .txtWork1Kb.TextValue.Trim()
            .txtWork2Kb.TextValue = .txtWork2Kb.TextValue.Trim()

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
            .cmbNrsBrCd.ItemName = "営業所"
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If
            '荷主コード (大)
            .txtCustCdL.ItemName = "荷主コード (大)"
            .txtCustCdL.IsHissuCheck = True
            .txtCustCdL.IsFullByteCheck = 5
            .txtCustCdL.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                Return False
            End If
            '荷主コード (中)
            .txtCustCdM.ItemName = "荷主コード (中)"
            .txtCustCdM.IsHissuCheck = True
            .txtCustCdM.IsFullByteCheck = 2
            .txtCustCdM.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                Return False
            End If
            '届先コード
            .txtDestCd.ItemName = "届先コード"
            .txtDestCd.IsHissuCheck = True
            .txtDestCd.IsForbiddenWordsCheck = True
            .txtDestCd.IsByteCheck = 15            
            If MyBase.IsValidateCheck(.txtDestCd) = False Then
                Return False
            End If

            '商品コード
            .txtGoodsCd.ItemName = "商品コード"
            .txtGoodsCd.IsHissuCheck = True
            .txtGoodsCd.IsForbiddenWordsCheck = True
            .txtGoodsCd.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtGoodsCd) = False Then
                Return False
            End If

            '納品書表示商品名
            .txtDelverGoodsNm.ItemName = "納品書表示商品名"
            .txtDelverGoodsNm.IsForbiddenWordsCheck = True            
            .txtDelverGoodsNm.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtDelverGoodsNm) = False Then
                Return False
            End If
            '作業料請求先コード（2011.08.30 検証結果一覧№53対応）
            .txtWorkSeiqCd.ItemName = "作業料請求先コード"
            .txtWorkSeiqCd.IsForbiddenWordsCheck = True
            .txtWorkSeiqCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtWorkSeiqCd) = False Then
                Return False
            End If
            '出荷時作業1
            .txtWork1Kb.ItemName = "出荷時作業1"
            .txtWork1Kb.IsForbiddenWordsCheck = True
            .txtWork1Kb.IsFullByteCheck = 5
            .txtWork1Kb.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtWork1Kb) = False Then
                Return False
            End If
            '出荷時作業2
            .txtWork2Kb.ItemName = "出荷時作業2"
            .txtWork2Kb.IsForbiddenWordsCheck = True
            .txtWork2Kb.IsFullByteCheck = 5
            .txtWork2Kb.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtWork2Kb) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveExistCheck() As Boolean

        With Me._Frm

            Dim custl As String = .txtCustCdL.TextValue
            Dim custm As String = .txtCustCdM.TextValue
            Dim defaultkb As String = "00"

            '荷主(大)、荷主(中)の関連チェック
            If Me.IsCustChk(custl, custm, defaultkb) = False Then
                Return False
            End If

            Dim drs As DataRow() = Nothing

            Dim brcd As String = .cmbNrsBrCd.SelectedValue.ToString()
            Dim destCd As String = .txtDestCd.TextValue
            '届先マスタ存在チェック
            drs = Me._Vcon.SelectDestListDataRow(brcd, custl, destCd)
            If drs.Length < 1 Then
                MyBase.ShowMessage("E079", New String() {"届先マスタ", destCd})
                .lblDestNm.TextValue = String.Empty
                Call Me.SetErrorControl(.txtDestCd)
                Return False
            End If

            'マスタの値を設定
            .txtDestCd.TextValue = drs(0).Item("DEST_CD").ToString()
            .lblDestNm.TextValue = drs(0).Item("DEST_NM").ToString()

            Dim goodsKey As String = .lblGoodsNrs.TextValue
            Dim goods As String = .txtGoodsCd.TextValue

            '商品マスタ存在チェック
            drs = Me._Vcon.SelectgoodsListDataGoodCdRow(brcd, goods, goodsKey, custl, custm)
            '0件
            If drs.Length < 1 Then
                MyBase.ShowMessage("E079", New String() {"商品マスタ", goods})
                Call Me.goodsCtlErrSet()
                Return False
            End If
            ''2件以上
            'If drs.Length > 1 Then
            '    MyBase.ShowMessage("E206", New String() {goods, "商品KEY"})
            '    Call Me.goodsCtlErrSet()
            '    Return False
            'End If

            'マスタの値を設定
            .txtGoodsCd.TextValue = drs(0).Item("GOODS_CD_CUST").ToString()
            .lblGoodsNm.TextValue = drs(0).Item("GOODS_NM_1").ToString()
            .lblGoodsNrs.TextValue = drs(0).Item("GOODS_CD_NRS").ToString()

            Dim workSeiq As String = .txtWorkSeiqCd.TextValue
            '入力あるときのみチェック
            If String.IsNullOrEmpty(workSeiq) = False Then
                '請求先マスタ存在チェック
                drs = Me._Vcon.SelectSeiqtoListDataRow(brcd, workSeiq)
                If drs.Length < 1 Then
                    MyBase.ShowMessage("E079", New String() {"請求先マスタ", workSeiq})
                    .lblWorkSeiqNm.TextValue = String.Empty
                    Me.SetErrorControl(.txtWorkSeiqCd)
                    Return False
                End If
                'マスタの値を設定
                .txtWorkSeiqCd.TextValue = drs(0).Item("SEIQTO_CD").ToString()
                Dim workSeiqto As String = Me._Vcon.EditConcatData(drs(0).Item("SEIQTO_NM").ToString(), drs(0).Item("SEIQTO_BUSYO_NM").ToString(), " ")
                .lblWorkSeiqNm.TextValue = workSeiqto
            End If


            Dim work1 As String = .txtWork1Kb.TextValue
            '入力あるときのみチェック
            If String.IsNullOrEmpty(work1) = False Then
                '作業項目マスタ存在チェック
                'START YANAI 要望番号376
                'drs = Me._Vcon.SelectSagyoListDataRow(work1, custl, brcd)
                Dim SelectSagyoString As String = String.Empty
                '削除フラグ
                SelectSagyoString = String.Concat(SelectSagyoString, " SYS_DEL_FLG = '0' ")
                '作業コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND SAGYO_CD = '", work1, "' ")
                '営業所コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND NRS_BR_CD = '", brcd, "' ")
                '荷主コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND (CUST_CD_L = '", custl, "' OR CUST_CD_L = 'ZZZZZ')")

                drs = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(SelectSagyoString)
                'END YANAI 要望番号376
                If drs.Length < 1 Then
                    MyBase.ShowMessage("E079", New String() {"作業項目マスタ", work1})
                    .lblWork1Nm.TextValue = String.Empty
                    Me.SetErrorControl(.txtWork1Kb)
                    Return False
                End If

                'マスタの値を設定
                .txtWork1Kb.TextValue = drs(0).Item("SAGYO_CD").ToString()
                .lblWork1Nm.TextValue = drs(0).Item("SAGYO_NM").ToString()

            End If


            Dim work2 As String = .txtWork2Kb.TextValue
            '入力あるときのみチェック
            If String.IsNullOrEmpty(work2) = False Then
                '作業項目マスタ存在チェック()
                'START YANAI 要望番号376
                'drs = Me._Vcon.SelectSagyoListDataRow(work2, custl, brcd)
                Dim SelectSagyoString As String = String.Empty
                '削除フラグ
                SelectSagyoString = String.Concat(SelectSagyoString, " SYS_DEL_FLG = '0' ")
                '作業コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND SAGYO_CD = '", work2, "' ")
                '営業所コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND NRS_BR_CD = '", brcd, "' ")
                '荷主コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND (CUST_CD_L = '", custl, "' OR CUST_CD_L = 'ZZZZZ')")

                drs = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(SelectSagyoString)
                'END YANAI 要望番号376
                If drs.Length < 1 Then
                    MyBase.ShowMessage("E079", New String() {"作業項目マスタ", work2})
                    .lblWork2Nm.TextValue = String.Empty
                    Me.SetErrorControl(.txtWork2Kb)
                    Return False
                End If

                'マスタの値を設定
                .txtWork2Kb.TextValue = drs(0).Item("SAGYO_CD").ToString()
                .lblWork2Nm.TextValue = drs(0).Item("SAGYO_NM").ToString()

            End If

            '重複チェック(出荷時作業1＝出荷時作業2のときエラー)
            Dim sagyo1 As String = Me._Frm.txtWork1Kb.TextValue.ToString()
            Dim sagyo2 As String = Me._Frm.txtWork2Kb.TextValue.ToString()

            '入力あるときのみチェック ----検証結果(メモ)№124対応(2011.09.13)---
            If String.IsNullOrEmpty(sagyo1) = True _
             AndAlso String.IsNullOrEmpty(sagyo2) = True Then
                'チェックスル－
            Else
                If sagyo1 = sagyo2 Then
                    MyBase.ShowMessage("E291", New String() {"作業1", "作業2", "どちらか"})
                    Call Me._Vcon.SetErrorControl(Me._Frm.txtWork1Kb)
                    Call Me._Vcon.SetErrorControl(Me._Frm.txtWork2Kb)
                    Return False
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 商品マスタ存在チェックエラー品名・商品KEY設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub goodsCtlErrSet()

        With Me._Frm

            .lblGoodsNm.TextValue = String.Empty
            .lblGoodsNrs.TextValue = String.Empty
            Call Me.SetErrorControl(.lblGoodsNrs)
            Call Me.SetErrorControl(.txtGoodsCd)

        End With

    End Sub

    ''' <summary>
    ''' 納品書表示商品名入力
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DestGoodsNmChk()

        With Me._Frm

            '入力が空のときのみ商品マスタの商品名1を入力
            If String.IsNullOrEmpty(.txtDelverGoodsNm.TextValue) = True Then
                Dim goodsNm As String = .lblGoodsNm.TextValue
                .txtDelverGoodsNm.TextValue = goodsNm
            End If

        End With

    End Sub


    ''' <summary>
    ''' 荷主存在チェック
    ''' </summary>
    ''' <param name="custL"></param>
    ''' <param name="custM"></param>
    ''' <param name="defaultkb"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsCustChk(ByVal custL As String, ByVal custM As String, ByVal defaultkb As String) As Boolean

        With Me._Frm

            '荷主(大)、荷主(中)に入力されていない場合
            If String.IsNullOrEmpty(custL) = True _
                AndAlso String.IsNullOrEmpty(custM) = True Then
                Return True
            End If

            '荷主(大)、荷主(中)に入力されていない場合
            If String.IsNullOrEmpty(custL) = False _
                AndAlso String.IsNullOrEmpty(custM) = False Then
                Return Me.IsCustMChk(custL, custM, defaultkb)
            End If

            '荷主コード(中)が存在しない場合はエラーを戻す
            MyBase.ShowMessage("E017", New String() {"荷主コード(大)", "荷主コード(中)"})
            Me.SetErrorControl(.txtCustCdM)
            Me.SetErrorControl(.txtCustCdL)
            Return False

        End With

    End Function

    ''' <summary>
    ''' 荷主マスタ存在チェック
    ''' </summary>
    ''' <param name="custL"></param>
    ''' <param name="custM"></param>
    ''' <param name="defaultkb"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsCustMChk(ByVal custL As String, ByVal custM As String, ByVal defaultkb As String) As Boolean

        With Me._Frm

            Dim drs As DataRow() = Me._Vcon.SelectCustListDataRow(custL, custM, defaultkb, defaultkb)

            If drs.Length < 1 Then
                MyBase.ShowMessage("E079", New String() {"荷主マスタ", String.Concat(custL, " - ", custM)})
                .lblCustNmL.TextValue = String.Empty
                .lblCustNmM.TextValue = String.Empty
                Me.SetErrorControl(.txtCustCdM)
                Me.SetErrorControl(.txtCustCdL)
                Return False
            End If

            .lblCustNmL.TextValue = drs(0).Item("CUST_NM_L").ToString()
            .lblCustNmM.TextValue = drs(0).Item("CUST_NM_M").ToString()

        End With

        Return True

    End Function

    ''' <summary>
    ''' データ存在チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsExistDataChk(ByVal frm As LMM160F) As Boolean

        If String.IsNullOrEmpty(frm.txtCustCdL.TextValue) = True Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRecordStatusChk(ByVal frm As LMM160F) As Boolean

        If RecordStatus.DELETE_REC.Equals(frm.lblSituation.RecordStatus) = True Then

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
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMM160F, ByVal eventShubetsu As LMM160C.EventShubetsu) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        'ユーザーのログイン営業所と異なる場合エラー
        'If frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
        '    Dim msg As String = String.Empty

        '    Select Case eventShubetsu

        '        Case LMM160C.EventShubetsu.HENSHU
        '            msg = "編集"

        '        Case LMM160C.EventShubetsu.HUKUSHA
        '            msg = "複写"

        '        Case LMM160C.EventShubetsu.SAKUJO
        '            msg = "削除・復活"

        '    End Select

        '    MyBase.ShowMessage("E178", New String() {msg})
        '    Return False

        'End If

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
            vCell.SetValidateCell(0, LMM160G.sprDetailDef.CUST_CD_L.ColNo)
            vCell.ItemName = "荷主コード（大）"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 5
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主コード（中）
            vCell.SetValidateCell(0, LMM160G.sprDetailDef.CUST_CD_M.ColNo)
            vCell.ItemName = "荷主コード（中）"
            vCell.IsForbiddenWordsCheck = True            
            vCell.IsByteCheck = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名（大）
            vCell.SetValidateCell(0, LMM160G.sprDetailDef.CUST_NM_L.ColNo)
            vCell.ItemName = "荷主名（大）"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名（中）
            vCell.SetValidateCell(0, LMM160G.sprDetailDef.CUST_NM_M.ColNo)
            vCell.ItemName = "荷主名（中）"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '届先コード
            vCell.SetValidateCell(0, LMM160G.sprDetailDef.CD.ColNo)
            vCell.ItemName = "届先コード"
            vCell.IsForbiddenWordsCheck = True            
            vCell.IsByteCheck = 15
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '届先名
            vCell.SetValidateCell(0, LMM160G.sprDetailDef.DEST_NM.ColNo)
            vCell.ItemName = "届先名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 80
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '商品コード
            vCell.SetValidateCell(0, LMM160G.sprDetailDef.GOODS_CD.ColNo)
            vCell.ItemName = "商品コード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20            
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '納品先表示商品名
            vCell.SetValidateCell(0, LMM160G.sprDetailDef.GOODS_NM.ColNo)
            vCell.ItemName = "納品書表示商品名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '出荷時作業1
            vCell.SetValidateCell(0, LMM160G.sprDetailDef.SAGYO_KB_1.ColNo)
            vCell.ItemName = "出荷時作業1"
            vCell.IsForbiddenWordsCheck = True            
            vCell.IsByteCheck = 5
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '出荷時作業2
            vCell.SetValidateCell(0, LMM160G.sprDetailDef.SAGYO_KB_2.ColNo)
            vCell.ItemName = "出荷時作業2"
            vCell.IsForbiddenWordsCheck = True            
            vCell.IsByteCheck = 5
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '作業料請求先コード
            vCell.SetValidateCell(0, LMM160G.sprDetailDef.SAGYO_SEIQTO_CD.ColNo)
            vCell.ItemName = "作業料請求先コード"
            vCell.IsForbiddenWordsCheck = True            
            vCell.IsByteCheck = 7
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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM160C.EventShubetsu) As Boolean
        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM160C.EventShubetsu.SHINKI           '新規
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

            Case LMM160C.EventShubetsu.HENSHU          '編集
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

            Case LMM160C.EventShubetsu.HUKUSHA          '複写
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


            Case LMM160C.EventShubetsu.SAKUJO          '削除・復活
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

            Case LMM160C.EventShubetsu.KENSAKU         '検索
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

            Case LMM160C.EventShubetsu.MASTEROPEN          'マスタ参照
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

            Case LMM160C.EventShubetsu.HOZON           '保存
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

            Case LMM160C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM160C.EventShubetsu.DCLICK         'ダブルクリック
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

            Case LMM160C.EventShubetsu.ENTER          'Enter
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
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMM160C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If objNm Is Nothing = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM160C.EventShubetsu.MASTEROPEN) = True Then
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

                    Dim custNm As String = .lblTitleCustCd.Text
                    ctl = New Win.InputMan.LMImTextBox() {.txtCustCdL, .txtCustCdM}
                    msg = New String() {String.Concat(custNm, LMMControlC.L_NM, LMMControlC.CD) _
                                        , String.Concat(custNm, LMMControlC.M_NM, LMMControlC.CD)}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblCustNmL, .lblCustNmM}

                Case .txtDestCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtDestCd}
                    msg = New String() {String.Concat(.lblTitleDest.Text, LMMControlC.CD)}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblDestNm}

                Case .txtWorkSeiqCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtWorkSeiqCd}
                    msg = New String() {String.Concat(.lblTitleWorkSeiq.Text, LMMControlC.CD)}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblWorkSeiqNm}

                Case .txtGoodsCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtGoodsCd}
                    msg = New String() {String.Concat(.lblTitleGoods.Text, LMMControlC.CD)}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblGoodsNm, .lblGoodsNrs}

                Case .txtWork1Kb.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtWork1Kb}
                    msg = New String() {String.Concat("作業", LMMControlC.CD, "1")}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblWork1Nm}

                Case .txtWork2Kb.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtWork2Kb}
                    msg = New String() {String.Concat("作業", LMMControlC.CD, "2")}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblWork2Nm}


            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), ctl, msg, focusCtl, clearCtl)

        End With

    End Function

#Region "部品化検討中"

    ''' <summary>
    ''' エラー項目の背景色とフォーカスを設定する
    ''' </summary>
    ''' <param name="ctl">エラーコントロール</param>
    ''' <remarks></remarks>
    Friend Sub SetErrorControl(ByVal ctl As Control)

        Dim errorColor As System.Drawing.Color = Utility.LMGUIUtility.GetAttentionBackColor

        If TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).BackColorDef = errorColor

        End If

        ctl.Focus()
        ctl.Select()

    End Sub

    ''' <summary>
    ''' セルから値を取得
    ''' </summary>
    ''' <param name="aCell">セル</param>
    ''' <returns>取得した値</returns>
    ''' <remarks></remarks>
    Friend Function GetCellValue(ByVal aCell As Cell) As String

        GetCellValue = String.Empty

        If TypeOf aCell.Editor Is CellType.ComboBoxCellType Then

            'コンボボックスの場合、Value値を返却
            If aCell.Value Is Nothing = False Then
                GetCellValue = aCell.Value.ToString()
            End If

        ElseIf TypeOf aCell.Editor Is CellType.CheckBoxCellType Then

            'チェックボックスの場合、Booleanの値をStringに変換
            If aCell.Text.Equals("True") = True Then
                GetCellValue = LMConst.FLG.ON
            ElseIf aCell.Text.Equals("False") = True Then
                GetCellValue = LMConst.FLG.OFF
            Else
                GetCellValue = aCell.Text
            End If

        ElseIf TypeOf aCell.Editor Is CellType.NumberCellType Then

            'ナンバーの場合、Value値を返却
            If aCell.Value Is Nothing = False Then
                GetCellValue = aCell.Value.ToString()
            Else
                GetCellValue = 0.ToString()
            End If

        Else

            'テキストの場合、Trimした値を返却
            GetCellValue = aCell.Text.Trim()

        End If

        Return GetCellValue

    End Function

#End Region

#End Region 'Method

End Class
