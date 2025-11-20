' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF070BLF : 運賃試算比較
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMF070BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF070BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMF070BLC = New LMF070BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Unchin As LMF800BLC = New LMF800BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMF070OUT"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private Const UNCHIN_CALC_IN As String = "UNCHIN_CALC_IN"

#End Region

#Region "Const"

    '[LMF800RESULT]結果ステータス
    Private Const RESULT_NOMAL As String = "00"
    Private Const RESULT_WAR_APPLI As String = "05"
    Private Const RESULT_ERR_PARAM As String = "10"
    Private Const RESULT_ERR_APPLI As String = "20"
    Private Const RESULT_ERR_ZERO As String = "30"
    Private Const RESULT_ERR_SYSTEM As String = "99"

    '結果
    Private errorId As String
    Private errorMsg As String


#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 運賃マスタ更新対象データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        ds = MyBase.CallBLC(Me._Blc, "SelectListData", ds)

        'メッセージの判定
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '運賃計算プログラムの呼び出し
        ds = Me.SelectUnchin(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 運賃マスタ更新対象データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchin(ByVal ds As DataSet) As DataSet

        Dim dtIn As DataTable = ds.Tables("LMF070IN")
        Dim drIn As DataRow = dtIn.Rows(0)

        If String.IsNullOrEmpty(drIn.Item("NEW_TARIFF_CD").ToString) = True AndAlso _
           String.IsNullOrEmpty(drIn.Item("NEW_ETARIFF_CD").ToString) = True AndAlso _
           String.IsNullOrEmpty(drIn.Item("NEW_KYORI_CD").ToString) = True AndAlso _
           String.IsNullOrEmpty(drIn.Item("NEW_ORIG_JIS").ToString) = True AndAlso _
           String.IsNullOrEmpty(drIn.Item("NEW_SOKO_NM").ToString) = True Then
            Return ds
        End If

        Dim dt As DataTable = ds.Tables(LMF070BLF.TABLE_NM_OUT)
        Dim max As Integer = dt.Rows.Count - 1

        Dim prmDs As DataSet = New LMF800DS()
        Dim prmDt As DataTable = prmDs.Tables("UNCHIN_CALC_IN")
        Dim prmdrOut As DataRow = Nothing
        Dim prmDr As DataRow = prmDt.NewRow()

        Dim dr As DataRow = Nothing

        '変数初期化
        errorId = String.Empty
        errorMsg = String.Empty

        With prmDr

            For i As Integer = 0 To max

                'LMF070OUTの情報を追加
                dr = dt.Rows(i)
                dr.Item("NEW_TARIFF_CD") = drIn.Item("NEW_TARIFF_CD").ToString
                dr.Item("NEW_ETARIFF_CD") = drIn.Item("NEW_ETARIFF_CD").ToString

                '引渡しパラメータを設定
                .Item("ACTION_FLG") = "00"
                .Item("NRS_BR_CD") = dr.Item("NRS_BR_CD").ToString()
                .Item("CUST_CD_L") = dr.Item("CUST_CD_L").ToString()
                .Item("CUST_CD_M") = dr.Item("CUST_CD_M").ToString()
                .Item("DEST_CD") = dr.Item("DEST_CD").ToString()
                .Item("DEST_JIS") = dr.Item("DEST_JIS_CD").ToString()
                .Item("ARR_PLAN_DATE") = dr.Item("ARR_PLAN_DATE").ToString()
                .Item("UNSO_PKG_NB") = dr.Item("SEIQ_NG_NB").ToString()
                .Item("NB_UT") = dr.Item("UNSO_TTL_QT").ToString()
                .Item("UNSO_WT") = dr.Item("SEIQ_WT").ToString()
                .Item("UNSO_ONDO_KB") = dr.Item("UNSO_ONDO_KB").ToString()
                .Item("TARIFF_BUNRUI_KB") = dr.Item("TARIFF_BUNRUI_KB").ToString()
                .Item("VCLE_KB") = dr.Item("VCLE_KB").ToString()
                .Item("MOTO_DATA_KB") = dr.Item("MOTO_DATA_KB").ToString()
                .Item("UNSO_TTL_QT") = dr.Item("UNSO_TTL_QT").ToString()
                .Item("SIZE_KB") = dr.Item("SIZE_KB").ToString()
                .Item("UNSO_DATE") = MyBase.GetSystemDate
                .Item("CARGO_KB") = ""
                .Item("CAR_TP") = "00"
                .Item("WT_LV") = 0
                .Item("DANGER_KB") = dr.Item("SEIQ_DANGER_KB").ToString()
                .Item("GOODS_CD_NRS") = ""

                If String.IsNullOrEmpty(drIn.Item("NEW_TARIFF_CD").ToString()) = True Then
                    .Item("SEIQ_TARIFF_CD") = dr.Item("SEIQ_TARIFF_CD").ToString()
                Else
                    .Item("SEIQ_TARIFF_CD") = dr.Item("NEW_TARIFF_CD").ToString()
                End If

                If String.IsNullOrEmpty(drIn.Item("NEW_ETARIFF_CD").ToString()) = True Then
                    .Item("SEIQ_ETARIFF_CD") = dr.Item("SEIQ_ETARIFF_CD").ToString()
                Else
                    .Item("SEIQ_ETARIFF_CD") = dr.Item("NEW_ETARIFF_CD").ToString()
                End If

                If String.IsNullOrEmpty(drIn.Item("NEW_KYORI_CD").ToString()) = True AndAlso _
                   String.IsNullOrEmpty(drIn.Item("NEW_ORIG_JIS").ToString()) = True Then
                    .Item("KYORI") = dr.Item("SEIQ_KYORI").ToString()
                Else
                    .Item("KYORI") = dr.Item("NEW_SEIQ_KYORI").ToString()
                End If

                '運賃タリフと割増タリフが空の場合、処理を飛ばす
                If String.IsNullOrEmpty(drIn.Item("NEW_TARIFF_CD").ToString()) = True AndAlso _
                   String.IsNullOrEmpty(drIn.Item("NEW_ETARIFF_CD").ToString()) = True AndAlso _
                   String.IsNullOrEmpty(dr.Item("SEIQ_TARIFF_CD").ToString()) = True AndAlso _
                   String.IsNullOrEmpty(dr.Item("SEIQ_ETARIFF_CD").ToString()) = True Then

                    'OUTのテーブルに挿入
                    dr.Item("NEW_DECI_UNCHIN") = 0
                    dr.Item("NEW_DECI_CITY_EXTC") = 0
                    dr.Item("NEW_DECI_WINT_EXTC") = 0
                    dr.Item("NEW_DECI_RELY_EXTC") = 0
                    dr.Item("NEW_DECI_TOLL") = 0
                    dr.Item("NEW_DECI_INSU") = 0

                    'パラメータ初期化
                    prmDs.Clear()
                    prmDt.Clear()
                    prmdrOut = Nothing

                    Continue For

                End If

                'INテーブルの情報を追加
                prmDt.Rows.Add(prmDr)

                prmDs = MyBase.CallBLC(Me._Unchin, "CalcUnchin", prmDs)

                Dim resDr As DataRow = prmDs.Tables("LMF800RESULT").Rows(0)

                Select Case resDr.Item("STATUS").ToString

                    Case RESULT_NOMAL, RESULT_WAR_APPLI, RESULT_ERR_ZERO

                        '正常終了
                        prmdrOut = prmDs.Tables("UNCHIN_CALC_OUT").Rows(0)

                        'OUTのテーブルに挿入
                        dr.Item("NEW_DECI_UNCHIN") = prmdrOut.Item("UNCHIN").ToString
                        dr.Item("NEW_DECI_CITY_EXTC") = prmdrOut.Item("CITY_EXTC").ToString
                        dr.Item("NEW_DECI_WINT_EXTC") = prmdrOut.Item("WINT_EXTC").ToString
                        dr.Item("NEW_DECI_RELY_EXTC") = prmdrOut.Item("WINT_EXTC").ToString
                        dr.Item("NEW_DECI_TOLL") = prmdrOut.Item("TOLL").ToString
                        dr.Item("NEW_DECI_INSU") = prmdrOut.Item("INSU").ToString

                    Case Else

                        '異常系
                        errorId = resDr.Item("ERROR_CD").ToString
                        errorMsg = resDr.Item("YOBI1").ToString
                        MyBase.SetMessage(errorId, New String() {errorMsg})
                        Exit For

                End Select

                'パラメータ初期化
                prmDs.Clear()
                prmDt.Clear()
                prmdrOut = Nothing

            Next

        End With

        Return ds

    End Function

#End Region

#End Region

End Class
