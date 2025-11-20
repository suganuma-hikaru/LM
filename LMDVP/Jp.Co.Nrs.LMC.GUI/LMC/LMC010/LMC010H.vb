' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷管理
'  プログラムID     :  LMC010H : 出荷データ一覧
'  作  成  者       :  [iwamoto]:
' ==========================================================================
Option Strict On
Option Explicit On

Imports System.IO
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.LM.Utility ' 2017/09/25 追加 李
Imports Jp.Co.Nrs.Win.Base
Imports Microsoft.Office.Interop
Imports Microsoft.VisualBasic.FileIO

''' <summary>
''' LMC010ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMC010H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMC010V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMC010G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconV As LMCControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconH As LMCControlH

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    '''検索条件格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FindDs As DataSet

    ''' <summary>
    ''' 印刷種類格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrintSybetu As String

    ''' <summary>
    ''' 印刷種類（Enum)格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrintSybetuEnum As LMC010C.PrintShubetsu

    ''' <summary>
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

    ''' <summary>
    ''' 実行時の更新条件格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _JikkouDs As DataSet

    ''' <summary>
    ''' 分析票パス格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _BunsekiArr As ArrayList

    ''' <summary>
    ''' イエローカードパス格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _YCardArr As ArrayList

    ''' <summary>
    ''' イエローカードRowNo格納フィールド（一括引当処理内で設定）
    ''' </summary>
    ''' <remarks></remarks>
    Private _YCardRowNoArr As ArrayList

    ''' <summary>
    ''' 納品書パス格納フィールド
    ''' </summary>
    ''' <remarks>ADD 2016/07/01</remarks>
    Private _NouhinsyoArr As ArrayList


    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    'START YANAI 要望番号627　こぐまくん対応
    ''' <summary>
    ''' サーバ日時を格納
    ''' </summary>
    ''' <remarks></remarks>
    Private _SysData(2) As String

    ''' <summary>
    ''' エラーフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _errFlg As Boolean = False

    ''' <summary>
    ''' サーバ日時を取得・設定
    ''' </summary>
    ''' <param name="index">
    ''' 0：サーバ日付
    ''' 1：サーバ時間
    ''' </param>
    ''' <value>サーバ日時用プロパティ</value>
    ''' <returns>truckNmのコントロール</returns>
    ''' <remarks></remarks>
    Private Property SysData(ByVal index As LMC010C.SysData) As String
        Get
            Return _SysData(index)
        End Get
        Set(ByVal value As String)
            _SysData(index) = value
        End Set
    End Property
    'END YANAI 要望番号627　こぐまくん対応

    'START YANAI 要望番号917
    ''' <summary>
    ''' 初期表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _ShokiFlg As Boolean
    'END YANAI 要望番号917

    '要望番号1921 修正START
    ''' <summary>
    ''' 引当合計個数
    ''' </summary>
    ''' <remarks></remarks>
    Private _HikiSumNb As Integer
    '要望番号1921 修正END

    '要望番号1921 修正START
    ''' <summary>
    ''' 引当合計数量
    ''' </summary>
    ''' <remarks></remarks>
    Private _HikiSumQt As Decimal
    '要望番号1921 修正END

    '要望番号1921 修正START
    ''' <summary>
    ''' 前行キー(出荷管理番号(大+中))
    ''' </summary>
    ''' <remarks></remarks>
    Private _PreKey As String
    '要望番号1921 修正END

    '2017/09/25 修正 李↓
    '    ''' <summary>
    '    ''' 選択した言語を格納するフィールド
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '#If False Then '_LangFlgが初期化される前にアクセスしてされる問題の仮対応 20151109 INOUE
    '    Private _LangFlg As String
    '#Else
    '    Private _LangFlg As String = Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage
    '#End If
    '2017/09/25 修正 李↑

    ''' <summary>
    ''' アクサルタ一括引当処理適用フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _HikiAXALTA As Boolean

    ''' <summary>
    ''' 印刷対象リモート PDF のコピー先ディレクトリ名
    ''' </summary>
    Private _copyToDirectoryName As String = ""

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        'START YANAI 要望番号917
        Me._ShokiFlg = True
        'END YANAI 要望番号917

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMC010F = New LMC010F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '社内入荷データ作成 terakawa Start
        'Gamenクラスの設定
        Me._G = New LMC010G(Me, frm)
        '社内入荷データ作成 terakawa End

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        '営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(frm.cmbEigyo, frm.cmbSoko)

        'Validate共通クラスの設定
        Me._LMCconV = New LMCControlV(Me, DirectCast(frm, Form))

        'Hnadler共通クラスの設定
        Me._LMCconH = New LMCControlH(DirectCast(frm, Form))

        'Validateクラスの設定
        Me._V = New LMC010V(Me, frm, Me._LMCconV)

        '社内入荷データ作成 terakawa Start
        'Gamenクラスの設定
        'Me._G = New LMC010G(Me, frm)
        '社内入荷データ作成 terakawa End

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        '要望番号:1568 terakawa 2013.01.17 Start
        'システム日付を取得
        Dim sysDate As String = GetSystemDateTime(0).ToString()

        'コントロール個別設定
        'Call Me._G.SetControl(MyBase.GetPGID(), frm)
        Call Me._G.SetControl(MyBase.GetPGID(), frm, sysDate)
        '要望番号:1568 terakawa 2013.01.17 End

        '社内入荷データ作成 terakawa Start
        'コンボ用の値取得
        Dim rtnDs As DataSet = Me.GetIntEdiNmData(frm)

        'コンボ設定
        Call Me._G.SetCmbIntEdi(rtnDs)
        '社内入荷データ作成 terakawa End

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()
        Call Me._G.SetInitValue(frm)

        'メッセージの表示
        Dim setMsg As String = "G007"
        If IsFFEM_OfflineNrsBrCd(LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()) Then
            If IsFFEM_OfflineIraishoMiPrint(frm, LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()) Then
                setMsg = "E02P"
            End If
        End If
        Call MyBase.ShowMessage(frm, setMsg)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        ' 印刷対象リモート PDF のコピー先ディレクトリ名 決定
        Call SetCopyToDirectoryName()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'START YANAI 要望番号917
        Me._ShokiFlg = False
        'END YANAI 要望番号917

    End Sub

#End Region '初期処理

#Region "外部メソッド"

    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMC010C.EventShubetsu, ByVal frm As LMC010F)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        'START YANAI 要望番号917
        If Me._ShokiFlg = True Then
            '初期表示時、画面初期化処理で、未印刷コンボが変更されたと判断され、
            '未印刷コンボ変更時の処理が行われてしまうため、フラグにて判定
            Exit Sub
        End If
        'END YANAI 要望番号917

        '処理開始アクション
        Call Me._LMCconH.StartAction(frm)

        '権限チェック（共通）
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Call Me._LMCconH.EndAction(frm)
            Exit Sub
        End If

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'パラメータ設定
        prm.ReturnFlg = False
        Dim prmDs As DataSet = Nothing
        Dim row As DataRow = Nothing

        'イベント種別による分岐
        Select Case eventShubetsu

            Case LMC010C.EventShubetsu.SINKI

                '******************「新規」******************'

                '要望番号:1568 terakawa 2013.01.17 Start
                '項目チェック
                If Me._V.IsShinkiSingleCheck() = False Then
                    Call Me._LMCconH.EndAction(frm) '終了処理
                    Exit Sub
                End If
                '要望番号:1568 terakawa 2013.01.17 End

                '「新規」処理の場合、荷主マスタ参照画面を開く(モーダレス)
                Call Me.ShowPopup(frm, LMC010C.EventShubetsu.SINKI.ToString(), prm)

                MyBase.ShowMessage(frm, "G007")

            Case LMC010C.EventShubetsu.KENSAKU

                '******************「検索」******************'

                '項目チェック
                If Me._V.IsKensakuSingleCheck() = False Then
                    Call Me._LMCconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '関連チェック
                If Me._V.IsKensakuKanrenCheck() = False Then
                    Call Me._LMCconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '検索処理を行う
                Call Me.SelectData(frm, "NEW")

                'キャッシュから名称取得

                '2017/09/25 修正 李↓
                MyBase.Logger.WriteLog(0, "LMH010H", "SetCachedName", lgm.Selector({"キャッシュ取得", "Cash acquisition", "캐쉬취득", "中国語"}))
                '2017/09/25 修正 李↑

                Call SetCachedName(frm, LMC010C.EventShubetsu.KENSAKU)
                'フォーカスの設定

                '2017/09/25 修正 李↓
                MyBase.Logger.WriteLog(0, "LMH010H", "SetFocus", lgm.Selector({"フォーカス設定", "Focus setting", "포커스설정", "中国語"}))
                '2017/09/25 修正 李↑

                Call Me._G.SetFoucus()

                '2017/09/25 修正 李↓
                MyBase.Logger.WriteLog(0, "LMH010H", "EndAction", lgm.Selector({"終了処理", "End processing", "종료처리", "中国語"}))
                '2017/09/25 修正 李↑

            Case LMC010C.EventShubetsu.MASTER

                '******************「マスタ参照」******************'

                '現在フォーカスのあるコントロール名の取得
                Dim objNm As String = frm.FocusedControlName()

                Select Case objNm

                    Case frm.txtCustCD.Name, frm.txtTrnCD.Name, frm.txtTrnBrCD.Name _
                        , frm.txtShinkiCustCdL.Name, frm.txtShinkiCustCdM.Name _
                        , frm.txtShinkiTrnCd.Name, frm.txtShinkiTrnBrCd.Name '要望番号:1568 terakawa 2013.01.17

                        '入力処理
                        If Me._V.IsRefMstInputCheck() = False Then
                            Call Me._LMCconH.EndAction(frm) '終了処理
                            Exit Sub
                        End If

                        '荷主コード
                        Call Me.ShowPopup(frm, objNm, prm)

                    Case Else
                        'ポップ対象外のテキストの場合
                        MyBase.ShowMessage(frm, "G005")
                End Select

            Case LMC010C.EventShubetsu.HENKO

                '******************「変更」******************'

                '入力チェック
                Me._ChkList = Me._V.GetCheckList()
                If Me._V.IsHenkoInputCheck(Me._ChkList) = False Then
                    Call Me._LMCconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                Me._ChkList = Me.IsHenkoChk(frm, Me._ChkList)

                ' FFEM原料プラント間転送の行のみの選択はエラーとするチェック
                If OnlyFFEM_MaterialPlantTransferLineChecked(frm, Me._ChkList) Then
                    Call Me._LMCconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                'コード値をキャッシュより再取得
                Me.SetDataSetInCd(frm)

                '変更処理
                Call Me.HenkoData(frm, Me._ChkList)

                'キャッシュから名称取得
                Call Me.SetCachedName(frm, LMC010C.EventShubetsu.HENKO)

            Case LMC010C.EventShubetsu.PRINT

                '******************「印刷」******************'

                '初期化
                Me._BunsekiArr = New ArrayList()
                Me._YCardArr = New ArrayList()
                Me._PrintSybetuEnum = 0
                Dim temp As String = frm.cmbPrint.SelectedValue.ToString()

                Select Case temp

                    Case "01"
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.NIHUDA
                    Case "02"
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.DENP
                    Case "03"
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.NHS
                    Case "04"
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.COA
                    Case "05"
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.PIG_GRP
                    Case "06"
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.DENP_GRP
                    Case "07"
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.HOKOKU
                    Case "08"
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.ALL_PRINT
                        'START YANAI 20120122 立会書印刷対応
                    Case "09"
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.TACHIAI
                        'END YANAI 20120122 立会書印刷対応
                        'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                    Case "10"
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.NIHUDA_GRP
                        'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                    Case "13"
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.DENP_GRP_CHK
                    Case "14"
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.ITW_NIHUDA

                    Case LMC010C.OutkaPrintSelectValues.PACKAGE_DETAILS ' 梱包明細
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.PACKAGE_DETAILS
#If True Then
                    Case LMC010C.OutkaPrintSelectValues.UNSO_HOKEN
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.UNSO_HOKEN

#End If
                    Case LMC010C.OutkaPrintSelectValues.YELLOW_CARD
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.YELLOW_CARD

#If True Then
                    Case LMC010C.OutkaPrintSelectValues.HOKOKU_DATE
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.HOKOKU_DATE

#End If

                    Case LMC010C.OutkaPrintSelectValues.ATTEND
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.ATTEND

                    Case LMC010C.OutkaPrintSelectValues.OUTBOUND_CHECK
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.OUTBOUND_CHECK

#If True Then 'ADD 2023/03/29 送品案内書(FFEM)追加
                    Case LMC010C.OutkaPrintSelectValues.SHIPMENTGUIDE
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.SHIPMENTGUIDE
#End If

                    Case LMC010C.OutkaPrintSelectValues.DOKU_JOJU
                        Me._PrintSybetuEnum = LMC010C.PrintShubetsu.DOKU_JOJU

                End Select

                '入力チェック
                Me._ChkList = Me._V.GetCheckList()
                If Me._V.IsPrintInputCheck(Me._PrintSybetuEnum, Me._ChkList) = False Then
                    Call Me._LMCconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                Call Me.IsPrintChk(frm)

                '印刷処理
                Call Me.PrintData(frm, lgm)

                'メッセージコードの判定
                If MyBase.IsMessageStoreExist = True Then

                    MyBase.ShowMessage(frm, "E235")

                    'EXCEL起動()
                    MyBase.MessageStoreDownload()

                End If

            Case LMC010C.EventShubetsu.JIKKOU

                '******************「実行」******************'
                '入力チェック
                Me._ChkList = Me._V.GetCheckList()

                If Me._V.IsJikkouInputCheck(Me._ChkList) = False Then
                    Call Me._LMCconH.EndAction(frm) '終了処理
                    Exit Sub
                End If


                '分析表の場合=================================================
                If ("01").Equals(frm.cmbJikkou.SelectedValue) = True Then

                    'イエローカードRowNo格納フィールドをクリア（一括引当に成功したRowNoが格納される）
                    Me._YCardRowNoArr = New ArrayList()

                    If (LMConst.FLG.ON).Equals(Me._V.IsHikiModeCheck(Me._ChkList)) Then
                        '=====On : FFEM：分析表同時印刷=====


                        Me._ChkList.Clear()

                        'リストの再取得
                        With frm.sprDetail.ActiveSheet

                            Dim list As ArrayList = New ArrayList()
                            Dim max As Integer = .Rows.Count - 1
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                            Dim defNo As Integer = LMC010C.SprColumnIndex.DEF
#Else
                            Dim defNo As Integer = LMC010G.sprDetailDef.DEF.ColNo
#End If
                            Dim nrsBrCd As String

                            For i As Integer = 0 To max
                                If Me._LMCconV.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then

                                    nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
                                    'エラーフラグの初期化
                                    Me._errFlg = False

                                    '対象行以外の行№のクリア
                                    list.Clear()
                                    '選択されたRowIndexを設定
                                    list.Add(i)

                                    '一括引当処理
                                    Call Me.JikkoShori(frm, list, prm)

                                    'エラーが発生した場合は次のレコードへ
                                    If Me._errFlg = True Then
                                        'エラーフラグの初期化
                                        Me._errFlg = False
                                        Continue For
                                    Else
                                        '出荷指示書の共通部品呼び出し
                                        Dim strPass(0) As String
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                                        strPass(0) = String.Concat("C:\LMUSER\", Me._LMCconV.GetCellValue(.Cells(i, LMC010C.SprColumnIndex.OUTKA_NO_L)), ".pdf")
#Else
                                        strPass(0) = String.Concat("C:\LMUSER\", Me._LMCconV.GetCellValue(.Cells(i, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)), ".pdf")
#End If
                                        Me._ChkList = Me._V.GetCheckList()
                                        '

                                        If strPass.Length <> 0 AndAlso (LMConst.FLG.ON).Equals(Me._V.IsShijiPrtModeCheck(Me._ChkList, i)) Then
                                            If System.IO.File.Exists(strPass(0)) = True AndAlso MyBase.PDFDirectPrint(strPass) = False Then
                                                '20151029 tsunehira add Start
                                                '英語化対応
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14
                                                MyBase.SetMessageStore("00", "E799", New String() {Me._LMCconV.GetCellValue(.Cells(i, LMC010C.SprColumnIndex.OUTKA_NO_L))}, list.Item(0).ToString())
#Else
                                                MyBase.SetMessageStore("00", "E799", New String() {Me._LMCconV.GetCellValue(.Cells(i, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))}, list.Item(0).ToString())
#End If
                                                'MyBase.SetMessageStore("00", "E237", New String() {String.Concat("出荷管理番号:", Me._LMCconV.GetCellValue(.Cells(i,LMC010C.SprColumnIndex.OUTKA_NO_L)), "　出荷指示書：プリンタ未設定または対象ファイルなし")}, list.Item(0).ToString())
                                                'MyBase.MessageStoreDownload()
                                            End If
                                        End If

#If True Then   'ADD 2023/03/29 送品案内書(FFEM)追加
                                        '送品案内書の共通部品呼び出し
                                        strPass(0) = String.Concat("C:\LMUSER\LMC543", Me._LMCconV.GetCellValue(.Cells(i, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)), ".pdf")
                                        If strPass.Length <> 0 AndAlso (LMConst.FLG.ON).Equals(Me._V.IsShijiPrtModeCheck(Me._ChkList, i)) Then
                                            If System.IO.File.Exists(strPass(0)) = True AndAlso MyBase.PDFDirectPrint(strPass) = False Then
                                                MyBase.SetMessageStore("00", "E02V", New String() {Me._LMCconV.GetCellValue(.Cells(i, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))}, list.Item(0).ToString())
                                            End If
                                        End If
#End If

                                        '毒劇物譲受書の共通部品呼び出し
                                        strPass(0) = String.Concat("C:\LMUSER\LMC901", Me._LMCconV.GetCellValue(.Cells(i, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)), ".pdf")
                                        If strPass.Length <> 0 AndAlso (LMConst.FLG.ON).Equals(Me._V.IsShijiPrtModeCheck(Me._ChkList, i)) Then
                                            If System.IO.File.Exists(strPass(0)) = True AndAlso MyBase.PDFDirectPrint(strPass) = False Then
                                                MyBase.SetMessageStore("00", "E02Y", New String() {Me._LMCconV.GetCellValue(.Cells(i, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))}, list.Item(0).ToString())
                                            End If
                                        End If
                                    End If

                                    '---ADD 2016/07/04 Start 納品書FFEM版対応
                                    '**** 区分マスタ KBN_GROUP_CD:F020 納品書フォルダーに設定がないと処理しない
                                    '****            KBN_NM1     :営業所コード
                                    '****            KBN_NM2     :荷主コード
                                    '****            

                                    Me._PrintSybetuEnum = LMC010C.PrintShubetsu.NHS

                                    If IsPrintNHSPdfCheck(frm, i) = True Then
                                        '納品書PDF印刷対象
                                        Dim strPass2 As String() = DirectCast(Me._NouhinsyoArr.ToArray(GetType(String)), String())

                                        ' リモートの 納品書 PDF ファイルのローカルへのコピー 
                                        strPass2 = CopyRemotePdf(frm, strPass2)

                                        If MyBase.PDFDirectPrint(strPass2, 7000) = False Then
                                            '20151029 tsunehira add Start
                                            '英語化対応
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                                            MyBase.SetMessageStore("00", "E800", New String() {Me._LMCconV.GetCellValue(.Cells(i, LMC010C.SprColumnIndex.OUTKA_NO_L))}, list.Item(0).ToString())
#Else
                                            MyBase.SetMessageStore("00", "E845", New String() {Me._LMCconV.GetCellValue(.Cells(i, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))}, list.Item(0).ToString())
#End If
                                            '高取倉庫以外
                                            If nrsBrCd.Equals("93") = False AndAlso IsSameAsTakatori(nrsBrCd) = False Then    'MOD 2019/03/25 要望管理005124
                                                Call Me.ShowStorePrintData(frm)
                                            End If
                                            Continue For
                                        End If

                                        '納品書 ログ出力
                                        Me.WritePrintLog(frm, strPass2, LMC010C.PrintShubetsu.NHS)

                                    Else
                                        'Call Me._LMCconH.EndAction(frm) '終了処理
                                        'Exit Sub

                                    End If

                                    '---ADD 2016/07/04 End  納品書FFEM版対応

                                    '印刷
                                    Me._PrintSybetuEnum = LMC010C.PrintShubetsu.COA

                                    If Me._V.IsPrintInputCheck(Me._PrintSybetuEnum, list, eventShubetsu) = False Then
                                        Call Me._LMCconH.EndAction(frm) '終了処理
                                        Exit Sub
                                    End If
                                    '↓こちら印刷処理
                                    If Me.IsPrintChk(frm, list) = True Then
                                        '成功分は出力フラグ更新

                                        '分析票の共通部品呼び出し
                                        Dim strPass() As String
                                        strPass = DirectCast(Me._BunsekiArr.ToArray(GetType(String)), String())

                                        If strPass.Length <> 0 Then
                                            ' リモートの 分析票 PDF ファイルのローカルへのコピー 
                                            strPass = CopyRemotePdf(frm, strPass)

                                            If MyBase.PDFDirectPrint(strPass, 7000) = False Then
                                                '20151029 tsunehira add Start
                                                '英語化対応
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                                                MyBase.SetMessageStore("00", "E800", New String() {Me._LMCconV.GetCellValue(.Cells(i, LMC010C.SprColumnIndex.OUTKA_NO_L))}, list.Item(0).ToString())
#Else
                                                MyBase.SetMessageStore("00", "E800", New String() {Me._LMCconV.GetCellValue(.Cells(i, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))}, list.Item(0).ToString())
#End If
                                                'MyBase.SetMessageStore("00", "E237", New String() {String.Concat("出荷管理番号:", Me._LMCconV.GetCellValue(.Cells(i, 16)), "　分析票：プリンタ未設定または対象ファイルなし")}, list.Item(0).ToString())
                                                'MyBase.MessageStoreDownload()
                                                '高取倉庫以外
                                                If nrsBrCd.Equals("93") = False AndAlso IsSameAsTakatori(nrsBrCd) = False Then    'MOD 2019/03/25 要望管理005124
                                                    Call Me.ShowStorePrintData(frm)
                                                End If
                                                Continue For
                                            End If

                                            '分析票 ログ出力
                                            Me.WritePrintLog(frm, strPass, LMC010C.PrintShubetsu.COA)

                                        End If

                                        '強制実行フラグの設定
                                        MyBase.SetForceOparation(False)
                                        'DataSet設定
                                        Dim ds As DataSet = Me.SetDataSetInCOAPrintData(frm, New LMC010DS(), Convert.ToInt32(list.Item(0)))
                                        Dim rtnDs As DataSet = New LMC010DS()

                                        '==== WSAクラス呼出（変更処理） ====
                                        rtnDs = MyBase.CallWSA("LMC010BLF", "UpdatePrintData", ds)

                                        'Else

                                        '    If MyBase.IsMessageStoreExist() = True Then
                                        '        'EXCEL起動 
                                        '        MyBase.MessageStoreDownload(True)
                                        '        MyBase.ShowMessage(frm, "E235")

                                        '    End If

                                    End If

                                    '高取倉庫以外
                                    If nrsBrCd.Equals("93") = False AndAlso IsSameAsTakatori(nrsBrCd) = False Then    'MOD 2019/03/25 要望管理005124
                                        Call Me.ShowStorePrintData(frm)
                                    End If

                                    'エラーフラグの初期化
                                    Me._errFlg = False

                                    list.Clear()

                                End If
                            Next

                            If MyBase.IsMessageStoreExist() = True Then
                                'EXCEL起動 
                                MyBase.MessageStoreDownload(True)
                                MyBase.ShowMessage(frm, "E235")

                            End If

                            'ADD 2016/07/14 
                            'MyBase.SetMessage("G084")   '一括引当・印刷処理が完了しました。

                            'ADD 2016/08/05 Start 一括引当・印刷処理が完了しましたエクセル出力
                            MyBase.SetMessageStore("02", "G084")

                            If MyBase.IsMessageStoreExist() = True Then
                                'EXCEL起動 
                                MyBase.MessageStoreDownload(True)

                            End If
                            'ADD 2016/08/05 End    一括引当・印刷処理が完了しましたエクセル出力


                        End With
                    Else
                        '=========Off : 通常==========
                        Call Me.JikkoShori(frm, Me._ChkList, prm)

                    End If

                    'イエローカード印刷処理
                    If Me._YCardRowNoArr.Count > 0 Then
                        If (LMConst.FLG.ON).Equals(Me._V.IsYCardModeCheck(Me._ChkList)) Then
                            Me._PrintSybetuEnum = LMC010C.PrintShubetsu.YELLOW_CARD
                            If Me.IsPrintChk(frm, Me._YCardRowNoArr) Then
                                '印刷先プリンタを取得する
                                Dim prtNm As String = LMUserInfoManager.GetYellowCardPrt

                                'イエローカードの共通部品呼び出し
                                Dim strPass() As String = DirectCast(Me._YCardArr.ToArray(GetType(String)), String())
                                If strPass.Length <> 0 Then
                                    ' リモートの イエローカード PDF ファイルのローカルへのコピー 
                                    strPass = CopyRemotePdf(frm, strPass)
                                    If MyBase.PDFDirectPrint(strPass, prtNm) Then
                                        ' イエローカード ログ出力
                                        Me.WritePrintLog(frm, strPass, LMC010C.PrintShubetsu.YELLOW_CARD)
                                    Else
                                        ' PDF 直接印刷共通部品でのエラー発生時のメッセージ編集処理
                                        Call ShowStorePrintErrorData(frm, lgm, Me._YCardRowNoArr)

                                        Call Me.ShowStorePrintData(frm)
                                    End If
                                End If
                            End If
                        End If
                    End If


                ElseIf ("03").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '名鉄CSV作成処理(横浜)

                    '実行処理
                    Call Me.MeitetuCsvMake(frm, Me._ChkList, prm)

                    'END YANAI 要望番号627　こぐまくん対応

                    'START YANAI 要望番号677　オカケンメイト対応
                ElseIf ("04").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    'オカケンメイトCSV作成処理

                    '実行処理
                    Call Me.OkakenCsvMake(frm, Me._ChkList, prm)
                    'END YANAI 要望番号677　オカケンメイト対応

                    'START YANAI 要望番号773　報告書Excel対応
                ElseIf ("05").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '報告書・Excel作成処理

                    'EXCEL出力データ検索処理
                    Dim rtnDs As DataSet = SelectHoukokuExcel(frm, Me._ChkList)

                    'EXCEL出力データ作成処理
                    Call Me.MakeHoukokuExcel(frm, rtnDs)

                    '進捗区分の更新
                    rtnDs = UpdateHoukokuExcel(frm, Me._ChkList)
                    'END YANAI 要望番号773　報告書Excel対応

                    'START YANAI 20120323 名鉄(大阪)対応
                ElseIf ("06").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '名鉄CSV作成処理(大阪)

                    '実行処理
                    Call Me.MeitetuCsvMakeOOSAKA(frm, Me._ChkList, prm)
                    'END YANAI 20120323 名鉄(大阪)対応

                    'START YANAI 20120322 特別梱包個数計算
                ElseIf ("07").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '特別梱包個数計算

                    '実行処理
                    Call Me.outkaPkgNbKeisan(frm, Me._ChkList)
                    'END YANAI 20120322 特別梱包個数計算

                    'START 中村 20121113 シグマ出荷対応
                ElseIf ("08").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    'シグマ出荷CSV作成処理

                    '実行処理
                    Call Me.SigmaCsvMake(frm, Me._ChkList, prm)
                    'END 中村 20121113 シグマ出荷対応

                    '社内入荷データ作成 terakawa Start
                ElseIf ("09").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '社内EDI入荷作成処理

                    '実行処理
                    Call Me.SyanaiInkaMake(frm, Me._ChkList)
                    '社内入荷データ作成 terakawa End

                    '(2013.01.24)埼玉BP対応 -- START --
                ElseIf ("10").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '名鉄CSV作成処理(埼玉)

                    '実行処理
                    Call Me.MeitetuCsvMakeSAITAMA(frm, Me._ChkList, prm)
                    '(2013.01.24)埼玉BP対応 --  END  --

                    'START KIM 20120919 特定荷主対応
                ElseIf ("02").Equals(frm.cmbJikkou.SelectedValue) = True Then

                    '実行(CSV引当処理) 処理
                    Call Me.JikkoShoriForHW(frm, prm)

                    'END KIM 20120919 特定荷主対応

                    '20130218 アグリマート対応 START
                ElseIf ("11").Equals(frm.cmbJikkou.SelectedValue) = True Then

                    '実行(CSV引当処理) 処理
                    Call Me.JikkoShoriForHW(frm, prm, Me._ChkList)
                    '20130218 アグリマート対応 END

                ElseIf ("12").Equals(frm.cmbJikkou.SelectedValue) = True Then

                    '2014.04.22 CALT対応 ST
                    '実行(出荷指示データ作成処理) 処理
                    Call Me.OutkaShiji(frm, Me._ChkList)
                    '2014.04.22 CALT対応 ED

                    '2015.06.22 協立化学　作業料対応 START
                ElseIf ("13").Equals(frm.cmbJikkou.SelectedValue) = True Then

                    '実行(作業料明細特殊作成) 処理
                    Call Me.SagyoMeisai(frm, Me._ChkList)
                    '2015.06.22 協立化学　作業料対応 END

                    'ADD 2016/06/16 Start ヤマト B2 CSV作成処理
                ElseIf ("16").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    'ヤマト B2 CSV作成処理

                    '実行処理
                    Call Me.YamatoB2CsvMake(frm, Me._ChkList, prm)
                    'ADD 2016/06/14 End  ヤマト B2 CSV作成処理

                    'ADD 2016/06/16 Start 佐川 e飛伝 CSV作成処理
                ElseIf ("17").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '佐川 CSV作成処理

                    '実行処理
                    Call Me.SagawaEHidenCsvMake(frm, Me._ChkList, prm)
                    'ADD 2016/06/14 End  佐川 e飛伝 CSV作成処理

                    'ADD 2016/06/16 Start ヤマト 送状番号更新処理
                ElseIf ("18").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    'ヤマト 送状番号更新処理

                    '実行処理
                    Call Me.YamatoDenpNoUpdate(frm, Me._ChkList, prm)
                    'ADD 2016/06/21 End  佐川 e飛伝 送状番号更新処理

                    'ADD 2016/06/21 Start 佐川 e飛伝 送状番号更新処理
                ElseIf ("19").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '佐川 送状番号更新処理

                    '実行処理
                    Call Me.SagawaEHidenDenpNoUpdate(frm, Me._ChkList, prm)
                    'ADD 2016/06/21 End  佐川 e飛伝 送状番号更新処理

                    'ADD 2017/02/24 Start エスライン CSV作成処理
                ElseIf ("20").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    'エスライン CSV作成処理

                    '実行処理
                    Call Me.SLineCsvMake(frm, Me._ChkList, prm)
                    'ADD 2017/02/24 End  エスライン CSV作成処理

                    '2017.09.19 届先追加対応 Annen add start 
                ElseIf ("21").Equals(frm.cmbJikkou.SelectedValue) = True Then

                    '届出更新処理を行う
                    Call Me.ChangeDestination(frm, Me._ChkList)

                    '画面の再表示を行う
                    Call Me.SelectData(frm, "RE")

                    '2017.09.19 届先追加対応 Annen add end

                    '顧客WEB出荷登録バッチ対応 t.kido Start
                ElseIf ("22").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '顧客WEB出荷登録

                    'チェック処理
                    If Me._V.IsWebOutkaSingleCheck() = False Then
                        Call Me._LMCconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If

                    '顧客WEB出荷登録処理
                    Call Me.WebOutkaTorokuShori(frm)
                    '顧客WEB出荷登録バッチ対応 t.kido End

                ElseIf ("23").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '名変入荷作成

                    'チェック処理
                    If Me._V.IsMeihenInkaSingleCheck() = False Then
                        Call Me._LMCconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If

                    '名変入荷作成処理
                    Call Me.MeihenInkaMake(frm, Me._ChkList)

                    '2015.01.06 名鉄対応 tsunehira add start 
#If False Then '名鉄対応(2499) 2016.1.27 changed inoue
                ElseIf ("14").Equals(frm.cmbJikkou.SelectedValue) = True Then

                    '実行処理
                    Call Me.MeitetuCsvMakeGunma(frm, Me._ChkList, prm)

                    '2015.01.06 名鉄対応 tsunehira add end

#ElseIf False Then '名鉄対応(2499) 20160323 deleted inoue 運送会社帳票印刷プルダウンへ移動
                ElseIf LMC010C.JikkouShubetsu.CREATE_MEITESU_CSV_WITHOUT_GROUPING _
                    .Equals(frm.cmbJikkou.SelectedValue) = True Then

                    '実行処理
                    Call Me.MeitetuPrintData(frm, Me._ChkList, prm, False)


                ElseIf LMC010C.JikkouShubetsu.CREATE_MEITESU_CSV_WITH_GROUPING _
                    .Equals(frm.cmbJikkou.SelectedValue) = True Then

                    '実行処理
                    Call Me.MeitetuPrintData(frm, Me._ChkList, prm, True)


#End If
#If True Then   'ADD 2018/07/10 依頼番号 : 001947    ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV
                ElseIf ("24").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    'ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV作成処理

                    '実行処理
                    Call Me.KangarooMagicCsvMake(frm, Me._ChkList, prm)

#End If
                ElseIf ("25").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '現場作業指示

                    ''チェック処理
                    'If Me._V.IsWHSagyoshijiSingleCheck(Me._ChkList) = False Then
                    '    Call Me._LMCconH.EndAction(frm) '終了処理
                    '    Exit Sub
                    'End If

                    '実行処理
                    Call Me.WHSagyoShiji(frm, Me._ChkList, prm)

                ElseIf ("26").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    'ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV(大黒)作成処理

                    '実行処理
                    Call Me.KangarooMagicDaikokuCsvMake(frm, Me._ChkList, prm)

                ElseIf ("27").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    'SBS再保管出荷実績取込処理

                    '実行処理
                    Call Me.SBSSaihokanOutkaImport(frm, prm)

                ElseIf ("28").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '新潟運輸SCV作成処理

                    '実行処理
                    Call Me.NiigataUnyuCsvMake(frm, Me._ChkList, prm)

                ElseIf ("29").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '【神原】オカケンメイトCSV作成処理

                    '実行処理
                    Call Me.KanbaraOkakenCsvMake(frm, Me._ChkList, prm)

                ElseIf ("30").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    'トールCSV作成処理

                    '実行処理
                    Call Me.TollCsvMake(frm, Me._ChkList, prm)

                ElseIf ("31").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '佐川e飛伝ⅢCSV作成

                    '実行処理
                    Call Me.SagawaEHiden3CsvMake(frm, Me._ChkList, prm)

                ElseIf ("32").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    'X-Track出荷登録

                    'チェック処理
                    If Me._V.IsWebOutkaSingleCheck() = False Then
                        Call Me._LMCconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If

                    'X-Track出荷登録処理
                    Call Me.XTrackOutkaTorokuShori(frm)
                End If

            Case LMC010C.EventShubetsu.KANRYO

                '******************「完了」******************'

                'チェックリスト格納変数
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                Dim arr As ArrayList = Me._LMCconH.GetCheckList(frm.sprDetail.ActiveSheet, LMC010C.SprColumnIndex.DEF)
#Else
                Dim arr As ArrayList = Me._LMCconH.GetCheckList(frm.sprDetail.ActiveSheet, LMC010G.sprDetailDef.DEF.ColNo)
#End If

                '入力処理
                If Me._V.IsKanryoInputCheck(_G, arr) = False Then
                    Call Me._LMCconH.EndAction(frm) '終了処理
                    Exit Sub
                End If
                'チェックリスト取得
                prm.ParamDataSet = Me.SetDataSetLMR010InData(frm, arr)

                '完了取込画面に遷移
                LMFormNavigate.NextFormNavigate(Me, "LMR010", prm)

                'エラーがある場合、メッセージ表示
                If MyBase.IsMessageExist() = True Then
                    MyBase.ShowMessage(frm)
                    '処理終了アクション
                    Call Me._LMCconH.EndAction(frm)
                    Exit Sub
                End If

                MyBase.ShowMessage(frm, "G007")

            Case LMC010C.EventShubetsu.DEF_CUST

                '******************「初回荷主変更」******************'

                Call Me.ShowPopup(frm, LMC010C.EventShubetsu.DEF_CUST.ToString(), prm)

                MyBase.ShowMessage(frm, "G007")

            Case LMC010C.EventShubetsu.DOUBLE_CLICK

                '******************「ダブルクリック」******************'

                '検索行をクリックしたのがどうかをチェックする
                If eventShubetsu = LMC010C.EventShubetsu.DOUBLE_CLICK Then
                    If Me.DoubleClickChk(frm) = False Then
                        Call Me._LMCconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If
                End If

                '選択したデータが存在しているかをチェック
                If Me.SelectOutkaData(frm) = True Then

                    prmDs = Me.SetDataSetLMC020InData(frm, Nothing, LMC010C.LMC020_STA_COPY)
                    prm.ParamDataSet = prmDs
                    prm.RecStatus = RecordStatus.NOMAL_REC

                    'モーダレスなので画面ロック必要なし
                    Call Me._LMCconH.EndAction(frm) '終了処理

                    '画面遷移
                    LMFormNavigate.NextFormNavigate(Me, "LMC020", prm)

                    MyBase.ShowMessage(frm, "G007")

                End If

                'START YANAI 要望番号917
            Case LMC010C.EventShubetsu.CMBPRINTSYUBETUCHENGE

                '******************「未印刷コンボ変更時」******************'
                frm.cmbPrint.SelectedValue = frm.cmbPrintSyubetu.SelectedValue

                '処理終了アクション
                Call Me._LMCconH.EndAction(frm)

                '検索処理を行う
                Call ActionControl(LMC010C.EventShubetsu.KENSAKU, frm)
                'END YANAI 要望番号917

#If True Then ' 名鉄対応(2499) 20160323 added inoue
            Case LMC010C.EventShubetsu.TRAPOPRINT

                '******************「運送会社帳票印刷」******************'

                '入力チェック
                Me._ChkList = Me._V.GetCheckList()

                'If Me._V.IsPrintInputCheck(Me._ChkList) = False Then
                If Me._V.IsTrapoPrintStart(Me._ChkList) = False Then

                    Call Me._LMCconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                If LMC010C.TrapoPrintSelectValues.CREATE_MEITESU_CSV_WITHOUT_GROUPING _
                    .Equals(frm.cmbTrapoPrint.SelectedValue) = True Then

                    '実行処理
                    Call Me.MeitetuPrintData(frm, Me._ChkList, prm, False)


                ElseIf LMC010C.TrapoPrintSelectValues.CREATE_MEITESU_CSV_WITH_GROUPING _
                    .Equals(frm.cmbTrapoPrint.SelectedValue) = True Then

                    '実行処理
                    Call Me.MeitetuPrintData(frm, Me._ChkList, prm, True)

                    '-------
                ElseIf LMC010C.TrapoPrintSelectValues.CREATE_MEITESU_BARA_OKURIJYO _
                    .Equals(frm.cmbTrapoPrint.SelectedValue) = True Then

                    '実行処理
                    Call Me.MeitetuPrintData(frm, Me._ChkList, prm, False)

                ElseIf LMC010C.TrapoPrintSelectValues.CREATE_MEITESU_BARA_NIFUDA _
                    .Equals(frm.cmbTrapoPrint.SelectedValue) = True Then

                    '実行処理
                    Call Me.MeitetuPrintData(frm, Me._ChkList, prm, False)

                    '------
                End If

#End If

        End Select

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub


#End Region '外部メソッド

#Region "内部メソッド"

#Region "ユーティティ"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMC010F, ByVal reFlg As String)

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        ''閾値の設定
        'Dim dr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0)
        'MyBase.SetLimitCount(Convert.ToInt32(Convert.ToDouble(dr.Item("VALUE1"))))

        'DataSet設定
        Dim rtDs As DataSet = New LMC010DS()

        If reFlg.Equals("NEW") Then
            '新規検索の場合
            Call Me.SetDataSetInData(frm, rtDs)

        ElseIf reFlg.Equals("RE") Then
            '再検索の場合
            rtDs = Me._FindDs
        End If

        ''SPREAD(表示行)初期化
        'frm.sprDetail.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        'DBリードオンリー設定 ADD 2021/10/27
        Dim rtnDs As DataSet = Me._LMCconH.CallWSAAction(DirectCast(frm, Form) _
                                                         , "LMC010BLF", "SelectListData", rtDs _
                                                         , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                                         (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))) _
                                                          , Convert.ToInt32(Convert.ToDouble(
                                                          MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                                       .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))),
                                                            "1")


        '検索成功時共通処理を行う
        If rtnDs IsNot Nothing Then

            Call Me.SuccessSelect(frm, rtnDs, reFlg)

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' 変更処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub HenkoData(ByVal frm As LMC010F, ByVal arr As ArrayList)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        'DataSet設定
        Dim ds As DataSet = New LMC010DS()
        Dim mUnsoDrs As DataRow() = Nothing
        Call Me.SetDataSetInData_UPDATE_HENKO(frm, ds, arr)

        ' #################### 更新処理　####################'

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "HenkoData")

        '2018/01/15 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add start
        '自動送状接頭文字情報取得
        Call setSetPrefixNumberOfInvoiceNumber(ds)
        '2018/01/15 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add end

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "HenkoData", ds)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            MyBase.ShowMessage(frm, "E235")

            'EXCEL起動()
            MyBase.MessageStoreDownload()

        Else

            '更新成功時、メッセージ表示

            '2017/09/25 修正 李↓
            Call Me.SuccessUpdate(frm, lgm.Selector({"運送会社変更", "Transport company changes", "운송회사변경", "中国語"}))
            '2017/09/25 修正 李↑

        End If

        Call Me._LMCconH.EndAction(frm)

    End Sub


    ''' <summary>
    ''' 検索成功時共通処理（画面別）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMC010F, ByVal ds As DataSet, ByVal reFlg As String)

        Dim dt As DataTable = ds.Tables(LMC010C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'SPREAD(表示行)初期化
        frm.sprDetail.CrearSpread()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        '検索条件の荷主名クリア
        Call Me._G.ClearCustNM()

        'Me._CntSelect = MyBase.GetResultCount.ToString()
        Me._CntSelect = dt.Rows.Count.ToString()

        '再描画する場合は検索結果メッセージを表示しない
        If reFlg.Equals("NEW") = True Then

            If 0 < Convert.ToInt32(Me._CntSelect) Then

                'メッセージエリアの設定
                MyBase.ShowMessage(frm, "G016", New String() {Me._CntSelect})
            End If

        End If

    End Sub



    ''' <summary>
    ''' 更新成功時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">成功メッセージに載せる変換文字</param>
    ''' <remarks></remarks>
    Private Sub SuccessUpdate(ByVal frm As LMC010F, ByVal msg As String)

        'メッセージエリアの設定
        '英語化対応
        '20151021 tsunehira add
        MyBase.ShowMessage(frm, "G074")
        'MyBase.ShowMessage(frm, "G002", New String() {msg, ""})

        '終了処理
        Call Me._LMCconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' Spreadダブルクリック検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function DoubleClickChk(ByVal frm As LMC010F) As Boolean

        'クリックした行が検索行の場合
        If frm.sprDetail.Sheets(0).ActiveRow.Index() = 0 Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 一括引当処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub JikkoShori(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        'ログ出力

        '2017/09/25 修正 李↓
        MyBase.Logger.WriteLog(0,
                               lgm.Selector({"一括引当処理", "Collective provisions Processing", "일괄충당처리", "中国語"}),
                               lgm.Selector({"一括引当処理", "Collective provisions Processing", "일괄충당처리", "中国語"}),
                               lgm.Selector({"処理開始", "Processing Start", "처리 개시", "中国語"})
                               )
        '2017/09/25 修正 李↑

        _JikkouDs = New LMC010DS()
        Dim setDs As DataSet = _JikkouDs.Copy()
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).NewRow()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_OUTKA_M_IN)
        Dim nrsBrCd As String = String.Empty
        Dim max As Integer = arr.Count - 1
        Dim setDt As DataTable = _JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_IN)
        Dim recNo As Integer = 0
        'START YANAI 要望番号1274 一括引当時の出荷指示書印刷で棟班別出力を行う
        Dim custDetailsDr() As DataRow = Nothing
        'END YANAI 要望番号1274 一括引当時の出荷指示書印刷で棟班別出力を行う

        Dim strdate As Date = Now
        Dim strtime As Long = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))

        '2017/09/25 修正 李↓
        MyBase.Logger.WriteLog(0, "LMC010H", "1-データセット詰", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)
        MyBase.Logger.WriteLog(0,
                               lgm.Selector({"LMC010H", "LMC010H", "LMC010H", "LMC010H"}),
                               lgm.Selector({"1-データセット詰", "1 data set packed", "1-데이터세트", "中国語"}),
                               lgm.Selector({"☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime,
                                             "☆☆Start Time：" & Format(strdate, "yyyyMMdd") & " " & strtime,
                                             "☆☆개시시간：" & Format(strdate, "yyyyMMdd") & " " & strtime,
                                             "☆☆中国語：" & Format(strdate, "yyyyMMdd") & " " & strtime})
                               )
        '2017/09/25 修正 李↑

        For i As Integer = 0 To max

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))

            recNo = Convert.ToInt32(arr(i))
            dr("NRS_BR_CD") = nrsBrCd
            dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr("SYS_UPD_DATE") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.SYS_UPD_DATE.ColNo))
            dr("SYS_UPD_TIME") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.SYS_UPD_TIME.ColNo))
            dr("ROW_NO") = recNo
            'START YANAI 20110914 一括引当対応
            dr("CUST_CD_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.CUST_CD_L.ColNo))
            'END YANAI 20110914 一括引当対応
            'START YANAI メモ②No.2
            dr("SASZ_USER") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.SASZ_USER.ColNo))
            'END YANAI メモ②No.2
            'START YANAI メモ②No.15,16,17
            dr("ERR_FLG") = "00"
            'END YANAI メモ②No.15,16,17
            '(2012.03.08) 納品書 再発行フラグ制御追加 LMC513対応 -- START --
            dr("NHS_FLAG") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.NHS_FLAG.ColNo))
            '(2012.03.08) 納品書 再発行フラグ制御追加 LMC513対応 --  END  --
            'START YANAI 要望番号1274 一括引当時の出荷指示書印刷で棟班別出力を行う
            '要望番号:1253 terakawa 2012.07.13 Start
            'custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD = '", Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.CUST_CD_L.ColNo)), "' AND SUB_KB = '37'"))
            custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.NRS_BR_CD.ColNo)),
                                                                                                            "' AND CUST_CD = '", Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.CUST_CD_L.ColNo)),
                                                                                                            "' AND SUB_KB = '37'"))
            '要望番号:1253 terakawa 2012.07.13 End
            If custDetailsDr.Length > 0 Then
                dr("TOU_BETU_FLG") = "1"
            Else
                dr("TOU_BETU_FLG") = "0"
            End If
            'END YANAI 要望番号1274 一括引当時の出荷指示書印刷で棟班別出力を行う

            If IsFFEM_MaterialPlantTransfer(frm, recNo) Then
                ' FFEM原料プラント間転送の場合
                ' ※FFEM原料プラント間転送の場合に LMConst.FLG.ON とするのみで、
                ' 　FFEM原料プラント間転送でない場合は値未設定のため、
                ' 「FFEM原料プラント間転送か否か」は、下記値が「LMConst.FLG.ON か否か」で判定すること
                dr("IS_FFEM_MATERIAL_PLANT_TRANSFER") = LMConst.FLG.ON
            End If

            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        '要望番号:1533 terakawa 2012.10.30 Start
        '出荷(大)排他チェック処理
        _JikkouDs = OutkaLExistChk(frm, _JikkouDs)

        'max値を更新
        max = _JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).Rows.Count - 1

        'RowIndexを再設定
        arr.Clear()
        For i As Integer = 0 To max
            arr.Add(_JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).Rows(i).Item("ROW_NO").ToString())
        Next
        '要望番号:1533 terakawa 2012.10.30 End

        ''2018/01/12 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen upd start
        'Memo)
        'ここでは一覧のデータ検索ループ外のため、下記の設定では行えない。
        '一度キャッシュのZ_KBNテーブルの情報をデータテーブルに確保し、BLFの中で再度割り当てて設定を行うようにする。

        Call setSetPrefixNumberOfInvoiceNumber(_JikkouDs)

        'ADD 2017/10/11トール送状先頭取得 Start

        ''DataSet初期化
        '_JikkouDs.Tables(LMC010C.TABLE_NM_OKURIJYO_WK).Clear()

        ''区分マスタより取得
        'Dim kbnDetailDr() As DataRow = Nothing
        'Dim unsoCD As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.UNSO_CD.ColNo))
        'Dim unsoBRCD As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.UNSO_BR_CD.ColNo))

        'kbnDetailDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'T029'")
        'kbnDetailDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'T029' AND ", _
        '                                                                                   "KBN_NM1 = '", nrsBrCd, "' AND ", _
        '                                                                                   "KBN_NM2 = '", unsoCD, "' AND ", _
        '                                                                                   "KBN_NM3 = '", unsoBRCD, "'"))

        'If kbnDetailDr.Count > 0 Then
        'Dim row As DataRow = _JikkouDs.Tables(LMC010C.TABLE_NM_OKURIJYO_WK).NewRow
        '    row("OKURIJYO_HEAD") = kbnDetailDr(0).Item("KBN_NM4")

        '    _JikkouDs.Tables(LMC010C.TABLE_NM_OKURIJYO_WK).Rows.Add(row)
        'End If
        ''ADD 2017/10/11トール送状先頭取得 End
        ''2018/01/12 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen upd end

        Dim enddate As Date = Now
        Dim endtime As Long = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))

        '2017/09/25 修正 李↓
        MyBase.Logger.WriteLog(0,
                               lgm.Selector({"LMC010H", "LMC010H", "LMC010H", "LMC010H"}),
                               lgm.Selector({"1-データセット詰", "1 data set packed", "1-데이터세트", "中国語"}),
                               lgm.Selector({"☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime,
                                             "☆☆End Time：" & Format(enddate, "yyyyMMdd") & " " & endtime,
                                             "☆☆종료시간：" & Format(enddate, "yyyyMMdd") & " " & endtime,
                                             "中国語：" & Format(enddate, "yyyyMMdd") & " " & endtime})
                               )
        MyBase.Logger.WriteLog(0,
                               lgm.Selector({"LMC010H", "LMC010H", "LMC010H", "LMC010H"}),
                               lgm.Selector({"1-データセット詰", "Collective provisions Processing", "1-데이터세트", "中国語"}),
                               lgm.Selector({"☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒",
                                             "☆☆Pass Time：" & endtime - strtime & "milli sec",
                                             "☆☆경과시간：" & endtime - strtime & "밀리 초",
                                             "中国語：" & endtime - strtime & "中国語"})
                               )
        '2017/09/25 修正 李↑

        '検索時WSAクラス呼び出し
        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))

        '2017/09/25 修正 李↓
        MyBase.Logger.WriteLog(0,
                               lgm.Selector({"LMC010H", "LMC010H", "LMC010H", "LMC010H"}),
                               lgm.Selector({"出荷M取得", "Shipping M acquisition", "출하M취득", "中国語"}),
                               lgm.Selector({"☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime,
                                             "☆☆Start Time：" & Format(strdate, "yyyyMMdd") & " " & strtime,
                                             "☆☆개시시간：" & Format(strdate, "yyyyMMdd") & " " & strtime,
                                             "中国語：" & Format(strdate, "yyyyMMdd") & " " & strtime})
                               )
        '2017/09/25 修正 李↑

        _JikkouDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "SelectOutkaM", _JikkouDs, 0)
        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))

        '2017/09/25 修正 李↓
        MyBase.Logger.WriteLog(0,
                               lgm.Selector({"LMC010H", "LMC010H", "LMC010H", "LMC010H"}),
                               lgm.Selector({"出荷M取得", "Shipping M acquisition", "출하M취득", "中国語"}),
                               lgm.Selector({"☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime,
                                             "☆☆Start Time：" & Format(enddate, "yyyyMMdd") & " " & endtime,
                                             "☆☆개시시간：" & Format(enddate, "yyyyMMdd") & " " & endtime,
                                             "中国語：" & Format(enddate, "yyyyMMdd") & " " & endtime})
                               )
        MyBase.Logger.WriteLog(0,
                               lgm.Selector({"LMC010H", "LMC010H", "LMC010H", "LMC010H"}),
                               lgm.Selector({"出荷M取得", "Shipping M acquisition", "출하M취득", "中国語"}),
                               lgm.Selector({"☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒",
                                             "☆☆Pass Time：" & endtime - strtime & "milli sec",
                                             "☆☆경과시간：" & endtime - strtime & "밀리 초",
                                             "中国語：" & endtime - strtime & "中国語"})
                               )
        '2017/09/25 修正 李↑

        'START YANAI 20110914 一括引当対応
        If Me._JikkouDs Is Nothing Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E192")
            'EXCEL起動()
            If MyBase.IsMessageStoreExist = True Then
                MyBase.MessageStoreDownload()
            End If
            Exit Sub
        End If
        'END YANAI 20110914 一括引当対応

        '(2013.03.12)要望番号1229 -- START --
        '「小分け」「サンプル」が含まれている場合はエラー
        For i As Integer = 0 To _JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT).Rows.Count - 1
            If ("03").Equals(_JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT).Rows(i).Item("ALCTD_KB").ToString) = True OrElse
                ("04").Equals(_JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT).Rows(i).Item("ALCTD_KB").ToString) = True Then
                '処理終了メッセージの表示
                '英語化対応
                '20151021 tsunehira add
                MyBase.ShowMessage(frm, "E636")
                'MyBase.ShowMessage(frm, "E375", New String() {"「小分け」「サンプル」出荷が含まれている", "一括引当"})
                Exit Sub
            End If
        Next
        '(2013.03.12)要望番号1229 --  END  --

        'IN情報を再設定
        Dim reSetDt As DataTable = _JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_IN)
        For i As Integer = 0 To max
            reSetDt.ImportRow(setDt.Rows(i))
        Next

        Dim dt As DataTable = _JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT)

        'START KIM START KIM 2012/10/10 要望番号：1499対応
        '出荷個数、数量が設定されていないデータが存在する場合、エラー
        Dim rtnFlg As Boolean = True
        For i As Integer = 0 To dt.Rows.Count - 1
            If ("0").Equals(dt.Rows(i).Item("OUTKA_TTL_NB").ToString) = True AndAlso
               ("0.000").Equals(dt.Rows(i).Item("OUTKA_TTL_QT").ToString) = True Then
                rtnFlg = False
                Exit For
            End If
        Next
        If rtnFlg = False Then
            'メッセージ表示
            MyBase.ShowMessage(frm, "E192")
            Exit Sub
        End If
        'END   KIM START KIM 2012/10/10 要望番号：1499対応

        Dim outkaNoL As String = String.Empty
        Dim dtOutkaNoL As String = String.Empty
        Dim arrhiki As ArrayList = New ArrayList()
        Dim recCnt As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max

            outkaNoL = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))

            For j As Integer = 0 To recCnt

                dtOutkaNoL = dt.Rows(j).Item("OUTKA_NO_L").ToString()

                If outkaNoL.Equals(dtOutkaNoL) = True Then
                    arrhiki.Add(arr(i).ToString())
                End If

            Next

        Next

        'アクサルタ一括引当処理の適用判定 要望番号1523
        Dim hikiErrFlg As Boolean = True
        If String.IsNullOrEmpty(frm.txtCustCD.TextValue()) = False Then
            Dim custDDr() As DataRow = Nothing
            custDDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyo.SelectedValue.ToString(),
                                                                                                            "' AND CUST_CD = '", frm.txtCustCD.TextValue(),
                                                                                                            "' AND SUB_KB = '02' AND SET_NAIYO = '08'"))
            If custDDr.Length > 0 Then
                _HikiAXALTA = True
            Else
                _HikiAXALTA = False
            End If
        Else
            _HikiAXALTA = False
        End If

        'Falseの場合、更新対象有
        Dim hikiFlg As Boolean = True

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        For i As Integer = 0 To recCnt
            If IsFFEM_MaterialPlantTransfer(frm, Convert.ToInt32(arrhiki(i))) Then
                ' FFEM原料プラント間転送の場合
                ' 出荷は登録時点で引当済みの状態、
                ' かつ個数・数量は LMS の計算式に当てはまらない当該業務固有の値で設定してあり変更の必要はないため
                ' 在庫引当には遷移しない。
                ' 後続のサーバサイド InsertSaveAction でもサーバー側で同様の判定を行い、更新は行わず一連の印刷処理のみ実行する
                hikiFlg = False
                Continue For
            End If

            strdate = Now
            strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))

            '2017/09/25 修正 李↓
            MyBase.Logger.WriteLog(0,
                                   lgm.Selector({"LMC010H", "LMC010H", "LMC010H", "LMC010H"}),
                                   lgm.Selector({String.Concat("引当画面呼出 ", i + 1, "件目"),
                                                 String.Concat("Provision screen calls ", i + 1, "Case"),
                                                 String.Concat("충당화면호출 ", i + 1, "건째"),
                                                 String.Concat("中国語 ", i + 1, "中国語")}),
                                   lgm.Selector({"☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime,
                                                 "☆☆Start Time：" & Format(strdate, "yyyyMMdd") & " " & strtime,
                                                 "☆☆개시시간：" & Format(strdate, "yyyyMMdd") & " " & strtime,
                                                 "中国語：" & Format(strdate, "yyyyMMdd") & " " & strtime})
                                   )
            '2017/09/25 修正 李↑

            '在庫引当画面を呼び出す
            'hikiFlg = hikiFlg And Not Me.SetReturnHikiatePop(frm, _JikkouDs, arrhiki, i)
            hikiErrFlg = Me.SetReturnHikiatePop(frm, _JikkouDs, arrhiki, i)  '引当エラー時は処理中止 要望番号1523

            enddate = Now
            endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))

            '2017/09/25 修正 李↓
            MyBase.Logger.WriteLog(0,
                                   lgm.Selector({"LMC010H", "LMC010H", "LMC010H", "LMC010H"}),
                                   lgm.Selector({String.Concat("引当画面呼出 ", i + 1, "件目"),
                                                 String.Concat("Provision screen calls ", i + 1, "Case"),
                                                 String.Concat("충당화면호출 ", i + 1, "건째"),
                                                 String.Concat("中国語 ", i + 1, "中国語")}),
                                   lgm.Selector({"☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime,
                                                 "☆☆End Time：" & Format(enddate, "yyyyMMdd") & " " & endtime,
                                                 "☆☆종료시간：" & Format(enddate, "yyyyMMdd") & " " & endtime,
                                                 "中国語：" & Format(enddate, "yyyyMMdd") & " " & endtime})
                                   )
            MyBase.Logger.WriteLog(0,
                                   lgm.Selector({"LMC010H", "LMC010H", "LMC010H", "LMC010H"}),
                                   lgm.Selector({String.Concat("引当画面呼出 ", i + 1, "件目"),
                                                 String.Concat("Provision screen calls ", i + 1, "Case"),
                                                 String.Concat("충당화면호출 ", i + 1, "건째"),
                                                 String.Concat("中国語 ", i + 1, "中国語")}),
                                   lgm.Selector({"☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒",
                                                 "☆☆Pass Time：" & endtime - strtime & "milli sec",
                                                 "☆☆경과시간：" & endtime - strtime & "밀리 초",
                                                 "中国語：" & endtime - strtime & "中国語"})
                                   )
            '2017/09/25 修正 李↑

            '引当エラー時は処理中止 要望番号1523
            hikiFlg = False
            If hikiErrFlg = False Then
                If _HikiAXALTA = True Then
                    Exit For
                End If
            End If

        Next

        '更新対象がない場合
        If hikiFlg = True Then

            'メッセージ表示
            MyBase.ShowMessage(frm, "E183")
            'EXCEL起動()
            If MyBase.IsMessageStoreExist = True Then
                MyBase.MessageStoreDownload()
            End If

            '終了処理
            Call Me._LMCconH.EndAction(frm)
            Exit Sub

        End If

        'START YANAI 引当エラーは音声CSV出力しない
        Me._JikkouDs.Tables(LMC010C.TABLE_NM_ERR).Clear()
        'END YANAI 引当エラーは音声CSV出力しない

        'Notes1932 START
        Dim flgHW As String = String.Empty
        flgHW = "0" '0:OFF
        'Notes1932 END

        'START YANAI 20110914 一括引当対応
        '出荷(大)の進捗区分、出荷梱包個数の設定
        Me._JikkouDs = Me.SetOutkaL(frm, Me._JikkouDs, flgHW)
        'END YANAI 20110914 一括引当対応

        'START YANAI 要望番号585
        '運送データ（大）データセットの生成
        Me._JikkouDs = Me.SetUnsoL(frm, Me._JikkouDs)
        'END YANAI 要望番号585

        _JikkouDs.Merge(New RdPrevInfoDS)
        _JikkouDs.Tables(LMConst.RD).Clear()

        'START YANAI 20110914 一括引当対応
        'START YANAI 要望番号1151 埼玉：大日精化 修正依頼
        ''送り状印刷対象荷主を取得
        'Me._JikkouDs = Me.setPrintCust(frm, Me._JikkouDs)
        'END YANAI 要望番号1151 埼玉：大日精化 修正依頼
        'END YANAI 20110914 一括引当対応

        '更新実行
        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))

        '2017/09/25 修正 李↓
        MyBase.Logger.WriteLog(0,
                               lgm.Selector({"LMC010H", "LMC010H", "LMC010H", "LMC010H"}),
                               lgm.Selector({"更新/印刷処理", "Update/Print Process", "갱신/인쇄처리", "中国語"}),
                               lgm.Selector({"☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime,
                                             "☆☆Start Time：" & Format(strdate, "yyyyMMdd") & " " & strtime,
                                             "☆☆개시시간：" & Format(strdate, "yyyyMMdd") & " " & strtime,
                                             "中国語：" & Format(strdate, "yyyyMMdd") & " " & strtime})
                               )
        '2017/09/25 修正 李↑

        _JikkouDs = MyBase.CallWSA("LMC010BLF", "InsertSaveAction", _JikkouDs)

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))

        '2017/09/25 修正 李↓
        MyBase.Logger.WriteLog(0,
                               lgm.Selector({"LMC010H", "LMC010H", "LMC010H", "LMC010H"}),
                               lgm.Selector({"更新/印刷処理", "Update/Print Process", "갱신/인쇄처리", "中国語"}),
                               lgm.Selector({"☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime,
                                             "☆☆End Time：" & Format(enddate, "yyyyMMdd") & " " & endtime,
                                             "☆☆종료시간：" & Format(enddate, "yyyyMMdd") & " " & endtime,
                                             "中国語：" & Format(enddate, "yyyyMMdd") & " " & endtime})
                               )
        MyBase.Logger.WriteLog(0,
                               lgm.Selector({"LMC010H", "LMC010H", "LMC010H", "LMC010H"}),
                               lgm.Selector({"更新/印刷処理", "Update/Print Process", "갱신/인쇄처리", "中国語"}),
                               lgm.Selector({"☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒",
                                             "☆☆Pass Time：" & endtime - strtime & "milli sec",
                                             "☆☆경과시간：" & endtime - strtime & "밀리 초",
                                             "中国語：" & endtime - strtime & "中国語"})
                               )
        '2017/09/25 修正 李↑


        '        MyBase.Logger.WriteLog(0, "LMC010H", String.Concat("更新/印刷処理"), "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        '       MyBase.Logger.WriteLog(0, "LMC010H", String.Concat("更新/印刷処理"), "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")

        'エラー帳票出力の判定
        'START YANAI メモ②No.15,16,17
        'If Me.ShowStorePrintData(frm) = False Then
        '    Exit Sub
        'Else
        '    '処理終了メッセージの表示
        '    MyBase.ShowMessage(frm, "G002", New String() {"実行", String.Empty})

        '    'プレビュー判定 
        '    Dim prevDt As DataTable = _JikkouDs.Tables(LMConst.RD)
        '    If prevDt.Rows.Count > 0 Then

        '        'プレビューの生成
        '        Dim prevFrm As New RDViewer()

        '        'データ設定
        '        prevFrm.DataSource = prevDt

        '        'プレビュー処理の開始
        '        prevFrm.Run()

        '        'プレビューフォームの表示
        '        prevFrm.Show()

        '    End If

        'End If
        '処理終了メッセージの表示
        'START YANAI 要望番号627　こぐまくん対応
        'MyBase.ShowMessage(frm, "G002", New String() {"実行", String.Empty})
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})
        'END YANAI 要望番号627　こぐまくん対応

        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))

        '2017/09/25 修正 李↓
        MyBase.Logger.WriteLog(0,
                               lgm.Selector({"LMC010H", "LMC010H", "LMC010H", "LMC010H"}),
                               lgm.Selector({"DIC音声データ作成", "Create DIC voice Data", "DIC음성데이터작성", "中国語"}),
                               lgm.Selector({"☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime,
                                             "☆☆Start Time：" & Format(strdate, "yyyyMMdd") & " " & strtime,
                                             "☆☆개시시간：" & Format(strdate, "yyyyMMdd") & " " & strtime,
                                             "中国語：" & Format(strdate, "yyyyMMdd") & " " & strtime})
                               )
        '2017/09/25 修正 李↑

        '要望番号:1339 yamanaka 2012.08.27 Start
        'START YANAI 引当エラーは音声CSV出力しない
        'If Me.ShowStorePrintData(frm) = True Then
        '    Me.DicCsvMake(frm, prm, _JikkouDs, arr)
        'End If

        '引当の更新時、排他エラーになったデータのチェックをはずす
        Call Me.SetCheckBox(frm, Me._JikkouDs)

        '選択された行の行番号を取得
        Dim chkList As ArrayList = Me._V.GetCheckList()

        '日立物流出荷音声データCSV作成処理
        Me.DicCsvMake(frm, prm, _JikkouDs, chkList)
        'END YANAI 引当エラーは音声CSV出力しない

        '引当成功分をイエローカードRowNo格納フィールドに設定
        Me._YCardRowNoArr = chkList

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))

        '2017/09/25 修正 李↓
        MyBase.Logger.WriteLog(0,
                               lgm.Selector({"LMC010H", "LMC010H", "LMC010H", "LMC010H"}),
                               lgm.Selector({"DIC音声データ作成", "Create DIC voice Data", "DIC음성데이터작성", "中国語"}),
                               lgm.Selector({"☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime,
                                             "☆☆End Time：" & Format(enddate, "yyyyMMdd") & " " & endtime,
                                             "☆☆종료시간：" & Format(enddate, "yyyyMMdd") & " " & endtime,
                                             "中国語：" & Format(enddate, "yyyyMMdd") & " " & endtime})
                               )
        MyBase.Logger.WriteLog(0,
                               lgm.Selector({"LMC010H", "LMC010H", "LMC010H", "LMC010H"}),
                               lgm.Selector({"DIC音声データ作成", "Create DIC voice Data", "DIC음성데이터작성", "中国語"}),
                               lgm.Selector({"☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒",
                                             "☆☆Pass Time：" & endtime - strtime & "milli sec",
                                             "☆☆경과시간：" & endtime - strtime & "밀리 초",
                                             "中国語：" & endtime - strtime & "中国語"})
                               )
        '2017/09/25 修正 李↑

        'START YANAI 引当エラーは音声CSV出力しない
        If nrsBrCd.Equals("93") = True OrElse IsSameAsTakatori(nrsBrCd) = True Then    'MOD 2019/03/25 要望管理005124
            Call Me.ShowStorePrintDataTaka(frm, arr)
        Else
            Call Me.ShowStorePrintData(frm)
        End If
        'END YANAI 引当エラーは音声CSV出力しない
        '要望番号:1339 yamanaka 2012.08.27 End

        'プレビュー判定 
        Dim prevDt As DataTable = _JikkouDs.Tables(LMConst.RD)

        If prevDt.Rows.Count > 0 Then

            'プレビューの生成
            Dim prevFrm As New RDViewer()

            'データ設定
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始
            prevFrm.Run()

            'プレビューフォームの表示
            prevFrm.Show()

            '2014.10.02 高取対応START
            '高取倉庫の場合はPDF化し、ユーザープリンターに直接印刷する為,プレビューを閉じる
            If nrsBrCd.Equals("93") = True OrElse IsSameAsTakatori(nrsBrCd) = True Then    'MOD 2019/03/25 要望管理005124
                prevFrm.Close()

            End If
            '2014.10.02 高取対応END

        End If
        'END YANAI メモ②No.15,16,17

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub

    '2018/01/15 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add start
    ''' <summary>
    ''' 自動送状番号接頭文字情報設定
    ''' </summary>
    ''' <param name="targetDs">対象データセット</param>
    ''' <remarks></remarks>
    Private Sub setSetPrefixNumberOfInvoiceNumber(ByRef targetDs As DataSet)
        Dim targetTbl As DataTable = targetDs.Tables(LMC010C.TABLE_NM_PRE_NO_OF_INVOICE_NO)
        Dim targetRows() As DataRow = Nothing
        targetTbl.Clear()
        targetRows = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'T029'")

        If targetRows.Count > 0 Then
            For Each targetRow As DataRow In targetRows
                Dim row As DataRow = targetTbl.NewRow
                row("NRS_BR_CD") = targetRow("KBN_NM1")
                row("UNSO_CD") = targetRow("KBN_NM2")
                row("UNSO_BR_CD") = targetRow("KBN_NM3")
                row("PREFIX_NO") = targetRow("KBN_NM4")
                targetTbl.Rows.Add(row)
            Next
        End If

    End Sub
    '2018/01/15 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add end

    'START YANAI 要望番号627　こぐまくん対応
    ''' <summary>
    ''' 名鉄CSV作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MeitetuCsvMake(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        Dim prmDs As DataSet = New LMC800DS
        Me._JikkouDs = New LMC010DS
        Dim setDs As DataSet = Me._JikkouDs.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_IN_CSV).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim max As Integer = arr.Count - 1
        Dim kbnDr() As DataRow = Nothing
        Dim nrsBrCd As String = String.Empty
        Dim unsoCd As String = String.Empty

        'システム日付の取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "SetSysDateTime", setDs)
        Dim sysDt As DataTable = rtnDs.Tables(LMC010C.TABLE_NM_SYS_DATETIME)
        If 0 < sysDt.Rows.Count Then

            Me.SysData(LMC010C.SysData.YYYYMMDD) = sysDt.Rows(0).Item("SYS_DATE").ToString()
            Me.SysData(LMC010C.SysData.HHMMSSsss) = sysDt.Rows(0).Item("SYS_TIME").ToString()

        End If

        '保存先のCSVファイルのパス・ファイル名
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C003' AND ",
                                                                                       "KBN_CD = '00'"))
        Dim filePath As String = kbnDr(0).Item("KBN_NM1").ToString
        Dim fileName As String = kbnDr(0).Item("KBN_NM2").ToString

        For i As Integer = 0 To max

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
            unsoCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_CD.ColNo))

            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C004' AND ",
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ",
                                                                                           "KBN_NM2 = '", unsoCd, "'"))
            If kbnDr.Length = 0 Then
                '区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If

            dr("NRS_BR_CD") = nrsBrCd
            dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr("ROW_NO") = Convert.ToInt32(arr(i))
            dr("FILEPATH") = filePath
            dr("FILENAME") = fileName
            dr("SYS_DATE") = Me.SysData(LMC010C.SysData.YYYYMMDD)
            dr("SYS_TIME") = Me.SysData(LMC010C.SysData.HHMMSSsss)
            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        If setDt.Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        '2015.12.28 名鉄対応 tsunehira adds start
        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        Me._JikkouDs.Merge(New RdPrevInfoDS)

        '検索時WSAクラス呼び出し
        'prmDs = MyBase.CallWSA("LMC010BLF", "SelectMeitetuCsv", Me._JikkouDs)
        prmDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "SelectMeitetuCsv", Me._JikkouDs, 0)

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        'エラー帳票出力の判定
        'Call Me.ShowStorePrintData(frm)

        '名鉄CSV出力処理呼出
        prm.ParamDataSet = prmDs
        LMFormNavigate.NextFormNavigate(Me, "LMC800", prm)

        ''プレビュー判定 
        'Dim prevDt As DataTable = prmDs.Tables(LMConst.RD)
        'If prevDt.Rows.Count > 0 Then

        '    'プレビューの生成 
        '    Dim prevFrm As New RDViewer()

        '    'データ設定 
        '    prevFrm.DataSource = prevDt

        '    'プレビュー処理の開始 
        '    prevFrm.Run()

        '    'プレビューフォームの表示 
        '    prevFrm.Show()

        'End If

        '2015.12.28 名鉄対応 tsunehira adds end

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub

    'START YANAI 要望番号677　オカケンメイト対応
    ''' <summary>
    ''' オカケンメイトCSV作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub OkakenCsvMake(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        Dim prmDs As DataSet = New LMC810DS
        Me._JikkouDs = New LMC010DS()
        Dim setDs As DataSet = Me._JikkouDs.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_IN_CSV).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim max As Integer = arr.Count - 1
        Dim kbnDr() As DataRow = Nothing
        Dim nrsBrCd As String = String.Empty
        Dim unsoCd As String = String.Empty

        'システム日付の取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "SetSysDateTime", setDs)
        Dim sysDt As DataTable = rtnDs.Tables(LMC010C.TABLE_NM_SYS_DATETIME)
        If 0 < sysDt.Rows.Count Then

            Me.SysData(LMC010C.SysData.YYYYMMDD) = sysDt.Rows(0).Item("SYS_DATE").ToString()
            Me.SysData(LMC010C.SysData.HHMMSSsss) = sysDt.Rows(0).Item("SYS_TIME").ToString()

        End If

        '保存先のCSVファイルのパス・ファイル名
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C003' AND ",
                                                                                       "KBN_CD = '01'"))
        Dim filePath As String = kbnDr(0).Item("KBN_NM1").ToString
        Dim fileName As String = kbnDr(0).Item("KBN_NM2").ToString

        For i As Integer = 0 To max

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
            unsoCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_CD.ColNo))

            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C005' AND ",
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ",
                                                                                           "KBN_NM2 = '", unsoCd, "'"))
            If kbnDr.Length = 0 Then
                '区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If

            dr("NRS_BR_CD") = nrsBrCd
            dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr("ROW_NO") = Convert.ToInt32(arr(i))
            dr("FILEPATH") = filePath
            dr("FILENAME") = fileName
            dr("SYS_DATE") = Me.SysData(LMC010C.SysData.YYYYMMDD)
            dr("SYS_TIME") = Me.SysData(LMC010C.SysData.HHMMSSsss)
            dr("CSV_OUTFLG") = LMConst.FLG.OFF          '0:標準
            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        If setDt.Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        '検索時WSAクラス呼び出し
        prmDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "SelectOkakenCsv", Me._JikkouDs, 0)

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        '岡山貨物CSV出力処理呼出
        prm.ParamDataSet = prmDs
        LMFormNavigate.NextFormNavigate(Me, "LMC810", prm)

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub
    'END YANAI 要望番号677　オカケンメイト対応

    'START YANAI 20120323 名鉄(大阪)対応
    ''' <summary>
    ''' 名鉄CSV作成処理(大阪)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MeitetuCsvMakeOOSAKA(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        Dim prmDs As DataSet = New LMC820DS
        Me._JikkouDs = New LMC010DS
        Dim setDs As DataSet = Me._JikkouDs.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_IN_CSV).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim max As Integer = arr.Count - 1
        Dim kbnDr() As DataRow = Nothing
        Dim nrsBrCd As String = String.Empty
        Dim unsoCd As String = String.Empty

        'システム日付の取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "SetSysDateTime", setDs)
        Dim sysDt As DataTable = rtnDs.Tables(LMC010C.TABLE_NM_SYS_DATETIME)
        If 0 < sysDt.Rows.Count Then

            Me.SysData(LMC010C.SysData.YYYYMMDD) = sysDt.Rows(0).Item("SYS_DATE").ToString()
            Me.SysData(LMC010C.SysData.HHMMSSsss) = sysDt.Rows(0).Item("SYS_TIME").ToString()

        End If

        '保存先のCSVファイルのパス・ファイル名
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C003' AND ",
                                                                                       "KBN_CD = '02'"))
        Dim filePath As String = kbnDr(0).Item("KBN_NM1").ToString
        Dim fileName As String = kbnDr(0).Item("KBN_NM2").ToString

        For i As Integer = 0 To max

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
            unsoCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_CD.ColNo))

            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C007' AND ",
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ",
                                                                                           "KBN_NM2 = '", unsoCd, "'"))
            If kbnDr.Length = 0 Then
                '区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If

            dr("NRS_BR_CD") = nrsBrCd
            dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr("ROW_NO") = Convert.ToInt32(arr(i))
            dr("FILEPATH") = filePath
            dr("FILENAME") = fileName
            dr("SYS_DATE") = Me.SysData(LMC010C.SysData.YYYYMMDD)
            dr("SYS_TIME") = Me.SysData(LMC010C.SysData.HHMMSSsss)
            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        If setDt.Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '検索時WSAクラス呼び出し
        prmDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "SelectMeitetuCsvOOSAKA", Me._JikkouDs, 0)

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        '名鉄CSV出力処理呼出
        prm.ParamDataSet = prmDs
        LMFormNavigate.NextFormNavigate(Me, "LMC820", prm)

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub
    'END YANAI 20120323 名鉄(大阪)対応

    '要望番号:1339 yamanaka 2012.08.27 Start
    ''' <summary>
    ''' 日立物流出荷音声データCSV作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub DicCsvMake(ByVal frm As LMC010F, ByVal prm As LMFormData, ByVal ds As DataSet, ByVal arr As ArrayList)

        'START YANAI 音声バッチは1回しか呼ばないようにする
        If arr.Count = 0 Then
            Exit Sub
        End If
        'END YANAI 音声バッチは1回しか呼ばないようにする

        Dim setDs As DataSet = New LMC830DS
        Dim setDt As DataTable = setDs.Tables("LMC830IN")
        Dim setDr As DataRow = setDt.NewRow()
        Dim sysDtTm As String() = MyBase.GetSystemDateTime()
        Dim recNo As Integer = 0

        For i As Integer = 0 To arr.Count - 1

            recNo = Convert.ToInt32(arr(i))
            Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyo.SelectedValue,
                                                                                                                             "' AND CUST_CD = '", Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.CUST_CD_L.ColNo)),
                                                                                                                             "' AND SUB_KB = '39'"))

            If 0 < custDetailsDr.Length Then
                'START YANAI 引当エラーは音声CSV出力しない
                'setDt.Clear()
                'setDr.Item("NRS_BR_CD") = ds.Tables("LMC010OUT_OUTKA_M").Rows(i).Item("NRS_BR_CD").ToString()
                'setDr.Item("OUTKA_NO_L") = ds.Tables("LMC010OUT_OUTKA_M").Rows(i).Item("OUTKA_NO_L").ToString()
                'setDr.Item("WH_CD") = ds.Tables("LMC010_OUTKA_M_OUT").Rows(i).Item("WH_CD").ToString()
                setDr = setDt.NewRow()
                setDr.Item("NRS_BR_CD") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
                setDr.Item("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
                setDr.Item("WH_CD") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.WH_CD.ColNo))
                'END YANAI 引当エラーは音声CSV出力しない

                setDr.Item("TOU_HAN_FLG") = "00"
                setDr.Item("SYS_DATE") = sysDtTm(0)
                setDr.Item("SYS_TIME") = Mid(sysDtTm(1), 1, 6)
                setDr.Item("COMPNAME") = Environment.MachineName
                setDr.Item("RPT_FLG") = "05"
                setDt.Rows.Add(setDr)

                'START YANAI 音声バッチは1回しか呼ばないようにする
                ''CSV出力処理呼出
                'prm.ParamDataSet = setDs
                'LMFormNavigate.NextFormNavigate(Me, "LMC830", prm)
                'END YANAI 音声バッチは1回しか呼ばないようにする
            End If
        Next

        'START YANAI 音声バッチは1回しか呼ばないようにする
        'CSV出力処理呼出
        prm.ParamDataSet = setDs
        LMFormNavigate.NextFormNavigate(Me, "LMC830", prm)
        'END YANAI 音声バッチは1回しか呼ばないようにする

    End Sub
    '要望番号:1339 yamanaka 2012.08.27 End

    'START YANAI 20120322 特別梱包個数計算
    ''' <summary>
    ''' 特別梱包個数計算
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub outkaPkgNbKeisan(ByVal frm As LMC010F, ByVal arr As ArrayList)

        _JikkouDs = New LMC010DS()
        Dim setDs As DataSet = _JikkouDs.Copy()
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).NewRow()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_OUTKA_M_IN)
        Dim nrsBrCd As String = String.Empty
        Dim max As Integer = arr.Count - 1
        Dim setDt As DataTable = _JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_IN)
        Dim recNo As Integer = 0
        Dim outkaPkgNb As String = String.Empty
        For i As Integer = 0 To max

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
            outkaPkgNb = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_PKG_NB.ColNo))

            If ("0").Equals(outkaPkgNb) = False Then
                recNo = Convert.ToInt32(arr(i))
                dr("NRS_BR_CD") = nrsBrCd
                dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
                dr("OUTKA_PLAN_DATE") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.OUTKA_PLAN_DATE.ColNo)).Replace("/", "")
                dr("ARR_PLAN_DATE") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.ARR_PLAN_DATE.ColNo)).Replace("/", "")
                dr("SYS_UPD_DATE") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.SYS_UPD_DATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.SYS_UPD_TIME.ColNo))
                dr("ROW_NO") = recNo
                inTbl.Rows.Add(dr)

                '持ちまわっているデータセットにつめなおす
                setDt.ImportRow(inTbl.Rows(0))

            End If

        Next

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        If 0 < setDt.Rows.Count Then
            '対象データありの場合は特別梱包個数計算処理
            Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "UpdateOutkaPkgNb", _JikkouDs)
        Else
            '対象データなし
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E458")
            Exit Sub
        End If

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        'エラー帳票出力の判定
        Call Me.ShowStorePrintData(frm)

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub
    'END YANAI 20120322 特別梱包個数計算

    'START YANAI 要望番号773
    ''' <summary>
    ''' EXCEL出力データ検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SelectHoukokuExcel(ByVal frm As LMC010F, ByVal arr As ArrayList) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMC010DS()

        'InDataSetの場合
        Call SetInHoukokuExcel(frm, rtDs, arr)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectHoukokuExcel")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMCconH.CallWSAAction(DirectCast(frm, Form),
                                                         "LMC010BLF",
                                                         "SelectHoukokuExcel",
                                                         rtDs,
                                                         0,
                                                         -1)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Return rtnDs
        End If

        'メッセージエリアの設定
        '英語化対応
        '20151021 tsunehira add
        MyBase.ShowMessage(frm, "G073")
        'MyBase.ShowMessage(frm, "G002", New String() {"出荷報告・EXCEL作成", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectHoukokuExcel")

        Return rtnDs

    End Function
    'START 中村 20121113 シグマ出荷対応
    ''' <summary>
    ''' シグマ出荷CSV作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SigmaCsvMake(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        Dim prmDs As DataSet = New LMC840DS
        Me._JikkouDs = New LMC010DS
        Dim setDs As DataSet = Me._JikkouDs.Copy()
        ''Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        ''Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_IN_CSV).NewRow()
        ''Dim setDt As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_SIGMA_IN_CSV)
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_SIGMA_IN_CSV).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_SIGMA_IN_CSV)
        Dim max As Integer = arr.Count - 1
        Dim kbnDr() As DataRow = Nothing
        Dim nrsBrCd As String = String.Empty
        Dim unsoCd As String = String.Empty
        Dim custCdL As String = String.Empty

        'システム日付の取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "SetSysDateTime", setDs)
        Dim sysDt As DataTable = rtnDs.Tables(LMC010C.TABLE_SIGMA_SYS_DATETIME)
        If 0 < sysDt.Rows.Count Then

            Me.SysData(LMC010C.SysData.YYYYMMDD) = sysDt.Rows(0).Item("SYS_DATE").ToString()
            Me.SysData(LMC010C.SysData.HHMMSSsss) = sysDt.Rows(0).Item("SYS_TIME").ToString()

        End If

        '保存先のCSVファイルのパス・ファイル名
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C003' AND ",
                                                                                       "KBN_CD = '03'"))
        Dim filePath As String = kbnDr(0).Item("KBN_NM1").ToString
        Dim fileName As String = kbnDr(0).Item("KBN_NM2").ToString

        For i As Integer = 0 To max

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
            unsoCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_CD.ColNo))
            custCdL = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo))

            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C020' AND ",
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ",
                                                                                           "KBN_NM2 = '", unsoCd, "'"))
            If kbnDr.Length = 0 Then
                '区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If

            dr("NRS_BR_CD") = nrsBrCd
            dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr("ROW_NO") = Convert.ToInt32(arr(i))
            dr("FILEPATH") = filePath
            dr("FILENAME") = fileName
            dr("SYS_DATE") = Me.SysData(LMC010C.SysData.YYYYMMDD)
            dr("SYS_TIME") = Me.SysData(LMC010C.SysData.HHMMSSsss)
            dr("CUST_CD_L") = custCdL
            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        If setDt.Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '検索時WSAクラス呼び出し
        prmDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "SelectSigmaCsv", Me._JikkouDs, 0)

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        'シグマCSV出力処理呼出
        prm.ParamDataSet = prmDs
        LMFormNavigate.NextFormNavigate(Me, "LMC840", prm)

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub
    'END 中村 20121113 シグマ出荷対応

    '(2013.01.24)埼玉BP対応 -- START --
    ''' <summary>
    ''' 名鉄CSV作成処理(埼玉)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MeitetuCsvMakeSAITAMA(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        Dim prmDs As DataSet = New LMC850DS
        Me._JikkouDs = New LMC010DS
        Dim setDs As DataSet = Me._JikkouDs.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_IN_CSV).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim max As Integer = arr.Count - 1
        Dim kbnDr() As DataRow = Nothing

        Dim nrsBrCd As String = String.Empty
        Dim unsoCd As String = String.Empty
        Dim unsoBrCd As String = String.Empty

        'システム日付の取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "SetSysDateTime", setDs)
        Dim sysDt As DataTable = rtnDs.Tables(LMC010C.TABLE_NM_SYS_DATETIME)

        If 0 < sysDt.Rows.Count Then
            Me.SysData(LMC010C.SysData.YYYYMMDD) = sysDt.Rows(0).Item("SYS_DATE").ToString()
            Me.SysData(LMC010C.SysData.HHMMSSsss) = sysDt.Rows(0).Item("SYS_TIME").ToString()
        End If

        '保存先のCSVファイルのパス・ファイル名
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C003' AND ",
                                                                                       "KBN_CD = '04'"))
        Dim filePath As String = kbnDr(0).Item("KBN_NM1").ToString
        Dim fileName As String = kbnDr(0).Item("KBN_NM2").ToString

        For i As Integer = 0 To max

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))      '営業所コード
            unsoCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_CD.ColNo))         '運送会社コード
            unsoBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_BR_CD.ColNo))    '運送会社支店コード

            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C021' AND ",
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ",
                                                                                           "KBN_NM2 = '", unsoCd, "' AND ",
                                                                                           "KBN_NM3 = '", unsoBrCd, "'"))
            If kbnDr.Length = 0 Then
                '区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If

            dr("NRS_BR_CD") = nrsBrCd
            dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr("ROW_NO") = Convert.ToInt32(arr(i))
            dr("FILEPATH") = filePath
            dr("FILENAME") = fileName
            dr("SYS_DATE") = Me.SysData(LMC010C.SysData.YYYYMMDD)
            dr("SYS_TIME") = Me.SysData(LMC010C.SysData.HHMMSSsss)
            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        If setDt.Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '検索時WSAクラス呼び出し
        prmDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "SelectMeitetuCsvSaitama", Me._JikkouDs, 0)

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        '名鉄CSV出力処理呼出
        prm.ParamDataSet = prmDs
        LMFormNavigate.NextFormNavigate(Me, "LMC850", prm)

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub
    '(2013.01.24)埼玉BP対応 --  END  --

    '2016/06/16 ヤマト B2 CSV作成処理 -- START --
    ''' <summary>
    ''' ヤマト B2 CSV
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub YamatoB2CsvMake(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        Dim prmDs As DataSet = New LMC890DS
        Me._JikkouDs = New LMC010DS
        Dim setDs As DataSet = Me._JikkouDs.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_IN_CSV).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim max As Integer = arr.Count - 1
        Dim kbnDr() As DataRow = Nothing
        Dim custDDr() As DataRow = Nothing      'ADD 216/08/22

        Dim nrsBrCd As String = String.Empty
        Dim unsoCd As String = String.Empty
        Dim unsoBrCd As String = String.Empty
        Dim custCd As String = String.Empty     'ADD 2016/08/22

        'システム日付の取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "SetSysDateTime", setDs)
        Dim sysDt As DataTable = rtnDs.Tables(LMC010C.TABLE_NM_SYS_DATETIME)

        If 0 < sysDt.Rows.Count Then
            Me.SysData(LMC010C.SysData.YYYYMMDD) = sysDt.Rows(0).Item("SYS_DATE").ToString()
            Me.SysData(LMC010C.SysData.HHMMSSsss) = sysDt.Rows(0).Item("SYS_TIME").ToString()
        End If

        '保存先のCSVファイルのパス・ファイル名
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C003' AND ",
                                                                                       "KBN_CD = '05'"))
        Dim filePath As String = kbnDr(0).Item("KBN_NM1").ToString
        Dim fileName As String = kbnDr(0).Item("KBN_NM2").ToString

        For i As Integer = 0 To max

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))      '営業所コード
            unsoCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_CD.ColNo))         '運送会社コード
            unsoBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_BR_CD.ColNo))    '運送会社支店コード

            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C029' AND ",
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ",
                                                                                           "KBN_NM2 = '", unsoCd, "' AND ",
                                                                                           "KBN_NM3 = '", unsoBrCd, "'"))
            If kbnDr.Length = 0 Then
                '区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If

            dr("NRS_BR_CD") = nrsBrCd
            dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr("ROW_NO") = Convert.ToInt32(arr(i))
            dr("FILEPATH") = filePath
            dr("FILENAME") = fileName
            dr("SYS_DATE") = Me.SysData(LMC010C.SysData.YYYYMMDD)
            dr("SYS_TIME") = Me.SysData(LMC010C.SysData.HHMMSSsss)

            'ADD 2016/08/22 FFEMか判定
            custCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo))    '荷主コードL

            custDDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ",
                                                                                           "CUST_CD = '", custCd, "' AND ",
                                                                                           "SUB_KB = '0M'"))
            If custDDr.Length = 0 Then
                '荷主詳細マスタに設定されていない場合
                If "40".Equals(nrsBrCd) And "00330".Equals(custCd) Then
                    '丸和物産(横浜)の場合
                    dr("CSV_OUTFLG") = "2"                  '2:丸和物産(横浜)
                Else
                    dr("CSV_OUTFLG") = "0"                  '0:標準
                End If
            Else
                dr("CSV_OUTFLG") = "1"                      '1:FFEM版

            End If
            'ADD 2016/10/04 代引き金額の内消費税を求めるため出荷日追加（M_TAX用）
            dr("OUTKA_PLAN_DATE") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_PLAN_DATE.ColNo))
            dr("OUTKA_PLAN_DATE") = dr("OUTKA_PLAN_DATE").ToString.Trim.Replace("/", "")

            If dr("CSV_OUTFLG").ToString().Equals(LMConst.FLG.ON) Then
                dr("CSV_OUTFLG2") = LMConst.FLG.OFF
            Else
                ' サクラファインテック専用出力か否かの判定
                custDDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ",
                                                                                           "CUST_CD = '", custCd, "' AND ",
                                                                                           "SUB_KB = 'A7'"))
                If custDDr.Length = 0 Then
                    ' 荷主詳細マスタに設定されていない場合
                    dr("CSV_OUTFLG2") = LMConst.FLG.OFF
                Else
                    dr("CSV_OUTFLG2") = LMConst.FLG.ON      '1:サクラファインテック専用出力

                End If
            End If

            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        If setDt.Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '検索時WSAクラス呼び出し
        prmDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "SelectYamatoB2Csv", Me._JikkouDs, 0)

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        'ヤマトCSV出力処理呼出
        prm.ParamDataSet = prmDs
        LMFormNavigate.NextFormNavigate(Me, "LMC890", prm)

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub

    '2016/06/16 佐川 e飛伝 CSV作成処理 -- START --
    ''' <summary>
    ''' 佐川 e飛伝 CSV
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SagawaEHidenCsvMake(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        Dim prmDs As DataSet = New LMC900DS
        Me._JikkouDs = New LMC010DS
        Dim setDs As DataSet = Me._JikkouDs.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_IN_CSV).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim max As Integer = arr.Count - 1
        Dim kbnDr() As DataRow = Nothing
        Dim custDDr() As DataRow = Nothing      'ADD 216/08/22

        Dim nrsBrCd As String = String.Empty
        Dim unsoCd As String = String.Empty
        Dim unsoBrCd As String = String.Empty
        Dim custCd As String = String.Empty     'ADD 2016/08/22

        'システム日付の取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "SetSysDateTime", setDs)
        Dim sysDt As DataTable = rtnDs.Tables(LMC010C.TABLE_NM_SYS_DATETIME)

        If 0 < sysDt.Rows.Count Then
            Me.SysData(LMC010C.SysData.YYYYMMDD) = sysDt.Rows(0).Item("SYS_DATE").ToString()
            Me.SysData(LMC010C.SysData.HHMMSSsss) = sysDt.Rows(0).Item("SYS_TIME").ToString()
        End If

        '保存先のCSVファイルのパス・ファイル名
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C003' AND ",
                                                                                       "KBN_CD = '06'"))
        Dim filePath As String = kbnDr(0).Item("KBN_NM1").ToString
        Dim fileName As String = kbnDr(0).Item("KBN_NM2").ToString

        For i As Integer = 0 To max

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))      '営業所コード
            unsoCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_CD.ColNo))         '運送会社コード
            unsoBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_BR_CD.ColNo))    '運送会社支店コード

            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C030' AND ",
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ",
                                                                                           "KBN_NM2 = '", unsoCd, "' AND ",
                                                                                           "KBN_NM3 = '", unsoBrCd, "'"))
            If kbnDr.Length = 0 Then
                '区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If

            dr("NRS_BR_CD") = nrsBrCd
            dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr("ROW_NO") = Convert.ToInt32(arr(i))
            dr("FILEPATH") = filePath
            dr("FILENAME") = fileName
            dr("SYS_DATE") = Me.SysData(LMC010C.SysData.YYYYMMDD)
            dr("SYS_TIME") = Me.SysData(LMC010C.SysData.HHMMSSsss)

            'ADD 2016/08/22 FFEMか判定
            custCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo))    '荷主コードL

            custDDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ",
                                                                                           "CUST_CD = '", custCd, "' AND ",
                                                                                           "SUB_KB = '0M'"))
            If custDDr.Length = 0 Then
                '荷主詳細マスタに設定されていない場合
                dr("CSV_OUTFLG") = LMConst.FLG.OFF          '0:標準
            Else
                dr("CSV_OUTFLG") = LMConst.FLG.ON           '1:FFEM版

            End If

            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        If setDt.Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '検索時WSAクラス呼び出し
        prmDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "SelectSagawaEHidenCsv", Me._JikkouDs, 0)

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        '佐川CSV出力処理呼出
        prm.ParamDataSet = prmDs
        LMFormNavigate.NextFormNavigate(Me, "LMC900", prm)

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub

    '2017/02/24 エスライン CSV作成処理 -- START --
    ''' <summary>
    ''' エスライン CSV
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SLineCsvMake(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        Dim prmDs As DataSet = New LMC910DS
        Me._JikkouDs = New LMC010DS
        Dim setDs As DataSet = Me._JikkouDs.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_IN_CSV).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim max As Integer = arr.Count - 1
        Dim kbnDr() As DataRow = Nothing
        Dim custDDr() As DataRow = Nothing

        Dim nrsBrCd As String = String.Empty
        Dim unsoCd As String = String.Empty
        Dim unsoBrCd As String = String.Empty
        Dim custCd As String = String.Empty

        'システム日付の取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "SetSysDateTime", setDs)
        Dim sysDt As DataTable = rtnDs.Tables(LMC010C.TABLE_NM_SYS_DATETIME)

        If 0 < sysDt.Rows.Count Then
            Me.SysData(LMC010C.SysData.YYYYMMDD) = sysDt.Rows(0).Item("SYS_DATE").ToString()
            Me.SysData(LMC010C.SysData.HHMMSSsss) = sysDt.Rows(0).Item("SYS_TIME").ToString()
        End If

        '保存先のCSVファイルのパス・ファイル名
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C003' AND ",
                                                                                       "KBN_CD = '07'"))
        Dim filePath As String = kbnDr(0).Item("KBN_NM1").ToString
        Dim fileName As String = kbnDr(0).Item("KBN_NM2").ToString

        For i As Integer = 0 To max

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))      '営業所コード
            unsoCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_CD.ColNo))         '運送会社コード
            unsoBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_BR_CD.ColNo))    '運送会社支店コード

            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C033' AND ",
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ",
                                                                                           "KBN_NM2 = '", unsoCd, "' AND ",
                                                                                           "KBN_NM3 = '", unsoBrCd, "'"))
            If kbnDr.Length = 0 Then
                '区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If

            dr("NRS_BR_CD") = nrsBrCd
            dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr("ROW_NO") = Convert.ToInt32(arr(i))
            dr("FILEPATH") = filePath
            dr("FILENAME") = fileName
            dr("SYS_DATE") = Me.SysData(LMC010C.SysData.YYYYMMDD)
            dr("SYS_TIME") = Me.SysData(LMC010C.SysData.HHMMSSsss)

            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        If setDt.Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '検索時WSAクラス呼び出し
        prmDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "SelectSLineCsv", Me._JikkouDs, 0)

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        'エスラインCSV出力処理呼出
        prm.ParamDataSet = prmDs
        LMFormNavigate.NextFormNavigate(Me, "LMC910", prm)

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub

    '2018/07/10 依頼番号 : 001947  　カンガルーマジックCSV作成処理 -- START --
    ''' <summary>
    ''' エスライン CSV
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub KangarooMagicCsvMake(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        Dim prmDs As DataSet = New LMC910DS
        Me._JikkouDs = New LMC010DS
        Dim setDs As DataSet = Me._JikkouDs.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_IN_CSV).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim max As Integer = arr.Count - 1
        Dim kbnDr() As DataRow = Nothing
        Dim custDDr() As DataRow = Nothing

        Dim nrsBrCd As String = String.Empty
        Dim unsoCd As String = String.Empty
        Dim unsoBrCd As String = String.Empty
        Dim custCd As String = String.Empty

        'システム日付の取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "SetSysDateTime", setDs)
        Dim sysDt As DataTable = rtnDs.Tables(LMC010C.TABLE_NM_SYS_DATETIME)

        If 0 < sysDt.Rows.Count Then
            Me.SysData(LMC010C.SysData.YYYYMMDD) = sysDt.Rows(0).Item("SYS_DATE").ToString()
            Me.SysData(LMC010C.SysData.HHMMSSsss) = sysDt.Rows(0).Item("SYS_TIME").ToString()
        End If

        '保存先のCSVファイルのパス・ファイル名
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C003' AND ",
                                                                                       "KBN_CD = '08'"))
        Dim filePath As String = kbnDr(0).Item("KBN_NM1").ToString
        Dim fileName As String = kbnDr(0).Item("KBN_NM2").ToString

        For i As Integer = 0 To max

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))      '営業所コード
            unsoCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_CD.ColNo))         '運送会社コード
            unsoBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_BR_CD.ColNo))    '運送会社支店コード
            custCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo))       '荷主コードL

            '作成運送会社の判定
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C035' AND ",
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ",
                                                                                           "KBN_NM2 = '", unsoCd, "' AND ",
                                                                                           "KBN_NM3 = '", unsoBrCd, "'"))
            If kbnDr.Length = 0 Then
                '区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If

            'CSV保存サブフォルダの取得
            Dim subPath As String = String.Empty
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C039' AND ",
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ",
                                                                                           "KBN_NM2 = '", custCd, "'"))
            If kbnDr.Length > 0 Then
                subPath = kbnDr(0).Item("KBN_NM3").ToString
                If Right(subPath, 1) <> "\" Then
                    subPath += "\"
                End If
            End If

            dr("NRS_BR_CD") = nrsBrCd
            dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr("ROW_NO") = Convert.ToInt32(arr(i))
            dr("FILEPATH") = If(String.IsNullOrEmpty(subPath), filePath, Path.Combine(filePath, subPath))
            dr("FILENAME") = fileName
            dr("SYS_DATE") = Me.SysData(LMC010C.SysData.YYYYMMDD)
            dr("SYS_TIME") = Me.SysData(LMC010C.SysData.HHMMSSsss)

            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        If setDt.Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '検索時WSAクラス呼び出し
        prmDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "SelectKangarooMagicCsv", Me._JikkouDs, 0)

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        'ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV出力処理呼出
        prm.ParamDataSet = prmDs
        LMFormNavigate.NextFormNavigate(Me, "LMC920", prm)

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' カンガルーマジックCSV(大黒)作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub KangarooMagicDaikokuCsvMake(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        Dim prmDs As DataSet = New LMC910DS
        Me._JikkouDs = New LMC010DS
        Dim setDs As DataSet = Me._JikkouDs.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_IN_CSV).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim max As Integer = arr.Count - 1
        Dim kbnDr() As DataRow = Nothing
        Dim custDDr() As DataRow = Nothing

        Dim nrsBrCd As String = String.Empty
        Dim unsoCd As String = String.Empty
        Dim unsoBrCd As String = String.Empty
        Dim custCd As String = String.Empty

        'システム日付の取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "SetSysDateTime", setDs)
        Dim sysDt As DataTable = rtnDs.Tables(LMC010C.TABLE_NM_SYS_DATETIME)

        If 0 < sysDt.Rows.Count Then
            Me.SysData(LMC010C.SysData.YYYYMMDD) = sysDt.Rows(0).Item("SYS_DATE").ToString()
            Me.SysData(LMC010C.SysData.HHMMSSsss) = sysDt.Rows(0).Item("SYS_TIME").ToString()
        End If

        '保存先のCSVファイルのパス・ファイル名
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C003' AND ",
                                                                                       "KBN_CD = '08'"))
        Dim filePath As String = kbnDr(0).Item("KBN_NM1").ToString
        Dim fileName As String = kbnDr(0).Item("KBN_NM2").ToString

        For i As Integer = 0 To max

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))      '営業所コード
            unsoCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_CD.ColNo))         '運送会社コード
            unsoBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_BR_CD.ColNo))    '運送会社支店コード
            custCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo))       '荷主コードL

            '作成運送会社の判定
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C035' AND ",
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ",
                                                                                           "KBN_NM2 = '", unsoCd, "' AND ",
                                                                                           "KBN_NM3 = '", unsoBrCd, "'"))
            If kbnDr.Length = 0 Then
                '区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If

            'CSV保存サブフォルダの取得
            Dim subPath As String = String.Empty
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C039' AND ",
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ",
                                                                                           "KBN_NM2 = '", custCd, "'"))
            If kbnDr.Length > 0 Then
                subPath = kbnDr(0).Item("KBN_NM3").ToString
                If Right(subPath, 1) <> "\" Then
                    subPath += "\"
                End If
            End If

            dr("NRS_BR_CD") = nrsBrCd
            dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr("ROW_NO") = Convert.ToInt32(arr(i))
            dr("FILEPATH") = If(String.IsNullOrEmpty(subPath), filePath, Path.Combine(filePath, subPath))
            dr("FILENAME") = fileName
            dr("SYS_DATE") = Me.SysData(LMC010C.SysData.YYYYMMDD)
            dr("SYS_TIME") = Me.SysData(LMC010C.SysData.HHMMSSsss)

            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        If setDt.Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '検索時WSAクラス呼び出し
        prmDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "SelectKangarooMagicDaikokuCsv", Me._JikkouDs, 0)

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        'ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV出力処理呼出
        prm.ParamDataSet = prmDs
        LMFormNavigate.NextFormNavigate(Me, "LMC940", prm)

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' SBS再保管出荷実績取込処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="prm"></param>
    Private Sub SBSSaihokanOutkaImport(ByVal frm As LMC010F, ByVal prm As LMFormData)

        Me._JikkouDs = New LMC010DS
        Dim setDs As DataSet = Me._JikkouDs.Copy()

        Dim tbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_SBS_SAIHOKAN)
        tbl.Clear()

        Dim localPath As String = String.Empty
        Dim localPath2 As String = String.Empty

        Dim syori_DateTime As String = String.Concat(MyBase.GetSystemDateTime(0), Mid(MyBase.GetSystemDateTime(1), 1, 6))

        Dim openFileDialog As New OpenFileDialog()

        openFileDialog.InitialDirectory = "c:\"
        openFileDialog.Filter = "Excel ファイル (*.xl*)|*.xl*"
        openFileDialog.FilterIndex = 1
        openFileDialog.Multiselect = True
        openFileDialog.RestoreDirectory = True
        openFileDialog.Title = "ファイルを選択してください..."

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            'データ取込の開始行番号
            Const ROW_START As Integer = 2

            '選択されたファイル数分ループ
            For iFiles As Integer = 0 To openFileDialog.FileNames.Count - 1
                Dim xlApp As Excel.Application = New Excel.Application()
                Dim xlBooks As Excel.Workbooks = xlApp.Workbooks
                Dim xlBook As Excel.Workbook = Nothing
                Dim xlSheets As Excel.Sheets = Nothing
                Dim xlSheet As Excel.Worksheet = Nothing
                Dim xlCells As Excel.Range = Nothing
                Dim xlCell As Excel.Range = Nothing

                'ファイル名を取得
                localPath = openFileDialog.FileNames(iFiles)

                '取込処理
                Try
                    xlBook = xlBooks.Open(localPath)
                    xlSheets = xlBook.Sheets
                    xlSheet = CType(xlSheets(1), Excel.Worksheet)
                    xlCells = xlSheet.Cells

                    'セル値を取得してDataTableにセットする
                    Dim y As Integer = ROW_START
                    Dim rowNum As Integer = 0

                    Do
                        'Yナンバー(取得)
                        xlCell = CType(xlCells(y, 1), Excel.Range)
                        Dim yNumber As String = If(xlCell.Value Is Nothing, String.Empty, xlCell.Value.ToString)
                        If xlCell IsNot Nothing Then
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCell)
                            xlCell = Nothing
                        End If

                        '終了判定
                        If String.IsNullOrEmpty(yNumber) Then
                            Exit Do
                        End If

                        Dim drow As DataRow = setDs.Tables(LMC010C.TABLE_NM_SBS_SAIHOKAN).NewRow
                        rowNum += 1

                        'Yナンバー(セット)
                        drow("OUTKA_NO_L") = yNumber

                        '運送会社コード
                        xlCell = CType(xlCells(y, 3), Excel.Range)
                        drow("UNSO_CD") = If(xlCell.Value Is Nothing, String.Empty, xlCell.Value.ToString)
                        If xlCell IsNot Nothing Then
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCell)
                            xlCell = Nothing
                        End If

                        '運送会社支店コード
                        xlCell = CType(xlCells(y, 4), Excel.Range)
                        drow("UNSO_BR_CD") = If(xlCell.Value Is Nothing, String.Empty, xlCell.Value.ToString)
                        If xlCell IsNot Nothing Then
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCell)
                            xlCell = Nothing
                        End If

                        '個口
                        xlCell = CType(xlCells(y, 5), Excel.Range)
                        drow("OUTKA_PKG_NB") = If(xlCell.Value Is Nothing, String.Empty, xlCell.Value.ToString)
                        If xlCell IsNot Nothing Then
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCell)
                            xlCell = Nothing
                        End If

                        'お問合せ番号
                        xlCell = CType(xlCells(y, 6), Excel.Range)
                        drow("DENP_NO") = If(xlCell.Value Is Nothing, String.Empty, xlCell.Value.ToString)
                        If xlCell IsNot Nothing Then
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCell)
                            xlCell = Nothing
                        End If

                        drow("ROW_NO") = rowNum.ToString()
                        drow("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
                        drow("LOCALPATH") = localPath.Trim()

                        tbl.Rows.Add(drow)

                        y += 1
                    Loop

                Catch ex As Exception
                    '例外がスローされたら処理強制終了
                    MyBase.ShowMessage(frm, "E547", New String() {"Excel操作"})

                    '処理終了アクション
                    Call Me._LMCconH.EndAction(frm)
                    Exit Sub

                Finally
                    'オブジェクトの解放
                    If xlCell IsNot Nothing Then
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCell)
                        xlCell = Nothing
                    End If
                    If xlCells IsNot Nothing Then
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCells)
                        xlCells = Nothing
                    End If
                    If xlSheet IsNot Nothing Then
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet)
                        xlSheet = Nothing
                    End If
                    If xlSheets IsNot Nothing Then
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheets)
                        xlSheets = Nothing
                    End If
                    If xlBook IsNot Nothing Then
                        xlBook.Close(False)
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook)
                        xlBook = Nothing
                    End If
                    If xlBooks IsNot Nothing Then
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBooks)
                        xlBooks = Nothing
                    End If
                    If xlApp IsNot Nothing Then
                        xlApp.DisplayAlerts = False
                        xlApp.Quit()
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp)
                        xlApp = Nothing
                    End If
                End Try
            Next
        End If

        '取込ファイルにレコードがない
        If tbl.Rows.Count = 0 Then
            'メッセージの表示
            MyBase.ShowMessage(frm, "E656")

            '処理終了アクション
            Call Me._LMCconH.EndAction(frm)
            Exit Sub
        End If

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'WSAクラス呼び出し
        _JikkouDs = MyBase.CallWSA("LMC010BLF", "UpdateSBSSaihokanOutkaImport", setDs)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            'メッセージの表示
            MyBase.ShowMessage(frm, "E235")

            'EXCEL起動()
            MyBase.MessageStoreDownload()

            '処理終了アクション
            Call Me._LMCconH.EndAction(frm)
            Exit Sub
        End If

        'ファイル名変更（処理したファイルに年月日時分秒を付加）
        localPath = String.Empty
        Dim localPathWK As String = String.Empty
        Dim sotRows As DataRow() = _JikkouDs.Tables(LMC010C.TABLE_NM_SBS_SAIHOKAN).Select(String.Empty, "LOCALPATH ASC")

        For Each Row As DataRow In sotRows
            localPathWK = Row.Item("LOCALPATH").ToString.Trim

            If localPath.Equals(localPathWK) = False Then
                localPath = Row.Item("LOCALPATH").ToString.Trim

                '処理日時を拡張子の前に付与する
                Dim sDir As String = Path.GetDirectoryName(localPath)
                Dim sFile As String = Path.GetFileNameWithoutExtension(localPath)
                Dim sExt As String = Path.GetExtension(localPath)
                localPath2 = String.Concat(sDir, "\", sFile, "_", syori_DateTime, sExt)

                System.IO.File.Move(localPath, localPath2)
            End If
        Next

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub

    '2016/06/21 ヤマト Denp_update処理 -- START --
    ''' <summary>
    ''' ヤマト e飛伝 CSVV
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub YamatoDenpNoUpdate(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        Me._JikkouDs = New LMC010DS
        Dim setDs As DataSet = Me._JikkouDs.Copy()

        Dim tbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_DENP_NO)
        tbl.Clear()

        Dim localPath As String = String.Empty
        Dim localPath2 As String = String.Empty

        Dim fileNM As String = String.Empty
        Dim fileNMlPath As String = String.Empty
        Dim syori_DateTime As String = String.Concat(MyBase.GetSystemDateTime(0), Mid(MyBase.GetSystemDateTime(1), 1, 6))

        'ヤマト送状番号設定CSVファイルのパス取得
        Dim kbnDr() As DataRow = Nothing
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C031' AND ",
                                                                                       "KBN_CD = '00'"))
        Dim filePathCSV As String = kbnDr(0).Item("KBN_NM1").ToString

        Dim fileStream As Stream
        Dim openFileDialog As New OpenFileDialog()

        openFileDialog.InitialDirectory = filePathCSV   '"c:\"
        openFileDialog.Filter = "CSV カンマ区切り (*.csv)|*.csv"  '"All files (*.*)|*.*"
        openFileDialog.FilterIndex = 1
        openFileDialog.Multiselect = True
        openFileDialog.RestoreDirectory = True
        openFileDialog.Title = "ファイルを選択してください..."

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            fileStream = openFileDialog.OpenFile()

            If Not (fileStream Is Nothing) Then

                Dim max As Integer = openFileDialog.FileNames.Count - 1
                For i As Integer = 0 To max

                    'ファイル名設定
                    localPath = openFileDialog.FileNames(i)
                    ''openFileDialog.FileNames(i).Clone()


                    fileNM = localPath.Substring(localPath.LastIndexOf("\") + 1)
                    fileNMlPath = localPath.Substring(0, localPath.LastIndexOf("\") + 1)

                    'Using fileReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(localPath, System.Text.Encoding.GetEncoding("shift_jis"))
                    Dim fileReader As New TextFieldParser(localPath, System.Text.Encoding.GetEncoding("shift_jis"))

                    With fileReader

                        .TextFieldType = FileIO.FieldType.Delimited
                        '区切り文字を「,(カンマ)」に設定します
                        .SetDelimiters(",")

                        Dim intRow As New Integer
                        Dim currentRow As String()
                        While Not .EndOfData
                            Try
                                intRow = intRow + 1     '行カウント
                                currentRow = .ReadFields()
                                Dim currentField As String
                                Dim intNo As New Integer
                                'UPD 2016/06/29 1行目より処理する
                                'If intRow > 1 Then
                                If intRow > 0 Then
                                    '２行目よりデータ設定

                                    Dim drow As DataRow = setDs.Tables(LMC010C.TABLE_NM_DENP_NO).NewRow

                                    For Each currentField In currentRow
                                        'フィールドを表示
                                        intNo = intNo + 1
                                        Select Case intNo
                                            '--お客様管理番号
                                            Case CInt(1)
                                                drow("OUTKA_NO_L") = CStr(currentField)

                                                '--お客様管理ナンバー
                                            Case CInt(4)
                                                drow("DENP_NO") = CStr(currentField)

                                            Case CInt(38)

                                                drow("OUTKA_PKG_NB") = CStr(currentField)
                                                Exit For
                                        End Select
                                    Next

                                    drow("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
                                    drow("LOCALPATH") = localPath.Trim
                                    'drow("OUTKA_PKG_NB") = "999".ToString

                                    tbl.Rows.Add(drow)

                                End If

                            Catch ex As MalformedLineException
                                'ファイル構造エラー
                                ''MsgBox(ex.Message)
                                Exit For

                            End Try
                        End While

                    End With

                    fileStream.Close()
                    fileReader.Close()

                Next

            End If
        End If

        If tbl.Rows.Count = 0 Then
            MyBase.ShowMessage(frm, "E656")
            Exit Sub

        End If

        '出荷管理番号チェック
        _JikkouDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "CheckOutka_No_L", setDs, 0)

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            Exit Sub
        End If
        '上記メッセージが有効にならないので
        If _JikkouDs.Tables("LMC010IN_OUTKA_DENP_NO").Rows.Count = 0 Then
            MyBase.ShowMessage(frm, "E079", New String() {"出荷", "CSVの出荷管理番号"})
            Exit Sub

        End If

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '検索時WSAクラス呼び出し UpdateYamatoDenp_NO
        '_JikkouDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "UpdateYamatoDenp_NO", setDs, 0)
        _JikkouDs = MyBase.CallWSA("LMC010BLF", "UpdateYamatoDenp_NO", setDs)

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        'ファイル名変更（処理したファイルに年月日時分秒を付加）
        localPath = String.Empty
        Dim localPathWK As String = String.Empty
        Dim sotRows As DataRow() = _JikkouDs.Tables("LMC010IN_OUTKA_DENP_NO").Select(String.Empty, "LOCALPATH ASC")

        For Each Row As DataRow In sotRows
            'Me._Ds.Tables("LMB020_KENPIN_DATA").ImportRow(Row)

            localPathWK = Row.Item("LOCALPATH").ToString.Trim

            If localPath.Equals(localPathWK) = False Then
                localPath = Row.Item("LOCALPATH").ToString.Trim

                localPath2 = localPath.Replace(".csv", String.Concat("_", syori_DateTime, ".csv"))
                localPath2 = localPath2.Replace(".CSV", String.Concat("_", syori_DateTime, ".CSV"))

                System.IO.File.Move(localPath, localPath2)
            End If

        Next

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub


    '2016/06/21 佐川 e飛伝 Denp_update処理 -- START --
    ''' <summary>
    ''' 佐川 e飛伝 CSVV
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SagawaEHidenDenpNoUpdate(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        Me._JikkouDs = New LMC010DS
        Dim setDs As DataSet = Me._JikkouDs.Copy()

        Dim tbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_DENP_NO)
        tbl.Clear()

        Dim localPath As String = String.Empty
        Dim localPath2 As String = String.Empty

        Dim fileNM As String = String.Empty
        Dim fileNMlPath As String = String.Empty

        Dim syori_DateTime As String = String.Concat(MyBase.GetSystemDateTime(0), Mid(MyBase.GetSystemDateTime(1), 1, 6))

        '佐川送状番号設定CSVファイルのパス取得
        Dim kbnDr() As DataRow = Nothing
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C031' AND ",
                                                                                       "KBN_CD = '01'"))
        Dim filePathCSV As String = kbnDr(0).Item("KBN_NM1").ToString

        Dim fileStream As Stream
        Dim openFileDialog As New OpenFileDialog()

        openFileDialog.InitialDirectory = filePathCSV   '"c:\"
        openFileDialog.Filter = "CSV カンマ区切り (*.csv)|*.csv"  '"All files (*.*)|*.*"
        openFileDialog.FilterIndex = 1
        openFileDialog.RestoreDirectory = True
        openFileDialog.Multiselect = True
        openFileDialog.Title = "ファイルを選択してください..."

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            fileStream = openFileDialog.OpenFile()
            If Not (fileStream Is Nothing) Then

                Dim max As Integer = openFileDialog.FileNames.Count - 1
                For i As Integer = 0 To max

                    'ファイル名設定
                    localPath = openFileDialog.FileNames(i)
                    fileNM = localPath.Substring(localPath.LastIndexOf("\") + 1)

                    Using fileReader As New TextFieldParser(localPath, System.Text.Encoding.GetEncoding("Shift_JIS"))

                        With fileReader

                            .TextFieldType = FileIO.FieldType.Delimited
                            '区切り文字を「,(カンマ)」に設定します
                            .SetDelimiters(",")

                            Dim intRow As New Integer
                            Dim currentRow As String()
                            While Not .EndOfData
                                Try
                                    intRow = intRow + 1     '行カウント
                                    currentRow = .ReadFields()
                                    Dim currentField As String
                                    Dim intNo As New Integer

                                    If intRow > 1 Then
                                        '２行目よりデータ設定

                                        Dim sDelKBN As String = String.Empty

                                        Dim drow As DataRow = setDs.Tables(LMC010C.TABLE_NM_DENP_NO).NewRow

                                        For Each currentField In currentRow
                                            'フィールドを表示
                                            intNo = intNo + 1
                                            Select Case intNo
                                                '--お問合せ送り状№
                                                Case CInt(1)
                                                    drow("DENP_NO") = CStr(currentField)

                                                    '--お客様管理ナンバー
                                                Case CInt(13)
                                                    drow("OUTKA_NO_L") = CStr(currentField)

                                                    '--出荷個数 ADD 2016/07/14
                                                Case CInt(35)
                                                    drow("OUTKA_PKG_NB") = CStr(currentField)

                                                    '--削除 ADD 2016/08/03
                                                Case CInt(53)
                                                    sDelKBN = CStr(currentField)

                                                    Exit For
                                            End Select
                                        Next

                                        'UPD 2016/08/03 削除データを対象にしない様に修正
                                        If ("0").Equals(sDelKBN) Then
                                            drow("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
                                            drow("LOCALPATH") = localPath.Trim

                                            tbl.Rows.Add(drow)

                                        End If

                                    End If

                                Catch ex As MalformedLineException
                                    'ファイル構造エラー
                                    ''MsgBox(ex.Message)
                                    Exit For

                                End Try

                            End While

                        End With
                        fileStream.Close()
                        fileReader.Close()

                    End Using

                Next

            End If
        End If

        If tbl.Rows.Count = 0 Then
            MyBase.ShowMessage(frm, "E656")
            Exit Sub

        End If

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '出荷管理番号チェック
        _JikkouDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "CheckOutka_No_L", setDs, 0)

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            Exit Sub
        End If
        '上記メッセージが有効にならないので
        If _JikkouDs.Tables("LMC010IN_OUTKA_DENP_NO").Rows.Count = 0 Then
            MyBase.ShowMessage(frm, "E079", New String() {"出荷", "CSVの出荷管理番号"})
            Exit Sub

        End If

        '検索時WSAクラス呼び出し UpdateYamatoDenp_NO
        '_JikkouDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "UpdateSagawaDenp_NO", setDs, 0)
        _JikkouDs = MyBase.CallWSA("LMC010BLF", "UpdateSagawaDenp_NO", setDs)

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        'ファイル名変更（処理したファイルに年月日時分秒を付加）
        localPath = String.Empty
        Dim localPathWK As String = String.Empty
        Dim sotRows As DataRow() = _JikkouDs.Tables("LMC010IN_OUTKA_DENP_NO").Select(String.Empty, "LOCALPATH ASC")

        For Each Row As DataRow In sotRows
            'Me._Ds.Tables("LMB020_KENPIN_DATA").ImportRow(Row)

            localPathWK = Row.Item("LOCALPATH").ToString.Trim

            If localPath.Equals(localPathWK) = False Then
                localPath = Row.Item("LOCALPATH").ToString.Trim

                localPath2 = localPath.Replace(".csv", String.Concat("_", syori_DateTime, ".csv"))
                localPath2 = localPath2.Replace(".CSV", String.Concat("_", syori_DateTime, ".CSV"))

                System.IO.File.Move(localPath, localPath2)
            End If

        Next

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub


    '社内入荷データ作成 terakawa Start
    ''' <summary>
    ''' 社内入荷データ作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SyanaiInkaMake(ByVal frm As LMC010F, ByVal arr As ArrayList)

        'パラメータのインスタンス生成
        Dim ds As DataSet = New LMC010DS()
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim outkaNoL As String = String.Empty
        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty
        Dim intEdiCd As String = frm.cmbIntEdi.SelectedValue.ToString()
        Dim sysUpdDate As String = String.Empty
        Dim sysUpdTime As String = String.Empty
        Dim nrsBrCd As String = String.Empty

        'IN情報を設定
        For i As Integer = 0 To max
            outkaNoL = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)).ToString()
            custCdL = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo)).ToString()
            custCdM = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_M.ColNo)).ToString()
            sysUpdDate = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.SYS_UPD_DATE.ColNo)).ToString()
            sysUpdTime = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.SYS_UPD_TIME.ColNo)).ToString()
            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo)).ToString()

            dr = ds.Tables(LMC010C.TABLE_NM_INKA_IN).NewRow()
            With dr
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                .Item("NRS_BR_CD") = nrsBrCd
                .Item("OUTKA_NO_L") = outkaNoL
                .Item("CUST_CD_L") = custCdL
                .Item("CUST_CD_M") = custCdM
                .Item("INT_EDI_CD") = intEdiCd
                .Item("SYS_UPD_DATE") = sysUpdDate
                .Item("SYS_UPD_TIME") = sysUpdTime
                .Item("ROW_NO") = arr(i)
            End With
            ds.Tables(LMC010C.TABLE_NM_INKA_IN).Rows.Add(dr)
        Next

        '実行時WSAクラス呼び出し
        ds = MyBase.CallWSA("LMC010BLF", "MakeInkaData", ds)

        If MyBase.IsMessageStoreExist() = True Then
            '処理終了メッセージの表示
            Me.ShowStorePrintData(frm)
            Exit Sub
        End If

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G071")
        'MyBase.ShowMessage(frm, "G002", New String() {"社内入荷データ作成", String.Empty})

    End Sub
    '社内入荷データ作成 terakawa End

    ''' <summary>
    ''' 出荷報告・EXCEL出力データ出力処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub MakeHoukokuExcel(ByVal frm As LMC010F, ByVal ds As DataSet)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        If ds.Tables(LMC010C.TABLE_NM_OUT_HOUKOKU_EXCEL).Rows.Count = 0 Then
            '英語化対応
            '20151021 tsunehira add
            MyBase.ShowMessage(frm, "E664")
            'MyBase.ShowMessage(frm, "E296", New String() {"Excel出力対象データ"})
            Exit Sub
        End If

        'システム日付の取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "SetSysDateTime", ds)
        Dim sysDt As DataTable = rtnDs.Tables(LMC010C.TABLE_NM_SYS_DATETIME)

        Dim xlsPathKbn As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'E022'")
        Dim xlsPath As String = xlsPathKbn(0).Item("KBN_NM1").ToString
        Dim xlsFileName As String = String.Concat(xlsPathKbn(0).Item("KBN_NM2").ToString,
                                                  sysDt.Rows(0).Item("SYS_DATE").ToString(),
                                                  "_",
                                                  sysDt.Rows(0).Item("SYS_TIME").ToString(),
                                                  ".xls")

        With ds.Tables(LMC010C.TABLE_NM_OUT_HOUKOKU_EXCEL)

            'DataSetの値を二次元配列に格納する
            Dim rowMax As Integer = .Rows.Count - 1
            Dim colMax As Integer = LMC010C.HOUKOKU_COL
            Dim excelData(0, 0) As Object
            ReDim excelData(rowMax + 1, colMax + 1)
            Dim titleRow As Integer = 0

            '2017/09/25 修正 李↓
            'タイトル列を設定
            excelData(titleRow, 0) = lgm.Selector({LMC010C.HOUKOKU_COL_01, LMC010C.HOUKOKU_COL_01_ENG, LMC010C.HOUKOKU_COL_01_KR, LMC010C.HOUKOKU_COL_01_ENG})
            excelData(titleRow, 1) = lgm.Selector({LMC010C.HOUKOKU_COL_02, LMC010C.HOUKOKU_COL_02_ENG, LMC010C.HOUKOKU_COL_02_KR, LMC010C.HOUKOKU_COL_02_ENG})
            excelData(titleRow, 2) = lgm.Selector({LMC010C.HOUKOKU_COL_03, LMC010C.HOUKOKU_COL_03_ENG, LMC010C.HOUKOKU_COL_03_KR, LMC010C.HOUKOKU_COL_03_ENG})
            excelData(titleRow, 3) = lgm.Selector({LMC010C.HOUKOKU_COL_04, LMC010C.HOUKOKU_COL_04_ENG, LMC010C.HOUKOKU_COL_04_KR, LMC010C.HOUKOKU_COL_04_ENG})
            excelData(titleRow, 4) = lgm.Selector({LMC010C.HOUKOKU_COL_05, LMC010C.HOUKOKU_COL_05_ENG, LMC010C.HOUKOKU_COL_05_KR, LMC010C.HOUKOKU_COL_05_ENG})
            excelData(titleRow, 5) = lgm.Selector({LMC010C.HOUKOKU_COL_06, LMC010C.HOUKOKU_COL_06_ENG, LMC010C.HOUKOKU_COL_06_KR, LMC010C.HOUKOKU_COL_06_ENG})
            excelData(titleRow, 6) = lgm.Selector({LMC010C.HOUKOKU_COL_07, LMC010C.HOUKOKU_COL_07_ENG, LMC010C.HOUKOKU_COL_07_KR, LMC010C.HOUKOKU_COL_07_ENG})
            excelData(titleRow, 7) = lgm.Selector({LMC010C.HOUKOKU_COL_08, LMC010C.HOUKOKU_COL_08_ENG, LMC010C.HOUKOKU_COL_08_KR, LMC010C.HOUKOKU_COL_08_ENG})
            excelData(titleRow, 8) = lgm.Selector({LMC010C.HOUKOKU_COL_09, LMC010C.HOUKOKU_COL_09_ENG, LMC010C.HOUKOKU_COL_09_KR, LMC010C.HOUKOKU_COL_09_ENG})
            excelData(titleRow, 9) = lgm.Selector({LMC010C.HOUKOKU_COL_10, LMC010C.HOUKOKU_COL_10_ENG, LMC010C.HOUKOKU_COL_10_KR, LMC010C.HOUKOKU_COL_10_ENG})
            excelData(titleRow, 10) = lgm.Selector({LMC010C.HOUKOKU_COL_11, LMC010C.HOUKOKU_COL_11_ENG, LMC010C.HOUKOKU_COL_11_KR, LMC010C.HOUKOKU_COL_11_ENG})
            titleRow = 1
            '2017/09/25 修正 李↑

            MyBase.Logger.WriteLog(0, "LMH010H", "SetCachedName", lgm.Selector({"キャッシュ取得", "Cash acquisition", "캐쉬취득", "中国語"}))

            '値を設定
            For i As Integer = 0 To rowMax
                excelData(i + titleRow, 0) = .Rows(i).Item("CUST_ORD_NO").ToString
                excelData(i + titleRow, 1) = .Rows(i).Item("CUST_ORD_NO_DTL").ToString
                excelData(i + titleRow, 2) = .Rows(i).Item("OUTKA_PLAN_DATE").ToString
                excelData(i + titleRow, 3) = .Rows(i).Item("DEST_CD").ToString
                excelData(i + titleRow, 4) = .Rows(i).Item("GOODS_CD_CUST").ToString
                excelData(i + titleRow, 5) = .Rows(i).Item("GOODS_NM_1").ToString
                excelData(i + titleRow, 6) = .Rows(i).Item("LOT_NO").ToString
                excelData(i + titleRow, 7) = Convert.ToDouble(.Rows(i).Item("OUTKA_TTL_QT"))
                excelData(i + titleRow, 8) = .Rows(i).Item("DENP_NO").ToString
                excelData(i + titleRow, 9) = String.Concat(.Rows(i).Item("DEST_NM").ToString,
                                                           Space(1),
                                                           .Rows(i).Item("DEST_AD_1").ToString,
                                                           Space(1),
                                                           .Rows(i).Item("DEST_AD_2").ToString,
                                                           Space(1),
                                                           .Rows(i).Item("DEST_AD_3").ToString
                                                           )
                excelData(i + titleRow, 10) = .Rows(i).Item("UNSOCO_NM").ToString
            Next

            'EXCEL起動
            Dim xlsApp As New Excel.Application
            Dim xlsBook As Excel.Workbook = Nothing
            Dim xlsSheets As Excel.Sheets = Nothing
            Dim xlsSheet As Excel.Worksheet = Nothing

            Dim startCell As Excel.Range = Nothing
            Dim endCell As Excel.Range = Nothing
            Dim range As Excel.Range = Nothing
            Dim rowCnt As Integer = 0

            xlsBook = xlsApp.Workbooks.Add()
            xlsSheet = DirectCast(xlsBook.Worksheets(1), Excel.Worksheet)
            startCell = DirectCast(xlsSheet.Cells(1, 1), Excel.Range)
            endCell = DirectCast(xlsSheet.Cells(rowMax + 2, colMax + 1), Excel.Range)

            'スタイル設定
            For j As Integer = 2 To rowMax + 2
                For i As Integer = 1 To colMax
                    Me.SetStyleData(xlsSheet.Range(xlsSheet.Cells(j, i), xlsSheet.Cells(rowMax + 2, i)), i)
                Next
            Next

            range = xlsSheet.Range(startCell, endCell)
            range.Value = excelData

            xlsApp.DisplayAlerts = False '保存時の問合せのダイアログを非表示に設定

            Try
                '保存時、上書き確認ポップで「いいえ」「キャンセル」選択時にエラーになるため
                System.IO.Directory.CreateDirectory(xlsPath)
                xlsBook.SaveAs(String.Concat(xlsPath, xlsFileName))
            Catch ex As Exception

            End Try
            xlsApp.DisplayAlerts = True      '元に戻す

            xlsSheets = Nothing
            xlsSheet = Nothing
            xlsBook.Close(False) 'Excelを閉じる
            xlsBook = Nothing
            xlsApp.Quit()
            xlsApp = Nothing

        End With

    End Sub

    ''' <summary>
    ''' 新潟運輸SCV作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub NiigataUnyuCsvMake(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        Dim prmDs As DataSet = New LMC950DS
        Me._JikkouDs = New LMC010DS
        Dim setDs As DataSet = Me._JikkouDs.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_IN_CSV).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim max As Integer = arr.Count - 1
        Dim kbnDr() As DataRow = Nothing
        Dim custDDr() As DataRow = Nothing

        Dim nrsBrCd As String = String.Empty
        Dim unsoCd As String = String.Empty
        Dim unsoBrCd As String = String.Empty
        Dim custCd As String = String.Empty

        ' システム日付の取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "SetSysDateTime", setDs)
        Dim sysDt As DataTable = rtnDs.Tables(LMC010C.TABLE_NM_SYS_DATETIME)

        If 0 < sysDt.Rows.Count Then
            Me.SysData(LMC010C.SysData.YYYYMMDD) = sysDt.Rows(0).Item("SYS_DATE").ToString()
            Me.SysData(LMC010C.SysData.HHMMSSsss) = sysDt.Rows(0).Item("SYS_TIME").ToString()
        End If

        ' 保存先の CSVファイルのパス・ファイル名
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C003' AND ",
                                                                                       "KBN_CD = '09'"))
        Dim filePath As String = kbnDr(0).Item("KBN_NM1").ToString
        Dim fileName As String = kbnDr(0).Item("KBN_NM2").ToString

        For i As Integer = 0 To max

            ' 別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))      '営業所コード
            unsoCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_CD.ColNo))         '運送会社コード
            unsoBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_BR_CD.ColNo))    '運送会社支店コード
            custCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo))       '荷主コードL

            ' 作成運送会社の判定
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C041' AND ",
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ",
                                                                                           "KBN_NM2 = '", unsoCd, "' AND ",
                                                                                           "KBN_NM3 = '", unsoBrCd, "'"))
            If kbnDr.Length = 0 Then
                ' 区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If

            dr("NRS_BR_CD") = nrsBrCd
            dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr("ROW_NO") = Convert.ToInt32(arr(i))
            dr("FILEPATH") = filePath
            dr("FILENAME") = fileName
            dr("SYS_DATE") = Me.SysData(LMC010C.SysData.YYYYMMDD)
            dr("SYS_TIME") = Me.SysData(LMC010C.SysData.HHMMSSsss)

            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        If setDt.Rows.Count = 0 Then
            ' 処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        ' メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        ' 検索時WSAクラス呼び出し
        prmDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "SelectNiigataUnyuCsv", Me._JikkouDs, 0)

        If MyBase.IsMessageExist() = True Then
            ' 処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        ' 新潟運輸 CSV出力処理呼出
        prm.ParamDataSet = prmDs
        LMFormNavigate.NextFormNavigate(Me, "LMC950", prm)

        ' 処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        ' 処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 【神原】オカケンメイトCSV作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub KanbaraOkakenCsvMake(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        Dim prmDs As DataSet = New LMC810DS
        Me._JikkouDs = New LMC010DS()
        Dim setDs As DataSet = Me._JikkouDs.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_IN_CSV).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim max As Integer = arr.Count - 1
        Dim kbnDr() As DataRow = Nothing
        Dim nrsBrCd As String = String.Empty
        Dim unsoCd As String = String.Empty

        'システム日付の取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "SetSysDateTime", setDs)
        Dim sysDt As DataTable = rtnDs.Tables(LMC010C.TABLE_NM_SYS_DATETIME)
        If 0 < sysDt.Rows.Count Then

            Me.SysData(LMC010C.SysData.YYYYMMDD) = sysDt.Rows(0).Item("SYS_DATE").ToString()
            Me.SysData(LMC010C.SysData.HHMMSSsss) = sysDt.Rows(0).Item("SYS_TIME").ToString()

        End If

        '保存先のCSVファイルのパス・ファイル名
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C003' AND ",
                                                                                       "KBN_CD = '10'"))
        Dim filePath As String = kbnDr(0).Item("KBN_NM1").ToString
        Dim fileName As String = kbnDr(0).Item("KBN_NM2").ToString

        For i As Integer = 0 To max

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
            unsoCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_CD.ColNo))

            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C042' AND ",
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ",
                                                                                           "KBN_NM2 = '", unsoCd, "'"))
            If kbnDr.Length = 0 Then
                '区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If

            dr("NRS_BR_CD") = nrsBrCd
            dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr("ROW_NO") = Convert.ToInt32(arr(i))
            dr("FILEPATH") = filePath
            dr("FILENAME") = fileName
            dr("SYS_DATE") = Me.SysData(LMC010C.SysData.YYYYMMDD)
            dr("SYS_TIME") = Me.SysData(LMC010C.SysData.HHMMSSsss)
            dr("CSV_OUTFLG") = LMConst.FLG.ON           '1:神原
            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        If setDt.Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        '検索時WSAクラス呼び出し
        prmDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "SelectOkakenCsv", Me._JikkouDs, 0)

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        '岡山貨物CSV出力処理呼出
        prm.ParamDataSet = prmDs
        LMFormNavigate.NextFormNavigate(Me, "LMC810", prm)

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' トールSCV作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub TollCsvMake(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        Dim prmDs As DataSet = New LMC960DS
        Me._JikkouDs = New LMC010DS
        Dim setDs As DataSet = Me._JikkouDs.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_IN_CSV).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim max As Integer = arr.Count - 1
        Dim kbnDr() As DataRow = Nothing
        Dim custDDr() As DataRow = Nothing

        Dim nrsBrCd As String = String.Empty
        Dim unsoCd As String = String.Empty
        Dim unsoBrCd As String = String.Empty

        ' システム日付の取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "SetSysDateTime", setDs)
        Dim sysDt As DataTable = rtnDs.Tables(LMC010C.TABLE_NM_SYS_DATETIME)

        If 0 < sysDt.Rows.Count Then
            Me.SysData(LMC010C.SysData.YYYYMMDD) = sysDt.Rows(0).Item("SYS_DATE").ToString()
            Me.SysData(LMC010C.SysData.HHMMSSsss) = sysDt.Rows(0).Item("SYS_TIME").ToString()
        End If

        ' 保存先の CSVファイルのパス・ファイル名
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C003' AND ",
                                                                                       "KBN_CD = '11'"))
        Dim filePath As String = kbnDr(0).Item("KBN_NM1").ToString
        Dim fileName As String = kbnDr(0).Item("KBN_NM2").ToString

        For i As Integer = 0 To max

            ' 別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))      '営業所コード
            unsoCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_CD.ColNo))         '運送会社コード
            unsoBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_BR_CD.ColNo))    '運送会社支店コード

            ' 作成運送会社の判定
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C043' AND ",
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ",
                                                                                           "KBN_NM2 = '", unsoCd, "' AND ",
                                                                                           "KBN_NM3 = '", unsoBrCd, "'"))
            If kbnDr.Length = 0 Then
                ' 区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If

            dr("NRS_BR_CD") = nrsBrCd
            dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr("ROW_NO") = Convert.ToInt32(arr(i))
            dr("FILEPATH") = filePath
            dr("FILENAME") = fileName
            dr("SYS_DATE") = Me.SysData(LMC010C.SysData.YYYYMMDD)
            dr("SYS_TIME") = Me.SysData(LMC010C.SysData.HHMMSSsss)

            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        If setDt.Rows.Count = 0 Then
            ' 処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        ' メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        ' 検索時WSAクラス呼び出し
        prmDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "SelectTollCsv", Me._JikkouDs, 0)

        If MyBase.IsMessageExist() = True Then
            ' 処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        ' トール CSV出力処理呼出
        prm.ParamDataSet = prmDs
        LMFormNavigate.NextFormNavigate(Me, "LMC960", prm)

        ' 処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        ' 処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 佐川 e飛伝Ⅲ CSV
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SagawaEHiden3CsvMake(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        Dim prmDs As DataSet = New LMC903DS
        Me._JikkouDs = New LMC010DS
        Dim setDs As DataSet = Me._JikkouDs.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_IN_CSV).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim max As Integer = arr.Count - 1
        Dim kbnDr() As DataRow = Nothing
        Dim custDDr() As DataRow = Nothing

        Dim nrsBrCd As String = String.Empty
        Dim unsoCd As String = String.Empty
        Dim unsoBrCd As String = String.Empty

        ' システム日付の取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "SetSysDateTime", setDs)
        Dim sysDt As DataTable = rtnDs.Tables(LMC010C.TABLE_NM_SYS_DATETIME)

        If 0 < sysDt.Rows.Count Then
            Me.SysData(LMC010C.SysData.YYYYMMDD) = sysDt.Rows(0).Item("SYS_DATE").ToString()
            Me.SysData(LMC010C.SysData.HHMMSSsss) = sysDt.Rows(0).Item("SYS_TIME").ToString()
        End If

        ' 保存先の CSVファイルのパス・ファイル名
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C003' AND ",
                                                                                       "KBN_CD = '13'"))
        Dim filePath As String = kbnDr(0).Item("KBN_NM1").ToString
        Dim fileName As String = kbnDr(0).Item("KBN_NM2").ToString

        For i As Integer = 0 To max

            ' 別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))      '営業所コード
            unsoCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_CD.ColNo))         '運送会社コード
            unsoBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_BR_CD.ColNo))    '運送会社支店コード

            ' 作成運送会社の判定
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C044' AND ",
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ",
                                                                                           "KBN_NM2 = '", unsoCd, "' AND ",
                                                                                           "KBN_NM3 = '", unsoBrCd, "'"))
            If kbnDr.Length = 0 Then
                ' 区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If

            dr("NRS_BR_CD") = nrsBrCd
            dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr("ROW_NO") = Convert.ToInt32(arr(i))
            dr("FILEPATH") = filePath
            dr("FILENAME") = fileName
            dr("SYS_DATE") = Me.SysData(LMC010C.SysData.YYYYMMDD)
            dr("SYS_TIME") = Me.SysData(LMC010C.SysData.HHMMSSsss)

            inTbl.Rows.Add(dr)

            ' 持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        If setDt.Rows.Count = 0 Then
            ' 処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        ' メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        ' 検索時WSAクラス呼び出し
        prmDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "SelectSagawaEHiden3Csv", Me._JikkouDs, 0)

        If MyBase.IsMessageExist() = True Then
            ' 処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        ' 佐川 e飛伝Ⅲ CSV出力処理呼出
        prm.ParamDataSet = prmDs
        LMFormNavigate.NextFormNavigate(Me, "LMC903", prm)

        ' 処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        ' 処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' X-Track出荷登録処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub XTrackOutkaTorokuShori(ByVal frm As LMC010F)

        Dim process As System.Diagnostics.Process = Nothing
        Dim exePath As String = String.Empty
        Dim arguments As String = String.Empty
        Dim kbnDr() As DataRow = Nothing

        Try
            '区分マスタから出荷バッチ実行EXEのパスを取得
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'S038' AND ",
                                                                                           "KBN_CD = '32'"))
            If 0 < kbnDr.Length Then
                exePath = kbnDr(0).Item("KBN_NM2").ToString
            End If

            '引数の設定　営業所コード、倉庫コード、荷主コード
            arguments = String.Format("{0} {1} {2} {3} {4}", "LBC200", frm.cmbEigyo.SelectedValue.ToString(), frm.cmbSoko.SelectedValue.ToString(), frm.txtCustCD.TextValue(), LMUserInfoManager.GetUserID())

            '出荷バッチアプリケーションの起動
            process = System.Diagnostics.Process.Start(exePath, arguments)

            '終了するまで待機する
            process.WaitForExit()

            '終了コードを取得する
            Dim ExitCode As Integer = process.ExitCode

            'プロセスの破棄
            process.Close()
            process.Dispose()

            '処理終了メッセージの表示
            Select Case ExitCode
                Case 0
                    '正常終了
                    MyBase.ShowMessage(frm, "G002", New String() {"X-Track出荷データの出荷登録", String.Empty})
                Case 1
                    '正常終了(該当データなし)
                    MyBase.ShowMessage(frm, "G001")
                Case 2
                    '引当可能データなし
                    MyBase.ShowMessage(frm, "E192")
                Case 3
                    '出荷登録に成功したが、引当に失敗
                    MyBase.ShowMessage(frm, "E979")
                Case 4
                    'エラーデータあり
                    MyBase.ShowMessage(frm, "E235")
                Case 9
                    '予期せぬエラー
                    MyBase.ShowMessage(frm, "S002")
            End Select

        Catch ex As Exception
            MyBase.Logger.WriteErrorLog(MyBase.GetType.Name _
                          , Reflection.MethodBase.GetCurrentMethod().Name _
                          , ex.Message _
                          , ex)

            '予期せぬエラー
            MyBase.ShowMessage(frm, "S002")
        End Try

    End Sub

    ''' <summary>
    ''' スタイル設定
    ''' </summary>
    ''' <param name="xlRange">設定する範囲</param>
    ''' <param name="colNo">列番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetStyleData(ByVal xlRange As Excel.Range, ByVal colNo As Integer) As Excel.Range

        With xlRange

            Select Case colNo

                Case 8

                    '出荷数
                    .EntireColumn.AutoFit()
                    xlRange = Me.SetStyleNum(xlRange)

                Case Else

                    'その他
                    .EntireColumn.AutoFit()
                    xlRange = Me.SetStyleText(xlRange)

            End Select

        End With

        Return xlRange

    End Function

    ''' <summary>
    ''' 文字スタイル設定
    ''' </summary>
    ''' <param name="xlRange">設定するセル範囲</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetStyleText(ByVal xlRange As Excel.Range) As Excel.Range
        xlRange.NumberFormatLocal = "@"
        Return xlRange
    End Function

    ''' <summary>
    ''' 数値スタイル設定
    ''' </summary>
    ''' <param name="xlRange">設定するセル範囲</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetStyleNum(ByVal xlRange As Excel.Range) As Excel.Range
        xlRange.NumberFormat = "###0"
        Return xlRange
    End Function

    ''' <summary>
    ''' EXCEL出力時の進捗区分更新処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function UpdateHoukokuExcel(ByVal frm As LMC010F, ByVal arr As ArrayList) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMC010DS()

        'InDataSetの場合
        Call SetInHoukokuExcel(frm, rtDs, arr)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateHoukokuExcel")

        '==========================
        'WSAクラス呼出
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "UpdateHoukokuExcel", rtDs)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Return rtnDs
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateHoukokuExcel")

        Return rtnDs

    End Function
    'END YANAI 要望番号773

    ''' <summary>
    ''' ダブルコーテーション付加
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDblQuotation(ByVal val As String) As String

        Return String.Concat("""", val, """")

    End Function
    'END YANAI 要望番号627　こぐまくん対応

    'START YANAI 要望番号853 まとめ処理対応
    ''' <summary>
    ''' 在庫データ検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Function SelectZaiDataMATOME(ByVal frm As LMC010F, ByVal ds As DataSet) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMC010DS()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectZaiDataMATOME")

        '==========================
        'WSAクラス呼出
        '==========================
        rtDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form),
                                         "LMC010BLF",
                                         "SelectZaiDataMATOME",
                                         ds,
                                         0,
                                         -1)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectZaiDataMATOME")

        Return rtDs

    End Function
    'END YANAI 要望番号853 まとめ処理対応

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub PrintData(ByVal frm As LMC010F, ByVal lgm As LmLangMGR)

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        'DataSet設定
        Dim ds As DataSet = Me.SetDataSetInPrintData(frm, New LMC010DS())
        Dim rtnDs As DataSet = New LMC010DS()
        Dim rtnDsYer As DataSet = New LMC010DS()        'ADD 2018/11/14 イエローカード用 依頼番号 : 003123   【LMS】一括印刷_出荷データ検索画面でプレビューが表示されない。(

        'START YANAI 要望番号800
        If ("01").Equals(Me._PrintSybetu) = True Then
            '荷札の場合、部数のチェックを行う
            If Me._V.IsPrintNihudaInputCheck(ds) = False Then
                Call Me._LMCconH.EndAction(frm) '終了処理
                Exit Sub
            End If
        End If
        'END YANAI 要望番号800

        rtnDs.Merge(New RdPrevInfoDS)
        rtnDs.Tables(LMConst.RD).Clear()

        'サーバに渡すレコードが存在する場合、更新処理
        If 0 < ds.Tables(LMC010C.TABLE_NM_IN_PRINT).Rows.Count Then

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintData")

            '==== WSAクラス呼出（変更処理） ====
            rtnDs = MyBase.CallWSA("LMC010BLF", "UpdatePrintData", ds)

        End If

        Call Me._LMCconH.EndAction(frm)

        '分析票と一括印刷以外スルー
        'START YANAI 要望番号741
        'If LMC010C.PrintShubetsu.COA = Me._PrintSybetuEnum OrElse _
        '   LMC010C.PrintShubetsu.ALL_PRINT = Me._PrintSybetuEnum Then
        If LMC010C.PrintShubetsu.COA = Me._PrintSybetuEnum Then
            'END YANAI 要望番号741

            '分析票の共通部品呼び出し
            Dim strPass() As String
            strPass = DirectCast(Me._BunsekiArr.ToArray(GetType(String)), String())

            If strPass.Length <> 0 Then
                ' リモートの 分析票 PDF ファイルのローカルへのコピー 
                strPass = CopyRemotePdf(frm, strPass)
                If MyBase.PDFDirectPrint(strPass) = False Then

                    '分析表の場合のみ、エラー帳票出力処理を行う
                    If LMC010C.PrintShubetsu.COA = Me._PrintSybetuEnum Then
                        ' PDF 直接印刷共通部品でのエラー発生時のメッセージ編集処理
                        Call ShowStorePrintErrorData(frm, lgm, Me._ChkList)

                        Me.ShowStorePrintData(frm)
                        '処理終了アクション
                        Call Me._LMCconH.EndAction(frm)
                        Exit Sub
                    End If

                End If

                ' 分析票 ログ出力
                Me.WritePrintLog(frm, strPass, LMC010C.PrintShubetsu.COA)
            End If

            '分析表の場合、ここで処理完了
            If LMC010C.PrintShubetsu.COA = Me._PrintSybetuEnum Then

                '処理終了アクション
                Call Me._LMCconH.EndAction(frm)
                Exit Sub

            End If

        End If

        'イエローカード以外スルー
        If LMC010C.PrintShubetsu.YELLOW_CARD = Me._PrintSybetuEnum Then
            '印刷先プリンタを取得する
            Dim prtNm As String = LMUserInfoManager.GetYellowCardPrt

            'イエローカードの共通部品呼び出し
            Dim strPass() As String = DirectCast(Me._YCardArr.ToArray(GetType(String)), String())

            If strPass.Length <> 0 Then
                ' リモートの イエローカード PDF ファイルのローカルへのコピー 
                strPass = CopyRemotePdf(frm, strPass)
                If MyBase.PDFDirectPrint(strPass, prtNm) = False Then
                    ' PDF 直接印刷共通部品でのエラー発生時のメッセージ編集処理
                    Call ShowStorePrintErrorData(frm, lgm, Me._ChkList)

                    'エラー帳票出力処理
                    Me.ShowStorePrintData(frm)
                    '処理終了アクション
                    Call Me._LMCconH.EndAction(frm)
                    Exit Sub
                End If

                ' イエローカード ログ出力
                Me.WritePrintLog(frm, strPass, LMC010C.PrintShubetsu.YELLOW_CARD)
            End If

            '処理終了アクション
            Call Me._LMCconH.EndAction(frm)
            Exit Sub
        End If

        'ADD 2016/07/04  Start
        'FFEM 納品書対応 1件目で判定
        Dim nrsBrCd As String = ds.Tables(LMC010C.TABLE_NM_IN_PRINT).Rows(0).Item("NRS_BR_CD").ToString
        Dim custCdL As String = ds.Tables(LMC010C.TABLE_NM_IN_PRINT).Rows(0).Item("CUST_CD_L").ToString

#If False Then  'UPD 2021/09/06 023522   【LMS】安田倉庫移転_PG改修点洗い出し_改修(営業荻山)
        If LMC010C.PrintShubetsu.NHS = Me._PrintSybetuEnum _
            AndAlso ((nrsBrCd.Equals("10") And custCdL.Equals("00135")) _
            OrElse (nrsBrCd.Equals("93") And custCdL.Equals("00135")) _
            OrElse (nrsBrCd.Equals("98") And custCdL.Equals("00135")) _
            OrElse (nrsBrCd.Equals("96") And custCdL.Equals("00135"))) Then    'MOD 2019/03/25 要望管理005124

#Else
        If LMC010C.PrintShubetsu.NHS = Me._PrintSybetuEnum _
            AndAlso ((nrsBrCd.Equals("10") And custCdL.Equals("00135")) _
            OrElse (nrsBrCd.Equals("40") And custCdL.Equals("00555")) _
            OrElse (nrsBrCd.Equals("60") And custCdL.Equals("00135")) _
            OrElse (nrsBrCd.Equals("93") And custCdL.Equals("00135")) _
            OrElse (nrsBrCd.Equals("98") And custCdL.Equals("00135")) _
            OrElse (nrsBrCd.Equals("96") And custCdL.Equals("00135")) _
            OrElse (nrsBrCd.Equals("F1") And custCdL.Equals("00135")) _
            OrElse (nrsBrCd.Equals("F2") And custCdL.Equals("00135")) _
            OrElse (nrsBrCd.Equals("F3") And custCdL.Equals("00135"))) Then 'ADD 2022/11/09 033380   【LMS】FFEM足柄工場LMS導入_PG改修点洗い出し_改修, 2023/11/28 039659 F3 追加

#End If

            Dim max As Integer = ds.Tables(LMC010C.TABLE_NM_IN_PRINT).Rows.Count - 1

            For i As Integer = 0 To max
                'スプレット行番号で処理
                Dim rowNo As Integer = CInt(ds.Tables(LMC010C.TABLE_NM_IN_PRINT).Rows(i).Item("ROW_NO").ToString)

                If IsPrintNHSPdfCheck(frm, rowNo) = True Then
                    '該当ありの場合は

                    '納品書PDF印刷対象
                    Dim strPass2 As String() = DirectCast(Me._NouhinsyoArr.ToArray(GetType(String)), String())

                    ' リモートの 納品書 PDF ファイルのローカルへのコピー 
                    strPass2 = CopyRemotePdf(frm, strPass2)
                    If MyBase.PDFDirectPrint(strPass2, 7000) Then
                        ' 納品書 ログ出力
                        Me.WritePrintLog(frm, strPass2, LMC010C.PrintShubetsu.NHS)
                    Else
                        ' PDF 直接印刷共通部品でのエラー発生時のメッセージ編集処理
                        Call ShowStorePrintErrorData(frm, lgm, New ArrayList(New String() {rowNo.ToString()}))

                        '高取倉庫以外
                        If nrsBrCd.Equals("93") = False AndAlso IsSameAsTakatori(nrsBrCd) = False Then    'MOD 2019/03/25 要望管理005124
                            Call Me.ShowStorePrintData(frm)
                        End If
                    End If
                End If

            Next

            'エラー帳票出力の判定
            If Me.ShowStorePrintData(frm) = True Then

                '処理終了メッセージの表示
                '2015.10.21 tusnehira add
                '英語化対応
                MyBase.ShowMessage(frm, "G062")

            End If

            '処理終了アクション
            Call Me._LMCconH.EndAction(frm)
            Exit Sub

        End If
        'ADD 2016/07/04  End

        'エラー帳票出力の判定
        If Me.ShowStorePrintData(frm) = True Then

            '処理終了メッセージの表示
            '2015.10.21 tusnehira add
            '英語化対応
            MyBase.ShowMessage(frm, "G062")

        End If

        'プレビュー判定 
        Dim prevDt As DataTable = rtnDs.Tables(LMConst.RD)
        If prevDt IsNot Nothing AndAlso 0 < prevDt.Rows.Count Then

            'プレビューの生成
            Dim prevFrm As RDViewer = New RDViewer()

            'データ設定
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始
            prevFrm.Run()

            'プレビューフォームの表示
            prevFrm.Show()

            'フォーカス設定
            prevFrm.Focus()

        End If

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 担当者名取得処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetTantoName(ByVal frm As LMC010F)

        '担当者テキストボックスに入力されたユーザーコードで名称を取得
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select("USER_CD = '" & frm.txtPicCD.TextValue().PadRight(5) & "' AND SYS_DEL_FLG = '0'")
        If getDr.Count() > 0 Then
            '担当者名ラベルに名称セット
            frm.lblPicNM.TextValue = getDr(0)("USER_NM").ToString()
        Else
            frm.lblPicNM.TextValue = String.Empty
        End If

    End Sub

#If True Then       'ADD 2018/11/02 依頼番号 : 002192   【LMS】荷主ごと_入庫日・出荷日の初期値設定

    ''' <summary>
    ''' 初期出荷予定日処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetShinkiOutkaDatee(ByVal frm As LMC010F)

        Dim custLCd As String = frm.txtShinkiCustCdL.TextValue
        Dim custMCd As String = frm.txtShinkiCustCdM.TextValue
        Dim nrsBrCd As String = frm.cmbEigyo.SelectedValue.ToString

        Dim sysDtTm As String() = MyBase.GetSystemDateTime()

        With frm

            Dim tmpdate As Date = Date.Parse(Format(Convert.ToDecimal(sysDtTm(0)), "0000/00/00"))

            .imdShinkiOutkaDate.TextValue = Format(tmpdate.Date, "yyyyMMdd")


            Dim custSetFLG As String = LMConst.FLG.OFF
            Dim custDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ",
                                                                                                      "CUST_CD_L = '", custLCd, "' AND ",
                                                                                                      "CUST_CD_M = '", custMCd, "'"))
            If 0 < custDr.Length Then
                custSetFLG = LMConst.FLG.ON

                Select Case custDr(0).Item("INIT_OUTKA_PLAN_DATE_KB").ToString
                    Case LMControlC.LMC020C_OUTKA_DATE_INIT_01
                        .imdShinkiOutkaDate.TextValue = DateAdd("d", -1, tmpdate).ToString("yyyyMMdd")
                    Case LMControlC.LMC020C_OUTKA_DATE_INIT_02

                    Case LMControlC.LMC020C_OUTKA_DATE_INIT_03
                        .imdShinkiOutkaDate.TextValue = DateAdd("d", 1, tmpdate).ToString("yyyyMMdd")

                    Case LMC020C.OUTKA_DATE_INIT_04
                        '前営業日
                        .imdShinkiOutkaDate.TextValue = Format(GetBussinessDay(.imdShinkiOutkaDate.TextValue, -1), "yyyyMMdd")

                    Case LMC020C.OUTKA_DATE_INIT_05
                        '翌営業日
                        .imdShinkiOutkaDate.TextValue = Format(GetBussinessDay(.imdShinkiOutkaDate.TextValue, +1), "yyyyMMdd")

                    Case Else

                        custSetFLG = LMConst.FLG.OFF
                End Select
            End If

            If (custSetFLG).Equals(LMConst.FLG.OFF) = True Then
                Dim mUser As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", LM.Base.LMUserInfoManager.GetUserID().ToString(), "'"))

                If 0 < mUser.Length Then
                    Select Case mUser(0).Item("OUTKA_DATE_INIT").ToString
                        Case LMControlC.LMC020C_OUTKA_DATE_INIT_01
                            .imdShinkiOutkaDate.TextValue = DateAdd("d", -1, tmpdate).ToString("yyyyMMdd")
                        Case LMControlC.LMC020C_OUTKA_DATE_INIT_02

                        Case LMControlC.LMC020C_OUTKA_DATE_INIT_03
                            .imdShinkiOutkaDate.TextValue = DateAdd("d", 1, tmpdate).ToString("yyyyMMdd")

                        Case LMC020C.OUTKA_DATE_INIT_04
                            '前営業日
                            .imdShinkiOutkaDate.TextValue = Format(GetBussinessDay(.imdShinkiOutkaDate.TextValue, -1), "yyyyMMdd")

                        Case LMC020C.OUTKA_DATE_INIT_05
                            '翌営業日
                            .imdShinkiOutkaDate.TextValue = Format(GetBussinessDay(.imdShinkiOutkaDate.TextValue, +1), "yyyyMMdd")

                    End Select

                End If
            End If
        End With

    End Sub

#End If

    ''' <summary>
    ''' 完了画面のパラメータ設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMR010InData(ByVal frm As LMC010F, ByVal arr As ArrayList) As DataSet

        Dim ds As DataSet = New LMR010DS()
        Dim dt As DataTable = ds.Tables(LMControlC.LMR010C_TABLE_NM_IN)
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        For i As Integer = 0 To max

            dr = dt.NewRow()
            With dr

                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                .Item("NRS_BR_CD") = Me._LMCconV.GetCellValue(spr.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
                .Item("INOUTKA_NO_L") = Me._LMCconV.GetCellValue(spr.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
                .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

            End With
            dt.Rows.Add(dr)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' コード値をキャッシュより再取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetInCd(ByVal frm As LMC010F) As Boolean

        '運送会社コード再設定
        Dim rtnResult As Boolean = True
        rtnResult = rtnResult AndAlso Me.SetUnsolUnsocoCd(frm)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 運送会社コード再設定
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetUnsolUnsocoCd(ByVal frm As LMC010F) As Boolean

        With frm
            Dim whereStr As String = "SYS_DEL_FLG = '0'"
            whereStr = whereStr & " AND NRS_BR_CD = '"
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'whereStr = whereStr & LMUserInfoManager.GetNrsBrCd() & "'"
            whereStr = whereStr & .cmbEigyo.SelectedValue.ToString() & "'"
            whereStr = whereStr & " AND UNSOCO_CD = '" & .txtTrnCD.TextValue & "'"
            whereStr = whereStr & " AND UNSOCO_BR_CD = '" & .txtTrnBrCD.TextValue & "'"

            Dim unsocoDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(whereStr)
            If 0 < unsocoDr.Length Then
                .txtTrnCD.TextValue = unsocoDr(0).Item("UNSOCO_CD").ToString
                .txtTrnBrCD.TextValue = unsocoDr(0).Item("UNSOCO_BR_CD").ToString
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' エラー帳票出力処理
    ''' </summary>
    ''' <returns>出力する場合:False　出力しない場合:True</returns>
    ''' <remarks></remarks>
    Private Function ShowStorePrintData(ByVal frm As LMC010F) As Boolean

        If MyBase.IsMessageStoreExist() = True Then

            ''高取倉庫の場合はこの時点で出力しない
            'If frm.cmbEigyo.SelectedValue.ToString() <> "93" Then
            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            MyBase.ShowMessage(frm, "E235")
            'End If
            Me._errFlg = True
            Return False

        End If

        Return True

    End Function

    '2014.10.02 高取対応START
    ''' <summary>
    ''' エラー帳票出力処理
    ''' </summary>
    ''' <returns>出力する場合:False　出力しない場合:True</returns>
    ''' <remarks></remarks>
    Private Function ShowStorePrintDataTaka(ByVal frm As LMC010F, ByVal arr As ArrayList) As Boolean

        If MyBase.IsMessageStoreExist(Convert.ToInt32(arr.Item(0))) = True Then

            ''高取倉庫の場合はこの時点で出力しない
            'If frm.cmbEigyo.SelectedValue.ToString() <> "93" Then
            'EXCEL起動 
            'MyBase.MessageStoreDownload(True)
            'MyBase.ShowMessage(frm, "E235")
            'End If
            Me._errFlg = True
            Return False

        End If

        Return True

    End Function
    '2014.10.02 高取対応END

    '2017.09.19 届先追加対応 Annen add start 

    ''' <summary>
    ''' 出荷データ（大）届出更新処理
    ''' </summary>
    ''' <param name="frm">自画面</param>
    ''' <param name="arr">スプレッド更新対象行群</param>
    ''' <returns>True：エラーなし False：エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChangeDestination(ByVal frm As LMC010F, ByVal arr As ArrayList) As Boolean

        'メッセージ情報を初期化する
        MyBase.ClearMessageData()
        MyBase.ClearMessageStoreData()

        '更新条件のデータテーブルを設定する
        Call SetDestinationInTable(frm, arr)

        If MyBase.IsMessageStoreExist() = True Then
            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            'エラーメッセージの表示
            MyBase.ShowMessage(frm, "E235")
            Return False
        End If
        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})
        Return True

    End Function

    ''' <summary>
    ''' 選択行のデータ存在チェック（届先実行用）
    ''' </summary>
    ''' <param name="frm">自画面</param>
    ''' <param name="rowNo">対象行</param>
    ''' <returns>True：正常 False：エラー</returns>
    ''' <remarks></remarks>
    Private Function SelectDestinationOutkaData(ByVal frm As LMC010F, ByVal rowNo As Integer) As Boolean

        With frm.sprDetail.ActiveSheet

            'データセット、データテーブル、データレコードの設定を行う
            Dim ds As DataSet = New LMC010DS()
            Dim dt As DataTable = ds.Tables(LMC010C.TABLE_NM_IN)
            Dim dr As DataRow = dt.NewRow()
            Dim outRow() As DataRow = Nothing
            dr.Item("NRS_BR_CD") = Me._LMCconV.GetCellValue(.Cells(rowNo, LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
            Dim outkaNoL As String = Me._LMCconV.GetCellValue(.Cells(rowNo, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr.Item("OUTKA_NO_L") = outkaNoL
            dt.Rows.Add(dr)

            '強制実行フラグの設定
            MyBase.SetForceOparation(True)

            'DBより対象データ存在確認を行う
            Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "SelectListData", ds)

            '取得できない場合、エラーとする
            If rtnDs.Tables(LMC010C.TABLE_NM_OUT).Rows.Count < 1 Then
                MyBase.SetMessageStore("00", "E659", New String() {outkaNoL}, rowNo.ToString())
                Return False
            End If

            '取得した情報に届先区分="00"(届先)、"02"(EDI)で絞込を掛け、0件であればエラーとする
            outRow = rtnDs.Tables(LMC010C.TABLE_NM_OUT).Select("DEST_KB = '02'")
            If outRow.Length = 0 Then
                MyBase.SetMessageStore("00", "E237", New String() {"届先区分が02(EDI)以外"}, rowNo.ToString())
                Return False
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 届出区分を変更する出荷データ（大）のInTableを用意する
    ''' </summary>
    ''' <param name="frm">自画面</param>
    ''' <param name="arr">更新対象行群</param>
    ''' <remarks></remarks>
    Private Sub SetDestinationInTable(ByVal frm As LMC010F, ByVal arr As ArrayList)

        Dim ds As DataSet = New LMC010DS()
        Dim dt As DataTable = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_IN)
        Dim dr As DataRow = dt.NewRow
        Dim nrsBrCd As String = String.Empty
        Dim max As Integer = arr.Count - 1
        Dim recNo As Integer = 0
        For i As Integer = 0 To max

            'メッセージデータの初期化を行う
            '(Storeの方は後でメッセージをEXCELに出力するため、初期化しない)
            MyBase.ClearMessageData()

            recNo = Convert.ToInt32(arr(i))

            'データ存在チェックを行う
            If SelectDestinationOutkaData(frm, recNo) = False Then
                Continue For
            End If

            'データテーブルをクリアする
            dt.Clear()

            'データ抽出に必要な条件をデータ行に設定する
            dr.Item("NRS_BR_CD") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
            dr.Item("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr.Item("ROW_NO") = recNo
            dr.Item("SYS_UPD_DATE") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.SYS_UPD_DATE.ColNo))
            dr.Item("SYS_UPD_TIME") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.SYS_UPD_TIME.ColNo))
            dt.Rows.Add(dr)

            'DB更新処理を行う
            Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "UpdateOutkaDestKbn", ds)

        Next

    End Sub

    '2017.09.19 届先追加対応 Annen add end 

    '顧客WEB出荷登録バッチ対応 t.kido Start
    ''' <summary>
    ''' 顧客WEB出荷登録処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub WebOutkaTorokuShori(ByVal frm As LMC010F)

        Dim process As System.Diagnostics.Process = Nothing
        Dim exePath As String = String.Empty
        Dim arguments As String = String.Empty
        Dim kbnDr() As DataRow = Nothing

        Try
            '区分マスタから出荷バッチ実行EXEのパスを取得
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'S038' AND ",
                                                                                           "KBN_CD = '22'"))
            If 0 < kbnDr.Length Then
                exePath = kbnDr(0).Item("KBN_NM2").ToString
            End If

            '引数の設定　営業所コード、倉庫コード、荷主コード
            'arguments = String.Format("{0} {1} {2} {3}", "LBC100", frm.cmbEigyo.SelectedValue.ToString(), frm.cmbSoko.SelectedValue.ToString(), frm.txtCustCD.TextValue())
            arguments = String.Format("{0} {1} {2} {3} {4}", "LBC100", frm.cmbEigyo.SelectedValue.ToString(), frm.cmbSoko.SelectedValue.ToString(), frm.txtCustCD.TextValue(), LMUserInfoManager.GetUserID())

            '出荷バッチアプリケーションの起動
            process = System.Diagnostics.Process.Start(exePath, arguments)

            '終了するまで待機する
            process.WaitForExit()

            '終了コードを取得する
            Dim ExitCode As Integer = process.ExitCode

            'プロセスの破棄
            process.Close()
            process.Dispose()

            '処理終了メッセージの表示
            Select Case ExitCode
                Case 0
                    '正常終了
                    MyBase.ShowMessage(frm, "G094")
                Case 1
                    '正常終了(該当データなし)
                    MyBase.ShowMessage(frm, "G001")
                Case 2
                    '引当可能データなし
                    MyBase.ShowMessage(frm, "E192")
                Case 3
                    '出荷登録に成功したが、引当に失敗
                    MyBase.ShowMessage(frm, "E979")
                Case 4
                    'エラーデータあり
                    MyBase.ShowMessage(frm, "E235")
                Case 9
                    '予期せぬエラー
                    MyBase.ShowMessage(frm, "S002")
            End Select
        Catch ex As Exception

            MyBase.Logger.WriteErrorLog(MyBase.GetType.Name _
                          , Reflection.MethodBase.GetCurrentMethod().Name _
                          , ex.Message _
                          , ex)

            '予期せぬエラー
            MyBase.ShowMessage(frm, "S002")
        End Try
    End Sub
    '顧客WEB出荷登録バッチ対応 t.kido End

    ''' <summary>
    ''' 名変入荷作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MeihenInkaMake(ByVal frm As LMC010F, ByVal arr As ArrayList)

        _JikkouDs = New LMC010DS()
        Dim setDs As DataSet = _JikkouDs.Copy()
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_MEIHEN_INKA_IN).NewRow()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_MEIHEN_INKA_IN)
        Dim nrsBrCd As String = String.Empty
        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty
        Dim taxKb As String = String.Empty
        Dim custDr As DataRow() = Nothing
        Dim max As Integer = arr.Count - 1
        Dim setDt As DataTable = _JikkouDs.Tables(LMC010C.TABLE_NM_MEIHEN_INKA_IN)
        Dim recNo As Integer = 0

        '振替先の荷主コードを設定
        custCdL = frm.txtShinkiCustCdL.TextValue()
        custCdM = frm.txtShinkiCustCdM.TextValue()

        '振替先荷主の課税区分を取得
        custDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat(
                                                                          "NRS_BR_CD = '", frm.cmbEigyo.SelectedValue.ToString(), "' AND " _
                                                                         , "CUST_CD_L = '", custCdL, "' AND " _
                                                                         , "CUST_CD_M = '", custCdM, "' AND " _
                                                                         , "CUST_CD_S = '00' AND " _
                                                                         , "CUST_CD_SS = '00' AND " _
                                                                         , "SYS_DEL_FLG = '0'"))
        If 0 < custDr.Length Then
            taxKb = custDr(0).Item("TAX_KB").ToString
        End If

        For i As Integer = 0 To max

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))

            recNo = Convert.ToInt32(arr(i))
            dr("NRS_BR_CD") = nrsBrCd
            dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr("ROW_NO") = recNo
            dr("CUST_CD_L") = custCdL
            dr("CUST_CD_M") = custCdM
            dr("TAX_KB") = taxKb
            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '名変入荷作成処理
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "InsertMeihenInkaData", _JikkouDs)

        If MyBase.IsMessageStoreExist() = True Then
            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            MyBase.ShowMessage(frm, "E235")
            Exit Sub
        End If

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

    End Sub

    ''' <summary>
    ''' 選択行のデータ存在チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SelectOutkaData(ByVal frm As LMC010F) As Boolean

        With frm.sprDetail.ActiveSheet

            Dim ds As DataSet = New LMC010DS()
            Dim dt As DataTable = ds.Tables(LMC010C.TABLE_NM_IN)
            Dim dr As DataRow = dt.NewRow()
            Dim rowNo As Integer = .ActiveRowIndex
            dr.Item("NRS_BR_CD") = Me._LMCconV.GetCellValue(.Cells(rowNo, LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
            Dim outkaNoL As String = Me._LMCconV.GetCellValue(.Cells(rowNo, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr.Item("OUTKA_NO_L") = outkaNoL
            dt.Rows.Add(dr)

            '強制実行フラグの設定
            MyBase.SetForceOparation(True)

            Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "SelectListData", ds)

            '取得できない場合、エラー
            If rtnDs.Tables(LMC010C.TABLE_NM_OUT).Rows.Count < 1 Then
                '20151020 tsunehira add
                MyBase.ShowMessage(frm, "E659", New String() {outkaNoL})
                'MyBase.ShowMessage(frm, "E079", New String() {"出荷(大)テーブル", outkaNoL})
                Return False
            End If

            Return True

        End With

    End Function

    '社内入荷データ作成 terakawa Start
    ''' <summary>
    ''' 出力(実行)コンボ選択時処理呼び出し
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub cmbJikkou_SelectedValueChanged(ByVal frm As LMC010F)

        Call Me._G.SetIntEdiControl(frm)

    End Sub

    ''' <summary>
    ''' 社内入荷データ作成先を取得
    ''' </summary>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function GetIntEdiNmData(ByVal frm As LMC010F) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMC010DS()
        Dim dt As DataTable = ds.Tables(LMC010C.TABLE_NM_INT_EDI_IN)
        Dim dr As DataRow = dt.NewRow()
        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd()
        dr.Item("USER_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
        dt.Rows.Add(dr)
        Return MyBase.CallWSA("LMC010BLF", "ComboData", ds)
    End Function
    '社内入荷データ作成 terakawa End

    '2014/04/21 CALT対応 黎 --ST--
    ''' <summary>
    ''' 出荷指示データ作成
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub OutkaShiji(ByVal frm As LMC010F, ByVal arr As ArrayList)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        'パラメータのインスタンス生成
        Dim ds As DataSet = New LMC010DS()
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1

        Dim sOutkaSts As String = String.Empty
        Dim sNrsBrCd As String = String.Empty
        Dim sOutkaNoL As String = String.Empty
        Dim sOutPlanDate As String = String.Empty
        Dim sRowNo As String = String.Empty
        Dim sSysUpdDate As String = String.Empty
        Dim sSysUpdTime As String = String.Empty

        'INデータ設定
        For i As Integer = 0 To max

            sOutkaSts = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_STATE_KB.ColNo))

            If sOutkaSts.Equals(LMC010C.SINTYOKU10) = False Then

                sNrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
                sOutkaNoL = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
                sOutPlanDate = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_PLAN_DATE.ColNo))
                sRowNo = arr(i).ToString()
                sSysUpdDate = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.SYS_UPD_DATE.ColNo))
                sSysUpdTime = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.SYS_UPD_TIME.ColNo))

                dr = ds.Tables(LMC010C.TABLE_NM_LMC100IN_OUTKA_DIRECT_SEND).NewRow()
                With dr
                    dr.Item("NRS_BR_CD") = sNrsBrCd
                    dr.Item("OUTKA_NO_L") = sOutkaNoL
                    dr.Item("OUTKA_STATE_KB") = sOutkaSts
                    dr.Item("OUTKA_PLAN_DATE") = sOutPlanDate.Replace("/", "")
                    dr.Item("ROW_NO") = sRowNo
                    dr.Item("SYS_UPD_DATE") = sSysUpdDate
                    dr.Item("SYS_UPD_TIME") = sSysUpdTime

                End With

                ds.Tables(LMC010C.TABLE_NM_LMC100IN_OUTKA_DIRECT_SEND).Rows.Add(dr)

            Else

                '2017/09/25 修正 李↓
                MyBase.SetMessageStore("00", "E434", New String() {lgm.Selector({"出荷指示", "Shipping instructions", "출하지시", "中国語"})}, arr(i).ToString())
                '2017/09/25 修正 李↑

            End If
        Next

        '実行時WSAクラス呼び出し
        ds = MyBase.CallWSA("LMC010BLF", "OutkaShiji", ds)

        'エラーストレージ形式(エド・ゲイン)
        If MyBase.IsMessageStoreExist() = True Then
            '処理終了メッセージの表示
            Me.ShowStorePrintData(frm)
            Exit Sub
        End If

        '処理終了メッセージの表示
        '英語化対応
        '20151021 tsunehira add
        MyBase.ShowMessage(frm, "G072")
        'MyBase.ShowMessage(frm, "G002", New String() {"出荷指示データ作成", String.Empty})

    End Sub
    '2014.04.21 CALT対応 黎 --ED--

    '2015.06.22 協立化学　作業料対応 START
    ''' <summary>
    ''' 作業料明細特殊作成
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SagyoMeisai(ByVal frm As LMC010F, ByVal arr As ArrayList)

        'パラメータのインスタンス生成
        Dim ds As DataSet = New LMC010DS()
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1

        Dim sOutkaSts As Integer = 0
        Dim Outkakosu As Integer = 0
        Dim OutkaSuryo As Decimal = 0
        Dim sNrsBrCd As String = String.Empty
        Dim sOutkaNoL As String = String.Empty
        Dim sRowNo As String = String.Empty
        Dim sSysUpdDate As String = String.Empty
        Dim sSysUpdTime As String = String.Empty

        'INデータ設定
        For i As Integer = 0 To max

            sOutkaSts = Convert.ToInt32(Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_STATE_KB.ColNo)))
            Outkakosu = Convert.ToInt32(Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.MIN_ALCTD_NB.ColNo)))
            OutkaSuryo = Convert.ToDecimal(Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.MIN_ALCTD_QT.ColNo)))

            If sOutkaSts >= Convert.ToInt32(LMC010C.SINTYOKU40) AndAlso (Outkakosu > 0 AndAlso OutkaSuryo > 0) Then

                sNrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
                sOutkaNoL = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
                sRowNo = arr(i).ToString()
                sSysUpdDate = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.SYS_UPD_DATE.ColNo))
                sSysUpdTime = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.SYS_UPD_TIME.ColNo))

                dr = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).NewRow()
                With dr
                    dr.Item("NRS_BR_CD") = sNrsBrCd
                    dr.Item("OUTKA_NO_L") = sOutkaNoL
                    dr.Item("ROW_NO") = sRowNo
                    dr.Item("SYS_UPD_DATE") = sSysUpdDate
                    dr.Item("SYS_UPD_TIME") = sSysUpdTime

                End With

                ds.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).Rows.Add(dr)

            Else

                MyBase.SetMessageStore("00", "E114", New String() {sOutkaNoL}, arr(i).ToString())

            End If
        Next

        If ds.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).Rows.Count = 0 Then

            If MyBase.IsMessageStoreExist() = True Then
                '処理終了メッセージの表示
                Me.ShowStorePrintData(frm)
                Exit Sub
            End If

        End If

        '実行時WSAクラス呼び出し
        ds = MyBase.CallWSA("LMC010BLF", "SagyoMeisai", ds)

        'エラーEXCEL
        If MyBase.IsMessageStoreExist() = True Then
            '処理終了メッセージの表示
            Me.ShowStorePrintData(frm)
            Exit Sub
        End If

        '処理終了メッセージの表示
        '2015.10.21 tusnehira add
        '英語化対応
        MyBase.ShowMessage(frm, "G070")
        'MyBase.ShowMessage(frm, "G002", New String() {"作業料明細特殊作成", String.Empty})

    End Sub
    '2015.06.22 協立化学　作業料対応 END

    '20150106 名鉄対応 tsunehira add start
#If False Then
#Else

    ''' <summary>
    ''' 名鉄帳票印刷(荷札,送状同時印刷)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="arr"></param>
    ''' <param name="prm"></param>
    ''' <param name="isGrouping"></param>
    ''' <remarks></remarks>
    Private Sub MeitetuPrintData(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData, ByVal isGrouping As Boolean)
#End If
        Dim prmDs As DataSet = New LMC800DS
        Me._JikkouDs = New LMC010DS
        Dim setDs As DataSet = Me._JikkouDs.Copy()
#If False Then
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_IN_CSV)
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_IN_CSV).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_IN_CSV)
#Else
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_IN_UPDATE)
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_IN_UPDATE).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_IN_UPDATE)
#End If
        Dim max As Integer = arr.Count - 1
        Dim kbnDr() As DataRow = Nothing
        Dim nrsBrCd As String = String.Empty
        Dim unsoCd As String = String.Empty
        Dim unsoShitenCd As String = String.Empty

        'システム日付の取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "SetSysDateTime", setDs)
        Dim sysDt As DataTable = rtnDs.Tables(LMC010C.TABLE_NM_SYS_DATETIME)
        If 0 < sysDt.Rows.Count Then

            Me.SysData(LMC010C.SysData.YYYYMMDD) = sysDt.Rows(0).Item("SYS_DATE").ToString()
            Me.SysData(LMC010C.SysData.HHMMSSsss) = sysDt.Rows(0).Item("SYS_TIME").ToString()

        End If
#If False Then
        '保存先のCSVファイルのパス・ファイル名
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C003' AND ", _
                                                                                       "KBN_CD = '00'"))
        Dim filePath As String = kbnDr(0).Item("KBN_NM1").ToString
        Dim fileName As String = kbnDr(0).Item("KBN_NM2").ToString

        For i As Integer = 0 To max

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
            unsoCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_CD.ColNo))

            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C004' AND ", _
                                 btnPrintUnsoco_Click                                                          "KBN_NM1 = '", nrsBrCd, "' AND ", _
                                                                                           "KBN_NM2 = '", unsoCd, "'"))
            If kbnDr.Length = 0 Then
                '区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If



            dr("NRS_BR_CD") = nrsBrCd
            dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr("ROW_NO") = Convert.ToInt32(arr(i))
            dr("FILEPATH") = filePath
            dr("FILENAME") = fileName
            dr("SYS_DATE") = Me.SysData(LMC010C.SysData.YYYYMMDD)
            dr("SYS_TIME") = Me.SysData(LMC010C.SysData.HHMMSSsss)

            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        If setDt.Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If


        '2015.12.28 名鉄対応 tsunehira adds start
        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()
#Else
        Dim targetRows As New ArrayList()

        For i As Integer = 0 To max
            nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
            unsoCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_CD.ColNo))
            unsoShitenCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.UNSO_BR_CD.ColNo))

            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C004' AND ",
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ",
                                                                                           "KBN_NM2 = '", unsoCd, "' AND ",
                                                                                           "KBN_NM3 = '", unsoShitenCd, "'"))
            If kbnDr.Length = 0 Then
                '区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If

            ' 対象の運送会社のみ設定
            targetRows.Add(arr(i))

        Next

        If targetRows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E070")
            Exit Sub
        End If

        Me.SetDataSetInMeitetuPrintData(frm, Me._JikkouDs, targetRows)
        Me.SetDataSetInData_UPDATE(frm, Me._JikkouDs, targetRows)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '検索時WSAクラス呼び出し
        If (isGrouping = True) Then
            ' まとめあり
            prmDs = MyBase.CallWSA("LMC010BLF", "PrintMeitetuReportWithGrouping", Me._JikkouDs)
        Else
            ' まとめなし
            prmDs = MyBase.CallWSA("LMC010BLF", "PrintMeitetuReport", Me._JikkouDs)
        End If
#End If

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E070")
            Exit Sub
        End If

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            MyBase.ShowMessage(frm, "E235")

            'EXCEL起動()
            MyBase.MessageStoreDownload()

        Else


            'エラー帳票出力の判定
            Call Me.ShowStorePrintData(frm)

            'プレビュー判定 
            Dim prevDt As DataTable = prmDs.Tables(LMConst.RD)
            If prevDt IsNot Nothing AndAlso prevDt.Rows.Count > 0 Then

                'プレビューの生成 
                Dim prevFrm As New RDViewer()

                'データ設定 
                prevFrm.DataSource = prevDt

                'プレビュー処理の開始 
                prevFrm.Run()

                'プレビューフォームの表示 
                prevFrm.Show()

            End If

            '2015.12.28 名鉄対応 tsunehira adds end

            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "G002", New String() {frm.cmbTrapoPrint.SelectedText, String.Empty})

        End If

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub
    '20150106 名鉄対応 tsunehira add end

    ''' <summary>
    ''' ログ出力
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub WritePrintLog(ByVal frm As LMC010F, ByVal fileName() As String, printKbn As Integer)

        Dim setDs As DataSet = New LMC010DS
        Dim setDt As DataTable = setDs.Tables("LMC010IN_WRITE_LOG")
        Dim setDr As DataRow = setDt.NewRow()
        For i As Integer = 0 To fileName.Length - 1
            setDr = setDt.NewRow()
            setDr.Item("PATH") = fileName(i).ToString
            setDr.Item("PRINT_KBN") = printKbn.ToString
            setDt.Rows.Add(setDr)
        Next
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "WritePrintLog", setDs)

    End Sub

#Region "タブレット対応"
    ''' <summary>
    ''' 庫内作業指示
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub WHSagyoShiji(ByVal frm As LMC010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        If arr.Count = 0 Then
            MyBase.ShowMessage(frm, "E009")
            Call Me._LMCconH.EndAction(frm)
            Exit Sub
        End If

        Me._JikkouDs = New LMC930DS
        For i As Integer = 0 To arr.Count - 1

            '検品済の場合データセットに登録
            Dim dRow As DataRow = Me._JikkouDs.Tables(LMC930C.TABLE_NM.LMC930IN).NewRow
            dRow.Item("NRS_BR_CD") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
            dRow.Item("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dRow.Item("OUTKA_STATE_KB") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_STATE_KB.ColNo))
            dRow.Item("ROW_NO") = Convert.ToInt32(arr(i))
            dRow.Item("PGID") = MyBase.GetPGID
            dRow.Item("SYS_UPD_DATE") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.SYS_UPD_DATE.ColNo))
            dRow.Item("SYS_UPD_TIME") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.SYS_UPD_TIME.ColNo))
            dRow.Item("WH_TAB_STATUS_KB") = LMC930C.WH_TAB_SIJI_STATUS.INSTRUCTED
            dRow.Item("PROC_TYPE") = LMC930C.PROC_TYPE.INSTRUCT
            dRow.Item("CUST_CD_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo))
            dRow.Item("CUST_CD_M") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_M.ColNo))
            Me._JikkouDs.Tables(LMC930C.TABLE_NM.LMC930IN).Rows.Add(dRow)

        Next

        '処理呼出
        prm.ParamDataSet = Me._JikkouDs

        LMFormNavigate.NextFormNavigate(Me, "LMC930", prm)

        If MyBase.IsMessageExist Then

            MyBase.ShowMessage(frm)
        Else
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        End If

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub

#End Region

    'ADD START 2019/03/25 要望管理005124
#Region "93:高取倉庫と同一処理を行う対象判定"

    Private Function IsSameAsTakatori(ByVal nrsBrCd As String) As Boolean

#If False Then  'UPD 2021/09/06 023522   【LMS】安田倉庫移転_PG改修点洗い出し_改修(営業荻山)
        If nrsBrCd.Equals("96") OrElse nrsBrCd.Equals("98") Then
#Else
        If nrsBrCd.Equals("96") OrElse nrsBrCd.Equals("98") OrElse nrsBrCd.Equals("F1") OrElse nrsBrCd.Equals("F2") OrElse nrsBrCd.Equals("F3") Then    'UPD 2022/11/09 033380   【LMS】FFEM足柄工場LMS導入 F2追加, 2023/11/28 039659 F3 追加
#End If

            '96:大分工場
            Return True
        End If

        Return False

    End Function

#End Region
    'ADD END   2019/03/25 要望管理005124

#End Region


#Region "PopUp"

    ''' <summary>
    ''' マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks>参照Popupが開く場合のコントロールです。</remarks>
    Private Sub ShowPopup(ByVal frm As LMC010F, ByVal objNM As String, ByRef prm As LMFormData)

        ''開始処理
        'Me._LMCconH.StartAction(frm)

        'オブジェクト名による分岐
        Select Case objNM

            Case LMC010C.EventShubetsu.SINKI.ToString()         '新規

                'inputDataSet作成
                Dim prmDs As DataSet = New LMZ260DS
                Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'row("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
                '要望番号:1568 terakawa 2013.01.17 Start
                row("CUST_CD_L") = frm.txtShinkiCustCdL.TextValue
                row("CUST_CD_M") = frm.txtShinkiCustCdM.TextValue
                'row("CUST_CD_L") = frm.txtCustCD.TextValue
                '要望番号:1568 terakawa 2013.01.17 End
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                row.Item("HYOJI_KBN") = LMZControlC.HYOJI_S
                prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = False

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

            Case "txtCustCD"                                    '荷主マスタ参照

                Dim prmDs As DataSet = New LMZ260DS
                Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
                'START YANAI 要望番号481
                'row("CUST_CD_L") = frm.txtCustCD.TextValue
                If Me._PopupSkipFlg = False Then
                    row("CUST_CD_L") = frm.txtCustCD.TextValue
                End If
                'END YANAI 要望番号481
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                row.Item("HYOJI_KBN") = LMZControlC.HYOJI_S
                prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                If String.IsNullOrEmpty(frm.txtCustCD.TextValue) = True Then
                    frm.lblCustNM.TextValue = String.Empty
                End If

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)
                '要望番号:1568 terakawa 2013.01.17 Start
            Case "txtShinkiCustCdL", "txtShinkiCustCdM"               '荷主マスタ参照

                Dim prmDs As DataSet = New LMZ260DS
                Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
                If Me._PopupSkipFlg = False Then
                    row("CUST_CD_L") = frm.txtShinkiCustCdL.TextValue
                    row("CUST_CD_M") = frm.txtShinkiCustCdM.TextValue
                End If
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                row.Item("HYOJI_KBN") = LMZControlC.HYOJI_S
                prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)
                '要望番号:1568 terakawa 2013.01.17 End
            Case "txtTrnCD", "txtTrnBrCD"                      '運送会社マスタ参照

                Dim prmDs As DataSet = New LMZ250DS()
                Dim row As DataRow = prmDs.Tables(LMZ250C.TABLE_NM_IN).NewRow
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
                'START YANAI 要望番号481
                'row("UNSOCO_CD") = frm.txtTrnCD.TextValue
                'row("UNSOCO_BR_CD") = frm.txtTrnBrCD.TextValue
                If Me._PopupSkipFlg = False Then
                    ' Me._PopupSkipFlg = False はEnter押下時の場合
                    row("UNSOCO_CD") = frm.txtTrnCD.TextValue
                    row("UNSOCO_BR_CD") = frm.txtTrnBrCD.TextValue
                End If
                'END YANAI 要望番号481
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                prmDs.Tables(LMZ250C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                If String.IsNullOrEmpty(frm.txtTrnCD.TextValue) = True AndAlso
                    String.IsNullOrEmpty(frm.txtTrnBrCD.TextValue) = True Then
                    frm.lblTrnNM.TextValue = String.Empty
                End If

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ250", prm)

                '要望番号:1568 terakawa 2013.01.17 Start
            Case "txtShinkiTrnCd", "txtShinkiTrnBrCd"                      '運送会社マスタ参照

                Dim prmDs As DataSet = New LMZ250DS()
                Dim row As DataRow = prmDs.Tables(LMZ250C.TABLE_NM_IN).NewRow
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
                If Me._PopupSkipFlg = False Then
                    row("UNSOCO_CD") = frm.txtShinkiTrnCd.TextValue
                    row("UNSOCO_BR_CD") = frm.txtShinkiTrnBrCd.TextValue
                End If
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                prmDs.Tables(LMZ250C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ250", prm)
                '要望番号:1568 terakawa 2013.01.17 End
            Case LMC010C.EventShubetsu.DEF_CUST.ToString()         '初期荷主変更
                Dim prmDs As DataSet = New LMZ010DS()
                Dim row As DataRow = prmDs.Tables(LMZ010C.TABLE_NM_IN).NewRow()
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                prmDs.Tables(LMZ010C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ010", prm)

        End Select

        '戻り処理
        If prm.ReturnFlg = True Then
            Select Case objNM

                Case LMC010C.EventShubetsu.SINKI.ToString()   '新規

                    'inputDataSet作成
                    Dim prmDs As DataSet = Me.SetDataSetLMC020InData(frm, prm.ParamDataSet, LMC010C.LMC020_STA_SINKI)
                    prm.ParamDataSet = prmDs
                    '要望番号:1568 terakawa 2013.01.17 Start
                    '新規設定用運送会社コードが入力されていた場合、POPUPを表示し、戻り値をIn情報にセット
                    If String.IsNullOrEmpty(frm.txtShinkiTrnCd.TextValue) = False OrElse
                       String.IsNullOrEmpty(frm.txtShinkiTrnBrCd.TextValue) = False Then
                        prm = ShowPopupShinkiUnsoco(frm, prm)
                    End If

                    prm.RecStatus = RecordStatus.NEW_REC

                    If prm.ReturnFlg = True Then
                        '画面遷移
                        LMFormNavigate.NextFormNavigate(Me, "LMC020", prm)
                    Else
                        MyBase.ShowMessage(frm, "E193")
                    End If
                    '要望番号:1568 terakawa 2013.01.17 End
                Case "txtCustCD"
                    '荷主マスタ参照

                    'PopUpから取得したデータをコントロールにセット
                    With prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
                        frm.txtCustCD.TextValue = .Item("CUST_CD_L").ToString()       '荷主コード（大）
                        frm.lblCustNM.TextValue = .Item("CUST_NM_L").ToString()       '荷主名
                        'デフォルト倉庫コード設定 yamanaka 2013.02.26 Start
                        frm.cmbSoko.SelectedValue = .Item("DEFAULT_SOKO_CD").ToString '（初期）デフォルト倉庫
                        'デフォルト倉庫コード設定 yamanaka 2013.02.26 End
                    End With

                    '要望番号:1568 terakawa 2013.01.17 Start
                Case "txtShinkiCustCdL", "txtShinkiCustCdM"
                    '荷主マスタ参照

                    'PopUpから取得したデータをコントロールにセット
                    With prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
                        frm.txtShinkiCustCdL.TextValue = .Item("CUST_CD_L").ToString()      '荷主コード（大）
                        frm.txtShinkiCustCdM.TextValue = .Item("CUST_CD_M").ToString()      '荷主コード（中）
                        '要望番号:1839 terakawa 2013.02.08 Start
                        frm.cmbPick.SelectedValue() = .Item("PICK_LIST_KB").ToString()      'ピッキングリスト区分
                        '要望番号:1839 terakawa 2013.02.08 End
                    End With
                    '要望番号:1568 terakawa 2013.01.17 End

                Case "txtTrnCD", "txtTrnBrCD"
                    '運送会社マスタ参照

                    'PopUpから取得したデータをコントロールにセット
                    With prm.ParamDataSet.Tables(LMZ250C.TABLE_NM_OUT).Rows(0)
                        frm.txtTrnCD.TextValue = .Item("UNSOCO_CD").ToString()      '運送会社コード
                        frm.txtTrnBrCD.TextValue = .Item("UNSOCO_BR_CD").ToString() '運送会社支店コード
                        frm.lblTrnNM.TextValue = .Item("UNSOCO_NM").ToString() + "　" + .Item("UNSOCO_BR_NM").ToString()      '運送会社名
                    End With

                    '要望番号:1568 terakawa 2013.01.17 Start
                Case "txtShinkiTrnCd", "txtShinkiTrnBrCd"
                    '運送会社マスタ参照

                    'PopUpから取得したデータをコントロールにセット
                    With prm.ParamDataSet.Tables(LMZ250C.TABLE_NM_OUT).Rows(0)
                        frm.txtShinkiTrnCd.TextValue = .Item("UNSOCO_CD").ToString()      '運送会社コード
                        frm.txtShinkiTrnBrCd.TextValue = .Item("UNSOCO_BR_CD").ToString() '運送会社支店コード
                    End With
                    '要望番号:1568 terakawa 2013.01.17 End

                Case LMC010C.EventShubetsu.DEF_CUST.ToString()         '初期荷主変更
                    With prm.ParamDataSet.Tables(LMZ010C.TABLE_NM_OUT).Rows(0)
                        frm.txtCustCD.TextValue = .Item("CUST_CD_L").ToString    '荷主コード（大）
                        frm.lblCustNM.TextValue = .Item("CUST_NM_L").ToString    '荷主名
                        '要望番号:1794 s.kobayashi 2013.1.22 
                        frm.txtShinkiCustCdL.TextValue = .Item("CUST_CD_L").ToString    '荷主コード（大）
                        frm.txtShinkiCustCdM.TextValue = .Item("CUST_CD_M").ToString    '荷主コード（中）
                        '要望番号:1839 terakawa 2013.02.08 Start
                        Dim getCustDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).
                            Select("SYS_DEL_FLG = '0' AND CUST_CD_L = '" & .Item("CUST_CD_L").ToString() &
                                    "' AND CUST_CD_M = '00' AND CUST_CD_S = '00' AND CUST_CD_SS = '00'")
                        If getCustDr.Length() > 0 Then
                            frm.cmbPick.SelectedValue() = getCustDr(0).Item("PICK_LIST_KB").ToString()   '（初期）ピッキングリスト区分
                        End If
                        '要望番号:1839 terakawa 2013.02.08 End
                    End With

            End Select

        Else
            Select Case objNM

                Case LMC010C.EventShubetsu.SINKI.ToString()   '新規
                    MyBase.ShowMessage(frm, "E193")
            End Select

        End If

        MyBase.ShowMessage(frm, "G007")

        ''終了処理
        'Me._LMCconH.EndAction(frm)

    End Sub

    '要望番号:1568 terakawa 2013.01.17 Start
    ''' <summary>
    ''' マスタ参照POP起動(新規ボタン押下時：運送会社マスタ)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks> 
    Private Function ShowPopupShinkiUnsoco(ByVal frm As LMC010F, ByVal prm As LMFormData) As LMFormData


        'パラメータクラス生成
        Dim prmUnsoco As LMFormData = New LMFormData()
        'パラメータ設定
        prmUnsoco.ReturnFlg = False
        Dim prmDs As DataSet = New LMZ250DS()
        Dim row As DataRow = prmDs.Tables(LMZ250C.TABLE_NM_IN).NewRow
        row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
        row("UNSOCO_CD") = frm.txtShinkiTrnCd.TextValue
        row("UNSOCO_BR_CD") = frm.txtShinkiTrnBrCd.TextValue
        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        prmDs.Tables(LMZ250C.TABLE_NM_IN).Rows.Add(row)
        prmUnsoco.ParamDataSet = prmDs
        prmUnsoco.SkipFlg = False

        'POP呼出
        LMFormNavigate.NextFormNavigate(Me, "LMZ250", prmUnsoco)

        If prmUnsoco.ReturnFlg = True Then
            With prmUnsoco.ParamDataSet.Tables(LMZ250C.TABLE_NM_OUT).Rows(0)
                prm.ParamDataSet.Tables(LMControlC.LMC020C_TABLE_NM_IN).Rows(0).Item("UNSOCO_CD") = .Item("UNSOCO_CD")                                '運送会社コード
                prm.ParamDataSet.Tables(LMControlC.LMC020C_TABLE_NM_IN).Rows(0).Item("UNSOCO_BR_CD") = .Item("UNSOCO_BR_CD")                          '運送会社支店コード
            End With
        Else
            prm.ReturnFlg = False
        End If

        Return prm
    End Function
    '要望番号:1568 terakawa 2013.01.17 End

    ''' <summary>
    ''' 在庫引当起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowHikiatePopup(ByVal frm As LMC010F, ByVal ds As DataSet, ByVal arr As ArrayList, ByVal i As Integer) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim dsHiki As DataSet = New LMC040DS()
        Dim dt As DataTable = dsHiki.Tables(LMControlC.LMC040C_TABLE_NM_IN)
        Dim dtOutkaM As DataTable = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT)
        Dim dr As DataRow = dt.NewRow()
        'START YANAI 20110914 一括引当対応
        Dim dt2 As DataTable = dsHiki.Tables(LMC040C.TABLE_NM_IN2)
        'END YANAI 20110914 一括引当対応
        With dr
            'START YANAI 要望番号341
            'Dim hikiateFlg As String = "01"
            Dim hikiateFlg As String = "02"
            'END YANAI 要望番号341

            .Item("NRS_BR_CD") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
            .Item("WH_CD") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.WH_CD.ColNo))
            .Item("CUST_CD_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo))
            .Item("CUST_CD_M") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_M.ColNo))
            .Item("CUST_NM_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_NM.ColNo))
            .Item("CUST_NM_M") = String.Empty
            'START YANAI メモ②No.27
            '.Item("GOODS_CD_NRS") = dtOutkaM.Rows(i).Item("GOODS_CD_NRS").ToString()
            'START YANAI 要望番号510
            'If String.IsNullOrEmpty(dtOutkaM.Rows(i).Item("GOODS_CD_NRS_FROM").ToString()) = True Then
            '    .Item("GOODS_CD_NRS") = dtOutkaM.Rows(i).Item("GOODS_CD_NRS").ToString()
            'Else
            '    .Item("GOODS_CD_NRS") = dtOutkaM.Rows(i).Item("GOODS_CD_NRS_FROM").ToString()
            'End If
            .Item("GOODS_CD_NRS") = dtOutkaM.Rows(i).Item("GOODS_CD_NRS").ToString()
            'END YANAI 要望番号510
            'END YANAI メモ②No.27
            'START YANAI 要望番号1474 一括引当時、引当が出来ない場合のエラーメッセージ変更
            '.Item("GOODS_CD_CUST") = String.Empty
            .Item("GOODS_CD_CUST") = dtOutkaM.Rows(i).Item("GOODS_CD_CUST").ToString()
            'END YANAI 要望番号1474 一括引当時、引当が出来ない場合のエラーメッセージ変更
            .Item("GOODS_NM") = dtOutkaM.Rows(i).Item("GOODS_NM").ToString()
            .Item("SERIAL_NO") = dtOutkaM.Rows(i).Item("SERIAL_NO").ToString()
            .Item("LOT_NO") = dtOutkaM.Rows(i).Item("LOT_NO").ToString()
            .Item("IRIME") = Me.FormatNumValue(dtOutkaM.Rows(i).Item("IRIME").ToString())
            .Item("IRIME_UT") = String.Empty
            .Item("ALCTD_KB") = dtOutkaM.Rows(i).Item("ALCTD_KB").ToString()
            .Item("NB_UT") = String.Empty
            .Item("STD_IRIME_UT") = String.Empty
            .Item("PKG_NB") = 0.ToString()
            .Item("ALCTD_NB") = Me.FormatNumValue(dtOutkaM.Rows(i).Item("ALCTD_NB").ToString())
            .Item("BACKLOG_NB") = Me.FormatNumValue(dtOutkaM.Rows(i).Item("BACKLOG_NB").ToString())
            .Item("ALCTD_QT") = Me.FormatNumValue(dtOutkaM.Rows(i).Item("ALCTD_QT").ToString())
            .Item("BACKLOG_QT") = Me.FormatNumValue(dtOutkaM.Rows(i).Item("BACKLOG_QT").ToString())
            .Item("PKG_NB") = Me.FormatNumValue(dtOutkaM.Rows(i).Item("PKG_NB").ToString())
            .Item("HASU") = Me.FormatNumValue(dtOutkaM.Rows(i).Item("OUTKA_HASU").ToString())
            .Item("KOSU") = Me.FormatNumValue(dtOutkaM.Rows(i).Item("OUTKA_TTL_NB").ToString())
            .Item("SURYO") = Me.FormatNumValue(dtOutkaM.Rows(i).Item("OUTKA_TTL_QT").ToString())
            .Item("KONSU") = Convert.ToDecimal(
                                    ((Convert.ToDecimal(.Item("KOSU")) -
                                      Convert.ToDecimal(.Item("HASU"))) /
                                      Convert.ToDecimal(.Item("PKG_NB"))))

            .Item("HIKIATE_FLG") = hikiateFlg
            'START YANAI No.4
            '要望番号:1253 terakawa 2012.07.13 Start
            'Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD = '", Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo)), "' AND SUB_KB = '02'"))
            Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo)), "'AND CUST_CD = '" _
                                                                                                                             , Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo)), "' AND SUB_KB = '02'"))
            '要望番号:1253 terakawa 2012.07.13 End

            If 0 < custDetailsDr.Length Then
                .Item("SORT_FLG") = custDetailsDr(0).Item("SET_NAIYO")
            Else
                .Item("SORT_FLG") = "00"
            End If
            'END YANAI No.4

            'START YANAI 20110914 一括引当対応
            Dim outkaPlanDate As String = String.Concat(Left(Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_PLAN_DATE.ColNo)), 4),
                                              Mid(Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_PLAN_DATE.ColNo)), 6, 2),
                                              Mid(Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_PLAN_DATE.ColNo)), 9, 2))
            .Item("OUTKA_PLAN_DATE") = outkaPlanDate
            'END YANAI 20110914 一括引当対応
            'START YANAI メモ②No.27
            If String.IsNullOrEmpty(dtOutkaM.Rows(i).Item("GOODS_CD_NRS_FROM").ToString()) = True Then
                .Item("TANINUSI_FLG") = "00"
            Else
                .Item("TANINUSI_FLG") = "01"
            End If
            'END YANAI メモ②No.27
            'START YANAI 要望番号507
            .Item("OUTKA_S_CNT") = "0"
            'END YANAI 要望番号507

            'START YANAI 要望番号547
            .Item("PGID") = MyBase.GetPGID()
            'END YANAI 要望番号547

            'START YANAI 要望番号1474 一括引当時、引当が出来ない場合のエラーメッセージ変更
            .Item("OUTKA_NO_L") = dtOutkaM.Rows(i).Item("OUTKA_NO_L").ToString()
            .Item("OUTKA_NO_M") = dtOutkaM.Rows(i).Item("OUTKA_NO_M").ToString()
            'END YANAI 要望番号1474 一括引当時、引当が出来ない場合のエラーメッセージ変更

            'START 要望管理番号008131
            Dim nrsBrCd As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
#If False Then  'UPD 2021/09/06 023522   【LMS】安田倉庫移転_PG改修点洗い出し_改修(営業荻山)
            If ("96".Equals(nrsBrCd) OrElse "98".Equals(nrsBrCd)) _
                And "00135".Equals(Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo))) Then


#Else
            'UPD 2022/11/09 033380   【LMS】FFEM足柄工場LMS導入 F2追加, 2023/11/28 039659 F3 追加
            If ("96".Equals(nrsBrCd) OrElse "98".Equals(nrsBrCd) OrElse "F1".Equals(nrsBrCd) OrElse "F2".Equals(nrsBrCd) OrElse "F3".Equals(nrsBrCd)) _
                And "00135".Equals(Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo))) Then


#End If

                'ADD START 要望番号009476
                Dim whCd As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.WH_CD.ColNo))
                Dim filter As String = "KBN_GROUP_CD = 'F030'" _
                                     & " AND KBN_NM4 = '" & nrsBrCd & "'" _
                                     & " AND KBN_NM5 = '" & whCd & "'" _
                                     & " AND KBN_NM6 = '1'"

                Dim drsF030 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)
                If drsF030.Length > 0 Then
                    '在庫ステータス更新対象の倉庫の場合
                    'ADD END 要望番号009476
                    .Item("GOODS_COND_KB_3") = "00"
                End If  'ADD 要望番号009476
            End If
            'END 要望管理番号008131
#If True Then   'ADD 2020/05/22007999   【LMS】セミEDI_ジョンソンエンドジョンソンJ&J_土気自動倉庫預かり_入荷・出荷新規
            '引当対象設定
            Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString
            Dim custCd As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo))
            Dim JJ_FLG As String = LMConst.FLG.OFF
            Dim JJ_HIKIATE As String = String.Empty
            Dim drjj As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C038' AND KBN_NM1 = '", brCd, "' And KBN_NM2 = '", custCd, "'"))
            If 0 < drjj.Length Then
                JJ_FLG = LMConst.FLG.ON
                JJ_HIKIATE = drjj(0).Item("KBN_NM4").ToString.Trim
            End If
            .Item("JJ_FLG") = JJ_FLG
            .Item("JJ_HIKIATE") = JJ_HIKIATE
#End If
        End With
        dt.Rows.Add(dr)

        'START YANAI 20110914 一括引当対応
        'START YANAI 20111122 一括引当バグ
        'Dim max As Integer = ds.Tables(LMC010C.TABLE_NM_ZAI_TRS).Rows.Count - 1
        'For j As Integer = 0 To max
        '    dr = dt2.NewRow()
        '    dr.Item("NRS_BR_CD") = ds.Tables(LMC010C.TABLE_NM_ZAI_TRS).Rows(j).Item("NRS_BR_CD").ToString()
        '    dr.Item("ZAI_REC_NO") = ds.Tables(LMC010C.TABLE_NM_ZAI_TRS).Rows(j).Item("ZAI_REC_NO").ToString()
        '    dr.Item("ALCTD_NB") = ds.Tables(LMC010C.TABLE_NM_ZAI_TRS).Rows(j).Item("ALCTD_NB").ToString()
        '    dr.Item("ALLOC_CAN_NB") = ds.Tables(LMC010C.TABLE_NM_ZAI_TRS).Rows(j).Item("ALLOC_CAN_NB").ToString()
        '    dr.Item("ALCTD_QT") = ds.Tables(LMC010C.TABLE_NM_ZAI_TRS).Rows(j).Item("ALCTD_QT").ToString()
        '    dr.Item("ALLOC_CAN_QT") = ds.Tables(LMC010C.TABLE_NM_ZAI_TRS).Rows(j).Item("ALLOC_CAN_QT").ToString()
        '    dt2.Rows.Add(dr)
        'Next
        Dim zaidr() As DataRow = Nothing
        zaidr = ds.Tables(LMC010C.TABLE_NM_ZAI_TRS).Select("ERR_FLG = '00'")
        Dim max As Integer = zaidr.Length - 1
        For j As Integer = 0 To max
            dr = dt2.NewRow()
            dr.Item("NRS_BR_CD") = zaidr(j).Item("NRS_BR_CD").ToString()
            dr.Item("ZAI_REC_NO") = zaidr(j).Item("ZAI_REC_NO").ToString()
            dr.Item("ALCTD_NB") = zaidr(j).Item("ALCTD_NB").ToString()
            dr.Item("ALLOC_CAN_NB") = zaidr(j).Item("ALLOC_CAN_NB").ToString()
            dr.Item("ALCTD_QT") = zaidr(j).Item("ALCTD_QT").ToString()
            dr.Item("ALLOC_CAN_QT") = zaidr(j).Item("ALLOC_CAN_QT").ToString()
            dt2.Rows.Add(dr)
        Next
        'END YANAI 20111122 一括引当バグ
        'END YANAI 20110914 一括引当対応

        prm.ParamDataSet = dsHiki

        'Pop起動
        Return Me.PopFormShow(prm, "LMC040")

    End Function

    ''' <summary>
    ''' 在庫引当の戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnHikiatePop(ByVal frm As LMC010F, ByVal ds As DataSet, ByVal arr As ArrayList, ByVal i As Integer) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        '★★★ START YANAI 一時的に小分け・サンプルの時はスルーするように設定
        If ("03").Equals(ds.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT).Rows(i).Item("ALCTD_KB").ToString) = True OrElse
            ("04").Equals(ds.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT).Rows(i).Item("ALCTD_KB").ToString) = True Then
            Return True
        End If
        '★★★ END YANAI 一時的に小分け・サンプルの時はスルーするように設定
        'START YANAI 20111122 一括引当バグ
        Dim zaidr() As DataRow = Nothing
        zaidr = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).Select(String.Concat("OUTKA_NO_L = '", Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)), "' AND ",
                                                                            "ERR_FLG = '01'"))

        If zaidr.Length > 0 Then
            Return True
        End If
        'END YANAI 20111122 一括引当バグ

        Dim prm As LMFormData = Me.ShowHikiatePopup(frm, ds, arr, i)
        '検索成功時共通処理を行う

        If prm.ReturnFlg = True Then
            Dim dt As DataTable = prm.ParamDataSet.Tables(LMControlC.LMC040C_TABLE_NM_OUT)
            '引当の戻り値をdsにセットする

            If dt.Rows.Count > 0 Then

                '出荷Mデータセットの生成
                Call Me.SetDatasetOutKaM(frm, ds, arr, i, dt)

                '出荷Sデータセットの生成
                Call Me.SetDatasetOutKaS(frm, ds, arr, i, dt)

                '在庫データセットの生成
                Call Me.SetDatasetZaiTrs(frm, ds, arr, i, dt)

            End If

        Else

            'START YANAI メモ②No.15,16,17
            'Return False
            'LMC010_OUTKA_M_INのERR_FLG更新
            Call Me.SetDatasetOutMIn(frm, ds, arr, i)

            'START YANAI 要望番号1474 一括引当時、引当が出来ない場合のエラーメッセージ変更
            'MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, MyBase.GetMessageID, , Convert.ToString(arr(i)))
            If ("E192").Equals(MyBase.GetMessageID) = True Then

                '2017/09/25 修正 李↓
                MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E486",
                        New String() {String.Concat(
                                        lgm.Selector({"[出荷管理番号(大)：", "[Shipment control number(L)：", "[출하관리번호(大) : ", "中国語"}), prm.ParamDataSet.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0).Item("OUTKA_NO_L").ToString(),
                                        lgm.Selector({"、出荷管理番号(中)：", ",Shipment control number(M)：", "출하관리번호(中) : ", "中国語"}), prm.ParamDataSet.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0).Item("OUTKA_NO_M").ToString(),
                                        lgm.Selector({"、商品コード：", ",Goods Code：", "상품코드 : ", "中国語"}), prm.ParamDataSet.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0).Item("GOODS_CD_CUST").ToString(),
                                        lgm.Selector({"、商品名：", ",Goods Name：", "상품명 : ", "中国語"}), prm.ParamDataSet.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0).Item("GOODS_NM").ToString(), "]"
                                      )},
                        Convert.ToString(arr(i)))
                '2017/09/25 修正 李↑

                '①対応
            ElseIf ("E536").Equals(MyBase.GetMessageID) = True Then

                '2017/09/25 修正 李↓
                MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E535",
                                                New String() {
                                                    lgm.Selector({"引当該当商品にリザーブが含まれる", "It includes reserved provision Shipping", "충당해당상품에 리저브가 포함되어 있음", "中国語"}),
                                                    String.Concat(
                                                        lgm.Selector({"[出荷管理番号(大)：", "[Shipment control number(L)：", "[출하관리번호(大) : ", "中国語"}), prm.ParamDataSet.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0).Item("OUTKA_NO_L").ToString(),
                                                        lgm.Selector({"、出荷管理番号(中)：", ",Shipment control number(M)：", "출하관리번호(中) : ", "中国語"}), prm.ParamDataSet.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0).Item("OUTKA_NO_M").ToString(),
                                                        lgm.Selector({"、商品コード：", ",Goods Code：", "상품코드 : ", "中国語"}), prm.ParamDataSet.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0).Item("GOODS_CD_CUST").ToString(),
                                                        lgm.Selector({"、商品名：", ",Goods Name：", "상품명 : ", "中国語"}), prm.ParamDataSet.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0).Item("GOODS_NM").ToString(), "]"
                                                )},
                                                Convert.ToString(arr(i)))
                '2017/09/25 修正 李↑

                '②対応
            ElseIf ("E537").Equals(MyBase.GetMessageID) = True Then

                '2017/09/25 修正 李↓
                MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E535",
                                                New String() {
                                                    lgm.Selector({"引当該当商品は有効期限切れです", "Provisions applicable product is expired", "충당해당상품은 유효기간이 지났음", "中国語"}),
                                                    String.Concat(
                                                        lgm.Selector({"[出荷管理番号(大)：", "[Shipment control number(L)：", "[출하관리번호(大) : ", "中国語"}), prm.ParamDataSet.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0).Item("OUTKA_NO_L").ToString(),
                                                        lgm.Selector({"、出荷管理番号(中)：", ",Shipment control number(M)：", "출하관리번호(中) : ", "中国語"}), prm.ParamDataSet.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0).Item("OUTKA_NO_M").ToString(),
                                                        lgm.Selector({"、商品コード：", ",Goods Code：", "상품코드 : ", "中国語"}), prm.ParamDataSet.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0).Item("GOODS_CD_CUST").ToString(),
                                                        lgm.Selector({"、商品名：", ",Goods Name：", "상품명 : ", "中国語"}), prm.ParamDataSet.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0).Item("GOODS_NM").ToString(), "]"
                                                )},
                                                Convert.ToString(arr(i)))
                '2017/09/25 修正 李↑

            Else
                MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, MyBase.GetMessageID, , Convert.ToString(arr(i)))
            End If
            'END YANAI 要望番号1474 一括引当時、引当が出来ない場合のエラーメッセージ変更
            'END YANAI メモ②No.15,16,17

            'START YANAI 引当エラーは音声CSV出力しない
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
            frm.sprDetail.SetCellValue(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.DEF, LMConst.FLG.OFF)
#Else
            frm.sprDetail.SetCellValue(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
#End If
            'END YANAI 引当エラーは音声CSV出力しない

            '引当エラー時は処理中止 要望番号1523
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' Pop起動処理
    ''' </summary>
    ''' <param name="prm">パラメータクラス</param>
    ''' <param name="id">画面ID</param>
    ''' <returns>パラメータクラス</returns>
    ''' <remarks></remarks>
    Private Function PopFormShow(ByVal prm As LMFormData, ByVal id As String) As LMFormData

        LMFormNavigate.NextFormNavigate(Me, id, prm)

        Return prm

    End Function

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function

    ''' <summary>
    ''' キャッシュから名称取得（全項目）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetCachedName(ByVal frm As LMC010F, ByVal eventShubetsu As LMC010C.EventShubetsu)

        With frm

            If (LMC010C.EventShubetsu.KENSAKU).Equals(eventShubetsu) = True Then
                '荷主名称
                If String.IsNullOrEmpty(.txtCustCD.TextValue) = False Then
                    .lblCustNM.TextValue = Me.GetCachedCust(.cmbEigyo.SelectedValue.ToString, .txtCustCD.TextValue, "00", "00", "00")
                End If
            End If

            If (LMC010C.EventShubetsu.KENSAKU).Equals(eventShubetsu) = True Then
                '担当者名称
                If String.IsNullOrEmpty(.txtPicCD.TextValue) = False Then
                    .lblPicNM.TextValue = Me.GetCachedUser(.txtPicCD.TextValue)
                End If
            End If

            If (LMC010C.EventShubetsu.HENKO).Equals(eventShubetsu) = True Then
                '運送会社名称
                If String.IsNullOrEmpty(.txtTrnCD.TextValue) = False AndAlso
                    String.IsNullOrEmpty(.txtTrnBrCD.TextValue) = False Then
                    .lblTrnNM.TextValue = Me.GetCachedUnsoco(.txtTrnCD.TextValue, .txtTrnBrCD.TextValue)
                End If
            End If

        End With

    End Sub

    ''' <summary>
    ''' 荷主キャッシュから名称取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCachedCust(ByVal nrsBrCd As String,
                                  ByVal custCdL As String,
                                   ByVal custCdM As String,
                                   ByVal custCdS As String,
                                   ByVal custCdSS As String) As String

        Dim dr As DataRow() = Nothing

        '荷主名称
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat(
                                                                          "NRS_BR_CD = '", nrsBrCd, "' AND " _
                                                                         , "CUST_CD_L = '", custCdL, "' AND " _
                                                                         , "CUST_CD_M = '", custCdM, "' AND " _
                                                                         , "CUST_CD_S = '", custCdS, "' AND " _
                                                                         , "CUST_CD_SS = '", custCdSS, "' AND " _
                                                                         , "SYS_DEL_FLG = '0'"))
        If 0 < dr.Length Then
            Return dr(0).Item("CUST_NM_L").ToString
        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' ユーザーキャッシュから名称取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCachedUser(ByVal userCd As String) As String

        Dim dr As DataRow() = Nothing

        'ユーザー名称
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat(
                                                                            "USER_CD = '", userCd, "' AND " _
                                                                          , "SYS_DEL_FLG = '0'"))
        If 0 < dr.Length Then
            Return dr(0).Item("USER_NM").ToString
        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' 運送会社キャッシュから名称取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCachedUnsoco(ByVal unsocoCd As String, ByVal unsocoBrCd As String) As String

        Dim dr As DataRow() = Nothing

        '運送会社名称
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(String.Concat(
                                                                            "UNSOCO_CD = '", unsocoCd, "' AND " _
                                                                          , "UNSOCO_BR_CD = '", unsocoBrCd, "' AND " _
                                                                          , "SYS_DEL_FLG = '0'"))
        If 0 < dr.Length Then
            Return String.Concat(dr(0).Item("UNSOCO_NM").ToString, "　", dr(0).Item("UNSOCO_BR_NM").ToString)
        End If

        Return String.Empty

    End Function
    'START YANAI 20110914 一括引当対応
    ''' <summary>
    ''' 区分キャッシュから送り状を印刷する荷主を取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function setPrintCust(ByVal frm As LMC010F, ByVal ds As DataSet) As DataSet

        Dim outDr As DataRow = Nothing
        Dim dr As DataRow() = Nothing
        '荷主
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'S040' AND ",
                                                                                    "KBN_NM1 = '", frm.cmbEigyo.SelectedValue, "'"))
        Dim max As Integer = dr.Length - 1

        For i As Integer = 0 To max
            outDr = ds.Tables(LMC010C.TABLE_NM_PRINT_CUST).NewRow()
            'START YANAI 要望番号1151 埼玉：大日精化 修正依頼
            'outDr("NRS_BR_CD") = dr(i).Item("NRS_BR_CD").ToString()
            'outDr("CUST_CD_L") = dr(i).Item("CUST_CD_L").ToString()
            outDr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
            outDr("CUST_CD_L") = dr(i).Item("KBN_NM2").ToString()
            'END YANAI 要望番号1151 埼玉：大日精化 修正依頼

            '検索条件をデータセットに設定
            ds.Tables(LMC010C.TABLE_NM_PRINT_CUST).Rows.Add(outDr)
        Next

        Return ds

    End Function
    'END YANAI 20110914 一括引当対応

    'START YANAI 20110914 一括引当対応
    ''' <summary>
    ''' 出荷(大)の進捗区分、出荷梱包個数の設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">データセット</param>
    ''' ''' <param name="flgHW">ハネウェル識別フラグ</param>
    ''' <remarks></remarks>
    Private Function SetOutkaL(ByVal frm As LMC010F, ByVal ds As DataSet, ByVal flgHW As String) As DataSet

        Dim dr As DataRow = Nothing
        Dim outkaMDr() As DataRow = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT).Select(Nothing, "OUTKA_NO_L ASC")
        Dim max As Integer = outkaMDr.Length - 1

        Dim outDt As DataTable = ds.Tables(LMC010C.TABLE_NM_OUTKA_M)
        Dim outDr() As DataRow = Nothing
        Dim outMax As Integer = 0

        Dim outSDt As DataTable = ds.Tables(LMC010C.TABLE_NM_OUTKA_S)
        Dim outSDr() As DataRow = Nothing

        Dim outkaNoL As String = String.Empty
        Dim oldStateKb As String = String.Empty
        Dim newStateKb As String = String.Empty
        Dim sokoDr() As DataRow = Nothing
        Dim outkaPkgNb As Decimal = 0

        For i As Integer = 0 To max
            If (outkaNoL).Equals(outkaMDr(i).Item("OUTKA_NO_L").ToString()) = False Then
                '出荷管理番号(大)が変わった時だけ処理
                outkaNoL = outkaMDr(i).Item("OUTKA_NO_L").ToString()
                oldStateKb = outkaMDr(i).Item("OUTKA_STATE_KB").ToString()
                dr = ds.Tables(LMC010C.TABLE_NM_OUTKA_L_IN).NewRow()
                dr.Item("OUTKA_NO_L") = outkaNoL

                outDr = outDt.Select(String.Concat("OUTKA_NO_L = '", outkaNoL, "'"))
                outMax = outDr.Length - 1


                Select Case flgHW  '2013/03/11 Notes1932 CASE分追加
                    Case "0"  ' ハネウェルCSV引当処理から(JikkoShori)ではない場合(一括引当処理)
                        '進捗区分設定処理
                        For j As Integer = 0 To outMax
                            '①引当残個数・数量が0の場合、"50"
                            If Convert.ToDecimal(outDr(j).Item("BACKLOG_NB").ToString()) = 0 AndAlso
                                Convert.ToDecimal(outDr(j).Item("BACKLOG_QT").ToString()) = 0 Then
                                newStateKb = LMC010C.SINTYOKU50
                            Else
                                'START YANAI メモ②No.15,16,17
                                'newStateKb = String.Empty
                                newStateKb = LMC010C.SINTYOKU10
                                'END YANAI メモ②No.15,16,17
                                Exit For
                            End If
                        Next

                    Case "1"  ' ハネウェルCSV引当処理から(JikkoShoriForHW)の場合(ハネウェルCSV引当処理)
                        '進捗区分設定処理
                        For j As Integer = 0 To outMax
                            '①引当残個数・数量が0の場合、"50"
                            If Convert.ToDecimal(outDr(j).Item("BACKLOG_NB").ToString()) = 0 AndAlso
                                Convert.ToDecimal(outDr(j).Item("BACKLOG_QT").ToString()) = 0 Then
                                newStateKb = LMC010C.SINTYOKU50
                            Else
                                'START YANAI メモ②No.15,16,17
                                'newStateKb = String.Empty
                                newStateKb = LMC010C.SINTYOKU10
                                'END YANAI メモ②No.15,16,17
                                'Exit For '2013/03/11 Notes1932 コメントアウト
                            End If
                        Next

                End Select


                'START YANAI メモ②No.15,16,17
                'If String.IsNullOrEmpty(newStateKb) = True Then
                '    '②出荷(小)が1件もない場合、"10"
                '    outSDr = outSDt.Select(String.Concat("OUTKA_NO_L = '", outkaNoL, "'"))
                '    If outSDr.Length < 1 Then
                '        newStateKb = LMC010C.SINTYOKU10
                '    End If
                'End If

                'If String.IsNullOrEmpty(newStateKb) = True Then
                '    '③(SOKO_MST)OUTKA_SASHIZU_PRT_YNが"00"以外の場合
                '    sokoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SOKO).Select(String.Concat("WH_CD = ", " '", outkaMDr(i).Item("WH_CD").ToString(), "' "))
                '    If ("00").Equals(sokoDr(0).Item("OUTKA_SASHIZU_PRT_YN").ToString()) = False Then
                '        newStateKb = LMC010C.SINTYOKU10
                '    End If
                'End If

                'If String.IsNullOrEmpty(newStateKb) = True Then
                '    '④ピッキングリスト区分(PICK_KB）が"01"以外の場合
                '    If ("01").Equals(outkaMDr(i).Item("PICK_KB").ToString()) = False Then
                '        newStateKb = LMC010C.SINTYOKU30
                '    End If
                'End If

                'If String.IsNullOrEmpty(newStateKb) = True Then
                '    '⑤(SOKO_MST)OUTOKA_KANRYO_YNが"00"以外の場合
                '    If ("00").Equals(sokoDr(0).Item("OUTOKA_KANRYO_YN").ToString()) = False Then
                '        newStateKb = LMC010C.SINTYOKU30
                '    End If
                'End If

                'If String.IsNullOrEmpty(newStateKb) = True Then
                '    '⑥(SOKO_MST)OUTKA_KENPIN_YNが"00"以外の場合
                '    If ("00").Equals(sokoDr(0).Item("OUTKA_KENPIN_YN").ToString()) = False Then
                '        newStateKb = LMC010C.SINTYOKU40
                '    End If
                'End If

                'If String.IsNullOrEmpty(newStateKb) = True Then
                '    '⑦(SOKO_MST)OUTKA_KENPIN_YNが"01"以外の場合
                '    If ("01").Equals(sokoDr(0).Item("OUTKA_KENPIN_YN").ToString()) = False Then
                '        newStateKb = LMC010C.SINTYOKU50
                '    End If
                'End If
                'END YANAI メモ②No.15,16,17

                If newStateKb < oldStateKb Then
                    newStateKb = oldStateKb
                End If
                dr.Item("OUTKA_STATE_KB") = newStateKb

                '出荷梱包個数設定処理
                outkaPkgNb = 0

                '2014.04.02 修正START
                Select Case flgHW
                    Case "0"
                        For j As Integer = 0 To outMax
                            outkaPkgNb = outkaPkgNb + Convert.ToDecimal(outDr(j).Item("OUTKA_M_PKG_NB").ToString())
                        Next
                    Case "1"
                        Dim dtNewOM As DataTable = Me.GetSumDs(outDt)
                        outMax = dtNewOM.Rows.Count - 1
                        For j As Integer = 0 To outMax
                            outkaPkgNb = outkaPkgNb + Convert.ToDecimal(dtNewOM(j).Item("OUTKA_M_PKG_NB").ToString())
                        Next
                End Select
                '2014.04.02 修正END

                dr.Item("OUTKA_PKG_NB") = outkaPkgNb

                '検索条件をデータセットに設定
                ds.Tables(LMC010C.TABLE_NM_OUTKA_L_IN).Rows.Add(dr)

            End If

        Next

        Return ds

    End Function
    'END YANAI 20110914 一括引当対応

    'START YANAI 引当エラーは音声CSV出力しない
    ''' <summary>
    ''' 引当の更新時、排他エラーになったデータのチェックをはずす
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetCheckBox(ByVal frm As LMC010F, ByVal ds As DataSet)

        Dim max As Integer = ds.Tables(LMC010C.TABLE_NM_ERR).Rows.Count - 1
        For i As Integer = 0 To max
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                        frm.sprDetail.SetCellValue(Convert.ToInt32(ds.Tables(LMC010C.TABLE_NM_ERR).Rows(i).Item("ROW_NO").ToString), LMC010C.SprColumnIndex.DEF, LMConst.FLG.OFF)
#Else
            frm.sprDetail.SetCellValue(Convert.ToInt32(ds.Tables(LMC010C.TABLE_NM_ERR).Rows(i).Item("ROW_NO").ToString), LMC010G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
#End If
        Next

    End Sub
    'END YANAI 引当エラーは音声CSV出力しない

#End Region 'PopUp

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMC010F, ByRef rtDs As DataSet)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim dr As DataRow = rtDs.Tables(LMC010C.TABLE_NM_IN).NewRow()

        Dim custDtlDr As DataRow() = Nothing

        '検索条件　単項目部
        dr("OUTKA_STATE_KB1") = frm.chkStaYotei.GetBinaryValue.ToString().Replace("0", "")
        dr("OUTKA_STATE_KB2") = frm.chkStaPrint.GetBinaryValue.ToString().Replace("0", "")
        dr("OUTKA_STATE_KB3") = frm.chkStaShukko.GetBinaryValue.ToString().Replace("0", "")
        dr("OUTKA_STATE_KB4") = frm.chkStaKenpin.GetBinaryValue.ToString().Replace("0", "")
        dr("OUTKA_STATE_KB5") = frm.chkStaShukka.GetBinaryValue.ToString().Replace("0", "")
        dr("OUTKA_STATE_KB6") = frm.chkStaHoukoku.GetBinaryValue.ToString().Replace("0", "")
        dr("OUTKA_STATE_KB7") = frm.chkStaTorikeshi.GetBinaryValue.ToString().Replace("0", "")
        dr("CUST_CD_L") = frm.txtCustCD.TextValue
        dr("TANTO_CD") = frm.txtPicCD.TextValue
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("WH_CD") = frm.cmbSoko.SelectedValue
        dr("SEARCH_DATE_KBN") = frm.cmbSearchDate.SelectedValue
        dr("SEARCH_DATE_FROM") = frm.imdSearchDate_From.TextValue
        dr("SEARCH_DATE_TO") = frm.imdSearchDate_To.TextValue
        'START YANAI 要望番号917
        'dr("PRINT_KBN") = frm.cmbPrintStatus.SelectedValue
        dr("PRINT_KBN") = "00"
        'END YANAI 要望番号917
        dr("PRINT_SYUBETU") = frm.cmbPrintSyubetu.SelectedValue
        If dr("PRINT_SYUBETU").ToString().Equals("03") = True Then

            custDtlDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyo.SelectedValue.ToString(), "' AND ",
                                                                               "CUST_CD = '", frm.txtCustCD.TextValue.ToString(), "' AND ",
                                                                               "SUB_KB = '54'"))

            If 0 < custDtlDr.Length Then
                If String.IsNullOrEmpty(custDtlDr(0).Item("SET_NAIYO").ToString) = False Then
                    dr.Item("NOT_NHS_FLAG") = custDtlDr(0).Item("SET_NAIYO").ToString
                End If
            End If

        End If
        'SHINODA START
        dr("PICK_SYUBETU") = frm.cmbPickSyubetu.SelectedValue
        'SHINODA END
        dr("PRINT_DATE_FROM") = frm.imdPrintDate_From.TextValue
        dr("PRINT_DATE_TO") = frm.imdPrintDate_To.TextValue
        dr("OUTKA_NO_L_DIRECT") = frm.txtOutkaNoL.TextValue

        If frm.chkSelectByNrsB.Checked = True Then
            dr("TANTO_USER_FLG") = LMConst.FLG.ON
        Else
            dr("TANTO_USER_FLG") = LMConst.FLG.OFF
        End If
        dr("USER_ID") = LMUserInfoManager.GetUserID

        '2017/09/25 修正 李↓
        dr("LANG_FLG") = lgm.MessageLanguage
        '2017/09/25 修正 李↑

#If True Then  ' FFEM機能改修(納品書未受信状態表示対応) 20170127 added
        Dim colorType As String = ""
        If (_G.IsCustomizeRowColorCustWithType(TryCast(dr("NRS_BR_CD"), String) _
                                             , TryCast(dr("CUST_CD_L"), String) _
                                             , colorType)) Then

            ' 行カラー変更タイプ設定
            dr("CUSTOMIZE_ROW_COLOR_TYPE") = colorType
        Else
            dr("CUSTOMIZE_ROW_COLOR_TYPE") = ""
        End If
#End If

        '検索条件　入力部（スプレッド）
        With frm.sprDetail.ActiveSheet

            dr("CUST_ORD_NO") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.CUST_ORD_NO.ColNo))
            dr("CUST_NM") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.CUST_NM.ColNo))
            'START YANAI 要望番号748
            dr("CUST_CD_S") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.CUST_CD_S.ColNo))
            'END YANAI 要望番号748
            dr("DEST_NM") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.DEST_NM.ColNo))
            '(2013.01.11)要望番号1700 -- START --
            'dr("GOODS_NM") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.GOODS_NM.ColNo))
            'dr("GOODS_NM") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.GOODS_NM.ColNo)).Replace("%", "[%]").Replace("_", "[_]").Replace("[", "[[]")
            dr("GOODS_NM") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.GOODS_NM.ColNo)).Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]")   '要望番号:1823（ロットＮｏの検索条件に%を含んだ場合、置換される値がおかしい）対応　 2013/02/05 本明
            '(2013.01.11)要望番号1700 --  END  --
            dr("DEST_AD") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.DEST_AD.ColNo))
            dr("DENP_NO") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.DENP_NO.ColNo))
            dr("UNSOCO_NM") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.UNSOCO_NM.ColNo))
            dr("BIN_KB") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.BIN_KB_NM.ColNo))
            '2018/04/20 001547 【LMS】出荷データ検索画面_注文番号項目追加(千葉BC物管２_石井) Annen add start
            dr("BUYER_ORD_NO") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.BUYER_ORD_NO.ColNo))
            '2018/04/20 001547 【LMS】出荷データ検索画面_注文番号項目追加(千葉BC物管２_石井) Annen add end
            dr("WEB_OUTKA_NO_L") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.WEB_OUTKA_NO_L.ColNo))
            dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            dr("SYUBETU_KB") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.SYUBETU_KB_NM.ColNo))
            'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷検索にロット、シリアル）
            dr("LOT_NO") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.LOT_NO_S.ColNo))
            dr("SERIAL_NO") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.SERIAL_NO_S.ColNo))
            'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷検索にロット、シリアル）
            dr("REMARK_UNSO") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.REMARK_UNSO.ColNo))     '要望番号1856対応　2013/02/21　本明
            dr("SYS_ENT_USER") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.SYS_ENT_USER.ColNo))
            dr("SYS_UPD_USER") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.SYS_UPD_USER.ColNo))

            '区分マスタ参照項目値判定
            If dr("SYUBETU_KB").ToString().Length() < 2 Then
                dr("SYUBETU_KB") = String.Empty
            End If

#If True Then ' 出荷作業ステータス対応 20160824 added inoue
            dr("SEARCH_WH_WORK_STATUS") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.WH_WORK_STATUS_NM.ColNo))
#End If
            dr("SEARCH_WH_TAB_STATUS") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.WH_TAB_STATUS_NM.ColNo))
            dr("COA_UMU") = Me._LMCconV.GetCellValue(.Cells(0, LMC010G.sprDetailDef.COA_UMU.ColNo))         'ADD 2019/06/18 004870
        End With

        '検索条件をデータセットに設定
        rtDs.Tables(LMC010C.TABLE_NM_IN).Rows.Add(dr)

        '再検索用データセットに保存
        Me._FindDs = rtDs

    End Sub



    ''' <summary>
    ''' 印刷時、利用する更新用INデータセット
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInPrintData(ByVal frm As LMC010F, ByVal ds As DataSet) As DataSet

        '印刷種別格納
        Me._PrintSybetu = frm.cmbPrint.SelectedValue.ToString()
        Dim updateCol As String = String.Empty

        Select Case Me._PrintSybetu
            Case "01" '荷札
                updateCol = "NIHUDA_FLAG"
            Case "02" '送状
                updateCol = "DENP_FLAG"
            Case "03" '納品書
                updateCol = "NHS_FLAG"
            Case "04" '分析表
                updateCol = "COA_FLAG"
                'yamanaka 2012.08.06 Start
            Case "05" '纏めピッキングリスト
                updateCol = "PIG_FLAG"
                'yamanaka 2012.08.06 End
            Case "06" '纏め送状
                updateCol = "DENP_FLAG"
            Case "07" '出荷報告
                updateCol = "HOKOKU_FLAG"
            Case "08" '一括印刷
                updateCol = "ALL_PRINT_FLAG"
                'START YANAI 20120122 立会書印刷対応
                'Case "09" '出荷指図
                '    updateCol = "SASZ_USER"
            Case "09" '立会書
                'Me._PrintSybetu = "10"  '"09"は出荷指図書のため、強制的に"10"に変更
                updateCol = String.Empty
                'END YANAI 20120122 立会書印刷対応
                'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
            Case "10" '立会書
                Me._PrintSybetu = "11"  '"10"は立会書のため、強制的に"11"に変更
                updateCol = "NIHUDA_FLAG"
                'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
            Case "13" '纏め送状
                updateCol = "DENP_FLAG"
            Case "14" 'ITW荷札
                updateCol = "NIHUDA_FLAG"

            Case LMC010C.OutkaPrintSelectValues.PACKAGE_DETAILS
                updateCol = String.Empty

#If True Then   'ADD 2018/07/24 依頼番号 : 001540  
            Case LMC010C.OutkaPrintSelectValues.UNSO_HOKEN
                updateCol = String.Empty
#End If

#If True Then   'ADD 2018/10/10 依頼番号 : 002381  
            Case LMC010C.OutkaPrintSelectValues.HOKOKU_DATE
                updateCol = String.Empty
#End If
            Case LMC010C.OutkaPrintSelectValues.YELLOW_CARD
                updateCol = String.Empty

            Case LMC010C.OutkaPrintSelectValues.ATTEND
                updateCol = String.Empty

            Case LMC010C.OutkaPrintSelectValues.OUTBOUND_CHECK
                updateCol = String.Empty

#If True Then 'ADD 2023/03/29 送品案内書(FFEM)追加
            Case LMC010C.OutkaPrintSelectValues.SHIPMENTGUIDE
                updateCol = String.Empty
#End If

            Case LMC010C.OutkaPrintSelectValues.DOKU_JOJU
                updateCol = String.Empty

            Case Else
                Return ds

        End Select

        Dim max As Integer = Me._ChkList.Count() - 1
        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        'Dim userNrCd As String = LM.Base.LMUserInfoManager.GetNrsBrCd()
        Dim userNrCd As String = frm.cmbEigyo.SelectedValue.ToString()
        Dim dt As DataTable = ds.Tables(LMC010C.TABLE_NM_IN_PRINT)
        Dim dr As DataRow = Nothing
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        Dim rowNo As Integer = 0
        Dim flg As Boolean = True
        Dim custDetailsDr As DataRow() = Nothing
        'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
        Dim matomeDr() As DataRow = Nothing
        'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
        '2013.1.24 kobayashi : 埼玉BP・まとめ送り状対応 Start
        Dim matomeDnpNum As Integer = 0
        '2013.1.24 kobayashi : 埼玉BP・まとめ送り状対応 End

        For i As Integer = 0 To max

            With spr.ActiveSheet

                '変換ミスはサーバに渡さない
                flg = Integer.TryParse(Me._ChkList(i).ToString(), rowNo)
                If flg = False Then
                    Continue For
                End If

                dr = dt.NewRow()
                dr.Item("USER_BR_CD") = userNrCd
                dr.Item("NRS_BR_CD") = .Cells(rowNo, LMC010G.sprDetailDef.NRS_BR_CD.ColNo).Value()
                dr.Item("OUTKA_NO_L") = .Cells(rowNo, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo).Value()
                dr.Item("OUTKA_STATE_KB") = Me.GetStageData(spr, rowNo)
                dr.Item("PICK_KB") = .Cells(rowNo, LMC010G.sprDetailDef.PICK_KB.ColNo).Value()
                dr.Item("OUTOKA_KANRYO_YN") = .Cells(rowNo, LMC010G.sprDetailDef.OUTOKA_KANRYO_YN.ColNo).Value()
                dr.Item("OUTKA_KENPIN_YN") = .Cells(rowNo, LMC010G.sprDetailDef.OUTKA_KENPIN_YN.ColNo).Value()
                dr.Item("SYS_UPD_DATE") = .Cells(rowNo, LMC010G.sprDetailDef.SYS_UPD_DATE.ColNo).Value()
                dr.Item("SYS_UPD_TIME") = .Cells(rowNo, LMC010G.sprDetailDef.SYS_UPD_TIME.ColNo).Value()
                dr.Item("OUTKA_SASHIZU_PRT_YN") = .Cells(rowNo, LMC010G.sprDetailDef.OUTKA_SASHIZU_PRT_YN.ColNo).Value()
                dr.Item("S_COUNT") = .Cells(rowNo, LMC010G.sprDetailDef.S_COUNT.ColNo).Value()
                'START YANAI 20120122 立会書印刷対応
                'dr.Item(updateCol) = "01"
                If String.IsNullOrEmpty(updateCol) = False Then
                    dr.Item(updateCol) = "01"
                End If
                'END YANAI 20120122 立会書印刷対応
                dr.Item("PRINT_KB") = Me._PrintSybetu
                dr.Item("ROW_NO") = rowNo
                dr.Item("CUST_CD_L") = .Cells(rowNo, LMC010G.sprDetailDef.CUST_CD_L.ColNo).Value()
                dr.Item("CUST_CD_M") = .Cells(rowNo, LMC010G.sprDetailDef.CUST_CD_M.ColNo).Value()
                dr.Item("NIHUDA_YN") = .Cells(rowNo, LMC010G.sprDetailDef.NIHUDA_YN.ColNo).Value()
                'START YANAI メモ②No.2
                dr.Item("SASZ_USER") = .Cells(rowNo, LMC010G.sprDetailDef.SASZ_USER.ColNo).Value()
                'END YANAI メモ②No.2
                'START UMANO メモ(EDI)No.49
                dr.Item("OUTKA_PKG_NB") = .Cells(rowNo, LMC010G.sprDetailDef.OUTKA_PKG_NB.ColNo).Value()
                'START YANAI 要望番号857
                '荷主明細マスタに設定されている荷主の場合、SET_NAIYOに設定されている部数をMAX部数とする(荷札のみ)
                If ("01").Equals(Me._PrintSybetu) = True OrElse
                    ("08").Equals(Me._PrintSybetu) = True Then
                    '要望番号:1253 terakawa 2012.07.13 Start
                    'custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD = '", dr.Item("CUST_CD_L").ToString, "' AND ", _
                    '                                                                                                "SUB_KB = '21'"))

                    custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", dr.Item("NRS_BR_CD").ToString, "' AND ",
                                                                                                                    "CUST_CD = '", dr.Item("CUST_CD_L").ToString, "' AND ",
                                                                                                                    "SUB_KB = '21'"))

                    '要望番号:1253 terakawa 2012.07.13 End
                    If 0 < custDetailsDr.Length Then
                        If Convert.ToDecimal(custDetailsDr(0).Item("SET_NAIYO").ToString) < Convert.ToDecimal(dr.Item("OUTKA_PKG_NB").ToString) Then
                            dr.Item("OUTKA_PKG_NB") = custDetailsDr(0).Item("SET_NAIYO").ToString
                        End If
                    End If
                End If
                If ("11").Equals(Me._PrintSybetu) = True Then
                    '要望番号:1253 terakawa 2012.07.13 Start
                    'custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD = '", dr.Item("CUST_CD_L").ToString, "' AND ", _
                    '                                                                                                "SUB_KB = '21'"))

                    custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", dr.Item("NRS_BR_CD").ToString, "' AND ",
                                                                                                                    "CUST_CD = '", dr.Item("CUST_CD_L").ToString, "' AND ",
                                                                                                                    "SUB_KB = '21'"))

                    '要望番号:1253 terakawa 2012.07.13 End
                    If 0 < custDetailsDr.Length Then
                        dr.Item("OUTKA_PKG_NB") = custDetailsDr(0).Item("SET_NAIYO").ToString
                    Else
                        dr.Item("OUTKA_PKG_NB") = "999"

                    End If
                End If
                'END YANAI 要望番号857
                'END UMANO メモ(EDI)No.49
                'START YANAI 20120122 立会書印刷対応
                dr.Item("TACHIAI_FLG") = .Cells(rowNo, LMC010G.sprDetailDef.TACHIAI_FLG.ColNo).Value()
                'END YANAI 20120122 立会書印刷対応

                '(2012.03.08) 納品書 再発行フラグの取得 --- STRAT ---
                dr.Item("NHS_FLAG") = .Cells(rowNo, LMC010G.sprDetailDef.NHS_FLAG.ColNo).Value()
                '(2012.03.08) 納品書 再発行フラグの取得 ---  END  ---

                'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                dr.Item("OUTKA_PLAN_DATE") = Jp.Co.Nrs.Com.Utility.DateFormatUtility.DeleteSlash(Convert.ToString(.Cells(rowNo, LMC010G.sprDetailDef.OUTKA_PLAN_DATE.ColNo).Value())) '出荷予定日
                dr.Item("ARR_PLAN_DATE") = Jp.Co.Nrs.Com.Utility.DateFormatUtility.DeleteSlash(Convert.ToString(.Cells(rowNo, LMC010G.sprDetailDef.ARR_PLAN_DATE.ColNo).Value())) '納入予定日
                dr.Item("UNSO_CD") = .Cells(rowNo, LMC010G.sprDetailDef.UNSO_CD.ColNo).Value() '運送会社コード
                dr.Item("UNSO_BR_CD") = .Cells(rowNo, LMC010G.sprDetailDef.UNSO_BR_CD.ColNo).Value() '運送会社支店コード
                dr.Item("DEST_CD") = .Cells(rowNo, LMC010G.sprDetailDef.DEST_CD.ColNo).Value() '届先コード
                'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)

                '要望番号1961 20130322 まとめ送状対応(BPC対応) 追加START
                dr.Item("CUST_DEST_CD") = .Cells(rowNo, LMC010G.sprDetailDef.CUST_DEST_CD.ColNo).Value() '顧客運賃まとめコード
                '要望番号1961 20130322 まとめ送状対応(BPC対応) 追加END

                'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                'まとめ荷札・送状の場合はまとめのキーである、
                '「営業所」、「荷主(大)(中)」、「出荷日」、「納入日」、「運送会社」、「運送会社支店」、「届先」が同じレコードをデータセットに追加しない
                If ("06").Equals(Me._PrintSybetu) = True OrElse
                    ("11").Equals(Me._PrintSybetu) = True Then

                    '要望番号1961 20130322 まとめ送状対応(BPC対応) 修正START
                    custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", dr.Item("NRS_BR_CD").ToString, "' AND ",
                                                                                                "CUST_CD = '", dr.Item("CUST_CD_L").ToString, "' AND ",
                                                                                                "SUB_KB = '57'"))

                    If 0 < custDetailsDr.Length AndAlso custDetailsDr(0).Item("SET_NAIYO").ToString().Equals("01") = True Then

                        matomeDr = dt.Select(String.Concat("NRS_BR_CD = '", dr.Item("NRS_BR_CD").ToString, "' AND ",
                                   "CUST_CD_L = '", dr.Item("CUST_CD_L").ToString, "' AND ",
                                   "CUST_CD_M = '", dr.Item("CUST_CD_M").ToString, "' AND ",
                                   "OUTKA_PLAN_DATE = '", dr.Item("OUTKA_PLAN_DATE").ToString, "' AND ",
                                   "ARR_PLAN_DATE = '", dr.Item("ARR_PLAN_DATE").ToString, "' AND ",
                                   "UNSO_CD = '", dr.Item("UNSO_CD").ToString, "' AND ",
                                   "UNSO_BR_CD = '", dr.Item("UNSO_BR_CD").ToString, "' AND ",
                                   "CUST_DEST_CD = '", dr.Item("CUST_DEST_CD").ToString, "'"))

                        dr.Item("MATOME_DEST_KBN") = custDetailsDr(0).Item("SET_NAIYO").ToString()

                    Else

                        matomeDr = dt.Select(String.Concat("NRS_BR_CD = '", dr.Item("NRS_BR_CD").ToString, "' AND ",
                                   "CUST_CD_L = '", dr.Item("CUST_CD_L").ToString, "' AND ",
                                   "CUST_CD_M = '", dr.Item("CUST_CD_M").ToString, "' AND ",
                                   "OUTKA_PLAN_DATE = '", dr.Item("OUTKA_PLAN_DATE").ToString, "' AND ",
                                   "ARR_PLAN_DATE = '", dr.Item("ARR_PLAN_DATE").ToString, "' AND ",
                                   "UNSO_CD = '", dr.Item("UNSO_CD").ToString, "' AND ",
                                   "UNSO_BR_CD = '", dr.Item("UNSO_BR_CD").ToString, "' AND ",
                                   "DEST_CD = '", dr.Item("DEST_CD").ToString, "'"))

                        dr.Item("MATOME_DEST_KBN") = "00"

                    End If

                    '要望番号1961 20130322 まとめ送状対応(BPC対応) 修正END

                    If matomeDr.Length > 0 Then
                        Continue For
                    End If
                End If
                'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)

                '2012.12.10 yamanaka : 埼玉BP・カストロール対応 Start
                dr.Item("ROW_COUNT") = Me._ChkList.Count
                '2012.12.10 yamanaka : 埼玉BP・カストロール対応 End

                '要望番号1961 20130322 まとめ送状対応(BPC対応) 修正START
                '2013.1.24 kobayashi : 埼玉BP・まとめ送り状対応 Start
                If ("13").Equals(Me._PrintSybetu) = True Then
                    matomeDr = dt.Select(String.Concat("NRS_BR_CD = '", dr.Item("NRS_BR_CD").ToString, "' AND ",
                                                       "CUST_CD_L = '", dr.Item("CUST_CD_L").ToString, "' AND ",
                                                       "CUST_CD_M = '", dr.Item("CUST_CD_M").ToString, "' AND ",
                                                       "OUTKA_PLAN_DATE = '", dr.Item("OUTKA_PLAN_DATE").ToString, "' AND ",
                                                       "ARR_PLAN_DATE = '", dr.Item("ARR_PLAN_DATE").ToString, "' AND ",
                                                       "UNSO_CD = '", dr.Item("UNSO_CD").ToString, "' AND ",
                                                       "UNSO_BR_CD = '", dr.Item("UNSO_BR_CD").ToString, "' AND ",
                                                       "CUST_DEST_CD = '", dr.Item("CUST_DEST_CD").ToString, "'"))
                    '"DEST_CD = '", dr.Item("DEST_CD").ToString, "'"))
                    '要望番号1961 20130322 まとめ送状対応(BPC対応) 修正START

                    If matomeDr.Length > 0 Then
                        dr.Item("MATOME_DENP_GRP") = matomeDr(0).Item("MATOME_DENP_GRP").ToString()
                    Else
                        dr.Item("MATOME_DENP_GRP") = matomeDnpNum.ToString()
                        matomeDnpNum = matomeDnpNum + 1
                    End If

                End If
                ''2013.1.24 kobayashi : 埼玉BP・まとめ送り状対応 End

                'ADD 2017/07/24 名鉄対応 start
                Dim kbnDr() As DataRow = Nothing
                kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C004' AND ",
                                                                                   "KBN_NM1 = '", .Cells(rowNo, LMC010G.sprDetailDef.NRS_BR_CD.ColNo).Value(), "' AND ",
                                                                                   "KBN_NM2 = '", .Cells(rowNo, LMC010G.sprDetailDef.UNSO_CD.ColNo).Value(), "' AND ",
                                                                                   "KBN_NM3 = '", .Cells(rowNo, LMC010G.sprDetailDef.UNSO_BR_CD.ColNo).Value(), "'"))
                If kbnDr.Length = 0 Then
                    dr.Item("MEITETSU_FLG") = "0"
                Else
                    dr.Item("MEITETSU_FLG") = "1"
                End If
                'ADD 2017/07/24 名鉄対応 End

                '倉庫コードを格納（中部物流センター判定用に追加した列）
                dr.Item("WH_CD") = frm.cmbSoko.SelectedValue

                dt.Rows.Add(dr)

            End With

        Next

        Return ds

    End Function


    ''' <summary>
    ''' 印刷時（COA専用：一括引き当て）、利用する更新用INデータセット
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInCOAPrintData(ByVal frm As LMC010F, ByVal ds As DataSet, ByVal rowNo As Integer) As DataSet

        '印刷種別格納
        Dim dt As DataTable = ds.Tables(LMC010C.TABLE_NM_IN_PRINT)
        Dim dr As DataRow = Nothing
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        'Dim rowNo As Integer = 0
        Dim flg As Boolean = True
        Dim custDetailsDr As DataRow() = Nothing
        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        'Dim userNrCd As String = LM.Base.LMUserInfoManager.GetNrsBrCd()
        Dim userNrCd As String = frm.cmbEigyo.SelectedValue.ToString()
        'For i As Integer = 0 To arr.Count - 1

        With spr.ActiveSheet


            dr = dt.NewRow()
            dr.Item("USER_BR_CD") = userNrCd
            dr.Item("NRS_BR_CD") = .Cells(rowNo, LMC010G.sprDetailDef.NRS_BR_CD.ColNo).Value()
            dr.Item("OUTKA_NO_L") = .Cells(rowNo, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo).Value()
            dr.Item("PRINT_KB") = "04" '分析表
            dr.Item("ROW_NO") = rowNo
            dt.Rows.Add(dr)

        End With

        'Next

        Return ds

    End Function

    ''' <summary>
    ''' 進捗区分の設定
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetStageData(ByVal spr As Win.Spread.LMSpread, ByVal rowNo As Integer) As String

        With spr.ActiveSheet
            GetStageData = String.Empty

            'START YANAI 要望番号497
            ''出荷報告の場合
            'If LMC010C.PRINT_HOKOKU.Equals(Me._PrintSybetu) = True Then
            '    Return LMC010C.SINTYOKU90
            'End If
            'END YANAI 要望番号497

            Dim stage As String = Me._LMCconV.GetCellValue(.Cells(rowNo, LMC010G.sprDetailDef.OUTKA_STATE_KB.ColNo))
            Select Case stage

                Case LMC010C.SINTYOKU10, LMC010C.SINTYOKU30
                Case Else

                    Return stage

            End Select

            '小レコードがない場合
            If Convert.ToInt32(Me._LMCconV.GetCellValue(.Cells(rowNo, LMC010G.sprDetailDef.S_COUNT.ColNo))) < 1 Then
                Return LMC010C.SINTYOKU10
            End If

            'まとめ無し
            If "01".Equals(Me._LMCconV.GetCellValue(.Cells(rowNo, LMC010G.sprDetailDef.PICK_KB.ColNo))) = True Then
                Return LMC010C.SINTYOKU30
            End If

            '出庫完了 = 使用
            If "01".Equals(Me._LMCconV.GetCellValue(.Cells(rowNo, LMC010G.sprDetailDef.OUTOKA_KANRYO_YN.ColNo))) = True Then
                Return LMC010C.SINTYOKU30
            End If

            '出荷検品 = 使用
            If "01".Equals(Me._LMCconV.GetCellValue(.Cells(rowNo, LMC010G.sprDetailDef.OUTKA_KENPIN_YN.ColNo))) = True Then
                Return LMC010C.SINTYOKU40
            End If

            Return LMC010C.SINTYOKU50

        End With

    End Function

    ''' <summary>
    ''' 実行時、利用するINデータセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInJikkoData(ByVal frm As LMC010F, ByRef rtDs As DataSet)

        'チェックされた行番号取得
        Dim chkList As ArrayList = Me._V.GetCheckList()
        Dim max As Integer = chkList.Count() - 1
        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        'Dim userNrCd As String = LM.Base.LMUserInfoManager.GetNrsBrCd()
        Dim userNrCd As String = frm.cmbEigyo.SelectedValue.ToString()

        For i As Integer = 0 To max

            With frm.sprDetail.ActiveSheet

                rtDs.Tables(LMC010C.TABLE_NM_IN).Rows.Add(rtDs.Tables(LMC010C.TABLE_NM_IN).NewRow())

                '============= TODO : データセット完成必要 ============='
                rtDs.Tables(LMC010C.TABLE_NM_IN).Rows(i)("NRS_BR_CD") = .Cells(Convert.ToInt32(chkList(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo).Value()
                rtDs.Tables(LMC010C.TABLE_NM_IN).Rows(i)("WH_CD") = .Cells(Convert.ToInt32(chkList(i)), LMC010G.sprDetailDef.WH_CD.ColNo).Value()
                rtDs.Tables(LMC010C.TABLE_NM_IN).Rows(i)("CUST_CD_L") = .Cells(Convert.ToInt32(chkList(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo).Value()
                'rtDs.Tables(LMC010C.TABLE_NM_IN).Rows(i)("CUST_CD_M") = .Cells(Convert.ToInt32(chkList(i)), LMC010G.sprDetailDef.CUST_CD_M.ColNo).Value()
                'rtDs.Tables(LMC010C.TABLE_NM_IN).Rows(i)("GOODS_CD_NRS") = .Cells(Convert.ToInt32(chkList(i)), LMC010G.sprDetailDef.GOODS_CD_NRS.ColNo).Value()
                'rtDs.Tables(LMC010C.TABLE_NM_IN).Rows(i)("SYS_UPD_DATE") = .Cells(Convert.ToInt32(chkList(i)), LMC010G.sprDetailDef.SYS_UPD_DATE.ColNo).Value()
                'rtDs.Tables(LMC010C.TABLE_NM_IN).Rows(i)("SYS_UPD_TIME") = .Cells(Convert.ToInt32(chkList(i)), LMC010G.sprDetailDef.SYS_UPD_TIME.ColNo).Value()

            End With

        Next

    End Sub

    ''' <summary>
    ''' 変更時、利用するデータセット設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData_UPDATE(ByVal frm As LMC010F, ByRef rtDs As DataSet, ByVal arr As ArrayList)

        Dim max As Integer = arr.Count - 1
        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        'Dim userNrCd As String = LM.Base.LMUserInfoManager.GetNrsBrCd()
        Dim userNrCd As String = frm.cmbEigyo.SelectedValue.ToString()
        Dim row As DataRow = rtDs.Tables(LMC010C.TABLE_NM_IN_UPDATE).NewRow
        Dim rowNo As Integer = 0
        Dim rtDt As DataTable = rtDs.Tables(LMC010C.TABLE_NM_IN_UPDATE)
        Dim flg As Boolean = True
        Dim unsoCd As String = frm.txtTrnCD.TextValue
        Dim unsoCdBr As String = frm.txtTrnBrCD.TextValue

        With frm.sprDetail.ActiveSheet

            For i As Integer = 0 To max

                '変換ミスはサーバに渡さない
                flg = Integer.TryParse(arr(i).ToString(), rowNo)
                If flg = False Then
                    Continue For
                End If

                row = rtDt.NewRow()

                row.Item("NRS_BR_CD") = .Cells(rowNo, LMC010G.sprDetailDef.NRS_BR_CD.ColNo).Value()
                row.Item("OUTKA_NO_L") = .Cells(rowNo, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo).Value()
                row.Item("UNSO_NO_L") = .Cells(rowNo, LMC010G.sprDetailDef.UNSO_NO_L.ColNo).Value()
                row.Item("UNSOCO_CD") = unsoCd
                row.Item("UNSOCO_BR_CD") = unsoCdBr
                row.Item("SYS_UPD_DATE") = .Cells(rowNo, LMC010G.sprDetailDef.SYS_UPD_DATE.ColNo).Value()
                row.Item("SYS_UPD_TIME") = .Cells(rowNo, LMC010G.sprDetailDef.SYS_UPD_TIME.ColNo).Value()
                row.Item("UNSO_SYS_UPD_DATE") = .Cells(rowNo, LMC010G.sprDetailDef.UNSO_SYS_UPD_DATE.ColNo).Value()
                row.Item("UNSO_SYS_UPD_TIME") = .Cells(rowNo, LMC010G.sprDetailDef.UNSO_SYS_UPD_TIME.ColNo).Value()
                row.Item("RECORD_NO") = rowNo

                '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
                row.Item("WH_CD") = .Cells(rowNo, LMC010G.sprDetailDef.WH_CD.ColNo).Value()
                '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  

#If True Then ' 西濃自動送り状番号対応 20160704 added inoue
                row.Item("AUTO_DENP_KBN") = .Cells(rowNo, LMC010G.sprDetailDef.AUTO_DENP_KBN.ColNo).Value()
                row.Item("AUTO_DENP_NO") = .Cells(rowNo, LMC010G.sprDetailDef.AUTO_DENP_NO.ColNo).Value()
#End If

                rtDt.Rows.Add(row)

            Next

        End With

    End Sub

    ''' <summary>
    ''' 運送会社一括変更時、利用するデータセット設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData_UPDATE_HENKO(ByVal frm As LMC010F, ByRef rtDs As DataSet, ByVal arr As ArrayList)

        Dim max As Integer = arr.Count - 1
        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        'Dim userNrCd As String = LM.Base.LMUserInfoManager.GetNrsBrCd()
        Dim userNrCd As String = frm.cmbEigyo.SelectedValue.ToString()
        Dim row As DataRow = rtDs.Tables(LMC010C.TABLE_NM_IN_UPDATE).NewRow
        Dim rowNo As Integer = 0
        Dim rtDt As DataTable = rtDs.Tables(LMC010C.TABLE_NM_IN_UPDATE)
        Dim flg As Boolean = True
        Dim unsoCd As String = frm.txtTrnCD.TextValue
        Dim unsoCdBr As String = frm.txtTrnBrCD.TextValue
        Dim tariffSet() As DataRow = Nothing
        Dim tariffKbun As String = String.Empty
        Dim unthinTariffCd As String = String.Empty

        With frm.sprDetail.ActiveSheet

            For i As Integer = 0 To max

                '変換ミスはサーバに渡さない
                flg = Integer.TryParse(arr(i).ToString(), rowNo)
                If flg = False Then
                    Continue For
                End If

                row = rtDt.NewRow()

                row.Item("NRS_BR_CD") = .Cells(rowNo, LMC010G.sprDetailDef.NRS_BR_CD.ColNo).Value()
                row.Item("OUTKA_NO_L") = .Cells(rowNo, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo).Value()
                row.Item("UNSO_NO_L") = .Cells(rowNo, LMC010G.sprDetailDef.UNSO_NO_L.ColNo).Value()
                row.Item("UNSOCO_CD") = unsoCd
                row.Item("UNSOCO_BR_CD") = unsoCdBr
                row.Item("SYS_UPD_DATE") = .Cells(rowNo, LMC010G.sprDetailDef.SYS_UPD_DATE.ColNo).Value()
                row.Item("SYS_UPD_TIME") = .Cells(rowNo, LMC010G.sprDetailDef.SYS_UPD_TIME.ColNo).Value()
                row.Item("UNSO_SYS_UPD_DATE") = .Cells(rowNo, LMC010G.sprDetailDef.UNSO_SYS_UPD_DATE.ColNo).Value()
                row.Item("UNSO_SYS_UPD_TIME") = .Cells(rowNo, LMC010G.sprDetailDef.UNSO_SYS_UPD_TIME.ColNo).Value()
                row.Item("RECORD_NO") = rowNo

                '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
                row.Item("WH_CD") = .Cells(rowNo, LMC010G.sprDetailDef.WH_CD.ColNo).Value()
                '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  

#If True Then ' 西濃自動送り状番号対応 20160704 added inoue
                row.Item("AUTO_DENP_KBN") = .Cells(rowNo, LMC010G.sprDetailDef.AUTO_DENP_KBN.ColNo).Value()
                row.Item("AUTO_DENP_NO") = .Cells(rowNo, LMC010G.sprDetailDef.AUTO_DENP_NO.ColNo).Value()
#End If
                row.Item("UPD_KB") = "0"
                If String.IsNullOrEmpty(.Cells(rowNo, LMC010G.sprDetailDef.UNSOCO_NM.ColNo).Value().ToString) = True Then

                    tariffKbun = String.Empty
                    unthinTariffCd = String.Empty

                    '運賃タリフセットマスタからタリフコードを取得し、設定
                    tariffSet = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_SET).Select(
                                                   String.Concat("NRS_BR_CD = '", .Cells(rowNo, LMC010G.sprDetailDef.NRS_BR_CD.ColNo).Value(), "' AND ",
                                                                 "CUST_CD_L = '", .Cells(rowNo, LMC010G.sprDetailDef.CUST_CD_L.ColNo).Value(), "' AND ",
                                                                 "CUST_CD_M = '", .Cells(rowNo, LMC010G.sprDetailDef.CUST_CD_M.ColNo).Value(), "' AND ",
                                                                 "SET_KB = '00' AND ",
                                                                 "DEST_CD = ''"))

                    If 0 < tariffSet.Length = True Then
                        tariffKbun = tariffSet(0).Item("TARIFF_BUNRUI_KB").ToString()

                        If ("40").Equals(tariffSet(0).Item("TARIFF_BUNRUI_KB").ToString()) = True Then
                            '横持ちの場合は横持ちタリフコードをセット
                            unthinTariffCd = tariffSet(0).Item("YOKO_TARIFF_CD").ToString()
                        ElseIf ("20").Equals(tariffSet(0).Item("TARIFF_BUNRUI_KB").ToString()) = True Then
                            '車扱いの場合は運賃タリフコード２をセット
                            unthinTariffCd = tariffSet(0).Item("UNCHIN_TARIFF_CD2").ToString()
                        Else
                            '横持ち以外の場合は運賃タリフコードをセット
                            unthinTariffCd = tariffSet(0).Item("UNCHIN_TARIFF_CD1").ToString()
                        End If
                    End If

                    '運送会社が未設定の場合、運送（大）を以下の値で更新
                    row.Item("UPD_KB") = "1"
                    row.Item("UNSO_TEHAI_KB") = LMC010C.UNSO_TEHAI_KB01
                    row.Item("TARIFF_BUNRUI_KB") = tariffKbun
                    row.Item("PC_KB") = LMC010C.PC_KB
                    row.Item("TAX_KB") = LMC010C.TAX_KB01
                    row.Item("SEIQ_TARIFF_CD") = unthinTariffCd
                End If

                rtDt.Rows.Add(row)

            Next

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(LMC020引数格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC020InData(ByVal frm As LMC010F, ByVal prmDs As DataSet, ByVal sta As String, Optional ByVal prosess As String = "") As DataSet

        Dim ds As DataSet = New LMC020DS()
        Dim dr As DataRow = ds.Tables(LMControlC.LMC020C_TABLE_NM_IN).NewRow()

        Select Case sta

            Case LMC010C.LMC020_STA_REF    '参照

            Case LMC010C.LMC020_STA_SINKI  '新規

                With prmDs.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
                    dr.Item("OUTKA_NO_L") = ""                               '管理番号
                    dr.Item("NRS_BR_CD") = .Item("NRS_BR_CD")                '営業所コード
                    dr.Item("WH_CD") = .Item("DEFAULT_SOKO_CD")              '倉庫コード
                    dr.Item("CUST_CD_L") = .Item("CUST_CD_L")                '荷主コード（大）
                    dr.Item("CUST_CD_M") = .Item("CUST_CD_M")                '荷主コード（中）
                    dr.Item("CUST_NM") = String.Concat(.Item("CUST_NM_L"), .Item("CUST_NM_M"))                    '荷主名
                    '要望番号:1568 terakawa 2013.01.17 Start
                    dr.Item("OUTKA_PLAN_DATE") = frm.imdShinkiOutkaDate.TextValue  '出荷予定日
                    '要望番号:1568 terakawa 2013.01.17 End
                    '要望番号:1793 terakawa 2013.01.21 Start
                    dr.Item("PICK_KB") = frm.cmbPick.SelectedValue                 'まとめピック区分
                    '要望番号:1793 terakawa 2013.01.21 End
                End With

            Case LMC010C.LMC020_STA_COPY   'ダブルクリック

                With frm.sprDetail.Sheets(0)
                    dr.Item("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(.ActiveRowIndex, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))                     '管理番号
                    dr.Item("NRS_BR_CD") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(.ActiveRowIndex, LMC010G.sprDetailDef.NRS_BR_CD.ColNo))                     '営業所コード
                    dr.Item("WH_CD") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(.ActiveRowIndex, LMC010G.sprDetailDef.WH_CD.ColNo))                             '倉庫コード
                    dr.Item("CUST_CD_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(.ActiveRowIndex, LMC010G.sprDetailDef.CUST_CD_L.ColNo))                     '荷主コード（大）
                    dr.Item("CUST_CD_M") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(.ActiveRowIndex, LMC010G.sprDetailDef.CUST_CD_M.ColNo))                     '荷主コード（中）
                    dr.Item("CUST_NM") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(.ActiveRowIndex, LMC010G.sprDetailDef.CUST_NM.ColNo))                         '荷主名
                    dr.Item("SHIKAKARI_HIN_FLG") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(.ActiveRowIndex, LMC010G.sprDetailDef.SHIKAKARI_HIN_FLG.ColNo))                         '荷主名
                End With

        End Select

        ds.Tables(LMControlC.LMC020C_TABLE_NM_IN).Rows.Add(dr)
        Return ds

    End Function

    ''' <summary>
    ''' データセット設定(LMZ010引数格納)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMZ010InData(ByVal frm As LMC010F) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMZ010DS()
        Dim dr As DataRow = ds.Tables(LMZ010C.TABLE_NM_IN).NewRow()
        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        'dr("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
        dr("DEFAULT_SEARCH_FLG") = LMConst.FLG.OFF
        dr("USER_CD") = LM.Base.LMUserInfoManager.GetUserID()
        ds.Tables(LMZ010C.TABLE_NM_IN).Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' データセット設定(出荷(小))
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">一括引当に使用するデータセット</param>
    ''' <param name="arr">スプレッドシートのチェックボックスに内包されている中レコードの合計を格納したArrayList</param>
    ''' <param name="rowCount">現在ループカウント</param>
    ''' <param name="dtHiki">在庫引当の戻り値のDataTable</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetOutKaS(ByVal frm As LMC010F, ByVal ds As DataSet, ByVal arr As ArrayList, ByVal rowCount As Integer, ByVal dtHiki As DataTable)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_OUTKA_S).NewRow()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_OUTKA_S)
        Dim goodsCdNrs As String = String.Empty
        Dim mGoodsDrs As DataRow() = Nothing
        Dim stdWtKgs As Decimal = 0
        Dim stdIrimeNb As Decimal = 0
        Dim irime As String = String.Empty
        Dim smplFlg As String = String.Empty

        '既にセットしてある項目を参照する為にインスタンスを生成
        Dim outkaMDr As DataRow = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT).Rows(rowCount)
        Dim outkaNoS As Integer = (Convert.ToInt32(Me.FormatNumValue(outkaMDr.Item("OUTKA_NO_S").ToString())) + 1)

        'START YANAI 20110914 一括引当対応
        Dim outSDr() As DataRow = Nothing
        Dim outSmax As Integer = 0
        Dim alctdCanNb As String = String.Empty
        Dim alctdCanQt As String = String.Empty
        'END YANAI 20110914 一括引当対応

        'START YANAI 要望番号853 まとめ処理対応
        Dim zaiMATOMEds As DataSet = Nothing
        Dim outSDr2() As DataRow = Nothing
        Dim insRows As DataRow = Nothing
        Dim zaiMax As Integer = 0
        Dim hikiKosu As Decimal = 0
        Dim hikiSuryo As Decimal = 0
        'END YANAI 要望番号853 まとめ処理対応

        With frm

            '自動引当の返り値分だけ登録条件をセットする
            Dim max As Integer = dtHiki.Rows.Count - 1
            For i As Integer = 0 To max

                '別インスタンスのデータロウを空にする
                inTbl.Clear()

                dr("NRS_BR_CD") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(rowCount)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
                dr("OUTKA_NO_L") = outkaMDr.Item("OUTKA_NO_L").ToString()
                dr("OUTKA_NO_M") = outkaMDr.Item("OUTKA_NO_M").ToString()
                dr("OUTKA_NO_S") = Format(outkaNoS, "000")
                dr("TOU_NO") = dtHiki.Rows(i).Item("TOU_NO").ToString()
                dr("SITU_NO") = dtHiki.Rows(i).Item("SITU_NO").ToString()
                dr("ZONE_CD") = dtHiki.Rows(i).Item("ZONE_CD").ToString()
                dr("LOCA") = dtHiki.Rows(i).Item("LOCA").ToString()
                dr("LOT_NO") = dtHiki.Rows(i).Item("LOT_NO").ToString()
                dr("SERIAL_NO") = dtHiki.Rows(i).Item("SERIAL_NO").ToString()
                dr("OUTKA_TTL_NB") = Convert.ToDecimal(outkaMDr.Item("BACKLOG_NB").ToString())
                dr("OUTKA_TTL_QT") = Convert.ToDecimal(outkaMDr.Item("BACKLOG_QT").ToString())
                dr("ZAI_REC_NO") = dtHiki.Rows(i).Item("ZAI_REC_NO").ToString()
                dr("INKA_NO_L") = dtHiki.Rows(i).Item("INKA_NO_L").ToString()
                dr("INKA_NO_M") = dtHiki.Rows(i).Item("INKA_NO_M").ToString()
                dr("INKA_NO_S") = dtHiki.Rows(i).Item("INKA_NO_S").ToString()
                dr("ZAI_UPD_FLAG") = "00"
                'START YANAI 要望番号853 まとめ処理対応
                dr("IRIME") = dtHiki.Rows(i).Item("IRIME").ToString()
                dr("GOODS_CD_NRS") = dtHiki.Rows(i).Item("GOODS_CD_NRS").ToString()
                dr("GOODS_COND_KB_1") = dtHiki.Rows(i).Item("GOODS_COND_KB_1").ToString()
                dr("GOODS_COND_KB_2") = dtHiki.Rows(i).Item("GOODS_COND_KB_2").ToString()
                dr("REMARK_OUT") = dtHiki.Rows(i).Item("REMARK_OUT").ToString()
                dr("REMARK") = dtHiki.Rows(i).Item("REMARK").ToString()
                dr("INKO_DATE") = dtHiki.Rows(i).Item("INKO_DATE").ToString()
                dr("LT_DATE") = dtHiki.Rows(i).Item("LT_DATE").ToString()
                dr("OFB_KB") = dtHiki.Rows(i).Item("OFB_KB").ToString()
                dr("SPD_KB") = dtHiki.Rows(i).Item("SPD_KB").ToString()
                dr("RSV_NO") = dtHiki.Rows(i).Item("RSV_NO").ToString()
                dr("GOODS_CRT_DATE") = dtHiki.Rows(i).Item("GOODS_CRT_DATE").ToString()
                dr("ALLOC_PRIORITY") = dtHiki.Rows(i).Item("ALLOC_PRIORITY").ToString()
                dr("INKO_PLAN_DATE") = dtHiki.Rows(i).Item("INKO_PLAN_DATE").ToString()
                'END YANAI 要望番号853 まとめ処理対応
                'START YANAI 要望番号780
                dr("INKA_DATE_KANRI_KB") = dtHiki.Rows(i).Item("INKA_DATE_KANRI_KB").ToString()
                'END YANAI 要望番号780

                'START YANAI 20110914 一括引当対応
                'dr("ALCTD_CAN_NB") = dtHiki.Rows(i).Item("ALLOC_CAN_NB").ToString()
                'dr("ALCTD_NB") = dtHiki.Rows(i).Item("HIKI_KOSU").ToString()
                'dr("ALCTD_CAN_QT") = dtHiki.Rows(i).Item("ALLOC_CAN_QT").ToString()
                'dr("ALCTD_QT") = dtHiki.Rows(i).Item("HIKI_SURYO").ToString()
                'START YANAI 要望番号630
                'outSDr = ds.Tables(LMC010C.TABLE_NM_OUTKA_S).Select(String.Concat("ZAI_REC_NO = '", dr("ZAI_REC_NO").ToString(), "' AND ", _
                '                                                                  "ALCTD_CAN_NB_HOZON <> ''"))
                outSDr = ds.Tables(LMC010C.TABLE_NM_OUTKA_S).Select(String.Concat("ZAI_REC_NO = '", dr("ZAI_REC_NO").ToString(), "' AND ",
                                                                                  "ALCTD_CAN_NB_HOZON <> '' AND ",
                                                                                  "ERR_FLG <> '01'"))
                'END YANAI 要望番号630
                outSmax = outSDr.Length - 1
                If -1 < outSmax Then
                    alctdCanNb = outSDr(0).Item("ALCTD_CAN_NB_HOZON").ToString
                    alctdCanQt = outSDr(0).Item("ALCTD_CAN_QT_HOZON").ToString
                    For j As Integer = 0 To outSmax
                        If Convert.ToDecimal(outSDr(j).Item("ALCTD_CAN_NB_HOZON").ToString()) < Convert.ToDecimal(alctdCanNb) Then
                            alctdCanNb = outSDr(j).Item("ALCTD_CAN_NB_HOZON").ToString()
                            alctdCanQt = outSDr(j).Item("ALCTD_CAN_QT_HOZON").ToString()
                        End If
                    Next
                    dr("ALCTD_CAN_NB") = Convert.ToString(Convert.ToDecimal(alctdCanNb) - Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_KOSU").ToString()))
                    dr("ALCTD_CAN_QT") = Convert.ToString(Convert.ToDecimal(alctdCanQt) - Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_SURYO").ToString()))
                Else
                    dr("ALCTD_CAN_NB") = dtHiki.Rows(i).Item("ALLOC_CAN_NB").ToString()
                    dr("ALCTD_CAN_QT") = dtHiki.Rows(i).Item("ALLOC_CAN_QT").ToString()
                End If

                'START YANAI 要望番号853 まとめ処理対応
                ds.Tables(LMC010C.TABLE_NM_IN_MATOME).Clear()
                insRows = ds.Tables(LMC010C.TABLE_NM_IN_MATOME).NewRow
                insRows.Item("NRS_BR_CD") = dr("NRS_BR_CD").ToString
                insRows.Item("GOODS_CD_NRS") = dtHiki.Rows(i).Item("GOODS_CD_NRS").ToString()
                insRows.Item("LOT_NO") = dtHiki.Rows(i).Item("LOT_NO").ToString
                insRows.Item("IRIME") = dtHiki.Rows(i).Item("IRIME").ToString
                insRows.Item("TOU_NO") = dtHiki.Rows(i).Item("TOU_NO").ToString
                insRows.Item("SITU_NO") = dtHiki.Rows(i).Item("SITU_NO").ToString
                insRows.Item("ZONE_CD") = dtHiki.Rows(i).Item("ZONE_CD").ToString
                insRows.Item("LOCA") = dtHiki.Rows(i).Item("LOCA").ToString
                insRows.Item("GOODS_COND_KB_1") = dtHiki.Rows(i).Item("GOODS_COND_KB_1").ToString
                insRows.Item("GOODS_COND_KB_2") = dtHiki.Rows(i).Item("GOODS_COND_KB_2").ToString
                insRows.Item("REMARK_OUT") = dtHiki.Rows(i).Item("REMARK_OUT").ToString
                insRows.Item("REMARK") = dtHiki.Rows(i).Item("REMARK").ToString
                insRows.Item("INKO_DATE") = dtHiki.Rows(i).Item("INKO_DATE").ToString
                insRows.Item("LT_DATE") = dtHiki.Rows(i).Item("LT_DATE").ToString
                insRows.Item("OFB_KB") = dtHiki.Rows(i).Item("OFB_KB").ToString
                insRows.Item("SPD_KB") = dtHiki.Rows(i).Item("SPD_KB").ToString
                insRows.Item("RSV_NO") = dtHiki.Rows(i).Item("RSV_NO").ToString
                insRows.Item("SERIAL_NO") = dtHiki.Rows(i).Item("SERIAL_NO").ToString
                insRows.Item("GOODS_CRT_DATE") = dtHiki.Rows(i).Item("GOODS_CRT_DATE").ToString
                insRows.Item("ALLOC_PRIORITY") = dtHiki.Rows(i).Item("ALLOC_PRIORITY").ToString
                insRows.Item("INKO_PLAN_DATE") = dtHiki.Rows(i).Item("INKO_PLAN_DATE").ToString
                'START YANAI 要望番号780
                insRows.Item("INKA_DATE_KANRI_KB") = dtHiki.Rows(i).Item("INKA_DATE_KANRI_KB").ToString()
                'END YANAI 要望番号780

                'データセットに追加
                ds.Tables(LMC010C.TABLE_NM_IN_MATOME).Rows.Add(insRows)

                Using cloneMatomeInDs As DataSet = ds.Clone

                    cloneMatomeInDs.Tables(LMC010C.TABLE_NM_IN_MATOME).Merge(ds.Tables(LMC010C.TABLE_NM_IN_MATOME))

                    '取得した値から各個数・数量のまとめた値を求める
                    zaiMATOMEds = Me.SelectZaiDataMATOME(frm, cloneMatomeInDs)

                End Using


                zaiMax = zaiMATOMEds.Tables(LMC010C.TABLE_NM_OUT_MATOME).Rows.Count - 1
                If 0 <= zaiMax Then
                    'START YANAI 要望番号780
                    ''まとめ対象の場合
                    'If String.IsNullOrEmpty(insRows.Item("INKO_DATE").ToString) = False Then
                    '    outSDr2 = ds.Tables(LMC010C.TABLE_NM_OUTKA_S).Select(String.Concat("NRS_BR_CD = '", insRows.Item("NRS_BR_CD").ToString, "' AND ", _
                    '                                                                       "GOODS_CD_NRS = '", insRows.Item("GOODS_CD_NRS").ToString, "' AND ", _
                    '                                                                       "LOT_NO = '", insRows.Item("LOT_NO").ToString, "' AND ", _
                    '                                                                       "IRIME = '", insRows.Item("IRIME").ToString, "' AND ", _
                    '                                                                       "TOU_NO = '", insRows.Item("TOU_NO").ToString, "' AND ", _
                    '                                                                       "SITU_NO = '", insRows.Item("SITU_NO").ToString, "' AND ", _
                    '                                                                       "ZONE_CD = '", insRows.Item("ZONE_CD").ToString, "' AND ", _
                    '                                                                       "LOCA = '", insRows.Item("LOCA").ToString, "' AND ", _
                    '                                                                       "GOODS_COND_KB_1 = '", insRows.Item("GOODS_COND_KB_1").ToString, "' AND ", _
                    '                                                                       "GOODS_COND_KB_2 = '", insRows.Item("GOODS_COND_KB_2").ToString, "' AND ", _
                    '                                                                       "REMARK_OUT = '", insRows.Item("REMARK_OUT").ToString, "' AND ", _
                    '                                                                       "REMARK = '", insRows.Item("REMARK").ToString, "' AND ", _
                    '                                                                       "INKO_DATE = '", insRows.Item("INKO_DATE").ToString, "' AND ", _
                    '                                                                       "LT_DATE = '", insRows.Item("LT_DATE").ToString, "' AND ", _
                    '                                                                       "OFB_KB = '", insRows.Item("OFB_KB").ToString, "' AND ", _
                    '                                                                       "SPD_KB = '", insRows.Item("SPD_KB").ToString, "' AND ", _
                    '                                                                       "RSV_NO = '", insRows.Item("RSV_NO").ToString, "' AND ", _
                    '                                                                       "SERIAL_NO = '", insRows.Item("SERIAL_NO").ToString, "' AND ", _
                    '                                                                       "GOODS_CRT_DATE = '", insRows.Item("GOODS_CRT_DATE").ToString, "' AND ", _
                    '                                                                       "ALLOC_PRIORITY = '", insRows.Item("ALLOC_PRIORITY").ToString, "'"))
                    'Else
                    '    outSDr2 = ds.Tables(LMC010C.TABLE_NM_OUTKA_S).Select(String.Concat("NRS_BR_CD = '", insRows.Item("NRS_BR_CD").ToString, "' AND ", _
                    '                                                                       "GOODS_CD_NRS = '", insRows.Item("GOODS_CD_NRS").ToString, "' AND ", _
                    '                                                                       "LOT_NO = '", insRows.Item("LOT_NO").ToString, "' AND ", _
                    '                                                                       "IRIME = '", insRows.Item("IRIME").ToString, "' AND ", _
                    '                                                                       "TOU_NO = '", insRows.Item("TOU_NO").ToString, "' AND ", _
                    '                                                                       "SITU_NO = '", insRows.Item("SITU_NO").ToString, "' AND ", _
                    '                                                                       "ZONE_CD = '", insRows.Item("ZONE_CD").ToString, "' AND ", _
                    '                                                                       "LOCA = '", insRows.Item("LOCA").ToString, "' AND ", _
                    '                                                                       "GOODS_COND_KB_1 = '", insRows.Item("GOODS_COND_KB_1").ToString, "' AND ", _
                    '                                                                       "GOODS_COND_KB_2 = '", insRows.Item("GOODS_COND_KB_2").ToString, "' AND ", _
                    '                                                                       "REMARK_OUT = '", insRows.Item("REMARK_OUT").ToString, "' AND ", _
                    '                                                                       "REMARK = '", insRows.Item("REMARK").ToString, "' AND ", _
                    '                                                                       "INKO_PLAN_DATE = '", insRows.Item("INKO_PLAN_DATE").ToString, "' AND ", _
                    '                                                                       "LT_DATE = '", insRows.Item("LT_DATE").ToString, "' AND ", _
                    '                                                                       "OFB_KB = '", insRows.Item("OFB_KB").ToString, "' AND ", _
                    '                                                                       "SPD_KB = '", insRows.Item("SPD_KB").ToString, "' AND ", _
                    '                                                                       "RSV_NO = '", insRows.Item("RSV_NO").ToString, "' AND ", _
                    '                                                                       "SERIAL_NO = '", insRows.Item("SERIAL_NO").ToString, "' AND ", _
                    '                                                                       "GOODS_CRT_DATE = '", insRows.Item("GOODS_CRT_DATE").ToString, "' AND ", _
                    '                                                                       "ALLOC_PRIORITY = '", insRows.Item("ALLOC_PRIORITY").ToString, "'"))
                    'End If
                    'まとめ対象の場合
                    'upd s.kobayashi NotesNo.1572 "01"-->"1"
                    If ("1").Equals(insRows.Item("INKA_DATE_KANRI_KB").ToString) = True Then
                        outSDr2 = ds.Tables(LMC010C.TABLE_NM_OUTKA_S).Select(String.Concat("NRS_BR_CD = '", insRows.Item("NRS_BR_CD").ToString, "' AND ",
                                                                                           "GOODS_CD_NRS = '", insRows.Item("GOODS_CD_NRS").ToString, "' AND ",
                                                                                           "LOT_NO = '", insRows.Item("LOT_NO").ToString, "' AND ",
                                                                                           "IRIME = '", insRows.Item("IRIME").ToString, "' AND ",
                                                                                           "TOU_NO = '", insRows.Item("TOU_NO").ToString, "' AND ",
                                                                                           "SITU_NO = '", insRows.Item("SITU_NO").ToString, "' AND ",
                                                                                           "ZONE_CD = '", insRows.Item("ZONE_CD").ToString, "' AND ",
                                                                                           "LOCA = '", insRows.Item("LOCA").ToString, "' AND ",
                                                                                           "GOODS_COND_KB_1 = '", insRows.Item("GOODS_COND_KB_1").ToString, "' AND ",
                                                                                           "GOODS_COND_KB_2 = '", insRows.Item("GOODS_COND_KB_2").ToString, "' AND ",
                                                                                           "REMARK_OUT = '", insRows.Item("REMARK_OUT").ToString, "' AND ",
                                                                                           "REMARK = '", insRows.Item("REMARK").ToString, "' AND ",
                                                                                           "LT_DATE = '", insRows.Item("LT_DATE").ToString, "' AND ",
                                                                                           "OFB_KB = '", insRows.Item("OFB_KB").ToString, "' AND ",
                                                                                           "SPD_KB = '", insRows.Item("SPD_KB").ToString, "' AND ",
                                                                                           "RSV_NO = '", insRows.Item("RSV_NO").ToString, "' AND ",
                                                                                           "SERIAL_NO = '", insRows.Item("SERIAL_NO").ToString, "' AND ",
                                                                                           "GOODS_CRT_DATE = '", insRows.Item("GOODS_CRT_DATE").ToString, "' AND ",
                                                                                           "ALLOC_PRIORITY = '", insRows.Item("ALLOC_PRIORITY").ToString, "'"))
                    ElseIf String.IsNullOrEmpty(insRows.Item("INKO_DATE").ToString) = False Then
                        outSDr2 = ds.Tables(LMC010C.TABLE_NM_OUTKA_S).Select(String.Concat("NRS_BR_CD = '", insRows.Item("NRS_BR_CD").ToString, "' AND ",
                                                                                           "GOODS_CD_NRS = '", insRows.Item("GOODS_CD_NRS").ToString, "' AND ",
                                                                                           "LOT_NO = '", insRows.Item("LOT_NO").ToString, "' AND ",
                                                                                           "IRIME = '", insRows.Item("IRIME").ToString, "' AND ",
                                                                                           "TOU_NO = '", insRows.Item("TOU_NO").ToString, "' AND ",
                                                                                           "SITU_NO = '", insRows.Item("SITU_NO").ToString, "' AND ",
                                                                                           "ZONE_CD = '", insRows.Item("ZONE_CD").ToString, "' AND ",
                                                                                           "LOCA = '", insRows.Item("LOCA").ToString, "' AND ",
                                                                                           "GOODS_COND_KB_1 = '", insRows.Item("GOODS_COND_KB_1").ToString, "' AND ",
                                                                                           "GOODS_COND_KB_2 = '", insRows.Item("GOODS_COND_KB_2").ToString, "' AND ",
                                                                                           "REMARK_OUT = '", insRows.Item("REMARK_OUT").ToString, "' AND ",
                                                                                           "REMARK = '", insRows.Item("REMARK").ToString, "' AND ",
                                                                                           "INKO_DATE = '", insRows.Item("INKO_DATE").ToString, "' AND ",
                                                                                           "LT_DATE = '", insRows.Item("LT_DATE").ToString, "' AND ",
                                                                                           "OFB_KB = '", insRows.Item("OFB_KB").ToString, "' AND ",
                                                                                           "SPD_KB = '", insRows.Item("SPD_KB").ToString, "' AND ",
                                                                                           "RSV_NO = '", insRows.Item("RSV_NO").ToString, "' AND ",
                                                                                           "SERIAL_NO = '", insRows.Item("SERIAL_NO").ToString, "' AND ",
                                                                                           "GOODS_CRT_DATE = '", insRows.Item("GOODS_CRT_DATE").ToString, "' AND ",
                                                                                           "ALLOC_PRIORITY = '", insRows.Item("ALLOC_PRIORITY").ToString, "'"))
                    Else
                        outSDr2 = ds.Tables(LMC010C.TABLE_NM_OUTKA_S).Select(String.Concat("NRS_BR_CD = '", insRows.Item("NRS_BR_CD").ToString, "' AND ",
                                                                                           "GOODS_CD_NRS = '", insRows.Item("GOODS_CD_NRS").ToString, "' AND ",
                                                                                           "LOT_NO = '", insRows.Item("LOT_NO").ToString, "' AND ",
                                                                                           "IRIME = '", insRows.Item("IRIME").ToString, "' AND ",
                                                                                           "TOU_NO = '", insRows.Item("TOU_NO").ToString, "' AND ",
                                                                                           "SITU_NO = '", insRows.Item("SITU_NO").ToString, "' AND ",
                                                                                           "ZONE_CD = '", insRows.Item("ZONE_CD").ToString, "' AND ",
                                                                                           "LOCA = '", insRows.Item("LOCA").ToString, "' AND ",
                                                                                           "GOODS_COND_KB_1 = '", insRows.Item("GOODS_COND_KB_1").ToString, "' AND ",
                                                                                           "GOODS_COND_KB_2 = '", insRows.Item("GOODS_COND_KB_2").ToString, "' AND ",
                                                                                           "REMARK_OUT = '", insRows.Item("REMARK_OUT").ToString, "' AND ",
                                                                                           "REMARK = '", insRows.Item("REMARK").ToString, "' AND ",
                                                                                           "INKO_PLAN_DATE = '", insRows.Item("INKO_PLAN_DATE").ToString, "' AND ",
                                                                                           "LT_DATE = '", insRows.Item("LT_DATE").ToString, "' AND ",
                                                                                           "OFB_KB = '", insRows.Item("OFB_KB").ToString, "' AND ",
                                                                                           "SPD_KB = '", insRows.Item("SPD_KB").ToString, "' AND ",
                                                                                           "RSV_NO = '", insRows.Item("RSV_NO").ToString, "' AND ",
                                                                                           "SERIAL_NO = '", insRows.Item("SERIAL_NO").ToString, "' AND ",
                                                                                           "GOODS_CRT_DATE = '", insRows.Item("GOODS_CRT_DATE").ToString, "' AND ",
                                                                                           "ALLOC_PRIORITY = '", insRows.Item("ALLOC_PRIORITY").ToString, "'"))

                    End If
                    'END YANAI 要望番号780

                    zaiMax = outSDr2.Length - 1
                    hikiKosu = 0
                    hikiSuryo = 0
                    For j As Integer = 0 To zaiMax
                        hikiKosu = hikiKosu + Convert.ToDecimal(outSDr2(j).Item("ALCTD_NB").ToString)
                        hikiSuryo = hikiSuryo + Convert.ToDecimal(outSDr2(j).Item("ALCTD_QT").ToString)
                    Next
                    dr("ALCTD_CAN_NB") = Convert.ToDecimal(zaiMATOMEds.Tables(LMC010C.TABLE_NM_OUT_MATOME).Rows(0).Item("ALLOC_CAN_NB").ToString) -
                                         hikiKosu -
                                         Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_KOSU").ToString())
                    dr("ALCTD_CAN_QT") = Convert.ToDecimal(zaiMATOMEds.Tables(LMC010C.TABLE_NM_OUT_MATOME).Rows(0).Item("ALLOC_CAN_QT").ToString) -
                                         hikiSuryo -
                                         Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_SURYO").ToString())
                End If
                'END YANAI 要望番号853 まとめ処理対応

                dr("ALCTD_NB") = dtHiki.Rows(i).Item("HIKI_KOSU").ToString()
                dr("ALCTD_QT") = dtHiki.Rows(i).Item("HIKI_SURYO").ToString()
                dr("ALCTD_CAN_NB_HOZON") = dr("ALCTD_CAN_NB").ToString
                dr("ALCTD_CAN_QT_HOZON") = dr("ALCTD_CAN_QT").ToString
                'END YANAI 20110914 一括引当対応

                irime = outkaMDr.Item("IRIME").ToString()

                dr("IRIME") = irime

                'START YANAI 要望番号341
                'goodsCdNrs = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(rowCount)), LMC010G.sprDetailDef.GOODS_CD_NRS.ColNo))

                'mGoodsDrs = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(String.Concat("GOODS_CD_NRS = ", " '", goodsCdNrs, "' "))

                'If 0 < mGoodsDrs.Length Then

                '    '標準重量を取得
                '    stdWtKgs = Convert.ToDecimal(mGoodsDrs(0).Item("STD_WT_KGS").ToString())

                '    '標準入目
                '    stdIrimeNb = Convert.ToDecimal(mGoodsDrs(0).Item("STD_IRIME_NB").ToString())

                'End If
                '標準重量を取得
                stdWtKgs = Convert.ToDecimal(dtHiki.Rows(i).Item("STD_WT_KGS").ToString())

                '標準入目
                stdIrimeNb = Convert.ToDecimal(dtHiki.Rows(i).Item("STD_IRIME_NB").ToString())
                'END YANAI 要望番号341

                dr("BETU_WT") = Convert.ToDecimal(irime) * stdWtKgs / stdIrimeNb
                dr("COA_FLAG") = "00"

                'START YANAI 要望番号1426 日立物流一括引当で備考小(社内)が取得できない
                'dr("REMARK") = String.Empty
                'END YANAI 要望番号1426 日立物流一括引当で備考小(社内)が取得できない

                If "03".Equals(outkaMDr.Item("ALCTD_KB").ToString()) = True Then
                    smplFlg = "01"
                Else
                    smplFlg = "00"
                End If
                dr("SMPL_FLAG") = smplFlg


                inTbl.Rows.Add(dr)

                '持ちまわっているデータセットにつめなおす
                ds.Tables(LMC010C.TABLE_NM_OUTKA_S).ImportRow(inTbl.Rows(0))

                outkaNoS += 1
            Next

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(出荷(中))
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">一括引当に使用するデータセット</param>
    ''' <param name="arr">スプレッドシートのチェックボックスに内包されている中レコードの合計を格納したArrayList</param>
    ''' <param name="rowCount">現在ループカウント</param>
    ''' <param name="dtHiki">在庫引当の戻り値のDataTable</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetOutKaM(ByVal frm As LMC010F, ByVal ds As DataSet, ByVal arr As ArrayList, ByVal rowCount As Integer, ByVal dtHiki As DataTable)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_OUTKA_M)
        Dim dr As DataRow = inTbl.NewRow()

        '既にセットしてある項目を参照する為にインスタンスを生成
        Dim outkaMDr As DataRow = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT).Rows(rowCount)

        Dim alctdNb As Decimal = 0
        Dim alctdQt As Decimal = 0
        Dim max As Integer = dtHiki.Rows.Count - 1
        'START YANAI 20110914 一括引当対応
        Dim amari As Decimal = 0
        'END YANAI 20110914 一括引当対応

        With frm

            '自動引当の返り値分だけ登録条件をセットする
            For i As Integer = 0 To max

                alctdNb += Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_KOSU").ToString())
                alctdQt += Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_SURYO").ToString())

            Next

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            'START YANAI 要望番号585
            dr("STD_IRIME_NB") = dtHiki.Rows(0).Item("STD_IRIME_NB").ToString()
            dr("STD_WT_KGS") = dtHiki.Rows(0).Item("STD_WT_KGS").ToString()
            dr("IRIME") = dtHiki.Rows(0).Item("IRIME").ToString()
            dr("TARE_YN") = dtHiki.Rows(0).Item("TARE_YN").ToString()
            dr("PKG_UT") = dtHiki.Rows(0).Item("PKG_UT").ToString()
            'END YANAI 要望番号585

            dr("NRS_BR_CD") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(rowCount)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
            dr("OUTKA_NO_L") = outkaMDr.Item("OUTKA_NO_L").ToString()
            dr("OUTKA_NO_M") = outkaMDr.Item("OUTKA_NO_M").ToString()
            'START YANAI 20110914 一括引当対応
            'dr("ALCTD_NB") = alctdNb
            'dr("ALCTD_QT") = alctdQt
            dr("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(outkaMDr.Item("ALCTD_NB").ToString()) + alctdNb)
            dr("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(outkaMDr.Item("ALCTD_QT").ToString()) + alctdQt)
            'END YANAI 20110914 一括引当対応
            dr("BACKLOG_NB") = Convert.ToDecimal(outkaMDr.Item("BACKLOG_NB").ToString()) - alctdNb
            dr("BACKLOG_QT") = Convert.ToDecimal(outkaMDr.Item("BACKLOG_QT").ToString()) - alctdQt
            'START YANAI 20110914 一括引当対応
            dr("OUTKA_M_PKG_NB") = Math.Floor(Convert.ToDecimal(outkaMDr.Item("OUTKA_TTL_NB").ToString()) / Convert.ToDecimal(outkaMDr.Item("PKG_NB").ToString()))
            amari = Convert.ToDecimal(outkaMDr.Item("OUTKA_TTL_NB").ToString()) Mod Convert.ToDecimal(outkaMDr.Item("PKG_NB").ToString())
            If 0 < amari Then
                dr("OUTKA_M_PKG_NB") = Convert.ToString(Convert.ToDecimal(dr("OUTKA_M_PKG_NB").ToString()) + 1)
            End If
            'END YANAI 20110914 一括引当対応

            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            ds.Tables(LMC010C.TABLE_NM_OUTKA_M).ImportRow(inTbl.Rows(0))

        End With

    End Sub

    'START YANAI 要望番号773　報告書Excel対応
    ''' <summary>
    ''' データセット設定(出荷報告・EXCEL)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">データセット</param>
    ''' <param name="arr">スプレッドシートのArrayList</param>
    ''' <remarks></remarks>
    Private Sub SetInHoukokuExcel(ByVal frm As LMC010F, ByVal ds As DataSet, ByVal arr As ArrayList)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_IN_HOUKOKU_EXCEL).NewRow()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_IN_HOUKOKU_EXCEL)

        With frm

            '自動引当の返り値分だけ登録条件をセットする
            Dim max As Integer = arr.Count() - 1
            For i As Integer = 0 To max

                '別インスタンスのデータロウを空にする
                inTbl.Clear()

                dr("NRS_BR_CD") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
                dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))

                inTbl.Rows.Add(dr)

                '持ちまわっているデータセットにつめなおす
                ds.Tables(LMC010C.TABLE_NM_IN_HOUKOKU_EXCEL).ImportRow(inTbl.Rows(0))

            Next

        End With

    End Sub
    'END YANAI 要望番号773　報告書Excel対応

    ''' <summary>
    ''' データセット設定(在庫)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">一括引当に使用するデータセット</param>
    ''' <param name="arr">スプレッドシートのチェックボックスに内包されている中レコードの合計を格納したArrayList</param>
    ''' <param name="rowCount">現在ループカウント</param>
    ''' <param name="dtHiki">在庫引当の戻り値のDataTable</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetZaiTrs(ByVal frm As LMC010F, ByVal ds As DataSet, ByVal arr As ArrayList, ByVal rowCount As Integer, ByVal dtHiki As DataTable)

        '別インスタンスのデータセットを宣言(コピーして)
        'START YANAI 20110914 一括引当対応
        'Dim setDs As DataSet = ds.Copy()
        'Dim dr As DataRow = setDs.Tables(LMC010C.TABLE_NM_ZAI_TRS).NewRow()
        'Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_ZAI_TRS)
        Dim dr As DataRow = Nothing
        'END YANAI 20110914 一括引当対応

        '既にセットしてある項目を参照する為にインスタンスを生成
        Dim outkaMDr As DataRow = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT).Rows(rowCount)

        Dim alctdKb As String = outkaMDr.Item("ALCTD_KB").ToString()
        Dim alctdNb As Decimal = 0
        Dim allocCanNb As Decimal = 0
        Dim alctdQt As Decimal = 0
        Dim allocCanQt As Decimal = 0

        With frm

            '自動引当の返り値分だけ登録条件をセットする
            Dim max As Integer = dtHiki.Rows.Count - 1
            For i As Integer = 0 To max

                '別インスタンスのデータロウを空にする
                'START YANAI 20110914 一括引当対応
                'inTbl.Clear()
                dr = ds.Tables(LMC010C.TABLE_NM_ZAI_TRS).NewRow()
                'END YANAI 20110914 一括引当対応

                dr("NRS_BR_CD") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(rowCount)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
                dr("ZAI_REC_NO") = dtHiki.Rows(i).Item("ZAI_REC_NO").ToString()

                If "01".Equals(alctdKb) = True Then
                    '個数の場合
                    alctdNb = Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_KOSU").ToString())
                    allocCanNb = Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_KOSU").ToString())
                    alctdQt = Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_SURYO").ToString())
                    allocCanQt = Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_SURYO").ToString())

                ElseIf "02".Equals(alctdKb) = True Then
                    '数量の場合
                    alctdNb = Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_KOSU").ToString())
                    allocCanNb = Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_KOSU").ToString())
                    alctdQt = Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_SURYO").ToString())
                    allocCanQt = Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_SURYO").ToString())

                ElseIf "03".Equals(alctdKb) = True Then
                    '小分けの場合
                    alctdNb = 1
                    allocCanNb = 1
                    alctdQt = Convert.ToDecimal(dtHiki.Rows(i).Item("IRIME").ToString())
                    allocCanQt = Convert.ToDecimal(dtHiki.Rows(i).Item("IRIME").ToString())

                Else
                    'サンプルの場合
                    alctdNb = 0
                    allocCanNb = 0
                    alctdQt = 0
                    allocCanQt = 0

                End If

                dr("ALCTD_NB") = alctdNb
                dr("ALLOC_CAN_NB") = allocCanNb
                dr("ALCTD_QT") = alctdQt
                dr("ALLOC_CAN_QT") = allocCanQt

                dr("ALCTD_KB") = alctdKb

                dr("SYS_UPD_DATE") = dtHiki.Rows(i).Item("SYS_UPD_DATE").ToString()
                dr("SYS_UPD_TIME") = dtHiki.Rows(i).Item("SYS_UPD_TIME").ToString()
                'START YANAI 20110914 一括引当対応
                dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(rowCount)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
                'END YANAI 20110914 一括引当対応

                'START YANAI 20111122 一括引当バグ
                dr("ERR_FLG") = "00"
                'END YANAI 20111122 一括引当バグ

                'START YANAI 20110914 一括引当対応
                'inTbl.Rows.Add(dr)
                'END YANAI 20110914 一括引当対応

                '持ちまわっているデータセットにつめなおす
                'START YANAI 20110914 一括引当対応
                'ds.Tables(LMC010C.TABLE_NM_ZAI_TRS).ImportRow(inTbl.Rows(0))
                ds.Tables(LMC010C.TABLE_NM_ZAI_TRS).Rows.Add(dr)
                'END YANAI 20110914 一括引当対応

            Next

        End With

    End Sub

    'START YANAI メモ②No.15,16,17
    ''' <summary>
    ''' データセット設定(在庫)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">一括引当に使用するデータセット</param>
    ''' <param name="arr">スプレッドシートのチェックボックスに内包されている中レコードの合計を格納したArrayList</param>
    ''' <param name="rowCount">現在ループカウント</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetOutMIn(ByVal frm As LMC010F, ByVal ds As DataSet, ByVal arr As ArrayList, ByVal rowCount As Integer)

        Dim dr As DataRow() = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).Select(String.Concat("OUTKA_NO_L = '", Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(rowCount)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)), "'"))
        If 0 < dr.Length Then
            dr(0).Item("ERR_FLG") = "01"
        End If

        '引当エラー時は処理中止 要望番号1523
        If _HikiAXALTA = True Then
            Dim dt As DataTable = ds.Tables("LMC010_OUTKA_M_IN")
            Dim maxOutkaM As Integer = dt.Rows.Count - 1
            Dim j As Integer = 0
            For i As Integer = 0 To maxOutkaM
                If dt.Rows(i).Item("OUTKA_NO_L").ToString() = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(rowCount)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)) = True Then
                    j = i
                End If
                If i > j Then
                    dt.Rows(i).Item("ERR_FLG") = "01"
                End If
            Next
        End If

        'START YANAI 20111122 一括引当バグ
        Dim zaidr() As DataRow = Nothing
        zaidr = ds.Tables(LMC010C.TABLE_NM_ZAI_TRS).Select(String.Concat("OUTKA_NO_L = '", Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(rowCount)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)), "'"))
        Dim max As Integer = zaidr.Length - 1
        For i As Integer = 0 To max
            zaidr(i).Item("ERR_FLG") = "01"
        Next
        'START YANAI 要望番号630
        Dim outSdr() As DataRow = Nothing
        outSdr = ds.Tables(LMC010C.TABLE_NM_OUTKA_S).Select(String.Concat("OUTKA_NO_L = '", Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(rowCount)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)), "'"))
        max = outSdr.Length - 1
        For i As Integer = 0 To max
            outSdr(i).Item("ERR_FLG") = "01"
        Next
        'END YANAI 要望番号630

        'END YANAI 20111122 一括引当バグ

    End Sub
    'END YANAI メモ②No.15,16,17

    'START YANAI 要望番号585
    ''' <summary>
    ''' 出荷(大)の進捗区分、出荷梱包個数の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetUnsoL(ByVal frm As LMC010F, ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = Nothing
        Dim outkaMDr() As DataRow = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT).Select(Nothing, "OUTKA_NO_L ASC")
        Dim max As Integer = outkaMDr.Length - 1

        Dim outMDt As DataTable = ds.Tables(LMC010C.TABLE_NM_OUTKA_M)
        Dim outMDr() As DataRow = Nothing
        Dim outMax As Integer = 0

        Dim outSDt As DataTable = ds.Tables(LMC010C.TABLE_NM_OUTKA_S)
        Dim outSDr() As DataRow = Nothing

        Dim outkaNoL As String = String.Empty

        Dim wt As Decimal = 0

        Dim drArrOutkaMIn() As DataRow

        'START YANAI 要望番号638
        Dim outLinDt As DataTable = ds.Tables(LMC010C.TABLE_NM_OUTKA_L_IN)
        Dim outLinDr() As DataRow = Nothing
        'END YANAI 要望番号638

        ds.Tables(LMC010C.TABLE_NM_JURYO_UNSO_L).Rows.Clear()
        ds.Tables(LMC010C.TABLE_NM_JURYO_OUTKA_M).Rows.Clear()
        ds.Tables(LMC010C.TABLE_NM_JURYO_OUTKA_S).Rows.Clear()

        For i As Integer = 0 To max
            If (outkaNoL).Equals(outkaMDr(i).Item("OUTKA_NO_L").ToString()) = False Then
                '出荷管理番号(大)が変わった時だけ処理
                outkaNoL = outkaMDr(i).Item("OUTKA_NO_L").ToString()
                ds.Tables(LMC010C.TABLE_NM_JURYO_UNSO_L).Rows.Clear()
                ds.Tables(LMC010C.TABLE_NM_JURYO_OUTKA_M).Rows.Clear()
                ds.Tables(LMC010C.TABLE_NM_JURYO_OUTKA_S).Rows.Clear()
                dr = Nothing

                outMDr = outMDt.Select(String.Concat("OUTKA_NO_L = '", outkaNoL, "'"))
                max = outMDr.Length - 1

                If max < 0 Then
                    drArrOutkaMIn = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).Select(String.Concat("OUTKA_NO_L = '", outkaNoL, "'"))
                    If drArrOutkaMIn.Length() > 0 AndAlso drArrOutkaMIn(0).Item("IS_FFEM_MATERIAL_PLANT_TRANSFER").ToString() = LMConst.FLG.ON Then
                        ' FFEM原料プラント間転送の場合
                        dr = Nothing
                        dr = ds.Tables(LMC010C.TABLE_NM_UNCHIN).NewRow()
                        dr.Item("NRS_BR_CD") = drArrOutkaMIn(0).Item("NRS_BR_CD").ToString()
                        dr.Item("UNSO_NO_L") = outkaMDr(i).Item("UNSO_NO_L").ToString()
                        dr.Item("SYS_UPD_DATE") = outkaMDr(i).Item("UNCHIN_SYS_UPD_DATE").ToString()
                        dr.Item("SYS_UPD_TIME") = outkaMDr(i).Item("UNCHIN_SYS_UPD_TIME").ToString()
                        dr.Item("OUTKA_NO_L") = outkaMDr(i).Item("OUTKA_NO_L").ToString()
                        dr.Item("WH_CD") = outkaMDr(i).Item("WH_CD").ToString()
                        dr.Item("UNSO_TEHAI_KB") = outkaMDr(i).Item("UNSO_TEHAI_KB").ToString()
                        ds.Tables(LMC010C.TABLE_NM_UNCHIN).Rows.Add(dr)
                    End If
                    Continue For
                End If

                '運送(大)…運送重量求める用
                dr = ds.Tables(LMC010C.TABLE_NM_JURYO_UNSO_L).NewRow()
                dr.Item("TARE_YN") = outkaMDr(i).Item("TARE_YN").ToString()
                ds.Tables(LMC010C.TABLE_NM_JURYO_UNSO_L).Rows.Add(dr)

                '出荷(中)…運送重量求める用
                dr = Nothing
                For j As Integer = 0 To max
                    dr = ds.Tables(LMC010C.TABLE_NM_JURYO_OUTKA_M).NewRow()
                    dr.Item("OUTKA_NO_M") = outMDr(j).Item("OUTKA_NO_M").ToString()
                    dr.Item("TARE_YN") = outMDr(j).Item("TARE_YN").ToString()
                    dr.Item("PKG_UT") = outMDr(j).Item("PKG_UT").ToString()
                    dr.Item("STD_IRIME_NB") = outMDr(j).Item("STD_IRIME_NB").ToString()
                    dr.Item("STD_WT_KGS") = outMDr(j).Item("STD_WT_KGS").ToString()
                    dr.Item("SYS_DEL_FLG") = "0"
                    '(2012.12.26)要望番号1692対応として追加しましたが・・・ -- START --
                    dr.Item("NRS_BR_CD") = ds.Tables(LMC010C.TABLE_NM_OUTKA_M).Rows(0).Item("NRS_BR_CD").ToString
                    dr.Item("GOODS_CD_NRS") = outkaMDr(j).Item("GOODS_CD_NRS").ToString()
                    '(2012.12.26)要望番号1692対応として追加しましたが・・・ --  END  --
                    ds.Tables(LMC010C.TABLE_NM_JURYO_OUTKA_M).Rows.Add(dr)
                Next

                '出荷(小)…運送重量求める用
                dr = Nothing
                For j As Integer = 0 To max
                    dr = ds.Tables(LMC010C.TABLE_NM_JURYO_OUTKA_S).NewRow()
                    dr.Item("OUTKA_NO_M") = outMDr(j).Item("OUTKA_NO_M").ToString()
                    dr.Item("IRIME") = outMDr(j).Item("IRIME").ToString()
                    dr.Item("ALCTD_NB") = outMDr(j).Item("ALCTD_NB").ToString()
                    dr.Item("SYS_DEL_FLG") = "0"
                    '(2012.12.08) 小分け時の運送重量対応 --- START ---
                    dr.Item("ALCTD_QT") = outMDr(j).Item("ALCTD_QT").ToString()
                    '(2012.12.08) 小分け時の運送重量対応 ---  END  ---
                    ds.Tables(LMC010C.TABLE_NM_JURYO_OUTKA_S).Rows.Add(dr)
                Next

                '運送…更新用
                wt = Me.SetWt(frm, ds)

                dr = Nothing
                dr = ds.Tables(LMC010C.TABLE_NM_UNSO_L).NewRow()
                dr.Item("NRS_BR_CD") = ds.Tables(LMC010C.TABLE_NM_OUTKA_M).Rows(0).Item("NRS_BR_CD").ToString
                dr.Item("UNSO_NO_L") = outkaMDr(i).Item("UNSO_NO_L").ToString()
                dr.Item("UNSO_WT") = wt
                dr.Item("SYS_UPD_DATE") = outkaMDr(i).Item("UNSOL_SYS_UPD_DATE").ToString()
                dr.Item("SYS_UPD_TIME") = outkaMDr(i).Item("UNSOL_SYS_UPD_TIME").ToString()
                dr.Item("OUTKA_NO_L") = outkaMDr(i).Item("OUTKA_NO_L").ToString()
                'START YANAI 要望番号638
                outLinDr = outLinDt.Select(String.Concat("OUTKA_NO_L = '", outkaNoL, "'"))
                dr.Item("OUTKA_PKG_NB") = outLinDr(0).Item("OUTKA_PKG_NB").ToString()
                'END YANAI 要望番号638

#If True Then ' 西濃自動送り状番号対応 20160701 added inoue
                dr.Item("AUTO_DENP_KBN") = outkaMDr(i).Item("AUTO_DENP_KBN").ToString()
                dr.Item("AUTO_DENP_NO") = outkaMDr(i).Item("AUTO_DENP_NO").ToString()
#End If

                ds.Tables(LMC010C.TABLE_NM_UNSO_L).Rows.Add(dr)




                'START YANAI 要望番号638
                dr = Nothing
                dr = ds.Tables(LMC010C.TABLE_NM_UNCHIN).NewRow()
                dr.Item("NRS_BR_CD") = ds.Tables(LMC010C.TABLE_NM_OUTKA_M).Rows(0).Item("NRS_BR_CD").ToString
                dr.Item("UNSO_NO_L") = outkaMDr(i).Item("UNSO_NO_L").ToString()
                dr.Item("SYS_UPD_DATE") = outkaMDr(i).Item("UNCHIN_SYS_UPD_DATE").ToString()
                dr.Item("SYS_UPD_TIME") = outkaMDr(i).Item("UNCHIN_SYS_UPD_TIME").ToString()
                dr.Item("OUTKA_NO_L") = outkaMDr(i).Item("OUTKA_NO_L").ToString()
                dr.Item("WH_CD") = outkaMDr(i).Item("WH_CD").ToString()
                dr.Item("UNSO_TEHAI_KB") = outkaMDr(i).Item("UNSO_TEHAI_KB").ToString()
                ds.Tables(LMC010C.TABLE_NM_UNCHIN).Rows.Add(dr)
                'END YANAI 要望番号638

                'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
                dr = Nothing
                dr = ds.Tables(LMC010C.TABLE_NM_SHIHARAI).NewRow()
                dr.Item("NRS_BR_CD") = ds.Tables(LMC010C.TABLE_NM_OUTKA_M).Rows(0).Item("NRS_BR_CD").ToString
                dr.Item("UNSO_NO_L") = outkaMDr(i).Item("UNSO_NO_L").ToString()
                dr.Item("OUTKA_NO_L") = outkaMDr(i).Item("OUTKA_NO_L").ToString()
                dr.Item("WH_CD") = outkaMDr(i).Item("WH_CD").ToString()
                dr.Item("UNSO_TEHAI_KB") = outkaMDr(i).Item("UNSO_TEHAI_KB").ToString()
                ds.Tables(LMC010C.TABLE_NM_SHIHARAI).Rows.Add(dr)
                'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等


            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 運送重量を求める
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetWt(ByVal frm As LMC010F, ByVal ds As DataSet) As Decimal

        With frm

            '運送サブの計算処理を呼び出す
            Dim unsoH As LMFControlH = New LMFControlH(frm, "LMC010")

            Dim wt As Decimal = unsoH.GetJuryoOutkaData(ds _
                                                        , LMC010C.TABLE_NM_JURYO_OUTKA_M _
                                                        , LMC010C.TABLE_NM_JURYO_UNSO_L _
                                                        , LMC010C.TABLE_NM_JURYO_OUTKA_S _
                                                        , "TARE_YN" _
                                                        , "PKG_UT" _
                                                        , "PKG_NB" _
                                                        , "STD_IRIME_NB" _
                                                        , "STD_WT_KGS"
                                                        )

            '切り上げ
            wt = Math.Ceiling(wt)

            Return wt

        End With

    End Function
    'END YANAI 要望番号585


    ''' <summary>
    ''' 印刷時、利用する更新用INデータセット(名鉄用)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInMeitetuPrintData(ByVal frm As LMC010F, ByRef rtDs As DataSet, ByVal arr As ArrayList)

        Dim userNrCd As String = frm.cmbEigyo.SelectedValue.ToString()
        Dim rtDt As DataTable = rtDs.Tables(LMC010C.TABLE_NM_IN_PRINT)
        Dim row As DataRow = Nothing
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        Dim rowNo As Integer = 0
        Dim flg As Boolean = True
        For i As Integer = 0 To arr.Count - 1

            With spr.ActiveSheet

                '変換ミスはサーバに渡さない
                flg = Integer.TryParse(arr(i).ToString(), rowNo)
                If flg = False Then
                    Continue For
                End If

                row = rtDt.NewRow()

                row.Item("USER_BR_CD") = userNrCd
                row.Item("NRS_BR_CD") = .Cells(rowNo, LMC010G.sprDetailDef.NRS_BR_CD.ColNo).Value()
                row.Item("OUTKA_NO_L") = .Cells(rowNo, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo).Value()
                row.Item("OUTKA_STATE_KB") = ""
                row.Item("PICK_KB") = .Cells(rowNo, LMC010G.sprDetailDef.PICK_KB.ColNo).Value()
                row.Item("OUTOKA_KANRYO_YN") = .Cells(rowNo, LMC010G.sprDetailDef.OUTOKA_KANRYO_YN.ColNo).Value()
                row.Item("OUTKA_KENPIN_YN") = .Cells(rowNo, LMC010G.sprDetailDef.OUTKA_KENPIN_YN.ColNo).Value()
                row.Item("SYS_UPD_DATE") = .Cells(rowNo, LMC010G.sprDetailDef.SYS_UPD_DATE.ColNo).Value()
                row.Item("SYS_UPD_TIME") = .Cells(rowNo, LMC010G.sprDetailDef.SYS_UPD_TIME.ColNo).Value()
                row.Item("OUTKA_SASHIZU_PRT_YN") = .Cells(rowNo, LMC010G.sprDetailDef.OUTKA_SASHIZU_PRT_YN.ColNo).Value()
                row.Item("S_COUNT") = .Cells(rowNo, LMC010G.sprDetailDef.S_COUNT.ColNo).Value()
                row.Item("PRINT_KB") = ""
                row.Item("ROW_NO") = rowNo
                row.Item("CUST_CD_L") = .Cells(rowNo, LMC010G.sprDetailDef.CUST_CD_L.ColNo).Value()
                row.Item("CUST_CD_M") = .Cells(rowNo, LMC010G.sprDetailDef.CUST_CD_M.ColNo).Value()
                row.Item("NIHUDA_YN") = .Cells(rowNo, LMC010G.sprDetailDef.NIHUDA_YN.ColNo).Value()
                row.Item("SASZ_USER") = .Cells(rowNo, LMC010G.sprDetailDef.SASZ_USER.ColNo).Value()
                row.Item("OUTKA_PKG_NB") = .Cells(rowNo, LMC010G.sprDetailDef.OUTKA_PKG_NB.ColNo).Value()
                row.Item("TACHIAI_FLG") = .Cells(rowNo, LMC010G.sprDetailDef.TACHIAI_FLG.ColNo).Value()
                row.Item("NHS_FLAG") = .Cells(rowNo, LMC010G.sprDetailDef.NHS_FLAG.ColNo).Value()
                row.Item("OUTKA_PLAN_DATE") = Jp.Co.Nrs.Com.Utility.DateFormatUtility.DeleteSlash(Convert.ToString(.Cells(rowNo, LMC010G.sprDetailDef.OUTKA_PLAN_DATE.ColNo).Value())) '出荷予定日
                row.Item("ARR_PLAN_DATE") = Jp.Co.Nrs.Com.Utility.DateFormatUtility.DeleteSlash(Convert.ToString(.Cells(rowNo, LMC010G.sprDetailDef.ARR_PLAN_DATE.ColNo).Value())) '納入予定日
                row.Item("UNSO_CD") = .Cells(rowNo, LMC010G.sprDetailDef.UNSO_CD.ColNo).Value()
                row.Item("UNSO_BR_CD") = .Cells(rowNo, LMC010G.sprDetailDef.UNSO_BR_CD.ColNo).Value()
                row.Item("DEST_CD") = .Cells(rowNo, LMC010G.sprDetailDef.DEST_CD.ColNo).Value()
                row.Item("CUST_DEST_CD") = .Cells(rowNo, LMC010G.sprDetailDef.CUST_DEST_CD.ColNo).Value()
                row.Item("MEITETU_PRINT_FLG") = frm.cmbTrapoPrint.SelectedValue          'add 2017/07/18 名鉄バラ荷札・送状個別印刷で使用
                row.Item("ROW_COUNT") = arr.Count

                rtDt.Rows.Add(row)

            End With

        Next


    End Sub




#End Region 'DataSet設定

    '要望番号:1533 terakawa 2012.10.30 Start
#Region "チェック"

#Region "出荷(大)排他チェック"
    Private Function OutkaLExistChk(ByVal frm As LMC010F, ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet = ds.Copy
        '排他チェック用データセットに移し替え
        setDs.Tables(LMC010C.TABLE_NM_HAITA).Merge(setDs.Tables(LMC010C.TABLE_NM_OUTKA_M_IN))

        MyBase.Logger.StartLog(MyBase.GetType.Name, "OutkaLExistChk")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC010BLF", "OutkaLExistChk", setDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "OutkaLExistChk")

        '排他チェック結果をINデータに反映
        ds.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).Clear()
        ds.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).Merge(rtnDs.Tables(LMC010C.TABLE_NM_HAITA))

        '除外されたデータのチェックボックスを外す
        Dim max As Integer = setDs.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).Rows.Count - 1
        Dim haitaMax As Integer = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).Rows.Count - 1
        Dim haitaOutkaNoL As String = String.Empty
        Dim OutkaNoL As String = String.Empty
        Dim updateFlg As Boolean = False

        For i As Integer = 0 To max
            '更新フラグをリセット
            updateFlg = False

            OutkaNoL = setDs.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).Rows(i).Item("OUTKA_NO_L").ToString()

            For j As Integer = 0 To haitaMax
                haitaOutkaNoL = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).Rows(j).Item("OUTKA_NO_L").ToString()

                If OutkaNoL.Equals(haitaOutkaNoL) Then
                    updateFlg = True
                    Exit For
                End If
            Next

            '更新フラグが立っていなかった場合、チェックボックスを外す
            If updateFlg = False Then
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                frm.sprDetail.SetCellValue(Convert.ToInt32(setDs.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).Rows(i).Item("ROW_NO").ToString), LMC010C.SprColumnIndex.DEF, LMConst.FLG.OFF)
#Else
                frm.sprDetail.SetCellValue(Convert.ToInt32(setDs.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).Rows(i).Item("ROW_NO").ToString), LMC010G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
#End If
            End If
        Next

        Return ds

    End Function
#End Region
    '要望番号:1533 terakawa 2012.10.30 End

#Region "一括変更"

    ''' <summary>
    ''' 一括変更時(行単位)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>リスト</returns>
    ''' <remarks></remarks>
    Private Function IsHenkoChk(ByVal frm As LMC010F, ByVal arr As ArrayList) As ArrayList

        Dim max As Integer = arr.Count - 1
        Dim rowNo As Integer = 0
        Dim rtnResult As Boolean = True
        For i As Integer = 0 To max

            '行番号を設定
            rowNo = Convert.ToInt32(arr(i))

            '運賃確定チェック
            rtnResult = Me.IsUnchinKakuteiChk(frm, rowNo)


            '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
            rtnResult = rtnResult And Me.IsShiharaiKakuteiChk(frm, rowNo)   '支払確定チェック
            rtnResult = rtnResult And Me.IsUnkoLinkChk(frm, rowNo)          '運行紐づきチェック
            '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  

            'エラー行はサーバに渡さない
            If rtnResult = False Then
                Me._ChkList(i) = String.Empty
            End If

        Next

        Return arr

    End Function

    ''' <summary>
    ''' 運賃確定チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnchinKakuteiChk(ByVal frm As LMC010F, ByVal rowNo As Integer) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
        Dim flg As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010C.SprColumnIndex.SEIQ_FIXED_FLAG))
#Else
        Dim flg As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.SEIQ_FIXED_FLAG.ColNo))
#End If
        If String.IsNullOrEmpty(flg) = False AndAlso flg.Equals("00") = False Then

            '2017/09/25 修正 李↓
            MyBase.SetMessageStore(LMC010C.GUIDANCE_KBN, "E126", New String() {String.Concat(rowNo.ToString(), lgm.Selector({"行", "Line", "행", "中国語"}))}, rowNo.ToString())
            '2017/09/25 修正 李↑

        End If

        Return True

    End Function

    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
    ''' <summary>
    ''' 支払運賃確定チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsShiharaiKakuteiChk(ByVal frm As LMC010F, ByVal rowNo As Integer) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
        Dim flg As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010C.SprColumnIndex.SHIHARAI_FIXED_FLAG))
#Else
        Dim flg As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.SHIHARAI_FIXED_FLAG.ColNo))
#End If

        If String.IsNullOrEmpty(flg) = False AndAlso flg.Equals("00") = False Then

            '2017/09/25 修正 李↓
            MyBase.SetMessageStore(LMC010C.GUIDANCE_KBN, "E497", New String() {String.Concat(rowNo.ToString(), lgm.Selector({"行", "Line", "행", "中国語"}))}, rowNo.ToString())
            '2017/09/25 修正 李↑

            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 運行紐づきチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnkoLinkChk(ByVal frm As LMC010F, ByVal rowNo As Integer) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                Dim sStr1 As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010C.SprColumnIndex.TRIP_NO))
        Dim sStr2 As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010C.SprColumnIndex.TRIP_NO_SYUKA))
        Dim sStr3 As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010C.SprColumnIndex.TRIP_NO_TYUKEI))
        Dim sStr4 As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010C.SprColumnIndex.TRIP_NO_HAIKA))

#Else
        Dim sStr1 As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.TRIP_NO.ColNo))
        Dim sStr2 As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.TRIP_NO_SYUKA.ColNo))
        Dim sStr3 As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.TRIP_NO_TYUKEI.ColNo))
        Dim sStr4 As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.TRIP_NO_HAIKA.ColNo))

#End If

        If String.IsNullOrEmpty(sStr1) = False OrElse
           String.IsNullOrEmpty(sStr2) = False OrElse
           String.IsNullOrEmpty(sStr3) = False OrElse
           String.IsNullOrEmpty(sStr4) = False Then

            '2017/09/25 修正 李↓
            MyBase.SetMessageStore(LMC010C.GUIDANCE_KBN, "E230", New String() {String.Concat(rowNo.ToString(), lgm.Selector({"行", "Line", "행", "中国語"}))}, rowNo.ToString())
            '2017/09/25 修正 李↑

            Return False
        End If

        Return True

    End Function
    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  

    ''' <summary>
    ''' FFEM原料プラント間転送の行のみの選択はエラーとするチェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="arr"></param>
    ''' <returns>True: FFEM原料プラント間転送の行のみの選択のためエラー</returns>
    Private Function OnlyFFEM_MaterialPlantTransferLineChecked(ByVal frm As LMC010F, ByVal arr As ArrayList) As Boolean

        Dim max As Integer = arr.Count - 1
        Dim rowNo As Integer = 0
        Dim existsNotFFEM_MaterialPlantTransferLine As Boolean = False

        For i As Integer = 0 To max

            ' 行番号
            rowNo = Convert.ToInt32(arr(i))

            If IsFFEM_MaterialPlantTransfer(frm, rowNo) Then
                ' FFEM原料プラント間転送の行はサーバに渡さない
                Me._ChkList(i) = ""
            Else
                existsNotFFEM_MaterialPlantTransferLine = True
            End If

        Next

        If existsNotFFEM_MaterialPlantTransferLine Then
            Return False
        End If

        ' FFEM原料プラント間転送の行のみの選択の場合
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        Me.ShowMessage(frm, "E375",
                       New String() {
                            lgm.Selector({"原料のプラント間転送のデータの", "Data on inter-plant transfer of raw materials", "원료 플랜트 간 전송 데이터", "Data on inter-plant transfer of raw materials"}),
                            lgm.Selector({"変更", "changeed", "변경", "changeed"})
                            })
        Return True

    End Function

#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 印刷時のチェック(行単位)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintChk(ByVal frm As LMC010F, Optional ByVal arr As ArrayList = Nothing) As Boolean

        Dim max As Integer = 0



        If arr Is Nothing = False Then
            max = arr.Count - 1
        Else
            max = Me._ChkList.Count - 1
        End If
        Dim rowNo As Integer = 0

        Dim rtnResult As Boolean = True
        Dim low As Integer = Me.GetStartKbn()
        Dim high As Integer = Me.GetEndKbn()
        Dim msg As String = Me.GetPrintErrMsg()
        Dim shikakariHinFlg As String

        'イエローカードパス格納エリアをクリア
        Me._YCardArr = New ArrayList()

        'ADD Start 2022/10/27 033515 【LMS】FFEM 高取倉庫　速度調査対応
        If LMC010C.PrintShubetsu.COA = Me._PrintSybetuEnum Then
            '分析票マスタ(キャッシュの再取得)
            Call Me.LMCacheMasterData(LMConst.CacheTBL.COA)
        End If
        'ADD End   2022/10/27 033515 【LMS】FFEM 高取倉庫　速度調査対応

        For i As Integer = 0 To max

            '行番号を設定
            If arr Is Nothing = False Then
                '一括引き当てより
                If (LMC010C.PrintShubetsu.YELLOW_CARD).Equals(Me._PrintSybetuEnum) Then
                    'イエローカードの場合
                    rowNo = Convert.ToInt32(arr.Item(i))
                Else
                    'それ以外の場合
                    rowNo = Convert.ToInt32(arr.Item(0))
                    Me._BunsekiArr = New ArrayList()
                End If
            Else
                '印刷処理より
                rowNo = Convert.ToInt32(Me._ChkList(i))
            End If

            shikakariHinFlg = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.SHIKAKARI_HIN_FLG.ColNo))
            If shikakariHinFlg = "1" Then
                'FFEM 仕掛品の場合はチェックを行わない(分析票/イエローカードの印刷も行わない)
                Continue For
            End If

            '分析表のマスタ存在チェック&出力
            rtnResult = Me.IsPrintCOACheck(frm, rowNo)

            'イエローカードのマスタ存在チェック&出力
            rtnResult = Me.IsPrintYCardCheck(frm, rowNo)

            If Me._V.IsShijiPrtModeCheck(Me._ChkList, rowNo) = LMConst.FLG.OFF Then
                rtnResult = False
            End If

            'エラー行はサーバに渡さない
            If rtnResult = False Then
                If arr Is Nothing = False Then
                    arr.Item(0) = String.Empty
                    Return False
                Else
                    Me._ChkList(i) = String.Empty
                End If

            End If

        Next

        Return rtnResult

    End Function

    ''' <summary>
    ''' 分析表出力時、存在チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintCOACheck(ByVal frm As LMC010F, ByVal rowNo As Integer) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        '分析票以外スルー
        If LMC010C.PrintShubetsu.COA <> Me._PrintSybetuEnum Then
            Return True
        End If

        Me._JikkouDs = New LMC010DS()

        Dim dr As DataRow = _JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_BUNSEKI_IN).NewRow()
        Dim inTbl As DataTable = _JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_BUNSEKI_IN)
        Dim nrsBrCd As String = String.Empty

        nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.NRS_BR_CD.ColNo))

        dr("NRS_BR_CD") = nrsBrCd
        dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
        dr("ROW_NO") = rowNo
        dr("SYS_UPD_DATE") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.SYS_UPD_DATE.ColNo))
        dr("SYS_UPD_TIME") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.SYS_UPD_TIME.ColNo))

        inTbl.Rows.Add(dr)

        '検索時WSAクラス呼び出し
        Me._JikkouDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "SelectBunsekiOutkaM", _JikkouDs, 0)

        'START YANAI 要望番号741
        If Me._JikkouDs Is Nothing Then

            '高取倉庫の場合は分析票対象なしの場合はエラー出力しない
            If nrsBrCd.Equals("93") = False AndAlso IsSameAsTakatori(nrsBrCd) = False Then    'MOD 2019/03/25 要望管理005124
                MyBase.SetMessageStore(LMC010C.GUIDANCE_KBN, "E070")
            End If
            Return False
        End If
        'END YANAI 要望番号741

        '分析表存在チェック
        Dim whereStr As String = String.Empty
        Dim goodsCdNrs As String = String.Empty
        Dim lotNo As String = String.Empty
        Dim destCd As String = String.Empty
        'START YANAI 要望番号376
        'Dim custCdL As String = String.Empty
        'END YANAI 要望番号376
        Dim custNm As String = String.Empty
        'START YANAI 要望番号741
        Dim outNoM As String = String.Empty
        Dim drBunseki As DataRow = Nothing
        'END YANAI 要望番号741
        '2014.10.02 高取対応START
        Dim goodsCdCust As String = String.Empty
        '2014.10.02 高取対応END
        'ADD START 2018/11/14 要望番号001939
        Dim whereStrInkaDate As String = String.Empty
        Dim inkaDate As String = String.Empty
        'ADD END   2018/11/14 要望番号001939

        Dim max As Integer = _JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_BUNSEKI_OUT).Rows.Count - 1

        For i As Integer = 0 To max

            'START YANAI 要望番号741
            drBunseki = _JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_BUNSEKI_OUT).Rows(i)
            If (goodsCdNrs).Equals(drBunseki.Item("GOODS_CD_NRS").ToString()) = False OrElse
               (lotNo).Equals(drBunseki.Item("LOT_NO").ToString()) = False Then
                'END YANAI 要望番号741

                nrsBrCd = drBunseki.Item("NRS_BR_CD").ToString()
                goodsCdNrs = drBunseki.Item("GOODS_CD_NRS").ToString()
                '2014.10.02 高取対応START
                goodsCdCust = drBunseki.Item("GOODS_CD_CUST").ToString()
                '2014.10.02 高取対応END
                lotNo = drBunseki.Item("LOT_NO").ToString()
                destCd = drBunseki.Item("DEST_CD").ToString()
                'START YANAI 要望番号376
                'custCdL = drBunseki.Item("CUST_CD_L").ToString()
                'END YANAI 要望番号376
                custNm = drBunseki.Item("CUST_NM_L").ToString()
                inkaDate = drBunseki.Item("INKA_DATE").ToString()   'ADD 2018/11/14 要望番号001939

                '条件式の生成
                whereStr = " SYS_DEL_FLG = '0' "

                whereStr = String.Concat(whereStr, " AND NRS_BR_CD = '", nrsBrCd, "'")

                If String.IsNullOrEmpty(goodsCdNrs) = False Then
                    whereStr = String.Concat(whereStr, " AND GOODS_CD_NRS = '", goodsCdNrs, "'")
                End If

                If String.IsNullOrEmpty(lotNo) = False Then
                    whereStr = String.Concat(whereStr, " AND LOT_NO = '", lotNo, "'")
                End If

                If String.IsNullOrEmpty(destCd) = False Then
                    'START YANAI 要望番号376
                    'whereStr = String.Concat(whereStr, " AND DEST_CD = '", destCd, "'")
                    whereStr = String.Concat(whereStr, " AND (DEST_CD = '", destCd, "' OR DEST_CD = 'ZZZZZZZZZZZZZZZ') ")
                Else
                    whereStr = String.Concat(whereStr, " AND DEST_CD = 'ZZZZZZZZZZZZZZZ'")
                    'END YANAI 要望番号376
                End If

                'ADD START 2018/11/14 要望番号001939
                If String.IsNullOrEmpty(inkaDate) = False Then
                    whereStrInkaDate = String.Concat(" AND INKA_DATE = '", inkaDate, "' ")
                End If
                'ADD END   2018/11/14 要望番号001939

                'START YANAI 要望番号376
                'If String.IsNullOrEmpty(custCdL) = False Then
                '    whereStr = String.Concat(whereStr, " AND CUST_CD_L = '", custCdL, "'")
                'End If
                'END YANAI 要望番号376

                'DEL Start 2022/10/27 033515 【LMS】FFEM 高取倉庫　速度調査対応
                ''分析票マスタ(キャッシュの再取得)
                'Call Me.LMCacheMasterData(LMConst.CacheTBL.COA)
                'DEL End   2022/10/27 033515 【LMS】FFEM 高取倉庫　速度調査対応

                '存在チェック
                'MOD START 2018/11/14 要望番号001939
                ''Dim drBunsekiMst As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.COA).Select(whereStr)

                '検索1回目:条件に入荷日を含む
                Dim drBunsekiMst As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.COA).Select(String.Concat(whereStr, whereStrInkaDate))
                '該当データなしの場合
                If drBunsekiMst.Length() = 0 Then
                    '検索2回目:入荷日なし(汎用)
                    whereStrInkaDate = " AND INKA_DATE_VERS_FLG = '1' "
                    drBunsekiMst = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.COA).Select(String.Concat(whereStr, whereStrInkaDate))
                End If
                'MOD END   2018/11/14 要望番号001939

                If drBunsekiMst.Length() = 0 Then
                    '存在エラー時

                    '2017/09/25 修正 李↓
                    MyBase.SetMessageStore(LMC010C.GUIDANCE_KBN, "E492", New String() {
                                           lgm.Selector({"分析票マスタ", "Analysis vote master", "분석표마스터", "中国語"}),
                                           String.Concat(custNm, lgm.Selector({"の分析票", "Analysis vote ", "의 분석표", "中国語"})),
                                           String.Concat(
                                               lgm.Selector({"【対象分析票】", "【Target analysis vote】", "【대상분석표】", "中国語"}),
                                               lgm.Selector({" 商品コード:", " Goods Code:", "상품코드", "中国語"}), goodsCdCust, " Lot№:", lotNo,
                                               lgm.Selector({"届先コード:", " Destination code:", "송달처코드", "中国語"}), destCd)
                                       }, rowNo.ToString())
                    '2017/09/25 修正 李↑

                    Return False
                Else
                    '分析票のパスを取得
                    If String.IsNullOrEmpty(drBunsekiMst(0).Item("COA_LINK").ToString()) = False OrElse String.IsNullOrEmpty(drBunsekiMst(0).Item("COA_NAME").ToString()) = False Then
                        Me._BunsekiArr.Add(String.Concat(drBunsekiMst(0).Item("COA_LINK").ToString(), "\", drBunsekiMst(0).Item("COA_NAME").ToString()))
                    Else

                        '2017/09/25 修正 李↓
                        MyBase.SetMessageStore(LMC010C.GUIDANCE_KBN, "E492", New String() {
                                               lgm.Selector({"分析票マスタ", "Analysis vote master", "분석표마스터", "中国語"}),
                                               String.Concat(custNm, lgm.Selector({"の分析票", "Analysis vote ", "의 분석표", "中国語"})),
                                               String.Concat(
                                                   lgm.Selector({"【対象分析票】", "【Target analysis vote】", "【대상분석표】", "中国語"}),
                                                   lgm.Selector({" 商品コード:", " Goods Code:", "상품코드", "中国語"}), goodsCdCust, " Lot№:", lotNo,
                                                   lgm.Selector({"届先コード:", " Destination code:", "송달처코드", "中国語"}), destCd)
                                           }, rowNo.ToString())
                        '2017/09/25 修正 李↑

                        Return False
                    End If

                End If

                'START YANAI 要望番号741
            End If
            'END YANAI 要望番号741

        Next

        Return True

    End Function

    ''' <summary>
    ''' イエローカード出力時、存在チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintYCardCheck(ByVal frm As LMC010F, ByVal rowNo As Integer) As Boolean

        'イエローカード以外スルー
        If Not LMC010C.PrintShubetsu.YELLOW_CARD.Equals(Me._PrintSybetuEnum) Then
            Return True
        End If

        'データ検索用パラメータ設定
        Me._JikkouDs = New LMC010DS()
        Dim dr As DataRow = _JikkouDs.Tables("LMC010_OUTKA_M_YCARD_IN").NewRow()
        Dim inTbl As DataTable = _JikkouDs.Tables("LMC010_OUTKA_M_YCARD_IN")

        dr("NRS_BR_CD") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
        dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
        dr("ROW_NO") = rowNo
        dr("SYS_UPD_DATE") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.SYS_UPD_DATE.ColNo))
        dr("SYS_UPD_TIME") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.SYS_UPD_TIME.ColNo))

        inTbl.Rows.Add(dr)

        '検索時WSAクラス呼び出し
        Me._JikkouDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "SelectYCardOutkaM", _JikkouDs, 0)

        '対象データなし
        If Me._JikkouDs Is Nothing Then
            MyBase.SetMessageStore(LMC010C.GUIDANCE_KBN, "E070")
            Return False
        End If

        'イエローカードチェック
        Dim goodsCdNrs As String = String.Empty
        Dim lotNo As String = String.Empty

        For i As Integer = 0 To _JikkouDs.Tables("LMC010_OUTKA_M_YCARD_OUT").Rows.Count - 1
            Dim drYCard As DataRow = _JikkouDs.Tables("LMC010_OUTKA_M_YCARD_OUT").Rows(i)

            If (Not goodsCdNrs.Equals(drYCard.Item("GOODS_CD_NRS").ToString)) OrElse
                    (Not lotNo.Equals(drYCard.Item("LOT_NO").ToString)) Then

                goodsCdNrs = drYCard.Item("GOODS_CD_NRS").ToString()
                lotNo = drYCard.Item("LOT_NO").ToString()
                Dim ycardLink As String = drYCard.Item("YCARD_LINK").ToString()
                Dim ycardName As String = drYCard.Item("YCARD_NAME").ToString()

                'イエローカードのパスをチェック
                If (Not String.IsNullOrEmpty(ycardLink)) OrElse (Not String.IsNullOrEmpty(ycardName)) Then
                    'パスが設定されている
                    Me._YCardArr.Add(String.Concat(ycardLink, "\", ycardName))
                Else
                    'パスが設定されていない
                    Dim goodsCdCust As String = drYCard.Item("GOODS_CD_CUST").ToString()
                    Dim shoboCd As String = drYCard.Item("SHOBO_CD").ToString()
                    Dim custNm As String = drYCard.Item("CUST_NM_L").ToString()

                    MyBase.SetMessageStore(
                            LMC010C.GUIDANCE_KBN,
                            "E492",
                            New String() {
                                    "イエローカード管理マスタ",
                                    String.Concat(custNm, "のイエローカード"),
                                    String.Concat("【対象分析票】 商品コード:", goodsCdCust, " Lot№:", lotNo, "消防コード:", shoboCd)
                                    },
                            rowNo.ToString)

                    Return False
                End If
            End If
        Next

        Return True

    End Function

    ''' <summary>
    ''' 納品書PDF出力時、存在チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks>追加 2016/07/01</remarks>
    Private Function IsPrintNHSPdfCheck(ByVal frm As LMC010F, ByVal rowNo As Integer) As Boolean

        '納品書以外スルー
        If LMC010C.PrintShubetsu.NHS <> Me._PrintSybetuEnum Then
            Return True
        End If

        'FFEM 仕掛品の場合はチェックを行わない(印刷も行わない)
        Dim shikakariHinFlg As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.SHIKAKARI_HIN_FLG.ColNo))
        If shikakariHinFlg = "1" Then
            Return False
        End If

        Me._JikkouDs = New LMC010DS()

        Dim dr As DataRow = _JikkouDs.Tables(LMC010C.TABLE_NM_NOUHINSYO_IN).NewRow()
        Dim inTbl As DataTable = _JikkouDs.Tables(LMC010C.TABLE_NM_NOUHINSYO_IN)
        Dim nrsBrCd As String = String.Empty

        nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.NRS_BR_CD.ColNo))

        dr("NRS_BR_CD") = nrsBrCd
        dr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
        dr("ROW_NO") = rowNo
        dr("SYS_UPD_DATE") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.SYS_UPD_DATE.ColNo))
        dr("SYS_UPD_TIME") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.SYS_UPD_TIME.ColNo))

        'inTbl.Rows.Add(dr)
        '-*****  ファイルパス設定後にする

        '2019/12/03 要望管理009415 add start
        '引当計上予実区分 = 5 ならば納品書を出力しない
        Try
            '検索時WSAクラス呼び出し
            Dim copyDs As DataSet = DirectCast(Me._JikkouDs.Copy(), LMC010DS)
            Dim copyDr As DataRow = copyDs.Tables(LMC010C.TABLE_NM_NOUHINSYO_IN).NewRow()
            copyDr("NRS_BR_CD") = nrsBrCd
            copyDr("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
            copyDs.Tables(LMC010C.TABLE_NM_NOUHINSYO_IN).Rows.Add(copyDr)
            copyDs.Tables(LMC010C.TABLE_NM_OUT).Rows.Clear()
            copyDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form) _
                                                , LMC010C.BLF_NAME _
                                                , "SelectNhsNoFFEM" _
                                                , copyDs _
                                                , 0)
            If copyDs IsNot Nothing Then
                If copyDs.Tables(LMC010C.TABLE_NM_OUT).Rows.Count = 0 Then
                    '引当計上予実区分が5以外のレコードが取得できなければスルー
                    Return False
                End If
            End If
        Catch ex As Exception
            ' 検索時にエラーが発生した場合はそのまま続行
        End Try
        '2019/12/03 要望管理009415 add end

        'フォルダー取得 　(営業所、荷主コード別で)
        '  区分マスタより対象フォルダー取得
        Dim custCDL As String = String.Empty
        Dim kbnDr() As DataRow = Nothing
        Dim folderNM As String = String.Empty

        nrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
        custCDL = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.CUST_CD_L.ColNo))

        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'F020' AND ",
                                                                                       "KBN_NM1 = '", nrsBrCd, "' AND ",
                                                                                       "KBN_NM2 = '", custCDL, "'"))
        If kbnDr.Length = 0 Then
            '区分マスタに設定されていないフォルダーの場合はスルー
            Return False
        End If

        folderNM = kbnDr(0).Item("KBN_NM3").ToString.Trim

        'ファイル名作成作成
        '①"DLV"(固定)
        '②プラントコード（検索対象外）
        '③出票番号（荷主注文番号）CUST_ORD_NO
        '④納品先（届け先CD）      DEST_CD
        Dim destCD As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.DEST_CD.ColNo))

        If Left(destCD, 2).Equals("BB") = True Then
            '届け先コード頭２桁が"BB"のときは、届け先コードで

        Else
            '頭に”0”を付加し、届け先コードの先頭９桁で（10桁編集）
            destCD = String.Concat("0", Mid(destCD, 1, 9))

        End If
        '⑤作成日付（一番新しい日付）
        Dim fileNM As String = String.Empty
        Dim custOderCD As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.CUST_ORD_NO.ColNo))
        Dim fileArr As ArrayList = New ArrayList()

        fileNM = String.Concat("DLV_", "*_", custOderCD.Trim, "_", destCD.Trim, "_", "*.pdf")

        'フォルダー内検索
        For Each stFilePath As String In System.IO.Directory.GetFiles(folderNM, fileNM, System.IO.SearchOption.TopDirectoryOnly)
            fileArr.Add(System.IO.Path.GetFullPath(stFilePath))
        Next

        If fileArr.Count = 0 Then

            Dim isNeedlessNhs As Boolean = False
            inTbl.Rows.Add(dr)

            Try
                '検索時WSAクラス呼び出し
                Me._JikkouDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form) _
                                                       , LMC010C.BLF_NAME _
                                                       , LMC010C.Functions.SelectNeedlessNhsData _
                                                       , _JikkouDs, 0)
                If Me._JikkouDs IsNot Nothing Then
                    isNeedlessNhs = (Me._JikkouDs.Tables(LMC010C.TABLE_NM_OUT).Rows.Count > 0)
                End If

            Catch ex As Exception
                ' 検索時にエラーが発生した場合は、納品書が必要として処理を行う。
                isNeedlessNhs = False
            End Try

            If (isNeedlessNhs) Then
                Me._NouhinsyoArr = New ArrayList()
                Return True
            End If

            '該当なし
            MyBase.SetMessageStore(LMC010C.GUIDANCE_KBN, "E492", New String() {String.Concat("納品書PDFフォルダー(", folderNM, ")"), fileNM, String.Concat("オーダー番号:", custOderCD.Trim)}, rowNo.ToString())

            '高取倉庫以外
            If nrsBrCd.Equals("93") = False AndAlso IsSameAsTakatori(nrsBrCd) = False Then    'MOD 2019/03/25 要望管理005124
                Call Me.ShowStorePrintData(frm)
            End If

            Return False
        End If

        fileArr.Sort()
        Dim lastCnt As Integer = fileArr.Count - 1

#If False Then   ' 未使用につき削除
        'ソート後の最後のファイルを設定
        dr("PDF_FILE_PATH") = fileArr(lastCnt)
        inTbl.Rows.Add(dr)
#End If


        '初期化
        Me._NouhinsyoArr = New ArrayList()
        _NouhinsyoArr.Add(fileArr(lastCnt))

        Return True

    End Function

    'START YANAI 20120122 立会書印刷対応
    ''' <summary>
    ''' 立会書出力時、存在チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsCustDetailsCheck(ByVal frm As LMC010F, ByVal rowNo As Integer) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim kbnCd As String = String.Empty
        Dim colNo As Integer = 49

        '立会書印刷対象以外はスルー
        If LMC010C.PrintShubetsu.NHS = Me._PrintSybetuEnum OrElse
            LMC010C.PrintShubetsu.TACHIAI = Me._PrintSybetuEnum OrElse
            LMC010C.PrintShubetsu.ALL_PRINT = Me._PrintSybetuEnum Then
            kbnCd = "13"
        Else
            Return True
        End If

        '未入力の場合はFalseを戻す
        If String.IsNullOrEmpty(kbnCd) = True Then
            Return False
        End If

        Dim custCdL As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.CUST_CD_L.ColNo))
        '要望番号:1253 terakawa 2012.07.13 Start
        Dim nrsBrCd As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
        '要望番号:1253 terakawa 2012.07.13 End
        frm.sprDetail.SetCellValue(rowNo, colNo, String.Empty)

        '荷主明細マスタの存在チェック

        'Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat( _
        '                                                                                                        "CUST_CD LIKE '", custCdL, "%' AND ", _
        '                                                                                                        "SUB_KB = '", kbnCd, "'") _
        '                                                                                                       )
        '要望番号:1253 terakawa 2012.07.13 Start
        Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat(
                                                                                                        "NRS_BR_CD ='", nrsBrCd, "' AND ",
                                                                                                        "CUST_CD LIKE '", custCdL, "%' AND ",
                                                                                                        "SUB_KB = '", kbnCd, "'")
                                                                                                       )
        '要望番号:1253 terakawa 2012.07.13 End

        If drs.Length < 1 Then
            If LMC010C.PrintShubetsu.TACHIAI = Me._PrintSybetuEnum Then

                '2017/09/25 修正 李↓
                MyBase.SetMessageStore(LMC010C.GUIDANCE_KBN, "E079", New String() {lgm.Selector({"荷主明細マスタ", "Shippers specification master", "하주명세마스터", "中国語"}), custCdL}, rowNo.ToString())
                '2017/09/25 修正 李↑

                Return False
            End If
        End If

        frm.sprDetail.SetCellValue(rowNo, colNo, "01")

        Return True

    End Function
    'END YANAI 20120122 立会書印刷対応

    ''' <summary>
    ''' 印刷対象チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <param name="low">下限</param>
    ''' <param name="high">上限</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsOutkaStateKbCheck(ByVal frm As LMC010F _
                                             , ByVal rowNo As Integer _
                                             , ByVal low As Integer _
                                             , ByVal high As Integer _
                                             , ByVal msg As String
                                             ) As Boolean

        '置換文字がない場合、スルー
        If String.IsNullOrEmpty(msg) = True Then
            Return True
        End If

        '出荷報告書関連チェック
        With frm.sprDetail.ActiveSheet
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
            Dim outkaStateKb As Integer = Convert.ToInt32(.Cells(rowNo, LMC010C.SprColumnIndex.OUTKA_STATE_KB).Text())
#Else
            Dim outkaStateKb As Integer = Convert.ToInt32(.Cells(rowNo, LMC010G.sprDetailDef.OUTKA_STATE_KB.ColNo).Text())
#End If

            If low < outkaStateKb And outkaStateKb < high Then
                MyBase.SetMessageStore(LMC010C.GUIDANCE_KBN, "E175", New String() {msg}, rowNo.ToString())
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 荷札出力時、荷札有無フラグのチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintNIHUDACheck(ByVal frm As LMC010F, ByVal rowNo As Integer) As Boolean

        '分析票以外スルー
        If LMC010C.PrintShubetsu.NIHUDA <> Me._PrintSybetuEnum Then
            Return True
        End If

        Dim nihudaYN As String = String.Empty
        nihudaYN = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.NIHUDA_YN.ColNo))

        If ("00").Equals(nihudaYN) = True Then
            MyBase.SetMessageStore(LMC010C.GUIDANCE_KBN, "E411", New String() {String.Empty}, rowNo.ToString())
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' ITW荷札出力時、帳票パターンマスタの存在チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintITWNIHUDACheck(ByVal frm As LMC010F, ByVal rowNo As Integer) As Boolean

        'ITW荷札以外スルー
        If LMC010C.PrintShubetsu.ITW_NIHUDA <> Me._PrintSybetuEnum Then
            Return True
        End If

        Dim dr As DataRow() = Nothing
        Dim nrsBrCd As String = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
        Dim rptId As String = String.Empty
        Select Case Me._PrintSybetuEnum
            Case LMC010C.PrintShubetsu.ITW_NIHUDA
                rptId = "LMC557"
        End Select

        '帳票マスタ
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.RPT).Select(String.Concat(
                                                                           "NRS_BR_CD = '", nrsBrCd, "' AND " _
                                                                         , "PTN_ID = '10' AND " _
                                                                         , "RPT_ID = '", rptId, "' AND " _
                                                                         , "SYS_DEL_FLG = '0'"))

        If 0 >= dr.Length Then
            'MyBase.SetMessageStore(LMC010C.GUIDANCE_KBN, "E320", New String() {"荷札の帳票パターンが未設定", "印刷"}, rowNo.ToString())
            MyBase.SetMessageStore(LMC010C.GUIDANCE_KBN, "E837", , rowNo.ToString())
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' スタート位置
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetStartKbn() As Integer

        GetStartKbn = 0

        Select Case Me._PrintSybetuEnum

            Case LMC010C.PrintShubetsu.ALL_PRINT _
                , LMC010C.PrintShubetsu.COA _
                , LMC010C.PrintShubetsu.NIHUDA _
                , LMC010C.PrintShubetsu.DENP _
                , LMC010C.PrintShubetsu.NHS

                GetStartKbn = 0

            Case LMC010C.PrintShubetsu.HOKOKU

                GetStartKbn = 0

        End Select


        Return GetStartKbn

    End Function

    ''' <summary>
    ''' エンド位置
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetEndKbn() As Integer

        GetEndKbn = 0

        Select Case Me._PrintSybetuEnum

            Case LMC010C.PrintShubetsu.ALL_PRINT _
                , LMC010C.PrintShubetsu.COA _
                , LMC010C.PrintShubetsu.NIHUDA _
                , LMC010C.PrintShubetsu.DENP _
                , LMC010C.PrintShubetsu.NHS

                GetEndKbn = 0

            Case LMC010C.PrintShubetsu.HOKOKU

                'START YANAI 要望番号497
                'GetEndKbn = 60
                GetEndKbn = 0
                'END YANAI 要望番号497

        End Select

        Return GetEndKbn

    End Function

    ''' <summary>
    ''' 置換文字
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetPrintErrMsg() As String

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        GetPrintErrMsg = String.Empty

        Select Case Me._PrintSybetuEnum

            Case LMC010C.PrintShubetsu.ALL_PRINT

                '2017/09/25 修正 李↓
                GetPrintErrMsg = lgm.Selector({"一括", "Lump", "일괄", "中国語"})
                '2017/09/25 修正 李↑

            Case LMC010C.PrintShubetsu.HOKOKU

                '2017/09/25 修正 李↓
                GetPrintErrMsg = lgm.Selector({"出荷報告", "O/D report", "출하보고", "中国語"})
                '2017/09/25 修正 李↑

            Case LMC010C.PrintShubetsu.COA

                '2017/09/25 修正 李↓
                GetPrintErrMsg = lgm.Selector({"分析票", "Analysis Sheet", "분석표", "中国語"})
                '2017/09/25 修正 李↑

            Case LMC010C.PrintShubetsu.NIHUDA

                '2017/09/25 修正 李↓
                GetPrintErrMsg = lgm.Selector({"荷札", "Tag", "꼬리표", "中国語"})
                '2017/09/25 修正 李↑

            Case LMC010C.PrintShubetsu.DENP

                '2017/09/25 修正 李↓
                GetPrintErrMsg = lgm.Selector({"送状", "Invoice", "송장", "中国語"})
                '2017/09/25 修正 李↑

            Case LMC010C.PrintShubetsu.NHS

                '2017/09/25 修正 李↓
                GetPrintErrMsg = lgm.Selector({"納品書", "Packing slip", "납품서", "中国語"})
                '2017/09/25 修正 李↑

                'START YANAI 20120122 立会書印刷対応
            Case LMC010C.PrintShubetsu.TACHIAI

                '2017/09/25 修正 李↓
                GetPrintErrMsg = lgm.Selector({"立会書", "Witness statement", "입회서", "中国語"})
                '2017/09/25 修正 李↑

        End Select

        Return GetPrintErrMsg

    End Function

    ''' <summary>
    ''' 印刷対象リモート PDF のコピー先ディレクトリ名 決定
    ''' </summary>
    Private Sub SetCopyToDirectoryName()

        Me._copyToDirectoryName = String.Concat(SetCopyToParentDirectoryName(), "\", "PDF_COPY")

    End Sub

    ''' <summary>
    ''' 印刷対象リモート PDF のコピー先の親ディレクトリ名 決定
    ''' </summary>
    Private Function SetCopyToParentDirectoryName() As String

        ' 仮初期値の設定
        Dim copyToParentDirectoryName As String = "C:\LMUSER"

        ' 区分マスタよりの分析票ローカルパスの取得
        Dim filter As String = "KBN_GROUP_CD = 'B009' AND KBN_CD = '00'"
        If MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter).Count = 0 Then
            ' 区分マスタが存在しない場合は仮初期値のままとする。
            Return copyToParentDirectoryName
        End If
        Dim coaDir As String =
            (MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)(0).Item("KBN_NM1")).ToString()
        If coaDir.Substring(coaDir.Length - 2, 2) = ":\" Then
            ' 分析票ローカルパスがドライブルートの場合は仮初期値のままとする。
            Return copyToParentDirectoryName
        End If
        ' 分析票ローカルパスがドライブルートでない場合
        ' 分析票ローカルパスの親ディレクトリをコピー先の親ディレクトリ名とする。
        Dim di As New System.IO.DirectoryInfo(coaDir)
        copyToParentDirectoryName = di.Parent.FullName
        If copyToParentDirectoryName.Substring(copyToParentDirectoryName.Length - 2, 2) = ":\" Then
            ' ただし、分析票ローカルパスの親ディレクトリがドライブルートの場合は
            ' 分析票ローカルパスそのものをコピー先の親ディレクトリ名とする。
            copyToParentDirectoryName = coaDir
        End If
        Return copyToParentDirectoryName

    End Function

    ''' <summary>
    ''' リモートの 納品書 または 分析票 または イエローカード PDF ファイルのローカルへのコピー 
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="src">リモートファイルパスの配列</param>
    ''' <returns>コピー先ローカルファイルパスの配列</returns>
    Private Function CopyRemotePdf(ByVal frm As LMC010F, ByVal src() As String) As String()

        Dim i As Integer

        ' 戻り値（コピー先ローカルファイルパスの配列）初期化
        ' 初期値：リモートファイルパスそのもの
        Dim dst() As String
        ReDim dst(src.Length() - 1)
        For i = 0 To dst.Length() - 1
            dst(i) = src(i)
        Next

        If Me._copyToDirectoryName.Length > 0 Then
            Select Case Me._PrintSybetuEnum
                Case LMC010C.PrintShubetsu.NHS, LMC010C.PrintShubetsu.COA, LMC010C.PrintShubetsu.YELLOW_CARD
                    ' 納品書 または 分析票 または イエローカード コピー前 ログ出力
                    Me.WritePrintLog(frm, src, Me._PrintSybetuEnum + 100)

                    ' コピー先ローカルファイルパスの編集
                    ' （リモートファイルパスの配列数分の繰り返し処理）
                    For i = 0 To src.Length() - 1
                        ' 繰り返し処理の現対象配列要素より手前に同一ファイル名がある場合のカウント
                        Dim dupCnt As Integer = 0
                        Dim dupExists As Boolean = False
                        For j As Integer = 0 To i - 1
                            If Path.GetFileName(src(i)).Equals(Path.GetFileName(src(j))) Then
                                dupCnt += 1
                                dupExists = True
                            End If
                        Next
                        ' 繰り返し処理の現対象配列要素より後に同一ファイル名があるか否かの判定
                        If Not dupExists Then
                            For k As Integer = i + 1 To src.Length() - 1
                                If Path.GetFileName(src(i)).Equals(Path.GetFileName(src(k))) Then
                                    dupExists = True
                                    Exit For
                                End If
                            Next
                        End If
                        ' コピー先ローカルファイルパスの編集
                        ' リモートファイルパスの配列内に同一ファイル名が存在する場合、
                        ' ローカルファイル名の拡張子（例: ".pdf"）の手前に "_" と連番文字列を付加する。
                        ' （リモートファイルパスの配列内に同一ファイル名が存在しなければリモートファイル名そのまま）
                        dst(i) = String.Concat(
                                Me._copyToDirectoryName, "\",
                                Path.GetFileNameWithoutExtension(src(i)),
                                IIf(dupExists, String.Concat("_", (dupCnt + 1).ToString()), ""),
                                Path.GetExtension(src(i)))
                    Next
                    Dim pathForLog() As String
                    ReDim pathForLog(dst.Length() - 1)
                    For i = 0 To dst.Length() - 1
                        Try
                            If Not Directory.Exists(Me._copyToDirectoryName) Then
                                Call Directory.CreateDirectory(Me._copyToDirectoryName)
                            End If
                            File.Copy(src(i), dst(i), True)
                            pathForLog(i) = dst(i)
                        Catch ex As Exception
                            pathForLog(i) = String.Concat(dst(i), " ", "コピー失敗（詳細は次行以降）", vbCrLf, ex.ToString())
                            dst(i) = String.Concat(
                                        Me._copyToDirectoryName, "\",
                                        "コピーに失敗した場合に意図的に印刷を失敗させるための実在しないファイル名", "_",
                                        DateTime.Now.ToString("yyyyMMddHHmmssfff"), "_",
                                        Path.ChangeExtension(Path.GetRandomFileName(), ".pdf"))
                        End Try
                    Next

                    ' 納品書 または 分析票 または イエローカード コピー後 ログ出力
                    Me.WritePrintLog(frm, pathForLog, Me._PrintSybetuEnum + 200)
            End Select
        End If

        Return dst

    End Function

    ''' <summary>
    ''' PDF 直接印刷共通部品でのエラー発生時のメッセージ編集処理
    ''' （複数行選択時はエラーのあった出荷管理番号を特定できないため、
    ''' 　すべてをカンマ区切りで表示する）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="lgm">言語に関するコントロールを行うクラスのインスタンス</param>
    ''' <param name="rowNoArr">選択行を格納した ArrayList</param>
    Private Sub ShowStorePrintErrorData(ByVal frm As LMC010F, ByVal lgm As LmLangMGR, ByVal rowNoArr As ArrayList)

        Dim messageId As String = ""

        ' エラー発生帳票別メッセージ決定
        Select Case Me._PrintSybetuEnum
            Case LMC010C.PrintShubetsu.NHS
                messageId = "E845"

            Case LMC010C.PrintShubetsu.COA
                messageId = "E800"

            Case LMC010C.PrintShubetsu.YELLOW_CARD
                messageId = "E846"

            Case Else
                Return

        End Select

        ' 選択行の出荷管理番号の配列化
        Dim outkaNoLArr As New List(Of String)(rowNoArr.Count)
        For j As Integer = 0 To rowNoArr.Count - 1
            Dim i As Integer = Convert.ToInt32(rowNoArr(j))
            With frm.sprDetail.ActiveSheet
                Dim outkaNoL As String = Me._LMCconV.GetCellValue(.Cells(i, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
                outkaNoLArr.Add(outkaNoL)
            End With
        Next
        If rowNoArr.Count = 1 Then
            ' 出荷管理番号が単一の場合、行と合わせて表示する。
            MyBase.SetMessageStore("00", messageId, New String() {outkaNoLArr(0)}, rowNoArr(0).ToString())
        Else
            ' 出荷管理番号が複数の場合、カンマ区切りで表示する。行指定は省略する。
            Dim outkaNo As String =
                String.Concat("[", String.Join(", ", outkaNoLArr), "]", " ",
                              "(", lgm.Selector({"いずれか", "Any of them", "Any of them", "Any of them"}), ")")
            MyBase.SetMessageStore("00", messageId, New String() {outkaNo})
        End If

    End Sub

#End Region

#End Region

#End Region '内部メソッド

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(新規)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMC010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "ShiftInsertStatus")

        '「新規」処理
        Call Me.ActionControl(LMC010C.EventShubetsu.SINKI, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "ShiftInsertStatus")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し(完了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMC010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "ShiftCompleteStatus")

        '「完了」処理
        Call Me.ActionControl(LMC010C.EventShubetsu.KANRYO, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "ShiftCompleteStatus")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMC010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Call ActionControl(LMC010C.EventShubetsu.KENSAKU, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMC010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        'イベント判定
        If e.KeyCode = Keys.Enter Then
            'Enterキー押下時イベント：１件時表示なし
            Me._PopupSkipFlg = False

        Else
            'F10押下時イベント：１件時表示あり
            Me._PopupSkipFlg = True

        End If


        Call Me.ActionControl(LMC010C.EventShubetsu.MASTER, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")


    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(初期荷主変更処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMC010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey11Press")

        'イベント判定
        If e.KeyCode = Keys.Enter Then
            'Enterキー押下時イベント：１件時表示なし
            Me._PopupSkipFlg = False

        Else
            'F10押下時イベント：１件時表示あり
            Me._PopupSkipFlg = True

        End If

        '「初期荷主変更」処理
        Call Me.ActionControl(LMC010C.EventShubetsu.DEF_CUST, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMC010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMC010F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "RowSelection")

        '「ダブルクリック」処理
        Call Me.ActionControl(LMC010C.EventShubetsu.DOUBLE_CLICK, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "RowSelection")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

    'START KIM 20120920 倉庫システムVer2.0 特定荷主対応
#Region "ハネウェルCSV引当対応"

#Region "CSV読み取り関連"

    ''' <summary>
    ''' CSVファイル読取
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCSVData(ByVal frm As LMC010F, ByRef csvDt As LMC010DS.LMC010_OUTKA_CSV_INDataTable) As Boolean

        Dim filePath As String = String.Empty
        Dim fileName As String = String.Empty

        'ファイルパス取得
        '取得先のCSVファイルのパス・ファイル名
        Dim kbnDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select( _
                                                  String.Concat("KBN_GROUP_CD = 'E032' AND KBN_CD = '02'"))
        If 0 < kbnDr.Length Then
            filePath = kbnDr(0).Item("KBN_NM1").ToString
            fileName = kbnDr(0).Item("KBN_NM2").ToString
        End If

        'フォルダ・ファイル存在チェック
        If Me._V.IsFolderExist(filePath) = False OrElse Me._V.IsFileExist(filePath, fileName) = False Then
            Return False
        End If

        'CSVファイル読込み
        If Me.ReadCSVFile(String.Concat(filePath, fileName), csvDt) = False Then
            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            MyBase.ShowMessage(frm, "E235")
            Return False
        End If

        'START 2013/3/4 KIM修正
        'INデータテーブルを出荷管理番号順でソートする
        Dim sortDrs As DataRow() = csvDt.Copy.Select("", "OUTKA_NO_L, OUTKA_NO_M")
        csvDt.Clear()
        Dim dr As LMC010DS.LMC010_OUTKA_CSV_INRow = Nothing
        For i As Integer = 0 To sortDrs.Length - 1
            dr = csvDt.NewLMC010_OUTKA_CSV_INRow
            dr.OUTKA_NO_L = sortDrs(i).Item("OUTKA_NO_L").ToString()
            dr.OUTKA_NO_M = sortDrs(i).Item("OUTKA_NO_M").ToString()
            dr.SERIAL_NO = sortDrs(i).Item("SERIAL_NO").ToString()
            csvDt.AddLMC010_OUTKA_CSV_INRow(dr)
        Next
        'END   2013/3/4 KIM修正

        Return True

    End Function

    ''' <summary>
    ''' ファイル読込み
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="csvDt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ReadCSVFile(ByVal filePath As String, ByRef csvDt As LMC010DS.LMC010_OUTKA_CSV_INDataTable) As Boolean

        Dim rtnBln As Boolean = True

        '格納変数
        Dim OUTKA_NO_LM As String = String.Empty
        Dim OUTKA_NO_L As String = String.Empty
        Dim OUTKA_NO_M As String = String.Empty
        Dim SERIAL_NO As String = String.Empty
        Dim rtnFlg As Boolean = True
        Dim rowCnt As Integer = 0

        Dim dr As LMC010DS.LMC010_OUTKA_CSV_INRow = Nothing

        Dim temp As String()

        Dim sr As New System.IO.StreamReader(filePath, System.Text.Encoding.GetEncoding("shift_jis"))

        '内容を一行ずつ読み込む
        Do While sr.Peek() >= 0

            temp = Split(sr.ReadLine(), ",")
            rowCnt = rowCnt + 1

            'タイトル行はチェックを行わない
            If rowCnt = 1 Then
                Continue Do
            End If

            'CSV項目チェック
            If Me.IsCSVDataCheck(temp, rowCnt) = False Then
                rtnFlg = False
                Continue Do
            End If

            OUTKA_NO_LM = temp(0).Trim(Chr(34))
            SERIAL_NO = temp(1).Trim(Chr(34))

            '明細データをデータテーブルに設定
            dr = csvDt.NewLMC010_OUTKA_CSV_INRow
            Me.SetDetailRow(dr, OUTKA_NO_LM, SERIAL_NO)
            csvDt.AddLMC010_OUTKA_CSV_INRow(dr)

        Loop

        '閉じる
        sr.Close()

        Return rtnFlg

    End Function

    ''' <summary>
    ''' 値セット
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="OUTKA_NO_LM"></param>
    ''' <param name="SERIAL_NO"></param>
    ''' <remarks></remarks>
    Private Sub SetDetailRow(ByRef dr As LMC010DS.LMC010_OUTKA_CSV_INRow, ByVal OUTKA_NO_LM As String, ByVal SERIAL_NO As String)

        dr.OUTKA_NO_L = OUTKA_NO_LM.Substring(0, 9)
        dr.OUTKA_NO_M = OUTKA_NO_LM.Substring(9, 3)
        dr.SERIAL_NO = SERIAL_NO

    End Sub

#End Region

    ''' <summary>
    ''' ハネウェルCSV引当処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    ''' '20130218 アグリマート対応 START
    Private Sub JikkoShoriForHW(ByVal frm As LMC010F, ByVal prm As LMFormData, Optional ByVal arr As ArrayList = Nothing)
        'Private Sub JikkoShoriForHW(ByVal frm As LMC010F, ByVal prm As LMFormData)
        '20130218 アグリマート対応 END

        Me._JikkouDs = New LMC010DS()
        Dim setDs As LMC010DS = DirectCast(Me._JikkouDs.Copy(), LMC010DS)
        Dim inTbl As LMC010DS.LMC010_OUTKA_M_INDataTable = setDs.LMC010_OUTKA_M_IN
        Dim setDt As LMC010DS.LMC010_OUTKA_M_INDataTable = DirectCast(Me._JikkouDs, LMC010DS).LMC010_OUTKA_M_IN
        Dim csvDt As LMC010DS.LMC010_OUTKA_CSV_INDataTable = DirectCast(Me._JikkouDs, LMC010DS).LMC010_OUTKA_CSV_IN

        Dim dr As LMC010DS.LMC010_OUTKA_M_INRow = setDs.LMC010_OUTKA_M_IN.NewLMC010_OUTKA_M_INRow
        Dim recNo As Integer = 0

        Dim strNrsBrCd As String = frm.cmbEigyo.SelectedValue().ToString()

        Dim custDetailsDr() As DataRow = Nothing

        '2013.02.18 アグリマート対応 START
        Dim jikkouSyubetu As String = frm.cmbJikkou.SelectedValue().ToString()

        Select Case jikkouSyubetu

            Case "11"

                Dim max As Integer = arr.Count - 1
                Dim setDsWit As DataSet = Me._JikkouDs.Copy()
                Dim inTblWit As DataTable = setDsWit.Tables("LMC010_OUTKA_M_IN")
                Dim pickDt As DataTable
                Dim drWit As DataRow

                For i As Integer = 0 To max

                    '別インスタンスのデータロウを空にする
                    inTblWit.Clear()

                    drWit = setDsWit.Tables("LMC010_OUTKA_M_IN").NewRow()

                    strNrsBrCd = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))

                    recNo = Convert.ToInt32(arr(i))
                    drWit("NRS_BR_CD") = strNrsBrCd
                    drWit("OUTKA_NO_L") = Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(recNo, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))
                    inTblWit.Rows.Add(drWit)

                    '出荷データ（中番）取得
                    setDsWit = MyBase.CallWSA("LMC010BLF", "SelectOutkaPickWk", setDsWit)
                    pickDt = setDsWit.Tables("LMC010_OUTKA_M_PICK_WK")

                    If pickDt.Rows.Count = 0 Then
                        Continue For
                    End If

                    inTblWit.Clear()

                    '値設定
                    For j As Integer = 0 To pickDt.Rows.Count - 1

                        '別インスタンスのデータロウを空にする
                        inTbl.Clear()

                        dr.NRS_BR_CD = pickDt.Rows(j).Item("NRS_BR_CD").ToString()
                        dr.OUTKA_NO_L = pickDt.Rows(j).Item("OUTKA_NO_L").ToString()
                        dr.OUTKA_NO_M = pickDt.Rows(j).Item("OUTKA_NO_M").ToString()
                        dr.SERIAL_NO = pickDt.Rows(j).Item("SERIAL_NO").ToString()
                        inTbl.Rows.Add(dr)
                        setDt.ImportRow(inTbl.Rows(0))
                    Next

                Next

            Case Else

                'CSV読み込み
                If Me.GetCSVData(frm, csvDt) = False Then
                    'エラーがあったら中断
                    Exit Sub
                End If

                '読み込みデータがない場合、エラー
                If csvDt.Rows.Count = 0 Then
                    '英語化対応
                    '20151021 tsunehira add
                    MyBase.ShowMessage(frm, "E675")
                    'MyBase.ShowMessage(frm, "E483", New String() {"CSV引当"})
                    Exit Sub
                End If

                'csvデータでinpuDataSet作成（検索結果spreadが存在しないため、基本抽出条件のみ格納
                For i As Integer = 0 To csvDt.Count - 1

                    '別インスタンスのデータロウを空にする
                    inTbl.Clear()

                    dr.NRS_BR_CD = strNrsBrCd
                    dr.OUTKA_NO_L = csvDt(i).OUTKA_NO_L
                    dr.OUTKA_NO_M = csvDt(i).OUTKA_NO_M
                    'START 2013/3/4 CSV引当対応
                    dr.SERIAL_NO = csvDt(i).SERIAL_NO
                    'END   2013/3/4 CSV引当対応
                    inTbl.Rows.Add(dr)
                    setDt.ImportRow(inTbl.Rows(0))

                Next

        End Select
        '20130218 アグリマート対応 END

        '出荷データ（LMC010_OUTKA_M_OUT）取得
        Me._JikkouDs = Me._LMCconH.CallWSAAction(DirectCast(frm, Form), "LMC010BLF", "SelectOutkaMForHW", Me._JikkouDs, 0)

        If Me._JikkouDs Is Nothing Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E192")
            Exit Sub
        End If

        'IN情報を再設定（出荷Mから取得したデータで再設定）
        Dim drs As DataRow() = Nothing
        Dim preOutkaNoL As String = String.Empty
        Dim reSetDt As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_IN)
        For i As Integer = 0 To setDt.Rows.Count - 1

            If String.IsNullOrEmpty(preOutkaNoL) = True OrElse preOutkaNoL.Equals(setDt.Rows(i).Item("OUTKA_NO_L").ToString()) = False Then

                reSetDt.ImportRow(setDt.Rows(i))

                reSetDt(i).Item("ERR_FLG") = "00"
                reSetDt(i).Item("ROW_NO") = i + 1
                reSetDt(i).Item("OUTKA_NO_M") = String.Empty

                drs = Me._JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT).Select(String.Concat("NRS_BR_CD = '", strNrsBrCd, "' AND OUTKA_NO_L = '", setDt(i).OUTKA_NO_L, "' "))

                If 0 < drs.Length Then

                    reSetDt(i).Item("SYS_UPD_DATE") = drs(0).Item("SYS_UPD_DATE").ToString()
                    reSetDt(i).Item("SYS_UPD_TIME") = drs(0).Item("SYS_UPD_TIME").ToString()
                    reSetDt(i).Item("CUST_CD_L") = drs(0).Item("CUST_CD_L").ToString()
                    reSetDt(i).Item("SASZ_USER") = drs(0).Item("SASZ_USER").ToString()
                    reSetDt(i).Item("NHS_FLAG") = drs(0).Item("NHS_FLAG").ToString()

                    custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select( _
                                    String.Concat("NRS_BR_CD = '", strNrsBrCd, "' AND CUST_CD = '", reSetDt(i).Item("CUST_CD_L"), "' AND SUB_KB = '37'"))

                    If custDetailsDr.Length > 0 Then
                        reSetDt(i).Item("TOU_BETU_FLG") = "1"
                    Else
                        reSetDt(i).Item("TOU_BETU_FLG") = "0"
                    End If

                End If

            End If

            preOutkaNoL = setDt.Rows(i).Item("OUTKA_NO_L").ToString()

        Next

        'Falseの場合、更新対象有
        Dim hikiFlg As Boolean = True

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '引当合計個数、引当合計数量、前行キーを初期化
        Me._HikiSumNb = 0
        Me._HikiSumQt = 0
        Me._PreKey = String.Empty

        For i As Integer = 0 To Me._JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT).Rows.Count - 1

            '在庫引当画面を呼び出す(ハネウェルCSV引当)

            hikiFlg = Not Me.SetReturnHikiatePopForHW(frm, Me._JikkouDs, i)
            If hikiFlg = True Then
                Exit For
            End If
            'hikiFlg = hikiFlg And Not Me.SetReturnHikiatePopForHW(frm, Me._JikkouDs, i)

        Next

        '更新対象がない場合
        If hikiFlg = True Then

            'メッセージ表示
            MyBase.ShowMessage(frm, "E183")

            '終了処理
            Call Me._LMCconH.EndAction(frm, "E183")
            Exit Sub

        End If

        'Notes1932 START
        Dim flgHW As String = String.Empty
        flgHW = "1" '1:ON
        'Notes1932 END

        '出荷(大)の進捗区分、出荷梱包個数の設定
        Me._JikkouDs = Me.SetOutkaL(frm, Me._JikkouDs, flgHW)

        '2014/.04.11 アグリマート対応(運送重量のずれ修正) 黎 --ST--
        Select Case jikkouSyubetu

            Case "11"
                'アグリマート対応
                '出荷Mのデータもシリアル単位で作成する、結果として出荷S相当数のレコードを出荷Mに作成してしまう。
                '出荷Mのレコード数を絞り込む事で対応

                '============= OUTKA_M_OUT ============
                '元となる出荷Mのレコードをコピー退避
                Dim dtEscapeOM As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT).Copy()

                '絞込み済出荷Mのデータテーブル
                Dim dtNewOM As DataTable = Me.GetSumDs(dtEscapeOM)

                'クリア
                Me._JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT).Clear()
                '再生成したものをマージ
                Me._JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT).Merge(dtNewOM)
                '============= OUTKA_M_OUT ============

                '============= OUTKA_M ============
                '元となる出荷Mのレコードをコピー退避
                Dim dtEscapeM As DataTable = Me._JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M).Copy()

                '絞込み済出荷Mのデータテーブル
                Dim dtNewM As DataTable = Me.GetSumDs(dtEscapeM)

                'クリア
                Me._JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M).Clear()
                '再生成したものをマージ
                Me._JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M).Merge(dtNewM)
                '============= OUTKA_M ============

                '運送データ（大）データセットの生成
                Me._JikkouDs = Me.SetUnsoL(frm, Me._JikkouDs)

                '============ 絞込みの復元 ============
                'クリア
                Me._JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT).Clear()
                '退避しておいたものをマージ
                Me._JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT).Merge(dtEscapeOM)

                'クリア
                Me._JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M).Clear()
                '退避しておいたものをマージ
                Me._JikkouDs.Tables(LMC010C.TABLE_NM_OUTKA_M).Merge(dtEscapeM)
                '============ 絞込みの復元 ============

            Case Else
                '通常ハネウェルCSV引当用

                '運送データ（大）データセットの生成
                Me._JikkouDs = Me.SetUnsoL(frm, Me._JikkouDs)

        End Select
        '2014/.04.11 アグリマート対応(運送重量のずれ修正) 黎 --ST--

        Me._JikkouDs.Merge(New RdPrevInfoDS)
        Me._JikkouDs.Tables(LMConst.RD).Clear()

        '更新実行
        Me._JikkouDs = MyBase.CallWSA("LMC010BLF", "InsertSaveAction", Me._JikkouDs)

        If IsMessageExist() = False Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})
        Else
            Call Me._LMCconH.EndAction(frm, MyBase.GetMessageID)
            Exit Sub
        End If

        '2013.03.05 HWでは使用しない為飛ばすSTART
        ''日立物流出荷音声データCSV作成処理
        'If Me.ShowStorePrintData(frm) = True Then
        '    Me.DicCsvMakeForHW(frm, prm, Me._JikkouDs)
        'End If
        '2013.03.05 HWでは使用しない為飛ばすEND

        'プレビュー判定 
        Dim prevDt As DataTable = _JikkouDs.Tables(LMConst.RD)
        If prevDt.Rows.Count > 0 Then

            'プレビューの生成
            Dim prevFrm As New RDViewer()

            'データ設定
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始
            prevFrm.Run()

            'プレビューフォームの表示
            prevFrm.Show()

        End If

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 在庫引当の戻り値を設定(ハネウェルCSV引当)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnHikiatePopForHW(ByVal frm As LMC010F, ByVal ds As DataSet, ByVal i As Integer) As Boolean

        Dim outkaMout As DataTable = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT)
        Dim outkaMin As DataTable = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_IN)

        If ("03").Equals(outkaMout(i).Item("ALCTD_KB")) = True OrElse _
           ("04").Equals(outkaMout(i).Item("ALCTD_KB")) = True Then
            Return True
        End If

        Dim zaidr() As DataRow = Nothing
        zaidr = outkaMin.Select(String.Concat("OUTKA_NO_L = '", outkaMout(i).Item("OUTKA_NO_L"), "' AND ERR_FLG = '01'"))

        If zaidr.Length > 0 Then
            Return True
        End If

        '在庫引当起動
        Dim prm As LMFormData = Me.ShowHikiatePopupForHW(frm, ds, i)

        If prm.ReturnFlg = True Then
            Dim dt As DataTable = prm.ParamDataSet.Tables(LMControlC.LMC040C_TABLE_NM_OUT)
            '引当の戻り値をdsにセットする

            If dt.Rows.Count > 0 Then

                '出荷Mデータセットの生成
                'Call Me.SetDatasetOutKaMForHW(frm, ds, i, dt)
                If Me.SetDatasetOutKaMForHW(frm, ds, i, dt) = False Then
                    Return False
                    Exit Function
                End If

                '出荷Sデータセットの生成
                Call Me.SetDatasetOutKaSForHW(frm, ds, i, dt)

                '在庫データセットの生成
                Call Me.SetDatasetZaiTrsForHW(frm, ds, i, dt)

            End If

        Else

            'データセット設定(在庫)
            Call Me.SetDatasetOutMInForHW(frm, ds, outkaMout(i).Item("OUTKA_NO_L").ToString())

            MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, MyBase.GetMessageID, , (i + 1).ToString())

        End If

        Return True

    End Function

    ''' <summary>
    ''' 在庫引当起動(CSV引当)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowHikiatePopupForHW(ByVal frm As LMC010F, ByVal ds As DataSet, ByVal i As Integer) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim dsHiki As DataSet = New LMC040DS()
        Dim dtOutkaM As DataTable = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT)

        Dim dt As DataTable = dsHiki.Tables(LMControlC.LMC040C_TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim dt2 As DataTable = dsHiki.Tables(LMC040C.TABLE_NM_IN2)
        Dim dr2 As DataRow = Nothing

        'INデータテーブル設定（LMC040IN）
        With dr

            Dim hikiateFlg As String = "03"

            .Item("NRS_BR_CD") = dtOutkaM(i).Item("NRS_BR_CD")
            .Item("WH_CD") = dtOutkaM(i).Item("WH_CD")
            .Item("CUST_CD_L") = dtOutkaM(i).Item("CUST_CD_L")
            .Item("CUST_CD_M") = dtOutkaM(i).Item("CUST_CD_M")
            .Item("CUST_NM_L") = dtOutkaM(i).Item("CUST_NM_L")
            .Item("CUST_NM_M") = dtOutkaM(i).Item("CUST_NM_M")
            .Item("GOODS_CD_NRS") = dtOutkaM(i).Item("GOODS_CD_NRS")
            .Item("GOODS_CD_CUST") = String.Empty
            .Item("GOODS_NM") = dtOutkaM(i).Item("GOODS_NM")
            .Item("SERIAL_NO") = dtOutkaM(i).Item("SERIAL_NO")
            .Item("LOT_NO") = dtOutkaM(i).Item("LOT_NO")
            .Item("IRIME") = Me.FormatNumValue(dtOutkaM(i).Item("IRIME").ToString())
            .Item("IRIME_UT") = String.Empty
            .Item("ALCTD_KB") = dtOutkaM(i).Item("ALCTD_KB")
            .Item("NB_UT") = String.Empty
            .Item("STD_IRIME_UT") = String.Empty
            .Item("PKG_NB") = 0.ToString()
            .Item("ALCTD_NB") = Me.FormatNumValue(dtOutkaM(i).Item("ALCTD_NB").ToString())
            .Item("BACKLOG_NB") = Me.FormatNumValue(dtOutkaM(i).Item("BACKLOG_NB").ToString())
            .Item("ALCTD_QT") = Me.FormatNumValue(dtOutkaM(i).Item("ALCTD_QT").ToString())
            .Item("BACKLOG_QT") = Me.FormatNumValue(dtOutkaM(i).Item("BACKLOG_QT").ToString())
            .Item("PKG_NB") = Me.FormatNumValue(dtOutkaM(i).Item("PKG_NB").ToString())
            .Item("HASU") = Me.FormatNumValue(dtOutkaM(i).Item("OUTKA_HASU").ToString())
            .Item("KOSU") = Me.FormatNumValue(dtOutkaM(i).Item("OUTKA_TTL_NB").ToString())
            .Item("SURYO") = Me.FormatNumValue(dtOutkaM(i).Item("OUTKA_TTL_QT").ToString())
            .Item("KONSU") = ((Convert.ToDecimal(.Item("KOSU")) - Convert.ToDecimal(.Item("HASU"))) / Convert.ToDecimal(.Item("PKG_NB"))).ToString()

            .Item("HIKIATE_FLG") = hikiateFlg
            Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select( _
                                             String.Concat("NRS_BR_CD = '", .Item("NRS_BR_CD"), "'AND CUST_CD = '", .Item("CUST_CD_L"), "' AND SUB_KB = '02'"))

            If 0 < custDetailsDr.Length Then
                .Item("SORT_FLG") = custDetailsDr(0).Item("SET_NAIYO").ToString()
            Else
                .Item("SORT_FLG") = "00"
            End If

            .Item("OUTKA_PLAN_DATE") = dtOutkaM(i).Item("OUTKA_PLAN_DATE")

            If String.IsNullOrEmpty(dtOutkaM(i).Item("GOODS_CD_NRS_FROM").ToString()) = True Then
                .Item("TANINUSI_FLG") = "00"
            Else
                .Item("TANINUSI_FLG") = "01"
            End If

            .Item("OUTKA_S_CNT") = "0"
            .Item("PGID") = MyBase.GetPGID()

        End With

        dt.Rows.Add(dr)

        'INデータテーブル設定（LMC040IN2）
        Dim zaidr() As DataRow = Nothing
        zaidr = ds.Tables(LMC010C.TABLE_NM_ZAI_TRS).Select("ERR_FLG = '00'")
        Dim max As Integer = zaidr.Length - 1

        For j As Integer = 0 To max

            dr2 = dt2.NewRow()
            dr2.Item("NRS_BR_CD") = zaidr(j).Item("NRS_BR_CD")
            dr2.Item("ZAI_REC_NO") = zaidr(j).Item("ZAI_REC_NO")
            dr2.Item("ALCTD_NB") = zaidr(j).Item("ALCTD_NB")
            dr2.Item("ALLOC_CAN_NB") = zaidr(j).Item("ALLOC_CAN_NB")
            dr2.Item("ALCTD_QT") = zaidr(j).Item("ALCTD_QT")
            dr2.Item("ALLOC_CAN_QT") = zaidr(j).Item("ALLOC_CAN_QT")
            dt2.Rows.Add(dr2)

        Next

        prm.ParamDataSet = dsHiki

        'Pop起動
        Return Me.PopFormShow(prm, "LMC040")

    End Function

    ''' <summary>
    ''' データセット設定(出荷(中))(ハネウェルCSV引当対応)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">一括引当に使用するデータセット</param>
    ''' <param name="rowCount">現在ループカウント</param>
    ''' <param name="dtHiki">在庫引当の戻り値のDataTable</param>
    ''' <remarks></remarks>
    Private Function SetDatasetOutKaMForHW(ByVal frm As LMC010F, ByVal ds As DataSet, ByVal rowCount As Integer, ByVal dtHiki As DataTable) As Boolean
        'Private Sub SetDatasetOutKaMForHW(ByVal frm As LMC010F, ByVal ds As DataSet, ByVal rowCount As Integer, ByVal dtHiki As DataTable)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_OUTKA_M)
        Dim dr As DataRow = inTbl.NewRow()

        '既にセットしてある項目を参照する為にインスタンスを生成
        Dim outkaMDr As DataRow = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT).Rows(rowCount)

        Dim alctdNb As Decimal = 0
        Dim alctdQt As Decimal = 0
        Dim max As Integer = dtHiki.Rows.Count - 1
        Dim amari As Decimal = 0

        Dim nowKey As String = String.Empty

        With frm

            '自動引当の返り値分だけ登録条件をセットする
            For i As Integer = 0 To max

                alctdNb += Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_KOSU").ToString())
                alctdQt += Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_SURYO").ToString())
                '引当合計個数、引当合計数量の格納
                Me._HikiSumNb = Me._HikiSumNb + Convert.ToInt32(dtHiki.Rows(i).Item("HIKI_KOSU").ToString())
                Me._HikiSumQt = Me._HikiSumQt + Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_SURYO").ToString())

            Next

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            dr.Item("STD_IRIME_NB") = dtHiki.Rows(0).Item("STD_IRIME_NB")
            dr.Item("STD_WT_KGS") = dtHiki.Rows(0).Item("STD_WT_KGS")
            dr.Item("IRIME") = dtHiki.Rows(0).Item("IRIME")
            dr.Item("TARE_YN") = dtHiki.Rows(0).Item("TARE_YN")
            dr.Item("PKG_UT") = dtHiki.Rows(0).Item("PKG_UT")
            dr.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()

            dr.Item("OUTKA_NO_L") = outkaMDr.Item("OUTKA_NO_L")
            dr.Item("OUTKA_NO_M") = outkaMDr.Item("OUTKA_NO_M")

            nowKey = String.Concat(outkaMDr.Item("OUTKA_NO_L").ToString(), outkaMDr.Item("OUTKA_NO_M").ToString())

            If nowKey.Equals(Me._PreKey) = False AndAlso String.IsNullOrEmpty(Me._PreKey) = False Then
                '出荷管理番号(大+中)が前行と異なる場合、引当合計個数、引当合計数量を初期化し計算する
                '2014.04.02 修正START
                'Me._HikiSumNb = 0
                'Me._HikiSumQt = 0
                Me._HikiSumNb = Convert.ToInt32(alctdNb)
                Me._HikiSumQt = alctdQt
                '2014.04.02 修正END
                dr.Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(outkaMDr.Item("ALCTD_NB").ToString()) + alctdNb)
                dr.Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(outkaMDr.Item("ALCTD_QT").ToString()) + alctdQt)
                dr.Item("BACKLOG_NB") = (Convert.ToDecimal(outkaMDr.Item("BACKLOG_NB")) - alctdNb).ToString()
                dr.Item("BACKLOG_QT") = (Convert.ToDecimal(outkaMDr.Item("BACKLOG_QT")) - alctdQt).ToString()
            Else
                '出荷管理番号(大+中)が前行と同じ場合、引当合計個数、引当合計数量を足しこみ計算する
                dr.Item("ALCTD_NB") = Convert.ToString(Convert.ToDecimal(outkaMDr.Item("ALCTD_NB").ToString()) + Me._HikiSumNb)
                dr.Item("ALCTD_QT") = Convert.ToString(Convert.ToDecimal(outkaMDr.Item("ALCTD_QT").ToString()) + Me._HikiSumQt)
                dr.Item("BACKLOG_NB") = (Convert.ToDecimal(outkaMDr.Item("BACKLOG_NB")) - Me._HikiSumNb).ToString()
                dr.Item("BACKLOG_QT") = (Convert.ToDecimal(outkaMDr.Item("BACKLOG_QT")) - Me._HikiSumQt).ToString()

            End If

            '引当残個数または引当残数量が　0より小さい場合はエラー
            If Convert.ToDecimal(dr.Item("BACKLOG_NB")) < 0 OrElse Convert.ToDecimal(dr.Item("BACKLOG_QT")) < 0 Then
                Return False
                Exit Function
            End If

            dr.Item("OUTKA_M_PKG_NB") = (Math.Floor(Convert.ToDecimal(outkaMDr.Item("OUTKA_TTL_NB")) / Convert.ToDecimal(outkaMDr.Item("PKG_NB")))).ToString()
            amari = Convert.ToDecimal(outkaMDr.Item("OUTKA_TTL_NB")) Mod Convert.ToDecimal(outkaMDr.Item("PKG_NB"))
            If 0 < amari Then
                dr.Item("OUTKA_M_PKG_NB") = Convert.ToString(Convert.ToDecimal(dr.Item("OUTKA_M_PKG_NB")) + 1)
            End If

            inTbl.Rows.Add(dr)

            '主キーのセット
            Me._PreKey = nowKey

            '持ちまわっているデータセットにつめなおす
            ds.Tables(LMC010C.TABLE_NM_OUTKA_M).ImportRow(inTbl.Rows(0))

        End With

        Return True

    End Function

    ''' <summary>
    ''' データセット設定(出荷(小))(ハネウェルCSV引当対応)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">一括引当に使用するデータセット</param>
    ''' <param name="rowCount">現在ループカウント</param>
    ''' <param name="dtHiki">在庫引当の戻り値のDataTable</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetOutKaSForHW(ByVal frm As LMC010F, ByVal ds As DataSet, ByVal rowCount As Integer, ByVal dtHiki As DataTable)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMC010C.TABLE_NM_OUTKA_S)
        Dim dr As DataRow = inTbl.NewRow()

        Dim mGoodsDrs As DataRow() = Nothing
        Dim goodsCdNrs As String = String.Empty
        Dim stdWtKgs As Decimal = 0
        Dim stdIrimeNb As Decimal = 0
        Dim irime As String = String.Empty
        Dim smplFlg As String = String.Empty

        '既にセットしてある項目を参照する為にインスタンスを生成
        Dim outkaMDr As DataRow = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT)(rowCount)
        Dim outkaNoS As Integer = (Convert.ToInt32(Me.FormatNumValue(outkaMDr.Item("OUTKA_NO_S").ToString())) + 1)

        Dim outSDr() As DataRow = Nothing
        Dim outSmax As Integer = 0
        Dim alctdCanNb As String = String.Empty
        Dim alctdCanQt As String = String.Empty

        Dim zaiMATOMEds As DataSet = Nothing
        Dim outSDr2() As DataRow = Nothing
        Dim insRows As DataRow = Nothing
        Dim zaiMax As Integer = 0
        Dim hikiKosu As Decimal = 0
        Dim hikiSuryo As Decimal = 0

        'START 2013/3/4 CSV引当対応（出荷S、PK重複エラー修正）
        Dim outSDrs As DataRow() = Nothing
        'END  2013/3/4 CSV引当対応

        With frm

            '自動引当の返り値分だけ登録条件をセットする
            Dim max As Integer = dtHiki.Rows.Count - 1
            For i As Integer = 0 To max

                '別インスタンスのデータロウを空にする
                inTbl.Clear()

                dr.Item("NRS_BR_CD") = outkaMDr.Item("NRS_BR_CD")
                dr.Item("OUTKA_NO_L") = outkaMDr.Item("OUTKA_NO_L")
                dr.Item("OUTKA_NO_M") = outkaMDr.Item("OUTKA_NO_M")
                dr.Item("OUTKA_NO_S") = Format(outkaNoS, "000")

                'START 2013/3/4 CSV引当対応（出荷S、PK重複エラー修正）
                outSDrs = ds.Tables(LMC010C.TABLE_NM_OUTKA_S).Select("OUTKA_NO_L = '" & dr.Item("OUTKA_NO_L").ToString & _
                                                             "' AND OUTKA_NO_M = '" & dr.Item("OUTKA_NO_M").ToString & "' ", "OUTKA_NO_S")
                If outSDrs.Length > 0 Then
                    outkaNoS = Convert.ToInt32(Me.FormatNumValue(outSDrs(outSDrs.Length - 1).Item("OUTKA_NO_S").ToString())) + 1
                    dr.Item("OUTKA_NO_S") = Format(outkaNoS, "000")
                End If
                'END  2013/3/4 CSV引当対応

                dr.Item("TOU_NO") = dtHiki.Rows(i).Item("TOU_NO")
                dr.Item("SITU_NO") = dtHiki.Rows(i).Item("SITU_NO")
                dr.Item("ZONE_CD") = dtHiki.Rows(i).Item("ZONE_CD")
                dr.Item("LOCA") = dtHiki.Rows(i).Item("LOCA")
                dr.Item("LOT_NO") = dtHiki.Rows(i).Item("LOT_NO")
                dr.Item("SERIAL_NO") = dtHiki.Rows(i).Item("SERIAL_NO")
                dr.Item("OUTKA_TTL_NB") = outkaMDr.Item("BACKLOG_NB")
                dr.Item("OUTKA_TTL_QT") = outkaMDr.Item("BACKLOG_QT")
                dr.Item("ZAI_REC_NO") = dtHiki.Rows(i).Item("ZAI_REC_NO")
                dr.Item("INKA_NO_L") = dtHiki.Rows(i).Item("INKA_NO_L")
                dr.Item("INKA_NO_M") = dtHiki.Rows(i).Item("INKA_NO_M")
                dr.Item("INKA_NO_S") = dtHiki.Rows(i).Item("INKA_NO_S")
                dr.Item("ZAI_UPD_FLAG") = "00"
                dr.Item("IRIME") = dtHiki.Rows(i).Item("IRIME")
                dr.Item("GOODS_CD_NRS") = dtHiki.Rows(i).Item("GOODS_CD_NRS")
                dr.Item("GOODS_COND_KB_1") = dtHiki.Rows(i).Item("GOODS_COND_KB_1")
                dr.Item("GOODS_COND_KB_2") = dtHiki.Rows(i).Item("GOODS_COND_KB_2")
                dr.Item("REMARK_OUT") = dtHiki.Rows(i).Item("REMARK_OUT")
                dr.Item("REMARK") = dtHiki.Rows(i).Item("REMARK")
                dr.Item("INKO_DATE") = dtHiki.Rows(i).Item("INKO_DATE")
                dr.Item("LT_DATE") = dtHiki.Rows(i).Item("LT_DATE")
                dr.Item("OFB_KB") = dtHiki.Rows(i).Item("OFB_KB")
                dr.Item("SPD_KB") = dtHiki.Rows(i).Item("SPD_KB")
                dr.Item("RSV_NO") = dtHiki.Rows(i).Item("RSV_NO")
                dr.Item("GOODS_CRT_DATE") = dtHiki.Rows(i).Item("GOODS_CRT_DATE")
                dr.Item("ALLOC_PRIORITY") = dtHiki.Rows(i).Item("ALLOC_PRIORITY")
                dr.Item("INKO_PLAN_DATE") = dtHiki.Rows(i).Item("INKO_PLAN_DATE")
                dr.Item("INKA_DATE_KANRI_KB") = dtHiki.Rows(i).Item("INKA_DATE_KANRI_KB")

                outSDr = ds.Tables(LMC010C.TABLE_NM_OUTKA_S).Select(String.Concat("ZAI_REC_NO = '", dr.Item("ZAI_REC_NO").ToString(), "' AND ", _
                                                                                  "ALCTD_CAN_NB_HOZON <> '' AND ", _
                                                                                  "ERR_FLG <> '01'"))
                outSmax = outSDr.Length - 1
                If -1 < outSmax Then
                    alctdCanNb = outSDr(0).Item("ALCTD_CAN_NB_HOZON").ToString()
                    alctdCanQt = outSDr(0).Item("ALCTD_CAN_QT_HOZON").ToString()
                    For j As Integer = 0 To outSmax
                        If Convert.ToDecimal(outSDr(j).Item("ALCTD_CAN_NB_HOZON")) < Convert.ToDecimal(alctdCanNb) Then
                            alctdCanNb = outSDr(j).Item("ALCTD_CAN_NB_HOZON").ToString()
                            alctdCanQt = outSDr(j).Item("ALCTD_CAN_QT_HOZON").ToString()
                        End If
                    Next
                    dr("ALCTD_CAN_NB") = Convert.ToString(Convert.ToDecimal(alctdCanNb) - Convert.ToDecimal(dtHiki(i).Item("HIKI_KOSU")))
                    dr("ALCTD_CAN_QT") = Convert.ToString(Convert.ToDecimal(alctdCanQt) - Convert.ToDecimal(dtHiki(i).Item("HIKI_SURYO")))
                Else
                    dr("ALCTD_CAN_NB") = dtHiki(i).Item("ALLOC_CAN_NB")
                    dr("ALCTD_CAN_QT") = dtHiki(i).Item("ALLOC_CAN_QT")
                End If

                ds.Tables(LMC010C.TABLE_NM_IN_MATOME).Clear()
                insRows = ds.Tables(LMC010C.TABLE_NM_IN_MATOME).NewRow
                insRows.Item("NRS_BR_CD") = dr("NRS_BR_CD")
                insRows.Item("GOODS_CD_NRS") = dtHiki.Rows(i).Item("GOODS_CD_NRS")
                insRows.Item("LOT_NO") = dtHiki.Rows(i).Item("LOT_NO")
                insRows.Item("IRIME") = dtHiki.Rows(i).Item("IRIME")
                insRows.Item("TOU_NO") = dtHiki.Rows(i).Item("TOU_NO")
                insRows.Item("SITU_NO") = dtHiki.Rows(i).Item("SITU_NO")
                insRows.Item("ZONE_CD") = dtHiki.Rows(i).Item("ZONE_CD")
                insRows.Item("LOCA") = dtHiki.Rows(i).Item("LOCA")
                insRows.Item("GOODS_COND_KB_1") = dtHiki.Rows(i).Item("GOODS_COND_KB_1")
                insRows.Item("GOODS_COND_KB_2") = dtHiki.Rows(i).Item("GOODS_COND_KB_2")
                insRows.Item("REMARK_OUT") = dtHiki.Rows(i).Item("REMARK_OUT")
                insRows.Item("REMARK") = dtHiki.Rows(i).Item("REMARK")
                insRows.Item("INKO_DATE") = dtHiki.Rows(i).Item("INKO_DATE")
                insRows.Item("LT_DATE") = dtHiki.Rows(i).Item("LT_DATE")
                insRows.Item("OFB_KB") = dtHiki.Rows(i).Item("OFB_KB")
                insRows.Item("SPD_KB") = dtHiki.Rows(i).Item("SPD_KB")
                insRows.Item("RSV_NO") = dtHiki.Rows(i).Item("RSV_NO")
                insRows.Item("SERIAL_NO") = dtHiki.Rows(i).Item("SERIAL_NO")
                insRows.Item("GOODS_CRT_DATE") = dtHiki.Rows(i).Item("GOODS_CRT_DATE")
                insRows.Item("ALLOC_PRIORITY") = dtHiki.Rows(i).Item("ALLOC_PRIORITY")
                insRows.Item("INKO_PLAN_DATE") = dtHiki.Rows(i).Item("INKO_PLAN_DATE")
                insRows.Item("INKA_DATE_KANRI_KB") = dtHiki.Rows(i).Item("INKA_DATE_KANRI_KB")

                'データセットに追加
                ds.Tables(LMC010C.TABLE_NM_IN_MATOME).Rows.Add(insRows)

                '取得した値から各個数・数量のまとめた値を求める
                zaiMATOMEds = Me.SelectZaiDataMATOME(frm, ds)

                zaiMax = zaiMATOMEds.Tables(LMC010C.TABLE_NM_OUT_MATOME).Rows.Count - 1
                If 0 <= zaiMax Then

                    'まとめ対象の場合
                    If ("01").Equals(insRows.Item("INKA_DATE_KANRI_KB").ToString) = True Then
                        outSDr2 = ds.Tables(LMC010C.TABLE_NM_OUTKA_S).Select(String.Concat("NRS_BR_CD = '", insRows.Item("NRS_BR_CD").ToString, "' AND ", _
                                                                                           "GOODS_CD_NRS = '", insRows.Item("GOODS_CD_NRS").ToString, "' AND ", _
                                                                                           "LOT_NO = '", insRows.Item("LOT_NO").ToString, "' AND ", _
                                                                                           "IRIME = '", insRows.Item("IRIME").ToString, "' AND ", _
                                                                                           "TOU_NO = '", insRows.Item("TOU_NO").ToString, "' AND ", _
                                                                                           "SITU_NO = '", insRows.Item("SITU_NO").ToString, "' AND ", _
                                                                                           "ZONE_CD = '", insRows.Item("ZONE_CD").ToString, "' AND ", _
                                                                                           "LOCA = '", insRows.Item("LOCA").ToString, "' AND ", _
                                                                                           "GOODS_COND_KB_1 = '", insRows.Item("GOODS_COND_KB_1").ToString, "' AND ", _
                                                                                           "GOODS_COND_KB_2 = '", insRows.Item("GOODS_COND_KB_2").ToString, "' AND ", _
                                                                                           "REMARK_OUT = '", insRows.Item("REMARK_OUT").ToString, "' AND ", _
                                                                                           "REMARK = '", insRows.Item("REMARK").ToString, "' AND ", _
                                                                                           "LT_DATE = '", insRows.Item("LT_DATE").ToString, "' AND ", _
                                                                                           "OFB_KB = '", insRows.Item("OFB_KB").ToString, "' AND ", _
                                                                                           "SPD_KB = '", insRows.Item("SPD_KB").ToString, "' AND ", _
                                                                                           "RSV_NO = '", insRows.Item("RSV_NO").ToString, "' AND ", _
                                                                                           "SERIAL_NO = '", insRows.Item("SERIAL_NO").ToString, "' AND ", _
                                                                                           "GOODS_CRT_DATE = '", insRows.Item("GOODS_CRT_DATE").ToString, "' AND ", _
                                                                                           "ALLOC_PRIORITY = '", insRows.Item("ALLOC_PRIORITY").ToString, "'"))
                    ElseIf String.IsNullOrEmpty(insRows.Item("INKO_DATE").ToString) = False Then
                        outSDr2 = ds.Tables(LMC010C.TABLE_NM_OUTKA_S).Select(String.Concat("NRS_BR_CD = '", insRows.Item("NRS_BR_CD").ToString, "' AND ", _
                                                                                           "GOODS_CD_NRS = '", insRows.Item("GOODS_CD_NRS").ToString, "' AND ", _
                                                                                           "LOT_NO = '", insRows.Item("LOT_NO").ToString, "' AND ", _
                                                                                           "IRIME = '", insRows.Item("IRIME").ToString, "' AND ", _
                                                                                           "TOU_NO = '", insRows.Item("TOU_NO").ToString, "' AND ", _
                                                                                           "SITU_NO = '", insRows.Item("SITU_NO").ToString, "' AND ", _
                                                                                           "ZONE_CD = '", insRows.Item("ZONE_CD").ToString, "' AND ", _
                                                                                           "LOCA = '", insRows.Item("LOCA").ToString, "' AND ", _
                                                                                           "GOODS_COND_KB_1 = '", insRows.Item("GOODS_COND_KB_1").ToString, "' AND ", _
                                                                                           "GOODS_COND_KB_2 = '", insRows.Item("GOODS_COND_KB_2").ToString, "' AND ", _
                                                                                           "REMARK_OUT = '", insRows.Item("REMARK_OUT").ToString, "' AND ", _
                                                                                           "REMARK = '", insRows.Item("REMARK").ToString, "' AND ", _
                                                                                           "INKO_DATE = '", insRows.Item("INKO_DATE").ToString, "' AND ", _
                                                                                           "LT_DATE = '", insRows.Item("LT_DATE").ToString, "' AND ", _
                                                                                           "OFB_KB = '", insRows.Item("OFB_KB").ToString, "' AND ", _
                                                                                           "SPD_KB = '", insRows.Item("SPD_KB").ToString, "' AND ", _
                                                                                           "RSV_NO = '", insRows.Item("RSV_NO").ToString, "' AND ", _
                                                                                           "SERIAL_NO = '", insRows.Item("SERIAL_NO").ToString, "' AND ", _
                                                                                           "GOODS_CRT_DATE = '", insRows.Item("GOODS_CRT_DATE").ToString, "' AND ", _
                                                                                           "ALLOC_PRIORITY = '", insRows.Item("ALLOC_PRIORITY").ToString, "'"))
                    Else
                        outSDr2 = ds.Tables(LMC010C.TABLE_NM_OUTKA_S).Select(String.Concat("NRS_BR_CD = '", insRows.Item("NRS_BR_CD").ToString, "' AND ", _
                                                                                           "GOODS_CD_NRS = '", insRows.Item("GOODS_CD_NRS").ToString, "' AND ", _
                                                                                           "LOT_NO = '", insRows.Item("LOT_NO").ToString, "' AND ", _
                                                                                           "IRIME = '", insRows.Item("IRIME").ToString, "' AND ", _
                                                                                           "TOU_NO = '", insRows.Item("TOU_NO").ToString, "' AND ", _
                                                                                           "SITU_NO = '", insRows.Item("SITU_NO").ToString, "' AND ", _
                                                                                           "ZONE_CD = '", insRows.Item("ZONE_CD").ToString, "' AND ", _
                                                                                           "LOCA = '", insRows.Item("LOCA").ToString, "' AND ", _
                                                                                           "GOODS_COND_KB_1 = '", insRows.Item("GOODS_COND_KB_1").ToString, "' AND ", _
                                                                                           "GOODS_COND_KB_2 = '", insRows.Item("GOODS_COND_KB_2").ToString, "' AND ", _
                                                                                           "REMARK_OUT = '", insRows.Item("REMARK_OUT").ToString, "' AND ", _
                                                                                           "REMARK = '", insRows.Item("REMARK").ToString, "' AND ", _
                                                                                           "INKO_PLAN_DATE = '", insRows.Item("INKO_PLAN_DATE").ToString, "' AND ", _
                                                                                           "LT_DATE = '", insRows.Item("LT_DATE").ToString, "' AND ", _
                                                                                           "OFB_KB = '", insRows.Item("OFB_KB").ToString, "' AND ", _
                                                                                           "SPD_KB = '", insRows.Item("SPD_KB").ToString, "' AND ", _
                                                                                           "RSV_NO = '", insRows.Item("RSV_NO").ToString, "' AND ", _
                                                                                           "SERIAL_NO = '", insRows.Item("SERIAL_NO").ToString, "' AND ", _
                                                                                           "GOODS_CRT_DATE = '", insRows.Item("GOODS_CRT_DATE").ToString, "' AND ", _
                                                                                           "ALLOC_PRIORITY = '", insRows.Item("ALLOC_PRIORITY").ToString, "'"))
                    End If

                    zaiMax = outSDr2.Length - 1
                    hikiKosu = 0
                    hikiSuryo = 0
                    For j As Integer = 0 To zaiMax
                        hikiKosu = hikiKosu + Convert.ToDecimal(outSDr2(j).Item("ALCTD_NB").ToString)
                        hikiSuryo = hikiSuryo + Convert.ToDecimal(outSDr2(j).Item("ALCTD_QT").ToString)
                    Next
                    dr("ALCTD_CAN_NB") = Convert.ToDecimal(zaiMATOMEds.Tables(LMC010C.TABLE_NM_OUT_MATOME).Rows(0).Item("ALLOC_CAN_NB").ToString) - _
                                         hikiKosu - _
                                         Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_KOSU").ToString())
                    dr("ALCTD_CAN_QT") = Convert.ToDecimal(zaiMATOMEds.Tables(LMC010C.TABLE_NM_OUT_MATOME).Rows(0).Item("ALLOC_CAN_QT").ToString) - _
                                         hikiSuryo - _
                                         Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_SURYO").ToString())
                End If

                dr("ALCTD_NB") = dtHiki.Rows(i).Item("HIKI_KOSU")
                dr("ALCTD_QT") = dtHiki.Rows(i).Item("HIKI_SURYO")
                dr("ALCTD_CAN_NB_HOZON") = dr("ALCTD_CAN_NB")
                dr("ALCTD_CAN_QT_HOZON") = dr("ALCTD_CAN_QT")

                irime = outkaMDr.Item("IRIME").ToString()

                dr("IRIME") = irime

                '標準重量を取得
                stdWtKgs = Convert.ToDecimal(dtHiki.Rows(i).Item("STD_WT_KGS").ToString())

                '標準入目
                stdIrimeNb = Convert.ToDecimal(dtHiki.Rows(i).Item("STD_IRIME_NB").ToString())

                dr("BETU_WT") = Convert.ToDecimal(irime) * stdWtKgs / stdIrimeNb
                dr("COA_FLAG") = "00"

                If "03".Equals(outkaMDr.Item("ALCTD_KB").ToString()) = True Then
                    smplFlg = "01"
                Else
                    smplFlg = "00"
                End If
                dr("SMPL_FLAG") = smplFlg

                inTbl.Rows.Add(dr)

                '持ちまわっているデータセットにつめなおす
                ds.Tables(LMC010C.TABLE_NM_OUTKA_S).ImportRow(inTbl.Rows(0))

                outkaNoS += 1

            Next

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(在庫)(ハネウェルCSV引当対応)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">一括引当に使用するデータセット</param>
    ''' <param name="rowCount">現在ループカウント</param>
    ''' <param name="dtHiki">在庫引当の戻り値のDataTable</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetZaiTrsForHW(ByVal frm As LMC010F, ByVal ds As DataSet, ByVal rowCount As Integer, ByVal dtHiki As DataTable)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim dr As DataRow = Nothing

        '既にセットしてある項目を参照する為にインスタンスを生成
        Dim outkaMDr As DataRow = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT)(rowCount)

        Dim alctdKb As String = outkaMDr.Item("ALCTD_KB").ToString()
        Dim alctdNb As Decimal = 0
        Dim allocCanNb As Decimal = 0
        Dim alctdQt As Decimal = 0
        Dim allocCanQt As Decimal = 0

        With frm

            '自動引当の返り値分だけ登録条件をセットする
            Dim max As Integer = dtHiki.Rows.Count - 1
            For i As Integer = 0 To max

                '別インスタンスのデータロウを空にする
                dr = ds.Tables(LMC010C.TABLE_NM_ZAI_TRS).NewRow()

                dr.Item("NRS_BR_CD") = outkaMDr.Item("NRS_BR_CD")
                dr.Item("ZAI_REC_NO") = dtHiki.Rows(i).Item("ZAI_REC_NO")

                If "01".Equals(alctdKb) = True Then
                    '個数の場合
                    alctdNb = Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_KOSU").ToString())
                    allocCanNb = Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_KOSU").ToString())
                    alctdQt = Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_SURYO").ToString())
                    allocCanQt = Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_SURYO").ToString())

                ElseIf "02".Equals(alctdKb) = True Then
                    '数量の場合
                    alctdNb = Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_KOSU").ToString())
                    allocCanNb = Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_KOSU").ToString())
                    alctdQt = Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_SURYO").ToString())
                    allocCanQt = Convert.ToDecimal(dtHiki.Rows(i).Item("HIKI_SURYO").ToString())

                ElseIf "03".Equals(alctdKb) = True Then
                    '小分けの場合
                    alctdNb = 1
                    allocCanNb = 1
                    alctdQt = Convert.ToDecimal(dtHiki.Rows(i).Item("IRIME").ToString())
                    allocCanQt = Convert.ToDecimal(dtHiki.Rows(i).Item("IRIME").ToString())

                Else
                    'サンプルの場合
                    alctdNb = 0
                    allocCanNb = 0
                    alctdQt = 0
                    allocCanQt = 0

                End If

                dr.Item("ALCTD_NB") = alctdNb.ToString()
                dr.Item("ALLOC_CAN_NB") = allocCanNb.ToString()
                dr.Item("ALCTD_QT") = alctdQt.ToString()
                dr.Item("ALLOC_CAN_QT") = allocCanQt.ToString()
                dr.Item("ALCTD_KB") = alctdKb
                dr.Item("SYS_UPD_DATE") = dtHiki.Rows(i).Item("SYS_UPD_DATE").ToString()
                dr.Item("SYS_UPD_TIME") = dtHiki.Rows(i).Item("SYS_UPD_TIME").ToString()
                dr.Item("OUTKA_NO_L") = outkaMDr.Item("OUTKA_NO_L")
                dr.Item("ERR_FLG") = "00"

                '持ちまわっているデータセットにつめなおす
                ds.Tables(LMC010C.TABLE_NM_ZAI_TRS).Rows.Add(dr)

            Next

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(在庫)(ハネウェルCSV引当対応)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">一括引当に使用するデータセット</param>
    ''' <param name="strOutkaNoL">対象データの出荷管理番号（大）</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetOutMInForHW(ByVal frm As LMC010F, ByVal ds As DataSet, ByVal strOutkaNoL As String)

        Dim dr As DataRow() = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_IN).Select(String.Concat("OUTKA_NO_L = '", strOutkaNoL, "'"))
        If 0 < dr.Length Then
            dr(0).Item("ERR_FLG") = "01"
        End If

        Dim zaidr() As DataRow = Nothing
        zaidr = ds.Tables(LMC010C.TABLE_NM_ZAI_TRS).Select(String.Concat("OUTKA_NO_L = '", strOutkaNoL, "'"))
        Dim max As Integer = zaidr.Length - 1
        For i As Integer = 0 To max
            zaidr(i).Item("ERR_FLG") = "01"
        Next

        Dim outSdr() As DataRow = Nothing
        outSdr = ds.Tables(LMC010C.TABLE_NM_OUTKA_S).Select(String.Concat("OUTKA_NO_L = '", strOutkaNoL, "'"))
        max = outSdr.Length - 1
        For i As Integer = 0 To max
            outSdr(i).Item("ERR_FLG") = "01"
        Next

    End Sub

    ''' <summary>
    ''' 日立物流出荷音声データCSV作成処理(ハネウェルCSV引当対応)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub DicCsvMakeForHW(ByVal frm As LMC010F, ByVal prm As LMFormData, ByVal ds As DataSet)

        Dim setDs As DataSet = New LMC830DS
        Dim setDt As DataTable = setDs.Tables("LMC830IN")
        Dim setDr As DataRow = setDt.NewRow()
        Dim sysDtTm As String() = MyBase.GetSystemDateTime()

        Dim dataDt As DataTable = ds.Tables(LMC010C.TABLE_NM_OUTKA_M_OUT)

        For i As Integer = 0 To dataDt.Rows.Count - 1

            Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select( _
                                             String.Concat("NRS_BR_CD = '", frm.cmbEigyo.SelectedValue, _
                                                         "' AND CUST_CD = '", dataDt(i).Item("CUST_CD_L"), _
                                                         "' AND SUB_KB = '39'"))

            If 0 < custDetailsDr.Length Then

                setDt.Clear()
                setDr.Item("NRS_BR_CD") = dataDt(i).Item("NRS_BR_CD")
                setDr.Item("OUTKA_NO_L") = dataDt(i).Item("OUTKA_NO_L")
                setDr.Item("WH_CD") = dataDt(i).Item("WH_CD")
                setDr.Item("TOU_HAN_FLG") = "00"
                setDr.Item("SYS_DATE") = sysDtTm(0)
                setDr.Item("SYS_TIME") = Mid(sysDtTm(1), 1, 6)
                setDr.Item("COMPNAME") = Environment.MachineName
                setDr.Item("RPT_FLG") = "05"
                setDt.Rows.Add(setDr)

                'CSV出力処理呼出
                prm.ParamDataSet = setDs
                LMFormNavigate.NextFormNavigate(Me, "LMC830", prm)

            End If
        Next

    End Sub

#Region "WIT対応(+ハネウェル?)"
    ''' <summary>
    ''' データセットの不要行の削除
    ''' </summary>
    ''' <param name="dtEscapeOM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetSumDs(ByVal dtEscapeOM As DataTable) As DataTable

        '============= OUTKA_M_OUT ============

        '絞込み済出荷Mのデータテーブル
        Dim dtNewOM As DataTable = dtEscapeOM.Clone()

        'ソート格納
        Dim drSelM() As DataRow = Nothing

        '判定キー
        Dim bShift As Boolean = True
        Dim sCutKey As String = String.Empty
        Dim sLogKey As String = String.Empty

        'カウンタ
        Dim iRowCnt As Integer = 0

        For Each cpRow As DataRow In dtEscapeOM.Rows
            sCutKey = cpRow.Item("OUTKA_NO_M").ToString()

            If iRowCnt = 0 Then
                bShift = True
            ElseIf iRowCnt <> 0 AndAlso sCutKey.Equals(sLogKey) = False Then
                bShift = True
            Else
                'ログキーの入替え
                bShift = False
            End If


            If bShift = True Then
                'Rowの入替え
                drSelM = dtEscapeOM.Select(String.Concat("OUTKA_NO_M = ", sCutKey), "ALCTD_NB DESC")

                dtNewOM.ImportRow(drSelM(0))
                sLogKey = sCutKey
            End If

            iRowCnt += 1
        Next
        '============= OUTKA_M_OUT ============

        Return dtNewOM

    End Function

#End Region

#Region "入力チェック（ハネウェルCSV引当）"

    ''' <summary>
    ''' CSVファイルの項目チェック
    ''' </summary>
    ''' <param name="temp"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsCSVDataCheck(ByVal temp As String(), ByVal rowCnt As Integer) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim rtnFlg As Boolean = True

        '項目数チェック
        If temp.Length < 2 Then

            '2017/09/25 修正 李↓
            MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E479", New String() {String.Concat(rowCnt, lgm.Selector({"行目", "Line", "째줄", "中国語"}))})
            '2017/09/25 修正 李↑

            Return False
        End If

        Dim strItemNm As String = String.Empty
        Dim OUTKA_NO_LM As String = temp(0).Trim(Chr(34))
        Dim SERIAL_NO As String = temp(1).Trim(Chr(34))

        'CSV項目チェック
        '2017/09/25 修正 李↓
        strItemNm = lgm.Selector({"出荷管理番号", "Shipment control number", "출하관리번호", "中国語"})
        '2017/09/25 修正 李↑

        If Me._V.IsCSVHissuCheck(OUTKA_NO_LM) = False Then

            '2017/09/25 修正 李↓
            MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E473", New String() {strItemNm, String.Concat(rowCnt, lgm.Selector({"行目", "Line", "째줄", "中国語"}))})
            '2017/09/25 修正 李↑

            rtnFlg = False
        End If

        If Me._V.IsCSVForbiddenWordsCheck(OUTKA_NO_LM) = False Then

            '2017/09/25 修正 李↓
            MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E478", New String() {strItemNm, String.Concat(rowCnt, lgm.Selector({"行目", "Line", "째줄", "中国語"}))})
            '2017/09/25 修正 李↑

            rtnFlg = False
        End If

        If Me._V.IsCSVFullByteCheck(OUTKA_NO_LM, 12) = False Then

            '2017/09/25 修正 李↓
            MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E518", New String() {strItemNm, String.Concat(rowCnt, lgm.Selector({"行目", "Line", "째줄", "中国語"}))})
            '2017/09/25 修正 李↑

            rtnFlg = False
        End If

        '2017/09/25 修正 李↓
        strItemNm = lgm.Selector({"シリアル番号", "Sirial No", "시리얼번호", "中国語"})
        '2017/09/25 修正 李↑

        If Me._V.IsCSVForbiddenWordsCheck(SERIAL_NO) = False Then

            '2017/09/25 修正 李↓
            MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E478", New String() {strItemNm, String.Concat(rowCnt, lgm.Selector({"行目", "Line", "째줄", "中国語"}))})
            '2017/09/25 修正 李↑

            rtnFlg = False
        End If

        If Me._V.IsCSVByteCheck(SERIAL_NO, 40) = False Then

            '2017/09/25 修正 李↓
            MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E474", New String() {strItemNm, String.Concat(rowCnt, lgm.Selector({"行目", "Line", "째줄", "中国語"}))})
            '2017/09/25 修正 李↑

            rtnFlg = False
        End If

        Return rtnFlg

    End Function

#End Region

#End Region
    'END KIM 20120920 倉庫システムVer2.0 特定荷主対応

#If True Then   ' ADD 2018/11/02 依頼番号 : 002192   【LMS】荷主ごと_入庫日・出荷日の初期値設定


#Region "営業日取得"
    '要望番号2690 前営業日・翌営業日対応
    ''' <summary>
    ''' 営業日取得
    ''' </summary>
    ''' <param name="sStartDay"></param>
    ''' <param name="iBussinessDays"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetBussinessDay(ByVal sStartDay As String, ByVal iBussinessDays As Integer) As DateTime
        'sStartDate     ：基準日（YYYYMMDD形式）
        'iBussinessDays ：基準日からの営業日数（前々営業日の場合は-2、前営業日の場合は-1、翌営業日の場合は+1、翌々営業日の場合は+2）
        '戻り値         ：求めた営業日（YYYY/MM/DD形式）

        'スラッシュを付加して日付型に変更
        Dim dBussinessDate As DateTime = Convert.ToDateTime((Convert.ToInt32(sStartDay)).ToString("0000/00/00"))

        For i As Integer = 1 To System.Math.Abs(iBussinessDays)  'マイナス値に対応するため絶対値指定

            '基準日からの営業日数分、Doループを繰り返す
            Do
                '日付加算
                If iBussinessDays > 0 Then
                    dBussinessDate = dBussinessDate.AddDays(1)      '翌営業日
                Else
                    dBussinessDate = dBussinessDate.AddDays(-1)     '前営業日
                End If

                If Weekday(dBussinessDate) = 1 OrElse Weekday(dBussinessDate) = 7 Then
                Else
                    '土日でない場合

                    '該当する日付が休日マスタに存在するか？
                    Dim sBussinessDate As String = Format(dBussinessDate, "yyyyMMdd")
                    Dim holDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.HOL).Select(" SYS_DEL_FLG = '0' AND HOL = '" & sBussinessDate & "'")
                    If holDr.Count = 0 Then
                        '休日マスタに存在しない場合、dBussinessDateが求める日
                        Exit Do
                    End If

                End If
            Loop
        Next

        Return dBussinessDate

    End Function
#End Region

#End If

#Region "FFEM オフライン品 依頼書未出力メッセージ表示用"

    ''' <summary>
    ''' FFEM オフライン品 運用営業所コードか否かの判定
    ''' </summary>
    ''' <param name="nrsBrCd"></param>
    ''' <returns></returns>
    Private Function IsFFEM_OfflineNrsBrCd(ByVal nrsBrCd As String) As Boolean

        Dim drNrsBr As DataRow
        drNrsBr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LM.Base.LMUserInfoManager.GetNrsBrCd().ToString() & "'"))(0)

        If drNrsBr.Item("LOCK_FLG").ToString.Equals("01") Then
            ' LMSレンタル先(NRS営業所以外) の場合
            Dim whereKbnN018 As String =
                String.Concat(
                    "    KBN_GROUP_CD = 'N018'",
                    "AND KBN_NM5 = '", nrsBrCd, "'",
                    "AND SYS_DEL_FLG = '0'")
            Dim drKbnN018 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(whereKbnN018)
            If drKbnN018.Count = 0 Then
                Return False
            Else
                ' 特定荷主機能開放レンタル先の場合
                Dim whereKbnF017 As String =
                String.Concat(
                    "    KBN_GROUP_CD = 'F017'",
                    "AND KBN_NM3 = '", nrsBrCd, "'",
                    "AND SYS_DEL_FLG = '0'")
                Dim drKbnF017 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(whereKbnF017)
                If drKbnF017.Count = 0 Then
                    Return False
                Else
                    ' プラントコード（FFEM）登録済み営業所の場合
                    ' FFEM オフライン品 運用営業所コードと判定する。
                    Return True
                End If
            End If
        Else
            ' NRS営業所の場合
            Return False
        End If

    End Function

    ''' <summary>
    ''' FFEM オフライン品 依頼書未出力有無判定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="nrsBrCd"></param>
    ''' <returns></returns>
    Private Function IsFFEM_OfflineIraishoMiPrint(ByVal frm As LMC010F, ByVal nrsBrCd As String) As Boolean

        Dim ds As DataSet = New LMC010DS()

        Dim dr As DataRow = ds.Tables("LMC010IN").NewRow()
        dr.Item("NRS_BR_CD") = nrsBrCd
        ds.Tables("LMC010IN").Rows.Add(dr)

        ds = CallWSA("LMC010BLF", "SelectOutkaediDtlFjfOffMiPrintCount", ds)

        If MyBase.GetResultCount > 0 Then
            Return True
        Else
            Return False
        End If

    End Function

#End Region ' "FFEM オフライン品 依頼書未出力メッセージ表示用"

#Region "FFEM 原料プラント間転送 関連"

    ''' <summary>
    ''' FFEM原料プラント間転送か否かの判定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rowNo"></param>
    ''' <returns></returns>
    Friend Function IsFFEM_MaterialPlantTransfer(ByVal frm As LMC010F, ByVal rowNo As Integer) As Boolean

        If _
            Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.ZFVYHKKBN.ColNo)) = "2" AndAlso
            Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.MATNR.ColNo)).StartsWith("243") AndAlso
            Me._LMCconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMC010G.sprDetailDef.ZFVYDENTYP.ColNo)) = "ZUB1" Then
            ' 引当計上予実区分(ZFVYHKKBN) = '2'(出荷予定) かつ
            ' 品目コード(GOODS_CD_CUST[設定元元:MATNR]) の左 3桁が '243'(原料) かつ
            ' 伝票タイプ区分(ZFVYDENTYP) = 'ZUB1'(在庫転送オーダー) の場合
            ' FFEM原料プラント間転送である
            Return True
        End If

        Return False

    End Function

#End Region ' "FFEM 原料プラント間転送 関連"

#End Region 'Method

End Class

