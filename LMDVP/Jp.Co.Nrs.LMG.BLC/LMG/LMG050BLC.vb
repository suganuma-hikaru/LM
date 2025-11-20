' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG050BLC : 請求処理 請求書作成
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.SAP.Call.Library
Imports Jp.Co.Nrs.SAP.Call.Library.Const
Imports Jp.Co.Nrs.SAP.Call.Library.Utility
Imports Jp.Co.Nrs.SAP.Journal.Library.Utility   'ADD 2023/04/10 依頼番号:036535

''' <summary>
''' LMG050BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG050BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMG050DAC = New LMG050DAC()

    ''' <summary>
    '''請求サブ共通DACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _CommonDac As LMG000DAC = New LMG000DAC()

#End Region

#Region "コンフィグ系"    'ADD START 2023/04/10 依頼番号:036535
    ''' <summary>
    ''' APIユーザ
    ''' </summary>
    ''' <remarks></remarks>             
    Private ReadOnly CallUser As String = Configuration.ConfigurationManager.AppSettings("SAP_CALL_API_USER")

    ''' <summary>
    ''' APIパス
    ''' </summary>
    ''' <remarks></remarks>           
    Private ReadOnly CallPass As String = Jp.Co.Nrs.EncryptTool.Utility.TripleDesMGR.Decript(Configuration.ConfigurationManager.AppSettings("SAP_CALL_API_PASS"))

    ''' <summary>
    ''' APIユーザ(EN)
    ''' </summary>
    ''' <remarks></remarks>             
    Private ReadOnly CallUserEn As String = Configuration.ConfigurationManager.AppSettings("SAP_CALL_API_USER_EN")

    ''' <summary>
    ''' APIパス(EN)
    ''' </summary>
    ''' <remarks></remarks>           
    Private ReadOnly CallPassEn As String = Jp.Co.Nrs.EncryptTool.Utility.TripleDesMGR.Decript(Configuration.ConfigurationManager.AppSettings("SAP_CALL_API_PASS_EN"))

#End Region 'ADD END 2023/04/10 依頼番号:036535

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 請求鑑ヘッダ検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        '請求鑑ヘッダ検索
        ds = MyBase.CallDAC(Me._Dac, "SelectHed", ds)

        '請求鑑詳細検索
        ds = MyBase.CallDAC(Me._Dac, "SelectDetail", ds)

        '契約通貨コード取得
        ds = MyBase.CallDAC(Me._Dac, "SelectContCurrCd", ds)

        Return ds

    End Function

    ''' <summary>
    ''' セット料金の単価マスタが登録された荷主の主請求先(であるか否か) の検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectSeiqtoCustSetPrice(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectSeiqtoCustSetPrice", ds)

        Return ds

    End Function

    ''' <summary>
    ''' TSMC請求明細よりの取込データの取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectTorikomiDataTsmc(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet = ds.Copy()
        Dim busyoCdSetClcDateSet As New HashSet(Of String)

        ' (抽出範囲の)TSMC請求明細よりの部署コードとセット料金計算日数区分の組み合わせの取得
        setDs = MyBase.CallDAC(Me._Dac, "SelectBusyoCdUnitKbInTorikomiDataTsmc", setDs)
        If setDs.Tables("LMG050DTL").Rows.Count = 0 Then
            Return ds
        End If
        For Each dr As DataRow In setDs.Tables("LMG050DTL").Rows
            busyoCdSetClcDateSet.Add(String.Concat(dr.Item("BUSYO_CD").ToString(), vbTab, dr.Item("UNIT_KB").ToString()))
        Next

        For Each dr As DataRow In ds.Tables("LMG050IN_TSMC").Rows()
            If Not busyoCdSetClcDateSet.Contains(String.Concat(dr.Item("BUSYO_CD").ToString(), vbTab, dr.Item("UNIT_KB").ToString())) Then
                ' (抽出範囲の)TSMC請求明細 に、抽出条件の部署コードとセット料金計算日数区分の組み合わせが存在しない場合
                ' 当該条件での抽出は行うまでもなく不要につき次の抽出条件へ
                Continue For
            End If

            ' TSMC請求明細よりの取込データの取得
            setDs.Tables("LMG050IN_TSMC").Clear()
            setDs.Tables("LMG050IN_TSMC").ImportRow(dr)
            setDs = MyBase.CallDAC(Me._Dac, "SelectTorikomiDataTsmc", setDs)
            If setDs.Tables("LMG050DTL").Rows.Count() > 0 Then
                ds.Tables("LMG050DTL").ImportRow(setDs.Tables("LMG050DTL").Rows(0))
            End If
        Next

        Return ds

    End Function

#End Region

#Region "チェック処理"

    ''' <summary>
    ''' 請求鑑ヘッダ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function HaitaChk(ByVal ds As DataSet) As DataSet

        '排他チェックを行う
        ds = MyBase.CallDAC(Me._Dac, "HaitaChk", ds)
        If MyBase.IsMessageExist = True Then
            Return ds
        End If

        '請求先コードマスタ存在チェックを行う
        ds = Me.ExistSeiqtoMChk(ds)

        Return ds

    End Function

    ''' <summary>
    '''  請求先マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ExistSeiqtoMChk(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "ExistSeiqtoMChk", ds)

    End Function

    '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen add start
    Private Function ExistMotoSeiqNoChk(ByVal ds As DataSet) As DataSet

        '鑑対象データの元請求書番号が設定されているかの確認を行う。
        Return MyBase.CallDAC(Me._Dac, "ExistMotoSeiqNoChk", ds)

    End Function
    '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen add end

    '2014.08.21 修正START 多通貨対応
    ''' <summary>
    '''  契約通貨コード取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectContCurrCd(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectContCurrCd", ds)

    End Function

    ''' <summary>
    '''  通貨マスタ(COM_DB)重複チェック＋契約通貨,小数点桁数取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function RepCurrChk(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "RepCurrChk", ds)

    End Function

    '2014.08.21 修正END 多通貨対応

    ''' <summary>
    ''' 新規データチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ChkNewData(ByVal ds As DataSet) As DataSet

        '請求先コードマスタ存在チェックを行う
        ds = Me.ExistSeiqtoMChk(ds)
        If MyBase.IsMessageExist = True Then
            Return ds
        End If

        '2014.08.21 修正START 多通貨対応
        '契約通貨重複チェックを行う
        ds = Me.RepCurrChk(ds)
        If ds.Tables("LMG050_CURRINFO").Rows.Count > 1 Then
            MyBase.SetMessage("E206", New String() {"請求建値", "契約建値"})
            Return ds
        End If
        '2014.08.21 修正END 多通貨対応

        '請求日関連チェックを行う
        If Me.SeiqtoDateChk(ds) = False Then
            Return ds
        End If

        'データ重複チェックを行う
        If ds.Tables("LMG050HED").Rows(0).Item("CRT_KB").ToString.Equals("00") Then

            'データ重複チェックを行う
            ds = MyBase.CallDAC(Me._Dac, "RepeatDataChk", ds)

            If MyBase.IsMessageExist = True Then
                Return ds
            End If

            '経理取込済チェックを行う
            ds = MyBase.CallDAC(Me._Dac, "KeiriTorikomiChk", ds)

            If MyBase.IsMessageExist = True Then
                Return ds
            End If

            '未来データ存在チェックを行う
            ds = MyBase.CallDAC(Me._Dac, "NextDataChk", ds)

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 請求日関連チェックを行う
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SeiqtoDateChk(ByVal ds As DataSet) As Boolean

        '請求日取得
        ds = MyBase.CallDAC(Me._Dac, "SeiqtoDateChk", ds)

        Dim dr As DataRow = ds.Tables("LMG050HED").Rows(0)
        Dim insertDate As String = dr.Item("SKYU_DATE").ToString()
        Dim day As String = insertDate.Substring(6, 2)

        Dim chkDr As DataRow = ds.Tables("LMG050_SEIQTOM").Rows(0)
        Dim clsKbn As String = chkDr.Item("CLOSE_KB").ToString()
        Dim rtnFlg As Boolean = False
        Select Case clsKbn
            Case "00"

                Dim year As Integer = Convert.ToInt32(insertDate.Substring(0, 4))
                Dim Month As Integer = Convert.ToInt32(insertDate.Substring(4, 2))
                If DateAndTime.DateSerial(year, Month + 1, 0).ToString().Substring(8, 2).Equals(day) Then
                    rtnFlg = True
                Else
                    rtnFlg = False
                End If

            Case Else

                If day.Equals(clsKbn) Then
                    rtnFlg = True
                Else
                    rtnFlg = False
                End If

        End Select

        If rtnFlg = False Then

            MyBase.SetMessage("E265", New String() {"【締日区分：" & chkDr.Item("CLOSE_KB_NM").ToString() & "】"})

        End If

        Return rtnFlg

    End Function

    ''' <summary>
    '''  請求先マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectSeiqtoData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectSeiqtoData", ds)

    End Function

#End Region

#If True Then   'ADD 2018/08/21 依頼番号 : 002136 
#Region "削除処理"

    ''' <summary>
    ''' 復活処理(請求鑑ヘッダ、請求鑑詳細復活)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function FukkatsuData(ByVal ds As DataSet) As DataSet

        '請求鑑ヘッダ復活
        ds = MyBase.CallDAC(Me._Dac, "UpFukkatsuKagamiHed", ds)

        '排他チェックでNGの場合、処理終了
        If MyBase.IsMessageExist = True Then
            Return ds
        End If

        '請求鑑詳細復活
        Return MyBase.CallDAC(Me._Dac, "UpFukkatsuKagamiDtl", ds)

    End Function

#End Region
#End If

#Region "削除処理"

    ''' <summary>
    ''' 削除処理(請求鑑ヘッダ、請求鑑詳細論理削除)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        '請求鑑ヘッダ論理削除
        ds = MyBase.CallDAC(Me._Dac, "UpDeleteKagamiHed", ds)

        '排他チェックでNGの場合、処理終了
        If MyBase.IsMessageExist = True Then
            Return ds
        End If

        '請求鑑詳細論理削除
        Return MyBase.CallDAC(Me._Dac, "UpDeleteKagamiDtl", ds)

    End Function

#End Region

#Region "ステージアップ処理"

    ''' <summary>
    ''' 請求開始日取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetInvFrom(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallDAC(Me._CommonDac, "GetInvFrom", ds)

    End Function

    ''' <summary>
    ''' 確定処理(請求鑑ヘッダ更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpKakuteiHed(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpKakuteiHed", ds)

    End Function

    ''' <summary>
    ''' ステージアップ処理(請求鑑ヘッダ更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpStageKagamiHed(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpStageKagamiHed", ds)

    End Function

#End Region

#Region "システム共通項目の更新処理"

    ''' <summary>
    ''' 更新処理(請求鑑ヘッダ、請求鑑詳細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSysData(ByVal ds As DataSet) As DataSet

        '請求鑑ヘッダシステム共通項目更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateSysDataHed", ds)

        '排他チェックでNGの場合、処理終了
        If MyBase.IsMessageExist = True Then
            Return ds
        End If

        '請求鑑詳細システム共通項目更新
        Return MyBase.CallDAC(Me._Dac, "UpdateSysDataDtl", ds)

    End Function

#End Region

#Region "未来データ取得"

    ''' <summary>
    ''' 請求鑑ヘッダ検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectNextData(ByVal ds As DataSet) As DataSet

        '請求鑑ヘッダ検索
        ds = MyBase.CallDAC(Me._Dac, "SelectNextData", ds)

        Return ds

    End Function

#End Region

#Region "保存処理"

    ''' <summary>
    ''' 新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        '請求鑑ヘッダ新規登録
        ds = MyBase.CallDAC(Me._Dac, "InsertHed", ds)

        '新規登録する明細データがない場合、処理終了
        If ds.Tables("LMG050DTL").Rows.Count = 0 Then
            Return ds
        End If

        '請求鑑詳細新規登録
        Return MyBase.CallDAC(Me._Dac, "InsertDtl", ds)

    End Function

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveData(ByVal ds As DataSet) As DataSet

        '請求鑑ヘッダ更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateHed", ds)

        '排他チェックでNGの場合、処理終了
        If MyBase.IsMessageExist = True Then
            Return ds
        End If

        '明細データがない場合、処理終了
        If ds.Tables("LMG050DTL").Rows.Count = 0 Then
            Return ds
        End If

        '新規登録、更新用データの設定
        Dim insDs As DataSet = New LMG050DS()
        Dim updDs As DataSet = New LMG050DS()
        updDs = ds.Copy()
        Me.SetUnpdateData(insDs, updDs)


        If insDs.Tables("LMG050DTL").Rows.Count > 0 Then
            '新規登録
            ds = MyBase.CallDAC(Me._Dac, "InsertDtl", insDs)
        End If

        If updDs.Tables("LMG050DTL").Rows.Count > 0 Then
            '更新登録
            ds = MyBase.CallDAC(Me._Dac, "UpdateDtl", updDs)
        End If

        Return ds

    End Function

    ''' <summary>
    '''  更新内容の再設定
    ''' </summary>
    ''' <param name="insDs">新規登録データ格納DS</param>
    ''' <param name="updDs">更新データ格納DS</param>
    ''' <remarks></remarks>
    Private Sub SetUnpdateData(ByVal insDs As DataSet, ByVal updDs As DataSet)

        Dim tableNm As String = "LMG050DTL"
        Dim insDt As DataTable = insDs.Tables(tableNm)
        Dim updDt As DataTable = updDs.Tables(tableNm)

        Dim selectDr As DataRow() = updDt.Select("INS_FLG = '1'") '新規登録用データを抽出する
        If selectDr.Length > 0 Then
            Dim max As Integer = selectDr.Length - 1
            For i As Integer = 0 To max
                '新規登録データ格納テーブルに追加する
                insDt.ImportRow(selectDr(i))
                '更新データ格納テーブルからさ駆除する
                updDt.Rows.Remove(selectDr(i))
            Next

        End If

    End Sub

#End Region

#Region "SAP処理"

    ''' <summary>
    ''' SAP出力処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SapOut(ByVal ds As DataSet) As DataSet

        ' 鑑検索よりの複数行更新時の行番号とエラー情報に設定する請求書番号の取得
        Dim rowNo As String = "0"
        Dim skyuNo As String = ""
        Dim langFlg As String = ""  'ADD 2023/04/10 依頼番号:036535
        If ds.Tables("LMG050SAPUPDIN").Rows.Count > 0 Then
            rowNo = ds.Tables("LMG050SAPUPDIN").Rows(0).Item("ROW_NO").ToString()
            langFlg = ds.Tables("LMG050SAPUPDIN").Rows(0).Item("LANG_FLG").ToString()   'ADD 2023/04/10 依頼番号:036535
            If ds.Tables("LMG050HED").Rows.Count > 0 Then
                skyuNo = ds.Tables("LMG050HED").Rows(0).Item("SKYU_NO").ToString()
            End If
        End If

        Dim cpDs As DataSet = ds.Copy
        Dim sapIfDs As SAPCallDS = New SAPCallDS
        'Dim sapClUtl As SapCallUtility = New SapCallUtility 'DEL 2023/04/10 依頼番号:036535
        'ADD START 2023/04/10 依頼番号:036535
        'SAP連携用項目
        Dim util As New SapJournalUtility()
        Dim callApi As Jp.Co.Nrs.SAP.Library.DocumentPoster
        If langFlg.Equals(CType(Jp.Co.Nrs.Com.Const.MessageType.MessageType_EN, String)) Then
            ' 英語版ユーザー
            callApi = New Jp.Co.Nrs.SAP.Library.DocumentPoster(CallUserEn, CallPassEn)
        Else
            callApi = New Jp.Co.Nrs.SAP.Library.DocumentPoster(CallUser, CallPass)
        End If
        'ADD END 2023/04/10 依頼番号:036535

        ' SAP出力処理対象取得
        Dim sapOutDs As DataSet = MyBase.CallDAC(Me._Dac, "SelectSapOut", cpDs)
        Dim sapOutDr As DataRow() = sapOutDs.Tables("LMG050SAPUPDOUT").Select()
        If sapOutDr.Count < 1 Then
            ' 対象レコード不在は排他エラーとみなす。
            If rowNo.Equals("0") Then
                MyBase.SetMessage("E011")
            Else
                MyBase.SetMessageStore("00", "E011", New String() {""}, rowNo, "請求書番号", skyuNo)
            End If
            Return ds
        Else
            ' 対象レコードがあれば先に進捗区分を更新（排他＆行ロックにより二重連携抑制）

            ' 請求鑑ヘッダ更新（進捗区分更新／ステージアップ処理同様）
            ds = MyBase.CallDAC(Me._Dac, "UpStageKagamiHed", cpDs)

            ' 排他チェックでNGの場合、処理終了
            If MyBase.IsMessageExist = True Then
                If Not rowNo.Equals("0") Then
                    MyBase.SetMessageStore("00", "E011", New String() {""}, rowNo, "請求書番号", skyuNo)
                End If
                Return ds
            End If
        End If

        sapIfDs.Tables(SapCallConst.TABLE_NM_REV_HEAD).Clear()
        sapIfDs.Tables(SapCallConst.TABLE_NM_REV_ITEM).Clear()
        ' REV_ITEM データテーブルにて、内税の明細の税抜き金額の最大値の行を特定するための行番号列の暫定追加
        Dim tmpColNmGyoNoSuffix As Integer = -1
        Dim tmpColNmGyoNo As String
        Do
            ' 万一 SAP側の仕様変更で REV_ITEM データテーブルに "GYO_NO" 列が追加された場合は、
            ' 項目名を重複させないために、"GYO_NO_n" のサフィックス付き項目名とする。
            ' n: 明細行数の桁数分の前ゼロ付き連番(※)
            '    ※最小値1から同名の列が存在しない番号までカウントアップした値
            tmpColNmGyoNoSuffix += 1
            tmpColNmGyoNo = "GYO_NO" &
                If(tmpColNmGyoNoSuffix = 0, "", "_" & tmpColNmGyoNoSuffix.ToString().PadLeft(sapOutDr.Count().ToString().Length, "0"c))
            If Not sapIfDs.Tables(SapCallConst.TABLE_NM_REV_ITEM).Columns.Contains(tmpColNmGyoNo) Then
                sapIfDs.Tables(SapCallConst.TABLE_NM_REV_ITEM).Columns.Add(tmpColNmGyoNo)
                Exit Do
            End If
        Loop
        Dim outHedDr As DataRow = sapIfDs.Tables(SapCallConst.TABLE_NM_REV_HEAD).NewRow()
        Dim outItmDr As DataRow
        Dim invAmt As Decimal = 0
        Dim maxUchizeiItemAmt As Decimal = 0
        Dim maxUchizeiIndex As String = ""
        Dim uchizeiAmtTtl As Decimal = 0
        Dim itemAmtTtl As Decimal = 0
        For i As Integer = 0 To sapOutDr.Count - 1
            Dim inDr As DataRow = sapOutDr(i)
            If i = 0 Then
                outHedDr.Item("SYS_ID") = "LM"
                outHedDr.Item("INV_NO") = inDr.Item("SKYU_NO").ToString()
                outHedDr.Item("INV_TO_BP") = inDr.Item("NRS_KEIRI_CD2").ToString()
                outHedDr.Item("HEAD_TXT") = inDr.Item("REMARK").ToString().Substring(0, Math.Min(inDr.Item("REMARK").ToString().Length, 25))
                outHedDr.Item("COMP_CD") = inDr.Item("COMP_CD").ToString()
                outHedDr.Item("INV_DATE") = inDr.Item("SKYU_DATE").ToString()
                outHedDr.Item("INV_PIC_NM") = inDr.Item("USER_NM").ToString().Substring(0, Math.Min(inDr.Item("USER_NM").ToString().Length, 12))
                outHedDr.Item("INV_DEPT_NM") = inDr.Item("INV_DEPT_NM").ToString()
                outHedDr.Item("INV_CURR_CD") = inDr.Item("INV_CURR_CD").ToString()
                invAmt += Decimal.Parse(inDr.Item("TAX_GK1").ToString())
                invAmt += Decimal.Parse(inDr.Item("TAX_HASU_GK1").ToString())
            End If
            outItmDr = sapIfDs.Tables(SapCallConst.TABLE_NM_REV_ITEM).NewRow()
            outItmDr.Item(tmpColNmGyoNo) = (i + 1).ToString()
            outItmDr.Item("INV_NO") = inDr.Item("SKYU_NO").ToString()
            outItmDr.Item("ACC_CD") = inDr.Item("KANJO_KAMOKU_CD").ToString()
            outItmDr.Item("DTL_TXT") = inDr.Item("TEKIYO").ToString().Substring(0, Math.Min(inDr.Item("TEKIYO").ToString().Length, 50))
            outItmDr.Item("REV_DEPT_CD") = inDr.Item("KEIRI_BUMON_CD").ToString()
            outItmDr.Item("PART_BP") = inDr.Item("NRS_KEIRI_CD2").ToString()
            outItmDr.Item("TAX_CD") = inDr.Item("TAX_CD_JDE").ToString()
            outItmDr.Item("ITEM_CURR_CD") = inDr.Item("ITEM_CURR_CD").ToString()
            Dim itemAmt As Decimal = 0
            itemAmt += Decimal.Parse(inDr.Item("KEISAN_TLGK").ToString())
            itemAmt -= Decimal.Parse(inDr.Item("NEBIKI_RTGK").ToString())
            itemAmt -= Decimal.Parse(inDr.Item("NEBIKI_GK").ToString())
            invAmt += itemAmt
            If inDr.Item("TAX_KB").ToString().Equals("04") Then
                ' 内税の明細の計 の保持
                uchizeiAmtTtl += itemAmt

                ' 内税の明細の場合、明細の金額は税抜き計算を行う
                ' ヘッダ税額との誤差はAPIより先で調整するため、明細単位で小数点以下四捨五入のみの端数処理とする。
                itemAmt = Math.Round(itemAmt / (1 + Decimal.Parse(inDr.Item("TAX_RATE").ToString())), 0, MidpointRounding.AwayFromZero)

                If maxUchizeiItemAmt < itemAmt Then
                    ' 内税の明細の税抜き金額の最大値(および同行番号) の保持
                    maxUchizeiItemAmt = itemAmt
                    maxUchizeiIndex = outItmDr.Item(tmpColNmGyoNo).ToString()
                End If
            End If
            outItmDr.Item("ITEM_AMT") = SapKingakuMarume(itemAmt.ToString())
            itemAmtTtl += Decimal.Parse(outItmDr.Item("ITEM_AMT").ToString())
            outItmDr.Item("JOB_NO") = inDr.Item("SKYU_NO").ToString()
            outItmDr.Item("REAL_CUST_BP") = inDr.Item("TCUST_BPCD").ToString()
            outItmDr.Item("SEG_GOODS_CD") = inDr.Item("PRODUCT_SEG_CD").ToString()
            outItmDr.Item("SEG_FROM_CD") = inDr.Item("ORIG_SEG_CD").ToString()
            outItmDr.Item("SEG_TO_CD") = inDr.Item("DEST_SEG_CD").ToString()
            sapIfDs.Tables(SapCallConst.TABLE_NM_REV_ITEM).Rows.Add(outItmDr)
            If i = sapOutDr.Count - 1 Then
                outHedDr.Item("INV_AMT") = SapKingakuMarume(invAmt.ToString())
                outHedDr.Item("RB_KBN") = IIf(inDr.Item("RB_FLG").ToString() = "00", "", "00001")
                sapIfDs.Tables(SapCallConst.TABLE_NM_REV_HEAD).Rows.Add(outHedDr)
            End If
        Next

        ' 鑑編集のヘッダ部『税額: e』 行の「内税分」相当額 算出
        Dim uchizeiTaxLMS As Decimal = Math.Truncate(
            uchizeiAmtTtl * Decimal.Parse(sapOutDr(0).Item("TAX_RATE").ToString()) / (Decimal.Parse(sapOutDr(0).Item("TAX_RATE").ToString()) + 1))
        ' 鑑編集のヘッダ部『税額: e, f』 行の「課税分」相当額
        Dim kaZeiTaxLMS As Decimal =
            Decimal.Parse(sapOutDr(0).Item("TAX_GK1").ToString()) + Decimal.Parse(sapOutDr(0).Item("TAX_HASU_GK1").ToString())
        ' 内税計算時の誤差 = (鑑編集のヘッダ部『税額: e, f』 行の「課税分」+「内税分」) - ( (REV_HEAD)INV_AMT - ( (REV_ITEM)ITEM_AMT )の計 )
        Dim uchizeiGosa As Decimal = (kaZeiTaxLMS + uchizeiTaxLMS) - (invAmt - itemAmtTtl)
        If uchizeiGosa <> 0 Then
            ' 内税計算時の誤差ありの場合
            ' 内税の明細の税抜き金額の最大値よりその値を減ずる
            For i As Integer = 0 To sapIfDs.Tables(SapCallConst.TABLE_NM_REV_ITEM).Rows.Count() - 1
                If Decimal.Parse(sapIfDs.Tables(SapCallConst.TABLE_NM_REV_ITEM).Rows(i).Item("ITEM_AMT").ToString()) = maxUchizeiItemAmt _
                    AndAlso sapIfDs.Tables(SapCallConst.TABLE_NM_REV_ITEM).Rows(i).Item(tmpColNmGyoNo).ToString() = maxUchizeiIndex Then
                    sapIfDs.Tables(SapCallConst.TABLE_NM_REV_ITEM).Rows(i).Item("ITEM_AMT") =
                        SapKingakuMarume((maxUchizeiItemAmt - uchizeiGosa).ToString())
                    Exit For
                End If
            Next
        End If
        ' REV_ITEM データテーブルに暫定追加した行番号列の除去
        sapIfDs.Tables(SapCallConst.TABLE_NM_REV_ITEM).Columns.Remove(tmpColNmGyoNo)

        'If Not sapClUtl.SapCallAR(Me, sapIfDs) Then    'DEL 2023/04/10 依頼番号:036535
        If util.SapJournalAR(Me, sapIfDs, callApi) = False Then 'ADD 2023/04/10 依頼番号:036535
            If rowNo.Equals("0") Then
                MyBase.SetMessage("E547", New String() {"SAP出力処理"})
            Else
                MyBase.SetMessageStore("00", "E547", New String() {"SAP出力処理"}, rowNo, "請求書番号", skyuNo)
            End If

            'ADD START 2023/04/10 依頼番号:036535
            'SAPロールバック
            callApi.TransactionRollback()
            'ADD END 2023/04/10 依頼番号:036535
            Return ds
        End If

        'メッセージクリア
        MyBase.SetMessage(Nothing)

        ' SAPから返却されたSAP伝票番号を取得
        cpDs.Tables("RESULT_SAP").Clear()
        Dim sapRow As DataRow = cpDs.Tables("RESULT_SAP").NewRow()
        sapRow.Item("SAP_NO") = sapIfDs.Tables(SapCallConst.TABLE_NM_RESULT_SAP).Rows(0).Item("SAP_NO").ToString()
        sapRow.Item("SAP_OUT_USER") = GetUserID()
        cpDs.Tables("RESULT_SAP").Rows.Add(sapRow)

        ' 請求鑑ヘッダ更新（SAP伝票番号／排他処理は済んでいるのでここでは無条件更新）
        ds = MyBase.CallDAC(Me._Dac, "UpSapNoKagamiHed", cpDs)

        ' 更新対象レコードがなかった場合、処理終了
        If MyBase.IsMessageExist = True Then
            If Not rowNo.Equals("0") Then
                MyBase.SetMessageStore("00", "E011", New String() {""}, rowNo, "請求書番号", skyuNo)
            End If

            'ADD START 2023/04/10 依頼番号:036535
            'SAPロールバック
            callApi.TransactionRollback()
            'ADD END 2023/04/10 依頼番号:036535
            Return ds
        End If

        'ADD START 2023/04/10 依頼番号:036535
        'SAPコミット
        callApi.TransactionCommit()
        'ADD END 2023/04/10 依頼番号:036535

        Return ds

    End Function

    ''' <summary>
    ''' SAP取消処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SapCancel(ByVal ds As DataSet) As DataSet

        ' 鑑検索よりの複数行更新時の行番号とエラー情報に設定する請求書番号の取得
        Dim rowNo As String = "0"
        Dim skyuNo As String = ""
        If ds.Tables("LMG050SAPUPDIN").Rows.Count > 0 Then
            rowNo = ds.Tables("LMG050SAPUPDIN").Rows(0).Item("ROW_NO").ToString()
            If ds.Tables("LMG050HED").Rows.Count > 0 Then
                skyuNo = ds.Tables("LMG050HED").Rows(0).Item("SKYU_NO").ToString()
            End If
        End If

        'メッセージクリア
        MyBase.SetMessage(Nothing)

        ' 請求鑑ヘッダ更新
        ' （ステージアップ処理同様の進捗区分更新）
        ds = MyBase.CallDAC(Me._Dac, "UpStageKagamiHed", ds)

        ' 排他チェックでNGの場合、処理終了
        If MyBase.IsMessageExist = True Then
            If Not rowNo.Equals("0") Then
                MyBase.SetMessageStore("00", "E011", New String() {""}, rowNo, "請求書番号", skyuNo)
            End If
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' SAP金額端数処理
    ''' </summary>
    ''' <param name="kingaku">端数処理対象金額文字列</param>
    ''' <returns>端数処理後の金額文字列</returns>
    Private Function SapKingakuMarume(ByVal kingaku As String) As String

        Dim dotPos As Integer = kingaku.IndexOf(".")
        Dim seisuBu As String
        Dim shosuBu As String

        If dotPos < 0 Then
            ' 小数点を含まない場合
            seisuBu = kingaku
            shosuBu = "00"
        Else
            ' 小数点を含む場合
            seisuBu = kingaku.Substring(0, dotPos)
            shosuBu = kingaku.Substring(dotPos + 1, kingaku.Length - (dotPos + 1))
        End If
        ' 対象文字列の小数点以下が2桁となるように編集して返す。
        Return String.Concat(seisuBu, ".", String.Concat(shosuBu, "00").Substring(0, 2))

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

    ''' <summary>
    ''' 地域セグメント取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>コンボボックスの選択肢として利用</remarks>
    Private Function SelectComboChiiki(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectComboChiiki", ds)

    End Function

    ''' <summary>
    ''' セグメント初期値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>行追加時の初期値として利用</remarks>
    Private Function SelectDefSeg(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectDefSeg", ds)

    End Function

#End Region

#End Region

End Class
