' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM040H : 届先マスタメンテナンス
'  作  成  者       :  [金へスル]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMM040BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM040BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM040DAC = New LMM040DAC()

    '2017/09/25 修正 李↓
    ''20151106 tsunehira add
    ' ''' <summary>
    ' ''' 選択した言語を格納するフィールド
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _LangFlg As String = MessageManager.MessageLanguage
    '2017/09/25 修正 李↑

#End Region

#Region "Const"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMM040IN"

    ''' <summary>
    ''' OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMM040OUT"

    ''' <summary>
    ''' DEST_DETAILSテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_OUT2 As String = "LMM040_DEST_DETAILS"

    ''' <summary>
    ''' UNCHIN_TARIFF_SET_MAXCDテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_UNCHIN_TARIFF_SET_MAXCD As String = "LMM040_UNCHIN_TARIFF_SET_MAXCD"

    ''' <summary>
    ''' 郵便番号マスタ(メッセージ用の名称)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ZIP_M As String = "郵便番号マスタ"

    ''' <summary>
    ''' 郵便番号(メッセージ用の名称)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ZIP As String = "郵便番号"

    ''' <summary>
    ''' JISコード(メッセージ用の名称)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const JIS As String = "JISコード"

    ''' <summary>
    ''' 運賃タリフセットマスタ情報(メッセージ用の名称)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const UNCHIN_TARIFF_SET As String = "運賃タリフセットマスタ情報"


    '2015.11.02 tusnehira add
    '英語化対応
    Private Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Private Const MESSEGE_LANGUAGE_ENGLISH As String = "1"

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 届先マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 届先マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '届先マスタデータ取得
        ds = Me.DacAccess(ds, "SelectListData")

        '届先明細データ取得
        ds = Me.DacAccess(ds, "SelectListData2")

        Return ds

    End Function

    ''' <summary>
    ''' 郵便番号マスタ情報データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectZipJisData(ByVal ds As DataSet) As DataSet

        '郵便番号マスタデータ取得
        ds = Me.DacAccess(ds, "SelectZipJisData")

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 届先マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistUserM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistDestM", ds)

        '処理件数による判定
        If MyBase.GetResultCount() > 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E010")
        End If

        Return ds

    End Function


    ''' <summary>
    ''' 届先マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaUserM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectDestM", ds)

        'MAX届先コード枝番のデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectMaxDestCdEdaData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 一括登録チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function ImportChk(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "ImportChk", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 届先マスタの存在チェック（一括登録）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function ImportExistChk(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistDestM", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 届先マスタ登録（一括登録）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function ImportInsertData(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "InsertDestM", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 届先マスタ更新（一括登録）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function ImportUpdateData(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "UpdateDestMImport", ds)

        Return ds

    End Function

#Region "新規登録"

    ''' <summary>
    ''' 届先マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        '新規登録
        Call Me.InsertData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As Boolean

        'MAXセットマスタコードのデータ取得
        Dim rtnResult As Boolean = Me.SelectTariffSetCdData(ds)

        '届先情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertDestData(ds)

        '届先明細情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelDestDetailsData(ds)

        '届先明細情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertDestDetailsData(ds)

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

        '届先情報の更新登録
        Dim rtnResult As Boolean = Me.UpdateDestData(ds)

        '届先明細情報の物理削除()
        rtnResult = rtnResult AndAlso Me.DelDestDetailsData(ds)

        '届先明細情報の新規登録()
        rtnResult = rtnResult AndAlso Me.InsertDestDetailsData(ds)

        Return rtnResult

    End Function

#End Region

#End Region

#Region "削除登録"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteAction(ByVal ds As DataSet) As DataSet

        '削除処理
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

        '届先情報の論理削除
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "DeleteDestM")

        '届先明細情報の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteDestDetailsM")

        '運賃タリフセット情報の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteUnchinTariffSetM")

        Return rtnResult

    End Function

#End Region

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

#End Region

#Region "共通更新"

    ''' <summary>
    ''' 運賃セットマスタ MAXセットマスタコード取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SelectMaxTariffSetMaxCdData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "SelectMaxTariffSetMaxCdData")

    End Function

    ''' <summary>
    ''' 届先 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertDestData(ByVal ds As DataSet) As Boolean

        '画面のタリフ分類区分≠空の場合は、運賃タリフセットマスタの新規登録を行う。
        If String.IsNullOrEmpty(ds.Tables(LMM040BLC.TABLE_NM_IN).Rows(0).Item("TARIFF_BUNRUI_KB").ToString()) = False Then

            Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "InsertDestM")               '届先マスタの新規登録　            
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "InsertUnchinTariffSetM") '運賃タリフセットマスタの新規登録
            Return rtnResult
        Else
            Return Me.ServerChkJudge(ds, "InsertDestM")                                   '届先マスタの新規登録
        End If

    End Function

    ''' <summary>
    ''' 届先 更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateDestData(ByVal ds As DataSet) As Boolean

        '運賃タリフセットマスタの存在チェック用に設定
        ds.Tables(LMM040BLC.TABLE_NM_IN).Rows(0).Item("CUST_CD_M") = "00"
        ds.Tables(LMM040BLC.TABLE_NM_IN).Rows(0).Item("SET_KB") = "01"

        '運賃タリフセットマスタの物理削除をする旨のワーニングメッセージで「OK」の場合、
        '届先マスタの更新処理（UPDATE）、運賃タリフセットマスタの更新処理（DELETE）を行う。
        If ("01").Equals(ds.Tables(LMM040BLC.TABLE_NM_IN).Rows(0).Item("DELETE_FLG")) = True Then
            Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "UpdateDestM")
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DelUnchinTariffSetM")
            Return rtnResult
        Else
            '画面のタリフ分類区分≠空の場合は、運賃タリフセットマスタの新規登録 or 更新を行う。
            If String.IsNullOrEmpty(ds.Tables(LMM040BLC.TABLE_NM_IN).Rows(0).Item("TARIFF_BUNRUI_KB").ToString()) = False Then
                '運賃タリフセットマスタの存在チェック
                ds = MyBase.CallDAC(Me._Dac, "SelectUnchinTariffSetM", ds)
                Dim count As Integer = MyBase.GetResultCount()

                If count < 1 Then
                    '該当データ＝0件の場合                
                    '届先マスタの更新処理（UPDATE）、運賃タリフセットマスタの更新処理（INSERT）を行う。
                    Dim rtnResult As Boolean = Me.SelectTariffSetCdData(ds)     'MAXセットマスタコードのデータ取得
                    rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateDestM")
                    rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "InsertUnchinTariffSetM")
                    Return rtnResult
                Else
                    '該当データ＞0件の場合
                    '運賃タリフセットマスタの排他チェック
                    'DACクラス呼出
                    Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "HaitaUnchinTariffSetM")
                    If MyBase.GetMessageID().Equals("E011") = True Then
                        Return rtnResult
                    Else
                        '届先マスタの更新処理（UPDATE）、運賃タリフセットマスタの更新処理（UPDATE）を行う。
                        Dim rtnResult2 As Boolean = Me.ServerChkJudge(ds, "UpdateDestM")
                        rtnResult2 = rtnResult2 AndAlso Me.ServerChkJudge(ds, "UpdateUnchinTariffSetM")
                        Return rtnResult2
                    End If

                End If

            Else
                '運賃タリフセットマスタの存在チェック
                ds = MyBase.CallDAC(Me._Dac, "SelectUnchinTariffSetM", ds)
                Dim count As Integer = MyBase.GetResultCount()

                If count < 1 Then
                    '該当データ＝0件の場合                
                    Return Me.ServerChkJudge(ds, "UpdateDestM")                                   '届先マスタの更新
                Else
                    '該当データ＞0件の場合
                    '運賃タリフセットマスタの物理削除をする旨のワーニングメッセージを表示する。
                    MyBase.SetMessage("W155", New String() {LMM040BLC.UNCHIN_TARIFF_SET})
                End If

            End If
        End If

    End Function


    ''' <summary>
    ''' 届先明細 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DelDestDetailsData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "DelDestDetailsM")

    End Function

    ''' <summary>
    ''' 届先明細 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertDestDetailsData(ByVal ds As DataSet) As Boolean

        '新規登録する明細データがない場合、処理終了
        If ds.Tables(LMM040BLC.TABLE_NM_OUT2).Rows.Count = 0 Then
            Return True
        Else
            Return Me.ServerChkJudge(ds, "InsertDestDetailsM")
        End If

    End Function

    ''2011.09.08 検証結果_導入時要望№1対応 START
    '''' <summary>
    '''' 郵便マスタ存在チェック
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet（プロキシ）</returns>
    '''' <remarks></remarks>
    'Private Function CheckZipM(ByVal ds As DataSet) As DataSet

    '    If String.IsNullOrEmpty(ds.Tables(LMM040BLC.TABLE_NM_IN).Rows(0).Item("ZIP").ToString()) = False Then
    '        'DACクラス呼出
    '        ds = MyBase.CallDAC(Me._Dac, "CheckExistZipM", ds)
    '        '処理件数による判定
    '        If MyBase.GetResultCount() = 0 Then
    '            '0件の場合、存在なしエラーメッセージを設定する
    '            MyBase.SetMessage("E079", New String() {LMM040BLC.ZIP_M, LMM040BLC.ZIP})
    '        End If
    '    End If


    '    Return ds

    'End Function
    ''2011.09.08 検証結果_導入時要望№1対応 END

    ''' <summary>
    ''' 郵便マスタ存在チェック(郵便番号とJISコードで存在チェックをする)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckZipJisM(ByVal ds As DataSet) As DataSet

        If String.IsNullOrEmpty(ds.Tables(LMM040BLC.TABLE_NM_IN).Rows(0).Item("ZIP").ToString()) = False AndAlso _
         String.IsNullOrEmpty(ds.Tables(LMM040BLC.TABLE_NM_IN).Rows(0).Item("JIS").ToString()) = False Then

            'DACクラス呼出
            ds = MyBase.CallDAC(Me._Dac, "CheckExistZipJisM", ds)

            '処理件数による判定
            If MyBase.GetResultCount() = 0 Then

                '0件の場合、郵便番号マスタから住所1～3・JISコードを取得する。
                'DACクラス呼出
                ds = MyBase.CallDAC(Me._Dac, "SelectZipJisData", ds)

                '画面の郵便番号がマスタに存在しない場合、JISコードが取得できないためワーニング
                If ds.Tables("LMM040OUT").Rows.Count = 0 Then
                    MyBase.SetMessage("W176", New String() {LMM040BLC.ZIP, LMM040BLC.JIS})
                Else
                    MyBase.SetMessage("W129", New String() {LMM040BLC.JIS, LMM040BLC.ZIP})
                End If

            End If
        End If

        Return ds

    End Function

    ''' <summary>
    ''' MAXセットマスタコードの採番
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SelectTariffSetCdData(ByVal ds As DataSet) As Boolean

        'MAXセットマスタコードのデータ取得
        Dim rtnResult As Boolean = Me.SelectMaxTariffSetMaxCdData(ds)

        '最大セットマスタコードの設定
        Dim oldMaxSetCd As String = String.Empty
        If String.IsNullOrEmpty(ds.Tables(LMM040BLC.TABLE_NM_UNCHIN_TARIFF_SET_MAXCD).Rows(0).Item("SET_MST_MAXCD").ToString()) = True Then
            oldMaxSetCd = "0"
        Else
            oldMaxSetCd = ds.Tables(LMM040BLC.TABLE_NM_UNCHIN_TARIFF_SET_MAXCD).Rows(0).Item("SET_MST_MAXCD").ToString()
        End If
        'セットマスタコードの採番
        Dim newMaxSetCd As String = String.Empty
        If ("0").Equals(oldMaxSetCd) = True Then
            '(2012.06.22)要望番号1178 セットマスタコードを3桁⇒4桁
            'newMaxSetCd = Me.SetZeroData(oldMaxSetCd, "000")
            newMaxSetCd = Me.SetZeroData(oldMaxSetCd, "0000")
        Else
            Dim maxSetCd As Integer = Convert.ToInt32(oldMaxSetCd) + 1

            '上限チェック
            If Me.IsMaxSetCdChk(maxSetCd) = False Then
                Return False
            End If

            '(2012.06.22)要望番号1178 セットマスタコードを3桁⇒4桁
            'newMaxSetCd = Me.SetZeroData(maxSetCd.ToString(), "000")
            newMaxSetCd = Me.SetZeroData(maxSetCd.ToString(), "0000")

        End If

        '採番したセットマスタコードをLMM040INに設定する
        ds.Tables(LMM040BLC.TABLE_NM_IN).Rows(0).Item("SET_MST_CD") = newMaxSetCd

        Return rtnResult

    End Function

    ''' <summary>
    ''' 前ゼロ設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="keta">前につけるゼロ</param>
    ''' <returns>設定値</returns>
    ''' <remarks></remarks>
    Private Function SetZeroData(ByVal value As String, ByVal keta As String) As String

        SetZeroData = String.Concat(keta, value)

        Dim ketasu As Integer = keta.Length

        Return SetZeroData.Substring(SetZeroData.Length - ketasu, ketasu)

    End Function

    ''' <summary>
    ''' セットコード限界値チェック
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMaxSetCdChk(ByVal value As Integer) As Boolean

        If 9999 < value Then
            '20151029 tsunehira add Start
            '英語化対応
            MyBase.SetMessage("E815")
            '20151029 tsunehira add End
            'MyBase.SetMessage("E062", New String() {"セットマスタコード"})
            Return False
        End If

        Return True

    End Function

#End Region

#End Region

End Class
