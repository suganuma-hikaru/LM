' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH040    : EDI出荷データ編集
'  作  成  者       :  [kim]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH040BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH040BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ' 使用するDACクラスの生成
    Private _Dac As LMH040DAC = New LMH040DAC()

#End Region

#Region "アクションConst"

    Private Const ACTION_ID_HAITA_CHECK As String = "HaitaChekuAction"     ' 排他チェックアクション
    Public Const ACTION_ID_EDIT_HAITA As String = "HaitaCheckForEdit"      ' 排他チェックアクション（編集時）
    Private Const ACTION_ID_SAVE As String = "SaveAction"                  ' 保存処理アクション
    Private Const ACTION_ID_SELECT As String = "SelectAction"              ' 検索アクション
    Private Const ACTION_ID_SELECT_INOUTKAEDI_HED_FJF As String = "SelectActionInoutkaEdiHedFjf"    ' 検索アクション(FFEM入出荷EDIデータ(ヘッダ))
    Private Const ACTION_ID_TORIKOMI As String = "TorikomiCheckAction"     ' 取込日付取得アクション


#End Region 'アクションConst

    ' 検索OUTテーブル(検索結果格納)
    Private Const TABLE_NM_OUT_L As String = "LMH040_OUTKAEDI_L"

#Region "検索処理"

    ''' <summary>
    ''' 検索の値取得（コントロール）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索データ件数などを取得</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '在庫履歴データ取得
        ds = Me.DacAccess(ds, LMH040BLC.ACTION_ID_SELECT)

        'メッセージ設定
        Dim count As Integer = ds.Tables(LMH040BLC.TABLE_NM_OUT_L).Rows.Count()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
            Return ds
        End If

        If Me.CheckInoutkaEdiDtlFjfExists(ds) Then
            ' FFEM入出荷EDIデータ(ヘッダ) 取得
            ds = Me.DacAccess(ds, LMH040BLC.ACTION_ID_SELECT_INOUTKAEDI_HED_FJF)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' FFEM入出荷EDIデータ(ヘッダ)テーブル 存在チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function CheckInoutkaEdiDtlFjfExists(ByVal ds As DataSet) As Boolean

        ds.Tables("LMH040_TBL_EXISTS").Clear()
        Dim drTblExists As DataRow = ds.Tables("LMH040_TBL_EXISTS").NewRow()
        drTblExists.Item("NRS_BR_CD") = ds.Tables("LMH040IN_FIX").Rows(0).Item("NRS_BR_CD")
        drTblExists.Item("TBL_NM") = "H_INOUTKAEDI_HED_FJF"
        ds.Tables("LMH040_TBL_EXISTS").Rows.Add(drTblExists)
        ds = Me.GetTrnTblExits(ds)

        Dim drExists As DataRow()
        drExists = ds.Tables("LMH040_TBL_EXISTS").Select("TBL_NM = 'H_INOUTKAEDI_HED_FJF'")
        If drExists.Count > 0 AndAlso drExists(0).Item("TBL_EXISTS").ToString() = "1" Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' 特定の荷主固有のテーブルが存在するか否かの判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function GetTrnTblExits(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "GetTrnTblExits", ds)

    End Function

#End Region

#Region "更新処理（保存）"

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveAction(ByVal ds As DataSet) As DataSet

        '排他チェック
        If Me.ServerChkJudge(ds, LMH040BLC.ACTION_ID_HAITA_CHECK) = False Then
            'エラーが存在する場合、処理中断
            Return ds
        End If

        '保存処理
        ds = Me.DacAccess(ds, LMH040BLC.ACTION_ID_SAVE)

        Return ds

    End Function

#End Region '更新処理（保存）

#Region "チェック"

    ''' <summary>
    ''' 排他チェック検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkHaitaData(ByVal ds As DataSet) As DataSet

        '排他対象データ取得
        ds = Me.DacAccess(ds, LMH040BLC.ACTION_ID_HAITA_CHECK)

        Return ds

    End Function

    ''' <summary>
    ''' 排他チェック（編集時）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function HaitaCheckForEdit(ByVal ds As DataSet) As DataSet

        '排他対象データ取得
        ds = Me.DacAccess(ds, LMH040BLC.ACTION_ID_EDIT_HAITA)

        Return ds

    End Function

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

    ''' <summary>
    ''' 取込日付チェック用データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TorikomiCheckAction(ByVal ds As DataSet) As DataSet

        '日付データ取得
        ds = Me.DacAccess(ds, LMH040BLC.ACTION_ID_HAITA_CHECK)

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

End Class
