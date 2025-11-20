' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷管理
'  プログラムID     :  LMB020BLC : 入荷データ編集
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
''' <summary>
''' LMB020BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB020BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMB020BLC = New LMB020BLC()

#End Region

#Region "Const"

    '印刷種別
    Private Const PRINT_HOUKOKUSHO As String = "01"
    Private Const PRINT_CHECKLIST As String = "02"
    Private Const PRINT_UKETSUKEHYOU As String = "03"
    '201212/12/06入荷報告チェックリスト追加
    Private Const PRINT_HOUKOKU_CHECKLIST As String = "04"
    '20012/12/10入荷確定入力モニター表
    Private Const PRINT_DECI_MONITER As String = "05"
    Private Const PRINT_HOUKOKUSHO_KAKUIN As String = "07"      'ADD 2018/11/01 依頼番号 : 002747   【LMS】入荷報告印刷_角印つけるつけないの選択機能
    Private Const PRINT_NYUKO_RENRAKUHYOU As String = "08"
    Private Const PRINT_UNSO_HOKEN As String = "09"      'ADD 2022/01/25 　026832 【LMS】運送保険料システム化_実装_運送保険申込書対応_運行・運送情報
    Private Const PRINT_CONTAINER_LAVEL As String = "10"
    '手配区分
    Private Const TEHAI_NRS As String = "10"

    Private Const TABLE_NM_INKA_L As String = "LMB020_INKA_L"
    Private Const TABLE_NM_SOKO As String = "SOKO"

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

        Return Me.BlcAccess(ds, "SelectInitData")

    End Function

    ''' <summary>
    ''' 保管・荷役料最終計算日 検索処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectHokanNiyakuCalculation(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, "SelectHokanNiyakuCalculation")

    End Function

    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    ''' <summary>
    ''' 申請外の商品保管ルール検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function getTouSituExp(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, "getTouSituExp")

    End Function
    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

    ''' <summary>
    ''' 現場作業取込
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function WHSagyoTorikomi(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, "WHSagyoTorikomi")

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

        ds = Me.ScopeStartEnd(ds, "InsertSaveAction", False, True)

        '印刷処理
        Call Me.PrintDataAction(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveAction(ByVal ds As DataSet) As DataSet

        ds = Me.ScopeStartEnd(ds, "UpdateSaveAction", False, True)

        '印刷処理
        Call Me.PrintDataAction(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 更新登録(起算日修正)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveDateAction(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, "UpdateSaveDateAction", False, False)

    End Function

    ''' <summary>
    ''' 更新登録(運送修正)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveUnsoAction(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, "UpdateSaveUnsoAction", False, True)

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteAction(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, "DeleteAction", False, False)

    End Function

#If True Then   'ADD 2020/08/06 014005   【LMS】商品マスタ_入荷仮置場機能の追加
    ''' <summary>
    ''' 荷主明細取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataGoodsMeisaiOkiba(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, "SelectDataGoodsMeisaiOkiba", False, False)

    End Function
#End If

#End Region

#Region "印刷"

    ''' <summary>
    ''' 新規登録・更新時のチェックリスト印刷処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckListPrintAction(ByVal ds As DataSet) As DataSet

        '印刷
        Return Me.DoPrint(LMB020BLF.PRINT_CHECKLIST, ds)

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function PrintAction(ByVal ds As DataSet) As DataSet

        Dim printType As String = ds.Tables("LMB020_PRINT_TYPE").Rows(0)("PRINT_TYPE").ToString()

        '排他（入荷L）
        Dim rtnResult As Boolean = Me.ActionResult(ds, "PrintChk")

        '入荷作業進捗区分設定
        ds = Me.SetStateKb(printType, ds)

        '更新
#If False Then  'ADD 2022/01/25 026832 【LMS】運送保険料システム化_実装_運送保険申込書対応_運行・運送情報
         If printType <> PRINT_NYUKO_RENRAKUHYOU Then
#Else
        If printType <> PRINT_NYUKO_RENRAKUHYOU AndAlso
                printType <> PRINT_UNSO_HOKEN Then
#End If
            If printType <> PRINT_CONTAINER_LAVEL Then
                rtnResult = rtnResult AndAlso Me.ActionResult(ds, "UpdateInkaLPrintData")
            End If
        End If

        If rtnResult = False Then
            Return ds
        End If

        '印刷
        Call Me.DoPrint(printType, ds)

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function PrintGHSAction(ByVal ds As DataSet) As DataSet

        Dim ds800 As DataSet = New LMB800DS()
        Dim dt As DataTable = ds800.Tables("LMB800IN")
        Dim dr As DataRow = Nothing

        dr = dt.NewRow()
        dr.Item("NRS_BR_CD") = ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD")
        dr.Item("INKA_NO_L") = ds.Tables("LMB020_INKA_L").Rows(0).Item("INKA_NO_L")

        dt.Rows.Add(dr)

        Dim rtnDs As DataSet = Nothing

        '検索処理
        rtnDs = MyBase.CallBLC(New LMB800BLC(), "DoPrintGHS", ds800)

        Return rtnDs

    End Function

    ''' <summary>
    ''' 印刷処理実行
    ''' </summary>
    ''' <param name="printType"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal printType As String, ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing
        Dim prtBlc As Com.Base.BaseBLC

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        Select Case printType
            Case PRINT_HOUKOKUSHO
                '入荷報告書
                Dim lmb520ds As DataSet = Me.BlcAccess(ds, "SetDataSetLMB520InData")
                prtBlc = New LMB520BLC()
                lmb520ds.Merge(New RdPrevInfoDS)
                rtnDs = MyBase.CallBLC(prtBlc, "DoPrint", lmb520ds)
                rdPrevDt.Merge(rtnDs.Tables(LMConst.RD))
            Case PRINT_CHECKLIST
                '入荷チェックリスト
                Dim lmb510ds As DataSet = Me.BlcAccess(ds, "SetDataSetLMB510InData")
                prtBlc = New LMB510BLC()
                lmb510ds.Merge(New RdPrevInfoDS)
                rtnDs = MyBase.CallBLC(prtBlc, "DoPrint", lmb510ds)
                rdPrevDt.Merge(rtnDs.Tables(LMConst.RD))
            Case PRINT_UKETSUKEHYOU
                '入荷受付表
                Dim lmb500ds As DataSet = Me.BlcAccess(ds, "SetDataSetLMB500InData")
                prtBlc = New LMB500BLC()
                lmb500ds.Merge(New RdPrevInfoDS)
                rtnDs = MyBase.CallBLC(prtBlc, "DoPrint", lmb500ds)
                rdPrevDt.Merge(rtnDs.Tables(LMConst.RD))

                '2012/12/06入荷報告チェックリスト追加
            Case PRINT_HOUKOKU_CHECKLIST
                '入荷報告チェックリスト
                Dim lmb530ds As DataSet = Me.BlcAccess(ds, "SetDataSetLMB530InData")
                prtBlc = New LMB530BLC()
                lmb530ds.Merge(New RdPrevInfoDS)
                rtnDs = MyBase.CallBLC(prtBlc, "DoPrint", lmb530ds)
                rdPrevDt.Merge(rtnDs.Tables(LMConst.RD))

            Case PRINT_DECI_MONITER
                '入荷確定入力モニター表
                Dim lmb540ds As DataSet = Me.BlcAccess(ds, "SetDataSetLMB540InData")
                prtBlc = New LMB540BLC()
                lmb540ds.Merge(New RdPrevInfoDS)
                rtnDs = MyBase.CallBLC(prtBlc, "DoPrint", lmb540ds)
                rdPrevDt.Merge(rtnDs.Tables(LMConst.RD))
#If True Then   'ADD 2018/11/01 依頼番号 : 002747   【LMS】入荷報告印刷_角印つけるつけないの選択機能
            Case PRINT_HOUKOKUSHO_KAKUIN
                '入荷報告書
                Dim lmb520ds As DataSet = Me.BlcAccess(ds, "SetDataSetLMB520InDataKakuin")
                prtBlc = New LMB520BLC()
                lmb520ds.Merge(New RdPrevInfoDS)
                rtnDs = MyBase.CallBLC(prtBlc, "DoPrint", lmb520ds)
                rdPrevDt.Merge(rtnDs.Tables(LMConst.RD))
#End If
            Case PRINT_NYUKO_RENRAKUHYOU
                '入庫連絡票
                Dim lmb550ds As DataSet = Me.BlcAccess(ds, "SetDataSetLMB550InData")
                prtBlc = New LMB550BLC()
                lmb550ds.Merge(New RdPrevInfoDS)
                rtnDs = MyBase.CallBLC(prtBlc, "DoPrint", lmb550ds)
                rdPrevDt.Merge(rtnDs.Tables(LMConst.RD))
#If True Then   'ADD 2022/01/26 026543 【LMS】運送保険料システム化_実装_運送保険申込書対応_入荷機能新規作成
            Case PRINT_UNSO_HOKEN
                '運送保険申込書
                Dim lmb560ds As DataSet = Me.BlcAccess(ds, "SetDataSetLMB560InData")
                prtBlc = New LMB560BLC()
                lmb560ds.Merge(New RdPrevInfoDS)
                rtnDs = MyBase.CallBLC(prtBlc, "DoPrint", lmb560ds)
                rdPrevDt.Merge(rtnDs.Tables(LMConst.RD))
#End If
            Case PRINT_CONTAINER_LAVEL
                'コンテナ番号ラベル
                Dim lmb570ds As DataSet = Me.BlcAccess(ds, "SetDataSetLMB570InData")
                prtBlc = New LMB570BLC()
                lmb570ds.Merge(New RdPrevInfoDS)
                rtnDs = MyBase.CallBLC(prtBlc, "DoPrint", lmb570ds)
                rdPrevDt.Merge(rtnDs.Tables(LMConst.RD))

        End Select

        ds.Tables(LMConst.RD).Merge(rdPrevDt)

        Return ds

    End Function

    ''' <summary>
    ''' 入荷作業進捗区分設定
    ''' </summary>
    ''' <param name="printType"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetStateKb(ByVal printType As String, ByVal ds As DataSet) As DataSet

        '倉庫データ取得
        ds = Me.BlcAccess(ds, "GetSokoData")

        Dim prtYn As String = String.Empty
        Dim kenpinYn As String = String.Empty

        If 0 < ds.Tables(LMB020BLF.TABLE_NM_SOKO).Rows.Count Then
            prtYn = ds.Tables(LMB020BLF.TABLE_NM_SOKO).Rows(0).Item("INKA_UKE_PRT_YN").ToString()
            kenpinYn = ds.Tables(LMB020BLF.TABLE_NM_SOKO).Rows(0).Item("INKA_KENPIN_YN").ToString()
        End If

        Dim stateKb As String = String.Empty

        '入荷作業進捗区分設定
        Select Case printType
            'START YANAI 要望番号497
            'Case PRINT_HOUKOKUSHO
            '    stateKb = "90"
            'END YANAI 要望番号497

            Case PRINT_CHECKLIST, PRINT_UKETSUKEHYOU

                If ds.Tables("LMB020_INKA_S").Rows.Count = 0 Then
                    stateKb = "10"
                ElseIf prtYn.Equals("00") = False Then
                    stateKb = "20"
                ElseIf kenpinYn.Equals("00") = False Then
                    stateKb = "30"
                Else
                    stateKb = "40"
                End If

        End Select

        If ds.Tables(LMB020BLF.TABLE_NM_INKA_L).Rows(0).Item("INKA_STATE_KB").ToString <= stateKb Then
            ds.Tables(LMB020BLF.TABLE_NM_INKA_L).Rows(0).Item("INKA_STATE_KB") = stateKb
        End If

        Return ds

    End Function

#End Region

#Region "チェック"

    ''' <summary>
    ''' 編集処理のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function EditChk(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, "EditChk")

    End Function

    ''' <summary>
    ''' 起算日修正処理のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DateEditChk(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, "DateEditChk")

    End Function

    ''' <summary>
    ''' 運送修正処理のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UnsoEditChk(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, "UnsoEditChk")

    End Function


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
#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <param name="selectFlg">再検索フラグ</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ScopeStartEnd(ByVal ds As DataSet _
                                   , ByVal actionId As String _
                                   , ByVal selectFlg As Boolean _
                                   , ByVal updateUnchin As Boolean _
                                   ) As DataSet

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '更新処理
            rtnResult = Me.SetItemData(ds, actionId, updateUnchin)
            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        '更新成功の場合
        If selectFlg = True AndAlso rtnResult = True Then

            '登録しいた情報で再建策
            Dim inTbl As DataTable = ds.Tables("LMB020IN")
            inTbl.Clear()
            inTbl.ImportRow(ds.Tables("LMB020_INKA_L").Rows(0))
            ds = Me.SelectInitData(ds)

        End If

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
    ''' BLCクラスアクセス（結果リターン）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="actionId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ActionResult(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        ds = Me.BlcAccess(ds, actionId)
        If MyBase.IsMessageExist() = True Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <param name="updateUnchin">運賃更新フラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetItemData(ByVal ds As DataSet, ByVal actionId As String, ByVal updateUnchin As Boolean) As Boolean

        '更新処理
        ds = Me.BlcAccess(ds, actionId)

        Dim rtnResult As Boolean = Not MyBase.IsMessageExist()


        '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
        'Return rtnResult AndAlso Me.SetUnchinData(ds, updateUnchin)
        Return rtnResult _
            AndAlso Me.SetUnchinData(ds, updateUnchin) _
            AndAlso Me.SetShiharaiData(ds, updateUnchin) _
            AndAlso Me.SetTabletCancelData(ds, actionId)
        '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End


    End Function

    ''' <summary>
    ''' 運賃情報の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="updateUnchin">運賃更新フラグ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetUnchinData(ByVal ds As DataSet, ByVal updateUnchin As Boolean) As Boolean

        '運賃情報を更新しない場合、スルー
        If updateUnchin = False Then
            Return True
        End If

        '運送情報がない場合、スルー
        Dim dt As DataTable = ds.Tables("LMB020_UNSO_L")
        If dt.Rows.Count < 1 Then
            Return True
        End If

        Dim inkaLDr As DataRow = ds.Tables("LMB020_INKA_L").Rows(0)

        '日陸手配の場合、プログラム起動
        If LMB020BLF.TEHAI_NRS.Equals(dt.Rows(0).Item("UNSO_TEHAI_KB").ToString()) = True Then

            Dim blc As LMF800BLC = New LMF800BLC()
            Dim inTbl As DataTable = ds.Tables("LMF800IN")
            Dim inDr As DataRow = inTbl.NewRow()
            Dim unsoDr As DataRow = dt.Rows(0)
            inDr.Item("UNCHIN") = unsoDr.Item("UNCHIN").ToString()
            inDr.Item("NRS_BR_CD") = inkaLDr.Item("NRS_BR_CD").ToString()
            inDr.Item("WH_CD") = inkaLDr.Item("WH_CD").ToString()
            inDr.Item("UNSO_NO_L") = unsoDr.Item("UNSO_NO_L").ToString()
            inTbl.Rows.Add(inDr)

            '計算処理
            ds = MyBase.CallBLC(blc, "CreateUnchinData", ds)

            'LMF800の戻り値判定
            Dim rtnResultDt As DataTable = ds.Tables("LMF800RESULT")
            Dim rtnResultDr As DataRow = rtnResultDt.Rows(0)

            If ("00").Equals(rtnResultDr.Item("STATUS").ToString) = True OrElse _
                ("05").Equals(rtnResultDr.Item("STATUS").ToString) = True OrElse _
                ("30").Equals(rtnResultDr.Item("STATUS").ToString) = True Then
                '正常の場合は保存処理

            Else
                'エラーの場合はエラー処理
                MyBase.SetMessage(rtnResultDr.Item("ERROR_CD").ToString, New String() {rtnResultDr.Item("YOBI1").ToString})
                Return Not MyBase.IsMessageExist()

            End If

        End If

        '更新処理
        ds = Me.BlcAccess(ds, "SetUnchinData")

        Return Not MyBase.IsMessageExist()

    End Function


    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
    ''' <summary>
    ''' 支払情報の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="updateShiharai">支払更新フラグ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetShiharaiData(ByVal ds As DataSet, ByVal updateShiharai As Boolean) As Boolean

        '支払情報を更新しない場合、スルー
        If updateShiharai = False Then
            Return True
        End If

        '運送情報がない場合、スルー
        Dim dt As DataTable = ds.Tables("LMB020_UNSO_L")
        If dt.Rows.Count < 1 Then
            Return True
        End If

        '運送会社が指定されていない場合、スルー
        If String.IsNullOrEmpty(dt.Rows(0).Item("UNSO_CD").ToString()) = True Then
            Return True
        End If

        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
        '運行紐付いている場合、スルー
        Dim dr As DataRow = dt.Rows(0)
        If String.IsNullOrEmpty(dr.Item("TRIP_NO").ToString()) = False OrElse _
           String.IsNullOrEmpty(dr.Item("TRIP_NO_SYUKA").ToString()) = False OrElse _
           String.IsNullOrEmpty(dr.Item("TRIP_NO_TYUKEI").ToString()) = False OrElse _
           String.IsNullOrEmpty(dr.Item("TRIP_NO_HAIKA").ToString()) = False Then
            Return True
        End If
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  


        Dim inkaLDr As DataRow = ds.Tables("LMB020_INKA_L").Rows(0)

        '日陸手配の場合、プログラム起動
        If LMB020BLF.TEHAI_NRS.Equals(dt.Rows(0).Item("UNSO_TEHAI_KB").ToString()) = True Then

            Dim blc As LMF810BLC = New LMF810BLC()
            Dim inTbl As DataTable = ds.Tables("LMF810IN")
            Dim inDr As DataRow = inTbl.NewRow()
            Dim unsoDr As DataRow = dt.Rows(0)
            inDr.Item("UNCHIN") = unsoDr.Item("UNCHIN").ToString()
            inDr.Item("NRS_BR_CD") = inkaLDr.Item("NRS_BR_CD").ToString()
            inDr.Item("WH_CD") = inkaLDr.Item("WH_CD").ToString()
            inDr.Item("UNSO_NO_L") = unsoDr.Item("UNSO_NO_L").ToString()
            inTbl.Rows.Add(inDr)

            '計算処理
            ds = MyBase.CallBLC(blc, "CreateUnchinData", ds)

            'LMF810の戻り値判定
            Dim rtnResultDt As DataTable = ds.Tables("LMF810RESULT")
            Dim rtnResultDr As DataRow = rtnResultDt.Rows(0)

            If ("00").Equals(rtnResultDr.Item("STATUS").ToString) = True OrElse _
               ("05").Equals(rtnResultDr.Item("STATUS").ToString) = True OrElse _
               ("30").Equals(rtnResultDr.Item("STATUS").ToString) = True Then
                '正常の場合は保存処理

            Else
                'エラーの場合はエラー処理
                MyBase.SetMessage(rtnResultDr.Item("ERROR_CD").ToString, New String() {rtnResultDr.Item("YOBI1").ToString})
                Return Not MyBase.IsMessageExist()

            End If

        End If

        '更新処理
        ds = Me.BlcAccess(ds, "SetShiharaiData")

        Return Not MyBase.IsMessageExist()

    End Function
    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

    ''' <summary>
    ''' タブレットデータのキャンセル処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetTabletCancelData(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        If Not "DeleteAction".Equals(actionId) Then
            Return True
        End If

        Dim inDs As New LMB810DS

        Dim nrsBrCd As String = ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString
        Dim inkaNoL As String = ds.Tables("LMB020_INKA_L").Rows(0).Item("INKA_NO_L").ToString

        Dim inDt As DataTable = inDs.Tables("LMB810IN")
        Dim inDr As DataRow = inDt.NewRow
        inDr.Item("NRS_BR_CD") = nrsBrCd
        inDr.Item("INKA_NO_L") = inkaNoL
        inDr.Item("WH_TAB_STATUS_KB") = "00"    '指示区分：未指示
        inDr.Item("PROC_TYPE") = "01"           '処理区分：削除
        inDt.Rows.Add(inDr)

        '更新処理
        ds = MyBase.CallBLC(New LMB810BLC(), LMB810BLC.FUNCTION_NM.WH_SAGYO_SHIJI_CANCEL, inDs)

        Return Not MyBase.IsMessageExist()

    End Function

    ''' <summary>
    ''' 保存印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub PrintDataAction(ByVal ds As DataSet)

        '印刷処理
        If MyBase.IsMessageExist() = False AndAlso LMConst.FLG.ON.Equals(ds.Tables("LMB020_INKA_L").Rows(0).Item("PRINT_FLG").ToString()) = True Then

            '印刷
            Call Me.DoPrint(LMB020BLF.PRINT_CHECKLIST, ds)

        End If

    End Sub

#End Region

#End Region

End Class
