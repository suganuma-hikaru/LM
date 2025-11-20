' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブ
'  プログラムID     :  LMF040BLF : 運送検索
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMF040BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF040BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMF040BLC = New LMF040BLC()

#End Region

#Region "Const"

    ''' <summary>
    ''' 運賃検索対象データ件数検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_COUNT As String = "SelectData"

    ''' <summary>
    ''' 運賃テーブル検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_UNCHIN As String = "SelectUnchinData"

    ''' <summary>
    ''' 運賃テーブル検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_INIT_UNCHIN As String = "SelectInitUnchinData"

    ''' <summary>
    ''' 運賃計算プログラム
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UNCHINI_CALC As String = "CalcUnchin"

    ''' <summary>
    ''' 運賃テーブル検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SAVE_SEIQTO As String = "SaveUnchinSeiqtoItemData"

    ''' <summary>
    ''' LMF800INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_800IN As String = "UNCHIN_CALC_IN"

    ''' <summary>
    ''' LMF800OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_800OUT As String = "UNCHIN_CALC_OUT"

    ''' <summary>
    ''' UNCHINテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNCHIN As String = "UNCHIN"

    ''' <summary>
    ''' 修正項目(請求先)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_SEIQTO As String = "05"

    ''' <summary>
    ''' 修正項目(タリフ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_TARIFF As String = "06"

    ''' <summary>
    ''' 修正項目(横持ち)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_YOKO As String = "07"

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
    ''' アクションフラグ(業務)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACT_FGL_GYOM As String = "00"

    ''' <summary>
    ''' アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum ActionType As Integer

        NOMAL = 0
        GROUP_CANCELL
        IKKATU_TARIFF

    End Enum

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 運送(大)検索対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = Me.BlcAccess(ds, LMF040BLF.ACTION_ID_COUNT)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '検索結果取得
        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 運送(大)検索対象データ検索(まとめ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListGroupData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 運賃タリフの承認チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectChkApproval(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

#End Region

#Region "設定処理"

#Region "確定、確定解除"

    ''' <summary>
    ''' 確定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetFixData(ByVal ds As DataSet) As DataSet

        Return Me.SetComSaveData(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 確定解除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetFixCancellData(ByVal ds As DataSet) As DataSet

        Return Me.SetComSaveData(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

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

        Dim dt As DataTable = ds.Tables(LMF040BLF.TABLE_NM_UNCHIN)
        Dim drs As DataRow() = dt.Select(String.Empty, " UNSO_NO_L , UNSO_NO_M ")
        Dim dr As DataRow = Nothing
        Dim setDr As DataRow = Nothing
        Dim rowMax As Integer = drs.Length - 1
        Dim colMax As Integer = dt.Columns.Count - 1
        Dim soJuryo As Decimal = 0
        Dim maxKyori As Decimal = 0
        Dim juryo As Decimal = 0
        Dim kyori As Decimal = 0
        Dim oyaDr As DataRow = drs(0)
        Dim unsoL As String = oyaDr.Item("UNSO_NO_L").ToString()
        Dim unsoM As String = oyaDr.Item("UNSO_NO_M").ToString()
        Dim remark As String = String.Empty
        'START YANAI 要望番号583
        Dim soKosu As Decimal = 0
        'END YANAI 要望番号583
        Dim hokenryo As Decimal = 0     'ADD 2018/10/23 依頼番号 : 002400   【LMS】運送保険_設定商品を出荷時、運送の保険料欄に保険料を自動入力させる

        Dim KenFlag As Boolean = False
        Dim KenKyori As String = String.Empty
#If True Then   'ADD 2020/07/20 013381   【LMS】日産物流の場合運賃請求明細にまとめた行を表示する
        Dim sMATOME_REMARK_UPNG_FLG As String = ds.Tables(LMF040BLF.TABLE_NM_UNCHIN).Rows(0).Item("MATOME_REMARK_UPNG_FLG").ToString

#End If
        '検索用
        Dim selectDs As DataSet = ds.Copy
        Dim selectDt As DataTable = selectDs.Tables(LMF040BLF.TABLE_NM_UNCHIN)

        For i As Integer = 0 To rowMax

            '値のクリア
            selectDt.Clear()

            '値の設定
            setDr = drs(i)
            selectDt.ImportRow(drs(i))

            '値取得
            selectDs = Me.BlcAccess(selectDs, LMF040BLF.ACTION_ID_SELECT_UNCHIN)

            '取得できない場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If
            dr = selectDs.Tables(LMF040BLF.TABLE_NM_UNCHIN).Rows(0)

            With dr

                '総重量を設定
                soJuryo += Convert.ToDecimal(.Item("DECI_WT").ToString())

#If True Then       'ADD 2018/10/23 依頼番号 : 002400   【LMS】運送保険_設定商品を出荷時、運送の保険料欄に保険料を自動入力させる

                '保険料設定
                hokenryo += Convert.ToDecimal(.Item("DECI_INSU").ToString())
#End If
                '距離の最大を設定
                kyori = Convert.ToDecimal(.Item("DECI_KYORI").ToString())
                If maxKyori < kyori Then
                    maxKyori = kyori
                End If

                '備考は元の値
                .Item("REMARK") = setDr.Item("REMARK").ToString()

                '100バイトより小さい場合、文字連結
                If Me.GetByteCount(remark) < 100 Then

                    '備考の設定
                    remark = Me.EditConcatData(remark, .Item("REMARK").ToString())

                    'オーバーした分を削除
                    remark = Me.CutByteValue(remark, 100)

                End If

                'START YANAI 要望番号583
                '総個数を設定
                soKosu += Convert.ToDecimal(.Item("DECI_NG_NB").ToString())
                'END YANAI 要望番号583

                '確定金額にゼロを設定
                .Item("DECI_UNCHIN") = 0
                .Item("DECI_CITY_EXTC") = 0
                .Item("DECI_WINT_EXTC") = 0
                .Item("DECI_RELY_EXTC") = 0
                .Item("DECI_TOLL") = 0
                .Item("DECI_INSU") = 0

                'まとめ番号の設定
                .Item("SEIQ_GROUP_NO") = unsoL
                .Item("SEIQ_GROUP_NO_M") = unsoM

                'タリフ分類確認
                If "05".Equals(.Item("TABLE_TP").ToString()) OrElse _
                   "06".Equals(.Item("TABLE_TP").ToString()) OrElse _
                   "07".Equals(.Item("TABLE_TP").ToString()) Then
                    If String.IsNullOrEmpty(.Item("DEST_JIS").ToString()) = False Then
                        KenKyori = Mid(.Item("DEST_JIS").ToString(), 1, 2)
                        KenFlag = True
                    End If
                End If

            End With

            '値の反映
            For j As Integer = 0 To colMax
                setDr.Item(j) = dr.Item(j).ToString()
            Next
#If True Then   'ADD 2020/07/20 013381   【LMS】日産物流の場合運賃請求明細にまとめた行を表示する
            setDr.Item("MATOME_REMARK_UPNG_FLG") = sMATOME_REMARK_UPNG_FLG.ToString

#End If
        Next

        '親レコードの情報を設定
        oyaDr.Item("DECI_WT") = soJuryo
        oyaDr.Item("DECI_KYORI") = maxKyori
        oyaDr.Item("REMARK") = remark
        'START YANAI 要望番号583
        oyaDr.Item("DECI_NG_NB") = soKosu
        'END YANAI 要望番号583
        oyaDr.Item("DECI_INSU") = hokenryo      'ADD 2018/10/23 依頼番号 : 002400   【LMS】運送保険_設定商品を出荷時、運送の保険料欄に保険料を自動入力させる

        '親レコードの計算した運賃を設定
        ds = Me.SetUnchinData(ds, oyaDr, New LMF800DS(), New LMF800BLC(), oyaDr.Item("SEIQ_TARIFF_CD").ToString(), KenFlag, KenKyori)

        '按分処理
        ds = Me.SetUncinData(ds)

        '更新処理
        ds = Me.ScopeStartEnd(ds, System.Reflection.MethodBase.GetCurrentMethod.Name, True)

        Return ds

    End Function

    'START YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない
    ''' <summary>
    ''' まとめ指示(日立物流用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetGroupDataDic(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMF040BLF.TABLE_NM_UNCHIN)
        'START YANAI 要望番号1278 運賃まとめを、Spreadの並び替え後実行すると変になる
        'Dim drs As DataRow() = dt.Select(String.Empty, " SEIQ_GROUP_NO , SEIQ_GROUP_NO_M ")
        Dim drs As DataRow() = dt.Select(String.Empty, " SEIQ_GROUP_NO , SEIQ_GROUP_NO_M , UNSO_NO_L , UNSO_NO_M ")
        'END YANAI 要望番号1278 運賃まとめを、Spreadの並び替え後実行すると変になる
        Dim dr As DataRow = Nothing
        Dim setDr As DataRow = Nothing
        Dim rowMax As Integer = drs.Length - 1
        Dim colMax As Integer = dt.Columns.Count - 1
        Dim soJuryo As Decimal = 0
        Dim maxKyori As Decimal = 0
        Dim juryo As Decimal = 0
        Dim kyori As Decimal = 0
        Dim remark As String = String.Empty
        Dim soKosu As Decimal = 0
        Dim seiqGroupNo As String = String.Empty
        Dim seiqGroupNoM As String = String.Empty
        Dim oyaRowNo As Integer = 0
        Dim rtnDs As DataSet = Nothing

        '検索用
        Dim selectDs As DataSet = ds.Copy
        Dim selectDt As DataTable = selectDs.Tables(LMF040BLF.TABLE_NM_UNCHIN)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            For i As Integer = 0 To rowMax

                If (seiqGroupNo).Equals(drs(i).Item("SEIQ_GROUP_NO").ToString) = False OrElse _
                    (seiqGroupNoM).Equals(drs(i).Item("SEIQ_GROUP_NO_M").ToString) = False Then
                    'まとめ番号が変わった時

                    If String.IsNullOrEmpty(seiqGroupNo) = False Then
                        '親レコードの情報を設定
                        drs(oyaRowNo).Item("DECI_WT") = soJuryo
                        drs(oyaRowNo).Item("DECI_KYORI") = maxKyori
                        drs(oyaRowNo).Item("REMARK") = remark
                        drs(oyaRowNo).Item("DECI_NG_NB") = soKosu

                        '親レコードの計算した運賃を設定
                        ds = Me.SetUnchinData(ds, drs(oyaRowNo), New LMF800DS(), New LMF800BLC(), drs(oyaRowNo).Item("SEIQ_TARIFF_CD").ToString())

                        '按分処理
                        ds = Me.SetUncinDataDic(ds, drs(oyaRowNo))

                    End If

                    '各種値のクリア
                    seiqGroupNo = drs(i).Item("SEIQ_GROUP_NO").ToString
                    seiqGroupNoM = drs(i).Item("SEIQ_GROUP_NO_M").ToString
                    oyaRowNo = i
                    soJuryo = 0
                    kyori = 0
                    maxKyori = 0
                    remark = String.Empty
                    soKosu = 0

                End If

                '値のクリア
                selectDt.Clear()

                'ここでグループ番号を空にしないと、次の値取得時に値が取得できない
                drs(i).Item("SEIQ_GROUP_NO") = String.Empty
                drs(i).Item("SEIQ_GROUP_NO_M") = String.Empty

                '値の設定
                setDr = drs(i)
                selectDt.ImportRow(drs(i))

                '値取得
                selectDs = Me.BlcAccess(selectDs, LMF040BLF.ACTION_ID_SELECT_UNCHIN)

                '取得できない場合、終了
                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If
                dr = selectDs.Tables(LMF040BLF.TABLE_NM_UNCHIN).Rows(0)

                'まとめ番号を設定
                dr.Item("SEIQ_GROUP_NO") = seiqGroupNo
                dr.Item("SEIQ_GROUP_NO_M") = seiqGroupNoM

                '総重量を設定
                soJuryo += Convert.ToDecimal(dr.Item("DECI_WT").ToString())

                '距離の最大を設定
                kyori = Convert.ToDecimal(dr.Item("DECI_KYORI").ToString())
                If maxKyori < kyori Then
                    maxKyori = kyori
                End If

                '備考は元の値
                dr.Item("REMARK") = setDr.Item("REMARK").ToString()

                '100バイトより小さい場合、文字連結
                If Me.GetByteCount(remark) < 100 Then

                    '備考の設定
                    remark = Me.EditConcatData(remark, dr.Item("REMARK").ToString())

                    'オーバーした分を削除
                    remark = Me.CutByteValue(remark, 100)

                End If

                '総個数を設定
                soKosu += Convert.ToDecimal(dr.Item("DECI_NG_NB").ToString())

                '確定金額にゼロを設定
                dr.Item("DECI_UNCHIN") = 0
                dr.Item("DECI_CITY_EXTC") = 0
                dr.Item("DECI_WINT_EXTC") = 0
                dr.Item("DECI_RELY_EXTC") = 0
                dr.Item("DECI_TOLL") = 0
                dr.Item("DECI_INSU") = 0


                '値の反映
                For j As Integer = 0 To colMax
                    setDr.Item(j) = dr.Item(j).ToString()
                Next

            Next

            If String.IsNullOrEmpty(seiqGroupNo) = False Then
                '更新処理

                '親レコードの情報を設定
                drs(oyaRowNo).Item("DECI_WT") = soJuryo
                drs(oyaRowNo).Item("DECI_KYORI") = maxKyori
                drs(oyaRowNo).Item("REMARK") = remark
                drs(oyaRowNo).Item("DECI_NG_NB") = soKosu

                '親レコードの計算した運賃を設定
                ds = Me.SetUnchinData(ds, drs(oyaRowNo), New LMF800DS(), New LMF800BLC(), drs(oyaRowNo).Item("SEIQ_TARIFF_CD").ToString())

                '按分処理
                ds = Me.SetUncinDataDic(ds, drs(oyaRowNo))

                '更新処理
                rtnDs = Me.BlcAccess(ds, "SetGroupData")

                'エラー判定
                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function
    'END YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない

    ''' <summary>
    ''' まとめ解除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetGroupCancellData(ByVal ds As DataSet) As DataSet

        Call Me.UpdateCommonAction(ds, LMF040BLF.ActionType.GROUP_CANCELL, System.Reflection.MethodBase.GetCurrentMethod.Name, False)

        Return ds

    End Function

    ''' <summary>
    ''' レコードの初期化
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetInitData(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMF040BLF.TABLE_NM_UNCHIN)
        Dim dr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1
        Dim unchin As String = String.Empty
        Dim city As String = String.Empty
        Dim wint As String = String.Empty
        Dim haz As String = String.Empty
        Dim rely As String = String.Empty
        Dim toll As String = String.Empty
        Dim insu As String = String.Empty
        Dim juryo As String = String.Empty
        Dim kyori As String = String.Empty
        Dim kosu As String = String.Empty
        For i As Integer = 0 To max

            dr = dt.Rows(i)

            '請求情報を確定、管理情報に設定
            unchin = dr.Item("SEIQ_UNCHIN").ToString()
            city = dr.Item("SEIQ_CITY_EXTC").ToString()
            wint = dr.Item("SEIQ_WINT_EXTC").ToString()
            rely = dr.Item("SEIQ_RELY_EXTC").ToString()
            toll = dr.Item("SEIQ_TOLL").ToString()
            insu = dr.Item("SEIQ_INSU").ToString()
            juryo = dr.Item("SEIQ_WT").ToString()
            kyori = dr.Item("SEIQ_KYORI").ToString()
            kosu = dr.Item("SEIQ_NG_NB").ToString()

            dr.Item("DECI_UNCHIN") = unchin
            dr.Item("DECI_CITY_EXTC") = city
            dr.Item("DECI_WINT_EXTC") = wint
            dr.Item("DECI_RELY_EXTC") = rely
            dr.Item("DECI_TOLL") = toll
            dr.Item("DECI_INSU") = insu
            dr.Item("DECI_WT") = juryo
            dr.Item("DECI_KYORI") = kyori
            dr.Item("DECI_NG_NB") = kosu

            dr.Item("KANRI_UNCHIN") = unchin
            dr.Item("KANRI_CITY_EXTC") = city
            dr.Item("KANRI_WINT_EXTC") = wint
            dr.Item("KANRI_RELY_EXTC") = rely
            dr.Item("KANRI_TOLL") = toll
            dr.Item("KANRI_INSU") = insu

        Next

        Return ds

    End Function

#End Region

#Region "一括変更"

    ''' <summary>
    ''' 一括変更処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveUnchinItemData(ByVal ds As DataSet) As DataSet

        'タリフを更新しない場合、通常の一括変更
        Select Case ds.Tables(LMF040BLF.TABLE_NM_UNCHIN).Rows(0).Item("ITEM_DATA").ToString()

            Case LMF040BLF.SHUSEI_SEIQTO

                '請求先の一括更新
                Return Me.SetComSaveData(ds, LMF040BLF.ACTION_ID_SAVE_SEIQTO)

            Case LMF040BLF.SHUSEI_TARIFF, LMF040BLF.SHUSEI_YOKO
            Case Else

                '通常の一括更新
                Return Me.SetComSaveData(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        End Select

        'タリフを更新する場合、特殊処理
        Return Me.SaveUnchinTariffData(ds)

    End Function

    ''' <summary>
    ''' 一括変更処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveUnchinTariffData(ByVal ds As DataSet) As DataSet

        Call Me.UpdateCommonAction(ds, LMF040BLF.ActionType.IKKATU_TARIFF, System.Reflection.MethodBase.GetCurrentMethod.Name, False, New LMF800DS(), New LMF800BLC())

        Return ds

    End Function

    ''' <summary>
    ''' 一括変更(タリフ)時の運賃再設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="unchinDs">DataSet</param>
    ''' <param name="blc">LMF800BLC</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetIkkatuTariffUnchinData(ByVal ds As DataSet, ByVal unchinDs As DataSet, ByVal blc As LMF800BLC) As DataSet

        Dim dt As DataTable = ds.Tables(LMF040BLF.TABLE_NM_UNCHIN)
        Dim dr As DataRow = dt.Rows(0)

        'まとめレコードの場合
        If String.IsNullOrEmpty(dr.Item("SEIQ_GROUP_NO").ToString()) = False Then
            dr = dt.Select(Me.GetOyaDataSql(dr))(0)
        End If

        '親レコードの計算した運賃を設定
        ds = Me.SetUnchinData(ds, dr, unchinDs, blc, dr.Item("CD_L").ToString())

        '按分処理
        ds = Me.SetUncinData(ds)

        Return ds

    End Function

#End Region

#Region "運賃更新処理(再計算時)"

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    ''' <summary>
    ''' 運賃更新処理(再計算時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function UpdUnchinData(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMF040BLF.TABLE_NM_UNCHIN)
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        Dim rtnResult As Boolean = False
        Dim rtnDs As DataSet = Nothing

        '更新処理
        For i As Integer = 0 To max

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '値のクリア
                inTbl = setDs.Tables(LMF040BLF.TABLE_NM_UNCHIN)
                inTbl.Clear()

                MyBase.SetMessage(Nothing)

                '条件の設定
                inTbl.ImportRow(dt.Rows(i))

                '更新
                rtnDs = MyBase.CallBLC(Me._Blc, "UpdUnchinData", setDs)

                'エラーがあるかを判定
                rtnResult = Not MyBase.IsMessageExist()
                If rtnResult = True Then
                    'エラーが無ければCommit
                    MyBase.CommitTransaction(scope)
                End If

            End Using

        Next

        Return rtnDs

    End Function
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

    '2017/10/13 Annen アクサルタ 運賃按分計算の自動化対応 START
    Private Function UpdUnchinDataAxaltaDousou(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMF040BLF.TABLE_NM_UNCHIN)
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        Dim rtnResult As Boolean = False
        Dim rtnDs As DataSet = Nothing

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '更新処理
            For i As Integer = 0 To max

                '値のクリア
                inTbl = setDs.Tables(LMF040BLF.TABLE_NM_UNCHIN)
                inTbl.Clear()

                MyBase.SetMessage(Nothing)

                '条件の設定
                inTbl.ImportRow(dt.Rows(i))

                '更新
                rtnDs = MyBase.CallBLC(Me._Blc, "UpdUnchinData", setDs)

            Next

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageStoreExist()
            If rtnResult = True Then
                'エラーが無ければCommit
                MyBase.CommitTransaction(scope)
            End If

        End Using

        Return rtnDs

    End Function
    '2017/10/13 Annen アクサルタ 運賃按分計算の自動化対応 END
#End Region

#Region "共通"

    ''' <summary>
    ''' 更新共通処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetComSaveData(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Call Me.UpdateCommonAction(ds, LMF040BLF.ActionType.NOMAL, actionId, False)

        Return ds

    End Function

#End Region

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionStr">メソッド名</param>
    ''' <param name="chkFlg">チェックフラグ　True:通常のエラー判定　False:一括更新のエラー判定</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ScopeStartEnd(ByVal ds As DataSet, ByVal actionStr As String, ByVal chkFlg As Boolean) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'BLCアクセス
            ds = Me.BlcAccess(ds, actionStr)

            'エラー判定
            Dim chk As Boolean = True
            If chkFlg = True Then
                chk = MyBase.IsMessageExist()
            Else
                chk = Me.IsMesExist(ds)
            End If

            'エラーがない場合、コミット
            If chk = False Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' BLCクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function BlcAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallBLC(Me._Blc, actionId, ds)

    End Function

    ''' <summary>
    ''' 運賃を計算して設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="unchinDs">DataSet</param>
    ''' <param name="blc">LMF800BLC</param>
    ''' <param name="tariffCd">タリフコード</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetUnchinData(ByVal ds As DataSet _
                                   , ByVal dr As DataRow _
                                   , ByVal unchinDs As DataSet _
                                   , ByVal blc As LMF800BLC _
                                   , ByVal tariffCd As String _
                                   , Optional ByVal KenFlag As Boolean = False _
                                   , Optional ByVal KenKyori As String = "") As DataSet

        'パラメータ設定
        Dim inTbl As DataTable = unchinDs.Tables(LMF040BLF.TABLE_NM_800IN)

        '値のクリア
        inTbl.Clear()

        Dim inDr As DataRow = inTbl.NewRow()

        With inDr

            .Item("ACTION_FLG") = LMF040BLF.ACT_FGL_GYOM
            .Item("NRS_BR_CD") = dr.Item("NRS_BR_CD").ToString()
            .Item("ARR_PLAN_DATE") = dr.Item("ARR_PLAN_DATE").ToString()
            .Item("MOTO_DATA_KB") = dr.Item("MOTO_DATA_KB").ToString()
            .Item("SEIQ_TARIFF_CD") = tariffCd
            .Item("SEIQ_ETARIFF_CD") = dr.Item("SEIQ_ETARIFF_CD").ToString()
            .Item("UNSO_PKG_NB") = dr.Item("DECI_NG_NB").ToString()
            .Item("NB_UT") = dr.Item("SEIQ_PKG_UT").ToString()
            .Item("UNSO_WT") = dr.Item("DECI_WT").ToString()
            .Item("TARIFF_BUNRUI_KB") = dr.Item("SEIQ_TARIFF_BUNRUI_KB").ToString()
            If KenFlag = False Then
                .Item("KYORI") = dr.Item("DECI_KYORI").ToString()
            Else
                .Item("KYORI") = KenKyori
            End If
            .Item("DANGER_KB") = dr.Item("SEIQ_DANGER_KB").ToString()
            .Item("CUST_CD_L") = dr.Item("CUST_CD_L").ToString()
            .Item("CUST_CD_M") = dr.Item("CUST_CD_M").ToString()
            .Item("DEST_CD") = dr.Item("DEST_CD").ToString()
            .Item("DEST_JIS") = dr.Item("DEST_JIS").ToString()
            .Item("VCLE_KB") = dr.Item("SEIQ_SYARYO_KB").ToString()
            .Item("UNSO_ONDO_KB") = dr.Item("UNSO_ONDO_KB").ToString()
            .Item("SIZE_KB") = dr.Item("SIZE_KB").ToString()
            .Item("UNSO_DATE") = Me.SetUnsoDate(dr)

        End With

        inTbl.Rows.Add(inDr)

        'プログラム起動
        unchinDs = MyBase.CallBLC(blc, LMF040BLF.ACTION_ID_UNCHINI_CALC, unchinDs)

        '戻り値の反映
        Dim outDr As DataRow = unchinDs.Tables(LMF040BLF.TABLE_NM_800OUT).Rows(0)
        With dr

            .Item("DECI_UNCHIN") = outDr.Item("UNCHIN").ToString()
            .Item("DECI_CITY_EXTC") = outDr.Item("CITY_EXTC").ToString()
            .Item("DECI_WINT_EXTC") = outDr.Item("WINT_EXTC").ToString()
            .Item("DECI_RELY_EXTC") = outDr.Item("RELY_EXTC").ToString()
            .Item("DECI_TOLL") = outDr.Item("TOLL").ToString()
            '.Item("DECI_INSU") = outDr.Item("INSU").ToString()      'DEL 2018/10/23 依頼番号 : 002400   【LMS】運送保険_設定商品を出荷時、運送の保険料欄に保険料を自動入力させる

        End With

        Return ds

    End Function

    ''' <summary>
    ''' 親レコード取得のSQL
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>SQL</returns>
    ''' <remarks></remarks>
    Private Function GetOyaDataSql(ByVal dr As DataRow) As String

        '通常レコードの場合
        If String.IsNullOrEmpty(dr.Item("SEIQ_GROUP_NO").ToString()) = True Then
            Return String.Empty
        End If

        Return String.Concat(" UNSO_NO_L = '", dr.Item("SEIQ_GROUP_NO").ToString(), "' " _
                                          , " AND UNSO_NO_M = '", dr.Item("SEIQ_GROUP_NO_M").ToString(), "' ")

    End Function

    ''' <summary>
    ''' 2つの値を連結して設定
    ''' </summary>
    ''' <param name="value1">値1</param>
    ''' <param name="value2">値2</param>
    ''' <returns>編集後の値</returns>
    ''' <remarks></remarks>
    Private Function EditConcatData(ByVal value1 As String, ByVal value2 As String, Optional ByVal str As String = " , ") As String

        EditConcatData = value1
        If String.IsNullOrEmpty(EditConcatData) = True Then

            EditConcatData = value2

        Else

            If String.IsNullOrEmpty(value2) = False Then

                EditConcatData = String.Concat(EditConcatData, str, value2)

            End If

        End If

        Return EditConcatData

    End Function

    ''' <summary>
    ''' 後ろの文字を削除する
    ''' </summary>
    ''' <param name="value">削る文字</param>
    ''' <param name="ketasu">桁数</param>
    ''' <returns>戻り値</returns>
    ''' <remarks></remarks>
    Private Function CutByteValue(ByVal value As String, ByVal ketasu As Integer) As String

        '空の場合、スルー
        If String.IsNullOrEmpty(value) = True Then
            Return value
        End If

        'バイト数を取得
        Dim byteCount As Integer = Me.GetByteCount(value)

        Dim max As Integer = value.Length - 1

        For index As Integer = max To 0 Step -1

            If byteCount > ketasu Then

                '後ろの文字を1文字カット
                value = value.Substring(0, index)

                'バイト数を更新
                byteCount = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(value)

            Else

                Exit For

            End If

        Next

        Return value

    End Function

    ''' <summary>
    ''' バイト数を取得
    ''' </summary>
    ''' <param name="value">文字</param>
    ''' <returns>バイト数</returns>
    ''' <remarks></remarks>
    Private Function GetByteCount(ByVal value As String) As Integer
        Return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(value)
    End Function

    ''' <summary>
    ''' 計算プログラムの運送日を設定
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>運送日</returns>
    ''' <remarks></remarks>
    Private Function SetUnsoDate(ByVal dr As DataRow) As String

        SetUnsoDate = String.Empty

        With dr

            Select Case .Item("MOTO_DATA_KB").ToString()

                Case LMF040BLF.MOTO_DATA_NYUKA

                    SetUnsoDate = .Item("ARR_PLAN_DATE").ToString()

                Case Else

                    Select Case .Item("UNTIN_CALCULATION_KB").ToString()

                        Case LMF040BLF.CALC_SHUKKA

                            SetUnsoDate = .Item("OUTKA_PLAN_DATE").ToString()

                        Case LMF040BLF.CALC_NYUKA

                            SetUnsoDate = .Item("ARR_PLAN_DATE").ToString()

                    End Select

            End Select

            Return SetUnsoDate

        End With

    End Function

    ''' <summary>
    ''' 運送番号が同じデータを抽出
    ''' </summary>
    ''' <param name="dt">DataTable</param>
    ''' <param name="unsoNoL">運送番号(大)</param>
    ''' <param name="unsoNoM">運送番号(中)</param>
    ''' <returns>DataRow配列</returns>
    ''' <remarks></remarks>
    Private Function GetUnsoLData(ByVal dt As DataTable, ByVal unsoNoL As String, Optional ByVal unsoNoM As String = "") As DataRow()

        Dim sql As String = String.Concat("UNSO_NO_L = '", unsoNoL, "' ")

        If String.IsNullOrEmpty(unsoNoM) = False Then

            sql = String.Concat(sql, " AND UNSO_NO_M = '", unsoNoM, "' ")

        End If

        '次処理で逆ループするため降順
        Return dt.Select(sql, "ROW_NO desc")

    End Function

    ''' <summary>
    ''' 一括更新エラー判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>エラーが設定されている場合、True エラー設定されていない場合、False</returns>
    ''' <remarks>
    ''' 画面で選択した行を複数同時に更新するケースがあるため
    ''' 設定されている全レコードに対してチェックを行う
    ''' </remarks>
    Private Function IsMesExist(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables(LMF040BLF.TABLE_NM_UNCHIN)
        Dim max As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To max
            If MyBase.IsMessageStoreExist(Convert.ToInt32(dt.Rows(i).Item("ROW_NO").ToString())) = True Then
                Return True
            End If
        Next

        Return False

    End Function

    ''' <summary>
    ''' 更新系のメソッド(まとめ指示以外)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <param name="chkFlg">チェックフラグ　True:通常のエラー判定　False:一括更新のエラー判定</param>
    ''' <param name="unchinDs">計算プログラム用 DataSet</param>
    ''' <param name="blc">計算プログラム用 BLCクラス</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateCommonAction(ByVal ds As DataSet _
                                            , ByVal actionType As LMF040BLF.ActionType _
                                            , ByVal actionId As String _
                                            , ByVal chkFlg As Boolean _
                                            , Optional ByVal unchinDs As DataSet = Nothing _
                                            , Optional ByVal blc As LMF800BLC = Nothing _
                                            ) As DataSet

        Dim chkDt As DataTable = ds.Tables(LMF040BLF.TABLE_NM_UNCHIN).Copy
        Dim dt As DataTable = Me.SetLoopDataSet(ds).Tables(LMF040BLF.TABLE_NM_UNCHIN)
        Dim upDs As DataSet = ds.Copy
        Dim upDt As DataTable = upDs.Tables(LMF040BLF.TABLE_NM_UNCHIN)
        Dim max As Integer = 0
        Dim unsoL As String = String.Empty
        Dim chkUnsoL As String = String.Empty
        Dim cnt As Integer = 0
        Dim recCnt As Integer = 0
        Dim selectDs As DataSet = ds.Copy
        Dim selectDt As DataTable = selectDs.Tables(LMF040BLF.TABLE_NM_UNCHIN)
#If True Then   'ADD 2020/07/20 013381   【LMS】日産物流の場合運賃請求明細にまとめた行を表示する
        Dim sMATOME_REMARK_UPNG_FLG As String = ds.Tables(LMF040BLF.TABLE_NM_UNCHIN).Rows(0).Item("MATOME_REMARK_UPNG_FLG").ToString
#End If
        While dt.Rows.Count <> 0

            '変数の初期化
            upDt.Clear()
            unsoL = String.Empty
            chkUnsoL = String.Empty

            '更新するデータを設定
            Call Me.SetUpdData(ds, dt, upDt, actionType, dt.Rows(0).Item("UNSO_NO_L").ToString(), String.Empty, unchinDs, blc)

            cnt = upDt.Rows.Count - 1
            For i As Integer = 0 To cnt

                '運送番号(大)を設定
                chkUnsoL = upDt.Rows(i).Item("UNSO_NO_L").ToString()

                '前回の運送番号(大)と違う場合、保持
                If unsoL.Equals(chkUnsoL) = False Then
                    unsoL = chkUnsoL

                    '紐付く運賃レコードを取得
                    selectDt.Clear()
                    selectDt.ImportRow(upDt.Rows(i))

                    selectDs = Me.BlcAccess(selectDs, LMF040BLF.ACTION_ID_SELECT_INIT_UNCHIN)
                    recCnt = selectDt.Rows.Count - 1
                    For j As Integer = 0 To recCnt
                        Call Me.SetUpdData(ds, dt, upDt, actionType, selectDt.Rows(j).Item("SEIQ_GROUP_NO").ToString(), selectDt.Rows(j).Item("SEIQ_GROUP_NO_M").ToString(), unchinDs, blc)
                    Next

                End If
#If True Then   'ADD 2020/07/20 013381   【LMS】日産物流の場合運賃請求明細にまとめた行を表示する
                upDt.Rows(i).Item("MATOME_REMARK_UPNG_FLG") = sMATOME_REMARK_UPNG_FLG.ToString
#End If
            Next


            '更新処理
            upDs = Me.ScopeStartEnd(Me.SetRowNoData(upDs, chkDt), actionId, chkFlg)

        End While

        Return upDs

    End Function

    ''' <summary>
    ''' 更新するデータを設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dt">DataTable</param>
    ''' <param name="upDt">DataTable</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="unsoNoL">運送番号(大)</param>
    ''' <param name="unsoNoM">運送番号(中)</param>
    ''' <param name="unchinDs">計算プログラム用　DataSet</param>
    ''' <param name="blc">計算プログラム用　BLCクラス</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetUpdData(ByVal ds As DataSet _
                                        , ByVal dt As DataTable _
                                        , ByVal upDt As DataTable _
                                        , ByVal actionType As LMF040BLF.ActionType _
                                        , ByVal unsoNoL As String _
                                        , ByVal unsoNoM As String _
                                        , ByVal unchinDs As DataSet _
                                        , ByVal blc As LMF800BLC _
                                        ) As DataSet

        Dim arr As ArrayList = Nothing
        Dim setArr As ArrayList = Nothing
        Dim max As Integer = 0

        '運送番号ごとのデータを抽出
        arr = Me.SetUpdData(ds, dt, upDt, Me.GetUnsoLData(dt, unsoNoL, unsoNoM), actionType, unchinDs, blc)

        While arr.Count <> 0

            '運送番号ごとのデータを抽出
            setArr = Me.SetUpdData(ds, dt, upDt, Me.GetUnsoLData(dt, arr(0).ToString()), actionType, unchinDs, blc)

            'リストから削除
            arr.RemoveAt(0)

            'マージ処理
            max = setArr.Count - 1
            For i As Integer = 0 To max
                arr.Add(setArr(i))
            Next

        End While

        Return ds

    End Function

    ''' <summary>
    ''' コミット判定データ作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dt">DataTable</param>
    ''' <param name="upDt">DataTable</param>
    ''' <param name="drs">DataRow配列</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="unchinDs">DataSet</param>
    ''' <param name="blc">LMF800BLC</param>
    ''' <returns>ArrayList</returns>
    ''' <remarks></remarks>
    Private Function SetUpdData(ByVal ds As DataSet _
                                    , ByVal dt As DataTable _
                                    , ByVal upDt As DataTable _
                                    , ByVal drs As DataRow() _
                                    , ByVal actionType As LMF040BLF.ActionType _
                                    , ByVal unchinDs As DataSet _
                                    , ByVal blc As LMF800BLC _
                                    ) As ArrayList

        Dim selectDs As DataSet = ds.Copy
        Dim selectDt As DataTable = selectDs.Tables(LMF040BLF.TABLE_NM_UNCHIN)
        Dim max As Integer = drs.Length - 1
        Dim cnt As Integer = 0
        Dim arr As ArrayList = New ArrayList()
        Dim unsoL As String = String.Empty
        Dim chkUnsoL As String = String.Empty
        For i As Integer = max To 0 Step -1

            '値のクリア
            selectDt.Clear()

            '検索条件の設定
            selectDt.ImportRow(drs(i))
            drs(i).Delete()

            '検索処理
            selectDs = Me.BlcAccess(selectDs, LMF040BLF.ACTION_ID_SELECT_UNCHIN)

            '取得できない場合、終了
            If MyBase.IsMessageExist() = True Then
                Continue For
            End If

            Select Case actionType

                Case LMF040BLF.ActionType.GROUP_CANCELL

                    'レコードの初期化
                    selectDs = Me.SetInitData(selectDs)

                Case LMF040BLF.ActionType.IKKATU_TARIFF

                    '運賃情報設定
                    selectDs = Me.SetIkkatuTariffUnchinData(selectDs, unchinDs, blc)

            End Select

            '取得した情報を保持
            selectDt = selectDs.Tables(LMF040BLF.TABLE_NM_UNCHIN)
            cnt = selectDt.Rows.Count - 1
            For index As Integer = 0 To cnt

                '運送番号(大)を設定
                chkUnsoL = selectDt.Rows(index).Item("UNSO_NO_L").ToString()

                '前回の運送番号(大)と違う場合、保持
                If unsoL.Equals(chkUnsoL) = False Then
                    unsoL = chkUnsoL
                    arr.Add(unsoL)
                End If

                upDt.ImportRow(selectDt.Rows(index))
            Next

        Next

        Return arr

    End Function

    ''' <summary>
    ''' まとめ指示を行っているレコードを削除 + 並び替えた値を再設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetLoopDataSet(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMF040BLF.TABLE_NM_UNCHIN)
        Dim setDt As DataTable = dt.Copy
        Dim drs As DataRow() = setDt.Select(String.Empty, "SEIQ_GROUP_NO desc,SEIQ_GROUP_NO_M desc")
        Dim setDr As DataRow = Nothing
        Dim max As Integer = 0

        '並び替えた値を設定(Table視点でループしたいため)
        Dim rowMax As Integer = setDt.Rows.Count - 1
        Dim colMax As Integer = setDt.Columns.Count - 1
        For i As Integer = 0 To rowMax
            For j As Integer = 0 To colMax
                dt.Rows(i).Item(j) = drs(i).Item(j).ToString()
            Next
        Next

        '並び替えた値を再設定
        setDt = dt.Copy

        '設定するDataTableはクリア
        dt.Clear()

        While setDt.Rows.Count <> 0

            '先頭行を設定
            setDr = setDt.Rows(0)

            '行追加
            dt.ImportRow(setDr)

            '通常レコードの場合、自レコードを削除
            If String.IsNullOrEmpty(setDr.Item("SEIQ_GROUP_NO").ToString()) = True Then

                setDr.Delete()
                Continue While

            End If

            'まとめレコードの場合
            drs = setDt.Select(String.Concat("SEIQ_GROUP_NO = '", setDr.Item("SEIQ_GROUP_NO").ToString(), "' AND SEIQ_GROUP_NO_M = '", setDr.Item("SEIQ_GROUP_NO_M").ToString(), "' "))

            'まとめレコード全てを削除
            max = drs.Length - 1
            For i As Integer = max To 0 Step -1
                drs(i).Delete()
            Next

        End While

        Return ds

    End Function

    ''' <summary>
    ''' 画面の行番号を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="chkDt">DataTable</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetRowNoData(ByVal ds As DataSet, ByVal chkDt As DataTable) As DataSet

        Dim dt As DataTable = ds.Tables(LMF040BLF.TABLE_NM_UNCHIN)
        Dim drs As DataRow() = Nothing
        Dim dr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To max

            'チェックする行を設定
            dr = dt.Rows(i)

            '画面のDataTableに設定した内容があるかを検索
            drs = chkDt.Select(String.Concat("UNSO_NO_L = '", dr.Item("UNSO_NO_L").ToString(), "' " _
                                          , " AND UNSO_NO_M = '", dr.Item("UNSO_NO_M").ToString(), "' "))

            '取得できない場合、スルー
            If drs.Length < 1 Then
                Continue For
            End If

            dr.Item("ROW_NO") = drs(0).Item("ROW_NO").ToString()

        Next

        Return ds

    End Function

#End Region

#Region "計算"

    ''' <summary>
    ''' 按分処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetUncinData(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMF040BLF.TABLE_NM_UNCHIN)
        Dim dr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1
        Dim soJuryo As Decimal = 0

        '合計値を取得
        For i As Integer = 0 To max

            '重量の合計
            soJuryo += Convert.ToDecimal(dt.Rows(i).Item("SEIQ_WT").ToString())

        Next

        '親レコード確定情報
        Dim unchinDr As DataRow = dt.Select(Me.GetOyaDataSql(dt.Rows(0)))(0)
        Dim dUnchin As Decimal = Convert.ToDecimal(unchinDr.Item("DECI_UNCHIN").ToString())
        Dim dCity As Decimal = Convert.ToDecimal(unchinDr.Item("DECI_CITY_EXTC").ToString())
        Dim dWint As Decimal = Convert.ToDecimal(unchinDr.Item("DECI_WINT_EXTC").ToString())
        Dim dRely As Decimal = Convert.ToDecimal(unchinDr.Item("DECI_RELY_EXTC").ToString())
        Dim dToll As Decimal = Convert.ToDecimal(unchinDr.Item("DECI_TOLL").ToString())
        Dim dInsu As Decimal = Convert.ToDecimal(unchinDr.Item("DECI_INSU").ToString())

        Dim unchin As Decimal = 0
        Dim city As Decimal = 0
        Dim wint As Decimal = 0
        Dim rely As Decimal = 0
        Dim toll As Decimal = 0
        Dim insu As Decimal = 0
        Dim juryo As Decimal = 0
        Dim value As Decimal = 0

        For i As Integer = 0 To max

            dr = dt.Rows(i)

            '重量の設定
            juryo = Convert.ToDecimal(dr.Item("SEIQ_WT").ToString())

            '運賃の按分
            value = Me.AnbunData(dUnchin, soJuryo, juryo)
            unchin += value
            dr.Item("KANRI_UNCHIN") = value

            '都市の按分
            value = Me.AnbunData(dCity, soJuryo, juryo)
            city += value
            dr.Item("KANRI_CITY_EXTC") = value

            '冬期の按分
            value = Me.AnbunData(dWint, soJuryo, juryo)
            wint += value
            dr.Item("KANRI_WINT_EXTC") = value

            '中継料の按分
            value = Me.AnbunData(dRely, soJuryo, juryo)
            rely += value
            dr.Item("KANRI_RELY_EXTC") = value

            '通行料の按分
            value = Me.AnbunData(dToll, soJuryo, juryo)
            toll += value
            dr.Item("KANRI_TOLL") = value

            '保険料の按分
            value = Me.AnbunData(dInsu, soJuryo, juryo)
            insu += value
            dr.Item("KANRI_INSU") = value

        Next

        '端数分を親レコードに設定
        unchinDr.Item("KANRI_UNCHIN") = Convert.ToDecimal(unchinDr.Item("KANRI_UNCHIN").ToString()) + dUnchin - unchin
        unchinDr.Item("KANRI_CITY_EXTC") = Convert.ToDecimal(unchinDr.Item("KANRI_CITY_EXTC").ToString()) + dCity - city
        unchinDr.Item("KANRI_WINT_EXTC") = Convert.ToDecimal(unchinDr.Item("KANRI_WINT_EXTC").ToString()) + dWint - wint
        unchinDr.Item("KANRI_RELY_EXTC") = Convert.ToDecimal(unchinDr.Item("KANRI_RELY_EXTC").ToString()) + dRely - rely
        unchinDr.Item("KANRI_TOLL") = Convert.ToDecimal(unchinDr.Item("KANRI_TOLL").ToString()) + dToll - toll
        unchinDr.Item("KANRI_INSU") = Convert.ToDecimal(unchinDr.Item("KANRI_INSU").ToString()) + dInsu - insu

        Return ds

    End Function

    'START YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない
    ''' <summary>
    ''' 按分処理(日立物流用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetUncinDataDic(ByVal ds As DataSet, _
                                     ByVal oyadr As DataRow) As DataSet

        Dim dr() As DataRow = ds.Tables(LMF040BLF.TABLE_NM_UNCHIN).Select(String.Concat("SEIQ_GROUP_NO = '", oyadr.Item("SEIQ_GROUP_NO").ToString, "' AND ", _
                                                                                        "SEIQ_GROUP_NO_M = '", oyadr.Item("SEIQ_GROUP_NO_M").ToString, "'"))
        Dim max As Integer = dr.Length - 1
        Dim soJuryo As Decimal = 0

        '合計値を取得
        For i As Integer = 0 To max

            '重量の合計
            soJuryo += Convert.ToDecimal(dr(i).Item("SEIQ_WT").ToString())

        Next

        '親レコード確定情報
        Dim dUnchin As Decimal = Convert.ToDecimal(oyadr.Item("DECI_UNCHIN").ToString())
        Dim dCity As Decimal = Convert.ToDecimal(oyadr.Item("DECI_CITY_EXTC").ToString())
        Dim dWint As Decimal = Convert.ToDecimal(oyadr.Item("DECI_WINT_EXTC").ToString())
        Dim dRely As Decimal = Convert.ToDecimal(oyadr.Item("DECI_RELY_EXTC").ToString())
        Dim dToll As Decimal = Convert.ToDecimal(oyadr.Item("DECI_TOLL").ToString())
        Dim dInsu As Decimal = Convert.ToDecimal(oyadr.Item("DECI_INSU").ToString())

        Dim unchin As Decimal = 0
        Dim city As Decimal = 0
        Dim wint As Decimal = 0
        Dim rely As Decimal = 0
        Dim toll As Decimal = 0
        Dim insu As Decimal = 0
        Dim juryo As Decimal = 0
        Dim value As Decimal = 0

        For i As Integer = 0 To max

            '重量の設定
            juryo = Convert.ToDecimal(dr(i).Item("SEIQ_WT").ToString())

            '運賃の按分
            value = Me.AnbunData(dUnchin, soJuryo, juryo)
            unchin += value
            dr(i).Item("KANRI_UNCHIN") = value

            '都市の按分
            value = Me.AnbunData(dCity, soJuryo, juryo)
            city += value
            dr(i).Item("KANRI_CITY_EXTC") = value

            '冬期の按分
            value = Me.AnbunData(dWint, soJuryo, juryo)
            wint += value
            dr(i).Item("KANRI_WINT_EXTC") = value

            '中継料の按分
            value = Me.AnbunData(dRely, soJuryo, juryo)
            rely += value
            dr(i).Item("KANRI_RELY_EXTC") = value

            '通行料の按分
            value = Me.AnbunData(dToll, soJuryo, juryo)
            toll += value
            dr(i).Item("KANRI_TOLL") = value

            '保険料の按分
            value = Me.AnbunData(dInsu, soJuryo, juryo)
            insu += value
            dr(i).Item("KANRI_INSU") = value

        Next

        '端数分を親レコードに設定
        oyadr.Item("KANRI_UNCHIN") = Convert.ToDecimal(oyadr.Item("KANRI_UNCHIN").ToString()) + dUnchin - unchin
        oyadr.Item("KANRI_CITY_EXTC") = Convert.ToDecimal(oyadr.Item("KANRI_CITY_EXTC").ToString()) + dCity - city
        oyadr.Item("KANRI_WINT_EXTC") = Convert.ToDecimal(oyadr.Item("KANRI_WINT_EXTC").ToString()) + dWint - wint
        oyadr.Item("KANRI_RELY_EXTC") = Convert.ToDecimal(oyadr.Item("KANRI_RELY_EXTC").ToString()) + dRely - rely
        oyadr.Item("KANRI_TOLL") = Convert.ToDecimal(oyadr.Item("KANRI_TOLL").ToString()) + dToll - toll
        oyadr.Item("KANRI_INSU") = Convert.ToDecimal(oyadr.Item("KANRI_INSU").ToString()) + dInsu - insu

        Return ds

    End Function
    'END YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない

    ''' <summary>
    ''' 按分計算
    ''' </summary>
    ''' <param name="unchin">運賃</param>
    ''' <param name="soJuryo">総重量</param>
    ''' <param name="juryo">重量</param>
    ''' <returns>按分結果</returns>
    ''' <remarks>通常レコードの場合、自レコードの重量と総重量は同値になるので計算してよい</remarks>
    Private Function AnbunData(ByVal unchin As Decimal, ByVal soJuryo As Decimal, ByVal juryo As Decimal) As Decimal

        'START YANAI 要望番号1267 運賃重量を変更しても運送重量が変わらない
        If soJuryo = 0 Then
            Return unchin
        End If
        'END YANAI 要望番号1267 運賃重量を変更しても運送重量が変わらない

        '運賃 * 自レコードの重量 / 総重量(切捨て)
        Return System.Math.Floor(unchin * juryo / soJuryo)

    End Function

#End Region

#End Region

End Class
