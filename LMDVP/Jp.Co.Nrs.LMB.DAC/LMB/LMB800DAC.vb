' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷管理
'  プログラムID     :  LMB800    : GHSラベル印刷
'  作  成  者       :  [daikoku]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB800DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB800DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

 

    ''' <summary>
    ''' 印刷データ抽出用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_DATA As String = " SELECT                                                                                   " & vbNewLine _
                                        & "  SUBSTRING(ISNULL(BIL.REMARK,''),1,7)              AS NYUKO_NO       --入庫番号          " & vbNewLine _
                                        & " ,CONCAT(BIL.INKA_NO_L,BIM.INKA_NO_M,BIS.INKA_NO_S) AS INKA_NO                            " & vbNewLine _
                                        & " ,MGS.GOODS_CD_CUST                                 AS GOODS_CD_CUST                      " & vbNewLine _
                                        & " ,MGS.GOODS_NM_1                                    AS GOODS_NM                           " & vbNewLine _
                                        & " ,BIS.LOT_NO                                        AS LOT_NO                             " & vbNewLine _
                                        & " ,BIS.IRIME                                         AS IRIME                              " & vbNewLine _
                                        & " ,MGS.STD_IRIME_UT                                  AS STD_IRIME_UT                       " & vbNewLine _
                                        & " ,BIS.KONSU                                         AS KONSU                              " & vbNewLine _
                                        & " ,MGS.PKG_NB                                        AS PKG_NB                             " & vbNewLine _
                                        & " ,(BIS.KONSU * MGS.PKG_NB) + BIS.HASU               AS KOSU                               " & vbNewLine _
                                        & " ,MGS.NB_UT                                         AS NB_UT                              " & vbNewLine _
                                        & " ,BIL.NRS_BR_CD                                     AS NRS_BR_CD                          " & vbNewLine _
                                        & " ,BIL.INKA_DATE                                     AS INKA_DATE                          " & vbNewLine _
                                        & " ,BIL.CUST_CD_L                                     AS CUST_CD_L                          " & vbNewLine _
                                        & " ,BIL.CUST_CD_M                                     AS CUST_CD_M                          " & vbNewLine _
                                        & " ,ISNULL(KBN_N001.KBN_NM1,'')                       AS NB_UT_NM                           " & vbNewLine _
                                        & " ,ISNULL(MGSD.SET_NAIYO,'')                         AS PDF_NM    --PDF名                  " & vbNewLine _
                                        & " ,ISNULL(MGSD2.SET_NAIYO,'')                        AS PDF_NO    --PDFパタン番号          " & vbNewLine _
                                        & " ,ISNULL(KBN_F021.KBN_NM4,'')                       AS FOLDER    --CSV設定フォルダー      " & vbNewLine _
                                        & " ,ISNULL(KBN_F021.KBN_NM5,'')                       AS BTW_NM    --BarTender名称          " & vbNewLine

    ''' <summary>
    ''' データ抽出用FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_FROM As String = " FROM                                                                        " & vbNewLine _
                                        & "  $LM_TRN$..B_INKA_L BIL                                                     " & vbNewLine _
                                        & "  LEFT JOIN                                                                  " & vbNewLine _
                                        & "       $LM_TRN$..B_INKA_M BIM                                                " & vbNewLine _
                                        & "     ON                                                                      " & vbNewLine _
                                        & "         BIL.NRS_BR_CD   = BIM.NRS_BR_CD                                     " & vbNewLine _
                                        & "     AND BIL.INKA_NO_L   = BIM.INKA_NO_L                                     " & vbNewLine _
                                        & "     AND BIM.SYS_DEL_FLG = '0'                                               " & vbNewLine _
                                        & "   LEFT JOIN                                                                 " & vbNewLine _
                                        & "        $LM_TRN$..B_INKA_S BIS                                               " & vbNewLine _
                                        & "      ON                                                                     " & vbNewLine _
                                        & "          BIM.NRS_BR_CD   = BIS.NRS_BR_CD                                    " & vbNewLine _
                                        & "      AND BIM.INKA_NO_L   = BIS.INKA_NO_L                                    " & vbNewLine _
                                        & "      AND BIM.INKA_NO_M   = BIS.INKA_NO_M                                    " & vbNewLine _
                                        & "      AND BIS.SYS_DEL_FLG = '0'                                              " & vbNewLine _
                                        & "  LEFT JOIN                                                                  " & vbNewLine _
                                        & "       $LM_MST$..M_GOODS MGS                                                 " & vbNewLine _
                                        & "     ON                                                                      " & vbNewLine _
                                        & "         MGS.NRS_BR_CD    = BIM.NRS_BR_CD                                    " & vbNewLine _
                                        & "     AND MGS.GOODS_CD_NRS = BIM.GOODS_CD_NRS                                 " & vbNewLine _
                                        & "  --PDF名                                                                    " & vbNewLine _
                                        & "   LEFT JOIN                                                                 " & vbNewLine _
                                        & "        $LM_MST$..M_GOODS_DETAILS MGSD                                       " & vbNewLine _
                                        & "      ON                                                                     " & vbNewLine _
                                        & "          MGSD.NRS_BR_CD    = BIM.NRS_BR_CD                                  " & vbNewLine _
                                        & "      AND MGSD.GOODS_CD_NRS = BIM.GOODS_CD_NRS                               " & vbNewLine _
                                        & "      AND MGSD.SUB_KB       = '59'                                           " & vbNewLine _
                                        & "    --PDF種類番号                                                            " & vbNewLine _
                                        & "   LEFT JOIN                                                                 " & vbNewLine _
                                        & "        $LM_MST$..M_GOODS_DETAILS MGSD2                                      " & vbNewLine _
                                        & "      ON                                                                     " & vbNewLine _
                                        & "          MGSD2.NRS_BR_CD    = BIM.NRS_BR_CD                                 " & vbNewLine _
                                        & "      AND MGSD2.GOODS_CD_NRS = BIM.GOODS_CD_NRS                              " & vbNewLine _
                                        & "      AND MGSD2.SUB_KB       = '60'                                          " & vbNewLine _
                                        & "   LEFT JOIN                                                                 " & vbNewLine _
                                        & "        $LM_MST$..Z_KBN KBN_N001                                             " & vbNewLine _
                                        & "      ON                                                                     " & vbNewLine _
                                        & "         KBN_N001.KBN_GROUP_CD = 'N001'                                      " & vbNewLine _
                                        & "     AND KBN_N001.KBN_CD = MGS.NB_UT                                         " & vbNewLine _
                                        & "   LEFT JOIN  --BarTender設定フォルダー                                      " & vbNewLine _
                                        & "        $LM_MST$..Z_KBN KBN_F021                                             " & vbNewLine _
                                        & "      ON                                                                     " & vbNewLine _
                                        & "          KBN_F021.KBN_GROUP_CD = 'F021'                                     " & vbNewLine _
                                        & "      AND KBN_F021.KBN_NM1 = BIL.NRS_BR_CD                                   " & vbNewLine _
                                        & "      AND KBN_F021.KBN_NM2 = BIL.CUST_CD_L                                   " & vbNewLine _
                                        & "      AND KBN_F021.KBN_NM3 = MGSD2.SET_NAIYO                                 " & vbNewLine _
                                        & "  WHERE                                                                      " & vbNewLine _
                                        & "         BIL.INKA_NO_L   = @INKA_NO_L         ----パラメータ                 " & vbNewLine _
                                        & "     AND BIL.NRS_BR_CD   = @NRS_BR_CD         ----パラメータ                 " & vbNewLine _
                                        & "     AND BIL.SYS_DEL_FLG = '0'                                               " & vbNewLine






    ''' <summary>
    ''' ORDER BY（①営業所、②管理番号）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY                                            " & vbNewLine _
                                        & "        BIL.NRS_BR_CD                          　    " & vbNewLine _
                                        & "       ,BIL.INKA_NO_L                            　  " & vbNewLine

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
    ''' 入荷データLテーブル対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入荷データLテーブル更新対象データ一覧検索結果取得SQLの構築・発行</remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMB800IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMB800DAC.SQL_SELECT_DATA)      'SQL構築(データ抽出用Select句)
        Me._StrSql.Append(LMB800DAC.SQL_FROM)             'SQL構築(データ抽出用From句)
        Call Me.SetConditionMasterSQL()                   'SQL構築(条件設定)
        Me._StrSql.Append(LMB800DAC.SQL_ORDER_BY)         'SQL構築(データ抽出用ORDER BY句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMB800DAC", "SelectPrintData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NYUKO_NO", "NYUKO_NO")
        map.Add("INKA_NO", "INKA_NO")
        map.Add("GOODS_CD_CUST", "GOODS_CD_CUST")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("LOT_NO", "LOT_NO")
        map.Add("IRIME", "IRIME")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("KONSU", "KONSU")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("KOSU", "KOSU")
        map.Add("NB_UT", "NB_UT")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INKA_DATE", "INKA_DATE")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("NB_UT_NM", "NB_UT_NM")
        map.Add("PDF_NM", "PDF_NM")
        map.Add("PDF_NO", "PDF_NO")
        map.Add("FOLDER", "FOLDER")
        map.Add("BTW_NM", "BTW_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMB800OUT")

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
        With Me._Row

            '営業所
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            '入荷管理番号
            whereStr = .Item("INKA_NO_L").ToString()
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", whereStr, DBDataType.CHAR))

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

End Class
