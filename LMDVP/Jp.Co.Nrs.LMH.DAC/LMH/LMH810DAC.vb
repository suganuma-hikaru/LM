' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : 
'  プログラムID     :  LMH810DAC : 分析票管理マスタ
'  作  成  者       :  小林信
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH810DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH810DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    'TODO:　(2012.09.27)追加START
#Region "SELECT_Z_KBN(分析票サーバーパス)"
    Private Const SQL_SELECT_Z_KBN As String = " SELECT                                        " & vbNewLine _
                                     & " Z_KBN.KBN_NM3                          AS COA_FOLDER  " & vbNewLine _
                                     & ",Z_KBN.KBN_NM4                          AS COA_ERR_FOLDER --ADD 2020/10/22 015075 " & vbNewLine _
                                     & " FROM                                                  " & vbNewLine _
                                     & " $LM_MST$..Z_KBN                        Z_KBN          " & vbNewLine _
                                     & " WHERE                                                 " & vbNewLine _
                                     & " Z_KBN.KBN_GROUP_CD = 'B010'                           " & vbNewLine _
                                     & " AND                                                   " & vbNewLine _
                                     & " --Z_KBN.KBN_CD   = '10' 2014.08.21 kikuchi            " & vbNewLine _
                                     & " Z_KBN.KBN_CD = @NRS_BR_CD                             " & vbNewLine

#End Region
    'TODO:　(2012.09.27)追加START

#Region "SELECT句"

    ''' <summary>
    ''' カウント用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(COA.NRS_BR_CD)		   AS SELECT_CNT   " & vbNewLine

    ''' <summary>
    ''' チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FLG As String = " SELECT SYS_DEL_FLG                                      " & vbNewLine

    ''' <summary>
    ''' COA_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                         " & vbNewLine _
                                            & "	      COA.NRS_BR_CD                               AS NRS_BR_CD                 " & vbNewLine _
                                            & "	     ,COA.CUST_CD_L                               AS CUST_CD_L                 " & vbNewLine _
                                            & "	     ,COA.CUST_CD_M                               AS CUST_CD_M                 " & vbNewLine _
                                            & "	     ,GOODS.GOODS_CD_CUST                         AS GOODS_CD_CUST             " & vbNewLine _
                                            & "	     ,COA.GOODS_CD_NRS                            AS GOODS_CD_NRS              " & vbNewLine _
                                            & "	     ,COA.LOT_NO                                  AS LOT_NO                    " & vbNewLine _
                                            & "	     ,COA.DEST_CD                                 AS DEST_CD                   " & vbNewLine _
                                            & "	     ,COA.COA_LINK                                AS COA_LINK                  " & vbNewLine _
                                            & "	     ,COA.COA_NAME                                AS COA_NAME                  " & vbNewLine _
                                            & "	     ,COA.SYS_DEL_FLG                             AS SYS_DEL_FLG               " & vbNewLine

    ''' <summary>
    ''' COA_Mデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_GOODS As String = " SELECT                                                                          " & vbNewLine _
                                            & "	      GOODS.NRS_BR_CD                             AS NRS_BR_CD                   " & vbNewLine _
                                            & "	     ,GOODS.GOODS_CD_CUST                         AS GOODS_CD_CUST               " & vbNewLine _
                                            & "	     ,GOODS.GOODS_CD_NRS                          AS GOODS_CD_NRS                " & vbNewLine


    'TODO:　(2012.09.27)追加START
    ''' <summary>
    ''' M_EDI_FOLDER_PASSデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FOLDERPASS As String = " SELECT                                                                  " & vbNewLine _
                                            & "	      FOLDERPASS.RCV_WORK_INPUT_DIR               AS COA_FOLDER               " & vbNewLine _
                                            & "	     ,FOLDERPASS.BACKUP_INPUT_DIR                 AS COA_BACKUP               " & vbNewLine _
                                            & "	     ,FOLDERPASS.RCV_FILE_EXTENTION               AS COA_EXTENTION            " & vbNewLine
    'TODO:　(2012.09.27)追加END

    '(2012.10.24)修正START 要望番号1531
    ''' <summary>
    ''' M_COACONFIGデータ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COACONFIG As String = "SELECT                                                                 " & vbNewLine _
                                                 & " COACONFIG.EDIT_MODE                       AS EDIT_MODE                " & vbNewLine _
                                                 & ",COACONFIG.DEST_CD_START_POS               AS DEST_CD_START_POS        " & vbNewLine _
                                                 & ",COACONFIG.DEST_CD_LEN                     AS DEST_CD_LEN              " & vbNewLine _
                                                 & ",COACONFIG.GOODS_CD_START_POS              AS GOODS_CD_START_POS       " & vbNewLine _
                                                 & ",COACONFIG.GOODS_CD_LEN                    AS GOODS_CD_LEN             " & vbNewLine _
                                                 & ",COACONFIG.LOT_NO_START_POS                AS LOT_NO_START_POS         " & vbNewLine _
                                                 & ",COACONFIG.LOT_NO_LEN                      AS LOT_NO_LEN               " & vbNewLine _
                                                 & ",COACONFIG.SEPARATE_CHAR                   AS SEPARATE_CHAR            " & vbNewLine _
                                                 & ",COACONFIG.DEST_CD_COL_NO                  AS DEST_CD_COL_NO           " & vbNewLine _
                                                 & ",COACONFIG.GOODS_CD_COL_NO                 AS GOODS_CD_COL_NO          " & vbNewLine _
                                                 & ",COACONFIG.LOT_NO_COL_NO                   AS LOT_NO_COL_NO            " & vbNewLine _
        '(2012.10.24)修正END 要望番号1531


#End Region

#Region "FROM句"
    Private Const SQL_FROM_DATA As String = "FROM                                                                         " & vbNewLine _
                                          & "                      $LM_MST$..M_COA AS COA                                 " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_NRS_BR                   AS NRSBR          " & vbNewLine _
                                          & "              ON COA.NRS_BR_CD       = NRSBR.NRS_BR_CD                       " & vbNewLine _
                                          & "             AND NRSBR.SYS_DEL_FLG   = '0'                                   " & vbNewLine _
                                          & "      LEFT OUTER JOIN $LM_MST$..M_GOODS     AS GOODS                         " & vbNewLine _
                                          & "              ON GOODS.NRS_BR_CD     = COA.NRS_BR_CD                         " & vbNewLine _
                                          & "             AND GOODS.CUST_CD_L     = COA.CUST_CD_L                         " & vbNewLine _
                                          & "             AND GOODS.CUST_CD_M     = COA.CUST_CD_M                         " & vbNewLine _
                                          & "             AND GOODS.GOODS_CD_NRS  = COA.GOODS_CD_NRS                      " & vbNewLine _
                                          & "             AND GOODS.SYS_DEL_FLG   = '0'                                   " & vbNewLine

    Private Const SQL_FROM_GOODS As String = "FROM                                                                         " & vbNewLine _
                                          & "                      $LM_MST$..M_GOODS AS GOODS                                 " & vbNewLine


    'TODO:　(2012.09.27)追加START
    Private Const SQL_FROM_FOLDERPASS As String = "FROM                                                                         " & vbNewLine _
                                          & "             $LM_MST$..M_EDI_FOLDER_PASS AS FOLDERPASS                             " & vbNewLine
    'TODO:　(2012.09.27)追加END

    'TODO:　(2012.09.27)追加START
    Private Const SQL_FROM_COACONFIG As String = "FROM                                                                         " & vbNewLine _
                                          & "             $LM_MST$..M_COACONFIG AS COACONFIG                                   " & vbNewLine
    'TODO:　(2012.09.27)追加END

#End Region

    'TODO:　(2012.09.27)追加START
#Region "WHERE句"

    Private Const SQL_WHERE_FOLDERPASS As String = "WHERE                                                       " & vbNewLine _
                                         & "        FOLDERPASS.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
                                         & "AND     FOLDERPASS.WH_CD = @WH_CD                                   " & vbNewLine _
                                         & "AND     FOLDERPASS.CUST_CD_L = @CUST_CD_L                           " & vbNewLine _
                                         & "AND     FOLDERPASS.CUST_CD_M = @CUST_CD_M                           " & vbNewLine _
                                         & "AND     FOLDERPASS.EXECUTE_KB = '01'                                " & vbNewLine

    Private Const SQL_WHERE_COACONFIG As String = "WHERE                                                        " & vbNewLine _
                                         & "       COACONFIG.NRS_BR_CD = @NRS_BR_CD                             " & vbNewLine _
                                         & "AND    COACONFIG.CUST_CD_L = @CUST_CD_L                             " & vbNewLine _
                                         & "AND    COACONFIG.CUST_CD_M = @CUST_CD_M                             " & vbNewLine


#End Region
    'TODO:　(2012.09.27)追加END

#Region "入力チェック"

    ''' <summary>
    ''' 存在チェック用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_EXIT_COA As String = "SELECT                                      " & vbNewLine _
                                            & "   COUNT(NRS_BR_CD)  AS REC_CNT          " & vbNewLine _
                                            & "FROM $LM_MST$..M_COA                     " & vbNewLine _
                                            & "WHERE NRS_BR_CD        = @NRS_BR_CD      " & vbNewLine _
                                            & "  AND GOODS_CD_NRS     = @GOODS_CD_NRS   " & vbNewLine _
                                            & "  AND LOT_NO           = @LOT_NO         " & vbNewLine _
                                            & "  AND DEST_CD          = @DEST_CD        " & vbNewLine

#End Region

#End Region

#Region "設定処理 SQL"

    ''' <summary>
    ''' 新規登録SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT As String = "INSERT INTO $LM_MST$..M_COA        " & vbNewLine _
                                       & "(                                  " & vbNewLine _
                                       & "       NRS_BR_CD                   " & vbNewLine _
                                       & "      ,GOODS_CD_NRS                " & vbNewLine _
                                       & "      ,LOT_NO                      " & vbNewLine _
                                       & "      ,DEST_CD                     " & vbNewLine _
                                       & "--ADD START 2018/12/18 要望管理003858 " & vbNewLine _
                                       & "      ,INKA_DATE                   " & vbNewLine _
                                       & "      ,INKA_DATE_VERS_FLG          " & vbNewLine _
                                       & "--ADD END   2018/12/18 要望管理003858 " & vbNewLine _
                                       & "      ,CUST_CD_L                   " & vbNewLine _
                                       & "      ,CUST_CD_M                   " & vbNewLine _
                                       & "      ,COA_LINK                    " & vbNewLine _
                                       & "      ,COA_NAME                    " & vbNewLine _
                                       & "      ,SYS_ENT_DATE                " & vbNewLine _
                                       & "      ,SYS_ENT_TIME                " & vbNewLine _
                                       & "      ,SYS_ENT_PGID                " & vbNewLine _
                                       & "      ,SYS_ENT_USER                " & vbNewLine _
                                       & "      ,SYS_UPD_DATE                " & vbNewLine _
                                       & "      ,SYS_UPD_TIME                " & vbNewLine _
                                       & "      ,SYS_UPD_PGID                " & vbNewLine _
                                       & "      ,SYS_UPD_USER                " & vbNewLine _
                                       & "      ,SYS_DEL_FLG                 " & vbNewLine _
                                       & "      ) VALUES (                   " & vbNewLine _
                                       & "       @NRS_BR_CD                  " & vbNewLine _
                                       & "      ,@GOODS_CD_NRS               " & vbNewLine _
                                       & "      ,@LOT_NO                     " & vbNewLine _
                                       & "      ,@DEST_CD                    " & vbNewLine _
                                       & "--ADD START 2018/12/18 要望管理003858 " & vbNewLine _
                                       & "      ,@INKA_DATE                  " & vbNewLine _
                                       & "      ,@INKA_DATE_VERS_FLG         " & vbNewLine _
                                       & "--ADD END   2018/12/18 要望管理003858 " & vbNewLine _
                                       & "      ,@CUST_CD_L                  " & vbNewLine _
                                       & "      ,@CUST_CD_M                  " & vbNewLine _
                                       & "      ,@COA_LINK                   " & vbNewLine _
                                       & "      ,@COA_NAME                   " & vbNewLine _
                                       & "      ,@SYS_ENT_DATE               " & vbNewLine _
                                       & "      ,@SYS_ENT_TIME               " & vbNewLine _
                                       & "      ,@SYS_ENT_PGID               " & vbNewLine _
                                       & "      ,@SYS_ENT_USER               " & vbNewLine _
                                       & "      ,@SYS_UPD_DATE               " & vbNewLine _
                                       & "      ,@SYS_UPD_TIME               " & vbNewLine _
                                       & "      ,@SYS_UPD_PGID               " & vbNewLine _
                                       & "      ,@SYS_UPD_USER               " & vbNewLine _
                                       & "      ,@SYS_DEL_FLG                " & vbNewLine _
                                       & ")                                  " & vbNewLine
    ''' <summary>
    ''' 更新SQL
    ''' </summary>
    ''' <remarks></remarks>

    Private Const SQL_UPDATE As String = "UPDATE $LM_MST$..M_COA SET                            " & vbNewLine _
                                       & "        COA_LINK              = @COA_LINK             " & vbNewLine _
                                       & "       ,COA_NAME              = @COA_NAME             " & vbNewLine _
                                       & "       ,SYS_UPD_DATE          = @SYS_UPD_DATE         " & vbNewLine _
                                       & "       ,SYS_UPD_TIME          = @SYS_UPD_TIME         " & vbNewLine _
                                       & "       ,SYS_UPD_PGID          = @SYS_UPD_PGID         " & vbNewLine _
                                       & "       ,SYS_UPD_USER          = @SYS_UPD_USER         " & vbNewLine _
                                       & "       ,SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                       & " WHERE                                                " & vbNewLine _
                                       & "         NRS_BR_CD            = @NRS_BR_CD            " & vbNewLine _
                                       & " AND     GOODS_CD_NRS         = @GOODS_CD_NRS         " & vbNewLine _
                                       & " AND     LOT_NO               = @LOT_NO               " & vbNewLine _
                                       & " AND     DEST_CD              = @DEST_CD              " & vbNewLine _
                                       & "--ADD START 2018/12/18 要望管理003858                 " & vbNewLine _
                                       & " AND     INKA_DATE            = @INKA_DATE            " & vbNewLine _
                                       & " AND     INKA_DATE_VERS_FLG   = @INKA_DATE_VERS_FLG   " & vbNewLine _
                                       & "--ADD END   2018/12/18 要望管理003858                 " & vbNewLine
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

#Region "検索処理"

    ''' <summary>
    ''' 分析票管理マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>分析票管理マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH810COAIN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH810DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMH810DAC.SQL_FROM_DATA)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionMasterSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH810DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("COA_LINK", "COA_LINK")
        map.Add("COA_NAME", "COA_NAME")
        map.Add("SYS_DEL_FLG", "SYS_DEL_FLG")


        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH810OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 分析票管理マスタ更新対象データ検索(商品マスタ：商品キー)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>分析票管理マスタ更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function GetGoodsCdNrs(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH810COAIN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH810DAC.SQL_SELECT_GOODS)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMH810DAC.SQL_FROM_GOODS)        'SQL構築(データ抽出用from句)
        Call Me.SetConditionGoodsSQL()                   '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH810DAC", "GetGoodsCdNrs", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")



        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH810GOODS")

        Return ds

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        Me._StrSql.Append("WHERE")
        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            Me._StrSql.Append(" (COA.NRS_BR_CD = @NRS_BR_CD)")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            whereStr = .Item("GOODS_CD_NRS").ToString()
            Me._StrSql.Append(" AND GOODS.GOODS_CD_NRS = @GOODS_CD_NRS")
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", whereStr, DBDataType.NVARCHAR))

            whereStr = .Item("LOT_NO").ToString()
            Me._StrSql.Append(" AND COA.LOT_NO = @LOT_NO")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", whereStr, DBDataType.NVARCHAR))

            whereStr = .Item("DEST_CD").ToString()
            Me._StrSql.Append(" AND COA.DEST_CD = @DEST_CD")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", whereStr, DBDataType.NVARCHAR))

            'ADD START 2018/12/18 要望管理003858
            whereStr = String.Empty
            Me._StrSql.Append(" AND COA.INKA_DATE = @INKA_DATE")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", whereStr, DBDataType.NVARCHAR))

            whereStr = "1"
            Me._StrSql.Append(" AND COA.INKA_DATE_VERS_FLG = @INKA_DATE_VERS_FLG")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_VERS_FLG", whereStr, DBDataType.NVARCHAR))
            'ADD END   2018/12/18 要望管理003858

            whereStr = .Item("CUST_CD_L").ToString()
            Me._StrSql.Append(" AND COA.CUST_CD_L = @CUST_CD_L")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))

            whereStr = .Item("CUST_CD_M").ToString()
            Me._StrSql.Append(" AND COA.CUST_CD_M = @CUST_CD_M")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionGoodsSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        Me._StrSql.Append("WHERE")
        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            whereStr = .Item("NRS_BR_CD").ToString()
            Me._StrSql.Append(" GOODS.NRS_BR_CD = @NRS_BR_CD")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            'ADD START 2019/06/20 要望管理005443
            whereStr = .Item("CUST_CD_L").ToString()
            Me._StrSql.Append(" AND GOODS.CUST_CD_L = @CUST_CD_L")
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))

            whereStr = .Item("CUST_CD_M").ToString()
            Me._StrSql.Append(" AND GOODS.CUST_CD_M = @CUST_CD_M")
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
            'ADD END 2019/06/20 要望管理005443

            whereStr = .Item("GOODS_CD_CUST").ToString()
            Me._StrSql.Append(" AND GOODS.GOODS_CD_CUST = @GOODS_CD_CUST")
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_CUST", whereStr, DBDataType.NVARCHAR))

            Me._StrSql.Append(" AND GOODS.SYS_DEL_FLG = '0'")


        End With

    End Sub

    'TODO:　(2012.09.27)追加START
    ''' <summary>
    ''' フォルダパス取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>EDIフォルダパスマスタ取得SQLの構築・発行</remarks>
    Private Function GetCustCoaFolder(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH810IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH810DAC.SQL_SELECT_FOLDERPASS)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMH810DAC.SQL_FROM_FOLDERPASS)        'SQL構築(データ抽出用from句)
        Me._StrSql.Append(LMH810DAC.SQL_WHERE_FOLDERPASS)       'SQL構築(データ抽出用Where句)
        Call Me.SetinParameter()                                '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH810DAC", "GetCustCoaFolder", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("COA_FOLDER", "COA_FOLDER")
        map.Add("COA_BACKUP", "COA_BACKUP")
        map.Add("COA_EXTENTION", "COA_EXTENTION")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH810COA_FOLDER")

        Return ds

    End Function
    'TODO:　(2012.09.27)追加END

    'TODO:　(2012.09.27)追加START
    ''' <summary>
    ''' 取込項目の開始位置・桁数取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>分析票取込マスタ取得SQLの構築・発行</remarks>
    Private Function GetMcoaConfig(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH810IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH810DAC.SQL_SELECT_COACONFIG)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMH810DAC.SQL_FROM_COACONFIG)        'SQL構築(データ抽出用from句)
        Me._StrSql.Append(LMH810DAC.SQL_WHERE_COACONFIG)       'SQL構築(データ抽出用Where句)
        Call Me.SetinParameter()                                '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH810DAC", "GetMcoaConfig", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '(2012.10.24)修正START 要望番号1531
        '取得データの格納先をマッピング
        map.Add("EDIT_MODE", "EDIT_MODE")
        map.Add("DEST_CD_START_POS", "DEST_CD_START_POS")
        map.Add("DEST_CD_LEN", "DEST_CD_LEN")
        map.Add("GOODS_CD_START_POS", "GOODS_CD_START_POS")
        map.Add("GOODS_CD_LEN", "GOODS_CD_LEN")
        map.Add("LOT_NO_START_POS", "LOT_NO_START_POS")
        map.Add("LOT_NO_LEN", "LOT_NO_LEN")
        map.Add("SEPARATE_CHAR", "SEPARATE_CHAR")
        map.Add("DEST_CD_COL_NO", "DEST_CD_COL_NO")
        map.Add("GOODS_CD_COL_NO", "GOODS_CD_COL_NO")
        map.Add("LOT_NO_COL_NO", "LOT_NO_COL_NO")
        '(2012.10.24)修正END 要望番号1531

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH810COACONFIG")

        Return ds

    End Function
    'TODO:　(2012.09.27)追加END

    'TODO:　(2012.09.27)追加START
    ''' <summary>
    ''' 各営業所の分析票格納パス取得(区分マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>区分マスタ取得SQLの構築・発行</remarks>
    Private Function GetBrCoaFolder(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH810IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMH810DAC.SQL_SELECT_Z_KBN)           'SQL構築(データ抽出用)
        'Call Me.SetKbnParameter()                                '条件設定
        '追加開始 --- 2014.08.21 kikuchi
        Call Me.SetParamKbnChk()                                  '条件設定
        '追加終了 ---
        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH810DAC", "GetBrCoaFolder", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("COA_FOLDER", "COA_FOLDER")
        map.Add("COA_ERR_FOLDER", "COA_ERR_FOLDER") 'ADD 2020/10/22 015075

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMH810COA_FOLDER")

        Return ds

    End Function
    'TODO:　(2012.09.27)追加END

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 分析票管理マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>分析票管理マスタ件数取得SQLの構築・発行</remarks>
    Private Function CheckExistCoaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH810COAIN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH810DAC.SQL_EXIT_COA, Me._Row.Item("NRS_BR_CD").ToString()))

        Dim reader As SqlDataReader = Nothing

        'SQLパラメータ初期化/設定
        Call Me.SetParamExistChk()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH810DAC", "CheckExistCoaM", cmd)

        'SQLの発行
        reader = MyBase.GetSelectResult(cmd)

        cmd.Parameters.Clear()

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function



    ''' <summary>
    ''' 分析票管理マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>分析票管理マスタ新規登録SQLの構築・発行</remarks>
    Private Function InsertCoaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH810COAIN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH810DAC.SQL_INSERT, Me._Row.Item("NRS_BR_CD").ToString()))

        '新規登録処理件数設定用
        Dim insCnt As Integer = 0

        'SQLパラメータ初期化/設定
        Call Me.SetParamInsert()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH810DAC", "InsertCoaM", cmd)


        MyBase.SetResultCount(MyBase.GetInsertResult(cmd))

        Return ds

    End Function

    ''' <summary>
    ''' 分析票管理マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>分析票管理マスタ更新SQLの構築・発行</remarks>
    Private Function UpdateCoaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMH810COAIN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMH810DAC.SQL_UPDATE, Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化/設定
        Call Me.SetParamUpdate()

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMH810DAC", "UpdateCoaM", cmd)


        '更新時排他チェック
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 更新時排他チェック
    ''' </summary>
    ''' <param name="cmd">更新SQL</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

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

#Region "パラメータ設定"

    'TODO:　(2012.09.27)追加START
    ''' <summary>
    ''' パラメータ設定(EDIフォルダパスマスタ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetinParameter()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))

        End With

    End Sub
    'TODO:　(2012.09.27)追加END

    ''' <summary>
    ''' パラメータ設定モジュール(分析票管理マスタ存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamExistChk()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub

    '追加開始 --- 2014.08.21 kikuchi
    ''' <summary>
    ''' パラメータ設定モジュール(区分マスタ取得)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamKbnChk()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        End With

    End Sub
    '追加終了 ---


    ''' <summary>
    ''' パラメータ設定モジュール(新規登録)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamInsert()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call Me.SetComParam()

        'システム項目
        Call Me.SetParamCommonSystemIns()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamUpdate()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '共通項目
        Call Me.SetComParam()

        '更新項目
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(更新登録用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetComParam()

        With Me._Row
            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.CHAR))
            'ADD START 2018/12/18 要望管理003858
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE", String.Empty, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_DATE_VERS_FLG", "1", DBDataType.CHAR))
            'ADD END   2018/12/18 要望管理003858
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_LINK", .Item("COA_LINK").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@COA_NAME", .Item("COA_NAME").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))
        Call Me.SetParamCommonSystemUpd()

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(削除・復活時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemDel()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", Me._Row.Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@LOT_NO", Me._Row.Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me._Row.Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", Me._Row.Item("SYS_DEL_FLG").ToString(), DBDataType.CHAR))

    End Sub

#End Region

#End Region

#End Region

End Class
