' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブ
'  プログラムID     :  LMF040    : 運賃検索
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMF040BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF040BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMF040DAC = New LMF040DAC()

#End Region

#Region "Const"

    ''' <summary>
    ''' 運送の更新処理アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_UNSO As String = "UpdateUnsoLSysData"

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    ''' <summary>
    ''' 運送(中)の更新処理アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_UNSOM As String = "UpdateUnsoM"
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

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
    ''' データセットテーブル名(INテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMF040IN"

    ''' <summary>
    ''' データセットテーブル名(OUTテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMF040OUT"

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

    '要望番号:1045 terakawa 2013.03.28 Start
    ''' <summary>
    ''' データセットテーブル名(G_HED_CHKテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_G_HED_CHK As String = "G_HED_CHK"
    '要望番号:1045 terakawa 2013.03.28 End

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
    ''' 並び順(荷主 , 運行番号)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ORDER_BY_CUSTTRIP As String = "01"

    ''' <summary>
    ''' 並び順(届先コード)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ORDER_BY_DEST As String = "02"

    ''' <summary>
    ''' 並び順(届先JIS)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ORDER_BY_DESTJIS As String = "03"

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    ''' <summary>
    ''' 並び順(日立物流用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ORDER_BY_DIC As String = "04"
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

    '2017/10/10 Annen アクサルタ 運賃按分計算の自動化対応 START
    ''' <summary>
    ''' アクサルタ同送用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ORDER_BY_AXSALTA As String = "05"
    '2017/10/10 Annen アクサルタ 運賃按分計算の自動化対応 END

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

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    ''' <summary>
    ''' 営業所コード(千葉)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const NRS_BR_CD_10 As String = "10"

    ''' <summary>
    ''' 営業所コード(群馬)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const NRS_BR_CD_30 As String = "30"

    ''' <summary>
    ''' 営業所コード(埼玉)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const NRS_BR_CD_50 As String = "50"

    ''' <summary>
    ''' 営業所コード(春日部)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const NRS_BR_CD_55 As String = "55"

    ''' <summary>
    ''' 日立物流まとめ荷主(千葉)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DIC_MATOME_01 As String = "01"

    ''' <summary>
    ''' 日立物流まとめ荷主(群馬)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DIC_MATOME_02 As String = "02"

    ''' <summary>
    ''' 日立物流まとめ荷主(埼玉)①
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DIC_MATOME_03 As String = "03"

    ''' <summary>
    ''' 日立物流まとめ荷主(埼玉)②
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DIC_MATOME_04 As String = "04"
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

    '要望番号2129 追加START 2013.11.25
    ''' <summary>
    ''' 日立物流まとめ荷主(埼玉)②
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DIC_MATOME_05 As String = "05"
    '要望番号2129 追加END 2013.11.25

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

        ds = Me.DacAccess(ds, LMF040BLC.ACTION_ID_DATA)
        Dim dt As DataTable = ds.Tables(LMF040BLC.TABLE_NM_OUT)
        Dim dr1 As DataRow = Nothing
        Dim dr2 As DataRow = Nothing
        Dim max1 As Integer = dt.Rows.Count - 2
        Dim max2 As Integer = dt.Rows.Count - 1
        Dim idx As Integer = 0
        Dim orderBy As String = ds.Tables(LMF040BLC.TABLE_NM_IN).Rows(0).Item("ORDER_BY").ToString()
        Dim unsoDate As String = String.Empty
        Dim tariffCd As String = String.Empty
        Dim seiq As String = String.Empty
        Dim extc As String = String.Empty
        Dim tax As String = String.Empty
        Dim tyukeiFlg1 As String = String.Empty
        Dim tyukeiFlg2 As String = String.Empty
        Dim item1 As String = String.Empty
        Dim item2 As String = String.Empty
        Dim item3 As String() = Nothing
        'START YANAI 20120622 DIC運賃まとめ及び再計算対応
        Dim outDr() As DataRow = Nothing
        Dim nrsBrCd As String = ds.Tables(LMF040BLC.TABLE_NM_IN).Rows(0).Item("NRS_BR_CD").ToString()
        Dim matomeKb As String = ds.Tables(LMF040BLC.TABLE_NM_IN).Rows(0).Item("MATOME_KB").ToString()
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応

        Dim flgOn As Boolean = True
        Dim flgOff As Boolean = False

        'START YANAI 20120622 DIC運賃まとめ及び再計算対応
        'For i As Integer = 0 To max1

        '    dr1 = dt.Rows(i)

        '    '既に相手が見つかっているレコードの場合、スルー
        '    If LMConst.FLG.ON.Equals(dr1.Item("GROUP_FLG").ToString()) = flgOn Then
        '        Continue For
        '    End If

        '    '日付を設定
        '    unsoDate = Me.GetUnsoDate(dr1)

        '    '値がない場合、スルー
        '    If String.IsNullOrEmpty(unsoDate) = flgOn Then
        '        Continue For
        '    End If

        '    '請求先を設定
        '    seiq = dr1.Item("SEIQTO_CD").ToString()

        '    '値がない場合、スルー
        '    If String.IsNullOrEmpty(seiq) = flgOn Then
        '        Continue For
        '    End If

        '    'タリフを設定
        '    tariffCd = dr1.Item("SEIQ_TARIFF_CD").ToString()

        '    '値がない場合、スルー
        '    If String.IsNullOrEmpty(tariffCd) = flgOn Then
        '        Continue For
        '    End If

        '    '税区分を設定
        '    tax = dr1.Item("TAX_KB").ToString()

        '    '値がない場合、スルー
        '    If String.IsNullOrEmpty(tax) = flgOn Then
        '        Continue For
        '    End If

        '    '中継フラグを設定
        '    tyukeiFlg1 = dr1.Item("TYUKEI_HAISO_FLG").ToString()

        '    Select Case orderBy

        '        Case LMF040BLC.ORDER_BY_CUSTTRIP

        '            '荷主(大)コードの必須チェック
        '            item1 = dr1.Item("CUST_CD_L").ToString()

        '            '値がない場合、スルー
        '            If String.IsNullOrEmpty(item1) = flgOn Then
        '                Continue For
        '            End If

        '            '荷主(中)コードの必須チェック
        '            item2 = dr1.Item("CUST_CD_M").ToString()

        '            '値がない場合、スルー
        '            If String.IsNullOrEmpty(item2) = flgOn Then
        '                Continue For
        '            End If

        '            '運行番号の設定
        '            item3 = Me.GetTripNo(dr1, tyukeiFlg1)

        '            '運行番号がない場合、スルー
        '            If Me.IsValueChk(item3) = flgOff Then
        '                Continue For
        '            End If

        '        Case LMF040BLC.ORDER_BY_DEST

        '            '届先コードの必須チェック
        '            item1 = dr1.Item("DEST_CD").ToString()

        '            '値がない場合、スルー
        '            If String.IsNullOrEmpty(item1) = flgOn Then
        '                Continue For
        '            End If

        '        Case LMF040BLC.ORDER_BY_DESTJIS

        '            '届先JISの必須チェック
        '            item1 = dr1.Item("DEST_JIS_CD").ToString()

        '            '値がない場合、スルー
        '            If String.IsNullOrEmpty(item1) = flgOn Then
        '                Continue For
        '            End If

        '    End Select

        '    '割増を設定
        '    extc = dr1.Item("SEIQ_ETARIFF_CD").ToString()

        '    idx = i + 1

        '    For j As Integer = idx To max2

        '        dr2 = dt.Rows(j)

        '        '日付が違う場合、スルー
        '        If unsoDate.Equals(Me.GetUnsoDate(dr2)) = flgOff Then
        '            Continue For
        '        End If

        '        '請求先が違う場合、スルー
        '        If seiq.Equals(dr2.Item("SEIQTO_CD").ToString()) = flgOff Then
        '            Continue For
        '        End If

        '        'タリフが違う場合、スルー
        '        If tariffCd.Equals(dr2.Item("SEIQ_TARIFF_CD").ToString()) = flgOff Then
        '            Continue For
        '        End If

        '        '割増が違う場合、スルー
        '        If extc.Equals(dr2.Item("SEIQ_ETARIFF_CD").ToString()) = flgOff Then
        '            Continue For
        '        End If

        '        '税区分が違う場合、スルー
        '        If tax.Equals(dr2.Item("TAX_KB").ToString()) = flgOff Then
        '            Continue For
        '        End If

        '        Select Case orderBy

        '            Case LMF040BLC.ORDER_BY_CUSTTRIP

        '                '荷主(大)コードが違い場合、スルー
        '                If item1.Equals(dr1.Item("CUST_CD_L").ToString()) = flgOff Then
        '                    Continue For
        '                End If

        '                '荷主(中)コードが違い場合、スルー
        '                If item2.Equals(dr1.Item("CUST_CD_M").ToString()) = flgOff Then
        '                    Continue For
        '                End If

        '                '運行番号の判定
        '                tyukeiFlg2 = dr2.Item("TYUKEI_HAISO_FLG").ToString()
        '                If Me.ChkTripNo(item3, tyukeiFlg1, Me.GetTripNo(dr2, tyukeiFlg2), tyukeiFlg2) = flgOff Then
        '                    Continue For
        '                End If

        '            Case LMF040BLC.ORDER_BY_DEST

        '                '届先コードが違い場合、スルー
        '                If item1.Equals(dr2.Item("DEST_CD").ToString()) = flgOff Then
        '                    Continue For
        '                End If

        '            Case LMF040BLC.ORDER_BY_DESTJIS

        '                '届先JISが違い場合、スルー
        '                If item1.Equals(dr2.Item("DEST_JIS_CD").ToString()) = flgOff Then
        '                    Continue For
        '                End If

        '        End Select

        '        '相手がいる場合、フラグを設定
        '        dr1.Item("GROUP_FLG") = LMConst.FLG.ON
        '        dr2.Item("GROUP_FLG") = LMConst.FLG.ON

        '    Next

        'Next
        '2017/10/10 Annen アクサルタ 運賃按分計算の自動化対応 START
        If (LMF040BLC.ORDER_BY_CUSTTRIP).Equals(orderBy) = True OrElse _
            (LMF040BLC.ORDER_BY_DEST).Equals(orderBy) = True OrElse _
            (LMF040BLC.ORDER_BY_DESTJIS).Equals(orderBy) = True OrElse _
            (LMF040BLC.ORDER_BY_AXSALTA).Equals(orderBy) = True Then
            'If (LMF040BLC.ORDER_BY_CUSTTRIP).Equals(orderBy) = True OrElse _
            '    (LMF040BLC.ORDER_BY_DEST).Equals(orderBy) = True OrElse _
            '    (LMF040BLC.ORDER_BY_DESTJIS).Equals(orderBy) = True Then
            '2017/10/10 Annen アクサルタ 運賃按分計算の自動化対応 END
            For i As Integer = 0 To max1

                dr1 = dt.Rows(i)

                '既に相手が見つかっているレコードの場合、スルー
                If LMConst.FLG.ON.Equals(dr1.Item("GROUP_FLG").ToString()) = flgOn Then
                    Continue For
                End If

                '日付を設定
                unsoDate = Me.GetUnsoDate(dr1)

                '値がない場合、スルー
                If String.IsNullOrEmpty(unsoDate) = flgOn Then
                    Continue For
                End If

                '請求先を設定
                seiq = dr1.Item("SEIQTO_CD").ToString()

                '値がない場合、スルー
                If String.IsNullOrEmpty(seiq) = flgOn Then
                    Continue For
                End If

                'タリフを設定
                tariffCd = dr1.Item("SEIQ_TARIFF_CD").ToString()

                '値がない場合、スルー
                If String.IsNullOrEmpty(tariffCd) = flgOn Then
                    Continue For
                End If

                '税区分を設定
                tax = dr1.Item("TAX_KB").ToString()

                '値がない場合、スルー
                If String.IsNullOrEmpty(tax) = flgOn Then
                    Continue For
                End If

                '中継フラグを設定
                tyukeiFlg1 = dr1.Item("TYUKEI_HAISO_FLG").ToString()

                Select Case orderBy

                    Case LMF040BLC.ORDER_BY_CUSTTRIP

                        '荷主(大)コードの必須チェック
                        item1 = dr1.Item("CUST_CD_L").ToString()

                        '値がない場合、スルー
                        If String.IsNullOrEmpty(item1) = flgOn Then
                            Continue For
                        End If

                        '荷主(中)コードの必須チェック
                        item2 = dr1.Item("CUST_CD_M").ToString()

                        '値がない場合、スルー
                        If String.IsNullOrEmpty(item2) = flgOn Then
                            Continue For
                        End If

                        '運行番号の設定
                        item3 = Me.GetTripNo(dr1, tyukeiFlg1)

                        '運行番号がない場合、スルー
                        If Me.IsValueChk(item3) = flgOff Then
                            Continue For
                        End If

                    Case LMF040BLC.ORDER_BY_DEST

                        '届先コードの必須チェック
                        item1 = dr1.Item("DEST_CD").ToString()

                        '値がない場合、スルー
                        If String.IsNullOrEmpty(item1) = flgOn Then
                            Continue For
                        End If

                    Case LMF040BLC.ORDER_BY_DESTJIS

                        '届先JISの必須チェック
                        item1 = dr1.Item("DEST_JIS_CD").ToString()

                        '値がない場合、スルー
                        If String.IsNullOrEmpty(item1) = flgOn Then
                            Continue For
                        End If

                        '2017/10/10 Annen アクサルタ 運賃按分計算の自動化対応 START
                    Case LMF040BLC.ORDER_BY_AXSALTA

                        '届先コードの必須チェック
                        item1 = dr1.Item("DEST_CD").ToString()

                        '値がない場合、スルー
                        If String.IsNullOrEmpty(item1) = flgOn Then
                            Continue For
                        End If

                        '荷主コード（大）のアクサルタチェック
                        item2 = dr1.Item("CUST_CD_L").ToString()

                        'アクサルタ以外の場合、スルー
                        If item2.Equals("00588") = flgOff Then
                            Continue For
                        End If
                        '2017/10/10 Annen アクサルタ 運賃按分計算の自動化対応 END

                End Select

                '割増を設定
                extc = dr1.Item("SEIQ_ETARIFF_CD").ToString()

                idx = i + 1

                For j As Integer = idx To max2

                    dr2 = dt.Rows(j)

                    '日付が違う場合、スルー
                    If unsoDate.Equals(Me.GetUnsoDate(dr2)) = flgOff Then
                        Continue For
                    End If

                    '2017/10/10 Annen アクサルタ 運賃按分計算の自動化対応 upd start
                    'アクサルタの場合、請求先が異なっていてもスルーさせない処理追加
                    If orderBy.Equals(LMF040BLC.ORDER_BY_AXSALTA) Then
                    Else
                        '請求先が違う場合、スルー
                        If seiq.Equals(dr2.Item("SEIQTO_CD").ToString()) = flgOff Then
                            Continue For
                        End If
                    End If
                    '2017/10/10 Annen アクサルタ 運賃按分計算の自動化対応 upd end

                    'タリフが違う場合、スルー
                    If tariffCd.Equals(dr2.Item("SEIQ_TARIFF_CD").ToString()) = flgOff Then
                        Continue For
                    End If

                    '割増が違う場合、スルー
                    If extc.Equals(dr2.Item("SEIQ_ETARIFF_CD").ToString()) = flgOff Then
                        Continue For
                    End If

                    '税区分が違う場合、スルー
                    If tax.Equals(dr2.Item("TAX_KB").ToString()) = flgOff Then
                        Continue For
                    End If

                    Select Case orderBy

                        Case LMF040BLC.ORDER_BY_CUSTTRIP

                            '荷主(大)コードが違い場合、スルー
                            If item1.Equals(dr1.Item("CUST_CD_L").ToString()) = flgOff Then
                                Continue For
                            End If

                            '荷主(中)コードが違い場合、スルー
                            If item2.Equals(dr1.Item("CUST_CD_M").ToString()) = flgOff Then
                                Continue For
                            End If

                            '運行番号の判定
                            tyukeiFlg2 = dr2.Item("TYUKEI_HAISO_FLG").ToString()
                            If Me.ChkTripNo(item3, tyukeiFlg1, Me.GetTripNo(dr2, tyukeiFlg2), tyukeiFlg2) = flgOff Then
                                Continue For
                            End If

                        Case LMF040BLC.ORDER_BY_DEST

                            '届先コードが違い場合、スルー
                            If item1.Equals(dr2.Item("DEST_CD").ToString()) = flgOff Then
                                Continue For
                            End If

                        Case LMF040BLC.ORDER_BY_DESTJIS

                            '届先JISが違い場合、スルー
                            If item1.Equals(dr2.Item("DEST_JIS_CD").ToString()) = flgOff Then
                                Continue For
                            End If

                            '2017/10/10 Annen アクサルタ 運賃按分計算の自動化対応 START
                        Case LMF040BLC.ORDER_BY_AXSALTA

                            '届先コードが違う場合、スルー
                            If item1.Equals(dr2.Item("DEST_CD").ToString()) = flgOff Then
                                Continue For
                            End If

                            '荷主コードがitem2と違う場合（"00588"(アクサルタ)と違う場合）、スルー
                            If item2.Equals(dr2.Item("CUST_CD_L").ToString()) = flgOff Then
                                Continue For
                            End If

                            '2017/10/10 Annen アクサルタ 運賃按分計算の自動化対応 END

                    End Select

                    '相手がいる場合、フラグを設定
                    dr1.Item("GROUP_FLG") = LMConst.FLG.ON
                    dr2.Item("GROUP_FLG") = LMConst.FLG.ON

                Next

            Next

        ElseIf (LMF040BLC.ORDER_BY_DIC).Equals(orderBy) = True Then
            'まとめ候補条件が日立物流の場合

            For i As Integer = 0 To max2

                dr1 = dt.Rows(i)

                '既に相手が見つかっているレコードの場合、スルー
                If (LMConst.FLG.ON).Equals(dr1.Item("GROUP_FLG").ToString()) = flgOn Then
                    Continue For
                End If

                If (LMF040BLC.DIC_MATOME_01).Equals(matomeKb) = True Then
                    '①千葉のまとめ対象荷主の場合
                    If String.IsNullOrEmpty(dr1.Item("ARR_PLAN_DATE").ToString()) = True OrElse
                        String.IsNullOrEmpty(dr1.Item("DEST_JIS_CD").ToString()) = True OrElse
                        String.IsNullOrEmpty(dr1.Item("ABUKA_CD").ToString()) = True OrElse
                        (String.IsNullOrEmpty(dr1.Item("DEST_CD").ToString()) = True AndAlso
                        String.IsNullOrEmpty(dr1.Item("MINASHI_DEST_CD").ToString()) = True) Then
                        Continue For
                    End If
                    outDr = dt.Select(String.Concat("ARR_PLAN_DATE = '", dr1.Item("ARR_PLAN_DATE").ToString(), "' AND ",
                                                    "DEST_JIS_CD = '", dr1.Item("DEST_JIS_CD").ToString(), "' AND ",
                                                    "ABUKA_CD <> '' AND ",
                                                    "(DEST_CD = '", dr1.Item("DEST_CD").ToString(), "' OR ",
                                                    "MINASHI_DEST_CD = '", dr1.Item("MINASHI_DEST_CD").ToString(), "')"))
                ElseIf (LMF040BLC.NRS_BR_CD_10).Equals(nrsBrCd) = True Then
                    '②千葉のまとめ対象荷主以外の場合
                    If (String.IsNullOrEmpty(dr1.Item("DEST_CD").ToString()) = True AndAlso
                        String.IsNullOrEmpty(dr1.Item("MINASHI_DEST_CD").ToString()) = True) Then
                        Continue For
                    End If
                    outDr = dt.Select(String.Concat("(DEST_CD = '", dr1.Item("DEST_CD").ToString(), "' OR ",
                                                    "MINASHI_DEST_CD = '", dr1.Item("MINASHI_DEST_CD").ToString(), "')"))
                ElseIf (LMF040BLC.DIC_MATOME_02).Equals(matomeKb) = True Then
                    '③群馬のまとめ対象荷主の場合
                    If String.IsNullOrEmpty(dr1.Item("OUTKA_PLAN_DATE").ToString()) = True OrElse
                        String.IsNullOrEmpty(dr1.Item("BIN_KB").ToString()) = True OrElse
                        (String.IsNullOrEmpty(dr1.Item("DEST_CD").ToString()) = True AndAlso
                        String.IsNullOrEmpty(dr1.Item("MINASHI_DEST_CD").ToString()) = True) Then
                        Continue For
                    End If
                    outDr = dt.Select(String.Concat("OUTKA_PLAN_DATE = '", dr1.Item("OUTKA_PLAN_DATE").ToString(), "' AND ",
                                                    "BIN_KB = '", dr1.Item("BIN_KB").ToString(), "' AND ",
                                                    "(DEST_CD = '", dr1.Item("DEST_CD").ToString(), "' OR ",
                                                    "MINASHI_DEST_CD = '", dr1.Item("MINASHI_DEST_CD").ToString(), "')"))

                    '要望番号2129 追加START 2013.11.25
                ElseIf (LMF040BLC.DIC_MATOME_05).Equals(matomeKb) = True Then
                    '④群馬のまとめ対象荷主の場合(日立物流用コンボ選択、BP荷主の場合。)
                    If String.IsNullOrEmpty(dr1.Item("ARR_PLAN_DATE").ToString()) = True OrElse
                        (String.IsNullOrEmpty(dr1.Item("DEST_CD").ToString()) = True AndAlso
                        String.IsNullOrEmpty(dr1.Item("MINASHI_DEST_CD").ToString()) = True) Then
                        Continue For
                    End If
                    outDr = dt.Select(String.Concat("ARR_PLAN_DATE = '", dr1.Item("ARR_PLAN_DATE").ToString(), "' AND ",
                                                    "(DEST_CD = '", dr1.Item("DEST_CD").ToString(), "' OR ",
                                                    "MINASHI_DEST_CD = '", dr1.Item("MINASHI_DEST_CD").ToString(), "')"))
                    '要望番号2129 追加END 2013.11.25

                ElseIf ((LMF040BLC.NRS_BR_CD_30).Equals(nrsBrCd)) And ("30001".Equals(dr1.Item("CUST_CD_L").ToString) Or "30002".Equals(dr1.Item("CUST_CD_L").ToString) Or "30010".Equals(dr1.Item("CUST_CD_L").ToString)) Then
                    '⑧春日部の場合（元々は営業所=55の処理）
                    If String.IsNullOrEmpty(dr1.Item("ARR_PLAN_DATE").ToString()) = True OrElse
                        String.IsNullOrEmpty(dr1.Item("ABUKA_CD").ToString()) = True OrElse
                        String.IsNullOrEmpty(dr1.Item("BIN_KB").ToString()) = True OrElse
                        (String.IsNullOrEmpty(dr1.Item("DEST_CD").ToString()) = True AndAlso
                        String.IsNullOrEmpty(dr1.Item("MINASHI_DEST_CD").ToString()) = True) Then
                        Continue For
                    End If
                    outDr = dt.Select(String.Concat("ARR_PLAN_DATE = '", dr1.Item("ARR_PLAN_DATE").ToString(), "' AND ",
                                                    "ABUKA_CD = '", dr1.Item("ABUKA_CD").ToString(), "' AND ",
                                                    "BIN_KB = '", dr1.Item("BIN_KB").ToString(), "' AND ",
                                                    "(DEST_CD = '", dr1.Item("DEST_CD").ToString(), "' OR ",
                                                    "MINASHI_DEST_CD = '", dr1.Item("MINASHI_DEST_CD").ToString(), "')"))

                ElseIf (LMF040BLC.NRS_BR_CD_30).Equals(nrsBrCd) = True Then
                    '④群馬のまとめ対象荷主以外の場合
                    If String.IsNullOrEmpty(dr1.Item("OUTKA_PLAN_DATE").ToString()) = True OrElse
                        (String.IsNullOrEmpty(dr1.Item("DEST_CD").ToString()) = True AndAlso
                        String.IsNullOrEmpty(dr1.Item("MINASHI_DEST_CD").ToString()) = True) Then
                        Continue For
                    End If
                    outDr = dt.Select(String.Concat("OUTKA_PLAN_DATE = '", dr1.Item("OUTKA_PLAN_DATE").ToString(), "' AND ",
                                                    "(DEST_CD = '", dr1.Item("DEST_CD").ToString(), "' OR ",
                                                    "MINASHI_DEST_CD = '", dr1.Item("MINASHI_DEST_CD").ToString(), "')"))
                ElseIf (LMF040BLC.DIC_MATOME_03).Equals(matomeKb) = True Then
                    '⑤埼玉のまとめ対象荷主の場合
                    If String.IsNullOrEmpty(dr1.Item("ARR_PLAN_DATE").ToString()) = True OrElse
                        String.IsNullOrEmpty(dr1.Item("ZBUKA_CD").ToString()) = True OrElse
                        String.IsNullOrEmpty(dr1.Item("ABUKA_CD").ToString()) = True OrElse
                        String.IsNullOrEmpty(dr1.Item("BIN_KB").ToString()) = True OrElse
                        (String.IsNullOrEmpty(dr1.Item("DEST_CD").ToString()) = True AndAlso
                        String.IsNullOrEmpty(dr1.Item("MINASHI_DEST_CD").ToString()) = True) Then
                        Continue For
                    End If
                    outDr = dt.Select(String.Concat("ARR_PLAN_DATE = '", dr1.Item("ARR_PLAN_DATE").ToString(), "' AND ",
                                                    "ZBUKA_CD = '", dr1.Item("ZBUKA_CD").ToString(), "' AND ",
                                                    "SUBSTRING(ABUKA_CD,1,5) = '", Mid(dr1.Item("ABUKA_CD").ToString(), 1, 5), "' AND ",
                                                    "BIN_KB = '", dr1.Item("BIN_KB").ToString(), "' AND ",
                                                    "(DEST_CD = '", dr1.Item("DEST_CD").ToString(), "' OR ",
                                                    "MINASHI_DEST_CD = '", dr1.Item("MINASHI_DEST_CD").ToString(), "')"))
                ElseIf (LMF040BLC.DIC_MATOME_04).Equals(matomeKb) = True Then
                    '⑥埼玉のまとめ対象荷主の場合
                    If String.IsNullOrEmpty(dr1.Item("ARR_PLAN_DATE").ToString()) = True OrElse
                        String.IsNullOrEmpty(dr1.Item("ABUKA_CD").ToString()) = True OrElse
                        (String.IsNullOrEmpty(dr1.Item("DEST_CD").ToString()) = True AndAlso
                        String.IsNullOrEmpty(dr1.Item("MINASHI_DEST_CD").ToString()) = True) Then
                        Continue For
                    End If
                    outDr = dt.Select(String.Concat("ARR_PLAN_DATE = '", dr1.Item("ARR_PLAN_DATE").ToString(), "' AND ",
                                                    "SUBSTRING(ABUKA_CD,1,5) = '", Mid(dr1.Item("ABUKA_CD").ToString(), 1, 5), "' AND ",
                                                    "(DEST_CD = '", dr1.Item("DEST_CD").ToString(), "' OR ",
                                                    "MINASHI_DEST_CD = '", dr1.Item("MINASHI_DEST_CD").ToString(), "')"))
                ElseIf (LMF040BLC.NRS_BR_CD_50).Equals(nrsBrCd) = True Then
                    '⑦埼玉のまとめ対象荷主以外の場合
                    If String.IsNullOrEmpty(dr1.Item("ARR_PLAN_DATE").ToString()) = True OrElse
                        (String.IsNullOrEmpty(dr1.Item("DEST_CD").ToString()) = True AndAlso
                        String.IsNullOrEmpty(dr1.Item("MINASHI_DEST_CD").ToString()) = True) Then
                        Continue For
                    End If
                    outDr = dt.Select(String.Concat("ARR_PLAN_DATE = '", dr1.Item("ARR_PLAN_DATE").ToString(), "' AND ",
                                                    "(DEST_CD = '", dr1.Item("DEST_CD").ToString(), "' OR ",
                                                    "MINASHI_DEST_CD = '", dr1.Item("MINASHI_DEST_CD").ToString(), "')"))
                    'ElseIf (LMF040BLC.NRS_BR_CD_55).Equals(nrsBrCd) = True Then
                    '    '⑧春日部の場合
                    '    If String.IsNullOrEmpty(dr1.Item("ARR_PLAN_DATE").ToString()) = True OrElse _
                    '        String.IsNullOrEmpty(dr1.Item("ABUKA_CD").ToString()) = True OrElse _
                    '        String.IsNullOrEmpty(dr1.Item("BIN_KB").ToString()) = True OrElse _
                    '        (String.IsNullOrEmpty(dr1.Item("DEST_CD").ToString()) = True AndAlso _
                    '        String.IsNullOrEmpty(dr1.Item("MINASHI_DEST_CD").ToString()) = True) Then
                    '        Continue For
                    '    End If
                    '    outDr = dt.Select(String.Concat("ARR_PLAN_DATE = '", dr1.Item("ARR_PLAN_DATE").ToString(), "' AND ", _
                    '                                    "ABUKA_CD = '", dr1.Item("ABUKA_CD").ToString(), "' AND ", _
                    '                                    "BIN_KB = '", dr1.Item("BIN_KB").ToString(), "' AND ", _
                    '                                    "(DEST_CD = '", dr1.Item("DEST_CD").ToString(), "' OR ", _
                    '                                    "MINASHI_DEST_CD = '", dr1.Item("MINASHI_DEST_CD").ToString(), "')"))
                Else
                    Continue For
                End If

                max1 = outDr.Length - 1
                If max1 > 0 Then
                    '同じ条件のデータが1件より多い場合のみ、まとめ対象
                    For j As Integer = 0 To max1
                        outDr(j).Item("GROUP_FLG") = LMConst.FLG.ON
                    Next
                End If
            Next

        End If
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応

        '不要レコードの削除処理
        ds = Me.DeleteNotGroupData(ds)

        'メッセージコードの設定
        Call Me.SetSelectErrMes(ds.Tables(LMF040BLC.TABLE_NM_OUT).Rows.Count)

        Return ds

    End Function

    ''' <summary>
    ''' 運賃タリフの承認チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectChkApproval(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 最終請求日を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>請求日</returns>
    ''' <remarks>取得できない場合は"00000000"を返却</remarks>
    Private Function SelectGheaderData(ByVal ds As DataSet) As String

        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Dim dt As DataTable = ds.Tables(LMF040BLC.TABLE_NM_G_HED)
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
        Dim guiDt As DataTable = guiDs.Tables(LMF040BLC.TABLE_NM_UNCHIN)
        Dim guiDr As DataRow = guiDt.Rows(0)

        'まとめ処理を行っているレコードの場合、紐付いているレコード全てを更新
        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If
        Dim dt As DataTable = ds.Tables(LMF040BLC.TABLE_NM_UNCHIN)
        Dim drs As DataRow() = dt.Select(String.Concat(" UNSO_NO_L = '", guiDr.Item("UNSO_NO_L").ToString(), "' AND UNSO_NO_M = '", guiDr.Item("UNSO_NO_M").ToString(), "' "))

        '画面の更新日時を反映
        drs(0).Item("SYS_UPD_DATE") = guiDr.Item("SYS_UPD_DATE").ToString()
        drs(0).Item("SYS_UPD_TIME") = guiDr.Item("SYS_UPD_TIME").ToString()

        Return ds

    End Function

#End Region

#Region "設定処理"

#Region "確定、確定解除処理"

    ''' <summary>
    ''' 確定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetFixData(ByVal ds As DataSet) As DataSet

        Return Me.SetComSaveDataAction(ds, False, String.Empty, False)

    End Function

    ''' <summary>
    ''' 確定解除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetFixCancellData(ByVal ds As DataSet) As DataSet

        Return Me.SetComSaveDataAction(ds, True, LMF040BLC.FIX_CANCELL, False)

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

        Return Me.SetComSaveDataAction(ds, True, LMF040BLC.GROUP, True)

    End Function

    ''' <summary>
    ''' まとめ解除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetGroupCancellData(ByVal ds As DataSet) As DataSet

        Return Me.SetComSaveDataAction(ds, True, LMF040BLC.GROUP_CANCELL, False)

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

        Return Me.SetComSaveDataAction(ds, True, LMF040BLC.IKKATU, False)

    End Function

    ''' <summary>
    ''' 一括変更(請求先)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveUnchinSeiqtoItemData(ByVal ds As DataSet) As DataSet

        Return Me.SetComSaveDataAction(ds, True, LMF040BLC.IKKATU, False, ds.Tables(LMF040BLC.TABLE_NM_UNCHIN).Rows(0).Item("CD_L").ToString())

    End Function

    ''' <summary>
    ''' 一括変更(タリフ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveUnchinTariffData(ByVal ds As DataSet) As DataSet

        '更新処理
        Return Me.SetComSaveDataAction(ds, True, LMF040BLC.IKKATU, False)

    End Function

#End Region

#Region "運賃更新処理(再計算時)"

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    ''' <summary>
    ''' 運賃更新処理(再計算時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdUnchinData(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing
        Dim drs As DataRow() = ds.Tables(LMF040BLC.TABLE_NM_UNCHIN).Select(String.Empty, " UNSO_NO_L , UNSO_NO_M ")

        '最終請求日のチェック
        If Me.ChkSeiqDate(ds, drs(0), String.Empty, String.Empty, False) = False Then
            Return Me.SetConnectionRecordErr(ds, drs(0).Item("ROW_NO").ToString())
        End If

        rtnDs = MyBase.CallDAC(Me._Dac, "SelectDataSaikeisan", ds)
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("E011")
            MyBase.SetMessageStore(LMF040BLC.GUIDANCE_KBN, "E011", , Convert.ToInt32(ds.Tables(LMF040BLC.TABLE_NM_UNCHIN).Rows(0).Item("ROW_NO")).ToString())
            Return rtnDs
        End If

        '更新
        rtnDs = MyBase.CallDAC(Me._Dac, "UpdUnchinData", ds)

        Return rtnDs

    End Function
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

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
            AndAlso Me.UpdateDataAction(ds, LMF040BLC.ACTION_ID_UPDATE_UNSO, chkErrFlg) = False _
            Then

            Return False
        End If

        'START YANAI 20120622 DIC運賃まとめ及び再計算対応
        '運送更新(中)フラグ
        If updateFlg = True _
            AndAlso Me.UpdateDataAction(ds, LMF040BLC.ACTION_ID_UPDATE_UNSOM, chkErrFlg) = False _
            Then

            Return False
        End If
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応

        '運賃の更新
        Return Me.UpdateDataAction(ds, LMF040BLC.ACTION_ID_UPDATE_UNCHIN, chkErrFlg)

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
        Dim upDt As DataTable = upDs.Tables(LMF040BLC.TABLE_NM_UNCHIN)

        Dim drs As DataRow() = ds.Tables(LMF040BLC.TABLE_NM_UNCHIN).Select(String.Empty, " UNSO_NO_L , UNSO_NO_M ")
        Dim max As Integer = drs.Length - 1
        Dim unsoL As String = String.Empty
        Dim chkUnsoL As String = String.Empty
        Dim updateFlg As Boolean = False
        For i As Integer = 0 To max

            '最終請求日のチェック
            If chkFlg = True AndAlso Me.ChkSeiqDate(ds, drs(i), msg, seiqtoCd, chkErrFlg) = False Then
                Return Me.SetConnectionRecordErr(ds, drs(i).Item("ROW_NO").ToString())
            End If

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

    ''' <summary>
    ''' 最終請求日のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="seiqtoCd">請求先コード(一括更新_請求先のみ使用)</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="chkErrFlg">チェックフラグ　True:通常のエラーチェック　False:一括更新のエラーチェック</param>
    ''' <returns>最終請求日</returns>
    ''' <remarks></remarks>
    Private Function ChkSeiqDate(ByVal ds As DataSet, ByVal dr As DataRow _
                                 , ByVal msg As String _
                                 , ByVal seiqtoCd As String _
                                 , ByVal chkErrFlg As Boolean _
                                 ) As Boolean

        Dim inDate As String = dr.Item("ARR_PLAN_DATE").ToString()
        Dim outDate As String = dr.Item("OUTKA_PLAN_DATE").ToString()
        Dim rowNo As String = dr.Item("ROW_NO").ToString()

        '納入日、出荷日の両方に値がない場合、スルー
        If String.IsNullOrEmpty(inDate) = True _
            AndAlso String.IsNullOrEmpty(outDate) Then
            Return True
        End If

        '元データ区分
        Dim moto As String = dr.Item("MOTO_DATA_KB").ToString()

        Dim selectDs As DataSet = ds.Copy
        Dim selectDt As DataTable = selectDs.Tables(LMF040BLC.TABLE_NM_UNCHIN)

        '請求日を取得用の情報を設定
        selectDt.Clear()
        selectDt.ImportRow(dr)

        '締め処理済の場合、スルー
        Dim rtnResult As Boolean = Me.ChkSeiqDate(ds, inDate, outDate, dr.Item("UNTIN_CALCULATION_KB").ToString(), Me.SelectGheaderData(selectDs), moto, msg, rowNo, chkErrFlg)

        '請求先コードに値がある場合、2回目のチェック
        If String.IsNullOrEmpty(seiqtoCd) = False Then

            '請求先コードを変更してチェック
            selectDt.Rows(selectDt.Rows.Count - 1).Item("SEIQTO_CD") = seiqtoCd
            rtnResult = rtnResult AndAlso Me.ChkSeiqDate(ds, inDate, outDate, dr.Item("UNTIN_CALCULATION_KB").ToString(), Me.SelectGheaderData(selectDs), moto, msg, rowNo, chkErrFlg)

        End If

        Return rtnResult

    End Function

    ''' <summary>
    ''' 最終請求日のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDate">納入日</param>
    ''' <param name="outDate">出荷日</param>
    ''' <param name="calcKbn">締め基準</param>
    ''' <param name="chkDate">請求日</param>
    ''' <param name="moto">元データ区分</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="chkErrFlg">チェックフラグ　True:通常のエラーチェック　False:一括更新のエラーチェック</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkSeiqDate(ByVal ds As DataSet _
                                     , ByVal inDate As String _
                                     , ByVal outDate As String _
                                     , ByVal calcKbn As String _
                                     , ByVal chkDate As String _
                                     , ByVal moto As String _
                                     , ByVal msg As String _
                                     , ByVal rowNo As String _
                                     , ByVal chkErrFlg As Boolean _
                                     ) As Boolean

        Select Case moto

            Case LMF040BLC.MOTO_DATA_NYUKA

                '元データ = 入荷は納入日とチェック
                Return Me.ChkDate(ds, inDate, chkDate, msg, rowNo, chkErrFlg)

            Case Else

                Select Case calcKbn

                    Case LMF040BLC.CALC_SHUKKA

                        '運賃計算締め基準によるチェック(出荷日)
                        Return Me.ChkDate(ds, outDate, chkDate, msg, rowNo, chkErrFlg)

                    Case LMF040BLC.CALC_NYUKA

                        '運賃計算締め基準によるチェック(納入日)
                        Return Me.ChkDate(ds, inDate, chkDate, msg, rowNo, chkErrFlg)

                End Select

        End Select

        Return True

    End Function

    ''' <summary>
    ''' 日付チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="value1">比較対象日</param>
    ''' <param name="value2">最終締め日</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <param name="chkErrFlg">チェックフラグ　True:通常のエラーチェック　False:一括更新のエラーチェック</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkDate(ByVal ds As DataSet _
                             , ByVal value1 As String _
                             , ByVal value2 As String _
                             , ByVal msg As String _
                             , ByVal rowNo As String _
                             , ByVal chkErrFlg As Boolean _
                             ) As Boolean

        '比較対象1に値がない場合、スルー
        If String.IsNullOrEmpty(value1) = True Then
            Return True
        End If

        'すでに締め処理が締め処理が終了している場合、エラー
        If value1 <= value2 Then

            '要望番号:1045 terakawa 2013.03.28 Start
            '新黒存在チェック用データセット作成
            Dim dr As DataRow = ds.Tables(LMF040BLC.TABLE_NM_G_HED_CHK).NewRow
            dr.Item("NRS_BR_CD") = ds.Tables(LMF040BLC.TABLE_NM_UNCHIN).Rows(0).Item("NRS_BR_CD")
            dr.Item("SEIQ_TARIFF_BUNRUI_KB") = ds.Tables(LMF040BLC.TABLE_NM_UNCHIN).Rows(0).Item("SEIQ_TARIFF_BUNRUI_KB")
            dr.Item("SEIQTO_CD") = ds.Tables(LMF040BLC.TABLE_NM_UNCHIN).Rows(0).Item("SEIQTO_CD")
            dr.Item("SKYU_DATE") = value1

            ds.Tables(LMF040BLC.TABLE_NM_G_HED_CHK).Rows.Add(dr)

            '新黒存在チェック
            ds = Me.DacAccess(ds, "NewKuroExistChk")
            If MyBase.GetResultCount() >= 1 Then

                '請求期間内チェック
                ds = Me.DacAccess(ds, "InSkyuDateChk")
                If MyBase.GetResultCount() >= 1 Then

                    Return True
                End If

            End If
            '要望番号:1045 terakawa 2013.03.28 End

            If chkErrFlg = True Then
                MyBase.SetMessage("E232", New String() {LMF040BLC.TORIKOMI, msg})
            Else
                MyBase.SetMessageStore(LMF040BLC.GUIDANCE_KBN, "E232", New String() {LMF040BLC.TORIKOMI, msg}, rowNo)
            End If
            Return False

        End If

        Return True

    End Function

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
        Return Not MyBase.IsMessageStoreExist( Convert.ToInt32(ds.Tables(LMF040BLC.TABLE_NM_UNCHIN).Rows(0).Item("ROW_NO").ToString()))

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

        Dim drs As DataRow() = ds.Tables(LMF040BLC.TABLE_NM_OUT).Select(String.Concat("GROUP_FLG = '0'"))
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

        '締め日基準が入荷の場合
        If LMF040BLC.CALC_NYUKA.Equals(dr.Item("UNTIN_CALCULATION_KB").ToString()) = True Then

            GetUnsoDate = dr.Item("ARR_PLAN_DATE").ToString()

        Else

            GetUnsoDate = dr.Item("OUTKA_PLAN_DATE").ToString()

        End If

        Return GetUnsoDate

    End Function

    ''' <summary>
    ''' 運行番号を取得
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="tyukeiFlg">中継配送フラグ</param>
    ''' <returns>運行番号</returns>
    ''' <remarks>
    ''' 1行目：運行番号 or 運行番号(集荷)
    ''' 2行目：Nothing  or 運行番号(中継)
    ''' 3行目：Nothing  or 運行番号(配荷)
    ''' </remarks>
    Private Function GetTripNo(ByVal dr As DataRow, ByVal tyukeiFlg As String) As String()

        GetTripNo = Nothing

        '中継配送有の場合、運行番号(配荷)
        If LMF040BLC.FLG_ON.Equals(tyukeiFlg) = True Then
            Return New String() {dr.Item("TRIP_NO_SYUKA").ToString(), dr.Item("TRIP_NO_TYUKEI").ToString(), dr.Item("TRIP_NO_HAIKA").ToString()}
        End If

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
    ''' <param name="tyukeiFlg1">中継配送フラグ1</param>
    ''' <param name="value2">値2</param>
    ''' <param name="tyukeiFlg2">中継配送フラグ2</param>
    ''' <returns>値が同じ場合、True　全て違う場合、False</returns>
    ''' <remarks></remarks>
    Private Function ChkTripNo(ByVal value1 As String(), ByVal tyukeiFlg1 As String, ByVal value2 As String(), ByVal tyukeiFlg2 As String) As Boolean

        Dim max1 As Integer = value1.Count - 1
        Dim max2 As Integer = value2.Count - 1

        'どちらかが中継配送無の場合、1番後ろと比較
        If LMF040BLC.FLG_ON.Equals(tyukeiFlg1) = False _
            OrElse LMF040BLC.FLG_ON.Equals(tyukeiFlg2) = False _
            Then
            Return value1(max1).Equals(value2(max2))
        End If

        '以下の処理は両方とも中継配送有のレコード
        'たて列が同じ場合、True
        For i As Integer = 0 To max1
            If value1(i).Equals(value2(i)) = True Then
                Return True
            End If
        Next

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
        Dim drs As DataRow() = ds.Tables(LMF040BLC.TABLE_NM_UNCHIN).Select(String.Concat("ROW_NO <> '", rowNo, "' "), " ROW_NO ")
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

            MyBase.SetMessageStore(LMF040BLC.GUIDANCE_KBN, "E380", errMsg, chkData)

        Next

        Return ds

    End Function

#End Region

End Class
