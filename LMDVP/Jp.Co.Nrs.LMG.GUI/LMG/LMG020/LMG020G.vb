' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG020G : 保管料/荷役料計算 [明細検索画面]
'  作  成  者       :  []
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports GrapeCity.Win.Editors.Fields
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LMG020Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMG020G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMG020F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMGControlG

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMG020F, ByVal g As LMGControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ControlG = g

    End Sub

#End Region

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim always As Boolean = True
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True
            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = LMGControlC.FUNCTION_KENSAKU
            .F10ButtonName = LMGControlC.FUNCTION_MST_SANSHO
            .F11ButtonName = String.Empty
            .F12ButtonName = LMGControlC.FUNCTION_TOJIRU

            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = always
            .F10ButtonEnabled = always
            .F11ButtonEnabled = lock
            .F12ButtonEnabled = always

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            '検索条件
            .grpSelect.TabIndex = LMG020C.CtlTabIndex.GRP_SELECT
            .cmbBr.TabIndex = LMG020C.CtlTabIndex.CMB_BR
            .txtCustCdL.TabIndex = LMG020C.CtlTabIndex.TXT_CUSTCD_L
            .txtCustCdM.TabIndex = LMG020C.CtlTabIndex.TXT_CUSTCD_M
            .txtCustCdS.TabIndex = LMG020C.CtlTabIndex.TXT_CUSTCD_S
            .txtCustCdSs.TabIndex = LMG020C.CtlTabIndex.TXT_CUSTCD_SS
            .lblCustNm.TabIndex = LMG020C.CtlTabIndex.LBL_CUSTNM
            .btnCustSet.TabIndex = LMG020C.CtlTabIndex.BTN_CUST
            .txtSekySaki.TabIndex = LMG020C.CtlTabIndex.TXT_SEKY
            .lblSeqtoNm.TabIndex = LMG020C.CtlTabIndex.LBL_SEKY
            .btnSeqtoSet.TabIndex = LMG020C.CtlTabIndex.BTN_SEKY
            .cmbSimebi.TabIndex = LMG020C.CtlTabIndex.CMB_SHIMEBI
            .imdInvDate.TabIndex = LMG020C.CtlTabIndex.IMD_INV_DATE
            .chkSelectByNrsB.TabIndex = LMG020C.CtlTabIndex.CHK_TANTOSHA
            .cmbPrint.TabIndex = LMG020C.CtlTabIndex.CMB_PRINT
            .chkMeisaiPrev.TabIndex = LMG020C.CtlTabIndex.CHK_PRE
            .numPrintCnt.TabIndex = LMG020C.CtlTabIndex.NUM_BUSU
            .btnPrint.TabIndex = LMG020C.CtlTabIndex.BTN_PRINT
            .sprMeisai.TabIndex = LMG020C.CtlTabIndex.SPE_DETAIL

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String, ByVal strDate As String)

        '編集部の項目をクリア
        Call Me.ClearControl()

        'コントロールの日付書式設定
        Call Me.SetDateControl()

        '営業所の設定
        Me._Frm.cmbBr.SelectedValue = LMUserInfoManager.GetNrsBrCd()

        '2014.08.04 FFEM高取対応 START
        'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
        Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

        If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
            Me._Frm.cmbBr.ReadOnly = True
        Else
            Me._Frm.cmbBr.ReadOnly = False
        End If
        '2014.08.04 FFEM高取対応 END

        '締日コンボボックス
        Me._Frm.cmbSimebi.SelectedValue = LMG010C.MATSUJIME

        'SBS高道）初期値の設定
        '請求日の設定
        Me._Frm.imdInvDate.TextValue = Me._ControlG.SetControlDate(strDate, -1)

        '実行モード
        Me._Frm.optSeikyuH.Checked = True

        'numberCellの桁数を設定する()
        Me._Frm.numPrintCnt.SetInputFields("#", , 1, 1, , 0, 0, , Convert.ToDecimal(9), Convert.ToDecimal(0))

        Me._Frm.numPrintCnt.TextValue = "1"

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        Dim noMnb As Boolean = True
        Dim dtTori As Boolean = True

        With Me._Frm


        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .grpSelect.Focus()
            .txtCustCdL.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm
            .cmbBr.SelectedValue = String.Empty
            .txtCustCdL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .txtCustCdS.TextValue = String.Empty
            .txtCustCdSs.TextValue = String.Empty
            .lblCustNm.TextValue = String.Empty
            .txtSekySaki.TextValue = String.Empty
            .lblSeqtoNm.TextValue = String.Empty
            .cmbSimebi.SelectedValue = String.Empty
            .imdInvDate.TextValue = String.Empty
            .chkSelectByNrsB.Checked = True
            .cmbPrint.SelectedValue = "01"
            .chkMeisaiPrev.Checked = False
            .numPrintCnt.TextValue = "0"

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定（荷主）
    ''' </summary>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadDataCust(ByVal row As Integer)

        With Me._Frm
            .txtCustCdL.TextValue = .sprMeisai.ActiveSheet.Cells(row, LMG020G.sprMeisaiDef.CUST_CD_L.ColNo).Text
            .txtCustCdM.TextValue = .sprMeisai.ActiveSheet.Cells(row, LMG020G.sprMeisaiDef.CUST_CD_M.ColNo).Text
            .txtCustCdS.TextValue = .sprMeisai.ActiveSheet.Cells(row, LMG020G.sprMeisaiDef.CUST_CD_S.ColNo).Text
            .txtCustCdSs.TextValue = .sprMeisai.ActiveSheet.Cells(row, LMG020G.sprMeisaiDef.CUST_CD_SS.ColNo).Text
            .lblCustNm.TextValue = .sprMeisai.ActiveSheet.Cells(row, LMG020G.sprMeisaiDef.CUST_NM.ColNo).Text
        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定（請求先）
    ''' </summary>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadDataSeiq(ByVal row As Integer)

        Dim Seiqdr As DataRow() = Nothing
        With Me._Frm
            .txtSekySaki.TextValue = .sprMeisai.ActiveSheet.Cells(row, LMG020G.sprMeisaiDef.OYA_SEIQTO_CD.ColNo).Text
            'Seiqdr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SEIQTO).Select(String.Concat("SEIQTO_CD = ", " '", .txtSekySaki.TextValue, "' "))

            '20160930 要番2622 tsunehira add
            Seiqdr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SEIQTO).Select(String.Concat("SEIQTO_CD = ", " '", .txtSekySaki.TextValue, "' ", " AND ", "NRS_BR_CD = ", " '", .cmbBr.SelectedValue.ToString(), "' ", " AND ", " SYS_DEL_FLG = '0' "))

            If Seiqdr.Count < 1 = False Then
                .lblSeqtoNm.TextValue = Seiqdr(0).Item("SEIQTO_NM").ToString()
            End If
        End With

    End Sub

    ''' <summary>
    ''' コントロールの背景色の初期化
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetBackColor(ByVal frm As LMG020F)

        With Me._Frm
            Me._ControlG.SetBackColor(.txtCustCdL)
            Me._ControlG.SetBackColor(.txtCustCdM)
            Me._ControlG.SetBackColor(.txtCustCdS)
            Me._ControlG.SetBackColor(.txtCustCdSs)
            Me._ControlG.SetBackColor(.txtSekySaki)
            Me._ControlG.SetBackColor(.cmbBr)
            Me._ControlG.SetBackColor(.cmbSimebi)
            Me._ControlG.SetBackColor(.imdInvDate)
            Me._ControlG.SetBackColor(.cmbPrint)
            Me._ControlG.SetBackColor(.numPrintCnt)
        End With

    End Sub

    ''' <summary>
    ''' 荷主名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetCustSeqName()

        Dim cust_nm_l As String = String.Empty
        Dim cust_nm_m As String = String.Empty
        Dim cust_nm_s As String = String.Empty
        Dim cust_nm_ss As String = String.Empty
        Dim cust_nm_sum As String = String.Empty
        Dim custcdl As String = Me._Frm.txtCustCdL.TextValue
        Dim custcdm As String = Me._Frm.txtCustCdM.TextValue
        Dim custcds As String = Me._Frm.txtCustCdS.TextValue
        Dim custcdss As String = Me._Frm.txtCustCdSs.TextValue
        Dim nrsbrcd As String = Me._Frm.cmbBr.SelectedValue.ToString  '20160927 要番2622 tsunehira add 


        With Me._Frm
            Dim cust As ArrayList = Me._ControlG.GetCustNm(NrsBrCd, custcdl, custcdm, custcds, custcdss)

            'リストが存在する場合、フォームにデータを設定する。
            If cust.Count >= 1 = True Then
                '2011/08/01 菱刈 取得項目変更  スタート
                '荷主コード(大)が入力されているとき
                If String.IsNullOrEmpty(custcdl) = False Then
                    '荷主名称と荷主コードを設定
                    .txtCustCdL.TextValue = cust(2).ToString()
                    cust_nm_l = cust(6).ToString()
                End If
                '荷主コード(中)に値が入力されている時
                If String.IsNullOrEmpty(custcdm) = False Then
                    '荷主名称と荷主コード(中)を設定
                    .txtCustCdM.TextValue = cust(3).ToString()
                    cust_nm_m = cust(7).ToString()
                End If

                '2011/08/02 菱刈 荷主コード(大)の条件を追加 スタート
                '荷主コード(小)が設定されているとき
                If String.IsNullOrEmpty(custcds) = False AndAlso _
                String.IsNullOrEmpty(custcdl) = True Then
                    '荷主名称と、荷主コード(小)を設定
                    .txtCustCdS.TextValue = cust(4).ToString()
                    cust_nm_s = cust(8).ToString()
                Else
                    '荷主コード(小)が設定されていて、荷主コード(大)が設定されていない場合
                    .txtCustCdS.TextValue = String.Empty
                End If

                '荷主コード(極小)が設定されているとき
                If String.IsNullOrEmpty(custcdss) = False AndAlso _
                String.IsNullOrEmpty(custcdl) = True Then
                    '荷主名称と、荷主コード(極小)を設定
                    .txtCustCdSs.TextValue = cust(5).ToString()
                    cust_nm_ss = cust(9).ToString()
                Else
                    '荷主コード(小)が設定されていて、荷主コード(大)が設定されていない場合
                    .txtCustCdSs.TextValue = String.Empty
                End If
                '2011/08/02 菱刈 荷主コード(大)の条件を追加 スタート

                cust_nm_sum = String.Concat(cust_nm_l, " ", cust_nm_m, " ", cust_nm_s, " ", cust_nm_ss)
                '連結した文字列をラベルに設定
                .lblCustNm.TextValue = cust_nm_sum

                '既存のコメント化
                '.lblCustNm.TextValue = String.Concat(cust(0).ToString(), " ", cust(1).ToString())
                '.txtCustCdL.TextValue = cust(2).ToString()
                '.txtCustCdM.TextValue = cust(3).ToString()
                '.txtCustCdS.TextValue = cust(4).ToString()
                '.txtCustCdSs.TextValue = cust(5).ToString()

                '2011/08/01 菱刈 項目取得 エンド

            End If

        End With

        '請求先名称の取得
        SetSeqName()

    End Sub

    ''' <summary>
    ''' 請求先名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSeqName()

        With Me._Frm
            If String.IsNullOrEmpty(.txtSekySaki.TextValue) = False Then
                'Dim Seq As ArrayList = Me._ControlG.GetSeqNm(.txtSekySaki.TextValue)
                '20160927 要番2622 tsunehira add Start
                Dim Seq As ArrayList = Me._ControlG.GetSeqNm(.cmbBr.SelectedValue.ToString, .txtSekySaki.TextValue)
                '20160927 要番2622 tsunehira add End

                'リストが存在する場合、フォームにデータを設定する。
                If Seq.Count >= 1 = True Then

                    '.lblSeqtoNm.TextValue = Me._ControlG.GetSeqNm(.txtSekySaki.TextValue)(0).ToString()
                    '.txtSekySaki.TextValue = Me._ControlG.GetSeqNm(.txtSekySaki.TextValue)(1).ToString()
                    '20160927 要番2622 tsunehira add Start
                    .lblSeqtoNm.TextValue = Me._ControlG.GetSeqNm(.cmbBr.SelectedValue.ToString, .txtSekySaki.TextValue)(0).ToString()
                    .txtSekySaki.TextValue = Me._ControlG.GetSeqNm(.cmbBr.SelectedValue.ToString, .txtSekySaki.TextValue)(1).ToString()
                    '20160927 要番2622 tsunehira add End

                End If
            End If
        End With

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 日付を表示するコントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateControl()

        With Me._Frm

            Call Me.SetDateFormat(.imdInvDate)

        End With

    End Sub

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <param name="ctl">書式設定を行うコントロール</param>
    ''' <remarks></remarks>
    Private Sub SetDateFormat(ByVal ctl As LMImDate)

        ctl.Format = DateFieldsBuilder.BuildFields("yyyyMM")
        ctl.DisplayFormat = DateDisplayFieldsBuilder.BuildFields("yyyy/MM")

    End Sub

#End Region

#End Region

#Region "検索結果表示"

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Public Sub SetSelectListData(ByVal ds As DataSet)

        '参考値の設定
        Call Me.SetSpread(ds)

        'Dim getDt As DataTable = ds.Tables(LMG020C.TABLE_NM_OUT)

        'Me._Frm.txtMemno.TextValue = getDt.Rows(0).Item("KMMID").ToString

        'For index As Integer = 0 To (getDt.Rows().Count - 1)

        '    Dim strKNAPTA As String = getDt.Rows(index).Item("KNAPTA").ToString

        '    Select Case Mid(strKNAPTA, 1, 1)

        '        Case "1"
        '            '取得データをヘッダ部に表示
        '            Call SetHeaderData(strKNAPTA, "1")
        '        Case "2"
        '            '取得データをSpreadに表示
        '            Call Me.SetSpread(strKNAPTA)
        '        Case "8"
        '            '取得データをヘッダ部に表示
        '            Call SetHeaderData(strKNAPTA, "8")

        '    End Select
        'Next

    End Sub

    ''' <summary>
    ''' 検索結果ヘッダー部表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHeaderData(ByVal strKNAPTA As String, ByVal strKBN As String)

        With Me._Frm

            'Select Case strKBN
            '    Case "1"
            '        .txtType.TextValue = Get_Shubetu(Mid(strKNAPTA, 2, 2))      
            '        .txtHurikomi.TextValue = Trim(Mid(strKNAPTA, 2, 10))        
            '        .txtHurikomiNm.TextValue = Trim(Mid(strKNAPTA, 15, 40))     
            '        .txtTorikumi.TextValue = Trim(Mid(strKNAPTA, 55, 4))        
            '        .txtBankno.TextValue = Trim(Mid(strKNAPTA, 59, 4))          
            '        .txtBankNm.TextValue = Trim(Mid(strKNAPTA, 63, 15))         
            '        .txtShitenno.TextValue = Trim(Mid(strKNAPTA, 78, 3))        
            '        .txtShitenNm.TextValue = Trim(Mid(strKNAPTA, 81, 15))       
            '        .txtYokinsyu.TextValue = Get_Yokin(Mid(strKNAPTA, 96, 1))   
            '        .txtKozabango.TextValue = Trim(Mid(strKNAPTA, 97, 7))       
            '    Case "8"
            '        .txtTotalcnt.Value = CInt(Trim(Mid(strKNAPTA, 2, 6)))       
            '        .txtTotalKin.Value = CDec(Trim(Mid(strKNAPTA, 8, 12)))      
            'End Select

        End With

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprMeisaiDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMG020C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SEIKYU_DATE As SpreadColProperty = New SpreadColProperty(LMG020C.SprColumnIndex.INV_DATE, "請求日", 90, True)
        Public Shared SIME_DATE As SpreadColProperty = New SpreadColProperty(LMG020C.SprColumnIndex.SHIMEBI, "締日", 80, True)
        Public Shared CUST_CD As SpreadColProperty = New SpreadColProperty(LMG020C.SprColumnIndex.CUST_CD, "荷主コード", 120, True)
        Public Shared CUST_NM As SpreadColProperty = New SpreadColProperty(LMG020C.SprColumnIndex.CUST_NM, "荷主名", 450, True)
        Public Shared SIKYU_JOB_NO As SpreadColProperty = New SpreadColProperty(LMG020C.SprColumnIndex.JOB_NO, "請求JOB番号", 100, True)
        Public Shared CREATE_DATE As SpreadColProperty = New SpreadColProperty(LMG020C.SprColumnIndex.CREATE_DATE, "作成日", 100, True)
        Public Shared CREATE_USER_NM As SpreadColProperty = New SpreadColProperty(LMG020C.SprColumnIndex.CREATE_USER, "作成者", 120, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMG020C.SprColumnIndex.CUST_CD_L, "荷主コードL", 90, False)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMG020C.SprColumnIndex.CUST_CD_M, "荷主コードM", 20, False)
        Public Shared CUST_CD_S As SpreadColProperty = New SpreadColProperty(LMG020C.SprColumnIndex.CUST_CD_S, "荷主コードS", 20, False)
        Public Shared CUST_CD_SS As SpreadColProperty = New SpreadColProperty(LMG020C.SprColumnIndex.CUST_CD_SS, "荷主コードSS", 20, False)
        '2011/08/18 菱刈 クローズ区分をコメント化 スタート
        'Public Shared CLOSE_KB As SpreadColProperty = New SpreadColProperty(LMG020C.SprColumnIndex.CLOSE_KB, "締日区分", 20, False)
        '2011/08/18 菱刈 クローズ区分をコメント化 エンド
        Public Shared OYA_SEIQTO_CD As SpreadColProperty = New SpreadColProperty(LMG020C.SprColumnIndex.OYA_SEIQTO_CD, "請求先コード", 50, False)
        Public Shared SEKY_FLG As SpreadColProperty = New SpreadColProperty(LMG020C.SprColumnIndex.SEKY_FLG, "請求フラグ", 50, False)
        Public Shared CUST_NM_L_M As SpreadColProperty = New SpreadColProperty(LMG020C.SprColumnIndex.CUST_NM_L_M, "荷主名(大・中)", 320, False)
        Public Shared SYS_ENT_PGID As SpreadColProperty = New SpreadColProperty(LMG020C.SprColumnIndex.SYS_ENT_PGID, "作成プログラムＩＤ", 50, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        Dim spr As LMSpreadSearch = Me._Frm.sprMeisai

        With spr

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = 16

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprSagyo.SetColProperty(New sprDetailDef)
            .SetColProperty(New LMG020G.sprMeisaiDef(), False)
            '2015.10.15 英語化対応END

            'スプレッドの列設定（列名、列幅、列の表示・非表示）  
            '.SetColProperty(New sprMeisaiDef)

            '列固定位置を設定します。(ex.ユーザー名で固定)
            'spr.ActiveSheet.FrozenColumnCount = sprDetailDef.CUST_NM.ColNo + 1

            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)

            '列設定
            .SetCellStyle(0, sprMeisaiDef.SEIKYU_DATE.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiDef.SIME_DATE.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiDef.CUST_CD.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiDef.CUST_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 240, False))
            .SetCellStyle(0, sprMeisaiDef.SIKYU_JOB_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 10, False))
            .SetCellStyle(0, sprMeisaiDef.CREATE_DATE.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiDef.CREATE_USER_NM.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiDef.CUST_CD_L.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiDef.CUST_CD_M.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiDef.CUST_CD_S.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiDef.CUST_CD_SS.ColNo, lbl)
            '2011/08/18 菱刈 クローズ区分をコメント化 スタート
            '.SetCellStyle(0, sprMeisaiDef.CLOSE_KB.ColNo, lbl)
            '2011/08/18 菱刈 クローズ区分をコメント化 エンド
            .SetCellStyle(0, sprMeisaiDef.OYA_SEIQTO_CD.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiDef.SEKY_FLG.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiDef.CUST_NM_L_M.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiDef.SYS_ENT_PGID.ColNo, lbl)

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMG020F)

        With frm.sprMeisai

            .Sheets(0).Cells(0, sprMeisaiDef.SEIKYU_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprMeisaiDef.SIME_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprMeisaiDef.CUST_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprMeisaiDef.CUST_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprMeisaiDef.SIKYU_JOB_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprMeisaiDef.CREATE_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprMeisaiDef.CREATE_USER_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprMeisaiDef.CUST_CD_L.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprMeisaiDef.CUST_CD_M.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprMeisaiDef.CUST_CD_S.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprMeisaiDef.CUST_CD_SS.ColNo).Value = String.Empty
            '2011/08/18 菱刈 クローズ区分をコメント化 スタート
            '.Sheets(0).Cells(0, sprMeisaiDef.CLOSE_KB.ColNo).Value = String.Empty
            '2011/08/18 菱刈 クローズ区分をコメント化 エンド
            .Sheets(0).Cells(0, sprMeisaiDef.OYA_SEIQTO_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprMeisaiDef.SEKY_FLG.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprMeisaiDef.CUST_NM_L_M.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprMeisaiDef.SYS_ENT_PGID.ColNo).Value = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet)

        Dim spr As LMSpreadSearch = Me._Frm.sprMeisai
        Dim dtOut As New DataSet
        Dim sekyflg As String = String.Empty

        With spr

            If Me._Frm.optSeikyuC.Checked = True Then
                sekyflg = "01"
            ElseIf Me._Frm.optSeikyuH.Checked = True Then
                sekyflg = "00"
            End If

            .SuspendLayout()
            .Sheets(0).Rows.Count = 1
            'データ挿入
            '行数設定
            Dim tbl As DataTable = ds.Tables(LMG020C.TABLE_NM_OUT)
            Dim lngcnt As Integer = tbl.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If
            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)

            Dim dr As DataRow
            Dim CUST_CD As String = String.Empty
            Dim CUST_NM_LM As String = String.Empty
            '値設定
            For i As Integer = 1 To lngcnt

                dr = tbl.Rows(i - 1)

                '荷主コードの設定
                CUST_CD = String.Concat(dr.Item("CUST_CD_L").ToString() _
                                        , "-", dr.Item("CUST_CD_M").ToString() _
                                        , "-", dr.Item("CUST_CD_S").ToString() _
                                        , "-", dr.Item("CUST_CD_SS").ToString())

                CUST_NM_LM = String.Concat(dr.Item("CUST_NM_L").ToString() _
                                          , " ", dr.Item("CUST_NM_M").ToString())
                'セルスタイル設定
                .SetCellStyle(i, sprMeisaiDef.DEF.ColNo, def)
                .SetCellStyle(i, sprMeisaiDef.SEIKYU_DATE.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiDef.SIME_DATE.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiDef.CUST_CD.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiDef.CUST_NM.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiDef.SIKYU_JOB_NO.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiDef.CREATE_DATE.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiDef.CREATE_USER_NM.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiDef.CUST_CD_L.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiDef.CUST_CD_M.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiDef.CUST_CD_S.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiDef.CUST_CD_SS.ColNo, lbl)
                '2011/08/18 菱刈  締日区分のコメント化 スタート
                '.SetCellStyle(i, sprMeisaiDef.CLOSE_KB.ColNo, lbl)
                '2011/08/18 菱刈  締日区分のコメント化 エンド
                .SetCellStyle(i, sprMeisaiDef.OYA_SEIQTO_CD.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiDef.SEKY_FLG.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiDef.CUST_NM_L_M.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiDef.SYS_ENT_PGID.ColNo, lbl)

                'セルに値を設定
                .SetCellValue(i, sprMeisaiDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprMeisaiDef.SEIKYU_DATE.ColNo, _
                             DateFormatUtility.EditSlash(dr.Item("INV_DATE_TO").ToString()))
                '.SetCellValue(i, sprMeisaiDef.SIME_DATE.ColNo, dr.Item("CLOSE_NM").ToString())
                .SetCellValue(i, sprMeisaiDef.SIME_DATE.ColNo, Me._Frm.cmbSimebi.TextValue)

                .SetCellValue(i, sprMeisaiDef.CUST_CD.ColNo, CUST_CD)
                .SetCellValue(i, sprMeisaiDef.CUST_NM.ColNo, dr.Item("CUST_NM").ToString())
                .SetCellValue(i, sprMeisaiDef.SIKYU_JOB_NO.ColNo, dr.Item("JOB_NO").ToString())
                .SetCellValue(i, sprMeisaiDef.CREATE_DATE.ColNo, _
                              DateFormatUtility.EditSlash(dr.Item("SYS_ENT_DATE").ToString()))
                .SetCellValue(i, sprMeisaiDef.CREATE_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, sprMeisaiDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, sprMeisaiDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, sprMeisaiDef.CUST_CD_S.ColNo, dr.Item("CUST_CD_S").ToString())
                .SetCellValue(i, sprMeisaiDef.CUST_CD_SS.ColNo, dr.Item("CUST_CD_SS").ToString())
                '2011/08/18 菱刈  締日区分のコメント化 スタート
                '.SetCellValue(i, sprMeisaiDef.CLOSE_KB.ColNo, dr.Item("CLOSE_KB").ToString())
                '2011/08/18 菱刈  締日区分のコメント化 エンド
                .SetCellValue(i, sprMeisaiDef.OYA_SEIQTO_CD.ColNo, dr.Item("OYA_SEIQTO_CD").ToString())
                .SetCellValue(i, sprMeisaiDef.SEKY_FLG.ColNo, sekyflg)
                .SetCellValue(i, sprMeisaiDef.CUST_NM_L_M.ColNo, CUST_NM_LM)
                .SetCellValue(i, sprMeisaiDef.SYS_ENT_PGID.ColNo, dr.Item("SYS_ENT_PGID").ToString())
            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region

#End Region

End Class
