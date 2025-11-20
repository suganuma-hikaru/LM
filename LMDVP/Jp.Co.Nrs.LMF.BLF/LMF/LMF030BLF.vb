' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブ
'  プログラムID     :  LMF030BLC : 運行編集
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMF030BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF030BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc010 As LMF010BLC = New LMF010BLC()
    Private _Blc As LMF030BLC = New LMF030BLC()

#End Region

#Region "Const"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMF030IN"

    ''' <summary>
    ''' F_UNSO_Lテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNSO_LL As String = "F_UNSO_LL"

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' IN_KEISANテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN_KEISAN As String = "LMF030IN_KEISAN"
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 初期検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInitData(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 新規検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectNewData(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 排他検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkHaitaData(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 対象運行データキャンセルチェックアクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkCancelData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc010, System.Reflection.MethodBase.GetCurrentMethod.Name, ds)

    End Function

    ''' <summary>
    ''' 運送(大)レコード存在チェック用検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUnsoLData(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    '要望番号:1205(車番入力後、各種情報を取得する) 2012/06/29 本明 Start
    ''' <summary>
    ''' 乗務員の値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDriverData(ByVal ds As DataSet) As DataSet

        '運送(特大)のデータ取得
        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function
    '要望番号:1205(車番入力後、各種情報を取得する) 2012/06/29 本明 End

    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
    ''' <summary>
    ''' 支払データの取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectShiharaiData(ByVal ds As DataSet) As DataSet

        '支払データ取得
        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function
    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End

    '2022.09.06 追加START
    ''' <summary>
    ''' 車輌マスタデータ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCarData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function
    '2022.09.06 追加END

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteAction(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, System.Reflection.MethodBase.GetCurrentMethod.Name, False)

    End Function

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 計算時の更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function KeisanSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEndKeisan(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    'START KIM 要望番号1485 支払い関連修正。
    ''' <summary>
    '''  (支払)運賃の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ShiharaisakiSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function
    'END KIM 要望番号1485 支払い関連修正。

    '要望番号2063 追加START 2015.05.27
    ''' <summary>
    ''' 手配作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertTehaiAction(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function
    '要望番号2063 追加END 2015.05.27

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionStr">メソッド名</param>
    ''' <param name="selectFlg">再検索フラグ</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ScopeStartEnd(ByVal ds As DataSet, ByVal actionStr As String, Optional ByVal selectFlg As Boolean = True) As DataSet

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'BLCアクセス
            ds = Me.BlcAccess(ds, actionStr)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        '更新成功の場合
        If selectFlg = True AndAlso rtnResult = True Then

            '登録しいた情報で再検索
            Dim inTbl As DataTable = ds.Tables(LMF030BLF.TABLE_NM_IN)
            inTbl.Clear()
            inTbl.ImportRow(ds.Tables(LMF030BLF.TABLE_NM_UNSO_LL).Rows(0))
            ds = Me.SelectInitData(ds)

        End If

        Return ds

    End Function

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionStr">メソッド名</param>
    ''' <param name="selectFlg">再検索フラグ</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ScopeStartEndKeisan(ByVal ds As DataSet, ByVal actionStr As String, Optional ByVal selectFlg As Boolean = True) As DataSet

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'BLCアクセス
            ds = Me.BlcAccess(ds, actionStr)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        '更新成功の場合
        If selectFlg = True AndAlso rtnResult = True Then

            '登録しいた情報で再検索
            Dim inTbl As DataTable = ds.Tables(LMF030BLF.TABLE_NM_IN)
            inTbl.Clear()
            inTbl.ImportRow(ds.Tables(LMF030BLF.TABLE_NM_IN_KEISAN).Rows(0))
            ds = Me.SelectInitData(ds)

        End If

        Return ds

    End Function
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    ''' <summary>
    ''' BLCクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function BlcAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallBLC(Me._Blc, actionId, ds)

    End Function

#End Region

End Class
