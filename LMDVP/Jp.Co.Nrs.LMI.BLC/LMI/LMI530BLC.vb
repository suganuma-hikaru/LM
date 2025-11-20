' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI530  : セミEDI環境切り替え(丸和物産)
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI530BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI530BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI530DAC = New LMI530DAC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 初期表示用データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    Private Function SelectInitData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectInitData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        ' EDI対象荷主マスタ 物理削除
        ds = MyBase.CallDAC(Me._Dac, "DeleteEdiCust", ds)

        ' セミEDI情報設定マスタ 物理削除
        ds = MyBase.CallDAC(Me._Dac, "DeleteSemiediInfoState", ds)

        ' EDI対象荷主マスタ 登録
        ds = MyBase.CallDAC(Me._Dac, "InsertEdiCust", ds)

        ' セミEDI情報設定マスタ 登録
        ds = MyBase.CallDAC(Me._Dac, "InsertSemiediInfoState", ds)

        Return ds

    End Function

#End Region

End Class
