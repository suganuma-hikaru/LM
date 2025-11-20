' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF200G : 運行未登録一覧
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
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李
Imports Jp.Co.Nrs.Win.Base   '2017/09/25 追加 李

''' <summary>
''' LMF200Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF200G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF200F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconG As LMFControlG


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF200F, ByVal g As LMFControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMFconG = g

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
            .F10ButtonName = "マスタ参照"
            .F11ButtonName = "Ｏ　Ｋ"
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F9ButtonEnabled = always
            .F10ButtonEnabled = always
            .F11ButtonEnabled = always
            .F12ButtonEnabled = always

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

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

            .cmbEigyo.TabIndex = LMF200C.CtlTabIndex.EIGYO
            .cmbBetsuEigyo.TabIndex = LMF200C.CtlTabIndex.YOSO
            .txtUnsocoCd.TabIndex = LMF200C.CtlTabIndex.UnsocoCd
            .txtUnsocoBrCd.TabIndex = LMF200C.CtlTabIndex.UnsocoBrCd
            .lblUnsocoNm.TabIndex = LMF200C.CtlTabIndex.UnsocoNm
            .imdArrDateFrom.TabIndex = LMF200C.CtlTabIndex.ArrDate

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

    End Sub

    ''' <summary>
    '''営業所コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetcmbNrsBrCd()

        Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
        Me._Frm.cmbEigyo.SelectedValue = brCd
        Me._Frm.cmbBetsuEigyo.SelectedValue = brCd

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        With Me._Frm

            Dim Unsoco As String = .txtUnsocoCd.TextValue.ToString
            Dim UnsocoBr As String = .txtUnsocoBrCd.TextValue.ToString
            Dim ArrFrom As String = .imdArrDateFrom.TextValue.ToString
            Dim ArrTo As String = .imdArrDateTo.TextValue.ToString

            '(2012.11.09)要望番号1462 運送会社１次の絞り込み不要 -- START --

            '全項目ロック解除
            Me.LockControl(False)

            '納入予定日From、納入予定日Toが入力されている時は、納入予定日From、納入予定日Toはロックする
            If String.IsNullOrEmpty(ArrFrom) = False AndAlso _
               String.IsNullOrEmpty(ArrTo) = False Then
                Me.SetLockControl(.imdArrDateFrom, True)
                Me.SetLockControl(.imdArrDateTo, True)
            End If

            ''運送コード、運送支社コードが入力されているとき
            'If String.IsNullOrEmpty(Unsoco) = False AndAlso _
            'String.IsNullOrEmpty(UnsocoBr) = False Then

            '    '納入予定日From、納入予定日Toが入力されているとき
            '    If String.IsNullOrEmpty(ArrFrom) = False AndAlso _
            '       String.IsNullOrEmpty(ArrTo) = False Then
            '        Me.LockControl(True)
            '        Me.SetLockControl(.cmbEigyo, False)

            '    Else
            '        Me.LockControl(False)

            '    End If
            'Else
            '    Me.LockControl(False)

            'End If

            '(2012.11.09)要望番号1462 運送会社１次の絞り込み不要 --  END  --

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbEigyo.SelectedValue = Nothing
            .cmbBetsuEigyo.SelectedValue = Nothing
            .txtUnsocoCd.TextValue = String.Empty
            .txtUnsocoBrCd.TextValue = String.Empty
            .lblUnsocoNm.TextValue = String.Empty
            .imdArrDateFrom.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal dr As DataRow)

        With Me._Frm

            'パラムのdrから納入予定日Fromの取得
            Dim arrDataFrom As String = dr.Item("ARR_PLAN_DATE_FROM").ToString()

            '画面の納入予定日Fromに取得した内容を設定
            .imdArrDateFrom.TextValue = arrDataFrom

            '画面の納入予定日Toに取得した内容を設定
            .imdArrDateTo.TextValue = arrDataFrom

            '(2012.11.09)要望番号1462 運送会社１次の絞り込み不要 -- START --

            .txtUnsocoCd.TextValue = String.Empty
            .txtUnsocoBrCd.TextValue = String.Empty
            .lblUnsocoNm.TextValue = String.Empty

            '2014.08.04 FFEM高取対応 START
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

            If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
                .cmbEigyo.ReadOnly = True
            Else
                .cmbEigyo.ReadOnly = False
            End If
            '2014.08.04 FFEM高取対応 END

            'LMF030から運送会社コード、運送支社コードが入力されていたら、そのまま表示
            'If String.IsNullOrEmpty(dr.Item("UNSO_CD").ToString()) = False AndAlso _
            '   String.IsNullOrEmpty(dr.Item("UNSO_BR_CD").ToString()) = False Then

            '    '運送会社の設定
            '    .txtUnsocoCd.TextValue = dr.Item("UNSO_CD").ToString()
            '    .txtUnsocoBrCd.TextValue = dr.Item("UNSO_BR_CD").ToString()
            '    .lblUnsocoNm.TextValue = dr.Item("UNSO_NM").ToString()

            'End If

            '(2012.11.09)要望番号1462 運送会社１次の絞り込み不要 --  END  --

            Dim spr As LMSpread = .sprDetail
            Dim max As Integer = spr.ActiveSheet.Columns.Count - 1

            For i As Integer = 1 To max
                spr.SetCellValue(0, i, String.Empty)
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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared UNSO_NO As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.UNSO_NO, "運送番号", 70, True)
        Public Shared BINKBN As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.BINKBN, "便区分", 100, True)
        Public Shared UNSO_TEHAI_KBN As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.UNSO_TEHAI_KBN, "タリフ分類", 100, True)
        Public Shared CUST_REF_NO As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.CUST_REF_NO, "荷主" & vbCrLf & "参照番号", 90, True)
        Public Shared ORIG_NM As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.ORIG_NM, "発地名", 90, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.DEST_NM, "届先名", 300, True)     'UPD 2022/08/29 サイズ90→300
        Public Shared DEST_ADD As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.DEST_ADD, "届先住所名", 270, True)     'ADD 2022/08/29  032102           
        Public Shared TASYA_WH_NM As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.TASYA_WH_NM, "製品置き場" & vbCrLf & "（他社倉庫名称）", 140, True)
        Public Shared ARIA As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.ARIA, "エリア", 90, True)
        Public Shared KOSU As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.KOSU, "総個数", 60, True)
        Public Shared JURYO As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.JURYO, "重量", 60, True)
        Public Shared KANRI_NO As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.KANRI_NO, "管理" & vbCrLf & "番号", 70, True)
        Public Shared NONYUDATE As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.NONYUDATE, "納入予定", 80, True)
        Public Shared UNSOCO_NM As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.UNSOCO_NM, "運送会社(1次)名", 140, True)
        Public Shared CUST_NM As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.CUST_NM, "荷主名", 120, True)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.REMARK, "備考", 120, True)
        Public Shared UNCHIN As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.UNCHIN, "運賃", 100, True)
        Public Shared KYORI As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.KYORI, "距離", 60, True)
        Public Shared GROUP_NO As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.GROUP_NO, "まとめ番号", 60, True)
        Public Shared ONKAN As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.ONKAN, "温管", 140, True)
        Public Shared MOTO_DATA_KBN As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.MOTO_DATA_KBN, "元データ" & vbCrLf & "区分", 80, True)
        Public Shared SHUNI_TI As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.SHUNI_TI, "集荷" & vbCrLf & "中継地", 80, True)
        Public Shared HAINI_TI As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.HAINI_TI, "配荷" & vbCrLf & "中継地", 80, True)
        Public Shared TRIP_NO_SHUKA As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.TRIP_NO_SHUKA, "運行番号(集荷)", 130, True)
        Public Shared TRIP_NO_CHUKEI As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.TRIP_NO_CHUKEI, "運行番号(中継)", 130, True)
        Public Shared TRIP_NO_HAIKA As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.TRIP_NO_HAIKA, "運行番号(配荷)", 130, True)
        Public Shared UNSOCO_NM_SHUKA As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.UNSOCO_NM_SHUKA, "運送会社(集荷)", 130, True)
        Public Shared UNSOCO_NM_CHUKEI As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.UNSOCO_NM_CHUKEI, "運送会社(中継)", 130, True)
        Public Shared UNSOCO_NM_HAIKA As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.UNSOCO_NM_HAIKA, "運送会社(配荷)", 130, True)

        '隠し項目
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.SYS_ENT_DATE, "作成日", 0, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 0, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.SYS_UPD_DATE, "更新日", 0, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 0, False)
        Public Shared ROW_NO As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.ROW_NO, "行番号", 0, False)
        Public Shared UNSO_CD As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.UNSO_CD, "運送コード", 0, False)
        Public Shared UNSO_BR_CD As SpreadColProperty = New SpreadColProperty(LMF200C.SprColumnIndex.UNSO_BR_CD, "運送支社コード", 0, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim spr As LMSpread = Me._Frm.sprDetail

        With spr
            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = 37   'UPD 2022/08/29 35→36

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New LMF200G.sprDetailDef())
            '.SetColProperty(New LMF200G.sprDetailDef(), False)
            .SetColProperty(New LMF200G.sprDetailDef(), True)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(チェックボックスで固定)
            .ActiveSheet.FrozenColumnCount = LMF200G.sprDetailDef.UNSO_TEHAI_KBN.ColNo + 1


            'ロックフラグ
            Dim lock As Boolean = False

            '運送会社コード、支社コードがロックされている場合、スプレッドの運送会社名をロック
            If Me._Frm.txtUnsocoCd.ReadOnly = True AndAlso _
            Me._Frm.txtUnsocoBrCd.ReadOnly = True Then


                'スプレッドの運送会社名をロック
                lock = True

            Else

                'スプレッドの運送会社名をロック解除
                lock = False

            End If


            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim uCom As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, "U001", False)
            Dim tCom As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, "T015", False)
            Dim uCom6 As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, "U006", False)

            '2017/09/25 修正 李↓
            Dim mCom As StyleInfo = LMSpreadUtility.GetComboCellMaster(spr, LMConst.CacheTBL.KBN, "KBN_CD",
                                                                       lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"}),
                                                                       False, New String() {"KBN_GROUP_CD", "VALUE1"}, New String() {LMKbnConst.KBN_M004, "1.000"}, LMConst.JoinCondition.AND_WORD)
            '2017/09/25 修正 李↑

            Dim mTxt As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, False)
            Dim mTxt80 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 80, False)
            Dim mTxt40 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 40, False)
            Dim mTxt20 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 20, False)
            Dim mTxt122 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 122, False)
            Dim mTxt122t As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 122, lock)
            Dim mTxt50 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 50, False)
            Dim stxt7 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 7, False)
            Dim stxt9 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 9, False)
            Dim stxt10 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 10, False)
            Dim stxt30 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 30, False)



            'スプレッドの初期設定
            .SetCellStyle(0, LMF200G.sprDetailDef.UNSO_NO.ColNo, stxt9)
            .SetCellStyle(0, LMF200G.sprDetailDef.BINKBN.ColNo, uCom)
            .SetCellStyle(0, LMF200G.sprDetailDef.UNSO_TEHAI_KBN.ColNo, tCom)
            .SetCellStyle(0, LMF200G.sprDetailDef.CUST_REF_NO.ColNo, stxt30)
            .SetCellStyle(0, LMF200G.sprDetailDef.ORIG_NM.ColNo, mTxt80)
            .SetCellStyle(0, LMF200G.sprDetailDef.DEST_NM.ColNo, mTxt80)
            .SetCellStyle(0, LMF200G.sprDetailDef.DEST_ADD.ColNo, mTxt80)   'ADD 2022/08/29
            .SetCellStyle(0, LMF200G.sprDetailDef.TASYA_WH_NM.ColNo, mTxt122)
            .SetCellStyle(0, LMF200G.sprDetailDef.ARIA.ColNo, mTxt20)
            .SetCellStyle(0, LMF200G.sprDetailDef.KOSU.ColNo, sLabel)
            .SetCellStyle(0, LMF200G.sprDetailDef.JURYO.ColNo, sLabel)
            .SetCellStyle(0, LMF200G.sprDetailDef.KANRI_NO.ColNo, stxt9)
            .SetCellStyle(0, LMF200G.sprDetailDef.NONYUDATE.ColNo, sLabel)
            .SetCellStyle(0, LMF200G.sprDetailDef.UNSOCO_NM.ColNo, mTxt122t)
            .SetCellStyle(0, LMF200G.sprDetailDef.CUST_NM.ColNo, mTxt122)
            .SetCellStyle(0, LMF200G.sprDetailDef.REMARK.ColNo, mTxt)
            .SetCellStyle(0, LMF200G.sprDetailDef.UNCHIN.ColNo, sLabel)
            .SetCellStyle(0, LMF200G.sprDetailDef.KYORI.ColNo, sLabel)
            .SetCellStyle(0, LMF200G.sprDetailDef.GROUP_NO.ColNo, stxt9)
            .SetCellStyle(0, LMF200G.sprDetailDef.ONKAN.ColNo, uCom6)
            .SetCellStyle(0, LMF200G.sprDetailDef.MOTO_DATA_KBN.ColNo, mCom)
            .SetCellStyle(0, LMF200G.sprDetailDef.SHUNI_TI.ColNo, mTxt50)
            .SetCellStyle(0, LMF200G.sprDetailDef.HAINI_TI.ColNo, mTxt50)
            .SetCellStyle(0, LMF200G.sprDetailDef.TRIP_NO_SHUKA.ColNo, stxt10)
            .SetCellStyle(0, LMF200G.sprDetailDef.TRIP_NO_CHUKEI.ColNo, stxt10)
            .SetCellStyle(0, LMF200G.sprDetailDef.TRIP_NO_HAIKA.ColNo, stxt10)
            .SetCellStyle(0, LMF200G.sprDetailDef.UNSOCO_NM_SHUKA.ColNo, mTxt122)
            .SetCellStyle(0, LMF200G.sprDetailDef.UNSOCO_NM_CHUKEI.ColNo, mTxt122)
            .SetCellStyle(0, LMF200G.sprDetailDef.UNSOCO_NM_HAIKA.ColNo, mTxt122)

            '隠し項目
            .SetCellStyle(0, LMF200G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
            .SetCellStyle(0, LMF200G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
            .SetCellStyle(0, LMF200G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
            .SetCellStyle(0, LMF200G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
            .SetCellStyle(0, LMF200G.sprDetailDef.ROW_NO.ColNo, sLabel)
            .SetCellStyle(0, LMF200G.sprDetailDef.UNSO_CD.ColNo, sLabel)
            .SetCellStyle(0, LMF200G.sprDetailDef.UNSO_BR_CD.ColNo, sLabel)


        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dt As DataTable = ds.Tables(LMF200C.TABLE_NM_OUT)
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
            Dim sNum12d2 As StyleInfo = Me.StyleInfoNum12d2(spr, lock)
            Dim sNum5 As StyleInfo = Me.StyleInfoNum5(spr, lock)
            Dim sNum12d3 As StyleInfo = Me.StyleInfoNum12(spr, lock)


            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                .SetCellStyle(i, LMF200G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMF200G.sprDetailDef.UNSO_NO.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.BINKBN.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.UNSO_TEHAI_KBN.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.CUST_REF_NO.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.ORIG_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.DEST_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.DEST_ADD.ColNo, sLabel)   'ADD 2022/08/29
                .SetCellStyle(i, LMF200G.sprDetailDef.TASYA_WH_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.ARIA.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.KOSU.ColNo, sNum10)
                .SetCellStyle(i, LMF200G.sprDetailDef.JURYO.ColNo, sNum12d3)
                .SetCellStyle(i, LMF200G.sprDetailDef.KANRI_NO.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.NONYUDATE.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.UNSOCO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.CUST_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.REMARK.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.UNCHIN.ColNo, sNum12d2)
                .SetCellStyle(i, LMF200G.sprDetailDef.KYORI.ColNo, sNum5)
                .SetCellStyle(i, LMF200G.sprDetailDef.GROUP_NO.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.ONKAN.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.MOTO_DATA_KBN.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.SHUNI_TI.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.HAINI_TI.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.TRIP_NO_SHUKA.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.TRIP_NO_CHUKEI.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.TRIP_NO_HAIKA.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.UNSOCO_NM_SHUKA.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.UNSOCO_NM_CHUKEI.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.UNSOCO_NM_HAIKA.ColNo, sLabel)

                '隠し項目
                .SetCellStyle(i, LMF200G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.UNSO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.UNSO_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMF200G.sprDetailDef.ROW_NO.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMF200G.sprDetailDef.UNSO_NO.ColNo, dr.Item("UNSO_NO_L").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.BINKBN.ColNo, dr.Item("BIN_NM").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.UNSO_TEHAI_KBN.ColNo, dr.Item("TARIFF_BUNRUI_NM").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.CUST_REF_NO.ColNo, dr.Item("CUST_REF_NO").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.ORIG_NM.ColNo, dr.Item("ORIG_NM").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.DEST_ADD.ColNo, dr.Item("DEST_AD_1").ToString()) 'ADD 2022/08/29
                .SetCellValue(i, LMF200G.sprDetailDef.TASYA_WH_NM.ColNo, dr.Item("TASYA_WH_NM").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.ARIA.ColNo, dr.Item("AREA_NM").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.KOSU.ColNo, dr.Item("UNSO_PKG_NB").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.JURYO.ColNo, dr.Item("UNSO_WT").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.KANRI_NO.ColNo, dr.Item("INOUTKA_NO_L").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.NONYUDATE.ColNo, DateFormatUtility.EditSlash(dr.Item("ARR_PLAN_DATE").ToString()))
                .SetCellValue(i, LMF200G.sprDetailDef.UNSOCO_NM.ColNo, String.Concat(dr.Item("UNSO_NM").ToString(), "　", dr.Item("UNSOCO_BR_NM").ToString()))
                .SetCellValue(i, LMF200G.sprDetailDef.CUST_NM.ColNo, String.Concat(dr.Item("CUST_NM_L").ToString(), "　", dr.Item("CUST_NM_M").ToString()))
                .SetCellValue(i, LMF200G.sprDetailDef.REMARK.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.UNCHIN.ColNo, dr.Item("UNCHIN").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.KYORI.ColNo, dr.Item("SEIQ_KYORI").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.GROUP_NO.ColNo, dr.Item("SEIQ_GROUP_NO").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.ONKAN.ColNo, dr.Item("UNSO_ONDO_NM").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.MOTO_DATA_KBN.ColNo, dr.Item("MOTO_DATA_NM").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.SHUNI_TI.ColNo, dr.Item("SYUKA_TYUKEI_NM").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.HAINI_TI.ColNo, dr.Item("HAIKA_TYUKEI_NM").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.TRIP_NO_SHUKA.ColNo, dr.Item("TRIP_NO_SYUKA").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.TRIP_NO_CHUKEI.ColNo, dr.Item("TRIP_NO_TYUKEI").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.TRIP_NO_HAIKA.ColNo, dr.Item("TRIP_NO_HAIKA").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.UNSOCO_NM_SHUKA.ColNo, String.Concat(dr.Item("UNSO_SYUKA_NM").ToString(), "　", dr.Item("UNSO_SYUKA_BR_NM").ToString()))
                .SetCellValue(i, LMF200G.sprDetailDef.UNSOCO_NM_CHUKEI.ColNo, String.Concat(dr.Item("UNSO_TYUKEI_NM").ToString(), "　", dr.Item("UNSO_TYUKEI_BR_NM").ToString()))
                .SetCellValue(i, LMF200G.sprDetailDef.UNSOCO_NM_HAIKA.ColNo, String.Concat(dr.Item("UNSO_HAIKA_NM").ToString(), "　", dr.Item("UNSO_HAIKA_BR_NM").ToString()))

                '隠し項目
                .SetCellValue(i, LMF200G.sprDetailDef.SYS_ENT_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("SYS_ENT_DATE").ToString()))
                .SetCellValue(i, LMF200G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.ROW_NO.ColNo, (i - 1).ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.UNSO_CD.ColNo, dr.Item("UNSO_CD").ToString())
                .SetCellValue(i, LMF200G.sprDetailDef.UNSO_BR_CD.ColNo, dr.Item("UNSO_BR_CD").ToString())

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

        Return LMSpreadUtility.GetNumberCell(spr, 0, 99999999999, True, 0, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数12桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum12(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999999.999, True, 3, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数12桁 少数2桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum12d2(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 99999999999999, True, 0, , ",")

    End Function


    ''' <summary>
    ''' セルのプロパティを設定(Number 整数5桁 )
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum5(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 99999, True, 0, , ",")

    End Function


#End Region

#Region "部品化検討中"

    ''' <summary>
    ''' 画面編集部ロック処理を行う
    ''' </summary>
    ''' <param name="lock">trueはロック処理</param>
    ''' <remarks></remarks>
    Friend Sub LockControl(ByVal lock As Boolean)

        With Me._Frm

            Me.SetLockControl(.cmbEigyo, lock)
            Me.SetLockControl(.txtUnsocoCd, lock)
            Me.SetLockControl(.txtUnsocoBrCd, lock)
            Me.SetLockControl(.imdArrDateFrom, lock)
            Me.SetLockControl(.imdArrDateTo, lock)
           
        End With

    End Sub

    ''' <summary>
    ''' ファンクションキーロック処理を行う
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub LockFunctionKey()

        Me.SetLockControl(Me._Frm.FunctionKey, True)

    End Sub

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
