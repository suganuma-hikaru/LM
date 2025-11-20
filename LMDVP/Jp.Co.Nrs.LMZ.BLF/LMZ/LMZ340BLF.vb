' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ340BLF : 入荷棟室ZONEチェック処理
'  作  成  者       :  asatsuma
' ==========================================================================
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMZ340BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ340BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMZ340BLC = New LMZ340BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    '''チェック処理不要フラグ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCheckFlg(ByVal ds As DataSet) As DataSet

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = False Then

            '検索結果取得
            Return MyBase.CallBLC(Me._Blc, "SelectCheckFlg", ds)

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 貯蔵最大数チェック検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCheckCapa(ByVal ds As DataSet) As DataSet

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = False Then

            '検索結果取得
            Return MyBase.CallBLC(Me._Blc, "SelectCheckCapa", ds)

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 商品数量計算
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCalcQty(ByVal ds As DataSet) As DataSet

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = False Then

            '検索結果取得
            Return MyBase.CallBLC(Me._Blc, "SelectCalcQty", ds)

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 属性系チェック検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCheckAttr(ByVal ds As DataSet) As DataSet

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = False Then

            '検索結果取得
            Return MyBase.CallBLC(Me._Blc, "SelectCheckAttr", ds)

        End If

        Return ds

    End Function

#End Region

#End Region

End Class
