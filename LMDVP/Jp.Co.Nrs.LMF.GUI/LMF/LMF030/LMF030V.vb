' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF030V : 運行情報入力
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF030Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMF030V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF030F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMFControlV

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMFControlG

    ''' <summary>
    ''' LMF020Gクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMF030G

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF030F, ByVal v As LMFControlV, ByVal g As LMF030G, ByVal ctlG As LMFControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

        Me._Gcon = ctlG

        Me._G = g

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
    Friend Function IsInputCheck(ByVal ds As DataSet) As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        Dim rtnResult As Boolean = Me.IsHeaderChk()

        'マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsMstExistChk()

        '関連チェック
        rtnResult = rtnResult AndAlso Me.IsConnectionChk(ds)

        'ワーニングチェック
        rtnResult = rtnResult AndAlso Me.IsWarningChk()

        Return rtnResult

    End Function

    '要望番号2063 追加START 2015.05.27
    ''' <summary>
    ''' モードチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsTehaiCheck(ByVal mode As String, ByVal Status As String) As Boolean

        With Me._Frm

            '手配種別
            '2016.01.06 UMANO 英語化対応START
            '.cmbTehaisyubetsu.ItemName = "手配種別"
            .cmbTehaisyubetsu.ItemName = "手配種別(Arrangement Classification)"
            '2016.01.06 UMANO 英語化対応END
            .cmbTehaisyubetsu.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbTehaisyubetsu) = False Then
                Return False
            End If

            '表示モード
            If .lblSituation.DispMode.Equals(mode) = True Then
                Call Me._Vcon.SetErrorControl(.btnTehaiCreate)
                Return Me._Vcon.SetErrMessage("E028", New String() {.lblSituation.Text, .btnTehaiCreate.Text})
            End If

            Return True

        End With

    End Function
    '要望番号2063 追加END 2015.05.27

    ''' <summary>
    ''' ヘッダ項目の単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHeaderChk() As Boolean

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '便区分
            .cmbBinKbn.ItemName = .lblTitleBinKbn.Text
            .cmbBinKbn.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbBinKbn) = errorFlg Then
                Return errorFlg
            End If

            '車番
            .txtCarNo.ItemName = .lblTitleCarNo.Text
            'START YANAI 要望番号1268 必須チェックはずし
            '.txtCarNo.IsHissuCheck = chkFlg
            'END YANAI 要望番号1268 必須チェックはずし
            .txtCarNo.IsForbiddenWordsCheck = chkFlg
            .txtCarNo.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtCarNo) = errorFlg Then
                Return errorFlg
            End If

            '運送会社コード
            .txtUnsocoCd.ItemName = String.Concat(.lblTitleUnsoco.Text, LMFControlC.CD)
            .txtUnsocoCd.IsHissuCheck = chkFlg
            .txtUnsocoCd.IsForbiddenWordsCheck = chkFlg
            .txtUnsocoCd.IsByteCheck = 5
            .txtUnsocoCd.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtUnsocoCd) = errorFlg Then
                Return errorFlg
            End If

            '運送会社支店コード
            .txtUnsocoBrCd.ItemName = String.Concat(.lblTitleUnsoco.Text, LMFControlC.BR_CD)
            .txtUnsocoBrCd.IsHissuCheck = chkFlg
            .txtUnsocoBrCd.IsForbiddenWordsCheck = chkFlg
            .txtUnsocoBrCd.IsByteCheck = 3
            .txtUnsocoBrCd.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtUnsocoBrCd) = errorFlg Then
                Return errorFlg
            End If

            '乗務員コード
            .txtDriverCd.ItemName = String.Concat(.lblTitleDriver.Text, LMFControlC.BR_CD)
            .txtDriverCd.IsForbiddenWordsCheck = chkFlg
            .txtDriverCd.IsByteCheck = 5
            .txtDriverCd.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtDriverCd) = errorFlg Then
                Return errorFlg
            End If

            '自傭車区分
            .cmbJshaKbn.ItemName = .lblTitleJshaKbn.Text
            .cmbJshaKbn.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbJshaKbn) = errorFlg Then
                Return errorFlg
            End If

            '備考
            .txtRem.ItemName = String.Concat(.lblTitleRem.Text, LMFControlC.BR_CD)
            .txtRem.IsForbiddenWordsCheck = chkFlg
            .txtRem.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtRem) = errorFlg Then
                Return errorFlg
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMstExistChk() As Boolean

        '車輌マスタ存在チェック
        Dim rtnResult As Boolean = Me.IsCarExistChk()

        '運送会社マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsUnsocoExistChk()

        '乗務員マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsDriverExistChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 車輌マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsCarExistChk() As Boolean

        With Me._Frm

            Dim carNo As String = .txtCarNo.TextValue

            '値がない場合、スルー
            If String.IsNullOrEmpty(carNo) = True Then
                Return True
            End If

            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectCarListDataRow(drs, carNo, .lblCarKey.TextValue) = False Then
                Call Me._Vcon.SetErrorControl(.txtCarNo)
                Return False
            End If

            '複数ある場合、エラー
            If 1 < drs.Length Then
                Call Me._Vcon.SetErrorControl(.txtCarNo)
                '2016.01.06 UMANO 英語化対応START
                'Return Me._Vcon.SetErrMessage("E206", New String() {.lblTitleCarNo.Text, LMF030C.CAR_KEY})
                Return Me._Vcon.SetErrMessage("E856", New String() {.lblTitleCarNo.Text})
                '2016.01.06 UMANO 英語化対応END
            End If

            'マスタ情報を反映
            .lblCarKey.TextValue = drs(0).Item("CAR_KEY").ToString()
            .lblCarType.TextValue = drs(0).Item("CAR_TP_KB_NM").ToString()
            .numOndoMx.Value = drs(0).Item("ONDO_MX").ToString()
            .numOndoMm.Value = drs(0).Item("ONDO_MM").ToString()
            .numLoadWt.Value = drs(0).Item("LOAD_WT").ToString()
            .lblSyakenTruck.TextValue = drs(0).Item("INSPC_DATE_TRUCK").ToString()
            .lblSyakenTrailer.TextValue = drs(0).Item("INSPC_DATE_TRAILER").ToString()
            Return True

        End With

    End Function

    ''' <summary>
    ''' 運送会社マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnsocoExistChk() As Boolean

        With Me._Frm

            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectUnsocoListDataRow(drs, .cmbEigyo.SelectedValue.ToString(), .txtUnsocoCd.TextValue, .txtUnsocoBrCd.TextValue) = False Then
                .txtUnsocoBrCd.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Call Me._Vcon.SetErrorControl(.txtUnsocoCd)
                Return False
            End If

            'マスタの値を設定
            .txtUnsocoCd.TextValue = drs(0).Item("UNSOCO_CD").ToString()
            .txtUnsocoBrCd.TextValue = drs(0).Item("UNSOCO_BR_CD").ToString()
            .lblUnsocoNm.TextValue = Me._Gcon.EditConcatData(drs(0).Item("UNSOCO_NM").ToString(), drs(0).Item("UNSOCO_BR_NM").ToString(), Space(1))

            Return True

        End With

    End Function

    ''' <summary>
    ''' 乗務員マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDriverExistChk() As Boolean

        With Me._Frm

            Dim cd As String = .txtDriverCd.TextValue

            '値がない場合、スルー
            If String.IsNullOrEmpty(cd) = True Then
                Return True
            End If

            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectDriverListDataRow(drs, cd) = False Then
                Call Me._Vcon.SetErrorControl(.txtDriverCd)
                Return False
            End If

            'マスタの値を設定
            .txtDriverCd.TextValue = drs(0).Item("DRIVER_CD").ToString()
            .lblDriverNm.TextValue = drs(0).Item("DRIVER_NM").ToString()

            Return True

        End With

    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsConnectionChk(ByVal ds As DataSet) As Boolean

        '配送区分の関連必須チェック
        Dim rtnResult As Boolean = Me.IsHaisoKbnHissuChk(ds)

        '同値チェック
        rtnResult = rtnResult AndAlso Me.IsDotiChk()

        '別運行番号チェック
        rtnResult = rtnResult AndAlso Me.IsBetuTripNoChk(ds)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 配送区分の必須チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHaisoKbnHissuChk(ByVal ds As DataSet) As Boolean

        '値がある場合、スルー
        If String.IsNullOrEmpty(Me._Frm.cmbHaiso.SelectedValue.ToString()) = False Then
            Return True
        End If

        '中継配送するレコードを取得
        Dim drs As DataRow() = ds.Tables(LMF030C.TABLE_NM_UNSO_L).Select(" SYS_DEL_FLG = '0' AND TYUKEI_HAISO_FLG = '01' ")

        '中継配送するレコードがない場合、スルー
        If drs.Length < 1 Then
            Return True
        End If

        Call Me._Vcon.SetErrorControl(Me._Frm.cmbHaiso)
        Return Me._Vcon.SetErrMessage("E247")

    End Function

    ''' <summary>
    ''' 納入日 , 運送会社の同値チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDotiChk() As Boolean

        Dim spr As Win.Spread.LMSpread = Me._Frm.sprDetail
        Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
        Dim arr As ArrayList = New ArrayList()
        For i As Integer = 0 To max
            arr.Add(i)
        Next

        Return Me._Vcon.IsDotiChk(spr, arr, LMF030G.sprDetailDef.NONYUDATE.ColNo, LMF030G.sprDetailDef.UNSO_CD.ColNo, LMF030G.sprDetailDef.UNSO_BR_CD.ColNo, False)

    End Function

    ''' <summary>
    ''' 別の運行に紐付いているかをチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function IsBetuTripNoChk(ByVal ds As DataSet) As Boolean

        With Me._Frm.sprDetail.ActiveSheet

            Dim max As Integer = .Rows.Count - 1

            '明細がない場合、スルー
            If max < 0 Then
                Return True
            End If

            '編集時の考慮
            '前回の配送区分と違う場合、運行番号に値があるのがエラー
            '前回の配送区分と同じ場合、画面の運行番号と違う場合、エラー
            '新規時はどちらのロジックでもよい
            Dim haisoKbn As String = Me._Frm.cmbHaiso.SelectedValue.ToString()
            Dim joken As String = String.Concat(" <> '", Me._Frm.lblTripNo.TextValue, "' ")
            If haisoKbn.Equals(ds.Tables(LMF030C.TABLE_NM_UNSO_LL).Rows(0).Item("HAISO_KB").ToString()) = False Then
                joken = String.Concat(" <> '' ")
            End If

            '画面の運行番号と違う運行番号が設定されている場合、エラー(中継配送有のみ)
            Dim colNm As String = Me.SetTripColNm(Me._Frm.cmbHaiso.SelectedValue.ToString())

            '列名がない場合、スルー
            If String.IsNullOrEmpty(colNm) = True Then
                Return True
            End If

            '新規、更新は気にしないで判定
            Dim sql As String = String.Concat(" SYS_DEL_FLG = '0' AND TYUKEI_HAISO_FLG = '01' AND ", colNm, joken)
            Dim drs As DataRow() = ds.Tables(LMF030C.TABLE_NM_UNSO_L).Select(sql)

            'レコードが存在する場合、エラー
            If 0 < drs.Length Then
                Return Me._Vcon.SetErrMessage("E246")
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' ワーニングチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsWarningChk() As Boolean

        '車検チェック
        Dim rtnResult As Boolean = Me.IsShakenChk()

        '重量チェック
        rtnResult = rtnResult AndAlso Me.IsJuryoChk()

        '温度チェック
        rtnResult = rtnResult AndAlso Me.IsOndoChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 車検チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsShakenChk() As Boolean

        With Me._Frm

            '運行日がない場合、スルー
            Dim tripDate As String = DateFormatUtility.DeleteSlash(.lblTripDate.TextValue)
            If String.IsNullOrEmpty(tripDate) = True Then
                Return True
            End If

            '車検日
            Dim shakenDate As String = Me.GetShakenDate()

            '値がない場合、スルー
            If String.IsNullOrEmpty(shakenDate) = True Then
                Return True
            End If

            '期限切れかをチェック
            Return Me.IsShakenChk(tripDate, shakenDate)

        End With

    End Function

    ''' <summary>
    ''' 車検期限日を取得
    ''' </summary>
    ''' <returns>車検期限日</returns>
    ''' <remarks></remarks>
    Private Function GetShakenDate() As String

        With Me._Frm

            '車検日(トラック)に値がない場合、車検日(トレーラー)を返却
            Dim truckDate As String = .lblSyakenTruck.TextValue
            Dim trailerDate As String = .lblSyakenTrailer.TextValue
            If String.IsNullOrEmpty(truckDate) = True Then
                Return trailerDate
            End If

            '車検日(トレーラー)に値がない場合、車検日(トラック)を返却
            If String.IsNullOrEmpty(trailerDate) = True Then
                Return truckDate
            End If

            '大小比較して小さいほうを返却
            If truckDate < trailerDate Then
                Return truckDate
            End If

            Return trailerDate

        End With

    End Function

    ''' <summary>
    ''' 車検日チェック
    ''' </summary>
    ''' <param name="tripDate">運行日</param>
    ''' <param name="shakenDate">車検日</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsShakenChk(ByVal tripDate As String, ByVal shakenDate As String) As Boolean

        '車検日がない場合、スルー
        If String.IsNullOrEmpty(shakenDate) = True Then
            Return True
        End If

        '車検期限 <= 運行日の場合、ワーニング
        If shakenDate <= tripDate Then
            Return Me._Vcon.IsWarningChk(MyBase.ShowMessage("W141"))
        End If

        Return True

    End Function

    ''' <summary>
    ''' 重量チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsJuryoChk() As Boolean

        With Me._Frm

            Dim sekisai As Decimal = Convert.ToDecimal(Me._Gcon.FormatNumValue(.numLoadWt.Value.ToString()))

            '値が0の場合、スルー
            If 0 = sekisai Then
                Return True
            End If

            '積載重量より積荷の方が重い場合、ワーニング
            If sekisai < Convert.ToDecimal(Me._Gcon.FormatNumValue(.numTripWt.Value.ToString())) Then

                Return Me._Vcon.IsWarningChk(MyBase.ShowMessage("W142"))

            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 温度チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsOndoChk() As Boolean

        With Me._Frm

            Dim rtnResult As Boolean = True

            If Me.IsOndoChk(.numUnsoOndo, .numOndoMm) = False _
                OrElse Me.IsOndoChk(.numOndoMx, .numUnsoOndo) = False Then

                rtnResult = Me._Vcon.IsWarningChk(MyBase.ShowMessage("W143"))

            End If

            '「いいえ」押下の場合、エラー設定
            If rtnResult = False Then
                Me._Vcon.SetErrorControl(.numUnsoOndo)
            End If

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 温度チェック
    ''' </summary>
    ''' <param name="ctl1">コントロール1</param>
    ''' <param name="ctl2">コントロール2</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks>
    ''' テキストがない場合、スルー
    ''' ctl2 ＜ ctl1 の場合、エラー
    ''' </remarks>
    Private Function IsOndoChk(ByVal ctl1 As Win.InputMan.LMImNumber, ByVal ctl2 As Win.InputMan.LMImNumber) As Boolean

        'テキストがない場合、スルー
        If String.IsNullOrEmpty(ctl1.TextValue) = True Then
            Return True
        End If

        'テキストがない場合、スルー
        If String.IsNullOrEmpty(ctl2.TextValue) = True Then
            Return True
        End If

        'ctl2 < ctl1 の場合、エラー
        If Convert.ToDecimal(ctl2.Value.ToString) < Convert.ToDecimal(ctl1.Value.ToString()) Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthority(ByVal actionType As LMF030C.ActionType) As Boolean

        Dim kengen As String = LMUserInfoManager.GetAuthoLv()
        Dim kengenFlg As Boolean = True

        Select Case actionType

            Case LMF030C.ActionType.EDIT

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMF030C.ActionType.DELETE


                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMF030C.ActionType.MASTEROPEN


                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMF030C.ActionType.ENTER

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMF030C.ActionType.SAVE

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMF030C.ActionType.CLOSE

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = True
                End Select

            Case LMF030C.ActionType.ADD

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMF030C.ActionType.DEL

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMF030C.ActionType.DOUBLECLICK

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = True
                End Select

                'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            Case LMF030C.ActionType.SHIHARAIKEISAN

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = True
                End Select
                'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

                '要望番号2063 追加START 2015.05.27
            Case LMF030C.ActionType.TEHAICREATE

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = True
                End Select
                '要望番号2063 追加END 2015.05.27

        End Select

        Return Me._Vcon.IsAuthorityChk(kengenFlg)

    End Function

    ''' <summary>
    ''' 自営業チェック
    ''' </summary>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMyNrsBrChk(ByVal msg As String) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        ''自営業でない場合、エラー
        'If LMUserInfoManager.GetNrsBrCd().Equals(Me._Frm.cmbEigyo.SelectedValue.ToString()) = False Then
        '    Return Me._Vcon.SetErrMessage("E178", New String() {msg})
        'End If

        Return True

    End Function

    ''' <summary>
    ''' オーバーフローチェック
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="minData">最小値</param>
    ''' <param name="maxData">最大値</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsCalcOver(ByVal value As String, ByVal minData As String, ByVal maxData As String, ByVal msg As String) As Boolean

        '上限チェック
        If Me._Vcon.IsCalcOver(value, minData, maxData) = False Then
            Return Me._Vcon.SetErrMessage("E117", New String() {msg, maxData})
        End If

        Return True

    End Function

    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
    ''' <summary>
    ''' 支払運賃確定チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFixShiharaiChk(ByVal ds As DataSet) As Boolean

        '支払運賃確定の場合、エラー
        Dim shiharaiDr() As DataRow = Nothing
        shiharaiDr = ds.Tables(LMF030C.TABLE_NM_SHIHARAI).Select("SHIHARAI_FIXED_FLAG = '01'")

        If shiharaiDr.Count > 0 Then
            Return Me._Vcon.SetErrMessage("E497", New String() {String.Empty})
        End If

        Return True

    End Function
    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  


    ''' <summary>
    ''' 編集時のチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsEditChk() As Boolean

        Return Me.IsMyNrsBrChk(Me._Vcon.SetRepMsgData(Me._Frm.FunctionKey.F2ButtonName))

    End Function

    ''' <summary>
    ''' 削除時のチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsDeleteChk() As Boolean

        Return Me.IsMyNrsBrChk(Me._Vcon.SetRepMsgData(Me._Frm.FunctionKey.F4ButtonName))

    End Function

    ''' <summary>
    ''' 行削除時のチェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRowDelChk(ByVal arr As ArrayList) As Boolean

        '未選択チェック
        Return Me._Vcon.IsSelectChk(arr.Count)

    End Function

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 計算時のチェック(画面項目)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsKeisanCheck(ByVal ds As dataset) As Boolean

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            Dim spr As Win.Spread.LMSpread = .sprDetail
            Dim max As Integer = spr.ActiveSheet.Rows.Count - 1

            '支払運賃データチェック
            If ds.Tables(LMF030C.TABLE_NM_SHIHARAI).Rows.Count = 0 Then
                MyBase.ShowMessage("E500")
                Return errorFlg
            End If

            '対象データチェック(上部の支払運賃データでエラーになり、基本的にここのチェックでエラーになるということはないはず。）
            If max < 0 Then
                MyBase.ShowMessage("E024")
                Return errorFlg
            End If

            '支払運賃計算コンボ
            .cmbShiharai.ItemName = .lblTitleShiharai.Text
            .cmbShiharai.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbShiharai) = errorFlg Then
                Return errorFlg
            End If

            '支払運賃計算コンボが"件数加算クリア"の場合、以降のチェックは行わない
            If ("02").Equals(.cmbShiharai.SelectedValue) = True Then
                Return True
            End If


            '支払タリフ分類コンボ
            .cmbTariffKbn.ItemName = .lblTitleTariffKb.Text
            .cmbTariffKbn.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbTariffKbn) = errorFlg Then
                Return errorFlg
            End If

            '金額
            .numKingaku.ItemName = .lblTitleKingaku.Text
            .numKingaku.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.numKingaku) = errorFlg Then
                Return errorFlg
            End If

            '支払重量
            .numShiharaiWt.ItemName = .lblTitleShiharaiWt.Text
            .numShiharaiWt.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.numShiharaiWt) = errorFlg Then
                Return errorFlg
            End If

            '支払件数
            .numKensu.ItemName = .lblTitleShiharaiKensu.Text
            .numKensu.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.numKensu) = errorFlg Then
                Return errorFlg
            End If

            '支払タリフ分類
            .cmbShiharai.ItemName = .lblTitleShiharai.Text
            .cmbShiharai.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbShiharai) = errorFlg Then
                Return errorFlg
            End If

            '支払タリフコード
            .txtTariffCd.ItemName = String.Concat(.lblTitleTariff.Text, LMFControlC.CD)
            .txtTariffCd.IsHissuCheck = chkFlg
            .txtTariffCd.IsForbiddenWordsCheck = chkFlg
            .txtTariffCd.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtTariffCd) = errorFlg Then
                Return errorFlg
            End If

            '支払割増タリフコード
            .txtExtcCd.ItemName = String.Concat(.lblTitleExtc.Text, LMFControlC.CD)
            .txtExtcCd.IsForbiddenWordsCheck = chkFlg
            .txtExtcCd.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtExtcCd) = errorFlg Then
                Return errorFlg
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 計算時のチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsKeisanDataSetCheck(ByVal ds As DataSet) As Boolean

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False
            Dim max As Integer = ds.Tables(LMF030C.TABLE_NM_IN_KEISAN).Rows.Count - 1

            For i As Integer = 0 To max
                '個数の上限チェック
                '2016.01.06 UMANO 英語化対応 未修正
                If Me.IsCalcOver(ds.Tables(LMF030C.TABLE_NM_IN_KEISAN).Rows(i).Item("SHIHARAI_UNCHIN").ToString, LMFControlC.MIN_0, Convert.ToString(LMFControlC.MAX_KETA_KINGAKU), "計算後の支払運賃") = False Then
                    Return False
                End If
            Next

            Return True

        End With

    End Function
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMF030C.ActionType) As Boolean

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            'ポップ対象外の場合
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMF030C.ActionType.MASTEROPEN) = True Then
                Call Me._Vcon.SetFocusErrMessage(False)
            End If
            Return False
        End If

        '判定するコントロール設定先変数
        Dim txtCtl As Win.InputMan.LMImTextBox() = Nothing
        Dim lblCtl As Control() = Nothing
        Dim msg As String() = Nothing

        With Me._Frm

            Select Case objNm

                Case .txtCarNo.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtCarNo}
                    '2022.09.06 修正START
                    'lblCtl = New Control() { .lblCarType, .lblCarKey, .numOndoMx, .numOndoMm, .numLoadWt, .lblSyakenTruck, .lblSyakenTrailer}
                    lblCtl = New Control() { .lblCarType, .lblCarKey, .numOndoMx, .numOndoMm, .numLoadWt, .lblSyakenTruck, .lblSyakenTrailer, .numPayAmt}
                    '2022.09.06 修正END
                    msg = New String() {String.Concat(.lblTitleCarNo.Text, LMFControlC.CD)}

                Case .txtUnsocoCd.Name, .txtUnsocoBrCd.Name

                    Dim unsoNm As String = .lblTitleUnsoco.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtUnsocoCd, .txtUnsocoBrCd}
                    lblCtl = New Control() {.lblUnsocoNm}
                    msg = New String() {String.Concat(unsoNm, LMFControlC.CD), String.Concat(unsoNm, LMFControlC.BR_CD)}

                Case .txtDriverCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtDriverCd}
                    lblCtl = New Control() {.lblDriverNm}
                    msg = New String() {String.Concat(.lblTitleDriver.Text, LMFControlC.CD)}

                    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
                Case .txtTariffCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtTariffCd}
                    lblCtl = New Control() {.lblTariffNm}
                    msg = New String() {String.Concat(.lblTariffNm.Text, LMFControlC.CD)}

                Case .txtExtcCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtExtcCd}
                    lblCtl = New Control() {.lblExtcNm}
                    msg = New String() {String.Concat(.lblExtcNm.Text, LMFControlC.CD)}
                    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

            End Select

            'START YANAI 要望番号529
            ''フォーカス位置チェック
            'Dim rtnResult As Boolean = Me._Vcon.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)
            'END YANAI 要望番号529

        End With

        'START YANAI 要望番号529
        'フォーカス位置チェック
        Dim rtnResult As Boolean = Me._Vcon.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)

        Return rtnResult
        'END YANAI 要望番号529

    End Function

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm

            .txtCarNo.TextValue = .txtCarNo.TextValue.Trim()
            .txtUnsocoCd.TextValue = .txtUnsocoCd.TextValue.Trim()
            .txtUnsocoBrCd.TextValue = .txtUnsocoBrCd.TextValue.Trim()
            .txtRem.TextValue = .txtRem.TextValue.Trim()

        End With

    End Sub

    ''' <summary>
    ''' 配送区分による設定先列名を取得
    ''' </summary>
    ''' <param name="haisoKbn">配送区分</param>
    ''' <returns>列名</returns>
    ''' <remarks></remarks>
    Friend Function SetTripColNm(ByVal haisoKbn As String) As String

        SetTripColNm = String.Empty

        Select Case haisoKbn

            Case LMFControlC.HAISO_SHUKA

                SetTripColNm = "TRIP_NO_SYUKA"

            Case LMFControlC.HAISO_THUKEI

                SetTripColNm = "TRIP_NO_TYUKEI"

            Case LMFControlC.HAISO_HAIKA

                SetTripColNm = "TRIP_NO_HAIKA"

        End Select

        Return SetTripColNm

    End Function

#End Region 'Method

End Class
