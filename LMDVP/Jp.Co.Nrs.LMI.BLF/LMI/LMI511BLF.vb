' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI511BLF : JNC EDI
'  作  成  者       :  
' ==========================================================================
Imports System.Transactions
Imports System.Linq
Imports System.Collections
Imports System.Collections.Generic
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI511BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI511BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Const"

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Class TABLE_NM
        Public Const IN_BO_MST As String = "LMI511IN_BO_MST"
        Public Const OUT_BO_MST As String = "LMI511OUT_BO_MST"
        Public Const INOUT_OLD_DATA_CHK As String = "LMI511INOUT_OLD_DATA_CHK"
        Public Const INOUT_GET_AUTO_CD As String = "LMI511INOUT_GET_AUTO_CD"
        Public Const IN_SEARCH As String = "LMI511IN_SEARCH"
        Public Const OUT As String = "LMI511OUT"
        Public Const IN_HED As String = "LMI511IN_HED"
        Public Const OUT_HED As String = "LMI511OUT_HED"
        Public Const IN_DTL As String = "LMI511IN_DTL"
        Public Const OUT_DTL As String = "LMI511OUT_DTL"
        Public Const OUT_HED_EDI As String = "LMI511OUT_HED_EDI"
        Public Const OUT_DTL_EDI As String = "LMI511OUT_DTL_EDI"
        Public Const IN_EDI_L As String = "LMI511IN_H_OUTKAEDI_L"
        Public Const OUT_EDI_L As String = "LMI511OUT_H_OUTKAEDI_L"
        Public Const IN_EDI_M As String = "LMI511IN_H_OUTKAEDI_M"
        Public Const IN_PRT1 As String = "LMI511IN_PRT1"
        Public Const IN_PRT2 As String = "LMI511IN_PRT2"
        Public Const IN_PRT3 As String = "LMI511IN_PRT3"
        Public Const IN_PRT4 As String = "LMI511IN_PRT4"
        Public Const IN_PRT5 As String = "LMI511IN_PRT5"
        Public Const IN_IDX As String = "LMI511IN_IDX"

        Public Const LMI512_IN As String = "LMI512IN"
        Public Const LMI514_IN As String = "LMI514IN"
        Public Const LMI515_IN As String = "LMI515IN"
        Public Const LMI516_IN As String = "LMI516IN"
        Public Const LMI517_IN As String = "LMI517IN"

    End Class

    ''' <summary>
    ''' 入出庫区分
    ''' </summary>
    ''' <remarks></remarks>
    Private Class INOUT_KB
        ''' <summary>
        ''' 出庫
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTKA As String = "0"
        ''' <summary>
        ''' 入庫
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INKA As String = "1"
    End Class

    ''' <summary>
    ''' データ種別
    ''' </summary>
    ''' <remarks></remarks>
    Private Class DATA_KIND
        ''' <summary>
        ''' 出荷
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTKA As String = "4001"
        ''' <summary>
        ''' 入荷
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INKA As String = "4101"
        ''' <summary>
        ''' 運送
        ''' </summary>
        ''' <remarks></remarks>
        Public Const UNSO As String = "3001"
    End Class

    ''' <summary>
    ''' 赤黒区分
    ''' </summary>
    ''' <remarks></remarks>
    Private Class RB_KBN
        ''' <summary>
        ''' 赤
        ''' </summary>
        ''' <remarks></remarks>
        Public Const RED As String = "1"
        ''' <summary>
        ''' 黒
        ''' </summary>
        ''' <remarks></remarks>
        Public Const BLACK As String = "2"
    End Class

    ''' <summary>
    ''' 変更区分
    ''' </summary>
    ''' <remarks></remarks>
    Private Class MOD_KBN
        ''' <summary>
        ''' 新
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SIN As String = "1"
        ''' <summary>
        ''' 訂
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TEI As String = "3"
        ''' <summary>
        ''' 消
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SHO As String = "9"
        ''' <summary>
        ''' ×
        ''' </summary>
        ''' <remarks></remarks>
        Public Const BAT As String = "E"
        ''' <summary>
        ''' 遅
        ''' </summary>
        ''' <remarks></remarks>
        Public Const LOW As String = "L"
    End Class

    ''' <summary>
    ''' 報告区分
    ''' </summary>
    ''' <remarks></remarks>
    Private Class RTN_FLG
        ''' <summary>
        ''' 未送
        ''' </summary>
        ''' <remarks></remarks>
        Public Const MISO As String = "0"
        ''' <summary>
        ''' 送待
        ''' </summary>
        ''' <remarks></remarks>
        Public Const WAIT As String = "1"
        ''' <summary>
        ''' 完了
        ''' </summary>
        ''' <remarks></remarks>
        Public Const COMP As String = "2"
    End Class

    ''' <summary>
    ''' 送信訂正区分
    ''' </summary>
    ''' <remarks></remarks>
    Private Class SND_CANCEL_FLG
        ''' <summary>
        ''' なし
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NORMAL As String = "0"
        ''' <summary>
        ''' 修赤
        ''' </summary>
        ''' <remarks></remarks>
        Public Const RED As String = "1"
        ''' <summary>
        ''' 修黒
        ''' </summary>
        ''' <remarks></remarks>
        Public Const BLACK As String = "2"
    End Class

    ''' <summary>
    ''' 旧データ区分
    ''' </summary>
    ''' <remarks></remarks>
    Private Class OLD_DATA_FLG
        ''' <summary>
        ''' 旧データでない
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SIN As String = ""
        ''' <summary>
        ''' 旧データである
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OLD As String = "Y"
    End Class

    ''' <summary>
    ''' 帳票種類
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum PrintType
        ''' <summary>
        ''' 出荷予定表
        ''' </summary>
        ''' <remarks></remarks>
        OUTKA_YOTEI = 0
        ''' <summary>
        ''' 酢酸出荷依頼書(川本倉庫)
        ''' </summary>
        SAKUSAN_KAWAMOTO
        ''' <summary>
        ''' ファクシミリ連絡票(三菱ケミカル)
        ''' </summary>
        FAX_MITSUBISHI_CHEMICAL
        ''' <summary>
        ''' 酢酸注文書(KHネオケム)
        ''' </summary>
        SAKUSAN_KH_NEOKEMU
        ''' <summary>
        ''' イソブタノール依頼書(ｼﾝｺｰｹﾐ神戸)
        ''' </summary>
        IBA_SHINKO_CHEMICAL_KOBE
    End Enum

#End Region 'Const

#Region "Method"

#Region "共通処理"

    ''' <summary>
    ''' データテーブルのカレントインデックスを設定(DACで参照)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="idx">カレントインデックス</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetCurrentIndexIn(ByVal ds As DataSet, ByVal idx As Integer) As DataSet

        ds.Tables(TABLE_NM.IN_IDX).Rows.Clear()
        Dim dr As DataRow = ds.Tables(TABLE_NM.IN_IDX).NewRow()
        dr("IDX") = idx
        ds.Tables(TABLE_NM.IN_IDX).Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' 採番：JNCまとめ番号
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function GetNewCombiNo(ByVal ds As DataSet) As DataSet

        Dim nrsBrCd As String = ds.Tables(TABLE_NM.INOUT_GET_AUTO_CD).Rows(0).Item("NRS_BR_CD").ToString()

        Dim num As New NumberMasterUtility
        ds.Tables(TABLE_NM.INOUT_GET_AUTO_CD).Rows(0).Item("NEW_CD") = num.GetAutoCode(NumberMasterUtility.NumberKbn.JNC_MTM_NO, Me, nrsBrCd)

        Return ds

    End Function

    ''' <summary>
    ''' 採番：JNC印刷番号
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function GetNewPrintNo(ByVal ds As DataSet) As DataSet

        Dim nrsBrCd As String = ds.Tables(TABLE_NM.INOUT_GET_AUTO_CD).Rows(0).Item("NRS_BR_CD").ToString()

        Dim num As New NumberMasterUtility
        ds.Tables(TABLE_NM.INOUT_GET_AUTO_CD).Rows(0).Item("NEW_CD") = num.GetAutoCode(NumberMasterUtility.NumberKbn.JNC_PRT_NO, Me, nrsBrCd)

        Return ds

    End Function

#End Region

#Region "マスタデータ"

    ''' <summary>
    ''' ＪＮＣ営業所マスタ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function BoMst(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC = New LMI511BLC

        '取得
        Return MyBase.CallBLC(rtnBlc, "BoMstSelect", ds)

    End Function

#End Region

#Region "チェック"

    ''' <summary>
    ''' 編集中に旧データになっていないかチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OldDataChk(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC = New LMI511BLC

        '取得
        Return MyBase.CallBLC(rtnBlc, "OldDataChkSelect", ds)

    End Function

#End Region

#Region "出荷登録処理"

    ''' <summary>
    ''' 出荷登録処理：取得：ＥＤＩ出荷データＬ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OutkaSaveSelectEdiL(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC = New LMI511BLC

        Return MyBase.CallBLC(rtnBlc, "OutkaSaveSelectEdiL", ds)

    End Function

    ''' <summary>
    ''' 出荷登録処理：取得：ＪＮＣＥＤＩ受信データ(ヘッダー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OutkaSaveSelectHed(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC = New LMI511BLC

        Return MyBase.CallBLC(rtnBlc, "OutkaSaveSelectHed", ds)

    End Function

    ''' <summary>
    ''' 出荷登録処理：取得：ＪＮＣＥＤＩ受信データ(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OutkaSaveSelectDtl(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC = New LMI511BLC

        Return MyBase.CallBLC(rtnBlc, "OutkaSaveSelectDtl", ds)

    End Function

    ''' <summary>
    ''' 出荷登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function OutkaSave(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC = New LMI511BLC

        'トランザクションを開始
        Using scope As TransactionScope = MyBase.BeginTransaction()
            'ＥＤＩ出荷データＬ
            With Nothing
                For dtIdx As Integer = 0 To ds.Tables(TABLE_NM.IN_EDI_L).Rows.Count - 1
                    'カレントインデックスを設定
                    ds = SetCurrentIndexIn(ds, dtIdx)

                    '登録
                    ds = MyBase.CallBLC(rtnBlc, "OutkaSaveInsertEdiL", ds)
                Next
            End With

            'ＥＤＩ出荷データＭ
            With Nothing
                For dtIdx As Integer = 0 To ds.Tables(TABLE_NM.IN_EDI_M).Rows.Count - 1
                    'カレントインデックスを設定
                    ds = SetCurrentIndexIn(ds, dtIdx)

                    '登録
                    ds = MyBase.CallBLC(rtnBlc, "OutkaSaveInsertEdiM", ds)
                Next
            End With

            'ＪＮＣＥＤＩ受信データ(ヘッダー)
            With Nothing
                For dtIdx As Integer = 0 To ds.Tables(TABLE_NM.IN_HED).Rows.Count - 1
                    'カレントインデックスを設定
                    ds = SetCurrentIndexIn(ds, dtIdx)

                    '更新
                    ds = MyBase.CallBLC(rtnBlc, "OutkaSaveUpdateHed", ds)
                Next
            End With

            'トランザクションを確定
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

#End Region

#Region "まとめ指示処理"

    ''' <summary>
    ''' まとめ指示処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function MtmSave(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC = New LMI511BLC

        'トランザクションを開始
        Using scope As TransactionScope = MyBase.BeginTransaction()
            For dtIdx As Integer = 0 To ds.Tables(TABLE_NM.IN_HED).Rows.Count - 1
                'カレントインデックスを設定
                ds = SetCurrentIndexIn(ds, dtIdx)

                'ＪＮＣＥＤＩ受信データ(ヘッダー)
                With Nothing
                    '排他
                    ds = MyBase.CallBLC(rtnBlc, "MtmSaveExitHed", ds)
                    If MyBase.IsMessageExist() Then
                        Return ds
                    End If

                    '更新
                    ds = MyBase.CallBLC(rtnBlc, "MtmSaveUpdateHed", ds)
                End With
            Next

            'トランザクションを確定
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

#End Region

#Region "まとめ解除処理"

    ''' <summary>
    ''' まとめ解除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function MtmCancel(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC = New LMI511BLC

        'トランザクションを開始
        Using scope As TransactionScope = MyBase.BeginTransaction()
            For dtIdx As Integer = 0 To ds.Tables(TABLE_NM.IN_HED).Rows.Count - 1
                'カレントインデックスを設定
                ds = SetCurrentIndexIn(ds, dtIdx)

                'ＪＮＣＥＤＩ受信データ(ヘッダー)
                With Nothing
                    '排他
                    ds = MyBase.CallBLC(rtnBlc, "MtmCancelExitHed", ds)
                    If MyBase.IsMessageExist() Then
                        Return ds
                    End If

                    '更新
                    ds = MyBase.CallBLC(rtnBlc, "MtmCancelUpdateHed", ds)
                End With
            Next

            'トランザクションを確定
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

#End Region

#Region "送信要求処理"

    ''' <summary>
    ''' 送信要求処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SndReq(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC = New LMI511BLC

        'トランザクションを開始
        Using scope As TransactionScope = MyBase.BeginTransaction()
            For dtIdx As Integer = 0 To ds.Tables(TABLE_NM.IN_HED).Rows.Count - 1
                'カレントインデックスを設定
                ds = SetCurrentIndexIn(ds, dtIdx)

                'ＪＮＣＥＤＩ受信データ(ヘッダー)
                With Nothing
                    '更新
#If False Then   'ADD 2020/08/31 013087   【LMS】JNC EDI　改修
                    ds = MyBase.CallBLC(rtnBlc, "SndReqUpdateHed", ds)
#Else
                    '出庫・入庫か
                    Dim OutIn_flg As String = ds.Tables(TABLE_NM.IN_HED).Rows(dtIdx).Item("OUT_RTN_FLG").ToString
                    '運送
                    Dim Unso_flg As String = ds.Tables(TABLE_NM.IN_HED).Rows(dtIdx).Item("UNSO_RTN_FLG").ToString
                    '実出荷数量(卸し)
                    Dim SURY_RPT_UNSO As String = ds.Tables(TABLE_NM.IN_HED).Rows(dtIdx).Item("SURY_RPT_UNSO").ToString
                    '実出荷数量(積み)
                    Dim SURY_RPT As String = ds.Tables(TABLE_NM.IN_HED).Rows(dtIdx).Item("SURY_RPT").ToString

                    If OutIn_flg = "0" Then
                        ds = MyBase.CallBLC(rtnBlc, "SndReqUpdateHed", ds)
                    End If

                    '運送データ対象化
                    If SURY_RPT_UNSO.Equals(String.Empty) Then
                        SURY_RPT_UNSO = "0"
                    End If
                    If SURY_RPT.Equals(String.Empty) Then
                        SURY_RPT = "0"
                    End If

                    If Unso_flg = "0" _
                        AndAlso Convert.ToInt32(Convert.ToDouble(SURY_RPT_UNSO)) > 0 _
                        AndAlso Convert.ToInt32(Convert.ToDouble(SURY_RPT)) > 0 Then
                        ds = MyBase.CallBLC(rtnBlc, "SndReqUpdateHedUnso", ds)
                    End If



#End If
                End With
            Next

            'トランザクションを確定
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

#End Region

#Region "送信取消処理"

    ''' <summary>
    ''' 送信取消処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SndCancel(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC = New LMI511BLC

        'トランザクションを開始
        Using scope As TransactionScope = MyBase.BeginTransaction()
            For dtIdx As Integer = 0 To ds.Tables(TABLE_NM.IN_HED).Rows.Count - 1
                'カレントインデックスを設定
                ds = SetCurrentIndexIn(ds, dtIdx)

                'ＪＮＣＥＤＩ受信データ(ヘッダー)
                With Nothing
                    '更新
#If False Then   'ADD 2020/08/31 013087   【LMS】JNC EDI　改修
                    ds = MyBase.CallBLC(rtnBlc, "SndCancelUpdateHed", ds)
#Else
                    '出庫・入庫か
                    Dim OutIn_flg As String = ds.Tables(TABLE_NM.IN_HED).Rows(0).Item("OUT_RTN_FLG").ToString
                    '運送
                    Dim Unso_flg As String = ds.Tables(TABLE_NM.IN_HED).Rows(0).Item("UNSO_RTN_FLG").ToString

                    If OutIn_flg = "1" Then
                        ds = MyBase.CallBLC(rtnBlc, "SndReqUpdateHed", ds)
                    End If

                    '運送データ対象化
                    If Unso_flg = "1" Then
                        ds = MyBase.CallBLC(rtnBlc, "SndReqUpdateHedUnso", ds)
                    End If

#End If
                End With
            Next

            'トランザクションを確定
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

#End Region

#Region "まとめ候補検索処理"

    ''' <summary>
    ''' まとめ候補検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function MtmSearch(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC = New LMI511BLC

        '強制実行フラグがオフ時のみ（オンになるのは閾値オーバーでも表示する時）
        If Not MyBase.GetForceOparation() Then
            '件数
            ds = MyBase.CallBLC(rtnBlc, "MtmSearchCount", ds)

            'メッセージの判定
            If MyBase.IsMessageExist() Then
                Return ds
            End If
        End If

        '取得
        Return MyBase.CallBLC(rtnBlc, "MtmSearchSelect", ds)

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Search(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC = New LMI511BLC

        '強制実行フラグがオフ時のみ（オンになるのは閾値オーバーでも表示する時）
        If Not MyBase.GetForceOparation() Then
            '件数
            ds = MyBase.CallBLC(rtnBlc, "SearchCount", ds)

            'メッセージの判定
            If MyBase.IsMessageExist() Then
                Return ds
            End If
        End If

        '取得
        Return MyBase.CallBLC(rtnBlc, "SearchSelect", ds)

    End Function

#End Region

#Region "保存処理(編集)"

    ''' <summary>
    ''' 保存処理(編集)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveEdit(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC = New LMI511BLC

        'トランザクションを開始
        Using scope As TransactionScope = MyBase.BeginTransaction()
            'ヘッダーと明細のDataTable件数は同じという前提のループ
            For dtIdx As Integer = 0 To ds.Tables(TABLE_NM.IN_HED).Rows.Count - 1
                'カレントインデックスを設定
                ds = SetCurrentIndexIn(ds, dtIdx)

                'ＪＮＣＥＤＩ受信データ(ヘッダー)
                With Nothing
                    '排他
                    ds = MyBase.CallBLC(rtnBlc, "SaveEditExitHed", ds)
                    If MyBase.IsMessageExist() Then
                        Return ds
                    End If

                    '更新
                    ds = MyBase.CallBLC(rtnBlc, "SaveEditUpdateHed", ds)
                End With

                'ＪＮＣＥＤＩ受信データ(明細)
                With Nothing
                    '排他
                    ds = MyBase.CallBLC(rtnBlc, "SaveEditExitDtl", ds)
                    If MyBase.IsMessageExist() Then
                        Return ds
                    End If

                    '更新
                    ds = MyBase.CallBLC(rtnBlc, "SaveEditUpdateDtl", ds)
                End With
            Next

            'トランザクションを確定
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

#End Region

#Region "保存処理(訂正)"

    ''' <summary>
    ''' 保存処理(訂正)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveRevision(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC = New LMI511BLC

        'トランザクションを開始
        Using scope As TransactionScope = MyBase.BeginTransaction()
            'ヘッダーと明細のDataTable件数は同じという前提のループ
            For dtIdx As Integer = 0 To ds.Tables(TABLE_NM.IN_HED).Rows.Count - 1
                'カレントインデックスを設定
                ds = SetCurrentIndexIn(ds, dtIdx)

                '現行のヘッダーを修黒として更新
                With Nothing
                    '排他
                    ds = MyBase.CallBLC(rtnBlc, "SaveRevisionExitHed", ds)
                    If MyBase.IsMessageExist() Then
                        Return ds
                    End If

                    '更新
                    ds = MyBase.CallBLC(rtnBlc, "SaveRevisionUpdateHed", ds)
                End With

                '現行のヘッダーを元データとして取得
                ds = MyBase.CallBLC(rtnBlc, "SaveRevisionSelectHed", ds)

                'データ種別でEDI管理番号採番用ナンバー区分を決定する
                Dim numberKbn As NumberMasterUtility.NumberKbn = NumberMasterUtility.NumberKbn.EDI_OUTKA_NO_L
                If ds.Tables(TABLE_NM.IN_HED).Rows(dtIdx).Item("DATA_KIND").ToString() = INOUT_KB.INKA Then
                    numberKbn = NumberMasterUtility.NumberKbn.EDI_INKA_NO_L
                End If

                'EDI管理番号(修赤用／訂正用)を採番する
                Dim nrsBrCd As String = ds.Tables(TABLE_NM.IN_HED).Rows(dtIdx).Item("NRS_BR_CD").ToString()
                Dim num As New NumberMasterUtility
                Dim redEdiCtlNo As String = num.GetAutoCode(numberKbn, Me, nrsBrCd)
                Dim newEdiCtlNo As String = num.GetAutoCode(numberKbn, Me, nrsBrCd)

                'ヘッダーの修赤を登録
                With Nothing
                    '元のDataSetは壊したくないのでコピーを使用する
                    Dim dsCopy As DataSet = ds.Copy()

                    'データセット設定
                    dsCopy = SaveRevisionSetHedDataRedIn(dsCopy, redEdiCtlNo)

                    '登録
                    dsCopy = MyBase.CallBLC(rtnBlc, "SaveRevisionInsertHed", dsCopy)
                End With

                'ヘッダーの訂正後を登録
                With Nothing
                    '元のDataSetは壊したくないのでコピーを使用する
                    Dim dsCopy As DataSet = ds.Copy()

                    'データセット設定
                    dsCopy = SaveRevisionSetHedDataNewIn(dsCopy, ds, newEdiCtlNo, dtIdx)

                    '登録
                    dsCopy = MyBase.CallBLC(rtnBlc, "SaveRevisionInsertHed", dsCopy)
                End With

                '現行の明細を元データとして取得
                ds = MyBase.CallBLC(rtnBlc, "SaveRevisionSelectDtl", ds)

                '明細の取得件数分繰り返し
                For miIdx As Integer = 0 To ds.Tables(TABLE_NM.OUT_DTL).Rows.Count() - 1
                    '明細の修赤を登録
                    With Nothing
                        '元のDataSetは壊したくないのでコピーを使用する
                        Dim dsCopy As DataSet = ds.Copy()

                        'データセット設定
                        dsCopy = SaveRevisionSetDtlDataRedIn(dsCopy, redEdiCtlNo, miIdx)

                        '登録
                        dsCopy = MyBase.CallBLC(rtnBlc, "SaveRevisionInsertDtl", dsCopy)
                    End With

                    '明細の訂正後を登録
                    With Nothing
                        '元のDataSetは壊したくないのでコピーを使用する
                        Dim dsCopy As DataSet = ds.Copy()

                        'データセット設定
                        dsCopy = SaveRevisionSetDtlDataNewIn(dsCopy, ds, newEdiCtlNo, miIdx, dtIdx)

                        '登録
                        dsCopy = MyBase.CallBLC(rtnBlc, "SaveRevisionInsertDtl", dsCopy)
                    End With
                Next
            Next

            'トランザクションを確定
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理(訂正)：データセット設定(ヘッダー：修赤：登録情報)
    ''' </summary>
    ''' <param name="dsCopy">DataSet</param>
    ''' <param name="ediCtlNo">EDI管理番号</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveRevisionSetHedDataRedIn(dsCopy As DataSet, ediCtlNo As String) As DataSet

        With dsCopy
            .Tables(TABLE_NM.IN_HED).Clear()

            Dim drTo As DataRow = .Tables(TABLE_NM.IN_HED).NewRow()
            Dim drFrom As DataRow = .Tables(TABLE_NM.OUT_HED).Rows(0)

            'ヘッダーの元データをベースとして登録用情報を設定する
            drTo("DEL_KB") = drFrom("DEL_KB")
            drTo("CRT_DATE") = drFrom("CRT_DATE")
            drTo("FILE_NAME") = drFrom("FILE_NAME")
            drTo("REC_NO") = drFrom("REC_NO")
            drTo("NRS_BR_CD") = drFrom("NRS_BR_CD")
            drTo("INOUT_KB") = drFrom("INOUT_KB")
            drTo("EDI_CTL_NO") = ediCtlNo
            drTo("INKA_CTL_NO_L") = drFrom("INKA_CTL_NO_L")
            drTo("OUTKA_CTL_NO") = drFrom("OUTKA_CTL_NO")
            drTo("CUST_CD_L") = drFrom("CUST_CD_L")
            drTo("CUST_CD_M") = drFrom("CUST_CD_M")
            drTo("PRTFLG") = drFrom("PRTFLG")
            drTo("PRTFLG_SUB") = drFrom("PRTFLG_SUB")
            drTo("CANCEL_FLG") = drFrom("CANCEL_FLG")
            drTo("NRS_TANTO") = drFrom("NRS_TANTO")
            drTo("DATA_KIND") = drFrom("DATA_KIND")
            drTo("SEND_CODE") = drFrom("SEND_CODE")
            drTo("SR_DEN_NO") = drFrom("SR_DEN_NO")
            drTo("HIS_NO") = drFrom("HIS_NO")
            drTo("PROC_YMD") = drFrom("PROC_YMD")
            drTo("PROC_TIME") = drFrom("PROC_TIME")
            drTo("PROC_NO") = drFrom("PROC_NO")
            drTo("SEND_DEN_YMD") = drFrom("SEND_DEN_YMD")
            drTo("SEND_DEN_TIME") = drFrom("SEND_DEN_TIME")
            drTo("BPID_KKN") = drFrom("BPID_KKN")
            drTo("BPID_SUB_KKN") = drFrom("BPID_SUB_KKN")
            drTo("BPID_HAN") = drFrom("BPID_HAN")
            drTo("RCV_COMP_CD") = drFrom("RCV_COMP_CD")
            drTo("SND_COMP_CD") = drFrom("SND_COMP_CD")
            drTo("RB_KBN") = RB_KBN.RED
            drTo("MOD_KBN") = MOD_KBN.SHO
            drTo("DATA_KBN") = drFrom("DATA_KBN")
            drTo("FAX_KBN") = drFrom("FAX_KBN")
            drTo("OUTKA_REQ_KBN") = drFrom("OUTKA_REQ_KBN")
            drTo("INKA_P_KBN") = drFrom("INKA_P_KBN")
            drTo("OUTKA_SEPT_KBN") = drFrom("OUTKA_SEPT_KBN")
            drTo("EM_OUTKA_KBN") = drFrom("EM_OUTKA_KBN")
            drTo("UNSO_ROUTE_P") = drFrom("UNSO_ROUTE_P")
            drTo("UNSO_ROUTE_A") = drFrom("UNSO_ROUTE_A")
            drTo("CAR_KIND_P") = drFrom("CAR_KIND_P")
            drTo("CAR_KIND_A") = drFrom("CAR_KIND_A")
            drTo("CAR_NO_P") = drFrom("CAR_NO_P")
            drTo("CAR_NO_A") = drFrom("CAR_NO_A")
            drTo("COMBI_NO_P") = drFrom("COMBI_NO_P")
            drTo("COMBI_NO_A") = drFrom("COMBI_NO_A")
            drTo("UNSO_REQ_YN") = drFrom("UNSO_REQ_YN")
            drTo("DEST_CHK_KBN") = drFrom("DEST_CHK_KBN")
            drTo("INKO_DATE_P") = drFrom("INKO_DATE_P")
            drTo("INKO_DATE_A") = drFrom("INKO_DATE_A")
            drTo("INKO_TIME") = drFrom("INKO_TIME")
            drTo("OUTKA_DATE_P") = drFrom("OUTKA_DATE_P")
            drTo("OUTKA_DATE_A") = drFrom("OUTKA_DATE_A")
            drTo("OUTKA_TIME_E") = drFrom("OUTKA_TIME_E")
            drTo("CARGO_BKG_DATE_P") = drFrom("CARGO_BKG_DATE_P")
            drTo("CARGO_BKG_DATE_A") = drFrom("CARGO_BKG_DATE_A")
            drTo("ARRIVAL_DATE_P") = drFrom("ARRIVAL_DATE_P")
            drTo("ARRIVAL_DATE_A") = drFrom("ARRIVAL_DATE_A")
            drTo("ARRIVAL_TIME") = drFrom("ARRIVAL_TIME")
            drTo("UNSO_DATE") = drFrom("UNSO_DATE")
            drTo("UNSO_TIME") = drFrom("UNSO_TIME")
            drTo("ZAI_RPT_DATE") = drFrom("ZAI_RPT_DATE")
            drTo("BAILER_CD") = drFrom("BAILER_CD")
            drTo("BAILER_NM") = drFrom("BAILER_NM")
            drTo("BAILER_BU_CD") = drFrom("BAILER_BU_CD")
            drTo("BAILER_BU_NM") = drFrom("BAILER_BU_NM")
            drTo("SHIPPER_CD") = drFrom("SHIPPER_CD")
            drTo("SHIPPER_NM") = drFrom("SHIPPER_NM")
            drTo("SHIPPER_BU_CD") = drFrom("SHIPPER_BU_CD")
            drTo("SHIPPER_BU_NM") = drFrom("SHIPPER_BU_NM")
            drTo("CONSIGNEE_CD") = drFrom("CONSIGNEE_CD")
            drTo("CONSIGNEE_NM") = drFrom("CONSIGNEE_NM")
            drTo("CONSIGNEE_BU_CD") = drFrom("CONSIGNEE_BU_CD")
            drTo("CONSIGNEE_BU_NM") = drFrom("CONSIGNEE_BU_NM")
            drTo("SOKO_PROV_CD") = drFrom("SOKO_PROV_CD")
            drTo("SOKO_PROV_NM") = drFrom("SOKO_PROV_NM")
            drTo("UNSO_PROV_CD") = drFrom("UNSO_PROV_CD")
            drTo("UNSO_PROV_NM") = drFrom("UNSO_PROV_NM")
            drTo("ACT_UNSO_CD") = drFrom("ACT_UNSO_CD")
            drTo("UNSO_TF_KBN") = drFrom("UNSO_TF_KBN")
            drTo("UNSO_F_KBN") = drFrom("UNSO_F_KBN")
            drTo("DEST_CD") = drFrom("DEST_CD")
            drTo("DEST_NM") = drFrom("DEST_NM")
            drTo("DEST_BU_CD") = drFrom("DEST_BU_CD")
            drTo("DEST_BU_NM") = drFrom("DEST_BU_NM")
            drTo("DEST_AD_CD") = drFrom("DEST_AD_CD")
            drTo("DEST_AD_NM") = drFrom("DEST_AD_NM")
            drTo("DEST_YB_NO") = drFrom("DEST_YB_NO")
            drTo("DEST_TEL_NO") = drFrom("DEST_TEL_NO")
            drTo("DEST_FAX_NO") = drFrom("DEST_FAX_NO")
            drTo("DELIVERY_NM") = drFrom("DELIVERY_NM")
            drTo("DELIVERY_SAGYO") = drFrom("DELIVERY_SAGYO")
            drTo("ORDER_NO") = drFrom("ORDER_NO")
            drTo("JYUCHU_NO") = drFrom("JYUCHU_NO")
            drTo("PRI_SHOP_CD") = drFrom("PRI_SHOP_CD")
            drTo("PRI_SHOP_NM") = drFrom("PRI_SHOP_NM")
            drTo("INV_REM_NM") = drFrom("INV_REM_NM")
            drTo("INV_REM_KANA") = drFrom("INV_REM_KANA")
            drTo("DEN_NO") = drFrom("DEN_NO")
            drTo("MEI_DEN_NO") = drFrom("MEI_DEN_NO")
            drTo("OUTKA_POSI_CD") = drFrom("OUTKA_POSI_CD")
            drTo("OUTKA_POSI_NM") = drFrom("OUTKA_POSI_NM")
            drTo("OUTKA_POSI_BU_CD_PA") = drFrom("OUTKA_POSI_BU_CD_PA")
            drTo("OUTKA_POSI_BU_CD_NAIBU") = drFrom("OUTKA_POSI_BU_CD_NAIBU")
            drTo("OUTKA_POSI_BU_NM_PA") = drFrom("OUTKA_POSI_BU_NM_PA")
            drTo("OUTKA_POSI_BU_NM_NAIBU") = drFrom("OUTKA_POSI_BU_NM_NAIBU")
            drTo("OUTKA_POSI_AD_CD_PA") = drFrom("OUTKA_POSI_AD_CD_PA")
            drTo("OUTKA_POSI_AD_NM_PA") = drFrom("OUTKA_POSI_AD_NM_PA")
            drTo("OUTKA_POSI_TEL_NO_PA") = drFrom("OUTKA_POSI_TEL_NO_PA")
            drTo("OUTKA_POSI_FAX_NO_PA") = drFrom("OUTKA_POSI_FAX_NO_PA")
            drTo("UNSO_JURYO") = drFrom("UNSO_JURYO")
            drTo("UNSO_JURYO_FLG") = drFrom("UNSO_JURYO_FLG")
            drTo("UNIT_LOAD_CD") = drFrom("UNIT_LOAD_CD")
            drTo("UNIT_LOAD_SU") = drFrom("UNIT_LOAD_SU")
            drTo("REMARK") = drFrom("REMARK")
            drTo("REMARK_KANA") = drFrom("REMARK_KANA")
            drTo("HARAI_KBN") = drFrom("HARAI_KBN")
            drTo("DATA_TYPE") = drFrom("DATA_TYPE")
            drTo("RTN_FLG") = RTN_FLG.MISO
            drTo("SND_CANCEL_FLG") = SND_CANCEL_FLG.RED
            drTo("OLD_DATA_FLG") = OLD_DATA_FLG.SIN
            drTo("PRINT_NO") = drFrom("PRINT_NO")
            drTo("NRS_SYS_FLG") = drFrom("NRS_SYS_FLG")
            drTo("OLD_SYS_FLG") = drFrom("OLD_SYS_FLG")
            drTo("RTN_FILE_DATE") = drFrom("RTN_FILE_DATE")
            drTo("RTN_FILE_TIME") = drFrom("RTN_FILE_TIME")
            drTo("RECORD_STATUS") = drFrom("RECORD_STATUS")
            drTo("DELETE_USER") = drFrom("DELETE_USER")
            drTo("DELETE_DATE") = drFrom("DELETE_DATE")
            drTo("DELETE_TIME") = drFrom("DELETE_TIME")
            drTo("DELETE_EDI_NO") = drFrom("DELETE_EDI_NO")
            drTo("PRT_USER") = drFrom("PRT_USER")
            drTo("PRT_DATE") = drFrom("PRT_DATE")
            drTo("PRT_TIME") = drFrom("PRT_TIME")
            drTo("EDI_USER") = drFrom("EDI_USER")
            drTo("EDI_DATE") = drFrom("EDI_DATE")
            drTo("EDI_TIME") = drFrom("EDI_TIME")
            drTo("OUTKA_USER") = drFrom("OUTKA_USER")
            drTo("OUTKA_DATE") = drFrom("OUTKA_DATE")
            drTo("OUTKA_TIME") = drFrom("OUTKA_TIME")
            drTo("INKA_USER") = drFrom("INKA_USER")
            drTo("INKA_DATE") = drFrom("INKA_DATE")
            drTo("INKA_TIME") = drFrom("INKA_TIME")
            drTo("UPD_USER") = drFrom("UPD_USER")
            drTo("UPD_DATE") = drFrom("UPD_DATE")
            drTo("UPD_TIME") = drFrom("UPD_TIME")
            drTo("SYS_ENT_DATE") = drFrom("SYS_ENT_DATE")
            drTo("SYS_ENT_TIME") = drFrom("SYS_ENT_TIME")
            drTo("SYS_ENT_PGID") = drFrom("SYS_ENT_PGID")
            drTo("SYS_ENT_USER") = drFrom("SYS_ENT_USER")
            drTo("SYS_UPD_DATE") = drFrom("SYS_UPD_DATE")
            drTo("SYS_UPD_TIME") = drFrom("SYS_UPD_TIME")
            drTo("SYS_UPD_PGID") = drFrom("SYS_UPD_PGID")
            drTo("SYS_UPD_USER") = drFrom("SYS_UPD_USER")
            drTo("SYS_DEL_FLG") = drFrom("SYS_DEL_FLG")
            drTo("PRT_FLG") = drFrom("PRT_FLG")
            drTo("MATOME_FLG") = drFrom("MATOME_FLG")

            .Tables(TABLE_NM.IN_HED).Rows.Add(drTo)
        End With

        Return dsCopy

    End Function

    ''' <summary>
    ''' 保存処理(訂正)：データセット設定(ヘッダー：訂正後：登録情報)
    ''' </summary>
    ''' <param name="dsCopy">DataSet</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="ediCtlNo">EDI管理番号</param>
    ''' <param name="dtIdx">カレントインデックス</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveRevisionSetHedDataNewIn(dsCopy As DataSet, ds As DataSet, ediCtlNo As String, dtIdx As Integer) As DataSet

        With dsCopy
            .Tables(TABLE_NM.IN_HED).Clear()

            Dim drTo As DataRow = .Tables(TABLE_NM.IN_HED).NewRow()
            Dim drFrom As DataRow = .Tables(TABLE_NM.OUT_HED).Rows(0)
            Dim drRev As DataRow = ds.Tables(TABLE_NM.IN_HED).Rows(dtIdx)

            'ヘッダーの元データをベースとして登録用情報を設定する
            drTo("DEL_KB") = drFrom("DEL_KB")
            drTo("CRT_DATE") = drFrom("CRT_DATE")
            drTo("FILE_NAME") = drFrom("FILE_NAME")
            drTo("REC_NO") = drFrom("REC_NO")
            drTo("NRS_BR_CD") = drFrom("NRS_BR_CD")
            drTo("INOUT_KB") = drFrom("INOUT_KB")
            drTo("EDI_CTL_NO") = ediCtlNo
            drTo("INKA_CTL_NO_L") = drFrom("INKA_CTL_NO_L")
            drTo("OUTKA_CTL_NO") = drFrom("OUTKA_CTL_NO")
            drTo("CUST_CD_L") = drFrom("CUST_CD_L")
            drTo("CUST_CD_M") = drFrom("CUST_CD_M")
            drTo("PRTFLG") = drFrom("PRTFLG")
            drTo("PRTFLG_SUB") = drFrom("PRTFLG_SUB")
            drTo("CANCEL_FLG") = drFrom("CANCEL_FLG")
            drTo("NRS_TANTO") = drFrom("NRS_TANTO")
            drTo("DATA_KIND") = drFrom("DATA_KIND")
            drTo("SEND_CODE") = drFrom("SEND_CODE")
            drTo("SR_DEN_NO") = drFrom("SR_DEN_NO")
            drTo("HIS_NO") = drFrom("HIS_NO")
            drTo("PROC_YMD") = drFrom("PROC_YMD")
            drTo("PROC_TIME") = drFrom("PROC_TIME")
            drTo("PROC_NO") = drFrom("PROC_NO")
            drTo("SEND_DEN_YMD") = drFrom("SEND_DEN_YMD")
            drTo("SEND_DEN_TIME") = drFrom("SEND_DEN_TIME")
            drTo("BPID_KKN") = drFrom("BPID_KKN")
            drTo("BPID_SUB_KKN") = drFrom("BPID_SUB_KKN")
            drTo("BPID_HAN") = drFrom("BPID_HAN")
            drTo("RCV_COMP_CD") = drFrom("RCV_COMP_CD")
            drTo("SND_COMP_CD") = drFrom("SND_COMP_CD")
            drTo("RB_KBN") = RB_KBN.BLACK
            drTo("MOD_KBN") = MOD_KBN.SHO
            drTo("DATA_KBN") = drFrom("DATA_KBN")
            drTo("FAX_KBN") = drFrom("FAX_KBN")
            drTo("OUTKA_REQ_KBN") = drFrom("OUTKA_REQ_KBN")
            drTo("INKA_P_KBN") = drFrom("INKA_P_KBN")
            drTo("OUTKA_SEPT_KBN") = drFrom("OUTKA_SEPT_KBN")
            drTo("EM_OUTKA_KBN") = drFrom("EM_OUTKA_KBN")
            drTo("UNSO_ROUTE_P") = drFrom("UNSO_ROUTE_P")
            drTo("UNSO_ROUTE_A") = drRev("UNSO_ROUTE_A")
            drTo("CAR_KIND_P") = drFrom("CAR_KIND_P")
            drTo("CAR_KIND_A") = drFrom("CAR_KIND_A")
            drTo("CAR_NO_P") = drFrom("CAR_NO_P")
            drTo("CAR_NO_A") = drRev("CAR_NO_A")
            drTo("COMBI_NO_P") = drFrom("COMBI_NO_P")
            drTo("COMBI_NO_A") = drFrom("COMBI_NO_A")
            drTo("UNSO_REQ_YN") = drFrom("UNSO_REQ_YN")
            drTo("DEST_CHK_KBN") = drFrom("DEST_CHK_KBN")
            drTo("INKO_DATE_P") = drFrom("INKO_DATE_P")
            drTo("INKO_DATE_A") = drFrom("INKO_DATE_A")
            drTo("INKO_TIME") = drFrom("INKO_TIME")
            drTo("OUTKA_DATE_P") = drFrom("OUTKA_DATE_P")
            drTo("OUTKA_DATE_A") = drFrom("OUTKA_DATE_A")
            drTo("OUTKA_TIME_E") = drFrom("OUTKA_TIME_E")
            drTo("CARGO_BKG_DATE_P") = drFrom("CARGO_BKG_DATE_P")
            drTo("CARGO_BKG_DATE_A") = drFrom("CARGO_BKG_DATE_A")
            drTo("ARRIVAL_DATE_P") = drFrom("ARRIVAL_DATE_P")
            drTo("ARRIVAL_DATE_A") = drFrom("ARRIVAL_DATE_A")
            drTo("ARRIVAL_TIME") = drFrom("ARRIVAL_TIME")
            drTo("UNSO_DATE") = drFrom("UNSO_DATE")
            drTo("UNSO_TIME") = drFrom("UNSO_TIME")
            drTo("ZAI_RPT_DATE") = drFrom("ZAI_RPT_DATE")
            drTo("BAILER_CD") = drFrom("BAILER_CD")
            drTo("BAILER_NM") = drFrom("BAILER_NM")
            drTo("BAILER_BU_CD") = drFrom("BAILER_BU_CD")
            drTo("BAILER_BU_NM") = drFrom("BAILER_BU_NM")
            drTo("SHIPPER_CD") = drFrom("SHIPPER_CD")
            drTo("SHIPPER_NM") = drFrom("SHIPPER_NM")
            drTo("SHIPPER_BU_CD") = drFrom("SHIPPER_BU_CD")
            drTo("SHIPPER_BU_NM") = drFrom("SHIPPER_BU_NM")
            drTo("CONSIGNEE_CD") = drFrom("CONSIGNEE_CD")
            drTo("CONSIGNEE_NM") = drFrom("CONSIGNEE_NM")
            drTo("CONSIGNEE_BU_CD") = drFrom("CONSIGNEE_BU_CD")
            drTo("CONSIGNEE_BU_NM") = drFrom("CONSIGNEE_BU_NM")
            drTo("SOKO_PROV_CD") = drFrom("SOKO_PROV_CD")
            drTo("SOKO_PROV_NM") = drFrom("SOKO_PROV_NM")
            drTo("UNSO_PROV_CD") = drFrom("UNSO_PROV_CD")
            drTo("UNSO_PROV_NM") = drFrom("UNSO_PROV_NM")
            drTo("ACT_UNSO_CD") = drRev("ACT_UNSO_CD")
            drTo("UNSO_TF_KBN") = drFrom("UNSO_TF_KBN")
            drTo("UNSO_F_KBN") = drFrom("UNSO_F_KBN")
            drTo("DEST_CD") = drFrom("DEST_CD")
            drTo("DEST_NM") = drFrom("DEST_NM")
            drTo("DEST_BU_CD") = drFrom("DEST_BU_CD")
            drTo("DEST_BU_NM") = drFrom("DEST_BU_NM")
            drTo("DEST_AD_CD") = drFrom("DEST_AD_CD")
            drTo("DEST_AD_NM") = drFrom("DEST_AD_NM")
            drTo("DEST_YB_NO") = drFrom("DEST_YB_NO")
            drTo("DEST_TEL_NO") = drFrom("DEST_TEL_NO")
            drTo("DEST_FAX_NO") = drFrom("DEST_FAX_NO")
            drTo("DELIVERY_NM") = drFrom("DELIVERY_NM")
            drTo("DELIVERY_SAGYO") = drFrom("DELIVERY_SAGYO")
            drTo("ORDER_NO") = drFrom("ORDER_NO")
            drTo("JYUCHU_NO") = drFrom("JYUCHU_NO")
            drTo("PRI_SHOP_CD") = drFrom("PRI_SHOP_CD")
            drTo("PRI_SHOP_NM") = drFrom("PRI_SHOP_NM")
            drTo("INV_REM_NM") = drFrom("INV_REM_NM")
            drTo("INV_REM_KANA") = drFrom("INV_REM_KANA")
            drTo("DEN_NO") = drFrom("DEN_NO")
            drTo("MEI_DEN_NO") = drFrom("MEI_DEN_NO")
            drTo("OUTKA_POSI_CD") = drFrom("OUTKA_POSI_CD")
            drTo("OUTKA_POSI_NM") = drFrom("OUTKA_POSI_NM")
            drTo("OUTKA_POSI_BU_CD_PA") = drFrom("OUTKA_POSI_BU_CD_PA")
            drTo("OUTKA_POSI_BU_CD_NAIBU") = drFrom("OUTKA_POSI_BU_CD_NAIBU")
            drTo("OUTKA_POSI_BU_NM_PA") = drFrom("OUTKA_POSI_BU_NM_PA")
            drTo("OUTKA_POSI_BU_NM_NAIBU") = drFrom("OUTKA_POSI_BU_NM_NAIBU")
            drTo("OUTKA_POSI_AD_CD_PA") = drFrom("OUTKA_POSI_AD_CD_PA")
            drTo("OUTKA_POSI_AD_NM_PA") = drFrom("OUTKA_POSI_AD_NM_PA")
            drTo("OUTKA_POSI_TEL_NO_PA") = drFrom("OUTKA_POSI_TEL_NO_PA")
            drTo("OUTKA_POSI_FAX_NO_PA") = drFrom("OUTKA_POSI_FAX_NO_PA")
            drTo("UNSO_JURYO") = drFrom("UNSO_JURYO")
            drTo("UNSO_JURYO_FLG") = drFrom("UNSO_JURYO_FLG")
            drTo("UNIT_LOAD_CD") = drFrom("UNIT_LOAD_CD")
            drTo("UNIT_LOAD_SU") = drFrom("UNIT_LOAD_SU")
            drTo("REMARK") = drFrom("REMARK")
            drTo("REMARK_KANA") = drFrom("REMARK_KANA")
            drTo("HARAI_KBN") = drFrom("HARAI_KBN")
            drTo("DATA_TYPE") = drFrom("DATA_TYPE")
            drTo("RTN_FLG") = RTN_FLG.MISO
            drTo("SND_CANCEL_FLG") = SND_CANCEL_FLG.NORMAL
            drTo("OLD_DATA_FLG") = OLD_DATA_FLG.SIN
            drTo("PRINT_NO") = drFrom("PRINT_NO")
            drTo("NRS_SYS_FLG") = drFrom("NRS_SYS_FLG")
            drTo("OLD_SYS_FLG") = drFrom("OLD_SYS_FLG")
            drTo("RTN_FILE_DATE") = drFrom("RTN_FILE_DATE")
            drTo("RTN_FILE_TIME") = drFrom("RTN_FILE_TIME")
            drTo("RECORD_STATUS") = drFrom("RECORD_STATUS")
            drTo("DELETE_USER") = drFrom("DELETE_USER")
            drTo("DELETE_DATE") = drFrom("DELETE_DATE")
            drTo("DELETE_TIME") = drFrom("DELETE_TIME")
            drTo("DELETE_EDI_NO") = drFrom("DELETE_EDI_NO")
            drTo("PRT_USER") = drFrom("PRT_USER")
            drTo("PRT_DATE") = drFrom("PRT_DATE")
            drTo("PRT_TIME") = drFrom("PRT_TIME")
            drTo("EDI_USER") = drFrom("EDI_USER")
            drTo("EDI_DATE") = drFrom("EDI_DATE")
            drTo("EDI_TIME") = drFrom("EDI_TIME")
            drTo("OUTKA_USER") = drFrom("OUTKA_USER")
            drTo("OUTKA_DATE") = drFrom("OUTKA_DATE")
            drTo("OUTKA_TIME") = drFrom("OUTKA_TIME")
            drTo("INKA_USER") = drFrom("INKA_USER")
            drTo("INKA_DATE") = drFrom("INKA_DATE")
            drTo("INKA_TIME") = drFrom("INKA_TIME")
            drTo("UPD_USER") = drFrom("UPD_USER")
            drTo("UPD_DATE") = drFrom("UPD_DATE")
            drTo("UPD_TIME") = drFrom("UPD_TIME")
            drTo("SYS_ENT_DATE") = drFrom("SYS_ENT_DATE")
            drTo("SYS_ENT_TIME") = drFrom("SYS_ENT_TIME")
            drTo("SYS_ENT_PGID") = drFrom("SYS_ENT_PGID")
            drTo("SYS_ENT_USER") = drFrom("SYS_ENT_USER")
            drTo("SYS_UPD_DATE") = drFrom("SYS_UPD_DATE")
            drTo("SYS_UPD_TIME") = drFrom("SYS_UPD_TIME")
            drTo("SYS_UPD_PGID") = drFrom("SYS_UPD_PGID")
            drTo("SYS_UPD_USER") = drFrom("SYS_UPD_USER")
            drTo("SYS_DEL_FLG") = drFrom("SYS_DEL_FLG")
            drTo("PRT_FLG") = drFrom("PRT_FLG")
            drTo("MATOME_FLG") = drFrom("MATOME_FLG")

            .Tables(TABLE_NM.IN_HED).Rows.Add(drTo)
        End With

        Return dsCopy

    End Function

    ''' <summary>
    ''' 保存処理(訂正)：データセット設定(明細：修赤：登録情報)
    ''' </summary>
    ''' <param name="dsCopy">DataSet</param>
    ''' <param name="ediCtlNo">EDI管理番号</param>
    ''' <param name="miIdx">カレントインデックス</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveRevisionSetDtlDataRedIn(dsCopy As DataSet, ediCtlNo As String, miIdx As Integer) As DataSet

        With dsCopy
            .Tables(TABLE_NM.IN_DTL).Clear()

            Dim drTo As DataRow = .Tables(TABLE_NM.IN_DTL).NewRow()
            Dim drFrom As DataRow = .Tables(TABLE_NM.OUT_DTL).Rows(miIdx)

            '明細の元データをベースとして登録用情報を設定する
            drTo("DEL_KB") = drFrom("DEL_KB")
            drTo("CRT_DATE") = drFrom("CRT_DATE")
            drTo("FILE_NAME") = drFrom("FILE_NAME")
            drTo("REC_NO") = drFrom("REC_NO")
            drTo("GYO") = drFrom("GYO")
            drTo("NRS_BR_CD") = drFrom("NRS_BR_CD")
            drTo("INOUT_KB") = drFrom("INOUT_KB")
            drTo("EDI_CTL_NO") = ediCtlNo
            drTo("EDI_CTL_NO_CHU") = drFrom("EDI_CTL_NO_CHU")
            drTo("INKA_CTL_NO_L") = drFrom("INKA_CTL_NO_L")
            drTo("INKA_CTL_NO_M") = drFrom("INKA_CTL_NO_M")
            drTo("OUTKA_CTL_NO") = drFrom("OUTKA_CTL_NO")
            drTo("OUTKA_CTL_NO_CHU") = drFrom("OUTKA_CTL_NO_CHU")
            drTo("CUST_CD_L") = drFrom("CUST_CD_L")
            drTo("CUST_CD_M") = drFrom("CUST_CD_M")
            drTo("SR_DEN_NO") = drFrom("SR_DEN_NO")
            drTo("HIS_NO") = drFrom("HIS_NO")
            drTo("MEI_NO_P") = drFrom("MEI_NO_P")
            drTo("MEI_NO_A") = drFrom("MEI_NO_A")
            drTo("JYUCHU_GOODS_CD") = drFrom("JYUCHU_GOODS_CD")
            drTo("GOODS_NM") = drFrom("GOODS_NM")
            drTo("GOODS_KANA1") = drFrom("GOODS_KANA1")
            drTo("GOODS_KANA2") = drFrom("GOODS_KANA2")
            drTo("NISUGATA_CD") = drFrom("NISUGATA_CD")
            drTo("IRISUU") = drFrom("IRISUU")
            drTo("LOT_NO_P") = drFrom("LOT_NO_P")
            drTo("LOT_NO_A") = drFrom("LOT_NO_A")
            drTo("SURY_TANI_CD") = drFrom("SURY_TANI_CD")
            drTo("SURY_REQ") = drFrom("SURY_REQ")
            drTo("SURY_RPT") = drFrom("SURY_RPT")
            drTo("MEI_REM1") = drFrom("MEI_REM1")
            drTo("MEI_REM2") = drFrom("MEI_REM2")
            drTo("RECORD_STATUS") = drFrom("RECORD_STATUS")
            drTo("JISSEKI_SHORI_FLG") = drFrom("JISSEKI_SHORI_FLG")
            drTo("JISSEKI_USER") = drFrom("JISSEKI_USER")
            drTo("JISSEKI_DATE") = drFrom("JISSEKI_DATE")
            drTo("JISSEKI_TIME") = drFrom("JISSEKI_TIME")
            drTo("SEND_USER") = drFrom("SEND_USER")
            drTo("SEND_DATE") = drFrom("SEND_DATE")
            drTo("SEND_TIME") = drFrom("SEND_TIME")
            drTo("DELETE_USER") = drFrom("DELETE_USER")
            drTo("DELETE_DATE") = drFrom("DELETE_DATE")
            drTo("DELETE_TIME") = drFrom("DELETE_TIME")
            drTo("DELETE_EDI_NO") = drFrom("DELETE_EDI_NO")
            drTo("DELETE_EDI_NO_CHU") = drFrom("DELETE_EDI_NO_CHU")
            drTo("UPD_USER") = drFrom("UPD_USER")
            drTo("UPD_DATE") = drFrom("UPD_DATE")
            drTo("UPD_TIME") = drFrom("UPD_TIME")
            drTo("SYS_ENT_DATE") = drFrom("SYS_ENT_DATE")
            drTo("SYS_ENT_TIME") = drFrom("SYS_ENT_TIME")
            drTo("SYS_ENT_PGID") = drFrom("SYS_ENT_PGID")
            drTo("SYS_ENT_USER") = drFrom("SYS_ENT_USER")
            drTo("SYS_UPD_DATE") = drFrom("SYS_UPD_DATE")
            drTo("SYS_UPD_TIME") = drFrom("SYS_UPD_TIME")
            drTo("SYS_UPD_PGID") = drFrom("SYS_UPD_PGID")
            drTo("SYS_UPD_USER") = drFrom("SYS_UPD_USER")
            drTo("SYS_DEL_FLG") = drFrom("SYS_DEL_FLG")

            .Tables(TABLE_NM.IN_DTL).Rows.Add(drTo)
        End With

        Return dsCopy

    End Function

    ''' <summary>
    ''' 保存処理(訂正)：データセット設定(明細：訂正後：登録情報)
    ''' </summary>
    ''' <param name="dsCopy">DataSet</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="ediCtlNo">EDI管理番号</param>
    ''' <param name="miIdx">カレントインデックス</param>
    ''' <param name="dtIdx">カレントインデックス</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveRevisionSetDtlDataNewIn(dsCopy As DataSet, ds As DataSet, ediCtlNo As String, miIdx As Integer, dtIdx As Integer) As DataSet

        With dsCopy
            .Tables(TABLE_NM.IN_DTL).Clear()

            Dim drTo As DataRow = .Tables(TABLE_NM.IN_DTL).NewRow()
            Dim drFrom As DataRow = .Tables(TABLE_NM.OUT_DTL).Rows(miIdx)
            Dim drRev As DataRow = ds.Tables(TABLE_NM.IN_DTL).Rows(dtIdx)

            '明細の元データをベースとして登録用情報を設定する
            drTo("DEL_KB") = drFrom("DEL_KB")
            drTo("CRT_DATE") = drFrom("CRT_DATE")
            drTo("FILE_NAME") = drFrom("FILE_NAME")
            drTo("REC_NO") = drFrom("REC_NO")
            drTo("GYO") = drFrom("GYO")
            drTo("NRS_BR_CD") = drFrom("NRS_BR_CD")
            drTo("INOUT_KB") = drFrom("INOUT_KB")
            drTo("EDI_CTL_NO") = ediCtlNo
            drTo("EDI_CTL_NO_CHU") = drFrom("EDI_CTL_NO_CHU")
            drTo("INKA_CTL_NO_L") = drFrom("INKA_CTL_NO_L")
            drTo("INKA_CTL_NO_M") = drFrom("INKA_CTL_NO_M")
            drTo("OUTKA_CTL_NO") = drFrom("OUTKA_CTL_NO")
            drTo("OUTKA_CTL_NO_CHU") = drFrom("OUTKA_CTL_NO_CHU")
            drTo("CUST_CD_L") = drFrom("CUST_CD_L")
            drTo("CUST_CD_M") = drFrom("CUST_CD_M")
            drTo("SR_DEN_NO") = drFrom("SR_DEN_NO")
            drTo("HIS_NO") = drFrom("HIS_NO")
            drTo("MEI_NO_P") = drFrom("MEI_NO_P")
            drTo("MEI_NO_A") = drFrom("MEI_NO_A")
            drTo("JYUCHU_GOODS_CD") = drFrom("JYUCHU_GOODS_CD")
            drTo("GOODS_NM") = drFrom("GOODS_NM")
            drTo("GOODS_KANA1") = drFrom("GOODS_KANA1")
            drTo("GOODS_KANA2") = drFrom("GOODS_KANA2")
            drTo("NISUGATA_CD") = drFrom("NISUGATA_CD")
            drTo("IRISUU") = drFrom("IRISUU")
            drTo("LOT_NO_P") = drFrom("LOT_NO_P")
            drTo("LOT_NO_A") = drFrom("LOT_NO_A")
            drTo("SURY_TANI_CD") = drFrom("SURY_TANI_CD")
            drTo("SURY_REQ") = drFrom("SURY_REQ")
            If Val(drFrom("EDI_CTL_NO_CHU").ToString()) = 1 Then
                '明細1行目：訂正された値
                drTo("SURY_RPT") = drRev("SURY_RPT")
            Else
                '明細2行目以降：元データの値
                drTo("SURY_RPT") = drFrom("SURY_RPT")
            End If
            drTo("MEI_REM1") = drFrom("MEI_REM1")
            drTo("MEI_REM2") = drFrom("MEI_REM2")
            drTo("RECORD_STATUS") = drFrom("RECORD_STATUS")
            drTo("JISSEKI_SHORI_FLG") = drFrom("JISSEKI_SHORI_FLG")
            drTo("JISSEKI_USER") = drFrom("JISSEKI_USER")
            drTo("JISSEKI_DATE") = drFrom("JISSEKI_DATE")
            drTo("JISSEKI_TIME") = drFrom("JISSEKI_TIME")
            drTo("SEND_USER") = drFrom("SEND_USER")
            drTo("SEND_DATE") = drFrom("SEND_DATE")
            drTo("SEND_TIME") = drFrom("SEND_TIME")
            drTo("DELETE_USER") = drFrom("DELETE_USER")
            drTo("DELETE_DATE") = drFrom("DELETE_DATE")
            drTo("DELETE_TIME") = drFrom("DELETE_TIME")
            drTo("DELETE_EDI_NO") = drFrom("DELETE_EDI_NO")
            drTo("DELETE_EDI_NO_CHU") = drFrom("DELETE_EDI_NO_CHU")
            drTo("UPD_USER") = drFrom("UPD_USER")
            drTo("UPD_DATE") = drFrom("UPD_DATE")
            drTo("UPD_TIME") = drFrom("UPD_TIME")
            drTo("SYS_ENT_DATE") = drFrom("SYS_ENT_DATE")
            drTo("SYS_ENT_TIME") = drFrom("SYS_ENT_TIME")
            drTo("SYS_ENT_PGID") = drFrom("SYS_ENT_PGID")
            drTo("SYS_ENT_USER") = drFrom("SYS_ENT_USER")
            drTo("SYS_UPD_DATE") = drFrom("SYS_UPD_DATE")
            drTo("SYS_UPD_TIME") = drFrom("SYS_UPD_TIME")
            drTo("SYS_UPD_PGID") = drFrom("SYS_UPD_PGID")
            drTo("SYS_UPD_USER") = drFrom("SYS_UPD_USER")
            drTo("SYS_DEL_FLG") = drFrom("SYS_DEL_FLG")

            .Tables(TABLE_NM.IN_DTL).Rows.Add(drTo)
        End With

        Return dsCopy

    End Function

#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理(出荷予定表)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Print1(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet = ds.Copy

        '印刷プレビュー用DataTable
        If ds.Tables(LMConst.RD) Is Nothing Then
            ds.Tables.Add(New RdPrevInfoDS.PREV_INFODataTable())
        End If
        Dim rtnDt As DataTable = ds.Tables(LMConst.RD)
        rtnDt.Clear()

        '印刷処理
        Dim setRptDt As DataTable = Me.DoPrint(setDs, PrintType.OUTKA_YOTEI).Tables(LMConst.RD)

        'プレビュー情報を設定
        If setRptDt IsNot Nothing Then
            For dtIdx As Integer = 0 To setRptDt.Rows.Count - 1
                rtnDt.ImportRow(setRptDt.Rows(dtIdx))
            Next
        End If

        'プレビュー情報がなければ抜ける
        If rtnDt.Rows.Count = 0 Then
            Return ds
        End If

        'トランザクションを開始
        Using scope As TransactionScope = MyBase.BeginTransaction()
            Dim rtnBlc As Com.Base.BaseBLC = New LMI512BLC

            'プリントフラグ更新
            setDs = Me.Print1SetDataIn(setDs, New LMI512DS())
            setDs = MyBase.CallBLC(rtnBlc, "UpdatePrtFlg", setDs)

            'トランザクションを確定
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理(酢酸出荷依頼書(川本倉庫))
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Print2(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet = ds.Copy

        '印刷プレビュー用DataTable
        If ds.Tables(LMConst.RD) Is Nothing Then
            ds.Tables.Add(New RdPrevInfoDS.PREV_INFODataTable())
        End If
        Dim rtnDt As DataTable = ds.Tables(LMConst.RD)
        rtnDt.Clear()

        '印刷処理
        Dim setRptDt As DataTable = Me.DoPrint(setDs, PrintType.SAKUSAN_KAWAMOTO).Tables(LMConst.RD)

        'プレビュー情報を設定
        If setRptDt IsNot Nothing Then
            For dtIdx As Integer = 0 To setRptDt.Rows.Count - 1
                rtnDt.ImportRow(setRptDt.Rows(dtIdx))
            Next
        End If

        'プレビュー情報がなければ抜ける
        If rtnDt.Rows.Count = 0 Then
            Return ds
        End If

        'トランザクションを開始
        Using scope As TransactionScope = MyBase.BeginTransaction()
            Dim rtnBlc As Com.Base.BaseBLC = New LMI514BLC

            'プリントフラグサブ更新
            setDs = Me.Print2SetDataIn(setDs, New LMI514DS())
            setDs = MyBase.CallBLC(rtnBlc, "UpdatePrtFlg", setDs)

            'トランザクションを確定
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理(ファクシミリ連絡票(三菱ケミカル))
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Print3(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet = ds.Copy

        '印刷プレビュー用DataTable
        If ds.Tables(LMConst.RD) Is Nothing Then
            ds.Tables.Add(New RdPrevInfoDS.PREV_INFODataTable())
        End If
        Dim rtnDt As DataTable = ds.Tables(LMConst.RD)
        rtnDt.Clear()

        '印刷処理
        Dim setRptDt As DataTable = Me.DoPrint(setDs, PrintType.FAX_MITSUBISHI_CHEMICAL).Tables(LMConst.RD)

        'プレビュー情報を設定
        If setRptDt IsNot Nothing Then
            For dtIdx As Integer = 0 To setRptDt.Rows.Count - 1
                rtnDt.ImportRow(setRptDt.Rows(dtIdx))
            Next
        End If

        'プレビュー情報がなければ抜ける
        If rtnDt.Rows.Count = 0 Then
            Return ds
        End If

        'トランザクションを開始
        Using scope As TransactionScope = MyBase.BeginTransaction()
            Dim rtnBlc As Com.Base.BaseBLC = New LMI515BLC

            'プリントフラグサブ更新
            setDs = Me.Print3SetDataIn(setDs, New LMI515DS())
            setDs = MyBase.CallBLC(rtnBlc, "UpdatePrtFlg", setDs)

            'トランザクションを確定
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理(酢酸注文書(KHネオケム))
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Print4(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet = ds.Copy

        '印刷プレビュー用DataTable
        If ds.Tables(LMConst.RD) Is Nothing Then
            ds.Tables.Add(New RdPrevInfoDS.PREV_INFODataTable())
        End If
        Dim rtnDt As DataTable = ds.Tables(LMConst.RD)
        rtnDt.Clear()

        '印刷処理
        Dim setRptDt As DataTable = Me.DoPrint(setDs, PrintType.SAKUSAN_KH_NEOKEMU).Tables(LMConst.RD)

        'プレビュー情報を設定
        If setRptDt IsNot Nothing Then
            For dtIdx As Integer = 0 To setRptDt.Rows.Count - 1
                rtnDt.ImportRow(setRptDt.Rows(dtIdx))
            Next
        End If

        'プレビュー情報がなければ抜ける
        If rtnDt.Rows.Count = 0 Then
            Return ds
        End If

        'トランザクションを開始
        Using scope As TransactionScope = MyBase.BeginTransaction()
            Dim rtnBlc As Com.Base.BaseBLC = New LMI516BLC

            'プリントフラグサブ更新
            setDs = Me.Print4SetDataIn(setDs, New LMI516DS())
            setDs = MyBase.CallBLC(rtnBlc, "UpdatePrtFlg", setDs)

            'トランザクションを確定
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理(イソブタノール依頼書(ｼﾝｺｰｹﾐ神戸))
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Print5(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet = ds.Copy

        '印刷プレビュー用DataTable
        If ds.Tables(LMConst.RD) Is Nothing Then
            ds.Tables.Add(New RdPrevInfoDS.PREV_INFODataTable())
        End If
        Dim rtnDt As DataTable = ds.Tables(LMConst.RD)
        rtnDt.Clear()

        '印刷処理
        Dim setRptDt As DataTable = Me.DoPrint(setDs, PrintType.IBA_SHINKO_CHEMICAL_KOBE).Tables(LMConst.RD)

        'プレビュー情報を設定
        If setRptDt IsNot Nothing Then
            For dtIdx As Integer = 0 To setRptDt.Rows.Count - 1
                rtnDt.ImportRow(setRptDt.Rows(dtIdx))
            Next
        End If

        'プレビュー情報がなければ抜ける
        If rtnDt.Rows.Count = 0 Then
            Return ds
        End If

        'トランザクションを開始
        Using scope As TransactionScope = MyBase.BeginTransaction()
            Dim rtnBlc As Com.Base.BaseBLC = New LMI517BLC

            'プリントフラグサブ更新
            setDs = Me.Print5SetDataIn(setDs, New LMI517DS())
            setDs = MyBase.CallBLC(rtnBlc, "UpdatePrtFlg", setDs)

            'トランザクションを確定
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="printType">印刷種類</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet, ByVal printType As LMI511BLF.PrintType) As DataSet

        Dim setDs As DataSet() = Nothing
        Dim prtBlc As Com.Base.BaseBLC() = Nothing

        '帳票種類による判定
        Select Case printType
            Case LMI511BLF.PrintType.OUTKA_YOTEI
                '出荷予定表
                prtBlc = New Com.Base.BaseBLC() {New LMI512BLC()}
                setDs = New DataSet() {Me.Print1SetDataIn(ds, New LMI512DS())}
            Case LMI511BLF.PrintType.SAKUSAN_KAWAMOTO
                '酢酸出荷依頼書(川本倉庫)
                prtBlc = New Com.Base.BaseBLC() {New LMI514BLC()}
                setDs = New DataSet() {Me.Print2SetDataIn(ds, New LMI514DS())}
            Case LMI511BLF.PrintType.FAX_MITSUBISHI_CHEMICAL
                'ファクシミリ連絡票(三菱ケミカル)
                prtBlc = New Com.Base.BaseBLC() {New LMI515BLC()}
                setDs = New DataSet() {Me.Print3SetDataIn(ds, New LMI515DS())}
            Case LMI511BLF.PrintType.SAKUSAN_KH_NEOKEMU
                '酢酸注文書(KHネオケム)
                prtBlc = New Com.Base.BaseBLC() {New LMI516BLC()}
                setDs = New DataSet() {Me.Print4SetDataIn(ds, New LMI516DS())}
            Case LMI511BLF.PrintType.IBA_SHINKO_CHEMICAL_KOBE
                'イソブタノール依頼書(ｼﾝｺｰｹﾐ神戸)
                prtBlc = New Com.Base.BaseBLC() {New LMI517BLC()}
                setDs = New DataSet() {Me.Print5SetDataIn(ds, New LMI517DS())}
        End Select

        If prtBlc Is Nothing Then
            Return ds
        End If

        Dim rtnDs As DataSet = Nothing
        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        For dsIdx As Integer = 0 To prtBlc.Count - 1
            If setDs Is Nothing Then
                Continue For
            End If

            setDs(dsIdx).Merge(New RdPrevInfoDS)
            rtnDs = MyBase.CallBLC(prtBlc(dsIdx), "DoPrint", setDs(dsIdx))
            rdPrevDt.Merge(setDs(dsIdx).Tables(LMConst.RD))
        Next

        rtnDs.Tables(LMConst.RD).Clear()
        rtnDs.Tables(LMConst.RD).Merge(rdPrevDt)

        Return rtnDs

    End Function

    ''' <summary>
    ''' 印刷処理(出荷予定表)：DataSetの受け渡し
    ''' </summary>
    ''' <param name="ds511"></param>
    ''' <param name="ds512"></param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Print1SetDataIn(ByVal ds511 As DataSet, ByVal ds512 As DataSet) As DataSet

        Dim dt512 As DataTable = ds512.Tables(TABLE_NM.LMI512_IN)

        For dtIdx As Integer = 0 To ds511.Tables(TABLE_NM.IN_PRT1).Rows.Count - 1
            Dim dr511 As DataRow = ds511.Tables(TABLE_NM.IN_PRT1).Rows(dtIdx)
            Dim dr512 As DataRow = dt512.NewRow()

            dr512.Item("NRS_BR_CD") = dr511.Item("NRS_BR_CD").ToString()
            dr512.Item("EDI_CTL_NO") = dr511.Item("EDI_CTL_NO").ToString()
            dr512.Item("DATA_KIND") = dr511.Item("DATA_KIND").ToString()
            dr512.Item("PRTFLG") = dr511.Item("PRTFLG").ToString()
            dr512.Item("SR_DEN_NO") = dr511.Item("SR_DEN_NO").ToString()
            dr512.Item("PRINT_NO") = dr511.Item("PRINT_NO").ToString()
            dr512.Item("CUST_CD_L") = dr511.Item("CUST_CD_L").ToString()
            dr512.Item("CUST_CD_M") = dr511.Item("CUST_CD_M").ToString()

            dt512.Rows.Add(dr512)
        Next

        Return ds512

    End Function

    ''' <summary>
    ''' 印刷処理(酢酸出荷依頼書(川本倉庫))：DataSetの受け渡し
    ''' </summary>
    ''' <param name="ds511"></param>
    ''' <param name="ds514"></param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Print2SetDataIn(ByVal ds511 As DataSet, ByVal ds514 As DataSet) As DataSet

        Dim dt514 As DataTable = ds514.Tables(TABLE_NM.LMI514_IN)

        For dtIdx As Integer = 0 To ds511.Tables(TABLE_NM.IN_PRT2).Rows.Count - 1
            Dim dr511 As DataRow = ds511.Tables(TABLE_NM.IN_PRT2).Rows(dtIdx)
            Dim dr514 As DataRow = dt514.NewRow()

            dr514.Item("NRS_BR_CD") = dr511.Item("NRS_BR_CD").ToString()
            dr514.Item("EDI_CTL_NO") = dr511.Item("EDI_CTL_NO").ToString()
            dr514.Item("DATA_KIND") = dr511.Item("DATA_KIND").ToString()
            dr514.Item("PRTFLG_SUB") = dr511.Item("PRTFLG_SUB").ToString()
            dr514.Item("SR_DEN_NO") = dr511.Item("SR_DEN_NO").ToString()
            dr514.Item("PRINT_NO") = dr511.Item("PRINT_NO").ToString()
            dr514.Item("CUST_CD_L") = dr511.Item("CUST_CD_L").ToString()
            dr514.Item("CUST_CD_M") = dr511.Item("CUST_CD_M").ToString()

            dt514.Rows.Add(dr514)
        Next

        Return ds514

    End Function

    ''' <summary>
    ''' 印刷処理(ファクシミリ連絡票(三菱ケミカル))：DataSetの受け渡し
    ''' </summary>
    ''' <param name="ds511"></param>
    ''' <param name="ds515"></param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Print3SetDataIn(ByVal ds511 As DataSet, ByVal ds515 As DataSet) As DataSet

        Dim dt515 As DataTable = ds515.Tables(TABLE_NM.LMI515_IN)

        For dtIdx As Integer = 0 To ds511.Tables(TABLE_NM.IN_PRT3).Rows.Count - 1
            Dim dr511 As DataRow = ds511.Tables(TABLE_NM.IN_PRT3).Rows(dtIdx)
            Dim dr515 As DataRow = dt515.NewRow()

            dr515.Item("NRS_BR_CD") = dr511.Item("NRS_BR_CD").ToString()
            dr515.Item("EDI_CTL_NO") = dr511.Item("EDI_CTL_NO").ToString()
            dr515.Item("DATA_KIND") = dr511.Item("DATA_KIND").ToString()
            dr515.Item("PRTFLG_SUB") = dr511.Item("PRTFLG_SUB").ToString()
            dr515.Item("SR_DEN_NO") = dr511.Item("SR_DEN_NO").ToString()
            dr515.Item("PRINT_NO") = dr511.Item("PRINT_NO").ToString()
            dr515.Item("CUST_CD_L") = dr511.Item("CUST_CD_L").ToString()
            dr515.Item("CUST_CD_M") = dr511.Item("CUST_CD_M").ToString()

            dt515.Rows.Add(dr515)
        Next

        Return ds515

    End Function

    ''' <summary>
    ''' 印刷処理(酢酸注文書(KHネオケム))：DataSetの受け渡し
    ''' </summary>
    ''' <param name="ds511"></param>
    ''' <param name="ds516"></param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Print4SetDataIn(ByVal ds511 As DataSet, ByVal ds516 As DataSet) As DataSet

        Dim dt516 As DataTable = ds516.Tables(TABLE_NM.LMI516_IN)

        For dtIdx As Integer = 0 To ds511.Tables(TABLE_NM.IN_PRT4).Rows.Count - 1
            Dim dr511 As DataRow = ds511.Tables(TABLE_NM.IN_PRT4).Rows(dtIdx)
            Dim dr516 As DataRow = dt516.NewRow()

            dr516.Item("NRS_BR_CD") = dr511.Item("NRS_BR_CD").ToString()
            dr516.Item("EDI_CTL_NO") = dr511.Item("EDI_CTL_NO").ToString()
            dr516.Item("DATA_KIND") = dr511.Item("DATA_KIND").ToString()
            dr516.Item("PRTFLG_SUB") = dr511.Item("PRTFLG_SUB").ToString()
            dr516.Item("SR_DEN_NO") = dr511.Item("SR_DEN_NO").ToString()
            dr516.Item("CUST_CD_L") = dr511.Item("CUST_CD_L").ToString()
            dr516.Item("CUST_CD_M") = dr511.Item("CUST_CD_M").ToString()
            dr516.Item("OUTKA_DATE_A") = dr511.Item("OUTKA_DATE").ToString()
            dr516.Item("OUTKA_POSI_BU_CD_PA") = dr511.Item("OUTKA_POSI_BU_CD_PA").ToString()
            dr516.Item("JYUCHU_GOODS_CD") = dr511.Item("JYUCHU_GOODS_CD").ToString()

            dt516.Rows.Add(dr516)
        Next

        Return ds516

    End Function

    ''' <summary>
    ''' 印刷処理(イソブタノール依頼書(ｼﾝｺｰｹﾐ神戸))：DataSetの受け渡し
    ''' </summary>
    ''' <param name="ds511"></param>
    ''' <param name="ds517"></param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Print5SetDataIn(ByVal ds511 As DataSet, ByVal ds517 As DataSet) As DataSet

        Dim dt517 As DataTable = ds517.Tables(TABLE_NM.LMI517_IN)

        For dtIdx As Integer = 0 To ds511.Tables(TABLE_NM.IN_PRT5).Rows.Count - 1
            Dim dr511 As DataRow = ds511.Tables(TABLE_NM.IN_PRT5).Rows(dtIdx)
            Dim dr517 As DataRow = dt517.NewRow()

            dr517.Item("NRS_BR_CD") = dr511.Item("NRS_BR_CD").ToString()
            dr517.Item("EDI_CTL_NO") = dr511.Item("EDI_CTL_NO").ToString()
            dr517.Item("DATA_KIND") = dr511.Item("DATA_KIND").ToString()
            dr517.Item("PRTFLG_SUB") = dr511.Item("PRTFLG_SUB").ToString()
            dr517.Item("SR_DEN_NO") = dr511.Item("SR_DEN_NO").ToString()
            dr517.Item("CUST_CD_L") = dr511.Item("CUST_CD_L").ToString()
            dr517.Item("CUST_CD_M") = dr511.Item("CUST_CD_M").ToString()
            dr517.Item("OUTKA_DATE_A") = dr511.Item("OUTKA_DATE").ToString()

            dt517.Rows.Add(dr517)
        Next

        Return ds517

    End Function
#End Region

#End Region 'Method

End Class
