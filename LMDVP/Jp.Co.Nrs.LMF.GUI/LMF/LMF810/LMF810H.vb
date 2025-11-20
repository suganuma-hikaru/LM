' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF810H : 支払データ生成メイン
'  作  成  者       :  YANAI
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMF810ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMF810H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMF810V

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Main(ByVal prm As LMFormData)

        '--------- ●初期処理

        'INPUTパラメータ取得
        Dim ds As DataSet = prm.ParamDataSet

        'Validateクラスの設定
        Me._V = New LMF810V(Me)

        '制御項目初期設定
        prm.ReturnFlg = False

        '--------- ●主処理

        'INPUTパラメータチェック(今のところ実装なし)
        Dim rtnValue As Boolean = Me._V.IsParamChk(ds)

        If rtnValue = False Then
            Exit Sub
        End If

        Try

            'WSA呼出
            ds = Me.CallWSA("LMF810BLF", "CalcUnchin", ds)

        Catch ex As System.Exception
            'システムエラー発生時処理
            MyBase.SetMessage("S002")

        End Try

        '--------- ●終了処理

        prm.ParamDataSet = ds
        prm.ReturnFlg = True
        Call LMFormNavigate.Revoke(Me)

    End Sub

#End Region '初期処理

#Region "個別メソッド"


#End Region '個別メソッド

#End Region 'Method

End Class
