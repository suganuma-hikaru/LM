' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF200V : 運行未登録一覧
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LMF200Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMF200V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF200F


    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMFControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF200F, ByVal v As LMFControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

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
    Friend Function IsInputCheck() As Boolean


        '単項目チェック(ヘッダー部)
        If Me.IsSingleCheck() = False Then
            Return False
        End If

        '単項目チェック(Spread部)
        If Me.IsSpreadInputChk() = False Then
            Return False
        End If

        '関連チェック
        If Me.IsSaveCheck() = False Then
            Return False
        End If



        Return True

    End Function

    ''' <summary>
    ''' 画面ヘッダー部入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSingleCheck() As Boolean

        With Me._Frm
            Dim errorFlg As Boolean = False

            '営業所
            '2016.01.06 UMANO 英語化対応START
            '.cmbEigyo.ItemName = "営業所"
            .cmbEigyo.ItemName = .lblTitleEigyo.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbEigyo.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                Return False
            End If

            '営業所
            '2016.01.06 UMANO 英語化対応START
            '.cmbBetsuEigyo.ItemName = "輸送部営業所"
            .cmbBetsuEigyo.ItemName = .lblTitleBetsuEigyo.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbBetsuEigyo.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbBetsuEigyo) = False Then
                Return False
            End If

            '納入予定日From
            '2016.01.06 UMANO 英語化対応START
            '.imdArrDateFrom.ItemName = "納入予定日From"
            .imdArrDateFrom.ItemName = String.Concat(.lblTitleNonyuDate.Text(), "From")
            '2016.01.06 UMANO 英語化対応END
            If Me._Vcon.IsInputDateFullByteChk(.imdArrDateFrom, .imdArrDateFrom.ItemName) = errorFlg Then
                Return errorFlg
            End If

            '納入予定日To
            '2016.01.06 UMANO 英語化対応START
            '.imdArrDateTo.ItemName = "納入予定日To"
            .imdArrDateTo.ItemName = String.Concat(.lblTitleNonyuDate.Text(), "To")
            '2016.01.06 UMANO 英語化対応END
            If Me._Vcon.IsInputDateFullByteChk(.imdArrDateTo, .imdArrDateTo.ItemName) = errorFlg Then
                Return errorFlg
            End If

            '運送会社コード(1次)
            '2016.01.06 UMANO 英語化対応START
            '.txtUnsocoCd.ItemName = "運送会社コード(1次)"
            .txtUnsocoCd.ItemName = .lblTitleUnsoco.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtUnsocoCd.IsForbiddenWordsCheck = True
            .txtUnsocoCd.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtUnsocoCd) = False Then
                Return False
            End If

            '運送支社コード(1次)
            '2016.01.06 UMANO 英語化対応START
            '.txtUnsocoBrCd.ItemName = "運送支社コード(1次)"
            .txtUnsocoBrCd.ItemName = .lblTitleUnsoco.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtUnsocoBrCd.IsForbiddenWordsCheck = True
            .txtUnsocoBrCd.IsByteCheck = 3
            If MyBase.IsValidateCheck(.txtUnsocoBrCd) = False Then
                Return False
            End If


            Return True

        End With
    End Function
    ''' <summary>
    ''' スプレッドの項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSpreadInputChk() As Boolean

        With Me._Frm
            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            '運送番号
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.UNSO_NO.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "運送番号"
            vCell.ItemName = LMF200G.sprDetailDef.UNSO_NO.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 9
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主参照番号
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.CUST_REF_NO.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主参照番号"
            vCell.ItemName = LMF200G.sprDetailDef.CUST_REF_NO.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 30
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '発地名
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.ORIG_NM.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "発地名"
            vCell.ItemName = LMF200G.sprDetailDef.ORIG_NM.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 80
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '届先名
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.DEST_NM.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "届先名"
            vCell.ItemName = LMF200G.sprDetailDef.DEST_NM.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 80
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
#If True Then   'ADD 2022/09/02 032102   【LMS】運行・運送画面の改修要望
            '届先住所
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.DEST_ADD.ColNo)
            vCell.ItemName = LMF200G.sprDetailDef.DEST_ADD.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 80
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
#End If
            'エリア
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.ARIA.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "エリア"
            vCell.ItemName = LMF200G.sprDetailDef.ARIA.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '管理番号
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.KANRI_NO.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "管理番号"
            vCell.ItemName = LMF200G.sprDetailDef.KANRI_NO.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 9
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '運送会社名称(1次)
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.UNSOCO_NM.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "運送会社名称(1次)"
            vCell.ItemName = LMF200G.sprDetailDef.UNSOCO_NM.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 122
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '備考
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.REMARK.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "備考"
            vCell.ItemName = LMF200G.sprDetailDef.REMARK.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'まとめ番号
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.GROUP_NO.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "まとめ番号"
            vCell.ItemName = LMF200G.sprDetailDef.GROUP_NO.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 9
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '集荷中継地
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.SHUNI_TI.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "集荷中継地"
            vCell.ItemName = LMF200G.sprDetailDef.SHUNI_TI.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 50
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '配荷中継地
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.HAINI_TI.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "配荷中継地"
            vCell.ItemName = LMF200G.sprDetailDef.HAINI_TI.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 50
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '運行番号(集荷)
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.TRIP_NO_SHUKA.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "運行番号(集荷)"
            vCell.ItemName = LMF200G.sprDetailDef.TRIP_NO_SHUKA.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '運行番号(中継)
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.TRIP_NO_CHUKEI.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "運行番号(中継)"
            vCell.ItemName = LMF200G.sprDetailDef.TRIP_NO_CHUKEI.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '運行番号(配荷)
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.TRIP_NO_HAIKA.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "運行番号(配荷)"
            vCell.ItemName = LMF200G.sprDetailDef.TRIP_NO_HAIKA.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '運送会社(集荷)
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.UNSOCO_NM_SHUKA.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "運送会社(集荷)"
            vCell.ItemName = LMF200G.sprDetailDef.UNSOCO_NM_SHUKA.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 122
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '運送会社(中継)
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.UNSOCO_NM_CHUKEI.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "運送会社(中継)"
            vCell.ItemName = LMF200G.sprDetailDef.UNSOCO_NM_CHUKEI.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 122
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '運送会社(配荷)
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.UNSOCO_NM_HAIKA.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "運送会社(配荷)"
            vCell.ItemName = LMF200G.sprDetailDef.UNSOCO_NM_HAIKA.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 122
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名
            vCell.SetValidateCell(0, LMF200G.sprDetailDef.CUST_NM.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "荷主名"
            vCell.ItemName = LMF200G.sprDetailDef.CUST_NM.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 122
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If


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

            'エラーフラグ
            Dim errorFlg As Boolean = False

            '2016.01.06 UMANO 英語化対応START
            '納入予定日From
            '.imdArrDateFrom.ItemName = "納入予定日From"
            .imdArrDateFrom.ItemName = String.Concat(.lblTitleNonyuDate.Text(), "From")
            '納入予定日To
            '.imdArrDateTo.ItemName = "納入予定日To"
            .imdArrDateTo.ItemName = String.Concat(.lblTitleNonyuDate.Text(), "To")
            '2016.01.06 UMANO 英語化対応END
            '納入予定日の大小チェック
            If Me._Vcon.IsDateFromToChk(.imdArrDateFrom, .imdArrDateTo, .imdArrDateTo.ItemName, .imdArrDateFrom.ItemName) = errorFlg Then
                Return errorFlg
            End If


        End With

        Return True

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMF200C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If objNm Is Nothing = True Then
            Return False
        End If

        '判定するコントロール設定先変数
        Dim txtCtl As Win.InputMan.LMImTextBox() = Nothing
        Dim lblCtl As Control() = Nothing
        Dim msg As String() = Nothing

        With Me._Frm

            Select Case objNm

                Case .txtUnsocoCd.Name, .txtUnsocoBrCd.Name

                    Dim unsoNm0 As String = .lblTitleUnsoco.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtUnsocoCd, .txtUnsocoBrCd}
                    lblCtl = New Control() {.lblUnsocoNm}
                    msg = New String() {String.Concat(unsoNm0, LMFControlC.CD), String.Concat(unsoNm0, LMFControlC.BR_CD)}

            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)

        End With

    End Function

#End Region 'Method

End Class
