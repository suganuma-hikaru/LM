' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG         : 請求サブシステム
'  プログラムID     :  LMGControlH : 請求サブシステム編集画面 共通処理
'  作  成  者       :  [笈川]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Base.GUI

''' <summary>
''' LMDControlハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMGControlH
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 共通クラス(V)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMGControlV

    ''' <summary>
    ''' 共通クラス(G)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMGControlG

    ''' <summary>
    ''' PGID
    ''' </summary>
    ''' <remarks></remarks>
    Private _Pgid As String

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New(ByRef frm As Form, ByVal pgid As String, ByVal v As LMGControlV, ByVal g As LMGControlG)

        Me.SetPGID(pgid)
        Me._ControlV = v
        Me._ControlG = g

    End Sub

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New(ByVal pgid As String)

        Me.SetPGID(pgid)

    End Sub

#End Region

    ''' <summary>
    ''' 確認メッセージの表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>OKの場合:True　Cancelの場合:False</returns>
    ''' <remarks></remarks>
    Friend Function SetMessageC001(ByVal frm As Form, ByVal msg As String) As Boolean

        '確認メッセージ表示
        If MyBase.ShowMessage(frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 処理終了メッセージを表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg1">置換文字1</param>
    ''' <param name="msg2">置換文字2</param>
    ''' <remarks></remarks>
    Friend Sub SetMessageG002(ByVal frm As Form, ByVal msg1 As String, ByVal msg2 As String)

        MyBase.ShowMessage(frm, "G002", New String() {msg1, msg2})

    End Sub

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub StartAction(ByVal frm As Form)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'メッセージのクリア
        MyBase.ClearMessageAria(DirectCast(frm, Jp.Co.Nrs.LM.GUI.Win.Interface.ILMForm))

    End Sub

    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub EndAction(ByVal frm As Form)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub FailureSelect(ByVal frm As Form)

        '画面解除
        MyBase.UnLockedControls(frm)

    End Sub

    ''' <summary>
    ''' 処理続行確認
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">メッセージ置換文字列(処理名)</param>
    ''' <param name="cntSelect">検索結果件数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ConfirmMsg(ByVal frm As Form, ByVal msg As String, ByVal cntSelect As String) As Boolean

        If MyBase.ShowMessage(frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G016", New String() {cntSelect})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' WSAクラス呼出
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="BLF">BLFファイル名</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <param name="rtDs">データセット</param>
    ''' <param name="mc">ワーニング件数の閾値</param>
    ''' <param name="rc">表示最大件数の閾値</param>
    ''' <returns>WSA呼出後のデータセット</returns>
    ''' <returns2>取得エラー時=Nothing。取得成功時=rtnDSを設定。取得0件の時もrtnDSを設定しているのは、呼び元画面にてSpreadクリアの判定に使用するため。</returns2>
    ''' <remarks></remarks>
    Friend Function CallWSAAction(ByVal frm As Form _
                                  , ByVal blf As String _
                                  , ByVal actionId As String _
                                  , ByVal rtDs As DataSet _
                                  , ByVal rc As Integer _
                                  , Optional ByVal mc As Integer = -1 _
                                  ) As DataSet

        '閾値の設定
        MyBase.SetLimitCount(rc)

        '表示最大件数の設定
        MyBase.SetMaxResultCount(mc)

        Dim rtnDs As DataSet = MyBase.CallWSA(blf, actionId, rtDs)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then
            If MyBase.IsWarningMessageExist() = True Then         'Warningの場合

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    'WSA呼出し
                    ''SBS菱刈　LMG020検索時レスポンス対応　閾値ワーニングOKの場合、BLFを回避
                    ' If "LMG020BLF".Equals(BLF) = False Then
                    rtnDs = MyBase.CallWSA(blf, actionId, rtDs)
                    'End If
                    '強制実行フラグの設定
                    MyBase.SetForceOparation(False)

                    '検索成功時
                    Return rtnDs

                Else    '「いいえ」を選択
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G007")

                    '検索失敗時、共通処理を行う
                    Call Me.FailureSelect(frm)
                    Return Nothing

                End If
            Else

                'メッセージエリアの設定
                MyBase.ShowMessage(frm)

                '検索失敗時、共通処理を行う
                Call Me.FailureSelect(frm)
                Return rtnDs

            End If
        Else
            '検索成功時
            Return rtnDs

        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' スプレッド明細行のチェックリスト(RowIndex)取得
    ''' </summary>
    ''' <param name="activeSheet">スプレッドシート</param>
    ''' <param name="defNo">レコードチェックボックスのカラム№</param>
    ''' <returns>チェックリスト</returns>
    ''' <remarks></remarks>
    Friend Function GetCheckList(ByVal activeSheet As SheetView, ByVal defNo As Integer) As ArrayList

        'チェック件数取得
        With activeSheet

            Dim list As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If Me._ControlV.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    list.Add(i)
                End If
            Next

            Return list

        End With

    End Function

    ''' <summary>
    ''' スプレッド明細行の全量(対象のColum)取得
    ''' </summary>
    ''' <param name="activeSheet">スプレッドシート</param>
    ''' <param name="defNo">レコードチェックボックスのカラム№</param>
    ''' <returns>チェックリスト</returns>
    ''' <remarks></remarks>
    Friend Function GetSpredList(ByVal activeSheet As SheetView, ByVal defNo As Integer) As ArrayList

        'チェック件数取得
        With activeSheet

            Dim list As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max

                list.Add(Me._ControlV.GetCellValue(.Cells(i, defNo)))

            Next

            Return list

        End With

    End Function

    ''' <summary>
    ''' 別PG起動処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="id">画面ID</param>
    ''' <param name="recType">レコードタイプ 初期値 = ""</param>
    ''' <returns>パラメータクラス</returns>
    ''' <remarks></remarks>
    Friend Function FormShow(ByVal ds As DataSet, ByVal id As String, Optional ByVal recType As String = "") As LMFormData

        'パラメータ設定
        Dim prm As LMFormData = New LMFormData()
        prm.ParamDataSet = ds
        prm.RecStatus = recType

        '画面起動
        LMFormNavigate.NextFormNavigate(Me, id, prm)

        Return prm

    End Function

    ''' <summary>
    ''' 勘定科目コード設定
    ''' </summary>
    ''' <param name="busyoCd">部署コード</param>
    ''' <param name="keiriKb">経理科目コード区分</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetKanjoKmkCd(ByVal busyoCd As String, ByVal keiriKb As String, Optional ByRef sJISYATASYA_KB As String = "") As String

        Dim kanjoKmkCd As String = Me.GetKanjoKmkCd(busyoCd, keiriKb, LMGControlC.GetKanjoKmkInfo.KANJO_KMK_CD, sJISYATASYA_KB)
        Dim keiriBumonCd As String = Me.GetKanjoKmkCd(busyoCd, keiriKb, LMGControlC.GetKanjoKmkInfo.KEIRI_BUMON_CD)

        Return Me._ControlG.EditConcatData(keiriBumonCd, kanjoKmkCd, ".")

    End Function

    ''' <summary>
    ''' 勘定科目コード取得
    ''' </summary>
    ''' <param name="busyoCd">部署コード</param>
    ''' <param name="keiriKb">経理科目コード区分</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetKanjoKmkCd(ByVal busyoCd As String, ByVal keiriKb As String, ByVal rtnInfo As LMGControlC.GetKanjoKmkInfo _
                                            , Optional ByRef sJISYATASYA_KB As String = "") As String

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
                'UPD 2016/09/06 最保管対応
                If ("02").Equals(sJISYATASYA_KB) = True Then
                    If String.Empty.Equals(kbnDr(0).Item("KBN_NM7").ToString.Trim) = False Then
                        rtnString = kbnDr(0).Item("KBN_NM7").ToString()
                    Else
                        rtnString = kbnDr(0).Item("KBN_NM3").ToString()
                    End If

                Else
                    rtnString = kbnDr(0).Item("KBN_NM3").ToString()

                End If
            Case LMGControlC.GetKanjoKmkInfo.KEIRI_BUMON_CD
                '経理部門コードを返却
                rtnString = kbnDr(0).Item("KBN_NM6").ToString()
        End Select

        Return rtnString

    End Function

    ' 2011/08/17 ADD-START SBS)SUGA 
    ''' <summary>
    ''' 四捨五入処理
    ''' </summary>
    ''' <param name="amt">四捨五入する値</param>
    ''' <param name="pos">四捨五入する際の小数点以下有効桁数(設定例…整数のみ有効の場合：0、小数点第1位まで有効の場合：1、など)</param>
    ''' <returns>四捨五入した結果</returns>
    ''' <remarks></remarks>
    Friend Function ToHalfAdjust(ByVal amt As Decimal, ByVal pos As Integer) As Decimal

        ' 後続の(整数で計算⇒小数に戻す)処理で使用するため、10の乗数を取得
        Dim dCoef As Decimal = Convert.ToDecimal(System.Math.Pow(10, pos))

        Dim i As Decimal = 0

        If amt > 0 Then
            i = Convert.ToDecimal(System.Math.Floor((amt * dCoef) + 0.5) / dCoef)
        Else
            i = Convert.ToDecimal(System.Math.Ceiling((amt * dCoef) - 0.5) / dCoef)
        End If

        Return i

    End Function
    ' 2011/08/17 ADD-E N D SBS)SUGA 

    '(2013.02.15)要望番号1832 -- START --
    ''' <summary>
    ''' キャッシュ抽出条件設定
    ''' </summary>
    ''' <param name="andstr"></param>
    ''' <param name="whereStr">画面で入力された値</param>
    ''' <param name="ptn">条件</param>
    ''' <param name="colNm">項目名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetWhereData(ByVal andstr As System.Text.StringBuilder, ByVal whereStr As String, ByVal ptn As LMGControlC.ConditionPattern _
                                 , ByVal colNm As String) As String

        SetWhereData = String.Empty

        If String.IsNullOrEmpty(whereStr) = False Then
            If andstr.Length <> 0 Then
                andstr.Append("AND")
            End If


            Select Case ptn
                '完全一致
                Case LMGControlC.ConditionPattern.equal

                    SetWhereData = String.Concat(" ", colNm, " = '", whereStr, "'", vbNewLine)
                    '前方一致
                Case LMGControlC.ConditionPattern.pre

                    SetWhereData = String.Concat(" ", colNm, " LIKE '", Me.EscForLike(whereStr), "%'", vbNewLine)
                    '部分一致
                Case LMGControlC.ConditionPattern.all

                    SetWhereData = String.Concat(" ", colNm, " LIKE '", "%", Me.EscForLike(whereStr), "%'", vbNewLine)

                Case LMGControlC.ConditionPattern.more

                    SetWhereData = String.Concat(" ", colNm, " >= '", whereStr, "'", vbNewLine)

                Case LMGControlC.ConditionPattern.less

                    SetWhereData = String.Concat(" ", colNm, " <= '", whereStr, "'", vbNewLine)


            End Select

        End If

        Return SetWhereData

    End Function

    ''' <summary>
    ''' 検索条件のエスケープ
    ''' </summary>
    ''' <param name="value">条件文字</param>
    ''' <returns>エスケープ処理後の文字</returns>
    ''' <remarks></remarks>
    Private Function EscForLike(ByVal value As String) As String

        Return System.Text.RegularExpressions.Regex.Replace(value, "([\[\]*%])", "[$1]")

    End Function

    ''' <summary>
    ''' キャッシュからデータ取得
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="cacheTbl">キャッシュ</param>
    ''' <param name="whereStr">キャッシュ条件</param>
    ''' <param name="sort">ソート</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SelectListData(ByVal ds As DataSet, ByVal tblNm As String, ByVal cacheTbl As String, ByVal whereStr As String, ByVal sort As String) As DataSet

        Dim locationRows As DataRow() = MyBase.GetLMCachedDataTable(cacheTbl).Select(whereStr, sort)

        'メッセージ設定
        Dim cnt As Integer = locationRows.Length
        Dim lmt As Integer = Me.SetLimit()

        If cnt = 0 Then
            '0件
            MyBase.SetMessage("G001")
        ElseIf lmt < cnt Then
            'l件以上
            MyBase.SetMessage("W001", New String() {Convert.ToString(lmt)})
        End If

        If 0 < cnt Then
            '正常時 OUTテーブルへデータ格納
            Me.SetDataSetOutListData(ds, tblNm, locationRows)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 閾値設定
    ''' </summary>
    ''' <param name="rn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetLimit(Optional ByVal rn As Integer = -1) As Integer

        If rn <> -1 Then
            Return rn
        End If

        Return Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                             (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1")))

    End Function

    ''' <summary>
    ''' 各画面データセットのOUTテーブルへデータを格納する
    ''' </summary>
    ''' <param name="ds">各画面のDS</param>
    ''' <param name="tblNm">抽出データ配列</param>
    ''' <remarks></remarks>
    Friend Function SetDataSetOutListData(ByVal ds As DataSet, ByVal tblNm As String, ByVal drs As DataRow()) As DataSet

        Dim dt As DataTable = ds.Tables(tblNm)
        Dim max As Integer = drs.Length - 1
        For i As Integer = 0 To max
            dt.ImportRow(drs(i))
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 取得件数によるメッセージ表示処理
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function CountRows(ByVal frm As Form, ByVal spr As Spread.LMSpreadSearch, ByVal outTbl As DataTable) As Boolean

        If MyBase.IsMessageExist() = True Then

            Dim warningChk As Boolean = MyBase.IsWarningMessageExist()
            Dim msg As Integer = MyBase.ShowMessage(frm)

            If warningChk = True Then         'Warningの場合(500件以上)

                Return Me.ShowWarningMsg(frm, msg)

            Else 'Errorの場合(0件)

                'SPREAD(表示行)初期化
                spr.CrearSpread()

                Return False

            End If
        Else     '上記以外

            Return True

        End If

    End Function

    ''' <summary>
    ''' ワーニングメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">メッセージの戻り値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShowWarningMsg(ByVal frm As Form, ByVal msg As Integer) As Boolean

        'メッセージを表示し、戻り値により処理を分ける
        If MsgBoxResult.Ok <> msg Then '「いいえ」を選択
            MyBase.ShowMessage(frm, "G007")
            Return False
        End If

        Return True

    End Function

    '(2013.02.15)要望番号1832 --  END  --

End Class
