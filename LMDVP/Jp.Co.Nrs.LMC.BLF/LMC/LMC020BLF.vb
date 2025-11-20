' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷
'  プログラムID     :  LMC020    : 出荷データ編集
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC020BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC020BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Const"

    ''' <summary>
    ''' 印刷区分(S039)
    ''' </summary>
    ''' <remarks></remarks>
    Private Class PRINT_KB

        ''' <summary>
        ''' 取扱説明書
        ''' </summary>
        ''' <remarks></remarks>
        Public Const USERS_MANUAL As String = "17"

        ''' <summary>
        ''' 梱包明細
        ''' </summary>
        ''' <remarks></remarks>
        Public Const PACKAGE_DETAILS As String = "18"

        ''' <summary>
        ''' 取扱説明書(個数１)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const USERS_MANUAL_KOSU1 As String = "19"

        ''' <summary>
        ''' 取扱説明書(端数)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const USERS_MANUAL_HASU As String = "20"

        ''' <summary>
        ''' 運送保険
        ''' </summary>
        ''' <remarks></remarks>
        Public Const UNSO_HOKEN As String = "21"

        ''' <summary>
        ''' 立合書
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ATTEND As String = "23"

        ''' <summary>
        ''' 出荷チェックリスト
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTBOUND_CHECK As String = "24"

#If True Then       'ADD 2023/03/29 送品案内書(FFEM)追加
        ''' <summary>
        ''' 送品案内書(FFEM)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SHIPMENTGUIDE As String = "25"
#End If

        ''' <summary>
        ''' 毒劇物譲受書(FFEM)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DOKU_JOJU As String = "26"

    End Class
#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMC020BLC = New LMC020BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _UnchinBlc As LMF800BLC = New LMF800BLC()

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _ShiharaiBlc As LMF810BLC = New LMF810BLC()
    'END UMANO 要望番号1302 支払運賃に伴う修正。

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 初期検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInitData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return Me.SetSysDateTime(MyBase.CallBLC(_Blc, "SelectInitData", ds))

    End Function

    ''' <summary>
    ''' 請求日のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function IsSeiqDateChk(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return Me.SetSysDateTime(MyBase.CallBLC(_Blc, "IsSeiqDateChk", ds))

    End Function

    ''' <summary>
    ''' 荷主マスタ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCustData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(_Blc, "SelectCustData", ds)

    End Function

    'START YANAI 要望番号853 まとめ処理対応
    ''' <summary>
    ''' 在庫データ検索処理(まとめ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectZaiDataMATOME(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(_Blc, "SelectZaiDataMATOME", ds)

    End Function
    'END YANAI 要望番号853 まとめ処理対応

    '要望番号:1350 terakawa 2012.08.27 Start  
    ''' <summary>
    ''' 同一置き場（商品・ロット）のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkGoodsLot(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, "ChkGoodsLot")

    End Function
    '要望番号:1350 terakawa 2012.08.27 End

    ''' <summary>
    ''' 入目チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>2019/12/16 要望管理009513 add</remarks>
    Private Function IrimeCheck(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, "IrimeCheck")

    End Function

    '要望番号:0612 nakamura 2012.11.26 Start  
    ''' <summary>
    ''' 振替一括削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInkaData(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, "SelectInkaData")

    End Function
    '要望番号:0612 nakamura 2012.11.26 End


    ''' <summary>
    ''' 支払データスキップ判定
    ''' </summary>
    ''' <returns>
    ''' false: スキップ対象外
    ''' true : スキップ対象
    ''' </returns>
    ''' <remarks>
    ''' 支払運賃タリフM,支払割増運賃M, 支払横持ちタリフヘッダに対象営業所の
    ''' データが存在しない場合は支払データの登録対象としない。
    ''' </remarks>
    Private Function IsSkipInsertShiharaiData(ByVal ds As DataSet) As Boolean

        ' ToDo: スキップ判定は、速度改善を目的としているので、判定自体が速度低下を
        '       招く場合は、Z_KBN等による営業所コード直接指定へ変更する。

        ' 支払タリフ件数取得
        Const actionId As String = "SelectCountShiharaiTariffByNrsBrCd"

        Using copy As DataSet = ds.Copy

            Me.ShiharaiBlcAccess(copy, actionId).Dispose()

            Return (0 = MyBase.GetResultCount())

        End Using

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.SaveComData(ds, "InsertSaveAction")

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.SaveComData(ds, "UpdateSaveAction")

    End Function

    ''' <summary>
    ''' 更新登録（終算日修正・運送修正）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SimpleUpdateSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.SimpleScopeStartEnd(ds, "UpdateSaveAction")

    End Function

    ''' <summary>
    ''' 保存処理共通
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveComData(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        '保存処理
        ds = Me.ScopeStartEnd(ds, actionId)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '印刷処理
        Call Me.SavePrintData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 削除時の更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, "UpdateSaveAction", False)

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteAction(ByVal ds As DataSet) As DataSet

        Call Me.ScopeStartEnd(ds, "DeleteAction", False)
        If MyBase.IsMessageExist() = False Then

            'エラーではない場合は印刷処理
            Return Me.DeletePrintAction(ds)

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 削除時の共通印刷アクション
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeletePrintAction(ByVal ds As DataSet) As DataSet

        '取消連絡印刷
        Dim max As Integer = ds.Tables("LMC020_OUTKA_M").Rows.Count - 1

        For i As Integer = 0 To max
            Dim outSdr() As DataRow = ds.Tables("LMC020_OUTKA_S").Select(String.Concat("NRS_BR_CD = '", ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString(), "' AND ", _
                    "OUTKA_NO_L = '", ds.Tables("LMC020_OUTKA_L").Rows(0).Item("OUTKA_NO_L").ToString(), "' AND ", _
                    "OUTKA_NO_M = '", ds.Tables("LMC020_OUTKA_M").Rows(i).Item("OUTKA_NO_M").ToString(), "' AND ", _
                    "SYS_DEL_FLG = '1'"))
            If outSdr.Length = 0 Then
                Return ds
            End If
        Next
        Call Me.DoPrint(ds, "09")
        Return ds

    End Function

    ''' <summary>
    ''' 印刷時の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdatePrintData(ByVal ds As DataSet) As DataSet

        ds.Tables(LMConst.RD).Clear()

        Dim dr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)
        Dim printKb As String = dr.Item("PRINT_KB").ToString()
        Dim printNb As Integer = Convert.ToInt32(dr.Item("PRT_NB").ToString())
        Dim printNbFrom As Integer = Convert.ToInt32(dr.Item("PRT_NB_FROM").ToString())
        Dim printNbTo As Integer = Convert.ToInt32(dr.Item("PRT_NB_TO").ToString())
        Dim touKanriYn As String = dr.Item("TOU_KANRI_YN").ToString()
        Dim saszUser As String = dr.Item("SASZ_USER").ToString()
        'START YANAI 20120122 立会書印刷対応
        Dim tachiaiFlg As String = dr.Item("TACHIAI_FLG").ToString()
        'END YANAI 20120122 立会書印刷対応
        Dim meitetsu_FLG As String = dr.Item("MEITETSU_FLG").ToString()     'ADD 2017/07/20

        '印刷（取消連絡）以外、更新処理を実行
        If "09".Equals(printKb) = False Then

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '更新処理
                ds = Me.BlcAccess(ds, "UpdatePrintData")

                'エラーの場合、終了
                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End Using

        End If

        'OUTKA_M退避
        Dim inOutkaM As DataTable = ds.Tables("LMC020_OUTKA_M").Copy

        '再検索
        Dim custCdL As String = dr.Item("CUST_CD_L").ToString()
        Dim inDt As DataTable = ds.Tables("LMC020IN")
        inDt.Clear()
        inDt.ImportRow(dr)
        Dim rtnDs As DataSet = Me.SelectInitData(ds)
        '印刷部数がクリアされてしまっているので、一時保存しておいた値を再設定
        rtnDs.Tables("LMC020_OUTKA_L").Rows(0).Item("PRT_NB") = printNb
        rtnDs.Tables("LMC020_OUTKA_L").Rows(0).Item("PRT_NB_FROM") = printNbFrom
        rtnDs.Tables("LMC020_OUTKA_L").Rows(0).Item("PRT_NB_TO") = printNbTo
        '棟・班別フラグがクリアされてしまっているので、一時保存しておいた値を再設定
        rtnDs.Tables("LMC020_OUTKA_L").Rows(0).Item("TOU_KANRI_YN") = touKanriYn

        '指図ユーザーが更新されているかを比較する為、一時保存しておいた値を再設定
        rtnDs.Tables("LMC020_OUTKA_L").Rows(0).Item("SASZ_USER_OLD") = saszUser

        'START YANAI 20120122 立会書印刷対応
        rtnDs.Tables("LMC020_OUTKA_L").Rows(0).Item("TACHIAI_FLG") = tachiaiFlg
        'END YANAI 20120122 立会書印刷対応

        rtnDs.Tables("LMC020_OUTKA_L").Rows(0).Item("MEITETSU_FLG") = meitetsu_FLG      'ADD 2017/07/20

        '再検索結果の印刷フラグを更新
        For Each inOutkaDr As DataRow In inOutkaM.Rows
            Dim rtnDr As DataRow() = rtnDs.Tables("LMC020_OUTKA_M").Select(String.Concat("NRS_BR_CD  = '", inOutkaDr.Item("NRS_BR_CD").ToString, "' AND ", _
                                                                                         "OUTKA_NO_M = '", inOutkaDr.Item("OUTKA_NO_M").ToString, "'"))
            If (LMConst.FLG.ON).Equals(inOutkaDr.Item("PRINT_FLG").ToString) Then
                If 0 < rtnDr.Length Then
                    rtnDr(0).Item("PRINT_FLG") = LMConst.FLG.ON
                End If
            Else
                If 0 < rtnDr.Length Then
                    rtnDr(0).Item("PRINT_FLG") = LMConst.FLG.OFF
                End If
            End If
        Next

        rtnDs.Merge(New RdPrevInfoDS)
        rtnDs.Tables(LMConst.RD).Clear()

        '印刷処理
        Call Me.DoPrint(ds, printKb)

        '(2012.12.26)大阪大日精化向けの仕様だったが、現在未使用の為コメント。　-- START --
        ''特殊荷主以外、スルー
        'If "00023".Equals(custCdL) = False Then
        '    Return rtnDs
        'End If

        ''印刷処理
        'Call Me.DoPrint(ds, "*")
        '(2012.12.26)大阪大日精化向けの仕様だったが、現在未使用の為コメント。　-- START --

        Return rtnDs

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionStr">メソッド名</param>
    ''' <param name="selectFlg">再検索フラグ</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SimpleScopeStartEnd(ByVal ds As DataSet, ByVal actionStr As String, Optional ByVal selectFlg As Boolean = True) As DataSet

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'BLCアクセス
            ds = Me.BlcAccess(ds, actionStr)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionStr">メソッド名</param>
    ''' <param name="selectFlg">再検索フラグ</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ScopeStartEnd(ByVal ds As DataSet, ByVal actionStr As String, Optional ByVal selectFlg As Boolean = True) As DataSet

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'BLCアクセス
            ds = Me.BlcAccess(ds, actionStr)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If (("InsertSaveAction").Equals(actionStr) = True OrElse _
                ("UpdateSaveAction").Equals(actionStr) = True) AndAlso _
                0 < ds.Tables("LMC020_UNSO_L").Rows.Count Then

                If rtnResult = True AndAlso LMConst.FLG.ON.Equals(ds.Tables("LMC020_UNSO_L").Rows(0).Item("TORIKESI_FLG")) = False Then

                    If ("10").Equals(ds.Tables("LMC020_UNSO_L").Rows(0).Item("UNSO_TEHAI_KB").ToString) = True Then
                        '日陸手配の時のみ運賃データを作成

                        'データセット設定
                        Dim unchinDs As DataSet = Me.SetUnchinInDataSet(ds)

                        'BLCアクセス
                        unchinDs = Me.UnchinBlcAccess(unchinDs, "CreateUnchinData")

                        'LMF800の戻り値判定
                        Dim rtnResultDt As DataTable = unchinDs.Tables("LMF800RESULT")
                        Dim rtnResultDr As DataRow = rtnResultDt.Rows(0)

                        If ("00").Equals(rtnResultDr.Item("STATUS").ToString) = True OrElse _
                            ("05").Equals(rtnResultDr.Item("STATUS").ToString) = True OrElse _
                            ("30").Equals(rtnResultDr.Item("STATUS").ToString) = True Then
                            '正常の場合は保存処理

                            'BLCアクセス
                            unchinDs.Tables.Add(ds.Tables("LMC020_UNSO_L").Copy)
                            unchinDs.Tables.Add(ds.Tables("LMC020_OUTKA_L").Copy)
                            unchinDs.Tables.Add(ds.Tables("LMC020_UNCHIN_SKYU_DATE").Copy)
                            unchinDs = Me.BlcAccess(unchinDs, "SetUnchinInsData")

                            'エラーがあるかを判定
                            rtnResult = Not MyBase.IsMessageExist()

                            If rtnResult = True Then

                                'START UMANO 要望番号1302 支払運賃に伴う修正。
                                'データセット設定
                                Dim shiharaiDs As DataSet = Me.SetShiharaiInDataSet(ds)

                                ' 支払運賃を登録できる営業所であるか判定結果を取得
                                Dim isSkipInsertShiharai As Boolean = Me.IsSkipInsertShiharaiData(shiharaiDs)
                                Dim rtnResultDr2 As DataRow = Nothing

                                If (Not isSkipInsertShiharai) Then

                                    'BLCアクセス
                                    shiharaiDs = Me.ShiharaiBlcAccess(shiharaiDs, "CreateUnchinData")

                                    'LMF810の戻り値判定
                                    Dim rtnResultDt2 As DataTable = shiharaiDs.Tables("LMF810RESULT")
                                    rtnResultDr2 = rtnResultDt2.Rows(0)

                                    If ("00").Equals(rtnResultDr2.Item("STATUS").ToString) = True OrElse _
                                        ("05").Equals(rtnResultDr2.Item("STATUS").ToString) = True OrElse _
                                        ("30").Equals(rtnResultDr2.Item("STATUS").ToString) = True Then
                                        '正常の場合は保存処理

                                        'BLCアクセス
                                        shiharaiDs.Tables.Add(ds.Tables("LMC020_UNSO_L").Copy)
                                        shiharaiDs.Tables.Add(ds.Tables("LMC020_OUTKA_L").Copy)
                                        shiharaiDs.Tables.Add(ds.Tables("LMC020_UNCHIN_SKYU_DATE").Copy)
                                        shiharaiDs = Me.BlcAccess(shiharaiDs, "SetShiharaiInsData")

                                    End If

                                    'エラーがあるかを判定
                                    rtnResult = Not MyBase.IsMessageExist()

                                End If

                                If rtnResult = True Then

                                    'LMF800で設定された値を、再描画する時のために、運送大に設定する
                                    ds = Me.SetUnsoLDataSet(ds, unchinDs)
                                    'トランザクション終了
                                    MyBase.CommitTransaction(scope)
                                Else

                                    If (rtnResultDr2 IsNot Nothing) Then
                                        'エラーの場合はエラー処理
                                        MyBase.SetMessage(rtnResultDr2.Item("ERROR_CD").ToString, New String() {rtnResultDr2.Item("YOBI1").ToString})

                                    End If

                                    rtnResult = False

                                End If

                                'END UMANO 要望番号1302 支払運賃に伴う修正。

                            End If

                        Else
                            'エラーの場合はエラー処理
                            MyBase.SetMessage(rtnResultDr.Item("ERROR_CD").ToString, New String() {rtnResultDr.Item("YOBI1").ToString})
                            rtnResult = False

                        End If

                    Else
                        '先方手配、未定の時はトランザクション終了

                        'トランザクション終了
                        MyBase.CommitTransaction(scope)

                    End If

                Else
                    'UNSO_LのTORIKESI_FLGがTRUEの時

                    If rtnResult = True Then

                        'トランザクション終了
                        MyBase.CommitTransaction(scope)

                    End If
                End If
   
            Else

                If ("DeleteAction").Equals(actionStr) = True Then
                    '倉庫タブレット対応
                    ds = Me.CancelTabletData(ds)
                    'エラーがあるかを判定
                    rtnResult = Not MyBase.IsMessageExist()
                End If

                If rtnResult = True Then

                    'トランザクション終了
                    MyBase.CommitTransaction(scope)

                End If

            End If

        End Using

        ''更新成功の場合
        'If (selectFlg = True AndAlso rtnResult = True)  Then

        '    '登録した情報で再建策
        '    Dim inTbl As DataTable = ds.Tables("LMC020IN")
        '    inTbl.Clear()
        '    inTbl.ImportRow(ds.Tables("LMC020_OUTKA_L").Rows(0))
        '    ds = Me.SelectInitData(ds)

        'End If

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
    ''' BLCクラスアクセス（LMF800用）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UnchinBlcAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallBLC(Me._UnchinBlc, actionId, ds)

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' BLCクラスアクセス（LMF810用）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ShiharaiBlcAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallBLC(Me._ShiharaiBlc, actionId, ds)

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' サーバ日時を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetSysDateTime(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("SYS_DATETIME")
        Dim dr As DataRow = dt.NewRow()
        dr.Item("SYS_DATE") = MyBase.GetSystemDate()
        dr.Item("SYS_TIME") = MyBase.GetSystemDate()
        dt.Rows.Add(dr)
        Return ds

    End Function

#End Region

#Region "DataSet"

    ''' <summary>
    ''' 運賃INの値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetUnchinInDataSet(ByVal ds As DataSet) As DataSet

        Dim unchinDs As DataSet = New LMF800DS()
        Dim insRows As DataRow = unchinDs.Tables("LMF800IN").NewRow

        insRows.Item("WH_CD") = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("WH_CD").ToString
        insRows.Item("NRS_BR_CD") = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString
        insRows.Item("UNSO_NO_L") = ds.Tables("LMC020_UNSO_L").Rows(0).Item("UNSO_NO_L").ToString

        'データセットに追加
        unchinDs.Tables("LMF800IN").Rows.Add(insRows)

        Return unchinDs

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 支払運賃INの値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetShiharaiInDataSet(ByVal ds As DataSet) As DataSet

        Dim unchinDs As DataSet = New LMF810DS()
        Dim insRows As DataRow = unchinDs.Tables("LMF810IN").NewRow

        insRows.Item("WH_CD") = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("WH_CD").ToString
        insRows.Item("NRS_BR_CD") = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString
        insRows.Item("UNSO_NO_L") = ds.Tables("LMC020_UNSO_L").Rows(0).Item("UNSO_NO_L").ToString

        'データセットに追加
        unchinDs.Tables("LMF810IN").Rows.Add(insRows)

        Return unchinDs

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等


    ''' <summary>
    ''' LMF800で設定された値を、再描画する時のために、運送大に設定する
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetUnsoLDataSet(ByVal ds As DataSet, ByVal unchinDs As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LMC020_UNSO_L")
        Dim unchinDt As DataTable = unchinDs.Tables("F_UNCHIN_TRS")

        Dim dr As DataRow = dt.Rows(0)
        Dim unchinDr As DataRow = unchinDt.Rows(0)

        dr.Item("KYORI") = unchinDr.Item("SEIQ_KYORI")
        dr.Item("SEIQ_FIXED_FLAG") = unchinDr.Item("SEIQ_FIXED_FLAG")

        Return ds

    End Function

#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 保存処理時の印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SavePrintData(ByVal ds As DataSet) As DataSet

        ds.Tables(LMConst.RD).Clear()

        Dim dr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)

        '######## 倉庫マスタ（SOKO_MST）の出荷指図書印刷（CHECKP_11）に"1"が設定されていて、
        '######## 中レコードが削除された場合、出荷取消連絡票（中削除）(LMC600)の印刷
        Dim drm As DataRow() = Nothing
        drm = ds.Tables("LMC020_OUTKA_M").Select(String.Concat("NRS_BR_CD = '", dr.Item("NRS_BR_CD").ToString(), "' AND SYS_DEL_FLG = 1 "))
        If 0 < drm.Length Then
            'START YANAI 要望番号394
            'If "01".Equals(dr.Item("OUTKA_SASHIZU_PRT_YN").ToString()) = True Then
            '    Call Me.DoPrint(ds, "10")
            'End If
            Call Me.DoPrint(ds, "10")
            'END YANAI 要望番号394
        End If

        '######## 倉庫マスタ（SOKO_MST）の出荷指図書印刷（CHECKP_11）に"1"が設定されていて、
        '######## 出荷データ（大）のピッキングリスト区分（PICK_KB)が"01"の場合、出荷指図書(LMC520)の印刷処理を行う。
        'START YANAI 要望番号394
        'If "01".Equals(dr.Item("PICK_KB").ToString()) = True AndAlso _
        '    "01".Equals(dr.Item("OUTKA_SASHIZU_PRT_YN").ToString()) = True AndAlso _
        '    (dr.Item("OUTKA_STATE_KB_OLD").ToString().Equals(dr.Item("OUTKA_STATE_KB").ToString()) = False AndAlso _
        '    "50".Equals(dr.Item("OUTKA_STATE_KB").ToString()) = True) Then
        '    '印刷処理
        '    Call Me.DoPrint(ds, "07")
        'End If
        drm = ds.Tables("LMC020_OUTKA_M").Select(String.Concat("NRS_BR_CD = '", dr.Item("NRS_BR_CD").ToString(), "' AND SYS_DEL_FLG = 0 "))
        Dim max As Integer = drm.Length - 1
        Dim chkFlg As Boolean = True
        For i As Integer = 0 To max
            If ("0").Equals(drm(i).Item("BACKLOG_NB").ToString) = False OrElse _
                ("0.000").Equals(drm(i).Item("BACKLOG_QT").ToString) = False Then
                chkFlg = False
                Exit For
            End If
        Next
        If "01".Equals(dr.Item("PICK_KB").ToString()) = True AndAlso _
            (dr.Item("OUTKA_STATE_KB_OLD").ToString().Equals(dr.Item("OUTKA_STATE_KB").ToString()) = False AndAlso _
            chkFlg = True) Then
            '印刷処理
#If False Then      'UPD 2018/12/07 依頼番号 : 003561   【LMS】横浜BC_アクサルタ出荷指図書＿棟ごと印刷網掛け+立合書は通常通り印刷
             Call Me.DoPrint(ds, "07")
#Else
            Call Me.DoPrint(ds, "07", "1")
#End If
        End If
        'END YANAI 要望番号394

        '######## 埼玉の倉庫コード"510"で、出荷指図書(LMC520)が初回印刷の時
        '######## 倉庫マスタの分析表DB有無（CHECKP_5）が"1"、商品マスタの分析表区分が"1"、画面出荷（大）の「分析表添付」が設定されている場合
        '######## TODO : 分析表を出力（仕様未確定）
        If "510".Equals(dr.Item("WH_CD").ToString()) = False Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 印刷のみ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        ds.Tables(LMConst.RD).Clear()

        Dim dr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)

        '印刷処理
        Call Me.DoPrint(ds, dr.Item("PRINT_KB").ToString())

        '(2012.12.26)大阪大日精化向けの仕様だったが、現在未使用の為コメント。　-- START --
        ''特殊荷主以外、スルー
        'If "00023".Equals(dr.Item("CUST_CD_L").ToString()) = False Then
        '    Return ds
        'End If

        ''印刷処理
        'Call Me.DoPrint(ds, "*")
        '(2012.12.26)大阪大日精化向けの仕様だったが、現在未使用の為コメント。　--  END  --

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="printKb">印刷種別</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet, ByVal printKb As String, _
                             Optional ByVal updateFlg As String = "0") As DataSet

        Dim setDs As DataSet() = Nothing
        Dim meitetsuDs As DataSet = Nothing         'ADD 2017/07/20
        Dim prtBlc As Com.Base.BaseBLC() = Nothing
        Dim nrsBrCd As String = ds.Tables("LMC020_OUTKA_L").Rows(0)("NRS_BR_CD").ToString()
        Dim custCdL As String = ds.Tables("LMC020_OUTKA_L").Rows(0)("CUST_CD_L").ToString()
        'START YANAI 要望番号497
        'Dim nihudaYn As String = ds.Tables("LMC020_UNSO_L").Rows(0)("NIHUDA_YN").ToString()
        Dim nihudaYn As String = "00"
        'END YANAI 要望番号497
        Dim outkaPkgNb As String = ds.Tables("LMC020_OUTKA_L").Rows(0)("OUTKA_PKG_NB").ToString()
        Dim stateKb As String = ds.Tables("LMC020_OUTKA_L").Rows(0)("OUTKA_STATE_KB").ToString()
        'START YANAI 20120122 立会書印刷対応
        Dim tachiaiFlg As String = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("TACHIAI_FLG").ToString()
        'END YANAI 20120122 立会書印刷対応
        'START YANAI 要望番号497
        'Dim nihudaYn As String = ds.Tables("LMC020_UNSO_L").Rows(0)("NIHUDA_YN").ToString()
        If 0 < ds.Tables("LMC020_UNSO_L").Rows.Count Then
            nihudaYn = ds.Tables("LMC020_UNSO_L").Rows(0)("NIHUDA_YN").ToString()
        End If
        'END YANAI 要望番号497

        Dim meitetsuFlg As String = ds.Tables("LMC020_OUTKA_L").Rows(0)("MEITETSU_FLG").ToString()      'ADD 2017/07/19 

        Select Case printKb

            Case "01" '荷札

#If True Then  '名鉄専用時処理追加 ADD 2017/07/19
                If ("1").Equals(meitetsuFlg) = True Then
                    '名鉄専用
                    '**********************************************************************************
                    '* LMC789BLC    : 名鉄・荷札
                    '* LMC794BLC    : 名鉄・送り状
                    '*   DoPrint は、データセレクトはしないで印刷処理のため
                    '*　 ここでデータセレクトしDSにセットする。以降は共通処理を行う　　　
                    '*
                    '*　※LMC010BLF PrintMeitetuReport(名鉄帳票印刷(荷札+送状) 参照
                    '**********************************************************************************

                    meitetsuDs = New LMC794DS

                    prtBlc = New Com.Base.BaseBLC() {New LMC794BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printKb)}

                    meitetsuDs.Merge(setDs(0))

                    meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                    meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                    meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                    meitetsuDs = PrintMeitetuReport(meitetsuDs)

                    ' 荷札用入力データ
                    meitetsuDs = Me.SetDataSetLMC789InData(meitetsuDs, "LMC794OUT")

                    '荷札用に再設定
                    prtBlc = New Com.Base.BaseBLC() {New LMC789BLC()}

                    setDs(0).Merge(meitetsuDs)

                Else
                    prtBlc = New Com.Base.BaseBLC() {New LMC550BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC550InData(ds, printKb)}

                End If
#Else
                 prtBlc = New Com.Base.BaseBLC() {New LMC550BLC()}
                 setDs = New DataSet() {Me.SetDataSetLMC550InData(ds, printKb)}

#End If
            Case "02" '送状

#If True Then  '名鉄専用時処理追加 ADD 2017/07/19
                If ("1").Equals(meitetsuFlg) = True Then
                    '名鉄専用
                    meitetsuDs = New LMC794DS

                    prtBlc = New Com.Base.BaseBLC() {New LMC794BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printKb)}

                    meitetsuDs.Merge(setDs(0))

                    meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                    meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                    meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                    meitetsuDs = PrintMeitetuReport(meitetsuDs)

                    setDs(0).Merge(meitetsuDs)

                Else
                    prtBlc = New Com.Base.BaseBLC() {New LMC560BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC560InData(ds)}
                End If
#Else
                                        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC560InData(ds)}
#End If
            Case "03" '納品書

                '====2012/07/10 Notes749,911　立会書(LMC640)を呼ばないようにコメントアウト 開始====

                'If nrsBrCd.Equals("40") = True AndAlso custCdL.Equals("00237") = True Then
                '    '営業所コード：'40'（YCC）荷主コード大：'00237'（サクラ）の場合
                '    '送信案内書(YCCサクラ用)(LMC540)と受領書(YCCサクラ用)(LMC541)を印刷
                '    'START YANAI 20120122 立会書印刷対応
                '    'prtBlc = New Com.Base.BaseBLC() {New LMC540BLC(), New LMC541BLC()}
                '    'setDs = New DataSet() {Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}
                '    If ("01").Equals(tachiaiFlg) = True Then
                '        '立会書を同時に印刷する
                '        prtBlc = New Com.Base.BaseBLC() {New LMC540BLC(), New LMC541BLC(), New LMC640BLC()}
                '        setDs = New DataSet() {Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC640InData(ds)}
                '    Else
                '        '立会書の印刷は行わない
                '        prtBlc = New Com.Base.BaseBLC() {New LMC540BLC(), New LMC541BLC()}
                '        setDs = New DataSet() {Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}
                '    End If
                '    'END YANAI 20120122 立会書印刷対応
                'Else
                '    'START YANAI 20120122 立会書印刷対応
                '    'prtBlc = New Com.Base.BaseBLC() {New LMC500BLC()}
                '    'setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printKb)}
                '    If ("01").Equals(tachiaiFlg) = True Then
                '        '立会書を同時に印刷する
                '        prtBlc = New Com.Base.BaseBLC() {New LMC500BLC(), New LMC640BLC()}
                '        setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC640InData(ds)}
                '    Else
                '        '立会書の印刷は行わない
                '        prtBlc = New Com.Base.BaseBLC() {New LMC500BLC()}
                '        setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printKb)}
                '    End If
                '    'END YANAI 20120122 立会書印刷対応
                'End If

                '====2012/07/10 Notes749,911　立会書(LMC640)を呼ばないようにコメントアウト　終了====

                '====2012/07/10 Notes749,911　立会書(LMC640)対策版 開始====

                If nrsBrCd.Equals("40") = True AndAlso custCdL.Equals("00237") = True Then
                    '営業所コード：'40'（YCC）荷主コード大：'00237'（サクラ）の場合
                    '送信案内書(YCCサクラ用)(LMC540)と受領書(YCCサクラ用)(LMC541)を印刷
                    prtBlc = New Com.Base.BaseBLC() {New LMC540BLC(), New LMC541BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}

                ElseIf nrsBrCd.Equals("10") = True AndAlso custCdL.Equals("00660") = True Then
                    '20170411 千葉ｻｸﾗ開始に伴い追記
                    '営業所コード：'10'（YCC）荷主コード大：'00237'（サクラ）の場合
                    '送信案内書(YCCサクラ用)(LMC540)と受領書(YCCサクラ用)(LMC541)を印刷
                    prtBlc = New Com.Base.BaseBLC() {New LMC540BLC(), New LMC541BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}
                Else
                    '立会書の印刷は行わない
                    prtBlc = New Com.Base.BaseBLC() {New LMC500BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printKb)}
                End If

                '====2012/07/10 Notes749,911　立会書(LMC640)対策版　終了====

            Case "04" '仮納品書				

                prtBlc = New Com.Base.BaseBLC() {New LMC500BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printKb)}

            Case "05" '分析票				

                'クライアント処理の為、記載なし

            Case "06" '出荷報告				

                prtBlc = New Com.Base.BaseBLC() {New LMC610BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC610InData(ds)}

            Case "07" '出荷指図				

                '====2012/08/05 群馬物流センター 現場用・事務所用2枚だし対応 開始====
                If nrsBrCd.Equals("30") = True AndAlso custCdL.Equals("00021") = True Then
                    '営業所コード：'30'（群馬）荷主コード大：'00021'（篠崎運送）の場合
                    '出荷指図書(現場用)(LMC500)と出荷指図書(事務所用)(LMC531)を印刷
                    prtBlc = New Com.Base.BaseBLC() {New LMC520BLC(), New LMC531BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC520InData(ds), Me.SetDataSetLMC531InData(ds)}
#If False Then  'DEL 2019/03/08 依頼番号 : 004623   【LMS】群馬日立FN_荷主コード:00072 荷主名:日立物流ファインネクスト トートタンクにて、出荷指図書が出ず確認票だけ出る
                ElseIf nrsBrCd.Equals("30") = True AndAlso custCdL.Equals("00072") = True Then
                    '営業所コード：'30'（群馬）荷主コード大：'00076'（DIC）の場合
                    '出荷指図書(現場用)(LMC500)と出荷指図書(事務所用)(LMC533)を印刷
                    'prtBlc = New Com.Base.BaseBLC() {New LMC520BLC(), New LMC531BLC()}
                    'setDs = New DataSet() {Me.SetDataSetLMC520InData(ds), Me.SetDataSetLMC531InData(ds)}
                    prtBlc = New Com.Base.BaseBLC() {New LMC531BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC531InData(ds)}
#End If
                ElseIf nrsBrCd.Equals("30") = True AndAlso custCdL.Equals("00076") = True Then
                    '営業所コード：'30'（群馬）荷主コード大：'00076'（DIC）の場合
                    '出荷指図書(現場用)(LMC500)と出荷指図書(事務所用)(LMC533)を印刷
                    'prtBlc = New Com.Base.BaseBLC() {New LMC520BLC(), New LMC531BLC()}
                    'setDs = New DataSet() {Me.SetDataSetLMC520InData(ds), Me.SetDataSetLMC531InData(ds)}
                    prtBlc = New Com.Base.BaseBLC() {New LMC531BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC531InData(ds)}
                    'ADD 2017/05/17 群馬山九対応 三井化学ＳＫＣポリウレタン(10001)も追加
                ElseIf nrsBrCd.Equals("30") = True AndAlso
                       (custCdL.Equals("00001") = True Or custCdL.Equals("10001") = True) Then
                    '営業所コード：'30'（群馬）荷主コード大：'00001'（山九）,'10001'(三井化学ＳＫＣポリウレタン)の場合
                    '出荷指図書(LMC850)と立会伝票(LMC851)を印刷
                    prtBlc = New Com.Base.BaseBLC() {New LMC520BLC(), New LMC520BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC520InData(ds), Me.SetDataSetLMC520InData(ds)}
                    '立会伝票用編集
                    If setDs(1).Tables("LMC520IN").Rows(0).Item("SAIHAKKO_FLG").Equals("0") = True Then
                        setDs(1).Tables("LMC520IN").Rows(0).Item("SAIHAKKO_FLG") = "2"
                    ElseIf setDs(1).Tables("LMC520IN").Rows(0).Item("SAIHAKKO_FLG").Equals("1") = True Then
                        setDs(1).Tables("LMC520IN").Rows(0).Item("SAIHAKKO_FLG") = "3"
                    End If
#If True Then   'ADD 2018/12/07 依頼番号 : 003561   【LMS】横浜BC_アクサルタ出荷指図書＿棟ごと印刷網掛け+立合書は通常通り印刷
                ElseIf nrsBrCd.Equals("40") = True AndAlso
                        custCdL.Equals("00588") = True AndAlso
                       ("1").Equals(updateFlg) Then
                    'アクサルタの登録時
                    prtBlc = New Com.Base.BaseBLC() {New LMC520BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC520InDataToroku(ds, updateFlg)}
#End If
                Else
                    prtBlc = New Com.Base.BaseBLC() {New LMC520BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC520InData(ds)}
                End If

                'prtBlc = New Com.Base.BaseBLC() {New LMC520BLC()}
                'setDs = New DataSet() {Me.SetDataSetLMC520InData(ds)}

                '====2012/08/05 群馬物流センター 現場用・事務所用2枚だし対応 終了====

            Case "08" '一括印刷

                '分析表,送状,納品書,荷札
                '====2012/07/10 Notes749,911　立会書(LMC640)を呼ばないようにコメントアウト 開始====
                'If nrsBrCd.Equals("40") = True AndAlso custCdL.Equals("00237") = True Then
                '    'START YANAI 20120122 立会書印刷対応
                '    ''運送会社マスタの荷札有無フラグが"01"(有)の場合は、荷札印刷
                '    'If nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = True Then
                '    '    prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC()}
                '    '    setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}

                '    'ElseIf nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = False Then
                '    '    prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC550BLC()}
                '    '    setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC550InData(ds, printKb)}

                '    'ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = True Then
                '    '    prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC()}
                '    '    setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}

                '    'ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = False Then
                '    '    prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC()}
                '    '    setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}
                '    'Else
                '    '    '基本的に上記のどれかに引っかかるので、elseにくることはない。
                '    '    prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC()}
                '    '    setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}
                '    'End If
                '    '運送会社マスタの荷札有無フラグが"01"(有)の場合は、荷札印刷
                '    If nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = True Then
                '        If ("01").Equals(tachiaiFlg) = True Then
                '            '立会書を同時に印刷する
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC640BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC640InData(ds)}
                '        Else
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}
                '        End If

                '    ElseIf nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = False Then
                '        If ("01").Equals(tachiaiFlg) = True Then
                '            '立会書を同時に印刷する
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC550BLC(), New LMC640BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC550InData(ds, printKb), Me.SetDataSetLMC640InData(ds)}
                '        Else
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC550BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC550InData(ds, printKb)}
                '        End If

                '    ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = True Then
                '        If ("01").Equals(tachiaiFlg) = True Then
                '            '立会書を同時に印刷する
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC640BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC640InData(ds)}
                '        Else
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}
                '        End If

                '    ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = False Then
                '        If ("01").Equals(tachiaiFlg) = True Then
                '            '立会書を同時に印刷する
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC640BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC640InData(ds)}
                '        Else
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}
                '        End If
                '    Else
                '        If ("01").Equals(tachiaiFlg) = True Then
                '            '立会書を同時に印刷する
                '            '基本的に上記のどれかに引っかかるので、elseにくることはない。
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC640BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC640InData(ds)}
                '        Else
                '            '基本的に上記のどれかに引っかかるので、elseにくることはない。
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}

                '        End If
                '    End If
                '    'END YANAI 20120122 立会書印刷対応
                'Else
                '    '運送会社マスタの荷札有無フラグが"01"(有)の場合は、荷札印刷
                '    'START YANAI 20120122 立会書印刷対応
                '    'If nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = True Then
                '    '    prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC()}
                '    '    setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb)}
                '    'ElseIf nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = False Then
                '    '    prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC550BLC()}
                '    '    setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC550InData(ds, printKb)}
                '    'ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = True Then
                '    '    prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC()}
                '    '    setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb)}
                '    'ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = False Then
                '    '    prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC()}
                '    '    setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb)}
                '    'Else
                '    '    prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC()}
                '    '    setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb)}
                '    'End If
                '    If nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = True Then
                '        If ("01").Equals(tachiaiFlg) = True Then
                '            '立会書を同時に印刷する
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC640BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC640InData(ds)}
                '        Else
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb)}
                '        End If
                '    ElseIf nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = False Then
                '        If ("01").Equals(tachiaiFlg) = True Then
                '            '立会書を同時に印刷する
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC550BLC(), New LMC640BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC550InData(ds, printKb), Me.SetDataSetLMC640InData(ds)}
                '        Else
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC550BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC550InData(ds, printKb)}
                '        End If
                '    ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = True Then
                '        If ("01").Equals(tachiaiFlg) = True Then
                '            '立会書を同時に印刷する
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC640BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC640InData(ds)}
                '        Else
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb)}
                '        End If
                '    ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = False Then
                '        If ("01").Equals(tachiaiFlg) = True Then
                '            '立会書を同時に印刷する
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC640BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC640InData(ds)}
                '        Else
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb)}
                '        End If
                '    Else
                '        If ("01").Equals(tachiaiFlg) = True Then
                '            '立会書を同時に印刷する
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC640BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC640InData(ds)}
                '        Else
                '            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC()}
                '            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb)}
                '        End If
                '    End If
                '    'END YANAI 20120122 立会書印刷対応
                'End If

                '====2012/07/10 Notes749,911　立会書(LMC640)を呼ばないようにコメントアウト　終了====

                '====2012/07/10 Notes749,911　立会書(LMC640)対策版 開始====

                '分析表,送状,納品書,荷札

#If False Then     'UPD 2017/07/20 名鉄処理前
                    If nrsBrCd.Equals("40") = True AndAlso custCdL.Equals("00237") = True Then
                        '運送会社マスタの荷札有無フラグが"01"(有)の場合は、荷札印刷
                        If nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = True Then
                            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}

                        ElseIf nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = False Then
                            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC550BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC550InData(ds, printKb)}

                        ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = True Then
                            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}

                        ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = False Then
                            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}

                        Else
                            '基本的に上記のどれかに引っかかるので、elseにくることはない。
                            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}
                        End If
                Else
                    If nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = True Then
                        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC()}
                        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb)}

                    ElseIf nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = False Then
                        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC550BLC()}
                        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC550InData(ds, printKb)}

                    ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = True Then
                        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC()}
                        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb)}

                    ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = False Then
                        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC()}
                        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb)}

                    Else
                        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC()}
                        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb)}
                    End If
#End If

                '**************************************************
                '   一括に運送保険を追加　 LMC870　ADD 2018/07/24 依頼番号 : 001540  
                '**************************************************

#If True Then     'UPD 2017/07/20 名鉄処理後
                If nrsBrCd.Equals("40") = True AndAlso custCdL.Equals("00237") = True Then
                    '運送会社マスタの荷札有無フラグが"01"(有)の場合は、荷札印刷
                    If nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = True Then
                        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC870BLC()}
                        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC870InData(ds)}

                    ElseIf nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = False Then
                        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC550BLC(), New LMC870BLC()}
                        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC550InData(ds, printKb), Me.SetDataSetLMC870InData(ds)}

                    ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = True Then
                        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC870BLC()}
                        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC870InData(ds)}

                    ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = False Then
                        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC870BLC()}
                        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC870InData(ds)}

                    Else
                        '基本的に上記のどれかに引っかかるので、elseにくることはない。
                        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC870BLC()}
                        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC870InData(ds)}
                    End If

                    'ADD 2022/04/20 028723 千葉専用追加 開始　**********************************************************************************************
                ElseIf nrsBrCd.Equals("10") = True Then
                    If nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = True Then
                        If ("1").Equals(meitetsuFlg) = True Then
                            '名鉄専用  荷札 560の変更
                            prtBlc = New Com.Base.BaseBLC() {New LMC500BLC(), New LMC520BLC(), New LMC794BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC520InData(ds), Me.SetDataSetLMC794InData(ds, printKb)}
                            ''prtBlc = New Com.Base.BaseBLC() {New LMC794BLC(), New LMC500BLC(), New LMC870BLC()}
                            ''setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printKb), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC870InData(ds)}

                            meitetsuDs = New LMC794DS

                            meitetsuDs.Merge(setDs(2))　' 送状を設定

                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                            meitetsuDs = PrintMeitetuReport(meitetsuDs)

                            setDs(2) = meitetsuDs.Copy

                        Else
                            prtBlc = New Com.Base.BaseBLC() {New LMC500BLC(), New LMC520BLC(), New LMC560BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC520InData(ds), Me.SetDataSetLMC560InData(ds)}
                        End If


                    ElseIf nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = False Then

                        If ("1").Equals(meitetsuFlg) = True Then
                            '名鉄専用  荷札 560の変更
                            prtBlc = New Com.Base.BaseBLC() {New LMC794BLC(), New LMC500BLC(), New LMC794BLC(), New LMC520BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printKb), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC794InData(ds, printKb), Me.SetDataSetLMC520InData(ds)}
                            ''prtBlc = New Com.Base.BaseBLC() {New LMC794BLC(), New LMC500BLC(), New LMC794BLC(), New LMC870BLC()}
                            ''setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printKb), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC794InData(ds, printKb), Me.SetDataSetLMC870InData(ds)}

                            meitetsuDs = New LMC794DS
                            meitetsuDs.Merge(setDs(0))

                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                            meitetsuDs = PrintMeitetuReport(meitetsuDs)
                            setDs(0) = meitetsuDs.Copy

                            '名鉄専用 送状 550の変更
                            meitetsuDs = New LMC794DS

                            prtBlc(2) = New LMC794BLC()
                            setDs(2) = Me.SetDataSetLMC794InData(ds, printKb)

                            meitetsuDs.Merge(setDs(2))

                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                            meitetsuDs = PrintMeitetuReport(meitetsuDs)
                            ' 荷札用入力データ
                            meitetsuDs = Me.SetDataSetLMC789InData(meitetsuDs, "LMC794OUT")

                            '荷札用に再設定
                            prtBlc(2) = New LMC789BLC()
                            setDs(2) = meitetsuDs.Copy

                        Else
                            prtBlc = New Com.Base.BaseBLC() {New LMC500BLC(), New LMC520BLC(), New LMC560BLC(), New LMC550BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC520InData(ds), Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC550InData(ds, printKb)}
                        End If

                    ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = True Then
                        If ("1").Equals(meitetsuFlg) = True Then
                            '名鉄専用  荷札 560の変更
                            prtBlc = New Com.Base.BaseBLC() {New LMC500BLC(), New LMC520BLC(), New LMC794BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC520InData(ds), Me.SetDataSetLMC794InData(ds, printKb)}
                            'prtBlc = New Com.Base.BaseBLC() {New LMC794BLC(), New LMC500BLC(), New LMC870BLC()}
                            'setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printKb), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC870InData(ds)}


                            meitetsuDs = New LMC794DS
                            meitetsuDs.Merge(setDs(2))

                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                            meitetsuDs = PrintMeitetuReport(meitetsuDs)

                            setDs(2) = meitetsuDs.Copy
                        Else
                            prtBlc = New Com.Base.BaseBLC() {New LMC500BLC(), New LMC520BLC(), New LMC560BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC520InData(ds), Me.SetDataSetLMC560InData(ds)}
                        End If

                    ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = False Then
                        If ("1").Equals(meitetsuFlg) = True Then
                            '名鉄専用  荷札 560の変更
                            prtBlc = New Com.Base.BaseBLC() {New LMC500BLC(), New LMC520BLC(), New LMC794BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC520InData(ds), Me.SetDataSetLMC794InData(ds, printKb)}
                            'prtBlc = New Com.Base.BaseBLC() {New LMC794BLC(), New LMC500BLC(), New LMC870BLC()}
                            'setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printKb), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC870InData(ds)}
                            meitetsuDs = New LMC794DS
                            meitetsuDs.Merge(setDs(2))

                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                            meitetsuDs = PrintMeitetuReport(meitetsuDs)

                            setDs(2) = meitetsuDs.Copy
                        Else
                            prtBlc = New Com.Base.BaseBLC() {New LMC500BLC(), New LMC520BLC(), New LMC560BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC520InData(ds), Me.SetDataSetLMC560InData(ds)}
                        End If

                    Else
                        If ("1").Equals(meitetsuFlg) = True Then
                            '名鉄専用  荷札 560の変更
                            prtBlc = New Com.Base.BaseBLC() {New LMC500BLC(), New LMC520BLC(), New LMC794BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC520InData(ds), Me.SetDataSetLMC794InData(ds, printKb)}
                            ''prtBlc = New Com.Base.BaseBLC() {New LMC794BLC(), New LMC500BLC(), New LMC870BLC()}
                            ''setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printKb), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC870InData(ds)}

                            meitetsuDs = New LMC794DS
                            meitetsuDs.Merge(setDs(2))

                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                            meitetsuDs = PrintMeitetuReport(meitetsuDs)

                            setDs(2) = meitetsuDs.Copy
                        Else
                            prtBlc = New Com.Base.BaseBLC() {New LMC500BLC(), New LMC520BLC(), New LMC560BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC520InData(ds), Me.SetDataSetLMC560InData(ds)}
                        End If
                    End If
                    'ADD 2022/04/20 028723 千葉専用追加 終了 ********************************************************************************************


                Else
                    If nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = True Then
                        If ("1").Equals(meitetsuFlg) = True Then
                            '名鉄専用  荷札 560の変更
                            prtBlc = New Com.Base.BaseBLC() {New LMC794BLC(), New LMC500BLC(), New LMC870BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printKb), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC870InData(ds)}

                            meitetsuDs = New LMC794DS

                            meitetsuDs.Merge(setDs(0))

                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                            meitetsuDs = PrintMeitetuReport(meitetsuDs)

                            setDs(0) = meitetsuDs.Copy

                        Else
                            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC870BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC870InData(ds)}
                        End If


                    ElseIf nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = False Then

                        If ("1").Equals(meitetsuFlg) = True Then
                            '名鉄専用  荷札 560の変更
                            prtBlc = New Com.Base.BaseBLC() {New LMC794BLC(), New LMC500BLC(), New LMC794BLC(), New LMC870BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printKb), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC794InData(ds, printKb), Me.SetDataSetLMC870InData(ds)}

                            meitetsuDs = New LMC794DS
                            meitetsuDs.Merge(setDs(0))

                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                            meitetsuDs = PrintMeitetuReport(meitetsuDs)
                            setDs(0) = meitetsuDs.Copy

                            '名鉄専用 送状 550の変更
                            meitetsuDs = New LMC794DS

                            prtBlc(2) = New LMC794BLC()
                            setDs(2) = Me.SetDataSetLMC794InData(ds, printKb)

                            meitetsuDs.Merge(setDs(2))

                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                            meitetsuDs = PrintMeitetuReport(meitetsuDs)
                            ' 荷札用入力データ
                            meitetsuDs = Me.SetDataSetLMC789InData(meitetsuDs, "LMC794OUT")

                            '荷札用に再設定
                            prtBlc(2) = New LMC789BLC()
                            setDs(2) = meitetsuDs.Copy

                        Else
                            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC550BLC(), New LMC870BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC550InData(ds, printKb), Me.SetDataSetLMC870InData(ds)}
                        End If

                    ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = True Then
                        If ("1").Equals(meitetsuFlg) = True Then
                            '名鉄専用  荷札 560の変更
                            prtBlc = New Com.Base.BaseBLC() {New LMC794BLC(), New LMC500BLC(), New LMC870BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printKb), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC870InData(ds)}

                            meitetsuDs = New LMC794DS
                            meitetsuDs.Merge(setDs(0))

                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                            meitetsuDs = PrintMeitetuReport(meitetsuDs)

                            setDs(0) = meitetsuDs.Copy
                        Else
                            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC870BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC870InData(ds)}
                        End If

                    ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = False Then
                        If ("1").Equals(meitetsuFlg) = True Then
                            '名鉄専用  荷札 560の変更
                            prtBlc = New Com.Base.BaseBLC() {New LMC794BLC(), New LMC500BLC(), New LMC870BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printKb), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC870InData(ds)}

                            meitetsuDs = New LMC794DS
                            meitetsuDs.Merge(setDs(0))

                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                            meitetsuDs = PrintMeitetuReport(meitetsuDs)

                            setDs(0) = meitetsuDs.Copy
                        Else
                            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC870BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC870InData(ds)}
                        End If

                    Else
                        If ("1").Equals(meitetsuFlg) = True Then
                            '名鉄専用  荷札 560の変更
                            prtBlc = New Com.Base.BaseBLC() {New LMC794BLC(), New LMC500BLC(), New LMC870BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printKb), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC870InData(ds)}

                            meitetsuDs = New LMC794DS
                            meitetsuDs.Merge(setDs(0))

                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                            meitetsuDs = PrintMeitetuReport(meitetsuDs)

                            setDs(0) = meitetsuDs.Copy
                        Else
                            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC870BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds, printKb), Me.SetDataSetLMC870InData(ds)}
                        End If
                    End If
                End If


#End If
                '====2012/07/10 Notes749,911　立会書(LMC640対策版　終了====
            Case "09" '取消連絡				

                If ("10").Equals(stateKb) = False Then
                    '進捗区分が予定入力済("10")以外の時は印刷する。
                    prtBlc = New Com.Base.BaseBLC() {New LMC600BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC600InData(ds)}
                End If

            Case "10" '出荷取消連絡票（中削除）

                If ("10").Equals(stateKb) = False Then
                    '進捗区分が予定入力済("10")以外の時は印刷する。
                    prtBlc = New Com.Base.BaseBLC() {New LMC601BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC601InData(ds)}
                End If

                'START YANAI 20120122 立会書印刷対応
            Case "11" '立会書
                '====2012/07/10 Notes749,911　立会書(LMC640)を呼ばないようにコメントアウト　開始====
                'If ("01").Equals(tachiaiFlg) = True Then
                '    prtBlc = New Com.Base.BaseBLC() {New LMC640BLC()}
                '    setDs = New DataSet() {Me.SetDataSetLMC640InData(ds)}
                'End If
                'END YANAI 20120122 立会書印刷対応
                '====2012/07/10 Notes749,911　立会書(LMC640)を呼ばないようにコメントアウト　終了====

                '船積確認書対応 yamanaka 2012.12.03 Start
            Case "12"

                prtBlc = New Com.Base.BaseBLC() {New LMC730BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC730InData(ds)}
                '船積確認書対応 yamanaka 2012.12.03 End

            Case "*" '特殊帳票				

                prtBlc = New Com.Base.BaseBLC() {New LMC541BLC(), New LMC520BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC520InData(ds)}

            Case "13" '出荷報告(日付毎)	2012/12/26追加			
                prtBlc = New Com.Base.BaseBLC() {New LMC614BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC614InData(ds)}

                '2015.07.08 協立化学　シッピングマーク対応　追加START
            Case "14" '梱包明細
                prtBlc = New Com.Base.BaseBLC() {New LMC790BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC790InData(ds)}
                '2015.07.08 協立化学　シッピングマーク対応　追加END

                '2015.07.08 協立化学　シッピングマーク対応　追加START
            Case "15" 'ケースマーク
                prtBlc = New Com.Base.BaseBLC() {New LMC791BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC791InData(ds)}
                '2015.07.08 協立化学　シッピングマーク対応　追加END

                '2015.09.30 シッピングマーク対応　追加START
            Case "16" 'シッピングマーク
                prtBlc = New Com.Base.BaseBLC() {New LMC792BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC792InData(ds)}
                '2015.09.30 シッピングマーク対応　追加END


            Case PRINT_KB.USERS_MANUAL ' 取扱説明書
                prtBlc = New Com.Base.BaseBLC() {New LMC796BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC796InData(ds)}

                setDs(0).Tables("LMC796IN").Rows(0).Item("PRT_NB") = "1"        '枚数１を設定　現行
                setDs(0).Tables("LMC796IN").Rows(0).Item("PRT_PTN") = "1"         '取扱説明書 ADD 2018/06/07 依頼番号 : 000574 

            Case PRINT_KB.PACKAGE_DETAILS
                prtBlc = New Com.Base.BaseBLC() {New LMC651BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC651InData(ds)}

            Case PRINT_KB.USERS_MANUAL_KOSU1 ' 取扱説明書(個数１)   ADD 2018/06/07 依頼番号 : 000574 
                prtBlc = New Com.Base.BaseBLC() {New LMC796BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC796InData2(ds)}

                setDs(0).Tables("LMC796IN").Rows(0).Item("PRT_PTN") = "2"         '取扱説明書(個数１)

            Case PRINT_KB.USERS_MANUAL_HASU ' 取扱説明書(端数)     ADD 2018/06/07 依頼番号 : 000574 
                prtBlc = New Com.Base.BaseBLC() {New LMC796BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC796InData3(ds)}

                setDs(0).Tables("LMC796IN").Rows(0).Item("PRT_PTN") = "3"         '取扱説明書(端数)

#If True Then     'ADD 2018/07/18 依頼番号 : 001540  運送保険申込書
            Case PRINT_KB.UNSO_HOKEN '運送保険
                prtBlc = New Com.Base.BaseBLC() {New LMC870BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC870InData(ds)}

                setDs(0).Tables("LMC870IN").Rows(0).Item("PRT_PTN") = PRINT_KB.UNSO_HOKEN.ToString      '運送保険（一括でない）該当なしメッセージ用に設定
#End If
            Case PRINT_KB.ATTEND
                prtBlc = New Com.Base.BaseBLC() {New LMC641BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC641InData(ds)}

            Case PRINT_KB.OUTBOUND_CHECK
                prtBlc = New Com.Base.BaseBLC() {New LMC642BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC642InData(ds)}

#If True Then       'ADD 2023/03/29 送品案内書(FFEM)追加
            Case PRINT_KB.SHIPMENTGUIDE
                prtBlc = New Com.Base.BaseBLC() {New LMC543BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC543InData(ds)}
#End If

            Case PRINT_KB.DOKU_JOJU
                prtBlc = New Com.Base.BaseBLC() {New LMC901BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC901InData(ds)}

        End Select

        If prtBlc Is Nothing = True Then
            Return ds
        End If

        Dim max As Integer = prtBlc.Count - 1
        Dim rtnDs As DataSet = Nothing

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        For i As Integer = 0 To max

            If setDs Is Nothing = True Then
                Continue For
            End If

            setDs(i).Merge(New RdPrevInfoDS)
            rtnDs = MyBase.CallBLC(prtBlc(i), "DoPrint", setDs(i))

            rdPrevDt.Merge(setDs(i).Tables(LMConst.RD))

        Next

        ds.Tables(LMConst.RD).Merge(rdPrevDt)

#If True Then  'ADD 2018/08/20 依頼番号 : 001540  
        If printKb.Equals(PRINT_KB.UNSO_HOKEN) = True Then
            If ds.Tables(LMConst.RD).Rows.Count = 0 Then
                MyBase.SetMessage("E024")
            End If

        End If
#End If

        Return rtnDs

    End Function

#Region "名鉄固有処理"

    ''' <summary>
    ''' 名鉄帳票印刷(荷札+送状:UpdatePrintData亜種) LMC010BLFよりコピーし修正
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintMeitetuReport(ByVal ds As DataSet) As DataSet

        ' 検索結果取得
        Dim selectData As DataSet = Nothing
        ' まとめなし LMC794BLCでよい　LMC010BLF参照
        selectData = MyBase.CallBLC(New LMC794BLC(), "MeitetuLabel", ds)

        If (MyBase.IsMessageStoreExist() = True OrElse _
            MyBase.IsMessageExist() = True) Then
            Return ds
        End If

        Return ds

    End Function
#End Region

    ''' <summary>
    ''' LMC520DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC520InData(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)
        'START YANAI 要望番号394
        'If "01".Equals(dr.Item("OUTKA_SASHIZU_PRT_YN").ToString()) = False Then
        '    Return Nothing
        'End If
        'END YANAI 要望番号394

        Dim inDs As DataSet = Me.SetDataSetLMCInData(ds, New LMC520DS(), "LMC520IN", True)
        Dim betuFlg As String = LMConst.FLG.OFF
        If "01".Equals(dr.Item("TOU_KANRI_YN").ToString()) = True Then
            betuFlg = LMConst.FLG.ON
        End If

        If String.IsNullOrEmpty(dr.Item("SASZ_USER_OLD").ToString()) = False Then
            inDs.Tables("LMC520IN").Rows(0).Item("SAIHAKKO_FLG") = "1"
        Else
            inDs.Tables("LMC520IN").Rows(0).Item("SAIHAKKO_FLG") = "0"
        End If

        inDs.Tables("LMC520IN").Rows(0).Item("TOU_BETU_FLG") = betuFlg

        Return inDs

    End Function

#If True Then   'ADD 2018/12/07 依頼番号 : 003561   【LMS】横浜BC_アクサルタ出荷指図書＿棟ごと印刷網掛け+立合書は通常通り印刷

    ''' <summary>
    ''' LMC520DSを生成登録時
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC520InDataToroku(ByVal ds As DataSet, Optional ByVal updateFlg As String = "0") As DataSet

        Dim dr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)

        Dim inDs As DataSet = Me.SetDataSetLMCInData(ds, New LMC520DS(), "LMC520IN", True)

        If String.IsNullOrEmpty(dr.Item("SASZ_USER_OLD").ToString()) = False Then
            inDs.Tables("LMC520IN").Rows(0).Item("SAIHAKKO_FLG") = "1"
        Else
            inDs.Tables("LMC520IN").Rows(0).Item("SAIHAKKO_FLG") = "0"
        End If

        inDs.Tables("LMC520IN").Rows(0).Item("TOU_BETU_FLG") = updateFlg

        Return inDs

    End Function
#End If

    '2012.08.05 群馬 現場用・事務所用2種類同時出力対応 開始 ------
    ''' <summary>
    ''' LMC531DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC531InData(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)
        Dim inDs As DataSet = Me.SetDataSetLMCInData(ds, New LMC531DS(), "LMC531IN", True)
        Dim betuFlg As String = LMConst.FLG.OFF

        If "01".Equals(dr.Item("TOU_KANRI_YN").ToString()) = True Then
            betuFlg = LMConst.FLG.ON
        End If

        If String.IsNullOrEmpty(dr.Item("SASZ_USER_OLD").ToString()) = False Then
            inDs.Tables("LMC531IN").Rows(0).Item("SAIHAKKO_FLG") = "1"
        Else
            inDs.Tables("LMC531IN").Rows(0).Item("SAIHAKKO_FLG") = "0"
        End If

        inDs.Tables("LMC531IN").Rows(0).Item("TOU_BETU_FLG") = betuFlg

        If ("30").Equals(dr.Item("NRS_BR_CD").ToString) = True AndAlso _
                (("00072").Equals(dr.Item("CUST_CD_L").ToString) = True OrElse ("00076").Equals(dr.Item("CUST_CD_L").ToString) = True) Then
            '一括引当時、群馬物流センター DIC運送(00072,00076)の場合は2部出力
            Dim busu As Integer = 1
            If String.IsNullOrEmpty(inDs.Tables("LMC531IN").Rows(0).Item("PRT_NB").ToString) = False _
                AndAlso "0".Equals(inDs.Tables("LMC531IN").Rows(0).Item("PRT_NB").ToString) = False Then
                busu = Integer.Parse(inDs.Tables("LMC531IN").Rows(0).Item("PRT_NB").ToString)
            End If
            inDs.Tables("LMC531IN").Rows(0).Item("PRT_NB") = 2 * busu
        End If

        Return inDs

    End Function
    '2012.08.05 群馬 現場用・事務所用2種類同時出力対応 終了 ------

    '''' <summary>
    '''' 分析表の
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function SetDataSetLMC500InData(ByVal ds As DataSet) As DataSet

    '    Return Me.SetDataSetLMCInData(ds, New LMC500DS(), "LMC500IN")

    'End Function

    ''' <summary>
    ''' LMC560DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    '''<remarks></remarks>
    Private Function SetDataSetLMC560InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMC560DS(), "LMC560IN", False)

    End Function

    ''' <summary>
    ''' LMC500DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC500InData(ByVal ds As DataSet, ByVal printKb As String) As DataSet

        Dim inDs As DataSet = Me.SetDataSetLMCInData(ds, New LMC500DS(), "LMC500IN", True, printKb)
        Dim inDr As DataRow = inDs.Tables("LMC500IN").Rows(0)
        inDr.Item("PTN_FLAG") = "0"

        '(2012.03.03) 再発行フラグ制御追加 LMC513対応 -- START --
        Dim dr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)
        If String.IsNullOrEmpty(dr.Item("SASZ_USER_OLD").ToString()) = False Then
            inDs.Tables("LMC500IN").Rows(0).Item("SAIHAKKO_FLG") = "1"
        Else
            inDs.Tables("LMC500IN").Rows(0).Item("SAIHAKKO_FLG") = "0"
        End If
        '(2012.03.03) 再発行フラグ制御追加 LMC513対応 --  END --

        If printKb = "04" Then
            inDs.Tables("LMC500IN").Rows(0).Item("KARI_FLG") = "1"
        Else
            inDs.Tables("LMC500IN").Rows(0).Item("KARI_FLG") = "0"
        End If

        Return inDs

    End Function

    ''' <summary>
    ''' LMC540DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC540InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMC540DS(), "LMC540IN", True)

    End Function

    ''' <summary>
    ''' LMC541DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC541InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMC541DS(), "LMC541IN", True)

    End Function

    ''' <summary>
    ''' LMC550DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC550InData(ByVal ds As DataSet, ByVal printKb As String) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMC550DS(), "LMC550IN", False, printKb)

    End Function

    ''' <summary>
    ''' LMC600DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC600InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMC600DS(), "LMC600IN", True)

    End Function

    ''' <summary>
    ''' LMC601DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC601InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMC601DS(), "LMC601IN", True)

    End Function

    ''' <summary>
    ''' LMC794DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC794InData(ByVal ds As DataSet, ByVal printKb As String) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMC794DS(), "LMC794IN", False, printKb)

    End Function


    ''' <summary>
    ''' 荷札印刷用入力データ設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC789InData(ByVal ds As DataSet, ByVal tblNm As String) As DataSet

        Dim tagPrintInData As New LMC789DS()
        Dim tagRow As LMC789DS.LMC789OUTRow = Nothing

        Dim inTable As DataTable = ds.Tables(tblNm)
        For i As Int32 = 0 To inTable.Rows.Count - 1

            tagRow = tagPrintInData.LMC789OUT.NewLMC789OUTRow()
            tagRow.ARR_PLAN_DATE = inTable.Rows(i).Item("ARR_PLAN_DATE").ToString()
            tagRow.ARR_PLAN_TIME = inTable.Rows(i).Item("ARR_PLAN_TIME").ToString()
            tagRow.AUTO_DENP_NO = inTable.Rows(i).Item("AUTO_DENP_NO").ToString()
            tagRow.CHAKU_CD = ""
            tagRow.CUST_CD_L = inTable.Rows(i).Item("CUST_CD_L").ToString()
            tagRow.CUST_NM_L = inTable.Rows(i).Item("CUST_NM_L").ToString()
            tagRow.CYAKU_NM = ""
            tagRow.DENPYO_NM = inTable.Rows(i).Item("DENPYO_NM").ToString()
            tagRow.DENPYO_NO = inTable.Rows(i).Item("DENPYO_NO").ToString()
            tagRow.HAITATSU_KBN = inTable.Rows(i).Item("HAITATSU_KBN").ToString()
            tagRow.HAITATSU_TIME_KBN = inTable.Rows(i).Item("HAITATSU_TIME_KBN").ToString()
            tagRow.HINSYU_KEIYAKU = ""
            tagRow.HOKENRYOU = "0"
            tagRow.JIS = inTable.Rows(i).Item("JIS").ToString()
            tagRow.JYURYO = inTable.Rows(i).Item("JYURYO").ToString()
            tagRow.KIJI_1 = inTable.Rows(i).Item("KIJI_1").ToString()
            tagRow.KIJI_2 = inTable.Rows(i).Item("KIJI_2").ToString()
            tagRow.KIJI_3 = inTable.Rows(i).Item("KIJI_3").ToString()
            tagRow.KIJI_4 = inTable.Rows(i).Item("KIJI_4").ToString()
            tagRow.KIJI_5 = inTable.Rows(i).Item("KIJI_5").ToString()
            tagRow.KIJI_6 = inTable.Rows(i).Item("KIJI_6").ToString()
            tagRow.KIJI_7 = inTable.Rows(i).Item("KIJI_7").ToString()
            tagRow.KIJI_8 = inTable.Rows(i).Item("KIJI_8").ToString()
            tagRow.KOSU = inTable.Rows(i).Item("KOSU").ToString()
            tagRow.NIOKURININ_ADD1 = inTable.Rows(i).Item("NIOKURININ_ADD1").ToString()
            tagRow.NIOKURININ_ADD2 = inTable.Rows(i).Item("NIOKURININ_ADD2").ToString()
            tagRow.NIOKURININ_ADD3 = inTable.Rows(i).Item("NIOKURININ_ADD3").ToString()
            tagRow.NIOKURININ_CD = inTable.Rows(i).Item("NIOKURININ_CD").ToString()
            tagRow.NIOKURININ_MEI1 = inTable.Rows(i).Item("NIOKURININ_MEI1").ToString()
            tagRow.NIOKURININ_MEI2 = inTable.Rows(i).Item("NIOKURININ_MEI2").ToString()
            tagRow.NIOKURININ_TEL = inTable.Rows(i).Item("NIOKURININ_TEL").ToString()
            tagRow.NIOKURININ_ZIP = inTable.Rows(i).Item("NIOKURININ_ZIP").ToString()
            tagRow.NIUKENIN_ADD1 = inTable.Rows(i).Item("NIUKENIN_ADD1").ToString()
            tagRow.NIUKENIN_ADD2 = inTable.Rows(i).Item("NIUKENIN_ADD2").ToString()
            tagRow.NIUKENIN_ADD3 = inTable.Rows(i).Item("NIUKENIN_ADD3").ToString()
            tagRow.NIUKENIN_CD = inTable.Rows(i).Item("NIUKENIN_CD").ToString()
            tagRow.NIUKENIN_MEI1 = inTable.Rows(i).Item("NIUKENIN_MEI1").ToString()
            tagRow.NIUKENIN_MEI2 = inTable.Rows(i).Item("NIUKENIN_MEI2").ToString()
            tagRow.NIUKENIN_TEL = inTable.Rows(i).Item("NIUKENIN_TEL").ToString()
            tagRow.NIUKENIN_ZIP = inTable.Rows(i).Item("NIUKENIN_ZIP").ToString()
            tagRow.NRS_BR_CD = inTable.Rows(i).Item("NRS_BR_CD").ToString()
            tagRow.OUTKA_NO_L = inTable.Rows(i).Item("OUTKA_NO_L").ToString()
            tagRow.OUTKA_NO_M = ""
            tagRow.OUTKA_PLAN_DATE = inTable.Rows(i).Item("OUTKA_PLAN_DATE").ToString()
            tagRow.PAGES = ""
            tagRow.PAGES_2 = ""
            tagRow.PARETTOSU = "0"
            tagRow.ROW_NO = inTable.Rows(i).Item("ROW_NO").ToString()
            tagRow.RPT_ID = ""
            tagRow.SHIHARAININ_CD = inTable.Rows(i).Item("SHIHARAININ_CD").ToString()
            tagRow.SHIP_NM_L = inTable.Rows(i).Item("SHIP_NM_L").ToString()
            tagRow.SIWAKE_NO = ""
            tagRow.SYS_DATE = ""
            tagRow.SYS_TIME = ""
            tagRow.SYS_UPD_DATE = inTable.Rows(i).Item("SYS_UPD_DATE").ToString()
            tagRow.SYS_UPD_TIME = inTable.Rows(i).Item("SYS_UPD_TIME").ToString()
            tagRow.TEL = inTable.Rows(i).Item("TEL").ToString()
            tagRow.UNCHIN_SEIQTO_CD = inTable.Rows(i).Item("UNCHIN_SEIQTO_CD").ToString()
            tagRow.UNSOCO_BR_NM = inTable.Rows(i).Item("UNSOCO_BR_NM").ToString()
            tagRow.YOSEKI = inTable.Rows(i).Item("YOSEKI").ToString()
            tagRow.ZIP_PATTERN = inTable.Rows(i).Item("ZIP_PATTERN").ToString()

            tagPrintInData.LMC789OUT.AddLMC789OUTRow(tagRow)
        Next

        Return tagPrintInData

    End Function


    ''' <summary>
    ''' LMC610DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC610InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = Me.SetDataSetLMCInData(ds, New LMC610DS(), "LMC610IN", False)

        Dim inDr As DataRow = inDs.Tables("LMC610IN").Rows(0)
        Dim dr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)

        inDr.Item("CUST_CD_L") = dr.Item("CUST_CD_L").ToString()
        inDr.Item("CUST_CD_M") = dr.Item("CUST_CD_M").ToString()

        Return inDs

    End Function
    '▼▼▼▼ 2012/12/26　LMC614追加開始 ▼▼▼▼
    ''' <summary>
    ''' LMC614DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC614InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = Me.SetDataSetLMCInData(ds, New LMC614DS(), "LMC614IN", False)

        Dim inDr As DataRow = inDs.Tables("LMC614IN").Rows(0)
        Dim dr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)

        inDr.Item("CUST_CD_L") = dr.Item("CUST_CD_L").ToString()
        inDr.Item("CUST_CD_M") = dr.Item("CUST_CD_M").ToString()

        Return inDs

    End Function
    '▲▲▲▲ 2012/12/26　LMC614追加終了 ▲▲▲▲
    ''' <summary>
    ''' LMC730DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC730InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = Me.SetDataSetLMCInData(ds, New LMC730DS(), "LMC730IN", False)

        Dim inDr As DataRow = inDs.Tables("LMC730IN").Rows(0)
        Dim dr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)

        inDr.Item("CUST_CD_L") = dr.Item("CUST_CD_L").ToString()

        Return inDs

    End Function

    '2015.07.08 協立化学　シッピングマーク対応　追加START
    ''' <summary>
    ''' LMC790DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC790InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = Me.SetDataSetLMCInData(ds, New LMC790DS(), "LMC790IN", False)

        Dim inDr As DataRow = inDs.Tables("LMC790IN").Rows(0)
        Dim dr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)

        inDr.Item("CUST_CD_L") = dr.Item("CUST_CD_L").ToString()

        Return inDs

    End Function
    '2015.07.08 協立化学　シッピングマーク対応　追加END

    '2015.07.08 協立化学　シッピングマーク対応　追加START
    ''' <summary>
    ''' LMC791DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC791InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = Me.SetDataSetLMCInData(ds, New LMC791DS(), "LMC791IN", False)

        Dim inDr As DataRow = inDs.Tables("LMC791IN").Rows(0)
        Dim dr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)

        inDr.Item("CUST_CD_L") = dr.Item("CUST_CD_L").ToString()

        Return inDs

    End Function
    '2015.07.08 協立化学　シッピングマーク対応　追加END

    '2015.09.30 協立化学　シッピングマーク対応　追加START
    ''' <summary>
    ''' LMC792DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC792InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = Me.SetDataSetLMCInData(ds, New LMC792DS(), "LMC792IN", False)

        Dim inDr As DataRow = inDs.Tables("LMC792IN").Rows(0)
        Dim dr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)

        inDr.Item("CUST_CD_L") = dr.Item("CUST_CD_L").ToString()
        inDr.Item("PRT_NB") = dr.Item("PRT_NB").ToString()

        Return inDs

    End Function
    '2015.09.30 協立化学　シッピングマーク対応　追加END

    '====2012/07/10 Notes749,911　立会書(LMC640)を呼ばないようにコメントアウト　開始====
    ''START YANAI 20120122 立会書印刷対応
    '''' <summary>
    '''' LMC640DSを生成
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function SetDataSetLMC640InData(ByVal ds As DataSet) As DataSet

    '    Dim dr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)
    '    Dim inDs As DataSet = Me.SetDataSetLMCInData(ds, New LMC640DS(), "LMC640IN", True)
    '    Dim betuFlg As String = LMConst.FLG.OFF
    '    If "01".Equals(dr.Item("TOU_KANRI_YN").ToString()) = True Then
    '        betuFlg = LMConst.FLG.ON
    '    End If
    '    inDs.Tables("LMC640IN").Rows(0).Item("TOU_BETU_FLG") = betuFlg

    '    Return inDs

    'End Function
    'END YANAI 20120122 立会書印刷対応
    '====2012/07/10 Notes749,911　立会書(LMC640)を呼ばないようにコメントアウト　終了====
    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="nbFlg">部数設定フラグ</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMCInData(ByVal ds As DataSet, ByVal inDs As DataSet,
                                         ByVal tblNm As String, ByVal nbFlg As Boolean,
                                         Optional ByVal printKb As String = "0") As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim setDr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)
        dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
        If tblNm.Equals("LMC500IN") = True Then
            dr.Item("KANRI_NO_L") = setDr.Item("OUTKA_NO_L").ToString()
        Else
            dr.Item("OUTKA_NO_L") = setDr.Item("OUTKA_NO_L").ToString()
        End If

#If False Then 'UPD 2017/07/19 名鉄荷札設定
            If nbFlg = True Then
                dr.Item("PRT_NB") = setDr.Item("PRT_NB").ToString()
            ElseIf tblNm.Equals("LMC550IN") = True Then
                If printKb.Equals("08") = True Then
                    dr.Item("PRT_NB") = setDr.Item("OUTKA_PKG_NB").ToString()
                Else
                    dr.Item("PRT_NB") = setDr.Item("PRT_NB").ToString()
                End If
            End If
#Else
        If nbFlg = True Then
            dr.Item("PRT_NB") = setDr.Item("PRT_NB").ToString()
        ElseIf tblNm.Equals("LMC550IN") = True _
            OrElse tblNm.Equals("LMC794IN") = True Then
            If printKb.Equals("08") = True Then
                dr.Item("PRT_NB") = setDr.Item("OUTKA_PKG_NB").ToString()
                dr.Item("PRT_NB_FROM") = setDr.Item("PRT_NB_FROM").ToString()
                dr.Item("PRT_NB_TO") = setDr.Item("OUTKA_PKG_NB").ToString()
            Else
                dr.Item("PRT_NB") = setDr.Item("PRT_NB").ToString()
                dr.Item("PRT_NB_FROM") = setDr.Item("PRT_NB_FROM").ToString()
                dr.Item("PRT_NB_TO") = setDr.Item("PRT_NB_TO").ToString()
            End If
        End If
#End If
        '倉庫コードの列があれば値を格納（中部物流センター判定用に追加した列）
        If dt.Columns.Contains("WH_CD") Then
            dr.Item("WH_CD") = setDr.Item("WH_CD").ToString()
        End If

#If True Then   'ADD　2022/01/26　026303 【LMS】運送保険料システム化_実装_運送保険申込書対応_出荷
        If tblNm.Equals("LMC870IN") = True Then
            dr.Item("UNSO_TEHAI_CHK") = LMConst.FLG.ON
        End If

#End If
#If True Then   'ADD 2022/04/25 依頼番号 : 028723 千葉BC　一括印刷で印刷する帳票について

        If ("LMC500IN").Equals(tblNm) = True Then
            If printKb.Equals("08") = True _
                AndAlso ("10").Equals(setDr.Item("NRS_BR_CD").ToString()) Then
                '千葉の一括時
                dr.Item("IKKATU_FLG") = LMConst.FLG.ON
            Else
                dr.Item("IKKATU_FLG") = LMConst.FLG.OFF
            End If
        End If
#End If

        dt.Rows.Add(dr)

        Return inDs

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成(取扱説明書ラベル[東レダウ])
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC796InData(ByVal ds As DataSet) As DataSet

        Dim printData As New LMC796DS()

        Return Me.SetDataSetLMCInData(ds _
                                    , printData _
                                    , printData.LMC796IN.TableName _
                                    , False)

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成(取扱説明書(個数１)ラベル[東レダウ])
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC796InData2(ByVal ds As DataSet) As DataSet

        Dim printData As New LMC796DS()

        Return Me.SetDataSetLMCInData(ds _
                                    , printData _
                                    , printData.LMC796IN.TableName _
                                    , True)

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成(取扱説明書(端数)ラベル[東レダウ])
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC796InData3(ByVal ds As DataSet) As DataSet

        Dim printData As New LMC796DS()

        Return Me.SetDataSetLMCInData(ds _
                                    , printData _
                                    , printData.LMC796IN.TableName _
                                    , True)

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成(運送保険)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC870InData(ByVal ds As DataSet) As DataSet

        Dim printData As New LMC870DS()

        Return Me.SetDataSetLMCInData(ds _
                                    , printData _
                                    , printData.LMC870IN.TableName _
                                    , True)

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成(梱包明細)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC651InData(ByVal ds As DataSet) As DataSet

        Dim inDs As New LMC651DS()
        'Dim inDt As DataTable = inDs.Tables("LMC651IN")
        Dim inDr As LMC651DS.LMC651INRow = Nothing

        For Each dr As DataRow In ds.Tables("LMC020_OUTKA_M").Rows
            inDr = inDs.LMC651IN.NewLMC651INRow
            inDr.NRS_BR_CD = dr.Item("NRS_BR_CD").ToString
            inDr.OUTKA_NO_L = dr.Item("OUTKA_NO_L").ToString
            inDr.OUTKA_NO_M = dr.Item("OUTKA_NO_M").ToString
            inDr.PRINT_FLG = dr.Item("PRINT_FLG").ToString
            inDs.LMC651IN.AddLMC651INRow(inDr)
        Next

        Return inDs

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成(立合書)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC641InData(ByVal ds As DataSet) As DataSet

        Dim printData As New LMC641DS()

        Return Me.SetDataSetLMCInData(ds _
                                    , printData _
                                    , printData.LMC641IN.TableName _
                                    , True)

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成(出荷チェックリスト)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC642InData(ByVal ds As DataSet) As DataSet

        Dim printData As New LMC642DS()

        Return Me.SetDataSetLMCInData(ds _
                                    , printData _
                                    , printData.LMC642IN.TableName _
                                    , True)

    End Function

    'ADD 2023/03/29 送品案内書(FFEM)追加
    ''' <summary>
    ''' 印刷時に使用するDataSetを生成(送品案内書(FFEM))
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC543InData(ByVal ds As DataSet) As DataSet

        Dim printData As New LMC543DS()

        Return Me.SetDataSetLMCInData(ds _
                                    , printData _
                                    , printData.LMC543IN.TableName _
                                    , True)

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成(毒劇物譲受書(FFEM))
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC901InData(ByVal ds As DataSet) As DataSet

        Dim printData As New LMC901DS()

        Return Me.SetDataSetLMCInData(ds _
                                    , printData _
                                    , printData.LMC901IN.TableName _
                                    , True)
        Return ds
    End Function

    ''' <summary>
    ''' ログ出力
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function WritePrintLog(ByVal ds As DataSet) As DataSet

        Dim fileName As String = String.Empty

        For Each dr As DataRow In ds.Tables("LMC020IN_WRITE_LOG").Rows
            Select Case dr.Item("PRINT_KBN").ToString
                Case "5"
                    fileName = String.Concat("分析票（印刷終了）：", dr.Item("PATH").ToString)
                Case "105"
                    fileName = String.Concat("分析票（コピー元）：", dr.Item("PATH").ToString)
                Case "205"
                    fileName = String.Concat("分析票（コピー先）：", dr.Item("PATH").ToString)
            End Select
            MyBase.Logger.WriteLog(0, "LMC020BLF", "WritePrintLog", fileName)
        Next

        Return ds

    End Function

#Region "タブレット対応"

    ''' <summary>
    ''' タブレットデータのキャンセル処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CancelTabletData(ByVal ds As DataSet) As DataSet

        Dim inDs As New LMC930DS

        Dim nrsBrCd As String = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString
        Dim outkaNoL As String = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("OUTKA_NO_L").ToString

        Dim inDt As DataTable = inDs.Tables("LMC930IN")
        Dim inDr As DataRow = inDt.NewRow
        inDr.Item("NRS_BR_CD") = nrsBrCd
        inDr.Item("OUTKA_NO_L") = outkaNoL
        inDr.Item("WH_TAB_STATUS_KB") = "00"    '現場指示：未指示
        inDr.Item("PROC_TYPE") = "02"           '処理区分：削除
        inDt.Rows.Add(inDr)

        Return MyBase.CallBLC(New LMC930BLC(), LMC930BLC.FUNCTION_NM.WH_SAGYO_SHIJI_CANCEL, inDs)

    End Function

    ''' <summary>
    '''営業所がタブレット対応かどうかを判定
    ''' </summary>
    ''' <param name="nrsBrCd">String</param>
    ''' <returns>boolean</returns>
    ''' <remarks></remarks>
    Private Function IsWhTabNrsBrCd(ByVal nrsBrCd As String) As Boolean

        Dim IsWhTabNrsFlg As Boolean = False
        Dim ds As DataSet = New LMC020DS
        Dim inRow As DataRow = ds.Tables("LMC020_TABLET_IN").NewRow()

        With inRow
            .Item("NRS_BR_CD") = nrsBrCd
        End With

        ds.Tables("LMC020_TABLET_IN").Rows.Add(inRow)

        MyBase.CallBLC(_Blc, "IsWhTabNrsBrCd", ds)

        If MyBase.GetResultCount() > 0 Then
            IsWhTabNrsFlg = True
        End If


        Return IsWhTabNrsFlg

    End Function

#End Region

#End Region

#End Region

End Class
