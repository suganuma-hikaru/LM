' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブ
'  プログラムID     :  LMF020BLC : 運送編集
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMF020BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF020BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc010 As LMF010BLC = New LMF010BLC()
    Private _Blc As LMF020BLC = New LMF020BLC()

#End Region

#Region "Const"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMF020IN"

    ''' <summary>
    ''' F_UNSO_Lテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNSO_L As String = "F_UNSO_L"

    ''' <summary>
    ''' SHIHARAI_INFOテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_SHIHARAI_INFO As String = "SHIHARAI_INFO"

    ''' <summary>
    ''' F_SHIHARAIテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_SHIHARAI_UNCHIN As String = "F_SHIHARAI_TRS"

    ''' <summary>
    ''' LMF800INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_800IN As String = "LMF800IN"

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' LMF810INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_810IN As String = "LMF810IN"
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' LMC500INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_500IN As String = "LMC500IN"

    ''' <summary>
    ''' LMC550INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_550IN As String = "LMC550IN"

    ''' <summary>
    ''' LMC566INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_560IN As String = "LMC560IN"

    ''' <summary>
    ''' LMF550INテーブル
    ''' </summary>
    ''' <remarks>2012/06/04 追加 埼玉対応</remarks>
    Private Const TABLE_NM_LMF550IN As String = "LMF550IN"

    ''' <summary>
    ''' LMF800RESULTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_RTN_TBL As String = "LMF800RESULT"

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' LMF800RESULTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_RTN2_TBL As String = "LMF810RESULT"
    'START UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' LMF680INテーブル
    ''' </summary>
    ''' <remarks>2012/06/04 追加 埼玉対応</remarks>
    Private Const TABLE_NM_LMF680IN As String = "LMF680IN"

    ''' <summary>
    ''' LMF690INテーブル
    ''' </summary>
    ''' <remarks>2021/01/2104 追加 026832対応</remarks>
    Private Const TABLE_NM_LMF690IN As String = "LMF690IN"

    ''' <summary>
    ''' LMF700INテーブル
    ''' </summary>
    Private Const TABLE_NM_LMF700IN As String = "LMF700IN"

    ''' <summary>
    ''' LMF710INテーブル
    ''' </summary>
    Private Const TABLE_NM_LMF710IN As String = "LMF710IN"

    ''' <summary>
    ''' 運賃テーブル更新アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_CALC_DATA As String = "CreateUnchinData"

    ''' <summary>
    ''' 印刷処理アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_PRINT As String = "DoPrint"

    ''' <summary>
    ''' 計算プログラムの戻り値(正常終了)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const RTN_KBN_NORMAL As String = "00"

    ''' <summary>
    ''' 計算プログラムの戻り値(ワーニング)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const RTN_KBN_WARNING As String = "05"

    ''' <summary>
    ''' 計算プログラムの戻り値(運賃Zero円)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const RTN_KBN_ZEROYEN As String = "30"

    ''' <summary>
    ''' 納品書
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PRINT_NOUHIN As String = "01"

    ''' <summary>
    ''' 送状
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PRINT_OKURI As String = "02"

    ''' <summary>
    ''' 荷札
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PRINT_NIFUDA As String = "03"

    ''' <summary>
    ''' 物品引取書
    ''' </summary>
    ''' <remarks>2012/06/04 追加 埼玉対応</remarks>
    Private Const PRINT_HIKITORI As String = "04"

    ''' <summary>
    ''' 梱包明細
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PRINT_KONPOU As String = "05"

#If True Then   'ADD 2018/11/20 依頼番号 : 002743   【LMS】運行運送情報画面_一括印刷機能
    ''' <summary>
    ''' 一括印刷
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PRINT_ALL As String = "06"
#End If
#If True Then      'ADD 2021/01/21 026832 【LMS】運送保険料システム化_実装_運送保険申込書対応_運行・運送情報(総務石川)

    ''' <summary>
    ''' 運送保険料申込書印刷
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PRINT_UNSO_HOKEN As String = "07"

#End If

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 初期検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInitData(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 新規検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectNewData(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 排他検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkHaitaData(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 対象運行データキャンセルチェックアクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkCancelData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc010, System.Reflection.MethodBase.GetCurrentMethod.Name, ds)

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, System.Reflection.MethodBase.GetCurrentMethod.Name, True)

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, System.Reflection.MethodBase.GetCurrentMethod.Name, True)

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteAction(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, System.Reflection.MethodBase.GetCurrentMethod.Name, False)

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <param name="updateUnchin">運賃更新フラグ</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ScopeStartEnd(ByVal ds As DataSet _
                                   , ByVal actionId As String _
                                   , ByVal updateUnchin As Boolean _
                                   ) As DataSet

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '更新処理
            rtnResult = Me.SetUnsoData(ds, actionId, updateUnchin)
            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 運送情報の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <param name="updateUnchin">運賃更新フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetUnsoData(ByVal ds As DataSet, ByVal actionId As String, ByVal updateUnchin As Boolean) As Boolean

        '更新処理
        ds = Me.BlcAccess(ds, actionId)

        Dim rtnResult As Boolean = Not MyBase.IsMessageExist()

        'START UMANO 要望番号1302 支払運賃に伴う修正。
        'Return rtnResult AndAlso Me.SetUnchinData(ds, updateUnchin)

        Return rtnResult AndAlso Me.SetUnchinData(ds, updateUnchin) AndAlso Me.SetShiharaiData(ds, updateUnchin)
        'END UMANO 要望番号1302 支払運賃に伴う修正。

    End Function

    ''' <summary>
    ''' BLCクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function BlcAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallBLC(Me._Blc, actionId, ds)

    End Function

    ''' <summary>
    ''' (請求)運賃情報の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="updateUnchin">運賃更新フラグ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetUnchinData(ByVal ds As DataSet, ByVal updateUnchin As Boolean) As Boolean

        '運賃情報を更新しない場合、スルー
        If updateUnchin = False Then
            Return True
        End If

        Dim blc As LMF800BLC = New LMF800BLC()
        Dim inTbl As DataTable = ds.Tables(LMF020BLF.TABLE_NM_800IN)
        inTbl.ImportRow(ds.Tables(LMF020BLF.TABLE_NM_UNSO_L).Rows(0))

        '計算処理
        ds = MyBase.CallBLC(blc, LMF020BLF.ACTION_ID_CALC_DATA, ds)

        '戻り値判定
        Dim rtnDr As DataRow = ds.Tables(LMF020BLF.TABLE_NM_RTN_TBL).Rows(0)
        Select Case rtnDr.Item("STATUS").ToString()

            Case LMF020BLF.RTN_KBN_NORMAL, LMF020BLF.RTN_KBN_WARNING, LMF020BLF.RTN_KBN_ZEROYEN
            Case Else
                MyBase.SetMessage(rtnDr.Item("ERROR_CD").ToString())
                Return False

        End Select

        '更新処理
        ds = Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Return Not MyBase.IsMessageExist()

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' (支払)運賃情報の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="updateShiharai">運賃更新フラグ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetShiharaiData(ByVal ds As DataSet, ByVal updateShiharai As Boolean) As Boolean

        '運賃情報を更新しない場合、スルー
        If updateShiharai = False Then
            Return True
        End If

        Dim blc As LMF810BLC = New LMF810BLC()
        Dim inTbl As DataTable = ds.Tables(LMF020BLF.TABLE_NM_810IN)
        inTbl.ImportRow(ds.Tables(LMF020BLF.TABLE_NM_UNSO_L).Rows(0))

        '計算処理
        ds = MyBase.CallBLC(blc, LMF020BLF.ACTION_ID_CALC_DATA, ds)

        '戻り値判定
        Dim rtnDr As DataRow = ds.Tables(LMF020BLF.TABLE_NM_RTN2_TBL).Rows(0)
        Select Case rtnDr.Item("STATUS").ToString()

            Case LMF020BLF.RTN_KBN_NORMAL, LMF020BLF.RTN_KBN_WARNING, LMF020BLF.RTN_KBN_ZEROYEN
            Case Else
                MyBase.SetMessage(rtnDr.Item("ERROR_CD").ToString())
                Return False

        End Select

        If Not ds.Tables(LMF020BLF.TABLE_NM_SHIHARAI_INFO).Rows(0).Item("DECI_UNCHIN").ToString.Equals("0") Then

            ds.Tables(LMF020BLF.TABLE_NM_SHIHARAI_UNCHIN).Rows(0).Item("DECI_UNCHIN") = ds.Tables(LMF020BLF.TABLE_NM_SHIHARAI_INFO).Rows(0).Item("DECI_UNCHIN")

        End If

        '更新処理
        ds = Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Return Not MyBase.IsMessageExist()

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 印刷実行処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet
        Return Me.DoPrint(ds, ds.Tables(LMF020BLF.TABLE_NM_UNSO_L).Rows(0).Item("PRINT_KB").ToString())
    End Function

    ''' <summary>
    ''' 印刷処理実行
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="printType">印刷種別</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet, ByVal printType As String) As DataSet
#If False Then  'UPD 2018/11/20 依頼番号 : 002743   【LMS】運行運送情報画面_一括印刷機能(
        Dim setDs As DataSet = Nothing
        Dim prtBlc As Com.Base.BaseBLC = Nothing

        Select Case printType

            Case LMF020BLF.PRINT_NOUHIN
                '納品書
                setDs = Me.SetDataSetLMC500InData(ds)
                prtBlc = New LMC500BLC()

            Case LMF020BLF.PRINT_NIFUDA
                '荷札
                setDs = Me.SetDataSetLMC550InData(ds)
                prtBlc = New LMC550BLC()

            Case LMF020BLF.PRINT_OKURI
                '送り状
                setDs = Me.SetDataSetLMC560InData(ds)
                prtBlc = New LMC560BLC()

            Case LMF020BLF.PRINT_HIKITORI
                '物品引取書(2012.06.04 追加)
                setDs = Me.SetDataSetLMF550InData(ds)
                prtBlc = New LMF550BLC()

            Case LMF020BLF.PRINT_KONPOU
                setDs = Me.SetDataSetLMF680InData(ds)
                prtBlc = New LMF680BLC()

        End Select

        Return MyBase.CallBLC(prtBlc, LMF020BLF.ACTION_ID_PRINT, setDs)

#Else
        Dim setDs As DataSet() = Nothing
        Dim prtBlc As Com.Base.BaseBLC() = Nothing

        Select Case printType

            Case LMF020BLF.PRINT_NOUHIN
                '納品書
                prtBlc = New Com.Base.BaseBLC() {New LMC500BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC500InData(ds)}

            Case LMF020BLF.PRINT_NIFUDA
                '荷札
                prtBlc = New Com.Base.BaseBLC() {New LMC550BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC550InData(ds)}

            Case LMF020BLF.PRINT_OKURI
                '送り状
                prtBlc = New Com.Base.BaseBLC() {New LMC560BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC560InData(ds)}

            Case LMF020BLF.PRINT_HIKITORI
                '物品引取書(2012.06.04 追加)
                prtBlc = New Com.Base.BaseBLC() {New LMF550BLC()}
                setDs = New DataSet() {Me.SetDataSetLMF550InData(ds)}

            Case LMF020BLF.PRINT_KONPOU
                prtBlc = New Com.Base.BaseBLC() {New LMF680BLC()}
                setDs = New DataSet() {Me.SetDataSetLMF680InData(ds)}

            Case LMF020BLF.PRINT_ALL
#If True Then    'ADD 2019/06/10 005795【LMS】運送メニュー日陸便の場合、一括印刷で荷札印刷しない

                Dim nifuda_FLG As String = ds.Tables(LMF020BLF.TABLE_NM_UNSO_L).Rows(0).Item("NIHUDA_FLAG").ToString

                If ("01").Equals(nifuda_FLG) = True Then
                    prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC550BLC(), New LMC500BLC(), New LMF690BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC550InData(ds), Me.SetDataSetLMC500InData(ds), Me.SetDataSetLMF690InData(ds)}

                Else
                    '荷札を印刷しない
                    prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(),New LMF690BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds), Me.SetDataSetLMF690InData(ds)}

                End If

#End If
#If True Then   'ADD 2021/01/21 026832 【LMS】運送保険料システム化_実装_運送保険申込書対応_運行・運送情報(総務石川)

            Case LMF020BLF.PRINT_UNSO_HOKEN
                prtBlc = New Com.Base.BaseBLC() {New LMF690BLC()}
                setDs = New DataSet() {Me.SetDataSetLMF690InData(ds)}

#End If

            Case "08"
                '運送チェックリスト
                prtBlc = New Com.Base.BaseBLC() {New LMF700BLC()}
                setDs = New DataSet() {Me.SetDataSetLMF700InData(ds)}

            Case "09"
                '立合書（運送）
                prtBlc = New Com.Base.BaseBLC() {New LMF710BLC()}
                setDs = New DataSet() {Me.SetDataSetLMF710InData(ds)}
        End Select

        If prtBlc Is Nothing = True Then
            Return ds
        End If
        Dim max As Integer = prtBlc.Count - 1
        Dim rtnDs As DataSet = Nothing

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        For i As Integer = 0 To max

            If setDs Is Nothing = True Then
                Continue For
            End If

            setDs(i).Merge(New RdPrevInfoDS)

            rtnDs = MyBase.CallBLC(prtBlc(i), LMF020BLF.ACTION_ID_PRINT, setDs(i))

            rdPrevDt.Merge(setDs(i).Tables(LMConst.RD))

        Next

        rtnDs.Tables(LMConst.RD).Clear()
        rtnDs.Tables(LMConst.RD).Merge(rdPrevDt)

        Return rtnDs

#End If

    End Function

    ''' <summary>
    ''' LMC500DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC500InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMC500DS(), LMF020BLF.TABLE_NM_500IN, True, "KANRI_NO_L")

    End Function

    ''' <summary>
    ''' LMC550DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC550InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMC550DS(), LMF020BLF.TABLE_NM_550IN, True)

    End Function

    ''' <summary>
    ''' LMC560DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC560InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMC560DS(), LMF020BLF.TABLE_NM_560IN, False)

    End Function

    ''' <summary>
    ''' LMF550DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks>2012.06.04 物品引取書 追加 埼玉対応</remarks>
    Private Function SetDataSetLMF550InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMF550DS(), LMF020BLF.TABLE_NM_LMF550IN, False, "UNSO_NO_L")

    End Function

    ''' <summary>
    ''' LMF550DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMF680InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMF680DS(), LMF020BLF.TABLE_NM_LMF680IN, True, "UNSO_NO_L")

    End Function

#If True Then   'ADD 2021/01/21 026832 【LMS】運送保険料システム化_実装_運送保険申込書対応_運行・運送情報(総務石川)
    ''' <summary>
    ''' LMF550DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMF690InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMF690InData(ds, New LMF690DS(), LMF020BLF.TABLE_NM_LMF690IN, True, "UNSO_NO_L")


    End Function

#End If

    ''' <summary>
    ''' LMF700DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMF700InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMF700InData(ds, New LMF700DS(), LMF020BLF.TABLE_NM_LMF700IN, True, "UNSO_NO_L")

    End Function

    ''' <summary>
    ''' LMF710DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMF710InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMF710InData(ds, New LMF710DS(), LMF020BLF.TABLE_NM_LMF710IN, True, "UNSO_NO_L")

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="nbFlg">部数設定フラグ</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMCInData(ByVal ds As DataSet _
                                         , ByVal inDs As DataSet _
                                         , ByVal tblNm As String _
                                         , ByVal nbFlg As Boolean _
                                         , Optional ByVal noCol As String = "OUTKA_NO_L") As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim setDr As DataRow = ds.Tables(LMF020BLF.TABLE_NM_UNSO_L).Rows(0)
        dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
        dr.Item(noCol) = setDr.Item("UNSO_NO_L").ToString()
        If nbFlg = True Then
            dr.Item("PRT_NB") = setDr.Item("PRT_NB").ToString()
        End If
        If tblNm.Equals("LMC550IN") = True Then
            dr.Item("PRT_NB_FROM") = setDr.Item("PRT_NB_FROM").ToString()
            dr.Item("PRT_NB_TO") = setDr.Item("PRT_NB_TO").ToString()
        End If
        dr.Item("PTN_FLAG") = LMConst.FLG.ON

        If tblNm.Equals("lmf690IN") Then
            dr.Item("MOTO_DATA_KB") = setDr.Item("MOTO_DATA_KB").ToString()
            dr.Item("KANRI_NO_L") = setDr.Item("KANRI_NO_L").ToString()
        End If
        dt.Rows.Add(dr)

        '帳票テーブル追加
        inDs = Me.SetBetuDataTable(inDs, New RdPrevInfoDS(), LMConst.RD)

        Return inDs

    End Function

#If True Then   'ADD 2022/01/24 026832 【LMS】運送保険料システム化_実装_運送保険申込書対応_運行・運送情報(
    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="nbFlg">部数設定フラグ</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMF690InData(ByVal ds As DataSet _
                                         , ByVal inDs As DataSet _
                                         , ByVal tblNm As String _
                                         , ByVal nbFlg As Boolean _
                                         , Optional ByVal noCol As String = "OUTKA_NO_L") As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim setDr As DataRow = ds.Tables(LMF020BLF.TABLE_NM_UNSO_L).Rows(0)
        dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
        dr.Item(noCol) = setDr.Item("UNSO_NO_L").ToString()

        dr.Item("MOTO_DATA_KB") = setDr.Item("MOTO_DATA_KB").ToString()
        dr.Item("KANRI_NO_L") = setDr.Item("INOUTKA_NO_L").ToString()

        dt.Rows.Add(dr)

        '帳票テーブル追加
        inDs = Me.SetBetuDataTable(inDs, New RdPrevInfoDS(), LMConst.RD)

        Return inDs

    End Function


#End If

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="nbFlg">部数設定フラグ</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMF700InData(ByVal ds As DataSet _
                                         , ByVal inDs As DataSet _
                                         , ByVal tblNm As String _
                                         , ByVal nbFlg As Boolean _
                                         , Optional ByVal noCol As String = "UNSO_NO_L") As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim setDr As DataRow = ds.Tables(LMF020BLF.TABLE_NM_UNSO_L).Rows(0)
        dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
        dr.Item(noCol) = setDr.Item("UNSO_NO_L").ToString()
        If nbFlg Then
            dr.Item("PRT_NB") = setDr.Item("PRT_NB").ToString()
        End If

        dt.Rows.Add(dr)

        '帳票テーブル追加
        inDs = Me.SetBetuDataTable(inDs, New RdPrevInfoDS(), LMConst.RD)

        Return inDs

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="nbFlg">部数設定フラグ</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMF710InData(ByVal ds As DataSet _
                                         , ByVal inDs As DataSet _
                                         , ByVal tblNm As String _
                                         , ByVal nbFlg As Boolean _
                                         , Optional ByVal noCol As String = "UNSO_NO_L") As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim setDr As DataRow = ds.Tables(LMF020BLF.TABLE_NM_UNSO_L).Rows(0)
        dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
        dr.Item(noCol) = setDr.Item("UNSO_NO_L").ToString()
        If nbFlg Then
            dr.Item("PRT_NB") = setDr.Item("PRT_NB").ToString()
        End If

        dt.Rows.Add(dr)

        '帳票テーブル追加
        inDs = Me.SetBetuDataTable(inDs, New RdPrevInfoDS(), LMConst.RD)

        Return inDs

    End Function

    ''' <summary>
    ''' DataTable追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="setDs">DataSet</param>
    ''' <param name="tblNm">Table名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetBetuDataTable(ByVal ds As DataSet, ByVal setDs As DataSet, ByVal tblNm As String) As DataSet

        'DataTableのインスタンス生成
        Dim setDt As DataTable = setDs.Tables(tblNm).Copy
        setDt.TableName = tblNm

        'テーブル追加
        ds.Tables.Add(setDt)

        Return ds

    End Function

#End Region

End Class
