' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN020V   : 出荷データ詳細
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI

''' <summary>
''' LMN020Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMN020V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMN020F

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMN020F)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMN020C.EventShubetsu) As Boolean

        Dim kengen As String = LMUserInfoManager.GetAuthoLv

        Select Case eventShubetsu  '削除/初期化 時
            Case LMN020C.EventShubetsu.SAKUJHO _
            , LMN020C.EventShubetsu.SHOKIKA
                If kengen.Equals(LMConst.AuthoKBN.VIEW) = True _
                OrElse kengen.Equals(LMConst.AuthoKBN.AGENT) = True Then   '10:閲覧者、50:外部の場合エラー

                    MyBase.ShowMessage("E016")
                    Return False
                End If
        End Select

        Return True

    End Function

    ''' <summary>
    ''' 初期状態チェック
    ''' </summary>
    ''' <param name="InsertFlg">新規登録フラグ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsInitStateChk(ByVal insertFlg As String) As Boolean

        If insertFlg.Equals(LMConst.FLG.ON) = True Then

            MyBase.ShowMessage("E153")
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' ステータスチェック
    ''' </summary>
    ''' <param name="eventShubetsu">ステータスチェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsStatusChk(ByVal eventShubetsu As LMN020C.EventShubetsu) As Boolean

        Dim status As String = Me._Frm.cmbStatus.SelectedValue.ToString()

        'ステータス「03:実績報告済み」時エラー
        If status.Equals(LMN020C.STATUS_JISSEKIHOKOKUZUMI) = True Then
            Dim msg As String = String.Empty
            Select Case eventShubetsu
                Case LMN020C.EventShubetsu.SAKUJHO '削除 時
                    msg = "削除"
                Case LMN020C.EventShubetsu.SHOKIKA '初期化 時
                    msg = "初期化"
            End Select
            MyBase.ShowMessage("E152", New String() {msg})
            Return False
        End If

        Return True

    End Function

#End Region

End Class
