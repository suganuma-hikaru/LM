' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫管理
'  プログラムID     :  LMD040BLF : 在庫履歴照会
'  作  成  者       :  [高道]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD040BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD040BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMD040BLC = New LMD040BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成(在庫履歴・ロット別印刷)
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrintRirekiLotBlc As LMD540BLC = New LMD540BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成(在庫履歴・商品別印刷)
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrintRirekiGoodsBlc As LMD541BLC = New LMD541BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成(置場別・在庫一覧表印刷)
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrintOkibaBlc As LMD500BLC = New LMD500BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成(棚卸し一覧表印刷)
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrintTanaBlc As LMD510BLC = New LMD510BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成(商品別・在庫一覧表)
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrintGoodsBlc As LMD520BLC = New LMD520BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成(棚卸し一覧表(社内)印刷)
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrintTanaSyanaiBlc As LMD515BLC = New LMD515BLC()

    '#(2012.07.24)コンソリ業務対応 --- START ---
    ''' <summary>
    ''' 使用するBLCクラスの生成(棚卸し一覧表(CFS業務用)印刷)
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrintTanaCFSBlc As LMD519BLC = New LMD519BLC()
    '#(2012.07.24)コンソリ業務対応 ---  END  ---

    '#(2012.11.07)千葉対応 --- START ---
    ''' <summary>
    ''' 使用するBLCクラスの生成(予定棚卸し一覧表印刷)
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrintYoteiTanaBlc As LMD610BLC = New LMD610BLC()
    '#(2012.11.07)千葉対応---  END  ---

    '追加開始 --- 2015.03.25
    ''' <summary>
    ''' 使用するBLCクラスの生成(予定棚卸し一覧表印刷)
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrintShoboBlc As LMD640BLC = New LMD640BLC()
    '追加終了 --- 2015.03.25


#End Region

#Region "Method"

#Region "検索処理"

#Region "現在庫"

    ''' <summary>
    ''' データ検索処理（商品）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectListDataGoods(ByVal ds As DataSet) As DataSet

        Return Me.SelectListData(ds, "SelectDataGoods", "SelectListDataGoods")

    End Function

    ''' <summary>
    ''' データ検索処理（商品・ロット・入目）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectListDataGoodsLot(ByVal ds As DataSet) As DataSet

        Return Me.SelectListData(ds, "SelectDataGoodsLot", "SelectListDataGoodsLot")

    End Function

    ''' <summary>
    ''' データ検索処理（置場・ロット・入目）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectListDataOkibaLot(ByVal ds As DataSet) As DataSet

        Return Me.SelectListData(ds, "SelectDataOkibaLot", "SelectListDataOkibaLot")

    End Function

    ''' <summary>
    ''' データ検索処理（置場）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectListDataOkiba(ByVal ds As DataSet) As DataSet

        Return Me.SelectListData(ds, "SelectDataOkiba", "SelectListDataOkiba")

    End Function

    ''' <summary>
    ''' データ検索処理（詳細）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectListDataAll(ByVal ds As DataSet) As DataSet

        Return Me.SelectListData(ds, "SelectDataAll", "SelectListDataAll")

    End Function

#End Region

#Region "入荷"

    ''' <summary>
    ''' データ検索処理（入荷ごと）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectListDataInka(ByVal ds As DataSet) As DataSet

        Return Me.SelectListData(ds, String.Empty, "SelectListDataInka")

    End Function

#End Region

#Region "在庫"

    ''' <summary>
    ''' データ検索処理（在庫ごと）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectListDataZaiko(ByVal ds As DataSet) As DataSet

        Return Me.SelectListData(ds, String.Empty, "SelectListDataZaiko")

    End Function

#End Region

#End Region '検索処理

#Region "印刷処理"

    ''' <summary>
    ''' 在庫履歴帳票（LOT別）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function DoLMD540Print(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        Return MyBase.CallBLC(Me._PrintRirekiLotBlc, "DoPrint", ds)

    End Function

    ''' <summary>
    ''' 在庫履歴帳票（商品別）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function DoLMD541Print(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        Return MyBase.CallBLC(Me._PrintRirekiGoodsBlc, "DoPrint", ds)

    End Function

    ''' <summary>
    ''' 置場別・在庫一覧表
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function DoLMD500Print(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(Me._PrintOkibaBlc, "DoPrint", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 商品別・在庫一覧表
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function DoLMD520Print(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(Me._PrintGoodsBlc, "DoPrint", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 棚卸し一覧表
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function DoLMD510Print(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(Me._PrintTanaBlc, "DoPrint", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 棚卸し一覧表(社内)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function DoLMD515Print(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(Me._PrintTanaSyanaiBlc, "DoPrint", ds)

        Return ds

    End Function

    '#(2012.07.24)コンソリ業務対応 --- START ---
    ''' <summary>
    ''' 棚卸し一覧表(CFS業務用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function DoLMD519Print(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(Me._PrintTanaCFSBlc, "DoPrint", ds)

        Return ds

    End Function
    '#(2012.07.24)コンソリ業務対応 ---  END  ---

    '#(2012.11.07)千葉対応 --- START ---
    ''' <summary>
    ''' 予定棚卸し一覧表
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function DoLMD610Print(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(Me._PrintYoteiTanaBlc, "DoPrint", ds)

        Return ds

    End Function
    '#(2012.11.07)千葉対応 --- END ---

    '追加開始 --- 2015.03.25
    ''' <summary>
    ''' 予定棚卸し一覧表
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function DoLMD640Print(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(Me._PrintShoboBlc, "DoPrint", ds)

        Return ds

    End Function
    '追加終了 --- 2015.03.25

#End Region '印刷処理

    'ADD START 2019/8/27 依頼番号:007116,007119
#Region "実行処理"

    ''' <summary>
    ''' 空棚参照のデータ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExecutionEmptyRack(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "ExecutionEmptyRack", ds)

    End Function

    ''' <summary>
    ''' 在庫差異リストのデータ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExecutionZaikoDiff(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "ExecutionZaikoDiff", ds)

    End Function

#End Region
    'ADD END 2019/8/27 依頼番号:007116,007119

#End Region

#Region "共通BLCコールクラス"

    ''' <summary>
    ''' 共通BLCコールクラス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectListData(ByVal ds As DataSet, ByVal actionCountId As String, ByVal actionDataId As String) As DataSet
        If String.IsNullOrEmpty(actionCountId) = False Then
            '強制実行フラグにより処理判定
            If MyBase.GetForceOparation() = False Then

                'データ件数取得
                ds = MyBase.CallBLC(Me._Blc, actionCountId, ds)
                'メッセージの判定
                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If

            End If
        End If

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, actionDataId, ds)

    End Function

#End Region



End Class
