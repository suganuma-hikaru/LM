' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : 特殊荷主機能
'  プログラムID     :  LMI410G : ビックケミー取込データ確認／報告
'  作  成  者       :  [Umano]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports FarPoint.Win.Spread

''' <summary>
''' LMI410Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMI410G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI410F

    ''' <summary>
    ''' Handlerクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _H As LMI410H

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMIControlG

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI410F, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ControlG = g

    End Sub

#End Region

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetFunctionKey()

        Dim unLock As Boolean = True
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True
            'ファンクションキー個別設定
            .F1ButtonName = "取　込"
            .F2ButtonName = "移動報告"
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = LMIControlC.FUNCTION_KENSAKU
            .F10ButtonName = LMIControlC.FUNCTION_POP
            .F11ButtonName = String.Empty
            .F12ButtonName = LMIControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = True
            .F2ButtonEnabled = True
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = False
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = True
            .F10ButtonEnabled = True
            .F11ButtonEnabled = False
            .F12ButtonEnabled = True

        End With

    End Sub

#End Region


#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm
            .cmbEigyo.TabIndex = LMI410C.CtlTabIndex.CMB_EIGYO
            .txtCustCdL.TabIndex = LMI410C.CtlTabIndex.TXT_CUST_CD_L
            .txtCustCdM.TabIndex = LMI410C.CtlTabIndex.TXT_CUST_CD_M
            .txtCustNm.TabIndex = LMI410C.CtlTabIndex.TXT_CUST_NM
            .txtUserCd.TabIndex = LMI410C.CtlTabIndex.TXT_USER_CD
            .txtUserNm.TabIndex = LMI410C.CtlTabIndex.TXT_USER_NM
            .cmbHoukoku.TabIndex = LMI410C.CtlTabIndex.CMB_JISSEKI_KBN
            .cmbSearchDate.TabIndex = LMI410C.CtlTabIndex.CMB_SEARCH_KBN
            .imdSearchDateFrom.TabIndex = LMI410C.CtlTabIndex.IMD_SEARCH_DATE_FROM
            .imdSearchDateTo.TabIndex = LMI410C.CtlTabIndex.IMD_SEARCH_DATE_TO
            .btnJikkou.TabIndex = LMI410C.CtlTabIndex.BTN_JIKKOU
            .cmbIkkatuKbn.TabIndex = LMI410C.CtlTabIndex.CMB_JISSEKI_KBN
            .txtIkkatuCustL.TabIndex = LMI410C.CtlTabIndex.TXT_IKKATU_CUST_CD_L
            .txtIkkatuCustM.TabIndex = LMI410C.CtlTabIndex.TXT_IKKATU_CUST_CD_M
            .imdIkkatuDate.TabIndex = LMI410C.CtlTabIndex.IMD_IKKATU_DATE
            .btnIkkatu.TabIndex = LMI410C.CtlTabIndex.BTN_IKKATU
        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

        End With

    End Sub

    ''' <summary>
    ''' ビックケミーの情報をフォームにセットする
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetCustData()

        With Me._Frm

            'ビックケミーの荷主コードをセット
            .txtCustCdL.TextValue = LMI410C.DEF_BYK_CUST

        End With

    End Sub


    ''' <summary>
    ''' コントロールに初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitControl(ByVal id As String, ByRef frm As LMI410F, ByVal sysDate As String)

        frm.cmbSearchDate.SelectedValue = LMI410C.CMB_01
        frm.imdSearchDateFrom.TextValue = sysDate
        frm.imdSearchDateTo.TextValue = sysDate

    End Sub

    ''' <summary>
    ''' ファンクションキーの制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()
        With _Frm
            Call Me.SetFunctionKey()

        End With

    End Sub


#Region "内部メソッド"

    ''' <summary>
    '''営業所コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SetcmbNrsBrCd()

        'ユーザーマスタの営業所をセット
        Me._Frm.cmbEigyo.SelectedValue = LMUserInfoManager.GetNrsBrCd()

    End Sub

#End Region

#Region "文字色"
    ''' <summary>
    ''' スプレッドの文字色設定
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Friend Sub SetSpreadColor(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprIdoList
        Dim dr As DataRow
        Dim lngcnt As Integer = dt.Rows.Count()

        With spr

            If lngcnt = 0 Then
                Exit Sub
            End If

            For i As Integer = 1 To lngcnt
                dr = dt.Rows(i - 1)

                If String.IsNullOrEmpty(dr.Item("CUST_CD_L").ToString) = True Then
                    '荷主コードが空の場合：赤
                    .ActiveSheet.Rows(i).ForeColor = Color.Red
                End If
            Next

        End With


    End Sub
#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(倉庫間転送)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprIdoList

        '**** 表示列 ****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.DEF, " ", 20, True)
        Public Shared CUST_CD As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.CUST_CD_LM, "荷主ｺｰﾄﾞ", 65, True)
        Public Shared SAGYO_STATE_NM As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.SAGYO_STATE_NM, "進捗状況", 67, True)
        Public Shared SYORI_SUB As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.SYORI_SUB, "処理", 50, True)
        Public Shared INOUTKA_DATE As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.INOUTKA_DATE, "入出荷" & vbCrLf & "(振替)日", 80, True)
        Public Shared CRT_DATE As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.CRT_DATE, "取込日", 80, True)
        Public Shared SAGYO_NAIYO As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.SAGYO_NAIYO, "作業内容", 100, True)
        Public Shared CURRENT_MATERIAL As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.CURRENT_MATERIAL, "元" & vbCrLf & "商品ｺｰﾄﾞ", 70, True)
        Public Shared CURRENT_DESCRIPTION As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.CURRENT_DESCRIPTION, "元" & vbCrLf & "商品名", 90, True)
        Public Shared CURRENT_GOODS_JOTAI As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.CURRENT_GOODS_JOTAI, "元商品" & vbCrLf & "状態", 50, True)
        Public Shared CURRENT_BATCH As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.CURRENT_BATCH, "元" & vbCrLf & "LOT№", 77, True)
        Public Shared CURRENT_QUANTITY As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.CURRENT_QUANTITY, "元" & vbCrLf & "個数", 35, True)
        Public Shared YAJIRUSI As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.YAJIRUSI, " ", 15, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.DEST_NM, "届先" & vbCrLf & "(出荷元)", 80, True)
        Public Shared DESTINATION_MATERIAL As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.DESTINATION_MATERIAL, "先" & vbCrLf & "商品ｺｰﾄﾞ", 70, True)
        Public Shared DESTINATION_DESCRIPTION As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.DESTINATION_DESCRIPTION, "先" & vbCrLf & "商品名", 90, True)
        Public Shared DESTINATION_GOODS_JOTAI As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.DESTINATION_GOODS_JOTAI, "先商品" & vbCrLf & "状態", 50, True)
        Public Shared DESTINATION_BATCH As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.DESTINATION_BATCH, "先" & vbCrLf & "LOT№", 77, True)
        Public Shared DESTINATION_QUANTITY As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.DESTINATION_QUANTITY, "先" & vbCrLf & "個数", 35, True)
        Public Shared SYS_UPD_USER As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.SYS_UPD_USER, "更新者", 80, True)
        Public Shared PRINT_KBN_NM As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.PRINT_KBN_NM, "印刷", 35, False)
        Public Shared TEXT_NM As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.TEXT_NM, "備考", 150, True)
        Public Shared FILE_NAME As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.FILE_NAME, "取込ファイル名", 200, True)

        '**** 隠し列 ****
        Public Shared JISSEKI_SHORI_FLG As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.JISSEKI_SHORI_FLG, "", 50, False)
        Public Shared SYORI_KBN As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.SYORI_KBN, "", 50, False)
        Public Shared PRINT_FLG As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.PRINT_FLG, "", 50, False)
        Public Shared CURRENT_STORAGE_LOCATION As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.CURRENT_STORAGE_LOCATION, "", 50, False)
        Public Shared DESTINATION_STORAGE_LOCATION As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.DESTINATION_STORAGE_LOCATION, "", 50, False)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.NRS_BR_CD, "", 50, False)
        Public Shared REC_NO As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.REC_NO, "", 50, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.SYS_UPD_DATE, "", 50, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.SYS_UPD_TIME, "", 50, False)
        Public Shared SAGYO_STATE_KBN As SpreadColProperty = New SpreadColProperty(LMI410C.SprIdoListColumnIndex.SAGYO_STATE_KBN, "", 50, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        '(倉庫間転送)Spreadの初期化処理
        Call Me.InitIdoSpread()

    End Sub

    ''' <summary>
    ''' 検索結果を商品Spreadに表示
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprIdoList

        With spr

            .SuspendLayout()

            'データ挿入
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            '列設定用変数
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim lblL As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            Dim numIrime As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999.999, True, 3, , ",")
            Dim numIrimeUnLock As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999.999, False, 3, , ",")
            Dim num As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 99999999, True, 0, , ",")
            Dim numUnLock As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 99999999, False, 0, , ",")
            Dim numKg As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999.999, True, 3, , ",")
            Dim nbUt As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_K002, True)
            Dim nbUtUnLock As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_K002, False)
            Dim stdIrimeUt As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_I001, True)
            Dim stdIrimeUtUnLock As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_I001, False)


            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                '**** 表示列 ****
                .SetCellStyle(i, sprIdoList.DEF.ColNo, def)
                .SetCellStyle(i, sprIdoList.CUST_CD.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.SAGYO_STATE_NM.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.SYORI_SUB.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.INOUTKA_DATE.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.FILE_NAME.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.CRT_DATE.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.PRINT_KBN_NM.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.SAGYO_NAIYO.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.TEXT_NM.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.CURRENT_MATERIAL.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.CURRENT_DESCRIPTION.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.CURRENT_GOODS_JOTAI.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.CURRENT_BATCH.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.CURRENT_QUANTITY.ColNo, num)
                .SetCellStyle(i, sprIdoList.YAJIRUSI.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.DEST_NM.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.DESTINATION_MATERIAL.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.DESTINATION_DESCRIPTION.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.DESTINATION_GOODS_JOTAI.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.DESTINATION_BATCH.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.DESTINATION_QUANTITY.ColNo, num)
                .SetCellStyle(i, sprIdoList.SYS_UPD_USER.ColNo, lblL)

                '**** 隠し列 ****
                .SetCellStyle(i, sprIdoList.JISSEKI_SHORI_FLG.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.SYORI_KBN.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.PRINT_FLG.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.CURRENT_STORAGE_LOCATION.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.DESTINATION_STORAGE_LOCATION.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.NRS_BR_CD.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.REC_NO.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.SYS_UPD_DATE.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.SYS_UPD_TIME.ColNo, lblL)
                .SetCellStyle(i, sprIdoList.SAGYO_STATE_KBN.ColNo, lblL)

                'セル値設定
                '**** 表示列 ****
                .SetCellValue(i, sprIdoList.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprIdoList.CUST_CD.ColNo, String.Concat(dr.Item("CUST_CD_L").ToString(), "-", dr.Item("CUST_CD_M").ToString()))
                .SetCellValue(i, sprIdoList.SAGYO_STATE_NM.ColNo, dr.Item("SAGYO_STATE_NM").ToString())
                .SetCellValue(i, sprIdoList.SYORI_SUB.ColNo, dr.Item("SYORI_SUB").ToString())
                .SetCellValue(i, sprIdoList.INOUTKA_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("INOUTKA_DATE").ToString()))
                .SetCellValue(i, sprIdoList.FILE_NAME.ColNo, dr.Item("FILE_NAME").ToString())
                .SetCellValue(i, sprIdoList.CRT_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("CRT_DATE").ToString()))
                .SetCellValue(i, sprIdoList.PRINT_KBN_NM.ColNo, dr.Item("PRINT_KBN_NM").ToString())
                .SetCellValue(i, sprIdoList.SAGYO_NAIYO.ColNo, dr.Item("SAGYO_NAIYO").ToString())
                .SetCellValue(i, sprIdoList.TEXT_NM.ColNo, dr.Item("TEXT_NM").ToString())
                .SetCellValue(i, sprIdoList.CURRENT_MATERIAL.ColNo, dr.Item("CURRENT_MATERIAL").ToString())
                .SetCellValue(i, sprIdoList.CURRENT_DESCRIPTION.ColNo, dr.Item("CURRENT_DESCRIPTION").ToString())
                .SetCellValue(i, sprIdoList.CURRENT_GOODS_JOTAI.ColNo, dr.Item("CURRENT_GOODS_JOTAI").ToString())
                .SetCellValue(i, sprIdoList.CURRENT_BATCH.ColNo, dr.Item("CURRENT_BATCH").ToString())
                .SetCellValue(i, sprIdoList.CURRENT_QUANTITY.ColNo, dr.Item("CURRENT_QUANTITY").ToString())
                .SetCellValue(i, sprIdoList.YAJIRUSI.ColNo, "⇒")
                .SetCellValue(i, sprIdoList.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())
                .SetCellValue(i, sprIdoList.DESTINATION_MATERIAL.ColNo, dr.Item("DESTINATION_MATERIAL").ToString())
                .SetCellValue(i, sprIdoList.DESTINATION_DESCRIPTION.ColNo, dr.Item("DESTINATION_DESCRIPTION").ToString())
                .SetCellValue(i, sprIdoList.DESTINATION_GOODS_JOTAI.ColNo, dr.Item("DESTINATION_GOODS_JOTAI").ToString())
                .SetCellValue(i, sprIdoList.DESTINATION_BATCH.ColNo, dr.Item("DESTINATION_BATCH").ToString())
                .SetCellValue(i, sprIdoList.DESTINATION_QUANTITY.ColNo, dr.Item("DESTINATION_QUANTITY").ToString())
                .SetCellValue(i, sprIdoList.SYS_UPD_USER.ColNo, dr.Item("SYS_UPD_USER").ToString())

                '**** 隠し列 ****
                .SetCellValue(i, sprIdoList.JISSEKI_SHORI_FLG.ColNo, dr.Item("JISSEKI_SHORI_FLG").ToString())
                .SetCellValue(i, sprIdoList.SYORI_KBN.ColNo, dr.Item("SYORI_KBN").ToString())
                .SetCellValue(i, sprIdoList.PRINT_FLG.ColNo, dr.Item("PRINT_FLG").ToString())
                .SetCellValue(i, sprIdoList.CURRENT_STORAGE_LOCATION.ColNo, dr.Item("CURRENT_STORAGE_LOCATION").ToString())
                .SetCellValue(i, sprIdoList.DESTINATION_STORAGE_LOCATION.ColNo, dr.Item("DESTINATION_STORAGE_LOCATION").ToString())
                .SetCellValue(i, sprIdoList.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, sprIdoList.REC_NO.ColNo, dr.Item("REC_NO").ToString())
                .SetCellValue(i, sprIdoList.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprIdoList.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprIdoList.SAGYO_STATE_KBN.ColNo, dr.Item("SAGYO_STATE_KBN").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region

#Region "内部メソッド"

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitIdoSpread()

        '商品Spreadの初期値設定
        Dim sprIdo As LMSpread = Me._Frm.sprIdoList

        With sprIdo

            ''スプレッドの行をクリア
            .CrearSpread()

            ''列数設定
            .ActiveSheet.ColumnCount = LMI410C.SprIdoListColumnIndex.LAST

            ''スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New sprIdoList)

            '列固定位置を設定します。
            .ActiveSheet.FrozenColumnCount = sprIdoList.INOUTKA_DATE.ColNo + 1

            '検索行の設定を行う
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(sprIdo)
            Dim sCustM As StyleInfo = Me.StyleInfoCustCond(sprIdo, Me._Frm, False)

            '**** 表示列 ****
            .SetCellStyle(0, sprIdoList.DEF.ColNo, lbl)
            .SetCellStyle(0, sprIdoList.CUST_CD.ColNo, lbl)
            .SetCellStyle(0, sprIdoList.SAGYO_STATE_NM.ColNo, LMSpreadUtility.GetComboCellKbn(sprIdo, "B028", False))
            .SetCellStyle(0, sprIdoList.SYORI_SUB.ColNo, LMSpreadUtility.GetComboCellKbn(sprIdo, "B023", False))
            .SetCellStyle(0, sprIdoList.INOUTKA_DATE.ColNo, lbl)
            .SetCellStyle(0, sprIdoList.FILE_NAME.ColNo, LMSpreadUtility.GetTextCell(sprIdo, InputControl.ALL_MIX, 300, False))
            .SetCellStyle(0, sprIdoList.CRT_DATE.ColNo, lbl)
            .SetCellStyle(0, sprIdoList.PRINT_KBN_NM.ColNo, LMSpreadUtility.GetComboCellKbn(sprIdo, LMKbnConst.KBN_K013, False))
            .SetCellStyle(0, sprIdoList.SAGYO_NAIYO.ColNo, lbl)
            .SetCellStyle(0, sprIdoList.TEXT_NM.ColNo, LMSpreadUtility.GetTextCell(sprIdo, InputControl.ALL_MIX, 100, False))
            .SetCellStyle(0, sprIdoList.CURRENT_MATERIAL.ColNo, LMSpreadUtility.GetTextCell(sprIdo, InputControl.ALL_HANKAKU, 18, False))
            .SetCellStyle(0, sprIdoList.CURRENT_DESCRIPTION.ColNo, LMSpreadUtility.GetTextCell(sprIdo, InputControl.ALL_MIX, 80, False))
            .SetCellStyle(0, sprIdoList.CURRENT_GOODS_JOTAI.ColNo, sCustM)
            .SetCellStyle(0, sprIdoList.CURRENT_BATCH.ColNo, LMSpreadUtility.GetTextCell(sprIdo, InputControl.ALL_HANKAKU, 10, False))
            .SetCellStyle(0, sprIdoList.CURRENT_QUANTITY.ColNo, lbl)
            .SetCellStyle(0, sprIdoList.YAJIRUSI.ColNo, lbl)
            .SetCellStyle(0, sprIdoList.DEST_NM.ColNo, LMSpreadUtility.GetTextCell(sprIdo, InputControl.ALL_MIX, 80, False))
            .SetCellStyle(0, sprIdoList.DESTINATION_MATERIAL.ColNo, LMSpreadUtility.GetTextCell(sprIdo, InputControl.ALL_HANKAKU, 18, False))
            .SetCellStyle(0, sprIdoList.DESTINATION_DESCRIPTION.ColNo, LMSpreadUtility.GetTextCell(sprIdo, InputControl.ALL_MIX, 80, False))
            .SetCellStyle(0, sprIdoList.DESTINATION_GOODS_JOTAI.ColNo, sCustM)
            .SetCellStyle(0, sprIdoList.DESTINATION_BATCH.ColNo, LMSpreadUtility.GetTextCell(sprIdo, InputControl.ALL_HANKAKU, 10, False))
            .SetCellStyle(0, sprIdoList.DESTINATION_QUANTITY.ColNo, lbl)
            .SetCellStyle(0, sprIdoList.SYS_UPD_USER.ColNo, lbl)

            '**** 隠し列 ****
            .SetCellStyle(0, sprIdoList.JISSEKI_SHORI_FLG.ColNo, lbl)
            .SetCellStyle(0, sprIdoList.SYORI_KBN.ColNo, lbl)
            .SetCellStyle(0, sprIdoList.PRINT_FLG.ColNo, lbl)
            .SetCellStyle(0, sprIdoList.CURRENT_STORAGE_LOCATION.ColNo, lbl)
            .SetCellStyle(0, sprIdoList.DESTINATION_STORAGE_LOCATION.ColNo, lbl)
            .SetCellStyle(0, sprIdoList.NRS_BR_CD.ColNo, lbl)
            .SetCellStyle(0, sprIdoList.REC_NO.ColNo, lbl)
            .SetCellStyle(0, sprIdoList.SYS_UPD_DATE.ColNo, lbl)
            .SetCellStyle(0, sprIdoList.SYS_UPD_TIME.ColNo, lbl)
            .SetCellStyle(0, sprIdoList.SAGYO_STATE_KBN.ColNo, lbl)

        End With

    End Sub


#End Region

#Region "実績報告種別区分変更時"
    ''' <summary>
    ''' 実績報告種別区分値変更のロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub LockDisp(ByVal frm As LMI410F, ByVal cmbsyubetu As Integer)

        With Me._Frm

            Me.SetLockControl(.txtCustCdL, False)
            Me.SetLockControl(.txtCustCdM, False)
            Me.SetLockControl(.txtUserCd, False)
            Me.SetLockControl(.cmbHoukoku, False)
            Me.SetLockControl(.cmbSearchDate, False)
            Me.SetLockControl(.imdSearchDateFrom, False)
            Me.SetLockControl(.imdSearchDateTo, False)
            Me.SetLockControl(.cmbIkkatuKbn, False)
            Me.SetLockControl(.txtIkkatuCustL, False)
            Me.SetLockControl(.txtIkkatuCustM, False)
            Me.SetLockControl(.imdIkkatuDate, False)

            Select Case cmbsyubetu

                Case LMI410C.comboShubetsu.JISSEKI_CMB

                    '実績報告種別区分
                    Dim JissekiKb As String = .cmbHoukoku.SelectedValue.ToString

                    Select Case JissekiKb

                        Case LMI410C.CMB_01, LMI410C.CMB_02
                            'BYK出荷報告作成
                            .cmbSearchDate.SelectedValue = LMI410C.CMB_02
                            .imdSearchDateTo.Visible = False
                            .lblTitleFromTo.Visible = False

                        Case Else
                            .imdSearchDateTo.Visible = True
                            .lblTitleFromTo.Visible = True

                    End Select

                Case LMI410C.comboShubetsu.SEARCH_HOUKOKU_CMB

                    .imdSearchDateFrom.Visible = True
                    .imdSearchDateTo.Visible = True
                    .lblTitleFromTo.Visible = True

                Case LMI410C.comboShubetsu.IKKATU_CMB

                    '実績報告種別区分
                    Dim IkkatuKb As String = .cmbIkkatuKbn.SelectedValue.ToString

                    .txtIkkatuCustL.Visible = False
                    .txtIkkatuCustM.Visible = False
                    .lblIkkatuCustNm.Visible = False
                    .imdIkkatuDate.Visible = False

                    Select Case IkkatuKb

                        Case LMI410C.CMB_01
                            '荷主コード
                            .txtIkkatuCustL.Visible = True
                            .txtIkkatuCustM.Visible = True
                            .lblIkkatuCustNm.Visible = True

                        Case LMI410C.CMB_02
                            .imdIkkatuDate.Visible = True

                        Case Else

                    End Select

            End Select

        End With

    End Sub
#End Region

    ''' <summary>
    ''' セルのプロパティを設定(CUSTCOND)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="frm">frm</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Friend Function StyleInfoCustCond(ByVal spr As LMSpread, ByVal frm As LMI410F, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetComboCellMaster(spr _
                                                  , LMConst.CacheTBL.CUSTCOND _
                                                  , "JOTAI_CD" _
                                                  , "JOTAI_NM" _
                                                  , lock _
                                                  , New String() {"NRS_BR_CD", "CUST_CD_L"} _
                                                  , New String() {Convert.ToString(frm.cmbEigyo.SelectedValue()), Convert.ToString(frm.txtCustCdL.TextValue)} _
                                                  , LMConst.JoinCondition.AND_WORD _
                                                  )

    End Function

#Region "ロック処理/ロック解除"
    ''' <summary>
    ''' ロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="ctl">制御対象項目</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Private Sub SetLockControl(ByVal ctl As Control, Optional ByVal lockFlg As Boolean = False)

        Dim arr As ArrayList = New ArrayList()
        Call Me.GetTarget(Of Nrs.Win.GUI.Win.Interface.IEditableControl)(arr, ctl)
        Dim lblArr As ArrayList = New ArrayList()

        'エディット系コントロールのロック
        For Each arrCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In arr

            'テキストボックスの場合、ラベル項目であったら処理対象外とする
            If TypeOf arrCtl Is Win.InputMan.LMImTextBox = True Then

                If DirectCast(arrCtl, Win.InputMan.LMImTextBox).Name.Substring(0, 3).Equals("lbl") = True Then
                    lblArr.Add(arrCtl)
                End If

            End If

            'ロック処理/ロック解除処理を行う
            arrCtl.ReadOnlyStatus = lockFlg
        Next

        'ラベル項目をロック
        For Each lblCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In lblArr
            lblCtl.ReadOnlyStatus = True
        Next

        'ボタンのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMButton)(arr, ctl)
        For Each arrCtl As Win.LMButton In arr
            'ロック処理/ロック解除処理を行う
            Call Me.LockButton(arrCtl, lockFlg)
        Next

        'チェックボックスのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMCheckBox)(arr, ctl)
        For Each arrCtl As Win.LMCheckBox In arr

            'ロック処理/ロック解除処理を行う
            Call Me.LockCheckBox(arrCtl, lockFlg)

        Next

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(ボタン)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockButton(ByVal ctl As Win.LMButton, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.Enabled = enabledFlg

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(チェックボックス)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockCheckBox(ByVal ctl As Win.LMCheckBox, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.EnableStatus = enabledFlg

    End Sub

    ''' <summary>
    ''' 画面上のコントロールから指定した型のクラスかその継承クラスを取得する
    ''' </summary>
    ''' <param name="arr">取得したコントロールを格納するArrayList</param>
    ''' <param name="ownControl">コントロール取得元となるコントロール</param>
    ''' <remarks></remarks>
    Private Sub GetTarget(Of T)(ByVal arr As ArrayList, ByVal ownControl As Control)

        If TypeOf ownControl Is T _
            OrElse ownControl.GetType.IsSubclassOf(GetType(T)) Then

            '指定されたクラスかその継承クラス
            arr.Add(ownControl)

        End If

        If 0 < ownControl.Controls.Count Then
            For Each targetControl As Control In ownControl.Controls
                Call Me.GetTarget(Of T)(arr, targetControl)
            Next
        End If

    End Sub


#End Region

#End Region

#End Region

End Class
