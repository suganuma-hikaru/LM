' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタ
'  プログラムID     :  LMM090BLC : 荷主マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM090BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM090BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM090DAC = New LMM090DAC()

#End Region

#Region "Method"

#Region "編集処理"

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function HaitaChk(ByVal ds As DataSet) As DataSet

        '荷主マスタ排他チェック
        ds = MyBase.CallDAC(Me._Dac, "CustMHaitaChk", ds)
        If MyBase.GetResultCount() = 0 Then
            Return ds
        End If

        '2011/08/15 仕様変更　：　運賃タリフセットマスタ排他チェックなし
        'Dim inDr As DataRow = ds.Tables("LMM090IN").Rows(0)
        'If inDr.Item("CLICK_M_FLG").Equals(LMConst.FLG.ON) Then
        '    '運賃タリフセットマスタ排他チェック
        '    If ds.Tables("LMM090_TARIFF_SET").Rows.Count > 0 Then
        '        ds = MyBase.CallDAC(Me._Dac, "TariffMHaitaChk", ds)
        '    End If
        'End If

        Return ds

    End Function

    ''' <summary>
    ''' 在庫データ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ZaikoExistChk(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "ZaikoExistChk", ds)

    End Function

#End Region

#Region "削除/復活処理"

    ''' <summary>
    ''' 更新対象レコードのチェックを行う
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ChkUpdateData(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMM090IN").Rows(0)
        Dim oyaDataFlg As Boolean = dr.Item("CUST_CD_M").Equals("00") _
                            AndAlso dr.Item("CUST_CD_S").Equals("00") _
                            AndAlso dr.Item("CUST_CD_SS").Equals("00")

        If dr.Item("SYS_DEL_FLG").Equals("1") Then
            '削除時⇒削除可否チェック
            If oyaDataFlg = True Then
                ds = MyBase.CallDAC(Me._Dac, "ChkDeleteData", ds)
            End If
        Else
            'Start 要望管理2034 20130617 
            ds = MyBase.CallDAC(Me._Dac, "ChkTaihiData", ds)
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If
            'End 要望管理2034 20130617 

            '復活時⇒親レコード同時復活処理
            If oyaDataFlg = False Then
                ds = MyBase.CallDAC(Me._Dac, "ChkOyaData", ds)
                If MyBase.GetResultCount() < 1 Then
                    '各更新用DataTableに、親データを追加
                    Call Me.SetOyaData(ds)
                End If
            End If
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 荷主マスタ削除/復活処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        '荷主マスタ削除/復活
        ds = MyBase.CallDAC(Me._Dac, "DeleteCustData", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '運賃タリフセットマスタ削除/復活
        ds = MyBase.CallDAC(Me._Dac, "DeleteTariffSetData", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '荷主別帳票マスタ削除/復活
        ds = MyBase.CallDAC(Me._Dac, "DeleteCustPrtData", ds)

        '要望番号:349 yamanaka 2012.07.19 Start
        '荷主別帳票マスタ削除/復活
        ds = MyBase.CallDAC(Me._Dac, "DeleteCustDetailData", ds)
        '要望番号:349 yamanaka 2012.07.19 End

        Return ds

    End Function

    ''' <summary>
    ''' 各更新用DataTableに、親データを追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetOyaData(ByVal ds As DataSet)

        Dim tableNmIn As String = "LMM090IN"

        Dim dr As DataRow = ds.Tables("LMM090IN").Rows(0)
        Dim loginBrCd As String = dr.Item("USER_BR_CD").ToString()
        Dim brCd As String = dr.Item("NRS_BR_CD").ToString()
        Dim cdL As String = dr.Item("CUST_CD_L").ToString()
        Dim delFlg As String = dr.Item("SYS_DEL_FLG").ToString()

        '****************** 荷主マスタ ***********************

        Dim custDr As DataRow = ds.Tables(tableNmIn).NewRow()

        '更新条件/更新内容を格納
        custDr.Item("NRS_BR_CD") = brCd
        custDr.Item("CUST_CD_L") = cdL
        custDr.Item("CUST_CD_M") = "00"
        custDr.Item("CUST_CD_S") = "00"
        custDr.Item("CUST_CD_SS") = "00"
        custDr.Item("SYS_DEL_FLG") = delFlg

        'スキーマ名取得用
        custDr.Item("USER_BR_CD") = loginBrCd

        ds.Tables(tableNmIn).Rows.Add(custDr)

        '****************** 運賃タリフセットマスタ ***********************

        Dim tarifDr As DataRow = Nothing
        Dim tableNmTariff As String = "LMM090_TARIFF_SET"
        If ds.Tables(tableNmTariff).Rows(0).Item("CUST_CD_M").Equals("00") = False Then

            tarifDr = ds.Tables(tableNmTariff).NewRow()

            '更新条件/更新内容を格納(入荷データ)
            tarifDr.Item("NRS_BR_CD") = brCd
            tarifDr.Item("CUST_CD_L") = cdL
            tarifDr.Item("CUST_CD_M") = "00"
            '(2012.06.22)要望番号1178 セットマスタコードを3桁⇒4桁
            tarifDr.Item("SET_MST_CD") = "0001"
            'tarifDr.Item("SET_MST_CD") = "001"
            tarifDr.Item("SYS_DEL_FLG") = delFlg

            'スキーマ名取得用
            tarifDr.Item("USER_BR_CD") = loginBrCd

            ds.Tables(tableNmTariff).Rows.Add(tarifDr)

            tarifDr = ds.Tables(tableNmTariff).NewRow()

            '更新条件/更新内容を格納(出荷データ)
            tarifDr.Item("NRS_BR_CD") = brCd
            tarifDr.Item("CUST_CD_L") = cdL
            tarifDr.Item("CUST_CD_M") = "00"
            '(2012.06.22)要望番号1178 セットマスタコードを3桁⇒4桁
            tarifDr.Item("SET_MST_CD") = "0000"
            'tarifDr.Item("SET_MST_CD") = "000"
            tarifDr.Item("SYS_DEL_FLG") = delFlg

            'スキーマ名取得用
            tarifDr.Item("USER_BR_CD") = loginBrCd

            ds.Tables(tableNmTariff).Rows.Add(tarifDr)

        End If

    End Sub

    ''' <summary>
    ''' 在庫データ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ChkZaiT(ByVal ds As DataSet) As DataSet

        '単価マスタ存在チェック
        ds = MyBase.CallDAC(Me._Dac, "ExistZaiT", ds)
        If MyBase.IsMessageExist = True Then
            Return ds
        End If

        Return ds

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 荷主マスタ検索処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
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

        Return ds

    End Function

    ''' <summary>
    '''初期検索を行う
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>荷主マスタ、荷主別帳票マスタ、荷主明細マスタ、運賃タリフセットマスタ検索</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '荷主マスタ検索
        ds = MyBase.CallDAC(Me._Dac, "SelectListData", ds)

#If True Then   'ADD 2018/12/28 依頼番号 : 003453   【LMS】IntegWeb利用状況を、荷主マスタに表示＆データ抽出機能
        'inのIntegWeb条件でLMM090OUTを再設定する


#End If

        '荷主別帳票マスタ検索
        ds = MyBase.CallDAC(Me._Dac, "SelectPrtListData", ds)

        '運賃タリフセットマスタ検索
        ds = MyBase.CallDAC(Me._Dac, "SelectTariffListData", ds)

        '要望番号:349 yamanaka 2012.07.11 Start
        '荷主明細マスタ検索
        ds = MyBase.CallDAC(Me._Dac, "SelectDetailListData", ds)
        '要望番号:349 yamanaka 2012.07.11 End

        Return ds

    End Function

    ''' <summary>
    ''' 変動保管料関連チェック用検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectVarStrage(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectVarStrage", ds)

    End Function

#End Region

#Region "契約通貨コンボ取得"
    ''' <summary>
    '''契約通貨コンボ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>契約通貨コンボ取得</remarks>
    Private Function SelectComboItemCurrCd(ByVal ds As DataSet) As DataSet

        '契約通貨コンボ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectComboItemCurrCd", ds)

        Return ds

    End Function

#End Region

#Region "保存処理"

#Region "チェック"

    ''2011.09.08 検証結果_導入時要望№1対応 START
    '''' <summary>
    '''' 郵便番号マスタチェック
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet（プロキシ）</returns>
    '''' <remarks></remarks>
    'Private Function ChkZipM(ByVal ds As DataSet) As DataSet

    '    Dim tableNm As String = "LMM090IN"

    '    If String.IsNullOrEmpty(ds.Tables(tableNm).Rows(0).Item("ZIP").ToString()) = False Then

    '        '郵便番号マスタ存在チェック
    '        ds = MyBase.CallDAC(Me._Dac, "ExistZipM", ds)

    '    End If
    '    Return ds

    'End Function
    ''2011.09.08 検証結果_導入時要望№1対応 END

    ''' <summary>
    ''' 新規採番コード取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetCustCd(ByVal ds As DataSet) As DataSet

        ' 新規採番コード取得
        Return MyBase.CallDAC(Me._Dac, "GetCustCd", ds)

    End Function

    ''' <summary>
    ''' 荷主マスタ重複チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ExistCustM(ByVal ds As DataSet) As DataSet

        ' 荷主マスタ存在チェック
        Return MyBase.CallDAC(Me._Dac, "ExistCustM", ds)

    End Function

#End Region

#Region "新規登録/更新"

    ''' <summary>
    ''' 荷主マスタ、運賃タリフセットマスタ、荷主別帳票マスタ、荷主明細マスタ 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMM090IN").Rows(0)
        Dim oyaDataFlg As Boolean = dr.Item("CUST_CD_M").Equals("00") _
                            AndAlso dr.Item("CUST_CD_S").Equals("00") _
                            AndAlso dr.Item("CUST_CD_SS").Equals("00")

        If oyaDataFlg = False Then
            ds = MyBase.CallDAC(Me._Dac, "ChkOyaData", ds)
            If MyBase.GetResultCount() < 1 Then
                'エラーメッセージ表示
                MyBase.SetMessage("E372")
                Return ds
            End If
        End If

        '荷主マスタ新規登録
        ds = MyBase.CallDAC(Me._Dac, "InsertCustM", ds)

        '新規登録フラグ
        Dim shinkiFlg As Boolean = LMConst.FLG.OFF.Equals(dr.Item("CLICK_M_FLG").ToString()) = True _
                           AndAlso LMConst.FLG.OFF.Equals(dr.Item("CLICK_S_FLG").ToString()) = True _
                           AndAlso LMConst.FLG.OFF.Equals(dr.Item("CLICK_SS_FLG").ToString()) = True

        If shinkiFlg = True _
        OrElse dr.Item("CLICK_M_FLG").Equals(LMConst.FLG.ON) Then
            '運賃タリフセットマスタ新規登録
            ds = MyBase.CallDAC(Me._Dac, "InsertTariffSetM", ds)
        End If

        '要望番号:349 yamanaka 2012.07.12 Start
        '荷主明細データがある場合、新規登録
        If ds.Tables("LMM090_CUST_DETAILS").Rows.Count > 0 Then
            '荷主明細マスタ登録
            ds = (MyBase.CallDAC(Me._Dac, "InsertCustDtlM", ds))
        End If
        '要望番号:349 yamanaka 2012.07.12 End

        '極小の場合、スルー
        If dr.Item("CLICK_SS_FLG").Equals(LMConst.FLG.ON) = True Then
            Return ds
        End If

        '要望番号:349 yamanaka 2012.07.12 Start
        '荷主別帳票データがある場合、新規登録
        If ds.Tables("LMM090_CUST_RPT").Rows.Count > 0 Then
            '荷主別帳票マスタ登録
            ds = (MyBase.CallDAC(Me._Dac, "InsertCustPrtM", ds))
        End If
        '要望番号:349 yamanaka 2012.07.12 End

        Return ds


    End Function

    ''' <summary>
    ''' 荷主マスタ、運賃タリフセットマスタ、荷主別帳票マスタ、荷主明細マスタ 更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveData(ByVal ds As DataSet) As DataSet

        Dim inDr As DataRow = ds.Tables("LMM090IN").Rows(0)

        '荷主マスタ更新登録
        Dim ssFlg As Boolean = LMConst.FLG.ON.Equals(inDr.Item("CLICK_SS_FLG").ToString())
        Dim methodNm As String = String.Empty
        If LMConst.FLG.ON.Equals(inDr.Item("CLICK_L_FLG").ToString()) = True Then
            methodNm = "UpdateCustM_L"
        ElseIf LMConst.FLG.ON.Equals(inDr.Item("CLICK_M_FLG").ToString()) = True Then
            methodNm = "UpdateCustM_M"
        ElseIf LMConst.FLG.ON.Equals(inDr.Item("CLICK_S_FLG").ToString()) = True Then
            methodNm = "UpdateCustM_S"
        ElseIf ssFlg = True Then
            methodNm = "UpdateCustM_SS"
        End If
        ds = MyBase.CallDAC(Me._Dac, methodNm, ds)

        '運賃タリフセットマスタ更新登録
        ds = MyBase.CallDAC(Me._Dac, "UpdateTariffSetM", ds)

        '要望番号:349 yamanaka 2012.07.12 Start
        '荷主明細マスタ物理削除
        ds = (MyBase.CallDAC(Me._Dac, "DeleteCustDtlM", ds))

        '更新登録する明細データがない場合、処理終了
        If ds.Tables("LMM090_CUST_DETAILS").Rows.Count > 0 Then
            '荷主明細マスタ新規登録
            ds = (MyBase.CallDAC(Me._Dac, "InsertCustDtlM", ds))
        End If
        '要望番号:349 yamanaka 2012.07.12 End

        '極小編集の場合、スルー
        If ssFlg = True Then
            Return ds
        End If

        '荷主別帳票マスタ物理削除
        ds = (MyBase.CallDAC(Me._Dac, "DeleteCustPrtM", ds))

        '要望番号:349 yamanaka 2012.07.12 Start
        '更新登録する明細データがない場合、処理終了
        If ds.Tables("LMM090_CUST_RPT").Rows.Count > 0 Then
            '荷主別帳票マスタ新規登録
            ds = (MyBase.CallDAC(Me._Dac, "InsertCustPrtM", ds))
        End If
        '要望番号:349 yamanaka 2012.07.12 End

        Return ds


    End Function

#End Region

#Region "ComboBox"

    ''' <summary>
    ''' 製品セグメント取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>コンボボックスの選択肢として利用</remarks>
    Private Function SelectComboSeihin(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectComboSeihin", ds)

    End Function

#End Region

#End Region

#End Region

End Class
