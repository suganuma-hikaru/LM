' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF210G : 運行未登録一覧
'  作  成  者       :  [ito]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LMF210Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF210G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF210F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF210F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

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
            .SetFKeysType(LMImFunctionKey.FkeyTypes.POP_L)

            .Enabled = True

            'ファンクションキー個別設定
            .F9ButtonName = "検　索"
            .F10ButtonName = String.Empty
            .F11ButtonName = "Ｏ　Ｋ"
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F9ButtonEnabled = always
            .F10ButtonEnabled = lock
            .F11ButtonEnabled = always
            .F12ButtonEnabled = always

        End With

    End Sub

#End Region 'FunctionKey

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            '.sprDetail.TabIndex = LMF210C.CtlTabIndex.DETAIL
            .cmbEigyo.TabIndex = LMF210C.CtlTabIndex.EIGYO
            .numLoadWtZanFrom.TabIndex = LMF210C.CtlTabIndex.LoadWtZanFrom
            .numLoadWtZanTo.TabIndex = LMF210C.CtlTabIndex.LoadWtZanTo
            .imdFrom.TabIndex = LMF210C.CtlTabIndex.UncouFrom
            .imdTo.TabIndex = LMF210C.CtlTabIndex.UncouTo
            .numOnkanFrom.TabIndex = LMF210C.CtlTabIndex.OnkanFrom
            .numOnkanTo.TabIndex = LMF210C.CtlTabIndex.OnkanTo

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

       
        '数値コントロールの書式設定
        Call Me.SetNumberControl()

        '編集部の項目をクリア
        Call Me.ClearControl()


    End Sub

    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm


            Dim d3 As Decimal = Convert.ToDecimal("99")
            Dim d3Min As Decimal = Convert.ToDecimal("-99")
            Dim d5 As Decimal = Convert.ToDecimal("99999")
            Dim d12d3 As Decimal = Convert.ToDecimal("999,999,999.999")

            .numLoadWtZanFrom.SetInputFields("###,###,##0.000", , 9, 0, , , , , d12d3, 0)
            .numLoadWtZanTo.SetInputFields("###,###,##0.000", , 9, 0, , , , , d12d3, 0)
            .numOnkanFrom.SetInputFields("#0", , 2, 0, , , , , d3, d3Min)
            .numOnkanTo.SetInputFields("#0", , 2, 0, , , , , d3, d3Min)

        End With
    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .numLoadWtZanFrom.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbEigyo.SelectedValue = Nothing
            .imdFrom.TextValue = String.Empty
            .imdTo.TextValue = String.Empty
            .numLoadWtZanFrom.TextValue = String.Empty
            .numLoadWtZanTo.TextValue = String.Empty
            .numOnkanFrom.TextValue = String.Empty
            .numOnkanTo.TextValue = String.Empty


        End With

    End Sub
    ''' <summary>
    '''営業所のコンボボックスを自営業所に設定
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SetNrsBrCd()

        '自営業所の取得
        Me._Frm.cmbEigyo.SelectedValue = LMUserInfoManager.GetNrsBrCd()

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()
        
        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim max As Integer = spr.ActiveSheet.Columns.Count - 1

        With spr

            For i As Integer = 1 To max
                .SetCellValue(0, i, String.Empty)
            Next

        End With
    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
     
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMF210C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared TRIP_NO As SpreadColProperty = New SpreadColProperty(LMF210C.SprColumnIndex.TRIP_NO, "運行番号", 80, True)
        Public Shared TRIP_DATE As SpreadColProperty = New SpreadColProperty(LMF210C.SprColumnIndex.TRIP_DATE, "運行予定日", 90, True)
        Public Shared UNSO_NM As SpreadColProperty = New SpreadColProperty(LMF210C.SprColumnIndex.UNSO_NM, "運送会社名", 90, True)
        Public Shared CAR_NO As SpreadColProperty = New SpreadColProperty(LMF210C.SprColumnIndex.CAR_NO, "車番", 60, True)
        Public Shared BIN As SpreadColProperty = New SpreadColProperty(LMF210C.SprColumnIndex.BIN, "便区分", 110, True)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMF210C.SprColumnIndex.REMARK, "備考", 60, True)
        Public Shared JSHA_NM As SpreadColProperty = New SpreadColProperty(LMF210C.SprColumnIndex.JSHA_NM, "自傭車" & vbCrLf & "区分", 60, True)
        Public Shared DRIVER_NM As SpreadColProperty = New SpreadColProperty(LMF210C.SprColumnIndex.DRIVER_NM, "乗務員", 60, True)
        Public Shared UNSO_PKG_NB As SpreadColProperty = New SpreadColProperty(LMF210C.SprColumnIndex.UNSO_PKG_NB, "運送個数", 70, True)
        Public Shared UNSO_WT As SpreadColProperty = New SpreadColProperty(LMF210C.SprColumnIndex.UNSO_WT, "運送重量", 100, True)
        Public Shared LOAD_WT As SpreadColProperty = New SpreadColProperty(LMF210C.SprColumnIndex.LOAD_WT, "積載重量", 100, True)
        Public Shared ON_KAN As SpreadColProperty = New SpreadColProperty(LMF210C.SprColumnIndex.ON_KAN, "管理温度", 70, True)
        Public Shared PAY_AMT As SpreadColProperty = New SpreadColProperty(LMF210C.SprColumnIndex.PAY_AMT, "下払金額", 100, True)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread(ByVal ds As DataSet)

        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim dt As DataTable = ds.Tables(LMF210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.Rows(0)
        Dim trip As String = dr.Item("TRIP_NO").ToString

        With spr
            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = 14

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMF210G.sprDetailDef())

            '列固定位置を設定します。(距離で固定)
            .ActiveSheet.FrozenColumnCount = LMF210G.sprDetailDef.DEF.ColNo + 1

            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sComU As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, "U001", False)
            Dim sComJ As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, "J002", False)
            Dim sTxt122 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 122, False)
            Dim sTxt100 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, False)
            Dim sTxt20 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 20, False)
            Dim hanTxt10 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 10, False)
            Dim hanTxt20 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 20, False)
            Dim hanTxt7 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 7, False)

            'スプレッドの初期設定
            '.SetCellStyle(0, LMF210G.sprDetailDef.DEF.ColNo, sDEF)
            .SetCellStyle(0, LMF210G.sprDetailDef.TRIP_NO.ColNo, hanTxt10)
            .SetCellStyle(0, LMF210G.sprDetailDef.TRIP_DATE.ColNo, sLabel)
            .SetCellStyle(0, LMF210G.sprDetailDef.UNSO_NM.ColNo, sTxt122)
            .SetCellStyle(0, LMF210G.sprDetailDef.CAR_NO.ColNo, hanTxt20)
            .SetCellStyle(0, LMF210G.sprDetailDef.BIN.ColNo, sComU)
            .SetCellStyle(0, LMF210G.sprDetailDef.JSHA_NM.ColNo, sComJ)
            .SetCellStyle(0, LMF210G.sprDetailDef.REMARK.ColNo, sTxt100)
            .SetCellStyle(0, LMF210G.sprDetailDef.DRIVER_NM.ColNo, sTxt20)
            .SetCellStyle(0, LMF210G.sprDetailDef.UNSO_PKG_NB.ColNo, sLabel)
            .SetCellStyle(0, LMF210G.sprDetailDef.UNSO_WT.ColNo, sLabel)
            .SetCellStyle(0, LMF210G.sprDetailDef.LOAD_WT.ColNo, sLabel)
            .SetCellStyle(0, LMF210G.sprDetailDef.ON_KAN.ColNo, sLabel)
            .SetCellStyle(0, LMF210G.sprDetailDef.PAY_AMT.ColNo, sLabel)


            '運送番号が入力されているときは入力
            If String.IsNullOrEmpty(trip) = False Then
                .SetCellValue(0, LMF210G.sprDetailDef.TRIP_NO.ColNo, trip)
            End If

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dt As DataTable = ds.Tables(LMF210C.TABLE_NM_OUT)
        Dim row As Integer = 0

        'ロック制御
        Dim lock As Boolean = True

        With spr

            '行クリア
            .CrearSpread()

            .SuspendLayout()

            '----データ挿入----'
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count()
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sNum10 As StyleInfo = Me.StyleInfoNum10(spr, lock)
            Dim sNum9d3 As StyleInfo = Me.StyleInfoNum9dec3(spr, lock)
            Dim sNum9 As StyleInfo = Me.StyleInfoNum9(spr, lock)
            Dim sNum2 As StyleInfo = Me.StyleInfoNum2(spr, lock)
            Dim sNumMax As StyleInfo = Me.StyleInfoNumMax(spr)
          

            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                '積載重量がNULLの時「０」を設定
                If String.IsNullOrEmpty(dr.Item("LOAD_WT").ToString()) = True Then

                    dr.Item("LOAD_WT") = "0"

                End If

                .SetCellStyle(i, LMF210G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMF210G.sprDetailDef.TRIP_NO.ColNo, sLabel)
                .SetCellStyle(i, LMF210G.sprDetailDef.TRIP_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMF210G.sprDetailDef.UNSO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF210G.sprDetailDef.CAR_NO.ColNo, sLabel)
                .SetCellStyle(i, LMF210G.sprDetailDef.BIN.ColNo, sLabel)
                .SetCellStyle(i, LMF210G.sprDetailDef.JSHA_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF210G.sprDetailDef.REMARK.ColNo, sLabel)
                .SetCellStyle(i, LMF210G.sprDetailDef.DRIVER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF210G.sprDetailDef.UNSO_PKG_NB.ColNo, sNum10)
                .SetCellStyle(i, LMF210G.sprDetailDef.UNSO_WT.ColNo, sNum9d3)
                .SetCellStyle(i, LMF210G.sprDetailDef.LOAD_WT.ColNo, sNum9d3)
                .SetCellStyle(i, LMF210G.sprDetailDef.ON_KAN.ColNo, sNUm2)
                .SetCellStyle(i, LMF210G.sprDetailDef.PAY_AMT.ColNo, sNumMax)

                ''セルに値を設定
                .SetCellValue(i, LMF210G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMF210G.sprDetailDef.TRIP_NO.ColNo, dr.Item("TRIP_NO").ToString())
                .SetCellValue(i, LMF210G.sprDetailDef.TRIP_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("TRIP_DATE").ToString()))
                .SetCellValue(i, LMF210G.sprDetailDef.UNSO_NM.ColNo, String.Concat(dr.Item("UNSOCO_NM").ToString(), "　", dr.Item("UNSOCO_BR_NM").ToString()))
                .SetCellValue(i, LMF210G.sprDetailDef.CAR_NO.ColNo, dr.Item("CAR_NO").ToString())
                .SetCellValue(i, LMF210G.sprDetailDef.BIN.ColNo, dr.Item("BIN").ToString())
                .SetCellValue(i, LMF210G.sprDetailDef.JSHA_NM.ColNo, dr.Item("JSHA_NM").ToString())
                .SetCellValue(i, LMF210G.sprDetailDef.REMARK.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue(i, LMF210G.sprDetailDef.DRIVER_NM.ColNo, dr.Item("DRIVER_NM").ToString())
                .SetCellValue(i, LMF210G.sprDetailDef.UNSO_PKG_NB.ColNo, dr.Item("UNSO_PKG_NB").ToString())
                .SetCellValue(i, LMF210G.sprDetailDef.UNSO_WT.ColNo, dr.Item("UNSO_WT").ToString())
                .SetCellValue(i, LMF210G.sprDetailDef.LOAD_WT.ColNo, dr.Item("LOAD_WT").ToString())
                .SetCellValue(i, LMF210G.sprDetailDef.ON_KAN.ColNo, dr.Item("UNSO_ONDO").ToString())
                .SetCellValue(i, LMF210G.sprDetailDef.PAY_AMT.ColNo, dr.Item("PAY_UNCHIN").ToString())


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
    Private Function StyleInfoLabel(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(英大数)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextEidaisu(ByVal spr As LMSpread, ByVal length As Integer) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA_U, length, False)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(MIX)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextMix(ByVal spr As LMSpread, ByVal length As Integer) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, length, False)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数10桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum10(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, , ",")
    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数12桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum12(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999999, True, 0, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数9桁　少数3桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum9dec3(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, True, 3, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数5桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum9(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999, True, 0, )

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数5桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum2(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, -99, 99, True, 0, )

    End Function
    ''' <summary>
    ''' セルのプロパティを設定(Number 整数最大桁[14])
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNumMax(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, LMFControlC.MAX_KETA_SPR, True, 0, , ",")

    End Function



#End Region

#End Region

#End Region

End Class
