' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH820BLF : 入荷確認データ取込
'  作  成  者       :  umano
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

Imports Jp.Co.Nrs.Com.Utility
'---- ExcelCreatorのインポート開始 -----
Imports AdvanceSoftware.ExcelCreator        'ExcelCreator(本体)
Imports AdvanceSoftware.ExcelCreator.Xlsx   'ExcelCreator(2007以降)
'---- ExcelCreatorのインポート終了 -----

''' <summary>
''' LMH820BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH820BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Const"

    Private Const TABLE_NM_IN As String = "LMH820IN"
    Private Const H_INKAEDI_HED_UTI As String = "LMH820_H_INKAEDI_HED_UTI"
    Private Const H_INKAEDI_DTL_UTI As String = "LMH820_H_INKAEDI_DTL_UTI"

    Private Const WORNING As String = "LMH820_WORNING"
    Private Const DEL As String = "LMH820_DEL"
    Private Const REPETE As String = "LMH820_DELI_REPETE"
    Private Const DEL_DELIVERY_NO As String = "LMH820_DEL_DELIVERY_NO"   'ADD 2018/05/22

    Private Const DATA_KBN_H01 As String = "H1"
    Private Const DATA_KBN_H02 As String = "H2"
    Private Const DATA_KBN_H03 As String = "H3"
    Private Const DATA_KBN_H04 As String = "H4"
    Private Const DATA_KBN_L01 As String = "L1"
    Private Const DATA_KBN_L02 As String = "L2"
    Private Const DATA_KBN_L03 As String = "L3"
    Private Const DATA_KBN_S01 As String = "S1"

    Private Const LEN_HED_PTN As Integer = 243
    Private Const LEN_HED_DELI As Integer = 96
    Private Const LEN_L01 As Integer = 32
    Private Const LEN_L02 As Integer = 346
    Private Const LEN_L03 As Integer = 548
    Private Const LEN_S01 As Integer = 47

    Private Const MODE_OF_TRANS As String = "Air"
    Private Const SHIP_POINT_CPDE As String = "0112"

    Private Const MOJI_CD_SJIS As String = "shift-jis"

    ''' <summary>
    '''データ区分格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _prePreDataKbn As String = String.Empty         '前々行データ区分
    Private _preDataKbn As String = String.Empty            '前行データ区分

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"

    ''' <summary>
    ''' EXCEL用COLUMタイトル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXCEL_COLTITLE_DELI As String = "Delivery №:"
    Public Const EXCEL_COLTITLE_INKAEDI As String = "受信ファイル名"

    ' ADD 2018/01/22 Start ------------------------------------
    'HED 
    Private H_SHIPMENT_NO As String = String.Empty
    Private H4_DELIVERY_NO As String = String.Empty
    Private H_SAP_ODER_NO As String = String.Empty
    Private H_CARRIER_NM As String = String.Empty
    Private H_CONT_TRAILER_ID As String = String.Empty
    Private H4_DATE As String = String.Empty
    Private H3_CODE As String = String.Empty
    Private H3_NAME1 As String = String.Empty
    Private H3_POSTAL_CODE As String = String.Empty
    Private H_SHIP_TO_PARTY_CITY As String = String.Empty
    Private H3_COUNTRY_CODE As String = String.Empty
    Private H_SHIP_COMPLETION_DATE As String = String.Empty
    Private H_PLAN_DELIV_DATE_SHIP As String = String.Empty
    Private H_CUSTOMER_REQ_DELIV_DATE_ORDER As String = String.Empty
    Private S1_SHIPPING_WEIGHT_NET As String = String.Empty
    Private S1_GROSS As String = String.Empty

    Private H_REFERENCE_DOC_NO As String = String.Empty
    Private H_VESSEL_AIRCRAFT_NM As String = String.Empty
    Private H_SHIPPED_FROM_COUNTRY As String = String.Empty
    Private H_ACT_GOODS_ISSUE_TIME As String = String.Empty
    Private H_SHIPTO_CITY As String = String.Empty
    Private H_DEST_COUNTRY As String = String.Empty
    Private H_SHIP_POINT As String = String.Empty
    Private H_SHIP_POINT_NM As String = String.Empty
    Private L2_PLANT_CODE As String = String.Empty
    Private H_PLANT_NM As String = String.Empty

    'DTL
    Private L2_ITEM_CODE As String = String.Empty
    Private L2_NAME_INTERNAL As String = String.Empty
    Private L2_BATCH_NO As String = String.Empty
    Private L2_QUANTITY As String = String.Empty
    Private L2_ORIGIN As String = String.Empty
    Private L2_COMMODITY_CODE As String = String.Empty
    Private L1_SALES_ORDER As String = String.Empty
    Private L3_CLASS_SUBRISK As String = String.Empty
    Private L3_PACKING_GROUP As String = String.Empty
    Private L3_UN_NUMBER As String = String.Empty
    '' ''Private L2_TEMP_CONDITION_TEXT As String = String.Empty
    Private L2_WEIGHT_NET As String = String.Empty
    Private L2_GROSS As String = String.Empty
    Private L2_TEMP_CONDITION_TEXT As String = String.Empty
    Private D_DELIVERY_QTY_IN_EA As String = String.Empty
    Private D_REFERENCE_DOC_NO As String = String.Empty
    Private L2_SHIPPING_POINT_CODE As String = String.Empty

    '対象外データ
    Public Const skipDataAir As String = "07 AIR"      'Delivery Shipping Type
    Public Const skipDataAirCD As String = "07"      'Delivery Shipping Type

    ' ADD 2018/01/22 End ------------------------------------


#End Region

#Region "入荷確認データ取込"
    Private _format As Object

    Private Property Format(p1 As Date, p2 As String) As Object
        Get
            Return _format
        End Get
        Set(value As Object)
            _format = value
        End Set
    End Property

    ''' <summary>
    ''' 出力処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function InkaConfTouroku(ByVal ds As DataSet) As DataSet
        '***TEST20190521
        MyBase.Logger.WriteErrorLog("LMH820BLF", "InkaConfTouroku Start******************************", "")


        Dim rtnDs As DataSet = Nothing
        Dim setBlc As New LMH820BLC

        setBlc = New LMH820BLC

        'ワークフォルダの取得
        '***TEST20190521
        MyBase.Logger.WriteErrorLog("LMH820BLF", "InkaConfTouroku ワークフォルダの取得******************************", "")

        'EDIフォルダパスマスタから取得
        Dim workFolderPath As String = String.Empty
        Dim backupFolderPath As String = String.Empty
        Dim rcvExtention As String = String.Empty
        'rtnDs = MyBase.CallBLC(setBlc, "GetEdiFolderPass", ds)
        Dim dt As DataTable = ds.Tables(LMH820BLF.TABLE_NM_IN)
        Dim dr As DataRow = dt.Rows(0)
        'If MyBase.GetResultCount = 0 Then
        '    Me.SetMessage("E223", New String() {"入荷確認ファイル読み込み元フォルダ"})
        '    Return ds

        'ElseIf String.IsNullOrEmpty(dr.Item("RCV_WORK_INPUT_DIR").ToString().Trim()) = True Then
        '    Me.SetMessage("E237", New String() {"入荷確認ファイル読み込み元フォルダパスが空"})
        '    Return ds
        'Else
        workFolderPath = dr.Item("RCV_WORK_INPUT_DIR").ToString()
        backupFolderPath = dr.Item("BACKUP_INPUT_DIR").ToString()
        rcvExtention = dr.Item("RCV_FILE_EXTENTION").ToString()
        'End If

        '***TEST20190521
        MyBase.Logger.WriteErrorLog("LMH820BLF", "InkaConfTouroku Directory.GetFiles 開始******************************", "")

        Dim files As String() = System.IO.Directory.GetFiles(workFolderPath, _
                                                             String.Concat("*", rcvExtention), _
                                                             System.IO.SearchOption.TopDirectoryOnly)
        '***TEST20190521
        MyBase.Logger.WriteErrorLog("LMH820BLF", "InkaConfTouroku Directory.GetFiles 終了******************************", "")

        If files.Length = 0 Then
            'メッセージセット
            Me.SetMessage("E460")
            Return ds
        End If

        '↓繰り返す
        Dim normalCnt As Integer = 0
        Dim errorCnt As Integer = 0

        Dim setDs As DataSet = Nothing
        Dim setDtHed As DataTable = Nothing
        Dim setDtDtl As DataTable = Nothing

        Dim max As Integer = 0

        Dim insHedCnt As Integer = 0
        Dim insDtlCnt As Integer = 0
        Dim preInsHedCnt As Integer = 0
        Dim preInsDtlCnt As Integer = 0

        Dim setReDr As DataRow = Nothing
        '***TEST20190521
        MyBase.Logger.WriteErrorLog("LMH820BLF", "InkaConfTouroku For Each fileName　開始******************************", "")


        For Each fileName As String In files

            '初期化
            '***TEST20190521
            MyBase.Logger.WriteErrorLog("LMH820BLF", "InkaConfTouroku F初期化　開始******************************", "")

            'dt.Clear()
            ds.Tables("LMH820Result").Clear()
            ds.Tables("LMH820Result").Rows.Add(ds.Tables("LMH820Result").NewRow)
            ds.Tables("LMH820Result").Rows(0)("IsErrorResult") = "0"
            '***TEST20190521
            MyBase.Logger.WriteErrorLog("LMH820BLF", "InkaConfTouroku F初期化　終了******************************", "")
            rtnDs = Me.ReadEDIFile(fileName, ds, workFolderPath)

            max = ds.Tables(LMH820BLF.H_INKAEDI_HED_UTI).Rows.Count - 1

            'koba
            'If ds.Tables(WORNING).Rows.Count = 0 OrElse ds.Tables(WORNING).Rows(0).Item("WORNING_FLG").ToString() <> "1" Then
            '***TEST20190521
            MyBase.Logger.WriteErrorLog("LMH820BLF", "InkaConfTouroku rtnDs = Me.setWorn(rtnDs)******************************", "")

            rtnDs = Me.setWorn(rtnDs)

            'End If

            For i As Integer = 0 To max
                '***TEST20190521
                MyBase.Logger.WriteErrorLog("LMH820BLF", "InkaConfTouroku or i As Integer = 0 To max****************************** " & i, "")

                '別インスタンス
                '***TEST20190521
                MyBase.Logger.WriteErrorLog("LMH820BLF", "InkaConfTouroku 別インスタンス  開始******************************", "")

                setDs = rtnDs.Copy()
                setDtHed = setDs.Tables(LMH820BLF.H_INKAEDI_HED_UTI)
                setDtDtl = setDs.Tables(LMH820BLF.H_INKAEDI_DTL_UTI)


                '***TEST20190521
                MyBase.Logger.WriteErrorLog("LMH820BLF", "InkaConfTouroku 別インスタンス 終了******************************", "")

                '値のクリア
                setDs.Clear()

                '条件の設定
                setDtHed.ImportRow(rtnDs.Tables(LMH820BLF.H_INKAEDI_HED_UTI).Rows(i))

                ''存在チェック(同一ファイル内)
                'If ds.Tables(WORNING).Rows.Count = 0 OrElse String.IsNullOrEmpty(ds.Tables(WORNING).Rows(0).Item("WORNING_FLG").ToString()) = True Then
                '    rtnDs = Me.sameFileDeliCheck(setDs, rtnDs)
                'End If

#If False Then  'DEL 2018/05/23  依頼番号 : 001652   【LMS】DSV_入荷確認ファイル取込時、同オーダー番号時エラー



                '存在チェック(DB)
                setDs = MyBase.CallBLC(setBlc, "GetHinkaEdiHedCheck", setDs)
                If MyBase.GetResultCount >= 1 Then

#If True Then   'UPD 2018/05/22 依頼番号 : 001652   【LMS】DSV_入荷確認ファイル取込時、同オーダー番号時エラー
                    setReDr = rtnDs.Tables(LMH820BLF.DEL_DELIVERY_NO).NewRow()

                    setReDr("NRS_BR_CD") = rtnDs.Tables(LMH820BLF.H_INKAEDI_HED_UTI).Rows(i).Item("NRS_BR_CD")
                    setReDr("CUST_CD_L") = rtnDs.Tables(LMH820BLF.H_INKAEDI_HED_UTI).Rows(i).Item("CUST_CD_L")
                    setReDr("CUST_CD_M") = rtnDs.Tables(LMH820BLF.H_INKAEDI_HED_UTI).Rows(i).Item("CUST_CD_M")
                    setReDr("DELIVERY_NO") = rtnDs.Tables(LMH820BLF.H_INKAEDI_HED_UTI).Rows(i).Item("H4_DELIVERY_NO")

                    rtnDs.Tables(LMH820BLF.DEL_DELIVERY_NO).Rows.Add(setReDr)


#Else
                    '要望番号1730 2013.0115 umano 修正START
                    '既に取り込まれているDelivery№はDEL_KB = "1"を立てて取込む
                    rtnDs.Tables(LMH820BLF.H_INKAEDI_HED_UTI).Rows(i).Item("DEL_KB") = "1"

                    If rtnDs.Tables(LMH820BLF.REPETE).Rows.Count = 0 Then
                        setReDr = rtnDs.Tables(LMH820BLF.REPETE).NewRow()

                        setReDr("REPETE_FLAG") = "1"

                        rtnDs.Tables(LMH820BLF.REPETE).Rows.Add(setReDr)

                    End If

                    For Each drD As DataRow In rtnDs.Tables(LMH820BLF.H_INKAEDI_DTL_UTI).Select(String.Concat("REC_NO = '", rtnDs.Tables(LMH820BLF.H_INKAEDI_HED_UTI).Rows(i).Item("REC_NO"), "'  AND FILE_NAME = '", rtnDs.Tables(LMH820BLF.H_INKAEDI_HED_UTI).Rows(i).Item("FILE_NAME"), "'"))

                        drD("DEL_KB") = "1"

                    Next

                    MyBase.SetMessageStore(LMH820BLF.GUIDANCE_KBN, "E022", New String() {String.Concat(LMH820BLF.EXCEL_COLTITLE_DELI, _
                                                                                         setDs.Tables(LMH820BLF.H_INKAEDI_HED_UTI).Rows(0).Item("H4_DELIVERY_NO").ToString())}, _
                                                                                         setDs.Tables(LMH820BLF.H_INKAEDI_HED_UTI).Rows(0).Item("REC_NO").ToString(), _
                                                                                         LMH820BLF.EXCEL_COLTITLE_INKAEDI, _
                                                                                         fileName)
                    '要望番号1730 2013.0115 umano 修正END

#End If

                End If

#End If

                '***TEST20190521
                MyBase.Logger.WriteErrorLog("LMH820BLF", "InkaConfTouroku or i As Integer = 0 To max  Next******************************", "")

            Next
            '***TEST20190521
            MyBase.Logger.WriteErrorLog("LMH820BLF", "InkaConfTouroku For Each fileName　終了******************************", "")

            'If rtnDs.Tables(LMH820BLF.WORNING).Rows.Count <> 0 Then
            '    Return ds
            'End If

            '要望番号1730 2013.0115 umano 修正START
            ''存在チェックでエラーがある場合、処理終了
            'If MyBase.IsMessageStoreExist() = True Then
            '    Return ds
            'End If
            '要望番号1730 2013.0115 umano 修正END

            '***TEST20190521
            MyBase.Logger.WriteErrorLog("LMH820BLF", "InkaConfTouroku Start トランザクション開始", "")

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

#If True Then   'ADD 2018/05/22 依頼番号 : 001652   【LMS】DSV_入荷確認ファイル取込時、同オーダー番号時エラー
                ' 取込済みDELIVERY_NOを削除する（DEL_KB = "1"　設定）
                '***TEST20190521
                MyBase.Logger.WriteErrorLog("LMH820BLF", "DeleteHInkaEdiHedUti Start", "")

                'UTI入荷確認データ(HED)差削除処理
                rtnDs = MyBase.CallBLC(setBlc, "DeleteHInkaEdiHedUti", rtnDs)
                '***TEST20190521
                MyBase.Logger.WriteErrorLog("LMH820BLF", "DeleteHInkaEdiDtlUti Start", "")

                'UTI入荷確認データ(DTL)削除処理
                rtnDs = MyBase.CallBLC(setBlc, "DeleteHInkaEdiDtlUti", rtnDs)

#End If
                '***TEST20190521
                MyBase.Logger.WriteErrorLog("LMH820BLF", "InsertHInkaEdiHedUti Start", "")
                'UTI入荷確認データ(HED)登録処理
                rtnDs = MyBase.CallBLC(setBlc, "InsertHInkaEdiHedUti", rtnDs)

                '***TEST20190521
                MyBase.Logger.WriteErrorLog("LMH820BLF", "InsertHInkaEdiDtlUti Start", "")

                'UTI入荷確認データ(DTL)登録処理
                rtnDs = MyBase.CallBLC(setBlc, "InsertHInkaEdiDtlUti", rtnDs)

                '***TEST20190521
                MyBase.Logger.WriteErrorLog("LMH820BLF", "コミット", "")

                'コミット
                If rtnDs.Tables("LMH820Result").Rows(0)("IsErrorResult").ToString = "0" Then
                    'トランザクション終了
                    MyBase.CommitTransaction(scope)

                    ''ファイルをバックアップへコピー
                    'Call Me.CopyAndDelete(fileName, backupFolderPath, rtnDs)
                    normalCnt = normalCnt + 1
                Else
                    'エラー行が発生した時点でコミットは行わない
                    '但し、エラーチェックは行う
                    errorCnt = errorCnt + 1
                    Continue For
                    'Return rtnDs

                End If

            End Using

            insHedCnt = Convert.ToInt32(rtnDs.Tables("LMH820Result").Rows(0).Item("INS_HED_CNT"))
            insDtlCnt = Convert.ToInt32(rtnDs.Tables("LMH820Result").Rows(0).Item("INS_DTL_CNT"))

            rtnDs.Tables("LMH820Result").Rows(0).Item("INS_HED_CNT") = preInsHedCnt + insHedCnt
            rtnDs.Tables("LMH820Result").Rows(0).Item("INS_DTL_CNT") = preInsDtlCnt + insDtlCnt

            preInsHedCnt = Convert.ToInt32(rtnDs.Tables("LMH820Result").Rows(0).Item("INS_HED_CNT"))
            preInsDtlCnt = Convert.ToInt32(rtnDs.Tables("LMH820Result").Rows(0).Item("INS_DTL_CNT"))

        Next

        rtnDs.Tables("LMH820Result").Rows(0).Item("NORMALCNT") = Convert.ToString(normalCnt)
        rtnDs.Tables("LMH820Result").Rows(0).Item("ERRORCNT") = Convert.ToString(errorCnt)

        '***TEST20190521
        MyBase.Logger.WriteErrorLog("LMH820BLF", "InkaConfTouroku End", "")

        Return rtnDs

    End Function

    '''' <summary>
    '''' 同一ファイル内Delivery №チェック
    '''' </summary>
    '''' <param name="ds"></param>
    '''' <returns>DataSet</returns>
    '''' <remarks></remarks>
    Private Function setWorn(ByVal ds As DataSet) As DataSet
        Dim deli As String = String.Empty
        Dim deliSAP_ODER_NO  As String = String.Empty       'ADD 2018/10/11
        Dim setDr As DataRow
        Dim dtWorn As DataTable = ds.Tables(LMH820BLF.WORNING)
        Dim dtDel As DataTable = ds.Tables(LMH820BLF.DEL)
        Dim fileDeliNo As String = String.Empty
        Dim fileRecNo As String = String.Empty

        'Dim fileCrtDate As String = String.Empty
        Dim fileFileNm As String = String.Empty
#If False Then      'UPD 2018/10/11 SAP_ODER_NO追加
        For Each drH As DataRow In ds.Tables(LMH820BLF.H_INKAEDI_HED_UTI).Select("", "H4_DELIVERY_NO")

            If String.IsNullOrEmpty(deli) = False AndAlso deli.Equals(drH("H4_DELIVERY_NO").ToString()) = True  Then

#Else
        For Each drH As DataRow In ds.Tables(LMH820BLF.H_INKAEDI_HED_UTI).Select("", "H4_DELIVERY_NO,SAP_ODER_NO")

            If String.IsNullOrEmpty(deli) = False AndAlso deli.Equals(drH("H4_DELIVERY_NO").ToString()) = True _
                AndAlso deli.Equals(drH("SAP_ODER_NO").ToString()) = True Then

#End If

                fileDeliNo = drH("H4_DELIVERY_NO").ToString()
                fileRecNo = drH("REC_NO").ToString()
                'fileCrtDate = MyBase.GetSystemDate()
                fileFileNm = drH("FILE_NAME").ToString()

                For Each drD As DataRow In ds.Tables(LMH820BLF.H_INKAEDI_DTL_UTI).Select(String.Concat("REC_NO = '", fileRecNo, "'  AND FILE_NAME = '", fileFileNm, "'"))
                    setDr = dtWorn.NewRow()

                    setDr("H4_DELIVERY_NO") = fileDeliNo
                    setDr("CRT_DATE") = MyBase.GetSystemDate()
                    setDr("FILE_NAME") = fileFileNm
                    setDr("REC_NO") = fileRecNo
                    setDr("GYO") = drD("GYO").ToString()
                    setDr("L3_UN_NUMBER") = drD("L3_UN_NUMBER").ToString()
                    setDr("L3_PROPER_SHIP_NAME") = drD("L3_PROPER_SHIP_NAME").ToString()
                    setDr("L3_CLASS_SUBRISK") = drD("L3_CLASS_SUBRISK").ToString()
                    setDr("L3_PACKING_GROUP") = drD("L3_PACKING_GROUP").ToString()
                    setDr("L3_FLASH_POINT_FOR_IMO") = drD("L3_FLASH_POINT_FOR_IMO").ToString()
                    setDr("L2_EXPORT_RESTRICT_NEW") = drD("L2_EXPORT_RESTRICT_NEW").ToString()


                    dtWorn.Rows.Add(setDr)
                Next

            End If
            deli = drH("H4_DELIVERY_NO").ToString()
            deliSAP_ODER_NO = drH("SAP_ODER_NO").ToString()
        Next

        'Dim wornDeli As String = String.Empty
        'Dim wornGyo As String = String.Empty
        'Dim wornUn As String = String.Empty
        'Dim wornPsNm As String = String.Empty
        'Dim wornClass As String = String.Empty
        'Dim wornFp As String = String.Empty
        'Dim wornEl As String = String.Empty

        For Each drW As DataRow In dtWorn.Select("", "H4_DELIVERY_NO,CRT_DATE,GYO")

            'If String.IsNullOrEmpty(wornDeli) = False AndAlso wornDeli.Equals(drW("H4_DELIVERY_NO").ToString()) = True AndAlso _
            '   wornGyo.Equals(drW("GYO").ToString()) = True AndAlso wornUn.Equals(drW("L3_UN_NUMBER").ToString()) = True AndAlso _
            '   wornPsNm.Equals(drW("L3_PROPER_SHIP_NAME").ToString()) = True AndAlso wornClass.Equals(drW("L3_CLASS_SUBRISK").ToString()) = True AndAlso _
            '   wornFp.Equals(drW("L3_FLASH_POINT_FOR_IMO").ToString()) = True AndAlso wornEl.Equals(drW("L2_EXPORT_RESTRICT_NEW").ToString()) = True Then

            setDr = dtDel.NewRow()

            setDr("CRT_DATE") = drW("CRT_DATE").ToString()
            setDr("FILE_NAME") = drW("FILE_NAME").ToString()
            setDr("REC_NO") = drW("REC_NO").ToString()
            setDr("GYO") = drW("GYO").ToString()

            dtDel.Rows.Add(setDr)

            'End If
            'wornDeli = drW("H4_DELIVERY_NO").ToString()
            'wornGyo = drW("GYO").ToString()
            'wornUn = drW("L3_UN_NUMBER").ToString()
            'wornPsNm = drW("L3_PROPER_SHIP_NAME").ToString()
            'wornClass = drW("L3_CLASS_SUBRISK").ToString()
            'wornFp = drW("L3_FLASH_POINT_FOR_IMO").ToString()
            'wornEl = drW("L2_EXPORT_RESTRICT_NEW").ToString()
        Next

        Return ds

    End Function

#Region "チェック"

    '''' <summary>
    '''' 同一ファイル内Delivery №チェック
    '''' </summary>
    '''' <param name="setDs"></param>
    '''' <param name="rtnDs"></param>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Private Function sameFileDeliCheck(ByVal setDs As DataSet, ByVal rtnDs As DataSet) As DataSet

    '    Dim tagDeliNo As String = setDs.Tables(H_INKAEDI_HED_UTI).Rows(0).Item("H4_DELIVERY_NO").ToString()
    '    Dim tagRecNo As String = setDs.Tables(H_INKAEDI_HED_UTI).Rows(0).Item("REC_NO").ToString()
    '    Dim fileDeliNo As String = String.Empty
    '    Dim fileRecNo As String = String.Empty
    '    Dim max As Integer = rtnDs.Tables(H_INKAEDI_HED_UTI).Rows.Count - 1
    '    Dim setDr As DataRow

    '    Dim dtWorn As DataTable = rtnDs.Tables(LMH820BLF.WORNING)
    '    Dim maxW As Integer = -1

    '    For i As Integer = 0 To max

    '        fileDeliNo = rtnDs.Tables(LMH820BLF.H_INKAEDI_HED_UTI).Rows(i).Item("H4_DELIVERY_NO").ToString()
    '        fileRecNo = rtnDs.Tables(LMH820BLF.H_INKAEDI_HED_UTI).Rows(i).Item("REC_NO").ToString()
    '        fileCrtDate = rtnDs.Tables(LMH820BLF.H_INKAEDI_HED_UTI).Rows(i).Item("CRT_DATE").ToString()
    '        fileFileNm = rtnDs.Tables(LMH820BLF.H_INKAEDI_HED_UTI).Rows(i).Item("FILE_NAME").ToString()

    '        If tagDeliNo.Equals(fileDeliNo) = True AndAlso tagRecNo.Equals(fileRecNo) = False Then

    '            'If maxW < 0 Then

    '            setDr = dtWorn.NewRow()

    '            For Each dr As DataRow In rtnDs.Tables(LMH820BLF.H_INKAEDI_DTL_UTI).Select("CRT_DATE = '", fileCrtDate, "'  AND FILE_NAME = '", fileFileNm, "'AND REC_NO = '", fileRecNo, ", "")

    '                setDr("H4_DELIVERY_NO") = fileDeliNo
    '                setDr("CRT_DATE") = fileCrtDate
    '                setDr("FILE_NAME") = fileFileNm
    '                setDr("REC_NO") = fileRecNo
    '                setDr("GYO") = dr("GYO").ToString()
    '                setDr("L3_UN_NUMBER") = dr("L3_UN_NUMBER").ToString()
    '                setDr("L3_PROPER_SHIP_NAME") = dr("L3_PROPER_SHIP_NAME").ToString()
    '                setDr("L3_CLASS_SUBRISK") = dr("L3_CLASS_SUBRISK").ToString()
    '                setDr("L3_PACKING_GROUP") = dr("L3_PACKING_GROUP").ToString()
    '                setDr("L3_FLASH_POINT_FOR_IMO") = dr("L3_FLASH_POINT_FOR_IMO").ToString()
    '                setDr("L2_EXPORT_RESTRICT_NEW") = dr("L2_EXPORT_RESTRICT_NEW").ToString()

    '            Next

    '            dtWorn.Rows.Add(setDr)

    '            'maxW = dtWorn.Rows.Count - 1

    '            'If tagDeliNo.Equals(dtWorn.Rows(j).Item("H4_DELIVERY_NO")) = True Then

    '            'End If
    '            ''Else
    '            ''    For j As Integer = 0 To maxW

    '            ''        If tagDeliNo.Equals(dtWorn.Rows(j).Item("H4_DELIVERY_NO")) = False Then

    '            ''            'If tagDeliNo.Equals(dtWorn.Rows(j).Item("H4_DELIVERY_NO")) = False AndAlso _
    '            ''            '   (tagDeliNo.Equals(dtWorn.Rows(j).Item("H4_DELIVERY_NO")) = True AndAlso _
    '            ''            '    fileRecNo.Equals(dtWorn.Rows(j).Item("REC_NO")) = True) Then

    '            ''            setDr = dtWorn.NewRow()

    '            ''            setDr("H4_DELIVERY_NO") = fileDeliNo
    '            ''            setDr("REC_NO") = fileRecNo

    '            ''            dtWorn.Rows.Add(setDr)


    '            ''        End If

    '            ''    Next

    '            ''    maxW = dtWorn.Rows.Count - 1

    '            ''End If

    '        End If

    '    Next

    '    Return rtnDs

    'End Function

    ''' <summary>
    ''' レコード長チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="dataKbn"></param>
    ''' <param name="line"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsCorrectDataLength(ByVal ds As DataTable, ByVal dataKbn As String, _
                                         ByVal line As String, ByVal rcdCnt As Integer, _
                                         ByVal filePath As String) As Boolean

        Dim lineLength As Integer = line.Length

        Select Case dataKbn

            Case LMH820BLF.DATA_KBN_H01, LMH820BLF.DATA_KBN_H02, LMH820BLF.DATA_KBN_H03

                If lineLength <> LMH820BLF.LEN_HED_PTN Then
                    MyBase.SetMessageStore(LMH820BLF.GUIDANCE_KBN, "E518", New String() {String.Concat("レコード行：", rcdCnt), String.Concat("ファイル名：", filePath)})
                    Return False
                End If

            Case LMH820BLF.DATA_KBN_H04

                If lineLength <> LMH820BLF.LEN_HED_DELI Then
                    MyBase.SetMessageStore(LMH820BLF.GUIDANCE_KBN, "E518", New String() {String.Concat("レコード行：", rcdCnt), String.Concat("ファイル名：", filePath)})
                    Return False
                End If

            Case LMH820BLF.DATA_KBN_L01

                If lineLength <> LMH820BLF.LEN_L01 Then
                    MyBase.SetMessageStore(LMH820BLF.GUIDANCE_KBN, "E518", New String() {String.Concat("レコード行：", rcdCnt), String.Concat("ファイル名：", filePath)})
                    Return False
                End If

            Case LMH820BLF.DATA_KBN_L02

                If lineLength <> LMH820BLF.LEN_L02 Then
                    MyBase.SetMessageStore(LMH820BLF.GUIDANCE_KBN, "E518", New String() {String.Concat("レコード行：", rcdCnt), String.Concat("ファイル名：", filePath)})
                    Return False
                End If

                'データ不整合になってしまう為、一旦コメントSTART
                'Case LMH820BLF.DATA_KBN_L03

                '    If lineLength <> LMH820BLF.LEN_L03 Then
                '        MyBase.SetMessageStore(LMH820BLF.GUIDANCE_KBN, "E518", New String() {String.Concat("レコード行：", rcdCnt), String.Concat("ファイル名：", filePath)})
                '        Return False
                '    End If
                'データ不整合になってしまう為、一旦コメントEND

            Case LMH820BLF.DATA_KBN_S01

                If lineLength <> LMH820BLF.LEN_S01 Then
                    MyBase.SetMessageStore(LMH820BLF.GUIDANCE_KBN, "E518", New String() {String.Concat("レコード行：", rcdCnt), String.Concat("ファイル名：", filePath)})
                    Return False
                End If

        End Select

        Return True

    End Function

#End Region

#Region "ReadEDIFile"

#If False Then  'UPD 2018/01/19 依頼番号 000963 テキスト(.txt)からエクセルに変更

    ''' <summary>
    ''' ファイル読込み
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ReadEDIFile(ByVal filePath As String, ByVal ds As DataSet, ByVal workFolderPath As String) As DataSet

        Dim rtnBln As Boolean = True

        Dim recNo As Integer = 0            'ヘッダー数
        Dim gyo As Integer = 0              '明細数
        Dim rcdCnt As Integer = 0           'ファイル内レコード数

        'データの型(ユーティーアイ)
        Dim txt As String = String.Empty
        Dim line As String = String.Empty
        Dim sr As New System.IO.StreamReader(filePath, _
            System.Text.Encoding.GetEncoding(LMH820BLF.MOJI_CD_SJIS))

        Dim dtIn As DataTable = ds.Tables(LMH820BLF.TABLE_NM_IN)  'INTBL
        Dim dtRcvhed As DataTable = ds.Tables(LMH820BLF.H_INKAEDI_HED_UTI)  'EDI受信HED
        Dim dtRcvDtl As DataTable = ds.Tables(LMH820BLF.H_INKAEDI_DTL_UTI)  'EDI受信DTL

        Dim dataKbn As String = String.Empty
        Dim airFlag As Boolean = False

        '内容を一行ずつ読み込む
        sr.Peek()

        Do Until sr.EndOfStream

            Dim rowLen As Integer = 0
            Dim L1Flg As Boolean = False        'L1レコードを再設定するかのフラグ
            '1行単位の読み込み
            line = sr.ReadLine()

            rcdCnt = rcdCnt + 1

            dataKbn = SubStringOfByte(line, 1, 2) 'データ構造により処理の判断を行う（H1,H2,H3,H4:ヘッダー、L1:明細、L2,L3,L4:ライン）

            Select Case dataKbn

                Case LMH820BLF.DATA_KBN_H01, LMH820BLF.DATA_KBN_H02, LMH820BLF.DATA_KBN_H03, _
                     LMH820BLF.DATA_KBN_H04, LMH820BLF.DATA_KBN_S01

                    If dataKbn.Equals(LMH820BLF.DATA_KBN_H01) = True Then
                        '採番
                        recNo = recNo + 1
                        gyo = 0
                        'SHINODA 要望管理2235対応 Start 
                        airFlag = False
                        'SHINODA 要望管理2235対応 E n d 
                    End If

                    ''ヘッダーチェック
                    'If Me.IsCorrectDataLength(dtRcvhed, dataKbn, line, rcdCnt, filePath) = False Then
                    '    rtnBln = False
                    '    Exit Do
                    'End If

                    '中間ファイル情報のセット(UTI受信HED)
                    'SHINODA 要望管理2235対応 Start 
                    If airFlag = False Then
                        'Me.SetHeaderRow(dtIn, dtRcvhed, line, dataKbn, recNo, filePath, workFolderPath)
                        Me.SetHeaderRow(dtIn, dtRcvhed, line, dataKbn, recNo, filePath, workFolderPath, airFlag)
                    End If
                    'SHINODA 要望管理2235対応 E n d 

                Case LMH820BLF.DATA_KBN_L01, LMH820BLF.DATA_KBN_L02, LMH820BLF.DATA_KBN_L03

                    'SHINODA 要望管理2235対応 Start
                    If airFlag = False Then
                        If Me._preDataKbn.Equals(LMH820BLF.DATA_KBN_H04) = True AndAlso _
                            dataKbn.Equals(LMH820BLF.DATA_KBN_L01) = True Then
                            gyo = gyo + 1
                        ElseIf Me._preDataKbn.Equals(LMH820BLF.DATA_KBN_L03) = True AndAlso _
                           dataKbn.Equals(LMH820BLF.DATA_KBN_L02) = True Then
                            gyo = gyo + 1
                            L1Flg = True
                        ElseIf Me._preDataKbn.Equals(LMH820BLF.DATA_KBN_L03) = True AndAlso _
                           dataKbn.Equals(LMH820BLF.DATA_KBN_L01) = True Then
                            gyo = gyo + 1
                            L1Flg = True
                        End If

                        ''明細チェック
                        'If Me.IsCorrectDataLength(dtRcvDtl, dataKbn, line, rcdCnt, filePath) = False Then
                        '    rtnBln = False
                        '    Exit Do
                        'End If

                        '中間ファイル情報のセット(UTI受信DTL)
                        Me.SetDetailRow(dtIn, dtRcvDtl, line, dataKbn, recNo, filePath, gyo, L1Flg, workFolderPath)                    '明細チェック

                    End If
                    'SHINODA 要望管理2235対応 E n d 

                    'データ区分が存在しない
                Case String.Empty

                    MyBase.SetMessageStore(LMH820BLF.GUIDANCE_KBN, "E223", New String() {String.Concat("ファイル名：", filePath, "レコード行：", rcdCnt, "レコード識別子")})
                    'Me.SetMessage("E223", New String() {String.Concat("ファイル名：", filePath, "レコード行：", rcdCnt, "レコード識別子")})

                    Exit Do

                    'レコード識別子が無効
                Case Else
                    MyBase.SetMessageStore(LMH820BLF.GUIDANCE_KBN, "E268", New String() {String.Concat("ファイル名：", filePath, "レコード行：", rcdCnt, "レコード識別子：", dataKbn)})
                    'Me.SetMessage("E268", New String() {String.Concat("ファイル名：", filePath, "レコード行：", rcdCnt, "レコード識別子：", dataKbn)})

                    Exit Do

            End Select

        Loop

        '閉じる
        sr.Close()

        Return ds

    End Function

#Else

    ''' <summary>
    ''' ファイル読込み
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ReadEDIFile(ByVal filePath As String, ByVal ds As DataSet, ByVal workFolderPath As String) As DataSet

        Dim exlDt As DataTable

        Dim cntAll As Integer = 0
        Dim errFlg As Boolean = False

        Dim dtIn As DataTable = ds.Tables(LMH820BLF.TABLE_NM_IN)  'INTBL
        Dim dtRcvhed As DataTable = ds.Tables(LMH820BLF.H_INKAEDI_HED_UTI)  'EDI受信HED
        Dim dtRcvDtl As DataTable = ds.Tables(LMH820BLF.H_INKAEDI_DTL_UTI)  'EDI受信DTL

        'エラーファイル用データテーブル作成
        Dim errDt As New DataTable
        errDt = CreateErrorDataTable()

        Try

            exlDt = CreateExcelDataTable()

            '---------- ExcelCreatorでExcelファイルを読み込み開始 -------
            '?ExcelCreator?インスタンス生成
            Dim components As New System.ComponentModel.Container()
            Dim excelCreator As New XlsxCreator(components)

            ' Excel ファイル (インポートファイル) を読み取り専用でオープンします。
            excelCreator.ReadBook(filePath)
            ' シート名からシート番号を取得
            'Dim sheetNo As Integer = excelCreator.SheetNo2(WNG010CO.EXCEL_SHEET_NAME)
            Dim sheetNo As Integer = 1

            ' 該当のシートをアクティブ化
            excelCreator.ActiveSheet = sheetNo

            ' データが設定されたセルの最大行と最大列の交点座標を取得します。
            Dim maxData As System.Drawing.Point = excelCreator.GetMaxData(AdvanceSoftware.ExcelCreator.MaxEndPoint.MaxPoint)

            '読み込むExcelのカラムを設定

            Dim exlRow As DataRow

            'Excelデータの読み込み（データ設定された行を全て読み込む）
            For row As Integer = 1 To maxData.Y + 1
                ' ヘッダ項目名をカラム名に設定
                If row = 1 Then
                    exlDt.Columns.Add(CStr(excelCreator.Cell("A" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("B" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("C" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("D" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("E" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("F" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("G" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("H" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("I" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("J" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("K" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("L" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("M" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("N" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("O" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("P" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("Q" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("R" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("S" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("T" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("U" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("V" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("W" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("X" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("Y" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("Z" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("AA" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("AB" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("AC" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("AD" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("AE" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("AF" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("AG" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("AH" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("AI" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("AJ" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("AK" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("AL" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("AM" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("AN" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("AO" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("AP" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("AQ" & row).Value), Type.GetType("System.String"))
                    exlDt.Columns.Add(CStr(excelCreator.Cell("AR" & row).Value), Type.GetType("System.String"))
                    Continue For
                End If
                ' データテーブルにExcelのデータを設定
                exlRow = exlDt.NewRow()
                exlRow(0) = CStr(excelCreator.Cell("A" & row).Value)
                exlRow(1) = CStr(excelCreator.Cell("B" & row).Value)
                exlRow(2) = CStr(excelCreator.Cell("C" & row).Value)
                exlRow(3) = CStr(excelCreator.Cell("D" & row).Value)
                exlRow(4) = CStr(excelCreator.Cell("E" & row).Value)
                exlRow(5) = CStr(excelCreator.Cell("F" & row).Value)
                exlRow(6) = CStr(excelCreator.Cell("G" & row).Value)

                Dim dateString As String = CStr(excelCreator.Cell("H" & row).Str)
                If String.IsNullOrEmpty(dateString) Then
                    dateString = String.Empty
                ElseIf IsNumeric(dateString) Then
                    dateString = Strings.Format(DateTime.FromOADate(CDbl(excelCreator.Cell("H" & row).Value)), "yyyyMMdd")
                End If
                exlRow(7) = dateString

                exlRow(8) = CStr(excelCreator.Cell("I" & row).Value)
                exlRow(9) = CStr(excelCreator.Cell("J" & row).Value)
                exlRow(10) = CStr(excelCreator.Cell("K" & row).Value)
                exlRow(11) = CStr(excelCreator.Cell("L" & row).Value)
                exlRow(12) = CStr(excelCreator.Cell("M" & row).Value)
                exlRow(13) = CStr(excelCreator.Cell("N" & row).Value)
                exlRow(14) = CStr(excelCreator.Cell("O" & row).Value)
                exlRow(15) = CStr(excelCreator.Cell("P" & row).Value)
                exlRow(16) = CStr(excelCreator.Cell("Q" & row).Value)
                exlRow(17) = CStr(excelCreator.Cell("R" & row).Value)
                exlRow(18) = CStr(excelCreator.Cell("S" & row).Value)
                exlRow(19) = CStr(excelCreator.Cell("T" & row).Value)
                exlRow(20) = CStr(excelCreator.Cell("U" & row).Value)
                exlRow(21) = CStr(excelCreator.Cell("V" & row).Value)
                exlRow(22) = CStr(excelCreator.Cell("W" & row).Value)

                'ShipmentCompletionDate
                ''exlRow(23) = CStr(excelCreator.Cell("X" & row).Value)
                dateString = CStr(excelCreator.Cell("X" & row).Str)
                If String.IsNullOrEmpty(dateString) Then
                    dateString = String.Empty
                ElseIf IsNumeric(dateString) Then
                    dateString = Strings.Format(DateTime.FromOADate(CDbl(excelCreator.Cell("X" & row).Value)), "yyyyMMdd")
                End If
                exlRow(23) = dateString

                'PlannedDelivDate_Shipment
                ''exlRow(24) = CStr(excelCreator.Cell("Y" & row).Value)
                dateString = CStr(excelCreator.Cell("Y" & row).Str)
                If String.IsNullOrEmpty(dateString) Then
                    dateString = String.Empty
                ElseIf IsNumeric(dateString) Then
                    dateString = Strings.Format(DateTime.FromOADate(CDbl(excelCreator.Cell("Y" & row).Value)), "yyyyMMdd")
                End If
                exlRow(24) = dateString

                'CustomerReqDelivDate_Order
                '' exlRow(25) = CStr(excelCreator.Cell("Z" & row).Value)
                dateString = CStr(excelCreator.Cell("Z" & row).Str)
                If String.IsNullOrEmpty(dateString) Then
                    dateString = String.Empty
                ElseIf IsNumeric(dateString) Then
                    dateString = Strings.Format(DateTime.FromOADate(CDbl(excelCreator.Cell("Z" & row).Value)), "yyyyMMdd")
                End If
                exlRow(25) = dateString

                exlRow(26) = CStr(excelCreator.Cell("AA" & row).Value)
                exlRow(27) = CStr(excelCreator.Cell("AB" & row).Value)
                exlRow(28) = CStr(excelCreator.Cell("AC" & row).Value)
                exlRow(29) = CStr(excelCreator.Cell("AD" & row).Value)
                exlRow(30) = CStr(excelCreator.Cell("AE" & row).Value)
                exlRow(31) = CStr(excelCreator.Cell("AF" & row).Value)
                exlRow(32) = CStr(excelCreator.Cell("AG" & row).Value)
                exlRow(33) = CStr(excelCreator.Cell("AH" & row).Value)
                exlRow(34) = CStr(excelCreator.Cell("AI" & row).Value)

                'ActualGoodsIssueTime
                ''exlRow(35) = CStr(excelCreator.Cell("AJ" & row).Value)
                dateString = CStr(excelCreator.Cell("AJ" & row).Str)
                If String.IsNullOrEmpty(dateString) Then
                    dateString = String.Empty
                ElseIf IsNumeric(dateString) Then
                    dateString = Strings.Format(DateTime.FromOADate(CDbl(excelCreator.Cell("AJ" & row).Value)), "HH:mm:ss")
                End If
                exlRow(35) = dateString

                exlRow(36) = CStr(excelCreator.Cell("AK" & row).Value)
                exlRow(37) = CStr(excelCreator.Cell("AL" & row).Value)
                exlRow(38) = CStr(excelCreator.Cell("AM" & row).Value)
                exlRow(39) = CStr(excelCreator.Cell("AN" & row).Value)
                exlRow(40) = CStr(excelCreator.Cell("AO" & row).Value)
                exlRow(41) = CStr(excelCreator.Cell("AP" & row).Value)
                exlRow(42) = CStr(excelCreator.Cell("AQ" & row).Value)
                exlRow(43) = CStr(excelCreator.Cell("AR" & row).Value)

                exlDt.Rows.Add(exlRow)

            Next

            ' ブックをクリアします。
            excelCreator.CloseBook(True)
            '---------- ExcelCreatorでExcelファイルを読み込み終了 -------

            errDt = CreateErrorDataTable()
            'impDt = CreateImportDataTable()

            Dim oleDELIVERY_NO As String = String.Empty
            Dim chkDELIVERY_NO As String = String.Empty     'ADD 2019/09/13

            Dim recNo As Integer = 0            'ヘッダー数
            Dim gyo As Integer = 0              '明細数
            Dim rcdCnt As Integer = 0           'ファイル内レコード数

#If True Then    'ADD 2018/06/22 依頼番号 : 001684  ヘッダーデータだけの明細用に退避
            Dim exlDrHED As DataRow = Nothing
#End If
            For Each exlDr As DataRow In exlDt.Rows
                Dim contDt As New DataTable
                errFlg = False

                'UPD 2018/04/10 パッケージの商品が、中身は取り込まない（SAPOrderNoが""）
                If exlDr(1).ToString = "" _
                    Or exlDr(2).ToString = "" Then
                    '2項目(Delivary)が空の場合取り込まない
                    '3項目(SAPOrderNo)が空の場合取り込まない

                    Continue For
                Else

                    cntAll = cntAll + 1

                    '入力ファイルのチェック
                    'If IsErrImportFileChk(exlDr, contDt, errDt) = True Then
                    '    errFlg = True
                    'End If

                    Dim keyKbnNm As String = "KBN_GROUP_CD='S040' AND KBN_NM6 = '" & exlDr(0).ToString & "'"

                    'デリバリーNo. New-OLD チェック
                    'UPD 2018/03/27 SAPOrderNoで処理している　→　デリバリーNo. 
                    'If oleDELIVERY_NO <> exlDr(2).ToString.Trim Then
                    'Delivery + SAPOrderNo
                    chkDELIVERY_NO = String.Concat(exlDr(1).ToString.Trim, exlDr(2).ToString.Trim)  'ADD 2019/09/13

                    'If oleDELIVERY_NO <> exlDr(1).ToString.Trim Then
                    If oleDELIVERY_NO <> chkDELIVERY_NO Then            'ADD 2019/09/13
#If True Then           'ADD 2018/06/22 依頼番号 : 001684  ヘッダーデータだけの時明細作成
                        If String.Empty.Equals(oleDELIVERY_NO) = False _
                            And gyo = 0 Then

                            gyo = gyo + 1
                            '中間ファイル情報のセット(UTI受信DTL)
                            Me.SetDetailRow(dtIn, dtRcvDtl, exlDrHED, recNo, filePath, gyo, workFolderPath)                    '明細作成

                        End If

#End If

                        '採番
                        recNo = recNo + 1
                        gyo = 0

                        Me.SetHeaderRow(dtIn, dtRcvhed, exlDr, recNo, filePath, workFolderPath)
                        'oleDELIVERY_NO = exlDr(1).ToString.Trim
                        oleDELIVERY_NO = String.Concat(exlDr(1).ToString.Trim, exlDr(2).ToString.Trim)  'ADD 2019/09/13

#If True Then       'ADD 2018/06/22 依頼番号 : 001684  ヘッダーデータだけの明細用に退避
                        exlDrHED = exlDr
#End If
                    Else
                        gyo = gyo + 1

                        '中間ファイル情報のセット(UTI受信DTL)
                        Me.SetDetailRow(dtIn, dtRcvDtl, exlDr, recNo, filePath, gyo, workFolderPath)                    '明細チェック

                    End If

                End If
            Next

#If True Then           'ADD 2018/06/22 依頼番号 : 001684  ヘッダーデータだけの時明細作成
            If gyo = 0 Then
                gyo = gyo + 1

                '中間ファイル情報のセット(UTI受信DTL)
                Me.SetDetailRow(dtIn, dtRcvDtl, exlDrHED, recNo, filePath, gyo, workFolderPath)                    '明細作成

            End If

#End If

            Return ds

        Catch ex As Exception
            ' 例外が発生した時の処理
            'MyBase.SetMessageArea(outMdl, errDivArea, "S001", navArea, New String() {"Processing of upload"})
            'Return False
        End Try

        Return ds
        '----------------------------------------------------------

        ''Dim rtnBln As Boolean = True

        ''Dim recNo As Integer = 0            'ヘッダー数
        ''Dim gyo As Integer = 0              '明細数
        ''Dim rcdCnt As Integer = 0           'ファイル内レコード数

        ' ''データの型(ユーティーアイ)
        ''Dim txt As String = String.Empty
        ''Dim line As String = String.Empty
        ''Dim sr As New System.IO.StreamReader(filePath, _
        ''    System.Text.Encoding.GetEncoding(LMH820BLF.MOJI_CD_SJIS))

        ''Dim dtIn As DataTable = ds.Tables(LMH820BLF.TABLE_NM_IN)  'INTBL
        ''Dim dtRcvhed As DataTable = ds.Tables(LMH820BLF.H_INKAEDI_HED_UTI)  'EDI受信HED
        ''Dim dtRcvDtl As DataTable = ds.Tables(LMH820BLF.H_INKAEDI_DTL_UTI)  'EDI受信DTL

        ''Dim dataKbn As String = String.Empty
        ''Dim airFlag As Boolean = False

        ' ''---------------------------------------
        ''Dim oleDELIVERY_NO As String = String.Empty
        ''Dim rowMax As Integer = 100
        ''Dim colMax As Integer = 38

        ''Dim exlDt As DataTable
        ''exlDt = CreateExcelDataTable()

        ' ''---------- ExcelCreatorでExcelファイルを読み込み開始 -------
        ' ''?ExcelCreator?インスタンス生成
        ''Dim components As New System.ComponentModel.Container()
        ''Dim excelCreator As New XlsxCreator(components)


        ' '' Excel ファイル (インポートファイル) を読み取り専用でオープンします。
        ''excelCreator.ReadBook(filePath)
        ' '' シート名からシート番号を取得
        ''Dim sheetNo As Integer = 1      'excelCreator.SheetNo2(WNG010CO.EXCEL_SHEET_NAME)

        ' '' 該当のシートをアクティブ化
        ''excelCreator.ActiveSheet = sheetNo

        ' '' データが設定されたセルの最大行と最大列の交点座標を取得します。
        ' ''Dim maxData As System.Drawing.Point = excelCreator.GetMaxData(AdvanceSoftware.ExcelCreator.MaxEndPoint.MaxPoint)
        ''Dim maxData = New System.Drawing.Point(53, 44)

        ' ''読み込むExcelのカラムを設定

        ' ''最大行を取得(rowNoKey列の最終入力行を取得)
        '' ''Dim rowNoMax As Integer = xlApp.ActiveCell.Row  '行の最大数
        '' ''Dim rowNoMin As Integer = 2                     '行の最小数
        '' ''Dim colNoMax As Integer = 100                  '列の最大数　実際は45だけど、メモなど列追加された場合のため100とする
        '' ''Dim rowNoKey As Integer = 3                    '最大行取得用RowNo  
        '' ''Dim dr As DataRow
        '' ''Dim errflg As Integer = 0
        '' ''Dim Y As Integer = 0
        '' ''Dim iAmt As Integer = 0

        ''Dim INVO_NO As String = String.Empty

        ' ''DS:JFB820DSEXCELクリア
        ' ''ds.Tables(JFB820C.TABLE_NM_EXCEL).Clear()

        ''xlSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Select()

        ' ''2次元配列を取得
        ''Dim arrData(,) As Object
        ''arrData = DirectCast(xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(xlApp.ActiveCell.Row, colNoMax)).Value, Object(,))


        ' ''2次元→DSにセットする
        ''For j As Integer = rowNoMin To xlApp.ActiveCell.Row     'rowNoMax

        ''    'arrData = DirectCast(xlSheet.Range(xlSheet.Cells(j, 1), xlSheet.Cells(xlApp.ActiveCell.Row, colNoMax)).Value, Object(,))

        ''    'Key列（デリバリーNo.）が空の場合は空行とみなしデータセットに登録しない
        ''    If arrData(j, 2) Is Nothing Then
        ''        Continue For
        ''    Else
        ''        If String.IsNullOrEmpty(arrData(j, 2).ToString) Then
        ''            Continue For
        ''        End If
        ''    End If

        ''    'Delivery Shipping Typeチェック(対象外データ判定)
        ''    If arrData(j, 43) Is Nothing Then
        ''        Continue For
        ''        ''Else
        ''        ''    If String.IsNullOrEmpty(arrData(j, 43).ToString) = True _
        ''        ''        Or arrData(j, 43).ToString.Trim.Equals(skipDataAir) Then

        ''        ''        Continue For
        ''        ''    End If
        ''    End If

        ''    'デリバリーNo. New-OLD チェック
        ''    If oleDELIVERY_NO <> arrData(j, 2).ToString.Trim Then
        ''        '採番
        ''        recNo = recNo + 1
        ''        gyo = 0

        ''        Me.SetHeaderRow(dtIn, dtRcvhed, arrData, dataKbn, recNo, filePath, workFolderPath, airFlag, j)

        ''        oleDELIVERY_NO = arrData(j, 2).ToString.Trim

        ''    Else
        ''        gyo = gyo + 1

        ''        '中間ファイル情報のセット(UTI受信DTL)
        ''        Me.SetDetailRow(dtIn, dtRcvDtl, arrData, recNo, filePath, gyo, workFolderPath, j)                    '明細チェック

        ''    End If

        ''    Debug.Print(arrData(j, 2).ToString)
        ''Next

        ''Return ds

    End Function

#End If

    ''' <summary>
    ''' 取込ファイル用データテーブルの作成
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Private Function CreateExcelDataTable() As DataTable
        Dim dt As New DataTable

        'データテーブルの列設定
        dt.Columns.Add("INV_STAGE", Type.GetType("System.String"))
        dt.Columns.Add("CONT_NO", Type.GetType("System.String"))
        dt.Columns.Add("INV_DATE", Type.GetType("System.String"))
        dt.Columns.Add("DEST_DEPOT", Type.GetType("System.String"))

        Return dt
    End Function


#End Region

#End Region

#Region "設定"

#If False Then      'UPD 2018/01/22 テキストからエクセル変更


    ''' <summary>
    ''' ヘッダーレコード値セット
    ''' </summary>
    ''' <param name="dtRcvhed"></param>
    ''' <param name="line"></param>
    ''' <remarks></remarks>
    Private Sub SetHeaderRow(ByVal dtIn As DataTable, ByVal dtRcvhed As DataTable, ByVal line As String, _
                             ByVal dataKbn As String, ByVal recNo As Integer, ByVal filePath As String, ByVal workFolderPath As String, ByRef airFlag As Boolean)

        Dim setDr As DataRow

        Dim Max As Integer = dtRcvhed.Rows.Count - 1

        If dataKbn.Equals(LMH820BLF.DATA_KBN_H01) = True Then
            setDr = dtRcvhed.NewRow()

            setDr("DEL_KB") = "0"
            'setDr("FILE_NAME") = String.Concat(MyBase.GetSystemDate(), "_", MyBase.GetSystemTime(), "_", filePath)
            setDr("FILE_NAME") = String.Concat(MyBase.GetSystemDate(), "_", MyBase.GetSystemTime(), "_", Replace(filePath, workFolderPath, String.Empty))
            setDr("REC_NO") = recNo.ToString().PadLeft(5, CChar("0"))
            setDr("NRS_BR_CD") = dtIn.Rows(0).Item("NRS_BR_CD")
            setDr("EDI_CTL_NO") = String.Empty
            setDr("INKA_CTL_NO_L") = String.Empty
            setDr("CUST_CD_L") = dtIn.Rows(0).Item("CUST_CD_L")
            setDr("CUST_CD_M") = dtIn.Rows(0).Item("CUST_CD_M")
            setDr("PRTFLG") = "0"
            setDr("CANCEL_FLG") = "0"

            setDr("H1_CODE") = SubStringOfByte(line, 3, 10)
            setDr("H1_NAME1") = SubStringOfByte(line, 13, 35)
            setDr("H1_NAME2") = SubStringOfByte(line, 48, 35)
            setDr("H1_NAME3") = SubStringOfByte(line, 83, 35)
            setDr("H1_ADRESS1") = SubStringOfByte(line, 118, 35)
            setDr("H1_ADRESS2") = SubStringOfByte(line, 153, 35)
            setDr("H1_POSTAL_CODE") = SubStringOfByte(line, 188, 10)
            setDr("H1_COUNTRY_CODE") = SubStringOfByte(line, 198, 3)
            setDr("H1_REGION_CODE") = SubStringOfByte(line, 201, 3)
            setDr("H1_TEL_NO") = SubStringOfByte(line, 204, 20)
            setDr("H1_FAX_NO") = SubStringOfByte(line, 224, 20)

            setDr("MISOUCYAKU_SHORI_FLG") = "0"
            setDr("MISOUCYAKU_USER") = String.Empty
            setDr("MISOUCYAKU_DATE") = String.Empty
            setDr("MISOUCYAKU_TIME") = String.Empty
            setDr("DELETE_USER") = String.Empty
            setDr("DELETE_DATE") = String.Empty
            setDr("DELETE_TIME") = String.Empty
            setDr("DELETE_EDI_NO") = String.Empty
            setDr("PRT_USER") = String.Empty
            setDr("PRT_DATE") = String.Empty
            setDr("PRT_TIME") = String.Empty
            setDr("EDI_USER") = String.Empty
            setDr("EDI_DATE") = String.Empty
            setDr("EDI_TIME") = String.Empty
            setDr("INKA_USER") = String.Empty
            setDr("INKA_DATE") = String.Empty
            setDr("INKA_TIME") = String.Empty
            setDr("UPD_USER") = String.Empty
            setDr("UPD_DATE") = String.Empty
            setDr("UPD_TIME") = String.Empty

            dtRcvhed.Rows.Add(setDr)

        ElseIf dataKbn-.Equals(LMH820BLF.DATA_KBN_H02) = True Then

            setDr = dtRcvhed.Rows(Max)

            setDr("H2_CODE") = SubStringOfByte(line, 3, 10)
            setDr("H2_NAME1") = SubStringOfByte(line, 13, 35)
            setDr("H2_NAME2") = SubStringOfByte(line, 48, 35)
            setDr("H2_NAME3") = SubStringOfByte(line, 83, 35)
            setDr("H2_ADRESS1") = SubStringOfByte(line, 118, 35)
            setDr("H2_ADRESS2") = SubStringOfByte(line, 153, 35)
            setDr("H2_POSTAL_CODE") = SubStringOfByte(line, 188, 10)
            setDr("H2_COUNTRY_CODE") = SubStringOfByte(line, 198, 3)
            setDr("H2_REGION_CODE") = SubStringOfByte(line, 201, 3)
            setDr("H2_TEL_NO") = SubStringOfByte(line, 204, 20)
            setDr("H2_FAX_NO") = SubStringOfByte(line, 224, 20)

        ElseIf dataKbn.Equals(LMH820BLF.DATA_KBN_H03) = True Then

            setDr = dtRcvhed.Rows(Max)

            setDr("H3_CODE") = SubStringOfByte(line, 3, 10)
            setDr("H3_NAME1") = SubStringOfByte(line, 13, 35)
            setDr("H3_NAME2") = SubStringOfByte(line, 48, 35)
            setDr("H3_NAME3") = SubStringOfByte(line, 83, 35)
            setDr("H3_ADRESS1") = SubStringOfByte(line, 118, 35)
            setDr("H3_ADRESS2") = SubStringOfByte(line, 153, 35)
            setDr("H3_POSTAL_CODE") = SubStringOfByte(line, 188, 10)
            setDr("H3_COUNTRY_CODE") = SubStringOfByte(line, 198, 3)
            setDr("H3_REGION_CODE") = SubStringOfByte(line, 201, 3)
            setDr("H3_TEL_NO") = SubStringOfByte(line, 204, 20)
            setDr("H3_FAX_NO") = SubStringOfByte(line, 224, 20)

        ElseIf dataKbn.Equals(LMH820BLF.DATA_KBN_H04) = True Then

            setDr = dtRcvhed.Rows(Max)

            setDr("H4_INVO_NO") = SubStringOfByte(line, 3, 10)
            setDr("H4_DATE") = SubStringOfByte(line, 13, 8)
            setDr("H4_MODE_OF_TRANSPORT") = SubStringOfByte(line, 21, 5)

            If (setDr("H4_MODE_OF_TRANSPORT").ToString).Equals(LMH820BLF.MODE_OF_TRANS) = True Then
                setDr("INKA_TAG_FLG") = "1"
            Else
                setDr("INKA_TAG_FLG") = "0"
            End If

            setDr("H4_DELIVERY_INCOTERM1") = SubStringOfByte(line, 26, 3)
            setDr("H4_DELIVERY_INCOTERM2") = SubStringOfByte(line, 29, 28)
            setDr("H4_DELIVERY_NO") = SubStringOfByte(line, 57, 10)
            setDr("H4_ROUTE") = SubStringOfByte(line, 67, 30)
            setDr("H4_PAYMENT_TERMS") = SubStringOfByte(line, 97, 30)

            'SHINODA 要望管理2235対応 Start 
            If (setDr("H4_MODE_OF_TRANSPORT").ToString).Equals(LMH820BLF.MODE_OF_TRANS) = True Then
                dtRcvhed.Rows(Max).Delete()
                airFlag = True
            End If
            'SHINODA 要望管理2235対応 E n d 

            Me._preDataKbn = LMH820BLF.DATA_KBN_H04

        ElseIf dataKbn.Equals(LMH820BLF.DATA_KBN_S01) = True Then

            setDr = dtRcvhed.Rows(Max)

            setDr("S1_TOTAL_AMOUNT") = SubStringOfByte(line, 3, 15)
            setDr("S1_CURRENCY") = SubStringOfByte(line, 18, 3)
            setDr("S1_SHIPPING_WEIGHT_NET") = SubStringOfByte(line, 21, 12)
            setDr("S1_GROSS") = SubStringOfByte(line, 33, 12)
            setDr("S1_UOM") = SubStringOfByte(line, 45, 3)

        End If

    End Sub
#Else

    ''' <summary>
    ''' ヘッダーレコード値セット
    ''' </summary>
    ''' <param name="dtRcvhed"></param>
    ''' <param name="arrData"></param>
    ''' <remarks></remarks>
    Private Sub SetHeaderRow(ByVal dtIn As DataTable, ByVal dtRcvhed As DataTable, ByVal arrData As DataRow, _
                             ByVal recNo As Integer, ByVal filePath As String, ByVal workFolderPath As String)

        Dim setDr As DataRow

        Dim Max As Integer = dtRcvhed.Rows.Count - 1

        setDr = dtRcvhed.NewRow()

        setDr("DEL_KB") = "0"
        setDr("FILE_NAME") = String.Concat(MyBase.GetSystemDate(), "_", MyBase.GetSystemTime(), "_", Replace(filePath, workFolderPath, String.Empty))
        setDr("REC_NO") = recNo.ToString().PadLeft(5, CChar("0"))
        setDr("NRS_BR_CD") = dtIn.Rows(0).Item("NRS_BR_CD")
        setDr("EDI_CTL_NO") = String.Empty
        setDr("INKA_CTL_NO_L") = String.Empty
        setDr("CUST_CD_L") = dtIn.Rows(0).Item("CUST_CD_L")
        setDr("CUST_CD_M") = dtIn.Rows(0).Item("CUST_CD_M")
        setDr("PRTFLG") = "0"
        setDr("CANCEL_FLG") = "0"

        '判定項目なし(AIrは対象外
        'UPD 2018/02/21 先頭2文字(CD)で判定する
        'If arrData(42).ToString.Trim.Equals(skipDataAir) Then
        If Mid(arrData(42).ToString.Trim, 1, 2).Equals(skipDataAirCD) Then
            setDr("INKA_TAG_FLG") = "1"

            'ADD 2018/03/01 Airは登録しないし用にする　依頼番号：001247 
            Exit Sub

        Else
            setDr("INKA_TAG_FLG") = "0"
        End If

        setDr("H1_CODE") = String.Empty
        setDr("H1_NAME1") = String.Empty
        setDr("H1_NAME2") = String.Empty
        setDr("H1_NAME3") = String.Empty
        setDr("H1_ADRESS1") = String.Empty
        setDr("H1_ADRESS2") = String.Empty
        setDr("H1_POSTAL_CODE") = String.Empty
        setDr("H1_COUNTRY_CODE") = String.Empty
        setDr("H1_REGION_CODE") = String.Empty
        setDr("H1_TEL_NO") = String.Empty
        setDr("H1_FAX_NO") = String.Empty

        setDr("MISOUCYAKU_SHORI_FLG") = "0"
        setDr("MISOUCYAKU_USER") = String.Empty
        setDr("MISOUCYAKU_DATE") = String.Empty
        setDr("MISOUCYAKU_TIME") = String.Empty
        setDr("DELETE_USER") = String.Empty
        setDr("DELETE_DATE") = String.Empty
        setDr("DELETE_TIME") = String.Empty
        setDr("DELETE_EDI_NO") = String.Empty
        setDr("PRT_USER") = String.Empty
        setDr("PRT_DATE") = String.Empty
        setDr("PRT_TIME") = String.Empty
        setDr("EDI_USER") = String.Empty
        setDr("EDI_DATE") = String.Empty
        setDr("EDI_TIME") = String.Empty
        setDr("INKA_USER") = String.Empty
        setDr("INKA_DATE") = String.Empty
        setDr("INKA_TIME") = String.Empty
        setDr("UPD_USER") = String.Empty
        setDr("UPD_DATE") = String.Empty
        setDr("UPD_TIME") = String.Empty

        setDr("H2_CODE") = String.Empty
        setDr("H2_NAME1") = String.Empty
        setDr("H2_NAME2") = String.Empty
        setDr("H2_NAME3") = String.Empty
        setDr("H2_ADRESS1") = String.Empty
        setDr("H2_ADRESS2") = String.Empty
        setDr("H2_POSTAL_CODE") = String.Empty
        setDr("H2_COUNTRY_CODE") = String.Empty
        setDr("H2_REGION_CODE") = String.Empty
        setDr("H2_TEL_NO") = String.Empty
        setDr("H2_FAX_NO") = String.Empty

        setDr("H3_CODE") = String.Empty
        If arrData(8) Is Nothing = False Then
            setDr("H3_CODE") = arrData(8).ToString
        End If

        setDr("H3_NAME1") = arrData(9)            '
        setDr("H3_NAME2") = String.Empty
        setDr("H3_NAME3") = String.Empty
        setDr("H3_ADRESS1") = String.Empty
        setDr("H3_ADRESS2") = String.Empty
        setDr("H3_POSTAL_CODE") = arrData(10)       '
        setDr("H3_COUNTRY_CODE") = arrData(12).ToString      '
        setDr("H3_REGION_CODE") = String.Empty
        setDr("H3_TEL_NO") = String.Empty
        setDr("H3_FAX_NO") = String.Empty

        setDr("H4_INVO_NO") = String.Empty
        'setDr("H4_DATE") = DateFormatUtility.DeleteSlash(arrData(7).ToString)
        'setDr("H4_DATE") = Format(Convert.ToDateTime(arrData(7)), "yyyyMMdd")

        setDr("H4_MODE_OF_TRANSPORT") = String.Empty

        setDr("H4_DELIVERY_INCOTERM1") = String.Empty
        If arrData(15) Is Nothing = False Then
            setDr("H4_DELIVERY_INCOTERM1") = arrData(15).ToString
        End If

        setDr("H4_DELIVERY_INCOTERM2") = String.Empty
        If arrData(16) Is Nothing = False Then
            setDr("H4_DELIVERY_INCOTERM2") = arrData(16).ToString
        End If
#If False Then      'UPD 2018/02/21 9桁の場合は前に0を付ける。
        setDr("H4_DELIVERY_NO") = arrData(1).ToString       
#Else
        If Len(arrData(1).ToString) = 9 Then
            setDr("H4_DELIVERY_NO") = String.Concat("0", arrData(1).ToString)
        Else

            setDr("H4_DELIVERY_NO") = arrData(1).ToString
        End If

#End If
        setDr("H4_ROUTE") = String.Empty
        setDr("H4_PAYMENT_TERMS") = String.Empty

        'SHINODA 要望管理2235対応 Start 
        'If (setDr("H4_MODE_OF_TRANSPORT").ToString).Equals(LMH820BLF.MODE_OF_TRANS) = True Then
        '    dtRcvhed.Rows(Max).Delete()
        '    airFlag = True
        'End If
        'SHINODA 要望管理2235対応 E n d 

        setDr("S1_TOTAL_AMOUNT") = "0".ToString
        setDr("S1_CURRENCY") = String.Empty

        setDr("S1_SHIPPING_WEIGHT_NET") = "0".ToString
        If arrData(27) Is Nothing = False Then
            setDr("S1_SHIPPING_WEIGHT_NET") = arrData(27).ToString       '
        End If

        setDr("S1_GROSS") = arrData(28).ToString
        setDr("S1_UOM") = String.Empty


        '--テーブル追加 Start
        setDr("SHIPMENT_NO") = String.Empty
        If arrData(0) Is Nothing = False Then
            setDr("SHIPMENT_NO") = arrData(0).ToString
        End If

        setDr("SAP_ODER_NO") = String.Empty
        If arrData(2) Is Nothing = False Then
            setDr("SAP_ODER_NO") = arrData(2).ToString
        End If

        setDr("CARRIER_NM") = String.Empty
        If arrData(5) Is Nothing = False Then
            setDr("CARRIER_NM") = arrData(5).ToString
        End If


        setDr("CONT_TRAILER_ID") = String.Empty
        If arrData(6) Is Nothing = False Then
            setDr("CONT_TRAILER_ID") = arrData(6).ToString
        End If

        setDr("SHIP_TO_PARTY_CITY") = String.Empty
        If arrData(11) Is Nothing = False Then
            setDr("SHIP_TO_PARTY_CITY") = arrData(11).ToString
        End If

        'setDr("SHIP_COMPLETION_DATE") = Format(Convert.ToDateTime(arrData(j, 24)), "yyyyMMdd")

        'setDr("PLAN_DELIV_DATE_SHIP") = Format(Convert.ToDateTime(arrData(j, 25)), "yyyyMMdd")

        'setDr("CUSTOMER_REQ_DELIV_DATE_ORDER") = Format(Convert.ToDateTime(arrData(j, 26)), "yyyyMMdd")

        setDr("VOYAGE_NO") = String.Empty
        If arrData(32) Is Nothing = False Then
            setDr("VOYAGE_NO") = arrData(32).ToString
        End If

        setDr("VESSEL_AIRCRAFT_NM") = String.Empty
        If arrData(33) Is Nothing = False Then
            setDr("VESSEL_AIRCRAFT_NM") = arrData(33).ToString
        End If

        setDr("SHIP_FROM_COUNTRY") = String.Empty
        If arrData(34) Is Nothing = False Then
            setDr("SHIP_FROM_COUNTRY") = arrData(34).ToString
        End If

        setDr("ACT_GOODS_ISSUE_TIME") = String.Empty
        'Time取得が出来ないため、とりあえず未設定
        If arrData(35) Is Nothing = False Then
            setDr("ACT_GOODS_ISSUE_TIME") = arrData(35).ToString
        End If

        setDr("SHIPTO_CITY") = String.Empty
        If arrData(36) Is Nothing = False Then
            setDr("SHIPTO_CITY") = arrData(36).ToString
        End If

        setDr("DEST_COUNTRY") = String.Empty
        If arrData(37) Is Nothing = False Then
            setDr("DEST_COUNTRY") = arrData(37).ToString
        End If

        setDr("SHIP_POINT_NM") = String.Empty
        If arrData(39) Is Nothing = False Then
            setDr("SHIP_POINT_NM") = arrData(39).ToString
        End If

        setDr("PLANT_NM") = String.Empty
        If arrData(41) Is Nothing = False Then
            setDr("PLANT_NM") = arrData(41).ToString
        End If

        setDr("DELIVERY_SHIPPNG_TYPE") = String.Empty
        If arrData(42) Is Nothing = False Then
            setDr("DELIVERY_SHIPPNG_TYPE") = arrData(42).ToString
        End If

        setDr("TRANSPORTNG_MODE") = String.Empty
        If arrData(43) Is Nothing = False Then
            setDr("TRANSPORTNG_MODE") = arrData(43).ToString
        End If

        '--テーブル追加 End

        dtRcvhed.Rows.Add(setDr)

    End Sub

#End If

#If False Then  'UPD 2018/01/22 テキストからエクセル変更

    ''' <summary>
    ''' 明細レコード値セット
    ''' </summary>
    ''' <param name="dtRcvDtl"></param>
    ''' <param name="line"></param>
    ''' <remarks></remarks>
    Private Sub SetDetailRow(ByVal dtIn As DataTable, ByVal dtRcvDtl As DataTable, ByVal line As String, _
                             ByVal dataKbn As String, ByVal recNo As Integer, ByVal filePath As String, _
                             ByVal gyo As Integer, ByVal L1Flg As Boolean, ByVal workFolderPath As String)

        Dim setDr As DataRow

        Dim Max As Integer = dtRcvDtl.Rows.Count - 1

        If dataKbn.Equals(LMH820BLF.DATA_KBN_L01) = True OrElse L1Flg = True Then

            setDr = dtRcvDtl.NewRow()

            setDr("DEL_KB") = "0"
            'setDr("FILE_NAME") = String.Concat(MyBase.GetSystemDate(), "_", MyBase.GetSystemTime(), "_", filePath)
            setDr("FILE_NAME") = String.Concat(MyBase.GetSystemDate(), "_", MyBase.GetSystemTime(), "_", Replace(filePath, workFolderPath, String.Empty))
            setDr("REC_NO") = recNo.ToString().PadLeft(5, CChar("0"))
            setDr("GYO") = gyo.ToString().PadLeft(3, CChar("0"))
            setDr("NRS_BR_CD") = dtIn.Rows(0).Item("NRS_BR_CD")
            setDr("EDI_CTL_NO") = String.Empty
            setDr("EDI_CTL_NO_CHU") = String.Empty
            setDr("INKA_CTL_NO_L") = String.Empty
            setDr("INKA_CTL_NO_M") = String.Empty
            setDr("CUST_CD_L") = dtIn.Rows(0).Item("CUST_CD_L")
            setDr("CUST_CD_M") = dtIn.Rows(0).Item("CUST_CD_M")

            setDr("RECORD_STATUS") = String.Empty
            setDr("JISSEKI_SHORI_FLG") = "1"
            setDr("JISSEKI_USER") = String.Empty
            setDr("JISSEKI_DATE") = String.Empty
            setDr("JISSEKI_TIME") = String.Empty
            setDr("SEND_USER") = String.Empty
            setDr("SEND_DATE") = String.Empty
            setDr("SEND_TIME") = String.Empty
            setDr("DELETE_USER") = String.Empty
            setDr("DELETE_DATE") = String.Empty
            setDr("DELETE_TIME") = String.Empty
            setDr("DELETE_EDI_NO") = String.Empty
            setDr("DELETE_EDI_NO_CHU") = String.Empty
            setDr("UPD_USER") = String.Empty
            setDr("UPD_DATE") = String.Empty
            setDr("UPD_TIME") = String.Empty

            Me._preDataKbn = LMH820BLF.DATA_KBN_L01

            If dataKbn.Equals(LMH820BLF.DATA_KBN_L01) = True Then

                setDr("L1_SALES_ORDER") = SubStringOfByte(line, 3, 10)
                setDr("L1_PO") = SubStringOfByte(line, 13, 20)

                dtRcvDtl.Rows.Add(setDr)

            ElseIf L1Flg = True Then

                setDr("L1_SALES_ORDER") = dtRcvDtl.Rows(Max).Item("L1_SALES_ORDER")
                setDr("L1_PO") = dtRcvDtl.Rows(Max).Item("L1_PO")

                dtRcvDtl.Rows.Add(setDr)

                setDr = dtRcvDtl.Rows(Max + 1)

                setDr("L2_ITEM_CODE") = SubStringOfByte(line, 3, 10)
                setDr("L2_NAME_INTERNAL") = SubStringOfByte(line, 13, 35)
                setDr("L2_COMMODITY_CODE") = SubStringOfByte(line, 48, 10)
                setDr("L2_BATCH_NO") = SubStringOfByte(line, 58, 10)
                setDr("L2_ORIGIN") = SubStringOfByte(line, 68, 3)
                setDr("L2_QUANTITY") = SubStringOfByte(line, 71, 12)
                setDr("L2_QUANTITY_UOM") = SubStringOfByte(line, 83, 3)
                setDr("L2_WEIGHT_NET") = SubStringOfByte(line, 86, 12)
                setDr("L2_GROSS") = SubStringOfByte(line, 98, 12)
                setDr("L2_UOM") = SubStringOfByte(line, 110, 3)
                setDr("L2_PRICE") = SubStringOfByte(line, 113, 12)
                setDr("L2_CURRENCY") = SubStringOfByte(line, 125, 3)
                setDr("L2_AMOUNT") = SubStringOfByte(line, 128, 15)
                setDr("L2_TEXT") = SubStringOfByte(line, 143, 80)
                setDr("L2_PLANT_CODE") = SubStringOfByte(line, 223, 4)
                setDr("L2_PRICE_UNIT") = SubStringOfByte(line, 227, 3)
                setDr("L2_PRICE_QUANTITY") = SubStringOfByte(line, 230, 12)
                setDr("L2_SHIPPING_POINT_CODE") = SubStringOfByte(line, 242, 4)
                If (setDr("L2_SHIPPING_POINT_CODE").ToString()).Equals(LMH820BLF.SHIP_POINT_CPDE) = True Then
                    setDr("INKA_TAG_FLG") = "1"
                Else
                    setDr("INKA_TAG_FLG") = "0"
                End If
                setDr("L2_TEMP_CONDITION_TEXT") = SubStringOfByte(line, 246, 80)
                setDr("L2_STORAGE_CONDITION_CODE") = SubStringOfByte(line, 326, 2)
                setDr("L2_REFRIGERATED_CODE") = SubStringOfByte(line, 328, 1)
                setDr("L2_EXPORT_RESTRICT") = SubStringOfByte(line, 329, 4)
                setDr("L2_EXPORT_RESTRICT_NEW") = SubStringOfByte(line, 333, 14)

                Me._prePreDataKbn = Me._preDataKbn
                Me._preDataKbn = LMH820BLF.DATA_KBN_L02


            End If

        ElseIf dataKbn.Equals(LMH820BLF.DATA_KBN_L02) = True Then

            setDr = dtRcvDtl.Rows(Max)

            setDr("L2_ITEM_CODE") = SubStringOfByte(line, 3, 10)
            setDr("L2_NAME_INTERNAL") = SubStringOfByte(line, 13, 35)
            setDr("L2_COMMODITY_CODE") = SubStringOfByte(line, 48, 10)
            setDr("L2_BATCH_NO") = SubStringOfByte(line, 58, 10)
            setDr("L2_ORIGIN") = SubStringOfByte(line, 68, 3)
            setDr("L2_QUANTITY") = SubStringOfByte(line, 71, 12)
            setDr("L2_QUANTITY_UOM") = SubStringOfByte(line, 83, 3)
            setDr("L2_WEIGHT_NET") = SubStringOfByte(line, 86, 12)
            setDr("L2_GROSS") = SubStringOfByte(line, 98, 12)
            setDr("L2_UOM") = SubStringOfByte(line, 110, 3)
            setDr("L2_PRICE") = SubStringOfByte(line, 113, 12)
            setDr("L2_CURRENCY") = SubStringOfByte(line, 125, 3)
            setDr("L2_AMOUNT") = SubStringOfByte(line, 128, 15)
            setDr("L2_TEXT") = SubStringOfByte(line, 143, 80)
            setDr("L2_PLANT_CODE") = SubStringOfByte(line, 223, 4)
            setDr("L2_PRICE_UNIT") = SubStringOfByte(line, 227, 3)
            setDr("L2_PRICE_QUANTITY") = SubStringOfByte(line, 230, 12)
            setDr("L2_SHIPPING_POINT_CODE") = SubStringOfByte(line, 242, 4)
            If (setDr("L2_SHIPPING_POINT_CODE").ToString()).Equals(LMH820BLF.SHIP_POINT_CPDE) = True Then
                setDr("INKA_TAG_FLG") = "1"
            Else
                setDr("INKA_TAG_FLG") = "0"
            End If
            setDr("L2_TEMP_CONDITION_TEXT") = SubStringOfByte(line, 246, 80)
            setDr("L2_STORAGE_CONDITION_CODE") = SubStringOfByte(line, 326, 2)
            setDr("L2_REFRIGERATED_CODE") = SubStringOfByte(line, 328, 1)
            setDr("L2_EXPORT_RESTRICT") = SubStringOfByte(line, 329, 4)
            setDr("L2_EXPORT_RESTRICT_NEW") = SubStringOfByte(line, 333, 14)

            Me._prePreDataKbn = Me._preDataKbn
            Me._preDataKbn = LMH820BLF.DATA_KBN_L02

        ElseIf dataKbn.Equals(LMH820BLF.DATA_KBN_L03) = True Then

            setDr = dtRcvDtl.Rows(Max)

            setDr("L3_PACKING_GROUP") = SubStringOfByte(line, 3, 3)
            setDr("L3_CLASS_SUBRISK") = SubStringOfByte(line, 6, 15)
            setDr("L3_UN_NUMBER") = SubStringOfByte(line, 21, 4)
            setDr("L3_LIMITED_QUANTITY") = SubStringOfByte(line, 25, 18)
            setDr("L3_PROPER_SHIP_NAME") = SubStringOfByte(line, 43, 80)
            setDr("L3_TECHNICAL_NAME") = SubStringOfByte(line, 123, 162)
            setDr("L3_IATA_PACKING_INSTRUCTION") = SubStringOfByte(line, 285, 18)
            setDr("L3_IATA_CARGO_ONLY") = SubStringOfByte(line, 303, 9)
            setDr("L3_IMO_EMERGENCY_RESPONSE") = SubStringOfByte(line, 312, 20)
            setDr("L3_TDD_CLASS_LIMITED_QUANTITY") = SubStringOfByte(line, 332, 15)
            setDr("L3_EMERGENCY_TELEPHONE_NUMBERS") = SubStringOfByte(line, 347, 18)
            setDr("L3_FLASH_POINT_FOR_IMO") = SubStringOfByte(line, 365, 18)
            setDr("L3_MARINE_POLLUTANT_HIS_NAME") = SubStringOfByte(line, 383, 160)
            setDr("L3_HAZARDOUS_INFORMATION_STATUS") = SubStringOfByte(line, 543, 6)

            Me._prePreDataKbn = Me._preDataKbn
            Me._preDataKbn = LMH820BLF.DATA_KBN_L03

        End If

    End Sub

#Else

    ''' <summary>
    ''' 明細レコード値セット
    ''' </summary>
    ''' <param name="dtRcvDtl"></param>
    ''' <param name="arrData"></param>
    ''' <remarks></remarks>
    Private Sub SetDetailRow(ByVal dtIn As DataTable, ByVal dtRcvDtl As DataTable, ByVal arrData As DataRow, _
                              ByVal recNo As Integer, ByVal filePath As String, _
                             ByVal gyo As Integer, ByVal workFolderPath As String)

        Dim setDr As DataRow

        Dim Max As Integer = dtRcvDtl.Rows.Count - 1

        setDr = dtRcvDtl.NewRow()

        setDr("DEL_KB") = "0"
        'setDr("FILE_NAME") = String.Concat(MyBase.GetSystemDate(), "_", MyBase.GetSystemTime(), "_", filePath)
        setDr("FILE_NAME") = String.Concat(MyBase.GetSystemDate(), "_", MyBase.GetSystemTime(), "_", Replace(filePath, workFolderPath, String.Empty))
        setDr("REC_NO") = recNo.ToString().PadLeft(5, CChar("0"))
        setDr("GYO") = gyo.ToString().PadLeft(3, CChar("0"))
        setDr("NRS_BR_CD") = dtIn.Rows(0).Item("NRS_BR_CD")
        setDr("EDI_CTL_NO") = String.Empty
        setDr("EDI_CTL_NO_CHU") = String.Empty
        setDr("INKA_CTL_NO_L") = String.Empty
        setDr("INKA_CTL_NO_M") = String.Empty
        setDr("CUST_CD_L") = dtIn.Rows(0).Item("CUST_CD_L")
        setDr("CUST_CD_M") = dtIn.Rows(0).Item("CUST_CD_M")

        setDr("RECORD_STATUS") = String.Empty
        setDr("JISSEKI_SHORI_FLG") = "1"
        setDr("JISSEKI_USER") = String.Empty
        setDr("JISSEKI_DATE") = String.Empty
        setDr("JISSEKI_TIME") = String.Empty
        setDr("SEND_USER") = String.Empty
        setDr("SEND_DATE") = String.Empty
        setDr("SEND_TIME") = String.Empty
        setDr("DELETE_USER") = String.Empty
        setDr("DELETE_DATE") = String.Empty
        setDr("DELETE_TIME") = String.Empty
        setDr("DELETE_EDI_NO") = String.Empty
        setDr("DELETE_EDI_NO_CHU") = String.Empty
        setDr("UPD_USER") = String.Empty
        setDr("UPD_DATE") = String.Empty
        setDr("UPD_TIME") = String.Empty

        'Airは対象外
        'UPD 2018/02/21 先頭2文字(CD)で判定する
        'If arrData(42).ToString.Trim.Equals(skipDataAir) Then
        If Mid(arrData(42).ToString.Trim, 1, 2).Equals(skipDataAirCD) Then

            setDr("INKA_TAG_FLG") = "1"

            'ADD 2018/03/01 Airは登録しないし用にする　依頼番号：001247 
            Exit Sub

        Else
            setDr("INKA_TAG_FLG") = "0"
        End If

        setDr("L1_SALES_ORDER") = arrData(19).ToString           '
        setDr("L1_PO") = String.Empty

#If False Then      'UPD 2018/08/30
                '後ろ７桁設定
        setDr("L2_ITEM_CODE") = Right(arrData(3).ToString.Trim, 7)

#Else
        If Left(arrData(3).ToString.Trim, 4) = "0000" Then
            '後ろ７桁設定
            setDr("L2_ITEM_CODE") = Right(arrData(3).ToString.Trim, 7)
        ElseIf Left(arrData(3).ToString.Trim, 3) = "000" Then
            '後ろ8桁設定
            setDr("L2_ITEM_CODE") = Right(arrData(3).ToString.Trim, 8)
        Else
            '後ろ７桁設定
            setDr("L2_ITEM_CODE") = Right(arrData(3).ToString.Trim, 7)
        End If
#End If

            setDr("L2_NAME_INTERNAL") = arrData(4).ToString
            setDr("L2_COMMODITY_CODE") = arrData(18).ToString
            setDr("L2_BATCH_NO") = arrData(13).ToString
            setDr("L2_ORIGIN") = arrData(17).ToString
            setDr("L2_QUANTITY") = arrData(14).ToString

            setDr("L2_QUANTITY_UOM") = String.Empty

            setDr("L2_WEIGHT_NET") = "0".ToString
            If arrData(27) Is Nothing = False Then
                setDr("L2_WEIGHT_NET") = arrData(27).ToString       '
            End If

            setDr("L2_GROSS") = arrData(28).ToString

            setDr("L2_UOM") = String.Empty
            setDr("L2_PRICE") = "0".ToString

            setDr("L2_CURRENCY") = String.Empty         'NACCSよりか？
            setDr("L2_AMOUNT") = "0".ToString           'NACCSよりか？
            setDr("L2_TEXT") = String.Empty
            setDr("L2_PLANT_CODE") = arrData(40).ToString

            setDr("L2_PRICE_UNIT") = String.Empty
            setDr("L2_PRICE_QUANTITY") = "0".ToString
            setDr("L2_SHIPPING_POINT_CODE") = arrData(38).ToString
            setDr("L2_TEMP_CONDITION_TEXT") = SubStringOfByte(String.Concat(arrData(26).ToString, " ", arrData(29).ToString), 1, 80)
            setDr("L2_STORAGE_CONDITION_CODE") = String.Empty
            setDr("L2_REFRIGERATED_CODE") = String.Empty
            setDr("L2_EXPORT_RESTRICT") = String.Empty
            setDr("L2_EXPORT_RESTRICT_NEW") = String.Empty

            setDr("L3_PACKING_GROUP") = String.Empty
            If arrData(21) Is Nothing = False Then
                setDr("L3_PACKING_GROUP") = arrData(21).ToString
            End If

            setDr("L3_CLASS_SUBRISK") = String.Empty
            If arrData(20) Is Nothing = False Then
                setDr("L3_CLASS_SUBRISK") = arrData(20).ToString
            End If

            setDr("L3_UN_NUMBER") = String.Empty
            If arrData(22) Is Nothing = False Then
                setDr("L3_UN_NUMBER") = arrData(22).ToString
            End If

            setDr("L3_LIMITED_QUANTITY") = "0".ToString
            setDr("L3_PROPER_SHIP_NAME") = String.Empty
            setDr("L3_TECHNICAL_NAME") = String.Empty
            setDr("L3_IATA_PACKING_INSTRUCTION") = String.Empty
            setDr("L3_IATA_CARGO_ONLY") = String.Empty
            setDr("L3_IMO_EMERGENCY_RESPONSE") = String.Empty

            setDr("L3_TDD_CLASS_LIMITED_QUANTITY") = String.Empty
            setDr("L3_EMERGENCY_TELEPHONE_NUMBERS") = String.Empty
            setDr("L3_FLASH_POINT_FOR_IMO") = String.Empty
            setDr("L3_MARINE_POLLUTANT_HIS_NAME") = String.Empty
            setDr("L3_HAZARDOUS_INFORMATION_STATUS") = String.Empty

            '--テーブル追加 Start

            setDr("DELIVERY_QTY_IN_EA") = "0".ToString
            If arrData(30) Is Nothing = False Then
                setDr("DELIVERY_QTY_IN_EA") = arrData(30).ToString
            End If

            setDr("REFERENCE_DOC_NO") = String.Empty
            If arrData(31) Is Nothing = False Then
                setDr("REFERENCE_DOC_NO") = arrData(31).ToString
            End If

            '--テーブル追加 End

            dtRcvDtl.Rows.Add(setDr)

    End Sub

#End If

    ''' <summary>
    ''' エラーファイル用データテーブルの作成
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Private Function CreateErrorDataTable() As DataTable
        Dim dt As New DataTable

        'データテーブルの列設定
        dt.Columns.Add("INV_STAGE", Type.GetType("System.String"))
        dt.Columns.Add("CONT_NO", Type.GetType("System.String"))
        dt.Columns.Add("INV_DATE", Type.GetType("System.String"))
        dt.Columns.Add("DEST_DEPOT", Type.GetType("System.String"))
        dt.Columns.Add("ERR_NO", Type.GetType("System.String"))
        dt.Columns.Add("ERR_MSG", Type.GetType("System.String"))

        Return dt
    End Function

    ''' <summary>
    ''' Import用データテーブルの作成
    ''' </summary>
    ''' <returns>DataTable</returns>
    ''' <remarks></remarks>
    Private Function CreateImportDataTable() As DataTable
        Dim dt As New DataTable

        'データテーブルの列設定
        dt.Columns.Add("INV_EVENT_KBN", Type.GetType("System.String"))
        dt.Columns.Add("INV_EVENT_DATE", Type.GetType("System.String"))
        dt.Columns.Add("STAGE_KBN", Type.GetType("System.String"))
        dt.Columns.Add("JOB_NO", Type.GetType("System.String"))
        dt.Columns.Add("CONT_SEQ", Type.GetType("System.String"))
        dt.Columns.Add("SYS_UPD_DATE", Type.GetType("System.String"))
        dt.Columns.Add("SYS_UPD_TIME", Type.GetType("System.String"))
        dt.Columns.Add("FUTURE_CONT_SEQ", Type.GetType("System.String"))
        dt.Columns.Add("FUTURE_JOB_NO", Type.GetType("System.String"))
        dt.Columns.Add("FUTURE_UPD_DATE", Type.GetType("System.String"))
        dt.Columns.Add("FUTURE_UPD_TIME", Type.GetType("System.String"))
        dt.Columns.Add("HAITA_FLG", Type.GetType("System.String"))
        dt.Columns.Add("LAST_JOB_NO", Type.GetType("System.String"))
        dt.Columns.Add("LAST_CONT_SEQ", Type.GetType("System.String"))
        dt.Columns.Add("LAST_JOB_UPD_DATE", Type.GetType("System.String"))
        dt.Columns.Add("LAST_JOB_UPD_TIME", Type.GetType("System.String"))
        dt.Columns.Add("LAST_CONT_STAGE_KBN", Type.GetType("System.String"))
        dt.Columns.Add("TANK_EXIST", Type.GetType("System.String"))
        dt.Columns.Add("DEST_DEPOT_CD", Type.GetType("System.String"))
        dt.Columns.Add("LOCALDATE", Type.GetType("System.String"))
        dt.Columns.Add("CONT_NO", Type.GetType("System.String"))

        Return dt
    End Function


#End Region

#Region "Util"

    ''' <summary>
    ''' バイト単位で文字列を抜き取る
    ''' </summary>
    ''' <param name="para"></param>
    ''' <param name="startIndex"></param>
    ''' <param name="length"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SubStringOfByte(ByVal para As String, ByVal startIndex As Integer, Optional ByVal length As Integer = 0) As String

        Return SubStringOfByteNotTrim(para, startIndex, length).Trim

    End Function

    ''' <summary>
    ''' バイト単位で文字列を抜き取る
    ''' </summary>
    ''' <param name="para"></param>
    ''' <param name="startIndex"></param>
    ''' <param name="length"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function SubStringOfByteNotTrim(ByVal para As String, ByVal startIndex As Integer, Optional ByVal length As Integer = 0) As String

        Dim rtnStr As String = String.Empty

        '空文字に対しては常に空文字を返す
        If String.IsNullOrEmpty(para) = True Then
            Return String.Empty
        End If

        'Lengthのチェック
        Dim RestLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(para) - startIndex + 1

        '2012.11.05 修正START
        If RestLength <= 0 Then
            Return String.Empty

        ElseIf length = 0 OrElse length > RestLength Then
            '2012.11.05 修正END
            length = RestLength

        End If

        '切り抜き
        Dim SJIS As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift-JIS")
        Dim B() As Byte = CType(Array.CreateInstance(GetType(Byte), length), Byte())

        Array.Copy(SJIS.GetBytes(para), startIndex - 1, B, 0, length)

        Dim st1 As String = SJIS.GetString(B)

        '切り抜いた結果、最後の１バイトが全角文字の半分だった場合、その半分は切り捨てる。
        Dim ResultLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(st1) - startIndex + 1

        If Asc(Strings.Right(st1, 1)) = 0 Then
            rtnStr = st1.Substring(0, st1.Length - 1)
        ElseIf length = ResultLength - 1 Then
            rtnStr = st1.Substring(0, st1.Length - 1)
        Else
            rtnStr = st1
        End If

        Return rtnStr

    End Function


    ''' <summary>
    ''' 空文字に対しては常に空文字を返す
    ''' </summary>
    ''' <param name="para"></param>
    ''' <param name="length"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Shared Function ExlString(ByVal para As String, Optional ByVal length As Integer = 0) As String

        '空文字に対しては常に空文字を返す
        If String.IsNullOrEmpty(para) = True Then
            Return String.Empty
        End If

        Return para

    End Function

#End Region

End Class
