' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN010BLF : 出荷データ一覧
'  作  成  者       :  [佐川央]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMN010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN010BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMN010DAC = New LMN010DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 出荷EDIデータ件数検索
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
    ''' 出荷EDIデータ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

    ''' <summary>
    ''' LMS側日付取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function KensakuGetLMSDate(ByVal ds As DataSet) As DataSet

        'テーブル名設定
        Dim inTableNm As String = "LMN010OUT"
        Dim rtnTableNm As String = "LMS_DATE"
        Dim brTableNm As String = "BR_CD_LIST"

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(inTableNm)
        'DataSetの営業所接続情報を取得
        Dim brTbl As DataTable = ds.Tables(brTableNm)

        '結果取得用データセット
        Dim inDs As DataSet = New LMN010DS()

        'IN情報を一行ずつ抽出
        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            '結果取得用データセットを初期化
            inDs.Clear()

            '入力用データセットのIN情報に一行設定
            inDs.Tables(inTableNm).ImportRow(inTbl.Rows(i))
            '入力用データセットの営業所接続情報に設定
            inDs.Tables(brTableNm).ImportRow(brTbl.Rows(0))

            '移行フラグより処理メソッド判断
            If inDs.Tables(brTableNm).Rows(0).Item("IKO_FLG").ToString() = "00" Then
                '移行前
                inDs = MyBase.CallDAC(Me._Dac, "KensakuGetLMSDateLMSVer1", inDs)
            Else
                '移行済み
                inDs = MyBase.CallDAC(Me._Dac, "KensakuGetLMSDateLMSVer2", inDs)
            End If

            '結果をリターンデータセットにマージ
            ds.Tables(rtnTableNm).Merge(inDs.Tables(rtnTableNm))

        Next

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 出荷EDIデータL存在チェック(N_OUTKASCM_L)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistN_OUTKASCM_L(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistN_OUTKASCM_L", ds)

        '処理件数による判定
        If MyBase.GetResultCount() > 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E010")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 出荷EDIデータL排他チェック(N_OUTKASCM_L)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaN_OUTKASCM_L(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckHaitaN_OUTKASCM_L", ds)

        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then
            '0件の場合、論理排他メッセージを設定する
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' BP出荷EDI受信ヘッダデータ排他チェック(N_OUTKASCM_HED_BP)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaN_OUTKASCM_HED_BP(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckHaitaN_OUTKASCM_HED_BP", ds)

        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then
            '0件の場合、論理排他メッセージを設定する
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 出荷EDIデータL新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SetteiInsertN_OUTKASCM_L(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SetteiInsertN_OUTKASCM_L", ds)

    End Function

    ''' <summary>
    ''' 出荷EDIデータM新規登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetteiInsertN_OUTKASCM_M(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SetteiInsertN_OUTKASCM_M", ds)

    End Function

    ''' <summary>
    ''' 出荷EDIデータL更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SetteiUpdateN_OUTKASCM_L(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SetteiUpdateN_OUTKASCM_L", ds)

    End Function

    ''' <summary>
    ''' BP出荷EDI受信ヘッダデータ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SetteiUpdateN_OUTKASCM_HED_BP(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SetteiUpdateN_OUTKASCM_HED_BP", ds)

    End Function

    ''' <summary>
    ''' BP出荷EDI受信明細データ抽出
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SetteiSelectN_OUTKASCM_DTL_BP(ByVal ds As DataSet) As DataSet

        'テーブル名設定
        Dim inTableNm As String = "LMN010IN"
        Dim rtnTableNm As String = "N_OUTKASCM_DTL_BP"

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(inTableNm)

        '結果取得用データセット
        Dim inDs As DataSet = New LMN010DS

        'IN情報を一行ずつ抽出
        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            '結果取得用データセットを初期化
            inDs.Clear()

            '入力用データセットのIN情報に一行設定
            inDs.Tables(inTableNm).ImportRow(inTbl.Rows(i))

            inDs = MyBase.CallDAC(Me._Dac, "SetteiSelectN_OUTKASCM_DTL_BP", inDs)

            'SCM管理番号Mを採番
            Dim rtnTblNum As Integer = inDs.Tables(rtnTableNm).Rows.Count - 1
            For j As Integer = 0 To rtnTblNum
                'SCM管理番号M取得(左0詰め)
                Dim scmCtlNoM As String = (j + 1).ToString("D4")
                'SCM管理番号Mを設定
                inDs.Tables(rtnTableNm).Rows(j).Item("SCM_CTL_NO_M") = scmCtlNoM
            Next

            '結果をリターンデータセットにマージ
            ds.Tables(rtnTableNm).Merge(inDs.Tables(rtnTableNm))

        Next

        Return ds

    End Function

    ''' <summary>
    ''' BP出荷EDI受信明細データ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SetteiUpdateN_OUTKASCM_DTL_BP(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SetteiUpdateN_OUTKASCM_DTL_BP", ds)

    End Function

#End Region

#Region "送信指示処理"

    ''' <summary>
    ''' 出荷EDIデータL排他チェック(N_OUTKASCM_L)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SoushinShijiCheckHaitaN_OUTKASCM_L(ByVal ds As DataSet) As DataSet

        'テーブル名設定
        Dim inTableNm As String = "LMN010IN"

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(inTableNm)

        '結果取得用データセット
        Dim inDs As DataSet = New LMN010DS()

        'IN情報を一行ずつ抽出
        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            '結果取得用データセットを初期化
            inDs.Clear()

            '入力用データセットのIN情報に一行設定
            inDs.Tables(inTableNm).ImportRow(inTbl.Rows(i))

            'DACクラス呼出
            inDs = MyBase.CallDAC(Me._Dac, "CheckHaitaN_OUTKASCM_L", inDs)

            '処理件数による判定
            If MyBase.GetResultCount() = 0 Then
                '0件の場合、論理排他メッセージを設定する
                MyBase.SetMessage("E011")
                Exit For
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 送信指示データ取得(N_OUTKASCM_HED_BP)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SoushinShijiSelectN_OUTKASCM_HED_BP(ByVal ds As DataSet) As DataSet

        'テーブル名設定
        Dim inTableNm As String = "LMN010IN"
        Dim rtnTableNm As String = "N_OUTKASCM_HED_BP"

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(inTableNm)

        '結果取得用データセット
        Dim inDs As DataSet = New LMN010DS()

        'IN情報を一行ずつ抽出
        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            '結果取得用データセットを初期化
            inDs.Clear()

            '入力用データセットのIN情報に一行設定
            inDs.Tables(inTableNm).ImportRow(inTbl.Rows(i))

            inDs = MyBase.CallDAC(Me._Dac, "SoushinShijiSelectN_OUTKASCM_HED_BP", inDs)

            '結果をリターンデータセットにマージ
            ds.Tables(rtnTableNm).Merge(inDs.Tables(rtnTableNm))

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 送信指示データ取得(N_OUTKASCM_DTL_BP)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SoushinShijiSelectN_OTUKASCM_DTL_BP(ByVal ds As DataSet) As DataSet

        'テーブル名設定
        Dim inTableNm As String = "N_OUTKASCM_HED_BP"
        Dim rtnTableNm As String = "N_OUTKASCM_DTL_BP"

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(inTableNm)

        '結果取得用データセット
        Dim inDs As DataSet = New LMN010DS()

        'IN情報を一行ずつ抽出
        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            '結果取得用データセットを初期化
            inDs.Clear()

            '入力用データセットのIN情報に一行設定
            inDs.Tables(inTableNm).ImportRow(inTbl.Rows(i))

            inDs = MyBase.CallDAC(Me._Dac, "SoushinShijiSelectN_OTUKASCM_DTL_BP", inDs)

            '結果をリターンデータセットにマージ
            ds.Tables(rtnTableNm).Merge(inDs.Tables(rtnTableNm))

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 出荷EDIデータL更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SoushinShijiUpdateN_OUTKASCM_L(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SoushinShijiUpdateN_OUTKASCM_L", ds)

    End Function

#End Region

#Region "実績取込処理"

    ''' <summary>
    ''' BP入出庫報告EDI送信データ存在チェック(N_SEND_BP)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistN_SEND_BP(ByVal ds As DataSet) As DataSet

        'テーブル名設定
        Dim inTableNm As String = "N_SEND_BP"

        'DataSetの取込送信データを取得
        Dim inTbl As DataTable = ds.Tables(inTableNm)

        '結果取得用データセット
        Dim inDs As DataSet = New LMN010DS()

        'IN情報を一行ずつ抽出
        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'データセットを初期化
            inDs.Clear()

            '入力用データセットの取込送信データに一行設定
            inDs.Tables(inTableNm).ImportRow(inTbl.Rows(i))

            'DACクラス呼出
            inDs = MyBase.CallDAC(Me._Dac, "CheckExistN_SEND_BP", inDs)

            '処理件数による判定
            If MyBase.GetResultCount() > 0 Then
                '1件以上の場合、マスタ存在メッセージを設定する
                MyBase.SetMessage("E010")
                Exit For
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' (LMS)実績取込データ取得(N_SEND_BP)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiTorikomiSelectN_SEND_BP(ByVal ds As DataSet) As DataSet

        'テーブル名設定
        Dim inTableNm As String = "BR_CD_LIST"
        Dim rtnTableNm As String = "N_SEND_BP"

        'DataSetの営業所接続情報を取得
        Dim inTbl As DataTable = ds.Tables(inTableNm)

        '結果取得用データセット
        Dim inDs As DataSet = New LMN010DS()

        'IN情報を一行ずつ抽出
        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            '結果取得用データセットを初期化
            inDs.Clear()

            '入力用データセットの営業所接続情報に一行設定
            inDs.Tables(inTableNm).ImportRow(inTbl.Rows(i))

            'LMS側BP入出庫報告EDI送信データから抽出
            '移行フラグより処理メソッド判断
            If inDs.Tables(inTableNm).Rows(0).Item("IKO_FLG").ToString() = "00" Then
                '移行前
                inDs = MyBase.CallDAC(Me._Dac, "JissekiTorikomiSelectN_SEND_BPLMSVer1", inDs)
            Else
                '移行済み
                inDs = MyBase.CallDAC(Me._Dac, "JissekiTorikomiSelectN_SEND_BPLMSVer2", inDs)
            End If

            '結果をリターンデータセットにマージ
            ds.Tables(rtnTableNm).Merge(inDs.Tables(rtnTableNm))

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 実績取込データ取得(N_OUTKASCM_DTL_BP)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiTorikomiSelectN_OUTKASCM_DTL_BP(ByVal ds As DataSet) As DataSet

        'テーブル名設定
        Dim inTableNm As String = "N_SEND_BP"
        Dim rtnTableNm As String = "N_OUTKASCM_DTL_BP"

        'DataSetの実績取込データ情報を取得
        Dim inTbl As DataTable = ds.Tables(inTableNm)

        '結果取得用データセット
        Dim inDs As DataSet = New LMN010DS()

        'IN情報を一行ずつ抽出
        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            '結果取得用データセットを初期化
            inDs.Clear()

            '入力用データセットの実績取込データ情報に一行設定
            inDs.Tables(inTableNm).ImportRow(inTbl.Rows(i))

            'LMS側BP入出庫報告EDI送信データから抽出
            inDs = MyBase.CallDAC(Me._Dac, "JissekiTorikomiSelectN_OUTKASCM_DTL_BP", inDs)

            '結果をリターンデータセットにマージ
            ds.Tables(rtnTableNm).Merge(inDs.Tables(rtnTableNm))

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 実績取込データ新規登録(N_SEND_BP)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiTorikomiInsertN_SEND_BP(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "JissekiTorikomiInsertN_SEND_BP", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 出荷EDI受信明細データ更新(N_OUTKASCM_DTL_BP)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiTorikomiUpdateN_OUTKASCM_DTL_BP(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "JissekiTorikomiUpdateN_OUTKASCM_DTL_BP", ds)

    End Function

    ''' <summary>
    ''' 出荷EDIデータM更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiTorikomiUpdateN_OUTKASCM_M(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "JissekiTorikomiUpdateN_OUTKASCM_M", ds)

    End Function

#End Region

#Region "実績送信処理"

    ''' <summary>
    ''' 実績送信データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinSelectN_SEND_BP(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "JissekiSoushinSelectN_SEND_BP", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' SCM側出荷日、納入日を取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinCheckSCMDate(ByVal ds As DataSet) As DataSet

        'テーブル名設定
        Dim inTableNm As String = "N_SEND_BP"
        Dim rtnTableNm As String = "SCM_DATE"

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(inTableNm)

        '結果取得用データセット
        Dim inDs As DataSet = New LMN010DS()

        'IN情報を一行ずつ抽出
        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            '結果取得用データセットを初期化
            inDs.Clear()

            '入力用データセットのIN情報に一行設定
            inDs.Tables(inTableNm).ImportRow(inTbl.Rows(i))

            inDs = MyBase.CallDAC(Me._Dac, "JissekiSoushinCheckSCMDate", inDs)

            '結果をリターンデータセットにマージ
            ds.Tables(rtnTableNm).Merge(inDs.Tables(rtnTableNm))

        Next

        Return ds

    End Function

    ''' <summary>
    ''' LMS側出荷日、納入日を取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinCheckLMSDate(ByVal ds As DataSet) As DataSet

        'テーブル名設定
        Dim inTableNm As String = "N_SEND_BP"
        Dim rtnTableNm As String = "LMS_DATE"
        Dim brTableNm As String = "BR_CD_LIST"

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(inTableNm)
        'DataSetの営業所接続情報を取得
        Dim brTbl As DataTable = ds.Tables(brTableNm)

        '結果取得用データセット
        Dim inDs As DataSet = New LMN010DS()

        'IN情報を一行ずつ抽出
        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            '結果取得用データセットを初期化
            inDs.Clear()

            '入力用データセットのIN情報に一行設定
            inDs.Tables(inTableNm).ImportRow(inTbl.Rows(i))
            '入力用データセットの営業所接続情報に設定
            inDs.Tables(brTableNm).ImportRow(brTbl.Rows(0))

            '移行フラグにより処理選択
            If inDs.Tables(brTableNm).Rows(0).Item("IKO_FLG").ToString() = "00" Then
                '移行前
                inDs = MyBase.CallDAC(Me._Dac, "JissekiSoushinCheckLMSDateLMSVer1", inDs)
            Else
                '移行済み
                inDs = MyBase.CallDAC(Me._Dac, "JissekiSoushinCheckLMSDateLMSVer2", inDs)
            End If


            '結果をリターンデータセットにマージ
            ds.Tables(rtnTableNm).Merge(inDs.Tables(rtnTableNm))

        Next

        Return ds

    End Function

    ''' <summary>
    ''' BP入出庫報告EDI送信データ（N_SEND_BP）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinUpdateN_SEND_BP(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "JissekiSoushinUpdateN_SEND_BP", ds)

        Return ds

    End Function

    ''' <summary>
    ''' BP出荷EDI受信明細データ（N_OUTKASCM_DTL_BP）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinUpdateN_OUTKASCM_DTL_BP(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "JissekiSoushinUpdateN_OUTKASCM_DTL_BP", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 出荷EDIデータM(N_OUTKASCM_M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinUpdateN_OUTKASCM_M(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "JissekiSoushinUpdateN_OUTKASCM_M", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 出荷EDIデータL(N_OUTKASCM_L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinUpdateN_OUTKASCM_L(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "JissekiSoushinUpdateN_OUTKASCM_L", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 接続データベース名取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinGetDBName(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "JissekiSoushinGetDBName", ds)

    End Function

    ''' <summary>
    ''' (LMS)BP出荷EDI受信ヘッダデータ(H_OUTKAEDI_DTL_BP)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinUpdateH_OUTKAEDI_DTL_BP(ByVal ds As DataSet) As DataSet

        '営業所接続情報を取得
        Dim brCdTable As DataTable = ds.Tables("BR_CD_LIST")

        '移行フラグより処理メソッド判断
        If brCdTable.Rows(0).Item("IKO_FLG").ToString() = "00" Then
            '移行前
            ds = MyBase.CallDAC(Me._Dac, "JissekiSoushinUpdateH_OUTKAEDI_DTL_BPLMSVer1", ds)
        Else
            '移行済み
            ds = MyBase.CallDAC(Me._Dac, "JissekiSoushinUpdateH_OUTKAEDI_DTL_BPLMSVer2", ds)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' (LMS)BP入出庫報告EDI送信データ(H_SENDEDI_BP)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushinUpdateH_SENDEDI_BP(ByVal ds As DataSet) As DataSet

        '営業所接続情報を取得
        Dim brCdTable As DataTable = ds.Tables("BR_CD_LIST")

        '移行フラグより処理メソッド判断
        If brCdTable.Rows(0).Item("IKO_FLG").ToString() = "00" Then
            '移行前
            ds = MyBase.CallDAC(Me._Dac, "JissekiSoushinUpdateH_SENDEDI_BPLMSVer1", ds)
        Else
            '移行済み
            ds = MyBase.CallDAC(Me._Dac, "JissekiSoushinUpdateH_SENDEDI_BPLMSVer2", ds)
        End If

        Return ds

    End Function

#End Region

#Region "欠品状態更新"

    ''' <summary>
    ''' 荷主商品コード取得(N_OUTKASCM_M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdKeppinJoutaiSelectN_OUTKASCM_M(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "UpdKeppinJoutaiSelectN_OUTKASCM_M", ds)

        Return ds

    End Function

#End Region

#Region "Spreadダブルクリック"

    ''' <summary>
    ''' 出荷EDIデータL存在チェック(N_OUTKASCM_L)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckNotExistN_OUTKASCM_L(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistN_OUTKASCM_L", ds)

        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E032")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 出荷EDIデータL存在チェック(N_OUTKASCM_L)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckNotExistN_OUTKASCM_HED_BP(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistN_OUTKASCM_HED_BP", ds)

        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E032")
        End If

        Return ds

    End Function

#End Region

#End Region

End Class
