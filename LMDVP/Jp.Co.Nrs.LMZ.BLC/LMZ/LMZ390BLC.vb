' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ390BLC : Rapidus次回分納情報取得
'  作  成  者       :  
' ==========================================================================

Option Explicit On

Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMZ390BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ390BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMZ390DAC = New LMZ390DAC()

#End Region ' "Field"

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 次回分納情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectJikaiBunnouInfo(ByVal ds As DataSet) As DataSet

        Dim drIn As DataRow

        drIn = ds.Tables("LMZ390IN").Rows(0)
        Dim dsDac As DataSet = ds.Clone()
        dsDac.Tables("LMZ390IN").ImportRow(drIn)

        Dim drRet As DataRow = Nothing

        ' Rapidus出荷EDIデータテーブル 存在チェック
        If Not Me.CheckOutkaEdiDtlRapiExists(dsDac) Then
            drRet = ds.Tables("LMZ390OUT").NewRow()
            ' 戻り値・次回分納有無←“無”
            drRet.Item("JIKAI_BUNNOU_UMU") = LMConst.FLG.OFF
            ds.Tables("LMZ390OUT").Rows.Add(drRet)
            Return ds
        End If

        ' 次回分納情報有無確認対象の出荷指示EDIデータの取得
        MyBase.CallDAC(Me._Dac, "SelectOutkaEdiDtlRapi", dsDac)
        If MyBase.GetResultCount = 0 Then
            drRet = ds.Tables("LMZ390OUT").NewRow()
            ' 戻り値・次回分納有無←“無”
            drRet.Item("JIKAI_BUNNOU_UMU") = LMConst.FLG.OFF
            ds.Tables("LMZ390OUT").Rows.Add(drRet)
            Return ds
        End If

        Dim dsDac2 As DataSet = dsDac.Copy()
        For i As Integer = 0 To dsDac2.Tables("H_OUTKAEDI_DTL_RAPI").Rows.Count() - 1
            dsDac.Tables("H_OUTKAEDI_DTL_RAPI").Clear()
            dsDac.Tables("H_OUTKAEDI_DTL_RAPI").ImportRow(dsDac2.Tables("H_OUTKAEDI_DTL_RAPI").Rows(i))

            drRet = ds.Tables("LMZ390OUT").NewRow()
            ' 戻り値・次回分納有無←“無”(初期値)
            drRet.Item("JIKAI_BUNNOU_UMU") = LMConst.FLG.OFF

            ' 次回分納の出荷指示EDIデータの取得
            MyBase.CallDAC(Me._Dac, "SelectOutkaEdiDtlRapiJikaiBunnou", dsDac)
            If MyBase.GetResultCount = 0 Then
                ds.Tables("LMZ390OUT").Rows.Add(drRet)
                Continue For
            End If

            ' 戻り値・次回分納有無←“有”
            drRet.Item("JIKAI_BUNNOU_UMU") = LMConst.FLG.ON

            Dim outkaCtlNo As String = dsDac.Tables("H_OUTKAEDI_DTL_RAPI_JIKAI_BUNNOU").Rows(0).Item("OUTKA_CTL_NO").ToString()
            ' 戻り値・次回分納出荷管理番号←次回分納の出荷指示EDIデータ.出荷管理番号
            drRet.Item("JIKAI_BUNNOU_OUTKA_CTL_NO") = outkaCtlNo

            ' 戻り値・次回分納EDI管理番号←次回分納の出荷指示EDIデータ.EDI管理番号
            ' 戻り値・次回分納データ受信日←次回分納の出荷指示EDIデータ.データ受信日
            ' 戻り値・次回分納受信ファイル名←次回分納の出荷指示EDIデータ.受信ファイル名
            drRet.Item("JIKAI_BUNNOU_EDI_CTL_NO") = dsDac.Tables("H_OUTKAEDI_DTL_RAPI_JIKAI_BUNNOU").Rows(0).Item("EDI_CTL_NO").ToString()
            drRet.Item("JIKAI_BUNNOU_CRT_DATE") = dsDac.Tables("H_OUTKAEDI_DTL_RAPI_JIKAI_BUNNOU").Rows(0).Item("CRT_DATE").ToString()
            drRet.Item("JIKAI_BUNNOU_FILE_NAME") = dsDac.Tables("H_OUTKAEDI_DTL_RAPI_JIKAI_BUNNOU").Rows(0).Item("FILE_NAME").ToString()

            ' 次回分納のEDI出荷L の取得
            MyBase.CallDAC(Me._Dac, "SelectOutkaEdiL_JikaiBunnou", dsDac)
            If MyBase.GetResultCount = 0 Then
                ds.Tables("LMZ390OUT").Rows.Add(drRet)
                Continue For
            End If

            ' 戻り値・次回分納実績処理フラグ←次回分納のEDI出荷L.実績処理フラグ
            ' 戻り値・次回分納のEDI出荷L.更新日、同.更新時刻 設定
            drRet.Item("JIKAI_BUNNOU_JISSEKI_FLAG") = dsDac.Tables("H_OUTKAEDI_L").Rows(0).Item("JISSEKI_FLAG")
            drRet.Item("EDI_L_SYS_UPD_DATE") = dsDac.Tables("H_OUTKAEDI_L").Rows(0).Item("SYS_UPD_DATE")
            drRet.Item("EDI_L_SYS_UPD_TIME") = dsDac.Tables("H_OUTKAEDI_L").Rows(0).Item("SYS_UPD_TIME")

            If outkaCtlNo.PadRight(8, " "c).Substring(1, 8) = New String("0"c, 8) Then
                ds.Tables("LMZ390OUT").Rows.Add(drRet)
                Continue For
            End If

            ' 次回分納の出荷指示EDIデータ.出荷管理番号 右8桁≠'00000000' の場合

            ' 次回分納の出荷L の取得
            MyBase.CallDAC(Me._Dac, "SelectOutkaL_JikaiBunnou", dsDac)
            ' 戻り値・次回分納出荷削除フラグ←次回分納の出荷L.削除フラグ(出荷L 未登録の場合は削除フラグ '1' とみなす)
            If MyBase.GetResultCount = 0 Then
                drRet.Item("JIKAI_BUNNOU_SYS_DEL_FLG") = LMConst.FLG.ON
            Else
                drRet.Item("JIKAI_BUNNOU_SYS_DEL_FLG") = dsDac.Tables("C_OUTKA_L").Rows(0).Item("SYS_DEL_FLG").ToString()
            End If

            ds.Tables("LMZ390OUT").Rows.Add(drRet)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' Rapidus出荷EDIデータテーブル 存在チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function CheckOutkaEdiDtlRapiExists(ByVal ds As DataSet) As Boolean

        ds.Tables("LMZ390_TBL_EXISTS").Clear()
        Dim drTblExists As DataRow = ds.Tables("LMZ390_TBL_EXISTS").NewRow()
        drTblExists.Item("NRS_BR_CD") = ds.Tables("LMZ390IN").Rows(0).Item("NRS_BR_CD")
        drTblExists.Item("TBL_NM") = "H_OUTKAEDI_DTL_RAPI"
        ds.Tables("LMZ390_TBL_EXISTS").Rows.Add(drTblExists)
        ds = Me.GetTrnTblExits(ds)

        Dim drExists As DataRow()
        drExists = ds.Tables("LMZ390_TBL_EXISTS").Select("TBL_NM = 'H_OUTKAEDI_DTL_RAPI'")
        If drExists.Count > 0 AndAlso drExists(0).Item("TBL_EXISTS").ToString() = "1" Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' 特定の荷主固有のテーブルが存在するか否かの判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function GetTrnTblExits(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "GetTrnTblExits", ds)

    End Function

#End Region ' "検索処理"

#End Region ' "Method"

End Class
