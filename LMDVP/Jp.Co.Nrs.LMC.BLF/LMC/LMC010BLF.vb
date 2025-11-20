' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC010BLC : 出荷データ検索
'  作  成  者       :  [金ヘスル]:
' ==========================================================================
Option Strict On
Option Explicit On

Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility
Imports System.Text.RegularExpressions

''' <summary>
''' LMC010BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC010BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Const"
    Private Class PRINT_TYPE

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <remarks></remarks>
        Public Const PACKAGE_DETAILS As String = "20"

#If True Then   'ADD 2018/07/24  依頼番号 : 001540  
        Public Const UNSO_HOKEN As String = "21"
#End If
        Public Const ATTEND As String = "23"

        Public Const OUTBOUND_CHECK As String = "24"

#If True Then   'ADD 2023/03/29 送品案内書(FFEM)追加
        Public Const SHIPMENTGUIDE As String = "25"
        Public Const SHIPMENTGUIDE_EX As String = "25EX"
#End If
        Public Const DOKU_JOJU As String = "26"
        Public Const DOKU_JOJU_EX As String = "26EX"
    End Class


    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Class TABLE_NM
        Public Const LMC010_INT_EDI As String = LMC010BLC.TABLE_NM.LMC010_INT_EDI
    End Class

#End Region


#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMC010BLC = New LMC010BLC()

    'START YANAI 要望番号638
    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _UnchinBlc As LMF800BLC = New LMF800BLC()
    'END YANAI 要望番号638

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _ShiharaiBlc As LMF810BLC = New LMF810BLC()
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = MyBase.CallBLC(Me._Blc, "SelectData", ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

    End Function

    '2013.02.18 アグリマート対応 START
    ''' <summary>
    ''' データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectOutkaPickWkメソッドに飛ぶ</remarks>
    Private Function SelectOutkaPickWk(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectOutkaPickWk", ds)

    End Function
    '2013.02.18 アグリマート対応 END

    ''' <summary>
    ''' データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectOutkaMメソッドに飛ぶ</remarks>
    Private Function SelectOutkaM(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectOutkaM", ds)

    End Function

    'START KIM 2012/09/19 特定荷主対応
    ''' <summary>
    ''' データ検索処理(ハネウェルCSV引当)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectOutkaMメソッドに飛ぶ</remarks>
    Private Function SelectOutkaMForHW(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectOutkaMForHW", ds)

    End Function
    'END KIM 2012/09/19 特定荷主対応

    ''' <summary>
    ''' データ検索処理(分析票発行時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectBunsekiOutkaMメソッドに飛ぶ</remarks>
    Private Function SelectBunsekiOutkaM(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectBunsekiOutkaM", ds)

    End Function

    ''' <summary>
    ''' データ検索処理(イエローカード発行時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectBunsekiOutkaMメソッドに飛ぶ</remarks>
    Private Function SelectYCardOutkaM(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectYCardOutkaM", ds)

    End Function

#If True Then  ' FFEM機能改修(納品書未受信状態表示対応) 20170127 added
    ''' <summary>
    ''' データ検索処理(納品書発行)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectNeedlessNhsData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectNeedlessNhsData", ds)

    End Function
#End If

    ''' <summary>
    ''' データ検索処理(納品書発行有無FFEM)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>2019/12/03 add</remarks>
    Private Function SelectNhsNoFFEM(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectNhsNoFFEM", ds)

    End Function

    '2015.10.01 tsunehira add

    'START YANAI 要望番号627　こぐまくん対応
    ''' <summary>
    ''' データ検索処理(名鉄CSV作成時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectMeitetuCsvメソッドに飛ぶ</remarks>
    Private Function SelectMeitetuCsv(ByVal ds As DataSet) As DataSet

        'Dim rdPrevDs As New RdPrevInfoDS
        'Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone
        'Dim rdPrevDt2 As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ''送り状
        'Dim rtnDs As DataSet = SetDataSetPrintInData(ds, New LMC794DS(), "LMC794IN")
        'rtnDs.Merge(New RdPrevInfoDS)
        ''検索結果取得
        'Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMC794BLC(), "MeitetuLabel", rtnDs))
        'rdPrevDt.Merge(rtnDs2.Tables(LMConst.RD))

        ''荷札
        'Dim rtnDs3 As DataSet = SetDataSetPrintInData(ds, New LMC789DS(), "LMC789IN")
        'rtnDs3.Merge(New RdPrevInfoDS)
        ''検索結果取得
        'Dim rtnDs4 As DataSet = (MyBase.CallBLC(New LMC789BLC(), "MeitetuTag", rtnDs3))
        'rdPrevDt2.Merge(rtnDs4.Tables(LMConst.RD))

        ''CSVを出す
        'Dim rtnDs5 As DataSet = SetDataSetCsvInData(ds, New LMC800DS(), "LMC800IN")
        ''検索結果取得
        'Dim rtnDs6 As DataSet = (MyBase.CallBLC(New LMC800BLC(), "MeitetuCsv", rtnDs5))

        'rtnDs6.Merge(New RdPrevInfoDS)
        'rtnDs6.Tables(LMConst.RD).Merge(rdPrevDt)
        'rtnDs6.Tables(LMConst.RD).Merge(rdPrevDt2)

        'Return rtnDs6

        'LMC800のデータセットIN情報を設定
        Dim rtnDs As DataSet = SetDataSetCsvInData(ds, New LMC800DS(), "LMC800IN")

        '検索結果取得
        Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMC800BLC(), "MeitetuCsv", rtnDs))

        Return rtnDs

    End Function

    'END YANAI 要望番号627　こぐまくん対応

    'ADD 2016/06/14  要望番号2575　ヤマトB2 CSV出力対応
    ''' <summary>
    ''' データ検索処理(ヤマトB2 CSV出力時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectMeitetuCsvメソッドに飛ぶ</remarks>
    Private Function SelectYamatoB2Csv(ByVal ds As DataSet) As DataSet

        'LMC800のデータセットIN情報を設定
        Dim rtnDs As DataSet = SetDataSetYamatoB2CsvInData(ds, New LMC890DS(), "LMC890IN")

        '検索結果取得
        Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMC890BLC(), "YamatoB2Csv", rtnDs))

        Return rtnDs

    End Function

    'ADD 2016/06/14  要望番号2575　佐川e飛伝 CSV出力対応
    ''' <summary>
    ''' データ検索処理(佐川e飛伝 CSV出力時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectMeitetuCsvメソッドに飛ぶ</remarks>
    Private Function SelectSagawaEHidenCsv(ByVal ds As DataSet) As DataSet

        'LMC800のデータセットIN情報を設定
        Dim rtnDs As DataSet = SetDataSetSagawaEHidenCsvInData(ds, New LMC900DS(), "LMC900IN")

        '検索結果取得
        Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMC900BLC(), "SagawaEHidenCsv", rtnDs))

        Return rtnDs

    End Function

    'ADD 2017/02/24 エスライン CSV出力対応
    ''' <summary>
    ''' データ検索処理(エスラインCSV出力時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSLineCsvメソッドに飛ぶ</remarks>
    Private Function SelectSLineCsv(ByVal ds As DataSet) As DataSet

        'LMC810のデータセットIN情報を設定
        Dim rtnDs As DataSet = SetDataSetSLineCsvInData(ds, New LMC910DS(), "LMC910IN")

        '検索結果取得
        Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMC910BLC(), "SLineCsv", rtnDs))

        Return rtnDs

    End Function


    'ADD 2018/07/10 依頼番号 : 001947  　ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV出力対応
    ''' <summary>
    ''' データ検索処理(ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV出力時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSLineCsvメソッドに飛ぶ</remarks>
    Private Function SelectKangarooMagicCsv(ByVal ds As DataSet) As DataSet

        'LMC810のデータセットIN情報を設定
        Dim rtnDs As DataSet = SetDataSetKangarooMagicCsvInData(ds, New LMC920DS(), "LMC920IN")

        '検索結果取得
        Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMC920BLC(), "KangarooMagicCsv", rtnDs))

        Return rtnDs

    End Function

    ''' <summary>
    ''' データ検索処理(ｶﾝｶﾞﾙｰﾏｼﾞｯｸCSV(大黒)出力時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSLineCsvメソッドに飛ぶ</remarks>
    Private Function SelectKangarooMagicDaikokuCsv(ByVal ds As DataSet) As DataSet

        'データセットIN情報を設定
        Dim rtnDs As DataSet = SetDataSetKangarooMagicCsvInData(ds, New LMC940DS(), "LMC940IN")

        '検索結果取得
        Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMC940BLC(), "KangarooMagicCsv", rtnDs))

        Return rtnDs

    End Function

    'START YANAI 要望番号677　オカケンメイト対応
    ''' <summary>
    ''' データ検索処理(岡山貨物CSV作成時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectOkakenCsvメソッドに飛ぶ</remarks>

    Private Function SelectOkakenCsv(ByVal ds As DataSet) As DataSet

        'LMC810のデータセットIN情報を設定
        Dim rtnDs As DataSet = SetDataSetCsvInData(ds, New LMC810DS(), "LMC810IN")
        For i As Integer = 0 To ds.Tables("LMC010IN_CSV").Rows.Count - 1
            rtnDs.Tables("LMC810IN").Rows(i).Item("CSV_OUTFLG") = ds.Tables("LMC010IN_CSV").Rows(i).Item("CSV_OUTFLG").ToString
        Next

        '検索結果取得
        Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMC810BLC(), "OkakenCsv", rtnDs))

        Return rtnDs

    End Function
    'END YANAI 要望番号677　オカケンメイト対応

    '2015.10.06 tsunehira add

    'START YANAI 20120323 名鉄(大阪)対応
    ''' <summary>
    ''' データ検索処理(名鉄CSV(大阪)作成時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectMeitetuCsvOOSAKAメソッドに飛ぶ</remarks>
    Private Function SelectMeitetuCsvOOSAKA(ByVal ds As DataSet) As DataSet
        'Dim rdPrevDs As New RdPrevInfoDS
        'Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone
        'Dim rdPrevDt2 As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ''送り状
        'Dim rtnDs As DataSet = SetDataSetPrintInData(ds, New LMC794DS(), "LMC794IN")
        'rtnDs.Merge(New RdPrevInfoDS)
        ''検索結果取得
        'Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMC794BLC(), "MeitetuLabel", rtnDs))
        'rdPrevDt.Merge(rtnDs2.Tables(LMConst.RD))


        ''荷札
        'Dim rtnDs3 As DataSet = SetDataSetPrintInData(ds, New LMC789DS(), "LMC789IN")
        'rtnDs3.Merge(New RdPrevInfoDS)
        ''検索結果取得
        'Dim rtnDs4 As DataSet = (MyBase.CallBLC(New LMC789BLC(), "MeitetuTag", rtnDs3))
        'rdPrevDt2.Merge(rtnDs4.Tables(LMConst.RD))

        ''CSVを出す
        ''Dim rtnD5 As DataSet = SetDataSetCsvInData(ds, New LMC800DS(), "LMC820IN")
        ''検索結果取得
        'Dim rtnDs6 As DataSet = (MyBase.CallBLC(New LMC820BLC(), "MeitetuCsv", rtnD5))

        'rtnDs6.Merge(New RdPrevInfoDS)
        'rtnDs6.Tables(LMConst.RD).Merge(rdPrevDt)
        'rtnDs6.Tables(LMConst.RD).Merge(rdPrevDt2)

        'Return rtnDs6

        'LMC820のデータセットIN情報を設定
        Dim rtnDs As DataSet = SetDataSetCsvInData(ds, New LMC820DS(), "LMC820IN")

        '検索結果取得
        Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMC820BLC(), "MeitetuCsv", rtnDs))

        Return rtnDs

    End Function
    'END YANAI 20120323 名鉄(大阪)対応

    'START 中村 20121113 シグマ対応
    ''' <summary>
    ''' データ検索処理(シグマCSV作成時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSigmaCsvメソッドに飛ぶ</remarks>
    Private Function SelectSigmaCsv(ByVal ds As DataSet) As DataSet

        'LMC840のデータセットIN情報を設定
        Dim rtnDs As DataSet = SetDataSetSigmaInData(ds, New LMC840DS(), "LMC840IN")

        '検索結果取得
        Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMC840BLC(), "SigmaCsv", rtnDs))

        Return rtnDs

    End Function

    '(2013.01.25)埼玉BP対応 -- START --
    ''' <summary>
    ''' データ検索処理(名鉄CSV作成時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectMeitetuCsvメソッドに飛ぶ</remarks>
    Private Function SelectMeitetuCsvSaitama(ByVal ds As DataSet) As DataSet

        'Dim rdPrevDs As New RdPrevInfoDS
        'Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone
        'Dim rdPrevDt2 As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ''送り状
        'Dim rtnDs As DataSet = SetDataSetPrintInData(ds, New LMC794DS(), "LMC794IN")
        'rtnDs.Merge(New RdPrevInfoDS)
        ''検索結果取得
        'Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMC794BLC(), "MeitetuLabel", rtnDs))
        'rdPrevDt.Merge(rtnDs2.Tables(LMConst.RD))

        ''荷札
        'Dim rtnDs3 As DataSet = SetDataSetPrintInData(ds, New LMC789DS(), "LMC789IN")
        'rtnDs3.Merge(New RdPrevInfoDS)
        ''検索結果取得
        'Dim rtnDs4 As DataSet = (MyBase.CallBLC(New LMC789BLC(), "MeitetuTag", rtnDs3))
        'rdPrevDt2.Merge(rtnDs4.Tables(LMConst.RD))

        ''CSVを出す
        ''Dim rtnD5 As DataSet = SetDataSetCsvInData(ds, New LMC800DS(), "LMC850IN")
        ''検索結果取得
        'Dim rtnDs6 As DataSet = (MyBase.CallBLC(New LMC850BLC(), "MeitetuCsv", rtnD5))

        'rtnDs6.Merge(New RdPrevInfoDS)
        'rtnDs6.Tables(LMConst.RD).Merge(rdPrevDt)
        'rtnDs6.Tables(LMConst.RD).Merge(rdPrevDt2)

        'Return rtnDs6

        'LMC850のデータセットIN情報を設定
        Dim rtnDs As DataSet = SetDataSetCsvInData(ds, New LMC850DS(), "LMC850IN")

        '検索結果取得
        Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMC850BLC(), "MeitetuCsv", rtnDs))

        Return rtnDs2

    End Function
    '(2013.01.25)埼玉BP対応 --  END  --

    ''' <summary>
    ''' データ検索処理(新潟運輸CSV出力時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSLineCsvメソッドに飛ぶ</remarks>
    Private Function SelectNiigataUnyuCsv(ByVal ds As DataSet) As DataSet

        'データセットIN情報を設定
        Dim rtnDs As DataSet = SetDataSetNiigataUnyuCsvInData(ds, New LMC950DS(), "LMC950IN")

        '検索結果取得
        Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMC950BLC(), "NiigataUnyuCsv", rtnDs))

        Return rtnDs

    End Function

    ''' <summary>
    ''' データ検索処理(トールCSV出力時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSLineCsvメソッドに飛ぶ</remarks>
    Private Function SelectTollCsv(ByVal ds As DataSet) As DataSet

        ' データセットIN情報を設定
        Dim rtnDs As DataSet = SetDataSetCsvInData(ds, New LMC960DS(), "LMC960IN")

        ' 検索結果取得
        Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMC960BLC(), "TollCsv", rtnDs))

        Return rtnDs

    End Function

    ''' <summary>
    ''' データ検索処理(トールCSV出力時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSLineCsvメソッドに飛ぶ</remarks>
    Private Function SelectSagawaEHiden3Csv(ByVal ds As DataSet) As DataSet

        ' データセットIN情報を設定
        Dim rtnDs As DataSet = SetDataSetCsvInData(ds, New LMC903DS(), "LMC903IN")

        ' 検索結果取得
        Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMC903BLC(), "SagawaEHiden3Csv", rtnDs))

        Return rtnDs

    End Function

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
        dr.Item("SYS_TIME") = MyBase.GetSystemTime()
        dt.Rows.Add(dr)
        Return ds

    End Function
    'END YANAI 要望番号627　こぐまくん対応

    'START YANAI 要望番号773　報告書Excel対応
    ''' <summary>
    ''' データ検索処理(出荷報告Excel作成時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectHoukokuExcelメソッドに飛ぶ</remarks>
    Private Function SelectHoukokuExcel(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectHoukokuExcel", ds)

    End Function

    ''' <summary>
    ''' FFEM オフライン品 依頼書未出力件数検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectOutkaediDtlFjfOffMiPrintCount(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectOutkaediDtlFjfOffMiPrintCount", ds)

    End Function

    ''' <summary>
    ''' データ更新処理(出荷報告Excel作成時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateHoukokuExcel(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LMC010IN_HOUKOKU_EXCEL")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        Dim rtnResult As Boolean = False

        '更新処理
        For i As Integer = 0 To max

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '値のクリア
                inTbl = setDs.Tables("LMC010IN_HOUKOKU_EXCEL")
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(dt.Rows(i))

                '更新
                setDs = MyBase.CallBLC(Me._Blc, "UpdateHoukokuExcel", setDs)

                'エラーがあるかを判定
                rtnResult = Not MyBase.IsMessageExist()
                If rtnResult = True Then
                    'エラーが無ければCommit
                    MyBase.CommitTransaction(scope)
                Else
                    Return ds
                End If

            End Using

        Next

        Return ds

    End Function

    'ADD 2016/06/21 要望番号:2580
    ''' <summary>
    ''' データ更新処理(佐川送状番号更新時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkSagawaDenp_NO(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LMC010IN_OUTKA_DENP_NO")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        Dim rtnResult As Boolean = False


        For i As Integer = 0 To max

            '値のクリア
            inTbl = setDs.Tables("LMC010IN_OUTKA_DENP_NO")
            inTbl.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            '検索
            setDs = MyBase.CallBLC(Me._Blc, "ChkDenp_No", setDs)

            'エラーがあるかを判定
            rtnResult = MyBase.IsMessageExist()
            If rtnResult = True Then
                Return ds
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' データ更新処理(佐川送状番号更新時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSagawaDenp_NO(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LMC010IN_OUTKA_DENP_NO")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        Dim rtnResult As Boolean = False

        '更新処理
        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            For i As Integer = 0 To max

                '値のクリア
                inTbl = setDs.Tables("LMC010IN_OUTKA_DENP_NO")
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(dt.Rows(i))

                '更新
                setDs = MyBase.CallBLC(Me._Blc, "UpdateDenp_No", setDs)

                'エラーがあるかを判定
                rtnResult = MyBase.IsMessageExist()
                If rtnResult = True Then
                    Return ds
                End If

            Next

            'エラーが無ければCommit
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function
    'ADD 2016/06/21 要望番号:2580
    ''' <summary>
    ''' データ更新処理(ヤマト送状番号更新時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateYamatoDenp_NO(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LMC010IN_OUTKA_DENP_NO")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing

        Dim rtnResult As Boolean = False

        Dim setDt As DataTable = dt.Copy
        Dim drs As DataRow() = setDt.Select(String.Empty, "OUTKA_NO_L asc,DENP_NO asc")
        Dim setDr As DataRow = Nothing

        Dim intDENP_Cnt As Integer = 0
        Dim htDenpCnt As Hashtable = New Hashtable

        '並び替えた値を設定(出荷管理番号L,送状番号)
        Dim rowMax As Integer = setDt.Rows.Count - 1
        Dim colMax As Integer = setDt.Columns.Count - 1
        For i As Integer = 0 To rowMax
            '梱数変更用
            If htDenpCnt.ContainsKey(drs(i)("OUTKA_NO_L").ToString) = True Then
                intDENP_Cnt += 1
                htDenpCnt.Remove(drs(i)("OUTKA_NO_L").ToString)
            Else
                intDENP_Cnt = 1
            End If
            htDenpCnt.Add(drs(i)("OUTKA_NO_L").ToString, intDENP_Cnt)

            For j As Integer = 0 To colMax
                dt.Rows(i).Item(j) = drs(i).Item(j).ToString()
            Next
        Next

        For i As Integer = 0 To rowMax
            intDENP_Cnt = CInt(htDenpCnt(dt(i)("OUTKA_NO_L").ToString))
            dt(i)("OUTKA_PKG_NB") = intDENP_Cnt
        Next

        Dim wkOUTKA_NO_L As String = String.Empty

        '更新処理
        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            For i As Integer = 0 To max

                '値のクリア
                inTbl = setDs.Tables("LMC010IN_OUTKA_DENP_NO")
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(dt.Rows(i))

                '出荷管理番号ブレーク時のみ処理する
                If dt.Rows(i).Item("OUTKA_NO_L").Equals(wkOUTKA_NO_L) = False Then

                    wkOUTKA_NO_L = dt.Rows(i).Item("OUTKA_NO_L").ToString

                    '更新 C_OUTKA_L（送状番号/送り状発行フラグ）
                    setDs = MyBase.CallBLC(Me._Blc, "UpdateDenp_No", setDs)

                    '同一出荷管理場号Lを削除(C_OUTKA_DENP_NO)
                    setDs = MyBase.CallBLC(Me._Blc, "DeleteOutkaDenp_No", setDs)

                End If

                '全てをInsert(C_OUTKA_DENP_NO)
                setDs = MyBase.CallBLC(Me._Blc, "InsertOutkaDenp_No", setDs)

                'エラーがあるかを判定
                rtnResult = MyBase.IsMessageExist()
                If rtnResult = True Then
                    Return ds
                End If

            Next

            'エラーが無ければCommit
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' データ更新処理（SBS再保管出荷実績取込処理）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function UpdateSBSSaihokanOutkaImport(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LMC010_SBS_SAIHOKAN")

        '取込データの内容チェック
        Dim errFlg As Boolean = False

        For i As Integer = 0 To dt.Rows.Count - 1
            Dim dr As DataRow = dt.Rows(i)

            '出荷、運送会社の存在有無を取得
            ds.Tables("LMC010_SBS_SAIHOKAN_ROW").Rows.Clear()
            Dim newRow As DataRow = ds.Tables("LMC010_SBS_SAIHOKAN_ROW").NewRow()
            newRow("NRS_BR_CD") = dr("NRS_BR_CD").ToString()
            newRow("OUTKA_NO_L") = dr("OUTKA_NO_L").ToString()
            newRow("UNSO_CD") = dr("UNSO_CD").ToString()
            newRow("UNSO_BR_CD") = dr("UNSO_BR_CD").ToString()
            ds.Tables("LMC010_SBS_SAIHOKAN_ROW").Rows.Add(newRow)

            ds = MyBase.CallBLC(Me._Blc, "SBSSaihokanOutkaImportChk", ds)

            '出荷の存在チェック
            If Convert.ToInt32(ds.Tables("LMC010_SBS_SAIHOKAN_ROW").Rows(0).Item("CNT_OUTKA_L").ToString) = 0 Then
                '[出荷]に該当データが存在しません。
                MyBase.SetMessageStore("00", "E078", {"出荷"}, dr("ROW_NO").ToString, "", dr("OUTKA_NO_L").ToString)
                errFlg = True
            End If

            '運送会社の存在チェック
            If Convert.ToInt32(ds.Tables("LMC010_SBS_SAIHOKAN_ROW").Rows(0).Item("CNT_UNSOCO").ToString) = 0 Then
                '[運送会社]に該当データが存在しません。
                MyBase.SetMessageStore("00", "E078", {"運送会社"}, dr("ROW_NO").ToString, "", String.Concat(dr("UNSO_CD").ToString, "-", dr("UNSO_BR_CD").ToString))
                errFlg = True
            End If

            '出荷梱包個数の桁数チェック
            If dr("OUTKA_PKG_NB").ToString.Length > 10 Then
                '[出荷梱包個数]は桁数が超えています。[(最大10桁)]
                MyBase.SetMessageStore("00", "E474", {"出荷梱包個数", "(最大10桁)"}, dr("ROW_NO").ToString, "", dr("OUTKA_PKG_NB").ToString)
                errFlg = True
            End If

            '出荷梱包個数の数値チェック
            If Not IsNumeric(dr("OUTKA_PKG_NB").ToString) Then
                '[出荷梱包個数]は数値を入力してください。
                MyBase.SetMessageStore("00", "E005", {"出荷梱包個数"}, dr("ROW_NO").ToString, "", dr("OUTKA_PKG_NB").ToString)
                errFlg = True
            End If

            '送状番号の桁数チェック
            If dr("DENP_NO").ToString.Length > 20 Then
                '[送状番号]は桁数が超えています。[(最大20桁)]
                MyBase.SetMessageStore("00", "E474", {"送状番号", "(最大20桁)"}, dr("ROW_NO").ToString, "", dr("DENP_NO").ToString)
                errFlg = True
            End If
        Next

        '取込データにエラーがあれば抜ける
        If errFlg Then
            Return ds
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim dr As DataRow = dt.Rows(i)

                ds.Tables("LMC010_SBS_SAIHOKAN_ROW").Rows.Clear()
                Dim newRow As DataRow = ds.Tables("LMC010_SBS_SAIHOKAN_ROW").NewRow()
                newRow("NRS_BR_CD") = dr("NRS_BR_CD").ToString()
                newRow("OUTKA_NO_L") = dr("OUTKA_NO_L").ToString()
                newRow("UNSO_CD") = dr("UNSO_CD").ToString()
                newRow("UNSO_BR_CD") = dr("UNSO_BR_CD").ToString()
                newRow("DENP_NO") = dr("DENP_NO").ToString()
                newRow("OUTKA_PKG_NB") = dr("OUTKA_PKG_NB").ToString()
                ds.Tables("LMC010_SBS_SAIHOKAN_ROW").Rows.Add(newRow)

                '出荷Lの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateSBSSaihokanOutkaL", ds)

                'エラーがあるかを判定
                Dim rtnResult As Boolean = MyBase.IsMessageExist()
                If rtnResult = True Then
                    Return ds
                End If

                '運送Lの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateSBSSaihokanUnsoL", ds)

                'エラーがあるかを判定
                rtnResult = MyBase.IsMessageExist()
                If rtnResult = True Then
                    Return ds
                End If
            Next

            'エラーが無ければCommit
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    'END YANAI 要望番号773　報告書Excel対応

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

    '社内入荷データ作成 terakawa Start
    ''' <summary>
    ''' 社内入荷データ作成先検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ComboData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(_Blc, "ComboData", ds)

    End Function
    '社内入荷データ作成 terakawa End

#Region "名鉄固有処理"

    ''' <summary>
    ''' 名鉄帳票印刷(荷札+送状:UpdatePrintData亜種)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="isGrouping"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintMeitetuReport(ByVal ds As DataSet _
                                      , ByVal isGrouping As Boolean) As DataSet
        'add 2017/07/18 
        Dim baraFLG As String = String.Empty
        If ds.Tables("LMC010IN_OUTKA_L") Is Nothing = False Then
            baraFLG = ds.Tables("LMC010IN_OUTKA_L").Rows(0).Item("MEITETU_PRINT_FLG").ToString.Trim
        End If


        Dim updateData As DataSet = ds.Copy()

        ' 送り状用パラメータ設定
        Dim inputDS As DataSet = SetDataSetPrintInData(ds, New LMC794DS(), "LMC794IN")

        ' 検索結果取得
        Dim selectData As DataSet = Nothing
        If (isGrouping = True) Then
            ' まとめあり
            selectData = MyBase.CallBLC(New LMC794BLC(), "MeitetuLabelWithGrouping", inputDS)
        Else
            ' まとめなし
            selectData = MyBase.CallBLC(New LMC794BLC(), "MeitetuLabel", inputDS)
        End If

        If (MyBase.IsMessageStoreExist() = True OrElse _
            MyBase.IsMessageExist() = True) Then
            Return ds
        End If

        Dim invoicePrintInData As DataSet = selectData.Clone()
        Dim tagPrintInData As DataSet = Nothing

        '更新用データ取得
        updateData.Merge(selectData.Tables("LMC794IN_UPDATE_UNSO_L"))

        ' プレビューデータ格納準備
        ds.Merge(New RdPrevInfoDS)
        Dim prevTempDs As New DataSet()
        prevTempDs.Merge(New RdPrevInfoDS)


        Dim keyNumber As String = String.Empty
        For Each selectRow As DataRow In selectData.Tables("LMC794OUT").Rows

            If (isGrouping) Then
                keyNumber = selectRow("AUTO_DENP_NO").ToString()
            Else
                keyNumber = selectRow("OUTKA_NO_L").ToString()
            End If

#If False Then
            ' お問い合わせ番号更新
            Me.UpdateAutoDenpNo(updateData, autoDenpNo)
            If (MyBase.IsMessageStoreExist() = True OrElse _
                MyBase.IsMessageExist() = True) Then
                Return ds
            End If

            ' 印刷フラグ更新(ToDo:UpdatePrintDataへ統合を検討)
            Me.UpdatePrintDataMeitetu(updateData)

            If (MyBase.IsMessageStoreExist() = True OrElse _
                MyBase.IsMessageExist() = True) Then
                Return ds
            End If
#Else
            Me.UpdatePrintDataMeitetu(updateData, keyNumber, isGrouping)
            If (MyBase.IsMessageStoreExist() = True OrElse _
                MyBase.IsMessageExist() = True) Then
                Return ds
            End If

#End If
            ' 送状用入力データ
            invoicePrintInData.Clear()
            invoicePrintInData.Tables("LMC794OUT").ImportRow(selectRow)

            ' 荷札用入力データ
            tagPrintInData = Me.SetDataSetLMC789InData(invoicePrintInData, "LMC794OUT")

            'バラ荷札以外
            If ("4").Equals(baraFLG.ToString) = False Then
                ' 送状印刷
                invoicePrintInData = MyBase.CallBLC(New LMC794BLC(), "DoPrint", invoicePrintInData)
                prevTempDs.Tables(LMConst.RD).Merge(invoicePrintInData.Tables(LMConst.RD))

            End If

            'バラ送状以外
            If ("3").Equals(baraFLG.ToString) = False Then
                ' 荷札印刷
                tagPrintInData = MyBase.CallBLC(New LMC789BLC(), "DoPrint", tagPrintInData)
                prevTempDs.Tables(LMConst.RD).Merge(tagPrintInData.Tables(LMConst.RD))

            End If

        Next

        ' プレビューデータ格納
        ds.Tables(LMConst.RD).Merge(prevTempDs.Tables(LMConst.RD))

        Return ds

    End Function

    ''' <summary>
    ''' 名鉄帳票印刷(荷札+送状:UpdatePrintData亜種) LMC010BLFよりコピーし修正
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintMeitetuReport2(ByVal ds As DataSet _
                                             , ByVal isGrouping As Boolean) As DataSet

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

    '2015.10.02 tunehira add
    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetPrintInData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
#If False Then
        Dim max As Integer = ds.Tables("LMC010IN_CSV").Rows.Count - 1

        For i As Integer = 0 To max
            dr.Item("NRS_BR_CD") = ds.Tables("LMC010IN_CSV").Rows(i).Item("NRS_BR_CD").ToString
            dr.Item("OUTKA_NO_L") = ds.Tables("LMC010IN_CSV").Rows(i).Item("OUTKA_NO_L").ToString
            dr.Item("ROW_NO") = ds.Tables("LMC010IN_CSV").Rows(i).Item("ROW_NO").ToString
            dr.Item("SYS_DATE") = ds.Tables("LMC010IN_CSV").Rows(i).Item("SYS_DATE").ToString
            dr.Item("SYS_TIME") = ds.Tables("LMC010IN_CSV").Rows(i).Item("SYS_TIME").ToString

            dt.Rows.Add(dr)
            dr = dt.NewRow()
        Next
#Else
        Dim TABLE_NM_IN_UPDATE As String = "LMC010IN_UPDATE"

        For i As Integer = 0 To ds.Tables(TABLE_NM_IN_UPDATE).Rows.Count - 1

            dr.Item("NRS_BR_CD") = ds.Tables(TABLE_NM_IN_UPDATE).Rows(i).Item("NRS_BR_CD").ToString
            dr.Item("OUTKA_NO_L") = ds.Tables(TABLE_NM_IN_UPDATE).Rows(i).Item("OUTKA_NO_L").ToString
            dr.Item("ROW_NO") = ds.Tables(TABLE_NM_IN_UPDATE).Rows(i).Item("RECORD_NO").ToString
            dr.Item("SYS_DATE") = ds.Tables(TABLE_NM_IN_UPDATE).Rows(i).Item("SYS_UPD_DATE").ToString
            dr.Item("SYS_TIME") = ds.Tables(TABLE_NM_IN_UPDATE).Rows(i).Item("SYS_UPD_TIME").ToString
            dr.Item("UNSO_SYS_UPD_DATE") = ds.Tables(TABLE_NM_IN_UPDATE).Rows(i).Item("UNSO_SYS_UPD_DATE").ToString
            dr.Item("UNSO_SYS_UPD_TIME") = ds.Tables(TABLE_NM_IN_UPDATE).Rows(i).Item("UNSO_SYS_UPD_TIME").ToString

            dt.Rows.Add(dr)
            dr = dt.NewRow()
        Next

#End If
        Return inDs

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
    ''' 
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintMeitetuReport(ByVal ds As DataSet) As DataSet
        Return Me.PrintMeitetuReport(ds, False)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintMeitetuReport2(ByVal ds As DataSet) As DataSet
        Return Me.PrintMeitetuReport2(ds, False)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintMeitetuReportWithGrouping(ByVal ds As DataSet) As DataSet
        Return Me.PrintMeitetuReport(ds, True)
    End Function

    Private Function UpdatePrintDataMeitetu(ByVal ds As DataSet _
                                          , ByVal keyNumber As String _
                                          , ByVal isGrouping As Boolean) As DataSet


        '更新用入力データテーブル名
        'Dim tableNm As String = "LMC010IN_UPDATE"
        Dim outKaInTableName As String = "LMC010IN_OUTKA_L"
        Dim unsoInTableName As String = "LMC794IN_UPDATE_UNSO_L"

        Dim inputUnsoTable As DataTable = ds.Tables(unsoInTableName)
        Dim inputOutkaTable As DataTable = ds.Tables(outKaInTableName)

        Dim updateData As DataSet = ds.Clone()
        Dim updateUnsoInTable As DataTable = updateData.Tables(unsoInTableName)
        Dim updateOutkaInTable As DataTable = updateData.Tables(outKaInTableName)

        ' 更新対象の出荷管理番号
        Dim outKaNoLList As New List(Of Object)()

        'メッセージクリア
        MyBase.SetMessage(Nothing)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            outKaNoLList.Clear()

            If (isGrouping) Then

                Dim updateUnsoRows As IEnumerable(Of DataRow) = _
                    From row In inputUnsoTable.AsEnumerable() _
                    Where row.Item("AUTO_DENP_NO").Equals(keyNumber)
                    Select row

                For Each updateRow As DataRow In updateUnsoRows

                    ' 更新対象の出荷管理番号格納
                    outKaNoLList.Add(updateRow("OUTKA_NO_L").ToString())

                    '初期化
                    updateData.Clear()

                    '条件の設定
                    updateUnsoInTable.ImportRow(updateRow)
                    ds = MyBase.CallBLC(New LMC794BLC(), "UpdateUnsoL", updateData)

                    If (MyBase.IsMessageStoreExist() OrElse _
                        MyBase.IsMessageExist()) Then

                        Return ds
                    End If

                Next
            Else
                outKaNoLList.Add(keyNumber)
            End If

            ' 出荷(大)テーブル更新
            For Each outKaNoL As String In outKaNoLList

                Dim updateOutKaRows As IEnumerable(Of DataRow) = _
                    From row In inputOutkaTable.AsEnumerable() _
                    Where row.Item("OUTKA_NO_L").Equals(outKaNoL)
                    Select row

                For Each updateRow As DataRow In updateOutKaRows

                    '自営業以外、スルー
                    If updateRow("USER_BR_CD").ToString().Equals(updateRow("NRS_BR_CD").ToString()) = False Then
                        Continue For
                    End If

                    '初期化
                    updateData.Clear()

                    '条件の設定
                    updateOutkaInTable.ImportRow(updateRow)

                    ds = MyBase.CallBLC(Me._Blc, "UpdateOutkaLPrintMeitetu", updateData)

                    If (MyBase.IsMessageStoreExist() OrElse _
                        MyBase.IsMessageExist()) Then

                        Return ds
                    End If


                Next
            Next


            'エラーがあるかを判定
            If (MyBase.IsMessageStoreExist() = False AndAlso _
                MyBase.IsMessageExist() = False) Then
                MyBase.CommitTransaction(scope)
            End If

        End Using

        Return ds

    End Function

#If False Then
    '20150106 名鉄対応 tsunehria add start
    ''' <summary>
    ''' データ検索処理(名鉄CSV作成時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectMeitetuCsvメソッドに飛ぶ</remarks>
    Private Function SelectMeitetuCsvGunma(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone
        Dim rdPrevDt2 As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        '送り状
        Dim rtnDs As DataSet = SetDataSetPrintInData(ds, New LMC794DS(), "LMC794IN")
        rtnDs.Merge(New RdPrevInfoDS)
        '検索結果取得
        Dim rtnDs2 As DataSet = (MyBase.CallBLC(New LMC794BLC(), "MeitetuLabel", rtnDs))

        '荷札
        Dim rtnDs3 As DataSet = SetDataSetPrintInData(ds, New LMC789DS(), "LMC789IN")
        rtnDs3.Merge(New RdPrevInfoDS)
        '検索結果取得
        Dim rtnDs4 As DataSet = (MyBase.CallBLC(New LMC789BLC(), "MeitetuTag", rtnDs3))
        rdPrevDt.Merge(rtnDs4.Tables(LMConst.RD))

        rtnDs2.Merge(New RdPrevInfoDS)
        rtnDs2.Tables(LMConst.RD).Merge(rdPrevDt)

        Return rtnDs2

    End Function
    '20150106 名鉄対応 tsunehria add end
#End If
#End Region

#Region "纏めピッキングWK"

    'START Kurihara WIT対応

    ''' <summary>
    ''' 対象の出荷管理番号Lを持つワークデータを検索します。
    ''' </summary>
    ''' <param name="ds">検索条件DataSet</param>
    ''' <returns>検索結果DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectMtmPickList(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(_Blc, "SelectMtmPickList", ds)

    End Function

    ''' <summary>
    ''' 纏めピッキングWKへの登録・削除判断を行うためのデータを抽出します。
    ''' </summary>
    ''' <param name="ds">検索条件DataSet</param>
    ''' <returns>検索結果DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectEntryJudge(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(_Blc, "SelectEntryJudge", ds)

    End Function

    ''' <summary>
    ''' まとめピッキングワークからレコードを削除します。
    ''' </summary>
    ''' <param name="ds">検索条件DataSet</param>
    ''' <remarks></remarks>
    Private Sub DeleteCTotalPicWk(ByVal ds As DataSet)

        '削除実行
        MyBase.CallBLC(_Blc, "DeleteCTotalPicWk", ds)

    End Sub

    ''' <summary>
    ''' まとめピッキングワークにレコードを追加します。
    ''' </summary>
    ''' <param name="ds">検索条件DataSet</param>
    ''' <remarks></remarks>
    Private Sub InsertCTotalPicWk(ByVal ds As DataSet)

        '追加実行
        MyBase.CallBLC(_Blc, "InsertCTotalPicWk", ds)

    End Sub

    'END Kurihara WIT対応

#End Region

#End Region

#Region "設定処理"

    ''' <summary>
    '''  運送会社変更時、マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Sub UnsoMCheck(ByVal ds As DataSet)

        '存在チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckExistUnsoM", ds)

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = False Then
            'データの新規登録
            ds = MyBase.CallBLC(Me._Blc, "CheckExistUnsoM", ds)
        End If

    End Sub

    ''' <summary>
    ''' 運送会社変更処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function HenkoData(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim tableNm As String = "LMC010IN_UPDATE"
        Dim dt As DataTable = ds.Tables(tableNm)

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNm)

        Dim rtnResult As Boolean = False


        Dim max As Integer = dt.Rows.Count - 1


        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
        '運送会社コードの支払タリフ、支払割増タリフを取得し、LMC010IN_UPDATEの支払タリフ、支払割増タリフにセットする
        Dim sShiharaiTariffCd As String = String.Empty
        Dim sShiharaiETariffCd As String = String.Empty
        Dim sAutoDenpKbn As String = String.Empty

        Dim autoDenpNo As String = String.Empty

        If max >= 0 Then    '更新すべきデータが存在する場合のみ
            Dim setDs2 As DataSet = ds.Copy()
            setDs2 = MyBase.CallBLC(Me._Blc, "GetTariffCdFromUnsoCd", setDs)
            sShiharaiTariffCd = setDs2.Tables("LMC010IN_UPDATE").Rows(0).Item("SHIHARAI_TARIFF_CD").ToString
            '要望番号:2408 2015.09.17 追加START
            sAutoDenpKbn = setDs2.Tables("LMC010IN_UPDATE").Rows(0).Item("AUTO_DENP_KBN").ToString
            '要望番号:2408 2015.09.17 追加END

        End If
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End

        For i As Integer = 0 To max

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '初期化
                inTbl.Clear()
                ds.Tables("LMC010OUT_CHKHAITA").Rows.Clear()

                '運送会社コードの支払タリフ、支払割増タリフを取得し、LMC010IN_UPDATEの支払タリフ、支払割増タリフにセットする
                dt.Rows(i).Item("SHIHARAI_TARIFF_CD") = sShiharaiTariffCd
                dt.Rows(i).Item("SHIHARAI_ETARIFF_CD") = sShiharaiETariffCd
                '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End

                '要望番号:2408 2015.09.17 追加START

#If False Then ' 西濃自動送り状番号対応 20160704 changed inoue
                dt.Rows(i).Item("AUTO_DENP_KBN") = sAutoDenpKbn
                Dim autoDenpNo As String = String.Empty
                Dim intMeitestuNo As Integer = 0
                If String.IsNullOrEmpty(sAutoDenpKbn) = False Then
                    autoDenpNo = Me.GetMeiTetsuDenpNoL(ds)
                    'チェックデジットの組込み
                    intMeitestuNo = Convert.ToInt32(autoDenpNo) Mod 7
                    autoDenpNo = String.Concat(autoDenpNo, Convert.ToString(intMeitestuNo))
                    dt.Rows(i).Item("AUTO_DENP_NO") = autoDenpNo
                End If
#Else
                If (dt.Rows(i).Item("AUTO_DENP_KBN").Equals(sAutoDenpKbn) = False) Then
                    ' 自動送り状番号区分が一括変更前と異なる場合、自動送り状番号をクリアする。
                    ' 自動送り状番号の払い出しは、BLC側で実施
                    dt.Rows(i).Item("AUTO_DENP_NO") = String.Empty
                End If
                dt.Rows(i).Item("AUTO_DENP_KBN") = sAutoDenpKbn
#End If

                '要望番号:2408 2015.09.17 追加END

                '条件の設定
                inTbl.ImportRow(dt.Rows(i))

                '2018/01/15 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add start
                Dim nrsBrCd As String = dt.Rows(i).Item("NRS_BR_CD").ToString
                Dim unsoCoCd As String = dt.Rows(i).Item("UNSOCO_CD").ToString
                Dim unsocoBrCd As String = dt.Rows(i).Item("UNSOCO_BR_CD").ToString
                Call SetPrefixOfAutoDenpNo(setDs, nrsBrCd, unsoCoCd, unsocoBrCd)
                '2018/01/15 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add end

                '運送L/出荷Lの更新処理を行う
                ds = MyBase.CallBLC(Me._Blc, "HenkoData", setDs)

                'タブレット運送情報更新
                Me.UpdateTabletUnsoData(setDs)

                If ds.Tables("LMC010OUT_CHKHAITA").Rows(0).Item("CHK_FLG").Equals(LMConst.FLG.OFF) Then

                    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
                    ''トランザクション終了
                    'MyBase.CommitTransaction(scope)

                    '--------------------------------------------------------------------------------------------------
                    ' LMF810をCallして支払運賃を登録する
                    '--------------------------------------------------------------------------------------------------
                    'データセット設定
                    Dim shiharaiDs As DataSet = Me.SetShiharaiInDataSet2(setDs, inTbl)

                    'BLCアクセス
                    shiharaiDs = Me.ShiharaiBlcAccess(shiharaiDs, "CreateUnchinData")

                    'LMF810の戻り値判定
                    Dim rtnResultDt2 As DataTable = shiharaiDs.Tables("LMF810RESULT")
                    Dim rtnResultDr2 As DataRow = rtnResultDt2.Rows(0)

                    If ("00").Equals(rtnResultDr2.Item("STATUS").ToString) = True OrElse _
                       ("05").Equals(rtnResultDr2.Item("STATUS").ToString) = True OrElse _
                       ("30").Equals(rtnResultDr2.Item("STATUS").ToString) = True Then
                        '正常の場合は保存処理

                        'BLCアクセス(支払運賃削除)
                        Dim rtnDs2 As DataSet = Nothing
                        rtnDs2 = MyBase.CallBLC(Me._Blc, "DeleteShiharaiData2", setDs)

                        'BLCアクセス(支払運賃作成)
                        shiharaiDs.Tables.Add(ds.Tables("LMC010OUT_SHIHARAI").Copy)
                        shiharaiDs = Me.CallBLC(Me._Blc, "InsertShiharaiData", shiharaiDs)

                    End If

                    'エラーがあるかを判定
                    rtnResult = Not MyBase.IsMessageExist()

                    If rtnResult = True Then

                        'トランザクション終了
                        MyBase.CommitTransaction(scope)
                    Else
                        'エラーの場合はエラー処理
                        MyBase.SetMessage(rtnResultDr2.Item("ERROR_CD").ToString, New String() {rtnResultDr2.Item("YOBI1").ToString})
                        rtnResult = False
                    End If
                    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  

                End If

            End Using

        Next

        Return ds

    End Function

    '2018/01/15 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add start
    ''' <summary>
    ''' 自動送状接頭番号設定処理
    ''' </summary>
    ''' <param name="ds">該当データセット</param>
    ''' <param name="outokaDrM">出荷Mデータテーブル該当行</param>
    ''' <remarks></remarks>
    Private Overloads Function SetPrefixOfAutoDenpNo(ByRef ds As DataSet, _
                                                     ByRef outokaDrM As DataRow) As Boolean

        Dim dtUnsoL As DataTable = ds.Tables("LMC010OUT_UNSO_L")
        Dim dtUnsoPre As DataTable = ds.Tables("LMC010_PRE_UNSO")
        Dim drUnsoLs() As DataRow

        Dim nrsBrCd As String = outokaDrM("NRS_BR_CD").ToString
        Dim outkaNoL As String = outokaDrM("OUTKA_NO_L").ToString
        Dim unsoNoL As String = String.Empty
        Dim sysUpdDate As String = String.Empty
        Dim sysUpdTime As String = String.Empty

        Dim unsoCd As String = String.Empty
        Dim unsoBrCd As String = String.Empty

        '出荷管理Mの情報から運送情報の取得を行う
        drUnsoLs = dtUnsoL.Select(String.Concat("NRS_BR_CD ='", nrsBrCd, "' AND ", _
                                                "OUTKA_NO_L ='", outkaNoL, "'"))

        '運送情報（データテーブル）より情報を取得する
        unsoNoL = drUnsoLs(0)("UNSO_NO_L").ToString
        sysUpdDate = drUnsoLs(0)("SYS_UPD_DATE").ToString
        sysUpdTime = drUnsoLs(0)("SYS_UPD_TIME").ToString

        '運送情報（データテーブル）より取得した情報より、運送情報の運送会社コード、運送会社支店コードを取得する
        dtUnsoPre.Clear()
        Dim drUnsoPre As DataRow = dtUnsoPre.NewRow
        drUnsoPre("NRS_BR_CD") = nrsBrCd
        drUnsoPre("UNSO_NO_L") = unsoNoL
        drUnsoPre("SYS_UPD_DATE") = sysUpdDate
        drUnsoPre("SYS_UPD_TIME") = sysUpdTime
        dtUnsoPre.Rows.Add(drUnsoPre)
        ds = MyBase.CallBLC(Me._Blc, "SelectTargetUnsoPre", ds)
        If dtUnsoPre.Rows.Count = 0 Then
            Return False
        End If

        unsoCd = dtUnsoPre(0)("UNSO_CD").ToString
        unsoBrCd = dtUnsoPre(0)("UNSO_BR_CD").ToString

        '取得した営業所コード、運送会社コード、運送会社支店コード(00)で該当する
        '自動送状接頭番号を自動送状接頭番号テーブルに格納する。
        Call SetPrefixOfAutoDenpNo(ds, nrsBrCd, unsoCd, unsoBrCd)

        Return True

    End Function

    ''' <summary>
    ''' 自動送状接頭番号設定処理
    ''' </summary>
    ''' <param name="ds">該当データセット</param>
    ''' <param name="nrsBrCd">該当営業所コード</param>
    ''' <param name="unsoCoCd">該当運送会社コード</param>
    ''' <param name="unsocoBrCd">該当運送会社支店コード</param>
    ''' <remarks></remarks>
    Private Overloads Sub SetPrefixOfAutoDenpNo(ByRef ds As DataSet, _
                                                ByVal nrsBrCd As String, _
                                                ByVal unsoCoCd As String, _
                                                ByVal unsocoBrCd As String)

        Dim inDt As DataTable = ds.Tables("LMC010_PRE_NO_OF_INVOICE_NO")
        Dim outDt As DataTable = ds.Tables("LMC010_OKURIJYO_WK")
        Dim inDrs() As DataRow = Nothing
        Dim inDr As DataRow = Nothing

        outDt.Clear()

        '自動送状接頭番号格納テーブルより営業所コード、運送会社コード、運送会社支店コードで
        '情報を抽出する。
        inDrs = inDt.Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ", _
                                          "UNSO_CD ='", unsoCoCd, "' AND ", _
                                          "UNSO_BR_CD = '" & unsocoBrCd, "'"))

        '抽出した情報分、自動送状接頭番号テーブルに格納する。
        For Each inDr In inDrs
            Dim dr As DataRow = ds.Tables("LMC010_OKURIJYO_WK").NewRow
            dr("OKURIJYO_HEAD") = inDr("PREFIX_NO")
            outDt.Rows.Add(dr)
        Next

    End Sub
    '2018/01/15 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add end

    ''' <summary>
    ''' 印刷時、更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdatePrintData(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "LMC010IN_OUTKA_L"
        Dim setDs As DataSet = ds.Copy
        Dim setDt As DataTable = setDs.Tables(tableNm)
        Dim dt As DataTable = ds.Tables(tableNm)
        Dim max As Integer = dt.Rows.Count - 1
        'Dim InDs As DataSet = ds.Copy 'INデータマージ用データセット
        'Dim InDt As DataTable = InDs.Tables(tableNm)
        'InDt.Clear()

        'レコードがない場合、スルー
        If max < 0 Then
            Return ds
        End If

        Dim printType As String = dt.Rows(0).Item("PRINT_KB").ToString()

        'プレビュー用DataTable
        If ds.Tables(LMConst.RD) Is Nothing = True Then

            ds.Tables.Add(New RdPrevInfoDS.PREV_INFODataTable())

        End If
        Dim rtnDt As DataTable = ds.Tables(LMConst.RD)
        rtnDt.Clear()

        Dim setRptDt As DataTable = Nothing
        Dim cnt As Integer = 0

        If "05".Equals(printType.ToString()) = False Then

            If "12".Equals(printType.ToString()) = False AndAlso "13".Equals(printType.ToString()) = False Then

                '通常の印刷  --(INデータマージなし)
                For i As Integer = 0 To max

                    '更新情報の設定
                    setDt.Clear()
                    setDt.ImportRow(dt.Rows(i))

                    '更新処理(エラーの場合、次レコードへ)
                    If Me.UpdatePrintDataAction(setDs) = False Then
                        Continue For
                    End If

                    '印刷処理
                    setRptDt = Me.DoPrint(setDs, printType).Tables(LMConst.RD)

                    'プレビュー情報を設定
                    If setRptDt Is Nothing = False Then
                        cnt = setRptDt.Rows.Count - 1
                        For j As Integer = 0 To cnt
                            rtnDt.ImportRow(setRptDt.Rows(j))
                        Next
                    End If
                Next

            ElseIf "13".Equals(printType.ToString()) = True Then '2013/01/24 チェックボックスのみ集約
                '通常の印刷  --(INデータマージなし)
                Dim dnpGrp As Integer = -1
                '要望番号:1883 yamanaka 2013.03.05 Start
                Dim upDs As DataSet = ds.Copy
                Dim upDt As DataTable = upDs.Tables(tableNm)
                Dim tempDt As DataTable = ds.Copy.Copy.Tables(tableNm)
                '要望番号:1883 yamanaka 2013.03.05 End

#If True Then   'ADD 2018/10/18 依頼番号 : 002601   【LMS】日医工_まとめ送状(選択)_送り状番号更新 ＋ オーダー番号最小の個数のみ送り状表示(千葉BC柴田) 

                Dim upDtSelect As DataTable = upDs.Tables("LMC010IN_OUTKA_L_SELECT")
                upDtSelect.Clear()

                If upDt.Rows.Count > 0 Then

                    For i As Integer = 0 To upDt.Rows.Count - 1

                        upDtSelect.ImportRow(setDt.Rows(i))

                    Next

                End If

#End If

                For i As Integer = 0 To max

                    'If dnpGrp > Integer.Parse(ds.Tables(tableNm).Rows(i).Item("MATOME_DENP_GRP").ToString()) Then
                    If dnpGrp >= Integer.Parse(ds.Tables(tableNm).Rows(i).Item("MATOME_DENP_GRP").ToString()) Then
                        Continue For
                    Else
                        dnpGrp = Integer.Parse(ds.Tables(tableNm).Rows(i).Item("MATOME_DENP_GRP").ToString())
                    End If

                    '更新情報の設定 
                    setDt.Clear()
                    For Each row As DataRow In ds.Tables(tableNm).Select(String.Concat("MATOME_DENP_GRP = '", ds.Tables(tableNm).Rows(i).Item("MATOME_DENP_GRP").ToString(), "'"), "MATOME_DENP_GRP")
                        setDt.ImportRow(row)
                    Next

                    '更新処理(エラーの場合、次レコードへ)
                    '要望番号:1883 yamanaka 2013.03.05 Start
                    tempDt.Clear()
                    For j As Integer = 0 To setDt.Rows.Count - 1

                        upDt.Clear()
                        upDt.ImportRow(setDt.Rows(j))

                        If Me.UpdatePrintDataAction(upDs) = False Then
                            Continue For
                        End If

                        tempDt.ImportRow(upDt.Rows(0))

                    Next

                    '全部エラーの場合、抜ける
                    If tempDt.Rows.Count = 0 Then
                        Continue For
                    End If

                    setDt.Clear()
                    For inCnt As Integer = 0 To tempDt.Rows.Count - 1
                        setDt.ImportRow(tempDt.Rows(inCnt))
                    Next

                    'If Me.UpdatePrintDataAction(setDs, i) = False Then
                    'If Me.UpdatePrintDataAction(setDs) = False Then
                    '    Continue For
                    'End If
                    '要望番号:1883 yamanaka 2013.03.05 End

                    '印刷処理
                    setRptDt = Me.DoPrint(setDs, printType).Tables(LMConst.RD)

                    'プレビュー情報を設定
                    If setRptDt Is Nothing = False Then
                        cnt = setRptDt.Rows.Count - 1
                        For j As Integer = 0 To cnt
                            rtnDt.ImportRow(setRptDt.Rows(j))
                        Next
                    End If

                    'dnpGrp = Integer.Parse(ds.Tables(tableNm).Rows(i).Item("MATOME_DENP_GRP").ToString())
                Next

            Else
                '通常の印刷  --(INデータマージあり)
                Dim tempDt As DataTable = setDs.Copy.Copy.Tables(tableNm)
                tempDt.Clear()

                For i As Integer = 0 To max

                    '更新情報の設定
                    setDt.Clear()
                    setDt.ImportRow(dt.Rows(i))

                    '更新処理(エラーの場合、次レコードへ)
                    'If Me.UpdatePrintDataAction(setDs) = False Then
                    If Me.UpdatePrintDataAction(setDs) = False Then
                        Continue For
                    End If
                    tempDt.ImportRow(dt.Rows(i))
                Next


                '成功したRowを入れる。
                setDt.Clear()
                '全部エラーの場合、抜ける
                If tempDt.Rows.Count = 0 Then
                    Return ds
                End If

                For j As Integer = 0 To tempDt.Rows.Count - 1
                    setDt.ImportRow(tempDt.Rows(j))
                Next

                '印刷処理
                setRptDt = Me.DoPrint(setDs, printType).Tables(LMConst.RD)

                'プレビュー情報を設定
                If setRptDt Is Nothing = False Then
                    cnt = setRptDt.Rows.Count - 1
                    For j As Integer = 0 To cnt
                        rtnDt.ImportRow(setRptDt.Rows(j))
                    Next
                End If
            End If

        Else
            Dim tempDt As DataTable = setDs.Copy.Copy.Tables(tableNm)
            tempDt.Clear()
            'ピッキングリストの場合
            For i As Integer = 0 To max
                '更新情報の設定
                setDt.Clear()
                setDt.ImportRow(dt.Rows(i))

                '更新処理(エラーの場合、次レコードへ)
                If Me.UpdatePrintDataAction(setDs) = True Then
                    tempDt.ImportRow(dt.Rows(i))
                End If
            Next

            '成功したRowを入れる。
            setDt.Clear()
            '全部エラーの場合、抜ける
            If tempDt.Rows.Count = 0 Then
                Return ds
            End If
            For j As Integer = 0 To tempDt.Rows.Count - 1
                setDt.ImportRow(tempDt.Rows(j))
            Next

            'For i As Integer = 0 To max
            '    Dim targetRow As DataRow = dt.Rows(i)
            '    '更新情報の設定
            '    setDt.Clear()
            '    setDt.ImportRow(targetRow)

            '    '更新処理(エラーの場合、次レコードへ)
            '    If Me.UpdatePrintDataAction(setDs) = True Then
            '        tempDt.ImportRow(dt.Rows(i))
            '    End If
            'Next

            '印刷処理
            setRptDt = Me.DoPrint(setDs, printType).Tables(LMConst.RD)

            'プレビュー情報を設定
            If setRptDt Is Nothing = False Then
                cnt = setRptDt.Rows.Count - 1
                For j As Integer = 0 To cnt
                    rtnDt.ImportRow(setRptDt.Rows(j))
                Next
            End If
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 印刷時の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdatePrintDataAction(ByVal ds As DataSet) As Boolean
        'Private Function UpdatePrintDataAction(ByVal ds As DataSet, ByVal i As Integer) As Boolean

        '更新対象テーブル名
        Dim tableNm As String = "LMC010IN_OUTKA_L"
        Dim updDt As DataTable = ds.Tables(tableNm)

        '******* 更新 処理を行う ***************

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            If updDt.Rows.Count <> 0 Then

                Dim dr As DataRow = updDt.Rows(0)

                '自営業以外、スルー
                If dr.Item("USER_BR_CD").ToString().Equals(dr.Item("NRS_BR_CD").ToString()) = False Then
                    Return True
                End If

                'データの更新
                'ds = MyBase.CallBLC(Me._Blc, "UpdateOutkaLPrint", ds, ByVal i As Integer)
                ds = MyBase.CallBLC(Me._Blc, "UpdateOutkaLPrint", ds)

                'メッセージの判定
                If MyBase.IsMessageStoreExist(Convert.ToInt32(dr.Item("ROW_NO").ToString())) = True Then
                    Return False
                End If

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return True

    End Function

    ''' <summary>
    ''' データ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Sub UpdateData(ByVal ds As DataSet, ByVal methodNm As String)

        'データの更新
        ds = MyBase.CallBLC(Me._Blc, methodNm, ds)

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = True Then
            Exit Sub
        End If

    End Sub

    '社内入荷データ作成対応 terakawa 2012.11.19 Start
    ''' <summary>
    ''' 社内入荷データ作成処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Function MakeInkaData(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing
        Dim max As Integer = ds.Tables("LMC010_INKA_IN").Rows.Count - 1
        Dim max2 As Integer = 0

        '■社内入荷データ作成許可チェック
        'BLCアクセス
        ds = MyBase.CallBLC(Me._Blc, "SelectIntEdiExist", ds)
        'エラーがあるかを判定
        rtnResult = MyBase.IsMessageExist()
        If rtnResult = True Then
            Return ds
        End If

        ' 社内入荷データ作成条件設定
        setDs.Tables(TABLE_NM.LMC010_INT_EDI).Merge(ds.Tables(TABLE_NM.LMC010_INT_EDI))


        Dim useGoodsConvertTable As Boolean = False
        If (ds.Tables.Contains(TABLE_NM.LMC010_INT_EDI) AndAlso _
            ds.Tables(TABLE_NM.LMC010_INT_EDI).Rows.Count > 0) Then

            ' 商品キーを変換テーブルによって取得するか確認
            useGoodsConvertTable _
                = (LMConst.FLG.ON.Equals(ds.Tables(TABLE_NM.LMC010_INT_EDI)(0).Item("USE_GOODS_CONV_TABLE")))

        End If


        '■社内入荷データ作成用データ取得
        For i As Integer = 0 To max
            '値のクリア
            inTbl = setDs.Tables("LMC010_INKA_IN")
            inTbl.Clear()

            'メッセージクリア
            MyBase.SetMessage(Nothing)

            '条件の設定
            inTbl.ImportRow(ds.Tables("LMC010_INKA_IN").Rows(i))

            'BLCアクセス
            setDs = MyBase.CallBLC(Me._Blc, "SelectMakeInkaData", setDs)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            ' エラーデータが存在する場合、それ以降のデータが追加されなくなる。
            If (useGoodsConvertTable = False) Then ' 依頼番号:000209 は継続に変更

                '2013.01.21 要望番号1789 追加START
                rtnResult = Not MyBase.IsMessageStoreExist()
                '2013.01.21 要望番号1789 追加END

            End If

            If rtnResult = True Then
                '取得したデータをsetDsからdsに移動
                max2 = setDs.Tables("LMC010_INKA_OUT").Rows.Count - 1
                For j As Integer = 0 To max2
                    ds.Tables("LMC010_INKA_OUT").ImportRow(setDs.Tables("LMC010_INKA_OUT").Rows(j))
                Next
            End If
        Next

        If ds.Tables("LMC010_INKA_OUT").Rows.Count = 0 Then
            '1件も作成対象データがない場合は終了
            Return ds
        End If

        'メッセージクリア
        MyBase.SetMessage(Nothing)

        '■社内入荷データ作成
        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'BLCアクセス
            ds = MyBase.CallBLC(Me._Blc, "MakeInkaData", ds)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then
                'トランザクション終了
                MyBase.CommitTransaction(scope)
            End If

        End Using

        Return ds

    End Function
    '社内入荷データ作成対応 terakawa 2012.11.19 End

#End Region

#Region "一括引当処理"

    ''' <summary>
    ''' データ登録、更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのInsertSaveActionメソッドに飛ぶ</remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMC010_OUTKA_M_IN")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing
        Dim prtDt As DataTable = Nothing

        Dim rtnResult As Boolean = False

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone
        Dim rtnDs As DataSet = Nothing

        'START YANAI 20110914 一括引当対応
        Dim printDt As DataTable = ds.Tables("LMC010PRINT_CUST")
        Dim printDr() As DataRow = Nothing
        'END YANAI 20110914 一括引当対応

        'START YANAI 要望番号638
        Dim rtnDs2 As DataSet = Nothing
        Dim unsoTehaiDr As DataRow() = Nothing
        'END YANAI 要望番号638

        'START YANAI 引当エラーは音声CSV出力しない
        Dim errRow As DataRow = Nothing
        'END YANAI 引当エラーは音声CSV出力しない

        '更新処理
        For i As Integer = 0 To max

            'START YANAI メモ②No.15,16,17
            If ("01").Equals(dt.Rows(i).Item("ERR_FLG").ToString()) = True Then
                'エラーフラグが"01"の場合はスルー
                Continue For
            End If
            'END YANAI メモ②No.15,16,17

            Dim strdate As Date = Nothing
            Dim strtime As Long = 0
            Dim strMsg As String = String.Empty


            Dim enddate As Date = Nothing
            Dim endtime As Long = 0

            Dim printType As String = "09"
            Dim printTypeShipg As String = PRINT_TYPE.SHIPMENTGUIDE     'ADD 2023/03/29 送品案内書(FFEM)追加
            Dim printTypeDokuj As String = PRINT_TYPE.DOKU_JOJU

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                'メッセージクリア
                MyBase.SetMessage(Nothing)

                '値のクリア
                inTbl = setDs.Tables("LMC010_OUTKA_M_IN")
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(dt.Rows(i))

                If dt.Rows(i).Item("IS_FFEM_MATERIAL_PLANT_TRANSFER").ToString() <> LMConst.FLG.ON Then

                    'チェック処理
                    setDs = MyBase.CallBLC(Me._Blc, "SelectCountOutkaM", setDs)
                    If CInt(setDs.Tables("LMC010_OUTKA_M_CONUNT_OUT").Rows(0).Item("CNT").ToString()) <> setDs.Tables("LMC010_OUTKA_M_OUT").Select(String.Concat("OUTKA_NO_L = '", setDs.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("OUTKA_NO_L").ToString(), "'")).Count() Then

                        errRow = setDs.Tables("LMC010_ERR_ROWNO").NewRow
                        errRow("ROW_NO") = dt.Rows(i).Item("ROW_NO").ToString
                        setDs.Tables("LMC010_ERR_ROWNO").Rows.Add(errRow)

                        strMsg = String.Concat(setDs.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("OUTKA_NO_L").ToString)
                        MyBase.SetMessageStore("00", "E914", New String() {strMsg}, dt.Rows(i).Item("ROW_NO").ToString())

                        ' 次のデータへ
                        Continue For

                    End If

                    '2018/01/15 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add start
                    If SetPrefixOfAutoDenpNo(setDs, dt.Rows(i)).Equals(False) Then
                        errRow = setDs.Tables("LMC010_ERR_ROWNO").NewRow
                        errRow("ROW_NO") = dt.Rows(i).Item("ROW_NO").ToString
                        setDs.Tables("LMC010_ERR_ROWNO").Rows.Add(errRow)

                        strMsg = String.Concat(setDs.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("OUTKA_NO_L").ToString)
                        MyBase.SetMessageStore("00", "E262", New String() {strMsg}, dt.Rows(i).Item("ROW_NO").ToString())

                        ' 次のデータへ
                        Continue For
                    End If

                    '2018/01/15 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add end

                    strdate = Now
                    strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
                    MyBase.Logger.WriteLog(0, "LMC010BLF", String.Concat("引当更新 ", i + 1, "件目"), "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

                    '更新
                    setDs = MyBase.CallBLC(Me._Blc, "UpdateData", setDs)

                    enddate = Now
                    endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
                    MyBase.Logger.WriteLog(0, "LMC010BLF", String.Concat("引当更新 ", i + 1, "件目"), "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
                    MyBase.Logger.WriteLog(0, "LMC010BLF", String.Concat("引当更新 ", i + 1, "件目"), "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")

                    rtnResult = Not MyBase.IsMessageStoreExist(Convert.ToInt32(inTbl.Rows(0).Item("ROW_NO").ToString()))


                    'エラーがあるかを判定
                    If rtnResult = False Then

                        'エラーの場合はエラー処理
                        'START YANAI 引当エラーは音声CSV出力しない
                        errRow = setDs.Tables("LMC010_ERR_ROWNO").NewRow
                        errRow("ROW_NO") = dt.Rows(i).Item("ROW_NO").ToString
                        setDs.Tables("LMC010_ERR_ROWNO").Rows.Add(errRow)
                        'END YANAI 引当エラーは音声CSV出力しない

                        ' 次のデータへ
                        Continue For

                    End If

                End If

                unsoTehaiDr = setDs.Tables("LMC010OUT_UNCHIN").Select(String.Concat("OUTKA_NO_L = '", dt.Rows(i).Item("OUTKA_NO_L").ToString(), "'"))

                If ("10").Equals(unsoTehaiDr(0).Item("UNSO_TEHAI_KB").ToString()) = True Then

                    If dt.Rows(i).Item("IS_FFEM_MATERIAL_PLANT_TRANSFER").ToString() <> LMConst.FLG.ON Then

                        '日陸手配の時のみ運賃データを作成
                        strdate = Now
                        strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
                        MyBase.Logger.WriteLog(0, "LMC010BLF", String.Concat("運賃計算 ", i + 1, "件目"), "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

                        'データセット設定
                        Dim unchinDs As DataSet = Me.SetUnchinInDataSet(setDs, inTbl)

                        'BLCアクセス
                        unchinDs = Me.UnchinBlcAccess(unchinDs, "CreateUnchinData")

                        'LMF800の戻り値判定
                        Dim rtnResultDt As DataTable = unchinDs.Tables("LMF800RESULT")
                        Dim rtnResultDr As DataRow = rtnResultDt.Rows(0)

                        If ("00").Equals(rtnResultDr.Item("STATUS").ToString) = True OrElse _
                           ("05").Equals(rtnResultDr.Item("STATUS").ToString) = True OrElse _
                           ("30").Equals(rtnResultDr.Item("STATUS").ToString) = True Then
                            '正常の場合は保存処理

                            'BLCアクセス(運賃削除)
                            rtnDs2 = MyBase.CallBLC(Me._Blc, "DeleteUnchinData", setDs)

                            'BLCアクセス(運賃作成)
                            unchinDs.Tables.Add(ds.Tables("LMC010OUT_UNCHIN").Copy)
                            unchinDs = Me.CallBLC(Me._Blc, "InsertUnchinData", unchinDs)

                        End If

                        enddate = Now
                        endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
                        MyBase.Logger.WriteLog(0, "LMC010BLF", String.Concat("運賃計算 ", i + 1, "件目"), "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
                        MyBase.Logger.WriteLog(0, "LMC010BLF", String.Concat("運賃計算 ", i + 1, "件目"), "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")

                        'エラーがあるかを判定
                        rtnResult = Not MyBase.IsMessageExist()

                        If rtnResult = True Then
                            'データセット設定
                            Dim shiharaiDs As DataSet = Me.SetShiharaiInDataSet(setDs, inTbl)

                            'BLCアクセス
                            shiharaiDs = Me.ShiharaiBlcAccess(shiharaiDs, "CreateUnchinData")

                            'LMF810の戻り値判定
                            Dim rtnResultDt2 As DataTable = shiharaiDs.Tables("LMF810RESULT")
                            Dim rtnResultDr2 As DataRow = rtnResultDt2.Rows(0)

                            If ("00").Equals(rtnResultDr2.Item("STATUS").ToString) = True OrElse _
                               ("05").Equals(rtnResultDr2.Item("STATUS").ToString) = True OrElse _
                               ("30").Equals(rtnResultDr2.Item("STATUS").ToString) = True Then
                                '正常の場合は保存処理
                                strdate = Now
                                strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
                                MyBase.Logger.WriteLog(0, "LMC010BLF", String.Concat("支払計算 ", i + 1, "件目"), "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

                                'BLCアクセス(支払運賃削除)
                                rtnDs2 = MyBase.CallBLC(Me._Blc, "DeleteShiharaiData", setDs)

                                'BLCアクセス(支払運賃作成)
                                shiharaiDs.Tables.Add(ds.Tables("LMC010OUT_SHIHARAI").Copy)
                                shiharaiDs = Me.CallBLC(Me._Blc, "InsertShiharaiData", shiharaiDs)

                                enddate = Now
                                endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
                                MyBase.Logger.WriteLog(0, "LMC010BLF", String.Concat("支払計算 ", i + 1, "件目"), "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
                                MyBase.Logger.WriteLog(0, "LMC010BLF", String.Concat("支払計算 ", i + 1, "件目"), "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")

                            End If

                        End If

                    End If

                Else

                    printType = "09EX"
                    printTypeShipg = PRINT_TYPE.SHIPMENTGUIDE_EX    'ADD 2023/03/29 送品案内書(FFEM)追加
                    printTypeDokuj = PRINT_TYPE.DOKU_JOJU_EX
                End If

                'エラーがあるかを判定
                rtnResult = Not MyBase.IsMessageExist()

                If rtnResult = False Then

                    'エラーの場合はエラー処理
                    errRow = setDs.Tables("LMC010_ERR_ROWNO").NewRow
                    errRow("ROW_NO") = dt.Rows(i).Item("ROW_NO").ToString
                    setDs.Tables("LMC010_ERR_ROWNO").Rows.Add(errRow)

                    ' 次のデータへ
                    Continue For

                End If

                'エラーが無ければCommit
                MyBase.CommitTransaction(scope)

            End Using

            ' 帳票印刷
            If rtnResult = True Then

                strdate = Now
                strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
                MyBase.Logger.WriteLog(0, "LMC010BLF", String.Concat("出荷指示書印刷 ", i + 1, "件目"), "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

                '印刷処理(出荷指示書)
                prtDt = setDs.Tables("LMC010IN_OUTKA_L")
                prtDt.Clear()
                prtDt.Rows.Add(Me.SetPrintDr(inTbl.Rows(0), prtDt.NewRow()))
                rtnDs = Me.DoPrint(setDs, printType)

                rdPrevDt.Merge(rtnDs.Tables(LMConst.RD))

                enddate = Now
                endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
                MyBase.Logger.WriteLog(0, "LMC010BLF", String.Concat("出荷指示書印刷 ", i + 1, "件目"), "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
                MyBase.Logger.WriteLog(0, "LMC010BLF", String.Concat("出荷指示書印刷 ", i + 1, "件目"), "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")

                printDr = printDt.Select(String.Concat("CUST_CD_L = '", inTbl.Rows(0).Item("CUST_CD_L").ToString(), "'"))
                If 0 < printDr.Length Then
                    '該当の荷主の場合のみ、送り状の印刷処理を行う

                    strdate = Now
                    strtime = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
                    MyBase.Logger.WriteLog(0, "LMC010BLF", String.Concat("送り状印刷 ", i + 1, "件目"), "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

                    '印刷処理
                    prtDt = setDs.Tables("LMC010IN_OUTKA_L")
                    prtDt.Clear()
                    prtDt.Rows.Add(Me.SetPrintDr(inTbl.Rows(0), prtDt.NewRow()))
                    rtnDs = Me.DoPrint(setDs, "02")

                    rdPrevDt.Merge(rtnDs.Tables(LMConst.RD))

                    enddate = Now
                    endtime = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
                    MyBase.Logger.WriteLog(0, "LMC010BLF", String.Concat("送り状印刷 ", i + 1, "件目"), "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
                    MyBase.Logger.WriteLog(0, "LMC010BLF", String.Concat("送り状印刷 ", i + 1, "件目"), "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")

                End If

#If True Then   'ADD 2023/03/29 送品案内書(FFEM)追加
                '印刷処理(送品案内書)
                prtDt = setDs.Tables("LMC010IN_OUTKA_L")
                prtDt.Clear()
                prtDt.Rows.Add(Me.SetPrintDr(inTbl.Rows(0), prtDt.NewRow()))
                rtnDs = Me.DoPrint(setDs, printTypeShipg)

                rdPrevDt.Merge(rtnDs.Tables(LMConst.RD))
#End If

                '印刷処理(毒劇物譲受書)
                prtDt = setDs.Tables("LMC010IN_OUTKA_L")
                prtDt.Clear()
                prtDt.Rows.Add(Me.SetPrintDr(inTbl.Rows(0), prtDt.NewRow()))
                rtnDs = Me.DoPrint(setDs, printTypeDokuj)

                rdPrevDt.Merge(rtnDs.Tables(LMConst.RD))

            End If

            'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

        Next

        setDs.Tables(LMConst.RD).Clear()
        setDs.Tables(LMConst.RD).Merge(rdPrevDt)

        Return setDs

    End Function

    ''' <summary>
    ''' 印刷に使う情報を設定
    ''' </summary>
    ''' <param name="setDr">DataRow</param>
    ''' <param name="dr">DataRow</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetPrintDr(ByVal setDr As DataRow, ByVal dr As DataRow) As DataRow

        dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
        dr.Item("OUTKA_NO_L") = setDr.Item("OUTKA_NO_L").ToString()

        '(2012.09.07) 要望番号1416 荷主コード(大)を設定 --- START ---
        dr.Item("CUST_CD_L") = setDr.Item("CUST_CD_L").ToString()
        '(2012.09.07) 要望番号1416 荷主コード(大)を設定 ---  END  ---

        Return dr

    End Function

    'START YANAI 要望番号638
    ''' <summary>
    ''' 運賃INの値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetUnchinInDataSet(ByVal ds As DataSet, ByVal inTbl As DataTable) As DataSet

        Dim unchinDs As DataSet = New LMF800DS()
        Dim insRows As DataRow = unchinDs.Tables("LMF800IN").NewRow

        Dim drs As DataRow() = ds.Tables("LMC010OUT_UNCHIN").Select(String.Concat("OUTKA_NO_L = '", inTbl.Rows(0).Item("OUTKA_NO_L").ToString(), "' "))

        insRows.Item("WH_CD") = drs(0).Item("WH_CD").ToString
        insRows.Item("NRS_BR_CD") = drs(0).Item("NRS_BR_CD").ToString
        insRows.Item("UNSO_NO_L") = drs(0).Item("UNSO_NO_L").ToString

        'データセットに追加
        unchinDs.Tables("LMF800IN").Rows.Add(insRows)

        Return unchinDs

    End Function

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 支払運賃INの値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetShiharaiInDataSet(ByVal ds As DataSet, ByVal inTbl As DataTable) As DataSet

        Dim unchinDs As DataSet = New LMF810DS()
        Dim insRows As DataRow = unchinDs.Tables("LMF810IN").NewRow

        Dim drs As DataRow() = ds.Tables("LMC010OUT_SHIHARAI").Select(String.Concat("OUTKA_NO_L = '", inTbl.Rows(0).Item("OUTKA_NO_L").ToString(), "' "))

        insRows.Item("WH_CD") = drs(0).Item("WH_CD").ToString
        insRows.Item("NRS_BR_CD") = drs(0).Item("NRS_BR_CD").ToString
        insRows.Item("UNSO_NO_L") = drs(0).Item("UNSO_NO_L").ToString

        'データセットに追加
        unchinDs.Tables("LMF810IN").Rows.Add(insRows)

        Return unchinDs

    End Function
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
    ''' <summary>
    ''' 支払運賃INの値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetShiharaiInDataSet2(ByVal ds As DataSet, ByVal inTbl As DataTable) As DataSet

        Dim unchinDs As DataSet = New LMF810DS()
        Dim insRows As DataRow = unchinDs.Tables("LMF810IN").NewRow

        'Dim drs As DataRow() = ds.Tables("LMC010OUT").Select(String.Concat("OUTKA_NO_L = '", inTbl.Rows(0).Item("OUTKA_NO_L").ToString(), "' "))

        insRows.Item("WH_CD") = inTbl.Rows(0).Item("WH_CD").ToString
        insRows.Item("NRS_BR_CD") = inTbl.Rows(0).Item("NRS_BR_CD").ToString
        insRows.Item("UNSO_NO_L") = inTbl.Rows(0).Item("UNSO_NO_L").ToString

        'データセットに追加
        unchinDs.Tables("LMF810IN").Rows.Add(insRows)

        Return unchinDs

    End Function
    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End



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
    'END YANAI 要望番号638

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
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
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

#End Region

#Region "特別梱包個数計算"
    'START YANAI 20120322 特別梱包個数計算
    ''' <summary>
    ''' 特別梱包個数計算
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaPkgNb(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim tableNm As String = "LMC010_OUTKA_M_IN"
        Dim dt As DataTable = ds.Tables(tableNm)

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNm)

        Dim rtnResult As Boolean = False
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                'メッセージクリア
                MyBase.SetMessage(Nothing)

                '初期化
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(dt.Rows(i))

                '特別梱包個数計算処理を行う
                ds = MyBase.CallBLC(Me._Blc, "UpdateOutkaPkgNb", setDs)

                'エラーがあるかを判定
                rtnResult = Not MyBase.IsMessageExist()
                If rtnResult = True Then
                    'エラーが無ければCommit
                    MyBase.CommitTransaction(scope)
                Else
                    If String.IsNullOrEmpty(ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("ERR_NO").ToString) = True Then
                        'エラーEXCELに設定
                        MyBase.SetMessageStore("00", "E011", , dt.Rows(i).Item("ROW_NO").ToString())
                    ElseIf ("E285").Equals(ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("ERR_NO").ToString) = True Then
                        'エラーEXCELに設定
                        If ("40").Equals(ds.Tables("F_UNCHIN_TRS").Rows(0).Item("TARIFF_BUNRUI_KB").ToString) = True Then
                            MyBase.SetMessageStore("00", ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("ERR_NO").ToString, New String() {String.Concat("[出荷管理番号=", dt.Rows(i).Item("OUTKA_NO_L").ToString(), "] ", "横持ち料")}, dt.Rows(i).Item("ROW_NO").ToString())
                        Else
                            MyBase.SetMessageStore("00", ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("ERR_NO").ToString, New String() {String.Concat("[出荷管理番号=", dt.Rows(i).Item("OUTKA_NO_L").ToString(), "] ", "運賃")}, dt.Rows(i).Item("ROW_NO").ToString())
                        End If
                    Else
                        'エラーEXCELに設定
                        MyBase.SetMessageStore("00", ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("ERR_NO").ToString, , dt.Rows(i).Item("ROW_NO").ToString())
                    End If
                End If

            End Using

        Next

        Return ds

    End Function
    'END YANAI 20120322 特別梱包個数計算
#End Region

#Region "名変入荷作成処理"
    ''' <summary>
    ''' 名変入荷作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertMeihenInkaData(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim tableNm As String = "LMC010_MEIHEN_INKA_IN"
        Dim dt As DataTable = ds.Tables(tableNm)

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNm)

        Dim rtnResult As Boolean = False
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                'メッセージクリア
                MyBase.SetMessage(Nothing)

                '初期化
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(dt.Rows(i))

                '名変入荷作成処理を行う
                ds = MyBase.CallBLC(Me._Blc, "InsertMeihenInkaData", setDs)

                'エラーがあるかを判定
                rtnResult = Not MyBase.IsMessageExist()
                If rtnResult = True Then
                    'エラーが無ければCommit
                    MyBase.CommitTransaction(scope)

                End If

                'データテーブルのレコードクリア
                setDs.Tables("LMC010_MEIHEN_INKA_L").Clear()
                setDs.Tables("LMC010_MEIHEN_INKA_M").Clear()
                setDs.Tables("LMC010_MEIHEN_INKA_S").Clear()
                setDs.Tables("LMC010_MEIHEN_ZAI_TRS").Clear()

            End Using

        Next

        Return ds

    End Function

#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="printType">印刷種別</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Dim setDs As DataSet() = Nothing
        Dim meitetsuDs As DataSet = Nothing         'ADD 2017/07/20
        Dim prtBlc As Com.Base.BaseBLC() = Nothing
        Dim nrsBrCd As String = ds.Tables("LMC010IN_OUTKA_L").Rows(0)("NRS_BR_CD").ToString()
        Dim custCdL As String = ds.Tables("LMC010IN_OUTKA_L").Rows(0)("CUST_CD_L").ToString()
        Dim nihudaYN As String = ds.Tables("LMC010IN_OUTKA_L").Rows(0)("NIHUDA_YN").ToString()
        'START YANAI 20120122 立会書印刷対応
        Dim tachiaiFlg As String = ds.Tables("LMC010IN_OUTKA_L").Rows(0)("TACHIAI_FLG").ToString()
        'END YANAI 20120122 立会書印刷対応

        Dim meitetsuFlg As String = ds.Tables("LMC010IN_OUTKA_L").Rows(0)("MEITETSU_FLG").ToString()      'ADD 2017/07/21

        Select Case printType

            Case "01" '荷札

#If False Then
                prtBlc = New Com.Base.BaseBLC() {New LMC550BLC()}
                'START UMANO メモ(EDI)No.49
                setDs = New DataSet() {Me.SetDataSetLMC550InData(ds, printType)}
                'START UMANO メモ(EDI)No.49
#Else

#If True Then   'ADD 2018/10/15 依頼番号 : 002524   【LMS】荷札発行無し設定だが、出荷データ検索画面から印刷すると出力されるバグ
                '荷札対象時の時だけ処理
                If nihudaYN.Equals("01") = True Then

                    '**********************************************************************************
                    '* LMC789BLC    : 名鉄・荷札
                    '* LMC794BLC    : 名鉄・送り状
                    '*   DoPrint は、データセレクトはしないで印刷処理のため
                    '*　 ここでデータセレクトしDSにセットする。以降は共通処理を行う　　　
                    '*
                    '*　※LMC010BLF PrintMeitetuReport(名鉄帳票印刷(荷札+送状) 参照
                    '**********************************************************************************

                    If ("1").Equals(meitetsuFlg) = True Then
                        '名鉄専用  送状 550の変更

                        meitetsuDs = New LMC794DS

                        prtBlc = New Com.Base.BaseBLC() {New LMC794BLC()}
                        setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printType)}

                        meitetsuDs.Merge(setDs(0))

                        meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                        meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = MyBase.GetSystemDate() 'ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                        meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = MyBase.GetSystemTime() 'ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                        meitetsuDs = PrintMeitetuReport2(meitetsuDs)
                        ' 荷札用入力データ
                        meitetsuDs = Me.SetDataSetLMC789InData(meitetsuDs, "LMC794OUT")

                        '荷札用に再設定
                        prtBlc(0) = New LMC789BLC()
                        setDs(0) = meitetsuDs.Copy

                    Else
                        prtBlc = New Com.Base.BaseBLC() {New LMC550BLC()}
                        setDs = New DataSet() {Me.SetDataSetLMC550InData(ds, printType)}

                    End If

#End If


                End If
#End If

            Case "02" '送状

#If False Then  'UPD 217/07/26 名鉄専用処理追加
                 prtBlc = New Com.Base.BaseBLC() {New LMC560BLC()}
                'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                'setDs = New DataSet() {Me.SetDataSetLMC560InData(ds)}
                setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType)}
                'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)

#Else
                If ("1").Equals(meitetsuFlg) = True Then
                    '名鉄専用  送状 560の変更

                    meitetsuDs = New LMC794DS

                    prtBlc = New Com.Base.BaseBLC() {New LMC794BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printType)}

                    meitetsuDs.Merge(setDs(0))

                    meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                    meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = MyBase.GetSystemDate() 'ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                    meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = MyBase.GetSystemTime() 'ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                    meitetsuDs = PrintMeitetuReport2(meitetsuDs)
                    setDs(0) = meitetsuDs.Copy
                Else
                    prtBlc = New Com.Base.BaseBLC() {New LMC560BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType)}

                End If

#End If

            Case "03" '納品書

                'START YANAI 20120122 立会書印刷対応
                'If nrsBrCd.Equals("40") = True AndAlso custCdL.Equals("00237") = True Then
                '    '営業所コード：'40'（YCC）荷主コード大：'00237'（サクラ）の場合
                '    '送信案内書(YCCサクラ用)(LMC540)と受領書(YCCサクラ用)(LMC541)を印刷
                '    prtBlc = New Com.Base.BaseBLC() {New LMC540BLC(), New LMC541BLC()}
                '    setDs = New DataSet() {Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}
                'Else
                '    prtBlc = New Com.Base.BaseBLC() {New LMC500BLC()}
                '    setDs = New DataSet() {Me.SetDataSetLMC500InData(ds)}
                'End If

                '====2012/07/10 Notes749,911　立会書(LMC640)を呼ばないようにコメントアウト 開始====
                'If nrsBrCd.Equals("40") = True AndAlso custCdL.Equals("00237") = True AndAlso ("01").Equals(tachiaiFlg) = True Then
                '    '営業所コード：'40'（YCC）荷主コード大：'00237'（サクラ）の場合
                '    '送信案内書(YCCサクラ用)(LMC540)と受領書(YCCサクラ用)(LMC541)を印刷
                '    '立会フラグが"01"の場合、立会書を印刷
                '    prtBlc = New Com.Base.BaseBLC() {New LMC540BLC(), New LMC541BLC(), New LMC640BLC()}
                '    setDs = New DataSet() {Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC640InData(ds)}
                'ElseIf nrsBrCd.Equals("40") = True AndAlso custCdL.Equals("00237") = True Then
                '    '営業所コード：'40'（YCC）荷主コード大：'00237'（サクラ）の場合
                '    '送信案内書(YCCサクラ用)(LMC540)と受領書(YCCサクラ用)(LMC541)を印刷
                '    prtBlc = New Com.Base.BaseBLC() {New LMC540BLC(), New LMC541BLC()}
                '    setDs = New DataSet() {Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}
                'ElseIf ("01").Equals(tachiaiFlg) = True Then
                '    '立会フラグが"01"の場合、立会書を印刷
                '    prtBlc = New Com.Base.BaseBLC() {New LMC500BLC(), New LMC640BLC()}
                '    setDs = New DataSet() {Me.SetDataSetLMC500InData(ds), Me.SetDataSetLMC640InData(ds)}
                'Else
                '    prtBlc = New Com.Base.BaseBLC() {New LMC500BLC()}
                '    setDs = New DataSet() {Me.SetDataSetLMC500InData(ds)}
                'End If
                'END YANAI 20120122 立会書印刷対応
                '====2012/07/10 Notes749,911　立会書(LMC640)を呼ばないようにコメントアウト　終了====

                '====2012/07/10 Notes749,911　立会書(LMC640対策版 開始====

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
                ElseIf nrsBrCd.Equals("15") = True AndAlso custCdL.Equals("00660") = True Then
                    '営業所コード：'15'（土気）荷主コード大：'00660'（サクラ）の場合
                    '送信案内書(YCCサクラ用)(LMC540)と受領書(YCCサクラ用)(LMC541)を印刷
                    prtBlc = New Com.Base.BaseBLC() {New LMC540BLC(), New LMC541BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}
                Else
                    '上記条件に当てはまらない場合、納品書(LMC500)を印刷。
                    prtBlc = New Com.Base.BaseBLC() {New LMC500BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printType)}
                End If

                '====2012/07/10 Notes749,911　立会書(LMC640対策版　終了====

            Case "04" '分析表


            Case "05" '纏めピッキングリスト

                prtBlc = New Com.Base.BaseBLC() {New LMC690BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC690InData(ds)}

            Case "06" '纏め送状

                prtBlc = New Com.Base.BaseBLC() {New LMC560BLC()}
                'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                'setDs = New DataSet() {Me.SetDataSetLMC560InData(ds)}
                setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType)}
                'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)

            Case "07" '出荷報告

                prtBlc = New Com.Base.BaseBLC() {New LMC610BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC610InData(ds)}

            Case "08" '一括印刷

                'START YANAI 20120122 立会書印刷対応
                ''分析表,送状,納品書,荷札
                'If nrsBrCd.Equals("40") = True AndAlso custCdL.Equals("00237") = True Then
                '    '荷札有無フラグが"01"の場合は荷札も印刷
                '    If nihudaYN.Equals("01") = True Then
                '        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC550BLC()}
                '        'START UMANO メモ(EDI)No.49
                '        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC550InData(ds, printType)}
                '        'END UMANO メモ(EDI)No.49
                '    Else
                '        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC()}
                '        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}
                '    End If
                'Else
                '    If nihudaYN.Equals("01") = True Then
                '        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC550BLC()}
                '        'START UMANO メモ(EDI)No.49
                '        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds), Me.SetDataSetLMC550InData(ds, printType)}
                '        'END UMANO メモ(EDI)No.49
                '    Else
                '        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC()}
                '        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds)}
                '    End If
                'End If

                '====2012/07/10 Notes749,911　立会書(LMC640)を呼ばないようにコメントアウト 開始====
                ''分析表,送状,納品書,荷札
                'If nrsBrCd.Equals("40") = True AndAlso custCdL.Equals("00237") = True Then
                '    '荷札有無フラグが"01"の場合は荷札も印刷
                '    If nihudaYN.Equals("01") = True AndAlso ("01").Equals(tachiaiFlg) = True Then
                '        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC550BLC(), New LMC640BLC()}
                '        'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                '        'setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC550InData(ds, printType), Me.SetDataSetLMC640InData(ds)}
                '        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC550InData(ds, printType), Me.SetDataSetLMC640InData(ds)}
                '        'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                '    ElseIf nihudaYN.Equals("01") = True Then
                '        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC550BLC()}
                '        'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                '        'setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC550InData(ds, printType)}
                '        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC550InData(ds, printType)}
                '        'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                '    ElseIf ("01").Equals(tachiaiFlg) = True Then
                '        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC640BLC()}
                '        'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                '        'setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC640InData(ds)}
                '        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC640InData(ds)}
                '        'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                '    Else
                '        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC()}
                '        'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                '        'setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}
                '        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds)}
                '        'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                '    End If
                'Else
                '    If nihudaYN.Equals("01") = True AndAlso ("01").Equals(tachiaiFlg) = True Then
                '        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC550BLC(), New LMC640BLC()}
                '        'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                '        'setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds), Me.SetDataSetLMC550InData(ds, printType), Me.SetDataSetLMC640InData(ds)}
                '        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType), Me.SetDataSetLMC500InData(ds), Me.SetDataSetLMC550InData(ds, printType), Me.SetDataSetLMC640InData(ds)}
                '        'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)

                '    ElseIf nihudaYN.Equals("01") = True Then
                '        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC550BLC()}
                '        'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                '        'setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds), Me.SetDataSetLMC550InData(ds, printType)}
                '        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType), Me.SetDataSetLMC500InData(ds), Me.SetDataSetLMC550InData(ds, printType)}
                '        'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                '    ElseIf ("01").Equals(tachiaiFlg) = True Then
                '        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC640BLC()}
                '        'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                '        'setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds), Me.SetDataSetLMC640InData(ds)}
                '        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType), Me.SetDataSetLMC500InData(ds), Me.SetDataSetLMC640InData(ds)}
                '        'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                '    Else
                '        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC()}
                '        'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                '        'setDs = New DataSet() {Me.SetDataSetLMC560InData(ds), Me.SetDataSetLMC500InData(ds)}
                '        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType), Me.SetDataSetLMC500InData(ds)}
                '        'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                '    End If
                'End If
                ''END YANAI 20120122 立会書印刷対応
                '====2012/07/10 Notes749,911　立会書(LMC640)を呼ばないようにコメントアウト　終了====

                '====2012/07/10 Notes749,911　立会書(LMC640対策版 開始====

                '**************************************************
                '   一括に運送保険を追加　 LMC870　ADD 2018/07/24 依頼番号 : 001540  
                '**************************************************

                '分析表,送状,納品書,荷札
                '横浜でかつサクラファインテックの場合
                If nrsBrCd.Equals("40") = True AndAlso custCdL.Equals("00237") = True Then
                    '荷札有無フラグが"01"の場合は荷札も印刷
                    If nihudaYN.Equals("01") = True Then
                        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC550BLC(), New LMC870BLC()}
                        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC550InData(ds, printType), Me.SetDataSetLMC870InData(ds, printType)}
                    ElseIf nrsBrCd.Equals("15") = True AndAlso custCdL.Equals("00660") = True Then
                        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC550BLC(), New LMC870BLC()}
                        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC550InData(ds, printType), Me.SetDataSetLMC870InData(ds, printType)}
                    Else
                        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC540BLC(), New LMC541BLC(), New LMC870BLC()}
                        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType), Me.SetDataSetLMC540InData(ds), Me.SetDataSetLMC541InData(ds), Me.SetDataSetLMC870InData(ds, printType)}
                    End If

                    'ADD 2022/04/21 028723 千葉専用追加 開始　**********************************************************************************************
                ElseIf nrsBrCd.Equals("10") = True Then
                    If nihudaYN.Equals("01") = True Then
                        If ("1").Equals(meitetsuFlg) = True Then
                            '名鉄専用  送状 560の変更

                            meitetsuDs = New LMC794DS

                            prtBlc = New Com.Base.BaseBLC() {New LMC794BLC(), New LMC500BLC(), New LMC794BLC(), New LMC520BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printType), Me.SetDataSetLMC500InData(ds, printType), Me.SetDataSetLMC794InData(ds, printType), Me.SetDataSetLMC520InData2(ds)}
                            'prtBlc = New Com.Base.BaseBLC() {New LMC794BLC(), New LMC500BLC(), New LMC794BLC(), New LMC870BLC()}
                            'setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printType), Me.SetDataSetLMC500InData(ds), Me.SetDataSetLMC794InData(ds, printType), Me.SetDataSetLMC870InData(ds, printType)}

                            meitetsuDs.Merge(setDs(0))   ' 送状を設定

                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = MyBase.GetSystemDate() 'ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = MyBase.GetSystemTime() 'ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                            meitetsuDs = PrintMeitetuReport2(meitetsuDs)
                            setDs(0) = meitetsuDs.Copy

                            '名鉄専用 荷札 550の変更
                            meitetsuDs = New LMC794DS

                            meitetsuDs.Merge(setDs(0))
                            meitetsuDs.Tables("LMC794OUT").Clear()

                            meitetsuDs = PrintMeitetuReport2(meitetsuDs)
                            ' 荷札用入力データ
                            meitetsuDs = Me.SetDataSetLMC789InData(meitetsuDs, "LMC794OUT")

                            '荷札用に再設定
                            prtBlc(2) = New LMC789BLC()
                            setDs(2) = meitetsuDs.Copy

                        Else
                            prtBlc = New Com.Base.BaseBLC() {New LMC500BLC(), New LMC520BLC(), New LMC560BLC(), New LMC550BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printType), Me.SetDataSetLMC520InData2(ds), Me.SetDataSetLMC560InData(ds, printType), Me.SetDataSetLMC550InData(ds, printType)}

                        End If

                    Else
                        If ("1").Equals(meitetsuFlg) = True Then
                            '名鉄専用  送状 560の変更

                            meitetsuDs = New LMC794DS

                            prtBlc = New Com.Base.BaseBLC() {New LMC500BLC(), New LMC520BLC(), New LMC794BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printType), Me.SetDataSetLMC520InData2(ds), Me.SetDataSetLMC794InData(ds, printType)}
                            ''prtBlc = New Com.Base.BaseBLC() {New LMC794BLC(), New LMC500BLC(), New LMC870BLC()}
                            ''setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printType), Me.SetDataSetLMC500InData(ds), Me.SetDataSetLMC870InData(ds, printType)}


                            meitetsuDs.Merge(setDs(2))　' 送状を設定

                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = MyBase.GetSystemDate() 'ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = MyBase.GetSystemTime() 'ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                            meitetsuDs = PrintMeitetuReport2(meitetsuDs)
                            setDs(2) = meitetsuDs.Copy　' 送状を設定
                        Else
                            prtBlc = New Com.Base.BaseBLC() {New LMC500BLC(), New LMC520BLC(), New LMC560BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC500InData(ds, printType), Me.SetDataSetLMC520InData2(ds), Me.SetDataSetLMC560InData(ds, printType)}
                        End If
                    End If
                    'ADD 2022/04/21 028723 千葉専用追加 終了 ********************************************************************************************


                Else
                    '荷札有無フラグが"01"の場合は荷札も印刷
#If False Then      'UPD 2017/07/21 名鉄対応
                    If nihudaYN.Equals("01") = True Then
                        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC550BLC()}
                        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType), Me.SetDataSetLMC500InData(ds), Me.SetDataSetLMC550InData(ds, printType)}
                    Else
                        prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC()}
                        setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType), Me.SetDataSetLMC500InData(ds)}
                    End If
#Else

                    '**********************************************************************************
                    '* LMC789BLC    : 名鉄・荷札
                    '* LMC794BLC    : 名鉄・送り状
                    '*   DoPrint は、データセレクトはしないで印刷処理のため
                    '*　 ここでデータセレクトしDSにセットする。以降は共通処理を行う　　　
                    '*
                    '*　※LMC010BLF PrintMeitetuReport(名鉄帳票印刷(荷札+送状) 参照
                    '**********************************************************************************

                    If nihudaYN.Equals("01") = True Then
                        If ("1").Equals(meitetsuFlg) = True Then
                            '名鉄専用  送状 560の変更

                            meitetsuDs = New LMC794DS

                            prtBlc = New Com.Base.BaseBLC() {New LMC794BLC(), New LMC500BLC(), New LMC794BLC(), New LMC870BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printType), Me.SetDataSetLMC500InData(ds, printType), Me.SetDataSetLMC794InData(ds, printType), Me.SetDataSetLMC870InData(ds, printType)}

                            meitetsuDs.Merge(setDs(0))

                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = MyBase.GetSystemDate() 'ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = MyBase.GetSystemTime() 'ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                            meitetsuDs = PrintMeitetuReport2(meitetsuDs)
                            setDs(0) = meitetsuDs.Copy

                            '名鉄専用 荷札 550の変更
                            meitetsuDs = New LMC794DS

                            meitetsuDs.Merge(setDs(0))
                            meitetsuDs.Tables("LMC794OUT").Clear()

                            meitetsuDs = PrintMeitetuReport2(meitetsuDs)
                            ' 荷札用入力データ
                            meitetsuDs = Me.SetDataSetLMC789InData(meitetsuDs, "LMC794OUT")

                            '荷札用に再設定
                            prtBlc(2) = New LMC789BLC()
                            setDs(2) = meitetsuDs.Copy

                        Else
                            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC550BLC(), New LMC870BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType), Me.SetDataSetLMC500InData(ds, printType), Me.SetDataSetLMC550InData(ds, printType), Me.SetDataSetLMC870InData(ds, printType)}

                        End If

                    Else
                        If ("1").Equals(meitetsuFlg) = True Then
                            '名鉄専用  送状 560の変更

                            meitetsuDs = New LMC794DS

                            prtBlc = New Com.Base.BaseBLC() {New LMC794BLC(), New LMC500BLC(), New LMC870BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC794InData(ds, printType), Me.SetDataSetLMC500InData(ds, printType), Me.SetDataSetLMC870InData(ds, printType)}

                            meitetsuDs.Merge(setDs(0))

                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("ROW_NO") = "1".ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_DATE") = MyBase.GetSystemDate() 'ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_DATE").ToString
                            meitetsuDs.Tables("LMC794IN").Rows(0).Item("SYS_TIME") = MyBase.GetSystemTime() 'ds.Tables("SYS_DATETIME").Rows(0).Item("SYS_TIME").ToString

                            meitetsuDs = PrintMeitetuReport2(meitetsuDs)
                            setDs(0) = meitetsuDs.Copy
                        Else
                            prtBlc = New Com.Base.BaseBLC() {New LMC560BLC(), New LMC500BLC(), New LMC870BLC()}
                            setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType), Me.SetDataSetLMC500InData(ds, printType), Me.SetDataSetLMC870InData(ds, printType)}
                        End If
                    End If

#End If
                End If

                '====2012/07/10 Notes749,911　立会書(LMC640対策版　終了====

            Case "09", "09EX" '出荷指図

                '====2012/09/07 要望番号1416　出荷指図書(事務所用)出力対応 開始====
                If nrsBrCd.Equals("30") = True AndAlso custCdL.Equals("00021") = True Then
                    '営業所コード：'30'（群馬）荷主コード大：'00021'（篠崎運送）の場合
                    '出荷指図書(現場用)(LMC500)と出荷指図書(事務所用)(LMC531)を印刷
                    printType = "09"
                    prtBlc = New Com.Base.BaseBLC() {New LMC520BLC(), New LMC531BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC520InData2(ds), Me.SetDataSetLMC531InData(ds)}
#If False Then  'DEL 2019/03/08 依頼番号 : 004623   【LMS】群馬日立FN_荷主コード:00072 荷主名:日立物流ファインネクスト トートタンクにて、出荷指図書が出ず確認票だけ出る
                ElseIf nrsBrCd.Equals("30") = True AndAlso custCdL.Equals("00072") = True Then
                    '営業所コード：'30'（群馬）荷主コード大：'00076'（DIC）の場合
                    '出荷指図書(現場用)(LMC500)と出荷指図書(事務所用)(LMC533)を印刷
                    printType = "09"
                    prtBlc = New Com.Base.BaseBLC() {New LMC531BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC531InData(ds)}
#End If
                ElseIf nrsBrCd.Equals("30") = True AndAlso custCdL.Equals("00076") = True Then
                    '営業所コード：'30'（群馬）荷主コード大：'00076'（DIC）の場合
                    '出荷指図書(現場用)(LMC500)と出荷指図書(事務所用)(LMC533)を印刷
                    printType = "09"
                    prtBlc = New Com.Base.BaseBLC() {New LMC531BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC531InData(ds)}
                ElseIf nrsBrCd.Equals("10") = True AndAlso custCdL.Equals("00135") = True OrElse
                     nrsBrCd.Equals("40") = True AndAlso custCdL.Equals("00555") = True OrElse
                     nrsBrCd.Equals("60") = True AndAlso custCdL.Equals("00135") = True Then
                    '営業所コード:'10'(千葉)荷主コード 荷主コード大：'00135' FFEM
                    '営業所コード:'40'(横浜)荷主コード 荷主コード大：'00555' FFEM
                    '営業所コード:'60'(中部)荷主コード 荷主コード大：'00135' FFEM
                    prtBlc = New Com.Base.BaseBLC() {New LMC520BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC520InData2(ds)}
#If False Then  'UPD 2021/09/06 023522   【LMS】安田倉庫移転_PG改修点洗い出し_改修(営業荻山)
                ElseIf (nrsBrCd.Equals("93") = True OrElse nrsBrCd.Equals("96") = True OrElse nrsBrCd.Equals("98") = True) AndAlso custCdL.Equals("00135") = True Then    'MOD 2019/03/25 要望管理005124
                    '営業所コード：'93'高取倉庫 or '96'大分工場 or '98'大牟田工場　荷主コード大：'00135' FFEM

#Else
                ElseIf (nrsBrCd.Equals("93") = True OrElse nrsBrCd.Equals("96") = True OrElse nrsBrCd.Equals("98") = True OrElse nrsBrCd.Equals("F1") = True _
                                             OrElse nrsBrCd.Equals("F2") = True OrElse nrsBrCd.Equals("F3") = True) AndAlso custCdL.Equals("00135") = True Then 'UPD 2022/10/19 033380   【LMS】FFEM足柄工場LMS導入 F2追加, 2023/11/28 039659 F3 追加
                    '営業所コード：'93'高取倉庫 or '96'大分工場 or '98'大牟田工場 or 'F1'安田倉庫 or 'F2'足柄工場 or 'F3'熊本工場　荷主コード大：'00135' FFEM
#End If
                    prtBlc = New Com.Base.BaseBLC() {New LMC520BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC520InData2(ds)}
                Else
                    printType = "09"
                    prtBlc = New Com.Base.BaseBLC() {New LMC520BLC()}
                    setDs = New DataSet() {Me.SetDataSetLMC520InData2(ds)}
                End If

                'prtBlc = New Com.Base.BaseBLC() {New LMC520BLC()}
                ''START YANAI メモ②No.2
                ''setDs = New DataSet() {Me.SetDataSetLMC520InData(ds)}
                'setDs = New DataSet() {Me.SetDataSetLMC520InData2(ds)}
                ''END YANAI メモ②No.2

                '====2012/09/07 要望番号1416　出荷指図書(事務所用)出力対応 終了====

                'START YANAI 20120122 立会書印刷対応
            Case "10" '立会書
                '====2012/07/10 Notes749,911　立会書(LMC640)を呼ばないようにコメントアウト　開始====
                'If ("01").Equals(tachiaiFlg) = True Then
                '    prtBlc = New Com.Base.BaseBLC() {New LMC640BLC()}
                '    setDs = New DataSet() {Me.SetDataSetLMC640InData(ds)}
                'End If
                'END YANAI 20120122 立会書印刷対応
                '====2012/07/10 Notes749,911　立会書(LMC640)を呼ばないようにコメントアウト　終了====
                'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
            Case "11" 'まとめ荷札
                prtBlc = New Com.Base.BaseBLC() {New LMC550BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC550InData(ds, printType)}
                'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
            Case "12" '出荷報告(日付毎)20121220
                prtBlc = New Com.Base.BaseBLC() {New LMC614BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC614InData(ds)}

            Case "13" '纏め送状(チェックボックスのみ集約)

                prtBlc = New Com.Base.BaseBLC() {New LMC560BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC560InData_Chk(ds, printType)}
                'setDs = New DataSet() {Me.SetDataSetLMC560InData(ds, printType)}

            Case "14" 'ITW荷札

                prtBlc = New Com.Base.BaseBLC() {New LMC550BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC550InData(ds, printType)}


            Case PRINT_TYPE.PACKAGE_DETAILS


                prtBlc = New Com.Base.BaseBLC() {New LMC651BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC651InData(ds, printType)}

#If True Then   'ADD 2018/07/24 依頼番号 : 001540  
            Case PRINT_TYPE.UNSO_HOKEN
                prtBlc = New Com.Base.BaseBLC() {New LMC870BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC870InData(ds, printType)}

#End If
            Case PRINT_TYPE.ATTEND
                prtBlc = New Com.Base.BaseBLC() {New LMC641BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC641InData(ds, printType)}

            Case PRINT_TYPE.OUTBOUND_CHECK
                prtBlc = New Com.Base.BaseBLC() {New LMC642BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC642InData(ds, printType)}

#If True Then   'ADD 2023/03/29 送品案内書(FFEM)追加
            Case PRINT_TYPE.SHIPMENTGUIDE, PRINT_TYPE.SHIPMENTGUIDE_EX
                prtBlc = New Com.Base.BaseBLC() {New LMC543BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC543InData(ds, printType)}
#End If

            Case PRINT_TYPE.DOKU_JOJU, PRINT_TYPE.DOKU_JOJU_EX
                prtBlc = New Com.Base.BaseBLC() {New LMC901BLC()}
                setDs = New DataSet() {Me.SetDataSetLMC901InData(ds, printType)}

        End Select

        If prtBlc Is Nothing = True Then
            Return ds
        End If

        'START Kurihara WIT対応 
        If "05".Equals(printType) Then
            '纏めピック番号採番処理　設計書項番27-Ⅲ-6
            Me.SetMtmPickNo(ds)
        End If
        'END Kurihara WIT対応 


        Dim max As Integer = prtBlc.Count - 1
        Dim rtnDs As DataSet = Nothing

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        For i As Integer = 0 To max

            If setDs Is Nothing = True Then
                Continue For
            End If
            Dim strdate As Date = Now
            Dim strtime As Long = CLng(strdate.Hour.ToString.PadLeft(2, CChar("0")) & strdate.Minute.ToString.PadLeft(2, CChar("0")) & strdate.Second.ToString.PadLeft(2, CChar("0")) & strdate.Millisecond.ToString.PadLeft(3, CChar("0")))
            MyBase.Logger.WriteLog(0, "LMC010BLF", String.Concat("印刷 ", i + 1, "件目"), "☆☆開始時間：" & Format(strdate, "yyyyMMdd") & " " & strtime)

            setDs(i).Merge(New RdPrevInfoDS)


            'BP速度改善(一括引当処理で出荷指示書がでない場合、印刷処理を行わない)
            If printType.Equals("09") Then
                rtnDs = MyBase.CallBLC(prtBlc(i), "SelectMPrt", setDs(i))
            Else
                rtnDs = Nothing
            End If

            If printType.Equals("09") AndAlso String.IsNullOrEmpty(rtnDs.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString()) = True Then

            Else
                'FFEM一括印刷経由の出荷指示印刷
                If printType.Equals("09EX") = True Then
                    rtnDs = MyBase.CallBLC(prtBlc(i), "DoPrint2", setDs(i))
#If True Then   'ADD 2023/03/29 送品案内書(FFEM)追加
                ElseIf printType.Equals(PRINT_TYPE.SHIPMENTGUIDE_EX) = True Then
                    rtnDs = MyBase.CallBLC(prtBlc(i), "DoPrint2", setDs(i))
#End If
                ElseIf printType.Equals(PRINT_TYPE.DOKU_JOJU_EX) = True Then
                    rtnDs = MyBase.CallBLC(prtBlc(i), "DoPrint2", setDs(i))
                Else
                    rtnDs = MyBase.CallBLC(prtBlc(i), "DoPrint", setDs(i))
                End If

            End If

            rdPrevDt.Merge(setDs(i).Tables(LMConst.RD))

            Dim enddate As Date = Now
            Dim endtime As Long = CLng(enddate.Hour.ToString.PadLeft(2, CChar("0")) & enddate.Minute.ToString.PadLeft(2, CChar("0")) & enddate.Second.ToString.PadLeft(2, CChar("0")) & enddate.Millisecond.ToString.PadLeft(3, CChar("0")))
            MyBase.Logger.WriteLog(0, "LMC010BLF", String.Concat("印刷 ", i + 1, "件目"), "☆☆終了時間：" & Format(enddate, "yyyyMMdd") & " " & endtime)
            MyBase.Logger.WriteLog(0, "LMC010BLF", String.Concat("印刷 ", i + 1, "件目"), "☆☆経過時間：" & endtime - strtime & "ﾐﾘ秒")

        Next

        rtnDs.Tables(LMConst.RD).Clear()
        rtnDs.Tables(LMConst.RD).Merge(rdPrevDt)

        Return rtnDs

    End Function

    ''' <summary>
    ''' 纏めピックワークに登録用データを生成します。
    ''' </summary>
    ''' <param name="ds">データ導出元のデータセット　設計書項番5-bで取得したもの</param>
    ''' <remarks>
    ''' 纏めピッキングワークにデータ登録
    ''' 対象の出荷管理番号Lを inputDataSet に設定
    ''' </remarks>
    Private Sub SetMtmPickNo(ByVal ds As DataSet)

        ' 改ページ条件と同一の纏めピック番号採番要否判定フィルタ
        Const WorkDataFilter As String = "OUTKA_PLAN_DATE = '{0}' " & _
                                         "AND TOU_NO      = '{1}' " & _
                                         "AND SITU_NO     = '{2}' " & _
                                         "AND ZONE_CD     = '{3}' " & _
                                         "AND LOCA        = '{4}' " & _
                                         "AND CUST_CD_L   = '{5}' " & _
                                         "AND MTM_PICK_NO <> '' "

        ' 纏めピッキングWKへの登録・削除判定フィルタ
        Const EntryJudgeDataFilter As String = _
                                         "OUTKA_NO_L      = '{0}' " & _
                                         "AND OUTKA_NO_M  = '{1}' " & _
                                         "AND OUTKA_NO_S  = '{2}' "

        ' 同じOUTKA_NO_Lを複数回処理しない為の情報保持
        Dim usedOutkaNoL As Dictionary(Of String, List(Of String)) = New Dictionary(Of String, List(Of String))

        ' 複数チェック時に対応する出荷管理番号L単位のループ
        For Each targetRow As DataRow In ds.Tables("LMC010IN_OUTKA_L").Rows

            ' WK作成対象荷主かどうかのチェック
            Dim chkDs As DataSet = ds.Copy
            chkDs.Tables("LMC010IN_OUTKA_L").Clear()
            chkDs.Tables("LMC010IN_OUTKA_L").ImportRow(targetRow)
            If IsMatomePickTargetCustHandy(chkDs) = False Then
                Continue For
            End If

            ' 同じOUTKA_NO_Lを複数回処理しない為の制御 (Start)
            Dim nrsBrCd As String = targetRow("NRS_BR_CD").ToString
            Dim outkaNoL As String = targetRow("OUTKA_NO_L").ToString
            If Not usedOutkaNoL.ContainsKey(nrsBrCd) Then
                usedOutkaNoL.Add(nrsBrCd, New List(Of String))
            End If
            If usedOutkaNoL(nrsBrCd).Contains(outkaNoL) Then Continue For
            usedOutkaNoL(nrsBrCd).Add(outkaNoL)
            ' 同じOUTKA_NO_Lを複数回処理しない為の制御 (End)

            '追加/削除判定を実施
            Dim entryJudgeInDs As LMC010DS = New LMC010DS
            entryJudgeInDs.LMC010IN_OUTKA_L.ImportRow(targetRow)
            Dim entryJudgeDs As DataSet = Me.SelectEntryJudge(entryJudgeInDs)
            Dim entryJudgeDt As DataTable = entryJudgeDs.Tables("LMC010OUT_ENTRY_JUDGE")

            ' LMC690纏めピック用データを取得
            Dim lmc690dsOut As DataSet = GetLMC690MtmPick(targetRow)

            Dim mtmPickRows() As DataRow = Nothing ' 纏めピッキング番号を保有するデータ行の配列を格納

            '印刷対象データごとの処理
            For Each lmc690out As DataRow In lmc690dsOut.Tables("LMC690OUT").Rows

                '商品管理番号がブランクまたは、再発行番号フォーマットに該当する行はWKを作成しない
                Dim goodsKanriNo As String = lmc690out("GOODS_KANRI_NO").ToString
                If String.IsNullOrEmpty(goodsKanriNo) OrElse Regex.IsMatch(goodsKanriNo, "^Z[0-9]+$") Then
                    Continue For
                End If

                '対象の出荷管理番号Lをもつワークデータを取得
                Dim mtmPickDs As DataSet = Me.SelectMtmPickList(ds)

                '同じ出荷予定日、棟番号、荷主コード、運送会社コードを持つレコードを抽出
                mtmPickRows = mtmPickDs.Tables("LMC010OUT_NUMBERING").Select( _
                                  String.Format(WorkDataFilter, _
                                                lmc690out("OUTKA_PLAN_DATE"), _
                                                lmc690out("TOU_NO"), _
                                                lmc690out("SITU_NO"), _
                                                lmc690out("ZONE_CD"), _
                                                lmc690out("LOCA"), _
                                                lmc690out("CUST_CD_L")))

                '纏めピッキング番号の決定
                Dim mtmPickNo As String = String.Empty
                If mtmPickRows IsNot Nothing AndAlso 0 < mtmPickRows.Length Then
                    '対象の出荷管理番号L、出荷荷主、運送会社、棟、出荷日を持つワークデータが存在
                    Dim mtmPickRow As DataRow = mtmPickRows(0)
                    mtmPickNo = mtmPickRow("MTM_PICK_NO").ToString
                Else
                    '存在しない場合は、纏めピッキング番号を新規採番する
                    mtmPickNo = Me.GetMatomePickNo(ds)
                End If

                '纏めピッキングワークデータを作成
                Dim workDs As LMC010DS = New LMC010DS
                workDs.LMC010IN_OUTKA_L.ImportRow(targetRow)
                Dim workDt As DataTable = workDs.Tables("LMC010_C_TOTAL_PIC_WK")
                Dim workRow As DataRow = workDt.NewRow
                workRow("NRS_BR_CD") = lmc690out("NRS_BR_CD").ToString
                workRow("OUTKA_NO_L") = lmc690out("OUTKA_NO_L").ToString
                workRow("OUTKA_NO_M") = lmc690out("OUTKA_NO_M").ToString
                workRow("OUTKA_NO_S") = lmc690out("OUTKA_NO_S").ToString
                workRow("GOODS_KANRI_NO") = lmc690out("GOODS_KANRI_NO").ToString
                workRow("OUTKA_TTL_NB") = lmc690out("ALCTD_NB").ToString
                workRow("NB_UT") = lmc690out("NB_UT").ToString
                workRow("GOODS_CD_CUST") = lmc690out("GOODS_CD_CUST").ToString
                workRow("GOODS_NM") = lmc690out("GOODS_NM_1").ToString
                workRow("LOT_NO") = lmc690out("LOT_NO").ToString
                workRow("IRIME_NB") = lmc690out("IRIME").ToString
                workRow("IRIME_UT") = lmc690out("IRIME_UT").ToString
                workRow("CHK_TANI") = lmc690out("CHK_TANI").ToString

                '取得した纏めピッキング番号を適用
                workRow("MTM_PICK_NO") = mtmPickNo
                workDt.Rows.Add(workRow)
                ds.Tables("LMC010_C_TOTAL_PIC_WK").ImportRow(workRow)
                Dim entryJudgeFilter As String = String.Format(EntryJudgeDataFilter, workRow("OUTKA_NO_L"), workRow("OUTKA_NO_M"), workRow("OUTKA_NO_S"))
                For Each entryJudgeRow As DataRow In entryJudgeDt.Select(entryJudgeFilter)
                    Dim hasTrn As Boolean = ("1").Equals(entryJudgeRow("TRN_FLG").ToString)
                    Dim hasWk As Boolean = ("1").Equals(entryJudgeRow("WK_FLG").ToString)

                    If hasTrn AndAlso hasWk Then
                        '共に存在　⇒　前回出力時から変化なし　
                        'ワークデータを保持する。（修正なし）
                        Continue For
                    End If

                    Dim filter As String = "NRS_BR_CD = '{0}' AND OUTKA_NO_L = '{1}' AND OUTKA_NO_M = '{2}' AND OUTKA_NO_S = '{3}'"

                    'LMC010_C_TOTAL_PIC_WK
                    Dim rows As DataRow() = _
                        workDs.Tables("LMC010_C_TOTAL_PIC_WK").Select(String.Format(filter, _
                                                                            entryJudgeRow("NRS_BR_CD"), _
                                                                            entryJudgeRow("OUTKA_NO_L"), _
                                                                            entryJudgeRow("OUTKA_NO_M"), _
                                                                            entryJudgeRow("OUTKA_NO_S")))
                    '対象のデータが存在しない
                    If rows.Count = 0 Then
                        Continue For
                    End If

                    Dim entryDs As LMC010DS = New LMC010DS
                    For Each row As DataRow In rows
                        entryDs.LMC010_C_TOTAL_PIC_WK.ImportRow(row)
                    Next

                    If hasWk Then
                        'ワークのみに存在　⇒ TRNからデータを削除している。
                        'ワークデータを削除する（Delete）
                        Me.DeleteCTotalPicWk(entryDs)
                    ElseIf hasTrn Then
                        'トランのみに存在　⇒ 新たに出荷Sに追加された
                        'ワークデータに追加する（Insert）
                        Me.InsertCTotalPicWk(entryDs)
                    End If

                Next
            Next
        Next
    End Sub

    ''' <summary>
    ''' LMC690纏めピック用データを取得（BPカストロール用）
    ''' </summary>
    ''' <param name="lmc010InOutkaLRow"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetLMC690MtmPick(ByVal lmc010InOutkaLRow As DataRow) As DataSet

        '690を取得
        Dim lmc690blc As LMC690BLC = New LMC690BLC
        Dim lmc690ds As LMC690DS = New LMC690DS

        Dim lmc690in As LMC690DS.LMC690INDataTable = lmc690ds.LMC690IN
        Dim lmc690inRow As LMC690DS.LMC690INRow = lmc690in.NewLMC690INRow

        lmc690inRow.NRS_BR_CD = lmc010InOutkaLRow("NRS_BR_CD").ToString
        lmc690inRow.OUTKA_NO_L = lmc010InOutkaLRow("OUTKA_NO_L").ToString
        lmc690inRow.ROW_COUNT = lmc010InOutkaLRow("ROW_COUNT").ToString
        lmc690in.AddLMC690INRow(lmc690inRow)

        Dim mprt As LMC690DS.M_RPTDataTable = lmc690ds.M_RPT
        Dim mprtRow As LMC690DS.M_RPTRow = mprt.NewM_RPTRow
        mprtRow.RPT_ID = "LMC697"
        mprt.AddM_RPTRow(mprtRow)

        Dim lmc690dsOut As DataSet = MyBase.CallBLC(lmc690blc, "SelectPrintData", lmc690ds)

        Return lmc690dsOut
    End Function

    'START YANAI 要望番号1274 一括引当時の出荷指示書印刷で棟班別出力を行う
    '''' <summary>
    '''' LMC520DSを生成
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function SetDataSetLMC520InData(ByVal ds As DataSet) As DataSet

    '    'START YANAI 要望番号394
    '    'If "01".Equals(ds.Tables("LMC010IN_OUTKA_L").Rows(0).Item("OUTKA_SASHIZU_PRT_YN").ToString()) = False Then
    '    '    Return Nothing
    '    'End If
    '    'END YANAI 要望番号394

    '    Return Me.SetDataSetLMCInData(ds, New LMC520DS(), "LMC520IN")

    'End Function
    'END YANAI 要望番号1274 一括引当時の出荷指示書印刷で棟班別出力を行う

    'START YANAI メモ②No.2
    ''' <summary>
    ''' LMC520DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC520InData2(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMC520DS(), "LMC520IN")

    End Function
    'END YANAI メモ②No.2

    '''' <summary>
    '''' 分析表の
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function SetDataSetLMC500InData(ByVal ds As DataSet) As DataSet

    '    Return Me.SetDataSetLMCInData(ds, New LMC500DS(), "LMC500IN")

    'End Function

    'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
    '''' <summary>
    '''' LMC560DSを生成
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function SetDataSetLMC560InData(ByVal ds As DataSet) As DataSet

    '    Return Me.SetDataSetLMCInData(ds, New LMC560DS(), "LMC560IN")

    'End Function
    ''' <summary>
    ''' LMC560DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC560InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMC560DS(), "LMC560IN", "OUTKA_NO_L", printType)

    End Function
    'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)

    ''' <summary>
    ''' LMC500DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC500InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMC500DS(), "LMC500IN", "KANRI_NO_L", printType)

    End Function

    ''' <summary>
    ''' LMC550DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC550InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        'START UMANO メモ(EDI)No.49
        Return Me.SetDataSetLMCInData(ds, New LMC550DS(), "LMC550IN", "OUTKA_NO_L", printType)
        'END UMANO メモ(EDI)No.49

    End Function

    ''' <summary>
    ''' LMC610DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC610InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMC610DS(), "LMC610IN")

    End Function

    'yamanaka 2012.08.06 Start
    ''' <summary>
    ''' LMC690DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC690InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMC690InData(ds, New LMC690DS(), "LMC690IN")

    End Function
    'yamanaka 2012.08.06 End

    '====2012/07/10 Notes749,911　立会書(LMC640)を呼ばないようにコメントアウト 開始====
    ''START YANAI 20120122 立会書印刷対応
    '''' <summary>
    '''' LMC640DSを生成
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function SetDataSetLMC640InData(ByVal ds As DataSet) As DataSet

    '    Return Me.SetDataSetLMCInData(ds, New LMC640DS(), "LMC640IN")

    'End Function

    'END YANAI 20120122 立会書印刷対応
    '====2012/07/10 Notes749,911　立会書(LMC640)を呼ばないようにコメントアウト 終了====

    ''' <summary>
    ''' LMC540DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC540InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMC540DS(), "LMC540IN")

    End Function

    ''' <summary>
    ''' LMC541DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC541InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMC541DS(), "LMC541IN")

    End Function

    'START>LMC614対応開始(INデータマージ版DS)-2012/12/21▼▼▼
    ''' <summary>
    ''' LMC610DS(INデータマージ版)を生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC614InData(ByVal ds As DataSet) As DataSet

        Return Me.SetDataSetLMCInData_MA(ds, New LMC614DS(), "LMC614IN")

    End Function
    '▲▲▲LMC対応終了(INデータマージ版DS)-2012/12/21<TOEND

    'START>LMC560対応開始(INデータマージ版DS)-2013/01/24▼▼▼
    ''' <summary>
    ''' LMC560DS(INデータマージ版)を生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC560InData_Chk(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMCInData_MA(ds, New LMC560DS(), "LMC560IN", "OUTKA_NO_L", printType)

    End Function
    '▲▲▲LMC560対応終了(INデータマージ版DS)-2013/01/24<TOEND

    ''' <summary>
    ''' LMC794DSを生成SetDataSetLMC500InData
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC794InData(ByVal ds As DataSet, ByVal printType As String) As DataSet

        Return Me.SetDataSetLMCInData(ds, New LMC794DS(), "LMC794IN", "OUTKA_NO_L", printType)

    End Function

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="colNm">列名　初期値 = "OUTKA_NO_L"</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    '''         'START UMANO メモ(EDI)No.49
    Private Function SetDataSetLMCInData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String, _
                                         Optional ByVal colNm As String = "OUTKA_NO_L", Optional ByVal printType As String = "01") As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim setDr As DataRow = ds.Tables("LMC010IN_OUTKA_L").Rows(0)
        dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
        dr.Item(colNm) = setDr.Item("OUTKA_NO_L").ToString()

        'START YANAI 20110914 一括引当対応
        'dr.Item("CUST_CD_L") = setDr.Item("CUST_CD_L").ToString()
        'dr.Item("CUST_CD_M") = setDr.Item("CUST_CD_M").ToString()
        'END YANAI 20110914 一括引当対応

        '倉庫コードの列があれば値を格納（中部物流センター判定用に追加した列）
        If dt.Columns.Contains("WH_CD") Then
            dr.Item("WH_CD") = setDr.Item("WH_CD").ToString()
        End If

        If ("LMC500IN").Equals(tblNm) = True Then
            dr.Item("PTN_FLAG") = "0"
            '(2012.03.03) 再発行フラグ制御追加 LMC513対応 -- START --
            Dim outDr() As DataRow = ds.Tables("LMC010IN_OUTKA_L").Select(String.Concat("OUTKA_NO_L = '", setDr.Item("OUTKA_NO_L").ToString(), "'"))
            If 0 < outDr.Length Then
                If outDr(0).Item("NHS_FLAG").ToString() = "00" Then
                    dr.Item("SAIHAKKO_FLG") = "0"
                Else
                    dr.Item("SAIHAKKO_FLG") = "1"
                End If
            End If
            '(2012.03.03) 再発行フラグ制御追加 LMC513対応 --  END --
        End If

        'START YANAI メモ②No.2
        If ("LMC520IN").Equals(tblNm) = True Then
            dr.Item("TOU_BETU_FLG") = "0"
            'END YANAI 要望番号1274 一括引当時の出荷指示書印刷で棟班別出力を行う
            Dim outDr() As DataRow = ds.Tables("LMC010_OUTKA_M_IN").Select(String.Concat("OUTKA_NO_L = '", setDr.Item("OUTKA_NO_L").ToString(), "'"))
            If 0 < outDr.Length Then
                If String.IsNullOrEmpty(outDr(0).Item("SASZ_USER").ToString()) = False Then
                    dr.Item("SAIHAKKO_FLG") = "1"
                Else
                    dr.Item("SAIHAKKO_FLG") = "0"
                End If
                'START YANAI 要望番号1274 一括引当時の出荷指示書印刷で棟班別出力を行う
                If ("1").Equals(outDr(0).Item("TOU_BETU_FLG").ToString()) = True Then
                    dr.Item("TOU_BETU_FLG") = "1"
                End If
                'END YANAI 要望番号1274 一括引当時の出荷指示書印刷で棟班別出力を行う

#If True Then 'ADD 2018/08/24 依頼番号 : 002114   【LMS】出荷指示書(LMC529)を出荷データ検索で再印刷するとタイトルが非表示
            Else
                Dim drOUTKA_L As DataRow = ds.Tables("LMC010IN_OUTKA_L").Rows(0)
                If String.IsNullOrEmpty(drOUTKA_L.Item("SASZ_USER").ToString()) = False Then
                    dr.Item("SAIHAKKO_FLG") = "1"
                Else
                    dr.Item("SAIHAKKO_FLG") = "0"
                End If
#End If

            End If

            '====2012/09/07 要望番号1416　出荷指図書(事務所用)出力対応 開始====
            If ("30").Equals(setDr.Item("NRS_BR_CD").ToString) = True AndAlso _
                ("00021").Equals(setDr.Item("CUST_CD_L").ToString) = True Then
                dr.Item("PRT_NB") = 3
            End If
            '====2012/09/07 要望番号1416　出荷指図書(事務所用)出力対応 終了====

        End If
        'END YANAI メモ②No.2

        'START UMANO メモ(EDI)No.49
        If ("LMC550IN").Equals(tblNm) = True Then
            'START UMANO メモ(EDI)No.49(再修正)
            'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
            'If printType.Equals("08") = True OrElse printType.Equals("01") = True Then
            If printType.Equals("08") = True OrElse printType.Equals("01") = True _
               OrElse printType.Equals("11") = True OrElse printType.Equals("14") = True Then
                'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
                dr.Item("PRT_NB") = setDr.Item("OUTKA_PKG_NB").ToString()
                dr.Item("PRT_NB_FROM") = "1"
                dr.Item("PRT_NB_TO") = setDr.Item("OUTKA_PKG_NB").ToString()
            End If
        End If
        'END UMANO メモ(EDI)No.49

        'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
        If ("LMC550IN").Equals(tblNm) = True Then
            If printType.Equals("11") = True Then
                'まとめ荷札
                dr.Item("MATOME_FLG") = "1"
                '要望番号1961 20130322 まとめ荷札対応(BPC対応) 修正START
                dr.Item("MATOME_DEST_KBN") = setDr.Item("MATOME_DEST_KBN").ToString()
                '要望番号1961 20130322 まとめ荷札対応(BPC対応) 修正END
            Else
                dr.Item("MATOME_FLG") = "0"
            End If
        End If

        If ("LMC560IN").Equals(tblNm) = True Then
            If printType.Equals("06") = True Then
                'まとめ送状
                dr.Item("MATOME_FLG") = "1"
            Else
                dr.Item("MATOME_FLG") = "0"
            End If
        End If
        'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)

        'START YANAI 要望番号800
        'dt.Rows.Add(dr)
        If ("LMC550IN").Equals(tblNm) = True Then
            If ("0").Equals(dr.Item("PRT_NB").ToString()) = False Then
                '荷札の部数0の時は、INの追加処理をしない
                dt.Rows.Add(dr)
            End If
        Else
            dt.Rows.Add(dr)
        End If
        'END YANAI 要望番号800

        'START s.kobayashi 要望番号1787
        If ("LMC550IN").Equals(tblNm) = True AndAlso printType.Equals("14") = True Then
            dr.Item("TOKUSHU_KBN") = "1"
            dr.Item("TOKUSHU_RPT_ID") = "LMC557"
        End If
        'END s.kobayashi 要望番号8178700

#If True Then       'ADD 20018/08/20 依頼番号 : 001540  
        If printType.Equals("21") = True Then
            '運送保険選択時
            dr.Item("PRT_PTN") = printType.ToString

        End If

#End If

#If True Then   'ADD 2019/01/24 依頼番号 : 002596   【LMS】大阪日本たばこ産業_運送保険料は引取りは運送保険申込書出力対象外にする
        If ("LMC870IN").Equals(tblNm) = True Then
            If printType.Equals("08") = True Then
                '一括時
                dr.Item("UNSO_TEHAI_CHK") = LMConst.FLG.ON
            Else
                '運送保険指定時
                'UPD 2022/01/26 026303 【LMS】運送保険料システム化_実装_運送保険申込書対応_出荷
                'dr.Item("UNSO_TEHAI_CHK") = LMConst.FLG.OFF
                dr.Item("UNSO_TEHAI_CHK") = LMConst.FLG.ON
            End If
        End If
#End If
#If True Then   'ADD 2022/04/25 依頼番号 : 028723 千葉BC　一括印刷で印刷する帳票について
        If ("LMC500IN").Equals(tblNm) = True Then
            If printType.Equals("08") = True _
                AndAlso ("10").Equals(setDr.Item("NRS_BR_CD").ToString()) Then
                '千葉の一括時
                dr.Item("IKKATU_FLG") = LMConst.FLG.ON
            Else
                dr.Item("IKKATU_FLG") = LMConst.FLG.OFF
            End If
        End If
#End If
        Return inDs
    End Function

    'START>LMC614対応開始(INデータマージ版DS)-2012/12/21▼▼▼
    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="colNm">列名　初期値 = "OUTKA_NO_L"</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    '''         'START UMANO メモ(EDI)No.49
    Private Function SetDataSetLMCInData_MA(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String, _
                                         Optional ByVal colNm As String = "OUTKA_NO_L", Optional ByVal printType As String = "01") As DataSet
        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = Nothing
        Dim setDr As DataRow() = ds.Tables("LMC010IN_OUTKA_L").Select

        For i As Integer = 0 To setDr.Length - 1
            dr = dt.NewRow

            dr.Item("NRS_BR_CD") = setDr(i).Item("NRS_BR_CD").ToString()
            dr.Item(colNm) = setDr(i).Item("OUTKA_NO_L").ToString()

            If ("LMC560IN").Equals(tblNm) = True Then
                If printType.Equals("13") = True Then
                    'まとめ送状
                    dr.Item("MATOME_FLG") = "1"
                Else
                    dr.Item("MATOME_FLG") = "0"
                End If

#If True Then       'ADD 20018/08/07 依頼番号 : 001572  
                If printType.Equals("13") = True Then
                    ''纏め送状(選択)の場合
                    dr.Item("MATOME_SELECT_FLG") = "1"
                Else
                    dr.Item("MATOME_SELECT_FLG") = "0"
                End If

#End If
            End If

            'InDsデータテーブルへ取得Rowアイテムの格納
            dt.Rows.Add(dr)
        Next

        '以下不要・・・念のために保留　2012/12/21
        'START YANAI 20110914 一括引当対応
        'dr.Item("CUST_CD_L") = setDr.Item("CUST_CD_L").ToString()
        'dr.Item("CUST_CD_M") = setDr.Item("CUST_CD_M").ToString()
        'END YANAI 20110914 一括引当対応

        'If ("LMC500IN").Equals(tblNm) = True Then
        '    dr.Item("PTN_FLAG") = "0"
        '    (2012.03.03) 再発行フラグ制御追加 LMC513対応 -- START --
        '    Dim outDr() As DataRow = ds.Tables("LMC010IN_OUTKA_L").Select(String.Concat("OUTKA_NO_L = '", setDr.Item("OUTKA_NO_L").ToString(), "'"))
        '    If 0 < outDr.Length Then
        '        If outDr(0).Item("NHS_FLAG").ToString() = "00" Then
        '            dr.Item("SAIHAKKO_FLG") = "0"
        '        Else
        '            dr.Item("SAIHAKKO_FLG") = "1"
        '        End If
        '    End If
        '    (2012.03.03) 再発行フラグ制御追加 LMC513対応 --  END --
        'End If

        'START YANAI メモ②No.2
        'If ("LMC520IN").Equals(tblNm) = True Then
        '    dr.Item("TOU_BETU_FLG") = "0"
        '    END YANAI 要望番号1274 一括引当時の出荷指示書印刷で棟班別出力を行う
        '    Dim outDr() As DataRow = ds.Tables("LMC010_OUTKA_M_IN").Select(String.Concat("OUTKA_NO_L = '", setDr.Item("OUTKA_NO_L").ToString(), "'"))
        '    If 0 < outDr.Length Then
        '        If String.IsNullOrEmpty(outDr(0).Item("SASZ_USER").ToString()) = False Then
        '            dr.Item("SAIHAKKO_FLG") = "1"
        '        Else
        '            dr.Item("SAIHAKKO_FLG") = "0"
        '        End If
        '        START YANAI 要望番号1274 一括引当時の出荷指示書印刷で棟班別出力を行う
        '        If ("1").Equals(outDr(0).Item("TOU_BETU_FLG").ToString()) = True Then
        '            dr.Item("TOU_BETU_FLG") = "1"
        '        End If
        '        END YANAI 要望番号1274 一括引当時の出荷指示書印刷で棟班別出力を行う
        '    End If

        '    ====2012/09/07 要望番号1416　出荷指図書(事務所用)出力対応 開始====
        'If ("30").Equals(setDr.Item("NRS_BR_CD").ToString) = True AndAlso _
        '    ("00021").Equals(setDr.Item("CUST_CD_L").ToString) = True Then
        '    dr.Item("PRT_NB") = 3
        'End If
        '    ====2012/09/07 要望番号1416　出荷指図書(事務所用)出力対応 終了====

        'End If
        'END YANAI メモ②No.2

        'START UMANO メモ(EDI)No.49
        'If ("LMC550IN").Equals(tblNm) = True Then
        '    START UMANO メモ(EDI)No.49(再修正)
        '    START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
        '    If printType.Equals("08") = True OrElse printType.Equals("01") = True Then
        '        If printType.Equals("08") = True OrElse printType.Equals("01") = True OrElse printType.Equals("11") = True Then
        '        END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
        '            dr.Item("PRT_NB") = setDr.Item("OUTKA_PKG_NB").ToString()
        '        End If
        '    End If
        'END UMANO メモ(EDI)No.49

        'START YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)
        '    If ("LMC550IN").Equals(tblNm) = True Then
        '        If printType.Equals("11") = True Then
        '            まとめ荷札()
        '            dr.Item("MATOME_FLG") = "1"
        '        Else
        '            dr.Item("MATOME_FLG") = "0"
        '        End If
        '    End If

        '    If ("LMC560IN").Equals(tblNm) = True Then
        '        If printType.Equals("06") = True Then
        '            まとめ送状()
        '            dr.Item("MATOME_FLG") = "1"
        '        Else
        '            dr.Item("MATOME_FLG") = "0"
        '        End If
        '    End If
        'END YANAI 20120615 まとめ荷札・送状対応(埼玉浮間対応)

        'START YANAI 要望番号800
        '    dt.Rows.Add(dr)
        '    If ("LMC550IN").Equals(tblNm) = True Then
        '        If ("0").Equals(dr.Item("PRT_NB").ToString()) = False Then
        '        荷札の部数0の時は、INの追加処理をしない
        '            dt.Rows.Add(dr)
        '        End If
        '    Else
        '        dt.Rows.Add(dr)
        '    End If
        'END YANAI 要望番号800

        Return inDs

    End Function
    '▲▲▲LMC614対応終了(INデータマージ版DS)-2012/12/21<TOEND

    '====2012/09/07 要望番号1416　出荷指図書(事務所用)出力対応 開始====
    ''' <summary>
    ''' LMC531DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC531InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = New LMC531DS
        Dim dt As DataTable = inDs.Tables("LMC531IN")
        Dim dr As DataRow = Nothing
        Dim setDr As DataRow = Nothing

        For i As Integer = 0 To ds.Tables("LMC010IN_OUTKA_L").Rows.Count - 1
            dr = dt.NewRow()
            setDr = ds.Tables("LMC010IN_OUTKA_L").Rows(i)
            dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
            dr.Item("OUTKA_NO_L") = setDr.Item("OUTKA_NO_L").ToString()

            '一括引当時、群馬物流センター 篠崎運送(00021)の場合は1部出力
            dr.Item("PRT_NB") = 0
            If ("30").Equals(setDr.Item("NRS_BR_CD").ToString) = True AndAlso _
                ("00021").Equals(setDr.Item("CUST_CD_L").ToString) = True Then
                dr.Item("PRT_NB") = 1
            ElseIf ("30").Equals(setDr.Item("NRS_BR_CD").ToString) = True AndAlso _
                (("00072").Equals(setDr.Item("CUST_CD_L").ToString) = True OrElse ("00076").Equals(setDr.Item("CUST_CD_L").ToString) = True) Then
                '一括引当時、群馬物流センター DIC運送(00072,00076)の場合は2部出力
                dr.Item("PRT_NB") = 2
            End If

            dr.Item("TOU_BETU_FLG") = "0"
            Dim outDr() As DataRow = ds.Tables("LMC010_OUTKA_M_IN").Select(String.Concat("OUTKA_NO_L = '", setDr.Item("OUTKA_NO_L").ToString(), "'"))
            If 0 < outDr.Length Then
                If String.IsNullOrEmpty(outDr(0).Item("SASZ_USER").ToString()) = False Then
                    dr.Item("SAIHAKKO_FLG") = "1"
                Else
                    dr.Item("SAIHAKKO_FLG") = "0"
                End If
                If ("1").Equals(outDr(0).Item("TOU_BETU_FLG").ToString()) = True Then
                    dr.Item("TOU_BETU_FLG") = "1"
                End If
            End If

            dt.Rows.Add(dr)

        Next

        Return inDs

    End Function
    '====2012/09/07 要望番号1416　出荷指図書(事務所用)出力対応 終了====

    ''' <summary>
    ''' 印刷時に使用するDataSetを生成(LMC690)ピッキングリスト用
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC690InData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = Nothing
        Dim setDr As DataRow = Nothing
        For i As Integer = 0 To ds.Tables("LMC010IN_OUTKA_L").Rows.Count - 1
            dr = dt.NewRow()
            setDr = ds.Tables("LMC010IN_OUTKA_L").Rows(i)
            dr.Item("NRS_BR_CD") = setDr.Item("NRS_BR_CD").ToString()
            dr.Item("OUTKA_NO_L") = setDr.Item("OUTKA_NO_L").ToString()

            '2012.12.10 yamanaka : 埼玉BP・カストロール対応 Start
            dr.Item("ROW_COUNT") = setDr.Item("ROW_COUNT").ToString()
            '2012.12.10 yamanaka : 埼玉BP・カストロール対応 End

            '埼玉BP・カストロール修正対応 yamanaka 2012.01.22 Start
            dr.Item("ROW_NO") = setDr.Item("ROW_NO").ToString()
            '埼玉BP・カストロール修正対応 yamanaka 2012.01.22 End

            dt.Rows.Add(dr)

        Next

        Return inDs

    End Function

    'START YANAI 要望番号627　こぐまくん対応
    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetCsvInData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim max As Integer = ds.Tables("LMC010IN_CSV").Rows.Count - 1

        For i As Integer = 0 To max
            dr.Item("NRS_BR_CD") = ds.Tables("LMC010IN_CSV").Rows(i).Item("NRS_BR_CD").ToString
            dr.Item("OUTKA_NO_L") = ds.Tables("LMC010IN_CSV").Rows(i).Item("OUTKA_NO_L").ToString
            dr.Item("ROW_NO") = ds.Tables("LMC010IN_CSV").Rows(i).Item("ROW_NO").ToString
            dr.Item("FILEPATH") = ds.Tables("LMC010IN_CSV").Rows(i).Item("FILEPATH").ToString
            dr.Item("FILENAME") = ds.Tables("LMC010IN_CSV").Rows(i).Item("FILENAME").ToString
            dr.Item("SYS_DATE") = ds.Tables("LMC010IN_CSV").Rows(i).Item("SYS_DATE").ToString
            dr.Item("SYS_TIME") = ds.Tables("LMC010IN_CSV").Rows(i).Item("SYS_TIME").ToString

            dt.Rows.Add(dr)
            dr = dt.NewRow()
        Next

        Return inDs

    End Function





    'END YANAI 要望番号627　こぐまくん対応

    'ADD 2016/06/14  要望番号2575　出荷 ヤマトB2用CSV出力
    ''' <summary>
    ''' CSVに使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetYamatoB2CsvInData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim max As Integer = ds.Tables("LMC010IN_CSV").Rows.Count - 1

        For i As Integer = 0 To max
            dr.Item("NRS_BR_CD") = ds.Tables("LMC010IN_CSV").Rows(i).Item("NRS_BR_CD").ToString
            dr.Item("OUTKA_NO_L") = ds.Tables("LMC010IN_CSV").Rows(i).Item("OUTKA_NO_L").ToString
            dr.Item("ROW_NO") = ds.Tables("LMC010IN_CSV").Rows(i).Item("ROW_NO").ToString
            dr.Item("FILEPATH") = ds.Tables("LMC010IN_CSV").Rows(i).Item("FILEPATH").ToString
            dr.Item("FILENAME") = ds.Tables("LMC010IN_CSV").Rows(i).Item("FILENAME").ToString
            dr.Item("SYS_DATE") = ds.Tables("LMC010IN_CSV").Rows(i).Item("SYS_DATE").ToString
            dr.Item("SYS_TIME") = ds.Tables("LMC010IN_CSV").Rows(i).Item("SYS_TIME").ToString
            dr.Item("CSV_OUTFLG") = ds.Tables("LMC010IN_CSV").Rows(i).Item("CSV_OUTFLG").ToString   'ADD 2016/08/22
            dr.Item("CSV_OUTFLG2") = ds.Tables("LMC010IN_CSV").Rows(i).Item("CSV_OUTFLG2").ToString
            dr.Item("OUTKA_PLAN_DATE") = ds.Tables("LMC010IN_CSV").Rows(i).Item("OUTKA_PLAN_DATE").ToString   'ADD 2016/10/04

            dt.Rows.Add(dr)
            dr = dt.NewRow()
        Next

        Return inDs

    End Function

    'ADD 2016/06/14  要望番号2575　出荷 佐川e飛伝用CSV出力
    ''' <summary>
    ''' CSVに使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetSagawaEHidenCsvInData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim max As Integer = ds.Tables("LMC010IN_CSV").Rows.Count - 1

        For i As Integer = 0 To max
            dr.Item("NRS_BR_CD") = ds.Tables("LMC010IN_CSV").Rows(i).Item("NRS_BR_CD").ToString
            dr.Item("OUTKA_NO_L") = ds.Tables("LMC010IN_CSV").Rows(i).Item("OUTKA_NO_L").ToString
            dr.Item("ROW_NO") = ds.Tables("LMC010IN_CSV").Rows(i).Item("ROW_NO").ToString
            dr.Item("FILEPATH") = ds.Tables("LMC010IN_CSV").Rows(i).Item("FILEPATH").ToString
            dr.Item("FILENAME") = ds.Tables("LMC010IN_CSV").Rows(i).Item("FILENAME").ToString
            dr.Item("SYS_DATE") = ds.Tables("LMC010IN_CSV").Rows(i).Item("SYS_DATE").ToString
            dr.Item("SYS_TIME") = ds.Tables("LMC010IN_CSV").Rows(i).Item("SYS_TIME").ToString
            dr.Item("CSV_OUTFLG") = ds.Tables("LMC010IN_CSV").Rows(i).Item("CSV_OUTFLG").ToString   'ADD 2016/08/22

            dt.Rows.Add(dr)
            dr = dt.NewRow()
        Next

        Return inDs

    End Function

    'ADD 2017/02/24 　出荷 エスライン用CSV出力
    ''' <summary>
    ''' CSVに使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetSLineCsvInData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim max As Integer = ds.Tables("LMC010IN_CSV").Rows.Count - 1

        For i As Integer = 0 To max
            dr.Item("NRS_BR_CD") = ds.Tables("LMC010IN_CSV").Rows(i).Item("NRS_BR_CD").ToString
            dr.Item("OUTKA_NO_L") = ds.Tables("LMC010IN_CSV").Rows(i).Item("OUTKA_NO_L").ToString
            dr.Item("ROW_NO") = ds.Tables("LMC010IN_CSV").Rows(i).Item("ROW_NO").ToString
            dr.Item("FILEPATH") = ds.Tables("LMC010IN_CSV").Rows(i).Item("FILEPATH").ToString
            dr.Item("FILENAME") = ds.Tables("LMC010IN_CSV").Rows(i).Item("FILENAME").ToString
            dr.Item("SYS_DATE") = ds.Tables("LMC010IN_CSV").Rows(i).Item("SYS_DATE").ToString
            dr.Item("SYS_TIME") = ds.Tables("LMC010IN_CSV").Rows(i).Item("SYS_TIME").ToString

            dt.Rows.Add(dr)
            dr = dt.NewRow()
        Next

        Return inDs

    End Function

    'ADD 2018/07/10 依頼番号 : 001947   　出荷 ｶﾝｶﾞﾙｰﾏｼﾞｯｸ用CSV出力
    ''' <summary>
    ''' CSVに使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetKangarooMagicCsvInData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim max As Integer = ds.Tables("LMC010IN_CSV").Rows.Count - 1

        For i As Integer = 0 To max
            dr.Item("NRS_BR_CD") = ds.Tables("LMC010IN_CSV").Rows(i).Item("NRS_BR_CD").ToString
            dr.Item("OUTKA_NO_L") = ds.Tables("LMC010IN_CSV").Rows(i).Item("OUTKA_NO_L").ToString
            dr.Item("ROW_NO") = ds.Tables("LMC010IN_CSV").Rows(i).Item("ROW_NO").ToString
            dr.Item("FILEPATH") = ds.Tables("LMC010IN_CSV").Rows(i).Item("FILEPATH").ToString
            dr.Item("FILENAME") = ds.Tables("LMC010IN_CSV").Rows(i).Item("FILENAME").ToString
            dr.Item("SYS_DATE") = ds.Tables("LMC010IN_CSV").Rows(i).Item("SYS_DATE").ToString
            dr.Item("SYS_TIME") = ds.Tables("LMC010IN_CSV").Rows(i).Item("SYS_TIME").ToString

            dt.Rows.Add(dr)
            dr = dt.NewRow()
        Next

        Return inDs

    End Function

    'ADD 2016/06/14  要望番号2575　
    ''' <summary>
    ''' 出荷管理番号存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function CheckOutka_No_L(ByVal ds As DataSet) As DataSet

        '検索結果取得
        ds = MyBase.CallBLC(Me._Blc, "CheckOutka_No_L", ds)

        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function


    'START 中村　シグマ出荷対応 2012.11.19
    ''' <summary>
    ''' 印刷時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetSigmaInData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim max As Integer = ds.Tables("LMC010IN_SIGMA").Rows.Count - 1

        For i As Integer = 0 To max
            dr.Item("NRS_BR_CD") = ds.Tables("LMC010IN_SIGMA").Rows(i).Item("NRS_BR_CD").ToString
            dr.Item("OUTKA_NO_L") = ds.Tables("LMC010IN_SIGMA").Rows(i).Item("OUTKA_NO_L").ToString
            dr.Item("ROW_NO") = ds.Tables("LMC010IN_SIGMA").Rows(i).Item("ROW_NO").ToString
            dr.Item("FILEPATH") = ds.Tables("LMC010IN_SIGMA").Rows(i).Item("FILEPATH").ToString
            dr.Item("FILENAME") = ds.Tables("LMC010IN_SIGMA").Rows(i).Item("FILENAME").ToString
            dr.Item("SYS_DATE") = ds.Tables("LMC010IN_SIGMA").Rows(i).Item("SYS_DATE").ToString
            dr.Item("SYS_TIME") = ds.Tables("LMC010IN_SIGMA").Rows(i).Item("SYS_TIME").ToString
            dr.Item("CUST_CD_L") = ds.Tables("LMC010IN_SIGMA").Rows(i).Item("CUST_CD_L").ToString

            dt.Rows.Add(dr)
            dr = dt.NewRow()
        Next

        Return inDs

    End Function
    'END 中村　シグマ出荷対応 2012.11.19

    ''' <summary>
    ''' CSVに使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetNiigataUnyuCsvInData(ByVal ds As DataSet, ByVal inDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim max As Integer = ds.Tables("LMC010IN_CSV").Rows.Count - 1

        For i As Integer = 0 To max
            dr.Item("NRS_BR_CD") = ds.Tables("LMC010IN_CSV").Rows(i).Item("NRS_BR_CD").ToString
            dr.Item("OUTKA_NO_L") = ds.Tables("LMC010IN_CSV").Rows(i).Item("OUTKA_NO_L").ToString
            dr.Item("ROW_NO") = ds.Tables("LMC010IN_CSV").Rows(i).Item("ROW_NO").ToString
            dr.Item("FILEPATH") = ds.Tables("LMC010IN_CSV").Rows(i).Item("FILEPATH").ToString
            dr.Item("FILENAME") = ds.Tables("LMC010IN_CSV").Rows(i).Item("FILENAME").ToString
            dr.Item("SYS_DATE") = ds.Tables("LMC010IN_CSV").Rows(i).Item("SYS_DATE").ToString
            dr.Item("SYS_TIME") = ds.Tables("LMC010IN_CSV").Rows(i).Item("SYS_TIME").ToString

            dt.Rows.Add(dr)
            dr = dt.NewRow()
        Next

        Return inDs

    End Function


    ''' <summary>
    ''' LMC651DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC651InData(ByVal ds As DataSet, ByVal printType As String) As DataSet
        Return Me.SetDataSetLMCInData(ds, New LMC651DS(), "LMC651IN", "OUTKA_NO_L", printType)
    End Function

#If True Then   'ADD 2018/07/24 依頼番号 : 001540  
    ''' <summary>
    ''' LMC870DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC870InData(ByVal ds As DataSet, ByVal printType As String) As DataSet
        Return Me.SetDataSetLMCInData(ds, New LMC870DS(), "LMC870IN", "OUTKA_NO_L", printType)
    End Function
#End If
    ''' <summary>
    ''' LMC641DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC641InData(ByVal ds As DataSet, ByVal printType As String) As DataSet
        Return Me.SetDataSetLMCInData(ds, New LMC641DS(), "LMC641IN", "OUTKA_NO_L", printType)
    End Function

    ''' <summary>
    ''' LMC642DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC642InData(ByVal ds As DataSet, ByVal printType As String) As DataSet
        Return Me.SetDataSetLMCInData(ds, New LMC642DS(), "LMC642IN", "OUTKA_NO_L", printType)
    End Function
    'START Kurihara WIT対応

    'ADD 2023/03/29 送品案内書(FFEM)追加
    ''' <summary>
    ''' LMC543DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC543InData(ByVal ds As DataSet, ByVal printType As String) As DataSet
        Return Me.SetDataSetLMCInData(ds, New LMC543DS(), "LMC543IN", "OUTKA_NO_L", printType)
    End Function

    ''' <summary>
    ''' LMC901DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC901InData(ByVal ds As DataSet, ByVal printType As String) As DataSet
        Return Me.SetDataSetLMCInData(ds, New LMC901DS(), "LMC901IN", "OUTKA_NO_L", printType)
    End Function

    ''' <summary>
    ''' MATOME_PICK_NOを採番する
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>MATOME_PICK_NO</returns>
    ''' <remarks></remarks>
    Private Function GetMatomePickNo(ByVal ds As DataSet) As String
        Dim targetDt As DataTable = ds.Tables("LMC010IN_OUTKA_L")

        '商品Keyを新規採番する
        Dim brCd As String = targetDt.Rows(0).Item("NRS_BR_CD").ToString()
        Dim num As New NumberMasterUtility
        Dim matomePickNo As String = num.GetAutoCode(NumberMasterUtility.NumberKbn.MATOME_PICK_NO, Me, brCd)

        Return matomePickNo

    End Function

    'END Kurihara WIT対応


    ''' <summary>
    '''営業所がタブレット対応かどうかを判定
    ''' </summary>
    ''' <param name="nrsBrCd">String</param>
    ''' <returns>boolean</returns>
    ''' <remarks></remarks>
    Private Function IsWhTabNrsBrCd(ByVal nrsBrCd As String) As Boolean

        Dim IsWhTabNrsFlg As Boolean = False
        Dim ds As DataSet = New LMC010DS
        Dim inRow As DataRow = ds.Tables("LMC010_TABLET_IN").NewRow()

        With inRow
            .Item("NRS_BR_CD") = nrsBrCd
        End With

        ds.Tables("LMC010_TABLET_IN").Rows.Add(inRow)

        MyBase.CallBLC(_Blc, "IsWhTabNrsBrCd", ds)

        If MyBase.GetResultCount() > 0 Then
            IsWhTabNrsFlg = True
        End If


        Return IsWhTabNrsFlg

    End Function

    ''' <summary>
    ''' ログ出力
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function WritePrintLog(ByVal ds As DataSet) As DataSet

        Dim fileName As String = String.Empty

        For Each dr As DataRow In ds.Tables("LMC010IN_WRITE_LOG").Rows
            Select Case dr.Item("PRINT_KBN").ToString
                Case "3"
                    fileName = String.Concat("納品書（印刷終了）：", dr.Item("PATH").ToString)
                Case "4"
                    fileName = String.Concat("分析票（印刷終了）：", dr.Item("PATH").ToString)
                Case "16"
                    fileName = String.Concat("イエローカード（印刷終了）：", dr.Item("PATH").ToString)
                Case "103"
                    fileName = String.Concat("納品書（コピー元）：", dr.Item("PATH").ToString)
                Case "104"
                    fileName = String.Concat("分析票（コピー元）：", dr.Item("PATH").ToString)
                Case "116"
                    fileName = String.Concat("イエローカード（コピー元）：", dr.Item("PATH").ToString)
                Case "203"
                    fileName = String.Concat("納品書（コピー先）：", dr.Item("PATH").ToString)
                Case "204"
                    fileName = String.Concat("分析票（コピー先）：", dr.Item("PATH").ToString)
                Case "216"
                    fileName = String.Concat("イエローカード（コピー先）：", dr.Item("PATH").ToString)
            End Select
            MyBase.Logger.WriteLog(0, "LMC010BLF", "WritePrintLog", fileName)
        Next

        Return ds

    End Function


#End Region
    '要望番号:1533 terakawa 2012.10.30 Start
#Region "チェック"
    ''' <summary>
    '''出荷(大)排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function OutkaLExistChk(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallBLC(Me._Blc, "OutkaLExistChk", ds)

        Return ds

    End Function

#End Region
    '要望番号:1533 terakawa 2012.10.30 End

#Region "WIT対応"

    'KASAMA 2013.10.29 WIT対応 Start
    ''' <summary>
    ''' 纏めピック対象ハンディ荷主かどうかを取得します。
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsMatomePickTargetCustHandy(ByVal ds As DataSet) As Boolean

        Dim checkDs As DataSet = ds.Copy

        Dim srcTbl As DataTable = checkDs.Tables("LMC010IN_OUTKA_L")
        Dim srcRow As DataRow = srcTbl.Rows(0)

        Dim inTbl As DataTable = checkDs.Tables("LMC010IN_CUST_HANDY")
        Dim newRow As DataRow = inTbl.NewRow

        newRow("NRS_BR_CD") = srcRow("NRS_BR_CD")
        newRow("OUTKA_NO_L") = srcRow("OUTKA_NO_L")
        newRow("CUST_CD_L") = srcRow("CUST_CD_L")

        inTbl.Rows.Add(newRow)

        ' ハンディ対象荷主情報取得
        checkDs = MyBase.CallBLC(Me._Blc, "SelectCustHandy", checkDs)

        Dim outTbl As DataTable = checkDs.Tables("LMC010OUT_CUST_HANDY")

        ' データが存在しない場合はfalse
        If outTbl Is Nothing OrElse outTbl.Rows.Count = 0 Then
            Return False
        End If

        Dim outRow As DataRow = outTbl.Rows(0)

        ' FLG_01:纏めピッキングWK作成可否フラグ
        Return LMConst.FLG.ON.Equals(outRow("FLG_01").ToString)

    End Function

#End Region

    '2014.04.21 CALT対応 黎 --ST--
#Region "出荷指示データ作成"
    ''' <summary>
    ''' 出荷指示データ作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function OutkaShiji(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = Nothing
        Dim max As Integer = ds.Tables("LMC010IN_OUTKA_DIRECT_SEND").Rows.Count - 1

        For i As Integer = 0 To max
            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                'メッセージのクリア
                MyBase.SetMessage(Nothing)

                '値のクリア
                inTbl = setDs.Tables("LMC010IN_OUTKA_DIRECT_SEND")
                inTbl.Clear()

                '条件の設定
                inTbl.ImportRow(ds.Tables("LMC010IN_OUTKA_DIRECT_SEND").Rows(i))

                '入荷データの取得処理を行う
                'BLCアクセス
                setDs = MyBase.CallBLC(Me._Blc, "OutkaShiji", setDs)

                'エラーがあるかを判定
                rtnResult = Not MyBase.IsMessageExist()

                If rtnResult = True Then
                    'resultのFalse戻し
                    rtnResult = False

                    'トランザクション終了
                    MyBase.CommitTransaction(scope)
                End If

            End Using

        Next

        Return ds

        Return ds
    End Function
#End Region
    '2014.04.21 CALT対応 黎 --ST--

    '2015.06.22 協立化学　作業料対応 START
#Region "作業料明細特殊作成"
    ''' <summary>
    ''' 作業料明細特殊作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SagyoMeisai(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False
        Dim scpResult As Boolean = False
        Dim setDs As DataSet = ds.Copy()
        Dim setSagDs As DataSet = Nothing
        Dim inTbl As DataTable = Nothing
        Dim max As Integer = ds.Tables("LMC010_OUTKA_M_IN").Rows.Count - 1
        Dim saginTbl As DataTable = Nothing
        Dim sagMax As Integer = 0

        For i As Integer = 0 To max

            'メッセージのクリア
            MyBase.SetMessage(Nothing)

            '値のクリア
            inTbl = setDs.Tables("LMC010_OUTKA_M_IN")
            inTbl.Clear()

            '条件の設定
            inTbl.ImportRow(ds.Tables("LMC010_OUTKA_M_IN").Rows(i))

            '出荷データ,作業レコード取得＋存在・排他チェック
            'BLCアクセス
            setDs = MyBase.CallBLC(Me._Blc, "OutkaSagyoList", setDs)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageStoreExist()

            If rtnResult = False Then
                Continue For

            ElseIf setDs.Tables("LMC010OUT_E_SAGYO_MEISAI").Rows.Count = 0 Then
                MyBase.SetMessageStore("00", "E463", {String.Empty}, ds.Tables("LMC010_OUTKA_M_IN").Rows(0).Item("ROW_NO").ToString())
                Continue For

            End If

            setSagDs = setDs.Copy()


            sagMax = setDs.Tables("LMC010OUT_E_SAGYO_MEISAI").Rows.Count - 1

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                For j As Integer = 0 To sagMax


                    '値のクリア
                    saginTbl = setSagDs.Tables("LMC010OUT_E_SAGYO_MEISAI")
                    saginTbl.Clear()

                    '条件の設定
                    saginTbl.ImportRow(setDs.Tables("LMC010OUT_E_SAGYO_MEISAI").Rows(j))

                    '作業料明細作成処理を行う
                    'BLCアクセス
                    setSagDs = MyBase.CallBLC(Me._Blc, "InsertSagyoRec", setSagDs)

                Next

                'エラーがあるかを判定
                scpResult = Not MyBase.IsMessageExist()

                If scpResult = True Then
                    'resultのFalse戻し
                    scpResult = False
                    'トランザクション終了
                    MyBase.CommitTransaction(scope)
                End If

            End Using

        Next

        Return ds

    End Function

#End Region
    '2015.06.22 協立化学　作業料対応 END

    '要望番号:2408 2015.09.17 追加START
    ''' <summary>
    ''' MEITETSU_DENP_NOを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>MEITETSU_DENP_NO</returns>
    ''' <remarks></remarks>
    Private Function GetMeiTetsuDenpNoL(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.MEITETSU_DENP_NO, Me, ds.Tables("LMC010IN_UPDATE").Rows(0).Item("NRS_BR_CD").ToString())

    End Function
    '要望番号:2408 2015.09.17 追加END

#Region "届先"

    '2017.09.19 届先追加対応 Annen add start
    ''' <summary>
    ''' データ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function UpdateOutkaDestKbn(ByVal ds As DataSet) As DataSet


        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            Dim resultDs As DataSet = MyBase.CallBLC(Me._Blc, "UpdateOutkaDestKbn", ds)

            'エラーがあるかを判定
            If MyBase.IsMessageExist() = False Then
                'エラーがなければコミットを行う
                MyBase.CommitTransaction(scope)
            End If

        End Using

        Return ds

    End Function
    '2017.09.19 届先追加対応 Annen add END
#End Region

#Region "タブレット対応"

    ''' <summary>
    ''' タブレットデータのキャンセル処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Sub UpdateTabletUnsoData(ByVal ds As DataSet)

        Dim inDs As New LMC930DS

        Dim nrsBrCd As String = ds.Tables("LMC010IN_UPDATE").Rows(0).Item("NRS_BR_CD").ToString
        Dim outkaNoL As String = ds.Tables("LMC010IN_UPDATE").Rows(0).Item("OUTKA_NO_L").ToString
        Dim whCd As String = ds.Tables("LMC010IN_UPDATE").Rows(0).Item("WH_CD").ToString
        Dim unsoCd As String = ds.Tables("LMC010IN_UPDATE").Rows(0).Item("UNSOCO_CD").ToString
        Dim unsoBrCd As String = ds.Tables("LMC010IN_UPDATE").Rows(0).Item("UNSOCO_BR_CD").ToString

        Dim inDt As DataTable = inDs.Tables("LMC930IN")
        Dim inDr As DataRow = inDt.NewRow
        inDr.Item("NRS_BR_CD") = nrsBrCd
        inDr.Item("OUTKA_NO_L") = outkaNoL
        inDr.Item("UNSO_NO_L") = String.Empty
        inDr.Item("WH_CD") = whCd
        inDr.Item("UNSO_CD") = unsoCd
        inDr.Item("UNSO_BR_CD") = unsoBrCd
        inDr.Item("PROC_TYPE") = "03"           '処理区分：運送変更
        inDt.Rows.Add(inDr)

        MyBase.CallBLC(New LMC930BLC(), LMC930BLC.FUNCTION_NM.WH_UNSO_UPDATE, inDs)

    End Sub

#End Region

#End Region

End Class

