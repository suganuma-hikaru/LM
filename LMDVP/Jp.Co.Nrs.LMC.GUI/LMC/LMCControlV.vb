' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 入荷管理
'  プログラムID     :  LMC010    : 入荷データ検索
'  作  成  者       :  [金ヘスル]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports FarPoint.Win.Spread

''' <summary>
''' LMCControlValidateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMCControlV
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">フォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As Form)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' エラー項目の背景色とフォーカスを設定する
    ''' </summary>
    ''' <param name="ctl">エラーコントロール</param>
    ''' <remarks></remarks>
    Friend Sub SetErrorControl(ByVal ctl As Control _
                               , Optional ByVal tab As Jp.Co.Nrs.LM.GUI.Win.LMTab = Nothing _
                               , Optional ByVal tabPage As System.Windows.Forms.TabPage = Nothing)

        Dim errorColor As System.Drawing.Color = Utility.LMGUIUtility.GetAttentionBackColor()

        If TypeOf ctl Is Win.InputMan.LMImCombo = True Then

            DirectCast(ctl, Win.InputMan.LMImCombo).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMComboKubun = True Then

            DirectCast(ctl, Win.InputMan.LMComboKubun).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMComboNrsBr = True Then

            DirectCast(ctl, Win.InputMan.LMComboNrsBr).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMComboSoko = True Then

            DirectCast(ctl, Win.InputMan.LMComboSoko).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMImNumber = True Then

            DirectCast(ctl, Win.InputMan.LMImNumber).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMImDate = True Then

            DirectCast(ctl, Win.InputMan.LMImDate).BackColorDef = errorColor

        End If

        If tab Is Nothing = False Then
            tab.SelectedTab = tabPage
        End If

        ctl.Focus()
        ctl.Select()

    End Sub


    ''' <summary>
    ''' 未選択チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectChk(ByVal chkCnt As Integer) As Boolean

        'チェック件数が0件
        If chkCnt = 0 Then

            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' 単一行選択チェック
    ''' </summary>
    ''' <param name="chkCnt">チェック行カウント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectOneChk(ByVal chkCnt As Integer) As Boolean

        If 1 < chkCnt Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 過去日チェック
    ''' </summary>
    ''' <param name="dateFrom">From日</param>
    ''' <param name="dateTo">To日</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFromToChk(ByVal dateFrom As String, ByVal dateTo As String) As Boolean

        'From日よりTo日が過去日の場合エラー
        'いずれも設定済である場合のみチェック
        If dateFrom.Equals(String.Empty) = False _
            And dateTo.Equals(String.Empty) = False Then

            If dateTo < dateFrom Then

                '出荷検索日Fromより出荷検索日Toが過去日の場合エラー
                Return False

            End If

        End If

        Return True

    End Function

    ''' <summary>
    ''' 相関チェック
    ''' </summary>
    ''' <param name="ctl1"></param>
    ''' <param name="ctl2"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRelationChk(ByVal ctl1 As String, ByVal ctl2 As String) As Boolean

        '引数の片方が入力されていて、片方に空があった場合はFalseをリターンする
        If String.IsNullOrEmpty(ctl1) = False Then

            If String.IsNullOrEmpty(ctl2) = True Then

                Return False

            End If

        Else

            If String.IsNullOrEmpty(ctl2) = False Then

                Return False

            End If

        End If

        Return True

    End Function

    ''' <summary>
    ''' スプレッドでチェックの付いたRowIndexを取得
    ''' </summary>
    ''' <param name="defNo">チェックボックスセルのカラム№</param>
    ''' <param name="sprDetail">対象スプレッド</param>
    ''' <returns>リスト</returns>
    ''' <remarks>チェックのある行のRowIndexをリストに入れて戻す</remarks>
    Friend Overloads Function SprSelectList(ByVal defNo As Integer, ByVal sprDetail As Spread.LMSpreadSearch) As ArrayList

        With sprDetail.ActiveSheet

            Dim list As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If Me.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    list.Add(i)
                End If
            Next

            Return list

        End With

    End Function

    ''' <summary>
    ''' スプレッドでチェックの付いたRowIndexを取得
    ''' </summary>
    ''' <param name="defNo">チェックボックスセルのカラム№</param>
    ''' <param name="sprDetail">対象スプレッド</param>
    ''' <returns>リスト</returns>
    ''' <remarks>チェックのある行のRowIndexをリストに入れて戻す</remarks>
    Friend Overloads Function SprSelectList(ByVal defNo As Integer, ByRef sprDetail As Spread.LMSpread) As ArrayList

        With sprDetail.ActiveSheet

            Dim list As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If Me.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    list.Add(i)
                End If
            Next

            Return list

        End With

    End Function

    ''' <summary>
    ''' マスタ存在チェック
    ''' </summary>
    ''' <param name="tbl">キャッシューテーブル名</param>
    ''' <param name="whereStr">条件分</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsExistMst(ByVal tbl As String, ByVal whereStr As String) As Boolean

        ''存在チェック
        Dim dr As DataRow() = MyBase.GetLMCachedDataTable(tbl).Select(whereStr)
        If dr.Length() = 0 Then
            '存在エラー時
            Return False
        Else

        End If

        Return True

    End Function

    ''' <summary>
    ''' MAXSEQチェック
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="colNm">列名</param>
    ''' <param name="rowCnt">行数</param>
    ''' <param name="replaceStr">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsMaxSeqChk(ByVal dr As DataRow _
                                   , ByVal colNm As String _
                                   , ByVal rowCnt As Integer _
                                   , ByVal replaceStr As String _
                                   ) As Boolean

        If 999 < rowCnt + Convert.ToInt32(dr.Item(colNm).ToString()) Then
            MyBase.ShowMessage("E062", New String() {replaceStr})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' MAXSEQチェック
    ''' </summary>
    ''' <param name="chkNo">値</param>
    ''' <param name="maxNo">Max値</param>
    ''' <param name="chkNm">項目名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsMaxChk(ByVal chkNo As Integer _
                                   , ByVal maxNo As Integer _
                                   , ByVal chkNm As String _
                                   ) As Boolean

        If maxNo < chkNo Then
            MyBase.ShowMessage("E062", New String() {chkNm})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="custSCd">荷主(小)コード</param>
    ''' <param name="custSSCd">荷主(極小)コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectCustListDataRow(ByVal custLCd As String _
                                          , Optional ByVal custMCd As String = "" _
                                          , Optional ByVal custSCd As String = "" _
                                          , Optional ByVal custSSCd As String = "" _
                                          ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(Me.SelectCustString(custLCd, custMCd, custSCd, custSSCd))

    End Function

    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="custSCd">荷主(小)コード</param>
    ''' <param name="custSSCd">荷主(極小)コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectCustString(ByVal custLCd As String _
                                     , ByVal custMCd As String _
                                     , ByVal custSCd As String _
                                     , ByVal custSSCd As String _
                                     ) As String

        SelectCustString = String.Empty

        '削除フラグ
        SelectCustString = String.Concat(SelectCustString, " SYS_DEL_FLG = '0' ")

        '荷主コード（大）
        SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_L = ", " '", custLCd, "' ")

        If String.IsNullOrEmpty(custMCd) = False Then

            '荷主コード（中）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_M = ", " '", custMCd, "' ")

        End If

        If String.IsNullOrEmpty(custSCd) = False Then

            '荷主コード（小）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_S = ", " '", custSCd, "' ")

        End If

        If String.IsNullOrEmpty(custSSCd) = False Then

            '荷主コード（極小）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_SS = ", " '", custSSCd, "' ")

        End If

        Return SelectCustString

    End Function
    ''' <summary>
    ''' 倉庫マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="whCd">倉庫コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectSokoListDataRow(ByVal whCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SOKO).Select(Me.SelectSokoString(whCd))

    End Function

    '要望番号:1350 terakawa 2012.08.27 Start
    ''' <summary>
    ''' 倉庫マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="whCd">倉庫コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectSokoString(ByVal whCd As String) As String

        SelectSokoString = String.Empty

        '削除フラグ
        SelectSokoString = String.Concat(SelectSokoString, " SYS_DEL_FLG = '0' ")

        '倉庫コード
        SelectSokoString = String.Concat(SelectSokoString, " AND ", "WH_CD = ", " '", whCd, "' ")

        Return SelectSokoString

    End Function
    '要望番号:1350 terakawa 2012.08.27 End


    ''' <summary>
    ''' 運賃タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <param name="tariffCdEda">運賃タリフコード枝番</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectUnchinTariffListDataRow(ByVal tariffCd As String _
                                                  , Optional ByVal tariffCdEda As String = "" _
                                                  , Optional ByVal startDate As String = "" _
                                                  ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF).Select(Me.SelectUnchinTariffString(tariffCd, tariffCdEda, startDate))

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <param name="tariffCdEda">運賃タリフコード枝番</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinTariffString(ByVal tariffCd As String _
                                              , Optional ByVal tariffCdEda As String = "" _
                                              , Optional ByVal startDate As String = "" _
                                              ) As String

        SelectUnchinTariffString = String.Empty

        '運賃タリフコード()
        SelectUnchinTariffString = String.Concat(SelectUnchinTariffString, " UNCHIN_TARIFF_CD = ", " '", tariffCd, "' ")

        '運賃タリフコード枝番
        If String.IsNullOrEmpty(tariffCdEda) = False Then
            SelectUnchinTariffString = String.Concat(SelectUnchinTariffString, " AND ", "UNCHIN_TARIFF_CD_EDA = ", " '", tariffCdEda, "' ")
        End If

        '適用開始日
        If String.IsNullOrEmpty(startDate) = False Then
            SelectUnchinTariffString = String.Concat(SelectUnchinTariffString, " AND ", "STR_DATE <= ", " '", startDate, "' ")
        End If

        Return SelectUnchinTariffString

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="tariffCd">横持ちタリフコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectYokoTariffListDataRow(ByVal brCd As String, ByVal tariffCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.YOKO_TARIFF_HD).Select(Me.SelectYokoTariffString(brCd, tariffCd))

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="tariffCd">横持ちタリフコード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectYokoTariffString(ByVal brCd As String, ByVal tariffCd As String) As String

        SelectYokoTariffString = String.Empty

        '削除フラグ
        SelectYokoTariffString = String.Concat(SelectYokoTariffString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectYokoTariffString = String.Concat(SelectYokoTariffString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '横持ちタリフコード
        SelectYokoTariffString = String.Concat(SelectYokoTariffString, " AND ", "YOKO_TARIFF_CD = ", " '", tariffCd, "' ")

        Return SelectYokoTariffString

    End Function

    'START YANAI 要望番号1386 タリフ分類区分を変更した際に誤ったタリフを設定する
    '''' <summary>
    '''' 運賃タリフセットマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    '''' </summary>
    '''' <param name="brCd">営業所コード</param>
    '''' <param name="custLCd">荷主(大)コード</param>
    '''' <param name="custMCd">荷主(中)コード</param>
    '''' <param name="setCd">セットマスタコード</param>
    '''' <param name="tehaiKbn">タリフ分類区分</param>
    '''' <param name="tariffCd1">運賃タリフコード（屯キロ建）</param>
    '''' <param name="tariffCd2">運賃タリフコード（車建）</param>
    '''' <param name="yokoCd">横持ちタリフコード</param>
    '''' <returns>データロウ配列</returns>
    '''' <remarks></remarks>
    'Friend Function SelectTariffSetListDataRow(ByVal brCd As String _
    '                                           , ByVal custLCd As String _
    '                                           , ByVal custMCd As String _
    '                                           , ByVal setCd As String _
    '                                           , Optional ByVal tehaiKbn As String = "" _
    '                                           , Optional ByVal tariffCd1 As String = "" _
    '                                           , Optional ByVal tariffCd2 As String = "" _
    '                                           , Optional ByVal yokoCd As String = "" _
    '                                           ) As DataRow()

    '    'キャッシュテーブルからデータ抽出
    '    Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_SET).Select(Me.SelectTariffSetString(brCd, custLCd, custMCd, setCd, tehaiKbn, tariffCd1, tariffCd2, yokoCd))

    'End Function
    ''' <summary>
    ''' 運賃タリフセットマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="setCd">セットマスタコード</param>
    ''' <param name="tehaiKbn">タリフ分類区分</param>
    ''' <param name="tariffCd1">運賃タリフコード（屯キロ建）</param>
    ''' <param name="tariffCd2">運賃タリフコード（車建）</param>
    ''' <param name="yokoCd">横持ちタリフコード</param>
    ''' <param name="destCd">届先コード</param>
    ''' <param name="setKb">セット区分</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectTariffSetListDataRow(ByVal brCd As String _
                                               , ByVal custLCd As String _
                                               , ByVal custMCd As String _
                                               , ByVal setCd As String _
                                               , Optional ByVal tehaiKbn As String = "" _
                                               , Optional ByVal tariffCd1 As String = "" _
                                               , Optional ByVal tariffCd2 As String = "" _
                                               , Optional ByVal yokoCd As String = "" _
                                               , Optional ByVal destCd As String = "" _
                                               , Optional ByVal setKb As String = "" _
                                               ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_SET).Select(Me.SelectTariffSetString(brCd, custLCd, custMCd, setCd, tehaiKbn, tariffCd1, tariffCd2, yokoCd, destCd, setKb))

    End Function
    'END YANAI 要望番号1386 タリフ分類区分を変更した際に誤ったタリフを設定する

    'START YANAI 要望番号1386 タリフ分類区分を変更した際に誤ったタリフを設定する
    '''' <summary>
    '''' 運賃タリフセットマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    '''' </summary>
    '''' <param name="brCd">営業所コード</param>
    '''' <param name="custLCd">荷主(大)コード</param>
    '''' <param name="custMCd">荷主(中)コード</param>
    '''' <param name="setCd">セットマスタコード</param>
    '''' <param name="tehaiKbn">タリフ分類区分</param>
    '''' <param name="tariffCd1">運賃タリフコード（屯キロ建）</param>
    '''' <param name="tariffCd2">運賃タリフコード（車建）</param>
    '''' <returns>データロウ配列</returns>
    '''' <remarks></remarks>
    'Private Function SelectTariffSetString(ByVal brCd As String _
    '                                       , ByVal custLCd As String _
    '                                       , ByVal custMCd As String _
    '                                       , ByVal setCd As String _
    '                                       , ByVal tehaiKbn As String _
    '                                       , ByVal tariffCd1 As String _
    '                                       , ByVal tariffCd2 As String _
    '                                       , ByVal yokoCd As String _
    '                                       ) As String

    '    SelectTariffSetString = String.Empty

    '    '営業所コード
    '    SelectTariffSetString = String.Concat(SelectTariffSetString, " NRS_BR_CD = ", " '", brCd, "' ")

    '    '荷主(大)コード
    '    SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "CUST_CD_L = ", " '", custLCd, "' ")

    '    '荷主(中)コード
    '    SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "CUST_CD_M = ", " '", custMCd, "' ")

    '    'セットマスタコード
    '    If String.IsNullOrEmpty(setCd) = False Then

    '        SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "SET_MST_CD = ", " '", setCd, "' ")

    '    End If

    '    'セット区分（キャッシュにセット区分なし）
    '    If String.IsNullOrEmpty(tehaiKbn) = False Then

    '        SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "TARIFF_BUNRUI_KB = ", " '", tehaiKbn, "' ")

    '    End If

    '    'タリフコード1
    '    If String.IsNullOrEmpty(tariffCd1) = False Then

    '        SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "UNCHIN_TARIFF_CD1 = ", " '", tariffCd1, "' ")

    '    End If

    '    'タリフコード2
    '    If String.IsNullOrEmpty(tariffCd2) = False Then

    '        SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "UNCHIN_TARIFF_CD2 = ", " '", tariffCd2, "' ")

    '    End If

    '    '横持ちタリフコード
    '    If String.IsNullOrEmpty(yokoCd) = False Then

    '        SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "YOKO_TARIFF_CD = ", " '", yokoCd, "' ")

    '    End If

    '    Return SelectTariffSetString

    'End Function
    ''' <summary>
    ''' 運賃タリフセットマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="setCd">セットマスタコード</param>
    ''' <param name="tehaiKbn">タリフ分類区分</param>
    ''' <param name="tariffCd1">運賃タリフコード（屯キロ建）</param>
    ''' <param name="tariffCd2">運賃タリフコード（車建）</param>
    ''' <param name="yokoCd">横持ちタリフコード</param>
    ''' <param name="destCd">届先コード</param>
    ''' <param name="setKb">セット区分</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Private Function SelectTariffSetString(ByVal brCd As String _
                                           , ByVal custLCd As String _
                                           , ByVal custMCd As String _
                                           , ByVal setCd As String _
                                           , ByVal tehaiKbn As String _
                                           , ByVal tariffCd1 As String _
                                           , ByVal tariffCd2 As String _
                                           , ByVal yokoCd As String _
                                           , ByVal destCd As String _
                                           , ByVal setKb As String _
                                           ) As String

        SelectTariffSetString = String.Empty

        '営業所コード
        SelectTariffSetString = String.Concat(SelectTariffSetString, " NRS_BR_CD = ", " '", brCd, "' ")

        '荷主(大)コード
        SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "CUST_CD_L = ", " '", custLCd, "' ")

        '荷主(中)コード
        SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "CUST_CD_M = ", " '", custMCd, "' ")

        'セットマスタコード
        If String.IsNullOrEmpty(setCd) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "SET_MST_CD = ", " '", setCd, "' ")

        End If

        'セット区分（キャッシュにセット区分なし）
        If String.IsNullOrEmpty(tehaiKbn) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "TARIFF_BUNRUI_KB = ", " '", tehaiKbn, "' ")

        End If

        'タリフコード1
        If String.IsNullOrEmpty(tariffCd1) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "UNCHIN_TARIFF_CD1 = ", " '", tariffCd1, "' ")

        End If

        'タリフコード2
        If String.IsNullOrEmpty(tariffCd2) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "UNCHIN_TARIFF_CD2 = ", " '", tariffCd2, "' ")

        End If

        '横持ちタリフコード
        If String.IsNullOrEmpty(yokoCd) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "YOKO_TARIFF_CD = ", " '", yokoCd, "' ")

        End If

        '届先コード
        If String.IsNullOrEmpty(destCd) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "DEST_CD = ", " '", destCd, "' ")

        End If

        'セット区分
        If String.IsNullOrEmpty(setKb) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "SET_KB = ", " '", setKb, "' ")

        End If

        Return SelectTariffSetString

    End Function
    'END YANAI 要望番号1386 タリフ分類区分を変更した際に誤ったタリフを設定する

#End Region

#Region "SPREAD関連"

    ''' <summary>
    ''' セルから値を取得
    ''' </summary>
    ''' <param name="aCell">セル</param>
    ''' <returns>取得した値</returns>
    ''' <remarks></remarks>
    Friend Function GetCellValue(ByVal aCell As Cell) As String

        GetCellValue = String.Empty

        If TypeOf aCell.Editor Is CellType.ComboBoxCellType Then

            'コンボボックスの場合、Value値を返却
            If aCell.Value Is Nothing = False Then
                GetCellValue = aCell.Value.ToString()
            End If

        ElseIf TypeOf aCell.Editor Is CellType.CheckBoxCellType Then

            'チェックボックスの場合、Booleanの値をStringに変換
            If aCell.Text.Equals("True") = True Then
                GetCellValue = LMConst.FLG.ON
            ElseIf aCell.Text.Equals("False") = True Then
                GetCellValue = LMConst.FLG.OFF
            Else
                GetCellValue = aCell.Text
            End If

        ElseIf TypeOf aCell.Editor Is CellType.NumberCellType Then

            'ナンバーの場合、Value値を返却
            If aCell.Value Is Nothing = False Then
                GetCellValue = aCell.Value.ToString()
            Else
                GetCellValue = 0.ToString()
            End If

        ElseIf TypeOf aCell.Editor Is CellType.DateTimeCellType Then

            '日付の場合、Value値を yyyyMMdd に変換して返却
            If aCell.Value Is Nothing = False AndAlso String.IsNullOrEmpty(aCell.Value.ToString()) = False Then
                GetCellValue = Convert.ToDateTime(aCell.Value).ToString("yyyyMMdd")
            End If

        Else
            'テキストの場合、Trimした値を返却
            GetCellValue = aCell.Text.Trim()

        End If

        Return GetCellValue

    End Function

    ''' <summary>
    ''' スプレッドの値をTrim
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Friend Sub TrimSpaceSprTextvalue(ByVal spr As Win.Spread.LMSpread, ByVal rowNo As Integer)

        With spr

            Dim colMax As Integer = .ActiveSheet.Columns.Count - 1
            Dim aCell As Cell = Nothing

            For i As Integer = 0 To colMax

                aCell = .ActiveSheet.Cells(rowNo, i)

                If TypeOf aCell.Editor Is CellType.ComboBoxCellType = True _
                    OrElse TypeOf aCell.Editor Is CellType.CheckBoxCellType = True _
                    OrElse TypeOf aCell.Editor Is CellType.DateTimeCellType = True _
                    OrElse TypeOf aCell.Editor Is CellType.NumberCellType = True _
                    Then
                    '処理なし
                Else
                    .SetCellValue(rowNo, i, Me.GetCellValue(.ActiveSheet.Cells(rowNo, i)))
                End If

            Next

        End With

    End Sub
#End Region

#Region "エラーメッセージ設定"

    ''' <summary>
    ''' メッセージ設定
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetErrMessage(ByVal id As String) As Boolean

        MyBase.ShowMessage(id)
        Return False

    End Function

    ''' <summary>
    ''' メッセージ設定
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetErrMessage(ByVal id As String, ByVal msg As String()) As Boolean

        MyBase.ShowMessage(id, msg)
        Return False

    End Function

    ''' <summary>
    ''' フォーカス位置エラーのメッセージ設定
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetFocusErrMessage() As Boolean

        Return Me.SetErrMessage("G005")

    End Function

    ''' <summary>
    ''' マスタ存在チェックエラーのメッセージ設定
    ''' </summary>
    ''' <param name="value1">置換文字1</param>
    ''' <param name="value2">置換文字2</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetMstErrMessage(ByVal value1 As String, ByVal value2 As String) As Boolean

        Return Me.SetErrMessage("E079", New String() {value1, value2})

    End Function

#End Region

End Class
