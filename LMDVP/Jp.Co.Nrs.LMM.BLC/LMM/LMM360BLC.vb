' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM360BLC : 請求先テンプレートマスタ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMM360BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM360BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM360DAC = New LMM360DAC()



#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 請求先テンプレートマスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 請求先テンプレートマスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 請求先テンプレートマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistSeiqTemplateM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistSeiqTemplateM", ds)

        '処理件数による判定
        If MyBase.GetResultCount() > 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E010")
        End If

        Return ds

    End Function

#If True Then   'ADD 2022/11/11 025485 【LMS】ABP_指摘・要望-69_荷主未登録請求先で製品セグメントが空

    ''' <summary>
    ''' 請求先テンプレートマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckCust_HOKAN(ByVal ds As DataSet) As DataSet

        Dim GROUP_KB As String = ds.Tables("LMM360IN").Rows(0).Item("GROUP_KB").ToString

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckCust_HOKAN", ds)

        Dim msgSEOQCD As String = ds.Tables("LMM360IN").Rows(0).Item("SEIQTO_CD").ToString
        Dim msg As String = "保管料請求先"

        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then
            '1件も存在しない場合、荷主マスタの設定するようにメッセージを設定する
            MyBase.SetMessage("E02J", New String() {"荷主マスタ", msg, msgSEOQCD})
        End If

        Return ds

    End Function


    ''' <summary>
    ''' 請求先テンプレートマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckCust_NIYAKU(ByVal ds As DataSet) As DataSet

        Dim GROUP_KB As String = ds.Tables("LMM360IN").Rows(0).Item("GROUP_KB").ToString

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckCust_NIYAKU", ds)

        Dim msgSEOQCD As String = ds.Tables("LMM360IN").Rows(0).Item("SEIQTO_CD").ToString
        Dim msg As String = "荷役料請求先"

        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then
            '1件も存在しない場合、荷主マスタの設定するようにメッセージを設定する
            MyBase.SetMessage("E02J", New String() {"荷主マスタ", msg, msgSEOQCD})
        End If

        Return ds

    End Function


    ''' <summary>
    ''' 請求先テンプレートマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckCust_UNCHIN(ByVal ds As DataSet) As DataSet

        Dim GROUP_KB As String = ds.Tables("LMM360IN").Rows(0).Item("GROUP_KB").ToString

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckCust_UNCHIN", ds)

        Dim msgSEOQCD As String = ds.Tables("LMM360IN").Rows(0).Item("SEIQTO_CD").ToString
        Dim msg As String = "運賃請求先"
        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then
            '1件も存在しない場合、荷主マスタの設定するようにメッセージを設定する
            MyBase.SetMessage("E02J", New String() {"荷主マスタ", msg, msgSEOQCD})
        End If

        Return ds

    End Function


    ''' <summary>
    ''' 請求先テンプレートマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckCust_SAGYO(ByVal ds As DataSet) As DataSet

        Dim GROUP_KB As String = ds.Tables("LMM360IN").Rows(0).Item("GROUP_KB").ToString

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckCust_SAGYO", ds)

        Dim msgSEOQCD As String = ds.Tables("LMM360IN").Rows(0).Item("SEIQTO_CD").ToString
        Dim msg As String = "作業料請求先"

        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then
            '1件も存在しない場合、荷主マスタの設定するようにメッセージを設定する
            MyBase.SetMessage("E02J", New String() {"荷主マスタ", msg, msgSEOQCD})
        End If

        Return ds

    End Function


    ''' <summary>
    ''' 請求先テンプレートマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckCust_OYA(ByVal ds As DataSet) As DataSet

        Dim GROUP_KB As String = ds.Tables("LMM360IN").Rows(0).Item("GROUP_KB").ToString

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckCust_OYA", ds)

        Dim msgSEOQCD As String = ds.Tables("LMM360IN").Rows(0).Item("SEIQTO_CD").ToString
        Dim msg As String = "主請求先"

        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then
            '1件も存在しない場合、荷主マスタの設定するようにメッセージを設定する
            MyBase.SetMessage("E02J", New String() {"荷主マスタ", msg, msgSEOQCD})
        End If

        Return ds

    End Function
#End If

    ''' <summary>
    ''' 請求先テンプレートマスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaSeiqTemplateM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectSeiqTemplateM", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 請求先テンプレートマスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertSeiqTemplateM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "InsertSeiqTemplateM", ds)

    End Function

    ''' <summary>
    ''' 請求先テンプレートマスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSeiqTemplateM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateSeiqTemplateM", ds)

    End Function

    ''' <summary>
    ''' 請求先テンプレートマスタ削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteSeiqTemplateM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "DeleteSeiqTemplateM", ds)

    End Function

#End Region

#End Region

End Class
