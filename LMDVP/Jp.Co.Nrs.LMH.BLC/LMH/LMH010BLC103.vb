' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH010    : EDI入荷検索
'  EDI荷主ID　　　　:  103　　　 : 富士フイルム(千葉)
'  作  成  者       :  terakawa
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
'↓FFEM特殊処理↓
'2014.06.09 使用START
Imports Jp.Co.Nrs.LM.DSL
'↑FFEM特殊処理↑
'2014.06.09 使用END
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH010BLC103
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH010DAC103 = New LMH010DAC103()

    Private _MstDac As LMH010DAC = New LMH010DAC()

    Private _ChkBlc As LMH010BLC = New LMH010BLC()

    '↓FFEM特殊処理↓
    '2014.06.09 使用START
    Private _MotoDelDac As LMB020DAC = New LMB020DAC()
    '↑FFEM特殊処理↑
    '2014.06.09 使用END

    '2014.09.17 追加START
    Private _ZaiRecNo As String = String.Empty
    '2014.09.17 追加END

#End Region

#Region "CONST"

    ' ''' <summary>
    ' ''' CONST
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Const FREE_C03 As String = "B150"  '入庫コード(ZFVYKWERKS) 

    '伝票タイプ
    Public Const KAKUAGE As String = "305"      'プラント転送入庫(高取格上)
    Public Const CENTER_INKA As String = "Z01"  '原価センター入庫
    Public Const TENSO_ORDER As String = "ZUB1"  '在庫転送オーダー

    '出荷条件
    Public Const OUTKA_TERMS As String = "Z3"  '出荷条件

#End Region

#Region "Method"

#Region "入荷登録処理"

#Region "入荷登録"
    ''' <summary>
    ''' 入荷登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InkaToroku(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("EDI_CTL_NO").ToString()

        Dim custIndex As Integer = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(0).Item("EDI_CUST_INDEX"))

        '↓FFEM特殊処理↓
        '実績データのキャンセル報告対応
        '2014.06.09 追加START
        Dim canHokokuF As Boolean = False           'キャンセル報告フラグ
        'Dim henHokokuF As Boolean = False           '変更報告フラグ
        '2014.06.09 追加END

        'EDI入荷(大)の取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiL", ds)

        '追加開始 --- kikuchi 2014.09.18
        '②入荷データ(元黒)の抽出処理
        'ds = Me.SelectInitData(ds)

        ' '' '' ''③元黒が存在する場合元黒の削除を促すメッセージを表示してエラーとする。
        '' '' ''If ds.Tables("LMB020_INKA_L").Rows.Count > 0 Then
        '' '' ''    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, String.Concat(ediCtlNo, "test"))
        '' '' ''    Return ds
        '' '' ''End If
        '追加終了 ---

        If ds.Tables("LMH010_INKAEDI_L").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds

            '↓FFEM特殊処理↓
            '2014.06.09 追加START
        ElseIf ds.Tables("LMH010_INKAEDI_L").Rows.Count = 1 AndAlso _
            ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("DEL_KB").ToString() = "2" Then
            canHokokuF = True

            'ElseIf ds.Tables("LMH010_INKAEDI_L").Rows.Count = 1 AndAlso _
            '    ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("DEL_KB").ToString() = "3" Then
            '    henHokokuF = True
            '↑FFEM特殊処理↑
            '2014.06.09 追加END
        End If

        'EDI入荷(中)の取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiM", ds)

        If ds.Tables("LMH010_INKAEDI_M").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If


        '①キャンセルデータの元黒データ抽出し、存在する場合エラーとする　2014/09/18
        ds = MyBase.CallDAC(Me._Dac, "SelectOrderMotoData", ds)
        If MyBase.GetResultCount > 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E377", New String() {"入荷データ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

#If False Then  'UPD 2021/09/06 023522   【LMS】安田倉庫移転_PG改修点洗い出し_改修(営業荻山) 
        If "96".Equals(ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("NRS_BR_CD")) OrElse "98".Equals(ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("NRS_BR_CD")) Then
            'FFEM大分工場(営業所コード='96') or 大牟田工場(営業所コード='98')の場合
#Else
        If "96".Equals(ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("NRS_BR_CD")) OrElse "98".Equals(ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("NRS_BR_CD")) OrElse "F1".Equals(ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("NRS_BR_CD")) _
                     OrElse "F2".Equals(ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("NRS_BR_CD")) _
                     OrElse "F3".Equals(ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("NRS_BR_CD")) Then   'ADD 2022/10/19 033380   【LMS】FFEM足柄工場LMS導入 F2追加    'ADD 2023/12/25 039659 F3 追加
            'FFEM大分工場(営業所コード='96') or 大牟田工場(営業所コード='98' or 安田倉庫(営業所コード='F1') or 足柄工場(営業所コード='F2') or 熊本工場(営業所コード='F3')の場合
#End If


            Dim inkaEdiMRow As DataRow = ds.Tables("LMH010_INKAEDI_M").Rows(0)
            Dim kbnNm1 As String
            If "5".Equals(inkaEdiMRow.Item("FREE_C20")) Then
                '引当計上予実区分が5:在庫転送の場合、「保管場所（振替後）」を参照
                kbnNm1 = inkaEdiMRow.Item("FREE_C21").ToString
            Else
                'その他の場合、「保管場所」を参照
                kbnNm1 = inkaEdiMRow.Item("FREE_C04").ToString
            End If

            '区分マスタF029(保管場所コード(FFEM))より対応する倉庫コードを取得
            Dim zKbnInDt As DataTable = ds.Tables("LMH010_Z_KBN_IN")
            Dim zKbnOutDt As DataTable = ds.Tables("LMH010_Z_KBN_OUT")
            zKbnInDt.Clear()
            zKbnOutDt.Clear()
            Dim zKbnInDr As DataRow = zKbnInDt.NewRow
#If False Then  'UPD 2020/03/04 011299   【LMS】FFEM大牟田対応_EDI入出荷登録プラントコード＋保管場所別登録（FFEM渡邉様）
            zKbnInDr.Item("KBN_GROUP_CD") = "F029"
            zKbnInDr.Item("KBN_NM1") = kbnNm1

#Else
            zKbnInDr.Item("KBN_GROUP_CD") = "F030"
            zKbnInDr.Item("KBN_NM2") = kbnNm1

            zKbnInDr.Item("KBN_NM1") = inkaEdiMRow.Item("FREE_C03").ToString   'プラントコード
            zKbnInDr.Item("KBN_NM4") = inkaEdiMRow.Item("NRS_BR_CD").ToString   '営業所コード

#End If
            zKbnInDt.Rows.Add(zKbnInDr)

            ds = MyBase.CallDAC(Me._MstDac, "SelectZKbnHanyo", ds)

            If zKbnOutDt.Rows.Count >= 1 Then
                '区分マスタより取得した倉庫コードを設定
#If False Then  'UPD 2020/03/04 011299   【LMS】FFEM大牟田対応_EDI入出荷登録プラントコード＋保管場所別登録（FFEM渡邉様）
                ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("NRS_WH_CD") = zKbnOutDt.Rows(0).Item("KBN_NM4").ToString

#Else
                ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("NRS_WH_CD") = zKbnOutDt.Rows(0).Item("KBN_NM5").ToString

#End If
            Else
                '区分マスタに該当なしの場合、固定値を設定
                ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("NRS_WH_CD") = "XXX"
            End If
        End If


        '↓FFEM特殊処理↓
        '2014.06.09 追加START
        If canHokokuF = True Then

            '伝票タイプ(FREE_C16)が以下の場合は実績なし　※高取倉庫,千葉BC共に
            Select Case ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("FREE_C16").ToString()

                Case LMH010BLC103.KAKUAGE, LMH010BLC103.CENTER_INKA

                Case LMH010BLC103.TENSO_ORDER

                    '出荷条件(FREE_C18)が"Z3"の場合は実績なし　※高取倉庫,千葉BC共に
                    If ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("FREE_C18").ToString().Equals(LMH010BLC103.OUTKA_TERMS) = True Then

                    Else
                        'データセット設定処理(受信ヘッダ)
                        ds = Me.SetDatasetRcvHed(ds, canHokokuF)

                        'データセット設定処理(受信明細)
                        ds = Me.SetDatasetRcvDtl(ds, canHokokuF)

                        '①キャンセルデータ実績作成処理
                        If Me.JissekiSakusei(ds) = False Then
                            Return ds
                        End If

                    End If

                Case Else

                    'データセット設定処理(受信ヘッダ)
                    ds = Me.SetDatasetRcvHed(ds, canHokokuF)

                    'データセット設定処理(受信明細)
                    ds = Me.SetDatasetRcvDtl(ds, canHokokuF)

                    '①キャンセルデータ実績作成処理
                    If Me.JissekiSakusei(ds) = False Then
                        Return ds
                    End If

            End Select

            ''高取データで伝票タイプが格上げ("305")の場合は
            'If custIndex = LMH010BLC.EdiCustIndex.FjfTaka00195_00 AndAlso ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("FREE_C16").ToString().Equals(KAKUAGE) = True Then


            '    ''2014/09/18 格上げは再度こない＆重複は手前でエラーチェック追加したので、削除
            '    '①キャンセルデータの元黒データ抽出
            '    'ds = MyBase.CallDAC(Me._Dac, "SelectOrderMotoData", ds)
            '    'If ds.Tables("H_SENDINEDI_FJF").Rows.Count = 0 Then
            '    '    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            '    '    Return ds
            '    'End If

            'Else

            '    'データセット設定処理(受信ヘッダ)
            '    ds = Me.SetDatasetRcvHed(ds, canHokokuF)

            '    'データセット設定処理(受信明細)
            '    ds = Me.SetDatasetRcvDtl(ds, canHokokuF)

            '    '①キャンセルデータ実績作成処理
            '    If Me.JissekiSakusei(ds) = False Then
            '        Return ds
            '    End If

            'End If

            'Dim rtDs As DataSet = New LMB020DS()

            'Dim dr As DataRow = rtDs.Tables("LMB020IN").NewRow()

            ''暫定的に先頭をセットしてみる
            'dr.Item("INKA_NO_L") = ds.Tables("H_SENDINEDI_FJF").Rows(0).Item("INKA_CTL_NO_L").ToString() '管理番号
            'dr.Item("NRS_BR_CD") = ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("NRS_BR_CD").ToString() '営業所コード
            'dr.Item("CUST_CD_L") = ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("CUST_CD_L").ToString() '荷主コード（大）
            'dr.Item("CUST_CD_M") = ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("CUST_CD_M").ToString() '荷主コード（中）
            'dr.Item("CUST_NM") = ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("CUST_NM_L").ToString()   '荷主名

            'rtDs.Tables("LMB020IN").Rows.Add(dr)

            ''削除開始 --- 2014.09.18 kikuchi 元黒の削除を手動で行う運用となったため。
            ' ''②入荷データ(元黒)の抽出処理
            'rtDs = Me.SelectInitData(rtDs)

            ''③削除処理チェック(LMB020クライアント側)
            'If Me.IsDeleteChk(rtDs) = False Then
            '    Return ds
            'End If

            ' ''③入荷データ(元黒)の削除処理
            'If Me.DeleteData(rtDs) = False Then
            '    Return ds
            'End If
            ''削除終了 ---

            Return ds
        End If
        '↑FFEM特殊処理↑
        '2014.06.09 追加END

        'EDI入荷(大)のタリフ設定を行う
        ds = Me.SetTariff(ds)

        'EDI入荷(大)の関連チェックを行う
        If Me.InkaLKanrenCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        'EDI入荷(中)の関連チェックを行う
        If Me.InkaMKanrenCheck(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        'DB存在チェック(大)
        If Me.DbCheckL(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        '商品マスタ値取得、EDI入荷(中)編集
        If Me.SetGoodsMst(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        '風袋の取得
        If Me.SetPkgUtZkbn(ds, rowNo, ediCtlNo) = False Then
            Return ds
        End If

        ''EDI入荷(中)のチェック(標準入目)を行う
        'If Me.InkaMIrimeCheck(ds, rowNo, ediCtlNo) = False Then
        '    Return ds
        'End If

        '入荷管理番号(大)の設定
        ds = Me.GetInkaNoL(ds)

        '入荷管理番号(中)の設定
        ds = Me.GetInkaNoM(ds)

        Dim eventShubetsu As String = ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()

        If eventShubetsu.Equals("3") Then
            '紐付処理をして終了
            ds = Me.Himoduke(ds)
            Return ds
        End If

        'データセット設定処理(入荷L)
        ds = Me.SetDatasetInkaL(ds)

        'データセット設定処理(入荷M)
        ds = Me.SetDatasetInkaM(ds)

        '伝票タイプ(FREE_C16)が以下の場合は入荷(小),在庫を作成　※高取倉庫,千葉BC共に
        Select Case ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("FREE_C16").ToString()

            '格上げ,原価センター入庫
            Case LMH010BLC103.KAKUAGE, LMH010BLC103.CENTER_INKA

                'データセット設定処理(入荷S,在庫)
                ds = Me.SetDatasetInkaSZaiRec(ds)

            Case LMH010BLC103.TENSO_ORDER

                '出荷条件(FREE_C18)が"Z3"の場合は入荷(小),在庫を作成　※高取倉庫,千葉BC共に
                If ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("FREE_C18").ToString().Equals(LMH010BLC103.OUTKA_TERMS) = True Then

                    'データセット設定処理(入荷S,在庫)
                    ds = Me.SetDatasetInkaSZaiRec(ds)

                End If

            Case Else

        End Select

        ''高取データで伝票タイプが格上げ("305")の場合は、入荷(小)・在庫を作成する
        'If custIndex = LMH010BLC.EdiCustIndex.FjfTaka00195_00 AndAlso ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("FREE_C16").ToString().Equals(KAKUAGE) = True Then

        '    ''データセット設定処理(入荷S)
        '    'ds = Me.SetDatasetInkaS(ds)

        '    ''データセット設定処理(在庫)
        '    'ds = Me.SetDatasetZaiTrs(ds)

        '    'データセット設定処理(入荷S,在庫)
        '    ds = Me.SetDatasetInkaSZaiRec(ds)

        'End If

        '富士フイルム追加箇所 terakawa 2012.08.02 Start
        'データセット設定処理(受信ヘッダ)
        ds = Me.SetDatasetRcvHed(ds)
        '富士フイルム追加箇所 terakawa 2012.08.02 End

        'データセット設定処理(受信明細)
        ds = Me.SetDatasetRcvDtl(ds)

        'データセット設定処理(作業)
        ds = Me.SetDatasetSagyo(ds)

        'データセット設定(運送大,中)
        If ds.Tables("LMH010_INKAEDI_L").Rows(0)("UNCHIN_TP").ToString() = "10" Then
            ds = Me.SetDatasetUnsoL(ds)
            ds = Me.SetDatasetUnsoM(ds)
            ds = Me.SetdatasetUnsoJyuryo(ds)

            '運送Lの関連チェック
            If Me.UnsoCheck(ds, rowNo, ediCtlNo) = False Then
                Return ds
            End If

        End If

        '↓FFEM特殊処理↓
        '2014.06.09 修正START
        'If henHokokuF = True Then

        '    '変更データ実績作成処理
        '    If Me.JissekiSakusei(ds) = False Then
        '        Return ds
        '    End If
        'Else

        'タブレット項目初期値設定
        ds = SetDatasetInkaLTabletData(ds)

        'EDI入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI入荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)

        '富士フイルム追加箇所 terakawa 2012.08.02 Start
        '受信ヘッダの更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If
        '富士フイルム追加箇所 terakawa 2012.08.02 End

        '受信明細の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If
        'End If
        '↑FFEM特殊処理↑
        '2014.06.09 修正START

        '入荷Lの作成
        ds = MyBase.CallDAC(Me._Dac, "InsertInkaL", ds)

        '入荷Mの作成
        ds = MyBase.CallDAC(Me._Dac, "InsertInkaM", ds)

        '入荷Sの作成
        'ds = MyBase.CallDAC(Me._Dac, "InsertInkaS", ds)

        ''高取データで伝票タイプが格上げ("305")の場合は、入荷(小)・在庫を作成する
        'If custIndex = LMH010BLC.EdiCustIndex.FjfTaka00195_00 AndAlso ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("FREE_C16").ToString().Equals(KAKUAGE) = True Then

        '    '入荷Sの作成
        '    ds = MyBase.CallDAC(Me._Dac, "InsertInkaS", ds)

        '    '在庫TRSの作成
        '    ds = MyBase.CallDAC(Me._Dac, "InsertZaiTrs", ds)

        'End If

        '伝票タイプ(FREE_C16)が以下の場合は入荷(小),在庫を作成　※高取倉庫,千葉BC共に
        Select Case ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("FREE_C16").ToString()

            '格上げ,原価センター入庫
            Case LMH010BLC103.KAKUAGE, LMH010BLC103.CENTER_INKA

                '入荷Sの作成
                ds = MyBase.CallDAC(Me._Dac, "InsertInkaS", ds)

                '在庫TRSの作成
                ds = MyBase.CallDAC(Me._Dac, "InsertZaiTrs", ds)

            Case LMH010BLC103.TENSO_ORDER

                '出荷条件(FREE_C18)が"Z3"の場合は入荷(小),在庫を作成　※高取倉庫,千葉BC共に
                If ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("FREE_C18").ToString().Equals(LMH010BLC103.OUTKA_TERMS) = True Then

                    '入荷Sの作成
                    ds = MyBase.CallDAC(Me._Dac, "InsertInkaS", ds)

                    '在庫TRSの作成
                    ds = MyBase.CallDAC(Me._Dac, "InsertZaiTrs", ds)

                End If

            Case Else

        End Select

        'If ds.Tables("LMH010_INKAEDI_M").Rows(0).Item("FREE_C03").ToString().Equals(FREE_C03) = False Then
        'End If

        '作業の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH010_E_SAGYO").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertSagyo", ds)
        End If

        '運送(大)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH010_UNSO_L").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoL", ds)
        End If

        '運送(中)の新規登録(データセットに設定されている場合のみ)
        If ds.Tables("LMH010_UNSO_M").Rows.Count <> 0 Then
            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoM", ds)
        End If

        Return ds

    End Function
#End Region

#Region "タリフ設定処理"
    ''' <summary>
    ''' タリフ設定処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetTariff(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim yokoTariff As String = dr.Item("YOKO_TARIFF_CD").ToString()
        Dim freeC29 As String = dr.Item("FREE_C29").ToString()
        Dim unchinTp As String = dr.Item("UNCHIN_TP").ToString()
        Dim unchinKb As String = dr.Item("UNCHIN_KB").ToString()
        Dim yokoTariffCd As String = String.Empty

        If String.IsNullOrEmpty(freeC29) = True Then
            freeC29 = "0"
        End If

        '日陸手配かつタリフ分類区分が空の場合、マスタ値を入れる
        If unchinTp = "10" AndAlso String.IsNullOrEmpty(unchinKb) = True Then
            ds = MyBase.CallDAC(Me._MstDac, "SelectDataTariffBunrui", ds)
        End If

        '横持ちタリフが空もしくはFREE_C29の1文字目が"0"または空の場合で
        '日陸手配かつ横持ちの場合はマスタ値を入れる
        If String.IsNullOrEmpty(yokoTariff) OrElse freeC29.Substring(0, 1) = "0" Then

            If unchinTp = "10" AndAlso unchinKb = "40" Then
                ds = MyBase.CallDAC(Me._MstDac, "SelectDataUnchinTariffSet", ds)
            End If

        End If

        Return ds

    End Function

#End Region

#Region "入荷登録処理(運賃作成)"

    ''' <summary>
    ''' 入荷登録処理(運賃作成)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UnchinSakusei(ByVal ds As DataSet) As DataSet

        '運賃の新規作成
        ds = MyBase.CallDAC(Me._Dac, "InsertUnchinData", ds)

        Return ds

    End Function

#End Region

#Region "入荷登録関連チェック"

    ''' <summary>
    ''' 入荷登録関連チェック(EDI_L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InkaLKanrenCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtEdiL As DataTable = ds.Tables("LMH010_INKAEDI_L")

        'EDI管理番号(大)のチェック
        If _ChkBlc.EdiLCheck(dtEdiL) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E349", New String() {"EDIデータ", "入荷管理番号"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E349", New String() {"EDIデータ", "入荷管理番号"})
            Return False
        End If

        '入荷日チェック
        If _ChkBlc.InkaDateCheck(dtEdiL) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E047", New String() {"入荷日"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E047", New String() {"入荷日"})
            Return False
        End If

        '保管料起算日チェック
        If _ChkBlc.HokanStrDateCheck(dtEdiL) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E047", New String() {"保管料起算日"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E047", New String() {"保管料起算日"})
            Return False
        End If

        '荷主コードL
        If _ChkBlc.CustCdLCheck(dtEdiL) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(大)"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E019", New String() {"荷主コード(大)"})
            Return False
        End If

        '荷主コードM
        If _ChkBlc.CustCdLCheck(dtEdiL) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E019", New String() {"荷主コード(中)"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E019", New String() {"荷主コード(中)"})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 入荷登録関連チェック(EDI_M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InkaMKanrenCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean


        Dim dtEdiM As DataTable = ds.Tables("LMH010_INKAEDI_M")

        '赤黒区分チェク
        If _ChkBlc.AkakuroCheck(dtEdiM) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E320", New String() {"赤データ", "入荷登録"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E320", New String() {"赤データ", "入荷登録"})
            Return False
        End If

        '個数チェック
        If _ChkBlc.NbCheck(dtEdiM) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E320", New String() {"マイナスデータ", "入荷登録"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E320", New String() {"マイナスデータ", "入荷登録"})
            Return False
        End If

        '商品チェック
        If _ChkBlc.GoodsCdCheck(dtEdiM) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E019", New String() {"商品コード"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E320", New String() {"マイナスデータ", "入荷登録"})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 入荷登録入目チェック(EDI_M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InkaMIrimeCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean


        Dim dtEdiM As DataTable = ds.Tables("LMH010_INKAEDI_M")

        '標準入目チェック
        If _ChkBlc.StdIrimeCheck(dtEdiM) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E333", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E333")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 入荷登録関連チェック(運送)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="rowNo"></param>
    ''' <param name="ediCtlNo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UnsoCheck(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtUnsoL As DataTable = ds.Tables("LMH010_UNSO_L")

        '運送重量チェック
        If _ChkBlc.UnsoJuryoCheck(dtUnsoL) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E424", New String() {LMH010BLC.MAX_UNSOWT, "入荷登録"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        Return True

    End Function

#End Region

#Region "入荷登録DB存在チェック(大)"
    Private Function DbCheckL(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim drJudge As DataRow = ds.Tables("LMH010_JUDGE").Rows(0)
        Dim drEdiL As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)

        '入荷種別
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N007
        drJudge("KBN_CD") = drEdiL("INKA_TP")

        Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"入荷種別", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"入荷種別", "区分マスタ"})
            Return False
        End If

        '入荷区分
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N006
        drJudge("KBN_CD") = drEdiL("INKA_KB")

        Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"入荷区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '進捗区分
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N004
        drJudge("KBN_CD") = drEdiL("INKA_STATE_KB")

        Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"進捗区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '倉庫
        Call MyBase.CallDAC(Me._MstDac, "SelectDataSoko", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"倉庫コード", "倉庫マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '荷主マスタ
        Call MyBase.CallDAC(Me._MstDac, "SelectDataCust", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"荷主コード", "荷主マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '課税区分
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_Z001
        drJudge("KBN_CD") = drEdiL("TAX_KB")
        Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"課税区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '運送手配区分
        If String.IsNullOrEmpty(drEdiL("UNCHIN_TP").ToString()) = False Then
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U005
            drJudge("KBN_CD") = drEdiL("UNCHIN_TP")
            Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"運送手配区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        'タリフ分類区分
        If String.IsNullOrEmpty(drEdiL("UNCHIN_KB").ToString()) = False Then
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_T015
            drJudge("KBN_CD") = drEdiL("UNCHIN_KB")
            Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"タリフ分類区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If
#If False Then  'ADD 2020/08/03 014417   【LMS】FFEM_入荷登録時の入荷元チェックを外して欲しい(営業吉川)
            '届先コード
            If String.IsNullOrEmpty(drEdiL("OUTKA_MOTO").ToString()) = False Then
                Call MyBase.CallDAC(Me._MstDac, "SelectDataDest", ds)

                If MyBase.GetResultCount = 0 Then
                    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"届先コード", "届先マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return False
                End If
            End If
#End If

        '車両区分
        If String.IsNullOrEmpty(drEdiL("SYARYO_KB").ToString()) = False Then
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S012
            drJudge("KBN_CD") = drEdiL("SYARYO_KB")
            Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"車両区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        '運送温度区分
        If String.IsNullOrEmpty(drEdiL("UNSO_ONDO_KB").ToString()) = False Then
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U006
            drJudge("KBN_CD") = drEdiL("UNSO_ONDO_KB")
            Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"運送温度区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        '運送会社コード
        If String.IsNullOrEmpty(drEdiL("UNSO_CD").ToString()) = False OrElse String.IsNullOrEmpty(drEdiL("UNSO_BR_CD").ToString()) = False Then

            If String.IsNullOrEmpty(drEdiL("UNSO_CD").ToString()) = True Then
                drEdiL("UNSO_CD") = String.Empty
            End If

            If String.IsNullOrEmpty(drEdiL("UNSO_BR_CD").ToString()) = True Then
                drEdiL("UNSO_BR_CD") = String.Empty
            End If

            Call MyBase.CallDAC(Me._MstDac, "SelectDataUnsoco", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"運送会社コード", "運送会社マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If
        End If

        'タリフコード
        If String.IsNullOrEmpty(drEdiL("YOKO_TARIFF_CD").ToString()) = False Then

            Dim unchinKB As String = drEdiL("UNCHIN_KB").ToString()

            Select Case unchinKB
                Case "10", "20", "50"

                    Call MyBase.CallDAC(Me._MstDac, "SelectDataUnchinTariff", ds)

                Case "40"

                    Call MyBase.CallDAC(Me._MstDac, "SelectDataYokoTariff", ds)

            End Select

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"タリフコード", "マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                Return False
            End If

        End If

        Dim drIn As DataRow = ds.Tables("LMH010INOUT").Rows(0)

        'オーダー番号重複チェック
        If String.IsNullOrEmpty(drEdiL("OUTKA_FROM_ORD_NO").ToString()) = False Then

            If drIn("ORDER_CHECK_FLG").Equals("1") = True Then
                Call MyBase.CallDAC(Me._MstDac, "SelectOrderCheckData", ds)
                If MyBase.GetResultCount > 0 Then
                    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E377", New String() {"入荷データ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return False
                End If

            End If
        End If

        Return True

    End Function
#End Region

#Region "入荷登録マスタ存在チェック(中)"
    Private Function SetGoodsMst(ByRef ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dtM As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim dtL As DataTable = ds.Tables("LMH010_INKAEDI_L")
        Dim max As Integer = dtM.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtM As DataTable = setDs.Tables("LMH010_INKAEDI_M")
        Dim setDtL As DataTable = setDs.Tables("LMH010_INKAEDI_L")
        Dim goodsDt As DataTable = setDs.Tables("LMH010_M_GOODS")

        Dim flgWarning As Boolean = False 'ワーニングフラグ
        Dim msgArray(5) As String
        Dim ediName As String = String.Empty
        Dim ediValue As String = String.Empty
        Dim mustValue As String = String.Empty
        Dim choiceKb As String = String.Empty

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            setDtM.ImportRow(dtM.Rows(i))
            setDtL.ImportRow(dtL.Rows(0))

            '↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
            '富士フイルム専用チェック
            '富士フイルム品目マスタ検索（荷主商品コード）
            setDs = (MyBase.CallDAC(Me._Dac, "SelectDataHinmokuFjf", setDs))

            If MyBase.GetResultCount() > 0 Then
                '未反映データが１件以上ある場合エラー
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E160", _
                                       New String() {String.Concat("荷主商品コード：", _
                                                                   setDtM.Rows(0).Item("CUST_GOODS_CD").ToString(), _
                                                                   " 商品名：", _
                                                                   setDtM.Rows(0).Item("GOODS_NM").ToString(), _
                                                                   "は、", _
                                                                   "富士フイルム品目マスタ"), "未反映データ"}, _
                                                                   rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)

                'このレコードのワーニングをクリア
                ds.Tables("WARNING_DTL").Rows.Clear()

                '要望 修正START 商品データがない場合、毎回エラー表示される回避対応
                '①falseで抜けない(コメント化)
                '②ワーニングフラグを使用(エラーだが同様に使用する)
                '③その明細の処理は抜け、次明細の処理を行う
                'Return False

                flgWarning = True 'ワーニングフラグをたてて処理続行
                Continue For
                '要望 修正END

            End If

            '↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

            '条件の再設定
            setDtM = Me.SetGoodsCdFromWarning(setDtM, ds, LMH010BLC.FJF_WID_M001)

            '商品マスタ検索（NRS商品コード or 荷主商品コード）
            setDs = (MyBase.CallDAC(Me._MstDac, "SelectDataGoods", setDs))

            If MyBase.GetResultCount = 0 Then
                '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
                'MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"商品", "商品マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                Dim sErrMsg As String = Me._ChkBlc.GetErrMsgE493(setDs)
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", sErrMsg}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End

                'このレコードのワーニングをクリア
                ds.Tables("WARNING_DTL").Rows.Clear()
                Return False

            ElseIf GetResultCount() > 1 Then

                '入目 + 荷主商品コードで再検索
                setDs = (MyBase.CallDAC(Me._MstDac, "SelectDataGoods2", setDs))

                If MyBase.GetResultCount = 1 Then
                Else
                    '再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                    ds = Me._ChkBlc.SetWarningM("W162", LMH010BLC.FJF_WID_M001, ds, setDs, msgArray)
                    flgWarning = True 'ワーニングフラグをたてて処理続行
                    Continue For
                End If

            End If

            'NRS商品キー
            dtM.Rows(i)("NRS_GOODS_CD") = goodsDt.Rows(0)("GOODS_CD_NRS")

            '↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
            '富士フイルム専用チェック

            '①商品名
            '送られてきた商品名を優先する為、商品マスタの商品名との整合性チェックは行わない
            '送られてきた商品名が空の場合のみセット
            If String.IsNullOrEmpty(dtM.Rows(i)("GOODS_NM").ToString()) = True Then
                dtM.Rows(i)("GOODS_NM") = goodsDt.Rows(0)("GOODS_NM_1")
            End If

            'If String.IsNullOrEmpty(dtM.Rows(i)("GOODS_NM").ToString()) = False Then
            '    If dtM.Rows(i)("GOODS_NM").ToString().Equals(goodsDt.Rows(0)("GOODS_NM_1").ToString()) = False Then
            '        choiceKb = Me._ChkBlc.GetWarningChoiceKbM(ds.Tables("LMH010_INKAEDI_M"), ds, LMH010BLC.FJF_WID_M002, i)

            '        If String.IsNullOrEmpty(choiceKb) = True Then

            '            ediName = "商品名"
            '            ediValue = dtM.Rows(i)("GOODS_NM").ToString()
            '            mustValue = goodsDt.Rows(0)("GOODS_NM_1").ToString()
            '            msgArray(1) = "商品名"
            '            msgArray(2) = "商品マスタ"
            '            msgArray(3) = "商品名"
            '            msgArray(4) = "EDIデータ"
            '            msgArray(5) = String.Empty

            '            ds = Me._ChkBlc.SetWarningM2("W159", LMH010BLC.FJF_WID_M002, ds, setDs, msgArray, ediName, ediValue, mustValue)
            '            flgWarning = True 'ワーニングフラグをたてて処理続行
            '        End If

            '        dtM.Rows(i)("GOODS_NM") = goodsDt.Rows(0)("GOODS_NM_1")
            '    End If
            'End If

            '②個数単位
            If String.IsNullOrEmpty(dtM.Rows(i)("NB_UT").ToString()) = True Then
                dtM.Rows(i)("NB_UT") = goodsDt.Rows(0)("NB_UT")
            Else
                If (dtM.Rows(i)("NB_UT").ToString()).Equals(goodsDt.Rows(0)("NB_UT").ToString()) = False Then

                    choiceKb = Me._ChkBlc.GetWarningChoiceKbM(ds.Tables("LMH010_INKAEDI_M"), ds, LMH010BLC.FJF_WID_M003, i)

                    If String.IsNullOrEmpty(choiceKb) = True Then

                        ediName = "個数単位"
                        ediValue = dtM.Rows(i)("NB_UT").ToString()
                        mustValue = goodsDt.Rows(0)("NB_UT").ToString()
                        msgArray(1) = "個数単位"
                        msgArray(2) = "商品マスタ"
                        msgArray(3) = "個数単位"
                        msgArray(4) = "EDIデータ"
                        msgArray(5) = String.Empty

                        ds = Me._ChkBlc.SetWarningM2("W159", LMH010BLC.FJF_WID_M003, ds, setDs, msgArray, ediName, ediValue, mustValue)
                        flgWarning = True 'ワーニングフラグをたてて処理続行
                    ElseIf choiceKb.Equals("01") = True Then
                        dtM.Rows(i)("NB_UT") = goodsDt.Rows(0)("NB_UT")
                    End If

                End If

            End If
            ''個数単位
            ''If String.IsNullOrEmpty(dtM.Rows(i)("NB_UT").ToString()) = True Then
            'dtM.Rows(i)("NB_UT") = goodsDt.Rows(0)("NB_UT")
            ''End If


            '包装個数(入数)
            '送られてくる入数と商品マスタの入数が異なる場合はエラー⇒実績の個数計算が異なってしまう為
            'If Convert.ToDecimal(dtM.Rows(i)("PKG_NB")) <> Convert.ToDecimal(goodsDt.Rows(0)("PKG_NB")) Then
            '    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E332", New String() {"包装個数", "商品マスタ", "包装個数"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            '    'このレコードのワーニングをクリア
            '    ds.Tables("WARNING_DTL").Rows.Clear()
            '    Return False
            'End If

            '包装個数(入数)
            dtM.Rows(i)("PKG_NB") = goodsDt.Rows(0)("PKG_NB")

            '③包装単位
            If String.IsNullOrEmpty(dtM.Rows(i)("PKG_UT").ToString()) = True Then
                dtM.Rows(i)("PKG_UT") = goodsDt.Rows(0)("PKG_UT")
            Else
                If (dtM.Rows(i)("PKG_UT").ToString()).Equals(goodsDt.Rows(0)("PKG_UT").ToString()) = False Then

                    choiceKb = Me._ChkBlc.GetWarningChoiceKbM(ds.Tables("LMH010_INKAEDI_M"), ds, LMH010BLC.FJF_WID_M004, i)

                    If String.IsNullOrEmpty(choiceKb) = True Then

                        ediName = "包装単位"
                        ediValue = dtM.Rows(i)("PKG_UT").ToString()
                        mustValue = goodsDt.Rows(0)("PKG_UT").ToString()
                        msgArray(1) = "包装単位"
                        msgArray(2) = "商品マスタ"
                        msgArray(3) = "包装単位"
                        msgArray(4) = "EDIデータ"
                        msgArray(5) = String.Empty

                        ds = Me._ChkBlc.SetWarningM2("W159", LMH010BLC.FJF_WID_M004, ds, setDs, msgArray, ediName, ediValue, mustValue)
                        flgWarning = True 'ワーニングフラグをたてて処理続行
                    ElseIf choiceKb.Equals("01") = True Then
                        dtM.Rows(i)("PKG_UT") = goodsDt.Rows(0)("PKG_UT")
                    End If

                End If

            End If

            ''包装単位
            ''If String.IsNullOrEmpty(dtM.Rows(i)("PKG_UT").ToString()) = True Then
            'dtM.Rows(i)("PKG_UT") = goodsDt.Rows(0)("PKG_UT")
            ''End If

            '入荷包装個数
            dtM.Rows(i)("INKA_PKG_NB") = Math.Floor(Convert.ToDecimal(dtM.Rows(i)("NB")) / Convert.ToDecimal(dtM.Rows(i)("PKG_NB")))

            '端数
            dtM.Rows(i)("HASU") = Convert.ToDecimal(dtM.Rows(i)("NB")) Mod Convert.ToDecimal(dtM.Rows(i)("PKG_NB"))

            '④標準入目
            If String.IsNullOrEmpty(dtM.Rows(i)("STD_IRIME").ToString()) = True Then
                dtM.Rows(i)("STD_IRIME") = goodsDt.Rows(0)("STD_IRIME_NB")
            Else
                If (dtM.Rows(i)("STD_IRIME").ToString()).Equals(goodsDt.Rows(0)("STD_IRIME_NB").ToString()) = False Then

                    choiceKb = Me._ChkBlc.GetWarningChoiceKbM(ds.Tables("LMH010_INKAEDI_M"), ds, LMH010BLC.FJF_WID_M005, i)

                    If String.IsNullOrEmpty(choiceKb) = True Then

                        ediName = "標準入目"
                        ediValue = dtM.Rows(i)("STD_IRIME").ToString()
                        mustValue = goodsDt.Rows(0)("STD_IRIME_NB").ToString()
                        msgArray(1) = "標準入目"
                        msgArray(2) = "商品マスタ"
                        msgArray(3) = "標準入目"
                        msgArray(4) = "EDIデータ"
                        msgArray(5) = String.Empty

                        ds = Me._ChkBlc.SetWarningM2("W159", LMH010BLC.FJF_WID_M005, ds, setDs, msgArray, ediName, ediValue, mustValue)
                        flgWarning = True 'ワーニングフラグをたてて処理続行
                    End If

                    dtM.Rows(i)("STD_IRIME") = goodsDt.Rows(0)("STD_IRIME_NB")

                End If

            End If

            ''標準入目
            ''If Convert.ToDecimal(dtM.Rows(i)("STD_IRIME")) = 0 Then
            'dtM.Rows(i)("STD_IRIME") = goodsDt.Rows(0)("STD_IRIME_NB")
            ''End If

            '⑤標準入目単位
            If String.IsNullOrEmpty(dtM.Rows(i)("STD_IRIME_UT").ToString()) = True Then
                dtM.Rows(i)("STD_IRIME_UT") = goodsDt.Rows(0)("STD_IRIME_UT")
            Else
                If (dtM.Rows(i)("STD_IRIME_UT").ToString()).Equals(goodsDt.Rows(0)("STD_IRIME_UT").ToString()) = False Then

                    choiceKb = Me._ChkBlc.GetWarningChoiceKbM(ds.Tables("LMH010_INKAEDI_M"), ds, LMH010BLC.FJF_WID_M006, i)

                    If String.IsNullOrEmpty(choiceKb) = True Then

                        ediName = "標準入目単位"
                        ediValue = dtM.Rows(i)("STD_IRIME_UT").ToString()
                        mustValue = goodsDt.Rows(0)("STD_IRIME_UT").ToString()
                        msgArray(1) = "標準入目単位"
                        msgArray(2) = "商品マスタ"
                        msgArray(3) = "標準入目単位"
                        msgArray(4) = "EDIデータ"
                        msgArray(5) = String.Empty

                        ds = Me._ChkBlc.SetWarningM2("W159", LMH010BLC.FJF_WID_M006, ds, setDs, msgArray, ediName, ediValue, mustValue)
                        flgWarning = True 'ワーニングフラグをたてて処理続行
                    End If

                    dtM.Rows(i)("STD_IRIME_UT") = goodsDt.Rows(0)("STD_IRIME_UT")

                End If

            End If

            ''標準入目単位
            ''If String.IsNullOrEmpty(dtM.Rows(i)("STD_IRIME_UT").ToString()) = True Then
            'dtM.Rows(i)("STD_IRIME_UT") = goodsDt.Rows(0)("STD_IRIME_UT")
            ''End If

            '⑥入目
            '入目が特定できていない場合は、強制的に商品マスタの値を設定
            If Convert.ToDecimal(dtM.Rows(i)("IRIME")) = 0 Then
                dtM.Rows(i)("IRIME") = goodsDt.Rows(0)("STD_IRIME_NB")
            End If
            '受信時にセットした入目と商品マスタの入目が異なる場合はエラー
            If Convert.ToDecimal(dtM.Rows(i)("IRIME")) <> Convert.ToDecimal(goodsDt.Rows(0)("STD_IRIME_NB")) Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E332", New String() {"入目", "商品マスタ", "標準入目"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                'このレコードのワーニングをクリア
                ds.Tables("WARNING_DTL").Rows.Clear()
                Return False
            End If

            '入目単位
            'If String.IsNullOrEmpty(dtM.Rows(i)("IRIME_UT").ToString()) = True Then
            dtM.Rows(i)("IRIME_UT") = goodsDt.Rows(0)("STD_IRIME_UT")
            'End If

            '個別重量
            'If Convert.ToDecimal(dtM.Rows(i)("BETU_WT")) = 0 Then
            dtM.Rows(i)("BETU_WT") = goodsDt.Rows(0)("STD_WT_KGS")
            'End If

            '容積
            'If Convert.ToDecimal(dtM.Rows(i)("CBM")) = 0 Then
            dtM.Rows(i)("CBM") = goodsDt.Rows(0)("STD_CBM")
            'End If

            '温度区分
            'If String.IsNullOrEmpty(dtM.Rows(i)("ONDO_KB").ToString()) = True Then
            dtM.Rows(i)("ONDO_KB") = goodsDt.Rows(0)("ONDO_KB")
            'End If

            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_1") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_1")
            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_2") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_2")
            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_3") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_3")
            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_4") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_4")
            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_5") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_5")

            dtM.Rows(i)("STD_WT_KGS") = goodsDt.Rows(0)("STD_WT_KGS")
            dtM.Rows(i)("STD_IRIME_NB") = goodsDt.Rows(0)("STD_IRIME_NB")
            dtM.Rows(i)("TARE_YN") = goodsDt.Rows(0)("TARE_YN")


            'FREE_C03(プラント),FREE_C04(保管場所)
            dtM.Rows(i)("TOU_NO") = Right(dtM.Rows(i).Item("FREE_C03").ToString(), 2)
            dtM.Rows(i)("SITU_NO") = Left(dtM.Rows(i).Item("FREE_C04").ToString(), 2)
            dtM.Rows(i)("ZONE_CD") = Right(dtM.Rows(i).Item("FREE_C04").ToString(), 2)
            '要望番号1003 2012.05.08 追加START
            '商品明細マスタより置場情報を取得(サブ区分="02")セット内容)
            'If String.IsNullOrEmpty(dtM.Rows(i)("NRS_GOODS_CD").ToString()) = False Then
            '    '商品明細マスタの取得
            '    setDs = (MyBase.CallDAC(Me._MstDac, "SelectDataGoodsMeisaiOkiba", setDs))
            '    If MyBase.GetResultCount <> 0 Then
            '        Dim setOkiba As String = setDs.Tables("LMH010_M_GOODS_DETAILS").Rows(0)("SET_NAIYO").ToString()
            '        '置場情報が5または6Byte取得できた時のみ置場情報をセット
            '        If setOkiba.Length = 6 OrElse setOkiba.Length = 5 Then
            '            dtM.Rows(i)("TOU_NO") = setOkiba.Substring(0, 2)
            '            dtM.Rows(i)("SITU_NO") = setOkiba.Substring(2, 2)
            '            If setOkiba.Length = 6 Then
            '                dtM.Rows(i)("ZONE_CD") = setOkiba.Substring(4, 2)
            '            ElseIf setOkiba.Length = 5 Then
            '                dtM.Rows(i)("ZONE_CD") = setOkiba.Substring(4, 1)
            '            End If
            '        End If
            '    End If
            'End If


            '要望番号1003 2012.05.08 追加END

        Next

        If flgWarning = True Then
            'ワーニングフラグが立っている場合False
            Return False
        End If

        Return True

    End Function

#End Region

#Region "入荷管理番号(大)取得"
    ''' <summary>
    ''' 入荷管理番号(大)取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetInkaNoL(ByVal ds As DataSet) As DataSet

        Dim inkaKanriNo As String = String.Empty
        Dim dr As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim nrsBrCd As String = dr("NRS_BR_CD").ToString
        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim max As Integer = dt.Rows.Count - 1
        Dim eventShubetsu As String = ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()
        Dim inkaKanriNoPrm As String = ds.Tables("LMH010INOUT").Rows(0)("INKA_CTL_NO_L").ToString()

        If eventShubetsu.Equals("3") Then
            '紐付け時は入荷管理番号(大)を引数のDataSetから取得
            inkaKanriNo = inkaKanriNoPrm
        Else
            '入荷登録時は入荷管理番号(大)をマスタから取得
            Dim num As New NumberMasterUtility
            inkaKanriNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.INKA_NO_L, Me, nrsBrCd)
        End If

        '入荷管理番号(大)をEDI入荷(大)に格納
        dr("INKA_CTL_NO_L") = inkaKanriNo

        '入荷管理番号(大)をEDI入荷(中)に格納
        For i As Integer = 0 To max

            dt.Rows(i)("INKA_CTL_NO_L") = inkaKanriNo

        Next

        Return ds

    End Function
#End Region

#Region "入荷管理番号(中)取得"
    ''' <summary>
    ''' 入荷管理番号(中)取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetInkaNoM(ByVal ds As DataSet) As DataSet

        Dim inkaKanriNo As String = String.Empty
        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim himodukeDt As DataTable = ds.Tables("LMH010_HIMODUKE")
        Dim max As Integer = dt.Rows.Count - 1
        Dim eventShubetsu As String = ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU").ToString()

        If eventShubetsu.Equals("3") Then
            '紐付け時は入荷管理番号(中)を引数のDataSetから取得
            For i As Integer = 0 To max
                inkaKanriNo = himodukeDt.Rows(i)("HIMODUKE_NO").ToString()
                dt.Rows(i)("INKA_CTL_NO_M") = inkaKanriNo
            Next

        Else
            For i As Integer = 0 To max

                inkaKanriNo = (i + 1).ToString("000")
                dt.Rows(i)("INKA_CTL_NO_M") = inkaKanriNo

            Next
        End If

        Return ds

    End Function
#End Region

#Region "データセット設定(入荷L)"
    ''' <summary>
    ''' データセット設定(入荷L)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetInkaL(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim inkaDr As DataRow = ds.Tables("LMH010_B_INKA_L").NewRow()

        Dim custIndex As Integer = Convert.ToInt32(ds.Tables("LMH010INOUT").Rows(0).Item("EDI_CUST_INDEX"))

        ''高取データで伝票タイプが格上げ("305")の場合は、入荷(小)・在庫を作成する
        'If custIndex = LMH010BLC.EdiCustIndex.FjfTaka00195_00 AndAlso ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("FREE_C16").ToString().Equals(KAKUAGE) = True Then
        '    ediDr("INKA_STATE_KB") = "40"   '検品済
        'End If

        '伝票タイプ(FREE_C16)が以下の場合は入荷(小),在庫を作成　※高取倉庫,千葉BC共に
        Select Case ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("FREE_C16").ToString()

            '格上げ,原価センター入庫
            Case LMH010BLC103.KAKUAGE, LMH010BLC103.CENTER_INKA

                ediDr("INKA_STATE_KB") = "40"   '検品済

            Case LMH010BLC103.TENSO_ORDER

                '出荷条件(FREE_C18)が"Z3"の場合は入荷(小),在庫を作成
                If ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("FREE_C18").ToString().Equals(LMH010BLC103.OUTKA_TERMS) = True Then

                    ediDr("INKA_STATE_KB") = "40"   '検品済

                End If

            Case Else

        End Select

        inkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
        inkaDr("INKA_NO_L") = ediDr("INKA_CTL_NO_L")
        inkaDr("INKA_TP") = ediDr("INKA_TP")
        inkaDr("INKA_KB") = ediDr("INKA_KB")
        inkaDr("INKA_STATE_KB") = ediDr("INKA_STATE_KB")
        inkaDr("INKA_DATE") = ediDr("INKA_DATE")
        inkaDr("NRS_WH_CD") = ediDr("NRS_WH_CD")
        inkaDr("CUST_CD_L") = ediDr("CUST_CD_L")
        inkaDr("CUST_CD_M") = ediDr("CUST_CD_M")
        inkaDr("INKA_PLAN_QT") = ediDr("INKA_PLAN_QT")
        inkaDr("INKA_PLAN_QT_UT") = ediDr("INKA_PLAN_QT_UT")

        Dim ediM As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim max As Integer = ediM.Rows.Count - 1
        Dim ediMNb As Long = 0

        For i As Integer = 0 To max
            ediMNb = ediMNb + Convert.ToInt64(ediM.Rows(i)("NB"))
        Next

        inkaDr("INKA_TTL_NB") = ediMNb
        inkaDr("BUYER_ORD_NO_L") = ediDr("BUYER_ORD_NO")
        inkaDr("OUTKA_FROM_ORD_NO_L") = ediDr("OUTKA_FROM_ORD_NO")
        inkaDr("TOUKI_HOKAN_YN") = FormatZero(ediDr("TOUKI_HOKAN_YN").ToString(), 2)
        inkaDr("HOKAN_STR_DATE") = ediDr("HOKAN_STR_DATE")
        inkaDr("HOKAN_YN") = FormatZero(ediDr("HOKAN_YN").ToString(), 2)
        inkaDr("HOKAN_FREE_KIKAN") = ediDr("HOKAN_FREE_KIKAN")
        inkaDr("NIYAKU_YN") = FormatZero(ediDr("NIYAKU_YN").ToString(), 2)
        inkaDr("TAX_KB") = ediDr("TAX_KB")
        inkaDr("REMARK") = ediDr("REMARK")
        inkaDr("REMARK_OUT") = ediDr("NYUBAN_L")
        inkaDr("UNCHIN_TP") = ediDr("UNCHIN_TP")
        inkaDr("UNCHIN_KB") = ediDr("UNCHIN_KB")
        inkaDr("SYS_DEL_FLG") = "0"

        'データセットに設定
        ds.Tables("LMH010_B_INKA_L").Rows.Add(inkaDr)

        Return ds

    End Function
#End Region

#Region "データセット設定(入荷M)"
    ''' <summary>
    ''' データセット設定(入荷M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetInkaM(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow
        Dim inkaDr As DataRow
        Dim max As Integer = ds.Tables("LMH010_INKAEDI_M").Rows.Count - 1
        Dim ediDrL As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)

        For i As Integer = 0 To max

            inkaDr = ds.Tables("LMH010_B_INKA_M").NewRow()
            ediDr = ds.Tables("LMH010_INKAEDI_M").Rows(i)

            inkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            inkaDr("INKA_NO_L") = ediDrL("INKA_CTL_NO_L")
            inkaDr("INKA_NO_M") = ediDr("INKA_CTL_NO_M")
            inkaDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            inkaDr("OUTKA_FROM_ORD_NO_M") = ediDr("OUTKA_FROM_ORD_NO")
            inkaDr("BUYER_ORD_NO_M") = ediDr("BUYER_ORD_NO")
            inkaDr("REMARK") = ediDr("REMARK")
            inkaDr("SYS_DEL_FLG") = "0"

            'データセットに設定
            ds.Tables("LMH010_B_INKA_M").Rows.Add(inkaDr)
        Next

        Return ds

    End Function
#End Region

#Region "データセット設定(入荷S)"
    ''' <summary>
    ''' データセット設定(入荷S)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetInkaS(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow
        Dim inkaDr As DataRow
        Dim max As Integer = ds.Tables("LMH010_INKAEDI_M").Rows.Count - 1

        For i As Integer = 0 To max

            inkaDr = ds.Tables("LMH010_B_INKA_S").NewRow()
            ediDr = ds.Tables("LMH010_INKAEDI_M").Rows(i)

            inkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            inkaDr("INKA_NO_L") = ediDr("INKA_CTL_NO_L")
            inkaDr("INKA_NO_M") = ediDr("INKA_CTL_NO_M")
            inkaDr("INKA_NO_S") = "001"
            inkaDr("TOU_NO") = ediDr("TOU_NO")
            inkaDr("SITU_NO") = ediDr("SITU_NO")
            inkaDr("ZONE_CD") = ediDr("ZONE_CD")
            inkaDr("LOT_NO") = ediDr("LOT_NO")
            inkaDr("KONSU") = ediDr("INKA_PKG_NB")
            inkaDr("HASU") = ediDr("HASU")
            inkaDr("IRIME") = ediDr("IRIME")
            inkaDr("BETU_WT") = ediDr("BETU_WT")
            inkaDr("SERIAL_NO") = ediDr("SERIAL_NO")
            inkaDr("SPD_KB") = "01"
            inkaDr("OFB_KB") = "01"
            inkaDr("ALLOC_PRIORITY") = "10"
            inkaDr("SYS_DEL_FLG") = ediDr("SYS_DEL_FLG")

            'データセットに設定
            ds.Tables("LMH010_B_INKA_S").Rows.Add(inkaDr)
        Next

        Return ds

    End Function
#End Region


#Region "データセット設定(在庫TRS)"
    ''' <summary>
    ''' データセット設定(在庫TRS)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetZaiTrs(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow
        Dim zaiDr As DataRow
        Dim max As Integer = ds.Tables("LMH010_INKAEDI_M").Rows.Count - 1
        Dim drL As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)

        '在庫情報作成
        For i As Integer = 0 To max

            zaiDr = ds.Tables("LMH010_ZAI_TRS").NewRow()
            ediDr = ds.Tables("LMH010_INKAEDI_M").Rows(i)

            '在庫番号を採番
            Dim num As New NumberMasterUtility

            zaiDr("NRS_BR_CD") = ediDr("NRS_BR_CD").ToString()
            zaiDr("ZAI_REC_NO") = num.GetAutoCode(NumberMasterUtility.NumberKbn.ZAI_REC_NO, Me, ediDr("NRS_BR_CD").ToString())
            zaiDr("WH_CD") = drL("NRS_WH_CD").ToString
            zaiDr("TOU_NO") = ediDr("TOU_NO").ToString()
            zaiDr("SITU_NO") = ediDr("SITU_NO").ToString()
            zaiDr("ZONE_CD") = ediDr("ZONE_CD").ToString()
            zaiDr("LOCA") = String.Empty
            zaiDr("LOT_NO") = ediDr("LOT_NO").ToString()
            zaiDr("CUST_CD_L") = drL("CUST_CD_L").ToString()
            zaiDr("CUST_CD_M") = drL("CUST_CD_M").ToString()
            zaiDr("GOODS_KANRI_NO") = String.Empty
            zaiDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD").ToString()
            zaiDr("INKA_NO_L") = ediDr("INKA_CTL_NO_L").ToString()
            zaiDr("INKA_NO_M") = ediDr("INKA_CTL_NO_M").ToString()
            zaiDr("INKA_NO_S") = "001"
            zaiDr("ALLOC_PRIORITY") = "10"
            zaiDr("RSV_NO") = String.Empty
            zaiDr("SERIAL_NO") = ediDr("SERIAL_NO").ToString()
            zaiDr("HOKAN_YN") = FormatZero(drL("HOKAN_YN").ToString(), 2)
            zaiDr("TAX_KB") = drL("TAX_KB").ToString()
            zaiDr("GOODS_COND_KB_1") = String.Empty
            zaiDr("GOODS_COND_KB_2") = String.Empty
            zaiDr("GOODS_COND_KB_3") = String.Empty
            zaiDr("OFB_KB") = "01"
            zaiDr("SPD_KB") = "01"
            zaiDr("REMARK_OUT") = String.Empty
            zaiDr("PORA_ZAI_NB") = ediDr("NB").ToString()
            zaiDr("ALCTD_NB") = "0"
            zaiDr("ALLOC_CAN_NB") = ediDr("NB").ToString()
            zaiDr("IRIME") = ediDr("IRIME").ToString()
            Dim irime As Double = 0
            Dim inkaNb As Integer = 0
            Double.TryParse(ediDr("IRIME").ToString(), irime)
            Integer.TryParse(ediDr("NB").ToString(), inkaNb)
            zaiDr("PORA_ZAI_QT") = CStr(inkaNb * irime)
            zaiDr("ALCTD_QT") = "0"
            zaiDr("ALLOC_CAN_QT") = CStr(inkaNb * irime)
            zaiDr("INKO_DATE") = MyBase.GetSystemDate
            zaiDr("INKO_PLAN_DATE") = MyBase.GetSystemDate
            zaiDr("ZERO_FLAG") = String.Empty
            zaiDr("LT_DATE") = String.Empty
            zaiDr("GOODS_CRT_DATE") = String.Empty
            zaiDr("DEST_CD_P") = String.Empty
            zaiDr("REMARK") = String.Empty
            zaiDr("SMPL_FLAG") = "00"
            zaiDr("SYS_DEL_FLG") = ediDr("SYS_DEL_FLG")

            'データセットに設定
            ds.Tables("LMH010_ZAI_TRS").Rows.Add(zaiDr)

        Next

        Return ds

    End Function
#End Region

#Region "データセット設定(入荷S)(在庫TRS)"
    ''' <summary>
    ''' データセット設定(入荷S)(在庫TRS)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetInkaSZaiRec(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow
        Dim inkaDr As DataRow
        Dim zaiDr As DataRow
        Dim drL As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim max As Integer = ds.Tables("LMH010_INKAEDI_M").Rows.Count - 1

        For i As Integer = 0 To max

            '2014.09.17 追加START
            _ZaiRecNo = String.Empty
            '2014.09.17 追加END

            '入荷S
            inkaDr = ds.Tables("LMH010_B_INKA_S").NewRow()
            ediDr = ds.Tables("LMH010_INKAEDI_M").Rows(i)

            '在庫番号を採番
            Dim num As New NumberMasterUtility

            inkaDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            inkaDr("INKA_NO_L") = ediDr("INKA_CTL_NO_L")
            inkaDr("INKA_NO_M") = ediDr("INKA_CTL_NO_M")
            inkaDr("INKA_NO_S") = "001"
            '2014.09.17 追加START
            inkaDr("ZAI_REC_NO") = num.GetAutoCode(NumberMasterUtility.NumberKbn.ZAI_REC_NO, Me, ediDr("NRS_BR_CD").ToString())
            _ZaiRecNo = inkaDr("ZAI_REC_NO").ToString()
            '2014.09.17 追加END
            inkaDr("TOU_NO") = ediDr("TOU_NO")
            inkaDr("SITU_NO") = ediDr("SITU_NO")
            inkaDr("ZONE_CD") = ediDr("ZONE_CD")
            inkaDr("LOT_NO") = ediDr("LOT_NO")
            inkaDr("KONSU") = ediDr("INKA_PKG_NB")
            inkaDr("HASU") = ediDr("HASU")
            inkaDr("IRIME") = ediDr("IRIME")
            inkaDr("BETU_WT") = ediDr("BETU_WT")
            inkaDr("SERIAL_NO") = ediDr("SERIAL_NO")
            'add 20150123 ma-takahashi ↓↓↓
            'inkaDr("LT_DATE") = ediDr("FREE_C06").ToString()
            'add 20150123 ma-takahashi ↑↑↑
            inkaDr("SPD_KB") = "01"
            inkaDr("OFB_KB") = "01"
            inkaDr("ALLOC_PRIORITY") = "10"
            inkaDr("SYS_DEL_FLG") = ediDr("SYS_DEL_FLG")

            'データセットに設定
            ds.Tables("LMH010_B_INKA_S").Rows.Add(inkaDr)

            '在庫TRS
            zaiDr = ds.Tables("LMH010_ZAI_TRS").NewRow()
            zaiDr("NRS_BR_CD") = ediDr("NRS_BR_CD").ToString()
            '2014.09.17 修正START
            zaiDr("ZAI_REC_NO") = _ZaiRecNo
            '2014.09.17 修正END
            zaiDr("WH_CD") = drL("NRS_WH_CD").ToString
            zaiDr("TOU_NO") = ediDr("TOU_NO").ToString()
            zaiDr("SITU_NO") = ediDr("SITU_NO").ToString()
            zaiDr("ZONE_CD") = ediDr("ZONE_CD").ToString()
            zaiDr("LOCA") = String.Empty
            zaiDr("LOT_NO") = ediDr("LOT_NO").ToString()
            zaiDr("CUST_CD_L") = drL("CUST_CD_L").ToString()
            zaiDr("CUST_CD_M") = drL("CUST_CD_M").ToString()
            zaiDr("GOODS_KANRI_NO") = String.Empty
            zaiDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD").ToString()
            zaiDr("INKA_NO_L") = ediDr("INKA_CTL_NO_L").ToString()
            zaiDr("INKA_NO_M") = ediDr("INKA_CTL_NO_M").ToString()
            zaiDr("INKA_NO_S") = "001"
            zaiDr("ALLOC_PRIORITY") = "10"
            zaiDr("RSV_NO") = String.Empty
            zaiDr("SERIAL_NO") = ediDr("SERIAL_NO").ToString()
            zaiDr("HOKAN_YN") = FormatZero(drL("HOKAN_YN").ToString(), 2)
            zaiDr("TAX_KB") = drL("TAX_KB").ToString()
            zaiDr("GOODS_COND_KB_1") = String.Empty
            zaiDr("GOODS_COND_KB_2") = String.Empty
            zaiDr("GOODS_COND_KB_3") = String.Empty
            zaiDr("OFB_KB") = "01"
            zaiDr("SPD_KB") = "01"
            zaiDr("REMARK_OUT") = String.Empty
            zaiDr("PORA_ZAI_NB") = ediDr("NB").ToString()
            zaiDr("ALCTD_NB") = "0"
            zaiDr("ALLOC_CAN_NB") = ediDr("NB").ToString()
            zaiDr("IRIME") = ediDr("IRIME").ToString()
            Dim irime As Double = 0
            Dim inkaNb As Integer = 0
            Double.TryParse(ediDr("IRIME").ToString(), irime)
            Integer.TryParse(ediDr("NB").ToString(), inkaNb)
            zaiDr("PORA_ZAI_QT") = CStr(inkaNb * irime)
            zaiDr("ALCTD_QT") = "0"
            zaiDr("ALLOC_CAN_QT") = CStr(inkaNb * irime)
            zaiDr("INKO_DATE") = MyBase.GetSystemDate
            zaiDr("INKO_PLAN_DATE") = MyBase.GetSystemDate
            zaiDr("ZERO_FLAG") = String.Empty
            'add 20150123 ma-takahashi ↓↓↓
            'zaiDr("LT_DATE") = ediDr("FREE_C06").ToString()
            'add 20150123 ma-takahashi ↑↑↑
            zaiDr("GOODS_CRT_DATE") = String.Empty
            zaiDr("DEST_CD_P") = String.Empty
            zaiDr("REMARK") = String.Empty
            zaiDr("SMPL_FLAG") = "00"
            zaiDr("SYS_DEL_FLG") = ediDr("SYS_DEL_FLG")

            'データセットに設定
            ds.Tables("LMH010_ZAI_TRS").Rows.Add(zaiDr)

        Next

        Return ds

    End Function
#End Region


    '富士フイルム追加箇所 terakawa 2012.08.02 Start
#Region "データセット設定(受信ヘッダ)"
    ''' <summary>
    ''' データセット設定(受信ヘッダ)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetRcvHed(ByVal ds As DataSet, Optional canHokokuF As Boolean = False) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim inDr As DataRow = ds.Tables("LMH010INOUT").Rows(0)
        Dim rcvDr As DataRow = ds.Tables("LMH010_RCV_HED").NewRow()

        rcvDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
        rcvDr("INKA_CTL_NO_L") = ediDr("INKA_CTL_NO_L")
        rcvDr("EDI_CTL_NO") = ediDr("EDI_CTL_NO")
        rcvDr("CUST_CD_L") = ediDr("CUST_CD_L")
        rcvDr("CUST_CD_M") = ediDr("CUST_CD_M")
        rcvDr("SYS_UPD_DATE") = inDr("RCV_UPD_DATE")
        rcvDr("SYS_UPD_TIME") = inDr("RCV_UPD_TIME")
        rcvDr("SYS_DEL_FLG") = "0"

        'キャンセルデータの場合のみ
        If canHokokuF = True Then
            'EDI入荷(大)の実績処理フラグを"1"にする
            ediDr("JISSEKI_FLAG") = "1"
        End If

        'データセットに設定
        ds.Tables("LMH010_RCV_HED").Rows.Add(rcvDr)

        Return ds

    End Function
#End Region
    '富士フイルム追加箇所 terakawa 2012.08.02 End

#Region "データセット設定(受信明細)"
    ''' <summary>
    ''' データセット設定(受信明細)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetRcvDtl(ByVal ds As DataSet, Optional canHokokuF As Boolean = False) As DataSet

        Dim ediDr As DataRow
        Dim rcvDr As DataRow

        Dim max As Integer = ds.Tables("LMH010_INKAEDI_M").Rows.Count - 1

        For i As Integer = 0 To max

            ediDr = ds.Tables("LMH010_INKAEDI_M").Rows(i)
            rcvDr = ds.Tables("LMH010_RCV_DTL").NewRow()

            rcvDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            rcvDr("EDI_CTL_NO") = ediDr("EDI_CTL_NO")
            rcvDr("EDI_CTL_NO_CHU") = ediDr("EDI_CTL_NO_CHU")
            rcvDr("INKA_CTL_NO_L") = ediDr("INKA_CTL_NO_L")
            rcvDr("INKA_CTL_NO_M") = ediDr("INKA_CTL_NO_M")
            rcvDr("SYS_DEL_FLG") = "0"

            'キャンセルデータの場合のみ
            If canHokokuF = True Then
                'EDI入荷(中)の実績処理フラグを"1"にする
                ediDr("JISSEKI_FLAG") = "1"
            End If

            'データセットに設定
            ds.Tables("LMH010_RCV_DTL").Rows.Add(rcvDr)
        Next

        Return ds

    End Function
#End Region

#Region "データセット設定(作業)"
    ''' <summary>
    ''' データセット設定(作業)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetSagyo(ByVal ds As DataSet) As DataSet

        Dim ediDrM As DataRow
        Dim sagyoDr As DataRow
        Dim ediDrL As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim max As Integer = ds.Tables("LMH010_INKAEDI_M").Rows.Count - 1
        Dim nrsBrCd As String = ds.Tables("LMH010INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim sagyoCD As String = String.Empty
        Dim num As New NumberMasterUtility

        For i As Integer = 0 To max

            ediDrM = ds.Tables("LMH010_INKAEDI_M").Rows(i)

            For j As Integer = 1 To 5

                sagyoCD = ediDrM(String.Concat("INKA_KAKO_SAGYO_KB_", j)).ToString

                If String.IsNullOrEmpty(sagyoCD) = False Then

                    sagyoDr = ds.Tables("LMH010_E_SAGYO").NewRow()

                    sagyoDr("SAGYO_COMP") = "00"
                    sagyoDr("SKYU_CHK") = "00"
                    sagyoDr("SAGYO_REC_NO") = num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, nrsBrCd)
                    sagyoDr("INOUTKA_NO_LM") = String.Concat(ediDrM("INKA_CTL_NO_L"), ediDrM("INKA_CTL_NO_M"))
                    sagyoDr("NRS_BR_CD") = ediDrL("NRS_BR_CD")
                    sagyoDr("WH_CD") = ediDrL("NRS_WH_CD")
                    sagyoDr("IOZS_KB") = "11"
                    sagyoDr("SAGYO_CD") = sagyoCD
                    sagyoDr("CUST_CD_L") = ediDrL("CUST_CD_L")
                    sagyoDr("CUST_CD_M") = ediDrL("CUST_CD_M")
                    sagyoDr("DEST_CD") = String.Empty
                    sagyoDr("DEST_NM") = String.Empty
                    sagyoDr("GOODS_CD_NRS") = ediDrM("NRS_GOODS_CD")
                    sagyoDr("GOODS_NM_NRS") = ediDrM("GOODS_NM")
                    sagyoDr("LOT_NO") = ediDrM("LOT_NO")
                    sagyoDr("REMARK_SKYU") = ediDrM("REMARK")
                    sagyoDr("DEST_SAGYO_FLG") = "00"
                    sagyoDr("SYS_DEL_FLG") = "0"

                    'データセットに設定
                    ds.Tables("LMH010_E_SAGYO").Rows.Add(sagyoDr)
                End If
            Next
        Next

        Return ds

    End Function
#End Region

#Region "データセット設定(運送L)"
    ''' <summary>
    ''' データセット設定(運送)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetUnsoL(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim unsoDr As DataRow = ds.Tables("LMH010_UNSO_L").NewRow()
        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim nrsBrCd As String = ds.Tables("LMH010INOUT").Rows(0).Item("NRS_BR_CD").ToString()
        Dim num As New NumberMasterUtility

        unsoDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
        '新規採番
        unsoDr("UNSO_NO_L") = num.GetAutoCode(NumberMasterUtility.NumberKbn.UNSO_NO_L, Me, nrsBrCd)
        unsoDr("YUSO_BR_CD") = ediDr("NRS_BR_CD")
        unsoDr("INOUTKA_NO_L") = ediDr("INKA_CTL_NO_L")
        unsoDr("UNSO_CD") = ediDr("UNSO_CD")
        unsoDr("UNSO_BR_CD") = ediDr("UNSO_BR_CD")
        unsoDr("BIN_KB") = "01"
        unsoDr("JIYU_KB") = "02"
        unsoDr("OUTKA_PLAN_DATE") = ediDr("INKA_DATE")
        unsoDr("ARR_PLAN_DATE") = ediDr("INKA_DATE")

        unsoDr("NRS_WH_CD") = ediDr("NRS_WH_CD")
        unsoDr("CUST_CD_L") = ediDr("CUST_CD_L")
        unsoDr("CUST_CD_M") = ediDr("CUST_CD_M")
        unsoDr("CUST_REF_NO") = ediDr("OUTKA_FROM_ORD_NO")
        unsoDr("ORIG_CD") = ediDr("OUTKA_MOTO")
        'unsoDr("DEST_CD") = ""                                  'マスタ値

        '運送梱包個数の計算
        Dim unsoPkgNb As Long = 0

        For i As Integer = 0 To dt.Rows.Count - 1

            unsoPkgNb = unsoPkgNb + Convert.ToInt64(dt.Rows(i).Item("INKA_PKG_NB"))
            If Convert.ToInt64(dt.Rows(i).Item("HASU")) > 0 Then
                unsoPkgNb = unsoPkgNb + 1
            End If

        Next

        unsoDr("UNSO_PKG_NB") = unsoPkgNb                              '集計値
        unsoDr("UNSO_WT") = ""
        unsoDr("UNSO_ONDO_KB") = ediDr("UNSO_ONDO_KB")
        unsoDr("TARIFF_BUNRUI_KB") = ediDr("UNCHIN_KB")
        unsoDr("VCLE_KB") = ediDr("SYARYO_KB")
        unsoDr("MOTO_DATA_KB") = "10"
        unsoDr("TAX_KB") = "01"
        unsoDr("REMARK") = ediDr("REMARK")
        unsoDr("SEIQ_TARIFF_CD") = ediDr("YOKO_TARIFF_CD")
        unsoDr("AD_3") = ""
        unsoDr("UNSO_TEHAI_KB") = ediDr("UNCHIN_TP")
        unsoDr("BUY_CHU_NO") = ediDr("BUYER_ORD_NO")
        unsoDr("SYS_DEL_FLG") = "0"
        '要望番号:1211(EDI出荷：運送登録時の中継配送フラグ) 2012/06/28 本明 Start
        unsoDr("TYUKEI_HAISO_FLG") = "00"   '中継配送フラグ"00:無し"を設定
        '要望番号:1211(EDI出荷：運送登録時の中継配送フラグ) 2012/06/28 本明 End

        'START UMANO 要望番号1302 支払運賃に伴う修正。
        If String.IsNullOrEmpty(ediDr("UNSO_CD").ToString()) = False AndAlso _
           String.IsNullOrEmpty(ediDr("UNSO_BR_CD").ToString()) = False Then

            '運送会社マスタの取得(支払請求タリフコード,支払割増タリフコード)
            ds = MyBase.CallDAC(Me._MstDac, "SelectListDataShiharaiTariff", ds)
            Dim unsocoMDr As DataRow = ds.Tables("LMH010_SHIHARAI_TARIFF").Rows(0)

            If MyBase.GetResultCount > 0 Then
                unsoDr("SHIHARAI_TARIFF_CD") = unsocoMDr("UNCHIN_TARIFF_CD")
                unsoDr("SHIHARAI_ETARIFF_CD") = unsocoMDr("EXTC_TARIFF_CD")
            End If

        End If
        'END UMANO 要望番号1302 支払運賃に伴う修正。

        'データセットに設定
        ds.Tables("LMH010_UNSO_L").Rows.Add(unsoDr)

        Return ds

    End Function
#End Region

#Region "データセット設定(運送M)"
    ''' <summary>
    ''' データセット設定(運送M)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetUnsoM(ByVal ds As DataSet) As DataSet

        Dim ediDr As DataRow
        Dim unsoMDr As DataRow
        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim unsoLDr As DataRow = ds.Tables("LMH010_UNSO_L").Rows(0)
        Dim unsoTtlQt As Decimal = 0

        Dim max As Integer = dt.Rows.Count - 1

        Dim stdWtKgs As Decimal = 0
        Dim irime As Decimal = 0
        Dim stdIrimeNb As Decimal = 0
        Dim nisugata As Decimal = 0
        Dim inkaTtlNb As Decimal = 0

        For i As Integer = 0 To max

            ediDr = ds.Tables("LMH010_INKAEDI_M").Rows(i)
            unsoMDr = ds.Tables("LMH010_UNSO_M").NewRow()

            unsoMDr("NRS_BR_CD") = ediDr("NRS_BR_CD")
            unsoMDr("UNSO_NO_L") = unsoLDr("UNSO_NO_L")
            unsoMDr("UNSO_NO_M") = ediDr("INKA_CTL_NO_M")
            unsoMDr("GOODS_CD_NRS") = ediDr("NRS_GOODS_CD")
            unsoMDr("GOODS_NM") = ediDr("GOODS_NM")
            unsoMDr("UNSO_TTL_NB") = ediDr("INKA_PKG_NB")
            unsoMDr("NB_UT") = ediDr("NB_UT")
            unsoTtlQt = Convert.ToDecimal(ediDr("IRIME")) * Convert.ToInt64(ediDr("NB"))
            unsoMDr("UNSO_TTL_QT") = unsoTtlQt
            unsoMDr("QT_UT") = ediDr("STD_IRIME_UT")
            unsoMDr("HASU") = ediDr("HASU")
            unsoMDr("IRIME") = ediDr("IRIME")
            unsoMDr("IRIME_UT") = ediDr("IRIME_UT")

            stdWtKgs = Convert.ToDecimal(ediDr("STD_WT_KGS"))
            irime = Convert.ToDecimal(ediDr("IRIME"))
            stdIrimeNb = Convert.ToDecimal(ediDr("STD_IRIME_NB"))

            If String.IsNullOrEmpty(ediDr("NISUGATA").ToString()) = False Then
                nisugata = Convert.ToDecimal(ediDr("NISUGATA"))
            End If

            If ediDr("TARE_YN").Equals("01") = False Then

                unsoMDr("BETU_WT") = stdWtKgs * irime / stdIrimeNb

            Else

                unsoMDr("BETU_WT") = stdWtKgs * irime / stdIrimeNb + nisugata

            End If

            unsoMDr("PKG_NB") = ediDr("PKG_NB")
            unsoMDr("LOT_NO") = ediDr("LOT_NO")
            unsoMDr("REMARK") = ediDr("REMARK")
            unsoMDr("SYS_DEL_FLG") = "0"

            'データセットに設定
            ds.Tables("LMH010_UNSO_M").Rows.Add(unsoMDr)
        Next

        Return ds

    End Function

#End Region

#Region "データセット設定(運送L：運送重量)"
    ''' <summary>
    ''' データセット設定(運送L：運送重量)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetdatasetUnsoJyuryo(ByVal ds As DataSet) As DataSet

        Dim unsoLDr As DataRow = ds.Tables("LMH010_UNSO_L").Rows(0)
        Dim unsoMDr As DataRow
        Dim ediMDr As DataRow
        Dim unsoJyuryo As Decimal = 0
        Dim max As Integer = ds.Tables("LMH010_UNSO_M").Rows.Count - 1

        For i As Integer = 0 To max

            unsoMDr = ds.Tables("LMH010_UNSO_M").Rows(i)
            ediMDr = ds.Tables("LMH010_INKAEDI_M").Rows(i)

            unsoJyuryo = Convert.ToDecimal(unsoMDr("BETU_WT")) * Convert.ToDecimal(ediMDr("NB")) + unsoJyuryo

        Next

        unsoLDr("UNSO_WT") = Math.Ceiling(unsoJyuryo)
        unsoLDr("NB_UT") = ds.Tables("LMH010_INKAEDI_M").Rows(0)("NB_UT")

        Return ds

    End Function

#End Region

#Region "データセット設定(タブレット項目の初期値設定)"
    ''' <summary>
    ''' データセット設定(タブレット項目の初期値設定)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetInkaLTabletData(ByVal ds As DataSet) As DataSet

        Dim drJudge As DataRow = ds.Tables("LMH010_JUDGE").Rows(0)
        Dim drInkaL As DataRow = ds.Tables("LMH010_B_INKA_L").Rows(0)
        Dim tabletYn As String = LMH010BLC.WH_TAB_YN_NO

        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_B007
        drJudge("KBN_CD") = drInkaL("NRS_BR_CD")
        drJudge("VALUE1") = "1.000"

        Call MyBase.CallDAC(Me._MstDac, "SelectDataTabletYN", ds)

        If MyBase.GetResultCount > 0 Then
            tabletYn = LMH010BLC.WH_TAB_YN_YES
        End If

        For Each dr As DataRow In ds.Tables("LMH010_B_INKA_L").Rows
            dr.Item("WH_TAB_STATUS") = LMH010BLC.WH_TAB_STATUS_UNPROCESSED
            dr.Item("WH_TAB_YN") = tabletYn
            dr.Item("WH_TAB_IMP_YN") = LMH010BLC.WH_TAB_IMP_YN_NO
        Next

        Return ds

    End Function
#End Region

#Region "風袋重量の取得"
    ''' <summary>
    ''' 風袋重量の取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetPkgUtZkbn(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim dt As DataTable = ds.Tables("LMH010_INKAEDI_M")
        Dim max As Integer = dt.Rows.Count - 1
        Dim drJudge As DataRow

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDt As DataTable = setDs.Tables("LMH010_INKAEDI_M")

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            setDt.ImportRow(dt.Rows(i))

            '荷姿(区分マスタ)
            drJudge = setDs.Tables("LMH010_JUDGE").NewRow()
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N001
            drJudge("KBN_CD") = dt.Rows(i)("PKG_UT")
            setDs.Tables("LMH010_JUDGE").Rows.Add(drJudge)

            '商品マスタ
            If dt.Rows(i)("TARE_YN").Equals("01") Then

                setDs = MyBase.CallDAC(Me._MstDac, "SelectDataPkgUtZkbn", setDs)

                If String.IsNullOrEmpty(setDt.Rows(0)("NISUGATA").ToString()) = False Then

                    dt.Rows(i)("NISUGATA") = setDt.Rows(0)("NISUGATA")
                Else
                    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"包装単位", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                    Return False
                End If
            End If

        Next

        Return True
    End Function

#End Region

#End Region

    '↓FFEM特殊処理↓
    '2014.06.09 使用START
#Region "実績作成処理(富士フイルム)"
    ''' <summary>
    ''' 実績作成処理(富士フイルム)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSakusei(ByVal ds As DataSet) As Boolean

        Dim rowNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        Dim caniFlg As Boolean = False

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtL As DataTable = setDs.Tables("LMH010_INKAEDI_L")
        Dim setDtM As DataTable = setDs.Tables("LMH010_INKAEDI_M")

        ''保留データの場合
        'If ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("DEL_KB").ToString().Equals("3") = True Then

        '    For i As Integer = 0 To ds.Tables("LMH010_INKAEDI_M").Rows.Count - 1
        '        '作業工程進捗ファイル(キャニスター缶かどうかの判断)データ取得
        '        '値のクリア
        '        setDs.Clear()
        '        setDtL.ImportRow(ds.Tables("LMH010_INKAEDI_L").Rows(0))
        '        setDtM.ImportRow(ds.Tables("LMH010_INKAEDI_M").Rows(i))

        '        'setDs = MyBase.CallDAC(Me._Dac, "SelectCanister", setDs)
        '        'If MyBase.GetResultCount > 1 Then
        '        '    caniFlg = True
        '        'Else
        '        '    '(富士フイルム)送信データ作成用情報取得
        '        '    setDs = MyBase.CallDAC(Me._Dac, "SelectSendFujiFilm", setDs)

        '        '    If MyBase.GetResultCount = 0 Then
        '        '        MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
        '        '        Return False
        '        '        Exit Function
        '        '    End If

        '        'End If

        '        '(富士フイルム)送信データ作成用情報取得
        '        setDs = MyBase.CallDAC(Me._Dac, "SelectSendFujiFilm", setDs)
        '        '(富士フイルム)送信データの更新
        '        setDs = MyBase.CallDAC(Me._Dac, "InsertSendFujiFilm", setDs)
        '        'If MyBase.GetResultCount = 0 Then
        '        '    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
        '        '    Return False
        '        '    Exit Function
        '        'End If

        '    Next
        'Else

        '(富士フイルム)送信データ作成用情報取得
        ds = MyBase.CallDAC(Me._Dac, "SelectSendFujiFilm", ds)


        If ds.Tables("H_SENDINEDI_FJF").Rows.Count > 0 Then

            'MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E492", New String() {"EDI入荷データ", "元黒または元黒の報告データ", ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("OUTKA_FROM_ORD_NO").ToString()}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'Return False
            'Exit Function
            '(富士フイルム)送信データの更新
            ds = MyBase.CallDAC(Me._Dac, "InsertSendFujiFilm", ds)

        End If


        'End If

        'If caniFlg = True Then
        '    Return True
        'End If

        '処理フラグを強制的に"2"(実績作成)に変換
        ds.Tables("LMH010_JUDGE").Rows(0)("EVENT_SHUBETSU") = "2"
        ''入荷登録時。。
        'ds.Tables("LMH010_INKAEDI_L").Rows(0).Item("JISSEKI_FLAG") = "2"


        'EDI入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        'EDI入荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)

        '受信(ヘッダー)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)

        '受信(明細)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        Return True

    End Function

#End Region

#Region "元黒データ削除処理"

    ''' <summary>
    ''' 初期検索の値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInitData(ByVal ds As DataSet) As DataSet

        '入荷(大)のデータ取得
        ds = Me.DacAccess(ds, "SelectInkaLData")

        '入荷(中)のデータ取得
        ds = Me.DacAccess(ds, "SelectInkaMData")

        '入荷(小)のデータ取得
        ds = Me.DacAccess(ds, "SelectInkaSData")

        '運送(大)のデータ取得
        ds = Me.DacAccess(ds, "SelectUnsoLData")

        '作業のデータ取得
        ds = Me.DacAccess(ds, "SelectSagyoData")

        '在庫のデータ取得
        ds = Me.DacAccess(ds, "SelectZaikoData")

        ''Maxシーケンスのデータ取得
        'ds = Me.SetMaxNo(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 削除処理チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsDeleteChk(ByVal ds As DataSet) As Boolean

        Dim rtnResult As Boolean = True

        ''EDIデータチェック
        'rtnResult = rtnResult AndAlso Me.IsEdiDataChk(ds)

        '引当済みチェック
        rtnResult = rtnResult AndAlso Me.IsHikiateChk(ds)
        'END YANAI 要望番号573

        '運賃確定チェック
        Dim msg As String = "削除"
        rtnResult = rtnResult AndAlso Me.IsUnchinKakuteiChk(ds, msg)

        '支払確定済みチェック
        rtnResult = rtnResult AndAlso Me.IsShiharaiKakuteiChk(ds, msg)

        '振替番号チェック
        rtnResult = rtnResult AndAlso Me.IsFuriDataChk(ds, msg)

        '作業レコードステージチェック
        rtnResult = rtnResult AndAlso Me.IsSagyoStageChk(ds)

        '在庫移動チェック
        rtnResult = rtnResult AndAlso Me.IsIdoTrsChk(ds)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As Boolean

        Dim inkaLDr As DataRow = ds.Tables("LMB020_INKA_L").Rows(0)
        Dim chkDate As String = inkaLDr.Item("INKA_DATE").ToString()
        Dim msg As String = "元黒データ入荷削除"

        '荷主情報取得
        ds = Me.SelectCustData(ds)

        '完了済チェック
        Dim rtnResult As Boolean = Me.ChkDateHokanNiyaku(ds, msg)

        '請求日チェック(作業料)
        rtnResult = rtnResult AndAlso Me.ChkSeiqDateSagyo(ds, ChkDate, msg)

        '請求日チェック(運賃)
        rtnResult = rtnResult AndAlso Me.ChkSeiqDateUnchin(ds, chkDate, msg, True)

        '入荷(大)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateInkaLDelFlg")

        '入荷(中)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateInkaMSysDelFlg")

        '入荷(小)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateInkaSSysDelFlg")

        '在庫の論理削除
        rtnResult = rtnResult AndAlso Me.DeleteZaiData(ds)

        '運送情報の削除処理
        rtnResult = rtnResult AndAlso Me.DeleteUnsoData(ds)

        '作業の物理削除
        rtnResult = rtnResult AndAlso Me.DeleteSagyoData(ds)

        '出荷の論理削除
        rtnResult = rtnResult AndAlso Me.DeleteOutKaData(ds)

        ''2014.04.24 CALT対応 入荷大削除時キャンセルデータ作成 黎 追加 --ST--
        'rtnResult = rtnResult AndAlso Me.DelInkaPlanSendData(ds)
        ''2014.04.24 CALT対応 入荷大削除時キャンセルデータ作成 黎 追加 --ED--

        Return rtnResult

    End Function

    ''' <summary>
    ''' 荷主情報を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCustData(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, "SelectCustData")

    End Function

    ''' <summary>
    ''' 完了済チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkDateHokanNiyaku(ByVal ds As DataSet, ByVal msg As String) As Boolean
        Return Me.ChkDate2(ds, msg)
    End Function

    ''' <summary>
    ''' 日付チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkDate2(ByVal ds As DataSet, ByVal msg As String) As Boolean

        '比較対象1に値がない場合、スルー
        Dim hokanDate As String = ds.Tables("LMB020_INKA_L").Rows(0).Item("HOKAN_STR_DATE").ToString()
        If String.IsNullOrEmpty(hokanDate) = True Then
            Return True
        End If

        'M単位でチェック
        Dim inkaMDt As DataTable = ds.Tables("LMB020_INKA_M")
        Dim drs As DataRow() = inkaMDt.Select(String.Empty, "CUST_CD_S,CUST_CD_SS")
        Dim max As Integer = drs.Length - 1

        'Mがない場合、代表コードチェック
        If max < 0 Then
            Return Me.SelectSubCustDataAtDateChk(ds, hokanDate, msg)
        End If

        Dim dr As DataRow = Nothing
        Dim sCd As String = String.Empty
        Dim ssCd As String = String.Empty
        Dim chkSCd As String = String.Empty
        Dim chkSSCd As String = String.Empty

        '検索用(処理軽量化のために必要のものだけテーブル設定)
        Dim selectDs As DataSet = New DataSet()
        Dim mDt As DataTable = inkaMDt.Copy
        Dim custDt As DataTable = ds.Tables("CUST").Copy
        selectDs.Tables.Add(mDt)
        custDt.Clear()
        selectDs.Tables.Add(custDt)

        For i As Integer = 0 To max

            'チェックする値を設定
            dr = drs(i)
            chkSCd = dr.Item("CUST_CD_S").ToString()
            chkSSCd = dr.Item("CUST_CD_SS").ToString()

            '前回の値と同じ場合、スルー
            If sCd.Equals(chkSCd) = True _
                AndAlso ssCd.Equals(chkSSCd) = True _
                Then
                Continue For
            End If

            '新しいコードを設定
            sCd = chkSCd
            ssCd = chkSSCd

            '検索する行を設定
            mDt.Clear()
            mDt.ImportRow(dr)

            '検索処理
            selectDs = Me.DacAccess(selectDs, "SelectSubCustData")

            '入力チェック
            If Me.SelectSubCustDataAtDateChk(selectDs, hokanDate, msg) = False Then
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 日付チェック
    ''' </summary>
    ''' <param name="value1">比較対象日</param>
    ''' <param name="value2">最終締め日</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkDate(ByVal value1 As String, ByVal value2 As String, ByVal msg As String, ByVal shoriKb As String, ByVal ds As DataSet) As Boolean

        '比較対象1に値がない場合、スルー
        If String.IsNullOrEmpty(value1) = True Then
            Return True
        End If

        'すでに締め処理が締め処理が終了している場合、エラー
        If value1 <= value2 Then
            Select Case shoriKb
                Case "SelectGheaderDataUnchin"
                    '運賃
                    If ("40").Equals(ds.Tables("LMB020_UNSO_L").Rows(0).Item("TARIFF_BUNRUI_KB").ToString()) = True Then
                        '横持ちの場合
                        'MyBase.SetMessage("E285", New String() {"横持ち料", msg})
                        MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E285", New String() {"横持ち料", msg}, "", "", "")
                    Else
                        '運賃の場合
                        'MyBase.SetMessage("E285", New String() {"運賃", msg})
                        MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E285", New String() {"運賃", msg}, "", "", "")
                    End If
                Case "SelectGheaderDataSagyo"
                    '作業
                    'MyBase.SetMessage("E285", New String() {"作業料", msg})
                    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E285", New String() {"作業料", msg}, "", "", "")
            End Select
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' 商品の荷主を取得し日付をチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="hokanDate">画面 起算日</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SelectSubCustDataAtDateChk(ByVal ds As DataSet, ByVal hokanDate As String, ByVal msg As String) As Boolean

        Dim custDt As DataTable = ds.Tables("CUST")
        Dim calcDate As String = String.Empty
        If 0 < custDt.Rows.Count Then
            calcDate = custDt.Rows(0).Item("HOKAN_NIYAKU_CALCULATION").ToString()
        End If

        '起算日、最終計算日チェック
        Return Me.IsHokanShimeChk(hokanDate, calcDate, msg)

    End Function

    ''' <summary>
    ''' 起算日、最終計算日チェック
    ''' </summary>
    ''' <param name="value1">画面 起算日</param>
    ''' <param name="value2">荷主M 最終計算日</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHokanShimeChk(ByVal value1 As String, ByVal value2 As String, ByVal msg As String) As Boolean

        '荷主M 最終計算日がない場合、スルー
        If String.IsNullOrEmpty(value2) = True Then
            Return True
        End If

        'すでに締め処理が締め処理が終了している場合、エラー
        If value1 <= value2 Then

            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E375", New String() {"保管料・荷役料が既に計算されている", msg}, "", "", "")
            'MyBase.SetMessage("E375", New String() {"保管料・荷役料が既に計算されている", msg})
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' 請求日チェック(作業料)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="value">入荷日</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkSeiqDateSagyo(ByVal ds As DataSet, ByVal value As String, ByVal msg As String) As Boolean

        '作業レコードがない場合、スルー
        Dim drs As DataRow() = ds.Tables("LMB020_SAGYO").Select("SYS_DEL_FLG = '0' AND UP_KBN <> '2' ")
        Dim max As Integer = drs.Length - 1
        If max < 0 Then
            Return True
        End If

        '作業の請求日チェック
        If Me.ChkDate(value, Me.SelectGheaderData(ds.Copy, "SelectGheaderDataSagyo"), msg, "SelectGheaderDataSagyo", ds) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 請求日チェック(運賃料)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="value">入荷日</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="selectFlg">検索フラグ　True:検索有　False:検索無</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkSeiqDateUnchin(ByVal ds As DataSet, ByVal value As String, ByVal msg As String, ByVal selectFlg As Boolean) As Boolean

        'レコードがない場合、スルー
        Dim dt As DataTable = ds.Tables("LMB020_UNSO_L")
        If dt.Rows.Count < 1 Then
            Return True
        End If

        '削除レコードの場合、スルー
        If LMConst.FLG.ON.Equals(dt.Rows(0).Item("SYS_DEL_FLG").ToString()) Then
            Return True
        End If

        '運賃情報の取得
        Dim selectDs As DataSet = Nothing
        If selectFlg = True Then
            selectDs = Me.DacAccess(ds, "SelectUnchinData")
        Else
            selectDs = ds.Copy
        End If
        Dim selectDt As DataTable = selectDs.Tables("F_UNCHIN_TRS")
        Dim max As Integer = selectDt.Rows.Count - 1

        Dim chkDs As DataSet = selectDs.Copy
        Dim chkDt As DataTable = chkDs.Tables("F_UNCHIN_TRS")

        For i As Integer = 0 To max

            chkDt.Clear()
            chkDt.ImportRow(selectDt.Rows(i))

            '運賃の請求日チェック
            If Me.ChkDate(value, Me.SelectGheaderData(chkDs, "SelectGheaderDataUnchin"), msg, "SelectGheaderDataUnchin", ds) = False Then
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 運送情報の削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteUnsoData(ByVal ds As DataSet) As Boolean

        'レコードがない場合、スルー
        Dim unsoLDt As DataTable = ds.Tables("LMB020_UNSO_L")
        If unsoLDt.Rows.Count < 1 Then
            Return True
        End If

        '運送番号(大)がない場合、スルー
        If String.IsNullOrEmpty(unsoLDt.Rows(0).Item("UNSO_NO_L").ToString()) = True Then
            Return True
        End If

        'あえて順番を変えている
        '運送(中)の物理削除
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "DeleteUnsoMData")

        '運賃の物理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteUnchinData")

        '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
        '支払の物理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteShiharaiData")
        '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

        '運送(大)の物理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteUnsoLData")

        Return rtnResult

    End Function

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

    End Function

    ''' <summary>
    ''' DACクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DacAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallDAC(Me._MotoDelDac, actionId, ds)

    End Function

    ''' <summary>
    ''' 在庫の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteZaiData(ByVal ds As DataSet) As Boolean

        ds = Me.SetDeleteData(ds, "LMB020_ZAI")

        Return Me.ServerChkJudge(ds, "UpdateZaiTrsData")

    End Function

    ''' <summary>
    ''' 作業の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteSagyoData(ByVal ds As DataSet) As Boolean

        ds = Me.SetDeleteData(ds, "LMB020_SAGYO")

        Return Me.ServerChkJudge(ds, "UpdateSagyoSysData")

    End Function

    ''' <summary>
    ''' 出荷情報の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteOutKaData(ByVal ds As DataSet) As Boolean

        '振替番号に値がない場合、スルー
        If String.IsNullOrEmpty(ds.Tables("LMB020_INKA_L").Rows(0).Item("FURI_NO").ToString()) = True Then
            Return True
        End If

        '出荷(大)の論理削除
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "DeleteOutKaL")

        '出荷(中)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteOutKaM")

        '出荷(小)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteOutKaS")

        Return rtnResult

    End Function

    ''' <summary>
    ''' 削除フラグを設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNm">DataTable名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDeleteData(ByVal ds As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = ds.Tables(tblNm)
        Dim max As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To max
            dt.Rows(i).Item("SYS_DEL_FLG") = LMConst.FLG.ON
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 最終請求日を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>請求日</returns>
    ''' <remarks>取得できない場合は"00000000"を返却</remarks>
    Private Function SelectGheaderData(ByVal ds As DataSet, ByVal actionId As String) As String

        ds = Me.DacAccess(ds, actionId)

        Dim dt As DataTable = ds.Tables("G_HED")
        If dt.Rows.Count < 1 Then

            Return "00000000"

        End If

        Return dt.Rows(0).Item("SKYU_DATE").ToString()

    End Function

    ''' <summary>
    ''' EDIデータチェック（削除時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsEdiDataChk(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables("LMB020_INKA_M")
        Dim dr() As DataRow = dt.Select("(JISSEKI_FLAG = '1' OR JISSEKI_FLAG = '2') AND EDI_FLG = '1' AND SYS_DEL_FLG = '0'")
        Dim cnt As Integer = dr.Length

        'EDIで作成されたデータ場合、エラー
        If 0 < cnt Then
            'Return Me._Vcon.SetErrMessage("E426", New String() {"削除"})
            'Return MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E426", New String() {"削除"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E426", New String() {"削除"}, "", LMH010BLC.EXCEL_COLTITLE, "")
            Return False
        End If

        Return True

    End Function

    'LMB020クライアントチェックSTART
    ''' <summary>
    ''' 引当済みチェック（削除時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHikiateChk(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables("LMB020_INKA_M")
        Dim dr() As DataRow = dt.Select("SYS_DEL_FLG = '0'")
        Dim max As Integer = dr.Length - 1
        For i As Integer = 0 To max

            '引当済みの場合、エラー
            If "済".Equals(dr(i).Item("HIKIATE").ToString()) = True Then
                'Return Me._Vcon.SetErrMessage("E139")
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E139", New String() {}, "", "入荷管理番号", String.Concat(dr(i).Item("INKA_NO_L").ToString(), "-", dr(i).Item("INKA_NO_M").ToString()))
                Return False
            End If
        Next

        Return True

    End Function

    ''' <summary>
    ''' 運賃確定チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnchinKakuteiChk(ByVal ds As DataSet, ByVal msg As String) As Boolean

        Dim unsoLDt As DataTable = ds.Tables("LMB020_UNSO_L")

        'レコードがない場合、スルー
        If unsoLDt.Rows.Count < 1 Then
            Return True
        End If

        '確定済みの場合、エラー
        If LMConst.FLG.OFF.Equals(unsoLDt.Rows(0).Item("FIXED_CHK").ToString()) = False Then
            'Return Me._Vcon.SetErrMessage("E126", New String() {String.Empty})
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E126", New String() {}, "", "運送番号", unsoLDt.Rows(0).Item("UNSO_NO_L").ToString())
            Return False
        End If

        'まとめ済みの場合、エラー
        If LMConst.FLG.OFF.Equals(unsoLDt.Rows(0).Item("GROUP_CHK").ToString()) = False Then
            'Return Me._Vcon.SetErrMessage("E232", New String() {"まとめ指示", msg})
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E232", New String() {"まとめ指示", msg}, "運送番号", unsoLDt.Rows(0).Item("UNSO_NO_L").ToString())
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 支払確定チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsShiharaiKakuteiChk(ByVal ds As DataSet, ByVal msg As String) As Boolean

        Dim unsoLDt As DataTable = ds.Tables("LMB020_UNSO_L")

        'レコードがない場合、スルー
        If unsoLDt.Rows.Count < 1 Then
            Return True
        End If

        '確定済みの場合、エラー
        If LMConst.FLG.OFF.Equals(unsoLDt.Rows(0).Item("SHIHARAI_FIXED_CHK").ToString()) = False Then
            'Return Me._Vcon.SetErrMessage("E497", New String() {String.Empty})
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E497", New String() {}, "", "運送番号", unsoLDt.Rows(0).Item("UNSO_NO_L").ToString())
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 振替番号チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsFuriDataChk(ByVal ds As DataSet, ByVal msg As String) As Boolean

        '振替管理番号がない場合、スルー
        If String.IsNullOrEmpty(ds.Tables("LMB020_INKA_L").Rows(0).Item("FURI_NO").ToString()) = True Then
            Return True
        End If

        'Return Me._Vcon.SetErrMessage("E028", New String() {"振替データ", msg})
        MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E028", New String() {"振替データ", msg}, "", "振替管理番号", ds.Tables("LMB020_INKA_L").Rows(0).Item("FURI_NO").ToString())
        Return False

    End Function

    ''' <summary>
    ''' 作業レコードのステージチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSagyoStageChk(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables("LMB020_SAGYO")
        Dim max As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To max

            With dt.Rows(i)

                '削除されているデータの場合、スルー
                If LMConst.FLG.ON.Equals(.Item("SYS_DEL_FLG").ToString()) = True Then
                    Continue For
                End If

                '作業が完了している場合、エラー
                If "01".Equals(.Item("SKYU_CHK").ToString()) = True Then
                    'Return Me._Vcon.SetErrMessage("E127")
                    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E127", New String() {}, "", "入荷管理番号", .Item("INOUTKA_NO_LM").ToString())
                    Return False
                End If

            End With

        Next

        Return True

    End Function

    ''' <summary>
    ''' 在庫移動チェック（削除時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsIdoTrsChk(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables("LMB020_INKA_S")
        Dim dr() As DataRow = dt.Select("SYS_DEL_FLG = '0'")
        Dim max As Integer = dr.Length - 1

        For i As Integer = 0 To max

            '在庫移動がある場合、エラー
            If String.IsNullOrEmpty(dr(i).Item("ZAI_REC_CNT").ToString()) = True Then
                dr(i).Item("ZAI_REC_CNT") = "0"
            End If

            If 0 < Convert.ToInt32(dr(i).Item("ZAI_REC_CNT").ToString()) Then
                'Return Me._Vcon.SetErrMessage("E148")
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E148", New String() {}, "", "入荷管理番号", dr(i).Item("INKA_NO_L").ToString())
                Return False
            End If
        Next

        Return True

    End Function
    'LMB020クライアントチェックEND

#End Region
    '2014.06.09 使用END
    '↑FFEM特殊処理↑

#Region "紐付け処理"
    ''' <summary>
    ''' 紐付け処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Himoduke(ByVal ds As DataSet) As DataSet

        '紐付けフラグの設定
        ds = Me.SetHimodukeFlg(ds)

        '受信DTLデータセット
        ds = Me.SetDatasetRcvDtl(ds)

        'EDI入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)

        'EDI入荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)

        '受信明細の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        Return ds
    End Function

    ''' <summary>
    ''' 紐付けフラグの設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetHimodukeFlg(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)

        dr.Item("MATCHING_FLAG") = "01"

        Return ds

    End Function

#End Region

#Region "左埋処理"
    ''' <summary>
    ''' 0埋処理
    ''' </summary>
    ''' <param name="val">対象文字列</param>
    ''' <param name="keta">0埋後の桁数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FormatZero(ByVal val As String, ByVal keta As Integer) As String

        val = val.PadLeft(keta, "0"c)

        Return val

    End Function

    ''' <summary>
    ''' スペース埋処理
    ''' </summary>
    ''' <param name="val"></param>
    ''' <param name="keta"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function FormatSpace(ByVal val As String, ByVal keta As Integer) As String

        val = val.PadLeft(keta)

        Return val

    End Function


#End Region

    ''' <summary>
    ''' 条件の再設定
    ''' </summary>
    ''' <param name="setDt"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>条件の再設定(ワーニング画面よりNRS商品コードが設定されている場合はそのNRS商品コードを使う)</remarks>
    Private Function SetGoodsCdFromWarning(ByVal setDt As DataTable, ByVal ds As DataSet, ByVal warningId As String) As DataTable

        Dim dtWarning As DataTable = ds.Tables("WARNING_SHORI")
        Dim ediCtlNoL As String = setDt.Rows(0)("EDI_CTL_NO").ToString()
        Dim ediCtlNoM As String = setDt.Rows(0)("EDI_CTL_NO_CHU").ToString()
        Dim max As Integer = dtWarning.Rows.Count - 1
        Dim dr As DataRow

        'ワーニング処理設定されていなければ処理終了
        If max = -1 Then
            Return setDt
        End If

        For i As Integer = 0 To max

            dr = dtWarning.Rows(i)

            If warningId.Equals(dr("EDI_WARNING_ID").ToString()) AndAlso ediCtlNoL.Equals(dr("EDI_CTL_NO_L")) _
                                                                AndAlso ediCtlNoM.Equals(dr("EDI_CTL_NO_M")) Then
                'ワーニング処理設定の値を反映
                setDt.Rows(0).Item("NRS_GOODS_CD") = dr.Item("MST_VALUE")

            End If

        Next

        Return setDt
    End Function

#End Region

End Class
