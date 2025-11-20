' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF810BLC : 支払データ生成メイン
'  作  成  者       :  YANAI
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMF810BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF810BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"

    'データセットテーブル名
    Private Const TABLE_NM_IN As String = "LMF810IN"
    Private Const TABLE_NM_OUT As String = "LMF810OUT"
    Private Const TABLE_NM_RESULT As String = "LMF810RESULT"
    Private Const TABLE_NM_SHIHARAI As String = "F_SHIHARAI_TRS"
    Private Const TABLE_NM_UNSO_L As String = "LMF810PRE_UNSO_L"
    Private Const TABLE_NM_UNSO_M As String = "LMF810PRE_UNSO_M"
    Private Const TABLE_NM_YOKO_HD As String = "LMF810M_YOKO_TARIFF_HD_SHIHARAI"
    Private Const TABLE_NM_TARIFF_LATEST As String = "LMF810UNCHIN_TARIFF_LATEST_REC"
    Private Const TABLE_NM_REV_UNIT As String = "LMF810GOODS_INFO_REV_UNIT"

    Private Const TABLE_NM_CALC_IN As String = "LMF810UNCHIN_CALC_IN"
    Private Const TABLE_NM_CALC_OUT As String = "LMF810UNCHIN_CALC_OUT"
    Private Const TABLE_NM_CALC_E_SHIHARAI As String = "LMF810M_EXTC_SHIHARAI"
    Private Const TABLE_NM_CALC_SYUHAI As String = "LMF810M_SYUHAI_SET"
    Private Const TABLE_NM_CALC_KYORI As String = "LMF810M_UNCHIN_KYORI"
    Private Const TABLE_NM_CALC_WEIGHT As String = "LMF810M_UNCHIN_WT"
    Private Const TABLE_NM_CALC_YOKO_HD As String = "LMF810M_YOKO_TARIFF_HD_CALC"
    Private Const TABLE_NM_CALC_YOKO_DTL As String = "LMF810M_YOKO_TARIFF_DTL_CALC"
    Private Const TABLE_NM_UNCHIN_CALC_IN As String = "LMF810UNCHIN_CALC_IN"

    '2013.07.04 追加START
    Private Const TABLE_NM_RCV_BP As String = "RCV_INOUT_BP"
    '2013.07.04 追加END

    'DACアクセス
    Private Const ACT_GET_UNSOL As String = "GetUnsoL"
    Private Const ACT_GET_UNSOM As String = "GetUnsoM"
    Private Const ACT_GET_GOODS_CUST As String = "GetGoodsCustInfo"
    Private Const ACT_GET_GOODS_CUST2 As String = "GetGoodsCustInfoSecond"
    Private Const ACT_GET_KYORI_DEST As String = "GetKyoriDestMst"
    Private Const ACT_GET_KYORI_MST As String = "GetKyoriKyoriMst"
    Private Const ACT_GET_TABLE_TYPE As String = "GetTableType"
    Private Const ACT_GET_UNSOM_GROUP As String = "GetUnsoMGroup"
    Private Const ACT_GET_YOKO_TARIFF_HD As String = "GetYokoTariffHd"
    Private Const ACT_GET_SEIQ_GROUP_COUNT As String = "GetSeiqGroupCount"
    Private Const ACT_GET_SPECIFIC_CUST_COUNT As String = "GetSpecificCustCount"
    Private Const ACT_GET_UNSOM_REV_UNIT As String = "GetUnsoMRevUnit"

    Private Const ACT_GET_DEST_JIS As String = "GetDestJis"
    Private Const ACT_GET_TARIFF_BASE As String = "GetTariffBase"
    Private Const ACT_GET_TARIFF_WEIGHT As String = "GetTariffWeight"
    Private Const ACT_GET_EXTC_SHUHAI As String = "GetExtcShuhai"
    Private Const ACT_GET_EXTC_SHIHARAI As String = "GetExtcShiharai"
    Private Const ACT_GET_YOKO_HD As String = "GetYokoTariffHead"
    Private Const ACT_GET_YOKO_DTL As String = "GetYokoTariffDetail"
    Private Const ACT_GET_ADJUST_CUST As String = "GetAdjustCust"
    Private Const ACT_GET_SHIHARAI_TARIFF_COUNT As String = "GetShiharaiTariffCountByNrsBrCd"

    '2013.07.04 追加START
    Private Const ACT_GET_RCV_BP_UNSOWT As String = "GetBpUNsoWt"
    '2013.07.04 追加START

    'NOTES 1911再修正(レスポンス向上,運送対応,重量個数乗算)対応 開始
    Private Const ACT_GET_ALCTD_KB As String = "GetAlctd_Kb"
    'NOTES 1911再修正(レスポンス向上,運送対応,重量個数乗算)対応 終了

    '[F_UNSO_L]元データ区分
    Private Const MOTO_NYUKA As String = "10"
    Private Const MOTO_SYUKA As String = "20"
    Private Const MOTO_UNSOU As String = "40"

    'NOTES 1911再修正(レスポンス向上,運送対応,重量個数乗算)対応 開始
    '[C_OUTKA_M]引当単位区分
    Private Const ALCTD_KOSU As String = "01"
    Private Const ALCTD_SURYO As String = "02"
    Private Const ALCTD_KOWAKW As String = "03"
    Private Const ALCTD_SAMPLE As String = "04"
    'NOTES 1911再修正(レスポンス向上,運送対応,重量個数乗算)対応 終了

    '[F_UNSO_L]運送手配区分
    Private Const UNSO_TEHAI_NRS As String = "10"

    '[F_UNSO_L]タリフ分類区分
    Private Const TARIFF_BUNRUI_KONSAI As String = "10"
    Private Const TARIFF_BUNRUI_SHAATU As String = "20"
    Private Const TARIFF_BUNRUI_TOKBIN As String = "30"
    Private Const TARIFF_BUNRUI_YKMOTI As String = "40"
    Private Const TARIFF_BUNRUI_NYUTYA As String = "50"

    '運送Ｍ取得単位
    Private Const LVL_UNSOU As String = "01"
    Private Const LVL_TAKYU As String = "02"

    '[M_SHIHARAI_TARIFF]テーブルタイプ
    Private Const TABLE_TYPE_D_WEIGHT As String = "00"
    Private Const TABLE_TYPE_D_SHASHU As String = "01"
    Private Const TABLE_TYPE_D_KODATE As String = "02"
    Private Const TABLE_TYPE_D_JYDATE As String = "03"
    Private Const TABLE_TYPE_D_SUDATE As String = "04"
    Private Const TABLE_TYPE_K_KODATE As String = "05"
    Private Const TABLE_TYPE_K_TAKKYU As String = "06"
    Private Const TABLE_TYPE_K_JYDATE As String = "07"
    Private Const TABLE_TYPE_J_WEIGHT As String = "08"
    Private Const TABLE_TYPE_J_KODATE As String = "09"

    '[M_CUST]運賃計算締め基準
    Private Const UNCHIN_CALC_KBN_SHUKKA As String = "01"
    Private Const UNCHIN_CALC_KBN_NOUNYU As String = "02"

    '[M_YOKO_TARIFF_HD]横持ちタリフ計算コード区分
    Private Const YOKO_CALC_KBN_SUGATA As String = "01"
    Private Const YOKO_CALC_KBN_KURUMA As String = "02"
    Private Const YOKO_CALC_KBN_TEIZOU As String = "03"
    Private Const YOKO_CALC_KBN_WEIGHT As String = "04"

    '[M_EXTC_SHIHARAI]冬期割増有無
    Private Const EXTC_WINT_A As String = "01"
    Private Const EXTC_WINT_B As String = "02"

    '[M_EXTC_SHIHARAI]都市割増有無
    Private Const EXTC_CITY_A As String = "01"
    Private Const EXTC_CITY_B As String = "02"

    '[M_EXTC_SHIHARAI]航送割増有無
    Private Const EXTC_FRRY_10KG As String = "01"
    Private Const EXTC_FRRY_RECORD As String = "02"

    '[M_EXTC_SHIHARAI]中継割増有無
    Private Const EXTC_RELY_NOMAL As String = "01"
    Private Const EXTC_RELY_DOUBLE As String = "02"

    '[LMF810RESULT]結果ステータス
    Private Const RESULT_NOMAL As String = "00"
    Private Const RESULT_WAR_APPLI As String = "05"
    Private Const RESULT_ERR_PARAM As String = "10"
    Private Const RESULT_ERR_APPLI As String = "20"
    Private Const RESULT_ERR_ZERO As String = "30"
    Private Const RESULT_ERR_SYSTEM As String = "99"

    '有無フラグ
    Private Const FLG_OFF As String = "00"
    Private Const FLG_ON As String = "01"

    '部品
    Private Const INIT_KBN As String = "00"
    Private Const INIT_NUM As Integer = 0
    Private Const INIT_DOU As Double = 0.0

#End Region 'Const

#Region "Field"

    Private _dac As LMF810DAC = New LMF810DAC()

    Private _errStatus As String
    Private _id As String
    Private _msg As String

    Private _unsoM As String

    Private _detailCount As Integer

    Private _yokoSplitUmu As String

    Private _kyoriRow As Integer        '距離列格納変数

    Private _weightLine As Integer      '重量行格納変数

#End Region 'Field

#Region " ■ Method [共通]"

#Region " ○0-1 Utility"

    ''' <summary>
    ''' DACクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DacAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallDAC(Me._dac, actionId, ds)

    End Function

    ''' <summary>
    ''' メッセージセット
    ''' </summary>
    ''' <param name="status">ｽﾃｰﾀｽ</param>
    ''' <param name="id">ﾒｯｾｰｼﾞID</param>
    ''' <param name="prm">ﾒｯｾｰｼﾞﾊﾟﾗﾒｰﾀ</param>
    ''' <returns>False：パラーメータエラー</returns>
    ''' <remarks>メッセージをプライベート変数に格納する</remarks>
    Private Function SetMsgInfo(ByVal status As String, ByVal id As String, ByVal prm As String) As Boolean

        Me._errStatus = status
        Me._id = id
        Me._msg = prm

        Return False

    End Function

    ''' <summary>
    ''' 返却用DataTableにﾒｯｾｰｼﾞを設定
    ''' </summary>
    ''' <param name="dt">返却用DataTable</param>
    ''' <remarks>起動元機能からのパラメータ内容を確認する</remarks>
    Private Sub SetResultMsg(ByVal dt As DataTable)

        dt.Rows(0).Item("STATUS") = Me._errStatus
        dt.Rows(0).Item("ERROR_CD") = Me._id
        dt.Rows(0).Item("YOBI1") = Me._msg

    End Sub

#End Region 'Utility

#End Region '共通処理

#Region " ■ Method [全体]"

#Region " ●1 Method [運賃データ生成]"

#Region " ○1-1 Main Method"

    ''' <summary>
    ''' 運賃データ生成メイン
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Public Function CreateUnchinData(ByVal ds As DataSet) As DataSet

        ' 入力パラメタ格納テーブル
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_IN)

        ' 初期値取得
        Me.InitItemUnchinCreate(ds)

        ' 運送Ｌ情報取得
        If Me.GetUnsoLInfo(ds) = False Then
            Me.SetResultMsg(ds.Tables(TABLE_NM_RESULT))
            Return ds
        End If

        ' 初期運賃レコード生成
        Me.SetInitForm(ds)

        ' 入力パラメータ整合チェック
        If Me.ChkInParam(ds) = False Then
            Me.SetResultMsg(ds.Tables(TABLE_NM_RESULT))
            Return ds
        End If

        ' タリフ分類区分により制御を切り分け
        Select Case ds.Tables(TABLE_NM_UNSO_L).Rows(0).Item("TARIFF_BUNRUI_KB").ToString
            Case TARIFF_BUNRUI_KONSAI _
               , TARIFF_BUNRUI_SHAATU _
               , TARIFF_BUNRUI_TOKBIN

                If Me.SetNomal(ds) = False Then
                    Me.SetResultMsg(ds.Tables(TABLE_NM_RESULT))
                    Return ds
                End If

            Case TARIFF_BUNRUI_YKMOTI

                If Me.SetYoko(ds) = False Then
                    Me.SetResultMsg(ds.Tables(TABLE_NM_RESULT))
                    Return ds
                End If

            Case TARIFF_BUNRUI_NYUTYA

                If Me.SetNyukaTyaku(ds) = False Then
                    Me.SetResultMsg(ds.Tables(TABLE_NM_RESULT))
                    Return ds
                End If

        End Select

        ' 最低保証料設定処理 (横持ち)
        Me.SetYokoMinUnchin(ds)

        Me.SetMsgInfo(RESULT_NOMAL, "G002", "")
        Me.SetResultMsg(ds.Tables(TABLE_NM_RESULT))

        ' Out用DataTable以外は初期化
        Me.ClearParamToOutput(ds)

        Return ds

    End Function

#End Region 'Main Method

#Region " ○1-2 Sub Method"

    ''' <summary>
    ''' 一般運賃生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True：正常 ／ False：異常（データ生成失敗）</returns>
    ''' <remarks></remarks>
    Private Function SetNomal(ByVal ds As DataSet) As Boolean

        Dim refTariffTableType As String = ""
        '●テーブルタイプ取得（ 参照先：運賃タリフ ）
        If Me.GetTableTypeFromTariff(ds) = False Then
            refTariffTableType = LVL_UNSOU

        Else
            '　[運賃タリフＭ]テーブルタイプより "一般／宅急便" 判断を行う
            Select Case ds.Tables(TABLE_NM_TARIFF_LATEST).Rows(0).Item("TABLE_TP").ToString
                Case TABLE_TYPE_K_TAKKYU
                    refTariffTableType = LVL_TAKYU
                Case Else
                    refTariffTableType = LVL_UNSOU
            End Select

        End If

        '●運送Ｍ情報の取得
        If Me.GetUnsoMInfo(ds, refTariffTableType) = False Then
            Return False
        End If


        '●(一般用)運賃分割有無判断
        Me.SetCreateRecCount(ds, refTariffTableType)


        For idx As Integer = 0 To Me._detailCount - 1

            '[初期運賃レコードの削除]
            If idx = 0 Then
                ds.Tables(TABLE_NM_SHIHARAI).Clear()
            End If


            '[共通処理]
            Me._unsoM = ds.Tables(TABLE_NM_UNSO_M).Rows(idx).Item("UNSO_NO_M").ToString
            If Me.CommonMethod(ds) = False Then
                Return False
            End If


            '[必要情報の取得]
            If Me.GetUnchinAttribute(ds) = False Then
                Return False
            End If


            '[運賃レコード構成]
            Me.SetNomalUnchinForm(ds, idx, refTariffTableType)


            '[運賃情報の取得]
            Me.GetUnchinRec(ds, idx)

            If MyBase.IsMessageExist() Then
                Return False
            Else
                Me.SetUnchinInfo(ds, idx)
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 横持ち運賃生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True：正常 ／ False：異常（データ生成失敗）</returns>
    ''' <remarks></remarks>
    Private Function SetYoko(ByVal ds As DataSet) As Boolean

        '●横持ちタリフヘッダ取得
        Me.GetYokoTariffHd(ds)

        '●運送Ｍ情報の取得
        If Me.GetUnsoMInfo(ds, LVL_UNSOU) = False Then
            Return False
        End If


        '●(横持ち用)運賃分割有無判断
        Me.SetYokoCreateCount(ds)


        '●横持ち運賃生成
        For idx As Integer = 0 To Me._detailCount - 1

            '[初期運賃レコードの削除]
            If idx = 0 Then
                ds.Tables(TABLE_NM_SHIHARAI).Clear()
            End If


            '[共通処理]
            Me._unsoM = ds.Tables(TABLE_NM_UNSO_M).Rows(idx).Item("UNSO_NO_M").ToString
            If Me.CommonMethod(ds) = False Then
                Return False
            End If


            '[運賃レコード構成]
            Me.SetYokoUnchinForm(ds, idx)


            '[運賃情報の取得]
            Me.GetYokoUnchin(ds, idx)
            If MyBase.IsMessageExist() Then
                Return False
            Else
                Me.SetUnchinInfo(ds, idx)
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 入荷着払い運賃生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True：正常 ／ False：異常（データ生成失敗）</returns>
    ''' <remarks></remarks>
    Private Function SetNyukaTyaku(ByVal ds As DataSet) As Boolean

        '●運送Ｍ情報の取得
        If Me.GetUnsoMInfo(ds, LVL_UNSOU) = False Then
            Return False
        End If


        '●共通処理
        Me._unsoM = ds.Tables(TABLE_NM_UNSO_M).Rows(0).Item("UNSO_NO_M").ToString
        If Me.CommonMethod(ds) = False Then
            Return False
        End If


        '●運賃適用
        Me.SetNyukaTyakuUnchinForm(ds)

        Return True

    End Function

#End Region 'Sub Method

#Region " ○1-3 部品"

    ''' <summary>
    ''' 初期化
    ''' </summary>
    ''' <remarks>各種属性情報を初期化する。</remarks>
    Private Sub InitItemUnchinCreate(ByVal ds As DataSet)

        Me._errStatus = ""
        Me._id = ""
        Me._msg = ""

        Me._unsoM = "000"
        Me._detailCount = INIT_NUM
        Me._yokoSplitUmu = FLG_OFF

        'DataTable初期化
        Me.ClearParamToInput(ds)

        'データ格納用DataTable
        ds.Tables(TABLE_NM_OUT).Rows.Add(ds.Tables(TABLE_NM_OUT).NewRow())

        '結果格納用DataTable
        ds.Tables(TABLE_NM_RESULT).Rows.Add(ds.Tables(TABLE_NM_RESULT).NewRow())

    End Sub

    ''' <summary>
    ''' "適用日" 取得
    ''' </summary>
    ''' <param name="unsoLDr">DataRow</param>
    ''' <param name="outDr">DataRow</param>
    ''' <returns>適用日</returns>
    ''' <remarks>運賃計算パラメータ項目「適用日」を求める。</remarks>
    Private Function GetUnsoDate(ByVal unsoLDr As DataRow, ByVal outDr As DataRow) As String

        '結果返却用変数
        Dim unsoDate As String = ""

        'If outDr.Item("UNTIN_CALCULATION_KB").ToString.Equals(UNCHIN_CALC_KBN_SHUKKA) Then
        '    unsoDate = unsoLDr.Item("OUTKA_PLAN_DATE").ToString
        'ElseIf outDr.Item("UNTIN_CALCULATION_KB").ToString.Equals(UNCHIN_CALC_KBN_SHUKKA) Then
        '    unsoDate = unsoLDr.Item("ARR_PLAN_DATE").ToString
        'End If
        unsoDate = unsoLDr.Item("ARR_PLAN_DATE").ToString

        'データ取得できない場合は、システム日付を設定
        If unsoDate.Equals("") Then
            unsoDate = MyBase.GetSystemDate
        End If

        Return unsoDate

    End Function

    ''' <summary>
    ''' 一般運賃データ生成単位取得
    ''' </summary>
    ''' <param name="ds">DataRow</param>
    ''' <param name="refKbn">String</param>
    ''' <remarks>一般運賃の生成単位（レコード数）を設定する。</remarks>
    Private Sub SetCreateRecCount(ByVal ds As DataSet, ByVal refKbn As String)

        '運賃生成単位を選定する。
        'パターン（運送 or 宅急便サイズ）
        If refKbn.Equals(LVL_UNSOU) Then
            Me._detailCount = 1
        Else
            Me._detailCount = ds.Tables(TABLE_NM_UNSO_M).Rows.Count                     '貨物明細数
        End If

    End Sub

    ''' <summary>
    ''' 横持ち運賃データ生成単位取得
    ''' </summary>
    ''' <param name="ds">DataRow</param>
    ''' <remarks>横持ち運賃の生成単位（レコード数）を設定する。</remarks>
    Private Sub SetYokoCreateCount(ByVal ds As DataSet)

        If ds.Tables(TABLE_NM_YOKO_HD).Rows.Count = 0 Then
            Me._detailCount = 1
            Me._yokoSplitUmu = FLG_OFF
            Exit Sub
        End If

        Dim splitKbn As String = ds.Tables(TABLE_NM_YOKO_HD).Rows(0).Item("SPLIT_FLG").ToString     '明細分割有無
        Dim detail As Integer = ds.Tables(TABLE_NM_UNSO_M).Rows.Count                               '貨物明細数

        If splitKbn.Equals(FLG_OFF) Then
            Me._detailCount = 1
            Me._yokoSplitUmu = FLG_OFF
        Else
            Me._detailCount = detail
            Me._yokoSplitUmu = FLG_ON
        End If

    End Sub

    ''' <summary>
    ''' 四捨五入
    ''' </summary>
    ''' <param name="dValue">Double</param>
    ''' <param name="iDigits">Integer</param>
    ''' <returns>計算結果</returns>
    ''' <remarks>四捨五入を行う。</remarks>
    Private Function ToHalfAdjust(ByVal dValue As Double, ByVal iDigits As Integer) As Double

        Dim dCoef As Double = System.Math.Pow(10, iDigits)

        If dValue > 0 Then
            Return System.Math.Floor((dValue * dCoef) + 0.5) / dCoef
        Else
            Return System.Math.Ceiling((dValue * dCoef) - 0.5) / dCoef
        End If

    End Function

    ''' <summary>
    ''' 切り上げ
    ''' </summary>
    ''' <param name="dValue">Double</param>
    ''' <param name="iDigits">Integer</param>
    ''' <returns>計算結果</returns>
    ''' <remarks>切り上げを行う。</remarks>
    Private Function ToRoundUp(ByVal dValue As Double, ByVal iDigits As Integer) As Double

        Dim dCoef As Double = System.Math.Pow(10, iDigits)

        If dValue > 0 Then
            Return System.Math.Ceiling(dValue * dCoef) / dCoef
        Else
            Return System.Math.Floor(dValue * dCoef) / dCoef
        End If

    End Function

    ''' <summary>
    ''' 最低保証料設定処理 （横持ち）
    ''' </summary>
    ''' <param name="ds">起動元機能からのパラメータ(unsoL)</param>
    ''' <remarks>横持ち時、最低保証料が設定されている場合は運賃に適用する。</remarks>
    Private Sub SetYokoMinUnchin(ByVal ds As DataSet)

        ' タリフ分類区分＝横持ち 判断
        If ds.Tables(TABLE_NM_UNSO_L).Rows(0).Item("TARIFF_BUNRUI_KB").ToString.Equals(TARIFF_BUNRUI_YKMOTI) _
          AndAlso ds.Tables(TABLE_NM_YOKO_HD).Rows.Count <> 0 Then

            Dim unchinMin As Double = Convert.ToDouble(ds.Tables(TABLE_NM_YOKO_HD).Rows(0).Item("YOKOMOCHI_MIN").ToString)
            Dim unchinDt As DataTable = ds.Tables(TABLE_NM_SHIHARAI)

            ' 運賃レコードに登録されている金額が「最低保証料」を満たない場合は、当該値を適用する。
            For idx As Integer = 0 To unchinDt.Rows.Count - 1

                If Convert.ToDouble(unchinDt.Rows(idx).Item("SHIHARAI_UNCHIN").ToString) < unchinMin Then
                    unchinDt.Rows(idx).Item("SHIHARAI_UNCHIN") = unchinMin
                    unchinDt.Rows(idx).Item("DECI_UNCHIN") = unchinMin
                    unchinDt.Rows(idx).Item("KANRI_UNCHIN") = unchinMin
                End If

            Next

        End If

    End Sub

    ''' <summary>
    ''' パラメタ初期化処理(開始時)
    ''' </summary>
    ''' <param name="ds">結果データセット</param>
    ''' <remarks>データセットを初期化する</remarks>
    Private Sub ClearParamToInput(ByVal ds As DataSet)

        'DataSet初期化
        ds.Tables(LMF810BLC.TABLE_NM_OUT).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_RESULT).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_REV_UNIT).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_TARIFF_LATEST).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_CALC_OUT).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_SHIHARAI).Clear()

        ds.Tables(LMF810BLC.TABLE_NM_CALC_E_SHIHARAI).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_CALC_SYUHAI).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_CALC_KYORI).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_CALC_WEIGHT).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_YOKO_HD).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_CALC_YOKO_HD).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_CALC_YOKO_DTL).Clear()

    End Sub

    ''' <summary>
    ''' パラメタ初期化処理(終了時)
    ''' </summary>
    ''' <param name="ds">結果データセット</param>
    ''' <remarks>データセットを初期化する</remarks>
    Private Sub ClearParamToOutput(ByVal ds As DataSet)

        'DataSet初期化
        ds.Tables(LMF810BLC.TABLE_NM_IN).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_OUT).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_REV_UNIT).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_TARIFF_LATEST).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_UNCHIN_CALC_IN).Clear()

        ds.Tables(LMF810BLC.TABLE_NM_CALC_E_SHIHARAI).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_CALC_SYUHAI).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_CALC_KYORI).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_CALC_WEIGHT).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_YOKO_HD).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_CALC_YOKO_HD).Clear()
        ds.Tables(LMF810BLC.TABLE_NM_CALC_YOKO_DTL).Clear()

    End Sub

    ''' <summary>
    ''' タリフ－県タイプ適用判断
    ''' </summary>
    ''' <param name="ds">結果データセット</param>
    ''' <remarks>適用タリフのテーブルタイプが県属性の場合、JISの先頭２桁を適用</remarks>
    Private Sub SetKyoriForKenType(ByVal ds As DataSet)

        If ds.Tables(TABLE_NM_TARIFF_LATEST).Rows.Count = 0 Then
            Exit Sub
        End If

        Select Case ds.Tables(TABLE_NM_TARIFF_LATEST).Rows(0).Item("TABLE_TP").ToString
            Case TABLE_TYPE_K_KODATE _
               , TABLE_TYPE_K_TAKKYU _
               , TABLE_TYPE_K_JYDATE

                '県タイプの場合、JISの先頭２桁を運賃計算用の適用距離へ設定する
                Dim destJis As String = ds.Tables(TABLE_NM_OUT).Rows(0).Item("DEST_JIS_CD").ToString
                If destJis.Length > 1 Then
                    ds.Tables(TABLE_NM_OUT).Rows(0).Item("KYORI_KEN") = Convert.ToInt32(destJis.Substring(0, 2))
                End If

            Case Else
                '処理なし
        End Select

    End Sub

    ''' <summary>
    ''' (貨物毎)商品個数算出
    ''' </summary>
    ''' <param name="unsoMDr">運送Ｍ DataRow</param>
    ''' <return>計算結果商品個数</return>
    ''' <remark>商品単位の個数を算出</remark>
    Private Function GetPkgNb(ByVal unsoMDr As DataRow) As Integer

        Dim nikosu As Integer = Convert.ToInt32(unsoMDr.Item("UNSO_TTL_NB").ToString)   '荷姿個数
        Dim irisu As Integer = Convert.ToInt32(unsoMDr.Item("PKG_NB").ToString)         '入数
        Dim hasu As Integer = Convert.ToInt32(unsoMDr.Item("HASU").ToString)            '端数

        Return nikosu * irisu + hasu

    End Function
    '2013.03.27 NOTES 1911 START
    ''' <summary>
    ''' (貨物毎)商品重量算出
    ''' </summary>
    ''' <return>計算結果商品重量</return>
    ''' <remark>商品単位の重量を算出</remark>
    Private Function GetPkgWt(ByVal ds As DataSet, ByVal index As Integer) As Double

        Dim retWt As Double = INIT_DOU    '結果重量

        '2013.03.19 / NOTES 1911 (風袋重量バグ) 対応　開始
        Dim unsoLDr As DataRow = ds.Tables(TABLE_NM_UNSO_L).Rows(0)
        Dim unsoMDr As DataRow = ds.Tables(TABLE_NM_UNSO_M).Rows(index)
        '2013.03.19 / NOTES 1911 (風袋重量バグ) 対応　終了

        If unsoMDr.Item("GOODS_MST_FLG").ToString.Equals(FLG_ON) Then

            '2013.04.18 START / 修正NOTES 2023 小分けの場合、運送重量が０になってしまう。
            If MOTO_SYUKA.Equals(unsoLDr.Item("MOTO_DATA_KB").ToString) = True _
            AndAlso Convert.ToDouble(unsoMDr.Item("UNSO_TTL_QT").ToString) < Convert.ToDouble(unsoMDr.Item("IRIME").ToString) Then
                '元データが出荷でかつ、運送数量が入目より小さい場合、小分けの判断
                retWt = ToRoundUp(Convert.ToDouble(unsoMDr.Item("UNSO_TTL_QT").ToString) _
                        * Convert.ToDouble(unsoMDr.Item("STD_WT_KGS").ToString) _
                        / Convert.ToDouble(unsoMDr.Item("STD_IRIME_NB").ToString), 3)

                '2013.05.13 START / 修正NOTES 2045 入荷横持の入目不足対応
            ElseIf MOTO_NYUKA.Equals(unsoLDr.Item("MOTO_DATA_KB").ToString) = True _
            AndAlso Convert.ToDouble(unsoMDr.Item("UNSO_TTL_QT").ToString) Mod Convert.ToDouble(unsoMDr.Item("IRIME").ToString) <> 0 Then

                retWt = Convert.ToDouble(unsoMDr.Item("UNSO_TTL_QT").ToString) _
                        / Convert.ToDouble(unsoMDr.Item("STD_IRIME_NB").ToString) _
                        * Convert.ToDouble(unsoMDr.Item("STD_WT_KGS").ToString)

                '2013.05.13 END / 修正NOTES 2045 入荷横持の入目不足対応
            Else
                '2013.04.18 END / 修正NOTES 2023 小分けの場合、運送重量が０になってしまう。
                retWt = Convert.ToDouble(unsoMDr.Item("UNSO_TTL_NB").ToString) _
                        * ToRoundUp(Convert.ToDouble(unsoMDr.Item("IRIME").ToString) _
                        * Convert.ToDouble(unsoMDr.Item("STD_WT_KGS").ToString) _
                        / Convert.ToDouble(unsoMDr.Item("STD_IRIME_NB").ToString), 3)
            End If
            'retWt = Convert.ToDouble(unsoMDr.Item("UNSO_TTL_NB").ToString) _
            '        * ToRoundUp(Convert.ToDouble(unsoMDr.Item("IRIME").ToString) _
            '        * Convert.ToDouble(unsoMDr.Item("STD_WT_KGS").ToString) _
            '        / Convert.ToDouble(unsoMDr.Item("STD_IRIME_NB").ToString), 3)

            '2013.03.26 / 再修正NOTES 1911 (風袋重量バグ) 対応　開始
            If unsoLDr.Item("UNSO_TARE_YN").ToString = "01" And unsoMDr.Item("GOODS_TARE_YN").ToString = "01" Then
                Select Case unsoLDr.Item("MOTO_DATA_KB").ToString
                    Case MOTO_SYUKA
                        Select Case GetAlctd_kb(ds, index)
                            Case ALCTD_KOSU, ALCTD_SURYO
                                retWt = retWt + (Convert.ToDouble(unsoMDr.Item("TARE_WT").ToString) * Convert.ToDouble(unsoMDr.Item("UNSO_TTL_NB").ToString))
                        End Select
                    Case Else
                        retWt = retWt + (Convert.ToDouble(unsoMDr.Item("TARE_WT").ToString) * Convert.ToDouble(unsoMDr.Item("UNSO_TTL_NB").ToString))
                End Select
            End If
            '2013.03.26 / 再修正NOTES 1911 (風袋重量バグ) 対応　終了

            'END YANAI 要望番号1303 (横持ち運賃)明細分割=有りで計算すると、運賃の重量が個別重量になる

        Else
            retWt = Convert.ToDouble(unsoMDr.Item("BETU_WT").ToString)    '個別重量
        End If

        '結果を小数点第一位で切り上げ
        retWt = ToRoundUp(retWt, 0)

        Return retWt

    End Function
    '▼▼▼旧商品重量算出ファンクション▼▼▼
    ''' <summary>
    ''' (貨物毎)商品重量算出
    ''' </summary>
    ''' <param name="unsoMDr">運送Ｍ DataRow</param>
    ''' <return>計算結果商品重量</return>
    ''' <remark>商品単位の重量を算出</remark>
    Private Function GetPkgWt_OLD(ByVal unsoMDr As DataRow) As Double

        Dim retWt As Double = INIT_DOU    '結果重量

        If unsoMDr.Item("GOODS_MST_FLG").ToString.Equals(FLG_ON) Then

            'START YANAI 要望番号1303 (横持ち運賃)明細分割=有りで計算すると、運賃の重量が個別重量になる
            'retWt = Convert.ToDouble(unsoMDr.Item("STD_WT_KGS").ToString) _
            '        * Convert.ToDouble(unsoMDr.Item("IRIME").ToString) _
            '        / Convert.ToDouble(unsoMDr.Item("STD_IRIME_NB").ToString) _
            '        + Convert.ToDouble(unsoMDr.Item("TARE_WT").ToString)
            retWt = Convert.ToDouble(unsoMDr.Item("UNSO_TTL_NB").ToString) _
                    * ToRoundUp(Convert.ToDouble(unsoMDr.Item("IRIME").ToString) _
                    * Convert.ToDouble(unsoMDr.Item("STD_WT_KGS").ToString) _
                    / Convert.ToDouble(unsoMDr.Item("STD_IRIME_NB").ToString), 3) _
                    + Convert.ToDouble(unsoMDr.Item("TARE_WT").ToString)
            'END YANAI 要望番号1303 (横持ち運賃)明細分割=有りで計算すると、運賃の重量が個別重量になる

        Else
            retWt = Convert.ToDouble(unsoMDr.Item("BETU_WT").ToString)    '個別重量
        End If

        '結果を小数点第一位で切り上げ
        retWt = ToRoundUp(retWt, 0)

        Return retWt

    End Function
    '▲▲▲旧商品重量算出ファンクション▲▲▲
    '2013.03.27 NOTES 1911 END
    ''' <summary>
    ''' （一般運賃計算用）適用距離判断
    ''' </summary>
    ''' <param name="outDr">OutDataRow</param>
    ''' <param name="unchinDr">UnchinDataRow</param>
    ''' <return>適用距離</return>
    ''' <remark>一般運賃計算用パラメータ"適用距離"を判断する</remark>
    ''' 
    Private Function SetKyoriValue(ByVal outDr As DataRow, ByVal unchinDr As DataRow) As Double

        Dim retKyori As Integer = INIT_NUM    '結果距離
        Dim kyoriKen As Integer = Convert.ToInt32(outDr.Item("KYORI_KEN").ToString)

        If kyoriKen = INIT_NUM Then
            retKyori = Convert.ToInt32(unchinDr.Item("DECI_KYORI").ToString)
        Else
            retKyori = kyoriKen
        End If

        Return retKyori

    End Function

#End Region '部品

#Region " ○1-4 データ参照"

    ''' <summary>
    ''' 共通処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True：正常 ／ False：異常（データ取得不備）</returns>
    ''' <remarks></remarks>
    Private Function CommonMethod(ByVal ds As DataSet) As Boolean

        '●適用する運送Ｍ情報の特定

        '[取得レベル設定]
        ds.Tables(TABLE_NM_IN).Rows(0).Item("UNSO_NO_M") = Me._unsoM


        '●運賃データ構成（基本情報取得）

        '[データアクセス ①(商品／荷主) ]
        ds = Me.DacAccess(ds, ACT_GET_GOODS_CUST)

        If ds.Tables(TABLE_NM_OUT).Rows(0).Item("CUST_CD_S").ToString.Equals("") Then

            '[データアクセス ②(商品／荷主) ]
            ds = Me.DacAccess(ds, ACT_GET_GOODS_CUST2)

            '[データ存在チェック]
            If ds.Tables(TABLE_NM_OUT).Rows(0).Item("CUST_CD_S").ToString.Equals("") Then
                Return Me.SetMsgInfo(RESULT_ERR_APPLI, "E078", SetMessageParamIsExists(TABLE_NM_OUT))
            End If
        End If

        '●距離取得

        '[ 1.届先マスタ参照 ]
        ds = Me.DacAccess(ds, ACT_GET_KYORI_DEST)


        '[ 2.タリフ-テーブルタイプによる距離取得（県タイプ） ]
        Select Case ds.Tables(TABLE_NM_UNSO_L).Rows(0).Item("TARIFF_BUNRUI_KB").ToString
            Case TARIFF_BUNRUI_KONSAI _
               , TARIFF_BUNRUI_SHAATU _
               , TARIFF_BUNRUI_TOKBIN

                Me.SetKyoriForKenType(ds)

            Case Else
                '処理なし
        End Select


        '[ 3.距離マスタ参照 ]
        With ds.Tables(TABLE_NM_OUT).Rows(0)

            ' "距離"有無を確認
            If Convert.ToInt32(.Item("KYORI").ToString()) = 0 Then
                ' 距離マスタに対する再検索
                ds = Me.DacAccess(ds, ACT_GET_KYORI_MST)
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 運送Ｌ情報取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True：正常 ／ False：異常（データ取得不備）</returns>
    ''' <remarks>In情報として利用するため最新情報を取得</remarks>
    Private Function GetUnsoLInfo(ByVal ds As DataSet) As Boolean

        '●適用する運送Ｌ情報の取得

        '[データアクセス]
        ds = Me.DacAccess(ds, ACT_GET_UNSOL)

        Return Me.IsDataExists(ds, TABLE_NM_UNSO_L, FLG_ON, RESULT_ERR_APPLI)

    End Function

    ''' <summary>
    ''' 運送Ｍ情報取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="kbn">String</param>
    ''' <returns>True：正常 ／ False：異常（データ取得不備）</returns>
    ''' <remarks>In情報として利用するため最新情報を取得</remarks>
    Private Function GetUnsoMInfo(ByVal ds As DataSet, ByVal kbn As String) As Boolean

        '●適用する運送Ｍ情報の取得

        '[取得レベル設定]
        ds.Tables(TABLE_NM_IN).Rows(0).Item("UNSO_M_LV_KBN") = kbn

        '[データアクセス]
        ds = Me.DacAccess(ds, ACT_GET_UNSOM)

        '[データ存在チェック]
        Return Me.IsDataExists(ds, TABLE_NM_UNSO_M, FLG_ON, RESULT_ERR_APPLI)

    End Function

    ''' <summary>
    ''' 運賃構成情報取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True：正常 ／ False：異常（データ取得不備）</returns>
    ''' <remarks>運賃レコード構成に必要な情報を収集する</remarks>
    Private Function GetUnchinAttribute(ByVal ds As DataSet) As Boolean

        '[(F_UNSO_M)データアクセス]
        ds = Me.DacAccess(ds, ACT_GET_UNSOM_GROUP)

        Return True

    End Function

    ''' <summary>
    ''' テーブルタイプ 取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True：正常 ／ False：異常（データ取得不備）</returns>
    ''' <remarks>最新タリフより適用 "テーブルタイプ" を取得する</remarks>
    Private Function GetTableTypeFromTariff(ByVal ds As DataSet) As Boolean

        '[データアクセス]
        ds = Me.DacAccess(ds, ACT_GET_TABLE_TYPE)

        '[データ存在チェック]
        Return Me.IsDataExists(ds, TABLE_NM_TARIFF_LATEST, FLG_ON, RESULT_WAR_APPLI)

    End Function

    ''' <summary>
    ''' 横持ちタリフヘッダ 情報取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True：正常 ／ False：異常（データ取得不備）</returns>
    ''' <remarks>横持ちタリフヘッダ情報を取得する。</remarks>
    Private Function GetYokoTariffHd(ByVal ds As DataSet) As Boolean

        '[データアクセス]
        ds = Me.DacAccess(ds, ACT_GET_YOKO_TARIFF_HD)

        '[データ存在チェック]
        Return Me.IsDataExists(ds, TABLE_NM_YOKO_HD, FLG_ON, RESULT_WAR_APPLI)

    End Function

    ''' <summary>
    ''' 運送Ｍ情報取得（請求先単位）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True：正常 ／ False：異常（データ取得不備）</returns>
    ''' <remarks>運送Ｍ情報を請求先単位に集約する。</remarks>
    Private Function GetRevUnitUnsoM(ByVal ds As DataSet) As Boolean

        '[データアクセス]
        ds = Me.DacAccess(ds, ACT_GET_UNSOM_REV_UNIT)

        '[結果チェック（２レコード以上存在しない場合は請求先按分不要）]
        If ds.Tables(TABLE_NM_REV_UNIT).Rows.Count < 2 Then
            Return False
        End If

        Return True

    End Function

#End Region 'データ参照

#Region " ○1-5 check Method"

    ''' <summary>
    ''' 入力パラメータ整合チェック（メイン）
    ''' </summary>
    ''' <param name="ds">起動元機能からのパラメータ</param>
    ''' <returns>True：正常 ／ False：異常（パラーメータエラー）</returns>
    ''' <remarks>起動元機能からのパラメータ内容を確認する</remarks>
    Private Function ChkInParam(ByVal ds As DataSet) As Boolean

        ' クライアントチェック
        If Me.ChkInDataOnClient(ds.Tables(TABLE_NM_UNSO_L)) = False Then
            Return False
        End If

        ' サーバチェック
        Return Me.ChkInDataOnServer(ds)

    End Function

    ''' <summary>
    ''' 入力パラメータ整合チェック（属性）
    ''' </summary>
    ''' <param name="unsoLDt">起動元機能からのパラメータ(unsoL)</param>
    ''' <returns>True：正常 ／ False：異常（パラーメータエラー）</returns>
    ''' <remarks>起動元機能からのパラメータ内容を確認する</remarks>
    Private Function ChkInDataOnClient(ByVal unsoLDt As DataTable) As Boolean

        '営業所コード
        If unsoLDt.Rows(0).Item("NRS_BR_CD").ToString.Equals("") Then
            Return Me.SetMsgInfo(RESULT_ERR_PARAM, "E245", "営業所 情報未設定")
        End If

        '運送番号
        If unsoLDt.Rows(0).Item("UNSO_NO_L").ToString.Equals("") Then
            Return Me.SetMsgInfo(RESULT_ERR_PARAM, "E245", "運送番号 情報未設定")
        End If

        '荷主（大）
        If unsoLDt.Rows(0).Item("CUST_CD_L").ToString.Equals("") Then
            Return Me.SetMsgInfo(RESULT_ERR_PARAM, "E245", "荷主（大） 情報未設定")
        End If

        '荷主（中）
        If unsoLDt.Rows(0).Item("CUST_CD_M").ToString.Equals("") Then
            Return Me.SetMsgInfo(RESULT_ERR_PARAM, "E245", "荷主（中） 情報未設定")
        End If

        '課税区分
        If unsoLDt.Rows(0).Item("TAX_KB").ToString.Equals("") Then
            Return Me.SetMsgInfo(RESULT_ERR_PARAM, "E245", "課税区分 情報未設定")
        End If

        '元データ区分
        Select Case unsoLDt.Rows(0).Item("MOTO_DATA_KB").ToString
            Case MOTO_NYUKA
            Case MOTO_SYUKA
            Case MOTO_UNSOU
                '正常
            Case Else
                Return Me.SetMsgInfo(RESULT_ERR_PARAM, "E245", "元データ区分 設定値不正")
        End Select

        '運送手配区分
        Select Case unsoLDt.Rows(0).Item("UNSO_TEHAI_KB").ToString
            Case UNSO_TEHAI_NRS
                '正常
            Case Else
                Return Me.SetMsgInfo(RESULT_ERR_PARAM, "E245", "運送手配区分 設定値不正")
        End Select

        'タリフ分類区分
        Select Case unsoLDt.Rows(0).Item("TARIFF_BUNRUI_KB").ToString
            Case TARIFF_BUNRUI_KONSAI
            Case TARIFF_BUNRUI_SHAATU
            Case TARIFF_BUNRUI_TOKBIN
            Case TARIFF_BUNRUI_YKMOTI
            Case TARIFF_BUNRUI_NYUTYA
                '正常
            Case Else
                Return Me.SetMsgInfo(RESULT_ERR_PARAM, "E245", "タリフ分類区分 設定値不正")
        End Select

        Return True

    End Function

    ''' <summary>
    ''' 入力パラメータ整合チェック（Databaseチェック）
    ''' </summary>
    ''' <param name="ds">起動元機能からのパラメータ</param>
    ''' <returns>True：正常 ／ False：異常（パラーメータエラー）</returns>
    ''' <remarks>起動元機能からのパラメータ内容を確認する</remarks>
    Private Function ChkInDataOnServer(ByVal ds As DataSet) As Boolean

        '「まとめ運賃」「確定運賃」存在チェック
        ds = Me.DacAccess(ds, ACT_GET_SEIQ_GROUP_COUNT)

        If Convert.ToInt32(ds.Tables(TABLE_NM_OUT).Rows(0).Item("SEIQ_GROUP_REC_COUNT").ToString) > 0 Then
            Return Me.SetMsgInfo(RESULT_ERR_PARAM, "E307", "運賃まとめ")
        End If

        If Convert.ToInt32(ds.Tables(TABLE_NM_OUT).Rows(0).Item("DECI_REC_COUNT").ToString) > 0 Then
            Return Me.SetMsgInfo(RESULT_ERR_PARAM, "E307", "確定運賃")
        End If

        Return True

    End Function

#End Region 'check Method

#Region " ○1-6 運賃レコード構成"

    ''' <summary>
    ''' [レコード構成] 初期値
    ''' </summary>
    ''' <param name="ds">起動元機能からのパラメータ</param>
    ''' <remarks>初期値として運賃レコードを構成する（一定の処理まで確認後、Delete-Insertされる）</remarks>
    Private Sub SetInitForm(ByVal ds As DataSet)

        ' パラメータとなるテーブル
        Dim unsoLDr As DataRow = ds.Tables(TABLE_NM_UNSO_L).Rows(0)

        ' 結果格納用運賃テーブル
        Dim unchinDt As DataTable = ds.Tables(TABLE_NM_SHIHARAI)
        Dim unchinDr As DataRow = unchinDt.NewRow()

        ' 運賃設定
        With unchinDr

            .Item("YUSO_BR_CD") = unsoLDr.Item("YUSO_BR_CD")
            .Item("NRS_BR_CD") = unsoLDr.Item("NRS_BR_CD")
            .Item("UNSO_NO_L") = unsoLDr.Item("UNSO_NO_L")
            .Item("UNSO_NO_M") = Me._unsoM
            .Item("CUST_CD_L") = unsoLDr.Item("CUST_CD_L")
            .Item("CUST_CD_M") = unsoLDr.Item("CUST_CD_M")
            .Item("CUST_CD_S") = ""
            .Item("CUST_CD_SS") = ""
            .Item("SHIHARAI_GROUP_NO") = ""
            .Item("SHIHARAI_GROUP_NO_M") = ""
            .Item("SHIHARAITO_CD") = ""
            .Item("UNTIN_CALCULATION_KB") = ""
            .Item("SHIHARAI_SYARYO_KB") = unsoLDr.Item("VCLE_KB")
            .Item("SHIHARAI_PKG_UT") = unsoLDr.Item("NB_UT")
            .Item("SHIHARAI_NG_NB") = unsoLDr.Item("UNSO_PKG_NB")
            .Item("SHIHARAI_DANGER_KB") = "01"
            .Item("SHIHARAI_TARIFF_BUNRUI_KB") = unsoLDr.Item("TARIFF_BUNRUI_KB")
            .Item("SHIHARAI_TARIFF_CD") = unsoLDr.Item("SHIHARAI_TARIFF_CD")
            .Item("SHIHARAI_ETARIFF_CD") = unsoLDr.Item("SHIHARAI_ETARIFF_CD")
            .Item("SHIHARAI_KYORI") = 0
            .Item("SHIHARAI_WT") = unsoLDr.Item("UNSO_WT")
            .Item("SHIHARAI_UNCHIN") = 0
            .Item("SHIHARAI_CITY_EXTC") = 0
            .Item("SHIHARAI_WINT_EXTC") = 0
            .Item("SHIHARAI_RELY_EXTC") = 0
            .Item("SHIHARAI_TOLL") = 0
            .Item("SHIHARAI_INSU") = 0
            .Item("SHIHARAI_FIXED_FLAG") = "00"
            .Item("DECI_NG_NB") = unsoLDr.Item("UNSO_PKG_NB")
            .Item("DECI_KYORI") = 0
            .Item("DECI_WT") = unsoLDr.Item("UNSO_WT")
            .Item("DECI_UNCHIN") = 0
            .Item("DECI_CITY_EXTC") = 0
            .Item("DECI_WINT_EXTC") = 0
            .Item("DECI_RELY_EXTC") = 0
            .Item("DECI_TOLL") = 0
            .Item("DECI_INSU") = 0
            .Item("KANRI_UNCHIN") = 0
            .Item("KANRI_CITY_EXTC") = 0
            .Item("KANRI_WINT_EXTC") = 0
            .Item("KANRI_RELY_EXTC") = 0
            .Item("KANRI_TOLL") = 0
            .Item("KANRI_INSU") = 0
            .Item("REMARK") = unsoLDr.Item("CUST_REF_NO")
            .Item("SIZE_KB") = ""
            .Item("TAX_KB") = unsoLDr.Item("TAX_KB")
            .Item("SAGYO_KANRI") = ""

            '要望番号2129 修正START 2013.11.20
            '2013.07.04 修正START
            'If unsoLDr.Item("NRS_BR_CD").ToString() = "50" AndAlso _
            If unsoLDr.Item("NRS_BR_CD").ToString() = "30" AndAlso _
               unsoLDr.Item("CUST_CD_L").ToString() = "00023" AndAlso _
               unsoLDr.Item("CUST_CD_M").ToString() = "00" Then
                '要望番号2129 修正END 2013.11.20

                ds = Me.DacAccess(ds, ACT_GET_RCV_BP_UNSOWT)

                If ds.Tables(TABLE_NM_RCV_BP).Rows.Count <> 0 Then
                    ' パラメータとなるテーブル
                    Dim bpRcvDr As DataRow = ds.Tables(TABLE_NM_RCV_BP).Rows(0)

                    .Item("SHIHARAI_WT") = bpRcvDr.Item("BP_UNSO_WT")
                    .Item("DECI_WT") = bpRcvDr.Item("BP_UNSO_WT")
                End If

            End If
            '2013.07.04 修正END


        End With

        unchinDt.Rows.Add(unchinDr)

    End Sub

    ''' <summary>
    ''' [レコード構成] 一般用
    ''' </summary>
    ''' <param name="ds">起動元機能からのパラメータ</param>
    ''' <param name="index">運送Ｍの参照インデックス</param>
    ''' <param name="createLv">運賃生成レベル</param>
    ''' <remarks>「運賃属性」以外の運賃レコードを構成する。</remarks>
    Private Sub SetNomalUnchinForm(ByVal ds As DataSet, ByVal index As Integer, ByVal createLv As String)

        ' パラメータとなるテーブル
        Dim outDr As DataRow = ds.Tables(TABLE_NM_OUT).Rows(0)
        Dim unsoLDr As DataRow = ds.Tables(TABLE_NM_UNSO_L).Rows(0)
        Dim unsoMDr As DataRow = ds.Tables(TABLE_NM_UNSO_M).Rows(index)

        ' 結果格納用運賃テーブル
        Dim unchinDt As DataTable = ds.Tables(TABLE_NM_SHIHARAI)
        Dim unchinDr As DataRow = unchinDt.NewRow()

        ' 運賃設定
        With unchinDr

            .Item("YUSO_BR_CD") = unsoLDr.Item("YUSO_BR_CD")
            .Item("NRS_BR_CD") = unsoLDr.Item("NRS_BR_CD")
            .Item("UNSO_NO_L") = unsoLDr.Item("UNSO_NO_L")
            .Item("UNSO_NO_M") = Me._unsoM
            .Item("CUST_CD_L") = unsoLDr.Item("CUST_CD_L")
            .Item("CUST_CD_M") = unsoLDr.Item("CUST_CD_M")
            .Item("CUST_CD_S") = outDr.Item("CUST_CD_S")
            .Item("CUST_CD_SS") = outDr.Item("CUST_CD_SS")
            .Item("SHIHARAI_GROUP_NO") = ""
            .Item("SHIHARAI_GROUP_NO_M") = ""
            If String.IsNullOrEmpty(unsoLDr.Item("SHIHARAITO_CD").ToString) = False Then
                .Item("SHIHARAITO_CD") = unsoLDr.Item("SHIHARAITO_CD").ToString
            Else
                .Item("SHIHARAITO_CD") = String.Concat(unsoLDr.Item("UNSO_CD"), unsoLDr.Item("UNSO_BR_CD"))
            End If
            .Item("UNTIN_CALCULATION_KB") = outDr.Item("UNTIN_CALCULATION_KB")
            .Item("SHIHARAI_SYARYO_KB") = unsoLDr.Item("VCLE_KB")
            .Item("SHIHARAI_PKG_UT") = unsoLDr.Item("NB_UT")
            .Item("SHIHARAI_DANGER_KB") = ""
            .Item("SHIHARAI_TARIFF_BUNRUI_KB") = unsoLDr.Item("TARIFF_BUNRUI_KB")
            .Item("SHIHARAI_TARIFF_CD") = unsoLDr.Item("SHIHARAI_TARIFF_CD")
            .Item("SHIHARAI_ETARIFF_CD") = unsoLDr.Item("SHIHARAI_ETARIFF_CD")
            .Item("SHIHARAI_KYORI") = outDr.Item("KYORI")
            .Item("SHIHARAI_WT") = unsoLDr.Item("UNSO_WT")
            .Item("SHIHARAI_UNCHIN") = 0
            .Item("SHIHARAI_CITY_EXTC") = 0
            .Item("SHIHARAI_WINT_EXTC") = 0
            .Item("SHIHARAI_RELY_EXTC") = 0
            .Item("SHIHARAI_TOLL") = 0
            .Item("SHIHARAI_INSU") = 0
            .Item("SHIHARAI_FIXED_FLAG") = "00"
            .Item("DECI_KYORI") = outDr.Item("KYORI")
            .Item("DECI_WT") = unsoLDr.Item("UNSO_WT")
            .Item("DECI_UNCHIN") = 0
            .Item("DECI_CITY_EXTC") = 0
            .Item("DECI_WINT_EXTC") = 0
            .Item("DECI_RELY_EXTC") = 0
            .Item("DECI_TOLL") = 0
            .Item("DECI_INSU") = 0
            .Item("KANRI_UNCHIN") = 0
            .Item("KANRI_CITY_EXTC") = 0
            .Item("KANRI_WINT_EXTC") = 0
            .Item("KANRI_RELY_EXTC") = 0
            .Item("KANRI_TOLL") = 0
            .Item("KANRI_INSU") = 0
            .Item("REMARK") = unsoLDr.Item("CUST_REF_NO")
            .Item("SIZE_KB") = unsoMDr.Item("SIZE_KB")
            .Item("TAX_KB") = unsoLDr.Item("TAX_KB")
            .Item("SAGYO_KANRI") = ""

            If createLv.Equals(LVL_UNSOU) Then
                .Item("SHIHARAI_NG_NB") = unsoLDr.Item("UNSO_PKG_NB")
                .Item("DECI_NG_NB") = unsoLDr.Item("UNSO_PKG_NB")
            Else
                .Item("SHIHARAI_NG_NB") = unsoMDr.Item("UNSO_TTL_NB")
                .Item("DECI_NG_NB") = unsoMDr.Item("UNSO_TTL_NB")
            End If

            '要望番号2129 修正START 2013.11.20
            '2013.07.04 修正START
            'If unsoLDr.Item("NRS_BR_CD").ToString() = "50" AndAlso _
            If unsoLDr.Item("NRS_BR_CD").ToString() = "30" AndAlso _
               unsoLDr.Item("CUST_CD_L").ToString() = "00023" AndAlso _
               unsoLDr.Item("CUST_CD_M").ToString() = "00" Then
                '要望番号2129 修正END 2013.11.20

                ds = Me.DacAccess(ds, ACT_GET_RCV_BP_UNSOWT)

                If ds.Tables(TABLE_NM_RCV_BP).Rows.Count <> 0 Then
                    ' パラメータとなるテーブル
                    Dim bpRcvDr As DataRow = ds.Tables(TABLE_NM_RCV_BP).Rows(0)

                    .Item("SHIHARAI_WT") = bpRcvDr.Item("BP_UNSO_WT")
                    .Item("DECI_WT") = bpRcvDr.Item("BP_UNSO_WT")
                End If

            End If
            '2013.07.04 修正END

        End With

        unchinDt.Rows.Add(unchinDr)

    End Sub

    ''' <summary>
    ''' [レコード構成] 横持ち用
    ''' </summary>
    ''' <param name="ds">起動元機能からのパラメータ</param>
    ''' <param name="index">明細行</param>
    ''' <remarks>「運賃属性」以外の運賃レコードを構成する。</remarks>
    Private Sub SetYokoUnchinForm(ByVal ds As DataSet, ByVal index As Integer)

        ' パラメータとなるテーブル
        Dim outDr As DataRow = ds.Tables(TABLE_NM_OUT).Rows(0)
        Dim unsoLDr As DataRow = ds.Tables(TABLE_NM_UNSO_L).Rows(0)
        Dim unsoMDr As DataRow = ds.Tables(TABLE_NM_UNSO_M).Rows(index)

        ' 結果格納用運賃テーブル
        Dim unchinDt As DataTable = ds.Tables(TABLE_NM_SHIHARAI)
        Dim unchinDr As DataRow = unchinDt.NewRow()

        ' 運賃設定
        With unchinDr

            .Item("YUSO_BR_CD") = unsoLDr.Item("YUSO_BR_CD")
            .Item("NRS_BR_CD") = unsoLDr.Item("NRS_BR_CD")
            .Item("UNSO_NO_L") = unsoLDr.Item("UNSO_NO_L")
            .Item("UNSO_NO_M") = Me._unsoM
            .Item("CUST_CD_L") = unsoLDr.Item("CUST_CD_L")
            .Item("CUST_CD_M") = unsoLDr.Item("CUST_CD_M")
            .Item("CUST_CD_S") = outDr.Item("CUST_CD_S")
            .Item("CUST_CD_SS") = outDr.Item("CUST_CD_SS")
            .Item("SHIHARAI_GROUP_NO") = ""
            .Item("SHIHARAI_GROUP_NO_M") = ""
            If String.IsNullOrEmpty(unsoLDr.Item("SHIHARAITO_CD").ToString) = False Then
                .Item("SHIHARAITO_CD") = unsoLDr.Item("SHIHARAITO_CD").ToString
            Else
                .Item("SHIHARAITO_CD") = String.Concat(unsoLDr.Item("UNSO_CD"), unsoLDr.Item("UNSO_BR_CD"))
            End If
            .Item("UNTIN_CALCULATION_KB") = outDr.Item("UNTIN_CALCULATION_KB")
            .Item("SHIHARAI_SYARYO_KB") = unsoLDr.Item("VCLE_KB")
            .Item("SHIHARAI_DANGER_KB") = outDr.Item("KIKEN_KB")
            .Item("SHIHARAI_TARIFF_BUNRUI_KB") = TARIFF_BUNRUI_YKMOTI
            .Item("SHIHARAI_TARIFF_CD") = unsoLDr.Item("SHIHARAI_TARIFF_CD")
            .Item("SHIHARAI_ETARIFF_CD") = unsoLDr.Item("SHIHARAI_ETARIFF_CD")
            .Item("SHIHARAI_KYORI") = outDr.Item("KYORI")
            .Item("SHIHARAI_UNCHIN") = 0
            .Item("SHIHARAI_CITY_EXTC") = 0
            .Item("SHIHARAI_WINT_EXTC") = 0
            .Item("SHIHARAI_RELY_EXTC") = 0
            .Item("SHIHARAI_TOLL") = 0
            .Item("SHIHARAI_INSU") = 0
            .Item("SHIHARAI_FIXED_FLAG") = "00"
            .Item("DECI_KYORI") = outDr.Item("KYORI")
            .Item("DECI_UNCHIN") = 0
            .Item("DECI_CITY_EXTC") = 0
            .Item("DECI_WINT_EXTC") = 0
            .Item("DECI_RELY_EXTC") = 0
            .Item("DECI_TOLL") = 0
            .Item("DECI_INSU") = 0
            .Item("KANRI_UNCHIN") = 0
            .Item("KANRI_CITY_EXTC") = 0
            .Item("KANRI_WINT_EXTC") = 0
            .Item("KANRI_RELY_EXTC") = 0
            .Item("KANRI_TOLL") = 0
            .Item("KANRI_INSU") = 0
            .Item("REMARK") = unsoLDr.Item("CUST_REF_NO")
            .Item("SIZE_KB") = ""
            .Item("TAX_KB") = unsoLDr.Item("TAX_KB")
            .Item("SAGYO_KANRI") = ""

            '明細分割有無で設定値に変動のある項目
            If Me._yokoSplitUmu.Equals(FLG_ON) Then
                '分割有
                .Item("SHIHARAI_PKG_UT") = unsoMDr.Item("NB_UT")
                .Item("SHIHARAI_NG_NB") = GetPkgNb(unsoMDr)
                .Item("DECI_NG_NB") = GetPkgNb(unsoMDr)

                '2013.03.18 / NOTES1911 風袋重量バグ 対応 開始
                '.Item("SHIHARAI_WT") = GetPkgWt(unsoMDr)
                '.Item("DECI_WT") = GetPkgWt(unsoMDr)
                .Item("SHIHARAI_WT") = GetPkgWt(ds, index) 'オプションでDSまるごと + index
                .Item("DECI_WT") = GetPkgWt(ds, index) 'オプションでDSまるごと + index
                '2013.03.18 / NOTES1911 風袋重量バグ 対応 終了
            Else
                '分割無
                .Item("SHIHARAI_PKG_UT") = unsoLDr.Item("NB_UT")
                .Item("SHIHARAI_NG_NB") = unsoLDr.Item("UNSO_PKG_NB")
                .Item("DECI_NG_NB") = unsoLDr.Item("UNSO_PKG_NB")
                .Item("SHIHARAI_WT") = unsoLDr.Item("UNSO_WT")
                .Item("DECI_WT") = unsoLDr.Item("UNSO_WT")

                '要望番号2129 修正START 2013.11.20
                '2013.07.04 修正START
                'If unsoLDr.Item("NRS_BR_CD").ToString() = "50" AndAlso _
                If unsoLDr.Item("NRS_BR_CD").ToString() = "30" AndAlso _
                   unsoLDr.Item("CUST_CD_L").ToString() = "00023" AndAlso _
                   unsoLDr.Item("CUST_CD_M").ToString() = "00" Then
                    '要望番号2129 修正END 2013.11.20

                    ds = Me.DacAccess(ds, ACT_GET_RCV_BP_UNSOWT)

                    If ds.Tables(TABLE_NM_RCV_BP).Rows.Count <> 0 Then
                        ' パラメータとなるテーブル
                        Dim bpRcvDr As DataRow = ds.Tables(TABLE_NM_RCV_BP).Rows(0)

                        .Item("SHIHARAI_WT") = bpRcvDr.Item("BP_UNSO_WT")
                        .Item("DECI_WT") = bpRcvDr.Item("BP_UNSO_WT")
                    End If

                End If
                '2013.07.04 修正END

            End If

        End With

        unchinDt.Rows.Add(unchinDr)

    End Sub

    ''' <summary>
    ''' [レコード構成] 一般/横持ち共用 (運賃設定)
    ''' </summary>
    ''' <param name="ds">起動元機能からのパラメータ</param>
    ''' <param name="index">明細行</param>
    ''' <remarks>「運賃属性」を設定する。</remarks>
    Private Sub SetUnchinInfo(ByVal ds As DataSet, ByVal index As Integer)

        ' 運賃計算結果格納テーブル
        Dim resultDr As DataRow = ds.Tables(TABLE_NM_CALC_OUT).Rows(0)

        ' 結果格納用運賃テーブル
        Dim unchinDr As DataRow = ds.Tables(TABLE_NM_SHIHARAI).Rows(index)

        ' 運賃設定
        With unchinDr

            .Item("SHIHARAI_UNCHIN") = resultDr.Item("UNCHIN").ToString
            .Item("SHIHARAI_CITY_EXTC") = resultDr.Item("CITY_EXTC").ToString
            .Item("SHIHARAI_WINT_EXTC") = resultDr.Item("WINT_EXTC").ToString
            .Item("SHIHARAI_RELY_EXTC") = resultDr.Item("RELY_EXTC").ToString
            .Item("SHIHARAI_TOLL") = resultDr.Item("TOLL").ToString
            .Item("SHIHARAI_INSU") = resultDr.Item("INSU").ToString

            .Item("DECI_UNCHIN") = resultDr.Item("UNCHIN").ToString
            .Item("DECI_CITY_EXTC") = resultDr.Item("CITY_EXTC").ToString
            .Item("DECI_WINT_EXTC") = resultDr.Item("WINT_EXTC").ToString
            .Item("DECI_RELY_EXTC") = resultDr.Item("RELY_EXTC").ToString
            .Item("DECI_TOLL") = resultDr.Item("TOLL").ToString
            .Item("DECI_INSU") = resultDr.Item("INSU").ToString

            .Item("KANRI_UNCHIN") = resultDr.Item("UNCHIN").ToString
            .Item("KANRI_CITY_EXTC") = resultDr.Item("CITY_EXTC").ToString
            .Item("KANRI_WINT_EXTC") = resultDr.Item("WINT_EXTC").ToString
            .Item("KANRI_RELY_EXTC") = resultDr.Item("RELY_EXTC").ToString
            .Item("KANRI_TOLL") = resultDr.Item("TOLL").ToString
            .Item("KANRI_INSU") = resultDr.Item("INSU").ToString

        End With

    End Sub

    ''' <summary>
    ''' [レコード構成] 入荷着払い用
    ''' </summary>
    ''' <param name="ds">起動元機能からのパラメータ</param>
    ''' <remarks>「入荷着払い」時の運賃レコードを構成する。</remarks>
    Private Sub SetNyukaTyakuUnchinForm(ByVal ds As DataSet)

        '[初期運賃レコードの削除]
        ds.Tables(TABLE_NM_SHIHARAI).Clear()

        ' パラメータとなるテーブル
        Dim inDr As DataRow = ds.Tables(TABLE_NM_IN).Rows(0)
        Dim outDr As DataRow = ds.Tables(TABLE_NM_OUT).Rows(0)
        Dim unsoLDr As DataRow = ds.Tables(TABLE_NM_UNSO_L).Rows(0)

        ' 結果格納用運賃テーブル
        Dim unchinDt As DataTable = ds.Tables(TABLE_NM_SHIHARAI)
        Dim unchinDr As DataRow = unchinDt.NewRow()

        ' 運賃設定
        With unchinDr

            .Item("YUSO_BR_CD") = unsoLDr.Item("YUSO_BR_CD")
            .Item("NRS_BR_CD") = unsoLDr.Item("NRS_BR_CD")
            .Item("UNSO_NO_L") = unsoLDr.Item("UNSO_NO_L")
            .Item("UNSO_NO_M") = Me._unsoM
            .Item("CUST_CD_L") = unsoLDr.Item("CUST_CD_L")
            .Item("CUST_CD_M") = unsoLDr.Item("CUST_CD_M")
            .Item("CUST_CD_S") = outDr.Item("CUST_CD_S")
            .Item("CUST_CD_SS") = outDr.Item("CUST_CD_SS")
            .Item("SHIHARAI_GROUP_NO") = ""
            .Item("SHIHARAI_GROUP_NO_M") = ""
            If String.IsNullOrEmpty(unsoLDr.Item("SHIHARAITO_CD").ToString) = False Then
                .Item("SHIHARAITO_CD") = unsoLDr.Item("SHIHARAITO_CD").ToString
            Else
                .Item("SHIHARAITO_CD") = String.Concat(unsoLDr.Item("UNSO_CD"), unsoLDr.Item("UNSO_BR_CD"))
            End If
            .Item("UNTIN_CALCULATION_KB") = outDr.Item("UNTIN_CALCULATION_KB")
            .Item("SHIHARAI_SYARYO_KB") = unsoLDr.Item("VCLE_KB")
            .Item("SHIHARAI_PKG_UT") = unsoLDr.Item("NB_UT")
            .Item("SHIHARAI_NG_NB") = unsoLDr.Item("UNSO_PKG_NB")
            .Item("SHIHARAI_DANGER_KB") = ""
            .Item("SHIHARAI_TARIFF_BUNRUI_KB") = unsoLDr.Item("TARIFF_BUNRUI_KB")
            .Item("SHIHARAI_TARIFF_CD") = unsoLDr.Item("SHIHARAI_TARIFF_CD")
            .Item("SHIHARAI_ETARIFF_CD") = unsoLDr.Item("SHIHARAI_ETARIFF_CD")
            .Item("SHIHARAI_KYORI") = outDr.Item("KYORI")
            .Item("SHIHARAI_WT") = unsoLDr.Item("UNSO_WT")
            .Item("SHIHARAI_UNCHIN") = 0                       '2013/04/01 入荷着払い時は支払0円 黎
            '.Item("SHIHARAI_UNCHIN") = inDr.Item("UNCHIN")
            .Item("SHIHARAI_CITY_EXTC") = 0
            .Item("SHIHARAI_WINT_EXTC") = 0
            .Item("SHIHARAI_RELY_EXTC") = 0
            .Item("SHIHARAI_TOLL") = 0
            .Item("SHIHARAI_INSU") = 0
            .Item("SHIHARAI_FIXED_FLAG") = "00"
            .Item("DECI_NG_NB") = unsoLDr.Item("UNSO_PKG_NB")
            .Item("DECI_KYORI") = outDr.Item("KYORI")
            .Item("DECI_WT") = unsoLDr.Item("UNSO_WT")
            .Item("DECI_UNCHIN") = 0                           '2013/04/01 入荷着払い時は支払0円 黎
            '.Item("DECI_UNCHIN") = inDr.Item("UNCHIN")
            .Item("DECI_CITY_EXTC") = 0
            .Item("DECI_WINT_EXTC") = 0
            .Item("DECI_RELY_EXTC") = 0
            .Item("DECI_TOLL") = 0
            .Item("DECI_INSU") = 0
            .Item("KANRI_UNCHIN") = 0                          '2013/04/01 入荷着払い時は支払0円 黎
            '.Item("KANRI_UNCHIN") = inDr.Item("UNCHIN")
            .Item("KANRI_CITY_EXTC") = 0
            .Item("KANRI_WINT_EXTC") = 0
            .Item("KANRI_RELY_EXTC") = 0
            .Item("KANRI_TOLL") = 0
            .Item("KANRI_INSU") = 0
            .Item("REMARK") = unsoLDr.Item("CUST_REF_NO")
            .Item("SIZE_KB") = ""
            .Item("TAX_KB") = unsoLDr.Item("TAX_KB")
            .Item("SAGYO_KANRI") = ""

            '要望番号2129 修正START 2013.11.20
            '2013.07.04 修正START
            'If unsoLDr.Item("NRS_BR_CD").ToString() = "50" AndAlso _
            If unsoLDr.Item("NRS_BR_CD").ToString() = "30" AndAlso _
               unsoLDr.Item("CUST_CD_L").ToString() = "00023" AndAlso _
               unsoLDr.Item("CUST_CD_M").ToString() = "00" Then
                '要望番号2129 修正END 2013.11.20

                ds = Me.DacAccess(ds, ACT_GET_RCV_BP_UNSOWT)

                If ds.Tables(TABLE_NM_RCV_BP).Rows.Count <> 0 Then
                    ' パラメータとなるテーブル
                    Dim bpRcvDr As DataRow = ds.Tables(TABLE_NM_RCV_BP).Rows(0)

                    .Item("SHIHARAI_WT") = bpRcvDr.Item("BP_UNSO_WT")
                    .Item("DECI_WT") = bpRcvDr.Item("BP_UNSO_WT")
                End If

            End If
            '2013.07.04 修正END

        End With

        unchinDt.Rows.Add(unchinDr)

    End Sub

#End Region '運賃レコード構成

#Region " ○1-7 運賃取得(calcUnchin CALL)"

    ''' <summary>
    ''' 運賃取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="index">運賃明細対象行</param>
    ''' <remarks>運賃計算プログラムへのパラメータを設定する</remarks>
    Private Sub GetUnchinRec(ByVal ds As DataSet, ByVal index As Integer)

        Dim outDr As DataRow = ds.Tables(TABLE_NM_OUT).Rows(0)
        Dim unsoLDr As DataRow = ds.Tables(TABLE_NM_UNSO_L).Rows(0)
        Dim unchinDr As DataRow = ds.Tables(TABLE_NM_SHIHARAI).Rows(index)

        '[引渡パラメータ設定]
        Dim calcInDt As DataTable = ds.Tables(TABLE_NM_CALC_IN)
        calcInDt.Clear()      '初期化

        '今回計算用のパラメータを設定
        Dim calcDr As DataRow = calcInDt.NewRow()

        With calcDr

            .Item("ACTION_FLG") = FLG_ON
            .Item("NRS_BR_CD") = unchinDr.Item("NRS_BR_CD").ToString
            .Item("CUST_CD_L") = unchinDr.Item("CUST_CD_L").ToString
            .Item("CUST_CD_M") = unchinDr.Item("CUST_CD_M").ToString
            .Item("DEST_CD") = unsoLDr.Item("DEST_CD").ToString
            .Item("DEST_JIS") = ""
            .Item("ARR_PLAN_DATE") = unsoLDr.Item("ARR_PLAN_DATE").ToString
            .Item("UNSO_PKG_NB") = unchinDr.Item("SHIHARAI_NG_NB").ToString
            .Item("NB_UT") = ""
            .Item("UNSO_WT") = unchinDr.Item("DECI_WT").ToString
            .Item("UNSO_ONDO_KB") = unsoLDr.Item("UNSO_ONDO_KB").ToString
            .Item("TARIFF_BUNRUI_KB") = unsoLDr.Item("TARIFF_BUNRUI_KB").ToString
            .Item("VCLE_KB") = unchinDr.Item("SHIHARAI_SYARYO_KB").ToString
            .Item("MOTO_DATA_KB") = unsoLDr.Item("MOTO_DATA_KB").ToString
            .Item("SHIHARAI_TARIFF_CD") = unchinDr.Item("SHIHARAI_TARIFF_CD").ToString
            .Item("SHIHARAI_ETARIFF_CD") = unchinDr.Item("SHIHARAI_ETARIFF_CD").ToString
            .Item("UNSO_TTL_QT") = outDr.Item("UNSO_TTL_QT_SUM").ToString
            .Item("SIZE_KB") = unchinDr.Item("SIZE_KB").ToString
            .Item("UNSO_DATE") = GetUnsoDate(unsoLDr, outDr)
            .Item("CARGO_KB") = ""
            .Item("CAR_TP") = ""
            .Item("WT_LV") = ""
            .Item("KYORI") = SetKyoriValue(outDr, unchinDr)
            .Item("DANGER_KB") = ""
            .Item("GOODS_CD_NRS") = ""
            .Item("ORIG_JIS_CD") = unsoLDr.Item("ORIG_JIS_CD").ToString
            .Item("DEST_JIS_CD") = unsoLDr.Item("DEST_JIS_CD").ToString

        End With

        calcInDt.Rows.Add(calcDr)

        '[運賃計算プログラム起動]
        ds = Me.CalcUnchin(ds)

    End Sub

    ''' <summary>
    ''' 横持ち運賃取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks>運賃計算プログラムへのパラメータを設定する</remarks>
    Private Sub GetYokoUnchin(ByVal ds As DataSet, ByVal idx As Integer)

        Dim outDr As DataRow = ds.Tables(TABLE_NM_OUT).Rows(0)
        Dim unsoLDr As DataRow = ds.Tables(TABLE_NM_UNSO_L).Rows(0)
        Dim unsoMDr As DataRow = ds.Tables(TABLE_NM_UNSO_M).Rows(idx)
        Dim unchinDr As DataRow = ds.Tables(TABLE_NM_SHIHARAI).Rows(idx)

        '[引渡パラメータ設定]
        Dim calcInDt As DataTable = ds.Tables(TABLE_NM_CALC_IN)
        calcInDt.Clear()      '初期化

        '今回計算用のパラメータを設定
        Dim calcDr As DataRow = calcInDt.NewRow()

        With calcDr

            .Item("ACTION_FLG") = FLG_ON
            .Item("NRS_BR_CD") = unchinDr.Item("NRS_BR_CD").ToString
            .Item("CUST_CD_L") = unchinDr.Item("CUST_CD_L").ToString
            .Item("CUST_CD_M") = unchinDr.Item("CUST_CD_M").ToString
            .Item("DEST_CD") = ""
            .Item("DEST_JIS") = ""
            .Item("ARR_PLAN_DATE") = ""
            .Item("UNSO_PKG_NB") = unchinDr.Item("DECI_NG_NB").ToString
            .Item("NB_UT") = unsoMDr.Item("NB_UT").ToString
            .Item("UNSO_WT") = unchinDr.Item("DECI_WT").ToString
            .Item("UNSO_ONDO_KB") = ""
            .Item("TARIFF_BUNRUI_KB") = unsoLDr.Item("TARIFF_BUNRUI_KB").ToString
            .Item("VCLE_KB") = unchinDr.Item("SHIHARAI_SYARYO_KB").ToString
            .Item("MOTO_DATA_KB") = unsoLDr.Item("MOTO_DATA_KB").ToString
            .Item("SHIHARAI_TARIFF_CD") = unchinDr.Item("SHIHARAI_TARIFF_CD").ToString
            .Item("SHIHARAI_ETARIFF_CD") = ""
            .Item("UNSO_TTL_QT") = ""
            .Item("SIZE_KB") = ""
            .Item("UNSO_DATE") = ""
            .Item("CARGO_KB") = ""
            .Item("CAR_TP") = ""
            .Item("WT_LV") = ""
            .Item("KYORI") = 0
            .Item("DANGER_KB") = unchinDr.Item("SHIHARAI_DANGER_KB").ToString
            .Item("GOODS_CD_NRS") = unsoMDr.Item("GOODS_CD_NRS").ToString

        End With

        calcInDt.Rows.Add(calcDr)

        '[運賃計算プログラム起動]
        ds = Me.CalcUnchin(ds)

    End Sub

#End Region '運賃取得

    '2013.03.26 / NOTES 1911 追加 開始
    Private Function GetAlctd_kb(ByVal ds As DataSet, ByVal index As Integer) As String

        '引数用一時DSL生成
        Dim Wk_ds As DataSet = ds.Copy
        Wk_ds.Clear()
        Dim wk_Indt As DataTable = Wk_ds.Tables(TABLE_NM_IN)
        Dim wk_Indr As DataRow = wk_Indt.NewRow
        '作業用DSL生成
        Dim Wk_ds_CPY As DataSet = ds.Copy
        Dim unsoLDr_CPY As DataRow = Wk_ds_CPY.Tables(TABLE_NM_UNSO_L).Rows(0)
        Dim unsoMDr_CPY As DataRow = Wk_ds_CPY.Tables(TABLE_NM_UNSO_M).Rows(index)
        '引数用一時DSLに詰め替え+
        wk_Indr.Item("NRS_BR_CD") = unsoMDr_CPY.Item("NRS_BR_CD").ToString
        wk_Indr.Item("UNSO_NO_L") = unsoLDr_CPY.Item("UNSO_NO_L").ToString
        wk_Indr.Item("UNSO_NO_M") = unsoMDr_CPY.Item("UNSO_NO_M").ToString
        wk_Indt.Rows.Add(wk_Indr)
        '返却受け取り用DSL生成
        Dim rtDS As DataSet = Me.DacAccess(Wk_ds, ACT_GET_ALCTD_KB)
        Dim rtTBL As DataTable = rtDS.Tables(TABLE_NM_UNSO_M)

        '返却値
        Return rtTBL.Rows(0).Item("ALCTD_KB").ToString

    End Function
    '2013.03.26 / NOTES 1911 追加 終了

#End Region '運賃データ生成

#Region " ●2 Method [運賃計算]"

#Region " ○2-1 Main Method"

    ''' <summary>
    ''' 運賃計算メイン
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Public Function CalcUnchin(ByVal ds As DataSet) As DataSet

        ' 入力パラメタ格納テーブル
        Dim inTbl As DataTable = ds.Tables(TABLE_NM_CALC_IN)

        ' 初期化
        Me.InitItemUnchinCalc(ds)

        ' 入力パラメータ整合チェック
        If Me.ChkInParamOnCalc(inTbl.Rows(0)) = False Then
            Me.SetResultMsg(ds.Tables(TABLE_NM_RESULT))
            Return ds
        End If

        ' 一般/横持ち判断
        If inTbl.Rows(0).Item("TARIFF_BUNRUI_KB").ToString.Equals(TARIFF_BUNRUI_YKMOTI) Then
            '横持ち
            Me.FormYokoUnchinRec(ds)
        Else
            '一般
            Me.FormUnchinRec(ds)
        End If

        '端数調整
        Me.AdjustFraction(ds)

        'エラー設定有無を判定し、calcUnchinの結果を設定
        Me.ResultSetFromCalcUnchin(ds)

        Me.SetResultMsg(ds.Tables(TABLE_NM_RESULT))

        ' Out用DataTable以外は初期化(LMF810内でCallする場合は除外)
        If inTbl.Rows(0).Item("ACTION_FLG").ToString.Equals(FLG_OFF) Then
            Me.ClearParamToOutput(ds)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 一般運賃計算メイン
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub FormUnchinRec(ByVal ds As DataSet)

        For idx As Integer = 1 To 2

            If Me.GetUnchin(ds, idx) Then
                'パラメータの再セット
                Me.SetInDataToSecondTariff(ds)
            Else
                Exit For
            End If

        Next

    End Sub

    ''' <summary>
    ''' 一般運賃計算
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True：運賃計算継続 ／ False：運賃計算終了</returns>
    ''' <remarks></remarks>
    Private Function GetUnchin(ByVal ds As DataSet, ByVal count As Integer) As Boolean

        Dim inTbl As DataTable = ds.Tables(TABLE_NM_CALC_IN) 'inParam
        Dim retFlg As Boolean = False                   '処理結果フラグ
        Dim tableTp As String = String.Empty            'タリフ計算種別

        '●運賃タリフ 基本情報取得（適用運賃・距離特定）

        '[距離列取得]
        If Me.GetKyoriBaseFromTariff(ds) = False Then
            Return retFlg
        End If

        '[タリフ計算種別取得]
        If ds.Tables(TABLE_NM_CALC_KYORI).Rows.Count > 0 Then
            tableTp = ds.Tables(TABLE_NM_CALC_KYORI).Rows(0).Item("TABLE_TP").ToString()
        End If

        '[２次タリフ存在確認]
        ' 第一回ループ時のみ参照
        If count = 1 Then
            retFlg = Me.ChkSecondTariff(ds)
        End If

        Select Case tableTp
            Case TABLE_TYPE_J_WEIGHT, TABLE_TYPE_J_KODATE
                '[重量or個数列特定]
                If Me.GetJKRow(ds, tableTp) = False Then
                    Return retFlg
                End If
            Case Else
                '[距離列特定]
                If Me.GetKyoriRow(ds) = False Then
                    Return retFlg
                End If
        End Select

        '[inTbl 情報格納]
        Me.SetInDataTable(ds)


        '●運賃タリフ 重量情報取得（金額部情報取得）

        '[データアクセス]
        If Me.GetWtFromTariff(ds) = False Then
            Return retFlg
        End If

        Select Case tableTp
            Case TABLE_TYPE_J_WEIGHT, TABLE_TYPE_J_KODATE
                '[JISコード行特定]
                If Me.GetJisLine(ds, tableTp) = False Then
                    Return retFlg
                End If
            Case Else
                '[適用重量行特定]
                If Me.GetWeightLine(ds) = False Then
                    Return retFlg
                End If
        End Select

        '[適用タリフ確定] 
        ' 適用タリフが存在する場合のみ、当箇所を通過
        ' 以降２次タリフ参照は行わない。
        retFlg = False


        '●運賃タリフ 重量情報取得（金額部情報取得）

        '運賃計算/結果DataTableへ格納
        Me.SetUnchin(ds)

        '●割増運賃作成

        '[割増運賃有無判定]
        If inTbl.Rows(0).Item("SHIHARAI_ETARIFF_CD").ToString.Equals("") = False Then

            '1-1 パラメータ整備（届先JISコード取得）
            Me.SetInDataToExtcUncnin(ds)

            '2-1 集配料設定ファイル参照
            ds = Me.DacAccess(ds, ACT_GET_EXTC_SHUHAI)

            '2-2-1 割増運賃マスタ情報参照
            If Me.GetExtcUnchin(ds) = False Then
                Return retFlg
            End If

            '3-1 割増運賃設定
            Me.SetExtcUnchin(ds)

        End If

        Return retFlg

    End Function

    ''' <summary>
    ''' 横持ち運賃計算メイン
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True：正常 ／ False：異常（パラーメータエラー）</returns>
    ''' <remarks></remarks>
    Private Function FormYokoUnchinRec(ByVal ds As DataSet) As Boolean

        '●横持ち運賃タリフヘッダ データ取得
        If Me.GetBaseFromYokoTariff(ds) = False Then
            Return False
        End If


        '●横持ち運賃タリフ明細 データ取得
        If Me.GetDetailFromYokoTariff(ds) = False Then
            Return False
        End If


        '●横持ち運賃計算
        Me.SetYokoUnchin(ds)

        Return True

    End Function

#End Region 'Main Method

#Region " ○2-2 check"

    ''' <summary>
    ''' 対象マスタ存在チェック（一般）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="table">DataTable名</param>
    ''' <param name="setErrorType">エラー判定用</param>
    ''' <param name="judgeKbn">結果ステータス判定用</param>
    ''' <returns>true:正常 false:エラー</returns>
    ''' <remarks>DB参照したレコードが存在するか否かを判断する</remarks>
    Private Function IsDataExists(ByVal ds As DataSet _
                                 , ByVal table As String _
                                 , ByVal setErrorType As String _
                                 , Optional ByVal judgeKbn As String = RESULT_ERR_APPLI) As Boolean

        Dim inDt As DataTable = ds.Tables(TABLE_NM_CALC_IN)
        Dim chkDt As DataTable = ds.Tables(table)     'チェック用DataTable

        'DataSetﾚｺｰﾄﾞ存在確認
        If chkDt.Rows.Count = INIT_NUM Then

            'エラー設定不要の場合
            If setErrorType.Equals(FLG_OFF) Then
                Return False
            End If

            'エラー設定要の場合
            Return Me.SetMsgInfo(judgeKbn, "E078", SetMessageParamIsExists(table))

        End If

        'タリフ（距離）
        Select Case table
            Case TABLE_NM_CALC_KYORI

                '適用運賃存在チェック
                If chkDt.Rows(0).Item("TABLE_TP").ToString = TABLE_TYPE_D_WEIGHT Then

                    If (Convert.ToDouble(inDt.Rows(0).Item("KYORI").ToString) = INIT_DOU Or _
                        Convert.ToDouble(inDt.Rows(0).Item("UNSO_WT").ToString) = INIT_DOU) Then
                        Return Me.SetMsgInfo(RESULT_ERR_ZERO, "G036", "")
                    End If
                End If

            Case Else
                'チェックなし
        End Select

        Return True

    End Function

    ''' <summary>
    ''' 入力パラメータ整合チェック（メイン）
    ''' </summary>
    ''' <param name="dr">起動元機能からのパラメータ</param>
    ''' <returns>True：正常 ／ False：異常（パラーメータエラー）</returns>
    ''' <remarks>起動元機能からのパラメータ内容を確認する</remarks>
    Private Function ChkInParamOnCalc(ByVal dr As DataRow) As Boolean

        '起動元フラグ
        Select Case dr.Item("ACTION_FLG").ToString
            Case FLG_OFF, _
                 FLG_ON
                '正常

            Case Else
                Return Me.SetMsgInfo(RESULT_ERR_PARAM, "E245", "起動元フラグ")
        End Select

        '営業所コード
        If dr.Item("NRS_BR_CD").ToString.Equals("") Then
            Return Me.SetMsgInfo(RESULT_ERR_PARAM, "E245", "営業所 情報未設定")
        End If

        'タリフコード
        If dr.Item("SHIHARAI_TARIFF_CD").ToString.Equals("") Then
            Return Me.SetMsgInfo(RESULT_ERR_PARAM, "E245", "タリフコード 情報未設定")
        End If

        '元データ区分
        Select Case dr.Item("MOTO_DATA_KB").ToString
            Case MOTO_NYUKA, _
                 MOTO_SYUKA, _
                 MOTO_UNSOU
                '正常

            Case Else
                Return Me.SetMsgInfo(RESULT_ERR_PARAM, "E245", "元データ区分 設定値不正")
        End Select

        'タリフ分類区分
        Select Case dr.Item("TARIFF_BUNRUI_KB").ToString
            Case TARIFF_BUNRUI_KONSAI, _
                 TARIFF_BUNRUI_SHAATU, _
                 TARIFF_BUNRUI_TOKBIN, _
                 TARIFF_BUNRUI_YKMOTI, _
                 TARIFF_BUNRUI_NYUTYA
                '正常

            Case Else
                Return Me.SetMsgInfo(RESULT_ERR_PARAM, "E245", "タリフ分類区分 設定値不正")
        End Select

        '距離
        If Convert.ToDouble(dr.Item("KYORI").ToString) < 0 Then
            Return Me.SetMsgInfo(RESULT_ERR_PARAM, "E245", "距離 情報不正")
        End If

        Return True

    End Function

    ''' <summary>
    ''' ２次タリフ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>true:２次タリフ有 false:２次タリフ無</returns>
    ''' <remarks>２次タリフが存在する否かを判断する</remarks>
    Private Function ChkSecondTariff(ByVal ds As DataSet) As Boolean

        Dim chkFlg As Boolean = False

        '距離DataTable内の２次タリフコードの存在確認
        If ds.Tables(TABLE_NM_CALC_KYORI).Rows(0).Item("SHIHARAI_TARIFF_CD2").ToString.Equals("") = False Then
            chkFlg = True
        End If

        Return chkFlg

    End Function

#End Region 'check

#Region " ○2-3 Set param"

    ''' <summary>
    ''' [初期化] Main
    ''' </summary>
    ''' <remarks>当該プログラム名で使用する項目の初期化を行う</remarks>
    Private Sub InitItemUnchinCalc(ByVal ds As DataSet)

        'DataTable初期化
        If ds.Tables(TABLE_NM_CALC_IN).Rows(0).Item("ACTION_FLG").ToString.Equals(FLG_OFF) Then
            Me.ClearParamToInput(ds)
        End If

        'メッセージ領域初期化
        If ds.Tables(TABLE_NM_CALC_IN).Rows(0).Item("ACTION_FLG").ToString.Equals(FLG_OFF) Then
            Me._errStatus = ""
            Me._id = ""
            Me._msg = ""

            '結果格納用DataTable
            ds.Tables(TABLE_NM_RESULT).Rows.Add(ds.Tables(TABLE_NM_RESULT).NewRow())

        End If

        'Inparamの初期値設定
        Me.InitInparam(ds)

        'Outparamの初期値設定
        Me.InitOutparam(ds)

    End Sub

    ''' <summary>
    ''' [初期化] Inparam
    ''' </summary>
    ''' <remarks>当該プログラム名で使用する項目の初期化を行う</remarks>
    Private Sub InitInparam(ByVal ds As DataSet)

        'Inparamの初期値設定
        Dim inDr As DataRow = ds.Tables(TABLE_NM_CALC_IN).NewRow()

        With ds.Tables(TABLE_NM_CALC_IN).Rows(0)

            If .Item("ARR_PLAN_DATE").ToString.Equals("") Then
                .Item("ARR_PLAN_DATE") = MyBase.GetSystemDate
            End If

            If .Item("UNSO_DATE").ToString.Equals("") Then
                .Item("UNSO_DATE") = MyBase.GetSystemDate
            End If

        End With


    End Sub

    ''' <summary>
    ''' [初期化] Outparam
    ''' </summary>
    ''' <remarks>当該プログラム名で使用する項目の初期化を行う</remarks>
    Private Sub InitOutparam(ByVal ds As DataSet)

        '結果用DataTable
        Dim outDt As DataTable = ds.Tables(TABLE_NM_CALC_OUT)

        outDt.Clear()     '初期化

        '新しい行を生成
        Dim outDr As DataRow = outDt.NewRow()
        outDt.Rows.Add(outDr)

    End Sub

    ''' <summary>
    ''' InParam情報の設定 ( afterGetTariff )
    ''' </summary>
    ''' <remarks>呼出元より設定されないパラメータを設定する</remarks>
    Private Sub SetInDataTable(ByVal ds As DataSet)

        Dim inDr As DataRow = ds.Tables(TABLE_NM_CALC_IN).Rows(0)
        Dim tableType As String = ds.Tables(TABLE_NM_CALC_KYORI).Rows(0).Item("TABLE_TP").ToString

        Dim shashu As String = ""
        Dim weightLv As Double = 0

        With inDr

            '車種
            Select Case tableType
                Case TABLE_TYPE_D_SHASHU

                    If .Item("TARIFF_BUNRUI_KB").ToString.Equals(TARIFF_BUNRUI_SHAATU) Then
                        shashu = .Item("VCLE_KB").ToString
                    Else
                        shashu = INIT_KBN
                    End If

                Case TABLE_TYPE_K_TAKKYU
                    shashu = .Item("SIZE_KB").ToString
                Case Else
                    shashu = INIT_KBN
            End Select

            '重量段階
            Select Case tableType
                Case TABLE_TYPE_D_SHASHU
                    weightLv = INIT_DOU
                Case TABLE_TYPE_D_KODATE
                    weightLv = Convert.ToDouble(.Item("UNSO_PKG_NB").ToString)
                Case TABLE_TYPE_D_SUDATE
                    weightLv = Convert.ToDouble(.Item("UNSO_TTL_QT").ToString)
                Case TABLE_TYPE_K_KODATE
                    weightLv = Convert.ToDouble(.Item("UNSO_PKG_NB").ToString)
                Case TABLE_TYPE_K_TAKKYU
                    weightLv = INIT_DOU
                Case Else
                    weightLv = Convert.ToDouble(.Item("UNSO_WT").ToString)

            End Select

            .Item("CAR_TP") = shashu
            .Item("WT_LV") = weightLv

        End With

    End Sub

    ''' <summary>
    ''' InParam情報の設定 ( beforeExtcUnchin )
    ''' </summary>
    ''' <remarks>呼出元より設定されないパラメータを設定する</remarks>
    Private Sub SetInDataToExtcUncnin(ByVal ds As DataSet)

        Dim inDr As DataRow = ds.Tables(TABLE_NM_CALC_IN).Rows(0)

        With inDr
            If .Item("DEST_JIS").ToString.Equals("") Then

                If .Item("CUST_CD_L").ToString.Equals("") = False And _
                   .Item("DEST_CD").ToString.Equals("") = False Then

                    ' 届先マスタ参照
                    ds = Me.DacAccess(ds, ACT_GET_DEST_JIS)

                End If
            End If
        End With

    End Sub

    ''' <summary>
    ''' InParam情報の設定 ( ２次タリフ参照用 )
    ''' </summary>
    ''' <remarks>２次タリフ参照用にInTableを再構成する</remarks>
    Private Sub SetInDataToSecondTariff(ByVal ds As DataSet)

        Dim inDr As DataRow = ds.Tables(TABLE_NM_CALC_IN).Rows(0)

        ' inTable
        With inDr

            .Item("SHIHARAI_TARIFF_CD") = ds.Tables(TABLE_NM_CALC_KYORI).Rows(0).Item("SHIHARAI_TARIFF_CD2").ToString
            .Item("DEST_JIS") = ""
            .Item("CAR_TP") = ""
            .Item("WT_LV") = ""

        End With

        ds.Tables(TABLE_NM_CALC_KYORI).Clear()
        ds.Tables(TABLE_NM_CALC_WEIGHT).Clear()

    End Sub

    ''' <summary>
    ''' 運賃計算結果ステータス格納
    ''' </summary>
    ''' <remarks>CalcUnchinメソッドの最終結果を設定する</remarks>
    Private Sub ResultSetFromCalcUnchin(ByVal ds As DataSet)

        '結果ステータスが未設定の場合、結果を書き込む
        If Me._errStatus.Equals("") Then

            If Convert.ToInt32(ds.Tables(TABLE_NM_CALC_OUT).Rows(0).Item("UNCHIN").ToString) > 0 Then
                Me.SetMsgInfo(RESULT_NOMAL, "G027", "")
            Else
                Me.SetMsgInfo(RESULT_ERR_ZERO, "G036", "")
            End If

        End If

    End Sub

#End Region 'Set param

#Region " ○2-4 Sub Method"

    ''' <summary>
    ''' 距離列特定
    ''' </summary>
    ''' <param name="ds">起動元機能からのパラメータ</param>
    ''' <returns>True：正常 ／ False：異常（距離列取得エラー）</returns>
    ''' <remarks>[inDataTable]適用距離 / [運賃タリフマスタ]距離行を元に、距離対象列を取得</remarks>
    Private Function GetKyoriRow(ByVal ds As DataSet) As Boolean

        Dim kyoriDt As DataTable = ds.Tables(TABLE_NM_CALC_KYORI)
        Dim inDt As DataTable = ds.Tables(TABLE_NM_CALC_IN)

        '距離
        Dim inKyori As Double = Convert.ToDouble(inDt.Rows(0).Item("KYORI").ToString)
        Dim mstKyori As Double = 0

        Dim retFlg As Boolean = False    '処理結果フラグ
        Me._kyoriRow = -1                '格納変数初期化

        '■属性の先頭から距離比較を行う
        For idx As Integer = 1 To 70

            '[マスタ設定]距離取得
            mstKyori = Convert.ToDouble(kyoriDt.Rows(0).Item(String.Concat("KYORI_", idx)).ToString)

            '距離Zeroは適用終了（対象タリフなし）
            If mstKyori = 0 Then
                Exit For
            Else
                '距離判定
                If inKyori <= mstKyori Then
                    '処理継続
                    Me._kyoriRow = idx
                    retFlg = True
                    Exit For
                Else
                    '距離確定
                    Me._kyoriRow = idx
                End If
            End If
        Next

        Return retFlg

    End Function

    ''' <summary>
    ''' 重量行特定
    ''' </summary>
    ''' <param name="ds">起動元機能からのパラメータ</param>
    ''' <returns>True：正常 ／ False：異常（距離列取得エラー）</returns>
    ''' <remarks>[inDataTable]重量段階 / [運賃タリフマスタ]重量段階を元に、重量対象行を取得</remarks>
    Private Function GetWeightLine(ByVal ds As DataSet) As Boolean

        Dim weightDt As DataTable = ds.Tables(TABLE_NM_CALC_WEIGHT)
        Dim inDt As DataTable = ds.Tables(TABLE_NM_CALC_IN)

        '重量段階
        Dim inWeight As Double = Convert.ToDouble(inDt.Rows(0).Item("WT_LV").ToString)
        Dim mstWeight As Double = 0

        Dim retFlg As Boolean = False               '処理結果フラグ
        Dim max As Integer = weightDt.Rows.Count    '重量DataTale行数

        Me._weightLine = -1                '格納変数初期化

        '■先頭レコードより重量比較を行う
        For cnt As Integer = 0 To max - 1

            '[マスタ設定]重量取得
            mstWeight = Convert.ToDouble(weightDt.Rows(cnt).Item("WT_LV").ToString)

            '重量判定
            If inWeight <= mstWeight Then
                Me._weightLine = cnt
                retFlg = True
                Exit For
            End If
        Next

        Return retFlg

    End Function

    ''' <summary>
    ''' 重量or個数列特定
    ''' </summary>
    ''' <param name="ds">起動元機能からのパラメータ</param>
    ''' <param name="tableTp">タリフ計算種別</param>
    ''' <returns>True：正常 ／ False：異常（距離列取得エラー）</returns>
    ''' <remarks>[inDataTable]適用重量or個数 / [運賃タリフマスタ]距離行を元に、重量or個数対象列を取得</remarks>
    Private Function GetJKRow(ByVal ds As DataSet, ByVal tableTp As String) As Boolean

        Dim kyoriDt As DataTable = ds.Tables(TABLE_NM_CALC_KYORI)
        Dim inDt As DataTable = ds.Tables(TABLE_NM_CALC_IN)

        '重量or個数
        Dim mstValue As Double = 0
        Dim inValue As Double = 0
        If TABLE_TYPE_J_WEIGHT.Equals(tableTp) Then
            '重量
            inValue = Convert.ToDouble(inDt.Rows(0).Item("UNSO_WT").ToString)
        Else
            '個数
            inValue = Convert.ToDouble(inDt.Rows(0).Item("UNSO_PKG_NB").ToString)
        End If

        Dim retFlg As Boolean = False    '処理結果フラグ
        Me._kyoriRow = -1                '格納変数初期化

        '■属性の先頭から距離比較を行う
        For idx As Integer = 1 To 70

            '[マスタ設定]重量or個数取得
            mstValue = Convert.ToDouble(kyoriDt.Rows(0).Item(String.Concat("KYORI_", idx)).ToString)

            '重量or個数Zeroは適用終了（対象タリフなし）
            If mstValue = 0 Then
                Exit For
            Else
                '距離判定
                If inValue <= mstValue Then
                    '処理継続
                    Me._kyoriRow = idx
                    retFlg = True
                    Exit For
                Else
                    '距離確定
                    Me._kyoriRow = idx
                End If
            End If
        Next

        Return retFlg

    End Function

    ''' <summary>
    ''' JISコード行特定
    ''' </summary>
    ''' <param name="ds">起動元機能からのパラメータ</param>
    ''' <param name="tableTp">タリフ計算種別</param>
    ''' <returns>True：正常 ／ False：異常（距離列取得エラー）</returns>
    ''' <remarks>[inDataTable]JISコード / [運賃タリフマスタ]JISコードを元に、重量対象行を取得</remarks>
    Private Function GetJisLine(ByVal ds As DataSet, ByVal tableTp As String) As Boolean

        Dim weightDt As DataTable = ds.Tables(TABLE_NM_CALC_WEIGHT)
        Dim inDt As DataTable = ds.Tables(TABLE_NM_CALC_IN)

        'JISコード（起点・着点）
        Dim inOrigJis As String = inDt.Rows(0).Item("ORIG_JIS_CD").ToString()
        Dim inDestJis As String = inDt.Rows(0).Item("DEST_JIS_CD").ToString()
        Dim mstOrigJis As String = String.Empty
        Dim mstDestJis As String = String.Empty

        Dim retFlg As Boolean = False               '処理結果フラグ
        Dim max As Integer = weightDt.Rows.Count    '重量DataTale行数

        Me._weightLine = -1                '格納変数初期化

        '■先頭レコードよりJISコード比較を行う
        For cnt As Integer = 0 To max - 1

            '[マスタ設定]JISコード取得
            mstOrigJis = weightDt.Rows(cnt).Item("ORIG_JIS_CD").ToString()
            mstDestJis = weightDt.Rows(cnt).Item("DEST_JIS_CD").ToString()

            'JISコード判定
            If mstOrigJis.Equals(inOrigJis) AndAlso mstDestJis.Equals(inDestJis) Then
                Me._weightLine = cnt
                retFlg = True
                Exit For
            End If
        Next

        Return retFlg

    End Function

    ''' <summary>
    ''' 横持ち運賃タリフ明細マスタ参照
    ''' </summary>
    ''' <param name="ds">起動元機能からのパラメータ</param>
    ''' <remarks>横持ち運賃タリフ明細の参照用パラメータセット</remarks>
    Private Sub SetYokoDetailParam(ByVal ds As DataSet)

        Dim inDr As DataRow = ds.Tables(TABLE_NM_CALC_IN).Rows(0)
        Dim yokoHeadDr As DataRow = ds.Tables(TABLE_NM_CALC_YOKO_HD).Rows(0)

        Dim cargoKbn As String = INIT_KBN    '荷姿区分
        Dim shashKbn As String = INIT_KBN    '車種区分
        Dim weightLV As Double = INIT_NUM    '重量

        Select Case yokoHeadDr.Item("CALC_KB").ToString
            Case YOKO_CALC_KBN_SUGATA
                cargoKbn = inDr.Item("NB_UT").ToString

            Case YOKO_CALC_KBN_KURUMA
                shashKbn = inDr.Item("VCLE_KB").ToString

            Case YOKO_CALC_KBN_TEIZOU
                weightLV = Convert.ToDouble(inDr.Item("UNSO_WT").ToString)

        End Select

        inDr.Item("CARGO_KB") = cargoKbn
        inDr.Item("CAR_TP") = shashKbn
        inDr.Item("WT_LV") = weightLV

    End Sub

    ''' <summary>
    ''' マスタ参照データ取得エラー（置換文字）
    ''' </summary>
    ''' <param name="table">取得エラーとなったデータテーブル</param>
    ''' <returns>エラーメッセージに設定する文言（E078）</returns>
    ''' <remarks>ＤＢ結果取得エラー時のメッセージ設定文字を設定</remarks>
    Private Function SetMessageParamIsExists(ByVal table As String) As String

        Dim retMsg As String = ""

        Select Case table

            Case TABLE_NM_UNSO_L
                retMsg = "運送Ｌ情報"

            Case TABLE_NM_UNSO_M
                retMsg = "運送Ｍ情報"

            Case TABLE_NM_OUT
                retMsg = "運賃基本（荷主、商品）情報"

            Case TABLE_NM_TARIFF_LATEST _
                , TABLE_NM_CALC_KYORI _
                , TABLE_NM_CALC_WEIGHT
                retMsg = "運賃タリフ情報"

            Case TABLE_NM_CALC_E_SHIHARAI
                retMsg = "割増運賃タリフ情報"

            Case TABLE_NM_CALC_YOKO_HD
                retMsg = "横持ちタリフ情報（ヘッダ）"

            Case TABLE_NM_CALC_YOKO_DTL
                retMsg = "横持ちタリフ情報（明細）"

        End Select


        Return retMsg

    End Function

#End Region 'Sub Method

#Region " ○2-5 運賃設定"

    ''' <summary>
    ''' 一般運賃設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks>戻りDataTableに運賃を設定</remarks>
    Private Sub SetUnchin(ByVal ds As DataSet)

        Dim unchin As Double = 0
        Dim weightDt As DataTable = ds.Tables(TABLE_NM_CALC_WEIGHT)

        '行/列 共に特定できる場合のみ処理を行う
        If Me._kyoriRow = -1 Or _
           Me._weightLine = -1 Then
            '距離取得失敗
        Else
            '運賃取得
            unchin = Convert.ToDouble(weightDt.Rows(Me._weightLine).Item(String.Concat("KYORI_", Me._kyoriRow)).ToString)

            '建値計算
            unchin = Me.CalcNomalTatene(ds, unchin)

        End If

        '結果を格納
        ds.Tables(TABLE_NM_CALC_OUT).Rows(0).Item("UNCHIN") = unchin

    End Sub

    ''' <summary>
    ''' 割増運賃設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks>戻りDataTableに運賃を設定</remarks>
    Private Sub SetExtcUnchin(ByVal ds As DataSet)

        Dim outDt As DataTable = ds.Tables(TABLE_NM_CALC_OUT)

        '■集配料( & 保険料 )設定
        outDt.Rows(0).Item("INSU") = Me.GetExtcShuhai(ds)

        '■冬期割増運賃設定
        outDt.Rows(0).Item("WINT_EXTC") = Me.GetExtcWinter(ds)

        '■都市割増運賃設定
        outDt.Rows(0).Item("CITY_EXTC") = Me.GetExtcCity(ds)

        '■航送料割増運賃設定
        outDt.Rows(0).Item("TOLL") = Me.GetExtcToll(ds)

        '■中継料割増運賃設定
        outDt.Rows(0).Item("RELY_EXTC") = Me.GetExtcRely(ds)

    End Sub

    ''' <summary>
    ''' 集配料・保険料計算
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>計算結果割増運賃</returns>
    ''' <remarks>集配料・保険料計算を行う</remarks>
    Private Function GetExtcShuhai(ByVal ds As DataSet) As Double

        Dim retUnchin As Double = 0     '返却変数
        Dim shuhaiRow As Integer = -1   '集配料格納列

        '集配料設定データ存在有無
        If ds.Tables(TABLE_NM_CALC_SYUHAI).Rows.Count = 0 Then

            '集配料無し

        Else

            shuhaiRow = Convert.ToInt32(ds.Tables(TABLE_NM_CALC_SYUHAI).Rows(0).Item("FIELD_NO").ToString)

            ' 集配料 + 保険料 を計算
            retUnchin = Convert.ToDouble(ds.Tables(TABLE_NM_CALC_WEIGHT).Rows(Me._weightLine).Item(String.Concat("KYORI_", shuhaiRow))) _
                        + Convert.ToDouble(ds.Tables(TABLE_NM_CALC_WEIGHT).Rows(Me._weightLine).Item("INSU"))

        End If

        Return retUnchin

    End Function

    ''' <summary>
    ''' 冬期割増運賃計算
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>計算結果割増運賃</returns>
    ''' <remarks>冬期割増運賃計算を行う</remarks>
    Private Function GetExtcWinter(ByVal ds As DataSet) As Double

        Dim retUnchin As Double = 0     '返却変数
        Dim extcDt As DataTable = ds.Tables(TABLE_NM_CALC_E_SHIHARAI)      '割増運賃DataTable


        Select Case extcDt.Rows(0).Item("WINT_EXTC_YN").ToString
            Case EXTC_WINT_A, _
                 EXTC_WINT_B

                Dim inDt As DataTable = ds.Tables(TABLE_NM_CALC_IN)

                Dim arrPlanDate As String = Right(inDt.Rows(0).Item("ARR_PLAN_DATE").ToString, 4)   '納入予定日 (下４桁)
                Dim wintFrom As String = extcDt.Rows(0).Item("WINT_KIKAN_FROM").ToString            '冬期期間From
                Dim wintTo As String = extcDt.Rows(0).Item("WINT_KIKAN_TO").ToString                '冬期期間To

                Dim extcFlg As Boolean = False


                '期間判定
                If wintFrom <= wintTo Then

                    If (wintFrom <= arrPlanDate AndAlso arrPlanDate <= wintTo) Then
                        extcFlg = True
                    End If
                Else
                    '年跨りVer
                    If (wintFrom <= arrPlanDate AndAlso arrPlanDate <= "1231") Or _
                       ("0101" <= arrPlanDate AndAlso arrPlanDate <= wintTo) Then
                        extcFlg = True
                    End If

                End If

                If extcFlg = True Then

                    '適用運賃判定
                    Select Case extcDt.Rows(0).Item("WINT_EXTC_YN").ToString
                        Case EXTC_WINT_A
                            retUnchin = Convert.ToDouble(ds.Tables(TABLE_NM_CALC_WEIGHT).Rows(Me._weightLine).Item("WINT_EXTC_A").ToString)
                        Case EXTC_WINT_B
                            retUnchin = Convert.ToDouble(ds.Tables(TABLE_NM_CALC_WEIGHT).Rows(Me._weightLine).Item("WINT_EXTC_B").ToString)
                    End Select

                End If

        End Select

        Return retUnchin

    End Function

    ''' <summary>
    ''' 都市割増運賃計算
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>計算結果割増運賃</returns>
    ''' <remarks>都市割増運賃計算を行う</remarks>
    Private Function GetExtcCity(ByVal ds As DataSet) As Double

        Dim retUnchin As Double = 0     '返却変数
        Dim extcDt As DataTable = ds.Tables(TABLE_NM_CALC_E_SHIHARAI)      '割増運賃DataTable

        '適用運賃判定
        Select Case extcDt.Rows(0).Item("CITY_EXTC_YN").ToString
            Case EXTC_CITY_A
                retUnchin = Convert.ToDouble(ds.Tables(TABLE_NM_CALC_WEIGHT).Rows(Me._weightLine).Item("CITY_EXTC_A").ToString)
            Case EXTC_CITY_B
                retUnchin = Convert.ToDouble(ds.Tables(TABLE_NM_CALC_WEIGHT).Rows(Me._weightLine).Item("CITY_EXTC_B").ToString)
        End Select

        Return retUnchin

    End Function

    ''' <summary>
    ''' 航送料割増運賃計算
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>計算結果割増運賃</returns>
    ''' <remarks>航送料割増運賃計算を行う</remarks>
    Private Function GetExtcToll(ByVal ds As DataSet) As Double

        Dim retUnchin As Double = 0     '返却変数
        Dim extcDt As DataTable = ds.Tables(TABLE_NM_CALC_E_SHIHARAI)      '割増運賃DataTable

        '適用運賃判定
        If extcDt.Rows(0).Item("FRRY_EXTC_YN").ToString.Equals(FLG_ON) Then

            '10KGあたりの航送料
            retUnchin = Math.Ceiling(Convert.ToDecimal(ds.Tables(TABLE_NM_CALC_IN).Rows(0).Item("UNSO_WT").ToString) _
                        / 10) _
                        * Convert.ToDecimal(extcDt.Rows(0).Item("FRRY_EXTC_10KG").ToString)

        End If

        Return retUnchin

    End Function

    ''' <summary>
    ''' 中継料割増運賃計算
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns>計算結果割増運賃</returns>
    ''' <remarks>中継料割増運賃計算を行う</remarks>
    Private Function GetExtcRely(ByVal ds As DataSet) As Double

        Dim retUnchin As Double = 0     '返却変数
        Dim extcDt As DataTable = ds.Tables(TABLE_NM_CALC_E_SHIHARAI)      '割増運賃DataTable

        '適用運賃判定
        Select Case extcDt.Rows(0).Item("RELY_EXTC_YN").ToString
            Case EXTC_RELY_NOMAL
                retUnchin = Convert.ToDouble(ds.Tables(TABLE_NM_CALC_WEIGHT).Rows(Me._weightLine).Item("RELY_EXTC").ToString)

            Case EXTC_RELY_DOUBLE
                retUnchin = Convert.ToDouble(ds.Tables(TABLE_NM_CALC_WEIGHT).Rows(Me._weightLine).Item("RELY_EXTC").ToString) * 2

        End Select

        Return retUnchin

    End Function

    ''' <summary>
    ''' 一般運賃 建値計算処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="unchin">タリフより取得した運賃</param>
    ''' <returns>建値計算後運賃</returns>
    ''' <remarks>一般運賃時の建値計算（テーブルタイプ毎）</remarks>
    Private Function CalcNomalTatene(ByVal ds As DataSet, ByVal unchin As Double) As Double

        '返却用変数を定義
        Dim retUnchin As Double = INIT_DOU

        Dim inRow As DataRow = ds.Tables(TABLE_NM_CALC_IN).Rows(0)
        Dim tableType As String = ds.Tables(TABLE_NM_CALC_WEIGHT).Rows(Me._weightLine).Item("TABLE_TP").ToString

        With inRow

            Select Case tableType
                Case TABLE_TYPE_D_WEIGHT
                    retUnchin = unchin

                Case TABLE_TYPE_D_SHASHU
                    retUnchin = unchin

                Case TABLE_TYPE_D_KODATE
                    retUnchin = unchin * Convert.ToInt32(.Item("UNSO_PKG_NB").ToString)

                Case TABLE_TYPE_D_JYDATE
                    retUnchin = unchin * Convert.ToDouble(.Item("UNSO_WT").ToString)

                Case TABLE_TYPE_D_SUDATE
                    retUnchin = unchin * Convert.ToInt32(.Item("UNSO_TTL_QT").ToString)

                Case TABLE_TYPE_K_KODATE
                    retUnchin = unchin * Convert.ToInt32(.Item("UNSO_PKG_NB").ToString)

                Case TABLE_TYPE_K_TAKKYU
                    retUnchin = unchin * Convert.ToInt32(.Item("UNSO_PKG_NB").ToString)

                Case TABLE_TYPE_K_JYDATE
                    retUnchin = unchin * Convert.ToDouble(.Item("UNSO_WT").ToString)

                Case TABLE_TYPE_J_WEIGHT
                    retUnchin = unchin

                Case TABLE_TYPE_J_KODATE
                    retUnchin = unchin * Convert.ToInt32(.Item("UNSO_PKG_NB").ToString)

            End Select


        End With

        Return retUnchin

    End Function

    ''' <summary>
    ''' 横持ち運賃算出・設定処理
    ''' </summary>
    ''' <param name="ds">起動元機能からのパラメータ</param>
    ''' <remarks>横持ち運賃タリフ明細の参照用パラメータセット</remarks>
    Private Sub SetYokoUnchin(ByVal ds As DataSet)

        Dim inDr As DataRow = ds.Tables(TABLE_NM_CALC_IN).Rows(0)
        Dim yokoHeadDr As DataRow = ds.Tables(TABLE_NM_CALC_YOKO_HD).Rows(0)
        Dim yokoDetailDr As DataRow = ds.Tables(TABLE_NM_CALC_YOKO_DTL).Rows(0)

        Dim untTanka As Double = Convert.ToDouble(yokoDetailDr.Item("UT_PRICE").ToString())
        Dim kgsTanka As Double = Convert.ToDouble(yokoDetailDr.Item("KGS_PRICE").ToString())
        Dim konsu As Integer = Convert.ToInt32(inDr.Item("UNSO_PKG_NB").ToString())
        Dim weight As Double = Convert.ToDouble(inDr.Item("UNSO_WT").ToString())

        Dim yokoUnchin As Double = 0.0

        '計算区分により設定値を切り替える
        Select Case yokoHeadDr.Item("CALC_KB").ToString
            Case YOKO_CALC_KBN_SUGATA
                yokoUnchin = untTanka * konsu

            Case YOKO_CALC_KBN_KURUMA
                yokoUnchin = untTanka

            Case YOKO_CALC_KBN_TEIZOU
                yokoUnchin = untTanka

            Case YOKO_CALC_KBN_WEIGHT
                yokoUnchin = kgsTanka * weight

        End Select

        ds.Tables(TABLE_NM_CALC_OUT).Rows(0).Item("UNCHIN") = yokoUnchin

    End Sub

    ''' <summary>
    ''' 端数調整処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks>算出後運賃の端数調整処理</remarks>
    Private Sub AdjustFraction(ByVal ds As DataSet)

        Dim calcOutDr As DataRow = ds.Tables(TABLE_NM_CALC_OUT).Rows(0)           '運賃結果DataRow

        Dim unchin As Double = Convert.ToDouble(calcOutDr.Item("UNCHIN").ToString)
        Dim cityKg As Double = Convert.ToDouble(calcOutDr.Item("CITY_EXTC").ToString)
        Dim wintKg As Double = Convert.ToDouble(calcOutDr.Item("WINT_EXTC").ToString)
        Dim relyKg As Double = Convert.ToDouble(calcOutDr.Item("RELY_EXTC").ToString)
        Dim tollKg As Double = Convert.ToDouble(calcOutDr.Item("TOLL").ToString)
        Dim insuKg As Double = Convert.ToDouble(calcOutDr.Item("INSU").ToString)

        '[データアクセス]
        ds = Me.DacAccess(ds, ACT_GET_ADJUST_CUST)

        '[データ存在チェック(レコード存在＝切り上げ)]
        If MyBase.GetResultCount() = 0 Then

            '四捨五入
            unchin = ToHalfAdjust(unchin, 0)
            cityKg = ToHalfAdjust(cityKg, 0)
            wintKg = ToHalfAdjust(wintKg, 0)
            relyKg = ToHalfAdjust(relyKg, 0)
            tollKg = ToHalfAdjust(tollKg, 0)
            insuKg = ToHalfAdjust(insuKg, 0)

        Else

            '切り上げ
            unchin = ToRoundUp(unchin, 0)
            cityKg = ToRoundUp(cityKg, 0)
            wintKg = ToRoundUp(wintKg, 0)
            relyKg = ToRoundUp(relyKg, 0)
            tollKg = ToRoundUp(tollKg, 0)
            insuKg = ToRoundUp(insuKg, 0)

        End If

        calcOutDr.Item("UNCHIN") = unchin
        calcOutDr.Item("CITY_EXTC") = cityKg
        calcOutDr.Item("WINT_EXTC") = wintKg
        calcOutDr.Item("RELY_EXTC") = relyKg
        calcOutDr.Item("TOLL") = tollKg
        calcOutDr.Item("INSU") = insuKg

    End Sub

#End Region '運賃設定

#Region " ○2-6 データ参照"

    ''' <summary>
    ''' 通常タリフ距離情報 取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True：正常 ／ False：異常（データ取得不備）</returns>
    ''' <remarks>最新タリフより適用 "距離" 情報を取得する</remarks>
    Private Function GetKyoriBaseFromTariff(ByVal ds As DataSet) As Boolean

        '[データアクセス]
        ds = Me.DacAccess(ds, ACT_GET_TARIFF_BASE)

        '[データ存在チェック]
        If Me.IsDataExists(ds, TABLE_NM_CALC_KYORI, FLG_ON, RESULT_WAR_APPLI) = False Then
            Return False
        End If

        '[テーブルタイプ整合チェック（トンキロ・車建て）]
        If ds.Tables(TABLE_NM_CALC_KYORI).Rows(0).Item("TABLE_TP").ToString.Equals(TABLE_TYPE_D_SHASHU) Then

            Select Case ds.Tables(TABLE_NM_CALC_IN).Rows(0).Item("TARIFF_BUNRUI_KB").ToString
                Case TARIFF_BUNRUI_SHAATU
                    '正常
                Case Else
                    'データ不整合
                    Me.SetMsgInfo(RESULT_WAR_APPLI, "E345", "")
                    Return False
            End Select

        End If

        Return True

    End Function

    ''' <summary>
    ''' 通常タリフ重量情報 取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True：正常 ／ False：異常（データ取得不備）</returns>
    ''' <remarks>最新タリフより適用 "重量" 情報を取得する</remarks>
    Private Function GetWtFromTariff(ByVal ds As DataSet) As Boolean

        '[データアクセス]
        ds = Me.DacAccess(ds, ACT_GET_TARIFF_WEIGHT)

        '[データ存在チェック]
        If Me.IsDataExists(ds, TABLE_NM_CALC_WEIGHT, FLG_ON, RESULT_WAR_APPLI) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 割増運賃情報 取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True：正常 ／ False：異常（データ取得不備）</returns>
    ''' <remarks>割増運賃Ｍより金額を取得する</remarks>
    Private Function GetExtcUnchin(ByVal ds As DataSet) As Boolean

        '[データアクセス]
        ds = Me.DacAccess(ds, ACT_GET_EXTC_SHIHARAI)

        '[データ存在チェック]
        If Me.IsDataExists(ds, TABLE_NM_CALC_E_SHIHARAI, FLG_ON, RESULT_WAR_APPLI) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 横持ちタリフ基本情報 取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True：正常 ／ False：異常（データ取得不備）</returns>
    ''' <remarks>最新横持ちタリフより基本情報を取得する</remarks>
    Private Function GetBaseFromYokoTariff(ByVal ds As DataSet) As Boolean

        '[データアクセス]
        ds = Me.DacAccess(ds, ACT_GET_YOKO_HD)

        '[データ存在チェック]
        If Me.IsDataExists(ds, TABLE_NM_CALC_YOKO_HD, FLG_ON, RESULT_WAR_APPLI) = False Then
            Return False
        End If

        '[テーブルタイプ整合チェック（トンキロ・車建て）]
        If ds.Tables(TABLE_NM_CALC_YOKO_HD).Rows(0).Item("CALC_KB").ToString.Equals(YOKO_CALC_KBN_KURUMA) Then
            If ds.Tables(TABLE_NM_CALC_IN).Rows(0).Item("VCLE_KB").ToString.Equals("") Then
                'データ不整合
                Me.SetMsgInfo(RESULT_WAR_APPLI, "E345", "")
                Return False
            End If
        End If

        Return True

    End Function

    ''' <summary>
    ''' 横持ちタリフ明細情報 取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True：正常 ／ False：異常（データ取得不備）</returns>
    ''' <remarks>最新横持ちタリフより明細情報を取得する</remarks>
    Private Function GetDetailFromYokoTariff(ByVal ds As DataSet) As Boolean

        '[パラメータセット]
        Me.SetYokoDetailParam(ds)

        '[データアクセス]
        ds = Me.DacAccess(ds, ACT_GET_YOKO_DTL)

        '[結果確認]
        If Me.IsDataExists(ds, TABLE_NM_CALC_YOKO_DTL, FLG_ON, RESULT_WAR_APPLI) = False Then
            Return False
        End If

        Return True

    End Function

#End Region 'Sub Method

#End Region '運賃計算


#Region "事前確認"

    ''' <summary>
    ''' 支払タリフ件数取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectCountShiharaiTariffByNrsBrCd(ByVal ds As DataSet) As DataSet
        Return Me.DacAccess(ds, ACT_GET_SHIHARAI_TARIFF_COUNT)
    End Function

#End Region



#End Region '全体

End Class
