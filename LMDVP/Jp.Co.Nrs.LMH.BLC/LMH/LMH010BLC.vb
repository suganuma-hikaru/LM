' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH010    : EDI入荷検索
'  EDI荷主ID　　　　:  共通
'  作  成  者       :  nishikawa
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMH010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH010BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH010DAC = New LMH010DAC()

#End Region

#Region "Const"
    'ビーピー・カストロール追加箇所 terakawa 2012.12.12 Start
    '富士フイルム追加箇所 terakawa 2012.08.01 start
    '住化カラー追加箇所 20120528 start
    '日医工追加箇所 20120426 start
    'ダウケミ追加箇所 20120223 start
    '東邦化学追加箇所 20120216 start
    '浮間追加箇所 20120213 start
    Public Const NCGO_WID_L001 As String = "601_IN_0_L001"
    Public Const UKM_WID_L001 As String = "203_IN_0_L001"
    Public Const NSN_WID_L001 As String = "104_IN_0_L001"     '日産物流追加箇所 terakawa 2012.09.07
    Public Const TOHO_WID_L001 As String = "201_IN_0_L001"
    Public Const TOHO_WID_L002 As String = "201_IN_0_L002"

    Public Const NCGO_WID_M001 As String = "601_IN_1_M001"
    Public Const UKM_WID_M001 As String = "203_IN_0_M001"
    Public Const UKM_WID_M002 As String = "203_IN_0_M002"
    Public Const UKM_WID_M003 As String = "203_IN_0_M003"
    Public Const UKM_WID_M004 As String = "203_IN_0_M004"     '浮間合成追加箇所 terakawa 20120528
    Public Const DUPONT_WID_M001 As String = "403_IN_1_M001"
    Public Const DOW_WID_M001 As String = "202_IN_1_M001"
    Public Const DOW_WID_M002 As String = "202_IN_0_M002"
    Public Const SMK_WID_M001 As String = "502_IN_1_M001"
    Public Const SMK_WID_M002 As String = "502_IN_0_M002"
    Public Const NSN_WID_M001 As String = "104_IN_1_M001"     '日産物流追加箇所 terakawa 2012.09.07
    Public Const TOHO_WID_M001 As String = "201_IN_1_M001"
    Public Const TOHO_WID_M002 As String = "201_IN_0_M002"
    Public Const NIK_WID_M001 As String = "101_IN_1_M001"
    Public Const NIK_WID_M002 As String = "101_IN_0_M002"
    Public Const FJF_WID_M001 As String = "103_IN_1_M001"
    Public Const FJF_WID_M002 As String = "103_IN_0_M002"
    Public Const FJF_WID_M003 As String = "103_IN_0_M003"
    Public Const FJF_WID_M004 As String = "103_IN_0_M004"
    Public Const FJF_WID_M005 As String = "103_IN_0_M005"
    Public Const FJF_WID_M006 As String = "103_IN_0_M006"
    Public Const BP_WID_M001 As String = "501_IN_1_M001"
    Public Const BP_WID_M002 As String = "501_IN_0_M002"

    '2013.08.30 要望番号2100 日立FN対応　追加START
    Public Const DIC_WID_L001 As String = "205_IN_0_L001"
    Public Const DIC_WID_M001 As String = "205_IN_1_M001"
    Public Const DIC_WID_M002 As String = "205_IN_0_M002"
    '2013.08.30 要望番号2100 日立FN対応　追加END

    ' フィルメニッヒセミEDI対応 20160912
    Public Const FIL_WID_L001 As String = "404_IN_0_L001"
    Public Const FIL_WID_M001 As String = "404_IN_1_M001"

    '浮間追加箇所 20120213 end
    '東邦化学追加箇所 20120216 end
    'ダウケミ追加箇所 20120223 end
    '日医工追加箇所 20120426 end
    '住化カラー追加箇所 20120528 end
    '富士フイルム追加箇所 terakawa 2012.08.01 end
    'ビーピー・カストロール追加箇所 terakawa 2012.12.12 End


    Public Const GUIDANCE_KBN As String = "00"
    Public Const EXCEL_COLTITLE As String = "EDI管理番号"
    Public Const EXCEL_COLTITLE_SEMIEDI As String = "受信ファイル名"
    Public Const MAX_UNSOWT As String = "999,999,999.999"

    Public Const WH_TAB_STATUS_UNPROCESSED As String = "00"
    Public Const WH_TAB_YN_YES As String = "01"
    Public Const WH_TAB_YN_NO As String = "00"
    Public Const WH_TAB_IMP_YN_YES As String = "01"
    Public Const WH_TAB_IMP_YN_NO As String = "00"
#End Region

#Region "EDI荷主INDEX"
    'EDI荷主INDEX追加 20120525 terakawa Start
    'イベント種別
    Public Enum EdiCustIndex As Integer

        Ncgo32516_00 = 24                   '日本合成化学(名古屋)
        Dupont00295_00 = 16                 'デュポン(横浜)
        Dupont00331_00 = 34                 'デュポン(DCSE)(横浜)
        Dupont00331_02 = 35                 'デュポン(ABS)(横浜)
        Dupont00588_00 = 36                 'デュポン(SFTP塗料)(横浜)
        Dupont00331_03 = 37                 'デュポン()(横浜)
        Dupont00089_00 = 3                  'デュポン(テフロン)(千葉)→(横浜)に移送     '2012.04.11 ADD
        Dupont00700_00 = 33                 'デュポン(DCSE)(大阪)
        Dupont00689_00 = 32                 'デュポン(PVFM)(大阪)
        Dupont00300_00 = 15                 'デュポン(EP)(大阪)
        Dow00109_00 = 17                    'ダウケミ(大阪)
        DowTaka00109_01 = 18                'ダウケミ(大阪・高石)
        Toho00275_00 = 26                   '東邦化学(大阪)
        UkimaOsk00856_00 = 38               '浮間合成(大阪)
        Nissan00145_00 = 13                 '日産物流(千葉)
        Nik00171_00 = 39                    '日医工(千葉)
        UkimaSai00856_00 = 1                '浮間合成(岩槻)
        Sumika00952_00 = 2                  '住化カラー(岩槻)
        Fjf00195_00 = 40                    '富士フイルム(千葉)    '2012.08.01 ADD
        Bp00023_00 = 30                     'ビーピー・カストロール(岩槻)    '2012.12.12 ADD

        DicGnm00076_00 = 43                 '（群馬）ディック物流群馬
        DicItk10001_00 = 44                 '（岩槻）ディック物流春日部
        DicItk10007_00 = 45                 '（岩槻）ディック物流東京営業所
        FjfTaka00195_00 = 46                '富士フイルム(高取)

    End Enum
    'EDI荷主INDEX追加 20120525 terakawa End
#End Region

#Region "Method"

#Region "EDI(大)データ"
    ''' <summary>
    ''' 入荷管理番号(大)の値チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks>空でない(入荷登録済み)場合エラー</remarks>
    Public Function EdiLCheck(ByVal dt As DataTable) As Boolean

        Dim inkaNoL As String = dt.Rows(0)("INKA_CTL_NO_L").ToString()

        If String.IsNullOrEmpty(inkaNoL) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    '''  保管料起算日チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks>値が設定されていて日付として不正な場合エラー</remarks>
    Public Function HokanStrDateCheck(ByVal dt As DataTable) As Boolean

        Dim hokanStrDate As String = dt.Rows(0)("HOKAN_STR_DATE").ToString()


        If String.IsNullOrEmpty(hokanStrDate) = False Then

            If hokanStrDate.Length < 8 Then

                Return False
            Else
                hokanStrDate = String.Concat(hokanStrDate.Substring(0, 4), "/", hokanStrDate.Substring(4, 2), "/", hokanStrDate.Substring(6, 2))

                If IsDate(hokanStrDate) = False Then

                    Return False
                Else

                End If

            End If

        End If

        Return True

    End Function

    ''' <summary>
    '''入荷日チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks>空の場合エラー,日付として不正な場合エラー</remarks>
    Public Function InkaDateCheck(ByVal dt As DataTable) As Boolean

        Dim inkaDate As String = dt.Rows(0)("INKA_DATE").ToString()

        If String.IsNullOrEmpty(inkaDate) = True Then
            Return False
        End If

        If inkaDate.Length < 8 Then

            Return False
        Else
            inkaDate = String.Concat(inkaDate.Substring(0, 4), "/", inkaDate.Substring(4, 2), "/", inkaDate.Substring(6, 2))

            If IsDate(inkaDate) = False Then

                Return False
            Else

            End If

        End If

        Return True

    End Function


    ''' <summary>
    ''' 荷主コードLの値チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks>空の場合エラー</remarks>
    Public Function CustCdLCheck(ByVal dt As DataTable) As Boolean

        Dim custCdL As String = dt.Rows(0)("CUST_CD_L").ToString()

        If String.IsNullOrEmpty(custCdL) = True Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 荷主コードMの値チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks>空の場合エラー</remarks>
    Public Function CustcdMCheck(ByVal dt As DataTable) As Boolean

        Dim ediNoM As String = dt.Rows(0)("CUST_CD_M").ToString()

        If String.IsNullOrEmpty(ediNoM) = True Then
            Return False
        End If

        Return True

    End Function


#End Region

#Region "EDI(中)"

    ''' <summary>
    ''' 赤黒区分チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks>0以外の場合エラー</remarks>
    Public Function AkakuroCheck(ByVal dt As DataTable) As Boolean

        Dim akakuroKbn As String = String.Empty
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max

            akakuroKbn = dt.Rows(i)("AKAKURO_KB").ToString()

            If akakuroKbn.Equals("0") = False Then
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 個数チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks>0以下の場合エラー</remarks>
    Public Function NbCheck(ByVal dt As DataTable) As Boolean

        Dim nb As Long = 0
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max

            nb = Convert.ToInt64(dt.Rows(i)("NB"))

            If nb <= 0 Then
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 標準入目チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks>0の場合エラー</remarks>
    Public Function StdIrimeCheck(ByVal dt As DataTable) As Boolean

        Dim stdIrime As Decimal = 0
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max

            stdIrime = Convert.ToDecimal(dt.Rows(i)("STD_IRIME"))

            If stdIrime = 0 Then
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 商品コード、商品キーチェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks>商品コードと商品キー共に空の場合エラー</remarks>
    Public Function GoodsCdCheck(ByVal dt As DataTable) As Boolean

        Dim goodsCdNrs As String = String.Empty
        Dim goodsCdCust As String = String.Empty

        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max

            goodsCdNrs = dt.Rows(i)("NRS_GOODS_CD").ToString()
            goodsCdCust = dt.Rows(i)("CUST_GOODS_CD").ToString()

            If String.IsNullOrEmpty(goodsCdNrs) = True AndAlso String.IsNullOrEmpty(goodsCdCust) = True Then
                Return False
            End If

        Next

        Return True

    End Function

    '▼▼▼要望番号:466
    ''' <summary>
    ''' 運送重量最大値チェック
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function UnsoJuryoCheck(ByVal dt As DataTable) As Boolean

        Dim unsoJuryo As Decimal = Convert.ToDecimal(dt.Rows(0)("UNSO_WT"))

        If Convert.ToDecimal(LMH010BLC.MAX_UNSOWT) < unsoJuryo Then
            Return False
        End If

        Return True

    End Function
    '▲▲▲要望番号:466

#End Region

#Region "ワーニング設定M"
    ''' <summary>
    ''' ワーニング設定
    ''' </summary>
    ''' <param name="msgId">メッセージID</param>
    ''' <param name="warningId">ワーニングID</param>
    ''' <param name="ds">データセット</param>
    ''' <returns>データセット</returns>
    ''' <remarks>ワーニング画面表示用データを作成する</remarks>
    Public Function SetWarningM(ByVal msgId As String, ByVal warningId As String, ByVal ds As DataSet, ByVal dsM As DataSet, ByVal msgArray() As String) As DataSet

        Dim drW As DataRow = ds.Tables("WARNING_DTL").NewRow()
        Dim drEdiL As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim drEdiM As DataRow = dsM.Tables("LMH010_INKAEDI_M").Rows(0)

        drW.Item("EDI_CTL_NO_L") = drEdiM("EDI_CTL_NO")
        drW.Item("EDI_CTL_NO_M") = drEdiM("EDI_CTL_NO_CHU")
        drW.Item("CUST_ORD_NO") = drEdiL("OUTKA_FROM_ORD_NO")
        drW.Item("CUST_ORD_NO_DTL") = drEdiM("OUTKA_FROM_ORD_NO")
        drW.Item("INOUTKA_NO") = String.Empty
        drW.Item("INOUTKA_NO_CHU_MAX") = String.Empty
        drW.Item("MESSAGE_ID") = msgId
        drW.Item("PARA1") = msgArray(1)
        drW.Item("PARA2") = msgArray(2)
        drW.Item("PARA3") = msgArray(3)
        drW.Item("PARA4") = msgArray(4)
        drW.Item("PARA5") = msgArray(5)

        Dim mstFlg As String = warningId.Substring(7, 1)
        If mstFlg = "1" Then
            drW.Item("GOODS_NM") = String.Empty
        Else
            drW.Item("GOODS_NM") = drEdiM("GOODS_NM")
        End If

        drW.Item("FIELD_NM") = "荷主商品コード"
        drW.Item("FIELD_VALUE") = drEdiM("CUST_GOODS_CD")

        drW.Item("MST_VALUE") = String.Empty
        drW.Item("EDI_WARNING_ID") = warningId

#If True Then ' フィルメニッヒ セミEDI対応  20160912 added inoue
        drW.Item("ADDITIONAL_FIELD_VALUE_1") = String.Empty
#End If

        ds.Tables("WARNING_DTL").Rows.Add(drW)

        Return ds
    End Function
#End Region

    '浮間追加箇所 20120213 start
#Region "ワーニング設定M2"

#If False Then ' フィルメニッヒ セミEDI対応  20160912 changed inoue
    ''' <summary>
    ''' ワーニング設定
    ''' </summary>
    ''' <param name="msgId">メッセージID</param>
    ''' <param name="warningId">ワーニングID</param>
    ''' <param name="ds">データセット</param>
    ''' <returns>データセット</returns>
    ''' <remarks>ワーニング画面表示用データを作成する</remarks>
    Public Function SetWarningM2(ByVal msgId As String, ByVal warningId As String, ByVal ds As DataSet, ByVal dsM As DataSet _
                                 , ByVal msgArray() As String, ByVal ediName As String, ByVal ediValue As String, ByVal mstValue As String) As DataSet
        Dim drW As DataRow = ds.Tables("WARNING_DTL").NewRow()
        Dim drEdiL As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim drEdiM As DataRow = dsM.Tables("LMH010_INKAEDI_M").Rows(0)

        drW.Item("EDI_CTL_NO_L") = drEdiM("EDI_CTL_NO")
        drW.Item("EDI_CTL_NO_M") = drEdiM("EDI_CTL_NO_CHU")
        drW.Item("CUST_ORD_NO") = drEdiL("OUTKA_FROM_ORD_NO")
        drW.Item("CUST_ORD_NO_DTL") = drEdiM("OUTKA_FROM_ORD_NO")
        drW.Item("INOUTKA_NO") = String.Empty
        drW.Item("INOUTKA_NO_CHU_MAX") = String.Empty
        drW.Item("MESSAGE_ID") = msgId
        drW.Item("PARA1") = msgArray(1)
        drW.Item("PARA2") = msgArray(2)
        drW.Item("PARA3") = msgArray(3)
        drW.Item("PARA4") = msgArray(4)
        drW.Item("PARA5") = msgArray(5)

        Dim mstFlg As String = warningId.Substring(7, 1)
        If mstFlg = "1" Then
            drW.Item("GOODS_NM") = String.Empty
        Else
            drW.Item("GOODS_NM") = drEdiM("GOODS_NM")
        End If

        drW.Item("FIELD_NM") = ediName
        drW.Item("FIELD_VALUE") = ediValue
        drW.Item("MST_VALUE") = mstValue
        drW.Item("EDI_WARNING_ID") = warningId

        ds.Tables("WARNING_DTL").Rows.Add(drW)

        Return ds
    End Function

#Else

    ''' <summary>
    ''' ワーニング設定
    ''' </summary>
    ''' <param name="msgId"></param>
    ''' <param name="warningId"></param>
    ''' <param name="ds"></param>
    ''' <param name="dsM"></param>
    ''' <param name="msgArray"></param>
    ''' <param name="ediName"></param>
    ''' <param name="ediValue"></param>
    ''' <param name="mstValue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetWarningM2(ByVal msgId As String _
                               , ByVal warningId As String _
                               , ByVal ds As DataSet _
                               , ByVal dsM As DataSet _
                               , ByVal msgArray() As String _
                               , ByVal ediName As String _
                               , ByVal ediValue As String _
                               , ByVal mstValue As String) As DataSet


        Return Me.SetWarningM2(msgId, warningId, ds, dsM, msgArray, ediName, ediValue, mstValue, String.Empty)

    End Function



    ''' <summary>
    ''' ワーニング設定
    ''' </summary>
    ''' <param name="msgId">メッセージID</param>
    ''' <param name="warningId">ワーニングID</param>
    ''' <param name="ds">データセット()</param>
    ''' <param name="dsM">データセット()</param>
    ''' <param name="msgArray">メッセージパラメータ</param>
    ''' <param name="ediName">項目名</param>
    ''' <param name="ediValue">項目値</param>
    ''' <param name="mstValue">マスター値</param>
    ''' <param name="addtionalFieldValue">追加項目値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SetWarningM2(ByVal msgId As String _
                               , ByVal warningId As String _
                               , ByVal ds As DataSet _
                               , ByVal dsM As DataSet _
                               , ByVal msgArray() As String _
                               , ByVal ediName As String _
                               , ByVal ediValue As String _
                               , ByVal mstValue As String _
                               , ByVal addtionalFieldValue As String) As DataSet
        Dim drW As DataRow = ds.Tables("WARNING_DTL").NewRow()
        Dim drEdiL As DataRow = ds.Tables("LMH010_INKAEDI_L").Rows(0)
        Dim drEdiM As DataRow = dsM.Tables("LMH010_INKAEDI_M").Rows(0)

        drW.Item("EDI_CTL_NO_L") = drEdiM("EDI_CTL_NO")
        drW.Item("EDI_CTL_NO_M") = drEdiM("EDI_CTL_NO_CHU")
        drW.Item("CUST_ORD_NO") = drEdiL("OUTKA_FROM_ORD_NO")
        drW.Item("CUST_ORD_NO_DTL") = drEdiM("OUTKA_FROM_ORD_NO")
        drW.Item("INOUTKA_NO") = String.Empty
        drW.Item("INOUTKA_NO_CHU_MAX") = String.Empty
        drW.Item("MESSAGE_ID") = msgId
        drW.Item("PARA1") = msgArray(1)
        drW.Item("PARA2") = msgArray(2)
        drW.Item("PARA3") = msgArray(3)
        drW.Item("PARA4") = msgArray(4)
        drW.Item("PARA5") = msgArray(5)

        Dim mstFlg As String = warningId.Substring(7, 1)
        If mstFlg = "1" Then
            drW.Item("GOODS_NM") = String.Empty
        Else
            drW.Item("GOODS_NM") = drEdiM("GOODS_NM")
        End If

        drW.Item("FIELD_NM") = ediName
        drW.Item("FIELD_VALUE") = ediValue        

        drW.Item("MST_VALUE") = mstValue
        drW.Item("EDI_WARNING_ID") = warningId

        drW.Item("ADDITIONAL_FIELD_VALUE_1") = addtionalFieldValue

        ds.Tables("WARNING_DTL").Rows.Add(drW)

        Return ds
    End Function
#End If

#End Region
    '浮間追加箇所 20120213 end

#Region "ワーニング設定L"
    ''' <summary>
    ''' ワーニング設定
    ''' </summary>
    ''' <param name="msgId">メッセージID</param>
    ''' <param name="warningId">ワーニングID</param>
    ''' <param name="ds">データセット</param>
    ''' <returns>データセット</returns>
    ''' <remarks>ワーニング画面表示用データを作成する</remarks>
    Public Function SetWarningL(ByVal msgId As String, ByVal warningId As String, ByVal ds As DataSet, ByVal msgArray() As String, Optional ByVal inkaNoL As String = "") As DataSet

        Dim drW As DataRow = ds.Tables("WARNING_DTL").NewRow()
        Dim drIN As DataRow = ds.Tables("LMH010INOUT").Rows(0)

        drW.Item("EDI_CTL_NO_L") = drIN("EDI_CTL_NO")
        drW.Item("EDI_CTL_NO_M") = String.Empty
        drW.Item("CUST_ORD_NO") = drIN("OUTKA_FROM_ORD_NO")
        drW.Item("CUST_ORD_NO_DTL") = String.Empty
        drW.Item("INOUTKA_NO") = String.Empty
        drW.Item("INOUTKA_NO_CHU_MAX") = String.Empty
        drW.Item("MESSAGE_ID") = msgId
        drW.Item("PARA1") = msgArray(1)
        drW.Item("PARA2") = msgArray(2)
        drW.Item("PARA3") = msgArray(3)
        drW.Item("PARA4") = msgArray(4)
        drW.Item("PARA5") = msgArray(5)
        drW.Item("GOODS_NM") = String.Empty
        drW.Item("FIELD_NM") = String.Empty
        drW.Item("FIELD_VALUE") = String.Empty
        drW.Item("MST_VALUE") = String.Empty
        drW.Item("EDI_WARNING_ID") = warningId


#If True Then ' フィルメニッヒ セミEDI対応  20160912 added inoue
        drW.Item("ADDITIONAL_FIELD_VALUE_1") = String.Empty
#End If

        ds.Tables("WARNING_DTL").Rows.Add(drW)

        Return ds
    End Function
#End Region

#Region "ワーニング処理(EDI(大))選択区分の取得"

    ''' <summary>
    ''' 選択区分の取得
    ''' </summary>
    ''' <param name="setDt"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWarningChoiceKb(ByVal setDt As DataTable, ByVal ds As DataSet, _
                                           ByVal warningId As String, ByVal count As Integer) As String

        Dim dtWarning As DataTable = ds.Tables("WARNING_SHORI")
        Dim ediCtlNoL As String = setDt.Rows(count)("EDI_CTL_NO").ToString()
        Dim max As Integer = dtWarning.Rows.Count - 1
        Dim dr As DataRow
        Dim choiceKb As String = String.Empty

        'ワーニング処理設定されていなければ処理終了
        If max = -1 Then
            Return choiceKb
        End If

        For i As Integer = 0 To max

            dr = dtWarning.Rows(i)

            If warningId.Equals(dr("EDI_WARNING_ID").ToString()) AndAlso ediCtlNoL.Equals(dr("EDI_CTL_NO_L")) Then
                'ワーニング処理設定の値を反映
                choiceKb = dr.Item("CHOICE_KB").ToString()
                Return choiceKb

            End If

        Next

        Return choiceKb

    End Function

#End Region

    '浮間追加箇所 20120213 start
#Region "ワーニング処理(EDI(中))選択区分の取得"

    ''' <summary>
    ''' 選択区分の取得
    ''' </summary>
    ''' <param name="setDt"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetWarningChoiceKbM(ByVal setDt As DataTable, ByVal ds As DataSet, _
                                           ByVal warningId As String, ByVal count As Integer) As String

        Dim dtWarning As DataTable = ds.Tables("WARNING_SHORI")
        Dim ediCtlNoL As String = setDt.Rows(count)("EDI_CTL_NO").ToString()
        Dim ediCtlNoM As String = setDt.Rows(count)("EDI_CTL_NO_CHU").ToString()
        Dim max As Integer = dtWarning.Rows.Count - 1
        Dim dr As DataRow
        Dim choiceKb As String = String.Empty

        'ワーニング処理設定されていなければ処理終了
        If max = -1 Then
            Return choiceKb
        End If

        For i As Integer = 0 To max

            dr = dtWarning.Rows(i)

            If warningId.Equals(dr("EDI_WARNING_ID").ToString()) AndAlso ediCtlNoL.Equals(dr("EDI_CTL_NO_L")) _
                                                    AndAlso ediCtlNoM.Equals(dr("EDI_CTL_NO_M")) Then

                'ワーニング処理設定の値を反映
                choiceKb = dr.Item("CHOICE_KB").ToString()
                Return choiceKb

            End If

        Next

        Return choiceKb

    End Function

#End Region
    '浮間追加箇所 20120213 end

    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 Start
#Region "メッセージ作成"
    ''' <summary>
    ''' メッセージ作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetErrMsgE493(ByVal ds As DataSet) As String

        'E493[%3]用のエラーメッセージを作成する

        Dim dtM As DataTable = ds.Tables("LMH010_INKAEDI_M")

        Dim sEDI_CTL_NO As String = dtM.Rows(0).Item("EDI_CTL_NO").ToString             'EDI_CTL_NO
        Dim sEDI_CTL_NO_CHU As String = dtM.Rows(0).Item("EDI_CTL_NO_CHU").ToString     'EDI_CTL_NO_CHU
        Dim sCUST_GOODS_CD As String = dtM.Rows(0).Item("CUST_GOODS_CD").ToString       '荷主商品コード
        Dim sGOODS_NM As String = dtM.Rows(0).Item("GOODS_NM").ToString                 '商品名

        Dim sErrMsg As String = String.Concat(" EDI管理番号 = ", sEDI_CTL_NO, "-", sEDI_CTL_NO_CHU _
                                           , "、荷主商品コード = ", sCUST_GOODS_CD _
                                           , "、商品名 = ", sGOODS_NM)
        Return sErrMsg
    End Function

#End Region
    '要望番号:1053(出荷登録時のエラーメッセージ表記について) 2012/06/21 本明 End

    '2013.10.10 追加START
#Region "SPACE除去 + 文字変換"
    ''' <summary>
    ''' SPACE除去 + 文字変換
    ''' </summary>
    ''' <param name="chkFld"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function SpaceCutChk(ByVal chkFld As String) As String

        chkFld = Replace(Trim(chkFld), Space(1), String.Empty)
        chkFld = Replace(chkFld, "　", String.Empty)
        chkFld = StrConv(chkFld, VbStrConv.Wide)

        Return chkFld

    End Function

#End Region
    '2013.10.10 追加END


#Region "検索処理"

#Region "検索件数取得"
    ''' <summary>
    ''' 検索件数取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
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
        ElseIf MyBase.GetLimitCount() < count Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function
#End Region

#Region "検索値の取得"
    ''' <summary>
    ''' 検索の値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function
#End Region

#End Region

    '▼▼▼二次
#Region "実績取消処理"
    ''' <summary>
    ''' 実績取消処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiTorikesi(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        Dim rcvNmHed As String = ds.Tables("LMH010INOUT").Rows(0).Item("RCV_NM_HED").ToString()
        Dim rcvNmDtl As String = ds.Tables("LMH010INOUT").Rows(0).Item("RCV_NM_DTL").ToString()

        'EDI入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        '受信ヘッダの更新
        If String.IsNullOrEmpty(rcvNmHed) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)
            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
        End If

        '受信明細の更新
        If String.IsNullOrEmpty(rcvNmDtl) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)
        End If

        Return ds

    End Function

#End Region

#Region "EDI取消"
    Private Function EdiTorikesi(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("EDI_CTL_NO").ToString()
        Dim rcvNmHed As String = ds.Tables("LMH010INOUT").Rows(0).Item("RCV_NM_HED").ToString()
        Dim rcvNmDtl As String = ds.Tables("LMH010INOUT").Rows(0).Item("RCV_NM_DTL").ToString()

        'EDI入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI入荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)

        '受信ヘッダの更新
        If String.IsNullOrEmpty(rcvNmHed) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)
            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                Return ds
            End If
        End If

        '受信明細の更新
        If String.IsNullOrEmpty(rcvNmDtl) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)
        End If

        Return ds

    End Function

#End Region

#Region "EDI取消⇒未登録, 報告用EDI取消"
    ''' <summary>
    ''' EDI取消⇒未登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>EDI入荷(中), 受信ヘッダ, 受信明細の削除フラグ変更</remarks>
    Private Function EdiOperation(ByVal ds As DataSet) As DataSet
        '大阪対応　20120322　Start
        Dim rcvNmHed As String = ds.Tables("LMH010INOUT").Rows(0).Item("RCV_NM_HED").ToString()
        '大阪対応　20120322　End

        'EDI入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)

        'EDI入荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)

        '大阪対応　20120322　Start
        '受信ヘッダの更新
        If String.IsNullOrEmpty(rcvNmHed) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)
        End If
        '大阪対応　20120322　End

        '受信明細の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        Return ds

    End Function
#End Region '削除予定

#Region "実績作成済⇒実績未, 実績送信済⇒実績未"
    ''' <summary>
    ''' 実績作成済⇒実績未, 実績送信済⇒実績未
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiOperation2(ByVal ds As DataSet) As DataSet

        Dim rcvNmHed As String = ds.Tables("LMH010INOUT").Rows(0).Item("RCV_NM_HED").ToString()
        Dim rcvNmDtl As String = ds.Tables("LMH010INOUT").Rows(0).Item("RCV_NM_DTL").ToString()
        Dim nmSend As String = ds.Tables("LMH010INOUT").Rows(0).Item("SND_NM").ToString()

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)

        'EDI受信(HED)の更新
        If String.IsNullOrEmpty(rcvNmHed) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)
        End If

        '受信明細の更新
        If String.IsNullOrEmpty(rcvNmDtl) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)
        End If

        'EDI送信の更新
        If String.IsNullOrEmpty(nmSend) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "DeleteSend", ds)
        End If

        '入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateInkaL", ds)

        Return ds

    End Function

#End Region

#Region "実績送信済⇒送信未"
    ''' <summary>
    ''' 実績送信済⇒送信未
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiOperation3(ByVal ds As DataSet) As DataSet

        Dim rcvNmHed As String = ds.Tables("LMH010INOUT").Rows(0).Item("RCV_NM_HED").ToString()
        Dim rcvNmDtl As String = ds.Tables("LMH010INOUT").Rows(0).Item("RCV_NM_DTL").ToString()
        Dim nmSend As String = ds.Tables("LMH010INOUT").Rows(0).Item("SND_NM").ToString()

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)

        'EDI受信(HED)の更新
        If String.IsNullOrEmpty(rcvNmHed) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)
        End If

        ''EDI受信(DTL)の更新
        If String.IsNullOrEmpty(rcvNmDtl) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)
        End If

        'EDI送信の更新
        If String.IsNullOrEmpty(nmSend) = True Then
        Else
            'EDI送信の更新
            ds = MyBase.CallDAC(Me._Dac, "UpdateSend", ds)
        End If

        Return ds

    End Function

#End Region

#Region "入荷取消⇒未登録"
    ''' <summary>
    ''' 入荷取消⇒未登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Mitouroku(ByVal ds As DataSet) As DataSet

        Dim rcvNmHed As String = ds.Tables("LMH010INOUT").Rows(0).Item("RCV_NM_HED").ToString()
        Dim rcvNmDtl As String = ds.Tables("LMH010INOUT").Rows(0).Item("RCV_NM_DTL").ToString()

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)

        'EDI受信(HED)の更新
        If String.IsNullOrEmpty(rcvNmHed) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)
        End If

        ''EDI受信(DTL)の更新
        If String.IsNullOrEmpty(rcvNmDtl) = True Then
        Else
            ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)
        End If

        Return ds

    End Function

#End Region

    '2015.09.07 tsunehira add
#Region "商品key変更"
    ''' <summary>
    ''' 商品key変更
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetEDI_M(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectEDI_M", ds)

        Dim dr As DataRow() = Nothing
        Dim preEdiCtlChu As String = String.Empty
        'Dim drM() As DataRow = Nothing

        'drM = ds.Tables("LMH010_INKAEDI_M").Select()

        For i As Integer = ds.Tables("LMH010_INKAEDI_M").Rows.Count() - 1 To 0 Step -1
            'For Each row As DataRow In drM
            'DSに格納
            '商品重複行は更新しない
            dr = ds.Tables("LMH010_INKAEDI_M").Select(String.Concat(" EDI_CTL_NO_CHU = '", ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("EDI_CTL_NO_CHU").ToString(), "'"))
            If dr.Length > 1 OrElse (dr.Length = 1 AndAlso preEdiCtlChu.Equals(ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("EDI_CTL_NO_CHU").ToString()) = True) Then
                preEdiCtlChu = ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("EDI_CTL_NO_CHU").ToString()
                'For j As Integer = 0 To dr.Length - 1
                'dr(j).Item("NRS_GOODS_CD") = String.Empty
                ds.Tables("LMH010_INKAEDI_M").Rows.Remove(ds.Tables("LMH010_INKAEDI_M").Rows(i))
                'Next
            Else
                preEdiCtlChu = ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("EDI_CTL_NO_CHU").ToString()
            End If

        Next

        For i As Integer = 0 To ds.Tables("LMH010_INKAEDI_M").Rows.Count() - 1
            If String.IsNullOrEmpty(ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("HASU").ToString) = True Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E609", , String.Empty, LMH010BLC.EXCEL_COLTITLE, ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("EDI_CTL_NO").ToString)
                Return ds
            End If

            'Dim Num As Decimal = Convert.ToDecimal(ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("HASU"))
            'Dim Num_Int As Decimal = Convert.ToDecimal(Fix(Num))

            If Convert.ToDecimal(ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("HASU")) <> 0 Then
                Try
                    'Convert.ToInt32(Num - Num_Int)
                    Convert.ToInt32(ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("HASU"))
                Catch ex As System.OverflowException
                    MyBase.SetMessage("E489", New String() {""})
                    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E489", New String() {""}, String.Empty, LMH010BLC.EXCEL_COLTITLE, ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("EDI_CTL_NO").ToString)
                    Return ds
                Catch ex As System.FormatException
                    MyBase.SetMessage("E489", New String() {""})
                    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E489", New String() {""}, String.Empty, LMH010BLC.EXCEL_COLTITLE, ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("EDI_CTL_NO").ToString)
                    Return ds
                Catch ex As System.ArgumentException
                    MyBase.SetMessage("E489", New String() {""})
                    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E489", New String() {""}, String.Empty, LMH010BLC.EXCEL_COLTITLE, ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("EDI_CTL_NO").ToString)
                    Return ds
                End Try
            End If

            If String.IsNullOrEmpty(ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("NB").ToString) = True Or String.IsNullOrEmpty(ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("NRS_GOODS_CD").ToString) = True Then
                MyBase.SetMessage("E609")
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E609", , String.Empty, LMH010BLC.EXCEL_COLTITLE, ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("EDI_CTL_NO").ToString)
                Return ds
            ElseIf Convert.ToDouble(ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("IRIME")) = 0.0 Then
                MyBase.SetMessage("E518", New String() {"入目(総重量)", "(値が0で不正です)"})
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E518", New String() {"入目(総重量)", "(値が0で不正です)"}, String.Empty, LMH010BLC.EXCEL_COLTITLE, ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("EDI_CTL_NO").ToString)
                Return ds
            ElseIf Convert.ToInt32(ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("NB")) = 0 AndAlso Convert.ToInt32(ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("HASU")) = 0 Then
                MyBase.SetMessage("E489", New String() {""})
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E489", New String() {""}, String.Empty, LMH010BLC.EXCEL_COLTITLE, ds.Tables("LMH010_INKAEDI_M").Rows(i).Item("EDI_CTL_NO").ToString)
                Return ds
            End If

        Next

        Return ds

    End Function
#End Region

    '2015.09.07 tsunehira add
#Region "一括変更(EDI入荷大)"

    Private Function Chg_EDI_L(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)
        Return ds

    End Function

#End Region

    '2015.09.07 tsunehira add
#Region "一括変更(EDI入荷中)"

    Private Function Chg_EDI_M(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)
        Return ds
        
    End Function

#End Region



    '2012.02.25 大阪対応 START
#Region "入荷取消⇒未登録時同一まとめレコード取得処理"
    ''' <summary>
    ''' 入荷取消⇒未登録時同一まとめレコード取得処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectMatomeTorikesi(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMatomeTorikesi", ds)

        Return ds

    End Function

#End Region
    '2012.02.25 大阪対応 END
    '▲▲▲二次

    '2012.03.14 大阪対応START
#Region "印刷フラグ更新"
    ''' <summary>
    ''' 印刷フラグ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintFlagUpDate(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "UpdatePrintFlag", ds)

        Return ds

    End Function

#End Region
    '2012.03.14 大阪対応END

    '要望番号1007 2012.05.08 追加START
#Region "EDI印刷対象テーブル追加"
    ''' <summary>
    ''' EDI印刷対象テーブル追加(削除⇒追加)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteInsertHEdiPrint(ByVal ds As DataSet) As DataSet

        Dim dtPrt As DataTable = ds.Tables("H_EDI_PRINT")
        Dim max As Integer = dtPrt.Rows.Count - 1
        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDtPrt As DataTable = setDs.Tables("H_EDI_PRINT")

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            setDtPrt.ImportRow(dtPrt.Rows(i))

            '①EDI印刷対象テーブル物理削除
            setDs = MyBase.CallDAC(Me._Dac, "DeleteHEdiPrint", setDs)

            '②EDI印刷対象テーブル新規追加
            setDs = MyBase.CallDAC(Me._Dac, "InsertHEdiPrint", setDs)

        Next

        Return ds

    End Function

#End Region
    '要望番号1007 2012.05.08 追加END

    'START UMANO 要望番号1302 支払運賃に伴う修正。支払追加処理
#Region "入荷登録処理(支払運賃作成)"

    ''' <summary>
    ''' 入荷登録処理(支払運賃作成)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShiharaiSakusei(ByVal ds As DataSet) As DataSet

        '支払運賃の新規作成
        ds = MyBase.CallDAC(Me._Dac, "InsertShiharaiUnchinData", ds)

        Return ds

    End Function

#End Region
    'END UMANO 要望番号1302 支払運賃に伴う修正。支払追加処理

    '2015.04.13 追加START
#Region "バイト切捨て(LeftB)"
    ''' <summary>Left関数のバイト版。文字数をバイト数で指定して文字列を切捨て。</summary>
    ''' <param name="str">対象の文字列</param>
    ''' <param name="Length">切り抜く文字列のバイト数</param>
    ''' <returns>切捨てられた文字列</returns>
    ''' <remarks>最後の１バイトが全角文字の半分になる場合、その１バイトは無視される。</remarks>
    Public Function LeftB(ByVal str As String, Optional ByVal Length As Integer = 0) As String

        If str = "" Then
            Return ""
        End If

        'Lengthが0か、バイト数をオーバーする場合は全バイトが指定されたものとみなす。
        Dim RestLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str)

        If Length = 0 OrElse Length > RestLength Then
            Length = RestLength
        End If

        '切捨て
        Dim SJIS As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift-JIS")
        Dim B() As Byte = CType(Array.CreateInstance(GetType(Byte), Length), Byte())

        Array.Copy(SJIS.GetBytes(str), 0, B, 0, Length)

        Dim st1 As String = SJIS.GetString(B)

        '切捨てた結果、最後の１バイトが全角文字の半分だった場合、その半分は切り捨てる。
        Dim ResultLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(st1)

        If Length = ResultLength - 1 Then
            Return st1.Substring(0, st1.Length - 1)
        Else
            Return st1
        End If

    End Function

#End Region
    '2015.04.13 追加END

    '2017.09.20 追加START
#Region "先頭" '"（apostrophe）を取る(ApostropheCut)"
    ''' <summary>先頭" '"を取って返す。</summary>
    ''' <param name="str">対象の文字列</param>
    ''' <returns>切捨てられた文字列</returns>
    ''' <remarks>最初の１バイトが"'"以外はそのまま返す</remarks>
    Public Function ApostropheCut(ByVal str As String) As String

        If Mid(str, 1, 1) <> "'" Then
            Return str
        End If

        str = Mid(str, 2)

        Return str

    End Function

#End Region
    '2017.09.20追加END

#End Region

End Class
