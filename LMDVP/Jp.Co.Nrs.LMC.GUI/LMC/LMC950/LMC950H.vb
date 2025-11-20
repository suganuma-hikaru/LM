' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷管理
'  プログラムID     :  LMC950  : 新潟運輸CSV出力
'  作  成  者       :  []
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports System.Text
Imports System.IO


''' <summary>
''' LMC950ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMC950H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconV As LMCControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconH As LMCControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconG As LMCControlG

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Prm As LMFormData

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

#End Region 'Field

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        Me._Prm = prm

        '画面間データを取得する
        Me._PrmDs = prm.ParamDataSet()

        'CSV出力処理
        Me.MakeCSV(Me._PrmDs)

        '出荷(大)の更新
        Me.UpdateNiigataUnyuCsv(Me._PrmDs)

    End Sub

#End Region '初期処理

#Region "Method"

    ''' <summary>
    ''' CSV作成
    ''' </summary>
    ''' <param name="prmDs">DataSet</param>
    ''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    Private Sub MakeCsv(ByVal prmDs As DataSet)

        Dim max As Integer = prmDs.Tables("LMC950OUT").Rows.Count - 1
        Dim setData As StringBuilder = New StringBuilder()

#Region "見出し文字列の追加"

        setData.Append(SetDblQuotation("お客様管理№"))
        setData.Append(",")
        setData.Append(SetDblQuotation("出荷日"))
        setData.Append(",")
        setData.Append(SetDblQuotation("依頼主コード"))
        setData.Append(",")
        setData.Append(SetDblQuotation("お届け先名称_1"))
        setData.Append(",")
        setData.Append(SetDblQuotation("お届け先名称_2"))
        setData.Append(",")
        setData.Append(SetDblQuotation("お届け先電話番号"))
        setData.Append(",")
        setData.Append(SetDblQuotation("お届け先郵便番号"))
        setData.Append(",")
        setData.Append(SetDblQuotation("お届け先住所_1"))
        setData.Append(",")
        setData.Append(SetDblQuotation("お届け先住所_2"))
        setData.Append(",")
        setData.Append(SetDblQuotation("お届け先住所_3"))
        setData.Append(",")
        setData.Append(SetDblQuotation("配達日"))
        setData.Append(",")
        setData.Append(SetDblQuotation("記事1"))
        setData.Append(",")
        setData.Append(SetDblQuotation("記事2"))
        setData.Append(",")
        setData.Append(SetDblQuotation("記事3"))
        setData.Append(",")
        setData.Append(SetDblQuotation("記事4"))
        setData.Append(",")
        setData.Append(SetDblQuotation("記事5"))
        setData.Append(",")
        setData.Append(SetDblQuotation("記事6"))
        setData.Append(",")
        setData.Append(SetDblQuotation("記事7"))
        setData.Append(",")
        setData.Append(SetDblQuotation("個数"))
        setData.Append(",")
        setData.Append(SetDblQuotation("重量"))
        setData.Append(",")
        setData.Append(SetDblQuotation("重量区分"))
        setData.Append(vbNewLine)

#End Region ' "見出し文字列の追加"

        For i As Integer = 0 To max
            With prmDs.Tables("LMC950OUT").Rows(i)
                setData.Append(SetDblQuotation(.Item("OUTKA_NO_L").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("OUTKA_PLAN_DATE").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NRS_BR_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_NM_1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_NM_2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_TEL").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_ZIP").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_AD_1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_AD_2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_AD_3").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("ARR_PLAN_DATE").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("ATSUKAISYA_NM").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOODS_NM_1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("IRIME").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("ARR_PLAN_TIME").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("UNSO_L_REMARK").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("CUST_ORD_NO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("BUYER_ORD_NO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("OUTKA_PKG_NB").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("UNSO_WT").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation("1"))    ' 重量区分: 固定値
                setData.Append(vbNewLine)
            End With
        Next

        '保存先のCSVファイルのパス
        Dim csvPath As String = String.Concat(prmDs.Tables("LMC950OUT").Rows(0).Item("FILEPATH").ToString, _
                                              prmDs.Tables("LMC950OUT").Rows(0).Item("FILENAME").ToString, _
                                              prmDs.Tables("LMC950OUT").Rows(0).Item("SYS_DATE").ToString, _
                                               "-", _
                                              Left(prmDs.Tables("LMC950OUT").Rows(0).Item("SYS_TIME").ToString, 6), _
                                              ".csv")

        'CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'ファイルを開く
        System.IO.Directory.CreateDirectory(prmDs.Tables("LMC950OUT").Rows(0).Item("FILEPATH").ToString)
        Dim sr As StreamWriter = New StreamWriter(csvPath, False, enc)

        '値の設定
        sr.Write(setData.ToString())

        'ファイルを閉じる
        sr.Close()

    End Sub

    ''' <summary>
    ''' ダブルコーテーション付加
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDblQuotation(ByVal val As String) As String

        Return String.Concat("""", val, """")

    End Function

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function UpdateNiigataUnyuCsv(ByVal ds As DataSet) As DataSet

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC950BLF", "UpdateNiigataUnyuCsv", ds)

        Return rtnDs

    End Function

#End Region 'Method

End Class