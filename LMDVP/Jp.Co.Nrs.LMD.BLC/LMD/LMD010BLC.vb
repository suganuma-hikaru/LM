' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 在庫管理
'  プログラムID     :  LMD010    : 在庫振替入力
'  作  成  者       :  [高道]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMD010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD010BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMD010DAC = New LMD010DAC()

    '2017/09/25 修正 李↓
    ''20151106 tsunehira add
    ' ''' <summary>
    ' ''' 選択した言語を格納するフィールド
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _LangFlg As String = MessageManager.MessageLanguage
    '2017/09/25 修正 李↑

    'データセット名称
    Private Const TABLE_NM_INKA_L As String = "LMD010_INKA_L"

#End Region

#Region "Const"

    '2015.11.02 tusnehira add
    '英語化対応
    Private Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Private Const MESSEGE_LANGUAGE_ENGLISH As String = "1"

#End Region

#Region "Method"

#Region "日付検索処理"

    ''' <summary>
    ''' 請求日検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectChkSeiqDate(ByVal ds As DataSet) As Boolean

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMD010_KAGAMI_IN")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim inTbl As DataTable = ds.Tables("LMD010_KAGAMI_IN")
        Dim setDtSAGYO As DataTable = ds.Tables("LMD010_SAGYO_SKYU_DATE")

        'データの抽出
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "SelectSeiqDate")

        '値設定
        Dim furikaeDate As String = dt.Rows(0).Item("CHECK_DATE").ToString()
        If 0 < setDtSAGYO.Rows.Count Then
            'START YANAI No.44
            'If furikaeDate < setDtSAGYO.Rows(0).Item("SKYU_DATE").ToString() = True Then
            If furikaeDate <= setDtSAGYO.Rows(0).Item("SKYU_DATE").ToString() = True Then
                'END YANAI No.44
                '2015.10.22 tusnehira add
                '英語化対応
                MyBase.SetMessage("E771")
                'MyBase.SetMessage("E285", New String() {"作業料"})
                Return False
            End If
        End If

        Return True

    End Function

    ''' <summary>
    ''' 移動、請求日検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectChkIdoDate(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LMD010_KAGAMI_IN")
        Dim outDtSAGYO As DataTable = ds.Tables("LMD010_SAGYO_SKYU_DATE")

        'データの抽出       
        Dim setDtSAGYO As DataTable = ds.Tables("LMD010_SAGYO_SKYU_DATE")
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "SelectSeiqDate")

        '値設定
        If 0 < setDtSAGYO.Rows.Count Then
            outDtSAGYO.ImportRow(setDtSAGYO.Rows(0))
        End If

        '元のデータ
        dt = ds.Tables("LMD010_IDO_TRS_IN")
        Dim outDt As DataTable = ds.Tables("LMD010_IDO_TRS_OUT")
        Dim max As Integer = dt.Rows.Count - 1

        Dim count As Integer = 0

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMD010_IDO_TRS_IN")
        Dim setDt As DataTable = setDs.Tables("LMD010_IDO_TRS_OUT")

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'データの抽出
            rtnResult = Me.ServerChkJudge(setDs, "SelectIdoDate")

            count = MyBase.GetResultCount()

            'エラーの場合はそのまま返却
            If rtnResult = False OrElse count = 0 Then
                Return ds
            End If

            '値設定
            outDt.ImportRow(setDt.Rows(0))

        Next

        Return ds

    End Function

    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    ''' <summary>
    ''' 申請外の商品保管ルール検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function getTouSituExp(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, "getTouSituExp")

    End Function
    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

#End Region

#Region "荷主明細 BYKキープ品管理 有無 検索"

    ''' <summary>
    ''' 荷主明細 BYKキープ品管理 有無 検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectIsBykKeepGoodsCd(ByVal ds As DataSet) As DataSet

        'データの抽出
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "SelectIsBykKeepGoodsCd")

        Return ds

    End Function

#End Region

#Region "新規登録"

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        '請求鑑ヘッダのチェック
        Dim rtnResult As Boolean = Me.SelectChkSeiqDate(ds)

        '入荷番号の採番
        Dim inkaNoL As String = Me.GetInkaNoL(ds)
        Dim value As String() = New String() {inkaNoL}
        Dim colNm As String() = New String() {"INKA_NO_L"}

        '入荷(大)に採番した値を設定
        ds = Me.SetValueData(ds, TABLE_NM_INKA_L, colNm, value)

        '入荷(中)に採番した値を設定
        ds = Me.SetValueData(ds, "LMD010_INKA_M", colNm, value)

        '入荷(小)に採番した値を設定
        ds = Me.SetValueData(ds, "LMD010_INKA_S", colNm, value)

        '振替先在庫に採番した値を設定
        ds = Me.SetValueData(ds, "LMD010_ZAI_NEW", colNm, value)

        '出荷番号の採番
        Dim outKaNoL As String = Me.GetOUTKA_NO_L(ds)
        Dim outValue As String() = New String() {outKaNoL}
        Dim outColNm As String() = New String() {"OUTKA_NO_L"}

        '出荷(大)に採番した値を設定
        ds = Me.SetValueData(ds, "LMD010_OUTKA_L", outColNm, outValue)

        '出荷(中)に採番した値を設定
        ds = Me.SetValueData(ds, "LMD010_OUTKA_M", outColNm, outValue)

        '出荷(小)に採番した値を設定
        ds = Me.SetValueData(ds, "LMD010_OUTKA_S", outColNm, outValue)

        '新規登録
        Call Me.InsertData(ds, inkaNoL, outKaNoL)

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet, ByVal inkaNoL As String, ByVal outKaNoL As String) As Boolean

        Dim rtnResult As Boolean = True

        '振替データ作成用のデータ取得
        rtnResult = rtnResult AndAlso Me.SelectFurikaeData(ds)

        '振替元在庫データ取得
        rtnResult = rtnResult AndAlso Me.SelectFurikaeMotoZaikoData(ds)

        '検索結果より、データセットの再設定
        If rtnResult = True Then
            Call Me.ResetDataSet(ds)
        End If

        '出荷(大)の新規登録
        rtnResult = rtnResult AndAlso Me.InsertOutKaLData(ds)

        '入荷(大)の新規登録
        rtnResult = rtnResult AndAlso Me.InsertInkaLData(ds)

        '出荷(中)の新規登録
        rtnResult = rtnResult AndAlso Me.SetOutKaMData(ds)

        '入荷(中)の新規登録
        rtnResult = rtnResult AndAlso Me.SetInkaMData(ds)

        '出荷(小)の新規登録
        rtnResult = rtnResult AndAlso Me.SetOutKaSData(ds)

        '入荷(小)の新規登録
        rtnResult = rtnResult AndAlso Me.SetInkaSData(ds)

        '在庫の新規登録、更新
        rtnResult = rtnResult AndAlso Me.SetZaiTrsData(ds)

        '作業の新規登録
        rtnResult = rtnResult AndAlso Me.SetSagyoData(ds, inkaNoL, outKaNoL)

#If True Then       'ADD 2019/01/08 依頼番号 : 004100   【LMS】在庫振替の新規画面追加・編集・削除・再印刷機能の追加機能
        '在庫振替の新規登録
        rtnResult = rtnResult AndAlso Me.InsertFurikaeData(ds)

#End If
        Return rtnResult

    End Function

#End Region

#Region "共通更新"

    ''' <summary>
    ''' 振替データを検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SelectFurikaeData(ByVal ds As DataSet) As Boolean

        Dim rtnResult As Boolean = True

        'データの抽出
        rtnResult = Me.ServerChkJudge(ds, "FuriMakeData")

        'エラーの場合はそのまま返却
        If rtnResult = False Then
            Return rtnResult
        End If

        Return rtnResult

    End Function

    ''' <summary>
    ''' 振替元在庫データを検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SelectFurikaeMotoZaikoData(ByVal ds As DataSet) As Boolean
        '元のデータ
        Dim dt As DataTable = ds.Tables("LMD010_ZAI_OLDIN")
        Dim outDt As DataTable = ds.Tables("LMD010_ZAI_OLDOUT")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMD010_ZAI_OLDIN")
        Dim setDt As DataTable = setDs.Tables("LMD010_ZAI_OLDOUT")

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'データの抽出
            rtnResult = Me.ServerChkJudge(setDs, "FuriZaiData")

            'エラーの場合はそのまま返却
            If rtnResult = False Then
                Return rtnResult
            End If

            '値設定
            outDt.ImportRow(setDt.Rows(0))

        Next

        Return rtnResult

    End Function

    ''' <summary>
    ''' 入荷(大)新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertInkaLData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertInKaL")

    End Function

    ''' <summary>
    ''' 入荷(中)新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetInkaMData(ByVal ds As DataSet) As Boolean

        '新規
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "InsertInKaM")

        Return rtnResult

    End Function

    ''' <summary>
    ''' 入荷(小)新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetInkaSData(ByVal ds As DataSet) As Boolean

        '在庫レコード番号の設定
        ds = Me.SetZaiRecNoData(ds)

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMD010_INKA_S")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMD010_INKA_S")

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            '新規
            rtnResult = Me.ServerChkJudge(setDs, "InsertInKaS")

            'エラーの場合はそのまま返却
            If rtnResult = False Then
                Return rtnResult
            End If

        Next

        Return rtnResult

    End Function

#If True Then   'ADD 2019/01/08 依頼番号 : 004100   【LMS】在庫振替の新規画面追加・編集・削除・再印刷機能の追加機能
    ''' <summary>
    ''' 振替データ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertFurikaeData(ByVal ds As DataSet) As Boolean

        '新規
        Return Me.ServerChkJudge(ds, "InsertFurikae")

    End Function
#End If


    ''' <summary>
    ''' 出荷(大)新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertOutKaLData(ByVal ds As DataSet) As Boolean

        '振替Noの設定
        ds = Me.SetFurikaeNoData(ds)

        Return Me.ServerChkJudge(ds, "InsertOutKaL")

    End Function

    ''' <summary>
    ''' 出荷(中)新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetOutKaMData(ByVal ds As DataSet) As Boolean

        '新規
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "InsertOutKaM")

        Return rtnResult

    End Function

    ''' <summary>
    ''' 出荷(小)新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetOutKaSData(ByVal ds As DataSet) As Boolean

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMD010_OUTKA_S")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMD010_OUTKA_S")

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            '新規
            rtnResult = Me.ServerChkJudge(setDs, "InsertOutKaS")

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

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMD010_ZAI_NEW")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMD010_ZAI_NEW")

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            '新規
            rtnResult = Me.ServerChkJudge(setDs, "InsertSakiZai")

            'エラーの場合はそのまま返却
            If rtnResult = False Then
                Return rtnResult
            End If

        Next

        '元のデータ
        Dim dtOld As DataTable = ds.Tables("LMD010_ZAI_OLD")
        Dim maxOld As Integer = dtOld.Rows.Count - 1

        '別インスタンス
        Dim inTblOld As DataTable = setDs.Tables("LMD010_ZAI_OLD")

        For i As Integer = 0 To maxOld

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTblOld.ImportRow(dtOld.Rows(i))

            '更新
            rtnResult = Me.ServerChkJudge(setDs, "UpdataMotoZai")

            'エラーの場合はそのまま返却
            If rtnResult = False Then
                Return rtnResult
            End If

        Next

        Return rtnResult

    End Function

    ''' <summary>
    ''' 作業レコードの更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inkaNoL"></param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetSagyoData(ByVal ds As DataSet, ByVal inkaNoL As String, ByVal outKaNoL As String) As Boolean

        '作業データの値設定
        ds = Me.SetSagyoToInkaNoData(ds, inkaNoL, outKaNoL)

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMD010_SAGYO_INKA")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMD010_SAGYO_INKA")

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            '新規
            rtnResult = Me.ServerChkJudge(setDs, "InsertSakiSagyo")

            'エラーの場合はそのまま返却
            If rtnResult = False Then
                Return rtnResult
            End If

        Next

        '再度コピー
        setDs = ds.Copy()

        '元のデータ
        Dim dtOld As DataTable = ds.Tables("LMD010_SAGYO_OUTKA")
        Dim maxOld As Integer = dtOld.Rows.Count - 1

        '別インスタンス
        Dim inTblOld As DataTable = setDs.Tables("LMD010_SAGYO_OUTKA")

        For i As Integer = 0 To maxOld

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTblOld.ImportRow(dtOld.Rows(i))

            '更新
            rtnResult = Me.ServerChkJudge(setDs, "InsertMotoSagyo")

            'エラーの場合はそのまま返却
            If rtnResult = False Then
                Return rtnResult
            End If

        Next

        Return rtnResult

    End Function

    ''' <summary>
    ''' 作業Noの設定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inkaNoL">入荷(大)番号</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetSagyoToInkaNoData(ByVal ds As DataSet, ByVal inkaNoL As String, ByVal outKaNoL As String) As DataSet

        Dim dtNew As DataTable = ds.Tables("LMD010_SAGYO_INKA")
        Dim dtOld As DataTable = ds.Tables("LMD010_SAGYO_OUTKA")
        Dim maxNew As Integer = dtNew.Rows.Count - 1
        Dim maxOld As Integer = dtOld.Rows.Count - 1

        '振替先の作業Noの設定
        For i As Integer = 0 To maxNew

            'PKがない場合、設定
            If String.IsNullOrEmpty(dtNew.Rows(i).Item("SAGYO_REC_NO").ToString()) = True Then
                dtNew.Rows(i).Item("SAGYO_REC_NO") = Me.GetSagyoRecNo(ds)
            End If

            '入出荷管理番号L + Mの設定
            dtNew.Rows(i).Item("INOUTKA_NO_LM") = String.Concat(inkaNoL, "001")

        Next

        '振替元の作業Noの設定
        For i As Integer = 0 To maxOld

            'PKがない場合、設定
            If String.IsNullOrEmpty(dtOld.Rows(i).Item("SAGYO_REC_NO").ToString()) = True Then
                dtOld.Rows(i).Item("SAGYO_REC_NO") = Me.GetSagyoRecNo(ds)
            End If

            '入出荷管理番号L + Mの設定
            dtOld.Rows(i).Item("INOUTKA_NO_LM") = String.Concat(outKaNoL, "001")

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 振替管理番号の設定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetFurikaeNoData(ByVal ds As DataSet) As DataSet

        Dim dtInkaL As DataTable = ds.Tables(TABLE_NM_INKA_L)
        Dim dtOutkaL As DataTable = ds.Tables("LMD010_OUTKA_L")
        Dim dtFurikae As DataTable = ds.Tables("LMD010_FURIKAE")    'ADD 2019/01/08 依頼番号 : 004100   【LMS】在庫振替の新規画面追加・編集・削除・再印刷機能の追加機能
        Dim max As Integer = dtInkaL.Rows.Count - 1
        Dim furiNo As String = String.Empty

        For i As Integer = 0 To max

            'PKがない場合、設定
            If String.IsNullOrEmpty(dtInkaL.Rows(i).Item("FURI_NO").ToString()) = True Then
                furiNo = Me.GetFuri_NO(ds)
                dtInkaL.Rows(i).Item("FURI_NO") = furiNo
                dtOutkaL.Rows(i).Item("FURI_NO") = furiNo

#If True Then   'ADD 2019/01/08 依頼番号 : 004100   【LMS】在庫振替の新規画面追加・編集・削除・再印刷機能の追加機能
                If i = 0 Then
                    dtFurikae.Rows(i).Item("FURI_NO") = furiNo
                End If
#End If
            End If


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

        Dim dtInkaS As DataTable = ds.Tables("LMD010_INKA_S")
        Dim dtZaiNew As DataTable = ds.Tables("LMD010_ZAI_NEW")
        Dim max As Integer = dtInkaS.Rows.Count - 1
        Dim zaiRecNo As String = String.Empty

        For i As Integer = 0 To max

            'PKがない場合、設定
            If String.IsNullOrEmpty(dtInkaS.Rows(i).Item("ZAI_REC_NO").ToString()) = True Then
                zaiRecNo = Me.GetZaiRecNo(ds)
                dtInkaS.Rows(i).Item("ZAI_REC_NO") = zaiRecNo
                dtZaiNew.Rows(i).Item("ZAI_REC_NO") = zaiRecNo
            End If


        Next

        Return ds

    End Function

    ''' <summary>
    ''' 検索結果を受けてデータセットの再設定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub ResetDataSet(ByVal ds As DataSet)

        '入荷(大)の再セット
        Call Me.ResetDataSetInkaL(ds)

        '入荷(中)の再セット
        Call Me.ResetDataSetInkaM(ds)

        '出荷(中)の再セット
        Call Me.ResetDataSetOutkaM(ds)

        '出荷(小)の再セット
        Call Me.ResetDataSetOutkaS(ds)

        '振替先作業の再セット
        Call Me.ResetDataSetSagyoNew(ds)

        '振替元作業の再セット
        Call Me.ResetDataSetSagyoOld(ds)

        '振替先在庫の再セット
        Call Me.ResetDataSetZaikoNew(ds)

        '振替元在庫の再セット
        Call Me.ResetDataSetZaikoOld(ds)

    End Sub

    ''' <summary>
    ''' 入荷(大)の再セット
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub ResetDataSetInkaL(ByVal ds As DataSet)

        'Select結果をそのまま対象テーブルにセット
        Dim dr As DataRow = ds.Tables(TABLE_NM_INKA_L).Rows(0)
        Dim drOut As DataRow = ds.Tables("LMD010OUT").Rows(0)

        dr.Item("HOKAN_YN") = drOut.Item("HOKAN_YN").ToString()
        dr.Item("HOKAN_FREE_KIKAN") = drOut.Item("HOKAN_FREE_KIKAN").ToString()


    End Sub

    ''' <summary>
    ''' 入荷(中)の再セット
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub ResetDataSetInkaM(ByVal ds As DataSet)

        'Select結果をそのまま対象テーブルにセット
        Dim dr As DataRow = ds.Tables("LMD010_INKA_M").Rows(0)
        Dim drOut As DataRow = ds.Tables("LMD010OUT").Rows(0)

        dr.Item("GOODS_CD_NRS") = drOut.Item("SAKI_GOODS_CD_NRS").ToString()

    End Sub

    ''' <summary>
    ''' 出荷(中)の再セット
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub ResetDataSetOutkaM(ByVal ds As DataSet)
        'Select結果をそのまま対象テーブルにセット
        Dim dr As DataRow = ds.Tables("LMD010_OUTKA_M").Rows(0)
        Dim drOut As DataRow = ds.Tables("LMD010OUT").Rows(0)

        dr.Item("UNSO_ONDO_KB") = drOut.Item("UNSO_ONDO_KB").ToString()
        dr.Item("IRIME") = drOut.Item("IRIME").ToString()
        dr.Item("IRIME_UT") = drOut.Item("IRIME_UT").ToString()

    End Sub

    ''' <summary>
    ''' 出荷(小)の再セット
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub ResetDataSetOutkaS(ByVal ds As DataSet)

        Dim max As Integer = ds.Tables("LMD010_OUTKA_S").Rows.Count - 1
        Dim dr As DataRow = Nothing
        Dim drZaiOut As DataRow = Nothing

        For i As Integer = 0 To max
            dr = ds.Tables("LMD010_OUTKA_S").Rows(i)
            drZaiOut = ds.Tables("LMD010_ZAI_OLDOUT").Rows(i)

            dr.Item("TOU_NO") = drZaiOut.Item("TOU_NO").ToString()
            dr.Item("SITU_NO") = drZaiOut.Item("SITU_NO").ToString()
            dr.Item("ZONE_CD") = drZaiOut.Item("ZONE_CD").ToString()
            dr.Item("LOCA") = drZaiOut.Item("LOCA").ToString()
            dr.Item("LOT_NO") = drZaiOut.Item("LOT_NO").ToString()
            dr.Item("SERIAL_NO") = drZaiOut.Item("SERIAL_NO").ToString()
            dr.Item("ZAI_REC_NO") = drZaiOut.Item("ZAI_REC_NO").ToString()
            dr.Item("INKA_NO_L") = drZaiOut.Item("INKA_NO_L").ToString()
            dr.Item("INKA_NO_M") = drZaiOut.Item("INKA_NO_M").ToString()
            dr.Item("INKA_NO_S") = drZaiOut.Item("INKA_NO_S").ToString()
            dr.Item("ALCTD_CAN_NB") = (Convert.ToInt32(drZaiOut.Item("ALLOC_CAN_NB").ToString()) - Convert.ToInt32(dr.Item("ALCTD_CAN_NB").ToString())).ToString()
            dr.Item("ALCTD_CAN_QT") = (Convert.ToDecimal(drZaiOut.Item("ALLOC_CAN_QT").ToString()) - Convert.ToDecimal(dr.Item("ALCTD_CAN_QT").ToString())).ToString()
            dr.Item("IRIME") = drZaiOut.Item("IRIME").ToString()
            dr.Item("REMARK") = drZaiOut.Item("REMARK").ToString()

        Next

    End Sub

    ''' <summary>
    ''' 振替先作業の再セット
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub ResetDataSetSagyoNew(ByVal ds As DataSet)

        Dim max As Integer = ds.Tables("LMD010_SAGYO_INKA").Rows.Count - 1
        Dim dr As DataRow = Nothing
        Dim drOut As DataRow = Nothing
        drOut = ds.Tables("LMD010OUT").Rows(0)

        For i As Integer = 0 To max
            dr = ds.Tables("LMD010_SAGYO_INKA").Rows(i)

            '作業請求先コードが設定されているか判定
            If String.IsNullOrEmpty(drOut.Item("SAGYO_SEIQTO_CD").ToString()) = False Then
                'NULLじゃない場合は作業請求先コードをセット
                dr("SEIQTO_CD") = drOut.Item("SAGYO_SEIQTO_CD").ToString()
            Else
                'NULLの場合は親請求先コードをセット
                dr("SEIQTO_CD") = drOut.Item("OYA_SEIQTO_CD").ToString()

            End If

            dr("GOODS_CD_NRS") = drOut.Item("SAKI_GOODS_CD_NRS").ToString()

        Next

    End Sub

    ''' <summary>
    ''' 振替元作業の再セット
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub ResetDataSetSagyoOld(ByVal ds As DataSet)

        Dim max As Integer = ds.Tables("LMD010_SAGYO_OUTKA").Rows.Count - 1
        Dim dr As DataRow = Nothing
        Dim drOut As DataRow = Nothing
        drOut = ds.Tables("LMD010OUT").Rows(0)

        For i As Integer = 0 To max
            dr = ds.Tables("LMD010_SAGYO_OUTKA").Rows(i)

            '作業請求先コードが設定されているか判定
            If String.IsNullOrEmpty(drOut.Item("SAGYO_SEIQTO_CD").ToString()) = False Then
                'NULLじゃない場合は作業請求先コードをセット
                dr("SEIQTO_CD") = drOut.Item("SAGYO_SEIQTO_CD").ToString()
            Else
                'NULLの場合は親請求先コードをセット
                dr("SEIQTO_CD") = drOut.Item("OYA_SEIQTO_CD").ToString()
            End If

        Next

    End Sub

    ''' <summary>
    ''' 振替先在庫の再セット
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub ResetDataSetZaikoNew(ByVal ds As DataSet)

        Dim max As Integer = ds.Tables("LMD010_ZAI_NEW").Rows.Count - 1
        Dim drs As DataRow() = Nothing
        Dim dr As DataRow = Nothing
        Dim zaiRec As String = String.Empty
        Dim drOut As DataRow = ds.Tables("LMD010OUT").Rows(0)

        For i As Integer = 0 To max

            '対象行の振替先在庫レコードの取得
            dr = ds.Tables("LMD010_ZAI_NEW").Rows(i)

            '対象行の在庫レコード番号を取得
            zaiRec = dr.Item("ZAI_REC_NO").ToString()

            '対象行の在庫レコード番号より、振替元の在庫レコード番号に紐づくデータロウを取得
            drs = ds.Tables("LMD010_ZAI_OLDOUT").Select(String.Concat(" ZAI_REC_NO = ", "'", zaiRec, "'"))

            dr("RSV_NO") = drs(0).Item("RSV_NO").ToString()
            dr("HOKAN_YN") = drs(0).Item("HOKAN_YN").ToString()
            dr("SMPL_FLAG") = drs(0).Item("SMPL_FLAG").ToString()
            dr("GOODS_CD_NRS") = drOut.Item("SAKI_GOODS_CD_NRS").ToString()
            'START ADD 2013/09/10 KOBAYASHI WIT対応
            '振替元からKeyが変更されていない場合、商品管理番号を設定
            Dim count As Integer = getGoodsKanriKey(drs, dr)
            If (count > -1) Then
                dr("GOODS_KANRI_NO") = drs(count).Item("GOODS_KANRI_NO").ToString()
            End If
            'END   ADD 2013/09/10 KOBAYASHI WIT対応

        Next

    End Sub

    ''' <summary>
    ''' 振替元在庫の再セット
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub ResetDataSetZaikoOld(ByVal ds As DataSet)

        Dim max As Integer = ds.Tables("LMD010_ZAI_OLD").Rows.Count - 1
        Dim dr As DataRow = Nothing
        Dim drZaiOut As DataRow = Nothing

        For i As Integer = 0 To max

            '対象行の振替元在庫レコードの取得
            dr = ds.Tables("LMD010_ZAI_OLD").Rows(i)
            drZaiOut = ds.Tables("LMD010_ZAI_OLDOUT").Rows(i)

            If drZaiOut.Item("SMPL_FLAG").ToString().Equals("01") And Convert.ToDecimal(drZaiOut.Item("ALCTD_QT").ToString()) <> 0 Then
                '小分け実施フラグが1かつ引当可能数量が0以外の場合
                dr("PORA_ZAI_NB") = (Convert.ToInt32(drZaiOut.Item("PORA_ZAI_NB").ToString()) - Convert.ToInt32(dr.Item("PORA_ZAI_NB").ToString())).ToString()
                dr("ALLOC_CAN_NB") = (Convert.ToInt32(drZaiOut.Item("ALLOC_CAN_NB").ToString()) - Convert.ToInt32(dr.Item("ALLOC_CAN_NB").ToString())).ToString()
                dr("PORA_ZAI_QT") = (((Convert.ToDecimal(drZaiOut.Item("PORA_ZAI_QT").ToString()) * 1000) - (Convert.ToDecimal(dr.Item("PORA_ZAI_QT").ToString()) * 1000)) / 1000).ToString()
                dr("ALLOC_CAN_QT") = (((Convert.ToDecimal(drZaiOut.Item("ALLOC_CAN_QT").ToString()) * 1000) - (Convert.ToDecimal(dr.Item("ALLOC_CAN_QT").ToString()) * 1000)) / 1000).ToString()

            ElseIf drZaiOut.Item("SMPL_FLAG").ToString().Equals("00") Then
                '小分け実施フラグが0の場合
                dr("PORA_ZAI_NB") = (Convert.ToInt32(drZaiOut.Item("PORA_ZAI_NB").ToString()) - Convert.ToInt32(dr.Item("PORA_ZAI_NB").ToString())).ToString()
                dr("ALLOC_CAN_NB") = (Convert.ToInt32(drZaiOut.Item("ALLOC_CAN_NB").ToString()) - Convert.ToInt32(dr.Item("ALLOC_CAN_NB").ToString())).ToString()
                dr("PORA_ZAI_QT") = (((Convert.ToDecimal(drZaiOut.Item("PORA_ZAI_QT").ToString()) * 1000) - (Convert.ToDecimal(dr.Item("PORA_ZAI_QT").ToString()) * 1000)) / 1000).ToString()
                dr("ALLOC_CAN_QT") = (((Convert.ToDecimal(drZaiOut.Item("ALLOC_CAN_QT").ToString()) * 1000) - (Convert.ToDecimal(dr.Item("ALLOC_CAN_QT").ToString()) * 1000)) / 1000).ToString()

            End If

        Next


    End Sub

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

    'START ADD 2013/09/10 KOBAYASHI WIT対応
    ''' <summary>
    ''' 商品管理番号を引継ぐか否かを判定
    ''' </summary>
    ''' <param name="oldRow">振替元Row</param>
    ''' <param name="newRow">振替先Row</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function getGoodsKanriKey(ByVal oldRow As DataRow(), ByVal newRow As DataRow) As Integer

        ' 返却Key
        Dim counter As Integer = -1

        ' 判定
        For idx As Integer = 0 To oldRow.Length - 1
            If (newRow.Item("LOT_NO").Equals(oldRow(idx).Item("LOT_NO")) _
                And newRow.Item("CUST_CD_L").Equals(oldRow(idx).Item("CUST_CD_L")) _
                And newRow.Item("CUST_CD_M").Equals(oldRow(idx).Item("CUST_CD_M")) _
                And newRow.Item("GOODS_CD_NRS").Equals(oldRow(idx).Item("GOODS_CD_NRS")) _
                And newRow.Item("IRIME").Equals(oldRow(idx).Item("IRIME")) _
                And newRow.Item("SERIAL_NO").Equals(oldRow(idx).Item("SERIAL_NO"))) Then

                '同条件のレコードあり
                counter = idx

                Return counter

            End If

        Next

        Return counter

    End Function
    'E N D ADD 2013/09/10 KOBAYASHI WIT対応

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
    ''' INKA_NO_Lを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>InkaNoL</returns>
    ''' <remarks></remarks>
    Private Function GetInkaNoL(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.INKA_NO_L, Me, ds.Tables("LMD010IN").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' OUTKA_NO_Lを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>InkaNoL</returns>
    ''' <remarks></remarks>
    Private Function GetOUTKA_NO_L(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.OUTKA_NO_L, Me, ds.Tables("LMD010IN").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' ZAI_REC_NOを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>ZaiRecNo</returns>
    ''' <remarks></remarks>
    Private Function GetZaiRecNo(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.ZAI_REC_NO, Me, ds.Tables("LMD010IN").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' SAGYO_REC_NOを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>ZaiRecNo</returns>
    ''' <remarks></remarks>
    Private Function GetSagyoRecNo(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, ds.Tables("LMD010IN").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' FURI_NOを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>ZaiRecNo</returns>
    ''' <remarks></remarks>
    Private Function GetFuri_NO(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.FURI_CTL_NO, Me, ds.Tables("LMD010IN").Rows(0).Item("NRS_BR_CD").ToString())

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
