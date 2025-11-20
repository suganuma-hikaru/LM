' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH030    : EDI出荷検索
'  EDI荷主ID　　　　:  501　　　 : ビーピー・カストロール（岩槻）
'  作  成  者       :  terakawa
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH030BLC501
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH030DAC501 = New LMH030DAC501()

    ''' <summary>
    ''' 使用するDACクラスの生成(共通DAC)
    ''' </summary>
    ''' <remarks></remarks>
    Private _DacCom As LMH030DAC = New LMH030DAC()

    ''' <summary>
    ''' 使用するBLC共通クラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMH030BLC = New LMH030BLC()

#End Region

#Region "Const"

    'EDI連携フラグ
    Private Const EDI_USED_KBN_USED As String = "01"  '連携する


#If True Then ' BP運送会社自動設定対応 20161117 added by inoue

    ''' <summary>
    ''' テーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TableNames

        ''' <summary>
        ''' 届先,運送重量別運送会社テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUT_UNSO_BY_WGT_AND_DEST As String = LMH030DAC501.TableNames.OUT_UNSO_BY_WGT_AND_DEST


        ''' <summary>
        ''' チャーター管理テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUT_CHARTER_MANAGEMENT As String = LMH030DAC501.TableNames.OUT_CHARTER_MANAGEMENT


        ''' <summary>
        ''' Amazon届先,運送送会社テーブル  ADD 2018/06/14 依頼番号 : 001580
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUT_UNSO_AMAZON_BY_DEST As String = LMH030DAC501.TableNames.OUT_UNSO_AMAZON_BY_DEST
    End Class

    ''' <summary>
    ''' BLCメソッド名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Functions

        ''' <summary>
        ''' 運送会社検索(BP)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SelectUnsoByWgtAndDestList As String = "SelectUnsoByWgtAndDestList"




        Public Const SelectCharterManagementList As String = "SelectCharterManagementList"


    End Class


    ''' <summary>
    ''' LMH030OUT_UNSO_BY_WGT_AND_DESTカラム名
    ''' </summary>
    ''' <remarks></remarks>
    Private Class UnsoByWgtAndDestColumns

        ''' <summary>
        ''' 届先コード
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DEST_CD As String = "DEST_CD"

        ''' <summary>
        ''' 出荷管理番号
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTKA_PLAN_DATE As String = "OUTKA_PLAN_DATE"

        ''' <summary>
        ''' 申し送り区分
        ''' </summary>
        ''' <remarks></remarks>
        Public Const MOSIOKURI_KB As String = "MOSIOKURI_KB"

        ''' <summary>
        ''' 運送会社コード
        ''' </summary>
        ''' <remarks></remarks>
        Public Const UNSOCO_CD As String = "UNSOCO_CD"

        ''' <summary>
        ''' 運送会社支店コード
        ''' </summary>
        ''' <remarks></remarks>
        Public Const UNSOCO_BR_CD As String = "UNSOCO_BR_CD"

        ''' <summary>
        ''' 運送会社コード(運送重量閾値超過用)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OVER_TH_UNSOCO_CD As String = "OVER_TH_UNSOCO_CD"

        ''' <summary>
        ''' 運送会社支店コード(運送重量閾値超過用)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OVER_TH_UNSOCO_BR_CD As String = "OVER_TH_UNSOCO_BR_CD"

        ''' <summary>
        ''' 同一届先、出荷予定日の運送重量総和
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SUM_WEIGHT_KG As String = "SUM_WEIGHT_KG"

        ''' <summary>
        ''' 運送重量閾値(運送会社切替判定用)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const THRESHOLD_WEIGHT_KG As String = "THRESHOLD_WEIGHT_KG"

        ''' <summary>
        ''' チャーター切替用運送重量閾値
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CHARTER_THRESHOLD_WEIGHT_KG As String = "CHARTER_THRESHOLD_WEIGHT_KG"

        ''' <summary>
        ''' チャーター運送会社コード(運送会社コード+ "-" + 運送会社支店コード)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CHARTER_UNSOCO_1 As String = "CHARTER_UNSOCO_1"
        Public Const CHARTER_UNSOCO_2 As String = "CHARTER_UNSOCO_2"
        Public Const CHARTER_UNSOCO_3 As String = "CHARTER_UNSOCO_3"
        Public Const CHARTER_UNSOCO_4 As String = "CHARTER_UNSOCO_4"
        Public Const CHARTER_UNSOCO_5 As String = "CHARTER_UNSOCO_5"

        ''' <summary>
        ''' 取込ファイル名
        ''' </summary>
        ''' <remarks></remarks>
        Public Const FILE_NAME As String = "FILE_NAME"

        ''' <summary>
        ''' 作成日
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CRT_DATE As String = "CRT_DATE"

        ''' <summary>
        ''' 県コード
        ''' </summary>
        ''' <remarks></remarks>
        Public Const KEN_CD As String = "KEN_CD"

        ''' <summary>
        ''' 離島判定
        ''' </summary>
        ''' <remarks></remarks>
        Public Const IS_REMOTE_ISLAND As String = "IS_REMOTE_ISLAND"

        ''' <summary>
        ''' 運送会社コード(離島)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ISLAND_UNSOCO_CD As String = "ISLAND_UNSOCO_CD"

        ''' <summary>
        ''' 運送会社支店コード(離島)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ISLAND_UNSOCO_BR_CD As String = "ISLAND_UNSOCO_BR_CD"

    End Class

    ''' <summary>
    ''' チャーター運送会社カラム名接頭文字
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CHARTER_UNSOCO_PREFIX As String = "CHARTER_UNSOCO_"


    ''' <summary>
    ''' チャーター運送会社値セパレータ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CHARTER_UNSOCO_SEPARATOR As Char = CChar("-")


    ''' <summary>
    ''' チャーター以外の運送会社を指定するチャーター番号
    ''' </summary>
    ''' <remarks></remarks>
    Private Const NOT_CHARTER_UNSO_CD_CHARTER_NO As Integer = 0

    ''' <summary>
    ''' LMH030_OUTKAEDI_Lテーブルカラム名
    ''' </summary>
    ''' <remarks></remarks>
    Private Class OutkaEdiLColumns

        ''' <summary>
        ''' 届先コード
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DEST_CD As String = "DEST_CD"

        ''' <summary>
        ''' 出荷予定日
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTKA_PLAN_DATE As String = "OUTKA_PLAN_DATE"

        ''' <summary>
        ''' 運送会社コード
        ''' </summary>
        ''' <remarks></remarks>
        Public Const UNSO_CD As String = "UNSO_CD"

        ''' <summary>
        ''' 運送会社支店コード
        ''' </summary>
        ''' <remarks></remarks>
        Public Const UNSO_BR_CD As String = "UNSO_BR_CD"

        ''' <summary>
        ''' FREE_C05
        ''' </summary>
        ''' <remarks></remarks>
        Public Const FREE_C05 As String = "FREE_C05"

        ''' <summary>
        ''' ファイル名
        ''' </summary>
        ''' <remarks></remarks>
        Public Const FREE_C02 As String = "FREE_C02"


        ''' <summary>
        ''' 作成日
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CRT_DATE As String = "CRT_DATE"

    End Class


    ''' <summary>
    ''' LMH030OUT_CHARTER_MANAGEMENTテーブルカラム名
    ''' </summary>
    ''' <remarks></remarks>
    Private Class CharterManagementColumns

        ''' <summary>
        ''' 営業所コード
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NRS_BR_CD As String = "NRS_BR_CD"

        ''' <summary>
        ''' 荷主コードL
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CUST_CD_L As String = "CUST_CD_L"

        ''' <summary>
        ''' 荷主コードM
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CUST_CD_M As String = "CUST_CD_M"

        ''' <summary>
        ''' 取込ファイル名
        ''' </summary>
        ''' <remarks></remarks>
        Public Const FILE_NAME As String = "FILE_NAME"

        ''' <summary>
        ''' 作成日
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CRT_DATE As String = "CRT_DATE"

        ''' <summary>
        ''' 出荷予定日
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTKA_PLAN_DATE As String = "OUTKA_PLAN_DATE"

        ''' <summary>
        ''' 県コード
        ''' </summary>
        ''' <remarks></remarks>
        Public Const KEN_CD As String = "KEN_CD"

        ''' <summary>
        ''' 届先コード
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DEST_CD As String = "DEST_CD"

        ''' <summary>
        ''' チャーター便番号
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CHARTER_NO As String = "CHARTER_NO"



    End Class


    ''' <summary>
    ''' 申送区分
    ''' </summary>
    ''' <remarks></remarks>
    Class MOSIOKURI_KB
        Public Const POMP_UP As String = "JP"
    End Class


    ''' <summary>
    ''' チャーター便最大
    ''' </summary>
    ''' <remarks></remarks>
    Private Const MAX_CHARTER_NUMBER As Integer = 5


    ''' <summary>
    ''' チャーター便の最大数の超過判定
    ''' </summary>
    ''' <remarks></remarks>
    Private _IsOverMaxCharterNumber As Boolean = False


#End If


#End Region

#Region "Method"

#Region "出荷登録処理"
    ''' <summary>
    ''' 出荷登録処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OutkaToroku(ByVal ds As DataSet) As DataSet

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()

        'TODO:テストのためログを入れる。START
        Dim strdate As Date = Now
        Dim strtime As Long = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "SelectEdiL", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        'EDI出荷(大)の値取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiL", ds)

        Dim enddate As Date = Now
        Dim endtime As Long = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "SelectEdiL", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "SelectEdiL", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'TODO:テストのためログを入れる。END

        If ds.Tables("LMH030_OUTKAEDI_L").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "SetEdiLShoki", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)


        'EDI出荷(大)の初期値設定
        ds = Me.SetEdiLShoki(ds)


        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "SetEdiLShoki", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "SetEdiLShoki", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'TODO:テストのためログを入れる。END

        'TODO:テストのためログを入れる。START
        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "SelectEdiM", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)
        'EDI出荷(中)の値取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiM", ds)

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "SelectEdiM", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "SelectEdiM", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'TODO:テストのためログを入れる。END

        If ds.Tables("LMH030_OUTKAEDI_M").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'TODO:テストのためログを入れる。START
        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "EdiLKanrenCheck", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        'EDI出荷(大)の初期値設定後の関連チェック
        If Me.EdiLKanrenCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "EdiLKanrenCheck", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "EdiLKanrenCheck", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'TODO:テストのためログを入れる。END

        'TODO:テストのためログを入れる。START
        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "EdiLDbExistsCheck", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        'EDI出荷(大)の初期値設定後のDB存在チェック
        If Me.EdiLDbExistsCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "EdiLDbExistsCheck", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "EdiLDbExistsCheck", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'TODO:テストのためログを入れる。END

        '届先コードの初期値設定
        ds = Me.SetDestCd(ds)

        'TODO:テストのためログを入れる。START
        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "EdiMMasterExistsCheck", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        'EDI出荷(中)の初期値設定後のマスタ存在チェック
        If Me.EdiMMasterExistsCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "EdiMMasterExistsCheck", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "EdiMMasterExistsCheck", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'TODO:テストのためログを入れる。END

        'TODO:テストのためログを入れる。START
        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "EdiMKanrenCheck", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        'EDI出荷(中)の初期値設定後の関連チェック
        If Me.EdiMKanrenCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "EdiMKanrenCheck", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "EdiMKanrenCheck", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'TODO:テストのためログを入れる。END

        'TODO:テストのためログを入れる。START
        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "SetDataset", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        Dim autoMatomeF As String = ds.Tables("LMH030INOUT").Rows(0)("AUTO_MATOME_FLG").ToString()
        Dim matomeNo As String = String.Empty
        Dim matomeFlg As Boolean = False
        Dim UnsoMatomeFlg As Boolean = False


        'ビーピー・カストロールはまとめ対象荷主ではない(共通のまとめSQLを使用)
        '自動まとめフラグ = "0" or "1"の場合、まとめ処理
        If autoMatomeF.Equals("0") OrElse autoMatomeF.Equals("1") Then

            'まとめ先取得
            ds = MyBase.CallDAC(Me._DacCom, "SelectMatomeTarget", ds)

            If MyBase.GetResultCount = 0 Then
                'まとめ先が無い場合、通常登録
                matomeFlg = False

            ElseIf MyBase.GetResultCount > 1 Then
                choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.BP_WID_L013, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then
                    'まとめ対象だったデータを出したい場合はコメントをはずす
                    'Dim matomeTargetNo As String = Me.matomesakiOutkaNo(ds)
                    msgArray(1) = "出荷管理番号(大)"
                    msgArray(2) = "出荷"
                    msgArray(3) = "注意)進捗区分が同一の場合は、管理番号が若い方にまとまります。"
                    msgArray(4) = String.Empty
                    matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                    ds = Me._Blc.SetComWarningL("W199", LMH030BLC.BP_WID_L013, ds, msgArray, matomeNo, String.Empty)
                    Return ds

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    '自動まとめ処理を行う
                    matomeFlg = True
                End If
                ''まとめ先が複数件の場合、エラー
                'matomeNo = Me.matomesakiOutkaNo(ds)
                'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E427", New String() {matomeNo}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                'Return ds
            ElseIf autoMatomeF.Equals("0") = True Then

                choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.BP_WID_L005, 0)

                If String.IsNullOrEmpty(choiceKb) = True Then
                    msgArray(1) = "出荷管理番号(大)"
                    msgArray(2) = String.Empty
                    msgArray(3) = String.Empty
                    msgArray(4) = String.Empty
                    matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                    ds = Me._Blc.SetComWarningL("W168", LMH030BLC.BP_WID_L005, ds, msgArray, matomeNo, String.Empty)
                    Return ds

                ElseIf choiceKb.Equals("01") = True Then
                    'ワーニングで"はい"を選択時
                    '自動まとめ処理を行う
                    matomeFlg = True

                ElseIf choiceKb.Equals("03") = True Then
                    'ワーニングで"キャンセル"を選択時
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return ds

                End If

            ElseIf autoMatomeF.Equals("1") = True Then
                Dim dtMatome As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")

                Dim matomeStatus As String = dtMatome.Rows(0).Item("OUTKA_STATE_KB").ToString()

                If matomeStatus.Equals("10") = False Then

                    choiceKb = Me.SetDestWarningChoiceKb(ds.Tables("LMH030_OUTKAEDI_L"), ds, LMH030BLC.BP_WID_L014, 0)

                    '進捗区分が予定入力より先になっているのでワーニングを出力
                    If String.IsNullOrEmpty(choiceKb) = True Then
                        msgArray(1) = "出荷管理番号(大)"
                        msgArray(2) = "出荷"
                        msgArray(3) = String.Empty
                        msgArray(4) = String.Empty
                        matomeNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0).Item("OUTKA_CTL_NO").ToString()
                        ds = Me._Blc.SetComWarningL("W198", LMH030BLC.BP_WID_L014, ds, msgArray, matomeNo, String.Empty)
                        Return ds

                    ElseIf choiceKb.Equals("01") = True Then
                        'ワーニングで"はい"を選択時
                        '自動まとめ処理を行う
                        matomeFlg = True

                    End If

                Else
                    'まとめ処理を行う
                    matomeFlg = True
                End If

            End If
        End If

        '出荷管理番号(大)の採番
        ds = Me.GetOutkaNoL(ds, matomeFlg)

        ''出荷管理番号(中)の採番
        ds = Me.GetOutkaNoM(ds, matomeFlg)

        '紐付け処理の場合は、別Funcでデータセット設定+更新処理
        Dim eventShubetsu As String = ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()
        If eventShubetsu.Equals("3") Then
            '紐付処理をして終了
            ds = Me.Himoduke(ds)
            Return ds

        End If

        '出荷(大)データセット設定処理
        ds = Me.SetDatasetOutkaL(ds, matomeFlg)

        '出荷(中)データセット設定
        ds = Me.SetDatasetOutkaM(ds)

        'EDI受信テーブル(HED)データセット設定
        ds = Me.SetDatasetEdiRcvHed(ds)

        'EDI受信テーブル(DTL)データセット設定
        ds = Me.SetDatasetEdiRcvDtl(ds)

        '作業レコードデータセット設定
        ds = Me.SetDatasetSagyo(ds)

        '運送(大,中)データセット設定
        ds = Me.SetDatasetUnsoL(ds, matomeFlg)
        ds = Me.SetDatasetUnsoM(ds)

        '運送(大)の運送重量をデータセットに再設定
        ds = Me.SetdatasetUnsoJyuryo(ds, matomeFlg)

        'タブレット項目の初期値設定
        ds = MyBase.CallBLC(Me._Blc, "SetDatasetOutnkaLTabletData", ds)

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "SetDataset", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "SetDataset", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'TODO:テストのためログを入れる。END

        'TODO:テストのためログを入れる。START
        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateOutkaEdiLData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        '出荷登録(通常処理)
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateOutkaEdiLData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateOutkaEdiLData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'TODO:テストのためログを入れる。END

        'TODO:テストのためログを入れる。START
        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateOutkaEdiMData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateOutkaEdiMData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateOutkaEdiMData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'TODO:テストのためログを入れる。END

        'TODO:テストのためログを入れる。START
        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateEdiRcvHedData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        ''2013.03.05 要望番号1922 BP速度改善対応START

        ''EDI受信(HED)の更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)

        'enddate = Now
        'endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        'MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateEdiRcvHedData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        'MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateEdiRcvHedData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        ''TODO:テストのためログを入れる。END

        ''TODO:テストのためログを入れる。START
        'strdate = Now
        'strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        'MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateEdiRcvDtlData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        ''EDI受信(DTL)の更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        'enddate = Now
        'endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        'MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateEdiRcvDtlData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        'MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateEdiRcvDtlData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        ''TODO:テストのためログを入れる。END

        ''2013.03.05 要望番号1922 BP速度改善対応END

        'TODO:テストのためログを入れる。START
        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertOutkaLData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        If matomeFlg = False Then
            '出荷(大)の新規登録
            ds = MyBase.CallDAC(Me._Dac, "InsertOutkaLData", ds)
        Else
            '出荷(大)のまとめ更新
            ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaLData", ds)
        End If

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertOutkaLData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertOutkaLData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'TODO:テストのためログを入れる。END

        'TODO:テストのためログを入れる。START
        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertOutkaMData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        '出荷(中)の新規登録
        ds = MyBase.CallDAC(Me._Dac, "InsertOutkaMData", ds)

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertOutkaMData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertOutkaMData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'TODO:テストのためログを入れる。END

        '届先マスタの自動追加
        If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
               AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_INSERT_FLG").Equals("1") = True Then
            ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "1"

            'TODO:テストのためログを入れる。START
            strdate = Now
            strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
            MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertMDestData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

            ds = MyBase.CallDAC(Me._Dac, "InsertMDestData", ds)

            enddate = Now
            endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
            MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertMDestData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
            MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertMDestData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
            'TODO:テストのためログを入れる。END

            ds.Tables("LMH030_M_DEST").Rows(0).Item("INSERT_TARGET_FLG") = "0"
        End If

        '届先マスタの更新
        If ds.Tables("LMH030_M_DEST").Rows.Count <> 0 _
           AndAlso ds.Tables("LMH030_M_DEST").Rows(0).Item("MST_UPDATE_FLG").Equals("1") = True Then
            ds.Tables("LMH030_M_DEST").Rows(0).Item("UPDATE_TARGET_FLG") = "1"

            'TODO:テストのためログを入れる。START
            strdate = Now
            strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
            MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateMDestData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

            ds = MyBase.CallDAC(Me._Dac, "UpdateMDestData", ds)

            enddate = Now
            endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
            MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateMDestData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
            MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateMDestData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
            'TODO:テストのためログを入れる。END

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
            ds.Tables("LMH030_M_DEST").Rows(0).Item("UPDATE_TARGET_FLG") = "0"
        End If

        '作業の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_E_SAGYO").Rows.Count <> 0 Then

            'TODO:テストのためログを入れる。START
            strdate = Now
            strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
            MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertSagyoData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

            ds = MyBase.CallDAC(Me._Dac, "InsertSagyoData", ds)

            enddate = Now
            endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
            MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertSagyoData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
            MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertSagyoData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
            'TODO:テストのためログを入れる。END

        End If

        '運送(大)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_UNSO_L").Rows.Count <> 0 Then
            If matomeFlg = False Then

                'TODO:テストのためログを入れる。START
                strdate = Now
                strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
                MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertUnsoLData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

                ds = MyBase.CallDAC(Me._Dac, "InsertUnsoLData", ds)

                enddate = Now
                endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
                MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertUnsoLData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
                MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertUnsoLData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
                'TODO:テストのためログを入れる。END

            Else
                ds = MyBase.CallDAC(Me._Dac, "UpdateMatomesakiUnsoLData", ds)
            End If
        End If

        '運送(中)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH030_UNSO_M").Rows.Count <> 0 Then

            'TODO:テストのためログを入れる。START
            strdate = Now
            strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
            MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertUnsoMData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoMData", ds)

            enddate = Now
            endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
            MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertUnsoMData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
            MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertUnsoMData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
            'TODO:テストのためログを入れる。END

        End If

        If matomeFlg = True Then
            'まとめ先EDI出荷(大)の更新(まとめ先EDIデータにまとめ番号を設定)
            ds = MyBase.CallDAC(Me._Dac, "UpdateMatomesakiEdiLData", ds)
            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
        End If

        'ビーピー・カストロール対応 terakawa 2012.12.12 Start
        'ビーピー・カストロールの場合、出荷完了後に実績作成を行う。
        ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU") = LMH030DAC.EventShubetsu.CREATEJISSEKI.GetHashCode
        ds = JissekiSakusei(ds)
        'ビーピー・カストロール対応 terakawa 2012.12.12 End

        Return ds

    End Function


#If True Then ' BP運送会社自動設定対応 20161116 added by inoue


    ''' <summary>
    ''' 運送重量別運送会社一覧取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectUnsoByWgtAndDestList(ByVal ds As DataSet) As DataSet
        Return MyBase.CallDAC(Me._Dac, LMH030DAC501.Functions.SelectUnsoByWgtAndDestList, ds)
    End Function

    ''' <summary>
    ''' チャーター管理一覧取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectCharterManagementList(ByVal ds As DataSet) As DataSet
        Return MyBase.CallDAC(Me._Dac, LMH030DAC501.Functions.SelectCharterManagementList, ds)
    End Function

#End If



#End Region

#Region "実績作成処理"
    ''' <summary>
    ''' 実績作成処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSakusei(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH030INOUT").Rows(0).Item("EDI_CTL_NO").ToString()

        'TODO:テストのためログを入れる。START
        Dim strdate As Date = Now
        Dim strtime As Long = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "SelectSend", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        '送信データ作成用情報取得
        ds = MyBase.CallDAC(Me._Dac, "SelectSend", ds)

        Dim enddate As Date = Now
        Dim endtime As Long = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "SelectSend", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "SelectSend", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'TODO:テストのためログを入れる。END

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
            Exit Function
        End If

        'TODO:テストのためログを入れる。START
        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertSend", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        '送信データの更新
        ds = MyBase.CallDAC(Me._Dac, "InsertSend", ds)

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertSend", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "InsertSend", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'TODO:テストのためログを入れる。END

        'TODO:テストのためログを入れる。START
        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateEdiRcvHedData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        '受信HEDの更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateEdiRcvHedData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateEdiRcvHedData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'TODO:テストのためログを入れる。END

        'TODO:テストのためログを入れる。START
        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateEdiRcvDtlData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        '受信明細の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateEdiRcvDtlData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateEdiRcvDtlData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'TODO:テストのためログを入れる。END

        'TODO:テストのためログを入れる。START
        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateOutkaEdiMData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateOutkaEdiMData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateOutkaEdiMData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'TODO:テストのためログを入れる。END

        'TODO:テストのためログを入れる。START
        strdate = Now
        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateOutkaEdiLData", "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        'If MyBase.GetResultCount = 0 Then
        '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E011", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
        '    Return ds
        'End If

        enddate = Now
        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateOutkaEdiLData", "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
        MyBase.Logger.WriteLog(0, "LMH030BLC501", "UpdateOutkaEdiLData", "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")
        'TODO:テストのためログを入れる。END

        'ビーピーカストロール対応 terakawa 2012.12.19 Start
        'BPは出荷作成時に実景作成するため、出荷大のステータスは更新しない
        '出荷(大)の更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaLData", ds)
        'ビーピーカストロール対応 terakawa 2012.12.19 Start

        Return ds

    End Function

#End Region

#Region "EDI_Lの初期値設定(出荷登録処理)"
    ''' <summary>
    ''' EDI_Lの初期設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>各マスタ値を取得しEDI_Lの初期設定をする</remarks>
    Private Function SetEdiLShoki(ByVal ds As DataSet) As DataSet

        '荷主M取得
        ds = MyBase.CallDAC(Me._DacCom, "SelectMcustOutkaToroku", ds)

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim mCustDr As DataRow = ds.Tables("LMH030_M_CUST").Rows(0)
        Dim mDestDr As DataRow = Nothing
        Dim mDestFlgYN As Boolean = False      '届先マスタ有無フラグ

        ' チャーター便数の超過識別
        _IsOverMaxCharterNumber = False

        '届先M取得
        If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = False OrElse _
            String.IsNullOrEmpty(ediDr("EDI_DEST_CD").ToString().Trim()) = False Then
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMdest", ds)
        End If

        If ds.Tables("LMH030_M_DEST").Rows.Count > 0 Then
            mDestDr = ds.Tables("LMH030_M_DEST").Rows(0)
            mDestFlgYN = True
        End If

        '出荷区分
        If String.IsNullOrEmpty(ediDr("OUTKA_KB").ToString().Trim()) = True Then
            ediDr("OUTKA_KB") = "10"
        End If

        '出荷種別区分
        If String.IsNullOrEmpty(ediDr("SYUBETU_KB").ToString().Trim()) = True Then
            ediDr("SYUBETU_KB") = "10"
        End If

        '出荷先国内・輸出
        If String.IsNullOrEmpty(ediDr("NAIGAI_KB").ToString().Trim()) = True Then
            ediDr("NAIGAI_KB") = "01"
        End If

        '作業進捗区分
        If String.IsNullOrEmpty(ediDr("OUTKA_STATE_KB").ToString().Trim()) = True Then
            ediDr("OUTKA_STATE_KB") = "10"
        End If

        '出荷報告有無
        If String.IsNullOrEmpty(ediDr("OUTKAHOKOKU_YN").ToString().Trim()) = True Then
            If String.IsNullOrEmpty(mCustDr("OUTKA_RPT_YN").ToString().Trim()) = False Then
                ediDr("OUTKAHOKOKU_YN") = Right(mCustDr("OUTKA_RPT_YN").ToString().Trim(), 1)
            Else
                ediDr("OUTKAHOKOKU_YN") = "0"
            End If
        End If

        'ピッキングリスト区分
        If String.IsNullOrEmpty(ediDr("PICK_KB").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                ediDr("PICK_KB") = mDestDr("PICK_KB").ToString().Trim()
            Else
                ediDr("PICK_KB") = "01"
            End If
        End If

        '出庫日
        If String.IsNullOrEmpty(ediDr("OUTKO_DATE").ToString().Trim()) = True Then
            ediDr("OUTKO_DATE") = ediDr("OUTKA_PLAN_DATE")
        End If

        '当期保管料負担有無
        If String.IsNullOrEmpty(ediDr("TOUKI_HOKAN_YN").ToString().Trim()) = True Then
            ediDr("TOUKI_HOKAN_YN") = "1"
        End If

        '荷主名(大)
        If String.IsNullOrEmpty(ediDr("CUST_NM_L").ToString().Trim()) = True Then
            ediDr("CUST_NM_L") = ds.Tables("LMH030_M_CUST").Rows(0).Item("CUST_NM_L").ToString()
        End If

        '荷主名(中)
        If String.IsNullOrEmpty(ediDr("CUST_NM_M").ToString().Trim()) = True Then
            ediDr("CUST_NM_M") = ds.Tables("LMH030_M_CUST").Rows(0).Item("CUST_NM_M").ToString()
        End If

        '荷送人名(大)
        If String.IsNullOrEmpty(ediDr("SHIP_CD_L").ToString().Trim()) = True Then
            ediDr("SHIP_NM_L") = ""
        Else
            'DACで値セットを行う
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMdestShip", ds)
            If ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows.Count <> 0 Then
                ediDr("SHIP_NM_L") = ds.Tables("LMH030_M_DEST_SHIP_CD_L").Rows(0).Item("DEST_NM").ToString().Trim()
            End If
        End If

        '指定納品書区分
        If String.IsNullOrEmpty(ediDr("SP_NHS_KB").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                ediDr("SP_NHS_KB") = mDestDr("SP_NHS_KB").ToString().Trim()
            End If
        End If

        '分析票添付区分
        If String.IsNullOrEmpty(ediDr("COA_YN").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                ediDr("COA_YN") = mDestDr("COA_YN").ToString().Trim().Substring(1, 1)
                '要望番号:483((出荷登録時)EDI出荷(大,中)の更新不具合の"COA_YN") 2012/06/21 本明 Start
            Else
                '届先マスタに存在しない場合、自動追加の値と同値をセットする
                ediDr("COA_YN") = "0"  'SetInsMDestFromDestの値と一致させる事！（荷主により値が異なるため）
                '要望番号:483((出荷登録時)EDI出荷(大,中)の更新不具合の"COA_YN") 2012/06/21 本明 End
            End If
        End If

#If False Then ' BP運送会社自動設定対応 20161115 changed by inoue
        '運送手配区分
        If String.IsNullOrEmpty(ediDr("UNSO_MOTO_KB").ToString().Trim()) = True Then
            ediDr("UNSO_MOTO_KB") = mCustDr("UNSO_TEHAI_KB").ToString().Trim()
        End If
#Else
        ' 運送手配区分(EDIバッチで日陸手配'01'が固定値で設定されているため上書)
        ediDr("UNSO_MOTO_KB") = mCustDr("UNSO_TEHAI_KB").ToString().Trim()
#End If
        '便区分
        If String.IsNullOrEmpty(ediDr("BIN_KB").ToString().Trim()) = True Then
            If mDestFlgYN = True Then
                If String.IsNullOrEmpty(mDestDr("BIN_KB").ToString().Trim()) = False Then
                    ediDr("BIN_KB") = mDestDr("BIN_KB")
                Else
                    ediDr("BIN_KB") = "01"
                End If
            Else
                ediDr("BIN_KB") = "01"
            End If
        End If

#If False Then
        '運送会社コード
        '運送会社支店コード
        '空の場合は届先マスタの値を設定、届先Mが空の場合は荷主マスタの値を設定
        If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString().Trim()) = True AndAlso _
           String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString().Trim()) = True Then

            If mDestFlgYN = True Then
                If String.IsNullOrEmpty(mDestDr("SP_UNSO_CD").ToString().Trim()) = False Then
                    ediDr("UNSO_CD") = mDestDr("SP_UNSO_CD").ToString().Trim()
                    ediDr("UNSO_BR_CD") = mDestDr("SP_UNSO_BR_CD").ToString().Trim()
                Else
                    ediDr("UNSO_CD") = mCustDr("SP_UNSO_CD").ToString().Trim()
                    ediDr("UNSO_BR_CD") = mCustDr("SP_UNSO_BR_CD").ToString().Trim()
                End If
            Else
                ediDr("UNSO_CD") = mCustDr("SP_UNSO_CD").ToString().Trim()
                ediDr("UNSO_BR_CD") = mCustDr("SP_UNSO_BR_CD").ToString().Trim()
            End If

        End If
#Else

        'Amazon便対応運送会社コード取得 ADD 2018/06/14
        '送信データ作成用情報取得
        ds = MyBase.CallDAC(Me._Dac, "SelectUnsoAmazonDestList", ds)

        Dim amazonCnt As Integer = ds.Tables("LMH030OUT_UNSO_AMAZON_BY_DEST").Rows.Count

        If amazonCnt > 0 Then
            'Amazon便対応
            ediDr(OutkaEdiLColumns.UNSO_CD) = ds.Tables("LMH030OUT_UNSO_AMAZON_BY_DEST").Rows(0).Item("UNSO_CD")
            ediDr(OutkaEdiLColumns.UNSO_BR_CD) = ds.Tables("LMH030OUT_UNSO_AMAZON_BY_DEST").Rows(0).Item("UNSO_BR_CD")

        Else
            '変更前処理 2018/06/14

            '運送会社コード
            Dim rows As IEnumerable(Of DataRow) = ds.Tables(TableNames.OUT_UNSO_BY_WGT_AND_DEST).Select()

            'チャーター管理一覧
            Dim charterMgmtList As IEnumerable(Of DataRow) = ds.Tables(TableNames.OUT_CHARTER_MANAGEMENT).Select()


            Dim mosiokuriKb As String = ""
            If (MOSIOKURI_KB.POMP_UP.Equals(ediDr(OutkaEdiLColumns.FREE_C05))) Then
                mosiokuriKb = MOSIOKURI_KB.POMP_UP
            End If


            '出荷予定日と届先によって、運送会社を取得する
            Dim dataRow As DataRow = _
                rows.Where(Function(s) s.Item(UnsoByWgtAndDestColumns.FILE_NAME).Equals(ediDr(OutkaEdiLColumns.FREE_C02)) AndAlso _
                                       s.Item(UnsoByWgtAndDestColumns.CRT_DATE).Equals(ediDr(OutkaEdiLColumns.CRT_DATE)) AndAlso _
                                       s.Item(UnsoByWgtAndDestColumns.DEST_CD).Equals(ediDr(OutkaEdiLColumns.DEST_CD)) AndAlso _
                                       s.Item(UnsoByWgtAndDestColumns.OUTKA_PLAN_DATE).Equals(ediDr(OutkaEdiLColumns.OUTKA_PLAN_DATE)) AndAlso _
                                       s.Item(UnsoByWgtAndDestColumns.MOSIOKURI_KB).Equals(mosiokuriKb)).FirstOrDefault

            If (dataRow IsNot Nothing) Then

                ' 運送会社を切り替える条件を取得
                Dim thWeight As Double = Double.Parse(dataRow.Item(UnsoByWgtAndDestColumns.THRESHOLD_WEIGHT_KG).ToString())
                Dim sumWeight As Double = Double.Parse(dataRow.Item(UnsoByWgtAndDestColumns.SUM_WEIGHT_KG).ToString())
                Dim charterWeight As Double = Double.Parse(dataRow.Item(UnsoByWgtAndDestColumns.CHARTER_THRESHOLD_WEIGHT_KG).ToString())
                Dim isRemoteIsland As String = dataRow.Item(UnsoByWgtAndDestColumns.IS_REMOTE_ISLAND).ToString()

                If (LMConst.FLG.ON.Equals(isRemoteIsland) AndAlso _
                    Len(dataRow(UnsoByWgtAndDestColumns.ISLAND_UNSOCO_CD)) > 0 AndAlso _
                    Len(dataRow(UnsoByWgtAndDestColumns.ISLAND_UNSOCO_BR_CD)) > 0) Then

                    ' 離島の場合
                    ediDr(OutkaEdiLColumns.UNSO_CD) = dataRow(UnsoByWgtAndDestColumns.ISLAND_UNSOCO_CD)
                    ediDr(OutkaEdiLColumns.UNSO_BR_CD) = dataRow(UnsoByWgtAndDestColumns.ISLAND_UNSOCO_BR_CD)

                ElseIf (charterWeight > 0 AndAlso _
                    sumWeight >= charterWeight) Then


                    Dim isNewCharter As Boolean = True

                    ' チャーターの便番号(1～MAX_CHARTER_NUMBER)
                    Dim selectCharterNumber As Integer = 1

                    ' 他の伝票で設定されているチャーター便に、対象となる便が存在するか検索
                    Dim selectedCharter As DataRow = _
                          charterMgmtList.Where(Function(s) s.Item(CharterManagementColumns.FILE_NAME).Equals(ediDr(OutkaEdiLColumns.FREE_C02)) AndAlso _
                                                            s.Item(CharterManagementColumns.CRT_DATE).Equals(ediDr(OutkaEdiLColumns.CRT_DATE)) AndAlso _
                                                            s.Item(CharterManagementColumns.DEST_CD).Equals(ediDr(OutkaEdiLColumns.DEST_CD)) AndAlso _
                                                            s.Item(CharterManagementColumns.OUTKA_PLAN_DATE).Equals(ediDr(OutkaEdiLColumns.OUTKA_PLAN_DATE))).FirstOrDefault

                    '他伝票に設定されたチャータの存在確認(上記の検索で見つからない or 見つかったがCHARTER_NOが0)
                    isNewCharter = (selectedCharter Is Nothing OrElse _
                                    (selectedCharter IsNot Nothing AndAlso _
                                     NOT_CHARTER_UNSO_CD_CHARTER_NO.Equals(selectedCharter.Item(CharterManagementColumns.CHARTER_NO))))


                    If (isNewCharter) Then

                        ' 他の伝票で設定されているチャーター便とは、別の便が必要となったので、同一エリア(県)の最後のチャーター便を検索する
                        ' (※チャーター便は、異なるエリア間で同一の運送会社コード+支店コードを設定しないこと)
                        Dim charterNoByArea As DataRow = _
                            charterMgmtList.Where(Function(s) s.Item(CharterManagementColumns.KEN_CD).Equals(dataRow.Item(UnsoByWgtAndDestColumns.KEN_CD)) AndAlso _
                                                              s.Item(CharterManagementColumns.OUTKA_PLAN_DATE).Equals(dataRow.Item(UnsoByWgtAndDestColumns.OUTKA_PLAN_DATE))) _
                                    .OrderByDescending(Function(s) s.Item(CharterManagementColumns.CHARTER_NO)).FirstOrDefault


                        If (charterNoByArea IsNot Nothing) Then

                            ' 同一エリア内にチャーターが存在したので、その便の一つ後の番号をチャーター番号として設定
                            selectCharterNumber = Convert.ToInt32(charterNoByArea.Item(CharterManagementColumns.CHARTER_NO)) + 1
                            If (selectCharterNumber > MAX_CHARTER_NUMBER) Then

                                ' チャーター便が最大数を超えた場合、運送会社を設定できないためエラーとする。
                                _IsOverMaxCharterNumber = True

                                ediDr(OutkaEdiLColumns.UNSO_CD) = ""
                                ediDr(OutkaEdiLColumns.UNSO_BR_CD) = ""

                                Return ds

                            End If
                        Else
                            ' 同一エリア内で一度もチャーター便が選択されていない場合は、1便となる。

                        End If

                    Else

                        ' 他の伝票と同じチャーター便が利用できるので、チャーター番号をそのまま利用する
                        selectCharterNumber = Convert.ToInt32(selectedCharter.Item(CharterManagementColumns.CHARTER_NO))
                        isNewCharter = False

                    End If

                    If (isNewCharter) Then

                        ' 新規にチャーター便を追加した場合、管理リストへ追加する
                        Dim newRow As DataRow = ds.Tables(LMH030BLC501.TableNames.OUT_CHARTER_MANAGEMENT).NewRow

                        newRow.Item(CharterManagementColumns.KEN_CD) = dataRow.Item(UnsoByWgtAndDestColumns.KEN_CD)
                        newRow.Item(CharterManagementColumns.CRT_DATE) = dataRow.Item(UnsoByWgtAndDestColumns.CRT_DATE)
                        newRow.Item(CharterManagementColumns.FILE_NAME) = dataRow.Item(UnsoByWgtAndDestColumns.FILE_NAME)
                        newRow.Item(CharterManagementColumns.OUTKA_PLAN_DATE) = dataRow.Item(UnsoByWgtAndDestColumns.OUTKA_PLAN_DATE)
                        newRow.Item(CharterManagementColumns.DEST_CD) = dataRow.Item(UnsoByWgtAndDestColumns.DEST_CD)
                        newRow.Item(CharterManagementColumns.CHARTER_NO) = selectCharterNumber

                        ds.Tables(LMH030BLC501.TableNames.OUT_CHARTER_MANAGEMENT).Rows.Add(newRow)

                    End If

                    ' 運送会社
                    Dim charterColumn As String = String.Concat(CHARTER_UNSOCO_PREFIX, selectCharterNumber)

                    Dim unsoco As String = dataRow(charterColumn).ToString()

                    ediDr(OutkaEdiLColumns.UNSO_CD) = unsoco.Split(CHARTER_UNSOCO_SEPARATOR)(0)
                    ediDr(OutkaEdiLColumns.UNSO_BR_CD) = unsoco.Split(CHARTER_UNSOCO_SEPARATOR)(1)

                ElseIf (thWeight > 0 AndAlso _
                        sumWeight >= thWeight) Then

                    ' 閾値以上の場合、超過用の運送会社を選択
                    ediDr(OutkaEdiLColumns.UNSO_CD) = dataRow(UnsoByWgtAndDestColumns.OVER_TH_UNSOCO_CD)
                    ediDr(OutkaEdiLColumns.UNSO_BR_CD) = dataRow(UnsoByWgtAndDestColumns.OVER_TH_UNSOCO_BR_CD)
                Else

                    ' 閾値未満の場合
                    ediDr(OutkaEdiLColumns.UNSO_CD) = dataRow(UnsoByWgtAndDestColumns.UNSOCO_CD)
                    ediDr(OutkaEdiLColumns.UNSO_BR_CD) = dataRow(UnsoByWgtAndDestColumns.UNSOCO_BR_CD)
                End If
            End If

        End If
#End If

        'タリフ分類区分
        '運賃タリフコード
        '割増タリフコード
        ''DACで値セットを行う
        'ds = MyBase.CallDAC(Me._DacCom, "SetTariffData", ds)

        'タリフ分類区分
        '運賃タリフコード(運賃タリフマスタ,横持ちヘッダー)
        '割増タリフコード(割増運賃タリフマスタ)
        'DACで値セットを行う
        '(三井化学：EDIの時点で値が入っててもタリフMに存在しないケースがある為の対応)
        '①荷主明細マスタの存在チェック(荷主明細マスタに存在していれば入替えOK)
        '荷主明細マスタの取得
        ds = MyBase.CallDAC(Me._DacCom, "SelectListDataMcustDetails", ds)
        'タリフセットマスタの取得(運賃タリフ)
        ds = MyBase.CallDAC(Me._DacCom, "SetTariffData", ds)

        'タリフセットマスタの取得(割増タリフ)
        ds = MyBase.CallDAC(Me._DacCom, "SetExtcTariffData", ds)


        '配送時注意事項
        If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = True Then
        Else
            If mDestFlgYN = True Then
                If String.IsNullOrEmpty(mDestDr("DELI_ATT").ToString().Trim()) = False Then

                    If String.IsNullOrEmpty(ediDr("UNSO_ATT").ToString().Trim()) = True Then

                        ediDr("UNSO_ATT") = mDestDr("DELI_ATT").ToString().Trim()
                    ElseIf InStr(ediDr("UNSO_ATT").ToString().Trim(), mDestDr("DELI_ATT").ToString().Trim()) > 0 Then
                    Else
                        ediDr("UNSO_ATT") = Me._Blc.LeftB(String.Concat(ediDr("UNSO_ATT").ToString() & Strings.Space(2), mDestDr("DELI_ATT").ToString().Trim()), 100)
                    End If
                End If

            End If

        End If

        '送り状作成有無
        If String.IsNullOrEmpty(ediDr("DENP_YN").ToString().Trim()) = True Then
            If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString().Trim()) = False AndAlso _
                String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString().Trim()) = False Then
                '要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 Start
                ''運送会社M取得
                'ds = MyBase.CallDAC(Me._DacCom, "SelectDataUnsoco", ds)
                '運送会社荷主別送り状マスタの存在チェック
                ds = MyBase.CallDAC(Me._DacCom, "SelectDataUnsoCustRpt", ds)
                '要望番号:1152(DENP_YN値を運送会社荷主別送り状マスタの存在有無で判断する) 2012/06/21 本明 Start
                If MyBase.GetResultCount = 0 Then
                    ediDr("DENP_YN") = "0"
                Else
                    ediDr("DENP_YN") = "1"
                End If
            Else
                ediDr("DENP_YN") = "0"
            End If

        End If

        '元着払区分
        If String.IsNullOrEmpty(ediDr("PC_KB").ToString().Trim()) = True Then
            ediDr("PC_KB") = "01"
        End If

        '運賃請求有無
        If (ediDr("UNSO_MOTO_KB").ToString()).Equals("10") = True OrElse _
           (ediDr("UNSO_MOTO_KB").ToString()).Equals("40") = True Then
            ediDr("UNCHIN_YN") = "1"
        Else
            ediDr("UNCHIN_YN") = "0"
        End If

        '荷役料有無
        If String.IsNullOrEmpty(ediDr("NIYAKU_YN").ToString().Trim()) = True Then
            ediDr("NIYAKU_YN") = "1"
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 届先コード設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>EDIデータの届先コードが空の場合、届先マスタの値を設定する
    ''' この設定はDB存在チェック後に行う</remarks>
    Private Function SetDestCd(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        '届先コード
        If String.IsNullOrEmpty(ediDr("DEST_CD").ToString().Trim()) = True Then
            If ds.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                ediDr("DEST_CD") = ds.Tables("LMH030_M_DEST").Rows(0)("DEST_CD").ToString().Trim() '2012.04.09 要望番号943 修正
            End If
        End If

        Return ds

    End Function

#End Region

#Region "EDI出荷(中)の初期値設定"

    ''' <summary>
    ''' EDI出荷(中)の初期値設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function EdiMDefaultSet(ByVal ds As DataSet, ByVal setDs As DataSet, _
                                    ByVal count As Integer, ByVal unsodata As String, _
                                    ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim ediLDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(count)
        Dim mGoodsDr As DataRow = setDs.Tables("LMH030_M_GOODS").Rows(0)

        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)



        ''-------------------------------------------------------------------------------------
        ''●荷主固有チェック
        ''-------------------------------------------------------------------------------------

        Dim flgWarning As Boolean = False
        Dim compareWarningFlg As Boolean = False
        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        '商品名
        Dim mGoodsNm As String = Replace(mGoodsDr("GOODS_NM_1").ToString(), "ｰ", "-").Trim()
        Dim ediGoodsNm As String = Replace(ediMDr("GOODS_NM").ToString(), "ｰ", "-").Trim()
        Dim mDestGoodsNm As String = String.Empty
        If String.IsNullOrEmpty(ediMDr("DEST_GOODS_NM").ToString()) = False Then
            mDestGoodsNm = Replace(ediMDr("DEST_GOODS_NM").ToString(), "ｰ", "-").Trim()
        End If

        If mGoodsNm.Equals(ediGoodsNm) = True Then
            'チェックなし
            ediMDr("GOODS_NM") = mGoodsDr("GOODS_NM_1").ToString()

        ElseIf String.IsNullOrEmpty(mGoodsNm) = False AndAlso String.IsNullOrEmpty(mDestGoodsNm) = False AndAlso _
               mDestGoodsNm.Equals(ediGoodsNm) = False Then
            ''2013.02.13 BPの場合ワーニングは廃止 START
            'choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.BP_WID_M001, count)

            'If String.IsNullOrEmpty(choiceKb) = True Then
            '    msgArray(1) = "商品名"
            '    msgArray(2) = "届先商品マスタ"
            '    msgArray(3) = "商品名"
            '    msgArray(4) = "注意) 受信した品名は「出荷時注意事項」に記載します。"
            '    msgArray(5) = String.Empty
            '    ds = Me._Blc.SetComWarningM("W194", LMH030BLC.BP_WID_M001, ds, setDs, msgArray, _
            'ediMDr("DEST_GOODS_NM").ToString(), mGoodsDr("GOODS_NM_1").ToString())
            '    compareWarningFlg = True

            'ElseIf choiceKb.Equals("01") = True Then
            ''2013.02.13 BPの場合ワーニングは廃止 END
            'ワーニングで"はい"を選択時
            If String.IsNullOrEmpty(ediMDr("REMARK").ToString()) = True Then
                ediMDr("REMARK") = String.Concat("受信品名＝", ediMDr("GOODS_NM"))
            Else
                ediMDr("REMARK") = _Blc.LeftB(String.Concat(ediMDr("REMARK"), Space(2), "受信品名＝", ediMDr("GOODS_NM")), 100)
            End If
            '全角変換したままの場合、桁あふれを起こす事がある
            ediMDr("GOODS_NM") = mGoodsDr("GOODS_NM_1").ToString()

            'End If

        ElseIf mGoodsNm.Equals(ediGoodsNm) = False Then

            ' ''2013.02.13 BPの場合ワーニングは廃止 START

            'choiceKb = Me.SetGoodsWarningChoiceKb(dtEdi, ds, LMH030BLC.UKIMA_WID_M002, count)

            'If String.IsNullOrEmpty(choiceKb) = True Then
            '    msgArray(1) = "商品名"
            '    msgArray(2) = "商品マスタ"
            '    msgArray(3) = "商品名"
            '    msgArray(4) = String.Empty
            '    msgArray(5) = String.Empty
            '    ds = Me._Blc.SetComWarningM("W194", LMH030BLC.UKIMA_WID_M002, ds, setDs, msgArray, _
            '                ediMDr("GOODS_NM").ToString(), mGoodsDr("GOODS_NM_1").ToString())
            '    compareWarningFlg = True

            'ElseIf choiceKb.Equals("01") = True Then

            ''2013.02.13 BPの場合ワーニングは廃止 END

            'ワーニングで"はい"を選択時
            '全角変換したままの場合、桁あふれを起こす事がある
            ediMDr("GOODS_NM") = mGoodsDr("GOODS_NM_1").ToString()

        End If

        'End If

        '入目単位
        If String.IsNullOrEmpty(ediMDr("IRIME_UT").ToString()) = True Then
            ediMDr("IRIME_UT") = mGoodsDr("STD_IRIME_UT")
        Else
            If unsodata.Equals("01") = False AndAlso ediMDr("IRIME_UT").Equals(mGoodsDr("STD_IRIME_UT")) = False Then
                '運送データ以外でEDI(中)と商品マスタで入目単位が異なる場合、エラー(サクラ以外)
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"入目単位", "商品マスタ", "入目単位"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        'サーチキー
        ediMDr("FREE_C21") = mGoodsDr("SEARCH_KEY_1").ToString()

        '分析表区分
        If String.IsNullOrEmpty(ediMDr("COA_YN").ToString()) = True Then
            ediMDr("COA_YN") = (mGoodsDr("COA_YN").ToString()).Substring(1, 1)
        End If

        '荷主注文番号(明細単位)
        If String.IsNullOrEmpty(ediMDr("CUST_ORD_NO_DTL").ToString()) = True Then
            ediMDr("CUST_ORD_NO_DTL") = ediLDr("CUST_ORD_NO")
        End If

        '買主注文番号(明細単位)
        If String.IsNullOrEmpty(ediMDr("BUYER_ORD_NO_DTL").ToString()) = True Then
            ediMDr("BUYER_ORD_NO_DTL") = ediLDr("BUYER_ORD_NO")
        End If

        '商品KEY
        If unsodata.Equals("01") = False Then
            ediMDr("NRS_GOODS_CD") = mGoodsDr("GOODS_CD_NRS")
        End If

        '引当単位区分
        If String.IsNullOrEmpty(ediMDr("ALCTD_KB").ToString()) = True Then
            If String.IsNullOrEmpty(mGoodsDr("ALCTD_KB").ToString()) = False Then

                ediMDr("ALCTD_KB") = mGoodsDr("ALCTD_KB")
            Else
                ediMDr("ALCTD_KB") = "01"
            End If
        End If

        '個数単位
        ediMDr("KB_UT") = mGoodsDr("NB_UT")

        '数量単位
        ediMDr("QT_UT") = mGoodsDr("STD_IRIME_UT")

        '包装個数
        ediMDr("PKG_NB") = mGoodsDr("PKG_NB")

        '包装単位
        ediMDr("PKG_UT") = mGoodsDr("PKG_UT")

        '温度区分
        ediMDr("ONDO_KB") = mGoodsDr("ONDO_KB")

        '運送温度区分
        If String.IsNullOrEmpty(ediMDr("UNSO_ONDO_KB").ToString()) = True Then

            If (mGoodsDr("ONDO_UNSO_STR_DATE").ToString()) < (mGoodsDr("ONDO_UNSO_END_DATE").ToString()) _
            AndAlso ((ediLDr("OUTKA_PLAN_DATE").ToString().Substring(4, 4)) < (mGoodsDr("ONDO_UNSO_STR_DATE").ToString()) _
            OrElse (mGoodsDr("ONDO_UNSO_END_DATE").ToString()) < (ediLDr("OUTKA_PLAN_DATE").ToString().Substring(4, 4))) Then
                ediMDr("UNSO_ONDO_KB") = "90"
            Else
                ediMDr("UNSO_ONDO_KB") = mGoodsDr("UNSO_ONDO_KB")
            End If

            If (mGoodsDr("ONDO_UNSO_END_DATE").ToString()) < (mGoodsDr("ONDO_UNSO_STR_DATE").ToString()) _
            AndAlso ((ediLDr("OUTKA_PLAN_DATE").ToString().Substring(4, 4)) < (mGoodsDr("ONDO_UNSO_END_DATE").ToString()) _
            OrElse (mGoodsDr("ONDO_UNSO_STR_DATE").ToString()) < (ediLDr("OUTKA_PLAN_DATE").ToString().Substring(4, 4))) Then
                ediMDr("UNSO_ONDO_KB") = "90"
            Else
                ediMDr("UNSO_ONDO_KB") = mGoodsDr("UNSO_ONDO_KB")
            End If
        Else
            '運送温度区分(区分マスタ)
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U006
            drJudge("KBN_CD") = ediMDr("UNSO_ONDO_KB")
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運送温度区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If

        End If


        If Convert.ToDecimal(ediMDr("IRIME")) = 0 _
        AndAlso Convert.ToDecimal(mGoodsDr("STD_IRIME_NB")) <> 0 Then
            ediMDr("IRIME") = mGoodsDr("STD_IRIME_NB")
        End If

        '出荷包装個数
        '出荷端数
        Dim pkgNb As Double = Convert.ToDouble(ediMDr("PKG_NB"))
        Dim outkaPkgNb As Double = Convert.ToDouble(ediMDr("OUTKA_PKG_NB"))
        Dim outkaHasu As Double = Convert.ToDouble(ediMDr("OUTKA_HASU"))
        Dim alctdKb As String = ediMDr("ALCTD_KB").ToString
        Dim irime As Double = Convert.ToDouble(ediMDr("IRIME"))
        Dim outkaTtlQt As Double = Convert.ToDouble(ediMDr("OUTKA_TTL_QT"))

        Select Case alctdKb

            Case "01"
                If 1 < pkgNb Then

                    ediMDr("OUTKA_PKG_NB") = Math.Floor((pkgNb * outkaPkgNb + outkaHasu) / pkgNb)
                    ediMDr("OUTKA_HASU") = (pkgNb * outkaPkgNb + outkaHasu) Mod pkgNb
                Else
                    ediMDr("OUTKA_PKG_NB") = pkgNb * outkaPkgNb + outkaHasu
                    ediMDr("OUTKA_HASU") = 0
                End If

                ediMDr("OUTKA_TTL_QT") = (pkgNb * outkaPkgNb + outkaHasu) * irime

            Case "02"
                ediMDr("OUTKA_PKG_NB") = 0
                If outkaTtlQt Mod irime = 0 Then
                    ediMDr("OUTKA_HASU") = outkaTtlQt / irime
                Else
                    ediMDr("OUTKA_HASU") = Math.Floor(outkaTtlQt / irime) + 1
                End If

                ediMDr("OUTKA_TTL_NB") = ediMDr("OUTKA_HASU")

            Case "03"
                ediMDr("OUTKA_PKG_NB") = 0
                ediMDr("OUTKA_HASU") = 0
                ediMDr("OUTKA_TTL_NB") = 0

            Case Else

        End Select

        '出荷数量
        ediMDr("OUTKA_QT") = ediMDr("OUTKA_TTL_QT")

        '個別重量(KGS)
        If unsodata.Equals("01") = False Then
            ediMDr("BETU_WT") = mGoodsDr("STD_WT_KGS")
        End If

        '出荷時加工作業区分1-5
        ediMDr("OUTKA_KAKO_SAGYO_KB_1") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_1")
        ediMDr("OUTKA_KAKO_SAGYO_KB_2") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_2")
        ediMDr("OUTKA_KAKO_SAGYO_KB_3") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_3")
        ediMDr("OUTKA_KAKO_SAGYO_KB_4") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_4")
        ediMDr("OUTKA_KAKO_SAGYO_KB_5") = mGoodsDr("OUTKA_KAKO_SAGYO_KB_5")

        'ワーニングが存在する場合はここでの判定はFalseで返す
        '(ビーピー・カストロールはワーニング設定有)
        If compareWarningFlg = True Then
            Return False
        End If

        Return True

    End Function

#End Region

#Region "入力チェック(出荷登録処理)"

#Region "EDI出荷(大)のBLC側でのチェック"

    ''' <summary>
    ''' EDI出荷(大)のBLC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiLKanrenCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_L")

        '-------------------------------------------------------------------------------------
        '●荷主共通チェック
        '-------------------------------------------------------------------------------------

        '出荷管理番号
        If Me._Blc.OutkaCtlNoCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E349", New String() {"EDIデータ", "出荷管理番号"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷報告有無
        If Me._Blc.OutkaHokokuYnCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"出荷報告有無"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷予定日
        If Me._Blc.OutkaPlanDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出荷予定日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出庫日
        If Me._Blc.OutkoDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出庫日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷予定日+出庫日
        If Me._Blc.OutkaPlanLargeSmallCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E166", New String() {"出荷予定日", "出庫日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '納入予定日
        If Me._Blc.arrPlanDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"納入予定日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷報告日
        If Me._Blc.HokokuDateCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E047", New String() {"出荷報告日"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '荷主コード(大)
        If Me._Blc.CustCdLCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(大)"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '荷主コード(中)
        If Me._Blc.CustCdMCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(中)"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '送り状作成有無
        If Me._Blc.DenpYnCheck(dt) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"送り状作成有無"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

#If True Then ' BP運送会社自動設定対応 20161207 added by inoue
        If (_IsOverMaxCharterNumber) Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E933", New String() {MAX_CHARTER_NUMBER.ToString()}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If
#End If

        Return True

    End Function

#End Region

#Region "EDI出荷(大)のDAC側でのチェック"

    ''' <summary>
    ''' EDI出荷(大)のDAC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiLDbExistsCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim drJudge As DataRow = ds.Tables("LMH030_JUDGE").Rows(0)
        Dim drEdiL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim drIn As DataRow = ds.Tables("LMH030INOUT").Rows(0)

        '-------------------------------------------------------------------------------------
        '●荷主共通チェック
        '-------------------------------------------------------------------------------------
        ''オーダー番号重複チェック
        If String.IsNullOrEmpty(drEdiL.Item("CUST_ORD_NO").ToString) = False Then

            If drIn("ORDER_CHECK_FLG").Equals("1") = True Then
                Call MyBase.CallDAC(Me._DacCom, "SelectOrderCheckData", ds)
                If MyBase.GetResultCount > 0 Then
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E377", New String() {"出荷データ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return False
                End If

            End If

        End If

        '出荷区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S014
        drJudge("KBN_CD") = drEdiL("OUTKA_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"出荷区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷種別区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S020
        drJudge("KBN_CD") = drEdiL("SYUBETU_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"出荷種別区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '作業進捗区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S010
        drJudge("KBN_CD") = drEdiL("OUTKA_STATE_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"作業進捗区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        'ピッキングリスト区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_P001
        drJudge("KBN_CD") = drEdiL("PICK_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"ピッキングリスト区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '倉庫コード(倉庫マスタ)
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataSoko", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"倉庫コード", "倉庫マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '納入予定時刻(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N010
        drJudge("KBN_CD") = drEdiL("ARR_PLAN_TIME")

        If String.IsNullOrEmpty(drJudge("KBN_CD").ToString()) = True Then

        Else
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"納入予定時刻", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        '荷主コード(荷主マスタ)
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataMcust", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"荷主コード", "荷主マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '運送元区分(区分マスタ) 注)値は運送手配区分を使用
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U005
        drJudge("KBN_CD") = drEdiL("UNSO_MOTO_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運送手配区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '運送手配区分(区分マスタ) 注)値はタリフ分類区分を使用
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_T015
        drJudge("KBN_CD") = drEdiL("UNSO_TEHAI_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"タリフ分類区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '車輌区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S012
        drJudge("KBN_CD") = drEdiL("SYARYO_KB")
        If String.IsNullOrEmpty(drJudge("KBN_CD").ToString()) = True Then

        Else

            ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"車輌区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If

        End If

        '便区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U001
        drJudge("KBN_CD") = drEdiL("BIN_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"便区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '運送会社コード
        If String.IsNullOrEmpty(drEdiL("UNSO_CD").ToString()) = False OrElse String.IsNullOrEmpty(drEdiL("UNSO_BR_CD").ToString()) = False Then

            If String.IsNullOrEmpty(drEdiL("UNSO_CD").ToString()) = True Then
                drEdiL("UNSO_CD") = String.Empty
            End If

            If String.IsNullOrEmpty(drEdiL("UNSO_BR_CD").ToString()) = True Then
                drEdiL("UNSO_BR_CD") = String.Empty
            End If

            Call MyBase.CallDAC(Me._DacCom, "SelectDataUnsoco", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運送会社コード", "運送会社マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        '運賃タリフコード(運賃タリフマスタ,横持ちヘッダー)
        Dim unchinTariffCd As String = String.Empty
        unchinTariffCd = drEdiL("UNCHIN_TARIFF_CD").ToString()
        Dim unsoTehaiKb As String = String.Empty
        unsoTehaiKb = drEdiL("UNSO_TEHAI_KB").ToString()

        If String.IsNullOrEmpty(unchinTariffCd) = True Then

        Else

            If unsoTehaiKb.Equals("40") = True Then
                ds = MyBase.CallDAC(Me._DacCom, "SelectDataMyokoTariffHd", ds)
            Else
                ds = MyBase.CallDAC(Me._DacCom, "SelectDataMunchinTariff", ds)
            End If

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"運賃タリフコード", "運賃タリフマスタまたは横持ちヘッダー"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If

        End If

        '割増運賃タリフコード(割増運賃タリフマスタ)
        Dim extcTariffCd As String = String.Empty
        extcTariffCd = drEdiL("EXTC_TARIFF_CD").ToString()
        If String.IsNullOrEmpty(extcTariffCd) = True Then

        Else
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMextcUnchin", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"割増運賃タリフコード", "割増運賃タリフマスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        '元着払い区分(区分マスタ)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_M001
        drJudge("KBN_CD") = drEdiL("PC_KB")
        ds = MyBase.CallDAC(Me._DacCom, "SelectDataZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"元着払い区分", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '-------------------------------------------------------------------------------------
        '●荷主固有チェック
        '-------------------------------------------------------------------------------------
        Dim flgWarning As Boolean = False

        '届先マスタ存在チェック
        Dim destCd As String = drEdiL("DEST_CD").ToString()         '届先コード
        Dim ediDestCd As String = drEdiL("EDI_DEST_CD").ToString()  'EDI届先コード
        Dim workDestCd As String = String.Empty                     '検索する届先コード格納変数
        Dim workDestString As String = String.Empty                 '"届先コード"or"EDI届先コード"
        Dim dtMS As DataTable = ds.Tables("LMH030_M_DEST_SHIP_CD_L")


        'DEST_CDが空の場合、EDI_DEST_CDを使う
        If String.IsNullOrEmpty(destCd) = False Then
            workDestCd = destCd
            workDestString = "届先コード"
        ElseIf String.IsNullOrEmpty(ediDestCd) = False Then
            workDestCd = ediDestCd
            workDestString = "EDI届先コード"
        Else
        End If

        Dim mDestCount As Integer = ds.Tables("LMH030_M_DEST").Rows.Count

        If mDestCount = 1 Then
            '1件に特定できた場合、マスタ値とEDI出荷(大)の整合性チェックとZIPコードのマスタ存在チェック
            If Me.DestCompareCheck(ds, rowNo, ediCtlNo) = False Then

                Dim chkRowNo As Integer = Convert.ToInt32(rowNo)
                If MyBase.IsMessageStoreExist(chkRowNo) = True Then
                    '整合性チェックでエラーがあった場合は処理終了
                    Return False
                Else
                    '整合性チェックでワーニングがあった場合は、flgWarning=True
                    flgWarning = True
                End If
            End If

        ElseIf mDestCount = 0 Then
            '0件の場合、ZIPコードのマスタ存在チェックを行い、届先マスタの更新をする
            'JISマスタに存在しない場合、エラー
            'JISマスタに存在するが、JISが空の場合、ワーニング
            If Me.ZipCompareCheck(ds, rowNo, ediCtlNo, workDestCd, workDestString) = False Then
                Dim chkRowNo As Integer = Convert.ToInt32(rowNo)
                If MyBase.IsMessageStoreExist(chkRowNo) = True Then
                    'チェックでエラーがあった場合は処理終了
                    Return False
                Else
                    'ワーニング⇒マスタ追加(ワーニング設定した場合はflgWarning=True)
                    flgWarning = True
                End If
            End If

        Else
            '複数件の場合、エラー
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E330", New String() {"EDI届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        If flgWarning = True Then
            'ワーニングフラグが立っている場合False
            Return False
        End If


        Return True

    End Function

#End Region

#Region "EDI出荷(中)のBLC側でのチェック"

    ''' <summary>
    ''' EDI出荷(中)のBLC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiMKanrenCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")

        '引当単位区分
        If Me._Blc.AlctdKbCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"引当単位区分"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '温度区分 + 便区分
        If Me._Blc.OndoBinKbCheck(dtL, dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E352", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '出荷端数
        If Me._Blc.OutkaHasuCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '入目 + 出荷総数量
        If Me._Blc.IrimeSosuryoLargeSmallCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E334", New String() {"入目と出荷総数量"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '赤黒区分
        If Me._Blc.AkakuroKbCheck(dtM) = False Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "出荷登録"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        Return True

    End Function

#End Region

#Region "EDI出荷(中)のDAC側でのチェック + 初期値設定"

    ''' <summary>
    ''' EDI出荷(中)のDAC側でのチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiMMasterExistsCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim dtL As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim max As Integer = dtM.Rows.Count - 1
        Dim unsoData As String = String.Empty
        Dim custGoodsCd As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtL As DataTable = setDs.Tables("LMH030_OUTKAEDI_L")
        Dim setDtM As DataTable = setDs.Tables("LMH030_OUTKAEDI_M")
        Dim dtGooDs As DataTable = setDs.Tables("LMH030_M_GOODS")

        Dim flgWarning As Boolean = False

        For i As Integer = 0 To max

            custGoodsCd = dtM.Rows(i)("CUST_GOODS_CD").ToString()

            If String.IsNullOrEmpty(custGoodsCd) = False Then

                '値のクリア
                setDs.Clear()

                '条件の設定
                setDtL.ImportRow(dtL.Rows(0))
                setDtM.ImportRow(dtM.Rows(i))

                choiceKb = Me.SetGoodsWarningChoiceKb(setDtM, ds, LMH030BLC.BP_WID_M001, 0)

                If choiceKb.Equals("03") = True Then
                    'ワーニングで"キャンセル"を選択時
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "G042", , rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                End If

                '商品マスタ検索（NRS商品コード or 荷主商品コード）
                setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsOutka", setDs))

                If MyBase.GetResultCount = 0 Then
                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                    'MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"商品コード", "商品マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    Dim sErrMsg As String = Me._Blc.GetErrMsgE493(setDs)
                    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End
                    Return False
                ElseIf GetResultCount() > 1 Then

                    '入目 + 荷主商品コードで再検索
                    setDs = (MyBase.CallDAC(Me._DacCom, "SelectDataMgoodsIrimeOutka", setDs))

                    If MyBase.GetResultCount = 1 Then
                    Else
                        '再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                        '注意!!! セットメッセージは消してよいのか判断がつかないので調査する
                        'MyBase.SetMessage("W162")
                        msgArray(1) = String.Empty
                        msgArray(2) = String.Empty
                        msgArray(3) = String.Empty
                        msgArray(4) = String.Empty

                        ds = Me._Blc.SetComWarningM("W162", LMH030BLC.BP_WID_M001, ds, setDs, msgArray, custGoodsCd, String.Empty)

                        flgWarning = True 'ワーニングフラグをたてて処理続行

                        Continue For
                    End If

                End If

                'EDI出荷(中)の初期値設定処理
                If Me.EdiMDefaultSet(ds, setDs, i, unsoData, rowNo, ediCtlNo) = False Then

                    'ビーピー・カストロールは現段階ではワーニングが存在するが、エラーはなし。共通のロジックを組み込む為入れておく
                    Dim chkRowNo As Integer = Convert.ToInt32(rowNo)
                    If MyBase.IsMessageStoreExist(chkRowNo) = True Then
                        '整合性チェックでエラーがあった場合は処理終了
                        Return False
                    Else
                        '整合性チェックでワーニングがあった場合は、flgWarning=True
                        flgWarning = True
                    End If

                End If

                '運送重量取得用項目をデータセット(EDI出荷(中))に格納
                If Me.SetDatasetEdiMUnsoJyuryo(ds, setDs, i, rowNo, ediCtlNo) = False Then
                    Return False
                End If

            Else
                MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E019", New String() {"商品コード"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If

        Next

        '----------------------------------------------------------------------------------------------------------
        'ワーニングがある場合はマスタから商品が選択できていない為、処理をつづけるとデータによってはアベンドする。
        'そのため中データのループが終わり、ワーニングがある（flgWarning=True）場合は処理を終了させる
        '-----------------------------------------------------------------------------------------------------------
        If flgWarning = True Then
            'ワーニングフラグが立っている場合False
            Return False
        End If

        Return True

    End Function

#End Region

#Region "届先マスタチェック"
    ''' <summary>
    ''' マスタ値とEDI出荷(大)の整合性チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DestCompareCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtMdest As DataTable = ds.Tables("LMH030_M_DEST")
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtMZip As DataTable = ds.Tables("LMH030_M_ZIP")

        Dim mSysDelF As String = dtMdest.Rows(0).Item("SYS_DEL_FLG").ToString()
        Dim mDestNm As String = dtMdest.Rows(0).Item("DEST_NM").ToString()
        Dim mAd1 As String = dtMdest.Rows(0).Item("AD_1").ToString()
        Dim mAd2 As String = dtMdest.Rows(0).Item("AD_2").ToString()
        Dim mAd3 As String = dtMdest.Rows(0).Item("AD_3").ToString()
        Dim mZip As String = dtMdest.Rows(0).Item("ZIP").ToString()
        Dim mTel As String = dtMdest.Rows(0).Item("TEL").ToString()
        Dim mJis As String = dtMdest.Rows(0).Item("JIS").ToString()
        Dim mAdAll As String = String.Concat(mAd1, mAd2, mAd3)
        Dim mZipJis As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim ediDestCd As String = dtEdi.Rows(0)("DEST_CD").ToString()
        Dim ediDestNm As String = dtEdi.Rows(0)("DEST_NM").ToString()
        Dim ediZip As String = dtEdi.Rows(0)("DEST_ZIP").ToString()
        Dim ediTel As String = dtEdi.Rows(0)("DEST_TEL").ToString()
        Dim ediFreeC21 As String = dtEdi.Rows(0)("FREE_C21").ToString()
        Dim ediDestJisCd As String = dtEdi.Rows(0)("DEST_JIS_CD").ToString()

        'Dim compareWarningFlg As Boolean = False

        '削除フラグ(届先マスタ)
        If mSysDelF.Equals("1") = True Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E331", New String() {"届先コード", "届先マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '2013.02.13 BPはワーニング廃止コメントSTART

        '2013.04.01 要望番号1987 修正START 届先マスタへの上書きをしない⇒マスタ値が空の場合は上書き

        mDestNm = _Blc.SpaceCutChk(mDestNm)
        ediDestNm = _Blc.SpaceCutChk(ediDestNm)

        ' ''届先名称(マスタ値が完全一致でなければワーニング)
        'If String.IsNullOrEmpty(mDestNm) = True Then
        '    dtMdest.Rows(0).Item("DEST_NM") = dtEdi.Rows(0)("DEST_NM").ToString()
        '    'マスタ更新対象フラグ
        '    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

        'End If

        If mDestNm.Equals(ediDestNm) = True Then
            'チェックなし
        Else
            'choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.BP_WID_L006, 0)

            'If String.IsNullOrEmpty(choiceKb) = True Then
            '    msgArray(1) = "届先名称"
            '    msgArray(2) = "届先マスタ"
            '    msgArray(3) = "届先名称"
            '    msgArray(4) = "EDIデータ"
            '    msgArray(5) = String.Empty

            '    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.BP_WID_L006, ds, msgArray, _
            '                                dtEdi.Rows(0)("DEST_NM").ToString(), dtMdest.Rows(0).Item("DEST_NM").ToString())

            '    compareWarningFlg = True

            'ElseIf choiceKb.Equals("01") = True Then
            'ワーニングで"はい"を選択時
            dtMdest.Rows(0).Item("DEST_NM") = dtEdi.Rows(0)("DEST_NM").ToString()
            'マスタ更新対象フラグ
            dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

            'End If

        End If

        ' ''FREE_C21:届先住所(マスタ値が完全一致でなければワーニング)
        'If String.IsNullOrEmpty(mAdAll) = True Then
        '    dtMdest.Rows(0).Item("AD_1") = dtEdi.Rows(0)("DEST_AD_1").ToString()
        '    dtMdest.Rows(0).Item("AD_2") = dtEdi.Rows(0)("DEST_AD_2").ToString()
        '    dtMdest.Rows(0).Item("AD_3") = dtEdi.Rows(0)("DEST_AD_3").ToString()
        '    'マスタ更新対象フラグ
        '    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

        'End If


        If String.IsNullOrEmpty(ediFreeC21) = True Then
            'チェックなし
        Else

            mAdAll = SpaceCutChk(mAdAll)
            ediFreeC21 = SpaceCutChk(ediFreeC21)
            If mAdAll.Equals(ediFreeC21) = False Then

                'choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.BP_WID_L007, 0)

                'If String.IsNullOrEmpty(choiceKb) = True Then

                '    msgArray(1) = "届先住所"
                '    msgArray(2) = "届先マスタ"
                '    msgArray(3) = "住所"
                '    msgArray(4) = "EDIデータ"
                '    msgArray(5) = String.Empty
                '    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.BP_WID_L007, ds, msgArray, ediFreeC21, mAdAll)

                '    compareWarningFlg = True

                'ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("AD_1") = dtEdi.Rows(0)("DEST_AD_1").ToString()
                dtMdest.Rows(0).Item("AD_2") = dtEdi.Rows(0)("DEST_AD_2").ToString()
                dtMdest.Rows(0).Item("AD_3") = dtEdi.Rows(0)("DEST_AD_3").ToString()
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                'End If
            End If

        End If


        ' ''届先郵便番号(マスタ値が完全一致でなければワーニング)
        'If String.IsNullOrEmpty(mZip) = True Then
        '    dtMdest.Rows(0).Item("ZIP") = dtEdi.Rows(0)("DEST_ZIP").ToString()
        '    'マスタ更新対象フラグ
        '    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"
        'End If

        If String.IsNullOrEmpty(ediZip) = True Then
            'チェックなし
        Else
            If mZip.Equals(Replace(ediZip, "-", String.Empty)) = False Then

                'choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.BP_WID_L008, 0)

                'If String.IsNullOrEmpty(choiceKb) = True Then

                '    msgArray(1) = "届先郵便番号"
                '    msgArray(2) = "届先マスタ"
                '    msgArray(3) = "郵便番号"
                '    msgArray(4) = "EDIデータ"
                '    msgArray(5) = String.Empty
                '    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.BP_WID_L008, ds, msgArray, ediZip, mZip)

                '    compareWarningFlg = True

                'ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("ZIP") = dtEdi.Rows(0)("DEST_ZIP").ToString()
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                'End If

            End If

        End If

        ' ''届先電話番号(マスタ値が完全一致でなければワーニング)
        'If String.IsNullOrEmpty(mTel) = True Then
        '    dtMdest.Rows(0).Item("TEL") = dtEdi.Rows(0)("DEST_TEL").ToString()
        '    'マスタ更新対象フラグ
        '    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"
        'End If

        If String.IsNullOrEmpty(ediTel) = True Then
            'チェックなし
        Else
            If mTel.Equals(ediTel) = False Then

                'choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.BP_WID_L008, 0)

                'If String.IsNullOrEmpty(choiceKb) = True Then

                '    msgArray(1) = "届先電話番号"
                '    msgArray(2) = "届先マスタ"
                '    msgArray(3) = "電話番号"
                '    msgArray(4) = "EDIデータ"
                '    msgArray(5) = String.Empty
                '    ds = Me._Blc.SetComWarningL("W166", LMH030BLC.BP_WID_L008, ds, msgArray, ediTel, mTel)

                '    compareWarningFlg = True

                'ElseIf choiceKb.Equals("01") = True Then
                'ワーニングで"はい"を選択時
                dtMdest.Rows(0).Item("TEL") = dtEdi.Rows(0)("DEST_TEL").ToString()
                'マスタ更新対象フラグ
                dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

                'End If

            End If

        End If

        ''郵便番号を元に、郵便番号マスタよりJISコードを取得する。
        ''JISマスタ存在チェック
        'Dim warningString As String = String.Empty

        If String.IsNullOrEmpty(mJis) = True Then
            'If String.IsNullOrEmpty(ediZip) = False Then
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromZip", ds)

            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し出荷登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
                'warningString = "郵便番号マスタ"
            End If

        End If

        ' ''Else
        ' ''取得できなかった場合は、再度住所を元にJISマスタよりJISコードを取得する
        'If String.IsNullOrEmpty(mZipJis) = True Then
        '    ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromAdd", ds)

        '    If MyBase.GetResultCount = 0 Then
        '        'JIS_CDが取得できなくても、ワーニングを出力し出荷登録可能とする
        '        mZipJis = String.Empty
        '    Else
        '        mZipJis = ds.Tables("LMH030_M_JIS").Rows(0)("JIS_CD").ToString().Trim()
        '        'warningString = "JISマスタ"
        '    End If
        'End If

        If String.IsNullOrEmpty(mJis) = True Then
            dtMdest.Rows(0).Item("JIS") = mZipJis
            'マスタ更新対象フラグ
            dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"
        End If

        'If String.IsNullOrEmpty(mZipJis) = True AndAlso String.IsNullOrEmpty(ediDestJisCd) = False Then
        '    'JISマスタのJISコードが空or郵便番号マスタのJISコードが空の場合かつ、EDIデストコードに値がある場合、更新ワーニング
        '    'choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.BP_WID_L009, 0)

        '    'If String.IsNullOrEmpty(choiceKb) = True Then

        '    '    msgArray(1) = "JISコード"
        '    '    msgArray(2) = "届先マスタ"
        '    '    msgArray(3) = "JISコード"
        '    '    msgArray(4) = "EDIデータ"
        '    '    msgArray(5) = "※郵便番号・住所からJISコードが特定できないため、出荷画面で運賃がゼロになる可能性があります。"
        '    '    ds = Me._Blc.SetComWarningL("W197", LMH030BLC.BP_WID_L009, ds, msgArray, ediDestJisCd, mJis)

        '    '    compareWarningFlg = True

        '    'ElseIf choiceKb.Equals("01") = True Then
        '    'ワーニングで"はい"を選択時
        '    dtMdest.Rows(0).Item("JIS") = ediDestJisCd
        '    'マスタ更新対象フラグ
        '    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

        '    'End If


        '    'ElseIf String.IsNullOrEmpty(mZipJis) = True AndAlso String.IsNullOrEmpty(ediDestJisCd) = True Then
        '    '    'JISマスタのJISコードが空or郵便番号マスタのJISコードが空の場合かつ、EDIデストコードが空の場合、処理続行確認
        '    '    choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.BP_WID_L010, 0)

        '    '    If String.IsNullOrEmpty(choiceKb) = True Then

        '    '        ds = Me._Blc.SetComWarningL("W188", LMH030BLC.BP_WID_L010, ds, msgArray, ediDestJisCd, mJis)

        '    '        compareWarningFlg = True

        '    '    ElseIf choiceKb.Equals("01") = True Then
        '    '        'ワーニングで"はい"を選択時

        '    '    End If

        'ElseIf String.IsNullOrEmpty(ediDestJisCd) = False AndAlso ediDestJisCd.Equals(mJis) = False Then
        '    'EDIのJISコードが空でなくEDIのJISコードと届先マスタのJISコードに差異がある場合
        '    'choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.BP_WID_L011, 0)

        '    'If String.IsNullOrEmpty(choiceKb) = True Then

        '    '    msgArray(1) = "JISコード"
        '    '    msgArray(2) = "届先マスタ"
        '    '    msgArray(3) = "JISコード"
        '    '    msgArray(4) = "EDIデータ"
        '    '    msgArray(5) = String.Empty
        '    '    ds = Me._Blc.SetComWarningL("W187", LMH030BLC.BP_WID_L011, ds, msgArray, ediDestJisCd, mJis)

        '    '    compareWarningFlg = True

        '    'ElseIf choiceKb.Equals("01") = True Then
        '    'ワーニングで"はい"を選択時
        '    dtMdest.Rows(0).Item("JIS") = ediDestJisCd
        '    'マスタ更新対象フラグ
        '    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

        '    'ElseIf choiceKb.Equals("02") = True Then
        '    'ワーニングで"いいえ"を選択時
        '    'End If

        'ElseIf String.IsNullOrEmpty(ediDestJisCd) = True AndAlso mZipJis.Equals(mJis) = False Then
        '    'EDIのJISコードが空でJISマスタ(郵便番号マスタ)のJISコードと届先マスタのJISコードに差異がある場合
        '    'choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.BP_WID_L011, 0)

        '    'If String.IsNullOrEmpty(choiceKb) = True Then

        '    '    msgArray(1) = String.Concat(warningString, "から取得したJISコード")
        '    '    msgArray(2) = "届先マスタ"
        '    '    msgArray(3) = "JISコード"
        '    '    msgArray(4) = warningString
        '    '    msgArray(5) = String.Empty
        '    '    ds = Me._Blc.SetComWarningL("W187", LMH030BLC.BP_WID_L011, ds, msgArray, mZipJis, mJis)

        '    '    compareWarningFlg = True

        '    'ElseIf choiceKb.Equals("01") = True Then
        '    'ワーニングで"はい"を選択時
        '    dtMdest.Rows(0).Item("JIS") = mZipJis
        '    dtEdi.Rows(0)("DEST_JIS_CD") = mZipJis
        '    'マスタ更新対象フラグ
        '    dtMdest.Rows(0).Item("MST_UPDATE_FLG") = "1"

        '    'ElseIf choiceKb.Equals("02") = True Then
        '    '    'ワーニングで"いいえ"を選択時
        '    'End If

        'End If

        ' ''ワーニングが存在する場合はここでの判定はFalseで返す
        ''If compareWarningFlg = True Then
        ''    Return False
        ''End If

        ''2013.02.13 BPはワーニング廃止コメントEND

        '2013.04.01 要望番号1987 修正END 届先マスタへの上書きをしない

        Return True

    End Function

#End Region

#Region "届先マスタ追加時チェック"
    ''' <summary>
    ''' 届先マスタ追加時チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ZipCompareCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String, ByVal workDestCd As String, ByVal workDestString As String) As Boolean

        Dim dtMdest As DataTable = ds.Tables("LMH030_M_DEST")
        Dim dtEdi As DataTable = ds.Tables("LMH030_OUTKAEDI_L")
        Dim dtMJis As DataTable = ds.Tables("LMH030_M_JIS")
        Dim drEdiL As DataRow = dtEdi.Rows(0)

        'Dim mZip As String = dtMdest.Rows(0).Item("ZIP").ToString()
        'Dim mTel As String = dtMdest.Rows(0).Item("TEL").ToString()
        'Dim mJis As String = dtMdest.Rows(0).Item("JIS").ToString()
        Dim mZipJis As String = String.Empty

        Dim msgArray(5) As String
        Dim choiceKb As String = String.Empty

        Dim ediZip As String = dtEdi.Rows(0)("DEST_ZIP").ToString()
        Dim ediDestJisCd As String = dtEdi.Rows(0)("DEST_JIS_CD").ToString()

        'Dim compareWarningFlg As Boolean = False

        '2012.03.28 要望番号948 修正START
        '郵便番号を元に、郵便番号マスタよりJISコードを取得する。
        'JISマスタ存在チェック
        Dim warningString As String = String.Empty

        If String.IsNullOrEmpty(ediZip) = False Then
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromZip", ds)

            'If MyBase.GetResultCount = 0 Then
            '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"届先JISコード", "JISマスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            '    Return False
            'End If

            'mZipJis = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
            'warningString = "郵便番号マスタ"

            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し出荷登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_ZIP").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "郵便番号マスタ"
            End If

        End If

        '取得できなかった場合は、再度住所を元にJISマスタよりJISコードを取得する
        If String.IsNullOrEmpty(mZipJis) = True Then
            'Else
            ds = MyBase.CallDAC(Me._DacCom, "SelectDataMjisFromAdd", ds)

            'If MyBase.GetResultCount = 0 Then
            '    MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E332", New String() {"届先住所１＋届先住所２＋届先住所３", "JISマスタ", "県＋市"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            '    Return False
            'End If

            'mZipJis = ds.Tables("LMH030_M_JIS").Rows(0)("JIS_CD").ToString().Trim()
            'warningString = "JISマスタ"

            If MyBase.GetResultCount = 0 Then
                'JIS_CDが取得できなくても、ワーニングを出力し出荷登録可能とする
                mZipJis = String.Empty
            Else
                mZipJis = ds.Tables("LMH030_M_JIS").Rows(0)("JIS_CD").ToString().Trim()
                warningString = "JISマスタ"
            End If

        End If
        '2012.03.28 要望番号948 修正END

        '2013.02.13 BPはワーニング廃止コメントSTART

        'choiceKb = Me.SetDestWarningChoiceKb(dtEdi, ds, LMH030BLC.BP_WID_L012, 0)

        'If String.IsNullOrEmpty(choiceKb) = True Then

        '    'ワーニング設定する
        '    msgArray(1) = workDestString
        '    msgArray(2) = "届先マスタ"
        '    msgArray(3) = "EDIデータ"
        '    If String.IsNullOrEmpty(mZipJis) = False Then
        '        msgArray(4) = String.Empty
        '    Else
        '        msgArray(4) = "※郵便番号・住所からJISコードが特定できないため、出荷画面で運賃がゼロになる可能性があります。"
        '    End If

        '    ds = Me._Blc.SetComWarningL("W186", LMH030BLC.BP_WID_L012, ds, msgArray, workDestCd, String.Empty) '追加箇所 20110222

        '    compareWarningFlg = True
        '    'Return True

        'ElseIf choiceKb.Equals("01") = True Then
        'ワーニングで"はい"を選択時
        Dim drMD As DataRow = dtMdest.NewRow()
        drMD("NRS_BR_CD") = drEdiL("NRS_BR_CD").ToString()
        drMD("CUST_CD_L") = drEdiL("CUST_CD_L").ToString()
        drMD("DEST_CD") = workDestCd
        drMD("EDI_CD") = workDestCd
        drMD("DEST_NM") = drEdiL("DEST_NM").ToString()
        drMD("ZIP") = Replace(drEdiL("DEST_ZIP").ToString().Trim(), "-", String.Empty)
        drMD("AD_1") = drEdiL("DEST_AD_1").ToString()
        drMD("AD_2") = drEdiL("DEST_AD_2").ToString()
        drMD("AD_3") = drEdiL("DEST_AD_3").ToString()
        drMD("COA_YN") = "00"
        drMD("TEL") = drEdiL("DEST_TEL").ToString()
        drMD("JIS") = mZipJis
        drMD("PICK_KB") = "01"
        drMD("BIN_KB") = "01"
        '要望番号2013 追加START 纏め顧客コードは届先コードで更新
        drMD("CUST_DEST_CD") = workDestCd
        '要望番号2013 追加END
        'マスタ自動追加対象フラグ
        drMD("MST_INSERT_FLG") = "1"
        dtMdest.Rows.Add(drMD)

        'End If

        ''ワーニングが存在する場合はここでの判定はFalseで返す
        'If compareWarningFlg = True Then
        '    Return False
        'End If

        '2013.02.13 BPはワーニング廃止コメントEND

        Return True

    End Function

#End Region

#Region "ワーニング処理(EDI(大)届先)選択区分の取得"

    ''' <summary>
    ''' 選択区分の取得
    ''' </summary>
    ''' <param name="setDt"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDestWarningChoiceKb(ByVal setDt As DataTable, ByVal ds As DataSet, _
                                           ByVal warningId As String, ByVal count As Integer) As String

        Dim dtWarning As DataTable = ds.Tables("WARNING_SHORI")
        Dim ediCtlNoL As String = setDt.Rows(count)("EDI_CTL_NO").ToString()
        Dim max As Integer = dtWarning.Rows.Count - 1
        Dim dr As DataRow
        Dim choiceKb As String = String.Empty

        'ワーニング処理設定されていなければ処理終了
        If max = -1 Then
            Return choiceKb
        End If

        For i As Integer = 0 To max

            dr = dtWarning.Rows(i)

            If warningId.Equals(dr("EDI_WARNING_ID").ToString()) AndAlso ediCtlNoL.Equals(dr("EDI_CTL_NO_L")) Then
                'ワーニング処理設定の値を反映
                choiceKb = dr.Item("CHOICE_KB").ToString()
                Return choiceKb

            End If

        Next

        Return choiceKb
    End Function

#End Region

#Region "ワーニング処理(EDI(中)商品)選択区分の取得"

    ''' <summary>
    ''' 選択区分の取得
    ''' </summary>
    ''' <param name="setDt"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetGoodsWarningChoiceKb(ByRef setDt As DataTable, ByVal ds As DataSet, _
                                           ByVal warningId As String, ByVal count As Integer) As String

        Dim dtWarning As DataTable = ds.Tables("WARNING_SHORI")
        Dim ediCtlNoL As String = setDt.Rows(0)("EDI_CTL_NO").ToString()
        Dim ediCtlNoM As String = setDt.Rows(count)("EDI_CTL_NO_CHU").ToString()
        Dim max As Integer = dtWarning.Rows.Count - 1
        Dim dr As DataRow
        Dim choiceKb As String = String.Empty
        Dim mstFlg As String = String.Empty

        'ワーニング処理設定されていなければ処理終了
        If max = -1 Then
            Return choiceKb
        End If

        For i As Integer = 0 To max

            dr = dtWarning.Rows(i)

            If warningId.Equals(dr("EDI_WARNING_ID").ToString()) AndAlso ediCtlNoL.Equals(dr("EDI_CTL_NO_L")) _
                                                                AndAlso ediCtlNoM.Equals(dr("EDI_CTL_NO_M")) Then
                'ワーニング画面の処理区分値を反映
                choiceKb = dr.Item("CHOICE_KB").ToString()

                mstFlg = warningId.Substring(7, 1)

                Select Case mstFlg
                    Case "1"
                        'ワーニング処理設定の値を反映
                        setDt.Rows(0).Item("NRS_GOODS_CD") = dr.Item("MST_VALUE")
                    Case Else

                End Select

                Return choiceKb

            End If

        Next

        Return choiceKb
    End Function

#End Region


#Region "SPACE除去 + 文字変換"
    ''' <summary>
    ''' SPACE除去 + 文字変換
    ''' </summary>
    ''' <param name="chkFld"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SpaceCutChk(ByVal chkFld As String) As String

        chkFld = Replace(Trim(chkFld), Space(1), String.Empty)
        chkFld = Replace(chkFld, "　", String.Empty)
        chkFld = StrConv(chkFld, VbStrConv.Wide)

        Return chkFld

    End Function

#End Region
#End Region

#Region "データセット設定"

#Region "データセット設定(出荷管理番号L)"

    ''' <summary>
    ''' データセット設定(出荷管理番号L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="matomeFlg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOutkaNoL(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

        Dim outkaKanriNo As String = String.Empty
        Dim dr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim outkaKanriNoPrm As String = ds.Tables("LMH030INOUT").Rows(0).Item("OUTKA_CTL_NO").ToString()

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim eventShubetsu As String = ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()

        Dim max As Integer = dt.Rows.Count - 1

        If eventShubetsu.Equals("3") = True Then

            '紐付け処理の場合
            dr("OUTKA_CTL_NO") = outkaKanriNoPrm
            For i As Integer = 0 To max
                dt.Rows(i)("OUTKA_CTL_NO") = outkaKanriNoPrm
            Next

        ElseIf matomeFlg = False Then

            '通常出荷登録処理の場合
            Dim num As New NumberMasterUtility
            outkaKanriNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.OUTKA_NO_L, Me, nrsBrCd)

            dr("OUTKA_CTL_NO") = outkaKanriNo

            For i As Integer = 0 To max
                dt.Rows(i)("OUTKA_CTL_NO") = outkaKanriNo
            Next

        Else
            'まとめ処理の場合
            outkaKanriNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("OUTKA_CTL_NO").ToString()
            dr("OUTKA_CTL_NO") = outkaKanriNo
            dr("FREE_C30") = String.Concat("04-", ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("EDI_CTL_NO").ToString())

            For i As Integer = 0 To max
                dt.Rows(i)("OUTKA_CTL_NO") = outkaKanriNo
            Next

        End If

        Return ds

    End Function

#End Region

#Region "データセット設定(出荷管理番号M)"
    ''' <summary>
    ''' 出荷管理番号(中)取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetOutkaNoM(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

        Dim outkaKanriNo As String = String.Empty
        Dim dtEdiM As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim dtHimoduke As DataTable = ds.Tables("LMH030_HIMODUKE")
        Dim nrsBrCd As String = dtEdiM.Rows(0).ToString
        Dim max As Integer = dtEdiM.Rows.Count - 1
        Dim eventShubetsu As String = ds.Tables("LMH030_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()

        If eventShubetsu.Equals("3") = True Then
            '紐付け処理の場合
            For i As Integer = 0 To max
                outkaKanriNo = dtHimoduke.Rows(i)("HIMODUKE_NO").ToString()
                dtEdiM.Rows(i)("OUTKA_CTL_NO_CHU") = outkaKanriNo
            Next

        ElseIf matomeFlg = False Then
            '通常出荷登録処理の場合
            For i As Integer = 0 To max
                outkaKanriNo = (i + 1).ToString("000")
                dtEdiM.Rows(i)("OUTKA_CTL_NO_CHU") = outkaKanriNo
            Next

        Else
            'まとめ処理の場合、まとめ先DataSetから取得
            Dim maxOutkaKanriNo As Integer = Me._DacCom.GetMaxOUTKA_NO_CHU(ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("NRS_BR_CD").ToString, ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)("OUTKA_CTL_NO").ToString)
            For i As Integer = 0 To max
                outkaKanriNo = (maxOutkaKanriNo + i + 1).ToString("000")
                dtEdiM.Rows(i)("OUTKA_CTL_NO_CHU") = outkaKanriNo
            Next

        End If

        Return ds

    End Function

#End Region

#Region "データセット設定(出荷L)"
    ''' <summary>
    ''' データセット設定(出荷L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetOutkaL(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim outkaDr As DataRow = ds.Tables("LMH030_C_OUTKA_L").NewRow()
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim matomesakiDt As DataTable = ds.Tables("LMH030_MATOMESAKI_EDIL")

        If matomeFlg = False Then
            '通常登録処理
            outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            outkaDr("OUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
            outkaDr("OUTKA_KB") = ediDr("OUTKA_KB")
            outkaDr("SYUBETU_KB") = ediDr("SYUBETU_KB")
            outkaDr("OUTKA_STATE_KB") = ediDr("OUTKA_STATE_KB")
            outkaDr("OUTKAHOKOKU_YN") = _Blc.FormatZero(ediDr("OUTKAHOKOKU_YN").ToString(), 2)
            outkaDr("PICK_KB") = ediDr("PICK_KB")
            outkaDr("DENP_NO") = String.Empty
            outkaDr("ARR_KANRYO_INFO") = String.Empty
            outkaDr("WH_CD") = ediDr("WH_CD")
            outkaDr("OUTKA_PLAN_DATE") = ediDr("OUTKA_PLAN_DATE")
            outkaDr("OUTKO_DATE") = ediDr("OUTKO_DATE")
            outkaDr("ARR_PLAN_DATE") = ediDr("ARR_PLAN_DATE")
            outkaDr("ARR_PLAN_TIME") = ediDr("ARR_PLAN_TIME")
            outkaDr("HOKOKU_DATE") = ediDr("HOKOKU_DATE")
            outkaDr("TOUKI_HOKAN_YN") = _Blc.FormatZero(ediDr("TOUKI_HOKAN_YN").ToString(), 2)
            outkaDr("END_DATE") = String.Empty
            outkaDr("CUST_CD_L") = ediDr("CUST_CD_L")
            outkaDr("CUST_CD_M") = ediDr("CUST_CD_M")
            outkaDr("SHIP_CD_L") = ediDr("SHIP_CD_L")
            outkaDr("SHIP_CD_M") = String.Empty

            '2013.02.14 住所３は必ずEDIの値をセットする。START
            outkaDr("DEST_AD_3") = ediDr("DEST_AD_3")

            If ds.Tables("LMH030_M_DEST").Rows.Count > 0 Then
                Dim destMDr As DataRow = ds.Tables("LMH030_M_DEST").Rows(0)
                outkaDr("DEST_CD") = destMDr("DEST_CD")
                'outkaDr("DEST_AD_3") = destMDr("AD_3")
                outkaDr("DEST_TEL") = destMDr("TEL")
            Else
                outkaDr("DEST_CD") = ediDr("DEST_CD")
                'outkaDr("DEST_AD_3") = ediDr("DEST_AD_3")
                outkaDr("DEST_TEL") = ediDr("DEST_TEL")
            End If
            '2013.02.14 住所３は必ずEDIの値をセットする。END

            outkaDr("NHS_REMARK") = String.Empty
            outkaDr("SP_NHS_KB") = ediDr("SP_NHS_KB")
            outkaDr("COA_YN") = _Blc.FormatZero(ediDr("COA_YN").ToString(), 2)
            outkaDr("CUST_ORD_NO") = ediDr("CUST_ORD_NO")
            outkaDr("BUYER_ORD_NO") = ediDr("BUYER_ORD_NO")
            outkaDr("REMARK") = ediDr("REMARK")
            outkaDr("OUTKA_PKG_NB") = SumPkgNb(dt)
            outkaDr("DENP_YN") = _Blc.FormatZero(ediDr("DENP_YN").ToString(), 2)
            outkaDr("PC_KB") = ediDr("PC_KB")
            outkaDr("UNCHIN_YN") = _Blc.FormatZero(ediDr("UNCHIN_YN").ToString(), 2)
            outkaDr("NIYAKU_YN") = _Blc.FormatZero(ediDr("NIYAKU_YN").ToString(), 2)
            outkaDr("ALL_PRINT_FLAG") = "00"
            outkaDr("NIHUDA_FLAG") = "00"
            outkaDr("NHS_FLAG") = "00"
            outkaDr("DENP_FLAG") = "00"
            outkaDr("COA_FLAG") = "00"
            outkaDr("HOKOKU_FLAG") = "00"
            outkaDr("MATOME_PICK_FLAG") = "00"
            outkaDr("LAST_PRINT_DATE") = String.Empty
            outkaDr("LAST_PRINT_TIME") = String.Empty
            outkaDr("SASZ_USER") = String.Empty
            outkaDr("OUTKO_USER") = String.Empty
            outkaDr("KEN_USER") = String.Empty
            outkaDr("OUTKA_USER") = String.Empty
            outkaDr("HOU_USER") = String.Empty
            outkaDr("ORDER_TYPE") = ediDr("FREE_C05") 'ビーピー・カストロール対応 terakawa 2012.12.12
            outkaDr("SYS_DEL_FLG") = "0"
            outkaDr("DEST_KB") = "02"
            outkaDr("DEST_NM") = ediDr("DEST_NM")
            outkaDr("DEST_AD_1") = ediDr("DEST_AD_1")
            outkaDr("DEST_AD_2") = ediDr("DEST_AD_2")
        Else
            'まとめ登録処理
            outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            outkaDr("OUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
            outkaDr("OUTKA_PKG_NB") = SumPkgNb(dt) + Convert.ToDouble(matomesakiDt.Rows(0)("OUTKA_PKG_NB"))
            outkaDr("SYS_UPD_DATE") = matomesakiDt.Rows(0)("SYS_UPD_DATE")
            outkaDr("SYS_UPD_TIME") = matomesakiDt.Rows(0)("SYS_UPD_TIME")
        End If
        'データセットに設定
        ds.Tables("LMH030_C_OUTKA_L").Rows.Add(outkaDr)

        Return ds

    End Function

#End Region

#Region "データセット設定(出荷包装個数)"
    Private Function SumPkgNb(ByVal dt As DataTable) As Double

        Dim max As Integer = dt.Rows.Count - 1
        Dim sumNb As Double = 0
        Dim calcPkgModNb As Long = 0
        Dim calcPkgQuoNb As Double = 0

        For i As Integer = 0 To max

            If String.IsNullOrEmpty(dt.Rows(i)("PKG_NB").ToString()) = False _
            AndAlso String.IsNullOrEmpty(dt.Rows(i)("OUTKA_TTL_NB").ToString()) = False _
            AndAlso (dt.Rows(i)("PKG_NB").ToString()).Equals("0") = False _
            AndAlso (dt.Rows(i)("OUTKA_TTL_NB").ToString()).Equals("0") = False Then

                calcPkgModNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) Mod Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) / Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Math.Floor(calcPkgQuoNb)

            End If

            sumNb = sumNb + Convert.ToDouble(dt.Rows(i)("OUTKA_PKG_NB"))

            If 0 = calcPkgModNb Then
            Else
                sumNb = sumNb + 1
            End If

        Next

        Return sumNb

    End Function
#End Region

#Region "データセット設定(出荷M)"
    ''' <summary>
    ''' データセット設定(出荷M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetOutkaM(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow
        Dim outkaDr As DataRow
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim remark As String = String.Empty
        Dim SetNo As String = String.Empty
        Dim ediLDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim max As Integer = dt.Rows.Count - 1
        Dim calcPkgModNb As Long = 0
        Dim calcPkgQuoNb As Double = 0


        For i As Integer = 0 To max

            ediDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
            outkaDr = ds.Tables("LMH030_C_OUTKA_M").NewRow()

            If String.IsNullOrEmpty(dt.Rows(i)("PKG_NB").ToString()) = False _
            AndAlso String.IsNullOrEmpty(dt.Rows(i)("OUTKA_TTL_NB").ToString()) = False _
            AndAlso (dt.Rows(i)("PKG_NB").ToString()).Equals("0") = False _
            AndAlso (dt.Rows(i)("OUTKA_TTL_NB").ToString()).Equals("0") = False Then

                calcPkgModNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) Mod Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Convert.ToInt64(dt.Rows(i)("OUTKA_TTL_NB")) / Convert.ToInt64(dt.Rows(i)("PKG_NB"))
                calcPkgQuoNb = Math.Floor(calcPkgQuoNb)
            End If

            outkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            outkaDr("OUTKA_NO_L") = ediLDr("OUTKA_CTL_NO")
            outkaDr("OUTKA_NO_M") = ediDr("OUTKA_CTL_NO_CHU")
            If ediDr("SET_KB").ToString = "2" Then
                outkaDr("EDI_SET_NO") = ediDr("FREE_C10")
            Else
                outkaDr("EDI_SET_NO") = String.Empty
            End If
            outkaDr("COA_YN") = _Blc.FormatZero(ediDr("COA_YN").ToString(), 2)
            outkaDr("CUST_ORD_NO_DTL") = ediDr("CUST_ORD_NO_DTL")
            outkaDr("BUYER_ORD_NO_DTL") = ediDr("BUYER_ORD_NO_DTL")
            outkaDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            outkaDr("RSV_NO") = ediDr("RSV_NO")
            outkaDr("LOT_NO") = ediDr("LOT_NO")
            outkaDr("SERIAL_NO") = ediDr("SERIAL_NO")
            outkaDr("ALCTD_KB") = ediDr("ALCTD_KB")
            outkaDr("OUTKA_PKG_NB") = ediDr("PKG_NB")
            outkaDr("OUTKA_HASU") = ediDr("OUTKA_HASU")
            outkaDr("OUTKA_QT") = ediDr("OUTKA_QT")
            outkaDr("OUTKA_TTL_NB") = ediDr("OUTKA_TTL_NB")
            outkaDr("OUTKA_TTL_QT") = ediDr("OUTKA_TTL_QT")
            outkaDr("ALCTD_NB") = 0
            outkaDr("ALCTD_QT") = 0
            outkaDr("BACKLOG_NB") = ediDr("OUTKA_TTL_NB")
            outkaDr("BACKLOG_QT") = ediDr("OUTKA_TTL_QT")
            outkaDr("UNSO_ONDO_KB") = ediDr("UNSO_ONDO_KB")
            outkaDr("IRIME") = ediDr("IRIME")
            outkaDr("IRIME_UT") = ediDr("IRIME_UT")

            If Convert.ToInt64(dt.Rows(i)("PKG_NB")) = 0 Then
                outkaDr("OUTKA_M_PKG_NB") = 0
            Else
                If 0 = calcPkgModNb Then
                    outkaDr("OUTKA_M_PKG_NB") = calcPkgQuoNb
                Else
                    outkaDr("OUTKA_M_PKG_NB") = calcPkgQuoNb + 1
                End If
            End If

            If Convert.ToInt64(outkaDr("OUTKA_M_PKG_NB")) > 999 Then
                outkaDr("OUTKA_M_PKG_NB") = 1
            End If

            outkaDr("REMARK") = ediDr("REMARK")
            outkaDr("SIZE_KB") = String.Empty
            outkaDr("ZAIKO_KB") = String.Empty
            outkaDr("SOURCE_CD") = String.Empty
            outkaDr("YELLOW_CARD") = String.Empty
            outkaDr("PRINT_SORT") = "99"
            outkaDr("SYS_DEL_FLG") = "0"

            'データセットに設定
            ds.Tables("LMH030_C_OUTKA_M").Rows.Add(outkaDr)

        Next

        Return ds

    End Function

#End Region

#Region "データセット設定(EDI受信DTL)"
    ''' <summary>
    ''' データセット設定(EDI受信テーブル(DTL))
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetEdiRcvDtl(ByVal ds As DataSet) As DataSet

        Dim rcvDr As DataRow
        Dim outkaedimDr As DataRow
        Dim outkaedilDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim max As Integer = dt.Rows.Count - 1
        Dim outkaCtlNoChu As String = String.Empty

        For i As Integer = 0 To max

            outkaedimDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
            rcvDr = ds.Tables("LMH030_EDI_RCV_DTL").NewRow()

            rcvDr("NRS_BR_CD") = outkaedilDr("NRS_BR_CD")
            rcvDr("EDI_CTL_NO") = outkaedilDr("EDI_CTL_NO")
            rcvDr("EDI_CTL_NO_CHU") = outkaedimDr("EDI_CTL_NO_CHU")
            rcvDr("OUTKA_CTL_NO") = outkaedimDr("OUTKA_CTL_NO")
            rcvDr("OUTKA_CTL_NO_CHU") = outkaedimDr("OUTKA_CTL_NO_CHU").ToString()
            rcvDr("SYS_DEL_FLG") = "0"

            'データセットに設定
            ds.Tables("LMH030_EDI_RCV_DTL").Rows.Add(rcvDr)

        Next

        Return ds

    End Function

#End Region

#Region "データセット設定(EDI受信HED)"
    ''' <summary>
    ''' データセット設定(EDI受信テーブル(HED))
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetEdiRcvHed(ByVal ds As DataSet) As DataSet

        Dim rcvDr As DataRow = ds.Tables("LMH030_EDI_RCV_HED").NewRow()
        Dim inDr As DataRow = ds.Tables("LMH030INOUT").Rows(0)
        Dim outkaediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        rcvDr("NRS_BR_CD") = outkaediDr("NRS_BR_CD")
        rcvDr("EDI_CTL_NO") = outkaediDr("EDI_CTL_NO")
        rcvDr("CUST_CD_L") = outkaediDr("CUST_CD_L")
        rcvDr("CUST_CD_M") = outkaediDr("CUST_CD_M")
        rcvDr("SYS_UPD_DATE") = inDr("RCV_SYS_UPD_DATE")
        rcvDr("SYS_UPD_TIME") = inDr("RCV_SYS_UPD_TIME")
        rcvDr("SYS_DEL_FLG") = "0"
        rcvDr("OUTKA_CTL_NO") = outkaediDr("OUTKA_CTL_NO")

        'データセットに設定
        ds.Tables("LMH030_EDI_RCV_HED").Rows.Add(rcvDr)

        Return ds

    End Function

#End Region

#Region "データセット設定(作業)"
    ''' <summary>
    ''' データセット設定(作業)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetSagyo(ByVal ds As DataSet) As DataSet

        Dim ediDrM As DataRow
        Dim sagyoDr As DataRow
        Dim ediDrL As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim max As Integer = ds.Tables("LMH030_OUTKAEDI_M").Rows.Count - 1
        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim sagyoCD As String = String.Empty
        Dim outkaNoLM As String = String.Empty
        Dim num As New NumberMasterUtility

        For i As Integer = 0 To max

            ediDrM = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)

            For j As Integer = 1 To 5

                sagyoCD = ediDrM("OUTKA_KAKO_SAGYO_KB_" & j).ToString()

                If String.IsNullOrEmpty(sagyoCD) = False Then

                    outkaNoLM = String.Concat(ediDrM("OUTKA_CTL_NO"), ediDrM("OUTKA_CTL_NO_CHU"))

                    sagyoDr = ds.Tables("LMH030_E_SAGYO").NewRow()

                    'sagyoDr("SAGYO_COMP") = "0"
                    'sagyoDr("SKYU_CHK") = "0"
                    sagyoDr("SAGYO_COMP") = "00"
                    sagyoDr("SKYU_CHK") = "00"
                    sagyoDr("SAGYO_REC_NO") = num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, nrsBrCd)
                    sagyoDr("SAGYO_SIJI_NO") = String.Empty
                    sagyoDr("INOUTKA_NO_LM") = outkaNoLM
                    sagyoDr("NRS_BR_CD") = ediDrL("NRS_BR_CD")
                    sagyoDr("WH_CD") = ediDrL("WH_CD")
                    sagyoDr("IOZS_KB") = "21"
                    sagyoDr("SAGYO_CD") = sagyoCD
                    sagyoDr("CUST_CD_L") = ediDrL("CUST_CD_L")
                    sagyoDr("CUST_CD_M") = ediDrL("CUST_CD_M")
                    sagyoDr("DEST_CD") = ediDrL("DEST_CD")
                    sagyoDr("DEST_NM") = ediDrL("DEST_NM")
                    sagyoDr("GOODS_CD_NRS") = ediDrM("NRS_GOODS_CD")
                    sagyoDr("GOODS_NM_NRS") = ediDrM("GOODS_NM")
                    sagyoDr("LOT_NO") = ediDrM("LOT_NO")
                    sagyoDr("SAGYO_NB") = 0
                    sagyoDr("SAGYO_GK") = 0
                    sagyoDr("REMARK_SKYU") = ediDrM("REMARK")
                    sagyoDr("SAGYO_COMP_CD") = String.Empty
                    sagyoDr("SAGYO_COMP_DATE") = String.Empty
                    sagyoDr("DEST_SAGYO_FLG") = "00"
                    sagyoDr("SYS_DEL_FLG") = "0"

                    'データセットに設定
                    ds.Tables("LMH030_E_SAGYO").Rows.Add(sagyoDr)
                End If
            Next

            For k As Integer = 1 To 2

                sagyoCD = ediDrM("SAGYO_KB_" & k).ToString()

                If String.IsNullOrEmpty(sagyoCD) = False Then

                    outkaNoLM = String.Concat(ediDrM("OUTKA_CTL_NO"), ediDrM("OUTKA_CTL_NO_CHU"))

                    sagyoDr = ds.Tables("LMH030_E_SAGYO").NewRow()

                    'sagyoDr("SAGYO_COMP") = "0"
                    'sagyoDr("SKYU_CHK") = "0"
                    sagyoDr("SAGYO_COMP") = "00"
                    sagyoDr("SKYU_CHK") = "00"
                    sagyoDr("SAGYO_REC_NO") = num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, nrsBrCd)
                    sagyoDr("SAGYO_SIJI_NO") = String.Empty
                    sagyoDr("INOUTKA_NO_LM") = outkaNoLM
                    sagyoDr("NRS_BR_CD") = ediDrL("NRS_BR_CD")
                    sagyoDr("WH_CD") = ediDrL("WH_CD")
                    sagyoDr("IOZS_KB") = "21"
                    sagyoDr("SAGYO_CD") = sagyoCD
                    sagyoDr("CUST_CD_L") = ediDrL("CUST_CD_L")
                    sagyoDr("CUST_CD_M") = ediDrL("CUST_CD_M")
                    sagyoDr("DEST_CD") = ediDrL("DEST_CD")
                    sagyoDr("DEST_NM") = ediDrL("DEST_NM")
                    sagyoDr("GOODS_CD_NRS") = ediDrM("NRS_GOODS_CD")
                    sagyoDr("GOODS_NM_NRS") = ediDrM("GOODS_NM")
                    sagyoDr("LOT_NO") = ediDrM("LOT_NO")
                    sagyoDr("SAGYO_NB") = 0
                    sagyoDr("SAGYO_GK") = 0
                    sagyoDr("REMARK_SKYU") = ediDrM("REMARK")
                    sagyoDr("SAGYO_COMP_CD") = String.Empty
                    sagyoDr("SAGYO_COMP_DATE") = String.Empty
                    sagyoDr("DEST_SAGYO_FLG") = "01"
                    sagyoDr("SYS_DEL_FLG") = "0"

                    'データセットに設定
                    ds.Tables("LMH030_E_SAGYO").Rows.Add(sagyoDr)
                End If
            Next
        Next

        Return ds

    End Function

#End Region

#Region "データセット設定(運送L)"
    ''' <summary>
    ''' データセット設定(運送L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetUnsoL(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)
        Dim unsoDr As DataRow = ds.Tables("LMH030_UNSO_L").NewRow()
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(0)
        Dim outkaLDr As DataRow = ds.Tables("LMH030_C_OUTKA_L").Rows(0)
        Dim nrsBrCd As String = ds.Tables("LMH030INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim num As New NumberMasterUtility

        If matomeFlg = False Then


            '通常登録
            unsoDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            unsoDr("UNSO_NO_L") = num.GetAutoCode(NumberMasterUtility.NumberKbn.UNSO_NO_L, Me, nrsBrCd)
            unsoDr("YUSO_BR_CD") = ediDr("NRS_BR_CD")
            unsoDr("WH_CD") = ediDr("WH_CD")
            unsoDr("INOUTKA_NO_L") = ediDr("OUTKA_CTL_NO")
            unsoDr("TRIP_NO") = String.Empty
            '要望番号2140 追加START 2013.12.25
            Dim unsoSetFlg As Boolean = True
            Dim unsoSetFlgExp As Boolean = False   'EDI連携区分
            Dim jisCd As String = String.Empty

#If False Then ' BP運送会社自動設定対応 20161115 changed by inoue
            '要望番号2142 追加START 2014.1.5
            '運送会社コードが空白ではなかった場合、EDI連携区分を参照する
            If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString().Trim()) = False AndAlso _
               String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString().Trim()) = False Then

                '運送会社連携区分が「セットする」の場合、JISにかかわらずそのまま設定。
                '運送会社M取得
                MyBase.Logger.WriteLog(0, "LMH030BLC501", "SelectDataMUnsocoList", "1")
                ds = MyBase.CallDAC(Me._DacCom, "SelectDataMUnsocoList", ds)
                If ds.Tables("LMH030_M_UNSOCO").Rows.Count <> 0 _
                    AndAlso EDI_USED_KBN_USED.Equals(ds.Tables("LMH030_M_UNSOCO").Rows(0).Item("EDI_USED_KBN").ToString()) Then
                    unsoSetFlgExp = True
                    unsoSetFlg = True
                End If


                MyBase.Logger.WriteLog(0, "LMH030BLC501", "SelectDataMUnsocoList", "2")
            End If

            If unsoSetFlgExp = False Then

                If String.IsNullOrEmpty(ediDr("DEST_JIS_CD").ToString()) = False Then
                    jisCd = ediDr("DEST_JIS_CD").ToString()
                ElseIf String.IsNullOrEmpty(ediDr("DEST_ZIP").ToString()) = False Then
                    '郵便番号からJISコードの取得
                    If Not ds.Tables("LMH030_GET_ZIPJIS") Is Nothing AndAlso ds.Tables("LMH030_GET_ZIPJIS").Rows.Count <> 0 Then
                        For i As Integer = 0 To ds.Tables("LMH030_GET_ZIPJIS").Rows.Count - 1
                            ds.Tables("LMH030_GET_ZIPJIS").Rows.RemoveAt(0)
                        Next
                    End If
                    Dim row As DataRow = ds.Tables("LMH030_GET_ZIPJIS").NewRow
                    row("NRS_BR_CD") = ediDr("NRS_BR_CD").ToString()
                    row("ZIP_NO") = ediDr("DEST_ZIP").ToString()
                    ds.Tables("LMH030_GET_ZIPJIS").Rows.Add(row)

                    ds = MyBase.CallDAC(Me._Dac, "SelectDataTgtJisMZIP", ds)
                    MyBase.Logger.WriteLog(0, "LMH030BLC501", "SetDatasetUnsoL", "1")
                    If ds.Tables("LMH030_GET_ZIPJIS").Rows.Count <> 0 Then
                        jisCd = ds.Tables("LMH030_GET_ZIPJIS").Rows(0).Item("JIS_CD").ToString
                        MyBase.Logger.WriteLog(0, "LMH030BLC501", "SetDatasetUnsoL", "2")
                    End If
                End If


                If String.IsNullOrEmpty(jisCd) = False Then

                    MyBase.Logger.WriteLog(0, "LMH030BLC501", "SetDatasetUnsoL", "3")
                    ds = MyBase.CallDAC(Me._Dac, "SelectDataTgtJisZkbn", ds)
                    MyBase.Logger.WriteLog(0, "LMH030BLC501", "SetDatasetUnsoL", "4")
                    If ds.Tables("LMH030_TGT_JIS").Rows.Count <> 0 Then
                        Dim where As String = String.Empty
                        where = String.Concat("TGT_JIS_CD = '", jisCd.Substring(0, 2), "'")
                        MyBase.Logger.WriteLog(0, "LMH030BLC501", "SetDatasetUnsoL", "5")

                        Dim drs As DataRow() = ds.Tables("LMH030_TGT_JIS").Select(where)
                        If drs.Count <> 0 Then
                            '関東１都６県＋静岡の場合、運送会社コードは空白にする
                            unsoDr("UNSO_CD") = String.Empty
                            unsoDr("UNSO_BR_CD") = String.Empty
                            unsoSetFlg = False
                        End If
                        MyBase.Logger.WriteLog(0, "LMH030BLC501", "SetDatasetUnsoL", "6")
                    End If

                End If
            End If

            If unsoSetFlg = True Then
                unsoDr("UNSO_CD") = ediDr("UNSO_CD")
                unsoDr("UNSO_BR_CD") = ediDr("UNSO_BR_CD")
            End If
#Else

            unsoDr("UNSO_CD") = ediDr("UNSO_CD")
            unsoDr("UNSO_BR_CD") = ediDr("UNSO_BR_CD")

#End If

            '要望番号2140 追加END 2013.12.25
            unsoDr("BIN_KB") = ediDr("BIN_KB")
            unsoDr("JIYU_KB") = String.Empty
            unsoDr("DENP_NO") = String.Empty
            unsoDr("OUTKA_PLAN_DATE") = ediDr("OUTKA_PLAN_DATE")
            unsoDr("OUTKA_PLAN_TIME") = String.Empty
            unsoDr("ARR_PLAN_DATE") = ediDr("ARR_PLAN_DATE")
            unsoDr("ARR_PLAN_TIME") = ediDr("ARR_PLAN_TIME")
            unsoDr("ARR_ACT_TIME") = String.Empty
            unsoDr("CUST_CD_L") = ediDr("CUST_CD_L")
            unsoDr("CUST_CD_M") = ediDr("CUST_CD_M")
            unsoDr("CUST_REF_NO") = ediDr("CUST_ORD_NO")
            unsoDr("SHIP_CD") = ediDr("SHIP_CD_L")
            unsoDr("DEST_CD") = ediDr("DEST_CD")
            unsoDr("UNSO_PKG_NB") = outkaLDr("OUTKA_PKG_NB")
            'unsoDr("NB_UT") = ediDr("NB_UT") '運送Mで取得の為ここではコメント
            unsoDr("UNSO_WT") = 0             '運送Mの集計値
            unsoDr("UNSO_ONDO_KB") = ediMDr("UNSO_ONDO_KB")
            '要望番号1476:(出荷登録時、元着払いが〔着払い〕になる(要望番号1457関連)) 2012/09/28 本明 Start
            'unsoDr("PC_KB") = ediDr("PICK_KB")
            unsoDr("PC_KB") = ediDr("PC_KB")
            '要望番号1476:(出荷登録時、元着払いが〔着払い〕になる(要望番号1457関連)) 2012/09/28 本明 End
            unsoDr("TARIFF_BUNRUI_KB") = ediDr("UNSO_TEHAI_KB")
            unsoDr("VCLE_KB") = ediDr("SYARYO_KB")
            unsoDr("MOTO_DATA_KB") = "20"
            unsoDr("TAX_KB") = "01" '課税区分は"01"(課税)固定とする
            'ビーピー・カストロール追加箇所 terakawa 2012.12.19 Start
            'unsoDr("REMARK") = ediDr("UNSO_ATT")
            unsoDr("REMARK") = ediDr("REMARK")
            'ビーピー・カストロール追加箇所 terakawa 2012.12.19 Start
            unsoDr("SEIQ_TARIFF_CD") = ediDr("UNCHIN_TARIFF_CD")
            unsoDr("SEIQ_ETARIFF_CD") = ediDr("EXTC_TARIFF_CD")
            unsoDr("AD_3") = outkaLDr("DEST_AD_3")
            unsoDr("UNSO_TEHAI_KB") = ediDr("UNSO_MOTO_KB")
            unsoDr("BUY_CHU_NO") = ediDr("BUYER_ORD_NO")
            unsoDr("AREA_CD") = String.Empty
            '要望番号:1211(EDI出荷：運送登録時の中継配送フラグ) 2012/06/28 本明 Start
            'unsoDr("TYUKEI_HAISO_FLG") = String.Empty
            unsoDr("TYUKEI_HAISO_FLG") = "00"   '中継配送フラグ"00:無し"を設定
            '要望番号:1211(EDI出荷：運送登録時の中継配送フラグ) 2012/06/28 本明 End
            unsoDr("SYUKA_TYUKEI_CD") = String.Empty
            unsoDr("HAIKA_TYUKEI_CD") = String.Empty
            unsoDr("TRIP_NO_SYUKA") = String.Empty
            unsoDr("TRIP_NO_TYUKEI") = String.Empty
            unsoDr("TRIP_NO_HAIKA") = String.Empty

            'START UMANO 要望番号1302 支払運賃に伴う修正。
            If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString()) = False AndAlso _
               String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString()) = False Then

                '運送会社マスタの取得(支払請求タリフコード,支払割増タリフコード)
                ds = MyBase.CallDAC(Me._DacCom, "SelectListDataShiharaiTariff", ds)
                Dim unsocoMDr As DataRow = ds.Tables("LMH030_SHIHARAI_TARIFF").Rows(0)

                If MyBase.GetResultCount > 0 Then
                    unsoDr("SHIHARAI_TARIFF_CD") = unsocoMDr("UNCHIN_TARIFF_CD")
                    unsoDr("SHIHARAI_ETARIFF_CD") = unsocoMDr("EXTC_TARIFF_CD")
                End If

            End If
            'END UMANO 要望番号1302 支払運賃に伴う修正。
        Else
            'まとめ処理
            Dim matomeDr As DataRow = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(0)
            unsoDr("NRS_BR_CD") = matomeDr("NRS_BR_CD")
            unsoDr("UNSO_NO_L") = matomeDr("UNSO_NO_L")
            unsoDr("SYS_UPD_DATE") = matomeDr("SYS_UNSO_UPD_DATE")
            unsoDr("SYS_UPD_TIME") = matomeDr("SYS_UNSO_UPD_TIME")

            '運送梱包個数の計算
            Dim unsoPkgNb As Long = 0
            Dim matomesakiUnsoPkgNb As Long = Convert.ToInt64(matomeDr("UNSO_PKG_NB"))
            Dim matomesakiOutkaPkgNb As Long = Convert.ToInt64(matomeDr("OUTKA_PKG_NB"))

            unsoDr("UNSO_PKG_NB") = Convert.ToInt64(outkaLDr("OUTKA_PKG_NB")) + matomesakiUnsoPkgNb - matomesakiOutkaPkgNb

        End If
        'データセットに設定
        ds.Tables("LMH030_UNSO_L").Rows.Add(unsoDr)

        Return ds

    End Function

#End Region

#Region "データセット設定(運送M)"
    ''' <summary>
    ''' データセット設定(運送M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetUnsoM(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow
        Dim unsoMDr As DataRow
        Dim dt As DataTable = ds.Tables("LMH030_OUTKAEDI_M")
        Dim unsoLDr As DataRow = ds.Tables("LMH030_UNSO_L").Rows(0)

        Dim stdWtKgs As Decimal = 0
        Dim irime As Decimal = 0
        Dim stdIrimeNb As Decimal = 0
        Dim nisugata As Decimal = 0
        Dim outkaTtlNb As Decimal = 0

        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max

            ediDr = ds.Tables("LMH030_OUTKAEDI_M").Rows(i)
            unsoMDr = ds.Tables("LMH030_UNSO_M").NewRow()

            unsoMDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            unsoMDr("UNSO_NO_L") = unsoLDr("UNSO_NO_L")
            unsoMDr("UNSO_NO_M") = ediDr("OUTKA_CTL_NO_CHU")
            unsoMDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            unsoMDr("GOODS_NM") = ediDr("GOODS_NM")
            unsoMDr("UNSO_TTL_NB") = ediDr("OUTKA_PKG_NB")
            unsoMDr("NB_UT") = ediDr("KB_UT")
            unsoMDr("UNSO_TTL_QT") = ediDr("OUTKA_TTL_QT")
            unsoMDr("QT_UT") = ediDr("QT_UT")
            unsoMDr("HASU") = ediDr("OUTKA_HASU")
            unsoMDr("ZAI_REC_NO") = String.Empty
            unsoMDr("UNSO_ONDO_KB") = ediDr("UNSO_ONDO_KB")
            unsoMDr("IRIME") = ediDr("IRIME")
            unsoMDr("IRIME_UT") = ediDr("IRIME_UT")

            stdWtKgs = Convert.ToDecimal(ediDr("STD_WT_KGS"))
            irime = Convert.ToDecimal(ediDr("IRIME"))
            stdIrimeNb = Convert.ToDecimal(ediDr("STD_IRIME_NB"))

            If String.IsNullOrEmpty(ediDr("NISUGATA").ToString()) = False Then
                nisugata = Convert.ToDecimal(ediDr("NISUGATA"))
            End If
            outkaTtlNb = Convert.ToDecimal(ediDr("OUTKA_TTL_NB"))

            If ediDr("TARE_YN").Equals("01") = False Then
                unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb)

            Else
                unsoMDr("BETU_WT") = (stdWtKgs * irime / stdIrimeNb + nisugata)

            End If

            unsoMDr("SIZE_KB") = String.Empty
            unsoMDr("ZBUKA_CD") = String.Empty
            unsoMDr("ABUKA_CD") = String.Empty
            unsoMDr("PKG_NB") = ediDr("PKG_NB")
            unsoMDr("LOT_NO") = ediDr("LOT_NO")
            unsoMDr("REMARK") = ediDr("REMARK")

            'データセットに設定
            ds.Tables("LMH030_UNSO_M").Rows.Add(unsoMDr)
        Next

        Return ds

    End Function

#End Region

#Region "データセット設定(運送L：運送重量)"
    ''' <summary>
    ''' データセット設定(運送L：運送重量)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetdatasetUnsoJyuryo(ByVal ds As DataSet, ByVal matomeFlg As Boolean) As DataSet

        Dim unsoLDr As DataRow = ds.Tables("LMH030_UNSO_L").Rows(0)
        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(0)
        Dim unsoJyuryo As Decimal = 0
        Dim matomeUnsoJyuryo As Decimal = 0

        'まとめ(運送Mデータの運送重量合算)
        If matomeFlg = True Then

            'まとめ先の中データ取得
            ds = MyBase.CallDAC(Me._DacCom, "SelectUnsoMatomeTarget", ds)
            If MyBase.GetResultCount = 0 Then
                '2013.07.05 修正START
                'unsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_UNSO_M")
                unsoJyuryo = Me.SetCalcBpRcvJyuryo(ds, "LMH030_OUTKAEDI_M")
                '2013.07.05 修正END
                unsoLDr("UNSO_WT") = Math.Ceiling(unsoJyuryo)
                unsoLDr("NB_UT") = ediMDr("KB_UT")
                Return ds

            Else
                '2013.07.05 修正START
                'matomeUnsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_MATOME_UNSO_M")
                matomeUnsoJyuryo = Convert.ToDecimal(unsoLDr("UNSO_WT"))
                'unsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_UNSO_M")
                unsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_OUTKAEDI_M")
                '2013.07.05 修正END
                unsoLDr("UNSO_WT") = Math.Ceiling(matomeUnsoJyuryo + unsoJyuryo)

                Return ds

            End If

        Else
            '2013.07.05 修正START
            'unsoJyuryo = Me.SetCalcJyuryo(ds, "LMH030_UNSO_M")
            unsoJyuryo = Me.SetCalcBpRcvJyuryo(ds, "LMH030_OUTKAEDI_M")
            '2013.07.05 修正END
            unsoLDr("UNSO_WT") = Math.Ceiling(unsoJyuryo)
            unsoLDr("NB_UT") = ediMDr("KB_UT")

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 運送重量再計算処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetCalcJyuryo(ByVal ds As DataSet, ByVal tblNm As String) As Decimal

        Dim unsoMDr As DataRow
        Dim unsoJyuryo As Decimal = 0
        Dim NB As Decimal = 0
        Dim max As Integer = ds.Tables(tblNm).Rows.Count - 1

        For i As Integer = 0 To max

            unsoMDr = ds.Tables(tblNm).Rows(i)

            '運送M個数の算出（梱数 * 入数 + 端数）
            NB = Convert.ToDecimal(unsoMDr("UNSO_TTL_NB")) * Convert.ToDecimal(unsoMDr("PKG_NB")) + Convert.ToDecimal(unsoMDr("HASU"))

            unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT")) * NB + unsoJyuryo

        Next

        Return unsoJyuryo

    End Function

    '2013.07.05 追加START
    ''' <summary>
    ''' 運送重量再計算処理(変更前：EDI出荷(中)FREE_N06→運送(大)UNSO_WT)→(変更後：EDI出荷(中)FREE_N07→運送(大)UNSO_WT)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetCalcBpRcvJyuryo(ByVal ds As DataSet, ByVal tblNm As String) As Decimal

        Dim ediMDr As DataRow
        Dim unsoJyuryo As Decimal = 0
        Dim totalWt As Decimal = 0
        Dim max As Integer = ds.Tables(tblNm).Rows.Count - 1

        For i As Integer = 0 To max

            ediMDr = ds.Tables(tblNm).Rows(i)

            '(2014.01.22)要望番号2149 修正START
            'TOTAL_WT→TOTAL_QTに値を変更
            '運送L運送重量の算出
            'unsoJyuryo = Convert.ToDecimal(ediMDr("FREE_N06")) + unsoJyuryo
            unsoJyuryo = Convert.ToDecimal(ediMDr("FREE_N07")) + unsoJyuryo
            '(2014.01.22)要望番号2149 修正END

        Next

        Return unsoJyuryo

    End Function
    '2013.07.05 追加END

#End Region

#Region "データセット設定(EDI出荷M：運送重量必要項目)"
    ''' <summary>
    ''' データセット設定(EDI出荷M：運送重量必要項目)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetEdiMUnsoJyuryo(ByVal ds As DataSet, ByVal setDs As DataSet, ByVal count As Integer, _
                                              ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim ediMDr As DataRow = ds.Tables("LMH030_OUTKAEDI_M").Rows(count)
        Dim mGoodsDr As DataRow = setDs.Tables("LMH030_M_GOODS").Rows(0)
        Dim drJudge As DataRow

        '標準重量
        ediMDr("STD_WT_KGS") = mGoodsDr("STD_WT_KGS")
        '標準入目
        ediMDr("STD_IRIME_NB") = mGoodsDr("STD_IRIME_NB")
        '風袋加算フラグ
        ediMDr("TARE_YN") = mGoodsDr("TARE_YN")

        '荷姿(区分マスタ)
        drJudge = ds.Tables("LMH030_JUDGE").Rows(0)
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N001
        drJudge("KBN_CD") = ediMDr("PKG_UT")

        ds = MyBase.CallDAC(Me._DacCom, "SelectDataPkgUtZkbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH030BLC.GUIDANCE_KBN, "E326", New String() {"包装単位", "区分マスタ"}, rowNo, LMH030BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        Dim zkbnDr As DataRow = ds.Tables("LMH030_Z_KBN").Rows(0)
        '風袋重量
        ediMDr("NISUGATA") = zkbnDr("NISUGATA")

        Return True

    End Function

#End Region

#End Region

#Region "まとめ先複数件の時出荷管理番号取得"

    ''' <summary>
    ''' まとめ先出荷管理番号の取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function matomesakiOutkaNo(ByVal ds As DataSet) As String

        Dim max As Integer = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows.Count - 1
        Dim concatOutkaNo As String = String.Empty
        Dim matomeOutkaNo As String = String.Empty

        For i As Integer = 0 To max

            'まとめ先出荷管理番号の取得
            matomeOutkaNo = ds.Tables("LMH030_MATOMESAKI_EDIL").Rows(i)("OUTKA_CTL_NO").ToString
            If i = 0 Then
                concatOutkaNo = matomeOutkaNo
            ElseIf i > 0 Then
                concatOutkaNo = String.Concat(concatOutkaNo, ",", matomeOutkaNo)
            End If

        Next

        Return concatOutkaNo

    End Function


#End Region

#Region "出荷登録処理(運賃作成)"

    ''' <summary>
    ''' 出荷登録処理(運賃作成)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UnchinSakusei(ByVal ds As DataSet) As DataSet

        '運賃の新規作成
        ds = MyBase.CallDAC(Me._Dac, "InsertUnchinData", ds)

        Return ds

    End Function

#End Region

#Region "紐付け処理"
    ''' <summary>
    ''' 紐付け処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Himoduke(ByVal ds As DataSet) As DataSet

        '紐付けフラグの設定
        ds = Me.SetHimodukeFlg(ds)

        '受信DTLデータセット
        ds = Me.SetDatasetEdiRcvDtl(ds)

        '受信HEDデータセット
        ds = Me.SetDatasetEdiRcvHed(ds)

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiLData", ds)
        If MyBase.GetResultCount = 0 Then
            Return ds
        End If

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaEdiMData", ds)

        'EDI受信(HED)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvHedData", ds)

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiRcvDtlData", ds)

        Return ds
    End Function

    ''' <summary>
    ''' 紐付けフラグの設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetHimodukeFlg(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMH030_OUTKAEDI_L").Rows(0)

        dr.Item("MATCHING_FLAG") = "01"

        Return ds

    End Function

#End Region

#End Region

End Class
