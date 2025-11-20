' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDIサブ
'  プログラムID     :  LMH020    : EDI入荷編集
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMF020BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH020BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH020DAC = New LMH020DAC()

#End Region

#Region "Const"

    ''' <summary>
    ''' EDI受信テーブル名抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_HED_TBL As String = "SelectHedTbl"

    ''' <summary>
    ''' フリー項目名抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_FREE As String = "SelectFree"

    ''' <summary>
    ''' EDI入荷(大)のデータ抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_L As String = "SelectLData"

    ''' <summary>
    ''' EDI入荷(中)のデータ抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_M As String = "SelectMData"

    ''' <summary>
    ''' EDI入荷(大)の排他アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_HAITA As String = "SelectHaitaData"

    ''' <summary>
    ''' 荷主検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_CUST As String = "SelectCustData"

    ''' <summary>
    ''' 請求ヘッダ検索(保管料)アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_G_HED_HOKAN As String = "SelectGheaderDataHokan"

    ''' <summary>
    ''' 請求ヘッダ検索(荷役料)アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_G_HED_NIYAKU As String = "SelectGheaderDataNiyaku"

    ''' <summary>
    ''' 請求ヘッダ検索(運賃)アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_G_HED_UNCHIN As String = "SelectGheaderDataUnchin"

    ''' <summary>
    ''' EDI入荷(大)の更新アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_L As String = "UpdateEdiInkaLData"

    ''' <summary>
    ''' EDI入荷(中)の更新アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_M As String = "UpdateEdiInkaMData"

    ''' <summary>
    ''' EDI(明細)受信テーブルの更新アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_DTL As String = "UpdateDtlTblData"

    ''' <summary>
    ''' INKAEDI_Lテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_L As String = "INKAEDI_L"

    ''' <summary>
    ''' INKAEDI_Mテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_M As String = "INKAEDI_M"

    ''' <summary>
    ''' CUSTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_CUST As String = "CUST"

    ''' <summary>
    ''' G_HEDテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_G_HED As String = "G_HED"

    ''' <summary>
    ''' EDI受信テーブル更新条件
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_JUSHIN_DATA As String = " SYS_DEL_FLG <> JYOTAI "

    ''' <summary>
    ''' 運送手配(日陸手配)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TEHAI_NRS As String = "10"

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 初期検索の値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'EDI受信テーブル名を取得
        ds = Me.DacAccess(ds, LMH020BLC.ACTION_ID_SELECT_HED_TBL)

        'フリー項目名を取得
        ds = Me.DacAccess(ds, LMH020BLC.ACTION_ID_SELECT_FREE)

        'EDI入荷(大)情報を取得
        ds = Me.DacAccess(ds, LMH020BLC.ACTION_ID_SELECT_L)

        'EDI入荷(中)情報を取得
        ds = Me.DacAccess(ds, LMH020BLC.ACTION_ID_SELECT_M)

        Return ds

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

        Dim dt As DataTable = ds.Tables(LMH020BLC.TABLE_NM_G_HED)
        If dt.Rows.Count < 1 Then

            Return "00000000"

        End If

        Return dt.Rows(0).Item("SKYU_DATE").ToString()

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveItemData(ByVal ds As DataSet) As DataSet

        ''最終請求日のチェック
        'Dim rtnResult As Boolean = Me.ChkSeiqDate(ds)
        Dim rtnResult As Boolean = True

        'EDI入荷(大)の更新
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMH020BLC.ACTION_ID_UPDATE_L)

        'EDI入荷(中)の更新
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMH020BLC.ACTION_ID_UPDATE_M)


        'EDI(ヘッダ)受信テーブルの更新
        Dim rcvHedTbl As String = ds.Tables("RCV_NM").Rows(0)("RCV_NM_HED").ToString() '受信HEDテーブル
        If String.IsNullOrEmpty(rcvHedTbl) = False Then
            rtnResult = rtnResult AndAlso Me.UpdateHedTblData(ds)
        End If

        'EDI(明細)受信テーブルの更新
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMH020BLC.ACTION_ID_UPDATE_DTL)

        Return ds

    End Function

    ''' <summary>
    ''' EDI(ヘッダ)受信テーブルの更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateHedTblData(ByVal ds As DataSet) As Boolean

        '状態が変わっていない場合、スルー
        If 0 = ds.Tables(LMH020BLC.TABLE_NM_M).Select(LMH020BLC.SQL_JUSHIN_DATA).Length Then
            Return True
        End If

        Return Me.ServerChkJudge(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

#End Region

#Region "チェック"

    ''' <summary>
    ''' 編集処理のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectEditData(ByVal ds As DataSet) As DataSet

        ''最終請求日のチェック
        'Dim rtnResult As Boolean = Me.ChkSeiqDate(ds)
        Dim rtnResult As Boolean = True

        '排他チェック
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMH020BLC.ACTION_ID_SELECT_HAITA)

        Return ds

    End Function

    ''' <summary>
    ''' 最終請求日のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkSeiqDate(ByVal ds As DataSet) As Boolean

        '請求先コードを荷主から取得
        ds = Me.DacAccess(ds, LMH020BLC.ACTION_ID_SELECT_CUST)

        '取得できない場合、スルー
        If ds.Tables(LMH020BLC.TABLE_NM_CUST).Rows.Count < 1 Then
            Return True
        End If

        '請求日チェック(保管料)
        Dim dr As DataRow = ds.Tables(LMH020BLC.TABLE_NM_L).Rows(0)
        Dim chkDate As String = dr.Item("INKA_DATE").ToString()
        Dim rtnResult As Boolean = Me.ChkDate(chkDate, Me.SelectGheaderData(ds, LMH020BLC.ACTION_ID_SELECT_G_HED_HOKAN), "保管料")

        '請求日チェック(荷役料)
        rtnResult = rtnResult AndAlso Me.ChkDate(chkDate, Me.SelectGheaderData(ds, LMH020BLC.ACTION_ID_SELECT_G_HED_NIYAKU), "荷役料")

        '請求日チェック(運賃)
        rtnResult = rtnResult AndAlso Me.ChkUnchinSeiqDateChk(ds, dr, chkDate)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 運賃の最終請求先のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="chkDate">入荷日</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkUnchinSeiqDateChk(ByVal ds As DataSet, ByVal dr As DataRow, ByVal chkDate As String) As Boolean

        '日陸手配以外、スルー
        If LMH020BLC.TEHAI_NRS.Equals(dr.Item("UNCHIN_TP").ToString()) = False Then
            Return True
        End If

        'タリフ分類区分がない場合、スルー
        If String.IsNullOrEmpty(dr.Item("UNCHIN_KB").ToString()) = True Then
            Return True
        End If

        'TODO:請求先コードの確認
        'Return True
        Return Me.ChkDate(chkDate, Me.SelectGheaderData(ds, LMH020BLC.ACTION_ID_SELECT_G_HED_UNCHIN), "運賃")

    End Function

    ''' <summary>
    ''' 日付チェック
    ''' </summary>
    ''' <param name="value1">比較対象日</param>
    ''' <param name="value2">最終締め日</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkDate(ByVal value1 As String, ByVal value2 As String, ByVal msg As String) As Boolean

        '比較対象1に値がない場合、スルー
        If String.IsNullOrEmpty(value1) = True Then
            Return True
        End If

        'すでに締め処理が締め処理が終了している場合、エラー
        If value1 <= value2 Then

            MyBase.SetMessage("E285", New String() {msg})
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

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
