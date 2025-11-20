' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC830  : 日立物流音声データCSV作成
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports System.Text
Imports System.IO

''' <summary>
''' LMC830ハンドラクラス
''' </summary>
''' <remarks></remarks>
''' <histry></histry>
Public Class LMC830H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ' ''' <summary>
    ' ''' H共通クラスを格納するフィールド
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _LMIConH As LMIControlH

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        'START YANAI 音声バッチは1回しか呼ばないようにする
        ''フォームの作成
        'Dim frm As LMC830F = New LMC830F(Me)

        ''キーイベントをフォームで受け取る
        'frm.KeyPreview = True
        'END YANAI 音声バッチは1回しか呼ばないようにする

        ''Hnadler共通クラスの設定
        'Me._LMIConH = New LMIControlH(MyBase.GetPGID())

        'START YANAI 音声バッチは1回しか呼ばないようにする
        ''CSV出力データ検索処理
        'Dim rtnDs As DataSet = Me.SelectCSV(frm, prmDs)

        ''CSV出力データ作成処理
        'Dim rtnFlg As Boolean = Me.MakeCSV(frm, rtnDs, prmDs)
        Dim max As Integer = prmDs.Tables(LMC830C.TABLE_NM_IN).Rows.Count - 1
        Dim rtnDs As DataSet = Nothing
        Dim rtnFlg As Boolean = True

        Dim dt As DataTable = prmDs.Tables(LMC830C.TABLE_NM_IN)
        '別インスタンス
        Dim setDs As DataSet = prmDs.Copy()
        Dim inTbl As DataTable = Nothing

        For i As Integer = 0 To max
            '値のクリア
            inTbl = setDs.Tables(LMC830C.TABLE_NM_IN)
            inTbl.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'CSV出力データ検索処理
            rtnDs = Me.SelectCSV(setDs)

            'CSV出力データ作成処理
            rtnFlg = Me.MakeCSV(rtnDs, setDs)

        Next
        'END YANAI 音声バッチは1回しか呼ばないようにする

        prm.ReturnFlg = rtnFlg

    End Sub

#End Region '初期処理

#Region "ユーティリティ"

    'START YANAI 音声バッチは1回しか呼ばないようにする
    '''' <summary>
    '''' CSV出力データ検索処理
    '''' </summary>
    '''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    '''' <remarks></remarks>
    'Private Function SelectCSV(ByVal frm As LMC830F, ByVal ds As DataSet) As DataSet

    '    'ログ出力
    '    MyBase.Logger.StartLog(MyBase.GetType.Name, String.Concat("SelectCSV■□出荷管理番号：", ds.Tables(LMC830C.TABLE_NM_IN).Rows(0).Item("OUTKA_NO_L").ToString()))

    '    '==========================
    '    'WSAクラス呼出
    '    '==========================
    '    Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(DirectCast(frm, Form), _
    '                                                     "LMC830BLF", _
    '                                                     "SelectCSV", _
    '                                                     ds, _
    '                                                     -1, _
    '                                                     -1)

    '    'ログ出力
    '    MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCSV")

    '    Return rtnDs

    'End Function
    ''' <summary>
    ''' CSV出力データ検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SelectCSV(ByVal ds As DataSet) As DataSet

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, String.Concat("SelectCSV■□出荷管理番号：", ds.Tables(LMC830C.TABLE_NM_IN).Rows(0).Item("OUTKA_NO_L").ToString()))

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC830BLF", "SelectCSV", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCSV")

        Return rtnDs

    End Function
    'END YANAI 音声バッチは1回しか呼ばないようにする

    'START YANAI 音声バッチは1回しか呼ばないようにする
    '''' <summary>
    '''' CSV作成
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <remarks>DACのMakeCSVメソッド呼出</remarks>
    'Private Function MakeCSV(ByVal frm As LMC830F, ByVal ds As DataSet, ByVal inDs As DataSet) As Boolean
    ''' <summary>
    ''' CSV作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks>DACのMakeCSVメソッド呼出</remarks>
    Private Function MakeCSV(ByVal ds As DataSet, ByVal inDs As DataSet) As Boolean
        'END YANAI 音声バッチは1回しか呼ばないようにする

        If ds.Tables(LMC830C.TABLE_NM_OUT_CSV).Rows.Count = 0 AndAlso ds.Tables(LMC830C.TABLE_NM_OUT_CSV_DEL).Rows.Count = 0 Then
            'ログ出力
            MyBase.Logger.EndLog(MyBase.GetType.Name, String.Concat("■□取得件数０件：出荷管理番号→", inDs.Tables(LMC830C.TABLE_NM_IN).Rows(0).Item("OUTKA_NO_L").ToString()))
            Return False
        End If

        Dim dtOut As New DataTable
        Dim rptFlg As Boolean = inDs.Tables(LMC830C.TABLE_NM_IN).Rows(0).Item("RPT_FLG").Equals(LMC830C.OUTKA_SASHIZU)
        Dim strData As String = String.Empty
        Dim kbnDr As DataRow() = Nothing
        Dim cnt As Integer = 0
        Dim renCnt As Integer = 1
        Dim decValue As Decimal = 0

        If rptFlg = True Then
            dtOut = ds.Tables(LMC830C.TABLE_NM_OUT_CSV)
        Else
            dtOut = ds.Tables(LMC830C.TABLE_NM_OUT_CSV_DEL)
        End If

        'CSV出力処理
        Dim max As Integer = dtOut.Rows.Count - 1
        Dim setData As StringBuilder = New StringBuilder()

        For i As Integer = 0 To max
            With dtOut.Rows(i)

                If String.IsNullOrEmpty(.Item("SAGYO_MEI_REC_NO_5").ToString()) = False Then
                    cnt = 5
                ElseIf String.IsNullOrEmpty(.Item("SAGYO_MEI_REC_NO_4").ToString()) = False Then
                    cnt = 4
                ElseIf String.IsNullOrEmpty(.Item("SAGYO_MEI_REC_NO_3").ToString()) = False Then
                    cnt = 3
                ElseIf String.IsNullOrEmpty(.Item("SAGYO_MEI_REC_NO_2").ToString()) = False Then
                    cnt = 2
                ElseIf String.IsNullOrEmpty(.Item("SAGYO_MEI_REC_NO_1").ToString()) = False Then
                    cnt = 1
                Else
                    cnt = 0
                End If

                For j As Integer = 0 To cnt
                    'コンピュ－タ名
                    '(2012.09.15) コンピュータ名は７桁まで --- START ---
                    strData = Mid(.Item("COMPNAME").ToString(), 1, 7)
                    'strData = .Item("COMPNAME").ToString()
                    '(2012.09.15) コンピュータ名は７桁まで --- END ---
                    setData.Append(String.Concat("""", strData, """", ","))

                    '帳票ＩＤ
                    If rptFlg = True Then
                        strData = LMC830C.RPT_ID_OUTKA
                    Else
                        strData = LMC830C.RPT_ID_CANCEL
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '発行ＮＯ
                    strData = .Item("HAKO_NO").ToString()
                    setData.Append(String.Concat("""", strData, """", ","))

                    'ヘッダ棟No
                    If rptFlg = True OrElse j = 0 Then
                        strData = .Item("TOU_HD").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))


                    'ヘッダ作業班
                    If rptFlg = True OrElse j = 0 Then
                        strData = .Item("HAN_HD").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    'レコード番号
                    strData = Me.MaeCoverData(Convert.ToString(renCnt), "0", 5)
                    setData.Append(String.Concat("""", strData, """", ","))

                    '出荷管理番号(大)
                    strData = Mid(.Item("OUTKA_NO_L").ToString(), 3, 7)
                    setData.Append(String.Concat("""", strData, """", ","))

                    '出荷管理番号(中)
                    strData = .Item("OUTKA_NO_M").ToString()
                    setData.Append(String.Concat("""", strData, """", ","))

                    '出荷管理番号(小)
                    If j = 0 Then
                        strData = .Item("OUTKA_NO_S").ToString()
                    Else
                        strData = "999"
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    'EDI出荷管理番号(中)
                    If j = 0 Then
                        strData = .Item("EDI_CTL_NO_CHU").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    'EDIセット親番号
                    If j = 0 Then
                        strData = .Item("EDI_SET_NO").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '営業所コード
                    If j = 0 Then
                        strData = .Item("NRS_BR_CD").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '営業所名
                    If j = 0 Then
                        strData = .Item("NRS_BR_NM").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '倉庫コード
                    If j = 0 Then
                        strData = .Item("WH_CD").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '倉庫名
                    If j = 0 Then
                        strData = .Item("WH_NM").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '出荷予定日
                    If j = 0 Then
                        strData = .Item("OUTKA_PLAN_DATE").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '出荷予定時刻
                    If j = 0 Then
                        strData = .Item("OUTKA_PLAN_TIME").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '出庫日
                    If j = 0 Then
                        strData = .Item("OUTKO_DATE").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '出庫時刻
                    If j = 0 Then
                        strData = .Item("OUTKO_TIME").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '納入予定日
                    If j = 0 Then
                        strData = .Item("ARR_PLAN_DATE").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '納入予定時刻
                    If j = 0 Then
                        strData = .Item("ARR_PLAN_TIME").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '荷主(大)コード
                    If j = 0 Then
                        strData = .Item("CUST_CD_L").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '荷主(中)コード
                    If j = 0 Then
                        strData = .Item("CUST_CD_M").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '荷主名(大)
                    If j = 0 Then
                        strData = .Item("CUST_NM_L").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '荷主名(中)
                    If j = 0 Then
                        strData = .Item("CUST_NM_M").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '荷主住所1
                    If j = 0 Then
                        strData = .Item("AD_1").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '荷主住所2
                    If j = 0 Then
                        strData = .Item("AD_2").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '荷主住所3
                    If j = 0 Then
                        strData = .Item("AD_3").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '荷主住所4
                    If j = 0 Then
                        strData = .Item("AD_4").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '荷主住所5
                    If j = 0 Then
                        strData = .Item("AD_5").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    'TEL
                    If j = 0 Then
                        strData = .Item("TEL").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    'FAX
                    If j = 0 Then
                        strData = .Item("FAX").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '日陸担当者名
                    If j = 0 Then
                        strData = .Item("NRS_BR_PIC_NM").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '売上先名
                    If j = 0 Then
                        strData = .Item("URIG_NM").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '届先コード
                    If j = 0 Then
                        strData = .Item("DEST_CD").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '届先名
                    If j = 0 Then
                        strData = .Item("DEST_NM").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '届先住所1
                    If j = 0 Then
                        strData = .Item("DEST_AD_1").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '届先住所2
                    If j = 0 Then
                        strData = .Item("DEST_AD_2").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '届先住所3
                    If j = 0 Then
                        strData = .Item("DEST_AD_3").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '届先住所4
                    If j = 0 Then
                        strData = .Item("KANA_NM").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '届先住所5
                    If j = 0 Then
                        strData = .Item("DEST_AD_5").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '届先電話番号
                    If j = 0 Then
                        strData = .Item("DEST_TEL").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '棟番号
                    If j = 0 Then
                        strData = .Item("S_TOU_NO").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '室番号
                    If j = 0 Then
                        strData = .Item("S_SITU_NO").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    'ZONEコード
                    If j = 0 Then
                        strData = .Item("S_ZONE_CD").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '集積場所名称
                    If j = 0 Then
                        strData = .Item("S_NM").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '荷主注文番号（明細単位）
                    If j = 0 Then
                        strData = .Item("CUST_ORD_NO_DTL").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '買主注文番号（明細単位）
                    If j = 0 Then
                        strData = .Item("BUYER_ORD_NO_DTL").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '運送会社コード
                    If j = 0 Then
                        strData = .Item("UNSOCO_CD").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '運送会社名
                    If j = 0 Then
                        strData = .Item("UNSOCO_NM").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '元着払区分
                    If j = 0 Then
                        strData = .Item("PC_KB").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '荷主商品コード
                    If j = 0 Then
                        strData = .Item("GOODS_CD_CUST").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '商品名
                    If j = 0 Then
                        strData = .Item("GOODS_NM").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    'ロット番号
                    If j = 0 Then
                        strData = .Item("LOT_NO").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '賞味有効期限
                    If j = 0 Then
                        strData = .Item("LT_DATE").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    'シリアル番号
                    If j = 0 Then
                        strData = .Item("SERIAL_NO").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '出荷梱包個数
                    If rptFlg = True AndAlso j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("SUM_ALCTD_NB").ToString())
                    ElseIf j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("OUTKA_KNP_NB").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '引当単位区分
                    If j = 0 Then
                        strData = .Item("ALCTD_KB").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '出荷包装個数
                    If j = 0 AndAlso String.IsNullOrEmpty(.Item("OUTKA_PKG_NB").ToString()) = False Then
                        decValue = Convert.ToDecimal(.Item("OUTKA_PKG_NB").ToString())
                        strData = Me.IfIsNullOrEmptyReturnZero(Convert.ToString(Convert.ToDouble(decValue)))
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '出荷端数
                    If j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("OUTKA_HASU").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '明細出荷包装個数
                    If rptFlg = True Then
                        If String.IsNullOrEmpty(.Item("OUTKA_PKG_NB_S").ToString()) = False Then
                            decValue = Convert.ToDecimal(.Item("OUTKA_PKG_NB_S").ToString())
                            strData = Me.IfIsNullOrEmptyReturnZero(Convert.ToString(Convert.ToDouble(decValue)))
                        Else
                            strData = "0"
                        End If
                    Else
                        If j = 0 AndAlso String.IsNullOrEmpty(.Item("OUTKA_PKG_NB_S").ToString()) = False Then
                            decValue = Convert.ToDecimal(.Item("OUTKA_PKG_NB_S").ToString())
                            strData = Me.IfIsNullOrEmptyReturnZero(Convert.ToString(Convert.ToDouble(decValue)))
                        Else
                            strData = "0"
                        End If
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '明細出荷端数
                    If j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("OUTKA_HASU_S").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '出荷数量
                    If j = 0 AndAlso String.IsNullOrEmpty(.Item("OUTKA_QT").ToString()) = False Then
                        decValue = Convert.ToDecimal(.Item("OUTKA_QT").ToString())
                        strData = Me.IfIsNullOrEmptyReturnZero(Convert.ToString(Convert.ToDouble(decValue)))
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '出荷総個数
                    If j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("OUTKA_TTL_NB").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '出荷総数量
                    If j = 0 AndAlso String.IsNullOrEmpty(.Item("OUTKA_TTL_QT").ToString()) = False Then
                        decValue = Convert.ToDecimal(.Item("OUTKA_TTL_QT").ToString())
                        strData = Me.IfIsNullOrEmptyReturnZero(Convert.ToString(Convert.ToDouble(decValue)))
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '引当済個数
                    If j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("ALCTD_NB").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '引当済数量
                    If j = 0 AndAlso String.IsNullOrEmpty(.Item("ALCTD_QT").ToString()) = False Then
                        decValue = Convert.ToDecimal(.Item("ALCTD_QT").ToString())
                        strData = Me.IfIsNullOrEmptyReturnZero(Convert.ToString(Convert.ToDouble(decValue)))
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '引当残個数
                    If rptFlg = True AndAlso j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("PORA_ZAI_NB").ToString())
                    ElseIf j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("BACKLOG_NB").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '引当残数量
                    If rptFlg = True AndAlso j = 0 Then
                        If String.IsNullOrEmpty(.Item("PORA_ZAI_QT").ToString()) = False Then
                            decValue = Convert.ToDecimal(.Item("PORA_ZAI_QT").ToString())
                            strData = Me.IfIsNullOrEmptyReturnZero(Convert.ToString(Convert.ToDouble(decValue)))
                        Else
                            strData = "0"
                        End If
                    ElseIf j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("BACKLOG_QT").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '個数単位
                    If j = 0 Then
                        strData = .Item("NB_UT").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '数量単位
                    If j = 0 Then
                        strData = .Item("QT_UT").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '包装個数
                    If j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("PKG_NB").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '包装単位
                    If j = 0 Then
                        strData = .Item("PKG_UT").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '入目
                    If j = 0 AndAlso String.IsNullOrEmpty(.Item("IRIME").ToString()) = False Then
                        decValue = Convert.ToDecimal(.Item("IRIME").ToString())
                        strData = Me.IfIsNullOrEmptyReturnZero(Convert.ToString(Convert.ToDouble(decValue)))
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '入目単位
                    If j = 0 Then
                        strData = .Item("IRIME_UT").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '在庫レコード番号
                    If j = 0 Then
                        strData = .Item("ZAI_REC_NO").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '棟
                    If rptFlg = True Then
                        If j = 0 Then
                            strData = .Item("TOU_NO").ToString()
                        ElseIf .Item("TOU_HD").ToString().Equals("00") = False Then
                            strData = .Item("TOU_HD").ToString()
                        Else
                            strData = ""
                        End If
                    ElseIf j = 0 Then
                        strData = .Item("TOU_NO").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '室
                    If j = 0 Then
                        strData = .Item("SITU_NO").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    'ZONEコード
                    If j = 0 Then
                        strData = .Item("ZONE_CD").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    'ロケーション
                    If j = 0 Then
                        strData = .Item("LOCA").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '作業明細番号
                    If j = 0 Then
                        strData = ""
                    ElseIf String.IsNullOrEmpty(.Item(String.Concat("SAGYO_MEI_REC_NO_", j)).ToString()) = False Then
                        strData = .Item(String.Concat("SAGYO_MEI_REC_NO_", j)).ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '出荷付帯作業コード
                    If rptFlg = True AndAlso j = 0 Then
                        strData = .Item("SAGYO_MEI_CD_1").ToString()
                    ElseIf j = 0 Then
                        strData = ""
                    ElseIf String.IsNullOrEmpty(.Item(String.Concat("SAGYO_MEI_CD_", j)).ToString()) = False Then
                        strData = .Item(String.Concat("SAGYO_MEI_CD_", j)).ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '作業名称
                    If rptFlg = True AndAlso j = 0 Then
                        strData = .Item("SAGYO_MEI_RYAK_NM").ToString()
                    ElseIf j = 0 Then
                        strData = ""
                    ElseIf String.IsNullOrEmpty(.Item(String.Concat("SAGYO_MEI_NM_", j)).ToString()) = False Then
                        strData = .Item(String.Concat("SAGYO_MEI_NM_", j)).ToString()
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '指図書再発行フラグ
                    If rptFlg = True OrElse j = 0 Then
                        strData = .Item("RPT_FLG").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '明細作業班
                    If j = 0 Then
                        strData = .Item("HAN_DTL").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '注意事項
                    If j = 0 Then
                        strData = .Item("REMARK_M").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '荷主注文番号（全体）
                    If j = 0 Then
                        strData = .Item("CUST_ORD_NO").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '買主注文番号（全体）
                    If j = 0 Then
                        strData = .Item("BUYER_ORD_NO").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '注意事項
                    If j = 0 Then
                        strData = .Item("REMARK_L").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '商品状態区分１
                    If rptFlg = True AndAlso j = 0 Then
                        strData = .Item("GOODS_COND_NM_1").ToString()
                    ElseIf j = 0 Then
                        strData = .Item("GOODS_COND_KB_1").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '商品状態区分２
                    If rptFlg = True AndAlso j = 0 Then
                        strData = .Item("GOODS_COND_NM_2").ToString()
                    ElseIf j = 0 Then
                        strData = .Item("GOODS_COND_KB_2").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    'コメント
                    If j = 0 Then
                        strData = .Item("REMARK_ZAI").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '在庫包装残個数
                    If rptFlg = True AndAlso j = 0 Then
                        If String.IsNullOrEmpty(.Item("ZAN_KONSU").ToString()) = False Then
                            decValue = Convert.ToDecimal(.Item("ZAN_KONSU").ToString())
                            strData = Me.IfIsNullOrEmptyReturnZero(Convert.ToString(Convert.ToDouble(decValue)))
                        Else
                            strData = "0"
                        End If
                    ElseIf j = 0 Then
                        If String.IsNullOrEmpty(.Item("PKG_NB_Z").ToString()) = False Then
                            decValue = Convert.ToDecimal(.Item("PKG_NB_Z").ToString())
                            strData = Me.IfIsNullOrEmptyReturnZero(Convert.ToString(Convert.ToDouble(decValue)))
                        Else
                            strData = "0"
                        End If
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '在庫出荷残端数
                    If rptFlg = True AndAlso j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("ZAN_HASU").ToString())
                    ElseIf j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("HASU_Z").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '入庫日
                    If j = 0 Then
                        strData = .Item("INKO_DATE").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '在庫包装残個数
                    If j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("PKG_NB_Z_ALL").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '在庫出荷残端数
                    If j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("HASU_Z_ALL").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '在庫包装残個数
                    If j = 0 AndAlso String.IsNullOrEmpty(.Item("PKG_NB_S").ToString()) = False Then
                        decValue = Convert.ToDecimal(.Item("PKG_NB_S").ToString())
                        strData = Me.IfIsNullOrEmptyReturnZero(Convert.ToString(Convert.ToDouble(decValue)))
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '在庫出荷残端数
                    If j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("HASU_S").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '運送重量
                    If j = 0 AndAlso String.IsNullOrEmpty(.Item("UNSO_WT").ToString()) = False Then
                        decValue = Convert.ToDecimal(.Item("UNSO_WT").ToString())
                        strData = Me.IfIsNullOrEmptyReturnZero(Convert.ToString(Convert.ToDouble(decValue)))
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '備考小（社外）
                    If j = 0 Then
                        strData = .Item("REMARK_OUT").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '運送距離
                    If rptFlg = True AndAlso j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("KYORI").ToString())
                    ElseIf j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("UNSO_KYORI").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '引当可能数量
                    If j = 0 AndAlso String.IsNullOrEmpty(.Item("ALCTD_CAN_QT").ToString()) = False Then
                        decValue = Convert.ToDecimal(.Item("ALCTD_CAN_QT").ToString())
                        strData = Me.IfIsNullOrEmptyReturnZero(Convert.ToString(Convert.ToDouble(decValue)))
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '納品書摘要
                    If j = 0 Then
                        strData = .Item("NHS_REMARK").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '配送時注意事項
                    If j = 0 Then
                        strData = .Item("REMARK_UNSO").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '正味重量
                    If j = 0 AndAlso String.IsNullOrEmpty(.Item("FREE_N01").ToString()) = False Then
                        decValue = Convert.ToDecimal(.Item("FREE_N01").ToString())
                        strData = Me.IfIsNullOrEmptyReturnZero(Convert.ToString(Convert.ToDouble(decValue)))
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '梱包数
                    If j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("FREE_N02").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '風袋重量
                    If j = 0 AndAlso String.IsNullOrEmpty(.Item("FREE_N03").ToString()) = False Then
                        decValue = Convert.ToDecimal(.Item("FREE_N03").ToString())
                        strData = Me.IfIsNullOrEmptyReturnZero(Convert.ToString(Convert.ToDouble(decValue)))
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '纏めﾋﾟｯｷﾝｸﾞﾘｽﾄ発行ﾌﾗｸﾞ
                    If j = 0 Then
                        '1固定に修正 20120924
                        strData = "1"
                        'strData = .Item("FREE_N04").ToString()
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '纏めﾋﾟｯｷﾝｸﾞﾘｽﾄ発行件数
                    If j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("FREE_N05").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '数値06
                    If j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("FREE_N06").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '数値07
                    If j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("FREE_N07").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '数値08
                    If j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("FREE_N08").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '数値09
                    If j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("FREE_N09").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '数値10
                    If j = 0 Then
                        strData = Me.IfIsNullOrEmptyReturnZero(.Item("FREE_N10").ToString())
                    Else
                        strData = "0"
                    End If
                    setData.Append(String.Concat(strData, ","))

                    '商品KEY
                    If j = 0 Then
                        strData = Mid(.Item("FREE_C01").ToString(), 6, 20)
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '売上先コード
                    If j = 0 Then
                        strData = .Item("FREE_C02").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '運送会社支店コード
                    If j = 0 Then
                        strData = .Item("FREE_C03").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '文字列04
                    If j = 0 Then
                        strData = .Item("FREE_C04").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '運送手配区分
                    If j = 0 Then
                        strData = .Item("FREE_C05").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '入庫日管理区分
                    If j = 0 Then
                        strData = .Item("FREE_C06").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '文字列07
                    If j = 0 Then
                        strData = .Item("FREE_C07").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '商品状態荷主
                    If j = 0 Then
                        strData = .Item("FREE_C08").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '荷主名（小）
                    If j = 0 Then
                        strData = .Item("FREE_C09").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '届先JISコード
                    If j = 0 Then
                        strData = .Item("FREE_C10").ToString()
                    Else
                        strData = ""
                    End If
                    setData.Append(String.Concat("""", strData, """", ","))

                    '作成者
                    strData = .Item("CRT_USER").ToString()
                    setData.Append(String.Concat("""", strData, """", ","))

                    'システム日付
                    strData = .Item("SYS_DATE").ToString()
                    setData.Append(String.Concat("""", strData, """", ","))

                    'システム時間
                    strData = String.Concat(Mid(.Item("SYS_TIME").ToString(), 1, 2), ":", _
                                            Mid(.Item("SYS_TIME").ToString(), 3, 2), ":", _
                                            Mid(.Item("SYS_TIME").ToString(), 5, 2))
                    setData.Append(String.Concat("""", strData, """"))

                    setData.Append(vbNewLine)

                    renCnt += 1
                Next
            End With
        Next

        '保存先のCSVファイルのパス
        Dim CSVPath As String = String.Empty

        If rptFlg = True Then
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C013' AND ", _
                                                                                        "KBN_CD = '00'"))

            If kbnDr.Length = 0 Then
                Return False
            End If

            CSVPath = String.Concat(kbnDr(0).Item("KBN_NM2").ToString() _
                                                  , Right(dtOut.Rows(0).Item("OUTKA_NO_L").ToString(), 7) _
                                                  , "_" _
                                                  , Right(dtOut.Rows(0).Item("SYS_DATE").ToString(), 4) _
                                                  , dtOut.Rows(0).Item("SYS_TIME").ToString() _
                                                  , ".csv")
        Else
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C013' AND ", _
                                                                            "KBN_CD = '01'"))

            If kbnDr.Length = 0 Then
                Return False
            End If

            CSVPath = String.Concat(kbnDr(0).Item("KBN_NM2").ToString() _
                                                  , Right(dtOut.Rows(0).Item("OUTKA_NO_L").ToString(), 7) _
                                                  , "_" _
                                                  , Right(dtOut.Rows(0).Item("SYS_DATE").ToString(), 4) _
                                                  , dtOut.Rows(0).Item("SYS_TIME").ToString() _
                                                  , ".csv")
        End If

        'CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'ファイルを開く
        System.IO.Directory.CreateDirectory(kbnDr(0).Item("KBN_NM2").ToString)
        Dim sr As StreamWriter = New StreamWriter(CSVPath, False, enc)

        '値の設定
        sr.Write(setData.ToString())

        'ファイルを閉じる
        sr.Close()
        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, String.Concat("■□CSV作成！：出荷管理番号→", inDs.Tables(LMC830C.TABLE_NM_IN).Rows(0).Item("OUTKA_NO_L").ToString(), "　ファイル名：", CSVPath))


        Return True

    End Function

#End Region

#Region "前埋め設定"

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

        For i As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(value) To keta - 1
            value = String.Concat(value2, value)
        Next

        Return value

    End Function

#End Region

#Region "数値項目空白時０返し"

    ''' <summary>
    ''' 数値項目空白時０返し
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function IfIsNullOrEmptyReturnZero(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            Return "0"
        End If

        Return value

    End Function

#End Region

#End Region 'Method

End Class
