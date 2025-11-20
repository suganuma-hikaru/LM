' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM150V : 請求テンプレートマスタメンテ
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
''' LMM150Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM150V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM150F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM150F, ByVal v As LMMControlV)

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

        'マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsMstExistChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMstExistChk() As Boolean

        Dim rtnResult As Boolean = True

        'JISマスタ存在チェック
        If String.IsNullOrEmpty(Me._Frm.txtJis.TextValue) = False Then
            rtnResult = Me.IsJisExistCheck()
        End If

        Return rtnResult

    End Function
    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRecordStatusChk(ByVal frm As LMM150F) As Boolean

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
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMM150F, ByVal eventShubetsu As LMM150C.EventShubetsu) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        ''ユーザーのログイン営業所と異なる場合エラー
        'If frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
        '    Dim msg As String = String.Empty

        '    Select Case eventShubetsu

        '        Case LMM150C.EventShubetsu.HENSHU
        '            msg = "編集"

        '        Case LMM150C.EventShubetsu.HUKUSHA
        '            msg = "複写"

        '        Case LMM150C.EventShubetsu.SAKUJO
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

            .txtWhCd.TextValue = .txtWhCd.TextValue.Trim()
            .txtWhNm.TextValue = .txtWhNm.TextValue.Trim()
            .txtZip.TextValue = .txtZip.TextValue.Trim()
            .txtAd1.TextValue = .txtAd1.TextValue.Trim()
            .txtAd2.TextValue = .txtAd2.TextValue.Trim()
            .txtAd3.TextValue = .txtAd3.TextValue.Trim()
            .txtTel.TextValue = .txtTel.TextValue.Trim()
            .txtFax.TextValue = .txtFax.TextValue.Trim()
            .txtJis.TextValue = .txtJis.TextValue.Trim()

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
            '2016.01.06 UMANO 英語化対応START
            '.cmbNrsBrCd.Name = "営業所"
            .cmbNrsBrCd.Name = .lblTitleNrsBrCd.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If

            '倉庫コード
            '2016.01.06 UMANO 英語化対応START
            '.txtWhCd.ItemName = "倉庫コード"
            .txtWhCd.ItemName = .lblTitleWh.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtWhCd.IsHissuCheck = True
            .txtWhCd.IsForbiddenWordsCheck = True
            .txtWhCd.IsFullByteCheck = 3
            .txtWhCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtWhCd) = False Then
                Return False
            End If

            '倉庫名
            '2016.01.06 UMANO 英語化対応START
            '.txtWhNm.ItemName = "倉庫名"
            .txtWhNm.ItemName = .lblTitleWh.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtWhNm.IsHissuCheck = True
            .txtWhNm.IsForbiddenWordsCheck = True
            .txtWhNm.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtWhNm) = False Then
                Return False
            End If

            '郵便番号
            '2016.01.06 UMANO 英語化対応START
            '.txtZip.ItemName = "郵便番号"
            .txtZip.ItemName = .lblTitleZip.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtZip.IsHissuCheck = True
            .txtZip.IsForbiddenWordsCheck = True
            .txtZip.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtZip) = False Then
                Return False
            End If

            '住所1
            '2016.01.06 UMANO 英語化対応START
            '.txtAd1.ItemName = "住所1"
            .txtAd1.ItemName = .lblTitleAd1.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtAd1.IsForbiddenWordsCheck = True
            .txtAd1.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtAd1) = False Then
                Return False
            End If

            '住所2
            '2016.01.06 UMANO 英語化対応START
            '.txtAd2.ItemName = "住所2"
            .txtAd2.ItemName = .lblTitleAd2.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtAd2.IsForbiddenWordsCheck = True
            .txtAd2.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtAd2) = False Then
                Return False
            End If

            '住所3
            '2016.01.06 UMANO 英語化対応START
            '.txtAd3.ItemName = "住所3"
            .txtAd3.ItemName = .lblTitleAd3.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtAd3.IsForbiddenWordsCheck = True
            .txtAd3.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtAd3) = False Then
                Return False
            End If

            '倉庫区分
            '2016.01.06 UMANO 英語化対応START
            '.cmbJtsFlg.ItemName = "倉庫区分"
            .cmbJtsFlg.ItemName = .lblTitleJtsFlg.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbJtsFlg.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbJtsFlg) = False Then
                Return False
            End If

            '電話番号
            '2016.01.06 UMANO 英語化対応START
            '.txtTel.ItemName = "電話番号"
            .txtTel.ItemName = .lblTitleTel.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtTel.IsForbiddenWordsCheck = True
            .txtTel.IsByteCheck = 20            
            If MyBase.IsValidateCheck(.txtTel) = False Then
                Return False
            End If

            'FAX番号
            '2016.01.06 UMANO 英語化対応START
            '.txtFax.ItemName = "FAX番号"
            .txtFax.ItemName = .lblTitleFax.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtFax.IsForbiddenWordsCheck = True
            .txtFax.IsByteCheck = 20            
            If MyBase.IsValidateCheck(.txtFax) = False Then
                Return False
            End If

            'JISコード
            '2016.01.06 UMANO 英語化対応START
            '.txtJis.ItemName = "JISコード"
            .txtJis.ItemName = .lblTitleJis.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtJis.IsForbiddenWordsCheck = True
            .txtJis.IsByteCheck = 7
            If MyBase.IsValidateCheck(.txtJis) = False Then
                Return False
            End If

            '入荷予定
            '2016.01.06 UMANO 英語化対応START
            '.cmbInkaYotei.ItemName = "入荷予定"
            .cmbInkaYotei.ItemName = .LmTitleLabel51.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbInkaYotei.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbInkaYotei) = False Then
                Return False
            End If

            '入荷受付票印刷
            '2016.01.06 UMANO 英語化対応START
            '.cmbInkaUkePrt.ItemName = "入荷受付票印刷"
            .cmbInkaUkePrt.ItemName = .LmTitleLabel50.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbInkaUkePrt.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbInkaUkePrt) = False Then
                Return False
            End If

            '入荷検品
            '2016.01.06 UMANO 英語化対応START
            '.cmbInkaKenpin.ItemName = "入荷検品"
            .cmbInkaKenpin.ItemName = .LmTitleLabel46.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbInkaKenpin.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbInkaKenpin) = False Then
                Return False
            End If

            '入荷確認
            '2016.01.06 UMANO 英語化対応START
            '.cmbInkaKakunin.ItemName = "入荷確認"
            .cmbInkaKakunin.ItemName = .LmTitleLabel47.Text()
            '2016.01.06 UMANO 英語化対応END            
            .cmbInkaKakunin.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbInkaKakunin) = False Then
                Return False
            End If

            '入荷報告
            '2016.01.06 UMANO 英語化対応START
            '.cmbInkaInfo.ItemName = "入荷報告"
            .cmbInkaInfo.ItemName = .LmTitleLabel49.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbInkaInfo.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbInkaInfo) = False Then
                Return False
            End If

            'START YANAI 要望番号394
            '出荷予定
            '2016.01.06 UMANO 英語化対応START
            '.cmbOutkaYotei.ItemName = "出荷予定"
            .cmbOutkaYotei.ItemName = .LmTitleLabel2.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbOutkaYotei.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbOutkaYotei) = False Then
                Return False
            End If
            'END YANAI 要望番号394

            '出荷指図書印刷
            '2016.01.06 UMANO 英語化対応START
            '.cmbOutkaSashizuPrt.ItemName = "出荷指図書印刷"
            .cmbOutkaSashizuPrt.ItemName = .LmTitleLabel41.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbOutkaSashizuPrt.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbOutkaSashizuPrt) = False Then
                Return False
            End If

            '出庫完了
            '2016.01.06 UMANO 英語化対応START
            '.cmbOutkaKanryo.ItemName = "出庫完了"
            .cmbOutkaKanryo.ItemName = .LmTitleLabel40.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbOutkaKanryo.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbOutkaKanryo) = False Then
                Return False
            End If

            '出荷検品
            '2016.01.06 UMANO 英語化対応START
            '.cmbOutkaKenpin.ItemName = "出荷検品"
            .cmbOutkaKenpin.ItemName = .LmTitleLabel36.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbOutkaKenpin.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbOutkaKenpin) = False Then
                Return False
            End If

            '出荷報告
            '2016.01.06 UMANO 英語化対応START
            '.cmbOutkaInfo.ItemName = "出荷報告"
            .cmbOutkaInfo.ItemName = .LmTitleLabel37.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbOutkaInfo.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbOutkaInfo) = False Then
                Return False
            End If

            'ロケーション管理
            '2016.01.06 UMANO 英語化対応START
            '.cmbLocManager.ItemName = "ロケーション管理"
            .cmbLocManager.ItemName = .LmTitleLabel26.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbLocManager.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbLocManager) = False Then
                Return False
            End If

            '棟管理
            '2016.01.06 UMANO 英語化対応START
            '.cmbTouKanri.ItemName = "棟管理"
            .cmbTouKanri.ItemName = .LmTitleLabel11.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbTouKanri.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbTouKanri) = False Then
                Return False
            End If

            '棟班別出荷指図
            '2016.01.06 UMANO 英語化対応START
            '.cmbTouhanSashizu.ItemName = "棟班別出荷指図"
            .cmbTouhanSashizu.ItemName = .LmTitleLabel27.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbTouhanSashizu.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbTouhanSashizu) = False Then
                Return False
            End If

            'START KIM 2012/09/12 要望番号1404 
            '商品・ロット違い管理有無
            '2016.01.06 UMANO 英語化対応START
            '.cmbGoodslotCheckYN.ItemName = "商品・ロット違い管理有無"
            .cmbGoodslotCheckYN.ItemName = .LmTitleLabel3.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbGoodslotCheckYN.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbGoodslotCheckYN) = False Then
                Return False
            End If
            'END KIM 2012/09/12 要望番号1404 

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

            '電話番号
            vCell.SetValidateCell(0, LMM150G.sprDetailDef.TEL.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "電話番号"
            vCell.ItemName = LMM150G.sprDetailDef.TEL.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20            
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'FAX番号
            vCell.SetValidateCell(0, LMM150G.sprDetailDef.FAX.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "FAX番号"
            vCell.ItemName = LMM150G.sprDetailDef.FAX.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20            
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' JISコード存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsJisExistCheck() As Boolean

        With Me._Frm

            Dim drs As DataRow() = Nothing
            Dim jisCd As String = .txtJis.TextValue

            drs = Me._Vcon.SelectJisListDataRow(jisCd)

            If drs.Length < 1 Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E079", New String() {"JISマスタ", jisCd})
                MyBase.ShowMessage("E828", New String() {jisCd})
                '2016.01.06 UMANO 英語化対応END
                .lblKen.TextValue = String.Empty
                .lblShi.TextValue = String.Empty
                Call Me._Vcon.SetErrorControl(.txtJis)
                Return False

            End If

            '2011/08/11 野島 共通テスト修正(コード補完機能) スタート
            .txtJis.TextValue = drs(0).Item("JIS_CD").ToString()
            '2011/08/11 野島 共通テスト修正(コード補完機能) エンド
            .lblKen.TextValue = drs(0).Item("KEN").ToString()
            .lblShi.TextValue = drs(0).Item("SHI").ToString()

        End With

        Return True


    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM150C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM150C.EventShubetsu.SHINKI           '新規
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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

            Case LMM150C.EventShubetsu.HENSHU          '編集
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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

            Case LMM150C.EventShubetsu.HUKUSHA          '複写
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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


            Case LMM150C.EventShubetsu.SAKUJO          '削除・復活
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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

            Case LMM150C.EventShubetsu.KENSAKU         '検索
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

            Case LMM150C.EventShubetsu.MASTEROPEN          'マスタ参照
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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

            Case LMM150C.EventShubetsu.HOZON           '保存
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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

            Case LMM150C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM150C.EventShubetsu.DCLICK         'ダブルクリック
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

            Case LMM150C.EventShubetsu.ENTER          'Enter
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
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMM150C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If objNm Is Nothing = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'Return Me._Vcon.SetFocusErrMessage()
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM150C.EventShubetsu.MASTEROPEN) = True Then
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

                Case .txtZip.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtZip}
                    msg = New String() {.lblTitleZip.Text}

                Case .txtJis.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtJis}
                    msg = New String() {.lblTitleJis.Text}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblKen, .lblShi}

            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), ctl, msg, focusCtl, clearCtl)

        End With

    End Function

#End Region 'Method

End Class
