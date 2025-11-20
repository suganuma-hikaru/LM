' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI960  : 出荷データ確認（ハネウェル）
'  作  成  者       :  Minagawa
' ==========================================================================
Option Strict On
Option Explicit On

Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI960BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI960BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI960BLC = New LMI960BLC()

#End Region

#Region "Const"

    ''' <summary>
    ''' 対象データ件数検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_COUNT As String = "SelectData"

    ''' <summary>
    ''' 実績作成アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_INSERT_SENDOUTEDI As String = "InsertSendOutEdi"

    ''' <summary>
    ''' 受注作成アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_INSERT_SENDOUTEDI_JUCHU As String = "InsertSendOutEdiJuchu"    'ADD 2019/12/12 009741

    ''' <summary>
    ''' 遅延送信アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_INSERT_SENDOUTEDI_DELAY As String = "InsertSendOutEdiDelay"

    ''' <summary>
    ''' 出荷登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_SHUKKA_TOUROKU As String = "ShukkaTouroku"    'ADD 2020/02/07 010901

    ''' <summary>
    ''' 一括変更アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_IKKATSU_CHANGE As String = "IkkatsuChange"    'ADD 2019/03/27

    ''' <summary>
    ''' 実績作成対象データ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_SAKUSEI_TARGET As String = "LMI960SAKUSEI_TARGET"

    ''' <summary>
    ''' 処理制御データ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_PROC_CTRL As String = "LMI960PROC_CTRL"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMI960IN"

    'ワーニング処理用データテーブル
    Public Const TABLE_NM_WARNING_HED As String = "WARNING_HED"
    Public Const TABLE_NM_WARNING_DTL As String = "WARNING_DTL"
    Public Const TABLE_NM_WARNING_SHORI As String = "WARNING_SHORI"

    ''' <summary>
    ''' 処理済みフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ShorizumiFlg
        Public Const Mishori As String = "0"
        Public Const ShoriZumi As String = "1"
        Public Const Cancel As String = "2"
    End Class

    ''' <summary>
    ''' 入出荷区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Class InOutKb
        Public Const Mitei As String = ""
        Public Const Inka As String = "1"
        Public Const Outka As String = "2"
        Public Const Unso As String = "3"
    End Class

#End Region

#Region "Method"

    ''' <summary>
    ''' 対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = Me.BlcAccess(ds, LMI960BLF.ACTION_ID_COUNT)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '検索結果取得
        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 実績作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendOutEdi(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = Me.BlcAccess(ds, LMI960BLF.ACTION_ID_INSERT_SENDOUTEDI)

            If (MyBase.IsMessageExist() = True) Then
                Return ds

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    'ADD S 2019/12/12 009741
    ''' <summary>
    ''' 受注作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendOutEdiJuchu(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = Me.BlcAccess(ds, LMI960BLF.ACTION_ID_INSERT_SENDOUTEDI_JUCHU)

            If (MyBase.IsMessageExist() = True) Then
                Return ds

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function
    'ADD E 2019/12/12 009741

    ''' <summary>
    ''' 遅延送信
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendOutEdiDelay(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = Me.BlcAccess(ds, LMI960BLF.ACTION_ID_INSERT_SENDOUTEDI_DELAY)

            If MyBase.IsMessageExist() Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    'ADD S 2020/02/27 010901
    ''' <summary>
    ''' 荷主自動振り分け
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateCustAuto(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

            If MyBase.IsMessageExist Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 荷主手動振り分け
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateCustManual(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

            If MyBase.IsMessageExist Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function
    'ADD E 2020/02/27 010901

    'ADD S 2020/02/07 010901
    ''' <summary>
    ''' 出荷登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShukkaTouroku(ByVal ds As DataSet) As DataSet

        Dim dtSakuseiTarget As DataTable = ds.Tables(LMI960BLF.TABLE_NM_SAKUSEI_TARGET)
        Dim drSakuseiTarget As DataRow
        Dim maxTarget As Integer = dtSakuseiTarget.Rows.Count - 1

        '処理制御データテーブルに行を追加
        Dim dtProcCtrlData As DataTable = ds.Tables(LMI960BLF.TABLE_NM_PROC_CTRL)
        dtProcCtrlData.Clear()
        Dim drProcCtrlData As DataRow = dtProcCtrlData.NewRow
        drProcCtrlData("INOUT_KB") = LMI960BLF.InOutKb.Outka
        dtProcCtrlData.Rows.Add(drProcCtrlData)

        'オーダーごとにループ
        For i As Integer = 0 To maxTarget

            '処理制御データテーブルに現在処理行を設定
            drProcCtrlData("ROW_NO") = i
            drProcCtrlData("WARNING_FLG") = String.Empty

            drSakuseiTarget = dtSakuseiTarget.Rows(i)

            If drSakuseiTarget("SHORIZUMI_FLG").ToString <> ShorizumiFlg.Mishori Then
                '処理済みフラグ≠未処理の場合
                Continue For
            End If

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                ds = Me.BlcAccess(ds, LMI960BLF.ACTION_ID_SHUKKA_TOUROKU)

                If MyBase.IsMessageExist Then
                    Return ds
                End If

                If MyBase.IsMessageStoreExist(CInt(drSakuseiTarget("ROW_NO"))) Then
                    '当該行のメッセージ蓄積がある場合
                    Continue For
                End If

                If Not String.IsNullOrEmpty(drProcCtrlData("WARNING_FLG").ToString) Then
                    'ワーニングがある場合
                    Continue For
                End If

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End Using

            '処理済みフラグの更新
            drSakuseiTarget("SHORIZUMI_FLG") = ShorizumiFlg.ShoriZumi

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 入荷登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NyukaTouroku(ByVal ds As DataSet) As DataSet

        Dim dtSakuseiTarget As DataTable = ds.Tables(LMI960BLF.TABLE_NM_SAKUSEI_TARGET)
        Dim drSakuseiTarget As DataRow
        Dim maxTarget As Integer = dtSakuseiTarget.Rows.Count - 1

        '処理制御データテーブルに行を追加
        Dim dtProcCtrlData As DataTable = ds.Tables(LMI960BLF.TABLE_NM_PROC_CTRL)
        dtProcCtrlData.Clear()
        Dim drProcCtrlData As DataRow = dtProcCtrlData.NewRow
        drProcCtrlData("INOUT_KB") = LMI960BLF.InOutKb.Inka
        dtProcCtrlData.Rows.Add(drProcCtrlData)

        'オーダーごとにループ
        For i As Integer = 0 To maxTarget

            '処理制御データテーブルに現在処理行を設定
            drProcCtrlData("ROW_NO") = i
            drProcCtrlData("WARNING_FLG") = String.Empty

            drSakuseiTarget = dtSakuseiTarget.Rows(i)

            If drSakuseiTarget("SHORIZUMI_FLG").ToString <> ShorizumiFlg.Mishori Then
                '処理済みフラグ≠未処理の場合
                Continue For
            End If

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                ds = Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

                If MyBase.IsMessageExist Then
                    Return ds
                End If

                If Not String.IsNullOrEmpty(drProcCtrlData("WARNING_FLG").ToString) Then
                    'ワーニングがある場合
                    Continue For
                End If

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End Using

            '処理済みフラグの更新
            drSakuseiTarget("SHORIZUMI_FLG") = ShorizumiFlg.ShoriZumi

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 運送登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UnsoTouroku(ByVal ds As DataSet) As DataSet

        Dim dtSakuseiTarget As DataTable = ds.Tables(LMI960BLF.TABLE_NM_SAKUSEI_TARGET)
        Dim drSakuseiTarget As DataRow
        Dim maxTarget As Integer = dtSakuseiTarget.Rows.Count - 1

        '処理制御データテーブルに行を追加
        Dim dtProcCtrlData As DataTable = ds.Tables(LMI960BLF.TABLE_NM_PROC_CTRL)
        dtProcCtrlData.Clear()
        Dim drProcCtrlData As DataRow = dtProcCtrlData.NewRow
        drProcCtrlData("INOUT_KB") = LMI960BLF.InOutKb.Unso
        dtProcCtrlData.Rows.Add(drProcCtrlData)

        'オーダーごとにループ
        For i As Integer = 0 To maxTarget

            '処理制御データテーブルに現在処理行を設定
            drProcCtrlData("ROW_NO") = i
            drProcCtrlData("WARNING_FLG") = String.Empty

            drSakuseiTarget = dtSakuseiTarget.Rows(i)

            If drSakuseiTarget("SHORIZUMI_FLG").ToString <> ShorizumiFlg.Mishori Then
                '処理済みフラグ≠未処理の場合
                Continue For
            End If

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                ds = Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

                If MyBase.IsMessageExist Then
                    Return ds
                End If

                If Not String.IsNullOrEmpty(drProcCtrlData("WARNING_FLG").ToString) Then
                    'ワーニングがある場合
                    Continue For
                End If

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End Using

            '処理済みフラグの更新
            drSakuseiTarget("SHORIZUMI_FLG") = ShorizumiFlg.ShoriZumi

        Next

        Return ds

    End Function

    ''' <summary>
    ''' GLIS受注存在チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsExistBookingForHwl(ByVal ds As DataSet) As DataSet

        'GLISのBLF呼び出し
        ds = MyBase.CallGLWSA("GLZ9300BLF", "IsExistBookingForHwl", ds)

        Return ds

    End Function

    ''' <summary>
    ''' GLIS受注更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdBookingForHwl(ByVal ds As DataSet) As DataSet

        'GLIS受注更新処理の入力データ作成処理
        ds = Me.BlcAccess(ds, "MakeInputDataForUpdBooking")

        If (MyBase.IsMessageExist) Then
            Return ds
        End If

        'GLISのBLF呼び出し(受注更新)
        ds = MyBase.CallGLWSA("GLZ9300BLF", "UpdBookingForHwl", ds)

        Return ds

    End Function

    ''' <summary>
    ''' GLIS受注削除処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DelBookingForHwl(ByVal ds As DataSet) As DataSet

        'GLIS受注削除処理の入力データ作成処理
        ds = Me.BlcAccess(ds, "MakeInputDataForDelBooking")

        If (MyBase.IsMessageExist) Then
            Return ds
        End If

        'GLISのBLF呼び出し(受注削除)
        ds = MyBase.CallGLWSA("GLZ9300BLF", "DelBookingForHwl", ds)

        Return ds

    End Function
    'ADD E 2020/02/07 010901

    ''' <summary>
    ''' シリンダー取込処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ImportCylinder(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

            If (MyBase.IsMessageExist() = True) Then
                Return ds

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    'ADD START 2019/03/27
    ''' <summary>
    ''' 一括変更
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IkkatsuChange(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = Me.BlcAccess(ds, LMI960BLF.ACTION_ID_IKKATSU_CHANGE)

            If (MyBase.IsMessageExist() = True) Then
                Return ds

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function
    'ADD END  2019/03/27

    ''' <summary>
    ''' 受注ステータス戻し処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RollbackJuchuStatus(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

            If (MyBase.IsMessageExist() = True) Then
                Return ds

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' JOB NO変更処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ModJobNo(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

            If (MyBase.IsMessageExist() = True) Then
                Return ds

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 未処理⇔EDI取消処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiTorikeshi(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

            If (MyBase.IsMessageExist() = True) Then
                Return ds

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function


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
