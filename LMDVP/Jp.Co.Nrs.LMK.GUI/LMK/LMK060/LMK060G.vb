' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMK     : 支払サブシステム
'  プログラムID     :  LMK060G : 支払印刷指示
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMK060Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMK060G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMK060F

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMKconG As LMKControlG

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMK060F, ByVal g As LMKControlG)

        '親クラスのコンストラクタを呼ぶ。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付る。
        MyBase.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付ける。
        MyBase.MyForm = frm

        Me._Frm = frm

        Me._LMKconG = g

    End Sub

#End Region

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim always As Boolean = True
        Dim lock As Boolean = False
        Dim empty As String = String.Empty

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = empty
            .F2ButtonName = empty
            .F3ButtonName = empty
            .F4ButtonName = empty
            .F5ButtonName = empty
            .F6ButtonName = empty
            .F7ButtonName = LMKControlC.FUNCTION_INSATU
            .F8ButtonName = empty
            .F9ButtonName = empty
            .F10ButtonName = LMKControlC.FUNCTION_MST_SANSHO
            .F11ButtonName = empty
            .F12ButtonName = LMKControlC.FUNCTION_TOJIRU

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

#End Region

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .cmbPrint.TabIndex = LMK060C.CtlTabIndex.Print
            .cmbBr.TabIndex = LMK060C.CtlTabIndex.Br
            .txtUnsocoCd.TabIndex = LMK060C.CtlTabIndex.CustCdL
            .lblUnsocoNm.TabIndex = LMK060C.CtlTabIndex.CustNmL
            .txtUnsocoBrCd.TabIndex = LMK060C.CtlTabIndex.CustCdM
            .lblUnsocoBrNm.TabIndex = LMK060C.CtlTabIndex.CustNmM
            .txtShiharaiCd.TabIndex = LMK060C.CtlTabIndex.ShiharaiCd
            .lblShiharaiNm.TabIndex = LMK060C.CtlTabIndex.ShiharaiNm
            .imdOutkaDateFrom.TabIndex = LMK060C.CtlTabIndex.OutkaDateFrom
            .imdOutkaDateTo.TabIndex = LMK060C.CtlTabIndex.OutkaDateTo

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal data As String)

        '編集部の項目をクリア
        Call Me.ClearControl()

        '自営業所の設定、日付の当月日付1日目、当月日付最終日の設定
        Call Me.SetInput(data)


    End Sub

#Region "コントロールの初期設定"

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControl()

        With Me._Frm

            .cmbPrint.SelectedValue = Nothing
            .cmbBr.SelectedValue = Nothing
            .txtUnsocoCd.TextValue = String.Empty
            .lblUnsocoNm.TextValue = String.Empty
            .txtUnsocoBrCd.TextValue = String.Empty
            .lblUnsocoBrNm.TextValue = String.Empty
            .imdOutkaDateFrom.TextValue = String.Empty
            .imdOutkaDateTo.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    '''営業所、日付の当月の1日目、当月の最終日の設定 
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SetInput(ByVal data As String)
        With Me._Frm
            .cmbBr.SelectedValue = LMUserInfoManager.GetNrsBrCd()

            '当月日付1日目の取得
            Dim nowDate As String = String.Concat(Convert.ToDateTime(DateFormatUtility.EditSlash(data)).ToString("yyyyMM"), "01")

            Dim nextDate As DateTime = Convert.ToDateTime(DateFormatUtility.EditSlash(nowDate))

            .imdOutkaDateFrom.TextValue = nowDate

            '当月の最終日取得
            .imdOutkaDateTo.TextValue = nextDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd")
        End With

    End Sub

    ''' <summary>
    '''荷主・日付の設定 
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SetPrmData(ByVal prmDs As DataSet)

        With Me._Frm
            If 0 < prmDs.Tables(LMControlC.LMK060C_TABLE_NM_IN).Rows.Count Then
                .txtUnsocoCd.TextValue = prmDs.Tables(LMControlC.LMK060C_TABLE_NM_IN).Rows(0).Item("UNSOCO_CD").ToString()
                .txtUnsocoBrCd.TextValue = prmDs.Tables(LMControlC.LMK060C_TABLE_NM_IN).Rows(0).Item("UNSOCO_BR_CD").ToString()
                If String.IsNullOrEmpty(prmDs.Tables(LMControlC.LMK060C_TABLE_NM_IN).Rows(0).Item("F_DATE").ToString()) = False Then
                    .imdOutkaDateFrom.TextValue = prmDs.Tables(LMControlC.LMK060C_TABLE_NM_IN).Rows(0).Item("F_DATE").ToString()
                End If
                If String.IsNullOrEmpty(prmDs.Tables(LMControlC.LMK060C_TABLE_NM_IN).Rows(0).Item("T_DATE").ToString()) = False Then
                    .imdOutkaDateTo.TextValue = prmDs.Tables(LMControlC.LMK060C_TABLE_NM_IN).Rows(0).Item("T_DATE").ToString()
                End If
                .txtShiharaiCd.TextValue = prmDs.Tables(LMControlC.LMK060C_TABLE_NM_IN).Rows(0).Item("SHIHARAI_CD").ToString()

                '名称を取得し、ラベルに表示を行う
                '運送会社
                Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(String.Concat( _
                                                                                                              "UNSOCO_CD = '", .txtUnsocoCd.TextValue, "' AND " _
                                                                                                            , "UNSOCO_BR_CD = '", .txtUnsocoBrCd.TextValue, "'" _
                                                                                               ))
                If 0 < dr.Length Then
                    .lblUnsocoNm.TextValue = dr(0).Item("UNSOCO_NM").ToString()
                    .lblUnsocoBrNm.TextValue = dr(0).Item("UNSOCO_BR_NM").ToString()
                End If

                '支払先
                dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SHIHARAITO).Select(String.Concat( _
                                                                                                 "SHIHARAITO_CD = '", .txtShiharaiCd.TextValue, "'" _
                                                                                  ))
                If 0 < dr.Length Then
                    .lblShiharaiNm.TextValue = String.Concat(dr(0).Item("SHIHARAITO_NM").ToString(), "　", dr(0).Item("SHIHARAITO_BUSYO_NM").ToString())
                End If

            End If
        End With

    End Sub

#End Region

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey()

    End Sub

    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm
            .cmbPrint.Focus()
        End With
    End Sub

    ''' <summary>
    ''' 支払検索から遷移時のみ初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlPrm(ByVal prmDs As DataSet, ByVal rootPGID As String)

        '支払検索から遷移時のみ初期値設定
        If (LMK060C.PGID_LMF080).Equals(rootPGID) = True Then
            Call Me.SetPrmData(prmDs)
        End If

    End Sub

#End Region

#Region "印刷区分変更時"

    ''' <summary>
    ''' 印刷区分値変更のロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub Locktairff(ByVal frm As LMK060F)

        With Me._Frm

            Dim lockflgCust As Boolean = False
            Dim lockflgSiqto As Boolean = False
            Dim lockflgdata As Boolean = False

            '印刷区分
            Dim PrintKb As String = .cmbPrint.SelectedValue.ToString

            '印刷種別が支払運賃チェックリストの場合
            If LMK060C.PRINT_SHIHARAI_CHECK.Equals(PrintKb) = True Then

                '支払先コードをロック
                lockflgSiqto = True

                'クリアするもの
                .txtShiharaiCd.TextValue = String.Empty
                .lblShiharaiNm.TextValue = String.Empty
            End If
            Me._LMKconG.SetLockControl(.cmbPrint, lockflgCust)
            Me._LMKconG.SetLockControl(.txtUnsocoCd, lockflgCust)
            Me._LMKconG.SetLockControl(.txtUnsocoBrCd, lockflgCust)
            Me._LMKconG.SetLockControl(.txtShiharaiCd, lockflgSiqto)
            Me._LMKconG.SetLockControl(.imdOutkaDateFrom, lockflgdata)
            Me._LMKconG.SetLockControl(.imdOutkaDateTo, lockflgdata)

        End With

    End Sub

#End Region

#End Region

#End Region

End Class
