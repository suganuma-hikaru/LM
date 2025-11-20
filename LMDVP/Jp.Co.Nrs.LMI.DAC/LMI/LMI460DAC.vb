' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI460  : 請求先コード変更
'  作  成  者       :  [daikoku]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI460DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI460DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "運賃データの更新"

#Region "運賃データの更新"

    ''' <summary>
    ''' 運賃データ更新　横浜専用です
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_SEIQTO_CD As String = " UPDATE                                                     " & vbNewLine _
                        & "  $LM_TRN$..F_UNCHIN_TRS                                  " & vbNewLine _
                        & " SET                                                      " & vbNewLine _
                        & "  SEIQTO_CD =                                             " & vbNewLine _
                        & "  (                                                       " & vbNewLine _
                        & "   CASE                                                   " & vbNewLine _
                        & "    --00061EO                                             " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001106'     and UM.GOODS_CD_NRS = 'Y0000000000000007553') THEN '00061EO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001106'     and UM.GOODS_CD_NRS = 'Y0000000000000007585') THEN '00061EO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001106'     and UM.GOODS_CD_NRS = 'Y0000000000000022711') THEN '00061EO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001106'     and UM.GOODS_CD_NRS = 'Y0000000000100002325') THEN '00061EO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001ﾕｳｾﾝ.'   and UM.GOODS_CD_NRS = 'Y0000000000000026113') THEN '00061EO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001ﾕｳｾﾝ.'   and UM.GOODS_CD_NRS = 'Y0000000000100000917') THEN '00061EO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001ﾕｳｾﾝ.'   and UM.GOODS_CD_NRS = 'Y0000000000100000991') THEN '00061EO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001ﾕｳｾﾝ.'   and UM.GOODS_CD_NRS = 'Y0000000000100023228') THEN '00061EO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001ﾕｳｾﾝ.'   and UM.GOODS_CD_NRS = 'Y0000000000100036087') THEN '00061EO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001ﾕｳｾﾝ.'   and UM.GOODS_CD_NRS = 'Y0000000000100041685') THEN '00061EO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = 'ｱﾙﾄ'        and UM.GOODS_CD_NRS = 'Y0000000000000007965') THEN '00061EO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = 'ｱﾙﾄ'        and UM.GOODS_CD_NRS = 'Y0000000000000008072') THEN '00061EO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = 'ｱﾙﾄ'        and UM.GOODS_CD_NRS = 'Y0000000000000019393') THEN '00061EO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = 'ｱﾙﾄ'        and UM.GOODS_CD_NRS = 'Y0000000000000022659') THEN '00061EO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = 'ｽｽﾞｷ'       and UM.GOODS_CD_NRS = 'Y0000000000000023559') THEN '00061EO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = 'ｽｽﾞｷ'       and UM.GOODS_CD_NRS = 'Y0000000000000023737') THEN '00061EO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = 'ｽｽﾞｷ'       and UM.GOODS_CD_NRS = 'Y0000000000000023775') THEN '00061EO' " & vbNewLine _
                        & "    --00061FO                                                                                                            " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001181.*'   and UM.GOODS_CD_NRS = 'Y0000000000000026112') THEN '00061FO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001181.*'   and UM.GOODS_CD_NRS = 'Y0000000000100027920') THEN '00061FO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001181.*'   and UM.GOODS_CD_NRS = 'Y0000000000100040282') THEN '00061FO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001ｻﾝﾘﾂ'    and UM.GOODS_CD_NRS = 'Y0000000000000007071') THEN '00061FO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001ｻﾝﾘﾂ'    and UM.GOODS_CD_NRS = 'Y0000000000000007375') THEN '00061FO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001ｻﾝﾘﾂ'    and UM.GOODS_CD_NRS = 'Y0000000000100040282') THEN '00061FO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001ﾕｳｾﾝ.'   and UM.GOODS_CD_NRS = 'Y0000000000000007071') THEN '00061FO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001ﾕｳｾﾝ.'   and UM.GOODS_CD_NRS = 'Y0000000000000007375') THEN '00061FO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001ﾕｳｾﾝ.'   and UM.GOODS_CD_NRS = 'Y0000000000000026187') THEN '00061FO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001ﾕｳｾﾝ.'   and UM.GOODS_CD_NRS = 'Y0000000000100039897') THEN '00061FO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '10243664'   and UM.GOODS_CD_NRS = 'Y0000000000000021867') THEN '00061FO' " & vbNewLine _
                        & "    --00061GO                                                                                                            " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000000006913') THEN '00061GO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000000007687') THEN '00061GO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000000007891') THEN '00061GO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000000020132') THEN '00061GO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000000024371') THEN '00061GO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000000024427') THEN '00061GO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000100019078') THEN '00061GO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000100020668') THEN '00061GO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000100022995') THEN '00061GO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000100025408') THEN '00061GO' " & vbNewLine _
                        & "    --00061HO                                                                                                            " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000000007202') THEN '00061HO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000000007203') THEN '00061HO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000000021110') THEN '00061HO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000000021153') THEN '00061HO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000000021228') THEN '00061HO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000000021435') THEN '00061HO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000000022270') THEN '00061HO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000000022933') THEN '00061HO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000100002152') THEN '00061HO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000100002787') THEN '00061HO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000100028181') THEN '00061HO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001108'     and UM.GOODS_CD_NRS = 'Y0000000000100031528') THEN '00061HO' " & vbNewLine _
                        & "    --00061GG                                                                                                            " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '861699'     and UM.GOODS_CD_NRS = 'Y0000000000100028219') THEN '00061GG' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '1827341'    and UM.GOODS_CD_NRS = 'Y0000000000100026665') THEN '00061GG' " & vbNewLine _
                        & "    --00061FO(REMARKが空)                                                                                                " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK = '' and UL.DEST_CD = '001ﾕｳｾﾝ.'    and UM.GOODS_CD_NRS = 'Y0000000000000007979') THEN '00061EO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK = '' and UL.DEST_CD = '001ﾕｳｾﾝ.'    and UM.GOODS_CD_NRS = 'Y0000000000000017316') THEN '00061FO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK = '' and UL.DEST_CD = '001ﾕｳｾﾝ.'    and UM.GOODS_CD_NRS = 'Y0000000000000022981') THEN '00061FO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK = '' and UL.DEST_CD = '001ﾕｳｾﾝ.'    and UM.GOODS_CD_NRS = 'Y0000000000100032913') THEN '00061FO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK = '' and UL.DEST_CD = '001ﾕｳｾﾝ.'    and UM.GOODS_CD_NRS = 'Y0000000000100034461') THEN '00061FO' " & vbNewLine _
                        & "    --00061A1                                                                                                            " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '1592241'    and UM.GOODS_CD_NRS = 'Y0000000000000018432') THEN '00061A1' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '1683635'    and UM.GOODS_CD_NRS = 'Y0000000000100032913') THEN '00061A1' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '1683635'    and UM.GOODS_CD_NRS = 'Y0000000000100033333') THEN '00061A1' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '1683635'    and UM.GOODS_CD_NRS = 'Y0000000000100032913') THEN '00061A1' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '10189219'   and UM.GOODS_CD_NRS = 'Y0000000000100037774') THEN '00061A1' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '10189219'   and UM.GOODS_CD_NRS = 'Y0000000000100029504') THEN '00061A1' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '10189219'   and UM.GOODS_CD_NRS = 'Y0000000000100040265') THEN '00061A1' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '10189219'   and UM.GOODS_CD_NRS = 'Y0000000000100038245') THEN '00061A1' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '10189219'   and UM.GOODS_CD_NRS = 'Y0000000000100043359') THEN '00061A1' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '10189219'   and UM.GOODS_CD_NRS = 'Y0000000000100043360') THEN '00061A1' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '1683635'    and UM.GOODS_CD_NRS = 'Y0000000000100043484') THEN '00061A1' " & vbNewLine _
                        & "    --00061B1                                                                                                            " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK = '' and UL.DEST_CD = '764577.'     and UM.GOODS_CD_NRS = 'Y0000000000100045570') THEN '00061B1' " & vbNewLine _
                        & "    --00061C1                                                                                                            " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK = '' and UL.DEST_CD = 'ﾒﾙﾃｯｸｸﾏｶﾞﾔ'  and UM.GOODS_CD_NRS = 'Y0000000000000014327') THEN '00061C1' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK = '' and UL.DEST_CD = 'ﾒﾙﾃｯｸｸﾏｶﾞﾔ'  and UM.GOODS_CD_NRS = 'Y0000000000000023533') THEN '00061C1' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK = '' and UL.DEST_CD = 'ﾒﾙﾃｯｸｸﾏｶﾞﾔ'  and UM.GOODS_CD_NRS = 'Y0000000000000022847') THEN '00061C1' " & vbNewLine _
                        & "    --00061EO                                                                                                            " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '001ﾕｳｾﾝ.'   and UM.GOODS_CD_NRS = 'Y0000000000100042614') THEN '00061EO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '3047500'    and UM.GOODS_CD_NRS = 'Y0000000000000008072') THEN '00061EO' " & vbNewLine _
                        & "    --00061DO(注意)                                                                                                      " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '3002355'    and UM.GOODS_CD_NRS = 'Y0000000000000006977') THEN '00061DO' " & vbNewLine _
                        & "    WHEN (UNCHIN.REMARK <> '' and UL.DEST_CD = '3013036'    and UM.GOODS_CD_NRS = 'Y0000000000100035376') THEN '00061DO' " & vbNewLine _
                        & "    --条件に合致しなければそのまま                                                                                       " & vbNewLine _
                        & "    ELSE UNCHIN.SEIQTO_CD                                                                                                " & vbNewLine _
                        & "   END                                                                                                                   " & vbNewLine _
                        & "  )                                                                                                                      " & vbNewLine _
                        & " , SYS_UPD_DATE     = @SYS_UPD_DATE             " & vbNewLine _
                        & " , SYS_UPD_TIME     = @SYS_UPD_TIME             " & vbNewLine _
                        & " , SYS_UPD_PGID     = @SYS_UPD_PGID             " & vbNewLine _
                        & " , SYS_UPD_USER     = @SYS_UPD_USER             " & vbNewLine _
                        & " FROM                                                                                                                    " & vbNewLine _
                        & "  $LM_TRN$..F_UNCHIN_TRS UNCHIN                                                                                          " & vbNewLine _
                        & " LEFT JOIN                                                                                                               " & vbNewLine _
                        & "  $LM_TRN$..F_UNSO_L UL                                                                                                  " & vbNewLine _
                        & " ON                                                                                                                      " & vbNewLine _
                        & "  UNCHIN.UNSO_NO_L = UL.UNSO_NO_L                                                                                        " & vbNewLine _
                        & "  and UNCHIN.NRS_BR_CD = UL.NRS_BR_CD                                                                                    " & vbNewLine _
                        & " LEFT JOIN                                                                                                               " & vbNewLine _
                        & "  $LM_TRN$..F_UNSO_M UM                                                                                                  " & vbNewLine _
                        & " ON                                                                                                                      " & vbNewLine _
                        & "  UNCHIN.NRS_BR_CD = UM.NRS_BR_CD                                                                                        " & vbNewLine _
                        & "  and UNCHIN.UNSO_NO_L = UM.UNSO_NO_L                                                                                    " & vbNewLine _
                        & "  and UNCHIN.UNSO_NO_M = UM.UNSO_NO_M                                                                                    " & vbNewLine _
                        & " WHERE                                                                                                                                                                  " & vbNewLine _
                        & "      UNCHIN.NRS_BR_CD    = @NRS_BR_CD                                                                                                                                  " & vbNewLine _
                        & "  and UL.CUST_CD_L        = @CUST_CD_L                                                                                                                                  " & vbNewLine _
                        & "  and UL.CUST_CD_M        = @CUST_CD_M                                                                                                                                  " & vbNewLine _
                        & "  and UNCHIN.SYS_DEL_FLG  = '0'                                                                                                                                         " & vbNewLine _
                        & "  and UL.SYS_DEL_FLG      = '0'                                                                                                                                         " & vbNewLine _
                        & "  and UM.SYS_DEL_FLG      = '0'                                                                                                                                         " & vbNewLine _
                        & "  and UNCHIN.SYS_DEL_FLG  = '0'                                                                                                                                         " & vbNewLine _
                        & "  and UNCHIN.UNTIN_CALCULATION_KB = '01'                                                                                                                                " & vbNewLine _
                        & "  and (UNCHIN.DECI_UNCHIN + UNCHIN.DECI_CITY_EXTC + UNCHIN.DECI_WINT_EXTC + UNCHIN.DECI_RELY_EXTC + UNCHIN.DECI_RELY_EXTC + UNCHIN.DECI_TOLL + UNCHIN.DECI_INSU) <> '0' " & vbNewLine _
                        & "  and UL.OUTKA_PLAN_DATE between @startdate and @enddate                                                                                                                " & vbNewLine

#End Region

#End Region

#Region "Field"

    '''' <summary>
    '''' 検索条件設定用
    '''' </summary>
    '''' <remarks></remarks>
    'Private _Row As Data.DataRow

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

    Private setWhereBr As Boolean
    Private setWhereCustL As Boolean
    Private setWhereCustM As Boolean
    Private setWhereCustS As Boolean
    Private setWhereCustSS As Boolean
    Private setWhereDateFrom As Boolean
    Private setWhereDateTo As Boolean
    Private setWhereDepart As Boolean


#End Region

#Region "SQLメイン処理"

#Region "運賃データ（作業料）の更新"

    ''' <summary>
    ''' 運賃データの更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdDataUnchin(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI460IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI460DAC.SQL_UPDATE_SEIQTO_CD)         'SQL構築

        'パラメータ設定
        Call Me.SetUntinParameter(inTbl.Rows(0), Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList, String.Empty)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString(), ds.Tables("LMI460IN").Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMI460DAC", "UpdateUnchin1", cmd)

        'SQLの発行
        'Me.UpdateResultChk(cmd) '0件でも実行する

        'SQLの発行
        Me.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#Region "SQL条件設定 共通"

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter(prmList, dataSetNm)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList, ByVal dataSetNm As String)

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))

    End Sub

    ''''' <summary>
    ''''' パラメータ設定モジュール(システム共通項目(登録時))
    ''''' </summary>
    ''''' <remarks></remarks>
    ''Private Sub SetParamCommonSystemIns()

    ''    'パラメータ設定
    ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
    ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
    ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
    ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
    ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
    ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
    ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
    ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
    ''    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

    ''End Sub

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String, ByVal mainBrCd As String) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        'デュポン業務主営業所トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN_DPN$", MyBase.GetDatabaseName(mainBrCd, DBKbn.TRN))

        Return sql

    End Function

    ''' <summary>
    ''' 運賃データの更新パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUntinParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@startdate", .Item("DATE_FROM").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@enddate", .Item("DATE_TO").ToString(), DBDataType.CHAR))

        End With

    End Sub


#End Region

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E483", New String() {"更新"}) '2013.03.07エラーメッセージ変更
            Return False
        End If

        Return True

    End Function

#End Region

End Class
