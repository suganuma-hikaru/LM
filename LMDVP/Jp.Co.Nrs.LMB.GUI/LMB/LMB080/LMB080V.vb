' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB080V : 登録済み画像照会
'  作  成  者       :  matsumoto
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.LMGUIUtility
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB080Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMB080V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMB080F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMBControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMB080F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = New LMBControlV(handlerClass, DirectCast(frm, Form))

    End Sub

#End Region 'Constructor

#Region "Method"

#End Region 'Method

End Class
