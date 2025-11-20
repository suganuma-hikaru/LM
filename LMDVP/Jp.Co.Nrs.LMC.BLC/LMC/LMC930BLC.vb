' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷
'  プログラムID     :  LMC930    : 現場作業指示
'  作  成  者       :  [hojo]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
Imports System.IO

''' <summary>
''' LMC930BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC930BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"
    ''' <summary>
    ''' ピックステータス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PICK_STATE_KB
        Public Const UNPROCESSED As String = "00"
        Public Const PROCESSING As String = "01"
        Public Const COMPLETED As String = "02"
        Public Const CANCEL As String = "98"
        Public Const DEL As String = "99"
    End Class
    ''' <summary>
    ''' 検品・立ち合いステータス(ヘッダ)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class KENPIN_ATTEND_STATE_KB
        Public Const UNPROCESSED As String = "00"
        Public Const PROCESSING As String = "01"
        Public Const COMPLETED As String = "02"
        Public Const CANCEL As String = "98"
        Public Const DEL As String = "99"
    End Class

    ''' <summary>
    ''' S068 処理状況区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Class PROC_STATE_KB
        Public Const UNPROCESSED As String = "00"
        Public Const COMPLETED As String = "01"
        Public Const PROCESSING As String = "02"
        Public Const CANCEL As String = "03"
    End Class

    ''' <summary>
    ''' 出荷差分比較.商品変更フラグ
    ''' </summary>
    Public Class TC_DIFF_COMPARISON_GOODS_CHANGE_FLG
        Public Const ADD As String = "01"
        Public Const MODIFY As String = "02"
        Public Const DELETE As String = "03"
    End Class

    Public Class FUNCTION_NM
        Public Const WH_SAGYO_SHIJI As String = "WHSagyoShiji"
        Public Const WH_SAGYO_SHIJI_CANCEL As String = "WHSagyoShijiCancel"
        Public Const CHECK_HAITA As String = "CheckHaita"
        Public Const WH_UNSO_UPDATE As String = "UpdateUnsoData"
    End Class

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC930DAC = New LMC930DAC()


#End Region

#Region "Method"

#Region "現場作業指示"
    ''' <summary>
    ''' 現場作業指示
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSLineCsvメソッド呼出</remarks>
    Private Function WHSagyoShiji(ByVal ds As DataSet) As DataSet

        Dim meisaiCancelFlg As Boolean = False  '明細キャンセル対応フラグ

        'タブレット ピックヘッダ取得（TC_PICK_HEADの最新レコード＝旧ヘッダ）
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.PICK_SELECT_HEAD, ds)

        If MyBase.GetResultCount > 0 Then
            'ピックヘッダが既に存在する（新規でない）場合
            If IsMeisaiCancelCust(ds) Then
                '明細キャンセル対応荷主の場合

                Dim dtOldHead As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_PICK_HEAD)
                If dtOldHead.Select("CANCEL_FLG = '01'").Length > 0 Then
                    '現場作業指示取消（キャンセル）後の現場作業指示の場合⇒従来通り
                Else
                    '出荷ピックヘッダ取得（最新の出荷データより＝新ヘッダ）※PICK_SEQが加算されている
                    MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.PICK_SELECT_LMS_DATA, ds)

                    'ヘッダの変更チェック（旧ヘッダと新ヘッダの比較）
                    If IsPickHeadModified(ds) Then
                        '変更がある場合⇒従来通り
                    Else
                        '作業取得
                        'TC_PICK_HEADの最新レコード＝旧作業
                        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.SELECT_TC_SAGYO, ds)
                        '最新のLMS作業データより＝新作業
                        Me.SelectOutkaSagyoData(ds)

                        '作業(大)の変更チェック
                        If IsSagyoLModified(ds) Then
                            '変更がある場合⇒従来通り
                        Else
                            '明細キャンセル対応する
                            meisaiCancelFlg = True
                        End If
                    End If
                End If
            End If
        End If

        If meisaiCancelFlg Then
            '明細単位でキャンセル処理

            '出荷データチェック
            If Not CheckOutkaData(ds) Then
                Return ds
            End If

            'ピックデータ更新
            Me.ModifyPickDataMeisaiCancel(ds)
            If MyBase.IsMessageExist Then
                Return ds
            End If

            '検品データ更新
            Me.ModifyKenpinDataMeisaiCancel(ds)
            If MyBase.IsMessageExist Then
                Return ds
            End If

            '作業データ更新（作業(大)を除く）
            Me.ModifySagyoDataMeisaiCancel(ds)
            If MyBase.IsMessageExist Then
                Return ds
            End If

            'LMSデータ更新
            Me.UpdateLMSData(ds)

        Else
            '従来通り（必要があればヘッダ単位でキャンセル処理）

            'フィルメ特殊処理フラグ取得
            Me.GetFirmenichFlg(ds)

            'ADD Start 2022/12/28 アフトン別名出荷対応
            'セミEDI出荷指示取込データの商品名表示フラグ取得
            Me.GetSemiEdiGoodsnmFlg(ds)
            'ADD End   2022/12/28 アフトン別名出荷対応

            '出荷データチェック
            If Not CheckOutkaData(ds) Then
                Return ds
            End If

            '出荷作業登録
            Me.InsertOutkaSagyoData(ds)

            'ピック・検品キャンセル処理
            Me.CancelOutkaData(ds)

            'ピックデータ登録
            Me.InsertOutkaPickData(ds)

            '出荷差分比較登録
            Me.InsertTcDiffComparison(ds)

            '検品データ登録
            Me.InsertOutkaKenpinData(ds)

            'LMSデータ更新
            Me.UpdateLMSData(ds)

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 明細キャンセル対応荷主か否か
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>True:明細単位でキャンセル処理する荷主 / False:ヘッダ単位でキャンセル処理する荷主</returns>
    Private Function IsMeisaiCancelCust(ByVal ds As DataSet) As Boolean

        'タブレット 明細キャンセル対応フラグ
        Const SubKb As String = "1P"

        Dim drIn As DataRow = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)
        Dim dtMCustDtl As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_M_CUST_DETAILS)

        '荷主コード(大)(中)で検索
        dtMCustDtl.Clear()
        Dim drMCustDtl As DataRow = dtMCustDtl.NewRow
        drMCustDtl("NRS_BR_CD") = drIn("NRS_BR_CD")
        drMCustDtl("CUST_CD") = String.Concat(drIn("CUST_CD_L"), drIn("CUST_CD_M"))
        drMCustDtl("SUB_KB") = SubKb
        dtMCustDtl.Rows.Add(drMCustDtl)

        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.SELECT_M_CUST_DETAILS, ds)

        If MyBase.GetResultCount > 0 Then
            Dim setNaiyo As String = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_M_CUST_DETAILS)(0)("SET_NAIYO").ToString

            If Not String.IsNullOrEmpty(setNaiyo) Then
                '該当データありで設定値が空でない場合
                If setNaiyo = LMConst.FLG.ON Then
                    Return True
                Else
                    Return False
                End If
            End If
        End If

        '荷主コード(大)で検索
        dtMCustDtl.Clear()
        drMCustDtl = dtMCustDtl.NewRow
        drMCustDtl("NRS_BR_CD") = drIn("NRS_BR_CD")
        drMCustDtl("CUST_CD") = drIn("CUST_CD_L")
        drMCustDtl("SUB_KB") = SubKb
        dtMCustDtl.Rows.Add(drMCustDtl)

        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.SELECT_M_CUST_DETAILS, ds)

        If MyBase.GetResultCount > 0 Then
            If ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_M_CUST_DETAILS)(0)("SET_NAIYO").ToString = LMConst.FLG.ON Then
                Return True
            End If
        End If

        Return False

    End Function

    ''' <summary>
    ''' フィルメ特殊処理フラグ取得
    ''' </summary>
    ''' <param name="ds">(入出力)データセット</param>
    ''' <returns>データセット</returns>
    Private Function GetFirmenichFlg(ByVal ds As DataSet) As DataSet

        '0L：セミEDI　フィルメ特殊処理フラグ
        Const SubKb As String = "0L"

        Dim drIn As DataRow = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)
        Dim dtMCustDtl As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_M_CUST_DETAILS)

        '荷主コード(大)で検索
        dtMCustDtl.Clear()
        Dim drMCustDtl As DataRow = dtMCustDtl.NewRow
        drMCustDtl = dtMCustDtl.NewRow
        drMCustDtl("NRS_BR_CD") = drIn("NRS_BR_CD")
        drMCustDtl("CUST_CD") = drIn("CUST_CD_L")
        drMCustDtl("SUB_KB") = SubKb
        dtMCustDtl.Rows.Add(drMCustDtl)

        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.SELECT_M_CUST_DETAILS, ds)

        If MyBase.GetResultCount > 0 Then
            drIn("CUST_DTL_0L") = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_M_CUST_DETAILS)(0)("SET_NAIYO").ToString
        Else
            drIn("CUST_DTL_0L") = ""
        End If

        Return ds

    End Function

    'ADD Start 2022/12/28 アフトン別名出荷対応
    ''' <summary>
    ''' セミEDI出荷指示取込データの商品名表示フラグ取得
    ''' </summary>
    ''' <param name="ds">(入出力)データセット</param>
    ''' <returns>データセット</returns>
    Private Function GetSemiEdiGoodsnmFlg(ByVal ds As DataSet) As DataSet

        'B2：セミEDI出荷指示取込データの商品名表示フラグ
        Const SubKb As String = "B2"

        Dim drIn As DataRow = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0)
        Dim dtMCustDtl As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_M_CUST_DETAILS)

        '荷主コード(大)で検索
        dtMCustDtl.Clear()
        Dim drMCustDtl As DataRow = dtMCustDtl.NewRow
        drMCustDtl = dtMCustDtl.NewRow
        drMCustDtl("NRS_BR_CD") = drIn("NRS_BR_CD")
        drMCustDtl("CUST_CD") = drIn("CUST_CD_L")
        drMCustDtl("SUB_KB") = SubKb
        dtMCustDtl.Rows.Add(drMCustDtl)

        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.SELECT_M_CUST_DETAILS, ds)

        If MyBase.GetResultCount > 0 Then
            drIn("CUST_DTL_B2") = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_M_CUST_DETAILS)(0)("SET_NAIYO").ToString
        Else
            drIn("CUST_DTL_B2") = ""
        End If

        Return ds

    End Function
    'ADD End   2022/12/28 アフトン別名出荷対応

#End Region

#Region "出荷削除処理"
    ''' <summary>
    ''' 出荷削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSLineCsvメソッド呼出</remarks>
    Private Function WHSagyoShijiCancel(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN)
        Dim inDs As DataSet = ds.Clone
        Dim inDt As DataTable = inDs.Tables(LMC930DAC.TABLE_NM.LMC930IN)

        'タブレット対応営業所のチェック
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.CHECK_TABLET_USE, ds)
        If MyBase.GetResultCount() = 0 Then
            Return ds
        End If

        'タブレット ピックヘッダ取得
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.PICK_SELECT_HEAD, ds)

        'タブレット ピックヘッダ取得
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.KENPIN_SELECT_HEAD, ds)

        'ピックキャンセル
        Me.CancelPickData(ds)

        '検品キャンセル
        Me.CancelKenpinData(ds)

        'LMS出荷データ更新
        Me.UpdateLMSData(ds)

        Return ds
    End Function
#End Region

#Region "出荷作業登録"
    ''' <summary>
    ''' 出荷作業登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSLineCsvメソッド呼出</remarks>
    Private Function InsertOutkaSagyoData(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = ds.Clone
        Dim inDt As DataTable = inDs.Tables(LMC930DAC.TABLE_NM.LMC930IN_SAGYO)
        Dim dt As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_SAGYO)

        '出荷作業取得
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.SAGYO_SELECT_LMS_DATA, ds)

        '自社他社チェック
        'KOSU_BAI ='01' かつ IOZS_KB  ='21'を抽出
        Dim drSagyo As DataRow() = dt.Select("KOSU_BAI ='01' AND IOZS_KB = '21' ")
        If drSagyo.Length > 0 Then
            For i As Integer = 0 To drSagyo.Length - 1

                '引当した在庫の棟室の自社他社それぞれの件数を取得する
                Dim inDtJisyaTasya As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_JISYATASYA)
                inDtJisyaTasya.Clear()

                Dim inDrJisyaTasya As DataRow = inDtJisyaTasya.NewRow
                inDrJisyaTasya.Item("NRS_BR_CD") = drSagyo(i).Item("NRS_BR_CD").ToString
                inDrJisyaTasya.Item("OUTKA_NO_L") = drSagyo(i).Item("OUTKA_NO_L").ToString
                inDrJisyaTasya.Item("OUTKA_NO_M") = drSagyo(i).Item("OUTKA_NO_M").ToString
                inDtJisyaTasya.Rows.Add(inDrJisyaTasya)

                'データ取得
                MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.SELECT_JISYATASYA_COUNT, ds)

                '取得結果確認
                Dim outDtJisyaTasya As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_JISYATASYA)
                If outDtJisyaTasya.Rows.Count > 0 Then
                    For j As Integer = 0 To outDtJisyaTasya.Rows.Count - 1
                        '自社0件他社0件以上の場合は基データを更新
                        If "0".Equals(outDtJisyaTasya.Rows(j).Item("JISYA_CNT").ToString) AndAlso
                            Not "0".Equals(outDtJisyaTasya.Rows(j).Item("TASYA_CNT").ToString) Then
                            drSagyo(i).Item("SAGYO_STATE1_KB") = "01"
                            drSagyo(i).Item("SAGYO_STATE2_KB") = "01"
                            drSagyo(i).Item("SAGYO_STATE3_KB") = "01"
                            drSagyo(i).Item("JISYATASYA_KB") = "02"
                        End If
                    Next
                End If
            Next
        End If


        For Each dr As DataRow In dt.Rows

            inDt.Clear()
            inDt.ImportRow(dr)

            '出荷作業登録
            MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.SAGYO_INSERT, inDs)

        Next

        Return ds

    End Function

#End Region

#Region "出荷作業取得"

    ''' <summary>
    ''' 出荷作業取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSLineCsvメソッド呼出</remarks>
    Private Function SelectOutkaSagyoData(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_SAGYO)

        '出荷作業取得
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.SAGYO_SELECT_LMS_DATA, ds)

        '自社他社チェック
        'KOSU_BAI ='01' かつ IOZS_KB  ='21'を抽出
        Dim drSagyo As DataRow() = dt.Select("KOSU_BAI ='01' AND IOZS_KB = '21' ")
        If drSagyo.Length > 0 Then
            For i As Integer = 0 To drSagyo.Length - 1

                '引当した在庫の棟室の自社他社それぞれの件数を取得する
                Dim inDtJisyaTasya As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_JISYATASYA)
                inDtJisyaTasya.Clear()

                Dim inDrJisyaTasya As DataRow = inDtJisyaTasya.NewRow
                inDrJisyaTasya.Item("NRS_BR_CD") = drSagyo(i).Item("NRS_BR_CD").ToString
                inDrJisyaTasya.Item("OUTKA_NO_L") = drSagyo(i).Item("OUTKA_NO_L").ToString
                inDrJisyaTasya.Item("OUTKA_NO_M") = drSagyo(i).Item("OUTKA_NO_M").ToString
                inDtJisyaTasya.Rows.Add(inDrJisyaTasya)

                'データ取得
                MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.SELECT_JISYATASYA_COUNT, ds)

                '取得結果確認
                Dim outDtJisyaTasya As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_JISYATASYA)
                If outDtJisyaTasya.Rows.Count > 0 Then
                    For j As Integer = 0 To outDtJisyaTasya.Rows.Count - 1
                        '自社0件他社0件以上の場合は基データを更新
                        If "0".Equals(outDtJisyaTasya.Rows(j).Item("JISYA_CNT").ToString) AndAlso
                            Not "0".Equals(outDtJisyaTasya.Rows(j).Item("TASYA_CNT").ToString) Then
                            drSagyo(i).Item("SAGYO_STATE1_KB") = "01"
                            drSagyo(i).Item("SAGYO_STATE2_KB") = "01"
                            drSagyo(i).Item("SAGYO_STATE3_KB") = "01"
                            drSagyo(i).Item("JISYATASYA_KB") = "02"
                        End If
                    Next
                End If
            Next
        End If

        Return ds

    End Function

#End Region

#Region "出荷作業更新"

    ''' <summary>
    ''' 明細キャンセル用作業更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function ModifySagyoDataMeisaiCancel(ByVal ds As DataSet) As DataSet

        Dim dtOld As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_SAGYO)  '旧作業
        Dim dtNew As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_SAGYO)   '新作業

        '新作業のWORK_SEQを旧作業と合わせる ※新作業はWORK_SEQが加算されている
        Dim dtOldHead As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_HEAD) '旧検品ヘッダ
        Dim workSeq As String = dtOldHead.Rows(0).Item("KENPIN_ATTEND_SEQ").ToString
        For Each drNew As DataRow In dtNew.Rows
            drNew("WORK_SEQ") = workSeq
        Next

        '追加された作業を探す（作業(大)は対象外）
        For Each drNew As DataRow In dtNew.Select("OUTKA_NO_M <> '000'")
            Dim newRow As Boolean = True

            For Each drOld As DataRow In dtOld.Select("OUTKA_NO_M <> '000'")
                '出荷管理番号M/Sを比較。これらが同一のときは他のキー項目も同一
                If drNew("OUTKA_NO_M").ToString = drOld("OUTKA_NO_M").ToString AndAlso
                   drNew("OUTKA_NO_S").ToString = drOld("OUTKA_NO_S").ToString Then
                    '新作業と出荷管理番号が一致する旧作業がある⇒追加された作業ではない
                    newRow = False
                    Exit For
                End If
            Next

            If newRow Then
                '追加された作業の場合
                drNew("SYS_NEW_FLG") = LMConst.FLG.ON
            End If
        Next


        '削除された作業を探す（作業(大)は対象外）
        For Each drOld As DataRow In dtOld.Select($"OUTKA_NO_M <> '000' AND SYS_DEL_FLG <> '{LMConst.FLG.ON}'")
            Dim delRow As Boolean = True

            For Each drNew As DataRow In dtNew.Select("OUTKA_NO_M <> '000'")
                '出荷管理番号M/Sを比較。これらが同一のときは他のキー項目も同一
                If drOld("OUTKA_NO_M").ToString = drNew("OUTKA_NO_M").ToString AndAlso
                   drOld("OUTKA_NO_S").ToString = drNew("OUTKA_NO_S").ToString Then
                    '旧作業と出荷管理番号が一致する新作業がある⇒削除された作業ではない
                    delRow = False
                    Exit For
                End If
            Next

            If delRow Then
                '削除された作業の場合
                drOld("SYS_DEL_FLG") = LMConst.FLG.ON
            End If
        Next


        'DAC引数用にデータセットの構造をコピー
        Dim dsDacIn As DataSet = ds.Clone

        '作業登録
        For Each drNew As DataRow In dtNew.Select($"SYS_NEW_FLG = '{LMConst.FLG.ON}'")
            dsDacIn.Clear()
            dsDacIn.Tables(LMC930DAC.TABLE_NM.LMC930IN_SAGYO).ImportRow(drNew)
            MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.SAGYO_INSERT, dsDacIn)
        Next

        '作業更新
        For Each drOld As DataRow In dtOld.Select($"SYS_DEL_FLG = '{LMConst.FLG.ON}'")
            dsDacIn.Clear()
            dsDacIn.Tables(LMC930DAC.TABLE_NM.LMC930OUT_SAGYO).ImportRow(drOld)
            MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.SAGYO_DELETE, dsDacIn)
            If MyBase.GetResultCount <> 1 Then
                MyBase.SetMessage("E262", {$"[出荷管理番号:{ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0).Item("OUTKA_NO_L").ToString}]"})
                Return ds
            End If
        Next

        Return ds

    End Function

#End Region

#Region "出荷キャンセル処理"
    ''' <summary>
    ''' 出荷キャンセル処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CancelOutkaData(ByVal ds As DataSet) As DataSet

        'タブレット ピックヘッダ取得
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.PICK_SELECT_HEAD, ds)
        'タブレット 検品ヘッダ取得
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.KENPIN_SELECT_HEAD, ds)
        'ピックキャンセル
        Me.CancelPickData(ds)
        '検品キャンセル
        Me.CancelKenpinData(ds)

        Return ds
    End Function

    ''' <summary>
    ''' ピックキャンセル
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CancelPickData(ByVal ds As DataSet) As DataSet
        If ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_PICK_HEAD).Rows.Count > 0 Then
            '指示済みの場合、ステータスをチェック
            For j As Integer = 0 To ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_PICK_HEAD).Rows.Count - 1

                Dim upDs As DataSet = ds.Clone
                Dim upDt As DataTable = upDs.Tables(LMC930DAC.TABLE_NM.LMC930OUT_PICK_HEAD)
                upDt.ImportRow(ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_PICK_HEAD).Rows(j))

                If LMC930BLC.PICK_STATE_KB.UNPROCESSED.Equals(upDt.Rows(0).Item("PICK_STATE_KB")) Then
                    '未処理の場合は削除
                    MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.PICK_DELETE, upDs)
                ElseIf LMC930BLC.PICK_STATE_KB.CANCEL.Equals(upDt.Rows(0).Item("PICK_STATE_KB")) OrElse
                       LMC930BLC.PICK_STATE_KB.DEL.Equals(upDt.Rows(0).Item("PICK_STATE_KB")) Then
                    '削除、キャンセルの場合なにもしない
                Else
                    '未処理・削除・キャンセル以外の場合
                    If "02".Equals(upDt.Rows(0).Item("JISYATASYA_KB")) Then
                        '自社他社区分が他社の場合は削除
                        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.PICK_DELETE, upDs)
                    Else
                        '自社他社区分が自社の場合はキャンセル
                        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.PICK_CANCEL, upDs)
                    End If

                End If

            Next
        End If
        Return ds
    End Function

    ''' <summary>
    ''' 検品キャンセル
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CancelKenpinData(ByVal ds As DataSet) As DataSet

        If ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_HEAD).Rows.Count > 0 Then
            '指示済みの場合、ステータスをチェック
            For j As Integer = 0 To ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_HEAD).Rows.Count - 1

                Dim upDs As DataSet = ds.Clone
                Dim upDt As DataTable = upDs.Tables(LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_HEAD)
                upDt.ImportRow(ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_HEAD).Rows(j))

                If LMC930BLC.KENPIN_ATTEND_STATE_KB.UNPROCESSED.Equals(upDt.Rows(0).Item("KENPIN_ATTEND_STATE_KB")) Then
                    '指示削除
                    MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.KENPIN_DELETE, upDs)
                ElseIf LMC930BLC.KENPIN_ATTEND_STATE_KB.CANCEL.Equals(upDt.Rows(0).Item("KENPIN_ATTEND_STATE_KB")) OrElse
                       LMC930BLC.KENPIN_ATTEND_STATE_KB.DEL.Equals(upDt.Rows(0).Item("KENPIN_ATTEND_STATE_KB")) Then
                    '削除、キャンセルの場合なにもしない
                Else
                    '指示キャンセル
                    MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.KENPIN_CANCEL, upDs)
                End If

            Next
        End If

        Return ds
    End Function

#End Region

#Region "出荷ピック登録"
    ''' <summary>
    ''' 出荷ピック登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertOutkaPickData(ByVal ds As DataSet) As DataSet

        '出荷ピックヘッダ取得
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.PICK_SELECT_LMS_DATA, ds)

        Dim dtPick As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_PICK_HEAD)
        Dim dtWork As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_SAGYO)

        Dim inDs As DataSet = ds.Clone
        Dim inDt As DataTable = inDs.Tables(LMC930DAC.TABLE_NM.LMC930IN_PICK_HEAD)

        'ピックに対して作業があるかをチェック
        '作業がある場合ピックのデータを更新
        For Each drPick As DataRow In dtPick.Rows
            For Each drWork As DataRow In dtWork.Rows

                If Not String.IsNullOrEmpty(drPick.Item("TOU_NO").ToString) _
                    AndAlso Not String.IsNullOrEmpty(drWork.Item("TOU_NO").ToString) Then
                    '棟室：ピックあり/作業あり かつ ピック＝作業
                    If drPick.Item("TOU_NO").Equals(drWork.Item("TOU_NO").ToString) _
                        AndAlso drPick.Item("SITU_NO").Equals(drWork.Item("SITU_NO").ToString) Then
                        drPick.Item("WORK_STATE_KB") = "01"
                    End If

                ElseIf Not String.IsNullOrEmpty(drPick.Item("TOU_NO").ToString) _
                    AndAlso String.IsNullOrEmpty(drWork.Item("TOU_NO").ToString) Then
                    '棟室：ピックあり/作業なし
                    drPick.Item("WORK_STATE_KB") = "01"
                End If

            Next

            inDt.Clear()
            inDt.ImportRow(drPick)

            '出荷ピック登録(ヘッダ)
            MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.PICK_INSERT_HEAD, inDs)

        Next

        '出荷ピック登録(明細)
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.PICK_INSERT_DTL, ds)

        Return ds

    End Function
#End Region

#Region "出荷ピック更新"

    ''' <summary>
    ''' 明細キャンセル用ピッキングヘッダ・明細更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function ModifyPickDataMeisaiCancel(ByVal ds As DataSet) As DataSet

        Dim dtOldHead As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_PICK_HEAD) '旧ヘッダ
        Dim dtNewHead As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_PICK_HEAD)  '新ヘッダ ※PICK_SEQが加算されている


        Dim removeRowList As List(Of DataRow) = New List(Of DataRow)  '除去対象リスト

        '追加されたヘッダを探す
        For Each drNewHead As DataRow In dtNewHead.Rows
            For Each drOldHead As DataRow In dtOldHead.Rows
                If drNewHead("TOU_NO").ToString = drOldHead("TOU_NO").ToString AndAlso
                   drNewHead("SITU_NO").ToString = drOldHead("SITU_NO").ToString Then
                    '新ヘッダと棟室が一致する旧ヘッダがある⇒追加されたヘッダではない
                    '新ヘッダのデータテーブルからの除去対象リストに追加
                    removeRowList.Add(drNewHead)
                    Exit For
                End If
            Next
        Next

        '追加されたヘッダ以外を新ヘッダのデータテーブルから除去する
        For Each delRow As DataRow In removeRowList
            dtNewHead.Rows.Remove(delRow)
        Next

        'ピックに対して作業があるかをチェック
        '作業がある場合ピックのデータを更新
        For Each drPick As DataRow In dtNewHead.Rows
            For Each drWork As DataRow In ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_SAGYO).Rows

                If Not String.IsNullOrEmpty(drPick.Item("TOU_NO").ToString) _
                AndAlso Not String.IsNullOrEmpty(drWork.Item("TOU_NO").ToString) Then
                    '棟室：ピックあり/作業あり かつ ピック＝作業
                    If drPick.Item("TOU_NO").Equals(drWork.Item("TOU_NO").ToString) _
                    AndAlso drPick.Item("SITU_NO").Equals(drWork.Item("SITU_NO").ToString) Then
                        drPick.Item("WORK_STATE_KB") = "01"
                    End If

                ElseIf Not String.IsNullOrEmpty(drPick.Item("TOU_NO").ToString) _
                AndAlso String.IsNullOrEmpty(drWork.Item("TOU_NO").ToString) Then
                    '棟室：ピックあり/作業なし
                    drPick.Item("WORK_STATE_KB") = "01"
                End If

            Next
        Next

        '新ヘッダのPICK_SEQを旧ヘッダと合わせる ※新ヘッダはPICK_SEQが加算されている
        Dim pickSeq As String = dtOldHead.Rows(0).Item("PICK_SEQ").ToString
        For Each drNewHead As DataRow In dtNewHead.Rows
            drNewHead("PICK_SEQ") = pickSeq
        Next


        'ピック明細取得（TC_PICK_DTLの最新レコード＝旧明細）
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.PICK_SELECT_DTL, ds)
        'ピック明細登録データ取得（最新の出荷データより＝新明細）※PICK_SEQは加算されていない
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.PICK_SELECT_LMS_PICK_DTL_DATA, ds)

        Dim dtOldDtl As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_PICK_DTL)   '旧明細
        Dim dtNewDtl As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_PICK_DTL)    '新明細

        '旧明細のピッキングステージを新明細にコピーする(削除された明細以外に未ピッキングの明細が存在するか否かの判定で参照するため)
        For Each drNewDtl As DataRow In dtNewDtl.Rows
            For Each drOldDtl As DataRow In dtOldDtl.Rows
                '出荷管理番号M/Sを比較。これらが同一のときは他のキー項目も同一
                If drNewDtl("OUTKA_NO_M").ToString = drOldDtl("OUTKA_NO_M").ToString AndAlso
                   drNewDtl("OUTKA_NO_S").ToString = drOldDtl("OUTKA_NO_S").ToString Then
                    drNewDtl("PICK_STATE_KB") = drOldDtl("PICK_STATE_KB")
                    Exit For
                End If
            Next
        Next

        '追加された明細を探す
        For Each drNewDtl As DataRow In dtNewDtl.Rows
            Dim newDtl As Boolean = True

            For Each drOldDtl As DataRow In dtOldDtl.Rows
                '出荷管理番号M/Sを比較。これらが同一のときは他のキー項目も同一
                If drNewDtl("OUTKA_NO_M").ToString = drOldDtl("OUTKA_NO_M").ToString AndAlso
                   drNewDtl("OUTKA_NO_S").ToString = drOldDtl("OUTKA_NO_S").ToString Then
                    '新明細と出荷管理番号が一致する旧明細がある⇒追加された明細ではない
                    newDtl = False
                    Exit For
                End If
            Next

            If newDtl Then
                '追加された明細の場合
                drNewDtl("SYS_NEW_FLG") = LMConst.FLG.ON

                'ヘッダを探す
                For Each drOldHead As DataRow In dtOldHead.Rows
                    If drNewDtl("TOU_NO").ToString = drOldHead("TOU_NO").ToString AndAlso
                       drNewDtl("SITU_NO").ToString = drOldHead("SITU_NO").ToString Then
                        '新明細と棟室が一致する旧ヘッダがある

                        Select Case drOldHead("PICK_STATE_KB").ToString
                            Case PICK_STATE_KB.COMPLETED
                                'ピッキング済→ピッキング中
                                drOldHead("PICK_STATE_KB") = PICK_STATE_KB.PROCESSING
                                drOldHead("SYS_MOD_FLG") = LMConst.FLG.ON
                            Case PICK_STATE_KB.DEL
                                '(削除)→未ピッキング
                                drOldHead("PICK_STATE_KB") = PICK_STATE_KB.UNPROCESSED
                                drOldHead("SYS_MOD_FLG") = LMConst.FLG.ON
                        End Select
                        Exit For
                    End If
                Next
            End If
        Next

        '削除された明細を探す
        For Each drOldDtl As DataRow In dtOldDtl.Select($"CANCEL_DTL_FLG <> '01' AND SYS_DEL_FLG <> '{LMConst.FLG.ON}'")
            Dim delDtl As Boolean = True

            For Each drNewDtl As DataRow In dtNewDtl.Rows
                '出荷管理番号M/Sを比較。これらが同一のときは他のキー項目も同一
                If drOldDtl("OUTKA_NO_M").ToString = drNewDtl("OUTKA_NO_M").ToString AndAlso
                   drOldDtl("OUTKA_NO_S").ToString = drNewDtl("OUTKA_NO_S").ToString Then
                    '旧明細と出荷管理番号が一致する新明細がある⇒削除された明細ではない
                    delDtl = False
                    Exit For
                End If
            Next

            If delDtl Then
                '削除された明細の場合
                drOldDtl("SYS_MOD_FLG") = LMConst.FLG.ON

                Select Case drOldDtl("PICK_STATE_KB").ToString
                    Case PROC_STATE_KB.COMPLETED
                        drOldDtl("CANCEL_DTL_FLG") = "01"
                    Case PROC_STATE_KB.UNPROCESSED
                        drOldDtl("SYS_DEL_FLG") = LMConst.FLG.ON
                End Select

                '削除された明細と同じ棟室の明細を探す
                Dim sameTouSituExists As Boolean = False
                Dim sameTouSituMipickExists As Boolean = False
                For Each drNewDtl As DataRow In dtNewDtl.Rows
                    If drOldDtl("TOU_NO").ToString = drNewDtl("TOU_NO").ToString AndAlso
                       drOldDtl("SITU_NO").ToString = drNewDtl("SITU_NO").ToString Then
                        '旧明細と棟室が一致する新明細がある
                        sameTouSituExists = True
                        If drNewDtl("PICK_STATE_KB").ToString = PROC_STATE_KB.UNPROCESSED Then
                            '削除された明細以外に未ピッキングの明細が存在する
                            sameTouSituMipickExists = True
                            Exit For
                        End If
                    End If
                Next

                'ヘッダを探す
                For Each drOldHead As DataRow In dtOldHead.Rows
                    If drOldDtl("TOU_NO").ToString = drOldHead("TOU_NO").ToString AndAlso
                       drOldDtl("SITU_NO").ToString = drOldHead("SITU_NO").ToString Then

                        If sameTouSituExists Then
                            '削除された明細と同じ棟室の明細がある

                            If drOldHead("PICK_STATE_KB").ToString = PICK_STATE_KB.PROCESSING Then
                                If Not sameTouSituMipickExists Then
                                    '他に未ピッキングの明細がない場合
                                    'ピッキング中→ピッキング済
                                    drOldHead("PICK_STATE_KB") = PICK_STATE_KB.COMPLETED
                                    drOldHead("SYS_MOD_FLG") = LMConst.FLG.ON
                                End If
                            End If

                        Else
                            '削除された明細と同じ棟室の明細がない

                            If drOldHead("PICK_STATE_KB").ToString = PICK_STATE_KB.UNPROCESSED Then
                                '未ピッキング→削除
                                drOldHead("PICK_STATE_KB") = PICK_STATE_KB.DEL
                                drOldHead("SYS_MOD_FLG") = LMConst.FLG.ON
                            End If
                        End If

                        Exit For
                    End If
                Next
            End If
        Next


        'DAC引数用にデータセットの構造をコピー
        Dim dsDacIn As DataSet = ds.Clone

        'ヘッダ登録
        For Each drNewHead As DataRow In dtNewHead.Rows
            dsDacIn.Clear()
            dsDacIn.Tables(LMC930DAC.TABLE_NM.LMC930IN_PICK_HEAD).ImportRow(drNewHead)
            MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.PICK_INSERT_HEAD, dsDacIn)
        Next

        'ヘッダ更新
        For Each drOldHead As DataRow In dtOldHead.Select($"SYS_MOD_FLG = '{LMConst.FLG.ON}'")
            dsDacIn.Clear()
            dsDacIn.Tables(LMC930DAC.TABLE_NM.LMC930OUT_PICK_HEAD).ImportRow(drOldHead)
            MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.UPDATE_PICK_HEAD_MEISAI_CANCEL, dsDacIn)
            If MyBase.GetResultCount <> 1 Then
                MyBase.SetMessage("E262", {$"[出荷管理番号:{ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0).Item("OUTKA_NO_L").ToString}]"})
                Return ds
            End If
        Next

        '明細登録
        For Each drNewHead As DataRow In dtNewDtl.Select($"SYS_NEW_FLG = '{LMConst.FLG.ON}'")
            dsDacIn.Clear()
            dsDacIn.Tables(LMC930DAC.TABLE_NM.LMC930IN_PICK_DTL).ImportRow(drNewHead)
            MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.PICK_INSERT_DTL_PLACEHOLDER, dsDacIn)
        Next

        '明細更新
        For Each drOldDtl As DataRow In dtOldDtl.Select($"SYS_MOD_FLG = '{LMConst.FLG.ON}'")
            dsDacIn.Clear()
            dsDacIn.Tables(LMC930DAC.TABLE_NM.LMC930OUT_PICK_DTL).ImportRow(drOldDtl)
            MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.UPDATE_PICK_DTL_MEISAI_CANCEL, dsDacIn)
            If MyBase.GetResultCount <> 1 Then
                MyBase.SetMessage("E262", {$"[出荷管理番号:{ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0).Item("OUTKA_NO_L").ToString}]"})
                Return ds
            End If
        Next


        Return ds

    End Function

#End Region

#Region "出荷検品登録"
    ''' <summary>
    ''' 出荷検品登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertOutkaKenpinData(ByVal ds As DataSet) As DataSet

        '出荷検品登録(ヘッダ)
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.KENPIN_INSERT_HEAD, ds)
        '出荷検品登録(明細)
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.KENPIN_INSERT_DTL, ds)

        Return ds

    End Function
#End Region

#Region "出荷検品更新"

    ''' <summary>
    ''' 明細キャンセル用検品ヘッダ・明細更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function ModifyKenpinDataMeisaiCancel(ByVal ds As DataSet) As DataSet

        '検品ヘッダ取得
        'TC_PICK_HEADの最新レコード＝旧ヘッダ
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.KENPIN_SELECT_HEAD, ds)

        Dim dtOldHead As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_HEAD) '旧ヘッダ


        '検品明細取得（TC_PICK_DTLの最新レコード＝旧明細）
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.KENPIN_SELECT_DTL, ds)
        '検品明細登録データ取得（最新の出荷データより＝新明細）※PICK_SEQは加算されていない
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.KENPIN_SELECT_LMS_KENPIN_DTL_DATA, ds)

        Dim dtOldDtl As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_DTL)   '旧明細
        Dim dtNewDtl As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_KENPIN_DTL)    '新明細

        '旧明細の検品・立ち合いステージを新明細にコピーする(削除された明細以外に未検品の明細が存在するか否かの判定で参照するため)
        For Each drNewDtl As DataRow In dtNewDtl.Rows
            For Each drOldDtl As DataRow In dtOldDtl.Rows
                '出荷管理番号M/Sを比較。これらが同一のときは他のキー項目も同一
                If drNewDtl("OUTKA_NO_M").ToString = drOldDtl("OUTKA_NO_M").ToString AndAlso
                   drNewDtl("OUTKA_NO_S").ToString = drOldDtl("OUTKA_NO_S").ToString Then
                    drNewDtl("KENPIN_STATE_KB") = drOldDtl("KENPIN_STATE_KB")
                    Exit For
                End If
            Next
        Next

        '追加された明細を探す
        For Each drNewDtl As DataRow In dtNewDtl.Rows
            Dim newDtl As Boolean = True

            For Each drOldDtl As DataRow In dtOldDtl.Rows
                '出荷管理番号M/Sを比較。これらが同一のときは他のキー項目も同一
                If drNewDtl("OUTKA_NO_M").ToString = drOldDtl("OUTKA_NO_M").ToString AndAlso
                   drNewDtl("OUTKA_NO_S").ToString = drOldDtl("OUTKA_NO_S").ToString Then
                    '新明細と出荷管理番号が一致する旧明細がある⇒追加された明細ではない
                    newDtl = False
                    Exit For
                End If
            Next

            If newDtl Then
                '追加された明細の場合
                drNewDtl("SYS_NEW_FLG") = LMConst.FLG.ON

                'ヘッダを更新
                For Each drOldHead As DataRow In dtOldHead.Rows
                    If drOldHead("KENPIN_ATTEND_STATE_KB").ToString = KENPIN_ATTEND_STATE_KB.COMPLETED Then
                        '検品済→検品中
                        drOldHead("KENPIN_ATTEND_STATE_KB") = KENPIN_ATTEND_STATE_KB.PROCESSING
                        drOldHead("SYS_MOD_FLG") = LMConst.FLG.ON
                    End If
                Next
            End If
        Next

        '削除された明細を探す
        For Each drOldDtl As DataRow In dtOldDtl.Select($"CANCEL_DTL_FLG <> '01' AND SYS_DEL_FLG <> '{LMConst.FLG.ON}'")
            Dim delDtl As Boolean = True

            For Each drNewDtl As DataRow In dtNewDtl.Rows
                '出荷管理番号M/Sを比較。これらが同一のときは他のキー項目も同一
                If drOldDtl("OUTKA_NO_M").ToString = drNewDtl("OUTKA_NO_M").ToString AndAlso
                   drOldDtl("OUTKA_NO_S").ToString = drNewDtl("OUTKA_NO_S").ToString Then
                    '旧明細と出荷管理番号が一致する新明細がある⇒削除された明細ではない
                    delDtl = False
                    Exit For
                End If
            Next

            If delDtl Then
                '削除された明細の場合
                drOldDtl("SYS_MOD_FLG") = LMConst.FLG.ON

                Select Case drOldDtl("KENPIN_STATE_KB").ToString
                    Case PROC_STATE_KB.COMPLETED
                        drOldDtl("CANCEL_DTL_FLG") = "01"
                    Case PROC_STATE_KB.UNPROCESSED
                        drOldDtl("SYS_DEL_FLG") = LMConst.FLG.ON
                End Select

                '削除された明細以外の明細（＝新明細）が存在するか
                Dim newDtlExists As Boolean = False
                Dim newDtlMikenpinExists As Boolean = False
                For Each drNewDtl As DataRow In dtNewDtl.Rows
                    newDtlExists = True
                    If drNewDtl("KENPIN_STATE_KB").ToString = PROC_STATE_KB.UNPROCESSED Then
                        '削除された明細以外に未検品の明細が存在する
                        newDtlMikenpinExists = True
                        Exit For
                    End If
                Next

                'ヘッダを更新
                For Each drOldHead As DataRow In dtOldHead.Rows
                    If newDtlExists Then
                        '削除された明細以外の明細が存在する

                        If drOldHead("KENPIN_ATTEND_STATE_KB").ToString = KENPIN_ATTEND_STATE_KB.PROCESSING Then
                            If Not newDtlMikenpinExists Then
                                '他に未検品の明細がない場合
                                '検品中→検品済
                                drOldHead("KENPIN_ATTEND_STATE_KB") = KENPIN_ATTEND_STATE_KB.COMPLETED
                                drOldHead("SYS_MOD_FLG") = LMConst.FLG.ON
                            End If
                        End If

                    Else
                        '削除された明細以外の明細が存在しない

                        If drOldHead("KENPIN_ATTEND_STATE_KB").ToString = KENPIN_ATTEND_STATE_KB.UNPROCESSED Then
                            '未検品→削除
                            drOldHead("KENPIN_ATTEND_STATE_KB") = KENPIN_ATTEND_STATE_KB.DEL
                            drOldHead("SYS_MOD_FLG") = LMConst.FLG.ON
                        End If
                    End If

                    Exit For
                Next
            End If
        Next


        'DAC引数用にデータセットの構造をコピー
        Dim dsDacIn As DataSet = ds.Clone

        'ヘッダ更新
        For Each drOldHead As DataRow In dtOldHead.Select($"SYS_MOD_FLG = '{LMConst.FLG.ON}'")
            dsDacIn.Clear()
            dsDacIn.Tables(LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_HEAD).ImportRow(drOldHead)
            MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.UPDATE_KENPIN_HEAD_MEISAI_CANCEL, dsDacIn)
            If MyBase.GetResultCount <> 1 Then
                MyBase.SetMessage("E262", {$"[出荷管理番号:{ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0).Item("OUTKA_NO_L").ToString}]"})
                Return ds
            End If
        Next

        '明細登録
        For Each drNewHead As DataRow In dtNewDtl.Select($"SYS_NEW_FLG = '{LMConst.FLG.ON}'")
            dsDacIn.Clear()
            dsDacIn.Tables(LMC930DAC.TABLE_NM.LMC930IN_KENPIN_DTL).ImportRow(drNewHead)
            MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.KENPIN_INSERT_DTL_PLACEHOLDER, dsDacIn)
        Next

        '明細更新
        For Each drOldDtl As DataRow In dtOldDtl.Select($"SYS_MOD_FLG = '{LMConst.FLG.ON}'")
            dsDacIn.Clear()
            dsDacIn.Tables(LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_DTL).ImportRow(drOldDtl)
            MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.UPDATE_KENPIN_DTL_MEISAI_CANCEL, dsDacIn)
            If MyBase.GetResultCount <> 1 Then
                MyBase.SetMessage("E262", {$"[出荷管理番号:{ds.Tables(LMC930DAC.TABLE_NM.LMC930IN).Rows(0).Item("OUTKA_NO_L").ToString}]"})
                Return ds
            End If
        Next


        Return ds

    End Function

#End Region

#Region "LMS出荷データ更新"
    ''' <summary>
    ''' LMS出荷データ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateLMSData(ByVal ds As DataSet) As DataSet

        '出荷L更新
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.UPDATE_WH_STATUS, ds)

        Return ds

    End Function
#End Region

#Region "出荷差分比較登録"

    ''' <summary>
    ''' 出荷差分比較登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' ピックヘッダと検品ヘッダとで比較項目の内容は同じなので、ピックヘッダのみ比較する。
    ''' 複数ヘッダある場合も各ヘッダの比較項目の内容は同じなので、1ヘッダのみ比較する。
    ''' </remarks>
    Private Function InsertTcDiffComparison(ByVal ds As DataSet) As DataSet

        Dim dtPick As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_PICK_HEAD)
        If dtPick.Rows.Count = 0 Then Return ds

        'ピックヘッダ取得
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.SELECT_PICK_HEAD_TO_COMPARE, ds)

        'ピックヘッダ比較・差分データ作成
        Dim dtCompHead As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930COMPARE_TC_PICK_HEAD)
        If dtCompHead.Rows.Count <> 2 Then Return ds
        Dim drBefore As DataRow = dtCompHead(0)
        Dim drAfter As DataRow = dtCompHead(1)
        Dim dtDiff As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_TC_DIFF_COMPARISON)
        Dim drDiff As DataRow
        Dim detailsSeq As Integer = 0

        '運送会社名/運送会社支店名
        If drBefore("UNSO_NM").ToString <> drAfter("UNSO_NM").ToString OrElse
           drBefore("UNSO_BR_NM").ToString <> drAfter("UNSO_BR_NM").ToString Then

            detailsSeq += 1
            drDiff = dtDiff.NewRow
            drDiff("NRS_BR_CD") = drAfter("NRS_BR_CD")
            drDiff("OUTKA_NO_L") = drAfter("OUTKA_NO_L")
            drDiff("OUTKA_SEQ") = drAfter("PICK_SEQ")
            drDiff("DETAILS_SEQ") = detailsSeq
            drDiff("ITEM_NAME") = "truckingCo"
            drDiff("BEFORE_VALUE") = drBefore("UNSO_NM").ToString & " " & drBefore("UNSO_BR_NM").ToString
            drDiff("AFTER_VALUE") = drAfter("UNSO_NM").ToString & " " & drAfter("UNSO_BR_NM").ToString
            dtDiff.Rows.Add(drDiff)
        End If

        '届先名/住所1/住所2/住所3
        If drBefore("DEST_NM").ToString <> drAfter("DEST_NM").ToString OrElse
           drBefore("DEST_AD_1").ToString <> drAfter("DEST_AD_1").ToString OrElse
           drBefore("DEST_AD_2").ToString <> drAfter("DEST_AD_2").ToString OrElse
           drBefore("DEST_AD_3").ToString <> drAfter("DEST_AD_3").ToString Then

            detailsSeq += 1
            drDiff = dtDiff.NewRow
            drDiff("NRS_BR_CD") = drAfter("NRS_BR_CD")
            drDiff("OUTKA_NO_L") = drAfter("OUTKA_NO_L")
            drDiff("OUTKA_SEQ") = drAfter("PICK_SEQ")
            drDiff("DETAILS_SEQ") = detailsSeq
            drDiff("ITEM_NAME") = "delivery"
            drDiff("BEFORE_VALUE") = RTrim(drBefore("DEST_NM").ToString & " " & drBefore("DEST_AD_1").ToString & " " & drBefore("DEST_AD_2").ToString & " " & drBefore("DEST_AD_3").ToString)
            drDiff("AFTER_VALUE") = RTrim(drAfter("DEST_NM").ToString & " " & drAfter("DEST_AD_1").ToString & " " & drAfter("DEST_AD_2").ToString & " " & drAfter("DEST_AD_3").ToString)
            dtDiff.Rows.Add(drDiff)
        End If

        '出庫日
        If drBefore("OUTKO_DATE").ToString <> drAfter("OUTKO_DATE").ToString Then

            detailsSeq += 1
            drDiff = dtDiff.NewRow
            drDiff("NRS_BR_CD") = drAfter("NRS_BR_CD")
            drDiff("OUTKA_NO_L") = drAfter("OUTKA_NO_L")
            drDiff("OUTKA_SEQ") = drAfter("PICK_SEQ")
            drDiff("DETAILS_SEQ") = detailsSeq
            drDiff("ITEM_NAME") = "outkoDate"
            drDiff("BEFORE_VALUE") = drBefore("OUTKO_DATE").ToString
            drDiff("AFTER_VALUE") = drAfter("OUTKO_DATE").ToString
            dtDiff.Rows.Add(drDiff)
        End If

        '出荷日
        If drBefore("OUTKA_PLAN_DATE").ToString <> drAfter("OUTKA_PLAN_DATE").ToString Then

            detailsSeq += 1
            drDiff = dtDiff.NewRow
            drDiff("NRS_BR_CD") = drAfter("NRS_BR_CD")
            drDiff("OUTKA_NO_L") = drAfter("OUTKA_NO_L")
            drDiff("OUTKA_SEQ") = drAfter("PICK_SEQ")
            drDiff("DETAILS_SEQ") = detailsSeq
            drDiff("ITEM_NAME") = "pickingDate"
            drDiff("BEFORE_VALUE") = drBefore("OUTKA_PLAN_DATE").ToString
            drDiff("AFTER_VALUE") = drAfter("OUTKA_PLAN_DATE").ToString
            dtDiff.Rows.Add(drDiff)
        End If

        '納入予定日
        If drBefore("ARR_PLAN_DATE").ToString <> drAfter("ARR_PLAN_DATE").ToString Then

            detailsSeq += 1
            drDiff = dtDiff.NewRow
            drDiff("NRS_BR_CD") = drAfter("NRS_BR_CD")
            drDiff("OUTKA_NO_L") = drAfter("OUTKA_NO_L")
            drDiff("OUTKA_SEQ") = drAfter("PICK_SEQ")
            drDiff("DETAILS_SEQ") = detailsSeq
            drDiff("ITEM_NAME") = "arrPlanDate"
            drDiff("BEFORE_VALUE") = drBefore("ARR_PLAN_DATE").ToString
            drDiff("AFTER_VALUE") = drAfter("ARR_PLAN_DATE").ToString
            dtDiff.Rows.Add(drDiff)
        End If

        '納入予定時刻区分
        If drBefore("ARR_PLAN_TIME").ToString <> drAfter("ARR_PLAN_TIME").ToString Then

            detailsSeq += 1
            drDiff = dtDiff.NewRow
            drDiff("NRS_BR_CD") = drAfter("NRS_BR_CD")
            drDiff("OUTKA_NO_L") = drAfter("OUTKA_NO_L")
            drDiff("OUTKA_SEQ") = drAfter("PICK_SEQ")
            drDiff("DETAILS_SEQ") = detailsSeq
            drDiff("ITEM_NAME") = "arrPlanTime"
            drDiff("BEFORE_VALUE") = drBefore("ARR_PLAN_TIME").ToString
            drDiff("AFTER_VALUE") = drAfter("ARR_PLAN_TIME").ToString
            dtDiff.Rows.Add(drDiff)
        End If

        '出荷時注意事項
        If drBefore("REMARK").ToString <> drAfter("REMARK").ToString Then

            detailsSeq += 1
            drDiff = dtDiff.NewRow
            drDiff("NRS_BR_CD") = drAfter("NRS_BR_CD")
            drDiff("OUTKA_NO_L") = drAfter("OUTKA_NO_L")
            drDiff("OUTKA_SEQ") = drAfter("PICK_SEQ")
            drDiff("DETAILS_SEQ") = detailsSeq
            drDiff("ITEM_NAME") = "outboundRemark"
            drDiff("BEFORE_VALUE") = drBefore("REMARK").ToString
            drDiff("AFTER_VALUE") = drAfter("REMARK").ToString
            dtDiff.Rows.Add(drDiff)
        End If

        '現場注意事項
        If drBefore("REMARK_SIJI").ToString <> drAfter("REMARK_SIJI").ToString Then

            detailsSeq += 1
            drDiff = dtDiff.NewRow
            drDiff("NRS_BR_CD") = drAfter("NRS_BR_CD")
            drDiff("OUTKA_NO_L") = drAfter("OUTKA_NO_L")
            drDiff("OUTKA_SEQ") = drAfter("PICK_SEQ")
            drDiff("DETAILS_SEQ") = detailsSeq
            drDiff("ITEM_NAME") = "remarkSiji"
            drDiff("BEFORE_VALUE") = drBefore("REMARK_SIJI").ToString
            drDiff("AFTER_VALUE") = drAfter("REMARK_SIJI").ToString
            dtDiff.Rows.Add(drDiff)
        End If

        '荷主注文番号
        If drBefore("CUST_ORD_NO").ToString <> drAfter("CUST_ORD_NO").ToString Then

            detailsSeq += 1
            drDiff = dtDiff.NewRow
            drDiff("NRS_BR_CD") = drAfter("NRS_BR_CD")
            drDiff("OUTKA_NO_L") = drAfter("OUTKA_NO_L")
            drDiff("OUTKA_SEQ") = drAfter("PICK_SEQ")
            drDiff("DETAILS_SEQ") = detailsSeq
            drDiff("ITEM_NAME") = "ordNo"
            drDiff("BEFORE_VALUE") = drBefore("CUST_ORD_NO").ToString
            drDiff("AFTER_VALUE") = drAfter("CUST_ORD_NO").ToString
            dtDiff.Rows.Add(drDiff)
        End If

        '買主注文番号
        If drBefore("BUYER_ORD_NO").ToString <> drAfter("BUYER_ORD_NO").ToString Then

            detailsSeq += 1
            drDiff = dtDiff.NewRow
            drDiff("NRS_BR_CD") = drAfter("NRS_BR_CD")
            drDiff("OUTKA_NO_L") = drAfter("OUTKA_NO_L")
            drDiff("OUTKA_SEQ") = drAfter("PICK_SEQ")
            drDiff("DETAILS_SEQ") = detailsSeq
            drDiff("ITEM_NAME") = "refNo"
            drDiff("BEFORE_VALUE") = drBefore("BUYER_ORD_NO").ToString
            drDiff("AFTER_VALUE") = drAfter("BUYER_ORD_NO").ToString
            dtDiff.Rows.Add(drDiff)
        End If

        '出荷梱包個数
        If drBefore("OUTKA_PKG_NB").ToString <> drAfter("OUTKA_PKG_NB").ToString Then

            detailsSeq += 1
            drDiff = dtDiff.NewRow
            drDiff("NRS_BR_CD") = drAfter("NRS_BR_CD")
            drDiff("OUTKA_NO_L") = drAfter("OUTKA_NO_L")
            drDiff("OUTKA_SEQ") = drAfter("PICK_SEQ")
            drDiff("DETAILS_SEQ") = detailsSeq
            drDiff("ITEM_NAME") = "packingUnit"
            drDiff("BEFORE_VALUE") = drBefore("OUTKA_PKG_NB").ToString
            drDiff("AFTER_VALUE") = drAfter("OUTKA_PKG_NB").ToString
            dtDiff.Rows.Add(drDiff)
        End If

        '出荷総個数
        If drBefore("OUTKA_TTL_NB").ToString <> drAfter("OUTKA_TTL_NB").ToString Then

            detailsSeq += 1
            drDiff = dtDiff.NewRow
            drDiff("NRS_BR_CD") = drAfter("NRS_BR_CD")
            drDiff("OUTKA_NO_L") = drAfter("OUTKA_NO_L")
            drDiff("OUTKA_SEQ") = drAfter("PICK_SEQ")
            drDiff("DETAILS_SEQ") = detailsSeq
            drDiff("ITEM_NAME") = "totalPkg"
            drDiff("BEFORE_VALUE") = drBefore("OUTKA_TTL_NB").ToString
            drDiff("AFTER_VALUE") = drAfter("OUTKA_TTL_NB").ToString
            dtDiff.Rows.Add(drDiff)
        End If

        '出荷総重量
        If drBefore("OUTKA_TTL_WT").ToString <> drAfter("OUTKA_TTL_WT").ToString Then

            detailsSeq += 1
            drDiff = dtDiff.NewRow
            drDiff("NRS_BR_CD") = drAfter("NRS_BR_CD")
            drDiff("OUTKA_NO_L") = drAfter("OUTKA_NO_L")
            drDiff("OUTKA_SEQ") = drAfter("PICK_SEQ")
            drDiff("DETAILS_SEQ") = detailsSeq
            drDiff("ITEM_NAME") = "weight"
            drDiff("BEFORE_VALUE") = drBefore("OUTKA_TTL_WT").ToString
            drDiff("AFTER_VALUE") = drAfter("OUTKA_TTL_WT").ToString
            dtDiff.Rows.Add(drDiff)
        End If

        '急ぎフラグ
        If drBefore("URGENT_FLG").ToString <> drAfter("URGENT_FLG").ToString Then

            detailsSeq += 1
            drDiff = dtDiff.NewRow
            drDiff("NRS_BR_CD") = drAfter("NRS_BR_CD")
            drDiff("OUTKA_NO_L") = drAfter("OUTKA_NO_L")
            drDiff("OUTKA_SEQ") = drAfter("PICK_SEQ")
            drDiff("DETAILS_SEQ") = detailsSeq
            drDiff("ITEM_NAME") = "quickly"
            drDiff("BEFORE_VALUE") = drBefore("URGENT_FLG").ToString
            drDiff("AFTER_VALUE") = drAfter("URGENT_FLG").ToString
            dtDiff.Rows.Add(drDiff)
        End If

        'ピック明細取得
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.SELECT_PICK_DTL_TO_COMPARE, ds)

        'ピック明細比較・差分データ作成
        For Each drCompDtl As DataRow In ds.Tables(LMC930DAC.TABLE_NM.LMC930COMPARE_TC_PICK_DTL).Rows
            Dim outkaNoSBefore As String = drCompDtl("OUTKA_NO_S_BEFORE").ToString
            Dim outkaNoSAfter As String = drCompDtl("OUTKA_NO_S_AFTER").ToString
            Dim goodsChangeFlg As String

            If Not String.IsNullOrEmpty(outkaNoSBefore) AndAlso
               Not String.IsNullOrEmpty(outkaNoSAfter) AndAlso
               outkaNoSBefore <> outkaNoSAfter Then

                goodsChangeFlg = TC_DIFF_COMPARISON_GOODS_CHANGE_FLG.MODIFY

            ElseIf String.IsNullOrEmpty(outkaNoSBefore) AndAlso
                   Not String.IsNullOrEmpty(outkaNoSAfter) Then

                goodsChangeFlg = TC_DIFF_COMPARISON_GOODS_CHANGE_FLG.ADD

            ElseIf Not String.IsNullOrEmpty(outkaNoSBefore) AndAlso
                   String.IsNullOrEmpty(outkaNoSAfter) Then

                goodsChangeFlg = TC_DIFF_COMPARISON_GOODS_CHANGE_FLG.DELETE

            Else

                Continue For

            End If

            detailsSeq += 1
            drDiff = dtDiff.NewRow
            drDiff("NRS_BR_CD") = drAfter("NRS_BR_CD")
            drDiff("OUTKA_NO_L") = drAfter("OUTKA_NO_L")
            drDiff("OUTKA_SEQ") = drAfter("PICK_SEQ")
            drDiff("DETAILS_SEQ") = detailsSeq
            drDiff("GOODS_CHANGE_FLG") = goodsChangeFlg
            drDiff("GOODS_NM") = drCompDtl("GOODS_NM_NRS")
            dtDiff.Rows.Add(drDiff)
        Next

        If dtDiff.Rows.Count > 0 Then
            '出荷差分比較テーブル登録
            MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.INSERT_DIFF_COMPARISON, ds)
        End If

        Return ds

    End Function
#End Region

#Region "ヘッダ比較"

    ''' <summary>
    ''' ピッキングヘッダに変更があるか否か
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>True:変更あり / False:変更なし</returns>
    ''' <remarks>
    ''' 複数ヘッダある場合も各ヘッダの比較項目の内容は同じなので、1ヘッダのみ比較する。
    ''' </remarks>
    Private Function IsPickHeadModified(ByVal ds As DataSet) As Boolean

        Dim dtOld As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_PICK_HEAD)
        Dim dtNew As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_PICK_HEAD)

        If IsPickHeadRowModified(dtOld(0), dtNew(0)) Then
            '変更がある場合
            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' ピッキングヘッダのレコードに変更があるか否か
    ''' </summary>
    ''' <param name="drBefore">変更前データ</param>
    ''' <param name="drAfter">変更後データ</param>
    ''' <returns>True:変更あり / False:変更なし</returns>
    Private Function IsPickHeadRowModified(ByVal drBefore As DataRow, ByVal drAfter As DataRow) As Boolean

        '運送会社名/運送会社支店名
        If drBefore("UNSO_NM").ToString <> drAfter("UNSO_NM").ToString OrElse
           drBefore("UNSO_BR_NM").ToString <> drAfter("UNSO_BR_NM").ToString Then
            Return True
        End If

        '届先名/住所1/住所2/住所3
        If drBefore("DEST_NM").ToString <> drAfter("DEST_NM").ToString OrElse
           drBefore("DEST_AD_1").ToString <> drAfter("DEST_AD_1").ToString OrElse
           drBefore("DEST_AD_2").ToString <> drAfter("DEST_AD_2").ToString OrElse
           drBefore("DEST_AD_3").ToString <> drAfter("DEST_AD_3").ToString Then
            Return True
        End If

        '届先電話番号
        If drBefore("DEST_TEL").ToString <> drAfter("DEST_TEL").ToString Then
            Return True
        End If

        '出庫日
        If drBefore("OUTKO_DATE").ToString <> drAfter("OUTKO_DATE").ToString Then
            Return True
        End If

        '出荷日
        If drBefore("OUTKA_PLAN_DATE").ToString <> drAfter("OUTKA_PLAN_DATE").ToString Then
            Return True
        End If

        '納入予定日
        If drBefore("ARR_PLAN_DATE").ToString <> drAfter("ARR_PLAN_DATE").ToString Then
            Return True
        End If

        '納入予定時刻区分
        If drBefore("ARR_PLAN_TIME").ToString <> drAfter("ARR_PLAN_TIME").ToString Then
            Return True
        End If

        '出荷時注意事項
        If drBefore("REMARK").ToString <> drAfter("REMARK").ToString Then
            Return True
        End If

        '現場注意事項
        If drBefore("REMARK_SIJI").ToString <> drAfter("REMARK_SIJI").ToString Then
            Return True
        End If

        '荷主注文番号
        If drBefore("CUST_ORD_NO").ToString <> drAfter("CUST_ORD_NO").ToString Then
            Return True
        End If

        '買主注文番号
        If drBefore("BUYER_ORD_NO").ToString <> drAfter("BUYER_ORD_NO").ToString Then
            Return True
        End If

        '出荷梱包個数
        If drBefore("OUTKA_PKG_NB").ToString <> drAfter("OUTKA_PKG_NB").ToString Then
            Return True
        End If

        '出荷総個数
        If drBefore("OUTKA_TTL_NB").ToString <> drAfter("OUTKA_TTL_NB").ToString Then
            Return True
        End If

        '出荷総重量
        If drBefore("OUTKA_TTL_WT").ToString <> drAfter("OUTKA_TTL_WT").ToString Then
            Return True
        End If

        '急ぎフラグ
        If drBefore("URGENT_FLG").ToString <> drAfter("URGENT_FLG").ToString Then
            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' 付帯作業(大)に変更があるか否か
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>True:変更あり / False:変更なし</returns>
    Private Function IsSagyoLModified(ByVal ds As DataSet) As Boolean

        Dim dtOld As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930OUT_SAGYO)  '旧作業
        Dim dtNew As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN_SAGYO)   '新作業

        '追加された作業(大)を探す
        For Each drNew As DataRow In dtNew.Select("OUTKA_NO_M = '000'")
            Dim newRow As Boolean = True

            For Each drOld As DataRow In dtOld.Select("OUTKA_NO_M = '000'")
                If drNew("SAGYO_REC_NO").ToString = drOld("SAGYO_REC_NO").ToString Then
                    '新作業と作業レコード番号が一致する旧作業がある⇒追加された作業ではない
                    newRow = False
                    Exit For
                End If
            Next

            If newRow Then
                '追加された作業(大)がある場合
                Return True
            End If
        Next

        '削除された作業(大)を探す
        For Each drOld As DataRow In dtOld.Select("OUTKA_NO_M = '000'")
            Dim delRow As Boolean = True

            For Each drNew As DataRow In dtNew.Select("OUTKA_NO_M = '000'")
                If drOld("SAGYO_REC_NO").ToString = drNew("SAGYO_REC_NO").ToString Then
                    '旧作業と作業レコード番号が一致する新作業がある⇒削除された作業ではない
                    delRow = False
                    Exit For
                End If
            Next

            If delRow Then
                '削除された作業(大)がある場合
                Return True
            End If
        Next

        Return False

    End Function

#End Region

#Region "運送情報更新"
    ''' <summary>
    ''' 運送情報更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateUnsoData(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = ds.Copy
        Dim inDt As DataTable = inDs.Tables(LMC930DAC.TABLE_NM.LMC930IN)

        '出荷管理番号が空の場合、運送情報から取得する
        If String.IsNullOrEmpty(inDt.Rows(0).Item("OUTKA_NO_L").ToString) Then
            MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.SELECT_UNSO_DATA, inDs)
            If MyBase.GetResultCount() = 0 Then
                Return ds
            End If
            inDt.Rows(0).Item("OUTKA_NO_L") = inDs.Tables("LMC930_F_UNSO_L").Rows(0).Item("OUTKA_NO_L").ToString
        End If

        'タブレット 検品ヘッダ取得
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.KENPIN_SELECT_HEAD, inDs)
        '指示済データがない場合更新しない
        If MyBase.GetResultCount() = 0 Then
            Return ds
        End If

        'ステータスがキャンセルフラグON、キャンセル済み、削除済みの場合は更新しない
        Dim kenpinAttendStateKb As String = inDs.Tables(LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_HEAD).Rows(0).Item("KENPIN_ATTEND_STATE_KB").ToString
        If "01".Equals(inDs.Tables(LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_HEAD).Rows(0).Item("CANCEL_FLG").ToString) _
            OrElse KENPIN_ATTEND_STATE_KB.CANCEL.Equals(kenpinAttendStateKb) _
            OrElse KENPIN_ATTEND_STATE_KB.DEL.Equals(kenpinAttendStateKb) Then
            Return ds
        End If

        'タブレット対応営業所のチェック
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.CHECK_TABLET_USE, inDs)
        If MyBase.GetResultCount() = 0 Then
            Return ds
        End If

        '倉庫情報取得
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.SELECT_SOKO_DATA, inDs)
        If MyBase.GetResultCount() = 0 Then
            Return ds
        End If
        '運送情報変更対象確認
        If Not "01".Equals(inDs.Tables(LMC930DAC.TABLE_NM.LMC930_M_SOKO).Rows(0).Item("WH_UNSO_CHG_YN").ToString) Then
            Return ds
        End If

        '以下は運送会社一括更新が必要な場合の処理
        '検品明細取得
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.KENPIN_SELECT_DTL, inDs)

        '運送会社取得
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.SELECT_UNSOCO_DATA, inDs)

        '更新用データセット作成
        Dim unsocoDt As DataTable = inDs.Tables(LMC930DAC.TABLE_NM.LMC930_M_UNSOCO)
        Dim inUnsoDt As DataTable = inDs.Tables(LMC930DAC.TABLE_NM.LMC930OUT_UNSO)
        Dim preNihudaChkFlg As String = inDs.Tables(LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_HEAD).Rows(0).Item("NIHUDA_CHK_FLG").ToString


        For Each dr As DataRow In inDs.Tables(LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_DTL).Select($"CANCEL_DTL_FLG <> '01' AND SYS_DEL_FLG <> '{LMConst.FLG.ON}'")

            Dim inUnsoDr As DataRow = inUnsoDt.NewRow
            inUnsoDr.Item("NRS_BR_CD") = inDt.Rows(0).Item("NRS_BR_CD").ToString
            inUnsoDr.Item("OUTKA_NO_L") = inDt.Rows(0).Item("OUTKA_NO_L").ToString
            inUnsoDr.Item("SEQ") = inDs.Tables(LMC930DAC.TABLE_NM.LMC930OUT_KENPIN_HEAD).Rows(0).Item("KENPIN_ATTEND_SEQ").ToString
            inUnsoDr.Item("UNSO_CD") = unsocoDt.Rows(0).Item("UNSO_CD").ToString
            inUnsoDr.Item("UNSO_NM") = unsocoDt.Rows(0).Item("UNSO_NM").ToString
            inUnsoDr.Item("UNSO_BR_CD") = unsocoDt.Rows(0).Item("UNSO_BR_CD").ToString
            inUnsoDr.Item("UNSO_BR_NM") = unsocoDt.Rows(0).Item("UNSO_BR_NM").ToString
            inUnsoDr.Item("NIHUDA_CHK_FLG") = unsocoDt.Rows(0).Item("NIHUDA_CHK_FLG").ToString
            inUnsoDr.Item("UNSO_CHG_FLG") = "01"
            inUnsoDr.Item("OUTKA_NO_M") = dr.Item("OUTKA_NO_M").ToString
            inUnsoDr.Item("OUTKA_NO_S") = dr.Item("OUTKA_NO_S").ToString
            inUnsoDr.Item("KENPIN_DTL_SEQ") = dr.Item("KENPIN_DTL_SEQ").ToString

            If Not "01".Equals(preNihudaChkFlg) AndAlso
                Not "01".Equals(unsocoDt.Rows(0).Item("NIHUDA_CHK_FLG").ToString) Then
                inUnsoDr.Item("KENPIN_ATTEND_STATE_KB") = kenpinAttendStateKb
                inUnsoDr.Item("KENPIN_STATE_KB") = dr.Item("KENPIN_STATE_KB").ToString
                inUnsoDr.Item("ATTEND_STATE_KB") = dr.Item("ATTEND_STATE_KB").ToString
            Else
                inUnsoDr.Item("KENPIN_ATTEND_STATE_KB") = "00"
                inUnsoDr.Item("KENPIN_STATE_KB") = "00"
                inUnsoDr.Item("ATTEND_STATE_KB") = "00"
            End If
            inUnsoDt.Rows.Add(inUnsoDr)
        Next

        'ピックヘッダ更新
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.PICK_UPDATE_UNSO_DATA, inDs)

        '検品ヘッダ更新
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.KENPIN_HEAD_UPDATE_UNSO_DATA, inDs)

        '検品明細更新        
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.KENPIN_DTL_UPDATE_UNSO_DATA, inDs)

        Return ds

    End Function
#End Region

#Region "データチェック"
    ''' <summary>
    ''' 出荷データチェック処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSLineCsvメソッド呼出</remarks>
    Private Function CheckOutkaData(ByVal ds As DataSet) As Boolean

        Dim dtIn As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN)
        Dim dtChk As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930CHECK)
        Dim chk As Boolean = True

        'ステータスチェック(検品済、完了済み以外エラー)
        If Not ("50".Equals(dtIn.Rows(0).Item("OUTKA_STATE_KB").ToString) OrElse
             "60".Equals(dtIn.Rows(0).Item("OUTKA_STATE_KB").ToString)) Then
            MyBase.SetMessageStore("00",
                                   "E991",
                                   New String() {dtIn.Rows(0).Item("OUTKA_NO_L").ToString}, dtIn.Rows(0).Item("ROW_NO").ToString)
            'ステータスエラーの場合はここでチェック終了
            Return False
        End If

        'チェック用データ取得
        MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.CHECK_OUTKA_DATA, ds)
        'データ取得件数が0件の場合エラー
        If MyBase.GetResultCount() = 0 Then
            MyBase.SetMessageStore("00",
                                   "E024",
                                   New String() {dtIn.Rows(0).Item("ROW_NO").ToString})
            chk = False
        End If

        '【出荷Lに対するチェック】
        '作業指示ステータスチェック(指示済みの場合エラー)
        If "01".Equals(dtChk.Rows(0).Item("WH_TAB_STATUS").ToString) Then
            MyBase.SetMessageStore("00",
                                   "E01D",
                                   New String() {dtIn.Rows(0).Item("OUTKA_NO_L").ToString}, dtIn.Rows(0).Item("ROW_NO").ToString)
            'ステータスエラーの場合はここでチェック終了
            Return False
        End If

        '現場作業対象チェック
        If Not "01".Equals(dtChk.Rows(0).Item("WH_TAB_YN").ToString) Then
            MyBase.SetMessageStore("00",
                                   "E00I",
                                   New String() {dtIn.Rows(0).Item("OUTKA_NO_L").ToString}, dtIn.Rows(0).Item("ROW_NO").ToString)
            chk = False
        End If

        '【出荷Mに対するチェック】
        '引当未済チェック
        Dim outkaNoM As String = String.Empty
        For Each drChk As DataRow In dtChk.Rows
            If Not outkaNoM.Equals(drChk.Item("OUTKA_NO_M").ToString) Then
                'Mが変わった時だけ実施
                If Integer.Parse(drChk.Item("BACKLOG_NB").ToString) > 0 Then
                    MyBase.SetMessageStore("00",
                                           "E114",
                                           New String() {dtIn.Rows(0).Item("OUTKA_NO_L").ToString}, dtIn.Rows(0).Item("ROW_NO").ToString)
                    chk = False
                    Exit For
                End If
            End If
        Next

        '【出荷Sに対するチェック】
        Dim jisya As Boolean = False
        Dim tasya As Boolean = False
        For Each drChk As DataRow In dtChk.Rows
            If Not String.IsNullOrEmpty(drChk.Item("OUTKA_NO_S").ToString) Then

                '主担当作業者のチェック
                If Not "02".Equals(drChk.Item("JISYATASYA_KB").ToString) AndAlso
                    String.IsNullOrEmpty(drChk.Item("USER_NM").ToString) Then
                    MyBase.SetMessageStore("00",
                                            "E00J",
                                            New String() {drChk.Item("TOU_NO").ToString,
                                                          drChk.Item("SITU_NO").ToString},
                                                          dtIn.Rows(0).Item("ROW_NO").ToString)
                    chk = False
                End If

                '自社他社区分チェック
                If "02".Equals(drChk.Item("JISYATASYA_KB").ToString) Then
                    tasya = True
                Else
                    jisya = True
                End If
            End If
        Next

        If jisya = False AndAlso tasya = True Then
            MyBase.SetMessageStore("00",
                                    "E01B",
                                    New String() {dtIn.Rows(0).Item("OUTKA_NO_L").ToString}, dtIn.Rows(0).Item("ROW_NO").ToString)
            chk = False
        End If

        Return chk

    End Function
#End Region

#Region "排他チェック"
    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaita(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMC930DAC.TABLE_NM.LMC930IN)

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, LMC930DAC.FUNCTION_NM.CHECK_HAITA, ds)

        Return ds

    End Function
#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' DACクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DacAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallDAC(Me._Dac, actionId, ds)

    End Function

#End Region

#End Region

End Class
