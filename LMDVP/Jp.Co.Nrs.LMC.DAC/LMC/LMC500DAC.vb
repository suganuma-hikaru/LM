' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC500    : 納品書印刷
'  作  成  者       :  [SAGAWA]
' ==========================================================================
'  LMC500DACのファイル肥大化によりVSが落ちるので定数定義を別ファイル化
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC500DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC500DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Field"

    ''' <summary>
    ''' 検索条件設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' 発行SQL作成用
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql As StringBuilder

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region


#Region "Method"

#Region "検索処理"

    ''' <summary>
    '''出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出力対象帳票パターン取得SQLの構築・発行</remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC500IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        '種別フラグを取得('0':出荷、'1':運送)
        Dim ptnFlag As String = Me._Row.Item("PTN_FLAG").ToString()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成(種別フラグで分岐)
        If ptnFlag = "0" Then '出荷
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MPrt_OUTKA)      'SQL構築(帳票種別用Select句)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_FROM_MPrt_OUTKA)        'SQL構築(データ抽出用From句)
        ElseIf ptnFlag = "1" Then '運送
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MPrt_UNSO)      'SQL構築(帳票種別用Select句)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_FROM_MPrt_UNSO)         'SQL構築(データ抽出用From句)
        Else 'とりあえず出荷
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MPrt_OUTKA)      'SQL構築(帳票種別用Select句)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_FROM_MPrt_OUTKA)        'SQL構築(データ抽出用From句)
        End If
        Call Me.SetConditionMasterSQLMprt(ptnFlag)                  '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        '20120613----------------------------
        cmd.CommandTimeout = 6000
        '------------------------------------
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC500DAC", "SelectMPrt", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("PTN_ID", "PTN_ID")
        map.Add("PTN_CD", "PTN_CD")
        map.Add("RPT_ID", "RPT_ID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "M_RPT")

        Return ds

    End Function


    ''' <summary>
    ''' 納品書出力対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>納品書出力対象データ取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMC500IN")
        'DataSetのM_RPT情報を取得
        Dim rptTbl As DataTable = ds.Tables("M_RPT")

        'LMC514対応開始
        '荷主明細マスタの設定値取得
        'Dim setNaiyo As String = String.Empty
        'Me.SelectMCustDetailsData(ds)
        'If ds.Tables("MCD_SET_NAIYO").Rows.Count > 0 Then
        '    setNaiyo = ds.Tables("MCD_SET_NAIYO").Rows(0)("MCD_SET_NAIYO").ToString()
        'End If
        'If ds.Tables("MCD_SET_NAIYO_2").Rows.Count > 0 Then
        '    setNaiyo = ds.Tables("MCD_SET_NAIYO_2").Rows(0)("MCD_SET_NAIYO_2").ToString()
        'End If
        'If ds.Tables("MCD_SET_NAIYO_3").Rows.Count > 0 Then
        '    setNaiyo = ds.Tables("MCD_SET_NAIYO_3").Rows(0)("MCD_SET_NAIYO_3").ToString()
        'End If
        'LMC514対応終了

        '(2012.04.10) Notes№962 FREE_C03使用有無対応 -- START --
        Dim FreeC03_Umu As String = String.Empty
        Me.SelectMCustDetailsData(ds)
        If ds.Tables("SET_NAIYO").Rows.Count > 0 Then
            FreeC03_Umu = ds.Tables("SET_NAIYO").Rows(0)("SET_NAIYO").ToString()
        End If
        '(2012.04.10) Notes№962 FREE_C03使用有無対応 --  END  --

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        '種別フラグを取得('0':出荷、'1':運送)
        Dim ptnFlag As String = Me._Row.Item("PTN_FLAG").ToString()

        ''20120124 INSERT START OIKAWA OOSAKA対応
        '営業所CDを取得
        Dim nrs_br_cd As String = inTbl.Rows(0).Item("NRS_BR_CD").ToString()
        ''20120124 INSERT END OIKAWA OOSAKA対応)
        '倉庫CDを取得
        Dim wh_cd As String = inTbl.Rows(0).Item("WH_CD").ToString()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        '出力帳票がLMC501(名古屋_日本合成)の場合は、LMC501(名古屋_日本合成)用SQLを構築
        If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC501" Then
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN_NITIGO)      'SQL構築(データ抽出用Select句)'20120731 群馬
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA)            'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_FROM_OUTKA)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_WHERE_OUTKA)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO)             'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)              'SQL構築(データ抽出用Where句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_NITIGO)         'SQL構築(データ抽出用GROUP BY句)'20120731 群馬
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY)                'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                          'SQL構築(条件設定)

            '出力帳票がLMC503  納品書（YCC_ローム　荷主名称S表示）の場合
        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC503" Then
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN1)       'SQL構築(データ抽出用Select句)'20120731 群馬
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_OUTKA_503)       'SQL構築(データ抽出用Select句)'20120731 群馬
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_OUTKA_503)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_WHERE_OUTKA)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO)        'SQL構築(データ抽出用Select句)'20120731 群馬
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)         'SQL構築(データ抽出用Where句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY1)          'SQL構築(データ抽出用GROUP BY句)'20120731 群馬
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY)           'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                     'SQL構築(条件設定)

            '出力帳票がLMC506（名古屋標準）の場合は、LMC506（名古屋標準）用SQLを構築
        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC506" OrElse
               rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC884" Then
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN_NAGOYA)      'SQL構築(データ抽出用Select句)'20120731 群馬
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA)            'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_FROM_OUTKA)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_WHERE_OUTKA)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO)             'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)              'SQL構築(データ抽出用Where句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_NAGOYA)         'SQL構築(データ抽出用GROUP BY句)'20120731 群馬
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY)                'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                          'SQL構築(条件設定)

        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC513" And FreeC03_Umu <> "01" Then
            'FREE_C03項目未使用
            'ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC513" Then
            '出力帳票がLMC513(大阪 三井化学･山九用)の場合は、LMC513(出荷指図書版)用SQLを構築
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_513)             'SQL構築(データ抽出用SELRCT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_513)               'SQL構築(データ抽出用FROM句)
            Call SetConditionMasterSQLMprt(ptnFlag)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_513)           'SQL構築(データ抽出用WHERE句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY_513)           'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL_SAIHAKKO()                'SQL構築(条件設定)
            'Call Me.SetConditionMasterSQL() 

        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC513" And FreeC03_Umu = "01" Then
            'FREE_C03項目使用
            '出力帳票がLMC513(大阪 三井化学･山九用)の場合は、LMC513(出荷指図書版)用SQLを構築
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_513_FREE)        'SQL構築(データ抽出用SELRCT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_513_FREE)          'SQL構築(データ抽出用FROM句)
            Call SetConditionMasterSQLMprt(ptnFlag)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_513_FREE)      'SQL構築(データ抽出用WHERE句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY_513)           'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL_SAIHAKKO()                'SQL構築(条件設定)
            'Call Me.SetConditionMasterSQL()

        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC514" Then '
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN_3)          'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA_SAGYO_3)   'SQL構築(データ抽出用Select句)★★★
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO_SAGYO_3)    'SQL構築(データ抽出用Select句)★★★
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)             'SQL構築(データ抽出用Where句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY2)              'SQL構築(データ抽出用GROUP BY句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY3)              'SQL構築(データ抽出用GROUP BY句緊急連絡先)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY)               'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                         'SQL構築(条件設定)

        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC515" Then '
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN1)           'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_OUTKA_515)       'SQL構築(データ抽出用Select句)'横浜ケマーズ
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_FROM_OUTKA)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_WHERE_OUTKA)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO_515)        'SQL構築(データ抽出用Select句)'横浜ケマーズ
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)             'SQL構築(データ抽出用Where句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY1_515)          'SQL構築(データ抽出用GROUP BY句)'横浜ケマーズ
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY)               'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                         'SQL構築(条件設定)

        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC516" Then '
            '通常
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN_TEFLON)       'SQL構築(データ抽出用Select句)'20130620 大黒
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA_TEFLON) 'SQL構築(データ抽出用Select句)'20130620 大黒
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO_TEFLON)  'SQL構築(データ抽出用Select句)'20130620 大黒
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)          'SQL構築(データ抽出用Where句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_TEFLON)     'SQL構築(データ抽出用GROUP BY句)'20130620 大黒
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY)            'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                      'SQL構築(条件設定)

            'アクサルタ納品書　注文番号（M単位）の表示対応 2017/10/04 Annen START
        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC857" Then '
            '通常
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN_TEFLON_FURUKAWA)    'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA_TEFLON)            'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO_TEFLON)             'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)                     'SQL構築(データ抽出用Where句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_TEFLON)                'SQL構築(データ抽出用GROUP BY句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY)                       'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                                 'SQL構築(条件設定)
            'アクサルタ納品書　注文番号（M単位）の表示対応 2017/10/04 Annen END

        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC517" Then
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_NIK)             'SQL構築(データ抽出用Select句)'20120731 群馬
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA_NIK)       'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_FROM_OUTKA_NIK)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_WHERE_OUTKA)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO)            'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)             'SQL構築(データ抽出用Where句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_NIK)           'SQL構築(データ抽出用GROUP BY句)'20120731 群馬
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY)               'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                         'SQL構築(条件設定)

            '(2012.08.06) 納品書(群馬・端数行分割版)用 置き場や商品が同じならば、明細行をまとめる --- START ---
        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC622" Then
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_622)             'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA)           'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_FROM_OUTKA)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_WHERE_OUTKA)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO)            'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)             'SQL構築(データ抽出用Where句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_622)           'SQL構築(データ抽出用GROUP BY句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY_622)           'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                         'SQL構築(条件設定)
            '(2012.08.06) 納品書(群馬・端数行分割版)用 置き場や商品が同じならば、明細行をまとめる ---  END  ---

            'LMC623の納品書から出荷指図書への変更により、コメントアウト
            'ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC623" Then '篠崎出荷立会書
            '    Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN1)       'SQL構築(データ抽出用Select句)
            '    Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA)       'SQL構築(データ抽出用Select句)
            '    Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_UNSO)        'SQL構築(データ抽出用Select句)
            '    Me._StrSql.Append(LMC500DAC_Const1.SQL_WHERE_MAIN)         'SQL構築(データ抽出用Where句)
            '    Me._StrSql.Append(LMC500DAC_Const1.SQL_GROUP_BY1)          'SQL構築(データ抽出用GROUP BY句)
            '    Me._StrSql.Append(LMC500DAC_Const1.SQL_ORDER_BY_SZ)        'SQL構築(データ抽出用ORDER BY句)篠崎運送専用
            '    Call Me.SetConditionMasterSQL()                     'SQL構築(条件設定)

            '(2012.08.28) 納品書(群馬・篠崎運輸用) --- START ---
        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC624" Then
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN1)           'SQL構築(データ抽出用Select句)'20120731 群馬
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA)           'SQL構築(データ抽出用Select句)'20120731 群馬
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_FROM_OUTKA)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_WHERE_OUTKA)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO)            'SQL構築(データ抽出用Select句)'20120731 群馬
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)             'SQL構築(データ抽出用Where句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY1)              'SQL構築(データ抽出用GROUP BY句)'20120731 群馬
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY_624)           'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                         'SQL構築(条件設定)
            '(2012.08.28) 納品書(群馬・篠崎運輸用) ---  END  ---

            '(2012.10.12) 納品書(千葉系納品書) --- START ---
            '(2016.07.12)千葉標準+バーコード(LMC819) 追加
        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC621" Or
               rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC628" Or
               rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC633" Or
               rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC634" Or
               rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC876" Or
               rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC635" Or
               rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC636" Or
               rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC637" Or
               rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC639" Or
               rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC732" Or
               rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC733" Or
               rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC734" Or
               rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC736" Or
               rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC738" Or
               rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC819" Then
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN_621)         'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA_621)        'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_FROM_OUTKA_621)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_WHERE_OUTKA)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO_621)         'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)              'SQL構築(データ抽出用WHER句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_621)            'SQL構築(データ抽出用GROUP BY句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY)                'SQL構築(データ抽出用ORDER BY句)

            Call Me.SetConditionMasterSQL()                          'SQL構築(条件設定)
            '(2012.10.12) 納品書(千葉・汎用) ---  END  ---
            '(2016.07.22) 納品書JT物流(LMC629)  追加 --- START ---
        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC629" OrElse
               rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC885" Then
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN_629)         'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA_629)        'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO_629)         'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)              'SQL構築(データ抽出用WHER句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_629)            'SQL構築(データ抽出用GROUP BY句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY)                'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                          'SQL構築(条件設定)
            '(2016.07.22) 納品書JT物流(LMC629)  追加 --- END   ---
            '(2012.10.12) 納品書(千葉・汎用) ---  END  ---
            '(2014.5.29) 納品書(千葉・BYK用荷主状態表示用) --- START ---
        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC753" Then

            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN_753)         'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA_753)        'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_FROM_OUTKA_753)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_WHERE_OUTKA)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO_753)        'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)              'SQL構築(データ抽出用WHER句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_753)            'SQL構築(データ抽出用GROUP BY句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY)                'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                          'SQL構築(条件設定)
            '(2014.5.29) 納品書(千葉・BYK用荷主状態表示用) --- END ---

            '(2012.10.30) 納品書(千葉・富士フィルム用) --- START ---
        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC631" Then
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN_631)         'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA_631)        'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_FROM_OUTKA_631)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_WHERE_OUTKA)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO_631)         'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)              'SQL構築(データ抽出用WHER句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_631)            'SQL構築(データ抽出用GROUP BY句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY)                'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                          'SQL構築(条件設定)
            '(2012.10.30) 納品書(千葉・富士フィルム用) ---  END  ---

            '(2012.11.28) LMC638対応 -- START --
        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC638" Then
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN_621)         'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA_621)        'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_FROM_OUTKA_621)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_WHERE_OUTKA)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO_621)         'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)              'SQL構築(データ抽出用WHER句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_621)            'SQL構築(データ抽出用GROUP BY句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY_638)            'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                          'SQL構築(条件設定)
            '(2012.11.28) LMC638対応 --  END  --

            '(2012.11.30) LMC630 納品書(千葉・ﾕｰﾃｨｱｲ用)対応 -- START --
        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC630" Then
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_DATA_UTI)         'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_UTI)                'SQL構築(データ抽出用FROM句)
            Call SetConditionMasterSQLMprt(ptnFlag)                  'SQL構築(条件設定)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_UTI)            'SQL構築(データ抽出用GROUP BY句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY_UTI)            'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL_SAIHAKKO()
            '(2012.11.30) LMC630 納品書(千葉・ﾕｰﾃｨｱｲ用)対応 --  END  --

            '(2012.12.17) 納品書(千葉・ロンザジャパン用)【千葉・標準版に社名確認印欄追加】 --- START ---
        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC731" Then
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN_731)         'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA_731)        'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO_731)         'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)              'SQL構築(データ抽出用WHER句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_731)            'SQL構築(データ抽出用GROUP BY句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY)                'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                          'SQL構築(条件設定)
            '(2012.12.17) 納品書(千葉・ロンザジャパン用)【千葉・標準版に社名確認印欄追加】 ---  END  ---

            '(2014.03.31) 納品書(千葉アグリマート納品書) --- START ---
        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC740" Then
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN_740)         'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA_740)        'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO_740)         'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)              'SQL構築(データ抽出用WHER句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_740)            'SQL構築(データ抽出用GROUP BY句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY_740)            'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                          'SQL構築(条件設定)
            '(2014.03.31) 納品書(千葉アグリマート) ---  END  ---

            '(2014.06.10) 大阪日合納品書 START
        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC737" Then
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN_737)         'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA_737)        'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO_740)         'SQL構築(データ抽出用SELECT句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)              'SQL構築(データ抽出用WHER句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_737)            'SQL構築(データ抽出用GROUP BY句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY_740)            'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                          'SQL構築(条件設定)
            '(2014.06.10) 大阪日合納品書 END

            '(2017/09/04) 千葉　アンファ納品書 START
        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC854" Then
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN_854)    'SQL構築(データ抽出用Select句)
#If False Then  'UPD 2020/01/15 009792【LMS】RD帳票作成_アンファー:送り状・納品書亜種
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA)       'SQL構築(データ抽出用Select句)
#Else
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA_854)       'SQL構築(データ抽出用Select句)
#End If
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_ADD_SAGYO)
#If False Then  'UPD 2020/01/15 009792【LMS】RD帳票作成_アンファー:送り状・納品書亜種
            Me._StrSql.Append(LMC500DAC_Const1.SQL_FROM_OUTKA)
#Else
            Me._StrSql.Append(LMC500DAC_Const1.SQL_FROM_OUTKA_854)
#End If

            Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_WHERE_OUTKA)
#If False Then  'UPD 2020/01/15 009792【LMS】RD帳票作成_アンファー:送り状・納品書亜種
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_UNSO)        'SQL構築(データ抽出用Select句)
#Else
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO_854)        'SQL構築(データ抽出用Select句)
#End If
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)         'SQL構築(データ抽出用Where句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_854)       'SQL構築(データ抽出用GROUP BY句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY)           'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                     'SQL構築(条件設定)
            '(2017/09/04) 千葉　アンファ納品書 END
        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC899" Then
            'TSMC
            '(2024/03/01時点の通常パターンより複写 & 改修・追加)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN1.Replace(",SHOBO_CD ", ",SHOBO_CD " & vbNewLine & ",HAISOU_REMARK_MST "))    'SQL構築(データ抽出用Select句)'20120731 群馬 → 2024/03/01 改
            Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA)       'SQL構築(データ抽出用Select句)'20120731 群馬
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_GOODS_OUTKA_ATT)  'TSMC
            Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_FROM_OUTKA)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_GOODS_DETAILS_87)   'TSMC
            Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_ADD_SAGYO)
            Me._StrSql.Append(LMC500DAC_Const1.SQL_WHERE_OUTKA)
            Me._StrSql.Append(
                System.Text.RegularExpressions.Regex.Replace(
                    System.Text.RegularExpressions.Regex.Replace(
                        LMC500DAC_Const2.SQL_SELECT_UNSO,
                            " AS SHOBO_CD +",
                            " AS SHOBO_CD " & vbNewLine &
                            Left(LMC500DAC_Const2.SQL_SELECT_GOODS_OUTKA_ATT, LMC500DAC_Const2.SQL_SELECT_GOODS_OUTKA_ATT.Length() - vbNewLine.Length())
                        ),
                    "--《動的に結合するテーブルを追加する場合に置換するコメント》 +",
                    Left(LMC500DAC_Const2.SQL_FROM_GOODS_DETAILS_87, LMC500DAC_Const2.SQL_FROM_GOODS_DETAILS_87.Length() - vbNewLine.Length())
                    )
                )                                                       'SQL構築(データ抽出用Select句)'20120731 群馬 → 2024/03/01 改
            Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)         'SQL構築(データ抽出用Where句)
            Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY1.Replace(",SHOBO_CD ", ",SHOBO_CD " & vbNewLine & ",HAISOU_REMARK_MST "))       'SQL構築(データ抽出用GROUP BY句) → 2024/03/01 改
            Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY)           'SQL構築(データ抽出用ORDER BY句)
            Call Me.SetConditionMasterSQL()                     'SQL構築(条件設定)
        Else
            '【Notes】№728作業に伴い、ロジックを整理。START ----------------------------------

            ' ''20120124 DELETE START OIKAWA OOSAKA対応
            ''Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN)          'SQL構築(データ抽出用Select句)
            ' ''20120124 DELETE END OIKAWA OOSAKA対応
            ' ''20120124 INSERT START OIKAWA OOSAKA対応
            'If nrs_br_cd.Equals("20") = False Then
            '    Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN1)      'SQL構築(データ抽出用Select句)
            'Else
            '    Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN2)      'SQL構築(データ抽出用Select句)
            'End If
            ' ''20120124 INSERT END OIKAWA OOSAKA対応)
            'Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA)          'SQL構築(データ抽出用Select句)
            'Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_UNSO)           'SQL構築(データ抽出用Select句)
            'Me._StrSql.Append(LMC500DAC_Const1.SQL_WHERE_MAIN)            'SQL構築(データ抽出用Where句)
            ' ''20120124 DELETE START OIKAWA OOSAKA対応
            ''Me._StrSql.Append(LMC500DAC_Const1.SQL_GROUP_BY)             'SQL構築(データ抽出用GROUP BY句)
            ' ''20120124 DELETE END OIKAWA OOSAKA対応
            ' ''20120124 INSERT START OIKAWA OOSAKA対応
            'If nrs_br_cd.Equals("20") = False Then
            '    Me._StrSql.Append(LMC500DAC_Const1.SQL_GROUP_BY1)         'SQL構築(データ抽出用GROUP BY句)
            'Else
            '    Me._StrSql.Append(LMC500DAC_Const1.SQL_GROUP_BY2)         'SQL構築(データ抽出用GROUP BY句)
            'End If
            ' ''20120124 INSERT END OIKAWA OOSAKA対応)
            'Me._StrSql.Append(LMC500DAC_Const1.SQL_ORDER_BY)              'SQL構築(データ抽出用ORDER BY句)
            'Call Me.SetConditionMasterSQL()                        'SQL構築(条件設定)

#If False Then  'UPD 2019/10/08  008105【倉庫タブレット】テスト環境で複合機版の納品書・受領書(LMC875)をテストしたいので、帳票マスタメンテの登録をしてほしい
            If nrs_br_cd.Equals("20") = False Then

#Else
            'LMC875 は大阪以外で処理する
            'LMC888(納品書A4(ﾌｨﾙﾒ) [横浜, 大阪]) は通常の抽出SQLで処理する

            If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC875" OrElse
                rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC888" OrElse
                nrs_br_cd.Equals("20") = False Then
#End If
                If "600".Equals(wh_cd) Then
                    '中部物流センターのみ
                    '（ロケーションをグループ条件に含めない）
                    Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN_WH600)  'SQL構築(データ抽出用Select句)
                    Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA)       'SQL構築(データ抽出用Select句)
                    Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_ADD_SAGYO)
                    Me._StrSql.Append(LMC500DAC_Const1.SQL_FROM_OUTKA)
                    Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_ADD_SAGYO)
                    Me._StrSql.Append(LMC500DAC_Const1.SQL_WHERE_OUTKA)
                    Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO)        'SQL構築(データ抽出用Select句)
                    Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)         'SQL構築(データ抽出用Where句)
                    Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY_WH600)     'SQL構築(データ抽出用GROUP BY句)
                    Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY)           'SQL構築(データ抽出用ORDER BY句)
                    Call Me.SetConditionMasterSQL()                     'SQL構築(条件設定)
                Else
                    '通常
                    Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN1)       'SQL構築(データ抽出用Select句)'20120731 群馬
                    Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA)       'SQL構築(データ抽出用Select句)'20120731 群馬
                    Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_ADD_SAGYO)
                    Me._StrSql.Append(LMC500DAC_Const1.SQL_FROM_OUTKA)
                    Me._StrSql.Append(LMC500DAC_Const2.SQL_FROM_ADD_SAGYO)
                    Me._StrSql.Append(LMC500DAC_Const1.SQL_WHERE_OUTKA)
                    Me._StrSql.Append(LMC500DAC_Const3.SQL_WHERE_OUTKA_2)
                    Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO)        'SQL構築(データ抽出用Select句)'20120731 群馬
                    Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)         'SQL構築(データ抽出用Where句)
                    Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY1)          'SQL構築(データ抽出用GROUP BY句)'20120731 群馬
                    Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY)           'SQL構築(データ抽出用ORDER BY句)
                    Call Me.SetConditionMasterSQL()                     'SQL構築(条件設定)
                End If
            Else
                '大阪物流センタ－のみ
                Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MAIN2)       'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_OUTKA_SAGYO) 'SQL構築(データ抽出用Select句) 'Notes822 修正先
                Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_UNSO_SAGYO)  'SQL構築(データ抽出用Select句)
                Me._StrSql.Append(LMC500DAC_Const2.SQL_WHERE_MAIN)         'SQL構築(データ抽出用Where句)
                Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY2)          'SQL構築(データ抽出用GROUP BY句)
                Me._StrSql.Append(LMC500DAC_Const2.SQL_GROUP_BY4)          'SQL構築(データ抽出用GROUP BY句)
                Me._StrSql.Append(LMC500DAC_Const2.SQL_ORDER_BY)           'SQL構築(データ抽出用ORDER BY句)
                Call Me.SetConditionMasterSQL(rptTbl.Rows(0).Item("RPT_ID").ToString())     'SQL構築(条件設定)    '2018/11/13 要望番号002713 引数を追加
            End If

            '【Notes】№728作業に伴い、ロジックを整理。END ------------------------------------

        End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        '20120613----------------------------
        cmd.CommandTimeout = 6000
        '------------------------------------
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC500DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        '(2012.03.03) LMC513対応 --  START  ---------------------------------------------
        If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC513" Then

            '(2012.03.03) LMC513 納品書(大阪・三井化学・山九用)
            map.Add("RPT_ID", "RPT_ID")
            map.Add("NRS_BR_CD", "NRS_BR_CD")
            map.Add("PRINT_SORT", "PRINT_SORT")
            map.Add("TOU_BETU_FLG", "TOU_BETU_FLG")
            map.Add("OUTKA_NO_L", "OUTKA_NO_L")
            map.Add("DEST_CD", "DEST_CD")
            map.Add("DEST_NM", "DEST_NM")
            map.Add("DEST_AD_1", "DEST_AD_1")
            map.Add("DEST_AD_2", "DEST_AD_2")
            map.Add("DEST_AD_3", "DEST_AD_3")
            map.Add("DEST_TEL", "DEST_TEL")
            map.Add("CUST_CD_L", "CUST_CD_L")
            map.Add("CUST_NM_L", "CUST_NM_L")
            map.Add("CUST_NM_M", "CUST_NM_M")
            map.Add("CUST_NM_S", "CUST_NM_S")
            map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
            map.Add("CUST_ORD_NO", "CUST_ORD_NO")
            map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
            map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
            map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
            map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
            map.Add("UNSOCO_NM", "UNSOCO_NM")
            map.Add("PC_KB", "PC_KB")
            map.Add("KYORI", "KYORI")
            map.Add("UNSO_WT", "UNSO_WT")
            map.Add("URIG_NM", "URIG_NM")
            map.Add("FREE_C03", "FREE_C03")
            map.Add("REMARK_L", "REMARK_L")
            map.Add("REMARK_UNSO", "REMARK_UNSO")
            map.Add("SAGYO_REC_NO_1", "SAGYO_REC_NO_1")
            map.Add("SAGYO_CD_1", "SAGYO_CD_1")
            map.Add("SAGYO_NM_1", "SAGYO_NM_1")
            map.Add("SAGYO_REC_NO_2", "SAGYO_REC_NO_2")
            map.Add("SAGYO_CD_2", "SAGYO_CD_2")
            map.Add("SAGYO_NM_2", "SAGYO_NM_2")
            map.Add("SAGYO_REC_NO_3", "SAGYO_REC_NO_3")
            map.Add("SAGYO_CD_3", "SAGYO_CD_3")
            map.Add("SAGYO_NM_3", "SAGYO_NM_3")
            map.Add("SAGYO_REC_NO_4", "SAGYO_REC_NO_4")
            map.Add("SAGYO_CD_4", "SAGYO_CD_4")
            map.Add("SAGYO_NM_4", "SAGYO_NM_4")
            map.Add("SAGYO_REC_NO_5", "SAGYO_REC_NO_5")
            map.Add("SAGYO_CD_5", "SAGYO_CD_5")
            map.Add("SAGYO_NM_5", "SAGYO_NM_5")
            map.Add("CRT_USER", "CRT_USER")
            map.Add("OUTKA_NO_M", "OUTKA_NO_M")
            map.Add("GOODS_NM", "GOODS_NM")
            map.Add("FREE_C08", "FREE_C08")
            map.Add("IRIME", "IRIME")
            map.Add("IRIME_UT", "IRIME_UT")
            map.Add("KONSU", "KONSU")
            map.Add("HASU", "HASU")
            map.Add("ALCTD_NB", "ALCTD_NB")
            map.Add("NB_UT", "NB_UT")
            map.Add("ALCTD_CAN_NB", "ALCTD_CAN_NB")
            map.Add("FREE_C07", "FREE_C07")
            map.Add("ALCTD_QT", "ALCTD_QT")
            map.Add("ZAN_KONSU", "ZAN_KONSU")
            map.Add("ZAN_HASU", "ZAN_HASU")
            map.Add("SERIAL_NO", "SERIAL_NO")
            map.Add("PKG_NB", "PKG_NB")
            map.Add("PKG_UT", "PKG_UT")
            map.Add("ALCTD_KB", "ALCTD_KB")
            map.Add("ALCTD_CAN_QT", "ALCTD_CAN_QT")
            map.Add("REMARK_OUT", "REMARK_OUT")
            map.Add("LOT_NO", "LOT_NO")
            map.Add("LT_DATE", "LT_DATE")
            map.Add("INKA_DATE", "INKA_DATE")
            map.Add("REMARK_S", "REMARK_S")
            map.Add("GOODS_COND_NM_1", "GOODS_COND_NM_1")
            map.Add("GOODS_COND_NM_2", "GOODS_COND_NM_2")
            map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
            map.Add("BETU_WT", "BETU_WT")
            map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
            map.Add("TOU_NO", "TOU_NO")
            map.Add("SITU_NO", "SITU_NO")
            map.Add("ZONE_CD", "ZONE_CD")
            map.Add("LOCA", "LOCA")
            map.Add("REMARK_M", "REMARK_M")
            map.Add("SAGYO_MEI_REC_NO_1", "SAGYO_MEI_REC_NO_1")
            map.Add("SAGYO_MEI_CD_1", "SAGYO_MEI_CD_1")
            map.Add("SAGYO_MEI_NM_1", "SAGYO_MEI_NM_1")
            map.Add("SAGYO_MEI_REC_NO_2", "SAGYO_MEI_REC_NO_2")
            map.Add("SAGYO_MEI_CD_2", "SAGYO_MEI_CD_2")
            map.Add("SAGYO_MEI_NM_2", "SAGYO_MEI_NM_2")
            map.Add("SAGYO_MEI_REC_NO_3", "SAGYO_MEI_REC_NO_3")
            map.Add("SAGYO_MEI_CD_3", "SAGYO_MEI_CD_3")
            map.Add("SAGYO_MEI_NM_3", "SAGYO_MEI_NM_3")
            map.Add("SAGYO_MEI_REC_NO_4", "SAGYO_MEI_REC_NO_4")
            map.Add("SAGYO_MEI_CD_4", "SAGYO_MEI_CD_4")
            map.Add("SAGYO_MEI_NM_4", "SAGYO_MEI_NM_4")
            map.Add("SAGYO_MEI_REC_NO_5", "SAGYO_MEI_REC_NO_5")
            map.Add("SAGYO_MEI_CD_5", "SAGYO_MEI_CD_5")
            map.Add("SAGYO_MEI_NM_5", "SAGYO_MEI_NM_5")
            map.Add("SAIHAKKO_FLG", "SAIHAKKO_FLG")
            map.Add("OYA_CUST_GOODS_CD", "OYA_CUST_GOODS_CD")
            map.Add("OYA_GOODS_NM", "OYA_GOODS_NM")
            map.Add("OYA_KATA", "OYA_KATA")
            map.Add("OYA_OUTKA_TTL_NB", "OYA_OUTKA_TTL_NB")
            map.Add("SET_NAIYO", "SET_NAIYO")
            map.Add("OUTKO_DATE", "OUTKO_DATE")
            map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC513OUT")


            '(2012.11.30) LMC630 納品書(千葉 ﾕｰﾃｨｱｲ用) --- START ---
        ElseIf rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC630" Then

            map.Add("RPT_ID", "RPT_ID")
            map.Add("NRS_BR_CD", "NRS_BR_CD")
            map.Add("PRINT_SORT", "PRINT_SORT")
            map.Add("TOU_BETU_FLG", "TOU_BETU_FLG")
            map.Add("OUTKA_NO_L", "OUTKA_NO_L")
            map.Add("DEST_CD", "DEST_CD")
            map.Add("DEST_NM", "DEST_NM")
            map.Add("DEST_AD_1", "DEST_AD_1")
            map.Add("DEST_AD_2", "DEST_AD_2")
            map.Add("DEST_AD_3", "DEST_AD_3")
            map.Add("DEST_TEL", "DEST_TEL")
            map.Add("CUST_CD_L", "CUST_CD_L")
            map.Add("CUST_NM_L", "CUST_NM_L")
            map.Add("CUST_NM_M", "CUST_NM_M")
            map.Add("CUST_NM_S", "CUST_NM_S")
            map.Add("OUTKA_PKG_NB", "OUTKA_PKG_NB")
            map.Add("CUST_ORD_NO", "CUST_ORD_NO")
            map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
            map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
            map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
            map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
            map.Add("UNSOCO_NM", "UNSOCO_NM")
            map.Add("PC_KB", "PC_KB")
            map.Add("KYORI", "KYORI")
            map.Add("UNSO_WT", "UNSO_WT")
            map.Add("URIG_NM", "URIG_NM")
            map.Add("FREE_C03", "FREE_C03")
            map.Add("REMARK_L", "REMARK_L")
            map.Add("REMARK_UNSO", "REMARK_UNSO")
            map.Add("SAGYO_REC_NO_1", "SAGYO_REC_NO_1")
            map.Add("SAGYO_CD_1", "SAGYO_CD_1")
            map.Add("SAGYO_NM_1", "SAGYO_NM_1")
            map.Add("SAGYO_REC_NO_2", "SAGYO_REC_NO_2")
            map.Add("SAGYO_CD_2", "SAGYO_CD_2")
            map.Add("SAGYO_NM_2", "SAGYO_NM_2")
            map.Add("SAGYO_REC_NO_3", "SAGYO_REC_NO_3")
            map.Add("SAGYO_CD_3", "SAGYO_CD_3")
            map.Add("SAGYO_NM_3", "SAGYO_NM_3")
            map.Add("SAGYO_REC_NO_4", "SAGYO_REC_NO_4")
            map.Add("SAGYO_CD_4", "SAGYO_CD_4")
            map.Add("SAGYO_NM_4", "SAGYO_NM_4")
            map.Add("SAGYO_REC_NO_5", "SAGYO_REC_NO_5")
            map.Add("SAGYO_CD_5", "SAGYO_CD_5")
            map.Add("SAGYO_NM_5", "SAGYO_NM_5")
            map.Add("CRT_USER", "CRT_USER")
            map.Add("OUTKA_NO_M", "OUTKA_NO_M")
            map.Add("GOODS_NM", "GOODS_NM")
            map.Add("FREE_C08", "FREE_C08")
            map.Add("IRIME", "IRIME")
            map.Add("IRIME_UT", "IRIME_UT")
            map.Add("KONSU", "KONSU")
            map.Add("HASU", "HASU")
            map.Add("ALCTD_NB", "ALCTD_NB")
            map.Add("NB_UT", "NB_UT")
            map.Add("ALCTD_CAN_NB", "ALCTD_CAN_NB")
            map.Add("FREE_C07", "FREE_C07")
            map.Add("ALCTD_QT", "ALCTD_QT")
            map.Add("ZAN_KONSU", "ZAN_KONSU")
            map.Add("ZAN_HASU", "ZAN_HASU")
            map.Add("SERIAL_NO", "SERIAL_NO")
            map.Add("PKG_NB", "PKG_NB")
            map.Add("PKG_UT", "PKG_UT")
            map.Add("ALCTD_KB", "ALCTD_KB")
            map.Add("ALCTD_CAN_QT", "ALCTD_CAN_QT")
            map.Add("REMARK_OUT", "REMARK_OUT")
            map.Add("LOT_NO", "LOT_NO")
            map.Add("LT_DATE", "LT_DATE")
            map.Add("INKA_DATE", "INKA_DATE")
            map.Add("REMARK_S", "REMARK_S")
            map.Add("GOODS_COND_NM_1", "GOODS_COND_NM_1")
            map.Add("GOODS_COND_NM_2", "GOODS_COND_NM_2")
            map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
            map.Add("BETU_WT", "BETU_WT")
            map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
            map.Add("TOU_NO", "TOU_NO")
            map.Add("SITU_NO", "SITU_NO")
            map.Add("ZONE_CD", "ZONE_CD")
            map.Add("LOCA", "LOCA")
            map.Add("REMARK_M", "REMARK_M")
            map.Add("SAGYO_MEI_REC_NO_1", "SAGYO_MEI_REC_NO_1")
            map.Add("SAGYO_MEI_CD_1", "SAGYO_MEI_CD_1")
            map.Add("SAGYO_MEI_NM_1", "SAGYO_MEI_NM_1")
            map.Add("SAGYO_MEI_REC_NO_2", "SAGYO_MEI_REC_NO_2")
            map.Add("SAGYO_MEI_CD_2", "SAGYO_MEI_CD_2")
            map.Add("SAGYO_MEI_NM_2", "SAGYO_MEI_NM_2")
            map.Add("SAGYO_MEI_REC_NO_3", "SAGYO_MEI_REC_NO_3")
            map.Add("SAGYO_MEI_CD_3", "SAGYO_MEI_CD_3")
            map.Add("SAGYO_MEI_NM_3", "SAGYO_MEI_NM_3")
            map.Add("SAGYO_MEI_REC_NO_4", "SAGYO_MEI_REC_NO_4")
            map.Add("SAGYO_MEI_CD_4", "SAGYO_MEI_CD_4")
            map.Add("SAGYO_MEI_NM_4", "SAGYO_MEI_NM_4")
            map.Add("SAGYO_MEI_REC_NO_5", "SAGYO_MEI_REC_NO_5")
            map.Add("SAGYO_MEI_CD_5", "SAGYO_MEI_CD_5")
            map.Add("SAGYO_MEI_NM_5", "SAGYO_MEI_NM_5")
            map.Add("SAIHAKKO_FLG", "SAIHAKKO_FLG")
            map.Add("OYA_CUST_GOODS_CD", "OYA_CUST_GOODS_CD")
            map.Add("OYA_GOODS_NM", "OYA_GOODS_NM")
            map.Add("OYA_KATA", "OYA_KATA")
            map.Add("OYA_OUTKA_TTL_NB", "OYA_OUTKA_TTL_NB")
            map.Add("SET_NAIYO", "SET_NAIYO")
            map.Add("OUTKO_DATE", "OUTKO_DATE")
            map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
            map.Add("CUST_NM_S_H", "CUST_NM_S_H")
            map.Add("RPT_FLG", "RPT_FLG")
            map.Add("GOODS_COND_NM_3", "GOODS_COND_NM_3")
            map.Add("OUTKA_NO_S", "OUTKA_NO_S")
            map.Add("WH_CD", "WH_CD")
            map.Add("CUST_NAIYO_1", "CUST_NAIYO_1")
            map.Add("CUST_NAIYO_2", "CUST_NAIYO_2")
            map.Add("CUST_NAIYO_3", "CUST_NAIYO_3")
            map.Add("DEST_REMARK", "DEST_REMARK")
            map.Add("DEST_SALES_CD", "DEST_SALES_CD")
            map.Add("DEST_SALES_NM_L", "DEST_SALES_NM_L")
            map.Add("DEST_SALES_NM_M", "DEST_SALES_NM_M")
            map.Add("ALCTD_NB_HEADKEI", "ALCTD_NB_HEADKEI")
            map.Add("ALCTD_QT_HEADKEI", "ALCTD_QT_HEADKEI")
            map.Add("HINMEI", "HINMEI")
            map.Add("NISUGATA", "NISUGATA")
            map.Add("UTI_UN", "UTI_UN")
            map.Add("UTI_NM", "UTI_NM")
            map.Add("UTI_IM", "UTI_IM")
            map.Add("UTI_PG", "UTI_PG")
            map.Add("UTI_FP", "UTI_FP")
            map.Add("UTI_EL", "UTI_EL")
            map.Add("CUST_AD_1", "CUST_AD_1")
            map.Add("CUST_AD_2", "CUST_AD_2")
            map.Add("CUST_AD_3", "CUST_AD_3")
            map.Add("CUST_TEL", "CUST_TEL")
            map.Add("SHIP_CD", "SHIP_CD")

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC630OUT")

            '(2012.11.30) LMC630 納品書(千葉 ﾕｰﾃｨｱｲ用) ---  END  ---
        Else

            '通常 納品書
            map.Add("RPT_ID", "RPT_ID")
            map.Add("NRS_BR_CD", "NRS_BR_CD")
            map.Add("PRINT_SORT", "PRINT_SORT")
            map.Add("DEST_CD", "DEST_CD")
            map.Add("DEST_NM", "DEST_NM")
            map.Add("DEST_AD_1", "DEST_AD_1")
            map.Add("DEST_AD_2", "DEST_AD_2")
            map.Add("DEST_AD_3", "DEST_AD_3")
            map.Add("DEST_TEL", "DEST_TEL")
            map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
            map.Add("CUST_ORD_NO", "CUST_ORD_NO")
            map.Add("BUYER_ORD_NO", "BUYER_ORD_NO")
            map.Add("UNSOCO_NM", "UNSOCO_NM")
            map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
            map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
            map.Add("OUTKA_NO_L", "OUTKA_NO_L")
            map.Add("URIG_NM", "URIG_NM")
            map.Add("CUST_NM_L", "CUST_NM_L")
            map.Add("CUST_AD_1", "CUST_AD_1")
            map.Add("CUST_AD_2", "CUST_AD_2")
            map.Add("CUST_TEL", "CUST_TEL")
            map.Add("NRS_BR_NM", "NRS_BR_NM")
            map.Add("NRS_BR_AD1", "NRS_BR_AD1")
            map.Add("NRS_BR_AD2", "NRS_BR_AD2")
            map.Add("NRS_BR_TEL", "NRS_BR_TEL")
            map.Add("NRS_BR_FAX", "NRS_BR_FAX")
            map.Add("PIC", "PIC")
            map.Add("OUTKA_NO_M", "OUTKA_NO_M")
            map.Add("OUTKA_NO_S", "OUTKA_NO_S")
            map.Add("INKA_DATE", "INKA_DATE")
            map.Add("REMARK_OUT", "REMARK_OUT")
            map.Add("GOODS_NM_1", "GOODS_NM_1")
            map.Add("LOT_NO", "LOT_NO")
            map.Add("SERIAL_NO", "SERIAL_NO")
            map.Add("IRIME", "IRIME")
            map.Add("PKG_NB", "PKG_NB")
            map.Add("KONSU", "KONSU")
            map.Add("HASU", "HASU")
            map.Add("ALCTD_NB", "ALCTD_NB")
            map.Add("PKG_UT", "PKG_UT")
            map.Add("PKG_UT_NM", "PKG_UT_NM")
            map.Add("ALCTD_QT", "ALCTD_QT")
            map.Add("IRIME_UT", "IRIME_UT")
            map.Add("WT", "WT")
            map.Add("UNSO_WT", "UNSO_WT")
            map.Add("TOU_NO", "TOU_NO")
            map.Add("SITU_NO", "SITU_NO")
            map.Add("ZONE_CD", "ZONE_CD")
            map.Add("LOCA", "LOCA")
            map.Add("ZAN_KONSU", "ZAN_KONSU")
            map.Add("ZAN_HASU", "ZAN_HASU")
            map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
            map.Add("REMARK_M", "REMARK_M")
            map.Add("CUST_NM_S", "CUST_NM_S")
            map.Add("CUST_ORD_NO_DTL", "CUST_ORD_NO_DTL")
            map.Add("OUTKA_REMARK", "OUTKA_REMARK")
            map.Add("HAISOU_REMARK", "HAISOU_REMARK")
            map.Add("NHS_REMARK", "NHS_REMARK")
            map.Add("KYORI", "KYORI")
            map.Add("OUTKA_PKG_NB_L", "OUTKA_PKG_NB_L")
            map.Add("SOKO_NM", "SOKO_NM")
            map.Add("BUSHO_NM", "BUSHO_NM")
            map.Add("NRS_BR_NM_NICHI", "NRS_BR_NM_NICHI")
            map.Add("ALCTD_CAN_QT", "ALCTD_CAN_QT")
            map.Add("ALCTD_KB", "ALCTD_KB")
            map.Add("PTN_FLAG", "PTN_FLAG")
            map.Add("CUST_CD_L", "CUST_CD_L")
            map.Add("ORDER_TYPE", "ORDER_TYPE")
            map.Add("ATSUKAISYA_NM", "ATSUKAISYA_NM")
            map.Add("CUST_NM_S_H", "CUST_NM_S_H")

#If False Then  'UPD 2019/10/08  008105【倉庫タブレット】テスト環境で複合機版の納品書・受領書(LMC875)をテストしたいので、帳票マスタメンテの登録をしてほしい
            ''20120124 INSERT START OIKAWA OOSAKA対応)
            If nrs_br_cd.Equals("20") = True AndAlso (rptTbl.Rows(0).Item("RPT_ID").ToString() <> "LMC737" AndAlso rptTbl.Rows(0).Item("RPT_ID").ToString() <> "LMC517") Then

#Else
            ''20120124 INSERT START OIKAWA OOSAKA対応)
            If nrs_br_cd.Equals("20") = True AndAlso (rptTbl.Rows(0).Item("RPT_ID").ToString() <> "LMC737" AndAlso rptTbl.Rows(0).Item("RPT_ID").ToString() <> "LMC517" AndAlso
              rptTbl.Rows(0).Item("RPT_ID").ToString() <> "LMC875" AndAlso rptTbl.Rows(0).Item("RPT_ID").ToString() <> "LMC888") Then
#End If


                map.Add("TITLE_SMPL", "TITLE_SMPL")
                map.Add("LT_DATE", "LT_DATE")
                map.Add("ALCTD_CAN_NB", "ALCTD_CAN_NB")
                map.Add("GOODS_COND_CD", "GOODS_COND_CD")
                map.Add("GOODS_COND_NM", "GOODS_COND_NM")
                map.Add("REMARK_ZAI", "REMARK_ZAI")
                map.Add("BACKLOG_QT", "BACKLOG_QT")
                map.Add("DOKU_NM", "DOKU_NM")
                '【Notes】№728 作業項目出力対応 START ---------------------
                map.Add("SAGYO_MEI_REC_NO_1", "SAGYO_MEI_REC_NO_1")
                map.Add("SAGYO_MEI_CD_1", "SAGYO_MEI_CD_1")
                map.Add("SAGYO_MEI_NM_1", "SAGYO_MEI_NM_1")
                map.Add("SAGYO_MEI_REC_NO_2", "SAGYO_MEI_REC_NO_2")
                map.Add("SAGYO_MEI_CD_2", "SAGYO_MEI_CD_2")
                map.Add("SAGYO_MEI_NM_2", "SAGYO_MEI_NM_2")
                map.Add("SAGYO_MEI_REC_NO_3", "SAGYO_MEI_REC_NO_3")
                map.Add("SAGYO_MEI_CD_3", "SAGYO_MEI_CD_3")
                map.Add("SAGYO_MEI_NM_3", "SAGYO_MEI_NM_3")
                map.Add("SAGYO_MEI_REC_NO_4", "SAGYO_MEI_REC_NO_4")
                map.Add("SAGYO_MEI_CD_4", "SAGYO_MEI_CD_4")
                map.Add("SAGYO_MEI_NM_4", "SAGYO_MEI_NM_4")
                map.Add("SAGYO_MEI_REC_NO_5", "SAGYO_MEI_REC_NO_5")
                map.Add("SAGYO_MEI_CD_5", "SAGYO_MEI_CD_5")
                map.Add("SAGYO_MEI_NM_5", "SAGYO_MEI_NM_5")
                '【Notes】№728 作業項目出力対応 END ---------------------
                map.Add("TOU_SITU_NM", "TOU_SITU_NM")
                map.Add("JISYATASYA_KB", "JISYATASYA_KB")
            End If
            ''20120124 INSERT END OIKAWA OOSAKA対応)

            '千葉対応 (LMC635)納品書(美浜用)を追加
            If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC514" _
        OrElse rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC629" _
        OrElse rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC885" _
        OrElse rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC635" _
        OrElse rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC630" Then
                map.Add("MCD_SET_NAIYO", "MCD_SET_NAIYO")
                map.Add("MCD_SET_NAIYO_2", "MCD_SET_NAIYO_2")
                map.Add("MCD_SET_NAIYO_3", "MCD_SET_NAIYO_3")
            End If

            '千葉対応 (LMC854)納品書(アンファ用)を追加
            If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC854" Then
                map.Add("LT_DATE", "LT_DATE")
            End If
            '【Notes】№995 元着払い区分出力対応 篠原 START ---------------------
            map.Add("PC_KB_NM", "PC_KB_NM")
            '【Notes】№995 元着払い区分出力対応 篠原  END  ---------------------

            '--【要望番号1017】 2012/04/24　注文番号明細  START 
            map.Add("BUYER_ORD_NO_DTL", "BUYER_ORD_NO_DTL")
            '--【要望番号1017】 2012/04/24　注文番号明細  END
            '日医工対策開始
            map.Add("TORI_KB", "TORI_KB")
            map.Add("SHIP_CD_L", "SHIP_CD_L")
            'map.Add("CHOKUSO_NM1", "CHOKUSO_NM1")
            'map.Add("CHOKUSO_CD", "CHOKUSO_CD")
            'map.Add("HAKKO_DATE", "HAKKO_DATE")
            map.Add("SET_NAIYO_4", "SET_NAIYO_4")
            map.Add("SET_NAIYO_5", "SET_NAIYO_5")
            map.Add("SET_NAIYO_6", "SET_NAIYO_6")
            map.Add("GOODS_NM_2", "GOODS_NM_2")
            map.Add("KICHO_KB", "KICHO_KB")
            map.Add("SEI_YURAI_KB", "SEI_YURAI_KB")
            map.Add("REMARK_UPPER", "REMARK_UPPER")
            map.Add("REMARK_LOWER", "REMARK_LOWER")
            map.Add("GOODS_SYUBETU", "GOODS_SYUBETU")
            map.Add("YUKOU_KIGEN", "YUKOU_KIGEN")
            map.Add("GOODS_NM_3", "GOODS_NM_3")
            map.Add("EDISHIP_CD", "EDISHIP_CD")
            map.Add("EDIDEST_CD", "EDIDEST_CD")
            '日医工対策終了
            map.Add("SET_NAIYO_FROM1", "SET_NAIYO_FROM1")
            map.Add("SET_NAIYO_FROM2", "SET_NAIYO_FROM2")
            map.Add("SET_NAIYO_FROM3", "SET_NAIYO_FROM3")
            '【要望番号1122】埼玉対応 --- START ---
            map.Add("CUST_AD_3", "CUST_AD_3")
            map.Add("DEST_REMARK", "DEST_REMARK")
            map.Add("DEST_SALES_CD", "DEST_SALES_CD")
            map.Add("DEST_SALES_NM", "DEST_SALES_NM")
            map.Add("DEST_SALES_AD_1", "DEST_SALES_AD_1")
            map.Add("DEST_SALES_AD_2", "DEST_SALES_AD_2")
            map.Add("DEST_SALES_AD_3", "DEST_SALES_AD_3")
            map.Add("DEST_SALES_TEL", "DEST_SALES_TEL")
            '【要望番号1122】埼玉対応 ---  END  ---

            '【要望番号1155】LMC519対応 --- START ---
            map.Add("CUST_NM_M", "CUST_NM_M")
            map.Add("DENPYO_NM", "DENPYO_NM")
            '【要望番号1155】LMC519対応 ---  END  ---

            '群馬(合算)端数行分割版向け対応(作業略称)
            If rptTbl.Rows(0).Item("RPT_ID").ToString() <> "LMC514" Then 'LMC514は、群馬で利用してるSQLを使ってないため。
                map.Add("SAGYO_RYAK_1", "SAGYO_RYAK_1")
                map.Add("SAGYO_RYAK_2", "SAGYO_RYAK_2")
                map.Add("SAGYO_RYAK_3", "SAGYO_RYAK_3")
                map.Add("SAGYO_RYAK_4", "SAGYO_RYAK_4")
                map.Add("SAGYO_RYAK_5", "SAGYO_RYAK_5")
                map.Add("DEST_SAGYO_RYAK_1", "DEST_SAGYO_RYAK_1")
                map.Add("DEST_SAGYO_RYAK_2", "DEST_SAGYO_RYAK_2")
                map.Add("SZ01_YUSO", "SZ01_YUSO")
                map.Add("SZ01_UNSO", "SZ01_UNSO")
            End If
            '群馬(合算)端数行分割版向け対応(作業略称)

            '(2012.10.19)要望番号1289 -- START --
            '(2016.07.12)千葉標準+バーコード(LMC819) 追加
            If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC621" Or
           rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC628" Or
           rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC629" Or
           rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC885" Or
           rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC631" Or
           rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC633" Or
           rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC634" Or
           rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC876" Or
           rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC635" Or
           rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC636" Or
           rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC637" Or
           rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC638" Or
           rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC639" Or
           rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC732" Or
           rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC733" Or
           rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC734" Or
           rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC819" Then
                map.Add("GOODS_COND_CD_1", "GOODS_COND_CD_1")
                map.Add("GOODS_COND_CD", "GOODS_COND_CD")
                map.Add("GOODS_COND_CD_3", "GOODS_COND_CD_3")
                map.Add("REMARK_ZAI", "REMARK_ZAI")
                map.Add("LT_DATE", "LT_DATE")
                map.Add("SET_NAIYO", "SET_NAIYO")
                map.Add("SYS_UPD_PGID", "SYS_UPD_PGID")
            End If
            '(2012.10.19)要望番号1289 --  END  --
#If True Then   'ADD 2020/12/02 
            If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC621" Then
                map.Add("CUST_CD_M", "CUST_CD_M")
                map.Add("CUST_CD_S", "CUST_CD_S")
            End If
#End If
            '(2014.05.29)要望番号2193 -- START --
            If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC753" Then
                map.Add("GOODS_COND_NM", "GOODS_COND_NM")
            End If
            '(2014.05.29)要望番号2193 --  END  --

            If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC629" OrElse
           rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC885" Then
                map.Add("SHOBO_KBN_NM", "SHOBO_KBN_NM")
                map.Add("HINMEI", "HINMEI")
                '(2012.11.21)要望番号1615 -- START --
                map.Add("PKG_WT", "PKG_WT")
                '(2012.11.21)要望番号1615 --  END  --
                '(2016.7.13) LMC629:製造日表示対応 -- START -- 
                map.Add("GOODS_CRT_DATE", "GOODS_CRT_DATE")
                '(2016.7.13) LMC629:製造日表示対応 --  END  -- 
            End If

            '(2012.11.28) LMC637対応 -- START --
            If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC637" Then
                map.Add("CUST_CD_M", "CUST_CD_M")
                map.Add("CUST_CD_S", "CUST_CD_S")
                map.Add("CUST_COST_CD2", "CUST_COST_CD2")
            End If
            '(2012.11.28) LMC637対応 --  END  --

            '(2012.12.17)LMC731対応 -- START --
            If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC731" Then
                map.Add("SEARCH_KEY_2", "SEARCH_KEY_2")
            End If
            '(2012.12.17)LMC731対応 --  END  --

            '【要望番号2068】デュポンテフロン対応 -- START --
            If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC516" Then
                map.Add("RUIJI_GOODS_FLG", "RUIJI_GOODS_FLG")
                map.Add("DUPONT_YORYO", "DUPONT_YORYO")
            End If
            '【要望番号2068】デュポンテフロン対応 --  END  --

            If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC899" Then
                map.Add("HAISOU_REMARK_MST", "HAISOU_REMARK_MST")
            End If

            If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC626" Then
                map.Add("SHOBO_CD", "SHOBO_CD")
            End If

            If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC875" Then
                map.Add("SHOBO_CD", "SHOBO_CD")
            End If

            If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC872" Then
                map.Add("WH_SIJI_REMARK", "WH_SIJI_REMARK")
            End If

            If rptTbl.Rows(0).Item("RPT_ID").ToString() = "LMC899" Then
                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC899OUT")
            Else
                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMC500OUT")
            End If


        End If

        '(2012.03.03) LMC513対応 --  END  ---------------------------------------------

        Return ds

    End Function

    ''LMC514対応  開始

    '''' <summary>
    ''''荷主明細マスタ(設定値)取得処理
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>String</returns>
    '''' <remarks>荷主明細マスタ取得SQLの構築・発行</remarks>
    'Private Function SelectMCustDetailsData(ByVal ds As DataSet) As DataSet

    '    'INTableの条件rowの格納
    '    Me._Row = ds.Tables("LMC500IN").Rows(0)

    '    'SQL格納変数の初期化
    '    Me._StrSql = New StringBuilder()

    '    'SQL作成
    '    Me._StrSql.Append(LMC500DAC_Const1.SQL_SELECT_MCUST_DETAILS)      'SQL構築(荷主明細マスタ設定値Select句)
    '    Call Me.setIndataParameter(Me._Row)                        '条件設定

    '    'スキーマ名設定
    '    Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

    '    'SQL文のコンパイル
    '    Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

    '    'パラメータの反映
    '    For Each obj As Object In Me._SqlPrmList
    '        cmd.Parameters.Add(obj)
    '    Next

    '    MyBase.Logger.WriteSQLLog("LMC500DAC", "SelectMCustDetailsData", cmd)

    '    'SQLの発行
    '    Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

    '    Dim map As Hashtable = New Hashtable()

    '    '取得データの格納先をマッピング
    '    map.Add("MCD_SET_NAIYO", "MCD_SET_NAIYO")
    '    map.Add("MCD_SET_NAIYO_2", "MCD_SET_NAIYO_2")
    '    map.Add("MCD_SET_NAIYO_3", "MCD_SET_NAIYO_3")

    '    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "MCD_SET_NAIYO")

    '    reader.Close()

    '    Return ds

    'End Function

    '2次対応 荷姿並び替え 2012.01.17 END


    '(2012.04.10) Notes№962 FREE_C03使用有無対応 -- START --
    ''' <summary>
    '''荷主明細マスタ(設定値)取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>String</returns>
    ''' <remarks>荷主明細マスタ取得SQLの構築・発行</remarks>
    Private Function SelectMCustDetailsData(ByVal ds As DataSet) As DataSet

        'INTableの条件rowの格納
        Me._Row = ds.Tables("LMC500IN").Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_MCUST_DETAILS)      'SQL構築(荷主明細マスタ設定値Select句)
        Call Me.setIndataParameter(Me._Row)                        '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)
        '20120613----------------------------
        cmd.CommandTimeout = 6000
        '------------------------------------
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMC500DAC", "SelectMCustDetailsData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SET_NAIYO", "SET_NAIYO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "SET_NAIYO")

        reader.Close()

        Return ds

    End Function
    '(2012.04.10) Notes№962 FREE_C03使用有無対応 --  END  --

    '2015.09.14 配送データ対応START
    ''' ==========================================================================
    ''' <summary>キーコードの取得</summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    ''' ==========================================================================
    Public Function SearchKeyCode(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMC500IN").Rows(0)
        Dim arrParam As New ArrayList

        'INTableの条件rowの格納
        Me._Row = dr

        'SQL作成
        Me._StrSql = New StringBuilder

        'SQL
        Me._StrSql.Append(LMC500DAC_Const2.SQL_SELECT_KEY_DATA)

        '参照スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())
        'SQL文のコンパイル
        Dim cmd As SqlCommand = Me.CreateSqlCommand(sql.ToString())

        Logger.WriteSQLLog("LMC500DAC", "SearchKeyCode", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = Me.GetSelectResult(cmd)

        Dim map As Hashtable = New Hashtable

        map.Add("KEY_CODE", "KEY_CODE")

        ds = Me.SetSelectResultToDataSet(map, ds, reader, "KEY_CODE")

        Return ds

    End Function
    '2015.09.14 配送データ対応END

    ''' <summary>
    ''' 納品書摘要欄追加情報(TSMC在庫データ・シリンダーNo.) 取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectZaiTsmcCylinderNo(ByVal ds As DataSet) As DataSet

        ' DataSet の IN情報の取得
        Dim inTbl As DataTable = ds.Tables("LMC899OUT")

        ' IN Tableの条件 row の格納
        Me._Row = inTbl.Rows(0)

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        ' SQL作成
        Me._StrSql.Append(LMC500DAC_Const3.SQL_SELECT_ZAI_TSMC_CYLINDER_NO)

        ' スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        ' SQLパラメータ初期化・設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", Me._Row.Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", Me._Row.Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                Dim map As Hashtable = New Hashtable()

                '取得データの格納先をマッピング
                map.Add("NRS_BR_CD", "NRS_BR_CD")
                map.Add("OUTKA_NO_L", "OUTKA_NO_L")
                map.Add("OUTKA_NO_M", "OUTKA_NO_M")
                map.Add("CYLINDER_NO", "CYLINDER_NO")
                map.Add("USE_FLG", "USE_FLG")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "D_ZAI_TSMC")

            End Using

            cmd.Parameters.Clear()

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 帳票種別取得条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQLMprt(ByVal ptnFlag As String)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            If ptnFlag = "0" Then '出荷

                '営業所
                whereStr = .Item("NRS_BR_CD").ToString()
                Me._StrSql.Append(" AND OUTL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

                '出荷管理番号
                whereStr = .Item("KANRI_NO_L").ToString()
                Me._StrSql.Append(" AND OUTL.OUTKA_NO_L = @KANRI_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_NO_L", whereStr, DBDataType.CHAR))

#If True Then   'ADD 2022/04/26  028723 千葉BC　一括印刷で印刷する帳票について
                '一括フラグ
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IKKATU_FLG", .Item("IKKATU_FLG").ToString(), DBDataType.CHAR))
#End If
            ElseIf ptnFlag = "1" Then '運送

                '営業所
                whereStr = .Item("NRS_BR_CD").ToString()
                Me._StrSql.Append(" AND UNSOL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

                '運送管理番号
                whereStr = .Item("KANRI_NO_L").ToString()
                Me._StrSql.Append(" AND UNSOL.UNSO_NO_L = @KANRI_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_NO_L", whereStr, DBDataType.CHAR))

            Else 'とりあえず出荷

                '営業所
                whereStr = .Item("NRS_BR_CD").ToString()
                Me._StrSql.Append(" AND OUTL.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

                '出荷管理番号
                whereStr = .Item("KANRI_NO_L").ToString()
                Me._StrSql.Append(" AND OUTL.OUTKA_NO_L = @KANRI_NO_L")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_NO_L", whereStr, DBDataType.CHAR))

            End If

        End With

    End Sub

    ''' <summary>
    ''' 帳票種別取得条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL(Optional ByVal rptId As String = "")  '2018/11/13 要望番号002713 引数を追加

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '管理番号
            whereStr = .Item("KANRI_NO_L").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KANRI_NO_L", whereStr, DBDataType.CHAR))

            'パターンフラグ('0':出荷、'1':運送)
            whereStr = .Item("PTN_FLAG").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PTN_FLAG", whereStr, DBDataType.CHAR))

            '(2012.03.03) 再発行フラグ追加 LMC513対応 -- START --
            '再発行フラグ
            'whereStr = .Item("SAIHAKKO_FLG").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAIHAKKO_FLG", .Item("SAIHAKKO_FLG").ToString(), DBDataType.CHAR))
            '(2012.03.03) 再発行フラグ追加 LMC513対応 --  END  --

            'ADD START 2018/11/13 要望番号002713
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@RPT_ID", rptId, DBDataType.CHAR))
            'ADD END   2018/11/13 要望番号002713

            'ADD 2022/04/26 028723
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IKKATU_FLG", .Item("IKKATU_FLG").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 帳票種別取得条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL_SAIHAKKO()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '再発行フラグ
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAIHAKKO_FLG", .Item("SAIHAKKO_FLG").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    '''  荷主明細マスタ用(LMC514)
    ''' </summary>
    ''' <remarks>荷主明細マスタ存在抽出用SQLの構築</remarks>
    Private Sub setIndataParameter(ByVal _Row As DataRow)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("KANRI_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        End With

    End Sub

#End Region

#Region "設定処理"

#Region "SQL"

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region

#End Region

#End Region

#Region "CustControl"

    '#Region "Feild"

    '    ''' <summary>
    '    ''' 荷主コード保持用
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private _custDs As DataSet

    '#End Region

    '    ''' <summary>
    '    ''' 荷主コード取得処理
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub CustControl()

    '        If _custDs Is Nothing = True Then

    '            Me.CreatecustDataSet()

    '            Me.SetConnectDataSet(_custDs)

    '        End If

    '    End Sub

    '    ''' <summary>
    '    ''' 荷主コード取得用テーブル作成
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub CreatecustDataSet()

    '        '荷主コード取得用テーブル作成
    '        _custDs = New DataSet
    '        Dim dt As DataTable = New DataTable
    '        _custDs.Tables.Add(dt)
    '        _custDs.Tables(0).TableName = "GET_CUST_CD"

    '        For i As Integer = 0 To 1
    '            _custDs.Tables("GET_CUST_CD").Columns.Add(SetCol(i))
    '        Next

    '    End Sub

    '    ''' <summary>
    '    ''' 荷主コード取得用テーブル設定
    '    ''' </summary>
    '    ''' <param name="colno"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function SetCol(ByVal colno As Integer) As DataColumn
    '        Dim col As DataColumn = New DataColumn
    '        Dim colname As String = String.Empty
    '        col = New DataColumn
    '        Select Case colno
    '            Case 0
    '                colname = "NRS_BR_CD"
    '            Case 1
    '                colname = "CUST_CD_L"
    '        End Select

    '        col.ColumnName = colname
    '        col.Caption = colname

    '        Return col
    '    End Function

    '    ''' <summary>
    '    ''' 出荷データの荷主コードを取得
    '    ''' </summary>
    '    ''' <param name="ds"></param>
    '    ''' <remarks></remarks>
    '    Private Sub SetConnectDataSet(ByVal ds As DataSet)

    '        'SQL格納変数の初期化
    '        Me._StrSql = New StringBuilder()

    '        'SQL作成
    '        Call Me.SQLGetConnection()
    '        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)

    '        'スキーマ名設定
    '        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

    '        'SQL文のコンパイル
    '        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me._StrSql.ToString())

    '        MyBase.Logger.WriteSQLLog("LMNControlDAC", "SQLGetConnection", cmd)

    '        'SQLの発行
    '        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

    '        'パラメータの反映
    '        For Each obj As Object In Me._SqlPrmList
    '            cmd.Parameters.Add(obj)
    '        Next

    '        'DataReader→DataTableへの転記
    '        Dim map As Hashtable = New Hashtable()

    '        '取得データの格納先をマッピング
    '        map.Add("NRS_BR_CD", "NRS_BR_CD")
    '        map.Add("CUST_CD_L", "CUST_CD_L")

    '        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "GET_CUST_CD")


    '    End Sub

    '    ''' <summary>
    '    '''荷主コード取得
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '    Private Sub SQLGetConnection()

    '        Me._StrSql.Append("SELECT       ")
    '        Me._StrSql.Append(vbNewLine)
    '        Me._StrSql.Append("  NRS_BR_CD	AS	NRS_BR_CD")
    '        Me._StrSql.Append(vbNewLine)
    '        Me._StrSql.Append(" ,CUST_CD_L	AS	CUST_CD_L")
    '        Me._StrSql.Append(vbNewLine)
    '        Me._StrSql.Append("FROM       ")
    '        Me._StrSql.Append(vbNewLine)

    '        '出荷画面から出力の場合、出荷データから取得
    '        If Me._Row.Item("PTN_FLAG").ToString() = "0" Then
    '            Me._StrSql.Append("$LM_TRN$..C_OUTKA_L       ")
    '            Me._StrSql.Append(vbNewLine)
    '            Me._StrSql.Append("WHERE       ")
    '            Me._StrSql.Append(vbNewLine)
    '            Me._StrSql.Append(" SYS_DEL_FLG = '0'       ")
    '            Me._StrSql.Append(vbNewLine)
    '            Me._StrSql.Append(" AND NRS_BR_CD = @NRS_BR_CD       ")
    '            Me._StrSql.Append(vbNewLine)
    '            Me._StrSql.Append(" AND OUTKA_NO_L = @KANRI_NO       ")
    '        Else  '運送画面から出力の場合、運送データから取得
    '            Me._StrSql.Append("$LM_TRN$..F_UNSO_L       ")
    '            Me._StrSql.Append(vbNewLine)
    '            Me._StrSql.Append("WHERE       ")
    '            Me._StrSql.Append(vbNewLine)
    '            Me._StrSql.Append(" SYS_DEL_FLG = '0'       ")
    '            Me._StrSql.Append(vbNewLine)
    '            Me._StrSql.Append(" AND NRS_BR_CD = @NRS_BR_CD       ")
    '            Me._StrSql.Append(vbNewLine)
    '            Me._StrSql.Append(" AND UNSO_NO_L = @KANRI_NO       ")
    '        End If

    '    End Sub

#End Region 'CustControl廃止

End Class

