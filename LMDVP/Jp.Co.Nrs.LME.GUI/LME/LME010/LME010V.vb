' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME010F : 作業料明細書作成
'  作  成  者       :  Nishikawa
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LME010Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LME010V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LME010F

    ''' <summary>
    ''' チェックリストを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

    ''' <summary>
    ''' エラー行を格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkListErr As ArrayList

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMEControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LME010F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = New LMEControlV(handlerClass, DirectCast(frm, Form))

        Me._ChkList = New ArrayList()

        Me._ChkListErr = New ArrayList()

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "外部メソッド"

#Region "権限チェック"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LME010C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LME010C.EventShubetsu.KAKUTEI          '確定処理

                '10:閲覧者、50:外部の場合エラー
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

            Case LME010C.EventShubetsu.KAKUTEIKAIJO    '確定解除処理

                '10:閲覧者、50:外部の場合エラー
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

            Case LME010C.EventShubetsu.DEF_CUST         '初期荷主変更

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

            Case LME010C.EventShubetsu.MASTER          'マスタ参照

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

            Case LME010C.EventShubetsu.KENSAKU          '検索

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

            Case LME010C.EventShubetsu.HOZON          '保存

                '10:閲覧者、50:外部の場合エラー
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

            Case LME010C.EventShubetsu.CLOSE           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LME010C.EventShubetsu.HENKO           '一括変更

                '10:閲覧者、50:外部の場合エラー
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


            Case LME010C.EventShubetsu.ROW_COPY        '行複写

                '10:閲覧者、50:外部の場合エラー
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

            Case LME010C.EventShubetsu.ROW_DEL         '行削除

                '10:閲覧者、50:外部の場合エラー
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

            Case LME010C.EventShubetsu.PRINT         '印刷

                '10:閲覧者、50:外部の場合エラー
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

                'START YANAI 20120319　作業画面改造
            Case LME010C.EventShubetsu.SINKI         '新規

                '10:閲覧者、50:外部の場合エラー
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

            Case LME010C.EventShubetsu.RENZOKU      '連続入力

                '10:閲覧者、50:外部の場合エラー
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

            Case LME010C.EventShubetsu.KANRYO      '完了

                '10:閲覧者、50:外部の場合エラー
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

                'END YANAI 20120319　作業画面改造

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

#Region "確定時チェック"

#Region "単項目チェック"
    ''' <summary>
    ''' 確定時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKakuteiSingleCheck(ByVal jobFlg As Double) As Boolean

        With Me._Frm
            '【単項目チェック】

            Dim strMsg As String = String.Empty

            If jobFlg = LME010C.EventShubetsu.KAKUTEI Then
                strMsg = "確定"
            Else
                strMsg = "確定解除"
            End If

            'チェックリスト初期化
            Me._ChkList = New ArrayList()
            'チェック行リスト取得
            Me._ChkList = Me.getCheckList()

            'スプレッド項目の入力チェック
            Dim max As Integer = Me._ChkList.Count - 1
            Dim chkVal As String = String.Empty
            Dim errVal As Integer = 0

            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            'If Convert.ToString(.cmbEigyo.SelectedValue) <> LMUserInfoManager.GetNrsBrCd().ToString() Then
            '    '営業所＋自営業所
            '    MyBase.ShowMessage("E178", New String() {strMsg})
            '    Return False
            'End If

            '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
            ''自営業所チェック
            'For i As Integer = 0 To max
            '    chkVal = .sprSagyo.ActiveSheet.Cells(Convert.ToInt32(_ChkList(i)), LME010G.sprDetailDef.NRS_BR_CD.ColNo).Value().ToString()

            '    If chkVal.Equals(LMUserInfoManager.GetNrsBrCd().ToString()) = False Then
            '        MyBase.ShowMessage("E178", New String() {strMsg})
            '        Return False
            '    End If

            'Next

            If jobFlg = LME010C.EventShubetsu.KAKUTEI Then
                For i As Integer = 0 To max
                    chkVal = .sprSagyo.ActiveSheet.Cells(Convert.ToInt32(_ChkList(i)), LME010G.sprDetailDef.SKYU_CHK.ColNo).Value().ToString()

                    If chkVal = "01" Then
                        'START YANAI No.7
                        'MyBase.ShowMessage("E284", New String() {"確定済", "確定"})
                        MyBase.ShowMessage("E127")
                        'END YANAI No.7
                        Return False
                    End If
                Next
            Else
                For i As Integer = 0 To max
                    chkVal = .sprSagyo.ActiveSheet.Cells(Convert.ToInt32(_ChkList(i)), LME010G.sprDetailDef.SKYU_CHK.ColNo).Value().ToString()

                    If chkVal = "00" Then
                        MyBase.ShowMessage("E284", New String() {"未確定", "確定解除"})
                        Return False
                    End If
                Next
            End If

            'START YANAI 20120319　作業画面改造
            '完了チェック
            For i As Integer = 0 To max
                chkVal = .sprSagyo.ActiveSheet.Cells(Convert.ToInt32(_ChkList(i)), LME010G.sprDetailDef.SAGYO_COMP.ColNo).Value().ToString()

                If chkVal = "00" Then
                    MyBase.ShowMessage("E454", New String() {"未完了", strMsg, String.Concat(Convert.ToInt32(_ChkList(i)), "行目")})
                    Return False
                End If
            Next
            'END YANAI 20120319　作業画面改造

        End With

        Return True

    End Function
#End Region

#Region "関連チェック"
    ''' <summary>
    ''' 確定時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Public Function IsKakuteiKanrenCheck() As Boolean

        With Me._Frm


        End With

        Return True

    End Function

#End Region

#End Region

#Region "検索時チェック"

#Region "単項目チェック"
    ''' <summary>
    ''' 検索時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKensakuSingleCheck() As Boolean

        'Trimチェック

        '検索

        'ヘッダ項目のスペース除去
        Call Me.TrimHeaderSpaceTextValue()

        'スプレッド項目のスペース除去
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprSagyo, 0, Me._Frm.sprSagyo.ActiveSheet.Columns.Count - 1)


        With Me._Frm

            '【単項目チェック】

            '******************** ヘッダ項目の入力チェック ********************

            '営業所
            .cmbEigyo.ItemName() = "営業所"
            .cmbEigyo.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                Return False
            End If

            '倉庫
            .cmbWare.ItemName() = "倉庫"
            .cmbWare.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbWare) = False Then
                Return False
            End If

            '荷主コード(大)
            .txtCustCD_L.ItemName() = "荷主コード(大)"
            .txtCustCD_L.IsForbiddenWordsCheck() = True
            .txtCustCD_L.IsByteCheck() = 5
            If MyBase.IsValidateCheck(.txtCustCD_L) = False Then
                Return False
            End If

            '荷主コード(中)
            .txtCustCD_M.ItemName() = "荷主コード(中)"
            .txtCustCD_M.IsForbiddenWordsCheck() = True
            .txtCustCD_M.IsByteCheck() = 2
            If MyBase.IsValidateCheck(.txtCustCD_M) = False Then
                Return False
            End If

            '請求先コード
            .txtSeikyuCD.ItemName() = "請求先コード"
            .txtSeikyuCD.IsForbiddenWordsCheck() = True
            .txtSeikyuCD.IsByteCheck() = 7
            If MyBase.IsValidateCheck(.txtSeikyuCD) = False Then
                Return False
            End If

            '作業コード
            .txtSagyoCD.ItemName() = "作業コード"
            .txtSagyoCD.IsForbiddenWordsCheck() = True
            .txtSagyoCD.IsByteCheck() = 5
            If MyBase.IsValidateCheck(.txtSagyoCD) = False Then
                Return False
            End If

            '作業指示書番号
            .txtSagyoSijiNO.ItemName() = "作業指示書番号"
            .txtSagyoSijiNO.IsForbiddenWordsCheck() = True
            .txtSagyoSijiNO.IsByteCheck() = 10
            If MyBase.IsValidateCheck(.txtSagyoSijiNO) = False Then
                Return False
            End If

            '作業日From
            If .imdSagyoDate_S.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"作業日From", "8"})
                Return False
            End If

            '作業日To
            If .imdSagyoDate_E.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"作業日To", "8"})
                Return False
            End If


            '******************** スプレッド項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprSagyo)

            '商品名
            vCell.SetValidateCell(0, LME010G.sprDetailDef.GOODS_NM.ColNo)
            vCell.ItemName() = "商品名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'ロット№
            vCell.SetValidateCell(0, LME010G.sprDetailDef.LOT_NO.ColNo)
            vCell.ItemName() = "ロット№"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '作業名
            vCell.SetValidateCell(0, LME010G.sprDetailDef.SAGYO_NM.ColNo)
            vCell.ItemName() = "作業名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '請求先名
            vCell.SetValidateCell(0, LME010G.sprDetailDef.SQTO_NM.ColNo)
            vCell.ItemName() = "請求先名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '届先名
            vCell.SetValidateCell(0, LME010G.sprDetailDef.DEST_NM.ColNo)
            vCell.ItemName() = "届先名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 80
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '備考(請求用)
            vCell.SetValidateCell(0, LME010G.sprDetailDef.SKYU_REMARK.ColNo)
            vCell.ItemName() = "備考(請求用)"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名(大)
            vCell.SetValidateCell(0, LME010G.sprDetailDef.CUST_NM_L.ColNo)
            vCell.ItemName() = "荷主名(大)"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '管理番号
            vCell.SetValidateCell(0, LME010G.sprDetailDef.IOKA_CTL_NO.ColNo)
            vCell.ItemName() = "管理番号"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 12
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '作業レコード番号
            vCell.SetValidateCell(0, LME010G.sprDetailDef.SAGYO_REC_NO.ColNo)
            vCell.ItemName() = "作業レコード番号"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '確認作業者名
            vCell.SetValidateCell(0, LME010G.sprDetailDef.SAGYO_COMP_NM.ColNo)
            vCell.ItemName() = "確認作業者名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '更新者
            vCell.SetValidateCell(0, LME010G.sprDetailDef.UPT_NM.ColNo)
            vCell.ItemName() = "最終更新者"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function
#End Region

#Region "関連チェック"
    ''' <summary>
    ''' 検索時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Public Function IsKensakuKanrenCheck() As Boolean

        With Me._Frm

            '作業日From + 作業日To

            '  作業日Fromより作業日Toが過去日の場合エラー()
            '  いずれも設定済 である場合のみチェック

            If .imdSagyoDate_S.TextValue.Equals(String.Empty) = False _
                And .imdSagyoDate_E.TextValue.Equals(String.Empty) = False Then

                If .imdSagyoDate_E.TextValue < .imdSagyoDate_S.TextValue Then

                    '入荷日Fromより入荷日Toが過去日の場合エラー
                    Me.ShowMessage("E039", New String() {"作業日To", "作業日From"})
                    .imdSagyoDate_S.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdSagyoDate_E.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdSagyoDate_S.Focus()
                    Return False

                End If

            End If

        End With

        Return True

    End Function
#End Region

#End Region

#Region "保存時チェック"

    ''' <summary>
    ''' 保存入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Public Function IsHozonKanrenCheck() As Boolean

        With Me._Frm

        End With

        Return True

    End Function


#End Region

#Region "一括変更時チェック"

#Region "単項目チェック"
    ''' <summary>
    ''' 一括変更時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsHenkoSingleCheck() As Boolean

        With Me._Frm

            '【単項目チェック】

            'チェックリスト初期化
            Me._ChkList = New ArrayList()

            'チェック行リスト取得
            Me._ChkList = Me.getCheckList()

            Dim max As Integer = Me._ChkList.Count - 1
            Dim chkVal As String = String.Empty
            Dim chkSagyoVal As Decimal = 0
            Dim sagyoGk As Decimal = 0

            '確定済のデータを一括変更したらエラー
            For i As Integer = 0 To max
                chkVal = .sprSagyo.ActiveSheet.Cells(Convert.ToInt32(_ChkList(i)), LME010G.sprDetailDef.SKYU_CHK.ColNo).Value().ToString()

                If chkVal = "01" Then
                    'START YANAI No.7
                    'MyBase.ShowMessage("E284", New String() {"確定済", "一括変更"})
                    MyBase.ShowMessage("E127")
                    'END YANAI No.7
                    Return False
                End If
            Next

            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            '最大件数チェック
            Dim limitHenkoCnt As Integer = Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'I004' AND KBN_CD = '02'")(0).Item("VALUE1")))
            If limitHenkoCnt < Me._ChkList.Count Then
                MyBase.ShowMessage("E168", New String() {_ChkList.Count.ToString(), limitHenkoCnt.ToString()})
                Return False
            End If


            'If Convert.ToString(.cmbEigyo.SelectedValue) <> LMUserInfoManager.GetNrsBrCd().ToString() Then
            '    '営業所＋自営業所
            '    MyBase.ShowMessage("E178", New String() {"一括変更"})
            '    Return False
            'End If

            '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
            ''自営業所チェック
            'For i As Integer = 0 To max
            '    chkVal = .sprSagyo.ActiveSheet.Cells(Convert.ToInt32(_ChkList(i)), LME010G.sprDetailDef.NRS_BR_CD.ColNo).Value().ToString()

            '    If chkVal.Equals(LMUserInfoManager.GetNrsBrCd().ToString()) = False Then
            '        MyBase.ShowMessage("E178", New String() {"一括変更"})
            '        Return False
            '    End If

            'Next

            '修正項目選択チェック
            If .cmbEditList.SelectedValue Is String.Empty Then
                MyBase.ShowMessage("E199", New String() {"修正項目"})
                Me._Vcon.SetErrorControl(.cmbEditList)
                Return False
            End If

            Dim selectCmbValue As String = .cmbEditList.SelectedValue.ToString

            Select Case selectCmbValue
                Case LME010C.EDIT_SELECT_GOODS '商品名
                    .txtEditTxt.TextValue = Trim(.txtEditTxt.TextValue)
                    .txtEditTxt.ItemName() = "商品名"
                    .txtEditTxt.IsHissuCheck() = True
                    .txtEditTxt.IsForbiddenWordsCheck() = True
                    .txtEditTxt.IsByteCheck() = 60
                    If MyBase.IsValidateCheck(.txtEditTxt) = False Then
                        Return False
                    End If

                Case LME010C.EDIT_SELECT_LOT 'ロット№
                    .txtEditTxt.TextValue = Trim(.txtEditTxt.TextValue)
                    .txtEditTxt.ItemName() = "ロット№"
                    .txtEditTxt.IsHissuCheck() = True
                    .txtEditTxt.IsForbiddenWordsCheck() = True
                    .txtEditTxt.IsByteCheck() = 40
                    If MyBase.IsValidateCheck(.txtEditTxt) = False Then
                        Return False
                    End If

                Case LME010C.EDIT_SELECT_SAGYONM '作業名称
                    .txtEditTxt.TextValue = Trim(.txtEditTxt.TextValue)
                    .txtEditTxt.ItemName() = "作業名称"
                    .txtEditTxt.IsHissuCheck() = True
                    .txtEditTxt.IsForbiddenWordsCheck() = True
                    .txtEditTxt.IsByteCheck() = 60
                    If MyBase.IsValidateCheck(.txtEditTxt) = False Then
                        Return False
                    End If

                Case LME010C.EDIT_SELECT_SAGYOSU '作業数
                    '.txtEditNum.ItemName() = "作業数"
                    '.txtEditNum.IsHissuCheck() = True
                    'If MyBase.IsValidateCheck(.txtEditNum) = False Then
                    '    Return False
                    'End If

                    .txtEditNum.TextValue = Trim(.txtEditNum.TextValue)

                    If Me.NumNullCheck(.txtEditNum.TextValue) = False Then
                        Me._Vcon.SetErrorControl(.txtEditNum)
                        Return False
                    End If

                    If Me.NumZeroCheck(.txtEditNum.TextValue) = False Then
                        Me._Vcon.SetErrorControl(.txtEditNum)
                        Return False
                    End If

                    If Me.IsHaniCheck(Convert.ToDecimal(.txtEditNum.Value), Convert.ToDecimal(LME010C.SAGYO_NB_MIN), Convert.ToDecimal(LME010C.SAGYO_NB_MAX)) = False Then
                        MyBase.ShowMessage("E014", New String() {"作業数", LME010C.SAGYO_NB_MIN, LME010C.SAGYO_NB_MAX})
                        Me._Vcon.SetErrorControl(.txtEditNum)
                        Return False
                    End If

                Case LME010C.EDIT_SELECT_SEIQUP '請求単価
                    '.txtEditNum.ItemName() = "請求単価"
                    '.txtEditNum.IsHissuCheck() = True
                    'If MyBase.IsValidateCheck(.txtEditNum) = False Then
                    '    Return False
                    'End If
                    .txtEditNum.TextValue = Trim(.txtEditNum.TextValue)

                    If Me.NumNullCheck(.txtEditNum.TextValue) = False Then
                        Me._Vcon.SetErrorControl(.txtEditNum)
                        Return False
                    End If

                    If Me.NumZeroCheck(.txtEditNum.TextValue) = False Then
                        Me._Vcon.SetErrorControl(.txtEditNum)
                        Return False
                    End If

                    If Me.IsHaniCheck(Convert.ToDecimal(.txtEditNum.Value), Convert.ToDecimal(LME010C.SAGYO_UP_MIN), Convert.ToDecimal(LME010C.SAGYO_UP_MAX)) = False Then
                        MyBase.ShowMessage("E014", New String() {"請求単価", LME010C.SAGYO_UP_MIN, LME010C.SAGYO_UP_MAX})
                        Me._Vcon.SetErrorControl(.txtEditNum)
                        Return False
                    End If

                    'For i As Integer = 0 To max
                    '    chkVal = .sprSagyo.ActiveSheet.Cells(Convert.ToInt32(_ChkList(i)), LME010G.sprDetailDef.SAGYOSU.ColNo).Value().ToString()
                    '    chkSagyoVal = Convert.ToDecimal(chkVal)
                    '    sagyoGk = Convert.ToDecimal(.txtEditNum.Value) * chkSagyoVal

                    '    If Me.IsHaniCheck(sagyoGk, Convert.ToDecimal(LME010C.SAGYO_GK_MIN), Convert.ToDecimal(LME010C.SAGYO_GK_MAX)) = False Then
                    '        MyBase.ShowMessage("E390", New String() {"作業数", "請求単価", String.Concat(LME010C.SAGYO_GK_MIN, "～", LME010C.SAGYO_GK_MAX)})
                    '        Me._Vcon.SetErrorControl(.txtEditNum)
                    '        Return False
                    '    End If

                    'Next

                Case LME010C.EDIT_SELECT_SEIQUT '請求単位
                    .cmbEditKbSkyu.ItemName() = "請求単位"
                    .cmbEditKbSkyu.IsHissuCheck() = True
                    If MyBase.IsValidateCheck(.cmbEditKbSkyu) = False Then
                        Return False
                    End If

                Case LME010C.EDIT_SELECT_SEIQGK '請求金額
                    '.txtEditNum.ItemName() = "請求金額"
                    'If MyBase.IsValidateCheck(.txtEditNum) = False Then
                    '    Return False
                    'End If
                    .txtEditNum.TextValue = Trim(.txtEditNum.TextValue)

                    If Me.NumNullCheck(.txtEditNum.TextValue) = False Then
                        Me._Vcon.SetErrorControl(.txtEditNum)
                        Return False
                    End If

                    If Me.NumZeroCheck(.txtEditNum.TextValue) = False Then
                        Me._Vcon.SetErrorControl(.txtEditNum)
                        Return False
                    End If

                    If Me.IsHaniCheck(Convert.ToDecimal(.txtEditNum.Value), Convert.ToDecimal(LME010C.SAGYO_GK_MIN), Convert.ToDecimal(LME010C.SAGYO_GK_MAX)) = False Then
                        MyBase.ShowMessage("E014", New String() {"請求金額", LME010C.SAGYO_GK_MIN, LME010C.SAGYO_GK_MAX})
                        Me._Vcon.SetErrorControl(.txtEditNum)
                        Return False
                    End If

                Case LME010C.EDIT_SELECT_SEIQTO '請求先コード

                    .txtEditTxt.TextValue = Trim(.txtEditTxt.TextValue)
                    .txtEditTxt.ItemName() = "請求先コード"
                    .txtEditTxt.IsHissuCheck() = True
                    .txtEditTxt.IsForbiddenWordsCheck() = True
                    .txtEditTxt.IsByteCheck() = 7
                    If MyBase.IsValidateCheck(.txtEditTxt) = False Then
                        Return False
                    End If

                    'マスタ存在チェック
                    If Me.IsSeikyuToExistChk() = False Then
                        MyBase.ShowMessage("E079", New String() {"請求先マスタ", .txtEditTxt.TextValue()})
                        Return False

                    End If

                Case LME010C.EDIT_SELECT_SAGYODATE '作業完了日
                    .cmbEditDate.ItemName() = "作業完了日"
                    .cmbEditDate.IsHissuCheck() = True
                    .cmbEditDate.IsDateFullByteCheck(8)
                    If MyBase.IsValidateCheck(.cmbEditDate) = False Then
                        Return False
                    End If

                Case LME010C.EDIT_SELECT_DEST '届先名
                    .txtEditTxt.TextValue = Trim(.txtEditTxt.TextValue)
                    .txtEditTxt.ItemName() = "届先名"
                    .txtEditTxt.IsHissuCheck() = True
                    .txtEditTxt.IsForbiddenWordsCheck() = True
                    .txtEditTxt.IsByteCheck() = 80
                    If MyBase.IsValidateCheck(.txtEditTxt) = False Then
                        Return False
                    End If

                Case LME010C.EDIT_SELECT_REMARK '備考
                    .txtEditTxt.TextValue = Trim(.txtEditTxt.TextValue)
                    .txtEditTxt.ItemName() = "備考"
                    .txtEditTxt.IsHissuCheck() = True
                    .txtEditTxt.IsForbiddenWordsCheck() = True
                    .txtEditTxt.IsByteCheck() = 100
                    If MyBase.IsValidateCheck(.txtEditTxt) = False Then
                        Return False
                    End If

                Case LME010C.EDIT_SELECT_TAX '課税区分
                    .cmbEditKbTax.ItemName() = "課税区分"
                    .cmbEditKbTax.IsHissuCheck() = True
                    If MyBase.IsValidateCheck(.cmbEditKbTax) = False Then
                        Return False
                    End If

                Case Else
                    Return False
            End Select


        End With

        Return True

    End Function
#End Region

#Region "関連チェック"

    ''' <summary>
    ''' 一括変更時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>errHt:Hashtable</returns>
    ''' <remarks></remarks>
    Friend Function IsHenkoKanrenCheck(ByRef errDs As DataSet) As Hashtable

        '【関連チェック】

        Dim errHt As Hashtable = New Hashtable
        errDs = New LME010DS()

        With Me._Frm

            'チェックリスト初期化
            Me._ChkList = New ArrayList()

            'チェック行リスト取得
            Me._ChkList = Me.getCheckList()

            Dim max As Integer = Me._ChkList.Count - 1
            Dim chkVal As String = String.Empty
            Dim chkSagyoVal As Decimal = 0
            Dim sagyoGk As Decimal = 0
            Dim sagyoRecNo As String = String.Empty

            Dim selectCmbValue As String = .cmbEditList.SelectedValue.ToString

            Select Case selectCmbValue

                Case LME010C.EDIT_SELECT_SAGYOSU '作業数

                    For i As Integer = 0 To max

                        chkVal = .sprSagyo.ActiveSheet.Cells(Convert.ToInt32(_ChkList(i)), LME010G.sprDetailDef.SAGYO_TANKA.ColNo).Value().ToString()
                        chkSagyoVal = Convert.ToDecimal(chkVal)
                        sagyoGk = ToRound(Convert.ToDecimal(.txtEditNum.Value) * chkSagyoVal, 0)

                        Dim dr As DataRow

                        If Me.IsHaniCheck(sagyoGk, Convert.ToDecimal(LME010C.SAGYO_GK_MIN), Convert.ToDecimal(LME010C.SAGYO_GK_MAX)) = False Then

                            'エラーがある場合、DataTableに設定
                            dr = errDs.Tables(LME010C.TABLE_NM_GUIERROR).NewRow()
                            dr("GUIDANCE_ID") = LME010C.GUIDANCE_KBN
                            dr("MESSAGE_ID") = "E390"
                            dr("PARA1") = "作業数"
                            dr("PARA2") = "請求単価"
                            dr("PARA3") = String.Concat(LME010C.SAGYO_GK_MIN, "～", LME010C.SAGYO_GK_MAX)
                            dr("KEY_NM") = LME010C.EXCEL_COLTITLE
                            dr("KEY_VALUE") = .sprSagyo.ActiveSheet.Cells(Convert.ToInt32(_ChkList(i)), LME010G.sprDetailDef.SAGYO_REC_NO.ColNo).Value().ToString()
                            dr("ROW_NO") = Convert.ToInt32(Me._ChkList(i))
                            errDs.Tables(LME010C.TABLE_NM_GUIERROR).Rows.Add(dr)

                            errHt.Add(i, String.Empty)

                            'MyBase.ShowMessage("E390", New String() {"作業数", "請求単価", String.Concat(LME010C.SAGYO_GK_MIN, "～", LME010C.SAGYO_GK_MAX)})

                        End If

                    Next

                Case LME010C.EDIT_SELECT_SEIQUP '請求単価

                    For i As Integer = 0 To max

                        chkVal = .sprSagyo.ActiveSheet.Cells(Convert.ToInt32(_ChkList(i)), LME010G.sprDetailDef.SAGYOSU.ColNo).Value().ToString()
                        chkSagyoVal = Convert.ToDecimal(chkVal)
                        sagyoGk = ToRound(Convert.ToDecimal(.txtEditNum.Value) * chkSagyoVal, 0)

                        Dim dr As DataRow

                        If Me.IsHaniCheck(sagyoGk, Convert.ToDecimal(LME010C.SAGYO_GK_MIN), Convert.ToDecimal(LME010C.SAGYO_GK_MAX)) = False Then

                            'エラーがある場合、DataTableに設定
                            dr = errDs.Tables(LME010C.TABLE_NM_GUIERROR).NewRow()
                            dr("GUIDANCE_ID") = LME010C.GUIDANCE_KBN
                            dr("MESSAGE_ID") = "E390"
                            dr("PARA1") = "請求単価"
                            dr("PARA2") = "作業数"
                            dr("PARA3") = String.Concat(LME010C.SAGYO_GK_MIN, "～", LME010C.SAGYO_GK_MAX)
                            dr("KEY_NM") = LME010C.EXCEL_COLTITLE
                            dr("KEY_VALUE") = .sprSagyo.ActiveSheet.Cells(Convert.ToInt32(_ChkList(i)), LME010G.sprDetailDef.SAGYO_REC_NO.ColNo).Value().ToString()
                            dr("ROW_NO") = Convert.ToInt32(Me._ChkList(i))
                            errDs.Tables(LME010C.TABLE_NM_GUIERROR).Rows.Add(dr)

                            errHt.Add(i, String.Empty)

                            'MyBase.ShowMessage("E390", New String() {"請求単価", "作業数", String.Concat(,LME010C.SAGYO_GK_MIN, "～", LME010C.SAGYO_GK_MAX)})

                        End If

                    Next

                Case Else

            End Select

        End With

        Return errHt

    End Function

#End Region

#End Region

#Region "行削除時チェック"

#Region "単項目チェック"
    ''' <summary>
    ''' 削除チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsRowDelSingleCheck() As Boolean

        With Me._Frm

            '【単項目チェック】

            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            'If Convert.ToString(.cmbEigyo.SelectedValue) <> LMUserInfoManager.GetNrsBrCd().ToString() Then
            '    '営業所＋自営業所
            '    MyBase.ShowMessage("E178", New String() {"行削除"})
            '    Return False
            'End If

            'スプレッド項目の入力チェック
            Dim max As Integer = Me._ChkList.Count - 1
            Dim chkVal As String = String.Empty
            Dim errVal As Integer = 0

            '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
            ''自営業所チェック
            'For i As Integer = 0 To max
            '    chkVal = .sprSagyo.ActiveSheet.Cells(Convert.ToInt32(_ChkList(i)), LME010G.sprDetailDef.NRS_BR_CD.ColNo).Value().ToString()

            '    If chkVal.Equals(LMUserInfoManager.GetNrsBrCd().ToString()) = False Then
            '        MyBase.ShowMessage("E178", New String() {"行削除"})
            '        Return False
            '    End If

            'Next

            For i As Integer = 0 To max
                chkVal = .sprSagyo.ActiveSheet.Cells(Convert.ToInt32(_ChkList(i)), LME010G.sprDetailDef.SKYU_CHK.ColNo).Value().ToString()

                If chkVal = "01" Then
                    'START YANAI No.7
                    'MyBase.ShowMessage("E284", New String() {"確定済", "削除"})
                    MyBase.ShowMessage("E127")
                    'END YANAI No.7
                    Return False
                End If

            Next

        End With

        Return True

    End Function
#End Region

#Region "関連チェック"
    ''' <summary>
    ''' 行削除時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Public Function IsRowDelKanrenCheck() As Boolean

        With Me._Frm

        End With

        Return True

    End Function

#End Region

#End Region

#Region "行複写時チェック"

#Region "単項目チェック"
    ''' <summary>
    ''' 複写チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsRowCopySingleCheck() As Boolean

        With Me._Frm

            '【単項目チェック】

            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            'If Convert.ToString(.cmbEigyo.SelectedValue) <> LMUserInfoManager.GetNrsBrCd().ToString() Then
            '    '営業所＋自営業所
            '    MyBase.ShowMessage("E178", New String() {"行複写"})
            '    Return False
            'End If


            '単一選択チェック
            If Me.IsSelectOneDataChk() = False Then
                Return False
            End If

            'スプレッド項目の入力チェック
            Dim max As Integer = Me._ChkList.Count - 1
            Dim chkVal As String = String.Empty

            '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
            ''自営業所チェック
            'For i As Integer = 0 To max
            '    chkVal = .sprSagyo.ActiveSheet.Cells(Convert.ToInt32(_ChkList(i)), LME010G.sprDetailDef.NRS_BR_CD.ColNo).Value().ToString()

            '    If chkVal.Equals(LMUserInfoManager.GetNrsBrCd().ToString()) = False Then
            '        MyBase.ShowMessage("E178", New String() {"行複写"})
            '        Return False
            '    End If

            'Next

        End With

        Return True

    End Function
#End Region

#Region "関連チェック"
    ''' <summary>
    ''' 複写時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Public Function IsRowCopyKanrenCheck() As Boolean

        With Me._Frm

        End With

        Return True

    End Function

#End Region

#End Region

#Region "マスタ参照時チェック"

    ''' <summary>
    ''' マスタ参照チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsPopSingleCheck(ByVal objNM As String) As Boolean

        With Me._Frm

            '営業所
            .cmbEigyo.ItemName() = "営業所"
            .cmbEigyo.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                Return False
            End If

            Select Case objNM

                Case "txtCustCD_L", "txtCustCD_M"                    '荷主マスタ参照

                    '荷主コード(大)
                    .txtCustCD_L.ItemName() = "荷主コード(大)"
                    .txtCustCD_L.IsForbiddenWordsCheck() = True
                    .txtCustCD_L.IsByteCheck() = 5
                    If MyBase.IsValidateCheck(.txtCustCD_L) = False Then
                        Return False
                    End If

                    '荷主コード(中)
                    .txtCustCD_M.ItemName() = "荷主コード(中)"
                    .txtCustCD_M.IsForbiddenWordsCheck() = True
                    .txtCustCD_M.IsByteCheck() = 2
                    If MyBase.IsValidateCheck(.txtCustCD_M) = False Then
                        Return False
                    End If

                Case "txtSeikyuCD"                                   '請求先マスタ参照

                    '請求先コード
                    .txtSeikyuCD.ItemName() = "請求先コード"
                    .txtSeikyuCD.IsForbiddenWordsCheck() = True
                    .txtSeikyuCD.IsByteCheck() = 7
                    If MyBase.IsValidateCheck(.txtSeikyuCD) = False Then
                        Return False
                    End If

                Case "txtSagyoCD"                                   '作業項目マスタ参照

                    '作業コード
                    .txtSagyoCD.ItemName() = "作業コード"
                    .txtSagyoCD.IsForbiddenWordsCheck() = True
                    .txtSagyoCD.IsByteCheck() = 5
                    If MyBase.IsValidateCheck(.txtSagyoCD) = False Then
                        Return False
                    End If

            End Select

            Return True

        End With

        Return True

    End Function

#End Region

#Region "印刷時チェック"

    ''' <summary>
    ''' 印刷チェック（単項目・関連チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsPrintInputCheck() As Boolean

        With Me._Frm

            'ヘッダ項目のスペース除去
            Call Me.TrimHeaderSpaceTextValue()

            '【単項目チェック】

            '印刷種別
            .cmbPrint.ItemName() = "印刷種別"
            .cmbPrint.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbPrint) = False Then
                Return False
            End If

            '荷主コード(大)
            .txtCustCD_L.ItemName() = "荷主コード(大)"
            .txtCustCD_L.IsHissuCheck() = True
            .txtCustCD_L.IsForbiddenWordsCheck = True
            .txtCustCD_L.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCD_L) = False Then
                Return False
            End If

            '荷主コード(中)
            .txtCustCD_M.ItemName() = "荷主コード(中)"
            .txtCustCD_M.IsHissuCheck() = True
            .txtCustCD_M.IsForbiddenWordsCheck = True
            .txtCustCD_M.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCD_M) = False Then
                Return False
            End If

            '営業所
            .cmbEigyo.ItemName() = "営業所"
            .cmbEigyo.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                Return False
            End If

            '倉庫
            .cmbWare.ItemName() = "倉庫"
            .cmbWare.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbWare) = False Then
                Return False
            End If

            '作業日From
            .imdSagyoDate_S.ItemName() = "作業日From"
            .imdSagyoDate_S.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.imdSagyoDate_S) = False Then
                Return False
            End If
            If .imdSagyoDate_S.IsDateFullByteCheck(8) = False OrElse _
                String.IsNullOrEmpty(.imdSagyoDate_S.TextValue.Trim()) = True Then
                MyBase.ShowMessage("E038", New String() {"作業日From", "8"})
                Return False
            End If

            '作業日To
            .imdSagyoDate_E.ItemName() = "作業日To"
            .imdSagyoDate_E.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.imdSagyoDate_E) = False Then
                Return False
            End If
            If .imdSagyoDate_E.IsDateFullByteCheck(8) = False OrElse _
                String.IsNullOrEmpty(.imdSagyoDate_E.TextValue.Trim()) = True Then
                MyBase.ShowMessage("E038", New String() {"作業日To", "8"})
                Return False
            End If


        End With

        Return True

    End Function

    ''' <summary>
    ''' 印刷チェック（関連チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsPrintKanrenCheck(ByVal chkDs As DataSet) As Boolean

        Dim rtn As MsgBoxResult

        With Me._Frm

            '【関連チェック】

            '作業日From + 作業日To
            If Me.IsKensakuKanrenCheck() = False Then
                Return False
            End If

            '荷主存在チェック
            If chkDs.Tables(LME010C.TABLE_NM_PRINTCHECK).Rows.Count = 0 _
            OrElse String.IsNullOrEmpty(chkDs.Tables(LME010C.TABLE_NM_PRINTCHECK).Rows(0).Item("CUST_NM_L").ToString()) = True Then
                MyBase.ShowMessage("E079", New String() {"荷主マスタ", String.Concat(.txtCustCD_L.TextValue(), "-", .txtCustCD_M.TextValue())})
                Me.SetErrorControl(.txtCustCD_M)
                Me.SetErrorControl(.txtCustCD_L)
                Return False
            End If

            Dim closeDate As String = chkDs.Tables(LME010C.TABLE_NM_PRINTCHECK).Rows(0).Item("CLOSE_DATE").ToString()

            '請求存在チェック
            If String.IsNullOrEmpty(closeDate) = True Then
                MyBase.ShowMessage("E079", New String() {"請求先マスタ", .txtSeikyuCD.TextValue})
                Me.SetErrorControl(.txtSeikyuCD)
                Return False
            End If

            Dim tDate As Date = .imdSagyoDate_E.Value  '作業日To
            Dim wID As String = String.Empty

            If "00".Equals(closeDate) = True Then

                If String.IsNullOrEmpty(.txtSeikyuCD.TextValue) = True Then
                    wID = "W171"
                Else
                    wID = "W173"
                End If

                '月末チェック
                If tDate.Day <> Date.DaysInMonth(tDate.Year, tDate.Month) Then
                    rtn = MyBase.ShowMessage(wID)

                    If rtn = MsgBoxResult.Ok Then
                        Return True

                    ElseIf rtn = MsgBoxResult.Cancel Then
                        Me.SetErrorControl(.imdSagyoDate_E)
                        Me.ShowMessage("G007")
                        Return False
                    End If

                End If
            ElseIf String.IsNullOrEmpty(closeDate) = False Then


                If String.IsNullOrEmpty(.txtSeikyuCD.TextValue) = True Then
                    wID = "W172"
                Else
                    wID = "W174"
                End If

                '作業日To < 締め日区分 の場合エラー
                If tDate.Day < Convert.ToInt16(closeDate) Then
                    rtn = MyBase.ShowMessage(wID, New String() {Convert.ToInt16(closeDate).ToString()})
                    If rtn = MsgBoxResult.Ok Then
                        Return True

                    ElseIf rtn = MsgBoxResult.Cancel Then
                        Me.SetErrorControl(.imdSagyoDate_E)
                        Me.ShowMessage("G007")
                        Return False
                    End If

                End If
            End If

        End With

        Return True

    End Function


#End Region '印刷時チェック

#Region "新規時チェック"

    'START YANAI 20120319　作業画面改造
#Region "単項目チェック"
    ''' <summary>
    ''' 検索時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSinkiSingleCheck(ByVal eventShubetsu As LME010C.EventShubetsu) As Boolean

        With Me._Frm

            '【単項目チェック】

            '******************** ヘッダ項目の入力チェック ********************

            '営業所
            '2016.01.06 UMANO 英語化対応START
            '.cmbEigyo.ItemName() = "営業所"
            .cmbEigyo.ItemName() = .LmTitleLabel4.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbEigyo.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                Return False
            End If

            '倉庫
            '2016.01.06 UMANO 英語化対応START
            '.cmbWare.ItemName() = "倉庫"
            .cmbWare.ItemName() = .LmTitleLabel5.Text()
            '2016.01.06 UMANO 英語化対応START
            .cmbWare.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbWare) = False Then
                Return False
            End If

            '荷主コード(大)
            '2016.01.06 UMANO 英語化対応START
            '.txtCustCD_L.ItemName() = "荷主コード(大)"
            .txtCustCD_L.ItemName() = .LmTitleLabel6.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtCustCD_L.IsHissuCheck() = True
            .txtCustCD_L.IsForbiddenWordsCheck() = True
            .txtCustCD_L.IsByteCheck() = 5
            .txtCustCD_L.IsFullByteCheck() = 5
            If MyBase.IsValidateCheck(.txtCustCD_L) = False Then
                Return False
            End If

        End With

        Return True

    End Function
    'END YANAI 20120319　作業画面改造
#End Region

#End Region

#Region "連続入力時チェック"
    'START YANAI 20120319　作業画面改造
#Region "単項目チェック"
    ''' <summary>
    ''' 連続入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsRenzokuSingleCheck() As Boolean

        With Me._Frm

            '【単項目チェック】

            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            'スプレッド項目の入力チェック
            Dim max As Integer = Me._ChkList.Count - 1
            Dim chkVal As String = String.Empty

            For i As Integer = 0 To max
                '請求確認チェック
                chkVal = .sprSagyo.ActiveSheet.Cells(Convert.ToInt32(_ChkList(i)), LME010G.sprDetailDef.SKYU_CHK.ColNo).Value().ToString()
                If chkVal = "01" Then
                    MyBase.ShowMessage("E127")
                    Return False
                End If

                '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
                ''自営業所チェック
                'chkVal = .sprSagyo.ActiveSheet.Cells(Convert.ToInt32(_ChkList(i)), LME010G.sprDetailDef.NRS_BR_CD.ColNo).Value().ToString()
                'If chkVal.Equals(LMUserInfoManager.GetNrsBrCd().ToString()) = False Then
                '    MyBase.ShowMessage("E178", New String() {"連続入力"})
                '    Return False
                'End If

            Next

        End With

        Return True

    End Function
#End Region
    'END YANAI 20120319　作業画面改造

#End Region

#Region "完了時チェック"

#Region "単項目チェック"
    ''' <summary>
    ''' 完了時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKanryoSingleCheck() As Boolean

        With Me._Frm
            '【単項目チェック】

            'チェックリスト初期化
            Me._ChkList = New ArrayList()
            'チェック行リスト取得
            Me._ChkList = Me.getCheckList()

            'スプレッド項目の入力チェック
            Dim max As Integer = Me._ChkList.Count - 1
            Dim chkVal As String = String.Empty
            Dim errVal As Integer = 0

            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
            ''自営業所チェック
            'For i As Integer = 0 To max
            '    chkVal = .sprSagyo.ActiveSheet.Cells(Convert.ToInt32(_ChkList(i)), LME010G.sprDetailDef.NRS_BR_CD.ColNo).Value().ToString()

            '    If chkVal.Equals(LMUserInfoManager.GetNrsBrCd().ToString()) = False Then
            '        MyBase.ShowMessage("E178", New String() {"完了"})
            '        Return False
            '    End If

            'Next

            '完了チェック
            For i As Integer = 0 To max
                chkVal = .sprSagyo.ActiveSheet.Cells(Convert.ToInt32(_ChkList(i)), LME010G.sprDetailDef.SAGYO_COMP.ColNo).Value().ToString()

                If chkVal = "01" Then
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E454", New String() {"完了済み", "完了", String.Concat(Convert.ToInt32(_ChkList(i)), "行目")})
                    MyBase.ShowMessage("E850", New String() {Convert.ToString(Convert.ToInt32(_ChkList(i)))})
                    '2016.01.06 UMANO 英語化対応END
                    Return False
                End If
            Next

        End With

        Return True

    End Function
#End Region

#Region "関連チェック"
    ''' <summary>
    ''' 完了時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Public Function IsKanryoKanrenCheck() As Boolean

        With Me._Frm


        End With

        Return True

    End Function

#End Region

#End Region

    'START YANAI 20120319　作業画面改造
#Region "ダブルクリック時チェック"

#Region "単項目チェック"
    ''' <summary>
    ''' ダブルクリックチェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsDoubleClickSingleCheck() As Boolean

        With Me._Frm

            '【単項目チェック】
            'スプレッド項目の入力チェック
            Dim chkVal As String = String.Empty

            '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
            ''自営業所チェック
            'chkVal = .sprSagyo.ActiveSheet.Cells(.sprSagyo.Sheets(0).ActiveRow.Index(), LME010G.sprDetailDef.NRS_BR_CD.ColNo).Value().ToString()

            'If chkVal.Equals(LMUserInfoManager.GetNrsBrCd().ToString()) = False Then
            '    MyBase.ShowMessage("E178", New String() {"編集"})
            '    Return False
            'End If

        End With

        Return True

    End Function
#End Region

#Region "関連チェック"
    ''' <summary>
    ''' ダブルクリック時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Public Function IsDoubleClickKanrenCheck() As Boolean

        With Me._Frm

        End With

        Return True

    End Function

#End Region

#End Region
    'END YANAI 20120319　作業画面改造

#End Region

#Region "内部メソッド"

#Region "マスタ存在チェック"
    ''' <summary>
    ''' ユーザーマスタの存在チェック
    ''' </summary>
    ''' <param name="text">チェック対象文字列</param>
    ''' <returns>ユーザー名</returns>
    ''' <remarks></remarks>
    Private Function IsExistUserNm(ByVal text As String) As String

        ''存在チェック
        Dim userNm As String = String.Empty

        IsExistUserNm = userNm

    End Function
#End Region

#Region "選択チェック"
    ''' <summary>
    ''' 選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSelectDataChk() As Boolean

        'チェックリスト初期化
        Me._ChkList = New ArrayList()

        'チェック行リスト取得
        Me._ChkList = Me.getCheckList()

        '選択チェック
        If Me._Vcon.IsSelectChk(Me._ChkList.Count()) = False Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function
#End Region

#Region "単一選択チェック"
    ''' <summary>
    ''' 単一選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSelectOneDataChk() As Boolean

        'チェックリスト初期化
        Me._ChkList = New ArrayList()

        'チェック行リスト取得
        Me._ChkList = Me.getCheckList()

        '選択チェック
        If Me._Vcon.IsSelectOneChk(Me._ChkList.Count()) = False Then
            MyBase.ShowMessage("E008")
            Return False
        End If

        Return True

    End Function
#End Region

#Region "選択行取得"
    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LME010C.SprColumnIndex.DEF

        Return Me._Vcon.SprSelectList(defNo, Me._Frm.sprSagyo)

    End Function

#End Region

#Region "数値Nullcheck"

    Private Function NumNullCheck(ByVal val As String) As Boolean


        If String.IsNullOrEmpty(val) = True Then
            MyBase.ShowMessage("E008")
            Return False

        End If

        Return True

    End Function
#End Region

#Region "数値ZeroCheck"

    Private Function NumZeroCheck(ByVal val As String) As Boolean

        Dim dblval As Double = 0

        dblval = Convert.ToDouble(val.ToString())

        Dim rtn As MsgBoxResult

        If dblval = Convert.ToDouble(LME010C.SAGYO_GK_MIN) = True Then
            '2016.01.06 UMANO 英語化対応START
            'rtn = MyBase.ShowMessage("W170", New String() {"請求金額", "０円"})
            rtn = MyBase.ShowMessage("W170")
            '2016.01.06 UMANO 英語化対応END

            If rtn = MsgBoxResult.Ok Then
                Return True

            ElseIf rtn = MsgBoxResult.Cancel Then
                Me.SetErrorControl(_Frm.txtEditNum)
                Return False
                Exit Function

            End If

        End If

        Return True

    End Function
#End Region

    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimHeaderSpaceTextValue()

        With Me._Frm

            .txtCustCD_L.TextValue = Me._Frm.txtCustCD_L.TextValue.Trim()
            .txtCustCD_M.TextValue = Me._Frm.txtCustCD_M.TextValue.Trim()
            .txtSeikyuCD.TextValue = Me._Frm.txtSeikyuCD.TextValue.Trim()
            .txtSagyoCD.TextValue = Me._Frm.txtSagyoCD.TextValue.Trim()
            .txtSagyoSijiNO.TextValue = Me._Frm.txtSagyoSijiNO.TextValue.Trim()
        End With

    End Sub

    ''' <summary>
    ''' 範囲チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsHaniCheck(ByVal value As Decimal, ByVal minData As Decimal, ByVal maxData As Decimal) As Boolean

        If value < minData OrElse _
            maxData < value Then
            Return False
        End If

        Return True

    End Function


    ''' <summary>
    ''' 請求先マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSeikyuToExistChk() As Boolean

        With Me._Frm

            '請求先コードの存在チェック
            Dim seikyuToCd As String = .txtEditTxt.TextValue

            '値がない場合、スルー
            If String.IsNullOrEmpty(seikyuToCd) = True Then
                Return True
            End If

            Dim drs As DataRow() = Me._Vcon.SelectSeiqtoListDataRow(.cmbEigyo.SelectedValue.ToString(), seikyuToCd)

            '取得できない場合、エラー
            If drs.Length < 1 Then
                Me._Vcon.SetErrorControl(.txtEditTxt)
                .lblEditNM.TextValue = String.Empty
                Return False
            End If

            'マスタの値を設定
            .txtEditTxt.TextValue = drs(0).Item("SEIQTO_CD").ToString()

            Return True

        End With

    End Function

#End Region

#End Region 'Method

#Region "ユーティリティ"

    ''' <summary>
    ''' エラー項目の背景色とフォーカスを設定する
    ''' </summary>
    ''' <param name="ctl">エラーコントロール</param>
    ''' <remarks></remarks>
    Friend Sub SetErrorControl(ByVal ctl As Control)

        Dim errorColor As System.Drawing.Color = Utility.LMGUIUtility.GetAttentionBackColor()

        If TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMComboKubun = True Then

            DirectCast(ctl, Win.InputMan.LMComboKubun).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMImNumber = True Then

            DirectCast(ctl, Win.InputMan.LMImNumber).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMImDate = True Then

            DirectCast(ctl, Win.InputMan.LMImDate).BackColorDef = errorColor


        End If

        ctl.Focus()
        ctl.Select()

    End Sub

    ''' <summary>
    ''' 四捨五入
    ''' </summary>
    ''' <param name="decValue"></param>
    ''' <param name="iDigits"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ToRound(ByVal decValue As Decimal, ByVal iDigits As Integer) As Decimal

        Dim dCoef As Double = System.Math.Pow(10, iDigits)

        If decValue > 0 Then
            Return Convert.ToDecimal(Math.Floor((decValue * dCoef) + 0.5) / dCoef)
        Else
            Return Convert.ToDecimal(Math.Ceiling((decValue * dCoef) - 0.5) / dCoef)
        End If
    End Function

#End Region

End Class
