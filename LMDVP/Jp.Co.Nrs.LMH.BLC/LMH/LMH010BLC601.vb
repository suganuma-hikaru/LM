' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH010    : EDI入荷検索
'  EDI荷主ID　　　　:  024　　　 : 日本合成化学(名古屋)
'  作  成  者       :  nishikawa
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH010BLC601
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Class TABLE_NM
        Public Const LMH010INOUT As String = LMH010DAC601.TABLE_NM.LMH010INOUT
        Public Const LMH010_INKAEDI_L As String = LMH010DAC601.TABLE_NM.LMH010_INKAEDI_L
        Public Const LMH010_INKAEDI_M As String = LMH010DAC601.TABLE_NM.LMH010_INKAEDI_M
        Public Const LMH010_DEST As String = LMH010DAC601.TABLE_NM.LMH010_DEST
        Public Const LMH010_ZAI_TRS_GOODS As String = LMH010DAC601.TABLE_NM.LMH010_ZAI_TRS_GOODS
        Public Const LMH010_B_INKA_L As String = LMH010DAC601.TABLE_NM.LMH010_B_INKA_L
        Public Const LMH010_B_INKA_M As String = LMH010DAC601.TABLE_NM.LMH010_B_INKA_M
        Public Const LMH010_B_INKA_S As String = LMH010DAC601.TABLE_NM.LMH010_B_INKA_S
        Public Const LMH010_OUTKA_L As String = LMH010DAC601.TABLE_NM.LMH010_OUTKA_L
        Public Const LMH010_OUTKA_M As String = LMH010DAC601.TABLE_NM.LMH010_OUTKA_M
        Public Const LMH010_OUTKA_S As String = LMH010DAC601.TABLE_NM.LMH010_OUTKA_S
        Public Const LMH010_UNSO_L As String = LMH010DAC601.TABLE_NM.LMH010_UNSO_L
        Public Const LMH010_UNSO_M As String = LMH010DAC601.TABLE_NM.LMH010_UNSO_M

    End Class

    ''' <summary>
    ''' 【区分Ｍ】有無フラグ(U009)
    ''' </summary>
    ''' <remarks>
    ''' 　00：無　01：有
    ''' </remarks>
    Private Class EXISTENCE_STATUS
        ''' <summary>
        ''' 無
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NO As String = "00"

        ''' <summary>
        ''' 有
        ''' </summary>
        ''' <remarks></remarks>
        Public Const YES As String = "01"
    End Class

    ''' <summary>
    ''' 【区分Ｍ】発行有無フラグ(H010)
    ''' </summary>
    ''' <remarks>
    ''' 　00：未発行　01：発行済
    ''' </remarks>
    Private Class PUBLICATION_STATUS

        ''' <summary>
        ''' 未発行
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NotYet As String = "00"

        ''' <summary>
        ''' 発行済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Dane As String = "01"
    End Class


    ''' <summary>
    '''【区分Ｍ】 実施フラグ(J005)
    ''' </summary>
    ''' <remarks>
    ''' </remarks>
    Private Class IMPLEMENTATION_STATUS
        ''' <summary>
        ''' 実施しない
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NotImplement As String = "00"

        ''' <summary>
        ''' 実施する
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Implement As String = "01"
    End Class

    ''' <summary>
    ''' 【区分Ｍ】出荷データ区分(S014)
    ''' </summary>
    ''' <remarks>
    ''' 通常出荷か振替で作成されたかを判別する。
    ''' </remarks>
    Private Class OUTKA_KB

        ''' <summary>
        ''' 通常
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Regular As String = "10"

        ''' <summary>
        ''' 名目（容変有り）
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NominalCC As String = "20"


        ''' <summary>
        ''' 名目（容変無し）
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NominalNC As String = "30"

    End Class

    ''' <summary>
    ''' 【区分Ｍ】出荷データ種別(S020)
    ''' </summary>
    ''' <remarks></remarks>
    Private Class SYUBETU_KB
        ''' <summary>
        ''' 通常
        ''' </summary>
        ''' <remarks></remarks>
        Public Const General As String = "10"

        ''' <summary>
        ''' 返品
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ReturnItem As String = "20"

        ''' <summary>
        ''' 倉庫間移動
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Movement As String = "30"

        ''' <summary>
        ''' 廃棄
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Waste As String = "40"

        ''' <summary>
        ''' 振替
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Transfer As String = "50"
    End Class

    ''' <summary>
    ''' 【区分Ｍ】出荷作業進捗(S010)
    ''' </summary>
    ''' <remarks>
    ''' 　出荷作業の進捗をステータス管理する。
    ''' </remarks>
    Private Class OUTKA_STATE_KB

        ''' <summary>
        ''' 予定入力済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const PlanKeyInCompleted As String = "10"

        ''' <summary>
        ''' 指図書印刷
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OrderPrint As String = "30"

        ''' <summary>
        ''' 出庫済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CollectionCompreted As String = "40"

        ''' <summary>
        ''' 検品済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const InspectionCompreted As String = "50"

        ''' <summary>
        ''' 出荷済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const PickingCompreted As String = "60"

        ''' <summary>
        ''' 報告済
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ReportCompleted As String = "90"

        ''' <summary>
        ''' 納品完了
        ''' </summary>
        ''' <remarks></remarks>
        Public Const DeliveryCompleted As String = "95"


    End Class

    ''' <summary>
    ''' 【区分Ｍ】元着払区分(M001)
    ''' </summary>
    ''' <remarks></remarks>
    Private Class PC_KB

        ''' <summary>
        ''' 元払い
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CashBeforeDelivery As String = "01"

        ''' <summary>
        ''' 着払い
        ''' </summary>
        ''' <remarks></remarks>
        Public Const CollectOnDelivery As String = "02"

    End Class

    ''' <summary>
    ''' 【区分Ｍ】ピッキングリスト区分(P001)
    ''' </summary>
    ''' <remarks></remarks>
    Private Class PICK_KB
        ''' <summary>
        ''' まとめ無し
        ''' </summary>
        ''' <remarks></remarks>
        Public Const None As String = "01"

        ''' <summary>
        ''' まとめﾋﾟｯｷﾝｸﾞﾘｽﾄ
        ''' </summary>
        ''' <remarks></remarks>
        Public Const PickList As String = "02"

        ''' <summary>
        ''' ﾄｰﾀﾙﾋﾟｯｷﾝｸﾞﾘｽﾄ
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TotalPickList As String = "03"

    End Class

    ''' <summary>
    ''' 【区分Ｍ】保留品区分(H003)
    ''' </summary>
    ''' <remarks></remarks>
    Private Class SPD_KB

        ''' <summary>
        ''' 出荷可能
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ShipOK As String = "01"
        ''' <summary>
        ''' 保留
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Pending As String = "02"

        ''' <summary>
        ''' 出荷止め
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ShipNG As String = "03"

        ''' <summary>
        ''' 廃棄予定
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Waste As String = "04"

        ''' <summary>
        ''' 保税陸上運送(OLT)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OverLandTransport As String = "05"

        ''' <summary>
        ''' 倉入保税貨物 (IS)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ImportForStorage As String = "06"

        ''' <summary>
        ''' 直接輸入(IC)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ImportForConsumption As String = "07"

        ''' <summary>
        ''' 蔵出輸入(ISW)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ImportFromStorageWarehouse As String = "08"


    End Class

    ''' <summary>
    ''' 【区分Ｍ】簿外品区分(B002)
    ''' </summary>
    ''' <remarks></remarks>
    Private Class OFB_KB

        ''' <summary>
        ''' 簿品
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Normal As String = "01"

        ''' <summary>
        ''' 簿外品
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OffBook As String = "02"
    End Class

    ''' <summary>
    ''' 【区分Ｍ】割当優先区分(W001)
    ''' </summary>
    ''' <remarks></remarks>
    Private Class ALLOC_PRIORITY

        ''' <summary>
        ''' 最優先
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TopPriority As String = "01"

        ''' <summary>
        ''' フリー
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Free As String = "10"

        ''' <summary>
        ''' リザーブ
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Reserve As String = "20"

    End Class

    ''' <summary>
    ''' 【区分Ｍ】届先区分(T018)
    ''' </summary>
    ''' <remarks></remarks>
    Private Class DEST_KB
        ''' <summary>
        ''' 届先
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Delivery As String = "00"

        ''' <summary>
        ''' 輸出情報
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Export As String = "01"

        ''' <summary>
        ''' EDI
        ''' </summary>
        ''' <remarks></remarks>
        Public Const EDI As String = "02"
    End Class

    ''' <summary>
    ''' 【区分Ｍ】運送事由区分(U004)　
    ''' </summary>
    ''' <remarks></remarks>
    Private Class JIYU_KB

        ''' <summary>
        ''' 配送
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Delivery As String = "01"

        ''' <summary>
        ''' 引取
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Accept As String = "02"

        ''' <summary>
        ''' 三角配送
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TriangleDelivery As String = "03"
    End Class

    ''' <summary>
    ''' 【区分Ｍ】元データ区分(M004)　
    ''' </summary>
    ''' <remarks>
    ''' (入荷・出荷～)
    ''' </remarks>
    Private Class MOTO_DATA_KB

        ''' <summary>
        ''' 入荷
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Inbound As String = "10"

        ''' <summary>
        ''' 出荷
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Outbound As String = "20"

        ''' <summary>
        ''' 在庫
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Inventory As String = "30"

        ''' <summary>
        ''' 運送
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Transport As String = "40"

        ''' <summary>
        ''' その他
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Other As String = "50"
    End Class

    ''' <summary>
    ''' 【区分Ｍ】運送温度区分(U006)
    ''' </summary>
    ''' <remarks></remarks>
    Private Class UNSO_ONDO_KB

        ''' <summary>
        ''' 定温車
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ConstTemp As String = "10"

        ''' <summary>
        ''' 保冷・保温剤梱包
        ''' </summary>
        ''' <remarks></remarks>
        Public Const KeepCoolOrWarmPack As String = "20"

        ''' <summary>
        ''' なし
        ''' </summary>
        ''' <remarks></remarks>
        Public Const No As String = "90"
    End Class

    ''' <summary>
    ''' 【区分Ｍ】運送手配(運送元)区分(U005)
    ''' </summary>
    ''' <remarks>
    ''' 
    ''' </remarks>
    Private Class UNSO_TEHAI_KB

        ''' <summary>
        ''' 日陸手配
        ''' </summary>
        ''' <remarks></remarks>
        Public Const NRS As String = "10"

        ''' <summary>
        ''' 先方手配
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OtherParty As String = "20"

        ''' <summary>
        ''' 未定
        ''' </summary>
        ''' <remarks></remarks>
        Public Const Undecided As String = "90"
    End Class


    ''' <summary>
    ''' 【区分Ｍ】税区分(Z001)
    ''' </summary>
    ''' <remarks></remarks>
    Private Class TAX_KB

        ''' <summary>
        ''' 日本
        ''' </summary>
        ''' <remarks></remarks>
        Public Class JP
            ''' <summary>
            ''' 課税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const Tax As String = "01"

            ''' <summary>
            ''' 免税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxFree As String = "02"

            ''' <summary>
            ''' 非課税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxExempt As String = "03"

            ''' <summary>
            ''' 内税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxIncluded As String = "04"
        End Class


        ''' <summary>
        ''' 韓国
        ''' </summary>
        ''' <remarks></remarks>
        Public Class KR
            ''' <summary>
            ''' 課税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const Tax As String = "05"

            ''' <summary>
            ''' 免税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxFree As String = "06"

            ''' <summary>
            ''' 非課税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxExempt As String = "07"

            ''' <summary>
            ''' 内税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxIncluded As String = "08"
        End Class

        ''' <summary>
        ''' 韓国(U)
        ''' </summary>
        ''' <remarks></remarks>
        Public Class KRU
            ''' <summary>
            ''' 課税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const Tax As String = "09"

            ''' <summary>
            ''' 免税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxFree As String = "10"

            ''' <summary>
            ''' 非課税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxExempt As String = "11"

            ''' <summary>
            ''' 内税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxIncluded As String = "12"
        End Class

        ''' <summary>
        ''' 台湾
        ''' </summary>
        ''' <remarks></remarks>
        Public Class TR

            ''' <summary>
            ''' 課税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const Tax As String = "13"

            ''' <summary>
            ''' 免税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxFree As String = "14"

            ''' <summary>
            ''' 非課税
            ''' </summary>
            ''' <remarks></remarks>
            Public Const TaxExempt As String = "15"
        End Class

    End Class


    ''' <summary>
    ''' 荷主参照番号(M品)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CUST_REF_NO_COND_M As String = "M検品"

    ''' <summary>
    ''' 発地コード(M品)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ORIG_CD_COND_M As String = "99999"



#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH010DAC601 = New LMH010DAC601()

    Private _MstDac As LMH010DAC = New LMH010DAC()

    Private _ChkBlc As LMH010BLC = New LMH010BLC()

    '要望番号419 2012.01.26 START
    Private _SetWarningFlg As Boolean = False
    '要望番号419 2012.01.26 END

    ''' <summary>
    ''' 使用するBLC共通クラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMH010BLC = New LMH010BLC()

#End Region

#Region "Method"


#Region "検索処理"


    ''' <summary>
    ''' M品出荷データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    Private Function SelectOutkaDataCondM(ByVal ds As DataSet) As DataSet

        Dim isSuccess As Boolean = False

        Do
            Dim row As DataRow = ds.Tables(TABLE_NM.LMH010INOUT)(0)


            Dim nrsBrCd As String = row.Item("NRS_BR_CD").ToString()
            Dim rowNo As String = row.Item("ROW_NO").ToString()
            Dim ediCtlNo As String = row.Item("EDI_CTL_NO").ToString()

            ' EDI入荷(大)取得
            ds.Merge(MyBase.CallDAC(Me._Dac, "SelectEdiL", ds))
            If (ds.Tables(TABLE_NM.LMH010_INKAEDI_L).Rows.Count = 0) Then

                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN _
                                      , "E011" _
                                      , _
                                      , rowNo _
                                      , LMH010BLC.EXCEL_COLTITLE _
                                      , ediCtlNo)

                Exit Do
            ElseIf (String.IsNullOrEmpty(ds.Tables(TABLE_NM.LMH010_INKAEDI_L)(0).Item("INKA_CTL_NO_L").ToString())) Then

                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN _
                  , "E968" _
                  , _
                  , rowNo _
                  , LMH010BLC.EXCEL_COLTITLE _
                  , ediCtlNo)

                Exit Do
            End If


            ' EDI入荷(中)取得
            ds.Merge(MyBase.CallDAC(Me._Dac, "SelectEdiM", ds))
            If (ds.Tables(TABLE_NM.LMH010_INKAEDI_M).Rows.Count = 0) Then

                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN _
                                      , "E011" _
                                      , _
                                      , rowNo _
                                      , LMH010BLC.EXCEL_COLTITLE _
                                      , ediCtlNo)

                Exit Do
            End If

            ' 届先取得
            ds.Merge(MyBase.CallDAC(Me._Dac, "SelectDestCondM", ds))
            If (ds.Tables(TABLE_NM.LMH010_DEST).Rows.Count = 0) Then

                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN _
                                      , "E967" _
                                      , _
                                      , rowNo _
                                      , LMH010BLC.EXCEL_COLTITLE _
                                      , ediCtlNo)

                Exit Do
            End If

            ' M品取得
            ds.Merge(MyBase.CallDAC(Me._Dac, "SelectZaiTrs", ds))
            If (ds.Tables(TABLE_NM.LMH010_ZAI_TRS_GOODS).Rows.Count = 0) Then

                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN _
                                      , "E966" _
                                      , _
                                      , rowNo _
                                      , LMH010BLC.EXCEL_COLTITLE _
                                      , ediCtlNo)

                Exit Do
            End If

            ' 入荷(大)取得
            ds.Merge(MyBase.CallDAC(Me._Dac, "SelectInkaL", ds))
            If (ds.Tables(TABLE_NM.LMH010_B_INKA_L).Rows.Count = 0) Then

                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN _
                                      , "E968" _
                                      , _
                                      , rowNo _
                                      , LMH010BLC.EXCEL_COLTITLE _
                                      , ediCtlNo)

                Exit Do
            End If

            ' 入荷(中)取得
            ds.Merge(MyBase.CallDAC(Me._Dac, "SelectInkaM", ds))
            If (ds.Tables(TABLE_NM.LMH010_B_INKA_M).Rows.Count = 0) Then

                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN _
                                      , "E011" _
                                      , _
                                      , rowNo _
                                      , LMH010BLC.EXCEL_COLTITLE _
                                      , ediCtlNo)

                Exit Do
            End If

            ' 入荷(小)取得
            ds.Merge(MyBase.CallDAC(Me._Dac, "SelectMaxInkaS", ds))
            If (ds.Tables(TABLE_NM.LMH010_B_INKA_S).Rows.Count = 0) Then

                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN _
                                      , "E011" _
                                      , _
                                      , rowNo _
                                      , LMH010BLC.EXCEL_COLTITLE _
                                      , ediCtlNo)

                Exit Do
            End If


            ' M品出荷データ設定
            isSuccess = Me.SetOutkaDataCondM(ds)

        Loop While (False)


        If (isSuccess = False) Then
            MyBase.SetMessage("E235")
        End If

        Return ds

    End Function
    ''' <summary>
    ''' M品出庫データ設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetOutkaDataCondM(ByVal ds As DataSet) As Boolean


        Dim ediLRow As DataRow = ds.Tables(TABLE_NM.LMH010_INKAEDI_L)(0)
        Dim nrsBrCd As String = ediLRow.Item("NRS_BR_CD").ToString()

        ' 出荷管理番号L払い出し
        Dim outkaNoL As String = New Utility.NumberMasterUtility() _
                                            .GetAutoCode(Utility.NumberMasterUtility.NumberKbn.OUTKA_NO_L _
                                                       , Me _
                                                       , nrsBrCd)

        ' M品の出荷管理番号を保持
        ediLRow.Item("FREE_C24") = outkaNoL


        ' 運送番号L払い出し
        Dim unsoNoL As String = New Utility.NumberMasterUtility() _
                                            .GetAutoCode(Utility.NumberMasterUtility.NumberKbn.UNSO_NO_L _
                                                       , Me _
                                                       , nrsBrCd)


        Dim inkaDate As String = ediLRow.Item("INKA_DATE").ToString()

        Dim outkaM As New Dictionary(Of Tuple(Of String, String, String), Integer)

        Dim newRow As DataRow = Nothing
        For Each zaiRow As DataRow In ds.Tables(TABLE_NM.LMH010_ZAI_TRS_GOODS).Rows
            newRow = ds.Tables(TABLE_NM.LMH010_OUTKA_S).NewRow()

            Dim key As Tuple(Of String, String, String) _
                = Tuple.Create(zaiRow.Item("GOODS_CD_NRS").ToString() _
                             , zaiRow.Item("LOT_NO").ToString() _
                             , zaiRow.Item("IRIME").ToString())

            If (outkaM.Keys.Contains(key) = False) Then
                outkaM.Add(key, outkaM.Count + 1)
            End If

            Dim irime As Double = Convert.ToDouble(zaiRow.Item("IRIME"))
            Dim stdIrime As Double = Convert.ToDouble(zaiRow.Item("STD_IRIME_NB"))
            Dim stdWeight As Double = Convert.ToDouble(zaiRow.Item("STD_WT_KGS"))
            Dim allocCanNb As Integer = Convert.ToInt32(zaiRow.Item("ALLOC_CAN_NB"))

            newRow.Item("NRS_BR_CD") = nrsBrCd
            newRow.Item("OUTKA_NO_L") = outkaNoL
            newRow.Item("OUTKA_NO_M") = String.Format("{0:D3}", outkaM(key))
            newRow.Item("OUTKA_NO_S") = String.Format("{0:D3}", ds.Tables(TABLE_NM.LMH010_OUTKA_S).Rows.Count + 1)
            newRow.Item("TOU_NO") = zaiRow.Item("TOU_NO").ToString()
            newRow.Item("SITU_NO") = zaiRow.Item("SITU_NO").ToString()
            newRow.Item("ZONE_CD") = zaiRow.Item("ZONE_CD").ToString()
            newRow.Item("LOCA") = zaiRow.Item("LOCA").ToString()
            newRow.Item("LOT_NO") = zaiRow.Item("LOT_NO").ToString()
            newRow.Item("SERIAL_NO") = zaiRow.Item("SERIAL_NO").ToString()
            newRow.Item("OUTKA_TTL_NB") = allocCanNb
            newRow.Item("OUTKA_TTL_QT") = allocCanNb * irime
            newRow.Item("ZAI_REC_NO") = zaiRow.Item("ZAI_REC_NO").ToString()
            newRow.Item("INKA_NO_L") = zaiRow.Item("INKA_NO_L").ToString()
            newRow.Item("INKA_NO_M") = zaiRow.Item("INKA_NO_M").ToString()
            newRow.Item("INKA_NO_S") = zaiRow.Item("INKA_NO_S").ToString()
            newRow.Item("ZAI_UPD_FLAG") = IMPLEMENTATION_STATUS.NotImplement ' J005
            newRow.Item("ALCTD_CAN_NB") = 0
            newRow.Item("ALCTD_NB") = allocCanNb
            newRow.Item("ALCTD_CAN_QT") = 0.0F
            newRow.Item("ALCTD_QT") = zaiRow.Item("ALLOC_CAN_QT").ToString()
            newRow.Item("IRIME") = zaiRow.Item("IRIME").ToString()
            newRow.Item("BETU_WT") = (irime * stdWeight) / stdIrime
            newRow.Item("COA_FLAG") = PUBLICATION_STATUS.NotYet ' H010
            newRow.Item("REMARK") = zaiRow.Item("REMARK").ToString()
            newRow.Item("SMPL_FLAG") = IMPLEMENTATION_STATUS.NotImplement ' J005
            newRow.Item("REC_NO") = ""
            ds.Tables(TABLE_NM.LMH010_OUTKA_S).Rows.Add(newRow)
        Next

        For Each outkaMM As KeyValuePair(Of Tuple(Of String, String, String), Integer) In outkaM


            Dim outkaNoM As String = String.Format("{0:D3}", outkaMM.Value)

            Dim outkaSRows As IEnumerable(Of DataRow) _
                = ds.Tables(TABLE_NM.LMH010_OUTKA_S).AsEnumerable() _
                       .Where(Function(r) outkaNoM.Equals(r.Item("OUTKA_NO_M")))

            Dim goodsRow As DataRow = ds.Tables(TABLE_NM.LMH010_ZAI_TRS_GOODS).AsEnumerable() _
                                           .Where(Function(r) nrsBrCd.Equals(r.Item("NRS_BR_CD")) AndAlso _
                                                              outkaMM.Key.Item1.Equals(r.Item("GOODS_CD_NRS"))) _
                                           .FirstOrDefault

            Dim outkaTtlNb As Integer = outkaSRows.Sum(Function(r) Convert.ToInt32(r.Item("OUTKA_TTL_NB")))
            Dim outkaTtlQt As Double = outkaSRows.Sum(Function(r) Convert.ToDouble(r.Item("OUTKA_TTL_QT")))
            Dim pkgNb As Integer = Convert.ToInt32(goodsRow.Item("PKG_NB"))

            Dim outkaHasu As Integer = 0
            Dim outkaPkgNb As Integer = Math.DivRem(outkaTtlNb, pkgNb, outkaHasu)

            newRow = ds.Tables(TABLE_NM.LMH010_OUTKA_M).NewRow()

            newRow.Item("NRS_BR_CD") = nrsBrCd
            newRow.Item("OUTKA_NO_L") = outkaNoL
            newRow.Item("OUTKA_NO_M") = outkaNoM
            newRow.Item("EDI_SET_NO") = ""
            newRow.Item("COA_YN") = goodsRow.Item("COA_YN")
            newRow.Item("CUST_ORD_NO_DTL") = ""
            newRow.Item("BUYER_ORD_NO_DTL") = ""
            newRow.Item("GOODS_CD_NRS") = outkaMM.Key.Item1
            newRow.Item("RSV_NO") = ""
            newRow.Item("LOT_NO") = outkaMM.Key.Item2
            newRow.Item("SERIAL_NO") = ""
            newRow.Item("ALCTD_KB") = goodsRow.Item("ALCTD_KB")
            newRow.Item("OUTKA_PKG_NB") = outkaPkgNb
            newRow.Item("OUTKA_HASU") = outkaHasu
            newRow.Item("OUTKA_QT") = "0.000"
            newRow.Item("OUTKA_TTL_NB") = outkaTtlNb
            newRow.Item("OUTKA_TTL_QT") = outkaTtlQt
            newRow.Item("ALCTD_NB") = outkaTtlNb
            newRow.Item("ALCTD_QT") = outkaTtlQt
            newRow.Item("BACKLOG_NB") = 0
            newRow.Item("BACKLOG_QT") = 0
            newRow.Item("UNSO_ONDO_KB") = ""
            newRow.Item("IRIME") = outkaMM.Key.Item3
            newRow.Item("IRIME_UT") = goodsRow.Item("STD_IRIME_UT")
            newRow.Item("OUTKA_M_PKG_NB") = outkaPkgNb + If(outkaHasu > 0, 1, 0)
            newRow.Item("REMARK") = ""
            newRow.Item("SIZE_KB") = ""
            newRow.Item("ZAIKO_KB") = ""
            newRow.Item("SOURCE_CD") = ""
            newRow.Item("YELLOW_CARD") = ""
            newRow.Item("GOODS_CD_NRS_FROM") = ""
            newRow.Item("PRINT_SORT") = 0

            ds.Tables(TABLE_NM.LMH010_OUTKA_M).Rows.Add(newRow)


            newRow = ds.Tables(TABLE_NM.LMH010_UNSO_M).NewRow

            newRow.Item("NRS_BR_CD") = nrsBrCd
            newRow.Item("UNSO_NO_L") = unsoNoL
            newRow.Item("UNSO_NO_M") = String.Format("{0:D3}", ds.Tables(TABLE_NM.LMH010_UNSO_M).Rows.Count + 1)
            newRow.Item("GOODS_CD_NRS") = outkaMM.Key.Item1
            newRow.Item("GOODS_NM") = goodsRow.Item("GOODS_NM")
            newRow.Item("UNSO_TTL_NB") = outkaTtlNb - outkaHasu / pkgNb
            newRow.Item("NB_UT") = goodsRow.Item("NB_UT")
            newRow.Item("UNSO_TTL_QT") = outkaTtlQt
            newRow.Item("QT_UT") = goodsRow.Item("STD_IRIME_UT")
            newRow.Item("HASU") = outkaHasu
            newRow.Item("ZAI_REC_NO") = ""
            newRow.Item("UNSO_ONDO_KB") = UNSO_ONDO_KB.No ' U006
            newRow.Item("IRIME") = outkaMM.Key.Item3
            newRow.Item("IRIME_UT") = goodsRow.Item("STD_IRIME_UT")
            newRow.Item("BETU_WT") = goodsRow.Item("STD_WT_KGS")
            newRow.Item("SIZE_KB") = "" ' T010
            newRow.Item("ZBUKA_CD") = ""
            newRow.Item("ABUKA_CD") = ""
            newRow.Item("PKG_NB") = goodsRow.Item("PKG_NB")
            newRow.Item("LOT_NO") = outkaMM.Key.Item2
            newRow.Item("REMARK") = ""
            newRow.Item("SYS_DEL_FLG") = LMConst.FLG.OFF

            ds.Tables(TABLE_NM.LMH010_UNSO_M).Rows.Add(newRow)


        Next


        Dim destRow As DataRow = ds.Tables(TABLE_NM.LMH010_DEST).Rows(0)

        newRow = ds.Tables(TABLE_NM.LMH010_OUTKA_L).NewRow()
        newRow.Item("NRS_BR_CD") = nrsBrCd
        newRow.Item("OUTKA_NO_L") = outkaNoL
        newRow.Item("FURI_NO") = ""
        newRow.Item("OUTKA_KB") = OUTKA_KB.Regular   ' S014
        newRow.Item("SYUBETU_KB") = SYUBETU_KB.General ' S020
        newRow.Item("OUTKA_STATE_KB") = OUTKA_STATE_KB.PickingCompreted ' S010
        newRow.Item("OUTKAHOKOKU_YN") = EXISTENCE_STATUS.NO ' U009
        newRow.Item("PICK_KB") = PICK_KB.None  ' P001
        newRow.Item("DENP_NO") = ""
        newRow.Item("ARR_KANRYO_INFO") = ""
        newRow.Item("WH_CD") = ediLRow.Item("NRS_WH_CD").ToString()
        newRow.Item("OUTKA_PLAN_DATE") = inkaDate
        newRow.Item("OUTKO_DATE") = inkaDate
        newRow.Item("ARR_PLAN_DATE") = inkaDate
        newRow.Item("ARR_PLAN_TIME") = "" ' N010
        newRow.Item("HOKOKU_DATE") = ""
        newRow.Item("TOUKI_HOKAN_YN") = EXISTENCE_STATUS.YES ' U009
        newRow.Item("END_DATE") = inkaDate
        newRow.Item("CUST_CD_L") = ediLRow.Item("CUST_CD_L").ToString()
        newRow.Item("CUST_CD_M") = ediLRow.Item("CUST_CD_M").ToString()
        newRow.Item("SHIP_CD_L") = ""
        newRow.Item("SHIP_CD_M") = ""
        newRow.Item("DEST_CD") = destRow.Item("DEST_CD")
        newRow.Item("DEST_AD_3") = ""
        newRow.Item("DEST_TEL") = ""
        newRow.Item("NHS_REMARK") = ""
        newRow.Item("SP_NHS_KB") = destRow.Item("SP_NHS_KB")
        newRow.Item("COA_YN") = destRow.Item("COA_YN")
        newRow.Item("CUST_ORD_NO") = ""
        newRow.Item("BUYER_ORD_NO") = ""
        newRow.Item("REMARK") = ""
        newRow.Item("OUTKA_PKG_NB") = ds.Tables(TABLE_NM.LMH010_OUTKA_M).AsEnumerable().Sum(Function(r) Convert.ToInt32(r.Item("OUTKA_M_PKG_NB")))
        newRow.Item("DENP_YN") = EXISTENCE_STATUS.NO   ' U009
        newRow.Item("PC_KB") = PC_KB.CashBeforeDelivery ' M001
        newRow.Item("NIYAKU_YN") = EXISTENCE_STATUS.NO  ' U009
        newRow.Item("DEST_KB") = DEST_KB.Delivery ' T018
        newRow.Item("DEST_NM") = destRow.Item("DEST_NM")
        newRow.Item("AD_1") = destRow.Item("AD_1")
        newRow.Item("AD_2") = destRow.Item("AD_2")
        newRow.Item("ALL_PRINT_FLAG") = PUBLICATION_STATUS.NotYet ' H010
        newRow.Item("NIHUDA_FLAG") = PUBLICATION_STATUS.NotYet ' H010
        newRow.Item("NHS_FLAG") = PUBLICATION_STATUS.NotYet ' H010
        newRow.Item("DENP_FLAG") = PUBLICATION_STATUS.NotYet ' H010
        newRow.Item("COA_FLAG") = PUBLICATION_STATUS.NotYet ' H010
        newRow.Item("HOKOKU_FLAG") = PUBLICATION_STATUS.NotYet ' H010
        newRow.Item("MATOME_PICK_FLAG") = PUBLICATION_STATUS.NotYet ' H010
        newRow.Item("MATOME_PRINT_DATE") = ""
        newRow.Item("MATOME_PRINT_TIME") = ""
        newRow.Item("LAST_PRINT_DATE") = ""
        newRow.Item("LAST_PRINT_TIME") = ""
        newRow.Item("SASZ_USER") = ""
        newRow.Item("OUTKO_USER") = Me.GetUserID
        newRow.Item("KEN_USER") = ""
        newRow.Item("OUTKA_USER") = ""
        newRow.Item("HOU_USER") = ""
        newRow.Item("ORDER_TYPE") = ""
        newRow.Item("WH_KENPIN_WK_STATUS") = "" ' S107


        ds.Tables(TABLE_NM.LMH010_OUTKA_L).Rows.Add(newRow)

        newRow = ds.Tables(TABLE_NM.LMH010_UNSO_L).NewRow

        newRow.Item("NRS_BR_CD") = nrsBrCd
        newRow.Item("UNSO_NO_L") = unsoNoL
        newRow.Item("YUSO_BR_CD") = nrsBrCd
        newRow.Item("INOUTKA_NO_L") = outkaNoL
        newRow.Item("NRS_WH_CD") = ediLRow.Item("NRS_WH_CD").ToString()
        newRow.Item("TRIP_NO") = ""
        newRow.Item("UNSO_CD") = destRow.Item("UNSO_CD")
        newRow.Item("UNSO_BR_CD") = destRow.Item("UNSO_BR_CD")
        newRow.Item("BIN_KB") = destRow.Item("BIN_KB")
        newRow.Item("JIYU_KB") = JIYU_KB.Accept ' U004
        newRow.Item("DENP_NO") = ""
        newRow.Item("OUTKA_PLAN_DATE") = inkaDate
        newRow.Item("OUTKA_PLAN_TIME") = ""
        newRow.Item("ARR_PLAN_DATE") = inkaDate
        newRow.Item("ARR_PLAN_TIME") = ""
        newRow.Item("ARR_ACT_TIME") = ""
        newRow.Item("CUST_CD_L") = ediLRow.Item("CUST_CD_L").ToString()
        newRow.Item("CUST_CD_M") = ediLRow.Item("CUST_CD_M").ToString()
        newRow.Item("CUST_REF_NO") = CUST_REF_NO_COND_M
        newRow.Item("SHIP_CD") = destRow.Item("DEST_CD")
        newRow.Item("ORIG_CD") = ORIG_CD_COND_M
        newRow.Item("DEST_CD") = destRow.Item("DEST_CD")
        newRow.Item("UNSO_PKG_NB") = ds.Tables(TABLE_NM.LMH010_OUTKA_M).AsEnumerable().Sum(Function(r) Convert.ToInt32(r.Item("OUTKA_M_PKG_NB")))
        newRow.Item("NB_UT") = ""
        newRow.Item("UNSO_WT") = 0
        newRow.Item("UNSO_ONDO_KB") = UNSO_ONDO_KB.No ' U006
        newRow.Item("PC_KB") = PC_KB.CashBeforeDelivery ' M001
        newRow.Item("TARIFF_BUNRUI_KB") = "" ' T015
        newRow.Item("VCLE_KB") = "" ' S012
        newRow.Item("MOTO_DATA_KB") = MOTO_DATA_KB.Outbound ' M004
        newRow.Item("TAX_KB") = "" ' Z001
        newRow.Item("REMARK") = ""
        newRow.Item("SEIQ_TARIFF_CD") = ""
        newRow.Item("SEIQ_ETARIFF_CD") = ""
        newRow.Item("AD_3") = ""
        newRow.Item("UNSO_TEHAI_KB") = UNSO_TEHAI_KB.Undecided ' U005
        newRow.Item("BUY_CHU_NO") = ""
        newRow.Item("AREA_CD") = ""
        newRow.Item("TYUKEI_HAISO_FLG") = EXISTENCE_STATUS.NO ' U009
        newRow.Item("SYUKA_TYUKEI_CD") = ""
        newRow.Item("HAIKA_TYUKEI_CD") = ""
        newRow.Item("TRIP_NO_SYUKA") = ""
        newRow.Item("TRIP_NO_TYUKEI") = ""
        newRow.Item("TRIP_NO_HAIKA") = ""
        newRow.Item("SHIHARAI_TARIFF_CD") = ""
        newRow.Item("SHIHARAI_ETARIFF_CD") = ""
        newRow.Item("SYS_DEL_FLG") = LMConst.FLG.OFF

        ds.Tables(TABLE_NM.LMH010_UNSO_L).Rows.Add(newRow)


        ' INKA_S_NOの最大値を複写して、登録用に初期化
        Dim maxInkaSRows As IEnumerable(Of DataRow) _
            = ds.Tables(TABLE_NM.LMH010_B_INKA_S).Copy().AsEnumerable()

        ds.Tables(TABLE_NM.LMH010_B_INKA_S).Clear()

        For Each mRow As DataRow In ds.Tables(TABLE_NM.LMH010_B_INKA_M).Rows

            Dim maxinkaRow As DataRow _
                = maxInkaSRows.Where(Function(r) r.Item("INKA_NO_M").Equals(mRow.Item("INKA_NO_M"))) _
                              .FirstOrDefault

            Dim inkaSNumber As Integer = 0
            If (maxinkaRow IsNot Nothing) Then
                inkaSNumber = Convert.ToInt32(maxinkaRow.Item("INKA_NO_S"))
            End If


            For Each zaiRow As DataRow In ds.Tables(TABLE_NM.LMH010_ZAI_TRS_GOODS).AsEnumerable() _
                                               .Where(Function(r) mRow.Item("GOODS_CD_NRS").Equals(r.Item("GOODS_CD_NRS")))

                Dim irime As Double = Convert.ToDouble(zaiRow.Item("IRIME"))
                Dim stdIrime As Double = Convert.ToDouble(zaiRow.Item("STD_IRIME_NB"))
                Dim stdWeight As Double = Convert.ToDouble(zaiRow.Item("STD_WT_KGS"))
                Dim allocCanNb As Integer = Convert.ToInt32(zaiRow.Item("ALLOC_CAN_NB"))
                Dim pkgNb As Integer = Convert.ToInt32(zaiRow.Item("PKG_NB"))

                Dim hasu As Integer = 0
                Dim konsu As Integer = Math.DivRem(allocCanNb, pkgNb, hasu)

                newRow = ds.Tables(TABLE_NM.LMH010_B_INKA_S).NewRow

                newRow.Item("NRS_BR_CD") = mRow.Item("NRS_BR_CD")
                newRow.Item("INKA_NO_L") = mRow.Item("INKA_NO_L")
                newRow.Item("INKA_NO_M") = mRow.Item("INKA_NO_M")
                newRow.Item("INKA_NO_S") = String.Format("{0:D3}", inkaSNumber + 1)
                newRow.Item("ZAI_REC_NO") = zaiRow.Item("ZAI_REC_NO")
                newRow.Item("LOT_NO") = zaiRow.Item("LOT_NO")
                newRow.Item("LOCA") = zaiRow.Item("LOCA")
                newRow.Item("TOU_NO") = zaiRow.Item("TOU_NO")
                newRow.Item("SITU_NO") = zaiRow.Item("SITU_NO")
                newRow.Item("ZONE_CD") = zaiRow.Item("ZONE_CD")
                newRow.Item("KONSU") = konsu
                newRow.Item("HASU") = hasu
                newRow.Item("IRIME") = zaiRow.Item("IRIME")
                newRow.Item("BETU_WT") = (stdWeight * irime) / stdIrime
                newRow.Item("SERIAL_NO") = zaiRow.Item("SERIAL_NO")
                newRow.Item("GOODS_COND_KB_1") = ""
                newRow.Item("GOODS_COND_KB_2") = ""
                newRow.Item("GOODS_COND_KB_3") = ""
                newRow.Item("GOODS_CRT_DATE") = zaiRow.Item("GOODS_CRT_DATE")
                newRow.Item("LT_DATE") = zaiRow.Item("LT_DATE")
                newRow.Item("SPD_KB") = SPD_KB.ShipOK ' H003
                newRow.Item("OFB_KB") = OFB_KB.Normal ' B002
                newRow.Item("DEST_CD") = ""
                newRow.Item("REMARK") = ""
                newRow.Item("ALLOC_PRIORITY") = ALLOC_PRIORITY.Free ' W001
                newRow.Item("REMARK_OUT") = ""
                newRow.Item("SYS_DEL_FLG") = LMConst.FLG.OFF

                ds.Tables(TABLE_NM.LMH010_B_INKA_S).Rows.Add(newRow)
                inkaSNumber += 1
            Next
        Next

        Return True

    End Function


    ''' <summary>
    ''' M振替出荷データ登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertOutkaCondM(ByVal ds As DataSet) As DataSet

        Dim isSuccess As Boolean = False

        ' メッセージクリア
        MyBase.SetMessage(Nothing)

        Do

            Dim row As DataRow = ds.Tables(TABLE_NM.LMH010INOUT)(0)
            Dim nrsBrCd As String = row.Item("NRS_BR_CD").ToString()
            Dim rowNo As String = row.Item("ROW_NO").ToString()
            Dim ediCtlNo As String = row.Item("EDI_CTL_NO").ToString()


            MyBase.CallDAC(Me._Dac, "InsertOutkaL", ds)
            If (Me.GetResultCount() <> ds.Tables(TABLE_NM.LMH010_OUTKA_L).Rows.Count) Then

                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN _
                                     , "E011", _
                                     , rowNo _
                                     , LMH010BLC.EXCEL_COLTITLE _
                                     , ediCtlNo)

                Exit Do
            End If

            ds = MyBase.CallDAC(Me._Dac, "InsertOutkaM", ds)
            If (Me.GetResultCount() <> ds.Tables(TABLE_NM.LMH010_OUTKA_M).Rows.Count) Then

                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN _
                                     , "E011", _
                                     , rowNo _
                                     , LMH010BLC.EXCEL_COLTITLE _
                                     , ediCtlNo)

                Exit Do
            End If
            ds = MyBase.CallDAC(Me._Dac, "InsertOutkaS", ds)
            If (Me.GetResultCount() <> ds.Tables(TABLE_NM.LMH010_OUTKA_S).Rows.Count) Then

                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN _
                                     , "E011", _
                                     , rowNo _
                                     , LMH010BLC.EXCEL_COLTITLE _
                                     , ediCtlNo)

                Exit Do
            End If

            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoL", ds)
            If (Me.GetResultCount() <> ds.Tables(TABLE_NM.LMH010_UNSO_L).Rows.Count) Then

                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN _
                                     , "E011", _
                                     , rowNo _
                                     , LMH010BLC.EXCEL_COLTITLE _
                                     , ediCtlNo)

                Exit Do
            End If
            ds = MyBase.CallDAC(Me._Dac, "InsertUnsoM", ds)
            If (Me.GetResultCount() <> ds.Tables(TABLE_NM.LMH010_UNSO_M).Rows.Count) Then

                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN _
                                     , "E011", _
                                     , rowNo _
                                     , LMH010BLC.EXCEL_COLTITLE _
                                     , ediCtlNo)

                Exit Do
            End If
            ds = MyBase.CallDAC(Me._Dac, "UpdateZaiTrsCondM", ds)
            If (Me.GetResultCount() <> ds.Tables(TABLE_NM.LMH010_ZAI_TRS_GOODS).Rows.Count) Then

                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN _
                                     , "E011", _
                                     , rowNo _
                                     , LMH010BLC.EXCEL_COLTITLE _
                                     , ediCtlNo)

                Exit Do
            End If

            ds = MyBase.CallDAC(Me._Dac, "UpdateInkaSSysDelFlgOn", ds)

            ds = MyBase.CallDAC(Me._Dac, "InsertInkaS", ds)
            If (Me.GetResultCount() <> ds.Tables(TABLE_NM.LMH010_B_INKA_S).Rows.Count) Then

                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN _
                                     , "E011", _
                                     , rowNo _
                                     , LMH010BLC.EXCEL_COLTITLE _
                                     , ediCtlNo)

                Exit Do
            End If
            ds = MyBase.CallDAC(Me._Dac, "UpdateInkaL", ds)
            If (Me.GetResultCount() <> ds.Tables(TABLE_NM.LMH010_B_INKA_L).Rows.Count) Then

                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN _
                                     , "E011", _
                                     , rowNo _
                                     , LMH010BLC.EXCEL_COLTITLE _
                                     , ediCtlNo)

                Exit Do
            End If

            ds = MyBase.CallDAC(Me._Dac, "UpdateEdiInkaLCondM", ds)
            If (Me.GetResultCount() <> ds.Tables(TABLE_NM.LMH010_INKAEDI_L).Rows.Count) Then

                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN _
                                     , "E011", _
                                     , rowNo _
                                     , LMH010BLC.EXCEL_COLTITLE _
                                     , ediCtlNo)

                Exit Do
            End If

            isSuccess = True

        Loop While (False)


        If (isSuccess = False) Then
            MyBase.SetMessage("E235")
        End If


        Return ds

    End Function


#End Region


#Region "検索処理(削除)"

    '#Region "検索件数取得"
    '    ''' <summary>
    '    ''' 検索件数取得
    '    ''' </summary>
    '    ''' <param name="ds"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Private Function SelectData(ByVal ds As DataSet) As DataSet

    '        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

    '        'メッセージコードの設定
    '        Dim count As Integer = MyBase.GetResultCount()
    '        If count < 1 Then
    '            '0件の場合
    '            MyBase.SetMessage("G001")
    '        ElseIf count > MyBase.GetMaxResultCount() Then
    '            '表示最大件数を超える場合
    '            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
    '        ElseIf MyBase.GetLimitCount() < count Then
    '            '閾値以上の場合
    '            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
    '        End If

    '        Return ds

    '    End Function
    '#End Region

    '#Region "検索値の取得"
    '    ''' <summary>
    '    ''' 検索の値取得
    '    ''' </summary>
    '    ''' <param name="ds">DataSet</param>
    '    ''' <returns>DataSet</returns>
    '    ''' <remarks></remarks>
    '    Private Function SelectListData(ByVal ds As DataSet) As DataSet

    '        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    '    End Function
    '#End Region

#End Region

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

        'EDI入荷(大)の取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiL", ds)

        If ds.Tables("LMH010_INKAEDI_L").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI入荷(中)の取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEdiM", ds)

        If ds.Tables("LMH010_INKAEDI_M").Rows.Count = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

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

        'データセット設定処理(入荷S)
        ds = Me.SetDatasetInkaS(ds)

        'データセット設定処理(受信ヘッダ)
        ds = Me.SetDatasetRcvHed(ds)

        'データセット設定処理(受信明細)
        ds = Me.SetDatasetRcvDtl(ds)

        'データセット設定処理(作業)
        ds = Me.SetDatasetSagyo(ds)

        'データセット設定(運送大,中)
        If ds.Tables("LMH010_INKAEDI_L").Rows(0)("UNCHIN_TP").ToString() = "10" Then
            ds = Me.SetDatasetUnsoL(ds)
            ds = Me.SetDatasetUnsoM(ds)
            ds = Me.SetdatasetUnsoJyuryo(ds)

            '▼▼▼要望番号:466
            '運送Lの関連チェック
            If Me.UnsoCheck(ds, rowNo, ediCtlNo) = False Then
                Return ds
            End If
            '▲▲▲要望番号:466
        End If

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

        '受信ヘッダの更新
        'DEL 2017/01/10
        'ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)
        'If MyBase.GetResultCount = 0 Then
        '    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
        '    Return ds
        'End If

        '受信明細の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        '入荷Lの作成
        ds = MyBase.CallDAC(Me._Dac, "InsertInkaL", ds)

        '入荷Mの作成
        ds = MyBase.CallDAC(Me._Dac, "InsertInkaM", ds)

        '入荷Sの作成
        ds = MyBase.CallDAC(Me._Dac, "InsertInkaS", ds)

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

    '▼▼▼要望番号:466
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
    '▲▲▲要望番号:466

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
            'MyBase.SetMessage("E326", New String() {"入荷区分", "区分マスタ"})
            Return False
        End If

        '進捗区分
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N004
        drJudge("KBN_CD") = drEdiL("INKA_STATE_KB")

        Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"進捗区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"進捗区分", "区分マスタ"})
            Return False
        End If

        '倉庫
        Call MyBase.CallDAC(Me._MstDac, "SelectDataSoko", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"倉庫コード", "倉庫マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"倉庫コード", "倉庫マスタ"})
            Return False
        End If

        '荷主マスタ
        Call MyBase.CallDAC(Me._MstDac, "SelectDataCust", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"荷主コード", "荷主マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"荷主コード", "荷主マスタ"})
            Return False
        End If

        '課税区分
        drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_Z001
        drJudge("KBN_CD") = drEdiL("TAX_KB")
        Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"課税区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            'MyBase.SetMessage("E326", New String() {"課税区分", "区分マスタ"})
            Return False
        End If

        '運送手配区分
        If String.IsNullOrEmpty(drEdiL("UNCHIN_TP").ToString()) = False Then
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_U005
            drJudge("KBN_CD") = drEdiL("UNCHIN_TP")
            Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"運送手配区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"運送手配区分", "区分マスタ"})
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
                'MyBase.SetMessage("E326", New String() {"タリフ分類区分", "区分マスタ"})
                Return False
            End If
        End If

        '届先コード
        If String.IsNullOrEmpty(drEdiL("OUTKA_MOTO").ToString()) = False Then
            Call MyBase.CallDAC(Me._MstDac, "SelectDataDest", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"届先コード", "届先マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"届先コード", "届先マスタ"})
                Return False
            End If
        End If

        '車両区分
        If String.IsNullOrEmpty(drEdiL("SYARYO_KB").ToString()) = False Then
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_S012
            drJudge("KBN_CD") = drEdiL("SYARYO_KB")
            Call MyBase.CallDAC(Me._MstDac, "SelectDataZKbn", ds)

            If MyBase.GetResultCount = 0 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E326", New String() {"車両区分", "区分マスタ"}, rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
                'MyBase.SetMessage("E326", New String() {"車両区分", "区分マスタ"})
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
                'MyBase.SetMessage("E326", New String() {"運送温度区分", "区分マスタ"})
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
                'MyBase.SetMessage("E326", New String() {"運送会社コード", "運送会社マスタ"})
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
                'MyBase.SetMessage("E326", New String() {"タリフコード", "マスタ"})
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
                    'MyBase.SetMessage("E377", New String() {"入荷データ"})
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

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            setDtM.ImportRow(dtM.Rows(i))
            setDtL.ImportRow(dtL.Rows(0))

            '条件の再設定
            setDtM = Me.SetGoodsCdFromWarning(setDtM, ds, LMH010BLC.NCGO_WID_M001)

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
                    ds = Me._ChkBlc.SetWarningM("W162", LMH010BLC.NCGO_WID_M001, ds, setDs, msgArray)
                    'MyBase.SetMessage("W162")
                    flgWarning = True 'ワーニングフラグをたてて処理続行
                    Continue For
                End If

                '要望番号419 2012.02.02 追加START
            ElseIf GetResultCount() = 1 Then

                If Me._SetWarningFlg = False AndAlso String.IsNullOrEmpty(dtM.Rows(i)("NRS_GOODS_CD").ToString()) = True AndAlso _
                   Convert.ToDecimal(dtM.Rows(i)("IRIME")).Equals(Convert.ToDecimal(goodsDt.Rows(0)("STD_IRIME_NB"))) = False Then
                    '再検索で商品が確定できない場合はワーニング設定して、次の中レコードへ
                    ds = Me._ChkBlc.SetWarningM("W192", LMH010BLC.NCGO_WID_M001, ds, setDs, msgArray)
                    flgWarning = True 'ワーニングフラグをたてて処理続行
                    Continue For
                End If
                '要望番号419 2012.02.02 追加END

            End If

            'NRS商品キー
            dtM.Rows(i)("NRS_GOODS_CD") = goodsDt.Rows(0)("GOODS_CD_NRS")

            '商品名
            dtM.Rows(i)("GOODS_NM") = goodsDt.Rows(0)("GOODS_NM_1")

            '個数単位
            dtM.Rows(i)("NB_UT") = goodsDt.Rows(0)("NB_UT")

            '包装個数(入数)
            dtM.Rows(i)("PKG_NB") = goodsDt.Rows(0)("PKG_NB")

            '包装単位
            dtM.Rows(i)("PKG_UT") = goodsDt.Rows(0)("PKG_UT")

            '入荷包装個数
            dtM.Rows(i)("INKA_PKG_NB") = Math.Floor(Convert.ToDecimal(dtM.Rows(i)("NB")) / Convert.ToDecimal(dtM.Rows(i)("PKG_NB")))

            '端数
            dtM.Rows(i)("HASU") = Convert.ToDecimal(dtM.Rows(i)("NB")) Mod Convert.ToDecimal(dtM.Rows(i)("PKG_NB"))

            '標準入目
            If Convert.ToDecimal(dtM.Rows(i)("STD_IRIME")) = 0 Then
                dtM.Rows(i)("STD_IRIME") = goodsDt.Rows(0)("STD_IRIME_NB")
            End If

            '標準入目単位
            If String.IsNullOrEmpty(dtM.Rows(i)("STD_IRIME_UT").ToString()) = True Then
                dtM.Rows(i)("STD_IRIME_UT") = goodsDt.Rows(0)("STD_IRIME_UT")
            End If

            '入目
            If Convert.ToDecimal(dtM.Rows(i)("IRIME")) = 0 Then
                dtM.Rows(i)("IRIME") = goodsDt.Rows(0)("STD_IRIME_NB")
            End If

            '入目単位
            If String.IsNullOrEmpty(dtM.Rows(i)("IRIME_UT").ToString()) = True Then
                dtM.Rows(i)("IRIME_UT") = goodsDt.Rows(0)("STD_IRIME_UT")
            End If

            '個別重量
            If Convert.ToDecimal(dtM.Rows(i)("BETU_WT")) = 0 Then
                dtM.Rows(i)("BETU_WT") = goodsDt.Rows(0)("STD_WT_KGS")
            End If

            '容積
            If Convert.ToDecimal(dtM.Rows(i)("CBM")) = 0 Then
                dtM.Rows(i)("CBM") = goodsDt.Rows(0)("STD_CBM")
            End If

            '温度区分
            If String.IsNullOrEmpty(dtM.Rows(i)("ONDO_KB").ToString()) = True Then
                dtM.Rows(i)("ONDO_KB") = goodsDt.Rows(0)("ONDO_KB")
            End If

            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_1") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_1")
            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_2") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_2")
            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_3") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_3")
            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_4") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_4")
            dtM.Rows(i)("INKA_KAKO_SAGYO_KB_5") = goodsDt.Rows(0)("INKA_KAKO_SAGYO_KB_5")

            dtM.Rows(i)("STD_WT_KGS") = goodsDt.Rows(0)("STD_WT_KGS")
            dtM.Rows(i)("STD_IRIME_NB") = goodsDt.Rows(0)("STD_IRIME_NB")
            dtM.Rows(i)("TARE_YN") = goodsDt.Rows(0)("TARE_YN")

            '要望番号1003 2012.05.08 追加START
            '商品明細マスタより置場情報を取得(サブ区分="02")セット内容)

            dtM.Rows(i)("TOU_NO") = String.Empty
            dtM.Rows(i)("SITU_NO") = String.Empty
            dtM.Rows(i)("ZONE_CD") = String.Empty
            If String.IsNullOrEmpty(dtM.Rows(i)("NRS_GOODS_CD").ToString()) = False Then
                '商品明細マスタの取得
                setDs = (MyBase.CallDAC(Me._MstDac, "SelectDataGoodsMeisaiOkiba", setDs))
                If MyBase.GetResultCount <> 0 Then
                    Dim setOkiba As String = setDs.Tables("LMH010_M_GOODS_DETAILS").Rows(0)("SET_NAIYO").ToString()
                    '置場情報が5または6Byte取得できた時のみ置場情報をセット
                    If setOkiba.Length = 6 OrElse setOkiba.Length = 5 Then
                        dtM.Rows(i)("TOU_NO") = setOkiba.Substring(0, 2)
                        dtM.Rows(i)("SITU_NO") = setOkiba.Substring(2, 2)
                        If setOkiba.Length = 6 Then
                            dtM.Rows(i)("ZONE_CD") = setOkiba.Substring(4, 2)
                        ElseIf setOkiba.Length = 5 Then
                            dtM.Rows(i)("ZONE_CD") = setOkiba.Substring(4, 1)
                        End If
                    End If
                End If
            End If
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
        'inkaDr("INKA_TTL_NB") = ediDr("INKA_TTL_NB")

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
            inkaDr("LOT_NO") = ediDr("LOT_NO")
            '要望番号1003 2012.05.08 追加START(商品明細マスタより取得)
            inkaDr("TOU_NO") = ediDr("TOU_NO")
            inkaDr("SITU_NO") = ediDr("SITU_NO")
            inkaDr("ZONE_CD") = ediDr("ZONE_CD")
            '要望番号1003 2012.05.08 追加END
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

#Region "データセット設定(受信ヘッダ)"
    ''' <summary>
    ''' データセット設定(受信ヘッダ)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetRcvHed(ByVal ds As DataSet) As DataSet

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

        'データセットに設定
        ds.Tables("LMH010_RCV_HED").Rows.Add(rcvDr)

        Return ds

    End Function
#End Region

#Region "データセット設定(受信明細)"
    ''' <summary>
    ''' データセット設定(受信明細)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDatasetRcvDtl(ByVal ds As DataSet) As DataSet

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

                    '2012.03.08 要望番号859 START
                    'sagyoDr("SAGYO_COMP") = "0"
                    'sagyoDr("SKYU_CHK") = "0"
                    sagyoDr("SAGYO_COMP") = "00"
                    sagyoDr("SKYU_CHK") = "00"
                    '2012.03.08 要望番号859 END
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
        'unsoDr("DEST_CD") = ""                                   'マスタ値

        '運送梱包個数の計算
        Dim unsoPkgNb As Long = 0

        For i As Integer = 0 To dt.Rows.Count - 1

            unsoPkgNb = unsoPkgNb + Convert.ToInt64(dt.Rows(i).Item("INKA_PKG_NB"))
            If Convert.ToInt64(dt.Rows(i).Item("HASU")) > 0 Then
                unsoPkgNb = unsoPkgNb + 1
            End If

        Next

        unsoDr("UNSO_PKG_NB") = unsoPkgNb                               '集計値
        unsoDr("UNSO_WT") = ""
        unsoDr("UNSO_ONDO_KB") = ediDr("UNSO_ONDO_KB")
        unsoDr("TARIFF_BUNRUI_KB") = ediDr("UNCHIN_KB")
        unsoDr("VCLE_KB") = ediDr("SYARYO_KB")
        unsoDr("MOTO_DATA_KB") = "10"
        '▼▼▼要望番号:602
        unsoDr("TAX_KB") = "01"
        '▲▲▲要望番号:602
        unsoDr("REMARK") = ediDr("REMARK")
        unsoDr("SEIQ_TARIFF_CD") = ediDr("YOKO_TARIFF_CD")
        unsoDr("AD_3") = ""                                      'マスタ値
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
            unsoMDr("QT_UT") = ediDr("STD_IRIME_UT")                 'マスタ
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

        '運送重量(運送Mの集計値:小数点以下第1位を切り上げ)
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

            '▼▼▼
            '荷姿(区分マスタ)
            'drJudge = ds.Tables("LMH010_JUDGE").NewRow()
            'drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N001
            'drJudge("KBN_CD") = dt.Rows(i)("PKG_UT")
            'ds.Tables("LMH010_JUDGE").Rows.Add(drJudge)
            '▲▲▲

            '条件の設定
            setDt.ImportRow(dt.Rows(i))

            '▼▼▼
            '荷姿(区分マスタ)
            drJudge = setDs.Tables("LMH010_JUDGE").NewRow()
            drJudge("KBN_GROUP_CD") = LMKbnConst.KBN_N001
            drJudge("KBN_CD") = dt.Rows(i)("PKG_UT")
            setDs.Tables("LMH010_JUDGE").Rows.Add(drJudge)
            '▲▲▲

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

#Region "実績作成処理"
    ''' <summary>
    ''' 実績作成処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSakusei(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("EDI_CTL_NO").ToString()

        If Me.JissekiWarningSet(ds, LMH010BLC.NCGO_WID_L001) = False Then

            'オーダー番号重複確認（日合専用チェック）
            If Me.ChkOrderNo(ds, rowNo, ediCtlNo) = False Then
                Return ds
            End If

        End If

        'EDI入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI入荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)

        '送信データ作成用情報取得
        ds = MyBase.CallDAC(Me._Dac, "SelectSend", ds)

        '送信データ編集
        ds = Me.SetSendDs(ds)

        '送信データの更新
        ds = MyBase.CallDAC(Me._Dac, "InsertSend", ds)
        'DEL 2017/01/17
        '受信ヘッダの更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)
        'If MyBase.GetResultCount = 0 Then
        '    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
        '    Return ds
        'End If

        '受信明細の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        '入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateInkaL", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 送信データ編集
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSendDs(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("H_SENDINEDI_NCGO").Rows(0)
        Dim id As String = dr("ID").ToString()

        'ID
        Select Case id
            Case "B05"
                dr("ID") = "F33"
            Case "F44"
                dr("ID") = "F34"
            Case Else
                dr("ID") = "F32"
        End Select


        Dim strTemp As String = dr("UKETSUKE_NO_EDA").ToString().Trim()
        Dim upFlg As String = dr("RCV_EDA_UP_FLG").ToString().Trim()

        Dim strTempSimo As String = strTemp.Substring(strTemp.Length - 1, 1)
        Dim strTempKami As String = strTemp.Substring(0, 1)

        '受付№枝番
        Select Case id
            Case "B05", "F44"
                Select Case strTempSimo
                    Case "0" To "8", "A" To "Y", "ｱ" To "ﾜ"
                        strTempSimo = Chr(Asc(strTempSimo) + 1)         '.net要変換
                    Case "9"
                        strTempSimo = "A"
                    Case "Z"
                        strTempSimo = "ｱ"
                    Case Else

                End Select

                If upFlg = "1" Then
                    Select Case strTempSimo
                        Case "0" To "8", "A" To "Y", "ｱ" To "ﾜ"
                            strTempSimo = Chr(Asc(strTempSimo) + 1)
                        Case "9"
                            strTempSimo = "A"
                        Case "Z"
                            strTempSimo = "ｱ"
                        Case Else

                    End Select

                End If

                dr("UKETSUKE_NO_EDA") = String.Concat(strTempKami, strTempSimo)
            Case Else
                '処理なし
        End Select

        '入力年月日
        'dr("INPUT_YMD") = Date.Now.ToString("yyyyMMdd")
        dr("INPUT_YMD") = MyBase.GetSystemDate()

        '容量
        dr("YORYO") = Me.FormatSpace(dr("YORYO").ToString(), 8)

        Return ds
    End Function

    ''' <summary>
    ''' ワーニング処理判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="warningId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiWarningSet(ByVal ds As DataSet, ByVal warningId As String) As Boolean

        Dim dtWarning As DataTable = ds.Tables("WARNING_SHORI")
        Dim ediCtlNoL As String = ds.Tables("LMH010INOUT").Rows(0)("EDI_CTL_NO").ToString()
        Dim max As Integer = dtWarning.Rows.Count - 1
        Dim dr As DataRow

        'ワーニング処理設定されていなければ処理終了
        If max = -1 Then
            Return False
        End If

        For i As Integer = 0 To max

            dr = dtWarning.Rows(i)

            If warningId.Equals(dr("EDI_WARNING_ID").ToString()) AndAlso ediCtlNoL.Equals(dr("EDI_CTL_NO_L")) Then
                Return True
            End If

        Next

        Return False

    End Function

    ''' <summary>
    ''' オーダー番号重複確認
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkOrderNo(ByVal ds As DataSet, ByVal rowNo As String, ByVal ediCtlNo As String) As Boolean

        Dim msgArray(5) As String

        '受信HEDから受付№、受付枝№を取得
        ds = MyBase.CallDAC(Me._Dac, "SelectUketsukeNo", ds)
        'レコードが無い場合、エラー
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return False
        End If

        '同一オーダー番号（枝番含まず）での変更データの有無確認
        ds = MyBase.CallDAC(Me._Dac, "OrderChk", ds)
        If MyBase.GetResultCount <> 0 Then
            'レコードがある場合、ワーニング設定
            msgArray(1) = "同一オーダー番号での変更"
            msgArray(2) = "入荷"
            msgArray(3) = String.Empty
            msgArray(4) = String.Empty
            msgArray(5) = String.Empty
            ds = Me._ChkBlc.SetWarningL("W181", LMH010BLC.NCGO_WID_L001, ds, msgArray)
            Return False
        End If

        '同一オーダー番号での取消データの有無確認
        ds = MyBase.CallDAC(Me._Dac, "OrderChkTorikeshi", ds)
        If MyBase.GetResultCount <> 0 Then
            'レコードがある場合、ワーニング設定
            msgArray(1) = "同一オーダー番号での取消"
            msgArray(2) = "入荷"
            msgArray(3) = String.Empty
            msgArray(4) = String.Empty
            msgArray(5) = String.Empty
            ds = Me._ChkBlc.SetWarningL("W181", LMH010BLC.NCGO_WID_L001, ds, msgArray)
            Return False
        End If

        Return True

    End Function

#End Region

#Region "実績取消処理"
    ''' <summary>
    ''' 実績取消処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiTorikesi(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("EDI_CTL_NO").ToString()

        'EDI入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If
        'DEL 2017/01/17
        '受信ヘッダの更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)
        'If MyBase.GetResultCount = 0 Then
        '    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
        '    Return ds
        'End If

        '受信明細の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        Return ds

    End Function

#End Region

#Region "EDI取消"
    Private Function EdiTorikesi(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = ds.Tables("LMH010INOUT").Rows(0).Item("EDI_CTL_NO").ToString()

        'EDI入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
            Return ds
        End If

        'EDI入荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)
        '2017/01/17
        '受信ヘッダの更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)
        'If MyBase.GetResultCount = 0 Then
        '    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E011", , rowNo, LMH010BLC.EXCEL_COLTITLE, ediCtlNo)
        '    Return ds
        'End If

        '受信明細の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        Return ds
    End Function

#End Region

#Region " EDI取消⇒未登録, 報告用EDI取消"
    ''' <summary>
    ''' EDI取消⇒未登録
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>EDI入荷(中), 受信ヘッダ, 受信明細の削除フラグ変更</remarks>
    Private Function EdiOperation(ByVal ds As DataSet) As DataSet

        'EDI入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)

        'EDI入荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)
        'DEL 2017/01/17
        ''受信ヘッダの更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)

        '受信明細の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        Return ds

    End Function
#End Region

#Region "実績作成済⇒実績未, 実績送信済⇒実績未"
    ''' <summary>
    ''' 実績作成済⇒実績未, 実績送信済⇒実績未
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EdiOperation2(ByVal ds As DataSet) As DataSet

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)
        'DEL 2017/01/17
        ''EDI受信(HED)の更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        'EDI送信の更新
        ds = MyBase.CallDAC(Me._Dac, "DeleteSend", ds)

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

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)
        'DEL 2017/01/17
        ''EDI受信(HED)の更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        'EDI送信の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateSend", ds)

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

        'EDI出荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)

        'EDI出荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)
        'DEL 2017/01/17
        ''EDI受信(HED)の更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)

        'EDI受信(DTL)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateRcvDtl", ds)

        Return ds

    End Function

#End Region

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

        '受信HEDデータセット
        ds = Me.SetDatasetRcvHed(ds)

        '受信DTLデータセット
        ds = Me.SetDatasetRcvDtl(ds)

        'EDI入荷(大)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiL", ds)

        'EDI入荷(中)の更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateEdiM", ds)
        'DEL 2017/01/17
        ''受信ヘッダの更新
        'ds = MyBase.CallDAC(Me._Dac, "UpdateRcvHed", ds)

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

#Region "セミEDI処理"

#Region "画面取込(セミEDI)チェック処理"

#Region "カラム項目の値・日付チェック"

    ''' <summary>
    ''' 値・日付チェック
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TorikomiValChk(ByVal dr As DataRow) As Boolean

        Dim sFileNm As String = dr.Item("FILE_NAME_RCV").ToString()
        Dim sRecNo As String = dr.Item("REC_NO").ToString()
        Dim bRet As Boolean = True

        Dim sMsg As String = String.Empty
        Dim sStr As String = String.Empty
        Dim sNum As String = String.Empty
        Dim dNum As Double = 0

        Dim dataId As String = dr.Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.DATA_ID)).ToString()

        sMsg = "受注ヘッダ訂正No.(カラム" & CType(LMH010DAC601.MclcArrivalColumns.JYUCHU_HED_TEISEI_NO, Int32) & "番目)["
        sNum = dr.Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.JYUCHU_HED_TEISEI_NO)).ToString()
        If String.IsNullOrEmpty(sNum) = True Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E001", New String() {sMsg.Substring(0, sMsg.Length - 1)}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        Else
            If IsConvertDbl(sNum).Equals(False) Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E005", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            Else
                If sNum.IndexOf(".") >= 0 AndAlso Convert.ToDecimal(sNum.Substring(sNum.IndexOf(".") + 1)) > 0 Then
                    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E025", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                End If
            End If
        End If

        sMsg = "赤黒区分(カラム" & CType(LMH010DAC601.MclcArrivalColumns.AKA_KURO_KBN, Int32) & "番目)["
        sStr = dr.Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.AKA_KURO_KBN)).ToString()
        If Not (sStr = "1" OrElse sStr = "2") Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, sStr, "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "細目区分(カラム" & CType(LMH010DAC601.MclcArrivalColumns.DETAIL_KBN, Int32) & "番目)["
        sStr = dr.Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.DETAIL_KBN)).ToString()
        If dataId = "201" Then
            If sStr <> "H" Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E433", New String() {String.Concat(sMsg, sStr, "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        Else
            If String.IsNullOrEmpty(sStr) = True Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E001", New String() {sMsg.Substring(0, sMsg.Length - 1)}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If

        sMsg = "荷主オーダーNo.(カラム" & CType(LMH010DAC601.MclcArrivalColumns.CUST_ORDER_NO, Int32) & "番目)["
        sStr = dr.Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.CUST_ORDER_NO)).ToString()
        If LenB(sStr) <> 20 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "文字項目（短）０３ (MCC元項目: 荷主伝票明細№)(カラム" & CType(LMH010DAC601.MclcArrivalColumns.STR_ITEM_SHORT_03, Int32) & "番目)["
        sStr = dr.Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.STR_ITEM_SHORT_03)).ToString()
        If LenB(sStr) > 6 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "文字項目（短）０５ (MCC元項目: 荷主依頼明細№)(カラム" & CType(LMH010DAC601.MclcArrivalColumns.STR_ITEM_SHORT_05, Int32) & "番目)["
        sStr = dr.Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.STR_ITEM_SHORT_05)).ToString()
        If dataId = "201" Then
            If LenB(sStr) > 8 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        Else
            If LenB(sStr) > 5 Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            End If
        End If

        sMsg = "指定作業日(カラム" & CType(LMH010DAC601.MclcArrivalColumns.SHITEI_WORK_DATE, Int32) & "番目)["
        sStr = dr.Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.SHITEI_WORK_DATE)).ToString()
        If IsDate(Jp.Co.Nrs.Win.Utility.DateFormatUtility.EditSlash(sStr)) = False Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E445", New String() {String.Concat(sMsg, sStr, "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "品名コード(カラム" & CType(LMH010DAC601.MclcArrivalColumns.ITEM_CD, Int32) & "番目)["
        sStr = dr.Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.ITEM_CD)).ToString()
        If LenB(sStr) > 30 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "品名愛称(カラム" & CType(LMH010DAC601.MclcArrivalColumns.ITEM_AISYO, Int32) & "番目)["
        sStr = dr.Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.ITEM_AISYO)).ToString()
        If LenB(sStr) > 40 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "ロットNo.(カラム" & CType(LMH010DAC601.MclcArrivalColumns.LOT_NO, Int32) & "番目)["
        sStr = dr.Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.LOT_NO)).ToString()
        If LenB(sStr) > 10 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "個数(カラム" & CType(LMH010DAC601.MclcArrivalColumns.KOSU, Int32) & "番目)["
        sNum = dr.Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.KOSU)).ToString().Trim()
        If String.IsNullOrEmpty(sNum) = True Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E001", New String() {sMsg.Substring(0, sMsg.Length - 1)}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        Else
            If IsConvertDbl(sNum).Equals(False) Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E005", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            Else
                dNum = Convert.ToDouble(sNum)
                dNum = System.Math.Abs(dNum)
                If Convert.ToDouble(sNum) = 0 Then
                    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E001", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                    'ElseIf Convert.ToDouble(sNum) < 0 Then
                    '    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E185", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    '    bRet = False
                ElseIf dNum > 9999999999 OrElse dNum < -9999999999 Then
                    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sNum, "]"), ""}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                Else
                    If sNum.IndexOf(".") >= 0 AndAlso Convert.ToDecimal(sNum.Substring(sNum.IndexOf(".") + 1)) > 0 Then
                        MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E025", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    End If
                End If
            End If
        End If

        sMsg = "数量(カラム" & CType(LMH010DAC601.MclcArrivalColumns.SUURYO, Int32) & "番目)["
        sNum = dr.Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.SUURYO)).ToString().Trim()
        If String.IsNullOrEmpty(sNum) = True Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E001", New String() {sMsg.Substring(0, sMsg.Length - 1)}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        Else
            If IsConvertDbl(sNum).Equals(False) Then
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E005", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                bRet = False
            Else
                dNum = Convert.ToDouble(sNum)
                dNum = System.Math.Abs(dNum)
                If Convert.ToDouble(sNum) = 0 Then
                    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E001", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                    'ElseIf Convert.ToDouble(sNum) < 0 Then
                    '    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E185", New String() {String.Concat(sMsg, sNum, "]")}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    '    bRet = False
                ElseIf dNum > 999999999.999 OrElse dNum < -999999999.999 Then
                    MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sNum, "]"), ""}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                    bRet = False
                Else
                    If sNum.IndexOf(".") >= 0 AndAlso Convert.ToDecimal(sNum.Substring(sNum.IndexOf(".") + 1)) > 999D Then
                        MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sNum, "]"), ""}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
                        bRet = False
                    End If
                End If
            End If
        End If

        sMsg = "先方SPコード(カラム" & CType(LMH010DAC601.MclcArrivalColumns.SENPOU_SP_CD, Int32) & "番目)["
        sStr = dr.Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.SENPOU_SP_CD)).ToString()
        If LenB(sStr) > 4 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "文字項目（短）１５ (MCC元項目: データ作成日)(カラム" & CType(LMH010DAC601.MclcArrivalColumns.STR_ITEM_SHORT_15, Int32) & "番目)["
        sStr = dr.Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.STR_ITEM_SHORT_15)).ToString()
        If LenB(sStr) > 8 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        sMsg = "文字項目（短）１６ (MCC元項目: データ作成時刻)(カラム" & CType(LMH010DAC601.MclcArrivalColumns.STR_ITEM_SHORT_16, Int32) & "番目)["
        sStr = dr.Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.STR_ITEM_SHORT_16)).ToString()
        If LenB(sStr) > 6 Then
            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E518", New String() {String.Concat(sMsg, sStr, "]"), ""}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
        End If

        '戻り値設定
        Return bRet

    End Function

    ''' <summary>
    ''' 文字列が数値（Double型）に変換出来るかチェックする
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <returns>True=変換できる　False=変換できない</returns>
    ''' <remarks></remarks>
    Private Function IsConvertDbl(ByVal targetString As String) As Boolean
        Dim d As Double
        Return Double.TryParse(targetString, d)
    End Function

    ''' <summary>
    ''' 文字列長（Shift_JIS 換算のバイト数）を求める
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <returns>対象文字列のバイト数</returns>
    ''' <remarks></remarks>
    Private Function LenB(ByVal targetString As String) As Integer
        Return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(targetString)
    End Function

#End Region ' "カラム項目の値・日付チェック"

#Region "商品マスタ存在チェック"

    ''' <summary>
    ''' マスタ整合性チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function TorikomiMstChk(ByVal dr As DataRow, ByVal ds As DataSet) As Boolean

        Dim sFileNm As String = dr.Item("FILE_NAME_RCV").ToString()
        Dim sRecNo As String = dr.Item("REC_NO").ToString()
        Dim bRet As Boolean = True

        Dim sGoodsCd As String = String.Empty

        sGoodsCd = dr.Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.ITEM_RYAKUGO)).ToString() '品目コード
        If MyBase.GetResultCount() = 0 Then

            MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E493", New String() {"商品コード", "商品マスタ", String.Concat("商品コード(品名略号)：", sGoodsCd)}, sRecNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)
            bRet = False
            Return bRet
        End If

        '戻り値設定
        Return bRet

    End Function

#End Region ' "商品マスタ存在チェック"

    ''' <summary>
    ''' 画面取込(セミEDI)チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SemiEdiTorikomiChk(ByVal ds As DataSet) As DataSet

        Dim dtSemiInfo As DataTable = ds.Tables("LMH010_SEMIEDI_INFO")
        Dim dtSemiHed As DataTable = ds.Tables("LMH010_EDI_TORIKOMI_HED")
        Dim dtSemiDtl As DataTable = ds.Tables("LMH010_EDI_TORIKOMI_DTL")

        Dim dr As DataRow
        Dim hedDr As DataRow = dtSemiHed.Rows(0)

        Dim max As Integer = dtSemiDtl.Rows.Count - 1
        Dim hedmax As Integer = dtSemiHed.Rows.Count - 1

        Dim iRowCnt As Integer = 0

        For i As Integer = 0 To hedmax

            If dtSemiHed.Rows(i).Item("ERR_FLG").ToString.Equals("1") Then
                '最初からエラーフラグが立っている場合（明細件数０件の場合）
                Dim sFileNm As String = dtSemiHed.Rows(i).Item("FILE_NAME_RCV").ToString()
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E460", , , LMH010BLC.EXCEL_COLTITLE_SEMIEDI, sFileNm)

            Else

                '対象データのみソートして抜き出す
                Dim strSort As String = String.Empty
                Dim filter As String
                filter = String.Concat(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.IRAI_SYUBETSU_CD), " = 'NK'")
                Dim drSelect As DataRow() = dtSemiDtl.Select(filter.ToString(), strSort)
                If drSelect.Count = 0 Then
                    '抜き出したデータRowが０件の場合
                    dtSemiHed.Rows(i).Item("ERR_FLG") = "1" '０件エラーフラグを立てる

                Else

                    'SelectしたデータをdtSemiDtlに再セットする
                    Dim dtSelect As DataTable = dtSemiDtl.Clone          'Select前テーブルの情報をクローン化
                    For Each row As DataRow In drSelect
                        dtSelect.ImportRow(row)         'SelectしたデータRowをクローンにセットする
                    Next

                    'dtSemiDtlに再セット（以降の処理はdtSemiDtlで処理されるため）
                    dtSemiDtl.Clear()
                    For k As Integer = 0 To dtSelect.Rows.Count - 1
                        dtSemiDtl.ImportRow(dtSelect.Rows(k))
                    Next

                End If

                max = dtSemiDtl.Rows.Count - 1

                For j As Integer = iRowCnt To max

                    dr = dtSemiDtl.Rows(j)
                    If (dr.Item("FILE_NAME_RCV").ToString().Trim()).Equals(dtSemiHed.Rows(i).Item("FILE_NAME_RCV").ToString().Trim()) = True Then
                        'ヘッダと明細のファイル名称が等しい場合
                        '入力チェック(数値,日付チェック)
                        If Me.TorikomiValChk(dr) = False Then
                            '異常の場合
                            '詳細のエラーフラグに"1"をセットする
                            dr.Item("ERR_FLG") = "1"
                            'ヘッダのエラーフラグに"1"をセットする
                            dtSemiHed.Rows(i).Item("ERR_FLG") = "1"
                        Else
                            '正常の場合は処理無し（未処理（:9）の状態を保持するため）
                        End If

                        '別インスタンス
                        Dim setDs As DataSet = ds.Copy()
                        Dim setDt As DataTable = setDs.Tables("LMH010_EDI_TORIKOMI_DTL")
                        Dim setSemiDt As DataTable = setDs.Tables("LMH010_SEMIEDI_INFO")

                        '値のクリア
                        setDs.Clear()

                        '条件の設定
                        setDt.ImportRow(dtSemiDtl.Rows(j))
                        setSemiDt.ImportRow(dtSemiInfo.Rows(0))

                        setDs = MyBase.CallDAC(Me._Dac, "CheckGoods", setDs)

                        '先方データと商品マスタチェック
                        If Me.TorikomiMstChk(dr, setDs) = False Then

                            '異常の場合
                            '詳細のエラーフラグに"1"をセットする
                            dr.Item("ERR_FLG") = "1"

                            'ヘッダのエラーフラグに"1"をセットする
                            dtSemiHed.Rows(i).Item("ERR_FLG") = "1"

                        End If

                    Else
                        'ヘッダと明細のファイル名称が等しくない場合
                        '現在行を保持してループを抜ける()
                        iRowCnt = j
                        Exit For
                    End If
                Next

            End If
        Next


        Return ds

    End Function

#End Region ' "画面取込(セミEDI)チェック処理"

#Region "画面取込(セミEDI)データセット設定"

#Region "セミEDI時　データセット設定(EDI受信DTL)"

    ''' <summary>
    ''' データセット設定(EDI受信HED・DTL)：セミEDI
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="i"></param>
    ''' <param name="dataId"></param>
    ''' <param name="detailKbn"></param>
    ''' <returns></returns>
    Private Function SetSemiInkaEdiRcv(ByVal ds As DataSet, ByVal i As Integer, ByVal dataId As String, ByVal detailKbn As String) As DataSet

        Dim drSemiEdiInfo As DataRow = ds.Tables("LMH010_SEMIEDI_INFO").Rows(0)
        Dim drSetDtl As DataRow = ds.Tables("LMH010_EDI_TORIKOMI_DTL").Rows(i)


        Dim drEdiRcvDtl As DataRow = ds.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").NewRow()

        If drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.AKA_KURO_KBN)).ToString().Equals("2") Then
            '赤
            drEdiRcvDtl.Item("DEL_KB") = "3"
        Else
            drEdiRcvDtl.Item("DEL_KB") = "0"
        End If
        drEdiRcvDtl.Item("CRT_DATE") = MyBase.GetSystemDate()
        drEdiRcvDtl.Item("FILE_NAME") = drSetDtl("FILE_NAME_RCV")
        drEdiRcvDtl.Item("REC_NO") = "00000"                                                                ' ソート後に設定する
        drEdiRcvDtl.Item("GYO") = (i + 1).ToString().PadLeft(5, "0"c)
        drEdiRcvDtl.Item("NRS_BR_CD") = drSemiEdiInfo("NRS_BR_CD")
        drEdiRcvDtl.Item("EDI_CTL_NO") = String.Empty                                                       ' ソート後に設定する
        drEdiRcvDtl.Item("EDI_CTL_NO_CHU") = String.Empty                                                   ' ソート後に設定する
        drEdiRcvDtl.Item("INKA_CTL_NO_L") = String.Concat(drSemiEdiInfo("BR_INITIAL"), "00000000")
        drEdiRcvDtl.Item("INKA_CTL_NO_M") = "000"
        drEdiRcvDtl.Item("CUST_CD_L") = drSemiEdiInfo.Item("CUST_CD_L")
        drEdiRcvDtl.Item("CUST_CD_M") = drSemiEdiInfo.Item("CUST_CD_M")

        ' 荷主固有データ
        '----------------
        ' データIDエリア
        drEdiRcvDtl.Item("DATA_ID_AREA") = Me._Blc.LeftB(drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.DATA_ID)).ToString().Trim(), 3)
        ' データID細目区分
        drEdiRcvDtl.Item("DATA_ID_DETAIL") = Me._Blc.LeftB(drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.DETAIL_KBN)).ToString().Trim(), 1)
        If drSetDtl.Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.JYUCHU_HED_TEISEI_NO)).ToString().Equals("1") AndAlso
            drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.AKA_KURO_KBN)).ToString().Equals("1") Then
            ' 受注ヘッダ訂正No. = 1 かつ赤黒区分“黒”の場合
            ' 訂正区分“新規”
            drEdiRcvDtl.Item("INPUT_KBN") = "1"
        Else
            ' 訂正区分“訂正”
            drEdiRcvDtl.Item("INPUT_KBN") = "2"
        End If
        If drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.AKA_KURO_KBN)).ToString().Equals("1") Then
            ' 赤伝区分“黒”
            drEdiRcvDtl.Item("AKADEN_KBN") = ""
        Else
            ' 赤伝区分“赤”
            drEdiRcvDtl.Item("AKADEN_KBN") = "1"
        End If
        ' データ作成日
        drEdiRcvDtl.Item("DATA_CRE_DATE") = Me._Blc.LeftB(drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.STR_ITEM_SHORT_15)).ToString(), 8)
        ' データ作成時刻
        drEdiRcvDtl.Item("DATA_CRE_TIME") = Me._Blc.LeftB(drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.STR_ITEM_SHORT_16)).ToString(), 6)
        Dim custOrderNo As String = drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.CUST_ORDER_NO)).ToString()
        If dataId = "201" AndAlso detailKbn = "H" Then
            ' データID が“出荷依頼”(返品) かつ細目区分が“返品”(受注返品) の場合

            ' 発注伝票NO.
            drEdiRcvDtl.Item("HACCHU_DENP_NO") = New String("0"c, 10)
            ' 発注伝票明細NO.
            drEdiRcvDtl.Item("HACCHU_DENP_DTL_NO") = New String("0"c, 5)
        Else
            ' 発注伝票NO.
            drEdiRcvDtl.Item("HACCHU_DENP_NO") = custOrderNo.Substring(0, 10)
            ' 発注伝票明細NO.
            drEdiRcvDtl.Item("HACCHU_DENP_DTL_NO") = Me._Blc.LeftB(drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.STR_ITEM_SHORT_05)).ToString(), 5)
        End If
        ' 出荷伝票No.
        drEdiRcvDtl.Item("OUTKA_DENP_NO") = custOrderNo.Substring(10, 10)
        ' 出荷伝票明細No.
        drEdiRcvDtl.Item("OUTKA_DENP_DTL_NO") = Me._Blc.LeftB(drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.STR_ITEM_SHORT_03)).ToString(), 6)
        If dataId = "201" AndAlso detailKbn = "H" Then
            ' データID が“出荷依頼”(返品) かつ細目区分が“返品”(受注返品) の場合

            ' 入出庫伝票NO.
            drEdiRcvDtl.Item("IO_DENP_NO") = custOrderNo.Substring(0, 10)
            ' 入出庫伝票明細NO.
            drEdiRcvDtl.Item("IO_DENP_DTL_NO") = Me._Blc.LeftB(drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.STR_ITEM_SHORT_05)).ToString(), 8)
        Else
            ' 入出庫伝票NO.
            drEdiRcvDtl.Item("IO_DENP_NO") = New String("0"c, 10)
            ' 入出庫伝票明細NO.
            drEdiRcvDtl.Item("IO_DENP_DTL_NO") = New String("0"c, 8)
        End If
        ' 整理年月日
        drEdiRcvDtl("SEIRI_DATE") = Jp.Co.Nrs.Com.Utility.DateFormatUtility.DeleteSlash(
            Me._Blc.LeftB(drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.SHITEI_WORK_DATE)).ToString().Trim(), 10))
        ' 品目コード
        drEdiRcvDtl("ITEM_CD") = Me._Blc.LeftB(drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.ITEM_CD)).ToString(), 30)
        ' 品目略号
        drEdiRcvDtl("ITEM_RYAKUGO") = Me._Blc.LeftB(drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.ITEM_RYAKUGO)).ToString(), 20)
        ' 品目愛称
        drEdiRcvDtl("ITEM_AISYO") = Me._Blc.LeftB(drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.ITEM_AISYO)).ToString(), 40)
        ' 製造ロット
        drEdiRcvDtl("SEIZO_LOT") = Me._Blc.LeftB(drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.LOT_NO)).ToString(), 10)
        ' 個数
        drEdiRcvDtl("KOSU") = Me._Blc.LeftB(drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.KOSU)).ToString(), 16)
        If drEdiRcvDtl("KOSU").ToString().Substring(0, 1) = "-" Then
            drEdiRcvDtl("KOSU") = drEdiRcvDtl("KOSU").ToString().Substring(1)
            drEdiRcvDtl("KOSU_FUGO") = "-"
        Else
            drEdiRcvDtl("KOSU_FUGO") = "+"
        End If
        ' 数量
        drEdiRcvDtl("SUURYO") = Me._Blc.LeftB(drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.SUURYO)).ToString(), 16)
        If drEdiRcvDtl("SUURYO").ToString().Substring(0, 1) = "-" Then
            drEdiRcvDtl("SUURYO") = drEdiRcvDtl("SUURYO").ToString().Substring(1)
            drEdiRcvDtl("SUURYO_FUGO") = "-"
        Else
            drEdiRcvDtl("SUURYO_FUGO") = "+"
        End If
        ' 入荷保管場所
        drEdiRcvDtl("INKA_HOKAN_BASYO") = Me._Blc.LeftB(drSetDtl(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.SENPOU_SP_CD)).ToString(), 4)
        ' 連番
        drEdiRcvDtl("RENBAN") = ""                                              ' 設定値未定(廃止の可能性あり)

        drEdiRcvDtl("JISSEKI_SHORI_FLG") = "1"                                  ' 実績処理フラグ

        drEdiRcvDtl("SYS_ENT_DATE") = MyBase.GetSystemDate()
        drEdiRcvDtl("SYS_ENT_TIME") = MyBase.GetSystemTime()
        drEdiRcvDtl("SYS_ENT_PGID") = MyBase.GetPGID()
        drEdiRcvDtl("SYS_ENT_USER") = MyBase.GetUserID()
        drEdiRcvDtl("SYS_UPD_DATE") = MyBase.GetSystemDate()
        drEdiRcvDtl("SYS_UPD_TIME") = MyBase.GetSystemTime()
        drEdiRcvDtl("SYS_UPD_PGID") = MyBase.GetPGID()
        drEdiRcvDtl("SYS_UPD_USER") = MyBase.GetUserID()
        drEdiRcvDtl("SYS_DEL_FLG") = "0"

        'データセットに設定
        ds.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Rows.Add(drEdiRcvDtl)

        Return ds

    End Function

#End Region ' "セミEDI時　データセット設定(EDI受信DTL)"

#End Region ' "画面取込(セミEDI)データセット設定"

#Region "画面取込(セミEDI)データセット＋更新処理"

    ''' <summary>
    ''' 画面取込(セミEDI)データセット＋更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SemiEdiTorikomi(ByVal ds As DataSet) As DataSet

        Dim dtSetHed As DataTable = ds.Tables("LMH010_EDI_TORIKOMI_HED")        '取込Hed
        Dim dtSetDtl As DataTable = ds.Tables("LMH010_EDI_TORIKOMI_DTL")        '取込Dtl
        Dim dtSetRet As DataTable = ds.Tables("LMH010_EDI_TORIKOMI_RET")        '処理件数

        Dim dtEdiRcvDtl As DataTable = ds.Tables("LMH010_INKAEDI_DTL_NCGO_NEW") 'EDI受信Dtl
        dtEdiRcvDtl.Clear()
        Dim drEdiRcvDtl As DataRow = Nothing

        Dim iSetDtlMax As Integer = dtSetDtl.Rows.Count - 1

        Dim iRcvHedInsCnt As Integer = 0            '書込件数（受信HED）
        Dim iRcvDtlInsCnt As Integer = 0            '書込件数（受信DTL）
        Dim iInHedInsCnt As Integer = 0             '書込件数（入荷EDI(大)）
        Dim iInDtlInsCnt As Integer = 0             '書込件数（入荷EDI(中)）
        Dim iRcvHedCanCnt As Integer = 0            '取消件数（受信HED）
        Dim iRcvDtlCanCnt As Integer = 0            '取消件数（受信Dtl）
        Dim iInHedCanCnt As Integer = 0             '取消件数（入荷EDI(大)）
        Dim iInDtlCanCnt As Integer = 0             '取消件数（入荷EDI(中)）

        Dim bNoErr As Boolean = True                'エラー無しフラグ（True：エラー無し、False：エラー有り）

        Dim num As New NumberMasterUtility

        Dim sInkaEDINoL As String = ""
        Dim iInkaEDINoM As Integer = 1
        Dim iRecNo As Integer = 0

        Dim sHACCHU_DENP_NO As String = ""
        Dim sHACCHU_DENP_DTL_NO As String = ""
        Dim sOUTKA_DENP_NO As String = ""
        Dim sOUTKA_DENP_DTL_NO As String = ""
        Dim sRENBAN As String = ""
        Dim sINPUT_KBN As String = ""
        Dim sAKADEN_KBN As String = ""
        Dim sChekKey As String = ""

        Dim recNoArr(dtSetDtl.Rows.Count) As String

        For i As Integer = 0 To iSetDtlMax
            recNoArr(i) = ds.Tables("LMH010_EDI_TORIKOMI_DTL").Rows(i).Item("REC_NO").ToString()

            Dim iraiSyubetsuCd As String = ds.Tables("LMH010_EDI_TORIKOMI_DTL").Rows(i).Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.IRAI_SYUBETSU_CD)).ToString()
            If Not (iraiSyubetsuCd = "NK") Then
                ' 依頼種別コードが“入荷”でない行は
                ' 入荷の対象外につきEDI受信データ設定対象外
                Continue For
            End If

            '---------------------------------------------------------------------------
            ' セミEDI取込(共通)⇒EDI受信データセット
            '---------------------------------------------------------------------------
            Dim dataId As String = ds.Tables("LMH010_EDI_TORIKOMI_DTL").Rows(i).Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.DATA_ID)).ToString()
            Dim detailKbn As String = ds.Tables("LMH010_EDI_TORIKOMI_DTL").Rows(i).Item(LMH010DAC601.GetColumnName(LMH010DAC601.MclcArrivalColumns.DETAIL_KBN)).ToString()
            ds = Me.SetSemiInkaEdiRcv(ds, i, dataId, detailKbn)
        Next

        ' EDI受信データセットの並べ替え
        Dim tmpDt As DataTable = dtEdiRcvDtl.Clone()
        Dim tmpDr1() As DataRow = Nothing
        tmpDr1 = dtEdiRcvDtl.Select("DATA_ID_DETAIL =  'K'", "HACCHU_DENP_NO, HACCHU_DENP_DTL_NO, RENBAN, DATA_CRE_DATE, DATA_CRE_TIME")
        Dim tmpDr2() As DataRow = Nothing
        tmpDr2 = dtEdiRcvDtl.Select("DATA_ID_DETAIL <> 'K'", "OUTKA_DENP_NO, OUTKA_DENP_DTL_NO, DATA_CRE_DATE, DATA_CRE_TIME")
        For Each row As DataRow In tmpDr1
            tmpDt.ImportRow(row)
        Next
        For Each row As DataRow In tmpDr2
            tmpDt.ImportRow(row)
        Next
        Call dtEdiRcvDtl.Clear()
        Call dtEdiRcvDtl.Merge(tmpDt)

        ' EDI_CTL_NO, EDI_CTL_NO_CHU 設定

        For i = 0 To dtEdiRcvDtl.Rows.Count - 1
            drEdiRcvDtl = dtEdiRcvDtl.Rows(i)

            ' 発注伝票No.
            sHACCHU_DENP_NO = drEdiRcvDtl.Item("HACCHU_DENP_NO").ToString().Trim()
            ' 発注伝票明細No.
            sHACCHU_DENP_DTL_NO = drEdiRcvDtl.Item("HACCHU_DENP_DTL_NO").ToString().Trim()
            ' 連番
            sRENBAN = drEdiRcvDtl.Item("RENBAN").ToString().Trim()

            ' 出荷伝票No.
            sOUTKA_DENP_NO = drEdiRcvDtl.Item("OUTKA_DENP_NO").ToString().Trim()
            ' 出荷伝票明細No.
            sOUTKA_DENP_DTL_NO = drEdiRcvDtl.Item("OUTKA_DENP_DTL_NO").ToString().Trim()

            ' インプット区分
            sINPUT_KBN = drEdiRcvDtl.Item("INPUT_KBN").ToString().Trim()
            ' 赤伝区分
            sAKADEN_KBN = drEdiRcvDtl.Item("AKADEN_KBN").ToString().Trim()

            ' 細目区分でブレイク条件変更(H_INKAEDI_L/Mにも条件で設定する)
            If drEdiRcvDtl.Item("DATA_ID_DETAIL").ToString().Trim().Equals("K") Then
                '購入入荷

                ' 発注伝票No. + 発注伝票明細No.+ 連番 + インプット区分 + 赤伝区分ブレイク
                If Not sChekKey.Equals(String.Concat(sHACCHU_DENP_NO.ToString().Trim(), sHACCHU_DENP_DTL_NO.ToString().Trim(), sRENBAN.ToString().Trim(), sINPUT_KBN.ToString().Trim(), sAKADEN_KBN.ToString().Trim())) Then
                    ' ブレイク時
                    ' 採番
                    sInkaEDINoL = num.GetAutoCode(NumberMasterUtility.NumberKbn.EDI_INKA_NO_L, Me, drEdiRcvDtl.Item("NRS_BR_CD").ToString())

                    iInkaEDINoM = 0

                    ' 発注伝票No. + 発注伝票明細No. + 連番 + インプット区分 + 赤伝区分ブレイク
                    sChekKey = String.Concat(sHACCHU_DENP_NO.ToString().Trim(), sHACCHU_DENP_DTL_NO.ToString().Trim(), sRENBAN.ToString().Trim(), sINPUT_KBN.ToString().Trim(), sAKADEN_KBN.ToString().Trim())
                End If
            Else
                ' 出荷伝票No. + インプット区分 + 赤伝区分ブレイク
                If Not sChekKey.Equals(String.Concat(sOUTKA_DENP_NO.ToString().Trim(), sOUTKA_DENP_DTL_NO.ToString().Trim(), sINPUT_KBN.ToString().Trim(), sAKADEN_KBN.ToString().Trim())) Then
                    ' ブレイク時
                    ' 採番
                    sInkaEDINoL = num.GetAutoCode(NumberMasterUtility.NumberKbn.EDI_INKA_NO_L, Me, drEdiRcvDtl.Item("NRS_BR_CD").ToString())

                    iInkaEDINoM = 0

                    ' 出荷伝票No. + 出荷伝票明細No. + インプット区分 + 赤伝区分ブレイク
                    sChekKey = String.Concat(sOUTKA_DENP_NO.ToString().Trim(), sOUTKA_DENP_DTL_NO.ToString().Trim(), sINPUT_KBN.ToString().Trim(), sAKADEN_KBN.ToString().Trim())
                End If

            End If

            iInkaEDINoM = iInkaEDINoM + 1

            ' 管理番号のセット
            drEdiRcvDtl.Item("EDI_CTL_NO") = sInkaEDINoL
            drEdiRcvDtl.Item("EDI_CTL_NO_CHU") = iInkaEDINoM.ToString().PadLeft(3, CChar("0"))

            ' ソート後の REC_NO の(再)設定
            iRecNo = iRecNo + 1
            drEdiRcvDtl.Item("REC_NO") = iRecNo.ToString().PadLeft(5, CChar("0"))
        Next

        Dim setDs As DataSet
        Dim setDs2 As DataSet

        ' EDI入荷データ件数および入荷データL 入荷管理番号L 等 取得(チェック)処理
        Dim isFileNameError As Boolean = False
        setDs = New Jp.Co.Nrs.LM.DSL.LMH010DS()
        For i = 0 To dtEdiRcvDtl.Rows.Count - 1
            setDs.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Clear()
            setDs.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").ImportRow(dtEdiRcvDtl.Rows(i))
            Dim recNo As String = recNoArr(Convert.ToInt32(dtEdiRcvDtl.Rows(i).Item("GYO").ToString()) - 1)
            Dim fileName As String = dtEdiRcvDtl.Rows(i).Item("FILE_NAME").ToString()

            setDs = MyBase.CallDAC(Me._Dac, "SelectInkaCntAndNoL", setDs)

            Dim ediRecCnt As Integer = MyBase.GetResultCount()
            Dim inkaNoL As String = setDs.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Rows(0).Item("INKA_CTL_NO_L").ToString()
            If ediRecCnt > 0 AndAlso (inkaNoL.Equals("") = False) Then
                ' 入荷EDI存在かつ入荷存在（「入荷管理番号L IS NULL → ""」でない）の場合
                ' 当該行はエラー
                MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E02I", New String() {inkaNoL},
                                   recNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, fileName)
                bNoErr = False
            End If

            Dim inkaEdiCnt As Integer = 0
            If isFileNameError = False Then
                ' 新規追加 EDI受信データ(DTL) と同一キーレコードの データID細目区分 と発注伝票No. と同明細No. および 入出庫伝票No. 取得
                ' データID細目区分 と発注伝票No. と同明細No. および 入出庫伝票No. …… EDI入荷データ件数 等 取得時の突合条件項目
                setDs2 = setDs.Copy()
                setDs2 = MyBase.CallDAC(Me._Dac, "SelectInkaEdiHacchuDenpNoAndDtlNoAndIoDenpNo", setDs2)
                inkaEdiCnt = MyBase.GetResultCount()
                If inkaEdiCnt > 0 Then
                    ' EDI受信データ(DTL) と同一キーレコード存在の場合
                    Dim dataIdDetailFile As String = setDs.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Rows(0).Item("DATA_ID_DETAIL").ToString()
                    Dim dataIdDetailDb As String = setDs2.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Rows(0).Item("DATA_ID_DETAIL").ToString()
                    If Not (dataIdDetailFile = dataIdDetailDb) Then
                        ' データID細目区分が不一致の場合(オーダー一致か否かの比較の前提条件から異なるため)
                        ' ファイル名はエラー
                        isFileNameError = True
                    Else
                        If dataIdDetailFile = "H" Then
                            ' データID細目区分が“返品”の場合
                            Dim IoDenpNoFile As String = setDs.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Rows(0).Item("IO_DENP_NO").ToString()
                            Dim IoDenpNoDb As String = setDs2.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Rows(0).Item("IO_DENP_NO").ToString()
                            If Not (IoDenpNoFile = IoDenpNoDb) Then
                                ' 入出庫伝票No. が不一致の場合
                                ' ファイル名はエラー
                                isFileNameError = True
                            End If
                        Else
                            ' データID細目区分が (“返品”以外) の場合
                            Dim hacchuDenpNoFile As String = setDs.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Rows(0).Item("HACCHU_DENP_NO").ToString()
                            Dim hacchuDenpNoDb As String = setDs2.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Rows(0).Item("HACCHU_DENP_NO").ToString()
                            Dim hacchuDenpDtlNoFile As String = setDs.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Rows(0).Item("HACCHU_DENP_DTL_NO").ToString()
                            Dim hacchuDenpDtlNoDb As String = setDs2.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Rows(0).Item("HACCHU_DENP_DTL_NO").ToString()
                            If Not (hacchuDenpNoFile = hacchuDenpNoDb AndAlso hacchuDenpDtlNoFile = hacchuDenpDtlNoDb) Then
                                ' 発注伝票No. と同明細No. 不一致の場合
                                ' ファイル名はエラー
                                isFileNameError = True
                            End If
                        End If
                    End If
                    If isFileNameError Then
                        MyBase.SetMessageStore(LMH010BLC.GUIDANCE_KBN, "E454",
                            New String() {"既に別のオーダーが登録されているファイル名", "処理", "ファイル名を変更して再度取込を行ってください。"},
                            recNo, LMH010BLC.EXCEL_COLTITLE_SEMIEDI, fileName)
                        bNoErr = False
                        ' ファイル名エラーメッセージは検出初回のみの出力とする
                        isFileNameError = True
                    End If
                End If
            End If

            If bNoErr Then
                ' EDI入荷データ件数 > 0 の場合、EDI入荷データの削除更新を行う
                If ediRecCnt > 0 Then

                    ' EDI入荷(大)の削除(論理削除)
                    setDs = MyBase.CallDAC(Me._Dac, "UpdateDelInkaEdiL", setDs)
                    iInHedCanCnt += MyBase.GetResultCount()

                    ' EDI入荷(中)の削除(論理削除)
                    setDs = MyBase.CallDAC(Me._Dac, "UpdateDelInkaEdiM", setDs)
                    iInDtlCanCnt += MyBase.GetResultCount()

                    ' EDI受信データ(DTL) の削除(論理削除)
                    ' 一度処理したファイル(サフィクスが付いて別ファイル名となる)の再取込みで、
                    ' 後続の物理削除処理の対象とならないことによる、中間テーブルのレコード消え残り回避処理
                    setDs = MyBase.CallDAC(Me._Dac, "UpdateDelInkaEdiDtl", setDs)
                    iRcvDtlCanCnt += MyBase.GetResultCount()

                End If

                If inkaEdiCnt > 0 Then
                    ' 新規追加 EDI受信データ(DTL) と同一キーのレコード件数 > 0 の場合
                    ' 同一キーレコード存在時の物理削除
                    setDs = MyBase.CallDAC(Me._Dac, "DeleteInkaEdi", setDs)
                    iRcvDtlCanCnt = iRcvDtlCanCnt + 1
                End If
            End If
        Next

        If bNoErr Then

            setDs = New Jp.Co.Nrs.LM.DSL.LMH010DS()
            setDs.Tables("LMH010_SEMIEDI_INFO").Merge(ds.Tables("LMH010_SEMIEDI_INFO"))
            setDs.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Merge(dtEdiRcvDtl)

            ' EDI受信データ(DTL)の新規追加
            setDs = MyBase.CallDAC(Me._Dac, "InsertInkaEdiRcvDtl", setDs)
            iRcvDtlInsCnt += setDs.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Rows.Count()

            ' 取消対象のデータの H_INKAEDI_DTL_NCGO_NEW からの読み込み
            setDs = MyBase.CallDAC(Me._Dac, "SelectInkaediDtlNcgoNewCancel", setDs)
            If MyBase.GetResultCount > 0 Then
                ' H_INKAEDI_DTL_NCGO_NEW 更新 (入荷赤伝・取消・論理削除)
                setDs = MyBase.CallDAC(Me._Dac, "UpdateInkaediDtlNcgoNewCancel", setDs)
                iRcvDtlCanCnt += MyBase.GetResultCount()

                ' H_INKAEDI_L 更新 (入荷赤伝・取消・論理削除)
                setDs = MyBase.CallDAC(Me._Dac, "UpdateInkaediL_Cancel", setDs)
                iInHedCanCnt += MyBase.GetResultCount()

                ' H_INKAEDI_M 更新 (入荷赤伝・取消・論理削除)
                setDs = MyBase.CallDAC(Me._Dac, "UpdateInkaediM_Cancel", setDs)
                iInDtlCanCnt += MyBase.GetResultCount()
            End If

            setDs.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Clear()
            setDs.Tables("LMH010_INKAEDI_DTL_NCGO_NEW").Merge(dtEdiRcvDtl)

            ' H_INKAEDI_DTL_NCGO_NEW 取得 (EDI入荷(大) 登録用)
            setDs = MyBase.CallDAC(Me._Dac, "SelectForInkaediL_FromInkaediDtlNcgoNew", setDs)

            ' H_INKAEDI_DTL_NCGO_NEW 取得 (EDI入荷(中) 登録用)
            setDs = MyBase.CallDAC(Me._Dac, "SelectForInkaediM_FromInkaediDtlNcgoNew", setDs)

            If setDs.Tables("LMH010_INKAEDI_L").Rows.Count() > 0 Then
                ' EDI入荷データ(大)テーブル新規登録
                setDs = MyBase.CallDAC(Me._Dac, "InsertInkaEdiL", setDs)
                iInHedInsCnt = setDs.Tables("LMH010_INKAEDI_L").Rows.Count()
            End If

            If setDs.Tables("LMH010_INKAEDI_M").Rows.Count() > 0 Then
                ' EDI入荷データ(中)テーブル新規登録
                setDs = MyBase.CallDAC(Me._Dac, "InsertInkaEdiM", setDs)
                iInDtlInsCnt = setDs.Tables("LMH010_INKAEDI_M").Rows.Count()
            End If

            ' H_UNSOEDI_DTL_NCGO EDI_CTR_NO 更新 (最新の取込日時の H_INKAEDI_DTL_NCGO_NEW の EDI_CTL_NO へ)
            ' 対象: 最新の取込日時の  H_INKAEDI_DTL_NCGO_NEW の OUTKA_DENP_NO, OUTKA_DENP_DTL_NO と一致する H_UNSOEDI_DTL_NCGO
            ' 踏襲する EDIバッチの出荷返品の処理にあった更新につき移植したが、H_UNSOEDI_DTL_NCGO の抽出条件が今回取込日時のため
            ' 実質的には更新は発生しないと考えられる。
            ' →　入荷セミEDIでは EDI出荷の出荷返品およびEDI入荷 に準じて H_UNSOEDI_DTL_NCGO は登録しないので、
            ' 　　今回取込日時を持つ H_UNSOEDI_DTL_NCGO レコードは存在しえない
            setDs = MyBase.CallDAC(Me._Dac, "UpdateUnsoediDtlNcgo_EdiCtlNo", setDs)

        End If

        If bNoErr Then
            'エラー無し
            dtSetHed.Rows(0).Item("ERR_FLG") = "0"
        Else
            'エラー有り
            dtSetHed.Rows(0).Item("ERR_FLG") = "1"
        End If

        '処理件数
        dtSetRet.Rows(0).Item("RCV_HED_INS_CNT") = iRcvHedInsCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_DTL_INS_CNT") = iRcvDtlInsCnt.ToString()
        dtSetRet.Rows(0).Item("IN_HED_INS_CNT") = iInHedInsCnt.ToString()
        dtSetRet.Rows(0).Item("IN_DTL_INS_CNT") = iInDtlInsCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_HED_CAN_CNT") = iRcvHedCanCnt.ToString()
        dtSetRet.Rows(0).Item("RCV_DTL_CAN_CNT") = iRcvDtlCanCnt.ToString()
        dtSetRet.Rows(0).Item("IN_HED_CAN_CNT") = iInHedCanCnt.ToString()
        dtSetRet.Rows(0).Item("IN_DTL_CAN_CNT") = iInDtlCanCnt.ToString()

        Return ds

    End Function

#End Region ' "画面取込(セミEDI)データセット＋更新処理"

#End Region ' "セミEDI処理"

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

        Me._SetWarningFlg = False

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

                Me._SetWarningFlg = True

            End If

        Next

        Return setDt
    End Function

#End Region

End Class
