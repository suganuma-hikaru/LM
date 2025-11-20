' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫管理
'  プログラムID     :  LMD000    : 在庫共通（最終請求日取得）
'  作  成  者       :  [金]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD000DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD000DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "請求日を取得"

    Public Const SQL_SELECT_HOKAN_CHK_DATE As String = "    SELECT                                 " & vbNewLine _
                                               & "      HOKAN_NIYAKU_CALCULATION                   " & vbNewLine _
                                               & "    FROM                                         " & vbNewLine _
                                               & "      $LM_MST$..M_CUST CUST                      " & vbNewLine _
                                               & "    LEFT JOIN                                    " & vbNewLine _
                                               & "      (                                          " & vbNewLine _
                                               & "       SELECT                                    " & vbNewLine _
                                               & "           CUST_CD_L                             " & vbNewLine _
                                               & "         , CUST_CD_M                             " & vbNewLine _
                                               & "         , CUST_CD_S                             " & vbNewLine _
                                               & "         , CUST_CD_SS                            " & vbNewLine _
                                               & "       FROM                                      " & vbNewLine _
                                               & "         $LM_MST$..M_GOODS                       " & vbNewLine _
                                               & "       WHERE                                     " & vbNewLine _
                                               & "         NRS_BR_CD = @NRS_BR_CD                  " & vbNewLine _
                                               & "       AND                                       " & vbNewLine _
                                               & "         GOODS_CD_NRS = @GOODS_CD_NRS            " & vbNewLine _
                                               & "      ) GOODS                                    " & vbNewLine _
                                               & "    ON                                           " & vbNewLine _
                                               & "     CUST.NRS_BR_CD = @NRS_BR_CD                 " & vbNewLine _
                                               & "    WHERE                                        " & vbNewLine _
                                               & "     CUST.CUST_CD_L = GOODS.CUST_CD_L            " & vbNewLine _
                                               & "    AND                                          " & vbNewLine _
                                               & "     CUST.CUST_CD_M = GOODS.CUST_CD_M            " & vbNewLine _
                                               & "    AND                                          " & vbNewLine _
                                               & "     CUST.CUST_CD_S = GOODS.CUST_CD_S            " & vbNewLine _
                                               & "    AND                                          " & vbNewLine _
                                               & "     CUST.CUST_CD_SS = GOODS.CUST_CD_SS          " & vbNewLine

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

#Region "データ取得"

    ''' <summary>
    ''' 最新の請求日検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectSeiqDate(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMD000IN")
        Dim outTbl As DataTable = ds.Tables("LMD000OUT")

        For i As Integer = 0 To inTbl.Rows.Count - 1

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)
            Dim row As DataRow = ds.Tables("LMD000OUT").NewRow
            row.Item("CHK_DATE") = Me._Row.Item("CHK_DATE")
            row.Item("REPLACE_STR1") = Me._Row.Item("REPLACE_STR1")
            row.Item("REPLACE_STR2") = Me._Row.Item("REPLACE_STR2")
            row.Item("HOKAN_NIYAKU_CALCULATION") = Me.GettSeiqDate()
            outTbl.Rows.Add(row)

        Next

        Return ds
      
    End Function

    ''' <summary>
    ''' 最新の請求日検索
    ''' </summary>
    ''' <returns>String</returns>
    ''' <remarks>検索結果取得SQLの構築・発行</remarks>
    Private Function GettSeiqDate() As String

        'SQL設定
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMD000DAC.SQL_SELECT_HOKAN_CHK_DATE)
        Dim calDate As String = String.Empty

        'パラメータの設定
        Call Me.SetCondition()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMD000DAC", "SelectSeiqDate", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '処理件数の設定
        reader.Read()
        calDate = reader("HOKAN_NIYAKU_CALCULATION").ToString()
        reader.Close()

        Return calDate

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetCondition()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty

        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))

            whereStr = .Item("GOODS_CD_NRS").ToString()
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", whereStr, DBDataType.CHAR))

        End With

    End Sub

#End Region 'データ取得

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

#End Region 'SQL

#End Region 'Method

End Class
