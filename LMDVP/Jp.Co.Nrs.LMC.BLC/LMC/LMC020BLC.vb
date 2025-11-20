' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷
'  プログラムID     :  LMC020    : 出荷データ編集
'  作  成  者       :  [矢内正之]
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMC020BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC020BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC020DAC = New LMC020DAC()

    '2017/09/25 修正 李↓
    ''20151106 tsunehira add
    ' ''' <summary>
    ' ''' 選択した言語を格納するフィールド
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _LangFlg As String = MessageManager.MessageLanguage
    '2017/09/25 修正 李↑

#End Region
#Region "Const"

    '2015.10.27 tusnehira add
    '英語化対応
    Private Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Private Const MESSEGE_LANGUAGE_ENGLISH As String = "1"



#If True Then ' 西濃自動送り状番号出力対応 20160701 added inoue

    ''' <summary>
    ''' 自動採番送り状区分(O010)
    ''' </summary>
    ''' <remarks></remarks>
    Class AUTO_DENP_KBN

        ''' <summary>
        ''' 名鉄運輸
        ''' </summary>
        ''' <remarks></remarks>
        Public Const MEITETSU_TRANSPORT As String = "01"

        ''' <summary>
        ''' 西濃運輸(千葉)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SEINO_TRANSPORTATION_CHIBA As String = "02"

        ''' <summary>
        ''' トールエクスプレス(大阪)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TTOLL_EXPRESS_OSAKA As String = "03"

        '2017/12/12 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add start
        Public Const TTOLL_EXPRESS_GUNMA As String = "04"
        '2017/12/12 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add end

        ''' <summary>
        ''' 西濃運輸土気(標準)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SEINO_TRANSPORTATION_TOKE As String = "05"

        ''' <summary>
        ''' JPロジスティクス[元トールエクスプレス](千葉)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TTOLL_EXPRESS_CHIBA As String = "06"

        ''' <summary>
        ''' JPロジスティクス[元トールエクスプレス](土気)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TTOLL_EXPRESS_TOKE As String = "07"

        ''' <summary>
        ''' 西濃運輸(袖ヶ浦)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SEINO_TRANSPORTATION_SODEGAURA As String = "08"

        ''' <summary>
        ''' 西濃運輸(大阪)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SEINO_TRANSPORTATION_OSAKA As String = "09"

    End Class

#End If

    '2018/12/07 ADD START 要望管理002171
#Region "出荷梱包個数自動計算"
    Public Const USE_GAMEN_VALUE_FALSE As String = "0"  '自動計算する
    Public Const USE_GAMEN_VALUE_TRUE As String = "1"   '画面入力値を使用する
#End Region
    '2018/12/07 ADD END   要望管理002171

#End Region


#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 初期検索の値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInitData(ByVal ds As DataSet) As DataSet

        '出荷(大)のデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectOutkaLData", ds)

        '出荷(中)のデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectOutkaMData", ds)

        ''出荷(小)、在庫のデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectOutkaSData", ds)

        '作業のデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectSagyoData", ds)

        '運送(大)のデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectUnsoLData", ds)

        '在庫のデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectZaiData", ds)

        'Maxシーケンスのデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectMaxNoData", ds)

        'EDI出荷(大)のデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectEDILData", ds)

        '2014/01/22 輸出情報追加 START
        '輸出情報のデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectExportLData", ds)
        '2014/01/22 輸出情報追加 END

        '2015.07.08 協立化学　シッピングマーク対応　追加START
        ds = MyBase.CallDAC(Me._Dac, "SelectMarkHedData", ds)
        '2015.07.08 協立化学　シッピングマーク対応　追加END

        '2015.07.08 協立化学　シッピングマーク対応　追加START
        ds = MyBase.CallDAC(Me._Dac, "SelectMarkDtlData", ds)
        '2015.07.08 協立化学　シッピングマーク対応　追加END

#If True Then   'add 2019/05/31 依頼番号 : 005136   【LMS】出荷毎に寄託価額✕商品　実際の金額を「LMC020 出荷データ編集」画面に出荷金額を表示(群馬本間) 
        ds = MyBase.CallDAC(Me._Dac, "SelectKitakugakuData", ds)
#End If
        If Me.CheckInoutkaEdiDtlFjfExists(ds) Then
            ' FFEM入出荷EDIデータ(ヘッダ) 取得
            ds = MyBase.CallDAC(Me._Dac, "SelectInoutkaEdiHedFjfData", ds)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 更新対象データ件数検索
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
        ElseIf MyBase.GetLimitCount() < count Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 請求鑑ヘッダテーブル検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKagamiData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectKagamiData", ds)

    End Function

    ''' <summary>
    ''' 荷主マスタ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCustData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectSubCustData", ds)

    End Function

    'START YANAI 要望番号853 まとめ処理対応
    ''' <summary>
    ''' 在庫データ検索(まとめ用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectZaiDataMATOME(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectZaiDataMATOME", ds)

    End Function
    'END YANAI 要望番号853 まとめ処理対応

    'START NAKAMURA 要望612 振替一括削除対応 2012.11.16
    ''' <summary>
    ''' 入荷マスタ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectInkaData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectFuriDelData", ds)

    End Function
    'END NAKAMURA 要望612 振替一括削除対応 2012.11.16


    ''' <summary>
    ''' FFEM入出荷EDIデータ(ヘッダ)テーブル 存在チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function CheckInoutkaEdiDtlFjfExists(ByVal ds As DataSet) As Boolean

        ds.Tables("LMC020_TBL_EXISTS").Clear()
        Dim drTblExists As DataRow = ds.Tables("LMC020_TBL_EXISTS").NewRow()
        drTblExists.Item("NRS_BR_CD") = ds.Tables("LMC020IN").Rows(0).Item("NRS_BR_CD")
        drTblExists.Item("TBL_NM") = "H_INOUTKAEDI_HED_FJF"
        ds.Tables("LMC020_TBL_EXISTS").Rows.Add(drTblExists)
        ds = Me.GetTrnTblExits(ds)

        Dim drExists As DataRow()
        drExists = ds.Tables("LMC020_TBL_EXISTS").Select("TBL_NM = 'H_INOUTKAEDI_HED_FJF'")
        If drExists.Count > 0 AndAlso drExists(0).Item("TBL_EXISTS").ToString() = "1" Then
            Return True
        Else
            Return False
        End If

    End Function

    ''' <summary>
    ''' 特定の荷主固有のテーブルが存在するか否かの判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function GetTrnTblExits(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "GetTrnTblExits", ds)

    End Function

#End Region

#Region "新規登録"

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)

        '請求日チェック
        Call Me.IsSeiqDateChk(ds, lgm.Selector({"保存", "Save", "보존", "中国語"}))
        '2017/09/25 修正 李↑

        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '出荷管理番号採番
        Dim outkaNoL As String = Me.GetOutkaNoL(ds)
        Dim value As String() = New String() {outkaNoL}
        Dim colNm As String() = New String() {"OUTKA_NO_L"}

        '出荷(大)に採番した値を設定
        ds = Me.SetValueData(ds, "LMC020_OUTKA_L", colNm, value)

        '出荷(中)に採番した値を設定
        ds = Me.SetValueData(ds, "LMC020_OUTKA_M", colNm, value)

        '出荷(小)に採番した値を設定
        ds = Me.SetValueData(ds, "LMC020_OUTKA_S", colNm, value)

        '2014/01/22 輸出情報追加 START
        '輸出情に採番した値を設定
        ds = Me.SetValueData(ds, "LMC020_EXPORT_L", colNm, value)
        '2014/01/22 輸出情報追加 START

        '2015.07.08 協立化学　シッピング対応 追加START
        ds = Me.SetValueData(ds, "LMC020_C_MARK_HED", colNm, value)
        '2015.07.08 協立化学　シッピング対応 追加END

        '2015.07.21 協立化学　シッピング対応 追加START
        ds = Me.SetValueData(ds, "LMC020_C_MARK_DTL", colNm, value)
        '2015.07.21 協立化学　シッピング対応 追加END

        Dim max As Integer = ds.Tables("LMC020_OUTKA_S").Rows.Count - 1
        Dim idoNo As String = String.Empty
        'START YANAI 要望番号681
        Dim zaiNo As String = String.Empty
        'END YANAI 要望番号681
        'START YANAI 20110913 小分け対応
        Dim outSDr() As DataRow = Nothing
        'END YANAI 20110913 小分け対応
        For i As Integer = 0 To max
            'START YANAI 20110913 小分け対応
            'If ("01").Equals(ds.Tables("LMC020_OUTKA_S").Rows(i).Item("SMPL_FLAG")) = True Then
            'START YANAI 要望番号772
            'If ("01").Equals(ds.Tables("LMC020_OUTKA_S").Rows(i).Item("SMPL_FLAG").ToString()) = True AndAlso _
            '    String.IsNullOrEmpty(ds.Tables("LMC020_OUTKA_S").Rows(i).Item("REC_NO").ToString()) = True Then
            If ("01").Equals(ds.Tables("LMC020_OUTKA_S").Rows(i).Item("SMPL_FLAG").ToString()) = True AndAlso _
                String.IsNullOrEmpty(ds.Tables("LMC020_OUTKA_S").Rows(i).Item("REC_NO").ToString()) = True AndAlso _
                Convert.ToDecimal(ds.Tables("LMC020_OUTKA_S").Rows(i).Item("ALCTD_CAN_NB").ToString()) <> 1 Then
                'END YANAI 要望番号772

                '同じ在庫データを小分けする場合は、移動レコードを1つにまとめるため、採番しない。
                'そのためのチェックを実施し、存在したら、移動レコード番号を設定
                outSDr = ds.Tables("LMC020_OUTKA_S").Select(String.Concat("ZAI_REC_NO = '", ds.Tables("LMC020_OUTKA_S").Rows(i).Item("ZAI_REC_NO").ToString(), "' AND ", _
                                                                          "REC_NO <> '' AND ", _
                                                                          "SYS_DEL_FLG = '0'"))
                If 0 < outSDr.Length Then
                    ds.Tables("LMC020_OUTKA_S").Rows(i).Item("REC_NO") = outSDr(0).Item("REC_NO").ToString()
                    'START YANAI 要望番号681
                    ds.Tables("LMC020_OUTKA_S").Rows(i).Item("N_ZAI_REC_NO") = outSDr(0).Item("N_ZAI_REC_NO").ToString()
                    'END YANAI 要望番号681
                    Continue For
                End If
                'END YANAI 20110913 小分け対応

                'START YANAI 要望番号681
                '在庫レコード番号採番
                zaiNo = Me.GetZaiRecNo(ds)
                value = New String() {zaiNo}
                colNm = New String() {"N_ZAI_REC_NO"}
                ds = Me.SetValueDataRow(ds, "LMC020_OUTKA_S", colNm, value, i)
                'END YANAI 要望番号681

                '移動レコード番号採番
                idoNo = Me.GetRecNo(ds)
                value = New String() {idoNo}
                colNm = New String() {"REC_NO"}
                ds = Me.SetValueDataRow(ds, "LMC020_OUTKA_S", colNm, value, i)

            End If
        Next

        '要望番号:2408 2015.09.17 追加START
#If False Then  ' 西濃自動送り状番号対応 20160701 chnaged inoue
        Dim autoDenpNo As String = String.Empty
        Dim intMeitestuNo As Integer = 0
        If String.IsNullOrEmpty(ds.Tables("LMC020_UNSO_L").Rows(0).Item("AUTO_DENP_KBN").ToString()) = False Then
            autoDenpNo = Me.GetMeiTetsuDenpNoL(ds)
            'チェックデジットの組込み
            intMeitestuNo = Convert.ToInt32(autoDenpNo) Mod 7
            autoDenpNo = String.Concat(autoDenpNo, Convert.ToString(intMeitestuNo))
        End If
#Else
        Dim autoDenpNo As String = String.Empty
        Dim autoDenpKbn As String = ds.Tables("LMC020_UNSO_L").Rows(0).Item("AUTO_DENP_KBN").ToString()
        If (String.IsNullOrEmpty(autoDenpKbn) = False) Then
            ' AUTO_DENP_KBNが変更された場合、呼び出し元でautoDenpNoはクリアされる。
            ' AUTO_DENP_KBN変更時、変更前のautoDenpNoが設定されていることは考慮しない。
            autoDenpNo = Me.GetAutoDenpNo(autoDenpKbn, ds)
        End If
#End If
        '要望番号:2408 2015.09.17 追加END

        '要望番号:2408 2015.09.17 修正START
        '新規登録
        Dim rtn As Boolean = Me.InsertData(ds, outkaNoL, autoDenpNo)    '2018/12/07 MOD 要望管理002171 戻り値を受け取るよう変更
        '要望番号:2408 2015.09.17 修正END

        '2018/12/07 ADD START 要望管理002171
        If rtn Then
            '出荷梱包個数自動計算（出荷(大)の更新）
            ds = Me.UpdateOutkaLOutkaPkgNb(ds)
        End If
        '2018/12/07 ADD END  要望管理002171

        Return ds

    End Function

    '要望番号:2408 2015.09.17 修正START
    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet, ByVal outkaNoL As String, ByVal autoDenpNo As String) As Boolean
        '要望番号:2408 2015.09.17 修正END

        '出荷(大)の新規登録
        Dim rtnResult As Boolean = Me.InsertOutkaLData(ds)

        '出荷(中)の新規登録
        rtnResult = rtnResult AndAlso Me.SetOutkaMData(ds)

        '出荷(小)の新規登録
        rtnResult = rtnResult AndAlso Me.SetOutkaSData(ds)

        '2014/01/22 輸出情報追加 START
        '輸出情報の新規登録
        rtnResult = rtnResult AndAlso Me.SetExportLData(ds)
        '2014/01/22 輸出情報追加 END

        '在庫の更新
        rtnResult = rtnResult AndAlso Me.SetZaiTrsData(ds, outkaNoL)

        '要望番号:2408 2015.09.17 修正START
        '運送項目の更新（運賃の作成は非対象）
        rtnResult = rtnResult AndAlso Me.SetUnsoData(ds, outkaNoL, autoDenpNo)
        '要望番号:2408 2015.09.17 修正END

        'START kasama WIT対応 商品管理番号採番処理
        '商品管理番号更新対象ハンディ荷主の場合のみ処理実施
        rtnResult = rtnResult AndAlso Me.UpdateGoodsKanriNo(ds)
        'END kasama WIT対応 商品管理番号採番処理

        '作業の更新
        rtnResult = rtnResult AndAlso Me.SetSagyoData(ds, outkaNoL)

        '2015.07.08 協立化学　シッピング対応 追加START
        If ds.Tables("LMC020_C_MARK_HED").Rows.Count > 0 Then
            'マーク(HED)の新規登録
            rtnResult = rtnResult AndAlso Me.SetMarkHedData(ds)
        End If
        '2015.07.08 協立化学　シッピング対応 追加END

        '2015.07.21 協立化学　シッピング対応 追加START
        If ds.Tables("LMC020_C_MARK_DTL").Rows.Count > 0 Then
            'マーク(DTL)の新規登録
            rtnResult = rtnResult AndAlso Me.SetMarkDtlData(ds)
        End If
        '2015.07.21 協立化学　シッピング対応 追加END

        '届先Mの更新しないようにした　
        '届先Mの更新 ADD 2018/05/14 依頼番号 001545 
        'rtnResult = rtnResult AndAlso Me.UpdateM_DEST(ds)


#If True Then   'add 2019/05/31 依頼番号 : 005136   【LMS】出荷毎に寄託価額✕商品　実際の金額を「LMC020 出荷データ編集」画面に出荷金額を表示(群馬本間) 
        ds = MyBase.CallDAC(Me._Dac, "SelectKitakugakuData", ds)
#End If
        Return rtnResult

    End Function

#End Region

#Region "更新登録"

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveAction(ByVal ds As DataSet) As DataSet

        '更新処理
        Dim rtn As Boolean = Me.UpdateData(ds)  '2018/12/07 MOD 要望管理002171 戻り値を受け取るよう変更

        '2018/12/07 ADD START 要望管理002171
        If rtn Then
            '出荷梱包個数自動計算（出荷(大)の更新）
            ds = Me.UpdateOutkaLOutkaPkgNb(ds)
        End If
        '2018/12/07 ADD END   要望管理002171

        Return ds

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As Boolean

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)

        '請求日チェック
        Call Me.IsSeiqDateChk(ds, lgm.Selector({"保存", "Save", "보존", "中国語"}))
        '2017/09/25 修正 李↑

        If MyBase.IsMessageExist() = True Then
            Return False
        End If

        Dim value As String() = New String() {String.Empty}
        Dim colNm As String() = New String() {String.Empty}
        Dim max As Integer = ds.Tables("LMC020_OUTKA_S").Rows.Count - 1
        Dim idoNo As String = String.Empty

        'START YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
        Dim zaiNo As String = String.Empty
        'END YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる

        'START YANAI 20110913 小分け対応
        Dim outSDr() As DataRow = Nothing
        'END YANAI 20110913 小分け対応
        For i As Integer = 0 To max
            'START YANAI 20110913 小分け対応
            'If ("01").Equals(ds.Tables("LMC020_OUTKA_S").Rows(i).Item("SMPL_FLAG")) = True Then
            'START YANAI 要望番号772
            'If ("01").Equals(ds.Tables("LMC020_OUTKA_S").Rows(i).Item("SMPL_FLAG").ToString()) = True AndAlso _
            '    String.IsNullOrEmpty(ds.Tables("LMC020_OUTKA_S").Rows(i).Item("REC_NO").ToString()) = True Then
            If ("01").Equals(ds.Tables("LMC020_OUTKA_S").Rows(i).Item("SMPL_FLAG").ToString()) = True AndAlso _
                String.IsNullOrEmpty(ds.Tables("LMC020_OUTKA_S").Rows(i).Item("REC_NO").ToString()) = True AndAlso _
                Convert.ToDecimal(ds.Tables("LMC020_OUTKA_S").Rows(i).Item("ALCTD_CAN_NB").ToString()) <> 1 Then
                'END YANAI 要望番号772

                '同じ在庫データを小分けする場合は、移動レコードを1つにまとめるため、採番しない。
                'そのためのチェックを実施し、存在したら、移動レコード番号を設定
                outSDr = ds.Tables("LMC020_OUTKA_S").Select(String.Concat("ZAI_REC_NO = '", ds.Tables("LMC020_OUTKA_S").Rows(i).Item("ZAI_REC_NO").ToString(), "' AND ", _
                                                                          "REC_NO <> '' AND ", _
                                                                          "SYS_DEL_FLG = '0' AND ", _
                                                                          "UP_KBN = '0'"))
                If 0 < outSDr.Length Then
                    ds.Tables("LMC020_OUTKA_S").Rows(i).Item("REC_NO") = outSDr(0).Item("REC_NO").ToString()
                    Continue For
                End If
                'END YANAI 20110913 小分け対応

                'START YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
                '在庫レコード番号採番
                zaiNo = Me.GetZaiRecNo(ds)
                value = New String() {zaiNo}
                colNm = New String() {"N_ZAI_REC_NO"}
                ds = Me.SetValueDataRow(ds, "LMC020_OUTKA_S", colNm, value, i)
                'END YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる

                '移動レコード番号採番
                idoNo = Me.GetRecNo(ds)
                value = New String() {idoNo}
                colNm = New String() {"REC_NO"}
                ds = Me.SetValueDataRow(ds, "LMC020_OUTKA_S", colNm, value, i)

            End If
        Next

        '在庫の更新登録
        Dim outkaNoL As String = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("OUTKA_NO_L").ToString()
        Dim rtnResult As Boolean = Me.SetZaiTrsData(ds, outkaNoL)

        '出荷(大)の更新登録
        rtnResult = rtnResult AndAlso Me.UpdateOutkaLData(ds)

        'START YANAI 20110913 小分け対応
        ''出荷(中)の更新登録
        'rtnResult = rtnResult AndAlso Me.SetOutkaMData(ds)

        ''出荷(小)の更新登録
        'rtnResult = rtnResult AndAlso Me.SetOutkaSData(ds)
        Dim torikeshiFlg As String = String.Empty
        torikeshiFlg = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("TORIKESHI_FLG").ToString

        If ("01").Equals(torikeshiFlg) = False Then
            '完了取消以外の時のみ行う

            '出荷(中)の更新登録
            rtnResult = rtnResult AndAlso Me.SetOutkaMData(ds)

            '出荷(小)の更新登録
            rtnResult = rtnResult AndAlso Me.SetOutkaSData(ds)
        End If
        'END YANAI 20110913 小分け対応

        '要望番号:997 terakawa 2012.10.22 Start
        If ds.Tables("LMC020_EDI_UPD_TBL").Rows.Count > 0 AndAlso _
            ds.Tables("LMC020_EDI_UPD_TBL").Rows(0).Item("EDI_UPD_FLG").ToString = LMConst.FLG.ON Then
            'EDI出荷(中)の更新
            rtnResult = rtnResult AndAlso Me.SetOutkaEdiMData(ds)

            'EDI出荷受信テーブルの更新
            rtnResult = rtnResult AndAlso Me.SetOutkaEdiDtlData(ds)
        End If
        '要望番号:997 terakawa 2012.10.22 End

        '要望番号:2408 2015.09.17 追加START

        Dim outLDenpKbn As String = ds.Tables("LMC020_UNSO_L").Rows(0).Item("AUTO_DENP_KBN").ToString()
        Dim outLDenpNo As String = ds.Tables("LMC020_UNSO_L").Rows(0).Item("AUTO_DENP_NO").ToString()
        Dim autoDenpNo As String = String.Empty
#If False Then  ' 西濃自動送り状番号対応 20160701 chnaged inoue
        Dim intMeitestuNo As Integer = 0
        If String.IsNullOrEmpty(outLDenpKbn) = False AndAlso String.IsNullOrEmpty(outLDenpNo) = True Then
            autoDenpNo = Me.GetMeiTetsuDenpNoL(ds)
            'チェックデジットの組込み
            intMeitestuNo = Convert.ToInt32(autoDenpNo) Mod 7
            autoDenpNo = String.Concat(autoDenpNo, Convert.ToString(intMeitestuNo))

        End If

#Else
        ' AUTO_DENP_KBNが変更された場合、呼び出し元でoutLDenpNoはクリアされる。
        ' AUTO_DENP_KBN変更時、変更前のoutLDenpNoが設定されていることは考慮しない。
        If String.IsNullOrEmpty(outLDenpKbn) = False AndAlso
           String.IsNullOrEmpty(outLDenpNo) = True Then
            autoDenpNo = Me.GetAutoDenpNo(outLDenpKbn, ds)
        End If
#End If
        '要望番号:2408 2015.09.17 追加END

        '要望番号:2408 2015.09.17 修正START
        '運送項目の更新
        rtnResult = rtnResult AndAlso Me.SetUnsoData(ds, outkaNoL, autoDenpNo)
        '要望番号:2408 2015.09.17 修正END

        'START kasama WIT対応 商品管理番号採番処理
        '商品管理番号更新対象ハンディ荷主の場合のみ処理実施
        rtnResult = rtnResult AndAlso Me.UpdateGoodsKanriNo(ds)
        'END kasama WIT対応 商品管理番号採番処理

        '作業の更新
        rtnResult = rtnResult AndAlso Me.SetSagyoData(ds, String.Empty)

        '2014/01/22 輸出情報追加 START
        '輸出情報の新規登録
        rtnResult = rtnResult AndAlso Me.SetExportLData(ds)
        '2014/01/22 輸出情報追加 END

        '2015.07.08 協立化学　シッピング対応 追加START
        If ds.Tables("LMC020_C_MARK_HED").Rows.Count > 0 Then
            'マーク(HED)の新規登録
            rtnResult = rtnResult AndAlso Me.SetMarkHedData(ds)
        End If
        '2015.07.08 協立化学　シッピング対応 追加END

        '2015.07.21 協立化学　シッピング対応 追加START
        If ds.Tables("LMC020_C_MARK_DTL").Rows.Count > 0 Then
            'マーク(DTL)の新規登録
            rtnResult = rtnResult AndAlso Me.SetMarkDtlData(ds)
        End If
        '2015.07.21 協立化学　シッピング対応 追加END

        '2014.07.09 (黎) PIC_WKの削除廃止(フロント側で下記のFunctionを呼ばないように対応)・・・戻せるように関連コード自体は生存させています --ST--
        '2014.04.09 (黎) WITピッキングWK削除
        'rtnResult = rtnResult AndAlso Me.IsDeletePickWK(ds)
        '2014.04.09 (黎) WITピッキングWK削除
        '2014.07.09 (黎) PIC_WKの削除廃止(フロント側で下記のFunctionを呼ばないように対応)・・・戻せるように関連コード自体は生存させています --ED--

        '届先Mの更新しないようにした
        '届先Mの更新 ADD 2018/05/14 依頼番号 001545 
        'rtnResult = rtnResult AndAlso Me.UpdateM_DEST(ds)

        If ds.Tables("LMC020_JIKAI_BUNNOU").Rows.Count > 0 Then
            ' 次回分納データの削除

            ' Rapidus出荷指示EDIデータ
            rtnResult = rtnResult AndAlso Me.DeleteOutkaEdiDtlRapiJikaiBunnou(ds)
            ' EDI出荷L
            rtnResult = rtnResult AndAlso Me.DeleteOutkaEdiLJikaiBunnou(ds)
            ' EDI出荷M
            rtnResult = rtnResult AndAlso Me.DeleteOutkaEdiMJikaiBunnou(ds)
        End If

#If True Then   'add 2019/05/31 依頼番号 : 005136   【LMS】出荷毎に寄託価額✕商品　実際の金額を「LMC020 出荷データ編集」画面に出荷金額を表示(群馬本間) 
        ds = MyBase.CallDAC(Me._Dac, "SelectKitakugakuData", ds)
#End If

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
    ''' 印刷時の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdatePrintData(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)
        Dim printKb As String = dr.Item("PRINT_KB").ToString()

        If "05".Equals(printKb) = True Then
            '分析表（排他なし）
            Me.DacAccess(ds, "UpdatePrintDataCOA")
        Else
            Me.DacAccess(ds, "UpdatePrintData")
        End If
        Return ds
    End Function

#End Region

#Region "削除登録"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteAction(ByVal ds As DataSet) As DataSet

        '更新処理
        Call Me.DeleteData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As Boolean

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)

        '請求日チェック
        Call Me.IsSeiqDateChk(ds, lgm.Selector({"保存", "Save", "보존", "中国語"}), LMConst.FLG.ON)
        '2017/09/25 修正 李↑

        If MyBase.IsMessageExist() = True Then
            Return False
        End If

        '在庫の論理削除
        Dim rtnResult As Boolean = Me.DeleteZaiData(ds)

        ' 2012.11.28 要望番号：612 振替一括削除対応 START Nakamura
        '振替データが存在するときのみ処理する
        If ds.Tables("LMC020_FURI_DEL").Rows.Count > 0 Then
            '2012.11.28 NAKAMURA 振替削除対応 START
            '入荷(大)の論理削除
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateInkaLDelFlg")

            '入荷(中)の論理削除
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateInkaMSysDelFlg")

            '入荷(小)の論理削除
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateInkaSSysDelFlg")

            '振替先の在庫の論理削除（削除フラグを立てる）
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateInZaiDelFlg")

        End If
        ' 2012.11.28 要望番号：612 振替一括削除対応 END Nakamura


        '出荷(大)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateOutkaLDelFlg")

        '出荷(中)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateOutkaMSysDelFlg")

        '出荷(小)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateOutkaSSysDelFlg")

        '2014/01/22 輸出情報追加 START
        '輸出情報のデータの論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateExportLSysDelFlg")
        '2014/01/22 輸出情報追加 END

        '2015.07.08 協立化学　シッピングマーク対応 追加START
        'マーク(HED)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateMarkHedSysDelFlg")
        '2015.07.08 協立化学　シッピングマーク対応 追加END

        '2015.07.21 協立化学　シッピングマーク対応 追加START
        'マーク(DTL)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "UpdateMarkDtlSysDelFlg")
        '2015.07.21 協立化学　シッピングマーク対応 追加END

        ' 2012.11.28 要望番号：612 振替一括削除対応 START Nakamura
        '振替の場合は運送、運賃の処理はしない。
        If ds.Tables("LMC020_FURI_DEL").Rows.Count = 0 Then

            If ds.Tables("LMC020_UNSO_L").Rows.Count > 0 Then 'ADD 2020/02/27 010901

                '運送(大)の物理削除
                rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteUnsoLData")

                '運送(中)の物理削除
                rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteUnsoMData")

                '運賃の物理削除
                rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteUnchinData")

                '要望番号：1612 terakawa 2012.12.03 Start
                '支払運賃の物理削除
                rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteShiharaiData")
                '要望番号：1612 terakawa 2012.12.03 End

            End If 'ADD 2020/02/27 010901

        End If

        '作業の物理削除
        rtnResult = rtnResult AndAlso Me.DeleteSagyoData(ds)

        ' 2012.11.28 要望番号：612 振替一括削除対応 START Nakamura
        '作業の物理削除 2012.12.13 入荷側の作業　物理削除追加
        If ds.Tables("LMC020_FURI_DEL").Rows.Count > 0 Then
            rtnResult = rtnResult AndAlso Me.DeleteSagyoDataIn(ds)
        End If
        ' 2012.11.28 要望番号：612 振替一括削除対応 END Nakamura

        '2014.04.24 CALT対応 入荷大削除時キャンセルデータ作成 黎 追加 --ST--
        rtnResult = rtnResult AndAlso Me.DelOutkaSendData(ds)
        '2014.04.24 CALT対応 入荷大削除時キャンセルデータ作成 黎 追加 --ED--

        Return rtnResult

    End Function

    ''' <summary>
    ''' 在庫の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteZaiData(ByVal ds As DataSet) As Boolean

        ds = Me.SetDeleteData(ds, "LMC020_ZAI")

        Return Me.ServerChkJudge(ds, "ComZaiTrsData")

    End Function

    ''' <summary>
    ''' 作業の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteSagyoData(ByVal ds As DataSet) As Boolean

        ds = Me.SetDeleteData(ds, "LMC020_SAGYO")

        Return Me.ServerChkJudge(ds, "ComSagyoData")

    End Function

    ' 2012.11.28 要望番号：612 振替一括削除対応 START Nakamura
    ''' <summary>
    ''' 作業の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteSagyoDataIn(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "DelSagyoDataIn")

    End Function
    ' 2012.11.28 要望番号：612 振替一括削除対応 END Nakamura

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

    '2014.04.17 CALT連携対応 ri追加 --ST--
#Region "CALT対応"
    ''' <summary>
    ''' 出荷指示データ作成処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DelOutkaSendData(ByVal ds As DataSet) As Boolean

        '--外部定義変数
        '***カウンター
        'LMB010IN_INKA_PLAN_SENDテーブルのRowカウンタ
        Dim iIDirectSendCnt As Integer = 0
        Dim Cnt As Integer = 0

        '****フラグ系
        '結果フラグ
        Dim bResult As Boolean = True

        '***データセット系
        'IN用データセット
        Dim dsI As DataSet = ds.Copy()
        Dim dtIDirectSend As DataTable = dsI.Tables("LMC020_OUTKA_L")
        '作業データセット
        Dim dsTmp As DataSet = ds.Clone()
        Dim drTmp As DataRow = Nothing
        '==========================================================

        'INデータ分だけループ(2014.04.24の組込時点ではループは一回のみ)
        iIDirectSendCnt = dtIDirectSend.Rows.Count
        For i As Integer = 0 To iIDirectSendCnt - 1

            Dim drIDirectSend As DataRow = Nothing
            drIDirectSend = dtIDirectSend.Rows(i)

            '--倉庫チェック
            MyBase.CallDAC(Me._Dac, "ChkWhCd", dsI)
            If MyBase.GetResultCount() < 1 Then
                Exit For
            End If

            '==========================================================

            '--キャンセルデータの抽出
            Dim dsC As DataSet = dsI.Copy
            dsC = MyBase.CallDAC(Me._Dac, "SelectSendCancel", dsC)

            '--DACアクセス(INSERT) --キャンセル報告データ作成--
            If MyBase.GetResultCount() > 0 Then

                '--パラメータの編集(必要があれば当Function内に記述
                Call Me.EditData(dsC)

                Cnt = 0
                Cnt = dsC.Tables("LMC020_OUTKA_DEL_OUT").Rows.Count
                For j As Integer = 0 To Cnt - 1
                    dsTmp.Tables("LMC020_OUTKA_DEL_OUT").Clear()

                    drTmp = dsC.Tables("LMC020_OUTKA_DEL_OUT").Rows(j)

                    dsTmp.Tables("LMC020_OUTKA_DEL_OUT").ImportRow(drTmp)

                    MyBase.CallDAC(Me._Dac, "InsertDirectSend", dsTmp)
                Next
            End If

        Next

        Return bResult

    End Function

    ''' <summary>
    ''' パラメータエディット処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub EditData(ByRef ds As DataSet)
        Dim dsEdit As DataSet = ds.Copy()
        Dim dtEditSend As DataTable = dsEdit.Tables("LMC020_OUTKA_DEL_OUT")

        Dim iSeq As Integer = 0

        For Each editRow As DataRow In dtEditSend.Rows
            iSeq = Convert.ToInt32(editRow.Item("SEND_SEQ"))
            editRow.Item("SEND_SEQ") = Right(String.Concat("000", iSeq), 3)
            '=================
        Next

        ds.Clear()

        ds = dsEdit
    End Sub

#End Region
    '2014.04.17 CALT連携対応 ri追加 --ST

#End Region

#Region "ユーティリティ"

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
    ''' OUTKA_NO_Lを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>OutkaNoL</returns>
    ''' <remarks></remarks>
    Private Function GetOutkaNoL(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.OUTKA_NO_L, Me, ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' ZAI_REC_NOを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>ZaiRecNo</returns>
    ''' <remarks></remarks>
    Private Function GetZaiRecNo(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.ZAI_REC_NO, Me, ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' UNSO_NO_Lを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>OutkaNoL</returns>
    ''' <remarks></remarks>
    Private Function GetUnsoNoL(ByVal ds As DataSet) As String

        GetUnsoNoL = ds.Tables("LMC020_UNSO_L").Rows(0).Item("UNSO_NO_L").ToString()

        '値が入っていない場合、採番
        If String.IsNullOrEmpty(GetUnsoNoL) = True Then
            Dim num As New NumberMasterUtility
            GetUnsoNoL = num.GetAutoCode(NumberMasterUtility.NumberKbn.UNSO_NO_L, Me, ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString())
        End If

        Return GetUnsoNoL

    End Function

    ''' <summary>
    ''' SAGYO_REC_NOを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>ZaiRecNo</returns>
    ''' <remarks></remarks>
    Private Function GetSagyoRecNo(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_REC_NO, Me, ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' REC_NO(移動レコード番号)を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>RecNo</returns>
    ''' <remarks></remarks>
    Private Function GetRecNo(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.IDO_REC_NO, Me, ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    '要望番号:2408 2015.09.17 追加START
    ''' <summary>
    ''' MEITETSU_DENP_NOを取得
    ''' </summary>
    ''' <returns>MEITETSU_DENP_NO</returns>
    ''' <remarks></remarks>
    Private Function GetMeiTetsuDenpNoL() As String


#If False Then ' 西濃自動送り状番号出力対応 20160701 chagned inoue
#Else
        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
            GetAutoCode(NumberMasterUtility.NumberKbn.MEITETSU_DENP_NO, Me)

        If (String.IsNullOrEmpty(newCode) = False) Then

            'チェックデジットの組込み
            Dim checkDigit As Integer = Convert.ToInt32(newCode) Mod 7
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo

#End If
    End Function
    '要望番号:2408 2015.09.17 追加END


    ''' <summary>
    ''' TOLL_DENP_NOを取得
    ''' </summary>
    ''' <returns>TOLL_DENP_NO</returns>
    ''' <remarks></remarks>
    Private Function GetTollDenpNoL(ByVal ds As DataSet) As String


        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
        GetAutoCode(NumberMasterUtility.NumberKbn.TOLL_DENP_NO, Me)

        Dim dt As DataTable = ds.Tables("LMC020_OKURIJYO_WK")
        Dim okurijyoHed As String = String.Empty

        If dt.Rows.Count > 0 Then
            okurijyoHed = dt.Rows(0).Item("OKURIJYO_HEAD").ToString
        End If

        If (String.IsNullOrEmpty(newCode) = False) Then

            Dim checkDigit As Integer

            'チェックデジットの組込み

            If String.Empty.Equals(okurijyoHed) = False Then
                '区分マスタより取得できた時
                newCode = String.Concat(okurijyoHed, newCode)
            End If

            checkDigit = CInt(Convert.ToInt64(newCode) Mod 7)
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo


    End Function

    '2017/12/12 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add start
    ''' <summary>
    ''' TOLL_DENP_NO(群馬)を取得
    ''' </summary>
    ''' <returns>TOLL_DENP_NO</returns>
    ''' <remarks></remarks>
    Private Function GetGunmaTollDenpNoL(ByVal ds As DataSet) As String


        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
        GetAutoCode(NumberMasterUtility.NumberKbn.TOLL_DENP_NO_CYD, Me)

        Dim dt As DataTable = ds.Tables("LMC020_OKURIJYO_WK")
        Dim okurijyoHed As String = String.Empty

        If dt.Rows.Count > 0 Then
            okurijyoHed = dt.Rows(0).Item("OKURIJYO_HEAD").ToString
        End If

        If (String.IsNullOrEmpty(newCode) = False) Then

            Dim checkDigit As Integer

            'チェックデジットの組込み

            If String.Empty.Equals(okurijyoHed) = False Then
                '区分マスタより取得できた時
                newCode = String.Concat(okurijyoHed, newCode)
            End If

            checkDigit = CInt(Convert.ToInt64(newCode) Mod 7)
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo


    End Function
    '2017/12/12 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add end

    ''' <summary>
    ''' 送り状番号生成(千葉JPロジスティクス[元トールエクスプレス])
    ''' </summary>
    ''' <returns>送り状番号</returns>
    ''' <remarks></remarks>
    Private Function GetChibaTollDenpNoL(ByVal ds As DataSet) As String

        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
        GetAutoCode(NumberMasterUtility.NumberKbn.JPLOGI_DENP_NO_CHI, Me)

        Dim dt As DataTable = ds.Tables("LMC020_OKURIJYO_WK")
        Dim okurijyoHed As String = String.Empty

        If dt.Rows.Count > 0 Then
            okurijyoHed = dt.Rows(0).Item("OKURIJYO_HEAD").ToString
        End If

        If (String.IsNullOrEmpty(newCode) = False) Then

            Dim checkDigit As Integer

            'チェックデジットの組込み

            If String.Empty.Equals(okurijyoHed) = False Then
                '区分マスタより取得できた時
                newCode = String.Concat(okurijyoHed, newCode)
            End If

            checkDigit = CInt(Convert.ToInt64(newCode) Mod 7)
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo


    End Function

    ''' <summary>
    ''' 送り状番号生成(土気JPロジスティクス[元トールエクスプレス])
    ''' </summary>
    ''' <returns>送り状番号</returns>
    ''' <remarks></remarks>
    Private Function GetTokeTollDenpNoL(ByVal ds As DataSet) As String

        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
        GetAutoCode(NumberMasterUtility.NumberKbn.JPLOGI_DENP_NO_TOK, Me)

        Dim dt As DataTable = ds.Tables("LMC020_OKURIJYO_WK")
        Dim okurijyoHed As String = String.Empty

        If dt.Rows.Count > 0 Then
            okurijyoHed = dt.Rows(0).Item("OKURIJYO_HEAD").ToString
        End If

        If (String.IsNullOrEmpty(newCode) = False) Then

            Dim checkDigit As Integer

            'チェックデジットの組込み

            If String.Empty.Equals(okurijyoHed) = False Then
                '区分マスタより取得できた時
                newCode = String.Concat(okurijyoHed, newCode)
            End If

            checkDigit = CInt(Convert.ToInt64(newCode) Mod 7)
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo


    End Function


#If True Then ' 西濃自動送り状番号出力対応 20160701 added inoue


    ''' <summary>
    ''' 自動採番送り状番号取得
    ''' </summary>
    ''' <param name="autoDenpKbn">自動送り状番号区分()</param>
    ''' <returns>自動送り状番号</returns>
    ''' <remarks>
    ''' LMC010BLC,LMC020BLC,LMF020BLCに同メソッドあり
    ''' ToDo:共通ライブラリ化を検討
    ''' </remarks>
    Private Function GetAutoDenpNo(ByVal autoDenpKbn As String, ByVal ds As DataSet) As String

        Dim autoDenpNo As String = String.Empty
        If (String.IsNullOrEmpty(autoDenpKbn)) Then
            ' 自動送り状番号払い出し対象外
            Return autoDenpNo
        End If

        Dim nubmerKbn As NumberMasterUtility.NumberKbn = Nothing
        Select Case autoDenpKbn
            Case AUTO_DENP_KBN.MEITETSU_TRANSPORT
                ' 名鉄運輸送り状番号生成
                autoDenpNo = Me.GetMeiTetsuDenpNoL()

            Case AUTO_DENP_KBN.SEINO_TRANSPORTATION_CHIBA
                ' 西濃運輸(千葉)送り状番号生成
                nubmerKbn = NumberMasterUtility.NumberKbn.AUTO_DENP_NO_SEINO_CHIBA
                autoDenpNo = New NumberMasterUtility().GetAutoCode(nubmerKbn, Me)

            Case AUTO_DENP_KBN.TTOLL_EXPRESS_OSAKA
                ' トールエクスプレス送り状番号生成                
                autoDenpNo = Me.GetTollDenpNoL(ds)

                '2017/12/12 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add start
            Case AUTO_DENP_KBN.TTOLL_EXPRESS_GUNMA
                ' トールエクスプレス送り状番号生成                
                autoDenpNo = Me.GetGunmaTollDenpNoL(ds)
                '2017/12/12 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add end

            Case AUTO_DENP_KBN.SEINO_TRANSPORTATION_TOKE
                ' 西濃運輸(千葉)送り状番号生成
                nubmerKbn = NumberMasterUtility.NumberKbn.AUTO_DENP_NO_SEINO_TOKE
                autoDenpNo = New NumberMasterUtility().GetAutoCode(nubmerKbn, Me)

            Case AUTO_DENP_KBN.TTOLL_EXPRESS_CHIBA
                'JPロジスティクス[元トールエクスプレス](千葉) 送り状番号生成
                autoDenpNo = Me.GetChibaTollDenpNoL(ds)

            Case AUTO_DENP_KBN.TTOLL_EXPRESS_TOKE
                'JPロジスティクス[元トールエクスプレス](土気) 送り状番号生成
                autoDenpNo = Me.GetTokeTollDenpNoL(ds)

            Case AUTO_DENP_KBN.SEINO_TRANSPORTATION_SODEGAURA
                ' 西濃運輸(袖ヶ浦)送り状番号生成
                nubmerKbn = NumberMasterUtility.NumberKbn.AUTO_DENP_NO_SEINO_SODEGAURA
                autoDenpNo = New NumberMasterUtility().GetAutoCode(nubmerKbn, Me)

            Case AUTO_DENP_KBN.SEINO_TRANSPORTATION_OSAKA
                ' 西濃運輸(大阪)送り状番号生成
                nubmerKbn = NumberMasterUtility.NumberKbn.AUTO_DENP_NO_SEINO_OSAKA
                autoDenpNo = New NumberMasterUtility().GetAutoCode(nubmerKbn, Me)

            Case Else
                Throw New InvalidOperationException(String.Format("定義されていないAUTO_DENP_KBN=[{0}]が指定されました。", autoDenpKbn))

        End Select

        Return autoDenpNo

    End Function

#End If


    ''' <summary>
    ''' 全ての行にValueの値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="colNm">列名</param>
    ''' <param name="value">設定したい値</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetValueData(ByVal ds As DataSet, ByVal tblNm As String, ByVal colNm As String(), ByVal value As String()) As DataSet

        Dim dt As DataTable = ds.Tables(tblNm)
        Dim max As Integer = dt.Rows.Count - 1
        Dim count As Integer = value.Length - 1

        For i As Integer = 0 To max

            For j As Integer = 0 To count

                dt.Rows(i).Item(colNm(j)) = value(j)

            Next

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 該当行にValueの値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="colNm">列名</param>
    ''' <param name="value">設定したい値</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetValueDataRow(ByVal ds As DataSet, ByVal tblNm As String, ByVal colNm As String(), ByVal value As String(), ByVal row As Integer) As DataSet

        Dim dt As DataTable = ds.Tables(tblNm)

        dt.Rows(row).Item(colNm(0)) = value(0)

        Return ds

    End Function

#End Region

#Region "共通更新"

    ''' <summary>
    ''' 出荷(大)新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertOutkaLData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertOutkaLData")

    End Function

    ''' <summary>
    ''' 出荷(大)更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaLData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateOutkaLData")

    End Function

    ''' <summary>
    ''' 出荷(大)削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteOutkaLData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateOutkaLDelFlg")

    End Function

    ''' <summary>
    ''' 出荷(中)更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetOutkaMData(ByVal ds As DataSet) As Boolean

        '共通
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "ComOutkaMData")

        Return rtnResult

    End Function

    ''' <summary>
    ''' 出荷(小)更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetOutkaSData(ByVal ds As DataSet) As Boolean

        '共通
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "ComOutkaSData")

        Return rtnResult

    End Function

    ''' <summary>
    ''' 届先M更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateM_DEST(ByVal ds As DataSet) As Boolean

        '共通
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "UpdateM_DEST")

        Return rtnResult

    End Function

    ''' <summary>
    ''' 在庫更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="OutkaNoL">入荷(大)番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetZaiTrsData(ByVal ds As DataSet, ByVal OutkaNoL As String) As Boolean

        '共通
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "ComZaiTrsData")

        Return rtnResult

    End Function

    ''' <summary>
    ''' 運送系の項目更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="OutNol">出荷(大)番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetUnsoData(ByVal ds As DataSet, ByVal OutNol As String, ByVal autoDenpNo As String) As Boolean

        '運送(大)番号
        Dim unsoNoL As String = String.Empty

        '運送(大)のデータセットが空の場合は保存処理を行わない
        If (0).Equals(ds.Tables("LMC020_UNSO_L").Rows.Count) = True Then
            Return True
        End If

        '運送(大)の削除フラグ = '1'の場合、スルー
        If LMConst.FLG.ON.Equals(ds.Tables("LMC020_UNSO_L").Rows(0).Item("SYS_DEL_FLG")) = False Then

            unsoNoL = Me.GetUnsoNoL(ds)
            Dim colNm As String() = New String() {"UNSO_NO_L"}
            Dim value As String() = New String() {unsoNoL}

            '運送番号の設定
            ds = Me.SetValueData(ds, "LMC020_UNSO_M", colNm, value)
            '要望番号:2408 2015.09.17 修正START
            ReDim Preserve colNm(1)
            ReDim Preserve value(1)
            colNm(1) = "INOUTKA_NO_L"
            value(1) = OutNol
            If String.IsNullOrEmpty(autoDenpNo) = False Then
                ReDim Preserve colNm(2)
                ReDim Preserve value(2)
                colNm(2) = "AUTO_DENP_NO"
                value(2) = autoDenpNo
                '要望番号:2408 2015.09.17 修正END
            End If
            ds = Me.SetValueData(ds, "LMC020_UNSO_L", colNm, value)

        End If

        '運送(大)の更新
        Dim rtnResult As Boolean = Me.SetUnsoLData(ds)

        If LMConst.FLG.ON.Equals(ds.Tables("LMC020_UNSO_L").Rows(0).Item("TORIKESI_FLG")) = False Then

            '(請求)運賃の更新
            rtnResult = rtnResult AndAlso Me.SetUnchinDelData(ds)

            'START UMANO 要望番号1369 支払運賃に伴う修正。
            If String.IsNullOrEmpty(ds.Tables("LMC020_UNSO_L").Rows(0).Item("TRIP_NO").ToString()) = True AndAlso _
               String.IsNullOrEmpty(ds.Tables("LMC020_UNSO_L").Rows(0).Item("TRIP_NO_SYUKA").ToString()) = True AndAlso _
               String.IsNullOrEmpty(ds.Tables("LMC020_UNSO_L").Rows(0).Item("TRIP_NO_TYUKEI").ToString()) = True AndAlso _
               String.IsNullOrEmpty(ds.Tables("LMC020_UNSO_L").Rows(0).Item("TRIP_NO_HAIKA").ToString()) = True Then
                'START UMANO 要望番号1302 支払運賃に伴う修正。
                '(支払)運賃の更新
                rtnResult = rtnResult AndAlso Me.SetShiharaiDelData(ds)
                'END UMANO 要望番号1302 支払運賃に伴う修正。
            End If
            'END UMANO 要望番号1369 支払運賃に伴う修正。

        End If

        Return rtnResult

    End Function

    ''' <summary>
    ''' 運送(大・中)の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetUnsoLData(ByVal ds As DataSet) As Boolean

        Dim rtnResult As Boolean = True

        Dim dt As DataTable = ds.Tables("LMC020_UNSO_L")
        If dt.Rows.Count < 1 Then
            Return rtnResult
        End If

        '新規登録 且つ 削除フラグ = 1はスルー
        Dim upKbnChk As Boolean = LMConst.FLG.OFF.Equals(dt.Rows(0).Item("UP_KBN").ToString())
        Dim delChk As Boolean = LMConst.FLG.ON.Equals(dt.Rows(0).Item("SYS_DEL_FLG").ToString())
        If upKbnChk = True AndAlso delChk = True Then
            Return rtnResult
        End If

        '削除フラグ = 1は物理削除
        If delChk = True Then
            rtnResult = Me.ServerChkJudge(ds, "DeleteUnsoLData")
            Return rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteUnsoMData")
        End If

        '新規の場合、レコード追加
        If upKbnChk = True Then
            rtnResult = Me.ServerChkJudge(ds, "InsertUnsoLData")
            Return rtnResult AndAlso Me.ServerChkJudge(ds, "InsertUnsoMData")
        End If

        '更新の場合
        rtnResult = Me.ServerChkJudge(ds, "UpdateUnsoLData")
        'START YANAI 20110913 小分け対応
        'rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteUnsoMData")
        Dim torikeshiFlg As String = String.Empty
        torikeshiFlg = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("TORIKESHI_FLG").ToString

        If ("01").Equals(torikeshiFlg) = False Then
            '完了取消以外の時のみ行う
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteUnsoMData")
            'START YANAI 要望番号1013
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "InsertUnsoMData")
            'END YANAI 要望番号1013
        End If
        'END YANAI 20110913 小分け対応
        'START YANAI 要望番号1013
        'Return rtnResult AndAlso Me.ServerChkJudge(ds, "InsertUnsoMData")
        Return rtnResult
        'END YANAI 要望番号1013

    End Function

    ''' <summary>
    ''' (請求)運賃の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetUnchinInsData(ByVal ds As DataSet) As DataSet


        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        'Insert
        Dim outkaLDr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)

        '2017/09/25 修正 李↓
        Dim rtnResult As Boolean
        rtnResult = Me.ChkSeiqDateUnchin(ds, outkaLDr.Item("OUTKA_PLAN_DATE").ToString(), outkaLDr.Item("ARR_PLAN_DATE").ToString(), lgm.Selector({"保存", "Save", "보존", "中国語"}), False)
        '2017/09/25 修正 李↑

        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "InsertUnchinData")
        Return ds

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' (支払)運賃の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetShiharaiInsData(ByVal ds As DataSet) As DataSet

        'Insert
        Dim outkaLDr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)
        'Dim rtnResult As Boolean = Me.ChkShiharaiDateUnchin(ds, outkaLDr.Item("OUTKA_PLAN_DATE").ToString(), outkaLDr.Item("ARR_PLAN_DATE").ToString(), "保存", False)

        'START UMANO 要望番号1369 支払運賃に伴う修正。
        If String.IsNullOrEmpty(ds.Tables("LMC020_UNSO_L").Rows(0).Item("TRIP_NO").ToString()) = True AndAlso _
           String.IsNullOrEmpty(ds.Tables("LMC020_UNSO_L").Rows(0).Item("TRIP_NO_SYUKA").ToString()) = True AndAlso _
           String.IsNullOrEmpty(ds.Tables("LMC020_UNSO_L").Rows(0).Item("TRIP_NO_TYUKEI").ToString()) = True AndAlso _
           String.IsNullOrEmpty(ds.Tables("LMC020_UNSO_L").Rows(0).Item("TRIP_NO_HAIKA").ToString()) = True Then
            Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "InsertShiharaiData")
        End If
        'END UMANO 要望番号1369 支払運賃に伴う修正。

        Return ds

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' (請求)運賃の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetUnchinDelData(ByVal ds As DataSet) As Boolean

        'Delete
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "DeleteUnchinData")
        Return rtnResult

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' (支払)運賃の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetShiharaiDelData(ByVal ds As DataSet) As Boolean

        'Delete
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "DeleteShiharaiData")
        Return rtnResult

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 作業レコードの更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="outkaNoL">出荷(大)番号：更新処理の場合、空文字を渡す</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetSagyoData(ByVal ds As DataSet, ByVal outkaNoL As String) As Boolean

        '作業データの値設定
        ds = Me.SetSagyoToOutkaNoData(ds, outkaNoL)

        '共通
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "ComSagyoData")

        Return rtnResult

    End Function

    ''' <summary>
    ''' 作業Noの設定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="outkaNoL">出荷(大)番号：更新処理の場合、空文字を渡す</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetSagyoToOutkaNoData(ByVal ds As DataSet, ByVal outkaNoL As String) As DataSet

        Dim dt As DataTable = ds.Tables("LMC020_SAGYO")
        Dim max As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To max

            'PKがない場合、設定
            If String.IsNullOrEmpty(dt.Rows(i).Item("SAGYO_REC_NO").ToString()) = True Then
                dt.Rows(i).Item("SAGYO_REC_NO") = Me.GetSagyoRecNo(ds)
            End If

            '出荷大番号が空の場合スルー(更新処理)
            If String.IsNullOrEmpty(outkaNoL) = True Then
                Continue For
            End If

            '入出荷管理番号L + Mの設定
            dt.Rows(i).Item("INOUTKA_NO_LM") = String.Concat(outkaNoL, dt.Rows(i).Item("INOUTKA_NO_LM").ToString())

        Next

        Return ds

    End Function

    '要望番号:997 terakawa 2012.10.22 Start
    ''' <summary>
    ''' EDI出荷(中)更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetOutkaEdiMData(ByVal ds As DataSet) As Boolean

        '共通
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "ComOutkaEdiMData")

        Return rtnResult

    End Function

    ''' <summary>
    ''' EDI受信DTL更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetOutkaEdiDtlData(ByVal ds As DataSet) As Boolean

        '共通
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "ComOutkaEdiDtlData")

        Return rtnResult

    End Function
    '要望番号:997 terakawa 2012.10.22 End

    '2014/01/22 輸出情報追加 START
    ''' <summary>
    ''' 出荷(小)更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetExportLData(ByVal ds As DataSet) As Boolean
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "ComExportLData")
        Return rtnResult
    End Function
    '2014/01/22 輸出情報追加 END

    '2015.07.08 協立化学　シッピング対応 追加START
    ''' <summary>
    ''' マーク(HED)更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetMarkHedData(ByVal ds As DataSet) As Boolean

        '共通
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "ComMarkHedData")

        Return rtnResult

    End Function
    '2015.07.08 協立化学　シッピング対応 追加END

    '2015.07.21 協立化学　シッピング対応 追加START
    ''' <summary>
    ''' マーク(DTL)更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetMarkDtlData(ByVal ds As DataSet) As Boolean

        '共通
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "ComMarkDtlData")

        Return rtnResult

    End Function
    '2015.07.21 協立化学　シッピング対応 追加END

    ''' <summary>
    ''' 次回分納 Rapidus出荷指示EDIデータ削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteOutkaEdiDtlRapiJikaiBunnou(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateDelFlgOutkaEdiDtlRapiJikaiBunnou")

    End Function

    ''' <summary>
    ''' 次回分納 EDI出荷L 削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteOutkaEdiLJikaiBunnou(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateDelFlgOutkaEdiL_JikaiBunnou")

    End Function

    ''' <summary>
    ''' 次回分納 EDI出荷M 削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteOutkaEdiMJikaiBunnou(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateDelFlgOutkaEdiM_JikaiBunnou")

    End Function

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 請求日のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function IsSeiqDateChk(ByVal ds As DataSet) As DataSet
        Return Me.IsSeiqDateChk(ds, ds.Tables("LMC020_KAGAMI_IN").Rows(0).Item("MSG").ToString(), , True)
    End Function

    ''' <summary>
    ''' 請求日のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="sysDelFlg">削除フラグ　初期値："0"</param>
    ''' <param name="selectFlg">運賃検索フラグ　初期値：False</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function IsSeiqDateChk(ByVal ds As DataSet, ByVal msg As String _
                                   , Optional ByVal sysDelFlg As String = "0" _
                                   , Optional ByVal selectFlg As Boolean = False _
                                   ) As DataSet

        '荷主マスタ取得
        Dim inDt As DataTable = ds.Tables("LMC020IN")
        Dim dr As DataRow = inDt.NewRow()
        Dim outkaLDr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)
        dr.Item("NRS_BR_CD") = outkaLDr.Item("NRS_BR_CD").ToString                '営業所コード
        'START YANAI 要望番号499
        'dr.Item("CUST_CD_L") = outkaLDr.Item("CUST_CD_L").ToString                '荷主コード（大）
        'dr.Item("CUST_CD_M") = outkaLDr.Item("CUST_CD_M").ToString                '荷主コード（中）
        'END YANAI 要望番号499
        inDt.Rows.Add(dr)
        Dim custInDr As DataRow = ds.Tables("LMC020IN").Rows(0)

        Dim custDs As DataSet = Nothing
        Dim custRow As DataRow = Nothing

        'START KIM 要望番号1510 2012/10/11
        If ds.Tables("LMC020_KAGAMI_IN").Rows.Count = 0 Then
            ds.Tables("LMC020_KAGAMI_IN").Rows.Add(ds.Tables("LMC020_KAGAMI_IN").NewRow())
            ds.Tables("LMC020_KAGAMI_IN").Rows(0).Item("NRS_BR_CD") = outkaLDr.Item("NRS_BR_CD").ToString
            ds.Tables("LMC020_KAGAMI_IN").Rows(0).Item("MSG") = msg
        End If
        'END KIM 要望番号1510 2012/10/11

        Dim indr As DataRow = ds.Tables("LMC020_KAGAMI_IN").Rows(0)
        Dim sql As String = String.Concat("SYS_DEL_FLG = '", sysDelFlg, "' ")
        Dim outkaMDrs As DataRow() = ds.Tables("LMC020_OUTKA_M").Select(sql)
        Dim max As Integer = outkaMDrs.Length - 1

        Dim chkDate As String = String.Empty
        Dim shukkaDate As String = outkaLDr.Item("OUTKA_PLAN_DATE").ToString()
        Dim nonyuDate As String = outkaLDr.Item("ARR_PLAN_DATE").ToString()

        Dim dt As DataTable = Nothing
        Dim sagyoDt As DataTable = ds.Tables("LMC020_SAGYO")
        Dim sagyoDr() As DataRow = sagyoDt.Select(sql)

        For i As Integer = 0 To max

            'START YANAI 要望番号499
            custInDr.Item("CUST_CD_L") = outkaMDrs(i).Item("CUST_CD_L_GOODS").ToString()
            custInDr.Item("CUST_CD_M") = outkaMDrs(i).Item("CUST_CD_M_GOODS").ToString()
            'END YANAI 要望番号499

            'S,SSを絞って抽出
            custInDr.Item("CUST_CD_S") = outkaMDrs(i).Item("CUST_CD_S").ToString()
            custInDr.Item("CUST_CD_SS") = outkaMDrs(i).Item("CUST_CD_SS").ToString()
            custDs = Me.SelectCustData(ds)
            custRow = custDs.Tables("LMC020_CUST").Rows(0)

            indr("STORAGE_SEIQTO_CD") = custRow.Item("HOKAN_SEIQTO_CD")
            indr("HANDLING_SEIQTO_CD") = custRow.Item("NIYAKU_SEIQTO_CD")
            indr("UNCHIN_SEIQTO_CD") = custRow.Item("UNCHIN_SEIQTO_CD")
            indr("SAGYO_SEIQTO_CD") = custRow.Item("SAGYO_SEIQTO_CD")
            indr("YOKOMOCHI_SEIQTO_CD") = custRow.Item("UNCHIN_SEIQTO_CD")

            '入力チェック（請求鑑取得してチェック）
            ds = Me.SelectKagamiData(ds)

            '運賃計算締め基準の値によって、チェック対象の日付を変更
            chkDate = Me.GetChkDate(custRow, shukkaDate, nonyuDate)

            '終算日チェック
            If Me.SelectSubCustDataAtDateChk(custDs, chkDate, msg) = False Then
                Return ds
            End If

            '作業料取込日(始めの行のみチェック)
            dt = ds.Tables("LMC020_SAGYO_SKYU_DATE")
            If i = 0 AndAlso 0 < dt.Rows.Count AndAlso 0 < sagyoDr.Length Then
                If Me.IsSagyoSkyuCheck(dt.Rows(0), chkDate) = False Then
                    Return ds
                End If
            End If

        Next

        '運賃をチェックしない場合、スルー
        If selectFlg = False Then
            Return ds
        End If

        '運賃取込日(始めの行のみチェック)
        Call Me.ChkSeiqDateUnchin(ds, shukkaDate, nonyuDate, msg, selectFlg)

        Return ds

    End Function

    ''' <summary>
    ''' 商品の荷主を取得し日付をチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="hokanDate">画面 起算日</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SelectSubCustDataAtDateChk(ByVal ds As DataSet, ByVal hokanDate As String, ByVal msg As String) As Boolean

        Dim custDt As DataTable = ds.Tables("LMC020_CUST")
        Dim calcDate As String = String.Empty
        If 0 < custDt.Rows.Count Then
            calcDate = custDt.Rows(0).Item("HOKAN_NIYAKU_CALCULATION").ToString()
        End If

        'START SHINODA 要望管理2165
        '起算日、最終計算日チェック
        'Return Me.IsHokanShimeChk(hokanDate, calcDate, msg)
        If Me.IsHokanShimeChk(hokanDate, calcDate, msg) = False Then
            Return False
        End If

        Dim outkaLDr As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)
        If String.IsNullOrEmpty(outkaLDr.Item("END_DATE2").ToString()) = False AndAlso String.IsNullOrEmpty(calcDate) = False Then
            'すでに終算日が最終計算日より過去の場合、取消不可
            If outkaLDr.Item("END_DATE2").ToString() <= calcDate Then
                '英語化対応
                '20151022 tsunehira add
                MyBase.SetMessage("E721")
                'MyBase.SetMessage("E375", New String() {"保管料・荷役料が既に計算されている", msg})
                Return False

            End If
        End If

        Return True
        'END SHINODA 要望管理2165

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
            '20151029 tsunehira add Start
            '英語化対応
            MyBase.SetMessage("E801", New String() {msg})
            'MyBase.SetMessage("E375", New String() {"保管料・荷役料が既に計算されている", msg})
            '20151029 tsunehira add End

            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' 作業料取込チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Private Function IsSagyoSkyuCheck(ByVal row As DataRow, ByVal checkDate As String) As Boolean

        'START YANAI No.44
        'If checkDate < row.Item("SKYU_DATE").ToString Then
        If checkDate <= row.Item("SKYU_DATE").ToString Then
            'END YANAI No.44
            '英語化対応
            '20151022 tsunehira add
            MyBase.SetMessage("E655")
            'MyBase.SetMessage("E285", New String() {"作業料"})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 請求日チェック(運賃料)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="shukkaDate">出荷日</param>
    ''' <param name="nonyuDate">納入日</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="selectFlg">検索フラグ　True:検索有　False:検索無</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkSeiqDateUnchin(ByVal ds As DataSet, ByVal shukkaDate As String, ByVal nonyuDate As String, ByVal msg As String, ByVal selectFlg As Boolean) As Boolean

        'レコードがない場合、スルー
        Dim dt As DataTable = ds.Tables("LMC020_UNSO_L")
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
            If Me.ChkDate(Me.GetChkDate(selectDt.Rows(i), shukkaDate, nonyuDate), Me.SelectGheaderData(chkDs, "SelectGheaderData"), msg, dt) = False Then
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
    Private Function ChkDate(ByVal value1 As String, ByVal value2 As String, ByVal msg As String, ByVal dt As DataTable) As Boolean

        '比較対象1に値がない場合、スルー
        If String.IsNullOrEmpty(value1) = True Then
            Return True
        End If

        'すでに締め処理が締め処理が終了している場合、エラー
        If value1 <= value2 Then

            '運賃
            If ("40").Equals(dt.Rows(0).Item("TARIFF_BUNRUI_KB").ToString()) = True Then
                '横持ちの場合
                '英語化対応
                '20151022 tsunehira add
                MyBase.SetMessage("E654")
                'MyBase.SetMessage("E285", New String() {"横持ち料", msg})
            Else
                '運賃の場合
                '英語化対応
                '20151022 tsunehira add
                MyBase.SetMessage("E653")
                'MyBase.SetMessage("E285", New String() {"運賃", msg})
            End If

            Return False

        End If

        Return True

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

        Dim dt As DataTable = ds.Tables("LMC020_UNCHIN_SKYU_DATE")
        If dt.Rows.Count < 1 Then

            Return "00000000"

        End If

        Return dt.Rows(0).Item("SKYU_DATE").ToString()

    End Function

    ''' <summary>
    ''' チェックする日付を取得
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="shukkaDate">出荷日</param>
    ''' <param name="nonyuDate">納入日</param>
    ''' <returns>チェック日付</returns>
    ''' <remarks></remarks>
    Private Function GetChkDate(ByVal dr As DataRow, ByVal shukkaDate As String, ByVal nonyuDate As String) As String

        '運賃計算締め基準の値によって、チェック対象の日付を変更
        If ("01").Equals(dr.Item("UNTIN_CALCULATION_KB").ToString()) = True Then
            GetChkDate = shukkaDate
        Else
            GetChkDate = nonyuDate
        End If

        Return GetChkDate

    End Function

    '要望番号:1350 terakawa 2012.08.27 Start
    ''' <summary>
    ''' 同一商品・LOTチェック(同一置き場に同一商品、LOTがあった場合ワーニング)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>データセット</returns>
    ''' <remarks></remarks>
    Private Function ChkGoodsLot(ByVal ds As DataSet) As DataSet

        Dim drL As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)
        Dim dtM As DataTable = ds.Tables("LMC020_OUTKA_M")
        Dim dtS As DataTable = ds.Tables("LMC020_OUTKA_S")
        Dim Count As Integer = dtS.Rows.Count - 1

        Dim setDs As DataSet = ds.Copy
        Dim setDtL As DataTable = setDs.Tables("LMC020_OUTKA_L")
        Dim setDtM As DataTable = setDs.Tables("LMC020_OUTKA_M")
        Dim setDtS As DataTable = setDs.Tables("LMC020_OUTKA_S")
        Dim setDtCustDtl As DataTable = setDs.Tables("CUST_DETAILS")
        Dim setDrM As DataRow()
        Dim worningDt As DataTable = ds.Tables("LMC020_WORNING")
        Dim worningDr As DataRow

        '荷主明細から同一置き場・商品チェック特殊荷主情報を取得
        ds = Me.DacAccess(ds, "GetCustDetail")
        Dim dtCustDtl As DataTable = ds.Tables("CUST_DETAILS")

        For i As Integer = 0 To Count
            '値のクリア
            setDs.Clear()

            '削除データの場合、チェックは行わない
            If dtS.Rows(i).Item("SYS_DEL_FLG").ToString().Equals("1") Then
                Continue For
            End If

            '関連づいている入荷Mを取得
            setDrM = dtM.Select(String.Concat("OUTKA_NO_M = '", dtS.Rows(i).Item("OUTKA_NO_M").ToString(), "'"))

            'チェック用データセットにインポート
            setDtL.ImportRow(drL)
            setDtM.ImportRow(setDrM(0))
            setDtS.ImportRow(dtS.Rows(i))
            If dtCustDtl.Rows.Count > 0 Then
                setDtCustDtl.ImportRow(dtCustDtl.Rows(0))
            End If

            '在庫データの重複チェック
            setDs = Me.DacAccess(setDs, "ChkGoodsLot")
            If GetResultCount() > 0 Then
                worningDr = ds.Tables("LMC020_WORNING").NewRow()
                With worningDr
                    .Item("GOODS_CD_CUST") = setDtM.Rows(0).Item("GOODS_CD_CUST").ToString()
                    .Item("TOU_NO") = setDtS.Rows(0).Item("TOU_NO").ToString()
                    .Item("SITU_NO") = setDtS.Rows(0).Item("SITU_NO").ToString()
                    .Item("ZONE_CD") = setDtS.Rows(0).Item("ZONE_CD").ToString()
                    .Item("LOCA") = setDtS.Rows(0).Item("LOCA").ToString()
                    .Item("LOT_NO") = setDtS.Rows(0).Item("LOT_NO").ToString()
                End With
                ds.Tables("LMC020_WORNING").Rows.Add(worningDr)
            End If
        Next

        Return ds

    End Function
    '要望番号:1350 terakawa 2012.08.27 End

    ''' <summary>
    ''' 出荷(小)と商品マスタの入目が違うものがあった場合ワーニング
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>データセット</returns>
    ''' <remarks>2019/12/16 要望管理009513 add</remarks>
    Private Function IrimeCheck(ByVal ds As DataSet) As DataSet

        Dim drL As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)
        Dim dtM As DataTable = ds.Tables("LMC020_OUTKA_M")
        Dim dtS As DataTable = ds.Tables("LMC020_OUTKA_S")
        Dim Count As Integer = dtS.Rows.Count - 1

        Dim setDs As DataSet = ds.Copy
        Dim setDtL As DataTable = setDs.Tables("LMC020_OUTKA_L")
        Dim setDtM As DataTable = setDs.Tables("LMC020_OUTKA_M")
        Dim setDtS As DataTable = setDs.Tables("LMC020_OUTKA_S")
        Dim setDrM As DataRow()

        ds.Tables("LMC020_WORNING_IRIME").Rows.Clear()

        '荷主明細から「入目相違警告チェック」の設定値を取得
        ds.Tables("CUST_DETAILS").Rows.Clear()
        ds = Me.DacAccess(ds, "GetCustDetail_9P")
        Dim dtCustDtl As DataTable = ds.Tables("CUST_DETAILS")

        '入目相違警告チェックが"チェックする"でなければ抜ける
        If dtCustDtl.Rows.Count = 0 Then
            Return ds
        Else
            If Not dtCustDtl.Rows(0).Item("SET_NAIYO").ToString().Equals("1") Then
                Return ds
            End If
        End If

        '出荷Sのループ
        For i As Integer = 0 To Count
            '値のクリア
            setDs.Clear()

            '削除データの場合、チェックは行わない
            If dtS.Rows(i).Item("SYS_DEL_FLG").ToString().Equals("1") Then
                Continue For
            End If

            '関連づいている出荷Mを取得
            setDrM = dtM.Select(String.Concat("OUTKA_NO_M = '", dtS.Rows(i).Item("OUTKA_NO_M").ToString(), "'"))

            'チェック用データセットにインポート
            setDtM.ImportRow(setDrM(0))
            setDtS.ImportRow(dtS.Rows(i))

            '入目の異なる商品マスタの件数を取得
            setDs = Me.DacAccess(setDs, "IrimeCheck")
            If GetResultCount() > 0 Then
                Dim worningDr As DataRow = ds.Tables("LMC020_WORNING_IRIME").NewRow()
                With worningDr
                    .Item("GOODS_CD_NRS") = setDtM.Rows(0).Item("GOODS_CD_NRS").ToString()
                End With
                ds.Tables("LMC020_WORNING_IRIME").Rows.Add(worningDr)
            End If
        Next

        Return ds

    End Function

#End Region

#Region "WIT対応"

    ''' <summary>
    ''' 商品管理番号採番処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateGoodsKanriNo(ByVal ds As DataSet) As Boolean

        Dim outkaLRow As DataRow = ds.Tables("LMC020_OUTKA_L").Rows(0)

        '作業用DS
        Dim setDs As DataSet = ds.Copy

        ' ハンディ対象荷主情報取得
        setDs = MyBase.CallDAC(Me._Dac, "SelectCustHandy", setDs)

        ' 商品管理番号更新対象ハンディ荷主の場合のみ処理実施
        If IsUpdateGoodsKanriNoTargetCustHandy(setDs) = False Then
            Return True
        End If

        '商品管理番号設定対象データの取得(全件)
        setDs = (MyBase.CallDAC(Me._Dac, "SelectGoodsKanriNoSrc", setDs))

        '対象データの更新処理
        For Each srcRow As DataRow In setDs.Tables("LMC020OUT_GOODS_KANRI_NO_SRC").Rows

            '作業用テーブルの初期化
            Dim setUpdDt As DataTable = setDs.Tables("LMC020IN_UPDATE_GOODS_KANRI_NO")
            setUpdDt.Clear()

            ' IN情報作成
            Dim newRow As DataRow = setUpdDt.NewRow()
            Dim custHandyRow As DataRow = setDs.Tables("LMC020OUT_CUST_HANDY").Rows(0)

            newRow("NRS_BR_CD") = srcRow("NRS_BR_CD").ToString
            newRow("ZAI_REC_NO") = srcRow("ZAI_REC_NO").ToString

            Dim newGoodsKanriNo As String = Me.CreateGoodsKanriNo(custHandyRow, srcRow)
            newRow("GOODS_KANRI_NO") = newGoodsKanriNo

            setUpdDt.Rows.Add(newRow)

            ' 更新
            setDs = (MyBase.CallDAC(Me._Dac, "UpdateGoodsKanriNo", setDs))

            ' 引数のDSの在庫レコードに新規採番した商品管理番号を設定する
            ' ※新規登録し、そのまま画面を閉じずに編集された際の対応用
            For Each zaiRow As DataRow In ds.Tables("LMC020_ZAI").Select(String.Format( _
                                                                         "NRS_BR_CD = '{0}' AND ZAI_REC_NO = '{1}'", _
                                                                         srcRow("NRS_BR_CD").ToString, _
                                                                         srcRow("ZAI_REC_NO").ToString))
                zaiRow("GOODS_KANRI_NO") = newGoodsKanriNo
            Next

        Next

        Return True

    End Function

    ''' <summary>
    ''' 商品管理番号を生成します。
    ''' </summary>
    ''' <param name="handyCustInfo"></param>
    ''' <param name="srcRow"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateGoodsKanriNo(ByVal handyCustInfo As DataRow, ByVal srcRow As DataRow) As String

        Dim format As String = handyCustInfo("S101_KBN_NM2").ToString
        Dim values As New List(Of String)

        For i As Integer = 3 To 10

            Dim kbnCol As String = String.Format("S101_KBN_NM{0}", i)
            Dim kbnValue As String = handyCustInfo(kbnCol).ToString

            If String.IsNullOrEmpty(kbnValue) = False Then
                values.Add(srcRow(kbnValue).ToString)
            Else
                '区分値が未設定の時点でループを抜ける
                Exit For
            End If

        Next

        Return String.Format(format, values.ToArray)

    End Function

    ''' <summary>
    ''' 商品管理番号更新対象ハンディ荷主かどうかを取得します。
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsUpdateGoodsKanriNoTargetCustHandy(ByVal ds As DataSet) As Boolean

        Dim outTbl As DataTable = ds.Tables("LMC020OUT_CUST_HANDY")

        ' データが存在しない場合はfalse
        If outTbl Is Nothing OrElse outTbl.Rows.Count = 0 Then
            Return False
        End If

        Dim outRow As DataRow = outTbl.Rows(0)

        ' フォーマットが存在しない場合はfalse
        If String.IsNullOrEmpty(outRow("S101_KBN_NM2").ToString) = True Then
            Return False
        End If

        ' FLG_03:商品管理番号更新対象可否フラグ
        Return [Const].LMConst.FLG.ON.Equals(outRow("FLG_03").ToString)

    End Function

    '2014.04.09 (黎) WITピッキングWK削除
    Private Function IsDeletePickWK(ByVal ds As DataSet) As Boolean
        'result(常にTrueです削除失敗でもOK[既にデータがない証拠です])
        Dim result As Boolean = True

        'データセットが空、もしくは連動削除フラグが '1' 以外の時は実行せずに正常返却(最初一行で判定)
        If ds.Tables("LMC020_OUTKA_PICK_WK").Rows.Count = 0 OrElse _
           ds.Tables("LMC020_OUTKA_PICK_WK").Rows(0).Item("ON_DELETE_FLG").ToString.Equals(LMConst.FLG.ON) = False Then

            Return result

        End If

        '実使用DS
        Dim useDs As DataSet = ds.Clone
        'テーブル
        Dim useDt As DataTable = useDs.Tables("LMC020_OUTKA_PICK_WK")
        'Row
        Dim useRow As DataRow = Nothing

        'カウンタ
        Dim useRowMax As Integer = 0

        useRowMax = ds.Tables("LMC020_OUTKA_PICK_WK").Rows.Count

        For i As Integer = 0 To useRowMax - 1
            'クリア
            useDt.Clear()

            '一行コピー
            useRow = ds.Tables("LMC020_OUTKA_PICK_WK").Rows(i)
            'DACアクセス用データセットに格納
            useDt.ImportRow(useRow)

            'DACアクセス
            Me.DacAccess(useDs, "DelPickWK")
        Next

        Return result
    End Function
    '2014.04.09 (黎) WITピッキングWK削除

#End Region

    '2018/12/07 ADD START 要望管理002171
#Region "出荷梱包個数自動計算"

    ''' <summary>
    ''' 出荷梱包個数自動計算
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaLOutkaPkgNb(ByVal ds As DataSet) As DataSet

        Dim drIn As DataRow = ds.Tables("LMC020IN").Rows(0)

        '出荷梱包個数自動計算(YCCサクラ)
        If drIn.Item("NRS_BR_CD").ToString() = "40" AndAlso drIn.Item("CUST_CD_L").ToString() = "00237" Then

            Dim dtCalkIn As DataTable = ds.Tables("LMC020_CALC_OUTKA_PKG_NB_IN")
            If dtCalkIn.Rows.Count > 0 Then

                If dtCalkIn.Rows(0).Item("AUTO_CALC_FLG").ToString() = "1" _
                AndAlso dtCalkIn.Rows(0).Item("USE_GAMEN_VALUE_FLG").ToString() = USE_GAMEN_VALUE_FALSE Then
                    '自動計算フラグ＝TRUE、かつ、画面入力値使用フラグ＝FALSEの場合のみ自動計算

                    ds.Tables("LMC020_CALC_OUTKA_PKG_NB_SAKURA").Clear()

                    Dim bErr As Boolean = False
                    ds = Me.UpdateCalcOutkaPkgNbSakura(ds, bErr)
                    If bErr Then
                        'エラー時はデータテーブルをクリア
                        ds.Tables("LMC020_CALC_OUTKA_PKG_NB_SAKURA").Clear()
                    End If

                End If
            End If
        End If

        Return ds

    End Function

    ''' <summary>
    ''' サクラ出荷梱包個数自動計算
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="bErr">エラーフラグ【参照渡し】</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateCalcOutkaPkgNbSakura(ByVal ds As DataSet, ByRef bErr As Boolean) As DataSet

        '自動計算に必要なマスタのエラーチェック
        Call MyBase.CallDAC(Me._Dac, "SelectCalcMstErrCntSakura", ds)
        If MyBase.GetResultCount() > 0 Then
            'エラーが1件でもある場合は自動計算を行わない
            bErr = True
            Return ds
        End If

        '出荷梱包個数の自動計算結果を取得
        ds = MyBase.CallDAC(Me._Dac, "SelectCalcOutkaPkgNbSakura", ds)
        '自動計算した出荷梱包個数
        Dim outkaPkgNbCalc As Integer = Convert.ToInt32(ds.Tables("LMC020_CALC_OUTKA_PKG_NB_SAKURA").Rows(0).Item("OUTKA_PKG_NB"))

        '出荷(大)テーブルの出荷梱包個数
        Dim outkaPkgNbOrg As Integer = Convert.ToInt32(ds.Tables("LMC020_OUTKA_L").Rows(0).Item("OUTKA_PKG_NB"))

        If outkaPkgNbCalc <> outkaPkgNbOrg Then
            '差異があった場合のみ自動計算した出荷梱包個数で更新
            ds.Tables("LMC020_OUTKA_L").Rows(0).Item("OUTKA_PKG_NB") = ds.Tables("LMC020_CALC_OUTKA_PKG_NB_SAKURA").Rows(0).Item("OUTKA_PKG_NB")
            ds = MyBase.CallDAC(Me._Dac, "UpdateOutkaLOutkaPkgNb", ds)
        End If

        Return ds

    End Function
#End Region
    '2018/12/07 ADD END   要望管理002171

#Region "タブレット対応"

    ''' <summary>
    '''営業所がタブレット対応かどうかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function IsWhTabNrsBrCd(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "IsWhTabNrsBrCd", ds)

    End Function

#End Region

#End Region

End Class
