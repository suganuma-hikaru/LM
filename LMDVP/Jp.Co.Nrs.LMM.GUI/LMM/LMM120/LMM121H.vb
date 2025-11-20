' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM121H : 単価マスタメンテナンス(セット料金)
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMM121ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMM121H
    Inherits LMM120H

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Overloads Sub Main(ByVal prm As LMFormData)

        ' 特定荷主フラグの設定
        ' 本クラスよりの呼び出しであれば TSMC と判定する
        Me._flgTSMC = True

        MyBase.Main(prm)

    End Sub

#End Region

End Class
