' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫
'  プログラムID     :  LMD110BLF : 在庫検索
'  作  成  者       :  DAIKOKU
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD110BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD110BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMD110BLC = New LMD110BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 初期検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグがオフ時のみ（オンになるのは閾値オーバーでも表示する時）
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = MyBase.CallBLC(_Blc, "SelectData", ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

    End Function

#End Region


#Region "印刷処理"

    ''' <summary>
    ''' 振替伝票印刷
    ''' </summary>
    ''' <param name="lmd600Ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintAction(ByVal lmd600Ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        lmd600Ds.Merge(New RdPrevInfoDS)

        Return MyBase.CallBLC(New LMD600BLC(), "DoPrint", lmd600Ds)

    End Function

#End Region


#End Region

End Class
