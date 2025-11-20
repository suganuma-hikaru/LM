' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM
'  プログラムID     :  LMM140V : ZONEマスタメンテ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMM140Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM140V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM140F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM140F, ByVal v As LMMControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

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

        '薬事法区分、毒劇区分、ガス管理区分 有無設定
        Call Me.SetCmbValue()

        '単項目チェック
        If Me.IsSaveSingleCheck() = False Then
            Return False
        End If

        '存在チェック
        If String.IsNullOrEmpty(_Frm.txtSeiqCd.TextValue) = False Then
            If Me.IsExitsCheck() = False Then
                Return False
            End If
        End If

        '関連チェック
        If Me.IsSaveCheck() = False Then
            Return False
        End If

        '単項目チェック(ゾーンマスタ消防Spread)
        If Me.IsZoneShoboChk() = False Then
            Return False
        End If

        '単項目チェック(棟室ゾーンチェックマスタSpread)
        If Me.IsExistTouSituZonChkSpr(_Frm.sprDetailDoku, LMM140G.sprDetailDefDoku.DOKU_KB.ColNo) = False Then
            Return False
        End If

        If Me.IsExistTouSituZonChkSpr(_Frm.sprDetailKouathuGas, LMM140G.sprDetailDefKouathuGas.KOUATHUGAS_KB.ColNo) = False Then
            Return False
        End If

        If Me.IsExistTouSituZonChkSpr(_Frm.sprDetailYakuzihoJoho, LMM140G.sprDetailDefYakuziho.YAKUZIHO_KB.ColNo) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' スペース除去(編集部)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm

            .txtTouNo.TextValue = .txtTouNo.TextValue.Trim()
            .txtSituNo.TextValue = .txtSituNo.TextValue.Trim()
            .txtZoneCd.TextValue = .txtZoneCd.TextValue.Trim().ToUpper()
            .txtZoneNm.TextValue = .txtZoneNm.TextValue.Trim()
            .txtSeiqCd.TextValue = .txtSeiqCd.TextValue.Trim()


        End With

    End Sub

    ''' <summary>
    ''' 薬事法区分、毒劇区分、ガス管理区分 有無設定
    ''' </summary>
    Private Sub SetCmbValue()

        Dim spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread
        Dim max As Integer
        Dim id As String = String.Empty

        With Me._Frm
            '毒劇区分
            spr = .sprDetailDoku
            max = spr.ActiveSheet.Rows.Count - 1

            '毒劇区分を無に設定
            .cmbPflKb.SelectedValue = LMM140C.NONE

            For i As Integer = 0 To max

                id = Me._Vcon.GetCellValue(spr.ActiveSheet.Cells(i, LMM140G.sprDetailDefDoku.DOKU_KB.ColNo))

                If (String.IsNullOrEmpty(id) = False) And (LMM140C.M_Z_KBN_DOKUGEKI_NONE.Equals(id) = False) Then
                    '毒劇区分を有に設定
                    Me._Frm.cmbPflKb.SelectedValue = LMM140C.ARI
                    Exit For
                End If

            Next

            '高圧ガス区分
            spr = .sprDetailKouathuGas
            max = spr.ActiveSheet.Rows.Count - 1

            '高圧ガス区分を無に設定
            .cmbGasCtlKb.SelectedValue = LMM140C.NONE

            For i As Integer = 0 To max

                id = Me._Vcon.GetCellValue(spr.ActiveSheet.Cells(i, LMM140G.sprDetailDefKouathuGas.KOUATHUGAS_KB.ColNo))

                If (String.IsNullOrEmpty(id) = False) And (LMM140C.M_Z_KBN_KOUATHUGAS_NONE.Equals(id) = False) Then
                    '高圧ガス区分を有に設定
                    Me._Frm.cmbGasCtlKb.SelectedValue = LMM140C.ARI
                    Exit For
                End If

            Next

            '薬事法区分
            spr = .sprDetailYakuzihoJoho
            max = spr.ActiveSheet.Rows.Count - 1

            '薬事法区分を無に設定
            .cmbPharKb.SelectedValue = LMM140C.NONE

            For i As Integer = 0 To max

                id = Me._Vcon.GetCellValue(spr.ActiveSheet.Cells(i, LMM140G.sprDetailDefYakuziho.YAKUZIHO_KB.ColNo))

                If (String.IsNullOrEmpty(id) = False) And (LMM140C.M_Z_KBN_YAKUZIHO_NONE.Equals(id) = False) Then
                    '薬事法区分を有に設定
                    Me._Frm.cmbPharKb.SelectedValue = LMM140C.ARI
                    Exit For
                End If

            Next

        End With

    End Sub

    ''' <summary>
    ''' データ存在チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsExistDataChk(ByVal frm As LMM140F) As Boolean

        If String.IsNullOrEmpty(frm.txtZoneCd.TextValue) = True Then
            MyBase.ShowMessage("E009")
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
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMM140F, ByVal eventShubetsu As LMM140C.EventShubetsu) As Boolean


        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        'ユーザーのログイン営業所と異なる場合エラー
        'If frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
        '    Dim msg As String = String.Empty

        '    Select Case eventShubetsu

        '        Case LMM140C.EventShubetsu.HENSHU
        '            msg = "編集"

        '        Case LMM140C.EventShubetsu.HUKUSHA
        '            msg = "複写"

        '        Case LMM140C.EventShubetsu.SAKUJO
        '            msg = "削除・復活"

        '    End Select

        '    MyBase.ShowMessage("E178", New String() {msg})
        '    Return False

        'End If

        Return True

    End Function

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
            '.cmbNrsBrCd.ItemName = "営業所"
            .cmbNrsBrCd.ItemName = .lblTitleEigyo.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If

            '倉庫
            '2016.01.06 UMANO 英語化対応START
            '.cmbSokoKb.ItemName = "倉庫"
            .cmbSokoKb.ItemName = .lblTitleSoko.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbSokoKb.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbSokoKb) = False Then
                Return False
            End If

            '棟番号
            '2016.01.06 UMANO 英語化対応START
            '.txtTouNo.ItemName = "棟番号"
            .txtTouNo.ItemName = .sprDetail.ActiveSheet.GetColumnLabel(0, LMM140C.SprColumnIndex.TOU_NO)
            '2016.01.06 UMANO 英語化対応END
            .txtTouNo.IsHissuCheck = True
            .txtTouNo.IsFullByteCheck = 2
            .txtTouNo.IsForbiddenWordsCheck = True
            .txtTouNo.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtTouNo) = False Then
                Return False
            End If

            '室番号
            '2016.01.06 UMANO 英語化対応START
            '.txtSituNo.ItemName = "室番号"
            .txtSituNo.ItemName = .sprDetail.ActiveSheet.GetColumnLabel(0, LMM140C.SprColumnIndex.SITU_NO)
            '2016.01.06 UMANO 英語化対応END
            .txtSituNo.IsHissuCheck = True
            'START YANAI 要望番号705
            '.txtSituNo.IsFullByteCheck = 1
            'START S_KOBA 要望番号705
            '.txtSituNo.IsFullByteCheck = 2
            .txtSituNo.IsByteCheck = 2
            'END S_KOBA 要望番号705
            'END YANAI 要望番号705
            .txtSituNo.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(.txtSituNo) = False Then
                Return False
            End If

            'ZONEコード
            '2016.01.06 UMANO 英語化対応START
            '.txtZoneCd.ItemName = "ZONEコード"
            .txtZoneCd.ItemName = .sprDetail.ActiveSheet.GetColumnLabel(0, LMM140C.SprColumnIndex.ZONE_CD)
            '2016.01.06 UMANO 英語化対応END
            .txtZoneCd.IsHissuCheck = True
            .txtZoneCd.IsForbiddenWordsCheck = True
            'START YANAI 要望番号705
            '.txtZoneCd.IsFullByteCheck = 1
            'START S_KOBA 要望番号705
            '.txtZoneCd.IsFullByteCheck = 2
            .txtZoneCd.IsByteCheck = 2
            'END S_KOBA 要望番号705
            'END YANAI 要望番号705
            If MyBase.IsValidateCheck(.txtZoneCd) = False Then
                Return False
            End If

            'ZONE名
            '2016.01.06 UMANO 英語化対応START
            '.txtZoneNm.ItemName = "ZONE名"
            .txtZoneNm.ItemName = .sprDetail.ActiveSheet.GetColumnLabel(0, LMM140C.SprColumnIndex.ZONE_NM)
            '2016.01.06 UMANO 英語化対応END
            .txtZoneNm.IsHissuCheck = True
            .txtZoneNm.IsForbiddenWordsCheck = True
            .txtZoneNm.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtZoneNm) = False Then
                Return False
            End If

            'ZONE区分
            '2016.01.06 UMANO 英語化対応START
            '.cmbZoneKb.ItemName = "ZONE区分"
            .cmbZoneKb.ItemName = .lblTitleZoneKb.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbZoneKb.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbZoneKb) = False Then
                Return False
            End If

            '温度管理区分
            '2016.01.06 UMANO 英語化対応START
            '.cmbHeatCtlKb.ItemName = "温度管理区分"
            .cmbHeatCtlKb.ItemName = .lblTitleHeatCtlKb.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbHeatCtlKb.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbHeatCtlKb) = False Then
                Return False
            End If

            '温度管理
            '2016.01.06 UMANO 英語化対応START
            '.cmbHeatCtl.ItemName = "温度管理"
            .cmbHeatCtl.ItemName = .sprDetail.ActiveSheet.GetColumnLabel(0, LMM140C.SprColumnIndex.ONDO_CTL_KB)
            '2016.01.06 UMANO 英語化対応END
            .cmbHeatCtl.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbHeatCtl) = False Then
                Return False
            End If

            '保税管理区分
            '2016.01.06 UMANO 英語化対応START
            '.cmbBondCtlKb.ItemName = "保税管理区分"
            .cmbBondCtlKb.ItemName = .sprDetail.ActiveSheet.GetColumnLabel(0, LMM140C.SprColumnIndex.HOZEI_KB)
            '2016.01.06 UMANO 英語化対応END
            .cmbBondCtlKb.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbBondCtlKb) = False Then
                Return False
            End If

            '薬事法区分
            '2016.01.06 UMANO 英語化対応START
            '.cmbPharKb.ItemName = "薬事法区分"
            .cmbPharKb.ItemName = .lblTitlePharKb.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbPharKb.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbPharKb) = False Then
                Return False
            End If

            '毒劇区分
            '2016.01.06 UMANO 英語化対応START
            '.cmbPflKb.ItemName = "毒劇区分"
            .cmbPflKb.ItemName = .lblTitlePflKb.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbPflKb.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbPflKb) = False Then
                Return False
            End If

            'ガス管理区分
            '2016.01.06 UMANO 英語化対応START
            '.cmbGasCtlKb.ItemName = "ガス管理区分"
            .cmbGasCtlKb.ItemName = .lblTitleGasCtlKb.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbGasCtlKb.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbGasCtlKb) = False Then
                Return False
            End If

            '請求先コード（2011.08.30 検証結果一覧№53対応）
            '2016.01.06 UMANO 英語化対応START
            '.txtSeiqCd.ItemName = "請求先コード"
            .txtSeiqCd.ItemName = .lblTitleSeiq.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtSeiqCd.IsForbiddenWordsCheck = True
            .txtSeiqCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtSeiqCd) = False Then
                Return False
            End If


        End With

        Return True

    End Function

    ''' <summary>
    ''' 請求先コード存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsExitsCheck() As Boolean

        With Me._Frm

            Dim drs As DataRow() = Nothing

            drs = Me._Vcon.SelectSeiqtoListDataRow(.cmbNrsBrCd.SelectedValue.ToString(), .txtSeiqCd.TextValue)

            If drs.Length < 1 Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E079", New String() {"請求先マスタ", .txtSeiqCd.TextValue})
                MyBase.ShowMessage("E830", New String() { .txtSeiqCd.TextValue})
                '2016.01.06 UMANO 英語化対応END
                .lblSeiqNm.TextValue = String.Empty
                Me._Vcon.SetErrorControl(.txtSeiqCd)
                Return False
            End If

            Dim item As String = String.Empty
            item = String.Concat(drs(0).Item("SEIQTO_NM").ToString(), " ", drs(0).Item("SEIQTO_BUSYO_NM").ToString())
            .lblSeiqNm.TextValue = item

        End With

        Return True


    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveCheck() As Boolean

        With Me._Frm

            Dim brcd As String = .cmbNrsBrCd.SelectedValue.ToString()
            Dim soko As String = .cmbSokoKb.SelectedValue.ToString()
            Dim touNo As String = .txtTouNo.TextValue
            Dim situNo As String = .txtSituNo.TextValue

            Dim drs As DataRow() = Nothing

            drs = Me._Vcon.SelectToShitsuListDataRow(brcd, soko, touNo, situNo)
            '棟室マスタ存在チェック
            If drs.Length < 1 Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E079", New String() {"棟室マスタ", String.Concat("棟番号", touNo, " ", "室番号", situNo)})
                MyBase.ShowMessage("E878", New String() {touNo, situNo})
                '2016.01.06 UMANO 英語化対応END
                .lblTouSituNm.TextValue = String.Empty
                Call Me._Vcon.SetErrorControl(.txtSituNo)
                Call Me._Vcon.SetErrorControl(.txtTouNo)
                Return False
            End If

            .lblTouSituNm.TextValue = drs(0).Item("TOU_SITU_NM").ToString()

            Dim heatmin As Integer = Convert.ToInt32(.numMinHeatLow.Value)
            Dim heatmax As Integer = Convert.ToInt32(.numMaxHeatLim.Value)
            Dim heatnow As Integer = Convert.ToInt32(.numSetHeat.Value)
            Dim teion As String = "02"

            '温度管理区分が"02":定温の場合チェックを行う
            If .cmbHeatCtlKb.SelectedValue.Equals(teion) = True Then
                '温度上限の大小チェック
                If heatmin >= heatmax Then
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E182", New String() {"最高設定温度上限", "最低設定温度下限"})
                    MyBase.ShowMessage("E182", New String() { .lblTitleMaxHeatLim.Text(), .lblTitleMinHeatLow.Text()})
                    '2016.01.06 UMANO 英語化対応END
                    Call Me._Vcon.SetErrorControl(.numMaxHeatLim)
                    Return False
                End If

                '温度下限の整合性チェック
                If heatmin >= heatnow Then
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E014", New String() {"現在設定温度", "最低設定温度下限", "最高設定温度上限"})
                    MyBase.ShowMessage("E014", New String() { .lblTitleNowSetHeat.Text(), .lblTitleMinHeatLow.Text(), .lblTitleMaxHeatLim.Text()})
                    '2016.01.06 UMANO 英語化対応END
                    Call Me._Vcon.SetErrorControl(.numSetHeat)
                    Return False

                End If

                '温度上限の整合性チェック
                If heatnow >= heatmax Then
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E014", New String() {"現在設定温度", "最低設定温度下限", "最高設定温度上限"})
                    MyBase.ShowMessage("E014", New String() { .lblTitleNowSetHeat.Text(), .lblTitleMinHeatLow.Text(), .lblTitleMaxHeatLim.Text()})
                    '2016.01.06 UMANO 英語化対応END
                    Call Me._Vcon.SetErrorControl(.numSetHeat)
                    Return False
                End If

            End If

            Dim jouon As String = "01"
            Dim ondokanrichu As String = "01"

            '温度管理区分の整合性チェック
            If .cmbHeatCtlKb.SelectedValue.Equals(jouon) = True _
            AndAlso .cmbHeatCtl.SelectedValue.Equals(ondokanrichu) = True Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E187", New String() {"温度管理区分が常温", "温度未管理"})
                MyBase.ShowMessage("E879")
                '2016.01.06 UMANO 英語化対応END
                Call Me._Vcon.SetErrorControl(.cmbHeatCtl)
                Return False

            End If

            Dim rentmonth As Integer = Convert.ToInt32(.numRentMonthly.Value)

            '貸料月額の整合性チェック
            If String.IsNullOrEmpty(.txtSeiqCd.TextValue) = True Then

                If 0 < rentmonth Then
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E224", New String() {"貸料月額", "請求先コード"})
                    MyBase.ShowMessage("E224", New String() { .lblTitleRentMonthly.Text(), .lblTitleSeiq.Text()})
                    '2016.01.06 UMANO 英語化対応END
                    Call Me._Vcon.SetErrorControl(.txtSeiqCd)
                    Return False
                End If

            End If

        End With


        Return True


    End Function

    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRecordStatusChk(ByVal frm As LMM140F) As Boolean

        If RecordStatus.DELETE_REC.Equals(frm.lblSituation.RecordStatus) Then
            MyBase.ShowMessage("E035")
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

            '棟
            vCell.SetValidateCell(0, LMM140G.sprDetailDef.TOU_NO.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "棟"
            vCell.ItemName = LMM140G.sprDetailDef.TOU_NO.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '室
            vCell.SetValidateCell(0, LMM140G.sprDetailDef.SITU_NO.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "室"
            vCell.ItemName = LMM140G.sprDetailDef.SITU_NO.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            'START YANAI 要望番号705
            'vCell.IsByteCheck = 1
            vCell.IsByteCheck = 2
            'END YANAI 要望番号705
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'ZONE
            vCell.SetValidateCell(0, LMM140G.sprDetailDef.ZONE_CD.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "ZONE"
            vCell.ItemName = LMM140G.sprDetailDef.ZONE_CD.ColName
            '2016.01.06 UMANO 英語化対応START
            vCell.IsForbiddenWordsCheck = True
            'START YANAI 要望番号705
            'vCell.IsByteCheck = 1
            vCell.IsByteCheck = 2
            'END YANAI 要望番号705
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'ZONE名
            vCell.SetValidateCell(0, LMM140G.sprDetailDef.ZONE_NM.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "ZONE名"
            vCell.ItemName = LMM140G.sprDetailDef.ZONE_NM.ColName
            '2016.01.06 UMANO 英語化対応END
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
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM140C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM140C.EventShubetsu.SHINKI           '新規
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

            Case LMM140C.EventShubetsu.HENSHU          '編集
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

            Case LMM140C.EventShubetsu.HUKUSHA          '複写
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


            Case LMM140C.EventShubetsu.SAKUJO          '削除・復活
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

            Case LMM140C.EventShubetsu.KENSAKU         '検索
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

            Case LMM140C.EventShubetsu.MASTEROPEN          'マスタ参照
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

            Case LMM140C.EventShubetsu.HOZON           '保存
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

            Case LMM140C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM140C.EventShubetsu.DCLICK         'ダブルクリック
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

            Case LMM140C.EventShubetsu.ENTER          'Enter
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
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMM140C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If objNm Is Nothing = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'Return Me._Vcon.SetFocusErrMessage()
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM140C.EventShubetsu.MASTEROPEN) = True Then
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


                Case .txtTouNo.Name, .txtSituNo.Name

                    Dim touNm As String = .lblTitleTouSitu.Text
                    ctl = New Win.InputMan.LMImTextBox() { .txtTouNo, .txtSituNo}
                    '2016.01.06 UMANO 英語化対応START
                    'msg = New String() {String.Concat(touNm, LMMControlC.TOU_NO), String.Concat(touNm, LMMControlC.SITU_NO)}
                    msg = New String() {String.Concat(touNm, .sprDetail.ActiveSheet.GetColumnLabel(0, LMM140C.SprColumnIndex.TOU_NO)), String.Concat(touNm, .sprDetail.ActiveSheet.GetColumnLabel(0, LMM140C.SprColumnIndex.SITU_NO))}
                    '2016.01.06 UMANO 英語化対応END
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() { .lblTouSituNm}

                Case .txtSeiqCd.Name
                    Dim seiqCd As String = .lblTitleSeiq.Text
                    ctl = New Win.InputMan.LMImTextBox() { .txtSeiqCd}
                    '2016.01.06 UMANO 英語化対応START
                    'msg = New String() {String.Concat(seiqCd, LMMControlC.CD)}
                    msg = New String() {String.Concat(seiqCd, .lblTitleSeiq.Text())}
                    '2016.01.06 UMANO 英語化対応END
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() { .lblSeiqNm}

            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), ctl, msg, focusCtl, clearCtl)

        End With

    End Function

    ''' <summary>
    ''' ゾーンマスタ消防Spreadの単項目チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsZoneShoboChk() As Boolean

        '**********ゾーンマスタ消防スプレッドのチェック
        Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprDetailShobo)

        With vCell

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False
            Dim spr As LMSpread = Me._Frm.sprDetailShobo
            Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To max

                '許可日
                .SetValidateCell(i, LMM140G.sprDetailDefShobo.WH_KYOKA_DATE.ColNo)
                .ItemName = LMM140G.sprDetailDefShobo.WH_KYOKA_DATE.ColName
                .IsFullByteCheck = 10
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

            Next

        End With

        Return True

    End Function

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
            Case "sprDetailDoku"
                sprName = "毒劇区分"
            Case "sprDetailKouathuGas"
                sprName = "高圧ガス区分"
            Case "sprDetailYakuzihoJoho"
                sprName = "薬機法区分"
        End Select

        Dim vCell As LMValidatableCells = New LMValidatableCells(spr)

        '必須チェック
        For i As Integer = 0 To max

            vCell.SetValidateCell(i, kbnColmNo)
            vCell.ItemName = sprName
            vCell.IsHissuCheck = True

            If MyBase.IsValidateCheck(vCell) = False Then
                Call Me._Vcon.SetErrorControl(spr, i, kbnColmNo)
                Return False
            End If
        Next

        '重複チェック(同一の区分が使用されている場合エラー)
        For i As Integer = 0 To max
            With spr.ActiveSheet

                id = Me._Vcon.GetCellValue(.Cells(i, kbnColmNo))
                If i <> max Then
                    For j As Integer = i + 1 To max
                        chckId = Me._Vcon.GetCellValue(.Cells(j, kbnColmNo))
                        If id.Equals(chckId) = True Then

                            MyBase.ShowMessage("E496", {sprName, "当該行：" & (i + 1).ToString() & "行目，" & (j + 1).ToString() & "行目"})
                            Call Me._Vcon.SetErrorControl(spr, {i, j}, {kbnColmNo, kbnColmNo})
                            Return False
                        End If
                    Next
                End If
            End With
        Next

        Return True

    End Function

    ''' <summary>
    ''' 行追加/行削除 入力チェック。ゾーンマスタ消防情報
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsRowCheck(ByVal eventShubetsu As LMM140C.EventShubetsu, ByVal ds As DataSet, ByVal frm As LMM140F) As Boolean
        Dim arr As ArrayList = Nothing

        Select Case eventShubetsu
            Case LMM140C.EventShubetsu.INS_T    '行追加

                Dim outSDt As DataTable = ds.Tables(LMZ280C.TABLE_NM_OUT)
                Dim outSRow As DataRow = Nothing

                For j As Integer = 0 To outSDt.Rows.Count - 1

                    outSRow = outSDt.Rows(j)
                    Dim ShoboCd As String = String.Empty
                    ShoboCd = outSRow.Item("SHOBO_CD").ToString

                    With Me._Frm

                        Dim sprMax As Integer = .sprDetailShobo.ActiveSheet.Rows.Count - 1
                        For i As Integer = 0 To sprMax

                            If (ShoboCd).Equals(Me._Vcon.GetCellValue(.sprDetailShobo.ActiveSheet.Cells(i, LMM140C.SprColumnIndexShobo.SHOBO_CD))) Then
                                MyBase.ShowMessage("E177", New String() {frm.grpShoboJoho.Text()})
                                Return False
                            End If
                        Next

                    End With

                Next

                Return True

            Case LMM140C.EventShubetsu.DEL_T    '行削除

                With Me._Frm
                    '選択ﾁｪｯｸ
                    arr = _Vcon.SprSelectList(LMM140C.SprColumnIndexShobo.DEF, .sprDetailShobo)
                    If 0 = arr.Count Then
                        .sprDetailShobo.Focus()
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
    Friend Function IsTouSituZoneChkRowCheck(ByVal eventShubetsu As LMM140C.EventShubetsu, ByVal frm As LMM140F, ByVal spr As LMSpread, ByVal defColNo As Integer) As Boolean

        Dim arr As ArrayList = Nothing

        Select Case eventShubetsu
            Case LMM140C.EventShubetsu.INS_DOKU,
                 LMM140C.EventShubetsu.INS_KOUATHUGAS,
                 LMM140C.EventShubetsu.INS_YAKUZIHO     '行追加

                '空行チェック
                If Me.IsKuranChk(spr, defColNo) = False Then
                    Return False
                End If

                Return True

            Case LMM140C.EventShubetsu.DEL_DOKU,
                 LMM140C.EventShubetsu.DEL_KOUATHUGAS,
                 LMM140C.EventShubetsu.DEL_YAKUZIHO     '行削除

                '選択ﾁｪｯｸ
                arr = _Vcon.SprSelectList(defColNo, spr)
                If 0 = arr.Count Then
                    spr.Focus()
                    MyBase.ShowMessage("E009")
                    Return False
                End If

                Return True

        End Select

    End Function

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
                If String.IsNullOrEmpty(Me._Vcon.GetCellValue(.Cells(i, defColNo + 1))) = False Then
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

#End Region 'Method

End Class
