' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷
'  プログラムID     :  LMC040    : 在庫引当
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMC040BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC040BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMC040BLC = New LMC040BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing

        If ("02").Equals(ds.Tables("LMC040IN").Rows(0).Item("HIKIATE_FLG").ToString()) = True Then
            rtnDs = MyBase.CallBLC(_Blc, "SelectListData", ds)
            If rtnDs.Tables("LMC040OUT_ZAI").Rows.Count < 1 Then
                MyBase.SetMessage("G001")
            End If
        Else

            '強制実行フラグがオフ時のみ（オンになるのは閾値オーバーでも表示する時）
            If MyBase.GetForceOparation() = False Then

                'データ件数取得
                ds = MyBase.CallBLC(_Blc, "SelectData", ds)
                'メッセージの判定
                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If

            End If

            '検索結果取得
            rtnDs = MyBase.CallBLC(_Blc, "SelectListData", ds)

        End If

        Return rtnDs

    End Function

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListDataTANINUSI(ByVal ds As DataSet) As DataSet

        '強制実行フラグがオフ時のみ（オンになるのは閾値オーバーでも表示する時）
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = MyBase.CallBLC(_Blc, "SelectDataTANINUSI", ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '検索結果取得
        Return MyBase.CallBLC(_Blc, "SelectListDataTANINUSI", ds)

    End Function


#End Region

#Region "設定処理"

#End Region

#End Region

End Class
