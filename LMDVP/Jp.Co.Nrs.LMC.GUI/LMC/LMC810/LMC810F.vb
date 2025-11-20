' ==========================================================================
'  システム名       :  GTO
'  サブシステム名   :  GTM     : マスタメンテナンス
'  プログラムID     :  LMC810F : 会社マスタ
'  作  成  者       :  [iwamoto]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMC810フォームクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMC810F

#Region "Field"

    ''' <summary>
    ''' このフォームを作成したハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Handler As LMC810H

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このフォームを生成したハンドラクラス</param>
    ''' <remarks>呼び出し元のハンドラクラスをこのフォームに紐付けます。</remarks>
    Public Sub New(ByVal handlerClass As LMC810H)

        MyBase.new()

        ' この呼び出しは、Windows フォームデザイナで必要です。
        InitializeComponent()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me._Handler = handlerClass

    End Sub

#End Region 'Constructor

#Region "Method"

    '========================  ↓↓↓その他のイベント ↓↓↓========================

    '========================  ↑↑↑その他のイベント ↑↑↑========================

#End Region 'Method

End Class
