' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF040V : 運賃検索
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMF040Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMF040V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF040F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF040F, ByVal v As LMFControlV, ByVal g As LMFControlG)

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

            'タリフコード
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtTariffCd, String.Concat(.lblTitleTariff.Text, LMFControlC.CD), 10)

            '割増タリフコード
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtExtcCd, String.Concat(.lblTitleExtc.Text, LMFControlC.CD), 10)

            '乗務員コード
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtDriverCd, String.Concat(.lblTitleDriver.Text, LMFControlC.CD), 5)

            '荷主コード
            rtnResult = rtnResult AndAlso Me.IsInputCustCd()

            '----->要望番号:928
            '届先コード
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtDestCd, String.Concat(.lblTitleDest.Text, LMFControlC.CD), 15)
            '<-----要望番号:928


            'START YANAI 20120622 DIC運賃まとめ及び再計算対応
            '項目表示
            rtnResult = rtnResult AndAlso Me.IsVisibleKbHissuChk()
            'END YANAI 20120622 DIC運賃まとめ及び再計算対応

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
            'START YANAI 要望番号476
            '.txtCustCdL.IsHissuCheck = chkFlg
            'END YANAI 要望番号476
            .txtCustCdL.IsForbiddenWordsCheck = chkFlg
            .txtCustCdL.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCdL) = errorFlg Then
                Return errorFlg
            End If

            '荷主コード(中)
            .txtCustCdM.ItemName = String.Concat(msg, LMFControlC.M_NM, LMFControlC.CD)
            'START YANAI 要望番号476
            '.txtCustCdM.IsHissuCheck = chkFlg
            'END YANAI 要望番号476
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
                                                           , LMF040G.sprDetailDef.CUST_NM.ColNo _
                                                           , LMF040G.sprDetailDef.CUST_NM.ColName, 122)

        'START YANAI 20120622 DIC運賃まとめ及び再計算対応
        '伝票№
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.CUST_REF_NO.ColNo _
                                                          , LMF040G.sprDetailDef.CUST_REF_NO.ColName, 30)
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応

        '請求先コード
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.SEIQTO_CD.ColNo _
                                                          , LMF040G.sprDetailDef.SEIQTO_CD.ColName, 7)

        '請求先名
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.SEIQTO_NM.ColNo _
                                                          , LMF040G.sprDetailDef.SEIQTO_NM.ColName, 60)

        '届先名
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.DEST_NM.ColNo _
                                                          , LMF040G.sprDetailDef.DEST_NM.ColName, 80)

        '運送会社(1次)
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.UNSO_NM.ColNo _
                                                          , LMF040G.sprDetailDef.UNSO_NM.ColName, 122)

        '運送会社(2次)
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.UNSOCO_NM.ColNo _
                                                          , LMF040G.sprDetailDef.UNSOCO_NM.ColName, 122)

        'START YANAI 20120622 DIC運賃まとめ及び再計算対応
        '在庫部課
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.ZBUKA_CD.ColNo _
                                                          , LMF040G.sprDetailDef.ZBUKA_CD.ColName, 7)

        '扱い部課
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.ABUKA_CD.ColNo _
                                                          , LMF040G.sprDetailDef.ABUKA_CD.ColName, 7)
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応

        'まとめ
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.GROUP.ColNo _
                                                          , LMF040G.sprDetailDef.GROUP.ColName, 9)

        'まとめM
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.GROUP_M.ColNo _
                                                          , LMF040G.sprDetailDef.GROUP_M.ColName, 3)

        '運賃備考
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.REMARK.ColNo _
                                                          , LMF040G.sprDetailDef.REMARK.ColName, 100)

        '管理番号
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.KANRI_NO.ColNo _
                                                          , LMF040G.sprDetailDef.KANRI_NO.ColName, 9)

        '運送番号
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.UNSO_NO.ColNo _
                                                          , LMF040G.sprDetailDef.UNSO_NO.ColName, 9)

        '運行番号
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.TRIP_NO.ColNo _
                                                          , LMF040G.sprDetailDef.TRIP_NO.ColName, 10)

        '集荷中継地
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.SHUKA_RELY_POINT.ColNo _
                                                          , LMF040G.sprDetailDef.SHUKA_RELY_POINT.ColName, 50)

        '配荷中継地
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.HAIKA_RELY_POINT.ColNo _
                                                          , LMF040G.sprDetailDef.HAIKA_RELY_POINT.ColName, 50)

        '運行番号(集荷)
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.TRIP_NO_SHUKA.ColNo _
                                                          , LMF040G.sprDetailDef.TRIP_NO_SHUKA.ColName, 10)

        '運行番号(中継)
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.TRIP_NO_CHUKEI.ColNo _
                                                          , LMF040G.sprDetailDef.TRIP_NO_CHUKEI.ColName, 10)

        '運行番号(配荷)
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.TRIP_NO_HAIKA.ColNo _
                                                          , LMF040G.sprDetailDef.TRIP_NO_HAIKA.ColName, 10)

        '運送会社(集荷)
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.UNSOCO_SHUKA.ColNo _
                                                          , LMF040G.sprDetailDef.UNSOCO_SHUKA.ColName, 122)

        '運送会社(中継)
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.UNSOCO_CHUKEI.ColNo _
                                                          , LMF040G.sprDetailDef.UNSOCO_CHUKEI.ColName, 122)

        '運送会社(配荷)
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.UNSOCO_HAIKA.ColNo _
                                                          , LMF040G.sprDetailDef.UNSOCO_HAIKA.ColName, 122)

        '届先コード
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.DEST_CD.ColNo _
                                                          , LMF040G.sprDetailDef.DEST_CD.ColName, 15)

        '届先JIS
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.DEST_JIS_CD.ColNo _
                                                          , LMF040G.sprDetailDef.DEST_JIS_CD.ColName, 7)

        '運送会社コード(1次)
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.UNSO_CD.ColNo _
                                                          , LMF040G.sprDetailDef.UNSO_CD.ColName, 5)

        '運送会社支店コード(1次)
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.UNSO_BR_CD.ColNo _
                                                          , LMF040G.sprDetailDef.UNSO_BR_CD.ColName, 3)

        '運送会社コード(2次)
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.UNSOCO_CD.ColNo _
                                                          , LMF040G.sprDetailDef.UNSOCO_CD.ColName, 5)

        '運送会社支店コード(1次)
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                          , LMF040G.sprDetailDef.UNSOCO_BR_CD.ColNo _
                                                          , LMF040G.sprDetailDef.UNSOCO_BR_CD.ColName, 3)

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
        Call Me.TrimHeaderSpaceTextValue(LMF040C.ActionType.SAVE)

        '未選択チェック
        Dim cnt As Integer = arr.Count
        Dim rtnResult As Boolean = Me._Vcon.IsSelectChk(cnt)

        '選択上限チェック
        rtnResult = rtnResult AndAlso Me._Vcon.IsSelectLimitChk(cnt, LMF040C.IKKATU_LMF040)

        '単項目チェック
        rtnResult = rtnResult AndAlso Me.IsSaveInputCheck()

        'マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsMstExistChk()

        '請求先コードの同値チェック
        rtnResult = rtnResult AndAlso Me.IsIkkatuDotiChk(arr, cnt - 1)

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
            .txtShuseiL.ItemName = String.Concat(LMF040C.HENKO, LMFControlC.CD, LMFControlC.L_NM)
            .txtShuseiL.IsHissuCheck = chkFlg
            .txtShuseiL.IsForbiddenWordsCheck = chkFlg
            'START YANAI 要望番号568
            '.txtShuseiL.IsByteCheck = 10
            Select Case .cmbShusei.SelectedIndex
                Case 1 '請求先コード
                    .txtShuseiL.IsByteCheck = 7

                Case 2 'タリフコード
                    .txtShuseiL.IsByteCheck = 10

                Case 3 '横持ちタリフコード
                    .txtShuseiL.IsByteCheck = 10

                Case 4 '荷主コード
                    .txtShuseiL.IsByteCheck = 5

                    'START YANAI 要望番号996
                Case 5 '割増タリフコード
                    .txtShuseiL.IsByteCheck = 10
                    'END YANAI 要望番号996

                    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
                Case 6 '在庫部課
                    .txtShuseiL.IsByteCheck = 7

                Case 7 '扱い部課
                    .txtShuseiL.IsByteCheck = 7
                    'END YANAI 20120622 DIC運賃まとめ及び再計算対応
                Case 8 '距離程コード
                    'START s.kobayashi 20140519 距離変更
                    .txtShuseiL.IsByteCheck = 3
                    'End s.kobayashi 20140519 距離変更
            End Select
            'END YANAI 要望番号568
            .txtShuseiL.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtShuseiL) = errorFlg Then
                Return errorFlg
            End If

            Dim henko As String = .cmbShusei.SelectedValue.ToString()

            '変更コード(中)
            .txtShuseiM.ItemName = String.Concat(LMF040C.HENKO, LMFControlC.CD, LMFControlC.M_NM)
            .txtShuseiM.IsHissuCheck = LMF040C.SHUSEI_CUST.Equals(henko)
            .txtShuseiM.IsForbiddenWordsCheck = chkFlg
            .txtShuseiM.IsFullByteCheck = 2
            .txtShuseiM.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtShuseiM) = errorFlg Then
                Return errorFlg
            End If

            '変更コード(小)
            .txtShuseiS.ItemName = String.Concat(LMF040C.HENKO, LMFControlC.CD, LMFControlC.S_NM)
            .txtShuseiS.IsHissuCheck = LMF040C.SHUSEI_CUST.Equals(henko)
            .txtShuseiS.IsForbiddenWordsCheck = chkFlg
            .txtShuseiS.IsFullByteCheck = 2
            .txtShuseiS.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtShuseiS) = errorFlg Then
                Return errorFlg
            End If

            '変更コード(極小)
            .txtShuseiSS.ItemName = String.Concat(LMF040C.HENKO, LMFControlC.CD, LMFControlC.SS_NM)
            .txtShuseiSS.IsHissuCheck = LMF040C.SHUSEI_CUST.Equals(henko)
            .txtShuseiSS.IsForbiddenWordsCheck = chkFlg
            .txtShuseiSS.IsFullByteCheck = 2
            .txtShuseiSS.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtShuseiSS) = errorFlg Then
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

            Case LMF040C.SHUSEI_SEIQTO

                Return Me.IsSeiqtoExistChk()

            Case LMF040C.SHUSEI_YOKO

                Return Me.IsYokoExistChk()

            Case LMF040C.SHUSEI_CUST

                Return Me.IsCustExistChk()

            Case LMF040C.SHUSEI_KYORI

                Return Me.IsKyoriExistChk()

        End Select

        Return True

    End Function

    ''' <summary>
    ''' 請求先マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSeiqtoExistChk() As Boolean

        With Me._Frm

            '取得できない場合、エラー
            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectSeiqtoListDataRow(drs, LMUserInfoManager.GetNrsBrCd(), .txtShuseiL.TextValue) = False Then
                Me._Vcon.SetErrorControl(.txtShuseiL)
                Return False
            End If

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
            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectYokoTariffListDataRow(drs, LMUserInfoManager.GetNrsBrCd(), .txtShuseiL.TextValue) = False Then
                Me._Vcon.SetErrorControl(.txtShuseiL)
                Return False
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 荷主マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsCustExistChk() As Boolean

        With Me._Frm

            '取得できない場合、エラー
            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectCustListDataRow(drs _
                                              , .cmbEigyo.SelectedValue.ToString() _
                                              , .txtShuseiL.TextValue _
                                              , .txtShuseiM.TextValue _
                                              , .txtShuseiS.TextValue _
                                              , .txtShuseiSS.TextValue _
                                              , LMFControlC.CustMsgType.CUST_SS _
                                              ) = False Then

                Dim errorColor As System.Drawing.Color = Utility.LMGUIUtility.GetAttentionBackColor()
                .txtShuseiM.BackColorDef = errorColor
                .txtShuseiS.BackColorDef = errorColor
                .txtShuseiSS.BackColorDef = errorColor
                Me._Vcon.SetErrorControl(.txtShuseiL)
                Return False
            End If

            .lblCalcKbn.TextValue = drs(0).Item("UNTIN_CALCULATION_KB").ToString()

            Return True

        End With

    End Function

    ''' <summary>
    ''' 距離マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKyoriExistChk() As Boolean

        With Me._Frm

            '取得できない場合、エラー
            Dim drs As DataRow() = Nothing
            If Me.SelectKyoriListDataRow(drs, LMUserInfoManager.GetNrsBrCd(), .txtShuseiL.TextValue) = False Then
                Me._Vcon.SetErrorControl(.txtShuseiL)
                Return False
            End If

            Return True

        End With

    End Function


    ''' <summary>
    ''' 距離程マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kyoriDrs">データロウ配列</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="kyoriCd">距離程コード　初期値 = "000"</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectKyoriListDataRow(ByRef kyoriDrs As DataRow() _
                                          , ByVal brCd As String _
                                          , ByVal kyoriCd As String _
                                          ) As Boolean

        'キャッシュテーブルからデータ抽出
        kyoriDrs = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KYORI_GRP).Select(String.Concat("NRS_BR_CD = '", brCd, "' AND KYORI_CD = '", kyoriCd, "'"))

        '取得できない場合、エラー
        If kyoriDrs.Length < 1 Then
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage("E079", New String() {"距離程マスタ", kyoriCd})
            MyBase.ShowMessage("E857", New String() {kyoriCd})
            '2016.01.06 UMANO 英語化対応END
            Return False
        End If

        Return True

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
        rtnResult = rtnResult AndAlso Me._Vcon.IsSelectLimitChk(cnt, LMF040C.IKKATU_LMF040)

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
        rtnResult = rtnResult AndAlso Me._Vcon.IsSelectLimitChk(cnt, LMF040C.IKKATU_LMF040)

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
        rtnResult = rtnResult AndAlso Me._Vcon.IsSelectLimitChk(cnt, LMF040C.IKKATU_LMF040)

        'まとめ検索チェック
        rtnResult = rtnResult AndAlso Me.IsGroupFindChk()

        'START KIM 2012/11/21 要望番号：1400
        'まとめ解除に合わせて修正（EXCEL出力に変更）
        'まとめ済み、確定済チェック
        'rtnResult = rtnResult AndAlso Me.IsGroupZumiChk(arr, cnt - 1)
        'START KIM 2012/11/21 要望番号：1400

        'まとめ条件チェック
        rtnResult = rtnResult AndAlso Me.IsGroupByChk(arr, cnt - 1)

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
                If String.IsNullOrEmpty(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.GROUP.ColNo))) = False Then
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

        If LMFControlC.FLG_ON.Equals(Me._Gcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.KAKUTEI_FLG.ColNo))) = flg Then
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
    ''' まとめ条件チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <param name="max">選択件数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsGroupByChk(ByVal arr As ArrayList, ByVal max As Integer) As Boolean

        With Me._Frm.sprDetail.ActiveSheet

            Dim rowNo As Integer = Convert.ToInt32(arr(0))

            'START YANAI 20120622 DIC運賃まとめ及び再計算対応
            ''出荷日の必須チェック
            'Dim chkDate As String = Me.GetUnsoDate(rowNo)
            'Dim chk As Boolean = Me._Vcon.IsDotiHissuChk(chkDate, Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.SHUKKA.ColName))

            ''請求先の必須チェック
            'Dim seiq As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.SEIQTO_CD.ColNo))
            'chk = chk AndAlso Me._Vcon.IsDotiHissuChk(seiq, Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.SEIQTO_CD.ColName))

            ''タリフの必須チェック
            'Dim tariff As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.TARIFF_CD.ColNo))
            'chk = chk AndAlso Me._Vcon.IsDotiHissuChk(tariff, Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.TARIFF_CD.ColName))

            ''税区分の必須チェック
            'Dim tax As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.ZEI.ColNo))
            'chk = chk AndAlso Me._Vcon.IsDotiHissuChk(tariff, Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.ZEI_KBN.ColName))

            'Dim item1 As String = String.Empty
            'Dim item2 As String = String.Empty
            'Dim item3 As String() = Nothing
            'Dim orderBy As String = Me._Frm.lblOrderBy.TextValue
            'Select Case orderBy

            '    Case LMF040C.ORDER_BY_CUSTTRIP

            '        '荷主(大)コードの必須チェック
            '        item1 = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.CUST_CD_L.ColNo))
            '        chk = chk AndAlso Me._Vcon.IsDotiHissuChk(item1, Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.CUST_CD.ColName))

            '        '荷主(中)コードの必須チェック
            '        item2 = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.CUST_CD_M.ColNo))
            '        chk = chk AndAlso Me._Vcon.IsDotiHissuChk(item2, Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.CUST_CD.ColName))

            '        '運行番号の必須チェック
            '        item3 = Me.GetTripNo(rowNo)

            '        '運行番号の必須チェック
            '        chk = chk AndAlso Me._Vcon.IsDotiHissuChk(item3, Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.TRIP_NO.ColName))

            '    Case LMF040C.ORDER_BY_DEST

            '        '届先コードの必須チェック
            '        item1 = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.DEST_CD.ColNo))
            '        chk = chk AndAlso Me._Vcon.IsDotiHissuChk(item1, Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.DEST_CD.ColName))

            '    Case LMF040C.ORDER_BY_DESTJIS

            '        '届先JISの必須チェック
            '        item1 = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.DEST_JIS_CD.ColNo))
            '        chk = chk AndAlso Me._Vcon.IsDotiHissuChk(item1, Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.DEST_JIS_CD.ColName))

            'End Select

            ''エラーの場合、終了
            'If chk = False Then
            '    Return chk
            'End If

            ''割増タリフコードは空でもよい
            'Dim extc As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.EXTC_TARIFF_CD.ColNo))

            ''タリフ分類
            'Dim tariffKbn As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.TARIFF_KBN.ColNo))

            'Dim groupFlg As Boolean = True
            'Dim tyukeiFlg As Boolean = LMFControlC.FLG_ON.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.TYUKEI_HAISO_FLG.ColNo)))

            'For i As Integer = 1 To max

            '    rowNo = Convert.ToInt32(arr(i))

            '    '日付が違う場合、エラー
            '    If chkDate.Equals(Me.GetUnsoDate(rowNo)) = False Then
            '        Return Me.SetErrMessageE239()
            '    End If

            '    '請求先が違う場合、エラー
            '    If seiq.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.SEIQTO_CD.ColNo))) = False Then
            '        Return Me.SetErrMessageE239()
            '    End If

            '    'タリフが違う場合、エラー
            '    If tariff.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.TARIFF_CD.ColNo))) = False Then
            '        Return Me.SetErrMessageE239()
            '    End If

            '    '税区分が違う場合、エラー
            '    If tax.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.ZEI.ColNo))) = False Then
            '        Return Me.SetErrMessageE239()
            '    End If

            '    '割増タリフコードが違う場合、エラー
            '    If extc.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.EXTC_TARIFF_CD.ColNo))) = False Then
            '        Return Me.SetErrMessageE239()
            '    End If

            '    '割増タリフコードが違う場合、エラー
            '    If extc.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.EXTC_TARIFF_CD.ColNo))) = False Then
            '        Return Me.SetErrMessageE239()
            '    End If

            '    'まとめ候補による条件チェック
            '    If Me.IsOrderByChk(rowNo, orderBy, item1, item2, item3) = False Then

            '        '荷主 / 運行以外は即エラー
            '        If LMF040C.ORDER_BY_CUSTTRIP.Equals(orderBy) = False Then
            '            Return Me.SetErrMessageE239()
            '        End If

            '        'エラー判定を保持
            '        groupFlg = False

            '    End If

            '    '中継配送無のレコードがあるかを判定
            '    tyukeiFlg = tyukeiFlg AndAlso LMFControlC.FLG_ON.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.TYUKEI_HAISO_FLG.ColNo)))

            '    'タリフ分類区分による条件チェック
            '    If Me.IsGroupTariffKbn(tariffKbn, rowNo) = False Then
            '        Return Me.SetErrMessageE239()
            '    End If

            'Next

            ''荷主 / 運行条件エラーかどうかを判定
            'If groupFlg = False Then

            '    '中継配送無のレコードがある場合、エラー
            '    If tyukeiFlg = False Then
            '        Return Me.SetErrMessageE239()
            '    End If

            '    '集荷 , 中継でのチェック
            '    If Me.IsTripDotiChk(arr, max, item3) = False Then
            '        Return Me.SetErrMessageE239()
            '    End If

            'End If
            Dim orderBy As String = Me._Frm.lblOrderBy.TextValue
            If (LMF040C.ORDER_BY_CUSTTRIP).Equals(ORDERBY) = True OrElse _
                (LMF040C.ORDER_BY_DEST).Equals(ORDERBY) = True OrElse _
                (LMF040C.ORDER_BY_DESTJIS).Equals(ORDERBY) = True Then
                '出荷日の必須チェック
                Dim chkDate As String = Me.GetUnsoDate(rowNo)
                Dim chk As Boolean = Me._Vcon.IsDotiHissuChk(chkDate, Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.SHUKKA.ColName))

                '請求先の必須チェック
                Dim seiq As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.SEIQTO_CD.ColNo))
                chk = chk AndAlso Me._Vcon.IsDotiHissuChk(seiq, Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.SEIQTO_CD.ColName))

                'タリフの必須チェック
                Dim tariff As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.TARIFF_CD.ColNo))
                chk = chk AndAlso Me._Vcon.IsDotiHissuChk(tariff, Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.TARIFF_CD.ColName))

                '税区分の必須チェック
                Dim tax As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.ZEI.ColNo))
                chk = chk AndAlso Me._Vcon.IsDotiHissuChk(tariff, Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.ZEI_KBN.ColName))

                Dim item1 As String = String.Empty
                Dim item2 As String = String.Empty
                Dim item3 As String() = Nothing
                Select Case orderBy

                    Case LMF040C.ORDER_BY_CUSTTRIP

                        '荷主(大)コードの必須チェック
                        item1 = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.CUST_CD_L.ColNo))
                        chk = chk AndAlso Me._Vcon.IsDotiHissuChk(item1, Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.CUST_CD.ColName))

                        '荷主(中)コードの必須チェック
                        item2 = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.CUST_CD_M.ColNo))
                        chk = chk AndAlso Me._Vcon.IsDotiHissuChk(item2, Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.CUST_CD.ColName))

                        '運行番号の必須チェック
                        item3 = Me.GetTripNo(rowNo)

                        '運行番号の必須チェック
                        chk = chk AndAlso Me._Vcon.IsDotiHissuChk(item3, Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.TRIP_NO.ColName))

                    Case LMF040C.ORDER_BY_DEST

                        '届先コードの必須チェック
                        item1 = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.DEST_CD.ColNo))
                        chk = chk AndAlso Me._Vcon.IsDotiHissuChk(item1, Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.DEST_CD.ColName))

                    Case LMF040C.ORDER_BY_DESTJIS

                        '届先JISの必須チェック
                        item1 = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.DEST_JIS_CD.ColNo))
                        chk = chk AndAlso Me._Vcon.IsDotiHissuChk(item1, Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.DEST_JIS_CD.ColName))

                End Select

                'エラーの場合、終了
                If chk = False Then
                    Return chk
                End If

                '割増タリフコードは空でもよい
                Dim extc As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.EXTC_TARIFF_CD.ColNo))

                'タリフ分類
                Dim tariffKbn As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.TARIFF_KBN.ColNo))

                Dim groupFlg As Boolean = True
                Dim tyukeiFlg As Boolean = LMFControlC.FLG_ON.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.TYUKEI_HAISO_FLG.ColNo)))

                For i As Integer = 1 To max

                    rowNo = Convert.ToInt32(arr(i))

                    '日付が違う場合、エラー
                    If chkDate.Equals(Me.GetUnsoDate(rowNo)) = False Then
                        Return Me.SetErrMessageE239()
                    End If

                    '請求先が違う場合、エラー
                    If seiq.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.SEIQTO_CD.ColNo))) = False Then
                        Return Me.SetErrMessageE239()
                    End If

                    'タリフが違う場合、エラー
                    If tariff.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.TARIFF_CD.ColNo))) = False Then
                        Return Me.SetErrMessageE239()
                    End If

                    '税区分が違う場合、エラー
                    If tax.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.ZEI.ColNo))) = False Then
                        Return Me.SetErrMessageE239()
                    End If

                    '割増タリフコードが違う場合、エラー
                    If extc.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.EXTC_TARIFF_CD.ColNo))) = False Then
                        Return Me.SetErrMessageE239()
                    End If

                    '割増タリフコードが違う場合、エラー
                    If extc.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.EXTC_TARIFF_CD.ColNo))) = False Then
                        Return Me.SetErrMessageE239()
                    End If

                    'まとめ候補による条件チェック
                    If Me.IsOrderByChk(rowNo, orderBy, item1, item2, item3) = False Then

                        '荷主 / 運行以外は即エラー
                        If LMF040C.ORDER_BY_CUSTTRIP.Equals(orderBy) = False Then
                            Return Me.SetErrMessageE239()
                        End If

                        'エラー判定を保持
                        groupFlg = False

                    End If

                    '中継配送無のレコードがあるかを判定
                    tyukeiFlg = tyukeiFlg AndAlso LMFControlC.FLG_ON.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.TYUKEI_HAISO_FLG.ColNo)))

                    'タリフ分類区分による条件チェック
                    If Me.IsGroupTariffKbn(tariffKbn, rowNo) = False Then
                        Return Me.SetErrMessageE239()
                    End If

                Next

                '荷主 / 運行条件エラーかどうかを判定
                If groupFlg = False Then

                    '中継配送無のレコードがある場合、エラー
                    If tyukeiFlg = False Then
                        Return Me.SetErrMessageE239()
                    End If

                    '集荷 , 中継でのチェック
                    If Me.IsTripDotiChk(arr, max, item3) = False Then
                        Return Me.SetErrMessageE239()
                    End If

                End If

            ElseIf (LMF040C.ORDER_BY_DIC).Equals(orderBy) = True Then
                '日立物流用の場合
                'START YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない
                'Dim nrsBrCd As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.NRS_BR_CD.ColNo))
                'Dim motoDataKb As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.MOTO_DATA_KBN.ColNo))
                'Dim outkaPlanDate As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.SHUKKA.ColNo))
                'Dim arrPlanDate As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.NONYU.ColNo))
                'Dim destJisCd As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.DEST_JIS_CD.ColNo))
                'Dim binKb As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.BIN_KB.ColNo))
                'Dim zBukaCd As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.ZBUKA_CD.ColNo))
                'Dim aBukaCd As String = Mid(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.ABUKA_CD.ColNo)), 1, 5)
                'Dim destCd As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.DEST_CD.ColNo))
                'Dim minashiDestCd As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.MINASHI_DEST_CD.ColNo))

                'Dim custDetailsDr() As DataRow = Nothing
                'Dim matomeKb As String = String.Empty
                'Dim matomeKb2 As String = String.Empty
                'custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD = '", Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.CUST_CD_L.ColNo)), "' AND ", _
                '                                                                                                "SUB_KB = '33'"))
                'If custDetailsDr.Length > 0 Then
                '    matomeKb = custDetailsDr(0).Item("SET_NAIYO").ToString
                'End If

                'For i As Integer = 1 To max
                '    rowNo = Convert.ToInt32(arr(i))

                '    '営業所コードが違う場合、エラー
                '    If (nrsBrCd).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.NRS_BR_CD.ColNo))) = False Then
                '        Return Me.SetErrMessageE239()
                '    End If

                '    '元データ区分が違う場合、エラー
                '    If (motoDataKb).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.MOTO_DATA_KBN.ColNo))) = False Then
                '        Return Me.SetErrMessageE239()
                '    End If

                '    'まとめ区分が違う場合、エラー
                '    custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD = '", Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.CUST_CD_L.ColNo)), "' AND ", _
                '                                                                                                    "SUB_KB = '33'"))
                '    If custDetailsDr.Length > 0 Then
                '        matomeKb2 = custDetailsDr(0).Item("SET_NAIYO").ToString
                '    End If
                '    If (matomeKb2).Equals(matomeKb) = False Then
                '        Return Me.SetErrMessageE239()
                '    End If

                '    If (LMF040C.DIC_MATOME_01).Equals(matomeKb) = True Then
                '        '①千葉のまとめ対象荷主の場合

                '        '納入日が違う場合、エラー
                '        If (arrPlanDate).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.NONYU.ColNo))) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '        '届先JISが違う場合、エラー
                '        If (destJisCd).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.DEST_JIS_CD.ColNo))) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '        '届先コード、みなし届先の両方が違う場合、エラー
                '        If (destCd).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.DEST_CD.ColNo))) = False AndAlso _
                '            (minashiDestCd).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.MINASHI_DEST_CD.ColNo))) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '    ElseIf (LMF040C.NRS_BR_CD_10).Equals(nrsBrCd) = True Then
                '        '②千葉のまとめ対象荷主以外の場合

                '        '届先コード、みなし届先の両方が違う場合、エラー
                '        If (destCd).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.DEST_CD.ColNo))) = False AndAlso _
                '            (minashiDestCd).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.MINASHI_DEST_CD.ColNo))) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '    ElseIf (LMF040C.DIC_MATOME_02).Equals(matomeKb) = True Then
                '        '③群馬のまとめ対象荷主の場合

                '        '出荷日が違う場合、エラー
                '        If (outkaPlanDate).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.SHUKKA.ColNo))) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '        '便区分が違う場合、エラー
                '        If (binKb).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.BIN_KB.ColNo))) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '        '届先コード、みなし届先の両方が違う場合、エラー
                '        If (destCd).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.DEST_CD.ColNo))) = False AndAlso _
                '            (minashiDestCd).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.MINASHI_DEST_CD.ColNo))) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '    ElseIf (LMF040C.NRS_BR_CD_30).Equals(nrsBrCd) = True Then
                '        '④群馬のまとめ対象荷主以外の場合

                '        '出荷日が違う場合、エラー
                '        If (outkaPlanDate).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.SHUKKA.ColNo))) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '        '届先コード、みなし届先の両方が違う場合、エラー
                '        If (destCd).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.DEST_CD.ColNo))) = False AndAlso _
                '            (minashiDestCd).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.MINASHI_DEST_CD.ColNo))) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '    ElseIf (LMF040C.DIC_MATOME_03).Equals(matomeKb) = True Then
                '        '⑤埼玉のまとめ対象荷主の場合

                '        '出荷日が違う場合、エラー
                '        If (outkaPlanDate).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.SHUKKA.ColNo))) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '        '在庫部課コードが違う場合、エラー
                '        If (zBukaCd).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.ZBUKA_CD.ColNo))) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '        '扱い部課コードが違う場合、エラー
                '        If (aBukaCd).Equals(Mid(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.ABUKA_CD.ColNo)), 1, 5)) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '        '便区分が違う場合、エラー
                '        If (binKb).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.BIN_KB.ColNo))) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '        '届先コード、みなし届先の両方が違う場合、エラー
                '        If (destCd).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.DEST_CD.ColNo))) = False AndAlso _
                '            (minashiDestCd).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.MINASHI_DEST_CD.ColNo))) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '    ElseIf (LMF040C.NRS_BR_CD_50).Equals(nrsBrCd) = True Then
                '        '⑥埼玉のまとめ対象荷主以外の場合

                '        '納入日が違う場合、エラー
                '        If (arrPlanDate).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.NONYU.ColNo))) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '        '届先コード、みなし届先の両方が違う場合、エラー
                '        If (destCd).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.DEST_CD.ColNo))) = False AndAlso _
                '            (minashiDestCd).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.MINASHI_DEST_CD.ColNo))) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '    ElseIf (LMF040C.NRS_BR_CD_55).Equals(nrsBrCd) = True Then
                '        '⑦春日部の場合

                '        '出荷日が違う場合、エラー
                '        If (outkaPlanDate).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.SHUKKA.ColNo))) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '        '扱い部課コードが違う場合、エラー
                '        If (aBukaCd).Equals(Mid(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.ABUKA_CD.ColNo)), 1, 5)) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '        '便区分が違う場合、エラー
                '        If (binKb).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.BIN_KB.ColNo))) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If

                '        '届先コード、みなし届先の両方が違う場合、エラー
                '        If (destCd).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.DEST_CD.ColNo))) = False AndAlso _
                '            (minashiDestCd).Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.MINASHI_DEST_CD.ColNo))) = False Then
                '            Return Me.SetErrMessageE239()
                '        End If
                '    Else
                '        '検索時に条件を絞っているので、ELSEに来ることはありえない
                '        Return Me.SetErrMessageE239()
                '    End If

                'Next
                'END YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない

            End If
            'END YANAI 20120622 DIC運賃まとめ及び再計算対応

        End With

        Return True

    End Function

    ''' <summary>
    ''' まとめ条件の日付を取得
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>運送日</returns>
    ''' <remarks></remarks>
    Private Function GetUnsoDate(ByVal rowNo As Integer) As String

        GetUnsoDate = String.Empty

        With Me._Frm.sprDetail.ActiveSheet

            '締め日基準が入荷の場合
            If LMFControlC.CALC_NYUKA.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo))) = True Then

                GetUnsoDate = DateFormatUtility.DeleteSlash(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.NONYU.ColNo)))

            Else

                GetUnsoDate = DateFormatUtility.DeleteSlash(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.SHUKKA.ColNo)))

            End If

        End With

        Return GetUnsoDate

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

            '中継配送有の場合、運行番号(配荷)
            If LMFControlC.FLG_ON.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.TYUKEI_HAISO_FLG.ColNo))) = True Then
                Return New String() {Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.TRIP_NO_SHUKA.ColNo)) _
                                     , Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.TRIP_NO_CHUKEI.ColNo)) _
                                     , Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.TRIP_NO_HAIKA.ColNo)) _
                                     }
            End If

            Return New String() {Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.TRIP_NO.ColNo))}

        End With

    End Function

    ''' <summary>
    ''' まとめ候補による条件チェック
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <param name="orderBy">検索押下時のまとめ候補</param>
    ''' <param name="item1">アイテム1</param>
    ''' <param name="item2">アイテム2</param>
    ''' <param name="item3">アイテム3(配列)</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsOrderByChk(ByVal rowNo As Integer, ByVal orderBy As String, ByVal item1 As String, ByVal item2 As String, ByVal item3 As String()) As Boolean

        With Me._Frm.sprDetail.ActiveSheet

            Select Case orderBy

                Case LMF040C.ORDER_BY_CUSTTRIP

                    '荷主(大)コードが違う場合、エラー
                    If item1.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.CUST_CD_L.ColNo))) = False Then
                        Return False
                    End If

                    '荷主(中)コードが違う場合、エラー
                    If item2.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.CUST_CD_M.ColNo))) = False Then
                        Return Me.SetErrMessageE239()
                    End If

                    '運行番号が違う場合、エラー
                    If Me.IsTripDotiChk(rowNo, item3) = False Then
                        Return False
                    End If

                Case LMF040C.ORDER_BY_DEST

                    '届先コードが違う場合、エラー
                    If item1.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.DEST_CD.ColNo))) = False Then
                        Return False
                    End If

                Case LMF040C.ORDER_BY_DESTJIS

                    '届先JISが違う場合、エラー
                    If item1.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.DEST_JIS_CD.ColNo))) = False Then
                        Return False
                    End If

            End Select

            Return True

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

        '集荷番号でチェック
        If Me.IsTripDotiChk(arr, max, value(1), LMF040G.sprDetailDef.TRIP_NO_SHUKA.ColNo) = True Then
            Return True
        End If

        '中継番号でチェック
        Return Me.IsTripDotiChk(arr, max, value(2), LMF040G.sprDetailDef.TRIP_NO_CHUKEI.ColNo)

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

        Dim chkData As String = Me._Gcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.TARIFF_KBN.ColNo))

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
        rtnResult = rtnResult AndAlso Me._Vcon.IsSelectLimitChk(cnt, LMF040C.IKKATU_LMF040)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 請求先の同値チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <param name="max">選択件数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsIkkatuDotiChk(ByVal arr As ArrayList, ByVal max As Integer) As Boolean

        With Me._Frm.sprDetail.ActiveSheet

            Select Case Me._Frm.cmbShusei.SelectedValue.ToString()

                Case LMF040C.SHUSEI_SEIQTO

                    '選択した請求先コードが同じでない場合、エラー
                    Dim seiqto As String = Me._Gcon.GetCellValue(.Cells(Convert.ToInt32(arr(0)), LMF040G.sprDetailDef.SEIQTO_CD.ColNo))
                    For i As Integer = 1 To max

                        If seiqto.Equals(Me._Gcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMF040G.sprDetailDef.SEIQTO_CD.ColNo))) = False Then
                            Return Me._Vcon.SetErrMessage("E227", New String() {Me._Vcon.SetRepMsgData(LMF040G.sprDetailDef.SEIQTO_CD.ColName)})
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
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMF040C.ActionType) As Boolean

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            'ポップ対象外の場合
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMF040C.ActionType.MASTEROPEN) = True Then
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

                Case .txtDriverCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtDriverCd}
                    lblCtl = New Control() {.lblDriverNm}
                    msg = New String() {String.Concat(.lblTitleDriver.Text, LMFControlC.CD)}

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    Dim custNm As String = .lblTitleCust.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtCustCdL, .txtCustCdM}
                    lblCtl = New Control() {.lblCustNm}
                    msg = New String() {String.Concat(custNm, LMFControlC.CD), String.Concat(custNm, LMFControlC.BR_CD)}

                Case .txtShuseiL.Name

                    txtCtl = Me.FocusCtl()
                    msg = New String() {String.Concat(LMF040C.HENKO, LMFControlC.L_NM) _
                                        , String.Concat(LMF040C.HENKO, LMFControlC.M_NM) _
                                        , String.Concat(LMF040C.HENKO, LMFControlC.S_NM) _
                                        , String.Concat(LMF040C.HENKO, LMFControlC.SS_NM) _
                                        }

                Case .txtShuseiM.Name, .txtShuseiS.Name, .txtShuseiSS.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtShuseiL, .txtShuseiM, .txtShuseiS, .txtShuseiSS}
                    msg = New String() {String.Concat(LMF040C.HENKO, LMFControlC.L_NM) _
                                        , String.Concat(LMF040C.HENKO, LMFControlC.M_NM) _
                                        , String.Concat(LMF040C.HENKO, LMFControlC.S_NM) _
                                        , String.Concat(LMF040C.HENKO, LMFControlC.SS_NM) _
                                        }

                    '----->要望番号:928
                Case .txtDestCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtDestCd}
                    lblCtl = New Control() {.lblDestNm}
                    msg = New String() {String.Concat(.lblTitleDest.Text, LMFControlC.CD)}
                    '<-----要望番号:928

            End Select

            'フォーカス位置チェック
            Dim rtnResult As Boolean = Me._Vcon.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)

            'START YANAI 要望番号568
            ''営業所必須
            'Return rtnResult AndAlso Me.IsNrsHissuChk(objNm)
            '営業所必須
            rtnResult = rtnResult AndAlso Me.IsNrsHissuChk(objNm)

            'START YANAI 要望番号1394 マスタ参照時、関係のない箇所で入力チェックにひっかかる
            ''バイト数チェック
            'rtnResult = rtnResult AndAlso Me.IsIkkatsuByteCheck()
            'バイト数チェック
            rtnResult = rtnResult AndAlso Me.IsIkkatsuByteCheck(objNm)
            'END YANAI 要望番号1394 マスタ参照時、関係のない箇所で入力チェックにひっかかる

            Return rtnResult
            'END YANAI 要望番号568

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

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    ''' <summary>
    ''' 必須チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsVisibleKbHissuChk() As Boolean

        Return Me.IsHissuChk(Me._Frm.cmbVisibleKb, Me._Frm.lblTitleVisibleKb.Text)

    End Function
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

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

                Case .txtShuseiL.Name, .txtShuseiM.Name, .txtShuseiS.Name, .txtShuseiSS.Name

                    Return True

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

            Select Case .cmbShusei.SelectedValue.ToString()

                Case LMF040C.SHUSEI_CUST

                    FocusCtl = New Win.InputMan.LMImTextBox() {.txtShuseiL, .txtShuseiM, .txtShuseiS, .txtShuseiSS}

                Case Else

                    FocusCtl = New Win.InputMan.LMImTextBox() {.txtShuseiL}

            End Select

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
    'START YANAI 要望番号568

    'START YANAI 要望番号1394 マスタ参照時、関係のない箇所で入力チェックにひっかかる
    '''' <summary>
    '''' 一括変更時のバイトチェック
    '''' </summary>
    '''' <returns>True:エラーなし,OK False:エラーあり</returns>
    '''' <remarks></remarks>
    'Private Function IsIkkatsuByteCheck() As Boolean
    ''' <summary>
    ''' 一括変更時のバイトチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsIkkatsuByteCheck(ByVal objNm As String) As Boolean
        'END YANAI 要望番号1394 マスタ参照時、関係のない箇所で入力チェックにひっかかる

        With Me._Frm

            'START YANAI 要望番号1394 マスタ参照時、関係のない箇所で入力チェックにひっかかる
            If (objNm).Equals(.txtShuseiL.Name) = False AndAlso _
                (objNm).Equals(.txtShuseiM.Name) = False AndAlso _
                (objNm).Equals(.txtShuseiS.Name) = False AndAlso _
                (objNm).Equals(.txtShuseiSS.Name) = False Then
                Return True
            End If
            'END YANAI 要望番号1394 マスタ参照時、関係のない箇所で入力チェックにひっかかる

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '変更コード(大)
            .txtShuseiL.ItemName = String.Concat(LMF040C.HENKO, LMFControlC.CD, LMFControlC.L_NM)
            Select Case .cmbShusei.SelectedIndex
                Case 1 '請求先コード
                    .txtShuseiL.IsByteCheck = 7

                Case 2 'タリフコード
                    .txtShuseiL.IsByteCheck = 10

                Case 3 '横持ちタリフコード
                    .txtShuseiL.IsByteCheck = 10

                Case 4 '荷主コード
                    .txtShuseiL.IsByteCheck = 5

                    'START YANAI 要望番号996
                Case 5 '割増タリフコード
                    .txtShuseiL.IsByteCheck = 10
                    'END YANAI 要望番号996

                    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
                Case 6 '在庫部課
                    .txtShuseiL.IsByteCheck = 7

                Case 7 '扱い部課
                    .txtShuseiL.IsByteCheck = 7
                    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

                Case 8 '距離程コード
                    .txtShuseiL.IsByteCheck = 3

            End Select
            If MyBase.IsValidateCheck(.txtShuseiL) = errorFlg Then
                Return errorFlg
            End If

            Return True

        End With

    End Function
    'END YANAI 要望番号568

    'START YANAI 要望番号561
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
    'END YANAI 要望番号561

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
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
        rtnResult = rtnResult AndAlso Me._Vcon.IsSelectLimitChk(cnt, LMF040C.SAIKEISAN_LMF040)

        Return rtnResult

    End Function
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthority(ByVal actionType As LMF040C.ActionType) As Boolean

        Dim kengen As String = LMUserInfoManager.GetAuthoLv()
        Dim kengenFlg As Boolean = True

        Select Case actionType

            'START YANAI 要望番号582
            Case LMF040C.ActionType.PRINT

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
                'END YANAI 要望番号582

                'START YANAI 要望番号561
            Case LMF040C.ActionType.LOOPEDIT

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
                'END YANAI 要望番号561

            Case LMF040C.ActionType.FIX

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

            Case LMF040C.ActionType.FIX_CANCELL

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

            Case LMF040C.ActionType.GROUP

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

            Case LMF040C.ActionType.GROUP_CANCELL

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

            Case LMF040C.ActionType.KENSAKU

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

            Case LMF040C.ActionType.ENTER

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

            Case LMF040C.ActionType.MASTEROPEN

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

            Case LMF040C.ActionType.DOUBLECLICK


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

            Case LMF040C.ActionType.SAVE

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

            Case LMF040C.ActionType.CLOSE

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

        End Select

        Return Me._Vcon.IsAuthorityChk(kengenFlg)

    End Function

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        'ヘッダ項目のスペース除去
        Call Me.TrimHeaderSpaceTextValue(LMF040C.ActionType.KENSAKU)

        'スプレッドのスペース除去
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0)

    End Sub

    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimHeaderSpaceTextValue(ByVal actionType As LMF040C.ActionType)

        With Me._Frm

            Select Case actionType

                Case LMF040C.ActionType.KENSAKU

                    .txtTariffCd.TextValue = .txtTariffCd.TextValue.Trim()
                    .txtExtcCd.TextValue = .txtExtcCd.TextValue.Trim()
                    .txtDriverCd.TextValue = .txtDriverCd.TextValue.Trim()
                    .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
                    .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()
                    '----->要望番号:928
                    .txtDestCd.TextValue = .txtDestCd.TextValue.Trim()
                    '<-----要望番号:928

                Case Else

                    .txtShuseiL.TextValue = .txtShuseiL.TextValue.Trim()
                    .txtShuseiM.TextValue = .txtShuseiM.TextValue.Trim()
                    .txtShuseiS.TextValue = .txtShuseiS.TextValue.Trim()
                    .txtShuseiSS.TextValue = .txtShuseiSS.TextValue.Trim()

            End Select

        End With

    End Sub

#End Region 'Method

End Class
