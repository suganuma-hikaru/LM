' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB020V : 入荷データ編集
'  作  成  者       :  [iwamoto]
' ==========================================================================
Imports Jp.Co.Nrs.Win.GUI
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMB020Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMB020V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMB020F

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMB020G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMBControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMBControlG

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMB020F, ByVal v As LMBControlV, ByVal gCon As LMBControlG, ByVal g As LMB020G)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

        Me._Gcon = gCon

        Me._G = g

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "Main"

    ''' <summary>
    ''' 入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheck() As Boolean

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '【単項目チェック】
        '入荷(大)単項目チェック
        Dim rtnResult As Boolean = Me.IsInputHeadChk()

        '入荷(中)単項目チェック
        rtnResult = rtnResult AndAlso Me.IsInputGoodsChk()

        '入荷(小)単項目チェック
        rtnResult = rtnResult AndAlso Me.IsInputDetailChk()

        '運送単項目チェック
        rtnResult = rtnResult AndAlso Me.IsUnsoDataChk()

        'マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsMstExistChk()

        '【関連項目チェック】
        'コントロールの関連
        rtnResult = rtnResult AndAlso Me.IsInputConnectionChk()

        'スプレッド項目の関連
        rtnResult = rtnResult AndAlso Me.IsSprDetailChk()

        '入荷日と起算日のワーニングチェック
        rtnResult = rtnResult AndAlso Me.IsInkaDateKisanDateEqualsChk()

        '保管料と荷役料のワーニングチェック
        rtnResult = rtnResult AndAlso Me.IsHokanUmuNiyakuUmuChk()

        'タリフコードの必須チェック(ワーニング)
        rtnResult = rtnResult AndAlso Me.IsTariffHissuChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 入荷(小)全レコードをチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsInkaSZenRecChk(ByVal ds As DataSet) As Boolean

        With Me._Frm

            Dim tariffCd As String = .txtUnsoTariffCD.TextValue

            '横持ちの場合 True
            Dim taniChk As Boolean = LMB020C.TARIFF_YOKO.Equals(.cmbUnchinUmu.SelectedValue.ToString())

            '値がある場合 True
            taniChk = taniChk AndAlso Not String.IsNullOrEmpty(tariffCd)

            Dim yokoDrs As DataRow() = Nothing
            If taniChk = True Then
                yokoDrs = Me._Vcon.SelectYokoTariffListDataRow(.cmbEigyo.SelectedValue.ToString(), tariffCd)
            End If

            'キャッシュから返却がある場合 True
            taniChk = taniChk AndAlso Not yokoDrs Is Nothing

            'キャッシュから取得できた場合 True
            taniChk = taniChk AndAlso 0 < yokoDrs.Length

            '明細分割フラグが'0'の場合 True
            taniChk = taniChk AndAlso LMConst.FLG.ON.Equals(yokoDrs(0).Item("SPLIT_FLG").ToString())

            '計算コード区分が荷姿建ての場合 True
            taniChk = taniChk AndAlso LMB020C.CALC_KB_NISUGATA.Equals(yokoDrs(0).Item("CALC_KB").ToString())

            Dim brCd As String = .cmbEigyo.SelectedValue.ToString()
            Dim whCd As String = .cmbSoko.SelectedValue.ToString()
            Dim sokoDrs As DataRow() = Me._Vcon.SelectSokoListDataRow(whCd)

            'キャッシュから値取得できた場合 True
            Dim hissuChk As Boolean = 0 < sokoDrs.Length

            'ロケーション管理の場合 True
            'START YANAI 要望番号433
            'hissuChk = hissuChk AndAlso ("01").Equals(sokoDrs(0).Item("LOC_MANAGER_YN").ToString())
            Dim hissuChk2 As Boolean = 0 < sokoDrs.Length
            If hissuChk = True Then
                If ("01").Equals(sokoDrs(0).Item("LOC_MANAGER_YN").ToString()) Then
                    hissuChk = True
                    hissuChk2 = True
                ElseIf ("02").Equals(sokoDrs(0).Item("LOC_MANAGER_YN").ToString()) Then
                    hissuChk = True
                    hissuChk2 = False
                Else
                    hissuChk = False
                    hissuChk2 = False
                End If
            End If
            'END YANAI 要望番号433

            Dim dt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_S)
            Dim max As Integer = dt.Rows.Count - 1
            Dim inakMNo As String = String.Empty
            If -1 < max Then

                Dim tani As String = dt.Rows(0).Item("STD_IRIME_UT").ToString()
                For i As Integer = 0 To max

                    '単位混在チェックをするケース 且つ 1行目以外の場合、チェックを行う
                    If taniChk = True AndAlso i <> 0 Then

                        '別の単位がある場合、エラー
                        If Me.IsTaniMixChk(ds, dt.Rows(i), tani) = False Then
                            Return False
                        End If

                    End If

                    'ロケーション管理している場合、チェックを行う
                    'START YANAI 要望番号433
                    'If hissuChk = True Then
                    If hissuChk = True OrElse hissuChk2 = True Then
                        'END YANAI 要望番号433

                        'ロケーション情報の必須チェック
                        'START YANAI 要望番号433
                        'If Me.IsLocationChk(ds, dt.Rows(i), brCd, whCd) = False Then
                        If Me.IsLocationChk(ds, dt.Rows(i), brCd, whCd, hissuChk, hissuChk2) = False Then
                            'END YANAI 要望番号433
                            Return False
                        End If

                    End If

                    '棟・室・ZONE情報の必須チェック
                    If Me.IsTouSituZoneChk(ds, dt.Rows(i), brCd, whCd) = False Then
                        Return False
                    End If

                Next

            End If


            '進捗区分と入荷M/Sの入力チェック
            Dim inkaStateKbn As String = ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0)("INKA_STATE_KB").ToString()
            Dim dr As DataRow = Nothing
            If LMB020C.STATE_YOTEI.Equals(inkaStateKbn) = False Then
                Dim drs As DataRow() = ds.Tables(LMB020C.TABLE_NM_INKA_M).Select("SYS_DEL_FLG='0'")
                Dim mMax As Integer = drs.Count - 1
                If drs.Count = 0 Then
                    Return Me._Vcon.SetErrMessage("E408")
                End If

                Dim inkaNoL As String = drs(0)("INKA_NO_L").ToString()
                Dim inkaNoM As String = String.Empty
                For i As Integer = 0 To drs.Count - 1
                    dr = drs(i)
                    inkaNoM = dr("INKA_NO_M").ToString()
                    '各入荷(中)に入荷(小)が存在しない場合
                    If 0 = ds.Tables(LMB020C.TABLE_NM_INKA_S).Select(String.Concat(" INKA_NO_L = '", inkaNoL, "' AND INKA_NO_M = '", inkaNoM, "' AND SYS_DEL_FLG = '0' ")).Length Then
                        Call Me.SetErrData(ds, dr, LMB020G.sprDetailDef.DEF.ColNo, False)
                        Return Me._Vcon.SetErrMessage("E337")
                    End If

                Next

            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 印刷処理のチェック
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsPrintChk(ByVal dr As DataRow) As Boolean

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        'スペース除去
        Call Me.TrimSpaceTextValue()

        'マスタ名称取得（SBS残作業修正）
        Call Me.GetMstNm()

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '2017/09/25 修正 李↓
            .cmbPrint.ItemName = lgm.Selector({"印刷種別", "Printing type", "인쇄종별", "中国語"})
            '2017/09/25 修正 李↑

            .cmbPrint.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbPrint) = errorFlg Then
                Return errorFlg
            End If

            'ADD 2017/08/04 GHSラベル時ラベル種類未設定はエラー
            If .cmbPrint.SelectedValue.Equals("06") = True Then
                'GHSラベル未選択時はエラー
                If String.IsNullOrEmpty(.cmbLabelTYpe.TextValue) = True Then
                    MyBase.ShowMessage("E199", New String() {"ラベル種類"})
                    .cmbLabelTYpe.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .cmbLabelTYpe.Focus()
                    Return False
                End If
            End If

        End With

        '進捗チェック
        Return Me.IsStageChk(dr)

    End Function

    ''' <summary>
    ''' 削除処理チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsDeleteChk(ByVal ds As DataSet) As Boolean

        'START YANAI 要望番号573
        ''引当済みチェック
        'Dim rtnResult As Boolean = Me.IsHikiateChk(ds)
        Dim rtnResult As Boolean = True

        'EDIデータチェック
        rtnResult = rtnResult AndAlso Me.IsEdiDataChk(ds)

        '引当済みチェック
        rtnResult = rtnResult AndAlso Me.IsHikiateChk(ds)
        'END YANAI 要望番号573

        '運賃確定チェック
        Dim msg As String = _Frm.FunctionKey.F4ButtonName.ToString
        rtnResult = rtnResult AndAlso Me.IsUnchinKakuteiChk(ds, msg)

        '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
        '支払確定済みチェック
        rtnResult = rtnResult AndAlso Me.IsShiharaiKakuteiChk(ds, msg)
        '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

        '振替番号チェック
        rtnResult = rtnResult AndAlso Me.IsFuriDataChk(ds, msg)

        '作業レコードステージチェック
        rtnResult = rtnResult AndAlso Me.IsSagyoStageChk(ds)

        '在庫移動チェック
        rtnResult = rtnResult AndAlso Me.IsIdoTrsChk(ds)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 引当済みチェック（入荷(中)削除時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsHikiateDelMChk(ByVal ds As DataSet, ByVal arr As ArrayList) As Boolean

        Dim max As Integer = arr.Count - 1
        For i As Integer = 0 To max
            If LMB020C.HIKIATE_ARI.Equals(Me._Vcon.GetCellValue(Me._Frm.sprGoodsDef.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMB020G.sprGoodsDef.HIKIATE_STATE.ColNo))) = True Then
                Return Me._Vcon.SetErrMessage("E139")
            End If
        Next

        Return True

    End Function

    ''' <summary>
    ''' 引当済みチェック（入荷(小)削除時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsHikiateDelSChk(ByVal ds As DataSet) As Boolean

        Dim inkaMno As String = Me._Frm.lblKanriNoM.TextValue
        Dim max As Integer = Me._Frm.sprGoodsDef.ActiveSheet.Rows.Count - 1
        For i As Integer = 0 To max
            If (inkaMno).Equals(Me._Vcon.GetCellValue(Me._Frm.sprGoodsDef.ActiveSheet.Cells(i, LMB020G.sprGoodsDef.KANRI_NO.ColNo))) = True Then
                If (LMB020C.HIKIATE_ARI).Equals(Me._Vcon.GetCellValue(Me._Frm.sprGoodsDef.ActiveSheet.Cells((i), LMB020G.sprGoodsDef.HIKIATE_STATE.ColNo))) = True Then
                    Return Me._Vcon.SetErrMessage("E139")
                Else
                    Return True
                End If
            End If
        Next

        Return True

    End Function

    ''' <summary>
    ''' 引当済みチェック（編集時）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsHikiateEditChk(ByVal msgId As String, ByVal msgStr As String()) As Boolean    'ADD 2019/08/01 要望管理005237
        ''Friend Function IsHikiateEditChk(ByVal ds As DataSet) As Boolean                          'DEL 2019/08/01 要望管理005237

        Dim max As Integer = Me._Frm.sprGoodsDef.ActiveSheet.Rows.Count - 1
        For i As Integer = 0 To max
            If LMB020C.HIKIATE_ARI.Equals(Me._Vcon.GetCellValue(Me._Frm.sprGoodsDef.ActiveSheet.Cells(i, LMB020G.sprGoodsDef.HIKIATE_STATE.ColNo))) = True Then
                Return Me._Vcon.SetErrMessage(msgId, msgStr)    'ADD 2019/08/01 要望管理005237
                ''Return Me._Vcon.SetErrMessage("E139")         'DEL 2019/08/01 要望管理005237
            End If
        Next

        Return True

    End Function

    ''' <summary>
    ''' 在庫移動チェック（削除時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsIdoTrsChk(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_S)
        Dim dr() As DataRow = dt.Select("SYS_DEL_FLG = '0'")
        Dim max As Integer = dr.Length - 1

        For i As Integer = 0 To max

            '在庫移動がある場合、エラー
            If 0 < Convert.ToInt32(Me._Gcon.FormatNumValue(dr(i).Item("ZAI_REC_CNT").ToString())) Then
                Return Me._Vcon.SetErrMessage("E148")
            End If
        Next

        Return True

    End Function

    ''' <summary>
    ''' 在庫移動チェック（入荷(中)削除時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsIdoTrsDelMChk(ByVal ds As DataSet, ByVal arr As ArrayList) As Boolean

        Dim dt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_S)
        Dim dr As DataRow() = Nothing
        Dim max As Integer = arr.Count - 1
        Dim maxS As Integer = 0

        For i As Integer = 0 To max
            dr = Nothing
            dr = dt.Select(String.Concat("INKA_NO_M = '", Me._Vcon.GetCellValue(Me._Frm.sprGoodsDef.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMB020G.sprGoodsDef.KANRI_NO.ColNo)), "' AND ", _
                                         "SYS_DEL_FLG = '0'"))
            maxS = dr.Length - 1
            For j As Integer = 0 To maxS

                '在庫移動がある場合、エラー
                If 0 < Convert.ToInt32(Me._Gcon.FormatNumValue(dr(j).Item("ZAI_REC_CNT").ToString())) Then
                    Return Me._Vcon.SetErrMessage("E148")
                End If
            Next

        Next

        Return True

    End Function

    ''' <summary>
    ''' 在庫移動チェック（入荷(小)削除時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsIdoTrsDelSChk(ByVal ds As DataSet, ByVal arr As ArrayList) As Boolean

        Dim dt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_S)
        Dim dr As DataRow() = Nothing
        Dim max As Integer = arr.Count - 1

        For i As Integer = 0 To max
            dr = dt.Select(String.Concat("INKA_NO_M = '", Me._Frm.lblKanriNoM.TextValue, "' AND ", _
                                         "INKA_NO_S = '", Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMB020G.sprDetailDef.KANRI_NO_S.ColNo)), "' AND ", _
                                         "SYS_DEL_FLG = '0'"))

            '在庫移動がある場合、エラー
            If 0 < Convert.ToInt32(Me._Gcon.FormatNumValue(dr(0).Item("ZAI_REC_CNT").ToString())) Then
                Return Me._Vcon.SetErrMessage("E148")
            End If

        Next

        Return True

    End Function

    '要望番号:1523 terakawa 2012.10.22 Start
    '''' <summary>
    '''' 入荷(小)存在チェック
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>True:エラーなし,OK False:エラーあり</returns>
    '''' <remarks></remarks>
    'Friend Function IsInkaSCheck(ByVal ds As DataSet) As Boolean

    '    Dim dt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_S)
    '    Dim dr() As DataRow = dt.Select("SYS_DEL_FLG = '0'")
    '    Dim max As Integer = dr.Length - 1

    '    If -1 = max Then
    '        '入荷(小)が0件の場合
    '        Return Me._Vcon.SetErrMessage("E337")
    '    End If

    '    Return True

    'End Function


    ''' <summary>
    ''' 入荷(小)存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsInkaSCheck(ByVal ds As DataSet) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim dr As DataRow = Nothing
        Dim drs As DataRow() = ds.Tables(LMB020C.TABLE_NM_INKA_M).Select("SYS_DEL_FLG='0'")
        Dim mMax As Integer = drs.Count - 1
        If drs.Count = 0 Then
            '入荷(中)が存在しない場合エラー
            'Return Me._Vcon.SetErrMessage("E525", New String() {"入荷(中)"})

            '2017/09/25 修正 李↓
            Return Me._Vcon.SetErrMessage("E525", New String() {lgm.Selector({"入荷(中)", "INKA(M)", "입하(中)", "中国語"})})
            '2017/09/25 修正 李↑

        End If

        Dim inkaNoL As String = drs(0)("INKA_NO_L").ToString()
        Dim inkaNoM As String = String.Empty
        For i As Integer = 0 To drs.Count - 1
            dr = drs(i)
            inkaNoM = dr("INKA_NO_M").ToString()
            '各入荷(中)に入荷(小)が存在しない場合エラー
            If 0 = ds.Tables(LMB020C.TABLE_NM_INKA_S).Select(String.Concat(" INKA_NO_L = '", inkaNoL, "' AND INKA_NO_M = '", inkaNoM, "' AND SYS_DEL_FLG = '0' ")).Length Then
                Call Me.SetErrData(ds, dr, LMB020G.sprDetailDef.DEF.ColNo, False)
                'Return Me._Vcon.SetErrMessage("E525", New String() {"入荷(小)"})

                '2017/09/25 修正 李↓
                Return Me._Vcon.SetErrMessage("E525", New String() {lgm.Selector({"入荷(小)", "INKA(S)", "입하(小)", "中国語"})})
                '2017/09/25 修正 李↑

            End If
        Next

        Return True

    End Function
    '要望番号:1523 terakawa 2012.10.22 End



    ''' <summary>
    ''' 編集モード切替処理チェック
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsEditChk(ByVal ds As DataSet) As Boolean

        '自営業所チェック
        Dim rtnResult As Boolean = Me.IsNrsChk()

        '作業レコードのステージチェック
        rtnResult = rtnResult AndAlso Me.IsSagyoStageChk(ds)

        '運賃確定済みチェック
        Dim msg As String = _Frm.FunctionKey.F2ButtonName.ToString
        rtnResult = rtnResult AndAlso Me.IsUnchinKakuteiChk(ds, msg)

        '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
        '支払確定済みチェック
        rtnResult = rtnResult AndAlso Me.IsShiharaiKakuteiChk(ds, msg)
        '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

        '要望番号:1097 yamanaka 2012.07.09  Start
        '振替番号チェック
        'rtnResult = rtnResult AndAlso Me.IsFuriDataChk(ds, msg)
        '要望番号:1097 yamanaka 2012.07.09  End

        Return rtnResult

    End Function

    ''' <summary>
    ''' 特殊編集モード切替処理チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSpecialEditChk(ByVal ds As DataSet, ByVal actionType As LMB020C.ActionType) As Boolean

        Select Case actionType

            Case LMB020C.ActionType.UNSOEDIT

                '運送修正時のチェック
                If Me.IsUnchinKakuteiChk(ds, _Frm.FunctionKey.F7ButtonName.ToString) = False Then
                    Return False
                End If

                '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
                '支払確定済みチェック
                If Me.IsShiharaiKakuteiChk(ds, _Frm.FunctionKey.F7ButtonName.ToString) = False Then
                    Return False
                End If
                '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

        End Select

        Return True

    End Function

    ''' <summary>
    ''' 一括変更時の項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAllChangeInputChk() As Boolean

        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)

        '関連チェック
        With _Frm

            Dim chkFlg As Boolean = True
            Dim brcd As String = String.Empty
            Dim whcd As String = String.Empty
            Dim touNo As String = String.Empty
            Dim situNo As String = String.Empty
            Dim zoneCd As String = String.Empty
            Dim loca As String = String.Empty

            brcd = .cmbEigyo.SelectedValue.ToString()
            whcd = .cmbSoko.SelectedValue.ToString()
            touNo = .txtTouNo.TextValue
            situNo = .txtSituNo.TextValue
            zoneCd = .txtZoneCd.TextValue
            loca = .txtLocation.TextValue

            '単項目チェック
            If Me.IsAllChangeCheck() = False Then
                Return False
            End If

            '関連チェック
            If String.IsNullOrEmpty(touNo) = True OrElse String.IsNullOrEmpty(situNo) = True OrElse _
            String.IsNullOrEmpty(zoneCd) = True Then
                Me.SetErrorControl(.txtSituNo)
                Me.SetErrorControl(.txtZoneCd)
                Me.SetErrorControl(.txtTouNo)
                Me._Vcon.SetErrMessage("E806")

                Return False
            End If

            '棟・室・ZONEマスタ存在チェック
            If Me._Vcon.SelectToShitsuZoneListDataRow(brcd, whcd, touNo, situNo, zoneCd).Length < 1 Then
                Me.SetErrorControl(.txtSituNo)
                Me.SetErrorControl(.txtZoneCd)
                Me.SetErrorControl(.txtTouNo)

                Dim msg As String = String.Empty
                msg = lgm.Selector({"棟室ZONEマスタ", "Building room ZONE master", "동(棟)실(室)존(Zone)마스터", "中国語"})

                Return Me._Vcon.SetMstErrMessage(msg, String.Concat(touNo, " - ", situNo, " - ", zoneCd))
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' エラー項目の背景色とフォーカスを設定する
    ''' </summary>
    ''' <param name="ctl">エラーコントロール</param>
    ''' <remarks></remarks>
    Friend Sub SetErrorControl(ByVal ctl As Control)

        Dim errorColor As System.Drawing.Color = Utility.LMGUIUtility.GetAttentionBackColor()

        If TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMComboKubun = True Then

            DirectCast(ctl, Win.InputMan.LMComboKubun).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMImNumber = True Then

            DirectCast(ctl, Win.InputMan.LMImNumber).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMImDate = True Then

            DirectCast(ctl, Win.InputMan.LMImDate).BackColorDef = errorColor

        End If

        ctl.Focus()
        ctl.Select()

    End Sub

    ''' <summary>
    ''' 一括変更時の単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsAllChangeCheck() As Boolean

        With Me._Frm

            '置場(棟)
            .txtTouNo.ItemName = "棟番号"
            .txtTouNo.IsForbiddenWordsCheck = True
            .txtTouNo.IsByteCheck = 2
            If MyBase.IsValidateCheck(.txtTouNo) = False Then
                Return False
            End If

            '置場(室)
            .txtSituNo.ItemName = "室番号"
            .txtSituNo.IsForbiddenWordsCheck = True
            .txtSituNo.IsByteCheck = 2
            If MyBase.IsValidateCheck(.txtSituNo) = False Then
                Return False
            End If

            '置場(ZONE)
            .txtZoneCd.ItemName = "ZONEコード"
            .txtZoneCd.IsForbiddenWordsCheck = True
            .txtZoneCd.IsByteCheck = 2
            If MyBase.IsValidateCheck(.txtZoneCd) = False Then
                Return False
            End If

            '置場(ロケーション)
            .txtLocation.ItemName = "ロケーション"
            .txtLocation.IsForbiddenWordsCheck = True
            .txtLocation.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtLocation) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 入荷(中)追加時の処理前チェック
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="arr">チェックONのリスト 初期値 = Nothing</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsInkaMAddChk(ByVal actionType As LMB020C.ActionType, Optional ByVal arr As ArrayList = Nothing) As Boolean

        Select Case actionType

            Case LMB020C.ActionType.DEL_M

                '入荷(中)の行削除処理のみ行う。
                '入荷中番がない場合、スルー
                Dim inkaNoM As String = Me._Frm.lblKanriNoM.TextValue
                If String.IsNullOrEmpty(inkaNoM) = True Then
                    Return True
                End If

                '現在表示されている情報を選択していた場合、チェックをしない
                Dim max As Integer = arr.Count - 1
                Dim spr As FarPoint.Win.Spread.SheetView = Me._Frm.sprGoodsDef.ActiveSheet
                Dim colNo As Integer = LMB020G.sprGoodsDef.KANRI_NO.ColNo
                For i As Integer = 0 To max
                    If inkaNoM.Equals(Me._Vcon.GetCellValue(spr.Cells(Convert.ToInt32(arr(i)), colNo))) = True Then
                        Return True
                    End If
                Next

        End Select

        'スペース除去
        Call Me.TrimSpaceTextValue()

        With Me._Frm

            '入荷(中)の単項目チェック
            Dim rtnResult As Boolean = Me.IsInputGoodsChk()

            '入荷(小)の単項目チェック
            rtnResult = rtnResult AndAlso Me.IsInputDetailChk()

            '作業(中)のマスタ存在チェック
            'START YANAI 要望番号495
            'rtnResult = rtnResult AndAlso Me.IsSagyoExistChk(LMB020C.SagyoData.M, .tabGoods)
            rtnResult = rtnResult AndAlso Me.IsSagyoExistChk(LMB020C.SagyoData.M, .tabUnso)
            'END YANAI 要望番号495

            '作業(中)のマスタ存在チェック
            'START YANAI 要望番号495
            'rtnResult = rtnResult AndAlso Me.IsSagyoExistChk(LMB020C.SagyoData.M, .tabGoods)
            rtnResult = rtnResult AndAlso Me.IsSagyoExistChk(LMB020C.SagyoData.M, .tabUnso)
            'END YANAI 要望番号495

            '入荷(小)の関連チェック
            rtnResult = rtnResult AndAlso Me.IsSprDetailChk()

            '作業(中)の重複チェック
            'START YANAI 要望番号495
            'rtnResult = rtnResult AndAlso Me.IsSagyoJufukuChk(LMB020C.SagyoData.M, .tabGoods)
            rtnResult = rtnResult AndAlso Me.IsSagyoJufukuChk(LMB020C.SagyoData.M, .tabUnso, True)
            'END YANAI 要望番号495

            '行追加時に渡す商品情報の単項目チェック
            rtnResult = rtnResult AndAlso Me.IsAddGoodsChk()

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 入荷(中)削除の処理前チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsInkaMDeleteChk(ByVal ds As DataSet) As Boolean

        '引当済みチェック
        Return Me.IsHikiateChk(ds)

    End Function

    ''' <summary>
    ''' 入荷(小)削除の処理前チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsInkaSDeleteChk(ByVal ds As DataSet) As Boolean

        '引当済みチェック
        Return Me.IsHikiateChk(ds)

    End Function

    ''' <summary>
    ''' 入荷(小)の値設定前チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsDoubleClickChk() As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        Dim rtnResult As Boolean = Me.IsInputDetailChk()

        '関連チェック
        rtnResult = rtnResult AndAlso Me.IsSprDetailChk()

        Return rtnResult

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
    ''' 入荷中番号の必須チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsInkaMNoHissuChk() As Boolean

        With Me._Frm

            '入荷(中)未選択チェック
            If String.IsNullOrEmpty(.lblKanriNoM.TextValue) = True Then
                Return Me._Vcon.SetErrMessage("E214")
            End If

        End With

        Return True

    End Function

    'START YANAI 要望番号557
    ''' <summary>
    ''' 複写行数の範囲チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsInkaNoSOverChk() As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            '複写行数の必須チェック
            If String.IsNullOrEmpty(.numRowCopyScnt.TextValue) = True Then
                Me._Vcon.SetErrorControl(.numRowCopyScnt)
                'Me._Vcon.SetErrMessage("E001", New String() {"複写行数"})

                '2017/09/25 修正 李↓
                Me._Vcon.SetErrMessage("E001", New String() {lgm.Selector({"複写行数", "Copying Line", "복사행 수", "中国語"})})
                '2017/09/25 修正 李↑

            End If

            '複写行数の範囲チェック
            If Convert.ToDecimal(LMB020C.COPY_S_MIN) > Convert.ToDecimal(.numRowCopyScnt.TextValue) OrElse _
                Convert.ToDecimal(.numRowCopyScnt.TextValue) > Convert.ToDecimal(LMB020C.COPY_S_MAX) Then
                Me._Vcon.SetErrorControl(.numRowCopyScnt)
                'Me._Vcon.SetErrMessage("E014", New String() {"複写行数", LMB020C.COPY_S_MIN, LMB020C.COPY_S_MAX})

                '2017/09/25 修正 李↓
                Me._Vcon.SetErrMessage("E014", New String() {lgm.Selector({"複写行数", "Copying Line", "복사행 수", "中国語"})})
                '2017/09/25 修正 李↑

            End If

        End With

        Return True

    End Function
    'END YANAI 要望番号557

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMB020C.ActionType) As Boolean

        'フォーカス位置がない場合(ポップ対象外の場合)、スルー
        If String.IsNullOrEmpty(objNm) = True Then

            'Enterは表示しない
            Select Case actionType

                Case LMB020C.ActionType.ENTER

                    Return False

            End Select

            Return Me._Vcon.SetFocusErrMessage()

        End If

        Dim ctl1 As Win.InputMan.LMImTextBox = Nothing
        Dim ctl2 As Win.InputMan.LMImTextBox = Nothing
        Dim ctl3 As Win.InputMan.LMImTextBox = Nothing
        Dim ctl4 As Win.InputMan.LMImTextBox = Nothing
        Dim msg1 As String = String.Empty
        Dim msg2 As String = String.Empty
        Dim msg3 As String = String.Empty
        Dim nullChk As Boolean = True
        Dim setFlg As Boolean = False

        With Me._Frm

            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    ctl1 = .txtCustCdL
                    msg1 = "荷主(大)コード"
                    ctl2 = .txtCustCdM
                    msg2 = "荷主(中)コード"
                    setFlg = True

                    ctl3 = .lblCustNm

                Case .txtUnsoCd.Name, .txtTrnBrCD.Name

                    ctl1 = .txtUnsoCd
                    msg1 = String.Concat(.lblTitleUnsoco.Text, "コード")
                    ctl2 = .txtTrnBrCD
                    msg2 = String.Concat(.lblTitleUnsoco.Text, "支店コード")
                    setFlg = True

                    ctl3 = .lblTrnNM

                Case .txtShukkaMotoCD.Name

                    ctl1 = .txtShukkaMotoCD
                    msg1 = String.Concat(.lblTitleShukkamoto.Text, "コード")

                    ctl3 = .lblShukkaMotoNM

                Case .txtUnsoTariffCD.Name

                    ctl1 = .txtUnsoTariffCD
                    msg1 = String.Concat(.lblTitleUnsoTariffCd.Text, "コード")

                    ctl3 = .lblUnsoTariffNM


                    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
                Case .txtShiharaiTariffCD.Name

                    ctl1 = .txtShiharaiTariffCD
                    msg1 = String.Concat(.lblTitleShiharaiTariffCd.Text, "コード")

                    ctl3 = .lblShiharaiTariffNM
                    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

                Case .txtSerchGoodsCd.Name, .txtSerchGoodsNm.Name

                    ctl1 = .txtSerchGoodsCd
                    msg1 = String.Concat(.lblTitleGoods.Text, "コード")

                    ctl2 = .txtSerchGoodsNm
                    msg2 = String.Concat(.lblTitleGoods.Text, "名称")

                Case .sprDetail.Name

                    Return True

                Case .txtNyukaComment.Name

                    'Enterは表示しない
                    Select Case actionType
                        Case LMB020C.ActionType.ENTER
                            Return False
                    End Select

                    ctl1 = .txtNyukaComment
                    msg1 = .lblTitleNyukaComment.Text
                    nullChk = False

                Case .txtGoodsComment.Name

                    'Enterは表示しない
                    Select Case actionType
                        Case LMB020C.ActionType.ENTER
                            Return False
                    End Select

                    ctl1 = .txtGoodsComment
                    msg1 = .lblTitleGoodsComment.Text
                    nullChk = False

                Case .txtSituNo.Name, .txtTouNo.Name, .txtZoneCd.Name
                    ctl1 = .txtTouNo
                    msg1 = "棟番号"
                    ctl2 = .txtSituNo
                    msg2 = "室番号"
                    ctl3 = .txtZoneCd
                    msg3 = "ZONEコード"

            End Select

            '作業コードの場合のチェック
            Select Case objNm.Substring(0, objNm.Length - 2)

                Case LMB020C.SAGYO_CD

                    ctl1 = Me._G.GetTextControl(objNm)
                    msg1 = "作業コード"

                    ctl3 = DirectCast(.Controls.Find(String.Concat(LMB020C.SAGYO_NM, objNm.Substring(objNm.Length - 2, 2)), True)(0), Win.InputMan.LMImTextBox)
                    ctl4 = DirectCast(.Controls.Find(String.Concat(LMB020C.SAGYO_FL, objNm.Substring(objNm.Length - 2, 2)), True)(0), Win.InputMan.LMImTextBox)

            End Select

            'Nothing判定用
            Dim ctlChk As Boolean = ctl2 Is Nothing
            Dim ctlChk2 As Boolean = ctl3 Is Nothing

            'フォーカス位置が参照可能でない場合、エラー
            If (ctl1 Is Nothing = True OrElse ctl1.ReadOnly = True) _
                OrElse (ctlChk = False AndAlso ctl2.ReadOnly = True) Then

                Select Case actionType

                    Case LMB020C.ActionType.MASTEROPEN

                        Return Me._Vcon.SetFocusErrMessage()

                    Case LMB020C.ActionType.ENTER

                        'Enterの場合はメッセージは設定しない
                        Return False

                End Select

            End If


            'Enterイベントの場合、空の場合、表示しない
            Select Case actionType

                Case LMB020C.ActionType.ENTER

                    If setFlg = True Then

                        '両方に値がない場合、スルー
                        If String.IsNullOrEmpty(ctl1.TextValue) = True _
                            AndAlso String.IsNullOrEmpty(ctl2.TextValue) = True Then

                            If Not ctl3 Is Nothing Then
                                ctl3.TextValue = String.Empty
                            End If
                            If Not ctl4 Is Nothing Then
                                ctl4.TextValue = String.Empty
                            End If
                            Return False
                        End If

                    Else

                        'ctl1に値がない 且つ (ctl2がNothing または ctl2に値がない)場合、スルー
                        If nullChk = True _
                            AndAlso String.IsNullOrEmpty(ctl1.TextValue) = True _
                            AndAlso (ctlChk = True OrElse String.IsNullOrEmpty(ctl2.TextValue) = True) _
                            Then

                            If Not ctl3 Is Nothing Then
                                ctl3.TextValue = String.Empty
                            End If
                            If Not ctl4 Is Nothing Then
                                ctl4.TextValue = String.Empty
                            End If
                            Return False

                        End If

                    End If

            End Select

            '禁止文字チェック(1つ目のコントロール)
            ctl1.ItemName = msg1
            ctl1.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(ctl1) = False Then
                Return False
            End If

            '禁止文字チェック(2つ目のコントロール)
            If ctlChk = False Then
                ctl2.ItemName = msg1
                ctl2.IsForbiddenWordsCheck = True
                If MyBase.IsValidateCheck(ctl2) = False Then
                    Return False
                End If
            End If

            '禁止文字チェック(3つ目のコントロール)
            If ctlChk2 = False Then
                ctl3.ItemName = msg3
                ctl3.IsForbiddenWordsCheck = True
                If MyBase.IsValidateCheck(ctl3) = False Then
                    Return False
                End If
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' カーソル位置チェック
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="cell">セル</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusSprChk(ByVal spr As Win.Spread.LMSpread, ByVal cell As FarPoint.Win.Spread.Cell) As Boolean

        'ロック項目はスルー
        If cell.Locked = True OrElse spr.ActiveSheet.Columns(cell.Column.Index).Locked = True Then

            Return Me._Vcon.SetFocusErrMessage()

        End If

        Return True

    End Function

    ''' <summary>
    ''' POP UP用入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsPopupInputCheck(ByVal spr As Win.Spread.LMSpread, ByVal rowNo As Integer, ByVal colNo As Integer, ByVal colNm As String) As Boolean

        'チェック用のセル
        Dim vCell As Utility.Spread.LMValidatableCells = New Utility.Spread.LMValidatableCells(spr)

        With vCell

            .SetValidateCell(rowNo, colNo)
            .ItemName = colNm
            .IsForbiddenWordsCheck = True

            Return Me.IsValidateCheck(vCell)

        End With

    End Function

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
    ''' 入荷（小）スプレッドのチェック取得
    ''' </summary>
    ''' <returns> </returns>
    ''' <remarks></remarks>
    Friend Function IsSprSSelectChk() As ArrayList

        Dim array As ArrayList = New ArrayList()

        With Me._Frm.sprDetail.Sheets(0)
            Dim max As Integer = .RowCount - 1
            For i As Integer = 0 To max
                'START YANAI 要望番号427
                'If .Cells(i, LMB020C.SprInkaSColumnIndex.DEF).Value.Equals(True) = True Then
                If .Cells(i, LMB020C.SprInkaSColumnIndex.DEF).Value.Equals(True) = True OrElse _
                    .Cells(i, LMB020C.SprInkaSColumnIndex.DEF).Value.Equals("True") = True Then
                    'END YANAI 要望番号427
                    array.Add(i)
                End If
            Next
        End With

        Return array

    End Function

    ''' <summary>
    ''' 権限チェックの共通処理
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthority(ByVal ActionType As LMB020C.ActionType) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case ActionType

            Case LMB020C.ActionType.INIT            '新規
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

            Case LMB020C.ActionType.EDIT            '編集
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

            Case LMB020C.ActionType.COPY            '複写
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

            Case LMB020C.ActionType.DELETE          '削除
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

            Case LMB020C.ActionType.UNSOEDIT        '運送修正
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

            Case LMB020C.ActionType.DATEEDIT        '起算日修正
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

            Case LMB020C.ActionType.MASTEROPEN      'マスタ参照
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

            Case LMB020C.ActionType.SAVE            '保存
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

            Case LMB020C.ActionType.CLOSE           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMB020C.ActionType.PRINT           '印刷
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

            Case LMB020C.ActionType.INIT_M          '追加（中）
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

            Case LMB020C.ActionType.DEL_M           '削除（中）
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

            Case LMB020C.ActionType.INIT_S          '追加（小）
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

            Case LMB020C.ActionType.DEL_S           '削除（小）
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

            Case LMB020C.ActionType.ENTER           'ENTER押下
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

            Case LMB020C.ActionType.HENKO           '一括変更
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

            Case LMB020C.ActionType.COA             '分析票
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

            Case LMB020C.ActionType.YCARD           'イエローカード
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

                'ADD 2022/11/07 倉庫写真アプリ対応 START
            Case LMB020C.ActionType.PHOTOSEL        '写真選択
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
                'ADD 2022/11/07 倉庫写真アプリ対応 END

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        Return kengenFlg

    End Function

    '追加開始 --- 2015.03.24
    ''' <summary>
    ''' 取込(検品取込)チェック
    ''' </summary>
    ''' <param name="count">対象区分マスタカウント</param>
    ''' <param name="msg">エラーメッセージ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsKenpinTorikomiActionChk(ByVal count As Integer, ByVal msg As String) As Boolean

        If 0 = count Then
            Return Me._Vcon.SetErrMessage("E208", New String() {msg})
        End If

        Return True

    End Function
    '追加終了 --- 2015.03.24

#End Region

#Region "Sub"

    ''' <summary>
    ''' 入荷(大)単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputHeadChk() As Boolean

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '倉庫
            .cmbSoko.ItemName = .lblTitleSoko.Text
            .cmbSoko.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbSoko) = errorFlg Then
                Return errorFlg
            End If

            '入荷種別
            .cmbNyukaType.ItemName = .lblTitleInkaType.Text
            .cmbNyukaType.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbNyukaType) = errorFlg Then
                Return errorFlg
            End If

            '入荷日
            .imdNyukaDate.ItemName = .lblTitleNyukaDate.Text
            .imdNyukaDate.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.imdNyukaDate) = errorFlg Then
                Return errorFlg
            End If

            '注文番号
            .txtBuyerOrdNo.ItemName = .lblTitleBuyerOrdNo.Text
            .txtBuyerOrdNo.IsForbiddenWordsCheck = chkFlg
            .txtBuyerOrdNo.IsByteCheck = 30
            If MyBase.IsValidateCheck(.txtBuyerOrdNo) = errorFlg Then
                Return errorFlg
            End If

            'オーダー番号
            .txtOrderNo.ItemName = .lblTitleOrderNo.Text
            .txtOrderNo.IsForbiddenWordsCheck = chkFlg
            .txtOrderNo.IsByteCheck = 30
            If MyBase.IsValidateCheck(.txtOrderNo) = errorFlg Then
                Return errorFlg
            End If

            '保管料フリー期間
            .numFreeKikan.ItemName = .lblTitleFreeKikan.Text
            If MyBase.IsValidateCheck(.numFreeKikan) = errorFlg Then
                Return errorFlg
            End If

            '保管料起算日
            If Me.IsInputDateFullByteChk(.imdHokanStrDate, .lblTitleHokanStrDate.Text) = errorFlg Then
                Return errorFlg
            End If

            '課税区分
            .cmbKazeiKbn.ItemName = .lblTitleKazeiKbn.Text
            .cmbKazeiKbn.IsHissuCheck = .cmbKazeiKbn.HissuLabelVisible
            If MyBase.IsValidateCheck(.cmbKazeiKbn) = errorFlg Then
                Return errorFlg
            End If

            '当期保管料
            .cmbToukiHokanUmu.ItemName = .lblTitleToukiHokanUmu.Text
            .cmbToukiHokanUmu.IsHissuCheck = .cmbToukiHokanUmu.HissuLabelVisible
            If MyBase.IsValidateCheck(.cmbToukiHokanUmu) = errorFlg Then
                Return errorFlg
            End If

            '全期保管料
            .cmbZenkiHokanUmu.ItemName = .lblTitleZenkiHokanUmu.Text
            .cmbZenkiHokanUmu.IsHissuCheck = .cmbZenkiHokanUmu.HissuLabelVisible
            If MyBase.IsValidateCheck(.cmbZenkiHokanUmu) = errorFlg Then
                Return errorFlg
            End If

            '荷役料
            .cmbNiyakuUmu.ItemName = .lblTitleNiyakuUmu.Text
            .cmbNiyakuUmu.IsHissuCheck = .cmbNiyakuUmu.HissuLabelVisible
            If MyBase.IsValidateCheck(.cmbNiyakuUmu) = errorFlg Then
                Return errorFlg
            End If

            '備考大(社外)
            .txtNyubanL.ItemName = .lblTitleNyubanL.Text
            .txtNyubanL.IsHissuCheck = .txtNyubanL.HissuLabelVisible
            .txtNyubanL.IsForbiddenWordsCheck = chkFlg
            .txtNyubanL.IsByteCheck = 15
            If MyBase.IsValidateCheck(.txtNyubanL) = errorFlg Then
                Return errorFlg
            End If

            '備考大(社内)
            .txtNyukaComment.ItemName = .lblTitleNyukaComment.Text
            .txtNyukaComment.IsHissuCheck = .txtNyukaComment.HissuLabelVisible
            .txtNyukaComment.IsForbiddenWordsCheck = chkFlg
            .txtNyukaComment.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtNyukaComment) = errorFlg Then
                Return errorFlg
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 入荷(中)詳細の単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputGoodsChk() As Boolean

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '印順
            .numSort.ItemName = .lblTitleSort.Text
            .numSort.IsHissuCheck = .numSort.HissuLabelVisible
            If MyBase.IsValidateCheck(.numSort) = errorFlg Then
                .tabMiddle.SelectTab(.tabGoods)
                Return errorFlg
            End If

            'オーダー番号
            .txtOrderNoM.ItemName = .lblTitleOrderNoM.Text
            .txtOrderNoM.IsForbiddenWordsCheck = chkFlg
            .txtOrderNoM.IsByteCheck = 30
            If MyBase.IsValidateCheck(.txtOrderNoM) = errorFlg Then
                'START YANAI 要望番号495
                '.tabMiddle.SelectTab(.tabGoods)
                .tabMiddle.SelectTab(.tabUnso)
                'END YANAI 要望番号495
                Return errorFlg
            End If

            '注文番号
            .txtBuyerOrdNoM.ItemName = .lblTitleBuyerOrdNoM.Text
            .txtBuyerOrdNoM.IsForbiddenWordsCheck = chkFlg
            .txtBuyerOrdNoM.IsByteCheck = 30
            If MyBase.IsValidateCheck(.txtBuyerOrdNoM) = errorFlg Then
                'START YANAI 要望番号495
                '.tabMiddle.SelectTab(.tabGoods)
                .tabMiddle.SelectTab(.tabUnso)
                'END YANAI 要望番号495
                Return errorFlg
            End If

            '商品コメント
            .txtGoodsComment.ItemName = .lblTitleGoodsComment.Text
            .txtGoodsComment.IsForbiddenWordsCheck = chkFlg
            .txtGoodsComment.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtGoodsComment) = errorFlg Then
                'START YANAI 要望番号495
                '.tabMiddle.SelectTab(.tabGoods)
                .tabMiddle.SelectTab(.tabUnso)
                'END YANAI 要望番号495
                Return errorFlg
            End If

            '作業コード
            If Me.IsInputSagyoChk(LMB020C.SagyoData.M) = errorFlg Then
                'START YANAI 要望番号495
                '.tabMiddle.SelectTab(.tabGoods)
                .tabMiddle.SelectTab(.tabUnso)
                'END YANAI 要望番号495
                Return errorFlg
            End If

            '作業備考
            If Me.IsInputSagyoRemarkChk(LMB020C.SagyoData.M) = errorFlg Then
                .tabMiddle.SelectTab(.tabSagyoM)
                Return errorFlg
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 入荷(小)の単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputDetailChk() As Boolean

        Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprDetail)

        With vCell

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False
            Dim spr As LMSpread = Me._Frm.sprDetail
            Dim max As Integer = spr.ActiveSheet.Rows.Count - 1

            For i As Integer = 0 To max

                'ロット№
                .SetValidateCell(i, LMB020G.sprDetailDef.LOT_NO.ColNo)
                .ItemName = LMB020G.sprDetailDef.LOT_NO.ColName
                .IsForbiddenWordsCheck = chkFlg
                .IsByteCheck = 40
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                '棟
                .SetValidateCell(i, LMB020G.sprDetailDef.TOU_NO.ColNo)
                .ItemName = LMB020G.sprDetailDef.TOU_NO.ColName
                .IsForbiddenWordsCheck = chkFlg
                .IsByteCheck = 2
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                '室
                .SetValidateCell(i, LMB020G.sprDetailDef.SHITSU_NO.ColNo)
                .ItemName = LMB020G.sprDetailDef.SHITSU_NO.ColName
                .IsForbiddenWordsCheck = chkFlg
                'START YANAI 要望番号705
                '.IsByteCheck = 1
                'START S_KOBA 要望番号705
                '.IsFullByteCheck = 2
                .IsByteCheck = 2
                'END S_KOBA 要望番号705
                'END YANAI 要望番号705
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                'ZONE
                .SetValidateCell(i, LMB020G.sprDetailDef.ZONE_CD.ColNo)
                .ItemName = LMB020G.sprDetailDef.ZONE_CD.ColName
                .IsForbiddenWordsCheck = chkFlg
                'START YANAI 要望番号705
                '.IsByteCheck = 1
                'START S_KOBA 要望番号705
                '.IsFullByteCheck = 2
                .IsByteCheck = 2
                'END S_KOBA 要望番号705
                'END YANAI 要望番号705
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                'Loc
                .SetValidateCell(i, LMB020G.sprDetailDef.LOCA.ColNo)
                .ItemName = LMB020G.sprDetailDef.LOCA.ColName
                .IsForbiddenWordsCheck = chkFlg
                .IsByteCheck = 10
                'START YANAI 要望番号548
                '.IsMiddleSpace = chkFlg
                'END YANAI 要望番号548
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                '入目
                If IsIrimeHaniChk(i) = False Then
                    errorFlg = False
                    Return errorFlg
                End If

                '賞味期限
                .SetValidateCell(i, LMB020G.sprDetailDef.LT_DATE.ColNo)
                .ItemName = LMB020G.sprDetailDef.LT_DATE.ColName
                .IsForbiddenWordsCheck = chkFlg
                .IsFullByteCheck = LMB020C.SPR_DATE_FULL
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                '備考小(社内)
                .SetValidateCell(i, LMB020G.sprDetailDef.REMARK.ColNo)
                .ItemName = LMB020G.sprDetailDef.REMARK.ColName
                .IsForbiddenWordsCheck = chkFlg
                .IsByteCheck = 100
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                '備考小(社外)
                .SetValidateCell(i, LMB020G.sprDetailDef.REMARK_OUT.ColNo)
                .ItemName = LMB020G.sprDetailDef.REMARK_OUT.ColName
                .IsForbiddenWordsCheck = chkFlg
                .IsByteCheck = 15
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                '簿外品
                .SetValidateCell(i, LMB020G.sprDetailDef.OFB_KBN.ColNo)
                .ItemName = LMB020G.sprDetailDef.OFB_KBN.ColName
                .IsHissuCheck = chkFlg
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                '保留品
                .SetValidateCell(i, LMB020G.sprDetailDef.SPD_KBN_S.ColNo)
                .ItemName = LMB020G.sprDetailDef.SPD_KBN_S.ColName
                .IsHissuCheck = chkFlg
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                'シリアル№
                .SetValidateCell(i, LMB020G.sprDetailDef.SERIAL_NO.ColNo)
                .ItemName = LMB020G.sprDetailDef.SERIAL_NO.ColName
                .IsForbiddenWordsCheck = chkFlg
                .IsByteCheck = 40
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                '製造日
                .SetValidateCell(i, LMB020G.sprDetailDef.GOODS_CRT_DATE.ColNo)
                .ItemName = LMB020G.sprDetailDef.GOODS_CRT_DATE.ColName
                .IsFullByteCheck = LMB020C.SPR_DATE_FULL
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                '届先コード
                .SetValidateCell(i, LMB020G.sprDetailDef.DEST_CD.ColNo)
                .ItemName = LMB020G.sprDetailDef.DEST_CD.ColName
                .IsForbiddenWordsCheck = chkFlg
                .IsByteCheck = 15
                .IsMiddleSpace = chkFlg
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                '2018/11/02 ADD START 要望番号002824'
                '割当優先
                .SetValidateCell(i, LMB020G.sprDetailDef.ALLOC_PRIORITY.ColNo)
                .ItemName = LMB020G.sprDetailDef.ALLOC_PRIORITY.ColName
                .IsHissuCheck = chkFlg
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If
                '2018/11/02 ADD END 要望番号002824'
            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 運送情報の単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnsoDataChk() As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '運賃有無
            .cmbUnchinUmu.ItemName = .lblTitleUnchinUmu.Text
            .cmbUnchinUmu.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbUnchinUmu) = errorFlg Then
                'START YANAI 要望番号495
                '.tabMiddle.SelectTab(.tabUnso)
                'END YANAI 要望番号495
                .tabTop.SelectTab(.tabUnsoInfo)
                Return errorFlg
            End If

            '運送会社コード
            '2017/09/25 修正 李↓
            .txtUnsoCd.ItemName = String.Concat(.lblTitleUnsoco.Text, lgm.Selector({"コード", "Code", "코드", "中国語"}))
            '2017/09/25 修正 李↑

            .txtUnsoCd.IsForbiddenWordsCheck = chkFlg
            .txtUnsoCd.IsByteCheck = 5
            .txtUnsoCd.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtUnsoCd) = errorFlg Then
                'START YANAI 要望番号495
                '.tabMiddle.SelectTab(.tabUnso)
                'END YANAI 要望番号495
                .tabTop.SelectTab(.tabUnsoInfo)
                Return errorFlg
            End If

            '運送会社支店コード
            '2017/09/25 修正 李↓
            .txtTrnBrCD.ItemName = String.Concat(.lblTitleUnsoco.Text, lgm.Selector({"支店コード", "Branch code", "지점코드", "中国語"}))
            '2017/09/25 修正 李↑

            .txtTrnBrCD.IsForbiddenWordsCheck = chkFlg
            .txtTrnBrCD.IsByteCheck = 3
            .txtTrnBrCD.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtTrnBrCD) = errorFlg Then
                'START YANAI 要望番号495
                '.tabMiddle.SelectTab(.tabUnso)
                'END YANAI 要望番号495
                .tabTop.SelectTab(.tabUnsoInfo)
                Return errorFlg
            End If

            '運賃タリフ
            .txtUnsoTariffCD.ItemName = .lblTitleUnsoTariffCd.Text
            .txtUnsoTariffCD.IsForbiddenWordsCheck = chkFlg
            .txtUnsoTariffCD.IsByteCheck = 10
            .txtUnsoTariffCD.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtUnsoTariffCD) = errorFlg Then
                'START YANAI 要望番号495
                '.tabMiddle.SelectTab(.tabUnso)
                'END YANAI 要望番号495
                .tabTop.SelectTab(.tabUnsoInfo)
                Return errorFlg
            End If

            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
            '支払タリフ
            .txtShiharaiTariffCD.ItemName = .lblTitleShiharaiTariffCd.Text
            .txtShiharaiTariffCD.IsForbiddenWordsCheck = chkFlg
            .txtShiharaiTariffCD.IsByteCheck = 10
            .txtShiharaiTariffCD.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtShiharaiTariffCD) = errorFlg Then
                .tabTop.SelectTab(.tabUnsoInfo)
                Return errorFlg
            End If
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End


            '出荷元
            .txtShukkaMotoCD.ItemName = .lblTitleShukkamoto.Text
            .txtShukkaMotoCD.IsForbiddenWordsCheck = chkFlg
            .txtShukkaMotoCD.IsByteCheck = 15
            If MyBase.IsValidateCheck(.txtShukkaMotoCD) = errorFlg Then
                'START YANAI 要望番号495
                '.tabMiddle.SelectTab(.tabUnso)
                'END YANAI 要望番号495
                .tabTop.SelectTab(.tabUnsoInfo)
                Return errorFlg
            End If

            '運送コメント
            .txtUnchinComment.ItemName = .lblTitleUnchinComment.Text
            .txtUnchinComment.IsForbiddenWordsCheck = chkFlg
            .txtUnchinComment.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtUnchinComment) = errorFlg Then
                'START YANAI 要望番号495
                '.tabMiddle.SelectTab(.tabUnso)
                'END YANAI 要望番号495
                .tabTop.SelectTab(.tabUnsoInfo)
                Return errorFlg
            End If

            '作業コード
            If Me.IsInputSagyoChk(LMB020C.SagyoData.L) = errorFlg Then
                'START YANAI 要望番号495
                '.tabMiddle.SelectTab(.tabUnso)
                'END YANAI 要望番号495
                .tabTop.SelectTab(.tabSagyoL)
                Return errorFlg
            End If

            '作業備考
            If Me.IsInputSagyoRemarkChk(LMB020C.SagyoData.L) = errorFlg Then
                .tabTop.SelectTab(.tabSagyoL)
                Return errorFlg
            End If

            '運送課税区分
            Dim hissuFlg As Boolean = False
            .cmbTax.ItemName = .lblTitleTax.Text
            If LMB020C.TEHAI_NRS.Equals(.cmbUnchinUmu.SelectedValue) = True Then
                hissuFlg = True
            End If
            .cmbTax.IsHissuCheck = hissuFlg
            If MyBase.IsValidateCheck(.cmbTax) = errorFlg Then
                'START YANAI 要望番号495
                '.tabMiddle.SelectTab(.tabUnso)
                'END YANAI 要望番号495
                .tabTop.SelectTab(.tabUnsoInfo)
                Return errorFlg
            End If


            '要望番号1357:(輸送営業所に初期値設定し、必須チェックを入れる) 2012/08/22 本明 Start
            '輸送営業所
            hissuFlg = False
            .cmbYusoBrCd.ItemName = .lblYusoBr.Text
            If LMB020C.TEHAI_NRS.Equals(.cmbUnchinUmu.SelectedValue) = True Then
                hissuFlg = True
            End If
            .cmbYusoBrCd.IsHissuCheck = hissuFlg
            If MyBase.IsValidateCheck(.cmbYusoBrCd) = errorFlg Then
                .tabTop.SelectTab(.tabUnsoInfo)
                Return errorFlg
            End If
            '要望番号1357:(輸送営業所に初期値設定し、必須チェックを入れる) 2012/08/22 本明 End





        End With

        Return True

    End Function

    ''' <summary>
    ''' 日付のフルバイトチェック
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="str">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputDateFullByteChk(ByVal ctl As Win.InputMan.LMImDate, ByVal str As String) As Boolean

        If ctl.IsDateFullByteCheck = False Then

            Return Me._Vcon.SetErrMessage("E038", New String() {str, "8"})

        End If

        Return True

    End Function

    ''' <summary>
    ''' 作業コードの単項目チェック
    ''' </summary>
    ''' <param name="type">タイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputSagyoChk(ByVal type As LMB020C.SagyoData) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim ctl As Win.InputMan.LMImTextBox = Nothing
        Dim str As String = String.Concat(LMB020C.SAGYO_CD, type.ToString())
        Dim max As Integer = LMB020C.SAGYO_MAX_REC - 1
        For i As Integer = 0 To max

            ctl = Me._G.GetTextControl(String.Concat(str, (i + 1).ToString()))

            '作業コード
            '2017/09/25 修正 李↓
            ctl.ItemName = lgm.Selector({"作業コード", "Working code", "작업코드", "中国語"})
            '2017/09/25 修正 李↑

            ctl.IsForbiddenWordsCheck = True
            ctl.IsFullByteCheck = 5
            ctl.IsMiddleSpace = True
            If MyBase.IsValidateCheck(ctl) = False Then
                Return False
            End If

        Next

        Return True

    End Function
    ''' <summary>
    ''' 作業備考の単項目チェック
    ''' </summary>
    ''' <param name="type">タイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputSagyoRemarkChk(ByVal type As LMB020C.SagyoData) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim ctl As Win.InputMan.LMImTextBox = Nothing
        Dim str As String = String.Concat(LMB020C.SAGYO_RMK_SIJI, type.ToString())
        Dim max As Integer = LMB020C.SAGYO_MAX_REC - 1
        For i As Integer = 0 To max

            ctl = Me._G.GetTextControl(String.Concat(str, (i + 1).ToString()))

            '作業コード
            '2017/09/25 修正 李↓
            ctl.ItemName = lgm.Selector({"作業備考", "Working Remark", "작업 비고", "中国語"})
            '2017/09/25 修正 李↑

            ctl.IsForbiddenWordsCheck = True
            ctl.IsByteCheck = 100
            If MyBase.IsValidateCheck(ctl) = False Then
                Return False
            End If

        Next

        Return True

    End Function
    ''' <summary>
    ''' マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMstExistChk() As Boolean

        With Me._Frm

            '荷主マスタ存在チェック
            Dim rtnResult As Boolean = Me.IsCustExistChk()

            '作業(中)マスタ存在チェック
            'START YANAI 要望番号495
            'rtnResult = rtnResult AndAlso Me.IsSagyoExistChk(LMB020C.SagyoData.M, .tabGoods)
            rtnResult = rtnResult AndAlso Me.IsSagyoExistChk(LMB020C.SagyoData.M, .tabUnso)
            'END YANAI 要望番号495

            '運送会社マスタ存在チェック
            rtnResult = rtnResult AndAlso Me.IsUnsocoExistChk()

            'タリフマスタ存在チェック
            rtnResult = rtnResult AndAlso Me.IsTariffExistChk()

            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
            '支払タリフマスタ存在チェック
            rtnResult = rtnResult AndAlso Me.IsShiharaiTariffExistChk()
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

            '出荷元マスタ存在チェック
            rtnResult = rtnResult AndAlso Me.IsDestExistChk()

            '作業(大)マスタ存在チェック
            'START YANAI 要望番号495
            'rtnResult = rtnResult AndAlso Me.IsSagyoExistChk(LMB020C.SagyoData.L, .tabUnso)
            rtnResult = rtnResult AndAlso Me.IsSagyoExistChk(LMB020C.SagyoData.L)
            'END YANAI 要望番号495

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' マスタ名称取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetMstNm()

        Dim nmFlg As Boolean = True

        With Me._Frm

            '荷主マスタ名称取得
            Call Me.IsCustExistChk(nmFlg)

            '作業(中)マスタ名称取得
            'START YANAI 要望番号495
            'Call Me.IsSagyoExistChk(LMB020C.SagyoData.M, .tabGoods, nmFlg)
            Call Me.IsSagyoExistChk(LMB020C.SagyoData.M, .tabUnso, nmFlg)
            'END YANAI 要望番号495

            '運送会社マスタ名称取得
            Call Me.IsUnsocoExistChk(nmFlg)

            'タリフマスタ名称取得
            Call Me.IsTariffExistChk(nmFlg)

            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
            '支払タリフマスタ名称取得
            Call Me.IsShiharaiTariffExistChk(nmFlg)
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

            '出荷元マスタ名称取得
            Call Me.IsDestExistChk(nmFlg)

            '作業(大)マスタ名称取得
            'START YANAI 要望番号495
            'Call Me.IsSagyoExistChk(LMB020C.SagyoData.L, .tabUnso, nmFlg)
            Call Me.IsSagyoExistChk(LMB020C.SagyoData.L)
            'END YANAI 要望番号495

        End With

    End Sub

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputConnectionChk() As Boolean

        With Me._Frm

            '入荷日 , 起算日の大小チェック
            Dim rtnResult As Boolean = Me.IsInkaDateKisanDateChk()

            'ADD 2017/07/11 
            '入荷日 , 総保入期限の大小チェック
            rtnResult = rtnResult AndAlso Me.IsInkaDateStorageDueDateChk()

            '作業(中)の重複チェック
            'START YANAI 要望番号495
            'rtnResult = rtnResult AndAlso Me.IsSagyoJufukuChk(LMB020C.SagyoData.M, .tabGoods)
            rtnResult = rtnResult AndAlso Me.IsSagyoJufukuChk(LMB020C.SagyoData.M, .tabUnso, True)
            'END YANAI 要望番号495

            '車輌の関連必須チェック
            rtnResult = rtnResult AndAlso Me.IsUnchinKbnChk()

            '出荷元の関連必須チェック
            rtnResult = rtnResult AndAlso Me.IsUnchinUmuChk()

            ''選択したタリフ区分の整合性チェック
            'rtnResult = rtnResult AndAlso Me.IsTariffSetExistChk()

            '入荷日・タリフ適用開始日の大小チェック
            rtnResult = rtnResult AndAlso Me.IsTariffDateNyukaDateChk()

            '作業(大)の重複チェック
            'START YANAI 要望番号495
            'rtnResult = rtnResult AndAlso Me.IsSagyoJufukuChk(LMB020C.SagyoData.L, .tabUnso)
            rtnResult = rtnResult AndAlso Me.IsSagyoJufukuChk(LMB020C.SagyoData.L)
            'END YANAI 要望番号495

            '全期保管料・当期保管料の関連チェック
            rtnResult = rtnResult AndAlso Me.IsHokanUmuChk()

            '出荷止設定可否チェック
            'Del 2019/10/09 要望管理007373  rtnResult = rtnResult AndAlso Me.IsStopAllocValid()     'ADD 2019/08/01 要望管理005237

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 荷主マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsCustExistChk(Optional ByVal nmFlg As Boolean = False) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            '荷主(大)コードの存在チェック
            Dim custL As String = .txtCustCdL.TextValue
            Dim custM As String = .txtCustCdM.TextValue
            Dim drs As DataRow() = Me._Vcon.SelectCustListDataRow(custL, custM)
            If drs.Length < 1 Then
                If nmFlg = False Then
                    .txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.txtCustCdL)
                    'Return Me._Vcon.SetMstErrMessage("荷主マスタ", String.Concat(custL, " - ", custM))

                    '2017/09/25 修正 李↓
                    Return Me._Vcon.SetMstErrMessage(lgm.Selector({"荷主マスタ", "Custmer Master", "하주마스터", "中国語"}), String.Concat(custL, " - ", custM))
                    '2017/09/25 修正 李↑

                Else
                    Return False
                End If
            End If

            '名称を設定
            .lblCustNm.TextValue = String.Concat(drs(0).Item("CUST_NM_L").ToString(), drs(0).Item("CUST_NM_M").ToString())

            '入荷日 , 最終計算日の大小チェック
            Return True

        End With

    End Function

    ''' <summary>
    ''' 作業マスタ存在チェック
    ''' </summary>
    ''' <param name="type">タイプ</param>
    ''' <param name="tabPage">タブページ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSagyoExistChk(ByVal type As LMB020C.SagyoData _
                                     , Optional ByVal tabPage As System.Windows.Forms.TabPage = Nothing _
                                     , Optional ByVal nmFlg As Boolean = False _
                                     ) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim ctl As Win.InputMan.LMImTextBox = Nothing
        Dim value As String = String.Empty
        Dim txtStr As String = String.Concat(LMB020C.SAGYO_CD, type.ToString())
        Dim lblStr As String = String.Concat(LMB020C.SAGYO_NM, type.ToString())
        Dim nrsbrStr As String = Convert.ToString(Me._Frm.cmbEigyo.SelectedValue)
        Dim custStr As String = Me._Frm.txtCustCdL.TextValue
        Dim max As Integer = LMB020C.SAGYO_MAX_REC - 1
        Dim drs As DataRow() = Nothing
        Dim recNo As String = String.Empty
        For i As Integer = 0 To max

            recNo = (i + 1).ToString()
            ctl = Me._G.GetTextControl(String.Concat(txtStr, recNo))

            '作業マスタ存在チェック
            value = ctl.TextValue

            '値がない場合、スルー
            If String.IsNullOrEmpty(value) = True Then
                Continue For
            End If

            '取得できない場合、エラー
            'START YANAI 要望番号376
            'drs = Me._Vcon.SelectSagyoListDataRow(nrsbrStr, value, custStr)
            Dim SelectSagyoString As String = String.Empty
            '削除フラグ
            SelectSagyoString = String.Concat(SelectSagyoString, " SYS_DEL_FLG = '0' ")
            '作業コード
            SelectSagyoString = String.Concat(SelectSagyoString, " AND SAGYO_CD = '", value, "' ")
            '営業所コード
            SelectSagyoString = String.Concat(SelectSagyoString, " AND NRS_BR_CD = '", nrsbrStr, "' ")
            '荷主コード
            SelectSagyoString = String.Concat(SelectSagyoString, " AND (CUST_CD_L = '", custStr, "' OR CUST_CD_L = 'ZZZZZ')")

            drs = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(SelectSagyoString)
            'END YANAI 要望番号376
            If drs.Length < 1 Then
                If nmFlg = False Then
                    'START YANAI 要望番号495
                    'Me._Vcon.SetErrorControl(ctl, Me._Frm.tabMiddle, tabPage)
                    Me._Vcon.SetErrorControl(ctl)
                    'END YANAI 要望番号495
                    'Return Me._Vcon.SetMstErrMessage("作業項目マスタ", value)

                    '2017/09/25 修正 李↓
                    Return Me._Vcon.SetMstErrMessage(lgm.Selector({"作業項目マスタ", "Work item Master", "작업항목마스터", "中国語"}), value)
                    '2017/09/25 修正 李↑

                Else
                    Continue For
                End If
            End If

            'マスタの値を設定
            ctl.TextValue = drs(0).Item("SAGYO_CD").ToString()
            Me._G.GetTextControl(String.Concat(lblStr, recNo)).TextValue = drs(0).Item("SAGYO_RYAK").ToString()

        Next

        Return True

    End Function

    ''' <summary>
    ''' 運送会社マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnsocoExistChk(Optional ByVal nmFlg As Boolean = False) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            '存在チェック前にセット入力チェック
            If Me.IsUnsocoSetChk(.txtUnsoCd, .txtTrnBrCD) = False Then
                Return False
            End If

            '運送会社コードの存在チェック
            Dim unsocoCd As String = .txtUnsoCd.TextValue
            Dim shitenCd As String = .txtTrnBrCD.TextValue

            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
            Dim sShiharaiTariffCD As String = .txtShiharaiTariffCD.TextValue
            '支払タリフが入力　かつ　運送会社コードが未入力の場合はエラー
            If String.IsNullOrEmpty(sShiharaiTariffCD) = False AndAlso _
               String.IsNullOrEmpty(unsocoCd) = True Then

                .txtTrnBrCD.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._Vcon.SetErrorControl(.txtUnsoCd)
                Return Me._Vcon.SetErrMessage("E790")
                'Return Me._Vcon.SetErrMessage("E001", New String() {"支払タリフが入力されている場合、運送会社コード"})
                Return False
            End If
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

            '値がない場合、スルー
            If String.IsNullOrEmpty(unsocoCd) = True _
                OrElse String.IsNullOrEmpty(shitenCd) = True Then
                Return True
            End If

            Dim drs As DataRow() = Me._Vcon.SelectUnsocoListDataRow(unsocoCd, shitenCd)

            '取得できない場合、エラー
            If drs.Length < 1 Then
                If nmFlg = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    .txtTrnBrCD.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    'START YANAI 要望番号495
                    'Me._Vcon.SetErrorControl(.txtUnsoCd, .tabMiddle, .tabUnso)
                    Me._Vcon.SetErrorControl(.txtUnsoCd)
                    'END YANAI 要望番号495

                    '2017/09/25 修正 李↓
                    Return Me._Vcon.SetMstErrMessage(lgm.Selector({"運送会社マスタ", "Shipping company Master", "운송회사마스터", "中国語"}), String.Concat(unsocoCd, " - ", shitenCd))
                    '2017/09/25 修正 李↑

                Else
                    Return False
                End If
            End If

            'マスタの値を設定
            .txtUnsoCd.TextValue = drs(0).Item("UNSOCO_CD").ToString()
            .txtTrnBrCD.TextValue = drs(0).Item("UNSOCO_BR_CD").ToString()
            Dim unsoNm As String = drs(0).Item("UNSOCO_NM").ToString()
            Dim unsoBrNm As String = drs(0).Item("UNSOCO_BR_NM").ToString()
            .lblTrnNM.TextValue = String.Concat(unsoNm, unsoBrNm)
            .lblUnsoNm.TextValue = unsoNm
            .lblUnsoBrNm.TextValue = unsoBrNm
            .lblTareYn.TextValue = drs(0).Item("TARE_YN").ToString()

            Return True

        End With

    End Function

    ''' <summary>
    ''' 運送会社のセット入力チェック
    ''' </summary>
    ''' <param name="compCd">運送会社コード</param>
    ''' <param name="sitenCd">運送会社支店コード</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnsocoSetChk(ByVal compCd As Win.InputMan.LMImTextBox _
                                          , ByVal sitenCd As Win.InputMan.LMImTextBox) As Boolean


        With Me._Frm

            Dim unsocoCd As String = compCd.TextValue
            Dim unsocoBrCd As String = sitenCd.TextValue

            '両方に値がない場合、スルー
            If String.IsNullOrEmpty(unsocoCd) = True _
                AndAlso String.IsNullOrEmpty(unsocoBrCd) = True Then
                Return True
            End If

            '両方に値がある場合、スルー
            If String.IsNullOrEmpty(unsocoCd) = False _
                AndAlso String.IsNullOrEmpty(unsocoBrCd) = False Then
                Return True
            End If

            '片方に値がある場合、エラー            
            Dim errorControl As Control() = New Control() {compCd, sitenCd}
            Call Me._Vcon.SetErrorControl(errorControl, compCd)
            'Return Me._Vcon.SetErrMessage("E017", New String() {String.Concat(.lblTitleUnsoco.Text, "コード"), String.Concat(.lblTitleUnsoco.Text, "支店コード")})
            '20151029 tsunehira add Start
            '英語化対応
            Return Me._Vcon.SetErrMessage("E791", New String() {.lblTitleUnsoco.Text.ToString, .lblTitleUnsoco.Text.ToString})
            '2015.10.29 tusnehira add End

        End With

    End Function

    ''' <summary>
    ''' タリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks>計算コードチェックを一緒に行う。</remarks>
    Private Function IsTariffExistChk(Optional ByVal nmFlg As Boolean = False) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            Dim rtnResult As Boolean = True

            '日陸手配以外、スルー
            If LMB020C.TEHAI_NRS.Equals(.cmbUnchinUmu.SelectedValue.ToString()) = False Then
                Return rtnResult
            End If

            'タリフコードの存在チェック
            Dim tariffCd As String = .txtUnsoTariffCD.TextValue

            '値がない場合、スルー
            If String.IsNullOrEmpty(tariffCd) = True Then
                Return rtnResult
            End If

            Dim drs As DataRow() = Nothing
            Dim lblColNm As String = String.Empty
            Dim txtColNm As String = String.Empty

            '運賃の場合
            If LMB020C.TARIFF_YOKO.Equals(.cmbUnchinKbn.SelectedValue.ToString()) = True Then

                '横持ちタリフマスタの存在チェック
                drs = Me._Vcon.SelectYokoTariffListDataRow(.cmbEigyo.SelectedValue.ToString(), tariffCd)
                If drs.Length < 1 Then
                    If nmFlg = False Then
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        'START YANAI 要望番号495
                        'Me._Vcon.SetErrorControl(.txtUnsoTariffCD, .tabMiddle, .tabUnso)
                        Me._Vcon.SetErrorControl(.txtUnsoTariffCD)
                        'END YANAI 要望番号495

                        '2017/09/25 修正 李↓
                        Return Me._Vcon.SetMstErrMessage(lgm.Selector({"横持ちタリフマスタ", "tariff master of drayage", "경유관세마스터", "中国語"}), tariffCd)
                        '2017/09/25 修正 李↑

                    Else
                        Return False
                    End If
                End If

                txtColNm = "YOKO_TARIFF_CD"
                lblColNm = "YOKO_REM"

                '横持ちと車輌区分とのチェック
                rtnResult = Me.IsYokoSharyoChk(drs(0))

            Else

                '運賃タリフマスタの存在チェック
                drs = Me._Vcon.SelectUnchinTariffListDataRow(tariffCd)
                If drs.Length < 1 Then
                    If nmFlg = False Then
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        'START YANAI 要望番号495
                        'Me._Vcon.SetErrorControl(.txtUnsoTariffCD, .tabMiddle, .tabUnso)
                        Me._Vcon.SetErrorControl(.txtUnsoTariffCD)
                        'END YANAI 要望番号495

                        '2017/09/25 修正 李↓
                        Return Me._Vcon.SetMstErrMessage(lgm.Selector({"運賃タリフマスタ", "tariff master of fare", "운임관세마스터", "中国語"}), tariffCd)
                        '2017/09/25 修正 李↑

                    Else
                        Return False
                    End If
                End If

                txtColNm = "UNCHIN_TARIFF_CD"
                lblColNm = "UNCHIN_TARIFF_REM"

                If nmFlg = False Then
                    '宅配便の場合、エラー
                    If LMB020C.TABLE_TYPE_TAK.Equals(drs(0).Item("TABLE_TP").ToString()) = True Then
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495

                        '20151029 tsunehira add Start
                        '英語化対応
                        Return Me._Vcon.SetErrMessage("E792")
                        '2015.10.29 tusnehira add End
                        'Return Me._Vcon.SetErrMessage("E224", New String() {"タリフコード", "宅配便以外"})
                    End If
                End If

            End If

            'マスタの値を設定
            .txtUnsoTariffCD.TextValue = drs(0).Item(txtColNm).ToString()
            .lblUnsoTariffNM.TextValue = drs(0).Item(lblColNm).ToString()

            '計算コードチェックエラー
            Return rtnResult

        End With

    End Function


    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
    ''' <summary>
    ''' 支払タリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks>計算コードチェックを一緒に行う。</remarks>
    Private Function IsShiharaiTariffExistChk(Optional ByVal nmFlg As Boolean = False) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            Dim rtnResult As Boolean = True

            '日陸手配以外、スルー
            If LMB020C.TEHAI_NRS.Equals(.cmbUnchinUmu.SelectedValue.ToString()) = False Then
                Return rtnResult
            End If

            'タリフコードの存在チェック
            Dim tariffCd As String = .txtShiharaiTariffCD.TextValue

            '値がない場合、スルー
            If String.IsNullOrEmpty(tariffCd) = True Then
                Return rtnResult
            End If

            Dim drs As DataRow() = Nothing
            Dim lblColNm As String = String.Empty
            Dim txtColNm As String = String.Empty

            '運賃の場合
            If LMB020C.TARIFF_YOKO.Equals(.cmbUnchinKbn.SelectedValue.ToString()) = True Then

                '横持ちタリフマスタの存在チェック
                drs = Me._Vcon.SelectShiharaiYokoTariffListDataRow(.cmbEigyo.SelectedValue.ToString(), tariffCd)
                If drs.Length < 1 Then
                    If nmFlg = False Then
                        Me._Vcon.SetErrorControl(.txtShiharaiTariffCD)

                        '2017/09/25 修正 李↓
                        Return Me._Vcon.SetMstErrMessage(lgm.Selector({"支払横持ちタリフマスタ", "tariff master of payment drayage", "지불경유관세마스터", "中国語"}), tariffCd)
                        '2017/09/25 修正 李↑

                    Else
                        Return False
                    End If
                End If

                txtColNm = "YOKO_TARIFF_CD"
                lblColNm = "YOKO_REM"

                '支払の場合、関係ない？
                ''横持ちと車輌区分とのチェック
                'rtnResult = Me.IsYokoSharyoChk(drs(0))

            Else

                '運賃タリフマスタの存在チェック
                drs = Me._Vcon.SelectShiharaiTariffListDataRow(tariffCd)
                If drs.Length < 1 Then
                    If nmFlg = False Then
                        Me._Vcon.SetErrorControl(.txtShiharaiTariffCD)

                        '2017/09/25 修正 李↓
                        Return Me._Vcon.SetMstErrMessage(lgm.Selector({"支払タリフマスタ", "tariff master of payment fare.", "지불관세마스터", "中国語"}), tariffCd)
                        '2017/09/25 修正 李↑

                    Else
                        Return False
                    End If
                End If

                txtColNm = "SHIHARAI_TARIFF_CD"
                lblColNm = "SHIHARAI_TARIFF_REM"

                If nmFlg = False Then
                    '宅配便の場合、エラー
                    If LMB020C.TABLE_TYPE_TAK.Equals(drs(0).Item("TABLE_TP").ToString()) = True Then
                        '20151029 tsunehira add Start
                        '英語化対応
                        Return Me._Vcon.SetErrMessage("793")
                        '2015.10.29 tusnehira add End
                        'Return Me._Vcon.SetErrMessage("E224", New String() {"支払タリフコード", "宅配便以外"})
                    End If
                End If

            End If

            'マスタの値を設定
            .txtShiharaiTariffCD.TextValue = drs(0).Item(txtColNm).ToString()
            .lblShiharaiTariffNM.TextValue = drs(0).Item(lblColNm).ToString()

            '計算コードチェックエラー
            Return rtnResult

        End With

    End Function
    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End


    ''' <summary>
    ''' 届先マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDestExistChk(Optional ByVal nmFlg As Boolean = False) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            Dim spr As LMSpread = .sprDetail

            '届先コードの存在チェック
            Dim destCd As String = .txtShukkaMotoCD.TextValue

            '値がない場合、スルー
            If String.IsNullOrEmpty(destCd) = True Then
                Return True
            End If

            Dim drs As DataRow() = Me._Vcon.SelectDestListDataRow(.cmbEigyo.SelectedValue.ToString(), .txtCustCdL.TextValue, destCd)

            '取得できない場合、エラー
            If drs.Length < 1 Then
                If nmFlg = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    'START YANAI 要望番号495
                    'Me._Vcon.SetErrorControl(.txtShukkaMotoCD, .tabMiddle, .tabUnso)
                    Me._Vcon.SetErrorControl(.txtShukkaMotoCD)
                    'END YANAI 要望番号495

                    '2017/09/25 修正 李↓
                    Return Me._Vcon.SetMstErrMessage(lgm.Selector({"届先マスタ", "master of delivery adress", "송달처마스터", "中国語"}), destCd)
                    '2017/09/25 修正 李↑

                Else
                    Return False
                End If
            End If

            'マスタの値を設定
            .txtShukkaMotoCD.TextValue = drs(0).Item("DEST_CD").ToString()
            .lblShukkaMotoNM.TextValue = drs(0).Item("DEST_NM").ToString()

            Return True

        End With

    End Function

    ''' <summary>
    ''' 届先マスタ存在チェック
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDestExistChk(ByVal rowNo As Integer, Optional ByVal nmFlg As Boolean = False) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            Dim spr As LMSpread = .sprDetail

            '届先コードの存在チェック
            Dim destCd As String = Me._Vcon.GetCellValue(spr.ActiveSheet.Cells(rowNo, LMB020G.sprDetailDef.DEST_CD.ColNo))

            '値がない場合、スルー
            If String.IsNullOrEmpty(destCd) = True Then
                Return True
            End If

            Dim drs As DataRow() = Me._Vcon.SelectDestListDataRow(.cmbEigyo.SelectedValue.ToString(), .txtCustCdL.TextValue, destCd)

            '取得できない場合、エラー
            If drs.Length < 1 Then
                If nmFlg = False Then
                    Me._Vcon.SetErrorControl(spr, rowNo, LMB020G.sprDetailDef.DEST_CD.ColNo)

                    '2017/09/25 修正 李↓
                    Return Me._Vcon.SetMstErrMessage(lgm.Selector({"届先マスタ", "master of delivery adress", "송달처마스터", "中国語"}), destCd)
                    '2017/09/25 修正 李↑

                Else
                    Return False
                End If
            End If

            'マスタの値を設定
            spr.SetCellValue(rowNo, LMB020G.sprDetailDef.DEST_CD.ColNo, drs(0).Item("DEST_CD").ToString())
            spr.SetCellValue(rowNo, LMB020G.sprDetailDef.DEST_NM.ColNo, drs(0).Item("DEST_NM").ToString())

            Return True

        End With

    End Function

    ''' <summary>
    ''' 横持ち車輌チェック
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsYokoSharyoChk(ByVal dr As DataRow) As Boolean

        '車建ての場合、
        If LMB020C.CALC_KB_KURUMA.Equals(dr.Item("CALC_KB").ToString()) = True Then

            '車輌区分がない場合、エラー
            'Return Me.IsYokoSharyoChk("E187", "車建て", "車輌区分", True)

            '20151029 tsunehira add Start
            '英語化対応
            Return Me.IsYokoSharyoChk("E797", "", "", True)

            '2015.10.29 tusnehira add End
        End If

        '車輌区分がある場合、エラー
        Return Me.IsYokoSharyoChk("E798", "", "", False)

    End Function

    ''' <summary>
    ''' 横持ち車輌チェック
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <param name="msg1">置換文字1</param>
    ''' <param name="msg2">置換文字2</param>
    ''' <param name="hissuFlg">必須フラグ　True:ない場合、エラー　False:ある場合、エラー</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsYokoSharyoChk(ByVal id As String, ByVal msg1 As String, ByVal msg2 As String, ByVal hissuFlg As Boolean) As Boolean

        '値がある(ない)場合、エラー
        If String.IsNullOrEmpty(Me._Frm.cmbSharyoKbn.SelectedValue.ToString()) = hissuFlg Then

            Me._Frm.txtUnsoTariffCD.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()

            'START YANAI 要望番号495
            'Me._Vcon.SetErrorControl(Me._Frm.cmbSharyoKbn, Me._Frm.tabMiddle, Me._Frm.tabUnso)
            Me._Vcon.SetErrorControl(Me._Frm.cmbSharyoKbn)
            'END YANAI 要望番号495
            Return Me._Vcon.SetErrMessage(id, New String() {msg1, msg2})

        End If

        Return True

    End Function

    'START YANAI 要望番号495
    '''' <summary>
    '''' 作業コードの重複チェック
    '''' </summary>
    '''' <param name="type">タイプ</param>
    '''' <param name="tabPage">タブページ</param>
    '''' <returns>True:エラーなし,OK False:エラーあり</returns>
    '''' <remarks></remarks>
    'Private Function IsSagyoJufukuChk(ByVal type As LMB020C.SagyoData, ByVal tabPage As System.Windows.Forms.TabPage ) As Boolean
    ''' <summary>
    ''' 作業コードの重複チェック
    ''' </summary>
    ''' <param name="type">タイプ</param>
    ''' <param name="tabPage">タブページ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSagyoJufukuChk(ByVal type As LMB020C.SagyoData _
                                     , Optional ByVal tabPage As System.Windows.Forms.TabPage = Nothing _
                                     , Optional ByVal nmFlg As Boolean = False _
                                     ) As Boolean
        'END YANAI 要望番号495

        With Me._Frm

            Dim ctl As Win.InputMan.LMImTextBox = Nothing
            Dim ctl2 As Win.InputMan.LMImTextBox = Nothing
            Dim str As String = String.Concat(LMB020C.SAGYO_CD, type.ToString())
            Dim max As Integer = LMB020C.SAGYO_MAX_REC - 1
            Dim value As String = String.Empty
            Dim sagyoCd As String = String.Empty
            For i As Integer = 0 To max

                ctl = Me._G.GetTextControl(String.Concat(str, (i + 1).ToString()))
                sagyoCd = ctl.TextValue

                '作業コードがない場合、スルー
                If String.IsNullOrEmpty(sagyoCd) = True Then
                    Continue For
                End If

                value = String.Empty
                For j As Integer = i + 1 To max

                    ctl2 = Me._G.GetTextControl(String.Concat(str, (j + 1).ToString()))
                    value = ctl2.TextValue

                    '比較対象データが空文字 且つ 作業コードが設定済みの場合、値設定

                    '比較対象データと値が同じ場合、エラー
                    If value.Equals(sagyoCd) = True Then
                        'START YANAI 要望番号495
                        'Me._Vcon.SetErrorControl(ctl, .tabMiddle, tabPage)
                        'Me._Vcon.SetErrorControl(ctl2, .tabMiddle, tabPage)
                        If nmFlg = True Then
                            Me._Vcon.SetErrorControl(ctl, .tabMiddle, tabPage)
                            Me._Vcon.SetErrorControl(ctl2, .tabMiddle, tabPage)
                        Else
                            Me._Vcon.SetErrorControl(ctl)
                            Me._Vcon.SetErrorControl(ctl2)
                        End If
                        'END YANAI 要望番号495
                        Return Me._Vcon.SetErrMessage("E131")
                    End If

                Next

            Next

            Return True

        End With

    End Function

    ''' <summary>
    ''' 入荷日 , 起算日の大小チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInkaDateKisanDateChk() As Boolean

        With Me._Frm

            '保管料起算日 < 入荷日の時、エラー
            If Me.IsLargeSmallChk(.imdHokanStrDate.TextValue, .imdNyukaDate.TextValue, False) = False Then

                '起算日修正時は別メッセージ
                If LMB020C.ActionType.DATEEDIT.ToString().Equals(.lblEdit.TextValue) = True Then

                    .imdHokanStrDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.imdHokanStrDate)
                    Return Me._Vcon.SetErrMessage("E039", New String() {.lblTitleHokanStrDate.Text, .lblTitleNyukaDate.Text})


                    'START YANAI 運送・運行・請求メモNo.44
                ElseIf .imdNyukaDate.Enabled = True Then
                    .imdHokanStrDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.imdHokanStrDate)
                    Return Me._Vcon.SetErrMessage("E039", New String() {.lblTitleHokanStrDate.Text, .lblTitleNyukaDate.Text})
                    'END YANAI 運送・運行・請求メモNo.44

                Else

                    .imdHokanStrDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.imdNyukaDate)
                    Return Me._Vcon.SetErrMessage("E134")

                End If

            End If

        End With

        Return True

    End Function


    ''' <summary>
    ''' 入荷日 , 総保入期限大小チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInkaDateStorageDueDateChk() As Boolean

        With Me._Frm

            '総保入期限 < 入荷日の時、エラー
            If Me.IsLargeSmallChk(.imdStorageDueDate.TextValue, .imdNyukaDate.TextValue, False) = False Then

                .imdStorageDueDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._Vcon.SetErrorControl(.imdStorageDueDate)
                Return Me._Vcon.SetErrMessage("E541", New String() {"総保入期限", "入荷日より未来の日付を指定して下さい。"})
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 全期保管料・当期保管料の関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHokanUmuChk() As Boolean

        With Me._Frm

            '全期保管料が"無"の時、当期保管料が"無"以外だったらエラー
            If ("00").Equals(.cmbZenkiHokanUmu.SelectedValue) = True Then
                If ("00").Equals(.cmbToukiHokanUmu.SelectedValue) = False Then
                    .cmbZenkiHokanUmu.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.cmbToukiHokanUmu)
                    Return Me._Vcon.SetErrMessage("E328")
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 入目の範囲チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsIrimeHaniChk(ByVal rowNo As Integer) As Boolean

        With Me._Frm

            Dim spr As LMSpread = .sprDetail

            '0.001～999999.999以外の時、エラー
            Dim irime As Decimal = Convert.ToDecimal(Me._Vcon.GetCellValue(spr.ActiveSheet.Cells(rowNo, LMB020G.sprDetailDef.IRIME.ColNo)))

            If Me.IsLargeSmallChk(irime, Convert.ToDecimal(LMB020C.IRIME_MIN), False) = False Then
                Me._Vcon.SetErrorControl(spr, rowNo, LMB020G.sprDetailDef.IRIME.ColNo)
                Me._Vcon.SetErrMessage("E014", New String() {.lblTitleIrisu.TextValue, LMB020C.IRIME_MIN, LMB020C.IRIME_MAX})
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 大小チェック
    ''' </summary>
    ''' <param name="large">大きい方の値</param>
    ''' <param name="small">小さい方の値</param>
    ''' <param name="equalFlg">イコールがエラーの場合：True　イコールがエラーではないの場合：False</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Overloads Function IsLargeSmallChk(ByVal large As String, ByVal small As String, ByVal equalFlg As Boolean) As Boolean

        '値がない場合、スルー
        If String.IsNullOrEmpty(large) = True OrElse _
           String.IsNullOrEmpty(small) = True _
           Then
            Return True
        End If

        '大小比較
        Return Me.IsLargeSmallChk(Convert.ToDecimal(Me._Gcon.FormatNumValue(large)), Convert.ToDecimal(Me._Gcon.FormatNumValue(small)), equalFlg)

    End Function

    ''' <summary>
    ''' 大小チェック
    ''' </summary>
    ''' <param name="large">大きい方の値</param>
    ''' <param name="small">小さい方の値</param>
    ''' <param name="equalFlg">イコールがエラーの場合：True　イコールがエラーではないの場合：False</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Overloads Function IsLargeSmallChk(ByVal large As Decimal, ByVal small As Decimal, ByVal equalFlg As Boolean) As Boolean

        '大小比較
        If equalFlg = True Then

            If large <= small Then
                Return False
            End If

        Else

            If large < small Then
                Return False
            End If

        End If

        Return True

    End Function

    ''' <summary>
    ''' 入荷(小)の関連チェック(画面の値)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSprDetailChk() As Boolean

        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim sum As Decimal = 0
        Dim kosu As Decimal = 0
        Dim irisu As Decimal = Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Frm.numIrisu.TextValue))

        With spr.ActiveSheet

            Dim max As Integer = .Rows.Count - 1
            For i As Integer = 0 To max

                '届先コードのマスタ存在チェック
                If Me.IsDestExistChk(i) = False Then
                    Return False
                End If

                '入数 , 端数チェック
                If Me.IsHasuIrisuChk(irisu _
                                     , Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Vcon.GetCellValue(.Cells(i, LMB020G.sprDetailDef.NB.ColNo)))) _
                                     , Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Vcon.GetCellValue(.Cells(i, LMB020G.sprDetailDef.HASU.ColNo)))) _
                                     , i) = False Then
                    Return False
                End If

                '個数のゼロチェック
                kosu = Convert.ToDecimal(Me._Vcon.GetCellValue(.Cells(i, LMB020G.sprDetailDef.SUM.ColNo)))
                If Me.IsZeroChk(kosu) = False Then

                    '入数 <> 1の場合、エラー背景色
                    If 1 <> Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Frm.numIrisu.Value.ToString())) Then
                        .Cells(i, LMB020G.sprDetailDef.HASU.ColNo).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                    End If
                    Me._Vcon.SetErrorControl(spr, i, LMB020G.sprDetailDef.NB.ColNo)
                    Return Me._Vcon.SetErrMessage("E120", New String() {(i + 1).ToString(), LMB020G.sprDetailDef.SUM.ColName})
                End If

                '賞味期限の関連必須
                If ("01").Equals(Me._Vcon.GetCellValue(.Cells(i, LMB020G.sprDetailDef.LT_DATE_CTL_KB.ColNo))) = True _
                    AndAlso String.IsNullOrEmpty(Me._Vcon.GetCellValue(.Cells(i, LMB020G.sprDetailDef.LT_DATE.ColNo))) = True _
                    Then
                    Me._Vcon.SetErrorControl(spr, i, LMB020G.sprDetailDef.LT_DATE.ColNo)
                    Return Me._Vcon.SetErrMessage("E001", New String() {LMB020G.sprDetailDef.LT_DATE.ColName})
                End If

                '製造日の関連必須
                If ("01").Equals(Me._Vcon.GetCellValue(.Cells(i, LMB020G.sprDetailDef.CRT_DATE_CTL_KB.ColNo))) = True _
                    AndAlso String.IsNullOrEmpty(Me._Vcon.GetCellValue(.Cells(i, LMB020G.sprDetailDef.GOODS_CRT_DATE.ColNo))) = True _
                    Then
                    Me._Vcon.SetErrorControl(spr, i, LMB020G.sprDetailDef.GOODS_CRT_DATE.ColNo)
                    Return Me._Vcon.SetErrMessage("E001", New String() {LMB020G.sprDetailDef.GOODS_CRT_DATE.ColName})
                End If

                '明細の個数を合計
                sum += kosu

                'ロット管理レベルが"00"(有り)の場合、スルー
                If ("00").Equals(Me._Vcon.GetCellValue(.Cells(i, LMB020G.sprDetailDef.LOT_CTL_KB.ColNo))) = True Then
                    Continue For
                End If

                'LotNoの必須チェック
                If String.IsNullOrEmpty(Me._Vcon.GetCellValue(.Cells(i, LMB020G.sprDetailDef.LOT_NO.ColNo))) = True Then
                    Me._Vcon.SetErrorControl(spr, i, LMB020G.sprDetailDef.LOT_NO.ColNo)
                    Return Me._Vcon.SetErrMessage("E001", New String() {LMB020G.sprDetailDef.LOT_NO.ColName})
                End If

            Next

        End With

        '入荷(小)の値を設定
        Me._Frm.numSumCnt.Value = sum

        Return True

    End Function

    ''' <summary>
    ''' 端数 , 入数の関連チェック
    ''' </summary>
    ''' <param name="irisu">入数</param>
    ''' <param name="konsu">梱数</param>
    ''' <param name="hasu">端数</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsHasuIrisuChk(ByVal irisu As Decimal, ByVal konsu As Decimal, ByVal hasu As Decimal, ByVal rowNo As Integer) As Boolean

        Dim spr As LMSpread = Me._Frm.sprDetail
        With spr.ActiveSheet

            '梱数がゼロの以外の場合、チェックを行う
            If 0 <> konsu Then

                '大小チェック
                If Me.IsLargeSmallChk(irisu, hasu, True) = False Then
                    Me._Vcon.SetErrorControl(spr, rowNo, LMB020G.sprDetailDef.HASU.ColNo)
                    Return Me._Vcon.SetErrMessage("E218")
                End If

            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' ゼロチェック
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsZeroChk(ByVal value As Decimal) As Boolean

        If 0 = value Then

            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' 車輌の関連必須チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnchinKbnChk() As Boolean

        With Me._Frm

            '車扱でない場合、スルー
            If LMB020C.TARIFF_KURUMA.Equals(.cmbUnchinKbn.SelectedValue.ToString()) = False Then
                Return True
            End If

            '車輌が未設定の場合、エラー
            If String.IsNullOrEmpty(.cmbSharyoKbn.SelectedValue.ToString()) = True Then
                'START YANAI 要望番号495
                '.tabMiddle.SelectTab(.tabUnso)
                'END YANAI 要望番号495
                'START YANAI 要望番号495
                'Me._Vcon.SetErrorControl(.cmbSharyoKbn, .tabMiddle, .tabUnso)
                Me._Vcon.SetErrorControl(.cmbSharyoKbn)
                'END YANAI 要望番号495
                Return Me._Vcon.SetErrMessage("E149")
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 出荷元の関連必須チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnchinUmuChk() As Boolean

        With Me._Frm

            '日陸手配以外の場合、スルー
            If LMB020C.TEHAI_NRS.Equals(.cmbUnchinUmu.SelectedValue.ToString()) = False Then
                Return True
            End If

            '出荷元コードが未設定の場合、エラー
            If String.IsNullOrEmpty(.txtShukkaMotoCD.TextValue) = True Then
                'START YANAI 要望番号495
                '.tabMiddle.SelectTab(.tabUnso)
                'END YANAI 要望番号495
                'START YANAI 要望番号495
                'Me._Vcon.SetErrorControl(.txtShukkaMotoCD, .tabMiddle, .tabUnso)
                Me._Vcon.SetErrorControl(.txtShukkaMotoCD)
                'END YANAI 要望番号495
                Return Me._Vcon.SetErrMessage("E150")
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 入荷日・タリフ適用開始日の大小チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTariffDateNyukaDateChk() As Boolean

        With Me._Frm

            '日陸手配以外、スルー
            If LMB020C.TEHAI_NRS.Equals(.cmbUnchinUmu.SelectedValue.ToString()) = False Then
                Return True
            End If

            '起算日修正の場合、スルー
            If LMB020C.ActionType.DATEEDIT.ToString().Equals(.lblEdit.TextValue) = True Then
                Return True
            End If

            '未入力の場合、スルー
            If String.IsNullOrEmpty(.txtUnsoTariffCD.TextValue) = True Then
                Return True
            End If

            Select Case .cmbUnchinKbn.SelectedValue.ToString()

                Case String.Empty, LMB020C.TARIFF_ROSEN, LMB020C.TARIFF_YOKO
                Case Else

                    '空値,横持ち、路線以外、チェック
                    '大小チェック
                    Dim mTariff As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF).Select(String.Concat("UNCHIN_TARIFF_CD = '", .txtUnsoTariffCD.TextValue, "'"))

                    If Me.IsLargeSmallChk(.imdNyukaDate.TextValue, mTariff(0).Item("STR_DATE").ToString, False) = False Then
                        Me._Vcon.SetErrorControl(.imdNyukaDate)
                        '20151029 tsunehira add Start
                        '英語化対応
                        Return Me._Vcon.SetErrMessage("E794")
                        '2015.10.29 tusnehira add End
                        'Return Me._Vcon.SetErrMessage("E039", New String() {"入荷日", "運賃タリフの適用開始日"})
                    End If
            End Select

            Return True

        End With

    End Function

    ''' <summary>
    ''' タリフコードの関連必須チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTariffHissuChk() As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            '日陸手配以外、スルー
            If LMB020C.TEHAI_NRS.Equals(.cmbUnchinUmu.SelectedValue.ToString()) = False Then
                Return True
            End If

            '起算日修正の場合、スルー
            If LMB020C.ActionType.DATEEDIT.ToString().Equals(.lblEdit.TextValue) = True Then
                Return True
            End If

            Select Case .cmbUnchinKbn.SelectedValue.ToString()

                Case String.Empty, LMB020C.TARIFF_ROSEN
                Case Else

                    '空値,路線以外、タリフ必須(ワーニング)
                    If String.IsNullOrEmpty(.txtUnsoTariffCD.TextValue) = True Then

                        '2017/09/25 修正 李↓
                        Dim msg As String = String.Empty
                        msg = lgm.Selector({"タリフコード", "tariff code", "관세코드", "中国語"})
                        '2017/09/25 修正 李↑

                        If MyBase.ShowMessage("W139", New String() {msg}) <> MsgBoxResult.Ok Then
                            'If MyBase.ShowMessage("W139", New String() {"タリフコード"}) <> MsgBoxResult.Ok Then
                            'START YANAI 要望番号495
                            '.tabMiddle.SelectTab(.tabUnso)
                            'END YANAI 要望番号495
                            'START YANAI 要望番号495
                            'Me._Vcon.SetErrorControl(.txtUnsoTariffCD, .tabMiddle, .tabUnso)
                            Me._Vcon.SetErrorControl(.txtUnsoTariffCD)
                            'END YANAI 要望番号495
                            Return False
                        End If
                    End If

            End Select

            Return True

        End With

    End Function

    ''' <summary>
    ''' エラー情報を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="colNo">列番号</param>
    ''' <remarks></remarks>
    Private Sub SetErrData(ByVal ds As DataSet, ByVal dr As DataRow, ByVal colNo As Integer, Optional ByVal recSFlg As Boolean = True)

        Dim dt As DataTable = ds.Tables(LMB020C.TABLE_NM_ERR)
        Dim row As DataRow = ds.Tables(LMB020C.TABLE_NM_ERR).NewRow
        row("INKA_L_NO") = dr.Item("INKA_NO_L")
        row("INKA_M_NO") = dr.Item("INKA_NO_M")
        If recSFlg = True Then row("INKA_S_NO") = dr.Item("INKA_NO_S")
        row("COLNO") = colNo

        dt.Rows.Add(row)

    End Sub

    ''' <summary>
    ''' 単位混在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="tani">単位</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTaniMixChk(ByVal ds As DataSet, ByVal dr As DataRow, ByVal tani As String) As Boolean

        '別の単位がある場合、エラー
        If tani.Equals(dr.Item("STD_IRIME_UT").ToString()) = False Then
            Call Me.SetErrData(ds, dr, LMB020G.sprDetailDef.TANI.ColNo)
            Return Me._Vcon.SetErrMessage("E140")
        End If

        Return True

    End Function

    'START YANAI 要望番号433
    '''' <summary>
    '''' 棟・室・ゾーンのチェック
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <param name="dr">DataRow</param>
    '''' <param name="brCd">営業所コード</param>
    '''' <param name="whCd">倉庫コード</param>
    '''' <returns>True:エラーなし,OK False:エラーあり</returns>
    '''' <remarks></remarks>
    'Private Function IsLocationChk(ByVal ds As DataSet, ByVal dr As DataRow, ByVal brCd As String, ByVal whCd As String) As Boolean
    ''' <summary>
    ''' 棟・室・ゾーンのチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="whCd">倉庫コード</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsLocationChk(ByVal ds As DataSet, ByVal dr As DataRow, ByVal brCd As String, ByVal whCd As String, _
                                   ByVal hissuChk As Boolean, ByVal hissuChk2 As Boolean) As Boolean
        'END YANAI 要望番号433

        '棟の必須チェック
        Dim touNo As String = dr.Item("TOU_NO").ToString()
        '要望番号2126 追加START 2013.11.08
        Dim sysDelFlg As String = dr.Item("SYS_DEL_FLG").ToString()
        '要望番号2126 追加END 2013.11.08
        'START YANAI 要望番号433
        'If String.IsNullOrEmpty(touNo) = True Then
        '要望番号2126 修正START 2013.11.08 削除Fを条件に追加
        'If String.IsNullOrEmpty(touNo) = True AndAlso _
        '    (hissuChk = True OrElse _
        '     hissuChk2 = True) Then
        If String.IsNullOrEmpty(touNo) = True AndAlso _
             (hissuChk = True OrElse _
              hissuChk2 = True) AndAlso _
           sysDelFlg.Equals("1") = False Then
            '要望番号2126 修正END 2013.11.08
            'END YANAI 要望番号433
            Call Me.SetErrData(ds, dr, LMB020G.sprDetailDef.TOU_NO.ColNo)
            Return Me._Vcon.SetErrMessage("E112")
        End If

        '室の必須チェック
        Dim situNo As String = dr.Item("SITU_NO").ToString()
        'START YANAI 要望番号433
        'If String.IsNullOrEmpty(situNo) = True Then
        ''要望番号2126 修正START 2013.11.08 削除Fを条件に追加
        'If String.IsNullOrEmpty(situNo) = True AndAlso _
        '    (hissuChk = True OrElse _
        '     hissuChk2 = True) Then
        If String.IsNullOrEmpty(situNo) = True AndAlso _
             (hissuChk = True OrElse _
              hissuChk2 = True) AndAlso _
           sysDelFlg.Equals("1") = False Then
            'END YANAI 要望番号433
            '要望番号2126 修正END 2013.11.08
            Call Me.SetErrData(ds, dr, LMB020G.sprDetailDef.SHITSU_NO.ColNo)
            Return Me._Vcon.SetErrMessage("E112")
        End If

        'ZONEの必須チェック
        Dim zoneCd As String = dr.Item("ZONE_CD").ToString()
        'START YANAI 要望番号433
        'If String.IsNullOrEmpty(zoneCd) = True Then
        '要望番号2126 修正START 2013.11.08 削除Fを条件に追加
        'If String.IsNullOrEmpty(zoneCd) = True AndAlso _
        '    hissuChk2 = True Then
        If String.IsNullOrEmpty(zoneCd) = True AndAlso _
            hissuChk2 = True AndAlso _
           sysDelFlg.Equals("1") = False Then
            'END YANAI 要望番号433
            '要望番号2126 修正END 2013.11.08
            Call Me.SetErrData(ds, dr, LMB020G.sprDetailDef.ZONE_CD.ColNo)
            Return Me._Vcon.SetErrMessage("E112")
        End If

        Return True

    End Function

    ''' <summary>
    ''' 棟・室・ゾーンのチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="whCd">倉庫コード</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTouSituZoneChk(ByVal ds As DataSet, ByVal dr As DataRow, ByVal brCd As String, ByVal whCd As String) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim touNo As String = dr.Item("TOU_NO").ToString()
        Dim situNo As String = dr.Item("SITU_NO").ToString()
        Dim zoneCd As String = dr.Item("ZONE_CD").ToString()

        '棟・室マスタ存在チェック
        If Me._Vcon.SelectToShitsuListDataRow(brCd, whCd, touNo, situNo).Length < 1 AndAlso _
            (String.IsNullOrEmpty(touNo) = False OrElse _
             String.IsNullOrEmpty(situNo) = False) Then
            Call Me.SetErrData(ds, dr, LMB020G.sprDetailDef.SHITSU_NO.ColNo)
            Call Me.SetErrData(ds, dr, LMB020G.sprDetailDef.TOU_NO.ColNo)

            '2017/09/25 修正 李↓
            Dim msg As String = String.Empty
            msg = lgm.Selector({"棟室マスタ", "Master of Building", "동(棟)실(室)마스터", "中国語"})
            '2017/09/25 修正 李↑

            If String.IsNullOrEmpty(touNo) = True OrElse String.IsNullOrEmpty(situNo) = True Then
                Return Me._Vcon.SetMstErrMessage(msg, String.Empty)
            Else
                Return Me._Vcon.SetMstErrMessage(msg, String.Concat(touNo, " - ", situNo))
            End If
        End If

        'ゾーンマスタ存在チェック
        Dim touDr As DataRow() = Me._Vcon.SelectZoneListDataRow(brCd, whCd, touNo, situNo, zoneCd)
        'START YANAI 要望番号433
        'If touDr.Length < 1 AndAlso _
        '    (String.IsNullOrEmpty(touNo) = False OrElse _
        '     String.IsNullOrEmpty(situNo) = False OrElse _
        '     String.IsNullOrEmpty(zoneCd) = False) Then
        If touDr.Length < 1 AndAlso _
                ((String.IsNullOrEmpty(touNo) = False OrElse _
                 String.IsNullOrEmpty(situNo) = False) AndAlso _
                 String.IsNullOrEmpty(zoneCd) = False) Then
            'END YANAI 要望番号433
            Call Me.SetErrData(ds, dr, LMB020G.sprDetailDef.ZONE_CD.ColNo)

            '2017/09/25 修正 李↓
            Return Me._Vcon.SetMstErrMessage(lgm.Selector({"ゾーンマスタ", "Master of Zone", "존(Zone)마스터", "中国語"}), zoneCd)
            '2017/09/25 修正 李↑

        End If

        If 0 < touDr.Length Then
            'マスタの値を設定
            dr.Item("TOU_NO") = touDr(0).Item("TOU_NO").ToString
            dr.Item("SITU_NO") = touDr(0).Item("SITU_NO").ToString
            dr.Item("ZONE_CD") = touDr(0).Item("ZONE_CD").ToString
        End If

        Return True

    End Function

    ''' <summary>
    ''' '入荷QRの取込を開始できるか確認する。
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsPossibleLoadInkaQrStart() As Boolean
        ' 庫内作業ステータス取得
        Dim wkStatus As String = _Frm.cmbWhWkStatus.SelectedValue.ToString()

        Select Case wkStatus
            Case LMB020C.WH_KENPIN_WK_STATUS_INKA.WAITING_FOR_CAPTURE
                ' 取込待ち
            Case LMB020C.WH_KENPIN_WK_STATUS_INKA.INSPECTED
                ' 取込済
            Case Else
                ' 未確定のため処理できません。
                _Vcon.SetErrorControl(_Frm.cmbWhWkStatus)
                _Vcon.SetErrMessage("E939")

                Return False
        End Select

        Return True

    End Function


    ''' <summary>
    ''' 検品実績と入荷データの商品と個数が一致しているか確認する。
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' </remarks>
    Function IsMatchInspecedGoods(ByVal ds As DataSet) As Boolean

        Dim inkaMRows As IEnumerable(Of DataRow) _
            = ds.Tables(LMB020C.TABLE_NM_INKA_M).AsEnumerable() _
              .Where(Function(r) LMConst.FLG.OFF.Equals(r.Item("SYS_DEL_FLG")))

        Dim inkaSRows As IEnumerable(Of DataRow) _
            = ds.Tables(LMB020C.TABLE_NM_INKA_S).AsEnumerable() _
              .Where(Function(r) LMConst.FLG.OFF.Equals(r.Item("SYS_DEL_FLG")))

        Dim inkaQrRows As IEnumerable(Of DataRow) _
            = ds.Tables(LMB020C.TABLE_NM_INKA_SEQ_QR).AsEnumerable()


        ' 入荷Mの商品が検品時点から変更されていないことを確認する。
        For Each qr As DataRow In inkaQrRows
            If (inkaMRows.Where(Function(m) qr.Item("INKA_NO_L").Equals(m.Item("INKA_NO_L")) AndAlso _
                                            qr.Item("INKA_NO_M").Equals(m.Item("INKA_NO_M")) AndAlso _
                                            qr.Item("GOODS_CD_NRS").Equals(m.Item("GOODS_CD_NRS"))).LongCount = 0) Then

                ' 同一商品キーであっても、INKA_NO_Mが変更されている場合はエラーとする。
                Return False
            End If
        Next

        ' 検品単位別の検品数の総和を算出
        For Each qr As Tuple(Of Tuple(Of String, String, String, String, String, String, String), Long) In _
            inkaQrRows.GroupBy(Function(q) Tuple.Create(Of String, String, String, String, String, String, String) _
                                  (q.Item("INKA_NO_L").ToString() _
                                 , q.Item("INKA_NO_M").ToString() _
                                 , q.Item("LOT_NO").ToString() _
                                 , q.Item("IRIME").ToString() _
                                 , q.Item("SERIAL_NO").ToString() _
                                 , q.Item("GOODS_CRT_DATE").ToString() _
                                 , q.Item("LT_DATE").ToString())) _
                      .Select(Function(q) Tuple.Create(q.Key _
                                                     , q.Sum(Function(k) Convert.ToInt64(k.Item("KENPIN_NB")))))

            If (inkaSRows.Where(Function(s) qr.Item1.Item1.Equals(s.Item("INKA_NO_L")) AndAlso _
                                            qr.Item1.Item2.Equals(s.Item("INKA_NO_M")) AndAlso _
                                            qr.Item1.Item3.Equals(s.Item("LOT_NO")) AndAlso _
                                            qr.Item1.Item4.Equals(s.Item("IRIME")) AndAlso _
                                            qr.Item1.Item5.Equals(s.Item("SERIAL_NO")) AndAlso _
                                            qr.Item1.Item6.Equals(s.Item("GOODS_CRT_DATE")) AndAlso _
                                            qr.Item1.Item7.Equals(s.Item("LT_DATE"))) _
                        .Sum(Function(s) Convert.ToInt64(s.Item("KOSU_S"))) <> qr.Item2) Then


                ' 入荷Sの入庫数と検品数が不一致
                Return False

            End If
        Next

        Return True

    End Function


    ''' <summary>
    ''' 日陸連番QRの対象となる置場をもつ入荷Sを抽出する。
    ''' </summary>
    ''' <param name="nrsBrCd">営業所コード</param>
    ''' <param name="whCd">倉庫コード</param>
    ''' <param name="custCdL">荷主コード(大)</param>
    ''' <param name="custCdM">荷主コード(中)</param>
    ''' <param name="inkaSRows">入荷Sデータ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExtractUseQrInkaSRows(ByVal nrsBrCd As String _
                                         , ByVal whCd As String _
                                         , ByVal custCdL As String _
                                         , ByVal custCdM As String _
                                         , ByVal inkaSRows As IEnumerable(Of DataRow)) As IEnumerable(Of DataRow)

        Dim resultRows As New List(Of DataRow)
        Dim existsInkaQrGoods As Boolean = False
        Dim useInkaQrLocRows As IEnumerable(Of DataRow) _
            = Me.GetUseInkaQrLocations(nrsBrCd _
                                     , whCd _
                                     , custCdL _
                                     , custCdM)

        If (useInkaQrLocRows.Count > 0) Then
            For Each inkaS As DataRow In inkaSRows
                If ((useInkaQrLocRows _
                        .Where(Function(z) inkaS.Item("TOU_NO").Equals(z.Item("KBN_NM3"))).Count > 0)) Then
                    resultRows.Add(inkaS)
                End If
            Next
        End If
        Return resultRows

    End Function


    ''' <summary>
    ''' 指定した入荷Mの行をアクティブにする。
    ''' </summary>
    ''' <param name="inkaNoM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectInkaNoMRow(ByVal inkaNoM As String) As Boolean
        Dim spr As FarPoint.Win.Spread.SheetView = _Frm.sprGoodsDef.ActiveSheet


        For i As Integer = 0 To spr.Rows.Count - 1

            '詳細の入荷中番と明細の入荷中番が同じ行を特定
            If inkaNoM.Equals(_Vcon.GetCellValue(spr.Cells(i, LMB020G.sprGoodsDef.KANRI_NO.ColNo))) Then
                spr.ActiveRowIndex = i

                Return True
            End If
        Next

        Return False
    End Function


    ''' <summary>
    ''' スプレッドの備考に値が設定されているか判定する。
    ''' </summary>
    ''' <param name="inkaNoS"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSprRemakEmpty(ByVal inkaNoS As String) As Boolean

        If (String.IsNullOrWhiteSpace(inkaNoS)) Then
            Return False
        End If

        Dim spr As FarPoint.Win.Spread.SheetView = _Frm.sprDetail.ActiveSheet
        For i As Integer = 0 To spr.Rows.Count - 1

            '詳細の入荷中番と明細の入荷中番が同じ行を特定
            If inkaNoS.Equals(_Vcon.GetCellValue(spr.Cells(i, LMB020G.sprDetailDef.KANRI_NO_S.ColNo))) Then

                Return (Len(_Vcon.GetCellValue(spr.Cells(i, LMB020G.sprDetailDef.REMARK.ColNo))) = 0 AndAlso _
                        Len(_Vcon.GetCellValue(spr.Cells(i, LMB020G.sprDetailDef.REMARK_OUT.ColNo))) = 0)
            End If
        Next

        Return False
    End Function



    ''' <summary>
    ''' リマーク品の入荷Sに備考(スプレット)が設定されているか確認する。
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsSpreadRemakSet(ByVal ds As DataSet) As Boolean

        'ERRデータテーブルを初期化
        ds.Tables(LMB020C.TABLE_NM_ERR).Clear()

        With _Frm
            Dim nrsBrCd As String = .cmbEigyo.SelectedValue().ToString()
            Dim whCd As String = .cmbSoko.SelectedValue().ToString()
            Dim custCdL As String = .txtCustCdL.TextValue()
            Dim custCdM As String = .txtCustCdM.TextValue()

            Dim inkaM As String = _Frm.lblKanriNoM.TextValue

            Dim inkaSRows As IEnumerable(Of DataRow) _
                = ds.Tables(LMB020C.TABLE_NM_INKA_S).AsEnumerable() _
                  .Where(Function(s) LMConst.FLG.OFF.Equals(s.Item("SYS_DEL_FLG")) AndAlso _
                                     inkaM.Equals(s.Item("INKA_NO_M")))

            ' 入荷QR対象の置場を指定した入荷Sを抽出する。
            Dim useQrInkaSRows As IEnumerable(Of DataRow) = _
                Me.ExtractUseQrInkaSRows(nrsBrCd _
                                       , whCd _
                                       , custCdL _
                                       , custCdM _
                                       , inkaSRows)



            For Each inkaS As DataRow In useQrInkaSRows

                ' リマーク品にリマーク(社内 or 社外)が設定されていない。
                If (LMConst.FLG.ON.Equals(inkaS.Item("EXISTS_REMARK")) AndAlso _
                    Me.IsSprRemakEmpty(inkaS.Item("INKA_NO_S").ToString())) Then

                    Dim inkaNoM As String = inkaS.Item("INKA_NO_M").ToString()

                    If (Me.SelectInkaNoMRow(inkaNoM)) Then
                        Me.SetErrData(ds, inkaS, LMB020G.sprDetailDef.REMARK.ColNo)
                        Me.SetErrData(ds, inkaS, LMB020G.sprDetailDef.REMARK_OUT.ColNo)

                        Me._G.SetInkaMInforData(ds, -1, inkaNoM)
                        Me._G.SetInkaSData(ds, LMB020C.ActionType.SAVE, inkaNoM)
                    End If


                    ' リマーク品として検品された商品は、備考を入力してください。
                    Me._Vcon.SetErrMessage("E940")

                    Return False

                End If
            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' リマーク品の入荷Sに備考が設定されているか確認する。
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="inkaSRows"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsRemarkSet(ByVal ds As DataSet _
                               , ByVal inkaSRows As IEnumerable(Of DataRow)) As Boolean

        'ERRデータテーブルを初期化
        ds.Tables(LMB020C.TABLE_NM_ERR).Clear()

        For Each inkaS As DataRow In inkaSRows

            ' リマーク品にリマーク(社内 or 社外)が設定されていない。
            If (LMConst.FLG.ON.Equals(inkaS.Item("EXISTS_REMARK")) AndAlso _
                Len(inkaS.Item("REMARK")) = 0 AndAlso _
                Len(inkaS.Item("REMARK_OUT")) = 0) Then

                Dim inkaNoM As String = inkaS.Item("INKA_NO_M").ToString()

                If (Me.SelectInkaNoMRow(inkaNoM)) Then
                    Me.SetErrData(ds, inkaS, LMB020G.sprDetailDef.REMARK.ColNo)
                    Me.SetErrData(ds, inkaS, LMB020G.sprDetailDef.REMARK_OUT.ColNo)

                    Me._G.SetInkaMInforData(ds, -1, inkaNoM)
                    Me._G.SetInkaSData(ds, LMB020C.ActionType.SAVE, inkaNoM)
                End If

                ' リマーク品として検品された商品は、備考を入力してください。
                Me._Vcon.SetErrMessage("E940")

                Return False

            End If
        Next

        Return True

    End Function



    ''' <summary>
    ''' 入荷データがすべて検品(取込)されているか確認する。
    ''' </summary>
    ''' <param name="qrRows"></param>
    ''' <param name="existsUseQrGoods"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsInspecedAllGoods(ByVal qrRows As IEnumerable(Of DataRow) _
                                   , ByVal existsUseQrGoods As Boolean) As Boolean

        If (qrRows.Where(Function(r) Len(r.Item("LOAD_DATE")) = 0 AndAlso _
                                     LMConst.FLG.ON.Equals(r.Item("IS_LOADING")) = False).Count > 0) Then

            ' 検品実績データは存在するが、未取込
            Return False


        ElseIf (existsUseQrGoods AndAlso qrRows.Count = 0) Then

            ' QRによる検品対象対象の置場の商品が存在するが検品データは0件
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' 検品結果の妥当性を確認する。
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="IsUnMatchInkaQrData">
    ''' 
    ''' </param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function IsCurrectInkaQrData(ByVal ds As DataSet _
                               , ByRef isUnmatchInkaQrData As Boolean) As Boolean

        isUnmatchInkaQrData = False

        If (ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows.Count > 0) Then

            Dim inkaL As DataRow = ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0)


            Dim isStatusLoaded As Boolean _
                = LMB020C.WH_KENPIN_WK_STATUS_INKA.INSPECTED.Equals(inkaL.Item("WH_KENPIN_WK_STATUS"))

            ' 庫内作業ステータス[検品済]確認
            Dim isStatusInspected As Boolean _
                = LMB020C.WH_KENPIN_WK_STATUS_INKA.INSPECTED.Equals(inkaL.Item("WH_KENPIN_WK_STATUS")) OrElse _
                  LMB020C.WH_KENPIN_WK_STATUS_INKA.WAITING_FOR_CAPTURE.Equals(inkaL.Item("WH_KENPIN_WK_STATUS"))

            Dim qrRows As IEnumerable(Of DataRow) _
                = ds.Tables(LMB020C.TABLE_NM_INKA_SEQ_QR).AsEnumerable()

            If (isStatusInspected = False AndAlso qrRows.Count = 0) Then
                ' チェック対象外
                Return True
            End If

            Dim inkaSRows As IEnumerable(Of DataRow) _
                = ds.Tables(LMB020C.TABLE_NM_INKA_S).AsEnumerable() _
                  .Where(Function(s) LMConst.FLG.OFF.Equals(s.Item("SYS_DEL_FLG")))

            ' 入荷QR対象の置場を指定した入荷Sを抽出する。
            Dim useQrInkaSRows As IEnumerable(Of DataRow) = _
                Me.ExtractUseQrInkaSRows(inkaL.Item("NRS_BR_CD").ToString() _
                                       , inkaL.Item("WH_CD").ToString() _
                                       , inkaL.Item("CUST_CD_L").ToString() _
                                       , inkaL.Item("CUST_CD_M").ToString() _
                                       , inkaSRows)

            Dim existsUseQrGoods As Boolean = (useQrInkaSRows.Count > 0)

            If (isStatusInspected) Then

                ' 未検品/未取込データが存在するか確認する。
                If (isStatusLoaded AndAlso _
                    Me.IsInspecedAllGoods(qrRows, existsUseQrGoods) = False) Then

                    ' 倉庫内の検品が完了していませんが、庫内ステータスを検品済として、よろしいですか？
                    If (MyBase.ShowMessage("W270") <> MsgBoxResult.Ok) Then

                        Me._Vcon.SetErrorControl(_Frm.cmbWhWkStatus)

                        Me.ShowMessage("G090")

                        Return False
                    End If

                End If

                ' 入荷データが検品後に変更されていないか確認する。
                If (Me.IsMatchInspecedGoods(ds) = False) Then

                    ' 検品データを表示
                    isUnmatchInkaQrData = True
                    Me.ShowMessage("E941")

                    Me._Vcon.SetErrorControl(_Frm.cmbWhWkStatus)
                    Return False
                End If

            End If

            If (existsUseQrGoods) Then
                ' リマーク品に備考が入力されているか確認する。
                If (Me.IsRemarkSet(ds, useQrInkaSRows) = False) Then

                    Return False
                End If
            End If

        End If

        Return True

    End Function

    ''' <summary>
    ''' 日陸連番QRの対象となる置場を取得する。
    ''' </summary>
    ''' <param name="nrsBrCd">営業所コード</param>
    ''' <param name="whCd">倉庫コード</param>
    ''' <param name="custCdL">荷主コード(大)</param>
    ''' <param name="custCdM">荷主コード(中)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetUseInkaQrLocations(ByVal nrsBrCd As String _
                                         , ByVal whCd As String _
                                         , ByVal custCdL As String _
                                         , ByVal custCdM As String) As IEnumerable(Of DataRow)

        If (String.IsNullOrEmpty(nrsBrCd) OrElse _
            String.IsNullOrEmpty(whCd) OrElse _
            String.IsNullOrEmpty(custCdL) OrElse _
            String.IsNullOrEmpty(custCdM)) Then


            Return Enumerable.Empty(Of DataRow)()

        End If

        Const USE_INKA_QR_LOCATIONS As String = "N027"

        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).AsEnumerable() _
            .Where(Function(r) USE_INKA_QR_LOCATIONS.Equals(r.Item("KBN_GROUP_CD")) AndAlso _
                               nrsBrCd.Equals(r.Item("KBN_NM1")) AndAlso _
                               whCd.Equals(r.Item("KBN_NM2")))
    End Function



    ''' <summary>
    ''' 自営業所チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsNrsChk() As Boolean

        'If LMUserInfoManager.GetNrsBrCd().Equals(Me._Frm.cmbEigyo.SelectedValue.ToString()) = False Then
        '    Me._Frm.cmbEigyo.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
        '    Return Me._Vcon.SetErrMessage("E178", New String() {"編集"})
        'End If

        Return True

    End Function

    ''' <summary>
    ''' 作業レコードのステージチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSagyoStageChk(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables(LMB020C.TABLE_NM_SAGYO)
        Dim max As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To max

            With dt.Rows(i)

                '削除されているデータの場合、スルー
                If LMConst.FLG.ON.Equals(.Item("SYS_DEL_FLG").ToString()) = True Then
                    Continue For
                End If

                '作業が完了している場合、エラー
                If LMB020C.SAGYO_STAGE_END.Equals(.Item("SKYU_CHK").ToString()) = True Then
                    Return Me._Vcon.SetErrMessage("E127")
                End If

            End With

        Next

        Return True

    End Function

    ''' <summary>
    ''' 入荷日と起算日のワーニングチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsInkaDateKisanDateEqualsChk() As Boolean

        With Me._Frm

            '運送修正の場合、スルー
            If LMB020C.ActionType.UNSOEDIT.ToString().Equals(.lblEdit.TextValue) = True Then
                Return True
            End If

            Dim hokanDate As String = .imdHokanStrDate.TextValue

            '起算日がない場合、スルー
            If String.IsNullOrEmpty(hokanDate) = True Then
                Return True
            End If

            '入荷日と起算日が違う場合、ワーニング
            If hokanDate.Equals(.imdNyukaDate.TextValue) = False Then

                If MyBase.ShowMessage("W106") = MsgBoxResult.Cancel Then
                    Me._Frm.imdHokanStrDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.imdNyukaDate)

                    Return False

                End If

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 保管料と荷役料のワーニングチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsHokanUmuNiyakuUmuChk() As Boolean

        With Me._Frm

            '運送修正、起算日修正の場合、スルー
            If LMB020C.ActionType.UNSOEDIT.ToString().Equals(.lblEdit.TextValue) = True OrElse _
               LMB020C.ActionType.DATEEDIT.ToString().Equals(.lblEdit.TextValue) = True Then
                Return True
            End If

            '当期保管料、全期保管料、荷役料のいずれかが"無"の場合、ワーニング
            If ("00").Equals(.cmbToukiHokanUmu.SelectedValue) = True OrElse _
               ("00").Equals(.cmbZenkiHokanUmu.SelectedValue) = True OrElse _
               ("00").Equals(.cmbNiyakuUmu.SelectedValue) = True Then

                If MyBase.ShowMessage("W283") = MsgBoxResult.Cancel Then

                    If ("00").Equals(.cmbNiyakuUmu.SelectedValue) = True Then
                        Me._Vcon.SetErrorControl(.cmbNiyakuUmu)
                    End If
                    If ("00").Equals(.cmbZenkiHokanUmu.SelectedValue) = True Then
                        Me._Vcon.SetErrorControl(.cmbZenkiHokanUmu)
                    End If
                    If ("00").Equals(.cmbToukiHokanUmu.SelectedValue) = True Then
                        Me._Vcon.SetErrorControl(.cmbToukiHokanUmu)
                    End If

                    Return False

                End If

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 引当済みチェック（削除時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHikiateChk(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_M)
        Dim dr() As DataRow = dt.Select("SYS_DEL_FLG = '0'")
        Dim max As Integer = dr.Length - 1
        For i As Integer = 0 To max

            '引当済みの場合、エラー
            If LMB020C.HIKIATE_ARI.Equals(dr(i).Item("HIKIATE").ToString()) = True Then
                Return Me._Vcon.SetErrMessage("E139")
            End If
        Next

        Return True

    End Function

    ''' <summary>
    ''' 運賃確定チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnchinKakuteiChk(ByVal ds As DataSet, ByVal msg As String) As Boolean

        Dim unsoLDt As DataTable = ds.Tables(LMB020C.TABLE_NM_UNSO_L)

        'レコードがない場合、スルー
        If unsoLDt.Rows.Count < 1 Then
            Return True
        End If

        '確定済みの場合、エラー
        If LMConst.FLG.OFF.Equals(unsoLDt.Rows(0).Item("FIXED_CHK").ToString()) = False Then
            Return Me._Vcon.SetErrMessage("E126", New String() {String.Empty})
        End If

        'まとめ済みの場合、エラー
        If LMConst.FLG.OFF.Equals(unsoLDt.Rows(0).Item("GROUP_CHK").ToString()) = False Then

            '20151029 tsunehira add Start
            '英語化対応
            Return Me._Vcon.SetErrMessage("E795", New String() {msg})
            '2015.10.29 tusnehira add End
            'Return Me._Vcon.SetErrMessage("E232", New String() {"まとめ指示", msg})
        End If

        Return True

    End Function

    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
    ''' <summary>
    ''' 支払確定チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsShiharaiKakuteiChk(ByVal ds As DataSet, ByVal msg As String) As Boolean

        Dim unsoLDt As DataTable = ds.Tables(LMB020C.TABLE_NM_UNSO_L)

        'レコードがない場合、スルー
        If unsoLDt.Rows.Count < 1 Then
            Return True
        End If

        '確定済みの場合、エラー
        If LMConst.FLG.OFF.Equals(unsoLDt.Rows(0).Item("SHIHARAI_FIXED_CHK").ToString()) = False Then
            Return Me._Vcon.SetErrMessage("E497", New String() {String.Empty})
        End If

        ''まとめ済みの場合、エラー
        'If LMConst.FLG.OFF.Equals(unsoLDt.Rows(0).Item("SHIHARAI_GROUP_CHK").ToString()) = False Then
        '    Return Me._Vcon.SetErrMessage("E232", New String() {"まとめ指示", msg})
        'End If

        Return True

    End Function
    '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End



    ''' <summary>
    ''' 行追加時に渡す商品情報の単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsAddGoodsChk() As Boolean

        Dim chkFlg As Boolean = True
        Dim errorFlg As Boolean = False

        With Me._Frm

            '商品コード
            .txtSerchGoodsCd.ItemName = LMB020G.sprGoodsDef.GOODS_CD.ColName
            '.txtSerchGoodsCd.ItemName = "商品コード"
            .txtSerchGoodsCd.IsForbiddenWordsCheck = chkFlg
            If MyBase.IsValidateCheck(.txtSerchGoodsCd) = errorFlg Then
                Return errorFlg
            End If


            '商品名
            .txtSerchGoodsNm.ItemName = LMB020G.sprGoodsDef.GOODS_NM.ColName
            '.txtSerchGoodsNm.ItemName = "商品名"
            .txtSerchGoodsNm.IsForbiddenWordsCheck = chkFlg
            If MyBase.IsValidateCheck(.txtSerchGoodsNm) = errorFlg Then
                Return errorFlg
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 入荷(中)の単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSprGoodsDefChk() As Boolean
        'TODO:消す
        Dim spr As LMSpread = Me._Frm.sprGoodsDef
        With spr.ActiveSheet

            Dim max As Integer = .Rows.Count - 1
            If max = -1 Then
                Return Me._Vcon.SetErrMessage("E295")
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 印刷処理時にステージチェック
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsStageChk(ByVal dr As DataRow) As Boolean

        With Me._Frm

            'START YANAI 要望番号497
            ''入庫報告以外、スルー
            'If LMB020C.PRINT_NYUKOHOKOKU.Equals(.cmbPrint.SelectedValue.ToString()) = False Then
            '    Return True
            'End If

            'Select Case dr.Item("INKA_STATE_KB").ToString()

            '    Case LMB020C.STATE_NYUKOZUMI, LMB020C.STATE_NYUKOHOUKOKUZUMI

            '        Return True

            'End Select

            'Return Me._Vcon.SetErrMessage("E175", New String() {.cmbPrint.SelectedText})
            Return True
            'END YANAI 要望番号497

        End With

    End Function

    ''' <summary>
    ''' 振替番号チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsFuriDataChk(ByVal ds As DataSet, ByVal msg As String) As Boolean

        '振替管理番号がない場合、スルー
        If String.IsNullOrEmpty(ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0).Item("FURI_NO").ToString()) = True Then
            Return True
        End If

        '20151029 tsunehira add Start
        '英語化対応
        Return Me._Vcon.SetErrMessage("E796", New String() {msg})
        '2015.10.29 tusnehira add End
        'Return Me._Vcon.SetErrMessage("E028", New String() {"振替データ", msg})

    End Function

    'START YANAI メモ②No.20
    ''' <summary>
    ''' EDIチェック（入荷(中)削除時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsEDIChk(ByVal ds As DataSet, ByVal arr As ArrayList) As Boolean

        'Dim dt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_S)
        'Dim dr As DataRow() = Nothing
        'Dim max As Integer = arr.Count - 1
        'Dim maxS As Integer = 0

        'For i As Integer = 0 To max
        '    dr = Nothing
        '    dr = dt.Select(String.Concat("INKA_NO_M = '", Me._Vcon.GetCellValue(Me._Frm.sprGoodsDef.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMB020G.sprGoodsDef.KANRI_NO.ColNo)), "' AND ", _
        '                                 "SYS_DEL_FLG = '0'"))
        '    maxS = dr.Length - 1
        '    For j As Integer = 0 To maxS

        '        '在庫移動がある場合、エラー
        '        If 0 < Convert.ToInt32(Me._Gcon.FormatNumValue(dr(j).Item("ZAI_REC_CNT").ToString())) Then
        '            Return Me._Vcon.SetErrMessage("E148")
        '        End If
        '    Next

        'Next

        '要望番号:1253 terakawa 2012.07.13 Start
        'Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD = '", Me._Frm.txtCustCdL.TextValue, "' AND SUB_KB = '06'"))
        Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyo.SelectedValue, _
                                                                                                                         "'AND CUST_CD = '", Me._Frm.txtCustCdL.TextValue, "' AND SUB_KB = '06'"))
        '要望番号:1253 terakawa 2012.07.13 End
        If 0 < custDetailsDr.Length Then
            If (LMConst.FLG.ON).Equals(custDetailsDr(0).Item("SET_NAIYO")) = True Then
                '荷主明細マスタに該当荷主のデータがある場合のみチェックを行う

                'チェックリスト取得
                Dim max As Integer = arr.Count - 1
                For i As Integer = 0 To max
                    'スプレッド(中)すべてを対象にするチェック
                    'EDI入荷にデータがあるかどうか取得
                    Dim inMdr() As DataRow = ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyo.SelectedValue, "' AND ", _
                                    "INKA_NO_L = '", Me._Frm.lblKanriNoL.TextValue, "' AND ", _
                                    "INKA_NO_M = '", Me._Vcon.GetCellValue(Me._Frm.sprGoodsDef.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMB020G.sprGoodsDef.KANRI_NO.ColNo)), "' AND ", _
                                    "SYS_DEL_FLG = '0'"))
                    If 0 < inMdr.Length Then
                        If ("1").Equals(inMdr(0).Item("EDI_FLG").ToString()) = True Then
                            Return Me._Vcon.SetErrMessage("E121", New String() {_Frm.FunctionKey.F4ButtonName.ToString})
                        End If
                    End If
                Next
            End If
        End If

        Return True

    End Function
    'END YANAI メモ②No.20

    'START YANAI 要望番号573
    ''' <summary>
    ''' EDIデータチェック（削除時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsEdiDataChk(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_M)
        Dim dr() As DataRow = dt.Select("(JISSEKI_FLAG = '1' OR JISSEKI_FLAG = '2') AND EDI_FLG = '1' AND SYS_DEL_FLG = '0'")
        Dim cnt As Integer = dr.Length

        '追加開始 --- 2014.09.19 kikuchi
        '特定荷主の場合、エラーチェック終了
        Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", ds.Tables("LMB020_INKA_L").Rows(0).Item("NRS_BR_CD").ToString(), _
                                                                                                 "' AND CUST_CD = '", ds.Tables("LMB020_INKA_L").Rows(0).Item("CUST_CD_L").ToString(), _
                                                                                                 "' AND SUB_KB = '82'"))

        If 0 < custDetailsDr.Length Then
            Return True
        End If
        '追加終了 ---

        'EDIで作成されたデータ場合、エラー
        If 0 < cnt Then
            Return Me._Vcon.SetErrMessage("E426", New String() {_Frm.FunctionKey.F4ButtonName.ToString})
        End If

        Return True

    End Function
    'END YANAI 要望番号573

    '要望番号:1350 terakawa 2012.08.24 Start
    ''' <summary>
    ''' 同一置場での同一商品・ロット重複チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsGoodsLotChk(ByVal ds As DataSet, ByVal serverChkFlg As Boolean) As Boolean


        '同一置き場に同一商品・ロットがある場合ワーニング
        Dim drs As DataRow() = ds.Tables(LMB020C.TABLE_NM_INKA_S).Select("SYS_DEL_FLG='0'")
        Dim max As Integer = drs.Count - 1
        Dim worningDt As DataTable = ds.Tables(LMB020C.TABLE_NM_WORNING)
        Dim worningDr As DataRow = Nothing
        Dim worningFlg As Boolean = Nothing

        For i As Integer = 0 To max
            Dim nrsBrCd As String = drs(i)("NRS_BR_CD").ToString()
            Dim inkaNoL As String = drs(i)("INKA_NO_L").ToString()
            Dim inkaNoM As String = drs(i)("INKA_NO_M").ToString()
            Dim custGoodsCd As String = String.Empty
            Dim lotNo As String = drs(i)("LOT_NO").ToString()
            Dim touNo As String = drs(i)("TOU_NO").ToString()
            Dim situNo As String = drs(i)("SITU_NO").ToString()
            Dim zoneCd As String = drs(i)("ZONE_CD").ToString()
            Dim loca As String = drs(i)("LOCA").ToString()
            Dim drM As DataRow() = ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(String.Concat(" NRS_BR_CD = '", nrsBrCd, "' AND INKA_NO_L = '", inkaNoL, _
                                                                                           "' AND INKA_NO_M = '", inkaNoM, "' AND SYS_DEL_FLG = '0'"))
            Dim nrsGoodsCd As String = drM(0).Item("GOODS_CD_NRS").ToString()

            '---↓
            'custGoodsCd = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, _
            '                                                                              "'AND GOODS_CD_NRS = '", nrsGoodsCd, "'"))(0).Item("GOODS_CD_CUST").ToString()
            Dim goodsDs As MGoodsDS = New MGoodsDS
            Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
            goodsDr.Item("NRS_BR_CD") = nrsBrCd
            goodsDr.Item("GOODS_CD_NRS") = nrsGoodsCd
            goodsDr.Item("SYS_DEL_FLG") = "0"    '要望番号1604 2012/11/16 本明追加
            goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
            Dim rtnDs As DataSet = MyBase.GetGoodsMasterData(goodsDs)
            custGoodsCd = rtnDs.Tables(LMConst.CacheTBL.GOODS)(0).Item("GOODS_CD_CUST").ToString
            '---↑

            'ワーニングフラグをリセット
            worningFlg = False

            '要望番号:1511 KIM 2012/10/12 START
            '商品コード違いの重複チェック
            'Dim drInkaS As DataRow() = ds.Tables(LMB020C.TABLE_NM_INKA_S).Select(String.Concat(" NRS_BR_CD = '", nrsBrCd, "' AND INKA_NO_L = '", inkaNoL, _
            '                                                       "' AND LOT_NO = '", lotNo, "' AND TOU_NO = '", touNo, _
            '                                                        "' AND SITU_NO = '", situNo, "' AND ZONE_CD = '", zoneCd, _
            '                                                        "' AND LOCA = '", loca, "' AND SYS_DEL_FLG = '0'"))
            'If drInkaS.Count >= 2 Then
            '    For j As Integer = 0 To drInkaS.Count - 1
            '        Dim drM2 As DataRow() = ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(String.Concat(" NRS_BR_CD = '", nrsBrCd, "' AND INKA_NO_L = '", inkaNoL, _
            '                                                                               "' AND INKA_NO_M = '", drInkaS(j).Item("INKA_NO_M").ToString(), "' AND SYS_DEL_FLG = '0'"))

            '        Dim custGoodsCd2 As String = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, _
            '                                                                              "'AND GOODS_CD_NRS = '", drM2(0).Item("GOODS_CD_NRS"), "'"))(0) _
            '                                                                              .Item("GOODS_CD_CUST").ToString()

            '        If custGoodsCd.Equals(custGoodsCd2) = False Then
            '            worningFlg = True
            '        End If
            '    Next
            'End If
            '要望番号:1511 KIM 2012/10/12 END

            'ロット番号違いの重複チェック
            Dim drSLot As DataRow() = ds.Tables(LMB020C.TABLE_NM_INKA_S).Select(String.Concat(" NRS_BR_CD = '", nrsBrCd, "' AND INKA_NO_L = '", inkaNoL, _
                                                                   "' AND LOT_NO <> '", lotNo, "' AND TOU_NO = '", touNo, _
                                                                    "' AND SITU_NO = '", situNo, "' AND ZONE_CD = '", zoneCd, _
                                                                    "' AND LOCA = '", loca, "' AND SYS_DEL_FLG = '0'"))
            If drSLot.Count >= 1 Then
                For j As Integer = 0 To drSLot.Count - 1
                    Dim drM3 As DataRow() = ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(String.Concat(" NRS_BR_CD = '", nrsBrCd, "' AND INKA_NO_L = '", inkaNoL, _
                                                                                           "' AND INKA_NO_M = '", drSLot(j).Item("INKA_NO_M").ToString(), "' AND SYS_DEL_FLG = '0'"))

                    '---↓
                    'Dim custGoodsCd3 As String = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, _
                    '                                                                      "'AND GOODS_CD_NRS = '", drM3(0).Item("GOODS_CD_NRS"), "'"))(0) _
                    '                                                                      .Item("GOODS_CD_CUST").ToString()
                    goodsDs = New MGoodsDS
                    goodsDr.Delete()
                    goodsDr = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
                    goodsDr.Item("NRS_BR_CD") = nrsBrCd
                    goodsDr.Item("GOODS_CD_NRS") = drM3(0).Item("GOODS_CD_NRS").ToString
                    goodsDr.Item("NRS_BR_CD") = nrsBrCd
                    goodsDr.Item("SYS_DEL_FLG") = "0"    '要望番号1604 2012/11/16 本明追加
                    goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
                    rtnDs = MyBase.GetGoodsMasterData(goodsDs)
                    Dim custGoodsCd3 As String = rtnDs.Tables(LMConst.CacheTBL.GOODS)(0).Item("GOODS_CD_CUST").ToString
                    '---↑

                    If custGoodsCd.Equals(custGoodsCd3) = True Then
                        worningFlg = True
                    End If
                Next
            End If

            'ワーニングフラグがTrueの場合、ワーニングテーブルに情報をセット
            If worningFlg = True Then
                worningDr = worningDt.NewRow()
                With worningDr
                    .Item("GOODS_CD_CUST") = custGoodsCd
                    .Item("TOU_NO") = touNo
                    .Item("SITU_NO") = situNo
                    .Item("ZONE_CD") = zoneCd
                    .Item("LOCA") = loca
                    .Item("LOT_NO") = lotNo
                End With
                ds.Tables(LMB020C.TABLE_NM_WORNING).Rows.Add(worningDr)
            End If
        Next

        If ds.Tables(LMB020C.TABLE_NM_WORNING).Rows.Count > 0 Then
            Return IsWorningChk(ds, serverChkFlg)
        End If

        Return True
    End Function

    ''' <summary>
    ''' 同一置場での同一商品・ロット重複チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:OK False:キャンセル</returns>
    ''' <remarks></remarks>
    Friend Function IsWorningChk(ByVal ds As DataSet, ByVal serverChkFlg As Boolean) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        'ワーニングテーブルに情報があった場合、ワーニング出力する
        Dim worningCount As Integer = ds.Tables(LMB020C.TABLE_NM_WORNING).Rows.Count
        If worningCount > 0 Then
            Dim strWorning As String = String.Empty
            Dim wGoodsCdCust As String = String.Empty
            Dim wOkiba As String = String.Empty
            Dim wLotNo As String = String.Empty
            For k As Integer = 0 To worningCount - 1
                strWorning = String.Concat(strWorning, vbCrLf)
                wGoodsCdCust = ds.Tables(LMB020C.TABLE_NM_WORNING).Rows(k).Item("GOODS_CD_CUST").ToString()
                wOkiba = String.Concat(ds.Tables(LMB020C.TABLE_NM_WORNING).Rows(k).Item("TOU_NO").ToString(), "-", _
                                       ds.Tables(LMB020C.TABLE_NM_WORNING).Rows(k).Item("SITU_NO").ToString(), "-", _
                                       ds.Tables(LMB020C.TABLE_NM_WORNING).Rows(k).Item("ZONE_CD").ToString())
                If String.IsNullOrEmpty(ds.Tables(LMB020C.TABLE_NM_WORNING).Rows(k).Item("LOCA").ToString()) = False Then
                    wOkiba = String.Concat(wOkiba, "-", ds.Tables(LMB020C.TABLE_NM_WORNING).Rows(k).Item("LOCA").ToString())
                End If
                wLotNo = ds.Tables(LMB020C.TABLE_NM_WORNING).Rows(k).Item("LOT_NO").ToString()

                '2017/09/25 修正 李↓
                strWorning = String.Concat(strWorning, _Frm.lblTitleGoodsCd.TextValue, "=", wGoodsCdCust, lgm.Selector({"、置場=", "、Place=", ", 하치장=", "中国語"}), wOkiba, "、LOT=", wLotNo)
                '2017/09/25 修正 李↑

            Next

            'ワーニングテーブルの中身を削除
            ds.Tables(LMB020C.TABLE_NM_WORNING).Clear()

            If serverChkFlg = True Then
                '20151020 tsunehira add start
                '20160628 tsunehira メッセージ内容修正
                If MyBase.ShowMessage("W242", New String() {"" _
                                                    , String.Concat(vbCrLf, vbCrLf, strWorning)}) <> MsgBoxResult.Ok Then
                    Return False
                End If
                'If MyBase.ShowMessage("W215", New String() {String.Concat(vbCrLf, "同じ場所に保管されています。") _
                '                                    , String.Concat(vbCrLf, vbCrLf, strWorning)}) <> MsgBoxResult.Ok Then
                '    Return False
                'End If
            Else

                If MyBase.ShowMessage("W243", New String() {"" _
                                                    , String.Concat(vbCrLf, vbCrLf, strWorning)}) <> MsgBoxResult.Ok Then
                    Return False
                End If
                'If MyBase.ShowMessage("W215", New String() {String.Concat(vbCrLf, "同じ場所で画面上に存在します。") _
                '                                    , String.Concat(vbCrLf, vbCrLf, strWorning)}) <> MsgBoxResult.Ok Then
                '    Return False
                'End If
                '20151020 tsunehira add end
            End If

        End If

        Return True
    End Function

    '要望番号:1350 terakawa 2012.08.24 End

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        'ヘッダ項目のスペース除去
        Call Me.TrimHeaderSpaceTextValue()

        'スプレッドのスペース除去
        'START YANAI 要望番号548
        'Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprDetail)
        Call Me.TrimSpaceSprTextvalue(Me._Frm.sprDetail)
        'END YANAI 要望番号548

    End Sub

    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimHeaderSpaceTextValue()

        With Me._Frm

#If False Then '区分タイトルラベル対応 Changed 20151116 INOUE
            .txtNyukaKbn.TextValue = .txtNyukaKbn.TextValue.Trim()
            .txtShinshokuKbn.TextValue = .txtShinshokuKbn.TextValue.Trim()
            .txtShinshokuKbnKbn.TextValue = .txtShinshokuKbnKbn.TextValue.Trim()
#End If

            .txtHuriKanriNo.TextValue = .txtHuriKanriNo.TextValue.Trim()
            .txtBuyerOrdNo.TextValue = .txtBuyerOrdNo.TextValue.Trim()
            .txtOrderNo.TextValue = .txtOrderNo.TextValue.Trim()
            .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
            .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()
            .txtNyubanL.TextValue = .txtNyubanL.TextValue.Trim()
            .txtNyukaComment.TextValue = .txtNyukaComment.TextValue.Trim()
            .txtSerchGoodsCd.TextValue = .txtSerchGoodsCd.TextValue.Trim()
            .txtSerchGoodsNm.TextValue = .txtSerchGoodsNm.TextValue.Trim()
            .lblKanriNoM.TextValue = .lblKanriNoM.TextValue.Trim()
            .txtOrderNoM.TextValue = .txtOrderNoM.TextValue.Trim()
            .txtBuyerOrdNoM.TextValue = .txtBuyerOrdNoM.TextValue.Trim()
            .txtGoodsComment.TextValue = .txtGoodsComment.TextValue.Trim()
            .txtSagyoCdM1.TextValue = .txtSagyoCdM1.TextValue.Trim()
            .txtSagyoCdM2.TextValue = .txtSagyoCdM2.TextValue.Trim()
            .txtSagyoCdM3.TextValue = .txtSagyoCdM3.TextValue.Trim()
            .txtSagyoCdM4.TextValue = .txtSagyoCdM4.TextValue.Trim()
            .txtSagyoCdM5.TextValue = .txtSagyoCdM5.TextValue.Trim()
            .txtUnsoNo.TextValue = .txtUnsoNo.TextValue.Trim()
            .txtUnsoCd.TextValue = .txtUnsoCd.TextValue.Trim()
            .txtTrnBrCD.TextValue = .txtTrnBrCD.TextValue.Trim()
            .txtUnsoTariffCD.TextValue = .txtUnsoTariffCD.TextValue.Trim()
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
            .txtShiharaiTariffCD.TextValue = .txtShiharaiTariffCD.TextValue.Trim()
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End
            .txtShukkaMotoCD.TextValue = .txtShukkaMotoCD.TextValue.Trim()
            .txtUnchinComment.TextValue = .txtUnchinComment.TextValue.Trim()
            .txtSagyoCdL1.TextValue = .txtSagyoCdL1.TextValue.Trim()
            .txtSagyoCdL2.TextValue = .txtSagyoCdL2.TextValue.Trim()
            .txtSagyoCdL3.TextValue = .txtSagyoCdL3.TextValue.Trim()
            .txtSagyoCdL4.TextValue = .txtSagyoCdL4.TextValue.Trim()
            .txtSagyoCdL5.TextValue = .txtSagyoCdL5.TextValue.Trim()

        End With

    End Sub

    'START YANAI 要望番号548
    ''' <summary>
    ''' スプレッドの値をTrim
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <remarks></remarks>
    Friend Sub TrimSpaceSprTextvalue(ByVal spr As Win.Spread.LMSpread)

        With spr
            Dim rowMax As Integer = .ActiveSheet.Rows.Count - 1

            For i As Integer = 0 To rowMax

                Call Me.TrimSpaceSprTextvalue(spr, i)

            Next

        End With

    End Sub

    ''' <summary>
    ''' スプレッドの値をTrim
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Friend Sub TrimSpaceSprTextvalue(ByVal spr As Win.Spread.LMSpread, ByVal rowNo As Integer)

        With spr

            Dim colMax As Integer = .ActiveSheet.Columns.Count - 1
            Dim aCell As Cell = Nothing

            For i As Integer = 0 To colMax

                aCell = .ActiveSheet.Cells(rowNo, i)

                If TypeOf aCell.Editor Is CellType.ComboBoxCellType = True _
                    OrElse TypeOf aCell.Editor Is CellType.CheckBoxCellType = True _
                    OrElse TypeOf aCell.Editor Is CellType.DateTimeCellType = True _
                    OrElse TypeOf aCell.Editor Is CellType.NumberCellType = True _
                    Then
                    '処理なし
                Else
                    If (LMB020G.sprDetailDef.LOCA.ColNo).Equals(i) = False AndAlso _
                        (LMB020G.sprDetailDef.REMARK.ColNo).Equals(i) = False AndAlso _
                        (LMB020G.sprDetailDef.REMARK_OUT.ColNo).Equals(i) = False Then
                        'LOCA、備考小(社内)、備考小(社外)以外の時のみTRIMを行う
                        .SetCellValue(rowNo, i, Me._Vcon.GetCellValue(.ActiveSheet.Cells(rowNo, i)))
                    End If
                End If

            Next

        End With

    End Sub
    'END YANAI 要望番号548

    ''' <summary>
    ''' ファイル存在チェック
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsFileExist(ByVal filePath As String, ByVal fileName As String) As Boolean

        If System.IO.File.Exists(String.Concat(filePath, fileName)) = False Then

            'ファイルが存在しない場合、エラーにする
            '2015.10.21 tusnehira add
            '英語化対応
            Me.ShowMessage("E657")
            'Me.ShowMessage("E469", New String() {"取込対象のファイル"})
            Return False

        End If

        Return True

    End Function


    'ADD Start 2019/08/01 要望管理005237
    ''' <summary>
    ''' 出荷止設定可否チェック
    ''' </summary>
    ''' <returns>True:出荷止設定可 False:出荷止設定不可</returns>
    ''' <remarks></remarks>
    Private Function IsStopAllocValid() As Boolean

        With Me._Frm

            '出荷止チェックあり
            If .chkStopAlloc.Checked Then

                '進捗区分が50:入荷済以降の場合、エラー
                If .lblShinshokuKbn.KbnValue >= LMB020C.STATE_NYUKOZUMI Then
                    Return Me._Vcon.SetErrMessage("E320", {"進捗区分が入荷済以降", "出荷止"})
                End If

                '引当済の場合、エラー
                Return IsHikiateEditChk("E320", {"引当済", "出荷止"})

            End If

        End With

        Return True

    End Function
    'ADD End   2019/08/01 要望管理005237

#End Region

    ''' <summary>
    ''' 棟 + 室 + ZONE（置き場情報）温度管理チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsOndoCheck(ByVal ds As DataSet) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            Dim nrsbrcd As String = .cmbEigyo.SelectedValue.ToString
            Dim sokocd As String = .cmbSoko.SelectedValue.ToString
            Dim inakLNo As String = .lblKanriNoL.TextValue.Trim
            Dim custcd As String = .txtCustCdL.TextValue.Trim
            'Dim subkb As String = "13"
            'Dim sql As String = String.Concat("NRS_BR_CD = ", " '", nrsbrcd, "' ", _
            '                                 " AND CUST_CD = ", " '", custcd, "' ", _
            '                                 " AND SUB_KB = ", " '", subkb, "' ")

            'キャッシュテーブルからデータ抽出
            'Dim custDtlDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(sql)

            'If custDtlDr.Length < 1 Then Return True
            'Return Me._Vcon.SetMstErrMessage("荷主明細マスタ", String.Concat(nrsbrcd, " - ", custcd, " - ", subkb))
            'End If
            'If "1".Equals(custDtlDr(0).Item("SET_NAIYO").ToString) = False Then Return True

            '入荷(中)全行
            Dim dtM As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_M)
            Dim maxM As Integer = dtM.Rows.Count - 1
            Dim inakMNo As String = String.Empty
            Dim goodsNRS As String = String.Empty

            '中
            Dim ondokbn As String = String.Empty
            Dim ondoStartDate As String = String.Empty
            Dim ondoEndDate As String = String.Empty
            Dim ondoUpper As String = String.Empty
            Dim ondoLower As String = String.Empty

            '小
            Dim touNo As String = String.Empty
            Dim situNo As String = String.Empty
            Dim zoneCd As String = String.Empty
            Dim ondoCtlKbn_TouSitu As String = String.Empty
            Dim ondoCtlKbn_Zone As String = String.Empty
            Dim ondo_TouSitu As String = String.Empty
            Dim ondo_Zone As String = String.Empty

            '判定
            If 0 > maxM Then Return True

            Dim msg As String = String.Empty

            '倉庫マスタチェック
            Dim sokoDrs As DataRow() = Me._Vcon.SelectSokoListDataRow(.cmbSoko.SelectedValue.ToString())
            If sokoDrs.Length < 1 Then Return Me._Vcon.SetMstErrMessage(msg, String.Concat(nrsbrcd, " - ", .cmbSoko.SelectedValue.ToString()))
            If sokoDrs(0).Item("LOC_MANAGER_YN").ToString = "00" Then
                Return True
            End If

            For i As Integer = 0 To maxM

                '2019/11/22 要望管理008247 add
                '行削除されているデータはチェックしない
                If LMConst.FLG.ON.Equals(dtM.Rows(i).Item("SYS_DEL_FLG").ToString()) Then
                    Continue For
                End If

                inakMNo = dtM.Rows(i).Item("INKA_NO_M").ToString()
                goodsNRS = dtM.Rows(i).Item("GOODS_CD_NRS").ToString()

                Dim goodsDr As DataRow() = Me._Vcon.SelectGoodsListDataRow(nrsbrcd, goodsNRS)

                '2017/09/25 修正 李↓
                msg = lgm.Selector({"商品マスタ", "Master of Goods", "인쇄종별", "中国語"})
                '2017/09/25 修正 李↑

                '判定
                If goodsDr.Length < 1 Then Return Me._Vcon.SetMstErrMessage(msg, String.Concat(nrsbrcd, " - ", goodsNRS))

                ondokbn = goodsDr(0).Item("ONDO_KB").ToString
                ondoStartDate = goodsDr(0).Item("ONDO_STR_DATE").ToString
                ondoEndDate = goodsDr(0).Item("ONDO_END_DATE").ToString
                ondoUpper = goodsDr(0).Item("ONDO_MX").ToString
                ondoLower = goodsDr(0).Item("ONDO_MM").ToString

                '判定
                If Not "02".Equals(ondokbn) Then Continue For

                Dim nyukaYYYY As String = Left(.imdNyukaDate.TextValue, 4)
                Dim startYYYY As String = nyukaYYYY
                Dim endYYYY As String = nyukaYYYY
                If ondoStartDate > ondoEndDate Then endYYYY = (Integer.Parse(nyukaYYYY) + 1).ToString

                '判定
                If Not (String.Concat(startYYYY, ondoStartDate) <= .imdNyukaDate.TextValue _
                        And String.Concat(endYYYY, ondoEndDate) >= .imdNyukaDate.TextValue) Then Continue For

                '入荷(小)全行
                Dim sqlS As String = String.Concat("NRS_BR_CD = ", " '", nrsbrcd, "' ", _
                                                 " AND INKA_NO_L = ", " '", inakLNo, "' ", _
                                                 " AND INKA_NO_M = ", " '", inakMNo, "' ")

                Dim drS As DataRow() = ds.Tables(LMB020C.TABLE_NM_INKA_S).Select(sqlS)
                Dim maxS As Integer = drS.Length - 1

                '棟室マスタ・ゾーンマスタ
                For j As Integer = 0 To maxS

                    touNo = drS(j).Item("TOU_NO").ToString()
                    situNo = drS(j).Item("SITU_NO").ToString()
                    zoneCd = drS(j).Item("ZONE_CD").ToString()

                    '棟室マスタ
                    Dim tousituDr As DataRow() = Me._Vcon.SelectTouSituListDataRow(nrsbrcd, sokocd, touNo, situNo)
                    '判定

                    '2017/09/25 修正 李↓
                    msg = lgm.Selector({"棟室マスタ", "Master of Building", "동(棟)실(室)마스터", "中国語"})
                    '2017/09/25 修正 李↑

                    If tousituDr.Length < 1 Then Return Me._Vcon.SetMstErrMessage(msg, String.Concat(nrsbrcd, " - ", sokocd, " - ", touNo, " - ", situNo))
                    ondoCtlKbn_TouSitu = tousituDr(0).Item("TOU_ONDO_CTL_KB").ToString
                    ondo_TouSitu = tousituDr(0).Item("TOU_ONDO").ToString

                    'ゾーンマスタ
                    Dim zoneDr As DataRow() = Me._Vcon.SelectZoneListDataRow(nrsbrcd, sokocd, touNo, situNo, zoneCd)
                    '判定

                    '2017/09/25 修正 李↓
                    msg = lgm.Selector({"ゾーンマスタ", "Master of Zone", "존(Zone)마스터", "中国語"})
                    '2017/09/25 修正 李↑

                    If zoneDr.Length < 1 Then Return Me._Vcon.SetMstErrMessage(msg, String.Concat(nrsbrcd, " - ", sokocd, " - ", touNo, " - ", situNo, " - ", zoneCd))
                    ondoCtlKbn_Zone = zoneDr(0).Item("ZONE_ONDO_CTL_KB").ToString
                    ondo_Zone = zoneDr(0).Item("ZONE_ONDO").ToString

                    '判定①
                    Dim kanriNo As String = String.Concat(" (", inakMNo, "-", "", drS(j).Item("INKA_NO_S").ToString, ") ")
                    If Not "02".Equals(ondoCtlKbn_TouSitu) And Not "02".Equals(ondoCtlKbn_Zone) Then

                        '20151020 tsunehira add
                        msg = String.Concat(touNo, "-", situNo, "-", zoneCd, kanriNo)
                        If MyBase.ShowMessage("W241", New String() {msg}) = MsgBoxResult.Cancel Then Return False
                        'msg = String.Concat("置場　", touNo, "-", situNo, "-", zoneCd, kanriNo)
                        'If MyBase.ShowMessage("W191", New String() {msg, "定温置場"}) = MsgBoxResult.Cancel Then Return False


                    End If

                    '判定②
                    '　ondoLower <= ondo_TouSitu <= ondoUpper　　　　　　　　　8>15 or 15 >12 ◎　　
                    '　ondoLower <= ondo_Zone <= ondoUpper                     8>10 or 10 >12 ◎
                    If String.IsNullOrEmpty(ondo_Zone) Then
                        'ゾーンなし
                        If ("02".Equals(ondoCtlKbn_TouSitu) And (Integer.Parse(ondoLower) > Integer.Parse(ondo_TouSitu) Or Integer.Parse(ondo_TouSitu) > Integer.Parse(ondoUpper))) Then
                            msg = String.Concat(touNo, "-", situNo, kanriNo)
                            If MyBase.ShowMessage("W240", New String() {msg}) = MsgBoxResult.Cancel Then Return False
                        End If
                    Else
                        'ゾーンあり
                        If ("02".Equals(ondoCtlKbn_TouSitu) And (Integer.Parse(ondoLower) > Integer.Parse(ondo_TouSitu) Or Integer.Parse(ondo_TouSitu) > Integer.Parse(ondoUpper))) _
                        Or ("02".Equals(ondoCtlKbn_Zone) And (Integer.Parse(ondoLower) > Integer.Parse(ondo_Zone) Or Integer.Parse(ondo_Zone) > Integer.Parse(ondoUpper))) Then
                            msg = String.Concat(touNo, "-", situNo, "-", zoneCd, kanriNo)
                            If MyBase.ShowMessage("W240", New String() {msg}) = MsgBoxResult.Cancel Then Return False
                        End If
                    End If



                Next

            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 棟 + 室  危険物倉庫、一般倉庫チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSokoCheck(ByVal ds As DataSet) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            Dim nrsbrcd As String = .cmbEigyo.SelectedValue.ToString
            Dim sokocd As String = .cmbSoko.SelectedValue.ToString
            Dim inakLNo As String = .lblKanriNoL.TextValue.Trim
            Dim custcd As String = .txtCustCdL.TextValue.Trim
            Dim kikenCheck As Boolean = True
            Dim ippanCheck As Boolean = True

            'Dim subkb As String = "75"
            'Dim sql As String = String.Concat("NRS_BR_CD = ", " '", nrsbrcd, "' ", _
            '                                 " AND CUST_CD = ", " '", custcd, "' ", _
            '                                 " AND SUB_KB = ", " '", subkb, "' ")

            'キャッシュテーブルからデータ抽出（危険物）
            'Dim custDtlDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(sql)


            'Dim subkb_2 As String = "76"
            'Dim sql_2 As String = String.Concat("NRS_BR_CD = ", " '", nrsbrcd, "' ", _
            '                                 " AND CUST_CD = ", " '", custcd, "' ", _
            '                                 " AND SUB_KB = ", " '", subkb_2, "' ")

            ''キャッシュテーブルからデータ抽出（一般物）
            'Dim custDtlDr_2 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(sql_2)


            'If custDtlDr.Length >= 1 Then
            '    If "1".Equals(custDtlDr(0).Item("SET_NAIYO").ToString) = True Then
            '        kikenCheck = True
            '    End If
            'End If

            'If custDtlDr_2.Length >= 1 Then
            '    If "1".Equals(custDtlDr_2(0).Item("SET_NAIYO").ToString) = True Then
            '        ippanCheck = True
            '    End If
            'End If

            ''危険物チェック、一般物チェック両方ともなしで関数抜ける
            'If kikenCheck = False AndAlso ippanCheck = False Then
            '    Return True
            'End If

            '2017/11/15 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
            '危険物チェック結果をワーニングで出すかエラーで出すかの情報を区分テーブルより取得
            Dim selectString As String = String.Empty
            '削除フラグ
            selectString = String.Concat(selectString, " SYS_DEL_FLG = '0' ")
            '区分グループコード
            selectString = String.Concat(selectString, " AND KBN_GROUP_CD = 'S111' ")
            '区分コード
            selectString = String.Concat(selectString, " AND KBN_CD = '0' ")

            Dim targetDataRow() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(selectString)
            Dim IsCheckError As Boolean = Convert.ToInt32(Convert.ToDecimal(targetDataRow(0).Item("VALUE1").ToString)).Equals(Convert.ToInt32(LMB020C.DangerousGoodsCheckErrorOrWarning.Err))
            '2017/11/15 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start

            '入荷(中)全行
            Dim dtM As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_M)
            Dim maxM As Integer = dtM.Rows.Count - 1
            Dim inakMNo As String = String.Empty
            Dim goodsNRS As String = String.Empty

            '中（商品）
            Dim kikenkbn As String = String.Empty

            '小
            Dim touNo As String = String.Empty
            Dim situNo As String = String.Empty
            Dim soko_kbn As String = String.Empty

            '判定
            If 0 > maxM Then Return True

            Dim msg As String = String.Empty

            '倉庫マスタチェック
            Dim sokoDrs As DataRow() = Me._Vcon.SelectSokoListDataRow(.cmbSoko.SelectedValue.ToString())
            If sokoDrs.Length < 1 Then Return Me._Vcon.SetMstErrMessage(msg, String.Concat(nrsbrcd, " - ", .cmbSoko.SelectedValue.ToString()))
            If sokoDrs(0).Item("LOC_MANAGER_YN").ToString = "00" Then
                Return True
            End If

            For i As Integer = 0 To maxM

                '2019/11/22 要望管理008247 add
                '行削除されているデータはチェックしない
                If LMConst.FLG.ON.Equals(dtM.Rows(i).Item("SYS_DEL_FLG").ToString()) Then
                    Continue For
                End If

                inakMNo = dtM.Rows(i).Item("INKA_NO_M").ToString()
                goodsNRS = dtM.Rows(i).Item("GOODS_CD_NRS").ToString()

                Dim goodsDr As DataRow() = Me._Vcon.SelectGoodsListDataRow(nrsbrcd, goodsNRS)

                '判定
                '2017/09/25 修正 李↓
                msg = lgm.Selector({"商品マスタ", "Master of Goods", "상품마스터", "中国語"})
                '2017/09/25 修正 李↑

                If goodsDr.Length < 1 Then Return Me._Vcon.SetMstErrMessage(msg, String.Concat(nrsbrcd, " - ", goodsNRS))

                kikenkbn = goodsDr(0).Item("KIKEN_KB").ToString

                '入荷(小)全行
                Dim sqlS As String = String.Concat("NRS_BR_CD = ", " '", nrsbrcd, "' ", _
                                                 " AND INKA_NO_L = ", " '", inakLNo, "' ", _
                                                 " AND INKA_NO_M = ", " '", inakMNo, "' ")

                Dim drS As DataRow() = ds.Tables(LMB020C.TABLE_NM_INKA_S).Select(sqlS)
                Dim maxS As Integer = drS.Length - 1

                '棟室マスタ・ゾーンマスタ
                For j As Integer = 0 To maxS

                    touNo = drS(j).Item("TOU_NO").ToString()
                    situNo = drS(j).Item("SITU_NO").ToString()

                    '棟室マスタ
                    Dim tousituDr As DataRow() = Me._Vcon.SelectTouSituListDataRow(nrsbrcd, sokocd, touNo, situNo)

                    '2017/11/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
                    '棟室マスタが取得出来ない場合、危険物チェックを行わない
                    If tousituDr.Length.Equals(0) Then
                        Continue For
                    End If
                    '2017/11/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

                    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
                    '自社他社情報を取得する
                    Dim isTasya As Boolean = tousituDr(0).Item("JISYATASYA_KB").ToString.Equals("02")
                    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

                    '判定

                    '2017/09/25 修正 李↓
                    msg = lgm.Selector({"棟室マスタ", "Master of Building", "동(棟)실(室)마스터", "中国語"})
                    '2017/09/25 修正 李↑

                    '2017/11/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen del start
                    '棟室マスタ情報が取得できない場合、危険物チェックを行わなくなったため、この処理は削除
                    'If tousituDr.Length < 1 Then Return Me._Vcon.SetMstErrMessage(msg, String.Concat(nrsbrcd, " - ", sokocd, " - ", touNo, " - ", situNo))
                    '2017/11/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen del start
                    soko_kbn = tousituDr(0).Item("SOKO_KB").ToString

                    Dim kanriNo As String = String.Concat(" (", inakMNo, "-", "", drS(j).Item("INKA_NO_S").ToString, ") ")

                    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen upd start
                    'エラー処理の場合
                    If IsCheckError Then

                        '危険物チェック
                        If kikenCheck = True Then

                            Dim isErr As Boolean = False
                            If (kikenkbn.Equals("02") OrElse kikenkbn.Equals("03")) AndAlso soko_kbn = "11" Then
                                If isTasya Then
                                    isErr = True
                                ElseIf String.IsNullOrEmpty(touNo) OrElse String.IsNullOrEmpty(situNo) Then
                                    isErr = True
                                Else
                                    Dim drTouSituExp As DataRow() = ds.Tables(LMB020C.TABLE_NM_TOU_SITU_EXP).Select("TOU_NO = '" & touNo & "' AND SITU_NO = '" & situNo & "'")
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
                        End If

                        '一般物チェック
                        If ippanCheck = True Then
                            '一般品の商品(kikenkbn=01、04)で、倉庫区分(soko_kb=02)が危険物倉庫であればエラー
                            If (kikenkbn.Equals("01") OrElse kikenkbn.Equals("04")) AndAlso soko_kbn = "02" Then
                                msg = String.Concat(touNo, "-", situNo, kanriNo)
                                MyBase.ShowMessage("G091", New String() {msg})
                                Return False
                            End If
                        End If
                    Else
                        'ワーニング出力の場合

                        '2012/12/06 他社の場合、ワーニングチェックを行わない処理を追記
                        If isTasya.Equals(False) Then

                            '危険品(kikenkbn<>01)で、倉庫区分(soko_kb=11)が普通倉庫であればワーニング
                            If kikenkbn <> "01" AndAlso soko_kbn = "11" Then
                                msg = String.Concat(touNo, "-", situNo, kanriNo)
                                If MyBase.ShowMessage("W239", New String() {msg}) = MsgBoxResult.Cancel Then Return False
                            End If
                            '一般品の商品(kikenkbn=01)で、倉庫区分(soko_kb=02)が危険物倉庫であればワーニング
                            If kikenkbn.Equals("01") AndAlso soko_kbn = "02" Then
                                msg = String.Concat(touNo, "-", situNo, kanriNo)
                                If MyBase.ShowMessage("W238", New String() {msg}) = MsgBoxResult.Cancel Then Return False
                            End If
                        End If
                    End If
                    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen upd end

                Next

            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 振替データチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsFurikae(ByVal ds As DataSet) As Boolean

        '種別取得
        Dim InkaTp As String = ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0).Item("INKA_TP").ToString()
        Dim msg As String = _Frm.FunctionKey.F3ButtonName.ToString

        If InkaTp = "50" Then

            Return Me._Vcon.SetErrMessage("E796", New String() {msg})
        End If

        Return True
    End Function

    ''' <summary>
    ''' 実行時チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsJikkouSingleCheck(ByVal ds As DataSet) As Boolean

        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)

        With Me._Frm
            '実行種別
            .cmbJikkou.ItemName() = lgm.Selector({"実行種別", "Execution type", "실행종별", "中国語"})
            .cmbJikkou.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbJikkou) = False Then
                Return False
            End If

            '入荷管理番号
            .lblKanriNoL.ItemName() = lgm.Selector({"入荷管理番号", "I/D management number", "입고 관리 번호", "中国語"})
            .lblKanriNoL.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.lblKanriNoL) = False Then
                Return False
            End If


            Select Case .cmbJikkou.SelectedValue.ToString
                Case "01"   '文書管理

                Case "02"   '現場作業指示取消
                    If Not "01".Equals(.cmbWHSagyoSijiStatus.SelectedValue) AndAlso _
                        Not "02".Equals(.cmbWHSagyoSijiStatus.SelectedValue) Then
                        MyBase.ShowMessage("E01A")
                        Return False
                    End If

                Case "03"   '現場作業指示

                    '入荷ステータス 検品済以外エラー
                    If Not "40".Equals(ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0).Item("INKA_STATE_KB").ToString) Then
                        MyBase.ShowMessage("E00O", {ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0).Item("INKA_NO_L").ToString})
                        Return False
                    End If

                    '現場作業指示ステータスが指示済みの場合エラー
                    If LMB020C.WH_TAB_SIJI_01.Equals(.cmbWHSagyoSijiStatus.SelectedValue) Then
                        MyBase.ShowMessage("E01Q", {ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0).Item("INKA_NO_L").ToString})
                        Return False
                    End If

                    '現場作業進捗区分 完了(取込済)はエラー
                    If LMB020C.WH_TAB_SAGYO_05.Equals(.cmbWHSagyoStatus.SelectedValue) Then
                        MyBase.ShowMessage("E00X", {ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0).Item("INKA_NO_L").ToString})
                        Return False
                    End If

                    '現場作業対象チェックがないとエラー
                    If Not .chkTablet.Checked Then
                        MyBase.ShowMessage("E00P", {ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0).Item("INKA_NO_L").ToString})
                        Return False
                    End If

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 文書管理チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsAddImgSingleCheck() As Boolean

        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)

        With Me._Frm

            With .sprDetail

                Dim max As Integer = .ActiveSheet.Rows.Count - 1
                Dim kanriNo As String = String.Empty
                Dim cnt As Integer = 0
                For i As Integer = 0 To max
                    If LMConst.FLG.ON.Equals(Me._Vcon.GetCellValue(.ActiveSheet.Cells(i, LMB020G.sprDetailDef.DEF.ColNo))) Then
                        cnt = cnt + 1
                    End If
                Next

                If cnt > 1 Then
                    MyBase.ShowMessage("E008")
                    Return False
                End If

                If cnt = 0 Then
                    MyBase.ShowMessage("E009")
                    Return False
                End If

            End With


        End With

        Return True

    End Function
#End Region 'Method

End Class
