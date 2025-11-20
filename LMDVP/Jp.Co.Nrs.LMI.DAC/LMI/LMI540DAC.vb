'  ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI540DAC : オフライン出荷検索(FFEM)
'  作  成  者       :  
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMI540DAC
''' </summary>
''' <remarks></remarks>
Public Class LMI540DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#End Region 'Const

#Region "SQL"

#Region "取込処理 SQL"

    ''' <summary>
    ''' 取込処理: 区分マスタ('F017') 存在チェック
    ''' </summary>
    Private Const SQL_SELECT_KBN_F017 As String = "" _
        & "SELECT                                                                        " & vbNewLine _
        & "      KBN_CD                                                                  " & vbNewLine _
        & "    , KBN_NM1 AS PLANT_CD                                                     " & vbNewLine _
        & "    , KBN_NM2 AS PLANT_NM                                                     " & vbNewLine _
        & "    , KBN_NM3 AS NRS_BR_CD                                                    " & vbNewLine _
        & "    , KBN_NM4 AS WH_CD                                                        " & vbNewLine _
        & "    , KBN_NM5 AS CUST_CD_L                                                    " & vbNewLine _
        & "    , KBN_NM6 AS CUST_CD_M                                                    " & vbNewLine _
        & "FROM                                                                          " & vbNewLine _
        & "    $LM_MST$..Z_KBN                                                           " & vbNewLine _
        & "WHERE                                                                         " & vbNewLine _
        & "    KBN_GROUP_CD = 'F017'                                                     " & vbNewLine _
        & "AND KBN_NM1 = @PLANT_CD                                                       " & vbNewLine _
        & "-- (96 大分 ではオフライン出荷の場合設定しない前提のプラントコード、          " & vbNewLine _
        & "--  または F1 安田 に移行済みで今後使用しない 98 大牟田 のデータ)、           " & vbNewLine _
        & "-- かつ 1プラントコードに対して複数営業所コードが存在するレコードは除外する。 " & vbNewLine _
        & "AND NOT(   (KBN_NM3 = '96' AND KBN_NM1 = 'B202')                              " & vbNewLine _
        & "        OR (KBN_NM3 = 'F2' AND KBN_NM1 = 'B300')  -- ADD 2022/11/24 033380 F2追加 " & vbNewLine _
        & "        OR (KBN_NM3 = '98' AND KBN_NM1 = 'B202'))                             " & vbNewLine _
        & "AND SYS_DEL_FLG = '0'                                                         " & vbNewLine _
        & ""

    ''' <summary>
    ''' 取込処理: FFEM オフライン入出荷受信データ 登録
    ''' </summary>
    Private Const SQL_INSERT_OUTKAEDI_DTL_FJF_OFF As String = "" _
        & "INSERT INTO $LM_TRN$..H_OUTKAEDI_DTL_FJF_OFF (" & vbNewLine _
        & "      KEY_NO                        " & vbNewLine _
        & "    , OFFLINE_NO                    " & vbNewLine _
        & "    , IRAI_DATE                     " & vbNewLine _
        & "    , IRAI_SYA                      " & vbNewLine _
        & "    , MOTO                          " & vbNewLine _
        & "    , PLANT_CD                      " & vbNewLine _
        & "    , NRS_BR_CD                     " & vbNewLine _
        & "    , SHUBETSU                      " & vbNewLine _
        & "    , SAP_NO                        " & vbNewLine _
        & "    , OUTKA_DATE                    " & vbNewLine _
        & "    , ARR_DATE                      " & vbNewLine _
        & "    , ZIP                           " & vbNewLine _
        & "    , DEST_AD                       " & vbNewLine _
        & "    , COMP_NM                       " & vbNewLine _
        & "    , BUSYO_NM                      " & vbNewLine _
        & "    , TANTO_NM                      " & vbNewLine _
        & "    , TEL                           " & vbNewLine _
        & "    , GOODS_NM                      " & vbNewLine _
        & "    , LOT_NO                        " & vbNewLine _
        & "    , INOUTKA_NB                    " & vbNewLine _
        & "    , ONDO                          " & vbNewLine _
        & "    , DOKUGEKI                      " & vbNewLine _
        & "    , REMARK                        " & vbNewLine _
        & "    , HAISO                         " & vbNewLine _
        & "    , SHIZI_KB                      " & vbNewLine _
        & "    , NOHIN_KB                      " & vbNewLine _
        & "    , SYS_ENT_DATE                  " & vbNewLine _
        & "    , SYS_ENT_TIME                  " & vbNewLine _
        & "    , SYS_ENT_PGID                  " & vbNewLine _
        & "    , SYS_ENT_USER                  " & vbNewLine _
        & "    , SYS_UPD_DATE                  " & vbNewLine _
        & "    , SYS_UPD_TIME                  " & vbNewLine _
        & "    , SYS_UPD_PGID                  " & vbNewLine _
        & "    , SYS_UPD_USER                  " & vbNewLine _
        & "    , SYS_DEL_FLG                   " & vbNewLine _
        & ")                                   " & vbNewLine _
        & "SELECT                              " & vbNewLine _
        & "      ISNULL((SELECT MAX(KEY_NO) FROM $LM_TRN$..H_OUTKAEDI_DTL_FJF_OFF), 0) + 1 AS KEY_NO " & vbNewLine _
        & "    , @OFFLINE_NO   AS OFFLINE_NO   " & vbNewLine _
        & "    , @IRAI_DATE    AS IRAI_DATE    " & vbNewLine _
        & "    , @IRAI_SYA     AS IRAI_SYA     " & vbNewLine _
        & "    , @MOTO         AS MOTO         " & vbNewLine _
        & "    , @PLANT_CD     AS PLANT_CD     " & vbNewLine _
        & "    , @NRS_BR_CD    AS NRS_BR_CD    " & vbNewLine _
        & "    , @SHUBETSU     AS SHUBETSU     " & vbNewLine _
        & "    , @SAP_NO       AS SAP_NO       " & vbNewLine _
        & "    , @OUTKA_DATE   AS OUTKA_DATE   " & vbNewLine _
        & "    , @ARR_DATE     AS ARR_DATE     " & vbNewLine _
        & "    , @ZIP          AS ZIP          " & vbNewLine _
        & "    , @DEST_AD      AS DEST_AD      " & vbNewLine _
        & "    , @COMP_NM      AS COMP_NM      " & vbNewLine _
        & "    , @BUSYO_NM     AS BUSYO_NM     " & vbNewLine _
        & "    , @TANTO_NM     AS TANTO_NM     " & vbNewLine _
        & "    , @TEL          AS TEL          " & vbNewLine _
        & "    , @GOODS_NM     AS GOODS_NM     " & vbNewLine _
        & "    , @LOT_NO       AS LOT_NO       " & vbNewLine _
        & "    , @INOUTKA_NB   AS INOUTKA_NB   " & vbNewLine _
        & "    , @ONDO         AS ONDO         " & vbNewLine _
        & "    , @DOKUGEKI     AS DOKUGEKI     " & vbNewLine _
        & "    , @REMARK       AS REMARK       " & vbNewLine _
        & "    , @HAISO        AS HAISO        " & vbNewLine _
        & "    , @SHIZI_KB     AS SHIZI_KB     " & vbNewLine _
        & "    , @NOHIN_KB     AS NOHIN_KB     " & vbNewLine _
        & "    , @SYS_ENT_DATE AS SYS_ENT_DATE " & vbNewLine _
        & "    , @SYS_ENT_TIME AS SYS_ENT_TIME " & vbNewLine _
        & "    , @SYS_ENT_PGID AS SYS_ENT_PGID " & vbNewLine _
        & "    , @SYS_ENT_USER AS SYS_ENT_USER " & vbNewLine _
        & "    , @SYS_UPD_DATE AS SYS_UPD_DATE " & vbNewLine _
        & "    , @SYS_UPD_TIME AS SYS_UPD_TIME " & vbNewLine _
        & "    , @SYS_UPD_PGID AS SYS_UPD_PGID " & vbNewLine _
        & "    , @SYS_UPD_USER AS SYS_UPD_USER " & vbNewLine _
        & "    , @SYS_DEL_FLG  AS SYS_DEL_FLG  " & vbNewLine _
        & ""

#End Region ' "取込処理 SQL"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 検索処理：取得：SELECT句（件数取得用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_SEARCH As String = "" _
            & "SELECT                   " & vbNewLine _
            & "  COUNT(*) AS SELECT_CNT " & vbNewLine _
            & ""

    ''' <summary>
    ''' 検索処理：取得：SELECT句（データ取得用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA_SEARCH As String = "" _
            & "SELECT DISTINCT                     " & vbNewLine _
            & "      OFFLN.KEY_NO                  " & vbNewLine _
            & "    , OFFLN.OFFLINE_NO              " & vbNewLine _
            & "    , OFFLN.IRAI_DATE               " & vbNewLine _
            & "    , OFFLN.IRAI_SYA                " & vbNewLine _
            & "    , OFFLN.MOTO                    " & vbNewLine _
            & "    , OFFLN.PLANT_CD                " & vbNewLine _
            & "    , OFFLN.NRS_BR_CD               " & vbNewLine _
            & "    , OFFLN.SHUBETSU                " & vbNewLine _
            & "    , OFFLN.SAP_NO                  " & vbNewLine _
            & "    , OFFLN.OUTKA_DATE              " & vbNewLine _
            & "    , OFFLN.ARR_DATE                " & vbNewLine _
            & "    , OFFLN.ZIP                     " & vbNewLine _
            & "    , OFFLN.DEST_AD                 " & vbNewLine _
            & "    , OFFLN.COMP_NM                 " & vbNewLine _
            & "    , OFFLN.BUSYO_NM                " & vbNewLine _
            & "    , OFFLN.TANTO_NM                " & vbNewLine _
            & "    , OFFLN.TEL                     " & vbNewLine _
            & "    , OFFLN.GOODS_NM                " & vbNewLine _
            & "    , OFFLN.LOT_NO                  " & vbNewLine _
            & "    , OFFLN.INOUTKA_NB              " & vbNewLine _
            & "    , OFFLN.ONDO                    " & vbNewLine _
            & "    , OFFLN.DOKUGEKI                " & vbNewLine _
            & "    , OFFLN.REMARK                  " & vbNewLine _
            & "    , OFFLN.HAISO                   " & vbNewLine _
            & "    , OFFLN.SHIZI_KB                " & vbNewLine _
            & "    , H010S.KBN_NM1 AS SHIZI_KB_NM  " & vbNewLine _
            & "    , OFFLN.NOHIN_KB                " & vbNewLine _
            & "    , H010N.KBN_NM1 AS NOHIN_KB_NM  " & vbNewLine _
            & "    , @WH_CD AS WH_CD               " & vbNewLine _
            & "    , @CUST_CD_L AS CUST_CD_L       " & vbNewLine _
            & "    , @CUST_CD_M AS CUST_CD_M       " & vbNewLine _
            & "    , OFFLN.SYS_UPD_DATE            " & vbNewLine _
            & "    , OFFLN.SYS_UPD_TIME            " & vbNewLine _
            & ""

    ''' <summary>
    ''' 検索処理：取得：FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_SEARCH As String = "" _
            & "FROM                                          " & vbNewLine _
            & "    $LM_TRN$..H_OUTKAEDI_DTL_FJF_OFF OFFLN    " & vbNewLine _
            & "LEFT JOIN                                     " & vbNewLine _
            & "    $LM_MST$..Z_KBN F017                      " & vbNewLine _
            & "        ON  F017.KBN_GROUP_CD = 'F017'        " & vbNewLine _
            & "        AND F017.KBN_NM1 = OFFLN.PLANT_CD     " & vbNewLine _
            & "        AND F017.SYS_DEL_FLG = '0'            " & vbNewLine _
            & "LEFT JOIN                                     " & vbNewLine _
            & "    $LM_MST$..Z_KBN H010S                     " & vbNewLine _
            & "        ON  H010S.KBN_GROUP_CD = 'H010'       " & vbNewLine _
            & "        AND H010S.KBN_CD = OFFLN.SHIZI_KB     " & vbNewLine _
            & "        AND H010S.SYS_DEL_FLG = '0'           " & vbNewLine _
            & "LEFT JOIN                                     " & vbNewLine _
            & "    $LM_MST$..Z_KBN H010N                     " & vbNewLine _
            & "        ON  H010N.KBN_GROUP_CD = 'H010'       " & vbNewLine _
            & "        AND H010N.KBN_CD = OFFLN.NOHIN_KB     " & vbNewLine _
            & "        AND H010N.SYS_DEL_FLG = '0'           " & vbNewLine _
            & ""

#End Region ' "検索処理 SQL"

#Region "印刷処理 SQL "

#Region "出荷指示書/納品書 出力有無 更新"

    ''' <summary>
    ''' 出荷指示書/納品書 出力有無 更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_PRT_SUMI_KB As String = "" _
        & "UPDATE $LM_TRN$..H_OUTKAEDI_DTL_FJF_OFF    " & vbNewLine _
        & "SET                                        " & vbNewLine _
        & "      SHIZI_KB = @SHIZI_KB                 " & vbNewLine _
        & "    , NOHIN_KB = @NOHIN_KB                 " & vbNewLine _
        & "    , SYS_UPD_DATE = @SYS_UPD_DATE         " & vbNewLine _
        & "    , SYS_UPD_TIME = @SYS_UPD_TIME         " & vbNewLine _
        & "    , SYS_UPD_USER = @SYS_UPD_USER         " & vbNewLine _
        & "    , SYS_UPD_PGID = @SYS_UPD_PGID         " & vbNewLine _
        & "WHERE                                      " & vbNewLine _
        & "        KEY_NO = @KEY_NO                   " & vbNewLine _
        & "    AND SYS_UPD_DATE = @SYS_UPD_DATE_HAITA " & vbNewLine _
        & "    AND SYS_UPD_TIME = @SYS_UPD_TIME_HAITA " & vbNewLine _
        & ""

#End Region

#End Region ' "印刷処理 SQL"

#End Region 'SQL

#Region "Field"

    ''' <summary>
    ''' DataTableの行抜き出し
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As Data.DataRow

    ''' <summary>
    ''' SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private _StrSql As StringBuilder

    ''' <summary>
    ''' SQLパラメータ
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList

#End Region 'Field

#Region "Method"

#Region "取込処理"

    ''' <summary>
    ''' 取込処理: 区分マスタ('F017') 存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectKbnF017(ByVal ds As DataSet) As DataSet

        Dim inTbl As DataTable = ds.Tables("LMI540IN_IMPORT")
        Dim nrsBrCd As String = ds.Tables("LMI540_TORIKOMI_HED").Rows(0).Item("NRS_BR_CD").ToString()

        Me._SqlPrmList = New ArrayList()

        Me._Row = inTbl.Rows(0)

        ' SQLの作成
        Me._StrSql = New StringBuilder(LMI540DAC.SQL_SELECT_KBN_F017)

        ' 条件およびパラメータの設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PLANT_CD", Me._Row.Item("PLANT_CD").ToString(), DBDataType.VARCHAR))

        Dim selectCnt As Integer = 0

        ' SQLのコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), nrsBrCd))

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            ' ログ出力
            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' 取得データの格納先をマッピング
                Dim map As Hashtable = New Hashtable()
                map.Add("KBN_CD", "KBN_CD")
                map.Add("PLANT_CD", "PLANT_CD")
                map.Add("PLANT_NM", "PLANT_NM")
                map.Add("NRS_BR_CD", "NRS_BR_CD")
                map.Add("WH_CD", "WH_CD")
                map.Add("CUST_CD_L", "CUST_CD_L")
                map.Add("CUST_CD_M", "CUST_CD_M")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI540OUT_KBN_F017")

                ' 取得件数の設定
                selectCnt = ds.Tables("LMI540OUT_KBN_F017").Rows.Count()

            End Using

            ' パラメータの初期化
            cmd.Parameters.Clear()

        End Using

        MyBase.SetResultCount(selectCnt)

        Return ds

    End Function

    ''' <summary>
    ''' 取込処理: FFEM オフライン入出荷受信データ 登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function InsertOutkaediDtlFjfOff(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LMI540IN_IMPORT")
        Dim nrsBrCd As String = ds.Tables("LMI540_TORIKOMI_HED").Rows(0).Item("NRS_BR_CD").ToString()

        ' SQL格納変数の初期化
        Me._StrSql = New StringBuilder(LMI540DAC.SQL_INSERT_OUTKAEDI_DTL_FJF_OFF)


        Me._SqlPrmList = New ArrayList()

        ' パラメータ設定
        Call Me.SetSqlParamInsertOutkaediDtlFjfOff(dt)

        ' スキーマ設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), nrsBrCd)

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            MyBase.GetInsertResult(cmd)

            ' パラメータの初期化
            cmd.Parameters.Clear()

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' SQL パラメータ設定 取込処理: FFEM オフライン入出荷受信データ 登録 用
    ''' </summary>
    ''' <param name="dt"></param>
    Private Sub SetSqlParamInsertOutkaediDtlFjfOff(ByVal dt As DataTable)

        With dt.Rows(0)

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OFFLINE_NO", Me.NullConvertString(.Item("OFFLINE_NO")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRAI_DATE", Me.NullConvertString(.Item("IRAI_DATE")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRAI_SYA", Me.NullConvertString(.Item("IRAI_SYA")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO", Me.NullConvertString(.Item("MOTO")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@PLANT_CD", Me.NullConvertString(.Item("PLANT_CD")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.NullConvertString(.Item("NRS_BR_CD")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHUBETSU", Me.NullConvertString(.Item("SHUBETSU")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAP_NO", Me.NullConvertString(.Item("SAP_NO")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE", Me.NullConvertString(.Item("OUTKA_DATE")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_DATE", Me.NullConvertString(.Item("ARR_DATE")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP", Me.NullConvertString(.Item("ZIP")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD", Me.NullConvertString(.Item("DEST_AD")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COMP_NM", Me.NullConvertString(.Item("COMP_NM")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUSYO_NM", Me.NullConvertString(.Item("BUSYO_NM")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TANTO_NM", Me.NullConvertString(.Item("TANTO_NM")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEL", Me.NullConvertString(.Item("TEL")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", Me.NullConvertString(.Item("GOODS_NM")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me.NullConvertString(.Item("LOT_NO")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NB", Me.NullConvertZero(.Item("INOUTKA_NB")), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO", Me.NullConvertString(.Item("ONDO")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DOKUGEKI", Me.NullConvertString(.Item("DOKUGEKI")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", Me.NullConvertString(.Item("REMARK")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAISO", Me.NullConvertString(.Item("HAISO")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIZI_KB", Me.NullConvertString(.Item("SHIZI_KB")), DBDataType.VARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NOHIN_KB", Me.NullConvertString(.Item("NOHIN_KB")), DBDataType.VARCHAR))

            ' システム管理用項目
            Call Me.SetDataInsertParameter()

        End With

    End Sub

#End Region ' "取込処理"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理：件数
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SearchCount(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI540IN_SEARCH")

        ' INTable の条件 row の格納
        Me._Row = inTbl.Rows(0)

        ' SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI540DAC.SQL_SELECT_COUNT_SEARCH & LMI540DAC.SQL_SELECT_FROM_SEARCH)

        ' 条件およびパラメータの設定
        Call Me.SearchSelectSetCondition()

        Dim selectCnt As Integer = 0

        ' SQLのコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            ' ログ出力
            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' 取得件数の設定
                If reader.Read() Then
                    selectCnt = Convert.ToInt32(reader("SELECT_CNT"))
                End If

            End Using

            ' パラメータの初期化
            cmd.Parameters.Clear()

        End Using

        MyBase.SetResultCount(selectCnt)

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理：取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SearchSelect(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI540IN_SEARCH")

        ' INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        ' SQLの作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI540DAC.SQL_SELECT_DATA_SEARCH & LMI540DAC.SQL_SELECT_FROM_SEARCH)

        ' 条件およびパラメータの設定
        Call Me.SearchSelectSetCondition()

        ' 並びの設定
        Call Me.SearchSelectSetOrder()

        Dim selectCnt As Integer = 0

        ' SQLのコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

            ' パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            ' ログ出力
            MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' 取得データの格納先をマッピング
                Dim map As Hashtable = New Hashtable()
                map.Add("KEY_NO", "KEY_NO")
                map.Add("OFFLINE_NO", "OFFLINE_NO")
                map.Add("IRAI_DATE", "IRAI_DATE")
                map.Add("IRAI_SYA", "IRAI_SYA")
                map.Add("MOTO", "MOTO")
                map.Add("PLANT_CD", "PLANT_CD")
                map.Add("NRS_BR_CD", "NRS_BR_CD")
                map.Add("SHUBETSU", "SHUBETSU")
                map.Add("SAP_NO", "SAP_NO")
                map.Add("OUTKA_DATE", "OUTKA_DATE")
                map.Add("ARR_DATE", "ARR_DATE")
                map.Add("ZIP", "ZIP")
                map.Add("DEST_AD", "DEST_AD")
                map.Add("COMP_NM", "COMP_NM")
                map.Add("BUSYO_NM", "BUSYO_NM")
                map.Add("TANTO_NM", "TANTO_NM")
                map.Add("TEL", "TEL")
                map.Add("GOODS_NM", "GOODS_NM")
                map.Add("LOT_NO", "LOT_NO")
                map.Add("INOUTKA_NB", "INOUTKA_NB")
                map.Add("ONDO", "ONDO")
                map.Add("DOKUGEKI", "DOKUGEKI")
                map.Add("REMARK", "REMARK")
                map.Add("HAISO", "HAISO")
                map.Add("SHIZI_KB", "SHIZI_KB")
                map.Add("SHIZI_KB_NM", "SHIZI_KB_NM")
                map.Add("NOHIN_KB", "NOHIN_KB")
                map.Add("NOHIN_KB_NM", "NOHIN_KB_NM")
                map.Add("WH_CD", "WH_CD")
                map.Add("CUST_CD_L", "CUST_CD_L")
                map.Add("CUST_CD_M", "CUST_CD_M")
                map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
                map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI540OUT")

                ' 取得件数の設定
                selectCnt = ds.Tables("LMI540OUT").Rows.Count()

            End Using

            ' パラメータの初期化
            cmd.Parameters.Clear()

        End Using

        MyBase.SetResultCount(selectCnt)

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理：条件
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SearchSelectSetCondition()

        Dim whereStr As String = String.Empty

        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' 条件
        Me._StrSql.Append("WHERE " & vbNewLine)
        With Me._Row
            ' 固定条件
            Me._StrSql.Append("    OFFLN.SYS_DEL_FLG = '0' " & vbNewLine)

            ' 営業所(FFEM オフライン入出荷受信データ.取り扱い拠点コード)
            whereStr = .Item("NRS_BR_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.NRS_BR_CD = @NRS_BR_CD " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.VARCHAR))
            End If

            ' 倉庫(区分マスタ('F017').区分名4)
            whereStr = .Item("WH_CD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND F017.KBN_NM4 = @WH_CD " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", whereStr, DBDataType.VARCHAR))
            End If

            ' 荷主コード(大)(区分マスタ('F017').区分名5)
            whereStr = .Item("CUST_CD_L").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND F017.KBN_NM5 = @CUST_CD_L " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.VARCHAR))
            End If

            ' 荷主コード(中)(区分マスタ('F017').区分名6)
            whereStr = .Item("CUST_CD_M").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND F017.KBN_NM6 = @CUST_CD_M " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.VARCHAR))
            End If

            ' 出荷日FROM
            whereStr = .Item("OUTKA_DATE_FROM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.OUTKA_DATE >= @OUTKA_DATE_FROM " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE_FROM", whereStr, DBDataType.VARCHAR))
            End If

            ' 出荷日TO
            whereStr = .Item("OUTKA_DATE_TO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.OUTKA_DATE <= @OUTKA_DATE_TO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OUTKA_DATE_TO", whereStr, DBDataType.VARCHAR))
            End If

            ' KEY_NO.
            whereStr = .Item("KEY_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) AndAlso Convert.ToInt32(whereStr) <> 0 Then
                Me._StrSql.Append("AND OFFLN.KEY_NO = @KEY_NO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEY_NO", whereStr, DBDataType.NUMERIC))
            End If

            ' オフラインNo.
            whereStr = .Item("OFFLINE_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.OFFLINE_NO LIKE @OFFLINE_NO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OFFLINE_NO", String.Concat(whereStr, "%"), DBDataType.VARCHAR))
            End If

            ' 出荷指示書出力
            whereStr = .Item("SHIZI_KB").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.SHIZI_KB = @SHIZI_KB " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIZI_KB", whereStr, DBDataType.VARCHAR))
            End If

            ' 納品書出力
            whereStr = .Item("NOHIN_KB").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.NOHIN_KB = @NOHIN_KB " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NOHIN_KB", whereStr, DBDataType.VARCHAR))
            End If

            ' 依頼日
            whereStr = .Item("IRAI_DATE").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.IRAI_DATE LIKE @IRAI_DATE " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRAI_DATE", String.Concat(whereStr, "%"), DBDataType.VARCHAR))
            End If

            ' 依頼者
            whereStr = .Item("IRAI_SYA").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.IRAI_SYA LIKE @IRAI_SYA " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@IRAI_SYA", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 出荷/回収元
            whereStr = .Item("MOTO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.MOTO LIKE @MOTO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MOTO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 種別
            whereStr = .Item("SHUBETSU").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.SHUBETSU LIKE @SHUBETSU " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHUBETSU", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' SAP受注登録番号
            whereStr = .Item("SAP_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.SAP_NO LIKE @SAP_NO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SAP_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '「出荷日」は既出

            ' 納品日
            whereStr = .Item("ARR_DATE").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.ARR_DATE LIKE @ARR_DATE " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ARR_DATE", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 郵便番号
            whereStr = .Item("ZIP").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.ZIP LIKE @ZIP " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ZIP", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 住所
            whereStr = .Item("DEST_AD").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.DEST_AD LIKE @DEST_AD " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_AD", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 会社名
            whereStr = .Item("COMP_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.COMP_NM LIKE @COMP_NM " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COMP_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 部署名
            whereStr = .Item("BUSYO_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.BUSYO_NM LIKE @BUSYO_NM " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BUSYO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 担当者名
            whereStr = .Item("TANTO_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.TANTO_NM LIKE @TANTO_NM " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TANTO_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 電話番号
            whereStr = .Item("TEL").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.TEL LIKE @TEL " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TEL", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 品名
            whereStr = .Item("GOODS_NM").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.GOODS_NM LIKE @GOODS_NM " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 製造ロット
            whereStr = .Item("LOT_NO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.LOT_NO LIKE @LOT_NO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Concat(whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 温度条件
            whereStr = .Item("ONDO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.ONDO LIKE @ONDO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ONDO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 毒劇物
            whereStr = .Item("DOKUGEKI").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.DOKUGEKI LIKE @DOKUGEKI " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DOKUGEKI", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 備考欄
            whereStr = .Item("REMARK").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.REMARK LIKE @REMARK " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REMARK", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            ' 配送便
            whereStr = .Item("HAISO").ToString()
            If Not String.IsNullOrEmpty(whereStr) Then
                Me._StrSql.Append("AND OFFLN.HAISO LIKE @HAISO " & vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HAISO", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 検索処理：並び
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SearchSelectSetOrder()

        ' 並び
        Me._StrSql.Append("ORDER BY" & vbNewLine)
        Me._StrSql.Append("      OFFLN.KEY_NO " & vbNewLine)
        Me._StrSql.Append("    , OFFLN.OUTKA_DATE " & vbNewLine)

    End Sub

#End Region ' "検索処理"

#Region "印刷処理"


#Region "プリントフラグ更新"

    ''' <summary>
    ''' プリントフラグ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdatePrtFlg(ByVal ds As DataSet) As DataSet

        For dtIdx As Integer = 0 To ds.Tables("LMI540IN_PRINT").Rows.Count - 1
            ' DataSetのIN情報を取得
            Dim inTbl As DataTable = ds.Tables("LMI540IN_PRINT")

            ' INTableの条件rowの格納
            Me._Row = inTbl.Rows(dtIdx)

            ' SQLの作成
            Me._StrSql = New StringBuilder()
            Me._StrSql.Append(LMI540DAC.SQL_UPDATE_PRT_SUMI_KB)

            ' SQLのコンパイル
            Using cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

                ' パラメータの設定
                Me._SqlPrmList = New ArrayList()
                With Me._Row
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIZI_KB", .Item("SHIZI_KB").ToString(), DBDataType.VARCHAR))
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NOHIN_KB", .Item("NOHIN_KB").ToString(), DBDataType.VARCHAR))
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.VARCHAR))
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.VARCHAR))
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.VARCHAR))
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KEY_NO", .Item("KEY_NO").ToString(), DBDataType.VARCHAR))
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_HAITA", .Item("SYS_UPD_DATE").ToString(), DBDataType.VARCHAR))
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_HAITA", .Item("SYS_UPD_TIME").ToString(), DBDataType.VARCHAR))
                End With

                ' パラメータの反映
                For Each obj As Object In Me._SqlPrmList
                    cmd.Parameters.Add(obj)
                Next

                ' ログ出力
                MyBase.Logger.WriteSQLLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

                ' SQLの発行
                Dim updateResult As Integer = MyBase.GetUpdateResult(cmd)
                MyBase.SetResultCount(updateResult)

                If updateResult < 1 Then
                    MyBase.SetMessage("E011")
                    Exit For
                End If

                ' パラメータの初期化
                cmd.Parameters.Clear()

            End Using
        Next

        Return ds

    End Function

#End Region

#End Region ' "印刷処理"

#Region "共通処理"

#Region "SQL パラメータ設定"

#Region "SQL 共通パラメータ設定"

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    Private Sub SetDataInsertParameter(Optional ByVal sysDelFlg As String = BaseConst.FLG.OFF)

        'システム項目
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", sysDelFlg, DBDataType.CHAR))
        Call Me.SetSysdataParameter()

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    Private Sub SetSysdataParameter()

        'システム項目
        Call Me.SetSysdataTimeParameter()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    Private Sub SetSysdataTimeParameter()

        '更新日時
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))

    End Sub

#End Region ' "SQL 共通パラメータ設定"

#End Region ' "SQL パラメータ設定"

#Region "スキーマ名称設定"

    ''' <summary>
    ''' 共通処理：スキーマ名称設定
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>SQL</returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region ' "スキーマ名称設定"

#Region "編集・変換"

#Region "Null変換"

    ''' <summary>
    ''' Null変換（文字列）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertString(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = String.Empty
        End If

        Return value

    End Function

    ''' <summary>
    ''' Null変換（数値）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullConvertZero(ByVal value As Object) As Object

        If IsDBNull(value) = True Then
            value = 0
        End If

        Return value

    End Function

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function

#End Region ' "Null変換"

#End Region ' "編集・変換"

#End Region ' "共通処理"

#End Region ' "Method"

End Class
