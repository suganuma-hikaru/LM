' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ230BLF : 横持ちマスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMZ230BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ230BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMZ230BLC = New LMZ230BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    '''横持ちタリフヘッダマスタ/運賃タリフセットマスタデータ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '荷主存在チェック(荷主ありの時のみ)
        If String.IsNullOrEmpty(ds.Tables("LMZ230IN").Rows(0).Item("CUST_CD_L").ToString()) = False Then

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

    '要望対応:1248 terakawa 2013.03.21 Start
    ''' <summary>
    ''' マイ運賃タリフ対象データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMyData(ByVal ds As DataSet) As DataSet

        'データ件数取得
        Return MyBase.CallBLC(Me._Blc, "SelectMyData", ds)

    End Function
    '要望対応:1248 terakawa 2013.03.21 End


#End Region

#End Region

End Class
