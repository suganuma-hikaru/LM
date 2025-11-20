' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM280V : 横持ちマスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMM280Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMM280V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM280F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMMControlV

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM280F, ByVal v As LMMControlV)

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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM280C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM280C.EventShubetsu.SHINKI           '新規
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

            Case LMM280C.EventShubetsu.HENSHU          '編集
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

            Case LMM280C.EventShubetsu.FUKUSHA          '複写
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


            Case LMM280C.EventShubetsu.SAKUJO_HUKKATU          '削除・復活
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

            Case LMM280C.EventShubetsu.KENSAKU         '検索
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

            Case LMM280C.EventShubetsu.HOZON           '保存
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

            Case LMM280C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM280C.EventShubetsu.DOUBLE_CLICK         'ダブルクリック
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

            Case LMM280C.EventShubetsu.ENTER          'Enter
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
    ''' 編集押下時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsHenshuChk() As Boolean

        Dim msg As String = "編集"

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.txtYokomochiTariff.TextValue) = False Then
            Return False
        End If

        '他営業所チェック
        If Me._ControlV.IsUserNrsBrCdChk(Me._Frm.cmbBr.SelectedValue.ToString(), msg) = False Then
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

        Dim msg As String = "複写"

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.txtYokomochiTariff.TextValue) = False Then
            Return False
        End If

        '他営業所チェック
        If Me._ControlV.IsUserNrsBrCdChk(Me._Frm.cmbBr.SelectedValue.ToString(), msg) = False Then
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

        Dim msg As String = "削除・復活"

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.txtYokomochiTariff.TextValue) = False Then
            Return False
        End If

        '他営業所チェック
        If Me._ControlV.IsUserNrsBrCdChk(Me._Frm.cmbBr.SelectedValue.ToString(), msg) = False Then
            Return False
        End If

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

        'スペース除去
        Call Me._ControlV.TrimSpaceSprTextvalue(Me._Frm.sprYokomochiHed, 0)

        '単項目チェック
        If Me.IsKensakuSingleChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 保存押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsSaveChk() As Boolean

        With Me._Frm

            'スペース除去
            Call Me._ControlV.TrimSpaceTextvalue(Me._Frm)
            Call Me._ControlV.TrimSpaceSprTextvalue(.sprYokomochiDtl _
                                                    , .sprYokomochiDtl.ActiveSheet.Rows.Count - 1 _
                                                    , .sprYokomochiDtl.ActiveSheet.Columns.Count - 1)

            '単項目チェック
            If Me.IsSaveSingleChk() = False Then
                Return False
            End If

            '関連チェック
            If Me.IsSaveRelationChk() = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 行追加時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsAddRowChk(ByVal maxSeq As Integer) As Boolean

        With Me._Frm
            '【計算方法】
            .cmbKeisanHoho.ItemName = .lblTitleKeisanHoho.Text
            .cmbKeisanHoho.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbKeisanHoho) = False Then
                Return False
            End If
        End With

        '空行チェック
        If Me.IsKuranChk() = False Then
            Return False
        End If

        '上限チェック
        maxSeq = maxSeq + 1
        If Me._ControlV.IsMaxChk(maxSeq, 999, "横持ちタリフコード枝番") = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 行削除時チェック
    ''' </summary>
    ''' <param name="list">選択行格納配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsDelRowChk(ByVal list As ArrayList) As Boolean

        '必須選択チェック
        If list.Count = 0 Then
            MyBase.ShowMessage("E009")
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
    ''' 検索押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleChk() As Boolean

        With Me._Frm

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprYokomochiHed)

            '【横持ちタリフコード】
            vCell.SetValidateCell(0, LMM280G.sprHedDef.YOKOMOCHI_TARIFF_CD.ColNo)
            vCell.ItemName = LMM280G.sprHedDef.YOKOMOCHI_TARIFF_CD.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【備考】
            vCell.SetValidateCell(0, LMM280G.sprHedDef.BIKO.ColNo)
            vCell.ItemName = LMM280G.sprHedDef.BIKO.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 保存押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSingleChk() As Boolean

        With Me._Frm

            '【営業所】
            .cmbBr.ItemName = .lblTitleBr.Text
            .cmbBr.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbBr) = False Then
                Return False
            End If

            '【横持ちタリフコード】
            .txtYokomochiTariff.ItemName = .lblTitleYokomochiTariff.Text
            .txtYokomochiTariff.IsHissuCheck = True
            .txtYokomochiTariff.IsForbiddenWordsCheck = True
            .txtYokomochiTariff.IsByteCheck = 10
            .txtYokomochiTariff.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtYokomochiTariff) = False Then
                Return False
            End If

            '【備考】
            .txtBiko.ItemName = .lblTitleBiko.Text
            .txtBiko.IsForbiddenWordsCheck = True
            .txtBiko.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtBiko) = False Then
                Return False
            End If

            '【計算方法】
            .cmbKeisanHoho.ItemName = .lblTitleKeisanHoho.Text
            .cmbKeisanHoho.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbKeisanHoho) = False Then
                Return False
            End If

            '【明細分割】
            .cmbMeisaiBunkatu.ItemName = .lblTitleMeisaiBunkatu.Text
            .cmbMeisaiBunkatu.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbMeisaiBunkatu) = False Then
                Return False
            End If

        End With

            Return True

    End Function

    ''' <summary>
    ''' 保存押下時関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveRelationChk() As Boolean

        Dim spr As Spread.LMSpread = Me._Frm.sprYokomochiDtl
        With spr.ActiveSheet

            '*********************** エラーチェック *****************************
            Dim max As Integer = .Rows.Count - 1
            '【明細必須チェック】
            Dim colIndex As Integer = 0
            Dim msg As String = String.Empty
            Select Case Me._Frm.cmbKeisanHoho.SelectedValue.ToString()
                Case LMM280C.KEISAN_CD_NISUGATA      '荷姿建て
                    colIndex = LMM280G.sprDtlDef.NISUGATA_KBN.ColNo
                    msg = "荷姿"
                Case LMM280C.KEISAN_CD_SHADATE       '車建て
                    colIndex = LMM280G.sprDtlDef.SHASHU.ColNo
                    msg = "車種"
                Case LMM280C.KEISAN_CD_TEIZO_UNCHIN  '逓増運賃
                    colIndex = LMM280G.sprDtlDef.KUGIRI_JURYO.ColNo
                    msg = "区切重量"
                Case LMM280C.KEISAN_CD_JURYO         '重量建て
                    colIndex = LMM280G.sprDtlDef.KIKENHIN.ColNo
                    msg = "危険品"
            End Select
            For i As Integer = 0 To max
                If String.IsNullOrEmpty(Me._ControlV.GetCellValue(.Cells(i, colIndex))) Then
                    MyBase.ShowMessage("E001", New String() {msg})
                    Me._ControlV.SetErrorControl(spr, i, colIndex)
                    Return False
                End If
                '危険品が空の場合は、01:一般品を設定
                If String.IsNullOrEmpty(Me._ControlV.GetCellValue(.Cells(i, LMM280G.sprDtlDef.KIKENHIN.ColNo))) Then
                    spr.SetCellValue(i, LMM280G.sprDtlDef.KIKENHIN.ColNo, "01")
                End If
            Next

            '【明細重複チェック】
            Dim chkNisugata As String = String.Empty
            Dim chkShashu As String = String.Empty
            Dim chkKugiriJuryo As String = String.Empty
            Dim chkKikenhin As String = String.Empty
            Dim chkFlg As Boolean = True
            For i As Integer = 0 To max
                chkNisugata = Me._ControlV.GetCellValue(.Cells(i, LMM280G.sprDtlDef.NISUGATA_KBN.ColNo))
                chkShashu = Me._ControlV.GetCellValue(.Cells(i, LMM280G.sprDtlDef.SHASHU.ColNo))
                chkKugiriJuryo = Me._ControlV.GetCellValue(.Cells(i, LMM280G.sprDtlDef.KUGIRI_JURYO.ColNo))
                chkKikenhin = Me._ControlV.GetCellValue(.Cells(i, LMM280G.sprDtlDef.KIKENHIN.ColNo))
                For j As Integer = i + 1 To max

                    If Me._ControlV.GetCellValue(.Cells(j, LMM280G.sprDtlDef.KUGIRI_JURYO.ColNo)).Equals(chkKugiriJuryo) = True _
                        AndAlso Me._ControlV.GetCellValue(.Cells(j, LMM280G.sprDtlDef.KIKENHIN.ColNo)).Equals(chkKikenhin) = True _
                        Then

                        Select Case Me._Frm.cmbKeisanHoho.SelectedValue.ToString()

                            Case LMM280C.KEISAN_CD_NISUGATA

                                If Me._ControlV.GetCellValue(.Cells(j, LMM280G.sprDtlDef.NISUGATA_KBN.ColNo)).Equals(chkNisugata) = False Then
                                    Continue For
                                End If

                            Case LMM280C.KEISAN_CD_SHADATE

                                If Me._ControlV.GetCellValue(.Cells(j, LMM280G.sprDtlDef.SHASHU.ColNo)).Equals(chkShashu) = False Then
                                    Continue For
                                End If

                        End Select

                        MyBase.ShowMessage("E177", New String() {"横持ちタリフ明細情報"})
                        Return False

                    End If
                Next
            Next

            '*********************** ワーニングチェック *****************************

            '【0円チェック】
            Select Case Me._Frm.cmbKeisanHoho.SelectedValue.ToString()
                Case LMM280C.KEISAN_CD_JURYO         '重量建て
                    colIndex = LMM280G.sprDtlDef.TANKA_PER_KGS.ColNo
                    msg = "KGSあたり単価"
                Case Else
                    colIndex = LMM280G.sprDtlDef.TANKA.ColNo
                    msg = "単価"
            End Select
            For i As Integer = 0 To max
                If Convert.ToDecimal(Me._ControlV.GetCellValue(.Cells(i, colIndex))) = 0 Then
                    If MyBase.ShowMessage("W136", New String() {msg}) = MsgBoxResult.Ok Then
                        Return True
                    Else
                        Me._ControlV.SetErrorControl(spr, i, colIndex)
                        Me.SetBaseMsg() '基本メッセージの設定
                        Return False
                    End If
                End If
            Next

        End With

        Return True

    End Function

    ''' <summary>
    '''行追加時、Spreadに何も入力されていない場合、エラー
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKuranChk() As Boolean

        With Me._Frm.sprYokomochiDtl.ActiveSheet

            Dim rowMax As Integer = .Rows.Count - 1
            Dim colMax As Integer = .Columns.Count - 1
            For i As Integer = 0 To rowMax
                If String.IsNullOrEmpty(Me._ControlV.GetCellValue(.Cells(i, LMM280G.sprDtlDef.NISUGATA_KBN.ColNo))) _
                AndAlso String.IsNullOrEmpty(Me._ControlV.GetCellValue(.Cells(i, LMM280G.sprDtlDef.SHASHU.ColNo))) _
                AndAlso Convert.ToDecimal(Me._ControlV.GetCellValue(.Cells(i, LMM280G.sprDtlDef.KUGIRI_JURYO.ColNo))) = 0 _
                AndAlso String.IsNullOrEmpty(Me._ControlV.GetCellValue(.Cells(i, LMM280G.sprDtlDef.KIKENHIN.ColNo))) _
                AndAlso Convert.ToDecimal(Me._ControlV.GetCellValue(.Cells(i, LMM280G.sprDtlDef.TANKA_PER_KGS.ColNo))) = 0 _
                AndAlso Convert.ToDecimal(Me._ControlV.GetCellValue(.Cells(i, LMM280G.sprDtlDef.TANKA.ColNo))) = 0 Then
                    MyBase.ShowMessage("E219")
                    Return False
                End If
            Next
        End With

        Return True

    End Function

#End Region

#End Region

End Class
