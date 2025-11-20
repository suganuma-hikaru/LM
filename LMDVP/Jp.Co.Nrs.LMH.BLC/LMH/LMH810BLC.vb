' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : マスタメンテ
'  プログラムID     :  LMH810BLC : 分析票管理マスタ
'  作  成  者       :  小林信
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMH810BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH810BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH810DAC = New LMH810DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 分析票管理マスタ更新対象データ検索
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
    ''' 分析票管理マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistCoaM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistCoaM", ds)

        '処理件数による判定
        If MyBase.GetResultCount() > 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E010")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 分析票取り込みチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CoaCheck(ByVal ds As DataSet) As DataSet

        'とりあえず現段階ではチェックなし
        Return ds

    End Function

    ''' <summary>
    ''' 分析票設定マスタの取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetM_COACONFIG(ByVal ds As DataSet) As DataSet

        'TODO:　(2012.09.27)修正START
        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "GetMcoaConfig", ds)

        'ここの値をマスタにセットする
        'ds.Tables("LMH810COACONFIG").Rows.Add(ds.Tables("LMH810COACONFIG").NewRow)

        'ds.Tables("LMH810COACONFIG").Rows(0)("DEST_CD_START_POS") = 4
        'ds.Tables("LMH810COACONFIG").Rows(0)("DEST_CD_LEN") = 9
        'ds.Tables("LMH810COACONFIG").Rows(0)("GOODS_CD_START_POS") = 14
        'ds.Tables("LMH810COACONFIG").Rows(0)("GOODS_CD_LEN") = 10
        'ds.Tables("LMH810COACONFIG").Rows(0)("LOT_NO_START_POS") = 25
        'ds.Tables("LMH810COACONFIG").Rows(0)("LOT_NO_LEN") = 6
        'TODO:　(2012.09.27)修正END

        Return ds

    End Function

    ''' <summary>
    ''' EDI関連フォルダの取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetCUSTCOA_FOLDER(ByVal ds As DataSet) As DataSet

        'TODO:　(2012.09.27)修正START
        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "GetCustCoaFolder", ds)

        'ds.Tables("LMH810COA_FOLDER").Rows.Add(ds.Tables("LMH810COA_FOLDER").NewRow)
        'ds.Tables("LMH810COA_FOLDER").Rows(0)("COA_FOLDER") = "\\SVTYO106s\project\LMS_NET\InputCoa"
        'TODO:　(2012.09.27)修正END

        Return ds

    End Function

    ''' <summary>
    ''' EDI関連フォルダの取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetBRCOA_FOLDER(ByVal ds As DataSet) As DataSet

        'TODO:　(2012.09.27)修正START
        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "GetBrCoaFolder", ds)

        'ds.Tables("LMH810COA_FOLDER").Rows.Add(ds.Tables("LMH810COA_FOLDER").NewRow)
        'ds.Tables("LMH810COA_FOLDER").Rows(0)("COA_FOLDER") = "\\SVTYO106s\project\LMS_NET\coa\10"

        'TODO:　(2012.09.27)修正END

        Return ds
    End Function

    ''' <summary>
    ''' 分析票管理マスタ登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CoaTouroku(ByVal ds As DataSet) As DataSet

        Dim rtnBln As Boolean = False

        '商品キー取得
        ds = MyBase.CallDAC(Me._Dac, "GetGoodsCdNrs", ds)

        If ds.Tables("LMH810GOODS").Rows.Count = 0 Then
            '取得した商品件数が０だったらエラー
            ds.Tables("LMH810Result").Rows(0)("IsErrorResult") = "1"
            MyBase.SetMessageStore("00" _
                       , "E078" _
                       , New String() {"商品マスタ"} _
                       , ds.Tables("LMH810COAIN").Rows(0)("COA_NAME").ToString() _
                       , "エラー項目" _
                       , String.Concat("荷主商品コード：", ds.Tables("LMH810COAIN").Rows(0)("GOODS_CD_CUST").ToString()))
#If True Then   'ADD 2020/10/22 015075
            '千葉営業所の分析票フォルダにエラーバックアップコピーし、取込元削除
            Me.CopyAndDelete2(ds.Tables("LMH810COAIN").Rows(0)("INPUT_COA_FILE").ToString(), ds.Tables("LMH810COA_FOLDER").Rows(0)("COA_ERR_FOLDER").ToString())

#End If
            Return ds
        ElseIf ds.Tables("LMH810GOODS").Rows.Count > 1 Then
            '取得した商品件数が重複していたらエラー
            ds.Tables("LMH810Result").Rows(0)("IsErrorResult") = "1"
            MyBase.SetMessageStore("00" _
                       , "E512" _
                       , _
                       , ds.Tables("LMH810COAIN").Rows(0)("COA_NAME").ToString() _
                       , "エラー項目" _
                       , String.Concat("荷主商品コード：", ds.Tables("LMH810COAIN").Rows(0)("GOODS_CD_CUST").ToString()))
            Return ds
        Else
            ds.Tables("LMH810COAIN").Rows(0)("GOODS_CD_NRS") = ds.Tables("LMH810GOODS").Rows(0)("GOODS_CD_NRS").ToString()
        End If

        '更新チェック
        ds = MyBase.CallDAC(Me._Dac, "SelectListData", ds)
        If ds.Tables("LMH810OUT").Rows.Count = 0 Then
            '新規
            ds = MyBase.CallDAC(Me._Dac, "InsertCoaM", ds)
        ElseIf ds.Tables("LMH810OUT").Rows.Count <> 0 Then


            '20141010 最新版に更新するよう修正
            Dim splitStrOut() As String = Split(ds.Tables("LMH810OUT").Rows(0)("COA_NAME").ToString, "_")
            Dim splitStrIn() As String = Split(ds.Tables("LMH810COAIN").Rows(0)("COA_NAME").ToString, "_")


            If splitStrOut(5) <splitStrIn(5)  Then
                ds = MyBase.CallDAC(Me._Dac, "UpdateCoaM", ds)

                'If String.IsNullOrEmpty(ds.Tables("LMH810OUT").Rows(0)("COA_NAME").ToString()) = True _
                '  OrElse "1".Equals(ds.Tables("LMH810OUT").Rows(0)("SYS_DEL_FLG").ToString()) = True Then
                '    '更新
                '    ds = MyBase.CallDAC(Me._Dac, "UpdateCoaM", ds)
                'Else
                '    'エラー
                '    MyBase.SetMessageStore("00" _
                '               , "E010" _
                '               , _
                '               , ds.Tables("LMH810COAIN").Rows(0)("COA_NAME").ToString() _
                '               , "エラー項目" _
                '               , String.Concat("荷主商品コード/ロットNo/届け先：", _
                '                               ds.Tables("LMH810COAIN").Rows(0)("GOODS_CD_CUST").ToString() _
                '                               , "/", ds.Tables("LMH810COAIN").Rows(0)("LOT_NO").ToString() _
                '                               , "/", ds.Tables("LMH810COAIN").Rows(0)("DEST_CD").ToString()))

                '    ds.Tables("LMH810Result").Rows(0)("IsErrorResult") = "1"
                'End If
            End If

            End If

        'If ds.Tables("LMH810Result").Rows(0)("IsErrorResult").ToString = "0" Then
        '千葉営業所の分析票フォルダにバックアップコピー
        Me.CopyAndDelete(ds.Tables("LMH810COAIN").Rows(0)("INPUT_COA_FILE").ToString(), ds.Tables("LMH810COA_FOLDER").Rows(0)("COA_FOLDER").ToString())

        'End If

        Return ds

    End Function

    Private Function CopyAndDelete(ByVal tgtFile As String, ByVal CopyTOFolder As String) As Boolean

        Try
            '上書きOKとしてコピー可能
            System.IO.File.Copy(tgtFile, String.Concat(CopyTOFolder, "\", IO.Path.GetFileName(tgtFile)), True)
            'System.IO.File.Delete(tgtFile)

        Catch ex As Exception
            Me.SetMessage("S002")
        End Try

    End Function

    Private Function CopyAndDelete2(ByVal tgtFile As String, ByVal CopyTOFolder As String) As Boolean

        Try
            '上書きOKとしてコピー可能
            System.IO.File.Copy(tgtFile, String.Concat(CopyTOFolder, "\", IO.Path.GetFileName(tgtFile)), True)
            System.IO.File.Delete(tgtFile)

        Catch ex As Exception
            Me.SetMessage("S002")
        End Try

    End Function
#End Region

#End Region

End Class
