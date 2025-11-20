' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特殊荷主機能
'  プログラムID     :  LMI110BLF : 日医工製品マスタ登録
'  作  成  者       :  [寺川徹]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI110BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI110BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Const"

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI110BLC = New LMI110BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 商品マスタ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

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

    End Function

#End Region

#Region "商品M反映"

    ''' <summary>
    ''' 商品M反映(商品マスタ、新規登録/更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SaveGoodsM(ByVal ds As DataSet) As DataSet

        '元DataSet
        Dim dtM_GOODS_HANEI As DataTable = ds.Tables("M_GOODS_HANEI")
        '要望番号:1250 terakawa 2012.07.12 Start
        Dim dtM_GOODS_DETAILS As DataTable = ds.Tables("M_GOODS_DETAILS")
        '要望番号:1250 terakawa 2012.07.12 End
        Dim iMax As Integer = dtM_GOODS_HANEI.Rows.Count - 1

        'Copy後のDataSet
        Dim dsCopy As DataSet = ds.Copy()
        Dim dtCopyM_GOODS_HANEI As DataTable = dsCopy.Tables("M_GOODS_HANEI")
        '要望番号:1250 terakawa 2012.07.12 Start
        Dim dtCopyM_GOODS_DETAILS As DataTable = dsCopy.Tables("M_GOODS_DETAILS")
        '要望番号:1250 terakawa 2012.07.12 End

        For iCnt As Integer = 0 To iMax

            'トランザクション開始（１マスタ毎にコミットする）
            Using scope As TransactionScope = MyBase.BeginTransaction()

                MyBase.SetMessage(Nothing)

                dsCopy.Clear()  '値のクリア
                dtCopyM_GOODS_HANEI.ImportRow(dtM_GOODS_HANEI.Rows(iCnt)) '値の設定
                '要望番号:1250 terakawa 2012.07.12 Start
                dtCopyM_GOODS_DETAILS.Merge(dtM_GOODS_DETAILS) '値の設定
                '要望番号:1250 terakawa 2012.07.12 End

                Select Case dtCopyM_GOODS_HANEI.Rows(0).Item("STATUS").ToString

                    Case "新規"

                        ' ''保存時共通チェック
                        ''If Me.ChkSaveCommon(dsCopy) = False Then
                        ''    Return ds
                        ''End If

                        '商品Keyの取得
                        If Me.SetGoodsKeyNo(dsCopy) = True Then

                            '新規登録時チェックを行う
                            If ChkInsertData(dsCopy) = False Then
                                'Return ds
                                Continue For
                            End If

                            '新規登録処理を行う
                            ds = MyBase.CallBLC(Me._Blc, "InsertData", dsCopy)

                            '日医工製品マスタの更新を行う
                            ds = MyBase.CallBLC(Me._Blc, "UpdateSehinM", dsCopy)
                            If MyBase.IsMessageExist() = True Then
                                Continue For
                            End If


                        End If

                    Case "変更", "重複"

                        ' ''保存時共通チェック
                        ''If Me.ChkSaveCommon(dsCopy) = False Then
                        ''    Return ds
                        ''End If

                        '更新時チェックを行う
                        ''If ChkUpdateData(dsCopy) = False Then
                        ''    Return ds
                        ''End If

                        'データの更新
                        ds = MyBase.CallBLC(Me._Blc, "UpdateSaveData", dsCopy)
                        If MyBase.IsMessageExist() = True Then
                            Continue For
                        End If

                        '日医工製品マスタの更新を行う
                        ds = MyBase.CallBLC(Me._Blc, "UpdateSehinM", dsCopy)
                        If MyBase.IsMessageExist() = True Then
                            Continue For
                        End If

                    Case Else

                        '無処理


                End Select

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End Using

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録処理の場合、商品KEYを採番する
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>TRUE:新規登録、FALSE:更新登録</returns>
    ''' <remarks></remarks>
    Private Function SetGoodsKeyNo(ByVal ds As DataSet) As Boolean

        Dim dtGoods As DataTable = ds.Tables("M_GOODS_HANEI")
        Dim dtGoodsDtl As DataTable = ds.Tables("M_GOODS_DETAILS")

        If String.IsNullOrEmpty(dtGoods.Rows(0).Item("GOODS_CD_NRS").ToString()) = False Then
            Return False
        End If

        '商品Keyを新規採番する
        Dim brCd As String = dtGoods.Rows(0).Item("NRS_BR_CD").ToString()
        Dim num As New NumberMasterUtility
        Dim goodsKey As String = num.GetAutoCode(NumberMasterUtility.NumberKbn.GOODS_CD_NRS, Me, brCd)

        '新規登録内容を設定する
        dtGoods.Rows(0).Item("GOODS_CD_NRS") = goodsKey
        '要望番号:1250 terakawa 2012.07.12 Start
        Dim max As Integer = dtGoodsDtl.Rows.Count - 1
        For i As Integer = 0 To max
            dtGoodsDtl.Rows(i).Item("GOODS_CD_NRS") = goodsKey
        Next
        '要望番号:1250 terakawa 2012.07.12 End

        Return True

    End Function

    ''' <summary>
    ''' 新規登録時チェック処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>True:エラーなし、False:エラー有り</returns>
    ''' <remarks></remarks>
    Private Function ChkInsertData(ByVal ds As DataSet) As Boolean

        '商品マスタ存在チェック
        ds = MyBase.CallBLC(Me._Blc, "ExistGoodsM", ds)
        If MyBase.IsMessageExist() Then
            Return False
        End If

        Return True

    End Function

#End Region


#End Region

End Class