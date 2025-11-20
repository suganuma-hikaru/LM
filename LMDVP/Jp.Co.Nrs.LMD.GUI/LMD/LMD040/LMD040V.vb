' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LM     : 在庫サブシステム
'  プログラムID     :  LMD040V : 
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD040Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMD040V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD040F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMDControlV

    ''' <summary>
    ''' チェックリストを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMD040F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = New LMDControlV(handlerClass, DirectCast(frm, Form))

        Me._ChkList = New ArrayList()

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMD040C.EventShubetsu) As Boolean

        '権限の設定
        Dim _Kengen As String = LMUserInfoManager.GetAuthoLv

        '閉じるイベントの場合
        If LMD040C.EventShubetsu.TOJIRU.Equals(eventShubetsu) = False Then
            Select Case _Kengen

                '閲覧者・外部
                '20160114 閲覧ユーザ除外
                'Case LMConst.AuthoKBN.VIEW, LMConst.AuthoKBN.AGENT
                Case LMConst.AuthoKBN.AGENT
                    MyBase.ShowMessage("E016")
                    Return False
            End Select
        End If

        Return True

    End Function

    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheck(ByVal mode As LMD040C.EventShubetsu, ByVal tabFlg As String, ByVal g As LMD040G) As Boolean

        '検索項目のTrim
        Call Me.TrimSpaceTextValue()

        With Me._Frm

            Select Case mode
                Case LMD040C.EventShubetsu.KENSAKU
                    If LMD040C.TAB_SONOTA.Equals(tabFlg) = True Then

                        '【フォーム項目単項目チェック】
                        '営業所
                        .cmbEigyo.ItemName = "営業所"
                        .cmbEigyo.IsHissuCheck = True
                        If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                            Return False
                        End If

                        '荷主コード(大)
                        .txtCust_Cd_L.ItemName = "荷主コード(大)"
                        '.txtCust_Cd_L.IsHissuCheck() = True    '2011.09.07 必須チェック 廃止
                        .txtCust_Cd_L.IsByteCheck = 5
                        .txtCust_Cd_L.IsForbiddenWordsCheck = True
                        If MyBase.IsValidateCheck(.txtCust_Cd_L) = False Then
                            Return False
                        End If

                        '荷主コード(中)
                        .txtCust_Cd_M.ItemName = "荷主コード(中)"
                        .txtCust_Cd_M.IsByteCheck = 2
                        .txtCust_Cd_M.IsForbiddenWordsCheck = True
                        If MyBase.IsValidateCheck(.txtCust_Cd_M) = False Then
                            Return False
                        End If

                        '荷主コード(小)
                        .txtCust_Cd_S.ItemName = "荷主コード(小)"
                        .txtCust_Cd_S.IsByteCheck = 2
                        .txtCust_Cd_S.IsForbiddenWordsCheck = True
                        If MyBase.IsValidateCheck(.txtCust_Cd_S) = False Then
                            Return False
                        End If

                        '荷主コード(極小)
                        .txtCust_Cd_SS.ItemName = "荷主コード(極小)"
                        .txtCust_Cd_SS.IsByteCheck = 2
                        .txtCust_Cd_SS.IsForbiddenWordsCheck = True
                        If MyBase.IsValidateCheck(.txtCust_Cd_SS) = False Then
                            Return False
                        End If

                        'コードが全て空だった場合は名称をクリアする
                        If String.IsNullOrEmpty(.txtCust_Cd_L.TextValue) = True _
                        And String.IsNullOrEmpty(.txtCust_Cd_M.TextValue) = True _
                        And String.IsNullOrEmpty(.txtCust_Cd_S.TextValue) = True _
                        And String.IsNullOrEmpty(.txtCust_Cd_SS.TextValue) = True Then
                            .txtCust_Nm.TextValue = String.Empty
                        End If

                        '(2013.02.14)要望番号1843 検索条件に置き場情報を追加 -- START --
                        '棟
                        .txtTouNo.ItemName = "棟"
                        .txtTouNo.IsForbiddenWordsCheck = True
                        .txtTouNo.IsFullByteCheck = 2
                        If MyBase.IsValidateCheck(.txtTouNo) = False Then
                            Return False
                        End If

                        '室
                        .txtSituNo.ItemName = "室"
                        .txtSituNo.IsForbiddenWordsCheck = True
                        .txtSituNo.IsByteCheck = 2
                        If MyBase.IsValidateCheck(.txtSituNo) = False Then
                            Return False
                        End If

                        'ZONE
                        .txtZoneCd.ItemName = "ZONE"
                        .txtZoneCd.IsForbiddenWordsCheck = True
                        .txtZoneCd.IsByteCheck = 2
                        If MyBase.IsValidateCheck(.txtZoneCd) = False Then
                            Return False
                        End If

                        'ロケーション
                        .txtLocation.ItemName = "ロケーション"
                        .txtLocation.IsForbiddenWordsCheck = True
                        .txtLocation.IsByteCheck = 10
                        If MyBase.IsValidateCheck(.txtLocation) = False Then
                            Return False
                        End If
                        '(2013.02.14)要望番号1843 検索条件に置き場情報を追加 --  END  --

                        '単項目チェック（スプレッド）
                        If Me.IsSpreadInputChk(g) = False Then
                            Return False
                        End If
                    End If

                Case LMD040C.EventShubetsu.MASTER

                    '営業所
                    .cmbEigyo.ItemName = "営業所"
                    .cmbEigyo.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                        Return False
                    End If

                    '荷主コード(大)
                    .txtCust_Cd_L.ItemName = "荷主コード(大)"
                    .txtCust_Cd_L.IsByteCheck = 5
                    .txtCust_Cd_L.IsForbiddenWordsCheck = True
                    If MyBase.IsValidateCheck(.txtCust_Cd_L) = False Then
                        Return False
                    End If

                    '荷主コード(中)
                    .txtCust_Cd_M.ItemName = "荷主コード(中)"
                    .txtCust_Cd_M.IsByteCheck = 2
                    .txtCust_Cd_M.IsForbiddenWordsCheck = True
                    If MyBase.IsValidateCheck(.txtCust_Cd_M) = False Then
                        Return False
                    End If

                    '荷主コード(小)
                    .txtCust_Cd_S.ItemName = "荷主コード(小)"
                    .txtCust_Cd_S.IsByteCheck = 2
                    .txtCust_Cd_S.IsForbiddenWordsCheck = True
                    If MyBase.IsValidateCheck(.txtCust_Cd_S) = False Then
                        Return False
                    End If

                    '荷主コード(極小)
                    .txtCust_Cd_SS.ItemName = "荷主コード(極小)"
                    .txtCust_Cd_SS.IsByteCheck = 2
                    .txtCust_Cd_SS.IsForbiddenWordsCheck = True
                    If MyBase.IsValidateCheck(.txtCust_Cd_SS) = False Then
                        Return False
                    End If

                Case LMD040C.EventShubetsu.PRINT

                    '印刷種別
                    .cmbPrint.ItemName = "印刷種別"
                    .cmbPrint.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbPrint) = False Then
                        Return False
                    End If

                    '印刷種別 + 表示タブチェック
                    Select Case .cmbPrint.SelectedValue.ToString

                        '【置場別・在庫一覧表】、【商品別・在庫一覧表】、【棚卸一覧表】【予定棚卸一覧表】を選択した場合
                        Case LMD040C.PRINT_GOODS_ZAIKOICHIRAN, LMD040C.PRINT_OKIBA_ZAIKOICHIRAN, LMD040C.PRINT_TANAOROSI_ICHIRAN, LMD040C.PRINT_YOTEI_TANAOROSI_ICHIRAN

                            '営業所
                            .cmbEigyo.ItemName = "営業所"
                            .cmbEigyo.IsHissuCheck = True
                            If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                                Return False
                            End If

                            '倉庫
                            .cmbSoko.ItemName = "倉庫"
                            .cmbSoko.IsHissuCheck = True
                            If MyBase.IsValidateCheck(.cmbSoko) = False Then
                                Return False
                            End If

                            '荷主コード(大)
                            .txtCust_Cd_L.ItemName = "荷主コード(大)"
                            .txtCust_Cd_L.IsByteCheck = 5
                            .txtCust_Cd_L.IsForbiddenWordsCheck = True
                            If MyBase.IsValidateCheck(.txtCust_Cd_L) = False Then
                                Return False
                            End If

                            '荷主コード(中)
                            .txtCust_Cd_M.ItemName = "荷主コード(中)"
                            .txtCust_Cd_M.IsByteCheck = 2
                            .txtCust_Cd_M.IsForbiddenWordsCheck = True
                            If MyBase.IsValidateCheck(.txtCust_Cd_M) = False Then
                                Return False
                            End If

                            '荷主コード(小)
                            .txtCust_Cd_S.ItemName = "荷主コード(小)"
                            .txtCust_Cd_S.IsByteCheck = 2
                            .txtCust_Cd_S.IsForbiddenWordsCheck = True
                            If MyBase.IsValidateCheck(.txtCust_Cd_S) = False Then
                                Return False
                            End If

                            '荷主コード(極小)
                            .txtCust_Cd_SS.ItemName = "荷主コード(極小)"
                            .txtCust_Cd_SS.IsByteCheck = 2
                            .txtCust_Cd_SS.IsForbiddenWordsCheck = True
                            If MyBase.IsValidateCheck(.txtCust_Cd_SS) = False Then
                                Return False
                            End If

                            'コードが全て空だった場合は名称をクリアする
                            If String.IsNullOrEmpty(.txtCust_Cd_L.TextValue) = True _
                            And String.IsNullOrEmpty(.txtCust_Cd_M.TextValue) = True _
                            And String.IsNullOrEmpty(.txtCust_Cd_S.TextValue) = True _
                            And String.IsNullOrEmpty(.txtCust_Cd_SS.TextValue) = True Then
                                .txtCust_Nm.TextValue = String.Empty
                            End If

                    End Select

                    'ADD START 2019/8/27 依頼番号:007116,007119
                Case LMD040C.EventShubetsu.EXECUTION

                    '実行種別
                    .cmbExecution.ItemName = "実行種別"
                    .cmbExecution.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbExecution) = False Then
                        Return False
                    End If
                    'ADD END 2019/8/27 依頼番号:007116,007119

            End Select
        End With

        Return True

    End Function

    ''' <summary>
    ''' スプレッドの項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSpreadInputChk(ByVal g As LMD040G) As Boolean

        With Me._Frm

            '【スプレッド項目単項目チェック】
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprGenzaiko)

            '置場
            vCell.SetValidateCell(0, g.sprGenzaikoDef.OKIBA.ColNo)
            vCell.ItemName = Me.SetRepMsgData(g.sprGenzaikoDef.OKIBA.ColName)
            vCell.IsForbiddenWordsCheck = True
            'START YANAI 要望番号705
            'vCell.IsByteCheck = 17
            vCell.IsByteCheck = 19
            'END YANAI 要望番号705
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '商品コード（荷主）
            vCell.SetValidateCell(0, g.sprGenzaikoDef.GOODS_CD_CUST.ColNo)
            vCell.ItemName = Me.SetRepMsgData(g.sprGenzaikoDef.GOODS_CD_CUST.ColName)
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '商品名
            vCell.SetValidateCell(0, g.sprGenzaikoDef.GOODS_NM.ColNo)
            vCell.ItemName = Me.SetRepMsgData(g.sprGenzaikoDef.GOODS_NM.ColName)
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主カテゴリ１
            vCell.SetValidateCell(0, g.sprGenzaikoDef.CUST_CATEGORY_1.ColNo)
            vCell.ItemName = Me.SetRepMsgData(g.sprGenzaikoDef.CUST_CATEGORY_1.ColName)
            vCell.IsForbiddenWordsCheck = True
            'START YANAI 要望番号1065 荷主カテゴリのバイト変更
            'vCell.IsByteCheck = 20
            vCell.IsByteCheck = 25
            'END YANAI 要望番号1065 荷主カテゴリのバイト変更
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'ロットNo
            vCell.SetValidateCell(0, g.sprGenzaikoDef.LOT_NO.ColNo)
            vCell.ItemName = Me.SetRepMsgData(g.sprGenzaikoDef.LOT_NO.ColName)
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '備考小（社内）
            vCell.SetValidateCell(0, g.sprGenzaikoDef.REMARK.ColNo)
            vCell.ItemName = Me.SetRepMsgData(g.sprGenzaikoDef.REMARK.ColName)
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'シリアルNo
            vCell.SetValidateCell(0, g.sprGenzaikoDef.SERIAL_NO.ColNo)
            vCell.ItemName = Me.SetRepMsgData(g.sprGenzaikoDef.SERIAL_NO.ColName)
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主勘定科目ｺｰﾄﾞ1
            vCell.SetValidateCell(0, g.sprGenzaikoDef.CUST_KANJYO_CD_1.ColNo)
            vCell.ItemName = Me.SetRepMsgData(g.sprGenzaikoDef.CUST_KANJYO_CD_1.ColName)
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主勘定科目ｺｰﾄﾞ2
            vCell.SetValidateCell(0, g.sprGenzaikoDef.CUST_KANJYO_CD_2.ColNo)
            vCell.ItemName = Me.SetRepMsgData(g.sprGenzaikoDef.CUST_KANJYO_CD_2.ColName)
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主カテゴリ２
            vCell.SetValidateCell(0, g.sprGenzaikoDef.CUST_CATEGORY_2.ColNo)
            vCell.ItemName = Me.SetRepMsgData(g.sprGenzaikoDef.CUST_CATEGORY_2.ColName)
            vCell.IsForbiddenWordsCheck = True
            'START YANAI 要望番号1065 荷主カテゴリのバイト変更
            'vCell.IsByteCheck = 20
            vCell.IsByteCheck = 25
            'END YANAI 要望番号1065 荷主カテゴリのバイト変更
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名
            vCell.SetValidateCell(0, g.sprGenzaikoDef.CUST_NM.ColNo)
            vCell.ItemName = Me.SetRepMsgData(g.sprGenzaikoDef.CUST_NM.ColName)
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '入荷管理番号
            vCell.SetValidateCell(0, g.sprGenzaikoDef.INKA_NO.ColNo)
            vCell.ItemName = Me.SetRepMsgData(g.sprGenzaikoDef.INKA_NO.ColName)
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 17
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '日陸商品ｺｰﾄﾞ
            vCell.SetValidateCell(0, g.sprGenzaikoDef.GOODS_CD_NRS.ColNo)
            vCell.ItemName = Me.SetRepMsgData(g.sprGenzaikoDef.GOODS_CD_NRS.ColName)
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '在庫No
            vCell.SetValidateCell(0, g.sprGenzaikoDef.ZAI_REC_NO.ColNo)
            vCell.ItemName = Me.SetRepMsgData(g.sprGenzaikoDef.ZAI_REC_NO.ColName)
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '予約届出
            vCell.SetValidateCell(0, g.sprGenzaikoDef.DEST_CD_NM.ColNo)
            vCell.ItemName = Me.SetRepMsgData(g.sprGenzaikoDef.DEST_CD_NM.ColName)
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 15
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '消防コード
            vCell.SetValidateCell(0, g.sprGenzaikoDef.SYOUBOU_CD.ColNo)
            vCell.ItemName = Me.SetRepMsgData(g.sprGenzaikoDef.SYOUBOU_CD.ColName)
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 3
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' vbCrLf,"　"を空文字に変換
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <returns>値</returns>
    ''' <remarks></remarks>
    Private Function SetRepMsgData(ByVal value As String) As String

        value = value.Replace(vbCrLf, String.Empty)
        value = value.Replace("　", String.Empty)
        Return value

    End Function

    ''' <summary>
    ''' 関連項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Public Function isRelationCheck(ByVal frm As LMD040F, ByVal mode As LMD040C.EventShubetsu, ByVal tabFlg As String) As Boolean

        Dim FullByte As Integer = 8
        Dim MinByte As Integer = 0

        With _Frm

            Select Case mode
                Case LMD040C.EventShubetsu.PRINT                         '印刷時

                    '印刷種別 + 表示タブチェック
                    Select Case .cmbPrint.SelectedValue.ToString

                        '在庫履歴帳票(LOT別)、在庫履歴帳票(商品別)
                        Case LMD040C.PRINT_ZAIKO_RIREKI_LOT, LMD040C.PRINT_ZAIKO_RIREKI_GOODS                              '

                            If .tabGenZaiko.Name.Equals(.tabRireki.SelectedTab.Name) = True Then
                                MyBase.ShowMessage("E207")
                                Me._Vcon.SetErrorControl(.cmbPrint)
                                Return False

                            ElseIf .tabInOutHistoryByInka.Name.Equals(.tabRireki.SelectedTab.Name) = True Then
                                If 0 = .sprNyusyukkaN.ActiveSheet.RowCount = True Then
                                    MyBase.ShowMessage("E207")
                                    Me._Vcon.SetErrorControl(.cmbPrint)
                                    Return False
                                End If

                            ElseIf .tabInOutHistoryByOutka.Name.Equals(.tabRireki.SelectedTab.Name) = True Then
                                If 0 = .sprNyusyukkaZ.ActiveSheet.RowCount = True Then
                                    MyBase.ShowMessage("E207")
                                    Me._Vcon.SetErrorControl(.cmbPrint)
                                    Return False
                                End If
                            End If

                            '未選択チェック
                            '★まとめ検証№75修正：在庫履歴帳票(LOT別)、在庫履歴帳票(商品別)以外は未選択チェックをしない
                            If Me.IsExistSelectDataChk() = False Then
                                Return False
                            End If

                    End Select

                Case LMD040C.EventShubetsu.KENSAKU                         '検索時判定処理
                    Select Case tabFlg
                        Case LMD040C.TAB_SONOTA                                           '在庫タブ選択時

                            '入荷日（From）・入荷日（To)大小チェック
                            If String.IsNullOrEmpty(.imdNyukaFrom.TextValue) = False AndAlso _
                                String.IsNullOrEmpty(.imdNyukaTo.TextValue) = False Then
                                If .imdNyukaFrom.TextValue <= .imdNyukaTo.TextValue = False Then
                                    '2015.10.22 tusnehira add
                                    '英語化対応
                                    MyBase.ShowMessage("E615")
                                    'MyBase.ShowMessage("E039", New String() {"入荷日 (To) ", "入荷日 (From) "})
                                    Me._Vcon.SetErrorControl(.imdNyukaTo)
                                    Me._Vcon.SetErrorControl(.imdNyukaFrom)
                                    Return False
                                End If
                            End If
                        Case LMD040C.TAB_INKA                                           '入出荷（入荷ごと）タブ選択時


                            '表示単位（From）・表示単位（To）大小チェック
                            If String.IsNullOrEmpty(.imdHyoujiFrom.TextValue) = False AndAlso _
                                String.IsNullOrEmpty(.imdHyoujiTo.TextValue) = False Then
                                If .imdHyoujiFrom.TextValue <= .imdHyoujiTo.TextValue = False Then
                                    '2015.10.22 tusnehira add
                                    '英語化対応
                                    MyBase.ShowMessage("E772")
                                    'MyBase.ShowMessage("E039", New String() {"表示単位 (To) ", "表示単位 (From) "})
                                    Me._Vcon.SetErrorControl(.imdHyoujiTo)
                                    Me._Vcon.SetErrorControl(.imdHyoujiFrom)
                                    Return False
                                End If
                            End If

                            'セット入力チェック
                            If Me.IsDateSetChk() = False Then
                                Return False
                            End If

                            If Me.IsSelectDataChk() = False Then
                                Return False
                            End If
                        Case LMD040C.TAB_ZAIK                                           '入出荷（在庫ごと）タブ選択時

                            If Me.IsSelectDataChk() = False Then
                                Return False
                            End If

                    End Select

                Case LMD040C.EventShubetsu.HENSHU                          '入出荷編集処理時チェック

                    Select Case .tabRireki.SelectedTab.Name

                        Case .tabInOutHistoryByInka.Name
                            '入出荷（入荷ごと）タブ選択時

                            '選択行判定処理
                            If String.IsNullOrEmpty(tabFlg) = True Then
                                If Me.IsSelectDataChkInka() = False Then
                                    Return False
                                End If
                            End If
                        Case .tabInOutHistoryByOutka.Name
                            '入出荷（在庫ごと）タブ選択時

                            '選択行判定処理
                            If String.IsNullOrEmpty(tabFlg) = True Then
                                If Me.IsSelectDataChkOutka() = False Then
                                    Return False
                                End If
                            End If
                    End Select

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' セット入力チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDateSetChk() As Boolean

        With Me._Frm

            Dim fromDate As String = .imdHyoujiFrom.TextValue
            Dim toDate As String = .imdHyoujiTo.TextValue

            '両方値がない場合、スルー
            If String.IsNullOrEmpty(fromDate) = True _
                AndAlso String.IsNullOrEmpty(toDate) = True _
                Then
                Return True
            End If

            '両方値がある場合、スルー
            If String.IsNullOrEmpty(fromDate) = False _
                AndAlso String.IsNullOrEmpty(toDate) = False _
                Then
                Return True
            End If

            .imdHyoujiTo.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Me._Vcon.SetErrorControl(.imdHyoujiFrom)

            '20151029 tsunehira add Start
            '英語化対応
            Return Me._Vcon.SetErrMessage("E017", New String() {String.Concat(.lblTitleHyojiTani.TextValue, "(From) "), String.Concat(.lblTitleHyojiTani.TextValue, "(To) ")})
            'Return Me._Vcon.SetErrMessage("E017", New String() {"表示単位 (From) ", "表示単位 (To) "})
            '20151029 tsunehira add End
        End With

    End Function

    ''' <summary>
    ''' 未選択チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsExistSelectDataChk() As Boolean

        'チェックリスト初期化
        Me._ChkList = New ArrayList()

        'チェック行リスト取得
        Call Me.getCheckList()

        '未選択チェック
        If Me._Vcon.IsSelectChk(Me._ChkList.Count()) = False Then
            Me.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    ''' 
    Friend Function IsSelectDataChk() As Boolean

        'チェックリスト初期化
        Me._ChkList = New ArrayList()

        'チェック行リスト取得
        Call Me.getCheckList()

        '未選択チェック
        If Me._Vcon.IsSelectChk(Me._ChkList.Count()) = False Then
            Me.ShowMessage("E009")
            Return False
        End If

        '複数選択チェック
        If Me._Vcon.IsSelectOneChk(Me._ChkList.Count()) = False Then
            Me.ShowMessage("E008")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectDataChkInka() As Boolean

        'チェックリスト初期化
        Me._ChkList = New ArrayList()

        'チェック行リスト取得
        Call Me.getCheckListInka()

        '未選択チェック
        If Me._Vcon.IsSelectChk(Me._ChkList.Count()) = False Then
            Me.ShowMessage("E009")
            Return False
        End If

        '複数選択チェック
        If Me._Vcon.IsSelectOneChk(Me._ChkList.Count()) = False Then
            Me.ShowMessage("E008")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectDataChkOutka() As Boolean

        'チェックリスト初期化
        Me._ChkList = New ArrayList()

        'チェック行リスト取得
        Call Me.getCheckListOutka()

        '未選択チェック
        If Me._Vcon.IsSelectChk(Me._ChkList.Count()) = False Then
            Me.ShowMessage("E009")
            Return False
        End If

        '複数選択チェック
        If Me._Vcon.IsSelectOneChk(Me._ChkList.Count()) = False Then
            Me.ShowMessage("E008")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMD040C.SprColumnIndex.DEF

        '選択された行の行番号を取得
        Me._ChkList = Me._Vcon.SprSelectList(defNo, Me._Frm.sprGenzaiko)

        Return Me._ChkList

    End Function

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納（入出荷（入荷ごと））
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckListInka() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMD040C.sprNyukaNIndex.DEF

        '選択された行の行番号を取得
        Me._ChkList = Me._Vcon.SprSelectList(defNo, Me._Frm.sprNyusyukkaN)

        Return Me._ChkList

    End Function

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納（入出荷（在庫ごと））
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckListOutka() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMD040C.sprNyukaZIndex.DEF

        '選択された行の行番号を取得
        Me._ChkList = Me._Vcon.SprSelectList(defNo, Me._Frm.sprNyusyukkaZ)

        Return Me._ChkList

    End Function

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm

            .imdNyukaFrom.TextValue = .imdNyukaFrom.TextValue.Trim()
            .imdNyukaTo.TextValue = .imdNyukaTo.TextValue.Trim()
            .txtCust_Cd_L.TextValue = .txtCust_Cd_L.TextValue.Trim()
            .txtCust_Cd_M.TextValue = .txtCust_Cd_M.TextValue.Trim()
            .txtCust_Cd_S.TextValue = .txtCust_Cd_S.TextValue.Trim()
            .txtCust_Cd_SS.TextValue = .txtCust_Cd_SS.TextValue.Trim()
            .imdHyoujiFrom.TextValue = .imdHyoujiFrom.TextValue.Trim()
            .imdHyoujiTo.TextValue = .imdHyoujiTo.TextValue.Trim()

            'スプレッドのスペース除去
            Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprGenzaiko, 0)

        End With

    End Sub

#End Region 'Method

End Class
