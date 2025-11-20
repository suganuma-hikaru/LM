' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI130  : 日医工詰め合わせ画面
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI130BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI130BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI130DAC = New LMI130DAC()

    ''' <summary>
    ''' データ作成判定フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _MakeFlg As Boolean = False

    Private Const TABLE_NM_SAGYO As String = "LMI130_E_SAGYO"
    Private Const TABLE_NM_INOUT As String = "LMI130INOUT"
    Private Const TABLE_NM_MSAGYO As String = "LMI130_M_SAGYO"
    Private Const TABLE_NM_MCUST As String = "LMI130_M_CUST"
    Private Const TABLE_NM_SAGYO_CD As String = "LMI130_SAGYO_CD"

#End Region

#Region "Method"

    ''' <summary>
    ''' 追加対象データの検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectAddData(ByVal ds As DataSet) As DataSet

        '追加対象データの検索処理
        ds = MyBase.CallDAC(Me._Dac, "SelectAddData", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 作業マスタの検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectSagyoData(ByVal ds As DataSet) As DataSet

        '作業マスタの検索処理
        ds = MyBase.CallDAC(Me._Dac, "SelectSagyoData", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 荷主マスタの検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectCustData(ByVal ds As DataSet) As DataSet

        '荷主マスタの検索処理
        ds = MyBase.CallDAC(Me._Dac, "SelectCustData", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 区分マスタから本特殊荷主用作業レコードを取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectSagyoCord(ByVal ds As DataSet) As DataSet

        '区分マスタへ検索
        ds = MyBase.CallDAC(Me._Dac, "SelectSagyoCord", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 作業レコードへの登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSagyoRecord(ByVal ds As DataSet) As DataSet

        ds = Me.SetInSagyoData(ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        ds = MyBase.CallDAC(Me._Dac, "InsertSagyoRecord", ds)

        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 作業レコードへの登録用DataSet
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetInSagyoData(ByRef ds As DataSet) As DataSet

        Dim num As New NumberMasterUtility
        Dim inDr As DataRow = ds.Tables(TABLE_NM_INOUT).Rows(0)
        Dim sagyoDr As DataRow = ds.Tables(TABLE_NM_SAGYO).NewRow()

        sagyoDr("NRS_BR_CD") = inDr("NRS_BR_CD")
        sagyoDr("SAGYO_REC_NO") = num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, inDr("NRS_BR_CD").ToString())
        sagyoDr("SAGYO_COMP") = "00"
        sagyoDr("SKYU_CHK") = "00"
        sagyoDr("SAGYO_SIJI_NO") = num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_SIJI_NO, Me, inDr("NRS_BR_CD").ToString())
        sagyoDr("INOUTKA_NO_LM") = String.Concat(inDr("OUTKA_NO_L").ToString, inDr("OUTKA_NO_M").ToString)
        sagyoDr("WH_CD") = inDr("WH_CD")
        sagyoDr("IOZS_KB") = "50"

        '区分マスタから作業レコードを取得
        ds = SelectSagyoCord(ds)
        Dim sagyo_Cd As String = Nothing
        If (ds.Tables(TABLE_NM_SAGYO_CD).Rows.Count > 0) Then
            sagyo_Cd = ds.Tables(TABLE_NM_SAGYO_CD).Rows(0).Item("SAGYO_CD").ToString()
        Else
            MyBase.SetMessage("E853")
            Return ds
        End If

        sagyoDr("SAGYO_CD") = sagyo_Cd
        sagyoDr("CUST_CD_L") = inDr("CUST_CD_L")
        sagyoDr("CUST_CD_M") = inDr("CUST_CD_M")
        sagyoDr("DEST_CD") = inDr("DEST_CD")
        sagyoDr("DEST_NM") = inDr("DEST_NM")
        sagyoDr("GOODS_CD_NRS") = inDr("GOODS_CD_NRS")
        sagyoDr("GOODS_NM_NRS") = inDr("GOODS_NM_1")
        sagyoDr("LOT_NO") = inDr("LOT_NO")
        sagyoDr("INV_TANI") = "02"
        sagyoDr("REMARK_ZAI") = String.Empty
        sagyoDr("REMARK_SKYU") = String.Empty
        sagyoDr("SAGYO_COMP_CD") = String.Empty
        sagyoDr("SAGYO_COMP_DATE") = inDr("OUTKA_PLAN_DATE")
        sagyoDr("DEST_SAGYO_FLG") = "00"
        sagyoDr("ZAI_REC_NO") = String.Empty
        sagyoDr("PORA_ZAI_NB") = "0"
        sagyoDr("PORA_ZAI_QT") = "0"

        '作業マスタから必要な情報を取得
        ds = Me.SelectSagyoData(ds)
        sagyoDr("SAGYO_NM") = ds.Tables(TABLE_NM_MSAGYO).Rows(0).Item("SAGYO_NM").ToString()
        sagyoDr("SAGYO_NB") = "1"
        sagyoDr("SAGYO_UP") = ds.Tables(TABLE_NM_MSAGYO).Rows(0).Item("SAGYO_UP").ToString()
        sagyoDr("SAGYO_GK") = CDbl(sagyoDr("SAGYO_NB")) * CDbl(sagyoDr("SAGYO_UP"))
        sagyoDr("TAX_KB") = ds.Tables(TABLE_NM_MSAGYO).Rows(0).Item("ZEI_KBN").ToString()

        '荷主マスタから必要な情報を取得
        ds = Me.SelectCustData(ds)
        sagyoDr("SEIQTO_CD") = ds.Tables(TABLE_NM_MCUST).Rows(0).Item("SAGYO_SEIQTO_CD").ToString()


        'データセットに設定
        ds.Tables(TABLE_NM_SAGYO).Rows.Add(sagyoDr)

        Return ds

    End Function


#End Region

End Class
