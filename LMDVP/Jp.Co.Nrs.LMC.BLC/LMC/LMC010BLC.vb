' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC010    : 出荷データ検索
'  作  成  者       :  [金ヘスル]:
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMC010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC010BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC010DAC = New LMC010DAC()

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

    '2015.10.20 tusnehira add
    '英語化対応
    Private Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Private Const MESSEGE_LANGUAGE_ENGLISH As String = "1"



#If True Then ' 西濃自動送り状番号出力対応 20160701 added inoue

    ''' <summary>
    ''' 自動採番送り状区分(O010)
    ''' </summary>
    ''' <remarks></remarks>
    Class AUTO_DENP_KBN

        ''' <summary>
        ''' 名鉄運輸
        ''' </summary>
        ''' <remarks></remarks>
        Public Const MEITETSU_TRANSPORT As String = "01"

        ''' <summary>
        ''' 西濃運輸千葉(標準)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SEINO_TRANSPORTATION_CHIBA As String = "02"

        ''' <summary>
        ''' トールエクスプレス(大阪)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TTOLL_EXPRESS_OSAKA As String = "03"

        '2017/12/12 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add start
        ''' <summary>
        ''' トールエクスプレス(群馬)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TTOLL_EXPRESS_GUNMA As String = "04"

        '2017/12/12 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add end

        ''' <summary>
        ''' 西濃運輸土気(標準)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SEINO_TRANSPORTATION_TOKE As String = "05"

        ''' <summary>
        ''' JPロジスティクス[元トールエクスプレス](千葉)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TTOLL_EXPRESS_CHIBA As String = "06"

        ''' <summary>
        ''' JPロジスティクス[元トールエクスプレス](土気)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TTOLL_EXPRESS_TOKE As String = "07"

        ''' <summary>
        ''' 西濃運輸(袖ヶ浦)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SEINO_TRANSPORTATION_SODEGAURA As String = "08"

        ''' <summary>
        ''' 西濃運輸(大阪)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SEINO_TRANSPORTATION_OSAKA As String = "09"

    End Class

#End If

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Class TABLE_NM
        Public Const LMC010_INT_EDI As String = LMC010DAC.TABLE_NM.LMC010_INT_EDI
    End Class


    ''' <summary>
    ''' 【区分Ｍ】保留品区分(H003)
    ''' </summary>
    ''' <remarks></remarks>
    Private Class SPD_KB

        ''' <summary>
        ''' 出荷可能
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ShipOK As String = "01"
        ''' <summary>
        ''' 保留
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Pending As String = "02"

        ''' <summary>
        ''' 出荷止め
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ShipNG As String = "03"

        ''' <summary>
        ''' 廃棄予定
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Waste As String = "04"

        ''' <summary>
        ''' 保税陸上運送(OLT)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OverLandTransport As String = "05"

        ''' <summary>
        ''' 倉入保税貨物 (IS)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ImportForStorage As String = "06"

        ''' <summary>
        ''' 直接輸入(IC)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ImportForConsumption As String = "07"

        ''' <summary>
        ''' 蔵出輸入(ISW)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ImportFromStorageWarehouse As String = "08"


    End Class

    ''' <summary>
    ''' 【区分Ｍ】簿外品区分(B002)
    ''' </summary>
    ''' <remarks></remarks>
    Private Class OFB_KB

        ''' <summary>
        ''' 簿品
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Normal As String = "01"

        ''' <summary>
        ''' 簿外品
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OffBook As String = "02"
    End Class


    ''' <summary>
    ''' 【区分Ｍ】割当優先区分(W001)
    ''' </summary>
    ''' <remarks></remarks>
    Private Class ALLOC_PRIORITY

        ''' <summary>
        ''' 最優先
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TopPriority As String = "01"

        ''' <summary>
        ''' フリー
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Free As String = "10"

        ''' <summary>
        ''' リザーブ
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Reserve As String = "20"

    End Class

    ''' <summary>
    ''' 【区分Ｍ】有無フラグ(U009)
    ''' </summary>
    ''' <remarks>
    ''' 　00：無　01：有
    ''' </remarks>
    Private Class EXISTENCE_STATUS
        ''' <summary>
        ''' 無
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NO As String = "00"

        ''' <summary>
        ''' 有
        ''' </summary>
        ''' <remarks></remarks>
        Public Const YES As String = "01"
    End Class

    ''' <summary>
    ''' 【区分Ｍ】入荷データ種別
    ''' </summary>
    ''' <remarks></remarks>
    Private Class INKA_TP
        ''' <summary>
        ''' 通常 
        ''' </summary>
        ''' <remarks></remarks>
        Public Const General As String = "10"

        ''' <summary>
        ''' 返品
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ReturnItem As String = "20"

        ''' <summary>
        '''  倉庫間移動
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Movement As String = "30"

        ''' <summary>
        '''  振替
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Transfer As String = "50"
    End Class

    ''' <summary>
    ''' 【区分Ｍ】入荷データ区分(N006)
    ''' </summary>
    ''' <remarks></remarks>
    Private Class INKA_KB

        ''' <summary>
        ''' 通常
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Regular As String = "10"

        ''' <summary>
        ''' 名目（容変有り）
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NominalCC As String = "20"


        ''' <summary>
        ''' 名目（容変無し）
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NominalNC As String = "30"

    End Class

    ''' <summary>
    ''' 【区分Ｍ】入荷作業進捗(N004)
    ''' </summary>
    ''' <remarks></remarks>
    Private Class INKA_STATE_KB
        ''' <summary>
        ''' 予定入力済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const AlreadyScheduledInput As String = "10"

        ''' <summary>
        ''' 受付表印刷
        ''' </summary>
        ''' <remarks></remarks>
        Public Const AcceptanceVotePrint As String = "20"

        ''' <summary>
        ''' 受付済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const AcceptanceSettled As String = "30"

        ''' <summary>
        ''' 検品済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const InspectionCompleted As String = "40"

        ''' <summary>
        ''' 入荷済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const AlreadyInStock As String = "50"

        ''' <summary>
        ''' 報告済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Reported As String = "90"
    End Class

    ''' <summary>
    ''' 【区分Ｍ】税区分(Z001)
    ''' </summary>
    ''' <remarks></remarks>
    Private Class TAX_KB

        ''' <summary>
        ''' 日本
        ''' </summary>
        ''' <remarks></remarks>
        Public Class JP
            ''' <summary>
            ''' 課税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const Tax As String = "01"

            ''' <summary>
            ''' 免税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxFree As String = "02"

            ''' <summary>
            ''' 非課税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxExempt As String = "03"

            ''' <summary>
            ''' 内税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxIncluded As String = "04"
        End Class


        ''' <summary>
        ''' 韓国
        ''' </summary>
        ''' <remarks></remarks>
        Public Class KR
            ''' <summary>
            ''' 課税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const Tax As String = "05"

            ''' <summary>
            ''' 免税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxFree As String = "06"

            ''' <summary>
            ''' 非課税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxExempt As String = "07"

            ''' <summary>
            ''' 内税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxIncluded As String = "08"
        End Class

        ''' <summary>
        ''' 韓国(U)
        ''' </summary>
        ''' <remarks></remarks>
        Public Class KRU
            ''' <summary>
            ''' 課税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const Tax As String = "09"

            ''' <summary>
            ''' 免税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxFree As String = "10"

            ''' <summary>
            ''' 非課税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxExempt As String = "11"

            ''' <summary>
            ''' 内税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxIncluded As String = "12"
        End Class

        ''' <summary>
        ''' 台湾
        ''' </summary>
        ''' <remarks></remarks>
        Public Class TR

            ''' <summary>
            ''' 課税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const Tax As String = "13"

            ''' <summary>
            ''' 免税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxFree As String = "14"

            ''' <summary>
            ''' 非課税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxExempt As String = "15"
        End Class

    End Class


    ''' <summary>
    ''' 【区分Ｍ】運送手配(運送元)区分(U005)
    ''' </summary>
    ''' <remarks>
    ''' 
    ''' </remarks>
    Private Class UNSO_TEHAI_KB

        ''' <summary>
        ''' 日陸手配
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NRS As String = "10"

        ''' <summary>
        ''' 先方手配
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OtherParty As String = "20"

        ''' <summary>
        ''' 未定
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Undecided As String = "90"
    End Class




    ''' <summary>
    ''' 印刷順(最大)
    ''' </summary>
    ''' <remarks></remarks>
    Private PRINT_SORT_MAX_VALUE As Integer = 99

#End Region


#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' データ件数検索
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
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function


    ''' <summary>
    ''' データ検索(一括引当時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectOutkaMメソッド呼出</remarks>
    Private Function SelectOutkaM(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMC010_OUTKA_M_IN")
        Dim outDt As DataTable = ds.Tables("LMC010_OUTKA_M_OUT")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMC010_OUTKA_M_IN")
        Dim setDt As DataTable = setDs.Tables("LMC010_OUTKA_M_OUT")
        Dim count As Integer = 0

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'データの抽出
            rtnResult = Me.ServerChkJudge(setDs, "SelectOutkaM")

            count = MyBase.GetResultCount()

            'エラーの場合はそのまま返却
            If rtnResult = False OrElse count = 0 Then
                Return Nothing
            End If

            'rtnResult = Me.ServerChkJudge(setDs, "SelectCountOutkaM")
            ''エラーの場合はそのまま返却
            'If CInt(setDs.Tables("LMC010_OUTKA_M_CONUNT_OUT").Rows(0).Item("CNT").ToString()) <> count Then
            '    Return Nothing
            'End If

            '値設定
            For j As Integer = 0 To count - 1
                outDt.ImportRow(setDt.Rows(j))
            Next

        Next

        Return ds

    End Function


    ''' <summary>
    ''' データ検索(一括引当時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectOutkaMメソッド呼出</remarks>
    Private Function SelectCountOutkaM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectCountOutkaM", ds)

    End Function

    '2018/01/15 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add start
    Private Function SelectTargetUnsoPre(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectTargetUnsoPre", ds)

    End Function
    '2018/01/15 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add end

    '2013.02.18 アグリマート対応 START
    ''' <summary>
    ''' データ取得(WITｼﾘﾝﾀﾞｰ引当時 出荷管理番号中番/シリアル№取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectOutkaPickWkメソッド呼出</remarks>
    Private Function SelectOutkaPickWk(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMC010_OUTKA_M_IN")
        Dim outDt As DataTable = ds.Tables("LMC010_OUTKA_M_PICK_WK")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMC010_OUTKA_M_IN")
        Dim setDt As DataTable = setDs.Tables("LMC010_OUTKA_M_PICK_WK")
        Dim count As Integer = 0

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'データの抽出
            rtnResult = Me.ServerChkJudge(setDs, "SelectOutkaPickWk")

            count = MyBase.GetResultCount()

            'エラーの場合はそのまま返却
            If rtnResult = False OrElse count = 0 Then
                Return Nothing
            End If

            '値設定
            For j As Integer = 0 To count - 1
                outDt.ImportRow(setDt.Rows(j))
            Next

        Next

        Return ds

    End Function
    '2013.02.18 アグリマート対応 END

    'START KIM 2012/09/19 特定荷主対応
    ''' <summary>
    ''' データ検索(ハネウェルCSV引当時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectOutkaMメソッド呼出</remarks>
    Private Function SelectOutkaMForHW(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMC010_OUTKA_M_IN")
        Dim outDt As DataTable = ds.Tables("LMC010_OUTKA_M_OUT")
        Dim max As Integer = dt.Rows.Count - 1

        'START 2013/3/4 KIM修正 CSV引当対応（出荷番号LMが同じ場合、DB検索を行わない）
        '別インスタンス
        Dim setDs As DataSet = ds.Clone
        Dim inTbl As DataTable = setDs.Tables("LMC010_OUTKA_M_IN")
        Dim setDt As DataTable = setDs.Tables("LMC010_OUTKA_M_OUT")
        Dim count As Integer = 0

        '格納インスタンス
        Dim copyDs As DataSet = ds.Copy()
        Dim preKey As String = String.Empty
        Dim nowKey As String = String.Empty
        Dim strSERIAL_NO As String = String.Empty

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.Clear()
            inTbl.ImportRow(dt.Rows(i))


            nowKey = String.Concat(dt.Rows(i).Item("OUTKA_NO_L").ToString(), dt.Rows(i).Item("OUTKA_NO_M").ToString())
            strSERIAL_NO = dt.Rows(i).Item("SERIAL_NO").ToString()

            'データの抽出
            If nowKey.Equals(preKey) = False Then

                rtnResult = Me.ServerChkJudge(setDs, "SelectOutkaMForHW")
                count = MyBase.GetResultCount()

                'エラーの場合はそのまま返却
                If rtnResult = False OrElse count = 0 Then
                    Return Nothing
                End If

                '格納データセットに取得データを格納
                copyDs = setDs.Copy()

            Else

                '2014/03/18 黎 SetDtの所有権を[copyDs]に剥奪されてしまうのでマージで対応 -- ST --
                '同じ出荷番号LMのデータを格納データセットから取得する
                'setDt = copyDs.Tables("LMC010_OUTKA_M_OUT")
                setDt.Merge(copyDs.Tables("LMC010_OUTKA_M_OUT"))
                '2014/03/18 黎 SetDtの所有権を[copyDs]に剥奪されてしまうのでマージで対応 -- ED --

            End If

            'SERIAL_NO設定
            For k As Integer = 0 To count - 1
                setDt.Rows(k).Item("SERIAL_NO") = strSERIAL_NO
            Next

            '値設定
            For j As Integer = 0 To count - 1
                outDt.ImportRow(setDt.Rows(j))
            Next

            preKey = nowKey
            'END 2013/3/4 KIM修正 CSV引当対応

        Next

        Return ds

    End Function
    'END KIM 2012/09/19 特定荷主対応

    ''' <summary>
    ''' データ検索(分析票発行時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectOutkaMメソッド呼出</remarks>
    Private Function SelectBunsekiOutkaM(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMC010_OUTKA_M_BUNSEKI_IN")
        Dim outDt As DataTable = ds.Tables("LMC010_OUTKA_M_BUNSEKI_OUT")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMC010_OUTKA_M_BUNSEKI_IN")
        Dim setDt As DataTable = setDs.Tables("LMC010_OUTKA_M_BUNSEKI_OUT")
        Dim count As Integer = 0

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'データの抽出
            rtnResult = Me.ServerChkJudge(setDs, "SelectBunsekiOutkaM")

            count = MyBase.GetResultCount()

            'エラーの場合はそのまま返却
            If rtnResult = False OrElse count = 0 Then
                Return Nothing
            End If

            '値設定
            For j As Integer = 0 To count - 1
                outDt.ImportRow(setDt.Rows(j))
            Next

        Next

        Return ds

    End Function

    ''' <summary>
    ''' データ検索(イエローカード発行時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectOutkaMメソッド呼出</remarks>
    Private Function SelectYCardOutkaM(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMC010_OUTKA_M_YCARD_IN")
        Dim outDt As DataTable = ds.Tables("LMC010_OUTKA_M_YCARD_OUT")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMC010_OUTKA_M_YCARD_IN")
        Dim setDt As DataTable = setDs.Tables("LMC010_OUTKA_M_YCARD_OUT")
        Dim count As Integer = 0

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'データの抽出
            rtnResult = Me.ServerChkJudge(setDs, "SelectYCardOutkaM")

            count = MyBase.GetResultCount()

            'エラーの場合はそのまま返却
            If rtnResult = False OrElse count = 0 Then
                Return Nothing
            End If

            '値設定
            For j As Integer = 0 To count - 1
                outDt.ImportRow(setDt.Rows(j))
            Next

        Next

        Return ds

    End Function

#If True Then  ' FFEM機能改修(納品書未受信状態表示対応) 20170127 added

    ''' <summary>
    ''' データ検索(納品書発行時)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectNeedlessNhsData(ByVal ds As DataSet) As DataSet

        If (Me.ServerChkJudge(ds, "SelectNeedlessNhsData") = False) Then
            Return Nothing
        End If

        Return ds

    End Function
#End If

    ''' <summary>
    ''' データ検索(納品書発行有無FFEM)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>2019/12/03 add</remarks>
    Private Function SelectNhsNoFFEM(ByVal ds As DataSet) As DataSet

        If (Me.ServerChkJudge(ds, "SelectNhsNoFFEM") = False) Then
            Return Nothing
        End If

        Return ds

    End Function



    'START YANAI 要望番号773
    ''' <summary>
    ''' データ検索処理(出荷報告Excel作成時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectHoukokuExcelメソッド呼出</remarks>
    Private Function SelectHoukokuExcel(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectHoukokuExcel", ds)

    End Function

    ''' <summary>
    ''' FFEM オフライン品 依頼書未出力件数検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectOutkaediDtlFjfOffMiPrintCount(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectOutkaediDtlFjfOffMiPrintCount", ds)

    End Function

    ''' <summary>
    ''' データ更新処理(出荷報告Excel作成時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateHoukokuExcel(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateHoukokuExcel", ds)

    End Function


    'ADD 2016/06/21 要望番号:2580
    ''' <summary>
    ''' データ更新処理(送状番号更新時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateDenp_No(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateDenp_No", ds)

    End Function

    ''' <summary>
    ''' 出荷データ更新処理(SBS再保管出荷実績取込)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSBSSaihokanOutkaL(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateSBSSaihokanOutkaL", ds)

    End Function

    ''' <summary>
    ''' 運送データ更新処理(SBS再保管出荷実績取込)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSBSSaihokanUnsoL(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateSBSSaihokanUnsoL", ds)

    End Function

    'ADD 2016/06/21 要望番号:2580
    ''' <summary>
    ''' 同一出荷番号削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteOutkaDenp_No(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "DeleteOutkaDenp_No", ds)

    End Function

    'ADD 2016/06/21 要望番号:2580
    ''' <summary>
    ''' 出荷送り状番号登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertOutkaDenp_No(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "InsertOutkaDenp_No", ds)

    End Function

    'ADD 2016/06/21 要望番号:2580
    ''' <summary>
    ''' 出荷管理番号存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckOutka_No_L(ByVal ds As DataSet) As DataSet

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SQLSelectOutkaL", ds)

        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then
            '出荷管理番号が存在しない場合、メッセージを設定する
            MyBase.SetMessage("E078", New String() {"出荷"})
            ds.Tables("LMC010IN_OUTKA_DENP_NO").Clear()
        End If

        Return ds

    End Function

    'END YANAI 要望番号773

    ''' <summary>
    ''' データ存在チェック（SBS再保管出荷実績取込）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SBSSaihokanOutkaImportChk(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SBSSaihokanOutkaImportChk", ds)

        Return ds

    End Function

    'START YANAI 要望番号853 まとめ処理対応
    ''' <summary>
    ''' 在庫データ検索(まとめ用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectZaiDataMATOME(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectZaiDataMATOME", ds)

    End Function
    'END YANAI 要望番号853 まとめ処理対応

    'START YANAI 20120322 特別梱包個数計算
    ''' <summary>
    ''' 運賃取込チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function ChkSeiqDateUnchinMain(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMC010_OUTKA_M_IN").Rows(0)

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)

        Dim rtnResult As Boolean
        rtnResult = Me.ChkSeiqDateUnchin(ds, dr.Item("OUTKA_PLAN_DATE").ToString(), dr.Item("ARR_PLAN_DATE").ToString(),
                                         lgm.Selector({"特別梱包個数計算", "Special packing piece counting", "특별포장개수계산", "中国語"}),
                                         True)
        '2017/09/25 修正 李↑

        If rtnResult = False Then
            MyBase.SetMessage("E285")
            ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("ERR_NO") = "E285"
        End If
        Return ds

    End Function

    ''' <summary>
    ''' 特別梱包個数計算
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function UpdateOutkaPkgNb(ByVal ds As DataSet) As DataSet

        '運賃取込チェック
        Dim rtnDs As DataSet = Me.ChkSeiqDateUnchinMain(ds)
        If String.IsNullOrEmpty(ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("ERR_NO").ToString) = False Then
            'エラーメッセージが設定されている場合は終了
            Return ds
        End If

        '出荷情報の取得
        ds = MyBase.CallDAC(Me._Dac, "SelectOutkaPkgNb", ds)

        If ds.Tables("LMC010OUT").Rows.Count = 0 Then
            '対象データが存在しない場合
            MyBase.SetMessage("E024")
            ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("ERR_NO") = "E024"
            Return ds
        End If

        '特別梱包個数計算処理
        ds = Me.outkaPkgNbKeisan(ds)

        '特別梱包個数計算の更新処理
        '出荷(大)
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaPkgNbOUTKAL", ds)

        Dim rtnResult As Boolean = Not MyBase.IsMessageExist()
        If rtnResult = True Then
            'エラーがなければ更新処理を続行
            '運送(大)
            ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaPkgNbUNSOL", ds)
        End If

        Return ds

    End Function
    'END YANAI 20120322 特別梱包個数計算


    '社内入荷データ作成 terakawa Start
    ''' <summary>
    ''' 社内入荷データ作成先データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ComboData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "ComboData", ds)


    End Function
    '社内入荷データ作成 terakawa End

    'START Kurihara WIT対応

    ''' <summary>
    ''' 対象の出荷管理番号Lを持つワークデータを検索します。
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectMtmPickList(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectMtmPickList", ds)

    End Function

    ''' <summary>
    ''' 纏めピッキングWKへの登録・削除判断を行うためのデータを抽出します。
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectEntryJudge(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectEntryJudge", ds)

    End Function

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Sub DeleteCTotalPicWk(ByVal ds As DataSet)

        MyBase.CallDAC(Me._Dac, "DeleteCTotalPicWk", ds)

    End Sub

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Sub InsertCTotalPicWk(ByVal ds As DataSet)

        MyBase.CallDAC(Me._Dac, "InsertCTotalPicWk", ds)

    End Sub


    'END Kurihara WIT対応


#End Region

#Region "設定処理"

    ''' <summary>
    ''' 運送会社マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistUnsoM(ByVal ds As DataSet) As DataSet

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SQLSelectExit", ds)

        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then
            '運送会社マスタにデータが存在しない場合、マスタ存在メッセージを設定する
            Dim unsoNm As String = ds.Tables("LMC010IN_UPDATE").Rows(0)("UNSOCO_CD").ToString() _
                                             & ds.Tables("LMC010IN_UPDATE").Rows(0)("UNSOCO_BR_CD").ToString()
            '20151020 tsunehira add
            MyBase.SetMessage("E658", New String() {unsoNm})
            'MyBase.SetMessage("E079", New String() {"運送会社マスタ", unsoNm})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 排他チェック(運送（大）、出荷データ（大））
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaita(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "HaitaCheckControl", ds)

        '処理件数による判定
        If MyBase.IsMessageExist() = True Then
            '0件の場合、論理排他メッセージを設定する
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
    ''' <summary>
    ''' 運送会社コードの支払タリフ、支払割増タリフを取得し、LMC010IN_UPDATEの支払タリフ、支払割増タリフにセットする
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetTariffCdFromUnsoCd(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "GetTariffCdFromUnsoCd", ds)

    End Function
    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End



    ''' <summary>
    ''' 運送L/出荷Lの更新処理を行う
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function HenkoData(ByVal ds As DataSet) As DataSet


#If True Then ' 西濃自動送り状番号対応 20160704 added inoue
        If (ds.Tables("LMC010IN_UPDATE").Rows.Count > 0) Then
            Dim inUpdateRow As DataRow = ds.Tables("LMC010IN_UPDATE").Rows(0)

            If (String.IsNullOrEmpty(inUpdateRow.Item("AUTO_DENP_NO").ToString())) Then
                inUpdateRow.Item("AUTO_DENP_NO") = Me.GetAutoDenpNo(inUpdateRow.Item("AUTO_DENP_KBN").ToString(), ds)
            End If
        End If
#End If

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "HenkoData", ds)

    End Function

    ''' <summary>
    ''' 出荷データ（大）更新（印刷時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaLPrint(ByVal ds As DataSet) As DataSet

        'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
        ''DACクラス呼出
        'Return MyBase.CallDAC(Me._Dac, "UpdateDataPrint", ds)
        If ("06").Equals(ds.Tables("LMC010IN_OUTKA_L").Rows(0).Item("PRINT_KB").ToString) OrElse
            ("11").Equals(ds.Tables("LMC010IN_OUTKA_L").Rows(0).Item("PRINT_KB").ToString) Then
            'まとめ荷札・送状の場合 
            'DACクラス呼出
            '検索対象データの検索処理
            ds = MyBase.CallDAC(Me._Dac, "SelectMatomeData", ds)

            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            'ADD 2016/08/29　まとめ送状対応　要望番号:2615 Start
            If ("06").Equals(ds.Tables("LMC010IN_OUTKA_L").Rows(0).Item("PRINT_KB").ToString) Then

                Dim matomeRowMax As Integer = ds.Tables("LMC010OUT_MATOME").Rows.Count - 1
                Dim sOutNo As String = String.Empty
                Dim dr As DataRow = Nothing

                If matomeRowMax >= 0 Then


                    '運送Lより、最小のAUTO_DENP_NO取得　（LMC010IN_MATOME_DENP_NO ⇒ LMC010OUT_MATOME_DENP_NO）
                    ds.Tables("LMC010IN_MATOME_DENP_NO").Clear()

                    For i As Integer = 0 To matomeRowMax
                        If i = 0 Then
                            sOutNo = String.Concat("'", ds.Tables("LMC010OUT_MATOME").Rows(i).Item("OUTKA_NO_L").ToString.Trim, "'")
                        Else
                            sOutNo = String.Concat(sOutNo, ",'", ds.Tables("LMC010OUT_MATOME").Rows(i).Item("OUTKA_NO_L").ToString.Trim, "'")
                        End If

                    Next

                    dr = ds.Tables("LMC010IN_MATOME_DENP_NO").NewRow
                    dr("NRS_BR_CD") = ds.Tables("LMC010IN_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString.Trim
                    dr("MATOME_OUTKA_L") = sOutNo
                    ds.Tables("LMC010IN_MATOME_DENP_NO").Rows.Add(dr)

                    ds = MyBase.CallDAC(Me._Dac, "SelectMatomeAutoDenpNo", ds)

                    If MyBase.IsMessageExist() = True Then
                        Return ds
                    End If

                    '最小のAUTO_DENP_NOで運送L更新
                    If ds.Tables("LMC010OUT_MATOME_DENP_NO").Rows.Count > 0 Then

                        Dim sAUTO_DENP_NO As String = ds.Tables("LMC010OUT_MATOME_DENP_NO").Rows(0).Item("AUTO_DENP_NO").ToString.Trim

                        dr("AUTO_DENP_NO") = sAUTO_DENP_NO

                        ds = MyBase.CallDAC(Me._Dac, "UpdateUnsoLMatome", ds)

                        If MyBase.IsMessageExist() = True Then
                            Return ds
                        End If

                        'LMC010OUT_MATOME_DENP_NO にAUTO_DENP_NO設定
                        'For i As Integer = 0 To ds.Tables("LMC010OUT_MATOME").Rows.Count - 1

                        '    ds.Tables("LMC010OUT_MATOME").Rows(i).Item("AUTO_DENP_NO") = sAUTO_DENP_NO

                        'Next
                    End If
                End If

            End If
            'ADD 2016/08/29　まとめ送状対応　要望番号:2615 End

            Return MyBase.CallDAC(Me._Dac, "UpdateDataPrintMatome", ds)

        ElseIf ("13").Equals(ds.Tables("LMC010IN_OUTKA_L").Rows(0).Item("PRINT_KB").ToString) Then
            'まとめ送り状（チェックのみ）
            'まとめ荷札・送状以外の場合

#If False Then      'UPD 2018/10/16 依頼番号 : 002601   【LMS】日医工_まとめ送状(選択)_送り状番号更新 ＋ オーダー番号最小の個数のみ送り状表示(千葉BC柴田) 
            Dim cpyDs As DataSet = ds.Copy
            ' '' '' '' ''For i As Integer = 0 To ds.Tables("LMC010IN_OUTKA_L").Rows.Count - 1
            cpyDs.Clear()
            cpyDs.Tables("LMC010IN_OUTKA_L").ImportRow(ds.Tables("LMC010IN_OUTKA_L").Rows(0))
            MyBase.CallDAC(Me._Dac, "UpdateDataPrint", cpyDs)
            'MyBase.CallDAC(Me._Dac, "UpdateDataPrintMatome", cpyDs) 
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If
            '' '' '' '' ''Next

            Return ds
            'memo MATOME_DENP_GRP
#Else
            Dim cpyDs As DataSet = ds.Copy
            cpyDs.Clear()
            cpyDs.Tables("LMC010IN_OUTKA_L").ImportRow(ds.Tables("LMC010IN_OUTKA_L").Rows(0))
            MyBase.CallDAC(Me._Dac, "UpdateDataPrint", cpyDs)
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '検索対象データの検索処理
            ds = MyBase.CallDAC(Me._Dac, "SelectMatomeDataSelect", ds)

            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '-----
            Dim matomeRowMax As Integer = ds.Tables("LMC010OUT_MATOME").Rows.Count - 1
            Dim sOutNo As String = String.Empty
            Dim dr As DataRow = Nothing

            If matomeRowMax >= 0 Then


                '運送Lより、最小のAUTO_DENP_NO取得　（LMC010IN_MATOME_DENP_NO ⇒ LMC010OUT_MATOME_DENP_NO）
                ds.Tables("LMC010IN_MATOME_DENP_NO").Clear()

                For i As Integer = 0 To matomeRowMax
                    If i = 0 Then
                        sOutNo = String.Concat("'", ds.Tables("LMC010OUT_MATOME").Rows(i).Item("OUTKA_NO_L").ToString.Trim, "'")
                    Else
                        sOutNo = String.Concat(sOutNo, ",'", ds.Tables("LMC010OUT_MATOME").Rows(i).Item("OUTKA_NO_L").ToString.Trim, "'")
                    End If

                Next

                dr = ds.Tables("LMC010IN_MATOME_DENP_NO").NewRow
                dr("NRS_BR_CD") = ds.Tables("LMC010IN_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString.Trim
                dr("MATOME_OUTKA_L") = sOutNo
                ds.Tables("LMC010IN_MATOME_DENP_NO").Rows.Add(dr)

                ds = MyBase.CallDAC(Me._Dac, "SelectMatomeAutoDenpNo", ds)

                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If

                '最小のAUTO_DENP_NOで運送L更新
                If ds.Tables("LMC010OUT_MATOME_DENP_NO").Rows.Count > 0 Then

                    Dim sAUTO_DENP_NO As String = ds.Tables("LMC010OUT_MATOME_DENP_NO").Rows(0).Item("AUTO_DENP_NO").ToString.Trim

                    dr("AUTO_DENP_NO") = sAUTO_DENP_NO

                    ds = MyBase.CallDAC(Me._Dac, "UpdateUnsoLMatome", ds)

                    If MyBase.IsMessageExist() = True Then
                        Return ds
                    End If

                    'LMC010OUT_MATOME_DENP_NO にAUTO_DENP_NO設定
                    'For i As Integer = 0 To ds.Tables("LMC010OUT_MATOME").Rows.Count - 1

                    '    ds.Tables("LMC010OUT_MATOME").Rows(i).Item("AUTO_DENP_NO") = sAUTO_DENP_NO

                    'Next
                End If
            End If

#End If
        ElseIf ("04").Equals(ds.Tables("LMC010IN_OUTKA_L").Rows(0).Item("PRINT_KB").ToString) Then
            '分析表
            Dim cpyDs As DataSet = ds.Copy
            cpyDs.Clear()
            cpyDs.Tables("LMC010IN_OUTKA_L").ImportRow(ds.Tables("LMC010IN_OUTKA_L").Rows(0))
            MyBase.CallDAC(Me._Dac, "UpdateDataCOAPrint", cpyDs)
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            Return ds

        Else
            'DACクラス呼出
            Return MyBase.CallDAC(Me._Dac, "UpdateDataPrint", ds)

        End If
        'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)

    End Function

#If True Then ' 名鉄対応(2499)

    ''' <summary>
    ''' 出荷データ（大）更新（印刷時）(名鉄用)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaLPrintMeitetu(ByVal ds As DataSet) As DataSet
        ' ToDo: UpdateDataPrintへ統合を検討
        Return MyBase.CallDAC(Me._Dac, "UpdateDataPrintMeitetu", ds)
    End Function
#End If

    'START YANAI 20120322 特別梱包個数計算
    ''' <summary>
    ''' 特別梱包個数計算
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function outkaPkgNbKeisan(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim drs As DataRow = ds.Tables("LMC010_OUTKA_M_IN").Rows(0)
        Dim dr As DataRow() = ds.Tables("LMC010OUT").Select(Nothing, "GOODS_CD_NRS,PKG_NB")
        Dim max As Integer = dr.Length - 1

        Dim outkaPkgNb As Decimal = 0
        Dim konsu As Decimal = 0
        Dim hasu As Decimal = 0
        Dim kosu As Decimal = 0
        Dim sumKonsu As Decimal = 0
        Dim sumHasu As Decimal = 0

        Dim goodsCdNrs As String = String.Empty
        Dim pkgNb As String = String.Empty

        Dim matomeFlg As Boolean = False

        If 0 <= max Then
            '1件目の値を設定
            goodsCdNrs = dr(0).Item("GOODS_CD_NRS").ToString
            pkgNb = dr(0).Item("PKG_NB").ToString
            matomeFlg = True
        End If
        For i As Integer = 0 To max

            If (goodsCdNrs).Equals(dr(i).Item("GOODS_CD_NRS").ToString) = True AndAlso
                (pkgNb).Equals(dr(i).Item("PKG_NB").ToString) = True Then
                '同じ時は加算
                kosu = kosu + Convert.ToDecimal(dr(i).Item("OUTKA_TTL_NB").ToString)

                matomeFlg = True
            Else
                '異なる場合は計算をする

                '個数から梱数・端数をそれぞれ求める
                konsu = Math.Floor(CalcData(kosu, Convert.ToDecimal(pkgNb)))
                hasu = CalcDataMod(kosu, Convert.ToDecimal(pkgNb))

                '合計値を保持している変数に梱数・端数加算する
                sumKonsu = sumKonsu + konsu
                sumHasu = sumHasu + hasu

                goodsCdNrs = dr(i).Item("GOODS_CD_NRS").ToString
                pkgNb = dr(i).Item("PKG_NB").ToString
                kosu = Convert.ToDecimal(dr(i).Item("OUTKA_TTL_NB").ToString)

                matomeFlg = False

            End If

        Next

        If 0 <= max Then
            '個数から梱数・端数をそれぞれ求める
            konsu = Math.Floor(CalcData(kosu, Convert.ToDecimal(pkgNb)))
            hasu = CalcDataMod(kosu, Convert.ToDecimal(pkgNb))

            '合計値を保持している変数に梱数・端数加算する
            sumKonsu = sumKonsu + konsu
            sumHasu = sumHasu + hasu
        End If

        If 1 <= sumHasu Then
            sumKonsu = sumKonsu + 1
        End If

        '梱数を設定
        drs.Item("OUTKA_PKG_NB") = Convert.ToString(sumKonsu)

        Return ds

    End Function

    ''' <summary>
    ''' ゼロ割回避処理(整数値を返却)
    ''' </summary>
    ''' <param name="value1">分子の値</param>
    ''' <param name="value2">分母の値</param>
    ''' <returns>分母が0の場合、0を返却</returns>
    ''' <remarks></remarks>
    Private Function CalcData(ByVal value1 As Decimal, ByVal value2 As Decimal) As Decimal

        If value2 = 0 Then
            Return 0
        End If

        Return value1 / value2

    End Function

    ''' <summary>
    ''' ゼロ割回避処理(あまり値を返却)
    ''' </summary>
    ''' <param name="value1">分子の値</param>
    ''' <param name="value2">分母の値</param>
    ''' <returns>分母が0の場合、0を返却</returns>
    ''' <remarks></remarks>
    Private Function CalcDataMod(ByVal value1 As Decimal, ByVal value2 As Decimal) As Decimal

        If value2 = 0 Then
            Return 0
        End If

        Return value1 Mod value2

    End Function
    'END YANAI 20120322 特別梱包個数計算

#End Region

#Region "一括引当処理"

    ''' <summary>
    ''' 一括引当処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        'タブレット用区分値設定
        ds = Me.SetTabletItemData(ds)

        '出荷(大)更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutKaL", ds)

        'エラーの場合、終了
        If MyBase.IsMessageStoreExist(Convert.ToInt32(ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("ROW_NO").ToString())) = True Then
            Return ds
        End If

        '出荷(中)更新
        ds = Me.UpdateOutKaMData(ds)

        '出荷(小)更新
        ds = Me.InsertOutKaSData(ds)

        '在庫更新処理
        ds = Me.UpdateZaiTrsData(ds)

        'START YANAI 要望番号1474 一括引当時、引当が出来ない場合のエラーメッセージ変更
        'エラーの場合、終了
        If MyBase.IsMessageStoreExist(Convert.ToInt32(ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("ROW_NO").ToString())) = True Then
            Return ds
        End If
        'END YANAI 要望番号1474 一括引当時、引当が出来ない場合のエラーメッセージ変更

        'START YANAI 要望番号585
        '運送(大)更新処理
        ds = Me.UpdateUnsoLData(ds)
        'END YANAI 要望番号585

        'START YANAI 要望番号1474 一括引当時、引当が出来ない場合のエラーメッセージ変更
        'エラーの場合、終了
        If MyBase.IsMessageStoreExist(Convert.ToInt32(ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("ROW_NO").ToString())) = True Then
            Return ds
        End If
        'END YANAI 要望番号1474 一括引当時、引当が出来ない場合のエラーメッセージ変更

        'START kasama WIT対応 商品管理番号採番処理
        '商品管理番号更新対象ハンディ荷主の場合のみ処理実施
        ds = Me.UpdateGoodsKanriNo(ds)
        'END kasama WIT対応 商品管理番号採番処理

        Return ds

    End Function

    ''' <summary>
    ''' 出荷(中)更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateOutKaMData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateOutKaM", ds)

    End Function

    ''' <summary>
    ''' 出荷(小)新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertOutKaSData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "InsertOutKaS", ds)

    End Function

    ''' <summary>
    ''' 在庫更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateZaiTrsData(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dr As DataRow = ds.Tables("LMC010_OUTKA_M_IN").Rows(0)
        Dim drs As DataRow() = ds.Tables("LMC010OUT_OUTKA_S").Select(String.Concat("OUTKA_NO_L = '", dr.Item("OUTKA_NO_L").ToString(), "' "))
        Dim max As Integer = drs.Length - 1
        Dim dt As DataTable = ds.Tables("LMC010OUT_ZAI")

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMC010OUT_ZAI")

        '値の初期化
        inTbl.Clear()

        Dim rowNo As String = dr.Item("ROW_NO").ToString()
        'START YANAI 20110914 一括引当対応
        'Dim setDr As DataRow = Nothing
        Dim setDr() As DataRow = Nothing
        Dim setMax As Integer = 0
        'END YANAI 20110914 一括引当対応

        For i As Integer = 0 To max

            '値設定
            'START YANAI 20110914 一括引当対応
            'setDr = dt.Select(String.Concat("ZAI_REC_NO = '", drs(i).Item("ZAI_REC_NO").ToString(), "' "))(0)
            'setDr.Item("ROW_NO") = rowNo
            'inTbl.ImportRow(setDr)
            setDr = dt.Select(String.Concat("ZAI_REC_NO = '", drs(i).Item("ZAI_REC_NO").ToString(), "' AND ",
                                            "OUTKA_NO_L = '", drs(i).Item("OUTKA_NO_L").ToString(), "'"))
            setMax = setDr.Length - 1
            For j As Integer = 0 To setMax
                If String.IsNullOrEmpty(setDr(j).Item("ROW_NO").ToString()) = True Then
                    setDr(j).Item("ROW_NO") = rowNo
                    inTbl.ImportRow(setDr(j))
                End If
            Next
            'END YANAI 20110914 一括引当対応

        Next

        '在庫更新処理
        setDs = (MyBase.CallDAC(Me._Dac, "UpdateZaiTrs", setDs))

        'START YANAI 20110914 一括引当対応
        '一括引当の中に同じ在庫レコードを更新するデータがある場合、排他エラーになってしまうので、
        'このタイミングで、更新日付、時間を置き換え、排他エラーにならないようにする。
        Dim sysUpdDate As String = String.Empty
        Dim sysUpdTime As String = String.Empty
        For i As Integer = 0 To max
            setDr = setDs.Tables("LMC010OUT_ZAI").Select(String.Concat("ZAI_REC_NO = '", drs(i).Item("ZAI_REC_NO").ToString(), "' AND ",
                                                                       "OUTKA_NO_L = '", drs(i).Item("OUTKA_NO_L").ToString(), "'"))
            sysUpdDate = setDr(0).Item("SYS_UPD_DATE").ToString()
            sysUpdTime = setDr(0).Item("SYS_UPD_TIME").ToString()

            setDr = dt.Select(String.Concat("ZAI_REC_NO = '", drs(i).Item("ZAI_REC_NO").ToString(), "'"))
            setMax = setDr.Length - 1
            For j As Integer = 0 To setMax
                setDr(j).Item("SYS_UPD_DATE") = sysUpdDate
                setDr(j).Item("SYS_UPD_TIME") = sysUpdTime
            Next
        Next
        'END YANAI 20110914 一括引当対応

        Return ds

    End Function

    'START YANAI 要望番号585
    ''' <summary>
    ''' 運送(大)更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateUnsoLData(ByVal ds As DataSet) As DataSet

#If True Then ' 西濃自動送り状番号対応 20160701 added inoue
        ' 自動送り状番号払い出し
        ds = Me.SetAutoDenpNo(ds)
#End If

        Return MyBase.CallDAC(Me._Dac, "UpdateUnsoL", ds)

    End Function
    'END YANAI 要望番号585

#If True Then ' 西濃自動送り状番号出力対応 20160701 added inoue

    ''' <summary>
    ''' MEITETSU_DENP_NOを取得
    ''' </summary>
    ''' <returns>MEITETSU_DENP_NO</returns>
    ''' <remarks></remarks>
    Private Function GetMeiTetsuDenpNoL() As String

        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
            GetAutoCode(NumberMasterUtility.NumberKbn.MEITETSU_DENP_NO, Me)


        If (String.IsNullOrEmpty(newCode) = False) Then

            'チェックデジットの組込み
            Dim checkDigit As Integer = Convert.ToInt32(newCode) Mod 7
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo

    End Function

    ''' <summary>
    ''' TOLL_DENP_NOを取得
    ''' </summary>
    ''' <returns>TOLL_DENP_NO</returns>
    ''' <remarks></remarks>
    Private Function GetTollDenpNoL(ByVal ds As DataSet) As String


        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
        GetAutoCode(NumberMasterUtility.NumberKbn.TOLL_DENP_NO, Me)

        Dim dt As DataTable = ds.Tables("LMC010_OKURIJYO_WK")
        Dim okurijyoHed As String = String.Empty

        If dt.Rows.Count > 0 Then
            okurijyoHed = dt.Rows(0).Item("OKURIJYO_HEAD").ToString
        End If

        If (String.IsNullOrEmpty(newCode) = False) Then

            Dim checkDigit As Integer

            'チェックデジットの組込み

            If String.Empty.Equals(okurijyoHed) = False Then
                '区分マスタより取得できた時
                newCode = String.Concat(okurijyoHed, newCode)
            End If

            checkDigit = CInt(Convert.ToInt64(newCode) Mod 7)
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo


    End Function

    '2017/12/12 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add start
    ''' <summary>
    ''' TOLL_DENP_NO(群馬)を取得
    ''' </summary>
    ''' <returns>TOLL_DENP_NO</returns>
    ''' <remarks></remarks>
    Private Function GetGunmaTollDenpNoL(ByVal ds As DataSet) As String


        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
        GetAutoCode(NumberMasterUtility.NumberKbn.TOLL_DENP_NO_CYD, Me)

        Dim dt As DataTable = ds.Tables("LMC010_OKURIJYO_WK")
        Dim okurijyoHed As String = String.Empty

        If dt.Rows.Count > 0 Then
            okurijyoHed = dt.Rows(0).Item("OKURIJYO_HEAD").ToString
        End If

        If (String.IsNullOrEmpty(newCode) = False) Then

            Dim checkDigit As Integer

            'チェックデジットの組込み

            If String.Empty.Equals(okurijyoHed) = False Then
                '区分マスタより取得できた時
                newCode = String.Concat(okurijyoHed, newCode)
            End If

            checkDigit = CInt(Convert.ToInt64(newCode) Mod 7)
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo


    End Function
    '2017/12/12 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add end

    ''' <summary>
    ''' 送り状番号生成(千葉JPロジスティクス[元トールエクスプレス])
    ''' </summary>
    ''' <returns>送り状番号</returns>
    ''' <remarks></remarks>
    Private Function GetChibaTollDenpNoL(ByVal ds As DataSet) As String


        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
        GetAutoCode(NumberMasterUtility.NumberKbn.JPLOGI_DENP_NO_CHI, Me)

        Dim dt As DataTable = ds.Tables("LMC010_OKURIJYO_WK")
        Dim okurijyoHed As String = String.Empty

        If dt.Rows.Count > 0 Then
            okurijyoHed = dt.Rows(0).Item("OKURIJYO_HEAD").ToString
        End If

        If (String.IsNullOrEmpty(newCode) = False) Then

            Dim checkDigit As Integer

            'チェックデジットの組込み

            If String.Empty.Equals(okurijyoHed) = False Then
                '区分マスタより取得できた時
                newCode = String.Concat(okurijyoHed, newCode)
            End If

            checkDigit = CInt(Convert.ToInt64(newCode) Mod 7)
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo


    End Function

    ''' <summary>
    ''' 送り状番号生成(土気JPロジスティクス[元トールエクスプレス])
    ''' </summary>
    ''' <returns>送り状番号</returns>
    ''' <remarks></remarks>
    Private Function GetTokeTollDenpNoL(ByVal ds As DataSet) As String


        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
        GetAutoCode(NumberMasterUtility.NumberKbn.JPLOGI_DENP_NO_TOK, Me)

        Dim dt As DataTable = ds.Tables("LMC010_OKURIJYO_WK")
        Dim okurijyoHed As String = String.Empty

        If dt.Rows.Count > 0 Then
            okurijyoHed = dt.Rows(0).Item("OKURIJYO_HEAD").ToString
        End If

        If (String.IsNullOrEmpty(newCode) = False) Then

            Dim checkDigit As Integer

            'チェックデジットの組込み

            If String.Empty.Equals(okurijyoHed) = False Then
                '区分マスタより取得できた時
                newCode = String.Concat(okurijyoHed, newCode)
            End If

            checkDigit = CInt(Convert.ToInt64(newCode) Mod 7)
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo


    End Function


    ''' <summary>
    ''' 自動送り状番号設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetAutoDenpNo(ByVal ds As DataSet) As DataSet

        Dim outkaMInRow As DataRow = ds.Tables("LMC010_OUTKA_M_IN").Rows(0)

        For Each row As DataRow In ds.Tables("LMC010OUT_UNSO_L").Select() _
            .Where(Function(s) s.Item("OUTKA_NO_L").Equals(outkaMInRow.Item("OUTKA_NO_L")))

            If (String.IsNullOrEmpty(row.Item("AUTO_DENP_NO").ToString())) Then

                Dim autoDenpKbn As String = row.Item("AUTO_DENP_KBN").ToString()
                row.Item("AUTO_DENP_NO") = Me.GetAutoDenpNo(autoDenpKbn, ds)

            End If
        Next

        Return ds

    End Function


    ''' <summary>
    ''' 自動採番送り状番号取得
    ''' </summary>
    ''' <param name="autoDenpKbn">自動送り状番号区分</param>
    ''' <returns>自動送り状番号</returns>
    ''' <remarks>
    ''' LMC010BLC,LMC020BLC,LMF020BLCに同メソッドあり
    ''' ToDo:共通ライブラリ化を検討
    ''' </remarks>
    Private Function GetAutoDenpNo(ByVal autoDenpKbn As String, ByVal ds As DataSet) As String


        Dim autoDenpNo As String = String.Empty

        If (String.IsNullOrEmpty(autoDenpKbn)) Then
            ' 自動送り状番号払い出し対象外
            Return autoDenpNo
        End If

        Dim nubmerKbn As NumberMasterUtility.NumberKbn = Nothing

        Select Case autoDenpKbn
            Case AUTO_DENP_KBN.MEITETSU_TRANSPORT
                ' 名鉄運輸送り状番号生成
                autoDenpNo = Me.GetMeiTetsuDenpNoL()

            Case AUTO_DENP_KBN.SEINO_TRANSPORTATION_CHIBA
                ' 西濃運輸(千葉)送り状番号生成
                nubmerKbn = NumberMasterUtility.NumberKbn.AUTO_DENP_NO_SEINO_CHIBA
                autoDenpNo = New NumberMasterUtility().GetAutoCode(nubmerKbn, Me)

                'ADD 2017/10/11
            Case AUTO_DENP_KBN.TTOLL_EXPRESS_OSAKA
                ' トールエクスプレス送り状番号生成                
                autoDenpNo = Me.GetTollDenpNoL(ds)

            Case AUTO_DENP_KBN.TTOLL_EXPRESS_GUNMA
                'トールエクスプレス（群馬）
                autoDenpNo = Me.GetGunmaTollDenpNoL(ds)

            Case AUTO_DENP_KBN.SEINO_TRANSPORTATION_TOKE
                ' 西濃運輸(土気)送り状番号生成
                nubmerKbn = NumberMasterUtility.NumberKbn.AUTO_DENP_NO_SEINO_TOKE
                autoDenpNo = New NumberMasterUtility().GetAutoCode(nubmerKbn, Me)

            Case AUTO_DENP_KBN.TTOLL_EXPRESS_CHIBA
                'JPロジスティクス[元トールエクスプレス](千葉) 送り状番号生成
                autoDenpNo = Me.GetChibaTollDenpNoL(ds)

            Case AUTO_DENP_KBN.TTOLL_EXPRESS_TOKE
                'JPロジスティクス[元トールエクスプレス](土気) 送り状番号生成
                autoDenpNo = Me.GetTokeTollDenpNoL(ds)

            Case AUTO_DENP_KBN.SEINO_TRANSPORTATION_SODEGAURA
                ' 西濃運輸(袖ヶ浦)送り状番号生成
                nubmerKbn = NumberMasterUtility.NumberKbn.AUTO_DENP_NO_SEINO_SODEGAURA
                autoDenpNo = New NumberMasterUtility().GetAutoCode(nubmerKbn, Me)

            Case AUTO_DENP_KBN.SEINO_TRANSPORTATION_OSAKA
                ' 西濃運輸(大阪)送り状番号生成
                nubmerKbn = NumberMasterUtility.NumberKbn.AUTO_DENP_NO_SEINO_OSAKA
                autoDenpNo = New NumberMasterUtility().GetAutoCode(nubmerKbn, Me)

            Case Else
                Throw New InvalidOperationException(String.Format("定義されていないAUTO_DENP_KBN=[{0}]が指定されました。", autoDenpKbn))

        End Select


        Return autoDenpNo


    End Function

#End If

    'START YANAI 要望番号638
    ''' <summary>
    ''' 運賃の削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteUnchinData(ByVal ds As DataSet) As DataSet

        'Delete
        Return MyBase.CallDAC(Me._Dac, "DeleteUnchinData", ds)

    End Function

    ''' <summary>
    ''' 運賃の作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertUnchinData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "InsertUnchinData", ds)

    End Function
    'END YANAI 要望番号638

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 支払運賃の削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteShiharaiData(ByVal ds As DataSet) As DataSet

        'Delete
        Return MyBase.CallDAC(Me._Dac, "DeleteShiharaiData", ds)

    End Function

    ''' <summary>
    ''' 支払運賃の作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertShiharaiData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "InsertShiharaiData", ds)

    End Function
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等


    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start  
    ''' <summary>
    ''' 支払運賃の削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteShiharaiData2(ByVal ds As DataSet) As DataSet

        'Delete
        Return MyBase.CallDAC(Me._Dac, "DeleteShiharaiData2", ds)

    End Function
    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  


#End Region

#Region "WIT対応"

    'KASAMA 2013.10.29 WIT対応 Start
    ''' <summary>
    ''' ハンディ対象荷主情報を取得します。
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectCustHandy(ByVal ds As DataSet) As DataSet

        ' ハンディ対象荷主情報取得
        ds = MyBase.CallDAC(Me._Dac, "SelectCustHandy", ds)

        Return ds

    End Function
    'KASAMA 2013.10.29 WIT対応 End

    'START kasama WIT対応 商品管理番号採番処理
    ''' <summary>
    ''' 商品管理番号採番処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateGoodsKanriNo(ByVal ds As DataSet) As DataSet

        Dim inRow As DataRow = ds.Tables("LMC010_OUTKA_M_IN").Rows(0)
        Dim zaiDt As DataTable = ds.Tables("LMC010OUT_ZAI")

        '作業用DS
        Dim setDs As DataSet = ds.Copy
        Dim setZaiDt As DataTable = setDs.Tables("LMC010OUT_ZAI")
        Dim setSrcDt As DataTable = setDs.Tables("LMC010OUT_GOODS_KANRI_NO_SRC")
        Dim setUpdDt As DataTable = setDs.Tables("LMC010IN_UPDATE_GOODS_KANRI_NO")

        ' ハンディ荷主情報取得
        Dim inCustHandyRow As DataRow = setDs.Tables("LMC010IN_CUST_HANDY").NewRow
        inCustHandyRow("NRS_BR_CD") = inRow("NRS_BR_CD")
        inCustHandyRow("OUTKA_NO_L") = inRow("OUTKA_NO_L")
        inCustHandyRow("CUST_CD_L") = inRow("CUST_CD_L")
        setDs.Tables("LMC010IN_CUST_HANDY").Rows.Add(inCustHandyRow)
        setDs = Me.SelectCustHandy(setDs)

        ' 商品管理番号更新対象ハンディ荷主の場合のみ処理実施
        If IsUpdateGoodsKanriNoTargetCustHandy(setDs) = False Then
            Return ds
        End If

        '排他調整用
        Dim updatedRowDt As DataTable = setUpdDt.Clone

        'IN情報から出荷L単位で出荷Sを抽出
        Dim outkaSRows As DataRow() = ds.Tables("LMC010OUT_OUTKA_S").Select(String.Concat("OUTKA_NO_L = '", inRow.Item("OUTKA_NO_L").ToString(), "' "))

        For Each outkaS As DataRow In outkaSRows

            '在庫レコードの抽出(実質1件)
            Dim dZaiRows As DataRow() = ds.Tables("LMC010OUT_ZAI").Select(String.Concat("ZAI_REC_NO = '", outkaS.Item("ZAI_REC_NO").ToString(), "' AND ",
                                            "OUTKA_NO_L = '", outkaS.Item("OUTKA_NO_L").ToString(), "'"))

            For Each zaiRow As DataRow In dZaiRows
                '作業用テーブルの初期化
                setUpdDt.Clear()
                setSrcDt.Clear()
                setZaiDt.Clear()
                setZaiDt.ImportRow(zaiRow)

                '商品管理番号を構成する元データの取得
                setDs = (MyBase.CallDAC(Me._Dac, "SelectGoodsKanriNoSrc", setDs))

                'データが取得できない場合は既に設定済みの在庫のため次のレコード処理へ
                If setSrcDt.Rows.Count = 0 Then
                    Continue For
                End If

                '商品管理番号元データ
                Dim srcRow As DataRow = setSrcDt.Rows(0)

                ' IN情報作成
                Dim newRow As DataRow = setUpdDt.NewRow()
                Dim custHandyRow As DataRow = setDs.Tables("LMC010OUT_CUST_HANDY").Rows(0)

                newRow("NRS_BR_CD") = zaiRow("NRS_BR_CD").ToString
                newRow("ZAI_REC_NO") = zaiRow("ZAI_REC_NO").ToString
                newRow("GOODS_KANRI_NO") = Me.CreateGoodsKanriNo(custHandyRow, srcRow)
                newRow("SYS_UPD_DATE") = zaiRow("SYS_UPD_DATE").ToString
                newRow("SYS_UPD_TIME") = zaiRow("SYS_UPD_TIME").ToString

                setUpdDt.Rows.Add(newRow)

                ' 更新
                setDs = (MyBase.CallDAC(Me._Dac, "UpdateGoodsKanriNo", setDs))

                '排他調整用
                updatedRowDt.ImportRow(newRow)

            Next

        Next

        '一括引当の中に同じ在庫レコードを更新するデータがある場合、排他エラーになってしまうので、
        'このタイミングで、更新日付、時間を置き換え、排他エラーにならないようにする。
        '更新処理を行ったため、updatedにはDACにて更新日付、時刻が設定されている。
        For Each updatedRow As DataRow In updatedRowDt.Rows

            Dim sysUpdDate As String = updatedRow("SYS_UPD_DATE").ToString()
            Dim sysUpdTime As String = updatedRow("SYS_UPD_TIME").ToString()

            For Each row As DataRow In zaiDt.Select(String.Concat("ZAI_REC_NO = '", updatedRow("ZAI_REC_NO").ToString(), "'"))
                row.Item("SYS_UPD_DATE") = sysUpdDate
                row.Item("SYS_UPD_TIME") = sysUpdTime
            Next

        Next

        Return ds
    End Function

    ''' <summary>
    ''' 商品管理番号を生成します。
    ''' </summary>
    ''' <param name="handyCustInfo"></param>
    ''' <param name="srcRow"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateGoodsKanriNo(ByVal handyCustInfo As DataRow, ByVal srcRow As DataRow) As String

        Dim format As String = handyCustInfo("S101_KBN_NM2").ToString
        Dim values As New List(Of String)

        For i As Integer = 3 To 10

            Dim kbnCol As String = String.Format("S101_KBN_NM{0}", i)
            Dim kbnValue As String = handyCustInfo(kbnCol).ToString

            If String.IsNullOrEmpty(kbnValue) = False Then
                values.Add(srcRow(kbnValue).ToString)
            Else
                '区分値が未設定の時点でループを抜ける
                Exit For
            End If

        Next

        Return String.Format(format, values.ToArray)

    End Function
    'END kasama WIT対応 商品管理番号採番処理

    ''' <summary>
    ''' 商品管理番号更新対象ハンディ荷主かどうかを取得します。
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsUpdateGoodsKanriNoTargetCustHandy(ByVal ds As DataSet) As Boolean

        Dim outTbl As DataTable = ds.Tables("LMC010OUT_CUST_HANDY")

        ' データが存在しない場合はfalse
        If outTbl Is Nothing OrElse outTbl.Rows.Count = 0 Then
            Return False
        End If

        Dim outRow As DataRow = outTbl.Rows(0)

        ' フォーマットが存在しない場合はfalse
        If String.IsNullOrEmpty(outRow("S101_KBN_NM2").ToString) = True Then
            Return False
        End If

        ' FLG_03:商品管理番号更新対象可否フラグ
        Return [Const].LMConst.FLG.ON.Equals(outRow("FLG_03").ToString)

    End Function

#End Region

    '社内入荷データ作成対応 terakawa 2012.11.19 Start
#Region "社内入荷データ作成処理"

    ''' <summary>
    ''' 社内入荷データ作成許可チェック処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectIntEdiExist(ByVal ds As DataSet) As DataSet

        '社内入荷データ作成の検索
        ds = MyBase.CallDAC(Me._Dac, "SelectIntEdiExist", ds)

        If MyBase.GetResultCount() = 0 Then
            '0件の場合

            '英語化対応
            '20151021 tsunehira add
            MyBase.SetMessageStore("00", "E528")
            'MyBase.SetMessageStore("00", "E528", New String() {"社内入荷データ作成"})
        End If

        Return ds

    End Function



    ''' <summary>
    ''' 社内入荷データ作成処理（セレクト）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMakeInkaData(ByVal ds As DataSet) As DataSet

        '社内入荷データ用データの検索
        ds = MyBase.CallDAC(Me._Dac, "SelectInkaData", ds)

        Dim outDs As DataTable = ds.Tables("LMC010_INKA_OUT")
        Dim max As Integer = outDs.Rows.Count - 1
        '2013.01.21 要望番号1789 追加START
        Dim strMsg As String = String.Empty
        Dim strMsg1 As String = String.Empty
        Dim strMsg2 As String = String.Empty
        '2013.01.21 要望番号1789 追加END

        If outDs.Rows.Count = 0 Then
            '0件の場合

            '英語化対応
            '20151021 tsunehira add


            strMsg = String.Concat(ds.Tables("LMC010_INKA_IN").Rows(0).Item("OUTKA_NO_L").ToString)
            MyBase.SetMessageStore("00" _
                                 , "E678", New String() {strMsg} _
                                 , ds.Tables("LMC010_INKA_IN").Rows(0).Item("ROW_NO").ToString())

            MyBase.SetMessage("E011")

            '2013.01.21 要望番号1789 追加START
        Else

            For i As Integer = 0 To max

                If String.IsNullOrEmpty(outDs.Rows(i).Item("GOODS_CD_NRS").ToString()) = True Then

                    '英語化対応
                    '20151021 tsunehira add
                    MyBase.SetMessageStore("00" _
                                         , "E677" _
                                         , {String.Concat(outDs.Rows(i).Item("OUTKA_NO_L").ToString _
                                                        , " - " _
                                                        , outDs.Rows(i).Item("OUTKA_NO_M").ToString)} _
                                         , ds.Tables("LMC010_INKA_IN").Rows(0).Item("ROW_NO").ToString())

                    MyBase.SetMessage("E235")

                End If
            Next
        End If
        '2013.01.21 要望番号1789 追加END

        Return ds

    End Function


    ''' <summary>
    ''' 社内入荷データ作成処理（メイン）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function MakeInkaData(ByVal ds As DataSet) As DataSet

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        '入荷(L)・入荷(中)・入荷(小)のデータセットを設定
#If False Then ' 依頼番号:000209 対応 changed by inoue
            ds = Me.SetInkaData(ds)
#Else

        Dim isGrouping As Boolean = False
        If (ds.Tables.Contains(TABLE_NM.LMC010_INT_EDI) AndAlso
            ds.Tables(TABLE_NM.LMC010_INT_EDI).Rows.Count > 0) Then

            ' まとめ処理の実施有無
            isGrouping _
                = (LMConst.FLG.ON.Equals(ds.Tables(TABLE_NM.LMC010_INT_EDI)(0).Item("IS_GROUPING")))

        End If

        If (isGrouping) Then
            ds = Me.SetInkaDataWithGrouping(ds)
        Else
            ds = Me.SetInkaData(ds)
        End If

#End If
        '入荷データ(大)の更新
        Dim rtnResult As Boolean = Me.InsertInkaL(ds)
        If rtnResult = False Then
            MyBase.SetMessageStore("00", "E666")
            MyBase.SetMessage("E666")

            'MyBase.SetMessageStore("00", "E305", New String() {"入荷データ(大)", String.Empty})
            'MyBase.SetMessage("E305")
            Return ds
        End If

        '入荷データ(中)の更新
        rtnResult = Me.InsertInkaM(ds)
        If rtnResult = False Then
            MyBase.SetMessageStore("00", "E667")
            MyBase.SetMessage("E667")

            'MyBase.SetMessageStore("00", "E305", New String() {"入荷データ(中)", String.Empty})
            'MyBase.SetMessage("E305")
            Return ds
        End If

        '入荷データ(小)の更新
        rtnResult = Me.InsertInkaS(ds)
        If rtnResult = False Then
            MyBase.SetMessageStore("00", "E665")
            MyBase.SetMessage("E665")

            'MyBase.SetMessageStore("00", "E305", New String() {"入荷データ(小)", String.Empty})
            'MyBase.SetMessage("E305")
            Return ds
        End If

        Return ds

    End Function


    ''' <summary>
    ''' 入荷(L)・入荷(中)・入荷(小)のデータセットを設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SetInkaDataWithGrouping(ByVal ds As DataSet) As DataSet

        Dim inkaL As DataTable = ds.Tables("LMC010_B_INKA_L")
        Dim inkaM As DataTable = ds.Tables("LMC010_B_INKA_M")
        Dim inkaS As DataTable = ds.Tables("LMC010_B_INKA_S")

        inkaL.Clear()
        inkaM.Clear()
        inkaS.Clear()

        Dim inkaLdr As DataRow = Nothing
        Dim inkaMdr As DataRow = Nothing
        Dim inkaSdr As DataRow = Nothing

        Dim inkaNoM As Integer = 0


        For Each row As DataRow In ds.Tables("LMC010_INKA_OUT").Rows()


            inkaLdr = inkaL.AsEnumerable().Where(Function(r) row.Item("TO_NRS_BR_CD").Equals(r.Item("NRS_BR_CD")) AndAlso
                                                             row.Item("TO_NRS_WH_CD").Equals(r.Item("NRS_WH_CD")) AndAlso
                                                             row.Item("TO_CUST_L").Equals(r.Item("CUST_CD_L")) AndAlso
                                                             row.Item("TO_CUST_M").Equals(r.Item("CUST_CD_M"))).FirstOrDefault

            If (inkaLdr Is Nothing) Then

                inkaLdr = inkaL.NewRow()
                inkaLdr.Item("NRS_BR_CD") = row.Item("TO_NRS_BR_CD")
                inkaLdr.Item("INKA_NO_L") = GetInkaNoL(ds)
                inkaLdr.Item("FURI_NO") = ""
                inkaLdr.Item("INKA_TP") = INKA_TP.General ' N007
                inkaLdr.Item("INKA_KB") = INKA_KB.Regular ' N006	
                inkaLdr.Item("INKA_STATE_KB") = INKA_STATE_KB.AlreadyScheduledInput ' N004
                inkaLdr.Item("INKA_DATE") = row.Item("OUTKA_PLAN_DATE")
                inkaLdr.Item("NRS_WH_CD") = row.Item("TO_NRS_WH_CD")
                inkaLdr.Item("CUST_CD_L") = row.Item("TO_CUST_L")
                inkaLdr.Item("CUST_CD_M") = row.Item("TO_CUST_M")
                inkaLdr.Item("INKA_PLAN_QT") = 0
                inkaLdr.Item("INKA_PLAN_QT_UT") = ""
                inkaLdr.Item("INKA_TTL_NB") = row.Item("OUTKA_PKG_NB")
                inkaLdr.Item("BUYER_ORD_NO_L") = row.Item("BUYER_ORD_NO")
                inkaLdr.Item("OUTKA_FROM_ORD_NO_L") = row.Item("CUST_ORD_NO")
                inkaLdr.Item("SEIQTO_CD") = ""
                inkaLdr.Item("TOUKI_HOKAN_YN") = EXISTENCE_STATUS.YES ' U009
                inkaLdr.Item("HOKAN_YN") = EXISTENCE_STATUS.YES ' U009
                inkaLdr.Item("HOKAN_FREE_KIKAN") = row.Item("HOKAN_FREE_KIKAN")
                inkaLdr.Item("HOKAN_STR_DATE") = row.Item("OUTKA_PLAN_DATE")
                inkaLdr.Item("NIYAKU_YN") = EXISTENCE_STATUS.YES ' U009
                inkaLdr.Item("TAX_KB") = TAX_KB.JP.Tax ' Z001
                inkaLdr.Item("REMARK") = row.Item("REMARK")
                inkaLdr.Item("REMARK_OUT") = ""
                inkaLdr.Item("CHECKLIST_PRT_DATE") = ""
                inkaLdr.Item("CHECKLIST_PRT_USER") = ""
                inkaLdr.Item("UKETSUKELIST_PRT_DATE") = ""
                inkaLdr.Item("UKETSUKELIST_PRT_USER") = ""
                inkaLdr.Item("UKETSUKE_DATE") = ""
                inkaLdr.Item("UKETSUKE_USER") = ""
                inkaLdr.Item("KEN_DATE") = ""
                inkaLdr.Item("KEN_USER") = ""
                inkaLdr.Item("INKO_DATE") = ""
                inkaLdr.Item("INKO_USER") = ""
                inkaLdr.Item("HOUKOKUSYO_PR_DATE") = ""
                inkaLdr.Item("HOUKOKUSYO_PR_USER") = ""
                inkaLdr.Item("UNCHIN_TP") = UNSO_TEHAI_KB.Undecided ' U005
                inkaLdr.Item("UNCHIN_KB") = "" ' U003
                inkaLdr.Item("WH_TAB_STATUS") = "00"
                inkaLdr.Item("WH_TAB_YN") = row.Item("WH_TAB_YN")
                inkaLdr.Item("WH_TAB_IMP_YN") = "00"
                inkaLdr.Item("SYS_ENT_DATE") = ""
                inkaLdr.Item("SYS_ENT_TIME") = ""
                inkaLdr.Item("SYS_ENT_PGID") = ""
                inkaLdr.Item("SYS_ENT_USER") = ""
                inkaLdr.Item("SYS_UPD_DATE") = ""
                inkaLdr.Item("SYS_UPD_TIME") = ""
                inkaLdr.Item("SYS_UPD_PGID") = ""
                inkaLdr.Item("SYS_UPD_USER") = ""
                inkaLdr.Item("SYS_DEL_FLG") = LMConst.FLG.OFF

                inkaL.Rows.Add(inkaLdr)

            End If


            inkaMdr = inkaM.AsEnumerable().Where(Function(r) inkaLdr.Item("INKA_NO_L").Equals(r.Item("INKA_NO_L")) AndAlso
                                                             row.Item("GOODS_CD_NRS").Equals(r.Item("GOODS_CD_NRS")) AndAlso
                                                             row.Item("CUST_ORD_NO_DTL").Equals(r.Item("OUTKA_FROM_ORD_NO_M")) AndAlso
                                                             row.Item("BUYER_ORD_NO_DTL").Equals(r.Item("BUYER_ORD_NO_M")) AndAlso
                                                             row.Item("REMARK_M").Equals(r.Item("REMARK"))).FirstOrDefault
            If (inkaMdr Is Nothing) Then

                inkaNoM = inkaM.AsEnumerable() _
                               .Where(Function(r) inkaLdr.Item("INKA_NO_L").Equals(r.Item("INKA_NO_L"))).Count + 1

                '■入荷(中)
                inkaMdr = inkaM.NewRow()

                inkaMdr.Item("NRS_BR_CD") = inkaLdr.Item("NRS_BR_CD")
                inkaMdr.Item("INKA_NO_L") = inkaLdr.Item("INKA_NO_L")
                inkaMdr.Item("INKA_NO_M") = String.Format("{0:D3}", inkaNoM)
                inkaMdr.Item("GOODS_CD_NRS") = row.Item("GOODS_CD_NRS")
                inkaMdr.Item("OUTKA_FROM_ORD_NO_M") = row.Item("CUST_ORD_NO_DTL")
                inkaMdr.Item("BUYER_ORD_NO_M") = row.Item("BUYER_ORD_NO_DTL")
                inkaMdr.Item("REMARK") = row.Item("REMARK_M").ToString()
                inkaMdr.Item("PRINT_SORT") = PRINT_SORT_MAX_VALUE
                inkaMdr.Item("SYS_ENT_TIME") = ""
                inkaMdr.Item("SYS_ENT_PGID") = ""
                inkaMdr.Item("SYS_ENT_USER") = ""
                inkaMdr.Item("SYS_UPD_DATE") = ""
                inkaMdr.Item("SYS_UPD_TIME") = ""
                inkaMdr.Item("SYS_UPD_PGID") = ""
                inkaMdr.Item("SYS_UPD_USER") = ""
                inkaMdr.Item("SYS_DEL_FLG") = LMConst.FLG.OFF

                inkaM.Rows.Add(inkaMdr)

            End If


            Dim inkaNoS As Integer = inkaS.AsEnumerable() _
                                          .Where(Function(r) inkaMdr.Item("INKA_NO_L").Equals(r.Item("INKA_NO_L")) AndAlso
                                                             inkaMdr.Item("INKA_NO_M").Equals(r.Item("INKA_NO_M"))).Count + 1

            '■入荷(小)
            inkaSdr = inkaS.NewRow()

            inkaSdr.Item("NRS_BR_CD") = inkaMdr.Item("NRS_BR_CD")
            inkaSdr.Item("INKA_NO_L") = inkaMdr.Item("INKA_NO_L")
            inkaSdr.Item("INKA_NO_M") = inkaMdr.Item("INKA_NO_M")
            inkaSdr.Item("INKA_NO_S") = String.Format("{0:D3}", inkaNoS)
            inkaSdr.Item("ZAI_REC_NO") = ""
            inkaSdr.Item("LOT_NO") = row.Item("LOT_NO").ToString()
            inkaSdr.Item("LOCA") = row.Item("LOCA").ToString()
            inkaSdr.Item("TOU_NO") = row.Item("TOU_NO").ToString()
            inkaSdr.Item("SITU_NO") = row.Item("SITU_NO").ToString()
            inkaSdr.Item("ZONE_CD") = row.Item("ZONE_CD").ToString()
            inkaSdr.Item("KONSU") = Math.Floor(Convert.ToDecimal(row.Item("ALCTD_NB")) / Convert.ToDecimal(row.Item("PKG_NB")))
            inkaSdr.Item("HASU") = Convert.ToDecimal(row.Item("ALCTD_NB")) Mod Convert.ToDecimal(row.Item("PKG_NB"))

            If (IsDBNull(row.Item("IRIME")) = False AndAlso
                row.Item("IRIME") IsNot Nothing) Then

                inkaSdr.Item("IRIME") = row.Item("IRIME")
            Else
                inkaSdr.Item("IRIME") = row.Item("STD_IRIME_NB")
            End If

            inkaSdr.Item("BETU_WT") = row.Item("BETU_WT")
            inkaSdr.Item("SERIAL_NO") = row.Item("SERIAL_NO")
            inkaSdr.Item("GOODS_COND_KB_1") = ""
            inkaSdr.Item("GOODS_COND_KB_2") = ""
            inkaSdr.Item("GOODS_COND_KB_3") = ""
            inkaSdr.Item("GOODS_CRT_DATE") = ""
            inkaSdr.Item("LT_DATE") = ""
            inkaSdr.Item("SPD_KB") = SPD_KB.ShipOK
            inkaSdr.Item("OFB_KB") = OFB_KB.Normal
            inkaSdr.Item("DEST_CD") = ""
            inkaSdr.Item("REMARK") = row.Item("REMARK_S")
            inkaSdr.Item("ALLOC_PRIORITY") = ALLOC_PRIORITY.Free
            inkaSdr.Item("REMARK_OUT") = ""
            inkaSdr.Item("SYS_ENT_DATE") = ""
            inkaSdr.Item("SYS_ENT_TIME") = ""
            inkaSdr.Item("SYS_ENT_PGID") = ""
            inkaSdr.Item("SYS_ENT_USER") = ""
            inkaSdr.Item("SYS_UPD_DATE") = ""
            inkaSdr.Item("SYS_UPD_TIME") = ""
            inkaSdr.Item("SYS_UPD_PGID") = ""
            inkaSdr.Item("SYS_UPD_USER") = ""
            inkaSdr.Item("SYS_DEL_FLG") = LMConst.FLG.OFF

            inkaS.Rows.Add(inkaSdr)

        Next

        Return ds


    End Function




    ''' <summary>
    ''' 入荷(L)・入荷(中)・入荷(小)のデータセットを設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SetInkaData(ByVal ds As DataSet) As DataSet

        ds.Tables("LMC010_B_INKA_L").Clear()
        ds.Tables("LMC010_B_INKA_M").Clear()
        ds.Tables("LMC010_B_INKA_S").Clear()
        Dim inkaLdr As DataRow = Nothing
        Dim inkaMdr As DataRow = Nothing
        Dim inkaSdr As DataRow = Nothing
        Dim inkaNoM As Decimal = 0
        Dim inkaNoS As Decimal = 0

        With ds.Tables("LMC010_INKA_OUT")
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max

                '■入荷(大)
                If i > 0 AndAlso .Rows(i - 1).Item("OUTKA_NO_L").ToString().Equals(.Rows(i).Item("OUTKA_NO_L").ToString()) Then
                    '入荷(大)が既に設定されている場合は、セットしない。
                Else
                    inkaNoM = 0

                    inkaLdr = ds.Tables("LMC010_B_INKA_L").NewRow()

                    inkaLdr.Item("NRS_BR_CD") = .Rows(i).Item("TO_NRS_BR_CD").ToString()
                    inkaLdr.Item("INKA_NO_L") = GetInkaNoL(ds)
                    inkaLdr.Item("FURI_NO") = ""
                    inkaLdr.Item("INKA_TP") = "10"
                    inkaLdr.Item("INKA_KB") = "10"
                    inkaLdr.Item("INKA_STATE_KB") = "10"
                    inkaLdr.Item("INKA_DATE") = .Rows(i).Item("OUTKA_PLAN_DATE").ToString()
                    inkaLdr.Item("NRS_WH_CD") = .Rows(i).Item("TO_NRS_WH_CD").ToString()
                    inkaLdr.Item("CUST_CD_L") = .Rows(i).Item("TO_CUST_L").ToString()
                    inkaLdr.Item("CUST_CD_M") = "00"
                    inkaLdr.Item("INKA_PLAN_QT") = 0
                    inkaLdr.Item("INKA_PLAN_QT_UT") = ""
                    inkaLdr.Item("INKA_TTL_NB") = .Rows(i).Item("OUTKA_PKG_NB").ToString()
                    inkaLdr.Item("BUYER_ORD_NO_L") = .Rows(i).Item("BUYER_ORD_NO").ToString()
                    inkaLdr.Item("OUTKA_FROM_ORD_NO_L") = .Rows(i).Item("CUST_ORD_NO").ToString()
                    inkaLdr.Item("SEIQTO_CD") = ""
                    inkaLdr.Item("TOUKI_HOKAN_YN") = "01"
                    inkaLdr.Item("HOKAN_YN") = "01"
                    inkaLdr.Item("HOKAN_FREE_KIKAN") = .Rows(i).Item("HOKAN_FREE_KIKAN").ToString()
                    inkaLdr.Item("HOKAN_STR_DATE") = .Rows(i).Item("OUTKA_PLAN_DATE").ToString()
                    inkaLdr.Item("NIYAKU_YN") = "01"
                    inkaLdr.Item("TAX_KB") = "01"
                    inkaLdr.Item("REMARK") = .Rows(i).Item("REMARK").ToString()
                    inkaLdr.Item("REMARK_OUT") = ""
                    inkaLdr.Item("CHECKLIST_PRT_DATE") = ""
                    inkaLdr.Item("CHECKLIST_PRT_USER") = ""
                    inkaLdr.Item("UKETSUKELIST_PRT_DATE") = ""
                    inkaLdr.Item("UKETSUKELIST_PRT_USER") = ""
                    inkaLdr.Item("UKETSUKE_DATE") = ""
                    inkaLdr.Item("UKETSUKE_USER") = ""
                    inkaLdr.Item("KEN_DATE") = ""
                    inkaLdr.Item("KEN_USER") = ""
                    inkaLdr.Item("INKO_DATE") = ""
                    inkaLdr.Item("INKO_USER") = ""
                    inkaLdr.Item("HOUKOKUSYO_PR_DATE") = ""
                    inkaLdr.Item("HOUKOKUSYO_PR_USER") = ""
                    inkaLdr.Item("UNCHIN_TP") = "20"
                    inkaLdr.Item("UNCHIN_KB") = ""
                    inkaLdr.Item("WH_TAB_STATUS") = "00"
                    inkaLdr.Item("WH_TAB_YN") = .Rows(i).Item("WH_TAB_YN").ToString()
                    inkaLdr.Item("WH_TAB_IMP_YN") = "00"
                    inkaLdr.Item("SYS_ENT_DATE") = ""
                    inkaLdr.Item("SYS_ENT_TIME") = ""
                    inkaLdr.Item("SYS_ENT_PGID") = ""
                    inkaLdr.Item("SYS_ENT_USER") = ""
                    inkaLdr.Item("SYS_UPD_DATE") = ""
                    inkaLdr.Item("SYS_UPD_TIME") = ""
                    inkaLdr.Item("SYS_UPD_PGID") = ""
                    inkaLdr.Item("SYS_UPD_USER") = ""
                    inkaLdr.Item("SYS_DEL_FLG") = "0"

                    ds.Tables("LMC010_B_INKA_L").Rows.Add(inkaLdr)
                End If

                '■EDI入荷(中)
                If i > 0 AndAlso .Rows(i - 1).Item("OUTKA_NO_L").ToString().Equals(.Rows(i).Item("OUTKA_NO_L").ToString()) _
                         AndAlso .Rows(i - 1).Item("OUTKA_NO_M").ToString().Equals(.Rows(i).Item("OUTKA_NO_M").ToString()) Then
                    'EDI(大)(中)が既に設定されている場合は、セットしない。
                Else
                    inkaNoM = inkaNoM + 1
                    inkaNoS = 0

                    '■入荷(中)
                    inkaMdr = ds.Tables("LMC010_B_INKA_M").NewRow()

                    inkaMdr.Item("NRS_BR_CD") = inkaLdr.Item("NRS_BR_CD")
                    inkaMdr.Item("INKA_NO_L") = inkaLdr.Item("INKA_NO_L")
                    inkaMdr.Item("INKA_NO_M") = Me.MaeCoverData(Convert.ToString(inkaNoM), "0", 3)
                    inkaMdr.Item("GOODS_CD_NRS") = .Rows(i).Item("GOODS_CD_NRS").ToString()
                    inkaMdr.Item("OUTKA_FROM_ORD_NO_M") = .Rows(i).Item("CUST_ORD_NO_DTL").ToString()
                    inkaMdr.Item("BUYER_ORD_NO_M") = .Rows(i).Item("BUYER_ORD_NO_DTL").ToString()
                    inkaMdr.Item("REMARK") = .Rows(i).Item("REMARK_M").ToString()
                    inkaMdr.Item("PRINT_SORT") = "99"
                    inkaMdr.Item("SYS_ENT_TIME") = ""
                    inkaMdr.Item("SYS_ENT_PGID") = ""
                    inkaMdr.Item("SYS_ENT_USER") = ""
                    inkaMdr.Item("SYS_UPD_DATE") = ""
                    inkaMdr.Item("SYS_UPD_TIME") = ""
                    inkaMdr.Item("SYS_UPD_PGID") = ""
                    inkaMdr.Item("SYS_UPD_USER") = ""
                    inkaMdr.Item("SYS_DEL_FLG") = ""

                    ds.Tables("LMC010_B_INKA_M").Rows.Add(inkaMdr)
                End If

                inkaNoS = inkaNoS + 1

                '■入荷(小)
                inkaSdr = ds.Tables("LMC010_B_INKA_S").NewRow()

                inkaSdr.Item("NRS_BR_CD") = inkaLdr.Item("NRS_BR_CD")
                inkaSdr.Item("INKA_NO_L") = inkaLdr.Item("INKA_NO_L")
                inkaSdr.Item("INKA_NO_M") = inkaMdr.Item("INKA_NO_M")
                inkaSdr.Item("INKA_NO_S") = Me.MaeCoverData(Convert.ToString(inkaNoS), "0", 3)
                inkaSdr.Item("ZAI_REC_NO") = ""
                inkaSdr.Item("LOT_NO") = .Rows(i).Item("LOT_NO").ToString()
                inkaSdr.Item("LOCA") = .Rows(i).Item("LOCA").ToString()
                inkaSdr.Item("TOU_NO") = .Rows(i).Item("TOU_NO").ToString()
                inkaSdr.Item("SITU_NO") = .Rows(i).Item("SITU_NO").ToString()
                inkaSdr.Item("ZONE_CD") = .Rows(i).Item("ZONE_CD").ToString()
                'inkaSdr.Item("KONSU") = .Rows(i).Item("PKG_NB").ToString()
                inkaSdr.Item("KONSU") = Math.Floor(Convert.ToDecimal(.Rows(i).Item("ALCTD_NB")) / Convert.ToDecimal(.Rows(i).Item("PKG_NB")))
                'inkaSdr.Item("HASU") = .Rows(i).Item("ALCTD_NB").ToString()
                inkaSdr.Item("HASU") = Convert.ToDecimal(.Rows(i).Item("ALCTD_NB")) Mod Convert.ToDecimal(.Rows(i).Item("PKG_NB"))
                inkaSdr.Item("IRIME") = .Rows(i).Item("STD_IRIME_NB").ToString()
                inkaSdr.Item("BETU_WT") = .Rows(i).Item("BETU_WT").ToString()
                inkaSdr.Item("SERIAL_NO") = .Rows(i).Item("SERIAL_NO").ToString()
                inkaSdr.Item("GOODS_COND_KB_1") = ""
                inkaSdr.Item("GOODS_COND_KB_2") = ""
                inkaSdr.Item("GOODS_COND_KB_3") = ""
                inkaSdr.Item("GOODS_CRT_DATE") = ""
                inkaSdr.Item("LT_DATE") = ""
                inkaSdr.Item("SPD_KB") = "01"
                inkaSdr.Item("OFB_KB") = "01"
                inkaSdr.Item("DEST_CD") = ""
                '要望番号:1947（出荷からの入荷作成において、備考小社内が対象外) 2013/03/14 START
                inkaSdr.Item("REMARK") = .Rows(i).Item("REMARK_S").ToString()
                'inkaSdr.Item("REMARK") = ""
                '要望番号:1947（出荷からの入荷作成において、備考小社内が対象外) 2013/03/14 追加START
                inkaSdr.Item("ALLOC_PRIORITY") = "10"
                inkaSdr.Item("REMARK_OUT") = ""
                inkaSdr.Item("SYS_ENT_DATE") = ""
                inkaSdr.Item("SYS_ENT_TIME") = ""
                inkaSdr.Item("SYS_ENT_PGID") = ""
                inkaSdr.Item("SYS_ENT_USER") = ""
                inkaSdr.Item("SYS_UPD_DATE") = ""
                inkaSdr.Item("SYS_UPD_TIME") = ""
                inkaSdr.Item("SYS_UPD_PGID") = ""
                inkaSdr.Item("SYS_UPD_USER") = ""
                inkaSdr.Item("SYS_DEL_FLG") = "0"

                ds.Tables("LMC010_B_INKA_S").Rows.Add(inkaSdr)

            Next

            Return ds
        End With

    End Function

    ''' <summary>
    ''' 入荷データ(大)の更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertInkaL(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertInkaL")

    End Function

    ''' <summary>
    ''' 入荷データ(中)の更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertInkaM(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertInkaM")

    End Function

    ''' <summary>
    ''' 入荷データ(小)の更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertInkaS(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertInkaS")

    End Function


    ''' <summary>
    ''' INKA_NO_Lを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>OutkaNoL</returns>
    ''' <remarks></remarks>
    Private Function GetInkaNoL(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.INKA_NO_L, Me, ds.Tables("LMC010_INKA_OUT").Rows(0).Item("TO_NRS_BR_CD").ToString())

    End Function
#End Region
    '社内入荷データ作成対応 terakawa 2012.11.19 End

    '2014.04.17 CALT連携対応 ri追加 --ST--
#Region "出荷指示データ作成処理"
    ''' <summary>
    ''' 出荷指示データ作成処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OutkaShiji(ByVal ds As DataSet) As DataSet

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        '--外部定義変数
        '***カウンター
        'LMB010IN_INKA_PLAN_SENDテーブルのRowカウンタ
        Dim iIDirectSendCnt As Integer = 0
        Dim Cnt As Integer = 0

        '****フラグ系
        '抽出処理成否フラグ
        Dim bSCForSelData As Boolean = True
        '作成処理成否フラグ
        Dim bSCForInsData As Boolean = True
        'キャンセル処理フラグ
        Dim bSCForCanData As Boolean = False

        '***データセット系
        'IN用データセット
        Dim dsI As DataSet = ds.Copy()
        Dim dtIDirectSend As DataTable = dsI.Tables("LMC010IN_OUTKA_DIRECT_SEND")
        '作業データセット
        Dim dsTmp As DataSet = ds.Clone()
        Dim drTmp As DataRow = Nothing
        '==========================================================

        'INデータ分だけループ(2014.04.21の組込時点ではループは一回のみ)
        iIDirectSendCnt = dtIDirectSend.Rows.Count
        For i As Integer = 0 To iIDirectSendCnt - 1

            Dim drIDirectSend As DataRow = Nothing
            drIDirectSend = dtIDirectSend.Rows(i)

            '--入力チェック

            '==========================================================

            '--DACアクセス(SELECT) --報告用データ抽出兼排他チェック--
            Dim dsO As DataSet = dsI.Copy
            dsO = MyBase.CallDAC(Me._Dac, "SelectOutkaLMS", dsO)

            '入力チェック(サーバ)
            '排他チェック
            If MyBase.GetResultCount() < 1 Then
                '2015.10.21 tusnehira add
                '英語化対応
                MyBase.SetMessage("E641")
                MyBase.SetMessageStore("00", "E641", , drIDirectSend.Item("ROW_NO").ToString())

                'Dim param() As String = {"出荷データが更新されている", "出荷指示データ作成"}
                'MyBase.SetMessage("E454", param)
                'MyBase.SetMessageStore("00", "E454", param, drIDirectSend.Item("ROW_NO").ToString())
                Return ds
            End If

            '出荷S作成チェック
            If MyBase.GetResultCount() > 0 Then

                Dim iRowCnt As Integer = 0
                Dim dtRow As DataRow = Nothing
                iRowCnt = dsO.Tables("LMC010OUT_OUTKA_DIRECT_SEND").Rows.Count

                For j As Integer = 0 To iRowCnt - 1
                    dtRow = dsO.Tables("LMC010OUT_OUTKA_DIRECT_SEND").Rows(j)

                    If String.IsNullOrEmpty(dtRow.Item("OUTKA_NO_S").ToString()) Then

                        '2017/09/25 修正 李↓
                        MyBase.SetMessage("E634")
                        MyBase.SetMessageStore("00", "E634", , drIDirectSend.Item("ROW_NO").ToString)
                        '2017/09/25 修正 李↑

                    End If
                Next

                If IsMessageExist() = True Then
                    Return ds
                End If

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
                Cnt = dsC.Tables("LMC010OUT_OUTKA_DIRECT_SEND").Rows.Count
                For j As Integer = 0 To Cnt - 1
                    dsTmp.Tables("LMC010OUT_OUTKA_DIRECT_SEND").Clear()

                    drTmp = dsC.Tables("LMC010OUT_OUTKA_DIRECT_SEND").Rows(j)

                    dsTmp.Tables("LMC010OUT_OUTKA_DIRECT_SEND").ImportRow(drTmp)

                    MyBase.CallDAC(Me._Dac, "InsertDirectSend", dsTmp)
                Next
            End If

            '==========================================================

            '--パラメータの編集(必要があれば当Function内に記述)
            Call Me.EditData(dsO)

            '--DACアクセス(INSERT) --報告データ作成--
            Cnt = 0
            Cnt = dsO.Tables("LMC010OUT_OUTKA_DIRECT_SEND").Rows.Count
            For j As Integer = 0 To Cnt - 1
                dsTmp.Tables("LMC010OUT_OUTKA_DIRECT_SEND").Clear()

                drTmp = dsO.Tables("LMC010OUT_OUTKA_DIRECT_SEND").Rows(j)

                dsTmp.Tables("LMC010OUT_OUTKA_DIRECT_SEND").ImportRow(drTmp)

                MyBase.CallDAC(Me._Dac, "InsertDirectSend", dsTmp)
            Next

        Next

        Return ds

    End Function

    ''' <summary>
    ''' パラメータエディット処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub EditData(ByRef ds As DataSet)
        Dim dsEdit As DataSet = ds.Copy()
        Dim dtEditSend As DataTable = dsEdit.Tables("LMC010OUT_OUTKA_DIRECT_SEND")

        Dim iSeq As Integer = 0

        For Each editRow As DataRow In dtEditSend.Rows
            iSeq = Convert.ToInt32(editRow.Item("SEND_SEQ"))
            editRow.Item("SEND_SEQ") = Right(String.Concat("000", iSeq), 3)
            '=================
        Next

        ds.Clear()

        ds = dsEdit
    End Sub

#End Region
    '2014.04.17 CALT連携対応 ri追加 --ST

    '2015.06.22 協立化学　作業料対応 START
#Region "作業料明細特殊作成(出荷データ,作業レコード取得＋存在・排他チェック)"

    ''' <summary>
    ''' 作業料明細特殊作成(出荷,作業　存在・排他チェック)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OutkaSagyoList(ByVal ds As DataSet) As DataSet

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        '--DACアクセス(SELECT) --出荷データ取得＋出荷排他・作業存在チェック--
        ds = MyBase.CallDAC(Me._Dac, "OutkaListCount", ds)

        '入力チェック(サーバ)
        '排他チェック
        If MyBase.GetResultCount() = 0 Then
            '2015.10.21 tusnehira add
            '英語化対応
            MyBase.SetMessage("E640")
            MyBase.SetMessageStore("00", "E640", , ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("ROW_NO").ToString())

            'Dim param() As String = {"出荷データが更新されている", "作業料明細作成"}
            'MyBase.SetMessage("E454", param)
            'MyBase.SetMessageStore("00", "E454", param, ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("ROW_NO").ToString())
            Return ds
        End If

        '2015.06.30 修正START
        ds.Tables("LMC010OUT_E_SAGYO_MEISAI").Clear()
        ds.Tables("LMC010OUT_E_SAGYO_MEISAI_LABEL").Clear()
        ds.Tables("LMC010OUT_E_SAGYO_MEISAI_HUTAI").Clear()
        ds.Tables("LMC010OUT_E_SAGYO_MEISAI_PLT").Clear()
        '2015.06.30 修正END

        '--DACアクセス(SELECT) --出荷より作業レコードデータ（ラベル）取得--
        ds = MyBase.CallDAC(Me._Dac, "OutkaSagyoListLabel", ds)

        '作業レコード作成チェック
        If MyBase.GetResultCount() > 0 Then

            Dim lRowCnt As Integer = 0
            Dim dtRow As DataRow = Nothing
            lRowCnt = ds.Tables("LMC010OUT_E_SAGYO_MEISAI_LABEL").Rows.Count

            For j As Integer = 0 To lRowCnt - 1
                dtRow = ds.Tables("LMC010OUT_E_SAGYO_MEISAI_LABEL").Rows(j)

                If String.IsNullOrEmpty(dtRow.Item("SAGYO_CD").ToString()) = True Then
                    Continue For
                ElseIf String.IsNullOrEmpty(dtRow.Item("INOUTKA_NO_LM").ToString()) = False Then

                    '2017/09/25 修正 李↓
                    MyBase.SetMessage("E633")
                    MyBase.SetMessageStore("00", "E633", New String() {ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("ROW_NO").ToString()})
                    '2017/09/25 修正 李↑

                    Return ds
                End If
            Next

            For k As Integer = 0 To lRowCnt - 1
                If String.IsNullOrEmpty(ds.Tables("LMC010OUT_E_SAGYO_MEISAI_LABEL").Rows(k).Item("SAGYO_CD").ToString()) = False Then
                    ds.Tables("LMC010OUT_E_SAGYO_MEISAI_LABEL").Rows(k).Item("INOUTKA_NO_LM") = String.Concat(ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("OUTKA_NO_L").ToString(), "000")
                    ds.Tables("LMC010OUT_E_SAGYO_MEISAI").ImportRow(ds.Tables("LMC010OUT_E_SAGYO_MEISAI_LABEL").Rows(k))
                End If
            Next

        End If

        '--DACアクセス(SELECT) --出荷より作業レコードデータ（フタイ）取得--
        ds = MyBase.CallDAC(Me._Dac, "OutkaSagyoListHutai", ds)

        '作業レコード作成チェック
        If MyBase.GetResultCount() > 0 Then

            Dim hRowCnt As Integer = 0
            Dim dtRow As DataRow = Nothing
            hRowCnt = ds.Tables("LMC010OUT_E_SAGYO_MEISAI_HUTAI").Rows.Count

            For j As Integer = 0 To hRowCnt - 1
                dtRow = ds.Tables("LMC010OUT_E_SAGYO_MEISAI_HUTAI").Rows(j)

                If String.IsNullOrEmpty(dtRow.Item("SAGYO_CD").ToString()) = True Then
                    Continue For
                ElseIf String.IsNullOrEmpty(dtRow.Item("INOUTKA_NO_LM").ToString()) = False Then

                    '2017/09/25 修正 李↓
                    MyBase.SetMessage("E633")
                    MyBase.SetMessageStore("00", "E633", New String() {ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("ROW_NO").ToString()})
                    '2017/09/25 修正 李↑

                    Return ds
                End If
            Next

            For k As Integer = 0 To hRowCnt - 1
                If String.IsNullOrEmpty(ds.Tables("LMC010OUT_E_SAGYO_MEISAI_HUTAI").Rows(k).Item("SAGYO_CD").ToString()) = False Then
                    ds.Tables("LMC010OUT_E_SAGYO_MEISAI_HUTAI").Rows(k).Item("INOUTKA_NO_LM") = String.Concat(ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("OUTKA_NO_L").ToString(), "000")
                    ds.Tables("LMC010OUT_E_SAGYO_MEISAI").ImportRow(ds.Tables("LMC010OUT_E_SAGYO_MEISAI_HUTAI").Rows(k))
                End If
            Next

        End If

        '--DACアクセス(SELECT) --出荷より作業レコードデータ（パレタイス）取得--
        ds = MyBase.CallDAC(Me._Dac, "OutkaSagyoListPlt", ds)

        '作業レコード作成チェック
        If MyBase.GetResultCount() > 0 Then

            Dim dtRow As DataRow = Nothing

            dtRow = ds.Tables("LMC010OUT_E_SAGYO_MEISAI_PLT").Rows(0)

            If String.IsNullOrEmpty(dtRow.Item("SAGYO_CD").ToString()) = True Then
                Return ds
            ElseIf String.IsNullOrEmpty(dtRow.Item("INOUTKA_NO_LM").ToString()) = False Then

                '2017/09/25 修正 李↓
                MyBase.SetMessage("E633")
                MyBase.SetMessageStore("00", "E633", New String() {ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("ROW_NO").ToString()})
                '2017/09/25 修正 李↑

                Return ds
            End If

            If Convert.ToInt32(dtRow.Item("LOT_COUNT").ToString()) > 1 Then
                If String.IsNullOrEmpty(ds.Tables("LMC010OUT_E_SAGYO_MEISAI_PLT").Rows(0).Item("SAGYO_CD").ToString()) = False Then
                    ds.Tables("LMC010OUT_E_SAGYO_MEISAI_PLT").Rows(0).Item("INOUTKA_NO_LM") = String.Concat(ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("OUTKA_NO_L").ToString(), "000")
                    ds.Tables("LMC010OUT_E_SAGYO_MEISAI").ImportRow(ds.Tables("LMC010OUT_E_SAGYO_MEISAI_PLT").Rows(0))
                End If
            End If

        End If

        Return ds

    End Function

#End Region

#Region "作業料明細特殊作成"
    ''' <summary>
    ''' 作業レコード作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSagyoRec(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMC010OUT_E_SAGYO_MEISAI").Rows(0)

        dr("SAGYO_REC_NO") = Me.GetSagyoRecNo(ds)

        '--DACアクセス(INSERT) 作業レコード作成
        ds = MyBase.CallDAC(Me._Dac, "InsertSagyoRec", ds)

        Return ds

    End Function

    ''' <summary>
    ''' SAGYO_REC_NOを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>ZaiRecNo</returns>
    ''' <remarks></remarks>
    Private Function GetSagyoRecNo(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, ds.Tables("LMC010OUT_E_SAGYO_MEISAI").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

#End Region
    '2015.06.22 協立化学　作業料対応 END

#Region "タブレット対応"
    ''' <summary>
    ''' タブレット使用項目の値を設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetTabletItemData(ByVal ds As DataSet) As DataSet

        'タブレット項目のデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectOutkaLWHData", ds)

        '棟室データ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectTouSituData", ds)

        '出荷Sチェック 
        '自社他社区分が他社(02)の出荷Sが1件でもあればタブレット使用なしに変更
        Dim tabTbl As DataTable = ds.Tables("LMC010_TABLET_IN")
        Dim tsTbl As DataTable = ds.Tables("LMC010OUT_TOUSITU")
        Dim osTbl As DataTable = ds.Tables("LMC010OUT_OUTKA_S")
        Dim osRow() As DataRow = osTbl.Select(String.Concat("OUTKA_NO_L = '", tabTbl.Rows(0).Item("OUTKA_NO_L").ToString(), "'"))
        Dim max As Integer = osRow.Length - 1
        Dim jisya As Boolean = False
        Dim tasya As Boolean = False

        For i As Integer = 0 To max
            Dim tsRow() As DataRow = tsTbl.Select(String.Concat("NRS_BR_CD  = '", osRow(i).Item("NRS_BR_CD").ToString(), "'",
                                                           " AND TOU_NO     = '", osRow(i).Item("TOU_NO").ToString(), "'",
                                                           " AND SITU_NO    = '", osRow(i).Item("SITU_NO").ToString(), "'",
                                                           " AND WH_CD      = '", tabTbl.Rows(0).Item("WH_CD").ToString(), "'"))
            If tsRow.Length > 0 Then
                If "02".Equals(tsRow(0).Item("JISYATASYA_KB").ToString) Then
                    tasya = True
                Else
                    jisya = True
                End If
            End If
        Next
        If tasya = True AndAlso jisya = False Then
            tabTbl.Rows(0).Item("WH_TAB_YN") = "00"
        End If

        'ロケ管理倉庫のチェック
        'ロケ管理対象外の場合タブレット使用なしに変更
        If "00".Equals(tabTbl.Rows(0).Item("LOC_MANAGER_YN").ToString) Then
            tabTbl.Rows(0).Item("WH_TAB_YN") = "00"
        End If

        If "00".Equals(tabTbl.Rows(0).Item("WH_TAB_YN").ToString) Then
            tabTbl.Rows(0).Item("WH_TAB_STATUS") = "99"
        End If

        Return ds

    End Function

    ''' <summary>
    '''営業所がタブレット対応かどうかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function IsWhTabNrsBrCd(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "IsWhTabNrsBrCd", ds)

    End Function

#End Region

#Region "届先"
    '2017.09.19 届先追加対応 Annen add start
    Private Function UpdateOutkaDestKbn(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateOutkaDestKbn", ds)

    End Function
    '2017.09.19 届先追加対応 Annen add end
#End Region

#Region "チェック"

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

    'START YANAI 20120322 特別梱包個数計算
    ''' <summary>
    ''' 請求日チェック(運賃料)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="shukkaDate">出荷日</param>
    ''' <param name="nonyuDate">納入日</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="selectFlg">検索フラグ　True:検索有　False:検索無</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkSeiqDateUnchin(ByVal ds As DataSet, ByVal shukkaDate As String, ByVal nonyuDate As String, ByVal msg As String, ByVal selectFlg As Boolean) As Boolean

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
            If Me.ChkDate(Me.GetChkDate(selectDt.Rows(i), shukkaDate, nonyuDate), Me.SelectGheaderData(chkDs, "SelectGheaderData"), msg, chkDt) = False Then
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
    Private Function ChkDate(ByVal value1 As String, ByVal value2 As String, ByVal msg As String, ByVal dt As DataTable) As Boolean

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

            '運賃
            If ("40").Equals(dt.Rows(0).Item("TARIFF_BUNRUI_KB").ToString()) = True Then
                '横持ちの場合
                '英語化対応
                '20151021 tsunehira add
                MyBase.SetMessage("E654")
                'MyBase.SetMessage("E285", New String() {"横持ち料", msg})

            Else
                '運賃の場合
                '英語化対応
                '20151021 tsunehira add
                MyBase.SetMessage("E653")
                'MyBase.SetMessage("E285", New String() {"横持ち料", msg})
            End If

            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' 最終請求日を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>請求日</returns>
    ''' <remarks>取得できない場合は"00000000"を返却</remarks>
    Private Function SelectGheaderData(ByVal ds As DataSet, ByVal actionId As String) As String

        ds = Me.DacAccess(ds, actionId)

        Dim dt As DataTable = ds.Tables("LMC010_UNCHIN_SKYU_DATE")
        If dt.Rows.Count < 1 Then

            Return "00000000"

        End If

        Return dt.Rows(0).Item("SKYU_DATE").ToString()

    End Function

    ''' <summary>
    ''' チェックする日付を取得
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="shukkaDate">出荷日</param>
    ''' <param name="nonyuDate">納入日</param>
    ''' <returns>チェック日付</returns>
    ''' <remarks></remarks>
    Private Function GetChkDate(ByVal dr As DataRow, ByVal shukkaDate As String, ByVal nonyuDate As String) As String

        '運賃計算締め基準の値によって、チェック対象の日付を変更
        If ("01").Equals(dr.Item("UNTIN_CALCULATION_KB").ToString()) = True Then
            GetChkDate = shukkaDate
        Else
            GetChkDate = nonyuDate
        End If

        Return GetChkDate

    End Function
    'END YANAI 20120322 特別梱包個数計算

    '要望番号:1533 terakawa 2012.10.30 Start
    ''' <summary>
    ''' 編集時排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function OutkaLExistChk(ByVal ds As DataSet) As DataSet
        Dim setDs As DataSet = ds.Copy()
        Dim rtnDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMC010_HAITA")

        Dim dt As DataTable = ds.Tables("LMC010_HAITA")
        Dim max As Integer = ds.Tables("LMC010_HAITA").Rows.Count - 1

        '詰め替え先のDataTableを空にする
        rtnDs.Tables("LMC010_HAITA").Clear()

        For i As Integer = 0 To max
            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'DACクラス呼出
            setDs = MyBase.CallDAC(Me._Dac, "OutkaLExistChk", setDs)
            'エラー判定
            If MyBase.GetResultCount() = 0 Then
                '0件の場合

                '英語化対応
                '20151021 tsunehira add
                Dim strMsg As String = String.Concat(ds.Tables("LMC010_HAITA").Rows(i).Item("OUTKA_NO_L").ToString)
                MyBase.SetMessageStore("00", "E678", New String() {strMsg}, dt.Rows(i).Item("ROW_NO").ToString())

                'Dim strMsg As String = String.Empty
                'strMsg = String.Concat("[出荷管理番号：", ds.Tables("LMC010_HAITA").Rows(i).Item("OUTKA_NO_L").ToString, "]")
                'MyBase.SetMessageStore("00", "E519", New String() {strMsg}, dt.Rows(i).Item("ROW_NO").ToString())
                Continue For
            End If

            rtnDs.Tables("LMC010_HAITA").ImportRow(dt.Rows(i))

        Next

        Return rtnDs


    End Function
    '要望番号:1533 terakawa 2012.10.30 End

    '社内入荷データ作成対応 terakawa 2012.11.19 Start
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

        For i As Integer = value.Length To keta - 1
            value = String.Concat(value2, value)
        Next

        Return value

    End Function
    '社内入荷データ作成対応 terakawa 2012.11.19 End

#End Region

#Region "名変入荷作成処理"

    ''' <summary>
    ''' 名変入荷作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function InsertMeihenInkaData(ByVal ds As DataSet) As DataSet

        Dim strMsg As String = String.Empty

        '出荷Lのデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectOutkaLDataMH", ds)

        '出荷Mのデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectOutkaMDataMH", ds)
        If ds.Tables("LMC010_MEIHEN_OUTKA_L").Rows.Count = 0 OrElse ds.Tables("LMC010_MEIHEN_OUTKA_M").Rows.Count = 0 Then
            strMsg = String.Concat("出荷管理番号:" & ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0).Item("OUTKA_NO_L").ToString)
            MyBase.SetMessageStore("00", "E519", New String() {strMsg}, ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0).Item("ROW_NO").ToString)
            MyBase.SetMessage("E519")
            ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0).Item("ERR_NO") = "E519"
            Return ds
        End If

        '出荷Sのデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectOutkaSDataMH", ds)
        If ds.Tables("LMC010_MEIHEN_OUTKA_S").Rows.Count = 0 Then
            MyBase.SetMessageStore("00", "E454", New String() {"出荷小が未作成", "処理", "出荷管理番号:" & ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0).Item("OUTKA_NO_L").ToString}, ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0).Item("ROW_NO").ToString)
            MyBase.SetMessage("E454")
            ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0).Item("ERR_NO") = "E454"
            Return ds
        End If

        '登録前チェック処理
        ds = Me.InkaTorokuCheck(ds)
        If String.IsNullOrEmpty(ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0).Item("ERR_NO").ToString) = False Then
            'エラーメッセージが設定されている場合は終了
            Return ds
        End If

        '入荷管理番号Lの取得
        ds = Me.GetInkaNoLMH(ds)

        'データセット設定処理(入荷L)
        ds = Me.SetDatasetInkaLMH(ds)

        'データセット設定処理(入荷M)
        ds = Me.SetDatasetInkaMMH(ds)

        'データセット設定処理(入荷S)
        ds = Me.SetDatasetInkaSMH(ds)

        'データセット設定処理(在庫データ)
        ds = Me.SetDatasetZaiTrsMH(ds)

        '入荷Lの作成
        ds = MyBase.CallDAC(Me._Dac, "InsertInkaLDataMH", ds)

        '入荷Mの作成
        ds = MyBase.CallDAC(Me._Dac, "InsertInkaMDataMH", ds)

        '入荷Sの作成
        ds = MyBase.CallDAC(Me._Dac, "InsertInkaSDataMH", ds)

        '在庫データの作成
        ds = MyBase.CallDAC(Me._Dac, "InsertZaiTrsMH", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 登録前チェック処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InkaTorokuCheck(ByVal ds As DataSet) As DataSet

        Dim strMsg As String = String.Empty

        '入荷Lに、すでに同じオーダー番号の情報が登録されていないか確認する
        ds = MyBase.CallDAC(Me._Dac, "SelectInkaLCountMH", ds)

        Dim count As Integer = MyBase.GetResultCount()
        If 0 < count Then
            'メッセージ出力　既に同じオーダー番号が登録されている
            strMsg = String.Concat("オーダー番号:" & ds.Tables("LMC010_MEIHEN_OUTKA_L").Rows(0).Item("CUST_ORD_NO").ToString)
            MyBase.SetMessageStore("00", "E269", New String() {strMsg}, ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0).Item("ROW_NO").ToString)
            MyBase.SetMessage("E269")
            ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0).Item("ERR_NO") = "E269"
            Return ds
        End If

        '1商品でも紐づけがない商品があればエラー
        Dim errFlg As Boolean = False
        Dim max As Integer = ds.Tables("LMC010_MEIHEN_OUTKA_M").Rows.Count - 1
        Dim outkaMDr As DataRow = Nothing
        For i As Integer = 0 To max

            outkaMDr = ds.Tables("LMC010_MEIHEN_OUTKA_M").Rows(i)

            If String.IsNullOrEmpty(outkaMDr("CD_NRS_TO").ToString) = True Then
                strMsg = String.Concat("紐付け先の商品なし（" & _
                                       "出荷管理番号:" & ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0).Item("OUTKA_NO_L").ToString & _
                                       " " & "商品コード:" & outkaMDr("GOODS_CD_CUST").ToString & _
                                       " " & "商品名:" & outkaMDr("GOODS_NM_1").ToString & _
                                       "）")     '2018/11/01 商品KEYを削除、商品コード、商品名を追加 要望番号002161
                MyBase.SetMessageStore("00", "E237", New String() {strMsg}, ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0).Item("ROW_NO").ToString)

                errFlg = True
            End If
        Next

        If errFlg = True Then
            MyBase.SetMessage("E237")
            ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0).Item("ERR_NO") = "E237"
        End If

        '入荷日が最終保管荷役計算日を超えている場合はエラー
        Dim inkaDate As String = ds.Tables("LMC010_MEIHEN_OUTKA_L").Rows(0).Item("OUTKO_DATE").ToString
        For i As Integer = 0 To max

            outkaMDr = ds.Tables("LMC010_MEIHEN_OUTKA_M").Rows(i)

            If outkaMDr("HOKAN_NIYAKU_CALCULATION").ToString >= inkaDate Then
                strMsg = String.Concat("出荷管理番号:" & ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0).Item("OUTKA_NO_L").ToString)
                MyBase.SetMessageStore("00", "E409", New String() {strMsg}, ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0).Item("ROW_NO").ToString)

                MyBase.SetMessage("E409")
                ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0).Item("ERR_NO") = "E409"

                Return ds

            End If
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 入荷管理番号(大)取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetInkaNoLMH(ByVal ds As DataSet) As DataSet

        Dim inkaKanriNo As String = String.Empty
        Dim dr As DataRow = ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0)
        Dim nrsBrCd As String = dr("NRS_BR_CD").ToString

        '入荷管理番号(大)をマスタから取得
        Dim num As New NumberMasterUtility
        inkaKanriNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.INKA_NO_L, Me, nrsBrCd)

        '入荷管理番号(大)を格納
        dr("INKA_NO_L") = inkaKanriNo

        Return ds

    End Function

    ''' <summary>
    ''' ZAI_REC_NOを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>ZaiRecNo</returns>
    ''' <remarks></remarks>
    Private Function GetZaiRecNoMH(ByVal ds As DataSet) As String

        Dim dr As DataRow = ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0)
        Dim nrsBrCd As String = dr("NRS_BR_CD").ToString

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.ZAI_REC_NO, Me, nrsBrCd)

    End Function

    ''' <summary>
    ''' データセット設定(入荷L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetInkaLMH(ByVal ds As DataSet) As DataSet

        Dim inDr As DataRow = ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0)
        Dim outkaLDr As DataRow = ds.Tables("LMC010_MEIHEN_OUTKA_L").Rows(0)
        Dim inkaLDr As DataRow = ds.Tables("LMC010_MEIHEN_INKA_L").NewRow()

        '入荷登録
        inkaLDr("NRS_BR_CD") = outkaLDr("NRS_BR_CD")
        inkaLDr("INKA_NO_L") = inDr("INKA_NO_L")
        inkaLDr("INKA_TP") = "10"
        inkaLDr("INKA_KB") = "10"
        inkaLDr("INKA_STATE_KB") = "40"
        inkaLDr("INKA_DATE") = outkaLDr("OUTKO_DATE")
        inkaLDr("WH_CD") = outkaLDr("WH_CD")
        inkaLDr("CUST_CD_L") = inDr("CUST_CD_L")
        inkaLDr("CUST_CD_M") = inDr("CUST_CD_M")
        inkaLDr("INKA_TTL_NB") = outkaLDr("OUTKA_PKG_NB")
        inkaLDr("BUYER_ORD_NO_L") = outkaLDr("BUYER_ORD_NO")
        inkaLDr("OUTKA_FROM_ORD_NO_L") = outkaLDr("CUST_ORD_NO")
        inkaLDr("TOUKI_HOKAN_YN") = "01"
        inkaLDr("HOKAN_YN") = "01"
        inkaLDr("HOKAN_STR_DATE") = outkaLDr("OUTKO_DATE")
        inkaLDr("NIYAKU_YN") = "00"
        inkaLDr("TAX_KB") = inDr("TAX_KB")
        inkaLDr("UNCHIN_TP") = "90"
        inkaLDr("WH_KENPIN_WK_STATUS") = outkaLDr("WH_KENPIN_WK_STATUS")
        inkaLDr("SYS_DEL_FLG") = "0"
        inkaLDr("WH_TAB_STATUS") = "00"
        inkaLDr("WH_TAB_YN") = outkaLDr("WH_TAB_YN")
        inkaLDr("WH_TAB_IMP_YN") = "00"
        'データセットに設定
        ds.Tables("LMC010_MEIHEN_INKA_L").Rows.Add(inkaLDr)

        Return ds

    End Function

    ''' <summary>
    ''' データセット設定(入荷M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetInkaMMH(ByVal ds As DataSet) As DataSet

        Dim inDr As DataRow = ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0)
        Dim outkaMDr As DataRow = Nothing
        Dim inkaMDr As DataRow = Nothing
        Dim max As Integer = ds.Tables("LMC010_MEIHEN_OUTKA_M").Rows.Count - 1

        For i As Integer = 0 To max

            inkaMDr = ds.Tables("LMC010_MEIHEN_INKA_M").NewRow()
            outkaMDr = ds.Tables("LMC010_MEIHEN_OUTKA_M").Rows(i)

            inkaMDr("NRS_BR_CD") = outkaMDr("NRS_BR_CD")
            inkaMDr("INKA_NO_L") = inDr("INKA_NO_L")
            inkaMDr("INKA_NO_M") = outkaMDr("OUTKA_NO_M")
            inkaMDr("GOODS_CD_NRS") = outkaMDr("CD_NRS_TO")
            inkaMDr("PRINT_SORT") = "99"
            inkaMDr("SYS_DEL_FLG") = "0"

            'データセットに設定
            ds.Tables("LMC010_MEIHEN_INKA_M").Rows.Add(inkaMDr)
        Next

        Return ds

    End Function

    ''' <summary>
    ''' データセット設定(入荷S)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetInkaSMH(ByVal ds As DataSet) As DataSet

        Dim inDr As DataRow = ds.Tables("LMC010_MEIHEN_INKA_IN").Rows(0)
        Dim outkaSDr As DataRow = Nothing
        Dim outkaMDr As DataRow = Nothing
        Dim inkaSDr As DataRow = Nothing
        Dim maxM As Integer = ds.Tables("LMC010_MEIHEN_OUTKA_M").Rows.Count - 1
        Dim maxS As Integer = ds.Tables("LMC010_MEIHEN_OUTKA_S").Rows.Count - 1
        Dim goodsPkgNb As Decimal = 0

        For i As Integer = 0 To maxS

            inkaSDr = ds.Tables("LMC010_MEIHEN_INKA_S").NewRow()
            outkaSDr = ds.Tables("LMC010_MEIHEN_OUTKA_S").Rows(i)

            inkaSDr("NRS_BR_CD") = outkaSDr("NRS_BR_CD")
            inkaSDr("INKA_NO_L") = inDr("INKA_NO_L")
            inkaSDr("INKA_NO_M") = outkaSDr("OUTKA_NO_M")
            inkaSDr("INKA_NO_S") = outkaSDr("OUTKA_NO_S")
            inkaSDr("ZAI_REC_NO") = Me.GetZaiRecNoMH(ds)
            inkaSDr("LOT_NO") = outkaSDr("LOT_NO")
            inkaSDr("LOCA") = outkaSDr("LOCA")
            inkaSDr("TOU_NO") = outkaSDr("TOU_NO")
            inkaSDr("SITU_NO") = outkaSDr("SITU_NO")
            inkaSDr("ZONE_CD") = outkaSDr("ZONE_CD")
            '梱数、端数の設定
            '出荷Mから商品Mの包装個数(PKG_NB)を取得
            For j As Integer = 0 To maxM

                outkaMDr = ds.Tables("LMC010_MEIHEN_OUTKA_M").Rows(j)

                If outkaMDr("OUTKA_NO_M").ToString = outkaSDr("OUTKA_NO_M").ToString Then

                    If String.IsNullOrEmpty(outkaMDr("GOODS_PKG_NB").ToString) = True Then
                        goodsPkgNb = 0
                    Else
                        goodsPkgNb = Convert.ToDecimal(outkaMDr("GOODS_PKG_NB"))
                    End If

                    Exit For

                End If
            Next
            If 0 < goodsPkgNb Then
                inkaSDr("KONSU") = Math.Floor(Convert.ToDecimal(outkaSDr("ALCTD_NB")) / goodsPkgNb)
                inkaSDr("HASU") = Convert.ToDecimal(outkaSDr("ALCTD_NB")) Mod goodsPkgNb
            Else
                inkaSDr("KONSU") = 0
                inkaSDr("HASU") = 0
            End If

            inkaSDr("IRIME") = outkaSDr("IRIME")
            inkaSDr("BETU_WT") = outkaSDr("BETU_WT")
            inkaSDr("SERIAL_NO") = outkaSDr("SERIAL_NO")
            '2018/11/01 ADD START 要望管理002824
            inkaSDr("GOODS_CRT_DATE") = outkaSDr("GOODS_CRT_DATE")
            inkaSDr("LT_DATE") = outkaSDr("LT_DATE")
            '2018/11/01 ADD END 要望管理002824
            inkaSDr("SPD_KB") = outkaSDr("SPD_KB")
            inkaSDr("OFB_KB") = outkaSDr("OFB_KB")
            '2018/11/01 ADD START 要望管理002824
            inkaSDr("REMARK") = outkaSDr("ZAI_REMARK")
            inkaSDr("ALLOC_PRIORITY") = outkaSDr("ALLOC_PRIORITY")
            inkaSDr("REMARK_OUT") = outkaSDr("REMARK_OUT")
            '2018/11/01 ADD END 要望管理002824
            inkaSDr("SYS_DEL_FLG") = "0"

            'データセットに設定
            ds.Tables("LMC010_MEIHEN_INKA_S").Rows.Add(inkaSDr)
        Next

        Return ds

    End Function

    ''' <summary>
    ''' データセット設定(在庫データ)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetZaiTrsMH(ByVal ds As DataSet) As DataSet

        Dim inkaLDr As DataRow = ds.Tables("LMC010_MEIHEN_INKA_L").Rows(0)
        Dim inkaSDr As DataRow = Nothing
        Dim outkaMDr As DataRow = Nothing
        Dim zaiTrsDr As DataRow = Nothing
        Dim maxM As Integer = ds.Tables("LMC010_MEIHEN_OUTKA_M").Rows.Count - 1
        Dim maxS As Integer = ds.Tables("LMC010_MEIHEN_INKA_S").Rows.Count - 1
        Dim goodsCdNrs As String = String.Empty
        Dim goodsPkgNb As Decimal = 0

        For i As Integer = 0 To maxS

            zaiTrsDr = ds.Tables("LMC010_MEIHEN_ZAI_TRS").NewRow()
            inkaSDr = ds.Tables("LMC010_MEIHEN_INKA_S").Rows(i)

            '出荷Mから商品Mの振替先商品キー、包装個数(PKG_NB)を取得
            For j As Integer = 0 To maxM

                outkaMDr = ds.Tables("LMC010_MEIHEN_OUTKA_M").Rows(j)

                If outkaMDr("OUTKA_NO_M").ToString = inkaSDr("INKA_NO_M").ToString Then

                    '振替先商品コード
                    goodsCdNrs = outkaMDr("CD_NRS_TO").ToString

                    '包装個数
                    If String.IsNullOrEmpty(outkaMDr("GOODS_PKG_NB").ToString) = True Then
                        goodsPkgNb = 0
                    Else
                        goodsPkgNb = Convert.ToDecimal(outkaMDr("GOODS_PKG_NB"))
                    End If

                    Exit For

                End If
            Next

            zaiTrsDr("NRS_BR_CD") = inkaSDr("NRS_BR_CD")
            zaiTrsDr("ZAI_REC_NO") = inkaSDr("ZAI_REC_NO")
            zaiTrsDr("WH_CD") = inkaLDr("WH_CD")
            zaiTrsDr("TOU_NO") = inkaSDr("TOU_NO")
            zaiTrsDr("SITU_NO") = inkaSDr("SITU_NO")
            zaiTrsDr("ZONE_CD") = inkaSDr("ZONE_CD")
            zaiTrsDr("LOCA") = inkaSDr("LOCA")
            zaiTrsDr("LOT_NO") = inkaSDr("LOT_NO")
            zaiTrsDr("CUST_CD_L") = inkaLDr("CUST_CD_L")
            zaiTrsDr("CUST_CD_M") = inkaLDr("CUST_CD_M")
            zaiTrsDr("GOODS_CD_NRS") = goodsCdNrs
            zaiTrsDr("GOODS_KANRI_NO") = String.Empty
            zaiTrsDr("INKA_NO_L") = inkaSDr("INKA_NO_L")
            zaiTrsDr("INKA_NO_M") = inkaSDr("INKA_NO_M")
            zaiTrsDr("INKA_NO_S") = inkaSDr("INKA_NO_S")
            zaiTrsDr("ALLOC_PRIORITY") = inkaSDr("ALLOC_PRIORITY")      '2018/11/01 値をセットするよう変更 要望番号002824
            zaiTrsDr("RSV_NO") = String.Empty
            zaiTrsDr("SERIAL_NO") = inkaSDr("SERIAL_NO")
            zaiTrsDr("HOKAN_YN") = inkaLDr("HOKAN_YN")
            zaiTrsDr("TAX_KB") = inkaLDr("TAX_KB")
            zaiTrsDr("GOODS_COND_KB_1") = String.Empty
            zaiTrsDr("GOODS_COND_KB_2") = String.Empty
            zaiTrsDr("GOODS_COND_KB_3") = String.Empty
            zaiTrsDr("OFB_KB") = inkaSDr("OFB_KB")
            zaiTrsDr("SPD_KB") = inkaSDr("SPD_KB")
            zaiTrsDr("REMARK_OUT") = inkaSDr("REMARK_OUT")              '2018/11/01 値をセットするよう変更 要望番号002824
            '実予在庫梱数
            zaiTrsDr("PORA_ZAI_NB") = (Convert.ToDecimal(inkaSDr("KONSU")) * goodsPkgNb) + Convert.ToDecimal(inkaSDr("HASU"))
            zaiTrsDr("ALCTD_NB") = "0"
            '引当可能梱数
            zaiTrsDr("ALLOC_CAN_NB") = zaiTrsDr("PORA_ZAI_NB")     '実予在庫梱数と同じ  
            zaiTrsDr("IRIME") = inkaSDr("IRIME")
            '実予在庫数量
            zaiTrsDr("PORA_ZAI_QT") = Convert.ToDecimal(zaiTrsDr("PORA_ZAI_NB")) * Convert.ToDecimal(inkaSDr("IRIME"))
            zaiTrsDr("ALCTD_QT") = "0"
            zaiTrsDr("ALLOC_CAN_QT") = zaiTrsDr("PORA_ZAI_QT")     '実予在庫数量と同じ
            zaiTrsDr("INKO_DATE") = inkaLDr("INKA_DATE")
            zaiTrsDr("INKO_PLAN_DATE") = inkaLDr("INKA_DATE")
            zaiTrsDr("ZERO_FLAG") = String.Empty
            zaiTrsDr("LT_DATE") = inkaSDr("LT_DATE")                    '2018/11/01 値をセットするよう変更 要望番号002824
            zaiTrsDr("GOODS_CRT_DATE") = inkaSDr("GOODS_CRT_DATE")      '2018/11/01 値をセットするよう変更 要望番号002824
            zaiTrsDr("DEST_CD_P") = String.Empty
            zaiTrsDr("REMARK") = inkaSDr("REMARK")                      '2018/11/01 値をセットするよう変更 要望番号002824
            zaiTrsDr("SMPL_FLAG") = "00"

            'データセットに設定
            ds.Tables("LMC010_MEIHEN_ZAI_TRS").Rows.Add(zaiTrsDr)
        Next

        Return ds

    End Function

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

#End Region

#End Region

End Class
