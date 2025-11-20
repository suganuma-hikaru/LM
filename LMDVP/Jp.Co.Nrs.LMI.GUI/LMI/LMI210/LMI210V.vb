' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI210  : ハネウェル管理
'  作  成  者       :  [KIM]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports FarPoint.Win.Spread
Imports System.IO

''' <summary>
''' LMI210Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI210V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI210F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMIControlV

    Private _G As LMI210G

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI210F, ByVal v As LMIControlV, ByVal g As LMI210G)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

        Me._G = g

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
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMI210C.EventShubetsu) As Boolean

        '【単項目チェック】

        Select Case eventShubetsu

            Case LMI210C.EventShubetsu.KENSAKU '検索
                Return Me.IsSearchSingleCheck()

        End Select

        Return True

    End Function

    ''' <summary>
    ''' 検索処理時の単項目チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSearchSingleCheck() As Boolean

        With Me._Frm

            '2013.08.15 要望番号2095 START
            '冷媒商品
            .cmbCoolantGoodsKb.ItemName() = .lblTitleCoolantGoods.TextValue
            .cmbCoolantGoodsKb.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbCoolantGoodsKb) = False Then
                Return False
            End If
            '2013.08.15 要望番号2095 END

            '入荷
            If .optInka.Checked = True Then
                '入荷日From
                .imdInkaDateFrom.ItemName() = "入荷日From"
                .imdInkaDateFrom.IsHissuCheck = True
                If MyBase.IsValidateCheck(.imdInkaDateFrom) = False Then
                    Return False
                End If

                '入荷日To
                .imdInkaDateTo.ItemName() = "入荷日To"
                .imdInkaDateTo.IsHissuCheck = True
                If MyBase.IsValidateCheck(.imdInkaDateTo) = False Then
                    Return False
                End If

                '遅延制度開始日
                .imdBaseDate.ItemName() = "遅延制度開始日"
                .imdBaseDate.IsHissuCheck = True
                If MyBase.IsValidateCheck(.imdBaseDate) = False Then
                    Return False
                End If

            End If
            '出荷
            If .optOutka.Checked = True Then

                '出荷日From
                .imdOutkaDateFrom.ItemName() = "出荷日From"
                .imdOutkaDateFrom.IsHissuCheck = True
                If MyBase.IsValidateCheck(.imdOutkaDateFrom) = False Then
                    Return False
                End If
                'If .imdInkaDateFrom.IsDateFullByteCheck(8) = False Then
                '    MyBase.ShowMessage("E012", New String() {"出荷日From", "8"})
                '    Return False
                'End If

                '出荷日To
                .imdOutkaDateTo.ItemName() = "出荷日To"
                .imdOutkaDateTo.IsHissuCheck = True
                If MyBase.IsValidateCheck(.imdOutkaDateTo) = False Then
                    Return False
                End If
                'If .imdInkaDateTo.IsDateFullByteCheck(8) = False Then
                '    MyBase.ShowMessage("E012", New String() {"出荷日To", "8"})
                '    Return False
                'End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連項目入力チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMI210C.EventShubetsu) As Boolean

        '【関連項目チェック】
        Select Case eventShubetsu

            Case LMI210C.EventShubetsu.KENSAKU '検索
                Return Me.IsSearchKanrenCheck()

            Case LMI210C.EventShubetsu.EXCEL   'EXCEL出力
                Return Me.IsExcelKanrenCheck()

        End Select

        Return True

    End Function

    ''' <summary>
    ''' 検索処理時の関連チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSearchKanrenCheck() As Boolean

        With Me._Frm
            '入荷
            If .optInka.Checked = True Then
                If String.IsNullOrEmpty(.imdInkaDateFrom.TextValue) = False AndAlso _
                   String.IsNullOrEmpty(.imdInkaDateTo.TextValue) = False Then
                    '大小チェック（入荷日）
                    If .imdInkaDateFrom.TextValue > .imdInkaDateTo.TextValue Then
                        MyBase.ShowMessage("E039", New String() {"入荷日To", "入荷From"})
                        .imdInkaDateFrom.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        .imdInkaDateTo.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        .imdInkaDateFrom.Focus()
                        .imdInkaDateFrom.Select()
                        Return False
                    End If
                End If
            End If
            '出荷
            If .optOutka.Checked = True Then
                If String.IsNullOrEmpty(.imdOutkaDateFrom.TextValue) = False AndAlso _
                   String.IsNullOrEmpty(.imdOutkaDateTo.TextValue) = False Then
                    '大小チェック（入荷日）
                    If .imdOutkaDateFrom.TextValue > .imdOutkaDateTo.TextValue Then
                        MyBase.ShowMessage("E039", New String() {"出荷日To", "出荷日From"})
                        .imdOutkaDateFrom.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        .imdOutkaDateTo.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        .imdOutkaDateFrom.Focus()
                        .imdOutkaDateTo.Select()
                        Return False
                    End If
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' EXCEL出力処理時の関連チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsExcelKanrenCheck() As Boolean

        If Me._Frm.sprDetails.ActiveSheet.Rows.Count = 0 Then
            MyBase.ShowMessage("E501", New String() {""})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI210C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI210C.EventShubetsu.KENSAKU, _
                 LMI210C.EventShubetsu.EXCEL, _
                 LMI210C.EventShubetsu.DOUBLECLICK

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

            Case LMI210C.EventShubetsu.CLOSE        '閉じる
                'すべての権限許可
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
                        kengenFlg = True
                End Select

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        Return kengenFlg

    End Function

#End Region 'Method

End Class
