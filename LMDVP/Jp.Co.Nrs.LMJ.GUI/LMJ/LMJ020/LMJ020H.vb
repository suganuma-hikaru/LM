' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMJ     : ｼｽﾃﾑ管理
'  プログラムID     :  LMJ020H : 未使用荷主データ退避
'  作  成  者       :  s.kobayashi
' ==========================================================================
Imports System.Text
Imports System.IO
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMJ020ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMJ020H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMJ020V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMJ020G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMJconV As LMJControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMJconG As LMJControlG

    ''' <summary>
    ''' H共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMJconH As LMJControlH

    ''' <summary>
    ''' 前回値保持変数
    ''' </summary>
    ''' <remarks></remarks>
    Private _PreInputData As String

    ''' <summary>
    ''' サーバ日付
    ''' </summary>
    ''' <remarks></remarks>
    Private _SysDate As String

    ''' <summary>
    ''' ロードフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _LoadFlg As String = LMConst.FLG.OFF

    ''' <summary>
    ''' 初期荷主の月末在庫情報を保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMJ020F = New LMJ020F(Me)

        'Gamen共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMJconG = New LMJControlG(sForm)

        'Validate共通クラスの設定
        Me._LMJconV = New LMJControlV(Me, sForm, Me._LMJconG)

        'Hnadler共通クラスの設定
        Me._LMJconH = New LMJControlH(sForm, MyBase.GetPGID(), Me)

        'Validateクラスの設定
        Me._V = New LMJ020V(Me, frm, Me._LMJconV)

        'Gamenクラスの設定
        Me._G = New LMJ020G(Me, frm, Me._LMJconG)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        '初期値設定
        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Me._G.InitSpread(True)
        Me._G.SetInitValue()

        'ロードフラグをON
        Me._LoadFlg = LMConst.FLG.ON

        'メッセージの表示
        Call Me.SetInitMessage(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMJ020F)

        '処理開始アクション
        Call Me._LMJconH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMJ020C.ActionType.KENSAKU) = False Then
            Call Me._LMJconH.EndAction(frm) '終了処理
            Exit Sub
        End If

        '項目チェック
        If Me._V.IsKensakuSingleCheck = False Then
            Call Me._LMJconH.EndAction(frm) '終了処理
            Exit Sub
        End If

        '検索処理を行う
        Call Me.SelectData(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        '処理終了アクション
        Call Me._LMJconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 実行処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub JikkouEvent(ByVal frm As LMJ020F)

        Dim jikkoAction As LMJ020C.ActionType = Nothing
        Dim arr As ArrayList = Nothing
        Dim ds As LMJ020DS

        '処理開始アクション
        Call Me._LMJconH.StartAction(frm)

        If LMJ020C.SHORI_ESCAPE.Equals(frm.cmbShori.SelectedValue.ToString()) = True Then
            jikkoAction = LMJ020C.ActionType.JIKKOU_ESCAPE
        Else
            jikkoAction = LMJ020C.ActionType.JIKKOU_MODOSHI
        End If

        '権限チェック
        If Me._V.IsAuthorityChk(jikkoAction) = False Then
            Call Me._LMJconH.EndAction(frm) '終了処理
            Exit Sub
        End If

        arr = Me._LMJconH.GetCheckList(frm.sprDetail.ActiveSheet, LMJ020C.SprColumnIndex.DEF)

        ds = Me.SetDataSetInData(frm, arr)

        '項目チェック
        Dim isSuccessChk As Boolean = False
        Select Case jikkoAction
            Case LMJ020C.ActionType.JIKKOU_ESCAPE
                isSuccessChk = Me._V.IsJikkouEscapeSingleCheck

            Case LMJ020C.ActionType.JIKKOU_MODOSHI
                isSuccessChk = Me._V.IsJikkouModoshiSingleCheck

        End Select
        If isSuccessChk = False Then
            Call Me._LMJconH.EndAction(frm) '終了処理
            Exit Sub
        End If

        '実行処理を行う
        Call Me.JikkoShori(frm, ds, jikkoAction)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        '処理終了アクション
        Call Me._LMJconH.EndAction(frm)

    End Sub

#End Region 'イベント定義(一覧)

#Region "内部メソッド"

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInData(ByVal frm As LMJ020F, ByVal arr As ArrayList) As LMJ020DS

        Dim ds As LMJ020DS = New LMJ020DS
        Dim dt As DataTable = ds.Tables(LMJ020C.TABLE_NM_IN)
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        For i As Integer = 0 To max

            dr = dt.NewRow()
            With dr

                .Item("NRS_BR_CD") = Me._LMJconV.GetCellValue(spr.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMJ020G.sprDetailDef.NRS_BR_CD.ColNo))
                .Item("CUST_CD_L") = Me._LMJconV.GetCellValue(spr.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMJ020G.sprDetailDef.CUST_CD_L.ColNo))
                .Item("PROCESS_KB") = Me._LMJconV.GetCellValue(spr.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMJ020G.sprDetailDef.PROCESS_KB.ColNo))
                .Item("LAST_UPD_DATE") = Me._LMJconV.GetCellValue(spr.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMJ020G.sprDetailDef.LAST_UPD_DATE.ColNo)).Replace(CChar("/"), "")
            End With
            dt.Rows.Add(dr)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInSelectData(ByVal frm As LMJ020F, ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMJ020C.TABLE_NM_IN).NewRow()

        '検索条件　単項目部
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
        dr("LAST_UPD_DATE") = frm.imdLastUpdDate.TextValue.ToString()
        dr("PROCESS_KB") = frm.cmbShori.SelectedValue.ToString()

        '検索条件　入力部（スプレッド）
        With frm.sprDetail.ActiveSheet

            dr("CUST_CD_L") = Me._LMJconV.GetCellValue(.Cells(0, LMJ020G.sprDetailDef.CUST_CD_L.ColNo))
            dr("CUST_NM_L") = Me._LMJconV.GetCellValue(.Cells(0, LMJ020G.sprDetailDef.CUST_NM_L.ColNo))
            dr("TANTO_USER") = Me._LMJconV.GetCellValue(.Cells(0, LMJ020G.sprDetailDef.TANTO_USER.ColNo))
            dr("TAIHI_USER") = Me._LMJconV.GetCellValue(.Cells(0, LMJ020G.sprDetailDef.TAIHI_USER.ColNo))

        End With

        '検索条件をデータセットに設定
        ds.Tables(LMJ020C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 移動テーブルのセット
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetTargetTables(ByVal ds As LMJ020DS)

        Dim dt As New LMJ020DS.TargetTablesDataTable

        'LM_MST
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_MST.M_COA)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_MST.M_COACONFIG)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_MST.M_CUSTCOND)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_MST.M_DEST)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_MST.M_DEST_DETAILS)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_MST.M_DESTGOODS)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_MST.M_FURI_GOODS)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_MST.M_GOODS)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_MST.M_GOODS_DETAILS)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_MST.M_HANDY_CUST)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_MST.M_OKURIJO_CSV)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_MST.M_SAGYO)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_MST.M_TANKA)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_MST.M_UNCHIN_TARIFF_SET)

        'LM_TRN
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.B_INKA_L)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.B_INKA_M)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.B_INKA_S)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.C_OUTKA_L)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.C_OUTKA_M)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.C_OUTKA_S)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.D_IDO_HANDY)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.D_IDO_TRS)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.D_WK_ZAI_PRT)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.D_ZAI_SHOGOH)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.D_ZAI_SHOGOH_CUST)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.D_ZAI_SHOGOH_CUST_SUM)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.D_ZAI_TRS)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.D_ZAI_ZAN_JITSU)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.E_SAGYO)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.E_SAGYO_SIJI)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.F_SHIHARAI_TRS)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.F_UNCHIN_TRS)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.F_UNSO_L)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.F_UNSO_LL)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.F_UNSO_M)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.G_DUPONT_INTERFACE_TRS)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.G_DUPONT_SEKY_GL)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.G_KAGAMI_DTL)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.G_KAGAMI_HED)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.G_SEKY_MEISAI)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.G_SEKY_MEISAI_PRT)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.G_SEKY_TBL)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.G_TANKA_WK)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.G_ZAIK_ZAN)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_EDI_GOODSREP_TBL)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_EDI_PRINT)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_GOODS_EDI_BP)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INKAEDI_DTL_BP)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INKAEDI_DTL_DPN)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INKAEDI_DTL_NCGO)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INKAEDI_DTL_NIK)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INKAEDI_DTL_NISSAN)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INKAEDI_DTL_NSN)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INKAEDI_DTL_UKM)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INKAEDI_HED_BP)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INKAEDI_HED_DPN)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INKAEDI_HED_NCGO)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INKAEDI_HED_NSN)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INKAEDI_L)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INKAEDI_M)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INOUTKAEDI_DTL_DIC_NEW)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INOUTKAEDI_DTL_DOW)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INOUTKAEDI_DTL_FJF)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INOUTKAEDI_DTL_M3PL)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INOUTKAEDI_DTL_SMK)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INOUTKAEDI_DTL_TOHO)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INOUTKAEDI_HED_DIC_NEW)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INOUTKAEDI_HED_DOW)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INOUTKAEDI_HED_FJF)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INOUTKAEDI_HED_SMK)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_INOUTKAEDI_HED_TOHO)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_NRSBIN_DIC)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_NRSBIN_TOR)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_NRSCUST_DIC)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_NRSGOODS_DIC)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_NRSGOODS_DNS)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKA_L_BP)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_ASH)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_BP)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_BYK)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_DIC)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_DNS)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_DPN)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_DSP)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_DSPAH)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_GODO)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_HON)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_JC)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_JT)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_KTK)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_LNZ)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_MHM)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_NCGO)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_NIK)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_NKS)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_NSN)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_OTK)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_SFJ)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_SNK)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_SNZ)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_TOR)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_DTL_UKM)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_HED_ASH)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_HED_BP)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_HED_DIC)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_HED_DNS)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_HED_DPN)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_HED_GODO)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_HED_HON)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_HED_JC)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_HED_NCGO)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_HED_NSN)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_HED_OTK)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_HED_SFJ)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_HED_TOR)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_HED_UKM)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_L)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_L_PRT_LNZ)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_M)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_OUTKAEDI_M_PRT_LNZ)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_SENDEDI_BP)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_SENDINEDI_DPN)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_SENDINEDI_NCGO)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_SENDINEDI_NIK)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_SENDINEDI_NSN)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_SENDINEDI_UTI)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_SENDINOUTEDI_DOW)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_SENDINOUTEDI_UKM)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_SENDMONTHLY_SNZ)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_SENDOUTEDI_ASH)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_SENDOUTEDI_DPN)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_SENDOUTEDI_NCGO)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_SENDOUTEDI_NIK)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_SENDOUTEDI_NSN)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_SENDOUTEDI_SFJ)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_SENDOUTEDI_SNK)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_UNSOCO_EDI)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.H_ZAIKO_EDI_BP)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.I_CONT_TRACK)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.I_CONT_TRACK_LOG)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.I_DOW_SEIQ_PRT)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.I_HAISO_UNCHIN_TRS)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.I_HIKITORI_UNCHIN_MEISAI)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.I_HON_TEIKEN)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.I_HONEY_ALBAS_CHG)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.I_HONEY_SHIPTOCD_CHG)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.I_MCPU_UNCHIN_CHK)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.I_NRC_KAISHU_TBL)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.I_UKI_BUNRUI_MST)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.I_UKI_HOKOKU)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.I_UKI_ZAIKO)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.I_UKI_ZAIKO_SUM)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.I_UKIMA_SEKY_MEISAI)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.I_YOKO_UNCHIN_TRS)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.I_YUSO_R)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.I_YUSO_R_SUM)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.M_BYK_GOODS)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.M_CHOKUSO_NIK)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.M_HINMOKU_FJF)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.M_HINMOKU_TRM)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.M_KOKYAKU_TRM)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.M_SEHIN_NIK)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.M_SET_GOODS_LNZ)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.M_TOKUI_FJF)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.M_TOKUI_NIK)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.Q_WK_INOUT)
        Me.SetTableNm(dt, LMJ020C.TARGET_TABLES_TRN.W_ZAI_SHOMEI)

        For Each row As DataRow In dt.Select("", "DEL_SORT")
            ds.Tables(LMJ020C.TABLE_NM_TARGETTBL).ImportRow(row)
        Next

    End Sub

    Private Sub SetTableNm(ByVal dt As DataTable, ByVal tType As LMJ020C.TARGET_TABLES_MST)

        Dim dr As DataRow = dt.NewRow

        dr("TableIDGroup") = tType.ToString.Substring(0, 1)
        dr("TableNM") = tType.ToString
        '削除順序
        dr("DEL_SORT") = DirectCast(tType, Integer)
        dt.Rows.Add(dr)

    End Sub

    Private Sub SetTableNm(ByVal dt As DataTable, ByVal tType As LMJ020C.TARGET_TABLES_TRN)

        Dim dr As DataRow = dt.NewRow

        dr("TableIDGroup") = tType.ToString.Substring(0, 1)
        dr("TableNM") = tType.ToString
        '削除順序
        dr("DEL_SORT") = DirectCast(tType, Integer)

        dt.Rows.Add(dr)

    End Sub

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMJ020F)

        '画面全ロック
        MyBase.LockedControls(frm)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'メッセージのクリア
        MyBase.ClearMessageAria(frm)

    End Sub

    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub EndAction(ByVal frm As LMJ020F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub


    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMJ020F)

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        '閾値の設定
        Dim lc As Integer = Convert.ToInt32(Convert.ToDouble( _
                              MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                              .Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1")))
        MyBase.SetLimitCount(lc)

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                              MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                              .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1")))
        MyBase.SetMaxResultCount(mc)

        'DataSet設定
        Dim rtDs As DataSet = New LMJ020DS()
        Call Me.SetDataSetInSelectData(frm, rtDs)

        ''SPREAD(表示行)初期化
        'frm.sprDetail.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMJconH.CallWSAAction(DirectCast(frm, Form) _
                                                         , "LMJ020BLF", "SelectListData", rtDs _
                                                         , lc, mc)

        '検索成功時共通処理を行う
        If rtnDs IsNot Nothing Then

            Call Me.SuccessSelect(frm, rtnDs)

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理（画面別）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMJ020F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMJ020C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'SPREAD(表示行)初期化
        frm.sprDetail.CrearSpread()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        Dim max As Integer = dt.Rows.Count

        If 0 < max Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G016", New String() {max.ToString()})

        End If

    End Sub


    ''' <summary>
    ''' 実行処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub JikkoShori(ByVal frm As LMJ020F, ByVal ds As DataSet, ByVal act As LMJ020C.ActionType)

        Dim methodNM As String = String.Empty
        Dim methodNMJpn As String = String.Empty
        Select Case act
            Case LMJ020C.ActionType.JIKKOU_ESCAPE
                methodNM = "ProcessDataEscape"
                methodNMJpn = "データ退避"
            Case LMJ020C.ActionType.JIKKOU_MODOSHI
                methodNM = "ProcessDataModoshi"
                methodNMJpn = "データ戻し"

        End Select

        Dim inDs As DataSet
        'IN情報を設定
        Dim errFlg As Boolean = False
        Dim thisErrFlg As Boolean = False
        Dim errCnt As Integer = 0
        Dim presentCnt As Integer = 0
        Dim totalCnt As Integer = ds.Tables(LMJ020C.TABLE_NM_IN).Rows.Count
        Dim keikaMSG As String = String.Empty
        For Each row As DataRow In ds.Tables(LMJ020C.TABLE_NM_IN).Rows
            presentCnt = presentCnt + 1
            thisErrFlg = False
            inDs = New LMJ020DS
            inDs.Tables(LMJ020C.TABLE_NM_IN).ImportRow(row)
            'ターゲットテーブルのセット
            Me.SetTargetTables(DirectCast(inDs, LMJ020DS))

            Try
                '実行時WSAクラス呼び出し
                ds = MyBase.CallWSA("LMJ020BLF", methodNM, inDs)
            Catch ex As Exception
                MsgBox(ex.ToString())
                Logger.WriteErrorLog("LMJ020", "JikkoShori", ex.ToString())
                errFlg = True
                thisErrFlg = True
            End Try

            If MyBase.IsMessageStoreExist() Then
                '処理終了メッセージの表示
                Me.ShowStorePrintData(frm)
                MyBase.ClearMessageStoreData()
                errFlg = True
                thisErrFlg = True
            End If

            If thisErrFlg = True Then
                errCnt = errCnt + 1
            End If
            keikaMSG = String.Concat("（", presentCnt.ToString(), " / ", totalCnt.ToString(), "件　エラー件数：", errCnt, "件）")
            MyBase.ShowMessage(frm, "G057", New String() {methodNMJpn, keikaMSG})
            frm.Refresh()
        Next

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {methodNMJpn, keikaMSG})

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.CUST)


    End Sub

#End Region

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMJ020F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find(LMJControlC.MES_AREA, True)(0).Text) = True Then

            'メッセージ設定
            Call Me.SetInitMessage(frm)

        End If

    End Sub

    ''' <summary>
    ''' 初期メッセージ
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetInitMessage(ByVal frm As LMJ020F)
        MyBase.ShowMessage(frm, "G007")
    End Sub

    ''' <summary>
    ''' エラー帳票出力処理
    ''' </summary>
    ''' <returns>出力する場合:False　出力しない場合:True</returns>
    ''' <remarks></remarks>
    Private Function ShowStorePrintData(ByVal frm As LMJ020F) As Boolean

        If MyBase.IsMessageStoreExist() = True Then

            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            Return False

        End If

        Return True

    End Function
#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMJ020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.JikkouEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMJ020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.SelectListEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMJ020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region

End Class