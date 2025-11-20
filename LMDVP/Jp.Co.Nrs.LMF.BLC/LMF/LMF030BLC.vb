' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブ
'  プログラムID     :  LMF030    : 運行編集
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
'2015.05.27 要望番号2063 追加START
Imports Jp.Co.Nrs.LM.DSL
Imports System.Security.Cryptography
Imports System.IO
Imports System.Text

'2015.05.27 要望番号2063 追加END

''' <summary>
''' LMF030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF030BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMF030DAC = New LMF030DAC()

    '2015.05.27 要望番号2063 追加START
    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac830 As LMF830DAC = New LMF830DAC()
    '2015.05.27 要望番号2063 追加END

    Private DesKey As Byte()
    Private DesIV As Byte()


#End Region

#Region "Const"

    ''' <summary>
    ''' 運送(特大)のデータ抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_LL As String = "SelectInitUnsoLLData"

    ''' <summary>
    ''' 運送(大)のデータ抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_L As String = "SelectUnsoLInitData"

    '2015.05.27 要望番号2063 追加START
    ''' <summary>
    ''' 運送会社用EDI送信のデータ抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_SENDUNSOEDI_LM As String = "SelectSendUnsoEdiLMInitData"
    '2015.05.27 要望番号2063 追加END

    '2015.05.27 要望番号2063 追加START
    ''' <summary>
    ''' 運送会社用EDI送信のデータ(過去分)抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_PRE_SENDUNSOEDI_LM As String = "SelectPreSendUnsoEdiLMInitData"
    '2015.05.27 要望番号2063 追加END

    '2015.05.27 要望番号2063 追加START
    ''' <summary>
    ''' 運送会社用EDI送信データ(LM)の新規登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_INSERT_SENDUNSOEDI_LM As String = "InsertSendUnsoEdiLMData"
    '2015.05.27 要望番号2063 追加END

    '2015.05.27 要望番号2063 追加START
    ''' <summary>
    ''' 運送会社用EDI送信データ(LM)の新規登録(過去データ赤)アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_INSERT_DELFLG_SENDUNSOEDI_LM As String = "InsertDelFlgSendUnsoEdiLMData"
    '2015.05.27 要望番号2063 追加END

    '2015.05.27 要望番号2063 追加START
    ''' <summary>
    ''' 運送会社用EDI送信データ(LM)の更新処理アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_PRE_UPDATE_SENDUNSOEDI_LM As String = "UpdatePreSendUnsoEdiLMData"
    '2015.05.27 要望番号2063 追加END

    '2015.05.27 要望番号2063 追加START
    ''' <summary>
    ''' 運送会社用EDI送信のデータ抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_XNG_ROJITYPE As String = "SelectXngRojiInitData"
    '2015.05.27 要望番号2063 追加END

    '2015.05.27 要望番号2063 追加START
    ''' <summary>
    ''' 手配情報TBLの新規登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_INSERT_TEHAIINFO_TBL As String = "InsertTehaiInfoTblData"
    '2015.05.27 要望番号2063 追加END

    '2015.05.27 要望番号2063 追加START
    ''' <summary>
    ''' 手配情報TBLの新規登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_TEHAIINFO_TBL As String = "UpdateTehaiInfoTblData"
    '2015.05.27 要望番号2063 追加END

    '2015.05.27 要望番号2063 追加START
    ''' <summary>
    ''' 手配情報TBLの抽出件数アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_TEHAIINFO_TBL As String = "SelectTehaiInfoTblData"
    '2015.05.27 要望番号2063 追加END

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 支払運賃のデータ抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_SHIHARAI As String = "SelectShiharaiInitData"
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    ''' <summary>
    ''' 運送(特大)の新規登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_INSERT_LL As String = "InsertUnsoLLData"

    ''' <summary>
    ''' 運送(特大)の更新登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_LL As String = "UpdateUnsoLLData"

    '要望番号1269 2012.07.12 追加START umano
    ''' <summary>
    ''' 運送(大)の更新登録アクション(運行初期値更新)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_DEFAULT_L As String = "UpdateUnsoLDefaultData"
    '要望番号1269 2012.07.12 追加END umano

    ''' <summary>
    ''' 運送(大)の更新登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_L As String = "UpdateUnsoLData"

    ''' <summary>
    ''' 運送(特大)の削除登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_DELETE_LL As String = "DeleteUnsoLData"

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 運送(大)の更新登録アクション（計算時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_L_KEISAN As String = "UpdateUnsoLKeisan"

    ''' <summary>
    ''' 支払運賃の更新登録アクション（計算時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_SHIHARAI_KEISAN As String = "UpdateShiharaiKeisan"

    ''' <summary>
    ''' 運送(特大)の更新登録アクション（計算時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_LL_KEISAN As String = "UpdateUnsoLLKeisan"

    ''' <summary>
    ''' 支払運賃の更新登録アクション（保存時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_SHIHARAI As String = "UpdateShiharai"
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMF030IN"

    ''' <summary>
    ''' F_UNSO_LLテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNSO_LL As String = "F_UNSO_LL"

    ''' <summary>
    ''' F_UNSO_Lテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNSO_L As String = "F_UNSO_L"

    ''' <summary>
    ''' 配送区分(集荷)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const HAISO_SHUKA As String = "01"

    ''' <summary>
    ''' 配送区分(中継)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const HAISO_THUKEI As String = "02"

    ''' <summary>
    ''' 配送区分(配荷)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const HAISO_HAIKA As String = "03"

    ''' <summary>
    ''' 中継配送無
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TYUKEI_HAISO_FLG_OFF As String = "00"

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 初期検索の値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInitData(ByVal ds As DataSet) As DataSet

        '運送(特大)のデータ取得
        ds = Me.DacAccess(ds, LMF030BLC.ACTION_ID_SELECT_LL)

        '運送(大)のデータ取得
        ds = Me.DacAccess(ds, LMF030BLC.ACTION_ID_SELECT_L)

        'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        '支払運賃のデータ取得
        ds = Me.DacAccess(ds, LMF030BLC.ACTION_ID_SELECT_SHIHARAI)
        'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

        Return ds

    End Function

    ''' <summary>
    ''' 新規検索の値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectNewData(ByVal ds As DataSet) As DataSet

        '運送(大)のデータ取得
        ds = Me.SelectNewUnsoLData(ds)

        '運送(特大)のデータ取得
        ds = Me.SelectNewUnsoLLData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 排他チェック検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkHaitaData(ByVal ds As DataSet) As DataSet

        '運送(特大)の排他チェック
        Dim rtnResult As Boolean = Me.SelectHaitaUnsoLLData(ds)

        '運送(大)の排他チェック
        rtnResult = rtnResult AndAlso Me.SelectHaitaUnsoLData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 運送(大)レコード存在チェック用検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUnsoLData(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function


    '要望番号:1205(車番入力後、各種情報を取得する) 2012/06/29 本明 Start
    ''' <summary>
    ''' 乗務員の値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDriverData(ByVal ds As DataSet) As DataSet

        '運送(特大)のデータ取得
        ds = Me.DacAccess(ds, "SelectDriverUnsoLLData")

        Return ds

    End Function
    '要望番号:1205(車番入力後、各種情報を取得する) 2012/06/29 本明 End



    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
    ''' <summary>
    ''' 支払データの取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectShiharaiData(ByVal ds As DataSet) As DataSet

        '支払データ取得
        ds = Me.DacAccess(ds, "SelectShiharaiData")
        Return ds


    End Function
    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End

    'START KIM 要望番号1485 支払い関連修正。
    ''' <summary>
    '''  (支払)運賃の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ShiharaisakiSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function
    'END KIM 要望番号1485 支払い関連修正。

    '2022.09.06 追加START
    ''' <summary>
    ''' 車輌マスタデータ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCarData(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function
    '2022.09.06 追加END

#End Region

#Region "新規登録"

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        '採番した値を設定
        ds = Me.SetTripNo(ds, Me.GetTripNo(ds))

        '運送(特大)の新規登録
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, LMF030BLC.ACTION_ID_INSERT_LL)

        '運送(大)の更新処理
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF030BLC.ACTION_ID_UPDATE_L)

        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
        '支払運賃の更新処理
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF030BLC.ACTION_ID_UPDATE_SHIHARAI)
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End


        Return ds

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

        Dim rtnResult As Boolean = True

        '2013.08.19 要望番号2063 追加START
        'LMF830INのデータセットIN情報を設定
        Dim rtnDs As DataSet = New LMF830DS()
        Call Me.SetDataUnsoSijiInData(ds, rtnDs, "LMF830IN", "UpdateSaveAction")

        '別インスタンス
        Dim setDs As DataSet = rtnDs.Copy()
        Dim setDt As DataTable = setDs.Tables("LMF830IN")

        For i As Integer = 0 To rtnDs.Tables("LMF830IN").Rows.Count - 1

            '値のクリア
            setDs.Clear()

            '条件の設定
            setDt.ImportRow(rtnDs.Tables("LMF830IN").Rows(i))

            If setDt.Rows(0).Item("SYS_DEL_FLG").ToString().Equals("1") = True AndAlso _
                String.IsNullOrEmpty(ds.Tables("F_UNSO_LL").Rows(0).Item("TEHAI_SYUBETSU").ToString()) = False Then

                '手配指示作成対象データ(LM)を取得
                rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_SELECT_SENDUNSOEDI_LM)

                For j As Integer = 0 To setDs.Tables("LMF830OUT_LM").Rows.Count - 1

                    Dim strInput As String = String.Concat(setDs.Tables("LMF830OUT_LM").Rows(j).Item("NRS_BR_CD").ToString(), setDs.Tables("LMF830OUT_LM").Rows(j).Item("UNSO_NO_L").ToString())
                    Dim key As String
                    Dim iv As String

                    setDs.Tables("LMF830OUT_LM").Rows(j).Item("QR_CODE") = Me.encrypt(strInput, key, iv)

                Next

                '各手配種別による必要項目取得処理(運送会社用EDI送信データ(LM)の編集処理)
                Select Case setDt.Rows(0).Item("TEHAI_SYUBETSU").ToString()

                    Case "01"
                        rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_SELECT_XNG_ROJITYPE)
                        setDs = Me.SetDataUnsoLMEditData(ds, setDs, "LMF830OUT_XNG_ROJI", LMF030BLC.ACTION_ID_SELECT_XNG_ROJITYPE)

                    Case Else

                End Select

                '運送会社用EDI送信データ(LM)の新規登録
                rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_INSERT_SENDUNSOEDI_LM)

                '手配情報TBLの検索処理
                rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_SELECT_TEHAIINFO_TBL)

                If MyBase.GetResultCount = 0 Then
                    '手配情報TBLの更新処理
                    rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_INSERT_TEHAIINFO_TBL)
                Else
                    '手配情報TBLの更新処理
                    rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_UPDATE_TEHAIINFO_TBL)
                End If

            End If

        Next
        '2013.08.19 要望番号2063 追加END

        '運送(特大)の更新処理
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF030BLC.ACTION_ID_UPDATE_LL)

        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
        '要望番号1369で削除済の運送(大)もDS内に残し、Updateするように処理を変更したので以下ロジックをコメント化
        ''要望番号1269 2012.07.12 追加START umano
        ''運送(大)の更新処理(運行項目初期値更新)
        'rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF030BLC.ACTION_ID_UPDATE_DEFAULT_L)
        ''要望番号1269 2012.07.12 追加END umano
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  

        '運送(大)の更新処理
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF030BLC.ACTION_ID_UPDATE_L)

        'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        '支払運賃の更新処理
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF030BLC.ACTION_ID_UPDATE_SHIHARAI)
        'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

        Return ds

    End Function

#End Region

#Region "暗号化ロジック"

    ''' <summary>
    ''' 暗号化ロジック
    ''' </summary>
    ''' <param name="strInput">string</param>
    ''' <returns>string</returns>
    ''' <remarks></remarks>
    Private Function encrypt(ByVal strInput As String, ByRef key As String, ByRef iv As String) As String

        Dim TDES As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        DesKey = TDES.Key
        DesIV = TDES.IV

        '' バイト配列に変換
        Dim input_bytes As Byte() = System.Text.Encoding.UTF8.GetBytes(strInput)
        'Dim input_bytes As Byte() = System.Text.Encoding.Unicode.GetBytes(strInput)

        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider

        ' 入出力用のストリームを生成します 
        Dim ms As MemoryStream = New MemoryStream
        Dim cs As CryptoStream = New CryptoStream(ms, des.CreateEncryptor(DesKey, DesIV), _
                                                                    CryptoStreamMode.Write)

        ' ストリームに暗号化されたデータを書き込みます 
        cs.Write(input_bytes, 0, input_bytes.Length)
        cs.FlushFinalBlock()
        'cs.Close()

        ' 復号化されたデータを byte 配列で取得します 
        Dim destination As Byte() = ms.ToArray()
        ms.Close()

        ' byte 配列を文字列に変換して表示します 
        'Return Encoding.Unicode.GetString(destination)
        'Return Encoding.UTF8.GetString(destination)
        Return Convert.ToBase64String(destination)


    End Function

#End Region

#Region "削除登録"

    ''' <summary>
    ''' 削除登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteAction(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = True

        '2015.05.27 要望番号2063 追加START

        'LMF830INのデータセットIN情報を設定
        Dim rtnDs As DataSet = New LMF830DS()
        Call Me.SetDataUnsoSijiInData(ds, rtnDs, "LMF830IN", "DeleteAction")

        '別インスタンス
        Dim setDs As DataSet = rtnDs.Copy()
        Dim setDt As DataTable = setDs.Tables("LMF830IN")

        For i As Integer = 0 To rtnDs.Tables("LMF830IN").Rows.Count - 1

            '2015.06.23 要望番号2063 追加START
            If String.IsNullOrEmpty(ds.Tables("F_UNSO_L").Rows(i).Item("DEL_TRIP_NO").ToString()) = True Then
                Continue For
            End If
            '2015.06.23 要望番号2063 追加END

            '値のクリア
            setDs.Clear()

            '条件の設定
            setDt.ImportRow(rtnDs.Tables("LMF830IN").Rows(i))

            '手配指示作成対象データ(LM)を取得
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_SELECT_SENDUNSOEDI_LM)

            '各手配種別による必要項目取得処理(運送会社用EDI送信データ(LM)の編集処理)
            Select Case setDt.Rows(0).Item("TEHAI_SYUBETSU").ToString()

                Case "01"
                    rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_SELECT_XNG_ROJITYPE)
                    setDs = Me.SetDataUnsoLMEditData(ds, setDs, "LMF830OUT_XNG_ROJI", LMF030BLC.ACTION_ID_SELECT_XNG_ROJITYPE)

                Case Else

            End Select

            '運送会社用EDI送信データ(LM)の新規登録
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_INSERT_SENDUNSOEDI_LM)

            '手配情報TBLの検索処理
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_SELECT_TEHAIINFO_TBL)

        Next
        '2015.05.27 要望番号2063 追加END

        '運送(特大)の更新処理
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF030BLC.ACTION_ID_DELETE_LL)

        '運送(大)の更新処理
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF030BLC.ACTION_ID_UPDATE_L)

        'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        '支払運賃の更新処理
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF030BLC.ACTION_ID_UPDATE_SHIHARAI)
        'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

        Return ds

    End Function

#End Region

    '2015.05.27 要望番号2063 追加START
#Region "手配作成"

    ''' <summary>
    ''' 手配作成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertTehaiAction(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = True

        'LMF830INのデータセットIN情報を設定
        Dim rtnDs As DataSet = New LMF830DS()

        Call Me.SetDataUnsoSijiInData(ds, rtnDs, "LMF830IN", "InsertTehaiAction")

        '別インスタンス
        Dim setDs As DataSet = rtnDs.Copy()
        Dim setDt As DataTable = setDs.Tables("LMF830IN")

        For i As Integer = 0 To rtnDs.Tables("LMF830IN").Rows.Count - 1

            '値のクリア
            setDs.Clear()

            '条件の設定
            setDt.ImportRow(rtnDs.Tables("LMF830IN").Rows(i))

            '手配指示作成対象データ(LM)の過去データを取得
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_SELECT_PRE_SENDUNSOEDI_LM)

            If MyBase.GetResultCount = 0 Then

                '手配指示作成対象データ(LM)を取得
                rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_SELECT_SENDUNSOEDI_LM)

                For j As Integer = 0 To setDs.Tables("LMF830OUT_LM").Rows.Count - 1

                    Dim strInput As String = String.Concat(setDs.Tables("LMF830OUT_LM").Rows(j).Item("NRS_BR_CD").ToString(), setDs.Tables("LMF830OUT_LM").Rows(j).Item("UNSO_NO_L").ToString())
                    Dim key As String
                    Dim iv As String

                    setDs.Tables("LMF830OUT_LM").Rows(j).Item("QR_CODE") = Me.encrypt(strInput, key, iv)

                Next

                '各手配種別による必要項目取得処理(運送会社用EDI送信データ(LM)の編集処理)
                Select Case setDt.Rows(0).Item("TEHAI_SYUBETSU").ToString()

                    Case "01"
                        rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_SELECT_XNG_ROJITYPE)
                        setDs = Me.SetDataUnsoLMEditData(ds, setDs, "LMF830OUT_XNG_ROJI", LMF030BLC.ACTION_ID_SELECT_XNG_ROJITYPE)

                    Case Else

                End Select

                '運送会社用EDI送信データ(LM)の新規登録
                rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_INSERT_SENDUNSOEDI_LM)

                '手配情報TBLの検索処理
                rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_SELECT_TEHAIINFO_TBL)

                If MyBase.GetResultCount = 0 Then
                    '手配情報TBLの更新処理
                    rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_INSERT_TEHAIINFO_TBL)
                Else
                    '手配情報TBLの更新処理
                    rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_UPDATE_TEHAIINFO_TBL)
                End If

            Else
                '手配指示作成対象データ(LM)を取得
                rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_SELECT_SENDUNSOEDI_LM)

                '各手配種別による必要項目取得処理(運送会社用EDI送信データ(LM)の編集処理)
                Select Case setDt.Rows(0).Item("TEHAI_SYUBETSU").ToString()

                    Case "01"
                        rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_SELECT_XNG_ROJITYPE)
                        setDs = Me.SetDataUnsoLMEditData(ds, setDs, "LMF830OUT_XNG_ROJI", LMF030BLC.ACTION_ID_SELECT_XNG_ROJITYPE)

                    Case Else

                End Select

                '手配指示作成対象データ(LM)の過去データと今回作成対象データの整合性チェック
                setDs = Me.SetCompareLMEditData(setDs, "LMF830OUT_LM")

                Dim max As Integer = setDs.Tables("LMF830OUT_LM").Rows.Count - 1

                For k As Integer = 0 To max

                    If setDs.Tables("LMF830OUT_LM").Rows(k).Item("UPD_KBN").ToString().Equals("01") = True Then

                        '運送会社用EDI送信データ(LM)の過去データ(赤)を新規登録
                        rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_INSERT_DELFLG_SENDUNSOEDI_LM)

                        '伝送№を最新の黒になるようにカウントアップ
                        setDs.Tables("LMF830OUT_LM").Rows(k).Item("SEND_NO") = Convert.ToInt64(setDs.Tables("LMF830OUT_LM").Rows(k).Item("SEND_NO")) + 1

                        '運送会社用EDI送信データ(LM)の新規登録
                        rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_INSERT_SENDUNSOEDI_LM)

                        '手配情報TBLの更新処理
                        rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_UPDATE_TEHAIINFO_TBL)

                    ElseIf setDs.Tables("LMF830OUT_LM").Rows(k).Item("UPD_KBN").ToString().Equals("02") = True Then

                        '手配指示作成対象データ(LM)の過去データを更新処理
                        rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_PRE_UPDATE_SENDUNSOEDI_LM)

                        '手配情報TBLの更新処理
                        rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_UPDATE_TEHAIINFO_TBL)

                    ElseIf String.IsNullOrEmpty(setDs.Tables("LMF830OUT_LM").Rows(k).Item("UPD_KBN").ToString()) = True Then

                        '運送会社用EDI送信データ(LM)の過去データ(赤)を新規登録
                        rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_INSERT_DELFLG_SENDUNSOEDI_LM)

                        '手配情報TBLの検索処理
                        rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_SELECT_TEHAIINFO_TBL)

                        If MyBase.GetResultCount = 0 Then
                            '手配情報TBLの更新処理
                            rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_INSERT_TEHAIINFO_TBL)
                        Else
                            '手配情報TBLの更新処理
                            rtnResult = rtnResult AndAlso Me.ServerChkJudge(setDs, LMF030BLC.ACTION_ID_UPDATE_TEHAIINFO_TBL)
                        End If

                    End If

                Next

            End If

        Next

        Return ds

    End Function

#End Region
    '2015.05.27 要望番号2063 追加END

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
#Region "計算時の更新登録"

    ''' <summary>
    ''' 計算時の更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function KeisanSaveAction(ByVal ds As DataSet) As DataSet

        '運送(大)の更新処理
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, LMF030BLC.ACTION_ID_UPDATE_L_KEISAN)

        '支払運賃の更新処理
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF030BLC.ACTION_ID_UPDATE_SHIHARAI_KEISAN)

        '運送(特大)の更新処理
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF030BLC.ACTION_ID_UPDATE_LL_KEISAN)

        Return ds

    End Function

#End Region
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    '2015.05.27 要望番号2063 追加START
#Region "運送指示処理(LMF830INにセット)"

    ''' <summary>
    ''' 運送指示処理時に使用するDataSetを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataUnsoSijiInData(ByVal ds As DataSet, ByVal inDs As DataSet, _
                                           ByVal tblNm As String, ByVal actionId As String) As DataSet

        Dim dt As DataTable = inDs.Tables(tblNm)
        Dim dr As DataRow = dt.NewRow()
        Dim max As Integer = ds.Tables("F_UNSO_L").Rows.Count - 1

        'LMF030IN → LMF830INへ
        For i As Integer = 0 To max
            dr.Item("NRS_BR_CD") = ds.Tables("F_UNSO_L").Rows(i).Item("NRS_BR_CD").ToString
            If String.IsNullOrEmpty(ds.Tables("F_UNSO_L").Rows(i).Item("TRIP_NO").ToString) = True Then
                dr.Item("TRIP_NO") = ds.Tables("F_UNSO_L").Rows(i).Item("DEL_TRIP_NO").ToString
                ''2013.12.11 要望番号2063 追加START
                ''(※現状保留 エクシング手配の場合は運行番号を固定で"P999999999"に置き換える)
                ''dr("H_UNSOEDI_TRIP_NO") = "P999999999"
                'dr("H_UNSOEDI_TRIP_NO") = ds.Tables("F_UNSO_L").Rows(i).Item("TRIP_NO").ToString
                ''2013.12.11 要望番号2063 追加END
            ElseIf String.IsNullOrEmpty(ds.Tables("F_UNSO_L").Rows(i).Item("DEL_TRIP_NO").ToString) = False Then
                dr.Item("TRIP_NO") = ds.Tables("F_UNSO_L").Rows(i).Item("TRIP_NO").ToString
                dr.Item("ADD_TRIP_NO") = ds.Tables("F_UNSO_L").Rows(i).Item("DEL_TRIP_NO").ToString
            Else
                dr.Item("TRIP_NO") = ds.Tables("F_UNSO_L").Rows(i).Item("TRIP_NO").ToString
                ''2013.12.11 要望番号2063 追加START
                ''(※現状保留 エクシング手配の場合は運行番号を固定で"P999999999"に置き換える)
                'If ds.Tables("F_UNSO_LL").Rows(0).Item("TEHAI_SYUBETSU").ToString.Equals("01") = True Then
                '    'dr("H_UNSOEDI_TRIP_NO") = "P999999999"
                '    dr("H_UNSOEDI_TRIP_NO") = ds.Tables("F_UNSO_L").Rows(i).Item("TRIP_NO").ToString
                'Else
                '    dr("H_UNSOEDI_TRIP_NO") = ds.Tables("F_UNSO_L").Rows(i).Item("TRIP_NO").ToString
                'End If
                ''2013.12.11 要望番号2063 追加END
            End If
            dr.Item("UNSO_NO_L") = ds.Tables("F_UNSO_L").Rows(i).Item("UNSO_NO_L").ToString

            dr.Item("TEHAI_SYUBETSU") = ds.Tables("F_UNSO_LL").Rows(0).Item("TEHAI_SYUBETSU").ToString

            If actionId.Equals("DeleteAction") = True Then
                dr.Item("SYS_DEL_FLG") = "1"
            Else
                dr.Item("SYS_DEL_FLG") = ds.Tables("F_UNSO_L").Rows(i).Item("SYS_DEL_FLG").ToString
            End If

            dt.Rows.Add(dr)
            dr = dt.NewRow()
        Next

        Return inDs

    End Function

#End Region
    '2015.05.27 要望番号2063 追加END

    '2015.05.27 要望番号2063 追加START
#Region "LMF830OUT_LM編集処理"

    ''' <summary>
    ''' 各種専用項目をLMF830OUT_LMに設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="outDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataUnsoLMEditData(ByVal ds As DataSet, ByVal outDs As DataSet, _
                                           ByVal tblNm As String, ByVal actionId As String) As DataSet

        Dim dt As DataTable = outDs.Tables(tblNm)
        Dim max As Integer = outDs.Tables("LMF830OUT_LM").Rows.Count - 1
        Dim dr As DataRow


        'LMF830_XNG_ROJI → LMF830OUT_LMへ
        For i As Integer = 0 To max
            dr = outDs.Tables("LMF830OUT_LM").Rows(i)

            Dim unsoNoM As String = dr("UNSO_NO_M").ToString()
            Dim xngRojiDr As DataRow() = dt.Select(String.Concat("UNSO_NO_M = '", unsoNoM, "'"))

            '2013.12.11 要望番号2063 追加START
            '(※現状保留 エクシング手配の場合は運行番号を固定で"P999999999"に置き換える)
            'dr("TRIP_NO") = "P999999999"
            '2013.12.11 要望番号2063 追加END

            dr("CUST_ORD_NO") = xngRojiDr(0).Item("CUST_ORD_NO")
            dr("BIKO1_HED") = xngRojiDr(0).Item("MAIN_DELI_KB")
            dr("BIKO2_HED") = xngRojiDr(0).Item("TSUMIKOMI_JIKAN")
            dr("BIKO3_HED") = xngRojiDr(0).Item("TSUMIKOMI_BASYO")
            dr("BIKO4_HED") = xngRojiDr(0).Item("KIJI_REMARK_1")
            dr("UNSO_WT") = xngRojiDr(0).Item("DECI_WT")
            dr("BETU_WT") = xngRojiDr(0).Item("YOKI_WT")
            dr("REMARK_OUTKA") = xngRojiDr(0).Item("REMARK")
            dr("BIKO1_DTL") = xngRojiDr(0).Item("LAW_KBN")
            dr("BIKO2_DTL") = xngRojiDr(0).Item("HIN_GROUP")
            dr("BIKO3_DTL") = xngRojiDr(0).Item("HIN_GROUP_NM")
            dr("BIKO4_DTL") = xngRojiDr(0).Item("PKG_UT")

            dr("KYORI") = xngRojiDr(0).Item("SEIQ_KYORI")
            'dr("DECI_UNCHIN") = xngRojiDr(0).Item("DECI_UNCHIN")
            'dr("NUM1_DTL") = xngRojiDr(0).Item("NISABAKI_KIN")
            'dr("NUM2_DTL") = xngRojiDr(0).Item("SPECIAL_KIN")
            dr("DECI_UNCHIN") = 0
            dr("NUM1_DTL") = xngRojiDr(0).Item("GROSS_WEIGHT")
            dr("NUM2_DTL") = 0

        Next

        Return outDs

    End Function

#End Region
    '2015.05.27 要望番号2063 追加END

    '2015.05.27 要望番号2063 追加START
#Region "新旧比較処理(LMF830OUT_LM,LMF830OUT_LM_PRE)"

    ''' <summary>
    ''' LMF830OUT_LMとLMF830OUT_LM_PREに設定
    ''' </summary>
    ''' <param name="outDs">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetCompareLMEditData(ByVal outDs As DataSet, ByVal tblNm As String) As DataSet

        Dim dtOut As DataTable = outDs.Tables(tblNm)
        Dim dtOutPre As DataTable = outDs.Tables("LMF830OUT_LM_PRE")

        Dim max As Integer = dtOutPre.Rows.Count - 1

        Dim dr As DataRow

        'LMF830OUT_LM,LMF830OUT_LM_PREの比較
        For i As Integer = 0 To max
            dr = outDs.Tables("LMF830OUT_LM_PRE").Rows(i)

            Dim unsoNoLM As String = String.Concat(dr("UNSO_NO_L").ToString(), dr("UNSO_NO_M").ToString())
            Dim OutDr As DataRow() = dtOut.Select(String.Concat("UNSO_NO_L + UNSO_NO_M = '", unsoNoLM, "'"))

            If OutDr.Length = 0 Then
                dr("UPD_KBN") = "03"
                Continue For
            End If

            If dr("TRIP_NO").ToString() <> OutDr(0).Item("TRIP_NO").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("MOTO_DATA_KB").ToString() <> OutDr(0).Item("MOTO_DATA_KB").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("UNSO_CD").ToString() <> OutDr(0).Item("UNSO_CD").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("UNSO_BR_CD").ToString() <> OutDr(0).Item("UNSO_BR_CD").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("WH_CD").ToString() <> OutDr(0).Item("WH_CD").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("WH_NM").ToString() <> OutDr(0).Item("WH_NM").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("WH_ZIP").ToString() <> OutDr(0).Item("WH_ZIP").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("WH_AD_1").ToString() <> OutDr(0).Item("WH_AD_1").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("WH_AD_2").ToString() <> OutDr(0).Item("WH_AD_2").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("WH_AD_3").ToString() <> OutDr(0).Item("WH_AD_3").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("WH_TEL").ToString() <> OutDr(0).Item("WH_TEL").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("OUTKA_PLAN_DATE").ToString() <> OutDr(0).Item("OUTKA_PLAN_DATE").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("ARR_PLAN_DATE").ToString() <> OutDr(0).Item("ARR_PLAN_DATE").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("ARR_PLAN_TIME").ToString() <> OutDr(0).Item("ARR_PLAN_TIME").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("CUST_CD_L").ToString() <> OutDr(0).Item("CUST_CD_L").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("CUST_CD_M").ToString() <> OutDr(0).Item("CUST_CD_M").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("CUST_NM_L").ToString() <> OutDr(0).Item("CUST_NM_L").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("CUST_NM_M").ToString() <> OutDr(0).Item("CUST_NM_M").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("SHIP_NM_L").ToString() <> OutDr(0).Item("SHIP_NM_L").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("DEST_CD").ToString() <> OutDr(0).Item("DEST_CD").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("DEST_NM").ToString() <> OutDr(0).Item("DEST_NM").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("DEST_ZIP").ToString() <> OutDr(0).Item("DEST_ZIP").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("DEST_AD_1").ToString() <> OutDr(0).Item("DEST_AD_1").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("DEST_AD_2").ToString() <> OutDr(0).Item("DEST_AD_2").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("DEST_AD_3").ToString() <> OutDr(0).Item("DEST_AD_3").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("DEST_TEL").ToString() <> OutDr(0).Item("DEST_TEL").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("DEST_FAX").ToString() <> OutDr(0).Item("DEST_FAX").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("DEST_JIS_CD").ToString() <> OutDr(0).Item("DEST_JIS_CD").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("SP_NHS_KB").ToString() <> OutDr(0).Item("SP_NHS_KB").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("COA_YN").ToString() <> OutDr(0).Item("COA_YN").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("CUST_ORD_NO").ToString() <> OutDr(0).Item("CUST_ORD_NO").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("BUYER_ORD_NO").ToString() <> OutDr(0).Item("BUYER_ORD_NO").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("NHS_REMARK").ToString() <> OutDr(0).Item("NHS_REMARK").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("REMARK").ToString() <> OutDr(0).Item("REMARK").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("UNSO_ATT").ToString() <> OutDr(0).Item("UNSO_ATT").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("DENP_YN").ToString() <> OutDr(0).Item("DENP_YN").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("PC_KB").ToString() <> OutDr(0).Item("PC_KB").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("BIN_KB").ToString() <> OutDr(0).Item("BIN_KB").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("BIN_NM").ToString() <> OutDr(0).Item("BIN_NM").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("UNSO_PKG_NB").ToString() <> OutDr(0).Item("UNSO_PKG_NB").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf Convert.ToDecimal(dr("UNSO_WT").ToString()) <> Convert.ToDecimal(OutDr(0).Item("UNSO_WT").ToString()) Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("KYORI").ToString() <> OutDr(0).Item("KYORI").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf Convert.ToDecimal(dr("DECI_UNCHIN").ToString()) <> Convert.ToDecimal(OutDr(0).Item("DECI_UNCHIN").ToString()) Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("BIKO1_HED").ToString() <> OutDr(0).Item("BIKO1_HED").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("BIKO2_HED").ToString() <> OutDr(0).Item("BIKO2_HED").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("BIKO3_HED").ToString() <> OutDr(0).Item("BIKO3_HED").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("BIKO4_HED").ToString() <> OutDr(0).Item("BIKO4_HED").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("GOODS_CD_NRS").ToString() <> OutDr(0).Item("GOODS_CD_NRS").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("GOODS_NM").ToString() <> OutDr(0).Item("GOODS_NM").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("COODS_CD_CUST").ToString() <> OutDr(0).Item("COODS_CD_CUST").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("UNSO_ONDO_KB").ToString() <> OutDr(0).Item("UNSO_ONDO_KB").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("UNSO_ONDO_NM").ToString() <> OutDr(0).Item("UNSO_ONDO_NM").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("ONDO_MX").ToString() <> OutDr(0).Item("ONDO_MX").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("ONDO_MM").ToString() <> OutDr(0).Item("ONDO_MM").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("CUST_ORD_NO_DTL").ToString() <> OutDr(0).Item("CUST_ORD_NO_DTL").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("BUYER_ORD_NO_DTL").ToString() <> OutDr(0).Item("BUYER_ORD_NO_DTL").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("REMARK_OUTKA").ToString() <> OutDr(0).Item("REMARK_OUTKA").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("REMARK_UNSO").ToString() <> OutDr(0).Item("REMARK_UNSO").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("LOT_NO").ToString() <> OutDr(0).Item("LOT_NO").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("SERIAL_NO").ToString() <> OutDr(0).Item("SERIAL_NO").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf Convert.ToDecimal(dr("IRIME").ToString()) <> Convert.ToDecimal(OutDr(0).Item("IRIME").ToString()) Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("IRIME_UT").ToString() <> OutDr(0).Item("IRIME_UT").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("PKG_UT").ToString() <> OutDr(0).Item("PKG_UT").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("OUTKA_TTL_NB").ToString() <> OutDr(0).Item("OUTKA_TTL_NB").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf Convert.ToDecimal(dr("OUTKA_TTL_QT").ToString()) <> Convert.ToDecimal(OutDr(0).Item("OUTKA_TTL_QT").ToString()) Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf Convert.ToDecimal(dr("BETU_WT").ToString()) <> Convert.ToDecimal(OutDr(0).Item("BETU_WT").ToString()) Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("BIKO1_DTL").ToString() <> OutDr(0).Item("BIKO1_DTL").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("BIKO2_DTL").ToString() <> OutDr(0).Item("BIKO2_DTL").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("BIKO3_DTL").ToString() <> OutDr(0).Item("BIKO3_DTL").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("BIKO4_DTL").ToString() <> OutDr(0).Item("BIKO4_DTL").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf Convert.ToDecimal(dr("NUM1_DTL").ToString()) <> Convert.ToDecimal(OutDr(0).Item("NUM1_DTL").ToString()) Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf Convert.ToDecimal(dr("NUM2_DTL").ToString()) <> Convert.ToDecimal(OutDr(0).Item("NUM2_DTL").ToString()) Then
                OutDr(0).Item("UPD_KBN") = "01"
            ElseIf dr("SYS_DEL_FLG").ToString() <> OutDr(0).Item("SYS_DEL_FLG").ToString() Then
                OutDr(0).Item("UPD_KBN") = "01"
            Else
                OutDr(0).Item("UPD_KBN") = "00"
            End If

        Next

        Return outDs

    End Function

#End Region
    '2015.05.27 要望番号2063 追加END


#Region "チェック"

    ''' <summary>
    ''' 運送(特大)の排他検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SelectHaitaUnsoLLData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 運送(大)の排他検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SelectHaitaUnsoLData(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables(LMF030BLC.TABLE_NM_UNSO_L)
        Dim max As Integer = dt.Rows.Count - 1
        Dim chkDs As DataSet = ds.Copy
        Dim chkDt As DataTable = chkDs.Tables(LMF030BLC.TABLE_NM_UNSO_L)
        For i As Integer = 0 To max

            '値のクリア
            chkDt.Clear()

            '検索するレコードを設定
            chkDt.ImportRow(dt.Rows(i))

            '排他チェック
            If Me.ServerChkJudge(chkDs, System.Reflection.MethodBase.GetCurrentMethod.Name) = False Then
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        '2015.05.27 要望番号2063 追加START
        Select Case actionId

            Case LMF030BLC.ACTION_ID_UPDATE_TEHAIINFO_TBL, LMF030BLC.ACTION_ID_INSERT_TEHAIINFO_TBL _
                , LMF030BLC.ACTION_ID_SELECT_TEHAIINFO_TBL, LMF030BLC.ACTION_ID_SELECT_SENDUNSOEDI_LM _
                , LMF030BLC.ACTION_ID_INSERT_SENDUNSOEDI_LM, LMF030BLC.ACTION_ID_SELECT_XNG_ROJITYPE _
                , LMF030BLC.ACTION_ID_SELECT_PRE_SENDUNSOEDI_LM, LMF030BLC.ACTION_ID_PRE_UPDATE_SENDUNSOEDI_LM _
                , LMF030BLC.ACTION_ID_INSERT_DELFLG_SENDUNSOEDI_LM
                'DACアクセス
                ds = Me.DacAccess830(ds, actionId)

            Case Else
                'DACアクセス
                ds = Me.DacAccess(ds, actionId)

        End Select
        '2015.05.27 要望番号2063 追加END

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

    End Function

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

    '2015.05.27 要望番号2063 追加START
    ''' <summary>
    ''' DACクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DacAccess830(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallDAC(Me._Dac830, actionId, ds)

    End Function
    '2015.05.27 要望番号2063 追加END

    ''' <summary>
    ''' TRIP_NOを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>TripNo</returns>
    ''' <remarks></remarks>
    Private Function GetTripNo(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.TRIP_NO_LL, Me, ds.Tables(LMF030BLC.TABLE_NM_UNSO_LL).Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    ''' <summary>
    ''' 運行新規時の運送(大)レコード検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectNewUnsoLData(ByVal ds As DataSet) As DataSet

        '1行目には営業所が必ず設定してある。
        '2行目以降があるかを判定
        Dim dt As DataTable = ds.Tables(LMF030BLC.TABLE_NM_IN)
        Dim max As Integer = dt.Rows.Count - 1
        If max < 1 Then
            Return ds
        End If

        'SQL構築
        Dim sql As String = String.Concat(" AND F02_01.UNSO_NO_L IN ( '", dt.Rows(1).Item("UNSO_NO_L").ToString(), "' ")
        For i As Integer = 2 To max
            sql = String.Concat(sql, " , '", dt.Rows(i).Item("UNSO_NO_L").ToString(), "' ")
        Next
        sql = String.Concat(sql, " ) ")
        dt.Rows(0).Item("UNSO_NO_L") = sql

        'データ抽出
        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 運行新規時の運送(特大)レコード検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectNewUnsoLLData(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMF030BLC.TABLE_NM_UNSO_L)
        Dim max As Integer = dt.Rows.Count - 1
        Dim binKb As String = String.Empty
        Dim tripDate As String = String.Empty

        If -1 < max Then

            binKb = dt.Rows(0).Item("BIN_KB").ToString()
            tripDate = dt.Rows(0).Item("ARR_PLAN_DATE").ToString()

            For i As Integer = 1 To max

                '違う便区分が設定されている場合、空文字設定
                If binKb.Equals(dt.Rows(0).Item("BIN_KB").ToString()) = False Then
                    binKb = String.Empty
                    Exit For
                End If

            Next

        End If

        Dim dr As DataRow = ds.Tables(LMF030BLC.TABLE_NM_IN).Rows(0)
        dr.Item("BIN_KB") = binKb
        dr.Item("ARR_PLAN_DATE") = tripDate

        'データ抽出
        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 運行番号設定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tripNo">運行番号</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetTripNo(ByVal ds As DataSet, ByVal tripNo As String) As DataSet

        Dim dt As DataTable = ds.Tables(LMF030BLC.TABLE_NM_UNSO_L)
        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow = ds.Tables(LMF030BLC.TABLE_NM_UNSO_LL).Rows(0)
        dr.Item("TRIP_NO") = tripNo
        Dim colNm As String = Me.SetTripColNm(dr.Item("HAISO_KB").ToString())
        For i As Integer = 0 To max

            With dt.Rows(i)

                '中継配送しない場合、運行番号を設定
                If LMF030BLC.TYUKEI_HAISO_FLG_OFF.Equals(.Item("TYUKEI_HAISO_FLG").ToString()) = True Then

                    .Item("TRIP_NO") = tripNo

                Else
                    .Item(colNm) = tripNo

                End If

            End With

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 配送区分による設定先列名を取得
    ''' </summary>
    ''' <param name="haisoKbn">配送区分</param>
    ''' <returns>列名</returns>
    ''' <remarks></remarks>
    Private Function SetTripColNm(ByVal haisoKbn As String) As String

        SetTripColNm = String.Empty

        Select Case haisoKbn

            Case LMF030BLC.HAISO_SHUKA

                SetTripColNm = "TRIP_NO_SYUKA"

            Case LMF030BLC.HAISO_THUKEI

                SetTripColNm = "TRIP_NO_TYUKEI"

            Case LMF030BLC.HAISO_HAIKA

                SetTripColNm = "TRIP_NO_HAIKA"

        End Select

        Return SetTripColNm

    End Function

#End Region

End Class
