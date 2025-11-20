' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI220  : 定期検査管理
'  作  成  者       :  [KIM]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win

''' <summary>
''' LMI220Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI220V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI220F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMIControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMIControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI220F, ByVal v As LMIControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = v

        'Gamen共通クラスの設定
        Me._Gcon = New LMIControlG(DirectCast(frm, Form))

    End Sub

#End Region 'Constructor

#Region "Method"


    Friend Function IsInputCheck(ByVal frm As LMI220F, ByVal eventShubetsu As LMI220C.EventShubetsu) As Boolean

        Select Case eventShubetsu

            Case LMI220C.EventShubetsu.SHINKI
                'チェックなし
            Case LMI220C.EventShubetsu.HENSHU
                Return Me.IsEditInputCheck(frm)

            Case LMI220C.EventShubetsu.SAKUJO_HUKKATSU
                Return Me.IsDelRevInputCheck(frm)

            Case LMI220C.EventShubetsu.KENSAKU '検索
                Return Me.IsKensakuInputCheck()

            Case LMI220C.EventShubetsu.HOZON '保存
                Return Me.IsSaveInputChk()

            Case LMI220C.EventShubetsu.DOUBLECLICK


        End Select

        Return True

    End Function

#Region "IsEditInputCheck"

    ''' <summary>
    ''' 入力チェック（編集）
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsEditInputCheck(ByVal frm As LMI220F) As Boolean

        '編集可否チェック
        If Me.IsRecordStatusChk(frm) = False Then
            Return False
        End If

        'データ存在チェック
        If String.IsNullOrEmpty(frm.txtSerialNo.TextValue) = True Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function
   
#End Region

#Region "IsDelRevInputCheck"

    ''' <summary>
    ''' 入力チェック（削除・復活）
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsDelRevInputCheck(ByVal frm As LMI220F) As Boolean

        '未選択チェック
        If String.IsNullOrEmpty(frm.txtSerialNo.TextValue) = True Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

#End Region

#Region "IsKensakuInputCheck"

    ''' <summary>
    ''' 入力チェック（検索）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuInputCheck() As Boolean

        'Trimチェック
        '検索
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0)

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '【単項目チェック】
            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            'シリンダ番号
            vCell.SetValidateCell(0, LMI220G.sprDetailDef.SERIAL_NO.ColNo)
            vCell.ItemName = LMI220G.sprDetailDef.SERIAL_NO.ColName
            vCell.IsByteCheck = 40
            vCell.IsForbiddenWordsCheck = chkFlg
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

            '製造日
            vCell.SetValidateCell(0, LMI220G.sprDetailDef.PROD_DATE.ColNo)
            vCell.ItemName = LMI220G.sprDetailDef.PROD_DATE.ColName
            vCell.IsFullByteCheck = 10
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

            '再検査日
            vCell.SetValidateCell(0, LMI220G.sprDetailDef.LAST_TEST_DATE.ColNo)
            vCell.ItemName = LMI220G.sprDetailDef.LAST_TEST_DATE.ColName
            vCell.IsFullByteCheck = 10
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

            '次回定検日
            vCell.SetValidateCell(0, LMI220G.sprDetailDef.NEXT_TEST_DATE.ColNo)
            vCell.ItemName = LMI220G.sprDetailDef.NEXT_TEST_DATE.ColName
            vCell.IsFullByteCheck = 10
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

            '【関連項目チェック】：なし

        End With

        Return True

    End Function

#End Region

#Region "IsSaveInputChk"

    ''' <summary>
    ''' 入力チェック（保管）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Private Function IsSaveInputChk() As Boolean

        Dim chkFlg As Boolean = True
        Dim errorFlg As Boolean = False

        With Me._Frm

            'スペース除去
            Call Me._Vcon.TrimSpaceTextvalue(Me._Frm)

            '【 単項目チェック 】

            '営業所
            .cmbNrsBrCd.ItemName = .lblEigyosyo.Text
            .cmbNrsBrCd.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = errorFlg Then
                Return errorFlg
            End If

            'サイズ
            .cmbSize.ItemName = .lblSize.Text
            .cmbSize.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbSize) = errorFlg Then
                Return errorFlg
            End If

            'シリンダ番号
            .txtSerialNo.ItemName = .lblSerialNo.Text
            .txtSerialNo.IsHissuCheck = chkFlg
            .txtSerialNo.IsForbiddenWordsCheck = chkFlg
            .txtSerialNo.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtSerialNo) = errorFlg Then
                Return errorFlg
            End If

            '製造日
            .imdProdDate.ItemName = .lblProdDate.Text
            .imdProdDate.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.imdProdDate) = errorFlg Then
                Return errorFlg
            End If

            If Me.IsInputDateFullByteChk(.imdProdDate, .lblProdDate.Text) = errorFlg Then
                Return errorFlg
            End If

            '再検査日
            .imdLastTestDate.ItemName = .lblLastTestDate.Text
            .imdLastTestDate.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.imdLastTestDate) = errorFlg Then
                Return errorFlg
            End If
            If Me.IsInputDateFullByteChk(.imdLastTestDate, .lblLastTestDate.Text) = errorFlg Then
                Return errorFlg
            End If

            '【 関連チェック 】
            '製造日より再検査日が小さいの場合はエラー
            If Me.IsLargeSmallChk(.imdLastTestDate.TextValue, .imdProdDate.TextValue, False) = False Then
                Return Me._Vcon.SetErrMessage("E039", New String() {.lblLastTestDate.Text, .lblProdDate.Text})
                Return errorFlg
            End If

        End With

        Return chkFlg

    End Function

#End Region

    ''' <summary>
    ''' 日付のフルバイトチェック
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="str">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputDateFullByteChk(ByVal ctl As Win.InputMan.LMImDate, ByVal str As String) As Boolean

        If ctl.IsDateFullByteCheck = False Then

            Return Me._Vcon.SetErrMessage("E038", New String() {str, "8"})

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
    Private Overloads Function IsLargeSmallChk(ByVal large As String, ByVal small As String, ByVal equalFlg As Boolean) As Boolean

        '値がない場合、スルー
        If String.IsNullOrEmpty(large) = True OrElse _
           String.IsNullOrEmpty(small) = True _
           Then
            Return True
        End If

        '大小比較
        Return Me._Vcon.IsLargeSmallChk(Convert.ToDecimal(Me._Gcon.FormatNumValue(large)), Convert.ToDecimal(Me._Gcon.FormatNumValue(small)), equalFlg)

    End Function

    ''' <summary>
    ''' 削除状態の混在チェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="chkList"></param>
    ''' <param name="delFlg">true：削除時 false：復活時</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SysDelFlgChk(ByVal frm As LMI220F, ByVal chkList As ArrayList, ByVal delFlg As Boolean) As Boolean

        Dim rtnFlg As Boolean = True
        Dim sysdelFlg As String = String.Empty
        Dim hikakuFlg As String = LMConst.FLG.OFF
        If delFlg = True Then
            hikakuFlg = LMConst.FLG.ON
        End If

        For i As Integer = 0 To chkList.Count - 1
            sysdelFlg = Me._Vcon.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(chkList(i)), LMI220C.SprColumnIndex.SYS_DEL_FLG))
            If sysdelFlg.Equals(hikakuFlg) = False Then
                rtnFlg = False
                Exit For
            End If
        Next

        Return rtnFlg

    End Function










　
    ''' <summary>
    ''' 保存時のtrim
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm

            .txtSerialNo.TextValue = .txtSerialNo.TextValue.Trim()

        End With

    End Sub

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
            ''営業所
            '.cmbNrsBrCd.ItemName = .lblEigyosyo.Text
            '.cmbNrsBrCd.IsHissuCheck = chkFlg
            'If MyBase.IsValidateCheck(.cmbNrsBrCd) = errorFlg Then
            '    Return errorFlg
            'End If

            ''荷主コード（大）
            '.txtCustCdL.ItemName = .lblCustL.Text
            '.txtCustCdL.IsHissuCheck = chkFlg
            '.txtCustCdL.IsForbiddenWordsCheck = chkFlg
            '.txtCustCdL.IsFullByteCheck = 5
            '.txtCustCdL.IsMiddleSpace = chkFlg
            'If MyBase.IsValidateCheck(.txtCustCdL) = errorFlg Then
            '    Return errorFlg
            'End If

            ''届先コード
            '.txtDestCd.ItemName = .lblDestCd.Text
            '.txtDestCd.IsHissuCheck = chkFlg
            '.txtDestCd.IsForbiddenWordsCheck = chkFlg
            '.txtDestCd.IsByteCheck = 15
            'If MyBase.IsValidateCheck(.txtDestCd) = errorFlg Then
            '    Return errorFlg
            'End If

            ''EDI届先コード
            '.txtEDICd.ItemName = .lblEDICd.Text
            '.txtEDICd.IsForbiddenWordsCheck = chkFlg
            '.txtEDICd.IsByteCheck = 20
            'If MyBase.IsValidateCheck(.txtEDICd) = errorFlg Then
            '    Return errorFlg
            'End If

            ''届先名
            '.txtDestNm.ItemName = .lblDestNm.Text
            '.txtDestNm.IsHissuCheck = chkFlg
            '.txtDestNm.IsForbiddenWordsCheck = chkFlg
            '.txtDestNm.IsByteCheck = 80
            'If MyBase.IsValidateCheck(.txtDestNm) = errorFlg Then
            '    Return errorFlg
            'End If

            ''要望番号:1330 terakawa 2012.08.09 Start
            ''届先カナ名
            '.txtKanaNm.ItemName = .lblKanaNm.Text
            '.txtKanaNm.IsForbiddenWordsCheck = chkFlg
            '.txtKanaNm.IsByteCheck = 40
            'If MyBase.IsValidateCheck(.txtKanaNm) = errorFlg Then
            '    Return errorFlg
            'End If
            ''要望番号:1330 terakawa 2012.08.09 End

            ''郵便番号
            '.txtZip.ItemName = .lblZip.Text
            '.txtZip.IsForbiddenWordsCheck = chkFlg
            '.txtZip.IsByteCheck = 10
            'If MyBase.IsValidateCheck(.txtZip) = errorFlg Then
            '    Return errorFlg
            'End If

            ''住所1
            '.txtAd1.ItemName = .lblAd1.Text
            '.txtAd1.IsForbiddenWordsCheck = chkFlg
            '.txtAd1.IsByteCheck = 40
            'If MyBase.IsValidateCheck(.txtAd1) = errorFlg Then
            '    Return errorFlg
            'End If

            ''住所2
            '.txtAd2.ItemName = .lblAd2.Text
            '.txtAd2.IsForbiddenWordsCheck = chkFlg
            '.txtAd2.IsByteCheck = 40
            'If MyBase.IsValidateCheck(.txtAd2) = errorFlg Then
            '    Return errorFlg
            'End If

            ''住所3
            '.txtAd3.ItemName = .lblAd3.Text
            '.txtAd3.IsForbiddenWordsCheck = chkFlg
            '.txtAd3.IsByteCheck = 40
            'If MyBase.IsValidateCheck(.txtAd3) = errorFlg Then
            '    Return errorFlg
            'End If

            ''顧客運賃纏めコード
            '.txtCustDestCd.ItemName = .lblDicDestCd.Text
            '.txtCustDestCd.IsForbiddenWordsCheck = chkFlg
            '.txtCustDestCd.IsByteCheck = 15
            'If MyBase.IsValidateCheck(.txtCustDestCd) = errorFlg Then
            '    Return errorFlg
            'End If

            ''電話番号
            '.txtTel.ItemName = .lblTel.Text
            '.txtTel.IsForbiddenWordsCheck = chkFlg
            '.txtTel.IsByteCheck = 20
            'If MyBase.IsValidateCheck(.txtTel) = errorFlg Then
            '    Return errorFlg
            'End If

            ''JISコード
            '.txtJIS.ItemName = .lblJIS.Text
            '.txtJIS.IsForbiddenWordsCheck = chkFlg
            '.txtJIS.IsByteCheck = 7
            'If MyBase.IsValidateCheck(.txtJIS) = errorFlg Then
            '    Return errorFlg
            'End If

            'If String.IsNullOrEmpty(.txtJIS.TextValue) = True Then
            '    If MyBase.ShowMessage("W128", New String() {.lblJIS.Text}) <> MsgBoxResult.Ok Then
            '        Me.SetErrorControl(.txtJIS)
            '        Return errorFlg
            '    End If
            'End If


            ''ファックス番号
            '.txtFax.ItemName = .lblFax.Text
            '.txtFax.IsForbiddenWordsCheck = chkFlg
            '.txtFax.IsByteCheck = 20
            'If MyBase.IsValidateCheck(.txtFax) = errorFlg Then
            '    Return errorFlg
            'End If

            ''分析表添付区分
            '.cmbCoaYn.ItemName = .lblCoaYn.Text
            '.cmbCoaYn.IsHissuCheck = chkFlg
            'If MyBase.IsValidateCheck(.cmbCoaYn) = errorFlg Then
            '    Return errorFlg
            'End If

            ''指定運送会社コード
            '.txtSpUnsoCd.ItemName = String.Concat(.grpUnso.Text, "コード")
            '.txtSpUnsoCd.IsForbiddenWordsCheck = chkFlg
            '.txtSpUnsoCd.IsByteCheck = 5
            'If MyBase.IsValidateCheck(.txtSpUnsoCd) = errorFlg Then
            '    Return errorFlg
            'End If

            ''指定運送会社支店コード
            '.txtSpUnsoBrCd.ItemName = String.Concat(.grpUnso.Text, "支店コード")
            '.txtSpUnsoBrCd.IsForbiddenWordsCheck = chkFlg
            '.txtSpUnsoBrCd.IsByteCheck = 3
            'If MyBase.IsValidateCheck(.txtSpUnsoBrCd) = errorFlg Then
            '    Return errorFlg
            'End If

            ''荷卸時間制限
            '.txtCargoTimeLimit.ItemName = .lblCargoTimeLimit.Text
            '.txtCargoTimeLimit.IsForbiddenWordsCheck = chkFlg
            '.txtCargoTimeLimit.IsByteCheck = 40
            'If MyBase.IsValidateCheck(.txtCargoTimeLimit) = errorFlg Then
            '    Return errorFlg
            'End If

            ''大型車輛
            '.cmbLargeCarYn.ItemName = .lblLargeCarYn.Text
            '.cmbLargeCarYn.IsHissuCheck = chkFlg
            'If MyBase.IsValidateCheck(.cmbLargeCarYn) = errorFlg Then
            '    Return errorFlg
            'End If

            ''配送時注意事項
            '.txtDeliAtt.ItemName = .lblDeliAtt.Text
            '.txtDeliAtt.IsForbiddenWordsCheck = chkFlg
            '.txtDeliAtt.IsByteCheck = 100
            'If MyBase.IsValidateCheck(.txtDeliAtt) = errorFlg Then
            '    Return errorFlg
            'End If

            ''START YANAI 要望番号881
            ''備考
            '.txtRemark.ItemName = .lblRemark.Text
            '.txtRemark.IsForbiddenWordsCheck = chkFlg
            '.txtRemark.IsByteCheck = 100
            'If MyBase.IsValidateCheck(.txtRemark) = errorFlg Then
            '    Return errorFlg
            'End If
            ''END YANAI 要望番号881

            ''納品書荷主名義コード
            '.txtSalesCd.ItemName = .lblSales.Text
            '.txtSalesCd.IsForbiddenWordsCheck = chkFlg
            '.txtSalesCd.IsFullByteCheck = 5
            '.txtSalesCd.IsMiddleSpace = chkFlg
            'If MyBase.IsValidateCheck(.txtSalesCd) = errorFlg Then
            '    Return errorFlg
            'End If

            ''売上先コード
            '.txtUriageCd.ItemName = .lblUriage.Text
            '.txtUriageCd.IsForbiddenWordsCheck = chkFlg
            '.txtUriageCd.IsByteCheck = 15
            'If MyBase.IsValidateCheck(.txtUriageCd) = errorFlg Then
            '    Return errorFlg
            'End If

            ''運賃請求先コード
            '.txtUnchinSeiqtoCd.ItemName = .lblUnchinSeiqto.Text
            '.txtUnchinSeiqtoCd.IsForbiddenWordsCheck = chkFlg
            '.txtUnchinSeiqtoCd.IsByteCheck = 7
            'If MyBase.IsValidateCheck(.txtUnchinSeiqtoCd) = errorFlg Then
            '    Return errorFlg
            'End If

            ''運賃タリフコード（屯キロ建）
            '.txtUnchinTariffCd1.ItemName = .lblUnchinTariff1.Text
            '.txtUnchinTariffCd1.IsForbiddenWordsCheck = chkFlg
            '.txtUnchinTariffCd1.IsByteCheck = 10
            'If MyBase.IsValidateCheck(.txtUnchinTariffCd1) = errorFlg Then
            '    Return errorFlg
            'End If

            ''運賃タリフコード（車建）
            '.txtUnchinTariffCd2.ItemName = .lblUnchinTariff2.Text
            '.txtUnchinTariffCd2.IsForbiddenWordsCheck = chkFlg
            '.txtUnchinTariffCd2.IsByteCheck = 10
            'If MyBase.IsValidateCheck(.txtUnchinTariffCd2) = errorFlg Then
            '    Return errorFlg
            'End If

            ''割増運賃タリフコード
            '.txtExtcTariffCd.ItemName = .lblExtcTariff.Text
            '.txtExtcTariffCd.IsForbiddenWordsCheck = chkFlg
            '.txtExtcTariffCd.IsByteCheck = 10
            'If MyBase.IsValidateCheck(.txtExtcTariffCd) = errorFlg Then
            '    Return errorFlg
            'End If

            ''横持ち運賃タリフコード
            '.txtYokoTariffCd.ItemName = .lblYokoTariff.Text
            '.txtYokoTariffCd.IsForbiddenWordsCheck = chkFlg
            '.txtYokoTariffCd.IsByteCheck = 10
            'If MyBase.IsValidateCheck(.txtYokoTariffCd) = errorFlg Then
            '    Return errorFlg
            'End If

        End With

        Return True

    End Function

 
 

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveCheck(ByVal frm As LMI220F) As Boolean

        '削除フラグチェック
        Dim rtnReutl As Boolean = Me.IsRecordStatusChk(frm)

        '未選択チェック
        'rtnReutl = Me.IsSprNullChk("E392")

        If rtnReutl = False Then
            'frm.sprDetail2.Focus()
        End If

        Return rtnReutl

    End Function



#Region "IsRecordStatusChk"

    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRecordStatusChk(ByVal frm As LMI220F) As Boolean

        If frm.lblSituation.RecordStatus = RecordStatus.DELETE_REC Then
            MyBase.ShowMessage("E035")
            Return False
        End If

        Return True

    End Function

#End Region



    ''' <summary>
    ''' 他営業所チェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMI220F, ByVal eventShubetsu As LMI220C.EventShubetsu) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        'ユーザーのログイン営業所と異なる場合エラー
        'If frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
        '    Dim msg As String = String.Empty

        '    Select Case eventShubetsu

        '        Case LMI220C.EventShubetsu.HENSHU
        '            msg = "編集"

        '        Case LMI220C.EventShubetsu.SAKUJO_HUKKATSU
        '            msg = "削除・復活"

        '    End Select

        '    MyBase.ShowMessage("E178", New String() {msg})
        '    Return False

        'End If

        Return True

    End Function

    '''' <summary>
    '''' スプレッドでチェックの付いたRowIndexを取得
    '''' </summary>
    '''' <returns>リスト</returns>
    '''' <remarks>チェックのある行のRowIndexをリストに入れて戻す</remarks>
    'Friend Function SprSelectCount() As ArrayList

    '    Dim defNo As Integer = LMI220G.sprDetailDef.DEF.ColNo

    '    With Me._Frm.sprDetail.ActiveSheet

    '        Dim list As ArrayList = New ArrayList()
    '        Dim max As Integer = .Rows.Count - 1

    '        For i As Integer = 0 To max
    '            If Me._Vcon.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
    '                '選択されたRowIndexを設定
    '                list.Add(i)
    '            End If
    '        Next

    '        Return list

    '    End With

    'End Function


#Region "IsAuthorityChk"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI220C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI220C.EventShubetsu.SHINKI           '新規
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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMI220C.EventShubetsu.HENSHU          '編集
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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMI220C.EventShubetsu.SAKUJO_HUKKATSU         '削除・復活
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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMI220C.EventShubetsu.KENSAKU        '検索
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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMI220C.EventShubetsu.HOZON           '保存
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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMI220C.EventShubetsu.CLOSE           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMI220C.EventShubetsu.DOUBLECLICK         'ダブルクリック
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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMI220C.EventShubetsu.TORIKOMI_KOSHIN           '取込更新
                '10:閲覧者の場合エラー
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
                        kengenFlg = True
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

#End Region

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As String) As Boolean

        ''フォーカス位置がない場合、スルー
        'If objNm Is Nothing = True Then
        '    '検証結果(メモ)№120対応(2011.09.14)
        '    'マスタ参照の場合、エラーメッセージ設定
        '    If actionType.Equals(LMI220C.EventShubetsu.MASTEROPEN) = True Then
        '        Call Me._Vcon.SetFocusErrMessage(False)
        '    End If
        '    Return False
        'End If

        '判定するコントロール設定先変数
        Dim ctl As Win.InputMan.LMImTextBox() = Nothing
        Dim msg As String() = Nothing
        Dim clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl() = Nothing

        With Me._Frm

            'Select Case objNm

            '    Case .txtCustCdL.Name

            '        Dim custNm As String = .lblCustL.Text
            '        ctl = New Win.InputMan.LMImTextBox() {.txtCustCdL}
            '        msg = New String() {String.Concat(custNm, LMMControlC.L_NM, LMMControlC.CD)}
            '        clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblCustNmL}

            '    Case .txtZip.Name

            '        ctl = New Win.InputMan.LMImTextBox() {.txtZip}
            '        msg = New String() {.lblZip.Text}

            '    Case .txtCustDestCd.Name

            '        ctl = New Win.InputMan.LMImTextBox() {.txtCustDestCd}
            '        msg = New String() {.lblDicDestCd.Text}

            '    Case .txtJIS.Name

            '        ctl = New Win.InputMan.LMImTextBox() {.txtJIS}
            '        msg = New String() {.lblJIS.Text}

            '    Case .txtSpUnsoCd.Name, .txtSpUnsoBrCd.Name

            '        ctl = New Win.InputMan.LMImTextBox() {.txtSpUnsoCd, .txtSpUnsoBrCd}
            '        msg = New String() {String.Concat(.grpUnso.Text, LMMControlC.CD) _
            '                            , String.Concat(.grpUnso.Text, LMMControlC.BR_CD)}
            '        clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblSpUnsoNm}

            '    Case .txtSalesCd.Name

            '        ctl = New Win.InputMan.LMImTextBox() {.txtSalesCd}
            '        msg = New String() {.lblSales.Text}
            '        clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblSalesNm}

            '    Case .txtUriageCd.Name

            '        ctl = New Win.InputMan.LMImTextBox() {.txtUriageCd}
            '        msg = New String() {.lblUriage.Text}
            '        clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblUriageNm}

            '    Case .txtUnchinSeiqtoCd.Name

            '        ctl = New Win.InputMan.LMImTextBox() {.txtUnchinSeiqtoCd}
            '        msg = New String() {.lblUnchinSeiqto.Text}
            '        clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblUnchinSeiqtoNm}

            '    Case .txtUnchinTariffCd1.Name

            '        ctl = New Win.InputMan.LMImTextBox() {.txtUnchinTariffCd1}
            '        msg = New String() {.lblUnchinTariff1.Text}
            '        clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblUnchinTariffRem1}

            '    Case .txtUnchinTariffCd2.Name

            '        ctl = New Win.InputMan.LMImTextBox() {.txtUnchinTariffCd2}
            '        msg = New String() {.lblUnchinTariff2.Text}
            '        clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblUnchinTariffRem2}

            '    Case .txtExtcTariffCd.Name

            '        ctl = New Win.InputMan.LMImTextBox() {.txtExtcTariffCd}
            '        msg = New String() {.lblExtcTariff.Text}
            '        clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblExtcTariffRem}

            '    Case .txtYokoTariffCd.Name

            '        ctl = New Win.InputMan.LMImTextBox() {.txtYokoTariffCd}
            '        msg = New String() {.lblYokoTariff.Text}
            '        clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblYokoTariffRem}

            'End Select

            'Dim focusCtl As Control = Me._Frm.ActiveControl
            'Return Me._Vcon.IsFocusChk(actionType, ctl, msg, focusCtl, clearCtl)

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

    '''' <summary>
    '''' セルから値を取得
    '''' </summary>
    '''' <param name="aCell">セル</param>
    '''' <returns>取得した値</returns>
    '''' <remarks></remarks>
    'Friend Function GetCellValue(ByVal aCell As Cell) As String

    '    GetCellValue = String.Empty

    '    If TypeOf aCell.Editor Is CellType.ComboBoxCellType Then

    '        'コンボボックスの場合、Value値を返却
    '        If aCell.Value Is Nothing = False Then
    '            GetCellValue = aCell.Value.ToString()
    '        End If

    '    ElseIf TypeOf aCell.Editor Is CellType.CheckBoxCellType Then

    '        'チェックボックスの場合、Booleanの値をStringに変換
    '        If aCell.Text.Equals("True") = True Then
    '            GetCellValue = LMConst.FLG.ON
    '        ElseIf aCell.Text.Equals("False") = True Then
    '            GetCellValue = LMConst.FLG.OFF
    '        Else
    '            GetCellValue = aCell.Text
    '        End If

    '    ElseIf TypeOf aCell.Editor Is CellType.NumberCellType Then

    '        'ナンバーの場合、Value値を返却
    '        If aCell.Value Is Nothing = False Then
    '            GetCellValue = aCell.Value.ToString()
    '        Else
    '            GetCellValue = 0.ToString()
    '        End If

    '    Else

    '        'テキストの場合、Trimした値を返却
    '        GetCellValue = aCell.Text.Trim()

    '    End If

    '    Return GetCellValue

    'End Function

#End Region

#End Region 'Method

End Class
