' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM070BLC : 割増運賃マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMM070BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM070BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM070DAC = New LMM070DAC()

#End Region

#Region "Const"

    ''' <summary>
    ''' 親レコード(JIS)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const OYA_JIS As String = "0000000"

    ''' <summary>
    ''' JISレコード
    ''' </summary>
    ''' <remarks></remarks>
    Private Const JIS As String = "[0000000]以外のJISコード"

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 都道府県名データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ComboData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "ComboData", ds)


    End Function

    ''' <summary>
    ''' 割増運賃マスタ更新対象データ件数検索
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
    ''' 割増運賃マスタ更新対象データ検索
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
    ''' 割増運賃マスタ存在チェック(新規)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistwarimashiM(ByVal ds As DataSet) As DataSet

        'レコード存在チェック
        Dim rtnResult As Boolean = Me.CheckExistwarimashiMGui(ds)

        rtnResult = rtnResult AndAlso Me.CheckExistwarimashiMOya(ds)

        Return ds

    End Function

    ''' <summary>
    ''' レコード存在チェック(新規)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>すでに登録されている場合、エラー</returns>
    ''' <remarks></remarks>
    Private Function CheckExistwarimashiMGui(ByVal ds As DataSet) As Boolean

        '件数チェック
        If Me.CountChk(ds) = False Then
            'マスタ存在メッセージを設定する
            MyBase.SetMessage("E010")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 親レコード存在チェック(新規)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>親レコードが存在しない場合、1行追加</returns>
    ''' <remarks></remarks>
    Private Function CheckExistwarimashiMOya(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables("LMM070IN")
        Dim dr As DataRow = dt.Rows(0)

        '親レコードの場合、スルー
        If LMM070BLC.OYA_JIS.Equals(dr.Item("JIS_CD").ToString()) = True Then
            Return True
        End If

        '1行追加
        dt.ImportRow(dr)

        '1行目のレコードに親コードを設定
        dr = dt.Rows(0)
        dr.Item("JIS_CD") = LMM070BLC.OYA_JIS
        dr.Item("WINT_KIKAN_FROM") = String.Empty
        dr.Item("WINT_KIKAN_TO") = String.Empty
        dr.Item("WINT_EXTC_YN") = String.Empty
        dr.Item("CITY_EXTC_YN") = String.Empty
        dr.Item("RELY_EXTC_YN") = String.Empty
        dr.Item("FRRY_EXTC_YN") = String.Empty

        '件数チェック
        If Me.CountChk(ds) = False Then
            '親レコードを削除
            dr.Delete()
        End If



        Return True

    End Function

    ''' <summary>
    ''' 件数チェック(新規時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>取得できている場合、False</returns>
    ''' <remarks></remarks>
    Private Function CountChk(ByVal ds As DataSet) As Boolean

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistwarimashiM", ds)

        If 0 < MyBase.GetResultCount() Then
            Return False
        End If
        Return True

    End Function

    ''' <summary>
    ''' 割増運賃マスタ存在チェック(削除)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistDeleteM(ByVal ds As DataSet) As DataSet

        'レコード存在チェック
        Dim rtnResult As Boolean = Me.CheckExistOyaDelete(ds)


        Return ds

    End Function

    ''' <summary>
    ''' 親レコード存在チェック(削除)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>親レコードの削除</returns>
    ''' <remarks></remarks>
    Private Function CheckExistOyaDelete(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables("LMM070IN")
        Dim dr As DataRow = dt.Rows(0)

        '子レコードの場合、スルー
        If LMM070BLC.OYA_JIS.Equals(dr.Item("JIS_CD").ToString()) = False Then
            Return True
        End If

        '件数チェック
        If Me.CountChkDelete(ds) = False Then
            'エラーメッセージ
            MyBase.SetMessage("E350", New String() {LMM070BLC.JIS, LMM070BLC.OYA_JIS})
            Exit Function
        End If

        Return True

    End Function

    ''' <summary>
    ''' 件数チェック(削除)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>取得できている場合、False</returns>
    ''' <remarks></remarks>
    Private Function CountChkDelete(ByVal ds As DataSet) As Boolean

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistDeleteM", ds)

        If 0 < MyBase.GetResultCount() Then
            Return False
        End If
        Return True

    End Function

    ''' <summary>
    ''' 割増運賃マスタ存在チェック(復活)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistRivivalM(ByVal ds As DataSet) As DataSet

        'レコード存在チェック
        Dim rtnResult As Boolean = Me.CheckExistRivivalMOya(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 親レコード存在チェック(復活)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>親レコードが存在しない場合、1行追加</returns>
    ''' <remarks></remarks>
    Private Function CheckExistRivivalMOya(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables("LMM070IN")
        Dim dr As DataRow = dt.Rows(0)

        '親レコードの場合、スルー
        If LMM070BLC.OYA_JIS.Equals(dr.Item("JIS_CD").ToString()) = True Then
            Return True
        End If

        If dr.Item("OYA_SYS_DEL_FLG").ToString().Equals("1") = True Then

            '1行追加
            dt.ImportRow(dr)

            '1行目のレコードに親コードを設定
            dr = dt.Rows(0)
            dr.Item("JIS_CD") = LMM070BLC.OYA_JIS
            dr.Item("SYS_UPD_DATE") = dr.Item("OYA_DATE")
            dr.Item("SYS_UPD_TIME") = dr.Item("OYA_TIME")

        End If

        Return True

    End Function

    ''' <summary>
    ''' 割増運賃マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitawarimashiM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectwarimashiM", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 割増運賃マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertwarimashiM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "InsertwarimashiM", ds)

    End Function

    ''' <summary>
    ''' 割増運賃マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdatewarimashiM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdatewarimashiM", ds)

    End Function

    ''' <summary>
    ''' 割増運賃マスタ削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeletewarimashiM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "DeletewarimashiM", ds)

    End Function

#End Region

#End Region

End Class
