' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタ
'  プログラムID     :  LMM280DAC : 横持ちマスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM280DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM280DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "編集処理 SQL"

    ''' <summary>
    ''' 横持ちタリフマスタ排他チェック処理(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_HAITA_CHK As String = "SELECT                                                  " & vbNewLine _
                                                & "       COUNT(YOKO_TARIFF_CD)     AS    SELECT_CNT " & vbNewLine _
                                                & "FROM                                              " & vbNewLine _
                                                & "     $LM_MST$..M_YOKO_TARIFF_HD    HED            " & vbNewLine _
                                                & "WHERE                                             " & vbNewLine _
                                                & "       NRS_BR_CD          =    @NRS_BR_CD         " & vbNewLine _
                                                & "AND    YOKO_TARIFF_CD     =    @YOKO_TARIFF_CD    " & vbNewLine _
                                                & "AND    SYS_UPD_DATE       =    @HAITA_DATE        " & vbNewLine _
                                                & "AND    SYS_UPD_TIME       =    @HAITA_TIME        " & vbNewLine


#End Region

#Region "検索処理 SQL"

#Region "横持ちタリフヘッダマスタ"

    ''' <summary>
    ''' 横持ちタリフヘッダマスタ検索処理(件数取得(SELECT句))用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_SELECT As String = " SELECT COUNT(HED.NRS_BR_CD)	   AS SELECT_CNT   " & vbNewLine


    ''' <summary>
    ''' 横持ちタリフヘッダマスタ検索処理(データ取得(SELECT句))用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_SELECT As String = " SELECT                                                         " & vbNewLine _
                                                   & "     HED.NRS_BR_CD           AS    NRS_BR_CD                    " & vbNewLine _
                                                   & "    ,BRM.NRS_BR_NM           AS    NRS_BR_NM                    " & vbNewLine _
                                                   & "    ,HED.YOKO_TARIFF_CD      AS    YOKO_TARIFF_CD               " & vbNewLine _
                                                   & "    ,HED.YOKO_REM            AS    YOKO_REM                     " & vbNewLine _
                                                   & "    ,HED.CALC_KB             AS    CALC_KB                      " & vbNewLine _
                                                   & "    ,KBN1.KBN_NM1            AS    CALC_KB_NM	                  " & vbNewLine _
                                                   & "    ,HED.SPLIT_FLG           AS    SPLIT_FLG                    " & vbNewLine _
                                                   & "    ,KBN2.KBN_NM1            AS    SPLIT_FLG_NM	              " & vbNewLine _
                                                   & "    ,HED.YOKOMOCHI_MIN       AS    YOKOMOCHI_MIN                " & vbNewLine _
                                                   & "    ,HED.SYS_ENT_DATE        AS    SYS_ENT_DATE                 " & vbNewLine _
                                                   & "    ,USE1.USER_NM            AS    SYS_ENT_USER_NM              " & vbNewLine _
                                                   & "    ,HED.SYS_UPD_DATE        AS    SYS_UPD_DATE                 " & vbNewLine _
                                                   & "    ,HED.SYS_UPD_TIME        AS    SYS_UPD_TIME                 " & vbNewLine _
                                                   & "    ,USE2.USER_NM            AS    SYS_UPD_USER_NM              " & vbNewLine _
                                                   & "    ,HED.SYS_DEL_FLG         AS    SYS_DEL_FLG                  " & vbNewLine _
                                                   & "    ,KBN3.KBN_NM1            AS    SYS_DEL_NM	                  " & vbNewLine

    ''' <summary>
    ''' 横持ちタリフヘッダマスタ検索処理(データ取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_FROM As String = " FROM                                               " & vbNewLine _
                                                 & "    $LM_MST$..M_YOKO_TARIFF_HD    HED               " & vbNewLine _
                                                 & "LEFT JOIN $LM_MST$..M_NRS_BR    BRM                 " & vbNewLine _
                                                 & "ON  BRM.NRS_BR_CD         =    HED.NRS_BR_CD        " & vbNewLine _
                                                 & "AND BRM.SYS_DEL_FLG       =    '0'                  " & vbNewLine _
                                                 & "LEFT JOIN $LM_MST$..S_USER      USE1                " & vbNewLine _
                                                 & "ON  USE1.USER_CD          =    HED.SYS_ENT_USER     " & vbNewLine _
                                                 & "AND USE1.SYS_DEL_FLG      =    '0'                  " & vbNewLine _
                                                 & "LEFT JOIN $LM_MST$..S_USER      USE2                " & vbNewLine _
                                                 & "ON  USE2.USER_CD          =    HED.SYS_UPD_USER     " & vbNewLine _
                                                 & "AND USE2.SYS_DEL_FLG      =    '0'                  " & vbNewLine _
                                                 & "LEFT JOIN $LM_MST$..Z_KBN      KBN1                 " & vbNewLine _
                                                 & "ON  KBN1.KBN_GROUP_CD     =    'K012'               " & vbNewLine _
                                                 & "AND KBN1.KBN_CD           =    HED.CALC_KB          " & vbNewLine _
                                                 & "AND KBN1.SYS_DEL_FLG      =    '0'                  " & vbNewLine _
                                                 & "LEFT JOIN $LM_MST$..Z_KBN      KBN2                 " & vbNewLine _
                                                 & "ON  KBN2.KBN_GROUP_CD     =    'U009'               " & vbNewLine _
                                                 & "AND KBN2.KBN_CD           =    HED.SPLIT_FLG        " & vbNewLine _
                                                 & "AND KBN2.SYS_DEL_FLG      =    '0'                  " & vbNewLine _
                                                 & "LEFT JOIN $LM_MST$..Z_KBN      KBN3                 " & vbNewLine _
                                                 & "ON  KBN3.KBN_GROUP_CD     =    'S051'               " & vbNewLine _
                                                 & "AND KBN3.KBN_CD           =    HED.SYS_DEL_FLG      " & vbNewLine _
                                                 & "AND KBN3.SYS_DEL_FLG      =    '0'                  " & vbNewLine

    ''' <summary>
    ''' 並び順
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                   " & vbNewLine _
                                         & "    HED.NRS_BR_CD          " & vbNewLine _
                                         & "   ,HED.YOKO_TARIFF_CD     " & vbNewLine
  
#End Region

#Region "横持ちタリフ明細マスタ"

    ''' <summary>
    ''' 横持ちタリフ明細マスタ検索処理(データ取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DTL_DATA As String = " SELECT                                                   " & vbNewLine _
                                                & "     DTL.NRS_BR_CD             AS    NRS_BR_CD            " & vbNewLine _
                                                & "    ,DTL.YOKO_TARIFF_CD        AS    YOKO_TARIFF_CD       " & vbNewLine _
                                                & "    ,DTL.YOKO_TARIFF_CD_EDA    AS    YOKO_TARIFF_CD_EDA   " & vbNewLine _
                                                & "    ,DTL.CARGO_KB              AS    CARGO_KB             " & vbNewLine _
                                                & "    ,DTL.CAR_KB                AS    CAR_KB               " & vbNewLine _
                                                & "    ,DTL.WT_LV                 AS    WT_LV                " & vbNewLine _
                                                & "    ,DTL.DANGER_KB             AS    DANGER_KB            " & vbNewLine _
                                                & "    ,DTL.UT_PRICE              AS    UT_PRICE             " & vbNewLine _
                                                & "    ,DTL.KGS_PRICE             AS    KGS_PRICE            " & vbNewLine _
                                                & "FROM                                                      " & vbNewLine _
                                                & "    $LM_MST$..M_YOKO_TARIFF_DTL    DTL                    " & vbNewLine


#End Region

#End Region

#Region "削除/復活処理 SQL"

    ''' <summary>
    ''' 横持ちタリフヘッダマスタ更新SQL(論理削除)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_DEL_HED As String = "UPDATE                                                " & vbNewLine _
                                            & "    $LM_MST$..M_YOKO_TARIFF_HD                        " & vbNewLine _
                                            & "SET                                                   " & vbNewLine _
                                            & "      SYS_UPD_DATE            =    @SYS_UPD_DATE      " & vbNewLine _
                                            & "     ,SYS_UPD_TIME            =    @SYS_UPD_TIME      " & vbNewLine _
                                            & "     ,SYS_UPD_PGID            =    @SYS_UPD_PGID      " & vbNewLine _
                                            & "     ,SYS_UPD_USER            =    @SYS_UPD_USER      " & vbNewLine _
                                            & "     ,SYS_DEL_FLG             =    @SYS_DEL_FLG       " & vbNewLine _
                                            & "WHERE                                                 " & vbNewLine _
                                            & "      NRS_BR_CD               =    @NRS_BR_CD         " & vbNewLine _
                                            & "AND   YOKO_TARIFF_CD          =    @YOKO_TARIFF_CD    " & vbNewLine _
                                            & "AND   SYS_UPD_DATE            =    @HAITA_DATE        " & vbNewLine _
                                            & "AND   SYS_UPD_TIME            =    @HAITA_TIME        " & vbNewLine


    ''' <summary>
    ''' 横持ちタリフ明細マスタ更新SQL(論理削除)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPD_DEL_DTL As String = "UPDATE                                                " & vbNewLine _
                                            & "    $LM_MST$..M_YOKO_TARIFF_DTL                       " & vbNewLine _
                                            & "SET                                                   " & vbNewLine _
                                            & "      SYS_UPD_DATE            =    @SYS_UPD_DATE      " & vbNewLine _
                                            & "     ,SYS_UPD_TIME            =    @SYS_UPD_TIME      " & vbNewLine _
                                            & "     ,SYS_UPD_PGID            =    @SYS_UPD_PGID      " & vbNewLine _
                                            & "     ,SYS_UPD_USER            =    @SYS_UPD_USER      " & vbNewLine _
                                            & "     ,SYS_DEL_FLG             =    @SYS_DEL_FLG       " & vbNewLine _
                                            & "WHERE                                                 " & vbNewLine _
                                            & "      NRS_BR_CD               =    @NRS_BR_CD         " & vbNewLine _
                                            & "AND   YOKO_TARIFF_CD          =    @YOKO_TARIFF_CD    " & vbNewLine



#End Region

#Region "保存処理 SQL"

    ''' <summary>
    ''' 横持ちタリフヘッダマスタ重複チェック処理(件数取得)用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_REPEAT_HED As String = "SELECT                                             " & vbNewLine _
                                            & "       COUNT(YOKO_TARIFF_CD)     AS    SELECT_CNT " & vbNewLine _
                                            & "FROM                                              " & vbNewLine _
                                            & "     $LM_MST$..M_YOKO_TARIFF_HD                   " & vbNewLine _
                                            & "WHERE                                             " & vbNewLine _
                                            & "       NRS_BR_CD          =    @NRS_BR_CD         " & vbNewLine _
                                            & "AND    YOKO_TARIFF_CD     =    @YOKO_TARIFF_CD    " & vbNewLine

#Region "新規処理 SQL"

    ''' <summary>
    ''' 横持ちタリフヘッダマスタ新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_HED_M As String = "INSERT INTO                          " & vbNewLine _
                                             & "    $LM_MST$..M_YOKO_TARIFF_HD       " & vbNewLine _
                                             & "    (                                " & vbNewLine _
                                             & "      NRS_BR_CD                      " & vbNewLine _
                                             & "     ,YOKO_TARIFF_CD                 " & vbNewLine _
                                             & "     ,YOKO_REM                       " & vbNewLine _
                                             & "     ,CALC_KB                        " & vbNewLine _
                                             & "     ,SPLIT_FLG                      " & vbNewLine _
                                             & "     ,YOKOMOCHI_MIN                  " & vbNewLine _
                                             & "     ,SYS_ENT_DATE                   " & vbNewLine _
                                             & "     ,SYS_ENT_TIME                   " & vbNewLine _
                                             & "     ,SYS_ENT_PGID                   " & vbNewLine _
                                             & "     ,SYS_ENT_USER                   " & vbNewLine _
                                             & "     ,SYS_UPD_DATE                   " & vbNewLine _
                                             & "     ,SYS_UPD_TIME                   " & vbNewLine _
                                             & "     ,SYS_UPD_PGID                   " & vbNewLine _
                                             & "     ,SYS_UPD_USER                   " & vbNewLine _
                                             & "     ,SYS_DEL_FLG                    " & vbNewLine _
                                             & "    )                                " & vbNewLine _
                                             & "VALUES                               " & vbNewLine _
                                             & "    (                                " & vbNewLine _
                                             & "      @NRS_BR_CD                     " & vbNewLine _
                                             & "     ,@YOKO_TARIFF_CD                " & vbNewLine _
                                             & "     ,@YOKO_REM                      " & vbNewLine _
                                             & "     ,@CALC_KB                       " & vbNewLine _
                                             & "     ,@SPLIT_FLG                     " & vbNewLine _
                                             & "     ,@YOKOMOCHI_MIN                 " & vbNewLine _
                                             & "     ,@SYS_ENT_DATE                  " & vbNewLine _
                                             & "     ,@SYS_ENT_TIME                  " & vbNewLine _
                                             & "     ,@SYS_ENT_PGID                  " & vbNewLine _
                                             & "     ,@SYS_ENT_USER                  " & vbNewLine _
                                             & "     ,@SYS_UPD_DATE                  " & vbNewLine _
                                             & "     ,@SYS_UPD_TIME                  " & vbNewLine _
                                             & "     ,@SYS_UPD_PGID                  " & vbNewLine _
                                             & "     ,@SYS_UPD_USER                  " & vbNewLine _
                                             & "     ,@SYS_DEL_FLG                   " & vbNewLine _
                                             & "    )                                " & vbNewLine


    ''' <summary>
    ''' 横持ちタリフ明細マスタ新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_DTL As String = "INSERT INTO                       " & vbNewLine _
                                           & "    $LM_MST$..M_YOKO_TARIFF_DTL   " & vbNewLine _
                                           & "    (                             " & vbNewLine _
                                           & "      NRS_BR_CD                   " & vbNewLine _
                                           & "     ,YOKO_TARIFF_CD              " & vbNewLine _
                                           & "     ,YOKO_TARIFF_CD_EDA          " & vbNewLine _
                                           & "     ,CARGO_KB                    " & vbNewLine _
                                           & "     ,CAR_KB                      " & vbNewLine _
                                           & "     ,WT_LV                       " & vbNewLine _
                                           & "     ,DANGER_KB                   " & vbNewLine _
                                           & "     ,UT_PRICE                    " & vbNewLine _
                                           & "     ,KGS_PRICE                   " & vbNewLine _
                                           & "     ,SYS_ENT_DATE                " & vbNewLine _
                                           & "     ,SYS_ENT_TIME                " & vbNewLine _
                                           & "     ,SYS_ENT_PGID                " & vbNewLine _
                                           & "     ,SYS_ENT_USER                " & vbNewLine _
                                           & "     ,SYS_UPD_DATE                " & vbNewLine _
                                           & "     ,SYS_UPD_TIME                " & vbNewLine _
                                           & "     ,SYS_UPD_PGID                " & vbNewLine _
                                           & "     ,SYS_UPD_USER                " & vbNewLine _
                                           & "     ,SYS_DEL_FLG                 " & vbNewLine _
                                           & "    )                             " & vbNewLine _
                                           & "VALUES                            " & vbNewLine _
                                           & "    (                             " & vbNewLine _
                                           & "      @NRS_BR_CD                  " & vbNewLine _
                                           & "     ,@YOKO_TARIFF_CD             " & vbNewLine _
                                           & "     ,@YOKO_TARIFF_CD_EDA         " & vbNewLine _
                                           & "     ,@CARGO_KB                   " & vbNewLine _
                                           & "     ,@CAR_KB                     " & vbNewLine _
                                           & "     ,@WT_LV                      " & vbNewLine _
                                           & "     ,@DANGER_KB                  " & vbNewLine _
                                           & "     ,@UT_PRICE                   " & vbNewLine _
                                           & "     ,@KGS_PRICE                  " & vbNewLine _
                                           & "     ,@SYS_ENT_DATE               " & vbNewLine _
                                           & "     ,@SYS_ENT_TIME               " & vbNewLine _
                                           & "     ,@SYS_ENT_PGID               " & vbNewLine _
                                           & "     ,@SYS_ENT_USER               " & vbNewLine _
                                           & "     ,@SYS_UPD_DATE               " & vbNewLine _
                                           & "     ,@SYS_UPD_TIME               " & vbNewLine _
                                           & "     ,@SYS_UPD_PGID               " & vbNewLine _
                                           & "     ,@SYS_UPD_USER               " & vbNewLine _
                                           & "     ,@SYS_DEL_FLG                " & vbNewLine _
                                           & "    )                             " & vbNewLine

#End Region

#Region "更新処理 SQL"

    ''' <summary>
    ''' 横持ちタリフヘッダマスタ更新SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_HED As String = "UPDATE                                             " & vbNewLine _
                                           & "    $LM_MST$..M_YOKO_TARIFF_HD                     " & vbNewLine _
                                           & "SET                                                " & vbNewLine _
                                           & "      YOKO_REM          =    @YOKO_REM             " & vbNewLine _
                                           & "     ,CALC_KB           =    @CALC_KB              " & vbNewLine _
                                           & "     ,SPLIT_FLG         =    @SPLIT_FLG            " & vbNewLine _
                                           & "     ,YOKOMOCHI_MIN     =    @YOKOMOCHI_MIN        " & vbNewLine _
                                           & "     ,SYS_UPD_DATE      =    @SYS_UPD_DATE         " & vbNewLine _
                                           & "     ,SYS_UPD_TIME      =    @SYS_UPD_TIME         " & vbNewLine _
                                           & "     ,SYS_UPD_PGID      =    @SYS_UPD_PGID         " & vbNewLine _
                                           & "     ,SYS_UPD_USER      =    @SYS_UPD_USER         " & vbNewLine _
                                           & "WHERE NRS_BR_CD         =    @NRS_BR_CD            " & vbNewLine _
                                           & "AND   YOKO_TARIFF_CD    =    @YOKO_TARIFF_CD       " & vbNewLine _
                                           & "AND   SYS_UPD_DATE      =    @HAITA_DATE           " & vbNewLine _
                                           & "AND   SYS_UPD_TIME      =    @HAITA_TIME           " & vbNewLine

    ''' <summary>
    ''' 横持ちタリフ明細マスタ物理削除SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_DTL As String = "DELETE FROM $LM_MST$..M_YOKO_TARIFF_DTL       " & vbNewLine _
                                           & "WHERE   NRS_BR_CD         = @NRS_BR_CD        " & vbNewLine _
                                           & "AND     YOKO_TARIFF_CD    = @YOKO_TARIFF_CD   " & vbNewLine


#End Region

#End Region

#End Region

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

#Region "編集処理"

    ''' <summary>
    ''' 横持ちタリフマスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>横持ちタリフマスタ検索結果取得SQLの構築・発行</remarks>
    Private Function HaitaChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM280IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM280DAC.SQL_HAITA_CHK)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()) _
                                                                        )

        'SQLパラメータ初期化/設定
        Call Me.SetParamHaitaChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM280DAC", "HaitaChk", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("SELECT_CNT")))
        reader.Close()

        'エラーメッセージの設定
        If MyBase.GetResultCount() < 1 Then
            MyBase.SetMessage("E011")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(横持ちタリフマスタ排他チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaitaChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '横持ちタリフヘッダマスタ主キー
        Call Me.SetParamPrimaryKeyHedM()

        '排他項目
        Call Me.SetParamHaita()

    End Sub

#End Region

#Region "削除/復活処理"

    ''' <summary>
    ''' 横持ちタリフヘッダマスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>横持ちタリフヘッダマスタ更新SQLの構築・発行</remarks>
    Private Function DeleteHedData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM280IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM280DAC.SQL_UPD_DEL_HED)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                     , Me._Row.Item("USER_BR_CD").ToString()) _
                                      )

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdDelData(True)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM280DAC", "DeleteHedData", cmd)

        '処理件数の設定
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        'エラーメッセージの設定
        If MyBase.GetResultCount() < 1 Then
            MyBase.SetMessage("E011")
        End If

        cmd = New SqlCommand()

        Return ds

    End Function

    ''' <summary>
    ''' 横持ちタリフ明細マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>横持ちタリフ明細マスタ更新SQLの構築・発行</remarks>
    Private Function DeleteDtlData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM280IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(LMM280DAC.SQL_UPD_DEL_DTL)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                     , Me._Row.Item("USER_BR_CD").ToString()) _
                                                       )

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdDelData(False)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM280DAC", "DeleteDtlData", cmd)

        '処理件数の設定
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(削除復活処理共通)
    ''' </summary>
    ''' <param name="hedFlg">TRUE:ヘッダ更新(排他チェック有り)、FALSE:明細更新</param>
    ''' <remarks></remarks>
    Private Sub SetParamUpdDelData(ByVal hedFlg As Boolean)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '削除フラグ
        With Me._Row
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", .Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))
        End With

        '横持ちタリフヘッダマスタ主キー
        Call Me.SetParamPrimaryKeyHedM()

        '更新時共通項目
        Call Me.SetParamCommonSystemUpd()

        If hedFlg = True Then
            '排他項目
            Call Me.SetParamHaita()
        End If

    End Sub

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 横持ちタリフヘッダ検索処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>横持ちタリフヘッダマスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM280IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM280DAC.SQL_SELECT_COUNT_SELECT)     'SQL構築(カウント用Select句)
        Me._StrSql.Append(LMM280DAC.SQL_SELECT_DATA_FROM)        'SQL構築(カウント用FROM句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM280DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 横持ちタリフヘッダ検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>横持ちタリフヘッダマスタ検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectHedData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM280IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM280DAC.SQL_SELECT_DATA_SELECT)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMM280DAC.SQL_SELECT_DATA_FROM)        'SQL構築(データ抽出用FROM句)
        Call Me.SetConditionMasterSQL()                          '条件設定
        Me._StrSql.Append(LMM280DAC.SQL_ORDER_BY)                'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM280DAC", "SelectHedData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("NRS_BR_NM", "NRS_BR_NM")
        map.Add("YOKO_TARIFF_CD", "YOKO_TARIFF_CD")
        map.Add("YOKO_REM", "YOKO_REM")
        map.Add("CALC_KB", "CALC_KB")
        map.Add("CALC_KB_NM", "CALC_KB_NM")
        map.Add("SPLIT_FLG", "SPLIT_FLG")
        map.Add("SPLIT_FLG_NM", "SPLIT_FLG_NM")
        map.Add("YOKOMOCHI_MIN", "YOKOMOCHI_MIN")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_ENT_USER_NM", "SYS_ENT_USER_NM")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("SYS_UPD_USER_NM", "SYS_UPD_USER_NM")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")
        map.Add("SYS_DEL_NM", "SYS_DEL_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM280OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 横持ちタリフ明細検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>横持ちタリフ明細マスタ検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectDtlData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM280IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM280DAC.SQL_SELECT_DTL_DATA)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("USER_BR_CD").ToString()))

        MyBase.Logger.WriteSQLLog("LMM280DAC", "SelectDtlData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("YOKO_TARIFF_CD", "YOKO_TARIFF_CD")
        map.Add("YOKO_TARIFF_CD_EDA", "YOKO_TARIFF_CD_EDA")
        map.Add("CARGO_KB", "CARGO_KB")
        map.Add("CAR_KB", "CAR_KB")
        map.Add("WT_LV", "WT_LV")
        map.Add("DANGER_KB", "DANGER_KB")
        map.Add("UT_PRICE", "UT_PRICE")
        map.Add("KGS_PRICE", "KGS_PRICE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMM280_YOKO_TARIFF_DTL")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        Dim andstr As StringBuilder = New StringBuilder()

        With Me._Row

            '【営業所コード：=】
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  HED.NRS_BR_CD = @NRS_BR_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '【横持ちタリフコード：LIKE 値%】
            whereStr = .Item("YOKO_TARIFF_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  HED.YOKO_TARIFF_CD LIKE @YOKO_TARIFF_CD")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKO_TARIFF_CD", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '【備考：LIKE %値%】
            whereStr = .Item("YOKO_REM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  HED.YOKO_REM LIKE @YOKO_REM")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKO_REM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '【計算コード区分：=】
            whereStr = .Item("CALC_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  HED.CALC_KB = @CALC_KB")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CALC_KB", whereStr, DBDataType.CHAR))
            End If

            '【明細分割フラグ：=】
            whereStr = .Item("SPLIT_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  HED.SPLIT_FLG = @SPLIT_FLG")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SPLIT_FLG", whereStr, DBDataType.CHAR))
            End If

            '【削除フラグ：=】
            whereStr = .Item("SYS_DEL_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If andstr.Length <> 0 Then
                    andstr.Append("AND")
                End If
                andstr.Append("  HED.SYS_DEL_FLG = @SYS_DEL_FLG")
                andstr.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", whereStr, DBDataType.CHAR))
            End If

            If andstr.Length <> 0 Then
                Me._StrSql.Append("WHERE")
                Me._StrSql.Append(andstr)
            End If

        End With

    End Sub

#End Region

#Region "保存処理"

    ''' <summary>
    ''' 横持ちタリフヘッダマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>横持ちタリフヘッダマスタ検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function ExistYokoTariffHedM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM280IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMM280DAC.SQL_REPEAT_HED)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()) _
                                                                        )
        'SQLパラメータ初期化/設定
        Call Me.SetParamYokoTariffHedMChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM280DAC", "ExistYokoTariffHedM", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()

        'エラーメッセージの設定
        If Convert.ToInt32(reader("SELECT_CNT")) > 0 Then
            MyBase.SetMessage("E010")
        End If

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(横持ちタリフヘッダマスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamYokoTariffHedMChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '横持ちタリフヘッダマスタ主キー設定
        Call Me.SetParamPrimaryKeyHedM()

    End Sub

#Region "新規登録/更新"

    ''' <summary>
    ''' 横持ちタリフヘッダマスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>横持ちタリフヘッダマスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertYokoTariffHedM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM280IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM280DAC.SQL_INSERT_HED_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()) _
                                                                        )

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsertYokoTariffHed()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM280DAC", "InsertYokoTariffHedM", cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' 横持ちタリフ明細マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>横持ちタリフ明細マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertYokoTariffDtlM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM280_YOKO_TARIFF_DTL")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM280DAC.SQL_INSERT_DTL)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()) _
                                                                        )

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化/設定
            Call Me.SetParamInsertYokoTariffDtl()

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMM280DAC", "InsertYokoTariffDtlM", cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 横持ちタリフヘッダマスタ更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>横持ちタリフヘッダマスタ更新登録SQLの構築・発行</remarks>
    Private Function UpdateYokoTariffHedM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM280IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM280DAC.SQL_UPDATE_HED)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()) _
                                                                        )

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdateYokoTariffHed()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM280DAC", "UpdateYokoTariffHedM", cmd)

        'SQLの発行
        MyBase.SetResultCount(MyBase.GetUpdateResult(cmd))
        If MyBase.GetResultCount < 1 Then
            MyBase.SetMessage("E011")
        End If

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    '''横持ちタリフ明細マスタ物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>横持ちタリフ明細マスタ物理削除SQLの構築・発行</remarks>
    Private Function DeleteYokoTariffDtlM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMM280IN")

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL構築
        Me._StrSql.Append(LMM280DAC.SQL_DELETE_DTL)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("USER_BR_CD").ToString()) _
                                                                        )

        'SQLパラメータ初期化/設定
        Call Me.SetParamDeleteYokoTariffDtlM()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMM280DAC", "DeleteYokoTariffDtlM", cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        cmd.Parameters.Clear()

        Return ds

    End Function

    ''' <summary>
    ''' パラメータ設定モジュール(横持ちタリフヘッダマスタ新規登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertYokoTariffHed()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '横持ちタリフヘッダマスタ全項目
        Call Me.SetParamYokoTariffHedM()

        '新規登録共通項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(横持ちタリフ明細マスタ新規登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsertYokoTariffDtl()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '横持ちタリフ明細マスタ全項目
        Call Me.SetParamYokoTariffDtlM()

        '新規登録共通項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(横持ちタリフヘッダマスタ更新用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdateYokoTariffHed()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '横持ちタリフヘッダマスタ全項目
        Call Me.SetParamYokoTariffHedM()

        '排他項目
        Call Me.SetParamHaita()

        '更新時共通項目
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(横持ちタリフ明細マスタ物理削除用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamDeleteYokoTariffDtlM()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '横持ちタリフヘッダマスタ主キー設定
        Call Me.SetParamPrimaryKeyHedM()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(横持ちタリフヘッダマスタ全項目)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamYokoTariffHedM()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKO_TARIFF_CD", .Item("YOKO_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKO_REM", .Item("YOKO_REM").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CALC_KB", .Item("CALC_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SPLIT_FLG", .Item("SPLIT_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKOMOCHI_MIN", .Item("YOKOMOCHI_MIN").ToString(), DBDataType.NUMERIC))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(横持ちタリフ明細マスタ全項目)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamYokoTariffDtlM()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKO_TARIFF_CD", .Item("YOKO_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKO_TARIFF_CD_EDA", .Item("YOKO_TARIFF_CD_EDA").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CARGO_KB", .Item("CARGO_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_KB", .Item("CAR_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WT_LV", .Item("WT_LV").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DANGER_KB", .Item("DANGER_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UT_PRICE", .Item("UT_PRICE").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KGS_PRICE", .Item("KGS_PRICE").ToString(), DBDataType.NUMERIC))

        End With

    End Sub

#End Region

#End Region

#Region "共通項目"

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(新規時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", Me.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(横持ちタリフヘッダマスタ主キー)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamPrimaryKeyHedM()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@YOKO_TARIFF_CD", .Item("YOKO_TARIFF_CD").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(排他チェック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamHaita()

        With Me._Row
            '排他共通項目
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAITA_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' スキーマ名称設定
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="brCd">営業所</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String _
                                 , ByVal brCd As String _
                                 ) As String

        'トラン系スキーマ名設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系スキーマ名設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region

#End Region

End Class
