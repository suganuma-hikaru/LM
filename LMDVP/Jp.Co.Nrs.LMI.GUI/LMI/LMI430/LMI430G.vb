' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI430  : シリンダー輸入取込
'  作  成  者       :  [inoue]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports FarPoint.Win.Spread
Imports Microsoft.VisualBasic.FileIO
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI430Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI430G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI430F = Nothing

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMIControlG = Nothing

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI430F, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMIConG = g

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim always As Boolean = True
        Dim edit As Boolean = False
        Dim view As Boolean = False
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = LMIControlC.FUNCTION_TORIKOMI
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = LMIControlC.FUNCTION_DEL
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = LMIControlC.FUNCTION_KENSAKU
            .F10ButtonName = LMIControlC.FUNCTION_POP
            .F11ButtonName = LMIControlC.FUNCTION_PRINT_EXEL
            .F12ButtonName = LMIControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = always
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = always
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = always
            .F10ButtonEnabled = always
            .F11ButtonEnabled = always
            .F12ButtonEnabled = always

        End With

    End Sub

#End Region 'FunctionKey


#Region "表示"

    Function GetDispCustName(ByVal custNmL As String, ByVal custNmM As String) As String

        Const CUST_NM_FORMAT As String = "{0}　{1}"

        Return String.Format(CUST_NM_FORMAT, custNmL, custNmM)

    End Function

#End Region


#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .txtCustCdL.TabIndex = LMI430C.CtlTabIndex.CUSTCD_L
            .txtCustCdM.TabIndex = LMI430C.CtlTabIndex.CUSTCD_M
            .imdInkaDateFrom.TabIndex = LMI430C.CtlTabIndex.INKADATE_FROM
            .imdInkaDateTo.TabIndex = LMI430C.CtlTabIndex.INKADATE_TO
            .imdInkaDate.TabIndex = LMI430C.CtlTabIndex.INKADATE
            .txtRemark1.TabIndex = LMI430C.CtlTabIndex.REMARK1
            .txtRemark2.TabIndex = LMI430C.CtlTabIndex.REMARK2
            .txtRemark3.TabIndex = LMI430C.CtlTabIndex.REMARK3
            .sprDetails.TabIndex = LMI430C.CtlTabIndex.SPRDETAILS

            .imdReadDateFrom.TabIndex = LMI430C.CtlTabIndex.READDATE_FROM   'ADD 2017/04/26
            .imdReadDateTo.TabIndex = LMI430C.CtlTabIndex.READDATE_TO       'ADD 2017/04/26

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal sysDate As String)

        With Me._Frm


            '自営業所の設定
            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            .cmbNrsBrCd.SelectedValue = brCd
            .cmbNrsBrCd.ReadOnly = False

            Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).AsEnumerable() _
                                   .Where(Function(r) r.Item(LMI430C.COL_NAME.NRS_BR_CD).Equals(LMUserInfoManager.GetNrsBrCd())).FirstOrDefault

            If (nrsDr IsNot Nothing) Then
                .cmbNrsBrCd.ReadOnly = (Len(nrsDr.Item(LMI430C.COL_NAME.LOCK_FLG)) > 0)
            End If


            ' 入荷日の設定
            .imdInkaDateFrom.TextValue = sysDate
            .imdInkaDateTo.TextValue = sysDate
            .imdInkaDate.TextValue = sysDate

            'ADD 2017/04/26 検品日設定
            .imdReadDateFrom.TextValue = sysDate
            .imdReadDateTo.TextValue = sysDate

            '初期荷主情報取得
            Dim initCust As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TCUST).AsEnumerable() _
                                     .Where(Function(r) r.Item(LMI430C.COL_NAME.SYS_DEL_FLG).Equals(LMConst.FLG.OFF) AndAlso _
                                                        r.Item(LMI430C.COL_NAME.USER_CD).Equals(LM.Base.LMUserInfoManager.GetUserID()) AndAlso _
                                                        r.Item(LMI430C.COL_NAME.DEFAULT_CUST_YN).Equals(LMI430C.YES_CD)).FirstOrDefault


            '初期荷主設定
            If (initCust IsNot Nothing) Then

                .txtCustCdL.TextValue = initCust.Field(Of String)(LMI430C.COL_NAME.CUST_CD_L)
                .txtCustCdM.TextValue = initCust.Field(Of String)(LMI430C.COL_NAME.CUST_CD_M)
                .lblCustNM.TextValue = Me.GetDispCustName(initCust.Field(Of String)(LMI430C.COL_NAME.CUST_NM_L) _
                                                        , initCust.Field(Of String)(LMI430C.COL_NAME.CUST_NM_M))
            End If

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()


        With Me._Frm

            .txtCustCdL.Focus()

        End With


    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .txtCustCdL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNM.TextValue = String.Empty

            .imdInkaDateFrom.TextValue = String.Empty
            .imdInkaDateTo.TextValue = String.Empty
            .imdInkaDate.TextValue = String.Empty

            .txtRemark1.TextValue = String.Empty
            .txtRemark2.TextValue = String.Empty
            .txtRemark3.TextValue = String.Empty

            .imdReadDateFrom.TextValue = String.Empty     'ADD 2017/04/26
            .imdReadDateTo.TextValue = String.Empty       'ADD 2017/04/26

            'スプレッドの行をクリア
            .sprDetails.CrearSpread()

        End With

    End Sub



#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetails

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI430C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared INKA_DATE As SpreadColProperty = New SpreadColProperty(LMI430C.SprColumnIndex.INKA_DATE, "入荷日", 100, True)
        Public Shared CRT_USER_NAME As SpreadColProperty = New SpreadColProperty(LMI430C.SprColumnIndex.CRT_USER_NAME, "取込者", 100, True)
        Public Shared LOAD_FILE_NAME As SpreadColProperty = New SpreadColProperty(LMI430C.SprColumnIndex.LOAD_FILE_NAME, "ファイル名", 180, True)
        Public Shared CYL_COUNT As SpreadColProperty = New SpreadColProperty(LMI430C.SprColumnIndex.LOAD_DATA_COUNT, "件数", 40, True)
        Public Shared REMARK_1 As SpreadColProperty = New SpreadColProperty(LMI430C.SprColumnIndex.REMARK_1, "備考1", 200, True)
        Public Shared REMARK_2 As SpreadColProperty = New SpreadColProperty(LMI430C.SprColumnIndex.REMARK_2, "備考2", 200, True)
        Public Shared REMARK_3 As SpreadColProperty = New SpreadColProperty(LMI430C.SprColumnIndex.REMARK_3, "備考3", 200, True)
        Public Shared CRT_DATE As SpreadColProperty = New SpreadColProperty(LMI430C.SprColumnIndex.CRT_DATE, "取込日", 80, True)
        Public Shared CRT_TIME As SpreadColProperty = New SpreadColProperty(LMI430C.SprColumnIndex.CRT_TIME, "取込時間", 80, True)

        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMI430C.SprColumnIndex.NRS_BR_CD, "", 0, False)
        Public Shared INKA_CYL_FILE_NO_L As SpreadColProperty = New SpreadColProperty(LMI430C.SprColumnIndex.INKA_CYL_FILE_NO_L, "", 0, False)
        Public Shared LAST_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMI430C.SprColumnIndex.LAST_UPD_DATE, "", 0, False)
        Public Shared LAST_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMI430C.SprColumnIndex.LAST_UPD_TIME, "", 0, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetails.CrearSpread()

            '列数設定
            .sprDetails.Sheets(0).ColumnCount = LMI430C.SprColumnIndex.INDEX_COUNT

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetails.SetColProperty(New sprDetails)

        End With

    End Sub


    ''' <summary>
    ''' スプレッドにデータを設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal spr As LMSpread, ByVal ds As DataSet, ByVal clearFlg As Boolean)

        With spr

            If clearFlg = True Then
                'スプレッドの行をクリア
                .CrearSpread()
            End If

            .SuspendLayout()

            '列設定
            Dim checkBoxStyle As StyleInfo = Me.StyleInfoChk(spr)
            Dim labelStyle As StyleInfo = Me.StyleInfoLabel(spr)

            Dim newRowNumber As Integer = 0

            Dim outTable As New LMI430DS.LMI430OUTDataTable()
            outTable.Merge(ds.Tables(LMI430C.TABLE_NM.OUTPUT))


            For Each row As LMI430DS.LMI430OUTRow In outTable.Rows()


                '設定する行数を設定
                newRowNumber = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(newRowNumber, 1)

                'セルスタイル設定
                .SetCellStyle(newRowNumber, LMI430G.sprDetails.DEF.ColNo, checkBoxStyle)
                .SetCellStyle(newRowNumber, LMI430G.sprDetails.INKA_DATE.ColNo, labelStyle)
                .SetCellStyle(newRowNumber, LMI430G.sprDetails.CRT_USER_NAME.ColNo, labelStyle)
                .SetCellStyle(newRowNumber, LMI430G.sprDetails.LOAD_FILE_NAME.ColNo, labelStyle)
                .SetCellStyle(newRowNumber, LMI430G.sprDetails.CYL_COUNT.ColNo, labelStyle)
                .SetCellStyle(newRowNumber, LMI430G.sprDetails.REMARK_1.ColNo, labelStyle)
                .SetCellStyle(newRowNumber, LMI430G.sprDetails.REMARK_2.ColNo, labelStyle)
                .SetCellStyle(newRowNumber, LMI430G.sprDetails.REMARK_3.ColNo, labelStyle)
                .SetCellStyle(newRowNumber, LMI430G.sprDetails.CRT_DATE.ColNo, labelStyle)
                .SetCellStyle(newRowNumber, LMI430G.sprDetails.CRT_TIME.ColNo, labelStyle)

                'セルに値を設定
                .SetCellValue(newRowNumber, sprDetails.DEF.ColNo, False.ToString())
                .SetCellValue(newRowNumber, sprDetails.INKA_DATE.ColNo, DateFormatUtility.EditSlash(row.INKA_DATE))
                .SetCellValue(newRowNumber, sprDetails.CYL_COUNT.ColNo, row.CYL_COUNT)

                .SetCellValue(newRowNumber, sprDetails.LOAD_FILE_NAME.ColNo, row.LOAD_FILE_NAME)
                .SetCellValue(newRowNumber, sprDetails.REMARK_1.ColNo, row.REMARK_1)
                .SetCellValue(newRowNumber, sprDetails.REMARK_2.ColNo, row.REMARK_2)
                .SetCellValue(newRowNumber, sprDetails.REMARK_3.ColNo, row.REMARK_3)

                .SetCellValue(newRowNumber, sprDetails.CRT_DATE.ColNo, DateFormatUtility.EditSlash(row.CRT_DATE))
                .SetCellValue(newRowNumber, sprDetails.CRT_TIME.ColNo, row.CRT_TIME)
                .SetCellValue(newRowNumber, sprDetails.CRT_USER_NAME.ColNo, row.CRT_USER_NM)


                .SetCellValue(newRowNumber, sprDetails.NRS_BR_CD.ColNo, row.NRS_BR_CD)
                .SetCellValue(newRowNumber, sprDetails.INKA_CYL_FILE_NO_L.ColNo, row.INKA_CYL_FILE_NO_L)
                .SetCellValue(newRowNumber, sprDetails.LAST_UPD_DATE.ColNo, row.LAST_UPD_DATE)
                .SetCellValue(newRowNumber, sprDetails.LAST_UPD_TIME.ColNo, row.LAST_UPD_TIME)

            Next

            .ResumeLayout(True)

        End With

    End Sub


#End Region

#Region "ユーティリティ"



#Region "プロパティ"

    ''' <summary>
    ''' セルのプロパティを設定(CheckBox)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoChk(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetCheckBoxCell(spr, False)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Label)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoLabel(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(TextHankaku)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextNUMBER(ByVal spr As LMSpread, ByVal length As Integer, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUMBER, length, lock)

    End Function

#End Region

#End Region

#End Region

End Class
