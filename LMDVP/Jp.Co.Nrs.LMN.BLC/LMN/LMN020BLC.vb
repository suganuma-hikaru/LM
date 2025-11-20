' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN020BLC : 出荷データ詳細
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMN020BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN020BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMN020DAC = New LMN020DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    '''  初期表示データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'ヘッダ部検索(N_OUTKASCM_L / N_OUTKASCM_HED_BP)
        ds = MyBase.CallDAC(Me._Dac, "SelectHeaderData", ds)

        Dim dtL As DataTable = ds.Tables("LMN020OUT_L")
        If dtL.Rows.Count = 0 Then
            '0件の場合
            MyBase.SetMessage("E180")
            Return ds

        End If

        Dim drL As DataRow = dtL.Rows(0)
        Dim drIn As DataRow = ds.Tables("LMN020IN").Rows(0)

        drIn.Item("SCM_CUST_CD") = drL.Item("SCM_CUST_CD")
        drIn.Item("SOKO_CD") = drL.Item("SOKO_CD")

        '明細部検索(N_OUTKASCM_M / N_OUTKASCM_DTL_BP)
        ds = MyBase.CallDAC(Me._Dac, "SelectDetailData", ds)

        '在庫データの検索
        Call Me.SelectZaikoData(ds)

        Return ds

    End Function

    ''' <summary>
    '''  在庫データテーブル検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectZaikoData(ByVal ds As DataSet) As DataSet

        '移行済みか否かで参照先が異なる
        Dim drIn As DataRow = ds.Tables("LMN020IN").Rows(0)
        Dim dacNm As String = String.Empty
        Select Case drIn.Item("IKO_FLG").ToString()
            Case "00" '未移行
                'LMSVer1参照

                'DAC呼び出しメソッド名
                dacNm = "SelectZaikoDataSver1"

            Case "01" '移行済
                'LMSVer2参照

                'DAC呼び出しメソッド名
                dacNm = "SelectZaikoDataSver2"
        End Select

        '在庫データの検索
        Dim dtM As DataTable = ds.Tables("LMN020OUT_M")
        Dim dtZ As DataTable = ds.Tables("LMN020OUT_ZAIKO")
        Dim setZaiko As String = String.Empty
        Dim max As Integer = dtM.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータ設定用データ編集
            drIn.Item("GOODS_CD_NRS") = dtM.Rows(i).Item("GOODS_CD_NRS")
            drIn.Item("CUST_GOODS_CD") = dtM.Rows(i).Item("CUST_GOODS_CD")

            '在庫データ検索
            ds = MyBase.CallDAC(Me._Dac, dacNm, ds)

            '取得結果格納
            setZaiko = dtZ.Rows(0).Item("PORA_ZAI_NB").ToString()
            If String.IsNullOrEmpty(setZaiko) Then
                setZaiko = "0"
            End If
            dtM.Rows(i).Item("PORA_ZAI_NB") = setZaiko

        Next

        Return ds

    End Function

#End Region

#Region "チェック"

    ''' <summary>
    ''' 出荷データ詳細削除時排他チェック(INSERT_FLG = '0'時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaShukkaDataInsFlgOff(ByVal ds As DataSet) As DataSet

        'N_OUTKASCM_Lに対して排他処理を行う
        ds = MyBase.CallDAC(Me._Dac, "SelectOutkaScmL", ds)

        'N_OUTKASCM_HED_BPに対して排他処理を行う(N_OUTKASCM_Lと紐づくデータをチェック)
        ds = MyBase.CallDAC(Me._Dac, "SelectOutkaScmHedBp_L", ds)

        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then
            '0件の場合、論理排他メッセージを設定する
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データ詳細削除時排他チェック(INSERT_FLG = '1'時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaShukkaDataInsFlgOn(ByVal ds As DataSet) As DataSet

        'N_OUTKASCM_HED_BPに対して排他処理を行う
        ds = MyBase.CallDAC(Me._Dac, "SelectOutkaScmHedBp", ds)

        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then
            '0件の場合、論理排他メッセージを設定する
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 倉庫側DBの対象データの削除状態チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHOutkaEdiL(ByVal ds As DataSet) As DataSet

        'H_OUTKAEDI_Lを削除フラグ'0'の検索条件で検索する
        ds = MyBase.CallDAC(Me._Dac, "SelectHOutkaEdiL", ds)

        '処理件数による判定
        If MyBase.GetResultCount() <> 0 Then
            'H_OUTKAEDI_Lに対象の使用可能レコードが残っている場合、メッセージを表示する
            MyBase.SetMessage("E146")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データ詳細初期化時排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaInitShukkaData(ByVal ds As DataSet) As DataSet

        'N_OUTKASCM_Lに対して排他処理を行う
        ds = MyBase.CallDAC(Me._Dac, "SelectOutkaScmL", ds)

        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then
            '0件の場合、論理排他メッセージを設定する
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

#End Region

#Region "削除処理"

    ''' <summary>
    ''' 出荷データ詳細削除(INSERT_FLG = '0'/ステータス「00:未設定」「01:設定済み」時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteShukkaDataInsFlgOff(ByVal ds As DataSet) As DataSet

        'N_OUTKASCM_Lに対して論理削除処理を行う
        ds = MyBase.CallDAC(Me._Dac, "DeleteOutkaScmL", ds)

        'N_OUTKASCM_Mに対して論理削除処理を行う
        ds = MyBase.CallDAC(Me._Dac, "DeleteOutkaScmM", ds)

        'N_OUTKASCM_HED_BPに対して論理削除処理を行う(N_OUTKASCM_Lと紐づくデータを削除)
        ds = MyBase.CallDAC(Me._Dac, "DeleteOutkaScmHedBp_L", ds)

        'N_OUTKASCM_DTL_BPに対して論理削除処理を行う(N_OUTKASCM_Lと紐づくデータを削除)
        ds = MyBase.CallDAC(Me._Dac, "DeleteOutkaScmDtlBp_L", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データ詳細削除(INSERT_FLG = '1'時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteShukkaDataInsFlgOn(ByVal ds As DataSet) As DataSet

        'N_OUTKASCM_HED_BPに対して論理削除処理を行う
        ds = MyBase.CallDAC(Me._Dac, "DeleteOutkaScmHedBp", ds)

        'N_OUTKASCM_DTL_BPに対して論理削除処理を行う
        ds = MyBase.CallDAC(Me._Dac, "DeleteOutkaScmDtlBp", ds)

        Return ds

    End Function

#End Region

#Region "初期化処理"

    ''' <summary>
    ''' 出荷データ詳細初期化処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InitShukkaData(ByVal ds As DataSet) As DataSet

        'N_OUTKASCM_Lに対して初期化処理を行う
        Return MyBase.CallDAC(Me._Dac, "InitShukkaData", ds)

    End Function

#End Region

#End Region

End Class
