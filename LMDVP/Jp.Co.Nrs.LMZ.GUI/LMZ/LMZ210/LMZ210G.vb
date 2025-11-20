' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ210G : 商品マスタ照会
'  作  成  者       :  kishi
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMZ210Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ210G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ210F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMZConG As LMZControlG


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ210F)

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

#Region "Form"

    ''' <summary>
    ''' 画面ヘッダー部値設定
    ''' </summary>
    ''' <param name="drow">荷主キャッシュから取得したデータロウ配列</param>
    ''' <remarks></remarks>
    Friend Sub CustHeaderDataSet(ByVal drow As DataRow)

        With _Frm

            .lblCustCdL.TextValue = drow("CUST_CD_L").ToString()
            .lblCustNmL.TextValue = drow("CUST_NM_L").ToString()

        End With

    End Sub

    ''' <summary>
    ''' 画面ヘッダー部ロック処理を行う
    ''' </summary>
    ''' <param name="lock">trueはロック処理</param>
    ''' <remarks></remarks>
    Friend Sub LockControl(ByVal lock As Boolean)

        With Me._Frm

            Me.SetLockControl(.cmbNrsBrCd, lock)
            Me.SetLockControl(.lblCustCdL, lock)
            Me.SetLockControl(.lblCustNmL, lock)
            Me.SetLockControl(.chkRelateFlg, lock)
            .sprDetail.Enabled = Not lock

        End With

    End Sub

    ''' <summary>
    ''' ファンクションキーのロック処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub FunctionKeyLock()

        With Me._Frm.FunctionKey
            .F9ButtonEnabled = False
            .F10ButtonEnabled = False
            .F11ButtonEnabled = False
            .F12ButtonEnabled = True

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

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
        'START YANAI 要望番号881
        'Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMZ210C.SprColumnIndex.DEF, " ", 20, True)
        'Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMZ210C.SprColumnIndex.DEST_NM, "届先名", 300, True)
        'Public Shared AD_1 As SpreadColProperty = New SpreadColProperty(LMZ210C.SprColumnIndex.AD_1, "住所", 390, True)
        'Public Shared DEST_CD As SpreadColProperty = New SpreadColProperty(LMZ210C.SprColumnIndex.DEST_CD, "届先コード", 150, True)
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMZ210C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMZ210C.SprColumnIndex.DEST_NM, "届先名", 175, True)
        Public Shared AD_1 As SpreadColProperty = New SpreadColProperty(LMZ210C.SprColumnIndex.AD_1, "住所", 175, True)

#If False Then ' フィルメニッヒ セミEDI対応  20160930 changed inoue
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMZ210C.SprColumnIndex.REMARK, "備考", 175, True)
        Public Shared DEST_CD As SpreadColProperty = New SpreadColProperty(LMZ210C.SprColumnIndex.DEST_CD, "届先コード", 150, True)
#Else
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMZ210C.SprColumnIndex.REMARK, "備考", 125, True)
        Public Shared DEST_CD As SpreadColProperty = New SpreadColProperty(LMZ210C.SprColumnIndex.DEST_CD, "届先コード", 90, True)
        Public Shared DELI_ATT As SpreadColProperty = New SpreadColProperty(LMZ210C.SprColumnIndex.DELI_ATT, "配送時注意事項", 125, True)
#End If



        'END YANAI 要望番号881
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMZ210C.SprColumnIndex.ROW_INDEX, "行番号", 10, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread(ByVal drow As DataRow)

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
#If False Then ' フィルメニッヒ セミEDI対応  20160930 changed inoue 
            'START YANAI 要望番号881
            '.sprDetail.ActiveSheet.ColumnCount = 5
            .sprDetail.ActiveSheet.ColumnCount = 6
            'END YANAI 要望番号881
#Else
            .sprDetail.ActiveSheet.ColumnCount = LMZ210C.SprColumnIndex.COLUMN_COUNT
#End If

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New LMZ210G.sprDetailDef())
            .sprDetail.SetColProperty(New LMZ210G.sprDetailDef(), False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMZ210G.sprDetailDef.DEF.ColNo + 1

            '列設定
            .sprDetail.SetCellStyle(0, LMZ210G.sprDetailDef.DEST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 80, False))
            .sprDetail.SetCellStyle(0, LMZ210G.sprDetailDef.AD_1.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 120, False))

            'START YANAI 要望番号881
            .sprDetail.SetCellStyle(0, LMZ210G.sprDetailDef.REMARK.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 100, False))
            'END YANAI 要望番号881

            .sprDetail.SetCellStyle(0, LMZ210G.sprDetailDef.DEST_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 15, False))
            .sprDetail.SetCellStyle(0, LMZ210G.sprDetailDef.ROW_INDEX.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))

#If True Then ' フィルメニッヒ セミEDI対応  20160930 added inoue
            .sprDetail.SetCellStyle(0, LMZ210G.sprDetailDef.DELI_ATT.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 100, False))
#End If

        End With


        Call Me.SetInitValue(drow)


    End Sub


    ''' <summary>
    ''' 画面初期値設定(スプレッド)
    ''' </summary>
    ''' <param name="drow"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal drow As DataRow)

        With Me._Frm.sprDetail

            .SetCellValue(0, LMZ210G.sprDetailDef.DEST_NM.ColNo, drow("DEST_NM").ToString())
            .SetCellValue(0, LMZ210G.sprDetailDef.AD_1.ColNo, drow("AD_1").ToString())
            'START YANAI 要望番号881
            .SetCellValue(0, LMZ210G.sprDetailDef.REMARK.ColNo, drow("REMARK").ToString())
            'END YANAI 要望番号881

            .SetCellValue(0, LMZ210G.sprDetailDef.DEST_CD.ColNo, drow("DEST_CD").ToString())

#If True Then ' フィルメニッヒ セミEDI対応  20160930 added inoue
            .SetCellValue(0, LMZ210G.sprDetailDef.DELI_ATT.ColNo, drow("DELI_ATT").ToString())
#End If

        End With

        Dim rltFlg As String = drow("RELATION_SHOW_FLG").ToString()
        With Me._Frm

            .cmbNrsBrCd.SelectedValue = drow("NRS_BR_CD").ToString()
            .lblCustCdL.TextValue = drow("CUST_CD_L").ToString()
            .chkRelateFlg.SetBinaryValue(rltFlg)
            '関連表示フラグ＝1（あり）のときロック解除
            If rltFlg.Equals(LMConst.FLG.ON) = True Then
                Me.SetLockControl(.chkRelateFlg, False)
            Else
                Me.SetLockControl(.chkRelateFlg, True)
            End If
        End With


    End Sub


    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As DataSet = New DataSet()

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()

            .SuspendLayout()

            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            'キーボード操作でチェックボックスＯＮ
            .KeyboardCheckBoxOn = True

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            Dim dRow As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dRow = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprDetailDef.DEST_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.AD_1.ColNo, sLabel)
                'START YANAI 要望番号881
                .SetCellStyle(i, sprDetailDef.REMARK.ColNo, sLabel)
                'END YANAI 要望番号881

                .SetCellStyle(i, sprDetailDef.DEST_CD.ColNo, sLabel)

#If True Then ' フィルメニッヒ セミEDI対応  20160930 added inoue
                .SetCellStyle(i, sprDetailDef.DELI_ATT.ColNo, sLabel)
#End If

                .SetCellStyle(i, sprDetailDef.ROW_INDEX.ColNo, sLabel)     '行番号

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.DEST_NM.ColNo, dRow.Item("DEST_NM").ToString())
                .SetCellValue(i, sprDetailDef.AD_1.ColNo, String.Concat(dRow.Item("AD_1").ToString() _
                                                                        , dRow.Item("AD_2").ToString() _
                                                                        , dRow.Item("AD_3").ToString()))
                'START YANAI 要望番号881
                .SetCellValue(i, sprDetailDef.REMARK.ColNo, dRow.Item("REMARK").ToString())
                'END YANAI 要望番号881
                .SetCellValue(i, sprDetailDef.DEST_CD.ColNo, dRow.Item("DEST_CD").ToString())

#If True Then ' フィルメニッヒ セミEDI対応  20160930 added inoue
                .SetCellValue(i, sprDetailDef.DELI_ATT.ColNo, dRow.Item("DELI_ATT").ToString())
#End If

                .SetCellValue(i, sprDetailDef.ROW_INDEX.ColNo, Convert.ToString(i - 1))
            Next

            .ResumeLayout(True)

        End With


    End Sub

#End Region 'Spread

#Region "部品"

    ''' <summary>
    ''' ロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="ctl">制御対象項目</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub SetLockControl(ByVal ctl As Control, Optional ByVal lockFlg As Boolean = False)

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

#End Region

#End Region

End Class
