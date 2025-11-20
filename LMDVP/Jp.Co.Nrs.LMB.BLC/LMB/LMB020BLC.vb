' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷管理
'  プログラムID     :  LMB020    : 入荷データ編集
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMB020BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB020BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"

    '2015.10.20 tusnehira add
    '英語化対応
    Private Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Private Const MESSEGE_LANGUAGE_ENGLISH As String = "1"

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMB020DAC = New LMB020DAC()

    '前ゼロ
    Private Const MAEZERO As String = "000"

    ''' <summary>
    ''' 入荷（大）データセット名称
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_INAK_L As String = "LMB020_INKA_L"
    Private Const TABLE_NM_INAK_M As String = "LMB020_INKA_M"

    '印刷データセット（IN）
    Private Const TABLE_NM_IN_LMB500 As String = "LMB500IN"
    Private Const TABLE_NM_IN_LMB510 As String = "LMB510IN"
    Private Const TABLE_NM_IN_LMB520 As String = "LMB520IN"
    '2012/12/06入荷報告チェックリスト追加
    Private Const TABLE_NM_IN_LMB530 As String = "LMB530IN"
    Private Const TABLE_NM_IN_LMB540 As String = "LMB540IN"
    Private Const TABLE_NM_IN_LMB550 As String = "LMB550IN"
    Private Const TABLE_NM_IN_LMB560 As String = "LMB560IN"
    Private Const TABLE_NM_IN_LMB570 As String = "LMB570IN"

    '2017/09/25 修正 李↓
    ''20151106 tsunehira add
    ' ''' <summary>
    ' ''' 選択した言語を格納するフィールド
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _LangFlg As String = MessageManager.MessageLanguage
    '2017/09/25 修正 李↑

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 初期検索の値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInitData(ByVal ds As DataSet) As DataSet

        '入荷(大)のデータ取得
        ds = Me.DacAccess(ds, "SelectInkaLData")

        '入荷(中)のデータ取得
        ds = Me.DacAccess(ds, "SelectInkaMData")

        '入荷(小)のデータ取得
        ds = Me.DacAccess(ds, "SelectInkaSData")

        '運送(大)のデータ取得
        ds = Me.DacAccess(ds, "SelectUnsoLData")

        '作業のデータ取得
        ds = Me.DacAccess(ds, "SelectSagyoData")

        '在庫のデータ取得
        ds = Me.DacAccess(ds, "SelectZaikoData")

        '検品結果のデータ取得
        ds = Me.DacAccess(ds, "SelectInkaQrData")

        'Maxシーケンスのデータ取得
        ds = Me.SetMaxNo(ds)

        'ADD 2022/11/07 倉庫写真アプリ対応 START
        '入荷写真データ取得
        ds = Me.DacAccess(ds, "SelectInkaPhotoData")
        'ADD 2022/11/07 倉庫写真アプリ対応 END

        Return ds

    End Function

    Private Function SelectHokanNiyakuCalculation(ByVal ds As DataSet) As DataSet

        ' 保管・荷役料最終計算日 検索処理
        ds = Me.DacAccess(ds, "SelectHokanNiyakuCalculation")

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

    ''' <summary>
    ''' 最終請求日を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>請求日</returns>
    ''' <remarks>取得できない場合は"00000000"を返却</remarks>
    Private Function SelectGheaderData(ByVal ds As DataSet, ByVal actionId As String) As String

        ds = Me.DacAccess(ds, actionId)

        Dim dt As DataTable = ds.Tables("G_HED")
        If dt.Rows.Count < 1 Then

            Return "00000000"

        End If

        Return dt.Rows(0).Item("SKYU_DATE").ToString()

    End Function

    ''' <summary>
    ''' 荷主情報を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCustData(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, "SelectCustData")

    End Function

#If True Then   'ADD 2020/08/06 014005   【LMS】商品マスタ_入荷仮置場機能の追加
    ''' <summary>
    ''' 荷主明細情報を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataGoodsMeisaiOkiba(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, "SelectDataGoodsMeisaiOkiba")

    End Function
#End If

    ''' <summary>
    ''' タブレットデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function WHSagyoTorikomi(ByVal ds As DataSet) As DataSet

        '入荷ヘッダ取得
        ds = Me.DacAccess(ds, "SelectTabHeadData")
        If ds.Tables("LMB020_TAB_HEAD").Rows.Count = 0 Then
            Me.SetMessage("E656")
            Return ds
        End If
        '入荷明細取得
        ds = Me.DacAccess(ds, "SelectTabDtlData")
        If ds.Tables("LMB020_TAB_DTL").Rows.Count = 0 Then
            Me.SetMessage("E656")
            Return ds
        End If

        Return ds

    End Function

#End Region

#Region "設定処理"

#Region "新規登録"

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        '採番
        Dim inkaNoL As String = Me.GetInkaNoL(ds)
        Dim value As String() = New String() {inkaNoL}
        Dim colNm As String() = New String() {"INKA_NO_L"}

        '入荷(大)に採番した値を設定
        ds = Me.SetValueData(ds, "LMB020_INKA_L", colNm, value)

        '入荷(中)に採番した値を設定
        ds = Me.SetValueData(ds, "LMB020_INKA_M", colNm, value)

        '入荷(小)に採番した値を設定
        ds = Me.SetValueData(ds, "LMB020_INKA_S", colNm, value)

        '2014/08/27 Ri [入荷番号大が反映されない] Add
        'シリアル検品WKに採番した値を設定(入荷大のみでOK、他は不要)
        ds = Me.SetValueData(ds, "LMB020_KENPIN_WK_DATA", colNm, value)

        'ADD 2022/11/07 倉庫写真アプリ対応 START
        '入荷写真データに採番した値を設定
        ds = Me.SetValueData(ds, "LMB020_INKA_PHOTO", colNm, value)
        'ADD 2022/11/07 倉庫写真アプリ対応 END

        '在庫に採番した値を設定
        ds = Me.SetZaiRecNo(ds, inkaNoL)

        '新規登録
        Call Me.InsertData(ds, inkaNoL)

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet, ByVal inkaNoL As String) As Boolean

        Dim inkaLDr As DataRow = ds.Tables("LMB020_INKA_L").Rows(0)
        Dim chkDate As String = inkaLDr.Item("INKA_DATE").ToString()

        Dim msg As String = "保存"

        '荷主情報取得
        ds = Me.SelectCustData(ds)

        '完了済チェック
        Dim rtnResult As Boolean = Me.ChkDateHokanNiyaku(ds, msg)

        '請求日チェック(作業料)
        rtnResult = rtnResult AndAlso Me.ChkSeiqDateSagyo(ds, chkDate, msg)

        '入荷(大)の新規登録
        rtnResult = rtnResult AndAlso Me.InsertInkaLData(ds)

        '入荷(中)の新規登録
        rtnResult = rtnResult AndAlso Me.SetInkaMData(ds)

        '入荷(小)の新規登録
        rtnResult = rtnResult AndAlso Me.SetInkaSData(ds)

        '在庫の新規登録
        rtnResult = rtnResult AndAlso Me.SetZaiTrsData(ds)

        '運送項目の更新
        rtnResult = rtnResult AndAlso Me.SetUnsoData(ds, inkaNoL)

        '作業の更新
        rtnResult = rtnResult AndAlso Me.SetSagyoData(ds, inkaNoL)

        '2014.07.30 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ST-
        'ｱｸﾞﾘﾏｰﾄ対応(入荷管理番号紐づけ処理)
        rtnResult = rtnResult AndAlso Me.UpdateInkaKenpinWkData(ds)
        '2014.07.30 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ED-

        'ADD 2022/11/07 倉庫写真アプリ対応 START
        '入荷写真データの新規登録
        rtnResult = rtnResult AndAlso Me.SetInkaPhotoData(ds)
        'ADD 2022/11/07 倉庫写真アプリ対応 END

        Return rtnResult

    End Function

#End Region

#Region "更新登録"

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveAction(ByVal ds As DataSet) As DataSet

        '在庫に採番した値を設定
        ds = Me.SetZaiRecNo(ds, ds.Tables("LMB020_INKA_L").Rows(0).Item("INKA_NO_L").ToString())

        '更新処理
        Call Me.UpdateData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As Boolean

        Dim inkaLDr As DataRow = ds.Tables("LMB020_INKA_L").Rows(0)
        Dim chkDate As String = inkaLDr.Item("INKA_DATE").ToString()
        Dim msg As String = "保存"

        '荷主情報取得
        ds = Me.SelectCustData(ds)

        '完了済チェック
        Dim rtnResult As Boolean = Me.ChkDateHokanNiyaku(ds, msg)

        '請求日チェック(作業料)
        rtnResult = rtnResult AndAlso Me.ChkSeiqDateSagyo(ds, chkDate, msg)

        '入荷(大)の更新登録
        rtnResult = rtnResult AndAlso Me.UpdateInkaLData(ds)

        'あえて順番を変更
        '在庫の更新登録
        rtnResult = rtnResult AndAlso Me.SetZaiTrsData(ds)

        '入荷(中)の更新登録
        rtnResult = rtnResult AndAlso Me.SetInkaMData(ds)

        '入荷(小)の更新登録
        rtnResult = rtnResult AndAlso Me.SetInkaSData(ds)

        'START ADD 2013/09/10 KURIHARA WIT対応
        '入荷WKの更新処理

        'ハンディ対象荷主チェック
        If IsHandyTargetCust(ds) Then
            rtnResult = rtnResult AndAlso Me.SetInkaWkData(ds)
        End If
        'END   ADD 2013/09/10 KURIHARA WIT対応

        '運送項目の更新
        rtnResult = rtnResult AndAlso Me.SetUnsoData(ds, ds.Tables("LMB020_INKA_L").Rows(0).Item("INKA_NO_L").ToString())

        '作業の更新
        rtnResult = rtnResult AndAlso Me.SetSagyoData(ds, String.Empty)

        '2014.07.30 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ST-
        'ｱｸﾞﾘﾏｰﾄ対応(入荷管理番号紐づけ処理)
        rtnResult = rtnResult AndAlso Me.UpdateInkaKenpinWkData(ds)
        '2014.07.30 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ED-

        'ADD 2022/11/07 倉庫写真アプリ対応 START
        '入荷写真データの更新登録
        rtnResult = rtnResult AndAlso Me.SetInkaPhotoData(ds)
        'ADD 2022/11/07 倉庫写真アプリ対応 END

        ' 入荷実績(連番QR)更新処理
        rtnResult = rtnResult AndAlso Me.UpdateInkaQrData(ds)

        'タブレット画像登録処理
        rtnResult = rtnResult AndAlso Me.SetTabletImageData(ds)

        'DataSet作成
        ds = SetInDataSet(ds)

        '入荷(小)のデータ取得
        ds = Me.DacAccess(ds, "SelectInkaSData")


        Return rtnResult

    End Function

    ''' <summary>
    ''' 更新登録(起算日修正)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveDateAction(ByVal ds As DataSet) As DataSet

        Dim msg As String = "保存"

        '完了済チェック
        Dim rtnResult As Boolean = Me.ChkDateHokanNiyaku(ds, msg)

        '更新処理
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateSaveDateAction")

        Return ds

    End Function

    ''' <summary>
    ''' 更新登録(運送修正)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveUnsoAction(ByVal ds As DataSet) As DataSet

        '入荷にある運送系項目を更新する用
        Dim rtnResult As Boolean = Me.UpdateInkaLData(ds)

        '運送系の更新
        rtnResult = rtnResult AndAlso Me.SetUnsoData(ds, ds.Tables("LMB020_INKA_L").Rows(0).Item("INKA_NO_L").ToString())

        Return ds

    End Function

#End Region

#Region "削除登録"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteAction(ByVal ds As DataSet) As DataSet

        '更新処理
        Call Me.DeleteData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As Boolean

        Dim inkaLDr As DataRow = ds.Tables("LMB020_INKA_L").Rows(0)
        Dim chkDate As String = inkaLDr.Item("INKA_DATE").ToString()
        Dim msg As String = "削除"

        '荷主情報取得
        ds = Me.SelectCustData(ds)

        '完了済チェック
        Dim rtnResult As Boolean = Me.ChkDateHokanNiyaku(ds, msg)

        '請求日チェック(作業料)
        rtnResult = rtnResult AndAlso Me.ChkSeiqDateSagyo(ds, ChkDate, msg)

        '請求日チェック(運賃)
        rtnResult = rtnResult AndAlso Me.ChkSeiqDateUnchin(ds, chkDate, msg, True)

        '入荷(大)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateInkaLDelFlg")

        '入荷(中)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateInkaMSysDelFlg")

        '入荷(小)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateInkaSSysDelFlg")

        '在庫の論理削除
        rtnResult = rtnResult AndAlso Me.DeleteZaiData(ds)

        '運送情報の削除処理
        rtnResult = rtnResult AndAlso Me.DeleteUnsoData(ds)

        '作業の物理削除
        rtnResult = rtnResult AndAlso Me.DeleteSagyoData(ds)

        '出荷の論理削除
        rtnResult = rtnResult AndAlso Me.DeleteOutKaData(ds)

        '2014.04.24 CALT対応 入荷大削除時キャンセルデータ作成 黎 追加 --ST--
        rtnResult = rtnResult AndAlso Me.DelInkaPlanSendData(ds)
        '2014.04.24 CALT対応 入荷大削除時キャンセルデータ作成 黎 追加 --ED--

        'ADD 2022/11/07 倉庫写真アプリ対応 START
        '入荷写真データの論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateInkaPhotoSysDelFlg")
        'ADD 2022/11/07 倉庫写真アプリ対応 END

        '入荷WKテーブル更新処理(INKA_TORI_FLGリセット)
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateInkaKenpinWkToriFlgInkaL")    'ADD 2019/12/02 006350

        Return rtnResult

    End Function

    ''' <summary>
    ''' 運送情報の削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteUnsoData(ByVal ds As DataSet) As Boolean

        'レコードがない場合、スルー
        Dim unsoLDt As DataTable = ds.Tables("LMB020_UNSO_L")
        If unsoLDt.Rows.Count < 1 Then
            Return True
        End If

        '運送番号(大)がない場合、スルー
        If String.IsNullOrEmpty(unsoLDt.Rows(0).Item("UNSO_NO_L").ToString()) = True Then
            Return True
        End If

        'あえて順番を変えている
        '運送(中)の物理削除
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "DeleteUnsoMData")

        '運賃の物理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteUnchinData")

        '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
        '支払の物理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteShiharaiData")
        '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

        '運送(大)の物理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteUnsoLData")

        Return rtnResult

    End Function

    ''' <summary>
    ''' 在庫の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteZaiData(ByVal ds As DataSet) As Boolean

        ds = Me.SetDeleteData(ds, "LMB020_ZAI")

        Return Me.ServerChkJudge(ds, "UpdateZaiTrsData")

    End Function

    ''' <summary>
    ''' 作業の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteSagyoData(ByVal ds As DataSet) As Boolean

        ds = Me.SetDeleteData(ds, "LMB020_SAGYO")

        Return Me.ServerChkJudge(ds, "UpdateSagyoSysData")

    End Function

    ''' <summary>
    ''' 出荷情報の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteOutKaData(ByVal ds As DataSet) As Boolean

        '振替番号に値がない場合、スルー
        If String.IsNullOrEmpty(ds.Tables("LMB020_INKA_L").Rows(0).Item("FURI_NO").ToString()) = True Then
            Return True
        End If

        '出荷(大)の論理削除
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "DeleteOutKaL")

        '出荷(中)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteOutKaM")

        '出荷(小)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteOutKaS")

        Return rtnResult

    End Function

    ''' <summary>
    ''' 削除フラグを設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNm">DataTable名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDeleteData(ByVal ds As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = ds.Tables(tblNm)
        Dim max As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To max
            dt.Rows(i).Item("SYS_DEL_FLG") = LMConst.FLG.ON
        Next

        Return ds

    End Function

    '2014.04.24 CALT対応 入荷大削除時キャンセルデータ作成 黎 追加 --ST--
    ''' <summary>
    ''' B_INKA_PLAN_SEND キャンセルデータ作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DelInkaPlanSendData(ByVal ds As DataSet) As Boolean

        '--外部定義変数
        '***カウンター
        'LMB010IN_INKA_PLAN_SENDテーブルのRowカウンタ
        Dim iIPlanSendCnt As Integer = 0
        Dim Cnt As Integer = 0

        '****フラグ系
        '結果フラグ
        Dim bResult As Boolean = True

        '***データセット系
        'IN用データセット
        Dim dsI As DataSet = ds.Copy()
        Dim dtIPlanSend As DataTable = dsI.Tables("LMB020_INKA_L")
        '作業データセット
        Dim dsTmp As DataSet = ds.Clone()
        Dim drTmp As DataRow = Nothing
        '==========================================================

        'INデータ分だけループ(2014.04.24の組込時点ではループは一回のみ)
        iIPlanSendCnt = dtIPlanSend.Rows.Count
        For i As Integer = 0 To iIPlanSendCnt - 1

            Dim drIPlanSend As DataRow = Nothing
            drIPlanSend = dtIPlanSend.Rows(i)

            '--倉庫チェック
            MyBase.CallDAC(Me._Dac, "ChkWhCd", dsI)
            If MyBase.GetResultCount() < 1 Then
                Exit For
            End If

            '==========================================================

            '--キャンセルデータの抽出
            Dim dsC As DataSet = dsI.Copy
            dsC = MyBase.CallDAC(Me._Dac, "SelectSendCancel", dsC)

            '--DACアクセス(INSERT) --キャンセル報告データ作成--
            If MyBase.GetResultCount() > 0 Then

                '--パラメータの編集(必要があれば当Function内に記述
                Call Me.EditData(dsC)

                Cnt = 0
                Cnt = dsC.Tables("LMB020_INKA_DEL_OUT").Rows.Count
                For j As Integer = 0 To Cnt - 1
                    dsTmp.Tables("LMB020_INKA_DEL_OUT").Clear()

                    drTmp = dsC.Tables("LMB020_INKA_DEL_OUT").Rows(j)

                    dsTmp.Tables("LMB020_INKA_DEL_OUT").ImportRow(drTmp)

                    MyBase.CallDAC(Me._Dac, "InsertSendInkaData", dsTmp)
                Next

            End If

        Next

        Return bResult

    End Function

    ''' <summary>
    ''' カラム編集
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub EditData(ByRef ds As DataSet)
        Dim dsEdit As DataSet = ds.Copy()
        Dim dtEditSend As DataTable = dsEdit.Tables("LMB020_INKA_DEL_OUT")

        Dim iSeq As Integer = 0


        For Each editRow As DataRow In dtEditSend.Rows
            iSeq = Convert.ToInt32(editRow.Item("SEND_SEQ"))
            editRow.Item("SEND_SEQ") = Right(String.Concat("000", iSeq), 3)
            '=================
        Next

        ds.Clear()

        ds = dsEdit
    End Sub
    '2014.04.24 CALT対応 入荷大削除時キャンセルデータ作成 黎 追加 --ED--

#End Region

#Region "共通更新"

    ''' <summary>
    ''' 入荷(大)新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertInkaLData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertInkaLData")

    End Function

    ''' <summary>
    ''' 入荷(大)更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaLData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateInkaLData")

    End Function

    ''' <summary>
    ''' 入荷(大)削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteInkaLData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateInkaLDelFlg")

    End Function

    ''' <summary>
    ''' 入荷(中)更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetInkaMData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateInkaMData")

    End Function

    ''' <summary>
    ''' 入荷(中)削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaMDelFlg(ByVal ds As DataSet) As Boolean
        Return Me.ServerChkJudge(ds, "UpdateInkaMDelFlg")
    End Function

    ''' <summary>
    ''' 入荷(小)更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetInkaSData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateInkaSData")

    End Function

    ''' <summary>
    ''' 入荷(小)削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaSDelFlg(ByVal ds As DataSet) As Boolean
        Return Me.ServerChkJudge(ds, "UpdateInkaSDelFlg")
    End Function

    'START ADD 2013/09/10 KURIHARA WIT対応
    ''' <summary>
    ''' 入荷WK更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetInkaWkData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateInkaWkData")

    End Function
    'END   ADD 2013/09/10 KURIHARA WIT対応

    ''' <summary>
    ''' 在庫更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetZaiTrsData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateZaiTrsData")

    End Function

    ''' <summary>
    ''' 在庫削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateZaiTrsDelFlg(ByVal ds As DataSet) As Boolean
        Return Me.ServerChkJudge(ds, "UpdateZaiTrsDelFlg")
    End Function

    ''' <summary>
    ''' 運送系の項目更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inkaNol">入荷(大)番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetUnsoData(ByVal ds As DataSet, ByVal inkaNol As String) As Boolean

        '運送(大)番号
        Dim unsoNoL As String = String.Empty

        '削除フラグ = '1'の場合、設定しない

        '運送(大)の削除フラグ = '1'の場合、スルー
        If LMConst.FLG.ON.Equals(ds.Tables("LMB020_UNSO_L").Rows(0).Item("SYS_DEL_FLG")) = False Then

            unsoNoL = Me.GetUnsoNoL(ds)
            Dim colNm As String() = New String() {"UNSO_NO_L"}
            Dim value As String() = New String() {unsoNoL}

            '運送番号の設定
            ds = Me.SetValueData(ds, "LMB020_UNSO_M", colNm, value)
            ReDim Preserve colNm(1)
            ReDim Preserve value(1)
            colNm(1) = "INOUTKA_NO_L"
            value(1) = inkaNol
            ds = Me.SetValueData(ds, "LMB020_UNSO_L", colNm, value)

        End If

        '運送(中)の更新
        Dim rtnResult As Boolean = Me.SetUnsoMData(ds)

        '運賃の削除 
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteUnchinData")

        '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
        '支払の削除
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
        'rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteShiharaiData")
        '運行紐付いていない場合のみ、支払を削除する
        Dim dt As DataTable = ds.Tables("LMB020_UNSO_L")
        Dim dr As DataRow = dt.Rows(0)
        If String.IsNullOrEmpty(dr.Item("TRIP_NO").ToString()) = True AndAlso _
           String.IsNullOrEmpty(dr.Item("TRIP_NO_SYUKA").ToString()) = True AndAlso _
           String.IsNullOrEmpty(dr.Item("TRIP_NO_TYUKEI").ToString()) = True AndAlso _
           String.IsNullOrEmpty(dr.Item("TRIP_NO_HAIKA").ToString()) = True Then
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteShiharaiData")
        End If
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  
        '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

        '運送(大)の更新
        rtnResult = rtnResult AndAlso Me.SetUnsoLData(ds)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 運送(大)の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetUnsoLData(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables("LMB020_UNSO_L")
        If dt.Rows.Count < 1 Then
            Return True
        End If

        Dim upKbnChk As Boolean = LMConst.FLG.OFF.Equals(dt.Rows(0).Item("UP_KBN").ToString())
        Dim delChk As Boolean = LMConst.FLG.ON.Equals(dt.Rows(0).Item("SYS_DEL_FLG").ToString())

        '新規 且つ 削除フラグ = 1はスルー
        If upKbnChk = True AndAlso delChk = True Then
            Return True
        End If

        '削除フラグ = 1は物理削除
        If delChk = True Then
            Return Me.ServerChkJudge(ds, "DeleteUnsoLData")
        End If

        '新規の場合、レコード追加
        If upKbnChk = True Then
            Return Me.ServerChkJudge(ds, "InsertUnsoLData")
        End If

        '更新の場合
        Return Me.ServerChkJudge(ds, "UpdateUnsoLData")

    End Function

    ''' <summary>
    ''' 運送(中)の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetUnsoMData(ByVal ds As DataSet) As Boolean

        'Delete & Insert
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "DeleteUnsoMData")

        '更新の場合
        Return rtnResult AndAlso Me.ServerChkJudge(ds, "InsertUnsoMData")

    End Function

    ''' <summary>
    ''' 運賃の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetUnchinData(ByVal ds As DataSet) As DataSet

        '請求日チェック
        Dim rtnResult As Boolean = Me.ChkSeiqDateUnchin(ds, ds.Tables("LMB020_INKA_L").Rows(0).Item("INKA_DATE").ToString(), "保存", False)

        'Insert
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "InsertUnchinData")

        Return ds

    End Function

    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
    ''' <summary>
    ''' 支払の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetShiharaiData(ByVal ds As DataSet) As DataSet

        '支払日チェック
        'Dim rtnResult As Boolean = Me.ChkShiharaiDate(ds, ds.Tables("LMB020_INKA_L").Rows(0).Item("INKA_DATE").ToString(), "保存", False)
        Dim rtnResult As Boolean = True

        'Insert
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "InsertShiharaiData")

        Return ds

    End Function
    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

    ''' <summary>
    ''' 作業レコードの更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inkaNoL">入荷(大)番号：更新処理の場合、空文字を渡す</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetSagyoData(ByVal ds As DataSet, ByVal inkaNoL As String) As Boolean

        '作業データの値設定
        ds = Me.SetSagyoToInkaNoData(ds, inkaNoL)

        '更新処理
        If String.IsNullOrEmpty(inkaNoL) = True Then
            Return Me.ServerChkJudge(ds, "UpdateSagyoSysData")
        End If

        '新規登録
        Return Me.ServerChkJudge(ds, "InsertSagyoData")

    End Function

    ''' <summary>
    ''' 作業Noの設定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inkaNoL">入荷(大)番号：更新処理の場合、空文字を渡す</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetSagyoToInkaNoData(ByVal ds As DataSet, ByVal inkaNoL As String) As DataSet

        Dim dt As DataTable = ds.Tables("LMB020_SAGYO")
        Dim max As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To max

            'PKがない場合、設定
            If String.IsNullOrEmpty(dt.Rows(i).Item("SAGYO_REC_NO").ToString()) = True Then
                dt.Rows(i).Item("SAGYO_REC_NO") = Me.GetSagyoRecNo(ds)
            End If

            '入荷大番号が空の場合スルー(更新処理)
            If String.IsNullOrEmpty(inkaNoL) = True Then
                Continue For
            End If

            '入出荷管理番号L + Mの設定
            dt.Rows(i).Item("INOUTKA_NO_LM") = String.Concat(inkaNoL, dt.Rows(i).Item("INOUTKA_NO_LM").ToString())

        Next

        Return ds

    End Function

    '2014.07.30 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ST-
    ''' <summary>
    ''' 入荷検品WK更新処理(ワークデータセットがあるときのみ実行される)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaKenpinWkData(ByVal ds As DataSet) As Boolean

        'DEL 2019/12/02 006350    Return Me.ServerChkJudge(ds, "UpdateInkaKenpinWkData")
        'ADD S 2019/12/02 006350
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "UpdateInkaKenpinWkData")

        '入荷WKテーブル更新処理(INKA_TORI_FLGリセット)
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateInkaKenpinWkToriFlg")

        Return rtnResult
        'ADD E 2019/12/02 006350

    End Function
    '2014.07.30 Ri [ｱｸﾞﾘﾏｰﾄ対応] Add -ED-



    ''' <summary>
    ''' 入荷実績(日陸連番QR)更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaQrData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateInkaQr")

    End Function


    ''' <summary>
    ''' タブレット画像取込処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetTabletImageData(ByVal ds As DataSet) As Boolean

        '取込フラグオフの場合は処理しない
        If Not LMConst.FLG.ON.Equals(ds.Tables("LMB020_INKA_L").Rows(0).Item("WH_TAB_IMP_PROC_FLG").ToString) Then
            Return True
        End If

        '画像DEL&INS用データテーブル作成
        Dim dt As DataTable = ds.Tables("LMB020_FILE")
        Dim dr As DataRow = dt.NewRow
        dr.Item("NRS_BR_CD") = ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString
        dr.Item("FILE_TYPE") = "L3"
        dr.Item("KEY_TYPE") = "44"
        dr.Item("CONTROL_NO_L") = ds.Tables("LMB020_INKA_L").Rows(0).Item("INKA_NO_L").ToString
        'dr.Item("CONTROL_SEQ") = ""
        dt.Rows.Add(dr)

        'Delete
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "DeleteMFileData")

        'Insert
        Return rtnResult AndAlso Me.ServerChkJudge(ds, "InsertMFileData")

    End Function

    ''' <summary>
    ''' 検索用DataSet作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetInDataSet(ByVal ds As DataSet) As DataSet

        '検索用DataSet作成
        Dim dt As DataTable = ds.Tables("LMB020IN")
        Dim dr As DataRow = dt.NewRow
        dr.Item("NRS_BR_CD") = ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString
        dr.Item("INKA_NO_L") = ds.Tables("LMB020_INKA_L").Rows(0).Item("INKA_NO_L").ToString
        dt.Rows.Add(dr)

        Return ds

    End Function

    'ADD 2022/11/07 倉庫写真アプリ対応 START
    ''' <summary>
    ''' 入荷写真データ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetInkaPhotoData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateInkaPhotoData")

    End Function
    'ADD 2022/11/07 倉庫写真アプリ対応 END

#End Region

#End Region

#Region "印刷処理"

    Private Function GetSokoData(ByVal ds As DataSet) As DataSet

        '倉庫データ取得
        Return MyBase.CallDAC(Me._Dac, "SelectSokoData", ds)

    End Function

    ''' <summary>
    ''' 入荷(大)テーブル更新(印刷)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaLPrintData(ByVal ds As DataSet) As DataSet

        '入荷(大)の論理削除
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "UpdateInkaLPrintData")

        Return ds

    End Function

    ''' <summary>
    ''' 入庫報告のインパラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB520InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DSL.LMB520DS = New DSL.LMB520DS
        Dim dt As DataTable = inDs.Tables(LMB020BLC.TABLE_NM_IN_LMB520)
        Dim dr As DataRow = dt.NewRow()
        With ds.Tables(LMB020BLC.TABLE_NM_INAK_L).Rows(0)

            dr.Item("NRS_BR_CD") = .Item("NRS_BR_CD").ToString()
            dr.Item("INKA_NO_L") = .Item("INKA_NO_L").ToString()
            dr.Item("PGID") = MyBase.GetPGID
            dr.Item("KAKUIN_FLG") = "0".ToString            'ADD 2018/11/01 依頼番号 : 002747   【LMS】入荷報告印刷_角印つけるつけないの選択機能

        End With
        dt.Rows.Add(dr)

        Return inDs

    End Function

#If True Then       'ADD 2018/11/01 依頼番号 : 002747   【LMS】入荷報告印刷_角印つけるつけないの選択機能
    ''' <summary>
    ''' 入庫報告のインパラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB520InDataKakuin(ByVal ds As DataSet) As DataSet

        Dim inDs As DSL.LMB520DS = New DSL.LMB520DS
        Dim dt As DataTable = inDs.Tables(LMB020BLC.TABLE_NM_IN_LMB520)
        Dim dr As DataRow = dt.NewRow()
        With ds.Tables(LMB020BLC.TABLE_NM_INAK_L).Rows(0)

            dr.Item("NRS_BR_CD") = .Item("NRS_BR_CD").ToString()
            dr.Item("INKA_NO_L") = .Item("INKA_NO_L").ToString()
            dr.Item("PGID") = MyBase.GetPGID
            dr.Item("KAKUIN_FLG") = "1".ToString

        End With
        dt.Rows.Add(dr)

        Return inDs

    End Function
#End If

    ''' <summary>
    ''' 入荷チェックリストのインパラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB510InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DSL.LMB510DS = New DSL.LMB510DS
        Dim dt As DataTable = inDs.Tables(LMB020BLC.TABLE_NM_IN_LMB510)
        Dim dr As DataRow = dt.NewRow()
        With ds.Tables(LMB020BLC.TABLE_NM_INAK_L).Rows(0)

            dr.Item("NRS_BR_CD") = .Item("NRS_BR_CD").ToString()
            dr.Item("INKA_NO_L") = .Item("INKA_NO_L").ToString()
            dr.Item("USER_CD") = .Item("TANTO_USER").ToString()

        End With
        dt.Rows.Add(dr)

        Return inDs

    End Function

    ''' <summary>
    ''' 入荷受付表のインパラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB500InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DSL.LMB500DS = New DSL.LMB500DS
        Dim dt As DataTable = inDs.Tables(LMB020BLC.TABLE_NM_IN_LMB500)
        Dim dr As DataRow = dt.NewRow()
        With ds.Tables(LMB020BLC.TABLE_NM_INAK_L).Rows(0)

            dr.Item("NRS_BR_CD") = .Item("NRS_BR_CD").ToString()
            dr.Item("INKA_NO_L") = .Item("INKA_NO_L").ToString()
            dr.Item("USER_CD") = .Item("TANTO_USER").ToString()

        End With
        dt.Rows.Add(dr)

        Return inDs

    End Function

    '2012/12/06入荷報告チェックリスト
    Private Function SetDataSetLMB530InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DSL.LMB530DS = New DSL.LMB530DS
        Dim dt As DataTable = inDs.Tables(LMB020BLC.TABLE_NM_IN_LMB530)
        Dim dr As DataRow = dt.NewRow()
        With ds.Tables(LMB020BLC.TABLE_NM_INAK_L).Rows(0)

            dr.Item("NRS_BR_CD") = .Item("NRS_BR_CD").ToString()
            dr.Item("INKA_NO_L") = .Item("INKA_NO_L").ToString()
            dr.Item("PGID") = MyBase.GetPGID

        End With
        dt.Rows.Add(dr)

        Return inDs

    End Function

    '2012/12/06入荷確定入力モニター表
    Private Function SetDataSetLMB540InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DSL.LMB540DS = New DSL.LMB540DS
        Dim dt As DataTable = inDs.Tables(LMB020BLC.TABLE_NM_IN_LMB540)
        Dim dr As DataRow = dt.NewRow()
        With ds.Tables(LMB020BLC.TABLE_NM_INAK_L).Rows(0)

            dr.Item("NRS_BR_CD") = .Item("NRS_BR_CD").ToString()
            dr.Item("INKA_NO_L") = .Item("INKA_NO_L").ToString()
            dr.Item("PGID") = MyBase.GetPGID

        End With
        dt.Rows.Add(dr)

        Return inDs

    End Function

    ''' <summary>
    ''' 入庫連絡票のインパラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB550InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DSL.LMB550DS = New DSL.LMB550DS
        Dim dt As DataTable = inDs.Tables(LMB020BLC.TABLE_NM_IN_LMB550)
        Dim dr As DataRow = dt.NewRow()
        With ds.Tables(LMB020BLC.TABLE_NM_INAK_L).Rows(0)

            dr.Item("NRS_BR_CD") = .Item("NRS_BR_CD").ToString()
            dr.Item("INKA_NO_L") = .Item("INKA_NO_L").ToString()
            dr.Item("USER_CD") = .Item("TANTO_USER").ToString()

        End With
        dt.Rows.Add(dr)

        Return inDs

    End Function

#If True Then   'ADD 2022/01/26 026543 【LMS】運送保険料システム化_実装_運送保険申込書対応_入荷機能新規作成
    ''' <summary>
    ''' 運送保険申込書のインパラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB560InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DSL.LMB560DS = New DSL.LMB560DS
        Dim dt As DataTable = inDs.Tables(LMB020BLC.TABLE_NM_IN_LMB560)
        Dim dr As DataRow = dt.NewRow()
        With ds.Tables(LMB020BLC.TABLE_NM_INAK_L).Rows(0)

            dr.Item("NRS_BR_CD") = .Item("NRS_BR_CD").ToString()
            dr.Item("INKA_NO_L") = .Item("INKA_NO_L").ToString()

            '運送保険指定時
            dr.Item("UNSO_TEHAI_CHK") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        Return inDs

    End Function
#End If

    ''' <summary>
    ''' コンテナ番号ラベルのインパラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB570InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DSL.LMB570DS = New DSL.LMB570DS
        Dim dt As DataTable = inDs.Tables(LMB020BLC.TABLE_NM_IN_LMB570)
        Dim dr As DataRow

        For i As Integer = 0 To ds.Tables(LMB020BLC.TABLE_NM_INAK_M).Rows.Count() - 1

            dr = dt.NewRow()

            With ds.Tables(LMB020BLC.TABLE_NM_INAK_M).Rows(i)

                dr.Item("NRS_BR_CD") = .Item("NRS_BR_CD").ToString()
                dr.Item("INKA_NO_L") = .Item("INKA_NO_L").ToString()
                dr.Item("INKA_NO_M") = .Item("INKA_NO_M").ToString()

            End With

            dt.Rows.Add(dr)
        Next

        Return inDs

    End Function

#End Region

#Region "チェック"

    ''' <summary>
    ''' 印刷処理時の排他チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintChk(ByVal ds As DataSet) As DataSet

        '排他検索[入荷(大)]
        Dim rtnResult As Boolean = Me.SelectInkaLSysDateTime(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 編集処理のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function EditChk(ByVal ds As DataSet) As DataSet

        Dim inkaLDr As DataRow = ds.Tables("LMB020_INKA_L").Rows(0)
        Dim chkDate As String = inkaLDr.Item("INKA_DATE").ToString()
        Dim msg As String = "編集"

        '荷主情報取得
        ds = Me.SelectCustData(ds)

        '完了済チェック
        Dim rtnResult As Boolean = Me.ChkDateHokanNiyaku(ds, msg)

        '請求日チェック(作業料)
        rtnResult = rtnResult AndAlso Me.ChkSeiqDateSagyo(ds, chkDate, msg)

        '請求日チェック(運賃)
        rtnResult = rtnResult AndAlso Me.ChkSeiqDateUnchin(ds, chkDate, msg, True)

        '排他検索[入荷(大)]
        rtnResult = rtnResult AndAlso Me.SelectInkaLSysDateTime(ds)

        '排他検索[出荷(大)]
        rtnResult = rtnResult AndAlso Me.SelectOutkaLSysDateTime(ds)

        '排他検索[運送(大)]
        rtnResult = rtnResult AndAlso Me.SelectUnsoLSysDateTime(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 起算日修正処理のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DateEditChk(ByVal ds As DataSet) As DataSet

        Dim msg As String = "起算日修正"

        '完了済チェック
        Dim rtnResult As Boolean = Me.ChkDateHokanNiyaku(ds, msg)

        '排他検索[入荷(大)]
        rtnResult = rtnResult AndAlso Me.SelectInkaLSysDateTime(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 運送修正処理のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UnsoEditChk(ByVal ds As DataSet) As DataSet

        Dim chkDate As String = ds.Tables("LMB020_INKA_L").Rows(0).Item("INKA_DATE").ToString()
        Dim msg As String = "運送修正"

        '請求日チェック(運賃)
        Dim rtnResult As Boolean = Me.ChkSeiqDateUnchin(ds, chkDate, msg, True)

        '排他検索[運送(大)]
        rtnResult = rtnResult AndAlso Me.SelectUnsoLSysDateTime(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 入荷(大)の排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInkaLSysDateTime(ByVal ds As DataSet) As Boolean

        '排他検索[入荷(大)]
        Return Me.ServerChkJudge(ds, "SelectInkaLSysDateTime")

    End Function

    ''' <summary>
    ''' 出荷(大)の排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectOutkaLSysDateTime(ByVal ds As DataSet) As Boolean

        '値がない場合、スルー
        If String.IsNullOrEmpty(ds.Tables("LMB020_INKA_L").Rows(0).Item("FURI_NO").ToString()) = True Then
            Return True
        End If

        '排他検索[入荷(大)]
        Return Me.ServerChkJudge(ds, "SelectOutkaLSysDateTime")

    End Function

    ''' <summary>
    ''' 運送(大)の排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUnsoLSysDateTime(ByVal ds As DataSet) As Boolean

        'レコードがない場合、スルー
        Dim unsoLDt As DataTable = ds.Tables("LMB020_UNSO_L")
        If unsoLDt.Rows.Count < 1 Then
            Return True
        End If

        '運送管理番号(大)がない場合、スルー
        If String.IsNullOrEmpty(unsoLDt.Rows(0).Item("UNSO_NO_L").ToString()) = True Then
            Return True
        End If

        '排他検索[入荷(大)]
        Return Me.ServerChkJudge(ds, "SelectUnsoLSysDateTime")

    End Function

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

    ''' <summary>
    ''' 完了済チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkDateHokanNiyaku(ByVal ds As DataSet, ByVal msg As String) As Boolean
        Return Me.ChkDate2(ds, msg)
    End Function

    ''' <summary>
    ''' 請求日チェック(作業料)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="value">入荷日</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkSeiqDateSagyo(ByVal ds As DataSet, ByVal value As String, ByVal msg As String) As Boolean

        '作業レコードがない場合、スルー
        Dim drs As DataRow() = ds.Tables("LMB020_SAGYO").Select("SYS_DEL_FLG = '0' AND UP_KBN <> '2' ")
        Dim max As Integer = drs.Length - 1
        If max < 0 Then
            Return True
        End If

        '2016.02.26 要望番号2469 追加START
        '入荷(大)データの入荷進捗区分が予定入力済(="10")の場合は、チェックしない
        Dim inkaStateKb As String = ds.Tables("LMB020_INKA_L").Rows(0).Item("INKA_STATE_KB").ToString()
        If inkaStateKb.Equals("10") = True Then
            Return True
        End If
        '2016.02.26 要望番号2469 追加END

        '作業の請求日チェック
        If Me.ChkDate(value, Me.SelectGheaderData(ds.Copy, "SelectGheaderDataSagyo"), msg, "SelectGheaderDataSagyo", ds) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 請求日チェック(運賃料)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="value">入荷日</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="selectFlg">検索フラグ　True:検索有　False:検索無</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkSeiqDateUnchin(ByVal ds As DataSet, ByVal value As String, ByVal msg As String, ByVal selectFlg As Boolean) As Boolean

        'レコードがない場合、スルー
        Dim dt As DataTable = ds.Tables("LMB020_UNSO_L")
        If dt.Rows.Count < 1 Then
            Return True
        End If

        '削除レコードの場合、スルー
        If LMConst.FLG.ON.Equals(dt.Rows(0).Item("SYS_DEL_FLG").ToString()) Then
            Return True
        End If

        '2016.02.26 要望番号2469 追加START
        '入荷(大)データの入荷進捗区分が予定入力済(="10")の場合は、チェックしない
        Dim inkaStateKb As String = ds.Tables("LMB020_INKA_L").Rows(0).Item("INKA_STATE_KB").ToString()
        If inkaStateKb.Equals("10") = True Then
            Return True
        End If
        '2016.02.26 要望番号2469 追加END

        '運賃情報の取得
        Dim selectDs As DataSet = Nothing
        If selectFlg = True Then
            selectDs = Me.DacAccess(ds, "SelectUnchinData")
        Else
            selectDs = ds.Copy
        End If
        Dim selectDt As DataTable = selectDs.Tables("F_UNCHIN_TRS")
        Dim max As Integer = selectDt.Rows.Count - 1

        Dim chkDs As DataSet = selectDs.Copy
        Dim chkDt As DataTable = chkDs.Tables("F_UNCHIN_TRS")

        For i As Integer = 0 To max

            chkDt.Clear()
            chkDt.ImportRow(selectDt.Rows(i))

            '運賃の請求日チェック
            If Me.ChkDate(value, Me.SelectGheaderData(chkDs, "SelectGheaderDataUnchin"), msg, "SelectGheaderDataUnchin", ds) = False Then
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 日付チェック
    ''' </summary>
    ''' <param name="value1">比較対象日</param>
    ''' <param name="value2">最終締め日</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkDate(ByVal value1 As String, ByVal value2 As String, ByVal msg As String, ByVal shoriKb As String, ByVal ds As DataSet) As Boolean

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        '比較対象1に値がない場合、スルー
        If String.IsNullOrEmpty(value1) = True Then
            Return True
        End If

        'すでに締め処理が締め処理が終了している場合、エラー
        If value1 <= value2 Then
            Select Case shoriKb
                Case "SelectGheaderDataUnchin"
                    '運賃
                    If ("40").Equals(ds.Tables("LMB020_UNSO_L").Rows(0).Item("TARIFF_BUNRUI_KB").ToString()) = True Then
                        '横持ちの場合
                        '20151020 tsunehira add
                        MyBase.SetMessage("E654")
                        'MyBase.SetMessage("E285", New String() {"横持ち料", msg})
                    Else
                        '運賃の場合
                        '20151020 tsunehira add
                        MyBase.SetMessage("E653")
                        'MyBase.SetMessage("E285", New String() {"運賃", msg})
                    End If
                Case "SelectGheaderDataSagyo"
                    '作業
                    '20151020 tsunehira add
                    MyBase.SetMessage("E655")
                    'MyBase.SetMessage("E285", New String() {"作業料", msg})
            End Select
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' 日付チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkDate2(ByVal ds As DataSet, ByVal msg As String) As Boolean

        '2016.02.26 要望番号2469 追加START
        '入荷(大)データの入荷進捗区分が予定入力済(="10")の場合は、チェックしない
        Dim inkaStateKb As String = ds.Tables("LMB020_INKA_L").Rows(0).Item("INKA_STATE_KB").ToString()
        If inkaStateKb.Equals("10") = True Then
            Return True
        End If
        '2016.02.26 要望番号2469 追加END

        '比較対象1に値がない場合、スルー
        Dim hokanDate As String = ds.Tables("LMB020_INKA_L").Rows(0).Item("HOKAN_STR_DATE").ToString()
        If String.IsNullOrEmpty(hokanDate) = True Then
            Return True
        End If

        'M単位でチェック
        Dim inkaMDt As DataTable = ds.Tables("LMB020_INKA_M")
        Dim drs As DataRow() = inkaMDt.Select(String.Empty, "CUST_CD_S,CUST_CD_SS")
        Dim max As Integer = drs.Length - 1

        'Mがない場合、代表コードチェック
        If max < 0 Then
            Return Me.SelectSubCustDataAtDateChk(ds, hokanDate, msg)
        End If

        Dim dr As DataRow = Nothing
        Dim sCd As String = String.Empty
        Dim ssCd As String = String.Empty
        Dim chkSCd As String = String.Empty
        Dim chkSSCd As String = String.Empty

        '検索用(処理軽量化のために必要のものだけテーブル設定)
        Dim selectDs As DataSet = New DataSet()
        Dim mDt As DataTable = inkaMDt.Copy
        Dim custDt As DataTable = ds.Tables("CUST").Copy
        selectDs.Tables.Add(mDt)
        custDt.Clear()
        selectDs.Tables.Add(custDt)

        For i As Integer = 0 To max

            'チェックする値を設定
            dr = drs(i)
            chkSCd = dr.Item("CUST_CD_S").ToString()
            chkSSCd = dr.Item("CUST_CD_SS").ToString()

            '前回の値と同じ場合、スルー
            If sCd.Equals(chkSCd) = True _
                AndAlso ssCd.Equals(chkSSCd) = True _
                Then
                Continue For
            End If

            '新しいコードを設定
            sCd = chkSCd
            ssCd = chkSSCd

            '検索する行を設定
            mDt.Clear()
            mDt.ImportRow(dr)

            '検索処理
            selectDs = Me.DacAccess(selectDs, "SelectSubCustData")

            '入力チェック
            If Me.SelectSubCustDataAtDateChk(selectDs, hokanDate, msg) = False Then
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 商品の荷主を取得し日付をチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="hokanDate">画面 起算日</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SelectSubCustDataAtDateChk(ByVal ds As DataSet, ByVal hokanDate As String, ByVal msg As String) As Boolean

        Dim custDt As DataTable = ds.Tables("CUST")
        Dim calcDate As String = String.Empty
        If 0 < custDt.Rows.Count Then
            calcDate = custDt.Rows(0).Item("HOKAN_NIYAKU_CALCULATION").ToString()
        End If

        '起算日、最終計算日チェック
        Return Me.IsHokanShimeChk(hokanDate, calcDate, msg)

    End Function

    ''' <summary>
    ''' 起算日、最終計算日チェック
    ''' </summary>
    ''' <param name="value1">画面 起算日</param>
    ''' <param name="value2">荷主M 最終計算日</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHokanShimeChk(ByVal value1 As String, ByVal value2 As String, ByVal msg As String) As Boolean

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        '荷主M 最終計算日がない場合、スルー
        If String.IsNullOrEmpty(value2) = True Then
            Return True
        End If

        'すでに締め処理が締め処理が終了している場合、エラー
        If value1 <= value2 Then

            '20151020 tsunehira add
            MyBase.SetMessage("E624")
            'MyBase.SetMessage("E375", New String() {"保管料・荷役料が既に計算されている", msg})
            Return False

        End If

        Return True

    End Function

    '要望番号:1350 terakawa 2012.08.27 Start
    ''' <summary>
    ''' 同一商品・LOTチェック(同一置き場に同一商品、LOTがあった場合ワーニング)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>データセット</returns>
    ''' <remarks></remarks>
    Private Function ChkGoodsLot(ByVal ds As DataSet) As DataSet

        Dim drL As DataRow = ds.Tables("LMB020_INKA_L").Rows(0)
        Dim dtM As DataTable = ds.Tables("LMB020_INKA_M")
        Dim dtS As DataTable = ds.Tables("LMB020_INKA_S")
        Dim Count As Integer = dtS.Rows.Count - 1

        Dim setDs As DataSet = ds.Copy
        Dim setDtL As DataTable = setDs.Tables("LMB020_INKA_L")
        Dim setDtM As DataTable = setDs.Tables("LMB020_INKA_M")
        Dim setDtS As DataTable = setDs.Tables("LMB020_INKA_S")
        Dim setDtCustDtl As DataTable = setDs.Tables("CUST_DETAILS")
        Dim setDrM As DataRow()
        Dim worningDt As DataTable = ds.Tables("LMB020_WORNING")
        Dim worningDr As DataRow

        '荷主明細から同一置き場・商品チェック特殊荷主情報を取得
        ds = Me.DacAccess(ds, "GetCustDetail")
        Dim dtCustDtl As DataTable = ds.Tables("CUST_DETAILS")

        '要望番号:1393 terakawa 2012.09.03 Start
        '削除データが存在した場合、在庫レコード番号を取得
        Dim DeleteRecNo As String = String.Empty
        For j As Integer = 0 To Count
            If dtS.Rows(j).Item("SYS_DEL_FLG").ToString().Equals("1") Then
                If String.IsNullOrEmpty(DeleteRecNo) = True Then
                    DeleteRecNo = dtS.Rows(j).Item("ZAI_REC_NO").ToString()
                Else
                    DeleteRecNo = String.Concat(DeleteRecNo, "','", dtS.Rows(j).Item("ZAI_REC_NO").ToString())
                End If
            End If
        Next
        '要望番号:1393 terakawa 2012.09.03 End

        For i As Integer = 0 To Count
            '値のクリア
            setDs.Clear()

            '削除データの場合、チェックは行わない
            If dtS.Rows(i).Item("SYS_DEL_FLG").ToString().Equals("1") Then
                Continue For
            End If

            '関連づいている入荷Mを取得
            setDrM = dtM.Select(String.Concat("INKA_NO_M = '", dtS.Rows(i).Item("INKA_NO_M").ToString(), "'"))

            'チェック用データセットにインポート
            setDtL.ImportRow(drL)
            setDtM.ImportRow(setDrM(0))
            setDtS.ImportRow(dtS.Rows(i))
            If dtCustDtl.Rows.Count > 0 Then
                setDtCustDtl.ImportRow(dtCustDtl.Rows(0))
            End If

            '要望番号:1393 terakawa 2012.09.03 Start
            '削除データが存在した場合、重複チェック用データの在庫レコード番号に、削除データの在庫レコード番号を連結
            If String.IsNullOrEmpty(DeleteRecNo) = False Then
                setDtS.Rows(0).Item("ZAI_REC_NO") = String.Concat(setDtS.Rows(0).Item("ZAI_REC_NO").ToString(), "','", DeleteRecNo)
            End If
            '要望番号:1393 terakawa 2012.09.03 End

            '在庫データの重複チェック
            setDs = Me.DacAccess(setDs, "ChkGoodsLot")
            If GetResultCount() > 0 Then
                worningDr = ds.Tables("LMB020_WORNING").NewRow()
                With worningDr
                    .Item("GOODS_CD_CUST") = setDtM.Rows(0).Item("GOODS_CD_CUST").ToString()
                    .Item("TOU_NO") = setDtS.Rows(0).Item("TOU_NO").ToString()
                    .Item("SITU_NO") = setDtS.Rows(0).Item("SITU_NO").ToString()
                    .Item("ZONE_CD") = setDtS.Rows(0).Item("ZONE_CD").ToString()
                    .Item("LOCA") = setDtS.Rows(0).Item("LOCA").ToString()
                    .Item("LOT_NO") = setDtS.Rows(0).Item("LOT_NO").ToString()
                End With
                ds.Tables("LMB020_WORNING").Rows.Add(worningDr)
            End If
        Next

        Return ds

    End Function
    '要望番号:1350 terakawa 2012.08.27 End

    'KASAMA 2013.10.29 WIT対応 Start
    ''' <summary>
    ''' ハンディ対象荷主かどうかを取得します。
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsHandyTargetCust(ByVal ds As DataSet) As Boolean

        ' 処理行が存在しない場合はFalse
        If ds.Tables("LMB020_INKA_WK") Is Nothing OrElse ds.Tables("LMB020_INKA_WK").Rows.Count = 0 Then
            Return False
        End If

        ' ハンディ対象荷主チェック
        ds = Me.DacAccess(ds, "ChkHandyCust")

        Return (0 < GetResultCount())

    End Function
    'KASAMA 2013.10.29 WIT対応 End

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
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.INKA_NO_L, Me, ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' UNSO_NO_Lを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>InkaNoL</returns>
    ''' <remarks></remarks>
    Private Function GetUnsoNoL(ByVal ds As DataSet) As String

        GetUnsoNoL = ds.Tables("LMB020_UNSO_L").Rows(0).Item("UNSO_NO_L").ToString()

        '値が入っていない場合、採番
        If String.IsNullOrEmpty(GetUnsoNoL) = True Then
            Dim num As New NumberMasterUtility
            GetUnsoNoL = num.GetAutoCode(NumberMasterUtility.NumberKbn.UNSO_NO_L, Me, ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString())
        End If

        Return GetUnsoNoL

    End Function

    ''' <summary>
    ''' ZAI_REC_NOを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>ZaiRecNo</returns>
    ''' <remarks></remarks>
    Private Function GetZaiRecNo(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.ZAI_REC_NO, Me, ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' SAGYO_REC_NOを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>ZaiRecNo</returns>
    ''' <remarks></remarks>
    Private Function GetSagyoRecNo(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString())

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

    ''' <summary>
    ''' 在庫の採番値の設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inkaNoL">INKA_NO_L</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetZaiRecNo(ByVal ds As DataSet, ByVal inkaNoL As String) As DataSet

        Dim recNo As String = String.Empty
        Dim inkaSDt As DataTable = ds.Tables("LMB020_INKA_S")
        Dim drs As DataRow() = ds.Tables("LMB020_ZAI").Select(String.Concat(" UP_KBN = '0' AND SYS_DEL_FLG = '0' "))
        Dim max As Integer = drs.Length - 1
        For i As Integer = 0 To max
            drs(i).Item("INKA_NO_L") = inkaNoL
            recNo = Me.GetZaiRecNo(ds)
            drs(i).Item("ZAI_REC_NO") = recNo

            '入荷(小)に在庫Recを設定
            inkaSDt.Select(String.Concat("INKA_NO_L = '", inkaNoL, "' " _
                                         , "AND INKA_NO_M = '", drs(i).Item("INKA_NO_M").ToString(), "' " _
                                         , "AND INKA_NO_S = '", drs(i).Item("INKA_NO_S").ToString(), "' " _
                                         ))(0).Item("ZAI_REC_NO") = recNo
        Next

        Return ds

    End Function

    ''' <summary>
    ''' MaxNoの設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetMaxNo(ByVal ds As DataSet) As DataSet

        'Maxシーケンスのデータ取得
        ds = Me.DacAccess(ds, "SelectMaxNo")
        Dim maxSeqDt As DataTable = ds.Tables("LMB020_MAX_NO")

        '取得できた場合、スルー
        If 0 < maxSeqDt.Rows.Count Then
            Return ds
        End If

        'シーケンスの初期値設定
        Dim maxSeqDr As DataRow = maxSeqDt.NewRow()
        Dim max As Integer = maxSeqDt.Columns.Count - 1
        For i As Integer = 0 To max
            maxSeqDr.Item(i) = LMB020BLC.MAEZERO
        Next
        maxSeqDt.Rows.Add(maxSeqDr)

        Return ds

    End Function

#End Region

#End Region

End Class
