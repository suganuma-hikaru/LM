' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主
'  プログラムID     :  LMI930 : 住化カラー実績報告データ作成
'  作  成  者       :  [umano]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI930フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI930F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _H As LMI930H

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMI930H)

        MyBase.new()

        ' この呼び出しは、Windows フォームデザイナで必要です。
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me._H = handlerClass

    End Sub

#End Region 'Constructor

#Region "Method"

#End Region

End Class