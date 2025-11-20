' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ240BLF : 割増運賃タリフマスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMZ240BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ240BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMZ240BLC = New LMZ240BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    '''割増運賃タリフマスタ/運賃タリフセットマスタデータ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '荷主存在チェック(荷主ありの時のみ)
        If String.IsNullOrEmpty(ds.Tables("LMZ240IN").Rows(0).Item("CUST_CD_L").ToString()) = False Then

            ds = MyBase.CallBLC(Me._Blc, "CheckCustM", ds)

        End If

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = False Then

            '強制実行フラグにより処理判定
            If MyBase.GetForceOparation() = False Then

                'データ件数取得
                ds = MyBase.CallBLC(Me._Blc, "SelectData", ds)
                'メッセージの判定
                If MyBase.IsMessageExist() = True Then

                    Return ds

                End If

            End If

            '検索結果取得
            Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

        End If

        Return ds

    End Function

#End Region

#End Region

End Class
