' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD080  : 荷主システム在庫数と日陸在庫数との照合
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Microsoft.VisualBasic.FileIO
Imports Microsoft.Office.Interop

''' <summary>
''' LMD080Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD080G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD080F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMD080V

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconG As LMDControlG

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconV As LMDControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMD080F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Gamen共通クラスの設定
        Me._LMDconG = New LMDControlG(MyBase.MyHandler, DirectCast(frm, Form))

        'Validate共通クラスの設定
        _LMDconV = New LMDControlV(handlerClass, DirectCast(frm, Form))

        'Validateクラスの設定
        Me._V = New LMD080V(handlerClass, frm, _LMDconV)

    End Sub

#End Region 'Constructor

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
            .F1ButtonName = LMDControlC.FUNCTION_CHECK
            .F2ButtonName = LMDControlC.FUNCTION_TORIKOMI
            .F3ButtonName = LMDControlC.FUNCTION_SHUKEI
            .F4ButtonName = LMDControlC.FUNCTION_SHOGO
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = LMDControlC.FUNCTION_POP
            .F11ButtonName = String.Empty
            .F12ButtonName = LMDControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = always
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = lock
            .F10ButtonEnabled = always
            .F11ButtonEnabled = lock
            .F12ButtonEnabled = always

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region 'FunctionKey

#Region "Mode&Status"

    ''' <summary>
    ''' Dispモードとレコードステータスの設定
    ''' </summary>
    ''' <param name="mode">Dispモード</param>
    ''' <param name="status">レコードステータス</param>
    ''' <remarks></remarks>
    Friend Sub SetModeAndStatus(ByVal mode As String, ByVal status As String)

        With Me._Frm

        End With

    End Sub

#End Region 'Mode&Status

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .pnlShogoTaisho.TabIndex = LMD080C.CtlTabIndex.PNLSHOGOHTAISHO
            .cmbEigyo.TabIndex = LMD080C.CtlTabIndex.EIGYO
            .txtCustCdL.TabIndex = LMD080C.CtlTabIndex.CUSTCDL
            .lblCustNmL.TabIndex = LMD080C.CtlTabIndex.CUSTNML
            .txtCustCdM.TabIndex = LMD080C.CtlTabIndex.CUSTCDM
            .lblCustNmM.TabIndex = LMD080C.CtlTabIndex.CUSTNMM
            .imdJisshiDate.TabIndex = LMD080C.CtlTabIndex.JISSHIDATE
            .cmbLayout.TabIndex = LMD080C.CtlTabIndex.LAYOUT
            .pnlTorikomi.TabIndex = LMD080C.CtlTabIndex.PNLTORIKOMI
            .btnCheck.TabIndex = LMD080C.CtlTabIndex.BTNCHECK
            .lblFolder.TabIndex = LMD080C.CtlTabIndex.FOLDER
            .lblFile.TabIndex = LMD080C.CtlTabIndex.FILENM
            .btnTorikomi.TabIndex = LMD080C.CtlTabIndex.BTNTORIKOMI
            .pnlShukei.TabIndex = LMD080C.CtlTabIndex.PNLSHUKEI
            .btnShukei.TabIndex = LMD080C.CtlTabIndex.BTNSHUKEI
            .pnlShogo.TabIndex = LMD080C.CtlTabIndex.PNLSHOGOH
            .pnlShogoKey.TabIndex = LMD080C.CtlTabIndex.PNLSHOGOHKEY
            .chkGoodsCdCust.TabIndex = LMD080C.CtlTabIndex.CHKCUSTCD
            .chkLotNo.TabIndex = LMD080C.CtlTabIndex.CHKLOTNO
            .chkSerialNo.TabIndex = LMD080C.CtlTabIndex.CHKSERIALNO
            .chkIrime.TabIndex = LMD080C.CtlTabIndex.CHKIRIME
            .chkIrimeUt.TabIndex = LMD080C.CtlTabIndex.CHKIRIMEUT
            .chkWriteFlg.TabIndex = LMD080C.CtlTabIndex.CHKWRITEFLG
            .btnShogo.TabIndex = LMD080C.CtlTabIndex.BTNSHOGO

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal sysDate As String)

        With Me._Frm
            '自営業所の設定
            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            .cmbEigyo.SelectedValue = brCd

            'システム日付の設定
            .imdJisshiDate.TextValue = sysDate

            '取込フォルダ・ファイルの設定
            Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'F002' AND ", _
                                                                                                            "KBN_NM1 = '", .cmbEigyo.SelectedValue, "'"))
            If 0 < kbnDr.Length Then
                .lblFolder.TextValue = kbnDr(0).Item("KBN_NM2").ToString
                .lblFile.TextValue = kbnDr(0).Item("KBN_NM3").ToString
            End If

            '照合キー(荷主商品コード)の設定
            .chkGoodsCdCust.Checked = True
            .chkGoodsCdCust.Enabled = False

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        With Me._Frm

            If (.txtCustCdL.TextValue).Equals(.txtCustCdLOld.TextValue) = False OrElse _
                (.txtCustCdM.TextValue).Equals(.txtCustCdMOld.TextValue) = False OrElse _
                (String.IsNullOrEmpty(.txtCustCdL.TextValue) = True AndAlso _
                 String.IsNullOrEmpty(.txtCustCdM.TextValue) = True) Then
                '荷主コードが変わった場合は初期化
                .FunctionKey.F2ButtonEnabled = False
                .FunctionKey.F3ButtonEnabled = False
                .FunctionKey.F4ButtonEnabled = False
                .btnTorikomi.Enabled = False
                .btnShukei.Enabled = False
                .btnShogo.Enabled = False
                .txtCustCdLOld.TextValue = .txtCustCdL.TextValue
                .txtCustCdMOld.TextValue = .txtCustCdM.TextValue
            End If

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .txtCustCdL.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

        End With

    End Sub

    ''' <summary>
    ''' チェック処理後のコントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatusCheck(ByVal ds As DataSet)

        Dim ZaiShoFlg As Boolean = False
        Dim zaiZanFlg As Boolean = False
        Dim ZaiShoSumFlg As Boolean = False

        With Me._Frm

            If 0 < ds.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUST).Rows.Count Then
                '荷主在庫数データにデータがある場合
                ZaiShoFlg = True
            End If
            If 0 < ds.Tables(LMD080C.TABLE_NM_INOUT_ZAIZAN).Rows.Count Then
                '月末在庫データがある場合
                zaiZanFlg = True
            End If
            If 0 < ds.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUSTSUM).Rows.Count Then
                '荷主在庫数データサマリにデータがある場合
                ZaiShoSumFlg = True
            End If

            .FunctionKey.F2ButtonEnabled = True
            .btnTorikomi.Enabled = True

            If zaiZanFlg = True Then
                .FunctionKey.F3ButtonEnabled = True
                .btnShukei.Enabled = True
            Else
                .FunctionKey.F3ButtonEnabled = False
                .btnShukei.Enabled = False
            End If

            If ZaiShoFlg = True AndAlso zaiZanFlg = True AndAlso ZaiShoSumFlg = True Then
                .FunctionKey.F4ButtonEnabled = True
                .btnShogo.Enabled = True
            Else
                .FunctionKey.F4ButtonEnabled = False
                .btnShogo.Enabled = False
            End If

        End With

    End Sub

    ''' <summary>
    ''' 取込処理後のコントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatusTorikomi()

        With Me._Frm

            If .FunctionKey.F3ButtonEnabled = True Then
                '集計ボタンが押下可能の場合
                .FunctionKey.F4ButtonEnabled = True
                .btnShogo.Enabled = True
            End If

        End With

    End Sub

    ''' <summary>
    ''' 集計処理後のコントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatusShukei()

        With Me._Frm

            .FunctionKey.F4ButtonEnabled = True
            .btnShogo.Enabled = True

        End With

    End Sub

    ''' <summary>
    ''' 荷主在庫レイアウト変更後のコントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatusLayout()

        With Me._Frm

            .FunctionKey.F2ButtonEnabled = False
            .FunctionKey.F3ButtonEnabled = False
            .FunctionKey.F4ButtonEnabled = False
            .btnTorikomi.Enabled = False
            .btnShukei.Enabled = False
            .btnShogo.Enabled = False
            
        End With

    End Sub

    ''' <summary>
    ''' 照合キーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetShogohKey(ByVal frm As LMD080F, ByVal ds As DataSet)

        Dim rowNo As Integer = frm.cmbLayout.SelectedIndex - 1

        frm.chkLotNo.Checked = False
        frm.chkSerialNo.Checked = False
        frm.chkIrime.Checked = False
        frm.chkIrimeUt.Checked = False
        frm.chkWriteFlg.Checked = False

        If rowNo < 0 Then
            '空が選択されている場合は処理終了
            Exit Sub
        End If

        'ロット№
        If (LMConst.FLG.ON).Equals(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("DEF_LOT_NO").ToString) = True Then
            frm.chkLotNo.Checked = True
        End If

        'シリアル№
        If (LMConst.FLG.ON).Equals(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("DEF_SERIAL_NO").ToString) = True Then
            frm.chkSerialNo.Checked = True
        End If

        '入目
        If (LMConst.FLG.ON).Equals(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("DEF_IRIME").ToString) = True Then
            frm.chkIrime.Checked = True
        End If

        '入目単位
        If (LMConst.FLG.ON).Equals(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("DEF_IRIME_UT").ToString) = True Then
            frm.chkIrimeUt.Checked = True
        End If

        '個数・数量が一致した在庫は出力しない
        If (LMConst.FLG.ON).Equals(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("DEF_EQ").ToString) = True Then
            frm.chkWriteFlg.Checked = True
        End If

    End Sub

    ''' <summary>
    ''' 荷主在庫レイアウトの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetComboLayout(ByVal frm As LMD080F, ByVal ds As DataSet)

        Dim max As Integer = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows.Count - 1

        frm.cmbLayout.Items.Add(String.Empty) '空を追加
        For i As Integer = 0 To max
            frm.cmbLayout.Items.Add(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(i).Item("SHOGOH_NAME").ToString) 'マスタ設定値を追加
        Next
        frm.cmbLayout.SelectedIndex = 0

    End Sub

#End Region

#Region "Spread"

#End Region

#Region "ユーティリティ"

#Region "プロパティ"

#End Region

#Region "ファイル取込"

#Region "ファイル取込メイン"

    ''' <summary>
    ''' ファイル取込メイン
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Function GetFileData(ByVal frm As LMD080F, ByVal ds As DataSet, ByRef rtDs As DataSet) As Boolean

        Dim rtnFlg As Boolean = False

        'EXCELファイルの場合
        rtnFlg = Me.GetFileDataExcel(frm, ds, rtDs)

        Return rtnFlg

    End Function

#End Region

#Region "ファイル取込(EXCEL)"

    ''' <summary>
    ''' ファイル取込(EXCEL)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Function GetFileDataExcel(ByVal frm As LMD080F, ByVal ds As DataSet, ByRef rtDs As DataSet) As Boolean

        Dim rowNo As Integer = frm.cmbLayout.SelectedIndex - 1

        Dim folderNm As String = frm.lblFolder.TextValue
        Dim fileNm As String = frm.lblFile.TextValue

        Dim xlApp As Excel.Application
        Dim xlBooks As Excel.Workbooks
        Dim xlBook As Excel.Workbook
        Dim xlSheet As Excel.Worksheet
        Dim xlCell As Excel.Range = Nothing

        xlApp = New Excel.Application()
        xlBooks = xlApp.Workbooks
        xlBook = xlBooks.Open(String.Concat(folderNm, fileNm))
        xlSheet = DirectCast(xlBook.Worksheets(1), Excel.Worksheet)

        xlApp.Visible = False

        Dim irimeWk As Decimal = 0
        Dim nbWk As Decimal = 0
        Dim qtWk As Decimal = 0

        'EXCEL取込
        '■マスタに設定されているEXCELの列番号を取得
        '見出し行数
        Dim headerLine As Integer = Convert.ToInt32(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("HEADER_LINE").ToString)
        'スキップ列1
        Dim colSkip1 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_SKIP_1").ToString)
        'スキップ列2
        Dim colSkip2 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_SKIP_2").ToString)
        'スキップ列3
        Dim colSkip3 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_SKIP_3").ToString)
        'スキップ列4
        Dim colSkip4 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_SKIP_4").ToString)
        'スキップ列5
        Dim colSkip5 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_SKIP_5").ToString)
        '商品コード1
        Dim colCustGoodsCd1 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_CUST_GOODS_CD_1").ToString)
        '商品コード2
        Dim colCustGoodsCd2 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_CUST_GOODS_CD_2").ToString)
        '商品コード3
        Dim colCustGoodsCd3 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_CUST_GOODS_CD_3").ToString)
        '商品名1
        Dim colGoodsNm1 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_GOODS_NM_1").ToString)
        '商品名2
        Dim colGoodsNm2 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_GOODS_NM_2").ToString)
        '商品名3
        Dim colGoodsNm3 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_GOODS_NM_3").ToString)
        'ロット番号
        Dim colLotNo As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_LOT_NO").ToString)
        'シリアル番号
        Dim colSerialNo As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_SERIAL_NO").ToString)
        '入目
        Dim colIrime As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_IRIME").ToString)
        '入目単位
        Dim colIrimeUt As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_IRIME_UT").ToString)
        '分類項目1
        Dim colclass1 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_CLASS_1").ToString)
        '分類項目2
        Dim colclass2 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_CLASS_2").ToString)
        '分類項目3
        Dim colclass3 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_CLASS_3").ToString)
        '分類項目4
        Dim colclass4 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_CLASS_4").ToString)
        '分類項目5
        Dim colclass5 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_CLASS_5").ToString)
        '赤黒区分
        Dim rbKb As Integer = 0
        Dim wkRbKb As Integer = 1
        If ("1").Equals(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("RB_KB").ToString) = True Then
            wkRbKb = -1
        End If
        '赤データ区分1
        Dim colRb1 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_RB_1").ToString)
        Dim dataRb1 As String = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("DATA_RB_1").ToString
        '赤データ区分2
        Dim colRb2 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_RB_2").ToString)
        Dim dataRb2 As String = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("DATA_RB_2").ToString
        '赤データ区分3
        Dim colRb3 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_RB_3").ToString)
        Dim dataRb3 As String = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("DATA_RB_3").ToString
        '個数
        Dim colNb As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_NB").ToString)
        '数量
        Dim colQt As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_QT").ToString)
        '数値01
        Dim colFreeN1 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_N01").ToString)
        '数値02
        Dim colFreeN2 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_N02").ToString)
        '数値03
        Dim colFreeN3 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_N03").ToString)
        '数値04
        Dim colFreeN4 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_N04").ToString)
        '数値05
        Dim colFreeN5 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_N05").ToString)
        '数値06
        Dim colFreeN6 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_N06").ToString)
        '数値07
        Dim colFreeN7 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_N07").ToString)
        '数値08
        Dim colFreeN8 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_N08").ToString)
        '数値09
        Dim colFreeN9 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_N09").ToString)
        '数値10
        Dim colFreeN10 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_N10").ToString)
        '文字01
        Dim colFreeC1 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_C01").ToString)
        '文字02
        Dim colFreeC2 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_C02").ToString)
        '文字03
        Dim colFreeC3 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_C03").ToString)
        '文字04
        Dim colFreeC4 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_C04").ToString)
        '文字05
        Dim colFreeC5 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_C05").ToString)
        '文字06
        Dim colFreeC6 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_C06").ToString)
        '文字07
        Dim colFreeC7 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_C07").ToString)
        '文字08
        Dim colFreeC8 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_C08").ToString)
        '文字09
        Dim colFreeC9 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_C09").ToString)
        '文字10
        Dim colFreeC10 As Integer = Me.getColNo(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("COL_FREE_C10").ToString)

        Dim dr As DataRow = rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUST).NewRow()
        Dim rowCnt As Integer = 1
        Dim gyoNo As Integer = 1
        Do
            'ヘッダーの場合は取り込まない
            If headerLine >= rowCnt Then
                rowCnt = rowCnt + 1
                Continue Do
            End If

            'スキップ列1の判定
            If colSkip1 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colSkip1), Excel.Range)
                If (ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("DATA_SKIP_1").ToString).Equals(Convert.ToString(xlCell.Value)) = True Then
                    rowCnt = rowCnt + 1
                    Continue Do
                End If
            End If

            'スキップ列2の判定
            If colSkip2 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colSkip2), Excel.Range)
                If (ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("DATA_SKIP_2").ToString).Equals(Convert.ToString(xlCell.Value)) = True Then
                    rowCnt = rowCnt + 1
                    Continue Do
                End If
            End If

            'スキップ列3の判定
            If colSkip3 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colSkip3), Excel.Range)
                If (ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("DATA_SKIP_3").ToString).Equals(Convert.ToString(xlCell.Value)) = True Then
                    rowCnt = rowCnt + 1
                    Continue Do
                End If
            End If

            'スキップ列4の判定
            If colSkip4 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colSkip4), Excel.Range)
                If (ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("DATA_SKIP_4").ToString).Equals(Convert.ToString(xlCell.Value)) = True Then
                    rowCnt = rowCnt + 1
                    Continue Do
                End If
            End If

            'スキップ列5の判定
            If colSkip5 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colSkip5), Excel.Range)
                If (ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("DATA_SKIP_5").ToString).Equals(Convert.ToString(xlCell.Value)) = True Then
                    rowCnt = rowCnt + 1
                    Continue Do
                End If
            End If

            xlCell = DirectCast(xlSheet.Cells(rowCnt, colCustGoodsCd1), Excel.Range)
            If String.IsNullOrEmpty(Convert.ToString(xlCell.Value)) = True Then
                '商品コードが空の場合、処理終了
                Exit Do
            End If

            dr = rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUST).NewRow()

            '■INのDataSetに値を設定(主にExcelの値を設定)
            '営業所コード
            dr.Item("NRS_BR_CD") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("NRS_BR_CD").ToString
            '荷主コード(大)
            dr.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            '荷主コード(中)
            dr.Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            '照合日
            dr.Item("CHECK_DATE") = frm.imdJisshiDate.TextValue
            '行番号
            dr.Item("GYO_NO") = Me.MaeCoverData(Convert.ToString(gyoNo), "0", 5)
            '枝番号
            dr.Item("EDA_NO") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("EDA_NO").ToString
            'ファイル名
            dr.Item("FILE_NAME") = frm.lblFile.TextValue
            'フォルダパス
            dr.Item("FILE_FOLDER") = frm.lblFolder.TextValue
            '倉庫コード
            dr.Item("WH_CD") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("WH_CD").ToString
            '商品コード(荷主)
            If colCustGoodsCd1 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colCustGoodsCd1), Excel.Range)
                If String.IsNullOrEmpty(Convert.ToString(xlCell.Value)) = False Then
                    dr.Item("GOODS_CD_CUST") = String.Concat(dr.Item("GOODS_CD_CUST"), Convert.ToString(xlCell.Value))
                    If colCustGoodsCd2 <> 0 Then
                        xlCell = DirectCast(xlSheet.Cells(rowCnt, colCustGoodsCd2), Excel.Range)
                        dr.Item("GOODS_CD_CUST") = String.Concat(dr.Item("GOODS_CD_CUST"), Convert.ToString(xlCell.Value))
                    End If
                    If colCustGoodsCd3 <> 0 Then
                        xlCell = DirectCast(xlSheet.Cells(rowCnt, colCustGoodsCd3), Excel.Range)
                        dr.Item("GOODS_CD_CUST") = String.Concat(dr.Item("GOODS_CD_CUST"), Convert.ToString(xlCell.Value))
                    End If
                End If
            End If
            '商品名
            If colGoodsNm1 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colGoodsNm1), Excel.Range)
                If String.IsNullOrEmpty(Convert.ToString(xlCell.Value)) = False Then
                    dr.Item("GOODS_NM") = String.Concat(dr.Item("GOODS_NM"), Convert.ToString(xlCell.Value))
                    If colGoodsNm2 <> 0 Then
                        xlCell = DirectCast(xlSheet.Cells(rowCnt, colGoodsNm2), Excel.Range)
                        dr.Item("GOODS_NM") = String.Concat(dr.Item("GOODS_NM"), Convert.ToString(xlCell.Value))
                    End If
                    If colGoodsNm3 <> 0 Then
                        xlCell = DirectCast(xlSheet.Cells(rowCnt, colGoodsNm3), Excel.Range)
                        dr.Item("GOODS_NM") = String.Concat(dr.Item("GOODS_NM"), Convert.ToString(xlCell.Value))
                    End If
                End If
            End If
            'ロット№
            If colLotNo <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colLotNo), Excel.Range)
                dr.Item("LOT_NO") = Convert.ToString(xlCell.Value)
            End If
            'シリアル№
            If colSerialNo <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colSerialNo), Excel.Range)
                dr.Item("SERIAL_NO") = Convert.ToString(xlCell.Value)
            End If
            '入目
            If colIrime <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colIrime), Excel.Range)
                dr.Item("IRIME") = Convert.ToString(xlCell.Value)
            End If
            '入目単位
            If colIrimeUt <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colIrimeUt), Excel.Range)
                dr.Item("IRIME_UT") = Convert.ToString(xlCell.Value)
            End If
            '分類項目1
            If colclass1 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colclass1), Excel.Range)
                dr.Item("CLASS_1") = Convert.ToString(xlCell.Value)
            End If
            '分類項目2
            If colclass2 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colclass2), Excel.Range)
                dr.Item("CLASS_2") = Convert.ToString(xlCell.Value)
            End If
            '分類項目3
            If colclass3 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colclass3), Excel.Range)
                dr.Item("CLASS_3") = Convert.ToString(xlCell.Value)
            End If
            '分類項目4
            If colclass4 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colclass4), Excel.Range)
                dr.Item("CLASS_4") = Convert.ToString(xlCell.Value)
            End If
            '分類項目5
            If colclass5 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colclass5), Excel.Range)
                dr.Item("CLASS_5") = Convert.ToString(xlCell.Value)
            End If
            '赤データ区分1
            If colRb1 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colRb1), Excel.Range)
                dr.Item("DATA_RB_1") = Convert.ToString(xlCell.Value)
            End If
            '赤データ区分2
            If colRb2 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colRb2), Excel.Range)
                dr.Item("DATA_RB_2") = Convert.ToString(xlCell.Value)
            End If
            '赤データ区分3
            If colRb3 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colRb3), Excel.Range)
                dr.Item("DATA_RB_3") = Convert.ToString(xlCell.Value)
            End If
            '個数
            If colNb <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colNb), Excel.Range)
                dr.Item("NB") = Convert.ToString(xlCell.Value)
            End If
            '数量
            If colQt <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colQt), Excel.Range)
                dr.Item("QT") = Convert.ToString(xlCell.Value)
            End If
            '数値01
            If colFreeN1 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeN1), Excel.Range)
                dr.Item("FREE_N01") = Convert.ToString(xlCell.Value)
            End If
            '数値02
            If colFreeN2 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeN2), Excel.Range)
                dr.Item("FREE_N02") = Convert.ToString(xlCell.Value)
            End If
            '数値03
            If colFreeN3 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeN3), Excel.Range)
                dr.Item("FREE_N03") = Convert.ToString(xlCell.Value)
            End If
            '数値04
            If colFreeN4 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeN4), Excel.Range)
                dr.Item("FREE_N04") = Convert.ToString(xlCell.Value)
            End If
            '数値05
            If colFreeN5 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeN5), Excel.Range)
                dr.Item("FREE_N05") = Convert.ToString(xlCell.Value)
            End If
            '数値06
            If colFreeN6 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeN6), Excel.Range)
                dr.Item("FREE_N06") = Convert.ToString(xlCell.Value)
            End If
            '数値07
            If colFreeN7 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeN7), Excel.Range)
                dr.Item("FREE_N07") = Convert.ToString(xlCell.Value)
            End If
            '数値08
            If colFreeN8 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeN8), Excel.Range)
                dr.Item("FREE_N08") = Convert.ToString(xlCell.Value)
            End If
            '数値09
            If colFreeN9 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeN9), Excel.Range)
                dr.Item("FREE_N09") = Convert.ToString(xlCell.Value)
            End If
            '数値10
            If colFreeN10 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeN10), Excel.Range)
                dr.Item("FREE_N10") = Convert.ToString(xlCell.Value)
            End If
            '文字01
            If colFreeC1 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeC1), Excel.Range)
                dr.Item("FREE_C01") = Convert.ToString(xlCell.Value)
            End If
            '文字02
            If colFreeC2 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeC2), Excel.Range)
                dr.Item("FREE_C02") = Convert.ToString(xlCell.Value)
            End If
            '文字03
            If colFreeC3 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeC3), Excel.Range)
                dr.Item("FREE_C03") = Convert.ToString(xlCell.Value)
            End If
            '文字04
            If colFreeC4 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeC4), Excel.Range)
                dr.Item("FREE_C04") = Convert.ToString(xlCell.Value)
            End If
            '文字05
            If colFreeC5 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeC5), Excel.Range)
                dr.Item("FREE_C05") = Convert.ToString(xlCell.Value)
            End If
            '文字06
            If colFreeC6 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeC6), Excel.Range)
                dr.Item("FREE_C06") = Convert.ToString(xlCell.Value)
            End If
            '文字07
            If colFreeC7 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeC7), Excel.Range)
                dr.Item("FREE_C07") = Convert.ToString(xlCell.Value)
            End If
            '文字08
            If colFreeC8 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeC8), Excel.Range)
                dr.Item("FREE_C08") = Convert.ToString(xlCell.Value)
            End If
            '文字09
            If colFreeC9 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeC9), Excel.Range)
                dr.Item("FREE_C09") = Convert.ToString(xlCell.Value)
            End If
            '文字10
            If colFreeC10 <> 0 Then
                xlCell = DirectCast(xlSheet.Cells(rowCnt, colFreeC10), Excel.Range)
                dr.Item("FREE_C10") = Convert.ToString(xlCell.Value)
            End If


            'ここでdrに設定された値に対して入力チェック
            If Me._V.IsTorikomiSingleCheck(dr, rowCnt) = False Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlCell)
                xlCell = Nothing
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet)
                xlSheet = Nothing
                xlBook.Close(False) 'Excelを閉じる
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBook)
                xlBook = Nothing
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBooks)
                xlBooks = Nothing
                xlApp.Quit()
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp)
                xlApp = Nothing

                Return False
            End If

            '■INのDataSetに値を設定(Excelに値が設定されていない場合の初期値を設定)
            '商品コード(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("GOODS_CD_CUST").ToString) = True Then
                dr.Item("GOODS_CD_CUST") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_CUST_GOODS_CD").ToString
            End If
            '商品名(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("GOODS_NM").ToString) = True Then
                dr.Item("GOODS_NM") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_GOODS_NM").ToString
            End If
            'ロット№(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("LOT_NO").ToString) = True Then
                dr.Item("LOT_NO") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_LOT_NO").ToString
            End If
            'シリアル№(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("SERIAL_NO").ToString) = True Then
                dr.Item("SERIAL_NO") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_SERIAL_NO").ToString
            End If
            '入目単位(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("IRIME_UT").ToString) = True Then
                dr.Item("IRIME_UT") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_IRIME_UT").ToString
            End If
            '分類項目1(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("CLASS_1").ToString) = True Then
                dr.Item("CLASS_1") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_CLASS_1").ToString
            End If
            '分類項目2(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("CLASS_2").ToString) = True Then
                dr.Item("CLASS_2") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_CLASS_2").ToString
            End If
            '分類項目3(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("CLASS_3").ToString) = True Then
                dr.Item("CLASS_3") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_CLASS_3").ToString
            End If
            '分類項目4(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("CLASS_4").ToString) = True Then
                dr.Item("CLASS_4") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_CLASS_4").ToString
            End If
            '分類項目5(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("CLASS_5").ToString) = True Then
                dr.Item("CLASS_5") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_CLASS_5").ToString
            End If
            '数値01(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_N01").ToString) = True Then
                dr.Item("FREE_N01") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_N01").ToString
            End If
            '数値02(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_N02").ToString) = True Then
                dr.Item("FREE_N02") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_N02").ToString
            End If
            '数値03(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_N03").ToString) = True Then
                dr.Item("FREE_N03") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_N03").ToString
            End If
            '数値04(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_N04").ToString) = True Then
                dr.Item("FREE_N04") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_N04").ToString
            End If
            '数値05(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_N05").ToString) = True Then
                dr.Item("FREE_N05") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_N05").ToString
            End If
            '数値06(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_N06").ToString) = True Then
                dr.Item("FREE_N06") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_N06").ToString
            End If
            '数値07(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_N07").ToString) = True Then
                dr.Item("FREE_N07") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_N07").ToString
            End If
            '数値08(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_N08").ToString) = True Then
                dr.Item("FREE_N08") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_N08").ToString
            End If
            '数値09(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_N09").ToString) = True Then
                dr.Item("FREE_N09") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_N09").ToString
            End If
            '数値10(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_N10").ToString) = True Then
                dr.Item("FREE_N10") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_N10").ToString
            End If
            '文字01(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_C01").ToString) = True Then
                dr.Item("FREE_C01") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_C01").ToString
            End If
            '文字02(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_C02").ToString) = True Then
                dr.Item("FREE_C02") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_C02").ToString
            End If
            '文字03(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_C03").ToString) = True Then
                dr.Item("FREE_C03") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_C03").ToString
            End If
            '文字04(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_C04").ToString) = True Then
                dr.Item("FREE_C04") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_C04").ToString
            End If
            '文字05(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_C05").ToString) = True Then
                dr.Item("FREE_C05") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_C05").ToString
            End If
            '文字06(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_C06").ToString) = True Then
                dr.Item("FREE_C06") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_C06").ToString
            End If
            '文字07(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_C07").ToString) = True Then
                dr.Item("FREE_C07") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_C07").ToString
            End If
            '文字08(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_C08").ToString) = True Then
                dr.Item("FREE_C08") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_C08").ToString
            End If
            '文字09(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_C09").ToString) = True Then
                dr.Item("FREE_C09") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_C09").ToString
            End If
            '文字10(空の場合は初期値を設定)
            If String.IsNullOrEmpty(dr.Item("FREE_C10").ToString) = True Then
                dr.Item("FREE_C10") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_FREE_C10").ToString
            End If


            '■INのDataSetに値を設定(入目・個数・数量を求める)
            '赤黒区分
            rbKb = 1
            If String.IsNullOrEmpty(dataRb1) = False AndAlso _
                (dataRb1).Equals(dr.Item("DATA_RB_1").ToString) = True Then
                rbKb = -1
            End If
            If String.IsNullOrEmpty(dataRb2) = False AndAlso _
                (dataRb2).Equals(dr.Item("DATA_RB_2").ToString) = True Then
                rbKb = -1
            End If
            If String.IsNullOrEmpty(dataRb3) = False AndAlso _
                (dataRb3).Equals(dr.Item("DATA_RB_3").ToString) = True Then
                rbKb = -1
            End If
            rbKb = rbKb * wkRbKb

            '入目WK
            If String.IsNullOrEmpty(dr.Item("IRIME").ToString) = True Then
                dr.Item("IRIME") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_IRIME").ToString
            End If
            irimeWk = Convert.ToDecimal(dr.Item("IRIME").ToString)
            '個数WK
            If String.IsNullOrEmpty(dr.Item("NB").ToString) = True Then
                dr.Item("NB") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_NB").ToString
            End If
            nbWk = Convert.ToDecimal(dr.Item("NB").ToString) * rbKb
            '数量WK
            If String.IsNullOrEmpty(dr.Item("QT").ToString) = True Then
                dr.Item("QT") = ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("INIT_QT").ToString
            End If
            qtWk = Convert.ToDecimal(dr.Item("QT").ToString) * rbKb


            '入目(ここで最終的な設定値を決定)
            If ("1").Equals(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("CAL_IRIME").ToString) = False Then
                dr.Item("IRIME") = Convert.ToString(irimeWk)
            ElseIf ("1").Equals(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("CAL_IRIME").ToString) = True AndAlso _
                    irimeWk = 0 Then
                dr.Item("IRIME") = "0"
            Else
                dr.Item("IRIME") = Convert.ToString(Me.ToRoundDown(qtWk / nbWk, 3))
            End If
            '個数(ここで最終的な設定値を決定)
            If ("1").Equals(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("CAL_NB").ToString) = False Then
                dr.Item("NB") = Convert.ToString(nbWk)
            ElseIf ("1").Equals(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("CAL_NB").ToString) = True AndAlso _
                    irimeWk = 0 Then
                dr.Item("NB") = "0"
            Else
                dr.Item("NB") = Convert.ToString(Me.ToRoundDown(qtWk / irimeWk, 0))
            End If
            '数量(ここで最終的な設定値を決定)
            If ("1").Equals(ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(rowNo).Item("CAL_QT").ToString) = False Then
                dr.Item("QT") = Convert.ToString(qtWk)
            Else
                dr.Item("QT") = Convert.ToString(nbWk * irimeWk)
            End If

            'ここでdrに設定された値に対して入力チェック
            If Me._V.IsTorikomiSingleCheck2(dr, rowCnt) = False Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlCell)
                xlCell = Nothing
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet)
                xlSheet = Nothing
                xlBook.Close(False) 'Excelを閉じる
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBook)
                xlBook = Nothing
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBooks)
                xlBooks = Nothing
                xlApp.Quit()
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp)
                xlApp = Nothing

                Return False
            End If


            '取り込んだ値をデータセットに設定
            rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUST).Rows.Add(dr)

            gyoNo = gyoNo + 1
            rowCnt = rowCnt + 1
        Loop

        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlCell)
        xlCell = Nothing
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet)
        xlSheet = Nothing
        xlBook.Close(False) 'Excelを閉じる
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBook)
        xlBook = Nothing
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBooks)
        xlBooks = Nothing
        xlApp.Quit()
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp)
        xlApp = Nothing

        Return True

    End Function

    ''' <summary>
    ''' Excelの列番号取得(26進数)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getColNo(ByVal value As String) As Integer

        If String.IsNullOrEmpty(value) = True Then
            Return 0
        End If

        Dim max As Integer = value.Length - 1
        Dim rtnValue As Integer = 0
        Dim valueCnt As Integer = 0

        'Excelの列はローマ字なので、数値に置き換える（A→1、J→10のような感じ)
        For i As Integer = max To 0 Step -1
            rtnValue = Convert.ToInt32(rtnValue + (Asc(Mid(value, i + 1, 1)) - 64) * (26 ^ valueCnt))
            valueCnt = valueCnt + 1
        Next i

        Return rtnValue

    End Function

#End Region

#End Region

#Region "数値の切り捨て"

    ''' <summary>
    ''' 数値の切り捨て
    ''' </summary>
    ''' <param name="value">切り捨てを行う数値</param>
    ''' <param name="value2">切り捨て後の数値の有効小数桁数</param>
    ''' <returns>切り捨てた数値</returns>
    ''' <remarks></remarks>
    Friend Function ToRoundDown(ByVal value As Decimal, _
                                ByVal value2 As Integer) As Decimal

        Dim maxLength As Decimal = Convert.ToDecimal(System.Math.Pow(10, value2))

        If value > 0 Then
            'value値が0より大きい場合は、Floorを使用して切り捨て
            Return Convert.ToDecimal(System.Math.Floor(value * maxLength) / maxLength)
        Else
            'value値が0以下の場合は、Ceilingを使用して切り捨て
            Return Convert.ToDecimal(System.Math.Ceiling(value * maxLength) / maxLength)
        End If

    End Function

#End Region

#Region "前埋め設定"

    ''' <summary>
    ''' 前埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">前埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>前埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function MaeCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(value) To keta - 1
            value = String.Concat(value2, value)
        Next

        Return value

    End Function

#End Region

#End Region

#End Region

End Class
