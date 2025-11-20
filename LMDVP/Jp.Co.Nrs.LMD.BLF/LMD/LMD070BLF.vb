' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 在庫
'  プログラムID     :  LMD070BLF : 在庫印刷指示
'  作  成  者       :  成田
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD070BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD070BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _BLC As LMD070BLC = New LMD070BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print530 As LMD530BLC = New LMD530BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print550 As LMD550BLC = New LMD550BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print570 As LMD570BLC = New LMD570BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print571 As LMD571BLC = New LMD571BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print590 As LMD590BLC = New LMD590BLC()

    '''' <summary>
    '''' 使用するBLCクラスの生成
    '''' </summary>
    '''' <remarks></remarks>
    'Private _Print580 As LMD580BLC = New LMD580BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print560 As LMD560BLC = New LMD560BLC()

    '(2013.01.10)要望番号1752 消防分類別在庫重量表 -- START --
    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print620 As LMD620BLC = New LMD620BLC()
    '(2013.01.10)要望番号1752 消防分類別在庫重量表 --  END  --

    '2013.02.01 消防類別在庫重量表 処理スピード向上 Start
    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print621 As LMD621BLC = New LMD621BLC()
    '2013.02.01 消防類別在庫重量表 処理スピード向上 End

    '2017.08.15 消防類別在庫重量表(全荷主・現在庫版)
    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print622 As LMD622BLC = New LMD622BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print630 As LMD630BLC = New LMD630BLC()


    ''' <summary>
    ''' INテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMD070IN"

#End Region

#Region "Method"

#Region "検索"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = MyBase.CallBLC(Me._BLC, "SelectData", ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '検索結果取得
        Return MyBase.CallBLC(Me._BLC, "SelectListData", ds)

    End Function

#End Region

#Region "完了チェック"

    ''' <summary>
    ''' 完了チェック
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function SelectCheck(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMD070BLF.TABLE_NM_IN)
        Dim dr As DataRow = dt.Rows(0)
        Dim zero As String = "00000000"
        Dim Getu As String = dr.Item("GETSUMATSU_ZAIKO").ToString

        '月末在庫が入力されている時、初期在庫の時は処理をしない
        If String.IsNullOrEmpty(Getu) = False AndAlso _
        zero.Equals(Getu) = False Then

            '月末在庫のチェック
            ds = MyBase.CallBLC(Me._BLC, "SelectDataSeigo", ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If
        End If

        '印刷種別が在庫証明書の場合チェックをする
        Dim Print As String = dr.Item("PRINT_FLAG").ToString
        Dim hantei As String = "03"
        If hantei.Equals(Print) = True Then
            '月末在庫が初期在庫のときに完了チェックをする
            If zero.Equals(Getu) = True Then

                'データ件数取得
                ds = MyBase.CallBLC(Me._BLC, "SelectDataKanryou", ds)
                'メッセージの判定
                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If

            End If
        End If

        ds = Me.PrintData(ds)

        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function



    'START 在庫証明書 処理スピード向上

#Region "SelectCheckLMD571"

    ''' <summary>
    ''' 完了チェック(LMD571)
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function SelectCheckLMD571(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMD070BLF.TABLE_NM_IN)
        Dim dr As DataRow = dt.Rows(0)
        Dim zero As String = "00000000"
        Dim Getu As String = dr.Item("GETSUMATSU_ZAIKO").ToString

        '月末在庫が入力されている時、初期在庫の時は処理をしない
        If String.IsNullOrEmpty(Getu) = False AndAlso _
        zero.Equals(Getu) = False Then

            '月末在庫のチェック
            ds = MyBase.CallBLC(Me._BLC, "SelectDataSeigo", ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If
        End If

        '印刷種別が在庫証明書の場合チェックをする
        Dim Print As String = dr.Item("PRINT_FLAG").ToString
        Dim hantei As String = "03"
        If hantei.Equals(Print) = True Then
            '月末在庫が初期在庫のときに完了チェックをする
            If zero.Equals(Getu) = True Then

                'データ件数取得
                ds = MyBase.CallBLC(Me._BLC, "SelectDataKanryou", ds)
                'メッセージの判定
                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If

            End If
        End If

        ds = Me.PrintDataLMD571(ds)

        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function

#End Region

    'END 在庫証明書 処理スピード向上


#End Region

#Region "印刷"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function PrintData(ByVal ds As DataSet) As DataSet

        '印刷種別がチェックリストか、出荷報告書を取得
        Dim dt As DataTable = ds.Tables(LMD070BLF.TABLE_NM_IN)
        Dim dr As DataRow = dt.Rows(0)
        Dim flg As String = String.Empty

        flg = dr.Item("PRINT_FLAG").ToString()

        Select Case flg

            Case "01"  '日次出荷別在庫リスト
                ds = Me.PrintLMD530(dr)

            Case "02"  '在庫表
                'Call Me.PrintLMD550(dr)

            Case "03"  '在庫証明書
                ds = Me.PrintLMD570(dr)

            Case "07"  '不動在庫リスト
                ds = Me.PrintLMD550(dr)

            Case "04", "05", "06"  '在庫受払表(各種)
                'Call Me.PrintLMD580(dr)

            Case "08", "09", "10"  '在庫整合性リスト 
                '印刷種別でPTNの種別の変更
                Dim Ptn As String = String.Empty
                Select Case flg
                    Case "08"      '在庫整合性リスト(実在庫)
                        Ptn = "1"
                    Case "09"      '在庫整合性リスト(出荷予定日)
                        Ptn = "2"
                    Case "10"      '在庫整合性リスト(引当数)
                        Ptn = "3"
                End Select

                'LMD560の呼び出し
                ds = Me.PrintLMD560(dr, Ptn)

            Case "11" '入出荷履歴表
                ds = Me.PrintLMD590(dr)

            Case "12" '在庫証明書(小･極小毎)
                ds = Me.PrintLMD570(dr)

            Case "13", "15" '消防分類別在庫重量表
                ds = Me.PrintLMD620(dr)

            Case "14" '在庫受払表(月間入出庫重量集計表)

                ds = Me.PrintLMD630(dr)

        End Select

        Return ds

    End Function

    ''' <summary>
    ''' 日次出荷別在庫リスト
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMD530(ByVal dr As DataRow) As DataSet

        '日次出荷別在庫リスト
        Dim PrmDs As DataSet = New LMD530DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMD530IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
        PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
        PrmDr("OUTKA_DATE") = dr.Item("OUTKA_PLAN_DATE")

        PrmDt.Rows.Add(PrmDr)

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        PrmDs.Merge(New RdPrevInfoDS)

        'LMD530の呼び出し
        PrmDs = MyBase.CallBLC(Me._Print530, "DoPrint", PrmDs)

        Return PrmDs

    End Function

    ''' <summary>
    ''' 在庫証明書／在庫証明書(小･極小毎)
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMD570(ByVal dr As DataRow) As DataSet

        '在庫証明書
        Dim PrmDs As DataSet = New LMD570DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMD570IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
        PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
        '(2012.12.14)要望番号1671 抽出条件追加 -- START --
        PrmDr("CUST_CD_S") = dr.Item("CUST_CD_S")
        PrmDr("CUST_CD_SS") = dr.Item("CUST_CD_SS")
        '(2012.12.14)要望番号1671 抽出条件追加 --  END  --
        PrmDr("PRINT_FROM") = dr.Item("PRT_YMD_FROM")
        PrmDr("ZAI_GETU") = dr.Item("GETSUMATSU_ZAIKO")
        If String.IsNullOrEmpty(dr.Item("GETSUMATSU_ZAIKO").ToString()) = True _
        OrElse "00000000".Equals(dr.Item("GETSUMATSU_ZAIKO")) = True Then
            PrmDr("GETU_FLG") = "0"
        Else
            PrmDr("GETU_FLG") = "1"
        End If
        'START YANAI 要望番号1057 在庫証明書出力順変更
        PrmDr("SORT_KBN") = dr.Item("SORT_KBN")
        'END YANAI 要望番号1057 在庫証明書出力順変更

        'ADD 2017/06/01 角印対応
        PrmDr("KAKUIN_FLG") = dr.Item("KAKUIN_FLG")
        PrmDr("KAKUIN_NM") = dr.Item("KAKUIN_NM")

        PrmDt.Rows.Add(PrmDr)

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        PrmDs.Merge(New RdPrevInfoDS)

        'LMD570の呼び出し
        PrmDs = MyBase.CallBLC(Me._Print570, "DoPrint", PrmDs)

        Return PrmDs

    End Function


    '(2013.01.10)要望番号1752 消防分類別在庫重量表 -- START --
    ''' <summary>
    ''' 消防分類別在庫重量表
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMD620(ByVal dr As DataRow) As DataSet

        '消防分類別在庫重量表
        Dim PrmDs As DataSet = New LMD620DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMD620IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
        PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
        PrmDr("PRINT_FROM") = dr.Item("PRT_YMD_FROM")
        If String.IsNullOrEmpty(dr.Item("GETSUMATSU_ZAIKO").ToString()) = True _
        OrElse "00000000".Equals(dr.Item("GETSUMATSU_ZAIKO")) = True Then
            PrmDr("GETU_FLG") = "0"
        Else
            PrmDr("GETU_FLG") = "1"
        End If

        PrmDt.Rows.Add(PrmDr)

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        PrmDs.Merge(New RdPrevInfoDS)

        '2013.02.01 消防類別在庫重量表 処理スピード向上 Start
        If dr.Item("SHOBO_FLG").ToString.Equals("00") = True Then
            'LMD620の呼び出し
            PrmDs = MyBase.CallBLC(Me._Print620, "DoPrint", PrmDs)
        ElseIf dr.Item("SHOBO_FLG").ToString.Equals("01") = True Then
            'LMD621の呼び出し
            PrmDs = MyBase.CallBLC(Me._Print621, "DoPrint", PrmDs)
        ElseIf dr.Item("SHOBO_FLG").ToString.Equals("02") = True Then
            'LMD622の呼び出し
            PrmDs = MyBase.CallBLC(Me._Print622, "DoPrint", PrmDs)
        End If

        ''LMD620の呼び出し
        'PrmDs = MyBase.CallBLC(Me._Print620, "DoPrint", PrmDs)
        '2013.02.01 消防類別在庫重量表 処理スピード向上 End

        Return PrmDs

    End Function
    '(2013.01.10)要望番号1752 消防分類別在庫重量表 --  END  --

    'START 在庫証明書 処理スピード向上
#Region "PrintDataLMD571"

    ''' <summary>
    ''' 在庫証明書／在庫証明書(小･極小毎)（ワークテーブル経由）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintDataLMD571(ByVal ds As DataSet) As DataSet

        '印刷種別がチェックリストか、出荷報告書を取得
        Dim dt As DataTable = ds.Tables(LMD070BLF.TABLE_NM_IN)
        Dim dr As DataRow = dt.Rows(0)

        '在庫証明書
        Dim PrmDs As DataSet = New LMD570DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMD570IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
        PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
        '(2012.12.14)要望番号1671 抽出条件追加 -- START --
        PrmDr("CUST_CD_S") = dr.Item("CUST_CD_S")
        PrmDr("CUST_CD_SS") = dr.Item("CUST_CD_SS")
        '(2012.12.14)要望番号1671 抽出条件追加 --  END  --
        PrmDr("PRINT_FROM") = dr.Item("PRT_YMD_FROM")
        PrmDr("ZAI_GETU") = dr.Item("GETSUMATSU_ZAIKO")
        If String.IsNullOrEmpty(dr.Item("GETSUMATSU_ZAIKO").ToString()) = True _
        OrElse "00000000".Equals(dr.Item("GETSUMATSU_ZAIKO")) = True Then
            PrmDr("GETU_FLG") = "0"
        Else
            PrmDr("GETU_FLG") = "1"
        End If

        PrmDr("SORT_KBN") = dr.Item("SORT_KBN")

        'ADD 2017/06/01 角印対応
        PrmDr("KAKUIN_FLG") = dr.Item("KAKUIN_FLG")

        PrmDr("KAKUIN_NM") = dr.Item("KAKUIN_NM")


        PrmDt.Rows.Add(PrmDr)

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        PrmDs.Merge(New RdPrevInfoDS)

        'LMD571の呼び出し
        Dim BLC As LMD571BLC = New LMD571BLC
        PrmDs = MyBase.CallBLC(BLC, "DoPrint", PrmDs)

        Return PrmDs

    End Function

#End Region
    'END   在庫証明書 処理スピード向上


    ''' <summary>
    ''' 在庫整合性リスト
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="ptn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMD560(ByVal dr As DataRow, ByVal ptn As String) As DataSet

        '在庫整合性リスト
        Dim PrmDs As DataSet = New LMD560DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMD560IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        PrmDr("PTN") = ptn
        PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
        PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
        PrmDr("OUTPUT_DATE") = dr.Item("PRT_YMD_FROM")
        PrmDr("RIREKI_DATE") = dr.Item("GETSUMATSU_ZAIKO")

        PrmDt.Rows.Add(PrmDr)

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        PrmDs.Merge(New RdPrevInfoDS)

        'LMD560の呼び出し
        PrmDs = MyBase.CallBLC(Me._Print560, "DoPrint", PrmDs)

        Return PrmDs

    End Function

    ''' <summary>
    ''' 不動在庫リスト
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMD550(ByVal dr As DataRow) As DataSet

        '在庫整合性リスト
        Dim PrmDs As DataSet = New LMD550DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMD550IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
        PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
        PrmDr("DATE_FROM") = dr.Item("PRT_YMD_FROM")
        PrmDr("DATE_TO") = dr.Item("PRT_YMD_TO")
        PrmDr("DATE_FLG") = dr.Item("ZAI_KIJUN")
        PrmDr("IDO_TANI_FLG") = dr.Item("MOVE_TANNI")

        PrmDt.Rows.Add(PrmDr)

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        PrmDs.Merge(New RdPrevInfoDS)

        'LMD550の呼び出し
        PrmDs = MyBase.CallBLC(Me._Print550, "DoPrint", PrmDs)

        Return PrmDs

    End Function

    ''' <summary>
    ''' 入出荷履歴表
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMD590(ByVal dr As DataRow) As DataSet

        '在庫整合性リスト
        Dim PrmDs As DataSet = New LMD590DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMD590IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
        PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
        PrmDr("DATE_FROM") = dr.Item("PRT_YMD_FROM")
        PrmDr("DATE_TO") = dr.Item("PRT_YMD_TO")

        PrmDt.Rows.Add(PrmDr)

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        PrmDs.Merge(New RdPrevInfoDS)

        'LMD590の呼び出し
        PrmDs = MyBase.CallBLC(Me._Print590, "DoPrint", PrmDs)

        Return PrmDs

    End Function

    ''' <summary>
    ''' 在庫受払表 (月間入出庫重量集計表)
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintLMD630(ByVal dr As DataRow) As DataSet

        '消防分類別在庫重量表
        Dim PrmDs As DataSet = New LMD630DS()
        Dim PrmDt As DataTable = PrmDs.Tables("LMD630IN")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        PrmDr("NRS_BR_CD") = dr.Item("NRS_BR_CD")
        PrmDr("CUST_CD_L") = dr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = dr.Item("CUST_CD_M")
        PrmDr("PRINT_FROM") = dr.Item("PRT_YMD_FROM")
        PrmDr("PRINT_TO") = dr.Item("PRT_YMD_TO")
        PrmDr("PRINT_FLAG") = dr.Item("PRINT_FLAG")
        PrmDr("PRINT_SUB_FLAG") = dr.Item("PRINT_SUB_FLAG")

        PrmDt.Rows.Add(PrmDr)

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        PrmDs.Merge(New RdPrevInfoDS)

        'LMD630の呼び出し
        'Using scope As TransactionScope = MyBase.BeginTransaction()
        PrmDs = MyBase.CallBLC(Me._Print630, "DoPrint", PrmDs)
        'トランザクション終了
        'MyBase.CommitTransaction(scope)
        'End Using

        'PrmDs = MyBase.CallBLC(Me._Print630, "DoPrint", PrmDs)

        Return PrmDs

    End Function

#End Region

#End Region

End Class
