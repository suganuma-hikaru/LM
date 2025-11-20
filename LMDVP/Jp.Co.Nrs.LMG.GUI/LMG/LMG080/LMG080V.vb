' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG080V : 状況詳細
'  作  成  者       :  [笈川]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMG080Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMG080V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMG080F


    Private _Vcon As LMGControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMG080F, ByVal v As LMGControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal SHUBETSU As LMG080C.EventShubetsu) As Boolean

        '権限の設定
        Dim _Kengen As String = LMUserInfoManager.GetAuthoLv

        Select Case _Kengen
            Case LMConst.AuthoKBN.AGENT
                If SHUBETSU.Equals(LMG080C.EventShubetsu.CLOSE) = False Then
                    MyBase.ShowMessage("E016")
                    Return False
                End If
            Case LMConst.AuthoKBN.VIEW
                If SHUBETSU.Equals(LMG080C.EventShubetsu.YOYAKU_CANCEL) = True Then
                    MyBase.ShowMessage("E016")
                    Return False
                End If
        End Select

        '強制実行時判定
        If SHUBETSU.Equals(LMG080C.EventShubetsu.KYOUSEI) = True Then
            If LMConst.AuthoKBN.MANAGER.Equals(_Kengen) = False Then
                MyBase.ShowMessage("E016")
                Return False
            End If
        End If

        Return True

    End Function

    ''' <summary>
    ''' 入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheck() As Boolean

        With Me._Frm

            'バッチ条件
            .cmbBatch.ItemName = "バッチ条件"
            .cmbBatch.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbBatch) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' スプレッドの項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSpreadInputChk() As Boolean

        With Me._Frm

            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            '実行指示者
            vCell.SetValidateCell(0, LMG080G.sprDetailDef.USER_NM.ColNo)
            vCell.ItemName = "実行指示者"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            If Me.IsValidateCheck(vCell) = False Then Return False

            '荷主コード
            vCell.SetValidateCell(0, LMG080G.sprDetailDef.CUST_CD.ColNo)
            vCell.ItemName = "荷主コード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 14
            If Me.IsValidateCheck(vCell) = False Then Return False

            '荷主名
            vCell.SetValidateCell(0, LMG080G.sprDetailDef.CUST_NM.ColNo)
            vCell.ItemName = "荷主名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 240
            If Me.IsValidateCheck(vCell) = False Then Return False

            'JOB番号
            vCell.SetValidateCell(0, LMG080G.sprDetailDef.JOB_NO.ColNo)
            vCell.ItemName = "JOB番号"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 10
            If Me.IsValidateCheck(vCell) = False Then Return False

        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <param name="SHUBETSU"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function isRelationCheck(ByVal SHUBETSU As LMG080C.EventShubetsu) As Boolean

        Dim Mishori As String = "未処理"
        Dim JikkouZumi As String = "処理済"
        Dim Syorichu As String = "処理中"
        Dim ShoriJokyo As String = String.Empty
        Dim UserId As String = String.Empty
        Dim LogUserId As String = LMUserInfoManager.GetUserID
        Dim ListNo As Integer = 0

        '検索時判定
        Select Case SHUBETSU
            Case LMG080C.EventShubetsu.KENSAKU
                '指示日 (From) +  指示日 (To) 関連チェック
                With Me._Frm
                    If .imdInvDateFrom.TextValue > .imdInvDateTo.TextValue = True Then
                        MyBase.ShowMessage("E182", New String() {"指示日 (To)", "指示日 (From)"})
                        Me._Vcon.SetErrorControl(.imdInvDateFrom)
                        Me._Vcon.SetErrorControl(.imdInvDateTo)
                        Return False
                    End If
                End With
            Case LMG080C.EventShubetsu.KYOUSEI

                If -1 = FindSelectedRowCount() = True Then
                    MyBase.ShowMessage("E009")
                    Return False
                End If
                Dim DefList As ArrayList = FindSelectRow()
                With Me._Frm.sprDetail.ActiveSheet()
                    For i As Integer = 0 To DefList.Count - 1
                        ListNo = Convert.ToInt32(DefList(i))
                        ShoriJokyo = Me._Vcon.GetCellValue(.Cells(ListNo _
                                                                  , LMG080G.sprDetailDef.JIKKOZUMI_KB.ColNo))
                        If Mishori.Equals(ShoriJokyo) = False Then
                            MyBase.ShowMessage("E348", New String() {Mishori})
                            Return False
                        End If
                    Next
                End With
            Case LMG080C.EventShubetsu.YOYAKU_CANCEL
                If -1 = FindSelectedRowCount() = True Then
                    MyBase.ShowMessage("E009")
                    Return False
                End If
                If 1 = FindSelectedRowCount() = True Then
                    MyBase.ShowMessage("E008")
                    Return False
                End If
                With Me._Frm.sprDetail.ActiveSheet()

                    ShoriJokyo = Me._Vcon.GetCellValue(.Cells(FindSelectRowOne _
                                                              , LMG080G.sprDetailDef.JIKKOZUMI_KB.ColNo))
                    UserId = Me._Vcon.GetCellValue(.Cells(FindSelectRowOne _
                                                          , LMG080G.sprDetailDef.USER_ID.ColNo))
                    If Mishori.Equals(ShoriJokyo) = False Then
                        MyBase.ShowMessage("E348", New String() {Mishori})
                        Return False
                    End If
                    If LogUserId.Equals(UserId) = False Then
                        MyBase.ShowMessage("E210", New String() {"予約取消"})
                        Return False
                    End If

                End With
            Case LMG080C.EventShubetsu.SHORISHOUSAI
                If -1 = FindSelectedRowCount() = True Then
                    MyBase.ShowMessage("E009")
                    Return False
                End If
                With Me._Frm.sprDetail.ActiveSheet()

                    For i As Integer = 0 To FindSelectRow.Count - 1

                        ShoriJokyo = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(FindSelectRow(i)) _
                                                                  , LMG080G.sprDetailDef.JIKKOZUMI_KB.ColNo))

                        If JikkouZumi.Equals(ShoriJokyo) = False Then
                            MyBase.ShowMessage("E348", New String() {JikkouZumi})
                            Return False
                        End If
                    Next

                End With

            Case LMG080C.EventShubetsu.KYOUSEI_DEL
                If -1 = FindSelectedRowCount() = True Then
                    MyBase.ShowMessage("E009")
                    Return False
                End If

                If 1 = FindSelectedRowCount() = True Then
                    MyBase.ShowMessage("E008")
                    Return False
                End If

                With Me._Frm.sprDetail.ActiveSheet()

                    For i As Integer = 0 To FindSelectRow.Count - 1

                        ShoriJokyo = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(FindSelectRow(i)) _
                                                                  , LMG080G.sprDetailDef.JIKKOZUMI_KB.ColNo))

                        UserId = Me._Vcon.GetCellValue(.Cells(FindSelectRowOne _
                                                          , LMG080G.sprDetailDef.USER_ID.ColNo))

                        If Syorichu.Equals(ShoriJokyo) = False Then
                            MyBase.ShowMessage("E348", New String() {Syorichu})
                            Return False
                        End If

                        If LogUserId.Equals(UserId) = False Then
                            MyBase.ShowMessage("E210", New String() {"強制削除"})
                            Return False
                        End If

                    Next

                End With

        End Select

        Return True

    End Function

    ''' <summary>
    ''' 5分経過チェック
    ''' </summary>
    ''' <param name="SysNowDate"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SysUpdDateTimeCheck(ByVal SysNowDate As String, ByVal SysNowTime As String) As Boolean

        Dim Sys_Upd_Date As String = String.Empty
        Dim Sys_Upd_Time As String = String.Empty
        Dim NowDateTime As DateTime
        Dim UpdDateTime As DateTime

        Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'G020' AND KBN_CD = '01'")
        If kbnDr.Length = 0 Then
            MyBase.ShowMessage("E296", New String() {"区分マスタG020"})
            Return False
        End If

        With Me._Frm.sprDetail.ActiveSheet()

            For i As Integer = 0 To FindSelectRow.Count - 1

                Sys_Upd_Date = Me._Vcon.GetCellValue(.Cells(FindSelectRowOne _
                                                  , LMG080G.sprDetailDef.SYS_UPD_DATE.ColNo))

                Sys_Upd_Time = Me._Vcon.GetCellValue(.Cells(FindSelectRowOne _
                                                  , LMG080G.sprDetailDef.SYS_UPD_TIME.ColNo))

            Next

            NowDateTime = DateTime.ParseExact(SysNowDate & SysNowTime, "yyyyMMddHHmmssfff", System.Globalization.CultureInfo.InvariantCulture)
            UpdDateTime = DateTime.ParseExact(Sys_Upd_Date & Sys_Upd_Time, "yyyyMMddHHmmssfff", System.Globalization.CultureInfo.InvariantCulture)

            If UpdDateTime.AddMinutes(CDbl(kbnDr(0).Item("KBN_NM1").ToString)) > NowDateTime Then
                MyBase.ShowMessage("E320", New String() {"最終更新時間から" & kbnDr(0).Item("KBN_NM1").ToString & "分経過していない", "強制削除"})
                Return False
            End If
        End With

        Return True

    End Function

    ''' <summary>
    ''' 選択行有無判別
    ''' </summary>
    ''' <param name="rowCnt">選択行数（省略可）</param>
    ''' <returns>-1：選択行無し　0：単一行選択中　1：複数行選択中</returns>
    ''' <remarks></remarks>
    Friend Function FindSelectedRowCount(Optional ByRef rowCnt As Integer = 0) As Integer

        With Me._Frm.sprDetail.Sheets(0)

            Dim rowIdx As Integer = -1

            For i As Integer = 1 To .RowCount - 1
                If .Cells(i, LMG080G.sprDetailDef.DEF.ColNo).Value.ToString = True.ToString Then
                    rowCnt = rowCnt + 1

                    If rowIdx = 0 Then
                        rowIdx = 1
                    End If
                    If rowIdx <> 1 Then
                        rowIdx = 0
                    End If
                End If
            Next

            Return rowIdx

        End With

    End Function

    ''' <summary>
    ''' 選択行取得
    ''' </summary>
    ''' <returns>選択行リスト</returns>
    ''' <remarks></remarks>
    Friend Function FindSelectRow() As ArrayList

        Dim DefList As ArrayList = New ArrayList()
        With Me._Frm.sprDetail.Sheets(0)

            Dim rowIdx As Integer = -1

            For i As Integer = 1 To .RowCount - 1
                If .Cells(i, LMG080G.sprDetailDef.DEF.ColNo).Value.ToString = True.ToString Then
                    DefList.Add(i)
                End If
            Next

            Return DefList

        End With

    End Function

    ''' <summary>
    ''' 選択行取得
    ''' </summary>
    ''' <returns>選択行リスト</returns>
    ''' <remarks></remarks>
    Friend Function FindSelectRowOne() As Integer

        Dim DefList As ArrayList = New ArrayList()
        With Me._Frm.sprDetail.Sheets(0)

            Dim rowIdx As Integer = -1

            For i As Integer = 1 To .RowCount - 1
                If .Cells(i, LMG080G.sprDetailDef.DEF.ColNo).Value.ToString = True.ToString Then
                    Return i
                End If
            Next

        End With

    End Function

#End Region 'Method

End Class
