' ==========================================================================
'  システム名       : LM
'  サブシステム名   : LMD       : 在庫管理
'  プログラムID     : LMD630    : 在庫受払表(月間入出庫重量集計表込み)
'  作  成  者       : SBS菊池
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD630BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD630BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMD630DAC = New LMD630DAC()


#End Region

#Region "Method"

#Region "印刷処理"

    ''' <summary>
    ''' 受払表印刷入口
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        Dim rtnDs As New DataSet()

        ''ワークテーブル作成①
        'Me.InsertD_WK_INOUT1(ds)

        ''ワークテーブル作成②
        'Me.InsertD_WK_INOUT2(ds)

        ''ワークテーブル作成③
        'Me.InsertD_WK_INOUT3(ds)

        ''ワークテーブル作成④
        'Me.InsertD_WK_INOUT4(ds)

        ''ワークテーブル作成⑤
        'rtnDs = Me.SelectD_WK_INOUT5(ds)

        Dim strdate As Date = Now
        Dim strtime As Long = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "【大外！】InsertD_WK_INOUT1", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)
        Me.InsertD_WK_INOUT1(ds)
        Dim enddate As Date = Now
        Dim endtime As Long = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "【大外！】InsertD_WK_INOUT1", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMD630BLC", "【大外！】InsertD_WK_INOUT1", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")


        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "InsertD_WK_INOUT2", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)
        'Me.InsertD_WK_INOUT2(ds)
        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "InsertD_WK_INOUT2", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMD630BLC", "InsertD_WK_INOUT2", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")


        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "InsertD_WK_INOUT3", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)
        'Me.InsertD_WK_INOUT3(ds)
        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "InsertD_WK_INOUT3", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMD630BLC", "InsertD_WK_INOUT3", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")


        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "InsertD_WK_INOUT4", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)
        Me.InsertD_WK_INOUT4(ds)
        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "InsertD_WK_INOUT4", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMD630BLC", "InsertD_WK_INOUT4", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")


        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "SelectD_WK_INOUT5", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)
        rtnDs = Me.SelectD_WK_INOUT5(ds)
        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "SelectD_WK_INOUT5", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMD630BLC", "SelectD_WK_INOUT5", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")


        Return rtnDs

    End Function

    ''' <summary>
    ''' ワークテーブル(Q_WK_INOUT1)の作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertD_WK_INOUT1(ByVal ds As DataSet) As DataSet

        '実行日から3ヶ月以内のwkテーブルデータを全削除後に入出荷移動データを作成する。

        ''全削除
        'ds = MyBase.CallDAC(Me._Dac, "Delete_D_WK_INOUT1", ds)

        ''入荷
        'ds = MyBase.CallDAC(Me._Dac, "Insert_D_WK_INOUT1_FROM_INKA", ds)

        ''出荷
        'ds = MyBase.CallDAC(Me._Dac, "Insert_D_WK_INOUT1_FROM_OUTKA", ds)

        ''移動後
        'ds = MyBase.CallDAC(Me._Dac, "Insert_D_WK_INOUT1_FROM_IDO_BEFORE", ds)

        ''移動前
        'ds = MyBase.CallDAC(Me._Dac, "Insert_D_WK_INOUT1_FROM_IDO_AFTER", ds)

        'SQL速度テスト-------------
        Dim strdate As Date = Now
        Dim strtime As Long = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "Delete_D_WK_INOUT1", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        ds = MyBase.CallDAC(Me._Dac, "Delete_D_WK_INOUT1", ds)

        Dim enddate As Date = Now
        Dim endtime As Long = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "Delete_D_WK_INOUT1", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMD630BLC", "Delete_D_WK_INOUT1", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")


        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "Insert_D_WK_INOUT1_FROM_INKA", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        ds = MyBase.CallDAC(Me._Dac, "Insert_D_WK_INOUT1_FROM_INKA", ds)

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "Insert_D_WK_INOUT1_FROM_INKA", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMD630BLC", "Insert_D_WK_INOUT1_FROM_INKA", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")


        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "Insert_D_WK_INOUT1_FROM_OUTKA", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        ds = MyBase.CallDAC(Me._Dac, "Insert_D_WK_INOUT1_FROM_OUTKA", ds)

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "Insert_D_WK_INOUT1_FROM_OUTKA", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMD630BLC", "Insert_D_WK_INOUT1_FROM_OUTKA", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")


        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "Insert_D_WK_INOUT1_FROM_IDO_BEFORE", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        ds = MyBase.CallDAC(Me._Dac, "Insert_D_WK_INOUT1_FROM_IDO_BEFORE", ds)

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "Insert_D_WK_INOUT1_FROM_IDO_BEFORE", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMD630BLC", "Insert_D_WK_INOUT1_FROM_IDO_BEFORE", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")


        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "Insert_D_WK_INOUT1_FROM_IDO_AFTER", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        ds = MyBase.CallDAC(Me._Dac, "Insert_D_WK_INOUT1_FROM_IDO_AFTER", ds)

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMD630BLC", "Insert_D_WK_INOUT1_FROM_IDO_AFTER", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMD630BLC", "Insert_D_WK_INOUT1_FROM_IDO_AFTER", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")

        Return ds

    End Function

    ''' <summary>
    ''' ワークテーブル(Q_WK_INOUT2)の作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertD_WK_INOUT2(ByVal ds As DataSet) As DataSet

        'wkテーブル全削除後に営業所コードごとの入出荷移動データを作成する。
        '

        '全削除
        ds = MyBase.CallDAC(Me._Dac, "Delete_D_WK_INOUT2", ds)

        '選択＆書き込み

        For ptn As Integer = 0 To 2
            ds.Tables("LMD630IN").Rows(0).Item("PTN_NUM") = ptn
            ds = MyBase.CallDAC(Me._Dac, "Insert_D_WK_INOUT2", ds)
        Next

        Return ds

    End Function

    ''' <summary>
    ''' ワークテーブル(Q_WK_INOUT3)の作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertD_WK_INOUT3(ByVal ds As DataSet) As DataSet

        'wkテーブル全削除後に入出荷移動データを作成する。

        '全削除
        ds = MyBase.CallDAC(Me._Dac, "Delete_D_WK_INOUT3", ds)

        '選択＆書き込み
        ds = MyBase.CallDAC(Me._Dac, "Insert_D_WK_INOUT3", ds)

        Return ds

    End Function

    ''' <summary>
    ''' ワークテーブル(Q_WK_INOUT4)の作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertD_WK_INOUT4(ByVal ds As DataSet) As DataSet

        'wkテーブル全削除後に入出荷移動データを作成する。

        '全削除
        ds = MyBase.CallDAC(Me._Dac, "Delete_D_WK_INOUT4", ds)
#If True Then   'ADD 2020/10/29　新 016063
        ds = MyBase.CallDAC(Me._Dac, "Delete_D_WK_INOUT4A", ds)
#End If
        '選択＆書き込み
        ds = MyBase.CallDAC(Me._Dac, "Insert_D_WK_INOUT4", ds)

        Return ds

    End Function

    ''' <summary>
    ''' ワークテーブル(Q_WK_INOUT5)の作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectD_WK_INOUT5(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "Select_D_WK_INOUT5", ds)

        Return ds

    End Function


#End Region

#End Region

End Class
