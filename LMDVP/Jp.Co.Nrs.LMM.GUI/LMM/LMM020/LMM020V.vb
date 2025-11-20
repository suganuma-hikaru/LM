' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM020V : 請求先マスタメンテ
'  作  成  者       :  平山
' ==========================================================================
Imports System.IO
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMM020Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM020V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM020F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM020F, ByVal v As LMMControlV)

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
    ''' <param name="svPath">ファイルサーバーパス名</param>
    ''' <param name="coaLink">画面のパス名</param>
    ''' <param name="coaNm">画面のファイル名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsSaveInputChk(ByVal svPath As String _
                                         , ByVal coaLink As String _
                                         , ByVal coaNm As String _
                                               ) As Boolean

        'trim
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        Dim rtnResult As Boolean = Me.IsSaveSingleCheck()

        '関連チェック
        rtnResult = rtnResult AndAlso Me.IsSaveConsistencyCheck()    'ADD 2018/11/14 要望番号001939

        'マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsMstExistChk()

        '========ファイル関連チェック========

        '画面で分析票が選択されていないときスルー
        If String.IsNullOrEmpty(coaLink) = False Then

            '区分マスタ存在チェック(ファイルサーバーパス)
            rtnResult = rtnResult AndAlso Me.IsKbnDataCheck(svPath)

            '区分マスタから取得したファイルサーバーパス存在チェック
            rtnResult = rtnResult AndAlso Me.IsFolderExistCheck(svPath)

            '画面で選択されているファイルの存在チェック
            rtnResult = rtnResult AndAlso Me.IsFileExistCheck(Path.Combine(coaLink, coaNm))

        End If

        '=====================================

        Return rtnResult

    End Function

#Region "単項目チェック(保存)"

    ''' <summary>
    ''' 保存時のtrim
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm

            .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
            .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()
            .txtGoodsCd.TextValue = .txtGoodsCd.TextValue.Trim()
            .lblGoodsNm.TextValue = .lblGoodsNm.TextValue.Trim()
            .lblGoodsKey.TextValue = .lblGoodsKey.TextValue.Trim()
            .txtDestCd.TextValue = .txtDestCd.TextValue.Trim()
            .lblCoaLink.TextValue = .lblCoaLink.TextValue.Trim()
            .lblCoaName.TextValue = .lblCoaName.TextValue.Trim()

        End With

    End Sub


    ''' <summary>
    ''' 単項目チェック(保存)
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

            '荷主コード(大)
            .txtCustCdL.ItemName = "荷主コード(大)"
            .txtCustCdL.IsHissuCheck = True
            .txtCustCdL.IsForbiddenWordsCheck = True
            .txtCustCdL.IsFullByteCheck = 5
            .txtCustCdL.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                Return False
            End If

            '荷主コード(中)
            .txtCustCdM.ItemName = "荷主コード(中)"
            .txtCustCdM.IsHissuCheck = True
            .txtCustCdM.IsForbiddenWordsCheck = True
            .txtCustCdM.IsFullByteCheck = 2
            .txtCustCdM.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtCustCdM) = False Then
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

            'ロット№
            .txtLotNo.ItemName = "ロット№"
            .txtLotNo.IsHissuCheck = True
            .txtLotNo.IsForbiddenWordsCheck = True
            .txtLotNo.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtLotNo) = False Then
                Return False
            End If

            '届先コード
            .txtDestCd.ItemName = "届先コード"
            .txtDestCd.IsHissuCheck = True
            .txtDestCd.IsForbiddenWordsCheck = True
            .txtDestCd.IsByteCheck = 15
            .txtDestCd.IsHankakuCheck = True
            If MyBase.IsValidateCheck(.txtDestCd) = False Then
                Return False
            End If

            'ADD START 2018/11/14 要望番号001939
            '入荷日
            If .imdInkaDate.IsDateFullByteCheck = False Then
                MyBase.ShowMessage("E038", New String() {.lblTitleInkaDate.Text, "8"})
                Call Me._Vcon.SetErrorControl(.imdInkaDate)
                Return False
            End If
            'ADD END   2018/11/14 要望番号001939

            '分析票ファイルパス
            .lblCoaLink.ItemName = "分析票ファイルパス"
            .lblCoaLink.IsForbiddenWordsCheck = True
            .lblCoaLink.IsByteCheck = 100
            If MyBase.IsValidateCheck(.lblCoaLink) = False Then
                Return False
            End If
            '分析票ファイル名
            .lblCoaName.ItemName = "分析票ファイル名"
            .lblCoaName.IsForbiddenWordsCheck = True
            .lblCoaName.IsByteCheck = 100
            If MyBase.IsValidateCheck(.lblCoaName) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#End Region

    'ADD START 2018/11/14 要望番号001939
#Region "関連チェック"
    ''' <summary>
    ''' 関連チェック(保存)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveConsistencyCheck() As Boolean

        With Me._Frm

            '新規または複写の場合
            If .lblSituation.RecordStatus = RecordStatus.NEW_REC OrElse .lblSituation.RecordStatus = RecordStatus.COPY_REC Then
                '入荷日が入力なし、かつ、汎用チェックボックスがチェックなしの場合
                If String.IsNullOrEmpty(.imdInkaDate.TextValue) AndAlso .chkInkaDateVersFlg.Checked = False Then
                    MyBase.ShowMessage("G014", New String() {.lblTitleInkaDate.Text})
                    Call Me._Vcon.SetErrorControl(.imdInkaDate)
                    Return False
                End If

                '入荷日が入力あり、かつ、汎用チェックボックスがチェックありの場合
                If Not String.IsNullOrEmpty(.imdInkaDate.TextValue) AndAlso .chkInkaDateVersFlg.Checked Then
                    MyBase.ShowMessage("G014", New String() {String.Concat(.lblTitleInkaDate.Text, "または", .chkInkaDateVersFlg.Text, "のどちらか一方のみ")})
                    Call Me._Vcon.SetErrorControl(.imdInkaDate)
                    Return False
                End If
            End If

        End With

        Return True

    End Function
#End Region
    'ADD END   2018/11/14 要望番号001939

#Region "マスタ存在チェック"


    ''' <summary>
    ''' マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMstExistChk() As Boolean

        '荷主マスタ存在チェック
        Dim rtnResult As Boolean = Me.IsCustExistChk()

        '商品マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsGoodsExistCheck()

        '届先マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsDestExistChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 荷主マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsCustExistChk() As Boolean

        With Me._Frm

            Dim custCdL As String = .txtCustCdL.TextValue
            Dim custCdM As String = .txtCustCdM.TextValue


            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectCustListDataRow(drs, custCdL, custCdM, LMMControlC.FLG_OFF, LMMControlC.FLG_OFF, LMMControlC.CustMsgType.CUST_M) = False Then
                .lblCustNmL.TextValue = String.Empty
                .lblCustNmM.TextValue = String.Empty
                Call Me._Vcon.SetErrorControl(.txtCustCdL)
                Call Me._Vcon.SetErrorControl(.txtCustCdM)
                Return False
            End If

            '名称を設定
            .lblCustNmL.TextValue = drs(0).Item("CUST_NM_L").ToString()
            .lblCustNmM.TextValue = drs(0).Item("CUST_NM_M").ToString()

            Return True

        End With

    End Function

    ''' <summary>
    ''' 商品マスタの存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsGoodsExistCheck() As Boolean

        With Me._Frm

            Dim brCd As String = .cmbNrsBrCd.SelectedValue.ToString()
            Dim goodCd As String = .txtGoodsCd.TextValue
            Dim goodKey As String = .lblGoodsKey.TextValue
            Dim custCdL As String = .txtCustCdL.TextValue
            Dim custCdM As String = .txtCustCdM.TextValue

            Dim drs As DataRow() = Nothing

            drs = Me._Vcon.SelectgoodsListDataGoodCdRow(brCd, goodCd, goodKey, custCdL, custCdM)

            '0件
            If drs.Length < 1 Then
                MyBase.ShowMessage("E079", New String() {"商品マスタ", goodCd})
                'エラー時コントロール設定
                Call Me.goodsCtlErrSet()
                Return False
            End If

            '2件以上
            If drs.Length > 1 Then
                MyBase.ShowMessage("E206", New String() {goodCd, "商品KEY"})
                'エラー時コントロール設定
                Call Me.goodsCtlErrSet()
                Return False
            End If

            .lblGoodsNm.TextValue = drs(0).Item("GOODS_NM_1").ToString()
            .lblGoodsKey.TextValue = drs(0).Item("GOODS_CD_NRS").ToString()

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
            .lblGoodsKey.TextValue = String.Empty
            Call Me._Vcon.SetErrorControl(.lblGoodsKey)
            Call Me._Vcon.SetErrorControl(.txtGoodsCd)

        End With

    End Sub

    ''' <summary>
    ''' 届先マスタの存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDestExistChk() As Boolean

        With Me._Frm

            Dim drs As DataRow() = Nothing
            Dim brCd As String = .cmbNrsBrCd.SelectedValue.ToString()
            Dim custCdL As String = .txtCustCdL.TextValue

            'START YANAI 要望番号376
            Dim destDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'A002' AND KBN_CD = '00' AND KBN_NM2 = '", .txtDestCd.TextValue, "'"))
            If 0 < destDr.Length Then
                Return True
            End If
            'END YANAI 要望番号376

            If Me._Vcon.SelectDestListDataRow(drs, brCd, custCdL, .txtDestCd.TextValue) = False Then
                .lblDestNm.TextValue = String.Empty
                Call Me._Vcon.SetErrorControl(.txtDestCd)
                Return False
            End If

            '名称を設定
            .lblDestNm.TextValue = drs(0).Item("DEST_NM").ToString()

        End With

        Return True

    End Function

#End Region

#Region "ファイル関連チェック"

    ''' <summary>
    ''' 区分マスタサーバーパス有無チェック
    ''' </summary>
    ''' <param name="svPath">区分マスタから取得したファイルサーバーパス名</param>
    ''' <returns>False：空だったら取得できていない</returns>
    ''' <remarks></remarks>
    Private Function IsKbnDataCheck(ByVal svPath As String) As Boolean

        If String.IsNullOrEmpty(svPath) = True Then
            MyBase.ShowMessage("E078", New String() {"区分マスタ"})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' フォルダ存在チェック
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function FolderExistChk(ByVal path As String) As Boolean

        Return Me.IsFolderExistCheck(path)

    End Function

    ''' <summary>
    ''' フォルダ有無チェック
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns>True</returns>
    ''' <remarks>フォルダがない場合、フォルダを作成する</remarks>
    Private Function IsFolderExistCheck(ByVal path As String) As Boolean

        If Directory.Exists(path) = False Then

            'ない場合、フォルダを作成
            Directory.CreateDirectory(path)

        End If

        Return True

    End Function

    ''' <summary>
    ''' ファイル存在チェック
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function FileExistChk(ByVal path As String) As Boolean

        Return Me.IsFileExistCheck(path)

    End Function

    ''' <summary>
    ''' ファイル存在チェック
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsFileExistCheck(ByVal path As String) As Boolean

        If File.Exists(path) = False Then
            MyBase.ShowMessage("E296", New String() {path})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 表示項目と隠し項目の差異チェック
    ''' </summary>
    ''' <param name="coaLink">表示ファイルパス名</param>
    ''' <param name="coaNm">表示ファイル名</param>
    ''' <param name="hidLink">隠しファイルパス名</param>
    ''' <param name="hidNm">隠しファイル名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsCoaHidChk(ByVal coaLink As String _
                                   , ByVal coaNm As String _
                                   , ByVal hidLink As String _
                                   , ByVal hidNm As String _
                                                   ) As Boolean

        Dim rtnResult As Boolean = Me.IsNullorEmptyChk(coaLink)

        rtnResult = rtnResult AndAlso Me.IsSameChk(coaLink, coaNm, hidLink, hidNm)

        Return rtnResult

    End Function

    ''' <summary>
    ''' BackUpファイル(LM\COABACK)への移動前チェック
    ''' </summary>
    ''' <param name="coaPath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsDriveChk(ByVal coaPath As String) As Boolean

        Dim rtnResult As Boolean = Me.IsNullorEmptyChk(coaPath)

        rtnResult = rtnResult AndAlso Me.IsCDriveChk(coaPath)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 表示項目の値チェック
    ''' </summary>
    ''' <param name="link">表示ファイルパス名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsNullorEmptyChk(ByVal link As String) As Boolean

        '表示項目が空のとき(ファイルが選択されていないとき)スルー
        If String.IsNullOrEmpty(link) = True Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 表示項目と隠し項目の同一チェック
    ''' </summary>
    ''' <param name="coaLink">表示ファイルパス名</param>
    ''' <param name="coaNm">表示ファイル名</param>
    ''' <param name="hidLink">隠しファイルパス名</param>
    ''' <param name="hidNm">隠しファイル名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSameChk(ByVal coaLink As String _
                                   , ByVal coaNm As String _
                                   , ByVal hidLink As String _
                                   , ByVal hidNm As String _
                                                   ) As Boolean

        Dim coaPath As String = String.Concat(coaLink, coaNm)
        Dim hidPath As String = String.Concat(hidLink, hidNm)

        '表示項目と隠し項目が同一の場合スルー
        If coaPath.Equals(hidPath) = True Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 選択元のドライブがCドライブかどうかチェック
    ''' </summary>
    ''' <param name="coaPath">選択元ファイルパス名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsCDriveChk(ByVal coaPath As String) As Boolean

        If Path.GetPathRoot(coaPath) <> LMM020C.CDRIVE Then
            Return False
        End If

        Return True

    End Function



#End Region

#Region "検索チェック"

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

        '単項目スプレッドチェック
        Dim rtnResult As Boolean = Me.IssprChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 単項目スプレッドチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IssprChk() As Boolean


        With Me._Frm

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            '荷主コード(大)
            vCell.SetValidateCell(0, LMM020G.sprDetailDef.CUST_CD_L.ColNo)
            vCell.ItemName = "荷主コード(大)"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 5
            vCell.IsHankakuCheck = True
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '荷主名(大)
            vCell.SetValidateCell(0, LMM020G.sprDetailDef.CUST_NM_L.ColNo)
            vCell.ItemName = "荷主名(大)"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '商品コード
            vCell.SetValidateCell(0, LMM020G.sprDetailDef.GOODS_CD_CUST.ColNo)
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
            vCell.SetValidateCell(0, LMM020G.sprDetailDef.GOODS_NM_1.ColNo)
            vCell.ItemName = "商品名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            'ロット№
            vCell.SetValidateCell(0, LMM020G.sprDetailDef.LOT_NO.ColNo)
            vCell.ItemName = "ロット№"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 40
            vCell.IsHankakuCheck = False
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '届先名
            vCell.SetValidateCell(0, LMM020G.sprDetailDef.DEST_NM.ColNo)
            vCell.ItemName = "届先名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 80
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
        End With

        Return True

    End Function

#End Region

#Region "クリアボタン押下時チェック"

    ''' <summary>
    '''クリアボタン押下時チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IslblPathFileInputChk() As Boolean

        If String.IsNullOrEmpty(Me._Frm.lblCoaLink.TextValue) = True Then
            MyBase.ShowMessage("E078", New String() {"分析票ファイルパス名"})
            Return False
        End If

        Return True

    End Function

#End Region

    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRecordStatusChk(ByVal frm As LMM020F) As Boolean

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
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMM020F, ByVal eventShubetsu As LMM020C.EventShubetsu) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        ''ユーザーのログイン営業所と異なる場合エラー
        'If frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
        '    Dim msg As String = String.Empty

        '    Select Case eventShubetsu

        '        Case LMM020C.EventShubetsu.HENSHU
        '            msg = "編集"

        '        Case LMM020C.EventShubetsu.HUKUSHA
        '            msg = "複写"

        '        Case LMM020C.EventShubetsu.SAKUJO
        '            msg = "削除・復活"

        '    End Select

        '    MyBase.ShowMessage("E178", New String() {msg})
        '    Return False

        'End If

        Return True

    End Function

    ''' <summary>
    ''' 他営業所チェック(開く)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="list"></param>
    ''' <param name="eventShubetsu"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMM020F, ByVal list As ArrayList, ByVal eventShubetsu As LMM020C.EventShubetsu) As Boolean


        Dim nrsbrCdCloNo As Integer = LMM020G.sprDetailDef.NRS_BR_CD.ColNo
        Dim nrsBrCd As String = String.Empty
        Dim rowIndex As Integer = 0
        Dim max As Integer = list.Count - 1
        Dim msg As String = String.Empty

        For i As Integer = 0 To max

            rowIndex = Convert.ToInt32(list(i))

            With frm.sprDetail.ActiveSheet

                nrsBrCd = Me._Vcon.GetCellValue(.Cells(rowIndex, nrsbrCdCloNo))

            End With

        Next

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        'If nrsBrCd.Equals(LMUserInfoManager.GetNrsBrCd) = False Then

        '    Select Case eventShubetsu

        '        Case LMM020C.EventShubetsu.OPEN
        '            msg = "ファイルを開くこと"

        '    End Select

        '    MyBase.ShowMessage("E178", New String() {msg})

        '    Return False

        'End If

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM020C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM020C.EventShubetsu.SHINKI           '新規
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

            Case LMM020C.EventShubetsu.HENSHU          '編集
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

            Case LMM020C.EventShubetsu.HUKUSHA          '複写
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


            Case LMM020C.EventShubetsu.SAKUJO          '削除・復活
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

            Case LMM020C.EventShubetsu.KENSAKU         '検索
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

            Case LMM020C.EventShubetsu.MASTEROPEN          'マスタ参照
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

            Case LMM020C.EventShubetsu.HOZON           '保存
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

            Case LMM020C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM020C.EventShubetsu.DCLICK         'ダブルクリック
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

            Case LMM020C.EventShubetsu.ENTER          'Enter
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

            Case LMM020C.EventShubetsu.ADD          '追加
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

            Case LMM020C.EventShubetsu.CLEAR          'クリア
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

            Case LMM020C.EventShubetsu.OPEN          '開く
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
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMM020C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM020C.EventShubetsu.MASTEROPEN) = True Then
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

                    Dim shipNm As String = .lblTitleCust.Text
                    ctl = New Win.InputMan.LMImTextBox() {.txtCustCdL, .txtCustCdM}
                    msg = New String() {String.Concat(shipNm, LMMControlC.L_NM, LMMControlC.CD) _
                                        , String.Concat(shipNm, LMMControlC.M_NM, LMMControlC.CD)}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblCustNmL, .lblCustNmM}

                Case .txtGoodsCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtGoodsCd}
                    msg = New String() {String.Concat(.lblTitleGoods.Text, LMMControlC.CD)}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblGoodsNm, .lblGoodsKey}

                Case .txtDestCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtDestCd}
                    msg = New String() {String.Concat(.lblTitleDest.Text, LMMControlC.CD)}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblDestNm}

            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), ctl, msg, focusCtl, clearCtl)

        End With

    End Function

    ''' <summary>
    ''' 選択行からパス名とファイル名を取得
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function PathNmExistchk(ByVal list As ArrayList) As String


        Dim pathCloNo As Integer = LMM020G.sprDetailDef.COA_LINK.ColNo
        Dim nmDtCloNo As Integer = LMM020G.sprDetailDef.COA_NAME.ColNo

        Dim rowIndex As Integer = 0

        Dim rowPath As String = String.Empty
        Dim nm As String = String.Empty

        Dim max As Integer = list.Count - 1
        For i As Integer = 0 To max

            rowIndex = Convert.ToInt32(list(i))

            With Me._Frm.sprDetail.ActiveSheet()

                rowPath = Me._Vcon.GetCellValue(.Cells(rowIndex, pathCloNo))
                nm = Me._Vcon.GetCellValue(.Cells(rowIndex, nmDtCloNo))

            End With

        Next

        '選択行にパス名・ファイル名がない場合、エラー
        If String.IsNullOrEmpty(rowPath) = True _
        AndAlso String.IsNullOrEmpty(nm) = True Then
            MyBase.ShowMessage("E297", New String() {"分析票ファイルパス", "分析票ファイル名"})
            Return Nothing
        End If

        'フォルダのパスとファイル名を結合したパスを取得する
        Dim link As String = Path.Combine(rowPath, nm)

        Return link


    End Function

    ''' <summary>
    ''' スプレッドでチェックの付いたRowIndexを取得
    ''' </summary>
    ''' <returns>リスト</returns>
    ''' <remarks>チェックのある行のRowIndexをリストに入れて戻す</remarks>
    Friend Function SprSelectCount() As ArrayList

        Dim defNo As Integer = LMM020G.sprDetailDef.DEF.ColNo

        With Me._Frm.sprDetail.ActiveSheet

            Dim list As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If _Vcon.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    list.Add(i)
                End If
            Next

            Return list

        End With

    End Function


#End Region 'Method


End Class
