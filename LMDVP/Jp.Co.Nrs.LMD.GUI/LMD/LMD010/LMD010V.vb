' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LM     : 在庫サブシステム
'  プログラムID     :  LMD010V : 在庫振替入力
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李

''' <summary>
''' LMD010Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMD010V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD010F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMDControlV

    '2017/09/25 修正 李↓
    '    ''' <summary>
    '    ''' 選択した言語を格納するフィールド
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '#If False Then '_LangFlgが初期化される前にアクセスしてされる問題の仮対応 20151109 INOUE
    '    Private _LangFlg As String
    '#Else
    '    Private _LangFlg As String = Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage
    '#End If
    '2017/09/25 修正 李↑

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMD010F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = New LMDControlV(handlerClass, DirectCast(frm, LMFormSxga))

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 振替元確定時の単項目チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsEditMotoInputChk(ByVal arr As ArrayList, ByVal chkDs As DataSet) As Boolean

        '検索項目のTrim
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        If Me.IsEditMotoInputChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 振替元確定時の各種チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsFurikaeInputChk(ByVal arr As ArrayList, ByVal chkDs As DataSet) As Boolean

        '荷主マスタ存在チェック
        If Me.IsExitsCustCheck(LMConst.FLG.OFF) = False Then
            Return False
        End If

        '作業項目マスタチェック
        If Me.IsExitsSagyoCheck(LMConst.FLG.OFF) = False Then
            Return False
        End If

        '関連チェック
        If Me.IsMotoInterrelatedCheck(arr, chkDs) = False Then
            Return False
        End If

        '振替元データチェック
        If Me.IsFuriMotoChk(arr) = False Then
            Return False
        End If

        '保管料有無チェック
        If Me.IsHokanUmuChk(arr) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 振替確定時の各種チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsFurikaeKakuteiInputChk(ByVal arrNew As ArrayList, ByVal arrOld As ArrayList, ByVal chkDs As DataSet) As Boolean

        '検索項目のTrim
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        If Me.IsEditSakiInputChk() = False Then
            Return False
        End If

        'スプレッドの単項目チェック
        If Me.IsSpreadSakiInputChk(arrNew) = False Then
            Return False
        End If

        '荷主マスタ存在チェック
        If Me.IsExitsCustCheck(LMConst.FLG.ON) = False Then
            Return False
        End If

        '商品マスタ存在チェック
        If Me.IsExitsGoodsCheck(LMConst.FLG.ON) = False Then
            Return False
        End If

        '作業項目マスタチェック
        If Me.IsExitsSagyoCheck(LMConst.FLG.ON) = False Then
            Return False
        End If

        '棟・室・ゾーンマスタチェック
        If Me.IsExitsTouSituZoneCheck(LMConst.FLG.ON) = False Then
            Return False
        End If

        '振替数量チェック
        If Me.totalSuryoChk(arrNew, arrOld) = False Then
            Return False
        End If

        '振替日＋請求日(保管、荷役、作業全て)
        If Me.IsSeiqDateChk(chkDs, LMConst.FLG.ON) = False Then
            Return False
        End If

        '作業重複チェック
        If Me.IsSagyoJufukuChk(LMD010C.SagyoData.N) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal actionType As LMD010C.ActionType) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case actionType

            Case LMD010C.ActionType.HENSHU          '編集
                '10:閲覧者、50:外部の場合エラー
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

            Case LMD010C.ActionType.FUKUSHA          '複写
                '10:閲覧者、50:外部の場合エラー
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


            Case LMD010C.ActionType.HIKIATE          '引当
                '10:閲覧者、50:外部の場合エラー
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

            Case LMD010C.ActionType.ZENRYO         '全量
                '10:閲覧者、50:外部の場合エラー
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

            Case LMD010C.ActionType.MASTER          'マスタ参照
                '10:閲覧者、50:外部の場合エラー
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

            Case LMD010C.ActionType.FURIKAEMOTOKAKUTEI       '振替元確定
                '10:閲覧者、50:外部の場合エラー
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

            Case LMD010C.ActionType.FURIKAEKAKUTEI           '振替確定
                '10:閲覧者、50:外部の場合エラー
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

            Case LMD010C.ActionType.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMD010C.ActionType.COLDEL           '行削除(振替元)
                '10:閲覧者、50:外部の場合エラー
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

            Case LMD010C.ActionType.COLADDNEW           '行追加(振替先)
                '10:閲覧者、50:外部の場合エラー
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

            Case LMD010C.ActionType.COLDELNEW           '行削除(振替先)
                '10:閲覧者、50:外部の場合エラー
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
    ''' 未選択チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectChk(ByVal chkCnt As Integer) As Boolean

        'チェック件数が0件
        If chkCnt = 0 Then

            MyBase.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 振替元データチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFuriMotoChk(ByVal arr As ArrayList) As Boolean

        If arr.Count = 0 Then
            MyBase.ShowMessage("E277")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 保管料有無チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsHokanUmuChk(ByVal arr As ArrayList) As Boolean

        Dim dr As DataRow = Nothing
        Dim arrHokan As ArrayList = New ArrayList()
        Dim newHokan As String = String.Empty
        Dim oldHokan As String = String.Empty


        For i As Integer = 0 To arr.Count - 1

            '保管料有無
            arrHokan.Add(Me._Vcon.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(i, LMD010G.sprDetailDef.MOTO_HOKAN_YN.ColNo)).ToString())

        Next

        For i As Integer = 0 To arrHokan.Count - 1

            If i = 0 Then
                newHokan = arrHokan(i).ToString()
                oldHokan = arrHokan(i).ToString()
            Else
                newHokan = arrHokan(i).ToString()

                If oldHokan.ToString().Equals(newHokan.ToString()) = True Then
                    Exit For
                Else
                    MyBase.ShowMessage("E278")
                    Return False
                End If

            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 引当数量、引当中チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function hikiateChk(ByVal arr As ArrayList) As Boolean

        Dim suryo As Decimal = 0
        Dim irime As Decimal = 0
        Dim jituSuryo As Decimal = 0

        For i As Integer = 0 To arr.Count - 1

            '引当中数量の取得
            suryo = Convert.ToDecimal(Me._Vcon.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_HURIKAE_SURYO.ColNo)).ToString())

            '入目の取得
            irime = Convert.ToDecimal(Me._Frm.numIrime.Value)

            '実予在庫数量
            jituSuryo = Convert.ToDecimal(Me._Vcon.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_PORA_ZAI_QT.ColNo)).ToString())

            '引当中チェック
            If suryo <> 0 = True AndAlso jituSuryo < irime AndAlso suryo < jituSuryo Then
                Me._Vcon.SetErrMessage("E276")
                Return False
            End If

            '引当中チェック
            If suryo <> 0 AndAlso jituSuryo >= irime AndAlso suryo < irime Then
                Me._Vcon.SetErrMessage("E276")
                Return False
            End If

        Next

        Return True

    End Function

#Region "関連チェック"

    ''' <summary>
    ''' 振替元確定時の関連チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsMotoInterrelatedCheck(ByVal arr As ArrayList, ByVal chkDs As DataSet) As Boolean

        With Me._Frm

            '引当残個数のチェック(0でない場合はエラー)
            If 0 <> Convert.ToInt32(.lblHikiZanCnt.TextValue) = True Then
                Me._Vcon.SetErrorControl(Me._Frm.lblHikiZanCnt)
                MyBase.ShowMessage("E275")
                Return False
            End If

            '振替日＋移動日
            If Me.IsIdoDateChk(chkDs) = False Then
                Return False
            End If

            '振替日＋入荷日
            If Me.IsLastInKaDateChk(arr) = False Then
                Return False
            End If

            '小分け引当中チェック
            If Me.hikiateChk(arr) = False Then
                Return False
            End If

            '上限チェック(個数)
            If Me.IsCalcOver(Me._Frm.lblKosuCnt.TextValue, LMD010C.SURYO_MIN, LMD010C.SURYO_MAX, .lblTitleKosu.TextValue) = False Then
                Me._Vcon.SetErrorControl(Me._Frm.numCnt)
                Me._Vcon.SetErrorControl(Me._Frm.numKonsu)
                Return False
            End If

            '上限チェック(引当残個数)
            If Me.IsCalcOver(Me._Frm.lblHikiZanCnt.TextValue, LMD010C.SURYO_MIN, LMD010C.SURYO_MAX, .lblTitleHikiZanCnt.TextValue) = False Then
                Me._Vcon.SetErrorControl(Me._Frm.numCnt)
                Me._Vcon.SetErrorControl(Me._Frm.numKonsu)
                Return False
            End If

            '振替日＋請求日(作業)
            If Me.IsSeiqDateChk(chkDs, LMConst.FLG.OFF) = False Then
                Return False
            End If

            '作業重複チェック
            If Me.IsSagyoJufukuChk(LMD010C.SagyoData.O) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    ''' <summary>
    ''' 危険物倉庫棟室チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arrMoto">チェック行群</param>
    ''' <param name="ikkatsu">0：行毎、1：一括</param>
    ''' <param name="expDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsDangerousGoodsCheck(ByVal frm As LMD010F, ByVal arrMoto As ArrayList, ByVal ikkatsu As String, ByVal expDs As DataSet) As Boolean

        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)

        With Me._Frm

            Dim motoMax As Integer = arrMoto.Count - 1
            Dim checkRow As Integer = 0

            Dim nrsbrcd As String = .cmbNrsBrCd.SelectedValue.ToString
            Dim sokocd As String = .cmbSoko.SelectedValue.ToString
            Dim custcd As String = .txtCustCdLNew.TextValue

            Dim goodsNRS As String = String.Empty
            Dim goodsDr As DataRow() = Nothing
            Dim kikenkbn As String = String.Empty

            Dim tousituDr As DataRow() = Nothing
            Dim soko_kbn As String = String.Empty
            Dim isTasya As Boolean = False

            Dim touNo As String = String.Empty
            Dim situNo As String = String.Empty
            Dim msg As String = String.Empty

            '危険物チェック結果をワーニングで出すかエラーで出すかの情報を区分テーブルより取得
            Dim selectString As String = String.Empty
            '削除フラグ
            selectString = String.Concat(selectString, " SYS_DEL_FLG = '0' ")
            '区分グループコード
            selectString = String.Concat(selectString, " AND KBN_GROUP_CD = 'S111' ")
            '区分コード
            selectString = String.Concat(selectString, " AND KBN_CD = '0' ")

            Dim targetDataRow() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(selectString)
            Dim IsCheckError As Boolean = Convert.ToInt32(Convert.ToDecimal(targetDataRow(0).Item("VALUE1").ToString)).Equals(Convert.ToInt32(LMD020C.DangerousGoodsCheckErrorOrWarning.Err))

            For i As Integer = 0 To motoMax

                checkRow = Convert.ToInt32(arrMoto(i).ToString)

                '商品マスタ
                goodsNRS = Me._Frm.lblGoodsCdNrsNew.TextValue.ToString
                goodsDr = Me._Vcon.SelectgoodsListDataRow(nrsbrcd, goodsNRS)

                kikenkbn = goodsDr(0).Item("KIKEN_KB").ToString

                touNo = Me._Vcon.GetCellValue(.sprDtlNew.ActiveSheet.Cells(checkRow, LMD010G.sprDetailNewDef.SAKI_TOU_NO.ColNo)).ToString
                situNo = Me._Vcon.GetCellValue(.sprDtlNew.ActiveSheet.Cells(checkRow, LMD010G.sprDetailNewDef.SAKI_SITU_NO.ColNo)).ToString

                '棟室マスタ
                tousituDr = Me._Vcon.SelectTouSituListDataRow(nrsbrcd, sokocd, touNo, situNo)

                '棟室マスタが取得できなければエラー、ワーニングとも起こさせない。
                If tousituDr.Length.Equals(0) Then
                    Continue For
                End If

                soko_kbn = tousituDr(0).Item("SOKO_KB").ToString
                isTasya = tousituDr(0).Item("JISYATASYA_KB").ToString.Equals("02")

                If IsCheckError Then

                    '危険物チェックをエラーとする場合

                    '危険物チェック
                    Dim isErr As Boolean = False
                    If (kikenkbn.Equals("02") OrElse kikenkbn.Equals("03")) AndAlso soko_kbn = "11" Then
                        If isTasya Then
                            isErr = True
                        ElseIf String.IsNullOrEmpty(touNo) OrElse String.IsNullOrEmpty(situNo) Then
                            isErr = True
                        Else
                            Dim drTouSituExp As DataRow() = expDs.Tables(LMD010C.TABLE_NM_TOU_SITU_EXP).Select("TOU_NO = '" & touNo & "' AND SITU_NO = '" & situNo & "'")
                            If drTouSituExp.Count = 0 Then
                                isErr = True
                            End If
                        End If
                        If isErr Then
                            msg = String.Concat(touNo, "-", situNo)
                            MyBase.ShowMessage("G092", New String() {msg})
                            Return False
                        End If
                    End If

                    '一般品の商品(kikenkbn=01)で、倉庫区分(soko_kb=02)が危険物倉庫であればワーニング
                    If (kikenkbn.Equals("01") OrElse kikenkbn.Equals("04")) AndAlso soko_kbn = "02" Then
                        msg = String.Concat(touNo, "-", situNo)
                        MyBase.ShowMessage("G091", New String() {msg})
                        Return False
                    End If
                Else

                    '2017/12/06 他社の場合はワーニングを出さない処理追記
                    If isTasya.Equals(False) Then

                        '危険物チェックをワーニングとする場合
                        msg = String.Concat(touNo, "-", situNo)

                        '危険品(kikenkbn<>01)で、倉庫区分(soko_kb=11)が普通倉庫であればワーニング
                        If kikenkbn <> "01" AndAlso soko_kbn = "11" Then
                            If MyBase.ShowMessage("W239", New String() {msg}) = MsgBoxResult.Cancel Then Return False
                        End If
                        '一般品の商品(kikenkbn=01)で、倉庫区分(soko_kb=02)が危険物倉庫であればワーニング
                        If kikenkbn.Equals("01") AndAlso soko_kbn = "02" Then
                            If MyBase.ShowMessage("W238", New String() {msg}) = MsgBoxResult.Cancel Then Return False
                        End If

                    End If

                End If

            Next

        End With

        Return True
    End Function
    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

#End Region

#Region "単項目チェック"

    ''' <summary>
    ''' 検索項目の単項目チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsEditInputChk() As Boolean

        '検索項目のTrim
        Call Me.TrimSpaceTextValue()

        With Me._Frm

            '営業所
            '20151030 tsunehira add Start
            '英語化対応
            .cmbNrsBrCd.ItemName = .lblTitleEigyo.TextValue
            '20151030 tsunehira add End
            '.cmbNrsBrCd.ItemName = "営業所"
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If

            '倉庫
            '20151030 tsunehira add Start
            '英語化対応
            .cmbSoko.ItemName = .lblTitleSoko.TextValue
            '20151030 tsunehira add End
            '.cmbSoko.ItemName = "倉庫"
            .cmbSoko.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbSoko) = False Then
                Return False
            End If

            '振替区分
            '20151030 tsunehira add Start
            '英語化対応
            .cmbFurikaeKbn.ItemName = .lblTitleFurikaeKbn.TextValue
            '20151030 tsunehira add End
            '.cmbFurikaeKbn.ItemName = "振替区分"
            .cmbFurikaeKbn.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbFurikaeKbn) = False Then
                Return False
            End If

            '振替日
            '20151030 tsunehira add Start
            '英語化対応
            .imdFurikaeDate.ItemName = .lblTitleFruikaeDate.TextValue
            '20151030 tsunehira add End
            '.imdFurikaeDate.ItemName = "振替日"
            .imdFurikaeDate.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdFurikaeDate) = False Then
                Return False
            End If

            If .imdFurikaeDate.IsDateFullByteCheck = False Then
                Me._Vcon.SetErrorControl(Me._Frm.imdFurikaeDate)
                Me._Vcon.SetErrMessage("E038", New String() {.imdFurikaeDate.ItemName, "8"})
                Return False
            End If

            '当期保管料負担区分
            '20151030 tsunehira add Start
            '英語化対応
            .cmbToukiHokanKbn.ItemName = .lblTitleToukiHokanKbn.TextValue
            '20151030 tsunehira add End
            '.cmbToukiHokanKbn.ItemName = "当期保管料負担区分"
            .cmbToukiHokanKbn.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbToukiHokanKbn) = False Then
                Return False
            End If

            '商品が変更出来ないようなケース（簿外品・ロット変更）は、容器有りだとエラーとする
            Select Case .cmbFurikaeKbn.SelectedValue.ToString()
                Case LMD010C.FURIKAE_KBN_HAKUGAIHIN _
                   , LMD010C.FURIKAE_KBN_LOT
                    If .chkYoukiChange.GetBinaryValue.Equals(LMConst.FLG.ON) Then
                        Me._Vcon.SetErrMessage("E382")
                        Me._Vcon.SetErrorControl(New Control() {.cmbFurikaeKbn, .chkYoukiChange}, .cmbFurikaeKbn)
                        Return False
                    End If
            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 振替元確定時の入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsEditMotoInputChk() As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            If Me.IsEditInputChk() = False Then
                Return False
            End If

            'オーダー番号
            '20151030 tsunehira add Start
            '英語化対応
            .txtOrderNo.ItemName = .lblTitleOrderNo.TextValue
            '20151030 tsunehira add End
            '.txtOrderNo.ItemName = "オーダー番号"
            .txtOrderNo.IsForbiddenWordsCheck = True
            .txtOrderNo.IsByteCheck = 30
            If MyBase.IsValidateCheck(.txtOrderNo) = False Then
                Return False
            End If

            '2017/09/25 修正 李↓
            .txtGoodsCdCust.ItemName = lgm.Selector({"商品コード", "Goods Code", "상품코드", "中国語"})
            '2017/09/25 修正 李↑

            .txtGoodsCdCust.IsHissuCheck = True
            .txtGoodsCdCust.IsForbiddenWordsCheck = True
            .txtGoodsCdCust.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtGoodsCdCust) = False Then
                Return False
            End If

            '商品マスタ存在チェック
            If Me.IsExitsGoodsCheck(LMConst.FLG.OFF) = False Then
                Return False
            End If

            '商品名称
            '20151030 tsunehira add Start
            '英語化対応
            .txtGoodsNmCust.ItemName = .lblTitleGoods.TextValue
            '20151030 tsunehira add End
            '.txtGoodsNmCust.ItemName = "商品名称"
            .txtGoodsNmCust.IsForbiddenWordsCheck = True
            .txtGoodsNmCust.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtGoodsNmCust) = False Then
                Return False
            End If

            'ロット№
            '20151030 tsunehira add Start
            '英語化対応
            .txtLotNo.ItemName = .lblTitleLotNo.TextValue
            '20151030 tsunehira add End
            '.txtLotNo.ItemName = "ロット№"
            .txtLotNo.IsForbiddenWordsCheck = True
            .txtLotNo.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtLotNo) = False Then
                Return False
            End If

            'シリアル№
            '20151030 tsunehira add Start
            '英語化対応
            .txtSerialNo.ItemName = .lblTitleSerialNo.TextValue
            '20151030 tsunehira add End
            '.txtSerialNo.ItemName = "シリアル№"
            .txtSerialNo.IsForbiddenWordsCheck = True
            .txtSerialNo.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtSerialNo) = False Then
                Return False
            End If

            '荷役料
            '20151030 tsunehira add Start
            '英語化対応
            .cmbNiyaku.ItemName = .lblTitleNiyaku.TextValue
            '20151030 tsunehira add End
            '.cmbNiyaku.ItemName = "荷役料"
            .cmbNiyaku.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNiyaku) = False Then
                Return False
            End If

            '付帯作業コード①
            '20151030 tsunehira add Start
            '英語化対応
            .txtSagyoCdO1.ItemName = String.Concat(.pnlHutaiSagyo.TextValue, "①")
            '20151030 tsunehira add End
            '.txtSagyoCdO1.ItemName = "付帯作業コード①"
            .txtSagyoCdO1.IsForbiddenWordsCheck = True
            .txtSagyoCdO1.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtSagyoCdO1) = False Then
                Return False
            End If

            '付帯作業コード②
            '20151030 tsunehira add Start
            '英語化対応
            .txtSagyoCdO2.ItemName = String.Concat(.pnlHutaiSagyo.TextValue, "②")
            '20151030 tsunehira add End
            '.txtSagyoCdO2.ItemName = "付帯作業コード②"
            .txtSagyoCdO2.IsForbiddenWordsCheck = True
            .txtSagyoCdO2.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtSagyoCdO1) = False Then
                Return False
            End If

            '付帯作業コード③
            '20151030 tsunehira add Start
            '英語化対応
            .txtSagyoCdO3.ItemName = String.Concat(.pnlHutaiSagyo.TextValue, "③")
            '20151030 tsunehira add End
            '.txtSagyoCdO3.ItemName = "付帯作業コード③"
            .txtSagyoCdO3.IsForbiddenWordsCheck = True
            .txtSagyoCdO3.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtSagyoCdO1) = False Then
                Return False
            End If

            '出荷時注意事項
            '20151030 tsunehira add Start
            '英語化対応
            .txtSyukkaRemark.ItemName = .pnlSyukkaRemark.TextValue
            '20151030 tsunehira add End
            '.txtSyukkaRemark.ItemName = "出荷時注意事項"
            .txtSyukkaRemark.IsForbiddenWordsCheck = True
            .txtSyukkaRemark.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtSyukkaRemark) = False Then
                Return False
            End If

            '引当済個数＜＞個数の場合、エラー
            If .lblHikiSumiCnt.TextValue.Equals(.lblKosuCnt.TextValue) = False Then
                .numCnt.BackColor = LM.Utility.LMGUIUtility.GetAttentionBackColor()
                Me._Vcon.SetErrorControl(.numKonsu)
                Me._Vcon.SetErrMessage("E304")
                Return False
            End If

            '同一振替不可の組み合わせチェック
            '　変動保管料の都合により、商品・ロット・入目が同一で入荷日が異なるものは一緒に振替不可とする。
            '　商品はヘッダにある（＝同一である）のでそれ以外の項目でチェックを行う。
            Dim max As Integer = .spdDtl.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To max
                Dim lotNo_1 As String = Me._Vcon.GetCellValue(.spdDtl.ActiveSheet.Cells(i, LMD010G.sprDetailDef.MOTO_LOT_NO.ColNo)).ToString()
                Dim irime_1 As String = Me._Vcon.GetCellValue(.spdDtl.ActiveSheet.Cells(i, LMD010G.sprDetailDef.MOTO_IRIME.ColNo)).ToString()
                Dim inkaDate_1 As String = Me._Vcon.GetCellValue(.spdDtl.ActiveSheet.Cells(i, LMD010G.sprDetailDef.MOTO_INKA_DATE.ColNo)).ToString()

                For j As Integer = i + 1 To max
                    Dim lotNo_2 As String = Me._Vcon.GetCellValue(.spdDtl.ActiveSheet.Cells(j, LMD010G.sprDetailDef.MOTO_LOT_NO.ColNo)).ToString()
                    Dim irime_2 As String = Me._Vcon.GetCellValue(.spdDtl.ActiveSheet.Cells(j, LMD010G.sprDetailDef.MOTO_IRIME.ColNo)).ToString()
                    Dim inkaDate_2 As String = Me._Vcon.GetCellValue(.spdDtl.ActiveSheet.Cells(j, LMD010G.sprDetailDef.MOTO_INKA_DATE.ColNo)).ToString()

                    If (lotNo_2.Equals(lotNo_1)) AndAlso (irime_2.Equals(irime_1)) AndAlso (Not inkaDate_2.Equals(inkaDate_1)) Then
                        Me._Vcon.SetErrMessage("E02R")
                        Return False
                    End If
                Next
            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 振替確定時の単項目チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsEditSakiInputChk() As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            '荷主コード(大)
            '.txtCustCdLNew.ItemName = "荷主コード(大)"
            .txtCustCdLNew.ItemName = .lblTitleCust.TextValue
            .txtCustCdLNew.IsForbiddenWordsCheck = True
            .txtCustCdLNew.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCdLNew) = False Then
                Return False
            End If

            '荷主コード(中)

            '2017/09/25 修正 李↓
            .txtCustCdMNew.ItemName = lgm.Selector({"荷主コード(中)", "荷主コード(中)", "하주코드(中)", "中国語"})
            '2017/09/25 修正 李↑

            .txtCustCdMNew.IsForbiddenWordsCheck = True
            .txtCustCdMNew.IsByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCdMNew) = False Then
                Return False
            End If

            '送状番号
            '20151030 tsunehira add Start
            '英語化対応
            .txtDenpNo.ItemName = .lblTitleDenpNo.TextValue
            '20151030 tsunehira add End
            '.txtDenpNo.ItemName = "送り状番号"
            .txtDenpNo.IsForbiddenWordsCheck = True
            .txtDenpNo.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtDenpNo) = False Then
                Return False
            End If

            '商品コード
           
            '2017/09/25 修正 李↓
            .txtGoodsCdCustNew.ItemName = lgm.Selector({"商品コード", "Goods Code", "상품코드", "中国語"})
            '2017/09/25 修正 李↑

            .txtGoodsCdCustNew.IsHissuCheck = True
            .txtGoodsCdCustNew.IsForbiddenWordsCheck = True
            .txtGoodsCdCustNew.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtGoodsCdCustNew) = False Then
                Return False
            End If

            '商品名称
            '20151030 tsunehira add Start
            '英語化対応
            .txtGoodsNmCustNew.ItemName = .lblTitleGoodsNew.TextValue
            '20151030 tsunehira add End
            '.txtGoodsNmCustNew.ItemName = "商品名称"
            .txtGoodsNmCustNew.IsForbiddenWordsCheck = True
            .txtGoodsNmCustNew.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtGoodsNmCustNew) = False Then
                Return False
            End If

            '商品キー必須チェック
            If String.IsNullOrEmpty(.lblGoodsCdNrsNew.TextValue) Then
                '20151029 tsunehira add Start
                '英語化対応
                Me._Vcon.SetErrMessage("E803")
                '2015.10.29 tusnehira add End
                'Me._Vcon.SetErrMessage("E019", New String() {"商品キー"})
                Me._Vcon.SetErrorControl(.txtGoodsCdCustNew)
                Return False
            End If

            '商品コードのマスタ存在チェック
            If Me.IsExitsGoodsCheck(LMConst.FLG.ON) = False Then
                Return False
            End If

            '荷役料
            '20151030 tsunehira add Start
            '英語化対応
            .cmbNiyakuNew.ItemName = .lblTitleNiyakuNew.TextValue
            '20151030 tsunehira add End
            '.cmbNiyakuNew.ItemName = "荷役料"
            .cmbNiyakuNew.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNiyakuNew) = False Then
                Return False
            End If

            '課税区分
            '20151030 tsunehira add Start
            '英語化対応
            .cmbTaxKbnNew.ItemName = .lblTitleTaxKbnNew.TextValue
            '20151030 tsunehira add End
            '.cmbTaxKbnNew.ItemName = "課税区分"
            .cmbTaxKbnNew.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbTaxKbnNew) = False Then
                Return False
            End If

            '付帯作業コード①
            '20151030 tsunehira add Start
            '英語化対応
            .txtSagyoCdN1.ItemName = String.Concat(.pnlHutaiSagyoNew.TextValue, "①")
            '20151030 tsunehira add End
            '.txtSagyoCdN1.ItemName = "付帯作業コード①"
            .txtSagyoCdN1.IsForbiddenWordsCheck = True
            .txtSagyoCdN1.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtSagyoCdN1) = False Then
                Return False
            End If

            '付帯作業コード②
            '20151030 tsunehira add Start
            '英語化対応
            .txtSagyoCdN2.ItemName = String.Concat(.pnlHutaiSagyoNew.TextValue, "②")
            '20151030 tsunehira add End
            '.txtSagyoCdN2.ItemName = "付帯作業コード②"
            .txtSagyoCdN2.IsForbiddenWordsCheck = True
            .txtSagyoCdN2.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtSagyoCdN2) = False Then
                Return False
            End If

            '付帯作業コード③
            '20151030 tsunehira add Start
            '英語化対応
            .txtSagyoCdN3.ItemName = String.Concat(.pnlHutaiSagyoNew.TextValue, "③")
            '20151030 tsunehira add End
            '.txtSagyoCdN3.ItemName = "付帯作業コード③"
            .txtSagyoCdN3.IsForbiddenWordsCheck = True
            .txtSagyoCdN3.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtSagyoCdN3) = False Then
                Return False
            End If

            '入荷時注意事項
            '20151030 tsunehira add Start
            '英語化対応
            .txtNyukaRemark.ItemName = .pnlNyukaRemark.TextValue
            '20151030 tsunehira add End
            '.txtNyukaRemark.ItemName = "入荷時注意事項"
            .txtNyukaRemark.IsForbiddenWordsCheck = True
            .txtNyukaRemark.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtNyukaRemark) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#If True Then   'ADD 2018/12/27依頼番号 : 003904   【LMS】在庫振替入力時の印刷プレビューを再度閲覧したい

    ''' <summary>
    ''' 印刷時の単項目チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsPrintInputChk() As Boolean

        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)

        With Me._Frm

            '振替管理番号
            .lblFurikaeNo.ItemName = .lblTitleFurikaeKnariNo.TextValue
            .lblFurikaeNo.IsForbiddenWordsCheck = True
            .lblFurikaeNo.IsByteCheck = 8
            .lblFurikaeNo.IsHissuCheck = True
            If MyBase.IsValidateCheck(.lblFurikaeNo) = False Then
                Return False
            End If

        End With

        Return True

    End Function
#End If

    ''' <summary>
    ''' 引当、全量ボタン押下時の単項目、関連チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsFunctionInputChk(ByVal hikiZenFlg As Integer) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            '荷主コード(大)
        
            '2017/09/25 修正 李↓
            .txtCustCdL.ItemName = lgm.Selector({"荷主コード(大)", "Custmer Code (L)", "하주코드(大)", "中国語"})
            '2017/09/25 修正 李↑

            '全量の場合のみ必須チェックを行う
            If hikiZenFlg = 1 Then
                .txtCustCdL.IsHissuCheck = True
            End If
            .txtCustCdL.IsForbiddenWordsCheck = True
            .txtCustCdL.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                Return False
            End If

            '荷主コード(中)
      
            '2017/09/25 修正 李↓
            .txtCustCdM.ItemName = lgm.Selector({"荷主コード(中)", "Custmer Code (M)", "하주코드(中)", "中国語"})
            '2017/09/25 修正 李↑

            If hikiZenFlg = 1 Then
                .txtCustCdM.IsHissuCheck = True
            End If
            .txtCustCdM.IsForbiddenWordsCheck = True
            .txtCustCdM.IsByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                Return False
            End If

            '荷主名称セット
            Call Me.IsExitsCustCheck(LMConst.FLG.OFF)

            '商品コード
          
            '2017/09/25 修正 李↓
            .txtGoodsCdCust.ItemName = lgm.Selector({"商品コード", "Goods Code", "상품코드", "中国語"})
            '2017/09/25 修正 李↑

            .txtGoodsCdCust.IsHissuCheck = True
            .txtGoodsCdCust.IsForbiddenWordsCheck = True
            .txtGoodsCdCust.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtGoodsCdCust) = False Then
                Return False
            End If

            '商品コードのマスタ存在チェック
            If Me.IsExitsGoodsCheck(LMConst.FLG.OFF) = False Then
                Return False
            End If

            'ロット№
            'コンボボックスの値が"ロット番号変更の場合のみチェックを行う"
            If .cmbFurikaeKbn.SelectedText.Equals("ロット番号変更") = True Then
                .txtLotNo.ItemName = .lblTitleLotNo.TextValue
                .txtLotNo.IsForbiddenWordsCheck = True
                .txtLotNo.IsByteCheck = 40
                If MyBase.IsValidateCheck(.txtLotNo) = False Then
                    Return False
                End If
            End If

            '要望番号:1098 yamanaka 2012.07.05 Start
            ''管理レベル(ロット指定の有無) + ロット№の必須チェック(引当時のみ)
            'If hikiZenFlg = 0 Then
            '    Dim drs As DataRow() = Nothing
            '    'キャッシュテーブルより、画面項目に該当するレコードを取得する(管理レベルは固定値で"01")
            '    drs = Me._Vcon.SelectgoodsListDataRow(.cmbNrsBrCd.SelectedValue.ToString() _
            '                                          , .txtGoodsCdCust.TextValue _
            '                                          , .txtCustCdL.TextValue _
            '                                          , .txtCustCdM.TextValue _
            '                                          , LMConst.FLG.ON)

            '    '取得したレコード:"01"かつロット№が空の場合はエラー
            '    If 0 <> drs.Length = True And String.IsNullOrEmpty(.txtLotNo.TextValue) = True Then
            '        MyBase.ShowMessage("E273")
            '        Me._Vcon.SetErrorControl(.txtLotNo)
            '        Return False
            '    End If
            'End If
            '要望番号:1098 yamanaka 2012.07.05 End

        End With

        Return True

    End Function

    ''' <summary>
    ''' 在庫テーブル参照時、必須項目チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsZaikoPopChk() As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            '荷主コード（大）
    
            '2017/09/25 修正 李↓
            .txtCustCdL.ItemName = lgm.Selector({"荷主コード(大)", "Custmer Code (L)", "하주코드(大)", "中国語"})
            '2017/09/25 修正 李↑

            .txtCustCdL.IsHissuCheck = True
            .txtCustCdL.IsForbiddenWordsCheck = True
            .txtCustCdL.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                Return False
            End If

            '荷主コード（中）
            
            '2017/09/25 修正 李↓
            .txtCustCdM.ItemName = lgm.Selector({"荷主コード(中)", "Custmer Code (M)", "하주코드(中)", "中国語"})
            '2017/09/25 修正 李↑

            .txtCustCdM.IsHissuCheck = True
            .txtCustCdM.IsForbiddenWordsCheck = True
            .txtCustCdM.IsByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                Return False
            End If

            Return True

        End With

    End Function

#End Region

#Region "スプレッド項目チェック"

    ''' <summary>
    ''' 振替確定時の移動先スプレッドの項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSpreadSakiInputChk(ByVal arr As ArrayList) As Boolean

        With Me._Frm

            For i As Integer = 0 To arr.Count - 1

                '******************** 振替先Spread部の入力チェック ********************
                Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDtlNew)
                Dim whCd As String = .cmbSoko.SelectedValue.ToString()
                Dim sokoDrs As DataRow() = Me._Vcon.SelectSokoListDataRow(whCd)
                'キャッシュから値取得できた場合 True
                Dim Chk As Boolean = 0 < sokoDrs.Length
                'START YANAI 要望番号433
                Dim Chk2 As Boolean = 0 < sokoDrs.Length
                'END YANAI 要望番号433

                '倉庫マスタの「ロケーション管理の場合 True
                'START YANAI 要望番号433
                'Chk = Chk AndAlso LMConst.FLG.ON.Equals(sokoDrs(0).Item("LOC_MANAGER_YN").ToString())
                If Chk = True Then
                    If ("01").Equals(sokoDrs(0).Item("LOC_MANAGER_YN").ToString()) Then
                        Chk = True
                        Chk2 = True
                    ElseIf ("02").Equals(sokoDrs(0).Item("LOC_MANAGER_YN").ToString()) Then
                        Chk = True
                        Chk2 = False
                    Else
                        Chk = False
                        Chk2 = False
                    End If
                End If
                'END YANAI 要望番号433

                '棟
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_TOU_NO.ColNo)
                '20151030 tsunehira add Start
                '英語化対応
                vCell.ItemName = LMD010G.sprDetailNewDef.SAKI_TOU_NO.ColName
                '20151030 tsunehira add End
                'vCell.ItemName = "棟"
                vCell.IsHissuCheck = Chk
                vCell.IsForbiddenWordsCheck = True
                vCell.IsByteCheck = 2
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                '室
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_SITU_NO.ColNo)
                '20151030 tsunehira add Start
                '英語化対応
                vCell.ItemName = LMD010G.sprDetailNewDef.SAKI_SITU_NO.ColName
                '20151030 tsunehira add End
                'vCell.ItemName = "室"
                vCell.IsHissuCheck = Chk
                vCell.IsForbiddenWordsCheck = True
                'START YANAI 要望番号705
                'vCell.IsByteCheck = 1
                'START S_KOBA 要望番号705
                'vCell.IsFullByteCheck = 2
                vCell.IsByteCheck = 2
                'END S_KOBA 要望番号705
                'END YANAI 要望番号705
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                'ZONE
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_ZONE_CD.ColNo)
                '20151030 tsunehira add Start
                '英語化対応
                vCell.ItemName = LMD010G.sprDetailNewDef.SAKI_ZONE_CD.ColName
                '20151030 tsunehira add End
                'vCell.ItemName = "ZONE"
                'START YANAI 要望番号433
                'vCell.IsHissuCheck = Chk
                vCell.IsHissuCheck = Chk2
                'END YANAI 要望番号433
                vCell.IsForbiddenWordsCheck = True
                'START YANAI 要望番号705
                'vCell.IsByteCheck = 1
                'START S_KOBA 要望番号705
                'vCell.IsFullByteCheck = 2
                vCell.IsByteCheck = 2
                'END S_KOBA 要望番号705
                'END YANAI 要望番号705
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                'ロケーション
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_LOCA.ColNo)
                '20151030 tsunehira add Start
                '英語化対応
                vCell.ItemName = LMD010G.sprDetailNewDef.SAKI_LOCA.ColName
                '20151030 tsunehira add End
                'vCell.ItemName = "ロケーション"
                vCell.IsForbiddenWordsCheck = True
                vCell.IsByteCheck = 10
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If


                'ロットNo
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_LOT_NO.ColNo)
                '20151030 tsunehira add Start
                '英語化対応
                vCell.ItemName = LMD010G.sprDetailNewDef.SAKI_LOT_NO.ColName
                '20151030 tsunehira add End
                'vCell.ItemName = "ロット№"
                vCell.IsForbiddenWordsCheck = True
                vCell.IsByteCheck = 40
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                '備考小(社内)
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_REMARK.ColNo)
                '20151030 tsunehira add Start
                '英語化対応
                vCell.ItemName = LMD010G.sprDetailNewDef.SAKI_REMARK.ColName
                '20151030 tsunehira add End
                'vCell.ItemName = "備考小(社内)"
                vCell.IsForbiddenWordsCheck = True
                vCell.IsByteCheck = 100
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                '賞味期限
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_LT_DATE.ColNo)
                '20151030 tsunehira add Start
                '英語化対応
                vCell.ItemName = LMD010G.sprDetailNewDef.SAKI_LT_DATE.ColName
                '20151030 tsunehira add End
                'vCell.ItemName = "賞味期限"
                vCell.IsFullByteCheck = 10
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                '備考(社外)
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_REMARK_OUT.ColNo)
                '20151030 tsunehira add Start
                '英語化対応
                vCell.ItemName = LMD010G.sprDetailNewDef.SAKI_REMARK_OUT.ColName
                '20151030 tsunehira add End
                'vCell.ItemName = "備考(社外)"
                vCell.IsForbiddenWordsCheck = True
                vCell.IsByteCheck = 15
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                'シリアルNo
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_SERIAL_NO.ColNo)
                '20151030 tsunehira add Start
                '英語化対応
                vCell.ItemName = LMD010G.sprDetailNewDef.SAKI_SERIAL_NO.ColName
                '20151030 tsunehira add End
                'vCell.ItemName = "シリアル№"
                vCell.IsForbiddenWordsCheck = True
                vCell.IsByteCheck = 40
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If


                '製造日
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_GOODS_CRT_DATE.ColNo)
                '20151030 tsunehira add Start
                '英語化対応
                vCell.ItemName = LMD010G.sprDetailNewDef.SAKI_GOODS_CRT_DATE.ColName
                '20151030 tsunehira add End
                'vCell.ItemName = "製造日"
                vCell.IsForbiddenWordsCheck = True
                vCell.IsFullByteCheck = 10
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                '届先コード
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_DEST_CD.ColNo)
                '20151030 tsunehira add Start
                '英語化対応
                vCell.ItemName = LMD010G.sprDetailNewDef.SAKI_DEST_CD.ColName
                '20151030 tsunehira add End
                'vCell.ItemName = "届先コード"
                vCell.IsForbiddenWordsCheck = True
                vCell.IsByteCheck = 15
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 振替先行追加押下時チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAddFurikaesakiChk(ByVal arrNew As ArrayList, ByVal arrOld As ArrayList) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            '【振替元すぷろっど選択チェック】
            Dim list As ArrayList = Me._Vcon.SprSelectList(LMD010C.SprColumnIndexSprDtl.DEF, .sprDtlNew)
            Dim chkCnt As Integer = list.Count()

            'チェック件数が1件以上
            If chkCnt > 1 Then
                MyBase.ShowMessage("E272")
                Return False
            End If

            'チェック件数が0件以上
            If chkCnt = 0 Then
                Return True
            End If

            '【商品コードチェック】(容器変更=有の場合のみ)
            If Me._Frm.chkYoukiChange.GetBinaryValue().Equals(LMConst.FLG.ON) Then
                '商品コード
              
                '2017/09/25 修正 李↓
                .txtGoodsCdCustNew.ItemName = lgm.Selector({"商品コード", "Goods Code", "상품코드", "中国語"})
                '2017/09/25 修正 李↑

                .txtGoodsCdCustNew.IsHissuCheck = True
                .txtGoodsCdCustNew.IsForbiddenWordsCheck = True
                .txtGoodsCdCustNew.IsByteCheck = 20
                If MyBase.IsValidateCheck(.txtGoodsCdCustNew) = False Then
                    Return False
                End If

                '商品名称
                '20151030 tsunehira add Start
                '英語化対応
                .txtGoodsNmCustNew.ItemName = .lblTitleGoodsNew.TextValue
                '20151030 tsunehira add End
                '.txtGoodsNmCustNew.ItemName = "商品名称"
                .txtGoodsNmCustNew.IsForbiddenWordsCheck = True
                .txtGoodsNmCustNew.IsByteCheck = 60
                If MyBase.IsValidateCheck(.txtGoodsNmCustNew) = False Then
                    Return False
                End If

                '商品キー必須チェック
                If String.IsNullOrEmpty(.lblGoodsCdNrsNew.TextValue) Then
                    '20151029 tsunehira add Start
                    '英語化対応
                    Me._Vcon.SetErrMessage("E803")
                    'Me._Vcon.SetErrMessage("E019", New String() {"商品キー"})
                    '20151029 tsunehira add End
                    Me._Vcon.SetErrorControl(.txtGoodsCdCustNew)
                    Return False
                End If

                '商品コードのマスタ存在チェック
                If Me.IsExitsGoodsCheck(LMConst.FLG.ON) = False Then
                    Return False
                End If

                '振替数量チェック
                If Me.totalSuryoChk(arrNew, arrOld) = False Then
                    Return False
                End If

            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 全削除チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAllDeleteChk(ByVal chkCnt As Integer, ByVal sprCnt As Integer) As Boolean

        '容器変更=有の場合は全削除可能
        If Me._Frm.chkYoukiChange.GetBinaryValue().Equals(LMConst.FLG.ON) Then
            Return True
        End If

        '全件削除の場合はエラー
        If chkCnt = sprCnt Then

            MyBase.ShowMessage("E280")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 振替数量チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function totalSuryoChk(ByVal arrNew As ArrayList, ByVal arrOld As ArrayList) As Boolean

        Dim totalSuryoSaki As Decimal = 0
        Dim nowSuryoSaki As Decimal = 0
        Dim totalSuryoMoto As Decimal = 0
        Dim oldMax As Integer = arrOld.Count - 1

        For i As Integer = 0 To oldMax

            '振替元の数量の取得
            totalSuryoMoto = totalSuryoMoto + Convert.ToDecimal(Me._Vcon.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arrOld(i)), LMD010G.sprDetailDef.MOTO_HURIKAE_SURYO.ColNo)).ToString())

        Next

        Dim newMax As Integer = arrNew.Count - 1
        For i As Integer = 0 To newMax

            nowSuryoSaki = Convert.ToDecimal(Me._Vcon.GetCellValue(Me._Frm.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arrNew(i)), LMD010G.sprDetailNewDef.SAKI_HURIKAE_SURYO.ColNo)).ToString())

            '振替先の数量の取得
            totalSuryoSaki = totalSuryoSaki + nowSuryoSaki

        Next

        '振替先の数量がひとつでも0の場合はエラー
        If nowSuryoSaki = 0 Then
            Return Me._Vcon.SetErrMessage("E279")
        End If

        '数量合計を比較して、異なっていた場合ワーニング
        If totalSuryoMoto <> totalSuryoSaki = True Then
            If MyBase.ShowMessage("W154") = MsgBoxResult.Cancel Then
                Return False
            End If
        End If

        Return True

    End Function

#End Region

#Region "大小チェック"

    ''' <summary>
    ''' 入荷日大小チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsLastInKaDateChk(ByVal arr As ArrayList) As Boolean

        Dim newInKaDate As String = String.Empty
        Dim inKaDate As String = String.Empty

        For i As Integer = 0 To arr.Count - 1

            inKaDate = DateFormatUtility.DeleteSlash(Me._Vcon.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_INKA_DATE.ColNo)).ToString())

            If newInKaDate < inKaDate Then
                newInKaDate = inKaDate
            End If

        Next

        If Me._Vcon.IsFromToChk(newInKaDate, Me._Frm.imdFurikaeDate.TextValue) = False Then

            '振替日が過去日の場合エラー
            Me._Frm.txtGoodsCdCust.ReadOnly = False
            Me._Frm.txtGoodsNmCust.ReadOnly = False
            If Me._Frm.imdFurikaeDate.ReadOnly = False Then
                Me._Vcon.SetErrorControl(Me._Frm.imdFurikaeDate)
            End If
            Me._Frm.txtGoodsCdCust.Focus()
            Me._Vcon.SetErrMessage("E387")
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' 移動日大小チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsIdoDateChk(ByVal chkDs As DataSet) As Boolean

        Dim idoDate As String = String.Empty
        Dim dt As DataTable = chkDs.Tables(LMD010C.TABLE_NM_IDO_TRS_OUT)
        Dim dr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1

        If max < 0 Then
            Return True
        End If

        For i As Integer = 0 To max

            dr = dt.Rows(i)

            idoDate = dr.Item("IDO_DATE").ToString()

            '振替日が過去日の場合エラー
            If Me._Vcon.IsFromToChk(idoDate, Me._Frm.imdFurikaeDate.TextValue) = False Then
                If Me._Frm.imdFurikaeDate.ReadOnly = False Then
                    Me._Vcon.SetErrorControl(Me._Frm.imdFurikaeDate)
                End If
                'Me._Vcon.SetErrMessage("E039", New String() {"振替日", "移動日"})
                '20151029 tsunehira add Start
                '英語化対応
                Me._Vcon.SetErrMessage("E804")
                '20151029 tsunehira add End
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 請求日(作業)大小チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSeiqDateChk(ByVal chkDs As DataSet, ByVal motosakiFlg As String) As Boolean

        Dim outDtSAGYO As DataTable = chkDs.Tables("LMD010_SAGYO_SKYU_DATE")
        Dim sagyoDt As DataTable = Nothing
        If (LMConst.FLG.OFF).Equals(motosakiFlg) = True Then
            sagyoDt = chkDs.Tables(LMD010C.TABLE_NM_SAGYO_OUTKA)
        Else
            sagyoDt = chkDs.Tables(LMD010C.TABLE_NM_SAGYO_INKA)
        End If

        If 0 < outDtSAGYO.Rows.Count AndAlso 0 < sagyoDt.Rows.Count Then
            'START YANAI No.44
            'If Me._Vcon.IsFromToChk(outDtSAGYO.Rows(0).Item("SKYU_DATE").ToString(), Me._Frm.imdFurikaeDate.TextValue) = False Then
            If Me._Vcon.IsFromToChk2(outDtSAGYO.Rows(0).Item("SKYU_DATE").ToString(), Me._Frm.imdFurikaeDate.TextValue) = False Then
                'END YANAI No.44
                If Me._Frm.imdFurikaeDate.ReadOnly = False Then
                    Me._Vcon.SetErrorControl(Me._Frm.imdFurikaeDate)
                End If
                'Me._Vcon.SetErrMessage("E285", New String() {"作業料"})
                '20151029 tsunehira add Start
                '英語化対応
                Me._Vcon.SetErrMessage("E805")
                '20151029 tsunehira add End
                Return False
            End If
        End If

        Return True

    End Function

#End Region

#Region "マスタ存在チェック"

    ''' <summary>
    ''' 商品マスタ存在チェック
    ''' </summary>
    ''' <param name="motosakiFlg">振替元時⇒"0" 振替先時⇒"1"</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsExitsGoodsCheck(ByVal motosakiFlg As String) As Boolean

        With Me._Frm

            '商品コード存在チェック
            Dim goods As String = String.Empty
            Dim goodsNm As String = String.Empty
            Dim custL As String = String.Empty
            Dim custM As String = String.Empty
            Dim goodsCustNrs As String = String.Empty
            Dim chkStr As String = String.Empty

            If LMConst.FLG.OFF.Equals(motosakiFlg) Then
                '振替元の場合
                custL = .txtCustCdL.TextValue
                custM = .txtCustCdM.TextValue
                goodsCustNrs = .lblGoodsCdNrs.TextValue
                goods = .txtGoodsCdCust.TextValue
                goodsNm = .txtGoodsNmCust.TextValue
                chkStr = goods
            Else
                '振替先の場合
                goodsCustNrs = .lblGoodsCdNrsNew.TextValue
                chkStr = goodsCustNrs

            End If

            Dim drGoods As DataRow() = Nothing

            '商品KEYよりキャッシュテーブルにアクセスする
            If String.IsNullOrEmpty(chkStr) = False Then

                '商品マスタより、荷主コードを取得
                If LMConst.FLG.OFF.Equals(motosakiFlg) = True Then
                    'キャッシュテーブルより検索結果を取得
                    'START YANAI 要望番号610
                    'drGoods = Me._Vcon.SelectgoodsListDataRow(.cmbNrsBrCd.SelectedValue.ToString(), goods, custL, custM, LMConst.FLG.OFF)
                    If String.IsNullOrEmpty(goodsCustNrs) = True Then
                        drGoods = Me._Vcon.SelectgoodsListDataRow(.cmbNrsBrCd.SelectedValue.ToString(), goods, custL, custM, LMConst.FLG.OFF)
                    Else
                        '---↓
                        'drGoods = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(String.Concat("GOODS_CD_NRS = ", " '", goodsCustNrs, "' "))

                        Dim goodsDs As MGoodsDS = New MGoodsDS
                        Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
                        goodsDr.Item("GOODS_CD_NRS") = goodsCustNrs
                        goodsDr.Item("SYS_DEL_FLG") = "0"    '要望番号1604 2012/11/16 本明追加
#If True Then   'ADD 2023/01/17 035090   【LMS】住友ファーマ　②その他機能でも使用している①と同ソース修正
                        goodsDr.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
#End If
                        goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
                        Dim rtnDs As DataSet = MyBase.GetGoodsMasterData(goodsDs)
                        drGoods = rtnDs.Tables(LMConst.CacheTBL.GOODS).Select
                        '---↑

                    End If
                    'END YANAI 要望番号610
                Else
                    '---↓
                    'drGoods = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(String.Concat("GOODS_CD_NRS = ", " '", goodsCustNrs, "' "))

                    Dim goodsDs As MGoodsDS = New MGoodsDS
                    Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
                    goodsDr.Item("GOODS_CD_NRS") = goodsCustNrs
                    goodsDr.Item("SYS_DEL_FLG") = "0"    '要望番号1604 2012/11/16 本明追加
#If True Then   'ADD 2023/01/17 035090   【LMS】住友ファーマ　②その他機能でも使用している①と同ソース修正
                    goodsDr.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
#End If
                    goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
                    Dim rtnDs As DataSet = MyBase.GetGoodsMasterData(goodsDs)
                    drGoods = rtnDs.Tables(LMConst.CacheTBL.GOODS).Select
                    '---↑

                End If

                If drGoods.Length < 1 Then

                    If LMConst.FLG.OFF.Equals(motosakiFlg) = True Then
                        '2015.10.22 tusnehira add
                        '英語化対応
                        MyBase.ShowMessage("E769", New String() {String.Concat(goods, " ", goodsNm)})
                        'MyBase.ShowMessage("E079", New String() {"商品マスタ", String.Concat(goods, " ", goodsNm)})
                        Me._Vcon.SetErrorControl(New Control() {.txtGoodsCdCust, .txtGoodsNmCust}, .txtGoodsNmCust)
                    Else
                        '2015.10.22 tusnehira add
                        '英語化対応
                        MyBase.ShowMessage("E769", New String() {goodsCustNrs})
                        'MyBase.ShowMessage("E079", New String() {"商品マスタ", goodsCustNrs})
                        Me._Vcon.SetErrorControl(.txtGoodsCdCustNew)
                    End If

                    Return False

                Else

                    If LMConst.FLG.OFF.Equals(motosakiFlg) Then
                        '振替元の場合
                        .txtGoodsCdCust.TextValue = drGoods(0).Item("GOODS_CD_CUST").ToString()
                        .txtGoodsNmCust.TextValue = drGoods(0).Item("GOODS_NM_1").ToString()
                        .lblGoodsCdNrs.TextValue = drGoods(0).Item("GOODS_CD_NRS").ToString()
                        'START YANAI メモNo.100
                        '.numIrime.Value = drGoods(0).Item("STD_IRIME_NB").ToString()
                        'END YANAI メモNo.100
#If False Then '区分タイトルラベル対応 Changed 20151119 INOUE
                        .lblIrimeTanniKB.TextValue = drGoods(0).Item("STD_IRIME_UT").ToString()
#Else
                        .lblIrimeTanni.KbnValue = drGoods(0).Item("STD_IRIME_UT").ToString()
#End If
                    Else
                        '振替先の場合
                        .txtGoodsCdCustNew.TextValue = drGoods(0).Item("GOODS_CD_CUST").ToString()
                        .txtGoodsNmCustNew.TextValue = drGoods(0).Item("GOODS_NM_1").ToString()
                        .lblGoodsCdNrsNew.TextValue = drGoods(0).Item("GOODS_CD_NRS").ToString()
                        .numIrimeNew.Value = drGoods(0).Item("STD_IRIME_NB").ToString()
#If False Then '区分タイトルラベル対応 Changed 20151119 INOUE
                        .lblIrimeTanniNew.TextValue = drGoods(0).Item("STD_IRIME_UT").ToString()
#Else
                        .lblIrimeTanniNew.KbnValue = drGoods(0).Item("STD_IRIME_UT").ToString()
#End If
                    End If

                End If

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 荷主マスタ存在チェック
    ''' </summary>
    ''' <param name="motosakiFlg">振替元時⇒"0" 振替先時⇒"1"</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsExitsCustCheck(ByVal motosakiFlg As String) As Boolean

        With Me._Frm

            '荷主コード(大)存在チェック

            Dim defaultkb As String = "00"
            Dim drs As DataRow() = Nothing

            Dim custL As String = String.Empty
            Dim custM As String = String.Empty
            Dim custNmL As String = String.Empty
            Dim custNmM As String = String.Empty

            If LMConst.FLG.OFF.Equals(motosakiFlg) Then
                '振替元の場合
                custL = .txtCustCdL.TextValue
                custM = .txtCustCdM.TextValue
                custNmL = .lblCustNmL.TextValue
                custNmM = .lblCustNmM.TextValue
            Else
                '振替先の場合
                custL = .txtCustCdLNew.TextValue
                custM = .txtCustCdMNew.TextValue
                custNmL = .lblCustNmLNew.TextValue
                custNmM = .lblCustNmMNew.TextValue
            End If

            '荷主コード(大)が存在する場合
            If String.IsNullOrEmpty(custL) = False Then

                drs = Me._Vcon.SelectCustListDataRow(custL, custM, defaultkb, defaultkb)

                '荷主コード(中)が存在する場合
                If String.IsNullOrEmpty(custM) = False Then
                    If drs.Length < 1 Then
                        '2015.10.22 tusnehira add
                        '英語化対応
                        MyBase.ShowMessage("E767", New String() {String.Concat(custL, " ", custM)})
                        'MyBase.ShowMessage("E079", New String() {"荷主マスタ", String.Concat(custL, " ", custM)})
                        If LMConst.FLG.OFF.Equals(motosakiFlg) = True Then
                            Me._Vcon.SetErrorControl(.txtCustCdM)
                            Me._Vcon.SetErrorControl(.txtCustCdL)
                        Else
                            Me._Vcon.SetErrorControl(.txtCustCdMNew)
                            Me._Vcon.SetErrorControl(.txtCustCdLNew)
                        End If

                        Return False
                    End If

                    custNmL = drs(0).Item("CUST_NM_L").ToString()
                    custNmM = drs(0).Item("CUST_NM_M").ToString()

                Else
                    '荷主コード(中)が存在しない場合はエラーを戻す
                    '2015.10.22 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E766")
                    'MyBase.ShowMessage("E017", New String() {"荷主コード(大)", "荷主コード(中)"})
                    If LMConst.FLG.OFF.Equals(motosakiFlg) = True Then
                        Me._Vcon.SetErrorControl(.txtCustCdM)
                        Me._Vcon.SetErrorControl(.txtCustCdL)
                    Else
                        Me._Vcon.SetErrorControl(.txtCustCdMNew)
                        Me._Vcon.SetErrorControl(.txtCustCdLNew)
                    End If
                    Return False
                End If

                '荷主コード(大)が存在しない場合
            Else
                '荷主コード(中)が存在する場合はエラーを戻す
                If String.IsNullOrEmpty(custM) = False Then
                    '2015.10.22 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E766")
                    'MyBase.ShowMessage("E017", New String() {"荷主コード(大)", "荷主コード(中)"})
                    If LMConst.FLG.OFF.Equals(motosakiFlg) = True Then
                        Me._Vcon.SetErrorControl(.txtCustCdM)
                        Me._Vcon.SetErrorControl(.txtCustCdL)
                    Else
                        Me._Vcon.SetErrorControl(.txtCustCdMNew)
                        Me._Vcon.SetErrorControl(.txtCustCdLNew)
                    End If
                    Return False
                End If

            End If

            '荷主名設定
            If LMConst.FLG.OFF.Equals(motosakiFlg) Then
                '振替元の場合
                .lblCustNmL.TextValue = custNmL
                .lblCustNmM.TextValue = custNmM
            Else
                '振替先の場合
                .lblCustNmLNew.TextValue = custNmL
                .lblCustNmMNew.TextValue = custNmM
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 作業項目マスタ存在チェック
    ''' </summary>
    ''' <param name="motosakiFlg">振替元時⇒"0" 振替先時⇒"1"</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsExitsSagyoCheck(ByVal motosakiFlg As String) As Boolean

        With Me._Frm

            Dim work1 As String = String.Empty
            Dim work2 As String = String.Empty
            Dim work3 As String = String.Empty
            Dim workNm1 As String = String.Empty
            Dim workNm2 As String = String.Empty
            Dim workNm3 As String = String.Empty
            Dim nrsBrCd As String = String.Empty
            Dim custCdL As String = String.Empty
            Dim drs As DataRow() = Nothing

            nrsBrCd = Convert.ToString(.cmbNrsBrCd.SelectedValue)
            If LMConst.FLG.OFF.Equals(motosakiFlg) = True Then
                '振替元の場合
                work1 = .txtSagyoCdO1.TextValue
                work2 = .txtSagyoCdO2.TextValue
                work3 = .txtSagyoCdO3.TextValue
                custCdL = .txtCustCdL.TextValue
            Else
                '振替先の場合
                work1 = .txtSagyoCdN1.TextValue
                work2 = .txtSagyoCdN2.TextValue
                work3 = .txtSagyoCdN3.TextValue
                custCdL = .txtCustCdLNew.TextValue
            End If


            '作業項目区分1マスタ存在チェック
            If String.IsNullOrEmpty(work1) = False Then

                'START YANAI 要望番号376
                'drs = Me._Vcon.SelectSagyoListDataRow(nrsBrCd, work1, custCdL)
                Dim SelectSagyoString As String = String.Empty
                '削除フラグ
                SelectSagyoString = String.Concat(SelectSagyoString, " SYS_DEL_FLG = '0' ")
                '作業コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND SAGYO_CD = '", work1, "' ")
                '営業所コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND NRS_BR_CD = '", nrsBrCd, "' ")
                '荷主コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND (CUST_CD_L = '", custCdL, "' OR CUST_CD_L = 'ZZZZZ')")

                drs = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(SelectSagyoString)
                'END YANAI 要望番号376
                If drs.Length < 1 Then
                    '2015.10.22 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E768", New String() {work1})
                    'MyBase.ShowMessage("E079", New String() {"作業項目マスタ", work1})

                    If LMConst.FLG.OFF.Equals(motosakiFlg) = True Then
                        Me._Vcon.SetErrorControl(.txtSagyoCdO1)
                    Else
                        Me._Vcon.SetErrorControl(.txtSagyoCdN1)
                    End If

                    Return False

                End If

                '画面項目にセット
                If LMConst.FLG.OFF.Equals(motosakiFlg) = True Then
                    .lblSagyoNmO1.TextValue = drs(0).Item("SAGYO_RYAK").ToString()
                    .lblSagyoInNmO1.TextValue = drs(0).Item("SAGYO_NM").ToString()
                Else
                    .lblSagyoNmN1.TextValue = drs(0).Item("SAGYO_RYAK").ToString()
                    .lblSagyoInNmN1.TextValue = drs(0).Item("SAGYO_NM").ToString()
                End If

            End If

            '作業料区分2マスタ存在チェック
            If String.IsNullOrEmpty(work2) = False Then

                'START YANAI 要望番号376
                'drs = Me._Vcon.SelectSagyoListDataRow(nrsBrCd, work2, custCdL)
                Dim SelectSagyoString As String = String.Empty
                '削除フラグ
                SelectSagyoString = String.Concat(SelectSagyoString, " SYS_DEL_FLG = '0' ")
                '作業コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND SAGYO_CD = '", work2, "' ")
                '営業所コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND NRS_BR_CD = '", nrsBrCd, "' ")
                '荷主コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND (CUST_CD_L = '", custCdL, "' OR CUST_CD_L = 'ZZZZZ')")

                drs = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(SelectSagyoString)
                'END YANAI 要望番号376
                If drs.Length < 1 Then
                    '2015.10.22 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E768", New String() {work2})
                    'MyBase.ShowMessage("E079", New String() {"作業項目マスタ", work2})

                    If LMConst.FLG.OFF.Equals(motosakiFlg) = True Then
                        Me._Vcon.SetErrorControl(.txtSagyoCdO2)
                    Else
                        Me._Vcon.SetErrorControl(.txtSagyoCdN2)
                    End If

                    Return False
                End If

                '画面項目にセット
                If LMConst.FLG.OFF.Equals(motosakiFlg) = True Then
                    .lblSagyoNmO2.TextValue = drs(0).Item("SAGYO_RYAK").ToString()
                    .lblSagyoInNmO2.TextValue = drs(0).Item("SAGYO_NM").ToString()
                Else
                    .lblSagyoNmN2.TextValue = drs(0).Item("SAGYO_RYAK").ToString()
                    .lblSagyoInNmN2.TextValue = drs(0).Item("SAGYO_NM").ToString()
                End If


            End If

            '作業料区分3マスタ存在チェック
            If String.IsNullOrEmpty(work3) = False Then

                'START YANAI 要望番号376
                'drs = Me._Vcon.SelectSagyoListDataRow(nrsBrCd, work3, custCdL)
                Dim SelectSagyoString As String = String.Empty
                '削除フラグ
                SelectSagyoString = String.Concat(SelectSagyoString, " SYS_DEL_FLG = '0' ")
                '作業コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND SAGYO_CD = '", work3, "' ")
                '営業所コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND NRS_BR_CD = '", nrsBrCd, "' ")
                '荷主コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND (CUST_CD_L = '", custCdL, "' OR CUST_CD_L = 'ZZZZZ')")

                drs = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(SelectSagyoString)
                'END YANAI 要望番号376
                If drs.Length < 1 Then

                    '2015.10.22 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E768", New String() {work3})
                    'MyBase.ShowMessage("E079", New String() {"作業項目マスタ", work3})

                    If LMConst.FLG.OFF.Equals(motosakiFlg) = True Then
                        Me._Vcon.SetErrorControl(.txtSagyoCdO3)
                    Else
                        Me._Vcon.SetErrorControl(.txtSagyoCdN3)
                    End If

                    Return False

                End If

                '画面項目にセット
                If LMConst.FLG.OFF.Equals(motosakiFlg) = True Then
                    .lblSagyoNmO3.TextValue = drs(0).Item("SAGYO_RYAK").ToString()
                    .lblSagyoInNmO3.TextValue = drs(0).Item("SAGYO_NM").ToString()
                Else
                    .lblSagyoNmN3.TextValue = drs(0).Item("SAGYO_RYAK").ToString()
                    .lblSagyoInNmN3.TextValue = drs(0).Item("SAGYO_NM").ToString()
                End If

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 棟・室・ゾーンマスタ存在チェック
    ''' </summary>
    ''' <param name="motosakiFlg">振替元時⇒"0" 振替先時⇒"1"</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsExitsTouSituZoneCheck(ByVal motosakiFlg As String) As Boolean

        With Me._Frm
            Dim touDr As DataRow() = Nothing
            Dim max As Integer = .sprDtlNew.ActiveSheet.Rows.Count - 1
            Dim touNo As String = String.Empty
            Dim situNo As String = String.Empty
            Dim zoneCd As String = String.Empty

            For i As Integer = 0 To max
                touNo = Me._Vcon.GetCellValue(.sprDtlNew.ActiveSheet.Cells(i, LMD010G.sprDetailNewDef.SAKI_TOU_NO.ColNo))
                situNo = Me._Vcon.GetCellValue(.sprDtlNew.ActiveSheet.Cells(i, LMD010G.sprDetailNewDef.SAKI_SITU_NO.ColNo))
                zoneCd = Me._Vcon.GetCellValue(.sprDtlNew.ActiveSheet.Cells(i, LMD010G.sprDetailNewDef.SAKI_ZONE_CD.ColNo))

                If String.IsNullOrEmpty(touNo) = False AndAlso _
                    String.IsNullOrEmpty(situNo) = False AndAlso _
                    String.IsNullOrEmpty(zoneCd) = False Then
                    touDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TOU_SITU_ZONE).Select(String.Concat("WH_CD = '", Convert.ToString(.cmbSoko.SelectedValue), "' AND ", _
                                                                                                             "TOU_NO = '", touNo, "' AND ", _
                                                                                                             "SITU_NO = '", situNo, "' AND ", _
                                                                                                             "ZONE_CD = '", zoneCd, "'"))

                    If 0 = touDr.Length Then
                        Me._Vcon.SetErrorControl(.sprDtlNew, i, LMD010G.sprDetailNewDef.SAKI_TOU_NO.ColNo)
                        Me._Vcon.SetErrorControl(.sprDtlNew, i, LMD010G.sprDetailNewDef.SAKI_SITU_NO.ColNo)
                        Me._Vcon.SetErrorControl(.sprDtlNew, i, LMD010G.sprDetailNewDef.SAKI_ZONE_CD.ColNo)

                        '2015.10.22 tusnehira add
                        '英語化対応
                        MyBase.ShowMessage("E770", New String() {touNo, situNo, zoneCd})
                        'MyBase.ShowMessage("E079", New String() {"棟室ゾーンマスタ", String.Concat("棟番号:", touNo, " 室番号:", situNo, " ゾーン:", zoneCd)})
                        Return False
                    End If

                End If
            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 作業コードの重複チェック
    ''' </summary>
    ''' <param name="type">タイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSagyoJufukuChk(ByVal type As LMD010C.SagyoData) As Boolean

        With Me._Frm

            Dim ctl As Win.InputMan.LMImTextBox = Nothing
            Dim ctl2 As Win.InputMan.LMImTextBox = Nothing
            Dim str As String = String.Concat(LMD010C.SAGYO_CD, type.ToString())
            Dim max As Integer = LMD010C.SAGYO_MAX_REC - 1
            Dim value As String = String.Empty
            Dim sagyoCd As String = String.Empty
            For i As Integer = 0 To max

                ctl = Me.GetTextControl(String.Concat(str, (i + 1).ToString()))
                sagyoCd = ctl.TextValue

                '作業コードがない場合、スルー
                If String.IsNullOrEmpty(sagyoCd) = True Then
                    Continue For
                End If

                value = String.Empty
                For j As Integer = i + 1 To max

                    ctl2 = Me.GetTextControl(String.Concat(str, (j + 1).ToString()))
                    value = ctl2.TextValue

                    '比較対象データが空文字 且つ 作業コードが設定済みの場合、値設定

                    '比較対象データと値が同じ場合、エラー
                    If value.Equals(sagyoCd) = True Then
                        Me._Vcon.SetErrorControl(ctl)
                        Me._Vcon.SetErrorControl(ctl2)
                        Return Me._Vcon.SetErrMessage("E131")
                    End If

                Next

            Next

            Return True

        End With

    End Function

#End Region

#Region "部品化検討"

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMD010C.ActionType) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        ''フォーカス位置がない場合、スルー
        'If String.IsNullOrEmpty(objNm) = True Then
        '    Return False
        'End If

        Dim ctl1 As Win.InputMan.LMImTextBox = Nothing
        Dim ctl2 As Win.InputMan.LMImTextBox = Nothing
        Dim msg1 As String = String.Empty
        Dim msg2 As String = String.Empty

        With Me._Frm

            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    '2017/09/25 修正 李↓
                    ctl1 = .txtCustCdL
                    msg1 = lgm.Selector({"荷主(大)コード", "Cust Code (L)", "하주(大)코드", "中国語"})
                    ctl2 = .txtCustCdM
                    msg2 = lgm.Selector({"荷主(中)コード", "Cust Code (M)", "하주(中)코드", "中国語"})
                    '2017/09/25 修正 李↑

                    'コードが空なら名称を消す
                    If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True _
                    And String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                        .lblCustNmL.TextValue = String.Empty
                        .lblCustNmM.TextValue = String.Empty
                    End If

                Case .txtCustCdLNew.Name, .txtCustCdMNew.Name

                    '2017/09/25 修正 李↓
                    ctl1 = .txtCustCdLNew
                    msg1 = lgm.Selector({"荷主(大)コード", "Cust Code (L)", "하주(大)코드", "中国語"})
                    ctl2 = .txtCustCdMNew
                    msg2 = lgm.Selector({"荷主(中)コード", "Cust Code (M)", "하주(中)코드", "中国語"})
                    '2017/09/25 修正 李↑

                    'コードが空なら名称を消す
                    If String.IsNullOrEmpty(.txtCustCdLNew.TextValue) = True _
                    And String.IsNullOrEmpty(.txtCustCdMNew.TextValue) = True Then
                        .lblCustNmLNew.TextValue = String.Empty
                        .lblCustNmMNew.TextValue = String.Empty
                    End If

                Case .txtGoodsCdCust.Name, .txtGoodsNmCust.Name

                    '2017/09/25 修正 李↓
                    ctl1 = .txtGoodsCdCust
                    msg1 = lgm.Selector({"商品コード", "Goods Code", "상품코드", "中国語"})
                    ctl2 = .txtGoodsNmCust
                    msg2 = lgm.Selector({"商品名称", "Goods Name", "상품명칭", "中国語"})
                    '2017/09/25 修正 李↑

                Case .txtGoodsCdCustNew.Name, .txtGoodsNmCustNew.Name

                    '2017/09/25 修正 李↓
                    ctl1 = .txtGoodsCdCustNew
                    msg1 = lgm.Selector({"商品コード", "Goods Code", "상품코드", "中国語"})
                    ctl2 = .txtGoodsNmCustNew
                    msg2 = lgm.Selector({"商品名称", "Goods Name", "상품명칭", "中国語"})
                    '2017/09/25 修正 李↑

                Case .sprDtlNew.Name

                    Return True

            End Select

            '作業コードの場合のチェック
            If String.IsNullOrEmpty(objNm) = False Then
                Select Case objNm.Substring(0, objNm.Length - 2)

                    Case LMD010C.SAGYO_CD

                        ctl1 = DirectCast(Me._Frm.Controls.Find(objNm, True)(0), Win.InputMan.LMImTextBox)
                      
                        '2017/09/25 修正 李↓
                        msg1 = lgm.Selector({"作業コード", "Work Code", "작업코드", "中国語"})
                        '2017/09/25 修正 李↑

                        If String.IsNullOrEmpty(ctl1.TextValue) = True Then
                            Dim arrCtlNm As ArrayList = Me.GetSagyoCtlNm(objNm)
                            Me.GetTextControl(arrCtlNm(1).ToString()).TextValue = String.Empty
                            Me.GetTextControl(arrCtlNm(2).ToString()).TextValue = String.Empty

                        End If

                End Select
            End If

            'Nothing判定用
            Dim ctlChk As Boolean = ctl2 Is Nothing

            'フォーカス位置が参照可能でない場合、エラー
            If (ctl1 Is Nothing = True OrElse ctl1.ReadOnly = True) _
                OrElse (ctlChk = False AndAlso ctl2.ReadOnly = True) Then

                Select Case actionType

                    Case LMD010C.ActionType.MASTER

                        Return _Vcon.SetFocusErrMessage()

                    Case LMD010C.ActionType.ENTER

                        'Enterの場合はメッセージは設定しない
                        Return False

                End Select


            End If

            '禁止文字チェック(1つ目のコントロール)
            ctl1.ItemName = msg1
            ctl1.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(ctl1) = False Then
                Return False
            End If

            '禁止文字チェック(2つ目のコントロール)
            If ctlChk = False Then
                ctl2.ItemName = msg2
                ctl2.IsForbiddenWordsCheck = True
                If MyBase.IsValidateCheck(ctl2) = False Then
                    Return False
                End If
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        'ヘッダ項目のスペース除去
        Call Me.TrimHeaderSpaceTextValue()

        '振替元スプレッドのスペース除去
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.spdDtl)

        '振替先スプレッドのスペース除去
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprDtlNew)

    End Sub

    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimHeaderSpaceTextValue()

        With Me._Frm

            .imdFurikaeDate.TextValue = .imdFurikaeDate.TextValue.Trim()
            .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
            .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()
            .txtOrderNo.TextValue = .txtOrderNo.TextValue.Trim()
            .txtGoodsCdCust.TextValue = .txtGoodsCdCust.TextValue.Trim()
            .txtGoodsNmCust.TextValue = .txtGoodsNmCust.TextValue.Trim()
            .txtLotNo.TextValue = .txtLotNo.TextValue.Trim()
            .txtSerialNo.TextValue = .txtSerialNo.TextValue.Trim()
            .txtSagyoCdO1.TextValue = .txtSagyoCdO1.TextValue.Trim()
            .txtSagyoCdO2.TextValue = .txtSagyoCdO2.TextValue.Trim()
            .txtSagyoCdO3.TextValue = .txtSagyoCdO3.TextValue.Trim()
            .txtSyukkaRemark.TextValue = .txtSyukkaRemark.TextValue.Trim()
            .txtCustCdLNew.TextValue = .txtCustCdLNew.TextValue.Trim()
            .txtCustCdMNew.TextValue = .txtCustCdMNew.TextValue.Trim()
            .txtDenpNo.TextValue = .txtDenpNo.TextValue.Trim()
            .txtGoodsCdCustNew.TextValue = .txtGoodsCdCustNew.TextValue.Trim()
            .txtGoodsNmCustNew.TextValue = .txtGoodsNmCustNew.TextValue.Trim()
            .txtSagyoCdN1.TextValue = .txtSagyoCdN1.TextValue.Trim()
            .txtSagyoCdN2.TextValue = .txtSagyoCdN2.TextValue.Trim()
            .txtSagyoCdN3.TextValue = .txtSagyoCdN3.TextValue.Trim()
            .txtNyukaRemark.TextValue = .txtNyukaRemark.TextValue.Trim()

        End With

    End Sub

    ''' <summary>
    ''' 作業Pop出力前チェック
    ''' </summary>
    ''' <param name="count">作業カウント</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSagyoPopupChk(ByVal count As Integer, ByVal msg As String) As Boolean

        If 0 = count Then
            Return Me._Vcon.SetErrMessage("E242", New String() {msg, "5"})
        End If

        Return True

    End Function

    ''' <summary>
    ''' 計算結果チェック
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="minData">最小値</param>
    ''' <param name="maxData">最大値</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsCalcOver(ByVal value As String _
                               , ByVal minData As String _
                               , ByVal maxData As String _
                               , ByVal msg As String _
                               ) As Boolean

        '桁数オーバーしている場合、エラー
        If Me._Vcon.IsCalcOver(value, minData, maxData) = False Then
            Return Me._Vcon.SetErrMessage("E117", New String() {msg, maxData})
        End If

        Return True

    End Function

    ''' <summary>
    ''' 作業コントロール名のリストを生成
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>コントロール名のリスト</returns>
    ''' <remarks>
    ''' リスト
    ''' ①：隠し(PK)名
    ''' ②：ラベル名
    ''' ③：フラグ名
    ''' ④：隠し(UP_KBN)名
    ''' </remarks>
    Private Function GetSagyoCtlNm(ByVal objNm As String) As ArrayList

        GetSagyoCtlNm = New ArrayList()

        '後ろ2桁を取得
        Dim ctlNm As String = objNm.Substring(objNm.Length - 2, 2)

        '隠し(PK)名を設定
        GetSagyoCtlNm.Add(String.Concat(LMD010C.SAGYO_PK, ctlNm))

        'ラベル名を設定
        GetSagyoCtlNm.Add(String.Concat(LMD010C.SAGYO_NM, ctlNm))

        'Insert用の作業名
        GetSagyoCtlNm.Add(String.Concat(LMD010C.SAGYO_IN_NM, ctlNm))

        '隠し(UP_KBN)名を設定
        GetSagyoCtlNm.Add(String.Concat(LMD010C.SAGYO_UP, ctlNm))

        Return GetSagyoCtlNm

    End Function

    ''' <summary>
    ''' 棟 + 室 + ZONE（置き場情報）温度管理チェック 
    ''' </summary>
    ''' <param name="arr"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsOndoCheck(ByVal arr As ArrayList) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            Dim arrMax As Integer = arr.Count - 1
            Dim checkRow As Integer = 0

            Dim nrsbrcd As String = .cmbNrsBrCd.SelectedValue.ToString
            Dim sokocd As String = .cmbSoko.SelectedValue.ToString
            Dim custcd As String = String.Empty
            'Dim subkb As String = "13"
            'Dim sql As String = String.Empty

            Dim custDtlDr As DataRow() = Nothing

            Dim goodsNRS As String = String.Empty
            Dim goodsDr As DataRow() = Nothing

            '
            Dim ondokbn As String = String.Empty
            Dim ondoStartDate As String = String.Empty
            Dim ondoEndDate As String = String.Empty
            Dim ondoUpper As String = String.Empty
            Dim ondoLower As String = String.Empty

            '
            Dim tousituDr As DataRow() = Nothing
            Dim zoneDr As DataRow() = Nothing

            Dim touNo As String = String.Empty
            Dim situNo As String = String.Empty
            Dim zoneCd As String = String.Empty
            Dim ondoCtlKbn_TouSitu As String = String.Empty
            Dim ondoCtlKbn_Zone As String = String.Empty
            Dim ondo_TouSitu As String = String.Empty
            Dim ondo_Zone As String = String.Empty

            Dim msg As String = String.Empty

            '倉庫マスタチェック
            Dim sokoDrs As DataRow() = Me._Vcon.SelectSokoListDataRow(.cmbSoko.SelectedValue.ToString())
            If sokoDrs.Length < 1 Then Return Me._Vcon.SetMstErrMessage(msg, String.Concat(nrsbrcd, " - ", .cmbSoko.SelectedValue.ToString()))
            If sokoDrs(0).Item("LOC_MANAGER_YN").ToString = "00" Then
                Return True
            End If

            For i As Integer = 0 To arrMax

                checkRow = Convert.ToInt32(arr(i).ToString)

                '
                'custcd = .txtCustCdLNew.TextValue
                'sql = String.Concat("NRS_BR_CD = ", " '", nrsbrcd, "' ", _
                '                                 " AND CUST_CD = ", " '", custcd, "' ", _
                '                                 " AND SUB_KB = ", " '", subkb, "' ")

                ''キャッシュテーブルからデータ抽出
                'custDtlDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(sql)

                'If custDtlDr.Length < 1 Then Continue For

                'If "1".Equals(custDtlDr(0).Item("SET_NAIYO").ToString) = False Then Continue For

                goodsNRS = .lblGoodsCdNrsNew.TextValue
                goodsDr = Me._Vcon.SelectgoodsListDataRow(nrsbrcd, goodsNRS)

                '判定
               
                '2017/09/25 修正 李↓
                msg = lgm.Selector({"商品マスタ", "Product Master", "상품마스터", "中国語"})
                '2017/09/25 修正 李↑

                If goodsDr.Length < 1 Then Return Me._Vcon.SetMstErrMessage(msg, String.Concat(nrsbrcd, " - ", goodsNRS))

                ondokbn = goodsDr(0).Item("ONDO_KB").ToString
                ondoStartDate = goodsDr(0).Item("ONDO_STR_DATE").ToString
                ondoEndDate = goodsDr(0).Item("ONDO_END_DATE").ToString
                ondoUpper = goodsDr(0).Item("ONDO_MX").ToString
                ondoLower = goodsDr(0).Item("ONDO_MM").ToString

                '判定
                If Not "02".Equals(ondokbn) Then Continue For


                Dim idoYYYY As String = Left(.imdFurikaeDate.TextValue, 4)
                Dim startYYYY As String = idoYYYY
                Dim endYYYY As String = idoYYYY
                If ondoStartDate > ondoEndDate Then endYYYY = (Integer.Parse(idoYYYY) + 1).ToString

                '判定
                If Not (String.Concat(startYYYY, ondoStartDate) <= .imdFurikaeDate.TextValue _
                        And String.Concat(endYYYY, ondoEndDate) >= .imdFurikaeDate.TextValue) Then Continue For


                '
                touNo = Me._Vcon.GetCellValue(.sprDtlNew.ActiveSheet.Cells(checkRow, LMD010G.sprDetailNewDef.SAKI_TOU_NO.ColNo)).ToString
                situNo = Me._Vcon.GetCellValue(.sprDtlNew.ActiveSheet.Cells(checkRow, LMD010G.sprDetailNewDef.SAKI_SITU_NO.ColNo)).ToString
                zoneCd = Me._Vcon.GetCellValue(.sprDtlNew.ActiveSheet.Cells(checkRow, LMD010G.sprDetailNewDef.SAKI_ZONE_CD.ColNo)).ToString

                '棟室マスタ
                tousituDr = Me._Vcon.SelectTouSituListDataRow(nrsbrcd, sokocd, touNo, situNo)
                '判定

                '2017/09/25 修正 李↓
                msg = lgm.Selector({"棟室マスタ", "Bulding Master", "동(棟)실(室)마스터", "中国語"})
                '2017/09/25 修正 李↑

                If tousituDr.Length < 1 Then Return Me._Vcon.SetMstErrMessage(msg, String.Concat(nrsbrcd, " - ", sokocd, " - ", touNo, " - ", situNo))
                ondoCtlKbn_TouSitu = tousituDr(0).Item("TOU_ONDO_CTL_KB").ToString
                ondo_TouSitu = tousituDr(0).Item("TOU_ONDO").ToString

                'ゾーンマスタ
                zoneDr = Me._Vcon.SelectZoneListDataRow(nrsbrcd, sokocd, touNo, situNo, zoneCd)
                '判定

                '2017/09/25 修正 李↓
                msg = lgm.Selector({"ゾーンマスタ", "ZONE Master", "존(ZONE)마스터", "中国語"})
                '2017/09/25 修正 李↑

                If zoneDr.Length < 1 Then Return Me._Vcon.SetMstErrMessage(msg, String.Concat(nrsbrcd, " - ", sokocd, " - ", touNo, " - ", situNo, " - ", zoneCd))
                ondoCtlKbn_Zone = zoneDr(0).Item("ZONE_ONDO_CTL_KB").ToString
                ondo_Zone = zoneDr(0).Item("ZONE_ONDO").ToString

                '判定①
                If Not "02".Equals(ondoCtlKbn_TouSitu) And Not "02".Equals(ondoCtlKbn_Zone) Then
                    '2015.10.22 tusnehira add
                    '英語化対応
                    msg = String.Concat(touNo, "-", situNo, "-", zoneCd)
                    If MyBase.ShowMessage("W241", New String() {msg}) = MsgBoxResult.Cancel Then Return False


                    'msg = String.Concat("置場　", touNo, "-", situNo, "-", zoneCd)
                    'If MyBase.ShowMessage("W191", New String() {msg, "定温置場"}) = MsgBoxResult.Cancel Then Return False
                End If

                '判定②
                '　ondoLower <= ondo_TouSitu <= ondoUpper　　　　　　　　　8>15 or 15 >12 ◎　　
                '　ondoLower <= ondo_Zone <= ondoUpper                     8>10 or 10 >12 ◎
                If String.IsNullOrEmpty(ondo_Zone) Then
                    'ゾーンなし
                    If ("02".Equals(ondoCtlKbn_TouSitu) And (Integer.Parse(ondoLower) > Integer.Parse(ondo_TouSitu) Or Integer.Parse(ondo_TouSitu) > Integer.Parse(ondoUpper))) Then
                        msg = String.Concat(touNo, "-", situNo)
                        If MyBase.ShowMessage("W240", New String() {msg}) = MsgBoxResult.Cancel Then Return False
                    End If
                Else
                    'ゾーンあり
                    If ("02".Equals(ondoCtlKbn_TouSitu) And (Integer.Parse(ondoLower) > Integer.Parse(ondo_TouSitu) Or Integer.Parse(ondo_TouSitu) > Integer.Parse(ondoUpper))) _
                    Or ("02".Equals(ondoCtlKbn_Zone) And (Integer.Parse(ondoLower) > Integer.Parse(ondo_Zone) Or Integer.Parse(ondo_Zone) > Integer.Parse(ondoUpper))) Then
                        msg = String.Concat(touNo, "-", situNo, "-", zoneCd)
                        If MyBase.ShowMessage("W240", New String() {msg}) = MsgBoxResult.Cancel Then Return False
                    End If
                End If

            Next

        End With

        Return True

    End Function

#Region "コントロール取得"

    ''' <summary>
    ''' フォームに検索した結果(Text)を取得
    ''' </summary>
    ''' <param name="objNm">コントロール名</param>
    ''' <returns>LMImTextBox</returns>
    ''' <remarks></remarks>
    Private Function GetTextControl(ByVal objNm As String) As Win.InputMan.LMImTextBox

        Return DirectCast(Me._Frm.Controls.Find(objNm, True)(0), Win.InputMan.LMImTextBox)

    End Function

#End Region

#End Region

#End Region

End Class
