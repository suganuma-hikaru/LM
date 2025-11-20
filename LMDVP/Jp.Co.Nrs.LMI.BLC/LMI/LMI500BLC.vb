' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : データ管理サブ
'  プログラムID     :  LMI500BLC : デュポン在庫報告
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI500BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI500BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI500DAC = New LMI500DAC()

#End Region

#Region "Const"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "CREATE_IN"

    ''' <summary>
    ''' COUNTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_COUNT As String = "COUNT"

    ''' <summary>
    ''' 初期在庫
    ''' </summary>
    ''' <remarks></remarks>
    Private Const START_DATE As String = "00000000"

    ''' <summary>
    ''' 直近在庫
    ''' </summary>
    ''' <remarks></remarks>
    Private Const END_DATE As String = "99999999"

    ''' <summary>
    ''' LMI500SETテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_LMI500SET As String = "LMI500SET"

    ''' <summary>
    ''' LMI501SETテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_LMI501SET As String = "LMI501SET"

    ''' <summary>
    ''' LMI502SETテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_LMI502SET As String = "LMI502SET"

    '(2012.08.20)要望管理番号：1353 在庫数量グラム対応 --- START ---
    ''' <summary>
    ''' グラム対応特殊プラントコード
    ''' </summary>
    ''' <remarks></remarks>
    Private Const GRAM_NRS_BR_CD As String = "20"

    ''' <summary>
    ''' グラム対応特殊プラントコード
    ''' </summary>
    ''' <remarks></remarks>
    Private Const GRAM_CUST_CD_L As String = "12590"

    ''' <summary>
    ''' グラム対応特殊プラントコード
    ''' </summary>
    ''' <remarks></remarks>
    Private Const GRAM_CUST_CD_M As String = "00"

    ''' <summary>
    ''' グラム対応特殊プラントコード
    ''' </summary>
    ''' <remarks></remarks>
    Private Const GRAM_PLANT_CD As String = "VF53"

    '(2012.08.20)要望管理番号：1353 在庫数量グラム対応 ---  END  ---



#End Region

#Region "検索処理"

    ''' <summary>
    ''' 日次在庫報告用データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectNitijiData(ByVal ds As DataSet) As DataSet

        Dim chk As Boolean = Me.SelectGetuData(ds)

        chk = chk AndAlso Me.SelectRirekiCount(ds)

        'エラーの場合、終了
        If chk = False Then
            Return ds
        End If

        'データ取得
        Return Me.ChkSelectData(Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name), LMI500BLC.TABLE_NM_LMI500SET)

    End Function

    ''' <summary>
    ''' 在庫証明書作成データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectZaiData(ByVal ds As DataSet) As DataSet

        Dim chk As Boolean = Me.SelectGetuData(ds)

        chk = chk AndAlso Me.SelectRirekiCount(ds)

        'エラーの場合、終了
        If chk = False Then
            Return ds
        End If

        'データ取得
        Return Me.ChkSelectData(Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name), LMI500BLC.TABLE_NM_LMI501SET)

    End Function

    ''' <summary>
    ''' SFTPデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectSftpData(ByVal ds As DataSet) As DataSet

        Dim chk As Boolean = Me.SelectGetuData(ds)

        chk = chk AndAlso Me.SelectRirekiCount(ds)

        'エラーの場合、終了
        If chk = False Then
            Return ds
        End If

        '(2012.08.20)要望管理番号：1353 在庫数量グラム対応 --- START ---

        'データ取得
        'Return Me.ChkSelectData(Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name), LMI500BLC.TABLE_NM_LMI502SET)

        'INデータ格納
        Dim NrsBrCd As String = ds.Tables("CREATE_IN").Rows(0).Item("NRS_BR_CD").ToString
        Dim CustCdL As String = ds.Tables("CREATE_IN").Rows(0).Item("CUST_CD_L").ToString
        Dim CustCdM As String = ds.Tables("CREATE_IN").Rows(0).Item("CUST_CD_M").ToString
        Dim PlantCd As String = ds.Tables("CREATE_IN").Rows(0).Item("PLANT_CD").ToString

        'データ取得
        ds = Me.ChkSelectData(Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name), LMI500BLC.TABLE_NM_LMI502SET)

        '大阪物流センター、荷主12590-00、プラントコード="VF53"の場合、値を1000倍してグラム表示にする
        If NrsBrCd = GRAM_NRS_BR_CD And CustCdL = GRAM_CUST_CD_L And CustCdM = GRAM_CUST_CD_M And PlantCd = GRAM_PLANT_CD Then
            '在庫数量グラム化(データセット編集)
            ds = Me.EditSelectDataSet(ds)
        End If

        Return ds
        '(2012.08.20)要望管理番号：1353 在庫数量グラム対応 ---  END  ---

    End Function

    '(2012.08.20)要望管理番号：1353 在庫数量グラム対応 --- START ---
    ''' <summary>
    ''' DataSet編集処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditSelectDataSet(ByVal ds As DataSet) As DataSet

        Dim outDt As DataTable = ds.Tables("LMI502SET") 'データ取得
        Dim max As Integer = outDt.Rows.Count - 1       '抽出データ数取得

        '編集処理
        For i As Integer = 0 To max

            '[1]数量
            '1000倍して、㎏表示からg表示に変更する
            outDt.Rows(i).Item("QT") = CDbl(outDt.Rows(i).Item("QT").ToString) * 1000

        Next

        Return ds

    End Function
    '(2012.08.20)要望管理番号：1353 在庫数量グラム対応 ---  END  ---

#End Region

#Region "チェック"

    ''' <summary>
    ''' 履歴データの存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SelectGetuData(ByVal ds As DataSet) As Boolean

        '荷主コード(大)に値がない場合、スルー
        Dim dr As DataRow = ds.Tables(LMI500BLC.TABLE_NM_IN).Rows(0)
        If String.IsNullOrEmpty(dr.Item("CUST_CD_L").ToString()) = True Then
            Return True
        End If

        '初期在庫 , 直近在庫の場合、スルー
        Dim pirekiDate As String = dr.Item("RIREKI_DATE").ToString()
        Select Case pirekiDate

            Case LMI500BLC.START_DATE, LMI500BLC.END_DATE

                Return True

        End Select

        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '取得できない場合、エラー
        If MyBase.GetResultCount() < 1 Then

            MyBase.SetMessage("E341", New String() {DateFormatUtility.EditSlash(pirekiDate)})
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' 月末在庫履歴データのチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SelectRirekiCount(ByVal ds As DataSet) As Boolean

        '荷主コード(大)に値がない場合、スルー
        Dim dr As DataRow = ds.Tables(LMI500BLC.TABLE_NM_IN).Rows(0)
        If String.IsNullOrEmpty(dr.Item("CUST_CD_L").ToString()) = True Then
            Return True
        End If

        '初期在庫 , 直近在庫の場合、スルー
        Select Case dr.Item("RIREKI_DATE").ToString()

            Case LMI500BLC.START_DATE, LMI500BLC.END_DATE

                Return True

        End Select

        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        dr = ds.Tables(LMI500BLC.TABLE_NM_COUNT).Rows(0)

        Dim rtnResult As Boolean = Me.ChkZaiRirekiData(dr.Item("JITSU_CNT").ToString())
        rtnResult = rtnResult AndAlso Me.CountChk(dr.Item("INKA_CNT").ToString(), "入荷")
        rtnResult = rtnResult AndAlso Me.CountChk(dr.Item("OUTKA_CNT").ToString(), "出荷")
        rtnResult = rtnResult AndAlso Me.CountChk(dr.Item("IDO_CNT").ToString(), "在庫移動")

        Return rtnResult

    End Function

    ''' <summary>
    ''' 在庫履歴データの件数チェック
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkZaiRirekiData(ByVal value As String) As Boolean

        If Convert.ToInt32(value) < 1 Then
            MyBase.SetMessage("E301")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 取得できている場合、エラー
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function CountChk(ByVal value As String, ByVal msg As String) As Boolean

        If 0 < Convert.ToInt32(value) Then
            MyBase.SetMessage("E302", New String() {msg})
            Return False
        End If

        Return True

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

    ''' <summary>
    ''' 取得件数チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkSelectData(ByVal ds As DataSet, ByVal tblNm As String) As DataSet

        If ds.Tables(tblNm).Rows.Count < 1 Then
            MyBase.SetMessage("G001")
        End If

        Return ds

    End Function

#End Region

End Class
