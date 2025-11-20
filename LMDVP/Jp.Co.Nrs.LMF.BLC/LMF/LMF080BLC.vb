' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブ
'  プログラムID     :  LMF080    : 支払検索
'  作  成  者       :  YANAI
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMF080BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF080BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMF080DAC = New LMF080DAC()

#End Region

#Region "Const"

    ''' <summary>
    ''' 運送の更新処理アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_UNSO As String = "UpdateUnsoLSysData"

    ''' <summary>
    ''' 運賃の更新処理アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_UNCHIN As String = "UpdateUnchinData"

    ''' <summary>
    ''' 運賃検索対象データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_DATA As String = "SelectListData"

    ''' <summary>
    ''' 按分対象データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ANBUN_DATA As String = "SelectAnbunData"

    ''' <summary>
    ''' データセットテーブル名(INテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMF080IN"

    ''' <summary>
    ''' データセットテーブル名(OUTテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMF080OUT"

    ''' <summary>
    ''' データセットテーブル名(UNCHINテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNCHIN As String = "UNCHIN"

    ''' <summary>
    ''' データセットテーブル名(G_HEDテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_G_HED As String = "G_HED"

    ''' <summary>
    ''' OUT_ANBUNテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT_ANBUN As String = "LMF080OUT_ANBUN"

    ''' <summary>
    ''' 元データ区分(入荷)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const MOTO_DATA_NYUKA As String = "10"

    ''' <summary>
    ''' 運賃計算締め基準(出荷日)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CALC_SHUKKA As String = "01"

    ''' <summary>
    ''' 運賃計算締め基準(納入日)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CALC_NYUKA As String = "02"

    ''' <summary>
    ''' 経理取込
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TORIKOMI As String = "経理取込"

    ''' <summary>
    ''' 確定解除
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FIX_CANCELL As String = "確定解除"

    ''' <summary>
    ''' まとめ指示
    ''' </summary>
    ''' <remarks></remarks>
    Private Const GROUP As String = "まとめ指示"

    ''' <summary>
    ''' まとめ解除
    ''' </summary>
    ''' <remarks></remarks>
    Private Const GROUP_CANCELL As String = "まとめ解除"

    ''' <summary>
    ''' 一括変更
    ''' </summary>
    ''' <remarks></remarks>
    Private Const IKKATU As String = "一括変更"

    ''' <summary>
    ''' まとめ条件(荷主 , 運行番号)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const MATOME_CUSTTRIP As String = "01"

    ''' <summary>
    ''' まとめ条件(届先コード , 運行番号)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const MATOME_DEST As String = "02"

    ''' <summary>
    ''' まとめ条件(届先JIS , 運行番号)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const MATOME_DESTJIS As String = "03"

    'START YANAI 要望番号1424 支払処理
    ''' <summary>
    ''' まとめ条件(届先住所 , 運行番号)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const MATOME_DESTAD As String = "04"
    'END YANAI 要望番号1424 支払処理

    ''' <summary>
    ''' フラグ(ON)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FLG_ON As String = "01"

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const GUIDANCE_KBN As String = "00"

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 運賃検索対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'メッセージコードの設定
        Call Me.SetSelectErrMes(MyBase.GetResultCount())

        Return ds

    End Function

    ''' <summary>
    ''' 運賃検索対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 運賃検索対象データ検索(まとめ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListGroupData(ByVal ds As DataSet) As DataSet

        ds = Me.DacAccess(ds, LMF080BLC.ACTION_ID_DATA)
        Dim dt As DataTable = ds.Tables(LMF080BLC.TABLE_NM_OUT)
        Dim dr1 As DataRow = Nothing
        Dim dr2 As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1
        Dim max1 As Integer = 0
        Dim idx As Integer = 0
        Dim orderBy As String = ds.Tables(LMF080BLC.TABLE_NM_IN).Rows(0).Item("ORDER_BY").ToString()
        Dim unsoDate As String = String.Empty
        Dim tariffCd As String = String.Empty
        Dim seiq As String = String.Empty
        Dim extc As String = String.Empty
        Dim tax As String = String.Empty
        Dim item1 As String = String.Empty
        Dim item2 As String = String.Empty
        Dim item3 As String() = Nothing
        Dim outDr() As DataRow = Nothing
        Dim nrsBrCd As String = ds.Tables(LMF080BLC.TABLE_NM_IN).Rows(0).Item("NRS_BR_CD").ToString()

        Dim flgOn As Boolean = True
        Dim flgOff As Boolean = False

        For i As Integer = 0 To max

            dr1 = dt.Rows(i)

            '既に相手が見つかっているレコードの場合、スルー
            If (LMConst.FLG.ON).Equals(dr1.Item("GROUP_FLG").ToString()) = flgOn Then
                Continue For
            End If

            Select Case orderBy
                Case LMF080BLC.MATOME_CUSTTRIP
                    '①荷主、運行番号の場合
                    If String.IsNullOrEmpty(dr1.Item("CUST_CD_L").ToString()) = True OrElse _
                        String.IsNullOrEmpty(dr1.Item("CUST_CD_M").ToString()) = True OrElse _
                        String.IsNullOrEmpty(dr1.Item("ARR_PLAN_DATE").ToString()) = True OrElse _
                        String.IsNullOrEmpty(dr1.Item("SHIHARAITO_CD").ToString()) = True OrElse _
                        String.IsNullOrEmpty(dr1.Item("SHIHARAI_TARIFF_CD").ToString()) = True OrElse _
                        String.IsNullOrEmpty(dr1.Item("TAX_KB").ToString()) = True Then
                        Continue For
                    End If
                    outDr = dt.Select(String.Concat("CUST_CD_L = '", dr1.Item("CUST_CD_L").ToString(), "' AND ", _
                                                    "CUST_CD_M = '", dr1.Item("CUST_CD_M").ToString(), "' AND ", _
                                                    "TRIP_NO = '", dr1.Item("TRIP_NO").ToString(), "' AND ", _
                                                    "ARR_PLAN_DATE = '", dr1.Item("ARR_PLAN_DATE").ToString(), "' AND ", _
                                                    "SHIHARAITO_CD = '", dr1.Item("SHIHARAITO_CD").ToString(), "' AND ", _
                                                    "SHIHARAI_TARIFF_CD = '", dr1.Item("SHIHARAI_TARIFF_CD").ToString(), "' AND ", _
                                                    "SHIHARAI_ETARIFF_CD = '", dr1.Item("SHIHARAI_ETARIFF_CD").ToString(), "' AND ", _
                                                    "TAX_KB = '", dr1.Item("TAX_KB").ToString(), "'"))

                Case LMF080BLC.MATOME_DEST
                    '②届先、運行番号の場合
                    If String.IsNullOrEmpty(dr1.Item("DEST_CD").ToString()) = True OrElse _
                        String.IsNullOrEmpty(dr1.Item("ARR_PLAN_DATE").ToString()) = True OrElse _
                        String.IsNullOrEmpty(dr1.Item("SHIHARAITO_CD").ToString()) = True OrElse _
                        String.IsNullOrEmpty(dr1.Item("SHIHARAI_TARIFF_CD").ToString()) = True OrElse _
                        String.IsNullOrEmpty(dr1.Item("TAX_KB").ToString()) = True Then
                        Continue For
                    End If
                    outDr = dt.Select(String.Concat("DEST_CD = '", dr1.Item("DEST_CD").ToString(), "' AND ", _
                                                    "TRIP_NO = '", dr1.Item("TRIP_NO").ToString(), "' AND ", _
                                                    "ARR_PLAN_DATE = '", dr1.Item("ARR_PLAN_DATE").ToString(), "' AND ", _
                                                    "SHIHARAITO_CD = '", dr1.Item("SHIHARAITO_CD").ToString(), "' AND ", _
                                                    "SHIHARAI_TARIFF_CD = '", dr1.Item("SHIHARAI_TARIFF_CD").ToString(), "' AND ", _
                                                    "SHIHARAI_ETARIFF_CD = '", dr1.Item("SHIHARAI_ETARIFF_CD").ToString(), "' AND ", _
                                                    "TAX_KB = '", dr1.Item("TAX_KB").ToString(), "'"))

                Case LMF080BLC.MATOME_DESTJIS
                    '③届先JIS、運行番号の場合
                    If String.IsNullOrEmpty(dr1.Item("DEST_JIS_CD").ToString()) = True OrElse _
                        String.IsNullOrEmpty(dr1.Item("ARR_PLAN_DATE").ToString()) = True OrElse _
                        String.IsNullOrEmpty(dr1.Item("SHIHARAITO_CD").ToString()) = True OrElse _
                        String.IsNullOrEmpty(dr1.Item("SHIHARAI_TARIFF_CD").ToString()) = True OrElse _
                        String.IsNullOrEmpty(dr1.Item("TAX_KB").ToString()) = True Then
                        Continue For
                    End If
                    outDr = dt.Select(String.Concat("DEST_JIS_CD = '", dr1.Item("DEST_JIS_CD").ToString(), "' AND ", _
                                                    "TRIP_NO = '", dr1.Item("TRIP_NO").ToString(), "' AND ", _
                                                    "ARR_PLAN_DATE = '", dr1.Item("ARR_PLAN_DATE").ToString(), "' AND ", _
                                                    "SHIHARAITO_CD = '", dr1.Item("SHIHARAITO_CD").ToString(), "' AND ", _
                                                    "SHIHARAI_TARIFF_CD = '", dr1.Item("SHIHARAI_TARIFF_CD").ToString(), "' AND ", _
                                                    "SHIHARAI_ETARIFF_CD = '", dr1.Item("SHIHARAI_ETARIFF_CD").ToString(), "' AND ", _
                                                    "TAX_KB = '", dr1.Item("TAX_KB").ToString(), "'"))

                    'START YANAI 要望番号1424 支払処理
                Case LMF080BLC.MATOME_DESTAD
                    '④届先住所、運行番号の場合
                    If String.IsNullOrEmpty(dr1.Item("DEST_AD").ToString()) = True OrElse _
                        String.IsNullOrEmpty(dr1.Item("ARR_PLAN_DATE").ToString()) = True OrElse _
                        String.IsNullOrEmpty(dr1.Item("SHIHARAITO_CD").ToString()) = True OrElse _
                        String.IsNullOrEmpty(dr1.Item("SHIHARAI_TARIFF_CD").ToString()) = True OrElse _
                        String.IsNullOrEmpty(dr1.Item("TAX_KB").ToString()) = True Then
                        Continue For
                    End If
                    outDr = dt.Select(String.Concat("DEST_AD = '", dr1.Item("DEST_AD").ToString(), "' AND ", _
                                                    "TRIP_NO = '", dr1.Item("TRIP_NO").ToString(), "' AND ", _
                                                    "ARR_PLAN_DATE = '", dr1.Item("ARR_PLAN_DATE").ToString(), "' AND ", _
                                                    "SHIHARAITO_CD = '", dr1.Item("SHIHARAITO_CD").ToString(), "' AND ", _
                                                    "SHIHARAI_TARIFF_CD = '", dr1.Item("SHIHARAI_TARIFF_CD").ToString(), "' AND ", _
                                                    "SHIHARAI_ETARIFF_CD = '", dr1.Item("SHIHARAI_ETARIFF_CD").ToString(), "' AND ", _
                                                    "TAX_KB = '", dr1.Item("TAX_KB").ToString(), "'"))
                    'END YANAI 要望番号1424 支払処理

            End Select

            max1 = outDr.Length - 1
            If max1 > 0 Then
                '同じ条件のデータが1件より多い場合のみ、まとめ対象
                For j As Integer = 0 To max1
                    outDr(j).Item("GROUP_FLG") = LMConst.FLG.ON
                Next
            End If
        Next

        '不要レコードの削除処理
        ds = Me.DeleteNotGroupData(ds)

        'メッセージコードの設定
        Call Me.SetSelectErrMes(ds.Tables(LMF080BLC.TABLE_NM_OUT).Rows.Count)

        Return ds

    End Function

    ''' <summary>
    ''' 最終請求日を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>請求日</returns>
    ''' <remarks>取得できない場合は"00000000"を返却</remarks>
    Private Function SelectGheaderData(ByVal ds As DataSet) As String

        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Dim dt As DataTable = ds.Tables(LMF080BLC.TABLE_NM_G_HED)
        If dt.Rows.Count < 1 Then

            Return "00000000"

        End If

        Return dt.Rows(0).Item("SKYU_DATE").ToString()

    End Function

    ''' <summary>
    ''' 運賃情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInitUnchinData(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 運賃情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinData(ByVal ds As DataSet) As DataSet

        '画面の情報を保持
        Dim guiDs As DataSet = ds.Copy
        Dim guiDt As DataTable = guiDs.Tables(LMF080BLC.TABLE_NM_UNCHIN)
        Dim guiDr As DataRow = guiDt.Rows(0)

        'START YANAI 要望番号1481 複数の支払いまとめを解除しようとしたらアベンド
        '排他チェック
        ds = Me.DacAccess(ds, "SelectUnchinDataCnt")
        If MyBase.IsMessageStoreExist() = True Then
            Return ds
        End If
        'END YANAI 要望番号1481 複数の支払いまとめを解除しようとしたらアベンド

        'まとめ処理を行っているレコードの場合、紐付いているレコード全てを更新
        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If
        Dim dt As DataTable = ds.Tables(LMF080BLC.TABLE_NM_UNCHIN)
        Dim drs As DataRow() = dt.Select(String.Concat(" UNSO_NO_L = '", guiDr.Item("UNSO_NO_L").ToString(), "' AND UNSO_NO_M = '", guiDr.Item("UNSO_NO_M").ToString(), "' "))

        '画面の更新日時を反映
        drs(0).Item("SYS_UPD_DATE") = guiDr.Item("SYS_UPD_DATE").ToString()
        drs(0).Item("SYS_UPD_TIME") = guiDr.Item("SYS_UPD_TIME").ToString()

        Return ds

    End Function

#End Region

#Region "設定処理"

#Region "確定、確定解除処理"

    'START YANAI 要望番号1424 支払処理 按分しないようにする
    '''' <summary>
    '''' 確定処理
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks></remarks>
    'Private Function SetFixData(ByVal ds As DataSet) As DataSet

    '    Dim newDs As DataSet = New LMF080DS()
    '    Dim newDt As DataTable = newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN)
    '    Dim newDr As DataRow = newDt.NewRow()

    '    Dim inDr() As DataRow = ds.Tables(LMF080BLC.TABLE_NM_UNCHIN).Select(String.Empty, " TRIP_NO, UNSO_NO_L, UNSO_NO_M ")
    '    Dim max As Integer = inDr.Length - 1
    '    Dim tripNo As String = String.Empty
    '    Dim updFlg As Boolean = True
    '    Dim anbunFlg As Boolean = True

    '    For i As Integer = 0 To max
    '        newDt.Clear()

    '        updFlg = False
    '        If String.IsNullOrEmpty(inDr(i).Item("TRIP_NO").ToString) = True Then
    '            '運行番号が空の場合
    '            updFlg = True
    '            anbunFlg = False
    '        ElseIf (tripNo).Equals(inDr(i).Item("TRIP_NO").ToString) = False Then
    '            '運行番号が変わった場合
    '            updFlg = True
    '            anbunFlg = True

    '            '運行番号の保存
    '            tripNo = inDr(i).Item("TRIP_NO").ToString
    '        End If

    '        If updFlg = True Then
    '            '同じ運行番号のデータを取得
    '            newDt.ImportRow(inDr(i))
    '            newDs = Me.DacAccess(newDs, LMF080BLC.ANBUN_DATA)

    '            '取得した按分対象データを元に"UNCHIN"テーブルに値を設定する
    '            newDs = Me.SetUnchinData(newDs, anbunFlg)
    '            newDs = Me.SetComSaveDataAction(newDs, False, String.Empty, False)
    '        End If

    '    Next

    '    Return ds

    'End Function
    ''' <summary>
    ''' 確定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetFixData(ByVal ds As DataSet) As DataSet

        Dim newDs As DataSet = New LMF080DS()
        Dim newDt As DataTable = newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN)
        Dim newDr As DataRow = newDt.NewRow()
#If False Then  'UPD 2019/03/26 依頼番号 : 005173   【LMS】バグ調査_大阪特定まとめ出荷番号の時、支払確定時タイムアウトエラー_テーブルロックになる
                Dim inDr() As DataRow = ds.Tables(LMF080BLC.TABLE_NM_UNCHIN).Select(String.Empty, " UNSO_NO_L, UNSO_NO_M ")

#Else
        Dim inDr() As DataRow = ds.Tables(LMF080BLC.TABLE_NM_UNCHIN).Select(String.Empty, "SHIHARAI_GROUP_NO, SHIHARAI_GROUP_NO_M,UNSO_NO_L, UNSO_NO_M ")

        Dim keySHIHARAI_GROUP_NO As String = String.Empty
        Dim keySHIHARAI_GROUP_NO_M As String = String.Empty
#End If
        Dim max As Integer = inDr.Length - 1
        Dim tripNo As String = String.Empty
        Dim updFlg As Boolean = True
        Dim anbunFlg As Boolean = True

        For i As Integer = 0 To max

#If False Then  'UPD 2019/03/26 依頼番号 : 005173   【LMS】バグ調査_大阪特定まとめ出荷番号の時、支払確定時タイムアウトエラー_テーブルロックになる
                        'ログ出力 確認用
            MyBase.Logger.StartLog(MyBase.GetType.Name, "☆☆☆☆　SetFixData " & i & " Start")

            newDt.Clear()

            '更新対象のデータを取得
            newDt.ImportRow(inDr(i))
            newDs = Me.DacAccess(newDs, LMF080BLC.ANBUN_DATA)


            'ログ出力 確認用
            MyBase.Logger.StartLog(MyBase.GetType.Name, "☆☆☆☆　SelectAnbunData　End")

            '取得した按分対象データを元に"UNCHIN"テーブルに値を設定する
            newDs = Me.SetUnchinData(newDs, anbunFlg)
            newDs = Me.SetComSaveDataAction(newDs, False, String.Empty, False)

            ''ログ出力 確認用
            'MyBase.Logger.StartLog(MyBase.GetType.Name, "☆☆☆☆　SetFixData " & i & " End")

#Else
            If String.Empty.Equals(inDr(i).Item("SHIHARAI_GROUP_NO").ToString) = False Then

                If keySHIHARAI_GROUP_NO.Equals(inDr(i).Item("SHIHARAI_GROUP_NO").ToString) = True _
                     AndAlso keySHIHARAI_GROUP_NO_M.Equals(inDr(i).Item("SHIHARAI_GROUP_NO_M").ToString) = True Then

                    Continue For
                Else
                    keySHIHARAI_GROUP_NO = inDr(i).Item("SHIHARAI_GROUP_NO").ToString
                    keySHIHARAI_GROUP_NO_M = inDr(i).Item("SHIHARAI_GROUP_NO_M").ToString

                End If
            Else
                keySHIHARAI_GROUP_NO = String.Empty
                keySHIHARAI_GROUP_NO_M = String.Empty
            End If

            ''ログ出力 確認用
            'MyBase.Logger.StartLog(MyBase.GetType.Name, "☆☆☆☆　SetFixData " & i & " Start")

            newDt.Clear()

            '更新対象のデータを取得
            newDt.ImportRow(inDr(i))
            newDs = Me.DacAccess(newDs, LMF080BLC.ANBUN_DATA)


            ''ログ出力 確認用
            'MyBase.Logger.StartLog(MyBase.GetType.Name, "☆☆☆☆　SelectAnbunData　End")

            '取得した按分対象データを元に"UNCHIN"テーブルに値を設定する
            newDs = Me.SetUnchinData(newDs, anbunFlg)
            newDs = Me.SetComSaveDataAction(newDs, False, String.Empty, False)

            ''ログ出力 確認用
            'MyBase.Logger.StartLog(MyBase.GetType.Name, "☆☆☆☆　SetFixData " & i & " End")

#End If
        Next

        Return ds

    End Function
    'END YANAI 要望番号1424 支払処理 按分しないようにする

    'START YANAI 要望番号1424 支払処理 按分しないようにする
    '''' <summary>
    '''' 確定解除処理
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks></remarks>
    'Private Function SetFixCancellData(ByVal ds As DataSet) As DataSet

    '    Dim newDs As DataSet = New LMF080DS()
    '    Dim newDt As DataTable = newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN)
    '    Dim newDr As DataRow = newDt.NewRow()

    '    Dim inDr() As DataRow = ds.Tables(LMF080BLC.TABLE_NM_UNCHIN).Select(String.Empty, " TRIP_NO, UNSO_NO_L, UNSO_NO_M ")
    '    Dim max As Integer = inDr.Length - 1
    '    Dim tripNo As String = String.Empty
    '    Dim updFlg As Boolean = True

    '    For i As Integer = 0 To max
    '        newDt.Clear()

    '        updFlg = False
    '        If String.IsNullOrEmpty(inDr(i).Item("TRIP_NO").ToString) = True Then
    '            '運行番号が空の場合
    '            updFlg = True

    '        ElseIf (tripNo).Equals(inDr(i).Item("TRIP_NO").ToString) = False Then
    '            '運行番号が変わった場合
    '            updFlg = True

    '            '運行番号の保存
    '            tripNo = inDr(i).Item("TRIP_NO").ToString
    '        End If

    '        If updFlg = True Then
    '            '同じ運行番号のデータを取得
    '            newDt.ImportRow(inDr(i))
    '            newDs = Me.DacAccess(newDs, LMF080BLC.ANBUN_DATA)

    '            '取得した按分対象データを元に"UNCHIN"テーブルに値を設定する
    '            newDs = Me.SetCancelUnchinData(newDs)
    '            newDs = Me.SetComSaveDataAction(newDs, False, LMF080BLC.FIX_CANCELL, False)
    '        End If

    '    Next

    '    Return ds

    'End Function
    ''' <summary>
    ''' 確定解除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetFixCancellData(ByVal ds As DataSet) As DataSet

        Dim newDs As DataSet = New LMF080DS()
        Dim newDt As DataTable = newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN)
        Dim newDr As DataRow = newDt.NewRow()

        Dim inDr() As DataRow = ds.Tables(LMF080BLC.TABLE_NM_UNCHIN).Select(String.Empty, " UNSO_NO_L, UNSO_NO_M ")
        Dim max As Integer = inDr.Length - 1
        Dim tripNo As String = String.Empty
        Dim updFlg As Boolean = True

        For i As Integer = 0 To max
            newDt.Clear()

            '同じ運行番号のデータを取得
            newDt.ImportRow(inDr(i))
            newDs = Me.DacAccess(newDs, LMF080BLC.ANBUN_DATA)

            '取得した按分対象データを元に"UNCHIN"テーブルに値を設定する
            newDs = Me.SetCancelUnchinData(newDs)
            newDs = Me.SetComSaveDataAction(newDs, False, LMF080BLC.FIX_CANCELL, False)
            
        Next

        Return ds

    End Function
    'END YANAI 要望番号1424 支払処理 按分しないようにする

    'START YANAI 要望番号1424 支払処理 按分しないようにする
    '''' <summary>
    '''' 按分処理
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks></remarks>
    'Private Function SetUnchinData(ByVal ds As DataSet, ByVal anbunFlg As Boolean) As DataSet

    '    Dim max As Integer = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows.Count - 1
    '    Dim sumDeciWt As Double = 0
    '    Dim sumDeciUnchin As Double = 0
    '    Dim sumDeciCity As Double = 0
    '    Dim sumDeciWint As Double = 0
    '    Dim sumDeciRely As Double = 0
    '    Dim sumDeciToll As Double = 0
    '    Dim sumDeciInsu As Double = 0

    '    Dim deciWt As Double = 0
    '    Dim deciUnchin As Double = 0
    '    Dim deciCity As Double = 0
    '    Dim deciWint As Double = 0
    '    Dim deciRely As Double = 0
    '    Dim deciToll As Double = 0
    '    Dim deciInsu As Double = 0

    '    Dim cntDeciUnchin As Double = 0
    '    Dim cntDeciCity As Double = 0
    '    Dim cntDeciWint As Double = 0
    '    Dim cntDeciRely As Double = 0
    '    Dim cntDeciToll As Double = 0
    '    Dim cntDeciInsu As Double = 0

    '    For i As Integer = 0 To max
    '        '各値の合計を求める
    '        sumDeciWt = sumDeciWt + Convert.ToDouble(ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SHIHARAI_WT").ToString) 'DECI_WTにはまとめ時、親に合計が設定され、子供はまとめ前の値が設定されているため、おかしくなってしまう
    '        sumDeciUnchin = sumDeciUnchin + Convert.ToDouble(ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_UNCHIN").ToString)
    '        sumDeciCity = sumDeciCity + Convert.ToDouble(ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_CITY_EXTC").ToString)
    '        sumDeciWint = sumDeciWint + Convert.ToDouble(ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_WINT_EXTC").ToString)
    '        sumDeciRely = sumDeciRely + Convert.ToDouble(ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_RELY_EXTC").ToString)
    '        sumDeciToll = sumDeciToll + Convert.ToDouble(ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_TOLL").ToString)
    '        sumDeciInsu = sumDeciInsu + Convert.ToDouble(ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_INSU").ToString)
    '    Next

    '    ''運行の支払金額(手入力金額)が設定されている場合
    '    'If Convert.ToDouble(ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(0).Item("SHIHARAI_UNCHIN").ToString) > 0 Then
    '    '    sumDeciUnchin = Convert.ToDouble(ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(0).Item("SHIHARAI_UNCHIN").ToString)
    '    '    sumDeciCity = 0
    '    '    sumDeciWint = 0
    '    '    sumDeciRely = 0
    '    '    sumDeciToll = 0
    '    '    sumDeciInsu = 0
    '    'End If

    '    Dim newDs As DataSet = New LMF080DS()
    '    Dim newDt As DataTable = newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN)
    '    Dim newDr As DataRow = newDt.NewRow()
    '    For i As Integer = 0 To max
    '        newDr = newDt.NewRow()

    '        newDr("NRS_BR_CD") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("NRS_BR_CD").ToString
    '        newDr("UNSO_NO_L") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("UNSO_NO_L").ToString
    '        newDr("UNSO_NO_M") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("UNSO_NO_M").ToString
    '        newDr("SHIHARAI_GROUP_NO") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SHIHARAI_GROUP_NO").ToString
    '        newDr("SHIHARAI_GROUP_NO_M") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SHIHARAI_GROUP_NO_M").ToString
    '        newDr("SHIHARAI_FIXED_FLAG") = "01"
    '        If anbunFlg = True Then
    '            newDr("ITEM_DATA") = "01A"
    '        Else
    '            newDr("ITEM_DATA") = "01A"
    '        End If
    '        newDr("SYS_UPD_DATE") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SYS_UPD_DATE").ToString
    '        newDr("SYS_UPD_TIME") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SYS_UPD_TIME").ToString
    '        newDr("ROW_NO") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("ROW_NO").ToString
    '        newDr("NEW_SYS_UPD_DATE") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("NEW_SYS_UPD_DATE").ToString
    '        newDr("NEW_SYS_UPD_TIME") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("NEW_SYS_UPD_TIME").ToString
    '        deciWt = Convert.ToDouble(ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SHIHARAI_WT").ToString) 'DECI_WTにはまとめ時、親に合計が設定され、子供はまとめ前の値が設定されているため、おかしくなってしまう

    '        If sumDeciUnchin * deciWt = 0 Then
    '            '0割り対策
    '            deciUnchin = 0
    '        Else
    '            deciUnchin = Me.ToHalfAdjust(sumDeciUnchin * deciWt / sumDeciWt, 2)
    '        End If

    '        If sumDeciCity * deciWt = 0 Then
    '            '0割り対策
    '            deciCity = 0
    '        Else
    '            deciCity = Me.ToHalfAdjust(sumDeciCity * deciWt / sumDeciWt, 2)
    '        End If

    '        If sumDeciWint * deciWt = 0 Then
    '            deciWint = 0
    '            '0割り対策
    '        Else
    '            deciWint = Me.ToHalfAdjust(sumDeciWint * deciWt / sumDeciWt, 2)
    '        End If

    '        If sumDeciRely * deciWt = 0 Then
    '            '0割り対策
    '            deciRely = 0
    '        Else
    '            deciRely = Me.ToHalfAdjust(sumDeciRely * deciWt / sumDeciWt, 2)
    '        End If

    '        If sumDeciToll * deciWt = 0 Then
    '            '0割り対策
    '            deciToll = 0
    '        Else
    '            deciToll = Me.ToHalfAdjust(sumDeciToll * deciWt / sumDeciWt, 2)
    '        End If

    '        If sumDeciInsu * deciWt = 0 Then
    '            '0割り対策
    '            deciInsu = 0
    '        Else
    '            deciInsu = Me.ToHalfAdjust(sumDeciInsu * deciWt / sumDeciWt, 2)
    '        End If

    '        newDr("KANRI_UNCHIN") = Convert.ToString(deciUnchin)
    '        newDr("KANRI_CITY_EXTC") = Convert.ToString(deciCity)
    '        newDr("KANRI_WINT_EXTC") = Convert.ToString(deciWint)
    '        newDr("KANRI_RELY_EXTC") = Convert.ToString(deciRely)
    '        newDr("KANRI_TOLL") = Convert.ToString(deciToll)
    '        newDr("KANRI_INSU") = Convert.ToString(deciInsu)

    '        '行追加
    '        newDt.Rows.Add(newDr)

    '        cntDeciUnchin = cntDeciUnchin + deciUnchin
    '        cntDeciCity = cntDeciCity + deciCity
    '        cntDeciWint = cntDeciWint + deciWint
    '        cntDeciRely = cntDeciRely + deciRely
    '        cntDeciToll = cntDeciToll + deciToll
    '        cntDeciInsu = cntDeciInsu + deciInsu

    '    Next

    '    '残額調整
    '    newDt.Rows(0).Item("KANRI_UNCHIN") = Convert.ToString(Convert.ToDouble(newDt.Rows(0).Item("KANRI_UNCHIN").ToString) + sumDeciUnchin - cntDeciUnchin)
    '    newDt.Rows(0).Item("KANRI_CITY_EXTC") = Convert.ToString(Convert.ToDouble(newDt.Rows(0).Item("KANRI_CITY_EXTC").ToString) + sumDeciCity - cntDeciCity)
    '    newDt.Rows(0).Item("KANRI_WINT_EXTC") = Convert.ToString(Convert.ToDouble(newDt.Rows(0).Item("KANRI_WINT_EXTC").ToString) + sumDeciWint - cntDeciWint)
    '    newDt.Rows(0).Item("KANRI_RELY_EXTC") = Convert.ToString(Convert.ToDouble(newDt.Rows(0).Item("KANRI_RELY_EXTC").ToString) + sumDeciRely - cntDeciRely)
    '    newDt.Rows(0).Item("KANRI_TOLL") = Convert.ToString(Convert.ToDouble(newDt.Rows(0).Item("KANRI_TOLL").ToString) + sumDeciToll - cntDeciToll)
    '    newDt.Rows(0).Item("KANRI_INSU") = Convert.ToString(Convert.ToDouble(newDt.Rows(0).Item("KANRI_INSU").ToString) + sumDeciInsu - cntDeciInsu)

    '    'まとめデータの場合、親に子の金額を加算。子は0を設定。
    '    Dim grpDr() As DataRow = Nothing
    '    max = newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows.Count - 1
    '    For i As Integer = 0 To max
    '        If String.IsNullOrEmpty(newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("SHIHARAI_GROUP_NO").ToString) = False AndAlso _
    '            ((newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("UNSO_NO_L").ToString).Equals(newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("SHIHARAI_GROUP_NO").ToString) = False OrElse _
    '             (newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("UNSO_NO_M").ToString).Equals(newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("SHIHARAI_GROUP_NO_M").ToString) = False) Then
    '            '親データを探して、加算する
    '            grpDr = newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Select(String.Concat("UNSO_NO_L = '", newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("SHIHARAI_GROUP_NO").ToString, "' AND ", _
    '                                                                                 "UNSO_NO_M = '", newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("SHIHARAI_GROUP_NO_M").ToString, "'"))
    '            If grpDr.Length > 0 Then
    '                grpDr(0).Item("KANRI_UNCHIN") = Convert.ToString(Convert.ToDouble(grpDr(0).Item("KANRI_UNCHIN").ToString) + Convert.ToDouble(newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("KANRI_UNCHIN").ToString))
    '                grpDr(0).Item("KANRI_CITY_EXTC") = Convert.ToString(Convert.ToDouble(grpDr(0).Item("KANRI_CITY_EXTC").ToString) + Convert.ToDouble(newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("KANRI_CITY_EXTC").ToString))
    '                grpDr(0).Item("KANRI_WINT_EXTC") = Convert.ToString(Convert.ToDouble(grpDr(0).Item("KANRI_WINT_EXTC").ToString) + Convert.ToDouble(newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("KANRI_WINT_EXTC").ToString))
    '                grpDr(0).Item("KANRI_RELY_EXTC") = Convert.ToString(Convert.ToDouble(grpDr(0).Item("KANRI_RELY_EXTC").ToString) + Convert.ToDouble(newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("KANRI_RELY_EXTC").ToString))
    '                grpDr(0).Item("KANRI_TOLL") = Convert.ToString(Convert.ToDouble(grpDr(0).Item("KANRI_TOLL").ToString) + Convert.ToDouble(newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("KANRI_TOLL").ToString))
    '                grpDr(0).Item("KANRI_INSU") = Convert.ToString(Convert.ToDouble(grpDr(0).Item("KANRI_INSU").ToString) + Convert.ToDouble(newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("KANRI_INSU").ToString))

    '                newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("KANRI_UNCHIN") = "0"
    '                newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("KANRI_CITY_EXTC") = "0"
    '                newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("KANRI_WINT_EXTC") = "0"
    '                newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("KANRI_RELY_EXTC") = "0"
    '                newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("KANRI_TOLL") = "0"
    '                newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(i).Item("KANRI_INSU") = "0"
    '            End If

    '        End If
    '    Next

    '    Return newDs

    'End Function
    ''' <summary>
    ''' 按分処理☆☆確定時の按分処理は必要ないということなので、実際は按分してません
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetUnchinData(ByVal ds As DataSet, ByVal anbunFlg As Boolean) As DataSet

        Dim max As Integer = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows.Count - 1

        Dim newDs As DataSet = New LMF080DS()
        Dim newDt As DataTable = newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN)
        Dim newDr As DataRow = newDt.NewRow()
        For i As Integer = 0 To max
            newDr = newDt.NewRow()

            newDr("NRS_BR_CD") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("NRS_BR_CD").ToString
            newDr("UNSO_NO_L") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("UNSO_NO_L").ToString
            newDr("UNSO_NO_M") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("UNSO_NO_M").ToString
            newDr("SHIHARAI_GROUP_NO") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SHIHARAI_GROUP_NO").ToString
            newDr("SHIHARAI_GROUP_NO_M") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SHIHARAI_GROUP_NO_M").ToString
            newDr("SHIHARAI_FIXED_FLAG") = "01"
            If anbunFlg = True Then
                newDr("ITEM_DATA") = "01A"
            Else
                newDr("ITEM_DATA") = "01A"
            End If
            newDr("SYS_UPD_DATE") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SYS_UPD_DATE").ToString
            newDr("SYS_UPD_TIME") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SYS_UPD_TIME").ToString
            newDr("ROW_NO") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("ROW_NO").ToString
            newDr("NEW_SYS_UPD_DATE") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("NEW_SYS_UPD_DATE").ToString
            newDr("NEW_SYS_UPD_TIME") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("NEW_SYS_UPD_TIME").ToString

            newDr("KANRI_UNCHIN") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_UNCHIN").ToString
            newDr("KANRI_CITY_EXTC") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_CITY_EXTC").ToString
            newDr("KANRI_WINT_EXTC") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_WINT_EXTC").ToString
            newDr("KANRI_RELY_EXTC") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_RELY_EXTC").ToString
            newDr("KANRI_TOLL") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_TOLL").ToString
            newDr("KANRI_INSU") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("DECI_INSU").ToString

            '行追加
            newDt.Rows.Add(newDr)

        Next

        Return newDs

    End Function
    'END YANAI 要望番号1424 支払処理 按分しないようにする

    ''' <summary>
    ''' 確定キャンセル処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetCancelUnchinData(ByVal ds As DataSet) As DataSet

        Dim max As Integer = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows.Count - 1

        Dim newDs As DataSet = New LMF080DS()
        Dim newDt As DataTable = newDs.Tables(LMF080BLC.TABLE_NM_UNCHIN)
        Dim newDr As DataRow = newDt.NewRow()
        For i As Integer = 0 To max
            newDr = newDt.NewRow()

            newDr("NRS_BR_CD") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("NRS_BR_CD").ToString
            newDr("UNSO_NO_L") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("UNSO_NO_L").ToString
            newDr("UNSO_NO_M") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("UNSO_NO_M").ToString
            newDr("SHIHARAI_FIXED_FLAG") = "00"
            newDr("ITEM_DATA") = "01"
            newDr("SYS_UPD_DATE") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SYS_UPD_DATE").ToString
            newDr("SYS_UPD_TIME") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("SYS_UPD_TIME").ToString
            newDr("ROW_NO") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("ROW_NO").ToString
            newDr("NEW_SYS_UPD_DATE") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("NEW_SYS_UPD_DATE").ToString
            newDr("NEW_SYS_UPD_TIME") = ds.Tables(LMF080BLC.TABLE_NM_OUT_ANBUN).Rows(i).Item("NEW_SYS_UPD_TIME").ToString

            '行追加
            newDt.Rows.Add(newDr)

        Next

        Return newDs

    End Function

#End Region

#Region "まとめ"

    ''' <summary>
    ''' まとめ指示
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetGroupData(ByVal ds As DataSet) As DataSet

        Return Me.SetComSaveDataAction(ds, True, LMF080BLC.GROUP, True)

    End Function

    ''' <summary>
    ''' まとめ解除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetGroupCancellData(ByVal ds As DataSet) As DataSet

        Return Me.SetComSaveDataAction(ds, True, LMF080BLC.GROUP_CANCELL, False)

    End Function

#End Region

#Region "一括変更"

    ''' <summary>
    ''' 一括変更(通常)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveUnchinItemData(ByVal ds As DataSet) As DataSet

        Return Me.SetComSaveDataAction(ds, True, LMF080BLC.IKKATU, False)

    End Function

    ''' <summary>
    ''' 一括変更(支払先)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveUnchinSeiqtoItemData(ByVal ds As DataSet) As DataSet

        Return Me.SetComSaveDataAction(ds, True, LMF080BLC.IKKATU, False, ds.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(0).Item("CD_L").ToString())

    End Function

    ''' <summary>
    ''' 一括変更(タリフ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveUnchinTariffData(ByVal ds As DataSet) As DataSet

        '更新処理
        Return Me.SetComSaveDataAction(ds, True, LMF080BLC.IKKATU, False)

    End Function

#End Region

#Region "運賃更新処理(再計算時)"

    ''' <summary>
    ''' 運賃更新処理(再計算時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdUnchinData(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing
        Dim drs As DataRow() = ds.Tables(LMF080BLC.TABLE_NM_UNCHIN).Select(String.Empty, " UNSO_NO_L , UNSO_NO_M ")

        ''最終請求日のチェック
        'If Me.ChkSeiqDate(ds, drs(0), String.Empty, String.Empty, False) = False Then
        '    Return Me.SetConnectionRecordErr(ds, drs(0).Item("ROW_NO").ToString())
        'End If

        rtnDs = MyBase.CallDAC(Me._Dac, "SelectDataSaikeisan", ds)
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("E011")
            MyBase.SetMessageStore(LMF080BLC.GUIDANCE_KBN, "E011", , Convert.ToInt32(ds.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(0).Item("ROW_NO")).ToString())
            Return rtnDs
        End If

        '更新
        rtnDs = MyBase.CallDAC(Me._Dac, "UpdUnchinData", ds)

        Return rtnDs

    End Function

#End Region

#Region "共通"

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="updateFlg">運送(大)の更新フラグ　True：更新する</param>
    ''' <param name="chkErrFlg">チェックフラグ　True:通常のエラーチェック　False:一括更新のエラーチェック</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet, ByVal updateFlg As Boolean, ByVal chkErrFlg As Boolean) As Boolean

        '運送更新フラグ
        If updateFlg = True _
            AndAlso Me.UpdateDataAction(ds, LMF080BLC.ACTION_ID_UPDATE_UNSO, chkErrFlg) = False _
            Then

            Return False
        End If

        '運賃の更新
        Return Me.UpdateDataAction(ds, LMF080BLC.ACTION_ID_UPDATE_UNCHIN, chkErrFlg)

    End Function

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <param name="chkErrFlg">チェックフラグ　True:通常のエラーチェック　False:一括更新のエラーチェック</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateDataAction(ByVal ds As DataSet, ByVal actionId As String, ByVal chkErrFlg As Boolean) As Boolean

        If chkErrFlg = True Then
            Return Me.ServerChkJudge(ds, actionId)
        Else
            Return Me.ServerChkJudgeStore(ds, actionId)
        End If

    End Function

    ''' <summary>
    ''' 更新共通
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="chkFlg">最終請求日のチェックフラグ　True：チェックを行う</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="chkErrFlg">チェックフラグ　True:通常のエラーチェック　False:一括更新のエラーチェック</param>
    ''' <param name="seiqtoCd">請求先コード(一括変更_請求先のみ使用)　初期値 = ''</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetComSaveDataAction(ByVal ds As DataSet _
                                          , ByVal chkFlg As Boolean _
                                          , ByVal msg As String _
                                          , ByVal chkErrFlg As Boolean _
                                          , Optional ByVal seiqtoCd As String = "" _
                                          ) As DataSet

        '更新用のDataSet
        Dim upDs As DataSet = ds.Copy
        Dim upDt As DataTable = upDs.Tables(LMF080BLC.TABLE_NM_UNCHIN)

        Dim drs As DataRow() = ds.Tables(LMF080BLC.TABLE_NM_UNCHIN).Select(String.Empty, " UNSO_NO_L , UNSO_NO_M ")
        Dim max As Integer = drs.Length - 1
        Dim unsoL As String = String.Empty
        Dim chkUnsoL As String = String.Empty
        Dim updateFlg As Boolean = False
        For i As Integer = 0 To max

            ''最終請求日のチェック
            'If chkFlg = True AndAlso Me.ChkSeiqDate(ds, drs(i), msg, seiqtoCd, chkErrFlg) = False Then
            '    Return Me.SetConnectionRecordErr(ds, drs(i).Item("ROW_NO").ToString())
            'End If

            '値のクリア
            upDt.Clear()
            updateFlg = False

            '更新レコードを設定
            upDt.ImportRow(drs(i))

            '運送番号(大)を設定
            chkUnsoL = upDt.Rows(0).Item("UNSO_NO_L").ToString()

            '前回の運送番号(大)と違う場合、運送(大)テーブルを更新
            If unsoL.Equals(chkUnsoL) = False Then

                unsoL = chkUnsoL
                updateFlg = True

            End If

            '更新処理
            If Me.UpdateData(upDs, updateFlg, chkErrFlg) = False Then
                Return Me.SetConnectionRecordErr(ds, drs(i).Item("ROW_NO").ToString())
            End If

        Next

        Return ds

    End Function

#End Region

#End Region

#Region "チェック"

    '''' <summary>
    '''' 最終請求日のチェック
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <param name="dr">DataRow</param>
    '''' <param name="seiqtoCd">請求先コード(一括更新_請求先のみ使用)</param>
    '''' <param name="msg">置換文字</param>
    '''' <param name="chkErrFlg">チェックフラグ　True:通常のエラーチェック　False:一括更新のエラーチェック</param>
    '''' <returns>最終請求日</returns>
    '''' <remarks></remarks>
    'Private Function ChkSeiqDate(ByVal ds As DataSet, ByVal dr As DataRow _
    '                             , ByVal msg As String _
    '                             , ByVal seiqtoCd As String _
    '                             , ByVal chkErrFlg As Boolean _
    '                             ) As Boolean

    '    Dim inDate As String = dr.Item("ARR_PLAN_DATE").ToString()
    '    Dim outDate As String = dr.Item("OUTKA_PLAN_DATE").ToString()
    '    Dim rowNo As String = dr.Item("ROW_NO").ToString()

    '    '納入日、出荷日の両方に値がない場合、スルー
    '    If String.IsNullOrEmpty(inDate) = True _
    '        AndAlso String.IsNullOrEmpty(outDate) Then
    '        Return True
    '    End If

    '    '元データ区分
    '    Dim moto As String = dr.Item("MOTO_DATA_KB").ToString()

    '    Dim selectDs As DataSet = ds.Copy
    '    Dim selectDt As DataTable = selectDs.Tables(LMF080BLC.TABLE_NM_UNCHIN)

    '    '請求日を取得用の情報を設定
    '    selectDt.Clear()
    '    selectDt.ImportRow(dr)

    '    '締め処理済の場合、スルー
    '    Dim rtnResult As Boolean = Me.ChkSeiqDate(ds, inDate, outDate, dr.Item("UNTIN_CALCULATION_KB").ToString(), Me.SelectGheaderData(selectDs), moto, msg, rowNo, chkErrFlg)

    '    '請求先コードに値がある場合、2回目のチェック
    '    If String.IsNullOrEmpty(seiqtoCd) = False Then

    '        '請求先コードを変更してチェック
    '        selectDt.Rows(selectDt.Rows.Count - 1).Item("SEIQTO_CD") = seiqtoCd
    '        rtnResult = rtnResult AndAlso Me.ChkSeiqDate(ds, inDate, outDate, dr.Item("UNTIN_CALCULATION_KB").ToString(), Me.SelectGheaderData(selectDs), moto, msg, rowNo, chkErrFlg)

    '    End If

    '    Return rtnResult

    'End Function

    '''' <summary>
    '''' 最終請求日のチェック
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <param name="inDate">納入日</param>
    '''' <param name="outDate">出荷日</param>
    '''' <param name="calcKbn">締め基準</param>
    '''' <param name="chkDate">請求日</param>
    '''' <param name="moto">元データ区分</param>
    '''' <param name="msg">置換文字</param>
    '''' <param name="chkErrFlg">チェックフラグ　True:通常のエラーチェック　False:一括更新のエラーチェック</param>
    '''' <returns>True:エラーなし,OK False:エラーあり</returns>
    '''' <remarks></remarks>
    'Private Function ChkSeiqDate(ByVal ds As DataSet _
    '                                 , ByVal inDate As String _
    '                                 , ByVal outDate As String _
    '                                 , ByVal calcKbn As String _
    '                                 , ByVal chkDate As String _
    '                                 , ByVal moto As String _
    '                                 , ByVal msg As String _
    '                                 , ByVal rowNo As String _
    '                                 , ByVal chkErrFlg As Boolean _
    '                                 ) As Boolean

    '    Select Case moto

    '        Case LMF080BLC.MOTO_DATA_NYUKA

    '            '元データ = 入荷は納入日とチェック
    '            Return Me.ChkDate(ds, inDate, chkDate, msg, rowNo, chkErrFlg)

    '        Case Else

    '            Select Case calcKbn

    '                Case LMF080BLC.CALC_SHUKKA

    '                    '運賃計算締め基準によるチェック(出荷日)
    '                    Return Me.ChkDate(ds, outDate, chkDate, msg, rowNo, chkErrFlg)

    '                Case LMF080BLC.CALC_NYUKA

    '                    '運賃計算締め基準によるチェック(納入日)
    '                    Return Me.ChkDate(ds, inDate, chkDate, msg, rowNo, chkErrFlg)

    '            End Select

    '    End Select

    '    Return True

    'End Function

    '''' <summary>
    '''' 日付チェック
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <param name="value1">比較対象日</param>
    '''' <param name="value2">最終締め日</param>
    '''' <param name="msg">置換文字</param>
    '''' <param name="rowNo">スプレッドの行番号</param>
    '''' <param name="chkErrFlg">チェックフラグ　True:通常のエラーチェック　False:一括更新のエラーチェック</param>
    '''' <returns>True:エラーなし,OK False:エラーあり</returns>
    '''' <remarks></remarks>
    'Private Function ChkDate(ByVal ds As DataSet _
    '                         , ByVal value1 As String _
    '                         , ByVal value2 As String _
    '                         , ByVal msg As String _
    '                         , ByVal rowNo As String _
    '                         , ByVal chkErrFlg As Boolean _
    '                         ) As Boolean

    '    '比較対象1に値がない場合、スルー
    '    If String.IsNullOrEmpty(value1) = True Then
    '        Return True
    '    End If

    '    'すでに締め処理が締め処理が終了している場合、エラー
    '    If value1 <= value2 Then

    '        If chkErrFlg = True Then
    '            MyBase.SetMessage("E232", New String() {LMF080BLC.TORIKOMI, msg})
    '        Else
    '            MyBase.SetMessageStore(LMF080BLC.GUIDANCE_KBN, "E232", New String() {LMF080BLC.TORIKOMI, msg}, rowNo)
    '        End If
    '        Return False

    '    End If

    '    Return True

    'End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

    End Function

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudgeStore(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageStoreExist( Convert.ToInt32(ds.Tables(LMF080BLC.TABLE_NM_UNCHIN).Rows(0).Item("ROW_NO").ToString()))

    End Function

    ''' <summary>
    ''' DACクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DacAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallDAC(Me._Dac, actionId, ds)

    End Function

    ''' <summary>
    ''' 不要レコードの削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteNotGroupData(ByVal ds As DataSet) As DataSet

        Dim drs As DataRow() = ds.Tables(LMF080BLC.TABLE_NM_OUT).Select(String.Concat("GROUP_FLG = '0'"))
        Dim dr As DataRow = Nothing
        Dim max As Integer = drs.Length - 1
        For i As Integer = max To 0 Step -1

            drs(i).Delete()

        Next

        Return ds

    End Function

    ''' <summary>
    ''' まとめ条件の日付を取得
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>運送日</returns>
    ''' <remarks></remarks>
    Private Function GetUnsoDate(ByVal dr As DataRow) As String

        GetUnsoDate = String.Empty

        GetUnsoDate = dr.Item("ARR_PLAN_DATE").ToString()

        Return GetUnsoDate

    End Function

    ''' <summary>
    ''' 運行番号を取得
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>運行番号</returns>
    ''' <remarks>
    ''' </remarks>
    Private Function GetTripNo(ByVal dr As DataRow) As String()

        GetTripNo = Nothing

        Return New String() {dr.Item("TRIP_NO").ToString()}

    End Function

    ''' <summary>
    ''' 値があるかを判定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsValueChk(ByVal value As String()) As Boolean

        Dim max As Integer = value.Count - 1
        For i As Integer = max To 0

            If String.IsNullOrEmpty(value(i)) = False Then
                Return True
            End If

        Next

        Return False

    End Function

    ''' <summary>
    ''' 運行番号の判定
    ''' </summary>
    ''' <param name="value1">値1</param>
    ''' <param name="value2">値2</param>
    ''' <returns>値が同じ場合、True　全て違う場合、False</returns>
    ''' <remarks></remarks>
    Private Function ChkTripNo(ByVal value1 As String(), ByVal value2 As String()) As Boolean

        Dim max1 As Integer = value1.Count - 1
        Dim max2 As Integer = value2.Count - 1

        Return value1(max1).Equals(value2(max2))
        
        Return False

    End Function

    ''' <summary>
    ''' 検索処理のエラーメッセージを設定
    ''' </summary>
    ''' <param name="count">件数</param>
    ''' <remarks></remarks>
    Private Sub SetSelectErrMes(ByVal count As Integer)

        'メッセージコードの設定
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

    End Sub

    ''' <summary>
    ''' 紐付いているレコードをエラー帳票に設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="rowNo">[画面] スプレッドのエラー行番号</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetConnectionRecordErr(ByVal ds As DataSet, ByVal rowNo As String) As DataSet

        'エラー行は除く
        Dim drs As DataRow() = ds.Tables(LMF080BLC.TABLE_NM_UNCHIN).Select(String.Concat("ROW_NO <> '", rowNo, "' "), " ROW_NO ")
        Dim max As Integer = drs.Length - 1
        Dim chkData As String = String.Empty
        Dim rowData As String = String.Empty
        Dim errMsg As String() = New String() {Convert.ToInt32(rowNo).ToString()}
        For i As Integer = 0 To max

            '行番号を設定
            chkData = Convert.ToInt32(drs(i).Item("ROW_NO")).ToString()

            '前回の行番号と同じ場合、スルー
            If rowData.Equals(chkData) = True Then
                Continue For
            End If

            '判定用の値を保持
            rowData = chkData

            MyBase.SetMessageStore(LMF080BLC.GUIDANCE_KBN, "E380", errMsg, chkData)

        Next

        Return ds

    End Function

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

#End Region

End Class
