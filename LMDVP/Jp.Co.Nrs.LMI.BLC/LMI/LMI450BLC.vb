' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI450  : 
'  作  成  者       :  [hojo]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI450BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI450BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TABLE_NAME

        ''' <summary>
        ''' 入力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INPUT As String = LMI450DAC.TABLE_NAME.INPUT
        ''' <summary>
        ''' 都道府県テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ADDR As String = LMI450DAC.TABLE_NAME.ADDR
        ''' <summary>
        ''' 商品テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const GOODS As String = LMI450DAC.TABLE_NAME.GOODS
        ''' <summary>
        ''' 出力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const EDI As String = LMI450DAC.TABLE_NAME.EDI

        ''' <summary>
        ''' 入力テーブル(輸送データ)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const EXCEL As String = LMI450DAC.TABLE_NAME.EXCEL


    End Class


    ''' <summary>
    ''' 関数名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FUNCTION_NAME

        ''' <summary>
        ''' EDIデータ取得
        ''' </summary>
        ''' <remarks></remarks>
        Public Const GetEdiData As String = "GetEdiData"

    End Class

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI450DAC = New LMI450DAC()

#End Region

#Region "Method"
    ''' <summary>
    ''' EDIデータ成型処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetEdiData(ByVal ds As DataSet) As DataSet

        Dim dtExcel As DataTable = ds.Tables(LMI450DAC.TABLE_NAME.EXCEL)
        Dim dtEdi As DataTable = ds.Tables(LMI450DAC.TABLE_NAME.EDI)
        Dim drEdi As DataRow
        Dim wk As String = String.Empty

        Dim dtIn As DataTable = ds.Tables(LMI450DAC.TABLE_NAME.INPUT)
        Dim pkgNb As Decimal = 1
        Dim i As Integer = 1
        For Each drExcel As DataRow In dtExcel.Rows
            i = i + 1

            '検索条件設定
            dtIn.Rows(0).Item("DEST_ADDR") = Left(drExcel.Item("SHUKKA_JUSHO").ToString, 4)
            dtIn.Rows(0).Item("GOODS_CD_CUST") = drExcel.Item("SEIHIN_CD").ToString.PadLeft(10, CChar("0"))

            ds.Tables(LMI450DAC.TABLE_NAME.ADDR).Clear()
            ds.Tables(LMI450DAC.TABLE_NAME.GOODS).Clear()

            '都道府県を取得
            MyBase.CallDAC(Me._Dac, LMI450DAC.FUNCTION_NAME.SelectKenData, ds)

            'CTNの場合、入数を取得
            pkgNb = 1
            If String.Concat(drExcel.Item("SEIHIN_TANI").ToString, Space(13)).Substring(0, 3).Equals("CTN") Then
                MyBase.CallDAC(Me._Dac, LMI450DAC.FUNCTION_NAME.SelectGoodsData, ds)
                If ds.Tables(TABLE_NAME.GOODS).Rows.Count = 0 Then
                    MyBase.SetMessageStore("00", "E769", New String() {String.Concat("商品コード:", drExcel.Item("SEIHIN_CD").ToString)}, i.ToString)
                    Continue For
                ElseIf ds.Tables(TABLE_NAME.GOODS).Rows.Count > 1 Then
                    MyBase.SetMessageStore("00", "E975", New String() {String.Concat("商品コード:", drExcel.Item("SEIHIN_CD").ToString)}, i.ToString)
                    Continue For
                Else
                    pkgNb = CDec(ds.Tables(TABLE_NAME.GOODS).Rows(0).Item(LMI450DAC.GOODS_COLUMN_NM.PKG_NB))
                End If
            End If

            drEdi = dtEdi.NewRow()
            'プラント(4)
            drEdi.Item("PLANT_CD") = String.Concat(drExcel.Item("PLANT_CD").ToString, Space(4)).Substring(0, 4)
            '出荷先CD(8)
            wk = String.Empty
            wk = drExcel.Item("SHUKKA_CD").ToString
            wk = wk.ToString.PadLeft(8, CChar("0"))
            drEdi.Item("SHUKKA_CD") = wk
            '出荷先会社名(38+2)
            wk = String.Empty
            wk = StrConv(String.Concat(drExcel.Item("SHUKKA_NM").ToString, Space(19)), VbStrConv.Wide).Substring(0, 19)
            wk = String.Concat(wk, Space(2))
            drEdi.Item("SHUKKA_NM") = wk
            '出荷先支店(38+2)
            drEdi.Item("SHUKKA_SHITEN_NM") = String.Concat(StrConv(Space(19), VbStrConv.Wide), Space(2))
            '出荷先住所1(8+2)
            wk = String.Empty
            If ds.Tables(LMI450DAC.TABLE_NAME.ADDR).Rows.Count = 1 Then
                wk = String.Concat(ds.Tables(LMI450DAC.TABLE_NAME.ADDR).Rows(0).Item("DEST_ADDR_KEN").ToString, Space(4)).Substring(0, 4)
                wk = String.Concat(StrConv(wk, VbStrConv.Wide), Space(2))
            Else
                wk = String.Concat(StrConv(Space(4), VbStrConv.Wide), Space(2))
            End If
            drEdi.Item("SHUKKA_JUSHO_1") = wk
            '出荷先住所2(38+2)
            wk = String.Empty
            wk = StrConv(String.Concat(drExcel.Item("SHUKKA_JUSHO").ToString, Space(19)), VbStrConv.Wide).Substring(0, 19)
            wk = String.Concat(wk, Space(2))
            drEdi.Item("SHUKKA_JUSHO_2") = wk
            '出荷先住所3(38+2)
            drEdi.Item("SHUKKA_JUSHO_3") = String.Concat(StrConv(Space(19), VbStrConv.Wide), Space(2))
            '出荷日(8)
            drEdi.Item("SHUKKA_BI") = String.Concat(drExcel.Item("SHUKKA_BI").ToString(), Space(8)).Substring(0, 8)
            '納期(8)
            drEdi.Item("NOUKI") = String.Concat(drExcel.Item("NOUKI").ToString(), Space(8)).Substring(0, 8)
            '製品CD(10)
            wk = String.Empty
            wk = drExcel.Item("SEIHIN_CD").ToString
            wk = wk.ToString.PadLeft(10, CChar("0"))
            drEdi.Item("SEIHIN_CD") = wk
            '製品名(58+2)
            wk = String.Empty
            wk = StrConv(String.Concat(drExcel.Item("SEIHIN_NM").ToString, Space(29)), VbStrConv.Wide).Substring(0, 29)
            wk = String.Concat(wk, Space(2))
            drEdi.Item("SEIHIN_NM") = wk
            '販売単位(3)
            drEdi.Item("SEIHIN_TANI") = String.Concat(drExcel.Item("SEIHIN_TANI").ToString, Space(13)).Substring(0, 3)
            'ロット番号(10)
            drEdi.Item("LOT_NO") = String.Concat(drExcel.Item("LOT_NO").ToString, Space(10)).Substring(0, 10)
            '個数(7)
            wk = String.Empty
            wk = drExcel.Item("KOSU").ToString
            If CDec(wk) - System.Math.Floor(CDec(wk)) > 0 Then
                'KOSU－KOSUの整数で0より大きい→小数値あり
                wk = "0"
            End If
            wk = (CDec(wk) * pkgNb).ToString("#,##0")
            wk = wk.Replace(".", "")
            wk = wk.Replace(",", "")
            If "-".Equals(Left(wk, 1)) = True Then
                wk = Mid(wk, 2).ToString.PadLeft(6, CChar("0"))
                wk = String.Concat("-", wk)
            Else
                wk = wk.ToString.PadLeft(6, CChar("0"))
                wk = String.Concat("+", wk)
            End If
            drEdi.Item("KOSU") = wk
            '重量(11)
            wk = String.Empty
            wk = drExcel.Item("JURYO").ToString
            wk = wk.Replace(".", "")
            wk = wk.Replace(",", "")
            If "-".Equals(Left(wk, 1)) = True Then
                wk = Mid(wk, 2).ToString.PadLeft(10, CChar("0"))
                wk = String.Concat("-", wk)
            Else
                wk = wk.ToString.PadLeft(10, CChar("0"))
                wk = String.Concat("+", wk)
            End If
            drEdi.Item("JURYO") = wk
            '荷材重量(11)
            drEdi.Item("NIZAI_JURYO") = Space(11)
            '当社オーダー(10)
            drEdi.Item("TOUSHA_ORDER") = Space(10)
            '保管場所(4)
            drEdi.Item("HOKAN_BASHO") = String.Concat(drExcel.Item("HOKAN_BASHO").ToString, Space(4)).Substring(0, 4)
            '出荷番号(10)
            drEdi.Item("SHUKKA_NO") = String.Concat(drExcel.Item("SHUKKA_NO").ToString, Space(10)).Substring(0, 10)
            '項目１ (3)
            drEdi.Item("KOMOKU_1") = Space(3)
            '項目２ (2)
            drEdi.Item("KOMOKU_2") = Space(2)
            '受注先CD(7)
            drEdi.Item("JUCHUSAKI_CD") = Space(7)
            '余白(24)
            drEdi.Item("YOHAKU") = Space(24)

            dtEdi.Rows.Add(drEdi)
        Next

        Return ds

    End Function

#End Region

End Class
