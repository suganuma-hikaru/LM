' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN810BLC : 欠品チェック
'  作  成  者       :  [佐川央]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMN810BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN810BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMN810DAC = New LMN810DAC()

#End Region

#Region "Method"

#Region "欠品数取得"

    ''' <summary>
    ''' 欠品数取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ChkKeppin(ByVal ds As DataSet) As DataSet

        '結果格納用データセット
        Dim rtnDs As DataSet = New LMN810DS()
        '結果格納テーブル名
        Dim outNm As String = "LMN810OUT"
        '項目名設定
        Dim plInkaNm As String = "PLAN_INKA_NB"
        Dim plOutkaNm As String = "PLAN_OUTKA_NB"
        Dim ZaikoNm As String = "ZAIKO_NB"
        Dim plZaikoNm As String = "PLAN_ZAIKO_NB"
        Dim scmOutkaNm As String = "SCM_OUTKA_NB"
        Dim KeppinNm As String = "KEPPIN_NB"
        '結果格納用変数
        Dim PlanInkaNb As Integer = 0
        Dim PlanOutkaNb As Integer = 0
        Dim ZaikoNb As Integer = 0
        Dim PlanZaikoNb As Integer = 0
        Dim ScmOutkaNb As Integer = 0
        Dim KeppinNb As Integer = 0

        '営業所接続情報の移行フラグを取得
        Dim ikoFlg As String = ds.Tables("BR_CD_LIST").Rows(0).Item("IKO_FLG").ToString()

        '(LMS)予定総入荷数取得(B_INKA_M)
        '移行フラグより処理メソッド判断
        If ikoFlg = "00" Then
            '移行前
            rtnDs = MyBase.CallDAC(Me._Dac, "GetB_INKA_MLMSVer1", ds)
        Else
            '移行済み
            rtnDs = MyBase.CallDAC(Me._Dac, "GetB_INKA_MLMSVer2", ds)
        End If
        '結果件数が存在する場合
        If rtnDs.Tables(outNm).Rows.Count > 0 Then
            If String.IsNullOrEmpty(rtnDs.Tables(outNm).Rows(0).Item(plInkaNm).ToString()) = False Then
                PlanInkaNb = Convert.ToInt32(rtnDs.Tables(outNm).Rows(0).Item(plInkaNm))
            End If
        End If

        '(LMS)予定総入荷数取得(H_INKAEDI_M)
        '移行フラグより処理メソッド判断
        If ikoFlg = "00" Then
            '移行前
            rtnDs = MyBase.CallDAC(Me._Dac, "GetH_INKAEDI_MLMSVer1", ds)
        Else
            '移行済み
            rtnDs = MyBase.CallDAC(Me._Dac, "GetH_INKAEDI_MLMSVer2", ds)
        End If
        '結果件数が存在する場合
        If rtnDs.Tables(outNm).Rows.Count > 0 Then
            If String.IsNullOrEmpty(rtnDs.Tables(outNm).Rows(0).Item(plInkaNm).ToString()) = False Then
                PlanInkaNb = PlanInkaNb + Convert.ToInt32(rtnDs.Tables(outNm).Rows(0).Item(plInkaNm))
            End If
        End If

        '(LMS)予定総出荷数取得(C_OUTKA_M)
        '移行フラグより処理メソッド判断
        If ikoFlg = "00" Then
            '移行前
            rtnDs = MyBase.CallDAC(Me._Dac, "GetC_OUTKA_MLMSVer1", ds)
        Else
            '移行済み
            rtnDs = MyBase.CallDAC(Me._Dac, "GetC_OUTKA_MLMSVer2", ds)
        End If
        '結果件数が存在する場合
        If rtnDs.Tables(outNm).Rows.Count > 0 Then
            If String.IsNullOrEmpty(rtnDs.Tables(outNm).Rows(0).Item(plOutkaNm).ToString()) = False Then
                PlanOutkaNb = Convert.ToInt32(rtnDs.Tables(outNm).Rows(0).Item(plOutkaNm))
            End If
        End If

        '(LMS)予定総出荷数取得(H_OUTKAEDI_M)
        '移行フラグより処理メソッド判断
        If ikoFlg = "00" Then
            '移行前
            rtnDs = MyBase.CallDAC(Me._Dac, "GetH_OUTKAEDI_MLMSVer1", ds)
        Else
            '移行済み
            rtnDs = MyBase.CallDAC(Me._Dac, "GetH_OUTKAEDI_MLMSVer2", ds)
        End If
        '結果件数が存在する場合
        If rtnDs.Tables(outNm).Rows.Count > 0 Then
            If String.IsNullOrEmpty(rtnDs.Tables(outNm).Rows(0).Item(plOutkaNm).ToString()) = False Then
                PlanOutkaNb = PlanOutkaNb + Convert.ToInt32(rtnDs.Tables(outNm).Rows(0).Item(plOutkaNm))
            End If
        End If

        '(LMS)現在在庫数取得(D_ZAI_TRS)
        '移行フラグより処理メソッド判断
        If ikoFlg = "00" Then
            '移行前
            rtnDs = MyBase.CallDAC(Me._Dac, "GetD_ZAI_TRSLMSVer1", ds)
        Else
            '移行済み
            rtnDs = MyBase.CallDAC(Me._Dac, "GetD_ZAI_TRSLMSVer2", ds)
        End If
        '結果件数が存在する場合
        If rtnDs.Tables(outNm).Rows.Count > 0 Then
            If String.IsNullOrEmpty(rtnDs.Tables(outNm).Rows(0).Item(ZaikoNm).ToString()) = False Then
                ZaikoNb = Convert.ToInt32(rtnDs.Tables(outNm).Rows(0).Item(ZaikoNm))
            End If
        End If

        '予定在庫数取得
        PlanZaikoNb = ZaikoNb + PlanInkaNb - PlanOutkaNb

        'SCM合計総出荷個数取得(N_OUTKASCM_M)
        rtnDs = MyBase.CallDAC(Me._Dac, "GetN_OUTKASCM_M", ds)
        '結果件数が存在する場合
        If rtnDs.Tables(outNm).Rows.Count > 0 Then
            If String.IsNullOrEmpty(rtnDs.Tables(outNm).Rows(0).Item(scmOutkaNm).ToString()) = False Then
                ScmOutkaNb = Convert.ToInt32(rtnDs.Tables(outNm).Rows(0).Item(scmOutkaNm))
            End If
        End If

        '欠品数取得
        KeppinNb = ScmOutkaNb - PlanZaikoNb
        '欠品数が負数の場合は0を設定
        If KeppinNb < 0 Then
            KeppinNb = 0
        End If

        'リターンデータセットに値を設定
        ds.Tables(outNm).Rows(0).Item("SCM_CUST_CD") = ds.Tables("LMN810IN").Rows(0).Item("SCM_CUST_CD").ToString()
        ds.Tables(outNm).Rows(0).Item("LMS_CUST_CD") = ds.Tables("LMN810IN").Rows(0).Item("LMS_CUST_CD").ToString()
        ds.Tables(outNm).Rows(0).Item("BR_CD") = ds.Tables("LMN810IN").Rows(0).Item("BR_CD").ToString()
        ds.Tables(outNm).Rows(0).Item("SOKO_CD") = ds.Tables("LMN810IN").Rows(0).Item("SOKO_CD").ToString()
        ds.Tables(outNm).Rows(0).Item("GOODS_CD_CUST") = ds.Tables("LMN810IN").Rows(0).Item("GOODS_CD_CUST").ToString()
        ds.Tables(outNm).Rows(0).Item(plInkaNm) = PlanInkaNb.ToString()
        ds.Tables(outNm).Rows(0).Item(plOutkaNm) = PlanOutkaNb.ToString()
        ds.Tables(outNm).Rows(0).Item(ZaikoNm) = ZaikoNb.ToString()
        ds.Tables(outNm).Rows(0).Item(plZaikoNm) = PlanZaikoNb.ToString()
        ds.Tables(outNm).Rows(0).Item(scmOutkaNm) = ScmOutkaNb.ToString()
        ds.Tables(outNm).Rows(0).Item(KeppinNm) = KeppinNb.ToString()

        Return ds

    End Function

#End Region

#End Region

End Class
