' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF          : 
'  プログラムID     :  LMFControlH  : LMF画面 共通処理
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMFControlハンドラクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 2010/04/09 ito
''' </histry>
Public Class LMFControlH
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="pgid"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal frm As Form, ByVal pgid As String)

        MyBase.New()
        MyBase.SetPGID(pgid)

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "ユーティリティ"

    ''' <summary>
    ''' 確認メッセージの表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>OKの場合:True　Cancelの場合:False</returns>
    ''' <remarks></remarks>
    Public Function SetMessageC001(ByVal frm As Form, ByVal msg As String) As Boolean

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
    ''' 別PG起動処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="id">画面ID</param>
    ''' <param name="recType">レコードタイプ 初期値 = ""</param>
    ''' <param name="skipFlg">画面表示フラグ 初期値 = False</param>
    ''' <returns>パラメータクラス</returns>
    ''' <remarks></remarks>
    Public Function FormShow(ByVal ds As DataSet, ByVal id As String _
                             , Optional ByVal recType As String = "" _
                             , Optional ByVal skipFlg As Boolean = False) As LMFormData

        'パラメータ設定
        Dim prm As LMFormData = New LMFormData()
        prm.ParamDataSet = ds
        prm.RecStatus = recType
        prm.SkipFlg = skipFlg

        '画面起動
        LMFormNavigate.NextFormNavigate(Me, id, prm)

        Return prm

    End Function

    ''' <summary>
    ''' サーバアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionStr">メソッド名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Friend Function ServerAccess(ByVal ds As DataSet, ByVal actionStr As String) As DataSet

        Return MyBase.CallWSA(String.Concat(MyBase.GetPGID(), LMControlC.BLF), actionStr, ds)

    End Function

    ''' <summary>
    ''' サーバアクセス(LMCOM使用版)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionStr">メソッド名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Friend Function ServerAccessLmcom(ByVal ds As DataSet, ByVal actionStr As String) As DataSet

        Return MyBase.CallWSA("LMCOMBLF", actionStr, ds)

    End Function

    ''' <summary>
    ''' ラジオボタンの値を返却
    ''' </summary>
    ''' <param name="ctl">ラジオボタンコントロール</param>
    ''' <returns>チェック有 = 1　チェック無 = 0</returns>
    ''' <remarks></remarks>
    Friend Function GetOptData(ByVal ctl As Win.LMOptionButton) As String

        GetOptData = LMConst.FLG.OFF

        'チェックが入っている場合、1を設定
        If ctl.Checked = True Then
            GetOptData = LMConst.FLG.ON
        End If

        Return GetOptData

    End Function

    ''' <summary>
    ''' 次コントロールにフォーカス移動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="eventFlg">Enterボタンの場合、True</param>
    ''' <remarks></remarks>
    Public Sub NextFocusedControl(ByVal frm As Form, ByVal eventFlg As Boolean)

        'Enter以外の場合、スルー
        If eventFlg = False Then
            Exit Sub
        End If

        ''エラーの場合、フォーカス移動しない
        'Dim msg As String = frm.Controls.Find(LMFControlC.MES_AREA, True)(0).Text
        'If String.IsNullOrEmpty(msg) = False _
        '    AndAlso LMFControlC.MESSAGE_E.Equals(frm.Controls.Find(LMFControlC.MES_AREA, True)(0).Text.Substring(0, 1)) = True _
        '    Then
        '    Exit Sub
        'End If

        frm.SelectNextControl(frm.ActiveControl, True, True, True, True)

    End Sub

    ''' <summary>
    ''' フォームに検索した結果(Text)を取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">コントロール名</param>
    ''' <returns>LMImTextBox</returns>
    ''' <remarks></remarks>
    Friend Function GetTextControl(ByVal frm As Form, ByVal objNm As String) As Win.InputMan.LMImTextBox
        Return DirectCast(frm.Controls.Find(objNm, True)(0), Win.InputMan.LMImTextBox)
    End Function

    ''' <summary>
    ''' メッセージクリア
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>Ture</returns>
    ''' <remarks></remarks>
    Private Function ClearMessage(ByVal frm As Win.Interface.ILMForm) As Boolean

        'メッセージのクリア
        MyBase.ClearMessageAria(frm)

        Return True

    End Function

    ''' <summary>
    ''' 選択した情報をチェック(入力チェック後)
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ChkSelectData(ByVal arr As ArrayList) As Boolean

        If arr.Count < 1 Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 一括更新処理終了アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rtnResult">エラー判定</param>
    ''' <param name="msg">置換文字</param>
    ''' <remarks></remarks>
    Public Sub IkkatuEndAction(ByVal frm As Form, ByVal rtnResult As Boolean, ByVal msg As String)

        'エラー帳票情報に値がある場合、出力
        If MyBase.IsMessageStoreExist() = True Then

            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            MyBase.ShowMessage(frm, "E235")
            Exit Sub

        End If

        'エラーがない場合、完了メッセージを表示
        If rtnResult = True Then
            Call Me.SetMessageG002(frm, msg, String.Empty)
        End If

    End Sub

    ''' <summary>
    ''' 印刷プレビュー表示
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub ShowPreviewData(ByVal ds As DataSet)

        'プレビュー判定 
        Dim prevDt As DataTable = ds.Tables(LMConst.RD)
        If 0 < prevDt.Rows.Count Then

            'プレビューの生成
            Dim prevFrm As RDViewer = New RDViewer()

            'データ設定
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始
            prevFrm.Run()

            'プレビューフォームの表示
            prevFrm.Show()

        End If

    End Sub

    ''' <summary>
    ''' 帳票テーブルの設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Friend Function SetRptDataTable(ByVal ds As DataSet) As DataSet

        '帳票DataTableがない場合、追加
        If ds.Tables(LMConst.RD) Is Nothing = True Then

            Dim rptDs As DataSet = New RdPrevInfoDS()

            'テーブル追加
            ds = Me.SetBetuDataTable(ds, rptDs, LMConst.RD)

        End If

        '値のクリア
        ds.Tables(LMConst.RD).Clear()

        Return ds

    End Function

    ''' <summary>
    ''' DataTable追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="setDs">DataSet</param>
    ''' <param name="tblNm">Table名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Friend Function SetBetuDataTable(ByVal ds As DataSet, ByVal setDs As DataSet, ByVal tblNm As String) As DataSet

        'DataTableのインスタンス生成
        Dim setDt As DataTable = setDs.Tables(tblNm).Copy
        setDt.TableName = tblNm

        'テーブル追加
        ds.Tables.Add(setDt)

        Return ds

    End Function

    'START YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない
    ''' <summary>
    ''' メッセージ設定
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetErrMessageStore(ByVal id As String, ByVal rowNo As Integer) As Boolean

        MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, id, , rowNo.ToString())
        Return False

    End Function
    'END YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub StartAction(ByVal frm As Form)

        '画面全ロック
        MyBase.LockedControls(frm)

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
    Friend Sub EndAction(ByVal frm As Form, Optional ByVal id As String = "G007")

        '画面解除
        MyBase.UnLockedControls(frm)

        'ガイダンスメッセージを表示
        Call Me.SetGMessage(frm, id)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' ガイダンスメッセージを表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="id">メッセージID</param>
    ''' <remarks></remarks>
    Private Sub SetGMessage(ByVal frm As Form, ByVal id As String)

        'メッセージ欄に値がある場合、スルー
        If String.IsNullOrEmpty(frm.Controls.Find("lblMsgAria", True)(0).Text) = False Then
            Exit Sub
        End If

        MyBase.ShowMessage(frm, id)

    End Sub

#End Region

#Region "計算処理"

#Region "運送重量"

    ''' <summary>
    ''' 運送重量を取得(実計算)
    ''' </summary>
    ''' <param name="calcFlg">風袋加算フラグ(商品)</param>
    ''' <param name="unsoCalcFlg">風袋加算フラグ(運送会社)</param>
    ''' <param name="suryoTani">数量単位</param>
    ''' <param name="irime">入目</param>
    ''' <param name="irisu">入数</param>
    ''' <param name="konsu">梱数</param>
    ''' <param name="hasu">端数</param>
    ''' <param name="stdIrime">標準入目</param>
    ''' <param name="stdWt">標準重量</param>
    ''' <param name="suryo">数量</param>
    ''' <returns>運送重量(行単位)</returns>
    ''' <remarks></remarks>
    Friend Function GetJuryoCalcData(ByVal calcFlg As String _
                                   , ByVal unsoCalcFlg As String _
                                   , ByVal suryoTani As String _
                                   , ByVal irime As Decimal _
                                   , ByVal irisu As Decimal _
                                   , ByVal konsu As Decimal _
                                   , ByVal hasu As Decimal _
                                   , ByVal stdIrime As Decimal _
                                   , ByVal stdWt As Decimal _
                                   , ByVal suryo As Decimal _
                                   , ByVal hutaiJyuryo As String _
                                   ) As Decimal

        '重量計算
        Return Me.GetJuryoCalcData(calcFlg, unsoCalcFlg, suryoTani, irime, (irisu * konsu + hasu), stdIrime, stdWt, suryo, hutaiJyuryo)

    End Function

    ''' <summary>
    ''' 運送重量を取得(実計算)
    ''' </summary>
    ''' <param name="calcFlg">風袋加算フラグ(商品)</param>
    ''' <param name="unsoCalcFlg">風袋加算フラグ(運送会社)</param>
    ''' <param name="suryoTani">数量単位</param>
    ''' <param name="irime">入目</param>
    ''' <param name="kosu">個数</param>
    ''' <param name="stdIrime">標準入目</param>
    ''' <param name="stdWt">標準重量</param>
    ''' <param name="suryo">数量</param>
    ''' <returns>運送重量(行単位)</returns>
    ''' <remarks></remarks>
    Friend Function GetJuryoCalcData(ByVal calcFlg As String _
                                         , ByVal unsoCalcFlg As String _
                                         , ByVal suryoTani As String _
                                         , ByVal irime As Decimal _
                                         , ByVal kosu As Decimal _
                                         , ByVal stdIrime As Decimal _
                                         , ByVal stdWt As Decimal _
                                         , ByVal suryo As Decimal _
                                         , Optional ByVal hutaiJyuryo As String = "" _
                                         ) As Decimal

        Dim futai As Decimal = 0

        '計算フラグ = 01の場合、風袋重量の取得
        If LMFControlC.FLG_ON.Equals(calcFlg) = True AndAlso LMFControlC.FLG_ON.Equals(unsoCalcFlg) = True Then

            '(2012.12.20)要望番号1692 容器重量対応 -- START --
            If hutaiJyuryo.ToString.Trim.Equals("") = True Then
                '区分マスタから値取得
                Dim sql As String = String.Concat(" KBN_CD = '", suryoTani, "' AND KBN_GROUP_CD = '", LMKbnConst.KBN_N001, "' ")
                Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(sql)
                If 0 < drs.Length Then
                    futai = Convert.ToDecimal(drs(0).Item("VALUE1").ToString())
                End If
            Else
                '荷主明細マスタから値取得
                futai = Convert.ToDecimal(hutaiJyuryo.ToString())
            End If

            ''区分マスタから値取得
            'Dim sql As String = String.Concat(" KBN_CD = '", suryoTani, "' AND KBN_GROUP_CD = '", LMKbnConst.KBN_N001, "' ")
            'Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(sql)

            'If 0 < drs.Length Then
            '    futai = Convert.ToDecimal(drs(0).Item("VALUE1").ToString())
            'End If

            '(2012.12.20)要望番号1692 容器重量対応 --  END  --

        End If

        '(2012.12.06)要望番号1649 小分け時の運送重量対応 --- START ---
        Dim juryo As Decimal = 0

        '出荷数量＜標準入目ならば、小分けとみなす
        If suryo < stdIrime Then
            '[小分け出荷] 商品１つあたりの重量 = （商品）標準重量 * 数量 / （商品）標準入目

            '(2012.12.26)小分け出荷、サンプル出荷時は風袋重量を加算しない -- START --
            'juryo = stdWt * suryo / stdIrime + futai
            juryo = stdWt * suryo / stdIrime
            '(2012.12.26)小分け出荷、サンプル出荷時は風袋重量を加算しない --  END  --

            Return juryo

        Else
            '[通常出荷用] 商品１つあたりの重量 = （商品）標準重量 * 数量 / （商品）標準入目 + 風袋重量
            If stdIrime <> 0 Then
                juryo = stdWt * Convert.ToDecimal(irime) / stdIrime + futai
            End If
            Return juryo * kosu

        End If


        ''標準重量 * 入目 / 標準入目 + 風袋
        'Dim juryo As Decimal = 0
        'If stdIrime <> 0 Then
        '    juryo = stdWt * Convert.ToDecimal(irime) / stdIrime + futai
        'End If

        ''重量 * 個数
        'Return juryo * kosu

        '(2012.12.06)要望番号1649 小分け時の運送重量対応 ---  END  ---

    End Function

    'START YANAI 要望番号790
    ''' <summary>
    ''' 運送重量を取得(実計算)
    ''' </summary>
    ''' <param name="calcFlg">風袋加算フラグ(商品)</param>
    ''' <param name="unsoCalcFlg">風袋加算フラグ(運送会社)</param>
    ''' <param name="suryoTani">数量単位</param>
    ''' <param name="irime">入目</param>
    ''' <param name="irisu">入数</param>
    ''' <param name="konsu">梱数</param>
    ''' <param name="hasu">端数</param>
    ''' <param name="stdSumJuryo">標準入目</param>
    ''' <param name="stdWt">標準重量</param>
    ''' <returns>運送重量(行単位)</returns>
    ''' <remarks></remarks>
    Friend Function GetJuryoInkaCalcData(ByVal calcFlg As String _
                                         , ByVal unsoCalcFlg As String _
                                         , ByVal suryoTani As String _
                                         , ByVal irime As Decimal _
                                         , ByVal irisu As Decimal _
                                         , ByVal konsu As Decimal _
                                         , ByVal hasu As Decimal _
                                         , ByVal stdSumJuryo As Decimal _
                                         , ByVal stdWt As Decimal _
                                         , ByVal hutaiJyuryo As String _
                                         ) As Decimal

        '重量計算
        Return Me.GetJuryoInkaCalcData(calcFlg, unsoCalcFlg, suryoTani, irime, (irisu * konsu + hasu), stdSumJuryo, stdWt, hutaiJyuryo)

    End Function

    ''' <summary>
    ''' 運送重量を取得(実計算)
    ''' </summary>
    ''' <param name="calcFlg">風袋加算フラグ(商品)</param>
    ''' <param name="unsoCalcFlg">風袋加算フラグ(運送会社)</param>
    ''' <param name="suryoTani">数量単位</param>
    ''' <param name="irime">入目</param>
    ''' <param name="kosu">個数</param>
    ''' <param name="stdSumJuryo">標準入目</param>
    ''' <param name="stdWt">標準重量</param>
    ''' <returns>運送重量(行単位)</returns>
    ''' <remarks></remarks>
    Friend Function GetJuryoInkaCalcData(ByVal calcFlg As String _
                                         , ByVal unsoCalcFlg As String _
                                         , ByVal suryoTani As String _
                                         , ByVal irime As Decimal _
                                         , ByVal kosu As Decimal _
                                         , ByVal stdSumJuryo As Decimal _
                                         , ByVal stdWt As Decimal _
                                         , Optional ByVal hutaiJyuryo As String = "" _
                                         ) As Decimal

        Dim futai As Decimal = 0

        '計算フラグ = 01の場合、風袋重量の取得
        If LMFControlC.FLG_ON.Equals(calcFlg) = True AndAlso LMFControlC.FLG_ON.Equals(unsoCalcFlg) = True Then

            '(2012.12.20)要望番号1692 容器重量対応 -- START --
            If hutaiJyuryo.ToString.Trim.Equals("") = True Then
                '区分マスタから値取得
                Dim sql As String = String.Concat(" KBN_CD = '", suryoTani, "' AND KBN_GROUP_CD = '", LMKbnConst.KBN_N001, "' ")
                Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(sql)
                If 0 < drs.Length Then
                    futai = Convert.ToDecimal(drs(0).Item("VALUE1").ToString())
                End If
            Else
                '荷主明細マスタから値取得
                futai = Convert.ToDecimal(hutaiJyuryo.ToString())
            End If

            ''区分マスタから値取得
            'Dim sql As String = String.Concat(" KBN_CD = '", suryoTani, "' AND KBN_GROUP_CD = '", LMKbnConst.KBN_N001, "' ")
            'Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(sql)
            'If 0 < drs.Length Then
            '    futai = Convert.ToDecimal(drs(0).Item("VALUE1").ToString())
            'End If

            '(2012.12.20)要望番号1692 容器重量対応 --  END  --

        End If

        '(2012.12.26)梱数分だけ掛けるようにする -- START --
        '標準重量 * 入目 / 標準入目 + (風袋重量 × 梱数)
        Dim juryo As Decimal = 0
        If stdSumJuryo <> 0 Then
            juryo = stdSumJuryo + futai * kosu
            'juryo = stdSumJuryo + futai
        End If
        '(2012.12.26)梱数分だけ掛けるようにする --  END  --

        '重量
        Return juryo

    End Function
    'END YANAI 要望番号790

#End Region

#Region "運送梱包個数"

    ''' <summary>
    ''' 運送梱包数の計算
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="unsoTtlNbCol">運送個数列名</param>
    ''' <param name="hasuCol">端数列名</param>
    ''' <param name="pkgNbCol">梱包個数列名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function GetUnsoKonpoData(ByVal dr As DataRow _
                                      , ByVal unsoTtlNbCol As String _
                                      , ByVal hasuCol As String _
                                      , ByVal pkgNbCol As String _
                                      ) As Decimal

        Return Me.GetUnsoKonpoData(Convert.ToDecimal(dr.Item(unsoTtlNbCol).ToString()) _
                                   , Convert.ToDecimal(dr.Item(hasuCol).ToString()) _
                                   , Convert.ToDecimal(dr.Item(pkgNbCol).ToString()))

    End Function

    ''' <summary>
    ''' 運送梱包数の計算
    ''' </summary>
    ''' <param name="kosu">運送個数</param>
    ''' <param name="hasu">端数</param>
    ''' <param name="konp">梱包個数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function GetUnsoKonpoData(ByVal kosu As Decimal _
                                      , ByVal hasu As Decimal _
                                      , ByVal konp As Decimal _
                                      ) As Decimal

        GetUnsoKonpoData = 0

        If kosu = 0 Then

            '切り上げした値を設定
            If konp <> 0 Then
                GetUnsoKonpoData = System.Math.Ceiling(hasu / konp)
            End If

        Else

            '端数が0より大きい場合、+1
            If 0 < hasu Then
                kosu = kosu + 1
            End If
            GetUnsoKonpoData = kosu

        End If

        Return GetUnsoKonpoData

    End Function

#End Region

#End Region

#Region "別サブ用"

    ''' <summary>
    ''' 運送重量を取得(別サブ用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNmM">中レコードのDataTable名</param>
    ''' <param name="tblNmUnsoL">運送(大)レコードのDataTable名</param>
    ''' <param name="tblNmUnsoM">運送(中)レコードのDataTable名</param>
    ''' <param name="calcCol">風袋加算フラグ(商品M)のDataRow列名</param>
    ''' <param name="suryoTaniCol">数量単位のDataRow列名</param>
    ''' <param name="irisuCol">入数のDataRow列名</param>
    ''' <param name="stdIrimeCol">標準入目のDataRow列名</param>
    ''' <param name="stdWtCol">標準重量のDataRow列名</param>
    ''' <param name="irimeCol">入目列名　初期値："IRIME"</param>
    ''' <param name="tareYnCol">風袋加算フラグ(運送会社)列名　初期値："TARE_YN"</param>
    ''' <param name="unsoTtlNbCol">運送個数列名　初期値："UNSO_TTL_NB"</param>
    ''' <param name="hasuCol">端数列名　初期値："HASU"</param>
    ''' <param name="sysDelCol">(中)テーブルの削除フラグ列名　初期値："SYS_DEL_FLG"</param>
    ''' <returns>運送重量</returns>
    ''' <remarks></remarks>
    Friend Function GetJuryoData(ByVal ds As DataSet _
                                     , ByVal tblNmM As String _
                                     , ByVal tblNmUnsoL As String _
                                     , ByVal tblNmUnsoM As String _
                                     , ByVal calcCol As String _
                                     , ByVal suryoTaniCol As String _
                                     , ByVal irisuCol As String _
                                     , ByVal stdIrimeCol As String _
                                     , ByVal stdWtCol As String _
                                     , Optional ByVal irimeCol As String = "IRIME" _
                                     , Optional ByVal tareYnCol As String = "TARE_YN" _
                                     , Optional ByVal unsoTtlNbCol As String = "UNSO_TTL_NB" _
                                     , Optional ByVal hasuCol As String = "HASU" _
                                     , Optional ByVal sysDelCol As String = "SYS_DEL_FLG" _
                                     , Optional ByVal suryo As String = "UNSO_TTL_QT" _
                                     , Optional ByVal hutaiJuryo As String = "" _
                                     ) As Decimal

        GetJuryoData = 0
        Dim mDt As DataTable = ds.Tables(tblNmM)
        Dim mDr As DataRow = Nothing
        Dim unsoMDt As DataTable = ds.Tables(tblNmUnsoM)
        Dim unsoMDr As DataRow = Nothing
        Dim max As Integer = unsoMDt.Rows.Count - 1
        If max < 0 Then
            Return 0
        End If

        '運送会社の風袋加算フラグ
        Dim tareYnUnso As String = ds.Tables(tblNmUnsoL).Rows(0).Item(tareYnCol).ToString()

        '行数分ループ
        For i As Integer = 0 To max

            mDr = mDt.Rows(i)

            '削除データの場合、スルー
            If LMConst.FLG.ON.Equals(mDr.Item(sysDelCol).ToString()) = True Then
                Continue For
            End If

            unsoMDr = unsoMDt.Rows(i)

            '(2012.12.20)要望番号1692 容器重量対応(hutaiJuryo追加)
            GetJuryoData += Me.GetJuryoCalcData(mDr.Item(calcCol).ToString() _
                                               , tareYnUnso _
                                               , mDr.Item(suryoTaniCol).ToString() _
                                               , Convert.ToDecimal(unsoMDr.Item(irimeCol).ToString()) _
                                               , Convert.ToDecimal(mDr.Item(irisuCol).ToString()) _
                                               , Convert.ToDecimal(unsoMDr.Item(unsoTtlNbCol).ToString()) _
                                               , Convert.ToDecimal(unsoMDr.Item(hasuCol).ToString()) _
                                               , Convert.ToDecimal(mDr.Item(stdIrimeCol).ToString()) _
                                               , Convert.ToDecimal(mDr.Item(stdWtCol).ToString()) _
                                               , Convert.ToDecimal(mDr.Item(suryo).ToString()) _
                                               , hutaiJuryo.ToString _
                                               )

        Next

        Return GetJuryoData

    End Function

    'START YANAI 要望番号790
    ''' <summary>
    ''' 運送重量を取得(別サブ用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNmM">中レコードのDataTable名</param>
    ''' <param name="tblNmUnsoL">運送(大)レコードのDataTable名</param>
    ''' <param name="tblNmUnsoM">運送(中)レコードのDataTable名</param>
    ''' <param name="calcCol">風袋加算フラグ(商品M)のDataRow列名</param>
    ''' <param name="suryoTaniCol">数量単位のDataRow列名</param>
    ''' <param name="irisuCol">入数のDataRow列名</param>
    ''' <param name="stdSumJuryoCol">標準入目のDataRow列名</param>
    ''' <param name="stdWtCol">標準重量のDataRow列名</param>
    ''' <param name="irimeCol">入目列名　初期値："IRIME"</param>
    ''' <param name="tareYnCol">風袋加算フラグ(運送会社)列名　初期値："TARE_YN"</param>
    ''' <param name="unsoTtlNbCol">運送個数列名　初期値："UNSO_TTL_NB"</param>
    ''' <param name="hasuCol">端数列名　初期値："HASU"</param>
    ''' <param name="sysDelCol">(中)テーブルの削除フラグ列名　初期値："SYS_DEL_FLG"</param>
    ''' <returns>運送重量</returns>
    ''' <remarks></remarks>
    Public Function GetJuryoInkaData(ByVal ds As DataSet _
                                     , ByVal tblNmM As String _
                                     , ByVal tblNmUnsoL As String _
                                     , ByVal tblNmUnsoM As String _
                                     , ByVal calcCol As String _
                                     , ByVal suryoTaniCol As String _
                                     , ByVal irisuCol As String _
                                     , ByVal stdSumJuryoCol As String _
                                     , ByVal stdWtCol As String _
                                     , Optional ByVal irimeCol As String = "IRIME" _
                                     , Optional ByVal tareYnCol As String = "TARE_YN" _
                                     , Optional ByVal unsoTtlNbCol As String = "UNSO_TTL_NB" _
                                     , Optional ByVal hasuCol As String = "HASU" _
                                     , Optional ByVal sysDelCol As String = "SYS_DEL_FLG" _
                                     ) As Decimal

        GetJuryoInkaData = 0
        Dim mDt As DataTable = ds.Tables(tblNmM)
        Dim mDr As DataRow = Nothing
        Dim unsoMDt As DataTable = ds.Tables(tblNmUnsoM)
        Dim unsoMDr As DataRow = Nothing
        Dim max As Integer = unsoMDt.Rows.Count - 1
        If max < 0 Then
            Return 0
        End If
        '(2012.12.20)要望番号1692 容器重量対応 -- START --
        Dim dsCom As DataSet = Nothing
        Dim goodsDtlDt As DataTable = Nothing
        Dim drCom As DataRow = Nothing
        Dim hutaiJuryo As String = String.Empty
        '(2012.12.20)要望番号1692 容器重量対応 --  END  --

        '運送会社の風袋加算フラグ
        Dim tareYnUnso As String = ds.Tables(tblNmUnsoL).Rows(0).Item(tareYnCol).ToString()

        '行数分ループ
        For i As Integer = 0 To max

            mDr = mDt.Rows(i)

            '削除データの場合、スルー
            If LMConst.FLG.ON.Equals(mDr.Item(sysDelCol).ToString()) = True Then
                Continue For
            End If

            unsoMDr = unsoMDt.Rows(i)

            '(2012.12.20)要望番号1692 容器重量対応 -- START --

            'DataSet・変数初期化
            dsCom = New DSL.LMCOMDS
            goodsDtlDt = dsCom.Tables("GOODS_DETAILS_IN")
            drCom = goodsDtlDt.NewRow()
            hutaiJuryo = String.Empty

            '商品明細マスタ抽出条件セット
            drCom.Item("NRS_BR_CD") = mDt.Rows(i).Item("NRS_BR_CD").ToString()
            drCom.Item("GOODS_CD_NRS") = mDt.Rows(i).Item("GOODS_CD_NRS").ToString()
            drCom.Item("SUB_KB") = "16"    '容器重量用SUB_KB
            goodsDtlDt.Rows.Add(drCom)

            '商品明細マスタデータ取得(BLF呼び出し)
            dsCom = ServerAccessLmcom(dsCom, "SelectGoodsDetailsData")

            If dsCom.Tables("GOODS_DETAILS_OUT").Rows.Count <> 0 Then
                hutaiJuryo = dsCom.Tables("GOODS_DETAILS_OUT").Rows(0).Item("SET_NAIYO").ToString   '風袋重量
            End If

            '(2012.12.20)要望番号1692 容器重量対応 --  END  --

            '(2012.12.20)要望番号1692 容器重量対応(hutaiJuryo追加)
            GetJuryoInkaData += Me.GetJuryoInkaCalcData(mDr.Item(calcCol).ToString() _
                                               , tareYnUnso _
                                               , mDr.Item(suryoTaniCol).ToString() _
                                               , Convert.ToDecimal(unsoMDr.Item(irimeCol).ToString()) _
                                               , Convert.ToDecimal(mDr.Item(irisuCol).ToString()) _
                                               , Convert.ToDecimal(unsoMDr.Item(unsoTtlNbCol).ToString()) _
                                               , Convert.ToDecimal(unsoMDr.Item(hasuCol).ToString()) _
                                               , Convert.ToDecimal(mDr.Item(stdSumJuryoCol).ToString()) _
                                               , Convert.ToDecimal(mDr.Item(stdWtCol).ToString()) _
                                               , hutaiJuryo.ToString _
                                               )

        Next

        Return GetJuryoInkaData

    End Function
    'END YANAI 要望番号790

    ''' <summary>
    ''' 運送重量を取得(別サブ用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNmM">中レコードのDataTable名</param>
    ''' <param name="tblNmUnsoL">運送(大)レコードのDataTable名</param>
    ''' <param name="tblNmOutS">出荷(小)レコードのDataTable名</param>
    ''' <param name="calcCol">風袋加算フラグ(商品M)のDataRow列名</param>
    ''' <param name="suryoTaniCol">数量単位のDataRow列名</param>
    ''' <param name="stdIrimeCol">標準入目のDataRow列名</param>
    ''' <param name="stdWtCol">標準重量のDataRow列名</param>
    ''' <param name="irimeCol">入目列名　初期値："IRIME"</param>
    ''' <param name="tareYnCol">風袋加算フラグ(運送会社)列名　初期値："TARE_YN"</param>
    ''' <param name="unsoTtlNbCol">運送個数列名　初期値："UNSO_TTL_NB"</param>
    ''' <param name="hasuCol">端数列名　初期値："HASU"</param>
    ''' <param name="sysDelCol">(中)テーブルの削除フラグ列名　初期値："SYS_DEL_FLG"</param>
    ''' <returns>運送重量</returns>
    ''' <remarks></remarks>
    Public Function GetJuryoOutkaData(ByVal ds As DataSet _
                                     , ByVal tblNmM As String _
                                     , ByVal tblNmUnsoL As String _
                                     , ByVal tblNmOutS As String _
                                     , ByVal calcCol As String _
                                     , ByVal suryoTaniCol As String _
                                     , ByVal irisuCol As String _
                                     , ByVal stdIrimeCol As String _
                                     , ByVal stdWtCol As String _
                                     , Optional ByVal irimeCol As String = "IRIME" _
                                     , Optional ByVal tareYnCol As String = "TARE_YN" _
                                     , Optional ByVal unsoTtlNbCol As String = "UNSO_TTL_NB" _
                                     , Optional ByVal hasuCol As String = "HASU" _
                                     , Optional ByVal sysDelCol As String = "SYS_DEL_FLG" _
                                     ) As Decimal

        GetJuryoOutkaData = 0
        Dim mDt As DataTable = ds.Tables(tblNmM)
        Dim mDr As DataRow = Nothing
        Dim max As Integer = mDt.Rows.Count - 1
        If max < 0 Then
            Return 0
        End If
        '(2012.12.25)要望番号1692 容器重量対応 -- START --
        Dim dsCom As DataSet = Nothing
        Dim goodsDtlDt As DataTable = Nothing
        Dim drCom As DataRow = Nothing
        Dim hutaiJuryo As String = String.Empty
        '(2012.12.25)要望番号1692 容器重量対応 --  END  --

        Dim outSDt As DataTable = ds.Tables(tblNmOutS)
        Dim outSDrs As DataRow() = Nothing
        Dim cnt As Integer = 0

        '運送会社の風袋加算フラグ
        Dim tareYnUnso As String = ds.Tables(tblNmUnsoL).Rows(0).Item(tareYnCol).ToString()

        '行数分ループ
        For i As Integer = 0 To max

            mDr = mDt.Rows(i)

            '削除データの場合、スルー
            If LMConst.FLG.ON.Equals(mDr.Item(sysDelCol).ToString()) = True Then
                Continue For
            End If

            outSDrs = outSDt.Select(String.Concat("OUTKA_NO_M = '", mDr.Item("OUTKA_NO_M").ToString(), "' AND SYS_DEL_FLG = '0'"))
            cnt = outSDrs.Length - 1
            If cnt < 0 Then
                Continue For
            End If

            '(2012.12.20)要望番号1692 容器重量対応 -- START --

            'DataSet・変数初期化
            dsCom = New DSL.LMCOMDS
            goodsDtlDt = dsCom.Tables("GOODS_DETAILS_IN")
            drCom = goodsDtlDt.NewRow()
            hutaiJuryo = String.Empty

            '商品明細マスタ抽出条件セット
            drCom.Item("NRS_BR_CD") = mDt.Rows(i).Item("NRS_BR_CD").ToString()
            drCom.Item("GOODS_CD_NRS") = mDt.Rows(i).Item("GOODS_CD_NRS").ToString()
            drCom.Item("SUB_KB") = "16"    '容器重量用SUB_KB
            goodsDtlDt.Rows.Add(drCom)

            '商品明細マスタデータ取得(BLF呼び出し)
            dsCom = ServerAccessLmcom(dsCom, "SelectGoodsDetailsData")

            If dsCom.Tables("GOODS_DETAILS_OUT").Rows.Count <> 0 Then
                hutaiJuryo = dsCom.Tables("GOODS_DETAILS_OUT").Rows(0).Item("SET_NAIYO").ToString   '風袋重量
            End If

            '(2012.12.20)要望番号1692 容器重量対応 --  END  --

            For j As Integer = 0 To cnt

                '(2012.12.06)要望番号1649 小分け時の運送重量対応(ALCTD_QT追加)
                '(2012.12.20)要望番号1692 容器重量対応(hutaiJuryo追加)
                GetJuryoOutkaData += Me.GetJuryoCalcData(mDr.Item(calcCol).ToString() _
                                                   , tareYnUnso _
                                                   , mDr.Item(suryoTaniCol).ToString() _
                                                   , Convert.ToDecimal(outSDrs(j).Item(irimeCol).ToString()) _
                                                   , Convert.ToDecimal(outSDrs(j).Item("ALCTD_NB").ToString()) _
                                                   , Convert.ToDecimal(mDr.Item(stdIrimeCol).ToString()) _
                                                   , Convert.ToDecimal(mDr.Item(stdWtCol).ToString()) _
                                                   , Convert.ToDecimal(outSDrs(j).Item("ALCTD_QT").ToString()) _
                                                   , hutaiJuryo.ToString _
                                                   )
            Next

        Next

        Return GetJuryoOutkaData

    End Function

    ''' <summary>
    ''' 運送梱包個数の合計
    ''' </summary>
    ''' <param name="dt">運送(中)DataTable</param>
    ''' <param name="unsoTtlNbCol">運送個数列名　初期値："UNSO_TTL_NB"</param>
    ''' <param name="hasuCol">端数列名　初期値："HASU"</param>
    ''' <param name="pkgNbCol">梱包個数列名　初期値："PKG_NB"</param>
    ''' <returns>運送梱包個数</returns>
    ''' <remarks></remarks>
    Public Function SetNbData(ByVal dt As DataTable _
                              , Optional ByVal unsoTtlNbCol As String = "UNSO_TTL_NB" _
                              , Optional ByVal hasuCol As String = "HASU" _
                              , Optional ByVal pkgNbCol As String = "PKG_NB" _
                              ) As Decimal

        SetNbData = 0

        Dim max As Integer = dt.Rows.Count - 1
        For i As Integer = max To 0 Step -1
            SetNbData += Me.GetUnsoKonpoData(dt.Rows(i), unsoTtlNbCol, hasuCol, pkgNbCol)
        Next

        Return SetNbData

    End Function

#End Region

#End Region

End Class
