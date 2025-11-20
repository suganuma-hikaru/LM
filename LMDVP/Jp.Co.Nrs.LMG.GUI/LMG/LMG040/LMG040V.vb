' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG040V : 請求処理 請求鑑検索
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMG040Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMG040V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMG040F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMGControlV

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMG040F, ByVal v As LMGControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._ControlV = v

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMG040C.EventShubetsu) As Boolean

        Dim kengen As String = LMUserInfoManager.GetAuthoLv

        Select Case eventShubetsu
            Case LMG040C.EventShubetsu.SINKI_TORIKOMI _
                , LMG040C.EventShubetsu.SINKI_TEGAKI _
                , LMG040C.EventShubetsu.SKYUCSV _
                , LMG040C.EventShubetsu.KAKUTEI  '新規取込、新規手書き、請求データ出力、確定

                '2011/08/03 菱刈 入力者(一般)のユーザーはエラーなしの設定 スタート
                If kengen.Equals(LMConst.AuthoKBN.VIEW) = True _
                OrElse kengen.Equals(LMConst.AuthoKBN.AGENT) = True Then  '10:閲覧者、50:外部の場合エラー
                    'OrElse kengen.Equals(LMConst.AuthoKBN.EDIT) = True _ '一般ユーザのコメント化
                    '2011/08/03 菱刈 入力者(一般)のユーザーはエラーなしの設定 エンド
                    MyBase.ShowMessage("E016")
                    Return False
                End If

            Case LMG040C.EventShubetsu.KENSAKU _
                , LMG040C.EventShubetsu.MST_SANSHO _
                , LMG040C.EventShubetsu.DOUBLE_CLICK  '検索、マスタ参照、ダブルクリック

                If kengen.Equals(LMConst.AuthoKBN.AGENT) = True Then   '50:外部の場合エラー

                    MyBase.ShowMessage("E016")
                    Return False
                End If

            Case LMG040C.EventShubetsu.ENTER  'エンター押下

                If kengen.Equals(LMConst.AuthoKBN.AGENT) = True Then   '50:外部の場合ポップアップ起動なし

                    Return False
                End If

            Case LMG040C.EventShubetsu.SAPOUT   'SAP出力

                Dim userDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", LMUserInfoManager.GetUserID, "'"))
                If userDr.Length = 0 Then
                    'ユーザ未登録はエラー（ログインしている限り有り得ないはず）
                    MyBase.ShowMessage("E016")
                    Return False
                Else
                    'SAP連携実行権限がなければエラー
                    If Not "01".Equals(userDr(0).Item("SAP_LINK_AUTHO").ToString) Then
                        MyBase.ShowMessage("E016")
                        Return False
                    End If
                End If
        End Select

        Return True

    End Function

    'START YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
    ''' <summary>
    ''' 削除押下時入力チェック
    ''' </summary>
    ''' <param name="list">選択行格納配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsDeleteInputChk(ByVal list As ArrayList) As Boolean

        '必須選択チェック
        If list.Count = 0 Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        '単項目チェック
        If Me.IsDeleteSingleChk(list) = False Then
            Return False
        End If

        Return True

    End Function
    'END YANAI 要望番号1040 鑑をまとめて初期化＆削除したい

    ''' <summary>
    ''' 確定押下時入力チェック
    ''' </summary>
    ''' <param name="list">選択行格納配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsKakuteiInputChk(ByVal list As ArrayList) As Boolean

        '必須選択チェック
        If list.Count = 0 Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        '単項目チェック
        If Me.IsKakuteiSingleChk(list) = False Then
            Return False
        End If

        Return True

    End Function

    'START YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
    ''' <summary>
    ''' 初期化押下時入力チェック
    ''' </summary>
    ''' <param name="list">選択行格納配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsClearInputChk(ByVal list As ArrayList) As Boolean

        '必須選択チェック
        If list.Count = 0 Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        '単項目チェック
        If Me.IsClearSingleChk(list) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' SAP出力押下時入力チェック
    ''' </summary>
    ''' <param name="list">選択行格納配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsSapOutChk(ByVal list As ArrayList) As Boolean

        ' 必須選択チェック
        If list.Count = 0 Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        ' 単項目チェック
        If Me.IsSapOutSingleChk(list) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 請求データ出力押下時入力チェック
    ''' </summary>
    ''' <param name="list">選択行格納配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsSkyuCsvChk(ByVal list As ArrayList) As Boolean

        '必須選択チェック
        If list.Count = 0 Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        '進捗区分チェック
        With Me._Frm.sprMeisai.ActiveSheet
            Dim max As Integer = list.Count - 1
            For i As Integer = 0 To max
                If "00".Equals(Me._ControlV.GetCellValue(.Cells(Convert.ToInt32(list(i)), LMG040G.sprDetailDef.STATE_KB.ColNo))) Then
                    MyBase.ShowMessage("E443", New String() {"確定済", ""})
                    Return False
                End If
            Next
        End With

        Return True

    End Function

    ''' <summary>
    ''' SAP取消押下時入力チェック
    ''' </summary>
    ''' <param name="list">選択行格納配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsSapCancelChk(ByVal list As ArrayList) As Boolean

        ' 必須選択チェック
        If list.Count = 0 Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        ' 単項目チェック
        If Me.IsSapCancelSingleChk(list) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 検索押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuInputChk() As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        If Me.IsKensakuSingleChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm"></param>
    ''' <param name="eventShubetsu"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal eventShubetsu As LMG040C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            Return False
        End If

        With Me._Frm

            Select Case objNm
                Case .txtSeikyuCd.Name

                    '禁止文字チェックを行う
                    If String.IsNullOrEmpty(.txtSeikyuCd.TextValue) = False Then
                        .txtSeikyuCd.ItemName = .lblTitleSeikyuCd.Text
                        .txtSeikyuCd.IsForbiddenWordsCheck = True
                        If MyBase.IsValidateCheck(.txtSeikyuCd) = False Then
                            Return False
                        End If

                    Else
                        '2011/08/01 菱刈 検証一覧No3(マスタ参照)
                        '請求先コードがブランクのとき
                        .lblSeikyuNm.TextValue = String.Empty
                    End If

                Case Else

                    Select Case eventShubetsu
                        Case LMG040C.EventShubetsu.MST_SANSHO
                            MyBase.ShowMessage("G005")
                    End Select
                    Return False

            End Select

            Return True

        End With

    End Function

#Region "内部メソッド"

    'START YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
    ''' <summary>
    ''' 削除押下時単項目チェック
    ''' </summary>
    ''' <param name="list">選択行格納配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsDeleteSingleChk(ByVal list As ArrayList) As Boolean

        '******************** ヘッダ項目の入力チェック ********************

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        'Dim userBr As String = LMUserInfoManager.GetNrsBrCd
        'If Me._Frm.lblBrCd.TextValue.Equals(userBr) = False Then
        '    MyBase.ShowMessage("E178", New String() {"削除処理"})
        '    Return False
        'End If

        '******************** Spread項目の入力チェック ********************
        With Me._Frm.sprMeisai.ActiveSheet

            Dim max As Integer = list.Count - 1
            For i As Integer = 0 To max

                '進捗区分チェック
                If Me._ControlV.GetCellValue(.Cells(Convert.ToInt32(list(i)), LMG040G.sprDetailDef.STATE_KB.ColNo)).Equals("00") = False Then
                    MyBase.ShowMessage("E260", New String() {"進捗区分が未確定以外のレコード"})
                    Return False
                End If

                '赤黒区分チェック
                If Me._ControlV.GetCellValue(.Cells(Convert.ToInt32(list(i)), LMG040G.sprDetailDef.AKAKURO_KB.ColNo)).Equals("01") Then
                    MyBase.ShowMessage("E028", New String() {"赤データ", "確定"})
                    Return False
                End If
#If False Then      'DEL 2018/08/09 依頼番号 : 002136  
                '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen add start
                '新黒チェック
                If Me._ControlV.GetCellValue(.Cells(Convert.ToInt32(list(i)), LMG040G.sprDetailDef.SEIKYU_NO_RELATED.ColNo)).Trim <> String.Empty Then
                    MyBase.ShowMessage("E028", New String() {"新黒データ", "削除"})
                    Return False
                End If
                '2018/03/19 000584 20171109【LMS】運賃編集_請求鑑赤黒発行後に編集ができない対応 Annen add end

#End If

#If True Then      'ADD 2018/08/21 依頼番号 : 002136  
                'SYS_DEL_FLGチェック
                If Me._ControlV.GetCellValue(.Cells(Convert.ToInt32(list(i)), LMG040G.sprDetailDef.SYS_DEL_FLG.ColNo)).Equals("1") Then
                    MyBase.ShowMessage("E028", New String() {"削除済みデータ", "削除"})
                    Return False
                End If

#End If
            Next

        End With

        Return True

    End Function
    'END YANAI 要望番号1040 鑑をまとめて初期化＆削除したい

    ''' <summary>
    ''' 確定押下時単項目チェック
    ''' </summary>
    ''' <param name="list">選択行格納配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsKakuteiSingleChk(ByVal list As ArrayList) As Boolean

        '******************** ヘッダ項目の入力チェック ********************

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        'Dim userBr As String = LMUserInfoManager.GetNrsBrCd
        'If Me._Frm.lblBrCd.TextValue.Equals(userBr) = False Then
        '    MyBase.ShowMessage("E178", New String() {"確定処理"})
        '    Return False
        'End If

        '******************** Spread項目の入力チェック ********************
        With Me._Frm.sprMeisai.ActiveSheet

            Dim max As Integer = list.Count - 1
            For i As Integer = 0 To max

                '進捗区分チェック
                If Me._ControlV.GetCellValue(.Cells(Convert.ToInt32(list(i)), LMG040G.sprDetailDef.STATE_KB.ColNo)).Equals("00") = False Then
                    MyBase.ShowMessage("E260", New String() {"進捗区分が未確定以外のレコード"})
                    Return False
                End If

                '赤黒区分チェック
                If Me._ControlV.GetCellValue(.Cells(Convert.ToInt32(list(i)), LMG040G.sprDetailDef.AKAKURO_KB.ColNo)).Equals("01") Then
                    MyBase.ShowMessage("E028", New String() {"赤データ", "確定"})
                    Return False
                End If

#If True Then      'ADD 2018/08/21 依頼番号 : 002136  
                'SYS_DEL_FLGチェック
                If Me._ControlV.GetCellValue(.Cells(Convert.ToInt32(list(i)), LMG040G.sprDetailDef.SYS_DEL_FLG.ColNo)).Equals("1") Then
                    MyBase.ShowMessage("E028", New String() {"削除済みデータ", "確定"})
                    Return False
                End If

#End If

            Next

        End With

        Return True

    End Function

    'START YANAI 要望番号1040 鑑をまとめて初期化＆削除したい
    ''' <summary>
    ''' 初期化押下時単項目チェック
    ''' </summary>
    ''' <param name="list">選択行格納配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsClearSingleChk(ByVal list As ArrayList) As Boolean

        '******************** ヘッダ項目の入力チェック ********************

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        'Dim userBr As String = LMUserInfoManager.GetNrsBrCd
        'If Me._Frm.lblBrCd.TextValue.Equals(userBr) = False Then
        '    MyBase.ShowMessage("E178", New String() {"初期化処理"})
        '    Return False
        'End If

        '******************** Spread項目の入力チェック ********************
        With Me._Frm.sprMeisai.ActiveSheet

            Dim max As Integer = list.Count - 1
            For i As Integer = 0 To max

                '進捗区分チェック
                If Me._ControlV.GetCellValue(.Cells(Convert.ToInt32(list(i)), LMG040G.sprDetailDef.STATE_KB.ColNo)).Equals("01") = False AndAlso _
                    Me._ControlV.GetCellValue(.Cells(Convert.ToInt32(list(i)), LMG040G.sprDetailDef.STATE_KB.ColNo)).Equals("02") = False Then
                    MyBase.ShowMessage("E260", New String() {"進捗区分が確定もしくは印刷済以外のレコード"})
                    Return False
                End If

                '赤黒区分チェック
                If Me._ControlV.GetCellValue(.Cells(Convert.ToInt32(list(i)), LMG040G.sprDetailDef.AKAKURO_KB.ColNo)).Equals("01") Then
                    MyBase.ShowMessage("E028", New String() {"赤データ", "確定"})
                    Return False
                End If

            Next

        End With

        Return True

    End Function
    'END YANAI 要望番号1040 鑑をまとめて初期化＆削除したい

    ''' <summary>
    ''' SAP出力押下時単項目チェック
    ''' </summary>
    ''' <param name="list">選択行格納配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSapOutSingleChk(ByVal list As ArrayList) As Boolean

        ' 請求日チェック用に区分マスタを検索し、SAP出力連携可能日付を取得する。
        Dim filter As String = String.Empty
        filter = String.Empty
        filter = String.Concat(filter, "KBN_GROUP_CD = '", LMG040C.KBN_SAP_OUT_START_DATE, "'")
        filter = String.Concat(filter, " AND KBN_CD = '", "01", "'")
        filter = String.Concat(filter, " AND SYS_DEL_FLG = '0'")
        Dim kbnDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)
        If kbnDr.Length = 0 OrElse
                kbnDr(0).Item("KBN_NM1").ToString().Trim().Length <> 8 Then
            MyBase.ShowMessage("E320",
                    New String() {
                        String.Concat("SAP出力連携可能日付が区分マスタ（区分グループコード='",
                                      LMG040C.KBN_SAP_OUT_START_DATE, "'）に未登録、または不適切な設定値"),
                        String.Concat(LMG040G.sprDetailDef.SEIKYUSYO_DATE.ColName, "の妥当性をチェック")})
            Return False
        End If
        Dim sapOutStartDate As String = DateFormatUtility.EditSlash(kbnDr(0).Item("KBN_NM1").ToString())

        With Me._Frm.sprMeisai.ActiveSheet

            Dim max As Integer = list.Count - 1
            For i As Integer = 0 To max
                ' 進捗区分チェック
                If Me._ControlV.GetCellValue(.Cells(Convert.ToInt32(list(i)), LMG040G.sprDetailDef.STATE_KB.ColNo)).Equals(LMG040C.STATE_INSATU_ZUMI) = False Then
                    MyBase.ShowMessage("E260",
                        New String() {String.Concat(Me._Frm.lblTitleStateKbn.Text, "が", Me._Frm.chkInsatuZumi.Text, "以外のレコード")})
                    Return False
                End If

                ' 請求書日付チェック
                If Me._ControlV.GetCellValue(.Cells(Convert.ToInt32(list(i)), LMG040G.sprDetailDef.SEIKYUSYO_DATE.ColNo)) < sapOutStartDate Then
                    MyBase.ShowMessage("E260",
                        New String() {
                            String.Concat(LMG040G.sprDetailDef.SEIKYUSYO_DATE.ColName, "が",
                                          sapOutStartDate, "より前のレコード")})
                    Return False
                End If

                ' SYS_DEL_FLGチェック
                If Me._ControlV.GetCellValue(.Cells(Convert.ToInt32(list(i)), LMG040G.sprDetailDef.SYS_DEL_FLG.ColNo)).Equals("1") Then
                    MyBase.ShowMessage("E028", New String() {"削除済みデータ", Me._Frm.FunctionKey.F7ButtonName})
                    Return False
                End If
            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' SAP取消押下時単項目チェック
    ''' </summary>
    ''' <param name="list">選択行格納配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSapCancelSingleChk(ByVal list As ArrayList) As Boolean

        ' 請求日チェック用に区分マスタを検索し、SAP出力連携可能日付を取得する。
        Dim filter As String = String.Empty
        filter = String.Empty
        filter = String.Concat(filter, "KBN_GROUP_CD = '", LMG040C.KBN_SAP_OUT_START_DATE, "'")
        filter = String.Concat(filter, " AND KBN_CD = '", "01", "'")
        filter = String.Concat(filter, " AND SYS_DEL_FLG = '0'")
        Dim kbnDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)
        If kbnDr.Length = 0 OrElse
                kbnDr(0).Item("KBN_NM1").ToString().Trim().Length <> 8 Then
            MyBase.ShowMessage("E320",
                    New String() {
                        String.Concat("SAP出力連携可能日付が区分マスタ（区分グループコード='",
                                      LMG040C.KBN_SAP_OUT_START_DATE, "'）に未登録、または不適切な設定値"),
                        String.Concat(LMG040G.sprDetailDef.SEIKYUSYO_DATE.ColName, "の妥当性をチェック")})
            Return False
        End If
        Dim sapOutStartDate As String = DateFormatUtility.EditSlash(kbnDr(0).Item("KBN_NM1").ToString())

        With Me._Frm.sprMeisai.ActiveSheet

            Dim max As Integer = list.Count - 1
            For i As Integer = 0 To max
                ' 進捗区分チェック
                If Me._ControlV.GetCellValue(.Cells(Convert.ToInt32(list(i)), LMG040G.sprDetailDef.STATE_KB.ColNo)).Equals(LMG040C.STATE_KEIRI_TORIKOMI_ZUMI) = False Then
                    MyBase.ShowMessage("E260",
                        New String() {String.Concat(Me._Frm.lblTitleStateKbn.Text, "が", Me._Frm.chkKeiriTorikomi.Text, "以外のレコード")})
                    Return False
                End If

                ' 請求書日付チェック
                If Me._ControlV.GetCellValue(.Cells(Convert.ToInt32(list(i)), LMG040G.sprDetailDef.SEIKYUSYO_DATE.ColNo)) < sapOutStartDate Then
                    MyBase.ShowMessage("E260",
                        New String() {
                            String.Concat(LMG040G.sprDetailDef.SEIKYUSYO_DATE.ColName, "が",
                                          sapOutStartDate, "より前のレコード")})
                    Return False
                End If

                ' SYS_DEL_FLGチェック
                ' 本チェック実装時点では、削除済みデータの抽出条件には「進捗区分 < 経理取込済」があるため、
                ' 先に必ず上記の進捗区分チェックに該当し、以下には至らない。
                If Me._ControlV.GetCellValue(.Cells(Convert.ToInt32(list(i)), LMG040G.sprDetailDef.SYS_DEL_FLG.ColNo)).Equals("1") Then
                    MyBase.ShowMessage("E028", New String() {"削除済みデータ", Me._Frm.FunctionKey.F8ButtonName})
                    Return False
                End If
            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        'ヘッダ項目のTrim
        Call Me.TrimSpaceHeaderTextValue()

        'スプレッドのTrim
        Call Me.TrimSpaceSprTextValue()

    End Sub

    ''' <summary>
    ''' ヘッダ項目のTrim
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceHeaderTextValue()

        With Me._Frm

            .txtSeikyuCd.TextValue = .txtSeikyuCd.TextValue.Trim()
            .txtSeikyuNo.TextValue = .txtSeikyuNo.TextValue.Trim()

        End With

    End Sub

    ''' <summary>
    ''' スプレッド部のTrim
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceSprTextValue()

        Dim aCell As Cell = Nothing

        With Me._Frm.sprMeisai

            Dim maxCol As Integer = .ActiveSheet.Columns.Count - 1
            For i As Integer = 0 To maxCol

                aCell = .ActiveSheet.Cells(0, i)
                If TypeOf aCell.Editor Is CellType.ComboBoxCellType = True _
                OrElse TypeOf aCell.Editor Is CellType.CheckBoxCellType = True _
                OrElse TypeOf aCell.Editor Is CellType.DateTimeCellType = True _
                OrElse TypeOf aCell.Editor Is CellType.NumberCellType = True Then

                Else
                    .SetCellValue(0, i, Me._ControlV.GetCellValue(aCell))
                End If
            Next

        End With

    End Sub

    ''' <summary>
    ''' 検索押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleChk() As Boolean

        With Me._Frm

            '******************** ヘッダ項目の入力チェック ********************
            '営業所
            .cmbBr.ItemName = .lblTitleEigyo.Text
            .cmbBr.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbBr) = False Then
                Return False
            End If

            '請求月
            If .imdSeikyuYm.IsDateFullByteCheck = False Then
                MyBase.ShowMessage("E038", New String() {.lblTitleSeikyuYm.Text, "6"})
                Return False
            End If


            '請求先コード
            If String.IsNullOrEmpty(.txtSeikyuCd.TextValue) = False Then
                .txtSeikyuCd.ItemName = .lblTitleSeikyuCd.Text
                .txtSeikyuCd.IsForbiddenWordsCheck = True
                .txtSeikyuCd.IsByteCheck = 7
                If MyBase.IsValidateCheck(.txtSeikyuCd) = False Then
                    Return False
                End If
            End If

            '請求書番号
            If String.IsNullOrEmpty(.txtSeikyuNo.TextValue) = False Then
                .txtSeikyuNo.ItemName = .lblTitleSeikyuNo.Text
                .txtSeikyuNo.IsForbiddenWordsCheck = True
                .txtSeikyuNo.IsByteCheck = 7
                If MyBase.IsValidateCheck(.txtSeikyuNo) = False Then
                    Return False
                End If
            End If

#If True Then   'ADD 2018/08/21 依頼番号 : 002136  
            If ("1").Equals(.chkKeiriTorikeshi.GetBinaryValue()) = True Then
                '削除済み経理戻し(黒)チェック指定時

                '請求月
                If String.Empty.Equals(.imdSeikyuYm.TextValue) = True Then
                    MyBase.ShowMessage("E028", New String() {"削除済み経理戻し(黒)で", "請求月指定なしで検索"})
                    .imdSeikyuYm.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdSeikyuYm.Focus()
                    Return False
                End If

                '請求先コード
                If String.Empty.Equals(.txtSeikyuCd.TextValue) = True Then
                    MyBase.ShowMessage("E028", New String() {"削除済み経理戻し(黒)で", "請求先コード指定なしで検索"})
                    .txtSeikyuCd.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .txtSeikyuCd.Focus()

                    Return False
                End If

            End If
#End If


            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprMeisai)

            '請求先名
            vCell.SetValidateCell(0, LMG040G.sprDetailDef.SEIKYU_NM.ColNo)
            vCell.ItemName = LMG040G.sprDetailDef.SEIKYU_NM.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '元請求書番号
            vCell.SetValidateCell(0, LMG040G.sprDetailDef.SEIKYU_NO_RELATED.ColNo)
            vCell.ItemName = LMG040G.sprDetailDef.SEIKYU_NO_RELATED.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 7
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#End Region

#End Region

End Class
