' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG900H : 請求処理 請求取込データ抽出作成
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMG900ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
Public Class LMG900H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 共通Handlerクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlH As LMGControlH

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMG900V

    ''' <summary>
    '''パラメータDataRowを保管
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDr As DataRow

    ''' <summary>
    '''返却用DataSet保管
    ''' </summary>
    ''' <remarks></remarks>
    Private _RtnDs As DataSet = New LMG900DS()
    '★ ADD START 2011/09/06 SUGA

    ''' <summary>
    '''返却用DataSet保管
    ''' </summary>
    ''' <remarks></remarks>
    Private _IsKazeiHokanRecFlg As Boolean = False
    '★ ADD E N D 2011/09/06 SUGA

#End Region

#Region "Method"

#Region "初期処理"

    ''' <summary>
    '''  ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <param name="prm"></param>
    ''' <remarks></remarks>
    Public Sub Main(ByVal prm As LMFormData)

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        Me._PrmDr = prmDs.Tables(LMG900C.TABLE_NM_IN).Rows(0)

        'Handler共通クラスの設定
        Me._ControlH = New LMGControlH("LMG900")

        'Validateクラスの設定
        Me._V = New LMG900V(Me)

        '取込データ検索処理
        Me.ImportData(prmDs)

        '返却パラメータへ取込データを設定
        Dim ds As DataSet = Me._RtnDs
        prm.ParamDataSet = ds

        '返却フラグを立てる
        prm.ReturnFlg = True

    End Sub

#End Region

    ''' <summary>
    '''取込データ検索処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub ImportData(ByVal ds As DataSet)

        Dim msg As String = "取込処理"

        '適用開始日取得処理
        Call Me.SetStartDate(ds)

        '移行データチェックを行う
        If Me.ChkExistIkoData(ds) = False Then
            Exit Sub
        End If

        '確定チェックを行う
        If Me.ChkImpData(ds) = False Then
            Exit Sub
        End If

        '取込データ検索処理
        Me.SelectImpData(ds)

        '対象データ件数チェック
        Dim importCnt As Integer = Me._RtnDs.Tables(LMG900C.TABLE_NM_OUT).Rows.Count
        If importCnt = 0 Then
            MyBase.SetMessage("G033", New String() {"取込対象データ"})
            Exit Sub
        End If

        '取得データ整合チェック
        If Me._V.ReturnChk(Me._RtnDs) = False Then
            MyBase.SetMessage("S001", New String() {msg})
            Exit Sub
        End If

        '最低保証レコードの追加
        If Me.CalcMinRec(ds) = False Then
            MyBase.SetMessage("S001", New String() {msg})
            Exit Sub
        End If

        '処理成功時メッセージ設定
        MyBase.SetMessage("G002", New String() {msg, ""})

    End Sub

    ''' <summary>
    ''' 適用開始日取得処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetStartDate(ByVal ds As DataSet)

        Dim getFlg As Boolean = False

        '経理戻しにより作成された新黒データの場合、運賃、作業料、横持料の取込開始日が既に設定されている
        If Me._PrmDr.Item("UNCHIN_FLG").ToString.Equals(LMConst.FLG.ON) _
        OrElse Me._PrmDr.Item("SAGYO_FLG").ToString.Equals(LMConst.FLG.ON) _
        OrElse Me._PrmDr.Item("YOKOMOCHI_FLG").ToString.Equals(LMConst.FLG.ON) Then
            If String.IsNullOrEmpty(Me._PrmDr.Item("UNC_SKYU_DATE_FROM").ToString) Then
                getFlg = True
            End If
        End If

        If getFlg = True Then

            '対象データを検索する
            Dim invDs As DataSet = New LMG000DS()
            Dim invDt As DataTable = invDs.Tables(LMGControlC.TABLE_NM_GET_INV_IN)
            Dim invDr As DataRow = invDt.NewRow()
            invDr.Item("SEIQTO_CD") = Me._PrmDr.Item("SEIQTO_CD")
            invDr.Item("SKYU_DATE") = Me._PrmDr.Item("SKYU_DATE")
            invDr.Item("NRS_BR_CD") = Me._PrmDr.Item("NRS_BR_CD")
            invDt.Rows.Add(invDr)

            invDs = MyBase.CallWSA("LMG900BLF", "GetInvFrom", invDs)

            '取得した請求開始日を設定
            Dim startDate As String = String.Empty
            Dim getDate As String = invDs.Tables(LMGControlC.TABLE_NM_GET_INV_OUT).Rows(0)("SKYU_DATE_FROM").ToString()
            If String.IsNullOrEmpty(getDate) Then
                startDate = "00000000"
            Else
                startDate = Date.ParseExact(getDate, "yyyyMMdd", Nothing).AddDays(1).ToString.Replace("/", "").Substring(0, 8)
            End If

            If String.IsNullOrEmpty(Me._PrmDr.Item("UNC_SKYU_DATE_FROM").ToString) Then
                Me._PrmDr.Item("UNC_SKYU_DATE_FROM") = startDate
            End If
            If String.IsNullOrEmpty(Me._PrmDr.Item("SAG_SKYU_DATE_FROM").ToString) Then
                Me._PrmDr.Item("SAG_SKYU_DATE_FROM") = startDate
            End If
            If String.IsNullOrEmpty(Me._PrmDr.Item("YOK_SKYU_DATE_FROM").ToString) Then
                Me._PrmDr.Item("YOK_SKYU_DATE_FROM") = startDate
            End If

            Dim rtnDt As DataTable = Me._RtnDs.Tables(LMG900C.TABLE_NM_DATE)
            Dim rtnDr As DataRow = rtnDt.NewRow()
            rtnDr.Item("SKYU_DATE_FROM") = startDate
            rtnDt.Rows.Add(rtnDr)

        End If

    End Sub

    ''' <summary>
    '''　保管料・荷役料移行データ存在チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkExistIkoData(ByVal ds As DataSet) As Boolean

        '保管料移行データ存在チェック
        MyBase.CallWSA("LMG900BLF", "ChkExistHokanIkoData", ds)
        If MyBase.IsMessageExist() = True Then
            Return False
        End If

        '荷役料移行データ存在チェック
        MyBase.CallWSA("LMG900BLF", "ChkExistNiyakuIkoData", ds)
        If MyBase.IsMessageExist() = True Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    '''　運賃、作業料、横持ち料確定チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkImpData(ByVal ds As DataSet) As Boolean

        If Me._PrmDr.Item("UNCHIN_FLG").ToString.Equals(LMConst.FLG.ON) Then
            '運賃データ確定チェック
            MyBase.CallWSA("LMG900BLF", "ChkUnchinData", ds)
            If MyBase.IsMessageExist() = True Then
                Return False
            End If
        End If
        If Me._PrmDr.Item("SAGYO_FLG").ToString.Equals(LMConst.FLG.ON) Then
            '作業料データ確定チェック
            MyBase.CallWSA("LMG900BLF", "ChkSagyoData", ds)
            If MyBase.IsMessageExist() = True Then
                Return False
            End If
        End If
        If Me._PrmDr.Item("YOKOMOCHI_FLG").ToString.Equals(LMConst.FLG.ON) Then
            '横持料データ確定チェック
            MyBase.CallWSA("LMG900BLF", "ChkYokomochiData", ds)
            If MyBase.IsMessageExist() = True Then
                Return False
            End If
        End If

        Return True

    End Function

#Region "データ取得、設定処理"

    ''' <summary>
    '''　対象データを抽出する
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SelectImpData(ByVal ds As DataSet)

        '外部倉庫用ABP対策
        Dim flgNoABP As Boolean = False
        Dim drABP As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'G203' AND KBN_NM1 = '", LM.Base.LMUserInfoManager.GetNrsBrCd, "'"))
        If drABP.Length > 0 Then
            flgNoABP = True
        End If

        If Me._PrmDr.Item("HOKAN_FLG").ToString.Equals(LMConst.FLG.ON) Then
            '保管料データ取得
            Call Me.CallHokanBLF(ds, "SelectHokanData", LMG900C.TABLE_NM_HOKAN, flgNoABP)

            '坪貸料データ取得
            Call Me.CallBLF(ds, "SelectHokanTuboData", LMG900C.TABLE_NM_IMPORT, flgNoABP)
        End If
        If Me._PrmDr.Item("NIYAKU_FLG").ToString.Equals(LMConst.FLG.ON) Then
            '荷役料データ取得
            Call Me.CallNiyakuBLF(ds, "SelectNiyakuData", LMG900C.TABLE_NM_NIYAKU, flgNoABP)
        End If
        If Me._PrmDr.Item("UNCHIN_FLG").ToString.Equals(LMConst.FLG.ON) Then
            '運賃データ取得
            'UPD 2016/09/08 再保管対応
            'Call Me.CallBLF(ds, "SelectUnchinData", LMG900C.TABLE_NM_IMPORT)
            Call Me.CallUnchinBLF(ds, "SelectUnchinData", LMG900C.TABLE_NM_IMPORT, flgNoABP)
        End If
        If Me._PrmDr.Item("SAGYO_FLG").ToString.Equals(LMConst.FLG.ON) Then
            '作業料データ取得
            '★ ADD START 2011/09/06 SUGA
            'Call Me.CallBLF(ds, "SelectSagyoData", LMG900C.TABLE_NM_IMPORT)
            Call Me.CallSagyoBLF(ds, "SelectSagyoData", LMG900C.TABLE_NM_SAGYO, flgNoABP)
            '★ ADD E N D 2011/09/06 SUGA
        End If
        If Me._PrmDr.Item("YOKOMOCHI_FLG").ToString.Equals(LMConst.FLG.ON) Then
            '横持料データ取得
            Call Me.CallBLF(ds, "SelectYokomochiData", LMG900C.TABLE_NM_IMPORT, flgNoABP)
        End If
        If Me._PrmDr.Item("TEMPLATE_FLG").ToString.Equals(LMConst.FLG.ON) Then
            'テンプレートマスタデータ取得
            Call Me.CallBLF(ds, "SelectTemplateData", LMG900C.TABLE_NM_IMPORT, flgNoABP)
        End If

        '返却用テーブル共通項目を設定
        Call Me.SetCommonData()

    End Sub

    ''' <summary>
    ''' WSA呼び出し処理を行う
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub CallBLF(ByVal ds As DataSet, ByVal methodNm As String, ByVal tableNm As String, Optional flgNoABP As Boolean = False)

        '対象データを検索する
        ds = MyBase.CallWSA("LMG900BLF", methodNm, ds)

        '外部倉庫であればABP用項目値をクリアする（後の集計処理に乗せるため）
        If flgNoABP Then
            ds = Me.ClearColumnABP(ds, tableNm)
        End If

        If ds.Tables(tableNm).Rows.Count > 0 Then
            '★ ADD START 2011/09/06 SUGA
            If "SelectHokanTuboData".Equals(methodNm) Then
                '坪貸しデータの場合、値引額再設定処理
                Call Me.ReSetTsuboNebiki(ds, tableNm)
            End If
            '★ ADD E N D 2011/09/06 SUGA

            '返却用DataSetに抽出結果格納
            Call Me.SetReturnDs(ds, tableNm)

        End If

    End Sub

    ''' <summary>
    ''' WSA呼び出し処理を行う(保管料計算)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub CallHokanBLF(ByVal ds As DataSet, ByVal methodNm As String, ByVal tableNm As String, Optional flgNoABP As Boolean = False)

        '空行作成処理のため、LMG900INを退避
        Dim dtIn As DataTable = ds.Tables(LMG900C.TABLE_NM_IN)

        '対象データを検索する
        'DBリードオンリー対応
        'ds = MyBase.CallWSA("LMG900BLF", methodNm, ds)
        ds = MyBase.CallWSA("LMG900BLF", methodNm, ds, True)

        '外部倉庫であればABP用項目値をクリアする（後の集計処理に乗せるため）
        If flgNoABP Then
            ds = Me.ClearColumnABP(ds, tableNm)
        End If

        '対象データ0件の場合、最低保証料取得のため、保管料請求先の荷主マスタから保管料の空行を作成
        If ds.Tables(tableNm).Rows.Count = 0 Then
            ds.Tables(LMG900C.TABLE_NM_IN).ImportRow(dtIn.Rows(0))
            ds = SetDataForMin(ds, tableNm)
        End If

        If ds.Tables(tableNm).Rows.Count > 0 Then

            '最低保証金額、保管料の比較を行う
            Call Me.CompareSum(ds, tableNm)

        End If

    End Sub

    ''' <summary>
    ''' ABP用項目値をクリアする（後の集計処理に乗せるため）
    ''' </summary>
    ''' <param name="tableNm"></param>
    Private Function ClearColumnABP(ByVal ds As DataSet, ByVal tableNm As String) As DataSet

        Dim dt As DataTable = ds.Tables(tableNm)
        For i As Integer = 0 To dt.Rows.Count - 1
            With dt.Rows(i)
                .Item("TCUST_BPCD") = String.Empty
                .Item("TCUST_BPNM") = String.Empty
                .Item("PRODUCT_SEG_CD") = String.Empty
                .Item("ORIG_SEG_CD") = String.Empty
                .Item("DEST_SEG_CD") = String.Empty
            End With
        Next

        Return ds

    End Function

    ''' <summary>
    ''' WSA呼び出し処理を行う(荷役料計算)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub CallNiyakuBLF(ByVal ds As DataSet, ByVal methodNm As String, ByVal tableNm As String, Optional flgNoABP As Boolean = False)

        '空行作成処理のため、LMG900INを退避
        Dim dtIn As DataTable = ds.Tables(LMG900C.TABLE_NM_IN)

        '対象データを検索する
        ds = MyBase.CallWSA("LMG900BLF", methodNm, ds)

        '外部倉庫であればABP用項目値をクリアする（後の集計処理に乗せるため）
        If flgNoABP Then
            ds = Me.ClearColumnABP(ds, tableNm)
        End If

        '対象データ0件の場合、最低保証料取得のため、荷役料請求先の荷主マスタから荷役料の空行を作成
        If ds.Tables(tableNm).Rows.Count = 0 Then
            ds.Tables(LMG900C.TABLE_NM_IN).ImportRow(dtIn.Rows(0))
            ds = SetDataForMin(ds, tableNm)
        End If

        If ds.Tables(tableNm).Rows.Count > 0 Then

            '誤差を考慮し、データテーブルに再度設定する
            Call Me.SetNiyakuGosa(ds)

            '課税区分、部署コード毎に請求額を計算する
            Call Me.CalcSeiq(ds, tableNm)

        End If

    End Sub

    'ADD 2016/09/08 再保管対応

    ''' <summary>
    ''' WSA呼び出し処理を行う(運賃計算)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub CallUnchinBLF(ByVal ds As DataSet, ByVal methodNm As String, ByVal tableNm As String, Optional flgNoABP As Boolean = False)

        '対象データを検索する
        ds = MyBase.CallWSA("LMG900BLF", methodNm, ds)

        '外部倉庫であればABP用項目値をクリアする（後の集計処理に乗せるため）
        If flgNoABP Then
            ds = Me.ClearColumnABP(ds, tableNm)
        End If

        If ds.Tables(tableNm).Rows.Count > 0 Then

            '課税区分、部署コード、自社他社区分毎に請求額を計算する
            Call Me.CalcSeiq(ds, tableNm)

        End If

    End Sub

    '★ ADD START 2011/09/06 SUGA

    ''' <summary>
    ''' WSA呼び出し処理を行う(作業料計算)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub CallSagyoBLF(ByVal ds As DataSet, ByVal methodNm As String, ByVal tableNm As String, Optional flgNoABP As Boolean = False)

        '対象データを検索する
        ds = MyBase.CallWSA("LMG900BLF", methodNm, ds)

        '外部倉庫であればABP用項目値をクリアする（後の集計処理に乗せるため）
        If flgNoABP Then
            ds = Me.ClearColumnABP(ds, tableNm)
        End If

        If ds.Tables(tableNm).Rows.Count > 0 Then

            '誤差を考慮し、データテーブルに再度設定する
            Call Me.SetSagyoGosa(ds)

            '課税区分、部署コード毎に請求額を計算する
            Call Me.CalcSeiq(ds, tableNm)

        End If

    End Sub
    '★ ADD E N D 2011/09/06 SUGA

    ''' <summary>
    ''' 返却用DataSetにデータを格納する
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetReturnDs(ByVal ds As DataSet, ByVal tableNm As String)

        Dim setDt As DataTable = ds.Tables(tableNm)
        Dim max As Integer = setDt.Rows.Count - 1
        For i As Integer = 0 To max
            Me._RtnDs.Tables(LMG900C.TABLE_NM_OUT).ImportRow(setDt.Rows(i))
        Next

    End Sub

    ''' <summary>
    ''' 共通項目を設定する
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCommonData()

        Dim dt As DataTable = Me._RtnDs.Tables(LMG900C.TABLE_NM_OUT)
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max
            With dt.Rows(i)
                .Item("KEISAN_TLGK") = CLng(dt.Rows(i).Item("KEISAN_TLGK"))
                .Item("SKYU_NO") = Me._PrmDr.Item("SKYU_NO")
                .Item("SKYU_SUB_NO") = String.Empty
                .Item("MAKE_SYU_KB") = LMGControlC.DETAIL_SAKUSEI_AUTO
                .Item("PRINT_SORT") = "99"
                .Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                .Item("INS_FLG") = LMConst.FLG.ON
                .Item("KANJO_KAMOKU_CD") = Me.GetKanjoKmkCd(.Item("BUSYO_CD").ToString, .Item("KEIRI_KB").ToString, LMGControlC.GetKanjoKmkInfo.KANJO_KMK_CD, .Item("JISYATASYA_KB").ToString)
                .Item("KEIRI_BUMON_CD") = Me.GetKanjoKmkCd(.Item("BUSYO_CD").ToString, .Item("KEIRI_KB").ToString, LMGControlC.GetKanjoKmkInfo.KEIRI_BUMON_CD)
                .Item("MAKE_SYU_KB_NM") = Me.SelectKbnData(LMGControlC.DETAIL_SAKUSEI_AUTO, LMKbnConst.KBN_K021)

                '科目分け対応
                'GROUP_KB='xxA'の役目は終わったので'xx'にする
                Select Case .Item("GROUP_KB").ToString()
                    Case "03A"
                        '運賃
                        .Item("GROUP_KB") = "03"
                    Case "05A"
                        '横持料
                        .Item("GROUP_KB") = "05"
                End Select
            End With
        Next

    End Sub

    ''' <summary>
    ''' 勘定科目コード取得
    ''' </summary>
    ''' <param name="busyoCd">部署コード</param>
    ''' <param name="keiriKb">経理科目コード区分</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetKanjoKmkCd(ByVal busyoCd As String, ByVal keiriKb As String, ByVal rtnInfo As LMGControlC.GetKanjoKmkInfo, Optional ByRef sJISYATASYA_KB As String = "") As String

        Dim rtnString As String = String.Empty

        If String.IsNullOrEmpty(busyoCd) _
        OrElse String.IsNullOrEmpty(keiriKb) Then
            Return rtnString
        End If

        Dim filter As String = String.Empty
        '区分マスタを検索し、取得結果が0件の場合、エラー
        filter = String.Empty
        filter = String.Concat(filter, "KBN_GROUP_CD = '", LMKbnConst.KBN_B006, "'")
        filter = String.Concat(filter, " AND KBN_NM4 = '", busyoCd, "'")
        filter = String.Concat(filter, " AND KBN_NM1 = '", keiriKb, "'")
        filter = String.Concat(filter, " AND SYS_DEL_FLG = '0'")

        Dim kbnDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)

        If kbnDr.Length = 0 Then
            Return rtnString
        End If

        Select Case rtnInfo
            Case LMGControlC.GetKanjoKmkInfo.KANJO_KMK_CD
                '勘定科目コードを返却
                'upd 2016/09/06 最保管対応
                Select Case sJISYATASYA_KB
                    Case "02"
                        If String.Empty.Equals(kbnDr(0).Item("KBN_NM7").ToString.Trim) = False Then
                            rtnString = kbnDr(0).Item("KBN_NM7").ToString()
                        Else
                            rtnString = kbnDr(0).Item("KBN_NM3").ToString()
                        End If
                    Case Else
                        rtnString = kbnDr(0).Item("KBN_NM3").ToString()
                End Select
            Case LMGControlC.GetKanjoKmkInfo.KEIRI_BUMON_CD
                '経理部門コードを返却
                rtnString = kbnDr(0).Item("KBN_NM6").ToString()
        End Select

        Return rtnString

    End Function

    ''' <summary>
    ''' 最低保証額計算
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CalcMinRec(ds As DataSet) As Boolean

        Dim rtnDs As DataSet = MyBase.CallWSA("LMG900BLF", "SelectSeiqtoData", ds)
        Dim rtnDt As DataTable = rtnDs.Tables(LMG900C.TABLE_NM_SEIQTO)

        If rtnDt.Rows.Count > 0 Then
            Me._RtnDs.Merge(rtnDt)

            '個別最低保証金額の計算
            CalcKobetuMin(ds)

            '鑑最低保証金額の計算
            CalcTotalMin(ds)

        End If

        Return True

    End Function

    ''' <summary>
    ''' 最低保証額計算
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CalcTotalMin(ByVal ds As DataSet) As Boolean

        Dim dtIn As DataTable = ds.Tables(LMG900C.TABLE_NM_IN)
        Dim dtOut As DataTable = Me._RtnDs.Tables(LMG900C.TABLE_NM_OUT)
        Dim dtSeiq As DataTable = Me._RtnDs.Tables(LMG900C.TABLE_NM_SEIQTO)

        Dim ttlMinAmt As Long = CLng(dtSeiq.Rows(0).Item("TOTAL_MIN_SEIQ_AMT").ToString)
        Dim tlgk As Long = 0


        '鑑最低保証額計算
        If ttlMinAmt > 0 _
            AndAlso ( _
                (LMConst.FLG.ON.Equals(dtSeiq.Rows(0).Item("STORAGE_TOTAL_FLG").ToString) _
                        AndAlso LMConst.FLG.ON.Equals(dtIn.Rows(0).Item("HOKAN_FLG").ToString)) _
                    OrElse (LMConst.FLG.ON.Equals(dtSeiq.Rows(0).Item("HANDLING_TOTAL_FLG").ToString) _
                        AndAlso LMConst.FLG.ON.Equals(dtIn.Rows(0).Item("NIYAKU_FLG").ToString)) _
                    OrElse (LMConst.FLG.ON.Equals(dtSeiq.Rows(0).Item("UNCHIN_TOTAL_FLG").ToString) _
                        AndAlso LMConst.FLG.ON.Equals(dtIn.Rows(0).Item("UNCHIN_FLG").ToString)) _
                    OrElse (LMConst.FLG.ON.Equals(dtSeiq.Rows(0).Item("SAGYO_TOTAL_FLG").ToString) _
                        AndAlso LMConst.FLG.ON.Equals(dtIn.Rows(0).Item("SAGYO_FLG").ToString)) _
                ) Then

            Dim ttlMin As Long = ttlMinAmt

            For Each rtnDr As DataRow In Me._RtnDs.Tables(LMG900C.TABLE_NM_OUT).Rows
                tlgk = 0

                If LMGControlC.STORQAGE_FEE1.Equals(rtnDr.Item("GROUP_KB").ToString) _
                    AndAlso LMConst.FLG.ON.Equals(dtSeiq.Rows(0).Item("STORAGE_TOTAL_FLG").ToString) Then
                    tlgk = CLng(rtnDr.Item("KEISAN_TLGK"))
                End If
                If LMGControlC.HANDLING_CHARGE1.Equals(rtnDr.Item("GROUP_KB").ToString) _
                    AndAlso LMConst.FLG.ON.Equals(dtSeiq.Rows(0).Item("HANDLING_TOTAL_FLG").ToString) Then
                    tlgk = CLng(rtnDr.Item("KEISAN_TLGK"))
                End If
                If LMGControlC.FREIGHT_TAXATION1.Equals(rtnDr.Item("GROUP_KB").ToString) _
                    AndAlso LMConst.FLG.ON.Equals(dtSeiq.Rows(0).Item("UNCHIN_TOTAL_FLG").ToString) Then
                    tlgk = CLng(rtnDr.Item("KEISAN_TLGK"))
                End If
                If LMGControlC.WORK_FEE1.Equals(rtnDr.Item("GROUP_KB").ToString) _
                    AndAlso LMConst.FLG.ON.Equals(dtSeiq.Rows(0).Item("SAGYO_TOTAL_FLG").ToString) Then
                    tlgk = CLng(rtnDr.Item("KEISAN_TLGK"))
                End If
                ttlMin = ttlMin - tlgk
            Next

            If ttlMin > 0 Then
                '
                Dim selectString As String = String.Empty
                selectString = String.Concat(selectString, " SYS_DEL_FLG = '0' ")
                selectString = String.Concat(selectString, " AND ", "GROUP_KB   = '01'")
                selectString = String.Concat(selectString, " AND ", "SEIQKMK_CD = ", " '", LMG900C.MIN_SEIQKMK_CD, "' ")
                Dim drSeiqKmk As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SEIQKMK).Select(selectString)

                If drSeiqKmk.Count > 0 Then

                    Dim busyoCd As String = dtIn.Rows(0).Item("BUSYO_CD").ToString
                    'Dim busyoNm As String = SelectKbnData(busyoCd, LMKbnConst.KBN_B007)
                    Dim taxKbnCd As String = drSeiqKmk(0).Item("TAX_KB").ToString
                    Dim taxKbnNm As String = SelectKbnData(taxKbnCd, LMKbnConst.KBN_Z001)
                    Dim dr As DataRow = Me._RtnDs.Tables(LMG900C.TABLE_NM_OUT).NewRow()

                    dr.Item("SKYU_NO") = Me._PrmDr.Item("SKYU_NO")
                    dr.Item("SKYU_SUB_NO") = String.Empty
                    dr.Item("GROUP_KB") = "01"
                    dr.Item("SEIQKMK_CD") = LMG900C.MIN_SEIQKMK_CD
                    dr.Item("SEIQKMK_NM") = drSeiqKmk(0).Item("SEIQKMK_NM").ToString
                    dr.Item("KEIRI_KB") = drSeiqKmk(0).Item("KEIRI_KB").ToString
                    dr.Item("TAX_KB") = drSeiqKmk(0).Item("TAX_KB").ToString
                    dr.Item("TAX_KB_NM") = taxKbnNm
                    dr.Item("MAKE_SYU_KB") = LMGControlC.DETAIL_SAKUSEI_AUTO
                    dr.Item("MAKE_SYU_KB_NM") = Me.SelectKbnData(LMGControlC.DETAIL_SAKUSEI_AUTO, LMKbnConst.KBN_K021)
                    dr.Item("BUSYO_CD") = busyoCd
                    dr.Item("KANJO_KAMOKU_CD") = Me.GetKanjoKmkCd(busyoCd, drSeiqKmk(0).Item("KEIRI_KB").ToString, LMGControlC.GetKanjoKmkInfo.KANJO_KMK_CD)
                    dr.Item("KEIRI_BUMON_CD") = Me.GetKanjoKmkCd(busyoCd, drSeiqKmk(0).Item("KEIRI_KB").ToString, LMGControlC.GetKanjoKmkInfo.KEIRI_BUMON_CD)
                    dr.Item("KEISAN_TLGK") = ttlMin
                    dr.Item("NEBIKI_RT") = 0
                    dr.Item("NEBIKI_GK") = 0
                    dr.Item("TEKIYO") = String.Concat(LMG900C.PREFIX_TOTAL_MIN, Space(1), ttlMinAmt.ToString("#,##0"))
                    dr.Item("PRINT_SORT") = "99"
                    dr.Item("TEMPLATE_IMP_FLG") = "00"
                    dr.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
                    dr.Item("INS_FLG") = LMConst.FLG.ON
                    dr.Item("SKYU_DATE_FROM") = String.Empty
                    dr.Item("RECORD_NO") = String.Empty
                    dr.Item("JISYATASYA_KB") = String.Empty

                    Me._RtnDs.Tables(LMG900C.TABLE_NM_OUT).Rows.Add(dr)
                End If

            End If
        End If

        Return True

    End Function


    ''' <summary>
    ''' 計算額を取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks>
    ''' </remarks>
    Private Function CalcKobetuMin(ByVal ds As DataSet) As Boolean

        '個別最低保証額
        Dim storageMin As Long = 0
        Dim storageOtherMin As Long = 0
        Dim handlingMin As Long = 0
        Dim handlingOtherMin As Long = 0
        Dim unchinMin As Long = 0
        Dim sagyoMin As Long = 0
        '取込金額
        Dim storageAmt As Long = 0
        Dim storageOtherAmt As Long = 0
        Dim handlingAmt As Long = 0
        Dim handlingOtherAmt As Long = 0
        Dim unchinAmt As Long = 0
        Dim sagyoAmt As Long = 0
        '加算額
        Dim storageAddAmt As Long = 0
        Dim storageOtherAddAmt As Long = 0
        Dim handlingAddAmt As Long = 0
        Dim handlingOtherAddAmt As Long = 0
        Dim unchinAddAmt As Long = 0
        Dim sagyoAddAmt As Long = 0

        Dim dtIn As DataTable = ds.Tables(LMG900C.TABLE_NM_IN)
        Dim dtOut As DataTable = Me._RtnDs.Tables(LMG900C.TABLE_NM_OUT)
        Dim dtSeiq As DataTable = Me._RtnDs.Tables(LMG900C.TABLE_NM_SEIQTO)

        '計算結果の金額を取得
        For Each drOut As DataRow In dtOut.Rows
            Select Case drOut.Item("GROUP_KB").ToString
                Case LMGControlC.STORQAGE_FEE1
                    If "01".Equals(drOut.Item("JISYATASYA_KB").ToString) Then
                        storageAmt += CLng(drOut.Item("KEISAN_TLGK"))
                    ElseIf "02".Equals(drOut.Item("JISYATASYA_KB").ToString) Then
                        storageOtherAmt += CLng(drOut.Item("KEISAN_TLGK"))
                    End If
                Case LMGControlC.HANDLING_CHARGE1
                    If "01".Equals(drOut.Item("JISYATASYA_KB").ToString) Then
                        handlingAmt += CLng(drOut.Item("KEISAN_TLGK"))
                    ElseIf "02".Equals(drOut.Item("JISYATASYA_KB").ToString) Then
                        handlingOtherAmt += CLng(drOut.Item("KEISAN_TLGK"))
                    End If
                Case LMGControlC.FREIGHT_TAXATION1
                    unchinAmt += CLng(drOut.Item("KEISAN_TLGK"))
                Case LMGControlC.WORK_FEE1
                    sagyoAmt += CLng(drOut.Item("KEISAN_TLGK"))
            End Select
        Next

        '最低保証金額取得
        storageMin = CLng(dtSeiq.Rows(0).Item("STORAGE_MIN_AMT").ToString)
        storageOtherMin = CLng(dtSeiq.Rows(0).Item("STORAGE_OTHER_MIN_AMT").ToString)
        handlingMin = CLng(dtSeiq.Rows(0).Item("HANDLING_MIN_AMT").ToString)
        handlingOtherMin = CLng(dtSeiq.Rows(0).Item("HANDLING_OTHER_MIN_AMT").ToString)
        unchinMin = CLng(dtSeiq.Rows(0).Item("UNCHIN_MIN_AMT").ToString)
        sagyoMin = CLng(dtSeiq.Rows(0).Item("SAGYO_MIN_AMT").ToString)

        '最低保証額の差額
        If LMConst.FLG.ON.Equals(dtIn.Rows(0).Item("HOKAN_FLG").ToString) Then
            If storageMin > storageAmt Then
                storageAddAmt = storageMin - storageAmt
            End If
            If storageOtherMin > storageOtherAmt Then
                storageOtherAddAmt = storageOtherMin - storageOtherAmt
            End If
        End If
        If LMConst.FLG.ON.Equals(dtIn.Rows(0).Item("NIYAKU_FLG").ToString) Then
            If handlingMin > handlingAmt Then
                handlingAddAmt = handlingMin - handlingAmt
            End If
            If handlingOtherMin > handlingOtherAmt Then
                handlingOtherAddAmt = handlingOtherMin - handlingOtherAmt
            End If
        End If
        If LMConst.FLG.ON.Equals(dtIn.Rows(0).Item("UNCHIN_FLG").ToString) Then
            If unchinMin > unchinAmt Then
                unchinAddAmt = unchinMin - unchinAmt
            End If

        End If
        If LMConst.FLG.ON.Equals(dtIn.Rows(0).Item("SAGYO_FLG").ToString) Then
            If sagyoMin > sagyoAmt Then
                sagyoAddAmt = sagyoMin - sagyoAmt
            End If
        End If

        '最低保証額で更新
        Dim storageUpdFlg As Boolean = False
        Dim storageOtherUpdFlg As Boolean = False
        Dim handlingUpdFlg As Boolean = False
        Dim handlingOtherUpdFlg As Boolean = False
        Dim unchinUpdFlg As Boolean = False
        Dim sagyoUpdFlg As Boolean = False

        For Each drOut As DataRow In dtOut.Rows
            Select Case drOut.Item("GROUP_KB").ToString
                Case LMGControlC.STORQAGE_FEE1
                    If LMConst.FLG.ON.Equals(dtIn.Rows(0).Item("HOKAN_FLG").ToString) Then
                        If "01".Equals(drOut.Item("JISYATASYA_KB").ToString) Then
                            If storageUpdFlg = False Then
                                drOut.Item("KEISAN_TLGK") = CLng(drOut.Item("KEISAN_TLGK")) + storageAddAmt
                                If storageAddAmt > 0 Then
                                    drOut.Item("TEKIYO") = String.Concat(LMG900C.PREFIX_KOBETU_MIN, Space(1), storageMin.ToString("#,##0"))
                                End If
                                storageUpdFlg = True
                            End If
                        ElseIf "02".Equals(drOut.Item("JISYATASYA_KB").ToString) Then
                            If storageOtherUpdFlg = False Then
                                drOut.Item("KEISAN_TLGK") = CLng(drOut.Item("KEISAN_TLGK")) + storageOtherAddAmt
                                If storageOtherAddAmt > 0 Then
                                    drOut.Item("TEKIYO") = String.Concat(LMG900C.PREFIX_KOBETU_MIN, Space(1), storageOtherMin.ToString("#,##0"))
                                End If
                                storageOtherUpdFlg = True
                            End If
                        End If
                    End If
                Case LMGControlC.HANDLING_CHARGE1
                    If LMConst.FLG.ON.Equals(dtIn.Rows(0).Item("NIYAKU_FLG").ToString) Then
                        If "01".Equals(drOut.Item("JISYATASYA_KB").ToString) Then
                            If handlingUpdFlg = False Then
                                drOut.Item("KEISAN_TLGK") = CLng(drOut.Item("KEISAN_TLGK")) + handlingAddAmt
                                If handlingAddAmt > 0 Then
                                    drOut.Item("TEKIYO") = String.Concat(LMG900C.PREFIX_KOBETU_MIN, Space(1), handlingMin.ToString("#,##0"))
                                End If
                                handlingUpdFlg = True
                            End If
                        ElseIf "02".Equals(drOut.Item("JISYATASYA_KB").ToString) Then
                            If handlingOtherUpdFlg = False Then
                                drOut.Item("KEISAN_TLGK") = CLng(drOut.Item("KEISAN_TLGK")) + handlingOtherAddAmt
                                If handlingOtherAddAmt > 0 Then
                                    drOut.Item("TEKIYO") = String.Concat(LMG900C.PREFIX_KOBETU_MIN, Space(1), handlingOtherMin.ToString("#,##0"))
                                End If
                                handlingOtherUpdFlg = True
                            End If
                        End If
                    End If
                Case LMGControlC.FREIGHT_TAXATION1
                    If unchinUpdFlg = False Then
                        If LMConst.FLG.ON.Equals(dtIn.Rows(0).Item("UNCHIN_FLG").ToString) Then
                            drOut.Item("KEISAN_TLGK") = CLng(drOut.Item("KEISAN_TLGK")) + unchinAddAmt
                            If unchinAddAmt > 0 Then
                                drOut.Item("TEKIYO") = String.Concat(LMG900C.PREFIX_KOBETU_MIN, Space(1), unchinMin.ToString("#,##0"))
                            End If
                            unchinUpdFlg = True
                        End If
                    End If
                Case LMGControlC.WORK_FEE1
                    If LMConst.FLG.ON.Equals(dtIn.Rows(0).Item("SAGYO_FLG").ToString) Then
                        If sagyoUpdFlg = False Then
                            drOut.Item("KEISAN_TLGK") = CLng(drOut.Item("KEISAN_TLGK")) + sagyoAddAmt
                            If sagyoAddAmt > 0 Then
                                drOut.Item("TEKIYO") = String.Concat(LMG900C.PREFIX_KOBETU_MIN, Space(1), sagyoMin.ToString("#,##0"))
                            End If
                            sagyoUpdFlg = True
                        End If
                    End If
            End Select
        Next

        Return True

    End Function

    ''' <summary>
    ''' 計算額を取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks>
    ''' </remarks>
    Private Function GetKeisanTlGk(ByVal ds As DataSet, ByVal groupKb As String, ByVal jisyatasyaKb As String) As DataRow()

        Dim dt As DataTable = ds.Tables(LMG900C.TABLE_NM_OUT)
        Dim filter As String = String.Empty
        filter = "GROUP_KB = '" & groupKb & "' AND JISYATASYA_KB = '" & jisyatasyaKb & "'"

        Return dt.Select(filter)

    End Function

    ''' <summary>
    ''' 保管料・荷役料最低保証取得用の空行を作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="tableNm"></param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataForMin(ByVal ds As DataSet, ByVal tableNm As String) As DataSet

        Dim dtIn As DataTable = ds.Tables(LMG900C.TABLE_NM_IN)

        Dim busyoCd As String = dtIn.Rows(0).Item("BUSYO_CD").ToString

        Dim dtInCust As DataTable = ds.Tables(LMG900C.TABLE_NM_IN_CUST)
        Dim drInCust As DataRow

        Dim rtDs As DataSet = New LMG900DS

        Dim targetDt As DataTable = rtDs.Tables(tableNm)
        Dim dr As DataRow = targetDt.NewRow()

        Dim seiqGrpCd As String = String.Empty

        Select Case tableNm
            Case LMG900C.TABLE_NM_HOKAN
                seiqGrpCd = "01"
            Case LMG900C.TABLE_NM_NIYAKU
                seiqGrpCd = "02"
            Case Else
                Return rtDs
        End Select

        drInCust = dtInCust.NewRow
        drInCust("NRS_BR_CD") = dtIn.Rows(0).Item("NRS_BR_CD").ToString
        drInCust("SEIQTO_CD") = dtIn.Rows(0).Item("SEIQTO_CD").ToString
        drInCust("GROUP_KB") = seiqGrpCd
        dtInCust.Rows.Add(drInCust)

        ds = MyBase.CallWSA("LMG900BLF", "SelectCustData", ds)

        '荷主マスタの取得0件、または必要な項目が空の場合は最低保証料取得不可のため、処理終了
        If ds.Tables(LMG900C.TABLE_NM_CUST).Rows.Count = 0 _
                OrElse String.IsNullOrEmpty(ds.Tables(LMG900C.TABLE_NM_CUST).Rows(0).Item("TAX_KB").ToString) _
                OrElse String.IsNullOrEmpty(ds.Tables(LMG900C.TABLE_NM_CUST).Rows(0).Item("JISYATASYA_KB").ToString) Then

            Return rtDs
        End If

        '請求先マスタの最低保証設定フラグが適用しないの場合、処理終了
        If (tableNm = LMG900C.TABLE_NM_HOKAN _
                AndAlso ds.Tables(LMG900C.TABLE_NM_CUST).Rows(0).Item("STORAGE_ZERO_FLG").ToString <> "1") _
            Or (tableNm = LMG900C.TABLE_NM_NIYAKU _
                AndAlso ds.Tables(LMG900C.TABLE_NM_CUST).Rows(0).Item("HANDLING_ZERO_FLG").ToString <> "1") Then

            Return rtDs
        End If

        dr.Item("GROUP_KB") = seiqGrpCd
        dr.Item("SEIQKMK_CD") = ds.Tables(LMG900C.TABLE_NM_CUST).Rows(0).Item("SEIQKMK_CD").ToString
        dr.Item("SEIQKMK_NM") = ds.Tables(LMG900C.TABLE_NM_CUST).Rows(0).Item("SEIQKMK_NM").ToString
        dr.Item("KEIRI_KB") = ds.Tables(LMG900C.TABLE_NM_CUST).Rows(0).Item("KEIRI_KB").ToString
        dr.Item("GOODS_CD_NRS") = String.Empty
        dr.Item("BUSYO_CD") = busyoCd
        dr.Item("TAX_KB") = ds.Tables(LMG900C.TABLE_NM_CUST).Rows(0).Item("TAX_KB").ToString
        dr.Item("TAX_KB_NM") = ds.Tables(LMG900C.TABLE_NM_CUST).Rows(0).Item("TAX_KB_NM").ToString
        dr.Item("KEISAN_TLGK") = 0
        dr.Item("NEBIKI_RT") = "0.00"
        dr.Item("NEBIKI_GK") = "0.000"
        dr.Item("TEKIYO") = String.Empty
        dr.Item("TEMPLATE_IMP_FLG") = "00"
        dr.Item("LOT_NO") = String.Empty
        dr.Item("JISYATASYA_KB") = ds.Tables(LMG900C.TABLE_NM_CUST).Rows(0).Item("JISYATASYA_KB").ToString

        Select Case tableNm
            Case LMG900C.TABLE_NM_HOKAN
                dr.Item("STORAGE_AMO_TTL") = "0.000"
                dr.Item("STORAGE_MIN") = "0.000"
                dr.Item("ANBUN_RATE") = "0"
                dr.Item("SEIQ_STORAGE_AMO_TTL") = "0.000000"
                dr.Item("SUM_STORAGE_AMO_TTL") = "0.000"
            Case LMG900C.TABLE_NM_NIYAKU
                dr.Item("HANDLING_AMO_TTL") = "0.000"
        End Select

        targetDt.Rows.Add(dr)

        Return rtDs

    End Function

#End Region

#Region "取得項目編集(保管料、荷役料誤差計算等)"

    ''' <summary>
    ''' 最低保証金額、保管料の比較を行う
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub CompareSum(ByVal ds As DataSet, ByVal tableNm As String)

        Dim hokanDt As DataTable = ds.Tables(tableNm)
        Dim max As Integer = hokanDt.Rows.Count - 1
        Dim sumHokan As Decimal = Convert.ToDecimal(hokanDt.Rows(0).Item("SUM_STORAGE_AMO_TTL"))

        '最低保証金額、保管料の比較を行う
        If Convert.ToDecimal(hokanDt.Rows(0).Item("STORAGE_MIN")) > sumHokan Then
            '按分率の再計算と誤差考慮
            Call Me.CalcShohinAnbunRate(ds, sumHokan)
        Else
            '商品コード毎の誤差を考慮する
            Call Me.CalcHokanGosa(ds)
        End If

        '課税区分、部署コード毎に請求額を計算する
        Call Me.CalcSeiq(ds, tableNm)

    End Sub

    ''' <summary>
    ''' 商品毎の按分率を求める(最低保証額請求)
    ''' </summary>
    ''' <param name="ds">抽出結果格納DS</param>
    ''' <param name="sumHokan">全体請求額</param>
    ''' <remarks></remarks>
    Private Sub CalcShohinAnbunRate(ByVal ds As DataSet, ByVal sumHokan As Decimal)

        Dim hokanDt As DataTable = ds.Tables(LMG900C.TABLE_NM_HOKAN)
        Dim max As Integer = hokanDt.Rows.Count - 1
        Dim calcHokan As Decimal = 0

        For i As Integer = 0 To max
            With hokanDt.Rows(i)
                .Item("KEISAN_TLGK") = Convert.ToDecimal(.Item("STORAGE_MIN")) * Convert.ToDecimal(.Item("ANBUN_RATE")) * Convert.ToDecimal(.Item("STORAGE_AMO_TTL")) / sumHokan
                .Item("KEISAN_TLGK") = Me._ControlH.ToHalfAdjust(Convert.ToDecimal(.Item("KEISAN_TLGK")), 0)
                calcHokan = calcHokan + Convert.ToDecimal(.Item("KEISAN_TLGK"))
            End With
        Next

        Dim minSeiq As Decimal = Convert.ToDecimal(hokanDt.Rows(0).Item("STORAGE_MIN"))
        If minSeiq - calcHokan <> 0 Then
            '誤差を考慮する
            hokanDt.Rows(max).Item("KEISAN_TLGK") = Convert.ToDecimal(hokanDt.Rows(max).Item("KEISAN_TLGK")) + (minSeiq - calcHokan)
        End If

    End Sub

    ''' <summary>
    ''' 商品コード毎の誤差を考慮する(保管料取込(通常請求))
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub CalcHokanGosa(ByVal ds As DataSet)

        Dim hokanDt As DataTable = ds.Tables(LMG900C.TABLE_NM_HOKAN)
        Dim max As Integer = hokanDt.Rows.Count - 1
        Dim seiqGk As Decimal = 0  '按分率を考慮する前の請求額
        Dim sumHokan As Decimal = 0 '按分率を考慮した請求額
        Dim goodsCd As String = String.Empty
        Dim lotNo As String = String.Empty
        Dim taxKb As String = String.Empty

        '商品コード毎の誤差を考慮する
        For i As Integer = 0 To max
            With hokanDt.Rows(i)
                .Item("KEISAN_TLGK") = Me._ControlH.ToHalfAdjust(Convert.ToDecimal(.Item("SEIQ_STORAGE_AMO_TTL")), 0)
                If goodsCd.Equals(.Item("GOODS_CD_NRS").ToString()) _
                   AndAlso lotNo.Equals(.Item("LOT_NO").ToString()) _
                   AndAlso taxKb.Equals(.Item("TAX_KB").ToString()) Then
                    sumHokan = sumHokan + Convert.ToDecimal(.Item("KEISAN_TLGK"))
                Else
                    If seiqGk - sumHokan <> 0 Then
                        '誤差を考慮する
                        hokanDt.Rows(i - 1).Item("KEISAN_TLGK") = Convert.ToDecimal(hokanDt.Rows(i - 1).Item("KEISAN_TLGK")) + (seiqGk - sumHokan)
                    End If

                    '比較値の初期化
                    seiqGk = Convert.ToDecimal(.Item("STORAGE_AMO_TTL"))
                    sumHokan = Convert.ToDecimal(.Item("KEISAN_TLGK"))
                    goodsCd = .Item("GOODS_CD_NRS").ToString()
                    lotNo = .Item("LOT_NO").ToString()
                    taxKb = .Item("TAX_KB").ToString()
                End If
            End With
        Next
        '最終行の誤差を考慮する
        If seiqGk - sumHokan <> 0 Then
            '誤差を考慮する
            hokanDt.Rows(max).Item("KEISAN_TLGK") = Convert.ToDecimal(hokanDt.Rows(max).Item("KEISAN_TLGK")) + (seiqGk - sumHokan)
        End If

    End Sub

    ''' <summary>
    ''' 課税区分、部署コード、自社他社区分、製品セグメント、地域セグメント(発/着)毎に請求額を計算する
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub CalcSeiq(ByVal ds As DataSet, ByVal tableNm As String)

        Dim targetDt As DataTable = ds.Tables(tableNm)
        Dim sumGk As Decimal = 0
        Dim seiqKmkCdS As String = String.Empty
        Dim taxKbn As String = String.Empty
        Dim bushoCd As String = String.Empty
        Dim JisyaTasyaKB As String = String.Empty       'ADD 2016/09/08 再保管対応
        Dim productSegCd As String = String.Empty
        Dim origSegCd As String = String.Empty
        Dim destSegCd As String = String.Empty
        Dim tcustBpCd As String = String.Empty
        Dim filter As String = String.Empty
        Select Case tableNm
            Case LMG900C.TABLE_NM_HOKAN
                filter = "GROUP_KB = '01'"
            Case LMG900C.TABLE_NM_NIYAKU
                'UPD 2016/09/08 再保管対応
                'filter = "GROUP_KB = '02'"
                '依頼番号:012387 計算結果が0円でも最低保証をかける
                'filter = "GROUP_KB = '02' AND KEISAN_TLGK <> '0.000000'"
                filter = "GROUP_KB = '02'"
                '★ ADD START 2011/09/06 SUGA
            Case LMG900C.TABLE_NM_SAGYO
                filter = "GROUP_KB = '04'"
                '★ ADD E N D 2011/09/06 SUGA

                'ADD 2016/09/08 Start 再保管対応　運賃のみ
            Case LMG900C.TABLE_NM_IMPORT
                filter = "GROUP_KB = '03'"
                'ADD 2016/09/08 End
        End Select

        'UPD 2016/09/08 再保管対応 JISYATASYA_KB追加
        'Dim selectDr As DataRow() = targetDt.Select(filter, "TAX_KB,BUSYO_CD")
        Dim selectDr As DataRow() = targetDt.Select(filter, "TAX_KB,BUSYO_CD,JISYATASYA_KB,PRODUCT_SEG_CD,ORIG_SEG_CD,DEST_SEG_CD,TCUST_BPCD,SEIQKMK_CD_S")

        'ADD 2016/10/20 selectDr.Length > 0 の条件のとき、下記を処理するように修正
        If selectDr.Length > 0 Then

            Dim max As Integer = selectDr.Length - 1
            '★ ADD START 2011/09/06 SUGA
            For i As Integer = 0 To max
                With selectDr(i)

                    If taxKbn.Equals(.Item("TAX_KB").ToString()) Then
                        '前明細と課税区分が同じであれば、値引額に0を設定する
                        .Item("NEBIKI_GK") = 0
                    End If

                    '比較値の初期化
                    taxKbn = .Item("TAX_KB").ToString()
                End With
            Next

            ' 運賃科目分け対応
            ' SEIQKMK_CD_Sによって2種類に分かれているので、SEIQKMK_CD_Sを最優先としてソートし直す
            Dim dtSortTemp As DataTable = Nothing
            dtSortTemp = selectDr(0).Table.Clone
            For Each drImport As DataRow In selectDr
                dtSortTemp.ImportRow(drImport)
            Next
            selectDr = dtSortTemp.Select("", "SEIQKMK_CD_S,TAX_KB,BUSYO_CD,JISYATASYA_KB,PRODUCT_SEG_CD,ORIG_SEG_CD,DEST_SEG_CD,TCUST_BPCD")

            ' 次のLOOPで使用するため、初期化する
            taxKbn = String.Empty
            '★ ADD E N D 2011/09/06 SUGA
            Dim setDs As DataSet = New LMG900DS()
            Dim setDt As DataTable = setDs.Tables(tableNm)

            For i As Integer = 0 To max
                With selectDr(i)
                    'UPD 2016/09/08 再保管対応 JisyaTasyaKB追加
                    If seiqKmkCdS.Equals(.Item("SEIQKMK_CD_S").ToString()) _
                    AndAlso taxKbn.Equals(.Item("TAX_KB").ToString()) _
                    AndAlso bushoCd.Equals(.Item("BUSYO_CD").ToString()) _
                    AndAlso JisyaTasyaKB.Equals(.Item("JISYATASYA_KB").ToString()) _
                    AndAlso productSegCd.Equals(.Item("PRODUCT_SEG_CD").ToString()) _
                    AndAlso origSegCd.Equals(.Item("ORIG_SEG_CD").ToString()) _
                    AndAlso destSegCd.Equals(.Item("DEST_SEG_CD").ToString()) _
                    AndAlso tcustBpCd.Equals(.Item("TCUST_BPCD").ToString()) Then
                        sumGk = sumGk + Convert.ToDecimal(.Item("KEISAN_TLGK"))
                    Else
                        If i <> 0 Then
                            selectDr(i - 1).Item("KEISAN_TLGK") = sumGk
                            setDt.ImportRow(selectDr(i - 1))
                        End If

                        '比較値の初期化
                        seiqKmkCdS = .Item("SEIQKMK_CD_S").ToString()
                        taxKbn = .Item("TAX_KB").ToString()
                        bushoCd = .Item("BUSYO_CD").ToString()
                        JisyaTasyaKB = .Item("JISYATASYA_KB").ToString()    'ADD 2016/09/08 再保管対応
                        productSegCd = .Item("PRODUCT_SEG_CD").ToString()
                        origSegCd = .Item("ORIG_SEG_CD").ToString()
                        destSegCd = .Item("DEST_SEG_CD").ToString()
                        tcustBpCd = .Item("TCUST_BPCD").ToString()
                        sumGk = Convert.ToDecimal(.Item("KEISAN_TLGK"))
                    End If
                End With
            Next
            '最終行のデータを追加する
            selectDr(max).Item("KEISAN_TLGK") = sumGk
            setDt.ImportRow(selectDr(max))
            '★ ADD START 2011/09/06 SUGA
            If LMG900C.TABLE_NM_HOKAN.Equals(tableNm) Then
                '返却用DataSetに抽出結果格納
                Call Me.ChkHokanKazeiRec(setDs)
            End If
            '★ ADD E N D 2011/09/06 SUGA

            '返却用DataSetに抽出結果格納
            Call Me.SetReturnDs(setDs, tableNm)

        End If

    End Sub

    ''' <summary>
    ''' 商品コード毎の誤差を考慮する(荷役料取込)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetNiyakuGosa(ByVal ds As DataSet)

        Dim niyakuDt As DataTable = ds.Tables(LMG900C.TABLE_NM_NIYAKU)
        Dim max As Integer = niyakuDt.Rows.Count - 1
        Dim seiqGk As Decimal = 0  '按分率を考慮する前の請求額
        Dim sumNiyaku As Decimal = 0 '按分率を考慮した請求額
        Dim goodsCd As String = String.Empty
        Dim lotNo As String = String.Empty
        Dim taxKb As String = String.Empty

        '商品コード毎の誤差を考慮する
        For i As Integer = 0 To max
            With niyakuDt.Rows(i)
                If goodsCd.Equals(.Item("GOODS_CD_NRS").ToString()) _
                   AndAlso lotNo.Equals(.Item("LOT_NO").ToString()) _
                   AndAlso taxKb.Equals(.Item("TAX_KB").ToString()) Then
                    sumNiyaku = sumNiyaku + Convert.ToDecimal(.Item("KEISAN_TLGK"))
                Else
                    If seiqGk - sumNiyaku <> 0 Then
                        '誤差を考慮する
                        niyakuDt.Rows(i - 1).Item("KEISAN_TLGK") = Convert.ToDecimal(niyakuDt.Rows(i - 1).Item("KEISAN_TLGK")) + (seiqGk - sumNiyaku)
                    End If

                    '比較値の初期化
                    seiqGk = Convert.ToDecimal(.Item("HANDLING_AMO_TTL"))
                    sumNiyaku = Convert.ToDecimal(.Item("KEISAN_TLGK"))
                    goodsCd = .Item("GOODS_CD_NRS").ToString()
                    lotNo = .Item("LOT_NO").ToString()
                    taxKb = .Item("TAX_KB").ToString()
                End If
            End With
        Next
        '最終行の誤差を考慮する
        If seiqGk - sumNiyaku <> 0 Then
            '誤差を考慮する
            niyakuDt.Rows(max).Item("KEISAN_TLGK") = Convert.ToDecimal(niyakuDt.Rows(max).Item("KEISAN_TLGK")) + (seiqGk - sumNiyaku)
        End If

    End Sub
    '★ ADD START 2011/09/06 SUGA

    ''' <summary>
    ''' 作業レコード毎の誤差を考慮する(作業料取込)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetSagyoGosa(ByVal ds As DataSet)

        Dim sagyoDt As DataTable = ds.Tables(LMG900C.TABLE_NM_SAGYO)
        Dim max As Integer = sagyoDt.Rows.Count - 1
        Dim seiqGk As Decimal = 0  '按分率を考慮する前の請求額
        Dim sumSagyo As Decimal = 0 '按分率を考慮した請求額
        Dim sagyoRecNo As String = String.Empty

        '作業レコード毎の誤差を考慮する
        For i As Integer = 0 To max
            With sagyoDt.Rows(i)
                If sagyoRecNo.Equals(.Item("SAGYO_REC_NO").ToString()) Then
                    sumSagyo = sumSagyo + Convert.ToDecimal(.Item("KEISAN_TLGK"))
                Else
                    If seiqGk - sumSagyo <> 0 Then
                        '誤差を考慮する
                        sagyoDt.Rows(i - 1).Item("KEISAN_TLGK") = Convert.ToDecimal(sagyoDt.Rows(i - 1).Item("KEISAN_TLGK")) + (seiqGk - sumSagyo)
                    End If

                    '比較値の初期化
                    seiqGk = Convert.ToDecimal(.Item("SAGYO_GK"))
                    sumSagyo = Convert.ToDecimal(.Item("KEISAN_TLGK"))
                    sagyoRecNo = .Item("SAGYO_REC_NO").ToString()
                End If
            End With
        Next
        '最終行の誤差を考慮する
        If seiqGk - sumSagyo <> 0 Then
            '誤差を考慮する
            sagyoDt.Rows(max).Item("KEISAN_TLGK") = Convert.ToDecimal(sagyoDt.Rows(max).Item("KEISAN_TLGK")) + (seiqGk - sumSagyo)
        End If

    End Sub

    ''' <summary>
    ''' 保管料(自動取込)データに課税レコードが存在するかチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks>
    ''' 保管料(自動取込)データに課税レコードが存在する場合、フラグをたてる（坪貸し料値引額設定判定用）
    ''' </remarks>
    Private Sub ChkHokanKazeiRec(ByVal ds As DataSet)

        Dim hokanDt As DataTable = ds.Tables(LMG900C.TABLE_NM_HOKAN)
        Dim filter As String = String.Empty
        filter = "GROUP_KB = '01' AND TAX_KB = '01'"
        Dim selectDr As DataRow() = hokanDt.Select(filter)

        If selectDr.Length > 0 Then
            '保管料（課税）レコードが存在する場合は True を設定
            Me._IsKazeiHokanRecFlg = True
        End If

    End Sub

    ''' <summary>
    ''' 坪貸しデータの場合、値引額再設定処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub ReSetTsuboNebiki(ByVal ds As DataSet, ByVal tableNm As String)

        Dim tsuboDt As DataTable = ds.Tables(tableNm)
        Dim max As Integer = tsuboDt.Rows.Count - 1

        For i As Integer = 0 To max
            With tsuboDt.Rows(i)
                If Me._IsKazeiHokanRecFlg OrElse _
                    i <> 0 Then
                    ' 保管料（自動取込）データに課税レコードが存在する
                    ' または 坪貸し分請求レコードの２レコード目以降の場合、値引額に0を設定
                    .Item("NEBIKI_GK") = 0
                End If
            End With
        Next

    End Sub
    '★ ADD E N D 2011/09/06 SUGA

#End Region

#Region "キャッシュから値取得"

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する
    ''' </summary>
    ''' <param name="kbn">区分コード</param>
    ''' <param name="groupCd">区分分類コード</param>
    ''' <returns>区分名１</returns>
    ''' <remarks></remarks>
    Private Function SelectKbnData(ByVal kbn As String, ByVal groupCd As String) As String

        SelectKbnData = String.Empty

        Dim drows As DataRow() = Me.SelectKbnListDataRow(kbn, groupCd)

        If drows.Length > 0 Then
            '正常時 レートを設定
            SelectKbnData = drows(0).Item("KBN_NM1").ToString()
        End If

        Return SelectKbnData

    End Function

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kbn">区分コード</param>
    ''' <param name="groupCd">区分分類コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Private Function SelectKbnListDataRow(ByVal kbn As String, ByVal groupCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(Me.SelectZbnString(kbn, groupCd))

    End Function

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kbn">区分コード</param>
    ''' <param name="groupCd">区分分類コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectZbnString(ByVal kbn As String, ByVal groupCd As String) As String

        SelectZbnString = String.Empty

        '削除フラグ
        SelectZbnString = String.Concat(SelectZbnString, " SYS_DEL_FLG = '0' ")

        SelectZbnString = String.Concat(SelectZbnString, " AND ", "KBN_GROUP_CD = ", " '", groupCd, "' ")

        If String.IsNullOrEmpty(kbn) = False Then

            SelectZbnString = String.Concat(SelectZbnString, " AND ", "KBN_CD = ", " '", kbn, "' ")

        End If

        Return SelectZbnString

    End Function

#End Region

#End Region

End Class
