' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主
'  プログラムID     :  LMI150V : 物産アニマルヘルス倉庫内処理編集
'  作  成  者       :  [HORI]
' ==========================================================================

Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI150Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI150V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI150F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMIControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI150F, ByVal v As LMIControlV, ByVal g As LMI150G)

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
    ''' 単項目入力チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMI150C.EventShubetsu) As Boolean

        '【単項目チェック】
        With Me._Frm

            If LMI150C.EventShubetsu.HENSHU.Equals(eventShubetsu) Then
                '編集

                'ステータス
                If Not "0".Equals(.cmbJissekiShoriFlg.SelectedValue.ToString()) Then
                    Me._Vcon.SetErrMessage("E320", New String() {"実績送信済", "編集"})
                    Return False
                End If

            ElseIf LMI150C.EventShubetsu.HOZON.Equals(eventShubetsu) Then
                '保存

                '処理日
                .imdProcDate.ItemName = .lblProcDate.Text
                .imdProcDate.IsHissuCheck = True
                If MyBase.IsValidateCheck(.imdProcDate) = False Then
                    Return False
                End If

                '出荷倉庫種類
                .cmbOutkaWhType.ItemName = .lblOutkaWhType.Text
                .cmbOutkaWhType.IsHissuCheck = True
                If MyBase.IsValidateCheck(.cmbOutkaWhType) = False Then
                    Return False
                End If

                '変更後商品ランク
                .cmbAfterGoodsRank.ItemName = .lblAfterGoodsRank.Text
                .cmbAfterGoodsRank.IsHissuCheck = True
                If MyBase.IsValidateCheck(.cmbAfterGoodsRank) = False Then
                    Return False
                End If

                '顧客商品CD
                .txtGoodsCd.ItemName = .lblGoodsCd.Text
                .txtGoodsCd.IsHissuCheck = True
                If MyBase.IsValidateCheck(.txtGoodsCd) = False Then
                    Return False
                End If

                '個数
                .numNb.ItemName = .lblNb.Text
                .numNb.IsHissuCheck = True
                If MyBase.IsValidateCheck(.numNb) = False Then
                    Return False
                End If

                If Convert.ToInt32(.numNb.Value) = 0 Then
                    Me._Vcon.SetErrMessage("E001", New String() { .lblNb.Text})
                    Return False
                End If

            ElseIf LMI150C.EventShubetsu.ZAIKO_SEL.Equals(eventShubetsu) Then
                '在庫選択

                '出荷倉庫種類
                .cmbOutkaWhType.ItemName = .lblOutkaWhType.Text
                .cmbOutkaWhType.IsHissuCheck = True
                If MyBase.IsValidateCheck(.cmbOutkaWhType) = False Then
                    Return False
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連項目入力チェック（エラー）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMI150C.EventShubetsu) As Boolean

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI150C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI150C.EventShubetsu.HENSHU
                '編集
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

            Case LMI150C.EventShubetsu.HOZON
                '保存
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

            Case Else
                'すべての権限許可
                kengenFlg = True
        End Select

        Return kengenFlg

    End Function

#End Region 'Method

End Class
