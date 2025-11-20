' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME040  : 作業指示書編集
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LME040BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LME040BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LME040DAC = New LME040DAC()

    ''' <summary>
    ''' データ作成判定フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _MakeFlg As Boolean = False

#End Region

#Region "Method"

#Region "初期検索処理"

    ''' <summary>
    ''' 対象データの検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        '対象データの検索処理

        '作業指示書データの検索
        ds = MyBase.CallDAC(Me._Dac, "SelectDataSagyoSiji", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '作業データの検索
        ds = MyBase.CallDAC(Me._Dac, "SelectDataSagyo", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function

#End Region

#Region "保存処理"

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function HozonData(ByVal ds As DataSet) As DataSet

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        Dim rtnDs As DataSet = Nothing
        Dim dr() As DataRow = Nothing
        Dim sagyoSijiNo As String = String.Empty
        Dim sagyoRecNo As String = String.Empty
        Dim max As Integer = 0

        '作業指示書
        If String.IsNullOrEmpty(ds.Tables("LME040INOUT_SAGYO_SIJI").Rows(0).Item("SAGYO_SIJI_NO").ToString) = True Then
            '作業指示書番号の採番
            sagyoSijiNo = Me.GetSagyoSijiNo(ds)
            ds.Tables("LME040INOUT_SAGYO_SIJI").Rows(0).Item("SAGYO_SIJI_NO") = sagyoSijiNo

            '新規
            rtnDs = MyBase.CallDAC(Me._Dac, "InsertSagyoSiji", ds)
        Else
            sagyoSijiNo = ds.Tables("LME040INOUT_SAGYO_SIJI").Rows(0).Item("SAGYO_SIJI_NO").ToString

            '更新
            rtnDs = MyBase.CallDAC(Me._Dac, "UpdateSagyosiji", ds)
        End If
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If


        '作業レコード
        dr = ds.Tables("LME040INOUT_SAGYO").Select(String.Concat("(UPD_FLG = '1' OR UPD_FLG = '2') AND ", _
                                                                 "SAGYO_CD <> '", String.Empty, "'"), _
                                                                 "KEY_NO,SAGYO_CD")
        max = dr.Length - 1
        For i As Integer = 0 To max
            dr(i).Item("SAGYO_SIJI_NO") = sagyoSijiNo
            If String.IsNullOrEmpty(dr(i).Item("SAGYO_REC_NO").ToString) = True Then
                '作業レコード番号の採番
                sagyoRecNo = Me.GetSagyoRecNo(ds)
                dr(i).Item("SAGYO_REC_NO") = sagyoRecNo
            End If

            '値のクリア
            inTbl = setDs.Tables("LME040INOUT_SAGYO")
            inTbl.Clear()

            '条件の設定
            inTbl.ImportRow(dr(i))

            If ("2").Equals(dr(i).Item("UPD_FLG").ToString) = True Then
                '削除
                rtnDs = MyBase.CallDAC(Me._Dac, "DeleteSagyoRec", setDs)
                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If

            End If

            If ("1").Equals(dr(i).Item("UPD_FLG").ToString) = True Then
                '追加
                rtnDs = MyBase.CallDAC(Me._Dac, "InsertSagyoRec", setDs)
                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If
            End If

        Next

        '作業指示書データの検索
        Dim inDr As DataRow = ds.Tables("LME040IN").NewRow
        inDr.Item("SAGYO_SIJI_NO") = sagyoSijiNo
        inDr.Item("NRS_BR_CD") = ds.Tables("LME040INOUT_SAGYO_SIJI").Rows(0).Item("NRS_BR_CD").ToString
        ds.Tables("LME040IN").Rows.Add(inDr)
        ds = MyBase.CallDAC(Me._Dac, "SelectDataSagyoSiji", ds)
        Return ds

    End Function

    ''' <summary>
    ''' 作業指示書番号を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>OutkaNoL</returns>
    ''' <remarks></remarks>
    Private Function GetSagyoSijiNo(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_SIJI_NO, Me, ds.Tables("LME040INOUT_SAGYO_SIJI").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' 作業明細番号を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>OutkaNoL</returns>
    ''' <remarks></remarks>
    Private Function GetSagyoRecNo(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, ds.Tables("LME040INOUT_SAGYO_SIJI").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

#End Region

#Region "削除処理"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        Dim rtnDs As DataSet = Nothing
        Dim sagyoSijiNo As String = String.Empty
        Dim sagyoRecNo As String = String.Empty
        Dim max As Integer = 0

        sagyoSijiNo = ds.Tables("LME040INOUT_SAGYO_SIJI").Rows(0).Item("SAGYO_SIJI_NO").ToString

        '作業指示書
        rtnDs = MyBase.CallDAC(Me._Dac, "DeleteSagyosiji", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '作業
        For Each dr As DataRow In ds.Tables("LME040INOUT_SAGYO").Rows
            dr.Item("SAGYO_SIJI_NO") = sagyoSijiNo

            '値のクリア
            inTbl = setDs.Tables("LME040INOUT_SAGYO")
            inTbl.Clear()

            '条件の設定
            inTbl.ImportRow(dr)

            '削除
            rtnDs = MyBase.CallDAC(Me._Dac, "DeleteSagyoRec", setDs)
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        Next

        Return ds

    End Function

    ' '' ''' <summary>
    ' '' ''' 作業指示書番号を取得
    ' '' ''' </summary>
    ' '' ''' <param name="ds">DataSet</param>
    ' '' ''' <returns>OutkaNoL</returns>
    ' '' ''' <remarks></remarks>
    ' ''Private Function GetSagyoSijiNo(ByVal ds As DataSet) As String

    ' ''    Dim num As New NumberMasterUtility
    ' ''    Return num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_SIJI_NO, Me, ds.Tables("LME040INOUT_SAGYO_SIJI").Rows(0).Item("NRS_BR_CD").ToString())

    ' ''End Function

    ' '' ''' <summary>
    ' '' ''' 作業明細番号を取得
    ' '' ''' </summary>
    ' '' ''' <param name="ds">DataSet</param>
    ' '' ''' <returns>OutkaNoL</returns>
    ' '' ''' <remarks></remarks>
    ' ''Private Function GetSagyoRecNo(ByVal ds As DataSet) As String

    ' ''    Dim num As New NumberMasterUtility
    ' ''    Return num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, ds.Tables("LME040INOUT_SAGYO_SIJI").Rows(0).Item("NRS_BR_CD").ToString())

    ' ''End Function

#End Region
#End Region

End Class
