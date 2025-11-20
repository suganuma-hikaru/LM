' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 在庫管理
'  プログラムID     :  LMD020    : 在庫移動
'  作  成  者       :  [高道]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMD020BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD020BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMD020DAC = New LMD020DAC()

    '2017/09/25 修正 李↓
    ''20151106 tsunehira add
    ' ''' <summary>
    ' ''' 選択した言語を格納するフィールド
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _LangFlg As String = MessageManager.MessageLanguage
    '2017/09/25 修正 李↑

#End Region
#Region "Const"

    '2015.11.02 tusnehira add
    '英語化対応
    Private Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Private Const MESSEGE_LANGUAGE_ENGLISH As String = "1"

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' データ件数検索
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
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    ''' <summary>
    ''' 申請外の商品保管ルール検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function getTouSituExp(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, "getTouSituExp")

    End Function
    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

#End Region

#Region "保存処理"

#Region "新規登録"

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        '新規登録
        Call Me.InsertData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As Boolean

        '在庫の新規登録、更新
        Dim rtnResult As Boolean = Me.SetZaiTrsData(ds)

        '在庫移動トランザクションの新規登録
        rtnResult = rtnResult AndAlso Me.SetIdoTrsData(ds)

        Return rtnResult

    End Function

#End Region

#Region "共通更新"


    ''' <summary>
    ''' 在庫移動トランザクション新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetIdoTrsData(ByVal ds As DataSet) As Boolean

        'レコード番号の設定
        ds = Me.SetRecNoData(ds)

        '要望管理009859
        '出庫(入庫)指示番号の設定
        ds = Me.SetInoutkoNo(ds)

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMD020_IDO")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMD020_IDO")

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            '新規登録
            rtnResult = Me.ServerChkJudge(setDs, "InsertIdoTrs")

            'エラーの場合はそのまま返却
            If rtnResult = False Then
                Return rtnResult
            End If

        Next

        Return rtnResult

    End Function

    ''' <summary>
    ''' 在庫新規登録、更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetZaiTrsData(ByVal ds As DataSet) As Boolean

        '在庫レコード番号の設定
        ds = Me.SetZaiRecNoData(ds)

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMD020_ZAI_NEW")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMD020_ZAI_NEW")

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            '新規
            rtnResult = Me.ServerChkJudge(setDs, "InsertZaiTrs")

            'エラーの場合はそのまま返却
            If rtnResult = False Then
                Return rtnResult
            End If

        Next

        '元のデータ
        Dim dtOld As DataTable = ds.Tables("LMD020_ZAI_OLD")
        Dim maxOld As Integer = dtOld.Rows.Count - 1

        '別インスタンス
        Dim inTblOld As DataTable = setDs.Tables("LMD020_ZAI_OLD")

        For i As Integer = 0 To maxOld

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTblOld.ImportRow(dtOld.Rows(i))

            '更新
            rtnResult = Me.ServerChkJudge(setDs, "UpdataZaiTrs")

            'エラーの場合はそのまま返却
            If rtnResult = False Then
                Return rtnResult
            End If

        Next

        Return rtnResult

    End Function

    ''' <summary>
    ''' レコード番号の設定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetRecNoData(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LMD020_IDO")
        Dim max As Integer = dt.Rows.Count - 1

        'レコード番号の設定
        For i As Integer = 0 To max

            'PKがない場合、設定
            If String.IsNullOrEmpty(dt.Rows(i).Item("REC_NO").ToString()) = True Then
                dt.Rows(i).Item("REC_NO") = Me.GetRecNo(ds)
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 出庫(入庫)指示番号の設定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>要望管理009859</remarks>
    Private Function SetInoutkoNo(ByVal ds As DataSet) As DataSet

        If ds.Tables("LMD020_IDO").Rows.Count = 0 Then
            Return ds
        End If

        If Not "07".Equals(ds.Tables("LMD020_IDO").Rows(0).Item("REMARK_KBN").ToString()) Then
            Return ds
        End If

        '入出庫指示番号の採番（一回の保存処理で登録される在庫移動データには同じ番号が振られるので取得は1回のみ）
        Dim num As New NumberMasterUtility
        Dim outkoNo As String = num.GetAutoCode(NumberMasterUtility.NumberKbn.OUTKO_NO, Me, ds.Tables("LMD020_IDO").Rows(0).Item("NRS_BR_CD").ToString())
        Dim inkoNo As String = num.GetAutoCode(NumberMasterUtility.NumberKbn.INKO_NO, Me, ds.Tables("LMD020_IDO").Rows(0).Item("NRS_BR_CD").ToString())

        '取得した出庫(入庫)指示番号をすべての在庫移動データにセットする
        For i As Integer = 0 To ds.Tables("LMD020_IDO").Rows.Count - 1
            With ds.Tables("LMD020_IDO").Rows(i)
                .Item("OUTKO_NO") = outkoNo
                .Item("INKO_NO") = inkoNo
            End With
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 在庫レコード番号の設定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetZaiRecNoData(ByVal ds As DataSet) As DataSet

        Dim dtNew As DataTable = ds.Tables("LMD020_ZAI_NEW")
        Dim dtIdo As DataTable = ds.Tables("LMD020_IDO")
        Dim max As Integer = dtNew.Rows.Count - 1
        Dim zaiRecNo As String = String.Empty

        For i As Integer = 0 To max

            'PKがない場合、設定
            If String.IsNullOrEmpty(dtNew.Rows(i).Item("ZAI_REC_NO").ToString()) = True Then
                zaiRecNo = Me.GetZaiRecNo(ds)
                dtNew.Rows(i).Item("ZAI_REC_NO") = zaiRecNo
                dtIdo.Rows(i).Item("N_ZAI_REC_NO") = zaiRecNo
            End If

        Next

        Return ds

    End Function

#End Region

#End Region

#Region "強制出庫"

    ''' <summary>
    ''' ファイル出力先/4号機指定ステーション取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectSendInfo(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, "SelectSendInfo")

    End Function

    ''' <summary>
    ''' 自動倉庫出庫予定テーブル登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function InsertOutkoPlanAutoWh(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, "InsertOutkoPlanAutoWh")

    End Function

    ''' <summary>
    ''' 日別連番取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectFileSeq(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, "SelectFileSeq")

    End Function

    ''' <summary>
    ''' 日別連番登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function InsertFileSeq(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, "InsertFileSeq")

    End Function

    ''' <summary>
    ''' 日別連番更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function UpdateFileSeq(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, "UpdateFileSeq")

    End Function

#End Region

#End Region

#Region "チェック"

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

    End Function

    '要望番号:1350 terakawa 2012.08.27 Start
    ''' <summary>
    ''' 同一商品・LOTチェック(同一置き場に同一商品、LOTがあった場合ワーニング)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>データセット</returns>
    ''' <remarks></remarks>
    Private Function ChkGoodsLot(ByVal ds As DataSet) As DataSet

        Dim dtNew As DataTable = ds.Tables("LMD020_ZAI_NEW")
        Dim dtOld As DataTable = ds.Tables("LMD020_ZAI_OLD")
        Dim Count As Integer = dtNew.Rows.Count - 1

        Dim setDs As DataSet = ds.Copy
        Dim setDtNew As DataTable = setDs.Tables("LMD020_ZAI_NEW")
        Dim setDtOld As DataTable = setDs.Tables("LMD020_ZAI_OLD")
        Dim setDtCustDtl As DataTable = setDs.Tables("CUST_DETAILS")
        Dim worningDt As DataTable = ds.Tables("LMD020_WORNING")
        Dim worningDr As DataRow

        '荷主明細から同一置き場・商品チェック特殊荷主情報を取得
        ds = Me.DacAccess(ds, "GetCustDetail")
        Dim dtCustDtl As DataTable = ds.Tables("CUST_DETAILS")

        For i As Integer = 0 To Count
            '値のクリア
            setDs.Clear()

            'チェック用データセットにインポート
            setDtNew.ImportRow(dtNew.Rows(i))
            'ZAI_OLDが複数行の場合（平行移動の場合)ZAI_NEWと対応する行を、
            '単一行の場合（複数移動、または平行移動で対象が1件)は1行目をセット
            If dtOld.Rows.Count = 1 Then
                setDtOld.ImportRow(dtOld.Rows(0))
            Else
                setDtOld.ImportRow(dtOld.Rows(i))
            End If

            If dtCustDtl.Rows.Count > 0 Then
                setDtCustDtl.ImportRow(dtCustDtl.Rows(0))
            End If

            '在庫データの重複チェック
            setDs = Me.DacAccess(setDs, "ChkGoodsLot")
            If GetResultCount() > 0 Then
                worningDr = ds.Tables("LMD020_WORNING").NewRow()
                With worningDr
                    .Item("GOODS_CD_CUST") = setDtNew.Rows(0).Item("GOODS_CD_CUST").ToString()
                    .Item("TOU_NO") = setDtNew.Rows(0).Item("TOU_NO").ToString()
                    .Item("SITU_NO") = setDtNew.Rows(0).Item("SITU_NO").ToString()
                    .Item("ZONE_CD") = setDtNew.Rows(0).Item("ZONE_CD").ToString()
                    .Item("LOCA") = setDtNew.Rows(0).Item("LOCA").ToString()
                    .Item("LOT_NO") = setDtNew.Rows(0).Item("LOT_NO").ToString()
                End With
                ds.Tables("LMD020_WORNING").Rows.Add(worningDr)
            End If
        Next

        Return ds

    End Function
    '要望番号:1350 terakawa 2012.08.27 End

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

    ''' <summary>
    ''' ZAI_REC_NOを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>ZaiRecNo</returns>
    ''' <remarks></remarks>
    Private Function GetZaiRecNo(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.ZAI_REC_NO, Me, ds.Tables("LMD020_ZAI_NEW").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' REC_NOを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>ZaiRecNo</returns>
    ''' <remarks></remarks>
    Private Function GetRecNo(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.IDO_REC_NO, Me, ds.Tables("LMD020_ZAI_NEW").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' 全ての行にValueの値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="colNm">列名</param>
    ''' <param name="value">設定したい値</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetValueData(ByVal ds As DataSet, ByVal tblNm As String, ByVal colNm As String(), ByVal value As String()) As DataSet

        Dim dt As DataTable = ds.Tables(tblNm)
        Dim max As Integer = dt.Rows.Count - 1
        Dim count As Integer = value.Length - 1

        For i As Integer = 0 To max


            For j As Integer = 0 To count

                dt.Rows(i).Item(colNm(j)) = value(j)

            Next

        Next

        Return ds

    End Function

#End Region

End Class
