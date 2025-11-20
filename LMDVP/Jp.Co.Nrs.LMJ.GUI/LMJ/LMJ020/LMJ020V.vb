' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMJ     : ｼｽﾃﾑ管理
'  プログラムID     :  LMJ020V : 未使用荷主データ退避
'  作  成  者       :  s.kobayashi
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMJ020Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMJ020V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMJ020F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMJControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMJ020F, ByVal v As LMJControlV)

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
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMJ020C.ActionType) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMJ020C.ActionType.KENSAKU        '検索
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

            Case LMJ020C.ActionType.CLOSE           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMJ020C.ActionType.JIKKOU_ESCAPE, LMJ020C.ActionType.JIKKOU_MODOSHI           '実行
                '10:閲覧者、50:外部の場合エラー
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
    ''' 検索時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKensakuSingleCheck() As Boolean


        With Me._Frm

            '【単項目チェック】

            '******************** ヘッダ項目の入力チェック ********************
            '処理区分
            .cmbShori.ItemName() = "処理区分"
            .cmbShori.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbShori) = False Then
                Return False
            End If

            '営業所
            .cmbEigyo.ItemName() = "営業所"
            .cmbEigyo.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                Return False
            End If

            '入荷日From
            .imdLastUpdDate.IsHissuCheck() = True
            If .imdLastUpdDate.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E038", New String() {"最終更新日", "8"})
                Return False
            End If

            '******************** スプレッド項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            '荷主コード
            vCell.SetValidateCell(0, LMJ020G.sprDetailDef.CUST_CD_L.ColNo)
            vCell.ItemName() = "荷主コード"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 5
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名
            vCell.SetValidateCell(0, LMJ020G.sprDetailDef.CUST_NM_L.ColNo)
            vCell.ItemName() = "荷主名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '担当者
            vCell.SetValidateCell(0, LMJ020G.sprDetailDef.TANTO_USER.ColNo)
            vCell.ItemName() = "担当者"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '退避実行者
            vCell.SetValidateCell(0, LMJ020G.sprDetailDef.TAIHI_USER.ColNo)
            vCell.ItemName() = "退避実行者"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 退避実行時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsJikkouEscapeSingleCheck() As Boolean


        With Me._Frm

            '【単項目チェック】

        End With

        Return True

    End Function

    ''' <summary>
    ''' 戻し実行時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsJikkouModoshiSingleCheck() As Boolean


        With Me._Frm

            '【単項目チェック】

        End With

        Return True

    End Function

#End Region 'Method

End Class
