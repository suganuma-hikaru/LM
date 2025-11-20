' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF010V : 運行・運送情報
'  作  成  者       :  kishi
' ==========================================================================
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMF010Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMF010V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF010F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF010F, ByVal v As LMFControlV, ByVal g As LMFControlG)

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
    Friend Function IsInputCheck(ByVal g As LMF010G) As Boolean 'i'Friend Function IsInputCheck() As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        Dim rtnResult As Boolean = Me.IsHeaderChk()
        'i'rtnResult = rtnResult AndAlso Me.IsSprChk()
        rtnResult = rtnResult AndAlso Me.IsSprChk(g)

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

            '輸送部営業所
            rtnResult = rtnResult AndAlso Me.IsHissuChk(.cmbBetsuEigyo, .lblTitleBetsuEigyo.Text)

            '運送会社コード(1次)
            '2016.01.06 UMANO 英語化対応START
            'rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtUnsocoCd1, String.Concat(.lblTitleUnsoco1.Text, LMFControlC.CD), 5)
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtUnsocoCd1, .sprUnsoUnkou.ActiveSheet.GetColumnLabel(0, LMF010C.SprColumnIndex.UNSOCO_CD_1), 5)
            '2016.01.06 UMANO 英語化対応END

            '運送会社支店コード(1次)
            '2016.01.06 UMANO 英語化対応START
            'rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtUnsocoBrCd1, String.Concat(.lblTitleUnsoco1.Text, LMFControlC.BR_CD), 3)
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtUnsocoBrCd1, .sprUnsoUnkou.ActiveSheet.GetColumnLabel(0, LMF010C.SprColumnIndex.UNSOCO_BR_CD_1), 3)
            '2016.01.06 UMANO 英語化対応END

            '運送会社コード(1次)
            '2016.01.06 UMANO 英語化対応START
            'rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtUnsocoCd2, String.Concat(.lblTitleUnsoco2.Text, LMFControlC.CD), 5)
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtUnsocoCd2, .sprUnsoUnkou.ActiveSheet.GetColumnLabel(0, LMF010C.SprColumnIndex.UNSOCO_CD_2), 5)
            '2016.01.06 UMANO 英語化対応END

            '運送会社支店コード(1次)
            '2016.01.06 UMANO 英語化対応START
            'rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtUnsocoBrCd2, String.Concat(.lblTitleUnsoco2.Text, LMFControlC.BR_CD), 3)
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtUnsocoBrCd2, .sprUnsoUnkou.ActiveSheet.GetColumnLabel(0, LMF010C.SprColumnIndex.UNSOCO_BR_CD_2), 3)
            '2016.01.06 UMANO 英語化対応END

            '荷主(大)コード
            '2016.01.06 UMANO 英語化対応START
            'rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtCustCdL, String.Concat(.lblTitleCustCd.Text, LMFControlC.L_NM, LMFControlC.CD), 5)
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtCustCdL, .lblTitleCustCd.Text, 5)
            '2016.01.06 UMANO 英語化対応END

            '荷主(中)コード
            '2016.01.06 UMANO 英語化対応START
            'rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtCustCdM, String.Concat(.lblTitleCustCd.Text, LMFControlC.M_NM, LMFControlC.CD), 2)
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtCustCdM, .lblTitleCustCd.Text, 2)
            '2016.01.06 UMANO 英語化対応END

            '作成者コード
            rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(.txtCntUserCd, String.Concat(.lblTitleCrtUser.Text, LMFControlC.CD), 5)

            '日付(From)
            '2016.01.06 UMANO 英語化対応START
            'rtnResult = rtnResult AndAlso Me._Vcon.IsInputDateFullByteChk(.imdTripDateFrom, String.Concat(LMFControlC.DATE_NM, LMFControlC.FROM_NM))
            rtnResult = rtnResult AndAlso Me._Vcon.IsInputDateFullByteChk(.imdTripDateFrom, String.Concat(.cmbDateKb.SelectedItem, LMFControlC.FROM_NM))
            '2016.01.06 UMANO 英語化対応END

            '日付(To)
            '2016.01.06 UMANO 英語化対応START
            'rtnResult = rtnResult AndAlso Me._Vcon.IsInputDateFullByteChk(.imdTripDateTo, String.Concat(LMFControlC.DATE_NM, LMFControlC.TO_NM))
            rtnResult = rtnResult AndAlso Me._Vcon.IsInputDateFullByteChk(.imdTripDateTo, String.Concat(.cmbDateKb.SelectedItem, LMFControlC.TO_NM))
            '2016.01.06 UMANO 英語化対応END

            'START YANAI 要望番号737 運送検索画面：全体が見えるようにする
            '項目表示
            rtnResult = rtnResult AndAlso Me.IsHissuChk(.cmbVisibleKb, .lblTitleVisibleKb.Text)
            'END YANAI 要望番号737 運送検索画面：全体が見えるようにする

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' スプレッドの単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSprChk(ByVal g As LMF010G) As Boolean 'i'Private Function IsSprChk() As Boolean

        Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprUnsoUnkou)
        'i'
        Dim sprUnsoUnkouDef As LMF010G.sprUnsoUnkouDefault = DirectCast(g.objSprDef, Jp.Co.Nrs.LM.GUI.LMF010G.sprUnsoUnkouDefault)

        '運送番号
        'i'
        Dim rtnResult As Boolean = Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                           , sprUnsoUnkouDef.UNSO_NO_L.ColNo _
                                                           , sprUnsoUnkouDef.UNSO_NO_L.ColName, 9)

        '運送会社(2次)名
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.UNSOCO_2.ColNo _
                                                              , sprUnsoUnkouDef.UNSOCO_2.ColName, 122)

        '荷主参照番号
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.CUST_REF_NO.ColNo _
                                                              , sprUnsoUnkouDef.CUST_REF_NO.ColName, 30)

        '発地名
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.ORIG_NM.ColNo _
                                                              , sprUnsoUnkouDef.ORIG_NM.ColName, 80)

        '届先名
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.DEST_NM.ColNo _
                                                              , sprUnsoUnkouDef.DEST_NM.ColName, 80)

        '届先住所
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.DEST_AD.ColNo _
                                                              , sprUnsoUnkouDef.DEST_AD.ColName, 40)

        'エリア名
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.AREA.ColNo _
                                                              , sprUnsoUnkouDef.AREA.ColName, 20)

        '管理番号
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.INOUTKA_NO_L.ColNo _
                                                              , sprUnsoUnkouDef.INOUTKA_NO_L.ColName, 9)

        '運行番号
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.TRIP_NO.ColNo _
                                                              , sprUnsoUnkouDef.TRIP_NO.ColName, 10)

        '乗務員
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.DRIVER.ColNo _
                                                              , sprUnsoUnkouDef.DRIVER.ColName, 20)

        '車番
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.CAR_NO.ColNo _
                                                              , sprUnsoUnkouDef.CAR_NO.ColName, 20)

        '運送会社(1次)名
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.UNSOCO_1.ColNo _
                                                              , sprUnsoUnkouDef.UNSOCO_1.ColName, 122)

        '荷主名
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.CUST_NM.ColNo _
                                                              , sprUnsoUnkouDef.CUST_NM.ColName, 122)

        '備考
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.UNSO_REM.ColNo _
                                                              , sprUnsoUnkouDef.UNSO_REM.ColName, 100)

        'まとめ番号
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.GROUP_NO.ColNo _
                                                              , sprUnsoUnkouDef.GROUP_NO.ColName, 9)

        '集荷中継地
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.SHUKA_RELY_POINT.ColNo _
                                                              , sprUnsoUnkouDef.SHUKA_RELY_POINT.ColName, 50)

        '配荷中継地
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.HAIKA_RELY_POINT.ColNo _
                                                              , sprUnsoUnkouDef.HAIKA_RELY_POINT.ColName, 50)

        '運行番号(集荷)
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.TRIP_NO_SHUKA.ColNo _
                                                              , sprUnsoUnkouDef.TRIP_NO_SHUKA.ColName, 10)

        '運行番号(中継)
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.TRIP_NO_CHUKEI.ColNo _
                                                              , sprUnsoUnkouDef.TRIP_NO_CHUKEI.ColName, 10)

        '運行番号(配荷)
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.TRIP_NO_HAIKA.ColNo _
                                                              , sprUnsoUnkouDef.TRIP_NO_HAIKA.ColName, 10)

        '運送会社(集荷)
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.UNSOCO_SHUKA.ColNo _
                                                              , sprUnsoUnkouDef.UNSOCO_SHUKA.ColName, 122)

        '運送会社(中継)
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.UNSOCO_CHUKEI.ColNo _
                                                              , sprUnsoUnkouDef.UNSOCO_CHUKEI.ColName, 122)

        '運送会社(配荷)
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.UNSOCO_HAIKA.ColNo _
                                                              , sprUnsoUnkouDef.UNSOCO_HAIKA.ColName, 122)

        '作成者
        'i'
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                              , sprUnsoUnkouDef.CNT_USER.ColNo _
                                                              , sprUnsoUnkouDef.CNT_USER.ColName, 20)

        '送り状番号
        rtnResult = rtnResult AndAlso Me._Vcon.IsKinsiByteChk(vCell, 0 _
                                                      , sprUnsoUnkouDef.DENP_NO.ColNo _
                                                      , sprUnsoUnkouDef.DENP_NO.ColName, 80)


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
            Dim rtnResult As Boolean =  Me._Vcon.IsDateCtlConnectionChk(.cmbDateKb, .imdTripDateFrom, .imdTripDateTo)


            '2016.01.06 UMANO 英語化対応START
            'From + Toの大小チェック
            'rtnResult = rtnResult AndAlso Me._Vcon.IsDateFromToChk(.imdTripDateFrom _
            '                                                       , .imdTripDateTo _
            '                                                       , String.Concat(LMFControlC.DATE_NM, LMFControlC.TO_NM) _
            '                                                       , String.Concat(LMFControlC.DATE_NM, LMFControlC.FROM_NM) _
            '                                                       )

            rtnResult = rtnResult AndAlso Me._Vcon.IsDateFromToChk(.imdTripDateFrom _
                                                       , .imdTripDateTo _
                                                       , String.Concat(.cmbDateKb.SelectedItem, LMFControlC.TO_NM) _
                                                       , String.Concat(.cmbDateKb.SelectedItem, LMFControlC.FROM_NM) _
                                                       )

            '2016.01.06 UMANO 英語化対応END

            Return rtnResult

        End With

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
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMF010C.ActionType) As Boolean

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            'ポップ対象外の場合
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMF010C.ActionType.MASTEROPEN) = True Then
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

                Case .txtUnsocoCd0.Name, .txtUnsocoBrCd0.Name

                    Dim unsoNm0 As String = .lblTitleUnsoco.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtUnsocoCd0, .txtUnsocoBrCd0}
                    lblCtl = New Control() {.lblUnsocoNm0}
                    msg = New String() {String.Concat(unsoNm0, LMFControlC.CD), String.Concat(unsoNm0, LMFControlC.BR_CD)}

                Case .txtUnsocoCd1.Name, .txtUnsocoBrCd1.Name

                    Dim unsoNm1 As String = .lblTitleUnsoco1.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtUnsocoCd1, .txtUnsocoBrCd1}
                    lblCtl = New Control() {.lblUnsocoNm1}
                    msg = New String() {String.Concat(unsoNm1, LMFControlC.CD), String.Concat(unsoNm1, LMFControlC.BR_CD)}

                Case .txtUnsocoCd2.Name, .txtUnsocoBrCd2.Name

                    Dim unsoNm2 As String = .lblTitleUnsoco2.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtUnsocoCd2, .txtUnsocoBrCd2}
                    lblCtl = New Control() {.lblUnsocoNm2}
                    msg = New String() {String.Concat(unsoNm2, LMFControlC.CD), String.Concat(unsoNm2, LMFControlC.BR_CD)}

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    Dim custNm As String = .lblTitleCustCd.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtCustCdL, .txtCustCdM}
                    lblCtl = New Control() {.lblCustNm}
                    msg = New String() {String.Concat(custNm, LMFControlC.L_NM, LMFControlC.CD) _
                                        , String.Concat(custNm, LMFControlC.M_NM, LMFControlC.CD)}

                Case .txtTripNo.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtTripNo}
                    msg = New String() {.lblTitleTripNo.Text}

                Case .txtCntUserCd.Name

                    'Enterでない場合、エラー
                    If LMF010C.ActionType.ENTER <> actionType Then
                        Return Me._Vcon.SetFocusErrMessage()
                    End If

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtCntUserCd}
                    lblCtl = New Control() {.lblCntUserNm}
                    msg = New String() {.lblTitleCrtUser.Text}

                Case Else
                    'ポップ対象外の場合
                    'マスタ参照の場合、エラーメッセージ設定
                    If actionType.Equals(LMF010C.ActionType.MASTEROPEN) = True Then
                        Call Me._Vcon.SetFocusErrMessage(False)
                    End If

            End Select

            'フォーカス位置チェック
            Dim rtnResult As Boolean = Me._Vcon.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)

            '営業所必須
            Return rtnResult AndAlso Me.IsNrsHissuChk(objNm, actionType)

        End With

    End Function

    ''' <summary>
    ''' 営業所の必須チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsNrsHissuChk(ByVal objNm As String, ByVal actionType As LMF010C.ActionType) As Boolean

        With Me._Frm

            '編集項目のPopは営業所のチェックはしない
            Select Case objNm


                Case .txtUnsocoCd0.Name, .txtUnsocoBrCd0.Name, .txtTripNo.Name

                    Return True

            End Select

            '営業所必須
            Return Me.IsNrsHissuChk()

        End With

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthority(ByVal actionType As LMF010C.ActionType) As Boolean

        Dim kengen As String = LMUserInfoManager.GetAuthoLv()
        Dim kengenFlg As Boolean = True

        Select Case actionType

            Case LMF010C.ActionType.UNCO_NEW

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

            Case LMF010C.ActionType.UNCO_EDIT

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
                        'START YANAI 20120615 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END YANAI 20120615 外部権限の変更(春日部対応)
                End Select

            Case LMF010C.ActionType.UNSO_NEW

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

            Case LMF010C.ActionType.KENSAKU

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
                        'START YANAI 20120615 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END YANAI 20120615 外部権限の変更(春日部対応)
                End Select

            Case LMF010C.ActionType.MASTEROPEN

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
                        'START YANAI 20120615 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END YANAI 20120615 外部権限の変更(春日部対応)
                End Select

            Case LMF010C.ActionType.ENTER

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
                        'START YANAI 20120615 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END YANAI 20120615 外部権限の変更(春日部対応)
                End Select

            Case LMF010C.ActionType.DOUBLECLICK

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
                        'START YANAI 20120615 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END YANAI 20120615 外部権限の変更(春日部対応)
                End Select

            Case LMF010C.ActionType.CLOSE

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

            Case LMF010C.ActionType.SAVE

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

            Case LMF010C.ActionType.BTN_UNCO_EDIT

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

                '2012.06.22 要望番号1189 追加START
            Case LMF010C.ActionType.BTN_UNSO_PRINT

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
                '2012.06.22 要望番号1189 追加END


                '(2012.08.13) 要望番号1341 車載受注渡し対応 --- STRAT ---
            Case LMF010C.ActionType.SYASAI_WATASHI
                '(F8)車載受注渡し押下時
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
                '(2012.08.13) 要望番号1341 車載受注渡し対応 ---  END  ---

                '2017/02/27 追加START
            Case LMF010C.ActionType.BTN_UNSOCO_PRINT

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
                '2017/02/27 追加START追加END


                '(2013.01.18) 要望番号1617 出荷編集遷移対応 --- STRAT ---
            Case LMF010C.ActionType.OUTKA_EDIT

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
                '(2013.01.18) 要望番号1617 出荷編集遷移対応 ---  END  ---

        End Select

        Return Me._Vcon.IsAuthorityChk(kengenFlg)

    End Function

    '2012.06.22 要望番号1189 追加START
    ''' <summary>
    ''' 印刷チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsPrintChk(ByVal arr As ArrayList) As Boolean

        ''スペース除去
        'Call Me.TrimHeaderSpaceTextValue(LMF010C.ActionType.BTN_UNSO_PRINT)

        '未選択チェック
        Dim cnt As Integer = arr.Count
        Dim rtnResult As Boolean = Me._Vcon.IsSelectChk(cnt)

        ''選択上限チェック
        'rtnResult = rtnResult AndAlso Me._Vcon.IsSelectLimitChk(cnt, LMF010C.PRINT_LMF010)

        '単項目チェック
        'rtnResult = rtnResult AndAlso Me.IsPrintInputCheck()

        ''マスタ存在チェック
        'rtnResult = rtnResult AndAlso Me.IsMstExistChk()

        Return rtnResult

    End Function
    '2012.06.22 要望番号1189 追加END

    ''' <summary>
    ''' 印刷時の単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintInputCheck() As Boolean

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '印刷種別
            .cmbPrintKb.ItemName = LMF010C.PRINT_KB
            .cmbPrintKb.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbPrintKb) = errorFlg Then
                Return errorFlg
            End If

            Return chkFlg

        End With

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' ダブルクリックチェック
    ''' </summary>
    ''' <param name="rowNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsDoubleClickChk(ByVal rowNo As Integer, ByVal g As LMF010G) As Boolean 'i'Friend Function IsDoubleClickChk(ByVal rowNo As Integer) As Boolean

        '選択行データチェック
        'i'Dim rtnResult As Boolean = Me.IsClickDataCheck(rowNo)
        Dim rtnResult As Boolean = Me.IsClickDataCheck(rowNo, g)

        Return rtnResult

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 選択行データチェック
    ''' </summary>
    ''' <param name="rowNo"></param>''' 
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsClickDataCheck(ByVal rowNo As Integer, ByVal g As LMF010G) As Boolean 'Private Function IsClickDataCheck(ByVal rowNo As Integer) As Boolean

        'i'
        Dim sprUnsoUnkouDef As LMF010G.sprUnsoUnkouDefault = DirectCast(g.objSprDef, Jp.Co.Nrs.LM.GUI.LMF010G.sprUnsoUnkouDefault)

        With Me._Frm.sprUnsoUnkou.ActiveSheet

            '支払運賃が確定済の場合はエラー
            'i'If LMFControlC.FLG_ON.Equals(Me._Gcon.GetCellValue(.Cells(Convert.ToInt32(rowNo), LMF010G.sprUnsoUnkouDef.SHIHARAI_FIXED_FLAG.ColNo))) = True Then
            'i'    Me._Vcon.SetErrMessage("E497", New String() {"編集できません。"})
            'i'    Return False
            ''End If
            If LMFControlC.FLG_ON.Equals(Me._Gcon.GetCellValue(.Cells(Convert.ToInt32(rowNo), sprUnsoUnkouDef.SHIHARAI_FIXED_FLAG.ColNo))) = True Then
                '2016.01.06 UMANO 英語化対応START
                'Me._Vcon.SetErrMessage("E497", New String() {"編集できません。"})
                Me._Vcon.SetErrMessage("E497", New String() {String.Empty})
                '2016.01.06 UMANO 英語化対応END
                Return False
            End If

            Return True

        End With

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 一括変更チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsSaveChk(ByVal arr As ArrayList) As Boolean

        'スペース除去
        Call Me.TrimHeaderSpaceTextValue(LMF010C.ActionType.SAVE)

        '未選択チェック
        Dim cnt As Integer = arr.Count
        Dim rtnResult As Boolean = Me._Vcon.IsSelectChk(cnt)

        '選択上限チェック
        rtnResult = rtnResult AndAlso Me._Vcon.IsSelectLimitChk(cnt, LMF010C.IKKATU_LMF010)

        '単項目チェック
        rtnResult = rtnResult AndAlso Me.IsSaveInputCheck()

        'マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsMstExistChk()

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
            .cmbShuSei.ItemName = .lblTitleSyusei.Text
            .cmbShuSei.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbShuSei) = errorFlg Then
                Return errorFlg
            End If

            Dim henkoKbn As String = .cmbShuSei.SelectedValue.ToString()

            '運行番号
            .txtTripNo.ItemName = .lblTitleTripNo.Text
            .txtTripNo.IsHissuCheck = .optEventY.Checked AndAlso LMF010C.SHUSEI_TRIP.Equals(henkoKbn)
            .txtTripNo.IsForbiddenWordsCheck = chkFlg
            .txtTripNo.IsFullByteCheck = 10
            .txtTripNo.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtTripNo) = errorFlg Then
                Return errorFlg
            End If

            '運送会社コード
            .txtUnsocoCd0.ItemName = String.Concat(.lblTitleUnsoco.Text, LMFControlC.CD)
            .txtUnsocoCd0.IsHissuCheck = .optEventY.Checked AndAlso LMF010C.SHUSEI_UNSOCO.Equals(henkoKbn)
            .txtUnsocoCd0.IsForbiddenWordsCheck = chkFlg
            .txtUnsocoCd0.IsByteCheck = 5
            .txtUnsocoCd0.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtUnsocoCd0) = errorFlg Then
                Return errorFlg
            End If

            '便区分
            .cmbBinKb.ItemName = .lblTitleBinKb.Text
            .cmbBinKb.IsHissuCheck = .optEventY.Checked AndAlso LMF010C.SHUSEI_BIN.Equals(henkoKbn)
            If MyBase.IsValidateCheck(.cmbBinKb) = errorFlg Then
                Return errorFlg
            End If

            '運送会社支店コード
            .txtUnsocoBrCd0.ItemName = String.Concat(.lblTitleUnsoco.Text, LMFControlC.BR_CD)
            .txtUnsocoBrCd0.IsHissuCheck = .optEventY.Checked AndAlso LMF010C.SHUSEI_UNSOCO.Equals(henkoKbn)
            .txtUnsocoBrCd0.IsForbiddenWordsCheck = chkFlg
            .txtUnsocoBrCd0.IsByteCheck = 3
            .txtUnsocoBrCd0.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtUnsocoBrCd0) = errorFlg Then
                Return errorFlg
            End If

            '中継地の必須チェック
            Return Me.IsChukeiHissuChk(henkoKbn)

        End With

    End Function

    ''' <summary>
    ''' 中継地の必須チェック
    ''' </summary>
    ''' <param name="henkoKbn">変更区分</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsChukeiHissuChk(ByVal henkoKbn As String) As Boolean

        With Me._Frm

            '更新区分 <> 更新の場合、True
            If .optEventY.Checked = False Then
                Return True
            End If

            '変更する内容が中継地以外、True
            If LMF010C.SHUSEI_CHUKEI.Equals(henkoKbn) = False Then
                Return True
            End If

            '中継地From + Toに値がない場合、エラー
            If String.IsNullOrEmpty(.cmbChukeiFrom.SelectedValue.ToString()) = True _
                AndAlso String.IsNullOrEmpty(.cmbChukeiTo.SelectedValue.ToString()) = True Then
                .cmbChukeiTo.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._Vcon.SetErrorControl(.cmbChukeiFrom)
                '2016.01.06 UMANO 英語化対応START
                'Return Me._Vcon.SetErrMessage("E270", New String() {String.Concat(LMF010C.SHUKA, .lblTitleChukeichi.Text), String.Concat(LMF010C.HAIKA, .lblTitleChukeichi.Text)})
                Return Me._Vcon.SetErrMessage("E270", New String() {.sprUnsoUnkou.ActiveSheet.GetColumnLabel(0, LMF010C.SprColumnIndex.SHUKA_RELY_POINT), .sprUnsoUnkou.ActiveSheet.GetColumnLabel(0, LMF010C.SprColumnIndex.HAIKA_RELY_POINT)})
                '2016.01.06 UMANO 英語化対応END
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

        '運送会社マスタ存在チェック
        Dim rtnResult As Boolean = Me.IsUnsocoExistChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 運送会社マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnsocoExistChk() As Boolean

        With Me._Frm

            Dim brCd As String = .cmbEigyo.SelectedValue.ToString()
            Dim unsoCd As String = .txtUnsocoCd0.TextValue
            Dim unsoBrCd As String = .txtUnsocoBrCd0.TextValue

            '値がない場合、スルー
            If String.IsNullOrEmpty(unsoCd) = True _
                OrElse String.IsNullOrEmpty(unsoBrCd) = True Then
                Return True
            End If

            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectUnsocoListDataRow(drs, brCd, unsoCd, unsoBrCd) = False Then
                .txtUnsocoBrCd0.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Call Me._Vcon.SetErrorControl(.txtUnsocoCd0)
                Return False
            End If

            '名称を設定
            .lblUnsocoNm0.TextValue = Me._Gcon.EditConcatData(drs(0).Item("UNSOCO_NM").ToString(), drs(0).Item("UNSOCO_BR_NM").ToString(), Space(1))

            Return True

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
    ''' 運送新規Pop未選択チェック
    ''' </summary>
    ''' <param name="prm">パラメータクラス</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsCustPopChk(ByVal prm As LMFormData) As Boolean

        If prm.ReturnFlg = False Then

            Return Me._Vcon.SetErrMessage("E193")

        End If

        Return True

    End Function

    'START UMANO 要望番号1369 運行紐付け対応
    ''' <summary>
    ''' 運行新規処理チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsNewTripChk(ByVal arr As ArrayList, _
                                 ByVal g As LMF010G, _
                                 Optional ByVal uncoFlg As Boolean = True, _
                                 Optional ByVal unsoFlg As Boolean = True) As Boolean 'i'Friend Function IsNewTripChk(ByVal arr As ArrayList, Optional ByVal uncoFlg As Boolean = True, Optional ByVal unsoFlg As Boolean = True) As Boolean
        'Friend Function IsNewTripChk(ByVal arr As ArrayList) As Boolean

        'i'
        Dim sprUnsoUnkouDef As LMF010G.sprUnsoUnkouDefault = DirectCast(g.objSprDef, Jp.Co.Nrs.LM.GUI.LMF010G.sprUnsoUnkouDefault)

        '未選択の場合、スルー
        Dim max As Integer = arr.Count - 1
        If max < 0 Then
            Return True
        End If

        '(2012.09.06)START UMANO 要望番号1410
        '同値チェック
        'i'
        Dim rtnResult As Boolean = Me._Vcon.IsDotiChk(Me._Frm.sprUnsoUnkou, arr _
                                                      , sprUnsoUnkouDef.ARR_PLAN_DATE.ColNo _
                                                      , sprUnsoUnkouDef.UNSOCO_CD_1.ColNo _
                                                      , sprUnsoUnkouDef.UNSOCO_BR_CD_1.ColNo _
                                                      , unsoFlg)
        '(2012.09.06)END UMANO 要望番号1410

        If uncoFlg = True Then
            '運行紐付け済みチェック
            'i'rtnResult = rtnResult AndAlso Me.IsTripHimozukeChk(arr, max, False, "E228")
            rtnResult = rtnResult AndAlso Me.IsTripHimozukeChk(arr, max, False, "E228", g)
        End If

        Return rtnResult

    End Function
    'END UMANO 要望番号1369 運行紐付け対応

    ''' <summary>
    ''' 運行編集処理チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsEditTripChk(ByVal arr As ArrayList, ByVal acitonType As LMF010C.ActionType, ByVal g As LMF010G) As Boolean 'i'Friend Function IsEditTripChk(ByVal arr As ArrayList, ByVal acitonType As LMF010C.ActionType) As Boolean

        Dim max As Integer = arr.Count

        '未選択チェック
        Dim rtnResult As Boolean = Me._Vcon.IsSelectChk(max)

        '複数選択チェック
        rtnResult = rtnResult AndAlso Me._Vcon.IsSelectOneChk(max)

        '配送区分の単項目チェック
        rtnResult = rtnResult AndAlso Me.IsEditTripInputChk(acitonType)

        '配送フラグチェック
        'i'rtnResult = rtnResult AndAlso Me.IsChukeiFlgChk(arr, acitonType)
        rtnResult = rtnResult AndAlso Me.IsChukeiFlgChk(arr, acitonType, g)

        '運行紐付け未チェック
        'i'rtnResult = rtnResult AndAlso Me.IsTripHimozukeChk(arr, max - 1, True, "E229")
        rtnResult = rtnResult AndAlso Me.IsTripHimozukeChk(arr, max - 1, True, "E229", g)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 配送区分の単項目チェック
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsEditTripInputChk(ByVal actionType As LMF010C.ActionType) As Boolean

        'ボタンの運行編集以外、スルー
        If LMF010C.ActionType.BTN_UNCO_EDIT <> actionType Then
            Return True
        End If

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '配送区分
            .cmbHaiso.ItemName = LMF010C.HAISO_KBN
            .cmbHaiso.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbHaiso) = errorFlg Then
                Return errorFlg
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 運行編集時の中継配送フラグチェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsChukeiFlgChk(ByVal arr As ArrayList, ByVal actionType As LMF010C.ActionType, ByVal g As LMF010G) As Boolean 'i'Private Function IsChukeiFlgChk(ByVal arr As ArrayList, ByVal actionType As LMF010C.ActionType) As Boolean

        'i'
        Dim sprUnsoUnkouDef As LMF010G.sprUnsoUnkouDefault = DirectCast(g.objSprDef, Jp.Co.Nrs.LM.GUI.LMF010G.sprUnsoUnkouDefault)

        With Me._Frm.sprUnsoUnkou.ActiveSheet

            Dim msg As String = String.Empty

            Select Case actionType

                Case LMF010C.ActionType.BTN_UNCO_EDIT

                    'ボタンでの運行編集は中継配送有の場合、True
                    'i'If LMFControlC.FLG_ON.Equals(Me._Gcon.GetCellValue(.Cells(Convert.ToInt32(arr(0)), LMF010G.sprUnsoUnkouDef.TYUKEI_FLG.ColNo))) = True Then
                    'i'    Return True
                    'i'End If
                    If LMFControlC.FLG_ON.Equals(Me._Gcon.GetCellValue(.Cells(Convert.ToInt32(arr(0)), sprUnsoUnkouDef.TYUKEI_FLG.ColNo))) = True Then
                        Return True
                    End If
                    '2016.01.06 UMANO 英語化対応START
                    'msg = LMF010C.NASHI
                    msg = Me._Frm.optTripN.Text()
                    '2016.01.06 UMANO 英語化対応END

                Case LMF010C.ActionType.UNCO_EDIT

                    'Fキーでの運行編集は中継配送無の場合、True
                    'i'If LMFControlC.FLG_OFF.Equals(Me._Gcon.GetCellValue(.Cells(Convert.ToInt32(arr(0)), LMF010G.sprUnsoUnkouDef.TYUKEI_FLG.ColNo))) = True Then
                    'i'    Return True
                    'i'End If
                    If LMFControlC.FLG_OFF.Equals(Me._Gcon.GetCellValue(.Cells(Convert.ToInt32(arr(0)), sprUnsoUnkouDef.TYUKEI_FLG.ColNo))) = True Then
                        Return True
                    End If

                    '2016.01.06 UMANO 英語化対応START
                    'msg = LMF010C.ARI
                    msg = Me._Frm.optTripY.Text()
                    '2016.01.06 UMANO 英語化対応END

            End Select

            Return Me._Vcon.SetErrMessage("E388", New String() {msg})

        End With

    End Function

    ''' <summary>
    ''' 運行の紐付けチェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <param name="max">件数</param>
    ''' <param name="chkFlg">チェック条件　True:空の場合、エラー　False:値がある場合、エラー</param>
    ''' <param name="id">メッセージID</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTripHimozukeChk(ByVal arr As ArrayList, ByVal max As Integer, ByVal chkFlg As Boolean, ByVal id As String, ByVal g As LMF010G) As Boolean 'i'Private Function IsTripHimozukeChk(ByVal arr As ArrayList, ByVal max As Integer, ByVal chkFlg As Boolean, ByVal id As String) As Boolean

        'i'
        Dim sprUnsoUnkouDef As LMF010G.sprUnsoUnkouDefault = DirectCast(g.objSprDef, Jp.Co.Nrs.LM.GUI.LMF010G.sprUnsoUnkouDefault)

        With Me._Frm.sprUnsoUnkou.ActiveSheet

            Dim rowNo As Integer = 0
            For i As Integer = 0 To max

                rowNo = Convert.ToInt32(arr(i))

                If LMFControlC.FLG_ON.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, sprUnsoUnkouDef.TYUKEI_FLG.ColNo))) = True Then 'i'If LMFControlC.FLG_ON.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF010G.sprUnsoUnkouDef.TYUKEI_FLG.ColNo))) = True Then

                    '運行番号のチェック
                    If Me.IsThukeiTripNoChk(rowNo, chkFlg, id, g) = False Then 'i'If Me.IsThukeiTripNoChk(rowNo, chkFlg, id) = False Then
                        Return False
                    End If

                Else

                    '中継配送しない場合は運行番号でチェック
                    If String.IsNullOrEmpty(Me._Gcon.GetCellValue(.Cells(rowNo, sprUnsoUnkouDef.TRIP_NO.ColNo))) = chkFlg Then 'i'If String.IsNullOrEmpty(Me._Gcon.GetCellValue(.Cells(rowNo, LMF010G.sprUnsoUnkouDef.TRIP_NO.ColNo))) = chkFlg Then
                        Return Me._Vcon.SetErrMessage(id, New String() {String.Empty})
                    End If

                End If

            Next

            Return True

        End With

    End Function

    ''' <summary>
    ''' 運行番号設定済みチェック(中継配送有レコード)
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <param name="chkFlg">チェックフラグ</param>
    ''' <param name="id">メッセージID</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsThukeiTripNoChk(ByVal rowNo As Integer, ByVal chkFlg As Boolean, ByVal id As String, ByVal g As LMF010G) As Boolean 'i'Private Function IsThukeiTripNoChk(ByVal rowNo As Integer, ByVal chkFlg As Boolean, ByVal id As String) As Boolean

        'i'
        Dim sprUnsoUnkouDef As LMF010G.sprUnsoUnkouDefault = DirectCast(g.objSprDef, Jp.Co.Nrs.LM.GUI.LMF010G.sprUnsoUnkouDefault)

        With Me._Frm.sprUnsoUnkou.ActiveSheet

            'i'Dim shukaNo As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF010G.sprUnsoUnkouDef.TRIP_NO_SHUKA.ColNo))
            'i'Dim thukeiNo As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF010G.sprUnsoUnkouDef.TRIP_NO_CHUKEI.ColNo))
            'i'Dim haikaNo As String = Me._Gcon.GetCellValue(.Cells(rowNo, LMF010G.sprUnsoUnkouDef.TRIP_NO_HAIKA.ColNo))
            Dim shukaNo As String = Me._Gcon.GetCellValue(.Cells(rowNo, sprUnsoUnkouDef.TRIP_NO_SHUKA.ColNo))
            Dim thukeiNo As String = Me._Gcon.GetCellValue(.Cells(rowNo, sprUnsoUnkouDef.TRIP_NO_CHUKEI.ColNo))
            Dim haikaNo As String = Me._Gcon.GetCellValue(.Cells(rowNo, sprUnsoUnkouDef.TRIP_NO_HAIKA.ColNo))

            '全てから判定の場合
            If chkFlg = False Then

                '中継配送する場合は3つでチェック
                If String.IsNullOrEmpty(shukaNo) = chkFlg _
                    AndAlso String.IsNullOrEmpty(thukeiNo) = chkFlg _
                    AndAlso String.IsNullOrEmpty(haikaNo) = chkFlg _
                    Then
                    Return Me._Vcon.SetErrMessage(id)
                End If

            Else

                '配送区分に応じてチェックする項目を切り替える
                Select Case Me._Frm.cmbHaiso.SelectedValue.ToString()

                    Case LMFControlC.HAISO_SHUKA

                        '集荷の場合、運行番号(集荷)荷値がない場合、エラー
                        If String.IsNullOrEmpty(shukaNo) = True Then
                            '2016.01.06 UMANO 英語化対応START
                            'Return Me._Vcon.SetErrMessage(id, New String() {String.Concat(LMFControlC.KAKKO_1, LMF010C.SHUKA, LMFControlC.KAKKO_2)})
                            Return Me._Vcon.SetErrMessage(id, New String() {sprUnsoUnkouDef.TRIP_NO_SHUKA.ColName})
                            '2016.01.06 UMANO 英語化対応END
                        End If

                    Case LMFControlC.HAISO_THUKEI

                        '中継の場合、運行番号(中継)荷値がない場合、エラー
                        If String.IsNullOrEmpty(thukeiNo) = True Then
                            '2016.01.06 UMANO 英語化対応START
                            'Return Me._Vcon.SetErrMessage(id, New String() {String.Concat(LMFControlC.KAKKO_1, LMF010C.TYUKEI, LMFControlC.KAKKO_2)})
                            Return Me._Vcon.SetErrMessage(id, New String() {sprUnsoUnkouDef.TRIP_NO_CHUKEI.ColName})
                            '2016.01.06 UMANO 英語化対応END
                        End If

                    Case LMFControlC.HAISO_HAIKA

                        '配荷の場合、運行番号(配荷)荷値がない場合、エラー
                        If String.IsNullOrEmpty(haikaNo) = True Then
                            '2016.01.06 UMANO 英語化対応START
                            'Return Me._Vcon.SetErrMessage(id, New String() {String.Concat(LMFControlC.KAKKO_1, LMF010C.HAIKA, LMFControlC.KAKKO_2)})
                            Return Me._Vcon.SetErrMessage(id, New String() {sprUnsoUnkouDef.TRIP_NO_HAIKA.ColName})
                            '2016.01.06 UMANO 英語化対応END
                        End If

                End Select

            End If

            Return True

        End With

    End Function

    '(2013.01.17)要望番号1617 -- START --
    ''' <summary>
    ''' 運行編集処理チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsEditOutkaChk(ByVal arr As ArrayList, ByVal acitonType As LMF010C.ActionType, ByVal g As LMF010G) As Boolean 'i'Friend Function IsEditOutkaChk(ByVal arr As ArrayList, ByVal acitonType As LMF010C.ActionType) As Boolean

        Dim max As Integer = arr.Count

        '未選択チェック
        Dim rtnResult As Boolean = Me._Vcon.IsSelectChk(max)

        'i'
        Dim sprUnsoUnkouDef As LMF010G.sprUnsoUnkouDefault = DirectCast(g.objSprDef, Jp.Co.Nrs.LM.GUI.LMF010G.sprUnsoUnkouDefault)

        '複数選択チェック
        rtnResult = rtnResult AndAlso Me._Vcon.IsSelectOneChk(max)

        '運送元区分チェック（※要修正）
        'i'rtnResult = rtnResult AndAlso Me.IsMoDataKbnChk(arr, max - 1)
        rtnResult = rtnResult AndAlso Me.IsMoDataKbnChk(arr, max - 1, g)

        Return rtnResult

    End Function
    '(2013.01.17)要望番号1617 --  END  --

    'START YANAI 要望番号1241 運送検索：運送複写機能を追加する
    ''' <summary>
    ''' 運行編集処理チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsCopyUnsoChk(ByVal arr As ArrayList, ByVal acitonType As LMF010C.ActionType) As Boolean

        Dim max As Integer = arr.Count

        '未選択チェック
        Dim rtnResult As Boolean = Me._Vcon.IsSelectChk(max)

        '複数選択チェック
        rtnResult = rtnResult AndAlso Me._Vcon.IsSelectOneChk(max)

        '積込予定日の単項目チェック
        rtnResult = rtnResult AndAlso Me.IsOrigDateInputChk(acitonType)

        '荷降予定日の単項目チェック
        rtnResult = rtnResult AndAlso Me.IsDestDateInputChk(acitonType)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 積込予定日の単項目チェック
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsOrigDateInputChk(ByVal actionType As LMF010C.ActionType) As Boolean

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '積込予定日
            .imdOrigDate.ItemName = .lblTitleOrigDate.TextValue
            .imdOrigDate.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.imdOrigDate) = errorFlg Then
                Return errorFlg
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 荷降予定日の単項目チェック
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDestDateInputChk(ByVal actionType As LMF010C.ActionType) As Boolean

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '荷降予定日
            .imdDestDate.ItemName = .lblTitleDestDate.TextValue
            .imdDestDate.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.imdDestDate) = errorFlg Then
                Return errorFlg
            End If

            Return True

        End With

    End Function
    'END YANAI 要望番号1241 運送検索：運送複写機能を追加する

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        'ヘッダ項目のスペース除去
        Call Me.TrimHeaderSpaceTextValue(LMF010C.ActionType.KENSAKU)

        'スプレッドのスペース除去
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprUnsoUnkou, 0)

    End Sub

    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimHeaderSpaceTextValue(ByVal actionType As LMF010C.ActionType)

        With Me._Frm

            Select Case actionType

                Case LMF010C.ActionType.KENSAKU

                    .txtUnsocoCd1.TextValue = .txtUnsocoCd1.TextValue.Trim()
                    .txtUnsocoBrCd1.TextValue = .txtUnsocoBrCd1.TextValue.Trim()
                    .txtUnsocoCd2.TextValue = .txtUnsocoCd2.TextValue.Trim()
                    .txtUnsocoBrCd2.TextValue = .txtUnsocoBrCd2.TextValue.Trim()
                    .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
                    .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()
                    .txtCntUserCd.TextValue = .txtCntUserCd.TextValue.Trim()

                Case Else

                    .txtTripNo.TextValue = .txtTripNo.TextValue.Trim()
                    .txtUnsocoCd0.TextValue = .txtUnsocoCd0.TextValue.Trim()
                    .txtUnsocoBrCd0.TextValue = .txtUnsocoBrCd0.TextValue.Trim()

            End Select

        End With

    End Sub

    ''' <remarks></remarks>
    Friend Function IsSyasaiJutyuWatashiChk(ByVal arr As ArrayList) As Boolean

        '未選択チェック
        Dim cnt As Integer = arr.Count
        Dim rtnResult As Boolean = Me._Vcon.IsSelectChk(cnt)

        Return rtnResult

    End Function


    ''' <summary>
    ''' 元データ区分チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsMoDataKbnChk(ByVal arr As ArrayList, ByVal max As Integer, ByVal g As LMF010G) As Boolean 'i'Friend Function IsMoDataKbnChk(ByVal arr As ArrayList, ByVal max As Integer) As Boolean

        'i'
        Dim sprUnsoUnkouDef As LMF010G.sprUnsoUnkouDefault = DirectCast(g.objSprDef, Jp.Co.Nrs.LM.GUI.LMF010G.sprUnsoUnkouDefault)

        With Me._Frm.sprUnsoUnkou.ActiveSheet

            Dim rowNo As Integer = 0
            For i As Integer = 0 To max

                rowNo = Convert.ToInt32(arr(i))

                If Me._Gcon.GetCellValue(.Cells(rowNo, sprUnsoUnkouDef.DEF.ColNo)).ToString.Equals("1") = True Then 'i'If Me._Gcon.GetCellValue(.Cells(rowNo, LMF010G.sprUnsoUnkouDef.DEF.ColNo)).ToString.Equals("1") = True Then

                    '元データ区分チェック：出荷でないとエラー
                    If Me._Gcon.GetCellValue(.Cells(rowNo, sprUnsoUnkouDef.MOTO_DATA_KB.ColNo)).ToString.Equals("出荷") = False Then 'i'If Me._Gcon.GetCellValue(.Cells(rowNo, LMF010G.sprUnsoUnkouDef.MOTO_DATA_KB.ColNo)).ToString.Equals("出荷") = False Then
                        '2016.01.06 UMANO 英語化対応START
                        Return Me._Vcon.SetErrMessage("E526", New String() {"出荷"})
                        'Return Me._Vcon.SetErrMessage("E926") '20160902 メッセージID修正
                        '2016.01.06 UMANO 英語化対応END
                    End If
                End If
            Next

            Return True

        End With

    End Function

    '2022.08.22 追加START
    ''' <summary>
    ''' データ送信チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsDataSendChk(ByVal arr As ArrayList, ByVal g As LMF010G) As Boolean

        Dim cnt As Integer = arr.Count
        Dim rtnResult As Boolean = True

        '未選択チェック
        rtnResult = rtnResult AndAlso Me._Vcon.IsSelectChk(cnt)

        If rtnResult Then

            '送信済みチェック
            Dim soshinFlg As Boolean = False
            Dim sprUnsoUnkouDef As LMF010G.sprUnsoUnkouDefault = DirectCast(g.objSprDef, Jp.Co.Nrs.LM.GUI.LMF010G.sprUnsoUnkouDefault)
            With Me._Frm.sprUnsoUnkou.ActiveSheet

                Dim rowNo As Integer = 0
                For i As Integer = 0 To cnt - 1

                    rowNo = Convert.ToInt32(arr(i))

                    If Me._Gcon.GetCellValue(.Cells(rowNo, sprUnsoUnkouDef.PF_SOSHIN.ColNo)) = "01" Then
                        '送信済みあり
                        soshinFlg = True
                        Exit For
                    End If
                Next
            End With

            '送信済みあり
            If soshinFlg Then
                rtnResult = Me._Vcon.IsWarningChk(MyBase.ShowMessage("W150", New String() {"送信済みデータ", "データ送信"}))
            End If

        End If

        If rtnResult Then

            'ユーザマスタ営業所と送信データの依頼元が一致しているかチェック
            Dim defFlg As Boolean = False
            Dim sprUnsoUnkouDef As LMF010G.sprUnsoUnkouDefault = DirectCast(g.objSprDef, Jp.Co.Nrs.LM.GUI.LMF010G.sprUnsoUnkouDefault)
            With Me._Frm.sprUnsoUnkou.ActiveSheet

                Dim rowNo As Integer = 0
                For i As Integer = 0 To cnt - 1

                    rowNo = Convert.ToInt32(arr(i))

                    If Not Me._Gcon.GetCellValue(.Cells(rowNo, sprUnsoUnkouDef.NRS_BR_CD.ColNo)) = LMUserInfoManager.GetNrsBrCd() Then
                        '一致していないデータあり
                        defFlg = True
                        Exit For
                    End If
                Next
            End With

            '一致していないデータあり
            If defFlg Then
                rtnResult = Me._Vcon.IsWarningChk(MyBase.ShowMessage("E178", New String() {"データ送信"}))
            End If

        End If

        Return rtnResult

    End Function
    '2022.08.22 追加END

#End Region 'Method

End Class
