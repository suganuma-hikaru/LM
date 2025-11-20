' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : ＳＣＭ
'  プログラムID     :  LMN080V : 欠品警告
'  作  成  者       :  [佐川央]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports FarPoint.Win.Spread

''' <summary>
''' LMN080Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMN080V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMN080F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMN080F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "検索"

    ''' <summary>
    ''' 検索押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuInputChk() As Boolean

        '単項目チェック
        If Me.IsKensakuSingleCheck() = False Then
            Return False
        End If

        '関連チェック
        If Me.IsKensakuRelationCheck() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 検索押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleCheck() As Boolean

        With Me._Frm

        End With

        Return True

    End Function

    ''' <summary>
    ''' 検索押下時関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuRelationCheck() As Boolean

        With Me._Frm

        End With

        Return True

    End Function

#End Region

#Region "ダブルクリック"

    ''' <summary>
    ''' ダブルクリック時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsDoubleClickInputChk(ByVal e As FarPoint.Win.Spread.CellClickEventArgs) As Boolean

        '単項目チェック
        If Me.IsDoubleClickSingleCheck(e) = False Then
            Return False
        End If

        '関連チェック
        If Me.IsDoubleClickRelationCheck(e) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' ダブルクリック時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDoubleClickSingleCheck(ByVal e As FarPoint.Win.Spread.CellClickEventArgs) As Boolean

        With Me._Frm

        End With

        Return True

    End Function

    ''' <summary>
    ''' ダブルクリック時関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDoubleClickRelationCheck(ByVal e As FarPoint.Win.Spread.CellClickEventArgs) As Boolean

        With Me._Frm.sprSokoDetail.ActiveSheet

            '欠品品目数取得
            Dim keppinNum As String = .Cells(e.Row, LMN080G.sprWareDetailDef.KEPPIN_HIN_NUM.ColNo).Text
            Dim preKeppinNum As String = .Cells(e.Row, LMN080G.sprWareDetailDef.KEPPINKIGU_HIN_NUM.ColNo).Text

            If (keppinNum = "0") And (preKeppinNum = "0") Then
                MyBase.ShowMessage("E079", New String() {"選択した倉庫", "欠品および欠品危惧品目"})
                Return False
            End If

        End With

        Return True

    End Function



#End Region

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMN080C.EventShubetsu) As Boolean

        Dim kengen As String = LMUserInfoManager.GetAuthoLv

        Select Case eventShubetsu

            '検索
            Case LMN080C.EventShubetsu.KENSAKU
                If kengen.Equals(LMConst.AuthoKBN.AGENT) = True Then  '50:外部の場合エラー
                    MyBase.ShowMessage("E016")
                    Return False
                End If

                'ダブルクリック
            Case LMN080C.EventShubetsu.DOUBLE_CLICK
                If kengen.Equals(LMConst.AuthoKBN.AGENT) = True Then  '50:外部の場合エラー
                    MyBase.ShowMessage("E016")
                    Return False
                End If

        End Select

        Return True

    End Function

#End Region 'Method

End Class
