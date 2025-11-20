' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDIサブシステム
'  プログラムID     :  LMH010V : EDI入荷データ検索
'  作  成  者       :  
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMH010Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMH010V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMH010F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMHControlV

    ''' <summary>
    ''' Gamen共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMHControlG

    ''' <summary>
    ''' チェックリストを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconV As LMHControlV

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMH010G

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMH010F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Gcon = New LMHControlG(frm)

        'Validate共通クラスの設定
        Me._Vcon = New LMHControlV(handlerClass, DirectCast(frm, Form), _Gcon)

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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMH010C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMH010C.EventShubetsu.KENSAKU, _
                 LMH010C.EventShubetsu.PRINT, _
                 LMH010C.EventShubetsu.JIKKOU_GENPIN_PRINT '検索,印刷,現品票印刷

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

            Case LMH010C.EventShubetsu.TOROKU          '登録

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

            Case LMH010C.EventShubetsu.HIMODUKE         '紐付

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

            Case LMH010C.EventShubetsu.JISSEKI_SAKUSE       '実績作成

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

            Case LMH010C.EventShubetsu.EDI_TORIKESI 'EDI取消

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

            Case LMH010C.EventShubetsu.JISSEKI_TORIKESI '実績取消

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

            Case LMH010C.EventShubetsu.MASTER    'マスタ参照

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

            Case LMH010C.EventShubetsu.DEF_CUST    '初期荷主変更

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


            Case LMH010C.EventShubetsu.CLOSE           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMH010C.EventShubetsu.JIKKOU_SAKUSEI_JISSEKIMI '実績作成⇒実績未

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


            Case LMH010C.EventShubetsu.JIKKOU_TORIKESI_MITOUROKU 'EDI取消⇒未登録

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



            Case LMH010C.EventShubetsu.JIKKOU_HOUKOKU_EDI_TORIKESI '報告用EDI取消

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

            Case LMH010C.EventShubetsu.JIKKOU_TOUROKU_MITOUROKU '登録済⇒未登録

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

            Case LMH010C.EventShubetsu.JIKKOU_SOUSIN_SOUSINMI '送信済⇒送信待
                '10:閲覧者、20:入力者(一般)、25:入力者(上級)、30：システム管理者、50:外部の場合エラー
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

            Case LMH010C.EventShubetsu.JIKKOU_SOUSIN_JISSEKIMI '送信済⇒実績未
                '10:閲覧者、20:入力者(一般)、25:入力者(上級)、30：システム管理者、50:外部の場合エラー
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

                    '(2012.10.02) 要望番号1439 追加START
                Case LMH010C.EventShubetsu.COA_TOUROKU '分析票ファイル取込

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
                '(2012.10.02) 要望番号1439 追加END


            Case LMH010C.EventShubetsu.BULK_CUST_CHANGE '荷主一括変更 2015.09.03 tsunehira add

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

            Case LMH010C.EventShubetsu.DOUBLE_CLICK 'ダブルクリック

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

#End Region '権限チェック

#Region "検索時チェック"


    ''' <summary>
    ''' 検索時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKensakuSingleCheck(ByVal g As LMH010G) As Boolean


        'ヘッダ項目のスペース除去
        Call Me.TrimHeaderSpaceTextValue()

        'スプレッドのスペース除去
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprEdiList, 0)

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
            '.txtCustCD_L.IsByteCheck() = 5
            .txtCustCD_L.IsHissuCheck() = True
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

            ''日付
            'If String.IsNullOrEmpty(.cmbSelectDate.SelectedText) = False AndAlso .imdEdiDateFrom.IsDateFullByteCheck(8) = False Then
            '    MyBase.ShowMessage("E038", New String() {"日付", "8"})
            '    Return False
            'End If

            'EDI取込日(FROM)
            If .imdEdiDateFrom.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"EDI   取込日From", "8"})
                Return False
            End If

            'EDI取込日(TO)
            If .imdEdiDateTo.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"EDI取込日To", "8"})
                Return False
            End If

            '入荷日(FROM)
            If .imdInkaDateFrom.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"入荷日From", "8"})
                Return False
            End If

            'EDI取込日(TO)
            If .imdInkaDateTo.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"入荷日To", "8"})
                Return False
            End If

            '******************** スプレッド項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprEdiList)

            'オーダー番号
            vCell.SetValidateCell(0, g.sprEdiListDef.ORDER_NO.ColNo)
            vCell.ItemName() = "オーダー番号"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 30
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名
            vCell.SetValidateCell(0, g.sprEdiListDef.CUST_NM.ColNo)
            vCell.ItemName() = "荷主名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 122
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '商品名
            vCell.SetValidateCell(0, g.sprEdiListDef.ITEM_NM.ColNo)
            vCell.ItemName() = "商品名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '運送会社名
            vCell.SetValidateCell(0, g.sprEdiListDef.UNSO_CORP.ColNo)
            vCell.ItemName() = "運送会社名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 122
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '2013.04.03 Notes1995 START
            '出荷元
            vCell.SetValidateCell(0, g.sprEdiListDef.OUTKA_MOTO_NM.ColNo)
            vCell.ItemName() = "出荷元"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 80
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '2013.04.03 Notes1995 END

            'EDI管理番号(大)
            vCell.SetValidateCell(0, g.sprEdiListDef.EDI_NO.ColNo)
            vCell.ItemName() = "EDI管理番号(大)"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 9
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '注文番号
            vCell.SetValidateCell(0, g.sprEdiListDef.BUYER_ORDER_NO.ColNo)
            vCell.ItemName() = "注文番号"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 30
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '担当者
            vCell.SetValidateCell(0, g.sprEdiListDef.TANTO_USER_NM.ColNo)
            vCell.ItemName() = "担当者"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '作成者
            vCell.SetValidateCell(0, g.sprEdiListDef.SYS_ENT_USER_NM.ColNo)
            vCell.ItemName() = "作成者"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '更新者
            vCell.SetValidateCell(0, g.sprEdiListDef.SYS_UPD_USER_NM.ColNo)
            vCell.ItemName() = "最終更新者"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function


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

            '入荷日
            If String.IsNullOrEmpty(.imdInkaDateFrom.TextValue) = False AndAlso String.IsNullOrEmpty(.imdInkaDateTo.TextValue) = False Then
                If Convert.ToInt32(.imdInkaDateTo.TextValue) < Convert.ToInt32(.imdInkaDateFrom.TextValue) Then
                    '入荷日Fromより入荷日Toが過去日の場合エラー
                    Me.ShowMessage("E039", New String() {"入荷日To", "入荷日From"})
                    .imdInkaDateFrom.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdInkaDateTo.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdInkaDateFrom.Focus()
                    Return False
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
            .txtTantouCd.TextValue = Me._Frm.txtTantouCd.TextValue.Trim()
        End With

    End Sub


#End Region '検索時チェック

#Region "入荷登録時チェック"

    ''' <summary>
    ''' 入荷登録時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function InkaTorokuSingleCheck(ByVal g As LMH010G) As Boolean

        With Me._Frm

            '【単項目チェック】
            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            '自営業所チェック
            Dim rtnMsg As String = String.Empty
            rtnMsg = "入荷登録"

            If Me.IsNrsChk(rtnMsg, g) = False Then
                Return False
            End If

        End With

        Return True

    End Function



    ''' <summary>
    ''' M品振替出荷単項目チェック
    ''' </summary>
    ''' <param name="g"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function TransferCondMSingleCheck(ByVal g As LMH010G) As Boolean

        With Me._Frm

            '【単項目チェック】
            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            '自営業所チェック
            Dim rtnMsg As String = _Frm.cmbJikkou.SelectedText

            If Me.IsNrsChk(rtnMsg, g) = False Then
                Return False
            End If

        End With

        Return True

    End Function


    ''' <summary>
    '''  M品振替出荷関連チェック
    ''' </summary>
    ''' <param name="eventShubetsu"></param>
    ''' <param name="errDs"></param>
    ''' <param name="g"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function TransferCondMKanrenCheck(ByVal eventShubetsu As LMH010C.EventShubetsu _
                                           , ByRef errDs As DataSet _
                                           , ByVal g As LMH010G) As Hashtable


        Dim errTable As Hashtable = New Hashtable

        errDs = New LMH010DS()

        'チェックされた行番号取得
        Me._ChkList = Me.getCheckList()

        Dim delKb As String = String.Empty
        Dim outF As String = String.Empty
        Dim ediCtlNo As String = String.Empty
        Dim outkaLCondM As String = String.Empty

        Dim inkaStatKb As String = String.Empty

        Dim dr As DataRow = Nothing

        Dim rowIdx As Integer = 0
        Dim max As Integer = Me._ChkList.Count() - 1
        For i As Integer = 0 To max

            With Me._Frm.sprEdiList.ActiveSheet

                rowIdx = Convert.ToInt32(Me._ChkList(i))
                delKb = .Cells(rowIdx, g.sprEdiListDef.DEL_KB.ColNo).Value().ToString()
                outF = .Cells(rowIdx, g.sprEdiListDef.OUT_FLAG.ColNo).Value().ToString()
                ediCtlNo = .Cells(rowIdx, g.sprEdiListDef.EDI_NO.ColNo).Value().ToString()
                outkaLCondM = .Cells(rowIdx, g.sprEdiListDef.OUTKA_CTL_NO_L_COND_M.ColNo).Value().ToString()

                inkaStatKb = .Cells(rowIdx, g.sprEdiListDef.INKA_STATE_KB.ColNo).Value().ToString()

                ' 進捗区分チェック
                If (LMConst.FLG.OFF.Equals(delKb) = False OrElse _
                    LMConst.FLG.ON.Equals(outF) = False OrElse
                    LMHControlC.INKA_STATE_KB.AlreadyScheduledInput.Equals(inkaStatKb) = False) Then

                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH010C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E336"
                    dr("PARA1") = "予定入力済"
                    dr("PARA2") = _Frm.cmbJikkou.SelectedText
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = rowIdx.ToString()
                    errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Add(dr)

                    errTable.Add(i, String.Empty)
                    Continue For
                ElseIf (String.IsNullOrEmpty(outkaLCondM) = False) Then

                    dr = errDs.Tables(LMH010C.TABLE_NM_GUIERROR).NewRow()

                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E969"
                    dr("PARA1") = ""
                    dr("PARA2") = ""
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = rowIdx.ToString()

                    errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Add(dr)

                    errTable.Add(i, String.Empty)
                    Continue For

                End If

            End With
        Next

        Return errTable


    End Function


    ''' <summary>
    ''' 入荷登録時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Public Function InkaTorokuKanrenCheck(ByVal eventShubetsu As LMH010C.EventShubetsu, ByRef errDs As DataSet, ByVal g As LMH010G) As Hashtable

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
        Dim ediCtlNo As String = String.Empty
        '↓FFEM特殊処理↓
        '2014.06.09 追加START
        Dim custindex As String = String.Empty
        '↑FFEM特殊処理↑
        '2014.06.09 追加END
        '2015.09.10 tsunehira add
        Dim data01 As String = String.Empty

        errDs = New LMH010DS()

        Dim dr As DataRow

        For i As Integer = 0 To max

            With Me._Frm.sprEdiList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                delKb = .Cells(selectRow, g.sprEdiListDef.DEL_KB.ColNo).Value().ToString()
                custHoldF = .Cells(selectRow, g.sprEdiListDef.EDI_CUST_HOLDOUT.ColNo).Value().ToString()
                outF = .Cells(selectRow, g.sprEdiListDef.OUT_FLAG.ColNo).Value().ToString()
                custUnsoF = .Cells(selectRow, g.sprEdiListDef.EDI_CUST_UNSO.ColNo).Value().ToString()
                mCount = .Cells(selectRow, g.sprEdiListDef.MDL_REC_CNT.ColNo).Value().ToString()
                akakuroF = .Cells(selectRow, g.sprEdiListDef.AKAKURO_FLG.ColNo).Value().ToString()
                jissekiF = .Cells(selectRow, g.sprEdiListDef.JISSEKI_FLAG.ColNo).Value().ToString()
                ediCtlNo = .Cells(selectRow, g.sprEdiListDef.EDI_NO.ColNo).Value().ToString()
                data01 = .Cells(selectRow, g.sprEdiListDef.CHG_CUST_CD.ColNo).Value().ToString()
                '↓FFEM特殊処理↓
                '2014.06.09 追加START
                custindex = .Cells(selectRow, g.sprEdiListDef.EDI_CUST_INDEX.ColNo).Value().ToString()
                '↑FFEM特殊処理↑
                '2014.06.09 追加END

                'テルモ専用チェック
                If "00001".Equals(.Cells(selectRow, g.sprEdiListDef.CUST_CD_L.ColNo).Value().ToString) Then
                    '倉庫移動データは入荷登録の対象としない
                    If "倉庫移動".Equals(Left(.Cells(selectRow, g.sprEdiListDef.ORDER_NO.ColNo).Value().ToString, 4)) Then
                        'エラーがある場合、DataTableに設定
                        dr = errDs.Tables(LMH010C.TABLE_NM_GUIERROR).NewRow()
                        dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                        dr("MESSAGE_ID") = "E01U"
                        dr("PARA1") = "倉庫移動は在庫振替で振替を実施してください。"
                        dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                        dr("KEY_VALUE") = ediCtlNo
                        dr("ROW_NO") = selectRow.ToString()
                        errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Add(dr)

                        errHt.Add(i, String.Empty)
                        Continue For
                    End If
                End If

                '未登録データチェック
                If (delKb = "0" OrElse delKb = "3" OrElse delKb = "2") AndAlso outF = "0" _
                    AndAlso (jissekiF = "0" OrElse jissekiF = "9") Then

                Else
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH010C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E336"
                    dr("PARA1") = "未登録データ"
                    dr("PARA2") = "荷主変更"
                    dr("PARA2") = "入荷登録"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Add(dr)

                    'Me.ShowMessage("E336", New String() {"未登録データ", "入荷登録"})   'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For

                End If

                '入荷登録対象チェック
                If custUnsoF <> "1" Then
                Else
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH010C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E320"
                    dr("PARA1") = "運送データ"
                    dr("PARA2") = "入荷登録"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Add(dr)

                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                If delKb = "3" AndAlso custHoldF = "1" Then
                    '保留データかつ保留データ登録フラグが"1"の場合はワーニング
                    rtn = Me.ShowMessage("W169", New String() {"入荷登録", ediCtlNo})
                    If rtn = MsgBoxResult.Ok Then
                    ElseIf rtn = MsgBoxResult.Cancel Then
                        errHt.Add(i, String.Empty)
                        Continue For
                    End If

                ElseIf delKb = "3" AndAlso custHoldF = "2" Then
                    'EDI取消するデータ
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH010C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E376"
                    dr("PARA1") = "保留データ"
                    dr("PARA2") = "入荷登録"
                    dr("PARA3") = "EDI取消"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Add(dr)

                    errHt.Add(i, String.Empty)
                    Continue For

                ElseIf delKb = "1" OrElse delKb = "2" Then

                    '↓FFEM特殊処理↓
                    '実績データのキャンセル報告対応
                    '2014.06.09 追加START
                    Select Case custindex
                        Case "40" '富士フイルム(千葉BC)

                            'キャンセルデータはエラーにしない
                            If delKb = "1" Then

                                'エラーがある場合、DataTableに設定
                                dr = errDs.Tables(LMH010C.TABLE_NM_GUIERROR).NewRow()
                                dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                                dr("MESSAGE_ID") = "E320"
                                dr("PARA1") = "削除データもしくはキャンセルデータ"
                                dr("PARA2") = "入荷登録"
                                dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                                dr("KEY_VALUE") = ediCtlNo
                                dr("ROW_NO") = selectRow.ToString()
                                errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Add(dr)

                                errHt.Add(i, String.Empty)
                                Continue For

                            End If

                        Case Else

                            'エラーがある場合、DataTableに設定
                            dr = errDs.Tables(LMH010C.TABLE_NM_GUIERROR).NewRow()
                            dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                            dr("MESSAGE_ID") = "E320"
                            dr("PARA1") = "削除データもしくはキャンセルデータ"
                            dr("PARA2") = "入荷登録"
                            dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                            dr("KEY_VALUE") = ediCtlNo
                            dr("ROW_NO") = selectRow.ToString()
                            errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Add(dr)

                            errHt.Add(i, String.Empty)
                            Continue For

                    End Select
                    '↑FFEM特殊処理↑
                    '実績データのキャンセル報告対応
                    '2014.06.09 追加END

                Else

                End If


                'EDI入荷(中)の件数が 0の場合エラー
                If mCount = "0" Then
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH010C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E320"
                    dr("PARA1") = "中件数が 0件"
                    dr("PARA2") = "入荷登録"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Add(dr)
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                'EDI入荷(中)に赤データがある場合エラー
                If akakuroF = "1" Then
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH010C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E320"
                    dr("PARA1") = "中データが赤データ"
                    dr("PARA2") = "入荷登録"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Add(dr)
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

            End With

        Next

        Return errHt
    End Function

#End Region '入荷登録時チェック

    '2015.09.10 tsunehira add
#Region "一括変更時、・未登録データチェック"
    ''' <summary>
    ''' 一括変更時、・未登録データチェック（関連チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Public Function InkaChgKanrenCheck(ByVal eventShubetsu As LMH010C.EventShubetsu, ByRef errDs As DataSet, ByVal g As LMH010G) As Hashtable

        Dim errHt As Hashtable = New Hashtable

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0

        Dim delKb As String = String.Empty
        Dim outF As String = String.Empty
        Dim jissekiF As String = String.Empty
        Dim data01 As String = String.Empty
        Dim ediCtlNo As String = String.Empty

        errDs = New LMH010DS()

        Dim dr As DataRow

        For i As Integer = 0 To max

            With Me._Frm.sprEdiList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                delKb = .Cells(selectRow, g.sprEdiListDef.DEL_KB.ColNo).Value().ToString()
                jissekiF = .Cells(selectRow, g.sprEdiListDef.JISSEKI_FLAG.ColNo).Value().ToString()
                outF = .Cells(selectRow, g.sprEdiListDef.OUT_FLAG.ColNo).Value().ToString()
                data01 = .Cells(selectRow, g.sprEdiListDef.CHG_CUST_CD.ColNo).Value().ToString()
                ediCtlNo = .Cells(selectRow, g.sprEdiListDef.EDI_NO.ColNo).Value().ToString()

                If data01 = "" Then
                    dr = errDs.Tables(LMH010C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E375"
                    dr("PARA1") = "変更先の荷主コードが設定されていない"
                    dr("PARA2") = "荷主変更"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Add(dr)

                    errHt.Add(i, String.Empty)
                    Continue For
                Else

                End If

                '未登録データチェック
                If (delKb = "0" OrElse delKb = "3" OrElse delKb = "2") AndAlso outF = "0" _
                    AndAlso (jissekiF = "0" OrElse jissekiF = "9") Then

                Else

                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH010C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E336"
                    dr("PARA1") = "未登録データ"
                    dr("PARA2") = "荷主変更"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Add(dr)

                    errHt.Add(i, String.Empty)

                    Continue For
                End If

            End With

        Next

        Return errHt
    End Function
#End Region

#Region "実績作成チェック"

    ''' <summary>
    ''' 実績作成時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function JissekiSakuseiSingleCheck(ByVal g As LMH010G) As Boolean

        '【単項目チェック】
        '選択チェック
        If Me.IsSelectDataChk() = False Then
            Return False
        End If

        '自営業所チェック
        Dim rtnMsg As String = String.Empty
        rtnMsg = "実績作成"

        If Me.IsNrsChk(rtnMsg, g) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 実績作成時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Public Function JissekiSakuseiKanrenCheck(ByVal eventShubetsu As LMH010C.EventShubetsu, ByRef errDs As DataSet, ByVal g As LMH010G) As Hashtable

        Dim errHt As Hashtable = New Hashtable

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0

        Dim jissekiF As String = String.Empty
        Dim custJissekiF As String = String.Empty
        Dim outF As String = String.Empty
        Dim ediDelF As String = String.Empty
        Dim inkaDelF As String = String.Empty
        Dim stateKb As String = String.Empty
        Dim ediCtlNo As String = String.Empty

        errDs = New LMH010DS()
        Dim dr As DataRow

        For i As Integer = 0 To max

            With Me._Frm.sprEdiList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                jissekiF = .Cells(selectRow, g.sprEdiListDef.JISSEKI_FLAG.ColNo).Value().ToString()
                custJissekiF = .Cells(selectRow, g.sprEdiListDef.EDI_CUST_JISSEKI.ColNo).Value().ToString()
                outF = .Cells(selectRow, g.sprEdiListDef.OUT_FLAG.ColNo).Value().ToString()
                ediDelF = .Cells(selectRow, g.sprEdiListDef.SYS_DEL_FLG.ColNo).Value().ToString()
                inkaDelF = .Cells(selectRow, g.sprEdiListDef.INKA_DEL_FLG.ColNo).Value().ToString()
                stateKb = .Cells(selectRow, g.sprEdiListDef.INKA_STATE_KB.ColNo).Value().ToString()
                ediCtlNo = .Cells(selectRow, g.sprEdiListDef.EDI_NO.ColNo).Value().ToString()

                If jissekiF = "0" Then

                Else
                    'EDI入荷(大)の実績書込Fが実績未でない場合エラー

                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH010C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E336"
                    dr("PARA1") = "実績未データ"
                    dr("PARA2") = "実績作成"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Add(dr)

                    'Me.ShowMessage("E336", New String() {"実績未データ", "実績作成"})    'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                Dim tempStateKb As Integer = 0
                If String.IsNullOrEmpty(stateKb) = False Then
                    tempStateKb = Convert.ToInt32(stateKb)
                End If

                If (custJissekiF = "1" OrElse custJissekiF = "2" OrElse custJissekiF = "3") AndAlso _
                   ediDelF = "0" AndAlso inkaDelF = "0" AndAlso tempStateKb >= 50 Then

                ElseIf custJissekiF = "2" AndAlso (ediDelF = "1" OrElse inkaDelF = "1") Then

                ElseIf custJissekiF = "3" AndAlso (ediDelF = "0" OrElse outF = "2") Then

                ElseIf custJissekiF = "4" AndAlso (ediDelF = "0" OrElse inkaDelF = "0") Then

                Else

                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH010C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E336"
                    dr("PARA1") = "実績未データ"
                    dr("PARA2") = "実績作成"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Add(dr)

                    'Me.ShowMessage("E336", New String() {"実績未データ", "実績作成"})     'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

            End With

        Next

        Return errHt

    End Function

#End Region 'EDI取消時チェック

    '2015.04.13 追加START
#Region "入力チェック（取込処理）"

#Region "単項目チェック"

    ''' <summary>
    ''' 取込処理イベントの入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsTorikomiSingleCheck(ByVal g As LMH010G) As Boolean

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
        rtnMsg = "取込"

        If Me.IsNrsChk(rtnMsg, g) = False Then
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

        Dim dr As DataRow = rtDs.Tables(LMH010C.SEMIEDI_INFO).Rows(0)

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

        Return True

    End Function

    ''' <summary>
    ''' 取込処理時入力チェック（関連チェック:標準用）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsTorikomiKanrenCheckStanderdEdition(ByVal rtDs As DataSet, ByVal fileArr As ArrayList) As Boolean

        Dim dr As DataRow = rtDs.Tables(LMH010C.SEMIEDI_INFO).Rows(0)

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

        '受信ファイル閾値チェック
        '受信ファイルが999件を超える場合エラー
        If Rcv_File_Count > 999 Then
            Me.ShowMessage("E117", New String() {"取込処理", "999件"})
            Return False
        End If

        Return True

    End Function

#End Region
    '2015.04.13 追加END

#End Region

#Region "入力チェック（紐付け）"

#Region "単項目チェック"
    ''' <summary>
    ''' 紐付けイベントの入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsHimodukeSingleCheck(ByVal g As LMH010G) As Boolean

        With Me._Frm

            '【単項目チェック】
            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            If Me.IsSelectOneChk() = False Then
                Return False
            End If


        End With

        '自営業所チェック
        Dim rtnMsg As String = String.Empty
        rtnMsg = "紐付け"

        If Me.IsNrsChk(rtnMsg, g) = False Then
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
    Friend Function IsHimodukeKanrenCheck(ByVal eventshubetsu As LMH010C.EventShubetsu, ByVal g As LMH010G) As Hashtable

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
        Dim ediCtlNo As String = String.Empty

        '続行確認
        Dim rtn As MsgBoxResult

        For i As Integer = 0 To max

            With Me._Frm.sprEdiList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                delKb = .Cells(selectRow, g.sprEdiListDef.DEL_KB.ColNo).Value().ToString()
                custHoldF = .Cells(selectRow, g.sprEdiListDef.EDI_CUST_HOLDOUT.ColNo).Value().ToString()
                outF = .Cells(selectRow, g.sprEdiListDef.OUT_FLAG.ColNo).Value().ToString()
                custUnsoF = .Cells(selectRow, g.sprEdiListDef.EDI_CUST_UNSO.ColNo).Value().ToString()
                mCount = .Cells(selectRow, g.sprEdiListDef.MDL_REC_CNT.ColNo).Value().ToString()
                akakuroF = .Cells(selectRow, g.sprEdiListDef.AKAKURO_FLG.ColNo).Value().ToString()
                jissekiF = .Cells(selectRow, g.sprEdiListDef.JISSEKI_FLAG.ColNo).Value().ToString()
                ediCtlNo = .Cells(selectRow, g.sprEdiListDef.EDI_NO.ColNo).Value().ToString()

                If delKb = "1" Then
                    'EDI入荷(大)が削除データの場合エラー
                    Me.ShowMessage("E320", New String() {"削除済データ", "紐付け処理"})     'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                '未登録データチェック
                If (delKb = "0" OrElse delKb = "3" OrElse delKb = "2") AndAlso outF = "0" _
                    AndAlso (jissekiF = "0" OrElse jissekiF = "9") Then

                Else
                    Me.ShowMessage("E336", New String() {"未登録データ", "紐付け処理"})   'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For

                End If

                '---
                '入荷登録対象チェック
                If custUnsoF <> "1" Then
                Else
                    Me.ShowMessage("E320", New String() {"運送データ", "紐付け処理"})   'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                If delKb = "3" AndAlso custHoldF = "1" Then
                    '保留データかつ保留データ登録フラグが"1"の場合はワーニング
                    rtn = Me.ShowMessage("W169", New String() {"紐付け処理", ediCtlNo})
                    If rtn = MsgBoxResult.Ok Then
                    ElseIf rtn = MsgBoxResult.Cancel Then
                        errHt.Add(i, String.Empty)
                        Continue For
                    End If

                ElseIf delKb = "3" AndAlso custHoldF = "2" Then
                    'EDI取消するデータ
                    Me.ShowMessage("E376", New String() {"保留データ", "紐付け処理", "EDI取消"})   'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For

                ElseIf delKb = "1" OrElse delKb = "2" Then
                    Me.ShowMessage("E320", New String() {"削除データもしくはキャンセルデータ", "紐付け処理"})   'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For

                Else

                End If

                '---

                'EDI入荷(中)の件数が 0の場合エラー
                If mCount = "0" Then
                    Me.ShowMessage("E320", New String() {"中レコードが0件", "紐付け処理"})    'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                'EDI入荷(中)に赤データがある場合エラー
                If akakuroF = "1" Then
                    Me.ShowMessage("E320", New String() {"中データが赤データ", "紐付け処理"})    'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

            End With

        Next

        Return errHt
    End Function
#End Region

#End Region '入力チェック（紐付け）

#Region "EDI取消時チェック"

    ''' <summary>
    ''' EDI取消時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function DelEdiSingleCheck(ByVal g As LMH010G) As Boolean

        '【単項目チェック】
        '選択チェック
        If Me.IsSelectDataChk() = False Then
            Return False
        End If

        '自営業所チェック
        Dim rtnMsg As String = String.Empty
        rtnMsg = "EDI取消"

        If Me.IsNrsChk(rtnMsg, g) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' EDI取消時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Public Function DelEdiKanrenCheck(ByVal eventShubetsu As LMH010C.EventShubetsu, ByRef errDs As DataSet, ByVal g As LMH010G) As Hashtable

        Dim errHt As Hashtable = New Hashtable

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0

        Dim delKb As String = String.Empty
        Dim outF As String = String.Empty
        Dim jissekiF As String = String.Empty
        Dim ediCtlNo As String = String.Empty

        errDs = New LMH010DS
        Dim dr As DataRow

        For i As Integer = 0 To max

            With Me._Frm.sprEdiList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                delKb = .Cells(selectRow, g.sprEdiListDef.DEL_KB.ColNo).Value().ToString()
                outF = .Cells(selectRow, g.sprEdiListDef.OUT_FLAG.ColNo).Value().ToString()
                jissekiF = .Cells(selectRow, g.sprEdiListDef.JISSEKI_FLAG.ColNo).Value().ToString()
                ediCtlNo = .Cells(selectRow, g.sprEdiListDef.EDI_NO.ColNo).Value().ToString()

                If delKb = "1" Then
                    'EDI入荷(大)が削除データの場合エラー

                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH010C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E320"
                    dr("PARA1") = "削除済データ"
                    dr("PARA2") = "EDI取消"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Add(dr)

                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                '未登録データチェック
                If (delKb = "0" OrElse delKb = "3" OrElse delKb = "2") AndAlso outF = "0" _
                    AndAlso (jissekiF = "0" OrElse jissekiF = "9") Then

                Else
                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH010C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E336"
                    dr("PARA1") = "未登録データ"
                    dr("PARA2") = "EDI取消"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Add(dr)

                    'Me.ShowMessage("E336", New String() {"未登録データ", "EDI取消"})   'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For

                End If


            End With

        Next

        Return errHt

    End Function

#End Region 'EDI取消時チェック

#Region "実績取消時チェック"

    ''' <summary>
    ''' 実績取消時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function JissekiTorikesiSingleCheck(ByVal g As LMH010G) As Boolean

        With Me._Frm

            '【単項目チェック】
            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            '自営業所チェック
            Dim rtnMsg As String = String.Empty
            rtnMsg = "実績取消"

            If Me.IsNrsChk(rtnMsg, g) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 実績取消時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Public Function JissekiTorikesiKanrenCheck(ByVal eventShubetsu As LMH010C.EventShubetsu, ByRef errDs As DataSet, ByVal g As LMH010G) As Hashtable

        Dim errHt As Hashtable = New Hashtable

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0

        Dim jissekiF As String = String.Empty
        Dim custJissekiF As String = String.Empty
        Dim outF As String = String.Empty
        Dim ediDelF As String = String.Empty
        Dim inkaDelF As String = String.Empty
        Dim stateKb As String = String.Empty
        Dim ediCtlNo As String = String.Empty

        errDs = New LMH010DS
        Dim dr As DataRow

        For i As Integer = 0 To max

            With Me._Frm.sprEdiList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                jissekiF = .Cells(selectRow, g.sprEdiListDef.JISSEKI_FLAG.ColNo).Value().ToString()
                custJissekiF = .Cells(selectRow, g.sprEdiListDef.EDI_CUST_JISSEKI.ColNo).Value().ToString()
                outF = .Cells(selectRow, g.sprEdiListDef.OUT_FLAG.ColNo).Value().ToString()
                ediDelF = .Cells(selectRow, g.sprEdiListDef.SYS_DEL_FLG.ColNo).Value().ToString()
                inkaDelF = .Cells(selectRow, g.sprEdiListDef.INKA_DEL_FLG.ColNo).Value().ToString()
                stateKb = .Cells(selectRow, g.sprEdiListDef.INKA_STATE_KB.ColNo).Value().ToString()
                ediCtlNo = .Cells(selectRow, g.sprEdiListDef.EDI_NO.ColNo).Value().ToString()

                If jissekiF = "0" Then

                Else
                    'EDI入荷(大)の実績書込Fが実績未でない場合エラー

                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH010C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E336"
                    dr("PARA1") = "実績未データ"
                    dr("PARA2") = "実績取消"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Add(dr)

                    'Me.ShowMessage("E336", New String() {"実績未データ", "実績取消"})     'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

                Dim tempStateKb As Integer = 0
                If String.IsNullOrEmpty(stateKb) = False Then
                    tempStateKb = Convert.ToInt32(stateKb)
                End If

                If (custJissekiF = "1" OrElse custJissekiF = "2" OrElse custJissekiF = "3") AndAlso _
                   ediDelF = "0" AndAlso inkaDelF = "0" AndAlso tempStateKb >= 50 Then

                ElseIf custJissekiF = "2" AndAlso (ediDelF = "1" OrElse inkaDelF = "1") Then

                ElseIf custJissekiF = "3" AndAlso (ediDelF = "0" OrElse outF = "2") Then

                ElseIf custJissekiF = "4" AndAlso (ediDelF = "0" OrElse inkaDelF = "0") Then

                Else

                    'エラーがある場合、DataTableに設定
                    dr = errDs.Tables(LMH010C.TABLE_NM_GUIERROR).NewRow()
                    dr("GUIDANCE_ID") = LMHControlC.GUIDANCE_KBN
                    dr("MESSAGE_ID") = "E336"
                    dr("PARA1") = "実績未データ"
                    dr("PARA2") = "実績取消"
                    dr("KEY_NM") = LMHControlC.EXCEL_COLTITLE
                    dr("KEY_VALUE") = ediCtlNo
                    dr("ROW_NO") = selectRow.ToString()
                    errDs.Tables(LMH010C.TABLE_NM_GUIERROR).Rows.Add(dr)

                    'Me.ShowMessage("E336", New String() {"実績未データ", "実績取消"})   'エラーメッセージ
                    errHt.Add(i, String.Empty)
                    Continue For
                End If

            End With

        Next

        Return errHt

    End Function

#End Region '実績取消時チェック

#Region "実行時チェック"

    ''' <summary>
    ''' 実行時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function JikkouSingleCheck(ByVal g As LMH010G) As Boolean

        With Me._Frm

            '【単項目チェック】
            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            If Me.IsSelectOneChk() = False Then
                Return False
            End If

            '自営業所チェック
            Dim rtnMsg As String = String.Empty
            rtnMsg = "実行処理"

            If Me.IsNrsChk(rtnMsg, g) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 実行時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Public Function JikkouKanrenCheck(ByVal eventshubetsu As LMH010C.EventShubetsu, ByVal g As LMH010G) As Hashtable

        Dim errHt As Hashtable = New Hashtable

        '2012.02.25 大阪対応 START
        '続行確認
        Dim rtn As MsgBoxResult
        '2012.02.25 大阪対応 END

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0

        Dim delF As String = String.Empty
        Dim jissekiF As String = String.Empty
        Dim freeC30 As String = String.Empty
        Dim inkaDelF As String = String.Empty
        '2012.02.25 大阪対応 START
        Dim matomeNo As String = String.Empty
        '2012.02.25 大阪対応 END

        For i As Integer = 0 To max

            With Me._Frm.sprEdiList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                delF = .Cells(selectRow, g.sprEdiListDef.SYS_DEL_FLG.ColNo).Value().ToString()
                jissekiF = .Cells(selectRow, g.sprEdiListDef.JISSEKI_FLAG.ColNo).Value().ToString()
                freeC30 = .Cells(selectRow, g.sprEdiListDef.FREE_C30.ColNo).Value().ToString()
                inkaDelF = .Cells(selectRow, g.sprEdiListDef.INKA_DEL_FLG.ColNo).Value().ToString()
                '2012.02.25 大阪対応 START
                If String.IsNullOrEmpty(.Cells(selectRow, g.sprEdiListDef.MATOME_NO.ColNo).Value().ToString()) = False Then
                    matomeNo = .Cells(selectRow, g.sprEdiListDef.MATOME_NO.ColNo).Value().ToString().Substring(1, 8)
                End If
                '2012.02.25 大阪対応 END

                Select Case eventshubetsu

                    Case LMH010C.EventShubetsu.JIKKOU_TORIKESI_MITOUROKU

                        If delF = "1" Then

                        Else
                            'EDI入荷(大)が削除データ以外の場合エラー
                            Me.ShowMessage("E336", New String() {"削除データ", "EDI取消⇒未登録"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If


                    Case LMH010C.EventShubetsu.JIKKOU_SAKUSEI_JISSEKIMI

                        If jissekiF = "1" Then

                        Else
                            'EDI入荷(大)の実績書込Fが実績作成でない場合エラー
                            Me.ShowMessage("E336", New String() {"実績作成済データ", "実績作成済⇒実績未"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                    Case LMH010C.EventShubetsu.JIKKOU_SOUSIN_SOUSINMI


                        If jissekiF = "2" Then

                        Else
                            'EDI入荷(大)の実績書込Fが実績送信でない場合エラー
                            Me.ShowMessage("E336", New String() {"実績送信済データ", "実績送信済⇒実績送信待"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                    Case LMH010C.EventShubetsu.JIKKOU_SOUSIN_JISSEKIMI

                        If jissekiF = "2" Then

                        Else
                            'EDI入荷(大)の実績書込Fが実績送信でない場合エラー
                            Me.ShowMessage("E336", New String() {"実績送信済データ", "実績送信済⇒実績未"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                    Case LMH010C.EventShubetsu.JIKKOU_HOUKOKU_EDI_TORIKESI

                        If delF = "0" Then

                        Else
                            'EDI入荷(大)が削除データの場合エラー
                            Me.ShowMessage("E320", New String() {"削除データ", "実績報告用EDIデータ取消"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        If String.IsNullOrEmpty(freeC30) = False AndAlso freeC30.Substring(0, 2).Equals("01") = True Then

                        Else
                            Me.ShowMessage("E336", New String() {"入荷データから実績報告用に作成されたデータ", "実績報告用EDIデータ取消"})
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                    Case LMH010C.EventShubetsu.JIKKOU_TOUROKU_MITOUROKU

                        If delF = "0" Then

                        Else
                            'EDI入荷(大)が削除データの場合エラー
                            Me.ShowMessage("E320", New String() {"削除データ", "入荷取消⇒未登録"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        If inkaDelF = "1" Then

                        Else
                            '入荷(大)が削除データ以外の場合エラー
                            Me.ShowMessage("E336", New String() {"入荷取消データ", "入荷取消⇒未登録"})    'エラーメッセージ
                            errHt.Add(i, String.Empty)
                            Continue For
                        End If

                        If String.IsNullOrEmpty(matomeNo) = False AndAlso matomeNo.Equals("0000000") = False Then

                            'まとめデータの場合はワーニング
                            rtn = Me.ShowMessage("W163", New String() {"入荷取消⇒未登録"})
                            If rtn = MsgBoxResult.Ok Then
                            ElseIf rtn = MsgBoxResult.Cancel Then
                                errHt.Add(i, String.Empty)
                                Continue For
                            End If

                        End If




                End Select

            End With

        Next

        Return errHt

    End Function

    'ADD START 2019/9/12 依頼番号:007111
    ''' <summary>
    ''' 現品票印刷時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function JikkouGenpinPrint(ByVal g As LMH010G, ByVal eventShubetsu As LMH010C.EventShubetsu) As Boolean

        With Me._Frm

            'ヘッダ項目のスペース除去
            Call Me.TrimHeaderSpaceTextValue()

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
            .txtCustCD_L.IsHissuCheck() = True
            .txtCustCD_L.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCD_L) = False Then
                Return False
            End If

            '荷主コード(中)
            .txtCustCD_M.ItemName() = "荷主コード(中)"
            .txtCustCD_M.IsForbiddenWordsCheck() = True
            .txtCustCD_M.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCD_M) = False Then
                Return False
            End If


            Select Case eventShubetsu

                Case LMH010C.EventShubetsu.JIKKOU_GENPIN_PRINT
                    '選択有無チェック
                    If Me.IsSelectDataChk() = False Then
                        Return False
                    End If

                Case LMH010C.EventShubetsu.JIKKOU_GENPIN_REPRINT
                    '単一選択チェック(選択なしもOK)
                    If Me.IsSelectOneChk() = False Then
                        Return False
                    End If

            End Select


            Dim nrsBrCd As String
            Dim whCd As String

            If _ChkList.Count >= 1 Then
                nrsBrCd = Me._Vcon.GetCellValue(.sprEdiList.ActiveSheet.Cells(Convert.ToInt32(_ChkList(0)), g.sprEdiListDef.NRS_BR_CD.ColNo))
                whCd = Me._Vcon.GetCellValue(.sprEdiList.ActiveSheet.Cells(Convert.ToInt32(_ChkList(0)), g.sprEdiListDef.WH_CD.ColNo))
            Else
                nrsBrCd = .cmbEigyo.SelectedValue.ToString
                whCd = .cmbWare.SelectedValue.ToString
            End If

            '営業所、倉庫チェック
#If False Then  'UPD 2022/11/09 033380   【LMS】FFEM足柄工場LMS導入
            If (Not "96".Equals(nrsBrCd)) OrElse (Not "W01".Equals(whCd)) Then
#Else
#If False Then  'UPD 2023/12/25 039659【LMS・EDI・ハンディ】FFEM熊本工場 LMS新規導入に伴う新規構築
            If (((Not "96".Equals(nrsBrCd)) OrElse (Not "W01".Equals(whCd))) AndAlso
                    ((Not "F2".Equals(nrsBrCd)) OrElse ((Not "A60".Equals(whCd) _
                    AndAlso (Not "A61".Equals(whCd)) _
                    AndAlso (Not "A63".Equals(whCd)) _
                    AndAlso (Not "A70".Equals(whCd)) _
                    AndAlso (Not "A72".Equals(whCd)))))) Then
#Else
            Dim strWhere As String = String.Concat("KBN_GROUP_CD = 'F030'",
                                                    " AND KBN_NM4 = '", nrsBrCd, "'",
                                                    " AND KBN_NM5 = '", whCd, "'",
                                                    " AND KBN_NM7 = '1'",
                                                    " AND SYS_DEL_FLG = '0'")
            If (((Not "96".Equals(nrsBrCd)) OrElse (MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(strWhere).Count = 0)) AndAlso
                ((Not "F2".Equals(nrsBrCd)) OrElse (MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(strWhere).Count = 0)) AndAlso
                ((Not "F3".Equals(nrsBrCd)) OrElse (MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(strWhere).Count = 0))) Then
#End If
#End If

                MyBase.ShowMessage("E209", New String() {""})
                Return False
            End If

        End With

        Return True

    End Function
    'ADD END 2019/9/12 依頼番号:007111

#If True Then   'ADD 2019/12/17
    ''' <summary>
    ''' 現品票印刷時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function GenpinPrintInDataCHK(ByVal g As LMH010G, ByVal eventShubetsu As LMH010C.EventShubetsu) As Boolean

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()
        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0
        Dim GENPINHYO_CHKFLG As String = String.Empty
        Dim ORDER_NO As String = String.Empty

        For i As Integer = 0 To max

            With Me._Frm.sprEdiList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))

                GENPINHYO_CHKFLG = .Cells(selectRow, g.sprEdiListDef.GENPINHYO_CHKFLG.ColNo).Value().ToString()

                If ("OK").Equals(GENPINHYO_CHKFLG.ToString.Trim) = False Then

                    ORDER_NO = .Cells(selectRow, g.sprEdiListDef.ORDER_NO.ColNo).Value().ToString()

                    MyBase.ShowMessage("E175", New String() {String.Concat("オーダー番号", ORDER_NO, " 入庫数量が入れ目=管理単位の倍数になっていないため現品票")})
                    Return False

                End If


            End With
        Next

        Return True

    End Function

#End If

#End Region '実行時チェック

#Region "印刷ボタン押下時チェック"
    ''' <summary>
    ''' 実行イベントの入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function printSingleCheck(ByVal eventShubetsu As Integer, _
                                     ByVal printShubetsu As Integer) As Boolean

        With Me._Frm
            '【単項目チェック】
            '******************** ヘッダ項目の入力チェック ********************

            '実行種別
            .cmbPrint.ItemName() = "印刷種別"
            .cmbPrint.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbPrint) = False Then
                Return False
            End If

            '************************************************************
            Select Case eventShubetsu

                Case Else

            End Select

        End With

        Return True

    End Function

#End Region

    '2015.09.03 tsunehira add
#Region "変更ボタン押下時チェック"
    ''' <summary>
    ''' 変更イベントの入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function chgSingleCheck(ByVal eventShubetsu As Integer, _
                                     ByVal chgShubetsu As Integer) As Boolean

        With Me._Frm

            '【単項目チェック】
            '******************** ヘッダ項目の入力チェック ********************
            '変更種別
            .cmbChg.ItemName() = "変更種別"
            .cmbChg.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbChg) = False Then
                MyBase.ShowMessage("E009")
                Return False
            End If
            '************************************************************
            Select Case chgShubetsu

                Case Else

            End Select

            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            If Me.IsSelectOneChk() = False Then
                Return False
            End If

        End With

        Return True

    End Function


#End Region

    '2012.03.13 大阪対応START
#Region "入力チェック(出力)※(CSV作成・印刷)"

    Friend Function IsOutputPrintCheck(ByVal outPutKb As String, ByVal outPutKb2 As String) As Boolean

        With Me._Frm

            Dim errorCommentFrom As String = String.Concat(.lblTitlePrintDate.Text.ToString(), "From")
            Dim errorCommentTo As String = String.Concat(.lblTitlePrintDate.Text.ToString(), "To")

            '印刷種別
            .cmbOutput.ItemName() = "CSV作成・印刷種別"
            .cmbOutput.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbOutput) = False Then
                Return False
            End If

            Select Case outPutKb

                Case LMH010C.JYUSIN_PRT, _
                    LMH010C.MISOUTYAKU_FILE_MAKE '未着・早着ファイル作成対応 

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

                Case Else

            End Select

            Dim ediCustDrs As DataRow()
            Dim inOutKb As String = "1"                                     '入出荷区分("1"(入荷))
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

                Case LMH010C.JYUSIN_PRT '受信帳票

                    '■■■ダウケミのみ印刷可能（暫定対応）Start　■■■
                    Select Case ediCustIdx
                        Case "17", "18", "13"    'ダウケミ,ダウケミ(高石) 2012-11-02日産物流(千葉)"13"追加

                        Case Else
                            MyBase.ShowMessage("E454", New String() {"印刷が許可されていない荷主", "印刷", ""})
                            Return False
                    End Select
                    '■■■ダウケミのみ印刷可能（暫定対応）End　■■■

                    '要望番号1007 2012.05.08 追加START
                    Select Case outPutKb2
                        Case LMH010C.OUTPUT_SUMI
                            '選択チェック
                            If IsSelectDataChk() = False Then
                                Return False
                            End If

                        Case Else

                    End Select
                    '要望番号1007 2012.05.08 追加END

                Case LMH010C.JYUSIN_ICHIRAN '受信一覧表

                    Select Case ediCustIdx
                        Case "38", "1"     '浮間合成(大阪、岩槻)

                        Case Else
                            MyBase.ShowMessage("E454", New String() {"印刷が許可されていない荷主", "印刷", ""})
                            Return False
                    End Select

                    '要望番号1007 2012.05.08 追加START
                    Select Case outPutKb2
                        Case LMH010C.OUTPUT_SUMI
                            '選択チェック
                            If IsSelectDataChk() = False Then
                                Return False
                            End If

                        Case Else

                    End Select
                    '要望番号1007 2012.05.08 追加END

                    '未着・早着ファイル作成対応 Start
                Case LMH010C.MISOUTYAKU_FILE_MAKE '未着・早着ファイル作成

                    Select Case ediCustIdx
                        Case "42"     'ユーティーアイ(千葉)

                        Case Else
                            MyBase.ShowMessage("E454", New String() {"対象外の荷主", "未着・早着ファイル作成は", ""})
                            Return False
                    End Select
                    '未着・装着ファイル作成対応 End

                    'EDI取込日
                    .imdOutputDateFrom.ItemName() = "EDI取込日"
                    .imdOutputDateFrom.IsHissuCheck() = True
                    If MyBase.IsValidateCheck(.imdOutputDateFrom) = False Then
                        Return False
                    End If
                    '未着・早着ファイル作成対応 End

                Case Else


            End Select

            '未着・早着ファイル作成対応 Start
            If outPutKb.Equals(LMH010C.MISOUTYAKU_FILE_MAKE) = True Then
                '未着・早着ファイル作成の場合は、TO-FROMのチェックを行わない
            Else
                '要望番号1007 2012.05.08 修正START
                Select Case outPutKb2

                    Case LMH010C.OUTPUT_SUMI

                    Case Else

                        '出荷日FROM or EDI取込日FROM
                        If .imdOutputDateFrom.IsDateFullByteCheck(8) = False Then
                            MyBase.ShowMessage("E038", New String() {errorCommentFrom, "8"})
                            Return False
                        End If

                        '出荷日TO or EDI取込日TO
                        If .imdOutputDateTo.IsDateFullByteCheck(8) = False Then
                            MyBase.ShowMessage("E038", New String() {errorCommentTo, "8"})
                            Return False
                        End If

                        '出荷日大小 or EDI取込日大小
                        If String.IsNullOrEmpty(.imdOutputDateFrom.TextValue) = False AndAlso String.IsNullOrEmpty(.imdOutputDateTo.TextValue) = False Then
                            If Convert.ToInt32(.imdOutputDateTo.TextValue) < Convert.ToInt32(.imdOutputDateFrom.TextValue) Then
                                Me.ShowMessage("E039", New String() {errorCommentTo, errorCommentFrom})
                                .imdOutputDateFrom.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                                .imdOutputDateTo.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                                .imdOutputDateFrom.Focus()
                                Return False
                            End If

                        End If

                End Select

            End If
            '未着・早着ファイル作成対応 End

        End With

        Return True

    End Function

#End Region
    '2012.03.13 大阪対応END

    '2012.09.11 富士フイルム対応START
#Region "入力チェック(分析票取込)"

    ''' <summary>
    ''' 入力チェック(分析票取込)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsCoaTourokuCheck() As Boolean

        With Me._Frm

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
            .txtCustCD_L.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCD_L) = False Then
                Return False
            End If

            '荷主コード(中)
            .txtCustCD_M.ItemName() = "荷主コード(中)"
            .txtCustCD_M.IsHissuCheck() = True
            .txtCustCD_M.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCD_M) = False Then
                Return False
            End If

            Dim ediCustDrs As DataRow()
            Dim inOutKb As String = "1"                                     '入出荷区分("1"(入荷))
            Dim brCd As String = .cmbEigyo.SelectedValue.ToString()         '営業所コード
            Dim whCd As String = .cmbWare.SelectedValue.ToString()          '倉庫コード
            Dim custCdL As String = .txtCustCD_L.TextValue.ToString()   '荷主コード(大)
            Dim custCdM As String = .txtCustCD_M.TextValue.ToString()   '荷主コード(中)
            'EDI対象荷主マスタの荷主のINDEXの取得(キャッシュ)
            ediCustDrs = Me._Vcon.SelectEdiCustListDataRow(inOutKb, brCd, whCd, custCdL, custCdM)
            If 0 = ediCustDrs.Length Then
                MyBase.ShowMessage("E361")
                Return False
            End If

            '分析票取込対象荷主フラグ(EDI対象荷主Mのフラグ15)が"0"(対象外荷主)の場合はエラー
            If ediCustDrs(0)("FLAG_15").ToString().Equals("0") = True Then
                'If ediCustDrs(0)("EDI_CUST_INDEX").ToString().Equals("40") = False Then
                MyBase.ShowMessage("E454", New String() {"対象外の荷主", "分析票の取込は", ""})
                Return False
            End If

        End With

        Return True

    End Function

#End Region
    '2012.03.13 大阪対応END

    '2012.11.30 ユーティアイ対応START
#Region "入力チェック(入荷確認ファイル取込)"

    ''' <summary>
    ''' 入力チェック(入荷確認ファイル取込)"
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsInkaConfTourokuCheck() As Boolean

        With Me._Frm

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
            .txtCustCD_L.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCD_L) = False Then
                Return False
            End If

            '荷主コード(中)
            .txtCustCD_M.ItemName() = "荷主コード(中)"
            .txtCustCD_M.IsHissuCheck() = True
            .txtCustCD_M.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCD_M) = False Then
                Return False
            End If

            Dim ediCustDrs As DataRow()
            Dim inOutKb As String = "1"                                     '入出荷区分("1"(入荷))
            Dim brCd As String = .cmbEigyo.SelectedValue.ToString()         '営業所コード
            Dim whCd As String = .cmbWare.SelectedValue.ToString()          '倉庫コード
            Dim custCdL As String = .txtCustCD_L.TextValue.ToString()       '荷主コード(大)
            Dim custCdM As String = .txtCustCD_M.TextValue.ToString()       '荷主コード(中)
            'EDI対象荷主マスタの荷主のINDEXの取得(キャッシュ)
            ediCustDrs = Me._Vcon.SelectEdiCustListDataRow(inOutKb, brCd, whCd, custCdL, custCdM)
            If 0 = ediCustDrs.Length Then
                MyBase.ShowMessage("E361")
                Return False
            End If

            'EDI対象荷主MのEDI_CUST_INDEXが"42"(ユーティーアイ)以外の場合はエラー
            If ediCustDrs(0)("EDI_CUST_INDEX").ToString().Equals("42") = False Then
                MyBase.ShowMessage("E454", New String() {"対象外の荷主", "入荷確認ファイルの取込は", ""})
                Return False
            End If

        End With

        Return True

    End Function

#End Region
    '2012.11.30 ユーティアイ対応END

    '2012.12.07 ユーティアイ対応START

#Region "入力チェック(荷主変更)"

    ''' <summary>
    ''' 入力チェック(荷主変更)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsChgCheck(ByVal g As LMH010G) As Boolean

        With Me._Frm

            ''営業所
            '.cmbChg.ItemName() = "変更種別"
            '.cmbChg.IsHissuCheck() = True
            'If MyBase.IsValidateCheck(.cmbChg) = False Then
            '    Return False
            'End If
            '選択チェック
            If Me.IsSelectDataChk() = False Then
                Return False
            End If

            Dim rtnMsg As String = String.Empty
            rtnMsg = "変更処理"

            If Me.IsNrsChk(rtnMsg, g) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#End Region



#Region "入力チェック(UTI確認データ削除)"

    ''' <summary>
    ''' 入力チェック(UTI確認データ削除)"
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsInkaConfDelCheck() As Boolean

        With Me._Frm

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
            .txtCustCD_L.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCD_L) = False Then
                Return False
            End If

            '荷主コード(中)
            .txtCustCD_M.ItemName() = "荷主コード(中)"
            .txtCustCD_M.IsHissuCheck() = True
            .txtCustCD_M.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCD_M) = False Then
                Return False
            End If

            Dim ediCustDrs As DataRow()
            Dim inOutKb As String = "1"                                     '入出荷区分("1"(入荷))
            Dim brCd As String = .cmbEigyo.SelectedValue.ToString()         '営業所コード
            Dim whCd As String = .cmbWare.SelectedValue.ToString()          '倉庫コード
            Dim custCdL As String = .txtCustCD_L.TextValue.ToString()       '荷主コード(大)
            Dim custCdM As String = .txtCustCD_M.TextValue.ToString()       '荷主コード(中)
            'EDI対象荷主マスタの荷主のINDEXの取得(キャッシュ)
            ediCustDrs = Me._Vcon.SelectEdiCustListDataRow(inOutKb, brCd, whCd, custCdL, custCdM)
            If 0 = ediCustDrs.Length Then
                MyBase.ShowMessage("E361")
                Return False
            End If

            'EDI対象荷主MのEDI_CUST_INDEXが"42"(ユーティーアイ)以外の場合はエラー
            If ediCustDrs(0)("EDI_CUST_INDEX").ToString().Equals("42") = False Then
                MyBase.ShowMessage("E454", New String() {"対象外の荷主", "UTI確認データ削除画面への遷移は", ""})
                Return False
            End If

        End With

        Return True

    End Function

#End Region
    '2012.12.07 ユーティアイ対応END


#Region "マスタ参照（時チェック)"

#Region "単項目チェック"
    ''' <summary>
    ''' マスタ参照（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsPopSingleCheck(ByVal objNM As String) As Boolean

        With Me._Frm

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


            End Select

            Return True

        End With

        Return True

    End Function
#End Region

#End Region

#Region "選択行取得"
    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMH010C.SprColumnIndex.DEF

        Return Me._Vcon.SprSelectList(defNo, Me._Frm.sprEdiList)

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


#Region "単一選択チェック"
    ''' <summary>
    ''' 単一選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSelectOneChk() As Boolean

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
#End Region

#Region "自営業所チェック"
    ''' <summary>
    ''' 自営業所チェック
    ''' </summary>
    ''' <returns>True:エラーなし, False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsNrsChk(ByVal rtnMsg As String, ByVal g As LMH010G) As Boolean

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()

        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0
        Dim nrsBrCd As String = String.Empty

        For i As Integer = 0 To max
            With Me._Frm.sprEdiList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                nrsBrCd = .Cells(selectRow, g.sprEdiListDef.NRS_BR_CD.ColNo).Value().ToString()

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

#Region "まとめ可能かチェック"
    ''' <summary>
    ''' 入荷日・荷主CDチェック ADD 2018/04/27
    ''' </summary>
    ''' <returns>True:エラーなし, False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsMatomeChk(ByVal g As LMH010G) As Boolean

        'チェックされた行番号取得
        Me._ChkList = New ArrayList()
        Me._ChkList = Me.getCheckList()

        Dim max As Integer = Me._ChkList.Count() - 1
        Dim selectRow As Integer = 0
        Dim INKA_DATE As String = String.Empty
        Dim CUST_CDL As String = String.Empty
        Dim CUST_CDM As String = String.Empty
        Dim STATUS_NM As String = String.Empty

        For i As Integer = 0 To max
            With Me._Frm.sprEdiList.ActiveSheet

                selectRow = Convert.ToInt32(Me._ChkList(i))
                '最初のデータと同じかチェックする（入荷日、ステータス名,荷主CDL,荷主CDM）
                INKA_DATE = CStr(IIf(INKA_DATE = String.Empty, .Cells(selectRow, g.sprEdiListDef.INKA_DATE.ColNo).Value().ToString(), INKA_DATE))
                CUST_CDL = CStr(IIf(CUST_CDL = String.Empty, .Cells(selectRow, g.sprEdiListDef.CUST_CD_L.ColNo).Value().ToString(), CUST_CDL))
                CUST_CDM = CStr(IIf(CUST_CDM = String.Empty, .Cells(selectRow, g.sprEdiListDef.CUST_CD_M.ColNo).Value().ToString(), CUST_CDM))
                STATUS_NM = CStr(IIf(STATUS_NM = String.Empty, .Cells(selectRow, g.sprEdiListDef.STATUS_NM.ColNo).Value().ToString(), STATUS_NM))

                If INKA_DATE <> CStr(.Cells(selectRow, g.sprEdiListDef.INKA_DATE.ColNo).Value().ToString()) _
                    OrElse STATUS_NM <> CStr(.Cells(selectRow, g.sprEdiListDef.STATUS_NM.ColNo).Value().ToString()) _
                    OrElse CUST_CDL <> CStr(.Cells(selectRow, g.sprEdiListDef.CUST_CD_L.ColNo).Value().ToString()) _
                    OrElse CUST_CDM <> CStr(.Cells(selectRow, g.sprEdiListDef.CUST_CD_M.ColNo).Value().ToString()) Then

                    Return False
                End If

            End With
        Next

        Return True

    End Function
#End Region

#End Region 'Method

End Class
