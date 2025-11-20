' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ290BLF : 支払運賃マスタ照会
'  作  成  者       :  馬野
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMZ290BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ290BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMZ290BLC = New LMZ290BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    '''支払運賃タリフマスタ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '運送会社マスタ存在チェック(運送会社コードありの時のみ)
        If String.IsNullOrEmpty(ds.Tables("LMZ290IN").Rows(0).Item("UNSOCO_CD").ToString()) = False Then

            ds = MyBase.CallBLC(Me._Blc, "CheckUnsocoM", ds)

            If String.IsNullOrEmpty(ds.Tables("LMZ290IN").Rows(0).Item("SHIHARAI_TARIFF_CD").ToString()) = True AndAlso _
               ds.Tables("LMZ290UNSOCO").Rows.Count > 0 AndAlso _
               String.IsNullOrEmpty(ds.Tables("LMZ290UNSOCO").Rows(0).Item("UNCHIN_TARIFF_CD").ToString()) = False Then

                ds.Tables("LMZ290IN").Rows(0).Item("SHIHARAI_TARIFF_CD") = ds.Tables("LMZ290UNSOCO").Rows(0).Item("UNCHIN_TARIFF_CD").ToString()

            End If

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
