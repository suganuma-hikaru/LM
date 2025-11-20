' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC903  : 佐川e飛伝Ⅲ CSV出力
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports System.Text
Imports System.IO

''' <summary>
''' LMC903ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMC903H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconV As LMCControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconH As LMCControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconG As LMCControlG

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Prm As LMFormData

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

#End Region 'Field

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        Me._Prm = prm

        '画面間データを取得する
        Me._PrmDs = prm.ParamDataSet()

        'CSV出力処理
        Me.MakeCSV(Me._PrmDs)

        '出荷(大)の更新
        Me.UpdateYamatoB2Csv(Me._PrmDs)

    End Sub

#End Region ' "初期処理"

#Region "Method"

    ''' <summary>
    ''' CSV作成
    ''' </summary>
    ''' <param name="prmDs">DataSet</param>
    ''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    Private Sub MakeCsv(ByVal prmDs As DataSet)

        Dim setDs As DataSet = prmDs.Copy()
        Dim setOutDt As DataTable = setDs.Tables("LMC903OUT")
        setOutDt.Clear()

        ' 並べ替え
        Dim outDr As DataRow() = prmDs.Tables("LMC903OUT").Select(Nothing, "BINSYU_HINMEI, HINMEI1")
        Dim max As Integer = outDr.Length - 1
        For i As Integer = 0 To max
            setOutDt.ImportRow(outDr(i))
        Next

        ' CSV出力処理
        Dim setData As StringBuilder = New StringBuilder()
        max = setOutDt.Rows.Count - 1
        For i As Integer = 0 To max
            With setOutDt.Rows(i)
                setData.Append(SetDblQuotation(.Item("TODOKESAKI_CD_GET_KB").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("TODOKESAKI_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_TEL").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_ZIP").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_AD_1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_AD_2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_AD_3").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_NM_1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_NM_2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("OKYAKU_KANRI_NO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("OKYAKU_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("BUSYO_TANTOUSYA_CD_GET_KB").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("BUSYO_TANTOUSYA_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("BUSYO_TANTOUSYA").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_TEL").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOIRAINUSHI_CD_GET_KB").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOIRAINUSHI_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOIRAINUSHI_TEL").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOIRAINUSHI_ZIP").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOIRAINUSHI_AD1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOIRAINUSHI_AD2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOIRAINUSHI_NM1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOIRAINUSHI_NM2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NISUGATA_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HINMEI1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HINMEI2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HINMEI3").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HINMEI4").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HINMEI5").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIFUDA_NISUGATA").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIFUDA_HINMEI1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIFUDA_HINMEI2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIFUDA_HINMEI3").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIFUDA_HINMEI4").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIFUDA_HINMEI5").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIFUDA_HINMEI6").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIFUDA_HINMEI7").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIFUDA_HINMEI8").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIFUDA_HINMEI9").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIFUDA_HINMEI10").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIFUDA_HINMEI11").ToString()))
                setData.Append(",")
                setData.Append(.Item("SYUKKA_KOSU").ToString())
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("BINSYU_SPEED").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("BINSYU_HINMEI").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HAITATSU_BI").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HAITATSU_JIKANTAI").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HAITATSU_JIKAN").ToString()))
                setData.Append(",")
                setData.Append(.Item("DAIBIKI_KINGAKU").ToString())
                setData.Append(",")
                setData.Append(.Item("TAX").ToString())
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KESSAI_SYUBETSU").ToString()))
                setData.Append(",")
                setData.Append(.Item("HOKEN_KINGAKU").ToString())
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SEAL1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SEAL2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SEAL3").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("EIGYOTENDOME").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SRC_KBN").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("EIGYOTEN_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("MOTOCHAKU_KBN").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("EMAIL_ADDRESSS").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("FUZAIJI_TEL").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUKKA_BI").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("TOIAWASE_DENPYO_NO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUKKABA_PRINT_KB").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUYAKU_KAIJO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("EDIT01").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("EDIT02").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("EDIT03").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("EDIT04").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("EDIT05").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("EDIT06").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("EDIT07").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("EDIT08").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("EDIT09").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("EDIT10").ToString()))
                setData.Append(vbNewLine)
            End With
        Next

        ' 保存先のCSVファイルのパス
        Dim csvPath As String = String.Concat(prmDs.Tables("LMC903OUT").Rows(0).Item("FILEPATH").ToString, _
                                              prmDs.Tables("LMC903OUT").Rows(0).Item("FILENAME").ToString, _
                                              prmDs.Tables("LMC903OUT").Rows(0).Item("SYS_DATE").ToString, _
                                               "-", _
                                              Left(prmDs.Tables("LMC903OUT").Rows(0).Item("SYS_TIME").ToString, 6), _
                                              ".csv")

        ' CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        ' ファイルを開く
        System.IO.Directory.CreateDirectory(prmDs.Tables("LMC903OUT").Rows(0).Item("FILEPATH").ToString)
        Dim sr As StreamWriter = New StreamWriter(csvPath, False, enc)

        ' 値の設定
        sr.Write(setData.ToString())

        ' ファイルを閉じる
        sr.Close()

    End Sub

    ''' <summary>
    ''' ダブルコーテーション付加
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDblQuotation(ByVal val As String) As String

        Return String.Concat("""", val, """")

    End Function

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="ds">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function UpdateYamatoB2Csv(ByVal ds As DataSet) As DataSet

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC903BLF", "UpdateSagawaEHiden3Csv", ds)

        Return rtnDs

    End Function

#End Region ' "Method"

End Class