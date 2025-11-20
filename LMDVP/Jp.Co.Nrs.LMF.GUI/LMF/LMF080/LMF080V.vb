' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF080V : 支払検索
'  作  成  者       :  YANAI
' ==========================================================================
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMF080Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMF080V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF080F

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

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF080F, ByVal v As LMFControlV, ByVal g As LMFControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

        Me._Gcon = g

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
    Friend Function IsInputCheck() As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        Dim rtnResult As Boolean = Me.IsHeaderChk()
        rtnResult = rtnResult AndAlso Me.IsSprChk()

        '関連チェック
        rtnResult = rtnResult AndAlso Me.IsInputConnectionChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' ヘッダ項目の単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHeaderChk() As Boolean

        With Me._Frm

            '営業所
            Dim rtnResult As Boolean = Me.IsNrsHissuChk()

            '支払タリフコード
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtTariffCd, String.Concat(.lblTitleTariff.Text, LMFControlC.CD), 10)

            '支払割増タリフコード
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtExtcCd, String.Concat(.lblTitleExtc.Text, LMFControlC.CD), 10)

            '運送会社コード
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtUnsocoCd, String.Concat(.lblTitleUnsoco.Text, LMFControlC.CD), 5)

            '運送会社支店コード
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtUnsocoBrCd, String.Concat(.lblTitleUnsoco.Text, LMFControlC.BR_CD), 3)

            '荷主コード
            rtnResult = rtnResult AndAlso Me.IsInputCustCd()

            '届先コード
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtDestCd, String.Concat(.lblTitleDest.Text, LMFControlC.CD), 15)

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 荷主コードの単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputCustCd() As Boolean

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            Dim msg As String = .lblTitleCust.Text

            '荷主コード(大)
            .txtCustCdL.ItemName = String.Concat(msg, LMFControlC.L_NM, LMFControlC.CD)
            .txtCustCdL.IsForbiddenWordsCheck = chkFlg
            .txtCustCdL.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCdL) = errorFlg Then
                Return errorFlg
            End If

            '荷主コード(中)
            .txtCustCdM.ItemName = String.Concat(msg, LMFControlC.M_NM, LMFControlC.CD)
            .txtCustCdM.IsForbiddenWordsCheck = chkFlg
            .txtCustCdM.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCdM) = errorFlg Then
                Return errorFlg
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' スプレッドの単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSprChk() As Boolean

        Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprDetail)

        '荷主名
        Dim rtnResult As Boolean = Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                           , LMF080G.sprDetailDef.CUST_NM.ColNo _
                                                           , LMF080G.sprDetailDef.CUST_NM.ColName, 122)

        '伝票№
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF080G.sprDetailDef.CUST_REF_NO.ColNo _
                                                          , LMF080G.sprDetailDef.CUST_REF_NO.ColName, 30)

        '支払先コード
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF080G.sprDetailDef.SHIHARAITO_CD.ColNo _
                                                          , LMF080G.sprDetailDef.SHIHARAITO_CD.ColName, 5)

        '支払先名
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF080G.sprDetailDef.SHIHARAI_NM.ColNo _
                                                          , LMF080G.sprDetailDef.SHIHARAI_NM.ColName, 122)

        '届先名
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF080G.sprDetailDef.DEST_NM.ColNo _
                                                          , LMF080G.sprDetailDef.DEST_NM.ColName, 80)

        'START YANAI 要望番号1424 支払処理
        '届先住所
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF080G.sprDetailDef.DEST_AD.ColNo _
                                                          , LMF080G.sprDetailDef.DEST_AD.ColName, 120)
        'END YANAI 要望番号1424 支払処理

        '運送会社名
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF080G.sprDetailDef.UNSO_NM.ColNo _
                                                          , LMF080G.sprDetailDef.UNSO_NM.ColName, 122)

        'まとめ
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF080G.sprDetailDef.GROUP.ColNo _
                                                          , LMF080G.sprDetailDef.GROUP.ColName, 9)

        'まとめM
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF080G.sprDetailDef.GROUP_M.ColNo _
                                                          , LMF080G.sprDetailDef.GROUP_M.ColName, 3)

        '支払備考
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF080G.sprDetailDef.REMARK.ColNo _
                                                          , LMF080G.sprDetailDef.REMARK.ColName, 100)

        '管理番号
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF080G.sprDetailDef.KANRI_NO.ColNo _
                                                          , LMF080G.sprDetailDef.KANRI_NO.ColName, 9)

        '運送番号
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF080G.sprDetailDef.UNSO_NO.ColNo _
                                                          , LMF080G.sprDetailDef.UNSO_NO.ColName, 9)

        '運行番号
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF080G.sprDetailDef.TRIP_NO.ColNo _
                                                          , LMF080G.sprDetailDef.TRIP_NO.ColName, 10)

        '届先JIS
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF080G.sprDetailDef.DEST_JIS_CD.ColNo _
                                                          , LMF080G.sprDetailDef.DEST_JIS_CD.ColName, 7)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputConnectionChk() As Boolean

        With Me._Frm

            '日付絞込の関連必須チェック
            Dim rtnResult As Boolean = Me._Vcon.IsDateCtlConnectionChk(.cmbDateKb, .imdFrom, .imdTo)

            'From + Toの大小チェック
            rtnResult = rtnResult AndAlso Me._Vcon.IsDateFromToChk(.imdFrom _
                                                                   , .imdTo _
                                                                   , String.Concat(LMFControlC.DATE_NM, LMFControlC.TO_NM) _
                                                                   , String.Concat(LMFControlC.DATE_NM, LMFControlC.FROM_NM) _
                                                                   )

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 一括変更チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSaveChk(ByVal arr As ArrayList) As Boolean

        'スペース除去
        Call Me.TrimHeaderSpaceTextValue(LMF080C.ActionType.SAVE)

        '未選択チェック
        Dim cnt As Integer = arr.Count
        Dim rtnResult As Boolean = Me._Vcon.IsSelectChk(cnt)

        '選択上限チェック
        rtnResult = rtnResult AndAlso Me._Vcon.IsSelectLimitChk(cnt, LMF080C.IKKATU_LMF080)

        '単項目チェック
        rtnResult = rtnResult AndAlso Me.IsSaveInputCheck()

        'マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsMstExistChk()

        '支払先コードの同値チェック
        rtnResult = rtnResult AndAlso Me.IsIkkatuDotiChk(arr, cnt - 1)

        'タリフ分類区分のチェック
        rtnResult = rtnResult AndAlso Me.IsIkkatuTariffBunruiKbChk(arr, cnt - 1)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 一括変更時の単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveInputCheck() As Boolean

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '修正項目
            .cmbShusei.ItemName = .lblTitleHenko.Text
            .cmbShusei.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbShusei) = errorFlg Then
                Return errorFlg
            End If

            '変更コード(大)
            .txtShuseiL.ItemName = String.Concat(LMF080C.HENKO, LMFControlC.CD)
            .txtShuseiL.IsHissuCheck = chkFlg
            .txtShuseiL.IsForbiddenWordsCheck = chkFlg
            Select Case .cmbShusei.SelectedIndex
                Case 1 '支払先コード
                    .txtShuseiL.IsByteCheck = 8

                Case 2 'タリフコード
                    .txtShuseiL.IsByteCheck = 10

                Case 3 '横持ちタリフコード
                    .txtShuseiL.IsByteCheck = 10

                Case 4 '割増タリフコード
                    .txtShuseiL.IsByteCheck = 10

            End Select
            .txtShuseiL.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtShuseiL) = errorFlg Then
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

        Select Case Me._Frm.cmbShusei.SelectedValue.ToString()

            Case LMF080C.SHUSEI_SHIHARAI

                Return Me.IsShiharaitoExistChk()

            Case LMF080C.SHUSEI_TARIFF

                Return Me.IsShiharaiTariffExistChk()

            Case LMF080C.SHUSEI_YOKO

                Return Me.IsYokoExistChk()

            Case LMF080C.SHUSEI_ETARIFF

                Return Me.IsExtcTariffExistChk()

        End Select

        Return True

    End Function

    ''' <summary>
    ''' 支払先マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsShiharaitoExistChk() As Boolean

        With Me._Frm

            '取得できない場合、エラー
            Dim drs As DataRow() = Me._Gcon.SelectShiharaitoListDataRow(.txtShuseiL.TextValue)
            If drs.Length < 1 Then
                Me._Vcon.SetErrorControl(.txtShuseiL)
                Return Me._Vcon.SetShiharaitoExistErr(.txtShuseiL.TextValue)
            End If
            .txtShuseiL.TextValue = drs(0).Item("SHIHARAITO_CD").ToString

            Return True

        End With

    End Function

    ''' <summary>
    ''' 支払タリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsShiharaiTariffExistChk() As Boolean

        With Me._Frm

            '取得できない場合、エラー
            Dim drs As DataRow() = Me._Gcon.SelectShiharaiTariffListDataRow(.txtShuseiL.TextValue, String.Empty, String.Empty)
            If drs.Length < 1 Then
                Me._Vcon.SetErrorControl(.txtShuseiL)
                Return Me._Vcon.SetShiharaiTariffExistErr(.txtShuseiL.TextValue)
            End If
            .txtShuseiL.TextValue = drs(0).Item("SHIHARAI_TARIFF_CD").ToString

            Return True

        End With

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsYokoExistChk() As Boolean

        With Me._Frm

            '取得できない場合、エラー
            Dim drs As DataRow() = Me._Gcon.SelectShiharaiYokoTariffListDataRow(LMUserInfoManager.GetNrsBrCd(), .txtShuseiL.TextValue)
            If drs.Length < 1 Then
                Me._Vcon.SetErrorControl(.txtShuseiL)
                Return Me._Vcon.SetShiharaiYokoTariffExistErr(.txtShuseiL.TextValue)
            End If
            .txtShuseiL.TextValue = drs(0).Item("YOKO_TARIFF_CD").ToString

            Return True

        End With

    End Function

    ''' <summary>
    ''' 支払割増タリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsExtcTariffExistChk() As Boolean

        With Me._Frm

            '取得できない場合、エラー
            Dim drs As DataRow() = Me._Gcon.SelectExtcShiharaiListDataRow(LMUserInfoManager.GetNrsBrCd(), .txtShuseiL.TextValue, String.Empty)
            If drs.Length < 1 Then
                Me._Vcon.SetErrorControl(.txtShuseiL)
                Return Me._Vcon.SetShiharaiExtcExistErr(.txtShuseiL.TextValue)
            End If
            .txtShuseiL.TextValue = drs(0).Item("EXTC_TARIFF_CD").ToString

            Return True

        End With

    End Function

    ''' <summary>
    ''' 確定チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFixChk(ByVal arr As ArrayList) As Boolean

        '未選択チェック
        Dim cnt As Integer = arr.Count
        Dim rtnResult As Boolean = Me._Vcon.IsSelectChk(cnt)

        '選択上限チェック
        rtnResult = rtnResult AndAlso Me._Vcon.IsSelectLimitChk(cnt, LMF080C.IKKATU_LMF080)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 確定解除チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFixCancellChk(ByVal arr As ArrayList) As Boolean

        '未選択チェック
        Dim cnt As Integer = arr.Count
        Dim rtnResult As Boolean = Me._Vcon.IsSelectChk(cnt)

        '選択上限チェック
        rtnResult = rtnResult AndAlso Me._Vcon.IsSelectLimitChk(cnt, LMF080C.IKKATU_LMF080)

        Return rtnResult

    End Function

    ''' <summary>
    ''' まとめ指示チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsGroupChk(ByVal arr As ArrayList) As Boolean

        '未選択チェック
        Dim cnt As Integer = arr.Count
        Dim rtnResult As Boolean = Me._Vcon.IsSelectChk(cnt)

        '多選択チェック
        rtnResult = rtnResult AndAlso Me.IsSelectsChk(cnt)

        '選択上限チェック
        rtnResult = rtnResult AndAlso Me._Vcon.IsSelectLimitChk(cnt, LMF080C.IKKATU_LMF080)

        'まとめ検索チェック
        rtnResult = rtnResult AndAlso Me.IsGroupFindChk()

        'まとめ済み、確定済チェック
        rtnResult = rtnResult AndAlso Me.IsGroupZumiChk(arr, cnt - 1)

        Return rtnResult

    End Function

    ''' <summary>
    ''' まとめ済み、確定済チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <param name="max">選択した件数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsGroupZumiChk(ByVal arr As ArrayList, ByVal max As Integer) As Boolean

        With Me._Frm.sprDetail.ActiveSheet

            Dim rowNo As Integer = 0

            For i As Integer = 0 To max

                rowNo = Convert.ToInt32(arr(i))

                'まとめ済チェック
                If String.IsNullOrEmpty(Me._Gcon.GetCellValue(.Cells(rowNo, LMF080G.sprDetailDef.GROUP.ColNo))) = False Then
                    Return Me._Vcon.SetErrMessage("E237", New String() {LMFControlC.MATOME_ZUMI})
                End If

                '確定済みチェック
                If Me.IsKakuteiChk(rowNo, True, LMFControlC.KAKUTEI_ZUMI) = False Then
                    Return False
                End If

            Next

            Return True

        End With

    End Function

    ''' <summary>
    ''' 確定済、未チェック
    ''' </summary>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <param name="flg">フラグ　True：確定済みの場合、エラー　False：確定未の場合、エラー</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKakuteiChk(ByVal rowNo As Integer, ByVal flg As Boolean, ByVal msg As String) As Boolean

        If LMFControlC.FLG_ON.Equals(Me._Gcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(rowNo, LMF080G.sprDetailDef.KAKUTEI_FLG.ColNo))) = flg Then
            Return Me._Vcon.SetErrMessage("E237", New String() {msg})
        End If

        Return True

    End Function

    ''' <summary>
    ''' まとめ検索チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsGroupFindChk() As Boolean

        'まとめ検索していない場合、エラー
        If String.IsNullOrEmpty(Me._Frm.lblOrderBy.TextValue) = True Then
            Return Me._Vcon.SetErrMessage("E236")
        End If

        Return True

    End Function

    ''' <summary>
    ''' 運行番号を取得
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>運行番号</returns>
    ''' <remarks>
    ''' 1行目：運行番号 or 運行番号(集荷)
    ''' 2行目：Nothing  or 運行番号(中継)
    ''' 3行目：Nothing  or 運行番号(配荷)
    ''' </remarks>
    Private Function GetTripNo(ByVal rowNo As Integer) As String()

        GetTripNo = Nothing

        With Me._Frm.sprDetail.ActiveSheet

            ''中継配送有の場合、運行番号(配荷)
            'If LMFControlC.FLG_ON.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF080G.sprDetailDef.TYUKEI_HAISO_FLG.ColNo))) = True Then
            '    Return New String() {Me._Gcon.GetCellValue(.Cells(rowNo, LMF080G.sprDetailDef.TRIP_NO_SHUKA.ColNo)) _
            '                         , Me._Gcon.GetCellValue(.Cells(rowNo, LMF080G.sprDetailDef.TRIP_NO_CHUKEI.ColNo)) _
            '                         , Me._Gcon.GetCellValue(.Cells(rowNo, LMF080G.sprDetailDef.TRIP_NO_HAIKA.ColNo)) _
            '                         }
            'End If

            Return New String() {Me._Gcon.GetCellValue(.Cells(rowNo, LMF080G.sprDetailDef.TRIP_NO.ColNo))}

        End With

    End Function

    ''' <summary>
    ''' 運行番号の同値チェック(配荷のみを見る)
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <param name="value">比較対象(文字型配列)</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTripDotiChk(ByVal rowNo As Integer, ByVal value As String()) As Boolean

        With Me._Frm.sprDetail.ActiveSheet

            Dim max As Integer = value.Count - 1
            Dim chkValue As String() = Me.GetTripNo(rowNo)

            '1番後ろが同じ場合、OK
            Return Me.IsTripDotiChk(value(value.Count - 1), chkValue(chkValue.Count - 1))

        End With

    End Function

    ''' <summary>
    ''' 運行番号の同値チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <param name="max">リストの件数</param>
    ''' <param name="value">比較する値配列</param>
    ''' <returns>同じ場合、True　違う場合、False</returns>
    ''' <remarks></remarks>
    Private Function IsTripDotiChk(ByVal arr As ArrayList, ByVal max As Integer, ByVal value As String()) As Boolean

        ''集荷番号でチェック
        'If Me.IsTripDotiChk(arr, max, value(1), LMF080G.sprDetailDef.TRIP_NO_SHUKA.ColNo) = True Then
        '    Return True
        'End If

        ''中継番号でチェック
        'Return Me.IsTripDotiChk(arr, max, value(2), LMF080G.sprDetailDef.TRIP_NO_CHUKEI.ColNo)

    End Function

    ''' <summary>
    ''' 運行番号の同値チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <param name="max">リストの件数</param>
    ''' <param name="value">比較する値</param>
    ''' <param name="colNo">列番号</param>
    ''' <returns>同じ場合、True　違う場合、False</returns>
    ''' <remarks></remarks>
    Private Function IsTripDotiChk(ByVal arr As ArrayList, ByVal max As Integer, ByVal value As String, ByVal colNo As Integer) As Boolean

        With Me._Frm.sprDetail.ActiveSheet

            For i As Integer = 1 To max

                '1つでも違うレコードが存在する場合、エラー
                If Me.IsTripDotiChk(value, Me._Gcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), colNo))) = False Then
                    Return False
                End If

            Next

            '全て一致している場合、OK
            Return True

        End With

    End Function

    ''' <summary>
    ''' 運行番号の同値チェック
    ''' </summary>
    ''' <param name="value1">値1</param>
    ''' <param name="value2">値2</param>
    ''' <returns>同じ場合、True　違う場合、False</returns>
    ''' <remarks></remarks>
    Private Function IsTripDotiChk(ByVal value1 As String, ByVal value2 As String) As Boolean

        If String.IsNullOrEmpty(value1) = True Then
            Return False
        End If
        If String.IsNullOrEmpty(value2) = True Then
            Return False
        End If
        Return value1.Equals(value2)

    End Function

    ''' <summary>
    ''' タリフ分類区分のグループ条件
    ''' </summary>
    ''' <param name="tariffKbn">タリフ分類区分</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsGroupTariffKbn(ByVal tariffKbn As String, ByVal rowNo As Integer) As Boolean

        Dim chkData As String = Me._Gcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(rowNo, LMF080G.sprDetailDef.TARIFF_KBN.ColNo))

        '基準(1行目)の区分
        Select Case tariffKbn

            Case LMFControlC.TARIFF_YOKO, LMFControlC.TARIFF_INKA

                '横持ち , 入荷着払いの場合、同値 = OK
                Return tariffKbn.Equals(chkData)

        End Select

        '上記以外、横持ち or 入荷着払いの場合、エラー
        Select Case chkData

            Case LMFControlC.TARIFF_YOKO, LMFControlC.TARIFF_INKA

                '横持ち , 入荷着払いの場合、同値 = OK
                Return False

        End Select

        Return True

    End Function

    ''' <summary>
    ''' エラーメッセージセット(E239)
    ''' </summary>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Private Function SetErrMessageE239() As Boolean
        Return Me._Vcon.SetErrMessage("E239")
    End Function

    ''' <summary>
    ''' まとめ解除チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsGroupCancellChk(ByVal arr As ArrayList) As Boolean

        '未選択チェック
        Dim cnt As Integer = arr.Count
        Dim rtnResult As Boolean = Me._Vcon.IsSelectChk(cnt)

        '選択上限チェック
        rtnResult = rtnResult AndAlso Me._Vcon.IsSelectLimitChk(cnt, LMF080C.IKKATU_LMF080)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 支払先の同値チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <param name="max">選択件数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsIkkatuDotiChk(ByVal arr As ArrayList, ByVal max As Integer) As Boolean

        With Me._Frm.sprDetail.ActiveSheet

            Select Case Me._Frm.cmbShusei.SelectedValue.ToString()

                Case LMF080C.SHUSEI_SHIHARAI

                    '選択した支払先コードが同じでない場合、エラー
                    Dim seiqto As String = Me._Gcon.GetCellValue(.Cells(Convert.ToInt32(arr(0)), LMF080G.sprDetailDef.SHIHARAITO_CD.ColNo))
                    For i As Integer = 1 To max

                        If seiqto.Equals(Me._Gcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMF080G.sprDetailDef.SHIHARAITO_CD.ColNo))) = False Then
                            Return Me._Vcon.SetErrMessage("E227", New String() {Me._Vcon.SetRepMsgData(LMF080G.sprDetailDef.SHIHARAITO_CD.ColName)})
                        End If

                    Next

            End Select

            Return True

        End With

    End Function

    ''' <summary>
    ''' タリフ分類区分のチェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <param name="max">選択件数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsIkkatuTariffBunruiKbChk(ByVal arr As ArrayList, ByVal max As Integer) As Boolean

        With Me._Frm.sprDetail.ActiveSheet

            Select Case Me._Frm.cmbShusei.SelectedValue.ToString()

                Case LMF080C.SHUSEI_ETARIFF

                    '選択したデータのタリフ分類区分が"車扱い"、"横持"の場合、エラー
                    Dim tariffKb As String = String.Empty
                    For i As Integer = 0 To max
                        tariffKb = Me._Gcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMF080G.sprDetailDef.TARIFF_KBN.ColNo))

                        If ("20").Equals(tariffKb) = True OrElse _
                            ("40").Equals(tariffKb) = True Then
                            Return Me._Vcon.SetErrMessage("E499", New String() {String.Concat(arr(i), "行目")})
                        End If

                    Next

            End Select

            Return True

        End With

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMF080C.ActionType) As Boolean

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            'ポップ対象外の場合
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMF080C.ActionType.MASTEROPEN) = True Then
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

                Case .txtTariffCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtTariffCd}
                    lblCtl = New Control() {.lblTariffNm}
                    msg = New String() {String.Concat(.lblTitleTariff.TextValue, LMFControlC.CD)}

                Case .txtExtcCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtExtcCd}
                    lblCtl = New Control() {.lblExtcNm}
                    msg = New String() {String.Concat(.lblTitleExtc.TextValue, LMFControlC.CD)}

                Case .txtUnsocoCd.Name, .txtUnsocoBrCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtUnsocoCd, .txtUnsocoBrCd}
                    lblCtl = New Control() {.lblUnsocoNm}
                    msg = New String() {String.Concat(.lblTitleUnsoco.Text, LMFControlC.CD), String.Concat(.lblTitleUnsoco.Text, LMFControlC.BR_CD)}

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    Dim custNm As String = .lblTitleCust.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtCustCdL, .txtCustCdM}
                    lblCtl = New Control() {.lblCustNm}
                    msg = New String() {String.Concat(custNm, LMFControlC.CD), String.Concat(custNm, LMFControlC.M_NM)}

                Case .txtDestCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtDestCd}
                    lblCtl = New Control() {.lblDestNm}
                    msg = New String() {String.Concat(.lblTitleDest.Text, LMFControlC.CD)}

                Case .txtShuseiL.Name

                    txtCtl = Me.FocusCtl()
                    msg = New String() {String.Concat(LMF080C.HENKO)}

            End Select

            'フォーカス位置チェック
            Dim rtnResult As Boolean = Me._Vcon.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)

            '営業所必須
            rtnResult = rtnResult AndAlso Me.IsNrsHissuChk(objNm)

            'バイト数チェック
            rtnResult = rtnResult AndAlso Me.IsIkkatsuByteCheck(objNm)

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 一括変更時の必須チェック
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHissuChk(ByVal ctl As Win.InputMan.LMImCombo, ByVal msg As String) As Boolean

        ctl.ItemName = msg
        ctl.IsHissuCheck = True
        Return MyBase.IsValidateCheck(ctl)

    End Function

    ''' <summary>
    ''' 必須チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsNrsHissuChk() As Boolean

        Return Me.IsHissuChk(Me._Frm.cmbEigyo, Me._Frm.lblTitleEigyo.Text)

    End Function

    ''' <summary>
    ''' 営業所の必須チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsNrsHissuChk(ByVal objNm As String) As Boolean

        With Me._Frm

            '編集項目のPopはチェックはしない
            Select Case objNm

            End Select

            '営業所必須
            Return Me.IsNrsHissuChk()

        End With

    End Function

    ''' <summary>
    ''' 修正項目のフォーカス位置チェック用コントロールを設定
    ''' </summary>
    ''' <returns>コントロール配列</returns>
    ''' <remarks></remarks>
    Private Function FocusCtl() As Win.InputMan.LMImTextBox()

        With Me._Frm

            FocusCtl = Nothing

            FocusCtl = New Win.InputMan.LMImTextBox() {.txtShuseiL}

            Return FocusCtl

        End With

    End Function

    ''' <summary>
    ''' 多選択チェック
    ''' </summary>
    ''' <param name="chkCnt">選択した件数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSelectsChk(ByVal chkCnt As Integer) As Boolean

        'チェック件数が2件より小さい場合、エラー
        If chkCnt < 2 Then

            Return Me._Vcon.SetErrMessage("E234")

        End If

        Return True

    End Function

    ''' <summary>
    ''' 一括変更時のバイトチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsIkkatsuByteCheck(ByVal objNm As String) As Boolean

        With Me._Frm

            If (objNm).Equals(.txtShuseiL.Name) = False Then
                Return True
            End If

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '変更コード(大)
            .txtShuseiL.ItemName = String.Concat(LMF080C.HENKO, LMFControlC.CD)
            Select Case .cmbShusei.SelectedIndex
                Case 1 '支払先コード
                    .txtShuseiL.IsByteCheck = 8

                Case 2 'タリフコード
                    .txtShuseiL.IsByteCheck = 10

                Case 3 '横持ちタリフコード
                    .txtShuseiL.IsByteCheck = 10

                Case 4 '割増タリフコード
                    .txtShuseiL.IsByteCheck = 10

            End Select
            .txtShuseiL.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtShuseiL) = errorFlg Then
                Return errorFlg
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 連続入力チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsLoopEditChk(ByVal arr As ArrayList) As Boolean

        '未選択チェック
        Dim cnt As Integer = arr.Count
        Dim rtnResult As Boolean = Me._Vcon.IsSelectChk(cnt)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 再計算チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSilicaChk(ByVal arr As ArrayList) As Boolean

        '未選択チェック
        Dim cnt As Integer = arr.Count
        Dim rtnResult As Boolean = Me._Vcon.IsSelectChk(cnt)

        '選択上限チェック
        rtnResult = rtnResult AndAlso Me._Vcon.IsSelectLimitChk(cnt, LMF080C.SAIKEISAN_LMF080)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthority(ByVal actionType As LMF080C.ActionType) As Boolean

        Dim kengen As String = LMUserInfoManager.GetAuthoLv()
        Dim kengenFlg As Boolean = True

        Select Case actionType

            Case LMF080C.ActionType.PRINT

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
                        kengenFlg = False
                End Select

            Case LMF080C.ActionType.LOOPEDIT

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
                        kengenFlg = False
                End Select

            Case LMF080C.ActionType.FIX

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
                        kengenFlg = False
                End Select

            Case LMF080C.ActionType.FIX_CANCELL

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
                        kengenFlg = False
                End Select

            Case LMF080C.ActionType.GROUP

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
                        kengenFlg = False
                End Select

            Case LMF080C.ActionType.GROUP_CANCELL

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
                        kengenFlg = False
                End Select

            Case LMF080C.ActionType.KENSAKU

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
                        kengenFlg = False
                End Select

            Case LMF080C.ActionType.ENTER

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
                        kengenFlg = False
                End Select

            Case LMF080C.ActionType.MASTEROPEN

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
                        kengenFlg = False
                End Select

            Case LMF080C.ActionType.DOUBLECLICK


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
                        kengenFlg = False
                End Select

            Case LMF080C.ActionType.SAVE

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
                        kengenFlg = False
                End Select

            Case LMF080C.ActionType.CLOSE

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

            Case LMF080C.ActionType.SAIKEISAN

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
                        kengenFlg = False
                End Select

        End Select

        Return Me._Vcon.IsAuthorityChk(kengenFlg)

    End Function

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        'ヘッダ項目のスペース除去
        Call Me.TrimHeaderSpaceTextValue(LMF080C.ActionType.KENSAKU)

        'スプレッドのスペース除去
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0)

    End Sub

    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimHeaderSpaceTextValue(ByVal actionType As LMF080C.ActionType)

        With Me._Frm

            Select Case actionType

                Case LMF080C.ActionType.KENSAKU

                    .txtTariffCd.TextValue = .txtTariffCd.TextValue.Trim()
                    .txtExtcCd.TextValue = .txtExtcCd.TextValue.Trim()
                    .txtUnsocoCd.TextValue = .txtUnsocoCd.TextValue.Trim()
                    .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
                    .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()
                    .txtDestCd.TextValue = .txtDestCd.TextValue.Trim()

                Case Else

                    .txtShuseiL.TextValue = .txtShuseiL.TextValue.Trim()

            End Select

        End With

    End Sub

#End Region 'Method

End Class
