' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDIサブシステム
'  プログラムID     :  LMH030V : EDI出荷データ検索
'  作  成  者       :  
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.DSL
Imports Microsoft.Office.Interop

''' <summary>
''' LMH030Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMH030V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMH030F

    ''' <summary>
    ''' このValidateクラスが紐付くハンドラクラスクラス
    ''' </summary>
    ''' <remarks></remarks>

    Private _H As LMH030H

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMHControlV

    ''' <summary>
    ''' チェックリストを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList


    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMHControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMH030F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        '2011.06.16 ADD 馬野 NEWする必要があるのか今後検討!!!
        Me._Gcon = New LMHControlG(frm)

        '2011.06.15 EDIT 馬野
        'Validate共通クラスの設定
        Me._Vcon = New LMHControlV(handlerClass, DirectCast(frm, Form), Me._Gcon)

        'Handlerクラスの設定
        Me._H = New LMH030H()

        Me._ChkList = New ArrayList()

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "権限チェック"
    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMH030C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMH030C.EventShubetsu.SAVEOUTKA          '出荷登録処理

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

            Case LMH030C.EventShubetsu.CREATEJISSEKI    '実績作成処理

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

            Case LMH030C.EventShubetsu.HIMODUKE    '紐付け

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

            Case LMH030C.EventShubetsu.EDITORIKESI  'EDI取消

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

            Case LMH030C.EventShubetsu.TORIKOMI     '取込

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

            Case LMH030C.EventShubetsu.SAVEUNSO     '運送登録

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

            Case LMH030C.EventShubetsu.TORIKESIJISSEKI     '実績取消

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

            Case LMH030C.EventShubetsu.KENSAKU          '検索

                '10:閲覧者、50:外部の場合エラー
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

            Case LMH030C.EventShubetsu.MASTER          'MASTER

                '10:閲覧者、50:外部の場合エラー
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

            Case LMH030C.EventShubetsu.DEF_CUST         '初期荷主変更

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

            Case LMH030C.EventShubetsu.CLOSE           '閉じる
                'すべての権限許可
                kengenFlg = True


            Case LMH030C.EventShubetsu.IKKATUHENKO  '一括変更

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

            Case LMH030C.EventShubetsu.OUTPUTPRINT  '出力(CSV作成・印刷)

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

            Case LMH030C.EventShubetsu.SELPRINT     '検索条件印刷

                '10:閲覧者、50:外部の場合エラー
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

                'Case LMH030C.EventShubetsu.EXE         '実行

                '    Select Case Convert.ToInt32(Me._Frm.cmbExe.SelectedValue)

            Case LMH030C.EventShubetsu.SOUSINZUMI_JISSEKIMI, LMH030C.EventShubetsu.SOUSINZUMI_SOUSINMACHI

                '実行種別が実績送信済⇒送信待,実績送信済⇒実績未は40:システム管理者以外はエラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = False
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = False
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMH030C.EventShubetsu.TORIKESI_MITOUROKU, LMH030C.EventShubetsu.SAKUSEIZUMI_JISSEKIMI, _
                 LMH030C.EventShubetsu.SAKURA_TUIKAJIKKOU, LMH030C.EventShubetsu.TOUROKUZUMI_MITOUROKU, _
                 LMH030C.EventShubetsu.UNSOTORIKESI_MITOUROKU, LMH030C.EventShubetsu.CUST_CD_SETUP '2012.07.02 追加 terakawa　

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

                'End Select

            Case LMH030C.EventShubetsu.DOUBLE_CLICK         'ダブルクリック

                '10:閲覧者、50:外部の場合エラー
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
#End Region

#Region "入力チェック（検索）"

    ''' <summary>
    ''' 検索時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKensakuSingleCheck() As Boolean

        'ヘッダ項目のスペース除去
        Call Me.TrimHeaderSpaceTextValue()

        'スプレッドのスペース除去
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprEdiList, 0)

        With Me._Frm

            '【単項目チェック】

            '******************** ヘッダ項目の入力チェック ********************

            '進捗区分

            'Dim chkFlg As Boolean = False

            ''進捗区分
            ''For Each item As Control In .grpSTATUS.Controls
            ''    If item.GetType().Equals(GetType(LMCheckBox)) Then
            ''        If DirectCast(item, CheckBox).Checked = True Then
            ''            chkFlg = True
            ''        End If
            ''    End If
            ''Next

            ''If chkFlg = False Then
            ''    MyBase.ShowMessage("E199", New String() {"進捗区分"})
            ''    Return False
            ''End If

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
            .txtCustCD_L.IsHissuCheck() = True
            .txtCustCD_L.IsForbiddenWordsCheck() = True
            '.txtCustCD_L.IsByteCheck() = 5
            .txtCustCD_L.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCD_L) = False Then
                Return False
            End If

            '荷主コード(中)
            .txtCustCD_M.ItemName() = "荷主コード(中)"
            .txtCustCD_M.IsForbiddenWordsCheck() = True
            '.txtCustCD_M.IsByteCheck() = 2
            .txtCustCD_M.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCD_M) = False Then
                Return False
            End If

            '担当者コード
            .txtTantouCd.ItemName() = "担当者コード"
            .txtTantouCd.IsForbiddenWordsCheck() = True
            .txtTantouCd.IsByteCheck() = 5
            If MyBase.IsValidateCheck(.txtTantouCd) = False Then
                Return False
            End If

            '届先コード
            .txtTodokesakiCd.ItemName() = "届先コード"
            .txtTodokesakiCd.IsForbiddenWordsCheck() = True
            .txtTodokesakiCd.IsByteCheck() = 15
            If MyBase.IsValidateCheck(.txtTodokesakiCd) = False Then
                Return False
            End If

            'EDI取込日FROM
            If .imdEdiDateFrom.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"EDI取込日From", "8"})
                Return False
            End If

            'EDI取込日TO
            If .imdEdiDateTo.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"EDI取込日To", "8"})
                Return False
            End If


            'EDI日付検索区分(出荷予定日,納入予定日)
            If String.IsNullOrEmpty(.cmbSelectDate.SelectedValue.ToString) = False Then

                Dim dateMsg As String = String.Empty

                If .cmbSelectDate.SelectedValue.ToString = "01" Then
                    dateMsg = "出荷予定日"
                Else
                    dateMsg = "納入予定日"
                End If

                If .imdSearchDateFrom.IsDateFullByteCheck(8) = False Then
                    MyBase.ShowMessage("E038", New String() {String.Concat(dateMsg, "From"), "8"})
                    Return False
                End If

                If .imdSearchDateTo.IsDateFullByteCheck(8) = False Then
                    MyBase.ShowMessage("E038", New String() {String.Concat(dateMsg, "To"), "8"})
                    Return False
                End If

            End If

            '******************** スプレッド項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprEdiList)

            'オーダー番号
            vCell.SetValidateCell(0, LMH030G.sprEdiListDef.ORDER_NO.ColNo)
            vCell.ItemName() = "オーダー番号"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 30
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名
            vCell.SetValidateCell(0, LMH030G.sprEdiListDef.CUST_NM.ColNo)
            vCell.ItemName() = "荷主名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 122
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '届先名
            vCell.SetValidateCell(0, LMH030G.sprEdiListDef.DEST_NM.ColNo)
            vCell.ItemName() = "届先名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 80
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '出荷時注意事項
            vCell.SetValidateCell(0, LMH030G.sprEdiListDef.REMARK.ColNo)
            vCell.ItemName() = "出荷時注意事項"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '配送時注意事項
            vCell.SetValidateCell(0, LMH030G.sprEdiListDef.UNSO_ATT.ColNo)
            vCell.ItemName() = "配送時注意事項"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '商品（中1）
            vCell.SetValidateCell(0, LMH030G.sprEdiListDef.GOODS_NM.ColNo)
            vCell.ItemName() = "商品（中1）"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '届先住所
            vCell.SetValidateCell(0, LMH030G.sprEdiListDef.DEST_AD.ColNo)
            vCell.ItemName() = "届先住所"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 124
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '運送会社名
            vCell.SetValidateCell(0, LMH030G.sprEdiListDef.UNSO_CORP.ColNo)
            vCell.ItemName() = "運送会社名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 122
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'EDI管理番号（大）
            vCell.SetValidateCell(0, LMH030G.sprEdiListDef.EDI_NO.ColNo)
            vCell.ItemName() = "EDI管理番号（大）"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 9
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'まとめ番号
            vCell.SetValidateCell(0, LMH030G.sprEdiListDef.EDI_NO.ColNo)
            vCell.ItemName() = "まとめ番号"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 9
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '出荷管理番号（大）
            vCell.SetValidateCell(0, LMH030G.sprEdiListDef.KANRI_NO.ColNo)
            vCell.ItemName() = "出荷管理番号（大）"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 9
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '注文番号
            vCell.SetValidateCell(0, LMH030G.sprEdiListDef.BUYER_ORDER_NO.ColNo)
            vCell.ItemName() = "注文番号"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 30
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '担当者
            vCell.SetValidateCell(0, LMH030G.sprEdiListDef.TANTO_USER_NM.ColNo)
            vCell.ItemName() = "担当者"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '作成者
            vCell.SetValidateCell(0, LMH030G.sprEdiListDef.SYS_ENT_USER_NM.ColNo)
            vCell.ItemName() = "作成者"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '最終更新者
            vCell.SetValidateCell(0, LMH030G.sprEdiListDef.SYS_UPD_USER_NM.ColNo)
            vCell.ItemName() = "最終更新者"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '2013.03.05 / Notes1909 受信ファイル追加 開始
            '受信ファイル名
            vCell.SetValidateCell(0, LMH030G.sprEdiListDef.EDI_FILE_NAME.ColNo)
            vCell.ItemName() = "受信ファイル名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 300
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            ''2013.03.05 / Notes1909 受信ファイル追加 終了
        End With

        Return True

    End Function

    ''' <summary>
    ''' 検索時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuKanrenCheck() As Boolean

        With Me._Frm

            'EDI取込日
            If String.IsNullOrEmpty(.imdEdiDateFrom.TextValue) = False AndAlso String.IsNullOrEmpty(.imdEdiDateTo.TextValue) = False Then
                If Convert.ToInt32(.imdEdiDateTo.TextValue) < Convert.ToInt32(.imdEdiDateFrom.TextValue) Then
                    'EDI取込日FromよりEDI取込日Toが過去日の場合エラー
                    Me.ShowMessage("E039", New String() {"EDI取込日To", "EDI取込日From"})
                    .imdEdiDateFrom.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdEdiDateTo.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdEdiDateFrom.Focus()
                    Return False
                End If

            End If

            'EDI日付検索区分(出荷予定日,納入予定日)
            If String.IsNullOrEmpty(.cmbSelectDate.SelectedValue.ToString) = False Then

                Dim dateMsg As String = String.Empty

                If .cmbSelectDate.SelectedValue.ToString = "01" Then
                    dateMsg = "出荷予定日"
                Else
                    dateMsg = "納入予定日"
                End If

                '出荷予定日,納入予定日
                If String.IsNullOrEmpty(.imdSearchDateFrom.TextValue) = False AndAlso String.IsNullOrEmpty(.imdSearchDateTo.TextValue) = False Then
                    If Convert.ToInt32(.imdSearchDateTo.TextValue) < Convert.ToInt32(.imdSearchDateFrom.TextValue) Then
                        'FromよりToが過去日の場合エラー
                        Me.ShowMessage("E039", New String() {String.Concat(dateMsg, "To"), String.Concat(dateMsg, "From")})
                        .imdSearchDateFrom.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                        .imdSearchDateTo.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                        .imdSearchDateFrom.Focus()
                        Return False
                    End If

                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimHeaderSpaceTextValue()

        With Me._Frm

            .txtCustCD_L.TextValue = Me._Frm.txtCustCD_L.TextValue.Trim()
            .txtCustCD_M.TextValue = Me._Frm.txtCustCD_M.TextValue.Trim()
            .txtTodokesakiCd.TextValue = Me._Frm.txtTodokesakiCd.TextValue.Trim()
            .txtTantouCd.TextValue = Me._Frm.txtTantouCd.TextValue.Trim()
        End With

    End Sub


#End Region '入力チェック（検索）

#Region "入力チェック（出荷登録,運送登録）"

    '2012.03.25 大阪対応START
#Region "単項目チェック"

    ''' <summary>
    ''' 出荷登録イベントの入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsSaveoutkaSingleCheck(ByVal eventShubetsu As LMH030C.EventShubetsu) As Boolean

        With Me._Frm

            '【単項目チェック】

            '選択チェック
            If IsSelectDataChk() = False Then
                Return False
            End If

        End With

        '自営業所チェック
        Dim rtnMsg As String = String.Empty
        '2012.03.25 大阪対応START
        Select Case eventShubetsu
            Case LMH030C.EventShubetsu.SAVEOUTKA

                rtnMsg = "出荷登録"
            Case LMH030C.EventShubetsu.SAVEUNSO
                rtnMsg = "運送登録"

        End Select
        '2012.03.25 大阪対応END

        If Me.IsNrsChk(rtnMsg) = False Then
            Return False
        End If

        Return True

    End Function

#End Region
    '2012.03.25 大阪対応END

#Region "関連チェック"

    ''' <summary>
    ''' 出荷登録,運送登録時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSaveoutkaKanrenCheck(ByVal eventshubetsu As Integer, ByRef errDs As DataSet) As Hashtable

        '続行確認
        Dim rtn As MsgBoxResult

        Dim errHt As Hashtable = New Hashtable

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0

        Dim delKb As String = String.Empty
        Dim custHoldF As String = String.Empty
        Dim outF As String = String.Empty
        Dim custUnsoF As String = String.Empty
        Dim mCount As String = String.Empty
        Dim akakuroF As String = String.Empty
        Dim jissekiF As String = String.Empty
        Dim custDelDisp As String = String.Empty
        Dim ediCtlNo As String = String.Empty
        '2012.03.26 大阪対応START
        Dim unsoKbn As String = String.Empty
        '2012.03.26 大阪対応END
        '要望番号:1243 terakawa 2012.07.05 Start
        Dim custindex As String = String.Empty
        '要望番号:1243 terakawa 2012.07.05 End

        ''2011.10.14 廃止START デュポンEDI即実績作成データは未登録には表示しない
        ''2011.10.07 START デュポンEDIデータ即実績作成対応
        'Dim custJissekiF As String = String.Empty
        ''2011.10.07 END
        ''2011.10.14 廃止END

#If True Then ' BP運送会社自動設定対応 20161117 added by inoue

        Dim destCd As String = String.Empty
        Dim outkaPlanDate As String = String.Empty
        Dim freeC05 As String = String.Empty
#End If

        Dim outka_CNT As Integer = 0        ''ADD 2018/10/09 依頼番号 : 002302   【LMS】千葉東レ_セミEDI_出荷数=0,マイナスでも、運送登録ができるバグ

        '日本合成運送データチェック対応（001704）
        Dim unsoDataExists As String = String.Empty
        Dim ncgoOpeoutOnly As String = String.Empty     'Add 2018/10/31 要望番号002808

        errDs = New LMH030DS()
        Dim dr As DataRow

        For i As Integer = 0 To max

            With Me._Frm.sprEdiList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                delKb = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_DEL_KB.ColNo).Value().ToString()
                custHoldF = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_HOLDOUT.ColNo).Value().ToString()
                outF = .Cells(selectRow, LMH030G.sprEdiListDef.OUT_FLAG.ColNo).Value().ToString()
                custUnsoF = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_UNSOFLG.ColNo).Value().ToString()
                mCount = .Cells(selectRow, LMH030G.sprEdiListDef.MDL_REC_CNT.ColNo).Value().ToString()
                akakuroF = .Cells(selectRow, LMH030G.sprEdiListDef.AKAKURO_FLG.ColNo).Value().ToString()
                jissekiF = .Cells(selectRow, LMH030G.sprEdiListDef.JISSEKI_FLAG.ColNo).Value().ToString()
                custDelDisp = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_DELDISP.ColNo).Value().ToString()
                ediCtlNo = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo).Value().ToString()
                '2012.03.26 大阪対応START
                If String.IsNullOrEmpty(.Cells(selectRow, LMH030G.sprEdiListDef.FREE_C30.ColNo).Value().ToString()) = False Then
                    unsoKbn = (.Cells(selectRow, LMH030G.sprEdiListDef.FREE_C30.ColNo).Value().ToString()).Substring(0, 2)
                End If
                '2012.03.26 大阪対応END
                '要望番号:1243 terakawa 2012.07.05 Start
                custindex = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INDEX.ColNo).Value().ToString()
                '要望番号:1243 terakawa 2012.07.05 End

                ' ''2011.10.14 廃止START デュポンEDI即実績作成データは未登録には表示しない
                ''2011.10.07 START デュポンEDIデータ即実績作成対応
                'custJissekiF = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_JISSEKI.ColNo).Value().ToString()
                ''2011.10.07 END
                ' ''2011.10.14 廃止END
#If True Then ' BP運送会社自動設定対応 20161117 added by inoue
                destCd = .Cells(selectRow, LMH030G.sprEdiListDef.DEST_CD.ColNo).Value().ToString()
                outkaPlanDate = .Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_PLAN_DATE.ColNo).Value().ToString()
                freeC05 = .Cells(selectRow, LMH030G.sprEdiListDef.FREE_C05.ColNo).Value().ToString()

#End If
                '日本合成運送データチェック対応(001704)
                unsoDataExists = .Cells(selectRow, LMH030G.sprEdiListDef.UNSOEDI_EXISTS_FLAG.ColNo).Value().ToString()
                ncgoOpeoutOnly = .Cells(selectRow, LMH030G.sprEdiListDef.NCGO_OPEOUT_ONLY_FLG.ColNo).Value().ToString()     'Add 2018/10/31 要望番号002808

                '未登録データチェック
                '★★★
                '2011.10.14 復活START デュポンEDI即実績作成データは未登録には表示しない
                If ((delKb = "0" OrElse delKb = "3" OrElse delKb = "2") OrElse (delKb = "1" AndAlso custDelDisp = "1")) _
                   AndAlso ((outF = "0" OrElse outF = "2") AndAlso (jissekiF = "0" OrElse jissekiF = "9")) Then
                    '2011.10.14 復活END
                    ''2011.10.14 廃止START デュポンEDI即実績作成データは未登録には表示しない
                    ''2011.10.07 START デュポンEDIデータ即実績作成対応
                    'If (((delKb = "0" OrElse delKb = "3" OrElse delKb = "2") OrElse (delKb = "1" AndAlso custDelDisp = "1")) _
                    '   AndAlso ((outF = "0" OrElse outF = "2") AndAlso (jissekiF = "0" OrElse jissekiF = "9"))) _
                    '   OrElse (((delKb = "0" OrElse delKb = "3" OrElse delKb = "2") AndAlso (custJissekiF = "3")) _
                    '   AndAlso ((outF = "0" OrElse outF = "2") AndAlso (jissekiF = "0" OrElse jissekiF = "1" OrElse jissekiF = "9"))) Then
                    '    ''2011.10.14 廃止END
                    '★★★

                Else
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E336"
                    dr("PARA1") = "未登録データ"
                    dr("PARA2") = "出荷登録"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                    'Me.ShowMessage("E336", New String() {"未登録データ", "出荷登録"})   'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For

                End If


#If True Then   'ADD 2018/10/09 依頼番号 : 002302   【LMS】千葉東レ_セミEDI_出荷数=0,マイナスでも、運送登録ができるバグ
                If (eventshubetsu).Equals(LMH030C.EventShubetsu.SAVEUNSO) Then
                    '運送登録時
                    outka_CNT = CInt(.Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_CNT.ColNo).Value().ToString())
                    If outka_CNT < 1 Then
                        'エラーがある場合、DataTableに設定
                        dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                        dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                        dr("MESSAGE_ID") = "E336"
                        dr("PARA1") = "出荷数１以上"
                        dr("PARA2") = "運送登録"
                        dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                        dr("KEY_VALUE") = ediCtlNo
                        dr("ROW_NO") = selectRow.ToString()
                        errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                        'Me.ShowMessage("E336", New String() {"出荷数１以上", 送登録"})   'エラーメッセージ
                        errHt.Add(i, String.Empty)
                        Continue For

                    End If
                End If

#End If
                '2012.03.26 大阪対応START

                Select Case eventshubetsu

                    Case LMH030C.EventShubetsu.SAVEOUTKA            '出荷登録の場合

                        '出荷登録対象チェック
                        'If delKb = "0" OrElse (delKb = "3" AndAlso (custHoldF = "1" OrElse custHoldF = "2")) _
                        '    AndAlso outF = "0" AndAlso custUnsoF <> "1" Then
                        'Else
                        '    Me.ShowMessage("E320", New String() {"削除データもしくは保留データ", "出荷登録"})    'エラーメッセージ
                        '    errHt.Add(i, String.Empty)
                        '    Continue For
                        'End If

                        '★★★
                        '出荷登録対象チェック
                        If outF = "0" Then
                        Else
                            'エラーがある場合、DataTableに設定
                            dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                            dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                            dr("MESSAGE_ID") = "E320"
                            dr("PARA1") = "出荷書込対象外データ"
                            dr("PARA2") = "出荷登録"
                            dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                            dr("KEY_VALUE") = ediCtlNo
                            dr("ROW_NO") = selectRow.ToString()
                            errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                            'Me.ShowMessage("E320", New String() {"削除データもしくは保留データ", "出荷登録"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        If custUnsoF <> "1" Then
                        Else
                            'エラーがある場合、DataTableに設定
                            dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                            dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                            dr("MESSAGE_ID") = "E320"
                            dr("PARA1") = "運送データ"
                            dr("PARA2") = "出荷登録"
                            dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                            dr("KEY_VALUE") = ediCtlNo
                            dr("ROW_NO") = selectRow.ToString()
                            errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                            'Me.ShowMessage("E320", New String() {"削除データもしくは保留データ", "出荷登録"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If
                        '★★★

                        If delKb = "3" AndAlso custHoldF = "1" Then
                            '保留データかつ保留データ登録フラグが"1"の場合はワーニング
                            rtn = Me.ShowMessage("W169", New String() {"出荷登録", ediCtlNo})
                            If rtn = MsgBoxResult.Ok Then
                            ElseIf rtn = MsgBoxResult.Cancel Then
                                errHt.Add(i, String.Empty)
                                Continue For
                            End If

                            '2012.05.30 修正START
                        ElseIf delKb = "3" AndAlso custHoldF = "2" Then
                            '2012.05.30 修正END
                            'エラーがある場合、DataTableに設定
                            dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                            dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                            dr("MESSAGE_ID") = "E376"
                            dr("PARA1") = "保留データ"
                            dr("PARA2") = "出荷登録"
                            dr("PARA3") = "EDI取消"
                            dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                            dr("KEY_VALUE") = ediCtlNo
                            dr("ROW_NO") = selectRow.ToString()
                            errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                            'Me.ShowMessage("E376", New String() {"保留データ", "出荷登録", "EDI取消"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For

                        ElseIf delKb = "1" OrElse delKb = "2" Then

                            '↓FFEM特殊処理↓
                            '実績データのキャンセル報告対応
                            '2014.06.09 追加START
                            Select Case custindex
                                'custindex は M_EDI_CUST.EDI_CUST_INDEX であり NRS_BR_CD ではないため当初記述に戻す↓ADD 2022/10/19 033380   【LMS】FFEM足柄工場LMS導入 F2追加
                                'custindex は M_EDI_CUST.EDI_CUST_INDEX であり NRS_BR_CD ではないため当初記述に戻す↓Case "96", "98", "F2" '富士フイルム(千葉BC)
                                Case "96" '富士フイルム(千葉BC)
                                    'キャンセルデータはエラーにしない
                                    If delKb = "1" Then
                                        'エラーがある場合、DataTableに設定
                                        dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                                        dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                                        dr("MESSAGE_ID") = "E320"
                                        dr("PARA1") = "削除データもしくはキャンセルデータ"
                                        dr("PARA2") = "出荷登録"
                                        dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                                        dr("KEY_VALUE") = ediCtlNo
                                        dr("ROW_NO") = selectRow.ToString()
                                        errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                                        'Me.ShowMessage("E320", New String() {"削除データもしくはキャンセルデータ", "出荷登録"})    'エラーメッセージ
                                        errHt.Add(i, String.Empty)
                                        Continue For
                                    Else
                                        ' DEL_KB = '2'
                                        ' 入荷EDI 準拠
                                        ' ([10 千葉, 60 中部] 以外) の営業所ではキャンセルデータもエラーとする
                                        ' custindex = "60" …… M_EDI_CUST.EDI_CUST_INDEX = 60(FFEM)
                                        If custDelDisp = "0" Then
                                            ' custDelDisp = "0" …… M_EDI_CUST.FLAG_08 = '0'([10 千葉, 60 中部] 以外)
                                            dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                                            dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                                            dr("MESSAGE_ID") = "E320"
                                            dr("PARA1") = "削除データもしくはキャンセルデータ"
                                            dr("PARA2") = "出荷登録"
                                            dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                                            dr("KEY_VALUE") = ediCtlNo
                                            dr("ROW_NO") = selectRow.ToString()
                                            errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                                            errHt.Add(i, String.Empty)
                                            Continue For
                                        End If
                                    End If

                                Case Else

                                    'エラーがある場合、DataTableに設定
                                    dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                                    dr("MESSAGE_ID") = "E320"
                                    dr("PARA1") = "削除データもしくはキャンセルデータ"
                                    dr("PARA2") = "出荷登録"
                                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                                    dr("KEY_VALUE") = ediCtlNo
                                    dr("ROW_NO") = selectRow.ToString()
                                    errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                                    'Me.ShowMessage("E320", New String() {"削除データもしくはキャンセルデータ", "出荷登録"})    'エラーメッセージ
                                    errHt.Add(i, String.Empty)
                                    Continue For

                            End Select
                            '↑FFEM特殊処理↑
                            '実績データのキャンセル報告対応
                            '2014.06.09 追加END

                        ElseIf delKb = "0" Then

                        End If

                        'EDI出荷(中)の件数が 0の場合エラー
                        If mCount = "0" Then
                            'エラーがある場合、DataTableに設定
                            dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                            dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                            dr("MESSAGE_ID") = "E320"
                            dr("PARA1") = "中件数が 0件"
                            dr("PARA2") = "出荷登録"
                            dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                            dr("KEY_VALUE") = ediCtlNo
                            dr("ROW_NO") = selectRow.ToString()
                            errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                            'Me.ShowMessage("E320", New String() {"中件数が 0件", "出荷登録"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        'EDI出荷(中)に赤データがある場合エラー
                        If akakuroF = "1" Then
                            'エラーがある場合、DataTableに設定
                            dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                            dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                            dr("MESSAGE_ID") = "E320"
                            dr("PARA1") = "中データが赤データ"
                            dr("PARA2") = "出荷登録"
                            dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                            dr("KEY_VALUE") = ediCtlNo
                            dr("ROW_NO") = selectRow.ToString()
                            errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                            'Me.ShowMessage("E320", New String() {"中データが赤データ", "出荷登録"})   'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If
#If True Then ' BP運送会社自動設定対応 20161117 added by inoue
                        If (LMH030C.EDI_CUST_INDEX.BP.Equals(custindex)) Then

                            If (String.IsNullOrEmpty(destCd)) Then
                                ' 届先CD未設定

                                'エラーがある場合、DataTableに設定
                                dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                                dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                                dr("MESSAGE_ID") = "E340"
                                dr("PARA1") = "届先"
                                dr("PARA2") = ""
                                dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                                dr("KEY_VALUE") = ediCtlNo
                                dr("ROW_NO") = selectRow.ToString()
                                errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                                errHt.Add(i, String.Empty)

                                Continue For

                            ElseIf (String.IsNullOrEmpty(outkaPlanDate)) Then
                                ' 出荷予定日未設定

                                'エラーがある場合、DataTableに設定
                                dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                                dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                                dr("MESSAGE_ID") = "E340"
                                dr("PARA1") = "出荷予定日"
                                dr("PARA2") = ""
                                dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                                dr("KEY_VALUE") = ediCtlNo
                                dr("ROW_NO") = selectRow.ToString()
                                errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                                errHt.Add(i, String.Empty)

                                Continue For

                            ElseIf (DirectCast(Me.MyHandler, LMH030H) _
                                    .GetUnsoByWgtAndDestRowBp(destCd, outkaPlanDate, freeC05) Is Nothing) Then
                                ' 届先、出荷予定日をキーとして設定する運送会社が存在しない

                                'エラーがある場合、DataTableに設定
                                dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                                dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                                dr("MESSAGE_ID") = "E011"
                                dr("PARA1") = ""
                                dr("PARA2") = ""
                                dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                                dr("KEY_VALUE") = ediCtlNo
                                dr("ROW_NO") = selectRow.ToString()
                                errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                                errHt.Add(i, String.Empty)

                                Continue For

                            End If

                        End If
                        'DEL 2019/04/17 Start 依頼番号 : 003370   【LMS】【EDI】中部日合_出荷EDI画面で赤くなり出荷登録できないデータがある
                        ''日本合成運送データチェック
                        'If unsoDataExists = "1" _
                        'AndAlso ncgoOpeoutOnly = "0" Then   '(この行の条件を)Add 2018/10/31 要望番号002808(先方手配(引き取り)の場合は出荷データのみで輸送データが来ないためエラーにしない)
                        '    '出荷データに紐づく運送データが存在しない

                        '    'エラーがある場合、DataTableに設定
                        '    dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                        '    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                        '    dr("MESSAGE_ID") = "E079"
                        '    dr("PARA1") = "出荷データ"
                        '    dr("PARA2") = "紐づく運送データ"
                        '    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                        '    dr("KEY_VALUE") = ediCtlNo
                        '    dr("ROW_NO") = selectRow.ToString()
                        '    errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                        '    errHt.Add(i, String.Empty)
                        'End If
                        'DEL 2019/04/17 End 依頼番号 : 003370   【LMS】【EDI】中部日合_出荷EDI画面で赤くなり出荷登録できないデータがある

#End If

#If True Then 'ADD 2020/03/12 011441【LMS】ITWﾊﾟﾌｫｰﾏﾝｽ_出荷データに意図しない売り上げ先が設定されている
                        If (LMH030C.EDI_CUST_INDEX.ITW.Equals(custindex)) Then

                            If (String.IsNullOrEmpty(destCd)) Then
                                ' 届先CD未設定

                                'エラーがある場合、DataTableに設定
                                dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                                dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                                dr("MESSAGE_ID") = "E340"
                                dr("PARA1") = "届先"
                                dr("PARA2") = ""
                                dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                                dr("KEY_VALUE") = ediCtlNo
                                dr("ROW_NO") = selectRow.ToString()
                                errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                                errHt.Add(i, String.Empty)

                                Continue For
                            End If
                        End If

#End If
                    Case LMH030C.EventShubetsu.SAVEUNSO         '運送登録の場合

                        '運送登録対象チェック

                        If custUnsoF = "1" Then
                        Else
                            'エラーがある場合、DataTableに設定
                            dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                            dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                            dr("MESSAGE_ID") = "E320"
                            dr("PARA1") = "出荷データ"
                            dr("PARA2") = "運送登録"
                            dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                            dr("KEY_VALUE") = ediCtlNo
                            dr("ROW_NO") = selectRow.ToString()
                            errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                            'Me.ShowMessage("E320", New String() {"削除データもしくは保留データ", "出荷登録"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        If unsoKbn = "01" Then
                        Else
                            'エラーがある場合、DataTableに設定
                            dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                            dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                            dr("MESSAGE_ID") = "E320"
                            dr("PARA1") = "識別区分が出荷"
                            dr("PARA2") = "運送登録"
                            dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                            dr("KEY_VALUE") = ediCtlNo
                            dr("ROW_NO") = selectRow.ToString()
                            errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                            'Me.ShowMessage("E320", New String() {"削除データもしくは保留データ", "出荷登録"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        '要望番号:1243 terakawa 2012.07.05 Start
                        Select Case custindex
                            Case "62", "63" 'DIC春日部、DIC春日部顔料
                                'DIC春日部、DIC春日部顔料の場合、キャンセルデータのチェックを行わない
                            Case Else
                                If delKb = "2" Then
                                    'エラーがある場合、DataTableに設定
                                    dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                                    dr("MESSAGE_ID") = "E320"
                                    dr("PARA1") = "キャンセルデータ"
                                    dr("PARA2") = "運送登録"
                                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                                    dr("KEY_VALUE") = ediCtlNo
                                    dr("ROW_NO") = selectRow.ToString()
                                    errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                                    'Me.ShowMessage("E320", New String() {"削除データもしくはキャンセルデータ", "出荷登録"})    'エラーメッセージ
                                    errHt.Add(i, String.Empty)
                                    Continue For
                                End If
                        End Select
                        '要望番号:1243 terakawa 2012.07.05 End

                        If outF = "0" Then
                        Else
                            'エラーがある場合、DataTableに設定
                            dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                            dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                            dr("MESSAGE_ID") = "E320"
                            dr("PARA1") = "運送書込対象外データ"
                            dr("PARA2") = "運送登録"
                            dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                            dr("KEY_VALUE") = ediCtlNo
                            dr("ROW_NO") = selectRow.ToString()
                            errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                            'Me.ShowMessage("E320", New String() {"削除データもしくは保留データ", "出荷登録"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        '★★★



                End Select

                '2012.03.26 大阪対応END

            End With

        Next

        Return errHt

    End Function

#End Region

#End Region '入力チェック（出荷登録）

#Region "入力チェック（実績作成）"

#Region "単項目チェック"

    ''' <summary>
    ''' 実績作成イベントの入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsCreatejissekiSingleCheck() As Boolean

        With Me._Frm

            '【単項目チェック】

            '選択チェック
            If IsSelectDataChk() = False Then
                Return False
            End If

        End With

        '自営業所チェック
        Dim rtnMsg As String = String.Empty
        rtnMsg = "実績作成"

        If Me.IsNrsChk(rtnMsg) = False Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "関連チェック"

    ''' <summary>
    ''' 実績作成時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsCreatejissekiKanrenCheck(ByVal eventshubetsu As Integer, ByRef errDs As DataSet, _
                                               ByVal sysdate As String) As Hashtable '要望番号:1092 terakawa 2012.06.28

        'Return Me.ChkEdiData(eventshubetsu)

        Dim errHt As Hashtable = New Hashtable

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0

        Dim jissekiF As String = String.Empty
        Dim custJissekiF As String = String.Empty
        Dim outF As String = String.Empty
        Dim ediDelKb As String = String.Empty
        Dim outkaDelKb As String = String.Empty
        Dim stateKb As String = String.Empty
        Dim ediCtlNo As String = String.Empty
        '要望番号:1092 terakawa 2012.06.28 Start
        Dim outkaPlanDate As String = String.Empty
        '要望番号:1092 terakawa 2012.06.28 End

        '2011.11.11 センコー対応　修正START
        Dim unsoKbn As String = String.Empty
        Dim custUnsoF As String = String.Empty
        Dim tripNo As String = String.Empty
        '2011.11.11 センコー対応　修正END

        errDs = New LMH030DS()
        Dim dr As DataRow

        For i As Integer = 0 To max

            With Me._Frm.sprEdiList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                jissekiF = .Cells(selectRow, LMH030G.sprEdiListDef.JISSEKI_FLAG.ColNo).Value().ToString()
                custJissekiF = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_JISSEKI.ColNo).Value().ToString()
                outF = .Cells(selectRow, LMH030G.sprEdiListDef.OUT_FLAG.ColNo).Value().ToString()
                ediDelKb = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_DEL_KB.ColNo).Value().ToString()
                outkaDelKb = .Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_DEL_KB.ColNo).Value().ToString()
                stateKb = .Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_STATE_KB.ColNo).Value().ToString()
                ediCtlNo = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo).Value().ToString()
                '要望番号:1092 terakawa 2012.06.28 Start
                outkaPlanDate = .Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_PLAN_DATE.ColNo).Value().ToString()
                '要望番号:1092 terakawa 2012.06.28 End

                '2011.11.11 センコー対応　修正START
                tripNo = .Cells(selectRow, LMH030G.sprEdiListDef.TRIP_NO.ColNo).Value().ToString()
                custUnsoF = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_UNSOFLG.ColNo).Value().ToString()
                If String.IsNullOrEmpty(.Cells(selectRow, LMH030G.sprEdiListDef.FREE_C30.ColNo).Value().ToString()) = False Then
                    unsoKbn = (.Cells(selectRow, LMH030G.sprEdiListDef.FREE_C30.ColNo).Value().ToString()).Substring(0, 2)
                End If
                '2011.11.11 センコー対応　修正END

                If jissekiF = "0" Then

                Else
                    'EDI入荷(大)の実績書込Fが実績未でない場合エラー
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E336"
                    dr("PARA1") = "実績未データ"
                    dr("PARA2") = "実績作成"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                    'Me.ShowMessage("E336", New String() {"実績未データ", "実績作成"})    'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                Dim tempStateKb As Integer = 0
                If String.IsNullOrEmpty(stateKb) = False Then
                    tempStateKb = Convert.ToInt32(stateKb)
                End If

                '2011.10.07 START デュポンEDIデータ即実績作成対応
                If (custJissekiF = "1" OrElse custJissekiF = "2" OrElse custJissekiF = "3") AndAlso _
                   ediDelKb = "0" AndAlso outkaDelKb = "0" AndAlso tempStateKb >= 60 Then
                    '2011.10.07 END

                ElseIf custJissekiF = "2" AndAlso (ediDelKb = "1" OrElse outkaDelKb = "1") Then

                ElseIf custJissekiF = "4" AndAlso (ediDelKb = "0" AndAlso outkaDelKb = "0") Then

                ElseIf custJissekiF = "3" AndAlso ediDelKb = "0" AndAlso (String.IsNullOrEmpty(outkaDelKb) = True) Then
                    '2011.10.07 END

                    '2012.11.11 センコー対応　修正START
                ElseIf custUnsoF = "1" AndAlso unsoKbn = "01" AndAlso custJissekiF = "1" AndAlso _
                       String.IsNullOrEmpty(tripNo) = False Then
                    '2012.11.11 センコー対応　修正END

                Else
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E336"
                    dr("PARA1") = "実績未データ"
                    dr("PARA2") = "実績作成"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                    'Me.ShowMessage("E336", New String() {"実績未データ", "実績作成"})     'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                '要望番号:1092 terakawa 2012.06.28 Start
                Dim outkaPlanDateTime As DateTime = Convert.ToDateTime(String.Concat(outkaPlanDate, " 00:00:00"))

                Dim sysDateTime As DateTime = Convert.ToDateTime(String.Concat(sysdate.Substring(0, 4), "/", _
                                                                                    sysdate.Substring(4, 2), "/", _
                                                                                    sysdate.Substring(6, 2), " 00:00:00"))

                '出荷予定日がシステム日付より未来日の場合、エラー。
                If outkaPlanDateTime > sysDateTime Then
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E320"
                    dr("PARA1") = "出荷予定日が未来日"
                    dr("PARA2") = "実績作成"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                    errHt.Add(i, String.Empty)
                    Continue For
                End If
                '要望番号:1092 terakawa 2012.06.28 End

            End With

        Next

        Return errHt


    End Function

#End Region

#End Region '入力チェック（実績作成）

#Region "入力チェック（実行）"

#Region "単項目チェック(必須)"
    ''' <summary>
    ''' 実行イベントの入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function JikkouHissuCheck() As Boolean

        With Me._Frm

            '【単項目チェック】

            '******************** ヘッダ項目の入力チェック ********************

            '実行種別
            .cmbExe.ItemName() = "実行種別"
            .cmbExe.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbExe) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#End Region

    '要望番号1129 2012.06.11 修正START
#Region "単項目チェック"
    ''' <summary>
    ''' 実行イベントの入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function JikkouSingleCheck(ByVal eventShubetsu As Integer, Optional ByVal strRtn() As String = Nothing) As Boolean

        With Me._Frm

            '【単項目チェック】

            '******************** ヘッダ項目の入力チェック ********************

            '実行種別
            .cmbExe.ItemName() = "実行種別"
            .cmbExe.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbExe) = False Then
                Return False
            End If

            '************************************************************
            Select Case eventShubetsu

                Case LMH030C.EventShubetsu.SAKURA_TUIKAJIKKOU

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
                    .txtCustCD_L.IsHissuCheck() = True
                    .txtCustCD_L.IsForbiddenWordsCheck() = True
                    .txtCustCD_L.IsByteCheck() = 5
                    If MyBase.IsValidateCheck(.txtCustCD_L) = False Then

                        Return False
                    End If

                    '荷主コード(中)
                    .txtCustCD_M.ItemName() = "荷主コード(中)"
                    .txtCustCD_M.IsHissuCheck() = True
                    .txtCustCD_M.IsForbiddenWordsCheck() = True
                    .txtCustCD_M.IsByteCheck() = 2
                    If MyBase.IsValidateCheck(.txtCustCD_M) = False Then

                        Return False
                    End If

                    '追加実行を認める荷主でなければエラー
                    'ゴードー溶剤(荷主INDEX = '4')でなければエラー
                    If strRtn(1).Equals("4") = False Then
                        Me.ShowMessage("E375", New String() {"追加実行対象荷主でない", "追加実行処理"})   'エラーメッセージ
                        .txtCustCD_L.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                        .txtCustCD_L.Focus()
                        Return False

                    End If

                    ''サクラファインテック(荷主コード='00237')でなければエラー
                    'If .txtCustCD_L.TextValue.Equals("00237") = False OrElse .cmbEigyo.SelectedValue.Equals("40") = False Then
                    '    Me.ShowMessage("E373")   'エラーメッセージ
                    '    .txtCustCD_L.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    '    .txtCustCD_L.Focus()
                    '    Return False
                    'End If

                    'フルパスチェック
                    If String.IsNullOrEmpty(strRtn(0)) = True Then

                        Me.ShowMessage("E326", New String() {"ファイル格納先のパス", "EDI対象荷主マスタ"})   'エラーメッセージ
                        .txtCustCD_L.Focus()
                        Return False
                    End If

                    ''(2012.10.09) 要望番号1502 コメントSTART
                    ''既存ファイルが存在すると実行できない為
                    ''既存ファイルの存在チェック
                    'If String.IsNullOrEmpty(Dir(strRtn(0))) = False Then

                    '    Me.ShowMessage("E160", New String() {"指定ディレクトリフォルダ", "実行ファイル"})   'エラーメッセージ
                    '    .txtCustCD_L.Focus()
                    '    Return False
                    'End If
                    ''(2012.10.09) 要望番号1502 コメントEND

                Case Else

                    '選択チェック
                    If IsSelectDataOneChk() = False Then

                        Return False
                    End If

            End Select

        End With

        '自営業所チェック
        Dim rtnMsg As String = String.Empty
        rtnMsg = "実行処理"

        If Me.IsNrsChk(rtnMsg) = False Then
            Return False
        End If

        Return True

    End Function

#End Region
    '要望番号1129 2012.06.11 修正END

#Region "関連チェック"

    ''' <summary>
    ''' 実行時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function JikkouKanrenCheck(ByVal eventShubetsu As Integer) As Hashtable

        Dim errHt As Hashtable = New Hashtable
        '続行確認
        Dim rtn As MsgBoxResult

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0

        Dim delKb As String = String.Empty
        Dim jissekiF As String = String.Empty
        Dim outkaDelKb As String = String.Empty
        Dim matomeNo As String = String.Empty
        '2012.04.04 大阪対応START
        Dim outF As String = String.Empty
        Dim custUnsoF As String = String.Empty
        Dim unsoKbn As String = String.Empty
        Dim unsoNo As String = String.Empty
        Dim unsoDelKb As String = String.Empty
        '2012.04.04 大阪対応END

        For i As Integer = 0 To max

            With Me._Frm.sprEdiList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                delKb = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_DEL_KB.ColNo).Value().ToString()
                outkaDelKb = .Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_DEL_KB.ColNo).Value().ToString()
                jissekiF = .Cells(selectRow, LMH030G.sprEdiListDef.JISSEKI_FLAG.ColNo).Value().ToString()
                If String.IsNullOrEmpty(.Cells(selectRow, LMH030G.sprEdiListDef.MATOME_NO.ColNo).Value().ToString()) = False Then
                    matomeNo = .Cells(selectRow, LMH030G.sprEdiListDef.MATOME_NO.ColNo).Value().ToString().Substring(1, 8)
                End If

                '2012.04.04 大阪対応START
                outF = .Cells(selectRow, LMH030G.sprEdiListDef.OUT_FLAG.ColNo).Value().ToString()
                custUnsoF = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_UNSOFLG.ColNo).Value().ToString()
                If String.IsNullOrEmpty(.Cells(selectRow, LMH030G.sprEdiListDef.FREE_C30.ColNo).Value().ToString()) = False Then
                    unsoKbn = (.Cells(selectRow, LMH030G.sprEdiListDef.FREE_C30.ColNo).Value().ToString()).Substring(0, 2)
                End If
                If String.IsNullOrEmpty(.Cells(selectRow, LMH030G.sprEdiListDef.UNSO_NO_L.ColNo).Value().ToString()) = False Then
                    unsoNo = .Cells(selectRow, LMH030G.sprEdiListDef.UNSO_NO_L.ColNo).Value().ToString().Substring(1, 8)
                End If
                unsoDelKb = .Cells(selectRow, LMH030G.sprEdiListDef.UNSO_DEL_KB.ColNo).Value().ToString()
                '2012.04.04 大阪対応END

                '2017/05/29 日合　運送対応　START
                If String.IsNullOrEmpty(.Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INDEX.ColNo).Value().ToString()) = False And _
                     String.IsNullOrEmpty(.Cells(selectRow, LMH030G.sprEdiListDef.FREE_C30.ColNo).Value().ToString()) = False Then
                    Dim sEDI_CUST_INDEX As String = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_INDEX.ColNo).Value().ToString()
                    Dim sFREE_C30 As String = .Cells(selectRow, LMH030G.sprEdiListDef.FREE_C30.ColNo).Value().ToString()
                    If sEDI_CUST_INDEX.Equals("57") = True Then
                        If ("01").Equals(Mid(sFREE_C30.ToString, 1, 2)) = True Then
                            custUnsoF = "1"
                            unsoNo = Mid(.Cells(selectRow, LMH030G.sprEdiListDef.FREE_C30.ColNo).Value().ToString(), 5, 8)
                        End If
                    End If
                End If
                '2017/05/29 日合　運送対応　END

                Select Case eventShubetsu

                    'EDI取消⇒未登録
                    Case LMH030C.EventShubetsu.TORIKESI_MITOUROKU

                        If delKb = "1" Then

                        Else
                            'EDI入荷(大)が削除データ以外の場合エラー
                            Me.ShowMessage("E336", New String() {"削除データ", "EDI取消⇒未登録"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        '実績作成済⇒実績未
                    Case LMH030C.EventShubetsu.SAKUSEIZUMI_JISSEKIMI

                        If jissekiF = "1" Then

                        Else
                            'EDI入荷(大)の実績書込Fが実績作成でない場合エラー
                            Me.ShowMessage("E336", New String() {"実績作成済データ", "実績作成済⇒実績未"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        '「～⇒実績未」処理共通チェック
                        Dim msgParts As String() = Nothing
                        Dim msgCd As String = JikkouKanrenCheckJissekimi(selectRow, msgParts)
                        If msgCd <> "" Then
                            Me.ShowMessage(msgCd, msgParts)
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        '実績送信済⇒実績送信待
                    Case LMH030C.EventShubetsu.SOUSINZUMI_SOUSINMACHI


                        If jissekiF = "2" Then

                        Else
                            'EDI入荷(大)の実績書込Fが実績送信でない場合エラー
                            Me.ShowMessage("E336", New String() {"実績送信済データ", "実績送信済⇒実績送信待"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        '実績送信済⇒実績未
                    Case LMH030C.EventShubetsu.SOUSINZUMI_JISSEKIMI

                        If jissekiF = "2" Then

                        Else
                            'EDI入荷(大)の実績書込Fが実績送信でない場合エラー
                            Me.ShowMessage("E336", New String() {"実績送信済データ", "実績送信済⇒実績未"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        '「～⇒実績未」処理共通チェック
                        Dim msgParts As String() = Nothing
                        Dim msgCd As String = JikkouKanrenCheckJissekimi(selectRow, msgParts)
                        If msgCd <> "" Then
                            Me.ShowMessage(msgCd, msgParts)
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        '出荷取消⇒未登録
                    Case LMH030C.EventShubetsu.TOUROKUZUMI_MITOUROKU

                        If delKb = "0" Then

                        Else
                            'EDI出荷(大)が削除データの場合エラー
                            Me.ShowMessage("E320", New String() {"削除データ", "出荷取消⇒未登録"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        If outkaDelKb = "1" Then

                        Else
                            '出荷(大)が削除データ以外の場合エラー
                            Me.ShowMessage("E336", New String() {"出荷取消データ", "出荷取消⇒未登録"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        If String.IsNullOrEmpty(matomeNo) = False AndAlso matomeNo.Equals("0000000") = False Then

                            'まとめデータの場合はワーニング
                            rtn = Me.ShowMessage("W163", New String() {"出荷取消⇒未登録"})
                            If rtn = MsgBoxResult.Ok Then
                            ElseIf rtn = MsgBoxResult.Cancel Then
                                errHt.Add(i, String.Empty)
                                Continue For
                            End If


                        End If

                        '2012.04.04 大阪対応START
                        '運送取消⇒未登録
                    Case LMH030C.EventShubetsu.UNSOTORIKESI_MITOUROKU


                        If custUnsoF = "1" Then

                        Else
                            '運送のみデータ作成荷主ではない場合エラー
                            Me.ShowMessage("E336", New String() {"運送登録荷主", "運送取消⇒未登録"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        If delKb = "0" Then

                        Else
                            'EDI出荷(大)が削除データの場合エラー
                            Me.ShowMessage("E320", New String() {"削除データ", "運送取消⇒未登録"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        If unsoDelKb = "1" Then

                        Else
                            '運送(大)が削除データ以外の場合エラー
                            Me.ShowMessage("E336", New String() {"運送取消データ", "運送取消⇒未登録"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        If outF = "1" Then
                        Else
                            '運送が未登録扱いの場合エラー
                            Me.ShowMessage("E320", New String() {"運送書込対象データ", "運送取消⇒未登録"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        If unsoKbn = "01" Then

                        Else
                            '運送識別区分が"01"ではないデータの場合エラー
                            Me.ShowMessage("E336", New String() {"識別区分が運送(01)", "運送取消⇒未登録"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        If String.IsNullOrEmpty(unsoNo) = False AndAlso unsoNo.Equals("00000000") = False Then

                        Else
                            '運送番号が未設定の場合エラー
                            Me.ShowMessage("E320", New String() {"運送番号が未設定", "運送取消⇒未登録"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        '2012.04.04 大阪対応END

                End Select

            End With

        Next

        Return errHt

    End Function

    ''' <summary>
    ''' 「～⇒実績未」処理共通チェック
    ''' </summary>
    ''' <param name="selectRow"></param>
    ''' <param name="msgParts"></param>
    ''' <returns></returns>
    Private Function JikkouKanrenCheckJissekimi(ByVal selectRow As Integer, ByRef msgParts As String()) As String

        Dim msgCd As String = ""

        With Me._Frm.sprEdiList.ActiveSheet

            Dim nrsBrCd As String = .Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo).Value().ToString()
            Dim outkaNoL As String = .Cells(selectRow, LMH030G.sprEdiListDef.KANRI_NO.ColNo).Value().ToString()
            Dim ediCtlNo As String = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo).Value().ToString()
            ' Rapidus次回分納情報取得
            Dim ds As DataSet = Me._H.SelectJikaiBunnouInfo(nrsBrCd, outkaNoL, ediCtlNo)

            If ds.Tables("LMZ390OUT").Rows(0).Item("JIKAI_BUNNOU_UMU").ToString() = LMConst.FLG.OFF Then
                ' 次回分納なしならば以降は行わない。
                Return msgCd
            End If

            ' 次回分納あり
            Dim jikaiBunnouOutkaCtlNo As String = ds.Tables("LMZ390OUT").Rows(0).Item("JIKAI_BUNNOU_OUTKA_CTL_NO").ToString()
            Dim jikaiBunnouEdiCtlNo As String = ds.Tables("LMZ390OUT").Rows(0).Item("JIKAI_BUNNOU_EDI_CTL_NO").ToString()

            Dim jissekiFlag As String = ds.Tables("LMZ390OUT").Rows(0).Item("JIKAI_BUNNOU_JISSEKI_FLAG").ToString()
            Select Case jissekiFlag
                Case ""
                    ' 次回分納の出荷指示EDIデータは存在するが
                    ' 同時に登録されたはずの EDI出荷 が存在しない場合(想定外)
                    msgCd = "E454"
                    msgParts = New String() {
                        "次回分納の出荷指示EDIデータのみ存在するデータ不整合状態", "実行",
                        String.Concat("(EDI管理番号:", jikaiBunnouEdiCtlNo, " ", ")", " ", "システム管理者に連絡してください。")}

                Case "0"
                    ' 次回分納の EDI出荷L 実績処理フラグが“未出力”の場合
                    If jikaiBunnouOutkaCtlNo.PadRight(8, " "c).Substring(1, 8) = New String("0"c, 8) Then
                        ' 次回分納が出荷未登録の場合
                        msgCd = "E454"
                        msgParts = New String() {
                            "次回分納のEDI出荷が登録済み", "実行",
                            String.Concat("EDI管理番号:", jikaiBunnouEdiCtlNo, " ", "の EDI取消 後に 再度実行してください。")}
                    Else
                        ' 次回分納が出荷登録済の場合
                        If ds.Tables("LMZ390OUT").Rows(0).Item("JIKAI_BUNNOU_SYS_DEL_FLG").ToString() = LMConst.FLG.ON Then
                            ' 次回分納出荷が削除済の場合
                            msgCd = "E454"
                            msgParts = New String() {
                                "次回分納のEDI出荷が登録済み", "実行",
                                String.Concat("EDI管理番号: ", jikaiBunnouEdiCtlNo, " ", "の「出荷取消⇒未登録」および「EDI取消」後に 再度実行してください。")}
                        Else
                            ' 次回分納出荷が非削除の場合
                            msgCd = "E454"
                            msgParts = New String() {
                                "次回分納の出荷が登録済み", "実行",
                                String.Concat("出荷管理番号:", jikaiBunnouOutkaCtlNo, " ",
                                              "の出荷を、完了済みの場合は完了取消後に削除してから再度実行してください。")}
                        End If
                    End If

                Case "1"
                    ' 次回分納の EDI出荷L 実績処理フラグが“出力済”の場合
                    msgCd = "E454"
                    msgParts = New String() {
                        "次回分納の出荷が登録済み", "実行",
                        String.Concat("EDI管理番号: ", jikaiBunnouEdiCtlNo, " ", "の「実績作成済⇒実績未」を実行し、出荷の完了を取り消してから再度実行してください。")}

                Case "2"
                    ' 次回分納の EDI出荷L 実績処理フラグが“送信済”の場合
                    msgCd = "E454"
                    msgParts = New String() {
                        "次回分納の出荷が登録済み", "実行",
                        String.Concat("EDI管理番号: ", jikaiBunnouEdiCtlNo, " ", "の「実績送信済⇒実績未」を実行し、出荷の完了を取り消してから再度実行してください。")}

            End Select

        End With

        Return msgCd

    End Function

#End Region

#End Region '入力チェック（実行処理）

#Region "入力チェック（紐付け）"

#Region "単項目チェック"
    ''' <summary>
    ''' 紐付けイベントの入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsHimodukeSingleCheck() As Boolean

        With Me._Frm

            '【単項目チェック】
            '選択チェック
            If IsSelectDataOneChk() = False Then
                Return False
            End If


        End With

        '自営業所チェック
        Dim rtnMsg As String = String.Empty
        rtnMsg = "紐付け"

        If Me.IsNrsChk(rtnMsg) = False Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "関連チェック"

    ''' <summary>
    ''' 紐付け時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsHimodukeKanrenCheck(ByVal eventshubetsu As Integer) As Hashtable

        'EDIデータ対象チェック

        'Return Me.ChkEdiData(eventshubetsu)

        '続行確認
        Dim rtn As MsgBoxResult

        Dim errHt As Hashtable = New Hashtable

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0

        Dim delKb As String = String.Empty
        Dim outF As String = String.Empty
        Dim jissekiF As String = String.Empty
        Dim custDelDisp As String = String.Empty
        Dim mCount As String = String.Empty
        Dim custHoldF As String = String.Empty
        Dim ediCtlNo As String = String.Empty
        Dim akakuroF As String = String.Empty
        '2012.03.26 大阪対応修正START
        Dim custUnsoF As String = String.Empty
        '2012.03.26 大阪対応修正END

        For i As Integer = 0 To max

            With Me._Frm.sprEdiList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                delKb = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_DEL_KB.ColNo).Value().ToString()
                outF = .Cells(selectRow, LMH030G.sprEdiListDef.OUT_FLAG.ColNo).Value().ToString()
                jissekiF = .Cells(selectRow, LMH030G.sprEdiListDef.JISSEKI_FLAG.ColNo).Value().ToString()
                custDelDisp = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_DELDISP.ColNo).Value().ToString()
                mCount = .Cells(selectRow, LMH030G.sprEdiListDef.MDL_REC_CNT.ColNo).Value().ToString()
                custHoldF = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_HOLDOUT.ColNo).Value().ToString()
                ediCtlNo = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo).Value().ToString()
                akakuroF = .Cells(selectRow, LMH030G.sprEdiListDef.AKAKURO_FLG.ColNo).Value().ToString()
                '2012.03.26 大阪対応修正START
                custUnsoF = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_UNSOFLG.ColNo).Value().ToString()
                '2012.03.26 大阪対応修正END

                If delKb = "1" Then
                    'EDI入荷(大)が削除データの場合エラー
                    Me.ShowMessage("E320", New String() {"削除済データ", "紐付け処理"})     'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                '未登録データチェック
                '★★★
                'If ((delKb = "0" OrElse delKb = "3" OrElse delKb = "2") OrElse (delKb = "1" AndAlso custDelDisp = "1")) _
                '   AndAlso (outF = "0" OrElse outF = "2") Then
                If ((delKb = "0" OrElse delKb = "3" OrElse delKb = "2") OrElse (delKb = "1" AndAlso custDelDisp = "1")) _
                    AndAlso ((outF = "0" OrElse outF = "2") AndAlso (jissekiF = "0" OrElse jissekiF = "9")) Then
                    '★★★

                Else
                    Me.ShowMessage("E336", New String() {"未登録データ", "紐付け処理"})   'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For

                End If

                '2012.03.26 大阪対応修正START
                ''運送データの場合は紐付け処理は行えない
                If custUnsoF <> "1" Then
                Else

                    Me.ShowMessage("E320", New String() {"運送データ", "紐付け処理"})    'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For
                End If
                '2012.03.26 大阪対応修正END

                '★★★
                '出荷登録対象チェック
                If outF = "0" Then
                Else
                    Me.ShowMessage("E320", New String() {"出荷書込対象外データ", "紐付け処理"})    'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For
                End If
                '★★★

                If delKb = "3" AndAlso custHoldF = "1" Then
                    '保留データかつ保留データ登録フラグが"1"の場合はワーニング
                    rtn = Me.ShowMessage("W169", New String() {"紐付け処理", ediCtlNo})
                    If rtn = MsgBoxResult.Ok Then
                    ElseIf rtn = MsgBoxResult.Cancel Then
                        errHt.Add(i, String.Empty)
                        Continue For
                    End If
                    '2012.06.07 修正START
                ElseIf delKb = "3" AndAlso custHoldF = "2" Then
                    '2012.06.07 修正END
                    Me.ShowMessage("E376", New String() {"保留データ", "紐付け処理", "EDI取消"})    'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For

                ElseIf delKb = "2" Then
                    Me.ShowMessage("E320", New String() {"キャンセルデータ", "紐付け処理"})    'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For

                ElseIf delKb = "0" Then

                End If

                'EDI出荷(中)の件数が 0の場合エラー
                If mCount = "0" Then
                    Me.ShowMessage("E320", New String() {"中レコード数が 0件", "紐付け処理"})    'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                'EDI出荷(中)に赤データがある場合エラー
                If akakuroF = "1" Then
                    Me.ShowMessage("E320", New String() {"中データが赤データ", "紐付け処理"})   'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

            End With

        Next

        Return errHt

    End Function

#End Region

#End Region '入力チェック（紐付け）

#Region "入力チェック（EDI取消）"

#Region "単項目チェック"

    ''' <summary>
    ''' EDI取消イベントの入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsEditorikesiSingleCheck() As Boolean

        With Me._Frm

            '【単項目チェック】

            '選択チェック
            If IsSelectDataChk() = False Then
                Return False
            End If

        End With

        '自営業所チェック
        Dim rtnMsg As String = String.Empty
        rtnMsg = "EDI取消"

        If Me.IsNrsChk(rtnMsg) = False Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "関連チェック"

    ''' <summary>
    ''' EDI取消時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsEditorikesiKanrenCheck(ByVal eventshubetsu As Integer, ByRef errDs As DataSet) As Hashtable

        'EDIデータ対象チェック

        'Return Me.ChkEdiData(eventshubetsu)

        Dim errHt As Hashtable = New Hashtable

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0

        Dim delKb As String = String.Empty
        Dim outF As String = String.Empty
        Dim jissekiF As String = String.Empty
        Dim custDelDisp As String = String.Empty
        Dim ediCtlNo As String = String.Empty

        errDs = New LMH030DS()
        Dim dr As DataRow

        For i As Integer = 0 To max

            With Me._Frm.sprEdiList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                delKb = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_DEL_KB.ColNo).Value().ToString()
                outF = .Cells(selectRow, LMH030G.sprEdiListDef.OUT_FLAG.ColNo).Value().ToString()
                jissekiF = .Cells(selectRow, LMH030G.sprEdiListDef.JISSEKI_FLAG.ColNo).Value().ToString()
                custDelDisp = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_DELDISP.ColNo).Value().ToString()
                ediCtlNo = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo).Value().ToString()

                If delKb = "1" Then
                    'EDI入荷(大)が削除データの場合エラー
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E320"
                    dr("PARA1") = "削除済データ"
                    dr("PARA2") = "EDI取消"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                    'Me.ShowMessage("E320", New String() {"削除済データ", "EDI取消"})     'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                '未登録データチェック
                '★★★
                'If ((delKb = "0" OrElse delKb = "3" OrElse delKb = "2") OrElse (delKb = "1" AndAlso custDelDisp = "1")) _
                '   AndAlso (outF = "0" OrElse outF = "2") Then
                If ((delKb = "0" OrElse delKb = "3" OrElse delKb = "2") OrElse (delKb = "1" AndAlso custDelDisp = "1")) _
                    AndAlso ((outF = "0" OrElse outF = "2") AndAlso (jissekiF = "0" OrElse jissekiF = "9")) Then
                    '★★★

                Else
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E336"
                    dr("PARA1") = "未登録データ"
                    dr("PARA2") = "EDI取消"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                    'Me.ShowMessage("E336", New String() {"未登録データ", "EDI取消"})   'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For

                End If

            End With

        Next

        Return errHt

    End Function

#End Region

#End Region '入力チェック（EDI取消）

#Region "入力チェック（実績取消）"

#Region "単項目チェック"

    ''' <summary>
    ''' 実績取消イベントの入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsJissekiSakuseiSingleCheck() As Boolean

        With Me._Frm

            '【単項目チェック】

            '選択チェック
            If IsSelectDataChk() = False Then
                Return False
            End If

        End With

        '自営業所チェック
        Dim rtnMsg As String = String.Empty
        rtnMsg = "実績取消"

        If Me.IsNrsChk(rtnMsg) = False Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "関連チェック"

    ''' <summary>
    ''' 実績取消時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsJissekiSakuseiKanrenCheck(ByVal eventshubetsu As Integer, ByRef errDs As DataSet) As Hashtable

        ''EDIデータ対象チェック
        'Return Me.ChkEdiData(eventshubetsu)

        Dim errHt As Hashtable = New Hashtable

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0

        Dim jissekiF As String = String.Empty
        Dim custJissekiF As String = String.Empty
        Dim outF As String = String.Empty
        Dim ediDelKb As String = String.Empty
        Dim outkaDelKb As String = String.Empty
        Dim stateKb As String = String.Empty
        Dim ediCtlNo As String = String.Empty

        errDs = New LMH030DS()
        Dim dr As DataRow

        For i As Integer = 0 To max

            With Me._Frm.sprEdiList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                jissekiF = .Cells(selectRow, LMH030G.sprEdiListDef.JISSEKI_FLAG.ColNo).Value().ToString()
                custJissekiF = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_JISSEKI.ColNo).Value().ToString()
                outF = .Cells(selectRow, LMH030G.sprEdiListDef.OUT_FLAG.ColNo).Value().ToString()
                ediDelKb = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_DEL_KB.ColNo).Value().ToString()
                outkaDelKb = .Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_DEL_KB.ColNo).Value().ToString()
                stateKb = .Cells(selectRow, LMH030G.sprEdiListDef.OUTKA_STATE_KB.ColNo).Value().ToString()
                ediCtlNo = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo).Value().ToString()

                If jissekiF = "0" Then

                Else
                    'EDI入荷(大)の実績書込Fが実績未でない場合エラー
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E336"
                    dr("PARA1") = "実績未データ"
                    dr("PARA2") = "実績取消"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                    'Me.ShowMessage("E336", New String() {"実績未データ", "実績取消"})     'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                Dim tempStateKb As Integer = 0
                If String.IsNullOrEmpty(stateKb) = False Then
                    tempStateKb = Convert.ToInt32(stateKb)
                End If

                '2011.10.07 START デュポンEDIデータ即実績作成対応
                If (custJissekiF = "1" OrElse custJissekiF = "2" OrElse custJissekiF = "3") AndAlso _
                   ediDelKb = "0" AndAlso outkaDelKb = "0" AndAlso tempStateKb >= 60 Then
                    '2011.10.07 END

                ElseIf custJissekiF = "2" AndAlso (ediDelKb = "1" OrElse outkaDelKb = "1") Then

                ElseIf custJissekiF = "4" AndAlso (ediDelKb = "0" AndAlso outkaDelKb = "0") Then

                ElseIf custJissekiF = "3" AndAlso ediDelKb = "0" AndAlso (String.IsNullOrEmpty(outkaDelKb) = True) Then
                    '2011.10.07 END

                    'If (custJissekiF = "1" OrElse custJissekiF = "2") AndAlso _
                    '   ediDelKb = "0" AndAlso outkaDelKb = "0" AndAlso tempStateKb >= 60 Then

                    'ElseIf custJissekiF = "2" AndAlso (ediDelKb = "1" OrElse outkaDelKb = "1") Then

                    'ElseIf custJissekiF = "4" AndAlso (ediDelKb = "0" AndAlso outkaDelKb = "0") Then

                Else
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E336"
                    dr("PARA1") = "実績未データ"
                    dr("PARA2") = "実績取消"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                    'Me.ShowMessage("E336", New String() {"実績未データ", "実績取消"})   'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

            End With

        Next

        Return errHt

    End Function

#End Region

#End Region '入力チェック（実績取消）

#Region "入力チェック（一括変更）"

#Region "単項目チェック"

    ''' <summary>
    ''' 一括変更イベントの入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsIkkatuhenkoSingleCheck(ByVal sysdate As String) As Boolean

        Dim henkoKbn As String = String.Empty

        With Me._Frm

            '******************** ヘッダ項目の入力チェック ********************

            '一括変更種別
            .cmbIkkatuChangeKbn.ItemName() = "一括変更種別"
            .cmbIkkatuChangeKbn.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbIkkatuChangeKbn) = False Then
                Return False
            End If

            '************************************************************

            '【単項目チェック】

            '選択チェック
            If IsSelectDataChk() = False Then
                Return False
            End If

            henkoKbn = .cmbIkkatuChangeKbn.SelectedValue.ToString()

            Select Case henkoKbn

                Case "01"
                    '便区分
                    .cmbEditKbn.ItemName() = "便区分"
                    .cmbEditKbn.IsHissuCheck() = True
                    If MyBase.IsValidateCheck(.cmbEditKbn) = False Then
                        Return False
                    End If

                Case "02"

                    '運送会社コード
                    .txtEditMain.ItemName() = "運送会社コード"
                    'ADD START 2018/11/14 要望管理002327 支店コード入力ありの場合のみ会社コード必須
                    If String.IsNullOrEmpty(.txtEditSub.TextValue) Then
                        .txtEditMain.IsHissuCheck() = False
                    Else
                        'ADD END 2018/11/14 要望管理002327
                        .txtEditMain.IsHissuCheck() = True
                    End If  'ADD 2018/11/14 要望管理002327
                    .txtEditMain.IsForbiddenWordsCheck() = True
                    .txtEditMain.IsByteCheck() = 5
                    If MyBase.IsValidateCheck(.txtEditMain) = False Then
                        Return False
                    End If

                    '運送会社支店コード
                    .txtEditSub.ItemName() = "運送会社支店コード"
                    'ADD START 2018/11/14 要望管理002327 会社コード入力ありの場合のみ支店コード必須
                    If String.IsNullOrEmpty(.txtEditMain.TextValue) Then
                        .txtEditSub.IsHissuCheck() = False
                    Else
                        'ADD END 2018/11/14 要望管理002327
                        .txtEditSub.IsHissuCheck() = True
                    End If  'ADD 2018/11/14 要望管理002327
                    .txtEditSub.IsForbiddenWordsCheck() = True
                    .txtEditSub.IsByteCheck() = 3
                    If MyBase.IsValidateCheck(.txtEditSub) = False Then
                        Return False
                    End If

                    'ADD START 2018/11/14 要望管理002327
                    If String.IsNullOrEmpty(.txtEditMain.TextValue) AndAlso String.IsNullOrEmpty(.txtEditSub.TextValue) Then
                        '会社コード・支店コードともに入力なしの場合、続行確認   
                        Dim rtn As MsgBoxResult
                        rtn = MyBase.ShowMessage("C001", New String() {"運送会社コードをブランクに戻"})    'C001:[%1]しますがよろしいですか？
                        If rtn = MsgBoxResult.Cancel Then
                            Return False
                        End If
                    End If
                    'ADD END   2018/11/14 要望管理002327

                Case "03", "04", "05"

                    Select Case henkoKbn
                        Case "03"
                            '出庫日
                            .cmbEditDate.ItemName() = "出庫日"
                        Case "04"
                            '出荷予定日
                            .cmbEditDate.ItemName() = "出荷予定日"
                        Case "05"
                            '納入予定日
                            .cmbEditDate.ItemName() = "納入予定日"

                    End Select
                    .cmbEditDate.IsHissuCheck() = True
                    If MyBase.IsValidateCheck(.cmbEditDate) = False Then
                        Return False
                    End If

                    '日付項目(出庫日,出荷予定日,納入予定日)

                    If .cmbEditDate.IsDateFullByteCheck(8) = False Then
                        Select Case henkoKbn
                            Case "03"
                                MyBase.ShowMessage("E038", New String() {"出庫日", "8"})

                            Case "04"
                                MyBase.ShowMessage("E038", New String() {"出荷予定日", "8"})

                            Case "05"
                                MyBase.ShowMessage("E038", New String() {"納入予定日", "8"})

                        End Select

                        Return False
                    End If


                    '範囲チェック(日付項目がシステム日付より14日より大きかったらエラー)

                    Dim fixedDate As Integer = 0
                    Dim editDate As String = String.Empty
                    editDate = .cmbEditDate.TextValue
                    fixedDate = 14
                    '続行確認
                    Dim rtn As MsgBoxResult
                    Dim editDateTime As DateTime = Convert.ToDateTime(String.Concat(editDate.Substring(0, 4), "/", _
                                                                                    editDate.Substring(4, 2), "/", _
                                                                                    editDate.Substring(6, 2), " 00:00:00"))
                    Dim sysDateTime As DateTime = Convert.ToDateTime(String.Concat(sysdate.Substring(0, 4), "/", _
                                                                                    sysdate.Substring(4, 2), "/", _
                                                                                    sysdate.Substring(6, 2), " 00:00:00"))

                    'BP・カストロール対応 terakawa 2012.12.26 Start
                    '入力日付が翌営業日を超えていた場合、ワーニング(BPカストロールのみ)
                    Dim skipFlg As Boolean = False
                    Dim ediCustIndex As String = Me._Frm.sprEdiList.ActiveSheet.Cells(Convert.ToInt32(Me._ChkList(0)), LMH030G.sprEdiListDef.EDI_CUST_INDEX.ColNo).Value().ToString()
                    If ediCustIndex.Equals(LMH030C.EDI_CUST_INDEX.BP) = True Then
                        Dim nextBussinessDateTime As DateTime = Me.GetBussinessDay(sysdate, +1)
                        If nextBussinessDateTime < editDateTime Then

                            Select Case henkoKbn
                                Case "03"
                                    rtn = MyBase.ShowMessage("W227", New String() {"出庫日"})

                                Case "04"
                                    rtn = MyBase.ShowMessage("W227", New String() {"出荷予定日"})

                                Case "05"
                                    rtn = MyBase.ShowMessage("W227", New String() {"納入予定日"})

                            End Select

                            If rtn = MsgBoxResult.Ok Then
                                '翌営業日のチェックを行った場合、日付が当日から2週間以上かのチェックをスキップする
                                skipFlg = True

                            ElseIf rtn = MsgBoxResult.Cancel Then
                                MyBase.ShowMessage("G007")
                                .cmbEditDate.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                                .cmbEditDate.Focus()
                                Return False

                            End If

                        End If

                        'SHINODA　過去日入力チェック対応 START
                        If sysDateTime > editDateTime Then

                            Select Case henkoKbn
                                Case "03"
                                    MyBase.ShowMessage("E039", New String() {"出庫日", "本日"})

                                Case "04"
                                    MyBase.ShowMessage("E039", New String() {"出荷予定日", "本日"})

                                Case "05"
                                    MyBase.ShowMessage("E039", New String() {"納入予定日", "本日"})

                            End Select

                            .cmbEditDate.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                            .cmbEditDate.Focus()
                            Return False

                        End If
                        'SHINODA　過去日入力チェック対応 END
                    End If

                    If skipFlg = False Then
                        'BP・カストロール対応 terakawa 2012.12.26 End


                        'If fixedDate < System.Math.Abs(Convert.ToInt32(editDate) - Convert.ToInt32(sysdate)) Then
                        If fixedDate < System.Math.Abs(DateDiff(DateInterval.Day, editDateTime, sysDateTime)) Then

                            Select Case henkoKbn
                                Case "03"
                                    rtn = MyBase.ShowMessage("W161", New String() {"出庫日"})

                                Case "04"
                                    rtn = MyBase.ShowMessage("W161", New String() {"出荷予定日"})

                                Case "05"
                                    rtn = MyBase.ShowMessage("W161", New String() {"納入予定日"})

                            End Select

                            If rtn = MsgBoxResult.Ok Then

                            ElseIf rtn = MsgBoxResult.Cancel Then
                                MyBase.ShowMessage("G007")
                                .cmbEditDate.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                                .cmbEditDate.Focus()
                                Return False

                            End If

                        End If
                        'BP・カストロール対応 terakawa 2012.12.26 Start
                    End If

                    '入力日付が当日でなかった場合、ワーニング
                    If henkoKbn.Equals("05") = True Then
                        '納入日の場合は、チェックを行わない
                    Else
                        If sysdate.Equals(editDate) = False Then

                            Select Case henkoKbn
                                Case "03"
                                    rtn = MyBase.ShowMessage("W226", New String() {"出庫日"})

                                Case "04"
                                    rtn = MyBase.ShowMessage("W226", New String() {"出荷予定日"})

                            End Select

                            If rtn = MsgBoxResult.Ok Then

                            ElseIf rtn = MsgBoxResult.Cancel Then
                                MyBase.ShowMessage("G007")
                                .cmbEditDate.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                                .cmbEditDate.Focus()
                                Return False

                            End If
                        End If
                    End If
                    'BP・カストロール対応 terakawa 2012.12.26 End

                Case "06"       'ADD 2018/02/22 

                    '届先コード
                    .txtEditDestCD.ItemName() = "届先コード"
                    .txtEditDestCD.IsHissuCheck() = True
                    .txtEditDestCD.IsForbiddenWordsCheck() = True
                    .txtEditDestCD.IsByteCheck() = 15
                    If MyBase.IsValidateCheck(.txtEditDestCD) = False Then
                        Return False
                    End If

                Case "07"
                    'ピック区分
                    .cmbEditKbn2.ItemName() = "ピック区分"
                    .cmbEditKbn2.IsHissuCheck() = True
                    If MyBase.IsValidateCheck(.cmbEditKbn2) = False Then
                        Return False
                    End If

            End Select

        End With

        '自営業所チェック
        Dim rtnMsg As String = String.Empty
        rtnMsg = "一括変更"

        If Me.IsNrsChk(rtnMsg) = False Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "関連チェック"

    ''' <summary>
    ''' 一括変更時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsIkkatuhenkoKanrenCheck(ByVal eventshubetsu As Integer, ByRef errDs As DataSet) As Hashtable

        ''EDIデータ対象チェック
        'Return Me.ChkEdiData(eventshubetsu)

        Dim errHt As Hashtable = New Hashtable

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0

        Dim delKb As String = String.Empty
        Dim outF As String = String.Empty
        Dim custDelDisp As String = String.Empty
        Dim ediCtlNo As String = String.Empty
        '★★★
        Dim jissekiF As String = String.Empty
        '★★★

        errDs = New LMH030DS()
        Dim dr As DataRow

        For i As Integer = 0 To max

            With Me._Frm.sprEdiList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                delKb = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_DEL_KB.ColNo).Value().ToString()
                outF = .Cells(selectRow, LMH030G.sprEdiListDef.OUT_FLAG.ColNo).Value().ToString()
                custDelDisp = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_CUST_DELDISP.ColNo).Value().ToString()
                ediCtlNo = .Cells(selectRow, LMH030G.sprEdiListDef.EDI_NO.ColNo).Value().ToString()
                '★★★
                jissekiF = .Cells(selectRow, LMH030G.sprEdiListDef.JISSEKI_FLAG.ColNo).Value().ToString()
                '★★★

                '未登録データチェック
                '★★★
                'If ((delKb = "0" OrElse delKb = "3" OrElse delKb = "2") OrElse (delKb = "1" AndAlso custDelDisp = "1")) _
                '   AndAlso (outF = "0" OrElse outF = "2") Then
                If ((delKb = "0" OrElse delKb = "3" OrElse delKb = "2") OrElse (delKb = "1" AndAlso custDelDisp = "1")) _
                    AndAlso ((outF = "0" OrElse outF = "2") AndAlso (jissekiF = "0" OrElse jissekiF = "9")) Then
                    '★★★


                Else
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH030C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E336"
                    dr("PARA1") = "未登録データ"
                    dr("PARA2") = "一括変更"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH030C.TABLE_NM_GUIERROR).Rows.Add(dr)
                    'Me.ShowMessage("E336", New String() {"未登録データ", "一括変更"})   'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For

                End If

            End With

        Next

        Return errHt

    End Function

#End Region

#End Region '入力チェック（一括変更）

#Region "入力チェック（マスタ参照）"

#Region "単項目チェック"

    ''' <summary>
    ''' マスタ参照時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsRefMstInputCheck() As Boolean

        With Me._Frm

            '【単項目チェック】

            '営業所
            .cmbEigyo.ItemName() = "営業所"
            .cmbEigyo.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                Return False
            End If

            '荷主コード(大)
            .txtCustCD_L.ItemName() = "荷主コード(大)"
            .txtCustCD_L.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtCustCD_L) = False Then
                Return False
            End If

            '荷主コード(中)
            .txtCustCD_M.ItemName() = "荷主コード(中)"
            .txtCustCD_M.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtCustCD_M) = False Then
                Return False
            End If

            '届先コード
            .txtTodokesakiCd.ItemName() = "届先コード"
            .txtTodokesakiCd.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtTodokesakiCd) = False Then
                Return False
            End If

            '運送会社コード
            .txtEditMain.ItemName() = "運送会社コード"
            .txtEditMain.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtEditMain) = False Then
                Return False
            End If

            '運送会社支店コード
            .txtEditSub.ItemName() = "運送会社支店コード"
            .txtEditSub.IsForbiddenWordsCheck() = True
            If MyBase.IsValidateCheck(.txtEditSub) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#End Region

#End Region '入力チェック（マスタ参照）

    '取込対応 20120305 Start
#Region "入力チェック（取込処理）"

#Region "単項目チェック"

    ''' <summary>
    ''' 取込処理イベントの入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsTorikomiSingleCheck() As Boolean

        With Me._Frm

            '【単項目チェック】

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
            .txtCustCD_L.IsHissuCheck() = True
            .txtCustCD_L.IsForbiddenWordsCheck() = True
            .txtCustCD_L.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCD_L) = False Then
                Return False
            End If

            '荷主コード(中)
            .txtCustCD_M.ItemName() = "荷主コード(中)"
            .txtCustCD_M.IsHissuCheck() = True
            .txtCustCD_M.IsForbiddenWordsCheck() = True
            .txtCustCD_M.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCD_M) = False Then
                Return False
            End If


        End With

        '自営業所チェック
        Dim rtnMsg As String = String.Empty
        rtnMsg = "取込処理"

        If Me.IsNrsChk(rtnMsg) = False Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "関連チェック"

    ''' <summary>
    ''' 取込処理時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsTorikomiKanrenCheck(ByVal rtDs As DataSet) As Boolean

        Dim dr As DataRow = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)

        Dim Rcv_Input_Dir As String = dr.Item("RCV_INPUT_DIR").ToString()
        Dim Work_Input_Dir As String = dr.Item("WORK_INPUT_DIR").ToString()
        Dim Backup_Input_Dir As String = dr.Item("BACKUP_INPUT_DIR").ToString()
        Dim Rcv_File_Nm As String = dr.Item("RCV_FILE_NM").ToString()
        Dim Rcv_File_Extention As String = dr.Item("RCV_FILE_EXTENTION").ToString()
        Dim Plural_File_Flag As String = dr.Item("PLURAL_FILE_FLAG").ToString()
        Dim Rcv_File_Count As Integer = 0
        Dim Work_File_Count As Integer = 0
        Dim Rcv_Extention_File_Flag As Boolean = False

        '②フォルダチェック
        '既存ファイルの存在チェック
        '②-1 受信格納フォルダパス
        '②-2 作業フォルダパス
        '②-3 BACKUPフォルダパス
        If System.IO.Directory.Exists(Rcv_Input_Dir) = False OrElse _
           System.IO.Directory.Exists(Work_Input_Dir) = False OrElse _
           System.IO.Directory.Exists(Backup_Input_Dir) = False Then

            'メッセージは入力チェック仕様を参照する
            Me.ShowMessage("E079", New String() {"設定されているパス", "フォルダ"})   'エラーメッセージ
            Return False
        End If


        '③フォルダ空チェック
        ' ワークファイルディレクトリ内のファイルを取得
        For Each stFilePath As String In System.IO.Directory.GetFiles(Work_Input_Dir, "*")
            Work_File_Count += 1
        Next stFilePath

        If Work_File_Count > 0 Then
            Me.ShowMessage("E160", New String() {"作業フォルダ", "作業ファイル"})   'エラーメッセージ
            Return False
        End If


        '④受信フォルダ存在チェック（受信ファイル名がセットされている場合）
        If String.IsNullOrEmpty(Rcv_File_Nm) = False Then
            If System.IO.File.Exists(String.Concat(Rcv_Input_Dir, Rcv_File_Nm)) = False Then
                Me.ShowMessage("E079", New String() {"受信格納フォルダ", "受信ファイル"})   'エラーメッセージ
                Return False
            End If
        Else
            '⑤受信フォルダ存在チェック（受信ファイル名がセットされていない場合）
            Dim extention As String = String.Empty

            For Each stFilePath As String In System.IO.Directory.GetFiles(Rcv_Input_Dir, String.Concat("*.", Rcv_File_Extention))
                extention = System.IO.Path.GetExtension(stFilePath)
                If String.Compare(extention, String.Concat(".", Rcv_File_Extention), True) = 0 Then
                    Rcv_Extention_File_Flag = True
                    Exit For
                End If
            Next stFilePath

            If Rcv_Extention_File_Flag = False Then
                Me.ShowMessage("E079", New String() {"受信格納フォルダ", "受信ファイル"})   'エラーメッセージ
                Return False
            End If

        End If

#If False Then      'UPD 2019/02/14 頼番号 : 003007   【LMS】セミEDI_大日本住友製薬ファイル取込時にThumb.dbの影響で取り込めない障害
                '受信ファイル数を取得
        For Each stFilePath As String In System.IO.Directory.GetFiles(Rcv_Input_Dir, "*")
            Rcv_File_Count += 1

            '⑧受信ファイル可否チェック
            On Error Resume Next
            FileOpen(Rcv_File_Count, stFilePath, OpenMode.Binary, OpenAccess.ReadWrite)
            FileClose(Rcv_File_Count)
            If Err.Number > 0 Then
                Me.ShowMessage("E160", New String() {"受信格納フォルダ", "受信中のファイル"})
                Return False
            End If

        Next stFilePath

#Else
        Dim sFileGet As String = "*"
        If rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("EDI_CUST_INDEX").ToString = "82" _
            Or rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("EDI_CUST_INDEX").ToString = "85" Then
            '大日本住友製薬
            sFileGet = String.Concat("*.", Rcv_File_Extention)

        End If

        '受信ファイル数を取得
        For Each stFilePath As String In System.IO.Directory.GetFiles(Rcv_Input_Dir, sFileGet)
            Rcv_File_Count += 1

            '⑧受信ファイル可否チェック
            On Error Resume Next
            FileOpen(Rcv_File_Count, stFilePath, OpenMode.Binary, OpenAccess.ReadWrite)
            FileClose(Rcv_File_Count)
            If Err.Number > 0 Then
                Me.ShowMessage("E160", New String() {"受信格納フォルダ", "受信中のファイル"})
                Return False
            End If

        Next stFilePath

#End If

        '⑥受信ファイル数チェック
        '受信ファイル名がセットされていない　かつ　複数ファイル許可フラグが"0" かつ　複数件ファイルが存在した場合エラー
        If String.IsNullOrEmpty(Rcv_File_Nm) = True AndAlso Plural_File_Flag = "0" AndAlso Rcv_File_Count > 1 Then
            Me.ShowMessage("E160", New String() {"受信格納フォルダ", "複数の受信ファイル"})  'エラーメッセージ
            Return False
        End If

        '⑦受信ファイル閾値チェック
        '受信ファイルが999件を超える場合エラー
        If Rcv_File_Count > 999 Then
            Me.ShowMessage("E117", New String() {"取込処理", "999件"})   'エラーメッセージ
            Return False
        End If

        '⑧受信ファイル名チェック（ＪＴ物流の場合のみ）
        If rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("EDI_CUST_INDEX").ToString = "70" Then
            For Each sFilePath As String In System.IO.Directory.GetFiles(Rcv_Input_Dir, String.Concat("*.", Rcv_File_Extention))
                Dim sFileName As String = System.IO.Path.GetFileName(sFilePath)
                '受信ファイル名のMID(11,8)が日付でない場合エラー
                '要望番号1593:(【セミEDI】JT物流　取込ファイル名変更) 2012/11/14 本明 Start
                'If IsNumeric(Mid(sFileName, 11, 8)) Then
                '    If IsDate(Convert.ToInt32(Mid(sFileName, 11, 8)).ToString("0000/00/00")) = False Then
                If IsNumeric(Mid(sFileName, 1, 8)) Then
                    If IsDate(Convert.ToInt32(Mid(sFileName, 1, 8)).ToString("0000/00/00")) = False Then
                        '要望番号1593:(【セミEDI】JT物流　取込ファイル名変更) 2012/11/14 本明 Start
                        '日付でない場合
                        Me.ShowMessage("E445", New String() {"取込ファイル名(" & sFileName & ")"})   'エラーメッセージ
                        Return False
                    End If
                Else
                    '数値でない場合
                    Me.ShowMessage("E445", New String() {"取込ファイル名(" & sFileName & ")"})   'エラーメッセージ
                    Return False
                End If
            Next
        End If


        '要望番号1982（ロンザ　セミEDI時のチェック追加）対応 2013/04/02 本明 Start
        '⑨エクセルのセル結合チェック（ロンザの場合のみ）
        If rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0).Item("EDI_CUST_INDEX").ToString = "84" Then

            Dim drSemiEdiInfo As DataRow = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)

            Dim folderNm As String = drSemiEdiInfo.Item("WORK_INPUT_DIR").ToString                      'フォルダ名
            Dim rowNoMin As Integer = Convert.ToInt32(drSemiEdiInfo.Item("TOP_ROW_CNT").ToString) + 1   '行の開始数
            Dim colNoMin As Integer = 1                                                                 '列の開始数
            Dim colNoMax As Integer = Convert.ToInt32(drSemiEdiInfo.Item("RCV_FILE_COL_CNT").ToString)  '列の最大数

            Dim fileNm As String = String.Empty

            '-----------------------------------------------------------------------------------------------
            ' EXCELファイル用
            '-----------------------------------------------------------------------------------------------
            Dim xlApp As Excel.Application = Nothing
            Dim xlBook As Excel.Workbook = Nothing
            Dim xlBooks As Excel.Workbooks = Nothing
            Dim xlSheet As Excel.Worksheet = Nothing
            Dim xlCell As Excel.Range = Nothing

            xlApp = New Excel.Application()
            xlBooks = xlApp.Workbooks

            For Each sFilePath As String In System.IO.Directory.GetFiles(Rcv_Input_Dir, String.Concat("*.", Rcv_File_Extention))

                Dim sFileName As String = System.IO.Path.GetFileName(sFilePath)


                ' EXCEL OPEN
                'fileNm = dtHed.Rows(i).Item("FILE_NAME_OPE").ToString
                xlBook = xlBooks.Open(sFilePath)

                xlSheet = DirectCast(xlBook.Worksheets(1), Excel.Worksheet)                 'とりあえず１番目のシートを設定
                xlApp.Visible = False

                '最大行を取得(rowNoKey列の最終入力行を取得)
                Dim rowNoMax As Integer = 0

                xlSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Select()

                rowNoMax = xlApp.ActiveCell.Row

                'セルが結合されている場合はエラーとする
                For row As Integer = rowNoMin To rowNoMax
                    For col As Integer = colNoMin To colNoMax
                        xlCell = DirectCast(xlSheet.Cells(row, col), Excel.Range)
                        If CBool(xlCell.MergeCells) = True Then
                            xlBooks.Close()

                            Me.ShowMessage("E542", New String() {"取込ファイル名(" & sFileName & ")"})   'エラーメッセージ
                            Return False

                        End If
                    Next
                Next

                xlBooks.Close()

            Next
        End If
        '要望番号1982（ロンザ　セミEDI時のチェック追加）対応 2013/04/02 本明 End

        Return True

    End Function

    '2015.05.18 修正START ローム2ファイル取込み対応
    ''' <summary>
    ''' 取込処理時入力チェック（関連チェック:標準用）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsTorikomiKanrenCheckStanderdEdition(ByVal rtDs As DataSet, ByVal fileArr As ArrayList, _
                                                         ByVal SafeFileNames() As String) As Boolean
        '2015.05.18 修正END ローム2ファイル取込み対応

        Dim dr As DataRow = rtDs.Tables(LMH030C.SEMIEDI_INFO).Rows(0)

        Dim Rcv_Input_Dir As String = dr.Item("RCV_INPUT_DIR").ToString()
        Dim Rcv_File_Nm As String = dr.Item("RCV_FILE_NM").ToString()
        Dim Rcv_File_Extention As String = dr.Item("RCV_FILE_EXTENTION").ToString()
        Dim Plural_File_Flag As String = dr.Item("PLURAL_FILE_FLAG").ToString()
        Dim Rcv_File_Count As Integer = 0
        Dim Work_File_Count As Integer = 0
        Dim Rcv_Extention_File_Flag As Boolean = False

        '①フォルダチェック
        '既存ファイルの存在チェック
        '受信格納フォルダパス
        If System.IO.Directory.Exists(Rcv_Input_Dir) = False Then
            'メッセージは入力チェック仕様を参照する
            Me.ShowMessage("E079", New String() {"設定されているパス", "フォルダ"})   'エラーメッセージ
            Return False
        End If

        '②ファイルチェック
        '実行前に実際にファイルが存在しているかをチェック
        For Each stFilePath As String In fileArr
            '受信ファイル可否チェック
            On Error Resume Next
            System.IO.File.Exists(String.Concat(Rcv_Input_Dir, stFilePath))
            If Err.Number > 0 Then
                Me.ShowMessage("E469", New String() {String.Concat("ファイル：", stFilePath)})
                Return False
            End If
        Next

        '③受信可否チェック(受信ファイルをオープンされていないかチェック)
        For Each stFilePath As String In fileArr
            Rcv_File_Count += 1

            '受信ファイル可否チェック
            On Error Resume Next
            FileOpen(Rcv_File_Count, String.Concat(Rcv_Input_Dir, stFilePath), OpenMode.Binary, OpenAccess.ReadWrite)
            FileClose(Rcv_File_Count)
            If Err.Number > 0 Then
                Me.ShowMessage("E160", New String() {"受信格納フォルダ", "受信中のファイル"})
                Return False
            End If
        Next

        '複数ファイル更新取込み(FLAG = '2')の場合、複数ファイル選択されていない場合はエラー
        If dr.Item("PLURAL_FILE_FLAG").Equals("2") = True Then
            If SafeFileNames.Length < 2 Then
                Me.ShowMessage("E234")
                Return False
            End If
        End If

        '受信ファイル閾値チェック
        '受信ファイルが999件を超える場合エラー
        If Rcv_File_Count > 999 Then
            Me.ShowMessage("E117", New String() {"取込処理", "999件"})
            Return False
        End If

        Return True

    End Function

#End Region

#End Region
    '取込対応 20120305 End

    ''' <summary>
    ''' ユーザーマスタの存在チェック
    ''' </summary>
    ''' <param name="text">チェック対象文字列</param>
    ''' <returns>ユーザー名</returns>
    ''' <remarks></remarks>
    Private Function IsExistUserNm(ByVal text As String) As String

        ''存在チェック
        Dim userNm As String = String.Empty
        'Dim strSqlUser As String = String.Empty
        'strSqlUser = "USER_ID = '" & text & "'"
        'strSqlUser = strSqlUser & "AND SYS_DEL_FLG = '0'"
        'Dim userRows As DataRow() = Me.GetCachedMasterDataSet().Tables(LMConst.CacheTBL.USER).Select(strSqlUser)
        'If userRows.LenLMh = 0 Then
        '    '存在エラー時
        '    Me.ShowMessage("E024")    'エラーメッセージ
        '    IsExistUserNm = ""
        'Else
        '    '正常時               
        '    userNm = userRows(0).Item("USER_NM").ToString
        'End If

        IsExistUserNm = userNm

    End Function

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMH030C.SprColumnIndex.DEF

        '選択された行の行番号を取得
        'Me._ChkList = Me._Vcon.SprSelectList(defNo, Me._Frm.sprDetail)

        Return Me._Vcon.SprSelectList(defNo, Me._Frm.sprEdiList)

    End Function

    ''' <summary>
    ''' 未選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectDataChk() As Boolean

        'チェックリスト初期化
        Me._ChkList = New ArrayList()

        'チェック行リスト取得
        Me._ChkList = Me.getCheckList()

        '選択チェック
        If Me._Vcon.IsSelectChk(Me._ChkList.Count()) = False Then
            Me.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 未選択、複数選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectDataOneChk() As Boolean

        'チェックリスト初期化
        Me._ChkList = New ArrayList()

        'チェック行リスト取得
        Me._ChkList = Me.getCheckList()

        '選択チェック
        If Me._Vcon.IsSelectChk(Me._ChkList.Count()) = False Then
            Me.ShowMessage("E009")
            Return False
        End If

        '単一選択チェック
        If Me._Vcon.IsSelectOneChk(Me._ChkList.Count()) = False Then
            Me.ShowMessage("E008")
            Return False
        End If

        Return True

    End Function


#Region "自営業所チェック"
    ''' <summary>
    ''' 自営業所チェック
    ''' </summary>
    ''' <returns>True:エラーなし, False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsNrsChk(ByVal rtnMsg As String) As Boolean

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()

        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0
        Dim nrsBrCd As String = String.Empty

        For i As Integer = 0 To max
            With Me._Frm.sprEdiList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                nrsBrCd = .Cells(selectRow, LMH030G.sprEdiListDef.NRS_BR_CD.ColNo).Value().ToString()

                '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
                'If LMUserInfoManager.GetNrsBrCd().Equals(nrsBrCd) = False Then
                '    Return Me._Vcon.SetErrMessage("E178", New String() {rtnMsg})

                'End If

            End With
        Next

        'If LMUserInfoManager.GetNrsBrCd().Equals(Me._Frm.cmbEigyo.SelectedValue.ToString()) = False Then
        '    Return Me._Vcon.SetErrMessage("E178", New String() {rtnMsg})
        'End If

        Return True

    End Function
#End Region


    '▼▼▼要望番号:467
#Region "入力チェック(出力)※(CSV作成・印刷)"

    '要望番号1061 2012.05.15 修正START
    Friend Function IsOutputPrintCheck(ByVal outPutKb As String, ByVal outPutKb2 As String) As Boolean

        '2012.03.03 大阪対応START
        With Me._Frm

            Dim errorCommentFrom As String = String.Concat(.lblTitlePrintDate.Text.ToString(), "From")
            Dim errorCommentTo As String = String.Concat(.lblTitlePrintDate.Text.ToString(), "To")

            '印刷種別
            .cmbOutput.ItemName() = "CSV作成・印刷種別"
            .cmbOutput.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbOutput) = False Then
                Return False
            End If

            '2012.03.18 大阪対応START

            Select Case outPutKb

                '受信帳票,受信一覧表,出荷伝票,一括印刷,EDI出荷取消チェックリスト
                '(2012.05.08)要望番号1007 追加START
                '(2012.09.12)要望番号1429 追加  
                '(2012.12.17)EDI納品送状_BP(埼玉),EDI納品書_BPオートバックス(埼玉),EDI納品書_BPタクティー(埼玉),EDI納品書_BPイエローハット(埼玉)
                '(2012.12.17)EDI納品書_日興イエローハット(大阪),EDI納品書_ロンザ(千葉)
                Case LMH030C.JYUSIN_PRT, LMH030C.JYUSIN_ICHIRAN, _
                     LMH030C.OUTKA_PRT, LMH030C.IKKATU_PRT, _
                     LMH030C.EDIOUTKA_TORIKESHI_CHECKLIST, _
                     LMH030C.NOHIN_OKURIJO, _
                     LMH030C.NOHINSYO_AUTO_BAKKUSU, _
                     LMH030C.NOHIN_TACTI, _
                     LMH030C.NOHIN_YELLOW_HAT, _
                     LMH030C.NOHIN_NIKKO, _
                     LMH030C.NOHIN_RONZA, _
                     LMH030C.NOHIN_OKURIJO_AUTO, _
                     LMH030C.SHIKIRI_TERUMO



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
                    .txtPrt_CustCD_L.ItemName() = "荷主コード(大)"
                    .txtPrt_CustCD_L.IsHissuCheck() = True
                    .txtPrt_CustCD_L.IsFullByteCheck = 5
                    If MyBase.IsValidateCheck(.txtPrt_CustCD_L) = False Then
                        Return False
                    End If

                    '荷主コード(中)
                    .txtPrt_CustCD_M.ItemName() = "荷主コード(中)"
                    .txtPrt_CustCD_M.IsHissuCheck() = True
                    .txtPrt_CustCD_M.IsFullByteCheck = 2
                    If MyBase.IsValidateCheck(.txtPrt_CustCD_M) = False Then
                        Return False
                    End If

#If True Then ' BP運送会社自動設定対応 20161122 added by inoue
                Case LMH030C.INVOICE_NIPPON_EXPRESS_BP _
                   , LMH030C.NOHIN_NICHIGO

                    '選択チェック
                    If IsSelectDataChk() = False Then
                        Return False
                    End If
#End If
                Case Else

            End Select

            Select Case outPutKb

                Case LMH030C.PRINT_CSV

                    'If .cmbOutput.SelectedValue.ToString = LMH030C.PRINT_CSV Then
                    'CSV作成先
                    .cmbOutPutCustKb.ItemName() = " 出荷送信荷主区分"
                    .cmbOutPutCustKb.IsHissuCheck() = True
                    If MyBase.IsValidateCheck(.cmbOutPutCustKb) = False Then
                        Return False
                    End If

                    'End If

                    '2012.04.18 要望番号1005 追加START
                Case LMH030C.RCVCONF_SEND       '受信確認送信

                    'CSV作成先
                    .cmbRcvSendCustkbn.ItemName() = "受信確認送信荷主区分"
                    .cmbRcvSendCustkbn.IsHissuCheck() = True
                    If MyBase.IsValidateCheck(.cmbRcvSendCustkbn) = False Then
                        Return False
                    End If

                    '2012.04.18 要望番号1005 追加END

                Case Else

                    Dim ediCustDrs As DataRow()
                    Dim inOutKb As String = "0"                                     '入出荷区分("0"(出荷))
                    Dim brCd As String = .cmbEigyo.SelectedValue.ToString()         '営業所コード
                    Dim whCd As String = .cmbWare.SelectedValue.ToString()          '倉庫コード
                    Dim custCdL As String = .txtPrt_CustCD_L.TextValue.ToString()   '荷主コード(大)
                    Dim custCdM As String = .txtPrt_CustCD_M.TextValue.ToString()   '荷主コード(中)
                    Dim ediCustIdx As String = String.Empty                         'EDI荷主番号
                    'EDI対象荷主マスタの荷主のINDEXの取得(キャッシュ)
                    ediCustDrs = Me._Vcon.SelectEdiCustListDataRow(inOutKb, brCd, whCd, custCdL, custCdM)
                    If 0 < ediCustDrs.Length Then
                        ediCustIdx = ediCustDrs(0)("EDI_CUST_INDEX").ToString()
                    Else
                        MyBase.ShowMessage("E361")
                        Return False
                    End If

                    Select Case outPutKb

                        Case LMH030C.JYUSIN_PRT '受信帳票

                            ''■■■ダウケミのみ印刷可能（暫定対応）Start　■■■
                            'If .txtPrt_CustCD_L.TextValue.ToString() = "00109" And .cmbEigyo.SelectedValue.ToString() = "20" Then
                            '    If .cmbWare.SelectedValue.ToString() = "200" And .txtPrt_CustCD_M.TextValue.ToString() = "00" Then
                            '    ElseIf .cmbWare.SelectedValue.ToString() = "203" And .txtPrt_CustCD_M.TextValue.ToString() = "01" Then
                            '    Else
                            '        MyBase.ShowMessage("E454", New String() {"印刷が許可されていない荷主", "印刷", ""})
                            '        Return False
                            '    End If
                            'Else
                            '    MyBase.ShowMessage("E454", New String() {"印刷が許可されていない荷主", "印刷", ""})
                            '    Return False
                            'End If
                            ''■■■ダウケミのみ印刷可能（暫定対応）End　■■■

                            '■■■ダウケミのみ印刷可能（暫定対応）Start　■■■
                            Select Case ediCustIdx
                                Case "43", "44", "32", "61", "33"  'ダウケミ,ダウケミ(高石) （2012/11/02日産物流"32"追加）(2012/11/15旭化成(ケミカルズ)"61"追加)(2012/11/15旭化成(イーマテリアルズ追加)"33")

                                Case Else
                                    MyBase.ShowMessage("E454", New String() {"印刷が許可されていない荷主", "印刷", ""})
                                    Return False
                            End Select
                            '■■■ダウケミのみ印刷可能（暫定対応）End　■■■

                            '要望番号1007 2012.05.08 追加START
                            Select Case outPutKb2
                                Case LMH030C.OUTPUT_SUMI
                                    '選択チェック
                                    If IsSelectDataChk() = False Then
                                        Return False
                                    End If

                                Case Else

                            End Select
                            '要望番号1007 2012.05.08 追加END

                        Case LMH030C.JYUSIN_ICHIRAN '受信一覧表

                            Select Case ediCustIdx
                                Case "28", "91", "3", "9", "10", "24"    'ディック大阪,浮間合成(大阪・埼玉),大日精化(埼玉)

                                Case Else
                                    MyBase.ShowMessage("E454", New String() {"印刷が許可されていない荷主", "印刷", ""})
                                    Return False
                            End Select

                            '要望番号1007 2012.05.08 追加START
                            Select Case outPutKb2
                                Case LMH030C.OUTPUT_SUMI
                                    '選択チェック
                                    If IsSelectDataChk() = False Then
                                        Return False
                                    End If

                                Case Else

                            End Select
                            '要望番号1007 2012.05.08 追加END

                        Case LMH030C.OUTKA_PRT      '出荷伝票

                            Select Case ediCustIdx
                                Case "91", "3", "9", "10", "24", "4"     '浮間合成(大阪・埼玉),大日精化(埼玉),ゴードー溶剤(埼玉)

                                Case Else
                                    MyBase.ShowMessage("E454", New String() {"印刷が許可されていない荷主", "印刷", ""})
                                    Return False
                            End Select

                            '要望番号1007 2012.05.08 追加START
                            Select Case outPutKb2
                                Case LMH030C.OUTPUT_SUMI
                                    '選択チェック
                                    If IsSelectDataChk() = False Then
                                        Return False
                                    End If

                                Case Else

                            End Select
                            '要望番号1007 2012.05.08 追加END

                        Case LMH030C.IKKATU_PRT     '一括印刷

                            Select Case ediCustIdx
                                Case "43", "44", "28", "91", "3", "9", "10", "24", "4"     'ダウケミ,ダウケミ(高石),ディック大阪,浮間合成(大阪・埼玉),大日精化(埼玉),ゴードー溶剤(埼玉)

                                Case Else
                                    MyBase.ShowMessage("E454", New String() {"印刷が許可されていない荷主", "印刷", ""})
                                    Return False
                            End Select

                            '要望番号1007 2012.05.08 追加START
                            Select Case outPutKb2
                                Case LMH030C.OUTPUT_SUMI
                                    MyBase.ShowMessage("E320", New String() {"出力済が選択されている", "一括印刷"})
                                    Return False

                                Case Else

                            End Select
                            '要望番号1007 2012.05.08 追加END

                            '2012.09.12 要望番号1429 追加START
                        Case LMH030C.EDIOUTKA_TORIKESHI_CHECKLIST     'EDI出荷取消チェックリスト

                            Select Case ediCustIdx
                                Case "25", "26"  'ディック物流群馬、ディック物流群馬(トートタンク)

                                Case Else
                                    MyBase.ShowMessage("E454", New String() {"印刷が許可されていない荷主", "印刷", ""})
                                    Return False
                            End Select

                            '要望番号:1444 terakawa 2012.09.18 Start
                            Select Case outPutKb2
                                Case LMH030C.OUTPUT_SUMI
                                    '選択チェック
                                    If IsSelectDataChk() = False Then
                                        Return False
                                    End If

                                Case Else

                            End Select
                            '2012.09.12 要望番号1429 追加END
                            '要望番号:1444 terakawa 2012.09.18 End

                            '2012.12.07 BP納品送状(埼玉) START
                        Case LMH030C.NOHIN_OKURIJO     'BP納品送状

                            Select Case ediCustIdx
                                Case "77"  'ビーピー・カストロール株式会社

                                Case Else
                                    MyBase.ShowMessage("E454", New String() {"印刷が許可されていない荷主", "印刷", ""})
                                    Return False
                            End Select

                            '(2013.02.05)要望番号1822 -- START --
                            ''要望番号:1444 terakawa 2012.09.18 Start
                            'Select Case outPutKb2
                            '    Case LMH030C.OUTPUT_SUMI
                            '        '選択チェック
                            '        If IsSelectDataChk() = False Then
                            '            Return False
                            '        End If

                            '    Case Else

                            'End Select
                            ''要望番号:1444 terakawa 2012.09.18 End

                            If IsSelectDataChk() = False Then
                                Return False
                            End If
                            '(2013.02.05)要望番号1822 --  END  --


                            '2012.12.07 BP納品送状(埼玉) END

                            '2012.12.07 BP納品書　オートバックス(埼玉) START
                        Case LMH030C.NOHINSYO_AUTO_BAKKUSU      'BP納品書(オートバックス用)

                            Select Case ediCustIdx
                                Case "77"  'ビーピー・カストロール株式会社

                                Case Else
                                    MyBase.ShowMessage("E454", New String() {"印刷が許可されていない荷主", "印刷", ""})
                                    Return False
                            End Select

                            '(2013.02.05)要望番号1822 -- START --
                            ''要望番号:1444 terakawa 2012.09.18 Start
                            'Select Case outPutKb2
                            '    Case LMH030C.OUTPUT_SUMI
                            '        '選択チェック
                            '        If IsSelectDataChk() = False Then
                            '            Return False
                            '        End If

                            '    Case Else

                            'End Select
                            ''要望番号:1444 terakawa 2012.09.18 End

                            '選択チェック
                            If IsSelectDataChk() = False Then
                                Return False
                            End If
                            '(2013.02.05)要望番号1822 --  END  --

                            '2012.12.07 BP納品書　オートバックス(埼玉) END

                            '2012.12.07 BP納品書　タクティー(埼玉) START
                        Case LMH030C.NOHIN_TACTI   'BP納品書(タクティー用)

                            Select Case ediCustIdx
                                Case "77"  'ビーピー・カストロール株式会社

                                Case Else
                                    MyBase.ShowMessage("E454", New String() {"印刷が許可されていない荷主", "印刷", ""})
                                    Return False
                            End Select

                            '(2013.02.05)要望番号1822 -- START --
                            ''要望番号:1444 terakawa 2012.09.18 Start
                            'Select Case outPutKb2
                            '    Case LMH030C.OUTPUT_SUMI
                            '        '選択チェック
                            '        If IsSelectDataChk() = False Then
                            '            Return False
                            '        End If

                            '    Case Else

                            'End Select
                            ''要望番号:1444 terakawa 2012.09.18 End

                            '選択チェック
                            If IsSelectDataChk() = False Then
                                Return False
                            End If
                            '(2013.02.05)要望番号1822 --  END  --

                            '2012.12.07 BP納品書　タクティー(埼玉) END

                            '2012.12.07 BP納品書　イエローハット(埼玉) START
                        Case LMH030C.NOHIN_YELLOW_HAT      'BP納品書(イエローハット用)

                            Select Case ediCustIdx
                                Case "77"  'ビーピー・カストロール株式会社

                                Case Else
                                    MyBase.ShowMessage("E454", New String() {"印刷が許可されていない荷主", "印刷", ""})
                                    Return False
                            End Select

                            '(2013.02.05)要望番号1822 -- START --
                            ''要望番号:1444 terakawa 2012.09.18 Start
                            'Select Case outPutKb2
                            '    Case LMH030C.OUTPUT_SUMI
                            '        '選択チェック
                            '        If IsSelectDataChk() = False Then
                            '            Return False
                            '        End If

                            '    Case Else

                            'End Select
                            ''要望番号:1444 terakawa 2012.09.18 End

                            '選択チェック
                            If IsSelectDataChk() = False Then
                                Return False
                            End If
                            '(2013.02.05)要望番号1822 --  END  --

                            '2012.12.07 BP納品書　イエローハット(埼玉) END

                            '2012.12.20 ロンザ納品書送り状(千葉) START
                        Case LMH030C.NOHIN_RONZA      'ロンザ納品書送り状(千葉)

                            Select Case ediCustIdx
                                Case "84"  'ロンザジャパン

                                Case Else
                                    MyBase.ShowMessage("E454", New String() {"印刷が許可されていない荷主", "印刷", ""})
                                    Return False
                            End Select

                            '要望番号:1444 terakawa 2012.09.18 Start
                            Select Case outPutKb2
                                Case LMH030C.OUTPUT_SUMI
                                    '選択チェック
                                    If IsSelectDataChk() = False Then
                                        Return False
                                    End If

                                Case Else

                            End Select
                            '要望番号:1444 terakawa 2012.09.18 End
                            '2012.12.20 ロンザ納品書送り状(千葉) END



                            '2012.12.26 EDI納品書 日興イエローハット(大阪) START
                        Case LMH030C.NOHIN_RONZA      'EDI納品書 日興イエローハット(大阪)

                            Select Case ediCustIdx
                                Case "98"  '日興産業株式会社

                                Case Else
                                    MyBase.ShowMessage("E454", New String() {"印刷が許可されていない荷主", "印刷", ""})
                                    Return False
                            End Select

                            '要望番号:1444 terakawa 2012.09.18 Start
                            Select Case outPutKb2
                                Case LMH030C.OUTPUT_SUMI
                                    '選択チェック
                                    If IsSelectDataChk() = False Then
                                        Return False
                                    End If

                                Case Else

                            End Select
                            '要望番号:1444 terakawa 2012.09.18 End
                            '2012.12.26 EDI納品書 日興イエローハット(大阪) END






                        Case Else

                    End Select

            End Select

            '2012.03.18 大阪対応END

            '2012.04.18 要望番号1005 修正START
            Select Case outPutKb

                Case LMH030C.RCVCONF_SEND

                Case Else

                    '要望番号1007 2012.05.08 修正START
                    Select Case outPutKb2

                        Case LMH030C.OUTPUT_SUMI

                        Case Else
                            '出荷日FROM or EDI取込日FROM
                            If .imdOutputDateFrom.IsDateFullByteCheck(8) = False Then
                                MyBase.ShowMessage("E038", New String() {errorCommentFrom, "8"})
                                'MyBase.ShowMessage("E038", New String() {"出荷日From", "8"})
                                Return False
                            End If

                            '出荷日TO or EDI取込日TO
                            If .imdOutputDateTo.IsDateFullByteCheck(8) = False Then
                                MyBase.ShowMessage("E038", New String() {errorCommentTo, "8"})
                                'MyBase.ShowMessage("E038", New String() {"出荷日To", "8"})
                                Return False
                            End If

                            '出荷日大小 or EDI取込日大小
                            If String.IsNullOrEmpty(.imdOutputDateFrom.TextValue) = False AndAlso String.IsNullOrEmpty(.imdOutputDateTo.TextValue) = False Then
                                If Convert.ToInt32(.imdOutputDateTo.TextValue) < Convert.ToInt32(.imdOutputDateFrom.TextValue) Then
                                    Me.ShowMessage("E039", New String() {errorCommentTo, errorCommentFrom})
                                    'Me.ShowMessage("E039", New String() {"出荷日To", "出荷日From"})
                                    .imdOutputDateFrom.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                                    .imdOutputDateTo.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                                    .imdOutputDateFrom.Focus()
                                    Return False
                                End If

                            End If

                    End Select
                    '要望番号1007 2012.05.08 追加END

            End Select
            '2012.04.18 要望番号1005 修正END

        End With

        Return True
        '2012.03.03 大阪対応END

    End Function

#End Region
    '▲▲▲要望番号:467

    '2012.04.18 要望番号1005 追加START
#Region "関連チェック(CSV作成・受信確認送信(浮間合成))"

    ''' <summary>
    ''' CSV作成・受信確認送信(浮間合成)時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRcvConfirmSendCheck(ByVal rtDs As DataSet) As Boolean

        Dim dr As DataRow = rtDs.Tables(LMH030C.TABLE_NM_RCVCONF_INFO).Rows(0)

        Dim inkaBackup_Input_Dir As String = dr.Item("INKA_BACKUP_INPUT_DIR").ToString()
        Dim outkaBackup_Input_Dir As String = dr.Item("OUTKA_BACKUP_INPUT_DIR").ToString()
        Dim Send_Input_Dir As String = dr.Item("SEND_INPUT_DIR").ToString()
        Dim Work_Input_Dir As String = dr.Item("WORK_INPUT_DIR").ToString()
        Dim inka_Hokoku_Dir As String = dr.Item("INKA_HOKOKU_DIR").ToString()
        Dim outka_Hokoku_Dir As String = dr.Item("OUTKA_HOKOKU_DIR").ToString()
        Dim Send_File_Nm As String = dr.Item("SEND_FILE_NM").ToString()

        Dim inkaWork_File_Count As Integer = 0
        Dim outkaWork_File_Count As Integer = 0

        '②フォルダチェック
        '既存ファイルの存在チェック
        '②-1 BACKUPフォルダパス
        '②-2 作業フォルダパス
        '②-3 送信データ格納フォルダパス
        '②-4 送信済ファイル格納フォルダパス
        If System.IO.Directory.Exists(inkaBackup_Input_Dir) = False OrElse _
           System.IO.Directory.Exists(outkaBackup_Input_Dir) = False OrElse _
           System.IO.Directory.Exists(Work_Input_Dir) = False OrElse _
           System.IO.Directory.Exists(inka_Hokoku_Dir) = False OrElse _
           System.IO.Directory.Exists(outka_Hokoku_Dir) = False OrElse _
           System.IO.Directory.Exists(Send_Input_Dir) = False Then

            'メッセージは入力チェック仕様を参照する
            Me.ShowMessage("E079", New String() {"設定されているパス", "フォルダ"})   'エラーメッセージ
            Return False
        End If


        '③フォルダ空チェック
        ' ワークファイルディレクトリ内のファイルを取得
        For Each stFilePath As String In System.IO.Directory.GetFiles(Work_Input_Dir, "*")
            inkaWork_File_Count += 1
        Next stFilePath

        If inkaWork_File_Count > 0 Then
            Me.ShowMessage("E160", New String() {"作業フォルダ", "作業ファイル"})   'エラーメッセージ
            Return False
        End If


        Return True

    End Function

#End Region
    '2012.04.18 要望番号1005 追加END

#Region "入力チェック(印刷ボタン押下)"

    Friend Function IsSelPrintCheck(ByVal eventShubetsu As Integer, _
                                     ByVal printShubetsu As Integer) As Boolean

        With Me._Frm

            '印刷種別
            .cmbPrint.ItemName() = "印刷種別"
            .cmbPrint.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbPrint) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#End Region

    'BP・カストロール対応 terakawa 2012.12.26 Start
#Region "営業日取得"
    ''' <summary>
    ''' 営業日取得
    ''' </summary>
    ''' <param name="sStartDay"></param>
    ''' <param name="iBussinessDays"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetBussinessDay(ByVal sStartDay As String, ByVal iBussinessDays As Integer) As DateTime
        'sStartDate     ：基準日（YYYYMMDD形式）
        'iBussinessDays ：基準日からの営業日数（前々営業日の場合は-2、前営業日の場合は-1、翌営業日の場合は+1、翌々営業日の場合は+2）
        '戻り値         ：求めた営業日（YYYY/MM/DD形式）

        'スラッシュを付加して日付型に変更
        Dim dBussinessDate As DateTime = Convert.ToDateTime((Convert.ToInt32(sStartDay)).ToString("0000/00/00"))

        For i As Integer = 1 To System.Math.Abs(iBussinessDays)  'マイナス値に対応するため絶対値指定

            '基準日からの営業日数分、Doループを繰り返す
            Do
                '日付加算
                If iBussinessDays > 0 Then
                    dBussinessDate = dBussinessDate.AddDays(1)      '翌営業日
                Else
                    dBussinessDate = dBussinessDate.AddDays(-1)     '前営業日
                End If

                If Weekday(dBussinessDate) = 1 OrElse Weekday(dBussinessDate) = 7 Then
                Else
                    '土日でない場合

                    '該当する日付が休日マスタに存在するか？
                    Dim sBussinessDate As String = Format(dBussinessDate, "yyyyMMdd")
                    Dim holDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.HOL).Select(" SYS_DEL_FLG = '0' AND HOL = '" & sBussinessDate & "'")
                    If holDr.Count = 0 Then
                        '休日マスタに存在しない場合、dBussinessDateが求める日
                        Exit Do
                    End If

                End If
            Loop
        Next

        Return dBussinessDate

    End Function

#End Region
    'BP・カストロール対応 terakawa 2012.12.26 End


#End Region

End Class
