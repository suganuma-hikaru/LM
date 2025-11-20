' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : データ管理サブ
'  プログラムID     :  LMI020G : デュポン在庫
'  作  成  者       :  
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMI020Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI020G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI020F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconG As LMIControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI020F, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMIconG = g

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
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.ALL_SPACE_12)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = LMIControlC.FUNCTION_CREATE
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = LMIControlC.FUNCTION_POP
            .F11ButtonName = String.Empty
            .F12ButtonName = LMIControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = always
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = lock
            .F10ButtonEnabled = always
            .F11ButtonEnabled = lock
            .F12ButtonEnabled = always

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

            .cmbPrint.TabIndex = LMI020C.CtlTabIndex.PRINT
            .cmbPlantCd.TabIndex = LMI020C.CtlTabIndex.PLANTCD
            .lblPlantNm.TabIndex = LMI020C.CtlTabIndex.PLANTNM
            .pnlSearch.TabIndex = LMI020C.CtlTabIndex.SEARCH
            .cmbEigyo.TabIndex = LMI020C.CtlTabIndex.EIGYO
            .txtCustCdL.TabIndex = LMI020C.CtlTabIndex.CUSTCDL
            .lblCustNmL.TabIndex = LMI020C.CtlTabIndex.CUSTNML
            .txtCustCdM.TabIndex = LMI020C.CtlTabIndex.CUSTCDM
            .lblCustNmM.TabIndex = LMI020C.CtlTabIndex.CUSTNMM
            .txtCustCdS.TabIndex = LMI020C.CtlTabIndex.CUSTCDS
            .lblCustNmS.TabIndex = LMI020C.CtlTabIndex.CUSTNMS
            .imdHokokuDate.TabIndex = LMI020C.CtlTabIndex.HOKOKUDATE
            .cmbZaiRirekiDate.TabIndex = LMI020C.CtlTabIndex.ZAIRIREKIDATE
            .pnlCust.TabIndex = LMI020C.CtlTabIndex.CUST
            .lblCustDtl.TabIndex = LMI020C.CtlTabIndex.CUSTDTL

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '日付コントロールの書式設定
        Me._LMIconG.SetDateFormat(Me._Frm.imdHokokuDate)

        '編集部の項目をクリア
        Call Me.ClearControl()

        '初期値設定
        Call Me.SetInitData()

    End Sub

    ''' <summary>
    ''' 初期値の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInitData()

        With Me._Frm

            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            .cmbEigyo.SelectedValue = brCd

            'プラントコンボの生成
            Call Me.SetPlantCdCombData(brCd)

            Dim plant As String = String.Empty

            'START YANAI 要望番号769
            If .cmbPlantCd.ReadOnly = False Then
                'END YANAI 要望番号769

                Select Case brCd

                    Case LMI020C.NRS_BR_CD_TIBA

                        plant = LMI020C.PLANT_TIBA

                    Case LMI020C.NRS_BR_CD_OSAKA

                        plant = LMI020C.PLANT_OSAKA

                    Case LMI020C.NRS_BR_CD_YOKOHAMA

                        plant = LMI020C.PLANT_YOKOHAMA

                End Select

                'START YANAI 要望番号769
            End If
            'END YANAI 要望番号769

            'ロード判定用隠し項目に値を設定
            .lblLoadFlg.TextValue = LMConst.FLG.ON

            'プラントコードの初期値設定
            .cmbPlantCd.SelectedValue = plant

            'ロード判定用隠し項目の値をクリア
            .lblLoadFlg.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' プラントコードの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetPlantCD()

        With Me._Frm

            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            Dim plant As String = String.Empty

            If .cmbPlantCd.ReadOnly = False Then

                Select Case brCd

                    Case LMI020C.NRS_BR_CD_TIBA

                        plant = LMI020C.PLANT_TIBA

                    Case LMI020C.NRS_BR_CD_OSAKA

                        plant = LMI020C.PLANT_OSAKA

                    Case LMI020C.NRS_BR_CD_YOKOHAMA

                        plant = LMI020C.PLANT_YOKOHAMA

                End Select

            End If

            'プラントコードの初期値設定
            .cmbPlantCd.SelectedValue = plant

        End With

    End Sub

    ''' <summary>
    ''' プラントコードによる荷主の設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetPlantCust()

        With Me._Frm

            '区分マスタからプラント荷主を取得
            Dim plant As String = .cmbPlantCd.SelectedValue.ToString()
            Dim drs As DataRow() = Nothing
            If String.IsNullOrEmpty(plant) = False Then
                drs = Me._LMIconG.SelectKbnListDataRow(plant, LMKbnConst.KBN_D005)
            End If

            Dim dr As DataRow = Nothing
            Dim plantNm As String = String.Empty
            Dim lCd As String = String.Empty
            Dim mCd As String = String.Empty
            Dim sCd As String = String.Empty

            '取得できた場合、設定
            If drs Is Nothing = False AndAlso 0 < drs.Length Then
                dr = drs(0)
                plantNm = dr.Item("KBN_NM3").ToString()
                lCd = dr.Item("KBN_NM5").ToString()
                mCd = dr.Item("KBN_NM6").ToString()
                sCd = dr.Item("KBN_NM7").ToString()
            End If

            .lblPlantNm.TextValue = plantNm
            .txtCustCdL.TextValue = lCd
            .txtCustCdM.TextValue = mCd
            .txtCustCdS.TextValue = sCd

            '名称を荷主マスタから取得
            drs = Me._LMIconG.SelectCustListDataRow(lCd, mCd, sCd)

            '取得できない場合、スルー
            Dim lNm As String = String.Empty
            Dim mNm As String = String.Empty
            Dim sNm As String = String.Empty
            If 0 < drs.Length Then
                dr = drs(0)
                lNm = dr.Item("CUST_NM_L").ToString()
                mNm = dr.Item("CUST_NM_M").ToString()
                sNm = dr.Item("CUST_NM_S").ToString()
            End If

            .lblCustNmL.TextValue = lNm
            .lblCustNmM.TextValue = mNm
            .lblCustNmS.TextValue = sNm

            '荷主(小)一覧に値を設定
            .lblCustDtl.TextValue = Me._LMIconG.EditConcatData(sCd, sNm, Space(1))

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            'フォーカス位置の初期化
            .Focus()

            .cmbPrint.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .lblLoadFlg.TextValue = String.Empty
            .cmbPrint.SelectedValue = Nothing
            .cmbPlantCd.SelectedValue = Nothing
            .lblPlantNm.TextValue = String.Empty
            .cmbEigyo.SelectedValue = Nothing
            .txtCustCdL.TextValue = String.Empty
            .lblCustNmL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNmM.TextValue = String.Empty
            .txtCustCdS.TextValue = String.Empty
            .lblCustNmS.TextValue = String.Empty
            .imdHokokuDate.Value = Nothing
            .cmbZaiRirekiDate.SelectedValue = Nothing
            .lblCustDtl.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' プラントコンボの設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetPlantCdCombData(ByVal brCd As String)

        With Me._Frm

            'コンボボックスのクリア
            .cmbPlantCd.Items.Clear()

            Dim sql As String = String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_D005, "' " _
                                              , " AND KBN_NM4 = '", brCd, "' " _
                                              )


            'コンボボックス生成
            Me._LMIconG.CreateComboBox(.cmbPlantCd, LMConst.CacheTBL.KBN, New String() {"KBN_CD"}, New String() {"KBN_NM1"}, sql, "KBN_CD")

        End With

    End Sub

    ''' <summary>
    ''' 画面値によるロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetLockControl()

        With Me._Frm

            Dim lock1 As Boolean = True
            Dim lock2 As Boolean = True
            Dim lock3 As Boolean = True
            Dim setFlg As Boolean = False

            Select Case .cmbPrint.SelectedValue.ToString()

                Case LMI020C.PRINT_NITHIJI

                    lock2 = False

                Case LMI020C.PRINT_ZAIKO

                    lock2 = False
                    lock3 = False

                Case LMI020C.PRINT_SFTP

                    lock1 = False
                    setFlg = True

            End Select

            'パターン1
            Me._LMIconG.SetLockInputMan(.cmbPlantCd, lock1, False)

            'パターン2
            Me._LMIconG.SetLockInputMan(.txtCustCdL, lock2)
            Me._LMIconG.SetLockInputMan(.txtCustCdM, lock2)

            'パターン3
            Me._LMIconG.SetLockInputMan(.txtCustCdS, lock3)

            '名称のクリア処理
            Call Me.ClearLabelData(.txtCustCdL, .lblCustNmL)
            Call Me.ClearLabelData(.txtCustCdM, .lblCustNmM)
            Call Me.ClearLabelData(.txtCustCdS, .lblCustNmS)

            'プラント荷主を設定
            If setFlg = True Then
                Call Me.SetPlantCust()
            End If

        End With

    End Sub

    ''' <summary>
    ''' テキストに紐付くラベルをクリア
    ''' </summary>
    ''' <param name="txt">コードコントロール</param>
    ''' <param name="lbl">名称コントロール</param>
    ''' <remarks></remarks>
    Private Sub ClearLabelData(ByVal txt As LMImTextBox, ByVal lbl As LMImTextBox)

        With Me._Frm

            'コードがロックの場合、ラベルをクリア
            If txt.ReadOnly = True Then
                lbl.TextValue = String.Empty
            End If

        End With

    End Sub

    ''' <summary>
    ''' 荷主(小)一覧に値を設定
    ''' </summary>
    ''' <param name="drs"></param>
    ''' <remarks></remarks>
    Friend Sub SetCustSDtl(ByVal drs As DataRow())

        With Me._Frm

            '値のクリア
            .lblCustDtl.TextValue = String.Empty

            '取得できていない場合、終了
            Dim max As Integer = drs.Length - 1
            If max < 0 Then
                Exit Sub
            End If

            '1行目はそのまま設定
            Dim value As String = Me._LMIconG.EditConcatData(drs(0).Item("CUST_CD_S").ToString(), drs(0).Item("CUST_NM_S").ToString(), Space(1))

            '2行目以降は改行文字を設定
            For i As Integer = 1 To max
                value = String.Concat(value, vbNewLine, Me._LMIconG.EditConcatData(drs(i).Item("CUST_CD_S").ToString(), drs(i).Item("CUST_NM_S").ToString(), Space(1)))
            Next

            '値を設定
            .lblCustDtl.TextValue = value

        End With

    End Sub

    ''' <summary>
    ''' 月末在庫コンボ生成処理
    ''' </summary>
    ''' <param name="setDrs">DataRow配列</param>
    ''' <remarks></remarks>
    Friend Sub SetZaikoDateControl(ByVal setDrs As DataRow())

        With Me._Frm

            'リストをクリア
            .cmbZaiRirekiDate.Items.Clear()

            Dim cd As String = String.Empty
            Dim item As String = String.Empty

            '空行追加
            Call Me._LMIconG.ComboBoxItemAdd(.cmbZaiRirekiDate, cd, item)

            'START YANAI 要望番号410
            ''直近在庫の設定
            'Dim drs As DataRow() = Me._LMIconG.SelectKbnListDataRow(, LMKbnConst.KBN_G003)
            'cd = drs(1).Item("KBN_NM2").ToString()
            'item = drs(1).Item("KBN_NM1").ToString()
            'Me._LMIconG.ComboBoxItemAdd(.cmbZaiRirekiDate, cd, item)
            Dim drs As DataRow() = Me._LMIconG.SelectKbnListDataRow(, LMKbnConst.KBN_G003)
            'END YANAI 要望番号410

            'START YANAI 要望番号410
            ''取得した情報の設定
            'Dim max As Integer = setDrs.Length - 1
            'For i As Integer = 0 To max
            '    Call Me.ComboBoxItemAdd(.cmbZaiRirekiDate, setDrs(i).Item("RIREKI_DATE").ToString())
            'Next
            'END YANAI 要望番号410

            '初期在庫の設定
            cd = drs(0).Item("KBN_NM2").ToString()
            item = drs(0).Item("KBN_NM1").ToString()
            Me._LMIconG.ComboBoxItemAdd(.cmbZaiRirekiDate, cd, item)

            'START YANAI 要望番号410
            ''在庫履歴の情報を取得している場合
            'If -1 < max Then

            '    '取得した1件目を初期表示
            '    .cmbZaiRirekiDate.SelectedIndex = 2

            'Else

            '    '直近在庫を初期表示
            '    .cmbZaiRirekiDate.SelectedIndex = 1

            'End If
            .cmbZaiRirekiDate.SelectedIndex = 1
            'END YANAI 要望番号410

        End With

    End Sub

    ''' <summary>
    ''' コンボに行を追加　
    ''' </summary>
    ''' <param name="cmb">コントロール</param>
    ''' <param name="value">設定値</param>
    ''' <remarks></remarks>
    Private Sub ComboBoxItemAdd(ByVal cmb As LMImCombo, ByVal value As String)
        Me._LMIconG.ComboBoxItemAdd(cmb, value, DateFormatUtility.EditSlash(value))
    End Sub

#End Region

#End Region

#End Region

End Class
