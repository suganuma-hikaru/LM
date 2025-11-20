' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM080V : 運送会社マスタメンテ
'  作  成  者       :  hirayama
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports System.Collections
Imports System.Collections.Generic
Imports System.Linq

''' <summary>
''' LMM080Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM080V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM080F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMMControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMMControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM080F, ByVal v As LMMControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = v

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "行削除チェック"

    ''' <summary>
    ''' 行削除チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function RowDelChk(ByVal arr As ArrayList) As Boolean

        'チェックが入った行の削除不可チェック
        Dim rtnResult As Boolean = Me.IsRowNullDeleteCheck(arr)

        Return rtnResult


    End Function

    ''' <summary>
    ''' 削除不可チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsRowNullDeleteCheck(ByVal arr As ArrayList) As Boolean

        With Me._Frm

            'チェックした数
            Dim chkCnt As Integer = arr.Count - 1
            'スプレッドの行数
            Dim rowMax As Integer = .sprDetail2.ActiveSheet.Rows.Count - 1

            Dim rowNo As Integer = 0
            Dim colNo As Integer = LMM080G.sprDetailDef2.CUST_CD_L.ColNo

            'スプレッド全行チェック時はtrue
            If rowMax = chkCnt Then
                Return True
            End If
#If False Then '名鉄対応(2499) 2016.2.1 changed inoue
            For i As Integer = 0 To chkCnt

                rowNo = Convert.ToInt32(arr(i))

                '空行を1行のみ削除しようとしたときエラー
                If rowMax = 1 _
                AndAlso chkCnt < 1 _
                AndAlso String.IsNullOrEmpty(Me.ChkRowExistCustCdL(rowNo, colNo)) = True Then

                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E320", New String() {"荷主コード空白行はセット入力", "1行のみの削除"})
                    MyBase.ShowMessage("E867")
                    '2016.01.06 UMANO 英語化対応END
                    Return False

                End If


                '荷主コードありの行が存在していて空行を削除しようとしたときエラー
                If rowMax > 1 _
                AndAlso String.IsNullOrEmpty(Me.ChkRowExistCustCdL(rowNo, colNo)) = True Then
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E375", New String() {"荷主固有の送り状が存在している", "削除"})
                    MyBase.ShowMessage("E868")
                    '2016.01.06 UMANO 英語化対応END
                    Return False

                End If

            Next
#Else

            ' 荷主が設定された荷札の設定行数を取得
            Dim ptnId As String = String.Empty
            Dim custCdL As String = String.Empty
            Dim isCustCdLEmpty As Boolean = False

            ' 行数(パターンID別)
            Dim rowCount As Dictionary(Of String, Int32) = Me.CountUnsoCustRptRow()

            ' 削除行数(パターンID別)
            Dim deleteCount As Dictionary(Of String, Int32) = Me.CountDeleteUnsoCustRptRow(arr)

            For Each row As Int32 In arr

                Dim rowIndex As Int32 = Convert.ToInt32(row)

                isCustCdLEmpty = String.IsNullOrEmpty(Me.ChkRowExistCustCdL(rowIndex, colNo))

                If (isCustCdLEmpty = True) Then
                    ' 荷主コード(大)未設定行

                    ' 削除行のパターンID取得
                    ptnId = Me._Vcon.GetCellValue(Me._Frm.sprDetail2.ActiveSheet.Cells(rowIndex, LMM080G.sprDetailDef2.PTN_ID.ColNo)).ToString()

                    If (deleteCount(ptnId) <= 1) Then
                        MyBase.ShowMessage("E867")
                        Return False
                    End If

                    If (rowCount(ptnId) > deleteCount(ptnId)) Then
                        If (ptnId.Equals(LMM080C.TAG_PTNID)) Then
                            MyBase.ShowMessage("E905")
                        Else
                            MyBase.ShowMessage("E868")
                        End If
                        Return False

                    End If

                    End If
            Next
#End If
        End With

        Return True

    End Function

    ''' <summary>
    ''' チェックした行に荷主コードの入力があるかチェック
    ''' </summary>
    ''' <param name="rowNo">チェックした行</param>
    ''' <param name="colNo">荷主コード(大)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkRowExistCustCdL(ByVal rowNo As Integer, ByVal colNo As Integer) As String

        Dim cellCustCdL As String = Me._Vcon.GetCellValue(Me._Frm.sprDetail2.ActiveSheet.Cells(rowNo, colNo)).ToString()

        Return cellCustCdL

    End Function


#If True Then '名鉄対応(2499) 2016.2.1 added inoue

    Private Function CountUnsoCustRptRow() As Dictionary(Of String, Int32)

        ' Key:PtnId, Value:件数
        Dim countRowByPtdId As New Dictionary(Of String, Int32)()

        Dim ptnId As String = String.Empty

        With Me._Frm.sprDetail2.ActiveSheet

            For i As Int32 = 0 To .Rows.Count - 1

                ptnId = Me._Vcon.GetCellValue(.Cells(i, LMM080C.SprColumnIndex2.PTN_ID)).ToString()

                If (countRowByPtdId.Keys.Contains(ptnId)) Then
                    countRowByPtdId(ptnId) += 1
                Else
                    countRowByPtdId.Add(ptnId, 1)
                End If
            Next
        End With

        Return countRowByPtdId

    End Function

    Private Function CountDeleteUnsoCustRptRow(ByVal deleteRowsIndexes As ArrayList) As Dictionary(Of String, Int32)

        ' Key:PtnId, Value:件数
        Dim countRowByPtdId As New Dictionary(Of String, Int32)()

        Dim ptnId As String = String.Empty

        With Me._Frm.sprDetail2.ActiveSheet

            For Each index As Integer In deleteRowsIndexes
                ptnId = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(index), LMM080C.SprColumnIndex2.PTN_ID)).ToString()

                If (countRowByPtdId.Keys.Contains(ptnId)) Then
                    countRowByPtdId(ptnId) += 1
                Else
                    countRowByPtdId.Add(ptnId, 1)
                End If
            Next
        End With

        Return countRowByPtdId

    End Function

    Private Function CountCheckedRowsByPtnId(ByVal ptnId As String) As Int32

        Dim counter As Int32 = 0
        Dim celCustCdL As String = String.Empty
        Dim celPtnId As String = String.Empty

        With Me._Frm.sprDetail2.ActiveSheet

            For i As Int32 = 0 To .Rows.Count - 1

                celPtnId = Me._Vcon.GetCellValue(.Cells(i, LMM080C.SprColumnIndex2.PTN_ID)).ToString()
                celCustCdL = Me._Vcon.GetCellValue(.Cells(i, LMM080C.SprColumnIndex2.CUST_CD_L)).ToString()

                If (ptnId.Equals(celPtnId) AndAlso String.IsNullOrEmpty(celCustCdL) = False) Then
                    counter += 1
                End If
            Next

        End With
        Return counter

    End Function

#End If

#End Region

    ''' <summary>
    ''' 入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSaveInputChk() As Boolean

        'trim
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        Dim rtnResult As Boolean = Me.IsSaveSingleCheck()

        '時間コントロールのフルバイト数チェック
        rtnResult = rtnResult AndAlso Me.IsTimeControlCheck()

        'マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsMstExistChk()

        '明細チェック
        rtnResult = rtnResult AndAlso Me.IsSprDetailChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 保存時のtrim
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm

            .txtUnsocoCd.TextValue = .txtUnsocoCd.TextValue.Trim()
            .txtUnsocoBrCd.TextValue = .txtUnsocoBrCd.TextValue.Trim()
            .txtUnsocoNm.TextValue = .txtUnsocoNm.TextValue.Trim()
            .txtUnsocoBrNm.TextValue = .txtUnsocoBrNm.TextValue.Trim()
            .txtZip.TextValue = .txtZip.TextValue.Trim()
            .txtAd1.TextValue = .txtAd1.TextValue.Trim()
            .txtAd2.TextValue = .txtAd2.TextValue.Trim()
            .txtAd3.TextValue = .txtAd3.TextValue.Trim()
            .txtTel.TextValue = .txtTel.TextValue.Trim()
            .txtFax.TextValue = .txtFax.TextValue.Trim()
            .txtURL.TextValue = .txtURL.TextValue.Trim()
            .txtPic.TextValue = .txtPic.TextValue.Trim()
            .txtNrsSbetuCd.TextValue = .txtNrsSbetuCd.TextValue.Trim()
            '(2012.08.17)支払サブ機能対応 --- START ---
            .txtShiharaitoCd.TextValue = .txtShiharaitoCd.TextValue.Trim()
            '(2012.08.17)支払サブ機能対応 ---  END  ---
            .txtUnchinTariffCd.TextValue = .txtUnchinTariffCd.TextValue.Trim()
            .txtExtcTariffCd.TextValue = .txtExtcTariffCd.TextValue.Trim()
            .txtKyoriCd.TextValue = .txtKyoriCd.TextValue.Trim()
            '要望番号:1275 yamanaka 2012.07.13 Start
            .txtRyakumei.TextValue = .txtRyakumei.TextValue.Trim()
            '要望番号:1275 yamanaka 2012.07.13 End

        End With

    End Sub

    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSingleCheck() As Boolean

        With Me._Frm

            '**********編集部のチェック

            '営業所
            '2016.01.06 UMANO 英語化対応START
            '.cmbNrsBrCd.ItemName = "営業所"
            .cmbNrsBrCd.ItemName = .LmTitleLabel12.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If

            '運送会社コード
            '2016.01.06 UMANO 英語化対応START
            '.txtUnsocoCd.ItemName = "運送会社コード"
            .txtUnsocoCd.ItemName = .lblTitleUnsoco.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtUnsocoCd.IsHissuCheck = True
            .txtUnsocoCd.IsForbiddenWordsCheck = True
            .txtUnsocoCd.IsByteCheck = 5
            .txtUnsocoCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtUnsocoCd) = False Then
                Return False
            End If

            '運送会社名
            '2016.01.06 UMANO 英語化対応START
            '.txtUnsocoNm.ItemName = "運送会社名"
            .txtUnsocoNm.ItemName = .lblTitleUnsoco.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtUnsocoNm.IsHissuCheck = True
            .txtUnsocoNm.IsForbiddenWordsCheck = True
            .txtUnsocoNm.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtUnsocoNm) = False Then
                Return False
            End If

            '運送会社支店コード
            '2016.01.06 UMANO 英語化対応START
            '.txtUnsocoBrCd.ItemName = "運送会社支店コード"
            .txtUnsocoBrCd.ItemName = .lblTitleUnsocoBr.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtUnsocoBrCd.IsHissuCheck = True
            .txtUnsocoBrCd.IsForbiddenWordsCheck = True
            .txtUnsocoBrCd.IsByteCheck = 3
            .txtUnsocoBrCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtUnsocoBrCd) = False Then
                Return False
            End If

            '運送会社支店名
            '2016.01.06 UMANO 英語化対応START
            '.txtUnsocoBrNm.ItemName = "運送会社支店名"
            .txtUnsocoBrNm.ItemName = .lblTitleUnsocoBr.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtUnsocoBrNm.IsHissuCheck = True
            .txtUnsocoBrNm.IsForbiddenWordsCheck = True
            .txtUnsocoBrNm.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtUnsocoBrNm) = False Then
                Return False
            End If

            '運送会社区分
            '2016.01.06 UMANO 英語化対応START
            '.cmbUnsocoKb.ItemName = "運送会社区分"
            .cmbUnsocoKb.ItemName = .lblTitleUnsocoKb.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbUnsocoKb.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbUnsocoKb) = False Then
                Return False
            End If

            '郵便番号
            '2016.01.06 UMANO 英語化対応START
            '.txtZip.ItemName = "郵便番号"
            .txtZip.ItemName = .lblTitleZip.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtZip.IsForbiddenWordsCheck = True
            .txtZip.IsByteCheck = 10
            .txtZip.IsHankakuCheck = True
            If MyBase.IsValidateCheck(.txtZip) = False Then
                Return False
            End If

            '住所1
            '2016.01.06 UMANO 英語化対応START
            '.txtAd1.ItemName = "住所1"
            .txtAd1.ItemName = .lblTitleAd1.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtAd1.IsForbiddenWordsCheck = True
            .txtAd1.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtAd1) = False Then
                Return False
            End If

            '住所2
            '2016.01.06 UMANO 英語化対応START
            '.txtAd2.ItemName = "住所2"
            .txtAd2.ItemName = .lblTitleAd2.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtAd2.IsForbiddenWordsCheck = True
            .txtAd2.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtAd2) = False Then
                Return False
            End If

            '住所3
            '2016.01.06 UMANO 英語化対応START
            '.txtAd3.ItemName = "住所3"
            .txtAd3.ItemName = .LmTitleLabel17.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtAd3.IsForbiddenWordsCheck = True
            .txtAd3.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtAd3) = False Then
                Return False
            End If

            '元請区分
            '2016.01.06 UMANO 英語化対応START
            '.cmbMotoukeKb.ItemName = "元請区分"
            .cmbMotoukeKb.ItemName = .LmTitleLabel21.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbMotoukeKb.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbMotoukeKb) = False Then
                Return False
            End If

            '(2012.08.17)支払サブ機能対応 --- START ---
            '支払先コード
            '2016.01.06 UMANO 英語化対応START
            '.txtShiharaitoCd.ItemName = "支払先コード"
            .txtShiharaitoCd.ItemName = .lblTitleSharaito.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtShiharaitoCd.IsForbiddenWordsCheck = True
            .txtShiharaitoCd.IsByteCheck = 8
            If MyBase.IsValidateCheck(.txtShiharaitoCd) = False Then
                Return False
            End If
            '(2012.08.17)支払サブ機能対応 ---  END  ---

            '支払用運賃タリフコード
            '2016.01.06 UMANO 英語化対応START
            '.txtUnchinTariffCd.ItemName = "支払用運賃タリフコード"
            .txtUnchinTariffCd.ItemName = String.Concat(.pnlCost.Text(), .lblTitleUnchin.Text())
            '2016.01.06 UMANO 英語化対応END
            .txtUnchinTariffCd.IsForbiddenWordsCheck = True
            .txtUnchinTariffCd.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtUnchinTariffCd) = False Then
                Return False
            End If

            '支払用割増運賃タリフコード
            '2016.01.06 UMANO 英語化対応START
            '.txtExtcTariffCd.ItemName = "支払用割増運賃タリフコード"
            .txtExtcTariffCd.ItemName = String.Concat(.pnlCost.Text(), .lblTitleUnchin.Text())
            '2016.01.06 UMANO 英語化対応END
            .txtExtcTariffCd.IsForbiddenWordsCheck = True
            .txtExtcTariffCd.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtExtcTariffCd) = False Then
                Return False
            End If

            '支払用距離程コード
            '2016.01.06 UMANO 英語化対応START            
            '.txtKyoriCd.ItemName = "支払用距離程コード"
            .txtKyoriCd.ItemName = String.Concat(.pnlCost.Text(), .lblTitleKyori.Text())
            '2016.01.06 UMANO 英語化対応END
            .txtKyoriCd.IsForbiddenWordsCheck = True
            .txtKyoriCd.IsByteCheck = 3
            If MyBase.IsValidateCheck(.txtKyoriCd) = False Then
                Return False
            End If

            '電話番号
            '2016.01.06 UMANO 英語化対応START
            '.txtTel.ItemName = "電話番号"
            .txtTel.ItemName = .LmTitleLabel11.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtTel.IsForbiddenWordsCheck = True
            .txtTel.IsByteCheck = 20
            .txtTel.IsHankakuCheck = True
            If MyBase.IsValidateCheck(.txtTel) = False Then
                Return False
            End If

            'FAX番号
            '2016.01.06 UMANO 英語化対応START
            '.txtFax.ItemName = "FAX番号"
            .txtFax.ItemName = .LmTitleLabel5.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtFax.IsForbiddenWordsCheck = True
            .txtFax.IsByteCheck = 20
            .txtFax.IsHankakuCheck = True
            If MyBase.IsValidateCheck(.txtFax) = False Then
                Return False
            End If

            '問合URL
            '2016.01.06 UMANO 英語化対応START
            '.txtURL.ItemName = "問合URL"
            .txtURL.ItemName = .LmTitleLabel25.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtURL.IsForbiddenWordsCheck = True
            .txtURL.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtURL) = False Then
                Return False
            End If

            '担当者
            '2016.01.06 UMANO 英語化対応START
            '.txtPic.ItemName = "担当者"
            .txtPic.ItemName = .LmTitleLabel23.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtPic.IsForbiddenWordsCheck = True
            .txtPic.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtPic) = False Then
                Return False
            End If

            '日陸識別コード
            '2016.01.06 UMANO 英語化対応START
            '.txtNrsSbetuCd.ItemName = "日陸識別コード"
            .txtNrsSbetuCd.ItemName = .LmTitleLabel26.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtNrsSbetuCd.IsForbiddenWordsCheck = True
            .txtNrsSbetuCd.IsByteCheck = 7
            If MyBase.IsValidateCheck(.txtNrsSbetuCd) = False Then
                Return False
            End If

            '荷札印刷
            '2016.01.06 UMANO 英語化対応START
            '.cmbNihudaYn.ItemName = "荷札印刷"
            .cmbNihudaYn.ItemName = .LmTitleLabel18.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbNihudaYn.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNihudaYn) = False Then
                Return False
            End If

            '風袋重量加算
            '2016.01.06 UMANO 英語化対応START
            '.cmbTareYn.ItemName = "風袋重量加算"
            .cmbTareYn.ItemName = .LmTitleLabel19.Text()
            '2016.01.06 UMANO 英語化対応END
            .cmbTareYn.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbTareYn) = False Then
                Return False
            End If

            '要望番号:1275 yamanaka 2012.07.13 Start
            '運送会社略名
            '2016.01.06 UMANO 英語化対応START
            '.txtRyakumei.ItemName = "運送会社略名"
            .txtRyakumei.ItemName = .LmTitleLabel1.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtRyakumei.IsForbiddenWordsCheck = True
            .txtRyakumei.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtRyakumei) = False Then
                Return False
            End If
            '要望番号:1275 yamanaka 2012.07.13 End

            '要望番号:2408 2015.09.17 追加START
            If .chkAutoDenp.Checked = True Then
                '自動送状区分
                '2016.01.06 UMANO 英語化対応START
                '.cmbAutoDenpKbn.ItemName = "自動送状区分"
                .cmbAutoDenpKbn.ItemName = .chkAutoDenp.Text()
                '2016.01.06 UMANO 英語化対応END
                .cmbAutoDenpKbn.IsHissuCheck = True
                If MyBase.IsValidateCheck(.cmbAutoDenpKbn) = False Then
                    Return False
                End If
            End If
            '要望番号:2408 2015.09.17 追加END

        End With

        Return True

    End Function


    ''' <summary>
    ''' 時間コントロールフルバイト数チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsTimeControlCheck() As Boolean

        Dim lastPuTime As String = String.Empty
        Dim byteCnt As Integer = 0
        Dim sysjis As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        lastPuTime = Me._Frm.dtpLastPuTime.TextValue
        byteCnt = sysjis.GetByteCount(lastPuTime)

        If byteCnt <> 4 _
        AndAlso byteCnt <> 0 Then
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage("E038", New String() {"最終集荷時間", "4"})
            MyBase.ShowMessage("E038", New String() {Me._Frm.LmTitleLabel24.Text(), "4"})
            '2016.01.06 UMANO 英語化対応END
            Return False
        End If

        Return True

    End Function


#Region "存在チェック"

    ''' <summary>
    ''' マスタ存在チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsMstExistChk() As Boolean

        With Me._Frm

            'START UMANO 要望番号1387 支払処理修正。
            '支払運賃タリフマスタ存在チェック
            Dim rtnResult As Boolean = Me.IsShiharaiTariffExistChk(.txtUnchinTariffCd, .lblUnshinTariffRem)

            '支払割増運賃タリフマスタ存在チェック
            rtnResult = rtnResult AndAlso Me.IsShiharaiExtcTariffExistChk(.txtExtcTariffCd, .lblExtcTariffRem)
            'END UMANO 要望番号1387 支払処理修正。

            '距離程マスタ存在チェック
            rtnResult = rtnResult AndAlso Me.IsKyoriExistChk(.txtKyoriCd, .lblKyoriRem)

            Return rtnResult

        End With

    End Function

    'START UMANO 要望番号1387 支払処理修正。
    ''' <summary>
    ''' 支払運賃タリフマスタ存在チェック
    ''' </summary>
    ''' <param name="ctlCd">マスタ存在チェックを行う項目</param>
    ''' <param name="ctlNm">名称設定項目</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    Private Function IsShiharaiTariffExistChk(ByVal ctlCd As Win.InputMan.LMImTextBox _
                                        , ByVal ctlNm As Win.InputMan.LMImTextBox) As Boolean

        With Me._Frm

            Dim checkValue As String = ctlCd.TextValue

            '名称項目を空にする
            ctlNm.TextValue = String.Empty

            '値がない場合、スルー
            If String.IsNullOrEmpty(checkValue) = True Then
                Return True
            End If

            '名称取得用テーブル
            Dim ExistChkDr As DataRow() = Nothing

            'マスタ存在チェックを行う
            If Me.SelectShiharaiTariffListDataRow(ExistChkDr, checkValue) = False Then
                Call Me._Vcon.SetErrorControl(ctlCd)
                Return False
            Else
                If ExistChkDr IsNot Nothing Then
                    ctlCd.TextValue = ExistChkDr(0).Item("SHIHARAI_TARIFF_CD").ToString()
                    ctlNm.TextValue = ExistChkDr(0).Item("SHIHARAI_TARIFF_REM").ToString()
                End If
            End If

            Return True

        End With

    End Function
    'END UMANO 要望番号1387 支払処理修正。

    'START UMANO 要望番号1387 支払処理修正。
    ''' <summary>
    ''' 運賃タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="drs">データロウ配列</param>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectShiharaiTariffListDataRow(ByRef drs As DataRow() _
                                                  , ByVal tariffCd As String _
                                                  ) As Boolean

        'キャッシュテーブルからデータ抽出
        drs = Me._Vcon.SelectShiharaiTariffListDataRow(tariffCd)

        '取得できない場合、エラー
        If drs.Length < 1 Then
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage("E079", New String() {"支払運賃タリフマスタ", tariffCd})
            MyBase.ShowMessage("E695")
            '2016.01.06 UMANO 英語化対応END
            Return False
        End If

        Return True

    End Function
    'END UMANO 要望番号1387 支払処理修正。

    'START UMANO 要望番号1387 支払処理修正。
    ''' <summary>
    ''' 支払割増運賃タリフマスタ存在チェック
    ''' </summary>
    ''' <param name="ctlCd">マスタ存在チェックを行う項目</param>
    ''' <param name="ctlNm">名称設定項目</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsShiharaiExtcTariffExistChk(ByVal ctlCd As Win.InputMan.LMImTextBox _
                                        , ByVal ctlNm As Win.InputMan.LMImTextBox) As Boolean

        With Me._Frm

            Dim brCd As String = .cmbNrsBrCd.SelectedValue.ToString()
            Dim checkValue As String = ctlCd.TextValue

            '名称項目を空にする
            ctlNm.TextValue = String.Empty

            '値がない場合、スルー
            If String.IsNullOrEmpty(checkValue) = True Then
                Return True
            End If

            '名称取得用テーブル
            Dim ExistChkDr As DataRow() = Nothing

            'マスタ存在チェックを行う
            If Me._Vcon.SelectExtcShiharaiListDataRow(ExistChkDr, brCd, checkValue) = False Then
                Call Me._Vcon.SetErrorControl(ctlCd)
                Return False
            Else
                If ExistChkDr IsNot Nothing Then
                    ctlCd.TextValue = ExistChkDr(0).Item("EXTC_TARIFF_CD").ToString()
                    ctlNm.TextValue = ExistChkDr(0).Item("EXTC_TARIFF_REM").ToString()
                End If
            End If

            Return True

        End With

    End Function
    'END UMANO 要望番号1387 支払処理修正。

    ''' <summary>
    ''' 距離程マスタ存在チェック
    ''' </summary>
    ''' <param name="ctlCd">マスタ存在チェックを行う項目</param>
    ''' <param name="ctlNm">名称設定項目</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKyoriExistChk(ByVal ctlCd As Win.InputMan.LMImTextBox _
                                        , ByVal ctlNm As Win.InputMan.LMImTextBox) As Boolean

        With Me._Frm

            Dim brCd As String = .cmbNrsBrCd.SelectedValue.ToString()
            Dim checkValue As String = ctlCd.TextValue

            '名称項目を空にする
            ctlNm.TextValue = String.Empty

            '値がない場合、スルー
            If String.IsNullOrEmpty(checkValue) = True Then
                Return True
            End If

            '名称取得用テーブル
            Dim ExistChkDr As DataRow() = Nothing

            'マスタ存在チェックを行う
            If Me._Vcon.SelectKyoriListDataRow(ExistChkDr, brCd, checkValue) = False Then
                Call Me._Vcon.SetErrorControl(ctlCd)
                Return False
            Else
                If ExistChkDr IsNot Nothing Then
                    ctlCd.TextValue = ExistChkDr(0).Item("KYORI_CD").ToString()
                    ctlNm.TextValue = ExistChkDr(0).Item("KYORI_REM").ToString()
                End If
            End If

            Return True

        End With

    End Function

#End Region

#Region "明細チェック"

    ''' <summary>
    ''' 明細チェック(保存時)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSprDetailChk() As Boolean

        '元着払区分存在チェック
        Dim rtnResult As Boolean = Me.IsMotoTyakKbExistChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 元着払区分存在チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsMotoTyakKbExistChk() As Boolean

        With Me._Frm.sprDetail2.ActiveSheet

            Dim sprMax As Integer = .Rows.Count - 1

            Dim motoTyakKb As Integer = LMM080G.sprDetailDef2.MOTO_TYAKU_KB.ColNo

            For i As Integer = 0 To sprMax

                If String.IsNullOrEmpty(Me._Vcon.GetCellValue(.Cells(i, motoTyakKb))) = True Then
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E001", New String() {"元着払区分"})
                    MyBase.ShowMessage("E001", New String() {LMM080G.sprDetailDef2.MOTO_TYAKU_KB.ColName})
                    '2016.01.06 UMANO 英語化対応END
                    Call Me._Vcon.SetErrorControl(Me._Frm.sprDetail2, i, motoTyakKb)
                    Return False
                End If

            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 枝番号限界値チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsEdabanChk(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables(LMM080C.TABLE_NM_CUSTRPT)

        Dim moto As String = LMM080C.MOTO
        Dim motoMsg As String = LMM080C.MOTOKBN

        Dim chaku As String = LMM080C.CHAKU
        Dim chakMsg As String = LMM080C.CHAKUKBN

        '元払い
        Dim rtnResult As Boolean = Me.IsMotoChakuKbnCntChk(dt, moto, motoMsg)
        '着払い
        rtnResult = rtnResult AndAlso Me.IsMotoChakuKbnCntChk(dt, chaku, chakMsg)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 元着払区分限界値チェック(カウント)
    ''' </summary>
    ''' <param name="dt">データテーブル</param>
    ''' <param name="kbn">元着払区分</param>
    ''' <param name="msg">メッセージ置換文字</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsMotoChakuKbnCntChk(ByVal dt As DataTable _
                                              , ByVal kbn As String _
                                              , ByVal msg As String) As Boolean

        Dim drs As DataRow() = dt.Select(Me.IsMotoChakuKbnCnt(kbn))

        If 1000 < drs.Length Then

            '2016.01.06 UMANO 英語化対応START
            If kbn.Equals(LMM080C.MOTO) = True Then
                'MyBase.ShowMessage("E237", New String() {String.Concat(LMM080C.MOTOCHAKUKBN _
                '                                       , msg _
                '                                       , "枝番が限界")})
                MyBase.ShowMessage("E869")
            ElseIf kbn.Equals(LMM080C.CHAKU) = True Then
                'MyBase.ShowMessage("E237", New String() {String.Concat(LMM080C.MOTOCHAKUKBN _
                '                       , msg _
                '                       , "枝番が限界")})
                MyBase.ShowMessage("E870")
            End If
            '2016.01.06 UMANO 英語化対応END

            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 元着払区分Select文生成
    ''' </summary>
    ''' <param name="kbn">元着払区分</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsMotoChakuKbnCnt(ByVal kbn As String) As String

        Return String.Concat(LMM080C.MOTOTYAKUKB, " = ", kbn)

    End Function

    ''' <summary>
    ''' 明細複数件チェック
    ''' </summary>
    ''' <param name="ds">保存データセット</param>
    ''' <returns>同一荷主コード(大)(中)元着払区分で、パターン名が複数入力されている場合エラー</returns>
    ''' <remarks></remarks>
    Friend Function IsFukusuChk(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables(LMM080C.TABLE_NM_CUSTRPT)
        Dim max As Integer = dt.Rows.Count - 1
        Dim drs As DataRow() = Nothing

        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty
        Dim motoTyakKb As String = String.Empty
#If True Then ' 名鉄対応(2499) 2016.2.1 added inoue
        Dim ptnId As String = String.Empty
#End If

        For i As Integer = 0 To max

            motoTyakKb = dt.Rows(i).Item(LMM080C.MOTOTYAKUKB).ToString()
            custCdL = dt.Rows(i).Item(LMM080C.CUSTCDL).ToString()
            custCdM = dt.Rows(i).Item(LMM080C.CUSTCDM).ToString()
#If False Then ' 名鉄対応(2499) 2016.2.1 changed inoue
            drs = dt.Select(String.Concat(LMM080C.MOTOTYAKUKB, " = ", "'", motoTyakKb, "'" _
                                          , " AND ", LMM080C.CUSTCDL, " = ", "'", custCdL, "'" _
                                          , " AND ", LMM080C.CUSTCDM, " = ", "'", custCdM, "'"))
#Else
            ptnId = dt.Rows(i).Item(LMM080C.PTN_ID).ToString()
            drs = dt.Select(String.Concat(LMM080C.MOTOTYAKUKB, " = ", "'", motoTyakKb, "'" _
                                          , " AND ", LMM080C.PTN_ID, " = ", "'", ptnId, "'" _
                                          , " AND ", LMM080C.CUSTCDL, " = ", "'", custCdL, "'" _
                                          , " AND ", LMM080C.CUSTCDM, " = ", "'", custCdM, "'"))
#End If
            If drs.Length > 1 Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E294", New String() {"同一荷主コード(大)･荷主コード(中)･元着払区分", "パターン名"})
                MyBase.ShowMessage("E294")
                '2016.01.06 UMANO 英語化対応END
                Return False
            End If

        Next

        Return True

    End Function

#End Region

    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsRecordStatusChk() As Boolean

        If Me._Frm.lblSituation.RecordStatus = RecordStatus.DELETE_REC Then
            MyBase.ShowMessage("E035")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 他営業所チェック
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <returns>True;エラーなし,False;エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsUserNrsBrCdChk(ByVal eventShubetsu As LMM080C.EventShubetsu) As Boolean

        'ユーザーのログイン営業所と異なる場合エラー
        If Me._Frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then

            Dim msg As String = String.Empty

            Select Case eventShubetsu

                Case LMM080C.EventShubetsu.HENSHU
                    '2016.01.06 UMANO 英語化対応START
                    'msg = "編集"
                    msg = Me._Frm.FunctionKey.F2ButtonName
                    '2016.01.06 UMANO 英語化対応END

                Case LMM080C.EventShubetsu.HUKUSHA
                    '2016.01.06 UMANO 英語化対応START
                    'msg = "複写"
                    msg = Me._Frm.FunctionKey.F3ButtonName
                    '2016.01.06 UMANO 英語化対応END

                Case LMM080C.EventShubetsu.SAKUJO
                    '2016.01.06 UMANO 英語化対応START
                    'msg = "削除・復活"
                    msg = Me._Frm.FunctionKey.F4ButtonName
                    '2016.01.06 UMANO 英語化対応END

            End Select

            MyBase.ShowMessage("E178", New String() {msg})
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' スプレッドでチェックの付いたRowIndexを取得
    ''' </summary>
    ''' <returns>リスト</returns>
    ''' <remarks>チェックのある行のRowIndexをリストに入れて戻す</remarks>
    Friend Function SprSelectCount() As ArrayList

        Dim defNo As Integer = LMM080G.sprDetailDef.DEF.ColNo

        With Me._Frm.sprDetail.ActiveSheet

            Dim list As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If Me._Vcon.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    list.Add(i)
                End If
            Next

            Return list

        End With

    End Function

    ''' <summary>
    ''' スプレッド(明細)行存在チェック
    ''' </summary>
    ''' <returns>true:行あり,false:行なし</returns>
    ''' <remarks></remarks>
    Friend Function SprRowExistChk() As Boolean

#If False Then '名鉄対応(2499) 2016.2.1 changed inoue
        With Me._Frm

            Dim sprMax As Integer = .sprDetail2.ActiveSheet.Rows.Count - 1

            If sprMax < 0 Then

                Return False

            End If

        End With

        Return True
#Else
        With Me._Frm
            ' 追加する帳票種別IDを取得
            Dim addPtnId As String = .cmbAddPtnId.SelectedValue.ToString()

            ' 全行の帳票種別IDを確認
            For i As Integer = 0 To .sprDetail2.ActiveSheet.Rows.Count - 1
                If (addPtnId.Equals(.sprDetail2.ActiveSheet.Cells(i, LMM080C.SprColumnIndex2.PTN_ID).Value)) Then
                    ' 一件でも存在すれば、Trueを返して終了
                    Return True
                End If
            Next
        End With

        Return False
#End If

    End Function

    ''' <summary>
    ''' 検索押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuInputChk() As Boolean

        'Trimチェック

        '検索
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0, Me._Frm.sprDetail.ActiveSheet.Columns.Count - 1)

        '単項目チェック
        If Me.IsKensakuSingleCheck() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 検索押下時スプレッド単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleCheck() As Boolean

        With Me._Frm

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            '運送会社コード
            vCell.SetValidateCell(0, LMM080G.sprDetailDef.UNSOCO_CD.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "運送会社コード"
            vCell.ItemName = LMM080G.sprDetailDef.UNSOCO_CD.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 5
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '運送会社支店コード
            vCell.SetValidateCell(0, LMM080G.sprDetailDef.UNSOCO_BR_CD.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "運送会社支店コード"
            vCell.ItemName = LMM080G.sprDetailDef.UNSOCO_BR_CD.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 3
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '運送会社名
            vCell.SetValidateCell(0, LMM080G.sprDetailDef.UNSOCO_NM.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "運送会社名"
            vCell.ItemName = LMM080G.sprDetailDef.UNSOCO_NM.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '運送会社支店名
            vCell.SetValidateCell(0, LMM080G.sprDetailDef.UNSOCO_BR_NM.ColNo)
            '2016.01.06 UMANO 英語化対応START
            'vCell.ItemName = "運送会社支店名"
            vCell.ItemName = LMM080G.sprDetailDef.UNSOCO_BR_NM.ColName
            '2016.01.06 UMANO 英語化対応END
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM080C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM080C.EventShubetsu.SHINKI           '新規
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM080C.EventShubetsu.HENSHU          '編集
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM080C.EventShubetsu.HUKUSHA          '複写
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select


            Case LMM080C.EventShubetsu.SAKUJO          '削除・復活
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM080C.EventShubetsu.KENSAKU         '検索
                '50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM080C.EventShubetsu.MASTEROPEN          'マスタ参照
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM080C.EventShubetsu.HOZON           '保存
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM080C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM080C.EventShubetsu.DCLICK         'ダブルクリック
                '50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case LMM080C.EventShubetsu.ENTER          'Enter
                '50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        If kengenFlg = True Then
            Return True
        Else
            MyBase.ShowMessage("E016")
        End If

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMM080C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If objNm Is Nothing = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM080C.EventShubetsu.MASTEROPEN) = True Then
                Call Me._Vcon.SetFocusErrMessage(False)
            End If
            Return False
        End If

        '判定するコントロール設定先変数
        Dim ctl As Win.InputMan.LMImTextBox() = Nothing
        Dim msg As String() = Nothing
        Dim clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl() = Nothing
        Dim focusCtl As Control = Me._Frm.ActiveControl

        With Me._Frm

            Select Case objNm

                Case .txtZip.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtZip}
                    msg = New String() {.lblTitleZip.Text}

                Case .txtUnchinTariffCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtUnchinTariffCd}
                    msg = New String() {.lblTitleUnchin.Text, LMMControlC.CD}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblUnshinTariffRem}

                Case .txtExtcTariffCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtExtcTariffCd}
                    msg = New String() {.lblTitleExtcUnchin.Text, LMMControlC.CD}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblExtcTariffRem}

                Case .txtKyoriCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtKyoriCd}
                    msg = New String() {.lblTitleKyori.Text, LMMControlC.CD}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblKyoriRem}

                    '(2012.08.17)支払サブ機能対応 --- START ---
                Case .txtShiharaitoCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtShiharaitoCd}
                    msg = New String() {.lblTitleSharaito.Text, LMMControlC.CD}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblShiharaitoNm}
                    '(2012.08.17)支払サブ機能対応 ---  END  ---

            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), ctl, msg, focusCtl, clearCtl)

        End With

    End Function

    ''' <summary>
    ''' エラー項目の背景色とフォーカスを設定する(時間コントロール用)
    ''' </summary>
    ''' <param name="errorCtl">エラーコントロール</param>
    ''' <param name="focusCtl">フォーカス設定コントロール</param>
    ''' <remarks></remarks>
    Friend Sub SetErrorControl(ByVal errorCtl As Control() _
                               , ByVal focusCtl As Control _
                               )

        Dim errorColor As System.Drawing.Color = Utility.LMGUIUtility.GetAttentionBackColor()

        Dim max As Integer = errorCtl.Length - 1

        For i As Integer = 0 To max

            If TypeOf errorCtl(i) Is Win.InputMan.LMImTime = True Then

                DirectCast(errorCtl(i), Win.InputMan.LMImTime).BackColorDef = errorColor

            End If

        Next

        focusCtl.Focus()
        focusCtl.Select()

    End Sub

#End Region 'Method

End Class
