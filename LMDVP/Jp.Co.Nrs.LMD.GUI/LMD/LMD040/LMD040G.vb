' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMD     : 在庫
'  プログラムID   : LMD040G : 
'  作  成  者     : 大貫和正
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.Win.Base   '2017/09/25 追加 李
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李

''' <summary>
''' LMD040Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD040G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD040F

    Friend objSprDef As Object = Nothing
    Friend sprGenzaikoDef As sprGenzaikoDefault

    '2017/09/25 修正 李↓
    ''2016.02.05 追加START
    ' ''' <summary>
    ' ''' 選択した言語を格納するフィールド
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _LangFlg As String = Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage
    ''2016.02.05 追加END
    '2017/09/25 修正 李↑

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMD040F)

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
    Friend Sub SetFunctionKey(ByVal mode As String)
        Dim always As Boolean = True

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True
            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = "入出荷編集"
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = "検　索"
            .F10ButtonName = "マスタ参照"
            .F11ButtonName = "初期荷主変更"
            .F12ButtonName = "閉じる"
            'ファンクションキーの制御
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = always
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = True
            .F10ButtonEnabled = always
            .F11ButtonEnabled = always
            .F12ButtonEnabled = always

            Select Case mode
                Case Me._Frm.tabGenZaiko.Name
                    .F5ButtonEnabled = False
            End Select
            If LMD040C.MODE_DEFAULT.Equals(mode) = True Then
                .F5ButtonEnabled = False
            End If

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region 'FunctionKey

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm
            .cmbEigyo.TabIndex = LMD040C.CtlTabIndex.EIGYO
            .cmbSoko.TabIndex = LMD040C.CtlTabIndex.SOKO
            .cmbPrint.TabIndex = LMD040C.CtlTabIndex.PRINT
            .btnPrint.TabIndex = LMD040C.CtlTabIndex.PRINT_BTN
            .imdNyukaFrom.TabIndex = LMD040C.CtlTabIndex.NYUKAFROM
            .imdNyukaTo.TabIndex = LMD040C.CtlTabIndex.NYUKATO
            .chkZeroZaiko.TabIndex = LMD040C.CtlTabIndex.ZEROZAIKO
            .txtCust_Cd_L.TabIndex = LMD040C.CtlTabIndex.CUST_CD_L
            .txtCust_Cd_M.TabIndex = LMD040C.CtlTabIndex.CUST_CD_M
            .txtCust_Cd_S.TabIndex = LMD040C.CtlTabIndex.CUST_CD_S
            .txtCust_Cd_SS.TabIndex = LMD040C.CtlTabIndex.CUST_CD_SS
            .optYotei.TabIndex = LMD040C.CtlTabIndex.YOTEI
            .optJikkou.TabIndex = LMD040C.CtlTabIndex.JIKKOU
            .optAll.TabIndex = LMD040C.CtlTabIndex.ALL
            .imdHyoujiFrom.TabIndex = LMD040C.CtlTabIndex.HYOUJIFROM
            .imdHyoujiTo.TabIndex = LMD040C.CtlTabIndex.HYOUJITO
            .optSyousai.TabIndex = LMD040C.CtlTabIndex.SYOUSAI
            .chkSyukkaTorikesi.TabIndex = LMD040C.CtlTabIndex.SYUKKATORIKESHI
            .optKosu.TabIndex = LMD040C.CtlTabIndex.KOSU
            .optKosuZaiko.TabIndex = LMD040C.CtlTabIndex.KOSUZAIKO
        End With
    End Sub

    ''' <summary>
    '''各コンボボックスの初期値設定 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetcmbValue()

        '営業所の設定
        Me._Frm.cmbEigyo.SelectedValue = LMUserInfoManager.GetNrsBrCd()

        '2014.08.04 FFEM高取対応 START
        'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
        Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

        If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
            Me._Frm.cmbEigyo.ReadOnly = True
        Else
            Me._Frm.cmbEigyo.ReadOnly = False
        End If
        '2014.08.04 FFEM高取対応 END

    End Sub

    ''' <summary>
    ''' コントロールに初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitControl(ByVal id As String, ByRef frm As LMD040F)

        '初期荷主情報取得
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TCUST). _
        Select("SYS_DEL_FLG = '0' AND USER_CD = '" & _
               LM.Base.LMUserInfoManager.GetUserID() & "' AND DEFAULT_CUST_YN = '01'")

        '初期値が存在するコントロール
        frm.cmbEigyo.SelectedValue() = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()     '（自）営業所
        frm.cmbSoko.SelectedValue() = LM.Base.LMUserInfoManager.GetWhCd().ToString()         '（自）倉庫

        If getDr.Length() > 0 Then
            frm.txtCust_Cd_L.TextValue = getDr(0).Item("CUST_CD_L").ToString()                  '（初期）荷主コード（大）
            frm.txtCust_Cd_M.TextValue = getDr(0).Item("CUST_CD_M").ToString()                  '（初期）荷主コード（中）
            frm.txtCust_Cd_S.TextValue = String.Empty                                           '（初期）荷主コード（小）
            frm.txtCust_Cd_SS.TextValue = String.Empty                                                  '（初期）荷主コード（極小）

            '荷主情報取得
            Dim getCustDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST). _
            Select(String.Concat("SYS_DEL_FLG = '0' AND ", _
                   "CUST_CD_L = '", getDr(0).Item("CUST_CD_L").ToString(), "' AND ", _
                   "CUST_CD_M = '", getDr(0).Item("CUST_CD_M").ToString(), "'"))
            If getCustDr.Length() > 0 Then
                frm.txtCust_Nm.TextValue = String.Concat(getCustDr(0).Item("CUST_NM_L").ToString(), "　", _
                                                         getCustDr(0).Item("CUST_NM_M").ToString(), "　", _
                                                         getCustDr(0).Item("CUST_NM_S").ToString(), "　", _
                                                         getCustDr(0).Item("CUST_NM_SS").ToString())
            End If
        End If

    End Sub

    ''' <summary>
    ''' コントロールのロック処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetControlLock(ByVal frm As LMD040F, ByVal flg As Boolean)

        With frm
            Me.SetLockControl(.cmbEigyo, flg)
            Me.SetLockControl(.cmbSoko, flg)
            Me.SetLockControl(.cmbPrint, flg)
            Me.SetLockControl(.txtCust_Cd_L, flg)
            Me.SetLockControl(.txtCust_Cd_M, flg)
            Me.SetLockControl(.txtCust_Cd_S, flg)
            Me.SetLockControl(.txtCust_Cd_SS, flg)
            Me.SetLockControl(.imdNyukaFrom, flg)
            Me.SetLockControl(.imdNyukaTo, flg)
            Me.SetLockControl(.chkZeroZaiko, flg)
            Me.SetLockControl(.optYotei, flg)
            Me.SetLockControl(.optJikkou, flg)
            Me.SetLockControl(.optAll, flg)
            Me.SetLockControl(.imdHyoujiFrom, flg)
            Me.SetLockControl(.imdHyoujiTo, flg)

        End With


    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String, ByVal frm As LMD040F)

        '編集部の項目をクリア
        Call Me.ClearControl()

        'コントロールに初期値設定
        Call Me.SetInitControl(id, frm)

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定（LMC020からの遷移時）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlLMC020(ByVal ds As DataSet, ByVal frm As LMD040F)

        '編集部の項目をクリア
        Call Me.ClearControl()

        'コントロールに初期値設定
        Dim dr As DataRow = ds.Tables(LMControlC.LMD040C_TABLE_NM_IN).Rows(0)
        With frm
            .cmbEigyo.SelectedValue() = dr.Item("NRS_BR_CD").ToString()
            .cmbSoko.SelectedValue() = dr.Item("WH_CD").ToString()
            .txtCust_Cd_L.TextValue = dr.Item("CUST_CD_L").ToString()
            .txtCust_Cd_M.TextValue = dr.Item("CUST_CD_M").ToString()

            .sprGenzaiko.SetCellValue(0, sprGenzaikoDef.GOODS_CD_CUST.ColNo, dr.Item("GOODS_CD_CUST").ToString())
            .sprGenzaiko.SetCellValue(0, sprGenzaikoDef.GOODS_NM.ColNo, dr.Item("GOODS_NM").ToString())
            .sprGenzaiko.SetCellValue(0, sprGenzaikoDef.LOT_NO.ColNo, dr.Item("LOT_NO").ToString())

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(Optional ByVal tmpKBN As LMD040C.EventShubetsu = LMD040C.EventShubetsu.DATAIL)

        With Me._Frm

            With Me._Frm
                Select Case tmpKBN
                    Case LMD040C.EventShubetsu.DATAIL
                        .cmbEigyo.Focus()
                    Case LMD040C.EventShubetsu.KENSAKU
                        .tabRireki.Focus()
                End Select

            End With

        End With

    End Sub

    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetNumberControl()

        With Me._Frm

            Dim d9_3 As Decimal = Convert.ToDecimal("999999999.999")
            Dim sharp9_3 As String = "###,###,##0.000"

            .lblIrimeN.SetInputFields(sharp9_3, , 9, 1, , 3, 3, , d9_3, 0)
            .lblIrimeZ.SetInputFields(sharp9_3, , 9, 1, , 3, 3, , d9_3, 0)

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm
            .cmbEigyo.SelectedValue = LMUserInfoManager.GetNrsBrCd()
            .cmbSoko.SelectedValue = String.Empty
            .cmbPrint.SelectedValue = String.Empty
            .txtCust_Cd_L.TextValue = String.Empty
            .cmbSoko.SelectedValue = String.Empty
            .txtCust_Cd_M.TextValue = String.Empty
            .txtCust_Cd_S.TextValue = String.Empty
            .txtCust_Cd_SS.TextValue = String.Empty
            .txtCust_Nm.TextValue = String.Empty
            .imdHyoujiFrom.TextValue = String.Empty
            .imdHyoujiTo.TextValue = String.Empty
            .imdNyukaFrom.TextValue = String.Empty
            .imdNyukaTo.TextValue = String.Empty
            .optAll.Checked = True
            .optSyousai.Checked = True
            .optKosu.Checked = True
            .optKosuZaiko.Checked = True

            Call Me.ClearTabControl(.tabInOutHistoryByInka.Name)
            Call Me.ClearTabControl(.tabInOutHistoryByOutka.Name)

        End With

    End Sub

    ''' <summary>
    ''' 履歴Tabのコントロールクリア
    ''' </summary>
    ''' <param name="tabNm"></param>
    ''' <remarks></remarks>
    Friend Sub ClearTabControl(ByVal tabNm As String)

        With Me._Frm

            Select Case tabNm

                Case .tabInOutHistoryByInka.Name  '入荷ごと

                    .lblGoodsCdCustN.TextValue = String.Empty
                    .lblGoodsNmN.TextValue = String.Empty
                    .lblLotNoN.TextValue = String.Empty
                    .lblIrimeN.TextValue = String.Empty

                Case .tabInOutHistoryByOutka.Name  '在庫ごと

                    .lblGoodsCdCustZ.TextValue = String.Empty
                    .lblGoodsNmZ.TextValue = String.Empty
                    .lblLotNoZ.TextValue = String.Empty
                    .lblIrimeZ.TextValue = String.Empty


            End Select

        End With

    End Sub

    ''' <summary>
    ''' 履歴Tabのコントロールも値をセット
    ''' </summary>
    ''' <param name="tabNm"></param>
    ''' <remarks></remarks>
    Friend Sub SetTabControl(ByVal tabNm As String, ByVal rowNo As Integer)

        'チェック行が存在しない場合、処理スキップ
        If rowNo < 1 Then
            Exit Sub
        End If

        Dim goodsCdCust As String = String.Empty
        Dim goodsNm As String = String.Empty
        Dim lotNo As String = String.Empty
        Dim irime As String = String.Empty

        With Me._Frm.sprGenzaiko.Sheets(0)
            'goodsCdCust = .Cells(rowNo, LMD040C.SprColumnIndex.GOODS_CD_CUST).Value.ToString()
            goodsCdCust = .Cells(rowNo, sprGenzaikoDef.GOODS_CD_CUST.ColNo).Value.ToString()
            'goodsNm = .Cells(rowNo, LMD040C.SprColumnIndex.GOODS_NM).Value.ToString()
            goodsNm = .Cells(rowNo, sprGenzaikoDef.GOODS_NM.ColNo).Value.ToString()
            'lotNo = .Cells(rowNo, LMD040C.SprColumnIndex.LOT_NO).Value.ToString()
            lotNo = .Cells(rowNo, sprGenzaikoDef.LOT_NO.ColNo).Value.ToString()
            'irime = .Cells(rowNo, LMD040C.SprColumnIndex.IRIME).Value.ToString()
            irime = .Cells(rowNo, sprGenzaikoDef.IRIME.ColNo).Value.ToString()
        End With

        '現在庫スプレッドの情報をヘッダ部に設定
        With Me._Frm

            Select Case tabNm

                Case .tabInOutHistoryByInka.Name  '入荷ごと

                    .lblGoodsCdCustN.TextValue = goodsCdCust
                    .lblGoodsNmN.TextValue = goodsNm
                    .lblLotNoN.TextValue = lotNo
                    If IsNumeric(irime) = False OrElse Convert.ToDecimal(irime.ToString()) = 0 Then
                        .lblIrimeN.TextValue = String.Empty
                    Else
                        .lblIrimeN.Value = irime
                    End If

                Case .tabInOutHistoryByOutka.Name  '在庫ごと

                    .lblGoodsCdCustZ.TextValue = goodsCdCust
                    .lblGoodsNmZ.TextValue = goodsNm
                    .lblLotNoZ.TextValue = lotNo
                    .lblIrimeZ.Value = irime


            End Select

        End With

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
            'arrCtl.EnableStatus = lockFlg
        Next

        'ラベル項目をロック
        For Each lblCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In lblArr
            lblCtl.ReadOnlyStatus = True
        Next

        'グループボックスのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.InputMan.LMComboKubun)(arr, ctl)
        For Each arrCtl As Win.InputMan.LMComboKubun In arr
            'ロック処理/ロック解除処理を行う
            Call Me.LockGroupBox(arrCtl, lockFlg)
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

            '指定されたクラスかその継承クラスを設定
            arr.Add(ownControl)

        End If

        If 0 < ownControl.Controls.Count Then
            For Each targetControl As Control In ownControl.Controls
                Call Me.GetTarget(Of T)(arr, targetControl)
            Next
        End If

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(グループボックス)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockGroupBox(ByVal ctl As Win.InputMan.LMComboKubun, ByVal lockFlg As Boolean)

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
    ''' タブページ追加・削除処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddRemoveTabPage()

        'ラジオボタン = 詳細の場合、スルー
        If Me._Frm.optSyousai.Checked = True Then
            Call Me.AddTabPage()
        Else
            Call Me.RemoveTabPage()
        End If

    End Sub

    ''' <summary>
    ''' タブページ追加処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AddTabPage()

        With Me._Frm

            '既に入荷ごと、在庫ごとタブがある場合、スルー
            If 2 < .tabRireki.TabPages.Count Then
                Exit Sub
            End If

            '.tabRireki.TabPages.Add(.tabInOutHistoryByInka)
            .tabRireki.TabPages.Add(.tabInOutHistoryByOutka)

        End With

    End Sub

    ''' <summary>
    ''' タブページ削除処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RemoveTabPage()

        With Me._Frm

            '既に入荷ごと、在庫ごとタブがない場合、スルー
            If 2 = .tabRireki.TabPages.Count Then
                Exit Sub
            End If

            .tabRireki.TabPages.Remove(.tabInOutHistoryByOutka)
            '.tabRireki.TabPages.Remove(.tabInOutHistoryByInka)

        End With

    End Sub

#End Region

#Region "検索結果表示"

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Public Sub SetSelectListData(ByVal ds As DataSet)


    End Sub

    ''' <summary>
    ''' 検索結果ヘッダー部表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHeaderData(ByVal strKNAPTA As String, ByVal strKBN As String)

        With Me._Frm

            'Select Case strKBN
            '    Case "1"
            '        .txtType.TextValue = Get_Shubetu(Mid(strKNAPTA, 2, 2))      
            '        .txtHurikomi.TextValue = Trim(Mid(strKNAPTA, 2, 10))        
            '        .txtHurikomiNm.TextValue = Trim(Mid(strKNAPTA, 15, 40))     
            '        .txtTorikumi.TextValue = Trim(Mid(strKNAPTA, 55, 4))        
            '        .txtBankno.TextValue = Trim(Mid(strKNAPTA, 59, 4))          
            '        .txtBankNm.TextValue = Trim(Mid(strKNAPTA, 63, 15))         
            '        .txtShitenno.TextValue = Trim(Mid(strKNAPTA, 78, 3))        
            '        .txtShitenNm.TextValue = Trim(Mid(strKNAPTA, 81, 15))       
            '        .txtYokinsyu.TextValue = Get_Yokin(Mid(strKNAPTA, 96, 1))   
            '        .txtKozabango.TextValue = Trim(Mid(strKNAPTA, 97, 7))       
            '    Case "8"
            '        .txtTotalcnt.Value = CInt(Trim(Mid(strKNAPTA, 2, 6)))       
            '        .txtTotalKin.Value = CDec(Trim(Mid(strKNAPTA, 8, 12)))      
            'End Select

        End With

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)現在庫タブ
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprGenzaikoDefault

        'スプレッド(タイトル列)の設定
        Public DEF As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.DEF, " ", 20, True)
        Public YOJITU As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.YOJITU, " ", 20, True)
        'START YANAI 要望番号906
        'Public Shared OKIBA As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.OKIBA, "置場", 70, True)
        'Public Shared GOODS_CD_CUST As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.GOODS_CD_CUST, "商品" & vbCrLf & "コード", 60, True)
        'Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.GOODS_NM, "商品名", 130, True)
        'Public Shared CUST_CATEGORY_1 As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.CUST_CATEGORY_1, "荷主カテゴリ１", 100, True)
        'Public Shared INKO_DATE As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.INKO_DATE, "入荷日", 77, True)
        'Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.LOT_NO, "ロット№", 90, True)
        'Public Shared IRIME As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.IRIME, "入目", 90, True)
        ''START YANAI 要望番号647
        ''Public Shared NB_UT As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.NB_UT, "単位", 40, True)
        'Public Shared NB_UT As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.NB_UT, "単位", 50, True)
        ''END YANAI 要望番号647
        'Public Shared ZAI_QT As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.ZAI_QT, "残数", 50, True)
        ''START YANAI 要望番号647
        ''Public Shared ZAI_UT As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.ZAI_UT, "単位", 40, True)
        'Public Shared ZAI_UT As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.ZAI_UT, "単位", 50, True)
        ''END YANAI 要望番号647
        'Public Shared ZAN_SUURYOU As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.ZAN_SUURYOU, "残数量", 90, True)
        'Public Shared HIKIATE_SURYOU As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.HIKIATE_SURYOU, "引当中数量", 90, True)
        'Public Shared JITU_QT As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.JITU_QT, "実数量", 90, True)
        ''START YANAI 要望番号647
        ''Public Shared JITU_UT As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.JITU_UT, "単位", 40, True)
        'Public Shared JITU_UT As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.JITU_UT, "単位", 50, True)
        ''END YANAI 要望番号647
        'Public Shared ALCTD_NB As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.ALCTD_NB, "引当中", 60, True)
        Public OKIBA As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.OKIBA, "置場", 65, True)
        Public GOODS_CD_CUST As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.GOODS_CD_CUST, "商品" & vbCrLf & "コード", 55, True)
        Public GOODS_NM As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.GOODS_NM, "商品名", 120, True)
        Public CUST_CATEGORY_1 As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.CUST_CATEGORY_1, "荷主カテゴリ１", 100, True)
        Public INKO_DATE As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.INKO_DATE, "入荷日", 75, True)
        Public LOT_NO As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.LOT_NO, "ロット№", 80, True)
        Public IRIME As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.IRIME, "入目", 85, True)
        Public NB_UT As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.NB_UT, "単位", 40, True)
        Public ZAI_QT As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.ZAI_QT, "残数", 50, True)
        Public ZAI_UT As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.ZAI_UT, "単位", 40, True)
        Public ZAN_SUURYOU As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.ZAN_SUURYOU, "残数量", 90, True)
        Public HIKIATE_SURYOU As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.HIKIATE_SURYOU, "引当中数量", 90, True)
        Public JITU_QT As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.JITU_QT, "実数量", 90, True)
        Public JITU_UT As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.JITU_UT, "単位", 40, True)
        Public ALCTD_NB As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.ALCTD_NB, "引当中", 60, True)
        'END YANAI 要望番号906
        Public REMARK As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.REMARK, "備考小（社内）", 100, True)
        Public REMARK_OUT As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.REMARK_OUT, "備考小（社外）", 100, True)
        Public LT_DATE As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.LT_DATE, "賞味期限", 77, True)
        Public SERIAL_NO As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.SERIAL_NO, "シリアル№", 80, True)
        Public GOODS_COND_NM_1 As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.GOODS_COND_NM_1, "状態 中身", 90, True)
        Public GOODS_COND_NM_2 As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.GOODS_COND_NM_2, "状態 外装", 90, True)
        Public GOODS_COND_NM_3 As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.GOODS_COND_NM_3, "状態 荷主", 90, True)
        Public IRISU As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.IRISU, "入数", 50, True)
        'START YANAI 要望番号647
        'Public Shared UT As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.UT, "単位", 40, True)
        Public UT As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.UT, "単位", 50, True)
        'END YANAI 要望番号647
        Public OFB_KB As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.OFB_KB, "薄外品", 100, False)
        Public OFB_NM As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.OFB_NM, "薄外品", 70, True)
        Public SPD_KB As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.SPD_KB, "保留品区分値", 100, False)
        Public SPD_NM As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.SPD_NM, "保留品", 70, True)
        Public CUST_KANJYO_CD_1 As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.CUST_KANJYO_CD_1, "荷主勘定科目コード1", 150, True)
        Public CUST_KANJYO_CD_2 As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.CUST_KANJYO_CD_2, "荷主勘定科目コード2", 150, True)
        Public CUST_CATEGORY_2 As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.CUST_CATEGORY_2, "荷主カテゴリ２", 100, True)
        Public CUST_NM As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.CUST_NM, "荷主名", 150, True)
        Public CUST_CD As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.CUST_CD, "荷主コード", 110, True)
        Public INKA_NO As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.INKA_NO, "入荷管理番号", 120, True)
        Public INKA_NO_L As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.INKA_NO_L, "入荷管理番号L", 130, False)
        Public INKA_NO_M As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.INKA_NO_M, "入荷管理番号M", 130, False)
        Public INKA_NO_S As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.INKA_NO_S, "入荷管理番号S", 130, False)
        Public GOODS_CD_NRS As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.GOODS_CD_NRS, "商品KEY", 130, True)
        Public ZAI_REC_NO As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.ZAI_REC_NO, "在庫" & vbCrLf & "レコード番号", 90, True)
        Public WARIATE As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.WARIATE, "割当", 100, False)
        Public WARIATE_NM As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.WARIATE_NM, "割当", 80, True)
        Public DEST_CD_NM As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.DEST_CD_NM, "予約届出", 80, True)
        Public SYOUBOU_CD As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.SYOUBOU_CD, "消防コード", 80, True)
        Public SYOUBOU_NM As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.SYOUBOU_NM, "消防情報", 130, True)
        Public ZEI_KB As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.ZEI_KB, "税区分値", 100, False)
        Public ZEI_KB_NM As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.ZEI_KB_NM, "課税区分", 80, True)
        Public DOKUGEKI As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.DOKUGEKI, "毒劇区分", 100, False)
        Public DOKUGEKI_NM As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.DOKUGEKI_NM, "毒劇", 80, True)
        Public ONDO As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.ONDO, "温度区分", 60, False)
        Public ONDO_NM As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.ONDO_NM, "温度", 60, True)
        Public INKA_IRIME As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.INKA_IRIME, "入荷時入目", 80, True)
        Public NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.NRS_BR_CD, "営業所コード", 2, False)
        Public NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.NRS_BR_NM, "営業所名", 2, False)
        Public NRS_CR_NM As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.NRS_CR_NM, "倉庫名", 100, True)
        Public NRS_CR_CD As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.NRS_CR_CD, "倉庫" & vbCrLf & "コード", 60, True)
        Public CD_NRS_TO As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.CD_NRS_TO, "振替商品KEY", 70, False)
        Public CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.CUST_CD_L, "荷主コードL", 70, False)
        Public CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.CUST_CD_M, "荷主コードM", 70, False)

        Public ZAI_QT_ANOTHER As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.ZAI_QT_ANOTHER, "残数", 50, False)
        Public ALCTD_NB_ANOTHER As SpreadColProperty = New SpreadColProperty(LMD040C.SprColumnIndex.ALCTD_NB_ANOTHER, "引当中", 60, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(上部)入出荷（在庫ごと）タブ
    ''' </summary>
    ''' <remarks></remarks>
    Public Class sprNyusyukkaZDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.DEF, " ", 20, True)
        Public Shared YOJITU_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.YOJITU_Z, " ", 20, True)
        Public Shared SYUBETU_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.SYUBETU_Z, "種別", 50, True)
        Public Shared PLAN_DATE_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.PLAN_DATE_Z, "日付", 77, True)
        Public Shared INKA_KOSU_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.INKA_KOSU_Z, "入荷個数", 60, True)
        Public Shared OUTKA_KOSU_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.OUTKA_KOSU_Z, "出荷個数", 60, True)
        Public Shared ZAN_KOSU_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.ZAN_KOSU_Z, "残個数", 60, True)
        Public Shared NB_UT_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.NB_UT_Z, "単位", 40, True)
        Public Shared INKA_SURYO_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.INKA_SURYO_Z, "入荷数量", 90, True)
        Public Shared OUTKA_SURYO_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.OUTKA_SURYO_Z, "出荷数量", 90, True)
        Public Shared ZAN_SURYO_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.ZAN_SURYO_Z, "残数量", 90, True)
        Public Shared STD_IRIME_UT_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.STD_IRIME_UT_Z, "単位", 40, True)
        Public Shared OKIBA_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.OKIBA_Z, "置場", 70, True)
        Public Shared KANRI_NO_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.KANRI_NO_Z, "入出荷管理番号", 120, True)
        Public Shared ZAI_REC_NO_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.ZAI_REC_NO_Z, "在庫" & vbCrLf & "レコード番号", 90, True)
        Public Shared DEST_NM_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.DEST_NM_Z, "出荷先", 80, True)
        Public Shared ORD_NO_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.ORD_NO_Z, "オーダー番号", 120, True)
        Public Shared BUYER_ORD_NO_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.BUYER_ORD_NO_Z, "注文番号", 120, True)
        Public Shared UNSOCO_NM_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.UNSOCO_NM_Z, "運送会社名", 150, True)
        Public Shared REMARK_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.REMARK_Z, "備考小（社内）", 100, True)
        Public Shared REMARK_OUT_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.REMARK_OUT_Z, "備考小（社外）", 100, True)
        Public Shared GOODS_COND_NM_1_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.GOODS_COND_NM_1_Z, "状態 中身", 90, True)
        Public Shared GOODS_COND_NM_2_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.GOODS_COND_NM_2_Z, "状態 外装", 90, True)
        Public Shared GOODS_COND_NM_3_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.GOODS_COND_NM_3_Z, "状態 荷主", 90, True)
        Public Shared SPD_KB_NM_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.SPD_KB_NM_Z, "保留品", 70, True)
        Public Shared OFB_KB_NM_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.OFB_KB_NM_Z, "薄外品", 70, True)
        Public Shared ALLOC_PRIORITY_NM_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.ALLOC_PRIORITY_NM_Z, "引当優先度", 90, True)
        Public Shared DEST_CD_NM_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.DEST_CD_NM_Z, "予約届出", 80, True)
        Public Shared RSV_NO_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.RSV_NO_Z, "予約番号", 80, True)
        Public Shared INKA_NO_L_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.INKA_NO_L_Z, "入荷管理番号L", 90, False)
        Public Shared INKA_NO_M_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.INKA_NO_M_Z, "入荷管理番号M", 80, False)
        Public Shared INKA_NO_S_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.INKA_NO_S_Z, "入荷管理番号S", 80, False)

        Public Shared LOT_NO_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.LOT_NO_Z, "ロット№", 90, True)
        '非表示追加
        Public Shared GOODS_NM_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.GOODS_NM_Z, "商品名", 130, False)
        Public Shared IRIME_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.IRIME_Z, "入目", 90, False)


#If True Then ' 1888_WIT_ロケーション変更強化対応 20160822 added inoue
        Public Shared ZAI_TRS_UPD_USER_NM_Z As SpreadColProperty = New SpreadColProperty(LMD040C.sprNyukaZIndex.ZAI_TRS_UPD_USER_NM, "実施者", 80, True)
#End If

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprGenzaiko.CrearSpread()
            .sprNyusyukkaN.CrearSpread()
            .sprNyusyukkaZ.CrearSpread()

            '列数設定
            .sprGenzaiko.Sheets(0).ColumnCount = LMD040C.SprColumnIndex.LAST
            .sprNyusyukkaN.Sheets(0).ColumnCount = LMD040C.sprNyukaNIndex.LAST_N
            .sprNyusyukkaZ.Sheets(0).ColumnCount = LMD040C.sprNyukaZIndex.LAST_Z

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprGenzaiko.SetColProperty(New LMD040G.sprGenzaikoDef())
            objSprDef = New sprGenzaikoDefault
            .sprGenzaiko.SetColProperty(objSprDef, True)
            sprGenzaikoDef = DirectCast(objSprDef, LMD040G.sprGenzaikoDefault)

            '2015.10.15 英語化対応START
            '.sprNyusyukkaN.SetColProperty(New LMD040G.sprNyusyukkaZDef())
            '.sprNyusyukkaZ.SetColProperty(New LMD040G.sprNyusyukkaZDef())
            .sprNyusyukkaN.SetColProperty(New LMD040G.sprNyusyukkaZDef(), False)
            .sprNyusyukkaZ.SetColProperty(New LMD040G.sprNyusyukkaZDef(), False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。
            '.sprGenzaiko.ActiveSheet.FrozenColumnCount = LMD040G.sprGenzaikoDef.GOODS_NM.ColNo + 1
            .sprGenzaiko.ActiveSheet.FrozenColumnCount = sprGenzaikoDef.GOODS_NM.ColNo + 1

            .sprNyusyukkaN.ActiveSheet.FrozenColumnCount = LMD040G.sprNyusyukkaZDef.GOODS_NM_Z.ColNo + 1
            .sprNyusyukkaZ.ActiveSheet.FrozenColumnCount = LMD040G.sprNyusyukkaZDef.GOODS_NM_Z.ColNo + 1

            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Right)

            '列設定(sprGenzaiko:現在庫)
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.YOJITU.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Right))
            'START YANAI 要望番号579
            '.sprGenzaiko.SetCellStyle(0, LMD040G.sprGenzaikoDef.OKIBA.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.HAN_NUM_ALPHA, 17, False))
            'START YANAI 要望番号705
            '.sprGenzaiko.SetCellStyle(0, LMD040G.sprGenzaikoDef.OKIBA.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.ALL_MIX_IME_OFF, 17, False))
            '(2013.02.14)要望番号1843 置き場 サーチ行使用不可 -- START --
            '.sprGenzaiko.SetCellStyle(0, LMD040G.sprGenzaikoDef.OKIBA.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.ALL_MIX_IME_OFF, 19, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.OKIBA.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.ALL_MIX_IME_OFF, 19, True))
            '(2013.02.14)要望番号1843 置き場 サーチ行使用不可 --  END  --
            'END YANAI 要望番号705
            'END YANAI 要望番号579
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.GOODS_CD_CUST.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.ALL_HANKAKU, 20, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.ALL_MIX_IME_OFF, 60, False)) '検証結果_導入時要望 №62対応(2011.09.13)
            'START YANAI 要望番号1028
            '.sprGenzaiko.SetCellStyle(0, LMD040G.sprGenzaikoDef.CUST_CATEGORY_1.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.HAN_NUM_ALPHA, 20, False))
            'START YANAI 要望番号1065 荷主カテゴリのバイト変更
            '.sprGenzaiko.SetCellStyle(0, LMD040G.sprGenzaikoDef.CUST_CATEGORY_1.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.ALL_MIX_IME_OFF, 20, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.CUST_CATEGORY_1.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.ALL_MIX_IME_OFF, 25, False))
            'END YANAI 要望番号1065 荷主カテゴリのバイト変更
            'END YANAI 要望番号1028
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.INKO_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Right))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.LOT_NO.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.ALL_MIX_IME_OFF, 40, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.IRIME.ColNo, LMSpreadUtility.GetNumberCell(.sprGenzaiko, -999999999.999, 999999999.999, False, 3, , ","))
            'START YANAI 要望番号647
            '.sprGenzaiko.SetCellStyle(0, LMD040G.sprGenzaikoDef.NB_UT.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Left))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.NB_UT.ColNo, LMSpreadUtility.GetComboCellMaster(.sprGenzaiko, LMConst.CacheTBL.KBN, "KBN_CD", "KBN_CD", False, "KBN_GROUP_CD", "I001"))
            'END YANAI 要望番号647
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.ZAI_QT.ColNo, LMSpreadUtility.GetNumberCell(.sprGenzaiko, -9999999999, 9999999999, True, 0, , ","))
            'START YANAI 要望番号647
            '.sprGenzaiko.SetCellStyle(0, LMD040G.sprGenzaikoDef.ZAI_UT.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Left))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.ZAI_UT.ColNo, LMSpreadUtility.GetComboCellMaster(.sprGenzaiko, LMConst.CacheTBL.KBN, "KBN_CD", "KBN_CD", False, "KBN_GROUP_CD", "K002"))
            'END YANAI 要望番号647
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.ZAN_SUURYOU.ColNo, LMSpreadUtility.GetNumberCell(.sprGenzaiko, -999999999999.999, 999999999999.999, True, 3, , ","))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.HIKIATE_SURYOU.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Right))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.JITU_QT.ColNo, LMSpreadUtility.GetNumberCell(.sprGenzaiko, -999999999999.999, 999999999999.999, True, 3, , ","))
            'START YANAI 要望番号647
            '.sprGenzaiko.SetCellStyle(0, LMD040G.sprGenzaikoDef.JITU_UT.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Left))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.JITU_UT.ColNo, LMSpreadUtility.GetComboCellMaster(.sprGenzaiko, LMConst.CacheTBL.KBN, "KBN_CD", "KBN_CD", False, "KBN_GROUP_CD", "I001"))
            'END YANAI 要望番号647
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.ALCTD_NB.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Right))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.REMARK.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.ALL_MIX, 100, False))
            '要望番号:1702  terakawa 2012.12.19 Start
            '.sprGenzaiko.SetCellStyle(0, LMD040G.sprGenzaikoDef.REMARK_OUT.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Right))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.REMARK_OUT.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.ALL_MIX, 15, False))
            '要望番号:1702  terakawa 2012.12.19 End
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.LT_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Right))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.SERIAL_NO.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.ALL_MIX_IME_OFF, 40, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.GOODS_COND_NM_1.ColNo, LMSpreadUtility.GetComboCellKbn(.sprGenzaiko, "S005", False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.GOODS_COND_NM_2.ColNo, LMSpreadUtility.GetComboCellKbn(.sprGenzaiko, "S006", False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.GOODS_COND_NM_3.ColNo, LMSpreadUtility.GetComboCellMaster(.sprGenzaiko, "M_CUSTCOND", "JOTAI_CD", "JOTAI_NM", False, "NRS_BR_CD", LMUserInfoManager.GetNrsBrCd()))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.IRISU.ColNo, LMSpreadUtility.GetNumberCell(.sprGenzaiko, -99999999, 99999999, True, 0, , ","))
            'START YANAI 要望番号647
            '.sprGenzaiko.SetCellStyle(0, LMD040G.sprGenzaikoDef.UT.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Right))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.UT.ColNo, LMSpreadUtility.GetComboCellMaster(.sprGenzaiko, LMConst.CacheTBL.KBN, "KBN_CD", "KBN_CD", False, "KBN_GROUP_CD", "N001"))
            'END YANAI 要望番号647
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.OFB_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Right))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.OFB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprGenzaiko, "B002", False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.SPD_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Right))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.SPD_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprGenzaiko, "H003", False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.CUST_KANJYO_CD_1.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.HAN_NUM_ALPHA, 10, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.CUST_KANJYO_CD_2.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.HAN_NUM_ALPHA, 10, False))
            'START YANAI 要望番号1028
            '.sprGenzaiko.SetCellStyle(0, LMD040G.sprGenzaikoDef.CUST_CATEGORY_2.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.HAN_NUM_ALPHA, 20, False))
            'START YANAI 要望番号1065 荷主カテゴリのバイト変更
            '.sprGenzaiko.SetCellStyle(0, LMD040G.sprGenzaikoDef.CUST_CATEGORY_2.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.ALL_MIX_IME_OFF, 20, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.CUST_CATEGORY_2.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.ALL_MIX_IME_OFF, 25, False))
            'END YANAI 要望番号1065 荷主カテゴリのバイト変更
            'END YANAI 要望番号1028
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.CUST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.ALL_MIX, 60, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.CUST_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Right))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.INKA_NO.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.HAN_NUM_ALPHA, 17, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.INKA_NO_L.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.HAN_NUM_ALPHA, 9, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.INKA_NO_M.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.HAN_NUM_ALPHA, 3, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.INKA_NO_S.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.HAN_NUM_ALPHA, 3, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.GOODS_CD_NRS.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.HAN_NUM_ALPHA, 20, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.ZAI_REC_NO.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.HAN_NUM_ALPHA, 10, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.WARIATE.ColNo, LMSpreadUtility.GetComboCellKbn(.sprGenzaiko, "W001", False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.WARIATE_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprGenzaiko, "W001", False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.DEST_CD_NM.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.HAN_NUM_ALPHA, 15, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.SYOUBOU_CD.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.HAN_NUM_ALPHA, 3, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.SYOUBOU_NM.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.ALL_MIX, 45, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.ZEI_KB.ColNo, LMSpreadUtility.GetComboCellKbn(.sprGenzaiko, "Z001", False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.ZEI_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprGenzaiko, "Z001", False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.DOKUGEKI.ColNo, LMSpreadUtility.GetComboCellKbn(.sprGenzaiko, "G001", False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.DOKUGEKI_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprGenzaiko, "G001", False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.ONDO.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Right))
            '(2012.06.19)要望番号1173 --- START ---
            '.sprGenzaiko.SetCellStyle(0, LMD040G.sprGenzaikoDef.ONDO_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprGenzaiko, LMKbnConst.KBN_O003, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.ONDO_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprGenzaiko, LMKbnConst.KBN_O002, False))
            '(2012.06.19)要望番号1173 ---  END  ---
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.INKA_IRIME.ColNo, LMSpreadUtility.GetNumberCell(.sprGenzaiko, -999999999.999, 999999999.999, True, 3, , ","))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.ALL_MIX, 50, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetTextCell(.sprGenzaiko, InputControl.HAN_NUM_ALPHA, 2, False))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.NRS_CR_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Right))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.NRS_CR_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Right))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.CD_NRS_TO.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Left, 30))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.CUST_CD_L.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Right))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.CUST_CD_M.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Right))

            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.ZAI_QT_ANOTHER.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Right))
            .sprGenzaiko.SetCellStyle(0, sprGenzaikoDef.ALCTD_NB_ANOTHER.ColNo, LMSpreadUtility.GetLabelCell(.sprGenzaiko, CellHorizontalAlignment.Right))

        End With

    End Sub

    '''<summary>
    ''' 現在庫スプレッド表示・非表示判定
    ''' </summary>
    ''' 
    Public Sub ChengedSpreadCol()

        Dim flg1 As Boolean = True
        Dim flg2 As Boolean = True
        Dim flg3 As Boolean = True
        Dim flg4 As Boolean = True
        Dim flgKosu As Boolean = True

        With Me._Frm
            Select Case True
                '詳細の場合
                Case .optSyousai.Checked
                    '現在庫スプレッドの表示可否処理
                    flg1 = True
                    flg2 = True
                    flg3 = True
                    flg4 = True
                    '上記以外の場合
                Case Else
                    Select Case True
                        '商品・ロット・入目の場合
                        Case .optGoodLotIrime.Checked
                            flg1 = False
                            flg2 = False
                            flg3 = True
                            flg4 = True

                            '商品の場合
                        Case .optGoods.Checked
                            flg1 = False
                            flg2 = False
                            flg3 = False
                            flg4 = False

                            '商品・ロット・置場の場合
                        Case .optGoodLotOkiba.Checked
                            flg1 = False
                            flg2 = True
                            flg3 = True
                            flg4 = False

                            '商品・ロット・入目・置場の場合
                        Case .optOkiba.Checked
                            flg1 = False
                            flg2 = True
                            flg3 = True
                            flg4 = True

                    End Select
            End Select

            '個数表示コントロール
            If .chkNaigaiKosu.Checked = True Then
                flgKosu = False
            Else
                flgKosu = True
            End If

            .SuspendLayout()


            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.OKIBA).Visible = flg2
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.OKIBA.ColNo).Visible = flg2

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.LOT_NO).Visible = flg3
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.LOT_NO.ColNo).Visible = flg3
            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.NB_UT).Visible = flg3
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.NB_UT.ColNo).Visible = flg3

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.IRIME).Visible = flg4
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.IRIME.ColNo).Visible = flg4

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.INKO_DATE).Visible = flg1
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.INKO_DATE.ColNo).Visible = flg1

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.REMARK).Visible = flg1
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.REMARK.ColNo).Visible = flg1

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.REMARK_OUT).Visible = flg1
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.REMARK_OUT.ColNo).Visible = flg1

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.LT_DATE).Visible = flg1
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.LT_DATE.ColNo).Visible = flg1

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.SERIAL_NO).Visible = flg1
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.SERIAL_NO.ColNo).Visible = flg1

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.GOODS_COND_NM_1).Visible = flg1
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.GOODS_COND_NM_1.ColNo).Visible = flg1

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.GOODS_COND_NM_2).Visible = flg1
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.GOODS_COND_NM_2.ColNo).Visible = flg1

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.GOODS_COND_NM_3).Visible = flg1
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.GOODS_COND_NM_3.ColNo).Visible = flg1

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.OFB_KB).Visible = flg1           '検証結果(メモ)№1対応(2011.09.09)
            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.OFB_NM).Visible = flg1
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.OFB_NM.ColNo).Visible = flg1

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.SPD_KB).Visible = flg1           '検証結果(メモ)№1対応(2011.09.09)
            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.SPD_NM).Visible = flg1
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.SPD_NM.ColNo).Visible = flg1

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.INKA_NO).Visible = flg1
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.INKA_NO.ColNo).Visible = flg1

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.GOODS_CD_NRS).Visible = flg1
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.GOODS_CD_NRS.ColNo).Visible = flg1

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.ZAI_REC_NO).Visible = flg1
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.ZAI_REC_NO.ColNo).Visible = flg1

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.DEST_CD_NM).Visible = flg1
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.DEST_CD_NM.ColNo).Visible = flg1

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.INKA_IRIME).Visible = flg1
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.INKA_IRIME.ColNo).Visible = flg1

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.WARIATE_NM).Visible = flg1
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.WARIATE_NM.ColNo).Visible = flg1

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.ZEI_KB_NM).Visible = flg1
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.ZEI_KB_NM.ColNo).Visible = flg1

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.ZAI_QT).Visible = flgKosu
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.ZAI_QT.ColNo).Visible = flgKosu

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.ZAI_QT_ANOTHER).Visible = Not flgKosu
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.ZAI_QT_ANOTHER.ColNo).Visible = Not flgKosu

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.ALCTD_NB).Visible = flgKosu
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.ALCTD_NB.ColNo).Visible = flgKosu

            '.sprGenzaiko.ActiveSheet.Columns(LMD040C.SprColumnIndex.ALCTD_NB_ANOTHER).Visible = Not flgKosu
            .sprGenzaiko.ActiveSheet.Columns(sprGenzaikoDef.ALCTD_NB_ANOTHER.ColNo).Visible = Not flgKosu

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 入出荷スプレッド表示・非表示判定
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ChangedSpreadNCol()
        Dim flg1 As Boolean = True
        Dim flg2 As Boolean = True
        Dim flg3 As Boolean = True

        With Me._Frm
            Select Case True

                Case .optKosu.Checked          '個数（入荷ごと）の場合
                    flg1 = True
                    flg2 = False
                Case .optSuryo.Checked         '数量（入荷ごと）の場合
                    flg1 = False
                    flg2 = True
            End Select

            '要望管理1992 2013.4.3 s.kobayashi
            If Me._Frm.optGoods.Checked = True Then
                flg3 = True
            Else
                flg3 = False
            End If

            .SuspendLayout()

            '入出荷（入荷ごと）スプレッド表示可否判定
            .sprNyusyukkaN.ActiveSheet.Columns(LMD040C.sprNyukaNIndex.INKA_KOSU_N).Visible = flg1
            .sprNyusyukkaN.ActiveSheet.Columns(LMD040C.sprNyukaNIndex.OUTKA_KOSU_N).Visible = flg1
            .sprNyusyukkaN.ActiveSheet.Columns(LMD040C.sprNyukaNIndex.ZAN_KOSU_N).Visible = flg1
            .sprNyusyukkaN.ActiveSheet.Columns(LMD040C.sprNyukaNIndex.NB_UT_N).Visible = flg1
            .sprNyusyukkaN.ActiveSheet.Columns(LMD040C.sprNyukaNIndex.INKA_SURYO_N).Visible = flg2
            .sprNyusyukkaN.ActiveSheet.Columns(LMD040C.sprNyukaNIndex.OUTKA_SURYO_N).Visible = flg2
            .sprNyusyukkaN.ActiveSheet.Columns(LMD040C.sprNyukaNIndex.ZAN_SURYO_N).Visible = flg2
            .sprNyusyukkaN.ActiveSheet.Columns(LMD040C.sprNyukaNIndex.STD_IRIME_UT_N).Visible = flg2
            '要望管理1992 2013.4.3 s.kobayashi
            .sprNyusyukkaN.ActiveSheet.Columns(LMD040C.sprNyukaNIndex.LOT_NO_N).Visible = flg3

            .ResumeLayout(True)

        End With
    End Sub

    ''' <summary>
    ''' 入出荷スプレッド表示・非表示判定
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ChangedSpreadZCol()
        Dim flg1 As Boolean = True
        Dim flg2 As Boolean = True
        Dim flg3 As Boolean = True

        With Me._Frm
            Select Case True

                Case .optKosuZaiko.Checked     '個数（在庫ごと）の場合
                    flg1 = True
                    flg2 = False
                Case .optSuryoZaiko.Checked    '数量（在庫ごと）の場合
                    flg1 = False
                    flg2 = True
            End Select

            '要望管理1992 2013.4.3 s.kobayashi
            If Me._Frm.optGoods.Checked = True Then
                flg3 = True
            Else
                flg3 = False
            End If

            .SuspendLayout()

            '入出荷（入荷ごと）スプレッド表示可否判定
            .sprNyusyukkaN.ActiveSheet.Columns(LMD040C.sprNyukaNIndex.INKA_KOSU_N).Visible = flg1
            .sprNyusyukkaN.ActiveSheet.Columns(LMD040C.sprNyukaNIndex.OUTKA_KOSU_N).Visible = flg1
            .sprNyusyukkaN.ActiveSheet.Columns(LMD040C.sprNyukaNIndex.ZAN_KOSU_N).Visible = flg1
            .sprNyusyukkaN.ActiveSheet.Columns(LMD040C.sprNyukaNIndex.NB_UT_N).Visible = flg1
            .sprNyusyukkaN.ActiveSheet.Columns(LMD040C.sprNyukaNIndex.INKA_SURYO_N).Visible = flg2
            .sprNyusyukkaN.ActiveSheet.Columns(LMD040C.sprNyukaNIndex.OUTKA_SURYO_N).Visible = flg2
            .sprNyusyukkaN.ActiveSheet.Columns(LMD040C.sprNyukaNIndex.ZAN_SURYO_N).Visible = flg2
            .sprNyusyukkaN.ActiveSheet.Columns(LMD040C.sprNyukaNIndex.STD_IRIME_UT_N).Visible = flg2
            '要望管理1992 2013.4.3 s.kobayashi
            .sprNyusyukkaN.ActiveSheet.Columns(LMD040C.sprNyukaNIndex.LOT_NO_N).Visible = flg3

            '入出荷（在庫ごと）スプレッド表示可否判定
            .sprNyusyukkaZ.ActiveSheet.Columns(LMD040C.sprNyukaZIndex.INKA_KOSU_Z).Visible = flg1
            .sprNyusyukkaZ.ActiveSheet.Columns(LMD040C.sprNyukaZIndex.OUTKA_KOSU_Z).Visible = flg1
            .sprNyusyukkaZ.ActiveSheet.Columns(LMD040C.sprNyukaZIndex.ZAN_KOSU_Z).Visible = flg1
            .sprNyusyukkaZ.ActiveSheet.Columns(LMD040C.sprNyukaZIndex.NB_UT_Z).Visible = flg1
            .sprNyusyukkaZ.ActiveSheet.Columns(LMD040C.sprNyukaZIndex.INKA_SURYO_Z).Visible = flg2
            .sprNyusyukkaZ.ActiveSheet.Columns(LMD040C.sprNyukaZIndex.OUTKA_SURYO_Z).Visible = flg2
            .sprNyusyukkaZ.ActiveSheet.Columns(LMD040C.sprNyukaZIndex.ZAN_SURYO_Z).Visible = flg2
            .sprNyusyukkaZ.ActiveSheet.Columns(LMD040C.sprNyukaZIndex.STD_IRIME_UT_Z).Visible = flg2
            .sprNyusyukkaZ.ActiveSheet.Columns(LMD040C.sprNyukaZIndex.LOT_NO_Z).Visible = False

            .ResumeLayout(True)

        End With
    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMD040F)

        With frm.sprGenzaiko
            .SetCellValue(0, sprGenzaikoDef.YOJITU.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.OKIBA.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.GOODS_CD_CUST.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.GOODS_NM.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.CUST_CATEGORY_1.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.INKO_DATE.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.LOT_NO.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.IRIME.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.NB_UT.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.ZAI_QT.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.ZAI_UT.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.ZAN_SUURYOU.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.HIKIATE_SURYOU.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.JITU_QT.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.JITU_UT.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.ALCTD_NB.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.REMARK.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.REMARK_OUT.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.LT_DATE.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.SERIAL_NO.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.GOODS_COND_NM_1.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.GOODS_COND_NM_2.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.GOODS_COND_NM_3.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.IRISU.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.UT.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.OFB_KB.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.OFB_NM.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.SPD_KB.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.SPD_NM.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.CUST_KANJYO_CD_1.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.CUST_KANJYO_CD_2.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.CUST_CATEGORY_2.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.CUST_NM.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.CUST_CD.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.INKA_NO.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.INKA_NO_L.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.INKA_NO_M.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.INKA_NO_S.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.GOODS_CD_NRS.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.ZAI_REC_NO.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.WARIATE.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.WARIATE_NM.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.DEST_CD_NM.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.SYOUBOU_CD.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.SYOUBOU_NM.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.ZEI_KB.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.ZEI_KB_NM.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.DOKUGEKI.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.DOKUGEKI_NM.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.ONDO.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.ONDO_NM.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.INKA_IRIME.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.NRS_BR_CD.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.NRS_BR_NM.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.NRS_CR_NM.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.NRS_CR_CD.ColNo, String.Empty)
            .SetCellValue(0, sprGenzaikoDef.CD_NRS_TO.ColNo, String.Empty)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(現在庫（明細）)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprGenzaiko
        Dim Hiduke As String = String.Empty
        Dim LtHiduke As String = String.Empty
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

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -999999999999.999, 999999999999.999, True, 3, , ",")
            Dim lNumber As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            '計算変数
            Dim konsu As Integer = 0
            Dim hasu As Integer = 0
            Dim irisu As Integer = 0
            Dim zankosu As Integer = 0
            Dim hikiatetyu As Integer = 0

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            'START YANAI 要望番号617
            Dim sumKosu As Decimal = 0
            'END YANAI 要望番号617

            '値設定
            For i As Integer = 1 To lngcnt
                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, sprGenzaikoDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprGenzaikoDef.YOJITU.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.OKIBA.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.GOODS_CD_CUST.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.GOODS_NM.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.CUST_CATEGORY_1.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.INKO_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.LOT_NO.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.IRIME.ColNo, LMSpreadUtility.GetNumberCell(spr, -999999999.999, 999999999.999, True, 3, , ","))
                .SetCellStyle(i, sprGenzaikoDef.NB_UT.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left))
                .SetCellStyle(i, sprGenzaikoDef.ZAI_QT.ColNo, LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, True, 0, , ","))
                .SetCellStyle(i, sprGenzaikoDef.ZAI_UT.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left))
                .SetCellStyle(i, sprGenzaikoDef.ZAN_SUURYOU.ColNo, LMSpreadUtility.GetNumberCell(spr, -999999999999.999, 999999999999.999, True, 3, , ","))
                .SetCellStyle(i, sprGenzaikoDef.HIKIATE_SURYOU.ColNo, sNumber)
                .SetCellStyle(i, sprGenzaikoDef.JITU_QT.ColNo, LMSpreadUtility.GetNumberCell(spr, -999999999999.999, 999999999999.999, True, 3, , ","))
                .SetCellStyle(i, sprGenzaikoDef.JITU_UT.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left))
                .SetCellStyle(i, sprGenzaikoDef.ALCTD_NB.ColNo, sNumber)
                .SetCellStyle(i, sprGenzaikoDef.REMARK.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.REMARK_OUT.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.LT_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.SERIAL_NO.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.GOODS_COND_NM_1.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.GOODS_COND_NM_2.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.GOODS_COND_NM_3.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.IRISU.ColNo, LMSpreadUtility.GetNumberCell(spr, -99999999, 99999999, True, 0, , ","))
                .SetCellStyle(i, sprGenzaikoDef.UT.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.OFB_KB.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.OFB_NM.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.SPD_KB.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.SPD_NM.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.CUST_KANJYO_CD_1.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.CUST_KANJYO_CD_2.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.CUST_CATEGORY_2.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.CUST_NM.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.CUST_CD.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.INKA_NO.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.INKA_NO_L.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.INKA_NO_M.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.INKA_NO_S.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.GOODS_CD_NRS.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.ZAI_REC_NO.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.WARIATE.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.WARIATE_NM.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.DEST_CD_NM.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.SYOUBOU_CD.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.SYOUBOU_NM.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.ZEI_KB.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.ZEI_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.DOKUGEKI.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.DOKUGEKI_NM.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.ONDO.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.ONDO_NM.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.INKA_IRIME.ColNo, LMSpreadUtility.GetNumberCell(spr, -999999999.999, 999999999.999, True, 3, , ","))
                .SetCellStyle(i, sprGenzaikoDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.NRS_CR_NM.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.NRS_CR_CD.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.CD_NRS_TO.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, sprGenzaikoDef.CUST_CD_M.ColNo, sLabel)

                '2011/08/23 追加
                .SetCellStyle(i, sprGenzaikoDef.ZAI_QT_ANOTHER.ColNo, lNumber)
                .SetCellStyle(i, sprGenzaikoDef.ALCTD_NB_ANOTHER.ColNo, lNumber)

                .SetCellValue(i, sprGenzaikoDef.YOJITU.ColNo, dr.Item("YOJITU").ToString())
                .SetCellValue(i, sprGenzaikoDef.OKIBA.ColNo, dr.Item("OKIBA").ToString())
                .SetCellValue(i, sprGenzaikoDef.GOODS_CD_CUST.ColNo, dr.Item("GOODS_CD_CUST").ToString())
                .SetCellValue(i, sprGenzaikoDef.GOODS_NM.ColNo, dr.Item("GOODS_NM").ToString())
                .SetCellValue(i, sprGenzaikoDef.CUST_CATEGORY_1.ColNo, dr.Item("SEARCH_KEY_1").ToString())
                .SetCellValue(i, sprGenzaikoDef.INKO_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("INKO_DATE").ToString()))
                .SetCellValue(i, sprGenzaikoDef.LOT_NO.ColNo, dr.Item("LOT_NO").ToString())
                .SetCellValue(i, sprGenzaikoDef.IRIME.ColNo, dr.Item("IRIME").ToString())
                .SetCellValue(i, sprGenzaikoDef.NB_UT.ColNo, dr.Item("IRIME_UT").ToString())
                .SetCellValue(i, sprGenzaikoDef.ZAI_QT.ColNo, dr.Item("ZANKOSU").ToString())
                .SetCellValue(i, sprGenzaikoDef.ZAI_UT.ColNo, dr.Item("NB_UT").ToString())
                .SetCellValue(i, sprGenzaikoDef.ZAN_SUURYOU.ColNo, dr.Item("ZANSURYO").ToString())
                .SetCellValue(i, sprGenzaikoDef.HIKIATE_SURYOU.ColNo, dr.Item("ALCTD_QT").ToString())
                .SetCellValue(i, sprGenzaikoDef.JITU_QT.ColNo, dr.Item("PORA_ZAI_QT").ToString())
                .SetCellValue(i, sprGenzaikoDef.JITU_UT.ColNo, dr.Item("STD_IRIME_UT").ToString())
                .SetCellValue(i, sprGenzaikoDef.ALCTD_NB.ColNo, dr.Item("ALCTD_NB").ToString())
                .SetCellValue(i, sprGenzaikoDef.REMARK.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue(i, sprGenzaikoDef.REMARK_OUT.ColNo, dr.Item("REMARK_OUT").ToString())
                .SetCellValue(i, sprGenzaikoDef.LT_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("LT_DATE").ToString()))
                .SetCellValue(i, sprGenzaikoDef.SERIAL_NO.ColNo, dr.Item("SERIAL_NO").ToString())
                .SetCellValue(i, sprGenzaikoDef.GOODS_COND_NM_1.ColNo, dr.Item("GOODS_COND_NM_1").ToString())
                .SetCellValue(i, sprGenzaikoDef.GOODS_COND_NM_2.ColNo, dr.Item("GOODS_COND_NM_2").ToString())
                .SetCellValue(i, sprGenzaikoDef.GOODS_COND_NM_3.ColNo, dr.Item("GOODS_COND_NM_3").ToString())
                .SetCellValue(i, sprGenzaikoDef.IRISU.ColNo, dr.Item("PKG_NB").ToString())
                .SetCellValue(i, sprGenzaikoDef.UT.ColNo, dr.Item("PKG_UT").ToString())
                .SetCellValue(i, sprGenzaikoDef.OFB_KB.ColNo, dr.Item("OFB_KB").ToString())
                .SetCellValue(i, sprGenzaikoDef.OFB_NM.ColNo, dr.Item("OFB_KB_NM").ToString())
                .SetCellValue(i, sprGenzaikoDef.SPD_KB.ColNo, dr.Item("SPD_KB").ToString())
                .SetCellValue(i, sprGenzaikoDef.SPD_NM.ColNo, dr.Item("SPD_KB_NM").ToString())
                .SetCellValue(i, sprGenzaikoDef.CUST_KANJYO_CD_1.ColNo, dr.Item("CUST_COST_CD1").ToString())
                .SetCellValue(i, sprGenzaikoDef.CUST_KANJYO_CD_2.ColNo, dr.Item("CUST_COST_CD2").ToString())
                .SetCellValue(i, sprGenzaikoDef.CUST_CATEGORY_2.ColNo, dr.Item("SEARCH_KEY_2").ToString())
                .SetCellValue(i, sprGenzaikoDef.CUST_NM.ColNo, dr.Item("CUST_NM").ToString())
                .SetCellValue(i, sprGenzaikoDef.CUST_CD.ColNo, dr.Item("CUST_CD").ToString())
                .SetCellValue(i, sprGenzaikoDef.INKA_NO.ColNo, dr.Item("INKA_NO").ToString())
                .SetCellValue(i, sprGenzaikoDef.INKA_NO_L.ColNo, dr.Item("INKA_NO_L").ToString())
                .SetCellValue(i, sprGenzaikoDef.INKA_NO_M.ColNo, dr.Item("INKA_NO_M").ToString())
                .SetCellValue(i, sprGenzaikoDef.INKA_NO_S.ColNo, dr.Item("INKA_NO_S").ToString())
                .SetCellValue(i, sprGenzaikoDef.GOODS_CD_NRS.ColNo, dr.Item("GOODS_CD_NRS").ToString())
                .SetCellValue(i, sprGenzaikoDef.ZAI_REC_NO.ColNo, dr.Item("ZAI_REC_NO").ToString())
                .SetCellValue(i, sprGenzaikoDef.WARIATE.ColNo, dr.Item("ALLOC_PRIORITY").ToString())
                .SetCellValue(i, sprGenzaikoDef.WARIATE_NM.ColNo, dr.Item("ALLOC_PRIORITY_NM").ToString())
                .SetCellValue(i, sprGenzaikoDef.DEST_CD_NM.ColNo, dr.Item("DEST_CD_NM").ToString())
                .SetCellValue(i, sprGenzaikoDef.SYOUBOU_CD.ColNo, dr.Item("SHOBO_CD").ToString())
                .SetCellValue(i, sprGenzaikoDef.SYOUBOU_NM.ColNo, dr.Item("SHOBO_NM").ToString())
                .SetCellValue(i, sprGenzaikoDef.ZEI_KB.ColNo, dr.Item("TAX_KB").ToString())
                .SetCellValue(i, sprGenzaikoDef.ZEI_KB_NM.ColNo, dr.Item("TAX_NM").ToString())
                .SetCellValue(i, sprGenzaikoDef.DOKUGEKI.ColNo, dr.Item("DOKU_KB").ToString())
                .SetCellValue(i, sprGenzaikoDef.DOKUGEKI_NM.ColNo, dr.Item("DOKU_NM").ToString())
                .SetCellValue(i, sprGenzaikoDef.ONDO.ColNo, dr.Item("ONDO_KB").ToString())
                .SetCellValue(i, sprGenzaikoDef.ONDO_NM.ColNo, dr.Item("ONDO_NM").ToString())
                .SetCellValue(i, sprGenzaikoDef.INKA_IRIME.ColNo, dr.Item("INKA_IRIME").ToString())
                .SetCellValue(i, sprGenzaikoDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, sprGenzaikoDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, sprGenzaikoDef.NRS_CR_NM.ColNo, dr.Item("WH_NM").ToString())
                .SetCellValue(i, sprGenzaikoDef.NRS_CR_CD.ColNo, dr.Item("WH_CD").ToString())
                .SetCellValue(i, sprGenzaikoDef.CD_NRS_TO.ColNo, dr.Item("CD_NRS_TO").ToString())
                .SetCellValue(i, sprGenzaikoDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, sprGenzaikoDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())

                '梱数　端数/入数　表記 （2011/08/23 追加）
                zankosu = Convert.ToInt32(dr.Item("ZANKOSU"))
                hikiatetyu = Convert.ToInt32(dr.Item("ALCTD_NB"))
                irisu = Convert.ToInt32(dr.Item("PKG_NB"))

                If irisu > 0 Then
                    konsu = zankosu \ irisu
                    hasu = zankosu Mod irisu
                Else
                    konsu = 0
                    hasu = 0
                End If

                '検証結果(メモ)№44対応(2011.09.12) 端数部分の分子が0の場合、梱数だけの表示
                If hasu = 0 Then
                    .SetCellValue(i, sprGenzaikoDef.ZAI_QT_ANOTHER.ColNo, Format(konsu, "#,0"))
                Else
                    .SetCellValue(i, sprGenzaikoDef.ZAI_QT_ANOTHER.ColNo, String.Concat(Format(konsu, "#,0"), " ", Format(hasu, "#,0"), "/", Format(irisu, "#,0")))
                End If

                If irisu > 0 Then
                    konsu = hikiatetyu \ irisu
                    hasu = hikiatetyu Mod irisu
                Else
                    konsu = 0
                    hasu = 0
                End If

                '検証結果(メモ)№44対応(2011.09.12) 端数部分の分子が0の場合、梱数だけの表示
                If hasu = 0 Then
                    .SetCellValue(i, sprGenzaikoDef.ALCTD_NB_ANOTHER.ColNo, Format(konsu, "#,0"))
                Else
                    .SetCellValue(i, sprGenzaikoDef.ALCTD_NB_ANOTHER.ColNo, String.Concat(Format(konsu, "#,0"), " ", Format(hasu, "#,0"), "/", Format(irisu, "#,0")))
                End If

                'START YANAI 要望番号617
                '合計個数を求める
                sumKosu = sumKosu + Convert.ToDecimal(dr.Item("ZANKOSU").ToString())
                'END YANAI 要望番号617

            Next

            'START YANAI 要望番号617
            Me._Frm.numSumKosu.Value = sumKosu
            'END YANAI 要望番号617

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定
    ''' </summary>
    ''' <param name="dt">DataTable</param>
    ''' <param name="spr">スプレッド</param>
    ''' <remarks></remarks>
    Friend Sub SetSpreadZaiko(ByVal dt As DataTable, ByVal spr As LMSpread, Optional ByVal nyuukaFlag As Boolean = False)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With spr

            .SuspendLayout()

            .CrearSpread()

            Dim lngcnt As Integer = dt.Rows.Count - 1

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sNum12dec3 As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -999999999999.999, 999999999999.999, True, 3, , ",")
            Dim sNum10 As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, True, 0, , ",")
            Dim sNum9dec3 As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -999999999.999, 999999999.999, True, 3, , ",")

            '2017/09/25 修正 李↓
            '2016.02.09 英語化対応　修正START
            Dim str As String = "前残"
            If lgm.MessageLanguage.Equals(LMConst.FLG.OFF) = False Then

                Dim kbn027Dr() As DataRow = Nothing
                kbn027Dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", "K027", _
                                                                                                                 "' AND SUBSTRING(KBN_CD,2,1) = '", lgm.MessageLanguage, _
                                                                                                                 "' AND SYS_DEL_FLG = '0'"))

                If kbn027Dr.Length > 0 Then
                    str = kbn027Dr(0).Item("KBN_NM1").ToString()
                End If
            End If
            '2016.02.09 英語化対応　修正END
            '2017/09/25 修正 李↑

            Dim dr As DataRow = Nothing
            Dim rowCnt As Integer = 0

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center
            Dim kosuCol As String() = New String() {"H", "I", "J"}
            Dim suryoCol As String() = New String() {"L", "M", "N"}
            Dim syubetsu As String = String.Empty
            Dim zenzanFlg As Boolean = False
            Dim zenzanGyo As Integer = 0
            Dim zenzanKosu As Decimal = 0
            Dim zenzanSuryo As Decimal = 0
            '値設定
            For i As Integer = 0 To lngcnt

                dr = dt.Rows(i)

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                syubetsu = dr.Item("SYUBETU").ToString()

                If zenzanFlg = True AndAlso str.Equals(syubetsu) = True Then
                    zenzanGyo = zenzanGyo + 1
                    'まえのぎょうにたしこみ
                    zenzanKosu = Convert.ToDecimal(.ActiveSheet.Cells(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ZAN_KOSU_Z.ColNo).Value.ToString()) _
                                 + Convert.ToDecimal(dr.Item("ZAN_KOSU").ToString())

                    zenzanSuryo = Convert.ToDecimal(.ActiveSheet.Cells(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ZAN_SURYO_Z.ColNo).Value.ToString()) _
                                 + Convert.ToDecimal(dr.Item("ZAN_SURYO").ToString())

                    .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ZAN_KOSU_Z.ColNo, zenzanKosu.ToString())
                    .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ZAN_SURYO_Z.ColNo, zenzanSuryo.ToString())

                    Continue For
                End If

                If str.Equals(syubetsu) = True AndAlso zenzanFlg = False Then zenzanFlg = True

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                'セルスタイル設定
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.DEF.ColNo, sDEF)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.YOJITU_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.SYUBETU_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.GOODS_NM_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.PLAN_DATE_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.LOT_NO_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.IRIME_Z.ColNo, sNum9dec3)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.SYUBETU_Z.ColNo, sLabel)

                '入荷出荷個数（基本スタイル）
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.INKA_KOSU_Z.ColNo, sNum10)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.OUTKA_KOSU_Z.ColNo, sNum10)

                '数式を設定
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ZAN_KOSU_Z.ColNo, sNum10)
                '2016.02.09 英語化対応　修正START
                'If str.Equals(syubetsu) = False Then
                '    '2016.02.09 英語化対応　修正END
                '    .ActiveSheet.SetFormula(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ZAN_KOSU_Z.ColNo, Me.CalcCellValue(i - zenzanGyo, kosuCol, nyuukaFlag))
                'End If

                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.NB_UT_Z.ColNo, LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left))
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.INKA_SURYO_Z.ColNo, sNum12dec3)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.OUTKA_SURYO_Z.ColNo, sNum12dec3)

                '数式を設定
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ZAN_SURYO_Z.ColNo, sNum12dec3)
                '2016.02.09 英語化対応　修正START
                'If str.Equals(syubetsu) = False Then
                '    '2016.02.09 英語化対応　修正END
                '    .ActiveSheet.SetFormula(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ZAN_SURYO_Z.ColNo, Me.CalcCellValue(i - zenzanGyo, suryoCol, nyuukaFlag))
                'End If

                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.STD_IRIME_UT_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.OKIBA_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.KANRI_NO_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ZAI_REC_NO_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.DEST_NM_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ORD_NO_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.BUYER_ORD_NO_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.UNSOCO_NM_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.REMARK_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.REMARK_OUT_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.GOODS_COND_NM_1_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.GOODS_COND_NM_2_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.GOODS_COND_NM_3_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.OFB_KB_NM_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.SPD_KB_NM_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ALLOC_PRIORITY_NM_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.DEST_CD_NM_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.RSV_NO_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.INKA_NO_L_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.INKA_NO_M_Z.ColNo, sLabel)
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.INKA_NO_S_Z.ColNo, sLabel)

#If True Then ' 1888_WIT_ロケーション変更強化対応 20160822 added inoue
                .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ZAI_TRS_UPD_USER_NM_Z.ColNo, sLabel)
#End If


                '値設定
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.YOJITU_Z.ColNo, dr.Item("YOJITU").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.SYUBETU_Z.ColNo, dr.Item("SYUBETU").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.GOODS_NM_Z.ColNo, dr.Item("GOODS_NM").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.PLAN_DATE_Z.ColNo, DateFormatUtility.EditSlash(dr.Item("PLAN_DATE").ToString()))
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.LOT_NO_Z.ColNo, dr.Item("LOT_NO").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.IRIME_Z.ColNo, dr.Item("IRIME").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.INKA_KOSU_Z.ColNo, dr.Item("INKA_KOSU").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.OUTKA_KOSU_Z.ColNo, dr.Item("OUTKA_KOSU").ToString())
                '2020.04.19 012266_2017バージョンアップ対応　修正START
                If str.Equals(syubetsu) = True Then
                    .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ZAN_KOSU_Z.ColNo, dr.Item("ZAN_KOSU").ToString())
                Else
                    .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ZAN_KOSU_Z.ColNo, Me.CalcCellValueReal(spr, i - zenzanGyo, True, nyuukaFlag))
                End If
                '2020.04.19 012266_2017バージョンアップ対応　修正END
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.NB_UT_Z.ColNo, dr.Item("NB_UT").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.INKA_SURYO_Z.ColNo, dr.Item("INKA_SURYO").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.OUTKA_SURYO_Z.ColNo, dr.Item("OUTKA_SURYO").ToString())
                '2020.04.19 012266_2017バージョンアップ対応　修正START
                If str.Equals(syubetsu) = True Then
                    .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ZAN_SURYO_Z.ColNo, dr.Item("ZAN_SURYO").ToString())
                Else
                    .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ZAN_SURYO_Z.ColNo, Me.CalcCellValueReal(spr, i - zenzanGyo, False, nyuukaFlag))
                End If
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.STD_IRIME_UT_Z.ColNo, dr.Item("STD_IRIME_UT").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.OKIBA_Z.ColNo, dr.Item("OKIBA").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.KANRI_NO_Z.ColNo, dr.Item("KANRI_NO").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ZAI_REC_NO_Z.ColNo, dr.Item("ZAI_REC_NO").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.DEST_NM_Z.ColNo, dr.Item("DEST_NM").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ORD_NO_Z.ColNo, dr.Item("ORD_NO").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.BUYER_ORD_NO_Z.ColNo, dr.Item("BUYER_ORD_NO").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.UNSOCO_NM_Z.ColNo, dr.Item("UNSOCO_NM").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.REMARK_Z.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.REMARK_OUT_Z.ColNo, dr.Item("REMARK_OUT").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.GOODS_COND_NM_1_Z.ColNo, dr.Item("GOODS_COND_NM_1").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.GOODS_COND_NM_2_Z.ColNo, dr.Item("GOODS_COND_NM_2").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.GOODS_COND_NM_3_Z.ColNo, dr.Item("GOODS_COND_NM_3").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.OFB_KB_NM_Z.ColNo, dr.Item("OFB_KB_NM").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.SPD_KB_NM_Z.ColNo, dr.Item("SPD_KB_NM").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ALLOC_PRIORITY_NM_Z.ColNo, dr.Item("ALLOC_PRIORITY_NM").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.DEST_CD_NM_Z.ColNo, dr.Item("DEST_CD_NM").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.RSV_NO_Z.ColNo, dr.Item("RSV_NO").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.INKA_NO_L_Z.ColNo, dr.Item("KANRI_NO_L").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.INKA_NO_M_Z.ColNo, dr.Item("KANRI_NO_M").ToString())
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.INKA_NO_S_Z.ColNo, dr.Item("KANRI_NO_S").ToString())


#If True Then ' 1888_WIT_ロケーション変更強化対応 20160822 added inoue
                .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.ZAI_TRS_UPD_USER_NM_Z.ColNo, dr.Item("ZAI_TRS_UPD_USER_NM").ToString())
#End If


                '    入荷出荷個数：値が0の場合は空欄
                If Convert.ToDecimal(dr.Item("INKA_KOSU").ToString()) = 0 Then
                    .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.INKA_KOSU_Z.ColNo, sLabel)
                    .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.INKA_KOSU_Z.ColNo, String.Empty)
                End If
                If Convert.ToDecimal(dr.Item("OUTKA_KOSU").ToString()) = 0 Then
                    .SetCellStyle(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.OUTKA_KOSU_Z.ColNo, sLabel)
                    .SetCellValue(i - zenzanGyo, LMD040G.sprNyusyukkaZDef.OUTKA_KOSU_Z.ColNo, String.Empty)
                End If

            Next

            .ResumeLayout(True)

        End With

    End Sub

    '''' <summary>
    '''' セルに設定する関数文字列
    '''' </summary>
    '''' <param name="rowNo">行番号</param>
    '''' <param name="colNm">計算に使う列名(アルファベット)</param>
    '''' <returns>数式文字列</returns>
    '''' <remarks></remarks>
    'Private Function CalcCellValue(ByVal rowNo As Integer, ByVal colNm As String(), Optional ByVal nyuukaFlag As Boolean = False) As String

    '    '2017/09/25 追加 李↓
    '    '多言語対応用ユーティリティ
    '    Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
    '    '2017/09/25 追加 李↑

    '    rowNo = rowNo + 1

    '    Dim maeRow As Integer = rowNo - 1

    '    Dim keisanStr As String = String.Concat(colNm(0), rowNo.ToString(), "-", FormuraIfStringEmpty(String.Concat(colNm(1), rowNo.ToString())))

    '    Dim inKeisanStr As String = String.Concat(colNm(0), rowNo.ToString())

    '    Dim maeData As String = "0"

    '    Dim inMaeData As String = "0"

    '    If 0 <> maeRow Then

    '        '2013.03.22 / Notes1920(二行移行に入荷数量を反映させない) 対応 開始 
    '        If 1 = maeRow AndAlso nyuukaFlag = False Then

    '            keisanStr = String.Concat(colNm(2), maeRow.ToString(), "-", FormuraIfStringEmpty(String.Concat(colNm(1), rowNo.ToString())))

    '            'inKeisanStr = String.Concat(colNm(2), maeRow.ToString(), "+", colNm(0), rowNo.ToString()) <=何かあった時のバックログ(1920以前に使用していたモノ)

    '            '2013.03.22 / Notes1920　二行目以降の入荷数足し込みバグ修正(正確には「入荷、移入、振入」時に前歴を足し込まないように修正)
    '            inKeisanStr = String.Concat(colNm(0), rowNo.ToString())

    '            maeData = String.Concat(colNm(2), maeRow.ToString())

    '        Else

    '            keisanStr = String.Concat(colNm(2), maeRow.ToString(), "-", FormuraIfStringEmpty(String.Concat(colNm(1), rowNo.ToString())))

    '            inKeisanStr = String.Concat(colNm(2), maeRow.ToString(), "+", FormuraIfStringEmpty(String.Concat(colNm(0), rowNo.ToString())))

    '            maeData = String.Concat(colNm(2), maeRow.ToString())
    '        End If
    '        '2013.03.22 / Notes1920(二行移行に入荷数量を反映させない) 対応 終了

    '    End If

    '    '2016.02.09 英語化対応　修正START
    '    Dim kbn032Dr() As DataRow = Nothing
    '    Dim strKesi As String = "消"
    '    Dim kbn033Dr() As DataRow = Nothing
    '    Dim strSample As String = "サ"
    '    Dim kbn028Dr() As DataRow = Nothing
    '    Dim strInka As String = "入荷"
    '    Dim kbn031Dr() As DataRow = Nothing
    '    Dim strInyu As String = "移入"
    '    Dim kbn029Dr() As DataRow = Nothing
    '    Dim strHurinyu As String = "振入"
    '    '2017/09/25 修正 李↓
    '    If lgm.MessageLanguage.Equals(LMConst.FLG.OFF) = False Then
    '        kbn032Dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", "K032", _
    '                                                                                                         "' AND SUBSTRING(KBN_CD,2,1) = '", lgm.MessageLanguage, _
    '                                                                                                         "' AND SYS_DEL_FLG = '0'"))

    '        If kbn032Dr.Length > 0 Then
    '            strKesi = kbn032Dr(0).Item("KBN_NM1").ToString()
    '        End If

    '        kbn033Dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", "K033", _
    '                                                                                                         "' AND SUBSTRING(KBN_CD,2,1) = '", lgm.MessageLanguage, _
    '                                                                                                         "' AND SYS_DEL_FLG = '0'"))

    '        If kbn033Dr.Length > 0 Then
    '            strSample = kbn033Dr(0).Item("KBN_NM1").ToString()
    '        End If

    '        kbn028Dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", "K028", _
    '                                                                                                         "' AND SUBSTRING(KBN_CD,2,1) = '", lgm.MessageLanguage, _
    '                                                                                                         "' AND SYS_DEL_FLG = '0'"))

    '        If kbn028Dr.Length > 0 Then
    '            strInka = kbn028Dr(0).Item("KBN_NM1").ToString()
    '        End If

    '        kbn031Dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", "K031", _
    '                                                                                                         "' AND SUBSTRING(KBN_CD,2,1) = '", lgm.MessageLanguage, _
    '                                                                                                         "' AND SYS_DEL_FLG = '0'"))

    '        If kbn031Dr.Length > 0 Then
    '            strInyu = kbn031Dr(0).Item("KBN_NM1").ToString()
    '        End If

    '        kbn029Dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", "K029", _
    '                                                                                                         "' AND SUBSTRING(KBN_CD,2,1) = '", lgm.MessageLanguage, _
    '                                                                                                         "' AND SYS_DEL_FLG = '0'"))
    '        '2017/09/25 修正 李↑

    '        If kbn029Dr.Length > 0 Then
    '            strHurinyu = kbn029Dr(0).Item("KBN_NM1").ToString()
    '        End If

    '        CalcCellValue = String.Concat("IF(OR(B", rowNo.ToString(), "=""", strKesi, """,B", rowNo.ToString(), "=""ヨ"",B", rowNo.ToString(), "=""", strSample, """),", maeData)
    '        CalcCellValue = String.Concat(CalcCellValue, ",IF(OR(C", rowNo.ToString(), "=""", strInka, """,C", rowNo.ToString(), "=""", strInyu, """,C", rowNo.ToString(), "=""", strHurinyu, """),", inKeisanStr, ",", keisanStr, "))")

    '    Else
    '        CalcCellValue = String.Concat("IF(OR(B", rowNo.ToString(), "=""消"",B", rowNo.ToString(), "=""ヨ"",B", rowNo.ToString(), "=""サ""),", maeData)
    '        CalcCellValue = String.Concat(CalcCellValue, ",IF(OR(C", rowNo.ToString(), "=""入荷"",C", rowNo.ToString(), "=""移入"",C", rowNo.ToString(), "=""振入""),", inKeisanStr, ",", keisanStr, "))")
    '    End If
    '    '2016.02.09 英語化対応　修正END

    '    Return CalcCellValue

    'End Function

    'Private Function FormuraIfStringEmpty(ByVal cellName As String) As String

    '    Return String.Concat("IF(", cellName, "="""",0,", cellName, ")")

    'End Function

    ''' <summary>
    ''' セルに設定する関数文字列（で実際に計算した値を返す）
    ''' </summary>
    ''' <param name="spr">スプレッドシートコントロール</param>
    ''' <param name="rowNo">対象となる行番号</param>
    ''' <param name="kosuFlag">True:残個数計算　False:残数量計算</param>
    ''' <param name="nyuukaFlag">True:入荷　False:在庫（省略時は在庫）</param>
    ''' <returns>計算結果</returns>
    ''' <remarks>
    ''' CalcCellValue()による計算式埋め込みはデータ件数増大により実用に耐えない程のパフォーマンス低下を引き起こすので計算を外で行う
    ''' </remarks>
    Private Function CalcCellValueReal(ByVal spr As LMSpread, ByVal rowNo As Integer, ByVal kosuFlag As Boolean, Optional ByVal nyuukaFlag As Boolean = False) As Decimal

        '多言語対応用ユーティリティ
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)

        '判定用文字列定義
        Dim strKesi As String = "消"
        Dim strSample As String = "サ"
        Dim strYo As String = "ヨ"
        Dim strInka As String = "入荷"
        Dim strInyu As String = "移入"
        Dim strHurinyu As String = "振入"

        '判定用文字列の多言語対応
        If Not lgm.MessageLanguage.Equals(LMConst.FLG.OFF) Then
            Dim kbn032Dr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                    .Select(String.Concat("KBN_GROUP_CD = 'K032' AND SUBSTRING(KBN_CD,2,1) = '", lgm.MessageLanguage, "' AND SYS_DEL_FLG = '0'"))
            If kbn032Dr.Length > 0 Then
                strKesi = kbn032Dr(0).Item("KBN_NM1").ToString()
            End If

            Dim kbn033Dr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                    .Select(String.Concat("KBN_GROUP_CD = 'K033' AND SUBSTRING(KBN_CD,2,1) = '", lgm.MessageLanguage, "' AND SYS_DEL_FLG = '0'"))
            If kbn033Dr.Length > 0 Then
                strSample = kbn033Dr(0).Item("KBN_NM1").ToString()
            End If

            Dim kbn028Dr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).
                    Select(String.Concat("KBN_GROUP_CD = 'K028' AND SUBSTRING(KBN_CD,2,1) = '", lgm.MessageLanguage, "' AND SYS_DEL_FLG = '0'"))
            If kbn028Dr.Length > 0 Then
                strInka = kbn028Dr(0).Item("KBN_NM1").ToString()
            End If

            Dim kbn031Dr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).
                    Select(String.Concat("KBN_GROUP_CD = 'K031' AND SUBSTRING(KBN_CD,2,1) = '", lgm.MessageLanguage, "' AND SYS_DEL_FLG = '0'"))
            If kbn031Dr.Length > 0 Then
                strInyu = kbn031Dr(0).Item("KBN_NM1").ToString()
            End If

            Dim kbn029Dr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).
                    Select(String.Concat("KBN_GROUP_CD = 'K029' AND SUBSTRING(KBN_CD,2,1) = '", lgm.MessageLanguage, "' AND SYS_DEL_FLG = '0'"))
            If kbn029Dr.Length > 0 Then
                strHurinyu = kbn029Dr(0).Item("KBN_NM1").ToString()
            End If
        End If

        '前行（スプレッドシートで１つ上の行）の残数を取得
        Dim maeZan As Decimal = 0
        If rowNo > 0 Then
            If kosuFlag Then
                maeZan = CDec(spr.ActiveSheet.Cells(rowNo - 1, LMD040G.sprNyusyukkaZDef.ZAN_KOSU_Z.ColNo).Value.ToString)
            Else
                maeZan = CDec(spr.ActiveSheet.Cells(rowNo - 1, LMD040G.sprNyusyukkaZDef.ZAN_SURYO_Z.ColNo).Value.ToString)
            End If
        End If

        '対象行の入荷数を取得
        Dim curInka As Decimal = 0
        If kosuFlag Then
            curInka = CDec(spr.ActiveSheet.Cells(rowNo, LMD040G.sprNyusyukkaZDef.INKA_KOSU_Z.ColNo).Value.ToString)
        Else
            curInka = CDec(spr.ActiveSheet.Cells(rowNo, LMD040G.sprNyusyukkaZDef.INKA_SURYO_Z.ColNo).Value.ToString)
        End If

        '対象行の出荷数を取得
        Dim curOUtka As Decimal = 0
        If kosuFlag Then
            curOUtka = CDec(spr.ActiveSheet.Cells(rowNo, LMD040G.sprNyusyukkaZDef.OUTKA_KOSU_Z.ColNo).Value.ToString)
        Else
            curOUtka = CDec(spr.ActiveSheet.Cells(rowNo, LMD040G.sprNyusyukkaZDef.OUTKA_SURYO_Z.ColNo).Value.ToString)
        End If

        'その他判定用項目値を取得
        Dim curYojitu As String = spr.ActiveSheet.Cells(rowNo, LMD040G.sprNyusyukkaZDef.YOJITU_Z.ColNo).Value.ToString()
        Dim curSyubetu As String = spr.ActiveSheet.Cells(rowNo, LMD040G.sprNyusyukkaZDef.SYUBETU_Z.ColNo).Value.ToString()

        '残数計算
        Dim curZan As Decimal = 0
        Select Case curYojitu
            Case strKesi, strSample, strYo
                curZan = maeZan
            Case Else
                Select Case curSyubetu
                    Case strInka, strInyu, strHurinyu
                        If (rowNo = 1) AndAlso (Not nyuukaFlag) Then
                            '元のメソッドにあったコメント
                            '二行目以降の入荷数足し込みバグ修正(正確には「入荷、移入、振入」時に前歴を足し込まないように修正)
                            'の再現
                            curZan = curInka
                        Else
                            curZan = maeZan + curInka
                        End If
                    Case Else
                        If rowNo = 0 Then
                            curZan = curInka - curOUtka
                        Else
                            curZan = maeZan - curOUtka
                        End If
                End Select
        End Select

        Return curZan

    End Function

#End Region 'Spread

#End Region

End Class
